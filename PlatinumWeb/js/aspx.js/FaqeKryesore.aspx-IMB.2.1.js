;
function Back() {
    history.go(-1);
    return false;
}
function Forward() {
    history.go(+1);
    return false;
}
function grupKlick(s, e, emrimenuse) {
    switch (emrimenuse) {
        case "settings":
            shfaqSettings();
            break;
        case "Dashboard.aspx":
            var paneKryesor = splitter.GetPaneByName('paneKryesor');
            paneKryesor.SetContentUrl(emrimenuse);
            paneKryesor.RefreshContentUrl();
            break;
        case "Mobile":
            $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("Konfigurime", "merrKonfigurimeMobile")
            }).done(function (result) {
                var guidObject = ktheGuidObject(result.param);
                window.open(result.urlMobile + "" + encodeURIComponent(JSON.stringify(guidObject)), '_blank');
            });
            break;
        default:
            break;
    }
}


function MbyllEmailPopup() {
    document.getElementById("popup-container").style.display = "none";
}
function kontrolloTeDrejta(s, e, emrimenuse) {
    if (e.item.GetItemCount && e.item.GetItemCount() > 0) //deri tani perdorej emri bosh, qe ketej e tutje kush bij nuk klikohet
        return;
    if (e.item.menu && e.item.parent == e.item.menu.GetRootItem())
        return;
    sessionStorage.removeItem(pageState.guid);
    if (emrimenuse === "mesazhe") {
        //$(".dialog-mesazhet").modal("show");
        myMesazh.ndertoMesazhet({ idGjuha: parseInt(hfState.Get("idGjuha")) });
        return;
    }

    if (emrimenuse === "abonimi") {
        myAbonim.ndertoAbonim({ idGjuha: parseInt(hfState.Get("idGjuha")) });
        return;
    }
    if (emrimenuse === "google") {
        signInWithGooglePopup();
        return;
    }

    if (emrimenuse === "transferimDaljePopup") {
        TransferimSerialeUnike.ndertoPopupTransferimSerialeUnike({ idGjuha: parseInt(hfState.Get("idGjuha")), idNdermarrje: pageState.idNdermarrje });
        return;
    }
    if (emrimenuse === "" || emrimenuse === "ikonaImazhPerdorues" || emrimenuse === "dalje" || emrimenuse === "manuali")
        return;
    if (emrimenuse.indexOf("alpha_web_help") !== -1)
        return;
    if (emrimenuse.indexOf("RemoteSupport") !== -1) {
        popupRemoteSupport.Show();
        return;
    }
    if (emrimenuse.indexOf("ProgramKaseNew") !== -1) {
        downloadURL("Kase/KASA.exe");
        return;
    }
    if (emrimenuse.indexOf("ProgramKase") !== -1) {
        downloadURL('Kase/IMBKase.exe');
        return;
    }

    if (emrimenuse === "Versioni") {
        return;
    }

    if (emrimenuse.indexOf("LupaPersonalizoPerdorues.aspx") !== -1) {
        emrimenuse = "LupaPersonalizoPerdorues.aspx";
        emrimenuse = Utils.setUrlVar("scopeID", Utils.getUrlVar("scopeID"), emrimenuse);
        myButtonClickLupa.LupaUniversal_Click("Perdorues", emrimenuse, 870, 550);
        return;
    }
    if (emrimenuse.indexOf("GISDefault") !== -1) {
        emrimenuse = 'GISDefault.aspx';
        emrimenuse = Utils.setUrlVar("scopeID", Utils.getUrlVar("scopeID"), emrimenuse);
        window.open(emrimenuse, '_blank');
        return;
    }

    if (emrimenuse.indexOf("CRM") > -1) {
        emrimenuse = Utils.setUrlVar("scopeID", Utils.getUrlVar("scopeID"), emrimenuse);
        window.open(emrimenuse, '_blank');
        return;
    }
    myCookies.createCookie('adresa', emrimenuse, 1);
    if (emrimenuse.indexOf("alpha_web_help") === -1 && emrimenuse.indexOf("RemoteSupport") === -1) {

        var idTheme = Utils.getNumberOrDefaultFromUrl('idTheme') == 0 ? hfState.Get("idTheme") : 0;

        if (isNaN(idTheme)) idTheme = 0;

        if (idTheme > 0) emrimenuse = emrimenuse.indexOf('?') != -1 ? emrimenuse + '&idTheme=' + idTheme : emrimenuse + '?' + 'idTheme=' + idTheme;

        if (emrimenuse.indexOf("ABPivotGrid") > -1) {
            emrimenuse = emrimenuse + "&width=" + window.document.body.clientWidth;
        }
        emrimenuse = Utils.setUrlVar("scopeID", Utils.getUrlVar("scopeID"), emrimenuse);


        if (e.htmlEvent.ctrlKey) {
            window.open(Utils.setUrlVar("newScopeId", "True", Utils.setUrlVar('ambienti', emrimenuse, window.location.origin + window.location.pathname)), '_blank');
            return;
        }
        var paneKryesor = splitter.GetPaneByName('paneKryesor');
        paneKryesor.SetContentUrl(emrimenuse);
        paneKryesor.RefreshContentUrl();
    }
}

