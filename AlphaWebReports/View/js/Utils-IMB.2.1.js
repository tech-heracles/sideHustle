;
//cdo funksion ne kete file ka si scope window.
(function () {
    $.ajaxSettings.pritPergjigje = false;

    //atribut te cilin e perdorim per te percaktuar nese do shfaqet ose jo nje loading panel gjate kerkeses
    $.ajaxSettings.showLoading = false;

    $.ajaxSettings.sendKeepAlive = true;

    $.ajaxSetup({
        cache: false,
        type: "POST",
        data: "{}",
        processData: false,
        contentType: "application/json",
        //timeout: 100000,
        dataType: "JSON",
        beforeSend: function () {
            if (this.url && this.url.indexOf('scopeID') == -1) {
                var preffix = this.url.indexOf('?') == -1 ? "?" : "&";
                if (this.url.slice(-1) == "/")
                    this.url = this.url.substring(0, this.url.length - 1) + preffix + 'scopeID=' + Utils.getUrlVar("scopeID");
                else
                    this.url = this.url + preffix + 'scopeID=' + Utils.getUrlVar("scopeID");
            }
            if (this.pritPergjigje) Utils.nrWsRrugesManager.rritNrWsRruges();
            //if (this.showLoading && window["LoadingPanel"])
            //    Utils.shfaqLoadingGif();;
            if (this.showLoading)
                Utils.shfaqLoadingGif();
        },
        complete: function (event) {
            //ketu vjen cdo kerkese sukses apo fail.

            //nese eshte kerkuar pritja e pergjigjes per nje kerkese atehere dekrementohet counteri i 
            //pergjigjeve pending
            if (this.pritPergjigje) Utils.nrWsRrugesManager.zbritNrWsRruges();

            //nese eshte kerkuar te shfaqet nje loading gjate kerkeses ne mberritje te pergjigjes fshehim loading.
            //if (this.showLoading && window["LoadingPanel"]) Utils.hiqLoadingGif();;
            if (this.showLoading)
                Utils.hiqLoadingGif();
            //nese pergjigja e mberritur ka dhene error sesioni apo ndonje redirect,ath bejme redirect kerkesen
            var newLocation = Utils.RedirectedUrl(event);
            if (newLocation) Utils.redirect(newLocation);

            //shtyjme sesionin nese eshte kerkuar mirembajtja e tij
            if (window.parent && window.parent.SessionTimeout && this.sendKeepAlive)
                window.parent.SessionTimeout.sendKeepAlive();
        }
    });
})();

var loadingGifDiv;
$(document).ready(function () {
    var el = loadingGifDiv;
    if (!el) {
        var loadingUrl = Utils.readCookie("loadingUrl");
        loadingGifDiv = $('<div id="loadingGifDiv" class="bootstrap-iso" style="display: none"><img id="loadingIMBLogo" src="' + loadingUrl + '" alt="Loading logo" class="img-responsive center-block"/></div>');
    }
});
var Paths = {
    defaultLoginPath: "Prezantohu.aspx",
    epaySlipLoginPath: "E-PaySlip/Login.aspx",
    defaultLoginNdermarrje: "Login_Ndermarrje.aspx"
};


var Utils = {};
function NrWsRrugesManager(ambienti) {
    /// <summary>
    /// ky objekt  eshte pergjegjes per menaxhimin e numrit te kerkesave  asinkrone
    /// ne rastin e kerkesave te derguara me $.ajax numri menaxhohet automatikisht
    /// ne rastin e kerkesave me callback te kontrolleve devexpress apo te ndonje forme tjeter
    /// numri duhet te menaxhohet duke thirrur manualisht  Utils.nrWsRrugesManager.rritNrWsRruges() ne begincallback 
    /// dhe Utils.nrWsRrugesManager.zbritNrWsRruges() ne endcallback .
    /// kjo behet per te ekzekutuar funksionet te cilet varen nga kompletimi i te gjitha kerkesave async
    /// </summary>
    /// <param name="ambienti" type="String">sherben per informacion.Tregon ambjentin te cilit i perket ky objekt</param>

    // qellimi i ketyre variblave private eshte qe te mos jene globale dhe te mos preken nga jashte,
    //vlera e mynrWsRruges te mundet te ndryshohet vetem ath kur duhet duke thirrur funksionet perkates.
    var myNrWsRruges = 0;
    var myAmbjent = ambienti;

    this.zbritNrWsRruges = function () {
        /// <summary>
        /// zbret numrin e kerkesave pending dhe ekzektuon funksionet qe jane pending nese nuk ka me kerkesa pending
        /// </summary>
        myNrWsRruges--;
        if (myNrWsRruges === 0 && Utils.myQueue.length > 0) {
            Utils.execFunksionNeRadhe();
        }
    };

    this.rritNrWsRruges = function () {
        /// <summary>
        /// rrit numrin e kerkesave pending
        /// </summary>
        myNrWsRruges++;
    };

    this.kanePerfunduarWs = function () {
        /// <summary>
        /// kthen pergjigje nese ka kerkesa pending
        /// </summary>
        /// <returns type=""></returns>
        return myNrWsRruges == 0;
    };

    this.merrNrWsRruges = function () {
        /// <summary>
        /// kthen numrin e kerkesave pending (per info)
        /// </summary>
        /// <returns type=""></returns>
        return myNrWsRruges;
    };

    this.merrAmbjentKerkesash = function () {
        /// <summary>
        /// kthen url e ambientit te cilin menaxhon ky objekt
        /// </summary>
        /// <returns type=""></returns>
        return myAmbjent;
    };
};

function AutoSaverManager(func, args, context) {
    this.func = func;
    this.args = args;
    this.context = context;

    this.AutoSavePending = false;
    this.Saving = false;

    this.SetSaving = function (saving) { this.Saving = saving; }
    this.SetPending = function (pending) { this.AutoSavePending = pending; }

    this.IsSaving = function () { return this.Saving; }
    this.IsPending = function () { return this.AutoSavePending; }

    this.Save = function () {
        if (this.IsSaving()) {
            this.SetPending(true);
            return;
        }
        if (this.IsPending()) {
            this.SetPending(false);
        }
        this.SetSaving(true);
        this.func.apply(this.context, this.args);

    }



};

Utils.getServerUrlHost = function () {
    if (!window.location.origin) {
        window.location.origin = window.location.protocol + "//" + window.location.hostname + (window.location.port ? ':' + window.location.port : '');
    }
    return window.location.origin;
};
Utils.getServerApiUrl = function (controller, method) {
    return Utils.getServerUrlHost() + "/api/" + controller + "/" + method + "/";
};
Utils.myQueue = [];
Utils.myQueueGrida = {};
Utils.nrWsRrugesManager = new NrWsRrugesManager(location.href);
Utils.llojZbritje = { Perqindje: 0, Vlere: 1 };
Utils.getLlojZbritje = function (myValue) {
    var myProperty;
    $.each(Utils.llojZbritje, function (index, item) {
        if (Utils.llojZbritje[item] == myValue) {
            myProperty = item;
            return;
        }
    });
    return myProperty;
}

Utils.llojeAdresash = { Biznesi: 1, Magazine: 2, Dege: 3 };
Utils.shtoFunksionNeRadhe = function (func, idGjuha, context, grida) {
    /// <summary>
    /// shton ne radhen e ekzekutmit nje funksion
    /// </summary>
    /// <param name="func" type="function">funksioni qe do vendoset ne radhe</param>
    /// <param name="context" type="object" optional="true">si pjese e kujt objekti do te ekzekutohet funksioni,null kur eshte funksion global</param>
    /// <param name="grida" type="string" optional="true">emri i grides</param>
    this.shtoFunksionNeRadheMeParametra(func, undefined, idGjuha, context, grida);
};



