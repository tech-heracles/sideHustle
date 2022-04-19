function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
window.parent.identifikuesPerPopupKodifikimin = "Shto_KlientFurnitor";
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvLupaGrupKlientFurnitor, "663", '');
}

function textChanged(s, e) {
    myMenu.textChanged(s, e);
}

function BeginCallback(s, e) {
    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}

function Init() {
    myMesazh.shtoHandler();
    try {
        myFaqeCelje.shtoHandlerSession();

        gvLupaGrupKlientFurnitor.SetFocusedRowIndex(0);
        btnOk.Focus();
    }
    catch (err) {
        ///alert('gabim');    
    }
}
var faqe = 'kodifikime';
document.onkeydown = ProcessKeyPress;
function ProcessKeyPress() {
    var currentIndex = gvLupaGrupKlientFurnitor.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaGrupKlientFurnitor.GetVisibleRowsOnPage() - 1) {
            gvLupaGrupKlientFurnitor.SetFocusedRowIndex(0);
        }
        else {
            gvLupaGrupKlientFurnitor.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaGrupKlientFurnitor.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();

    }
}

function zgjidhElement() {
    if (window.parent.identifikuesPerPopupKodifikimin == "Shto_KlientFurnitor" || window.parent.identifikuesPerPopupKodifikimin == "KonfigurimDokumentash") {
//        if (gvLupaGrupKlientFurnitor.GetSelectedRowCount() == 1)
//            gvLupaGrupKlientFurnitor.GetRowValues(gvLupaGrupKlientFurnitor.GetFocusedRowIndex(), 'IdGrupi;KodGrupi;PershkrimGrupi;NivelGrupi', OnCompletePerWebServiceOneRow);
//        else
        gvLupaGrupKlientFurnitor.GetSelectedFieldValues('IdGrupi;KodGrupi;PershkrimGrupi;NivelGrupi', OnCompletePerWebService);
    }
    else {
        OnGridSelectionChanged();
        pergjigja.SetText("");
    }
}

function OnGridSelectionChanged() {
    gvLupaGrupKlientFurnitor.GetSelectedFieldValues('IdGrupi;KodGrupi;PershkrimGrupi;NivelGrupi', OnGridSelectionComplete);
    //gvLupaGrupKlientFurnitor.GetRowValues(gvLupaGrupKlientFurnitor.GetFocusedRowIndex(), 'IdGrupi;KodGrupi;PershkrimGrupi;NivelGrupi', OnGridSelectionComplete);
}

function OnCompletePerWebService(values) {
    if (values != null && values.length != 0) {
        $.ajax({
            url: Utils.getServerApiUrl("Konfigurime", "eshteKodifikimiKFPrind"),
            data: JSON.stringify({ input: values[0][0], idNdermarrje:hfState.Get('idNdermarrje') })
        }).done(SucceededCallback);
    }
    else
        myMesazh.ShtoMesazhGabimi("Zgjidhni të paktën një rresht!");
}


//function OnCompletePerWebServiceOneRow(values) {
//    if (values != null && values.length != 0) {
//        PlatinumWeb.wsfunc.eshteKodifikimiKFPrind(values[0], SucceededCallback);
//    }
//    else
//        myMesazh.ShtoMesazhGabimi("Zgjidhni të paktën një rresht!");
//}

//function OnCompletePerWebServiceMultipleRows(values) {
//    if (values != null && values.length != 0) {
//        var id = '';
//        for (i = 0; i < values.length; i++) {
//            if (values[i][0] != undefined)
//                id = id + ';' + values[i][0];
//        }
//        PlatinumWeb.wsfunc.janeKodifikimetKFPrind(id, SucceededCallback);
//    }
//    else
//        myMesazh.ShtoMesazhGabimi("Zgjidhni të paktën një rresht!");
//}

function SucceededCallback(result) {
    if (result == "true" || result == true) {
        myMesazh.ShtoMesazhGabimi("Nuk mund të zgjidhni një grup i cili është prind!");
    }
    else {
        OnGridSelectionChanged();
    }
}

function OnGridSelectionComplete(values) {
    var s = new String();
    //s += values[0];
    s = s + values[0];
    var vl = s.split(",");
    var kodi = '';
    for (var i = 0; i < values.length; i++) {
        if (kodi == '')
            kodi = values[i][1];
        else
            kodi = kodi + ',' + values[i][1];
    }

    if (window.parent.identifikuesPerPopupKodifikimin == "Kodifikim") {
        window.parent.editorPrind.SetText(vl[2]);
        var hf = window.parent.document.getElementById("hfPrindi");
        hf.value = vl[1];
        window.parent.editorNiveli.SetText(parseInt(vl[3]) + 1);
        window.parent.editorPrind.SetFocus(true);
    }
    else if (window.parent.identifikuesPerPopupKodifikimin == "Shto_KlientFurnitor") {
        if (window.parent.grida == 'grida') {
            window.parent.editorKodifikime.SetText(vl[1]);
            window.parent.editorKodifikime.SetFocus(true);
        }
        else if (window.parent.grida == '1') {
            window.parent.btneGrupimi1.SetText(vl[1]);
            window.parent.btneGrupimi1.SetFocus(true);
        }
        else if (window.parent.grida == '2') {
            window.parent.btneGrupimi2.SetText(vl[1]);
            window.parent.btneGrupimi2.SetFocus(true);
        }
        else if (window.parent.grida == '3') {
            window.parent.btneGrupimi3.SetText(vl[1]);
            window.parent.btneGrupimi3.SetFocus(true);0
        }
    }
    else if (window.parent.identifikuesPerPopupKodifikimin == "raportGrupim") {
        window.parent.editorGlobal.SetText(kodi);
        window.parent.editorGlobal.SetFocus(true);
    }
    else if (window.parent.identifikuesPerPopupKodifikimin == "Skema") {
        window.parent.editorGlobal.SetText(kodi);
        window.parent.editorGlobal.SetFocus(true);
        window.parent.window['NiveliApr' + window.parent.keyGlobal].SetEnabled(true);
    }
    else if (window.parent.identifikuesPerPopupKodifikimin == "Import") {
        window.parent.editorGlobal.SetText(vl[1]);
        window.parent.editorGlobal.SetFocus(true);
    }
    else if (window.parent.identifikuesPerPopupKodifikimin == "KonfigurimDokumentash") {
        window.parent.editorGrupKlientFurnitor.SetText(vl[1]);
        window.parent.editorGrupKlientFurnitor.SetFocus(true);
    }
    window.parent.popupUniversal.Hide();
}

function menu_click(s, e) {
    //    if (e.item.name == 'Filtra')
    //        popZgjidhFiltrin.Show();
    //    if (e.item.name == 'Ruaj')
    //        popRuaj.Show();
    if (e.item.name == "OK") {
        e.processOnServer = false;
        //OnGridSelectionChanged();
        zgjidhElement();
    }
    else if (e.item.name = "Anullo") {
        window.parent.popupUniversal.Hide();
    }
}

$(window).load(function () {
    try {
        $("#div").show();//$("#div")[0].style.visibility = 'visible';
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaGrupKlientFurnitor.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});
$(window).bind('resize', function () {//po
    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaGrupKlientFurnitor.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');


//metoda per te shfaqur popupin e filtrave
function Filtra_Click() {
    popFiltra.SetHeaderText('Zgjidh Filtrin');
    popFiltra.SetSize(500, 350);
    popFiltra.SetContentUrl('LupaFiltra.aspx?grida=gvLupaGrupKlientFurnitor&page=LupaGrupimeKlientFurnitor.aspx');
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
    