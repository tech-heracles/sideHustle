<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PlatinumWeb._Default" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>AVEC Accounting</title>
    <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <link href="css/font-awesome-4.3.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="css/avec-dashboard.css?v=3" rel="stylesheet" />
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

            // "+ Shto" on a list card: opens the form for a new document of that list
            function addNewButton(c, onClick) {
                var b = el('span', 'dash-card-new');
                b.setAttribute('role', 'button');
                b.setAttribute('tabindex', '0');
                b.title = 'Shto dokument të ri';
                b.innerHTML = '<i class="fa fa-plus"></i> Shto';
                function go(ev) { ev.stopPropagation(); ev.preventDefault(); onClick(ev); }
                b.addEventListener('click', go);
                b.addEventListener('keydown', function (ev) { if (ev.key === 'Enter' || ev.key === ' ') go(ev); });
                c.classList.add('dash-card-has-new');
                c.appendChild(b);
            }

            // the page header shows the name of the card that opened the page (FaqeKryesore: avecMenuTitleFor)
            function open(source, item, ev, title) {
                host.avecMenuTitle = title ? { url: item.name || '', title: title } : null;
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

            // ---- render ----
            function showModule(m) {
                var meta = MODULES[m.key] || { icon: 'fa-folder-o' };
                grid.innerHTML = '';
                grid.classList.add('dash-grid-items');
                crumbs.hidden = false;
                crumbTitle.textContent = m.text;

                var entries = m.entries.slice();
                (MODULE_REPORTS[m.key] || []).forEach(function (u) {
                    var r = seen[u];
                    if (r && entries.indexOf(r) === -1)
                        entries.push({ title: r.title, header: r.header || r.title, desc: '', section: 'Raporte', source: r.source, item: r.item, report: true });
                });

                // list + new form of the same document = one card with an "E re" button
                var forms = entries.filter(isNewForm), lists = entries.filter(function (e) { return !isNewForm(e); });
                var used = [];
                lists.forEach(function (l) {
                    if (l.report) return;
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
                var withHeaders = names.length > 1;
                names.forEach(function (s) {
                    if (withHeaders) grid.appendChild(el('h2', 'dash-section', s || 'Kryesore'));
                    sections[s].sort(function (a, b) { return byText(a.title, b.title) || byText(a.desc, b.desc); });
                    sections[s].forEach(function (e) {
                        var desc = e.newForm ? (e.desc || '').replace(/\s*·?\s*lista$/i, '') : e.desc;
                        var c = card(e.report ? 'fa-file-text-o' : meta.icon, e.title, desc, function (ev) { open(e.source, e.item, ev, e.header || e.title); }, true);
                        if (e.newForm) {
                            var f = e.newForm;
                            addNewButton(c, function (ev) { open(f.source, f.item, ev, f.header && f.header !== f.title ? f.header : 'Shto ' + e.title.toLowerCase()); });
                        }
                        grid.appendChild(c);
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
                        if (m.entries.length === 1) open(m.entries[0].source, m.entries[0].item, ev, m.entries[0].header || m.entries[0].title); else showModule(m);
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
