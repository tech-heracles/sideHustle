; //variabla per te kaluar nga nje reshti i grides tek tjetri
var indexSel = 0;
var id;
var numur;
var kategoria;
var indexModifiko;
var idetape;
var nrprocesi;
var muaji;
var pageState = {
    kushte: {
        hapRaportAprovimi: ""
    },
    initState: function () {
        this.kushte.hapRaportAprovimi = hfState.Get("kushtHapRapPerAprovim");
    }
};

$(document).ready(function () {
    $(document).keydown(function (e) {
        switch (e.which) {
            case 13:
                e.preventDefault();
                break;
            case 116: //F5
                window.parent.rifresko = true;
                break;
            case 82:
                if (e.ctrlKey) //ctrl+r
                    window.parent.rifresko = true;
            default:
                break;
        }
    });
    $(window).on('load', function () {
        Init();
    });
    changeName();
});
function Init() {
    pageState.initState();
}

function OnGetRowValues(values) {
    id = values[0];
    numur = values[1];
    kategoria = values[2]
    idetape = values[3];
    nrprocesi = values[4];
    muaji = values[5];
    viti = values[6];
    if (values[7] != null)
        lloji = values[7].toLowerCase();
    else lloji = '';
    focuschange = false;
    if (mbush) {
        var status = Utils.getUrlVar("status");
        var url = MerrUrlDokumenti(kategoria, id, numur, indexModifiko, status, idetape, nrprocesi, lloji);
        if (url != "" && url != null)
            myMenu.ShikoClick(editor, url);
    }
}
function MerrUrlDokumenti(kategoria, id, numur, indexModifiko, status, idetape, nrprocesi, lloji) {

    var url = "";
    switch (kategoria) {
        case 1:
        case 2:
            var shitjeblerje = kategoria == 1 ? 'shitje' : 'blerje';
            url = 'Shto_RegjistrimDokumentash.aspx?shitje_blerje=' + shitjeblerje + '&shtim_modifikim=modifikim&id=' + id + '&numer=' + numur + '&indexrow=' + indexModifiko + '&vjenNga=' + status + '&idetapa=' + idetape + '&nrprocesi=' + nrprocesi;
           break;
        case 38:
            if (pageState.kushte.hapRaportAprovimi == "Po") {
                location.href = 'Raporti.aspx?emriReal=perAprovim&Muaji=' + muaji + '&Viti=' + viti + '&vjenNga=Aprovim';
                return;
            }
            url = 'Shto_ListPagesa.aspx?shtim_modifikim=modifikim&id=' + id + '&numer=' + numur + '&indexrow=' + indexModifiko + '&vjenNga=' + status + '&idetapa=' + idetape + '&nrprocesi=' + nrprocesi;
            break;
        case 3:
            url = 'ShtoVeprimBanka.aspx?lloji=' + lloji + '&shtim_modifikim=modifikim&id=' + id + '&numer=' + numur + '&indexrow=' + indexModifiko + '&vjenNga=' + status + '&idetapa=' + idetape + '&nrprocesi=' + nrprocesi;
            break;
        case 179:
            url = 'B_Shto_RegjistrimDokumentBuxheti.aspx?lloji=planifikimEkzekutimi&shtim_modifikim=modifikim&id=' + id + '&numer=' + numur + '&indexrow=' + indexModifiko + '&vjenNga=' + status + '&idetapa=' + idetape + '&nrprocesi=' + nrprocesi;
            break;
        default:
            console.error("Kategoria e panjohur!", kategoria);
    }
    return url;
}

//per filtrat
function checkText(s, e) {
    myMenu.checkText(s, e);
} var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvAprovimet, "", "");
}
function textChanged(s, e) {
    myMenu.textChanged(s, e);
}


/*
Function: Poshte_click

perdoret per te selektuar rreshtin me poshte

Parameters:

e-eventi
*/
function Poshte_click(e) {
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, gvAprovimet, indexSel);
}
/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, gvAprovimet, indexSel);
}
/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, gvAprovimet, indexSel);
}
/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, gvAprovimet, indexSel);
}

