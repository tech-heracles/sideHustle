; //variabla per te kaluar nga nje reshti i grides tek tjetri
var indexSel = 0;
var id;
var numur;

var indexModifiko;
var focuschange = false;
function OnGetRowValues(values) {
    var hf = $("#hfLloji")[0];
    id = values[0];
    numur = values[1]; focuschange = false;
    if (mbush)
        myMenu.ShikoClick(editor, 'Shto_RegjistrimRezervimi.aspx?lloj=' + Utils.getUrlVar('lloj') + '&id=' + gvRegRZ.GetRowKey(gvRegRZ.GetFocusedRowIndex()) + '&numer=' + values[1] + '&indexrow=' + indexModifiko + '&shtim_modifikim=modifikim');
}

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
    changeName();
});

//per filtrat
function checkText(s, e) {
    myMenu.checkText(s, e);
} var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvRegRZ, "", "");
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
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, gvRegRZ, indexSel);
}
/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, gvRegRZ, indexSel);
}
/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, gvRegRZ, indexSel);
}
/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, gvRegRZ, indexSel);
}

/*
Function: menu_click

perdoret per veprimet e menuse ne javascript

Parameters:

e-eventi
*/
function menu_click(s, e) {
    var hf = $("#hfLloji")[0];
    myMenu.menu_click_regjistrime(s, e, "Shto_RegjistrimRezervimi.aspx?lloj=" + Utils.getUrlVar('lloj') + '&shtim_modifikim=shtim', 'Shto_RegjistrimRezervimi.aspx?lloj=' + Utils.getUrlVar('lloj') + '&id=' + gvRegRZ.GetRowKey(gvRegRZ.GetFocusedRowIndex()) + '&numer=' + numur + '&indexrow=' + indexModifiko + '&shtim_modifikim=modifikim');
    if (e.item.name == "Konverto") {
        e.processOnServer = false;
        gvRegRZ.GetSelectedFieldValues('IdKokaRezervime;NrDok;IdNivel;IdKlientFurnitor', OnGridSelectionComplete);
    }
}
function OnGridSelectionComplete(values) {
    if (values.length === 0) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("regjisDokZgjidhniTePakten1DokPerKonvertim"))
        return;
    }
    var idnivel = values[0][2];
    var idklienti = values[0][3];
    var ids = new Array();
    for (i = 0; i < values.length; i++) {
        if (values[i][2] != idnivel) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgDokTeJeneTeSeNjejtesNenkategori"));
            return;
        }

        else if (idklienti == 0 && values[i][3] != 0)
            idklienti = values[i][3];
        else if (values[i][3] != 0 && idklienti != values[i][3]) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgDokTeKeneTeNjejtinKF"));
            return;
        }
        ids[i] = values[i][0];
    }
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "KontrolloKonvertuarRezervime"),
        data: JSON.stringify({ ids: ids, pageId: window['CurrentPageId'] })
    }).done(SuccededCallbackKonvertime);

}
var konfigurimet, idte;
function SuccededCallbackKonvertime(result) {
    if (result[1] == "Nuk jane konvertuar") {
        if (result[0].length == 0) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgDokNukMundTeKonvertohet")); return;
        }

        cmbKonverto.ClearItems();
        for (i = 0; i < result[0].length; i++)
            cmbKonverto.AddItem(result[0][i].Kodi, result[0][i].IdNivel); //AddItem(teksti, vlera);
        cmbKonverto.SelectIndex(0);
        cmbKonf.ClearItems();
        for (i = 0; i < result[3].length; i++)
            if (result[3][i].IdNivel == result[0][0].IdNivel)
                cmbKonf.AddItem(result[3][i].KodKonfigAmbjente, result[3][i].IdKonfigAmbjente); //AddItem(teksti, vlera);
        cmbKonf.SelectIndex(0);
        konfigurimet = result[3];
        idte = result[2];
        popKonvertim.Show();
    }
    else
        myMesazh.ShtoMesazhGabimi(result[1]);
}

function ndryshoNiveli(s, e) {
    cmbKonf.ClearItems();
    for (i = 0; i < konfigurimet.length; i++)
        if (konfigurimet[i].IdNivel == s.GetValue())
            cmbKonf.AddItem(konfigurimet[i].KodKonfigAmbjente, konfigurimet[i].IdKonfigAmbjente); //AddItem(teksti, vlera);
    cmbKonf.SelectIndex(0);
}

function konverto() {
    Utils.konverto("", "", idte, SuccededCallbackKontrolloEkzistojneDokQePoKonvertohen);
}

function SuccededCallbackKontrolloEkzistojneDokQePoKonvertohen() {
    
    if (cmbKonverto.GetText() == 'FD')
        myFaqeCelje.kontrolloTeDrejta('Shto_RegjistrimMagazine.aspx?lloj=dalje' + '&id=' + idte[0] + '&numer=' + numur + '&indexrow=' + indexModifiko + '&shtim_modifikim=rezervim&niveli=' + cmbKonverto.GetValue() + '&konfigurim=' + cmbKonf.GetValue() + '&pageCacheId=' + window['CurrentPageId'])

    else
        myFaqeCelje.kontrolloTeDrejta('Shto_RegjistrimDokumentash.aspx?shitje_blerje=shitje' + '&id=' + idte[0] + '&numer=' + numur + '&indexrow=' + indexModifiko + '&shtim_modifikim=rezervim&niveli=' + cmbKonverto.GetValue() + '&konfigurim=' + cmbKonf.GetValue() + '&pageCacheId=' + window['CurrentPageId']);
}

var mbush = false;
function OnGridDoubleClick(e, index) {
    if (!focuschange) {
        var hf = $("#hfLloji")[0];
        myMenu.ShikoClick(editor, 'Shto_RegjistrimRezervimi.aspx?lloj=' + Utils.getUrlVar('lloj') + '&id=' + gvRegRZ.GetRowKey(gvRegRZ.GetFocusedRowIndex()) + '&numer=' + numur + '&indexrow=' + indexModifiko + '&shtim_modifikim=modifikim');
    }
    else {
        indexModifiko = index;
        mbush = true;
    }
}

var editor;
//merr te dhenat e rreshtit te selektuar
function mbushfusha(e) {
    editor = e;
    indexModifiko = gvRegRZ.GetFocusedRowIndex();

    gvRegRZ.GetRowValues(indexModifiko, 'IdKokaRezervime;NrDok', OnGetRowValues);
    focuschange = true;
}

function changeName() {
    var hf = $("#hfKonffillestar")[0];
    myFaqeCelje.changeName('RegjistrimRezervimi.aspx?lloj=' + Utils.getUrlVar('lloj'), 0, hf);

    myMesazh.InicializoTimer();
    myMesazh.shtoHandler();
    $('#dvMenu').show();
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
    callWebserviceKonfigurimi("513", cmbKonfigurimi.GetText());
}

function ndryshoKonfiguriminInit() {

}

function callWebserviceKonfigurimi(idKomp, kodKonf) {

    gvRegRZ.PerformCallback(idKomp + ";" + kodKonf);
}

function EndRequestHandler(sender, args) {
}