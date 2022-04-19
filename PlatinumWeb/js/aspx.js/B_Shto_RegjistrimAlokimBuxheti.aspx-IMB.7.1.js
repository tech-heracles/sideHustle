;

var _idKomponente;
var _idGjuha;
var _idNdermarrje;
var _idNdermViti;
var _idPerdoruesi;
var _idViti;
var colKontrollet;
var colAtrTrupi;
var myMesazh;
var ndryshuarNjehere;
var gridaTrupi;
var _kokaHapurAlokimBuxheti;


var varKonfig = {
    identifikuesPerLocalStorageKey: 'AlokimBuxheti'
};

$(document).ready(function () {
	$(document).on("keydown", function (e) {//po
		switch (e.which) {
			case 13:
				e.preventDefault();
				break;
			case 116: //F5
				if (window.parent !== undefined)
					window.parent.rifresko = true;
				break;
			case 82:
				if (e.ctrlKey) //ctrl+r
					window.parent.rifresko = true;
			default:
				break;
		}
	});
	Utils.konfiguroAccorditionNeDocReady(varKonfig.identifikuesPerLocalStorageKey);
});

$(window).on("load", function () {
    Init();
    DevExpress.localization.locale(_idGjuha == 0 ? "al" : "en");
});


function Init() {
	changeName();
	myMesazh.shtoHandler();
	ndryshoKonfigurimin();
}


function changeName() {
	_idKomponente = hfState.Get("_idKomponente");
	_idGjuha = hfState.Get("_idGjuha");
	_idNdermarrje = hfState.Get("_idNdermarrje");
	_idNdermViti = hfState.Get("_idNdermarrjeVit");
	_idPerdoruesi = hfState.Get("_idPerdoruesi");
	_idViti = hfState.Get("_idViti");
	myFaqeCelje.changeName("B_Shto_RegjistrimBuxheti.aspx?planifikim_miratim=" + Utils.getUrlVar('planifikim_miratim'), 0);
}


function menu_click(s, e) {
	switch (e.item.name) {
		case "Shto":
			e.processOnServer = false;
			$("#hfShtimModifikim").val("shtim");
			pastroFusha(true);
			break;
		case "Fshi":
			e.processOnServer = false;
			callWebserviceFshiDokumentAlokimBuxheti();
			break;
		case "Ruaj":
			if (!myFaqeCelje.validim(s, e))
				return;
			e.processOnServer = false;
			RuajDokumentAlokimBuxheti(1);
			break;
		case "Draft":
			if (!myFaqeCelje.validim(s, e))
				return;
			e.processOnServer = false;
			RuajDokumentAlokimBuxheti(0);
			break;
		case "Posto":
			e.processOnServer = false;
			callWebservicePostoDokumentAlokimBuxheti();
			break;
		case "Anullo":
		    myFaqeCelje.kontrolloTeDrejta('B_RegjistrimBuxheti.aspx?lloji=alokim');
			e.processOnServer = false;
			break;
	    case "Eksporto":
	        e.processOnServer = false;
	        callWebserviceEksportoDokumentAlokimBuxheti();
	        break;
		default:
			break;
	}
}

function callWebserviceEksportoDokumentAlokimBuxheti() {
    Utils.shfaqLoadingGif();
    var eshteKonvertim = $("#hfShtimModifikim").val() == "konvertim";
    var idDok = (eshteKonvertim) ? _kokaHapurAlokimBuxheti.IdKokaKonvertimiNga : $("#hfId").val();
    var wsUrl = Utils.getServerApiUrl('Buxheti', 'EksportoDokumentBuxheti');
    var params = { idNdermarrje: _idNdermarrje, eshtekonvertim: eshteKonvertim, idkokabuxheti: idDok, data: dteDate.GetText() };
    Utils.EksportoDokumentNgaWebService(wsUrl, params);
    Utils.hiqLoadingGif();
}