Utils.shtoFunksionNeRadheMeParametra = function (func, params, idGjuha, context, grida) {
    /// <summary>
    /// shton ne radhen e ekzekutimit nje funksion
    /// </summary>
    /// <param name="func" type="function">funksioni qe do ruhen ne radhen e ekzekutimit pasi te mbarojne ws</param>
    /// <param name="params" type="Array">nje array me parametra</param>
    /// <param name="context" type="object" optional="true">si pjese e kujt objekti do te ekzekutohet funksioni,null kur eshte funksion global </param>
    /// <param name="grida" type="string" optional="true">emri i grides</param>
    var queue;
    if (this.IsNullOrEmpty(grida)) {
        queue = this.myQueue;
    }
    else {
        if (typeof (this.myQueueGrida[grida]) == "undefined")
            this.myQueueGrida[grida] = new Array();
        queue = this.myQueueGrida[grida];
    }
    if (queue.length === 1)
        throw new Error("nuk mund te shtoni dy funksione ne radhe sepse nuk eshte implementuar !!");
    queue.push({ func: func, args: params, context: context });
    if (!idGjuha)
        idGjuha = 0;
    if (!this.prisniPakSekonda) {
        this.prisniPakSekonda = myMesazh.ShtoMesazh({ type: "alert", text: (idGjuha == 0 ? 'Ju lutem prisni pak sekonda...' : 'Please wait for a couple of seconds...'), timeout: false });
    }
};

Utils.doPostback = function (s, e, doPostback) {
    if (e.processOnServer && doPostback)
        __doPostBack(s.name, "CLICK:" + e.item.index);
}

Utils.execFunksionNeRadhe = function (grida) {
    /// <summary>
    /// ekzekuton funksionin qe eshte vendosur ne radhe,sipas kontekstit te percaktuar
    /// </summary>
    /// <param name="grida" type="string" optional="true">emri i grides ne endcallback te se ciles do exe funksioni</param>
    if (grida && typeof (this.myQueueGrida[grida]) == "undefined")
        throw new Error("nuk ekziston nje queue me funksione per griden :" + grida);

    var myFuncObj = grida ? this.myQueueGrida[grida].shift() : this.myQueue.shift();
    if (myFuncObj)
        var func = myFuncObj["func"];
    var args = myFuncObj["args"];
    var context = myFuncObj["context"];

    if (args == undefined || args.length === 0)
        func.apply(context);
    else
        func.apply(context, args);

    if (this.prisniPakSekonda) {
        this.prisniPakSekonda.close();
        this.prisniPakSekonda = 0;
    }
};
Utils.KanePerfunduarWs = function () {
    /// <summary>
    /// OBSOLETE ("perdor direkt nrWsRrugesManager.kanePerfunduarWs()")
    /// kontrollon nese ka ws per te pritur qe sjellin ndyshime ne kete ambient,apo lupe (ne window)
    /// 
    /// </summary>
    /// <returns type=""></returns>
    return this.nrWsRrugesManager.kanePerfunduarWs();
};
Utils.KaFunksionPendingGrida = function (grida) {
    /// <summary>
    /// kontrollon nese ka ndonje funksion pending qe duhet te exe ne endcallback te kesaj gride
    /// </summary>
    /// <param name="grida" type="type">emri i grides</param>
    /// <returns type=""></returns>
    var queue = this.myQueueGrida[grida];
    return typeof (queue) != "undefined" && queue.length > 0;

};
Utils.KaFunksionPending = function () {
    var queue = this.myQueue;
    return typeof (queue) != "undefined" && queue.length > 0;
};
Utils.AjaxFail = function (e, x, settings, exception) {
    var message = undefined;
    /* if (e.state() == "rejected") {
         message = "Kerkesa nuk eshte pranuar nga serveri";
         //alert("Kerkesa nuk eshte pranuar nga serveri");
     }
     */
    if (e.state() === "rejected") {//&& e.status==200 ){
        console.log('e.state() === "rejected" - eshte bere logout');
        console.log("status:" + e.status + ";" + "e.responseText" + e.responseText);
        //window.location.href = "FaqeKryesore.aspx";        
        return;
    }
    if (e.status) {
        if (e.state == 404) {//gabim url
            console.log("Not Found!/");
        }
        if (e.status == 401) {
            //Kerkese e paautorizuar
            // myCookies.readCookie('adresa', Paths.defaultLoginPath, 1);
            window.location.href = "FaqeKryesore.aspx";
        }
        else {
            message = e.responseJSON != undefined ? e.responseJSON.Message : e.responseText;
        }
    }
    else if (exception == 'parsererror') {
        message = "Error.\nNuk behet ne rregull parsimi (JSON)";
    } else if (exception == 'timeout') {
        message = "Kerkesa ka tejkaluar kohen qe i eshte lejuar per te pritur! ";
    } else if (exception == 'abort') {
        message = "Kerkesa eshte nderprere nga serveri!";
    } else {
        message = "Gabim i panjohur! \n";
    }
    console.log(message);
};
Utils.fshiSessionFailCheck = function (result) {
    if (result && result.d)
        result = result.d;
    if (!result) {
        alert("Duhet te rilogoheni");
        myCookies.readCookie('adresa', Paths.defaultLoginPath, 1);
        window.location.href = "FaqeKryesore.aspx";
    }
};

Utils.hiqEnter = function (stringa) {
    var tmp = stringa;
    tmp = tmp.replace(/\r\n/g, ' '); //heq hapesirat ne IE
    tmp = tmp.replace(/\n/g, ' '); //heq hapesirat ne chrome, safari
    return tmp;
};

Utils.getUrlVars = function () {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
};
Utils.buildUrl = function (emerFaqe, urlVars) {
    return urlVars ? emerFaqe + "?" + $.param(urlVars) : emerFaqe;
};
Utils.toggleKontrolletPeriudha = function (theRadio, txtNga, txtDeri) {
    if (theRadio.GetSelectedIndex() != -1) {
        var rblCaseControl = theRadio.GetSelectedItem().value;
        if (rblCaseControl == 'Periudha') {
            if (Utils.getUrlVar('idraporti') == 98 || Utils.getUrlVar('idraporti') == 353) {
                if ($(txtDeri.GetInputElement()).attr('name').match('txtDeriDok$') != null) //kontrollon nese kontrollit i mbaron emri me txtDeriDok
                    txtNga.SetEnabled(false); //txtDeriDok
                txtDeri.SetEnabled(true);
            }
            else {
                txtNga.SetEnabled(true);
                txtDeri.SetEnabled(true);
            }
        }
        else {
            txtNga.SetEnabled(false);
            txtDeri.SetEnabled(false);
        }
        if (Utils.getUrlVar('idraporti') == 111 || Utils.getUrlVar('idraporti') == 138) {
            if ($(txtDeri.GetInputElement()).attr('name').match('txtDeriDok$') != null) //kontrollon nese kontrollit i mbaron emri me txtDeriDok
                txtDeri.SetEnabled(false); //txtDeriDok
        }
    }
};

Utils.getUrlVar = function (name) {
    return decodeURIComponent(Utils.getUrlVars()[name]);
};
Utils.getNumberOrDefaultFromUrl = function (name) {
    /// <summary>
    /// perdoret kur nga querystring duhet te merret nje vlere number (jo undefined)
    /// </summary>
    /// <param name="name" type="type"></param>
    var valueNgaQueryString = Utils.getUrlVar(name);
    return (valueNgaQueryString == undefined || valueNgaQueryString == "undefined" || valueNgaQueryString == '' || valueNgaQueryString == 'null') ? 0 : valueNgaQueryString;
};
Utils.setUrlVar = function (name, value, url) {
    if (!url)
        url = window.location.href;
    if (value.indexOf('?') !== -1)
        value = value.replace('?', '&');
    if (url.indexOf('?') === -1)
        url += '?' + name + '=' + value;
    else {
        //url += '&' + name + '=' + value;
        url = url.split('?')[0] + '?' + name + '=' + value + '&' + url.split('?')[1];
    }
    return url;
};
Utils.AddOrReplaceQueryString = function (url, key, newValue) {
    if (url.indexOf(key + "=") == -1)
        return Utils.setUrlVar(key, newValue, url);
    return url.replace(new RegExp(key + "=\\w+"), key + "=" + newValue);
};
Utils.getUrlSettingVar = function (name, value) {
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    var ekziston = false;
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        if (hash[0] == name) {
            ekziston = true;
            hashes[i] = hash[0] + '=' + value;
        }
    }
    if (!ekziston)
        hashes.push(name + '=' + value);
    return window.location.href.slice(0, window.location.href.indexOf('?') + 1) + hashes.join('&');
};

