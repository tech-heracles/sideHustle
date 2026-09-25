# Build first:  MSBuild.exe PlatinumWeb.sln -p:Configuration=Release -m
# Deploys the local build to C:\Sites\AlphaWeb:
#  - DLL/PDB files in bin that differ from the live site
#  - pages and scripts (.aspx/.ascx/.master/.asmx/.ashx/.js/.css) that differ and already exist on the site
#  - machineKey.config first, then the Web.config that points to it
#  - pages removed from the project
# Restarts the app (everyone is logged out). First page load afterwards takes a few minutes.
$repo = "C:\Users\CTS\Documents\PosiLagur\alpha\PlatinumWeb"
$site = "C:\Sites\AlphaWeb"

function Same($a, $b) { (Test-Path $b) -and ((Get-FileHash $a).Hash -eq (Get-FileHash $b).Hash) }

$n = 0
Get-ChildItem "$repo\bin" -File | Where-Object { $_.Extension -in '.dll', '.pdb' } | ForEach-Object {
    $target = Join-Path "$site\bin" $_.Name
    if ((Test-Path $target) -and -not (Same $_.FullName $target)) {
        Copy-Item $_.FullName $target -Force
        $n++
    }
}
"Copied $n files to $site\bin"

$n = 0
$ext = '.aspx', '.ascx', '.master', '.asmx', '.ashx', '.js', '.css'
Get-ChildItem $repo -Recurse -File | Where-Object {
    $_.Extension -in $ext -and $_.FullName -notmatch '\\(bin|obj)\\'
} | ForEach-Object {
    $target = $site + $_.FullName.Substring($repo.Length)
    if ((Test-Path $target) -and -not (Same $_.FullName $target)) {
        Copy-Item $_.FullName $target -Force
        "  updated " + $_.FullName.Substring($repo.Length + 1)
        $n++
    }
}
"Updated $n pages/scripts"

if (-not (Same "$repo\machineKey.config" "$site\machineKey.config")) {
    Copy-Item "$repo\machineKey.config" "$site\machineKey.config" -Force
    "Copied machineKey.config"
}
if (-not (Same "$repo\Web.config" "$site\Web.config")) {
    Copy-Item "$site\Web.config" "C:\Sites\AlphaWeb.Web.config.bak" -Force   # outside the web root
    Copy-Item "$repo\Web.config" "$site\Web.config" -Force
    "Copied Web.config (previous one saved as C:\Sites\AlphaWeb.Web.config.bak)"
}

# pages removed from the project
foreach ($f in 'WebService_mobile.asmx') {
    if (Test-Path "$site\$f") { Remove-Item "$site\$f"; "Removed $f" }
}
