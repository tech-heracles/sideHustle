;
if (typeof myFaqeCelje == 'undefined') {
    myFaqeCelje = {};
}

myFaqeCelje.krijoTable = function (rresht, kolone, emertabele) {
    var rr = rresht - 1;
    if (rresht <= $("#" + emertabele).children('tbody').children('tr').size()) {
        if (kolone > $("#" + emertabele + ">tbody>tr:eq(" + rr + ")").children('td').size())
            for (i = $("#" + emertabele + ">tbody>tr:eq(" + rr + ")").children('td').size(); i < kolone; i++)
                $('<td>' + '</td>').appendTo($("#" + emertabele + ">tbody>tr:eq(" + rr + ")"));
    }
    else for (j = $("#" + emertabele).children('tbody').children('tr').size(); j < rresht; j++) {
        $("#" + emertabele).children('tbody').append('<tr>' + '</tr>');
        for (i = 0; i < kolone; i++)
            $('<td>' + '</td>').appendTo($("#" + emertabele + ">tbody>tr:eq(" + j + ")"));
    }
};

myFaqeCelje.krijoTableMeNrKol = function (rresht, kolone, emertabele) {
    var rr = rresht - 1;
    if (rresht <= $("#" + emertabele).children('tbody').children('tr').size()) {
        if (kolone > $("#" + emertabele + ">tbody>tr:eq(" + rr + ")").children('td').size())
            for (i = $("#" + emertabele + ">tbody>tr:eq(" + rr + ")").children('td').size(); i < kolone; i++)
                $('<td>' + '</td>').appendTo($("#" + emertabele + ">tbody>tr:eq(" + rr + ")"));
    }
    else for (j = $("#" + emertabele).children('tbody').children('tr').size(); j < rresht; j++) {
        $("#" + emertabele).children('tbody').append('<tr>' + '</tr>');
        for (i = 0; i < kolone; i++)
            $('<td>' + '</td>').appendTo($("#" + emertabele + ">tbody>tr:eq(" + j + ")"));
    }
};

myFaqeCelje.valido = function (s, e, PageControl, hfTeDrejta, hfShtimModifikim, validopun, mosNdryshoTab) {
    var activeTabIndex = PageControl.GetActiveTab().index;
    var tabPageCount = PageControl.GetTabCount();
    var isValid = true;
    for (var i = 1; i < tabPageCount; i++) {
        if(!mosNdryshoTab) PageControl.SetActiveTab(PageControl.GetTab(i));
        isValid = ASPxClientEdit.ValidateGroup("entries");
        if (isValid == false) {
            e.processOnServer = false;
            PageControl.SetActiveTab(PageControl.GetTab(i));
            Utils.hiqLoadingGif();
            myMenu.PercaktoMenuSipasTabit(i, hfTeDrejta, hfShtimModifikim);
            break;
        }

        else if (validopun != undefined) {
            isvalid = ASPxClientEdit.ValidateGroup("entriesPun");
            if (isvalid == false) {
                e.processOnServer = false;
                PageControl.SetActiveTab(PageControl.GetTab(i));
                Utils.hiqLoadingGif();
                myMenu.PercaktoMenuSipasTabit(i, hfTeDrejta, hfShtimModifikim);
                break;
            } else PageControl.SetActiveTab(PageControl.GetTab(activeTabIndex));
        } else PageControl.SetActiveTab(PageControl.GetTab(activeTabIndex));

    }
    return isValid;
};
myFaqeCelje.validoKontrolleDheGriden = function (s, e, PageControl, grida, hfTeDrejta, hfShtimModifikim) {
    var activeTabIndex = PageControl.GetActiveTab().index;
    var tabPageCount = PageControl.GetTabCount();
    var isValid = true;
    for (var i = 1; i < tabPageCount; i++) {
        PageControl.SetActiveTab(PageControl.GetTab(i));
        isValid = ASPxClientEdit.ValidateGroup("entries") && grida.batchEditApi.ValidateRows();
        if (isValid == false) {
            e.processOnServer = false;
            PageControl.SetActiveTab(PageControl.GetTab(i));
            Utils.hiqLoadingGif();
            myMenu.PercaktoMenuSipasTabit(i, hfTeDrejta, hfShtimModifikim);
            break;
        }
        else
            PageControl.SetActiveTab(PageControl.GetTab(activeTabIndex));
    }
    return isValid;
};
myFaqeCelje.validoPun = function (s, e, PageControl, hfTeDrejta, hfShtimModifikim) {
    var activeTabIndex = PageControl.GetActiveTab().index;
    var tabPageCount = PageControl.GetTabCount();

    for (var i = 1; i < tabPageCount; i++) {
        PageControl.SetActiveTab(PageControl.GetTab(i));
        isvalid = ASPxClientEdit.ValidateGroup("entriesPun");
        if (isvalid == false) {
            e.processOnServer = false;
            PageControl.SetActiveTab(PageControl.GetTab(i));
            Utils.hiqLoadingGif();
            myMenu.PercaktoMenuSipasTabit(i, hfTeDrejta, hfShtimModifikim);
            break;
        }
        else
            PageControl.SetActiveTab(PageControl.GetTab(activeTabIndex));
    }
};

/*
Function: EndRequestHandler

Thirret nese ruajtja eshte bere ne rregull. Pastron koken e dokumentit dhe array-t e perdorura.
Shiko funksionet  <ndryshoKonfigurimin>.
*/
myFaqeCelje.EndRequestHandler = function (sender, args, hfStatus, hfKontrollet, hfShtimModifikim, hfId, indexModifiko, PageControl, emergride, nrKomponente, hfTeDrejta) {
    if (hfStatus.val() == "true") {
        if (hfShtimModifikim.val() != "modifikim") {
            aktivizoFusha(hfKontrollet.value);
            //        aktivFusha(hfKontrollet.value);
            window.mbush = false;
            hfShtimModifikim.val("shtim"); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
            hfId.val(0); //hidden fieldi qe ruan id  e rreshtit te selektuar
            indexModifiko = -1; //indexi i reshtit te selektuar
            pastrofusha();
            hfStatus.val("false");

            if (PageControl.GetActiveTabIndex() != 0) {
                PageControl.SetActiveTabIndex(1);
                myMenu.PercaktoMenuSipasTabit(1, hfTeDrejta, hfShtimModifikim);
            }
            SucceededCallbackKonfigurimiInit(resultkonf);
        }
        else {
            hfStatus.val("false");
            if (PageControl.GetActiveTabIndex() != 0) {
                PageControl.SetActiveTabIndex(0);
                myMenu.PercaktoMenuSipasTabit(0, hfTeDrejta, hfShtimModifikim);
            }
        }

        emergride.PerformCallback(nrKomponente + ";" + cmbKonfigurimi.GetText());
        emergride.ClearFilter();
    }
    else myMenu.PercaktoMenuSipasTabit(PageControl.GetActiveTabIndex(), hfTeDrejta, hfShtimModifikim);
    Utils.hiqLoadingGif();
    return indexModifiko;
};

myFaqeCelje.EndRequestHandlerNew = function (sender, args, hfStatus, colKontrollet, colAtrTrupi, hfShtimModifikim, hfId, indexModifiko, PageControl, emergride, nrKomponente, pastrofusha, hfTeDrejta, vendosKonfig, resultkonfig, clearFilter) {
    if (hfStatus.val() == "true") {
        if (hfShtimModifikim.val() != "modifikim" && hfShtimModifikim.val() != "modifikimPrindi") {
            window.mbush = false;
            if (hfShtimModifikim.val() != "shtimPrindi")
                hfShtimModifikim.val("shtim"); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim; kur eshte shtimPrindi le te qendroje shtimPrindi

            hfId.val(0); //hidden fieldi qe ruan id  e rreshtit te selektuar
            indexModifiko = -1; //indexi i reshtit te selektuar

            if (pastrofusha) pastrofusha();

            hfStatus.val("false");

            if (PageControl && PageControl.GetActiveTabIndex() != 0) {
                PageControl.SetActiveTabIndex(1);
                myMenu.PercaktoMenuSipasTabit(1, hfTeDrejta, hfShtimModifikim);
            }
            if (vendosKonfig === undefined)
                SucceededCallbackKonfig(resultkonf);
            else
                vendosKonfig(resultkonfig);
        }
        else {
            hfStatus.val("false");

            if (PageControl && PageControl.GetActiveTabIndex() != 0) {
                PageControl.SetActiveTabIndex(0); myMenu.PercaktoMenuSipasTabit(0, hfTeDrejta, hfShtimModifikim);
            }
        }
        if (emergride) {
            emergride.PerformCallback(nrKomponente + ";" + cmbKonfigurimi.GetText());
            if (clearFilter || clearFilter === undefined) {
                emergride.ClearFilter();
            }
        }
    }
    else {
        var tabIndex = PageControl ? PageControl.GetActiveTabIndex() : 0;
        myMenu.PercaktoMenuSipasTabit(tabIndex, hfTeDrejta, hfShtimModifikim);
    }
    Utils.hiqLoadingGif();
    return indexModifiko;
};

myFaqeCelje.EndRequestHandlerPas = function (sender, args, hfStatus, hfShtimModifikim, hfId, indexModifiko, PageControl, emergride, nrKomponente, hfTeDrejta) {
    if (hfStatus.val() === "true") {
        if (hfShtimModifikim.val() != "modifikim") {
            window.mbush = false;
            hfShtimModifikim.val("shtim"); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
            hfId.val(0); //hidden fieldi qe ruan id  e rreshtit te selektuar
            indexModifiko = -1; //indexi i reshtit te selektuar
            pastrofusha();
            hfStatus.val("false");

            if (PageControl.GetActiveTabIndex() !== 0) {
                PageControl.SetActiveTabIndex(1);
                myMenu.PercaktoMenuSipasTabit(1, hfTeDrejta, hfShtimModifikim);
            }
        }
        else {
            hfStatus.val("false");

            if (PageControl.GetActiveTabIndex() != 0) {
                PageControl.SetActiveTabIndex(0);
                myMenu.PercaktoMenuSipasTabit(0, hfTeDrejta, hfShtimModifikim);
            }
        }
        //mos e hiq, sepse nuk shtohet elementi ne gride
        emergride.PerformCallback();
        //emergride.ClearFilter();
    }
    else myMenu.PercaktoMenuSipasTabit(PageControl.GetActiveTabIndex(), hfTeDrejta, hfShtimModifikim);
    Utils.hiqLoadingGif();
    return indexModifiko;
};

myFaqeCelje.shtoHandlerSession = function () {
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(myFaqeCelje.EndRequestSession);
};
myFaqeCelje.EndRequestSession = function (sender, args) {
    try {
        window.parent.SessionTimeout.sendKeepAlive();
    }
    catch (err) {
        try {
            window.parent.window.parent.SessionTimeout.sendKeepAlive();
        }
        catch (er) { }
    }
};

