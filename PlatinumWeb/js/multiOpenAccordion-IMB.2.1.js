;
if (typeof multiOpenAccordion == 'undefined') {
    multiOpenAccordion = {};
}

multiOpenAccordion.shfaqDokumentin = function () {
    $(this).addClass("ui-accordion ui-accordion-icons ui-widget ui-helper-reset")
      .find("h3")
        .addClass("ui-accordion-header ui-helper-reset ui-state-default ui-corner-top ui-corner-bottom").css("display", "flex")
        .hover(function () { $(this).toggleClass("ui-state-hover"); })
        .prepend('<span class="ui-icon ui-icon-triangle-1-e ui-not-accordion-icon"></span>')
        .click(function () {
            $(this)
              .toggleClass("ui-accordion-header-active ui-state-active ui-state-default ui-corner-bottom")
              .find("> .ui-icon").toggleClass("ui-icon-triangle-1-e ui-icon-triangle-1-s").addClass("ui-not-accordion-icon").end()
              .next().toggleClass("ui-accordion-content-active").slideToggle();
            if ($(this).hasClass('ui-accordion-header-active')) {
                localStorage.setItem($(this).attr('id') + 'State' + varKonfig.identifikuesPerLocalStorageKey, 'true');
            }
            else {
                localStorage.setItem($(this).attr('id') + 'State' + varKonfig.identifikuesPerLocalStorageKey, 'false');
            }
            return false;
        })
        .next()
          .addClass("ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom")
          //.css("display", "block")
          .hide()
        .end();
};