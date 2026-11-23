#!/usr/bin/env node
// Compare two smoke runs: node compare.js <baseline.csv> <new.csv> [out.md]
// Reports every page/lupa that was OK (or WARN) in the baseline and is worse now, every one that got fixed,
// before/after load times of the 20 slowest baseline pages, median/total page time and lupa ready time.
'use strict';
const fs = require('fs');

function parseCsv(file) {
  const text = fs.readFileSync(file, 'utf8').replace(/^﻿/, '');
  const rows = []; let row = [], cell = '', q = false;
  for (let i = 0; i < text.length; i++) {
    const c = text[i];
    if (q) {
      if (c === '"') { if (text[i + 1] === '"') { cell += '"'; i++; } else q = false; } else cell += c;
    } else if (c === '"') q = true;
    else if (c === ',') { row.push(cell); cell = ''; }
    else if (c === '\n') { row.push(cell); rows.push(row); row = []; cell = ''; }
    else if (c !== '\r') cell += c;
  }
  if (cell || row.length) { row.push(cell); rows.push(row); }
  const [head, ...data] = rows;
  return data.filter(r => r.length > 1).map(r => Object.fromEntries(head.map((h, i) => [h, r[i]])));
}

const [, , aFile, bFile, outFile] = process.argv;
if (!aFile || !bFile) { console.error('usage: node compare.js <baseline.csv> <new.csv> [out.md]'); process.exit(2); }
const A = parseCsv(aFile), B = parseCsv(bFile);
const key = r => r.type + '|' + r.page_url + '|' + (r.lupa_button || '');
const mapA = new Map(A.map(r => [key(r), r])), mapB = new Map(B.map(r => [key(r), r]));
const rank = { OK: 0, NOOPEN: 0, SKIPPED: 0, WARN: 1, JSERR: 2, ERROR: 3 };
const num = v => (v === '' || v === undefined) ? null : Number(v);
const median = a => { const s = a.slice().sort((x, y) => x - y); return s.length ? s[Math.floor(s.length / 2)] : 0; };

const L = [`# Smoke comparison`, '', `baseline: ${aFile}`, `new: ${bFile}`, ''];
const worse = [], better = [], missing = [];
for (const [k, a] of mapA) {
  const b = mapB.get(k);
  if (!b) { if (a.type === 'page') missing.push(a); continue; }
  if ((rank[b.status] ?? 3) > (rank[a.status] ?? 3)) worse.push([a, b]);
  else if ((rank[b.status] ?? 3) < (rank[a.status] ?? 3)) better.push([a, b]);
}
L.push(`## Regressions (${worse.length})`, '');
if (!worse.length) L.push('none');
worse.forEach(([a, b]) => L.push(`- ${b.type} ${b.page_url} ${b.lupa_button || ''}: ${a.status} -> **${b.status}** ${(b.error || b.js_errors || b.console_errors || '').slice(0, 250)}`));
L.push('', `## Fixed (${better.length})`, '');
if (!better.length) L.push('none');
better.forEach(([a, b]) => L.push(`- ${b.type} ${b.page_url} ${b.lupa_button || ''}: ${a.status} -> ${b.status}`));
if (missing.length) { L.push('', `## Pages missing in the new run (${missing.length})`, ''); missing.forEach(a => L.push(`- ${a.page_url}`)); }

const pagesBoth = [...mapA.values()].filter(a => a.type === 'page' && num(a.load_ms) > 0 && mapB.get(key(a)) && num(mapB.get(key(a)).load_ms) > 0);
const tot = (rows, f) => rows.reduce((s, r) => s + f(r), 0);
const la = pagesBoth.map(a => num(a.load_ms)), lb = pagesBoth.map(a => num(mapB.get(key(a)).load_ms));
const ta = pagesBoth.map(a => num(a.ttfb_ms)), tb = pagesBoth.map(a => num(mapB.get(key(a)).ttfb_ms));
L.push('', `## Pages (${pagesBoth.length} in both runs)`, '', '| | before | after | change |', '|---|---|---|---|');
const line = (n, x, y) => L.push(`| ${n} | ${x} | ${y} | ${x ? (((y - x) / x) * 100).toFixed(1) + '%' : ''} |`);
line('median full load ms', median(la), median(lb));
line('total full load s', Math.round(tot(la, x => x) / 1000), Math.round(tot(lb, x => x) / 1000));
line('median server (TTFB) ms', median(ta), median(tb));
line('total server (TTFB) s', Math.round(tot(ta, x => x) / 1000), Math.round(tot(tb, x => x) / 1000));

const lupaBoth = [...mapA.values()].filter(a => a.type === 'lupa' && num(a.settled_ms) > 0 && mapB.get(key(a)) && num(mapB.get(key(a)).settled_ms) > 0);
if (lupaBoth.length) {
  const ra = lupaBoth.map(a => num(a.settled_ms)), rb = lupaBoth.map(a => num(mapB.get(key(a)).settled_ms));
  L.push('', `## Lupat (${lupaBoth.length} in both runs)`, '', '| | before | after | change |', '|---|---|---|---|');
  line('median ready ms', median(ra), median(rb));
  line('total ready s', Math.round(tot(ra, x => x) / 1000), Math.round(tot(rb, x => x) / 1000));
}

L.push('', '## 20 slowest baseline pages', '', '| page | load before | load after | server before | server after |', '|---|---|---|---|---|');
pagesBoth.slice().sort((x, y) => num(y.load_ms) - num(x.load_ms)).slice(0, 20).forEach(a => {
  const b = mapB.get(key(a));
  L.push(`| ${a.page_url} | ${a.load_ms} | ${b.load_ms} | ${a.ttfb_ms} | ${b.ttfb_ms} |`);
});
const md = L.join('\n') + '\n';
if (outFile) fs.writeFileSync(outFile, md, 'utf8');
console.log(md);
process.exitCode = worse.length ? 1 : 0;