myFaqeCelje.changeName = function (emerfaqe, id, hfKonffillestar) {
    if (window.parent && window.parent.callWebServiceKtheInfoLart) {
        window.parent.callWebServiceKtheInfoLart(emerfaqe, id);
        window.parent.createCookie('adresa', window.location.href, 1);
    }

    myFaqeCelje.shtoHandlerSession();

    if (hfKonffillestar !== undefined && hfKonffillestar !== null) {
        lblKonfigurimi.SetText(hfKonffillestar.value.split(';')[1]);
        cmbKonfigurimi.SetText(hfKonffillestar.value.split(';')[0]);
        // cmbKonfigurimi.SetText(hfKonffillestar.value);
        ndryshoKonfiguriminInit();
    }
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    if (window['EndRequestHandler'])
        prm.add_endRequest(EndRequestHandler);
    prm.add_endRequest(myMesazh.EndRequestTimer);
};
myFaqeCelje.changeNameRegjistrime = function (emerfaqe, id) {
    window.parent.callWebServiceKtheInfoLart(emerfaqe, id);
    myFaqeCelje.shtoHandlerSession();
    window.parent.createCookie('adresa', emerfaqe, 1);
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(myMesazh.EndRequestTimer);
};
var timeout;
//myFaqeCelje.EndRequestTimer = function (sender, args) {
//    if (mesazhPage.GetText() != "" && mesazhPage.GetText().split('?').length == 1) {
//        clearTimeout(timeout);
//        timeout = setTimeout(function () {
//            mesazhPage.SetText('');
//        }, 10000);
//    }
//    //    var mesazh = mesazhPage.GetText();
//    //    timer.DoTick();
//    //    timer.Stop();
//    //
//    //    timer.SetEnabled(false);
//    //    mesazhPage.SetText(mesazh);
//    //   { timer.SetInterval(10000); timer.SetEnabled(true); }
//    //    else timer.SetEnabled(false);
//    Utils.hiqLoadingGif();;
//}
//ben aktive ose jo fushat nqs eshte i lidhur ose aktivizon fushat e bera inaktive kur behet shtim
myFaqeCelje.aktivizoFusha = function (vlerat, hfMod, hfLidhur, emerContaineri) {
    var kontrollet = vlerat.split(';');
    for (var i = 0; i < kontrollet.length - 1; i++) {
        if (kontrollet[i].split(',')[1].toString() == '5')  //tipi gride
            continue;


        idkontrolli = kontrollet[i].split(',')[0];
        div = $('#dv' + idkontrolli)[0];
        kontrolli = $(emerContaineri + idkontrolli)[0];
        k = Utils.ktheKontroll(idkontrolli);
        if (!k)
            continue;
        var enabled = false;
        if (hfMod.value == "modifikim") {
            if (kontrollet[i].split(',')[12] == "1")
                enabled = false;
            if (kontrollet[i].split(',')[12] == "2" && hfLidhur.value == "True") {
                enabled = false; if (k.ReadOnly) k.ReadOnly = true;
            }
            if (kontrollet[i].split(',')[12] == "2" && hfLidhur.value != "True")
                enabled = true;
        }
        else {
            if (kontrollet[i].split(',')[9] == "False")
                enabled = false;
            else if (kontrollet[i].split(',')[9] == "True")
                enabled = true;
        }
            k.SetEnabled(enabled);
    }
};
myFaqeCelje.aktivFusha = function (colKontrollet, colAtrTrupi, hfMod, isLidhur, emerContaineri, arrdrejta, arrTabela) {
    //    var kontrollet = vlerat.split(';');
    if (colKontrollet === undefined || colKontrollet === null) return;
    for (var i = 0; i < colKontrollet.length; i++) {
        if (colKontrollet[i].IdTipiKontrollit == 5)   //tipi gride
            continue;

        idkontrolli = colKontrollet[i].KodKontrolli;
        div = $('#dv' + idkontrolli);
        kontrolli = $(emerContaineri + idkontrolli);
        k = Utils.ktheKontroll(idkontrolli);
        if (!k)
            continue;
        if ((idkontrolli == "dteSalesRepStartDate" || idkontrolli == "dteLeaveDateVodafoneVod") && k.GetEnabled() == false)
            continue;
        var enabled = ((hfMod.val() == "modifikim" || hfMod.val() == "modifikimPrindi") && (colAtrTrupi[i].Identifikues == 1 || (colAtrTrupi[i].Identifikues == 2 && isLidhur))) ?
                    false : colAtrTrupi[i].Enabled;

            k.SetEnabled(enabled);
            kontrolli = $(k.GetMainElement());
        if (kontrolli.attr('id').search('cll' + idkontrolli) != -1)
            kontrolli = kontrolli.parent().parent().parent().parent().parent();
        if (arrTabela != undefined)
            for (var m = 0; m < arrTabela.length; m++) {
                if (!kontrolli["parents"]) continue;

                if (colAtrTrupi[i].Visible && (((emerContaineri != undefined && kontrolli.parents('[id=' + emerContaineri + 'C' + parseInt(parseInt(m) + parseInt(1)) + ']').attr('id') != undefined) || (emerContaineri == undefined && kontrolli.parents('[id=' + arrPrind[m] + ']').attr('id') != undefined)) || kontrolli.parents('[id=' + arrTabela[m] + ']').attr('id') != undefined)) {
                    if (arrdrejta != undefined && arrdrejta[m] == false && colKontrollet[i].IdTipiKontrollit.toString() != "0") {
                        k.SetEnabled(false);
                        break;
                    }
                }
            }

    }
    Utils.hiqLoadingGif();
};

//perdoret tek raporti
myFaqeCelje.SucceededCallbackKonfig = function (colKontrollet, colAtrTrupi, hfKontrollet, hfMod, komboModelFushaShtese, arrTabela, emerContaineri, emertabi, fillimtabi) {
    hfKontrollet.val(JSON.stringify(colKontrollet));
    if (fillimtabi == undefined)
        fillimtabi = 1;
    for (var i = 0; i < colKontrollet.length; i++) {
        if (colKontrollet[i].IdTipiKontrollit == 5)
            continue;
        idkontrolli = colKontrollet[i].KodKontrolli;
        div = $('#dv' + idkontrolli);
        kontrolli = $(emerContaineri + idkontrolli);
        k = Utils.ktheKontroll(idkontrolli);
        if (!k)
            continue;
        if (colAtrTrupi[i].Visible)
            div.show();
        else
            div.hide();
        switch (colKontrollet[i].IdTipiKontrollit) {
            case 0: //label
                k.SetValue(colAtrTrupi[i].VlereDefault);
                break;
            case 2: //comboBox
                if (colAtrTrupi[i].VlereDefault != "") {
                    Utils.SelectComboItem(k, colAtrTrupi[i].VlereDefault);
                }
                break;
            case 3: //textbox
                if (colAtrTrupi[i].VlereDefault != "")
                    k.SetText(colAtrTrupi[i].VlereDefault);
                break;
            case 8:
                if (colAtrTrupi[i].VlereDefault != "" && (colAtrTrupi[i].VlereDefault == "true" || colAtrTrupi[i].VlereDefault == "false"))
                    k.SetChecked(colAtrTrupi[i].VlereDefault);
                break;
            default: //etj
                if (colAtrTrupi[i].VlereDefault != "")
                    k.SetText(colAtrTrupi[i].VlereDefault);
                break;
        }
        if (hfMod.val() == "modifikim" && colAtrTrupi[i].Identifikues == 1)
            k.SetEnabled(false);
        else
            k.SetEnabled(colAtrTrupi[i].Enabled);

        for (m = 0; m < arrTabela.length; m++) {
            if (colAtrTrupi[i].Visible && (div.parent().attr("id") == emertabi + parseInt(parseInt(m) + parseInt(fillimtabi)) || $("#" + arrTabela[m]).children('tbody')[0].innerHTML.search('id=dv' + idkontrolli + '>') != -1)) {
                krijoTable(colAtrTrupi[i].Rreshti, colAtrTrupi[i].Kolona, arrTabela[m]);
                var rr = colAtrTrupi[i].Rreshti - 1;
                var kk = colAtrTrupi[i].Kolona - 1;
                $("#" + arrTabela[m] + ">tbody>tr:eq(" + rr + ")>td:eq(" + kk + ")")[0].appendChild(div[0]);
            }
        }
        kontrolli.height(colAtrTrupi[i].Height);
        kontrolli.width(colAtrTrupi[i].Width + '%');
        k.SetWidth(kontrolli.width());
        if (colAtrTrupi[i].Detyrueshme)
            k.validationGroup = "entries";
        else
            k.validationGroup = "entries1";
    }
};

