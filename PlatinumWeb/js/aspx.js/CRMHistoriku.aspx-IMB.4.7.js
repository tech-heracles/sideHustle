; $(document).ready(function (e) {
    var idNdermarrje = hfState.Get("idNdermarrje");
    var idPerdoruesi = hfState.Get("idPerdoruesi");
    var idVitNdermarrje = hfState.Get("idNdermarrjeVit");
    myFaqeCelje.krijoMenuPerCRM(idPerdoruesi, idNdermarrje, idVitNdermarrje);
	changeName();
});

//variabla per te kaluar nga nje reshti i grides tek tjetri
var indexSel = 0;
//indexi i zgjedhur per modifikim
var indexModifiko;
//perdoret per te treguar nese duhet te mbushen tabet apo jo kjo perdoret qe tabet te mbushen vetem heren e pare dhe jo pas cdo here qe  kalohet nga nje tab tek tjetri
var mbush = true;
//perdoret per te daluar rastet kur bejme double click apo thjesht click ne menyre qe ne double click te kaloje tek tabi i dyte
var kaloTab = false;
//perdoret per griden e llogarive ne menyre qe te lodohet vetem ne momentin kur shkon ne tabin e saj
var lista = true;
var btnFiltrat;

function checkText(s, e) {
	myMenu.checkText(s, e);
}

function aplikoFiltra(s, e) {
	btnFiltrat = s;
	myMenu.aplikoFiltra(s, e, gvHistoriku, "2003", cmbKonfigurimi.GetText());
}

function textChanged(s, e) {
	myMenu.textChanged(s, e);
}

//metodat per te hapur faqen e modifikimit me double click
function OnGridDoubleClick(index) {
	indexModifiko = index;
	lista = true;
	mbushfusha();
}

function menu_click(s, e) {
	hfShtimModifikim = $('#hfShtimModifikim'); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
	hfId = $('#hfId'); //hidden fieldi qe ruan id  e rreshtit te selektuar
	myMenu.menu_click(s, e, hfShtimModifikim, hfId, PageControl, false, undefined, indexModifiko, pastrofusha, SucceededCallbackKonfig, resultkonf, colKontrollet, aktivizoFusha, colAtrTrupi);

	if (e.item.name == "Shiko") {
		window.kaloTab = true;
		window.lista = true;
		mbushfusha();
		e.processOnServer = false;
	}
}

//merr te dhenat e rreshtit te selektuar
function mbushfusha() {
	mbush = false;
	$('#hfShtimModifikim').val("modifikim");
	indexModifiko = gvHistoriku.GetFocusedRowIndex();
	if (indexModifiko == -1)
		myMesazh.ShtoMesazhGabimi('Duhet te zgjidhni nje takim!');
	else
	    gvHistoriku.GetRowValues(indexModifiko, 'Id;KodiAgjenti;KodiKlienti;Klienti;DtVizita;Emri;Mbiemri;Shenime;IdAgjenti;IdKlienti;Kohezgjatja;KordinateFillimi;KordinateMbarimi;DateFillimiRealizimi;DateMbarimRealizimi;Anketa;KoordinataKlienti', OnGetRowValuesMod);
}

