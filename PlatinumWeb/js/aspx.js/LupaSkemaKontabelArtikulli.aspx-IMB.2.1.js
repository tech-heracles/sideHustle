function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvLupaSkemaKontArt, "647", '');
}

function textChanged(s, e) {
    myMenu.textChanged(s, e);
}

function BeginCallback(s, e) {
    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}

function Init() {
    try {
       
        myFaqeCelje.shtoHandlerSession();
        gvLupaSkemaKontArt.SetFocusedRowIndex(0);
        var llojiArtUrl = Utils.getUrlVar('llojiart');
        if (llojiArtUrl != undefined)
            if (llojiArtUrl == "afatshkurter")
                gvLupaSkemaKontArt.ApplyFilter('[LlojiArt]=false');
            else
                gvLupaSkemaKontArt.ApplyFilter('[LlojiArt]=true');
        btnOk.Focus();
    }
    catch (err) {
        ///alert('gabim');    
    }
}
document.onkeydown = ProcessKeyPress;
function ProcessKeyPress() {
    var currentIndex = gvLupaSkemaKontArt.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaSkemaKontArt.GetVisibleRowsOnPage() - 1) {
            gvLupaSkemaKontArt.SetFocusedRowIndex(0);
        }
        else {
            gvLupaSkemaKontArt.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaSkemaKontArt.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();

    }
}
function OnGridSelectionChanged() {
    gvLupaSkemaKontArt.GetSelectedFieldValues('KodiSkemaKontabilitetiArtikulli;IdSkemaKontabilitetiArtikulli;NrLlogariInventari;NrLlogariBlerje;NrLlogariShitje;NrLlogariTekTeTretet;NrLlogariShpenzimi;NrLlogariAmortizimi;IdLlogariInventari;IdLlogariBlerje;IdLlogariShitje;IdLlogariTekTeTretet;IdLlogariShpenzimi;IdLlogariAmortizimi;NrLlogariPakesimi;IdLlogariPakesimi', OnGridSelectionComplete);
}
function OnGridSelectionComplete(values) {
    var s = new String();
    //s += values[0];
    s = s + values[0];
    //        alert(s+1);
    //      txtPersh.SetText(s);
    //      //gvLupaSkemaKontArt.PerformCallback();
    //        }
    //        function Return() {
    //         alert(txtPersh.GetText()+2);
    var vl = s.split(",");
    if (window.parent.identifikuesPerPopupSkema === "KonfigurimDokumentash") {
        window.parent.editorSKA.SetValue(vl[1]);
        window.parent.editorSKA.SetFocus();
        //                window.parent.editorLLI.SetText(vl[2]);
        //                window.parent.editorLLB.SetText(vl[3]);
        //                window.parent.editorLLS.SetText(vl[4]);
        //                window.parent.editorLLT.SetText(vl[5]);
        //                window.parent.editorLLSH.SetText(vl[6]);
        window.parent.colAtribute[window.parent.keySK].VlereDefault = vl[1];
        window.parent.pageState.fushaLlogarie.llogariInv.fusha.VlereDefault = vl[8];
        window.parent.pageState.fushaLlogarie.llogariB.fusha.VlereDefault = vl[9];
        window.parent.pageState.fushaLlogarie.llogariSh.fusha.VlereDefault = vl[10];
        window.parent.pageState.fushaLlogarie.llogariTr.fusha.VlereDefault = vl[11];
        window.parent.pageState.fushaLlogarie.llogariShp.fusha.VlereDefault  = vl[12];
        window.parent.pageState.fushaLlogarie.llogariA.fusha.VlereDefault  = vl[13];
        firstPageElementIndex = (10 * (window.parent.grid_kontrollet.cpNoPage));
        lastPageElementIndex = (10 * (window.parent.grid_kontrollet.cpNoPage + 1)) - 1;
       
        if (window.parent.pageState.fushaLlogarie.llogariInv.index >= firstPageElementIndex && window.parent.pageState.fushaLlogarie.llogariInv.index <= lastPageElementIndex)
            window.parent.editorLLI.SetValue(vl[8]);
     
        if (window.parent.pageState.fushaLlogarie.llogariB.index >= firstPageElementIndex && window.parent.pageState.fushaLlogarie.llogariB.index <= lastPageElementIndex)
            window.parent.editorLLB.SetValue(vl[9]);  
        if (window.parent.pageState.fushaLlogarie.llogariSh.index >= firstPageElementIndex && window.parent.pageState.fushaLlogarie.llogariSh.index <= lastPageElementIndex)
            window.parent.editorLLSh.SetValue(vl[10]);
        if (window.parent.pageState.fushaLlogarie.llogariTr.index >= firstPageElementIndex && window.parent.pageState.fushaLlogarie.llogariTr.index <= lastPageElementIndex)
            window.parent.editorLLT.SetValue(vl[11]);
     if (window.parent.pageState.fushaLlogarie.llogariShp.index >= firstPageElementIndex && window.parent.pageState.fushaLlogarie.llogariShp.index <= lastPageElementIndex)
            window.parent.editorLLShp.SetValue(vl[12]);
        if (window.parent.pageState.fushaLlogarie.llogariA.index >= firstPageElementIndex && window.parent.pageState.fushaLlogarie.llogariA.index <= lastPageElementIndex)
            window.parent.editorLLA.SetValue(vl[13]);
    }
    else if (window.parent.identifikuesPerPopupSkema === "Import") {
        window.parent.editorSKA.SetValue(vl[0]);
        window.parent.editorSKA.SetFocus();
    }
    else {

        Utils.SelectComboItem(window.parent.btneSkema, vl[1], vl[0]);
        window.parent.btneSkema.SetFocus(true);
        //                window.parent.btneLlogInv.SetText(vl[2]);
        try {
            Utils.SelectComboItem(window.parent.btneLlogInv, vl[8], vl[2]);
            //window.parent.btneLlogBle.SetText(vl[3]);
            Utils.SelectComboItem(window.parent.btneLlogBle, vl[9], vl[3]);
            //window.parent.btneLlogShit.SetText(vl[4]);
            Utils.SelectComboItem(window.parent.btneLlogShit, vl[10], vl[4]);
            //window.parent.btneLlogTretet.SetText(vl[5]);
            Utils.SelectComboItem(window.parent.btneLlogTretet, vl[11], vl[5]);
            //window.parent.btnLlogShpe.SetText(vl[6]);
            Utils.SelectComboItem(window.parent.btnLlogShpe, vl[12], vl[6]);
            Utils.SelectComboItem(window.parent.cmbLlogAmortizimi, vl[13], vl[7]);
            Utils.SelectComboItem(window.parent.btnLlogPakesim,  vl[15],vl[14]);
        }
        catch (ee) {
        }
        var hf = window.parent.document.getElementById("hfSkema");
        hf.value = window.parent.btneSkema.GetText();
        //KEVI: nuk perdoret kjo poshte
        //                if (window.parent.identifikuesPerSkemat!=="modifikim") {
        //                    window.parent.IdSkemaKontabilitetiArtikulli.SetText(vl[0]);
        //                    //   window.parent.ProcessTextCahnged('IdSkemaKontabilitetiArtikulli', vl[0]);
        //                }
        ////        callWebservice(vl[2]);
        ////        callWebservice1(vl[3]);
        ////        callWebservice2(vl[4]);
        ////        callWebservice3(vl[5]);
        ////        callWebservice4(vl[6]); 




    }  window.parent.popupUniversal.Hide();
}
function callWebservice(name) {
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "ktheNrLlogarie"),
        data: JSON.stringify({ prefixText: name })
    }).done(SucceededCallback);

}
function callWebservice1(name) {
    $.ajax({      
        url: Utils.getServerApiUrl("Rregjistrime", "ktheNrLlogarie"),
        data: JSON.stringify({ prefixText: name })
    }).done(SucceededCallback1);

}
function callWebservice2(name) {
    $.ajax({      
        url: Utils.getServerApiUrl("Rregjistrime", "ktheNrLlogarie"),
        data: JSON.stringify({ prefixText: name })
    }).done(SucceededCallback2);
}
function callWebservice3(name) {
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "ktheNrLlogarie"),
        data: JSON.stringify({ prefixText: name })
    }).done(SucceededCallback3);
}
function callWebservice4(name) {
    $.ajax({      
        url: Utils.getServerApiUrl("Rregjistrime", "ktheNrLlogarie"),
        data: JSON.stringify({ prefixText: name })
    }).done(SucceededCallback4);
}
function SucceededCallback(result) {
    window.parent.btneLlogInv.SetText(result);
}
function SucceededCallback1(result) {
    window.parent.btneLlogBle.SetText(result);
}
function SucceededCallback2(result) {

    window.parent.btneLlogShit.SetText(result);
}
function SucceededCallback3(result) {
    window.parent.btneLlogTretet.SetText(result);
}
function SucceededCallback4(result) {
    window.parent.btnLlogShpe.SetText(result);

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
function Filtra_Click(s, e, idKontainer) { //KEVI jep error
    document.getElementById(idKontainer).src = 'LupaFiltra.aspx?grida=gvLupaSkemaKontArt&page=LupaSkemaKontabelArtikulli.aspx';
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
        gvLupaSkemaKontArt.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});
$(window).bind('resize', function () {//po

    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaSkemaKontArt.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');