Utils.getServerUrl = function () {
    return window.location.href.slice(Utils.getServerUrlHost());
};

Utils.getWebMethodUrl = function (klasa, metoda) {
    //Duhet vendosur "/" ne fund ne menyre qe te jene te gjitha funksionet qe kthejne url ne nje forme. Ky "/" i hiqet ne before send te ajax
    return Utils.getServerUrlHost() + "/" + klasa + ".aspx/" + metoda + "/";
};

Utils.ShtoNeseNukGjendetDheSelektoCombo = function (combo, value, text) {
    var itemFound = combo.FindItemByValue(value);
    if (itemFound !== null) {
        combo.SetSelectedItem(itemFound);
        return;
    }
    combo.AddItem(text, value);
    itemFound = combo.FindItemByValue(value);
    combo.SetSelectedItem(itemFound);
}

/*
Selecton nga komboja e dhene newValue, nese nuk gjendet atehere shton nje item te ri me text: newText dhe value: newValue
*/
Utils.SelectComboItem = function (combo, newValue, newText, otherText) {
    var comboName = combo.name.split('_')[1];
    if (newValue !== null && newValue !== undefined && newValue !== 0) {
        var itemFound = combo.FindItemByValue(newValue);
        if (itemFound !== null) {
            combo.SetSelectedItem(itemFound);
            if (itemFound.text.indexOf(';') != -1)//rasti kur vendosen vlerat ne selektimin e nje rreshti             
                combo.SetText(itemFound.text.split(';')[0]);
            else if (!Utils.IsUndefined(newText) && ((typeof newText != "string" && !Utils.IsNullOrEmpty(newText)) || (typeof newText == "string" && !Utils.IsNullOrWhiteSpace(newText))) && !Utils.IsNullOrEmpty(otherText) && Utils.IsNullOrWhiteSpace(otherText))
                combo.SetText(newText);
            if (comboName == 'txtNr2')
                try {
                    nrLlogariChange();
                }
                catch (ee) {
                }
            return true;
        }
        if (newText === undefined || newText === null || newText === '') {
            switch (comboName) {
                case 'btneLlogInv': case 'btneLlogBle': case 'btneLlogShit': case 'btneLlogTretet': case 'btnLlogShpe': case 'cmbLlogAmortizimi': case 'txtNr2': case 'txtNrLlogari': case 'btnLlogPakesim': case 'btnLLogariKomisioni':
                case 'cmbLlogariKunderParti':
                    $.ajax({
                        pritPergjigje: true,
                        url: Utils.getServerApiUrl("Rregjistrime", "merrInfoComboLlogariByID"),
                        data: JSON.stringify({ idLlogari: newValue, kodKontrolli: comboName })
                    }).done(Utils.SuccededCallBackLlogari);
                    break;
                case 'btneSkema':
                    $.ajax({
                        pritPergjigje: true,
                        url: Utils.getServerApiUrl("Rregjistrime", "merrInfoComboSkemaArtikulliByID"),
                        data: JSON.stringify({ idSkema: newValue, kodKontrolli: comboName })
                    }).done(Utils.SuccededCallBackSkemaKontabilitetiArtikulli);
                    break;
                default:
                    combo.SetText(newValue);
            }
            return false;
        }
        var array;
        if (otherText != null || otherText || undefined) {
            array = [newText, otherText];
            var indeksi = combo.AddItem(array, newValue);
            var item = combo.GetItem(indeksi);
            combo.SelectedIndex = indeksi;
            combo.SetText(item.text);
            combo.SetValue(newValue);
            //combo.SetSelectedIndex(combo.AddItem(array, newValue));
            return true;
        }
        combo.SetSelectedIndex(combo.AddItem(newText, newValue));
        combo.SetText(newText);
        return true;
    }

    if (newText === null || newText === '' || newText === undefined) {
        combo.SetSelectedIndex(-1);
        return true;
    }
    var itemFoundByText = combo.FindItemByText(newText);
    if (itemFoundByText !== null) {
        combo.SetSelectedItem(itemFoundByText);
        return true;
    }
    switch (comboName) {
        case 'btneLlogInv': case 'btneLlogBle': case 'btneLlogShit': case 'btneLlogTretet': case 'btnLlogShpe': case 'cmbLlogAmortizimi': case 'btnLlogPakesim': case 'cmbLlogariKunderParti': case 'btnLLogariKomisioni':
            $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("Rregjistrime", "merrInfoComboLlogariByKod"),
                data: JSON.stringify({ kodLlogari: newText, kodKontrolli: comboName })
            }).done(Utils.SuccededCallBackLlogari);
            break;
        case 'btneSkema':
            $.ajax({
                pritPergjigje: true,
                url: Utils.getServerApiUrl("Rregjistrime", "merrInfoComboSkemaArtikulliByID"),
                data: JSON.stringify({ idSkema: newValue, kodKontrolli: comboName })
            }).done(Utils.SuccededCallBackSkemaKontabilitetiArtikulli);
            break;
        default:
    }
};

//mbush kombot e llogarive me elementin
Utils.SuccededCallBackLlogari = function (result) {

    if (result !== null) {
        var kontrolli = Utils.ktheKontroll(result.kodKontrolli);
        Utils.SelectComboItem(kontrolli, result.idLlogari, result.kodLlogari);
        try {
            nrLlogariChange();
        }
        catch (ee) {
            console.log(ee);
        } try {
            llogari_TextChanged();
        }
        catch (ee) {
            console.log(ee);
        }
    }
};

//mbush kombon e skemes me elementin e dhene
Utils.SuccededCallBackSkemaKontabilitetiArtikulli = function (result) {
    if (result !== null) {
        var kontrolli = Utils.ktheKontroll(result.kodKontrolli);
        Utils.SelectComboItem(kontrolli, result.IdSkemaKontabilitetiArtikulli, result.KodiSkemaKontabilitetiArtikulli);
    }
};

Utils.SucceededCallbackGrupimDokumentash = function (result) {
    Utils.mbushComboGrupDokMeVlereDefault(result[0], cmbGrup1);
    Utils.mbushComboGrupDokMeVlereDefault(result[1], cmbGrup2);
    Utils.mbushComboGrupDokMeVlereDefault(result[2], cmbGrup3);
};

Utils.mbushComboGrupDokMeVlereDefault = function (result, cmbGrup) {
    var gr, valueGr;
    if (cmbGrup.GetSelectedItem() != null)
        gr = cmbGrup.GetSelectedItem().value;
    else
        valueGr = cmbGrup.GetValue();
    cmbGrup.ClearItems();
    for (i = 0; i < result.length; i++)
        cmbGrup.AddItem([result[i].Kodi, result[i].Pershkrimi], result[i].IdGrupimKoka); //AddItem(teksti, vlera);
    if (gr)
        cmbGrup.SetValue(gr);
    if (valueGr)
        cmbGrup.SetSelectedItem(cmbGrup.FindItemByValue(valueGr));
    if (cmbGrup.GetSelectedItem() == null)
        cmbGrup.SetText('');
}

Utils.SetOrRefreshSplitterPaneContentUrl = function (pane, contentUrl) {
    var paneContent = window.parent.splitter.GetPaneByName(pane);
    paneContent.SetContentUrl(contentUrl);
};

Utils.SetFormatTextBoxDev = function (emerkontrolli, nrshifrapaspresjes) {
    ///<summary> metode per te formatuar fushat textbox te devexpresit sipas formatit te numrit</summary>
    ///<param name="emerkontrolli" type"string">emri i kontrolllit te devexpresit</param>
    ///<param name="nrshifrapaspresje" type="integer"> nr i shifrave mbas presjes</param>
    var format = "{0:0.";
    for (var i = 0; i < nrshifrapaspresjes; i++)
        format += "0";
    format += "}";
    Utils.ktheKontroll(emerkontrolli).displayFormat = format;
};

