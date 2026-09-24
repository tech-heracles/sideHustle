<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PlatinumWeb._Default" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>AVEC Accounting</title>
    <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <link href="css/font-awesome-4.3.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="css/avec-dashboard.css" rel="stylesheet" />
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
            // Module cards are built from the main-frame navbar (FaqeKryesore), so they follow the user's rights.
            var MODULES = {
                'Administrimi': { icon: 'fa-shield', desc: 'Përdorues, role dhe ndërmarrje' },
                'kontabiliteti': { icon: 'fa-book', desc: 'Llogari dhe fletë kontabël' },
                'inventari': { icon: 'fa-cubes', desc: 'Artikuj, magazina, hyrje e dalje' },
                'blerjeShitje': { icon: 'fa-shopping-cart', desc: 'Klientë, furnitorë dhe fatura' },
                'arkaBanka': { icon: 'fa-university', desc: 'Arkëtime, pagesa dhe banka' },
                'hr': { icon: 'fa-users', desc: 'Punonjës dhe departamente' },
                'pagesa': { icon: 'fa-credit-card', desc: 'Urdhërpagesat' },
                'prodhimi': { icon: 'fa-industry', desc: 'Planifikim dhe ekzekutim' },
                'qk': { icon: 'fa-sitemap', desc: 'Qendrat e kostos' },
                'amortizimi': { icon: 'fa-building', desc: 'Aktivet afatgjata' },
                'aprovime-dokumentash': { icon: 'fa-check-square-o', desc: 'Aprovimi i dokumenteve' },
                'rap': { icon: 'fa-file-text-o', desc: 'Të gjitha raportet' },
                'bi': { icon: 'fa-bar-chart', desc: 'Analiza e biznesit' },
                'map': { icon: 'fa-map-o', desc: 'Hartat e shitjeve' },
                'crm': { icon: 'fa-comments', desc: 'Klientët, anketat dhe detyrat' },
                'gis': { icon: 'fa-globe', desc: 'Sistemi gjeografik' },
                'analizeBuxheti': { icon: 'fa-line-chart', desc: 'Analiza e buxhetit' },
                'buxheti': { icon: 'fa-pie-chart', desc: 'Planifikimi i buxhetit' },
                'dashboard': { icon: 'fa-tachometer', desc: 'Treguesit kryesorë' },
                'help': { icon: 'fa-question-circle', desc: 'Manuali dhe ndihma' },
                'Mobile': { icon: 'fa-mobile', desc: 'Aplikacioni celular' },
                'settings': { icon: 'fa-cog', desc: 'Cilësimet' }
            };

            var host = window.parent && window.parent !== window ? window.parent : null;
            var navbar = host && host.navbar;
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

            function visibleItems(group) {
                var list = [];
                for (var j = 0; j < group.GetItemCount(); j++) {
                    var it = group.GetItem(j);
                    if (it.GetVisible() && it.GetText()) list.push(it);
                }
                return list;
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

            function openItem(item) {
                host.kontrolloTeDrejta(navbar, { item: item, processOnServer: false }, item.name);
            }

            function showGroup(group, items) {
                var meta = MODULES[group.name] || { icon: 'fa-folder-o' };
                grid.innerHTML = '';
                grid.classList.add('dash-grid-items');
                crumbs.hidden = false;
                crumbTitle.textContent = group.GetText();
                items.forEach(function (it) {
                    grid.appendChild(card(meta.icon, it.GetText(), '', function () { openItem(it); }, true));
                });
                window.scrollTo(0, 0);
            }

            function showModules() {
                grid.innerHTML = '';
                grid.classList.remove('dash-grid-items');
                crumbs.hidden = true;
                var shown = 0;
                if (!navbar) { empty.hidden = false; return; }
                for (var i = 0; i < navbar.GetGroupCount(); i++) {
                    var g = navbar.GetGroup(i);
                    if (!g.GetVisible()) continue;
                    var items = visibleItems(g);
                    if (!items.length) continue;
                    var meta = MODULES[g.name] || { icon: 'fa-folder-o', desc: '' };
                    var desc = meta.desc || (items.length + ' faqe');
                    (function (group, list) {
                        grid.appendChild(card(meta.icon, group.GetText(), desc, function () {
                            if (list.length === 1) openItem(list[0]); else showGroup(group, list);
                        }));
                    })(g, items);
                    shown++;
                }
                empty.hidden = shown > 0;
            }

            document.getElementById('dashBack').addEventListener('click', showModules);
            showModules();
        })();
    </script>
</body>
</html>
