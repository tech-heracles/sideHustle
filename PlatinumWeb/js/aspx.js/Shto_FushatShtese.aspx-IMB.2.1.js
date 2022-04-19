
;

var pageState = {
    colFushat: new Array(),
    //perdoret per te treguar nese duhet te mbushen tabet apo jo kjo perdoret qe tabet te mbushen vetem heren e pare dhe jo pas cdo here qe  kalohet nga nje tab tek tjetri
    mbush: true,
    lidhur: false,
    indexModifiko: 0,

};
var lista;
var kaloTab = false;
$(document).ready(function () {
    $(document).keydown(function (e) {
        switch (e.which) {
            case 13:
                e.preventDefault();
                break;
            case 116: //F5
                window.parent.rifresko = true;
                break;
            case 82:
                if (e.ctrlKey) //ctrl+r
                    window.parent.rifresko = true;
            default:
                break;
        }
    });
    changeName();
});

function checkText(s, e) {
    myMenu.checkText(s, e);
} var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, ASPxGridView_Modelet, "", "");
}
function textChanged(s, e) {
    myMenu.textChanged(s, e);
}
//metodat per te hapur faqen e modifikimit me double click
function OnGridDoubleClick(index) {
    pageState.indexModifiko = index;
    mbushfusha();
}
function menu_click(s, e) {
    hfShtimModifikim = $('#hfShtimModifikim'); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
    hfId = $('#hfId'); //hidden fieldi qe ruan id  e rreshtit te selektuar
    //        myMenu.menu_click(s, e, hfShtimModifikim, hfId, PageControl, false);
    myMenu.menu_click(s, e, hfShtimModifikim, hfId, PageControl, false, false, pageState.indexModifiko, pastrofusha, undefined, undefined, undefined, undefined, undefined);
    if (e.item.name == 'Ruaj') {
        merrTeDhena();
    }
}
//merr te dhenat e rreshtit te selektuar
function mbushfusha() {
    pageState.mbush = false;

    $('#hfShtimModifikim').val("modifikim");
    pageState.indexModifiko = ASPxGridView_Modelet.GetFocusedRowIndex();
    if (pageState.indexModifiko == -1)
        myMesazh.ShtoMesazhGabimi('Duhet te zgjidhni nje model!');
    else
        ASPxGridView_Modelet.GetRowValues(pageState.indexModifiko, 'IdModeliFushaShtese;KodiModeliFushaShtese;PershkrimModeliFushaShtese;IdLlojModeliFushaShtese', OnGetRowValuesMod);
    Utils.shfaqLoadingGif();;
}
//pageState.mbush fushat me te dhenat e rreshtit te selektuar
function OnGetRowValuesMod(values) {
    if ($('#hfShtimModifikim').val() == "shtim")
        return;
    $('#hfId').val(values[0]);
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "callWsGetAutorizimeSipasLlojitDheIdLidhese"),
        data: JSON.stringify({ idLidhese: values[0], kodLloji: 'FushatShtese', idPerdorues: hfState.Get('idPerdoruesi') })
    }).done(SucceededCallbackKtheAutorizime);
    txtKodi.SetText(values[1]);
    txtKodi.SetEnabled(false);
    txtPershkrimi.SetText(values[2]);
    cmbLloji.SetValue(values[3]);
    grid_fushatShtese.PerformCallback(values[0] + ";modifiko");
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "kaVeprimeFusha"),
        data: JSON.stringify({ iddokumenti: values[0] })
    }).done(SucceededCallbackLidhur);
    Utils.hiqLoadingGif();;
    if (kaloTab) {
        PageControl.SetActiveTabIndex(1);
        myMenu.PercaktoMenuSipasTabit(1, hfTeDrejta, $('#hfShtimModifikim'));
    }
}
function SucceededCallbackLidhur(result) {
    pageState.lidhur = result;
}
//pastron fushat per shtim dhe ben aktive fushat
function pastrofusha() {
    txtKodi.SetText('');
    txtKodi.SetEnabled(true);
    txtPershkrimi.SetText('');
    cmbLloji.SetSelectedIndex(2);
    cmbAutorizimi.SetValue(null);
    pageState.lidhur = false;
    cmbLloji.SetEnabled(true);
    grid_fushatShtese.PerformCallback(-1 + ";shto");

}

function SucceededCallbackKtheAutorizime(result) {
    if (result.idLidhese == $('#hfId').val())
        cmbAutorizimi.SetText(result.autorizime);
    else cmbAutorizimi.SetValue(null);
}
/* 
Function: OnGridSelectionChanged

perdoret per te ruajtur indexin e selektimit

Parameters:

e-eventi
*/
function OnGridSelectionChanged(e) {
    //nuk eshte me e nevojshme
}


function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ?
		        evt.keyCode : evt.charCode;
}

