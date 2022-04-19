; $(document).ready(function (e) {
    changeName();
});
//variabla per te kaluar nga nje reshti i grides tek tjetri
var indexSel = 0;
//indexi i zgjedhur per modifikim
var indexModifiko;
//perdoret per te treguar nese duhet te mbushen tabet apo jo kjo perdoret qe tabet te mbushen vetem heren e pare dhe jo pas cdo here qe  kalohet nga nje tab tek tjetri
var mbush = true;
//perdoret per te daluar rastet kur bejme double click apo thjesht click ne menyre qe ne double click te kaloje tek tabi i dyte
var kaloTab = false;
var IdStatusDok = undefined;
var editingIndex = undefined;

var resultkonf;
var colKontrollet, colAtrTrupi;

function checkText(s, e) {
    myMenu.checkText(s, e);
} var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvListaPasqyrat, hfState.Get("idKomponente"), cmbKonfigurimi.GetText());
}
function textChanged(s, e) {
    myMenu.textChanged(s, e);
}
//metodat per te hapur faqen e modifikimit me double click
function OnGridDoubleClick(index) {
    indexModifiko = index;
    mbushfusha();
}

function menu_click(s, e) {
    myMenu.menu_click_batchEdit(s, e, $('#hfShtimModifikim'), $('#hfId'), PageControl, gvPasqyra, false, indexModifiko, pastrofusha, SucceededCallbackKonfig, resultkonf, colKontrollet, aktivizoFusha, colAtrTrupi, $("#hfStatusDokumenti"));
    e.processOnServer = false;
}
function valido(s, e) {
    
    return CustomValidation(s, e) && myFaqeCelje.validoKontrolleDheGriden(s, e, PageControl, gvPasqyra, hfTeDrejta, $('#hfShtimModifikim'));
}

function CustomValidation(s, e) {
    //  gvPasqyra = new ASPxClientGridView();
    var kolona = gvPasqyra.GetColumnById("IdProfesioni");
    //for (var i = 0, rreshtat = gvPasqyra.GetVisibleRowsOnPage() ; i < rreshtat;i++)
    //{
    //    gvPasqyra.batchEditApi.ValidateRow(i);
    //    gvPasqyra.batchEditApi.StartEdit(i, kolona.index);
    //}

    if (gvPasqyra.GetVisibleRowsOnPage() == 0) {
        myMesazh.ShtoMesazhGabimi("Nuk mund te ruhet dokumenti me trup bosh!");
        return false;
    }
    if (txtTotaliFemra.GetValue() < 0 || txtTotaliMeshkuj.GetValue() < 0) {
        myMesazh.ShtoMesazhGabimi("Nuk mund te vendosen vlera negative per numrin e femrave ose te meshkujve! ");
        return false;
    }
    if (!gvPasqyra.batchEditApi.ValidateRows()) {
        myMesazh.ShtoMesazhGabimi("Trupi i dokumentit nuk eshte i rregullt!");
        return false;
    }
    return true;
}


function CustomButtonsClick(s, e) {
    if (e.buttonID == 'btnDelete') 
            s.DeleteRow(e.visibleIndex);
}


function vendosNrAutomatik(colAtrTrupi, colKontrollet) {
    myNrAuto.vendosNrAutomatik(colAtrTrupi, colKontrollet, new Date());
}
//merr te dhenat e rreshtit te selektuar
function mbushfusha() {
    mbush = false;
    $('#hfShtimModifikim').val("modifikim");
    indexModifiko = gvListaPasqyrat.GetFocusedRowIndex();
    if (indexModifiko == -1)
        myMesazh.ShtoMesazhGabimi('Duhet te zgjidhni nje pasqyre!');
    else
        gvListaPasqyrat.GetRowValues(indexModifiko, 'IdKokaDok;NrDok;DtDok;Muaji;TotaliFemra;TotaliMeshkuj;IdStatusDok', OnGetRowValuesMod);
    Utils.shfaqLoadingGif();;
}

//mbush fushat me te dhenat e rreshtit te selektuar
function OnGetRowValuesMod(values) {
    if ($('#hfShtimModifikim').val() == "shtim")
        return;

    // $('#hfId').val(values[0]);
    if ($('#hfShtimModifikim').val() == "modifikim") {
        txtNrDok.SetEnabled(false);
    } else {
        txtNrDok.SetEnabled(true);
    }

    txtNrDok.SetText(values[1]);
    txtDate.SetValue(values[2]);
    cmbMuaji.SetValue(values[3]);
    txtTotaliFemra.SetText(values[4]);
    txtTotaliMeshkuj.SetText(values[5]);
    IdStatusDok = values[6];
    ///te futen ne callbackpanel qe te mos perdorim dy callback
    gvPasqyra.PerformCallback(values[0]);
    if (kaloTab) {
        PageControl.SetActiveTabIndex(1);
        ASPxMenu1.AdjustControl();
        myMenu.PercaktoMenuSipasTabit(PageControl.GetActiveTabIndex(), hfTeDrejta, $('#hfShtimModifikim'));
        // AktivizoDraft();
    }

    Utils.hiqLoadingGif();;
}

