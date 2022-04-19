; var editmode = false;
var indexEdit = -1;
var indexModifiko;
var grida = "gvKonfigurimi";

//per filtrat kontrollon emrin e filtrit
function checkText(s, e) {
    myMenu.checkText(s, e);
}
//kur dokumenti behet gati 
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

});

var btnFiltrat;
///aplikon filtrin e zgjedhur
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    $('#hfRuaj').val('Filtra');
   
        myMenu.aplikoFiltra(s, e, gvKonfigurimi, "", '');
  
}
/// kur ndryshon fusha e filtrit 
function textChanged(s, e) {
    myMenu.textChanged(s, e);
}
//kur behet callback zbrazim fushen e filtrit
function BeginCallback(s, e) {
   
 
    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}
///kur grida ben double click kontrollohen te drejtat dhe  hyn ne editim
function RowDblClickGrida1(s, e) {
    callWebservice();
    indexModifiko = e.visibleIndex;
    grida = 'gvKonfigurimi';
}

///validim i fushes koeficienti e bere nga ne qe fusha te jete numer
function validate(s, e) {
 if(gvKonfigurimi.GetEditValue("Koeficienti")!=null)
    if(!gvKonfigurimi.GetEditValue("Koeficienti").match('^[0-9]*\.?[0-9]+$'))
    {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgKoeficientiDuhetNumer"));
        e.isValid = false;
        e.errorText = hfState.Get('msgKoeficientiDuhetNumer');
    }
    
}

/*
Function: Poshte_click

perdoret per te selektuar rreshtin me poshte

Parameters:

e-eventi
*/
function Poshte_click(e) {
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, gvKonfigurimi, indexSel);
}
/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, gvKonfigurimi, indexSel);
}
/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, gvKonfigurimi, indexSel);
}
/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, gvKonfigurimi, indexSel);
}

//pastron arrayt
function pastro() {
    arrKategoria = new Array();
    arrLloji = new Array();
}

function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ?
												            evt.keyCode : evt.charCode;
}
///therret web service e te drejtave per te pare nqs perdoruesi ka te drejte te modifikoje
function callWebservice() {
    var emer = 'KonfigurimListOrari.aspx';
    $.ajax({        
        url: Utils.getServerApiUrl("Rregjistrime", "eshteVeprimILejuar"),
        data: JSON.stringify({ emerKomponente: emer, veprimi: 'Modifiko' })
    }).done(SucceededCallback);
}

// This is the callback function that
// processes the Web Service return value.
//nqs perdoruesi ka te drejta hyn ne modifikim prn del mesazhi
function SucceededCallback(result) {
    if (result == "true") {
        switchEditMode(indexModifiko, grida);
    }
    else {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukKeniTeDrejta"));
    }
}
var arrKategoria = new Array();
var arrLloji = new Array();
var mod = false;
// fut griden ne modifikim
function switchEditMode(index, grida) {
    $("#hfRuaj").val('Modifiko');
    ASPxMenu1.GetItemByName('Ruaj').SetVisible(true);
    myMenu.menuSipasTeDrejtaCeljeVogel($("#hfRuaj"), hfTeDrejta);
    pastro();
    indexEdit = index;
    arrKategoria = new Array();
    arrLloji = new Array();
    mod = true;
     gvKonfigurimi.StartEditRow(index); gvKonfigurimi.GetRowValues(index, "DiteJave;OreFillimi;OreMbarimi", OnCallback); 

}
///mbush fushat sipas te dhenave te grides
var orefillimi, orembarimi;
function OnCallback(result) {
    $('#hfOreFillimi').val(result[1]);
    $('#hfOreMbarimi').val(result[2]);
    orefillimi = result[1];
    orembarimi = result[2];
    arrLloji = result[0].split(';');
    var items = new Array();

    try {
        for (var i = 0; i < arrLloji.length - 1; i++)
            items.push(lbxKategoria.FindItemByText(arrLloji[i]));
        lbxKategoria.SelectItems(items);
    } catch (ee) {
        
    }
    
    try {
        txtOreFillimi.SetText(result[1]);
        txtOreMbarimi.SetText(result[2]);
    } catch (ee) {
        
    }
}
///metoda init per te inicializuar emrin  e faqes
function Init() {
    changeName();
    ASPxMenu1.GetItemByName('Ruaj').SetVisible(false);
    myMenu.menuSipasTeDrejtaCeljeVogel($("#hfRuaj"), hfTeDrejta);
    editmode = false;
    indexEdit = -1;
}
//theret emrin e faqes
function changeName() {
    myFaqeCelje.changeNameRegjistrime('KonfigurimListOrari.aspx', 0);
}