Utils.SetPeriudha = function (periudha) {
    sessionStorage.setItem('periudha', periudha);
};

var recheckPeriudhaTimer;
Utils.GetPeriudha = function () {
    var periudha = sessionStorage.getItem('periudha');
    if (periudha == null) {
        periudha = new Date();
        sessionStorage.setItem('periudha', periudha);
        return periudha;
    }
    return periudha;
};

Utils.lostFocusTxtNumer = function (s, e) {
    //console.log("LostFocus: " + s.name);
    s.SetText(s.oldGetText());
    //var value = s.oldGetText();
    //var formati = $(s.GetMainElement()).data('formatNumri');
    //console.log(s.name + " vlereReale "+value);
    //$(s.GetMainElement()).data('vlereReale', value);
    //if (typeof value != 'undefined' && value !== '' && !isNaN(value) && typeof formati != 'undefined') {
    //    value = parseFloat(value).toFixed(formati);
    //    value = Utils.FormatNumberBy3(value, ".", ",");
    //    s.oldSetText(value);
    //}
};

Utils.gotFocusTxtNumer = function (s, e) {

    var value = $(s).data('vlereReale');
    //console.log("GotFocus: " + s.name + " vlereReale: "+value);
    if (typeof value != 'undefined' && value !== '' && !isNaN(value)) {
        s.oldSetText(value);
    }
};

Utils.initTxtNumber = function (s, e) {
    var value = s.GetText();
    s.oldGetText = s.GetText;
    s.oldSetText = s.SetText;
    s.GetText = Utils.GetValueTextBox;
    s.SetText = Utils.SetValueTextBox;
    s.SetText(value);
};

Utils.GetValueTextBox = function () {
    var vlereReale = $(this).data('vlereReale');
    if (typeof vlereReale != 'undefined' && vlereReale !== '' && !isNaN(vlereReale))
        return vlereReale;
    return this.oldGetText();
};

Utils.SetValueTextBox = function (value) {
    var formati = Utils.getFormatNumri(this);
    if (typeof value == "string")
        value = value.replace(",", "");
    $(this).data('vlereReale', value);
    //console.log(this.name + " SET vlereReale " + value);
    if (!isNaN(value) && value !== '' && typeof formati != 'undefined') {
        if (!$(this).is(':focus')) {
            //console.log(this.name + " FOCUS " + value);
            value = parseFloat(value).toFixed(formati);
            value = Utils.FormatNumberBy3(value, ".", ",");
        }
    }
    this.oldSetText(value);
    $(this).data('beforeValue', this.GetValue());
};
Utils.setFormatNumri = function (s, formati) {
    $(s).data('formatNumri', formati);
};
Utils.getFormatNumri = function (s) {
    return $(s).data('formatNumri');
};

Utils.formatoTextBox = function (s) {
    var value = s.GetText();
    if (typeof value != 'undefined' && value !== '' && !isNaN(value))
        s.SetText(value);
};

Utils.unFormatoTextBox = function (s) {
    var value = s.GetText();
    if (typeof value != 'undefined' && value !== '' && !isNaN(value))
        s.oldSetText(value);
};

Utils.formatoShumeTextBox = function () {
    for (var i = 0, count = arguments.length; i < count; i++)
        Utils.formatoTextBox(arguments[i]);
};

Utils.unFormatoShumeTextBox = function () {
    for (var i = 0, count = arguments.length; i < count; i++)
        Utils.unFormatoTextBox(arguments[i]);
};



var winWidth = 0, winHeight = 0, resizeTimeout;
Utils.resizeSplitter = function () {
    var onResize = function () {
        splitter.SetHeight($(document).height());//$(document).height());//splitter.SetHeight(document.documentElement.clientHeight - size);
    }
    window.clearTimeout(resizeTimeout);
    resizeTimeout = window.setTimeout(function () { onResize() }, 300);
};

Utils.isGridResized = function () {
    var winNewWidth = $(window).width(),
        winNewHeight = $(window).height();
    // krahaso vlerat e vjetra me te rejat
    if (winWidth != winNewWidth || winHeight != winNewHeight) {
        winWidth = winNewWidth;
        winHeight = winNewHeight;
        return false;
    }
    return true;
};
Utils.FormatNumberBy3 = function (num, decpoint, sep) {
    // check for missing parameters and use defaults if so
    if (arguments.length == 2) {
        sep = ",";
    }
    if (arguments.length == 1) {
        sep = ",";
        decpoint = ".";
    }
    var minus = '';
    if (num < 0) {
        num = num.substring(1, num.length);
        minus = "-";
    }
    // need a string for operations
    num = num.toString();
    // separate the whole number and the fraction if possible
    a = num.split(decpoint);
    x = a[0]; // decimal
    y = a[1]; // fraction
    z = "";


    if (typeof (x) != "undefined") {
        // reverse the digits. regexp works from left to right.
        for (var i = x.length - 1; i >= 0; i--) {
            if (x.charAt(i) == sep)
                continue;
            z += x.charAt(i);
        }
        // add seperators. but undo the trailing one, if there
        z = z.replace(/(\d{3})/g, "$1" + sep);
        if (z.slice(-sep.length) == sep)
            z = z.slice(0, -sep.length);
        x = "";
        // reverse again to get back the number
        for (var i = z.length - 1; i >= 0; i--)
            x += z.charAt(i);
        // add the fraction back in, if it was there
        if (typeof (y) != "undefined" && y.length > 0)
            x += decpoint + y;
    }

    return minus + x;
};
//konverton nje object ne parametra querystring
Utils.KonvertoObjectQueryString = function (inputObject) {
    var str = '';

    for (var key in inputObject) {
        if (!inputObject.hasOwnProperty(key) || typeof inputObject[key] === 'function') { continue; }
        if (typeof inputObject[key] === 'object') {
            str += this.KonvertoObjectQueryString(inputObject[key]);
        } else {
            str += key + '=' + encodeURIComponent(inputObject[key]) + '&';
        }
    }

    return str;
};
Utils.KonvertoObjectNeArray = function (obj) {
    return $.extend(true, [], obj); //konverton ne array
};
Utils.ExpandCollapse = function (grida, hfState) {
    var collapsed = hfState.Get("collapsedGrida");
    if (collapsed == undefined || collapsed) {
        grida.ExpandAllDetailRows();
        hfState.Set("collapsedGrida", false);
    }
    else {
        grida.CollapseAllDetailRows();
        hfState.Set("collapsedGrida", true);
    }
};
Utils.JaneEkuivalentObjektet = function (a, b) {
    // Create arrays of property names
    var aProps = Object.getOwnPropertyNames(a);
    var bProps = Object.getOwnPropertyNames(b);

    // If number of properties is different,
    // objects are not equivalent
    if (aProps.length != bProps.length) {
        return false;
    }

    for (var i = 0; i < aProps.length; i++) {
        var propName = aProps[i];

        // If values of same property are not equal,
        // objects are not equivalent
        if (a[propName] != b[propName]) {
            return false;
        }
    }

    // If we made it this far, objects
    // are considered equivalent
    return true;
};

Utils.HiqOren = function (d) {
    d.setHours(0, 0, 0, 0);
    return d;
};