function changeName() {

    myFaqeCelje.changeNameRegjistrime('Shto_FushatShtese.aspx', 0);
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(EndRequestHandler); enable();
    myMenu.PercaktoMenuSipasTabit(0, hfTeDrejta, $('#hfShtimModifikim'));
}
/*
Function: EndRequestHandler
    
Thirret nese ruajtja eshte bere ne rregull. Pastron koken e dokumentit dhe array-t e perdorura.
Shiko funksionet  <ndryshoKonfigurimin>.
*/
function EndRequestHandler(sender, args) {
    var hf = $("#hfStatusi");
    var hfKontrollet = $('#hfKontrollet');
    var hfShtimModifikim = $('#hfShtimModifikim'); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
    var hfId = $('#hfId');  //hidden fieldi qe ruan id  e rreshtit te selektuar

    if (hf.val() == "true") {
        if (hfShtimModifikim.val() != "modifikim") {
            pageState.mbush = false;
            hfShtimModifikim.val("shtim"); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
            hfId.val(0); //hidden fieldi qe ruan id  e rreshtit te selektuar
            pageState.indexModifiko = -1; //indexi i reshtit te selektuar      
            pastrofusha();
            hf.val("false");

            if (PageControl.GetActiveTabIndex() != 0) {
                PageControl.SetActiveTabIndex(1);
                myMenu.PercaktoMenuSipasTabit(1, hfTeDrejta, hfShtimModifikim);

            }
            ASPxGridView_Modelet.PerformCallback();

        }
        else {
            hf.val("false");
            if (PageControl.GetActiveTabIndex() != 0) {

                PageControl.SetActiveTabIndex(0); myMenu.PercaktoMenuSipasTabit(0, hfTeDrejta, hfShtimModifikim);
                ASPxGridView_Modelet.PerformCallback();

            }

        }


    }
    else myMenu.PercaktoMenuSipasTabit(PageControl.GetActiveTabIndex(), hfTeDrejta, hfShtimModifikim);
    Utils.hiqLoadingGif();;
}

function valido(s, e) {
    myFaqeCelje.valido(s, e, PageControl, hfTeDrejta, $('#hfShtimModifikim'));
}

function BeginCallback(s, e) {

    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}

var KPF;
function Autorizime_Click() {
    myButtonClickLupa.Autorizime_Click('Zgjidh autorizimet', "", 500, 500);
}


function enable() {


    for (i = 0; i < grid_fushatShtese.cpNoRows; i++) {
        var editor = Utils.ktheKontroll('cmbTipi' + i.toString());
        var editorgj = Utils.ktheKontroll('txtGjatesia' + i.toString());
        if (editor.GetValue() == 0 || editor.GetValue() == 6)
            editorgj.SetText(0);
        if (editor.GetValue() == 0 || editor.GetValue() == 4 || editor.GetValue() == 5 || editor.GetValue() == 6)
            editorgj.SetEnabled(false);
        else editorgj.SetEnabled(true);
        if (pageState.lidhur && Utils.ktheKontroll('txtPershkrimi' + i.toString()).GetText() != '')
            editor.SetEnabled(false);
        pageState.colFushat[i] = MerrFushShteseSipasRreshtitNeGride(i);
    }

}
function ShtoPershkrim(editor, editorgjatesi, editortip, idRreshti) {
    var fushaShteseNew = MerrFushShteseSipasRreshtitNeGride(idRreshti);
    if (editortip.GetValue() == 6) {
        var pershkrimiOld = pageState.colFushat[idRreshti].PershkrimiFushaShtese || '';
        pageState.colFushat[idRreshti] = fushaShteseNew;

        if (pershkrimiOld != fushaShteseNew.PershkrimiFushaShtese) {

            //nese ky rresht i grides eshte tip combo dhe ka ndryshuar pershkrimi ath duhet te updatohet pershkrimi ne te gjithe kombot ku eshte perdorur
            for (var i = 0, count = pageState.colFushat.length; i < count; i++) {
                var fushaShteseCombo = pageState.colFushat[i];
                //pervec fushave listbox te tjerat nuk kane prind
                if (fushaShteseCombo.TipiFushaShtese != 6) continue;

                if ((fushaShteseCombo.PershkrimiFushaShtese || '') == '' && (fushaShteseCombo.AtiTipiFushaShtese || 0) == 0) continue; //fushe e pa plotesuar  
                mbushCombonEPrinditv2(i);
            }



        }
        else mbushCombonEPrinditv2(idRreshti);
    }
    else pageState.colFushat[idRreshti] = fushaShteseNew;

    if (idRreshti == grid_fushatShtese.cpNoRows - 1) {

        merrTeDhena();
        grid_fushatShtese.PerformCallback();
    }

}
//metoda per te marre vlerat e ndryshuara te buxhetit te dyte dhe per te ndryshuar differencen dhe totalin

