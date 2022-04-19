var pageState = {
    hyrje_dalje: "hyrje",
    idNdermarrje: 0,
    guidString: "",
    selektoriGrides: "#rowed5",
    lidhur: false,
    artikujTePerbereSasi: new Array(),
    artikujMeSasi: new Array(),
    IdMag: 0,
    beepIntervalId: 0,
    KontrollSinkron: true,
    ModifikoSasi: true,
    Shitje: true,
    Id: 0,
    Kthim: false,
    IdKthimesh: new Array(),
    NrTotalSerial: 0,
    IdSeti: 0
};
var arrayIdKolonaGrides = new Array();
var arrayPershkrimiKolonaGrides = new Array();
var arrayVisibleKolonaGrides = new Array();
var arrayWidthKolonaGrides = new Array();
var arrayReadOnlyKolonaGrides= new Array();
var arrayRenditjeKolonaGrides= new Array();
var KaNdryshime = false;

$(document).keydown(function (e) {
    if (e.which == 13) {
        e.preventDefault();
    }
});


$(document).ready(function () {
    
    pageState.hyrje_dalje = Utils.getUrlVar("hyrje_dalje");
    pageState.guidString = hfState.Get("guidString");
    pageState.lidhur = Utils.getUrlVar("lidhur") == "true";
    pageState.idNdermarrje = hfState.Get("IdNdermarrje");
    pageState.IdMag = Utils.getUrlVar("IdMag");
    pageState.ModifikoSasi = Utils.getUrlVar("MNSA") == "true";
    pageState.KontrollSinkron = Utils.getUrlVar("MenyreKontrollSeriali") === "true";
    pageState.Shitje = Utils.getUrlVar("Shitje") === "true";
    pageState.Kthim = Utils.getUrlVar("Kthim") === "true";
    pageState.Id = Utils.getUrlVar("Id");
    pageState.MerrMagazinePerberesi = Utils.getUrlVar("KGJAPMR");
    pageState.IdSeti = Utils.getUrlVar("IdSeti");
    VendosNrSerialesh();
    var MerrArtikujSet = Utils.getUrlVar("MerrArtikujSet");
    if (MerrArtikujSet == "true" && pageState.Shitje)
        VendosArtikujSetNgaShitja();
    if (MerrArtikujSet == "true" && !pageState.Shitje)
        VendosArtikujSetNgaMagazina();
    if (!pageState.ModifikoSasi)
        MerrArtikujDheSasitePerkatese();

    if (pageState.Kthim)
        VendosIdKthimesh();

    var colGridaTrupi = $.parseJSON(hfState.Get("colGridaTrupi"));
    if (pageState.hyrje_dalje == "hyrje" || pageState.Kthim)
        $("#dvFillim").hide();
    else
        $("#dvFillim").show();
    hfState.Remove("colGridaTrupi");
    myJQGrid.formArrayKolGridesNew(colGridaTrupi, arrayIdKolonaGrides, arrayPershkrimiKolonaGrides, arrayVisibleKolonaGrides, arrayReadOnlyKolonaGrides, false,//lidhur
         arrayWidthKolonaGrides, [], arrayRenditjeKolonaGrides);
    myFaqeCelje.shtoHandlerSession();
   
    callWsMerrTrupDokumenti();
    Checked();
});

function VendosArtikujSetNgaShitja() {
    if (!window.parent) return;

    var artikujt = window.parent.memoryArt.GetAll().filter(function (art) { return art.Klasa === 4; });
    var rreshta = window.parent.$(window.parent.pageState.gridaSelector).getTeDhenaRreshti().filter(function (s) { return s.txtIdKodi != '' && s.txtIdKodi == pageState.IdSeti });
    
    for (var i = 0; i < rreshta.length; i++)
        for (var j = 0; j < artikujt.length; j++)
            if (rreshta[i].txtIdKodi == artikujt[j].IdArtikulli)
                pageState.artikujTePerbereSasi.push({ IdSeti: artikujt[j].IdArtikulli, Sasi: rreshta[i].txtSasia, Mag: rreshta[i].txtMagazina, IdArtikulli: 0 });
    
   
}

