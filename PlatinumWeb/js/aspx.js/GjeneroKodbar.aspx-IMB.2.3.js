

jQuery(document).ready(function () {//po
    $(window).on('resize', function () {//po
        try {
            if (Utils.isGridResized())
                return;
        }
        catch (ee) {
        }
    }).trigger('resize');
    $(window).on('load', function () {
        Init();
        Utils.resizeSplitter();
    });

    
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
});


/*
Function: EndRequestHandler
    
Thirret nese ruajtja eshte bere ne rregull. Pastron koken e dokumentit dhe array-t e perdorura.
Shiko funksionet <pastro>, <pastroFushatKokes> dhe <ndryshoKonfigurimin>.
*/
function EndRequestHandler(sender, args) {
    var hf = document.getElementById("status1");

    click = false;
    if (hf.value === "true") {
        myFaqeCelje.kontrolloTeDrejta('GjeneroKodbar.aspx', true);
    }
    if (hf.value == "export") {
        gvExport.PerformCallback();

    }
    Utils.hiqLoadingGif();;
}
function Init() {//po
    if (typeof (isPostBack) == "undefined") {
        changeName();
        myMesazh.shtoHandler();
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(EndRequestHandler);
        $("#divgride1").show();
        $("#dvFillim").show();
        enabled();
     //   merrTeDhena();

        //  TextChangedLlojEksporti(false);
    }
}

function changeName() {//po

    myFaqeCelje.shtoHandlerSession();
    try { window.parent.callWebServiceKtheInfoLart('GjeneroKodbar.aspx', 0); } catch (e) { }
    myCookies.createCookie('adresa', window.location.href, 1);
}
function enabled() {


    if (rbTipi.GetValue() == "XLS" || rbTipi.GetValue() == "XLSX") {
        txtSimboliNdares.SetText('');
        txtSimboliNdares.SetEnabled(false);
        cbSimboliNdares.SetChecked(false);
        cbSimboliNdares.SetEnabled(false);
        txtEmerSheet.SetEnabled(true);
    }
    else {
        txtEmerSheet.SetText('');
        txtSimboliNdares.SetEnabled(true);
        cbSimboliNdares.SetEnabled(true);
        txtEmerSheet.SetEnabled(false);
    }

}


/*
Function: menuClick

Percakton veprimin qe kryhet nese klikohet nje nga butonat e menuse.
*/
function menu_click(s, e) {
    //   gvExport.GetSelectedFieldValues('IdArtikulli;Niveli i cmimit;Kodbari', vlerat);
    if (e.item.name === "Eksporto") {
        //  btnExporto.DoClick();
        e.processOnServer = false;
        if (gvExport.GetSelectedRowCount() == 0) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukKaRreshtaPerEksport"));     
            return;
        }


        if (rbTipi.GetValue() == "CSV" && (txtSimboliNdares.GetText() == "" && !cbSimboliNdares.GetChecked())) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgShenoKarakterinNdares")); 
            return;
        }
        if (txtEmerSkedari.GetText() == '') {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjidhEmerSkedar")); 
            return;
        }
        if (gvExport.cpNoRows == 0) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgSkaRreshtaNeGrid")); 
            e.processOnServer = false;
            return;
        }
        if (arrKodbare.length == 0) {
            popKodbare.Show();
            return;
        }
        else
        for (var i in arrKodbare) {
            if (arrKodbare[i] == null) {
                popKodbare.Show();
                return;
            }
        }
        btnExporto.DoClick();

    }
    else if (e.item.name === "Gjenerokodbar") {
        e.processOnServer = false;
        if (gvExport.GetSelectedRowCount() == 0) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukEshteSelektuarAsnjeRresht")); 
            return;
        }

        if (gvExport.cpNoRows == 0) {
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgNukKaRReshtGrida"));
            e.processOnServer = false;
            return;
        }
        btnGjenero.DoClick();
    }




    else if (e.item.name === 'Ngarko') {
        Utils.shfaqLoadingGif();;
        //  merrTeDhena();

        return;

    }
}


function TextChangedSasi(editor, key) {
    // gvExport.GetSelectedFieldValues('IdArtikulli;Niveli i cmimit;Kodbari', vlerat);
    //var a = new Array();
    //a = editor.GetText().toString();
    //var hf = $("#hfSasia")[0];
    //hf.value = editor.GetText();
    //arrSasia = new Array();
    //cou1 = 0;
    //  editor.GetText();
    gvExport.SelectRowOnPage(key);
    if (isNaN(editor.GetValue())) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgSasiaNumerike"));  
        editor.SetText(1);
    }
    if (editor.GetValue() < 1) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgSasiaMeEVogelSeNje")); 
        editor.SetText(1);
    }
    merrTeDhena(key);
}
var arrSasia = new Array();
var arrKodbare = new Array();
function vlerat(values) {


    // alert(values);
    cou1 = 0;
    arrKodbare = new Array();
    // alert(values);
    for (var i in values) {
        arrKodbare[cou1] = values[i];
        cou1++;
    }
   

}
var cou1 = 0;
var index = 0;

function vendosSasi(index) {

   // arrSasia[index] = 1;
   //$("#hfSasia").val(JSON.stringify(arrSasia));
}
function vendosSasiaTeGjitha() {


}
function merrTeDhena(key) {
    // rasti kur modifikohet vlera e sasise
    if (key !== "") {
        var sasia=Utils.ktheKontroll('Sasia' + key);
        if (!sasia)
            return;
        arrSasia[key] = sasia.GetValue();
    }
   
    else {
        if (gvExport.cpNoRows > 15 * (gvExport.cpNoPage + 1))

            for (i = 15 * gvExport.cpNoPage; i < 15 * (gvExport.cpNoPage + 1) ; i++) {
                if (gvExport._isRowSelected(i)) {
                    gvExport.GetSelectedFieldValues('Kodbari', vlerat);
                }
            }
        else {
            for (i = 15 * gvExport.cpNoPage; i < gvExport.cpNoRows; i++) {
                if (gvExport._isRowSelected(i)) {
                    gvExport.GetSelectedFieldValues('Kodbari', vlerat);
                }

            }
        }
    }
    $("#hfSasia1").val(JSON.stringify(arrSasia));
}

function ShfaqTeDhenat() {
   // alert(gvExport.cpNoPage);
    if (gvExport.cpNoRows > 15 * (gvExport.cpNoPage + 1))
        for (i = 15 * gvExport.cpNoPage; i < 15 * (gvExport.cpNoPage + 1) ; i++) {
         //   if (gvExport._isRowSelected(i)){
            editorSasia =  Utils.ktheKontroll ('Sasia' + i);
            if (!editorSasia)
                return;
                if (arrSasia[i] != undefined)
                    editorSasia.SetText(arrSasia[i]);
          //  }

        }
    else 
        for (i = 15 * gvExport.cpNoPage; i < gvExport.cpNoRows; i++) {
          //  if (gvExport._isRowSelected(i)){
            editorSasia = Utils.ktheKontroll ('Sasia' + i);
            if (!editorSasia)
                return;
                if (arrSasia[i] != undefined)
                    editorSasia.SetText(arrSasia[i]);
            }
    //  }

}


function Selection(editor, key) {
   
 merrTeDhena("");

}
function Init_MenuInfo(s, e) {
    $('#dvMenu').show();
    myMesazh.InicializoTimer();
}
function Click_ButtonOk3(s, e) {
    popKodbare.Hide();
  
    btnExporto.DoClick();                                                    
}
function closing(s, e) {
    popupUniversal.SetContentUrl('');
}


