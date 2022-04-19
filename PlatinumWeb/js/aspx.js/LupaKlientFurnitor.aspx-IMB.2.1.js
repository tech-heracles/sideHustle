function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvLupaKlientFurnitor, "601", '');
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
        $("#div").show();
        Init();
    }
    catch (e) {
    }
});

$(window).bind('resize', function () {//po

}).trigger('resize');

$(window).on('unload', function () {
    $.ajax({
        url: Utils.getServerApiUrl("Autorizime", "fshiGrideNgaSessioniLupa"),
        data: JSON.stringify({})
    }).done(Utils.fshiSessionFailCheck);
});

function Init() {
    try {
        myFaqeCelje.shtoHandlerSession();
        btnOk.Focus();
    }
    catch (err) {
        ///alert('gabim');    
    }
}
document.onkeydown = ProcessKeyPress;
function ProcessKeyPress() {
    var currentIndex = gvLupaKlientFurnitor.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaKlientFurnitor.GetVisibleRowsOnPage() - 1) {
            gvLupaKlientFurnitor.SetFocusedRowIndex(0);
        }
        else {
            gvLupaKlientFurnitor.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaKlientFurnitor.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();

    }
}

function OnGridSelectionChanged() {
    gvLupaKlientFurnitor.GetSelectedFieldValues('KodKlientFurnitor;EmertimiKF;IdKlientFurnitor;IdNivelCmimi', OnGridSelectionComplete);
}
var hide = true;
var kodi; var lloji;
function OnGridSelectionComplete(value) {
    if (value.length == 0) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgSelektoniNjeRresht"));
        return;
    }
    hide = true;
    kodi = ''; var emri = ''; var nrLlog = ''; var id = ''; var menyraTransportit = ''; var kushtDergimi = ''; var agjShitje = '';
    var kushtPagese = ''; var maturimi = ''; var zbritjeTotal = ''; var krediti = ''; var limiti = ''; var zbritjeAnalitike = ''; var nivelCmimi = ''; var lloji = ''; var llojiText = "";
    var monedha = '';
    var grida = window.parent.$("#rowed5");
    var idRow = grida["getLastSel2"] == undefined ? 0 : grida.getLastSel2();
    if (value.length > 1) {
        for (i = 0; i < value.length - 1; i++) {
            var values = value[i];
            //kodi += values[0] + ",";
            kodi = kodi + values[0] + ",";
        }
        values = value[value.length - 1];
        //kodi += values[0];
        kodi = kodi + values[0];
    }
    else {

        values = value[0];
        kodi = values[0];
        emri = values[1];
        id = values[2];
        nivelCmimi = values[3];
    }
    switch (window.parent.identikuesPerPopupKlientFurnitori) {
        case "Artikull_Grida":
            window.parent.editorNrLlogariFurnitor.SetText(kodi);
            window.parent.editorEmertimiF.SetText(emri);
            window.parent.editorNrLlogariFurnitor.SetFocus(true);
            break;
        case "CRM":
            var combo = window.parent.ddKlienti;
            Utils.SelectComboItem(combo, id, kodi, emri);
            combo.SetFocus(true);
            window.parent.popupUniversal.Hide();
            break;
            //window.parent.ddKlienti.SetValue(id);
            //window.parent.$('#hfklienti').val(id);
            //window.parent.console.log(id);
            //window.parent.ddKlienti.SetFocus(true);
            //break;
        case "raportklientfurnitor":
            window.parent.editorGlobal.SetText(kodi);
            window.parent.editorGlobal.SetFocus(true);
            if (window.parent.textChangedBtneKodKf)
                window.parent.textChangedBtneKodKf(window.parent.editorGlobal);
            break;
        case "AzhornimKlientFurnitori":
            window.parent.furnitori_ComboBox.SetText(kodi);
            window.parent.furnitori_ComboBox.SetFocus(true);
            break;
        case "AzhornimKlientFurnitoriGrida":
            $.ajax({
                url: Utils.getServerApiUrl("Rregjistrime", "ktheKlientFurnitorVeprimeKFMeIDRow"),
                data: JSON.stringify({ IDkf: id, rreshti: idRow, date: window.parent.Data_DateEdit.GetDate(), llojKursi: 1 })
            }).done(function () { console.log("erdhi pergjigja") });
            break;
        case "Artikull_ButtonEdit":
            var kf = new Array(kodi, emri);
            window.parent.txtFurnitori.SetSelectedIndex(window.parent.txtFurnitori.AddItem(kf, id));
            //window.parent.txtFurnitori.SetSelectedIndex(window.parent.txtFurnitori.AddItem(kodi, id));
            window.parent.txtFurnitori.SetFocus(true);
            break;
        case "Dokumenta":
            window.parent.editorKF.SetText(kodi);
            window.parent.editorKF.SetFocus(true);
            break;
        case "KonfigurimDokumentash":
            window.parent.editorKF.AddItem(kodi, id);
            window.parent.editorKF.SetText(kodi);
            window.parent.editorKF.SetFocus(true);
            break;
        case "Import":
            window.parent.editorKF.SetText(kodi);
            window.parent.editorKF.SetFocus(true);
            break;
        case "Cmim Artikulli":
            var kf = new Array(kodi, emri);
            window.parent.btneFurnitori.SetSelectedIndex(window.parent.btneFurnitori.AddItem(kf, id));
            break;
        case "Magazina":
            window.parent.btneKlientFurnitori.SetText(kodi);
            break;
        case "GjeneroProjekt":
            var kf = new Array(kodi, emri);
            window.parent.btneKlienti.SetSelectedIndex(window.parent.btneKlienti.AddItem(kf, id));
            break;
        case "Planifikim":
            var kf = new Array(kodi, emri);
            window.parent.btneKlientFurnitori.SetSelectedIndex(window.parent.btneKlientFurnitori.AddItem(kf, id));
            break;
        case "Zbritje Analitike":
            //window.parent.btneFurnitori.SetText(kodi);
            var kf = new Array(kodi, emri);
            window.parent.btneFurnitori.SetSelectedIndex(window.parent.btneFurnitori.AddItem(kf, id));
            break;
        case "VeprimeBanka":
            if (window.parent.combo == true) {
                var kf = new Array(kodi, emri);
                window.parent.furnitori_ComboBox.AddItem(kf, id);
                window.parent.furnitori_ComboBox.SetValue(id);
                window.parent.TextChangedFurnitori();
                window.parent.furnitori_ComboBox.Focus();
            }
            else if (window.parent.combo == false) {
                window.parent.vendosSubjektNgaLupa(value, "KlientFurnitor");
            }
            break;
        case "LidhjaDokumentave":
            var kf = new Array(kodi, emri);
            window.parent.klientFurnitor_ButtonEdit.SetSelectedIndex(window.parent.klientFurnitor_ButtonEdit.AddItem(kf, id));
            window.parent.klientFurnitor_ButtonEdit.Focus();
            window.parent.KlientFurnitoriChanged();
            break;
        case "RecetaOptike":
            window.parent.cmbKlienti.SetSelectedIndex(window.parent.cmbKlienti.AddItem(new Array(kodi, emri), id));
            window.parent.cmbKlienti.Focus();
            window.parent.KlientFurnitoriChanged();
            break;
        case "RegjistrimDokumentash":
            if (value.length > 1 && window.parent.regjistrimKF == 2 && window.parent.editorkf == "1") {
                alert(hfState.Get("msgLupaKFNukMundTeZgjidhniMeShumeSeNjeKF"));
                hide = false;
            }
            else {
                if (window.parent.btneAutomjeti.GetText() != '') {
                    //window.parent.btneAutomjeti.SetText('');
                    //window.parent.btneAutomjeti.SetSelectedIndex(-1);
                }

                if (id == "") {
                    id = value[0][2];
                    
                }   

                var ids = [];
                for (var i = 0; i < value.length; i++) {
                    ids[i] = value[i][2];
                }
                if (window.parent.editorkf == "1") {
                    window.parent.callWebserviceKFDefault(id, false, true);
                    window.parent.btnKlienti.SetValue(id);
                    window.parent.btnKlienti.Focus();
                }
                else
                    window.parent.ShtoNeKomboKfVartes(ids);
            }
            break;
        case "ShperndarjeShpenzimesh":
            window.parent.btnFurnitori.SetText(kodi);
            break;
        case "RegjistrimMagazine":
            if (value.length > 1 && window.parent.regjistrimKF == 2) {
                alert(hfState.Get("msgLupaKFNukMundTeZgjidhniMeShumeSeNjeKF"));
                hide = false;
            }
            else {
                var kf = new Array(kodi, emri);
                window.parent.btneKlientFurnitori.SetSelectedIndex(window.parent.btneKlientFurnitori.AddItem(kf, id));
                window.parent.NivelCmimi = nivelCmimi;
                window.parent.btneKlientFurnitori.Focus();
            }
            break;
        case "VeprimeKFKrye":
            var kf = new Array(kodi, emri);
            window.parent.cmbKFKunderParti.SetSelectedIndex(window.parent.cmbKFKunderParti.AddItem(kf, id));
            window.parent.cmbKFKunderParti.Focus();
            break;
        case "VeprimeKFgrida":
            window.parent.editorGlobal.val(kodi);
            window.parent.editorGlobal.focus();
            break;
        case "VeprimeKF":
            var index = idRow;
            var idKontrolli = "#txtKodi" + index;
            if (kodi != "") {
                var kf = kodi.split(',');
                window.parent.kfTeshtuar = kf.length;
                if (kf.length > 1) {
                    window.parent.jQuery(idKontrolli)[0].value = kf[0];
                    var index = parseInt(idRow);
                    var iddata = "#dteData";
                    var data = window.parent.jQuery(iddata + idRow)[0].value;
                    var idpershk = "#txtPershkrim" + index;
                    var pershk = window.parent.jQuery(idpershk)[0].value;
                    var rreshtaTeGrides = grida.getRowData();
                    var indexe = grida.getDataIDs();
                    var ind = 0;
                    for (i = 0; i < rreshtaTeGrides.length; i++) {
                        if (rreshtaTeGrides[i].txtKodi.toString().search('value') != -1)
                            ind = i;
                    }
                    for (j = ind + 1; j < indexe.length; j++)
                        grida.delRowData(indexe[j]);

                    var index2 = index + 1;
                    var ekziston = false;

                    for (var i = 0; i < kf.length - 1; i++) {
                        var kfTjeter = kf[parseInt(i + 1)];
                        if (window.parent.cmbLloji.GetText() != "ND") {
                            for (m = 0; m < rreshtaTeGrides.length; m++) {

                                var editorkodi = rreshtaTeGrides[m].txtKodi;
                                if (editorkodi == kfTjeter && editorkodi != "") {
                                    alert(hfState.Get("msgLupaKFEkzistonKyKFNeGride"));
                                    ekziston = true;
                                    window.parent.kfTeshtuar--;
                                    break;
                                }
                            }
                        }
                        if (!ekziston) {
                            if (window.parent.arrayReadOnlyKolonaGrides[0] == 'True')
                                be = "<input id='butonFshi" + index2 + "' type='image' value='Fshi'   onBlur = 'lostFocusKoloneFundit()' disabled='disabled' onmouseover='ndryshoImazhin(1," + index2 + ")' onmouseout='ndryshoImazhin(0," + index2 + ")'  src='images/square-icon.png' onclick='fshiClicked(" + index2 + ")'/>";

                            else be = "<input id='butonFshi" + index2 + "' type='image' value='Fshi'  onBlur = 'lostFocusKoloneFundit()' onmouseover='ndryshoImazhin(1," + index2 + ")' onmouseout='ndryshoImazhin(0," + index2 + ")'  src='images/square-icon.png' onclick='fshiClicked(" + index2 + ")'/>";
                            var datarow = {
                                txtKodi: kfTjeter, txtEmertimi: "", txtPershkrim: pershk, dteData: data,
                                cmbDebiKredi: "", txtVlefta: "0.00", txtMonedha: "", txtKursi: "0.00", txtVleftaMon: "0.00", txtFshi: be
                            };
                            var su = grida.addRowData(parseInt(index2), datarow);
                            index2 = index2 + 1;
                        }
                        ekziston = false;
                    }
                    for (j = ind + 1; j < rreshtaTeGrides.length; j++) {
                        var su = grida.addRowData(parseInt(index2), rreshtaTeGrides[j]);
                        index2 = index2 + 1;
                    }
                    var rreshtiFundit = grida.getGridParam('reccount');
                    if (grida.getCell(rreshtiFundit, 'txtKodi') != "") {//Shto nje rresht bosh ne fund nese nuk ka nje te tille
                        var rreshtiTjeter = rreshtiFundit + 1;
                        if (window.parent.arrayReadOnlyKolonaGrides[0] == 'True')
                            be = "<input id='butonFshi" + rreshtiTjeter + "' type='image' value='Fshi'  onBlur = 'lostFocusKoloneFundit()' disabled='disabled' onmouseover='ndryshoImazhin(1," + rreshtiTjeter + ")' onmouseout='ndryshoImazhin(0," + rreshtiTjeter + ")'  src='images/square-icon.png' onclick='fshiClicked(" + rreshtiTjeter + ")'/>";

                        else be = "<input id='butonFshi" + rreshtiTjeter + "' type='image' value='Fshi'  onBlur = 'lostFocusKoloneFundit()' onmouseover='ndryshoImazhin(1," + rreshtiTjeter + ")' onmouseout='ndryshoImazhin(0," + rreshtiTjeter + ")'  src='images/square-icon.png' onclick='fshiClicked(" + rreshtiTjeter + ")'/>";
                        var datarow = {
                            txtKodi: "", txtEmertimi: "", txtPershkrim: "", dteData: "",
                            cmbDebiKredi: "", txtVlefta: "", txtMonedha: "", txtKursi: "", txtVleftaMon: "", txtFshi: be
                        }; var su = grida.addRowData(parseInt(rreshtiTjeter), datarow);
                    }
                }
                else {
                    window.parent.$("#txtKodi" + index).val(kodi);
                    var index = idRow;
                    var idKontrolli = "#txtKodi" + index;
                    var ui = new Object();
                    ui.item = new Object();
                    ui.item.label = kodi;
                    ui.item.value = id;
                    window.parent.selectFunc(null, ui, idKontrolli, id, kodi);
                }
            }
            else
                if (kodi == "")
                    window.parent.$(idKontrolli).val("");
            window.parent.$(idKontrolli)[0].focus();
            break;
        case "RegjistrimRezervimi":
            if (value.length > 1) {

                alert(hfState.Get("msgLupaKFNukMundTeZgjidhniMeShumeSeNjeKF"));
                hide = false;
            }
            else {
                var kf = new Array(kodi, emri);
                window.parent.btneKlientFurnitori.SetSelectedIndex(window.parent.btneKlientFurnitori.AddItem(kf, id));
                window.parent.NivelCmimi = nivelCmimi;
                window.parent.btneKlientFurnitori.Focus();
            }
            break;
        case "ShtoAutomjet":
        case 'LupaAutomjetShpejte':
            Utils.SelectComboItem(window.parent.btneKlienti, id, emri, kodi);
            window.parent.btneKlienti.SetFocus(true);
            break;
        case "ShtoKartaKlienti":
            if (value.length > 1 && window.parent.multiselect == 'False') {
                //alert(hfState.Get("msgLupaKFNukMundTeZgjidhniMeShumeSeNjeKF"));
                myMesazh.ShtoMesazhGabimi(hfState.Get("msgLupaKFNukMundTeZgjidhniMeShumeSeNjeKF"));
                hide = false;
            }
            else {
                var kf = new Array(kodi, emri);
                window.parent.cmbKlienti.SetSelectedIndex(window.parent.cmbKlienti.AddItem(kf, id));
                window.parent.cmbKlienti.Focus();
            }
            break;
        case "Shto_KF":

            var kf = new Array(kodi, emri);
            window.parent.btneKlientiKryesor.SetSelectedIndex(window.parent.btneKlientiKryesor.AddItem(kf, id));
            window.parent.btneKlientiKryesor.Focus();
            break;
        case "PerfitimBuxheti":
            var kf = new Array(kodi, emri);
            window.parent.cmbEntiteti.SetSelectedIndex(window.parent.cmbEntiteti.AddItem(kf, id));
            window.parent.cmbEntiteti.Focus();
            window.parent.valueChangedEntiteti(window.parent.cmbEntiteti);
            break;
        default:
            var kf = new Array(kodi, emri);
            window.parent.btneKlientiKryesor.SetSelectedIndex(window.parent.btneKlientiKryesor.AddItem(kf, id));
            window.parent.btneKlientiKryesor.Focus();
            break;
    }
    if (hide) {
        $.ajax({
            url: Utils.getServerApiUrl("Autorizime", "fshiGrideNgaSessioniLupa"),
            data: JSON.stringify({})
        }).done(Utils.fshiSessionFailCheck);
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
            if (window.parent.identikuesPerPopupKlientFurnitori == 'VeprimeBanka') {
                window.parent.myButtonClickLupa.LupaUniversal_Click(hfState.Get("JQgridShtoKlient"), 'LupaKlientShpejte.aspx?vjenNga=ShtoVeprimBanka&kf=klient', 850, 600);
            }
            else if (window.parent.identikuesPerPopupKlientFurnitori == 'VeprimeKF')
                window.parent.myButtonClickLupa.LupaUniversal_Click(hfState.Get("JQgridShtoKlient"), 'LupaKlientShpejte.aspx?vjenNga=Shto_VeprimeKF&kf=klient', 850, 600);

            else {
                $.ajax({
                    url: Utils.getServerApiUrl("Autorizime", "fshiGrideNgaSessioniLupa"),
                    data: JSON.stringify({})
                }).done(Utils.fshiSessionFailCheck);
                $.ajax({
                    url: Utils.getServerApiUrl("Rregjistrime", "ruajNeSessionURL"),
                    data: JSON.stringify({ url: window.location.href })
                }).done(Succeded);
                $.ajax({
                    url: Utils.getServerApiUrl("Konfigurime", "ruajNeSessionURLLupaShpejte"),
                    data: JSON.stringify({ url: window.location.href, lupa: 'LupaKlientShpejte' })
                }).done(Succeded);
            }
            break;
        case 'ShtoFurnitor':
            e.processOnServer = false;
            if (window.parent.identikuesPerPopupKlientFurnitori == 'VeprimeBanka') {
                window.parent.myButtonClickLupa.LupaUniversal_Click(hfState.Get("JQgridShtoFurnitor"), 'LupaKlientShpejte.aspx?vjenNga=ShtoVeprimBanka&kf=furnitor', 850, 600);
            }
            else if (window.parent.identikuesPerPopupKlientFurnitori == 'VeprimeKF')
                window.parent.myButtonClickLupa.LupaUniversal_Click(hfState.Get("JQgridShtoFurnitor"), 'LupaKlientShpejte.aspx?vjenNga=Shto_VeprimeKF&kf=furnitor', 850, 600);
            else {
                $.ajax({
                    url: Utils.getServerApiUrl("Autorizime", "fshiGrideNgaSessioniLupa"),
                    data: JSON.stringify({})
                }).done(Utils.fshiSessionFailCheck);
                $.ajax({
                    url: Utils.getServerApiUrl("Rregjistrime", "ruajNeSessionURL"),
                    data: JSON.stringify({ url: window.location.href })
                }).done(SuccededF);
                $.ajax({
                    url: Utils.getServerApiUrl("Konfigurime", "ruajNeSessionURLLupaShpejte"),
                    data: JSON.stringify({ url: window.location.href, lupa: 'LupaKlientShpejte' })
                }).done(SuccededF);
            }

            break;
        case 'Modifiko':
            e.processOnServer = false;
            if (gvLupaKlientFurnitor.GetSelectedRowCount() == 0)
                {
                myMesazh.ShtoMesazhGabimi(hfState.Get("msgSelektoniNjeRresht"));
                return;
            }
            gvLupaKlientFurnitor.GetSelectedFieldValues("KodKlientFurnitor;LlojiKF", modifikoKF);
            break;
        case 'Klono':
            e.processOnServer = false;
            if (gvLupaKlientFurnitor.GetFocusedRowIndex() == -1) {
                myMesazh.ShtoMesazhGabimi(hfState.Get("msgSelektoniNjeRresht"));
                return;
            }
            gvLupaKlientFurnitor.GetRowValues(gvLupaKlientFurnitor.GetFocusedRowIndex(), 'KodKlientFurnitor;LlojiKF', klonoKF);
            break;
    }
}
function modifikoKF(values) {
    var kodi = values[0][0];
    if (values[0][1] === true)
        lloji = 'klient';
    else lloji = 'furnitor';
    window.parent.myButtonClickLupa.LupaUniversal_Click(hfState.Get("JQgridModifikoKlientFurnitor"), 'LupaKlientShpejte.aspx?modifikim=true&kodi=' + kodi + '&kf=' + lloji, 850, 600);
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "ruajNeSessionURLLupaShpejte"),
        data: JSON.stringify({ url: window.location.href, lupa: 'LupaKlientShpejte' })
    }).done(SuccededK);
}