function VendosIdKthimesh() {
    if (!window.parent) return;

    var data = window.parent.$(window.parent.pageState.gridaSelector).getTeDhenaRreshti().filter(function (s) { return s.txtIdKodi != '' });
    for (var i = 0; i < data.length; i++) {
        pageState.IdKthimesh.push(data[i].txtIdTrupiKthim);
    }
}


function VendosArtikujSetNgaMagazina() {
    if (!window.parent) return;

    var artikujt = window.parent.colPerberesList.filter(function (s) { return s.IdSeti == pageState.IdSeti });
    var rreshta = window.parent.$(window.parent.pageState.gridaSelector).getTeDhenaRreshti();
    var ids = window.parent.$(window.parent.pageState.gridaSelector).getDataIDs();
    for (var i = 0; i < rreshta.length; i++) {
        if (rreshta[i].txtIdArtikullSet == '')
            continue;
        for (var j = 0; j < artikujt.length; j++)
            if (ids[i] == artikujt[j].IdRreshti) {
                    pageState.artikujTePerbereSasi.push({ IdSeti: artikujt[j].IdSeti, Sasi: artikujt[j].SasiaSet, Mag: rreshta[i].txtMagazina, IdArtikulli: artikujt[j].IdArtikulli });
                 
                break;
            }
    }
}


function MerrArtikujDheSasitePerkatese() {

    pageState.artikujMeSasi = window.parent.MerrArtikujDheSasitePerkatese();
}

function ndryshoImazhin(nr, index) {     //po
    myJQGrid.ndryshoImazhin(nr, index);
}


function menu_click(s, e) {
    e.processOnServer = false;
    switch (e.item.name) {
        case "Anullo":
            AnulloClick();
            break;
        case "OK":
            OkClick();
           
            break;
    }
}

function AnulloClick() {

    if (!KaNdryshime){
        MbyllLupeSerialesh();
        return;
    }
    
    window.parent.myMesazh.ShtoMesazh({
        type: "confirm",
        UseCancelButton: true,
        text: hfState.Get("msgNdryshimeTeParuajtura"),
        modal: true,
        layout: "center",
        idGjuha: pageState.idGjuha,
        okClick: function (noty) {
            MbyllLupeSerialesh();
            return;
        },
        cancelClick: function (noty) {
            MbyllLupeSerialesh();
            return;
        }
    });    
}

function MbyllLupeSerialesh() {
    $.ajax({
        pritPergjigje: false,
        showLoading: false,
        data: JSON.stringify({
            guidString: pageState.guidString,
            kaNdryshime: KaNdryshime
        }),
        url: Utils.getServerApiUrl("SerialeUnike", "MbyllLupeSerialesh")
    });
    window.parent.popupSerialet.Hide(); 
}

function OkClick() {
    if (kaRreshtaMeProbleme()) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgSerialMeProbleme"));
        return;
    }
    $.ajax({
        pritPergjigje: true,
        showLoading: true,
        data: JSON.stringify({
            guidString: pageState.guidString
        }),
        url: Utils.getServerApiUrl("SerialeUnike", "MergeSerialeTeReja")
    }).done(function (result) {
        if (result) {
            if (pageState.ModifikoSasi)
                window.parent.ShtoArtikujPerSerialet(result.artikujt, result.idArtFshire, result.NrSeriale);
            else
                window.parent.SetKaSeriale(result.artikujt, result.NrSeriale);
        }
        window.parent.popupSerialet.Hide();
    })
}

function Checked ()
{
        txtSasia.SetEnabled(cbAutomatik.GetChecked());
        btnGjenero.SetEnabled(cbAutomatik.GetChecked());
}


