#!/usr/bin/env node
// AVEC Accounting smoke + timing test.
//
// Logs in, collects every page reachable from the main menu (top menu + navbar of FaqeKryesore.aspx),
// opens each page in the main pane exactly like a menu click does, and records HTTP status, timings,
// sizes, ViewState size and any error. On form pages it also opens every lupa (magnifier button next
// to a field), waits for its grid, and times a search in it. Runs the whole crawl N times (cold, warm)
// and writes results/<timestamp>-<run>.csv, results/<timestamp>-summary.md and, if SQL access is
// configured, results/querystore-<timestamp>.csv (top 20 queries by total and by average duration).
//
// Usage (from tools/smoke):  npm install   (once)   then   node smoke-test.js [options]
//   --base http://localhost:8080/   --company DRAFT   --user konfig   --password <pw>
//   --runs cold,warm        names of the passes (default cold,warm)
//   --lupa on|off           test lupat on form pages (default on)
//   --search a              term typed into each lupa's search box ('' = no search)
//   --only <regex>          only pages whose url matches
//   --skip <regex>          extra pages to skip (the dangerous ones are always skipped)
//   --limit N               first N pages only (for a quick check)
//   --headed                show the browser
//   --sql-server localhost\HERACLES --sql-db DRAFT --sql-user AVEC --sql-password <pw>   (Query Store export)
// Every option can also come from smoke.local.json (git-ignored) or env AVEC_SMOKE_<OPTION> (e.g.
// AVEC_SMOKE_PASSWORD, AVEC_SMOKE_SQL_PASSWORD).
'use strict';
const fs = require('fs');
const path = require('path');
const { chromium } = require('playwright-core');

// ---------- options ----------
const DEFAULTS = {
  base: 'http://localhost:8080/', company: 'DRAFT', user: 'konfig', password: '',
  runs: 'cold,warm', lupa: 'on', search: 'a', only: '', skip: '', limit: 0, headed: false,
  'sql-server': 'localhost\\HERACLES', 'sql-db': 'DRAFT', 'sql-user': 'AVEC', 'sql-password': '',
  'page-timeout': 60000, 'lupa-timeout': 30000,
};
function readOptions() {
  const o = Object.assign({}, DEFAULTS);
  const local = path.join(__dirname, 'smoke.local.json');
  if (fs.existsSync(local)) Object.assign(o, JSON.parse(fs.readFileSync(local, 'utf8')));
  for (const k of Object.keys(DEFAULTS)) {
    const env = process.env['AVEC_SMOKE_' + k.toUpperCase().replace(/-/g, '_')];
    if (env !== undefined) o[k] = env;
  }
  const a = process.argv.slice(2);
  for (let i = 0; i < a.length; i++) {
    if (!a[i].startsWith('--')) continue;
    const k = a[i].slice(2);
    if (k === 'headed') { o.headed = true; continue; }
    o[k] = a[++i];
  }
  o.limit = Number(o.limit) || 0;
  if (!o.base.endsWith('/')) o.base += '/';
  if (!o.password) { console.error('Password missing: pass --password, set AVEC_SMOKE_PASSWORD or put it in smoke.local.json'); process.exit(2); }
  return o;
}
const opt = readOptions();

// pages that must never be opened by the crawler (they log out, reload the app or are actions)
const ALWAYS_SKIP = /^(dalje|mesazhe|transferimDaljePopup)$|RivleresimMagazine\.aspx|ABKonfiguroFusha|ABPlanifikimRealizim|ABProkurimePublikeParashikim|ABRaporti\.aspx/i;
const ERROR_TEXT = /Global Page Error|Server Error in '|Runtime Error|Exception of type|Object reference not set|Stack Trace:|virtualPath|Invalid (column|object) name|Could not find stored procedure|Timeout expired|Execution Timeout|Procedure or function .* expects|Conversion failed|Must declare the scalar|The page cannot be displayed|HTTP Error \d{3}/i;
const HOST = new URL(opt.base).host;

