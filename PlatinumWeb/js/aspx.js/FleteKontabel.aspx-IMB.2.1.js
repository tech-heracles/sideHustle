; var indexModifiko;
//variabla per te kaluar nga nje reshti i grides tek tjetri
var indexSel = 0;
var indentifikuesPerPopup = "FleteKontabel";
var id;
var numur; var focuschange = false;
var idgjenerues;
function OnGetRowValues(values) {
    id = values[0];
    numur = values[1];
    idgjenerues = values[2];
    focuschange = false;
    if (mbush) myMenu.ShikoClick(editor, 'Shto_FleteKontabel.aspx?shtim_modifikim=modifikim&numur=' + values[1] + '&id=' + gvFleteKontabelKoka.GetRowKey(gvFleteKontabelKoka.GetFocusedRowIndex()));
}
function shfaqLupeGrupKontabilizimi() {

    myButtonClickLupa.LupaUniversal_Click(hfState.Get("zgjidhGrupinEKontabilizimit"), 'LupaGrupKontabilizim.aspx', 700, 560);
}
//per filtrat
function checkText(s, e) {
    myMenu.checkText(s, e);
} var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvFleteKontabelKoka, "", "");
}
function textChanged(s, e) {
    myMenu.textChanged(s, e);
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

/*
Function: Poshte_click

perdoret per te selektuar rreshtin me poshte

Parameters:

e-eventi
*/
function Poshte_click(e) {
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, gvFleteKontabelKoka, indexSel);
}
/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, gvFleteKontabelKoka, indexSel);
}
/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, gvFleteKontabelKoka, indexSel);
}
/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, gvFleteKontabelKoka, indexSel);
}

//        // This is the callback function that
//        // processes the Web Service return value.
//        function SucceededCallback(result) {
//            if (result == "true") {
//                gvFleteKontabelKoka.GetRowValues(indexModifiko, 'IdKokaFleteKontabel;NrDukumentiKokaFleteKontabel', OnGetRowValues);
//            }
//            else {
//                alert("Nuk ke te drejta per te kryer kete veprim");
//            }
//        }
/*
Function: menu_click

perdoret per veprimet e menuse ne javascript

Parameters:

e-eventi
*/
function menu_click(s, e) {

    myMenu.menu_click_regjistrime(s, e, "Shto_FleteKontabel.aspx?shtim_modifikim=shtim&id=0", 'Shto_FleteKontabel.aspx?shtim_modifikim=modifikim&numur=' + numur + '&id=' + gvFleteKontabelKoka.GetRowKey(gvFleteKontabelKoka.GetFocusedRowIndex()));

    if (e.item.name == "Klono")
        if (idgjenerues != null) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukMundTeKlononi"));
            e.processOnServer = false;
        }
        else
            myFaqeCelje.kontrolloTeDrejta('Shto_FleteKontabel.aspx?shtim_modifikim=klonim&numur=' + numur + '&id=' + gvFleteKontabelKoka.GetRowKey(gvFleteKontabelKoka.GetFocusedRowIndex()));
}
var mbush = false;
function OnGridDoubleClick(e, index) {
    if (!focuschange)
        myMenu.ShikoClick(editor, 'Shto_FleteKontabel.aspx?shtim_modifikim=modifikim&numur=' + numur + '&id=' + gvFleteKontabelKoka.GetRowKey(gvFleteKontabelKoka.GetFocusedRowIndex()));
    else {
        indexModifiko = index;
        mbush = true;
    }
    //  myMenu.ShikoClick(e,'Shto_FleteKontabel.aspx?id=' +id + '&numur=' +numur + '&shtim_modifikim=modifikim');
    //  mbushfusha(e);
    //  callWebservice();
}
var editor;
//merr te dhenat e rreshtit te selektuar
function mbushfusha(e) {
    editor = e; focuschange = true;
    indexModifiko = gvFleteKontabelKoka.GetFocusedRowIndex();

    gvFleteKontabelKoka.GetRowValues(indexModifiko, 'IdKokaFleteKontabel;NrDukumentiKokaFleteKontabel;IdGjenerues', OnGetRowValues);
}
function changeName() {
    var hf = $("#hfKonffillestar")[0];
    myFaqeCelje.changeName('FleteKontabel.aspx',0, hf);
    myMesazh.InicializoTimer();
}

var arrSel = new Array();
var arrUnSel = new Array();
var count = 0;
var count1 = 0;

//function kundert() {

//    if (gvFleteKontabelKoka.cpNoRows > 15 * (gvFleteKontabelKoka.cpNoPage + 1)) {
//        for (l = 15 * gvFleteKontabelKoka.cpNoPage; l < 15 * (gvFleteKontabelKoka.cpNoPage + 1); l++) {
//            gvFleteKontabelKoka.SelectRowOnPage(l, !gvFleteKontabelKoka.IsRowSelectedOnPage(l));


//        }
//    }
//    else {
//        for (l = 15 * gvFleteKontabelKoka.cpNoPage; l < gvFleteKontabelKoka.cpNoRows; l++) {
//            gvFleteKontabelKoka.SelectRowOnPage(l, !gvFleteKontabelKoka.IsRowSelectedOnPage(l));


//        }
//    }
//}
//function KlikoTeGjitha() {
//    gvFleteKontabelKoka.SelectAllRowsOnPage();



//}
//function HiqTeGjitha() {
//    gvFleteKontabelKoka.UnselectAllRowsOnPage();


//}

function clickExport(e) {
    if (gvFleteKontabelKoka.GetSelectedRowCount() == 0) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjdhniNjeNgaElementetEListes"));
        e.processOnServer = false;
    }
}

function SelectionChange(s, e) {

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
    callWebserviceKonfigurimi("117", cmbKonfigurimi.GetText());
    $.ajax({       
        url: Utils.getServerApiUrl("Rregjistrime", "ktheIndexSelectedFilterPeriudhaKusht"),
        data: JSON.stringify({ idKonfigurimi: cmbKonfigurimi.GetValue() })
    }).done(SuccededCallbacPeriudhaKusht);
}

function SuccededCallbacPeriudhaKusht(result) {
    hfState.Set("PeriudhaSelektuar", result);
    radDtDok.SetSelectedIndex(result);
}

function onSelectionChanged(s, e) {
    hfState.Set('PeriudhaSelektuar', radDtDok.GetSelectedIndex());
    gvFleteKontabelKoka.PerformCallback();
}

function ndryshoKonfiguriminInit() {

}
function callWebserviceKonfigurimi(idKomp, kodKonf) {

    gvFleteKontabelKoka.PerformCallback(idKomp + ";" + kodKonf);
} function EndRequestHandler(sender, args) {
}
function Click_ButtonOk(s, e) {
    popFshi.Hide();
    Utils.shfaqLoadingGif();;
}
function closing(s, e) {
    popupUniversal.SetContentUrl('');
}