var downloadURL = function downloadURL(url) {
    var hiddenIFrameID = 'hiddenDownloader',
        iframe = document.getElementById(hiddenIFrameID);
    if (iframe === null) {
        iframe = document.createElement('iframe');
        iframe.id = hiddenIFrameID;
        iframe.style.display = 'none';
        document.body.appendChild(iframe);
    }
    iframe.src = url;
    //  location.href = url;
};

function btnDownloadProgramKaseClick(s, e) {
    e.processOnServer = false;
    downloadURL('Kase/NodeJsKase.zip');
}

function SuccededVendosPeriudha(result) {
    pageKryesoreState.periudha = result;
    //vendosHfPeriudhe(id, textPeriudhe, dateFillim, dateMbarim);
    hfState.Set("periudha", JSON.stringify(result));
    popupUniversal.Hide();
    var btnPeriudha = splitter.GetPaneByName('Footer').GetContentIFrame().contentWindow.btnPeriudha;
    if (btnPeriudha != undefined)
        btnPeriudha.SetText(result.EmerPeriudha + " (" + new Date(result.FillimiPeriudha).getFullYear() + ")");
    if (result.Ekycur)
        btnPeriudha.inputElement.style.color = "Gray";
    else
        btnPeriudha.inputElement.style.color = "Black";
}

function lexoHfPeriudhe() {
    return JSON.parse(hfState.Get("periudha"));
    //return pageKryesoreState.periudha;
    //var HfPeriudheObj = new Object();
    //HfPeriudheObj.idPeriudha = hfPeriudhKontabel.Get("idPeriudha");
    //HfPeriudheObj.emerPeriudha = hfPeriudhKontabel.Get("emerPeriudha");
    //HfPeriudheObj.fillimiPeriudha = hfPeriudhKontabel.Get("fillimiPeriudha");
    //HfPeriudheObj.mbarimiPeriudha = hfPeriudhKontabel.Get("mbarimiPeriudha");
    //return HfPeriudheObj;
}

function fshiCookie() {
    myCookies.createCookie('adresa', "", -1);
}

function readCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) === 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}

function createCookie(name, value, days) {
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        var expires = "; expires=" + date.toGMTString();
    }
    else expires = "";
    document.cookie = name + "=" + value + expires + "; path=/";
}

var rifresko, fayeClient, currentChannel, channels = [
    { tabSelector: '#tabs-njoftime', text: 'Njoftime', channel: '/global/njoftime', publish: '/global/njoftime', canPublish: false, indeks: 0 },
    { tabSelector: '#tabs-ndihme', text: 'Ndihme', channel: '/global/ndihme', publish: '/global/ndihme', canPublish: true, indeks: 1 },
    { tabSelector: '#tabs-nderm-6', text: '6', channel: '/global/metaNderm/6', publish: '/global/metaNderm/6', canPublish: true, indeks: 2 },
    { tabSelector: '#tabs-nderm-rsu', text: 'RSU', channel: '/global/metaRol/6/rsu', publish: '/global/metaRol/6/rsu', canPublish: true, indeks: 3 },
    { tabSelector: '#tabs-nderm-ra', text: 'RA', channel: '/global/metaRol/6/ra', publish: '/global/metaRol/6/ra', canPublish: true, indeks: 4 }
];