Utils.FormatoNumberMePresje = function (str, shifraPasPresjes) {
    if (shifraPasPresjes == undefined)
        shifraPasPresjes = 2;
    str = parseFloat(str).toFixed(shifraPasPresjes);

    return ("" + str).replace(/\B(?=(\d{3})+(?!\d))/g, ",");
};
Utils.HiqPresjet = function (str) {
    if (str != null && str != 'undefined') {
        return parseFloat(str.toString().replace(/,/g, ''));
    } return 0.00;
};
Utils.LlogaritGrupinPerKeteRresht == function (grida, rowIndex, kolona) {
    var firstGroupIndex = grida.GetTopVisibleIndex();
    var lastGroupIndex = grida.GetVisibleRowsOnPage() - 1;
    var groupIndex;
    for (var i = firstGroupIndex; i < lastGroupIndex; ++i) {
        if (grida.IsGroupRow(i)) {
            if (i < firstGroupIndex)
                continue;
            if (i < rowIndex)
                groupIndex = i;
            else {
                break;
            }
        }
    }
    this.LlogaritShumenEGrupin(grida, groupIndex, kolona);
};
///llogarit shumatoren e grupit
Utils.LlogaritShumenPerGrupin = function (grid, firstGroupIndex, shifraPasPresjes, kolona) {
    var lastGroupIndex = grid.GetVisibleRowsOnPage();
    var total = 0;

    for (var r = firstGroupIndex + 1; r < lastGroupIndex; r++) {
        if (grid.IsGroupRow(r))
            break;
        total += grid.batchEditApi.GetCellValue(r, kolona);
    }

    var lblGroupTotal = Utils.ktheKontroll("group_" + kolona + firstGroupIndex);
    // lblGroupTotal.SetValue(Utils.FormatoNumberMePresje(total));// Lori thote hiqe formatin
    lblGroupTotal.SetValue(parseFloat(total).toFixed(shifraPasPresjes));
};
///llogarit gjithe grouptotalet per kolonene e dhene 
Utils.RillogaritTotaletPerGrup = function (grid, shifraPasPresjes, kolona) {
    var firstGroupIndex = grid.GetTopVisibleIndex();
    var lastGroupIndex = grid.GetVisibleRowsOnPage();

    for (var i = firstGroupIndex; i < lastGroupIndex; i++) {
        if (grid.IsGroupRow(i) && grid.IsGroupRowExpanded(i))
            this.LlogaritShumenPerGrupin(grid, i, shifraPasPresjes, kolona);
    }
};

//Kthen false nqs gjatesia e kodbarit dhe formules nuk perkojne
//Perndryshe kthen nje objekt me sasine, gjeresine dhe gjatesine e llogaritur ne baze te formules
//Formula duhet te kete trajten 000000xxxyyyy ose 0000ssxxxyyyy
//ku x: nenkupton gjeresine qe do merret nga kodbari me pozicionet e x(fillim dhe fund) te formula
//y: nenkupton gjatesine qe do merret nga kodbari me pozicionet e y(fillim dhe fund) te formula
//s: nenkupton sasine qe do merret nga kodbari me pozicionet e s(fillim dhe fund) te formula
//nese ne formule nuk ka s ath sasia do te llogaritet gjeresi * gjatesi
Utils.llogaritSasiGjeresiGjatesiArtSipasFormulesKodbarit = function (kodbari, formuleKodbari, sasiPermase) {
    if (kodbari.length != formuleKodbari.length)
        return false;
    var gjeresi = kodbari.substring(formuleKodbari.indexOf('x'), formuleKodbari.lastIndexOf('x') + 1);
    var gjatesi = kodbari.substring(formuleKodbari.indexOf('y'), formuleKodbari.lastIndexOf('y') + 1);
    var sasia;
    if (formuleKodbari.indexOf('s') != -1)
        sasia = kodbari.substring(formuleKodbari.indexOf('s'), formuleKodbari.lastIndexOf('s') + 1);
    else
        sasia = sasiPermase * gjeresi * gjatesi;
    return {
        Sasia: sasia,
        Gjeresia: gjeresi,
        Gjatesia: gjatesi
    };
};

Utils.hapAccordionSipasLocalStorage = function (identifikuesLSAmbjenti) {
    if (localStorage) {
        var kokeKonfigurimiState = localStorage.getItem('kokeKonfigurimiState' + identifikuesLSAmbjenti);
        if ((!kokeKonfigurimiState || kokeKonfigurimiState == 'true') && !$('div > #kokeKonfigurimi').hasClass('ui-accordion-header-active')) {
            $('div > #kokeKonfigurimi').trigger('click');
        }

        var trupKonfigurimiState = localStorage.getItem('trupKonfigurimiState' + identifikuesLSAmbjenti);
        if ((!trupKonfigurimiState || trupKonfigurimiState == 'true') && !$('div > #trupKonfigurimi').hasClass('ui-accordion-header-active')) {
            $('div > #trupKonfigurimi').trigger('click');
        }
        var trupKonfigurimiKomisionState = localStorage.getItem('trupKonfigurimiKomisionState' + identifikuesLSAmbjenti);
        if ((!trupKonfigurimiKomisionState || trupKonfigurimiKomisionState == 'true') && !$('div > #trupKonfigurimiKomision').hasClass('ui-accordion-header-active')) {
            $('div > #trupKonfigurimiKomision').trigger('click');
        }

        var fundKonfigurimiState = localStorage.getItem('fundKonfigurimiState' + identifikuesLSAmbjenti);
        if ((!fundKonfigurimiState || fundKonfigurimiState == 'true') && !$('div > #fundKonfigurimi').hasClass('ui-accordion-header-active')) {
            $('div > #fundKonfigurimi').trigger('click');
        }

    }
    else {
        $('#accordition > div h3').trigger('click');
    }
};

