# Apliko skriptet e optimizimit (00, 01, 02, 04-27) ne nje databaze, me rradhe; ndalon te gabimi i pare.
# 00 ruan gjendjen para cdo ndryshimi, keshtu qe rollback-optimisation.ps1 e rikthen databazen pikerisht si ishte.
# Gjithe dalja (p.sh. cilat indekse u hoqen ose u lane nga 01) ruhet ne apply-optimisation-<databaza>-<data>.log.
# Perdorimi (PowerShell, nga kjo dosje ose me rrugen e plote):
#   .\apply-optimisation.ps1 -Server 'localhost\HERACLES' -Database 'web_vg'
# Kerkon sqlcmd. Lidhja me Windows authentication (-E).
param(
    [Parameter(Mandatory = $true)] [string] $Server,
    [Parameter(Mandatory = $true)] [string] $Database
)
$ErrorActionPreference = 'Stop'
$scripts = @(
    '00-ruajtje.sql',
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
    '13-renditja-koka-magazine.sql',
    '14-adresa-klienti.sql',
    '15-kodbari-indeks.sql',
    '16-eksport-shitje-autorizim.sql',
    '17-cmim-mesatar-indekse.sql',
    '18-kontroll-gjendje-pa-ddl.sql',
    '19-atribute-unike.sql',
    '20-periudha-indekse.sql',
    '21-ruajtje-magazine-pa-rikompilim.sql',
    '22-artikuj-import-grup.sql',
    '23-lupa-artikull-pa-gjendje-kur-bosh.sql',
    '24-ruajtje-magazine-progresivi.sql',
    '25-cmim-mesatar-shuma-direkte.sql',
    '26-cmim-mesatar-konfigurime.sql',
    '27-konfigurim-grida-indekse.sql'
)
$log = Join-Path $PSScriptRoot ("apply-optimisation-{0}-{1:yyyyMMdd-HHmmss}.log" -f ($Database -replace '[^\w]', '_'), (Get-Date))
foreach ($s in $scripts) {
    $path = Join-Path $PSScriptRoot $s
    $t = Get-Date
    $out = & sqlcmd -S $Server -E -C -d $Database -I -b -t 0 -i $path 2>&1
    Add-Content -Path $log -Value (@("===== $s") + $out)
    if ($LASTEXITCODE -ne 0) {
        $out | Select-Object -Last 8 | ForEach-Object { Write-Host $_ -ForegroundColor Red }
        Write-Host "$s DESHTOI (kodi $LASTEXITCODE). Ndalova; rregullojeni dhe riniseni (skriptet mund te riekzekutohen). Log: $log" -ForegroundColor Red
        exit 1
    }
    Write-Host ("{0} ok ({1:N0} s)" -f $s, ((Get-Date) - $t).TotalSeconds)
}
Write-Host "Te gjitha skriptet u aplikuan ne $Database. Log: $log" -ForegroundColor Green
