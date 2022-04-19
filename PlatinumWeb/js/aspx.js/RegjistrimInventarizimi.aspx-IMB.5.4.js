; //variabla per te kaluar nga nje reshti i grides tek tjetri
var indexSel = 0;
var id;
var numur;
var idgjenerues;
var krahasuar;
var indexModifiko;
var focuschange = false;
function OnGetRowValues(values) {
    var hf = $("#hfLloji")[0];
    id = values[0];
    numur = values[1]; idgjenerues = values[2]; krahasuar=values[3], focuschange = false;
    if (mbush) myMenu.ShikoClick(editor, 'Shto_RegjistrimInventarizimi.aspx?lloj=' + Utils.getUrlVar('lloj') + '&id=' + grid_RegInv.GetRowKey(grid_RegInv.GetFocusedRowIndex()) + '&numer=' + values[1] + '&indexrow=' + indexModifiko + '&krahasuar='+ values[3]+'&shtim_modifikim=modifikim');
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
    myMenu.aplikoFiltra(s, e, grid_RegInv, "", "");
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
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, grid_RegInv, indexSel);
}
/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, grid_RegInv, indexSel);
}
/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, grid_RegInv, indexSel);
}
/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, grid_RegInv, indexSel);
}

/*
Function: menu_click

perdoret per veprimet e menuse ne javascript

Parameters:

e-eventi
*/
function menu_click(s, e) {
    var hf = $("#hfLloji")[0];
    if (e.item.name === 'Fshi') {
        e.processOnServer = false;
        popFshi.Hide();
        grid_RegInv.GetSelectedFieldValues('IdKoka;NrDok;IdGjenerues;Krahasuar', SuccededCallbackFshi);
    }
    else
        myMenu.menu_click_regjistrime(s, e, "Shto_RegjistrimInventarizimi.aspx?lloj=" + Utils.getUrlVar('lloj') + '&shtim_modifikim=shtim', 'Shto_RegjistrimInventarizimi.aspx?lloj=' + Utils.getUrlVar('lloj') + '&id=' + grid_RegInv.GetRowKey(grid_RegInv.GetFocusedRowIndex()) + '&numer=' + numur + '&indexrow=' + indexModifiko + '&krahasuar=' + krahasuar + '&shtim_modifikim=modifikim', grid_RegInv.GetSelectedRowCount());
    if (e.item.name === 'Klono') {
        if (idgjenerues != null) {
            myMesazh.ShtoMesazhGabimi('Nuk mund te klononi nje dokument te gjeneruar nga nje ambjent tjeter!');
            e.processOnServer = false;
        }
        else
            if (id === null)
                myMesazh.ShtoMesazhGabimi(hfState.Get("regjisDokNukKeniAsnjeDokTeZgjedhur"));
            else
                myFaqeCelje.kontrolloTeDrejta('Shto_RegjistrimInventarizimi.aspx?lloj=' + Utils.getUrlVar('lloj') + '&id=' + grid_RegInv.GetRowKey(grid_RegInv.GetFocusedRowIndex()) + '&numer=' + numur + '&indexrow=' + indexModifiko + '&shtim_modifikim=klonim');
        e.processOnServer = false;
    }
    if (e.item.name == "Krahaso") {
        e.processOnServer = false;
        grid_RegInv.GetSelectedFieldValues('IdKoka;DtDok;IdMag;Krahasuar;NrDok', OnGridSelectionComplete);
    }
    if (e.item.name == "Eksporto") {
        e.processOnServer = false;
        grid_RegInv.GetSelectedFieldValues('IdKoka', OnGridSelectionCompleteEksport);
    }
}

function OnGridSelectionCompleteEksport(values) {
    var lloji = Utils.getUrlVar('lloj') == 'agj' ? 'inventarizimAgj' : 'inventarizimAsh';
    var idkatdok = lloji == 'inventarizimAgj' ? 136 : 135;
    var kategoria = lloji == 'inventarizimAgj' ? 'Inventarizimi afatgjate' : 'Inventarizimi';
    var formati = lloji == 'inventarizimAgj' ? 'Format Standart per inventarizimin e artikujve afatgjate' : 'Format Standart per inventarizimin e artikujve afatshkurter';
    myButtonClickLupa.ButtonClickLupaEksporto(values, lloji, idkatdok, kategoria, formati);
}