var editorPrind;
var editorNiveli;
var editorKodi;
var editorGrupi;



///kur ndryshon data, key ka vlere OreFillimi ose OreMbarimi, ora mund te jete variabli orefillimi ose orembarimi
function DateChanged(s, e, key) {
    //if (txtOreFillimi.GetDate() > txtOreMbarimi.GetDate())
    //{
    //    myMesazh.ShtoMesazhGabimi('Ora e fillimit duhet te jete me e vogel se ora e mbarimit');
    //    txtOreFillimi.SetDate(txtOreMbarimi.GetDate());
    //}
    var kontroll = Utils.ktheKontroll("txt" + key);
    var ora = kontroll.GetText();
    $('#hf' + key).val(ora);
    if (key == "OreFillimi") {
        orefillimi = ora;
    } else {
        orembarimi = ora;
    }
}

//kur inicializohet data e fillimit/mbarimit
function DateInit(s, e, key) {
    var kontroll = Utils.ktheKontroll("txt" + key);
    if (key == "OreFillimi")
        kontroll.SetText(orefillimi);
    else
        kontroll.SetText(orembarimi);
    $('#hf' + key).val(kontroll.GetText());
}

//kur inicializohet data e mbarimit
//function DateMbarimiChanged1(s, e) {
//    txtOreMbarimi.SetText(orembarimi);
//    $('#hfOreMbarimi').val(txtOreMbarimi.GetText());
//}


///menu click kur shtypet nje buton i menuse
function menu_click(s, e) {
    var hfRuaj = $('#hfRuaj')
   

        myMenu.menu_click_celjevogel(s, e, hfRuaj, gvKonfigurimi, hfTeDrejta);
  
    if (e.item.name == 'Shto') {
        ASPxMenu1.GetItemByName('Ruaj').SetVisible(true);
        pastro();
        var data = new Date();
        var datamb = new Date();
        data.setHours(0, 0, 0);
        datamb.setHours(23, 59, 0);
        orefillimi = '00:00';
        orembarimi = '23:59';
    }


    if (e.item.name == 'Modifiko') {

        pastro();
        ASPxMenu1.GetItemByName('Ruaj').SetVisible(true);
        mod = true;
        gvKonfigurimi.GetRowValues(indexModifiko, "DiteJave;OreFillimi;OreMbarimi", OnCallback);
       
    }
}
/// per te shfaqur mesazhin e serverit meqe behet ne callback dhe nuk e merr ndryshimin
function EndCallbackGrida(s, e) {
    if ($('#hfRuaj').val() == 'Modifiko' || $('#hfRuaj').val() == 'Ruaj') {
        $.ajax({
            url: Utils.getServerApiUrl("Rregjistrime", "merrMesazhNgaSesioni"),
            data: JSON.stringify({})
        }).done(SucceededCallbackMesazhi);
    }

    try {
        txtOreFillimi.SetText(orefillimi);
        txtOreMbarimi.SetText(orembarimi);
    } catch (ee) {
        
    }
}
///shfaqja e mesazhit
function SucceededCallbackMesazhi(result) {
    var arr = result.split(':');
    if (arr[1] == "Green") {
        ASPxMenu1.GetItemByName('Ruaj').SetVisible(false);
        myMesazh.ShtoMesazhSuksesi(arr[0]);
    }
    else
        if (arr[1] == "Red")
            myMesazh.ShtoMesazhGabimi(arr[0]);
}

///inicializon fushen e diteve

function InitKat(s, e) {
  
    var items = new Array();
    for (var i = 0; i < arrLloji.length - 1; i++)
        items.push(lbxKategoria.FindItemByText(arrLloji[i]));

    lbxKategoria.SelectItems(items);
       
}