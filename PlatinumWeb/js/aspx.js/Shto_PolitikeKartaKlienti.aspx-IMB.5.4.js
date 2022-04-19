; //variabla per te kaluar nga nje reshti i grides tek tjetri
var indexSel = 0;
//indexi i zgjedhur per modifikim
var indexModifiko;
//perdoret per te treguar nese duhet te mbushen tabet apo jo kjo perdoret qe tabet te mbushen vetem heren e pare dhe jo pas cdo here qe  kalohet nga nje tab tek tjetri
var mbush = true;
//perdoret per te daluar rastet kur bejme double click apo thjesht click ne menyre qe ne double click te kaloje tek tabi i dyte
var kaloTab = false;
//perdoret per griden e llogarive ne menyre qe te lodohet vetem ne momentin kur shkon ne tabin e saj
var lista = true;


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
    changeName();


    $(window).on("load", function () {
        ASPxGridView_KategoriPike.SetClientVisible(false);
        cmdShto.SetClientVisible(false);
        cmdEdit.SetClientVisible(false);
        cmdFshi.SetClientVisible(false);
        lblVleraPike.SetClientVisible(false);
        txtVleraPike.SetClientVisible(false);
        lblVleraPike2.SetClientVisible(false);
        if (hfState.Get("lupe"))
        PageControl.GetTab(1).SetVisible(false);
    });
    
});

function checkText(s, e) {
    myMenu.checkText(s, e);
} var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, ASPxGridView_Politikat, "2018", "");
}
function textChanged(s, e) {
    myMenu.textChanged(s, e);
}
//metodat per te hapur faqen e modifikimit me double click
function OnGridDoubleClick(index) {
    if (hfState.Get("lupe")) {
        ASPxGridView_Politikat.GetSelectedFieldValues('Kodi', function (values) {
            var kodi = '';

            for (var i = 0; i < values.length; i++) {
                if (kodi == '')
                    kodi = values[i];
                else
                    kodi = kodi + ',' + values[i];
            }

            window.parent.editorGlobal.SetText(kodi);
            window.parent.editorGlobal.Focus();
            window.parent.popupUniversal.Hide();
        });
    }
    else {
        indexModifiko = index;
        lista = true;
        mbushfusha();
    }
}
/*
Function: Poshte_click

perdoret per te selektuar rreshtin me poshte

Parameters:

e-eventi
*/
function Poshte_click(e) {
    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, ASPxGridView_Politikat, indexSel);
}
/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
function Lart_click(e) {
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, ASPxGridView_Politikat, indexSel);
}
/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes
                
Parameters:

e-eventi
*/
function Fillim_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, ASPxGridView_Politikat, indexSel);
}
/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
function Fund_click(e) {
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, ASPxGridView_Politikat, indexSel);
}

/*
Function: menu_click

perdoret per veprimet e menuse ne javascript

Parameters:

e-eventi
*/

function menu_click(s, e) {
    var hfShtimModifikim = $('#hfShtimModifikim'); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
    var hfId = $('#hfId'); //hidden fieldi qe ruan id  e rreshtit te selektuar
    var ruajbuxhetet = false; //i here per i here   

    if (e.item.name == "Ruaj") {
        if (ASPxGridView_KategoriPike.IsEditing() && rdPike.GetChecked()) {
            e.processOnServer = false;
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgKategoriPolitkashRuajtje"));
            return;
        }

    }

    if (hfState.Get("lupe")) {
        switch (e.item.name) {
            case "OK":
                e.processOnServer = false;
                OnGridSelectionLupa();
                break;
            case "Anullo":
                e.processOnServer = false;
                window.parent.popupUniversal.Hide();
                break;
        }
        return;
    }

    myMenu.menu_click(s, e, hfShtimModifikim, hfId, PageControl, false, false, indexModifiko, pastrofusha, SucceededCallbackKonfig, resultkonf, colKontrollet, aktivizoFusha, colAtrTrupi);
}

//merr te dhenat e rreshtit te selektuar
function mbushfusha() {
    mbush = false;
    $('#hfShtimModifikim').val("modifikim");
    indexModifiko = ASPxGridView_Politikat.GetFocusedRowIndex();
    if (indexModifiko == -1)
        myMesazh.ShtoMesazhGabimi('Duhet te zgjidhni nje karte!');
    else
        rdPerqindje.SetValue(false);
        rdPike.SetValue(false);
        ASPxGridView_Politikat.GetRowValues(indexModifiko, 'IdPolitike;Kodi;Lloji;VleraPikes', OnGetRowValuesMod);// Utils.shfaqLoadingGif();;
    }

