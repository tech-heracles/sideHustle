;
function checkText(s, e) {
    myMenu.checkText(s, e);
}

var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvLupaBanka, "612", '');
}

function textChanged(s, e) {
    myMenu.textChanged(s, e);
}

function BeginCallback(s, e) {
    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}

$(window).on('unload', function () {
});

function Init() {
    try {
        myFaqeCelje.shtoHandlerSession();
        gvLupaBanka.SetFocusedRowIndex(0);
        btnOk.Focus();
    }
    catch (err) {
    }
}

document.onkeydown = ProcessKeyPress;

function ProcessKeyPress() {
    var currentIndex = gvLupaBanka.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaBanka.GetVisibleRowsOnPage() - 1) {
            gvLupaBanka.SetFocusedRowIndex(0);
        }
        else {
            gvLupaBanka.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaBanka.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();
    }
}

function OnGridSelectionChanged() {
    gvLupaBanka.GetSelectedFieldValues( 'KodiBanka;IdMonedhaBanka;IdDegeAdministrative;IdBanka;EmerBanka', OnGridSelectionComplete);
}

function OnGridSelectionComplete(values) {
    var banka = ''; banka = values[0][0];
    var monedha = ''; monedha = values[0][1];
    var dege = values[0][2];
    var idBanka = values[0][3];
    var emerBanka = values[0][4];
    if (Utils.getUrlVar('vjenNga') === 'Punonjes') {
        window.parent.cmbBanka.SetSelectedIndex(window.parent.cmbBanka.AddItem(values[0], values[3]));
        window.parent.cmbBanka.SetFocus(true);
    }
    else
        if (Utils.getUrlVar('vjenNga') === 'RegjDokumentash') {

            window.parent.btneArka.SetText(banka);
            window.parent.btneArka.SetFocus(true);
            window.parent.TextChangedArka();
        }
        if (window.parent.identifikuesPerPopupBanka == "raportbanka") {

            if (values.length > 1) {
                for (i = 1; i < values.length; i++)
                    banka = banka + "," + values[i][0];
            } 
            window.parent.editorGlobal.SetText(banka);
            window.parent.editorGlobal.SetFocus(true);
        }
        else if (window.parent.identifikuesPerPopupBanka == "KonfigurimDokumentash") {
            window.parent.editorKF.SetValue(idBanka);
            window.parent.editorKF.SetText(banka);
            window.parent.editorKF.SetFocus(true);
          
        }
        else if (window.parent.identifikuesPerPopupBanka == "Import") {
            window.parent.editorGlobal.SetText(banka);
            window.parent.editorGlobal.SetFocus(true);
        }
        else if (window.parent.identifikuesPerPopup == "Shto_KlientFurnitor") {
            window.parent.txtEmriBanka.SetText(banka);
            window.parent.txtEmriBanka.SetFocus(true);
            window.parent.TextChangedBanka();
        }
        else if (window.parent.identifikuesPerPopupBanka == "ShtoVeprimBanka") {
            window.parent.ndryshokurs = true;
            Utils.SelectComboItem(window.parent.banka_ComboBox, idBanka, banka, emerBanka);
            window.parent.banka_ComboBox.SetFocus(true);
            window.parent.cmbDegeAdministrative.SetValue(dege);
            window.parent.TextChangedBanka();
        }
        else if (window.parent.identifikuesPerPopupBanka == "PerfitimBuxheti"){
            var arka = new Array(banka, emerBanka);
            window.parent.cmbEntiteti.SetSelectedIndex(window.parent.cmbEntiteti.AddItem(arka, idBanka));
            window.parent.cmbEntiteti.Focus();
        }
        else if (window.parent.identifikuesPerPopupBanka == "GjendjeArkeDitore") {
            window.parent.cmbArka.SetSelectedIndex(window.parent.cmbArka.AddItem(new Array(banka, emerBanka), idBanka));
            window.parent.cmbArka.SetFocus(true);
        }
    
        //else {
        //    window.parent.ndryshokurs = true;
        //    window.parent.banka_ComboBox.SetText(banka);
        //    window.parent.banka_ComboBox.SetFocus(true);
        //    window.parent.cmbDegeAdministrative.SetValue(dege);
        //    window.parent.TextChangedBanka();
        //}
    window.parent.popupUniversal.Hide();
}


function menu_click(s, e) {
    if (e.item.name == "OK") {
        e.processOnServer = false;
        OnGridSelectionChanged();
    }
    else if (e.item.name == 'Anullo') {
        window.parent.popupUniversal.Hide();
    }
}

//metoda per te shfaqur popupin e filtrave
function Filtra_Click() {
    document.getElementById('<%= Container.ClientID %>').src = 'LupaFiltra.aspx?grida=gvLupaBanka&page=LupaBanka.aspx&idKonfigAmbjente=577';
    popFiltra.Show();
}

function gup(name) {
    name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regexS = "[\\?&]" + name + "=([^&#]*)";
    var regex = new RegExp(regexS);
    var results = regex.exec(window.location.href);
    if (results == null)
        return "";
    else
        return results[1];
}

$(window).load(function () {
    try {
        //$("#div")[0].style.visibility = 'visible';
        $("#div").show();
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaBanka.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});

$(window).bind('resize', function () {//po
    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaBanka.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');