#Requires -RunAsAdministrator
# Prepares IIS on a fresh client PC/server for AVEC Accounting. Safe to run more than once.
# Run in an elevated PowerShell:
#   Set-ExecutionPolicy -Scope Process Bypass -Force
#   .\setup-iis.ps1                      # defaults: C:\Sites\AVEC-Accounting-2026-10-21\site, site AlphaWeb, pool AlphaWebPool, port 80
#
# What it does:
#   1. Installs IIS + ASP.NET 4.x (Windows 10/11 or Windows Server)
#   2. Checks .NET Framework >= 4.7.1
#   3. Creates the site folder (+ log, App_Data) and machineKey.config with fresh random keys
#   4. Creates placeholder connectionStrings.config / avecLicense.config if missing (fill them in afterwards)
#   5. Creates the app pool (.NET v4.0, Integrated, 64-bit, idle timeout 0, AlwaysRunning, recycle 03:00)
#   6. Creates the site on the given port and stops "Default Web Site" if it holds that port
#   7. Grants the app pool Modify rights on the site folder
#   8. Opens the port in Windows Firewall (Domain/Private networks)
# It does NOT copy the build or touch SQL Server.
param(
    [string] $SitePath = 'C:\Sites\AVEC-Accounting-2026-10-21\site',
    [string] $SiteName = 'AlphaWeb',
    [string] $PoolName = 'AlphaWebPool',
    [int]    $Port     = 80
)
$ErrorActionPreference = 'Stop'
function Step($msg) { Write-Host "`n== $msg" -ForegroundColor Cyan }
function Ok($msg)   { Write-Host "   ok: $msg" -ForegroundColor Green }
function Warn($msg) { Write-Warning $msg }

# ---------------------------------------------------------------- 1. IIS features
Step 'IIS + ASP.NET 4.x'
$isServer = (Get-CimInstance Win32_OperatingSystem).ProductType -ne 1
$restart = $false
if ($isServer) {
    $r = Install-WindowsFeature Web-Server, Web-Default-Doc, Web-Static-Content, Web-Http-Errors, `
        Web-Http-Logging, Web-Stat-Compression, Web-Dyn-Compression, Web-Filtering, `
        Web-Net-Ext45, Web-Asp-Net45, Web-ISAPI-Ext, Web-ISAPI-Filter, Web-AppInit, Web-Mgmt-Console
    $restart = $r.RestartNeeded -eq 'Yes'
} else {
    $features = 'IIS-WebServerRole', 'IIS-WebServer', 'IIS-CommonHttpFeatures', 'IIS-DefaultDocument',
        'IIS-StaticContent', 'IIS-HttpErrors', 'IIS-HttpLogging', 'IIS-HttpCompressionStatic',
        'IIS-HttpCompressionDynamic', 'IIS-RequestFiltering', 'IIS-NetFxExtensibility45',
        'IIS-ISAPIExtensions', 'IIS-ISAPIFilter', 'IIS-ASPNET45', 'IIS-ApplicationInit',
        'IIS-ManagementConsole'
    $r = Enable-WindowsOptionalFeature -Online -All -NoRestart -FeatureName $features
    $restart = [bool]$r.RestartNeeded
}
Ok ("IIS features installed" + $(if ($restart) { ' (Windows wants a restart; finish this script first)' } else { '' }))

# ---------------------------------------------------------------- 2. .NET Framework
Step '.NET Framework'
$release = (Get-ItemProperty 'HKLM:\SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full' -ErrorAction SilentlyContinue).Release
if (-not $release -or $release -lt 461308) {
    Warn ".NET Framework 4.7.1+ not found (Release=$release). Install .NET Framework 4.8 before using the site."
} else { Ok ".NET Framework release $release" }

Import-Module WebAdministration

# ---------------------------------------------------------------- 3/4. Folder + config files
Step "Site folder $SitePath"
foreach ($d in $SitePath, "$SitePath\log", "$SitePath\App_Data") {
    if (-not (Test-Path $d)) { New-Item -ItemType Directory -Path $d | Out-Null }
}
Ok 'folders exist'

