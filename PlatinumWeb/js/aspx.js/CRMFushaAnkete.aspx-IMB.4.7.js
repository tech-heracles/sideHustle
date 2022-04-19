; $(document).ready(function (e) {
    var idNdermarrje = hfState.Get("idNdermarrje");
    var idPerdoruesi = hfState.Get("idPerdoruesi");
    var idVitNdermarrje = hfState.Get("idVitNdermarrje");
    myFaqeCelje.krijoMenuPerCRM(idPerdoruesi, idNdermarrje, idVitNdermarrje);
    $(window).on('load', function () {
        Init();
    });
    
});

var editmode = false;
var indexEdit = -1;
var indexModifiko;

//per filtrat
function checkText(s, e) {
    myMenu.checkText(s, e);
} var btnFiltrat;
function aplikoFiltra(s, e) {
    $('#hfRuaj').val('Filtra'); btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvFushaAnkete, "", '');
}
function textChanged(s, e) {
    myMenu.textChanged(s, e);
}

function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ?
												            evt.keyCode : evt.charCode;
}

function Init() {
    changeName();
    ASPxMenu1.GetItemByName('Ruaj').SetVisible(false);
    myMenu.menuSipasTeDrejtaCeljeVogel($("#hfRuaj"), hfTeDrejta);
    editmode = false;
    indexEdit = -1;
}
function menu_click(s, e) {
    var hfRuaj = $('#hfRuaj');
    myMenu.menu_click_celjevogel(s, e, hfRuaj, gvFushaAnkete, hfTeDrejta);
}

function changeName() {
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(myMesazh.EndRequestTimer);
    prm.add_endRequest(EndRequesHandler);
}
function EndRequesHandler(sender, args) {
    var hf = $("#hfStatusi");
    var hfKontrollet = $('#hfKontrollet');
    var hfShtimModifikim = $('#hfShtimModifikim'); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
    var hfId = $('#hfId');  //hidden fieldi qe ruan id  e rreshtit te selektuar

    indexModifiko = myFaqeCelje.EndRequestHandlerNew(sender, args, hf, hfKontrollet, undefined, hfShtimModifikim, hfId, indexModifiko, undefined, gvFushaAnkete, "2004", undefined, hfTeDrejta)

    // var isLidhur = ($("#hfLidhur").val().toLowerCase() === 'true');
    // enable(false, isLidhur);
}
function callWebservice() {
    var emer = 'CRMFushaAnkete.aspx';
    $.ajax({        
        url: Utils.getServerApiUrl("Rregjistrime", "eshteVeprimILejuar"),
        data: JSON.stringify({ emerKomponente: emer, veprimi: 'Modifiko' })
    }).done(SucceededCallback);
}

function BeginCallback(s, e) {
    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}

function SucceededCallback(result) {
    if (result == "true") {
        $.ajax({
            url: Utils.getServerApiUrl("Rregjistrime", "kaVeprimeOpsion"),
            data: JSON.stringify({ idopsioni: indexModifiko })
        }).done(vazhdoModifikim);
    }
    else {
        myMesazh.ShtoMesazhGabimi("Nuk ke te drejta per te kryer kete veprim");
    }
}

function vazhdoModifikim(result) {    
    if (result !== "true") {
        switchEditMode(indexModifiko);
    }
    else {
        myMesazh.ShtoMesazhGabimi('Ky opsion është i lidhur dhe nuk mund të modifikohet!');
    }    
}

function switchEditMode(index)
{
    $("#hfRuaj").val('Modifiko');
    ASPxMenu1.GetItemByName('Ruaj').SetVisible(true);
    myMenu.menuSipasTeDrejtaCeljeVogel($("#hfRuaj"), hfTeDrejta);
    gvFushaAnkete.StartEditRow(index);
    indexEdit = index;
}



function EndCallbackGrida(s, e) {
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "merrMesazhNgaSesioni"),
        data: JSON.stringify({})
    }).done(SucceededCallbackMesazhi);
}

function SucceededCallbackMesazhi(result) {
    if (result != null) {
        var arr = result.split(':');
        if (arr[1] == "Green") {
            myMesazh.ShtoMesazhSuksesi(arr[0]);
            ASPxMenu1.GetItemByName('Ruaj').SetVisible(false);
        }
        else if (arr[1] == "Red")
            myMesazh.ShtoMesazhGabimi(arr[0]);
    }
}
function Click_ButtonOk(s, e) {
    popFshi.Hide();
    Utils.shfaqLoadingGif();;
}
function Row_DblClick(s, e) {
    callWebservice();
    indexModifiko=e.visibleIndex;    
}