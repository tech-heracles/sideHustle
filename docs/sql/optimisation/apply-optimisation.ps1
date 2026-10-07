# Apliko skriptet e optimizimit (01, 02, 04-13) ne nje databaze, me rradhe; ndalon te gabimi i pare.
# Perdorimi (PowerShell, nga kjo dosje ose me rrugen e plote):
#   .\apply-optimisation.ps1 -Server 'localhost\HERACLES' -Database 'web_vg'
# Kerkon sqlcmd. Lidhja me Windows authentication (-E).
param(
    [Parameter(Mandatory = $true)] [string] $Server,
    [Parameter(Mandatory = $true)] [string] $Database
)
$ErrorActionPreference = 'Stop'
$scripts = @(
    '01-drop-redundant-indexes.sql',
    '02-file-growth.sql',
    '04-eksport-shitje.sql',
    '05-eksport-magazina.sql',
    '06-kontroll-gjendje.sql',
    '07-kontroll-dublikate.sql',
    '08-ruajtje-magazine.sql',
    '09-cmim-mesatar.sql',
    '10-nr-reference.sql',
    '11-renditja-magazine.sql',
    '12-kontroll-dublikate-plan.sql',
    '13-renditja-koka-magazine.sql'
)
foreach ($s in $scripts) {
    $path = Join-Path $PSScriptRoot $s
    $t = Get-Date
    & sqlcmd -S $Server -E -C -d $Database -I -b -t 0 -i $path | Out-Null
    if ($LASTEXITCODE -ne 0) {
        Write-Host "$s DESHTOI (kodi $LASTEXITCODE). Ndalova; rregullojeni dhe riniseni (skriptet mund te riekzekutohen)." -ForegroundColor Red
        exit 1
    }
    Write-Host ("{0} ok ({1:N0} s)" -f $s, ((Get-Date) - $t).TotalSeconds)
}
Write-Host "Te gjitha skriptet u aplikuan ne $Database." -ForegroundColor Green
