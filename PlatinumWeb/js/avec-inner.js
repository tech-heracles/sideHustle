/*
 * AVEC theme helpers for the inner pages. FaqeKryesore.aspx adds this script to every page it opens
 * in its main frame, so the pages themselves need no change.
 *
 * - Grid filter row: hidden by default and shown with a funnel button next to the grid tools.
 *   The choice is remembered per browser (localStorage "avec.filtersOn").
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

    body.classList.toggle('avec-filters-off', !filtersOn());
    ensureButtons();

    // grids re-render after callbacks and partial postbacks
    var pending = false;
    new MutationObserver(function () {
        if (pending) return;
        pending = true;
        setTimeout(function () { pending = false; ensureButtons(); }, 150);
    }).observe(body, { childList: true, subtree: true });
})();