function inicializoGride(rreshtat) {
    var classes = '';
    if (pageState.hyrje_dalje === 'hyrje')
        classes = 'uigray';
    var arrayPershkrime = [arrayPershkrimiKolonaGrides[0], arrayPershkrimiKolonaGrides[1], arrayPershkrimiKolonaGrides[2], arrayPershkrimiKolonaGrides[3], arrayPershkrimiKolonaGrides[4], arrayPershkrimiKolonaGrides[5]];
    var arrayModel = [
        { name: arrayIdKolonaGrides[0], index: arrayIdKolonaGrides[0], width: arrayWidthKolonaGrides[0], hidden: arrayVisibleKolonaGrides[0], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemSeriali, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[1], index: arrayIdKolonaGrides[1], width: arrayWidthKolonaGrides[1], hidden: arrayVisibleKolonaGrides[1], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: function (value) { return myElemTextBox(value, 1); }, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[2], index: arrayIdKolonaGrides[2], width: arrayWidthKolonaGrides[2], hidden: arrayVisibleKolonaGrides[2], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: function (value) { return myElemTextBox(value, 2); }, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[3], index: arrayIdKolonaGrides[3], width: arrayWidthKolonaGrides[3], hidden: arrayVisibleKolonaGrides[3], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: function (value) { return myElemTextBox(value, 3); }, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[4], index: arrayIdKolonaGrides[4], width: arrayWidthKolonaGrides[4], hidden: arrayVisibleKolonaGrides[4], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: function (value) { return myElemTextBox(value, 4); }, custom_value: myJQGrid.myValueTextBox } },
        { name: arrayIdKolonaGrides[5], index: arrayIdKolonaGrides[5], width: arrayWidthKolonaGrides[5], hidden: arrayVisibleKolonaGrides[5], classes: classes, sortable: false, editable: true, edittype: 'custom', editoptions: { custom_element: myElemButtonFshi, custom_value: myValueButtonFshi }, hidedlg: true }
    ];

    
        
    var gridParams = {
        emergride: pageState.selektoriGrides,
        emerEditorKodi: "txtSeriali",
        arrayPershkrime: arrayPershkrime,
        lidhur: pageState.lidhur || pageState.hyrje_dalje == "hyrje",
        arrayModel: arrayModel,
        widthi: $('#divgride2').width(),
        subgrid: false,
        mosshtorresht: false,
        lostFocusKoloneFundit: lostFocusKoloneFundit,
        resetRreshtKorent: resetRreshtKorent,
        readOnly: pageState.hyrje_dalje == "hyrje",
        data: rreshtat
    };
    myJQGrid.initGride(gridParams);
   
}


function lostFocusKoloneFundit() {
    $(pageState.selektoriGrides).lostFocusKoloneFundit();
}

function myElemSeriali(value) {
    disabled = arrayReadOnlyKolonaGrides[0] == 'True' || pageState.hyrje_dalje == "hyrje";
    return myJQGrid.myElemEmertimi(value, disabled, $(pageState.selektoriGrides).getLastSel2(), arrayIdKolonaGrides[0], null, txtSerialiKeydown);

}

function myElemTextBox(value, i) {
    return myJQGrid.myElemEmertimi(value, arrayReadOnlyKolonaGrides[i] == 'True', $(pageState.selektoriGrides).getLastSel2(), arrayIdKolonaGrides[i]);
}

function myValueButtonFshi(elem, operation, value) {
    return myJQGrid.myValueButtonFshi(percaktoButonFshiDisabled(), $(pageState.selektoriGrides).getLastSel2(), pageState.selektoriGrides);
}

/*
Function: myElemButon

Nderton nje buton per te fshire nje rresht te grides
*/
function myElemButtonFshi() {
    return myJQGrid.myElemButtonFshi(percaktoButonFshiDisabled(), $(pageState.selektoriGrides).getLastSel2(), pageState.selektoriGrides, lostFocusKoloneFundit);
}

function fshiClicked(index) {//po
    var grida = $(pageState.selektoriGrides);
    var seriali = grida.getTekstQelize('txtSeriali', index);
    var Artikulli = grida.getTekstQelize('txtArtikulli', index);
    if (!grida.merrTeDhenaPerQelizen('txtSeriali', index, "Gabim")) {
        HiqSerial(seriali);
        //UpdateNrSerial(false);
    }
    if (Artikulli && Artikulli.length != 0)
        KaNdryshime = true;
    myJQGrid.fshiClicked(index, pageState.selektoriGrides, inicializoGride);
}
function HiqSerial(seriali) {
    if (seriali && seriali.length != 0) {
        $.ajax({
            pritPergjigje: false,
            showLoading: false,
            data: JSON.stringify({
                guidString: pageState.guidString,
                seriali: seriali
            }),
            url: Utils.getServerApiUrl("SerialeUnike", "HiqSerial")
        });
        UpdateNrSerial(false);
    }

}
function callWsMerrTrupDokumenti() {
    $.ajax({
        pritPergjigje: true,
        showLoading: true,
        data: JSON.stringify({
            idNdermarrje: pageState.idNdermarrje,
            guidString: pageState.guidString,
            idSeti: pageState.IdSeti
        }),
        url: Utils.getServerApiUrl("SerialeUnike", "MbushTrupKategoriSeriali")
    }).done(function (result) {
        MbushTrupDokumenti(result, false);
    });
}

