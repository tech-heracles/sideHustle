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

                // strip: one tab for the whole group (the active one while open, else the first)
                var mbaj = bashkuar ? i === aktiv : i === pare;
                klase(el('_T' + i), 'avec-merge-hide', !mbaj);
                klase(el('_AT' + i), 'avec-merge-hide', !mbaj);
            }

            if (bashkuar && !uBashkua) {
                // grids that were laid out while hidden get their real width
                try { if (C.AdjustControls) C.AdjustControls(main); } catch (e) { }
            }
            if (bashkuar && aktiv !== aktivMeParë && aktiv !== pare) {
                // a script moved to one of the record tabs (e.g. a field to correct): bring that section into view
                var s = permbajtja(aktiv);
                if (s && s.scrollIntoView) s.scrollIntoView({ block: 'start' });
            }
            uBashkua = bashkuar;
            aktivMeParë = aktiv;
        }

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

    body.classList.toggle('avec-filters-off', !filtersOn());
    ensureButtons();
    translateEmpty();
    try { bashkoForma(); } catch (e) { /* the page keeps its tabs */ }

    // grids re-render after callbacks and partial postbacks
    var pending = false;
    new MutationObserver(function () {
        if (pending) return;
        pending = true;
        setTimeout(function () { pending = false; ensureButtons(); translateEmpty(); }, 150);
    }).observe(body, { childList: true, subtree: true });
})();
