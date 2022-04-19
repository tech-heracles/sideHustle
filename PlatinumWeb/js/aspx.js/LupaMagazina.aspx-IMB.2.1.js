function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvLupaMagazina, "632", '');
}

function textChanged(s, e) {
    myMenu.textChanged(s, e);
}

function BeginCallback(s, e) {
    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}

$(window).on('unload', function () {
    $.ajax({
        url: Utils.getServerApiUrl("Autorizime", "fshiGrideNgaSessioniLupa"),
        data: JSON.stringify({})
    }).done(Utils.fshiSessionFailCheck);
});

function Init() {
    try {
        if (window.parent.identifikuesPerPopupMagazina !== 'GisDefault')           
        myFaqeCelje.shtoHandlerSession();
        //gvLupaMagazina.SelectRowOnPage(0, true);
        //gvLupaMagazina.SetFocusedRowIndex(0);
        btnOk.Focus();
    }
    catch (err) {
        ///alert('gabim');    
    }
}

document.onkeydown = ProcessKeyPress;

function ProcessKeyPress() {
    var currentIndex = gvLupaMagazina.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaMagazina.GetVisibleRowsOnPage() - 1) {
            gvLupaMagazina.SetFocusedRowIndex(0);
        }
        else {
            gvLupaMagazina.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaMagazina.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();
    }
}

function OnGridSelectionChanged() {
    //gvMakro.GetSelectedFieldValues('NrLlogariKF;EmertimiKF', OnGridSelectionComplete);  gvLupaMagazina.GetFocusedRowIndex(),
    switch (window.parent.identifikuesPerPopupMagazina) {
        case "GisDefault":
            gvLupaMagazina.GetSelectedFieldValues('Kodi;Pershkrimi;IdDegeAdministrative;IdNjesiAdministrative;Adresa;KodMagPrind;gidPrindi', OnGridSelectionComplete);
            break;
        default:
            gvLupaMagazina.GetSelectedFieldValues('Kodi;Pershkrimi;IdDegeAdministrative;IdNjesiAdministrative;Adresa', OnGridSelectionComplete);
            break;
    }
}