/*
colKontrollet -> jane kontrollet e faqes qe merren nga tabela T_KONTROLLE
colAtrTrupi   -> jane atributet per cdo kontroll te colKontrolle
hfKontrollet  -> eshte hidden field-i ku vendoset koleksioni i kontrolleve
hfMod         -> hidden field ku ruhet nese eshte shtim, modifikim, klonim, etj.
komboModelFushaShtese ->
arrTabela     -> Array me emrat e tabelave ku vendosen kontrollet
emertabi      -> emertabi vendoset emri i pagecontrol me prapashtesen "_C"
fillimtabi    -> indeksi i tabit te pare ku jane vendosur kontrollet per t'u konfiguruar
hfLidhur      -> hidden field qe ruan nese eshte i lidhur rreshti i zgjedhur ne gride
arrPrind      -> perdoret kryesisht te regjistrimet. Ketu mbahen divet ku jane vendosur tabelat qe jane te arrTabela
aprovim       ->nqs dokumenti eshte me status aprovuar nuk duhet te modifikohet
*/
myFaqeCelje.SucceededCallbackKonfigurimPergjithshem = function (colKontrollet, colAtrTrupi, hfKontrollet, hfMod, komboModelFushaShtese, arrTabela, emertabi, fillimtabi, hfLidhur, arrPrind, aprovim, arrdrejta, vendosKlasaAutomatike, vleraDefaultKlonimi) {
    if (hfKontrollet != undefined)
        hfKontrollet.val(JSON.stringify(colKontrollet));
    if (fillimtabi == undefined)
        fillimtabi = 1;

    var vlerat = '', tmpKontroll, tmpAtrTrupi;
    for (var i = 0, nrKontrolle = colKontrollet.length; i < nrKontrolle; i++) {
        tmpKontroll = colKontrollet[i];
        tmpAtrTrupi = colAtrTrupi[i];

        if (tmpKontroll.IdTipiKontrollit == 5) {
            kontrolli = $("#rowed5")[0];
            if (tmpAtrTrupi.Identifikues == 2 && hfLidhur.val() == "True") //kujdes se mund te jete pa vlere hfli
                lidhur = true;
            else
                lidhur = false;
            if (aprovim != undefined && aprovim)
                lidhur = true;
            continue;
        }

        idkontrolli = tmpKontroll.KodKontrolli;
        k = Utils.ktheKontroll(idkontrolli);
        if (Object.keys(k).length === 0)
            continue;
        kontrolli = $(k.GetMainElement());

        if (tmpAtrTrupi.Visible !== null || tmpAtrTrupi.Enabled !== undefined) {
            k.SetVisible(tmpAtrTrupi.Visible);
        }
        if (tmpAtrTrupi.Detyrueshme)
            k.validationGroup = "entries";
        else {
            k.validationGroup = "entries1";
            k.validateOnLeave = false;
        }
        if (tmpKontroll.IdTipiKontrollit == 11)
            $("#" + idkontrolli).attr("required", tmpAtrTrupi.Detyrueshme);
        if (hfMod != undefined && (hfMod.val() == "modifikim" || hfMod.val() == "modifikimPrindi" || (hfMod.val() == "klonim" && Utils.getUrlVar("modMarreveshje") == "modifikim")) && tmpAtrTrupi.Identifikues == 1) {
            k.SetEnabled(false);
        } else if (tmpAtrTrupi.Enabled !== null || tmpAtrTrupi.Enabled !== undefined) {
            k.SetEnabled(tmpAtrTrupi.Enabled);
        }
        if (hfMod != undefined && hfMod.val() != "shtim" && hfMod.val() != "shtimPrindi" && hfMod.val() != "shtimraport" && hfMod.val() != "rezervim" && hfMod.val() != "riparim" && fillimtabi != 0 && hfMod.val() != "konvertim") {
            if (tmpKontroll.IdTipiKontrollit == 0 && idkontrolli !== 'lblKonfigurimi')
                k.SetValue(tmpAtrTrupi.VlereDefault.toString());

            if (idkontrolli === 'cbRenditje')
                 k.SetChecked(tmpAtrTrupi.VlereDefault == 'true' ? true : false);

            if (hfMod.val() == "modifikim" || hfMod.val() == "modifikimPrindi" || (hfMod.val() == "klonim" && Utils.getUrlVar("modMarreveshje") == "modifikim")) {
                if ((aprovim != undefined && aprovim && tmpKontroll.IdTipiKontrollit != 0) || tmpAtrTrupi.Identifikues == 1 || (tmpAtrTrupi.Identifikues == 2 && hfLidhur !== undefined && hfLidhur.val() == "True")) {
                    k.SetEnabled(false);
                }
            }

            if (vleraDefaultKlonimi && k.GetValue && !k.GetValue())
                this.VendosVleraDefaultKonfigurimi(tmpKontroll, tmpAtrTrupi, idkontrolli, k, hfMod);
        }
        else {
            this.VendosVleraDefaultKonfigurimi(tmpKontroll, tmpAtrTrupi, idkontrolli, k, hfMod);
        }

        if (tmpKontroll.IdTipiKontrollit != 11 && kontrolli.attr('id').search('cll' + idkontrolli) != -1)
            kontrolli = kontrolli.parent().parent().parent().parent().parent();

        for (m = 0; m < arrTabela.length; m++) {

            if (tmpAtrTrupi.Visible && (k.Options && k.Options.container == arrTabela[m] || (((emertabi != undefined && kontrolli.parents('[id=' + emertabi + parseInt(parseInt(m) + parseInt(fillimtabi)) + ']').attr('id') != undefined) || (emertabi == undefined && kontrolli.parents('[id=' + arrPrind[m] + ']').attr('id') != undefined)) || kontrolli.parents('[id=' + arrTabela[m] + ']').attr('id') != undefined))) {

                myFaqeCelje.krijoTable(tmpAtrTrupi.Rreshti, tmpAtrTrupi.Kolona, arrTabela[m]);
                var rr = tmpAtrTrupi.Rreshti - 1;
                var kk = tmpAtrTrupi.Kolona - 1;
                if (arrTabela[m] == 'tblPunesim' && k.validationGroup == "entries")
                    k.validationGroup = "entriesPun";
                if (arrdrejta != undefined && arrdrejta[m] == false && tmpKontroll.IdTipiKontrollit.toString() != "0")
                    k.SetEnabled(false);
                var cellElement = $("#" + arrTabela[m] + ">tbody>tr:eq(" + rr + ")>td:eq(" + kk + ")");
                if (tmpKontroll.IdTipiKontrollit.toString() == '7' && !(idkontrolli == 'txtShenime' && (colKontrollet[i].IdKomponente.toString() == '3072' || colKontrollet[i].IdKomponente == '3076' || colKontrollet[i].IdKomponente == '3078'))) {
                    cellElement.attr('rowspan', 3).attr('colspan', 1).addClass("renditKontrolleCell");
                }
                else
                    if (colKontrollet[i].IdTipiKontrollit.toString() == '7' && idkontrolli == 'txtShenime' && (colKontrollet[i].IdKomponente.toString() == '3072' || colKontrollet[i].IdKomponente == '3076' || colKontrollet[i].IdKomponente == '3078')) {
                        cellElement.attr('colspan', 5).addClass("renditKontrolleCell").data('llojKontrolli', 'label');
                    }
                    else
                        if (tmpKontroll.IdTipiKontrollit.toString() == '10') {
                            cellElement.attr('colspan', 2).addClass("renditKontrolleCell");
                        }
                        else
                            if (idkontrolli == 'lblKonfigurimi') {
                                cellElement.attr('colspan', 4).addClass("renditKontrolleCaption").data('llojKontrolli', 'label');
                            }

                            else
                                if (tmpKontroll.IdTipiKontrollit.toString() == '0') {
                                    cellElement.addClass("renditKontrolleCaption").data('llojKontrolli', 'label');
                                }
                                else {
                                    cellElement.attr('colspan', 1).attr('rowspan', 1).addClass("renditKontrolleCell");
                                }
                var kontainerKontrolli = kontrolli.parents('[id^=' + kontrolli.attr('id') + ']').last();
                cellElement[0].appendChild(kontainerKontrolli.length != 0 ? kontainerKontrolli[0] : kontrolli[0]);
                cellElement.data('idkontrolli', idkontrolli);
            }
        }
        if (tmpKontroll.IdTipiKontrollit == 11)
            k.Init();
    }
    if (vendosKlasaAutomatike) {
        myFaqeCelje.rregulloGjeresiteFushave(arrTabela);
    }
    return vlerat;
};

myFaqeCelje.VendosVleraDefaultKonfigurimi = function (tmpKontroll, tmpAtrTrupi, idkontrolli, k, hfMod) {
    switch (tmpKontroll.IdTipiKontrollit) {
        case 0: //label
            if (idkontrolli !== 'lblKonfigurimi')
                k.SetValue(tmpAtrTrupi.VlereDefault);
            break;
        case 2:
        case 4: //comboBox
            if (tmpAtrTrupi.PershkrimKontroll.toLowerCase() !== 'caktimi i kursit' && tmpAtrTrupi.PershkrimKontroll.toLowerCase() !== 'caktimi i kursit te pageses' && !(tmpAtrTrupi.PershkrimKontroll.toLowerCase() == 'caktimi i agjentit' && hfMod.val() == "konvertim")) {
                if (tmpAtrTrupi.VlereDefault != "") {
                    Utils.SelectComboItem(k, tmpAtrTrupi.VlereDefault);
                }
            }
            break;
        case 3: //textbox
            if (tmpAtrTrupi.VlereDefault != "")
                k.SetText(tmpAtrTrupi.VlereDefault);
            break;
        case 8: //checkbox
            if (tmpAtrTrupi.VlereDefault != "" && (tmpAtrTrupi.VlereDefault == "true" || tmpAtrTrupi.VlereDefault == "false"))
                if (idkontrolli !== 'cbRuajFilter')
                    k.SetChecked(tmpAtrTrupi.VlereDefault == 'true' ? true : false);
            break;
        case 11: //multiselect
            {
                if (tmpAtrTrupi.VlereDefault != "") {
                    k.SetValue(tmpAtrTrupi.VlereDefault.split(','));
                }
                break;
            }
        default: //etj
            if (tmpAtrTrupi.VlereDefault != "" && !(tmpAtrTrupi.PershkrimKontroll.toLowerCase() == 'perqindja e fitimit te agjentit per faturen perkatese' && hfMod.val() == "konvertim"))
                k.SetText(tmpAtrTrupi.VlereDefault);
            break;
    }
};

