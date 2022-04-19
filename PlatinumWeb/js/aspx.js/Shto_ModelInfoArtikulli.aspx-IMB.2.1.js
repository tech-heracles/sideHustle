;
var editmode = false;
var indexEdit = -1;

//variabla per te kaluar nga nje reshti i grides tek tjetri
var indexSel = 0;
//indexi i zgjedhur per modifikim
var indexModifiko;
//perdoret per te treguar nese duhet te mbushen tabet apo jo kjo perdoret qe tabet te mbushen vetem heren e pare dhe jo pas cdo here qe  kalohet nga nje tab tek tjetri


/*
Function: menu_click
perdoret per veprimet e menuse ne javascript
Parameters: e-eventi
*/
function menu_click(s, e) {
    var hfRuaj = $('#hfRuaj')
    myMenu.menu_click_celjevogelinfo(s, e, hfRuaj, ASPxGridView_InfoArtikulli, hfTeDrejta);

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
    ASPxMenu1.GetItemByName('Ruaj').SetVisible(false);
    changeName();
});

function checkText(s, e) {
    myMenu.checkText(s, e);
}

var btnFiltrat;

function aplikoFiltra(s, e) {
    $('#hfRuaj').val('Filtra'); btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, ASPxGridView_InfoArtikulli, "656", '');
}

function textChanged(s, e) {
    myMenu.textChanged(s, e);
}

function changeName() {
    myFaqeCelje.changeNameRegjistrime('Shto_ModelInfoArtikulli.aspx',0);

}

function OnGridDoubleClick(index) {
    indexModifiko = index;
    lista = true;
    mbushfusha();
}

/*
Function: Poshte_click  -- perdoret per te selektuar rreshtin me poshte
Parameters:  e-eventi
*/
function Poshte_click(e) {
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, ASPxGridView_InfoArtikulli, indexSel);
}

