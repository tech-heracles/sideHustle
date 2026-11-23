;

window.RaportetAll = (function () {
    var ngaCRM = Utils.getUrlVar("vjenNga");
    var width = $(window).width();
    var splitpane0 = window.parent && window.parent.splitter ? window.parent.splitter.GetPane(0) : undefined;
    var splitpane1 = window.parent && window.parent.splitter ? window.parent.splitter.GetPane(1) : undefined;
    var splitpane2 = splitpane1 ? splitpane1.GetPane(0) : undefined;
    var splitpane3 = splitpane1 ? splitpane1.GetPane(1) : undefined;


    var hapRaportin = function (s) {
        window.location = "Raporti.aspx?idraporti=" + s + "&windowWidth=" + width + "&radButon=" + btnradPeriudha.GetSelectedItem().value + "&dateNga=" + dtdoknga.GetText() + "&dateDeri=" + dtdokderi.GetText() + "&Filtro=false&vjenNga=" + ngaCRM + "&previousPage=RaportetAll";
    };    

    var filtroButtonClick = function (idRaporti) {
        window.location = "Raporti.aspx?idraporti=" + idRaporti + "&windowWidth=" + width + "&radButon=&dateNga=&dateDeri=&Filtro=true&vjenNga=" + ngaCRM + "&previousPage=RaportetAllNew";
    };

    var klickselectedvaluedok = function (theRadio, e) {
        if (theRadio.GetSelectedIndex() != -1) {
            var rblCaseControlDok = theRadio.GetSelectedItem().value;
            if (rblCaseControlDok == '1') {
                dtdoknga.SetEnabled(true);
                dtdokderi.SetEnabled(true);
            }
            else {
                dtdoknga.SetEnabled(false);
                dtdokderi.SetEnabled(false);
            }
        }
    };

    var expandAll = function (s, e) {
        if (splitpane0 && splitpane1) splitpane0.Collapse(splitpane1);   // paneli i navigimit nuk ekziston me
    };

    var collapseAll = function (s, e) {
        if (splitpane0 && splitpane1) splitpane0.Expand(splitpane1);
    };

    return {
        hapRaportin: filtroButtonClick,
        filtroButtonClick: filtroButtonClick,
        klickselectedvaluedok: klickselectedvaluedok,
        expandAll: expandAll,
        collapseAll: collapseAll
    };
})();

function hapraportin(s) {
    RaportetAll.hapRaportin(s);
}