var selectedTabSelector, pageKryesoreState = {};
var pageState = { menuJson: {}, guid: '', webhook: {} };
function hideNotice() {
    $(".footer1").parent().css("margin-top", "0px");
    document.querySelector("#certificate-notice").style.display = "none";
    document.querySelector(".einvoice-notice").style.display = "none";
    document.querySelector(".x-image").style.display = "none";
}
function SucceededCallbackVerifyEmail(result) {
    if (!result)
        $("#popup-container").css("display", "block");
}
$(document).ready(function (e) {
    rifresko = false;
    //document.getElementById("organizata_p").innerHTML = " " + hfState.Get("organizata");
    //if (hfState.Get("adminUser")) {
    //    $.ajax({
    //        async: true,
    //        url: Utils.getServerApiUrl("Autorizime", "CheckIfEmailIsVerified"),
    //        data: JSON.stringify({ email: hfState.Get("emailPerdoruesi") })
    //    }).done(SucceededCallbackVerifyEmail);
    //}

    //$(".footer1").parent().css("margin-top", "-30px");
    //$(".footer1").parent().css("position", "absolute");
    $(window).on('unload', function (event) {
    });
    //var dateSkadimiCertifikate = hfState.Get("SkadimCertifikate");
    //if (dateSkadimiCertifikate != "" && dateSkadimiCertifikate != undefined) {
    //    var opsionMbyllje = hfState.Get("MenuItemMbyll");
    //    var popUpOptions = { prependSelector: "body", dialogClass: "dialog-skadim-certifikate", contentClass: "tabele-skadim-certifikate", titulli: "Njoftim", text: { mbyll: opsionMbyllje } };
    //    var myPopup = Utils.ndertoPopupCertifikata(popUpOptions, dateSkadimiCertifikate);
    //    myPopup.modal("show");
    //    $("#certificate-notice").css("display", "block");
    //    document.getElementById("certificate-notice").innerHTML = "Certifikata Elektronike e Fiskalizimit per kompanine tuaj skadon ne date <b>" + dateSkadimiCertifikate + "</b> Ju lutem ngarkoni certifikaten e re. (Pas dates <b>" + dateSkadimiCertifikate + "</b> nuk do mund te leshoni fatura me certifikaten e vjeter.)"

    //    $(".footer1").parent().css("margin-top", "-50px");
    //    $(".footer1").parent().css("position", "absolute");
    //}

    $(document).on('keydown', function (e) {//po
        switch (e.which) {
            case 13:
                e.preventDefault();
                break;
            case 116: //F5
                rifresko = true;
                break;
            case 82:
                if (e.ctrlKey) //ctrl+r
                    rifresko = true;
                break;
            default:
                break;
        }
    });

    $(window).on('load', function () {

        Utils.PushToGoogleAnalytics(hfState.Get("googleAnalytics"), hfState.Get("googleAnalyticsTrackingId"));
        pageKryesoreState.periudha = $.parseJSON(hfState.Get("periudha"));
        stringKonfigMenuMajtas = hfState.Get("konfigMenuMajtas");
        stringKonfigMenuSiper = hfState.Get("konfigMenuSiper");
        pageState.idPerdoruesi = hfState.Get("idPerdoruesi");
        pageState.idNdermarrje = hfState.Get("idNdermarrje");
        window.menuMajtas = new imbMenu(navbar, stringKonfigMenuMajtas, window.menuTypes.navbar, pageState.idPerdoruesi, pageState.idNdermarrje, { idGjuha: hfState.Get("idGjuha") });
        if (Utils.getUrlVar("ambDef") == "raporteMenaxheriale")
            pergatitHapjePerMobile();

        njoftimet = JSON.parse(hfState.Get("njoftimet"));
        afishonjoftime = hfState.Get("afishonjoftime");
        if (njoftimet.length != 0 && afishonjoftime) {
            myMesazh.ndertoNjoftime({ mesazhet: njoftimet, idPerdoruesi: pageState.idPerdoruesi });
        }
        Utils.AppendLiveHelperChat(hfState.Get("chatAktiv"), hfState.Get("chatLink"), hfState.Get("chatPortHttp"), hfState.Get("chatPortHttps"));
        merrKonfigurimeWebhook(hfState.Get("idNdermarrje"));

    });

});