Utils.documentKeyDown = function (e) {
    switch (e.which) {
        //case 8:  // Backspace
        //    e.preventDefault();
        //    break;
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
};
//funksione per stringjet

Utils.IsNullOrEmpty = function (str) {
    return str === undefined || str === null || 0 === str.length;
};

Utils.IsUndefined = function (str) {
    return str === undefined || str == "undefined" || typeof (str) == undefined || str == null;
};

Utils.IsNullOrWhiteSpace = function (str) {
    if (typeof str != "string")
        throw new Error(str + " nuk eshte e tipit string");
    return str === undefined || str === null || !str.trim();
};

//kthen objekt date nga objekti DateTime i kaluar ne input
Utils.zeroOren = function (dateOre) {
    if (typeof dateOre === "string")
        dateOre = new Date(dateOre);
    return new Date(dateOre.getFullYear(), dateOre.getMonth(), dateOre.getDate());
};
//merr ne input periudhen si objekt dhe kthen daten dhe oren default. ex nese se vihet tani vihet bashke me 
Utils.readCookie = function (name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
};
Utils.ktheDateServeriFromCookies = function () {
    var data = Utils.readCookie("dateServeri");
    if (!data) return null;

    return new Date(Utils.readCookie("dateServeri"));
};
Utils.ktheDateOreDefault = function (hfPeriudheObj) {
    var dateServeri = Utils.ktheDateServeriFromCookies();
    if (dateServeri >= new Date(hfPeriudheObj.FillimiPeriudha) && dateServeri <= new Date(hfPeriudheObj.MbarimiPeriudha))
        return dateServeri;
    else
        return new Date(hfPeriudheObj.FillimiPeriudha);
};
//merr ne input periudhen si objekt dhe kthen daten default
Utils.ktheDateDefault = function (hfPeriudheObj) {
    return Utils.zeroOren(Utils.ktheDateOreDefault(hfPeriudheObj));
};
Utils.BenPjeseNeNjeNgaKeto = function (fusha, listFushash) {

    for (var i = 0; i < listFushash.length; i++)
        if (fusha == listFushash[i])
            return true;
    return false;
};

Utils.MerrMesazhNgaGrida = function (grida) {
    var mesazhi = grida["cpMesazhNeGride"];
    if (typeof (mesazhi) == "undefined") {
        //default object,qe te mos jap exception nese andej nga ku mund te perdoret
        return {
            Status: false,
            Kodi: 1000,
            PershkrimMesazhi: "Nuk ka mesazh"
        };
    }
    //ekziston mesazh i marr nga grida
    var parsedMsg = JSON.parse(mesazhi);
    delete grida["cpMesazhNeGride"];
    return {
        Status: parsedMsg.Status,
        Kodi: parsedMsg.Kodi,
        PershkrimMesazhi: parsedMsg.PershkrimMesazhi
    };
};
Utils.konfiguroAccorditionNeDocReady = function (identifikuesLSAmbjenti) {
    $('#accordition > div h3').hide();

    $(document).on('click', '#accordition > div h3', function () {
        if ($(this).hasClass('ui-accordion-header-active')) {
            localStorage.setItem($(this).attr('id') + 'State' + identifikuesLSAmbjenti, 'true');
        }
        else {
            localStorage.setItem($(this).attr('id') + 'State' + identifikuesLSAmbjenti, 'false');
        }
        return false;
    });
};

Utils.konfiguroAccorditionPasKonfigDokumenti = function (identifikuesLSAmbjenti) {
    if (!$('#accordition > div').hasClass('ui-accordion')) {
        $('#accordition > div h3').show();
        $('#accordition > div').accordion({
            header: "h3",
            heightStyle: "content",
            collapsible: true,
            active: false
        });
    }
    Utils.hapAccordionSipasLocalStorage(identifikuesLSAmbjenti);
};

Utils.ktheDateInvariant = function (dateMeOre) {
    return Date.parseInvariant(dateMeOre);
};
//<summary>
//formatet e suportuar jane kombinimet dd/MM/yyyy .default 'dd/MM/yyyy'
//</summary>
Utils.CreateDateFromString = function (dataString, format) {
    if (format == undefined) format = 'dd/MM/yyyy';
    var dateArray = dataString.split('/');
    var formatArray = format.split('/');
    return new Date(dateArray[formatArray.indexOf('yyyy')], dateArray[formatArray.indexOf('MM')] - 1, dateArray[formatArray.indexOf('dd')]);
};
Utils.KtheDateOseBosh = function (dataString) {

    if (dataString == "") return "";
    var data = new Date(dataString);
    if (data.getFullYear() == 1 || data.getFullYear() == 1901) return "";
    return data;
};

Utils.ktheVleratESelektuaraTeBashkuara = function (tabela, indexi, ndaresja) {
    var kodi = "";
    if (tabela.length > 1) {
        for (i = 0; i < tabela.length - 1; i++)
            kodi += tabela[i][indexi] + ndaresja;

        kodi += tabela[tabela.length - 1][indexi];
    }
    else if (tabela.length == 1)
        kodi = tabela[0][indexi];

    return kodi;

};
Utils.RaiseCustomCallbackFiltrimi = function (grida) {
    var modifiedAutoFilter = grida.filterHelper.GetChangedAutoFilterValues();
    if (Object.keys(modifiedAutoFilter).length > 0)
        grida.filterHelper.ApplyMultiColumnAutoFilter();
    else
        grida.PerformCallback("APPLYCOLUMNFILTER");
};


Utils.findObjectByAttribute = function (items, attribute, searchValue) {
    for (var i = 0; i < items.length; i++) {
        if (items[i][attribute] === searchValue) {
            return items[i];
        }
    }
    return null;
};
Utils.findFieldValueByAttribute = function (items, attribute, field, searchValue) {
    var item = Utils.findObjectByAttribute(items, attribute, searchValue);
    return (item != null) ? item[field] : null;
};
Utils.ndertoPopup = function (options) {
    var defaults = { text: { mbyll: "Mbyll", ruaj: "Ruaj" } };
    options = jQuery.extend({}, defaults, options);
    var myPopup = $(options.prependSelector + " > ." + options.dialogClass);
    if (!myPopup.length) { //nuk e nderton prape nese ekziston        
        myPopup = $("<div class='modal " + options.dialogClass + "' role='dialog'><div class='modal-dialog'><div class='modal-content'><div class='modal-header'><button type='button' class='close' data-dismiss='modal' aria-hidden='true'>x</button><h4 class='modal-title'>" + options.titulli + "</h4></div><div class='modal-body'><div class='" + options.contentClass + "'></div></div><div class='modal-footer'><button type='button' class='btn btn-default' data-dismiss='modal'>" + options.text.mbyll + "</button>" + (options.saveClick ? "<button type='button' class='btn btn-primary'>" + options.text.ruaj + "</button>" : " ") + "</div></div></div></div>");
        $(options.prependSelector).prepend(myPopup);
        if (options.saveClick)
            $("." + options.dialogClass + " .btn-primary").on("click", options.saveClick);
    }
    return myPopup;
};
Utils.JopopupClick = function (hfUrl) {
    if (hfUrl != undefined && hfUrl.val() != '')
        hfUrl.val('');
};

Utils.hapPopUp = function (titull, hfUrl) {
    if (hfUrl != undefined && hfUrl.val() != '') {
        myButtonClickLupa.LupaUniversal_Click(titull, hfUrl.val(), 900, 600);
        hfUrl.val('');
    }
};



Utils.ktheElement = function (myArray, identifikuesi, vlera) {
    var result = $.grep(myArray, function (e) {
        return e[identifikuesi] === vlera;
    });
    if (result.length == 1)
        return result[0];
    return false;
};

Utils.redirect = function (url) {
    var mainWindow;
    if (window.parent)
        mainWindow = window.parent;
    else
        mainWindow = window;
    mainWindow.location.href = url;
};

Utils.RedirectedUrl = function (response) {


    if (response.status == 401) {
        if (response.statusText == "ScopeExpired")
            return location.origin + "/" + (location.origin.indexOf("E-PaySlip") == -1 ? Paths.defaultLoginNdermarrje : Paths.epaySlipLoginPath);
        else
            return location.origin + "/" + (location.origin.indexOf("E-PaySlip") == -1 ? Paths.defaultLoginPath : Paths.epaySlipLoginPath);
    }
    //rasti kur ka skaduar sesioni dhe kthen si pergjigje faqen e loginit
    if (response.responseText.indexOf("identifikuesLogin") != -1 && response.responseText.indexOf("LoginPage") != -1)
        return location.origin + "/" + Paths.defaultLoginPath + "?arsye=MbarimSessioni";

    //rasti kur skadimi i sesionit sjell nje exception dhe vjen url-ja e faqes se loginit ne response header
    var headers = response.getAllResponseHeaders();
    var locationIndex = headers.indexOf("Location: ");

    if (locationIndex != -1) {
        var newLocation = headers.substring(locationIndex + "Location: ".length, headers.indexOf('\n', locationIndex));
        return newLocation;
    }

    return undefined;
};

Utils.Contains = function (array, value) {
    if (!array || !array.length)
        return false;
    for (var i = 0; i < array.length; i++) {
        if (array[i] === value)
            return true;
    }

    return false;

};

Utils.MbushCombo = function (combo, dataArray, valueColumn, textColumn) {
    if (combo == undefined)
        return;
    combo.BeginUpdate();
    combo.ClearItems();
    for (i = 0; i < dataArray.length; i++)
        combo.AddItem(dataArray[i][textColumn], dataArray[i][valueColumn]);
    combo.EndUpdate();
};
Utils.krijoSlider = function (listaUrl) {
    var arkiveSlider = $('<div id="arkiveCarousel" class="carousel slide" data-interval="0"></div>');
    var sliderIndikues = $('<ol class="carousel-indicators"></ol>');
    var sliderBrenda = $('<div class="carousel-inner" role="listbox"></div>');
    $.each(listaUrl, function (i, item) {
        sliderIndikues.append($('<li data-target="#arkiveCarousel" data-slide-to="' + i + '" ' + (i == 0 ? 'class="active"' : '') + ' ></li>'));
        sliderBrenda.append('<div class="item ' + (i == 0 ? 'active' : '') + '"><img src="' + item["Path"].replace("~/", "").replace("~\\", "") + '" alt="' + item["FileName"] + '"></div>');
    });
    arkiveSlider.append(sliderIndikues);
    arkiveSlider.append(sliderBrenda);
    var butoniMajtas = $('<a class="left carousel-control" href="#arkiveCarousel" role="button" data-slide="prev" ><span class="glyphicon glyphicon-chevron-left" aria-hidden="true"></span><span class="sr-only">Previous</span></a>');
    var butoniDjathtas = $('<a class="right carousel-control" href="#arkiveCarousel" role="button"  data-slide="next"><span class="glyphicon glyphicon-chevron-right" aria-hidden="true"></span><span class="sr-only">Next</span></a>');
    arkiveSlider.append(butoniMajtas);
    arkiveSlider.append(butoniDjathtas);
    return arkiveSlider;
};
Utils.hapLupeImazhe = function (listaUrl, popUpOptions) {
    var defaults = { prependSelector: "#bootPopUp", dialogClass: "dialog-imazhe-default", contentClass: "content-imazhe-default" }
    options = $.extend({}, defaults, popUpOptions);
    if (listaUrl.length == 0) {
        console.log("Error i paparashikuar lista e urleve duhet te ishte plot");
        return;
    }
    $("." + options.contentClass).html("");
    var myPopup = Utils.ndertoPopup(options);
    $("." + options.contentClass).prepend(Utils.krijoSlider(listaUrl));
    myPopup.modal("show");
};
Utils.hapLupe = function (options) {
    options.emerPopUpi.SetHeaderText(options.titull);
    options.emerPopUpi.SetSize(options.width, options.height);
    var myParamsArray = [];
    $.each(options.params, function (key, value) {
        myParamsArray.push(key + "=" + value);
    });
    var myUrl = options.baseUrl + (myParamsArray.length ? "?" : "") + myParamsArray.join("&");
    options.emerPopUpi.SetContentUrl(myUrl);
    options.emerPopUpi.Show();
};
Utils.ktheKontroll = function (idkontrolli) {
    if (typeof idkontrolli === 'object') return idkontrolli;
    if (typeof idkontrolli == "undefined") {
        console.log("Kontrolli: " + idkontrolli + " nuk ekziston!");
        return {}

    }
    var k = window[idkontrolli];
    if (typeof k == 'undefined') {
        console.log("Kontrolli: " + idkontrolli + " nuk ekziston!");
        return {};
    }
    return k;

};

Utils.kaRreshtaTeSelektuarGrida = function (grida) {
    return (grida.GetSelectedRowCount() > 0);
};

Utils.PermbanKaraktereSpeciale = function (pershkrim) {
    if (pershkrim.indexOf("<") != -1) return true;
    if (pershkrim.indexOf('>') != -1) return true;

    return false;
};

Utils.findArrayIndexByAttrValue = function (array, attr, value) {
    //kthen index te nje Array me Objects nese attribute me value te dhene ekziston ne array-in e dhene, ne te kunder kthen -1
    var indeksiKerkuar = -1;
    for (var i = 0; i < array.length; i += 1) {
        if (array[i][attr] == value) {
            indeksiKerkuar = i;
            break;
        }
    }
    return indeksiKerkuar;
};
Utils.formatoPresje = function (numer, shifraPasPresjes) {
    numer = numer.toFixed(shifraPasPresjes);
    var indexi = numer.indexOf(".");
    var numri, presje;
    if (indexi == -1 || indexi == 0) {
        var num = 0;
        numri = numer.replace(/(\d)(?=(\d\d\d)+(?!\d))/g, function ($1) { return $1 + "," });
        presje = num.toFixed(shifraPasPresjes).substring(1);
        return numri + presje;
    }
    else {
        var nr = numer.substring(0, indexi);
        numri = ("" + nr).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, function ($1) { return $1 + "," });
        presje = numer.substring(indexi);
        return numri + presje;
    }

};

