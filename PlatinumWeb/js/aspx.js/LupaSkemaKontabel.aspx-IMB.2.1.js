function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvLupaSkemaKont, "645", '');
}

function textChanged(s, e) {
    myMenu.textChanged(s, e);
}

function BeginCallback(s, e) {
    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}

function Init() {
    try {
        myFaqeCelje.shtoHandlerSession();
        gvLupaSkemaKont.SetFocusedRowIndex(0);
        btnOk.Focus();
    }
    catch (err) {
        ///alert('gabim');    
    }
}

document.onkeydown = ProcessKeyPress;

function ProcessKeyPress() {
    var currentIndex = gvLupaSkemaKont.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaSkemaKont.GetVisibleRowsOnPage() - 1) {
            gvLupaSkemaKont.SetFocusedRowIndex(0);
        }
        else {
            gvLupaSkemaKont.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaSkemaKont.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();

    }
}
function OnGridSelectionChanged() {
    //gvLupaSkemaKont.GetSelectedFieldValues('KodiSkemaKontabelKoka', OnGridSelectionComplete);
    gvLupaSkemaKont.GetRowValues(gvLupaSkemaKont.GetFocusedRowIndex(), 'KodiSkemaKontabelKoka', OnGridSelectionComplete);
}
function OnGridSelectionComplete(values) {
    var txtskemekontabel;
    txtskemekontabel = '';

    //        for (var i = 0; i < values.length; i++) {
    //            if (txtskemekontabel == '')
    //                txtskemekontabel = values[values.length - 1];
    //            else
    //                txtskemekontabel = txtskemekontabel + ',' + values[i];
    //        }
    ////        alert(window.parent.editorGlobal);

    txtskemekontabel = values;

    if (window.parent.editorGlobal != null) {
        window.parent.editorGlobal.SetText(txtskemekontabel);
        window.parent.editorGlobal.Focus();
    }
    else {
        window.parent.lblKodiSkemaKontabel.SetText(txtskemekontabel);

    }
    window.parent.popupUniversal.Hide();
}


function menu_click(s, e) {
//    if (e.item.name == 'Filtra')
//        popZgjidhFiltrin.Show();
//    if (e.item.name == 'Ruaj')
//        popRuaj.Show();
    switch (e.item.name) {
        case "OK":
            e.processOnServer = false;
            OnGridSelectionChanged();
            break;
        case "Anullo":
            e.processOnServer = false;
            window.parent.popupUniversal.Hide();
            break;
    }
}

//metoda per te shfaqur popupin e filtrave
function Filtra_Click() {
    document.getElementById('<%= Container.ClientID %>').src = 'LupaFiltra.aspx?grida=gvLupaSkemaKont&page=LupaSkemaKontabel.aspx&idKonfigAmbjente=577';
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
        $("#div").show();//$("#div")[0].style.visibility = 'visible';
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaSkemaKont.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});
$(window).bind('resize', function () {//po

    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaSkemaKont.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');