myFaqeCelje.SucceededCallbackKonfigurimPergjithshemSeriale = function (colKontrollet, colAtrTrupi, hfKontrollet, hfMod, komboModelFushaShtese, arrTabela, emertabi, fillimtabi, hfLidhur, arrPrind, aprovim, arrdrejta) {
    if (hfKontrollet != undefined)
        hfKontrollet.val(JSON.stringify(colKontrollet));
    if (fillimtabi == undefined)
        fillimtabi = 1;

    var vlerat = '';
    for (var i = 0; i < colKontrollet.length; i++) {
        if (colKontrollet[i].IdTipiKontrollit == 5) {
            kontrolli = $("#rowed5")[0];
            if (colAtrTrupi[i].Identifikues == 2 && hfLidhur.val() == "True") //kujdes se mund te jete pa vlere hfli
                lidhur = true;
            else
                lidhur = false;
            if (aprovim != undefined && aprovim)
                lidhur = true;
            continue;
        }

        idkontrolli = colKontrollet[i].KodKontrolli;
        k = Utils.ktheKontroll(idkontrolli);
        if (!k)
            continue;
        kontrolli = $(k.GetMainElement());

        if (colAtrTrupi[i].Visible !== null || colAtrTrupi[i].Enabled !== undefined)
            k.SetVisible(colAtrTrupi[i].Visible);
        if (colAtrTrupi[i].Detyrueshme)
            k.validationGroup = "entries";
        else
            k.validationGroup = "entries1";
        if (hfMod != undefined && hfMod.val() == "modifikim" && colAtrTrupi[i].Identifikues == 1)
            k.SetEnabled(false);
            //else if (colKontrollet[i].IdTipiKontrollit == 3 && hfMod != undefined && hfMod.val() == "modifikim" && colAtrTrupi[i].Identifikues == 2)
            //    k.SetEnabled(false);
        else if (colAtrTrupi[i].Enabled !== null || colAtrTrupi[i].Enabled !== undefined)
            k.SetEnabled(colAtrTrupi[i].Enabled);

        if (hfMod != undefined && hfMod.val() != "shtim" && hfMod.val() != "rezervim" && hfMod.val() != "riparim" && fillimtabi != 0 && hfMod.val() != "konvertim") {
            if (colKontrollet[i].IdTipiKontrollit == 0 && idkontrolli !== 'lblKonfigurimi')
                k.SetValue(colAtrTrupi[i].VlereDefault.toString());
            if (idkontrolli === 'cbRenditje')
                k.SetChecked(colAtrTrupi[i].VlereDefault == 'true' ? true : false);
            if (idkontrolli === komboModelFushaShtese && colAtrTrupi[i].VlereDefault != '' && colKontrollet[i].IdTipiKontrollit == 2) {
                k.SetText(colAtrTrupi[i].VlereDefault);
                k.SelectedIndexChanged.FireEvent(k);
            }
            if (hfMod.val() == "modifikim") {
                if (aprovim != undefined && aprovim && colKontrollet[i].IdTipiKontrollit != 0)
                    k.SetEnabled(false);
                else {
                    if (colAtrTrupi[i].Identifikues == 1)
                        k.SetEnabled(false);
                    if (colAtrTrupi[i].Identifikues == 2 && hfLidhur !== undefined && hfLidhur.val() == "True")
                        k.SetEnabled(false);
                }
            }
        }
        else {
            switch (colKontrollet[i].IdTipiKontrollit) {
                case 0: //label
                    if (idkontrolli !== 'lblKonfigurimi')
                        k.SetValue(colAtrTrupi[i].VlereDefault);
                    break;
                case 2:
                case 4: //comboBox
                    if (colAtrTrupi[i].PershkrimKontroll.toLowerCase() !== 'caktimi i kursit' && colAtrTrupi[i].PershkrimKontroll.toLowerCase() !== 'caktimi i kursit te pageses') {
                        if (colAtrTrupi[i].VlereDefault != "") {
                            Utils.SelectComboItem(k, colAtrTrupi[i].VlereDefault);
                        }
                    }
                    break;
                case 3: //textbox
                    if (colAtrTrupi[i].VlereDefault != "")
                        k.SetText(colAtrTrupi[i].VlereDefault);
                    break;
                case 8: //checkbox
                    if (colAtrTrupi[i].VlereDefault != "" && (colAtrTrupi[i].VlereDefault == "true" || colAtrTrupi[i].VlereDefault == "false"))
                        k.SetChecked(colAtrTrupi[i].VlereDefault == 'true' ? true : false);
                    break;
                default: //etj
                    if (colAtrTrupi[i].VlereDefault != "")
                        k.SetText(colAtrTrupi[i].VlereDefault);
                    break;
            }
        }

        if (kontrolli.attr('id').search('cll' + idkontrolli) != -1)
            kontrolli = kontrolli.parent().parent().parent().parent().parent();
        for (m = 0; m < arrTabela.length; m++) {
            if (colAtrTrupi[i].Visible && (((emertabi != undefined && kontrolli.parents('[id=' + emertabi + parseInt(parseInt(m) + parseInt(fillimtabi)) + ']').attr('id') != undefined) || (emertabi == undefined && kontrolli.parents('[id=' + arrPrind[m] + ']').attr('id') != undefined)) || kontrolli.parents('[id=' + arrTabela[m] + ']').attr('id') != undefined)) {
                krijoTable(colAtrTrupi[i].Rreshti, colAtrTrupi[i].Kolona, arrTabela[m]);
                var rr = colAtrTrupi[i].Rreshti - 1;
                var kk = colAtrTrupi[i].Kolona - 1;
                if (arrTabela[m] == 'tblPunesim' && k.validationGroup == "entries")
                    k.validationGroup = "entriesPun";
                if (arrdrejta != undefined && arrdrejta[m] == false && colKontrollet[i].IdTipiKontrollit.toString() != "0")
                    k.SetEnabled(false);
                if (colKontrollet[i].IdTipiKontrollit.toString() == '7' && !(idkontrolli == 'txtShenime' && (colKontrollet[i].IdKomponente.toString() == '3072' || colKontrollet[i].IdKomponente == '3076' || colKontrollet[i].IdKomponente == '3078'))) {
                    $("#" + arrTabela[m] + ">tbody>tr:eq(" + rr + ")>td:eq(" + kk + ")").attr('rowspan', 3);
                    $("#" + arrTabela[m] + ">tbody>tr:eq(" + rr + ")>td:eq(" + kk + ")").attr('colspan', 1);
                    $("#" + arrTabela[m] + ">tbody>tr:eq(" + rr + ")>td:eq(" + kk + ")").addClass("renditKontrolleCell");
                }
                else
                    if (colKontrollet[i].IdTipiKontrollit.toString() == '7' && idkontrolli == 'txtShenime' && (colKontrollet[i].IdKomponente.toString() == '3072' || colKontrollet[i].IdKomponente == '3076' || colKontrollet[i].IdKomponente == '3078')) {
                        $("#" + arrTabela[m] + ">tbody>tr:eq(" + rr + ")>td:eq(" + kk + ")").attr('colspan', 5);
                        $("#" + arrTabela[m] + ">tbody>tr:eq(" + rr + ")>td:eq(" + kk + ")").addClass("renditKontrolleCell");
                        $("#" + arrTabela[m] + ">tbody>tr:eq(" + rr + ")>td:eq(" + kk + ")").data('llojKontrolli', 'label');
                    }
                else
                    if (colKontrollet[i].IdTipiKontrollit.toString() == '10') {
                        $("#" + arrTabela[m] + ">tbody>tr:eq(" + rr + ")>td:eq(" + kk + ")").attr('colspan', 2);
                        $("#" + arrTabela[m] + ">tbody>tr:eq(" + rr + ")>td:eq(" + kk + ")").addClass("renditKontrolleCell");
                    }
                    else
                        if (idkontrolli == 'lblKonfigurimi') {
                            $("#" + arrTabela[m] + ">tbody>tr:eq(" + rr + ")>td:eq(" + kk + ")").attr('colspan', 4);
                            $("#" + arrTabela[m] + ">tbody>tr:eq(" + rr + ")>td:eq(" + kk + ")").addClass("renditKontrolleCaption");
                            $("#" + arrTabela[m] + ">tbody>tr:eq(" + rr + ")>td:eq(" + kk + ")").data('llojKontrolli', 'label');
                        }

                        else
                            if (colKontrollet[i].IdTipiKontrollit.toString() == '0') {
                                $("#" + arrTabela[m] + ">tbody>tr:eq(" + rr + ")>td:eq(" + kk + ")").addClass("renditKontrolleCaption");
                                $("#" + arrTabela[m] + ">tbody>tr:eq(" + rr + ")>td:eq(" + kk + ")").data('llojKontrolli', 'label');
                            }
                            else {
                                $("#" + arrTabela[m] + ">tbody>tr:eq(" + rr + ")>td:eq(" + kk + ")").attr('colspan', 1);
                                $("#" + arrTabela[m] + ">tbody>tr:eq(" + rr + ")>td:eq(" + kk + ")").attr('rowspan', 1);
                                $("#" + arrTabela[m] + ">tbody>tr:eq(" + rr + ")>td:eq(" + kk + ")").addClass("renditKontrolleCell");
                            }
                var kontainerKontrolli = kontrolli.parents('[id^=' + kontrolli.attr('id') + ']').last();
                if (kontainerKontrolli.length != 0) {
                    $("#" + arrTabela[m] + ">tbody>tr:eq(" + rr + ")>td:eq(" + kk + ")")[0].appendChild(kontainerKontrolli[0]);
                    $("#" + arrTabela[m] + ">tbody>tr:eq(" + rr + ")>td:eq(" + kk + ")").data('idkontrolli', idkontrolli);
                }
                else {
                    $("#" + arrTabela[m] + ">tbody>tr:eq(" + rr + ")>td:eq(" + kk + ")")[0].appendChild(kontrolli[0]);
                    $("#" + arrTabela[m] + ">tbody>tr:eq(" + rr + ")>td:eq(" + kk + ")").data('idkontrolli', idkontrolli);
                }
            }
        }
    }
    for (i = 0; i < arrTabela.length; i++) {
        myFaqeCelje.rregulloGjeresite($('#' + arrTabela[i]));
    }
    return vlerat;
};

myFaqeCelje.rregulloGjeresiteFushave = function (arrTabela, rialokim) {
    for (var j = 0; j < arrTabela.length; j++) {
        myFaqeCelje.rregulloGjeresite($('#' + arrTabela[j]), rialokim);
    }
};

myFaqeCelje.rregulloGjeresite = function (emertabele, rialokim) {
    var trs = emertabele[0].getElementsByTagName("tr");
    var nrKolRr = 0;
    var nrKolTot = 0;

    for (var i = 0; i < trs.length; i++) {
        var tds = trs[i].getElementsByTagName("td");
        for (var j = 0; j < tds.length; j++) {
            var td = tds[j];
            var elements = td.getElementsByTagName("label");
            if (elements.length > 0 && elements[0].id)
                continue;
            if (td.children.length > 0) {
                var kontr = eval(td.dataset["idkontrolli"]);
                if (kontr != undefined && ((kontr.GetVisible && kontr.GetVisible()) || !kontr.hidden))
                    nrKolRr++;
            }
        }
        if (nrKolRr > nrKolTot)
            nrKolTot = nrKolRr;

        nrKolRr = 0;
    }

    myFaqeCelje.rregulloGjeresiteKolone(emertabele, nrKolTot, rialokim);
    return nrKolTot;
};

myFaqeCelje.rregulloGjeresiteKolone = function (emertabele, nrKolonash, rialokim) {
    var table = emertabele[0];
    if (table.className.indexOf('renditKontrolle') == 0) { //nese nuk eshte custom        
        if (nrKolonash === 1)
            table.className = 'renditKontrolleNje';
        else if (nrKolonash === 2)
            table.className = 'renditKontrolleDy';
        else if (nrKolonash === 3 || (rialokim == true && nrKolonash === 1))
            table.className = 'renditKontrolleTre';
        else if (nrKolonash >= 4)
            table.className = 'renditKontrolle';
    }

    return true;
};

myFaqeCelje.kontrolloTeDrejta = function (emrimenuse, buttonshto, buttonKerko, faqeRe) {  //GETSON
    if (emrimenuse == "")
        return;
    if (buttonshto) {
        PastroClick();
        return;
    }
    if (buttonKerko) {
        if (ButtonClickKerko)
            ButtonClickKerko(emrimenuse);
        else
            console.Error("Mungon funksioni ButtonClickKerko ne ambjent!");
        return;
    }
    if (faqeRe) {
     
        window.open(Utils.setUrlVar('ambienti', emrimenuse, window.location.origin + window.location.pathname), '_blank');
        return;
    }
    window.location = emrimenuse;
};

myFaqeCelje.hapFaqeNeTabTeRi = function (emrimenuse) {
    if (emrimenuse == "")
        return;
    var path = "/FaqeKryesore.aspx";
    window.open(Utils.setUrlVar('ambienti', emrimenuse, window.location.origin + path), '_blank');

    return
    window.location = emrimenuse;
};

if (typeof myMenu == 'undefined') {
    myMenu = {};
}
if (typeof myMenu.JSlevizNeGride == 'undefined') {
    myMenu.JSlevizNeGride = {};
}
var levizNgaShigjetat = false;
/*
Function: Poshte_click

perdoret per te selektuar rreshtin me poshte

Parameters:

e-eventi
*/
myMenu.JSlevizNeGride.Poshte_click = function (e, emriGrida, indexSel) {
    levizNgaShigjetat = true;
    emriGrida.UnselectAllRowsOnPage();
    if ((indexSel + 1) < (emriGrida.cpPageIndex + 1) * emriGrida.cpPageRow) {
        indexSel = indexSel + 1;
        if (indexSel >= emriGrida.cpRowCount - 1) //eshte ne faqen qe ka me pak rreshta se page size
            indexSel = emriGrida.cpPageIndex * emriGrida.cpPageRow;
    }
    else {
        indexSel = emriGrida.cpPageIndex * emriGrida.cpPageRow;
    }

    emriGrida.SelectRowOnPage(indexSel);
    e.processOnServer = false;
    levizNgaShigjetat = false;
    return indexSel;
};
/*Function: Lart_click

perdoret per te selektuar rreshtin me lart

Parameters:

e-eventi
*/
myMenu.JSlevizNeGride.Lart_click = function (e, emriGrida, indexSel) {
    levizNgaShigjetat = true;
    emriGrida.UnselectAllRowsOnPage();
    if ((indexSel - 1) < emriGrida.cpPageIndex * emriGrida.cpPageRow) {
        indexSel = (emriGrida.cpPageIndex + 1) * emriGrida.cpPageRow - 1;
    }
    else {
        indexSel = indexSel - 1;
    }
    //kontroll nqs jemi ne faqe qe ka me pak rreshta se page size
    if (indexSel >= emriGrida.cpRowCount - 1)
        indexSel = emriGrida.cpRowCount - 1;
    emriGrida.SelectRowOnPage(indexSel);
    e.processOnServer = false;
    levizNgaShigjetat = false;
    return indexSel;
};
/*
Function: Fillim_click

perdoret per te shkuar ne fillim te faqes

Parameters:

e-eventi
*/
myMenu.JSlevizNeGride.Fillim_click = function (e, emriGrida, indexSel) {
    levizNgaShigjetat = true;
    emriGrida.UnselectAllRowsOnPage();
    indexSel = emriGrida.cpPageIndex * emriGrida.cpPageRow;
    emriGrida.SelectRowOnPage(indexSel);
    e.processOnServer = false;
    levizNgaShigjetat = false;
    return indexSel;
};
/*
Function: Fund_click

perdoret per te shkuar ne fund te faqes

Parameters:

e-eventi
*/
myMenu.JSlevizNeGride.Fund_click = function (e, emriGrida, indexSel) {
    levizNgaShigjetat = true;
    emriGrida.UnselectAllRowsOnPage();
    indexSel = Math.min((emriGrida.cpPageIndex + 1) * emriGrida.cpPageRow, emriGrida.cpRowCount) - 1;
    emriGrida.SelectRowOnPage(indexSel);
    e.processOnServer = false;
    levizNgaShigjetat = false;
    return indexSel;
};
/*
Function: OnGridSelectionChanged

perdoret per te ruajtur indexin e selektimit

Parameters:

e-eventi
*/
myMenu.JSlevizNeGride.OnGridSelectionChanged = function (e, indexSel) {
    //mqs ne kete funks hyn sa here ndryshon sel te nje row
    //ketu duhet te hyje vetem ne init te faqes dhe kur selectohet manualisht nga design,
    //me mouse nje row qe prish rregullin para, pas per indexin
    if (levizNgaShigjetat == false) {
        indexSel = e.visibleIndex;
    }
    return indexSel;
};
myMenu.menu_click = function (s, e, hfShtimModifikim, hfId, PageControl, ruajbuxhetet, konfigurim, indexModifiko, pastroFusha, SuccededCallbackKonfig, resultKonf, colKontrollet, aktivizoFusha, colAtrTrupi) {
    if (e.item.name == 'Fshi') {
        myMenu.FshiClick(e);
        return;
    }
    if (e.item.name == 'Shto') {
        if (indexModifiko === undefined)
            indexModifiko = window.indexModifiko;
        if (pastroFusha === undefined)
            pastroFusha = pastrofusha;
        if (konfigurim !== false) {
            if (SuccededCallbackKonfig === undefined)
                SuccededCallbackKonfig = SucceededCallbackKonfigurimiInit;
            if (resultKonf === undefined)
                resultKonf = resultkonf;
            if (colKontrollet === undefined)
                colKontrollet = $('#hfKontrollet').val();
            if (aktivizoFusha === undefined)
                aktivizoFusha = window.aktivizoFusha;
        }
        myMenu.ShtoClick(e, hfShtimModifikim, hfId, PageControl, konfigurim, indexModifiko, pastroFusha, SuccededCallbackKonfig, resultKonf, colKontrollet, aktivizoFusha, colAtrTrupi);
        return;
    }
    if (e.item.name == 'Ruaj') {
        Utils.shfaqLoadingGif();
        myMenu.RuajClick(s, e, ruajbuxhetet, PageControl);
        return;
    }
    if (e.item.name == 'Modifiko') {
        myMenu.ModifikoClick(e, hfShtimModifikim);
        return;
    }
    if (e.item.name == 'Klono') {
        myMenu.KlonoClick(e, hfShtimModifikim, hfId);
        return;
    }
    if (e.item.name == 'Kthim') {
        myMenu.KthimClick(e, hfShtimModifikim, hfId);
        return;
    }
    if (e.item.name == 'GoldenNumbers' || e.item.name == 'NormalNumbers') {
        myMenu.ShtoClick(e, hfShtimModifikim, hfId, PageControl, konfigurim, indexModifiko, pastroFusha, SuccededCallbackKonfig, resultKonf, colKontrollet, aktivizoFusha, colAtrTrupi);
        return;
    }
};