//mbush fushat me te dhenat e rreshtit te selektuar
function OnGetRowValuesMod(values) {
    if ($('#hfShtimModifikim').val() == "shtim")
        return;
    $('#hfId').val(values[0]);
    txtAgjenti.SetText(values[1] + " (" + values[5] + " " + values[6] + ")");
    txtKlienti.SetText(values[2] + " (" + values[3] + ")");
    dteDtVeprimi.SetDate(values[4]);
    txtPershkrimi.SetText(values[7]);
    txbKohezgjatja.SetText(values[10]);

    var kordFillimMbarim = "";
    if (values[11] != null && values[11] != "")
        kordFillimMbarim += values[11].substring(values[11].indexOf('('), values[11].length).replace(" ", ":");
    kordFillimMbarim += " , ";
    if (values[12] != null && values[12] != "")
        kordFillimMbarim += values[12].substring(values[12].indexOf('('), values[12].length).replace(" ", ":");
    btnCaktoNeHarte.SetText(kordFillimMbarim);
    
    var koordinataKlienti = "";
    if (values[16] != null && values[16] != "")
        koordinataKlienti = values[16].substring(values[16].indexOf('('), values[16].length).replace(" ", ":");
    btnKoordinatatEKlientit.SetText(koordinataKlienti);

    var tmpGeoms = new Array(2);
    tmpGeoms[0] = values[11];
    tmpGeoms[1] = values[12];
    hfState.Set("geomTakimi", JSON.stringify(tmpGeoms));

    var tmpGeomsKlienti = new Array(1);
    tmpGeomsKlienti[0] = values[16];
    hfState.Set("geomKlienti", JSON.stringify(tmpGeomsKlienti));

    dtDateFillimRealizimi.SetValue(values[13]);
    dtDatePerfundimRealizimi.SetValue(values[14]);

    //mund te behet callback tabi
    var params = values[8] + ";" + values[9] + ";" + JSON.stringify(values[4]) + ";" + values[15];
    DetajetPanel.PerformCallback(params);

    if (kaloTab) {
        PageControl.SetActiveTabIndex(1);
        ASPxMenu1.AdjustControl();
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
	txtAgjenti.SetText('');
	txtKlienti.SetText('');
	dteDtVeprimi.SetText('');
	DetajetPanel.PerformCallback(-1,-1);
	aktivizoFusha(colKontrollet, colAtrTrupi, false);
}

function callWebserviceKonfigurimi(idKomp, kodKonf) {
	var idGjuha = hfState.Get('idGjuha');
	$.ajax({	  
	    url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
	    data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: hfState.Get('idNdermarrje'), idGjuha: idGjuha })
	}).done(SucceededCallbackKonfig);
	gvHistoriku.PerformCallback(idKomp + ";" + kodKonf);
}

function callWebserviceKonfigurimiInit(idKomp, kodKonf) {
	$("#dvAktiviteti").show();
	var idGjuha = hfState.Get('idGjuha');
	$.ajax({   
	    url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
	    data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: hfState.Get('idNdermarrje'), idGjuha: idGjuha })
	}).done(SucceededCallbackKonfig);
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
		var arrTabela = ['tblBurimet'];

		myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, hf, hfMod, '', arrTabela, "ASPxPageControl1_C");
	}
}

function LupaKontrollet(kontrollet, colAtrTrupi) {
}

function aktivizoFusha(colKontrollet, colAtrTrupi, isLidhur) {
	var hfMod = $('#hfShtimModifikim');
	myFaqeCelje.aktivFusha(colKontrollet, colAtrTrupi, hfMod, isLidhur, '#ASPxPageControl1_');
}

function ndryshoKonfigurimin() {
	lblKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[1]);
	cmbKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[0]);
	callWebserviceKonfigurimi("2003", cmbKonfigurimi.GetText());
}

function ndryshoKonfiguriminInit() {
	callWebserviceKonfigurimiInit("2003", cmbKonfigurimi.GetText());
}

function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ? evt.keyCode : evt.charCode;
}

function changeName() {
	var hf = $("#hfKonffillestar")[0];
	if (hf !== null) {
		lblKonfigurimi.SetText(hf.value.split(';')[1]);
		cmbKonfigurimi.SetText(hf.value.split(';')[0]);
		ndryshoKonfiguriminInit();
	}
	var prm = Sys.WebForms.PageRequestManager.getInstance();
	prm.add_endRequest(EndRequestHandler);
	prm.add_endRequest(myMesazh.EndRequestTimer);
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

function merrTeDhena() {
} 

function ShikoFoto(key,llojDok,lupaHeader) {
	myButtonClickLupa.LupaUniversal_Click(lupaHeader, 'CRMLupaHistoriku.aspx?id=' + key+"&llojDok="+llojDok, 800, 600);
}

function tabChanged(s, e) {
    indexModifiko = gvHistoriku.GetFocusedRowIndex();
    if (mbush) {
        if (indexModifiko != -1) {
            OnGridDoubleClick(indexModifiko);
        }
        else {
            mbush = false;
            $('#hfShtimModifikim').val('shtim');
            $('#hfId').val(0); pastrofusha();
        }
    }
    kaloTab = false;
    ASPxMenu1.AdjustControl();
}

function hapLupeHarte(s, e, vjenNga) {
    popupUniversal.SetHeaderText('Kordinatat e takimit ne harte');
    popupUniversal.SetContentUrl('LupaHarta.aspx?vjenNga='+vjenNga);
    popupUniversal.SetSize(700, 600);
    popupUniversal.Show();
}

function Click_ButtonOk(s, e) {
    popFshi.Hide();
    Utils.shfaqLoadingGif();;
}

function Row_DblClick(s, e) {
    OnGridDoubleClick(e.visibleIndex);  
    kaloTab=true; 
}

function closing(s, e) {
    popupUniversal.SetContentUrl('');
}