function RandomHex([int] $bytes) {
    $b = New-Object byte[] $bytes
    [System.Security.Cryptography.RandomNumberGenerator]::Create().GetBytes($b)
    -join ($b | ForEach-Object { $_.ToString('X2') })
}
$mk = Join-Path $SitePath 'machineKey.config'
if (-not (Test-Path $mk)) {
    "<machineKey validation=`"HMACSHA256`" validationKey=`"$(RandomHex 64)`" decryption=`"AES`" decryptionKey=`"$(RandomHex 32)`" />" |
        Set-Content $mk -Encoding UTF8
    Ok 'machineKey.config created with new keys'
} else { Ok 'machineKey.config already there (kept)' }

$cs = Join-Path $SitePath 'connectionStrings.config'
if (-not (Test-Path $cs)) {
    @'
<?xml version="1.0" encoding="utf-8"?>
<connectionStrings>
  <add name="connStringAlpha"
       connectionString="Data Source=localhost\INSTANCE;Initial Catalog=DATABASE;User ID=AVEC;Password=PASSWORD;Connection Lifetime=0;Min pool size=1;Max pool size=200;Pooling=true"
       providerName="System.Data.SqlClient" />
</connectionStrings>
'@ | Set-Content $cs -Encoding UTF8
    Warn "connectionStrings.config created with PLACEHOLDERS - fill in instance, database and password once SQL Server is ready."
} else { Ok 'connectionStrings.config already there (kept)' }

$lic = Join-Path $SitePath 'avecLicense.config'
if (-not (Test-Path $lic)) {
    @'
<?xml version="1.0" encoding="utf-8"?>
<appSettings>
  <add key="AvecLicenseUrl" value="https://europe-west8-poss-66570.cloudfunctions.net/avecLicenses" />
  <add key="AvecInstallationId" value="" />
  <add key="AvecInstallationKey" value="" />
</appSettings>
'@ | Set-Content $lic -Encoding UTF8
    Warn "avecLicense.config created EMPTY - fill in AvecInstallationId and AvecInstallationKey for this server."
} else { Ok 'avecLicense.config already there (kept)' }

# ---------------------------------------------------------------- 5. App pool
Step "App pool $PoolName"
if (-not (Test-Path "IIS:\AppPools\$PoolName")) { New-WebAppPool $PoolName | Out-Null }
$p = "IIS:\AppPools\$PoolName"
Set-ItemProperty $p -Name managedRuntimeVersion -Value 'v4.0'
Set-ItemProperty $p -Name managedPipelineMode -Value 'Integrated'
Set-ItemProperty $p -Name enable32BitAppOnWin64 -Value $false
Set-ItemProperty $p -Name processModel.idleTimeout -Value ([TimeSpan]::Zero)
Set-ItemProperty $p -Name startMode -Value 'AlwaysRunning'
Set-ItemProperty $p -Name recycling.periodicRestart.time -Value ([TimeSpan]::Zero)
Clear-ItemProperty $p -Name recycling.periodicRestart.schedule
New-ItemProperty $p -Name recycling.periodicRestart.schedule -Value @{ value = '03:00:00' } | Out-Null
Ok '.NET v4.0, Integrated, 64-bit, idle timeout 0, AlwaysRunning, recycle 03:00'

# ---------------------------------------------------------------- 6. Site
Step "Site $SiteName on port $Port"
Get-Website | Where-Object { $_.Name -ne $SiteName -and $_.Bindings.Collection.bindingInformation -contains "*:${Port}:" } |
    ForEach-Object {
        Stop-Website $_.Name
        Set-ItemProperty "IIS:\Sites\$($_.Name)" -Name serverAutoStart -Value $false
        Ok "stopped '$($_.Name)' (it was using port $Port)"
    }
if (-not (Test-Path "IIS:\Sites\$SiteName")) {
    New-Website -Name $SiteName -PhysicalPath $SitePath -ApplicationPool $PoolName -Port $Port | Out-Null
} else {
    Set-ItemProperty "IIS:\Sites\$SiteName" -Name physicalPath -Value $SitePath
    Set-ItemProperty "IIS:\Sites\$SiteName" -Name applicationPool -Value $PoolName
}
Set-ItemProperty "IIS:\Sites\$SiteName" -Name applicationDefaults.preloadEnabled -Value $true
Ok "site points to $SitePath"

# ---------------------------------------------------------------- 7. Permissions
Step 'Folder permissions'
& icacls $SitePath /grant "IIS AppPool\${PoolName}:(OI)(CI)M" /T /Q | Out-Null
if ($LASTEXITCODE -ne 0) { throw "icacls failed ($LASTEXITCODE)" }
Ok "IIS AppPool\$PoolName has Modify on $SitePath"

# ---------------------------------------------------------------- 8. Firewall
Step 'Firewall'
$rule = "AVEC HTTP $Port"
if (-not (Get-NetFirewallRule -DisplayName $rule -ErrorAction SilentlyContinue)) {
    New-NetFirewallRule -DisplayName $rule -Direction Inbound -Protocol TCP -LocalPort $Port -Action Allow -Profile Domain, Private | Out-Null
}
Ok "port $Port open for Domain/Private networks"

# ---------------------------------------------------------------- Summary
Step 'Done'
if (-not (Test-Path (Join-Path $SitePath 'Web.config'))) {
    Warn "No Web.config in $SitePath yet - copy the Release build there (without log\, App_Data\ and the three *.config files above)."
}
if (-not (Test-Path (Join-Path $SitePath 'bin\DevExpress.Xpo.v18.2.dll'))) {
    Warn "DevExpress 18.2.7 DLLs not found in bin\ (e.g. DevExpress.Xpo.v18.2.dll). Copy them from the old server if the build lacks them."
}
Write-Host @"

Next:
  1. Copy the build into $SitePath (if not done yet).
  2. After SQL Server is installed: create the AVEC login (docs\sql\create-avec-sql-login.ps1)
     and fill in $cs
  3. Fill in $lic
  4. Restart-WebAppPool $PoolName   then open http://localhost:$Port/  (first load takes 1-3 minutes)
"@
if ($restart) { Warn 'Restart Windows once before testing the site.' }