function ShtoTip(editor, idRreshti) {

    if (editor.GetText() == '') {
        editor.SetFocus();
        MyMesazh.ShtoMesazhGabimi('Jepni Tipin');
    }
    else {
        enable();
        pageState.colFushat[idRreshti] = MerrFushShteseSipasRreshtitNeGride(idRreshti);
        var editorgj = Utils.ktheKontroll('txtGjatesia' + idRreshti.toString());
        if (editor.GetValue() == 0)
            editorgj.SetText(0);
        if (editor.GetValue() == 1)
            editorgj.SetText(255);
        if (editor.GetValue() == 2)
            editorgj.SetText(10);
        if (editor.GetValue() == 3)
            editorgj.SetText(20);
        if (editor.GetValue() == 4)
            editorgj.SetText(10);
        if (editor.GetValue() == 5)
            editorgj.SetText(1);
        if (editor.GetValue() == 6) {
            editorgj.SetText(0);

        }
        if (idRreshti == grid_fushatShtese.cpNoRows - 1) {

            merrTeDhena();
            grid_fushatShtese.PerformCallback();
        }

    }

}
function ShtoGjatesi(editor, idRreshti) {
    if (isNaN(editor.GetText())) {
        editor.SetFocus();
        MyMesazh.ShtoMesazhGabimi('Vlerat e gjatesise duhet te jete numerike');
    }
    else if (editor.GetText() == '') {
        editor.SetFocus();
        MyMesazh.ShtoMesazhGabimi('Jepni vleren e gjatesise');
    }
    else {
        pageState.colFushat[idRreshti] = MerrFushShteseSipasRreshtitNeGride(idRreshti);
        if (idRreshti == grid_fushatShtese.cpNoRows - 1) {

            merrTeDhena();
            grid_fushatShtese.PerformCallback();
        }
    }
}

