;
var idModuliRaporteve;
var pageState = {}; 
function hapraportin(idRaporti, emerRaporti) {
    hapRaport(idRaporti, emerRaporti, "false");
}
function myTimeStamp() {
                var myDate = new Date();
                return myDate.getHours() + ":" + myDate.getMinutes() + ":" + myDate.getSeconds() +
    ":" + myDate.getMilliseconds();
}
function hapRaport(idRaporti, emerRaporti, filtro) {
    console.log(emerRaporti);
    if (window.parent && window.parent.SucceededCallbackInfoLart)
        window.parent.SucceededCallbackInfoLart({ emerKomponente: emerRaporti });
    var myParams = [];
    myParams.push("idraporti=" + idRaporti);
    myParams.push("idmod=" + pageState.idModuliRaporteve)
    myParams.push("windowWidth=" + $(window).width());
    myParams.push("Filtro=" + filtro);
    var ngaCRM = Utils.getUrlVar("vjenNga");
    if (ngaCRM != "undefined")
        myParams.push("vjenNga=" + ngaCRM); 

    window.location.href = (idRaporti == 292 ? "CRMHarte.aspx?" : "Raporti.aspx?") + myParams.join('&');
}

function filtroButtonClick(idRaporti, emerRaporti) {
    hapRaport(idRaporti, emerRaporti, "true");
}

function klickselectedvaluedok(theRadio, e) {
    if (theRadio.GetSelectedIndex() != -1) {
        var rblCaseControlDok = theRadio.GetSelectedItem().value;
        if (rblCaseControlDok == '1') {
            dtdoknga.SetEnabled(true);
            dtdokderi.SetEnabled(true);
        }
        else {
            dtdoknga.SetEnabled(false);
            dtdokderi.SetEnabled(false);
        }
    }
}
function initPageState() {
    pageState.idModuliRaporteve = hfState.Get("idModuli");
    pageState.idPerdoruesi = hfState.Get("idPerdoruesi");
    pageState.idNdermarrje = hfState.Get("idNdermarrje");
    pageState.idGjuha = hfState.Get("idGjuha");
    pageState.kategoria = $("#moduliLabel").text();
    pageState.fushaEmrit = "RAPEMRI";
    pageState.fushaLinkut = "NAVIGATEURL";
    pageState.idRaporti = "IDRAPORTI";
    pageState.selektorHomeMenu = "#menuHomePage";
    pageState.extendKeySize = pageState.idPerdoruesi.toString() +"_"+ pageState.idNdermarrje.toString();
    pageState.extendKeyCollapse = pageState.idPerdoruesi.toString() + "_" + pageState.idModuliRaporteve.toString();
    pageState.params = {};
    //"335",
};
function ruajKonfigClick(s, e) {
    e.processOnServer = false;
    saveKonfig(true, pageState.params);
}
$.noty.defaults = {
    layout: 'topCenter',
    theme: 'relax', // or 'relax'
    type: 'alert',
    text: '', // can be html or string
    dismissQueue: true, // If you want to use queue feature set this true
    template: '<div class="noty_message"><span class="noty_text"></span><div class="noty_close"></div></div>',
    animation: {
        open: { height: 'toggle' }, // or Animate.css class names like: 'animated bounceInLeft'
        close: { height: 'toggle' }, // or Animate.css class names like: 'animated bounceOutLeft'
        easing: 'swing',
        speed: 500 // opening & closing animation speed
    },
    timeout: 5000, // delay for closing event. Set false for sticky notifications
    force: false, // adds notification to the beginning of queue when set to true
    modal: false,
    maxVisible: 3, // you can set max visible notification for dismissQueue true option,
    killer: false, // for close all notifications before show
    closeWith: ['click'], // ['click', 'button', 'hover', 'backdrop'] // backdrop click will close all notifications
    callback: {
        onShow: function (e) {

        },
        afterShow: function () {
        },
        onClose: function () { },
        afterClose: function () { },
        onCloseClick: function () { },
    },
    buttons: false // an array of buttons
};
$(document).ready(function () {
    initPageState();
    changeName();
    $(document).keydown(Utils.documentKeyDown);
    var stringKonfigRap = hfState.Get("konfigRap");
    hfState.Remove("konfigRap");
    pageState.params = { konfigRap: stringKonfigRap == "" ? false : JSON.parse(stringKonfigRap), lista: JSON.parse(hfState.Get("listaRap")), fushaLinkut: pageState.fushaLinkut, idModuliRaporteve: pageState.idModuliRaporteve, selektorHomeMenu: pageState.selektorHomeMenu, fushaEmrit: pageState.fushaEmrit, fushaIdRaportit: pageState.idRaporti, etiketa: { kryesore: (pageState.idGjuha == 0 ? "Kryesore" : "Main"), teTjera: (pageState.idGjuha == 0 ? "Te tjera" : "Others"), raporteTeri: (pageState.idGjuha == 0 ? "Raporte te rinj" : "New reports") }, selektorMenu: ".titull" };
    ndertoKartat();
});