/*
Function: menu_click

perdoret per veprimet e menuse ne javascript

Parameters:

e-eventi
*/
function menu_click(s, e) {
    indexModifiko = gvAprovimet.GetFocusedRowIndex();
    if (indexModifiko == -1) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgDuhetTeZgjidhniNjeEtape"));
        e.processOnServer = false;
    }
    else if (e.item.name == "Shiko") {
        mbush = true;
        mbushfusha(e);
        e.processOnServer = false;
    }
    else if (e.item.name == 'Komento') {
        LinkClick1(s, e);
        e.processOnServer = false;
    }

}

var mbush = false;

function OnGridDoubleClick(e, index) {
    mbush = true;
    mbushfusha(e);
}
var editor;
var focuschange = false;
//merr te dhenat e rreshtit te selektuar
function mbushfusha(e) {
    editor = e;
    indexModifiko = gvAprovimet.GetFocusedRowIndex();
    gvAprovimet.GetRowValues(indexModifiko, 'IdKokaShitje;NrDokumenti;IDKATDOK;IdEtapa;NrProcesi;Muaji;Viti;LlojVeprimit', OnGetRowValues);
    focuschange = true;

}
function changeName() {

    var hf = $("#hfKonffillestar")[0];
    myFaqeCelje.changeName('ListeAprovimi.aspx?status=' + Utils.getUrlVar("status"), 0, hf);
    myMesazh.InicializoTimer();
    myMesazh.shtoHandler(); $('#dvMenu').show();//[0].style.visibility = 'visible';
}

var indexModifiko;

function OnError(message, context) {
    if (message == "Session TimeOut")
        ndryshoUrlFrame(Paths.defaultLoginPath);
}

function selection(index) {
    hf = $("#hfReshtaTeSelektuar")[0];
    hf.value = gvAprovimet.GetSelectedKeysOnPage();
}
function OnGridSelectionCompleteMultiSelect(values) {


}
function BeginCallback(s, e) {

    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}
function enter() {
    if (window.event.keyCode == 13) {
        event.returnValue = false;
        event.cancel = true;
    }
}

function ndryshoKonfigurimin() {
    lblKonfigurimi.SetText(cmbKonfigurimi.GetSelectedItem().GetColumnText("PershkrimKonfigAmbjente"));
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetSelectedItem().GetColumnText("KodKonfigAmbjente"));
    callWebserviceKonfigurimi("535", cmbKonfigurimi.GetText());
}
function ndryshoKonfiguriminInit() {

}
function callWebserviceKonfigurimi(idKomp, kodKonf) {

    gvAprovimet.PerformCallback(idKomp + ";" + kodKonf);
} function EndRequestHandler(sender, args) {
    kategoria = 0;
}
function LinkClick1(s, e) {
    gvAprovimet.GetRowValues(indexModifiko, 'IdEtapa;NrProcesi;IDKATDOK', OnGetRowValuesLink);

} function LinkClick(s, key) {

    gvAprovimet.GetRowValues(key, 'IdEtapa;NrProcesi;IDKATDOK', OnGetRowValuesLink2);

}
function OnGetRowValuesLink(values) {
    myButtonClickLupa.LupaUniversal_Click(hfState.Get("LupaKomentet"), 'LupaKomente.aspx?idetapa=' + values[0] + '&nrprocesi=' + values[1] + '&veprimi=Gjitha&idkategoria=' + values[2], 600, 500);

} function OnGetRowValuesLink2(values) {

    myButtonClickLupa.LupaUniversal_Click(hfState.Get("LupaKomentet"), 'LupaKomente.aspx?idetapa=' + values[0] + '&nrprocesi=' + values[1] + '&veprimi=Etapa&idkategoria=' + values[2], 600, 500);

}

function vendosNgjyre(s, e) {
    if (s["GetPosition"] == undefined) return;
    var pos = s.GetPosition();
    var tdKontroll = $(s.GetMainElement()).find("td:last");
    switch (pos) {
        case 0:
            tdKontroll.css('color', '#CC0000');
            break;
        case 100:
            tdKontroll.css("color", "#006600");
            break;
        default:
            tdKontroll.css("color", "#FF6600");
    }
}