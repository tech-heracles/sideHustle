/*
 * AVEC theme helpers for the inner pages. FaqeKryesore.aspx adds this script to every page it opens
 * in its main frame, so the pages themselves need no change.
 *
 * - Grid filter row: hidden by default and shown with a funnel button next to the grid tools.
 *   The choice is remembered per browser (localStorage "avec.filtersOn").
 * - Empty grids: the English "No data to display" becomes "Nuk ka të dhëna".
 * - Record forms (Artikuj, Klientet/Furnitoret...): the record tabs are shown on one page (BASHKO below).
 */
(function () {
    'use strict';
    if (window.__avecInner) return;
    window.__avecInner = true;

    var KEY = 'avec.filtersOn';
    var body = document.body;

    function filtersOn() {
        try { return localStorage.getItem(KEY) === '1'; } catch (e) { return false; }
    }

    function apply(on) {
        body.classList.toggle('avec-filters-off', !on);
        var buttons = document.querySelectorAll('.avec-filter-toggle');
        for (var i = 0; i < buttons.length; i++) {
            buttons[i].setAttribute('aria-pressed', on ? 'true' : 'false');
            buttons[i].title = on ? 'Fshih filtrat e kolonave' : 'Shfaq filtrat e kolonave';
        }
    }

    function makeButton() {
        var b = document.createElement('button');
        b.type = 'button';
        b.className = 'avec-filter-toggle';
        b.setAttribute('aria-label', 'Filtrat e kolonave');
        b.innerHTML = '<svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" ' +
            'stroke-linecap="round" stroke-linejoin="round" aria-hidden="true"><path d="M4 5h16l-6 7.5V19l-4 1.5v-8z"></path></svg>';
        b.addEventListener('click', function (e) {
            e.preventDefault();
            var on = body.classList.contains('avec-filters-off');
            try { localStorage.setItem(KEY, on ? '1' : '0'); } catch (err) { /* private mode */ }
            apply(on);
        });
        return b;
    }

    // one button per grid that has a filter row, in its title panel (grid tools) or else its search panel
    function ensureButtons() {
        var grids = document.querySelectorAll('table.dxgvControl_Material');
        for (var i = 0; i < grids.length; i++) {
            var g = grids[i];
            if (!g.querySelector('.dxgvFilterRow_Material')) continue;
            var host = g.querySelector('.dxgvTitlePanel_Material') || g.querySelector('.dxgvSearchPanel_Material');
            if (!host || host.querySelector('.avec-filter-toggle')) continue;
            var btns = host.querySelectorAll('div.dxbButton_Material');
            var last = btns.length ? btns[btns.length - 1] : null;
            if (last) last.parentNode.insertBefore(makeButton(), last.nextSibling);
            else host.appendChild(makeButton());
        }
        apply(!body.classList.contains('avec-filters-off'));
    }

    // empty grids say it in Albanian, like the rest of the program ("No data to display" -> "Nuk ka të dhëna")
    var EN = /^\s*No data( to display)?\s*$/;
    function translateEmpty() {
        var cells = document.querySelectorAll('.dxgvEmptyDataRow_Material td, .dx-datagrid-nodata, .dx-empty-message, .dxpgEmptyDataRow');
        for (var i = 0; i < cells.length; i++) {
            var walker = document.createTreeWalker(cells[i], NodeFilter.SHOW_TEXT, null, false), n;
            while ((n = walker.nextNode())) {
                if (EN.test(n.nodeValue)) n.nodeValue = 'Nuk ka të dhëna';
            }
        }
    }

    // ---- record forms on one page (Alkid, 2026-11-27): the record tabs (Informacion, Inventari, Kontabiliteti...)
    // of the pages below are shown one under the other, each with its own title, as soon as a record is open.
    // Only the display changes: the tab control, its tabs and the page scripts stay as they were, so saving,
    // validation (myFaqeCelje.valido walks the tabs) and the toolbar work as before. 'all' = every record tab;
    // a list = only those tabs, the others stay separate tabs (they have their own toolbar actions).
    var BASHKO = {
        'shto_artikull.aspx': 'all',
        'shto_klientfurnitor.aspx': 'all',
        'shto_llogari.aspx': 'all',
        'shto_aktivitete.aspx': 'all',
        'shto_nrautom.aspx': 'all',
        'shto_njesiadministrative.aspx': 'all',
        'shto_transportues.aspx': 'all',
        'shto_qendrakosto.aspx': 'all',
        'shto_perdorues.aspx': 'all',
        'shto_autorizimet.aspx': 'all',
        'shto_banka.aspx': 'all',
        'shto_kartaklienti.aspx': 'all',
        'shto_ndermarrje.aspx': [1, 2, 3],   // Logo Ndermarrje (4) has its own "Hiq logo" action
        'shto_punonjes.aspx': [1, 2]         // Punesimet, Pagat... have their own Shto/Ruaj/Fshi
    };

    function faqja() {
        var p = (location.pathname || '').split('/');
        return (p[p.length - 1] || '').toLowerCase();
    }

    function bashkoForma() {
        var cfg = BASHKO[faqja()];
        var C = window.ASPxClientControl, PC = window.ASPxClientPageControl;
        if (!cfg || !C || !PC || !C.GetControlCollection) return;

        // the page's own tab control: at the top, not inside another tab control or a popup
        var pc = null, kontrollet = [], koleksioni = C.GetControlCollection();
        if (koleksioni.ForEachControl) koleksioni.ForEachControl(function (c) { kontrollet.push(c); });
        else for (var k in (koleksioni.elements || {})) kontrollet.push(koleksioni.elements[k]);
        for (var q = 0; q < kontrollet.length && !pc; q++) {
            var kp = kontrollet[q];
            if (!kp || !(kp instanceof PC) || !kp.GetMainElement) continue;
            var me = kp.GetMainElement();
            if (!me || (me.parentElement && me.parentElement.closest('.dxtcLite_Material, .dxpc-content'))) continue;
            pc = kp;
        }
        if (!pc || pc.GetTabCount() < 3) return;

        var n = pc.GetTabCount(), grupi = [];
        for (var i = 1; i < n; i++) {
            if (cfg === 'all' || cfg.indexOf(i) >= 0) grupi.push(i);
        }
        if (grupi.length < 2) return;
        var main = pc.GetMainElement();

        function el(id) { return document.getElementById(pc.name + id); }
        function permbajtja(i) { return el('_C' + i); }
        function shihet(i) {
            var t = pc.GetTab(i);
            return !!t && t.GetVisible() && t.clientVisible !== false;
        }
        function vendosDisplay(e, v) { if (e && e.style.display !== v) e.style.display = v; }
        function klase(e, cls, on) { if (e && e.classList.contains(cls) !== on) e.classList.toggle(cls, on); }

        var aktivMeParë = -1, uBashkua = false;

        function zbato() {
            var aktiv = pc.GetActiveTabIndex();
            var bashkuar = grupi.indexOf(aktiv) >= 0;
            var pare = -1;   // the group's first visible tab: its tab stays in the strip for the whole group
            for (var g = 0; g < grupi.length; g++) if (shihet(grupi[g])) { pare = grupi[g]; break; }

            klase(main, 'avec-merged', bashkuar);
            for (var j = 0; j < grupi.length; j++) {
                var i = grupi[j], c = permbajtja(i);
                if (!c) continue;

                // the section title, taken from the tab's own text
                var titull = c.firstElementChild && c.firstElementChild.classList.contains('avec-merge-title') ? c.firstElementChild : null;
                if (!titull) {
                    titull = document.createElement('div');
                    titull.className = 'avec-merge-title';
                    c.insertBefore(titull, c.firstChild);
                }
                var tekst = pc.GetTab(i).GetText();
                if (titull.textContent !== tekst) titull.textContent = tekst;

                var trego = bashkuar && shihet(i);
                klase(c, 'avec-merge-section', trego);
                if (trego) {
                    vendosDisplay(c, '');
                    c.setAttribute('data-avec-merge', '1');
                } else if (c.getAttribute('data-avec-merge')) {
                    c.removeAttribute('data-avec-merge');
                    if (i !== aktiv) vendosDisplay(c, 'none');
                }

                // strip: while open the group's tabs give way to the record header (title + section pills);
                // in the list view the group's first tab stays
                var mbaj = bashkuar ? false : i === pare;
                klase(el('_T' + i), 'avec-merge-hide', !mbaj);
                klase(el('_AT' + i), 'avec-merge-hide', !mbaj);
            }

            klase(body, 'avec-rec-open', bashkuar);
            if (bashkuar) koka.rifresko();
            if (bashkuar && !uBashkua) {
                // grids that were laid out while hidden get their real width
                try { if (C.AdjustControls) C.AdjustControls(main); } catch (e) { }
            }
            if (bashkuar) paneli.kerko(uBashkua ? 60 : 0);
            if (bashkuar && aktiv !== aktivMeParë && aktiv !== pare) {
                // a script moved to one of the record tabs (e.g. a field to correct): bring that section into view
                // (scroll-margin-top keeps it below the sticky header)
                var s = permbajtja(aktiv);
                if (s && s.scrollIntoView) s.scrollIntoView({ block: 'start' });
            }
            uBashkua = bashkuar;
            aktivMeParë = aktiv;
        }

        function seksionet() {
            var out = [];
            for (var j = 0; j < grupi.length; j++) {
                var c = permbajtja(grupi[j]);
                if (c && c.classList.contains('avec-merge-section')) out.push({ i: grupi[j], el: c });
            }
            return out;
        }

        // ---- 2B "kartela kompakte" (Alkid, 2026-11-27): sticky record header (the tab row, with the record's
        // title next to "Te pergjithshme" and the page toolbar on the right) and a row of section pills under it;
        // the sections are panels, two per row on wide screens.
        var strip = main.querySelector('ul.dxtc-strip');
        var permbajtjaEl = main.querySelector('.dxtc-content');

        var koka = (function () {
            var pills = document.createElement('nav');
            pills.className = 'avec-rec-pills';
            pills.setAttribute('aria-label', 'Shko te seksioni');
            if (permbajtjaEl) permbajtjaEl.insertBefore(pills, permbajtjaEl.firstChild);
            var listaPills = null, aktivPill = -1;

            // record title: the first two filled text fields of the first section (code · description)
            function titulli() {
                var secs = seksionet();
                if (!secs.length) return '';
                var fushat = secs[0].el.querySelectorAll('input[type=text], textarea'), vlerat = [];
                for (var f = 0; f < fushat.length && vlerat.length < 2; f++) {
                    var e = fushat[f];
                    if (!e.offsetParent || e.closest('table.dxeButtonEdit_Material, .dxgvControl_Material')) continue;
                    var v = (e.value || '').replace(/\s+/g, ' ').trim();
                    if (v) vlerat.push(v.length > 70 ? v.slice(0, 70) + '…' : v);
                }
                return vlerat.length ? vlerat.join(' · ') : 'Regjistrim i ri';
            }

            function lartesia() {
                return (strip ? strip.offsetHeight : 0) + pills.offsetHeight;
            }

            function rifresko() {
                var secs = seksionet(), ids = secs.map(function (x) { return x.i; }).join(',');
                if (ids !== listaPills) {
                    listaPills = ids;
                    pills.innerHTML = '';
                    secs.forEach(function (x) {
                        var a = document.createElement('a');
                        a.href = '#';
                        a.className = 'avec-rec-pill';
                        a.textContent = pc.GetTab(x.i).GetText();
                        a.setAttribute('data-avec-sec', x.i);
                        a.addEventListener('click', function (e) {
                            e.preventDefault();
                            zgjedhur = x.i;
                            aktivPill = -1;
                            var c = permbajtja(x.i);
                            if (c) window.scrollTo({ top: c.getBoundingClientRect().top + window.pageYOffset - lartesia() - 8, behavior: 'smooth' });
                        });
                        pills.appendChild(a);
                    });
                    aktivPill = -1;
                }
                if (strip) {
                    var t = titulli();
                    if (strip.getAttribute('data-avec-title') !== t) strip.setAttribute('data-avec-title', t);
                    if (pills.style.top !== strip.offsetHeight + 'px') pills.style.top = strip.offsetHeight + 'px';
                }
                var h = lartesia() + 'px';
                if (body.style.getPropertyValue('--avec-rec-head') !== h) body.style.setProperty('--avec-rec-head', h);
                ndiqScroll();
            }

            // the pill of the section under the header is marked
            function ndiqScroll() {
                if (!body.classList.contains('avec-rec-open')) return;
                var secs = seksionet(), kufi = lartesia() + 24, kjo = secs.length ? secs[0].i : -1;
                for (var k = 0; k < secs.length; k++) if (secs[k].el.getBoundingClientRect().top <= kufi) kjo = secs[k].i;
                if (zgjedhur >= 0) kjo = zgjedhur;   // the pill just clicked, until the user scrolls
                else if (secs.length && window.pageYOffset > 0 &&
                    window.innerHeight + window.pageYOffset >= document.documentElement.scrollHeight - 2) kjo = secs[secs.length - 1].i;
                if (kjo === aktivPill) return;
                aktivPill = kjo;
                var as = pills.querySelectorAll('.avec-rec-pill');
                for (var a = 0; a < as.length; a++) as[a].classList.toggle('avec-on', +as[a].getAttribute('data-avec-sec') === kjo);
            }

            var zgjedhur = -1;
            ['wheel', 'touchmove', 'keydown', 'mousedown'].forEach(function (ev) {
                window.addEventListener(ev, function (e) {
                    if (zgjedhur < 0 || (e.target && e.target.closest && e.target.closest('.avec-rec-pill'))) return;
                    zgjedhur = -1;
                    ndiqScroll();
                }, { passive: true });
            });
            window.addEventListener('scroll', ndiqScroll, { passive: true });
            // the title follows the record (its fields are filled by callbacks after it opens)
            setInterval(function () { if (body.classList.contains('avec-rec-open')) rifresko(); }, 1000);
            return { rifresko: rifresko };
        })();

        // panels: two per row; a section whose content does not fit in half the width takes the whole row
        // (measured, never clipped)
        var paneli = (function () {
            var afat = null, neMatje = false;

            function igjere(sec) {
                if (sec.scrollWidth > sec.clientWidth + 1) return true;
                var gridat = sec.querySelectorAll('table.dxgvControl_Material');
                for (var g = 0; g < gridat.length; g++) {
                    var gr = gridat[g];
                    if (!gr.offsetParent) continue;
                    if (gr.scrollWidth > gr.clientWidth + 1) return true;
                    // grid columns squeezed below a readable width
                    var kr = gr.querySelector('tr[id*="DXHeadersRow"]');
                    if (kr && kr.children.length * 80 > gr.clientWidth) return true;
                    var sc = gr.querySelector('.dxgvCSD');
                    if (sc && sc.scrollWidth > sc.clientWidth + 1) return true;
                }
                // editors squeezed too much (labels sit beside the fields and do not wrap)
                var ed = sec.querySelectorAll('table.dxeTextBox_Material, table.dxeButtonEdit_Material');
                for (var e = 0; e < ed.length; e++) {
                    if (!ed[e].offsetParent || ed[e].closest('.dxgvControl_Material')) continue;
                    if (ed[e].getBoundingClientRect().width < 90) return true;
                }
                return false;
            }

            function mat() {
                afat = null;
                if (!body.classList.contains('avec-rec-open') || !permbajtjaEl) return;
                neMatje = true;
                var secs = seksionet().map(function (x) { return x.el; });
                // empty lines (<br>) left at the end of a section's markup
                secs.forEach(function (sec) {
                    var p = sec.lastElementChild;
                    for (var e = p && p.lastElementChild; e; e = e.previousElementSibling) {
                        if (e.tagName === 'BR') e.classList.add('avec-br-fund');
                        else if (e.tagName !== 'SCRIPT' && getComputedStyle(e).display !== 'none') break;
                    }
                });
                var para =secs.map(function (sec) { return sec.classList.contains('avec-sec-full'); });
                secs.forEach(function (sec) { sec.classList.remove('avec-sec-full'); });
                var kolonat = (getComputedStyle(permbajtjaEl).gridTemplateColumns || '').split(' ').filter(Boolean).length;
                var gjere = secs.map(function () { return false; });
                if (kolonat >= 2) {
                    gjere = secs.map(igjere);
                    // a half-width panel left alone in its row (the next one takes the whole row) takes it too
                    var radhe = 0;
                    for (var k = 0; k <= secs.length; k++) {
                        if (k === secs.length || gjere[k]) {
                            if (radhe % 2 === 1) gjere[k - 1] = true;
                            radhe = 0;
                        } else radhe++;
                    }
                }
                secs.forEach(function (sec, k) { if (gjere[k]) sec.classList.add('avec-sec-full'); });
                if (gjere.join() !== para.join()) {
                    try { if (C.AdjustControls) C.AdjustControls(main); } catch (e) { }
                }
                // jqGrid tables (e.g. Inventari's stock per warehouse) keep the pixel width they got at load
                var jq = window.jQuery;
                if (jq && jq.fn && jq.fn.jqGrid) secs.forEach(function (sec) {
                    var boxes = sec.querySelectorAll('.ui-jqgrid[id^="gbox_"]');
                    for (var b = 0; b < boxes.length; b++) {
                        var box = boxes[b], prind = box.parentElement;
                        if (!box.offsetParent || !prind) continue;
                        var cs = getComputedStyle(prind);
                        var w = Math.floor(prind.clientWidth - parseFloat(cs.paddingLeft) - parseFloat(cs.paddingRight));
                        if (w > 200 && Math.abs(box.offsetWidth - w) > 2) {
                            try { jq('#' + box.id.slice(5)).jqGrid('setGridWidth', w); } catch (e) { }
                        }
                    }
                });
                koka.rifresko();
                setTimeout(function () { neMatje = false; }, 300);
            }

            function kerko(vonese) {
                if (afat) clearTimeout(afat);
                afat = setTimeout(mat, vonese || 0);
            }

            window.addEventListener('resize', function () { kerko(150); });
            // fields arrive by callback after the record opens: measure again when a section changes size
            if (window.ResizeObserver) {
                var ro = new ResizeObserver(function () { if (!neMatje) kerko(250); });
                for (var j = 0; j < grupi.length; j++) { var c = permbajtja(grupi[j]); if (c) ro.observe(c); }
            }
            return { kerko: kerko };
        })();

        var radhe = false;
        function rizbato() {
            if (radhe) return;
            radhe = true;
            (window.requestAnimationFrame || setTimeout)(function () { radhe = false; zbato(); });
        }

        zbato();
        if (pc.ActiveTabChanged && pc.ActiveTabChanged.AddHandler) pc.ActiveTabChanged.AddHandler(rizbato);
        // scripts change tabs (SetActiveTab, SetVisible) without raising ActiveTabChanged: the tab control
        // shows / hides its tabs and pages through their style, so watch that
        if (window.MutationObserver) {
            var obs = new MutationObserver(rizbato);
            for (var t = 0; t < n; t++) {
                [el('_C' + t), el('_T' + t), el('_AT' + t)].forEach(function (e) {
                    if (e) obs.observe(e, { attributes: true, attributeFilter: ['style'] });
                });
            }
        }
    }

    // ---- 3B "shiriti i shpejte" for the sales document (Shto_RegjistrimDokumentash.aspx?shitje_blerje=shitje;
    // Alkid, 2026-11-27). Only the display changes, no node is moved (the page's scripts address the cells as
    // #tbl>tbody>tr:eq(r)>td:eq(k)):
    // - header fields (tblFillim): every field that shows today, as compact label + field pairs in a wrapping grid
    // - the line being edited in the article grid is the prominent entry line (orange, 52px)
    // - Fund Dokumenti (tblFund: totals and the payment fields) is a bar that stays at the bottom of the pane,
    //   the totals as columns (Total large in orange), and the page toolbar (Ruaj...) on its right
    function shitje3B() {
        if (faqja() !== 'shto_regjistrimdokumentash.aspx' || !/shitje_blerje=shitje/i.test(location.search)) return;
        var koke = document.getElementById('tblFillim'), fund = document.getElementById('tblFund');
        var h3Fund = document.getElementById('fundKonfigurimi');
        var butonat = document.getElementById('dvMenuButona');
        var pane = document.getElementById('ASPxSplitter1_0');
        if (!koke || !fund) return;
        body.classList.add('avec-3b');
        var seksioniFund = h3Fund ? h3Fund.parentElement : null;
        if (seksioniFund) seksioniFund.classList.add('avec-3b-fund');
        if (butonat) butonat.classList.add('avec-3b-butonat');

        function ka(td) {
            // something of the cell shows (measured while the table has its own layout)
            if (getComputedStyle(td).display === 'none') return false;
            var r = td.getBoundingClientRect();
            if (r.width === 0 && r.height === 0) return false;
            var tw = document.createTreeWalker(td, NodeFilter.SHOW_ELEMENT | NodeFilter.SHOW_TEXT, null, false), n;
            while ((n = tw.nextNode())) {
                if (n.nodeType === 3) {
                    if (!n.nodeValue.trim()) continue;
                    var p = n.parentElement;
                    if (!p || p.tagName === 'SCRIPT' || p.tagName === 'STYLE') continue;
                    var pr = p.getBoundingClientRect();
                    if (pr.width > 0 && pr.height > 0 && getComputedStyle(p).visibility !== 'hidden') return true;
                    continue;
                }
                if (/^(INPUT|TEXTAREA|SELECT|IMG|BUTTON|CANVAS)$/.test(n.tagName) || /CheckBox|dxich|dxeIRadio/.test(n.className || '')) {
                    if (n.type === 'hidden') continue;
                    var er = n.getBoundingClientRect();
                    if (er.width > 0 && er.height > 0 && getComputedStyle(n).visibility !== 'hidden') return true;
                }
            }
            return false;
        }

        // label + fields groups of a renditKontrolle table, in reading order
        function grupet(tbl) {
            var out = [], rows = tbl.tBodies[0] ? tbl.tBodies[0].rows : [];
            for (var r = 0; r < rows.length; r++) {
                var g = null;
                for (var c = 0; c < rows[r].cells.length; c++) {
                    var td = rows[r].cells[c];
                    if (td.classList.contains('renditKontrolleCaption') || !g) { g = { cap: null, fields: [] }; out.push(g); }
                    if (td.classList.contains('renditKontrolleCaption') && !g.cap && !g.fields.length) g.cap = td;
                    else g.fields.push(td);
                }
            }
            return out;
        }

        function pastro(tbl) {
            var tds = tbl.querySelectorAll('td');
            for (var i = 0; i < tds.length; i++) {
                var td = tds[i];
                td.classList.remove('avec-on', 'avec-s2', 'avec-lbl', 'avec-total', 'avec-total-lbl', 'avec-val2');
                td.style.gridRow = '';
                td.style.gridColumn = '';
            }
        }

        // header: pairs (label | field); a field without its label, or a label alone, takes both tracks
        function rregulloKoke() {
            koke.classList.add('avec-mat');
            var gr = grupet(koke).map(function (g) {
                return { cap: g.cap && ka(g.cap) ? g.cap : null, fields: g.fields.filter(ka) };
            });
            pastro(koke);
            koke.classList.remove('avec-mat');
            gr.forEach(function (g) {
                if (!g.cap && !g.fields.length) return;
                if (g.cap) g.cap.classList.add('avec-on', 'avec-lbl');
                g.fields.forEach(function (f, k) {
                    f.classList.add('avec-on');
                    if (!g.cap || k > 0) f.classList.add('avec-s2');
                });
                if (g.cap && !g.fields.length) g.cap.classList.add('avec-s2');
            });
            var w = koke.parentElement ? koke.parentElement.clientWidth : 0;
            var p = Math.max(1, Math.min(5, Math.floor((w + 14) / 300)));
            koke.style.setProperty('--avec-p', p);
        }

        // footer: each group is a column, label above its value(s); groups with two values (totals in the
        // document's and the base currency) come last, after a column with the currency labels
        function rregulloFund() {
            fund.classList.add('avec-mat');
            var gr = grupet(fund).map(function (g) {
                return { cap: g.cap && ka(g.cap) ? g.cap : null, fields: g.fields.filter(ka), n: g.fields.filter(function (f) { return f.classList.contains('renditKontrolleCell'); }).length };
            }).filter(function (g) { return g.cap || g.fields.length; });
            pastro(fund);
            fund.classList.remove('avec-mat');
            // totals: the groups laid out with a value per currency (even when the base currency one is hidden)
            var shuma = gr.filter(function (g) { return g.n >= 2; });
            var koka = shuma.length ? gr.filter(function (g) { return g.cap && !g.n; }).slice(0, 2) : [];
            var tjera = gr.filter(function (g) { return shuma.indexOf(g) < 0 && koka.indexOf(g) < 0; });
            var vend = fund.parentElement ? fund.parentElement.clientWidth - 24 : 800;
            var kol = Math.max(3, Math.floor((vend + 14) / 134));
            fund.style.setProperty('--avec-k', kol);

            function vendos(td, rr, cc, extra) {
                td.classList.add('avec-on');
                if (extra) td.classList.add(extra);
                td.style.gridRow = String(rr);
                td.style.gridColumn = String(cc);
            }
            var rresht = 1;
            if (tjera.length) {
                var maxF = Math.max(1, Math.max.apply(null, tjera.map(function (g) { return g.fields.length; })));
                for (var i = 0; i < tjera.length; i++) {
                    var b = Math.floor(i / kol), c = i % kol + 1, g = tjera[i], r1 = rresht + b * (1 + maxF);
                    if (g.cap) vendos(g.cap, r1, c, 'avec-lbl');
                    g.fields.forEach(function (f, k) { vendos(f, r1 + 1 + k, c); });
                }
                rresht += Math.ceil(tjera.length / kol) * (1 + maxF);
            }
            if (shuma.length) {
                var maxS = Math.max(1, Math.max.apply(null, shuma.map(function (g) { return g.fields.length; })));
                maxS = Math.max(maxS, koka.length);   // a currency label stays even when its values are hidden
                var nis = koka.length ? 2 : 1, ne = Math.max(1, kol - nis + 1);
                koka.forEach(function (h, k) { vendos(h.cap, rresht + 1 + k, 1, 'avec-lbl'); });
                for (var j = 0; j < shuma.length; j++) {
                    var bb = Math.floor(j / ne), cc = j % ne + nis, r0 = rresht + (1 + maxS) * bb, s = shuma[j];
                    var eshteTotal = s.cap && /^\s*total\s*:?\s*$/i.test(s.cap.textContent);
                    if (s.cap) vendos(s.cap, r0, cc, eshteTotal ? 'avec-total-lbl' : 'avec-lbl');
                    s.fields.forEach(function (f, k) { vendos(f, r0 + 1 + k, cc, k === 0 ? (eshteTotal ? 'avec-total' : null) : 'avec-val2'); });
                }
            }
        }

        function nenshkrim() {
            var s = '', tds = document.querySelectorAll('#tblFillim td, #tblFund td');
            for (var i = 0; i < tds.length; i++) s += tds[i].className + tds[i].style.gridRow + '/' + tds[i].style.gridColumn + ';';
            return s + koke.style.getPropertyValue('--avec-p') + fund.style.getPropertyValue('--avec-k');
        }

        // the toolbar sits in the bottom bar's title row, on the right (the bar follows the pane's scrolling
        // until it reaches the bottom, so this runs on scroll too)
        var h3 = h3Fund;
        function vendosButonat() {
            if (!butonat || !seksioniFund || !h3) return;
            var fr = seksioniFund.getBoundingClientRect();
            var vw = document.documentElement.clientWidth;
            var titulli = h3.firstElementChild ? h3.firstElementChild.getBoundingClientRect().right - fr.left + 24 : 220;
            var v = {
                '--avec-3b-top': Math.round(fr.top + 4) + 'px',
                '--avec-3b-right': Math.round(Math.max(0, vw - fr.right) + 10) + 'px',
                '--avec-3b-maxw': Math.round(Math.max(200, fr.width - titulli - 10)) + 'px'
            };
            for (var k in v) if (butonat.style.getPropertyValue(k) !== v[k]) butonat.style.setProperty(k, v[k]);
            // back in its own place at the top while the bar is out of the pane's view (e.g. the pane collapsed)
            var shihet = fr.height > 0 && pane && fr.top < pane.getBoundingClientRect().bottom - 10 && fr.top > (pane.getBoundingClientRect().top - 2);
            butonat.classList.toggle('avec-3b-jashte', !shihet);
            var hb = butonat.offsetHeight + 8 + 'px';
            if (seksioniFund.style.getPropertyValue('--avec-3b-bh') !== hb) seksioniFund.style.setProperty('--avec-3b-bh', hb);
        }

        // the line being edited: the code field is the entry ("Kodi / barkodi")
        function rreshtiHyrjes() {
            var k = document.querySelectorAll('#rowed5 tr.jqgrow[editable="1"] input[id^="txtKodi"]');
            for (var i = 0; i < k.length; i++) if (!k[i].placeholder) k[i].placeholder = 'Kodi / barkodi…';
        }

        // the splitter (Height=100%) sat under the toolbar row and ran past the bottom of the frame, so the bottom
        // bar would be cut: with the toolbar in the bar it is fitted to the frame
        function pershtatSplitter() {
            var sp = window.splitter;
            if (!sp || !sp.GetMainElement || !sp.SetHeight) return;
            var el = sp.GetMainElement(), top = el.getBoundingClientRect().top + (window.pageYOffset || 0);
            var h = Math.max(400, Math.floor(window.innerHeight - top - 4));
            if (Math.abs(el.offsetHeight - h) > 1) sp.SetHeight(h);
        }

        var radha = null, neZbatim = false, iFundit = null;
        function zbato() {
            radha = null;
            neZbatim = true;
            // on any error the table keeps its own layout (.avec-mat turns the 3B rules off), so no field is lost
            try { rregulloKoke(); } catch (e) { koke.classList.add('avec-mat'); }
            try { rregulloFund(); } catch (e) { fund.classList.add('avec-mat'); }
            try { if (butonat && !butonat.classList.contains('avec-3b-jashte')) pershtatSplitter(); } catch (e) { }
            try { vendosButonat(); } catch (e) { }
            try { rreshtiHyrjes(); } catch (e) { }
            var n = nenshkrim();
            if (n !== iFundit) {
                // editors laid out for the old widths get their new size (only when the layout changed)
                iFundit = n;
                try { if (window.ASPxClientControl && ASPxClientControl.AdjustControls) ASPxClientControl.AdjustControls(document.body); } catch (e) { }
            }
            setTimeout(function () { neZbatim = false; }, 50);
        }
        function kerko(v) {
            if (radha) clearTimeout(radha);
            radha = setTimeout(zbato, v || 0);
        }

        zbato();
        // the fields are placed by callbacks after the page loads, and scripts show / hide them later
        var obs = new MutationObserver(function (list) {
            if (neZbatim) return;
            for (var i = 0; i < list.length; i++) {
                var t = list[i].target;
                if (list[i].type === 'attributes' && (t === koke || t === fund)) continue;   // our own column counts
                kerko(120);
                return;
            }
        });
        [koke, fund].forEach(function (t) { obs.observe(t, { childList: true, subtree: true, attributes: true, attributeFilter: ['style', 'class'] }); });
        var rr = document.getElementById('rowed5');
        if (rr) new MutationObserver(function () { rreshtiHyrjes(); }).observe(rr, { attributes: true, subtree: true, attributeFilter: ['editable'] });
        window.addEventListener('resize', function () { kerko(150); vendosButonat(); });
        var scroller = document.getElementById('ASPxSplitter1_0_CC');
        if (scroller) scroller.addEventListener('scroll', vendosButonat, { passive: true });
        if (window.ResizeObserver) {
            var ro = new ResizeObserver(function () { vendosButonat(); if (!neZbatim) kerko(150); });
            [pane, butonat, seksioniFund].forEach(function (e) { if (e) ro.observe(e); });
        }
        setTimeout(vendosButonat, 600);
        setTimeout(vendosButonat, 2000);
    }

    body.classList.toggle('avec-filters-off', !filtersOn());
    ensureButtons();
    translateEmpty();
    try { bashkoForma(); } catch (e) { /* the page keeps its tabs */ }
    try { shitje3B(); } catch (e) { /* the page keeps its layout */ }

    // grids re-render after callbacks and partial postbacks
    var pending = false;
    new MutationObserver(function () {
        if (pending) return;
        pending = true;
        setTimeout(function () { pending = false; ensureButtons(); translateEmpty(); }, 150);
    }).observe(body, { childList: true, subtree: true });
})();