// Raportet e modulit si karta, njesoj si kartat e Kryefaqes (pa grupe, pa "Te tjera", pa shirita), sipas alfabetit.
// Karta hap raportin si me pare, me filtra (filtroButtonClick).
function ndertoKartat() {
    var p = pageState.params;
    var kerko = $('<div class="avec-raportet-kerko"><input type="search" placeholder="Kërko raport…" /></div>');
    $(p.selektorMenu).empty().append(kerko);
    var grid = $('<main class="dash-grid dash-grid-items"></main>');
    var bosh = $('<p class="dash-empty" hidden>Nuk ka raporte.</p>');
    $(p.selektorHomeMenu).empty().append(grid, bosh);
    var lista = (p.lista || []).slice().sort(function (a, b) {
        return String(a[p.fushaEmrit] || '').localeCompare(String(b[p.fushaEmrit] || ''), 'sq', { sensitivity: 'base' });
    });
    function hap(item) {
        filtroButtonClick(item[p.fushaIdRaportit], item[p.fushaEmrit]);
    }
    function shfaq(filtri) {
        var f = (filtri || '').toLowerCase();
        grid.empty();
        var n = 0;
        lista.forEach(function (item) {
            var emri = String(item[p.fushaEmrit] || '');
            if (f && emri.toLowerCase().indexOf(f) === -1) return;
            var c = $('<button type="button" class="dash-card dash-card-small">' +
                '<div class="dash-card-top"><span class="dash-icon"><i class="fa fa-file-text-o"></i></span><i class="fa fa-arrow-right dash-arrow"></i></div>' +
                '<div class="dash-card-body"><span class="dash-card-title"></span></div></button>');
            c.find('.dash-card-title').text(emri);
            c.on('click', function () { hap(item); });
            grid.append(c);
            n++;
        });
        bosh.prop('hidden', n > 0);
    }
    kerko.find('input').on('input', function () { shfaq(this.value); });
    shfaq('');
}

function changeName() {
    myFaqeCelje.shtoHandlerSession();
    if (window.parent && window.parent.callWebServiceKtheInfoLart) {
        window.parent.callWebServiceKtheInfoLart('Raportet.aspx?idmod=' + Utils.getUrlVar('idmod'), 0);

        ////            callWebservicePerUserLabel(emrimenuse);
        window.parent.createCookie('adresa', window.location.href, 1);
        setVisibleModuli(false);
        return;
    }
    setVisibleModuli(true);
}
function setVisibleModuli(visible) {
    if (visible)
        $("#moduliLabel").show();
    else
        $("#moduliLabel").hide();        
}
function expandAll(s, e) {

    if (Utils.getUrlVar("vjenNga") == 'CRM') {
        return;
    }
    var splitpane0 = window.parent.splitter.GetPane(0);
    var splitpane1 = window.parent.splitter.GetPane(1);
    var splitpane4 = window.parent.splitter.GetPane(2);
    var splitpane2 = splitpane1.GetPane(0);
    var splitpane3 = splitpane1.GetPane(1);
    splitpane0.Collapse(splitpane1);
    splitpane4.Collapse(splitpane1);
    splitpane2.Collapse(splitpane3);
    setVisibleModuli(true);
}

function collapseAll(s, e) {    
    if (top.location.href != window.location.href) {
        var splitpane0 = window.parent.splitter.GetPane(0);
        var splitpane1 = window.parent.splitter.GetPane(1);
        var splitpane4 = window.parent.splitter.GetPane(2);
        var splitpane2 = splitpane1.GetPane(0);
        var splitpane3 = splitpane1.GetPane(1);
        splitpane0.Expand(splitpane1);
        splitpane4.Expand(splitpane1);
        splitpane2.Expand(splitpane3);
        setVisibleModuli(false);
    }
    else {
        if (Utils.getUrlVar("vjenNga") == 'CRM') {
            window.location.href = "CRMDefault.aspx";
            return;
        }
        window.location.href = "FaqeKryesore.aspx";
    }
}
function kerkoTextChanged(filterString) {
    krijoListen({ lista: pageState.myList, filterString: filterString?filterString:this.text(), fushaEmrit: pageState.fushaEmrit, fushaLinkut: pageState.fushaLinkut, selektorHomeMenu: pageState.selektorHomeMenu, fushaIdRaportit: pageState.idRaporti });
}
function kerkoInit(s, e) {    
    }