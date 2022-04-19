function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvLupaKpf, "625", '');
}

function textChanged(s, e) {
    myMenu.textChanged(s, e);
}

function BeginCallback(s, e) {
    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}

$(window).on('unload',function () {
});
function Init() {
    try {
        myFaqeCelje.shtoHandlerSession();
        gvLupaKpf.SetFocusedRowIndex(0); gvLupaArtikull.SelectRowOnPage(0, true);
        btnOkKPF1.Focus();
    }
    catch (err) {
        ///alert('gabim');    
    }
}

document.onkeydown = ProcessKeyPress;

function ProcessKeyPress() {
    var currentIndex = gvLupaKpf.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaKpf.GetVisibleRowsOnPage() - 1) {
            gvLupaKpf.SetFocusedRowIndex(0);
        }
        else {
            gvLupaKpf.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaKpf.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();

    }
}
function OnCompletePerWebService(values) {
    $.ajax({      
        url: Utils.getServerApiUrl("Konfigurime", "eshteKPFPrind"),
        data: JSON.stringify({ kodi: values[0][0], niveli: values[0][1], grupi: Utils.getUrlVar("id"), idNdermarrje: hfState.Get('idNdermarrje') })
    }).done(SucceededCallback);
}

function SucceededCallback(result) {
    if (result == true) {
        pergjigja.SetText("Kjo llogari standarte nuk mund te zgjidhet sepse eshte prind! Ju lutem zgjidhni nje tjeter!");
    }
    else {
        OnGridSelectionChanged();
    }
}

function zgjidhElement() {
    if (window.parent.identifikuesperKPF == "Shto_Llogari.aspx") {
        gvLupaKpf.GetSelectedFieldValues('KodiKPF;NiveliKPF;EmertimiKPF', OnCompletePerWebService);
    }
    else {
        OnGridSelectionChanged();
        pergjigja.SetText("");

    }
}

function OnGridSelectionChanged() {
    //gvLupaKpf.GetSelectedFieldValues('KodiKPF', OnGridSelectionComplete);
    gvLupaKpf.GetRowValues(gvLupaKpf.GetFocusedRowIndex(), 'KodiKPF;NiveliKPF;EmertimiKPF', OnGridSelectionComplete);
}

function OnGridSelectionComplete(values) {
    var txtkodikpf;
    txtkodikpf = '';
    txtkodikpf = values[0];
    if (Utils.getUrlVar('vjennga') === "KonfigPash") {
        window.parent.editorGlobal.val(values[0]);
        window.parent.editorPershkrimillogaria.val(values[2]);
        window.parent.editorGlobal.focus();
    }
    else {
        if (window.parent.identifikuesperKPF == "Import") { //kur thirret nga celja e shpejte e llogarive
            window.parent.editorGlobal.SetText(txtkodikpf);
            window.parent.editorGlobal.SetFocus(true);
        }
        else if (window.parent.identifikuesperKPF == "LlogariShpejte") { //kur thirret nga celja e shpejte e llogarive
            window.parent.cmbKpf1.SetValue(txtkodikpf);
            window.parent.cmbKpf1.SetFocus(true);
        }
        else {
            if (window.parent.arrLL != null) {
                window.parent.editorGlobal.val(values[0]);
                window.parent.editorPershkrimillogaria.val(values[2]);
                //  window.parent.editorGjendja.SetValue("Gjithmone");
                //  window.parent.editorShenja.SetValue("Pozitive");
                window.parent.editorGlobal.focus();
            }
            else if (window.parent.txtLlog == "kpf") {
                window.parent.cmbPrindi.SetText(txtkodikpf);
                window.parent.txtKodi.SetText(txtkodikpf);
                window.parent.kodi_TextBox.SetText(txtkodikpf);
                window.parent.cmbNiveleKPF.SetText(parseInt(values[1]) + 1);
                window.parent.txtKodiLlog.SetText(txtkodikpf);
                window.parent.cmbPrindi.SetFocus(true);
            }
            else if (window.parent.grida != false) {//rasti kur thirret nga grida
                if (window.parent.txtLlog.GetText() == 'KPF1') {
                    window.parent.editorKPFD.SetValue(txtkodikpf);
                    window.parent.editorKPFD.SetFocus(true);
                }
                else if (window.parent.txtLlog.GetText() == 'KPF1K') {
                    window.parent.editorKPFK.SetValue(txtkodikpf);
                    window.parent.editorKPFK.SetFocus(true);
                }
            }

            else {//rasti kur thirret nga textboxet
                if (window.parent.txtLlog.GetText() == 'KPF1') {
                    window.parent.FSC_1_Debi_TextBox.SetText(txtkodikpf);
                    window.parent.FSC_1_Debi_TextBox.SetFocus(true);
                }
                else if (window.parent.txtLlog.GetText() == 'KPF2') {
                    window.parent.FSC_2_Debi_TextBox.SetText(txtkodikpf);
                    window.parent.FSC_2_Debi_TextBox.SetFocus(true);
                }
                else if (window.parent.txtLlog.GetText() == 'KPF3') {
                    window.parent.FSC_3_Debi_TextBox.SetText(txtkodikpf);
                    window.parent.FSC_3_Debi_TextBox.SetFocus(true);
                }
            }
        }
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
    document.getElementById('<%= Container.ClientID %>').src = 'LupaFiltra.aspx?grida=gvLupaKpf&page=LupaKpf.aspx&idKonfigAmbjente=577';
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
        gvLupaKpf.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});
$(window).bind('resize', function () {//po
    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaKpf.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');
