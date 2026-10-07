# Nderron celesin e instalimit AVEC (avecInstallations/<id> ne Firebase) dhe e shkruan ne avecLicense.config
# te site-it (dhe te projektit, nese ekziston), pa e shfaqur ne ekran. Rinis aplikacionin (te gjithe dalin).
# Perdorimi (PowerShell):
#   .\rotate-avec-key.ps1 -ServiceAccount 'C:\...\serviceAccountKey.json'
param(
    [Parameter(Mandatory = $true)] [string] $ServiceAccount,
    [string] $InstallationId = 'avec-local',
    [string] $Name = 'AVEC Accounting - server lokal',
    [string[]] $Configs = @('C:\Sites\AlphaWeb\avecLicense.config', "$PSScriptRoot\..\PlatinumWeb\avecLicense.config"),
    [string] $Site = 'C:\Sites\AlphaWeb'
)
$ErrorActionPreference = 'Stop'
$functions = Join-Path $PSScriptRoot '..\..\manager-backend\functions'
if (-not (Test-Path (Join-Path $functions 'scripts\avec-admin.mjs'))) { throw "Nuk u gjet manager-backend\functions\scripts\avec-admin.mjs" }

$env:GOOGLE_APPLICATION_CREDENTIALS = (Resolve-Path $ServiceAccount).Path
Push-Location $functions
try {
    $out = & node scripts/avec-admin.mjs installation $InstallationId $Name --rotate 2>&1 | Out-String
} finally {
    Pop-Location
    Remove-Item Env:GOOGLE_APPLICATION_CREDENTIALS -ErrorAction SilentlyContinue
}
$m = [regex]::Match($out, ':\s*([A-Za-z0-9_-]{40,})\s*$', 'Multiline')
if (-not $m.Success) { throw "Celesi i ri nuk u lexua nga avec-admin.mjs:`n$($out -replace '[A-Za-z0-9_-]{40,}', '***')" }
$key = $m.Groups[1].Value

foreach ($c in $Configs) {
    if (-not (Test-Path $c)) { continue }
    $text = [IO.File]::ReadAllText($c)
    $text = [regex]::Replace($text, '(key="AvecInstallationKey"\s+value=")[^"]*"', { param($x) $x.Groups[1].Value + $key + '"' })
    $text = [regex]::Replace($text, '(key="AvecInstallationId"\s+value=")[^"]*"', { param($x) $x.Groups[1].Value + $InstallationId + '"' })
    [IO.File]::WriteAllText($c, $text, (New-Object Text.UTF8Encoding($false)))
    Write-Host "Celesi i ri u vendos ne $c"
}

# avecLicense.config lexohet vetem ne nisje te aplikacionit: rinisja
(Get-Item (Join-Path $Site 'Web.config')).LastWriteTime = Get-Date
Write-Host "Celesi u nderrua per '$InstallationId' dhe site-i u rinis." -ForegroundColor Green
