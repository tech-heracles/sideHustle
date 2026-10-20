# AVEC Accounting: go-live at the client

Follow the steps in order. Each step says how to check it worked. Steps marked **STOP** must pass before you
continue; if one fails, stop there and roll back (section 8) rather than pushing on.

Everything referred to here is in this repository:

| What | Where |
|---|---|
| Database scripts 01, 02, 04–26 (each has a `NN-rollback-...sql` next to it) | `docs/sql/optimisation/` |
| Runs all of them in order, stops at the first error, safe to run again | `docs/sql/optimisation/apply-optimisation.ps1` |
| Index usage report (read only) | `docs/sql/diagnostics/index-usage-trupimagazina.sql` |
| IIS app-pool settings | `tools/iis-keep-warm.ps1` |
| AVEC installation key rotation | `tools/rotate-avec-key.ps1` |

---

## 1. Before you go (at the office)

1. **Rotate the leaked secrets.** The repository was public with secrets in its history.
   - Revoke the two Google Cloud service-account keys that were committed, create new ones, and scrub them from the
     git history (e.g. `git filter-repo`), then force-push and re-clone everywhere.
   - Rotate the INTEGRIM-F5 service-account key.
   - Rotate the AVEC installation key: `tools\rotate-avec-key.ps1 -ServiceAccount <new key json>`.
   - Check: the old keys show as deleted in the Google Cloud console; login on the local site still works.
2. **Build the release** from a clean checkout of `main`:
   `MSBuild.exe PlatinumWeb.sln -p:Configuration=Release -m`. It must finish with 0 errors.
3. **Prepare the site package**: copy `C:\Sites\AlphaWeb` (the local site, already on this build) to a USB drive or
   share, **without** `log\`, `App_Data\`, `connectionStrings.config`, `avecLicense.config` and `machineKey.config`.
   Those are server-specific and are created or kept at the client (step 5).
4. **Licence.** In the Firebase console, check the client's company: `avecAccounting.active = true` and
   `endDate` well in the future. (Locally "Tona F" had expired and blocked every login.) Note the installation id
   and key you will put in the client's `avecLicense.config`.
5. Take this document, the rollback scripts and the previous build of their site with you.

## 2. Check the client server

1. **SQL Server edition and version** (run in SSMS):
   ```sql
   SELECT SERVERPROPERTY('Edition'), SERVERPROPERTY('ProductVersion'), SERVERPROPERTY('ProductLevel');
   EXEC sp_spaceused;   -- database size
   ```
   - **Express edition:** the database cannot grow past **10 GB** (data file) and SQL uses at most ~1.4 GB of memory.
     If the database is near 10 GB, plan the move to Standard edition before anything else.
   - Install the latest **cumulative update** for their SQL Server version (an unpatched SQL Server 2025 crashed
     internally on our test machine).
2. **Disk space**: at least the size of the database free for the backup, plus ~2 GB for index rebuilds.
3. **Windows / IIS**: .NET Framework 4.7.1+, DevExpress 18.2.7 in the GAC (as on the current server).

## 3. Rehearsal on a copy (do not skip)

Do this on a test server or a second database on their server, **not** on the live database.

1. Restore last night's backup of their database as e.g. `<db>_test`.
2. Run the scripts: `.\apply-optimisation.ps1 -Server '<server\instance>' -Database '<db>_test'`.
   - Check: every script prints `ok`, then `Te gjitha skriptet u aplikuan`. Note how long the first run takes: it
     drops and builds indexes, so on a large database it can take several minutes and locks those tables while it
     runs. That is the time to plan for in step 4 (a second run only takes seconds).
3. Point a copy of the site at `<db>_test` and check **with their real files**:
   - Sales, purchases, warehouse exits/transfers (FDT), production entries (FHTT), items: run **Kontrollo**, then **Importo**.
   - Their **"dev post"** file (columns `barcode, produkti, kosto, sasia, dataSkadences`) and their own format for
     `MagazineFDT.xlsx`: these formats exist only at the client and were not tested by us.
   - Two people importing at the same time (two PCs or two browsers): no document may fail with
     *"Dokumenti nuk mund te importohet ne kete moment"*.
4. **STOP** if anything differs from what users expect. Compare at least: stock of a few busy items, the trial
   balance for the current month, and one imported document of each type against the same document saved by hand.

## 4. Go-live window (outside working hours)

1. Tell users to log out; everyone is logged out by the deploy anyway.
2. **Full backup** of the live database (`BACKUP DATABASE ... WITH COPY_ONLY, CHECKSUM`) and a copy of the current
   site folder. **STOP** until both exist and the backup verifies (`RESTORE VERIFYONLY`).
3. Run `apply-optimisation.ps1` on the live database. **STOP** if any script fails.

## 5. Deploy the site

1. Stop the app pool.
2. Copy the package over the site folder. Keep the server's own `connectionStrings.config` (connection string
   must keep `Max pool size=200`), `machineKey.config` and `avecLicense.config`. If the site has no
   `avecLicense.config` yet, create it from `avecLicense.example.config` with the installation id/key from step 1.4.
3. **Delete files removed from the product** (the copy does not delete):
   - `bin\AlphaWeb.Services.dll`, `bin\AlphaWeb.Services.pdb`, `bin\AlphaWeb.Services.dll.config`
   - `Test\TestDI.aspx` (and the empty `Test` folder)
   - `Scripts\dx.all.debug.js`, `Scripts\dx.viz-web.debug.js`, `Scripts\dx.viz.debug.js`, `Scripts\dx.web.debug.js`,
     `Scripts\vectormap-utils\dx.vectormaputils.debug.js`
   - `js\OpenLayers-2.13.1\OpenLayers.debug.js`, `OpenLayers.light.debug.js`, `OpenLayers.mobile.debug.js`,
     `js\srcCRM\js\jquery.mmenu.debugger.js`
4. App pool: 64-bit (32-bit applications **off**). Run `tools\iis-keep-warm.ps1` as administrator (pool name in
   the script is `AlphaWebPool`; change it if theirs differs): idle timeout 0, AlwaysRunning, recycle at 03:00.
5. Start the app pool and open the login page once. The first load compiles the site and takes 1–3 minutes.

## 6. Checks after the deploy

1. Login works, the company list shows their company. **STOP** if not: check `avecLicense.config` and the licence
   dates in Firebase.
2. `http://<site>/api/Test/Index` returns **401** (the old test endpoint is gone).
3. Open the sales list, a purchase, a warehouse document; save one manual sales document and one warehouse document.
4. Import one small real file of each type the client uses.
5. **Fiscalization** (not testable on our side): issue one e-invoice and, if they use them, one transport document
   (`Tipi = WTN`); check they reach the tax authority.
