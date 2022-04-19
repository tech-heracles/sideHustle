;$(document).ready(function (e) {
    var idNdermarrje = hfState.Get("idNdermarrje");
    var idPerdoruesi = hfState.Get("idPerdoruesi");
    var idViti = hfState.Get("idViti");
    myFaqeCelje.krijoMenuPerCRM(idPerdoruesi, idNdermarrje, idViti);
	changeName();
});


var editmode = false;
var indexEdit = -1;

//variabla per te kaluar nga nje reshti i grides tek tjetri
var indexSel = 0;
//indexi i zgjedhur per modifikim
var indexModifiko;
//perdoret per te treguar nese duhet te mbushen tabet apo jo kjo perdoret qe tabet te mbushen vetem heren e pare dhe jo pas cdo here qe  kalohet nga nje tab tek tjetri


/*
Function: menu_click
perdoret per veprimet e menuse ne javascript
Parameters: e-eventi
*/
function menu_click(s, e) {
	var hfRuaj = $('#hfRuaj')
	if (e.item.name == 'OK') {
		gvKlienti.GetSelectedFieldValues('IdKlientFurnitor', OnGridSelectionComplete);
		e.processOnServer = false;
	}
	if (e.item.name == 'Arkiva') {
		ButtonClickArkiva();
		e.processOnServer = false;
	}
	ASPxMenu1.AdjustControl();
}

function ButtonClickArkiva() {
	indexModifiko = gvKlienti.GetRowKey(gvKlienti.GetFocusedRowIndex())
	if (indexModifiko == -1)
		myMesazh.ShtoMesazhGabimi('Duhet te zgjidhni te pakten nje klient!');
	else {
		popupUniversal.SetHeaderText('Arkiva');		
		popupUniversal.SetSize(738, 548);
		popupUniversal.SetContentUrl('LupaArkiva.aspx?vjenNga=Lista&veprimi=kf&idDok=' + indexModifiko
            //+ '&shtim_modifikim=' + $('#hfShtimModifikim').val()
            );
		popupUniversal.Show();
	}
}

function OnGridSelectionComplete(values) {
	
   var klient = '';

	for (var i = 0; i < values.length; i++) {
		if (klient == '')
			klient = values[i];
		else
			klient = klient + ',' + values[i];
	}
	
	$('#hfKontrollet').val(klient);
	var obj = {
		eshteLupe:true,
		meAnketa: true,
		kategoria: 2,
		klienti:values[0]
	};
	if (values.length != 1)
	    delete obj.klienti;

	queryString = Utils.KonvertoObjectQueryString(obj);
	var url = "CRMDetyra.aspx?" + queryString;
	myButtonClickLupa.LupaUniversal_Click('Zgjidhni nje ankete ose detyre', url, 1000, 690);
}


function checkText(s, e) {
	myMenu.checkText(s, e);
}

var btnFiltrat;

function aplikoFiltra(s, e) {
	$('#hfRuaj').val('Filtra'); btnFiltrat = s;
	myMenu.aplikoFiltra(s, e, gvKlienti, "2005", '');
}

function textChanged(s, e) {
	myMenu.textChanged(s, e);
}

function changeName() {
	var prm = Sys.WebForms.PageRequestManager.getInstance();
	prm.add_endRequest(myMesazh.EndRequestTimer);
	prm.add_endRequest(EndRequestHandler);

	EndRequestHandler();
}

function _getKeyCode(evt) {
	return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ?
				evt.keyCode : evt.charCode;
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
function ClickKonfiguro(id) {
	myButtonClickLupa.LupaUniversal_Click('Zgjidhni anketen', 'CRMLupaAnketa.aspx?idklienti='+id, 900, 600);
	

}

//per lidhjet qe ka nje klient me detyra dhe anketa (subgrida)

function checkedChangedVetemTeVlefshmet(s,e)
{
    Utils.merrSubGrideSipasEmrit(s, "chkbVlefshmeria", "gvLidhjet").PerformCallback(s.GetChecked());
}
function InitVlefshmeriaChkb(s, e)
{
    s.SetChecked(Utils.merrSubGrideSipasEmrit(s, "chkbVlefshmeria", "gvLidhjet").cpVlefshmeria);
}

function EndRequestHandler() {


    if(gvKlienti.cpHequrLidhje || hfStatusi.value == 'true')
    {
        gvKlienti.Refresh();
        delete gvKlienti.cpHequrLidhje;
    }

}


function Click_ButtonOk(s, e) {
    popFshi.Hide();
    Utils.shfaqLoadingGif();
}
function closing(s, e) {
    popupUniversal.SetContentUrl('');
    gvKlienti.PerformCallback();
}