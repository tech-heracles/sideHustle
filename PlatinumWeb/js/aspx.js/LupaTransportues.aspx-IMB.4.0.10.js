function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvLupaTransportues, "914", '');
}

function textChanged(s, e) {
    myMenu.textChanged(s, e);
}

function BeginCallback(s, e) {
    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}

$(window).load(function () {
    try {
        $("#div").show();//$("#div")[0].style.visibility = 'visible';
       
        Init();
    }
    catch (e) {
    }
});

$(window).bind('resize', function () {//po
    try {
      
    }
    catch (e) {
    }
}).trigger('resize');

$(window).unload(function () {
})

function Init() {
    try {
        myFaqeCelje.shtoHandlerSession();
        gvLupaTransportues.SelectRowOnPage(0, true);
        btnOk.Focus();
    }
    catch (err) {
       
    }
}
document.onkeydown = ProcessKeyPress;
function ProcessKeyPress() {
    var currentIndex = gvLupaTransportues.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaTransportues.GetVisibleRowsOnPage() - 1) {
            gvLupaTransportues.SetFocusedRowIndex(0);
        }
        else {
            gvLupaTransportues.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaTransportues.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();

    }
}

function OnGridSelectionChanged() {
    gvLupaTransportues.GetSelectedFieldValues('IdTransportues;Emertimi;Nipt;Adresa;Tel;IdStatusDok;IdKrijues;IdNdermarrje;IdPerdorues;DtKrijimi;DtModifikimi', OnGridSelectionComplete);
}
var hide = true;
var kodi;
function OnGridSelectionComplete(value) {
    if (value.length == 0) {
        alert("Selektoni nje rresht");
        return;
    }
    hide = true;
     var emri = ''; var nipt = ''; var id = ''; var adresa = ''; var tel = '';
    if (value.length > 1) {
        for (i = 0; i < value.length - 1; i++) {
            var values = value[i];
          
            id = id + values[0] + ",";
        }
        values = value[value.length - 1];
       
        id = id + values[0];
    }
    else {

        values = value[0];
        id = values[0];
        emri = values[2];
        nipt = values[1];
        adresa = values[4];
        tel = values[5];       
    }
    switch (window.parent.identikuesPerPopupTransportuesi) {
       
        case "RegjistrimDokumentash":
            if (value.length > 1) {
                alert('Nuk mund te zgjidhni me shume se nje Transportues');
                hide = false;
            }
            else {
                if (window.parent.btnTransportues.GetText() != '') {
                    window.parent.btnTransportues.SetText('');
                    window.parent.btnTransportues.SetSelectedIndex(-1);
                }
                window.parent.btnTransportues.SetValue(values[0]);
                window.parent.btnTransportues.SetText(values[1]);
                window.parent.btnTransportues.Focus(true);                
            }
            break;
        case "Import":
            window.parent.editorGlobal.SetText(values[1]);
            window.parent.editorGlobal.SetFocus(true);
            break;        
        default:
            break;
    }
    if (hide) {
        window.parent.popupUniversal.Hide();
    }
}

function menu_click(s, e) {
    switch (e.item.name) {
        case "OK":
            e.processOnServer = false;
            OnGridSelectionChanged();
            break;
        case "Anullo":
            e.processOnServer = false;
            window.parent.popupUniversal.Hide();
            break;
        case 'Shto':
            e.processOnServer = false;
            if (window.parent.identikuesPerPopupTransportuesi == 'RegjistrimDokumentash') {
                window.parent.myButtonClickLupa.LupaUniversal_Click('Shto Transportues', 'Shto_Transportues.aspx?lupe=true', 850, 600);
            }
            break;
    }
 }


//function Succeded() {
//    window.parent.myButtonClickLupa.LupaUniversal_Click('Shto Transportues', 'LupaTransportuesShpejte.aspx', document.documentElement.clientWidth, document.documentElement.clientHeight);
//}

function onNdryshimFokusi() {
    try {
        if (PageControl.GetActiveTabIndex() == 0)
            mbush = true;
    } catch (e) { }
}

//metoda per te shfaqur popupin e filtrave
function Filtra_Click() {
    popFiltra.SetHeaderText('Zgjidh Filtrin');
    popFiltra.SetSize(500, 350);
    popFiltra.SetContentUrl('LupaFiltra.aspx?grida=gvLupaTransportues&page=Shto_Transportues.aspx?lupe=true');
      
        
    popFiltra.Show();
}
