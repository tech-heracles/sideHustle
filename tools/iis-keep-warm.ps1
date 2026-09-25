# Keeps the AVEC Accounting app warm so the first request after a quiet period is not a cold start.
# Run in an elevated PowerShell (Run as administrator). Safe to run more than once.
#   - idle timeout 0      : IIS no longer shuts the app pool down after 20 idle minutes
#   - AlwaysRunning       : the worker process starts with IIS instead of on the first request
#   - daily recycle 03:00 : replaces the default "every 29 hours" recycle, which can land in working hours
# Undo: set idleTimeout back to 00:20:00, startMode OnDemand, periodicRestart.time 1.05:00:00, clear schedule.
Import-Module WebAdministration
$pool = "AlphaWebPool"

Set-ItemProperty "IIS:\AppPools\$pool" -Name processModel.idleTimeout -Value ([TimeSpan]::Zero)
Set-ItemProperty "IIS:\AppPools\$pool" -Name startMode -Value "AlwaysRunning"
Set-ItemProperty "IIS:\AppPools\$pool" -Name recycling.periodicRestart.time -Value ([TimeSpan]::Zero)
Clear-ItemProperty "IIS:\AppPools\$pool" -Name recycling.periodicRestart.schedule
New-ItemProperty "IIS:\AppPools\$pool" -Name recycling.periodicRestart.schedule -Value @{value = "03:00:00"}

Get-ItemProperty "IIS:\AppPools\$pool" | Select-Object name, startMode,
    @{n = 'idleTimeout'; e = { $_.processModel.idleTimeout }},
    @{n = 'recycleEvery'; e = { $_.recycling.periodicRestart.time }}