function MbushTrupDokumenti(colTrupi, mergeEkzistuese) {
    var grida = $(pageState.selektoriGrides);
    grida.setLastSel2(1);
    
    var rreshtat = KrijoRreshtat(colTrupi, mergeEkzistuese);

    if (pageState.hyrje_dalje == "hyrje"){
        inicializoGride(rreshtat);
            return;
    }
    if (!mergeEkzistuese)
        inicializoGride();

    var nrRreshtash = rreshtat.length;
    grida.jqGrid('clearGridData');
    grida[0].addJSONData(rreshtat);
    if (pageState.hyrje_dalje == "hyrje") {
        grida.setLastSel2(-1);
    }
    else {
        grida.setMaxLastSel(nrRreshtash);
        var data = grida.getDataIDs();
        for (var i = 0; i < data.length; i++) {
            grida.vendosTeDhenaPerQelizen("txtSeriali", data[i], "IshVlera", grida.getTekstQelize("txtSeriali", data[i]));
            grida.ngjyrosEkzistonFushat(data[i], true);
        }
        grida.shtoNeRreshtinEPareBosh("txtSeriali", percaktoButonFshiDisabled(), pageState.selektoriGrides);
    }
    pageState.NrTotalSerial = nrRreshtash
    VendosNrSerialesh();
}


function txtSerialiKeydown(id, idRreshti, event) {
    if (event.which != 13 || window.parent.myMesazh.kaPyetjeTeHapur())//butoni enter
        return;
    ShtoSerial(id, idRreshti, 1, false);
}

function Gjenero() {
    var sasiaTxt = txtSasia.GetText();
    if (sasiaTxt.length == 0 || sasiaTxt == "0") {
        myMesazh.ShtoMesazhGabimi("Ju lutem vendosni sasine qe do gjenerohet!");
        return;
    }
    var sasia = parseInt(sasiaTxt);
    if (sasia <= 0 || isNaN(sasia)) {
        myMesazh.ShtoMesazhGabimi("Ju lutem vendosni nje sasi pozitive!");
        return;
    }
    if (!Utils.KanePerfunduarWs()) {
        myMesazh.ShtoMesazh({ type: "alert", text: 'Ju lutem prisni pak sekonda...', timeout: false });
        return;
    }
    ShtoSerial("txtSeriali", $(pageState.selektoriGrides).getLastSel2(), sasia, true);
}

function ShtoSerial(id, idRreshti, sasia, gjenerimAutomatik) {
    var grida = $(pageState.selektoriGrides);
    var seriali = grida.getTekstQelize(id, idRreshti);
    seriali = seriali.indexOf('/') > -1 ? seriali.split('/')[1] : seriali;
    var serialiIVjeter = grida.merrTeDhenaPerQelizen(id, idRreshti, "IshVlera");
    if (seriali == "" || (seriali === serialiIVjeter && sasia == 1 && !grida.merrTeDhenaPerQelizen(id, idRreshti, "Gabim")))
        return;
    callWsShtoSerial(seriali, idRreshti, serialiIVjeter, sasia, gjenerimAutomatik);
}