function ndryshoKonfigurimin() {
	var cmbModKontroll = cmbModeli.GetSelectedItem();
	var pershkKonfigAmb = cmbModKontroll.GetColumnText("PershkrimKonfigAmbjente");
	var pershKokeDok = "Kokë Dokumenti";
	if (pershkKonfigAmb != undefined) {
		lblKonfigurimi.SetText(pershkKonfigAmb);
		lblKonfigurimi.SetVisible(false);
		$('#kokeKonfigurimi').text(pershKokeDok + ': ' + pershkKonfigAmb);
	}
	callWebserviceKonfigurimi(cmbModKontroll.text);
}


function ndryshoKonfiguriminInit() {
	callWebserviceKonfigurimiInit(cmbModeli.GetText());
}

function callWebserviceKonfigurimi(kodKonf) {
	Utils.shfaqLoadingGif();
	var eshteShtim = $("#hfShtimModifikim") == 'shtim';
	$.ajax({
		url: Utils.getServerApiUrl("Konfigurime", "ktheKonfigDB"),
		data: JSON.stringify({ idKomp: _idKomponente, kodKonf: kodKonf, idNdermarrje: _idNdermarrje, kodKontrollKlienti: "", idKlienti: -1, shtim: eshteShtim, merrFormatKursi: false, idGjuha: _idGjuha })
	}).done(SucceededCallbackKonfig);
}

function callWebserviceKonfigurimiInit(kodKonf) {
	callWebserviceKonfigurimi(kodKonf);
}

var formatNumriZgjedhur;
function SucceededCallbackKonfig(result) {
	if (result != "" && result != null) {
		colKontrollet = result.colKontroll;
		colAtrTrupi = result.colAtrTrupi;
		resultkonf = result;
		formatNumriZgjedhur = result.formatNumri;
		var hf = $("#hfKontrollet");
		var hfMod = $("#hfShtimModifikim");
		var arrTabela = ['tblFillim'];
		var arrPrind = ["dvFillim"];
		myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, hf, hfMod, "", arrTabela, undefined, undefined, undefined, arrPrind);
		myFaqeCelje.rregulloGjeresiteFushave(arrTabela);
		Utils.konfiguroAccorditionPasKonfigDokumenti(varKonfig.identifikuesPerLocalStorageKey);

		Utils.setFormatNumri(txtBuxhetiMiratuar, formatNumriZgjedhur.ShifraPasPresjesVlefta || 2);

		pastroFusha(false);
		ndryshuarNjehere = false;

		percaktoVisibleMenuBuxheti(hfMod.val() == 'modifikim');

	}
	Utils.hiqLoadingGif();
}

/*
Function: TextChangedNiveli
Therret funksionin <callWebserviceNiveli> per te vendosur templaten sipas nivelit te zgjedhur.
*/
function TextChangedNiveli() {//po
	var hidField1 = document.getElementById("hfVeprimi");
	var mod;
	if (pageState.lloji != 'shtim')
		mod = true;
	else mod = false;
	if (cmbNiveli.GetText() != "") {
		callWebserviceNiveli(cmbNiveli.GetValue(), hidField1.value, mod);
		percaktoMenuSipasTeDrejtavePerNivelin(cmbNiveli.GetValue());
	}
	else callWebserviceNiveli(0, hidField1.value, mod);
}

function callWebserviceNiveli(idNiveli, veprimi, mod) {//po
	try {
		$.ajax({
			pritPergjigje: true,
			url: Utils.getServerApiUrl("Rregjistrime", "ktheTemplateNiveli"), data: JSON.stringify({ idNiveli: idNiveli, veprimi: veprimi, mod: mod, idPerdoruesi: pageState.idPerdoruesi, idNdermarrje: pageState.idNdermarrje, idGjuha: pageState.idGjuha })
		}).done(SucceededCallbackNiveli);
	}
	catch (e) {
		console.log(e.message)
		myMesazh.ShtoMesazhGabimi(hfState.Get("msgNdodhiGabimGjateMarrjesSeTeDhenave"));
	}
}

