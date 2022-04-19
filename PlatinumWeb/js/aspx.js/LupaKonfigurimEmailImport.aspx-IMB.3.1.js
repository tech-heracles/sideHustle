function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
var arrStatusi = new Array();
var arrLloji = new Array();
var arrDestinacion = new Array();
var arrEmail = new Array();
var editorStatusi;
var editorLloji;
var editorDestinacion;
var editorEmail;
var editorKodi;
var indeksModifiko;
var indexCounter = 0;
var cmbKodi;
var cmbLloji;

function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvLupaKonfigurimEmailImport, "4013", '');
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
        url: Utils.getServerApiUrl("Rregjistrime", "fshiGrideNgaSessioniLupa"),
        data: JSON.stringify({})
    }).done(Utils.fshiSessionFailCheck);
});


function Init() {
        window.parent.window.parent.SessionTimeout.sendKeepAlive();
        myMesazh.shtoHandler();
        gvLupaKonfigurimEmailImport.SelectRowOnPage(0, true);
        gvLupaKonfigurimEmailImport.SetFocusedRowIndex(0);
    //btnOk.Focus();
        EndRequesHandler();
}


function EndRequesHandler() {
    var mesazhi = Utils.MerrMesazhNgaGrida(gvLupaKonfigurimEmailImport);
    if (mesazhi.Kodi !== 1000) {
        if (mesazhi.Status) {
            //ruajta u be me sukses
            myMesazh.ShtoMesazhSuksesi(mesazhi.PershkrimMesazhi);
           
        } else {
            myMesazh.ShtoMesazhGabimi(mesazhi.PershkrimMesazhi);
        }
    }
    Utils.hiqLoadingGif();

}


document.onkeydown = ProcessKeyPress;

function ProcessKeyPress() {
    var currentIndex = gvLupaKonfigurimEmailImport.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaKonfigurimEmailImport.GetVisibleRowsOnPage() - 1) {
            gvLupaKonfigurimEmailImport.SetFocusedRowIndex(0);
        }
        else {
            gvLupaKonfigurimEmailImport.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaKonfigurimEmailImport.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();
    }
}

function OnGridSelectionChanged(index) {
    if (index != -1)
        gvLupaKonfigurimEmailImport.GetRowValues(index, 'Id;IdTemplateImporti;Statusi;Lloji;Destinacion;Email', OnGridSelectionComplete);
}

function OnGridSelectionComplete(values) {
    if (values[0] != undefined) {
        window.parent.editorGlobal.SetText(values[1]);
        window.parent.editorGlobal.SetFocus();
    }
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "fshiGrideNgaSessioniLupa"),
        data: JSON.stringify({})
    }).done(Utils.fshiSessionFailCheck);

    window.parent.popupUniversal.Hide();
}

function menu_click(s, e) {
    if (e.item.name == "Ruaj") {
        merrTeDhena();
        //gvLupaKonfigurimEmailImport.PerformCallback();
    }
    else if (e.item.name == 'Anullo') 
        window.parent.popupUniversal.Hide();
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
        $("#div")[0].style.visibility = 'visible';
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaKonfigurimEmailImport.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});