function OnGridSelectionComplete(values) {
    if (values[0] != undefined) {
        $.ajax({
            url: Utils.getServerApiUrl("Rregjistrime", "mbushMagazinenSipasID"),
            data: JSON.stringify({ id: values[0][3]})
        }).done(function (id) {
            if (id == 1) {
                myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukMundTeKryesniVeprimeMeMagazinaMeArtikujAfatShkurter"));
                return;
            }
        }).error(function (id) {
            console.log(id);
        });
        var emri = values[0][1];
        var kodi = values[0][0];
        var dega = values[0][2];
        var idDege = values[0][3];
        var adrese = values[0][4];
        switch (window.parent.identifikuesPerPopupMagazina) {
            case "RegjistrimDokumentash":
                window.parent.btnMagazina.SetSelectedIndex(window.parent.btnMagazina.AddItem(new Array(kodi, emri), idDege));
                window.parent.btnMagazina.SetFocus();
                window.parent.cmbDegeAdministrative.SetValue(dega);
                window.parent.popupUniversal.Hide();
                window.parent.MagazinaChanged();
                break;
            case "RegjistrimDokumentash_Trupi":
                window.parent.pageState.vendosMagazineNgaLupa({ idKodi: idDege, kodi: kodi });
                break;
            case "raportmagazina":
            //case "raportPershMag":
                if (values.length > 1) {
                    for (i = 1; i < values.length; i++)
                        kodi = kodi + "," + values[i][0];
                }
                window.parent.editorGlobal.SetText(kodi);
                window.parent.editorGlobal.SetFocus();
                break;
            case "Rivleresim":
                for (i = 1; i < values.length; i++) //kodi += "," + values[i][0];
                    kodi = kodi + "," + values[i][0];
                window.parent.cmbMagazina.SetText(kodi);
                window.parent.cmbMagazina.SetFocus();
                window.parent.LostFocus();
                break;
            case "KonfigurimDokumentash":
                window.parent.editorNJA.SetText(kodi);
                window.parent.editorNJA.SetFocus();
                break;
            case "ShperndarjeShpenzimesh":
                window.parent.btnMagazina.SetText(kodi);
                break;
            case "RegjistrimAmortizimi":
                window.parent.cmbMagazina.SetText(kodi + " (" + emri + ")");
                break;
            case "RivleresimeAmortizimi":
                window.parent.btneMagazina.SetText(kodi + " (" + emri + ")");
                break;
            case "RegjistrimRiparime":
                window.parent.btneMagazina.SetText(kodi);
                window.parent.btneMagazina.SetFocus();
                window.parent.TextChangedMagazina();
                break;
            case "RegjistrimMagazine":
                if (window.parent.identifikuesMagazina == 'Mag1') {
                    window.parent.btneMagazina.SetSelectedIndex(window.parent.btneMagazina.AddItem(new Array(kodi, emri), idDege));
                    window.parent.btneMagazina.SetFocus();
                    window.parent.cmbDegeAdministrative.SetValue(dega);
                    window.parent.popupUniversal.Hide();
                    window.parent.MagazinaChanged();
                }
                else {
                    window.parent.btneMagazina2.SetSelectedIndex(window.parent.btneMagazina2.AddItem(new Array(kodi, emri), idDege));
                    window.parent.btneMagazina2.SetFocus();
                    window.parent.TextChangedMagazina2();
                }
                break;
            case "RegjistrimMagazine_Trupi":
                if (window.parent.identifikuesMagazina == 'Mag1') {
                    window.parent.pageState.vendosMagazineNgaLupa({ idKodi: idDege, kodi: kodi });
                }
                else {
                    window.parent.pageState.vendosMagazine2NgaLupa({ idKodi: idDege, kodi: kodi });
                }
                break;
            case "RegjistrimNdryshimCmimSasi":
                window.parent.btneMagazina.SetText(kodi);
                window.parent.btneMagazina.SetFocus();
                window.parent.cmbDegeAdministrative.SetValue(dega);
                window.parent.TextChangedMagazina();
                break;
            case "RegjistrimInventarizimi":
                window.parent.btneMagazina.SetText(kodi);
                window.parent.btneMagazina.SetFocus();
                break;
            case "Planifikim":
                window.parent.btneMagazina.SetText(kodi);
                window.parent.btneMagazina.SetFocus();
                window.parent.TextChangedMagazina();
                break;
            case "EkzekutimProdhimi":
                window.parent.editorMag.SetText(kodi);
                window.parent.editorMag.SetFocus();
                window.parent.TextChangedMagazina(window.parent.identifikuesMagazina);
                break;
            case "ShtoArtikull":
                window.parent.btnMagazina.SetText(kodi);
                window.parent.btnMagazina.SetFocus();
                break;
            case "Import":
                window.parent.editorGlobal.SetText(kodi);
                window.parent.editorGlobal.SetFocus(true);
                break;
            case "RegjistrimRezervimi":
                window.parent.btneMagazina.SetText(kodi);
                window.parent.btneMagazina.SetFocus();
                window.parent.cmbDegeAdministrative.SetValue(dega);
                window.parent.TextChangedMagazina();
                break;
            case 'LupaArtikull':
                window.parent.btneMagazina.SetText(kodi);
                window.parent.btneMagazina.SetFocus();
                if (window.parent.cbGjendje.GetChecked() || window.parent.cbKosto.GetChecked())
                    window.parent.MerrGjendjeKosto();
                break;
            case "NjesiAdministrative":
                window.parent.Utils.SelectComboItem(window.parent.cmbMagPrind, idDege, kodi);
                break;
            case "raportPershMag":
                if (values.length > 1) {
                    for (i = 1; i < values.length; i++)
                        kodi = kodi + "," + values[i][1];
                }
                window.parent.editorGlobal.SetText(emri);
                window.parent.editorGlobal.SetFocus();
                break;
            case "GisDefault":               
                window.parent.varSettings.editim.objLidhjeGisWeb.KODI = kodi;
                window.parent.varSettings.editim.objLidhjeGisWeb.PERSHKRIMI = emri;
                window.parent.varSettings.editim.objLidhjeGisWeb.IDMAGAZINA = idDege;
                window.parent.varSettings.editim.objLidhjeGisWeb.IDKODIFIKIMI = 0;
                window.parent.varSettings.editim.objLidhjeGisWeb.IDARTIKULLI = 0;
                window.parent.varSettings.editim.objLidhjeGisWeb.IDSERIALI = 0;
                window.parent.varSettings.editim.objLidhjeGisWeb.IDKOKADOK = 0;
                window.parent.varSettings.editim.objLidhjeGisWeb.IDTRUPIDOKLIDHES = 0;
                window.parent.varSettings.editim.objLidhjeGisWeb.SERIALKOD = '';
                window.parent.varSettings.editim.objLidhjeGisWeb.KODKODIFIKIMI = '';
                window.parent.varSettings.editim.objLidhjeGisWeb.DTMODIFIKIMI = '';
                window.parent.varSettings.editim.objLidhjeGisWeb.gidPrindi = values[0][6];
                window.parent.varSettings.editim.objLidhjeGisWeb.kodPrindi = values[0][5];
                window.parent.objSelektuarNgaLupa = true;
                window.parent.varSettings.editim.objLidhjeGisWebAfishim.ADRESA = adrese;
                $.extend(window.parent.varSettings.editim.objLidhjeGisWebAfishim, window.parent.varSettings.editim.objLidhjeGisWeb);
                window.parent.Ext.getCmp('ConnButtonId').enable();
                window.parent.Ext.getCmp('DeleteConnButtonId').enable();
                window.parent.plotesoTeDhenaPaneliUpdateInsert();
                break;
            default:
                break;
        }
    }
    
    window.parent.popupUniversal.Hide();
}
function VendosIdMagazine(id) {
    if (id == 1) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukMundTeKryesniVeprimeMeMagazinaMeArtikujAfatShkurter"));
        return;
    }
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
    document.getElementById('<%= Container.ClientID %>').src = 'LupaFiltra.aspx?grida=gvLupaMagazina&page=LupaMagazina.aspx&idKonfigAmbjente=577';
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
        $("#div").show();// $("#div")[0].style.visibility = 'visible';
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaMagazina.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});
$(window).bind('resize', function () {//po

    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaMagazina.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');