/*
Function: SucceededCallbackNiveli

Mbush combo-n modeli me vlerat sipas nivelit te zgjedhur
*/
var selektoKonfigurim = false;

function SucceededCallbackNiveli(colModelet) {
	if ($("#hfShtimModifikim").val() == "shtim") {
		cmbModeli.ClearItems();
		for (i = 0; i < colModelet.length; i++) {
			cmbModeli.AddItem([colModelet[i].KodKonfigAmbjente, colModelet[i].PershkrimKonfigAmbjente], colModelet[i].IdKonfigAmbjente);
		}
		cmbModeli.SelectIndex(0);
	}
	ndryshoKonfigurimin();
}


function callWebserviceDokumentAlokimBuxheti() {
	Utils.shfaqLoadingGif();
	$.ajax({
		url: Utils.getServerApiUrl("Buxheti", "KtheDokumentAlokimBuxheti"),
		data: JSON.stringify({
		    idKomponente: _idKomponente, idKonfigurim: cmbModeli.GetValue(), idNdermarrje: _idNdermarrje, idGjuha: _idGjuha, emerGride: 'dxTreeList_TrupAlokimi',
		    idKokaAlokimi: $("#hfId").val(), llojKonvertimiNga: hfState.Get("llojKonvertimiNga"), shtimModifikim: $("#hfShtimModifikim").val()
		})
	}).done(SucceededCallbackDokumentAlokimBuxheti);
}

function RuajDokumentAlokimBuxheti(idStatusDok) {
	unFormatoFushaDevi();
	var kokaAlokimBuxheti = krijoKokeDokumenti(idStatusDok);
	var trupiAlokimBuxheti = krijoTrupDokumenti(kokaAlokimBuxheti.IdBuxhetiKoka);
	callWebserviceRuajDokument(kokaAlokimBuxheti, trupiAlokimBuxheti);
}

function callWebservicePostoDokumentAlokimBuxheti() {
	Utils.shfaqLoadingGif();
	var komponente = "B_Shto_RegjistrimAlokimBuxheti.aspx";
	$.ajax({
	    url: Utils.getServerApiUrl("Buxheti", "PostoDokumentAlokimBuxheti"),
	    data: JSON.stringify({ idKokaAlokimi: $("#hfId").val(), komponente: komponente, idNdermarrje: _idNdermarrje, idPerdoruesi: _idPerdoruesi, idViti: _idViti, idSkemaWF: 0, statusAprovimi: 0 })
	}).done(SucceededCallbackPostoDokument);
}

function callWebserviceRuajDokument(kokaBuxheti, trupiBuxheti) {
	Utils.shfaqLoadingGif();
	var komponente = "B_Shto_RegjistrimAlokimBuxheti.aspx";
	$.ajax({
		url: Utils.getServerApiUrl("Buxheti", "RuajDokumentBuxheti"),
		data: JSON.stringify({ komponente: komponente, idNdermarrje: _idNdermarrje, idPerdoruesi: _idPerdoruesi, idViti: _idViti, kokaBuxheti: kokaBuxheti, trupiBuxheti: trupiBuxheti, idSkemaWF: 0, statusAprovimi: 0, idEtapa: 0 })
	}).done(SucceededCallbackRuajDokument);
}

function callWebserviceFshiDokumentAlokimBuxheti() {
	Utils.shfaqLoadingGif();
	var komponente = "B_Shto_RegjistrimAlokimBuxheti.aspx";
	$.ajax({
		pritPergjigje: true,
		url: Utils.getServerApiUrl("Buxheti", "FshiDokumentBuxheti"),
		data: JSON.stringify({ ids: new Array($('#hfId').val()), komponente: komponente })
	}).done(SucceededCallbackFshiDokumentAlokimBuxheti)
}

function SucceededCallbackPostoDokument(result) {
	ShfaqMesazh(result.mesazh);
	Utils.hiqLoadingGif();
}

