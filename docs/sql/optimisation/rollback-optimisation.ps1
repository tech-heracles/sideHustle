# Rikthe skriptet e optimizimit ne rradhe te kundert (26 ... 04, 01, pastaj 00); ndalon te gabimi i pare.
# Kur skriptet u aplikuan me apply-optimisation.ps1 (me 00-ruajtje.sql), cdo procedure dhe indeks rikthehet pikerisht
# si ishte ne kete databaze para aplikimit. 02 (rritja e skedareve) nuk rikthehet.
# Perdorimi (PowerShell, nga kjo dosje ose me rrugen e plote):
#   .\rollback-optimisation.ps1 -Server 'localhost\HERACLES' -Database 'web_vg'
# Kerkon sqlcmd. Lidhja me Windows authentication (-E). Dalja ruhet ne rollback-optimisation-<databaza>-<data>.log.
param(
    [Parameter(Mandatory = $true)] [string] $Server,
    [Parameter(Mandatory = $true)] [string] $Database
)
$ErrorActionPreference = 'Stop'
$scripts = @(Get-ChildItem -Path $PSScriptRoot -Filter '*-rollback-*.sql' |
    Where-Object { $_.Name -match '^\d\d-' -and $_.Name -notlike '00-*' } | Sort-Object Name -Descending | ForEach-Object { $_.Name }) + '00-rollback-ruajtje.sql'
$log = Join-Path $PSScriptRoot ("rollback-optimisation-{0}-{1:yyyyMMdd-HHmmss}.log" -f ($Database -replace '[^\w]', '_'), (Get-Date))
foreach ($s in $scripts) {
    $path = Join-Path $PSScriptRoot $s
    $t = Get-Date
    $out = & sqlcmd -S $Server -E -C -d $Database -I -b -t 0 -i $path 2>&1
    Add-Content -Path $log -Value (@("===== $s") + $out)
    if ($LASTEXITCODE -ne 0) {
        $out | Select-Object -Last 8 | ForEach-Object { Write-Host $_ -ForegroundColor Red }
        Write-Host "$s DESHTOI (kodi $LASTEXITCODE). Ndalova. Log: $log" -ForegroundColor Red
        exit 1
    }
    Write-Host ("{0} ok ({1:N0} s)" -f $s, ((Get-Date) - $t).TotalSeconds)
}
Write-Host "Te gjitha skriptet u rikthyen ne $Database. Log: $log" -ForegroundColor Green