function ShtoPrind(editor, idRreshti) {
    var fushaShtese = MerrFushShteseSipasRreshtitNeGride(idRreshti);
    pageState.colFushat[idRreshti] = fushaShtese;
    if (fushaShtese.PershkrimiPrindit || '' != '' && fushaShtese.AtiTipiFushaShtese || 0 == 0) {
        var fushaPrind = pageState.colFushat.find(function (item) {
            return item.PershkrimiFushaShtese == fushaShtese.PershkrimiPrindit;
        });
        fushaShtese.AtiTipiFushaShtese = fushaPrind.IdFushaShtese;
    }
    if (idRreshti == grid_fushatShtese.cpNoRows - 1) {
        merrTeDhena();
        grid_fushatShtese.PerformCallback();
    }

}
///merr nga grida nje objekt fushashtese
function MerrFushShteseSipasRreshtitNeGride(idRreshti) {
    ///var fushaShtese = pageState.colFushat[idRreshti];
    //if (fushaShtese == undefined) 
    fushaShtese = {};

    fushaShtese["IdFushaShtese"] = grid_fushatShtese.GetRowKey(idRreshti);
    fushaShtese["TipiFushaShtese"] = Utils.ktheKontroll('cmbTipi' + idRreshti).GetValue();
    fushaShtese["GjatesiaFushaShtese"] = Utils.ktheKontroll('txtGjatesia' + idRreshti).GetText();
    fushaShtese["PershkrimiFushaShtese"] = Utils.ktheKontroll('txtPershkrimi' + idRreshti).GetText();
    fushaShtese["PershkrimiPrindit"] = Utils.ktheKontroll("cmbPrindi" + idRreshti).GetText();
    fushaShtese["AtiTipiFushaShtese"] = Utils.ktheKontroll("cmbPrindi" + idRreshti).GetValue() || 0;
    fushaShtese["Shfaq"] = Utils.ktheKontroll("ckbShfaq" + idRreshti).GetValue();
    fushaShtese["Detyrueshme"] = Utils.ktheKontroll("ckbDetyrueshme" + idRreshti).GetValue();
    fushaShtese["Lejueshme"] = Utils.ktheKontroll("ckbLejueshme" + idRreshti).GetValue();
    fushaShtese["VlereDefault"] = Utils.ktheKontroll('txtVlereDefault' + idRreshti).GetText();
    fushaShtese["Kodi"] = Utils.ktheKontroll('txtKodi' + idRreshti).GetText();
    fushaShtese["PershkrimiEng"] = Utils.ktheKontroll('txtPershkrimiEng' + idRreshti).GetText();
    fushaShtese["Shenime"] = Utils.ktheKontroll('txtShenime' + idRreshti).GetText();
    return fushaShtese;

}
function merrTeDhena() {//merren te dhenat qe ka grida
    var gridDataObject = $('#gridDataObject');
    var numerRreshtashNeGride = grid_fushatShtese.cpNoRows;
    //for (i = 0; i < numerRreshtashNeGride ; i++) {
    //    pageState.colFushat[i] = MerrFushShteseSipasRreshtitNeGride(i);
    //}
    if (numerRreshtashNeGride < pageState.colFushat.length) {       //fshin nga array te gjithe rreshtat qe kane indeks me te madh se indeksi i fundit i grides,per tu siguruar qe nuk ka vlera te mbetura
        pageState.colFushat.splice(numerRreshtashNeGride - 1, pageState.colFushat.length);
    }
    gridDataObject.val(JSON.stringify(pageState.colFushat));
}
function mbushCombonEPrinditv2(idRreshti) {
    var prinderitEVlefshem = merrPrinderit(idRreshti);
    //nese combo ka vetem elementin bosh skip
    if (prinderitEVlefshem.length <= 1 && prinderitEVlefshem[0].Vlera == 0) return;
    var editor = Utils.ktheKontroll("cmbPrindi" + idRreshti);
    editor.BeginUpdate();
    editor.ClearItems();
    for (var i = 0, count = prinderitEVlefshem.length; i < count ; i++) {
        editor.AddItem(prinderitEVlefshem[i].Pershkrimi, prinderitEVlefshem[i].Vlera);
    }

    editor.SetValue(pageState.colFushat[idRreshti].AtiTipiFushaShtese)
    editor.EndUpdate();

}
function mbushCombonEPrindit(idRreshti) {
    var prinderitEVlefshem = merrPrinderit(idRreshti);
    var idRreshtash = [idRreshti];

    //nese key eshte undefiend duhet te merren ne shqyrtim te gjithe rreshtat
    if (idRreshti == undefined) {
        for (i = 0, count = grid_fushatShtese.cpNoRows; i < count; i++)
            idRreshtash[i] = i;
    }

    for (var k = 0, countKeys = idRreshtash.length; k < countKeys; k++) {
        //marrim vetem prinderit e mundshem
        idRreshti = idRreshtash[k];
        var editor = Utils.ktheKontroll("cmbPrindi" + idRreshti);
        var vleraPrindi = undefined;
        var textiPrindi = undefined;
        //editor = new ASPxClientComboBox();
        editor.BeginUpdate();
        editor.ClearItems();


        for (var i = 0, count = prinderitEVlefshem.length; i < count ; i++) {
            editor.AddItem(prinderitEVlefshem[i].Pershkrimi, prinderitEVlefshem[i].Vlera);
            if (prinderitEVlefshem[i].Vlera == vleraPrindi)
                textiPrindi = prinderitEVlefshem[i].Pershkrimi;
        }
        //grid_fushatShtese.GetRowValues(idRreshti, "AtiTipiFushaShtese", function (result) {
        //    vleraPrindi = result;
        //    if ((vleraPrindi || 0) != 0) {
        //        var item = editor.FindItemByValue(vleraPrindi);
        //        if (item != undefined)
        //            editor.SetSelectedItem(item);
        //        else
        //            editor.SetSelectedIndex(editor.AddItem(textiPrindi, vleraPrindi));

        //    }

        //});
        editor.SetValue(pageState.colFushat[idRreshti].AtiTipiFushaShtese)
        //if ((oldVlera || 0) != 0)
        //    Utils.SelectComboItem(editor,)
        editor.EndUpdate();
    }
}
function merrPrinderit(idRreshti) {
    var fushaShtese;
    var prinderitEVlefshem = new Array();
    //marrim gjite vlerat
    // new ASPxClientGridView().GetRowKey()
    for (i = 0, count = idRreshti ; i < count; i++) {
        fushaShtese = pageState.colFushat[i];
        if (fushaShtese.TipiFushaShtese == 6 && (fushaShtese.PershkrimiPrindit || "") == "")
            prinderitEVlefshem.push({ Pershkrimi: fushaShtese.PershkrimiFushaShtese, Vlera: fushaShtese.IdFushaShtese });

    }
    prinderitEVlefshem.push({ Pershkrimi: "", Vlera: 0 });
    return prinderitEVlefshem;
}
function Active_TabChanged(s, e) {
    pageState.indexModifiko = ASPxGridView_Modelet.GetFocusedRowIndex();
    if (pageState.mbush) {
        if (pageState.indexModifiko > 0) {
            OnGridDoubleClick(pageState.indexModifiko);
        }
        else {
            pageState.mbush = false;
            $('#hfShtimModifikim').val('shtim');
            $('#hfId').val(0); pastrofusha();
        }
    }
    kaloTab = false;
    myMenu.PercaktoMenuSipasTabit(e.tab.index, hfTeDrejta, $('#hfShtimModifikim'));

}