function SucceededCallbackRuajDokument(result) {
	var status = ShfaqMesazh(result.mesazh);
	if (status) {
		$("#hfShtimModifikim").val("shtim");
		var hf = $("#hfKontrollet");
		var hfMod = $("#hfShtimModifikim");
		var arrTabela = ["tblFillim"];
		var arrPrind = ["dvFillim"];
		myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, hf, hfMod, "", arrTabela, undefined, undefined, undefined, arrPrind);
		myFaqeCelje.rregulloGjeresiteFushave(arrTabela);
		pastroFusha(true);
	}
	formatoFushaDevi();
}

function SucceededCallbackFshiDokumentAlokimBuxheti(result) {
	if (result.mesazhSukses != "")
		myMesazh.ShtoMesazhSuksesi(result.mesazhSukses);
	if (result.mesazhGabim != "")
		myMesazh.ShtoMesazhGabimi(result.mesazhGabim);

	if (result.mesazhSukses != "")
	    myFaqeCelje.kontrolloTeDrejta('B_RegjistrimBuxheti.aspx?lloji=alokim');
	Utils.hiqLoadingGif();

}

function ShfaqMesazh(mesazh) {
	if (!mesazh.Status)
		myMesazh.ShtoMesazhGabimi(mesazh.PershkrimMesazhi);
	else
		myMesazh.ShtoMesazhSuksesi(mesazh.PershkrimMesazhi);
	return mesazh.Status;
}

function SucceededCallbackDokumentAlokimBuxheti(result) {
    var kokaAlokimBuxheti = _kokaHapurAlokimBuxheti = result.kokaAlokimBuxheti;
	var dsTrupiAlokimBuxheti = result.dsTrupiAlokimBuxheti;
	var columnsKonfig = result.columnsKonfig;
	$("#hfId").val(kokaAlokimBuxheti.IdBuxhetiKoka);

	if (kokaAlokimBuxheti.IdBuxhetiKoka > 0 || $("#hfShtimModifikim").val() == 'konvertim')
	    mbushFushaKoke(kokaAlokimBuxheti);
	if (kokaAlokimBuxheti.IdStatusDok != 0 && $("#hfShtimModifikim").val() != 'konvertim')
	    ASPxMenu1.GetItemByName('Draft').SetVisible(false);

	var columnsForSum = new Array("JanarVlera", "ShkurtVlera", "MarsVlera", "PrillVlera", "MajVlera", "QershorVlera", "KorrikVlera", "GushtVlera", "ShtatorVlera", "TetorVlera", "NentorVlera", "DhjetorVlera");
	bejGatiGride(columnsKonfig, dsTrupiAlokimBuxheti, columnsForSum);
	Utils.hiqLoadingGif();
}


function bejGatiGride(columnsKonfig, dataSource, columnsForSum) {
	gridaTrupi = new myDxTreeList("dxTreeList_TrupAlokimi", {
		dataSource: dataSource,
		keyExpr: "IdKategoriBuxhetimi",
		parentIdExpr: "IdKategoriBuxhetimiPrind",
		formatNumriZgjedhur: formatNumriZgjedhur,
		editorColumnOption: { step: 0 },
		onRowPrepared: function (row) {
		    if (row.data && row.data.IdKategoriBuxhetimiPrind == null)
		        row.rowElement.css("background", "#f5f5f5");
		}
	});

	gridaTrupi.SetColumnsFromConfig(columnsKonfig); //percakton kolonat e grides
	gridaTrupi.AddCustomOptionToColumns(new Array("VleraAlokuar"), "calculateCellValue", customTotalFunction);
	gridaTrupi.AddCustomOptionToColumns(new Array("VleraAlokuar", "VleraMiratuar"), "cssClass", "dxeDisabled_MetropolisBlue");
}

function customTotalFunction(e) {
    var rowTotal = e.JanarVlera + e.ShkurtVlera + e.MarsVlera + e.PrillVlera + e.MajVlera + e.QershorVlera + e.KorrikVlera + e.GushtVlera + e.ShtatorVlera + e.TetorVlera + e.NentorVlera + e.DhjetorVlera;
    return rowTotal;
}