//pastron fushat per shtim dhe ben aktive fushat
function pastrofusha() {
    txtNrDok.SetText("");
    // txtNrDok.SetEnabled(true);
    txtDate.SetValue(new Date());
    cmbMuaji.SetValue(null);
    txtTotaliFemra.SetText("");
    txtTotaliMeshkuj.SetText("");
    aktivizoFusha(colKontrollet, colAtrTrupi, false);
    gvPasqyra.PerformCallback(-1);
    IdStatusDok = undefined;
    // AktivizoDraft();
}

function callWebserviceKonfigurimi(idKomp, kodKonf) {
    var idGjuha = hfState.Get('idGjuha');
    $.ajax({      
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
        data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: hfState.Get('idNdermarrje'), idGjuha: idGjuha })
    }).done(SucceededCallbackKonfig);
    gvListaPasqyrat.PerformCallback(idKomp + ";" + kodKonf);
}

function callWebserviceKonfigurimiInit(idKomp, kodKonf) {
    $("#dvDetyra").show();
    var idGjuha = hfState.Get('idGjuha');

    $.ajax({    
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
        data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: hfState.Get('idNdermarrje'), idGjuha: idGjuha })
    }).done(SucceededCallbackKonfig);
}

function SucceededCallbackKonfig(result) {
    if (!result.d && !result)
        return;
    if (result && result.d)
        result = result.d;
    if (result !== "" && result !== null) {
        colKontrollet = result.colKontrollet;
        colAtrTrupi = result.colAtrTrupi;
        resultkonf = result;
        //  LupaKontrollet(colKontrollet, colAtrTrupi);
        var hf = $('#hfKontrollet');
        var hfLidhur = $("#hfLidhur");
        var hfMod = $('#hfShtimModifikim');
        var arrTabela = ['tblPasqyra', 'tblPasqyraFooter'];
        var arrPrind = ['divFillim', 'divFundi'];

        myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, hf, hfMod, '', arrTabela, undefined, undefined, hfLidhur, arrPrind);
        myMenu.PercaktoMenuSipasTabit(PageControl.GetActiveTabIndex(), hfTeDrejta, $('#hfShtimModifikim'));

        $("#divFillim").hide();
        // $("#divGrida").width($("#tblPasqyra").width() + "%");
        $("#divGrida").show();
        $("#divFundi").show();

        if (hfMod.val() == "shtim" || hfMod.val() == "klonim") {
            hfNrAuto.Clear();
            hfNrAutoDet.Clear();
            vendosNrAutomatik(colAtrTrupi, colKontrollet);
            IdStatusDok = 0;
        }
        //AktivizoDraft();
    }
}

function aktivizoFusha(colKontrollet, colAtrTrupi, isLidhur) {
    var hfMod = $('#hfShtimModifikim');
    myFaqeCelje.aktivFusha(colKontrollet, colAtrTrupi, hfMod, isLidhur, '#ASPxPageControl1_');
    if (hfMod.val() != "klonim") {
        txtNrDok.SetEnabled(true);
        txtNrDok.SetText("");
    }
}

function ndryshoKonfigurimin() {
    lblKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[1]);
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[0]);
    callWebserviceKonfigurimi(hfState.Get("idKomponente"), cmbKonfigurimi.GetText());
}

function ndryshoKonfiguriminInit() {
    callWebserviceKonfigurimiInit(hfState.Get("idKomponente"), cmbKonfigurimi.GetText());
}

function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ?
				evt.keyCode : evt.charCode;
}

function changeName() {
    var hf = $("#hfKonffillestar")[0];
    if (hf !== null) {
        lblKonfigurimi.SetText(hf.value.split(';')[1]);
        cmbKonfigurimi.SetText(hf.value.split(';')[0]);
        ndryshoKonfiguriminInit();
    }
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(EndRequestHandler);
    prm.add_endRequest(myMesazh.EndRequestTimer);
    myFaqeCelje.changeName(hfState.Get('komponente'), 0, hf);
}

/*
Function: EndRequestHandler

Thirret nese ruajtja eshte bere ne rregull. Pastron koken e dokumentit dhe array-t e perdorura.
Shiko funksionet  <ndryshoKonfigurimin>.
*/
function EndRequestHandler(sender, args) {
    var hf = $("#hfStatusVeprimi");
    var colKontrollet = $('#hfKontrollet');
    var hfShtimModifikim = $('#hfShtimModifikim'); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
    var hfId = $('#hfId');  //hidden fieldi qe ruan id  e rreshtit te selektuar
    indexModifiko = myFaqeCelje.EndRequestHandlerNew(sender, args, hf, colKontrollet, colAtrTrupi, hfShtimModifikim, hfId, indexModifiko, PageControl, gvListaPasqyrat, hfState.Get("idKomponente"), pastrofusha, hfTeDrejta)
}

