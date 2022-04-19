
var idModeliMeparshem = 0;
var dtMeparshme = "";
var arrVlerat = new Array();
///ben callback griden e fushave shtese
///index: eshte indexi i rreshtit qe ka fushat shtese
///veprimi: eshte lloj veprimit qe po kryhet psh:mod,shtim
///
function RefreshFushatShtese(index, veprimi) {
    var parameter = index + ';' + veprimi;
    dtAktivizimi.SetDate(new Date("01/01/2000"));
    cmbDtNdryshimi.ClearItems();
    HfFushaShtese.Set('selectedDate', null);
    gvFushat.PerformCallback(parameter);
}

function PastroFushatShtese() {
    arrVlerat = new Array();
    dtAktivizimi.SetDate(new Date("01/01/2000"));
    cmbDtNdryshimi.ClearItems();
    HfFushaShtese.Set('selectedDate', null);
    gvFushat.PerformCallback('shtim');
}
function ndryshoKonfigurimFushaShtese(vlera) {
    HfFushaShtese.Set("idKonfigurimi", vlera);
    PastroFushatShtese();
}
///kur ndryshon indexi i modelit
function SelectedModelIndexChanged(s, e) {
    idModeliMeparshem = HfFushaShtese.Get("selectedIndex");
    var idEntiteti = $('#hfId').val();
    if (idEntiteti == "") idEntiteti = 0;
    var idModeli = cmbModeli.GetValue();
    var dataEModelitMeparshem = dtAktivizimi.GetText();
    HfFushaShtese.Set("selectedIndex", idModeli);

    MbushComboMeDataAktivizimi(idEntiteti, idModeli, function () {
        ruajFushaShtese();
        var parameter = idEntiteti + ";" + "ndryshoiModeli" + ";" + idModeliMeparshem + ";" + dataEModelitMeparshem;
        gvFushat.PerformCallback(parameter);//ruaj vlerat e modelit siper
    });
}

function ShtoStringOrDate(editor, key) {//rasti per string dhe date

    arrVlerat = myFushaShtese.ShtoStringOrDate(editor, key, arrVlerat);
}

function ShtoIntOrDouble(editor, key) {//rasti per int dhe double

    arrVlerat = myFushaShtese.ShtoIntOrDouble(editor, key, arrVlerat);
}

function ShtoCheck(editor, key) {//rasti per check box
    arrVlerat = myFushaShtese.ShtoCheck(editor, key, arrVlerat);
}
function ShtoList(editor, key) {//rasti per list box
    arrVlerat = myFushaShtese.ShtoList(editor, key, arrVlerat);
}
function ruajFushaShtese() {
    arrVlerat = new Array();
    for (i = 0; i < gvFushat.cpRowCount; i++) {
        var kontrolliRenderizuar = window['txtVlera' + i];
        if (kontrolliRenderizuar == undefined) {
            console.log("kontrolli txtVlera " + i + " nuk eshte renderizuar ");
            arrVlerat[i] = "specialValue_fshehur";
            continue;
        }
        if (kontrolliRenderizuar.inputElement == null && kontrolliRenderizuar.internalCheckBox == undefined)
            arrVlerat[i] = "";
        else if (kontrolliRenderizuar.internalCheckBox == undefined)
            arrVlerat[i] = kontrolliRenderizuar.GetText();
        else arrVlerat[i] = kontrolliRenderizuar.GetValue();
    }
    HfFushaShtese.Set('fushat', JSON.stringify(arrVlerat));
}
function MbushComboMeDataAktivizimi(idEntiteti, idModeli, then) {
    var done = $.ajax({
        pritPergjigje: true,
        showLoading:true,
        url: Utils.getServerApiUrl("FushatShtese", "MerrDataAktivizimi"),
        data: JSON.stringify({ idLidhese: idEntiteti, idModeli: idModeli })
    }).done(function (result) {
        var currentSelection = cmbDtNdryshimi.GetSelectedIndex();
        if (currentSelection == -1) currentSelection = 0;
        cmbDtNdryshimi.BeginUpdate();
        cmbDtNdryshimi.ClearItems();
        if (result == undefined || result.length == 0) {
            dtAktivizimi.SetDate(new Date("01/01/2000"));
            return;
        }
        else {

            for (var i = 0; i < result.length; i++)
                cmbDtNdryshimi.AddItem(result[i]);

            if (result.length == 1) currentSelection = 0;
            dtAktivizimi.SetDate(Utils.CreateDateFromString(result[currentSelection], 'dd/MM/yyyy'));
            cmbDtNdryshimi.SetSelectedIndex(currentSelection);
            HfFushaShtese.Set("selectedDate", cmbDtNdryshimi.GetItem(currentSelection).value);
        }
        // var cmbDtNdryshimi = new ASPxClientComboBox();    
        cmbDtNdryshimi.EndUpdate();

    });
    if (then) done.then(then);
}
function DtNdryshimiChanged(s, e) {
    ruajFushaShtese();
    dtMeparshme = HfFushaShtese.Get('selectedDate');
    var parameter = $('#hfId').val() + ";" + "ndryshoiData" + ";" + dtMeparshme;
    var dtNdryshimi = cmbDtNdryshimi.GetText();
    dtAktivizimi.SetDate(Utils.CreateDateFromString(dtNdryshimi, 'dd/MM/yyyy'));
    gvFushat.PerformCallback(parameter);//ruaj vlerat e modelit siper
    HfFushaShtese.Set("selectedDate", dtMeparshme);
}
function gvFushatBeginCallback(s, e) {
    Utils.shfaqLoadingGif();
}
function gvFushatEndCallback(s, e) {
    var merrData = s["cpMerrData"];
    var idEntiteti = s["cpIdEntieti"];
    var idModeliFillestar = s["cpModeliMeparshem"];
    var modeliDefault = s["cpModelDefault"];
    if (modeliDefault) {
        cmbModeli.SetValue(modeliDefault);
        delete s["cpModelDefault"];
    }
    if (merrData) {
        MbushComboMeDataAktivizimi($('#hfId').val(), cmbModeli.GetValue());
    }
    if (idEntiteti) {
        HfFushaShtese.Set("idRreshti", idEntiteti);
        delete s["cpIdEntieti"];
    }

    if (idModeliFillestar && !HfFushaShtese.Get("modeliDefault")) {
        HfFushaShtese.Set("selectedIndex", idModeliFillestar);
        delete s["cpModeliMeparshem"];
    }
    Utils.hiqLoadingGif();
}