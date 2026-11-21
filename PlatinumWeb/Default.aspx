<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PlatinumWeb._Default" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>AVEC Accounting</title>
    <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <link href="css/font-awesome-4.3.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="css/avec-dashboard.css?v=2" rel="stylesheet" />
</head>
<body class="avec-dashboard">
    <form id="form1" runat="server">
        <header class="dash-header">
            <div>
                <p class="dash-eyebrow">Mirë se erdhe</p>
                <h1 class="dash-title"><asp:Label ID="lblEmerNderm" runat="server" /></h1>
            </div>
            <dl class="dash-facts">
                <div><dt>NIPT</dt><dd><asp:Label ID="lblEmerNIpti" runat="server" /></dd></div>
                <div><dt>Viti</dt><dd><asp:Label ID="lblEmerViti" runat="server" /></dd></div>
                <div><dt>Qyteti</dt><dd><asp:Label ID="lblEmerQyteti" runat="server" /></dd></div>
            </dl>
        </header>

        <nav class="dash-crumbs" id="dashCrumbs" hidden>
            <button type="button" class="dash-back" id="dashBack"><i class="fa fa-arrow-left"></i> Kryefaqja</button>
            <span class="dash-crumb-title" id="dashCrumbTitle"></span>
        </nav>

        <main class="dash-grid" id="dashGrid" aria-live="polite"></main>
        <p class="dash-empty" id="dashEmpty" hidden>Nuk keni akses në asnjë modul. Kontaktoni administratorin.</p>
    </form>

    <script>
        (function () {
            // The home screen is the only menu: its cards are built from the main frame's navbar and top menu
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
                'aprovime-dokumentash': { icon: 'fa-check-square-o', desc: 'Aprovimi i dokumenteve' },
                'rap': { icon: 'fa-file-text-o', desc: 'Të gjitha raportet' },
                'bi': { icon: 'fa-bar-chart', desc: 'Analiza e biznesit' },
                'map': { icon: 'fa-map-marker', desc: 'Hartat e shitjeve' },
                'crm': { icon: 'fa-comments', desc: 'Klientët, anketat dhe detyrat' },
                'gis': { icon: 'fa-globe', desc: 'Sistemi gjeografik' },
                'analizeBuxheti': { icon: 'fa-line-chart', desc: 'Analiza e buxhetit' },
                'buxheti': { icon: 'fa-pie-chart', desc: 'Planifikimi i buxhetit' },
                'dashboard': { icon: 'fa-tachometer', desc: 'Treguesit kryesorë' },
                'help': { icon: 'fa-question-circle', desc: 'Manuali dhe ndihma' },
                'settings': { icon: 'fa-cog', desc: 'Cilësimet' },
                // modules that exist only in the top menu
                'konfigurime': { icon: 'fa-sliders', desc: 'Dokumente, formate, import/eksport' },
                'sherbime': { icon: 'fa-wrench', desc: 'Riparime dhe receta optike', text: 'Shërbimet' }
            };

            // Which card each branch of the top menu belongs to, by menu path (item names joined with "/").
            // The longest matching prefix wins; an item that is also in the navbar stays in the navbar's card.
            var MENU_TO_MODULE = {
                'administrimi': 'Administrimi',
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
                'konfigurime/konfig-gis': 'gis',
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
                'regjistrime/aprovime': 'aprovime-dokumentash',
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
                return collator ? collator.compare(a, b) : (a < b ? -1 : a > b ? 1 : 0);
            }

            var host = window.parent && window.parent !== window ? window.parent : null;
            var navbar = host && host.navbar;
            var topMenu = host && host.menu;
            var grid = document.getElementById('dashGrid');
            var crumbs = document.getElementById('dashCrumbs');
            var crumbTitle = document.getElementById('dashCrumbTitle');
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

            function card(icon, title, desc, onClick, small) {
                var c = el('button', 'dash-card' + (small ? ' dash-card-small' : ''));
                c.type = 'button';
                var top = el('div', 'dash-card-top');
                var ic = el('span', 'dash-icon');
                ic.appendChild(el('i', 'fa ' + icon));
                top.appendChild(ic);
                var arrow = el('i', 'fa fa-arrow-right dash-arrow');
                top.appendChild(arrow);
                c.appendChild(top);
                var body = el('div', 'dash-card-body');
                body.appendChild(el('span', 'dash-card-title', title));
                if (desc) body.appendChild(el('span', 'dash-card-desc', desc));
                c.appendChild(body);
                c.addEventListener('click', onClick);
                return c;
            }

            function open(source, item, ev) {
                host.kontrolloTeDrejta(source, { item: item, processOnServer: false, htmlEvent: ev || window.event }, item.name);
            }

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
                    var title = it.GetText(), desc = parentText;
                    if (GENERIC.test(title) && parentText) {
                        title = parentText;
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
                    var entry = { title: title, desc: desc, section: section, source: topMenu, item: it };
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

            // ---- render ----
            function showModule(m) {
                var meta = MODULES[m.key] || { icon: 'fa-folder-o' };
                grid.innerHTML = '';
                grid.classList.add('dash-grid-items');
                crumbs.hidden = false;
                crumbTitle.textContent = m.text;

                var sections = {}, names = [];
                m.entries.forEach(function (e) {
                    var s = e.section || '';
                    if (!sections[s]) { sections[s] = []; names.push(s); }
                    sections[s].push(e);
                });
                names.sort(function (a, b) { return a === '' ? -1 : b === '' ? 1 : byText(a, b); });
                var withHeaders = names.length > 1;
                names.forEach(function (s) {
                    if (withHeaders) grid.appendChild(el('h2', 'dash-section', s || 'Kryesore'));
                    sections[s].sort(function (a, b) { return byText(a.title, b.title) || byText(a.desc, b.desc); });
                    sections[s].forEach(function (e) {
                        grid.appendChild(card(meta.icon, e.title, e.desc, function (ev) { open(e.source, e.item, ev); }, true));
                    });
                });
                window.scrollTo(0, 0);
            }

            function showModules() {
                grid.innerHTML = '';
                grid.classList.remove('dash-grid-items');
                crumbs.hidden = true;
                var list = order.map(function (k) { return modules[k]; }).filter(function (m) { return m.entries.length; });
                list.sort(function (a, b) { return byText(a.text, b.text); });
                list.forEach(function (m) {
                    var meta = MODULES[m.key] || { icon: 'fa-folder-o', desc: '' };
                    var desc = meta.desc || (m.entries.length + ' faqe');
                    grid.appendChild(card(meta.icon, m.text, desc, function (ev) {
                        if (m.entries.length === 1) open(m.entries[0].source, m.entries[0].item, ev); else showModule(m);
                    }));
                });
                empty.hidden = list.length > 0;
            }

            document.getElementById('dashBack').addEventListener('click', showModules);
            showModules();
        })();
    </script>
</body>
</html>