function klonoKF(values) {
    kodi = values[0];
    if (values[1] === true)
        lloji = 'klient';
    else lloji = 'furnitor';
    window.parent.myButtonClickLupa.LupaUniversal_Click(hfState.Get("headerPopUpKlonoKlientFurnitor"), 'LupaKlientShpejte.aspx?klonim=true&kodi=' + kodi + '&kf=' + lloji, 850, 600);
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "ruajNeSessionURLLupaShpejte"),
        data: JSON.stringify({ url: window.location.href, lupa: 'LupaKlientShpejte' })
    }).done(SuccededK);
}

function Succeded() {
    window.parent.myButtonClickLupa.LupaUniversal_Click(hfState.Get("JQgridShtoKlient"), 'LupaKlientShpejte.aspx?kf=klient&vjenNgaRoute=' + Utils.getUrlVar("vjenNgaRoute"), 850, 600);
}

function SuccededK() {
}

function SuccededF() {
    window.parent.myButtonClickLupa.LupaUniversal_Click(hfState.Get("JQgridShtoFurnitor"), 'LupaKlientShpejte.aspx?kf=furnitor', 850, 600);
}

//metoda per te shfaqur popupin e filtrave
function Filtra_Click() {
    popFiltra.SetHeaderText(hfState.Get("headerPopUpZgjidhFiltrin"));
    popFiltra.SetSize(500, 350);
    popFiltra.SetContentUrl('LupaFiltra.aspx?grida=gvLupaKlientFurnitor&page=LupaKlientFurnitor.aspx&idKonfigAmbjente=577');
    popFiltra.Show();
}