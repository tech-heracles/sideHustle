function Init() {
    try {
        myFaqeCelje.shtoHandlerSession();
        gvLupaLloji.SetFocusedRowIndex(0);
        btnOk.Focus();
    }
    catch (err) {
    }
}

var arrCheck = new Array();
var arrEmertimi = new Array();
var arrPrioriteti = new Array();

function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ? evt.keyCode : evt.charCode;
}

var cou1 = 0;
var indeksi = -1;
var editorCheck;
var editorEmertimi;
var editorPrioriteti;
var indexCounter = 0;
var prio = "";

function merrTeDhena() {//merren te dhenat qe ka grida
    arrCheck = new Array();
    arrEmertimi = new Array();
    arrPrioriteti = new Array();
    cou1 = 0;
    indexCounter = 0;
    var value = new Array(); stringValue = new Array();
    for (i = 0; i < gvLupaLloji.cpNoRows; i++) {
        editorCheck = Utils.ktheKontroll('cbCheck' + i);
        editorEmertimi = Utils.ktheKontroll('txtEmertimi' + i);
        editorPrioriteti = Utils.ktheKontroll('txtPrioriteti' + i);
        if (editorCheck.GetChecked()) {
            value[editorPrioriteti.GetText() - 1] = gvLupaLloji.keys[i];
            stringValue[editorPrioriteti.GetText() - 1] = editorEmertimi.GetText();
        }
    }
    var vler = "";
    var emer = "";
    for (j = 0; j < value.length; j++)
        vler += "," + value[j];
    for (j = 0; j < stringValue.length; j++)
        emer = emer + stringValue[j] + ", ";
    emer = emer.substr(0, emer.lastIndexOf(" ") - 1);
    window.parent.editorLloji.SetText(emer);
    hfPrioriteti.SetText(vler);
    window.parent.$("input[id$='hfPrioriteti']").val(vler);
    window.parent.editorLloji.SetFocus(true);
}

var prioritet = 0;
function CheckedChenged(key) {
    editorCheck = Utils.ktheKontroll('cbCheck' + key);
    editorPrioriteti = Utils.ktheKontroll('txtPrioriteti' + key);
    var prio = editorPrioriteti.GetText();
    if (editorCheck.GetChecked() == true) {
        var maxPrio = 1;
        for (i = 0; i < gvLupaLloji.cpNoRows; i++) {
            if (i == key)
                continue;
            editorCheck = Utils.ktheKontroll('cbCheck' + i);
            if (editorCheck.GetChecked())
                maxPrio++;
        }
        editorPrioriteti.SetText(maxPrio);
    }
    else {
        editorPrioriteti.SetText('0');
        for (i = 0; i < gvLupaLloji.cpNoRows; i++) {
            if (i == key)
                continue;
            else {
                editorPrioriteti = Utils.ktheKontroll('txtPrioriteti' + i);
                editorCheck = Utils.ktheKontroll('cbCheck' + i);
                if (editorCheck.GetChecked() && editorPrioriteti.GetText() > prio)
                    editorPrioriteti.SetText(editorPrioriteti.GetText() - 1);
            }
        }
    }
}