function shfaqSettings() {
    window.menuMajtas.personalizo();
}
var GetWebhook;
function pageRefresh(s, e) {//

    if (top.location !== document.location) {
        top.location = location;
    }
    var adresa = myCookies.readCookie('adresa');
    var paneli = splitter.GetPaneByName('paneKryesor');

    if (rifresko) {
        if (!(adresa === null)) {
            paneli.SetContentUrl(adresa, true);
        }
        else {
            paneli.SetContentUrl(Paths.defaultLoginPath, true);
        }
        btn.DoClick();

    }
    $("[name='frameKryesor']").attr('width', '100%');
    $("[name='frameFooter']").attr('width', '100%');
}

function callWebServiceKtheInfoLart(emerKomponente, id) {
    var idNdermarrje = hfState.Get("idNdermarrje");
    var idPerdoruesi = hfState.Get("idPerdoruesi");
    var idGjuha = hfState.Get("idGjuha");
    $.ajax({
        url: Utils.getServerApiUrl("Autorizime", "KtheInfoLart"),
        data: JSON.stringify({
            urlKomponente: emerKomponente, idDokRegjistrimi: id,
            idNdermarrje: idNdermarrje, idPerdoruesi: idPerdoruesi, idGjuha: idGjuha
        })
    }).done(SucceededCallbackInfoLart);
}
function konfirmoEmail() {
    $("#popup-container").css("display", "none");
    signInWithGooglePopup();

}
function SucceededCallbackEmailConfirm() {
    alert("Ju lutem konfirmoni email-in!");
    $("#popup-container").css("display", "none");
}
function callWebServiceVendosPeriudhen(idPeriudha) {

    var innerPageScopeId = Utils.getVarFromUrl(document.getElementsByName("frameKryesor")[0].contentWindow.location.href, "scopeID");
    frameKryesorScopeId = Utils.getUrlVar("scopeID");

    if (frameKryesorScopeId == innerPageScopeId)
        innerPageScopeId = "";

    var idgjuha = hfState.Get("idGjuha");
    $.ajax({
        url: Utils.getServerApiUrl("Autorizime", "vendosPeriudhen"),
        data: JSON.stringify({ idPeriudha: idPeriudha, idgjuha: idgjuha, otherScopeID: innerPageScopeId })
    }).done(SuccededVendosPeriudha);
}

function callWebServiceKtheInfoLartPerdorues(emerKomponente, id, idPerdoruesi, idNdermarrje, Logu, CultureInfo) {
    $.ajax({
        url: Utils.getServerApiUrl("Autorizime", "KtheInfoLartPerdorues"),
        data: JSON.stringify({ urlKomponente: emerKomponente, id: id, idNdermarrje: idNdermarrje, idPerdoruesi: idPerdoruesi, Logu: Logu, ci: CultureInfo })
    }).done(SucceededCallbackInfoLart);
}

function SucceededCallbackInfoLart(result) {
    if (result && result.d)
        result = result.d;
    var boolShfaqPerdoruesMenu = hfState.Get("ShfaqPerdoruesMenu");
    if (boolShfaqPerdoruesMenu) {
        var emerMbiemerPerdorues = hfState.Get("EmerMbiemerPerdorues");
        lblFaqja.SetText(emerMbiemerPerdorues + ' : ' + result.emerKomponente);
    }
    else
        lblFaqja.SetText(result.emerKomponente);
    //lblUserEmri.SetText(result.EmerPerdoruesi + '   |   ');
}

function callWebServiceKtheMesazhPerPerdoruesin() {

    $.ajax({
        url: Utils.getServerApiUrl("Autorizime", "ktheMesazhPerPerdoruesin")
    }).done(SucceededCallbackMesazhPerPerdoruesin);
}