myMenu.menu_click_batchEdit = function (s, e, hfShtimModifikim, hfId, PageControl, grida, konfigurim, indexModifiko, pastroFusha, SuccededCallbackKonfig, resultKonf, colKontrollet, aktivizoFusha, colAtrTrupi, hfStatus) {
    if (e.item.name == 'Fshi') {
        myMenu.FshiClick(e);
        return;
    }
    if (e.item.name == 'Shto') {
        if (indexModifiko === undefined)
            indexModifiko = window.indexModifiko;
        if (pastroFusha === undefined)
            pastroFusha = pastrofusha;
        if (konfigurim !== false) {
            if (SuccededCallbackKonfig === undefined)
                SuccededCallbackKonfig = SucceededCallbackKonfigurimiInit;
            if (resultKonf === undefined)
                resultKonf = resultkonf;
            if (colKontrollet === undefined)
                colKontrollet = $('#hfKontrollet').val();
            if (aktivizoFusha === undefined)
                aktivizoFusha = window.aktivizoFusha;
        }
        myMenu.ShtoClick(e, hfShtimModifikim, hfId, PageControl, konfigurim, indexModifiko, pastroFusha, SuccededCallbackKonfig, resultKonf, colKontrollet, aktivizoFusha, colAtrTrupi);
        return;
    }
    if (e.item.name == 'Ruaj') {
        Utils.shfaqLoadingGif();
        myMenu.RuajClickBatch(s, e, PageControl, grida, hfStatus);
        return;
    }
    if (e.item.name == "Draft") {
        Utils.shfaqLoadingGif();
        myMenu.DraftClickBatch(s, e, PageControl, grida, hfStatus);
        return;
    }
    if (e.item.name == 'Modifiko') {
        myMenu.ModifikoClick(e);
        return;
    }
    if (e.item.name == 'Klono') {
        myMenu.KlonoClick(e, hfShtimModifikim, hfId);
        return;
    }
};

/*
Function: menu_click

perdoret per veprimet e menuse ne javascript

Parameters:

e-eventi
*/
//myMenu.menu_click = function (s, e, hfShtimModifikim, hfId, PageControl, ruajbuxhetet, konfigurim) {
//    if (e.item.name == 'Fshi') {
//        myMenu.FshiClick(e);
//        return;
//    }
//    if (e.item.name == 'Shto') {
//        myMenu.ShtoClick(e, hfShtimModifikim, hfId, PageControl, konfigurim);
//        return;
//    }
//    if (e.item.name == 'Ruaj') {
//        Utils.shfaqLoadingGif();;
//        myMenu.RuajClick(s, e, ruajbuxhetet);
//        return;
//    }
//    if (e.item.name == 'Modifiko') {
//        myMenu.ModifikoClick(e);
//        return;
//    }
//    if (e.item.name == 'Klono') {
//        myMenu.KlonoClick(e, hfShtimModifikim, hfId);
//        return;
//    }
//}
myMenu.menu_click_regjistrime = function (s, e, url, urlMod, selectrowcount) {
    switch (e.item.name) {
        case 'Fshi': myMenu.FshiClick(e);
            return true;
        case 'Shto': myMenu.ShtoClickRegjistrime(e, url);
            return true;
        case 'Shiko': myMenu.ShikoClick(e, urlMod);
            return true;
        case 'Riruaj': myMenu.RiruajClick(e, selectrowcount);
            return true;
        case 'Grupo': myMenu.GrupoClick(e);
            return true;
        default:
            return false;
    }
};
myMenu.menu_click_celjevogel = function (s, e, hfRuaj, grid, hfTeDrejta) {
    if (e.item.name == 'Fshi')
        myMenu.FshiClick(e);
    if (e.item.name == 'Shto') {
        hfRuaj.val('Ruaj');
        s.GetItemByName('Ruaj').SetVisible(true);
        grid.AddNewRow();
        e.processOnServer = false;
    }
    if (e.item.name == 'Modifiko') {
        hfRuaj.val('Modifiko');
        s.GetItemByName('Ruaj').SetVisible(true);
        indexModifiko = grid.GetFocusedRowIndex();
        grid.StartEditRow(indexModifiko);
        e.processOnServer = false;
    }
    if (e.item.name == 'Ruaj') {
        grid.UpdateEdit();
        e.processOnServer = false;
    }
    ASPxMenu1.AdjustControl();
    myMenu.menuSipasTeDrejtaCeljeVogel(hfRuaj, hfTeDrejta);
};

myMenu.menuSipasTeDrejtaCeljeVogel = function (hfRuaj, hfTeDrejta) {
    var ruajButon = ASPxMenu1.GetItemByName('Ruaj');
    if (ruajButon != null)
        if (hfTeDrejta.Get('Shtim') == true && hfRuaj.val() != 'Modifiko') {
            ruajButon.SetEnabled(true);
        }
        else
            if (hfTeDrejta.Get('Modifikim') == true && hfRuaj.val() == 'Modifiko') {
                ruajButon.SetEnabled(true);
            }
            else {
                ruajButon.SetEnabled(false);
            }
    var modifikoButon = ASPxMenu1.GetItemByName('Modifiko');
    if (modifikoButon != null)
        if (hfTeDrejta.Get('Modifikim') == true)
            modifikoButon.SetEnabled(true);
        else
            modifikoButon.SetEnabled(false);
    ASPxMenu1.AdjustControl();
};

myMenu.menuSipasTeDrejtaRegjistrim_old = function (hf, hfTeDrejta) {
    if (hfTeDrejta.Get('Shtim') == true && hf.val() != 'modifikim') {
        try {
            ASPxMenu1.GetItemByName('Ruaj').SetEnabled(true);
            ASPxMenu1.GetItemByName('Draft').SetEnabled(true);
            ASPxMenu1.GetItemByName('RuajPrint').SetEnabled(true);
            ASPxMenu1.GetItemByName('RuajKase').SetEnabled(true);
        }
        catch (ee) {
        }
        try {
            ASPxMenu1.GetItemByName('Kontabilizo').SetEnabled(true);
        }
        catch (ee) {
        }
    }
    else if (hfTeDrejta.Get('Modifikim') == true && hf.val() == 'modifikim') {
            try {
                ASPxMenu1.GetItemByName('Ruaj').SetEnabled(true);
                ASPxMenu1.GetItemByName('Draft').SetEnabled(true);
                ASPxMenu1.GetItemByName('RuajPrint').SetEnabled(true);
                ASPxMenu1.GetItemByName('RuajKase').SetEnabled(true);
            }
            catch (ee) { }
            try {
                ASPxMenu1.GetItemByName('Kontabilizo').SetEnabled(true);
            }
            catch (ee) { }
    }
    else {
            try {
                ASPxMenu1.GetItemByName('Ruaj').SetEnabled(false);
                ASPxMenu1.GetItemByName('Draft').SetEnabled(false);
                ASPxMenu1.GetItemByName('RuajPrint').SetEnabled(false);
                ASPxMenu1.GetItemByName('RuajKase').SetEnabled(false);
            }
            catch (ee) { }
            try {
                ASPxMenu1.GetItemByName('Kontabilizo').SetEnabled(false);
            }
            catch (ee) { }
         }
};

myMenu.menuSipasTeDrejtaRegjistrim = function (hf, hfTeDrejta) {
    if (hf.val() != 'modifikim') {
        if (hfTeDrejta.Get('Shtim')) {
            try {
                ASPxMenu1.GetItemByName('Ruaj').SetEnabled(true);
                ASPxMenu1.GetItemByName('RuajPrint').SetEnabled(true);
                ASPxMenu1.GetItemByName('RuajKase').SetEnabled(true);
            }
            catch (ex) { }
        }
        else {
            try {
                ASPxMenu1.GetItemByName('Ruaj').SetEnabled(false);
                ASPxMenu1.GetItemByName('RuajPrint').SetEnabled(false);
                ASPxMenu1.GetItemByName('RuajKase').SetEnabled(false);
            }
            catch (ex) { }
        }

        if (hfTeDrejta.Get('ShtimDraft'))
            try {
                ASPxMenu1.GetItemByName('Draft').SetEnabled(true);
            }
            catch (ex) { }
        else
            try {
                ASPxMenu1.GetItemByName('Draft').SetEnabled(false);
            }
            catch (ex) { }
    }
    else if (hf.val() == 'modifikim') {
        if (hfTeDrejta.Get('Modifikim')) {
            try {
                ASPxMenu1.GetItemByName('Ruaj').SetEnabled(true);
                ASPxMenu1.GetItemByName('RuajPrint').SetEnabled(true);
                ASPxMenu1.GetItemByName('RuajKase').SetEnabled(true);
            } catch (ex) { }
        }
        else {
            try {
                ASPxMenu1.GetItemByName('Ruaj').SetEnabled(false);
                ASPxMenu1.GetItemByName('RuajPrint').SetEnabled(false);
                ASPxMenu1.GetItemByName('RuajKase').SetEnabled(false);
            } catch (ex) { }
        }
        if (hfTeDrejta.Get('ModifikimDraft')) {
            try {
                ASPxMenu1.GetItemByName('Draft').SetEnabled(true);
            } catch (ex) { }
        }
        else {
            try {
                ASPxMenu1.GetItemByName('Draft').SetEnabled(false);
            } catch (ex) { }
        }
    }
    try {
        ASPxMenu1.GetItemByName('Pezullo').SetEnabled(hfTeDrejta.Get('Pezullo'));
        ASPxMenu1.GetItemByName('Posto').SetEnabled(hfTeDrejta.Get('Posto'));
    }
    catch (ex) { }
    ASPxMenu1.AdjustControl();
};

