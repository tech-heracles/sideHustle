
function Init() {
    try {
        myFaqeCelje.shtoHandlerSession();
        // fokus te periudha aktuale; btnOk.Focus() e rrotullonte dritaren poshte dhe fshihte vitin dhe kolonat
        var index = 0;
        var aktuale = window.parent && window.parent.lexoHfPeriudhe ? window.parent.lexoHfPeriudhe() : null;
        if (aktuale) {
            for (var i = 0; i < gvLupaPerKont.GetVisibleRowsOnPage(); i++) {
                if (String(gvLupaPerKont.GetRowKey(i)) === String(aktuale.IdPeriudha)) { index = i; break; }
            }
        }
        gvLupaPerKont.SetFocusedRowIndex(index);
        window.scrollTo(0, 0);
        var rreshti = gvLupaPerKont.GetRow(index);
        if (rreshti && rreshti.getBoundingClientRect().bottom > window.innerHeight) rreshti.scrollIntoView({ block: 'center' });
    }
    catch (err) {
        ///alert('gabim');    
    }
}

document.onkeydown = ProcessKeyPress;

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
    //            window.parent.SelectAndClosePopup(id, values[4],dateFillim,dateMbarim); jepte probleme me daten
    window.parent.callWebServiceVendosPeriudhen(id);
}



//    //nuk i ben refresh faqes brenda pane te spliterit nqs i kalon te njejten URL te faqes 
//    //prandaj ndryshoj gjithmone ne menyre Random URL per faqen kur dua te bej refresh, pa prishur pune :)   
//    //shiko http://www.devexpress.com/Support/Center/p/B190784.aspx?searchtext=splitter+reload+page+in+pane+js 

      