//mbush fushat me te dhenat e rreshtit te selektuar
function OnGetRowValuesMod(values) {
    if ($('#hfShtimModifikim').val() == "shtim")
        return;
    $('#hfId').val(values[0]);
    txtKodi.SetText(values[1]);
    if ($('#hfShtimModifikim').val() == "modifikim") {
        txtKodi.SetEnabled(false);
    }
    if ((values[2]) == 'me zbritje') {
        rdPerqindje.SetValue(true);
        ASPxGridView_KategoriPike.SetClientVisible(false);    
        cmdShto.SetClientVisible(false);
        cmdFshi.SetClientVisible(false);
        cmdEdit.SetClientVisible(false);
        lblVleraPike.SetClientVisible(false);
        txtVleraPike.SetClientVisible(false);
        lblVleraPike2.SetClientVisible(false);
    }
    else {
        rdPike.SetValue(true);
        ASPxGridView_KategoriPike.SetClientVisible(true);
        cmdShto.SetClientVisible(true);
        cmdFshi.SetClientVisible(true);
        cmdEdit.SetClientVisible(true);
        lblVleraPike.SetClientVisible(true);
        txtVleraPike.SetClientVisible(true);
        lblVleraPike2.SetClientVisible(true);
        if (values[2] == 'zbritje dhe pike')  rdPerqindje.SetValue(true);
    }
    txtVleraPike.SetText(values[3]);
  
    ASPxGridView_KategoriPike.PerformCallback(rdPike.GetValue());
   
    if (kaloTab) {
        PageControl.SetActiveTabIndex(1);
        myMenu.PercaktoMenuSipasTabit(1, hfTeDrejta, $('#hfShtimModifikim'));
    }
}

//therret funksionin qe ben enabled dhe disabled fushat sipas lidhjes
function SucceededCallbackLidhur(result) {
    var hf = $('#hfKontrollet')[0]; //mban te dhenat mbi kontrollet
    var hfLidhur = $("#hfLidhur");
    hfLidhur.val(result);
    aktivizoFusha(colKontrollet, colAtrTrupi, eval(result.toLowerCase()));
}

//pastron fushat per shtim dhe ben aktive fushat
function pastrofusha() {
    txtKodi.SetText('');
    txtVleraPike.SetText('');   
    aktivizoFusha(colKontrollet, colAtrTrupi, false);
    rdPerqindje.SetValue(true);
    rdPike.SetValue(false);
    ASPxGridView_KategoriPike.PerformCallback(false);
    ASPxGridView_KategoriPike.SetClientVisible(false);
    cmdShto.SetClientVisible(false);
    cmdFshi.SetClientVisible(false);
    cmdEdit.SetClientVisible(false);
    lblVleraPike.SetClientVisible(false);
    txtVleraPike.SetClientVisible(false);
    lblVleraPike2.SetClientVisible(false);

}

/* 
Function: OnGridSelectionChanged

perdoret per te ruajtur indexin e selektimit

Parameters:

e-eventi
*/

function OnGridSelectionChanged(e) {
    indexSel = myMenu.JSlevizNeGride.OnGridSelectionChanged(e, indexSel);
}
function OnGridSelectionLupa() {
    ASPxGridView_Politikat.GetSelectedFieldValues('Kodi', function (values) {
        var kodi = '';

        for (var i = 0; i < values.length; i++) {
            if (kodi == '')
                kodi = values[i];
            else
                kodi = kodi + ',' + values[i];
        }

        window.parent.editorGlobal.SetText(kodi);
        window.parent.editorGlobal.Focus();
        window.parent.popupUniversal.Hide();
    });
}

function callWebserviceKonfigurimi(idKomp, kodKonf) {
    var idGjuha = hfState.Get('_idGjuha');
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
        data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: hfState.Get('_idNdermarrje'), idGjuha: idGjuha })
    }).done(SucceededCallbackKonfig);
    ASPxGridView_Politikat.PerformCallback(idKomp + ";" + kodKonf);
}