// ---------- helpers ----------
const now = () => Date.now();
const sleep = ms => new Promise(r => setTimeout(r, ms));
const csvCell = v => { if (v === undefined || v === null) return ''; const s = String(v).replace(/\r?\n/g, ' ').replace(/\s+/g, ' ').trim(); return /[",;]/.test(s) ? '"' + s.replace(/"/g, '""') + '"' : s; };
const stamp = () => new Date().toISOString().replace(/[:T]/g, '-').replace(/\..+/, '');

class Net {   // in-flight request tracker for "settled" timing
  constructor(page) {
    this.inflight = new Set(); this.last = now();
    const local = r => { try { const u = new URL(r.url()); return u.host === HOST && !/\/signalr\//i.test(u.pathname); } catch { return false; } };
    page.on('request', r => { if (local(r)) { this.inflight.add(r); this.last = now(); } });
    const done = r => { if (this.inflight.delete(r)) this.last = now(); };
    page.on('requestfinished', done); page.on('requestfailed', done);
  }
  async idle(quiet = 500, max = 30000) {
    const start = now();
    while (now() - start < max) {
      if (this.inflight.size === 0 && now() - this.last >= quiet) return true;
      await sleep(50);
    }
    return false;
  }
  // ms from t0 to the end of the last local request (the quiet window is not counted)
  since(t0, floor) { return Math.max(floor || 0, this.last - t0);
  }
}

async function login(page) {
  await page.goto(opt.base, { waitUntil: 'networkidle' });
  await page.waitForFunction(() => window.cmbServerat && window.loginButton && window.txtUserName, null, { timeout: 30000 });
  await page.evaluate(c => { const it = cmbServerat.FindItemByText(c); if (!it) throw new Error('company not found: ' + c); cmbServerat.SetSelectedItem(it); }, opt.company);
  await page.fill('#Login1_UserName_I', opt.user);
  await page.fill('#Login1_Password_I', opt.password);
  await page.evaluate(() => loginButton.DoClick());
  await page.waitForURL(u => /FaqeKryesore/i.test(u.toString()), { timeout: 60000 });
  await page.waitForLoadState('load');
  await page.waitForFunction(() => window.splitter && window.menu, null, { timeout: 30000 });
}

async function collectMenu(page) {
  return page.evaluate(() => {
    const out = [], seen = {};
    function walk(node, texts) {
      for (let n = 0; n < node.GetItemCount(); n++) {
        const it = node.GetItem(n), t = texts.concat(it.GetText());
        if (it.GetItemCount() > 0) { walk(it, t); continue; }
        if (!it.GetVisible() || !it.name || !it.GetText() || seen[it.name]) continue;
        seen[it.name] = 1; out.push({ url: it.name, title: t.filter(Boolean).join(' > ') });
      }
    }
    if (window.menu) walk(menu.GetRootItem(), []);
    if (window.navbar) for (let i = 0; i < navbar.GetGroupCount(); i++) {
      const g = navbar.GetGroup(i); if (!g.GetVisible()) continue;
      for (let j = 0; j < g.GetItemCount(); j++) {
        const it = g.GetItem(j);
        if (!it.GetVisible() || !it.name || seen[it.name]) continue;
        seen[it.name] = 1; out.push({ url: it.name, title: g.GetText() + ' > ' + it.GetText() });
      }
    }
    return out;
  });
}

// facts about a loaded frame: navigation timing, sizes, error text
async function frameFacts(frame) {
  return frame.evaluate((reSrc) => {
    const re = new RegExp(reSrc, 'i');
    const n = performance.getEntriesByType('navigation')[0] || {};
    const vs = document.getElementById('__VIEWSTATE');
    const body = document.body ? document.body.innerText || '' : '';
    let err = '';
    const m = body.match(re);
    if (m) { const i = Math.max(0, m.index - 60); err = body.slice(i, i + 300); }
    const noty = Array.from(document.querySelectorAll('.noty_type_error, .noty_type_alert .noty_text, .noty_bar.noty_type_error'))
      .map(e => e.innerText).filter(Boolean).join(' | ');
    return {
      ttfb: Math.round(n.responseStart - n.requestStart) || 0,
      responseEnd: Math.round(n.responseEnd - n.startTime) || 0,
      dom: Math.round(n.domContentLoadedEventEnd - n.startTime) || 0,
      load: Math.round(n.loadEventEnd - n.startTime) || 0,
      transfer: n.transferSize || 0, html: n.decodedBodySize || 0,
      viewstate: vs ? vs.value.length : 0, err, noty,
      title: document.title || '',
    };
  }, ERROR_TEXT.source).catch(e => ({ err: 'cannot read frame: ' + e.message }));
}

function withScope(page, url) {
  const scope = new URL(page.url()).searchParams.get('scopeID');
  return url + (url.includes('?') ? '&' : '?') + 'scopeID=' + scope;
}

// ---------- one pass over all pages ----------
async function runPass(browser, runName, pagesFilter) {
  const context = await browser.newContext({ viewport: { width: 1600, height: 1000 } });
  const page = await context.newPage();
  const net = new Net(page);
  let sink = null;   // the record that console errors / dialogs are attributed to
  const note = (k, msg) => { if (sink) sink[k].push(msg.replace(/\s+/g, ' ').slice(0, 300)); };
  page.on('console', m => { if (m.type() === 'error' && !/fonts\.googleapis|favicon/i.test(m.text())) note('console', m.text()); });
  page.on('pageerror', e => note('jserr', e.message));
  page.on('dialog', d => { note('dialogs', d.message()); d.dismiss().catch(() => {}); });
  const docStatus = new Map();   // frame -> [statuses]
  page.on('response', r => {
    const q = r.request();
    if (q.resourceType() !== 'document') return;
    const f = r.frame(); if (!docStatus.has(f)) docStatus.set(f, []);
    docStatus.get(f).push(r.status());
  });

  await login(page);
  let menuItems = await collectMenu(page);
  const all = menuItems.length;
  menuItems = pagesFilter(menuItems);
  console.log(`[${runName}] ${all} menu items, ${menuItems.length} to test`);
  const mainFrame = () => page.frames().find(f => f.name() === 'frameKryesor');
  const rows = [];

  async function ensureLoggedIn() {
    if (/FaqeKryesore/i.test(page.url()) && mainFrame()) return false;
    await login(page); return true;
  }

  for (let i = 0; i < menuItems.length; i++) {
    const item = menuItems[i];
    const rec = { run: runName, type: 'page', seq: i + 1, page_url: item.url, title: item.title, console: [], jserr: [], dialogs: [] };
    rows.push(rec);
    if (ALWAYS_SKIP.test(item.url) || (opt.skip && new RegExp(opt.skip, 'i').test(item.url)) || !/\./.test(item.url)) {
      rec.status = 'SKIPPED'; rec.error = 'skipped (logs out / reloads app / not a page)'; continue;
    }
    try {
      if (await ensureLoggedIn()) rec.dialogs.push('re-logged in before this page');
      const frame = mainFrame();
      docStatus.set(frame, []);
      await net.idle(300, 10000);
      sink = rec;
      const target = withScope(page, item.url);
      const t0 = now();
      const nav = page.waitForEvent('framenavigated', { predicate: f => f === frame && f.url() !== 'about:blank', timeout: opt['page-timeout'] });
      await page.evaluate(u => { const p = splitter.GetPaneByName('paneKryesor'); p.SetContentUrl(u); p.RefreshContentUrl(); }, target);
      await nav;
      await frame.waitForLoadState('load', { timeout: opt['page-timeout'] });
      const loaded = now();
      const settledOk = await net.idle(500, 30000);
      rec.wall_load_ms = loaded - t0;
      rec.settled_ms = net.since(t0, rec.wall_load_ms);
      if (!/FaqeKryesore/i.test(page.url())) { rec.status = 'ERROR'; rec.error = 'app left FaqeKryesore (logout/reload): ' + page.url(); sink = null; continue; }
      if (frame.isDetached()) { rec.status = 'ERROR'; rec.error = 'page reloaded the whole app (top window navigated to ' + page.url() + ')'; sink = null; continue; }
      const f = await frameFacts(frame);
      Object.assign(rec, { final_url: frame.url(), ttfb_ms: f.ttfb, response_ms: f.responseEnd, dom_ms: f.dom, load_ms: f.load,
        transfer_bytes: f.transfer, html_bytes: f.html, viewstate_bytes: f.viewstate });
      const st = docStatus.get(frame) || []; rec.http_status = st.length ? st[st.length - 1] : '';
      if (st.some(s => s >= 300 && s < 400)) rec.redirects = st.join('>');
      const errs = [];
      if (rec.http_status >= 400) errs.push('HTTP ' + rec.http_status);
      if (f.err) errs.push(f.err);
      if (f.noty) errs.push('message: ' + f.noty);
      if (/Prezantohu|Error|Gabim/i.test(new URL(frame.url()).pathname) && !/Error/i.test(item.url)) errs.push('redirected to ' + new URL(frame.url()).pathname);
      if (!settledOk) rec.dialogs.push('network not idle after 30s');
      rec.error = errs.join(' | ');
      // lupat
      sink = null;
      if (opt.lupa === 'on' && !errs.length) await testLupat(page, net, frame, rec, rows, note, (s) => { sink = s; }, docStatus);
    } catch (e) {
      rec.status = 'ERROR'; rec.error = (rec.error ? rec.error + ' | ' : '') + 'crawler: ' + e.message.split('\n')[0];
    } finally {
      sink = null;
      finish(rec);
      const msg = `[${runName}] ${String(i + 1).padStart(3)}/${menuItems.length} ${rec.status.padEnd(7)} ${String(rec.load_ms ?? '').padStart(6)}ms ${item.url}${rec.error ? '  ' + rec.error.slice(0, 120) : ''}`;
      console.log(msg);
    }
  }
  // log out so the session/licence slot is freed
  try { await page.goto(opt.base + 'Prezantohu.aspx?arsye=logout&google=true', { timeout: 10000 }).catch(() => {}); } catch { }
  await context.close();
  return rows;
}

function finish(rec) {
  if (rec.status === 'SKIPPED') return;
  rec.console_errors = rec.console.join(' || ');
  rec.js_errors = rec.jserr.join(' || ');
  rec.notes = rec.dialogs.join(' || ');
  if (!rec.status) {
    if (rec.error) rec.status = 'ERROR';
    else if (rec.jserr.length) rec.status = 'JSERR';
    else if (rec.console.length) rec.status = 'WARN';
    else rec.status = rec.type === 'lupa' && rec.lupa_url === '' ? 'NOOPEN' : 'OK';
  }
}

// ---------- lupat on a page ----------
async function testLupat(page, net, frame, pageRec, rows, note, setSink, docStatus) {
  const count = await frame.evaluate(() => {
    const vis = e => !!(e.offsetParent) && !/dxeDisabled/.test(e.className);
    const c = [];
    document.querySelectorAll('td.dxeButtonEditButton[onclick*="BEClick"], td.dxeButton[onclick*="BEClick"]').forEach(e => { if (vis(e) && !/_B-\d+$/.test(e.id)) c.push(e); });
    document.querySelectorAll('.imb-jqgrid-buttonEdit .ui-icon-search').forEach(s => { const b = s.closest('button,a,.ui-button') || s; if (vis(b)) c.push(b); });
    const uniq = Array.from(new Set(c));
    uniq.forEach((e, i) => {
      e.setAttribute('data-smoke-lupa', String(i));
      const field = e.id ? e.id.replace(/_B\d+$/, '') : (e.closest('[id]') || {}).id || '';
      e.setAttribute('data-smoke-name', field.replace(/^ASPxSplitter1_/, ''));
    });
    return uniq.length;
  });
  if (!count) return;
  const seenKeys = new Set();
  for (let k = 0; k < count; k++) {
    const name = await frame.getAttribute(`[data-smoke-lupa="${k}"]`, 'data-smoke-name').catch(() => '') || ('#' + k);
    const rec = { run: pageRec.run, type: 'lupa', seq: pageRec.seq, page_url: pageRec.page_url, title: pageRec.title, lupa_button: name, lupa_url: '', console: [], jserr: [], dialogs: [] };
    try {
      if (!/FaqeKryesore/i.test(page.url()) || frame.isDetached()) { rec.status = 'ERROR'; rec.error = 'app left the page before this lupa'; finish(rec); rows.push(rec); break; }
      const frameUrlBefore = frame.url();
      await net.idle(300, 10000);
      setSink(rec);
      const t0 = now();
      const navP = page.waitForEvent('framenavigated', { predicate: f => f !== page.mainFrame() && f.url() !== 'about:blank' && !/DXR\.axd|DX\.ashx/i.test(f.url()) && (f === frame ? f.url() !== frameUrlBefore : true), timeout: 6000 }).catch(() => null);
      // a covered/zero-size button: fall back to a forced click, then to a DOM click
      await frame.click(`[data-smoke-lupa="${k}"]`, { timeout: 3000 })
        .catch(() => frame.click(`[data-smoke-lupa="${k}"]`, { timeout: 3000, force: true }))
        .catch(() => frame.evaluate(i => { const e = document.querySelector(`[data-smoke-lupa="${i}"]`); if (!e) throw new Error('lupa button gone'); e.click(); }, k));
      const lf = await navP;
      if (!lf) {
        rec.lupa_url = ''; rec.status = 'NOOPEN'; rec.error = '';
        rec.notes = 'no popup opened' + (rec.dialogs.length ? ': ' + rec.dialogs.join(' | ') : '');
        setSink(null); finish(rec); rec.notes = rec.notes || 'no popup opened'; rows.push(rec); await closePopups(frame, page); continue;
      }
      if (lf === frame) { rec.status = 'ERROR'; rec.error = 'button navigated the page to ' + lf.url(); setSink(null); finish(rec); rows.push(rec); break; }
      const u = new URL(lf.url());
      const key = u.pathname + '?' + Array.from(u.searchParams).filter(([n]) => !/^(scopeID|id)/i.test(n)).map(([n, v]) => n + '=' + v).sort().join('&');
      rec.lupa_url = u.pathname.replace(/^\//, '') + u.search.replace(/[?&]scopeID=[^&]*/i, '');
      if (seenKeys.has(key)) { setSink(null); await closePopups(frame, page); continue; }   // same lupa as another field on this page
      seenKeys.add(key);
      await lf.waitForLoadState('load', { timeout: opt['lupa-timeout'] });
      const loaded = now();
      await net.idle(500, opt['lupa-timeout']);
      rec.wall_load_ms = loaded - t0;
      rec.settled_ms = net.since(t0, rec.wall_load_ms);
      const f = await frameFacts(lf);
      const st = docStatus.get(lf) || []; rec.http_status = st.length ? st[st.length - 1] : '';
      Object.assign(rec, { final_url: lf.url(), ttfb_ms: f.ttfb, response_ms: f.responseEnd, dom_ms: f.dom, load_ms: f.load,
        transfer_bytes: f.transfer, html_bytes: f.html, viewstate_bytes: f.viewstate });
      rec.rows = await gridRows(lf);
      const errs = [];
      if (rec.http_status >= 400) errs.push('HTTP ' + rec.http_status);
      if (f.err) errs.push(f.err);
      if (f.noty) errs.push('message: ' + f.noty);
      // search
      if (opt.search && !errs.length) {
        const s0 = now();
        const ok = await lf.evaluate(term => {
          let done = false;
          ASPx.GetControlCollection().ForEachControl(c => {
            if (done || !(window.ASPxClientGridView && c instanceof ASPxClientGridView)) return;
            if (!document.getElementById(c.name + '_DXSE_I')) return;
            c.ApplySearchPanelFilter(term); done = true;
          });
          return done;
        }, opt.search).catch(() => false);
        if (ok) {
          await sleep(30);
          await net.idle(400, opt['lupa-timeout']);
          await lf.waitForFunction(() => { let busy = false; ASPx.GetControlCollection().ForEachControl(c => { if (c.InCallback && c.InCallback()) busy = true; }); return !busy; }, null, { timeout: opt['lupa-timeout'] }).catch(() => {});
          rec.search_term = opt.search;
          rec.search_ms = net.since(s0);
          rec.search_rows = await gridRows(lf);
          const f2 = await frameFacts(lf);
          if (f2.err) errs.push('after search: ' + f2.err);
        }
      }
      rec.error = errs.join(' | ');
    } catch (e) {
      rec.status = 'ERROR'; rec.error = 'crawler: ' + e.message.split('\n')[0];
    }
    setSink(null);
    finish(rec);
    rows.push(rec);
    console.log(`        lupa ${rec.status.padEnd(7)} ${String(rec.settled_ms ?? '').padStart(6)}ms search ${String(rec.search_ms ?? '-').padStart(5)}ms ${rec.lupa_button} -> ${rec.lupa_url}${rec.error ? '  ' + rec.error.slice(0, 100) : ''}`);
    await closePopups(frame, page);
  }
}

async function gridRows(f) {
  return f.evaluate(() => {
    let n = null;
    ASPx.GetControlCollection().ForEachControl(c => {
      if (n === null && window.ASPxClientGridView && c instanceof ASPxClientGridView) n = c.GetVisibleRowsOnPage();
    });
    if (n === null && window.jQuery) { const g = jQuery('.ui-jqgrid-btable'); if (g.length) n = g.find('tr.jqgrow').length; }
    return n;
  }).catch(() => null);
}

async function closePopups(frame, page) {
  for (const f of [frame, page.mainFrame()]) {
    if (f.isDetached()) continue;
    await f.evaluate(() => {
      if (!window.ASPx || !window.ASPxClientPopupControl) return;
      ASPx.GetControlCollection().ForEachControl(c => {
        if (c instanceof ASPxClientPopupControl && c.IsVisible && c.IsVisible()) {
          try { const ifr = c.GetContentIFrame && c.GetContentIFrame(); if (ifr) ifr.src = 'about:blank'; } catch (e) { }
          try { c.SetContentUrl && c.SetContentUrl('about:blank'); } catch (e) { }
          c.Hide();
        }
      });
    }).catch(() => {});
  }
  await sleep(100);
}

// ---------- reporting ----------
const COLS = ['run', 'type', 'seq', 'status', 'page_url', 'title', 'lupa_button', 'lupa_url', 'final_url', 'http_status', 'redirects',
  'ttfb_ms', 'response_ms', 'dom_ms', 'load_ms', 'wall_load_ms', 'settled_ms', 'search_term', 'search_ms', 'rows', 'search_rows',
  'transfer_bytes', 'html_bytes', 'viewstate_bytes', 'error', 'js_errors', 'console_errors', 'notes'];
function writeCsv(file, rows) {
  fs.writeFileSync(file, '﻿' + COLS.join(',') + '\n' + rows.map(r => COLS.map(c => csvCell(r[c])).join(',')).join('\n') + '\n', 'utf8');
}

function summarize(runs, qs) {
  const L = [];
  L.push(`# AVEC smoke test ${new Date().toISOString()}`, '', `Base ${opt.base}, company ${opt.company}, user ${opt.user}, lupa ${opt.lupa}, search "${opt.search}"`, '');
  for (const [name, rows] of runs) {
    const pages = rows.filter(r => r.type === 'page'), lupat = rows.filter(r => r.type === 'lupa');
    const cnt = (list) => Object.entries(list.reduce((a, r) => (a[r.status] = (a[r.status] || 0) + 1, a), {})).map(([k, v]) => `${k} ${v}`).join(', ');
    L.push(`## Run: ${name}`, '', `Pages: ${pages.length} (${cnt(pages)})  `, `Lupat: ${lupat.length} (${cnt(lupat)})`, '');
    const tested = pages.filter(r => r.load_ms > 0);
    if (tested.length) {
      const sum = tested.reduce((a, r) => a + r.load_ms, 0);
      L.push(`Total page load ${Math.round(sum / 1000)} s, median ${median(tested.map(r => r.load_ms))} ms, mean ${Math.round(sum / tested.length)} ms`, '');
    }
    L.push('### 20 slowest pages (full load)', '', '| # | ms load | ms server (TTFB) | ms settled | KB html | KB viewstate | page |', '|---|---|---|---|---|---|---|');
    tested.slice().sort((a, b) => b.load_ms - a.load_ms).slice(0, 20).forEach((r, i) =>
      L.push(`| ${i + 1} | ${r.load_ms} | ${r.ttfb_ms} | ${r.settled_ms} | ${kb(r.html_bytes)} | ${kb(r.viewstate_bytes)} | ${r.page_url} |`));
    L.push('', '### Failing pages', '');
    const bad = pages.filter(r => ['ERROR', 'JSERR'].includes(r.status));
    if (!bad.length) L.push('none');
    bad.forEach(r => L.push(`- **${r.status}** ${r.page_url} (${r.title}): ${(r.error || r.js_errors || '').slice(0, 300)}`));
    const warn = pages.filter(r => r.status === 'WARN');
    if (warn.length) { L.push('', `### Pages with console errors only (${warn.length})`, ''); warn.forEach(r => L.push(`- ${r.page_url}: ${r.console_errors.slice(0, 200)}`)); }
    const opened = lupat.filter(r => r.settled_ms > 0);
    if (opened.length) {
      L.push('', '### 20 slowest lupat (click to grid ready)', '', '| # | ms ready | ms server | ms search | rows | lupa | field | page |', '|---|---|---|---|---|---|---|---|');
      opened.slice().sort((a, b) => b.settled_ms - a.settled_ms).slice(0, 20).forEach((r, i) =>
        L.push(`| ${i + 1} | ${r.settled_ms} | ${r.ttfb_ms} | ${r.search_ms ?? ''} | ${r.rows ?? ''} | ${r.lupa_url.split('?')[0]} | ${r.lupa_button} | ${r.page_url} |`));
      const slowSearch = opened.filter(r => r.search_ms > 0).sort((a, b) => b.search_ms - a.search_ms).slice(0, 10);
      if (slowSearch.length) {
        L.push('', '### 10 slowest lupa searches', '');
        slowSearch.forEach(r => L.push(`- ${r.search_ms} ms (${r.search_rows ?? '?'} rows) ${r.lupa_url.split('?')[0]} on ${r.page_url} [${r.lupa_button}]`));
      }
    }
    const badL = lupat.filter(r => ['ERROR', 'JSERR'].includes(r.status));
    L.push('', '### Failing lupat', '');
    if (!badL.length) L.push('none');
    badL.forEach(r => L.push(`- **${r.status}** ${r.lupa_url || r.lupa_button} on ${r.page_url}: ${(r.error || r.js_errors || '').slice(0, 300)}`));
    L.push('');
  }
  if (runs.length === 2) {
    const [a, b] = runs.map(([, rows]) => new Map(rows.filter(r => r.type === 'page' && r.load_ms > 0).map(r => [r.page_url, r])));
    const both = [...a.keys()].filter(k => b.has(k));
    if (both.length) {
      const sa = both.reduce((s, k) => s + a.get(k).load_ms, 0), sb = both.reduce((s, k) => s + b.get(k).load_ms, 0);
      L.push(`## ${runs[0][0]} vs ${runs[1][0]}`, '', `${both.length} pages in both: total load ${Math.round(sa / 1000)} s vs ${Math.round(sb / 1000)} s`, '');
    }
  }
  if (qs && qs.length) {
    L.push('## Query Store (this test window)', '');
    for (const by of ['total', 'avg']) {
      L.push(`### Top 20 by ${by} duration`, '', '| # | total ms | avg ms | execs | avg reads | object | query |', '|---|---|---|---|---|---|---|');
      qs.filter(r => r.rank_by === by).forEach(r => L.push(`| ${r.rank} | ${r.total_ms} | ${r.avg_ms} | ${r.execs} | ${r.avg_logical_reads} | ${r.object_name || ''} | ${String(r.query_text).slice(0, 120).replace(/\|/g, '/')} |`));
      L.push('');
    }
  }
  return L.join('\n');
}
const median = a => { const s = a.slice().sort((x, y) => x - y); return s.length ? s[Math.floor(s.length / 2)] : 0; };
const kb = b => b ? Math.round(b / 1024) : '';

// ---------- Query Store ----------
async function queryStore(sinceUtc) {
  if (!opt['sql-password']) { console.log('Query Store export skipped (no --sql-password)'); return null; }
  let sql;
  try { sql = require('mssql'); } catch { console.log('Query Store export skipped (mssql package missing)'); return null; }
  const [server, instance] = opt['sql-server'].split('\\');
  const pool = await sql.connect({ server: server === '.' ? 'localhost' : server, database: opt['sql-db'], user: opt['sql-user'], password: opt['sql-password'],
    options: { instanceName: instance, trustServerCertificate: true, encrypt: false }, requestTimeout: 120000 });
  const q = (by) => `
    SELECT TOP 20 '${by}' AS rank_by, ROW_NUMBER() OVER (ORDER BY ${by === 'total' ? 'SUM(rs.avg_duration * rs.count_executions)' : 'SUM(rs.avg_duration * rs.count_executions) / NULLIF(SUM(rs.count_executions),0)'} DESC) AS rank,
      q.query_id, OBJECT_SCHEMA_NAME(q.object_id) + '.' + OBJECT_NAME(q.object_id) AS object_name,
      CAST(SUM(rs.avg_duration * rs.count_executions) / 1000.0 AS DECIMAL(18,1)) AS total_ms,
      SUM(rs.count_executions) AS execs,
      CAST(SUM(rs.avg_duration * rs.count_executions) / NULLIF(SUM(rs.count_executions),0) / 1000.0 AS DECIMAL(18,1)) AS avg_ms,
      CAST(MAX(rs.max_duration) / 1000.0 AS DECIMAL(18,1)) AS max_ms,
      CAST(SUM(rs.avg_cpu_time * rs.count_executions) / NULLIF(SUM(rs.count_executions),0) / 1000.0 AS DECIMAL(18,1)) AS avg_cpu_ms,
      CAST(SUM(rs.avg_logical_io_reads * rs.count_executions) / NULLIF(SUM(rs.count_executions),0) AS BIGINT) AS avg_logical_reads,
      MAX(LEFT(REPLACE(REPLACE(qt.query_sql_text, CHAR(13), ' '), CHAR(10), ' '), 400)) AS query_text
    FROM sys.query_store_runtime_stats rs
    JOIN sys.query_store_runtime_stats_interval i ON i.runtime_stats_interval_id = rs.runtime_stats_interval_id
    JOIN sys.query_store_plan p ON p.plan_id = rs.plan_id
    JOIN sys.query_store_query q ON q.query_id = p.query_id
    JOIN sys.query_store_query_text qt ON qt.query_text_id = q.query_text_id
    WHERE rs.last_execution_time >= @since
    GROUP BY q.query_id, q.object_id
    ORDER BY rank`;
  const out = [];
  for (const by of ['total', 'avg']) {
    const r = await pool.request().input('since', sql.DateTimeOffset, sinceUtc).query(q(by));
    out.push(...r.recordset);
  }
  await pool.close();
  return out;
}

// ---------- main ----------
(async () => {
  const ts = stamp();
  const resDir = path.join(__dirname, 'results');
  fs.mkdirSync(resDir, { recursive: true });
  const runNames = String(opt.runs).split(',').map(s => s.trim()).filter(Boolean);
  const filter = items => {
    let l = items;
    if (opt.only) l = l.filter(i => new RegExp(opt.only, 'i').test(i.url));
    if (opt.limit) l = l.slice(0, opt.limit);
    return l;
  };
  const since = new Date();
  const browser = await chromium.launch({ headless: !opt.headed });
  const runs = [];
  try {
    for (const name of runNames) {
      const rows = await runPass(browser, name, filter);
      writeCsv(path.join(resDir, `${ts}-${name}.csv`), rows);
      runs.push([name, rows]);
    }
  } finally { await browser.close(); }
  let qs = null;
  try {
    qs = await queryStore(since);
    if (qs) {
      const cols = ['rank_by', 'rank', 'query_id', 'object_name', 'total_ms', 'execs', 'avg_ms', 'max_ms', 'avg_cpu_ms', 'avg_logical_reads', 'query_text'];
      fs.writeFileSync(path.join(resDir, `querystore-${ts}.csv`), '﻿' + cols.join(',') + '\n' + qs.map(r => cols.map(c => csvCell(r[c])).join(',')).join('\n') + '\n', 'utf8');
    }
  } catch (e) { console.log('Query Store export failed: ' + e.message); }
  const md = summarize(runs, qs);
  const sumFile = path.join(resDir, `${ts}-summary.md`);
  fs.writeFileSync(sumFile, md, 'utf8');
  console.log('\n' + md + '\n\nResults: ' + resDir + '  (' + ts + ')');
  const failing = runs.some(([, rows]) => rows.some(r => r.status === 'ERROR'));
  process.exitCode = failing ? 1 : 0;
})().catch(e => { console.error(e); process.exit(3); });