function OnGridSelectionComplete(values) {
    if (values.length === 0) {
        myMesazh.ShtoMesazhGabimi("Zgjidhni te pakten nje dokument");
        return;
    }
    var shfaqmesazh = false;
    var mesazh = "Dokumentat ";
    data = values[0][1]
    magazina = values[0][2];
    var ids = new Array();
    var status = new Array();
    for (i = 0; i < values.length; i++) {
        if (values[i][1].toDateString() != data.toDateString()) {
            myMesazh.ShtoMesazhGabimi("Dokumentat duhet te jene te te njejtes date!");
            return;
        }
        else if ( magazina != values[i][2]) {
            myMesazh.ShtoMesazhGabimi("Dokumentat duhet te jene te te njejtes magazine!");
            return;
        }
        if (values[i][3] == 'Po') {
            mesazh += values[i][4] + ',';
            shfaqmesazh = true;
        }
        ids[i] = values[i][0];
        status[i] = values[i][3];
    }
    mesazh = mesazh.substr(0, mesazh.length - 1);
    mesazh += " jane perdorur per inventarizm!   "
    if (shfaqmesazh)
        alert(mesazh);

    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "RuajIdInventarizimi"),
        data: JSON.stringify({
            ids: ids,
            pageId: window['CurrentPageId']
        })
    }).done(SuccededCallbackInventarizime);
}
function SuccededCallbackInventarizime(result)
{
    var queryStr = {

        lloj: Utils.getUrlVar("lloj"),
        id: result,
        RegjistrimInventarizimiPageId: window['CurrentPageId']
    };
    myFaqeCelje.kontrolloTeDrejta('KrahasimInventarizimi.aspx?'+Utils.KonvertoObjectQueryString(queryStr));
}
function SuccededCallbackFshi(selectedValues) {
    var mesazh = "Dokumentat ";
    var shfaqmesazh = false;
    for (var i = 0; i < selectedValues.length; i++)
    {
        if (selectedValues[i][3] == 'Po') {
            mesazh += selectedValues[i][1] + ',';
            shfaqmesazh = true;
        }

    }
    mesazh = mesazh.substr(0, mesazh.length - 1);
    mesazh+= " jane perdorur per inventarizm!   "
    lblMsgbox.SetText((shfaqmesazh? mesazh:"")+hfState.Get("msgJuKeniZgjedhur") + selectedValues.length + hfState.Get("msgRreshta") + hfState.Get("labelAdministrimiMsgJeniSigurt"));
    popFshi.Show();
}
var mbush = false;
function OnGridDoubleClick(e, index) {
    if (!focuschange) {
        var hf = $("#hfLloji")[0];
        myMenu.ShikoClick(editor, 'Shto_RegjistrimInventarizimi.aspx?lloj=' + Utils.getUrlVar('lloj') + '&id=' + grid_RegInv.GetRowKey(grid_RegInv.GetFocusedRowIndex()) + '&numer=' + numur + '&indexrow=' + indexModifiko + '&krahasuar=' +krahasuar+ '&shtim_modifikim=modifikim');
    } else {
        indexModifiko = index;
        mbush = true;
    }
}
var editor;
//merr te dhenat e rreshtit te selektuar
function mbushfusha(e) {
    editor = e;
    indexModifiko = grid_RegInv.GetFocusedRowIndex();

    grid_RegInv.GetRowValues(indexModifiko, 'IdKoka;NrDok;IdGjenerues;Krahasuar', OnGetRowValues);
    focuschange = true;
}
function changeName() {
    var hf = $("#hfKonffillestar")[0];
    myFaqeCelje.changeName('RegjistrimInventarizimi.aspx?lloj=' + Utils.getUrlVar('lloj'), 0, hf);

    myMesazh.InicializoTimer();
    myMesazh.shtoHandler();
    $('#dvMenu').show();//[0].style.visibility = 'visible';
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
    lblKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[1]);
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[0]);
    callWebserviceKonfigurimi("547", cmbKonfigurimi.GetText());
    $.ajax({        
        url: Utils.getServerApiUrl("Rregjistrime", "ktheIndexSelectedFilterPeriudhaKusht"),
        data: JSON.stringify({ idKonfigurimi: cmbKonfigurimi.GetValue() })
    }).done(SuccededCallbacPeriudhaKusht);
}

function SuccededCallbacPeriudhaKusht(result) {
    hfState.Set("PeriudhaSelektuar", result);
    radDtDok.SetSelectedIndex(result);
}

function ndryshoKonfiguriminInit() {

}
function callWebserviceKonfigurimi(idKomp, kodKonf) {

    grid_RegInv.PerformCallback(idKomp + ";" + kodKonf);
} function EndRequestHandler(sender, args) {
}

function clickExport(e) {
    if (grid_RegInv.GetSelectedRowCount() == 0) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjdhniNjeNgaElementetEListes"));
        e.processOnServer = false;
    }
}
function ndryshoKonfigurimin() {
    lblKonfigurimi.SetText(cmbKonfigurimi.GetSelectedItem().GetColumnText("PershkrimKonfigAmbjente"));
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetSelectedItem().GetColumnText("KodKonfigAmbjente"));
    callWebserviceKonfigurimi("552", cmbKonfigurimi.GetText());
    //TODO getson me kismet te behet edhe kjo funksionale
    //   PlatinumWeb.wsfunc.ktheIndexSelectedFilterPeriudhaKusht(cmbKonfigurimi.GetValue(), SuccededCallbacPeriudhaKusht, myWS.webServiceFail);
}

function SuccededCallbacPeriudhaKusht(result) {
    hfState.Set("PeriudhaSelektuar", result);
    radDtDok.SetSelectedIndex(result);
}

function ndryshoKonfiguriminInit() {

}
function callWebserviceKonfigurimi(idKomp, kodKonf) {

    grid_RegMag.PerformCallback(idKomp + ";" + kodKonf);
}