Utils.findInArray = function (array, callback) {
    //nese browseri suporton funksionin nativ find
    if (array.find)
        return array.find(callback);
    for (var i = 0; i < array.length; i++) {
        if (callback(array[i]))
            return array[i];
    }

    return undefined;
};
Utils.findIndexInArray = function (array, callback) {
    for (var i = 0; i < array.length; i++) {
        if (callback(array[i]))
            return i;
    }

    return -1;
}

Utils.ComboBoxFindValueByText = function (combo, text) {
    if (combo == undefined || Utils.IsNullOrEmpty(text)) return -1;

    var item = combo.FindItemByText(text);
    if (item == undefined)
        return -1;

    return item.value;
};

Utils.shfaqLoadingGif = function (selector) {
    if (selector != undefined) {
        if (!$(selector).block)
            return;
        $(selector).block({
            message: loadingGifDiv,
            css: {
                border: "none",
                backgroundColor: "transparent"
            },
            ignoreIfBlocked: true
        });
    } else {
        if (!$.blockUI)
            return;
        $.blockUI({
            message: loadingGifDiv,
            css: {
                border: 'none',
                backgroundColor: 'transparent'
            },
            ignoreIfBlocked: true
        });
    }
};

Utils.hiqLoadingGif = function (selector) {
    if (selector != undefined) {
        if (!$(selector).unblock)
            return;
        $(selector).unblock();
    } else {
        if (!$.unblockUI)
            return;
        $.unblockUI();
    }
};

Utils.bllokoFaqe = function () {
    if (window.parent.parent)
        window.parent.parent.Utils.blloko();
    if (window.parent)
        window.parent.Utils.blloko();
    else
        Utils.blloko();
};

Utils.blloko = function () {
    if (!$.blockUI)
        return;
    $.blockUI({
        message: null,
        css: {
            border: 'none',
            backgroundColor: 'transparent'
        },
        ignoreIfBlocked: true
    });
}

Utils.zhbllokoFaqe = function () {
    if (window.parent.parent)
        window.parent.parent.Utils.zhblloko();
    if (window.parent)
        window.parent.Utils.zhblloko();
    else
        Utils.zhblloko();
};

Utils.zhblloko = function () {
    if (!$.unblockUI)
        return;
    $.unblockUI();
}

Utils.vendosVlereNeHiddenField = function (hiddenFieldId, value) {
    var hf = $("#" + hiddenFieldId);
    if (hf.length > 0)
        hf[0].value = value;
};
Utils.Contains = function (array, value) {
    if (!array || !array.length)
        return false;
    for (var i = 0; i < array.length; i++) {
        if (array[i] === value)
            return true;
    }

    return false;

};
Utils.redirectKlono = function (idDok, shitjeBlerje, emerFaqe, mesazhNukKeniAnsjeDokTeZgjedhur) {
    if (idDok === null)
        myMesazh.ShtoMesazhGabimi(mesazhNukKeniAnsjeDokTeZgjedhur);
    else
        myFaqeCelje.kontrolloTeDrejta(Utils.buildUrl(emerFaqe, { 'shitje_blerje': shitjeBlerje, 'id': idDok, 'shtim_modifikim': 'klonim' }));
};