function SucceededCallbackMesazhPerPerdoruesin(result) {
    if (result && result.d)
        result = result.d;
    lblMesazhiPer.SetText(result);
    popUpAzhornim.Show();
}

function merrImazhPerdoruesi() {
    var idPerdoruesi = hfState.Get("idPerdoruesi");
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Celje", "ktheImazhPerdoruesi"),
        data: JSON.stringify({ idPerdoruesi: idPerdoruesi })
    }).done(vendosImazhPerdoruesi);
}

function vendosImazhPerdoruesi(result) {
    if (result) {
        menu.GetItemByName("ikonaImazhPerdorues").SetImageUrl(result);
    }
}

function Click_ASPxHyperLink1(s, e) {
    var boolShfaqPerdoruesMenu = hfState.Get("ShfaqPerdoruesMenu");
    if (boolShfaqPerdoruesMenu) {
        var emerMbiemerPerdorues = hfState.Get("EmerMbiemerPerdorues");
        lblFaqja.SetText(emerMbiemerPerdorues);
    }
    else
        lblFaqja.SetText('');
}

function pergatitHapjePerMobile() {
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Konfigurime", "merrKonfigurimeMobile")
    }).done(function (result) {
        var guidObject = ktheGuidObject(result.param);
        var url = result.urlMobile + "" + encodeURIComponent(JSON.stringify(guidObject));
        loadDashboardFromUrl(url);
    });
}

function loadDashboardFromUrl(url) {
    var paneKryesor = splitter.GetPaneByName('paneKryesor');
    //kontrollohet nese ekziston faqja e mobile ne projekt
    //nese nuk ekziston vendoset faqja default
    $.ajax({
        url: url,
        type: 'HEAD',
        error: function () {
            paneKryesor.SetContentUrl("Default.aspx");
            paneKryesor.RefreshContentUrl();
            myMesazh.ShtoMesazhGabimi("Konfigurimi i dashboard-it nuk eshte i sakte!");
        },
        success: function () {
            paneKryesor.SetContentUrl(url);
            paneKryesor.RefreshContentUrl();
        }
    });
}

function ktheGuidObject(param) {
    var guid = gjeneroGUID();
    pageState.guid = guid;
    sessionStorage.setItem(guid, encodeURIComponent(JSON.stringify(param)));
    var guidObject = { id: guid };
    return guidObject;
}

function gjeneroGUID() {
    function s4() {
        return Math.floor((1 + Math.random()) * 0x10000)
            .toString(16)
            .substring(1);
    }
    return s4() + s4() + '-' + s4() + '-' + s4() + '-' + s4() + '-' + s4() + s4() + s4();
}


function bllokoFaqe() {
    Utils.blloko();
}

function zhbllokoFaqe() {
    Utils.zhblloko();
}


function merrKonfigurimeWebhook(idnderrmarje) {
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "ktheKonfigurimeWebhook"), data: JSON.stringify({ idndermarje: idnderrmarje })
    }).done(SucceededCallbackWebhook);
}

function SucceededCallbackWebhook(result) {


    pageState.webhook = result;

    if (GetWebhook == undefined && sessionStorage.getItem("GetWebhook") == null) {
        for (var i = 0; i < pageState.webhook.length; i++) {


            if (pageState.webhook[i].Eventi == 0 && pageState.webhook[i].Kategoria == 4 && pageState.webhook[i].Aktive == true) {
                var Logini = { Perdoruesi: hfState.Get("Username"), Data: new Date().toDateString(), KodNderrmarja: hfState.Get("Kadnderrmarje") };

                $.ajax({
                    type: "POST",
                    url: pageState.webhook[i].Urlpritese,

                    data: JSON.stringify(Logini),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json"
                }).done(function (response) {
                    if (response != null) {
                        console.log("success");
                        console.log(response);

                    } else {
                        console.log("Something went wrong");
                    }

                }).fail(function (response) {
                    console.log(response.responseText);
                });
            }
        }
    }
    GetWebhook = true;
    sessionStorage.setItem("GetWebhook", GetWebhook);
}
