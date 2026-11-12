# Build check on GitHub

`.github/workflows/build.yml` compiles `PlatinumWeb.sln` (Release) on GitHub's Windows machines for every push,
so a compile error shows as a red ✗ on the commit instead of on the server. A second step compiles every
`.aspx`/`.ascx` page the way IIS does on first request (`aspnet_compiler`), which also catches markup errors.

## One-time setup

1. **Make the repository private** (GitHub → Settings → General → Danger Zone → Change visibility).
   It is public now. The DevExpress assemblies below must never be in a public repository, and the secrets listed
   in `go-live.md` step 1 are still readable in the public history.
2. **Upload DevExpress 18.2.7** from a PC where AVEC already runs (DevExpress in the GAC), in PowerShell
   from the repository folder:
   ```powershell
   .\tools\pack-devexpress-for-ci.ps1 -Upload
   ```
   Without the GitHub CLI (`gh`): run it without `-Upload`, then on GitHub → Releases → *Draft a new release* →
   tag `build-deps`, tick *Set as a pre-release*, attach `devexpress-18.2.7.zip`, publish.
3. GitHub → **Actions** → *Build* → *Run workflow*. The first run takes longest.

## Reading the result

- ✓ green: the solution compiles.
- ✗ red at *DevExpress 18.2.7 assemblies*: step 2 was not done (or the repository is public and the asset is missing).
- ✗ red at *Build (Release)*: open the step, the errors are listed at the end with file and line.
- *Compile pages* is allowed to fail for now (`continue-on-error`) until it has been green once; after that, remove
  that line in `build.yml` so page errors also turn the build red.

## Cost

Private repositories on the free plan get 2,000 Actions minutes a month; Windows minutes count double.
Pushes that only touch `docs/`, `tools/` or `.md` files do not start a build, and a newer push cancels the
running one on the same branch.
