/*
 * AVEC theme: marker classes for the theme CSS (App_Themes/Avec).
 *
 * The theme used to pick these elements with CSS :has(). With :has() in the stylesheet every DOM change made the
 * browser re-check the whole page, and the page scripts change the DOM hundreds of times while a page opens
 * (measured on Shitje e re: about 3.7 s of the 4.5 s of script time went there). The same tests now run here, a
 * handful of times, and the CSS only looks at the classes.
 *
 * Global.asax adds this script (defer) to the <head> of every page. Keep the tests in step with the rules in
 * avec.css / zzzz-avec-review.css that use these classes.
 */
(function () {
    'use strict';
    if (window.__avecTags) return;
    window.__avecTags = true;

    var d = document;

    function set(el, cls, on) {
        if (el.classList.contains(cls) !== on) el.classList.toggle(cls, on);
    }

    function each(sel, fn) {
        var l = d.querySelectorAll(sel);
        for (var i = 0; i < l.length; i++) fn(l[i]);
    }

    function hasStyle(el, parts) {
        var s = el.getAttribute('style') || '';
        for (var i = 0; i < parts.length; i++) if (s.indexOf(parts[i]) >= 0) return true;
        return false;
    }

    // the record tabs hide while the lead tab is hidden; the lead tab is shown / hidden through its style
    var leadObserver = window.MutationObserver ? new MutationObserver(function (records) {
        for (var i = 0; i < records.length; i++) tagStrip(records[i].target.parentNode);
    }) : null;
    var watched = typeof WeakSet === 'function' ? new WeakSet() : null;

    function tagStrip(ul) {
        if (!ul || ul.nodeType !== 1) return;
        var hide = false;
        for (var c = ul.firstElementChild; c; c = c.nextElementSibling) {
            if (!c.classList.contains('dxtc-lead') || !c.classList.contains('dxtc-tab') || c.tagName !== 'LI') continue;
            if (leadObserver && watched && !watched.has(c)) {
                watched.add(c);
                leadObserver.observe(c, { attributes: true, attributeFilter: ['style'] });
            }
            if (hasStyle(c, ['display:none', 'display: none'])) hide = true;
        }
        set(ul, 'avec-strip-nolead', hide);
    }

    function tag() {
        var b = d.body;
        if (!b) return;

        set(b, 'avec-has-toptabs', !!d.querySelector('.dxtcLite_Material.dxtc-top'));
        set(b, 'avec-has-toptabs-page', !!d.querySelector('.dxtcLite_Material.dxtc-top:not(.ui-accordion .dxtcLite_Material)'));
        set(b, 'avec-has-menu1', !!d.querySelector('.dxm-main[id$="ASPxMenu1"]'));

        // icon-only buttons stay transparent, buttons with a caption are primary (orange)
        each('div.dxbButton_Material', function (e) {
            var vam = !!e.querySelector('span.dx-vam');
            set(e, 'avec-btn-text', vam);
            set(e, 'avec-btn-icon', !vam && !!e.querySelector('img'));
        });

        // page toolbar
        each('li.dxm-separator', function (e) {
            var n = e.nextElementSibling;
            set(e, 'avec-sep-tmpl', !!(n && n.tagName === 'LI' && n.classList.contains('dxm-tmpl')));
        });
        each('li.dxm-tmpl', function (e) {
            set(e, 'avec-mi-hidden', !!e.querySelector('[id$="btnFiltra"], [id$="btnCollapseAll"], [id$="btnExpandAll"]'));
        });
        each('li.dxm-item', function (e) {
            set(e, 'avec-mi-import', !!e.querySelector('img[src*="/menu/Import."]'));
            set(e, 'avec-mi-arkiva', !!e.querySelector('img[src*="/menu/Arkiva"]'));
        });
        each('div.dxm-main[id$="ASPxMenu1"]', function (m) {
            // a div whose own table holds the toolbar
            for (var p = m.parentElement; p && p !== b; p = p.parentElement) {
                if (p.tagName === 'TABLE' && p.parentElement && p.parentElement.tagName === 'DIV') set(p.parentElement, 'avec-menu-host', true);
            }
        });

        // Model picker (cmbKonfigurimi) and its caption
        each('[id$="konfigurimi_Label"], [id$="lblKonfigurimi"], [id$="lblModeli"], table[id$="cmbKonfigurimi"]', function (e) {
            var td = e.parentElement;
            if (!td || td.tagName !== 'TD') return;
            set(td, 'avec-td-konfig', true);
            set(td, 'avec-td-grid', !!td.querySelector('.dxgv, .ui-jqgrid'));
            var tr = td.parentElement;
            if (e.tagName === 'TABLE' && /cmbKonfigurimi$/.test(e.id) && tr && tr.tagName === 'TR')
                set(tr, 'avec-tr-konfig-only', !tr.querySelector('input:not([id*="cmbKonfigurimi"])'));
        });

        // grids
        each('table.dxgvControl_Material', function (g) {
            set(g, 'avec-grid-search', !!g.querySelector('.dxgvSearchPanel_Material'));
            set(g, 'avec-grid-filterable', !!g.querySelector('.avec-filter-toggle'));
        });
        each('.dxgvTitlePanel_Material', function (p) {
            set(p, 'avec-tp-editors', !!p.querySelector('.dxeRadioButtonList_Material, .dxeButtonEdit_Material, .dxeTextBox_Material, span.dxeBase_Material'));
        });
        each('form > div', function (e) {
            if (hasStyle(e, ['width: 50%', 'width:50%'])) set(e, 'avec-half-grid', !!e.querySelector('table.dxgvControl_Material'));
        });

        // tab strips
        each('ul.dxtc-strip, ul.dxtc-row', tagStrip);
    }

    tag();
    if (d.readyState === 'loading') d.addEventListener('DOMContentLoaded', tag);
    window.addEventListener('load', tag);

    // callbacks and page scripts add and replace parts of the page: tag again before the next frame is drawn
    if (window.MutationObserver) {
        var pending = false;
        new MutationObserver(function () {
            if (pending) return;
            pending = true;
            (window.requestAnimationFrame || setTimeout)(function () { pending = false; tag(); });
        }).observe(d.documentElement, { childList: true, subtree: true });
    }

    window.avecTags = tag;
})();
