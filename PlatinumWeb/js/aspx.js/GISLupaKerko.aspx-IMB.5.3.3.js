function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvLupaKerko, "2028", '');
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
        gvLupaKerko.SetFocusedRowIndex(0);
        btnOk.Focus();
    }
    catch (err) {

    }
}
document.onkeydown = ProcessKeyPress;
function ProcessKeyPress() {
    var currentIndex = gvLupaKerko.GetFocusedRowIndex();
    if (event.keyCode == 40) {
        if (currentIndex == gvLupaKerko.GetVisibleRowsOnPage() - 1) {
            gvLupaKerko.SetFocusedRowIndex(0);
        }
        else {
            gvLupaKerko.SetFocusedRowIndex(currentIndex + 1);
        }
    }
    if (event.keyCode == 38) {
        if (currentIndex == 0) {
            return;
        }
        else {
            gvLupaKerko.SetFocusedRowIndex(currentIndex - 1);
        }
    }
    if (event.keyCode == 13) {
        OnGridSelectionChanged();
    }
}

function OnGridSelectionChanged() {
    gvLupaKerko.GetSelectedFieldValues('gid;the_geom;IDLAYER;Status', OnGridSelectionComplete);
}

function OnGridSelectionComplete(values) {
    if (values && values.length > 0) {
        var arrayKerkimiObjects = values.map(function (value, index) {
            //IDLayer ka formatin IdLayerType_IdLayer
            var vlerat = value[2].split("_");
            var grupo = cmbGrupo.GetCheckState() == "Checked" ? 1 : 0;
            
            return {
                gid: value[0],
                the_geom: value[1],
                IDLAYER: (vlerat[0] == 3 ? vlerat[1] : vlerat[0]),
                NrStatusi: value[3],
                Grupo: grupo
            };
        });
        var arrayKerkimiLayerIDs = values.map(function (value, index) {
            var vlerat = value[2].split("_");
            return (vlerat[0] == 3 ? vlerat[1] : vlerat[0]);
        });
    }
    window.parent.varSettings.kerkimResultSelected.arrayKerkimiObjects = arrayKerkimiObjects;
    window.parent.varSettings.kerkimResultSelected.arrayKerkimiLayerIDs = arrayKerkimiLayerIDs;
    window.parent.neFundKerkimiPozicionoRezultate(arrayKerkimiObjects, arrayKerkimiLayerIDs, "T");
}

function menu_click(s, e) {
    switch (e.item.name) {
        case "OK":
            e.processOnServer = false;
            OnGridSelectionChanged();
            break;
        case "Anullo":
            e.processOnServer = false;
            window.parent.selektuarVleraNgaKerkimi = false;
            window.parent.varSettings.kerkimResultSelected.arrayKerkimiObjects = undefined;
            window.parent.varSettings.kerkimResultSelected.arrayKerkimiLayerIDs = undefined;
            window.parent.popupUniversal2.Hide();
            break;
        default:
            e.processOnServer = false;
            break;
    }
}

//metoda per te shfaqur popupin e filtrave
function Filtra_Click() {
    document.getElementById('<%= Container.ClientID %>').src = 'LupaFiltra.aspx?grida=gvLupaKerko&page=GISLupaKerko.aspx&idKonfigAmbjente=';
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
        gvLupaKerko.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});
$(window).bind('resize', function () {//po
    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvLupaKerko.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');

$(window).bind('unload', function () {
    window.parent.Ext.getCmp('KerkoBtnAll').toggle(false);

});
function gvBeginCallback(s, e) {
    console.log('begin');
}


function cmbLayerChanged(s, e) {
    cmbFiltri.SetValue(null);
    hfGrida.Set("filterExpression", null);
    callBackPanel.PerformCallback('LayerChanged');
    e.processOnServer = false;
}
function cmbGrupoChanged(s, e) {
    callBackPanel.PerformCallback('GrupoChanged');
}
function btnKerkoClick(s, e) {
    if (cmbLayers.GetValue() != undefined)
        ApplyFilter();
}

function ApplyFilter() {

    var idLayerType = cmbLayers.GetValue().split('_')[0];//merr id layer type
    var filterCondition = "";
    var andOperator = "";
    var nrKolonave = gvLupaKerko.GetColumnsCount();

    for (var i = 0; i < nrKolonave ; i++) {
        var filterEditorControl = gvLupaKerko.GetAutoFilterEditor(i); 
        if (filterEditorControl == undefined && filterEditorControl == null)
            continue;       
        var filterEditor = filterEditorControl.GetValue();
        if (filterEditor != null && filterEditor!=undefined && filterEditor != "") {
            var emriKolones = gvLupaKerko.GetColumn(i).fieldName;
            if (emriKolones == "Statusi" && idLayerType == 3)
                continue;

            filterCondition += (filterCondition == "") ? "" : "And ";
            if (emriKolones && emriKolones != "gid" && emriKolones != "the_geom" && emriKolones != "IDLAYER") {
                filterCondition = filterCondition + andOperator + " Contains( [" + emriKolones + "]," + "'" + filterEditor + "')"; 
            }
        }
    }
    filterCondition = filterCondition.replace("And Contains( [Status],'')", "").replace(" Contains( [Status],'')", "");
    callBackPanel.PerformCallback(filterCondition);
}

function Selected_IndexChanged(s, e) {
    if (s.FindItemByText(s.GetInputElement().value) != null) {
        btnRuaj.SetEnabled(false); btnFshi.SetEnabled(true);
        callBackPanel.PerformCallback('Filter;'+s.GetValue());
    } else if (s.GetText() == '') {
        btnRuaj.SetEnabled(false);
        btnFshi.SetEnabled(false);
    }
}