function callWebserviceKonfigurimiInit(idKomp, kodKonf) {

    $("#dvKarta").show();
    var idGjuha = hfState.Get('_idGjuha');
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
        data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: hfState.Get('_idNdermarrje'), idGjuha: idGjuha })
    }).done(SucceededCallbackKonfig);
}



function ndryshoKonfiguriminInit() {
    callWebserviceKonfigurimiInit("2018", "");
}


var resultkonf;
var colKontrollet, colAtrTrupi;
function SucceededCallbackKonfig(result) {
    if (result !== "" && result !== null) {
        colKontrollet = result.colKontrollet;
        colAtrTrupi = result.colAtrTrupi;
        resultkonf = result;
        LupaKontrollet(colKontrollet, colAtrTrupi);
        var hf = $('#hfKontrollet');
        var hfLidhur = $("#hfLidhur");
        var hfMod = $('#hfShtimModifikim');
        var arrTabela = ['tblPolitika'];   
        myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, hf, hfMod, '', arrTabela, "ASPxPageControl1_C");
    }

}

function LupaKontrollet(kontrollet, colAtrTrupi) {
    var hf6 = $("#hfLupaLlog");
    for (var i = 0; i < kontrollet.length; i++) {
        if (kontrollet[i].KodKontrolli == "cmbLlog")
            hf6.val(colAtrTrupi[i].IdKonfigAmbjenteLupa);
    }
}

function aktivizoFusha(colKontrollet, colAtrTrupi, isLidhur) {
    var hfMod = $('#hfShtimModifikim');
    myFaqeCelje.aktivFusha(colKontrollet, colAtrTrupi, hfMod, isLidhur, '#ASPxPageControl1_');
}


function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ?
		        evt.keyCode : evt.charCode;
}

function changeName() {
    var hf = $("#hfKonffillestar")[0];
    myMenu.PercaktoMenuSipasTabit(0, hfTeDrejta, $('#hfShtimModifikim'));
    if (hfState.Get("lupe")) {
        if (ASPxMenu1.GetItemByName('Anullo') != null)
            ASPxMenu1.GetItemByName('Anullo').SetVisible(true);
    }
    myFaqeCelje.changeName(hfState.Get('komponente'), 0, hf);
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(EndRequestHandler);
    myMesazh.shtoHandler();
}

/*
Function: EndRequestHandler
    
Thirret nese ruajtja eshte bere ne rregull. Pastron koken e dokumentit dhe array-t e perdorura.
Shiko funksionet  <ndryshoKonfigurimin>.
*/
function EndRequestHandler(sender, args) {
    var hf = $("#hfStatusi");
    var hfKontrollet = $('#hfKontrollet');
    var hfShtimModifikim = $('#hfShtimModifikim'); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
    var hfId = $('#hfId');  //hidden fieldi qe ruan id  e rreshtit te selektuar
    indexModifiko = myFaqeCelje.EndRequestHandlerNew(sender, args, hf, colKontrollet, colAtrTrupi, hfShtimModifikim, hfId, indexModifiko, PageControl, ASPxGridView_Politikat, "2018", pastrofusha, hfTeDrejta);
}

function valido(s, e) {
    myFaqeCelje.valido(s, e, PageControl, hfTeDrejta, $('#hfShtimModifikim'));
}

function krijoTable(rresht, kolone, emerTabele) {
    myFaqeCelje.krijoTable(rresht, kolone, emerTabele);
}
function BeginCallback(s, e) {

    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}
function enter() {
    if (window.event.keyCode == 13) {
        event.returnValue = false;
        event.cancel = true;
    }
}
var KPF;

function EndCallbackGrida(s, e) {
    if ($('#hfShtimModifikim').val() == 'modifikim') {
        txtKodi.SetEnabled(false);    
    }
    else  {
        txtKodi.SetEnabled(true);
    }
 //   $.ajax({
 //       url: Utils.getServerApiUrl("Konfigurime", "lexoMesazhNgaSessioni"),
 //   data: JSON.stringify({ })
    //}).done(SucceededCallbackMesazhi);
    var mesazhi = Utils.MerrMesazhNgaGrida(ASPxGridView_KategoriPike);
    if (mesazhi.Kodi != 1000) {
        if (mesazhi.Status) {
            pastrofusha();
            //ASPxGridView_Politikat.ClearFilter();
            //PageControl.SetActiveTabIndex(0);
            myMesazh.ShtoMesazhSuksesi(mesazhi.PershkrimMesazhi);
        }
        else {
            myMesazh.ShtoMesazhGabimi(mesazhi.PershkrimMesazhi);
        }
    }
    Utils.hiqLoadingGif();;
}

