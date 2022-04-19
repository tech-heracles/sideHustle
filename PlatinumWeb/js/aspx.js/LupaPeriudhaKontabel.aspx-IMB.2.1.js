
function Init() {
    try {
        myFaqeCelje.shtoHandlerSession();
        gvLupaPerKont.SetFocusedRowIndex(0);
        btnOk.Focus();
    }
    catch (err) {
        ///alert('gabim');    
    }
}

document.onkeydown = ProcessKeyPress;

function refreshFooterPanel() {
    //            var parentWindow = window.parent;
    //            var paneContent = parentWindow.splitter.GetPaneByName('Footer');
    //            var e = Math.floor(Math.random() * 100000).toString();
    //            var contentUrl = 'FooterPanelInfo.aspx?' + e;
    //            paneContent.SetContentUrl(contentUrl);
    //            paneContent.RefreshContentUrl();
}

function ProcessKeyPress() {
    var currentIndex = gvLupaPerKont.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaPerKont.GetVisibleRowsOnPage() - 1) {
            gvLupaPerKont.SetFocusedRowIndex(0);
        }
        else {
            gvLupaPerKont.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaPerKont.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();

    }
}

function OnGridSelectionChanged() {
    gvLupaPerKont.GetRowValues(gvLupaPerKont.GetFocusedRowIndex(), 'IdPeriudha;NrPeriudha;FillimiPeriudha;MbarimiPeriudha;EmerPeriudha', OnGridSelectionComplete);
}

function OnGridSelectionComplete(values) {
    var id = values[0];
    //var kodi = ''; text = values[1];
    //            var dateFillim = {values[2].format('dd/MM/yyyy hh:mm:ss')};
    //            dateFillim.setDate();
    //            var dateMbarim = {values[3].format('dd/MM/yyyy hh:mm:ss')};
    //            dateMbarim = setDate();
    //            var periudha = data1 + '-' + data2;
    //            window.parent.document.getElementById("hfPeriudha").value = periudha;
    //            ASPxCallback1.PerformCallback();
    //window.parent.popupUniversal.Hide();
    //            window.parent.SelectAndClosePopup(id, values[4],dateFillim,dateMbarim); jepte probleme me daten
    window.parent.callWebServiceVendosPeriudhen(id);
}


function unLoadPeriudha() {
    //kam zgjedhur ndermarrjen dhe eshte caktuar periudha aktuale, jane ne sesion
    //duhet t'i bej refresh footerit te spliterit ne faqen kryesore qe te ngarkohen keto te dhena
    Utils.SetOrRefreshSplitterPaneContentUrl("Footer", "FooterPanelInfo.aspx");
}

//function SetSplitterPaneContentUrl(pane, contentUrl) {
//    var parentWindow = window.parent;
//    var paneContent = parentWindow.splitter.GetPaneByName(pane);

//    //nuk i ben refresh faqes brenda pane te spliterit nqs i kalon te njejten URL te faqes 
//    //prandaj ndryshoj gjithmone ne menyre Random URL per faqen kur dua te bej refresh, pa prishur pune :)   
//    //shiko http://www.devexpress.com/Support/Center/p/B190784.aspx?searchtext=splitter+reload+page+in+pane+js 

//    var e = Math.floor(Math.random() * 100000).toString();
//    contentUrl = contentUrl + '?' + e;
//    paneContent.SetContentUrl(contentUrl);
//    paneContent.RefreshContentUrl();
//}
      