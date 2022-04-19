; var editmode = false;
var indexEdit = -1;
var indexModifiko;

//per filtrat

//kontrollon tekstin e filtrit
function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
///aplikon filtrin e zgjedhur
function aplikoFiltra(s, e) {
    $('#hfRuaj').val('Filtra'); btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvKalendar, "", '');
}
///kur dokumenti eshte gati 
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
//kur ndryshon teksti i filtrit
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
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, gvKalendar, indexSel);
}

/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, gvKalendar, indexSel);
}
/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, gvKalendar, indexSel);
}
/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, gvKalendar, indexSel);
}

function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ?
												            evt.keyCode : evt.charCode;
}
///therret webservice per te pare ne ka te drejta perdoruesi per modifikim
function callWebservice() {
    var emer = 'KalendarFestash.aspx';
    $.ajax({      
        url: Utils.getServerApiUrl("Rregjistrime", "eshteVeprimILejuar"),
        data: JSON.stringify({ emerKomponente: emer, veprimi: 'Modifiko' })
    }).done(SucceededCallback);
}
///kthimi i web servicit qe vendos griden ne gjendje modifikimi nqs perdoruesi ka te drejta
function SucceededCallback(result) {
    if (result == "true") {
        switchEditMode(indexModifiko);
    }
    else {
        myMesazh.ShtoMesazhGabimi("Nuk ke te drejta per te kryer kete veprim");
    }
}
//kalimi i grides ne gjendje editimi
function switchEditMode(index) {
    $("#hfRuaj").val('Modifiko');
    ASPxMenu1.GetItemByName('Ruaj').SetVisible(true);
    myMenu.menuSipasTeDrejtaCeljeVogel($("#hfRuaj"), hfTeDrejta);
    gvKalendar.StartEditRow(index);
    indexEdit = index;
}
///metoda init per vendosjen e emrit te faqes dhe variablat fillestare
function Init() {
    changeName();
    ASPxMenu1.GetItemByName('Ruaj').SetVisible(false);
    myMenu.menuSipasTeDrejtaCeljeVogel($("#hfRuaj"), hfTeDrejta);
    editmode = false;
    indexEdit = -1;

}
///kur shtypet menuja
function menu_click(s, e) {
    var hfRuaj = $('#hfRuaj');
    myMenu.menu_click_celjevogel(s, e, hfRuaj, gvKalendar, hfTeDrejta);
}
///marrja e emrit te faqes
function changeName() {
    myFaqeCelje.changeNameRegjistrime('KalendarFestash.aspx', 0);
}
///kur nis callback i grides
function BeginCallback(s, e) {
    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}
///perdoret per te marre mesazhin nga sessioni kur behet callback
function EndCallbackGrida(s, e) {
    if (gvKalendar.IsEditing() &&$('#hfRuaj').val() == 'Modifiko') {
        Data.SetEnabled(false);
    }
    else if ($('#hfRuaj').val() == 'Ruaj') {
        Data.SetEnabled(true);
    }
    $.ajax({      
        url: Utils.getServerApiUrl("Rregjistrime", "merrMesazhNgaSesioni"),
        data: JSON.stringify({})
    }).done(SucceededCallbackMesazhi);
}
///validim i fushes koeficienti e bere nga ne qe fusha te jete numer
function validate(s, e) {
    if (gvKalendar.GetEditValue("Koeficienti") != null)
    if (!gvKalendar.GetEditValue("Koeficienti").match('^[0-9]*\.?[0-9]+$')) {
        myMesazh.ShtoMesazhGabimi("Koeficienti duhet te jete numer!");
        e.isValid = false;
        e.errorText = 'Koeficienti duhet te jete numer!';
    }

}
///kthini i webservicet te mesazhit
function SucceededCallbackMesazhi(result) {
    var arr = result.split(':');
    if (arr[1] == "Green") {
        ASPxMenu1.GetItemByName('Ruaj').SetVisible(true);
        myMesazh.ShtoMesazhSuksesi(arr[0]);
    }
    else if (arr[1] == "Red")
        myMesazh.ShtoMesazhGabimi(arr[0]);
}
function Click_ButtonOk(s, e) {
    popFshi.Hide();
    Utils.shfaqLoadingGif();;
}
function RowDblClick_gvKalendar(s, e) {
    callWebservice();
    indexModifiko=e.visibleIndex;    
}