$(window).bind('resize', function () {
    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaKonfigurimEmailImport.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');

function merrTeDhena() {
    arrStatusi = new Array();
    arrLloji = new Array();
    arrDestinacion = new Array();
    arrEmail = new Array();
    var hidField1 = $("#hfDestinacion"); 
    var hidfield2 = $("#hfLloji");
    var hidField3 = $("#hfStatusi");
    var hidField4 = $("#hfEmail");
    for (i = 0; i < gvLupaKonfigurimEmailImport.cpNoRows; i++) {
        arrStatusi[i] = eval('Statusi' + i).GetValue();
        arrLloji[i] = eval('Lloji' + i).GetValue();
        arrDestinacion[i] = eval('Destinacion' + i).GetValue();
        arrEmail[i] = eval('Email' + i).GetText();
    }
    hidField1.val(JSON.stringify(arrDestinacion));
    hidfield2.val(JSON.stringify(arrLloji));
    hidField3.val(JSON.stringify(arrStatusi));
    hidField4.val(JSON.stringify(arrEmail));
}

function SucceededCallbackPerdoruesiemail(result) {
    var editorDestinacion = eval('Destinacion' + indeksModifiko);
    if (result[0] != null && result[1] == "") {
        if (result[0].PerdoruesEmail == "") {
            editorDestinacion.SetText('');
            myMesazh.ShtoMesazhGabimi('Ky perdorues nuk ka email te konfiguruar!');
        }
    }
    else if (result[0] != null && result[1] != "") {
        editorDestinacion.SetText('');
        myMesazh.ShtoMesazhGabimi(result[1]);
    }
    else {
        editorDestinacion.SetText('');
        myMesazh.ShtoMesazhGabimi('Ky perdorues nuk ekziston!');
    }
    if (result[2] != null && result[2] != "") {
        editorDestinacion.SetText('');
        myMesazh.ShtoMesazhGabimi(result[2].toString());
    }
    if (indeksModifiko == gvLupaKonfigurimEmailImport.cpNoRows - 1) {
        indexCounter = indexCounter + 1;
        merrTeDhena();
        gvLupaKonfigurimEmailImport.PerformCallback();
    }
}

function SucceededCallbackRoli(result) {
    var editorDestinacion = eval('Destinacion' + indeksModifiko);
    if (result[0] != null) {
        if (result[1] == true) {
            editorDestinacion.SetText('');
            myMesazh.ShtoMesazhGabimi(result[2]);
        }
        else if (result[1] == false && result[2] != "") {
            editorDestinacion.SetText('');
            myMesazh.ShtoMesazhGabimi(result[2]);
        }
    }
    else {
        editorDestinacion.SetText('');
        myMesazh.ShtoMesazhGabimi('Ky rol nuk ekziston!');
    }
    if (indeksModifiko == gvLupaKonfigurimEmailImport.cpNoRows - 1) {
        indexCounter = indexCounter + 1;
        merrTeDhena();
        gvLupaKonfigurimEmailImport.PerformCallback();
    }
}

function FshiClicked(key) {
    gvLupaKonfigurimEmailImport.PerformCallback(key);
}

function TextChangedEmail(editor, key) {
    if (editor.GetText() != "") {
        var emailRegEx = /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,9}$/i;
        if (editor.GetText().search(emailRegEx) == -1) {
            editor.SetText('');
            myMesazh.ShtoMesazhGabimi("E-mail nuk eshte i vlefshem!");
        }
    }
}

function TextChangedStatusi(editor, key) {
    keyGlobal = key;
    var editorStatusi = editor;
    //var editorEmail = eval('Email' + key);
    //editorEmail.SetText('');
    //editorEmail.SetEnabled(false);
    //var editorDestinacion = eval('Destinacion' + key);
    //editorDestinacion.SetText('');
    //eval('Destinacion' + key).ClearItems();
    if (key === gvLupaKonfigurimEmailImport.cpNoRows - 1) {
        indexCounter = indexCounter + 1;
        merrTeDhena();
        gvLupaKonfigurimEmailImport.PerformCallback();
    }
}

function TextChangedLloji(editor, field, key) {
    keyGlobal = key;
    var editorLloji = editor;
    var editorDestinacion = eval('Destinacion' + key);
    editorDestinacion.SetText('');
    var editorEmail = eval('Email' + key);
    editorEmail.SetText('');
    if (editorLloji.GetValue() == 3) {
        editorEmail.SetEnabled(true);
        editorDestinacion.SetEnabled(false);
    }
    else {
        editorEmail.SetEnabled(false);
        editorDestinacion.SetEnabled(true);
    }
    eval('Destinacion' + key).ClearItems();
    if (key === gvLupaKonfigurimEmailImport.cpNoRows - 1) {
        indexCounter = indexCounter + 1;
        merrTeDhena();
        gvLupaKonfigurimEmailImport.PerformCallback();
    }
}

function TextChangedDestinacion(editor, key) {
    var value = eval(editor).GetText();
    indeksModifiko = key;
    if (value != '')
        callWebservice(value, key);

    if (key == gvLupaKonfigurimEmailImport.cpNoRows - 1) {//shton rresht kur jemi ne rreshtin e fundit
        indexCounter = indexCounter + 1;
        merrTeDhena();
        gvLupaKonfigurimEmailImport.PerformCallback();
    }
}

function callWebservice(name, key) {
    merrTeDhena()
    editorLloji = Utils.ktheKontroll('Lloji' + key);
    if (editorLloji.GetValue() == 1) {
        $.ajax({
            url: Utils.getServerApiUrl("Rregjistrime", "ktheEmailPerdoruesi"),
            data: JSON.stringify({ prefixText: name, perdorues: arrDestinacion, lloji: arrLloji, key: key })
        }).done(SucceededCallbackPerdoruesiemail);
    }
    else {
        $.ajax({
            url: Utils.getServerApiUrl("Rregjistrime", "kontrolloKaAdresaroli"),
            data: JSON.stringify({ prefixText: name, perdorues: arrDestinacion, lloji: arrLloji, key: key })
        }).done(SucceededCallbackRoli);
    }
}

var tekstiShkruar = "";

function KeyPressDestinacion(kodi, editor, key) {
    var editorLloji = eval('Lloji' + key);
    var vler = eval(editor).GetInputElement().value;
    tekstiShkruar = vler;
    if (editorLloji.GetSelectedItem().text == "Perdorues" || editorLloji.GetSelectedItem().text == "Rol") {
        indeksiArtPerb = key;
        $.ajax({
            url: Utils.getServerApiUrl("Rregjistrime", "ktheArtikullOseMakro2"),
            data: JSON.stringify({ prefixText: vler, count: 0, contextKey: editorLloji.GetSelectedItem().text, klasa: 0 })
        }).done(SucceededCallbackKodi);
    }
    
    if (event.keyCode == 13) {
        editorKodi = editor;
        editorKodi = document.getElementById(editor);
        editorEmail = eval('Email' + key);
        keyGlobal = key;
        if (editorLloji != null) {
            if (editorLloji.GetSelectedItem().text == "Perdorues") {
                myButtonClickLupa.LupaUniversal_Click('Zgjidh Perdoruesin', 'LupaPerdorues.aspx?vjenNga=Perdoruesi', 550, 500);
            }
            else if (editorLloji.GetSelectedItem().text == "Rol") {
                myButtonClickLupa.LupaUniversal_Click('Zgjidh rolin', 'LupaRole.aspx?vjenNga=Perdoruesi', 550, 500);
            }
        }
        event.returnValue = false;
        event.cancel = true;
    }
}



function SucceededCallbackKodi(result) {
    cmbKodi = eval('Destinacion' + indeksModifiko);
    cmbLloji = eval('Lloji' + indeksModifiko)
    cmbKodi.BeginUpdate();
    cmbKodi.ClearItems();
    if (result != null && result.length != 0) {
        for (i = 0; i < result.length; i++) {
            if (cmbLloji.GetValue() == 1)
                cmbKodi.AddItem([result[i].PerdoruesUsername, result[i].EmriPerdorues + " " + result[i].MbiemriPerdorues], result[i].IdPerdorues); 
            else if (cmbLloji.GetValue() == 2)
                cmbKodi.AddItem([result[i].KodRoli, result[i].PershkrimRoli], result[i].IdRoli);
        }
    }
    cmbKodi.EndUpdate();
    if (cmbLloji.GetValue() != 3)
        cmbKodi.SetText(tekstiShkruar);
    else cmbKodi.SetText('');
    cmbKodi.ShowDropDown();

}
function ButtonClickedDestinacion(editor, key) {
    editorKodi = editor;
    var editorLloji = eval('Lloji' + key);
    var editorEmail = eval('Email' + key);
    keyGlobal = key;
    if (editorLloji != null) {
        if (editorLloji.GetSelectedItem().text == "Perdorues") 
            myButtonClickLupa.LupaUniversal_Click('Zgjidh Perdoruesin', 'LupaPerdorues.aspx?vjenNga=KonfEmailImport', 550, 500);
        else if (editorLloji.GetSelectedItem().text == "Rol") 
            myButtonClickLupa.LupaUniversal_Click('Zgjidh rolin', 'LupaRole.aspx?vjenNga=Perdoruesi', 550, 500);
    }
}

function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ? evt.keyCode : evt.charCode;
}