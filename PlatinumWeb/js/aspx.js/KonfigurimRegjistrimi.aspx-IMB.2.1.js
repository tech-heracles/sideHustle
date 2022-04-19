; var editmode = false;
var indexEdit = -1;
var indexModifiko;

function changeName() {
    myFaqeCelje.shtoHandlerSession();
    window.parent.callWebServiceKtheInfoLart('KonfigurimRegjistrimi.aspx',0);
    window.parent.createCookie('adresa', 'KonfigurimRegjistrimi.aspx', 1);
}

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
    $(window).on('load', function () {
        Init();
    });

});

function switchEditMode(index) {
    $("#hfRuaj").val('Modifiko');
    ASPxMenu1.GetItemByName('Ruaj').SetVisible(true);
    myMenu.menuSipasTeDrejtaCeljeVogel($("#hfRuaj"), hfTeDrejta); 
    gvKategoriDok.StartEditRow(index);
    indexEdit = index;
}

function Init() {
    changeName();
    ASPxMenu1.GetItemByName('Ruaj').SetVisible(false);
    myMenu.menuSipasTeDrejtaCeljeVogel($("#hfRuaj"), hfTeDrejta);
    editmode = false;
    indexEdit = -1;
    var hf = document.getElementById('hfVeprimi');
    hf.Value = "false";
}

function InitRuaj() {
    var hf = document.getElementById('hfVeprimi');
    hf.Value = "true";
}

function callWebservice() {
    var emer = 'KonfigurimRegjistrimi.aspx';
    var veprim = 'Modifiko';
    $.ajax({       
        url: Utils.getServerApiUrl("Rregjistrime", "eshteVeprimILejuar"),
        data: JSON.stringify({ emerKomponente: emer, veprimi: veprim })
    }).done(SucceededCallback);
}

function SucceededCallback(result) {
    if (result == "true") {
        switchEditMode(indexModifiko);
    }
    else {
        alert(hfTeDrejta.Get("msgNukKeniDrejtaPerVeprim"));
    }
}


//funksionet per dropDownEdit per konvertimin e nivelit
var textSeparator = ";";
function OnListBoxSelectionChanged(listBox, args) {
    UpdateText();
}

function UpdateSelectAllItemState() {
    IsAllSelected() ? checkListBox.SelectIndices([0]) : checkListBox.UnselectIndices([0]);
}

function IsAllSelected() {
    for (var i = 0; i < checkListBox.GetItemCount(); i++)
        if (!checkListBox.GetItem(i).selected)
            return false;
    return true;
}

function UpdateText() {
    var selectedItems = checkListBox.GetSelectedItems();
    checkComboBox.SetText(GetSelectedItemsText(selectedItems));
}

function SynchronizeListBoxValues(dropDown, args) {
    checkListBox.UnselectAll();
    var texts = dropDown.GetText().split(textSeparator);
    var values = GetValuesByTexts(texts);
    checkListBox.SelectValues(values);
   // UpdateSelectAllItemState();
    UpdateText();  // for remove non-existing texts
}

function GetSelectedItemsText(items) {
    var texts = [];
    for (var i = 0; i < items.length; i++)
    //  if (items[i].index != 0)
        texts.push(items[i].text);
    return texts.join(textSeparator);
}

function GetValuesByTexts(texts) {
    var actualValues = [];
    var value = "";
    for (var i = 0; i < texts.length; i++) {
        value = GetValueByText(texts[i]);
        if (value != null)
            actualValues.push(value);
    }
    return actualValues;
}

function GetValueByText(text) {
    for (var i = 0; i < checkListBox.GetItemCount(); i++)
        if (checkListBox.GetItem(i).text.toUpperCase() == text.toUpperCase())
            return checkListBox.GetItem(i).value;
    return null;
}

function SucceededCallbackNivele(text) {
    checkListBox.ClearItems();
    var arr = text.split(';')
    for (i = 0; i < arr.length - 1; i++)
        checkListBox.AddItem(arr[i].split(',')[0], arr[i].split(',')[1]);
}

function menu_click(s, e) {
    var hfRuaj = $('#hfRuaj');
    myMenu.menu_click_celjevogel(s, e, hfRuaj, gvKategoriDok, hfTeDrejta);
}

var btnFiltrat;
function checkText(s, e) {
    myMenu.checkText(s, e);
}

function aplikoFiltra(s, e) {
    btnFiltrat = s;
    $('#hfRuaj').val( 'Filtra');
    myMenu.aplikoFiltra(s, e, gvKategoriDok, "", '');
}

function textChanged(s, e) {
    myMenu.textChanged(s, e);
}

function BeginCallback(s, e) {
    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}

function EndCallbackGrida(s, e) {
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "merrMesazhNgaSesioni"),
        data: JSON.stringify({})
    }).done(SucceededCallbackMesazhi);
}

function SucceededCallbackMesazhi(result) {
    var arr = result.split(':');
    if (arr[1] == "Green") {
        ASPxMenu1.GetItemByName('Ruaj').SetVisible(false);
        myMesazh.ShtoMesazhSuksesi(arr[0]);
    }
    else if (arr[1] == "Red")
        myMesazh.ShtoMesazhGabimi(arr[0]);
}

function gvKategoriDokSelectedIndexChanged(s, e) {
    var id = cmbKategoria.GetValue();
    if (id == 1 || id == 2 || id == 3 || id == 4)
        cbNrSerialUnik.SetEnabled(true);
    else
        cbNrSerialUnik.SetEnabled(false);
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "ktheNivele"),
        data: JSON.stringify({ id: id })
    }).done(SucceededCallbackNivele);
}

function Row_DblClick(s, e) {
    gvKategoriDok.StartEditRow(e.visibleIndex); 
    document.getElementById('hfRuaj').value='Modifiko';
    ASPxMenu1.GetItemByName('Ruaj').SetVisible(true);
}

function gvKategoriDok_Init(s, e) {
    if (document.getElementById('hfRuaj').value == 'Modifiko') {
        cmbKategoria.SetEnabled(false);
        txtKodi.SetEnabled(false);
    } else {
        cmbKategoria.SetEnabled(true);
        txtKodi.SetEnabled(true);
    }
}