myMenu.menuSipasTeDrejtaRegjistrimSipasNivelit = function (hf, hfTeDrejta) {
    myMenu.menuSipasTeDrejtaRegjistrim(hf, hfTeDrejta);

    ASPxMenu1.GetItemByName('Klono').SetEnabled(hfTeDrejta.Get('Shtim') || hfTeDrejta.Get('ShtimDraft'));
    ASPxMenu1.GetItemByName('Arkiva').SetEnabled(hfTeDrejta.Get('Arkiva'));
    ASPxMenu1.GetItemByName('Konverto').SetEnabled(hfTeDrejta.Get('Konverto'));
};

myMenu.menuSipasTeDrejtaRegjistrimPaDraft = function (hf, hfTeDrejta) {
    if (hfTeDrejta.Get('Shtim') == true && hf.val() != 'modifikim') {
        ASPxMenu1.GetItemByName('Ruaj').SetEnabled(true);
    }
    else if (hfTeDrejta.Get('Modifikim') == true && hf.val() == 'modifikim') {
        ASPxMenu1.GetItemByName('Ruaj').SetEnabled(true);
    }
    else {
        ASPxMenu1.GetItemByName('Ruaj').SetEnabled(false);
    }
};

myMenu.menu_click_celjevogelinfo = function (s, e, hfRuaj, grid, hfTeDrejta) {
    if (e.item.name == 'Fshi') {
        myMenu.FshiClick(e);
        s.GetItemByName('Ruaj').SetVisible(false);
    }
    if (e.item.name == 'Shto') {
        hfRuaj.val('Ruaj');
        $('#hfId').val(-1);
        s.GetItemByName('Ruaj').SetVisible(true);
        $.ajax({
            url: Utils.getServerApiUrl("Rregjistrime", "ruajNeSessionTrupInfoArt"),
            data: JSON.stringify({ idkoka: -1 })
        }).done(Succedcallback);
        grid.AddNewRow();
        e.processOnServer = false;
    }
    if (e.item.name == 'Modifiko') {
        hfRuaj.val('Modifiko');
        s.GetItemByName('Ruaj').SetVisible(true);
        indexModifiko = grid.GetFocusedRowIndex();
        $('#hfId').val(grid.GetRowKey(indexModifiko));
        $.ajax({
            url: Utils.getServerApiUrl("Rregjistrime", "ruajNeSessionTrupInfoArt"),
            data: JSON.stringify({ idkoka: grid.GetRowKey(indexModifiko) })
        }).done(Succedcallback);
        grid.StartEditRow(indexModifiko);
        e.processOnServer = false;
    }
    if (e.item.name == 'Ruaj') {
        grid.UpdateEdit();
        e.processOnServer = false;
    }
    ASPxMenu1.AdjustControl();
    myMenu.menuSipasTeDrejtaCeljeVogel(hfRuaj, hfTeDrejta);
};

myMenu.menu_click_celjevogeltree = function (s, e, hfRuaj, tree, hfTeDrejta) {
    if (e.item.name == 'Fshi')
        myMenu.FshiClick(e);
    if (e.item.name == 'Shto') {//per te shtuar bir
        hfRuaj.val('Ruaj');
        ASPxMenu1.GetItemByName('Ruaj').SetVisible(true);
        var prind = tree.GetFocusedNodeKey();
        tree.StartEditNewNode(prind);
        e.processOnServer = false;
    }
    else if (e.item.name == "ShtoPrind") {//per te shtuar prind
        hfRuaj.val('Ruaj');
        ASPxMenu1.GetItemByName('Ruaj').SetVisible(true);
        tree.StartEditNewNode();
        e.processOnServer = false;
    }
    if (e.item.name == 'Modifiko') {
        hfRuaj.val('Modifiko');
        ASPxMenu1.GetItemByName('Ruaj').SetVisible(true);
        indexModifiko = tree.GetFocusedNodeKey();
        tree.StartEdit(indexModifiko);
        e.processOnServer = false;
    }
    if (e.item.name == 'Ruaj') {
        tree.UpdateEdit();
        e.processOnServer = false;
    }
    ASPxMenu1.AdjustControl();
    myMenu.menuSipasTeDrejtaCeljeVogel(hfRuaj, hfTeDrejta);
};

myMenu.ShtoClickRegjistrime = function (e, url) {
    myFaqeCelje.kontrolloTeDrejta(url);
    e.processOnServer = false;
};

myMenu.ShikoClick = function (e, url) {
    if (url.split('id=')[1].split('&')[0] == "null")
        myMesazh.ShtoMesazhGabimi("Nuk keni asnje dokument te zgjedhur!");
    else
        myFaqeCelje.kontrolloTeDrejta(url);
    if (e && e.processOnServer) {
        e.processOnServer = false;
    }
};
myMenu.RiruajClick = function (e, selectrowcount) {
    if (selectrowcount == 0) {
        myMesazh.ShtoMesazhGabimi("Nuk keni asnje dokument te zgjedhur!");
        e.processOnServer = false;
        return;
    }
    Utils.shfaqLoadingGif();
};
myMenu.FshiClick = function (e) {
    if (typeof popFshi !== "undefined") {
        popFshi.Show();
        e.processOnServer = false;
    }
};
myMenu.GrupoClick = function (e) {
    shfaqLupeGrupKontabilizimi();
    e.processOnServer = false;
};
myMenu.ShtoClick = function (e, hfShtimModifikim, hfId, PageControl, konfigurim, indexModifiko, pastroFusha, SuccededCallbackKonfig, resultKonf, colKontrollet, aktivizoFusha, colAtrTrupi) {
    mbush = false;
    hfShtimModifikim.val('shtim'); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
    hfId.val(0); //hidden fieldi qe ruan id  e rreshtit te selektuar
    indexModifiko = -1; //indexi i reshtit te selektuar
    pastroFusha();
    if (konfigurim !== false) {
        SuccededCallbackKonfig(resultKonf);
        if (colAtrTrupi !== undefined)
            aktivizoFusha(colKontrollet, colAtrTrupi, false);
        else
            aktivizoFusha(colKontrollet);
    }
    if (PageControl)
        PageControl.SetActiveTabIndex(1);
    //myMenu.PercaktoMenuSipasTabit(1);
    e.processOnServer = false;
};
//myMenu.ShtoClick = function (e, hfShtimModifikim, hfId, PageControl,konfigurim) {
//    window.mbush = false;
//    hfShtimModifikim.value = "shtim"; //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
//    hfId.value = 0; //hidden fieldi qe ruan id  e rreshtit te selektuar
//    window.indexModifiko = -1; //indexi i reshtit te selektuar
//    pastrofusha();
//    if (konfigurim!==false) {
//        SucceededCallbackKonfigurimiInit(resultkonf);
//        var hf = $('#hfKontrollet')[0];
//        aktivizoFusha(hf.value);
//    }
//    PageControl.SetActiveTabIndex(1);
//    //myMenu.PercaktoMenuSipasTabit(1);
//    e.processOnServer = false;
//}

myMenu.UpdateFilter = function (cmb, colFiltraGrida, filtriDefault) {
    cmb.BeginUpdate();
    cmb.ClearItems();
    for (var i = 0; i < colFiltraGrida.length; i++) {
        cmb.AddItem(colFiltraGrida[i].FiltraKodi);
    }
    if (filtriDefault && filtriDefault != null)
        Utils.SelectComboItem(cmb, filtriDefault.FiltraKodi);
    else
        Utils.SelectComboItem(cmb, "");
    cmb.EndUpdate();
};

