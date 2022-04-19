//[[40,20,{emri:agim,ora:8}],[]];
var editorGlobal;
var identikuesPerPopupPerdoruesi;
var bounds;
var infowindow;

$(document).ready(function () {
    google.maps.event.addDomListener(window, 'load', initialize);
    initialize();
});
function getRandomColor() {
    var letters = '0123456789ABCDEF'.split('');
    var color = '#';
    for (var i = 0; i < 6; i++) {
        color += letters[Math.floor(Math.random() * 16)];
    }
    return color;
}
function krijoContent(data) {
    var html = "<div class='myInfoWindow'><ul>";
    var li = "";

    for (var property in data) {

        li += "<li>" + property + ": " + data[property] + "</li>";
    }
    html += li;
    html += "</ul><div>";
   
    return html;
}
function MerrArrayKorinata(kords) {
    var formattedKords = kords.map(function (item) {
        var kord = item.substring(item.indexOf('(') + 1, item.indexOf(")"));

        var lat = parseFloat(kord.substring(kord.indexOf('('), kord.indexOf(" ")));
        var lng = parseFloat(kord.substring(kord.indexOf(" ") + 1));

        return new google.maps.LatLng(lng, lat);
    });
    return formattedKords;
}
function initialize() {
    var colVlerat = JSON.parse(hfState.Get('data'));//formati [Koka:{},Koordinata:[{},{}],TrupatInfo:[{},{}]]
    //var mapOptions = {
    //    zoom: 4,
    //    center: new google.maps.LatLng(41.327972, 19.818418)
    //}

    var map = new google.maps.Map(document.getElementById('map'));
    var lineSymbol = {
        path: google.maps.SymbolPath.FORWARD_CLOSED_ARROW
    };
    bounds = new google.maps.LatLngBounds();
     infowindow = new google.maps.InfoWindow();
    var vijat = new Array();

    //bredhim gjithe vijat
    for (var i = 0; i < colVlerat.length; i++) {
        vijat.push(MerrArrayKorinata(colVlerat[i].Koordinata));

        for (var k = 0; k < colVlerat[i].Koordinata.length; k++) {
            var marker = new MarkerWithLabel({ // new google.maps.Marker({
                map: map,
                position: vijat[i][k],
                animation: google.maps.Animation.DROP,
                labelContent: colVlerat[i].TrupatInfo[k].NrTakimi,
                labelAnchor: new google.maps.Point(28, 21),
                labelClass: "crm-raporte-levizja-agjentit-label",
                labelInBackground: true,
                icon: { url: 'images/CRM/klient_harte.png', scaledSize: new google.maps.Size(35, 35) },
                title: krijoContent($.extend({}, colVlerat[i].Koka, colVlerat[i].TrupatInfo[k]))
            });
            bounds.extend(vijat[i][k]);
            map.fitBounds(bounds);
            google.maps.event.addListener(marker, 'click', function () { //'mouseover'
                infowindow.setContent(this.title);
                infowindow.open(map, this);
            });
        }
        if (vijat[i].length > 1) {
            var vija = new google.maps.Polyline({
                path: vijat[i],
                geodesic: true,
                strokeColor: getRandomColor(),
                strokeOpacity: 1.0,
                strokeWeight: 3,
                map:map,
                icons: [{
                    icon: lineSymbol,
                    offset: '100%'
                }]

            });
        }
       
    }
}

//google.maps.event.addDomListener(window, 'load', initialize);




function initNgaDok(s, e) {
    //$(s.GetInputElement()).css('zIndex', 3000);
}
function ngaDokDateChanged(s, e) {
    if (txtDeriDok.GetDate() < txtNgaDok.GetDate())
        txtDeriDok.SetDate(txtNgaDok.GetDate());

    //if (Utils.getUrlVar('idraporti') == 111 || Utils.getUrlVar('idraporti') == 138 || Utils.getUrlVar('idraporti') == 187) {
    //    txtDeriDok.SetDate(txtNgaDok.GetDate());
    //}
}
function deriDokDateChanged(s, e) {
    //if (Utils.getUrlVar('idraporti') == 111 || Utils.getUrlVar('idraporti') == 138 || Utils.getUrlVar('idraporti') == 187) {
        txtNgaDok.SetDate(txtDeriDok.GetDate());
    }


function lidhes_valueChanged1(s, e, filterVeprim, filter) {
    if (s.GetSelectedIndex = 0) {
        filterVeprim.SetEnabled(false);
        filter.SetEnabled(false);
        filterVeprim.SetText('');
        filter.SetText('');
    }
    else {
        filterVeprim.SetSelectedIndex(0);
        filterVeprim.SetEnabled(true);
        filter.SetEnabled(true);
    }
}
function ButtonClickedbtnAgjentShitje(editor) {
    var headerText = hfState.Get("msgZgjidhAgjentinEShitjes");
    var contentUrl = 'LupaAgjenteShitje.aspx';
    var widthLupa = '900';
    var heightLupa = '600';
    editorGlobal = editor;
    identikuesPerPopupPerdoruesi = "raporti";
    myButtonClickLupa.LupaUniversal_Click(headerText, contentUrl, widthLupa, heightLupa);
}

function selectBenPjeseNe(s, e, veprimi) {
    if (s.GetText().indexOf(',') !== -1 || (e.htmlEvent && e.htmlEvent.which === 188)) { //nuk ka presje dhe nuk eshte presje karakteri i fundit i shtypur        
        var itemToSelect = veprimi.FindItemByValue("7");
        if (itemToSelect !== null)
            veprimi.SetSelectedItem(itemToSelect);
    }
}

function kontrolloPresje(s, e, kodi) {
    if (kodi.GetText().indexOf(',') === -1)
        return;
    myMesazh.ShtoMesazhInformues(hfState.Get("msgRaportiDuhetTeFshiniNdaresinNgaFushaEKodit"));
    var itemToSelect = s.FindItemByValue("7");
    if (itemToSelect !== null)
        s.SetSelectedItem(itemToSelect);
}
function filter2_Init(sender, event, lidhes) {
    if (lidhes.GetText() == ' ') {
        sender.SetEnabled(false);
        var name = sender.name;
        if (name.indexOf('cmbVepri') > 0)
            sender.SetSelectedIndex(-1);
        else
            sender.SetText("");
    }
}

function menu_click(s, e) {

    switch (e.item.name) {
        case "Shiko":
            e.processOnServer = false;
            callbackPanel.PerformCallback();
            break;
        case 'Pastro':
            e.processOnServer = false;
            pastroFiltrat();
            break;
        case "HapPopup":
            e.processOnServer = false;
            window.open("CRMHarte.aspx");
            break;
        case "Anullo":
            e.processOnServer = false;
            window.location = "Raportet.aspx?idmod=24";
            break;
            //else if (e.item.name == "Eksporto") { return; }

        default:
            var filtraAvancuar = navBarFiltrat.GetGroupByName("filtraAvancuar");
            filtraAvancuar.SetExpanded(false);
            e.processOnServer = false;
            break;
    }
}
function pastroFiltrat()
{
    cmbAgjentShitjesh.SetText("");
}
function init_MenuInfo(s, e) {
    $('#dvMenu').show();
    myMesazh.InicializoTimer();
}
function closing(s, e) {
    popupUniversal.SetContentUrl('');
}
function ValueChanged_radDtDok(s, e) {
    Utils.toggleKontrolletPeriudha(s,txtNgaDok,txtDeriDok);
}
function Init_radDtDok(s, e) {
    Utils.toggleKontrolletPeriudha(s,txtNgaDok,txtDeriDok);
}