function callWebservice() {
    var emer = 'Shto_PolitikeKartaKlienti.aspx';
    $.ajax({      
        url: Utils.getServerApiUrl("Rregjistrime", "eshteVeprimILejuar"),
        data: JSON.stringify({ emerKomponente: emer, veprimi: 'Modifiko' })
    }).done(SucceededCallback);
}

function SucceededCallback(result) {
    if (result == "true") {
        switchEditMode(indexModifiko);
    }
    else {
        myMesazh.ShtoMesazhGabimi("Nuk ke te drejta per te kryer kete veprim");
    }
}

function switchEditMode(index) {
    $("#hfShtimModifikim").val('modifikim');
    ASPxMenu1.GetItemByName('Ruaj').SetVisible(true);
    myMenu.menuSipasTeDrejtaCeljeVogel($("#hfShtimModifikim"), hfTeDrejta);
    ASPxGridView_Politikat.StartEditRow(index);
    indexEdit = index;
}

function SucceededCallbackMesazhi(result) {
    if (result && result.d)
        result = result.d;
    if (result.length == undefined)
        return;
    {
        var arr = result.split(':');
        if (arr[1] == "Green") {
            myMesazh.ShtoMesazhSuksesi(arr[0]);
            pastrofusha();

        }
        else if (arr[1] == "Red")
            myMesazh.ShtoMesazhGabimi(arr[0]);
    }
}

function OnShtoClick(s, e) {
    ASPxGridView_KategoriPike.AddNewRow();
}

function OnDeleteClick(s, e) {
    var index = ASPxGridView_KategoriPike.GetFocusedRowIndex();
    if (index == -1)
        myMesazh.ShtoMesazhGabimi("Ju lutem zgjidhni nje kategori per te fshire!");
    else
        ASPxGridView_KategoriPike.DeleteRow(index);
}

function OnEditClick(s, e) {
    var index = ASPxGridView_KategoriPike.GetFocusedRowIndex();
    if (index == -1)
        myMesazh.ShtoMesazhGabimi("Ju lutem zgjidhni nje kategori per te Modifikuar!");
    else
        ASPxGridView_KategoriPike.StartEditRow(index);
}
function OnSaveClick(s, e) {
    ASPxGridView_KategoriPike.UpdateEdit();
}
function chkbPikeChanged(s, e) {
    if(s.name == 'ASPxPageControl1_chkPike' && s.GetChecked() ){
        ASPxGridView_KategoriPike.SetClientVisible(true);
        cmdShto.SetClientVisible(true);
        cmdEdit.SetClientVisible(true);
        cmdFshi.SetClientVisible(true);
        lblVleraPike.SetClientVisible(true);
        txtVleraPike.SetClientVisible(true);
        lblVleraPike2.SetClientVisible(true);
        ASPxGridView_KategoriPike.AddNewRow();                                       
    }
    else{
        ASPxGridView_KategoriPike.SetClientVisible(false);
        cmdShto.SetClientVisible(false);
        cmdEdit.SetClientVisible(false);
        cmdFshi.SetClientVisible(false);
        lblVleraPike.SetClientVisible(false);
        txtVleraPike.SetClientVisible(false);
        lblVleraPike2.SetClientVisible(false);

    }
}

function Active_TabChanged(s, e) {
    indexModifiko = ASPxGridView_Politikat.GetFocusedRowIndex();    
    if(mbush)
    { 
        if(indexModifiko !=-1)
        {
            OnGridDoubleClick(indexModifiko); 
        }
        else 
        {   
            mbush = false;
            $('#hfShtimModifikim').val( 'shtim'); 
            $('#hfId').val(0); pastrofusha();
        }
    }
    kaloTab = false;
    myMenu.PercaktoMenuSipasTabit(e.tab.index, hfTeDrejta, $('#hfShtimModifikim'));
    
                  
}