function pastroFusha(fshiId) {
	if (fshiId)
	    $("#hfId").val(null);
	_kokaHapurAlokimBuxheti = null;
	cmbViti.SetSelectedIndex(0);
	txtNrDok.SetValue(null);
	dteDate.SetValue(Utils.ktheDateDefault($.parseJSON(hfState.Get("periudha"))));
	txtShenime.SetValue(null);
	txtBuxhetiMiratuar.SetText(0);
	callWebserviceDokumentAlokimBuxheti();
	percaktoVisibleMenuBuxheti($("#hfShtimModifikim").val() == 'modifikim');
	myFaqeCelje.aktivFusha(colKontrollet, colAtrTrupi, $("#hfShtimModifikim"), false, "");
	ASPxMenu1.GetItemByName('Draft').SetVisible(true);
	formatoFushaDevi();
	if ($("#hfShtimModifikim").val() == "shtim" || $("#hfShtimModifikim").val() == "konvertim") {
	    hfNrAuto.Clear();
	    hfNrAutoShitje.Clear();
	    myNrAuto.vendosNrAutomatik(colAtrTrupi, colKontrollet, dteDate.GetDate(), _kokaHapurAlokimBuxheti);
	}

}

function mbushFushaKoke(kokaAlokimBuxheti) {
	cmbViti.SetValue(kokaAlokimBuxheti.Viti);
	txtNrDok.SetValue(kokaAlokimBuxheti.NrDok);
	dteDate.SetValue(new Date(kokaAlokimBuxheti.DtDok));
	txtShenime.SetValue(kokaAlokimBuxheti.Shenime);
	if (kokaAlokimBuxheti.IdRaportDesign && $("#hfShtimModifikim").val() != "konvertim")
	    cmbFormatiPrintimit.SetValue(kokaAlokimBuxheti.IdRaportDesign);
	txtBuxhetiMiratuar.SetText(kokaAlokimBuxheti.Totali);
	formatoFushaDevi();
}

function formatoFushaDevi() {
    Utils.formatoTextBox(txtBuxhetiMiratuar);
}

function unFormatoFushaDevi() {
    Utils.unFormatoTextBox(txtBuxhetiMiratuar);
}

function percaktoVisibleMenuBuxheti(value) {
	ASPxMenu1.GetItemByName('Fshi').SetVisible(value);
	ASPxMenu1.GetItemByName('Posto').SetVisible(value);
}

function krijoKokeDokumenti(idStatusDok) {
    var idKokaAlokimi = $("#hfId").val();
    var konvertim = $("#hfShtimModifikim").val() == "konvertim";
	var clsKokaAlokimBuxheti = {};

	clsKokaAlokimBuxheti.IdBuxhetiKoka = idKokaAlokimi ? idKokaAlokimi : 0;
	clsKokaAlokimBuxheti.IdNivel = cmbNiveli.GetValue();
	clsKokaAlokimBuxheti.IdKonfigAmbjente = cmbModeli.GetValue();
	clsKokaAlokimBuxheti.Viti = cmbViti.GetValue();
	clsKokaAlokimBuxheti.IdKatDok = 172;
	clsKokaAlokimBuxheti.NrDok = txtNrDok.GetValue();
	clsKokaAlokimBuxheti.DtDok = dteDate.GetDate();
	clsKokaAlokimBuxheti.Totali = txtBuxhetiMiratuar.GetValue() ? txtBuxhetiMiratuar.GetValue() : 0;
	clsKokaAlokimBuxheti.TotaliPaTvsh = clsKokaAlokimBuxheti.Totali;
	clsKokaAlokimBuxheti.IdStatusDok = idStatusDok;
	clsKokaAlokimBuxheti.IdNderm = _idNdermarrje;
	clsKokaAlokimBuxheti.IdNdermVit = _idNdermViti;
	clsKokaAlokimBuxheti.Shenime = txtShenime.GetValue();
	if (idKokaAlokimi > 0)
		clsKokaAlokimBuxheti.IdPerdoruesi = _idPerdoruesi;
	else
		clsKokaAlokimBuxheti.IdKrijuesi = _idPerdoruesi;
	clsKokaAlokimBuxheti.IdRaportDesign = cmbFormatiPrintimit.GetValue() ? cmbFormatiPrintimit.GetValue() : 0;
	clsKokaAlokimBuxheti.IdNdermPostuesi = konvertim ? 0 : _kokaHapurAlokimBuxheti.IdNdermPostuesi;
	clsKokaAlokimBuxheti.KodNdermPostuesi = konvertim ? '' : _kokaHapurAlokimBuxheti.KodNdermPostuesi;
	clsKokaAlokimBuxheti.IdDokPostuesi = konvertim ? 0 : _kokaHapurAlokimBuxheti.IdDokPostuesi;
	clsKokaAlokimBuxheti.IdKokaKonvertimiNga = _kokaHapurAlokimBuxheti.IdKokaKonvertimiNga;
	clsKokaAlokimBuxheti.LlojKonfigKonvertimiNga = _kokaHapurAlokimBuxheti.LlojKonfigKonvertimiNga;

	return clsKokaAlokimBuxheti;
}