6. **Passwords**: change a user's password, log out and back in with the new one; if password history is on, reusing
   an old password must be refused; an existing user with an old password must still log in.
7. **Employees / payroll**: open an employee and the payroll list. Their data is encrypted with keys protected by the
   database master key. **Never run `docs/sql/recreate-employee-keys.sql` on the client database**: it would make
   their real employee data unreadable. If the database was moved to a new server and payroll fails at
   `OPEN SYMMETRIC KEY`, open the master key with its password and run
   `ALTER MASTER KEY ADD ENCRYPTION BY SERVICE MASTER KEY` instead.

## 7. First week

- Check `log\<date>.Error.txt` daily, and `log\app-lifetime.txt` for unexpected restarts.
- Deadlocks (should be none):
  ```sql
  SELECT COUNT(*) FROM sys.fn_xe_file_target_read_file('system_health*.xel', NULL, NULL, NULL)
  WHERE object_name = 'xml_deadlock_report';
  ```
- Ask users which screens are slow; note screen, filter and time.
- After at least 3–4 working days **without a SQL Server restart**, run
  `docs/sql/diagnostics/index-usage-trupimagazina.sql` and send the result back: it decides which unused indexes
  can be dropped (the next speed-up for every document save).

## 8. Rollback

- **Database scripts only**: run the matching `NN-rollback-...sql` files in reverse order (26 down to 01; 02 only changes file growth
  settings and has no rollback). Each restores
  the previous procedure or index exactly.
- **Everything**: stop the app pool, restore the site folder copy and the backup from step 4.2, start the app pool.
  Documents entered after the go-live would be lost with a full restore, so decide quickly.
