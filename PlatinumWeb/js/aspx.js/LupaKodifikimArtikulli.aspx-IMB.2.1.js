;
function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvLupaKodArt, "622", '');
}

function textChanged(s, e) {
    myMenu.textChanged(s, e);
}

function BeginCallback(s, e) {
    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}

function Init() {
 //   myMesazh.shtoHandler();
    try {
        myFaqeCelje.shtoHandlerSession();      
        btnOk.Focus();
    }
    catch (err) {
    }
}
var faqe = 'kodifikime';
document.onkeydown = ProcessKeyPress;
function ProcessKeyPress() {
    var currentIndex = gvLupaKodArt.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaKodArt.GetVisibleRowsOnPage() - 1) {
            gvLupaKodArt.SetFocusedRowIndex(0);
        }
        else {
            gvLupaKodArt.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaKodArt.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();

    }
}

function zgjidhElement() {
    if (window.parent.identifikuesPerPopupKodifikimin == "Shto_Artikull"||window.parent.identifikuesPerPopupKodifikimin == "Import") {
        gvLupaKodArt.GetSelectedFieldValues('IdKodifikimi;KodKodifikimi;PershkrimKodifikimi;NivelKodifikimi;', OnCompletePerWebService);
    }
    else {
        OnGridSelectionChanged();
        pergjigja.SetText("");

    }
}
function OnGridSelectionChanged() {
    if (gvLupaKodArt.GetSelectedRowCount() == 0) {
        myMesazh.ShtoMesazhGabimi("Nuk keni zgjedhur asnje grupim");
        return;
    } 
        gvLupaKodArt.GetSelectedFieldValues('IdKodifikimi;KodKodifikimi;PershkrimKodifikimi;NivelKodifikimi;IdLlogariPakesimi;NrLlogPakesimi', OnGridSelectionComplete);
}
function OnCompletePerWebService(values) {
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "eshteKodifikimiPrind"),
        data: JSON.stringify({ input: values[0][0], idNdermarrje: hfState.Get('idNdermarrje') })
    }).done(SucceededCallback);
}

function SucceededCallback(result) {
    if (result == "prind") {
        myMesazh.ShtoMesazhGabimi("Nuk mund te zgjidhni nje grup i cili eshte prind!");
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
    var kodifik = vl[1];
    switch (window.parent.identifikuesPerPopupKodifikimin) {
        case "LupaArtikull":
            if (Utils.getUrlVar('llojKodifikimi') == '1') {
                Utils.SelectComboItem(window.parent.btneKodifikimi1, vl[0], vl[1]);
                window.parent.btneKodifikimi1.SetFocus(true);
            }
            else if (Utils.getUrlVar('llojKodifikimi') == '2') {
                Utils.SelectComboItem(window.parent.btneKodifikimi2, vl[0], vl[1]);
                window.parent.btneKodifikimi2.SetFocus(true);
            }
            else if (Utils.getUrlVar('llojKodifikimi') == '3') {
                Utils.SelectComboItem(window.parent.btneKodifikimi3, vl[0], vl[1]);
                window.parent.btneKodifikimi3.SetFocus(true);
            }
            break;
        case "Kodifikim":

           window.parent.editorPrind.SetText(vl[2]);
            var hf = window.parent.document.getElementById("hfPrindi");
            hf.value = vl[1];
            window.parent.editorNiveli.SetText(parseInt(vl[3]) + 1);
            window.parent.editorPrind.SetFocus(true);
    
           break;
        case "KodifikimAQT":
            
            Utils.SelectComboItem(window.parent.editorPrind, vl[0], [vl[1], vl[2], vl[3]]);
            var hf = window.parent.document.getElementById("hfPrindi");
            hf.value = vl[1];
            window.parent.editorPrind.SetValue(vl[0]);
            window.parent.editorPrind.SetFocus(true);
            window.parent.editorNiveli.SetText(parseInt(vl[3]) + 1);
            window.parent.editorPrind.SetFocus(true);
            window.parent.merrSkeme(window.parent.editorPrind);
            break;
        case "Shto_Artikull":
            if (window.parent.grida == '1') {
                //                        window.parent.btneKodifikimi1.SetText(vl[1]);
                Utils.SelectComboItem(window.parent.btneKodifikimi1, vl[0], vl[1]);
                window.parent.btneKodifikimi1.SetFocus(true);
                window.parent.UpdateGrida(vl[0]);
                Utils.SelectComboItem(window.parent.btnLlogPakesim, vl[4], vl[5]);
            }
            else if (window.parent.grida == '2') {
                //                        window.parent.btneKodifikimi2.SetText(vl[1]);
                Utils.SelectComboItem(window.parent.btneKodifikimi2, vl[0], vl[1]);
              
                window.parent.btneKodifikimi2.SetFocus(true);
                //Utils.SelectComboItem(window.parent.btnLlogPakesim, vl[4], vl[5]);
            }
            else if (window.parent.grida == '3') {
                //                        window.parent.btneKodifikimi2.SetText(vl[1]);
                Utils.SelectComboItem(window.parent.btneKodifikimi3, vl[0], vl[1]);
              
                window.parent.btneKodifikimi3.SetFocus(true);
                //Utils.SelectComboItem(window.parent.btnLlogPakesim, vl[4], vl[5]);
            }
            break;
        case "Cmim Artikulli":
            if (window.parent.grida == '1') {
                window.parent.btneKodifikimi1.SetText(vl[1]);
                window.parent.btneKodifikimi1.SetFocus(true);
            }
            else if (window.parent.grida == '2') {
                window.parent.btneKodifikimi2.SetText(vl[1]);
                window.parent.btneKodifikimi2.SetFocus(true);
            }
            break;
        case "Zbritje Analitike":
            if (window.parent.grida == '1') {
                window.parent.btneKodifikimi1.SetText(vl[1]);
                window.parent.btneKodifikimi1.SetFocus(true);
            }
            else if (window.parent.grida == '2') {
                window.parent.btneKodifikimi2.SetText(vl[1]);
                window.parent.btneKodifikimi2.SetFocus(true);
            }
            break;
         case "raportGrupim":
             for (i = 1; i < values.length; i++) //degaAdmin += "," + values[i][1];
                 kodifik = kodifik + "," + values[i][1];
             window.parent.editorGlobal.SetText(kodifik);
             window.parent.editorGlobal.SetFocus(true);
            //window.parent.editorGlobal.SetText(vl[1]);
            window.parent.editorGlobal.SetFocus(true);
            break; 
            case "Import":
            window.parent.editorGlobal.SetText(vl[1]);
            window.parent.editorGlobal.SetFocus(true);
            break;
        case "GjeneroProjekt":
            window.parent.editorGlobal.SetText(vl[1]);
            break;
        case "ShtoKartaKlientiLupaGrupimArtikulli":
            window.parent.VendosKodifikim1Artikulli(vl[0], vl[1]);
            break;
        default:
            break;
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
            zgjidhElement();
            break;
        case "Anullo":
            e.processOnServer = false;
            window.parent.popupUniversal.Hide();
            break;
    }
}

//metoda per te shfaqur popupin e filtrave
function Filtra_Click() {
    document.getElementById('<%= Container.ClientID %>').src = 'LupaFiltra.aspx?grida=gvLupaKodArt&page=LupaKodifikimArtikulli.aspx';
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
        gvLupaKodArt.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});
$(window).bind('resize', function () {//po
    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaKodArt.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');