function krijoTable(rresht, kolone, emerTabele) {
    myFaqeCelje.krijoTable(rresht, kolone, emerTabele);
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

function PageControlTabChanging(s, e) {
    indexModifiko = gvListaPasqyrat.GetFocusedRowIndex();
    if (mbush) {
        if (indexModifiko != -1) {
            OnGridDoubleClick(indexModifiko);
        }
        else {
            mbush = false;
            $('#hfShtimModifikim').val('shtim');
            $('#hfId').val(0);

            pastrofusha();
        }
    }
    kaloTab = false;
    myMenu.PercaktoMenuSipasTabit(PageControl.GetActiveTabIndex(), hfTeDrejta, $('#hfShtimModifikim'));

    ASPxMenu1.AdjustControl();
}

function gvEndCallback(s, e) {
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "lexoMesazhNgaSessioni"),
    data: JSON.stringify({})
    }).done(SucceededCallbackMesazhi);
}
function SucceededCallbackMesazhi(result) {
    if (result == null)
        return;

    if (result.d)
        result = result.d;

    if (result.length == undefined)
        return;

   
        var arr = result.split(':');
        if (arr[1] == "Green") {
            myMesazh.ShtoMesazhSuksesi(arr[0]);
            gvListaPasqyrat.Refresh();
            PageControl.SetActiveTabIndex(0);
        }
        else if (arr[1] == "Red")
            myMesazh.ShtoMesazhGabimi(arr[0]);
    
    Utils.hiqLoadingGif();;
}
function AktivizoDraft() {
    if (PageControl.GetActiveTabIndex() == 1) {
        if (IdStatusDok == 1)
            ASPxMenu1.GetItemByName("Draft").SetEnabled(false)
        else
            ASPxMenu1.GetItemByName("Draft").SetEnabled(true)
    }
}
//function rowValidation(s, e) {
//    if (e.visibleIndex == (s.GetVisibleRowsOnPage() - 1))//rreshti i fundit
//        return;
//    for (var col in e.validationInfo) {
//        if (col.value == null) {
//            col.isvalid = false;
//            col.errorText = "Vlera nuk mund te jete bosh!";
//        }
//    }
//}

function rowValidation(s, e) {
    //s = ASPxClientGridView.Cast(s);
    //kontrollohet nese ekziston i njejti profesion ne grid!
    //var kolona = s.GetColumnById("IdProfesioni");

 


    //if (kolona != undefined) {
    //    var profesioni = e.validationInfo[kolona.index].value;
    //    var count = 0;
    //    for (var i = 0, rreshtat = s.GetVisibleRowsOnPage() ; i < rreshtat ; i++) {
         
    //        var vleraNew = s.batchEditApi.GetCellValue(i, "IdProfesioni");
    //        if (vleraNew != null && i != editingIndex && vleraNew == profesioni) {
    //            e.validationInfo[kolona.index].isValid = false;
    //            e.validationInfo[kolona.index].errorText = "Ky profesion ekziston njehere ne grid!";
    //            break;
    //        }
    //    }
    //}

    var vleraFakt = s.GetColumnById("VleraFakt");
    if (vleraFakt != undefined) {
        if (e.validationInfo[vleraFakt.index].value < 0) {
            e.validationInfo[vleraFakt.index].isValid = false;
            e.validationInfo[vleraFakt.index].errorText = "Vlera fakt nuk mund te jete negative!";
        }
    }

    var vleraPlan = s.GetColumnById("VleraPlan");
    if (vleraPlan != undefined) {
        if (e.validationInfo[vleraPlan.index].value < 0) {
            e.validationInfo[vleraPlan.index].isValid = false;
            e.validationInfo[vleraPlan.index].errorText = "Vlera fakt nuk mund te jete negative!";
        }
    }

    var totaliMeshkuj = s.GetColumnById("TotaliMeshkuj");
    if (totaliMeshkuj != undefined) {
        if (e.validationInfo[totaliMeshkuj.index].value < 0) {
            e.validationInfo[totaliMeshkuj.index].isValid = false;
            e.validationInfo[totaliMeshkuj.index].errorText = "Numri i punonjesve meshkuj nuk mund te jete negativ";
        }
    }

    var totaliFemra = s.GetColumnById("TotaliFemra");
    if (totaliFemra != undefined) {
        if (e.validationInfo[totaliFemra.index].value < 0) {
            e.validationInfo[totaliFemra.index].isValid = false;
            e.validationInfo[totaliFemra.index].errorText = "Numri i punonjesve femra nuk mund te jete negativ";
        }
    }
}

function startEditing(s, e) {
    editingIndex = e.visibleIndex;//rreshti qe po editohet
}
function ItemClickMenu(s, e) {
    menu_click(s,e);
}
function ClickButtonOk(s, e) {
    popFshi.Hide();
    Utils.shfaqLoadingGif();;
}
function ClickButtonCancel(s, e) {
    popFshi.Hide();
}
function cmbKonfigurimiSelectedIndexChanged(s, e) {
    ndryshoKonfigurimin()
}
function gvListaPasqyratRowDblClick(s, e) {
    OnGridDoubleClick(e.visibleIndex); 
    kaloTab=true; 
}

function gvListaPasqyratFocusedRowChanged(s, e)
{
    mbush=true;
}
function gvListaPasqyratBeginCallback(s, e)
{
    BeginCallback(s, e);
}