myMenu.RuajClick = function (s, e, ruajBuxhetet, PageControl) {
    valido(s, e, PageControl);
    if (ruajBuxhetet)
        ruajBuxhet();
};
myMenu.RuajClickBatch = function (s, e, PageControl, grida, hfStatus) {

    if (valido(s, e, PageControl)) {
        hfStatus.val("ruajtur");
        grida.AddNewRow();
        grida.UpdateEdit();
    }
};
myMenu.ModifikoClick = function (e, hfShtimModifikim) {
    window.kaloTab = true;
    window.lista = true;
    mbushfusha();
    if (hfShtimModifikim)
        hfShtimModifikim.val('modifikim');
    e.processOnServer = false;
};
myMenu.KlonoClick = function (e, hfShtimModifikim, hfId) {
    window.kaloTab = true;
    window.lista = true;
    mbushfusha();
    hfShtimModifikim.val('klonim');
    e.processOnServer = false;
    hfId.val(0);
};
myMenu.KthimClick = function (e, hfShtimModifikim, hfId) {
    window.kaloTab = true;
    window.lista = true;
    mbushfusha();
    hfShtimModifikim.val('kthim');
    e.processOnServer = false;
    hfId.val(0);
};
myMenu.ProspektClick = function (e, hfShtimModifikim, hfId) {
};
myMenu.DraftClickBatch = function (s, e, PageControl, grida, hfStatus) {
    if (valido(s, e, PageControl)) {
        hfStatus.val("draft");
        grida.AddNewRow();
        grida.UpdateEdit();
    }
};
myMenu.PercaktoMenuSipasTabit = function (index, hfTeDrejta, hfShtimModifikim, hapurSiLupe) {
    if (index == 0) {
        ASPxMenu1.GetItemByName('Ruaj').SetVisible(false);
        if (ASPxMenu1.GetItemByName('Draft') != null)
            ASPxMenu1.GetItemByName('Draft').SetVisible(false);
        if (ASPxMenu1.GetItemByName('Modifiko') != null)
            ASPxMenu1.GetItemByName('Modifiko').SetVisible(hapurSiLupe ? false : true);
        if (ASPxMenu1.GetItemByName('ModifikoPrind') != null)
            ASPxMenu1.GetItemByName('ModifikoPrind').SetVisible(hapurSiLupe ? false : true);
        if (ASPxMenu1.GetItemByName('Anullo') != null)
            ASPxMenu1.GetItemByName('Anullo').SetVisible(hapurSiLupe ? true: false);
        if (ASPxMenu1.GetItemByName('Pastro') != null)
            ASPxMenu1.GetItemByName('Pastro').SetVisible(false);
        if (ASPxMenu1.GetItemByName('Shiko') != null)
            ASPxMenu1.GetItemByName('Shiko').SetVisible(false);
        if (ASPxMenu1.GetItemByName('RuajList') != null)
            ASPxMenu1.GetItemByName('RuajList').SetVisible(hapurSiLupe ? false : true);
        if (ASPxMenu1.GetItemByName('Default') != null)
            ASPxMenu1.GetItemByName('Default').SetVisible(true);
        if (ASPxMenu1.GetItemByName('TemplatedItemFilter') != null)
            ASPxMenu1.GetItemByName('TemplatedItemFilter').SetVisible(hapurSiLupe ? false : true);
        if (ASPxMenu1.GetItemByName('TemplatedItemExport') != null)
            ASPxMenu1.GetItemByName('TemplatedItemExport').SetVisible(false);
        if (ASPxMenu1.GetItemByName('OK') != null)
            ASPxMenu1.GetItemByName('OK').SetVisible(true);
        if (ASPxMenu1.GetItemByName('Aktivizo') != null)
            ASPxMenu1.GetItemByName('Aktivizo').SetVisible(hapurSiLupe ? false : true);
        if (ASPxMenu1.GetItemByName('ZgjidhNdermarrje') != null)
            ASPxMenu1.GetItemByName('ZgjidhNdermarrje').SetVisible(false);
        if (ASPxMenu1.GetItemByName('GoldenNumbers') != null)
            ASPxMenu1.GetItemByName('GoldenNumbers').SetVisible(true);
        if (ASPxMenu1.GetItemByName('NormalNumbers') != null)
            ASPxMenu1.GetItemByName('NormalNumbers').SetVisible(true);
        if (ASPxMenu1.GetItemByName('Klono') != null)
            ASPxMenu1.GetItemByName('Klono').SetVisible(hapurSiLupe ? false : true);
        if (ASPxMenu1.GetItemByName('Fshi') != null)
            ASPxMenu1.GetItemByName('Fshi').SetVisible(hapurSiLupe ? false : true);
        if (ASPxMenu1.GetItemByName('Shto') != null)
            ASPxMenu1.GetItemByName('Shto').SetVisible(hapurSiLupe ? false : true);
    }
    else if (index == 1 || index == 2 || index == 3 || index == 4 || index == 5 || index == 6 || index == 7 || index == 8) {
        ASPxMenu1.GetItemByName('Ruaj').SetVisible(true);
        if (hfTeDrejta.Get('Shtim') == true && hfShtimModifikim.val() != 'modifikim' && hfShtimModifikim.val() != 'modifikimPrindi')
            ASPxMenu1.GetItemByName('Ruaj').SetEnabled(true);
        else if (hfTeDrejta.Get('Modifikim') == true && (hfShtimModifikim.val() == 'modifikim' || hfShtimModifikim.val() == 'modifikimPrindi'))
            ASPxMenu1.GetItemByName('Ruaj').SetEnabled(true);
        else
            ASPxMenu1.GetItemByName('Ruaj').SetEnabled(false);
        if (ASPxMenu1.GetItemByName('Modifiko') != null)
            ASPxMenu1.GetItemByName('Modifiko').SetVisible(false);
        if (ASPxMenu1.GetItemByName('ModifikoPrind') != null)
            ASPxMenu1.GetItemByName('ModifikoPrind').SetVisible(false);
        if (ASPxMenu1.GetItemByName('Draft') != null)
            ASPxMenu1.GetItemByName('Draft').SetVisible(true);
        if (ASPxMenu1.GetItemByName('Shiko') != null)
            ASPxMenu1.GetItemByName('Shiko').SetVisible(true);
        if (ASPxMenu1.GetItemByName('RuajList') != null)
            ASPxMenu1.GetItemByName('RuajList').SetVisible(false);
        if (ASPxMenu1.GetItemByName('Default') != null)
            ASPxMenu1.GetItemByName('Default').SetVisible(false);
        if (ASPxMenu1.GetItemByName('TemplatedItemFilter') != null) ASPxMenu1.GetItemByName('TemplatedItemFilter').SetVisible(false);
        if (ASPxMenu1.GetItemByName('TemplatedItemExport') != null && index == 2)
            ASPxMenu1.GetItemByName('TemplatedItemExport').SetVisible(true);
        if (ASPxMenu1.GetItemByName('OK') != null)
            ASPxMenu1.GetItemByName('OK').SetVisible(false);
        if (ASPxMenu1.GetItemByName('Aktivizo') != null)
            ASPxMenu1.GetItemByName('Aktivizo').SetVisible(false);
        if (ASPxMenu1.GetItemByName('GoldenNumbers') != null)
            ASPxMenu1.GetItemByName('GoldenNumbers').SetVisible(false);
        if (ASPxMenu1.GetItemByName('NormalNumbers') != null)
            ASPxMenu1.GetItemByName('NormalNumbers').SetVisible(false);
        if (ASPxMenu1.GetItemByName('Anullo') != null)
            ASPxMenu1.GetItemByName('Anullo').SetVisible(true);
        if (ASPxMenu1.GetItemByName('Pastro') != null)
            ASPxMenu1.GetItemByName('Pastro').SetVisible(true);
    }
    ASPxMenu1.AdjustControl();
};

///funksione per filtrin tek menuja
//BEGIN

myMenu.checkText = function (s, e) {
    if (s.FindItemByText(s.GetInputElement().value) == null) {
        btnRuaj.SetEnabled(true);
        btnFshi.SetEnabled(false);
    }
    else {
        btnRuaj.SetEnabled(false);
        btnFshi.SetEnabled(true);
    }
    e.processOnServer = false;
};
myMenu.aplikoFiltra = function (s, e, grid, komponente, konfigurim) {
    if (s.FindItemByText(s.GetInputElement().value) != null) {
        btnRuaj.SetEnabled(false); btnFshi.SetEnabled(true);
        grid.PerformCallback(komponente + ";" + konfigurim + ";" + s.GetText());
    } else if (s.GetText() == '') {
        btnRuaj.SetEnabled(false);
        btnFshi.SetEnabled(false);
    }
};
myMenu.textChanged = function (s, e) {
    if (s.GetText() == '') {
        btnRuaj.SetEnabled(false);
        btnFshi.SetEnabled(false);
    }
    else if (s.FindItemByText(s.GetInputElement().value) == null) {
        btnRuaj.SetEnabled(true);
        btnFshi.SetEnabled(false);
    }
    else {
        btnRuaj.SetEnabled(false);
        btnFshi.SetEnabled(true);
    }
};

///END

myFaqeCelje.buttonKonfiguroClick = function (s, e, grida, idKomponente, idKonfig, customCustomizationWindow) {
    if (grida.IsCustomizationWindowVisible())
        grida.HideCustomizationWindow();
    else {
        if (customCustomizationWindow) {
            myFaqeCelje.CreateCustomizationWindow(grida, idKomponente, idKonfig);
            return;
        }
        grida.ShowCustomizationWindow();
    }
};

var pageGrids = {
    customizationGrid: null
};

myFaqeCelje.CreateCustomizationWindow = function (grida, idKomponente, idKonfig) {
    $.ajax({
        showLoading: true,
        url: Utils.getServerApiUrl("Konfigurime", "GetGridColumns"),
        data: JSON.stringify({ gridId: grida.globalName, komponenteId: idKomponente, konfigId: idKonfig })
    }).done(
        function (result) {
            var myPopup;
            var saveKonfig = function () {
                pageGrids.customizationGrid.SaveCurrentValues();
                var gridData = pageGrids.customizationGrid.GetData();
                $.ajax({
                    showLoading: true,
                    url: Utils.getServerApiUrl("Konfigurime", "SaveGridColumns"),
                    data: JSON.stringify({ gridColumns: gridData })
                }).done(
                    function (result) {
                        if (result.Status) {
                            myMesazh.ShtoMesazhSuksesi(result.PershkrimMesazhi);
                            location.reload();
                            myPopup.modal('hide');
                        }
                        else {
                            myMesazh.ShtoMesazhGabimi(result.PershkrimMesazhi);
                        }
                    });
            };

            var opsionMbyllje = "Cancel";
            var opsionRuajtje = "Save Changes";
            var titulliModal = "Select Columns";
            var popUpOptions = { prependSelector: "body", dialogClass: "dialog-select-menu", contentClass: "content-select-menu", titulli: titulliModal, text: { mbyll: opsionMbyllje, ruaj: opsionRuajtje }, saveClick: saveKonfig };
            myPopup = Utils.ndertoPopup(popUpOptions);

            $("." + popUpOptions.contentClass).html("");
            $("." + popUpOptions.contentClass).prepend("<div id='data-grid-dashboard'><div id='gvCustomizationGrid' class='noUndoGrida'></div></div>");

            pageGrids.customizationGrid = new myDxDataGrid("gvCustomizationGrid", {
                dataSource: result,
                paging: {
                    enabled: true,
                    pageSize: 10,
                    pageIndex: 0
                },
                scrolling: {
                    mode: "standard"
                },
                focusStateEnabled: false,
                columnResizingMode: "nextColumn",
                editing: {
                    mode: "cell",
                    allowUpdating: true
                },
                showRowLines: true
            });

            var columns = new Array();
            columns.push({ KodiTrupi: "PershkrimiTrupi", PershkrimiTrupi: "Column", ReadonlyTrupi: true, VisibleTrupi: true, TipiFushes: "string" });
            columns.push({ KodiTrupi: "IndexTrupi", PershkrimiTrupi: "Position", ReadonlyTrupi: false, VisibleTrupi: true, TipiFushes: "numeric", sortOrder: "asc" });
            columns.push({ KodiTrupi: "WidthTrupi", PershkrimiTrupi: "Width", ReadonlyTrupi: false, VisibleTrupi: true, TipiFushes: "numeric" });
            columns.push({ KodiTrupi: "VisibleTrupi", PershkrimiTrupi: "Visible", ReadonlyTrupi: false, VisibleTrupi: true, TipiFushes: "boolean" });
            
            pageGrids.customizationGrid.SetColumnsFromConfig(columns);

            myPopup.modal('show');
            myPopup.css('z-index', 2);
            $($('.modal-backdrop')[0]).css('z-index', 1);
        }
    );
};

myFaqeCelje.hapRaporti = function (s, e, grida) {
    var width = $(window).width();
    if (Utils.getUrlVar("shitje_blerje") == "shitje" || Utils.getUrlVar('shitje_blerje') == 'shitjediscount' || Utils.getUrlVar('shitje_blerje') == 'bazaar') {
        if (grida.GetSelectedRowCount() == 0)
            window.location = "Raporti.aspx?emriReal=procedimProdhimi&windowWidth=" + width + "&Filtro=false";
        else {
            //hap raportin te filtruar sipas dokumentave te selektuara ne gride
            e.processOnServer = false;
            grida.GetSelectedFieldValues('IdShitjeKoka;IdNivel', kontrolloNivel);
        }
    }
};

// ben te dukshem ose jo butonin e raportit Porosi Dealer ne ambjentin e shitje/blerje ose te Procedim Prodhimi
myFaqeCelje.shfaqButonRaportPorosiDealerdheProcedimProdhimi= function (s, e, emerRaporti) {
    if (!hfState.Get(emerRaporti.toLowerCase()))
        s.SetVisible(false);
    else
        if (Utils.getUrlVar("shitje_blerje") == "shitje" || Utils.getUrlVar('shitje_blerje') == 'shitjediscount' || Utils.getUrlVar('shitje_blerje') == 'bazaar')
            s.SetVisible(true);
        else s.SetVisible(false);
};

myFaqeCelje.shfaqButonRaportOfertaBlerje = function (s, e, emerRaporti) {
    if (!hfState.Get(emerRaporti.toLowerCase()))
        s.SetVisible(false);
    else if (Utils.getUrlVar("shitje_blerje") == "blerje")
        s.SetVisible(true);
    else s.SetVisible(false);
};

//ky eshte butoni te lista e shitjes qe hap raportin Pagesa Dealer te Vodafone
myFaqeCelje.hapRaportinPagesa = function (s, e, grida) {
    var width = $(window).width();
    if (Utils.getUrlVar("shitje_blerje") == "shitje" || Utils.getUrlVar('shitje_blerje') == 'shitjediscount' || Utils.getUrlVar('shitje_blerje') == 'bazaar') {
        if (grida.GetSelectedRowCount() == 0)
            window.location = "Raporti.aspx?emriReal=porosiDealerVodafone&windowWidth=" + width + "&Filtro=false";
        else {
            //hap raportin te filtruar sipas dokumentave te selektuara ne gride
            e.processOnServer = false;
            grida.GetSelectedFieldValues('IdShitjeKoka;IdNivel', kontrolloNivelPorosiDealer);
        }
    }
};

