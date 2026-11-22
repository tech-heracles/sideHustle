# AVEC smoke + timing test

Opens every page of the main menu (and every lupa on form pages) the way a menu click does, and records
status, timings, sizes and errors, so a performance change can be proven not to break anything.

## Setup (once)

```powershell
cd tools\smoke
npm install
npx playwright-core install chromium-headless-shell
```

Put the passwords in `smoke.local.json` (git-ignored) or pass them on the command line / env:

```json
{ "password": "<konfig password>", "sql-password": "<AVEC sql password>" }
```

## Run

```powershell
node smoke-test.js                       # cold + warm pass over all pages and lupat, plus Query Store export
node smoke-test.js --runs warm --lupa off
node smoke-test.js --only "Fatura|Magazin" --runs check
```

Options: `--base --company --user --password --runs --lupa on|off --search <term> --only <regex> --skip <regex>
--limit N --headed --sql-server --sql-db --sql-user --sql-password` (also `AVEC_SMOKE_<OPTION>` env vars).

Pages that log out or reload the app (Dalje, RivleresimMagazine, the PBA AB* pages that call logout) are always skipped.
"Cold" is the first pass of a fresh browser and login; it does not recycle the IIS app pool (needs admin).

## Output (`results/`, git-ignored)

- `<ts>-<run>.csv`: one row per page (`type=page`) and per lupa (`type=lupa`): HTTP status, TTFB, DOM, load,
  settled (until the last local request finished), html/transfer/ViewState bytes, lupa grid rows, search time,
  error text, uncaught JS errors, console errors.
- `<ts>-summary.md`: counts per status, 20 slowest pages, failing pages, slowest lupat/searches, failing lupat,
  cold vs warm, Query Store top 20.
- `querystore-<ts>.csv`: top 20 queries by total and by average duration since the test started
  (needs Query Store ON in the database).

Status: `OK`, `WARN` (console errors only), `JSERR` (uncaught JS exception), `ERROR` (HTTP >= 400, error text on
the page, logout, timeout), `NOOPEN` (button opened no lupa), `SKIPPED`. Exit code 1 if anything is `ERROR`.