Utils.konfirmoFshirje = function (popFshi, labelBox, numRreshta, mesazhi, mesazhgabimi) {
    if (numRreshta == 0) {
        myMesazh.ShtoMesazhGabimi(mesazhgabimi);
        return;
    }
    labelBox.SetText(mesazhi.replace("#X", numRreshta));
    popFshi.Show();
};
Utils.getGlobalization = function (idGjuha) {
    if (idGjuha == 1) {
        return messages = {
            typeError: "{file} has an invalid extension. Valid extension(s): {extensions}.",
            sizeError: "{file} is too large, maximum file size is {sizeLimit}.",
            minSizeError: "{file} is too small, minimum file size is {minSizeLimit}.",
            emptyError: "{file} is empty, please select files again without it.",
            noFilesError: "No files to upload.",
            tooManyItemsError: "Too many items ({netItems}) would be uploaded.  Item limit is {itemLimit}.",
            maxHeightImageError: "Image is too tall.",
            maxWidthImageError: "Image is too wide.",
            minHeightImageError: "Image is not tall enough.",
            minWidthImageError: "Image is not wide enough.",
            retryFailTooManyItems: "Retry failed - you have reached your file limit.",
            onLeave: "The files are being uploaded, if you leave now the upload will be canceled.",
            unsupportedBrowserIos8Safari: "Unrecoverable error - this browser does not permit file uploading of any kind due to serious bugs in iOS8 Safari.  Please use iOS8 Chrome until Apple fixes these issues."
        };
    }
    else
        return mesagges = {
            typeError: "Formati i skedarit {file} nuk eshte i sakte . Formatet e vlefshme jane :{extensions}",
            sizeError: "Madhesia e {file} eshte shume e madhe, madhesia maksimale eshte {sizeLimit}.",
            minSizeError: "Madhesia e {file} eshte shume e vogel, madhesia minimum eshte {minSizeLimit}.",
            emptyError: "{file} eshte bosh ,  ju lutemi zgjidhni skedaret perseri pa kete te fundit.",
            noFilesError: "Ska skedar per tu ngarkuar.",
            tooManyItemsError: "Shume skedare  ({netItems}) po ngarkohen .  Limiti i tyre eshte  {itemLimit}.",
            maxHeightImageError: "Lartesia e imazhit eshte shume e madhe .",
            maxWidthImageError: "Gjeresia e imazhit eshte shume e madhe .",
            minHeightImageError: "Imazhi nuk eshte mjaftueshem i larte .",
            minWidthImageError: "Imazhi nuk eshte mjaftueshem i gjere .",
            retryFailTooManyItems: "Ju keni arritur limitin e provave per vendosjen e skedarit.",
            onLeave: "Skedaret po ngarkohen , nqs nderprisni ngarkimi do te deshtoje.",
            unsupportedBrowserIos8Safari: "Error i i parikuperueshem - ky browser nuk lejon ngarkimin e asnjelloj skedari per shkak bugs te iOS8 Safari.  Ju lutemi perdorni  iOS8 Chrome derisa Apple te rregulloje keto probleme."
        };
};

//Kontrollet nuk kane me nje client instance name ndaj kapim id-ne e checkboxit, te cilen e kemi nga eventi, dhe nga ajo id, gjejme id-ne e grides
Utils.merrSubGrideSipasEmrit = function (s, emerKontrolli, emerGride) {
    return window[s.name.replace(emerKontrolli, emerGride)];
};
Utils.ShtoNeseNukEkziston = function (array, element, funksionKrahasues) {
    if (!Utils.findInArray(array, funksionKrahasues))
        array.push(element);
};

Utils.KategoriArkive = { shitje: 1, blerje: 2, arketim: 3, pagese: 3, terheqje: 4, derdhje: 4, artikull: 13, kf: 12, grupArtikull: 67, magazina: 6, njesiadmin: 23, seriale: 116, gis: 142 };

//nese selektohet rreshti bosh ne dropdown
Utils.SelektimiBosh = function (s, e) {
    if (s.GetValue() == null || s.GetValue() == "0" || s.GetValue() == 0)
        s.SetSelectedIndex(-1);
};

Utils.CloneObject = function (object) {
    return JSON.parse(JSON.stringify(object));
};

Utils.EksportoDokumentNgaWebService = function (wsUrl, params) {
    var xhr = new XMLHttpRequest();
    xhr.open('POST', wsUrl, true);
    xhr.responseType = 'arraybuffer';
    xhr.onload = function () {
        if (this.status === 200) {
            var filename = "";
            var disposition = xhr.getResponseHeader('Content-Disposition');
            if (disposition && disposition.indexOf('attachment') !== -1) {
                var filenameRegex = /filename[^;=\n]*=((['"]).*?\2|[^;\n]*)/;
                var matches = filenameRegex.exec(disposition);
                if (matches != null && matches[1]) filename = matches[1].replace(/['"]/g, '');
            }
            var type = xhr.getResponseHeader('Content-Type');

            var blob = new Blob([this.response], { type: type });
            if (typeof window.navigator.msSaveBlob !== 'undefined') {
                // IE workaround for "HTML7007: One or more blob URLs were revoked by closing the blob for which they were created. These URLs will no longer resolve as the data backing the URL has been freed."
                window.navigator.msSaveBlob(blob, filename);
            } else {
                var URL = window.URL;// || window.webkitURL;
                var downloadUrl = URL.createObjectURL(blob);

                if (filename) {
                    // use HTML5 a[download] attribute to specify filename
                    var a = document.createElement("a");
                    // safari doesn't support this yet
                    if (typeof a.download === 'undefined') {
                        window.open(downloadUrl);
                    } else {
                        a.href = downloadUrl;
                        a.download = filename;
                        document.body.appendChild(a);
                        a.click();
                    }
                } else {
                    window.open(downloadUrl);
                }

                setTimeout(function () { URL.revokeObjectURL(downloadUrl); }, 100); // cleanup
            }
        }
    };
    xhr.setRequestHeader('Content-type', 'application/x-www-form-urlencoded');
    xhr.send($.param(params));
};

if (!Array.prototype.findIndex) {
    Object.defineProperty(Array.prototype, 'findIndex', {
        value: function (predicate) {
            // 1. Let O be ? ToObject(this value).
            if (this == null) {
                throw new TypeError('"this" is null or not defined');
            }

            var o = Object(this);

            // 2. Let len be ? ToLength(? Get(O, "length")).
            var len = o.length >>> 0;

            // 3. If IsCallable(predicate) is false, throw a TypeError exception.
            if (typeof predicate !== 'function') {
                throw new TypeError('predicate must be a function');
            }

            // 4. If thisArg was supplied, let T be thisArg; else let T be undefined.
            var thisArg = arguments[1];

            // 5. Let k be 0.
            var k = 0;

            // 6. Repeat, while k < len
            while (k < len) {
                // a. Let Pk be ! ToString(k).
                // b. Let kValue be ? Get(O, Pk).
                // c. Let testResult be ToBoolean(? Call(predicate, T, « kValue, k, O »)).
                // d. If testResult is true, return k.
                var kValue = o[k];
                if (predicate.call(thisArg, kValue, k, o)) {
                    return k;
                }
                // e. Increase k by 1.
                k++;
            }

            // 7. Return -1.
            return -1;
        }
    });
};

Utils.konverto = function (idshitje, idmag, idrez, SuccededCallbackKontrolloEkzistojneDokQePoKonvertohen) {
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "kontrolloEkzistojneDokQePoKonvertohenSipasIdkoka"),
        data: JSON.stringify({ idshitje: idshitje, idmag: idmag, idrez: idrez })
    }).done(
        function (result) {
            if (!result) {
                myMesazh.ShtoMesazhGabimi("Dokumenti qe doni te konvertoni mund te jete modifikuar nga nje perdorues tjeter! Ju lutemi, rifreskoni listen dhe konvertojeni perseri ose rihapeni!");
                return;
            }
            SuccededCallbackKontrolloEkzistojneDokQePoKonvertohen();
        });
};

Utils.kontrolloGridSipasKeyValue = function (idDokumenti, keyFieldName, fields, SuccededCallbackkontrolloGridSipasKeyValue) {
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "gjejRowSipasIdFaturaNgaDSGrides"),
        data: JSON.stringify({ pageId: window["CurrentPageId"], idDokumenti: idDokumenti, komponente: hfState.Get('komponente'), idNdermarrje: hfState.Get('idNdermarrje'), idKatDokShitje: hfState.Get('idKatDokShitje'), keyFieldName: keyFieldName, fields: fields })
    }).done(
        function (result) {
            SuccededCallbackkontrolloGridSipasKeyValue(result);
        });
};

Utils.PushToGoogleAnalytics = function (googleAnalytics, googleAnalyticsTrackingId) {
    if (googleAnalytics && googleAnalyticsTrackingId !== '') {
        var googleAnalyticsScript = document.createElement('script');
        googleAnalyticsScript.setAttribute('src', 'https://www.googletagmanager.com/gtag/js?id=' + googleAnalyticsTrackingId);
        googleAnalyticsScript.setAttribute('async', true);
        document.head.appendChild(googleAnalyticsScript);
            
        window.dataLayer = window.dataLayer || [];
        function gtag() { dataLayer.push(arguments); }
        gtag('js', new Date());
        gtag('config', googleAnalyticsTrackingId);
    }    
};