/*Function: Lart_click -- perdoret per te selektuar rreshtin me lart
Parameters: e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, ASPxGridView_InfoArtikulli, indexSel);
}

/*
Function: Fillim_click -- perdoret per te shkuar ne fillim te faqes
Parameters: e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, ASPxGridView_InfoArtikulli, indexSel);
}
/*
Function: Fund_click -- perdoret per te shkuar ne fund te faqes
Parameters: e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, ASPxGridView_InfoArtikulli, indexSel);
}

function callWebservice(s,e) {
    var emer = 'Shto_ModelInfoArtikulli.aspx';
    var veprim = 'Modifiko';
    indexModifiko = e.visibleIndex;
    //e.processOnServer = false;
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "eshteVeprimILejuar"),
        data: JSON.stringify({ emerKomponente: emer, veprimi: veprim })
    }).done(SucceededCallback);
}
function SucceededCallback(result) {
    if (result == "true") {
        var idInfo = ASPxGridView_InfoArtikulli.GetRowKey(indexModifiko);
        $('#hfId').val(idInfo);
        $("#hfRuaj").val('Modifiko');
        $.ajax({          
            url: Utils.getServerApiUrl("Rregjistrime", "ruajNeSessionTrupInfoArt"),
            data: JSON.stringify({ idkoka: idInfo })
        }).done(Succedcallback);
        switchEditMode(indexModifiko);
    }
    else {
        myMesazh.ShtoMesazhGabimi("Nuk ke te drejta per te kryer kete veprim");
    }
}
function switchEditMode(index) {
    ASPxMenu1.GetItemByName('Ruaj').SetVisible(true);
    myMenu.menuSipasTeDrejtaCeljeVogel($("#hfRuaj"), hfTeDrejta);
    ASPxGridView_InfoArtikulli.StartEditRow(index);
    //indexModifiko = index;
}
function Init() {
    changeName();
    myMenu.menuSipasTeDrejtaCeljeVogel($("#hfRuaj"), hfTeDrejta);
    editmode = false;
    indexEdit = -1;

}

function Succedcallback(result) {
}
function SuccedcallbackHapPopUp(result) {
    myButtonClickLupa.LupaUniversal_Click('Zgjidhni konfigurimin e infos ', 'LupaKonfigurimInfo.aspx?id=' + $('#hfId').val() + '&ruaj=po', 600, 500);
}


function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ?
		        evt.keyCode : evt.charCode;
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
function ClickKonfiguro(id) {
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "ruajNeSessionTrupInfoArt"),
        data: JSON.stringify({ idkoka: id })
    }).done(SuccedcallbackHapPopUp);
    $('#hfId').val(id);


}
function HapLupe() {
    myButtonClickLupa.LupaUniversal_Click('Zgjidhni konfigurimin e infos', 'LupaKonfigurimInfo.aspx?id=' + $('#hfId').val() + '&ruaj=po', 600, 500);

}
function InitKonf(ind) {
    if (ASPxGridView_InfoArtikulli.IsNewRowEditing()) {
        if (Utils.ktheKontroll('Lloji').GetValue() == null) {
            Utils.ktheKontroll('Lloji').SetSelectedIndex(1);
            Utils.ktheKontroll('Periudha').SetSelectedIndex(1);
        }
    } if (Utils.ktheKontroll('Lloji').GetValue() == "2" || Utils.ktheKontroll('Lloji').GetValue() == "2") {
        Utils.ktheKontroll('Periudha').SetEnabled(false);
    }
    for (var i = 0; i < ASPxGridView_InfoArtikulli.cpRowCount; i++)
        if (ASPxGridView_InfoArtikulli.IsEditing()) {
            Utils.ktheKontroll('btnKonfiguro' + i).SetEnabled(false);
        }
        else Utils.ktheKontroll('btnKonfiguro' + i).SetEnabled(true);
}
function SelectionChangedLloji() {
    if (ASPxGridView_InfoArtikulli.IsNewRowEditing()) {
        if (Utils.ktheKontroll('Lloji').GetValue() == "1") {
            Utils.ktheKontroll('Periudha').SetEnabled(true); Utils.ktheKontroll('Periudha').SetSelectedIndex(1);
            $('#hfId').val(-1);
            $.ajax({               
                url: Utils.getServerApiUrl("Rregjistrime", "ruajNeSessionTrupInfoArt"),
                data: JSON.stringify({ idkoka: -1 })
            }).done(Succedcallback);
        }
        else if (Utils.ktheKontroll('Lloji').GetValue() == "2") {
            Utils.ktheKontroll('Periudha').SetEnabled(false); Utils.ktheKontroll('Periudha').SetSelectedIndex(0);
            $('#hfId').val(-2);
            $.ajax({
                url: Utils.getServerApiUrl("Rregjistrime", "ruajNeSessionTrupInfoArt"),
                data: JSON.stringify({ idkoka: -2 })
            }).done(Succedcallback);
        } else if (Utils.ktheKontroll('Lloji').GetValue() == "3") {
            Utils.ktheKontroll('Periudha').SetEnabled(false); Utils.ktheKontroll('Periudha').SetSelectedIndex(0);
            $('#hfId').val(-3);
            $.ajax({
                url: Utils.getServerApiUrl("Rregjistrime", "ruajNeSessionTrupInfoArt"),
                data: JSON.stringify({ idkoka: -3 })
            }).done(Succedcallback);
        }
        else {
            myMesazh.ShtoMesazhGabimi('Lloji nuk mund te jete bosh');
            Utils.ktheKontroll('Lloji').SetSelectedIndex(1);
            Utils.ktheKontroll('Periudha').SetSelectedIndex(1); Utils.ktheKontroll('Periudha').SetEnabled(true);
            $('#hfId').val(-1);
            $.ajax({
                url: Utils.getServerApiUrl("Rregjistrime", "ruajNeSessionTrupInfoArt"),
                data: JSON.stringify({ idkoka: -1 })
            }).done(Succedcallback);
        }
    }
}
function SelectionChangedPeriudha() {
    if (ASPxGridView_InfoArtikulli.IsEditing()) {
        if (Utils.ktheKontroll('Periudha').GetValue() == -1 && Utils.ktheKontroll('Lloji').GetValue() == 1) {
            myMesazh.ShtoMesazhGabimi('Periudha nuk mund te jete bosh per llojin Artikull');
            Utils.ktheKontroll('Periudha').SetSelectedIndex(1);
        }
    }
}

function EndCallbackGrida(s, e) {
    if ($('#hfRuaj').val() == 'Modifiko') {
        Kodi.SetEnabled(false); Lloji.SetEnabled(false);
    }
    else if ($('#hfRuaj').val() == 'Ruaj') {
        Kodi.SetEnabled(true); Lloji.SetEnabled(true);
    }
  
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "merrMesazhNgaSesioni"),
        data: JSON.stringify({})
    }).done(SucceededCallbackMesazhi);
}

function SucceededCallbackMesazhi(result) {
    var arr = result.split(':');
    if (arr[1] == "Green") {
        ASPxMenu1.GetItemByName('Ruaj').SetVisible(false);
        myMesazh.ShtoMesazhSuksesi(arr[0]);
    }
    else if (arr[1] == "Red")
    { myMesazh.ShtoMesazhGabimi(arr[0]);}
}