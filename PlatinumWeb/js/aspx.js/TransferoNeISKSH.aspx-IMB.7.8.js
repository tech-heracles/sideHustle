;
var btnFiltrat;

$(document).ready(function (e) {
    $(window).on("load", function () {
        myMesazh.InicializoTimer();
        myMesazh.shtoHandler();
    });
});

function Init() {
    if (typeof (isPostBack) == "undefined") {
        myMesazh.shtoHandler();
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(EndRequestHandler);
    }
}

function menu_click(s, e) {
    switch (e.item.name) {
        case "ListaGabimeve":
            e.processOnServer = false;
            if (hfKaGabime.value == "false")
                myMesazh.ShtoMesazhGabimi("Kontrolloni rreshtat per transferim perpara se te hapni listen e gabimeve!");
            else
                window.open("RaportiShpejte.aspx?emriReal=gabimeImporti&printo=0&Sesioni=false&db=jo&scopeID=" + Utils.getUrlVar("scopeID"));
            break;
        case "Kontrollo":
        case "Transfero":
            Utils.shfaqLoadingGif();
            break;
    }
}

function textChanged(s, e) {
    hfState.Set("ruajtjeFiltri", true);
    myMenu.textChanged(s, e);
}

function ndryshoKonfiguriminInit() {
    callWebserviceKonfigurimiInit(3097, cmbKonfigurimi.GetText());
}

function callWebserviceKonfigurimiInit(idKomp, kodKonf) {
    var idGjuha = hfState.Get('_idGjuha');
    var idNdermarrje = hfState.Get('_idNdermarrje');
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
        data: JSON.stringify({
            idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: idNdermarrje, idGjuha: idGjuha
        })
    }).done(SucceededCallbackKonfig);
}

function SucceededCallbackKonfig(result) {
    if (result != "" && result != null) {
        var colKontrollet = result.colKontrollet;
        var colAtrTrupi = result.colAtrTrupi;
        cmbKategoria.SetValue(1);
        var arrTabela = ['tblKontrollet'];
        var data = new Date();
        dataNga.SetDate(new Date(data.toString()));
        dataDeri.SetDate(new Date(data.toString()));
        myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, null, null, '', arrTabela, undefined, undefined, undefined, ["divKontrollet"], undefined, undefined, false);
        if (cmbKategoria.FindItemByText(cmbKategoria.GetText()) == null)
            cmbKategoria.SetValue(1);
    }
}

function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gv_TransferoNeISKSH, hfState.Get("_idKomponente"), cmbKonfigurimi.GetText());
}

function BeginCallback(s, e) {
    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}

function checkText(s, e) {
    myMenu.checkText(s, e);
}

function EndRequestHandler(sender, args) {
    if (hfState.Get("ruajtjeFiltri") && btnFiltrat) {
        btnFiltrat.PerformCallback();
    }
    hfState.Set("ruajtjeFiltri", false);
}

function SetBtnFiltraValue(s, e) {
    cmbfiltra.SetValue(s.GetValue());
}

function clickExport(e) {
    if (gv_TransferoNeISKSH.GetSelectedRowCount() == 0) {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgZgjdhniNjeNgaElementetEListes"));
        e.processOnServer = false;
    }
}

function txtPathDbAksesi_Init(s, e) {
    var pathDbAksesi = localStorage.getItem("txtPathDbAksesi");
    txtPathDbAksesi.SetText(pathDbAksesi);
}

function txtPathDbAksesi_TextChanged(s, e) {
    localStorage.setItem("txtPathDbAksesi", txtPathDbAksesi.GetText());
}