function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvLupaDok, "615", '');
}

function textChanged(s, e) {
    myMenu.textChanged(s, e);
}

function BeginCallback(s, e) {
    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
    Utils.shfaqLoadingGif();
}

function EndCallback(s, e) {
    Utils.hiqLoadingGif();
    if (!gvLupaDok.cpMesazh)
        return;

    var mesazh = JSON.parse(gvLupaDok.cpMesazh);
    if (mesazh.Status)
        myMesazh.ShtoMesazhSuksesi(mesazh.PershkrimMesazhi);
    else
        myMesazh.ShtoMesazhGabimi(mesazh.PershkrimMesazhi);
    delete gvLupaDok.cpMesazh;
}

$(window).on('unload',function () {
});
$(window).load(function () {
    try {
        $("#div").show();//$("#div")[0].style.visibility = 'visible';
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaDok.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});
$(window).bind('resize', function () {//po

    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaDok.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');
var identikuesPerPopupKlientFurnitori = "Dokumenta";
function Init() {

    myFaqeCelje.shtoHandlerSession();
    gvLupaDok.SetFocusedRowIndex(0);
    window.parent.Utils.hiqLoadingGif();
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    //  btnOk.Focus();

    //  alert(window.parent.document.documentElement.clientHeight);
    //   alert(document.documentElement.clientHeight);
    if (rbNga.GetChecked() == false) {
        dteNga.SetEnabled(false);
        dteDeri.SetEnabled(false);
    }
    else {
        dteNga.SetEnabled(true);
        dteDeri.SetEnabled(true);
    }


}

function EndRequestHandler(sender, args) {

    Utils.hiqLoadingGif();
}
document.onkeydown = ProcessKeyPress;
function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ?
												            evt.keyCode : evt.charCode;
}
function KeyPresKF(kodi, editor, key) {//kur shtypet nje key per kolonen KPF debi
    if (kodi == 13) {
        indeksi = key;
        editorKF = Utils.ktheKontroll('IdKlientFurnitori');
        KK_Click();
    }
}

function LostFocusKF(key) {//kur humb fokusin kolona KPF debi
    indeksi = key;
    editorKF = Utils.ktheKontroll('IdKlientFurnitori');
    vlera = 0;


}
function ButtonClickedKF(editor, key) {//kur klikon butonin e kolones KPF debi
    indeksi = key;
    editorKF = editor;
    KF_Click();


}
var editorKF;
function TextChangedKF(key) {//kur ndryshon texti tek kolona KPF debi
    indeksi = key;
    var editor = Utils.ktheKontroll('IdKlientFurnitori');

    editorKF = editor;

}
function KF_Click() {
    var editorniveli = Utils.ktheKontroll('IdNiveli');
    popupUniversal.SetHeaderText('Zgjidh Klient/furnitorin');
    if (editorniveli.GetText() == 'FB' || editorniveli.GetText() == 'KB' || editorniveli.GetText() == 'OB' || editorniveli.GetText() == 'UB')
        document.getElementById('<%= Container.ClientID %>').src = 'LupaKlientFurnitor.aspx?veprimi=1';
    else if (editorniveli.GetText() == 'FSH' || editorniveli.GetText() == 'KSH' || editorniveli.GetText() == 'OSH' || editorniveli.GetText() == 'USH')

        document.getElementById('<%= Container.ClientID %>').src = 'LupaKlientFurnitor.aspx?veprimi=2';
    else
        document.getElementById('<%= Container.ClientID %>').src = 'LupaKlientFurnitor.aspx';

    popupUniversal.Show();
}
function ProcessKeyPress() {
    var currentIndex = gvLupaDok.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaDok.GetVisibleRowsOnPage() - 1) {
            gvLupaDok.SetFocusedRowIndex(0);
        }
        else {
            gvLupaDok.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaDok.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();

    }
}
function buttonClick() {
    vlera = 0;
    if (rbAktuale.GetChecked() == true)
        vlera = 1;
    if (rbVitiUshtrimor.GetChecked() == true)
        vlera = 2;
    if (rbNga.GetChecked() == true)
        vlera = "3";//:" + dteNga.GetText() + ":" + dteDeri.GetText();
    gvLupaDok.PerformCallback(vlera);
}

function OnGridSelectionChanged() {
    if ((window.parent.identifikuesPerPopupDokumentat == "ShperndarjeShpenzimeshKerko"))
        gvLupaDok.GetSelectedFieldValues('IdDokumenti;Vlefta;NrDokumenti', OnGridSelectionCompleteMultiSelect);
    else

        gvLupaDok.GetRowValues(gvLupaDok.GetFocusedRowIndex(), 'IdDokumenti;NrDokumenti;Pershkrimi;IdNiveli', OnGridSelectionComplete);
}

var id;
function OnGridSelectionComplete(values) {
    id = values[0];
    var nivel = values[3];
    var kategori = 0;

    if (id == "" || id == null) {
        window.parent.myMesazh.ShtoMesazhGabimi('Nuk keni asnje dokument te zgjedhur!');
        window.parent.popupUniversal.Hide();
        return;
    }
    if (window.parent.identifikuesPerPopupDokumentat == "ShtoVeprimBanka.aspx")
        window.parent.location = 'ShtoVeprimBanka.aspx?lloji=' + Utils.getUrlVar('lloji') + '&id=' + id + '&shtim_modifikim=modifikim';
    if (window.parent.identifikuesPerPopupDokumentat == "VeprimeBankaPaprintuar") {
        window.parent.hapLupeValidimi = false;
        window.parent.location = 'ShtoVeprimBanka.aspx?lloji=' + Utils.getUrlVar('lloji') + '&id=' + id + '&shtim_modifikim=modifikim';
    }
    if (window.parent.identifikuesPerPopupDokumentat == "FleteDoganore.aspx?lloji=import") {
        window.parent.location = 'Shto_FleteDoganore.aspx?lloji=import&id=' + id + '&shtim_modifikim=modifikim';
    } if (window.parent.identifikuesPerPopupDokumentat == "FleteDoganore.aspx?lloji=export") {
        window.parent.location = 'Shto_FleteDoganore.aspx?lloji=export&id=' + id + '&shtim_modifikim=modifikim';
    }
    if (window.parent.identifikuesPerPopupDokumentat === "ListPagesa") {
        window.parent.location = 'Shto_ListPagesa.aspx?id=' + id + '&shtim_modifikim=modifikim';
    } 
     if (window.parent.identifikuesPerPopupDokumentat === "QendraKosto") {
        window.parent.location = 'Shto_RegjistrimQendraKosto.aspx?shtim_modifikim=modifikim&id=' + id ;
    } 
    if (window.parent.identifikuesPerPopupDokumentat === "Planifikim") {
        window.parent.location = 'Shto_Planifikim.aspx?id=' + id + '&shtim_modifikim=modifikim';
    }
    if (window.parent.identifikuesPerPopupDokumentat === "GjeneroProjekt") {
        window.parent.btneNrUrdherShitje.SetText(values[1]);
    }
    if (window.parent.identifikuesPerPopupDokumentat === "EkzekutimProdhimi") {
        window.parent.location = 'Shto_Ekzekutim.aspx?id=' + id + '&shtim_modifikim=modifikim';
    } if (window.parent.identifikuesPerPopupDokumentat === "RivleresimeAmortizimi") {
        var lloji = window.parent.cmbLloji.GetText();
        if(lloji=="AMFI")
            window.parent.location = 'Shto_RivleresimeAmortizimi.aspx?lloj=amortizim&id=' + id + '&shtim_modifikim=modifikim';
        else  window.parent.location = 'Shto_RivleresimeAmortizimi.aspx?lloj=rivleresim&id=' + id + '&shtim_modifikim=modifikim';
    }
    if (window.parent.identifikuesPerPopupDokumentat === "Amortizimi") {
        window.parent.location = 'Shto_RegjistrimAmortizimi.aspx?id=' + id + '&shtim_modifikim=modifikim';
    }
    if (window.parent.identifikuesPerPopupDokumentat === "UrdherPagesa") {
        window.parent.location = 'Shto_UrdherPagesa.aspx?id=' + id + '&shtim_modifikim=modifikim';
    }
    if (window.parent.identifikuesPerPopupDokumentat == "Shto_RegjistrimDokumentash.aspx") {
        var arr = document.getElementById("kategori").value.split(',');
        for (i = 0; i < arr.length; i++) {
            var val = arr[i].split(":");
            if (val[0] == nivel)
                kategori = val[1];
        }
        if (kategori == 1)
            window.parent.location = "Shto_RegjistrimDokumentash.aspx?shitje_blerje=shitje&id=" + id + "&shtim_modifikim=modifikim";
        else if (kategori == 2)
            window.parent.location = "Shto_RegjistrimDokumentash.aspx?shitje_blerje=blerje&id=" + id + "&shtim_modifikim=modifikim";
    }
    if (window.parent.identifikuesPerPopupDokumentat == "Shto_RegjistrimDokumentashPaPrint.aspx") {
        window.parent.hapLupeValidimi = false;
        window.parent.location = "Shto_RegjistrimDokumentash.aspx?shitje_blerje=shitje&id=" + id + "&shtim_modifikim=modifikim";

    }
    else if (window.parent.identifikuesPerPopupDokumentat == "Shto_FleteKontabel.aspx") {

        window.parent.location = "Shto_FleteKontabel.aspx?shtim_modifikim=modifikim&id=" + id;


    }
    else if (window.parent.identifikuesPerPopupDokumentat == "Shto_RegjistrimMagazine.aspx") {
        var lloji = window.parent.cmbLloji.GetText();
        if (lloji == 'FH' || lloji == 'UH')
            window.parent.location = "Shto_RegjistrimMagazine.aspx?lloj=hyrje&id=" + id + "&shtim_modifikim=modifikim";

        else if (lloji == 'FD' || lloji == 'UD')
            window.parent.location = "Shto_RegjistrimMagazine.aspx?lloj=dalje&id=" + id + "&shtim_modifikim=modifikim";

    }
    else if (window.parent.identifikuesPerPopupDokumentat == "Shto_RegjistrimNdryshimCmimSasi.aspx") {
        var lloji = window.parent.cmbLloji.GetText();

        window.parent.location = "Shto_RegjistrimNdryshimCmimSasi.aspx?shtim_modifikim=modifikim&id=" + id;

    } else if (window.parent.identifikuesPerPopupDokumentat == "Shto_RegjistrimInventarizimi.aspx") {
        var lloji = window.parent.cmbLloji.GetText();
        if (lloji == 'IAASH')
            window.parent.location = "Shto_RegjistrimInventarizimi.aspx?lloj=ash&shtim_modifikim=modifikim&id=" + id;
        else if (lloji == 'IAAGJ')
            window.parent.location = "Shto_RegjistrimInventarizimi.aspx?lloj=agj&shtim_modifikim=modifikim&id=" + id;

    } else if (window.parent.identifikuesPerPopupDokumentat == "Shto_SkedulimProdhimi.aspx") {


        window.parent.location = "Shto_SkedulimProdhimi.aspx?shtim_modifikim=modifikim&id=" + id;

    }
    else if (window.parent.identifikuesPerPopupDokumentat == "Shto_RegjistrimRezervimi.aspx") {
        var lloji = window.parent.cmbLloji.GetText();
        if (lloji == 'RH')
            window.parent.location = "Shto_RegjistrimRezervimi.aspx?lloj=hyrje&id=" + id + "&shtim_modifikim=modifikim";

        else if (lloji == 'RD')
            window.parent.location = "Shto_RegjistrimRezervimi.aspx?lloj=dalje&id=" + id + "&shtim_modifikim=modifikim";

    }
    else if (window.parent.identifikuesPerPopupDokumentat == "Shto_VeprimeKF.aspx") {

        window.parent.location = "Shto_VeprimeKF.aspx?id=" + id + "&shtim_modifikim=modifikim";


    } else if (window.parent.identifikuesPerPopupDokumentat == "RegjistrimRiparime.aspx") {

        window.parent.location = "Shto_RegjistrimRiparimi.aspx?id=" + id + "&shtim_modifikim=modifikim";


    } else if (window.parent.identifikuesPerPopupDokumentat == "Shto_AzhornimKlientFurnitor.aspx?vep=azhornim") {

        window.parent.location = 'Shto_AzhornimKlientFurnitor.aspx?vep=azhornim&id=' + id + '&shtim_modifikim=modifikim';



    } else if (window.parent.identifikuesPerPopupDokumentat == "Shto_AzhornimKlientFurnitor.aspx?vep=mbyllje") {

        window.parent.location = 'Shto_AzhornimKlientFurnitor.aspx?vep=mbyllje&id=' + id + '&shtim_modifikim=modifikim';



    } else if (window.parent.identifikuesPerPopupDokumentat == "ShperndarjeShpenzimeshKerkoMenu") {

        window.parent.location = 'Shto_ShperndarjeShpenzimesh.aspx?id=' + id + '&shtim_modifikim=modifikim';



    }
    else if (window.parent.identifikuesPerPopupDokumentat == "LidhjaDokumentave") {

        window.parent.location = "LidhjaDokumentave.aspx?id=" + id + "&shtim_modifikim=modifikim";


    } else if (window.parent.identifikuesPerPopupDokumentat == "RecetaOptike.aspx")
        window.parent.location = "Shto_RecetaOptike.aspx?Id=" + id;
    window.parent.popupUniversal.Hide();
}

function OnGridSelectionCompleteMultiSelect(values) {
    var vlefta = '';
    var idFaturave = '';
    var nrDok = '';
    for (var i = 0; i < values.length; i++) {
        if (vlefta == '') {
            vlefta = values[i][1];
            idFaturave = values[i][0];
            nrDok = values[i][2];
        }
        else {
            vlefta = vlefta + ',' + values[i][1];
            idFaturave = idFaturave + ',' + values[i][0];
            nrDok = nrDok + ',' + values[i][2];
        }
    }

    if (window.parent.identifikuesPerPopupDokumentat == "ShperndarjeShpenzimeshKerko") {
        var vleftaTotale = 0;
        if (gvLupaDok.GetSelectedRowCount() > 1) {
            var vleftat = vlefta.split(',');
            for (i = 0; i < vleftat.length; i++)
            //vleftaTotale += parseFloat(vleftat[i].toString());
                vleftaTotale = vleftaTotale + parseFloat(vleftat[i].toString());
        }
        else if (gvLupaDok.GetSelectedRowCount() == 1)
            vleftaTotale = vlefta;
        if (window.parent.identifikuesPerPopupDokumentat == "ShperndarjeShpenzimeshKerko") {
            window.parent.txtVlera.SetText(vleftaTotale);
            window.parent.txtVlera.SetFocus(true);
        }
        else {
            window.parent.btnFatura.SetText(nrDok);
            window.parent.btnFatura.SetFocus(true);
        }
        window.parent.document.getElementById("hfIdFaturave").value = idFaturave;
    }
    window.parent.popupUniversal.Hide();
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
            //if (window.parent.identifikuesPerPopupDokumentat == "VeprimeBankaPaprintuar" || window.parent.identifikuesPerPopupDokumentat == "Shto_RegjistrimDokumentashPaPrint.aspx")
            //    window.parent.HapLupeValidimiPasLupesPaPrintuar();
            break;
        case "Printo":
            Utils.shfaqLoadingGif();
            e.processOnServer = false;
            gvLupaDok.PerformCallback("Printo;TeSelektuar");
            break;
    }
}

//metoda per te shfaqur popupin e filtrave
function Filtra_Click() {
    document.getElementById('<%= Iframe1.ClientID %>').src = 'LupaFiltra.aspx?grida=gvLupaDok&page=LupaDokumenta.aspx&idKonfigAmbjente=577';
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

function CheckedChanged_rbAktuale(s, e) {
    if(rbAktuale.GetChecked()==true)
    {rbVitiUshtrimor.SetChecked(false);
        rbNga.SetChecked(false);
        dteNga.SetEnabled(false);
        dteDeri.SetEnabled(false);  buttonClick();
    }
}
function CheckedChanged_rbVitiUshtrimor(s, e) {
    if(rbVitiUshtrimor.GetChecked()==true)
    {rbAktuale.SetChecked(false);
        rbNga.SetChecked(false);
        dteNga.SetEnabled(false);
        dteDeri.SetEnabled(false); buttonClick();
    }
}
function CheckedChanged_rbNga(s, e) {
    if(rbNga.GetChecked()==true)
    {rbAktuale.SetChecked(false);
        rbVitiUshtrimor.SetChecked(false);
        dteNga.SetEnabled(true);
        dteDeri.SetEnabled(true); buttonClick();
    }
}