//ky eshte butoni te lista e shitjeve qe hap raportin e aparateve/kartave/ringarkuesve te shitur/a te Vodafone
myFaqeCelje.hapRaportin = function (s, e, emerRaporti) {
    if (Utils.getUrlVar("shitje_blerje") == "shitje") {
        if (grid_RegDok.GetSelectedRowCount() == 0)
            window.open("RaportiShpejte.aspx?Sesioni=false&emriReal=" + emerRaporti + "&printo=0&scopeID=" + Utils.getUrlVar("scopeID"), "_blank");
        else {
            //hap raportin te filtruar sipas dokumentave te selektuara ne gride
            e.processOnServer = false;
            var sipasID = emerRaporti == "AparateTeShitur" || emerRaporti == "KartaTeShitura" || emerRaporti == "RingarkuesTeShitur";
            var selectedFields = sipasID ? "IdShitjeKoka" : "NrDok";
            console.log(selectedFields);
            grid_RegDok.GetSelectedFieldValues(selectedFields, function (result) {
                var numrat = '';//ruajme id/NrDok dokumentave te selektuara te ndara me '-' qe t'ia kalojme raportit ne url
                for (var i = 0; i < result.length; i++) {
                    if (numrat == '') 
                        numrat = result[i];
                    else 
                        numrat = numrat + '^' + result[i];      
                }
                if (sipasID)
                    window.open("RaportiShpejte.aspx?Sesioni=false&emriReal=" + emerRaporti + "&printo=0&idfaturash=" + numrat + "&scopeID=" + Utils.getUrlVar("scopeID"), "_blank");
                else
                    window.open("RaportiShpejte.aspx?Sesioni=false&emriReal=" + emerRaporti + "&printo=0&nrDok=" + numrat + "&scopeID=" + Utils.getUrlVar("scopeID"), "_blank");
            });
        }
    }
};

//ky eshte butoni te lista e blerjeve qe hap raportin e ofertave te blerjeve javore te Vodafone
myFaqeCelje.hapRaportinOfertaBlerje = function (s, e) {
    var width = $(window).width();
    if (Utils.getUrlVar("shitje_blerje") == "blerje") {
        if (grid_RegDok.GetSelectedRowCount() == 0)
            window.location = "RaportiShpejte.aspx?Sesioni=false&emriReal=ofertBlerje&printo=false&idDokumenti=-1";
        else {
            //hap raportin te filtruar sipas dokumentave te selektuara ne gride
            e.processOnServer = false;
            grid_RegDok.GetSelectedFieldValues('IdShitjeKoka;IdNivel', kontrolloNivelOferteBlerje);
        }
    }
};

myFaqeCelje.hapRaportinShitjeLikujdime = function (s, e) {
    if (Utils.getUrlVar("shitje_blerje") == "shitje") {
        if (grid_RegDok.GetSelectedRowCount() == 0)
            window.open("RaportiShpejte.aspx?Sesioni=false&emriReal=ShitjeDheLikujdimePermbledhese&printo=0&kaFiltra=true&scopeID=" + Utils.getUrlVar("scopeID"), "_blank");
        else {
            //hap raportin te filtruar sipas dokumentave te selektuara ne gride
            e.processOnServer = false;
            grid_RegDok.GetSelectedFieldValues('IdKlientFurnitor', function (result) {
                var idKlient = '';//ruajme id e dokumentave te selektuara te ndara me '-' qe t'ia kalojme raportit ne url
                for (var i = 0; i < result.length; i++) {
                    if (idKlient == '')
                        idKlient = result[i];
                    else
                        idKlient = idKlient + '^' + result[i];
                }
                window.open("RaportiShpejte.aspx?Sesioni=false&emriReal=ShitjeDheLikujdimePermbledhese&printo=0&kaFiltra=true&idKlient=" + idKlient + "&scopeID=" + Utils.getUrlVar("scopeID"), "_blank");
            });
        }
    }
};
myFaqeCelje.hapFaturenEFiskalizuar = function (s, e) {
    if (Utils.getUrlVar("shitje_blerje") == "shitje") {
        if (grid_RegDok.GetSelectedRowCount() == 1) {
            grid_RegDok.GetSelectedFieldValues('IdShitjeKoka', ktheVleratEShitjesPerFiskalizimin);
        }
        else if (grid_RegDok.GetSelectedRowCount() == 0) {
            myMesazh.ShtoMesazhGabimi("Ju lutem, zgjidhni nje fature!");
        }
        else {
            myMesazh.ShtoMesazhGabimi("Ju lutem, zgjidhni vetem nje fature!");
        }
    }
};
myFaqeCelje.hapRaportinShitjeEinvoice = function (s, e) {
    var width = $(window).width();
    if (Utils.getUrlVar("shitje_blerje") == "shitje") {
        window.open("RaportiShpejte.aspx?Sesioni=false&emriReal=FaturaShitjeEinvoice&printo=false&idDokumenti=-1");

    }
};
myFaqeCelje.hapRaportinBlerjeEinvoice = function (s, e) {
    var width = $(window).width();
    if (Utils.getUrlVar("shitje_blerje") == "blerje") {
        window.open("RaportiShpejte.aspx?Sesioni=false&emriReal=FaturaBlerjeEinvoice&printo=false&idDokumenti=-1");

    }
};
//shfaq butonat per raportin e aparateve/kartave/ringarkuesve te shitur/a te Vodafone dhe te ShitjeDheLikujdimePermbledhese
myFaqeCelje.shfaqButonRaporti = function (s, e, emerRaporti) {
    if (!hfState.Get(emerRaporti.toLowerCase()))
        s.SetVisible(false);
    else if (Utils.getUrlVar("shitje_blerje") == "shitje")
        s.SetVisible(true);
    else s.SetVisible(false);
};
myFaqeCelje.shfaqButonRaportiBlerje = function (s, e, emerRaporti) {
    if (!hfState.Get(emerRaporti.toLowerCase()))
        s.SetVisible(false);
    else if (Utils.getUrlVar("shitje_blerje") == "blerje")
        s.SetVisible(true);
    else s.SetVisible(false);
};
myFaqeCelje.shfaqButonFiskalizimi = function (s, e) {
    if (Utils.getUrlVar("shitje_blerje") == "shitje")
        s.SetVisible(true);
    else s.SetVisible(false);
};
myFaqeCelje.InitTeDrejtaKonf = function (s, e) {
    if ($('#hfTeDrejtaKonfGride').val() == 'True')
        s.SetVisible(true);
    else
        s.SetVisible(false);
};

myFaqeCelje.validim = function (s, e) {
    isvalid = ASPxClientEdit.ValidateGroup("entries");
    if (isvalid == false) {
        Utils.hiqLoadingGif();
        e.processOnServer = false;
    }
    return isvalid;
};


function createCookie(name, value, days) {
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        var expires = "; expires=" + date.toGMTString();
    }
    else expires = "";
    document.cookie = name + "=" + value + expires + "; path=/";
}

myFaqeCelje.krijoMenuPerCRM = function (idPerdoruesi, idNdermarrje, idVitNdermarrje, eshteHomePage) {
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "ktheTeDrejtaAmbjenteshPerCRM"),
        data: JSON.stringify({ idPerdoruesi: idPerdoruesi, idNdermarrje: idNdermarrje, idVitNdermarrje: idVitNdermarrje })
    }).done(function (menute) {
        if (!menute)
            return;
        var html = htmlHomePage = "";
        var $ul = $('#ulMenu');
        var $ulHomePage = $('#myHomePage');
        var menuShtese = ['Raport_PivotGrid.aspx', 'Shto_KlientFurnitor.aspx?kf=klient', 'Raportet.aspx?idmod=24'];

        for (var i = 0; i < menute.length; i++) {
            if (eshteHomePage && menute[i].KOMPONEMRI == "CRMDefault.aspx") //Eshte vet Home dhe e shtojme vetem te menuja anash dhe jo te faqja default sepse te ajo jemi
            {
                html += "<li><a href=" + menute[i].KOMPONEMRI + ">" + "<span style='vertical-align:middle' class='fa " + menute[i].IMAGEURL + " fa-2x'></span>" + "&nbsp; " + menute[i].PERSHKRIMKOMPONENTE + "</a></li>";
                continue;
            }

            if ((menute[i].KOMPONEMRI).indexOf(menuShtese[0]) != -1 || (menute[i].KOMPONEMRI).indexOf(menuShtese[1]) != -1 || (menute[i].KOMPONEMRI).indexOf(menuShtese[2]) != -1) {
                html += "<li><a href=" + menute[i].KOMPONEMRI + "&vjenNga=CRM>" + "<span style='vertical-align:middle' class='fa " + menute[i].IMAGEURL + " fa-2x'></span>" + "&nbsp; " + menute[i].PERSHKRIMKOMPONENTE + "</a></li>";
                if (eshteHomePage) {
                    htmlHomePage += "<li class='ui-li-has-thumb'><a class='ui-btn ui-btn-icon-right ui-icon-carat-r' href=" + menute[i].KOMPONEMRI + "&vjenNga=CRM>" + "<img class='ui-li-thumb' src='images/CRM/" + menute[i].IMAGEURL + ".png'>" + "<h2>" + menute[i].PERSHKRIMKOMPONENTE + "</h2></a></li>";
                }
            }
            else {
                if (menute[i].KOMPONEMRI.indexOf("RaportetAllNew") > -1) {
                    menute[i].KOMPONEMRI += ("?width=" + document.body.clientWidth + "&vjenNga=CRM");
                }

                html += "<li><a href=" + menute[i].KOMPONEMRI + ">" + "<span style='vertical-align:middle' class='fa " + menute[i].IMAGEURL + " fa-2x'></span>" + "&nbsp; " + menute[i].PERSHKRIMKOMPONENTE + "</a></li>";

                if (eshteHomePage) {
                    htmlHomePage += "<li class='ui-li-has-thumb'><a class='ui-btn ui-btn-icon-right ui-icon-carat-r' href=" + menute[i].KOMPONEMRI + ">" + "<img class='ui-li-thumb' src='images/CRM/" + menute[i].IMAGEURL + ".png'>" + "<h2>" + menute[i].PERSHKRIMKOMPONENTE + "</h2></a></li>";
                }
            }
        }
        html += "<li><a href=Login_Ndermarrje.aspx><span style='vertical-align:middle' class='fa fa-building-o fa-2x'></span>&nbsp; Ndermarrjet </a></li>";
        html += "<li><a target='_blank' href=FaqeKryesore.aspx?vjenNga=CRM><img style='vertical-align:middle' class='ui-li-thumb' width='28px' height='28px' src='images/CRM/AlphaWebMenu.png'>&nbsp; Alpha Web </a></li>";

        htmlHomePage += "<li class='ui-li-has-thumb'><a class='ui-btn ui-btn-icon-right ui-icon-carat-r' href=Login_Ndermarrje.aspx><img class='ui-li-thumb' src='images/CRM/fa-building-o.png'><h2>Ndermarrjet</h2></a></li>";
        htmlHomePage += "<li class='ui-li-has-thumb'><a target='_blank' class='ui-btn ui-btn-icon-right ui-icon-carat-r' href=FaqeKryesore.aspx?vjenNga=CRM><img class='ui-li-thumb' src='images/CRM/AlphaWeb.png'><h2>Alpha Web</h2></a></li>";

        $ul.html(html);
        $ulHomePage.html(htmlHomePage);
        $("#logout a").attr("href", Paths.defaultLoginPath + "?arsye=logout");
    }).fail(function (e) {
        console.log(e);
    });
};