var muajt = [{ id: 1, value: "JanarVlera" },
           { id: 2, value: "ShkurtVlera" },
           { id: 3, value: "MarsVlera" },
           { id: 4, value: "PrillVlera" },
           { id: 5, value: "MajVlera" },
           { id: 6, value: "QershorVlera" },
           { id: 7, value: "KorrikVlera" },
           { id: 8, value: "GushtVlera" },
           { id: 9, value: "ShtatorVlera" },
           { id: 10, value: "TetorVlera" },
           { id: 11, value: "NentorVlera" },
           { id: 12, value: "DhjetorVlera" }, ]

function krijoTrupDokumenti(idKokaAlokimi) {
    gridaTrupi.SaveCurrentValues();
    var trupi = gridaTrupi.GetData();

    var colTrupiAlokimBuxheti = new Array();
    var clsTrupiAlokimBuxheti = {};
    var idAlokimiTrupi = 0;

    trupi.map(function (rreshti) {
        var totaliKategorise = 0;
        var trupPerMuajt = new Array();
        muajt.map(function (muaji) {
            clsTrupiAlokimBuxheti = {};
            idAlokimiTrupi = rreshti["IdTrupiAlokimi_" + muaji.id] ? rreshti["IdTrupiAlokimi_" + muaji.id] : 0;

            clsTrupiAlokimBuxheti.IdBuxhetiKoka = idKokaAlokimi;
            clsTrupiAlokimBuxheti.IdBuxhetiTrupi = idAlokimiTrupi;
            clsTrupiAlokimBuxheti.IdKategoriBuxhetimi = rreshti.IdKategoriBuxhetimi;
            clsTrupiAlokimBuxheti.IdNdermarrje = rreshti.IdNdermarrje;
            clsTrupiAlokimBuxheti.Periudha = muaji.id;
            clsTrupiAlokimBuxheti.Vlera = rreshti[muaji.value] ? rreshti[muaji.value] : 0;
            clsTrupiAlokimBuxheti.VleraPaTvsh = clsTrupiAlokimBuxheti.Vlera;
            clsTrupiAlokimBuxheti.IdLLojPeriudhe = 1;
            clsTrupiAlokimBuxheti.LlogaritNeGjendje = 1;
            totaliKategorise += rreshti[muaji.value] ? rreshti[muaji.value] : 0;
            clsTrupiAlokimBuxheti.VleraPlanifikuar = rreshti.VleraMiratuar ? rreshti.VleraMiratuar : 0;

            trupPerMuajt.push(clsTrupiAlokimBuxheti);
        });
        trupPerMuajt.map(function (item) { item.VleraKategorise = totaliKategorise;});
        colTrupiAlokimBuxheti = colTrupiAlokimBuxheti.concat(trupPerMuajt);
    });
    return colTrupiAlokimBuxheti;
}