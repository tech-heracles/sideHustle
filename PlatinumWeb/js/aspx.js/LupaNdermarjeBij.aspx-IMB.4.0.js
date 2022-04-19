function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvLupaNdermarje, "691", '');
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
        myMesazh.shtoHandler();
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(EndRequestHandler);
        myFaqeCelje.shtoHandlerSession();
        //gvLupaNdermarje.SelectRowOnPage(0, true);
        //gvLupaNdermarje.SetFocusedRowIndex(0);
        btnOk.Focus();

    }
    catch (err) {
        ///alert('gabim');    
    }
}

document.onkeydown = ProcessKeyPress;

function ProcessKeyPress() {
    var currentIndex = gvLupaNdermarje.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaNdermarje.GetVisibleRowsOnPage() - 1) {
            gvLupaNdermarje.SetFocusedRowIndex(0);
        }
        else {
            gvLupaNdermarje.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaNdermarje.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {


    }
}


function menu_click(s, e) {

    if (e.item.name == "OK") {
        e.processOnServer = false;
        if (gvLupaNdermarje.GetSelectedRowCount() == 0) {
            myMesazh.ShtoMesazhGabimi('Zgjidhni te pakten nje ndermarje bij');
            return;
        }

        if (Utils.getUrlVar('vjenNga') == "KodifikimArtikulli")
            window.parent.gvKodifikimArtikulli.GetSelectedFieldValues('IdKodifikimi', SuccededCallback);
        else if (Utils.getUrlVar('vjenNga') == "LlojDifekti")
            window.parent.gvLlojDifekti.GetSelectedFieldValues('Id', SuccededCallbackLlojDifekti);
        else if (Utils.getUrlVar('vjenNga') == "StatusRiparimi")
            window.parent.gvStatusRiparimi.GetSelectedFieldValues('Id', SuccededCallbackLlojDifekti);
        else
            if (Utils.getUrlVar('vjenNga') == "Artikulli")
            window.parent.ASPxGridView_Artikull.GetSelectedFieldValues('IdArtikulli', SuccededCallbackArt);
        else if (Utils.getUrlVar('vjenNga') == "Cmime") {
            window.parent.merrTeDhena();

            window.parent.ruajTeDhenaJson();
            $('#hfCmimi').val( window.parent.$('#hfCmimi').val());
            $('#hfCmimi2').val(window.parent.$('#hfCmimi2').val());
            $('#hfDtFillimi').val(window.parent.$('#hfDtFillimi').val());
            $('#hfDtMbarimi').val(window.parent.$('#hfDtMbarimi').val());
            $('#hfSasiMin').val(window.parent.$('#hfSasiMin').val());
            $('#hfSasiMax').val(window.parent.$('#hfSasiMax').val());
            $('#hfReshtaTeSelektuar').val(window.parent.$('#hfReshtaTeSelektuar').val());

            btn.DoClick();
            Utils.shfaqLoadingGif();;

        }
        else if ((Utils.getUrlVar('vjenNga') == "Shto_KF") || (Utils.getUrlVar('vjenNga') == "Raporti") || (Utils.getUrlVar('vjenNgaRaporti') == "true") || (Utils.getUrlVar('vjenNga') == "AB"))
            OnGridSelectionChanged()

    }
    else if (e.item.name == 'Anullo') {
        window.parent.popupUniversal.Hide();
    }
}
function OnGridSelectionChanged() {
    gvLupaNdermarje.GetSelectedFieldValues('IdNdermarrje;NdermarrjeKodi', OnGridSelectionComplete);
}
function OnGridSelectionComplete(values) {
    var s = new String();
    //s += values[0];
    s = s + values[0];
    var vl = s.split(",");
    if (Utils.getUrlVar('vjenNga') == "Shto_KF") {
        //window.parent.btneNiveli.SetText(vl[2]);
        var id = vl[0];
        var kodi = vl[1];

        window.parent.cmbBij.SetSelectedIndex(window.parent.cmbBij.AddItem(kodi, id)); window.parent.popupUniversal.Hide();
    }

    else if ((Utils.getUrlVar('vjenNga') == "Raporti" || Utils.getUrlVar('vjenNgaRaporti') == "true") && Utils.getUrlVar('KodeBashke') != "true") {
        window.parent.btneKompania.SetValue(vl[1]);
        window.parent.btneKompania.SetFocus(true);
        window.parent.popupUniversal.Hide();
    }
    else if (Utils.getUrlVar('vjenNgaRaporti') == "true" && Utils.getUrlVar('KodeBashke') == "true") {
        var kodet = Utils.ktheVleratESelektuaraTeBashkuara(values, 1, ",")
        window.parent.btneKompania.SetValue(kodet);
        window.parent.btneKompania.SetFocus(true);
        window.parent.popupUniversal.Hide();
    } else {
        var kodet = Utils.ktheVleratESelektuaraTeBashkuara(values, 1, ",")
        window.parent.cmbNdermarrje.SetValue(kodet);
        window.parent.popupUniversal.Hide();
    }
    if (Utils.getUrlVar('vjenNga') == 'AB') {
        window.parent.FiltroRaport();
    }

}
var rez = new Array();
function SuccededCallback(result) {
    rez = result;
    window.parent.gvKodifikimArtikulliGr2.GetSelectedFieldValues('IdKodifikimi', SuccededCallback2);
}
function SuccededCallback2(result) {
    for (i = 0; i < result.length; i++)
        rez.push(result[i]);
    $('#hfId').val(JSON.stringify(rez));
    btn.DoClick();
    Utils.shfaqLoadingGif();;
}
function SuccededCallbackLlojDifekti(result) {
    $('#hfId').val(JSON.stringify(result));
    btn.DoClick();
    Utils.shfaqLoadingGif();;
}
function SuccededCallbackArt(result) {
    $('#hfId').val(JSON.stringify(result));
    btn.DoClick();
    Utils.shfaqLoadingGif();;
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
        gvLupaNdermarje.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});
$(window).bind('resize', function () {//po

    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaNdermarje.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');

function EndRequestHandler(sender, args) {
    var hf = $("input[id$='hfStatusi']");
    var hfMesazhi = JSON.parse($("input[id$='hfMesazhi']").val());
    if (hf.val() == "true" && hfMesazhi.Tipi == 1) {
        myMesazh.ShtoMesazhSuksesi(hfMesazhi.PershkrimMesazhi);
        timeout = setTimeout(function () {
            window.parent.popupUniversal.Hide(); //  gvQendra.PerformCallback();
        }, 3000);
    }
    else myMesazh.ShtoMesazhSesioni(hfMesazhi);
}