function callWsShtoSerial(seriali, idRow, serialiIVjeter, sasia, gjenerimAutomatik) {
    $.ajax({
        pritPergjigje: true,
        async: !pageState.KontrollSinkron,
        showLoading: Utils.getUrlVar("MenyreKontrollSeriali") === "true" || sasia > 1,
        data: JSON.stringify({
            idNdermarrje: pageState.idNdermarrje,
            guidString: pageState.guidString,
            seriali: seriali,
            date: window.parent.editorData.GetDate(),
            serialiIVjeter: serialiIVjeter ? serialiIVjeter : "",
            sasia: sasia,
            ArtikujSet: JSON.stringify(pageState.artikujTePerbereSasi),
            IdMag: pageState.IdMag,
            gjenerimAutomatik: gjenerimAutomatik,
            KontrolloSasi: !pageState.ModifikoSasi,
            artikujMeSasi: JSON.stringify(pageState.artikujMeSasi),
            RiktheSerialTevjeter: pageState.KontrollSinkron,
            Shitje: pageState.Shitje,
            Id: pageState.Id,
            MerrMagazinePerberesi: pageState.MerrMagazinePerberesi,
            Kthim: pageState.Kthim,
            IdKthimesh: JSON.stringify(pageState.IdKthimesh)
        }),
        url: Utils.getServerApiUrl("SerialeUnike", "ShtoSerial")
    }).done(function (result) { DoneWsShtoSerial(result, idRow, serialiIVjeter, seriali); });
}

function DoneWsShtoSerial(result, idRow, serialiIVjeter, seriali) {
    if (!result)
        return;
    var grida = $(pageState.selektoriGrides);
    if (!result.Mesazh.Status) {
        
        if (pageState.KontrollSinkron) {
            pageState.beepIntervalId = setInterval(function () { beep() }, 200);
            window.parent.myMesazh.ShtoMesazh({
                type: "confirm",
                UseCancelButton: false,
                text: result.Mesazh.PershkrimMesazhi,
                modal: true,
                layout: "center",
                idGjuha: 1,
                okClick: function (noty) {
                    HiqRreshtNgaGrida(idRow);
                    clearInterval(pageState.beepIntervalId);
                }
            });
        }
        else {
            HiqSerial(grida.merrTeDhenaPerQelizen("txtSeriali", idRow, "IshVlera"));
            vendosTedhenaNeRresht(grida, idRow, {Seriali: seriali}, false);
            grida.shtoNeRreshtinEPareBosh("txtSeriali", percaktoButonFshiDisabled(), pageState.selektoriGrides);
            myMesazh.ShtoMesazhGabimi(result.Mesazh.PershkrimMesazhi);
            beep();
        }
        Utils.hiqLoadingGif();
        return;
    }

    UpdateNrSerial(true);
    KaNdryshime = true;
    if (result.MesazhSerialiRradhe)
        myMesazh.ShtoMesazhInformues(result.MesazhSerialiRradhe.PershkrimMesazhi);

    var readOnlyKolonaFshi = percaktoButonFshiDisabled();
    
    if (result.RreshtaPerGride.length == 1 && (Utils.IsNullOrEmpty(serialiIVjeter) || serialiIVjeter != seriali)) {
        vendosTedhenaNeRresht(grida, idRow, result.RreshtaPerGride[0], true);
        idRow = grida.shtoNeRreshtinEPareBosh("txtSeriali", percaktoButonFshiDisabled(), pageState.selektoriGrides);
    } else if (result.RreshtaPerGride.length > 1 && !Utils.IsNullOrEmpty(serialiIVjeter) && serialiIVjeter != seriali) {
        vendosTedhenaNeRresht(grida, idRow, result.RreshtaPerGride[0], true);
        result.RreshtaPerGride.shift();
        MbushTrupDokumenti(result.RreshtaPerGride, true);
    }
    else {
        MbushTrupDokumenti(result.RreshtaPerGride, true);
    }
    Utils.hiqLoadingGif();
}

function kaRreshtaMeProbleme()
{
    var grida = $(pageState.selektoriGrides);
    var ids = grida.getDataIDs();
    for (var i = 0; i < ids.length; i++) {
        if(grida.merrTeDhenaPerQelizen("txtSeriali", ids[i], "Gabim"))
            return true;
    }
    return false;
}

