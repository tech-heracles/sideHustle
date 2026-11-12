# Packs the DevExpress 18.2.7 assemblies from this PC (GAC and/or the DevExpress 18.2 install folder) into
# devexpress-18.2.7.zip for the GitHub build check (.github/workflows/build.yml), and optionally uploads it
# as the release asset "build-deps". See docs/build-check.md.
#
# Run on a PC where the site already runs (DevExpress 18.2.7 in the GAC), in PowerShell from the repository:
#   .\tools\pack-devexpress-for-ci.ps1            # only makes the zip
#   .\tools\pack-devexpress-for-ci.ps1 -Upload    # makes it and uploads it (needs GitHub CLI "gh", logged in)
#
# -Upload refuses to run while the repository is public: the DevExpress licence does not allow publishing them.
param(
    [string] $OutFile = (Join-Path (Get-Location) 'devexpress-18.2.7.zip'),
    [switch] $Upload,
    [string] $Repo = 'tech-heracles/sideHustle'
)
$ErrorActionPreference = 'Stop'
$version = '18.2.7.0'
$token = 'b88d1754d700e49a'
$root = Split-Path $PSScriptRoot -Parent

# 1. what the solution references
$needed = Get-ChildItem $root -Recurse -Filter *.csproj |
    Select-String -Pattern '<Reference Include="(DevExpress\.[^,"]+\.v18\.2[^,"]*)' -AllMatches |
    ForEach-Object { $_.Matches } | ForEach-Object { $_.Groups[1].Value } | Sort-Object -Unique
"The solution references {0} DevExpress assemblies." -f $needed.Count

# 2. collect every DevExpress 18.2.7 assembly found on this PC (also the ones referenced only indirectly)
$staging = Join-Path ([IO.Path]::GetTempPath()) ("dx-ci-" + [guid]::NewGuid())
New-Item -ItemType Directory -Path $staging | Out-Null
$sources = @(
    "$env:WINDIR\Microsoft.NET\assembly\GAC_MSIL",
    "$env:WINDIR\Microsoft.NET\assembly\GAC_32",
    "$env:WINDIR\Microsoft.NET\assembly\GAC_64"
) | Where-Object { Test-Path $_ }
foreach ($gac in $sources) {
    Get-ChildItem $gac -Directory -Filter 'DevExpress.*v18.2*' | ForEach-Object {
        $dir = Join-Path $_.FullName "v4.0_${version}__$token"
        if (Test-Path $dir) {
            Get-ChildItem $dir -Filter *.dll | ForEach-Object { Copy-Item $_.FullName $staging -Force }
        }
    }
}
$installBin = "${env:ProgramFiles(x86)}\DevExpress 18.2\Components\Bin\Framework"
if (Test-Path $installBin) {
    Get-ChildItem $installBin -Filter 'DevExpress.*v18.2*.dll' | ForEach-Object {
        if ($_.VersionInfo.FileVersion -like '18.2.7*' -and -not (Test-Path (Join-Path $staging $_.Name))) {
            Copy-Item $_.FullName $staging
        }
    }
}

$found = Get-ChildItem $staging -Filter *.dll
"Found {0} DevExpress 18.2.7 assemblies on this PC." -f $found.Count
$missing = $needed | Where-Object { -not (Test-Path (Join-Path $staging "$_.dll")) }
if ($missing) {
    Remove-Item $staging -Recurse -Force
    throw ("Missing on this PC (the build would fail without them):`n  " + ($missing -join "`n  "))
}

# 3. zip
if (Test-Path $OutFile) { Remove-Item $OutFile }
Compress-Archive -Path (Join-Path $staging '*.dll') -DestinationPath $OutFile
Remove-Item $staging -Recurse -Force
"Wrote {0} ({1:N0} MB)" -f $OutFile, ((Get-Item $OutFile).Length / 1MB)

# 4. upload (private repository only)
if ($Upload) {
    if (-not (Get-Command gh -ErrorAction SilentlyContinue)) { throw "GitHub CLI 'gh' is not installed: upload the zip by hand (docs/build-check.md)." }
    $visibility = gh repo view $Repo --json visibility -q .visibility
    if ($visibility -ne 'PRIVATE') { throw "Repository $Repo is $visibility. Make it private first; DevExpress assemblies must not be published." }
    gh release view build-deps --repo $Repo *> $null
    if ($LASTEXITCODE -ne 0) {
        gh release create build-deps --repo $Repo --prerelease --title 'Build dependencies (private)' `
            --notes 'DevExpress 18.2.7 assemblies for the GitHub build check. Do not publish.'
    }
    gh release upload build-deps $OutFile --repo $Repo --clobber
    if ($LASTEXITCODE -ne 0) { throw "upload failed" }
    "Uploaded. Re-run the 'Build' workflow on GitHub (Actions tab)."
}
