; $(document).ready(function (e) {
    changeName();
});

var editorGlobal;

function ndryshoKonfigurimin() {
}

function _getKeyCode(evt) {
	return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ?
				evt.keyCode : evt.charCode;
}

function changeName() {
	var hf = $("#hfKonffillestar")[0];
	
	var prm = Sys.WebForms.PageRequestManager.getInstance();
	//prm.add_endRequest(EndRequestHandler);
	prm.add_endRequest(myMesazh.EndRequestTimer);

	myFaqeCelje.changeName(hfState.Get('komponenteRaporti'), 0);
	
}


function SucceededCallbackMesazhi(result) {
    if (result == null)
        return;
	if (result && result.d)
		result = result.d;
	if (result.length == undefined)
		return;

	
	var arr = result.split(':');
	if (arr[1] == "Green") {

		myMesazh.ShtoMesazhSuksesi(arr[0]);
	}
	else if (arr[1] == "Red")
		myMesazh.ShtoMesazhGabimi(arr[0]);

	Utils.hiqLoadingGif();;

}

function gvEndCallback(s, e) {

	$.ajax({
	    url: Utils.getServerApiUrl("Konfigurime", "lexoMesazhNgaSessioni"),
    data: JSON.stringify({})
	}).done(SucceededCallbackMesazhi);
}

function ItemClickMenu(s, e) {
    menu_click(s,e);
}
function MenuInfoInit(s, e) {
    $('#dvMenu').show();
    myMesazh.InicializoTimer();
}

function ButtonClickNdermarrje(editor) {
    editorGlobal = editor;
    var raportuese = hfState.Get('Raportuese');
    myButtonClickLupa.LupaUniversal_Click("Zgjidh ndermarrjet", 'LupaNdermarjeBij.aspx?vjenNga=AB&Raportuese=' + raportuese, 750, 600);
}

function FiltroRaport()
{
    gvRaporti.PerformCallback("filtrim");
}