function vendosTedhenaNeRresht(grida, idRow, rreshtiPerGride, rreshtISakte) {
    grida.vendosTeDhenaPerQelizen("txtSeriali", idRow, "IshVlera", rreshtiPerGride.Seriali);
    grida.setTekstQelize("txtSeriali", idRow, rreshtiPerGride.Seriali);
    grida.setTekstQelize("txtArtikulli", idRow, rreshtiPerGride.Artikulli);
    grida.setTekstQelize("txtLlojSeriali", idRow, rreshtiPerGride.Kategoria);
    grida.setTekstQelize("txtSasia", idRow, rreshtiPerGride.Sasia);
    grida.setTekstQelize("txtArtikulliSet", idRow, rreshtiPerGride.Seti);
    grida.ngjyrosEkzistonFushat(idRow, rreshtISakte);
    grida.vendosTeDhenaPerQelizen("txtSeriali", idRow, "Gabim", !rreshtISakte);
}

function resetRreshtKorent(idRreshti) {
    var grida = $(pageState.selektoriGrides);
    var idRow = grida.getLastSel2();
    if (idRreshti == idRow && $('#txtSeriali' + idRow).val() != undefined) {
        grida.setTekstQelize("txtSeriali", idRreshti, '');
        grida.setTekstQelize("txtArtikulli", idRreshti, '');
        grida.setTekstQelize("txtLlojSeriali", idRreshti, '');
        grida.setTekstQelize("txtSasia", idRreshti, '');
        grida.setTekstQelize("txtArtikulliSet", idRreshti, '');
        return;
    }
    grida.jqGrid('delRowData', idRreshti);    
    return;
}

function HiqRreshtNgaGrida(idRow) {
    var grida = $(pageState.selektoriGrides);
    var serialiIVjeter = grida.merrTeDhenaPerQelizen("txtSeriali", idRow, "IshVlera");
    grida.setTekstQelize("txtSeriali", idRow, serialiIVjeter ? serialiIVjeter : "");
    grida.selektoRreshtin(idRow, true);
    grida.setFocus("txtSeriali", idRow);
}

function KrijoRreshtat(colTrupi, mergeEkzistuese) {
    if (!colTrupi) return new Array();
    var rreshtat = new Array();

    var isReadOnlyKolonaFshi = percaktoButonFshiDisabled();
    var grida = $(pageState.selektoriGrides);
    var ekzistues = grida.getTeDhenaRreshti().filter(function (s) { return s.txtArtikulli != '' });
    var idRreshti = mergeEkzistuese ? ekzistues.length + 1 : 1;

    if (mergeEkzistuese) {
        ekzistues.forEach(function (element, index) {
            element.txtFshi = myJQGrid.myValueButtonFshi(isReadOnlyKolonaFshi, index + 1, pageState.selektoriGrides);
        })
    }

    for (var i = 0; i < colTrupi.length; i++) {
        rreshtat.push(KrijoRresht(colTrupi[i], isReadOnlyKolonaFshi, idRreshti));
        idRreshti++;
    }
    if(!mergeEkzistuese)
        return rreshtat;

    return ekzistues.concat(rreshtat);
}

function KrijoRresht(clsTrupi, readOnlyKolonaFshi, idRreshti) {
    return {
        txtSeriali: clsTrupi.Seriali,
        txtArtikulli: clsTrupi.Artikulli,
        txtLlojSeriali: clsTrupi.Kategoria,
        txtSasia: clsTrupi.Sasia,
        txtArtikulliSet: clsTrupi.Seti,
        txtFshi: myJQGrid.myValueButtonFshi(readOnlyKolonaFshi, idRreshti, pageState.selektoriGrides)
    };
}

function percaktoButonFshiDisabled() {
    return arrayReadOnlyKolonaGrides[arrayReadOnlyKolonaGrides - 1] === 'True' || pageState.lidhur || pageState.hyrje_dalje == "hyrje";
}

function beep() {
    (new
      Audio(
      "data:audio/wav;base64,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 /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////VEFHAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAU291bmRib3kuZGUAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAMjAwNGh0dHA6Ly93d3cuc291bmRib3kuZGUAAAAAAAAAACU="
      )).play();
}

function UpdateNrSerial(rritje) {
    if (rritje)
        pageState.NrTotalSerial += 1;
    else {
        if (pageState.NrTotalSerial == 0)
            return
        pageState.NrTotalSerial -= 1;
    }

    VendosNrSerialesh();
}

function VendosNrSerialesh() {
    lblNrSerialesh.SetText("Nr Serialesh: " + pageState.NrTotalSerial);
}