<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PlatinumWeb._Default" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>AVEC Accounting</title>
    <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <link href="css/font-awesome-4.3.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="css/avec-dashboard.css?v=5" rel="stylesheet" />
    <link href="css/avec-home.css?v=1" rel="stylesheet" />
</head>
<body class="avec-dashboard avec-home">
    <form id="form1" runat="server">
        <%-- Kryefaqja (2026-11-27): majtas modulet, djathtas faqet dhe raportet e modulit te zgjedhur.
             Ne hyrje djathtas shfaqen vetem faqet/raportet me yll (favoritet, per perdorues: T_PERDORUES_FAVORITE). --%>
        <div class="home" id="home">
            <aside class="home-nav" aria-label="Modulet">
                <label class="home-search">
                    <i class="fa fa-search" aria-hidden="true"></i>
                    <input type="search" id="homeSearch" placeholder="Kërko faqe ose raport…" autocomplete="off" />
                    <kbd class="home-kbd">/</kbd>
                </label>
                <button type="button" class="home-nav-item home-nav-fav" id="navFav" data-key="__fav">
                    <span class="home-nav-icon"><i class="fa fa-star"></i></span>
                    <span class="home-nav-text"><span class="home-nav-title">Të preferuarat</span><span class="home-nav-desc">Faqet dhe raportet me yll</span></span>
                    <span class="home-nav-count" id="navFavCount"></span>
                </button>
                <div class="home-nav-label">Modulet</div>
                <nav class="home-nav-list" id="navList"></nav>
            </aside>
            <section class="home-main" aria-live="polite">
                <header class="home-head" id="homeHead">
                    <span class="home-head-icon" id="headIcon"><i class="fa fa-star"></i></span>
                    <div class="home-head-text">
                        <h1 id="headTitle">Të preferuarat</h1>
                        <p id="headDesc"></p>
                    </div>
                    <label class="home-filter" id="headFilterWrap" hidden>
                        <i class="fa fa-filter" aria-hidden="true"></i>
                        <input type="search" id="headFilter" placeholder="Filtro në këtë modul…" autocomplete="off" />
                    </label>
                </header>
                <div class="home-body" id="homeBody"></div>
            </section>
        </div>
        <p class="dash-empty" id="dashEmpty" hidden>Nuk keni akses në asnjë modul. Kontaktoni administratorin.</p>
    </form>

    <script>
        (function () {
            // The home screen is the only menu: its modules are built from the main frame's navbar and top menu
            // (FaqeKryesore), which the server has already filtered by the user's rights.
            var MODULES = {
                'Administrimi': { icon: 'fa-shield', desc: 'Përdorues, role dhe ndërmarrje' },
                'kontabiliteti': { icon: 'fa-book', desc: 'Llogari dhe fletë kontabël' },
                'inventari': { icon: 'fa-cubes', desc: 'Artikuj, magazina, hyrje e dalje' },
                'blerjeShitje': { icon: 'fa-shopping-cart', desc: 'Klientë, furnitorë dhe fatura' },
                'arkaBanka': { icon: 'fa-university', desc: 'Arkëtime, pagesa dhe banka' },
                'hr': { icon: 'fa-users', desc: 'Punonjës, paga dhe orare' },
                'pagesa': { icon: 'fa-credit-card', desc: 'Urdhërpagesat' },
                'prodhimi': { icon: 'fa-cogs', desc: 'Planifikim dhe ekzekutim' },
                'qk': { icon: 'fa-sitemap', desc: 'Qendrat e kostos' },
                'amortizimi': { icon: 'fa-building', desc: 'Aktivet afatgjata' },
                'rap': { icon: 'fa-file-text-o', desc: 'Të gjitha raportet' },
                'analizeBuxheti': { icon: 'fa-line-chart', desc: 'Analiza e buxhetit' },
                'buxheti': { icon: 'fa-pie-chart', desc: 'Planifikimi i buxhetit' },
                'help': { icon: 'fa-question-circle', desc: 'Manuali dhe ndihma' },
                'settings': { icon: 'fa-cog', desc: 'Cilësimet' },
                // modules that exist only in the top menu
                'konfigurime': { icon: 'fa-sliders', desc: 'Dokumente, formate dhe makro' },
                'importEksport': { icon: 'fa-exchange', desc: 'Import, eksport dhe integrime', text: 'Import / Eksport' },
                'sherbime': { icon: 'fa-wrench', desc: 'Riparime dhe receta optike', text: 'Shërbimet' }
            };

            // Which card each branch of the top menu belongs to, by menu path (item names joined with "/").
            // The longest matching prefix wins; an item that is also in the navbar stays in the navbar's card.
            var MENU_TO_MODULE = {
                'administrimi': 'Administrimi',
                'administrimi/KonfigurimeFtp.aspx': 'importEksport',
                'konfigurime': 'konfigurime',
                'konfigurime/amortizimi': 'amortizimi',
                'konfigurime/cmimet': 'blerjeShitje',
                'konfigurime/Shto_KategoriShpenzimi.aspx': 'kontabiliteti',
                'konfigurime/qk': 'qk',
                'konfigurime/rap-financiare': 'kontabiliteti',
                'konfigurime/skedulimi-punonjesve': 'hr',
                'konfigurime/Shto_KPF.aspx': 'kontabiliteti',
                'konfigurime/konfig-urdher-pagese': 'pagesa',
                'konfigurime/zbritje-analitike': 'blerjeShitje',
                // gjithcka per import/eksport dhe integrime ne nje karte te vetme
                'konfigurime/import-eksport': 'importEksport',
                'konfigurime/ElementePerIntegrim.aspx': 'importEksport',
                'celje': 'konfigurime',
                'celje/Shto_AgjentShitje.aspx': 'blerjeShitje',
                'celje/arka-banka': 'arkaBanka',
                'celje/artikujt': 'inventari',
                'celje/Shto_Automjete.aspx': 'blerjeShitje',
                'celje/komponente-buxheti': 'buxheti',
                'celje/elemnte-page': 'hr',
                'celje/elemente-prodhimi': 'prodhimi',
                'celje/karta-klienti': 'blerjeShitje',
                'celje/kf': 'blerjeShitje',
                'celje/Shto_Llogari.aspx': 'kontabiliteti',
                'celje/LlojDifekti.aspx': 'sherbime',
                'celje/Makro.aspx': 'konfigurime',
                'celje/njesi-administrative': 'inventari',
                'celje/njesi-administrative/Shto_NjesiProdhimi.aspx': 'prodhimi',
                'celje/njesi-administrative/Shto_PikeShitjeFurnizimi.aspx?sf=shitje': 'blerjeShitje',
                'celje/njesi-administrative/Shto_PikeShitjeFurnizimi.aspx?sf=furnizim': 'blerjeShitje',
                'celje/Shto_Punonjes.aspx': 'hr',
                'celje/komponente-qk': 'qk',
                'celje/StatusRiparimi.aspx': 'sherbime',
                'celje/Shto_Transportues.aspx': 'blerjeShitje',
                'regjistrime': 'konfigurime',
                'regjistrime/amortizimi': 'amortizimi',
                'regjistrime/fatura-blerje': 'blerjeShitje',
                'regjistrime/EkzekutimBuxheti': 'buxheti',
                'regjistrime/flete-doganore': 'blerjeShitje',
                'regjistrime/fk': 'kontabiliteti',
                'regjistrime/lidhja-dokumentave': 'blerjeShitje',
                'regjistrime/listpagesa': 'hr',
                'regjistrime/magazina': 'inventari',
                'regjistrime/prodhim': 'prodhimi',
                'regjistrime/regjistrim-qk': 'qk',
                'regjistrime/receta-optike': 'sherbime',
                'regjistrime/regjistrim-riparimi': 'sherbime',
                'regjistrime/rezervime': 'inventari',
                'regjistrime/SigurimBuxheti': 'buxheti',
                'regjistrime/fatura-shitjesh': 'blerjeShitje',
                'regjistrime/shperndarja-shpenzimeve': 'kontabiliteti',
                'regjistrime/urdher-pagesa': 'pagesa',
                'regjistrime/veprime-arka-banka': 'arkaBanka',
                'regjistrime/veprime-kf': 'blerjeShitje',
                'raportet': 'rap'
            };


            // leaf labels that only make sense next to their parent ("Lista", "E Re", ...)
            var GENERIC = /^(lista|e re|veprim i ri|azhornim i ri|mbyllje e re)$/i;
            var collator = window.Intl && Intl.Collator ? new Intl.Collator('sq', { sensitivity: 'base', numeric: true }) : null;
            function byText(a, b) {
                return collator ? collator.compare(a || '', b || '') : (a < b ? -1 : a > b ? 1 : 0);
            }

            var host = window.parent && window.parent !== window ? window.parent : null;
            // faqet qe perdoruesi mund te hape (sipas te drejtave), nga menuja e vjeter lart dhe navbar-i qe nuk shfaqen me:
            // FaqeKryesore.aspx.cs i dergon si window.avecMenuTeDhena; ketu marrin te njejten forme si kontrollet e dikurshme
            var teDhena = host && host.avecMenuTeDhena;
            function nyje(d) {
                var femijet = (d.c || []).map(nyje);
                return {
                    name: d.n, GetText: function () { return d.t; }, GetVisible: function () { return true; },
                    GetItemCount: function () { return femijet.length; }, GetItem: function (i) { return femijet[i]; }
                };
            }
            var grupet = teDhena ? teDhena.navbar.map(nyje) : [];
            var navbar = teDhena && { GetGroupCount: function () { return grupet.length; }, GetGroup: function (i) { return grupet[i]; } };
            var rrenja = teDhena && nyje({ n: '', t: '', c: teDhena.menu });
            var topMenu = teDhena && { GetRootItem: function () { return rrenja; } };
            var navList = document.getElementById('navList');
            var navFav = document.getElementById('navFav');
            var navFavCount = document.getElementById('navFavCount');
            var body = document.getElementById('homeBody');
            var headIcon = document.getElementById('headIcon');
            var headTitle = document.getElementById('headTitle');
            var headDesc = document.getElementById('headDesc');
            var headFilterWrap = document.getElementById('headFilterWrap');
            var headFilter = document.getElementById('headFilter');
            var search = document.getElementById('homeSearch');
            var empty = document.getElementById('dashEmpty');

            function el(tag, cls, text) {
                var e = document.createElement(tag);
                if (cls) e.className = cls;
                if (text) e.textContent = text;
                return e;
            }

            function key(name) {
                return (name || '').replace(/[?&]+$/, '').toLowerCase();
            }

            if (!host) return;

            // ---- collect the modules and their pages ----
            var modules = {};   // key -> { key, text, entries: [] }
            var order = [];
            var seen = {};      // page name -> entry

            function module(k, text) {
                if (!modules[k]) {
                    var meta = MODULES[k] || {};
                    modules[k] = { key: k, text: text || meta.text || k, entries: [] };
                    order.push(k);
                } else if (text && modules[k].text === k) {
                    modules[k].text = text;
                }
                return modules[k];
            }

            // navbar: the modules as they were
            var navModuleOf = {};
            if (navbar) {
                for (var i = 0; i < navbar.GetGroupCount(); i++) {
                    var g = navbar.GetGroup(i);
                    if (!g.GetVisible()) continue;
                    for (var j = 0; j < g.GetItemCount(); j++) {
                        var it = g.GetItem(j);
                        if (!it.GetVisible() || !it.GetText()) continue;
                        var m = module(g.name, g.GetText());
                        var entry = { title: it.GetText(), desc: '', section: '', source: navbar, item: it };
                        m.entries.push(entry);
                        seen[key(it.name)] = entry;
                        navModuleOf[key(it.name)] = g.name;
                    }
                }
            }

            function moduleFor(path) {
                for (var p = path.length; p > 0; p--) {
                    var k = MENU_TO_MODULE[path.slice(0, p).join('/')];
                    if (k) return k;
                }
                return null;
            }

            // top menu: every page the user may open, placed in the card it belongs to
            function walk(node, path, texts, section) {
                for (var n = 0; n < node.GetItemCount(); n++) {
                    var it = node.GetItem(n);
                    if (!it.GetVisible()) continue;
                    var p = path.concat(it.name), t = texts.concat(it.GetText());
                    if (it.GetItemCount() > 0) { walk(it, p, t, section); continue; }
                    if (!it.name || !it.GetText()) continue;
                    // t = [section, ..., parent, leaf]; the levels between the section and the leaf give the context
                    var parentText = t.length > 2 ? t[t.length - 2] : '';
                    var title = it.GetText(), desc = parentText, header = title;
                    if (GENERIC.test(title) && parentText) {
                        title = parentText;
                        header = /^lista$/i.test(it.GetText()) ? parentText
                            : /^e re$/i.test(it.GetText()) ? 'Shto ' + parentText.toLowerCase()
                            : parentText + ' – ' + it.GetText();
                        desc = (t.length > 3 ? t[t.length - 3] + ' · ' : '') + it.GetText();
                    }
                    var k = key(it.name);
                    var target = navModuleOf[k] || moduleFor(p);
                    if (!target) continue;
                    var known = seen[k];
                    if (known) {
                        // already on a card from the navbar: just tell which part of the program it belongs to
                        if (!known.section) known.section = section;
                        continue;
                    }
                    var entry = { title: title, header: header, desc: desc, section: section, source: topMenu, item: it };
                    module(target).entries.push(entry);
                    seen[k] = entry;
                }
            }

            if (topMenu) {
                var root = topMenu.GetRootItem();
                for (var r = 0; r < root.GetItemCount(); r++) {
                    var top = root.GetItem(r);
                    if (top.name === 'ikonaImazhPerdorues') continue;
                    if (top.name === 'konfigurime' && !modules.konfigurime) module('konfigurime', top.GetText());
                    walk(top, [top.name], [top.GetText()], top.GetText());
                }
            }

            // ---- grouping inside a module, by what the page is for (2026-11-22) ----
            // sections of the top menu -> the function they serve, in this order
            var SECTION_OF = {
                'regjistrime': 'Dokumente', 'çelje': 'Të dhëna bazë', 'celje': 'Të dhëna bazë',
                'konfigurime': 'Konfigurime', 'raporte': 'Raporte', 'raportet': 'Raporte', 'administrim': 'Të dhëna bazë'
            };
            var SECTION_ORDER = ['', 'Dokumente', 'Të dhëna bazë', 'Konfigurime', 'Raporte', 'Administrim'];
            // each module also shows its own reports (they stay in the "Raporte" card too)
            var MODULE_REPORTS = {
                kontabiliteti: ['raportet.aspx?idmod=7'],
                inventari: ['raportet.aspx?idmod=16', 'raportegrida.aspx?lloji=gjendjaemagazines', 'raportegrida.aspx?lloji=gjendjaeartikujvemeseriale'],
                blerjeShitje: ['raportet.aspx?idmod=12', 'raportet.aspx?idmod=13', 'raportet.aspx?idmod=9'],
                arkaBanka: ['raportet.aspx?idmod=2', 'raportet.aspx?idmod=6'],
                hr: ['raportet.aspx?idmod=17'],
                prodhimi: ['raportet.aspx?idmod=18'],
                qk: ['raportet.aspx?idmod=20'],
                amortizimi: ['raportet.aspx?idmod=21'],
                buxheti: ['raportet.aspx?idmod=57']
            };

            function sectionOf(e) {
                var s = SECTION_OF[(e.section || '').toLowerCase()];
                if (s !== undefined) return s;
                return e.section ? e.section : '';
            }

            // a page that creates a new document (its list is shown on another card)
            function isNewForm(e) {
                var u = (e.item.name || '').toLowerCase();
                return u.indexOf('shtim_modifikim=shtim') !== -1 || /(^|·\s*)e re$/i.test(e.desc || '') ||
                    /^shto /i.test(e.title) || / (e re|i ri)$/i.test(e.title);
            }

            // page name without "Shto"/"Shto_" and without the letter e, so Shto_Planifikim ~ Planifikimi,
            // ShtoVeprimBanka ~ VeprimeBanka; query values (lloji=..., lloj=...) must match too
            function pageKey(u) {
                var parts = (u || '').toLowerCase().split('?');
                var name = parts[0].replace('.aspx', '').replace(/shto_?/g, '').replace(/e/g, '');
                var vals = (parts[1] || '').split('&').map(function (kv) { return kv.split('=')[1] || ''; })
                    .filter(function (v) { return v && v !== 'shtim'; });
                return { name: name, vals: vals };
            }

            function sameDocument(list, form) {
                if (list.title.toLowerCase() === form.title.toLowerCase().replace(/^shto /, '')) return true;
                var a = pageKey(list.item.name), b = pageKey(form.item.name);
                if (!(a.name && b.name && (a.name.indexOf(b.name) === 0 || b.name.indexOf(a.name) === 0))) return false;
                return a.vals.every(function (v) { return b.vals.indexOf(v) !== -1; }) &&
                    b.vals.every(function (v) { return a.vals.indexOf(v) !== -1; });
            }

            // ---- reports inside the right panel (no separate Raportet.aspx screen) ----
            var REPORT_URL = /^raportet\.aspx\?idmod=(\d+)$/;
            function reportModuleOf(e) {
                var m = REPORT_URL.exec(key(e.item.name));
                return m ? parseInt(m[1], 10) : 0;
            }
            // report categories the user may open (they have the "Raportet.aspx?idmod=N" page), with their names
            var reportCats = {};
            Object.keys(seen).forEach(function (k) {
                var e = seen[k], id = reportModuleOf(e);
                if (id) reportCats[id] = e.title;
            });
            var reports = {};      // idmod -> [{ id, emri }]
            var pending = {};      // idmod -> true while loading

            function api(method, data) {
                return fetch('Default.aspx/' + method, {
                    method: 'POST', credentials: 'same-origin',
                    headers: { 'Content-Type': 'application/json; charset=utf-8' },
                    body: JSON.stringify(data || {})
                }).then(function (r) { if (!r.ok) throw new Error(r.status); return r.json(); })
                  .then(function (r) { return r.d; });
            }

            function loadReports(ids) {
                ids = ids.filter(function (id) { return reportCats[id] && !reports[id] && !pending[id]; });
                if (!ids.length) return Promise.resolve();
                ids.forEach(function (id) { pending[id] = true; });
                return api('MerrRaportet', { module: ids }).then(function (txt) {
                    var res = JSON.parse(txt || '{}');
                    ids.forEach(function (id) { reports[id] = res[id] || []; delete pending[id]; });
                }).catch(function () {
                    ids.forEach(function (id) { reports[id] = reports[id] || null; delete pending[id]; });
                });
            }

            function reportEntry(idmod, r) {
                return { title: r.emri, desc: reportCats[idmod] || 'Raporte', report: true, idmod: idmod, idraporti: r.id, fav: 'rap:' + idmod + ':' + r.id };
            }

            // ---- favourites (stars), saved per user ----
            var favs = [];
            var favSet = {};
            function setFavs(list) {
                favs = list.slice();
                favSet = {};
                favs.forEach(function (k) { favSet[k] = true; });
            }
            function favKey(e) { return e.fav || key(e.item && e.item.name); }

            function toggleFav(e, btn) {
                var k = favKey(e), on = !favSet[k];
                if (on) { favs.push(k); favSet[k] = true; } else { favs = favs.filter(function (x) { return x !== k; }); delete favSet[k]; }
                if (btn) { btn.classList.toggle('on', on); btn.setAttribute('aria-pressed', on); btn.title = on ? 'Hiq nga të preferuarat' : 'Shto te të preferuarat'; }
                updateFavCount();
                api('NdryshoFavorit', { celesi: k, shto: on }).then(function (ok) {
                    if (ok === false) throw new Error('nuk u ruajt');
                }).catch(function () {
                    // revert: the star was not saved
                    if (on) { favs = favs.filter(function (x) { return x !== k; }); delete favSet[k]; } else { favs.push(k); favSet[k] = true; }
                    if (btn) btn.classList.toggle('on', !on);
                    updateFavCount();
                    toast('Ylli nuk u ruajt. Provoni përsëri.');
                });
                // the tile moves to / from the starred group at the top
                if (current !== '__search') setTimeout(function () { render(true); }, 260);
            }

            // ---- recently opened (per browser, only a convenience) ----
            var RECENT_KEY = 'avec.home.recent.' + ((host.hfState && host.hfState.Get && host.hfState.Get('idPerdoruesi')) || '');
            function recentList() {
                try { return JSON.parse(localStorage.getItem(RECENT_KEY) || '[]') || []; } catch (err) { return []; }
            }
            function rememberRecent(e) {
                try {
                    var k = favKey(e), list = recentList().filter(function (x) { return x !== k; });
                    list.unshift(k);
                    localStorage.setItem(RECENT_KEY, JSON.stringify(list.slice(0, 8)));
                } catch (err) { /* private mode */ }
            }

            // every page entry by its key, with the module it belongs to
            var byKey = {};
            order.forEach(function (mk) {
                modules[mk].entries.forEach(function (e) { e.module = mk; byKey[key(e.item.name)] = e; });
            });
            function moduleOfReport(idmod) {
                for (var mk in MODULE_REPORTS) {
                    if (MODULE_REPORTS[mk].indexOf('raportet.aspx?idmod=' + idmod) !== -1 && modules[mk]) return mk;
                }
                return 'rap';
            }
            // a favourite/recent key -> entry (null while its report list is not loaded or the user lost the right)
            function resolve(k) {
                var m = /^rap:(\d+):(\d+)$/.exec(k);
                if (!m) return byKey[k] || null;
                var idmod = parseInt(m[1], 10), id = parseInt(m[2], 10), list = reports[idmod];
                if (!list) return null;
                for (var i = 0; i < list.length; i++) if (list[i].id === id) { var e = reportEntry(idmod, list[i]); e.module = moduleOfReport(idmod); return e; }
                return null;
            }
            function reportModulesIn(keys) {
                var ids = [];
                keys.forEach(function (k) { var m = /^rap:(\d+):/.exec(k); if (m && ids.indexOf(+m[1]) === -1) ids.push(+m[1]); });
                return ids;
            }

            // ---- opening a page or a report ----
            function openEntry(e, ev) {
                rememberRecent(e);
                if (e.report) {
                    var title = e.title;
                    host.avecMenuTitle = { url: 'Raporti.aspx?idraporti=' + e.idraporti, title: title };
                    if (ev && (ev.ctrlKey || ev.metaKey)) {
                        window.open('Raporti.aspx?idraporti=' + e.idraporti + '&idmod=' + e.idmod + '&Filtro=true', '_blank');
                        return;
                    }
                    if (host.SucceededCallbackInfoLart) { try { host.SucceededCallbackInfoLart({ emerKomponente: title }); } catch (err) { } }
                    var p = ['idraporti=' + e.idraporti, 'idmod=' + e.idmod, 'windowWidth=' + window.innerWidth, 'Filtro=true', 'previousPage=Kryefaqja'];
                    var scope = /[?&]scopeID=([^&]+)/.exec(location.search);
                    if (scope) p.push('scopeID=' + scope[1]);
                    window.location.href = 'Raporti.aspx?' + p.join('&');
                    return;
                }
                host.avecMenuTitle = { url: e.item.name || '', title: e.header || e.title };
                host.kontrolloTeDrejta(e.source, { item: e.item, processOnServer: false, htmlEvent: ev || window.event }, e.item.name);
            }
            function openNew(e, f, ev) {
                rememberRecent(e);
                var title = f.header && f.header !== f.title ? f.header : 'Shto ' + e.title.toLowerCase();
                host.avecMenuTitle = { url: f.item.name || '', title: title };
                host.kontrolloTeDrejta(f.source, { item: f.item, processOnServer: false, htmlEvent: ev || window.event }, f.item.name);
            }

            // ---- tiles ----
            function tile(e, opts) {
                opts = opts || {};
                var meta = MODULES[e.module] || {};
                var t = el('div', 'home-tile' + (e.report ? ' is-report' : ''));
                t.setAttribute('role', 'button');
                t.tabIndex = 0;
                var ic = el('span', 'home-tile-icon');
                ic.appendChild(el('i', 'fa ' + (e.report ? 'fa-file-text-o' : (meta.icon || 'fa-file-o'))));
                t.appendChild(ic);
                var tx = el('span', 'home-tile-text');
                tx.appendChild(el('span', 'home-tile-title', e.title));
                var desc = opts.showModule ? (modules[e.module] ? modules[e.module].text : '') + (e.report && e.desc ? ' · ' + e.desc : '')
                    : (e.newForm ? (e.desc || '').replace(/\s*·?\s*lista$/i, '') : (e.report ? '' : e.desc));
                if (desc) tx.appendChild(el('span', 'home-tile-desc', desc));
                t.appendChild(tx);
                var acts = el('span', 'home-tile-actions');
                if (e.newForm) {
                    var nb = el('button', 'home-tile-new');
                    nb.type = 'button';
                    nb.title = 'Shto dokument të ri';
                    nb.innerHTML = '<i class="fa fa-plus"></i><span>Shto</span>';
                    nb.addEventListener('click', function (ev) { ev.stopPropagation(); openNew(e, e.newForm, ev); });
                    acts.appendChild(nb);
                }
                var on = !!favSet[favKey(e)];
                var star = el('button', 'home-star' + (on ? ' on' : ''));
                star.type = 'button';
                star.setAttribute('aria-pressed', on);
                star.title = on ? 'Hiq nga të preferuarat' : 'Shto te të preferuarat';
                star.innerHTML = '<i class="fa fa-star-o"></i><i class="fa fa-star"></i>';
                star.addEventListener('click', function (ev) { ev.stopPropagation(); toggleFav(e, star); });
                acts.appendChild(star);
                t.appendChild(acts);
                t.addEventListener('click', function (ev) { openEntry(e, ev); });
                t.addEventListener('keydown', function (ev) {
                    if (ev.target !== t) return;
                    if (ev.key === 'Enter' || ev.key === ' ') { ev.preventDefault(); openEntry(e, ev); }
                });
                return t;
            }

            function section(title, items, opts) {
                opts = opts || {};
                var s = el('section', 'home-section' + (opts.cls ? ' ' + opts.cls : ''));
                if (opts.id) s.id = opts.id;
                if (title) {
                    var h = el('h2', 'home-section-title');
                    if (opts.icon) { h.appendChild(el('i', 'fa ' + opts.icon)); }
                    h.appendChild(document.createTextNode(title));
                    if (items && items.length) h.appendChild(el('span', 'home-section-count', String(items.length)));
                    s.appendChild(h);
                }
                var g = el('div', 'home-grid');
                (items || []).forEach(function (e) { g.appendChild(tile(e, opts)); });
                s.appendChild(g);
                return s;
            }

            function loadingSection(title) {
                var s = el('section', 'home-section');
                var h = el('h2', 'home-section-title'); h.appendChild(el('i', 'fa fa-file-text-o')); h.appendChild(document.createTextNode(title)); s.appendChild(h);
                var g = el('div', 'home-grid');
                for (var i = 0; i < 4; i++) g.appendChild(el('div', 'home-tile home-skeleton'));
                s.appendChild(g);
                return s;
            }

            function emptyState(icon, title, text) {
                var d = el('div', 'home-empty');
                var i = el('span', 'home-empty-icon'); i.appendChild(el('i', 'fa ' + icon)); d.appendChild(i);
                d.appendChild(el('h3', '', title));
                if (text) d.appendChild(el('p', '', text));
                return d;
            }

            var toastTimer;
            function toast(msg) {
                var t = document.getElementById('homeToast');
                if (!t) { t = el('div', 'home-toast'); t.id = 'homeToast'; document.body.appendChild(t); }
                t.textContent = msg;
                t.classList.add('show');
                clearTimeout(toastTimer);
                toastTimer = setTimeout(function () { t.classList.remove('show'); }, 3200);
            }

            // ---- the pages of one module, grouped (list + its "new" form = one tile with "+ Shto") ----
            function moduleView(m) {
                var entries = m.entries.filter(function (e) { return !reportModuleOf(e); });
                var reportIds = [];
                m.entries.forEach(function (e) { var id = reportModuleOf(e); if (id && reportIds.indexOf(id) === -1) reportIds.push(id); });
                (MODULE_REPORTS[m.key] || []).forEach(function (u) {
                    var mm = REPORT_URL.exec(u);
                    if (mm) { if (reportCats[+mm[1]] && reportIds.indexOf(+mm[1]) === -1) reportIds.push(+mm[1]); return; }
                    var r = seen[u];
                    if (r && entries.indexOf(r) === -1)
                        entries.push({ title: r.title, header: r.header || r.title, desc: '', section: 'Raporte', source: r.source, item: r.item, module: m.key, extraReport: true });
                });

                var forms = entries.filter(isNewForm), lists = entries.filter(function (e) { return !isNewForm(e); });
                var used = [];
                lists.forEach(function (l) {
                    l.newForm = null;
                    if (l.extraReport) return;
                    for (var i = 0; i < forms.length; i++) {
                        if (used.indexOf(forms[i]) === -1 && sameDocument(l, forms[i])) { l.newForm = forms[i]; used.push(forms[i]); break; }
                    }
                });
                var shown = lists.concat(forms.filter(function (f) { return used.indexOf(f) === -1; }));

                var sections = {}, names = [];
                shown.forEach(function (e) {
                    var s = e.newForm && sectionOf(e) === '' ? sectionOf(e.newForm) : sectionOf(e);
                    if (!sections[s]) { sections[s] = []; names.push(s); }
                    sections[s].push(e);
                });
                names.sort(function (a, b) {
                    var ia = SECTION_ORDER.indexOf(a), ib = SECTION_ORDER.indexOf(b);
                    if (ia === -1) ia = SECTION_ORDER.length; if (ib === -1) ib = SECTION_ORDER.length;
                    return ia - ib || byText(a, b);
                });
                names.forEach(function (s) { sections[s].sort(function (a, b) { return byText(a.title, b.title) || byText(a.desc, b.desc); }); });
                reportIds.sort(function (a, b) { return byText(reportCats[a], reportCats[b]); });
                return { names: names, sections: sections, reportIds: reportIds };
            }

            var SECTION_ICON = { 'Dokumente': 'fa-file-o', 'Të dhëna bazë': 'fa-database', 'Konfigurime': 'fa-sliders', 'Raporte': 'fa-bar-chart', 'Administrim': 'fa-shield' };

            function matches(e, f) {
                if (!f) return true;
                return ((e.title || '') + ' ' + (e.desc || '')).toLowerCase().indexOf(f) !== -1;
            }

            // ---- views ----
            var current = '__fav';

            function setHead(icon, title, desc, filter) {
                headIcon.innerHTML = '';
                headIcon.appendChild(el('i', 'fa ' + icon));
                headTitle.textContent = title;
                headDesc.textContent = desc || '';
                headFilterWrap.hidden = !filter;
            }

            function renderFavourites() {
                setHead('fa-star', 'Të preferuarat', 'Faqet dhe raportet që keni shënuar me yll', false);
                body.innerHTML = '';
                var waiting = reportModulesIn(favs.concat(recentList())).filter(function (id) { return reportCats[id] && !reports[id]; });
                var items = favs.map(resolve).filter(Boolean);
                if (items.length) {
                    // grouped by module, in the order of the modules on the left
                    var groups = {}, gnames = [];
                    items.forEach(function (e) { if (!groups[e.module]) { groups[e.module] = []; gnames.push(e.module); } groups[e.module].push(e); });
                    gnames.sort(function (a, b) { return byText(modules[a] ? modules[a].text : a, modules[b] ? modules[b].text : b); });
                    gnames.forEach(function (mk) {
                        var meta = MODULES[mk] || {};
                        groups[mk].sort(function (a, b) { return (a.report ? 1 : 0) - (b.report ? 1 : 0) || byText(a.title, b.title); });
                        body.appendChild(section(modules[mk] ? modules[mk].text : 'Raporte', groups[mk], { icon: meta.icon || 'fa-folder-o' }));
                    });
                } else if (!waiting.length) {
                    body.appendChild(emptyState('fa-star-o', 'Ende nuk keni faqe të preferuara',
                        'Zgjidhni një modul majtas dhe shtypni yllin ☆ te faqet ose raportet që përdorni më shpesh. Ato do t’ju presin këtu sa herë hapni programin.'));
                }
                if (waiting.length) body.appendChild(loadingSection('Duke ngarkuar raportet…'));
                var rec = recentList().filter(function (k) { return !favSet[k]; }).map(resolve).filter(Boolean).slice(0, 6);
                if (rec.length) body.appendChild(section('Hapur së fundmi', rec, { icon: 'fa-history', showModule: true, cls: 'home-section-recent' }));
                if (waiting.length) loadReports(waiting).then(function () { if (current === '__fav') render(); });
            }

            function renderModule(m) {
                var meta = MODULES[m.key] || { icon: 'fa-folder-o' };
                setHead(meta.icon || 'fa-folder-o', m.text, meta.desc || '', true);
                body.innerHTML = '';
                var f = (headFilter.value || '').trim().toLowerCase();
                var v = moduleView(m);
                var favHere = [], n = 0;
                function notFav(e) { if (favSet[favKey(e)]) { favHere.push(e); return false; } return true; }
                var blocks = [];
                v.names.forEach(function (s) {
                    var items = v.sections[s].filter(function (e) { return matches(e, f); }).filter(notFav);
                    n += items.length;
                    if (items.length) blocks.push(section(s || 'Kryesore', items, { icon: SECTION_ICON[s] || 'fa-th-large' }));
                });
                var missing = [];
                v.reportIds.forEach(function (id) {
                    var list = reports[id];
                    if (list === undefined) { missing.push(id); blocks.push(loadingSection('Raporte · ' + reportCats[id])); return; }
                    if (list === null) { blocks.push(emptyState('fa-exclamation-triangle', 'Raportet nuk u ngarkuan', 'Rifreskoni faqen për të provuar përsëri.')); return; }
                    var items = list.map(function (r) { var e = reportEntry(id, r); e.module = m.key; return e; })
                        .filter(function (e) { return matches(e, f); }).filter(notFav)
                        .sort(function (a, b) { return byText(a.title, b.title); });
                    n += items.length;
                    var title = v.reportIds.length > 1 || m.key === 'rap' ? 'Raporte · ' + reportCats[id] : 'Raporte';
                    if (items.length) blocks.push(section(title, items, { icon: 'fa-bar-chart', id: 'raporte-' + id }));
                });
                if (favHere.length) {
                    favHere.sort(function (a, b) { return (a.report ? 1 : 0) - (b.report ? 1 : 0) || byText(a.title, b.title); });
                    body.appendChild(section('Të preferuarat', favHere, { icon: 'fa-star', cls: 'home-section-fav' }));
                }
                blocks.forEach(function (b) { body.appendChild(b); });
                if (!n && !favHere.length && !missing.length)
                    body.appendChild(f ? emptyState('fa-search', 'Asgjë nuk përputhet me “' + headFilter.value + '”', '') : emptyState('fa-folder-open-o', 'Ky modul nuk ka faqe', ''));
                if (missing.length) loadReports(missing).then(function () { if (current === m.key) render(true); });
            }

            function renderSearch(q) {
                setHead('fa-search', 'Kërkimi', '“' + q + '” në të gjitha modulet', false);
                body.innerHTML = '';
                var f = q.toLowerCase(), pages = [], reps = [];
                order.forEach(function (mk) {
                    modules[mk].entries.forEach(function (e) { if (!reportModuleOf(e) && matches(e, f)) pages.push(e); });
                });
                var all = Object.keys(reportCats).map(Number), missing = all.filter(function (id) { return reports[id] === undefined; });
                all.forEach(function (id) {
                    (reports[id] || []).forEach(function (r) {
                        var e = reportEntry(id, r); e.module = moduleOfReport(id);
                        if (matches(e, f)) reps.push(e);
                    });
                });
                pages.sort(function (a, b) { return byText(a.title, b.title); });
                reps.sort(function (a, b) { return byText(a.title, b.title); });
                if (pages.length) body.appendChild(section('Faqe', pages.slice(0, 60), { icon: 'fa-file-o', showModule: true }));
                if (reps.length) body.appendChild(section('Raporte', reps.slice(0, 60), { icon: 'fa-bar-chart', showModule: true }));
                if (missing.length) {
                    body.appendChild(loadingSection('Duke kërkuar edhe në raporte…'));
                    loadReports(missing).then(function () { if (current === '__search' && search.value.trim()) render(); });
                } else if (!pages.length && !reps.length) {
                    body.appendChild(emptyState('fa-search', 'Asgjë nuk u gjet', 'Provoni një fjalë tjetër, p.sh. “fatura” ose “ditari”.'));
                }
            }

            function render(keepScroll) {
                var top = body.parentNode.scrollTop;
                var q = search.value.trim();
                if (q) { current = '__search'; } else if (current === '__search') { current = lastModule; }
                var navItems = document.querySelectorAll('.home-nav-item');
                for (var i = 0; i < navItems.length; i++) navItems[i].classList.toggle('active', navItems[i].getAttribute('data-key') === current);
                if (current === '__search') renderSearch(q);
                else if (current === '__fav' || !modules[current]) { current = '__fav'; navFav.classList.add('active'); renderFavourites(); }
                else renderModule(modules[current]);
                body.parentNode.scrollTop = keepScroll ? top : 0;
            }

            var lastModule = '__fav';
            function select(k) {
                current = k;
                lastModule = k;
                search.value = '';
                headFilter.value = '';
                try { history.replaceState(null, '', k === '__fav' ? location.pathname + location.search : '#' + k); } catch (err) { }
                render();
            }

            function updateFavCount() {
                navFavCount.textContent = favs.length ? String(favs.length) : '';
            }

            // ---- left: the modules ----
            var list = order.map(function (k) { return modules[k]; }).filter(function (m) { return m.entries.length; });
            list.sort(function (a, b) { return byText(a.text, b.text); });
            list.forEach(function (m) {
                var meta = MODULES[m.key] || { icon: 'fa-folder-o', desc: '' };
                var b = el('button', 'home-nav-item');
                b.type = 'button';
                b.setAttribute('data-key', m.key);
                var ic = el('span', 'home-nav-icon'); ic.appendChild(el('i', 'fa ' + (meta.icon || 'fa-folder-o'))); b.appendChild(ic);
                var tx = el('span', 'home-nav-text');
                tx.appendChild(el('span', 'home-nav-title', m.text));
                tx.appendChild(el('span', 'home-nav-desc', meta.desc || (m.entries.length + ' faqe')));
                b.appendChild(tx);
                b.appendChild(el('i', 'fa fa-angle-right home-nav-arrow'));
                b.addEventListener('click', function () { select(m.key); });
                navList.appendChild(b);
            });
            navFav.addEventListener('click', function () { select('__fav'); });
            empty.hidden = list.length > 0;
            document.getElementById('home').hidden = list.length === 0;

            var searchTimer;
            search.addEventListener('input', function () { clearTimeout(searchTimer); searchTimer = setTimeout(function () { render(); }, 120); });
            search.addEventListener('keydown', function (ev) { if (ev.key === 'Escape') { search.value = ''; render(); } });
            headFilter.addEventListener('input', function () { render(true); });
            headFilter.addEventListener('keydown', function (ev) { if (ev.key === 'Escape') { headFilter.value = ''; render(true); } });
            document.addEventListener('keydown', function (ev) {
                var t = ev.target, typing = t && (t.tagName === 'INPUT' || t.tagName === 'TEXTAREA');
                if (!typing && (ev.key === '/' || ((ev.ctrlKey || ev.metaKey) && ev.key.toLowerCase() === 'k'))) { ev.preventDefault(); search.focus(); search.select(); }
            });

            // first view: the favourites (or the module/report section the user came back to, e.g. from a report)
            var hash = decodeURIComponent((location.hash || '').slice(1));
            var back = /^raporte-(\d+)$/.exec(hash);
            if (back) {
                var mk = moduleOfReport(+back[1]);
                if (modules[mk]) { current = lastModule = mk; }
            } else if (hash && modules[hash]) {
                current = lastModule = hash;
            }
            render();
            api('MerrFavoritet').then(function (txt) {
                setFavs(JSON.parse(txt || '[]') || []);
                updateFavCount();
                render(true);
                if (back) { var s = document.getElementById(hash); if (s) s.scrollIntoView(); }
            }).catch(function () { toast('Të preferuarat nuk u ngarkuan.'); });
        })();
    </script>
</body>
</html>
