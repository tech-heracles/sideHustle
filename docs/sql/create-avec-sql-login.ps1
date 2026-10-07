# Krijon (ose rifreskon) perdoruesin SQL "AVEC" me te cilin AVEC Accounting lidhet me databazat e kompanive.
# I njejti perdorues/fjalekalim ne cdo server on-premise: Manager nuk i kerkon me, i vendos backend-i
# (sekreti AVEC_SQL_PASSWORD i funksioneve duhet te kete te njejtin fjalekalim).
#
# Perdorimi (PowerShell si administrator i SQL Server-it, me Windows authentication):
#   .\create-avec-sql-login.ps1 -Server 'localhost\HERACLES' -Database 'web_vg' -Password '<fjalekalimi>'
#   (disa databaza: -Database 'db1','db2')
#
# Login-i: pa politike/skadim fjalekalimi (llogari sherbimi), db_owner ne secilen databaze te dhene.
# Serveri duhet te pranoje edhe SQL Server authentication (Mixed Mode); skripti paralajmeron kur jo.
# I sigurt per t'u ekzekutuar disa here.
param(
    [Parameter(Mandatory = $true)] [string]   $Server,
    [Parameter(Mandatory = $true)] [string[]] $Database,
    [Parameter(Mandatory = $true)] [string]   $Password
)
$ErrorActionPreference = 'Stop'
$pw = $Password.Replace("'", "''")

function Sql([string] $db, [string] $query) {
    $out = & sqlcmd -S $Server -E -C -d $db -b -h -1 -W -Q ("SET NOCOUNT ON; " + $query)
    if ($LASTEXITCODE -ne 0) { throw "sqlcmd deshtoi ne $db`n$out" }
    return $out
}

$vetemWindows = (Sql 'master' "SELECT CAST(SERVERPROPERTY('IsIntegratedSecurityOnly') AS int)") | Select-Object -First 1
if ("$vetemWindows".Trim() -eq '1') {
    Write-Warning "Serveri pranon vetem Windows authentication. Aktivizoni 'SQL Server and Windows Authentication mode' (SSMS > Server Properties > Security) dhe rinisni sherbimin, perndryshe AVEC nuk lidhet."
}

Sql 'master' @"
IF SUSER_ID(N'AVEC') IS NULL
    CREATE LOGIN [AVEC] WITH PASSWORD = N'$pw', CHECK_POLICY = OFF, CHECK_EXPIRATION = OFF, DEFAULT_DATABASE = [master];
ELSE
    ALTER LOGIN [AVEC] WITH PASSWORD = N'$pw', CHECK_POLICY = OFF, CHECK_EXPIRATION = OFF;
ALTER LOGIN [AVEC] ENABLE;
"@ | Out-Null
Write-Host "Login AVEC gati ne $Server"

foreach ($db in $Database) {
    Sql $db @"
IF DATABASE_PRINCIPAL_ID(N'AVEC') IS NULL
    CREATE USER [AVEC] FOR LOGIN [AVEC];
ELSE
    ALTER USER [AVEC] WITH LOGIN = [AVEC];
IF IS_ROLEMEMBER(N'db_owner', N'AVEC') = 0
    ALTER ROLE [db_owner] ADD MEMBER [AVEC];
"@ | Out-Null
    Write-Host "  $db : perdoruesi AVEC (db_owner)"
}

# prova: lidhja me vete perdoruesin AVEC
foreach ($db in $Database) {
    $r = & sqlcmd -S $Server -U AVEC -P $Password -C -d $db -b -h -1 -W -Q "SET NOCOUNT ON; SELECT DB_NAME()"
    if ($LASTEXITCODE -eq 0) { Write-Host "  $db : lidhja me AVEC funksionon" -ForegroundColor Green }
    else { Write-Warning "  $db : lidhja me AVEC deshtoi: $r" }
}
