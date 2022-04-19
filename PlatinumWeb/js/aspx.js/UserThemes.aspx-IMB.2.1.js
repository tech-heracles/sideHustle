;
var btnFiltrat;
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
var ku;
// ky variabel mban emrin e dataview ku eshte check-uar theme.
var veprimiCbFrames = false;
// ky variabel mban veprimin nese eshte checkuar apo jo checkboxi tek dataview per temen e frameve te faqes kryesore.
var veprimiCbAmbjKr = false;
// ky variabel mban veprimin nese eshte checkuar apo jo checkboxi tek dataview per temen e ambjentit kryesor.
var veprimiCbJQuery = false;
// ky variabel mban veprimin nese eshte checkuar apo jo checkboxi tek dataview per temen tek gridat JQuery.
var veprimiCbBgImg = false;
//ky variabel ruan veprimin nese eshte checkuar apo jo checkboxi tek dataview me imazhet.

var ruaj = false;

$(document).ready(function () {
    $(document).keydown(function (e) {
        switch (e.which) {
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
    });
    changeName();
});

function changeName() {
    myFaqeCelje.changeName('UserThemes.aspx',0, null);
    myMenu.PercaktoMenuSipasTabit(PageControl.GetActiveTabIndex(), hfTeDrejta, $('#hfShtimModifikim'));
}

function OnGridSelectionChanged(s, e) {
    indexSel = myMenu.JSlevizNeGride.OnGridSelectionChanged(e, indexSel);
}

function textChanged(s, e) {
    myMenu.textChanged(s, e);
}

function checkText(s, e) {
    myMenu.checkText(s, e);
}

//nuk perdoret me
function apliko(s, e, ruajap) {
    ruaj = ruajap;
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "KthePathinEThemit"),
        data: JSON.stringify({ input: s.GetMainElement().title })
    }).done(SucceededCallback);
}

//nuk perdoret me
function SucceededCallback(result) {
    window.parent.backDiv.style.backgroundImage = "url(" + result.split(':')[0] + ")";
    $('#hfIdBgImagePrev')[0].value = result.split(':')[1];
    if (ruaj) {
        Utils.shfaqLoadingGif();;
        btn.DoClick();
    }
}

function BeginCallback(s, e) {
    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}

function gridFocusRowCanged(s, e) {
    mbush = true;
}

function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvThemesAmbjente, "161", '');
}

var hapPreview = false;
var klonim = false;

function menu_click(s, e) {
    if (e.item.name == 'Shto') {
        DataThemes.PerformCallback();
        txtKodi.SetEnabled(true);
        enableControls('enable');
    }
    if (e.item.name == 'Ruaj') {
        if (cbZgjedhur.GetChecked())
            $.ajax({
                url: Utils.getServerApiUrl("Rregjistrime", "KthePathinEThemit"),
                data: JSON.stringify({ input: $('#hfBgImage')[0].value })
            }).done(aplikoBackground);
    }
    if (e.item.name == 'Klono') {
        klonim = true;
    }
    if (e.item.name == 'ZgjidhTheme') {
        //$('#hfShtimModifikim')[0].value = "";
        //            if (gvThemesAmbjente.GetFocusedRowIndex() == -1)
        //                    myMesazh.ShtoMesazhGabimi('Duhet te zgjidhni nje theme!');
        //            else gvThemesAmbjente.GetRowValues(gvThemesAmbjente.GetFocusedRowIndex(), 'IdThemeAmbjente;KodTheme', merrTeDhenaThemeSelektuar);
        //mbushfusha();
    }
    if (e.item.name == 'Preview') {
        e.processOnServer = false;
        if (PageControl.GetActiveTabIndex() != 0) {
            popPreview.SetContentUrl('PreviewThemeAmbjente.aspx?ThemeAmbjKr=' + $('#hfThemeAmbjKr')[0].value + '&ThemeFramet=' + $('#hfThemeFramet')[0].value + '&ThemeJQuery=' + $('#hfThemeJQuery')[0].value + '&BackImg=' + $('#hfBgImage')[0].value);
            popPreview.Show();
        }
        else {
            hapPreview = true;
            if (gvThemesAmbjente.GetFocusedRowIndex() == -1)
                myMesazh.ShtoMesazhGabimi('Duhet te zgjidhni nje motiv!');
            else
                gvThemesAmbjente.GetRowValues(gvThemesAmbjente.GetFocusedRowIndex(), 'IdThemeAmbjente;KodTheme', merrTeDhenaThemeSelektuar);
        }
    }
    var hfShtimModifikim = $('#hfShtimModifikim'); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
    var hfId = $('#hfId'); //hidden fieldi qe ruan id  e rreshtit te selektuar
    var ruajbuxhetet = false; //i here per i here
    myMenu.menu_click(s, e, hfShtimModifikim, hfId, PageControl, ruajbuxhetet, false, indexModifiko, pastrofusha, null, null, undefined, undefined, undefined);
}

//funksioni qe aplikon backgroundin nqs ruhet tema e zgjedhur
function aplikoBackground(result) {
    var pathi = result.split(':')[0];
    if (pathi != null && pathi != "")
        window.parent.backDiv.style.backgroundImage = "url(" + pathi + ")";
    else window.parent.backDiv.style.backgroundImage = "url(images/backgrounds/imagesJ105.jpg)";
}

function EndRequestHandler(sender, args) {
    var hf = $("#hfStatusi");
    var hfShtimModifikim = $('#hfShtimModifikim'); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
    var hfId = $('#hfId');  //hidden fieldi qe ruan id  e rreshtit te selektuar
    indexModifiko = myFaqeCelje.EndRequestHandlerPas(sender, args, hf, hfShtimModifikim, hfId, indexModifiko, PageControl, gvThemesAmbjente, "161", hfTeDrejta)
    myMenu.PercaktoMenuSipasTabit(PageControl.GetActiveTabIndex(), hfTeDrejta, $('#hfShtimModifikim'));
}

function valido(s, e) {
    var activeTabIndex = PageControl.GetActiveTab().index;
    for (var i = 1; i < 2; i++) {
        PageControl.SetActiveTab(PageControl.GetTab(i));
        var isvalid = ASPxClientEdit.ValidateGroup("entries");
        if (isvalid === false) {
            e.processOnServer = false;
            PageControl.SetActiveTab(PageControl.GetTab(i));
            Utils.hiqLoadingGif();;
            myMenu.PercaktoMenuSipasTabit(i, hfTeDrejta, $('#hfShtimModifikim'));
            break;
        }
        else
            PageControl.SetActiveTab(PageControl.GetTab(activeTabIndex), hfTeDrejta, $('#hfShtimModifikim'));
    }
}

function tabsActiveTabChanged(s, e) {
    indexModifiko = gvThemesAmbjente.GetFocusedRowIndex();
    if (mbush) {
        if (indexModifiko !== -1) {
            OnGridDoubleClick(indexModifiko);
        }
        else {
            mbush = false;
            $('#hfShtimModifikim')[0].value = 'shtim';
            $('#hfId')[0].value = 0;
            pastrofusha();
        }
    }
    kaloTab = false;
    myMenu.PercaktoMenuSipasTabit(e.tab.index, hfTeDrejta, $('#hfShtimModifikim'));
    if (e.tab.index != 0)
        ASPxMenu1.GetItemByName('ZgjidhTheme').SetVisible(false);
    else ASPxMenu1.GetItemByName('ZgjidhTheme').SetVisible(true);
}

function endCallBackDataViewThemes(s, e) {
    if ($('#hfShtimModifikim')[0].value != 'shtim') {
        $.ajax({
            url: Utils.getServerApiUrl("Rregjistrime", "ktheThemeAmbjenteSipasId"),
            data: JSON.stringify({ id: $('#hfId')[0].value })
        }).done(checkBgImage);
    }
}

function checkBgImage(result) {
    var tema = result;
    if (result.IdBgImage != 0) ASPxClientControl.GetControlCollection().GetByName(result.IdBgImage + 'bgImg').SetChecked(true);
    if (result.DefaultTheme == true && klonim == false) {
        var collection = ASPxClientControl.GetControlCollection();
        for (var key in collection.elements) {
            var control = collection.elements[key];
            if (control != null && control != undefined && control.name.indexOf('DataThemes') != -1 && (control.name.indexOf('cbZgjidh') != -1 || control.name.indexOf('ImgTheme') != -1 || control.name.indexOf('imgCover') != -1))
                control.SetEnabled(false);
        }
    }
}
var eshteThemeDefault;

function OnGridDoubleClick(index) {
    indexModifiko = index;
    lista = true;
    mbushfusha();
}

function pastrofusha() {
    txtKodi.SetText('');
    txtPershkrimi.SetText('');
    cbDefault.SetChecked(false);
    cbZgjedhur.SetChecked(false);
    var collection = ASPxClientControl.GetControlCollection();
    for (var key in collection.elements) {
        var control = collection.elements[key];
        if (control != null && control.name.indexOf('cbZgjidh') != -1)
            control.SetChecked(false);
    }
}

function mbushfusha() {
    mbush = false;
    $('#hfShtimModifikim')[0].value = "modifikim";
    indexModifiko = gvThemesAmbjente.GetFocusedRowIndex();
    if (indexModifiko == -1)
        myMesazh.ShtoMesazhGabimi('Duhet te zgjidhni nje theme!');
    else
        gvThemesAmbjente.GetRowValues(indexModifiko, 'IdThemeAmbjente;KodTheme;PershkrimTheme;DefaultTheme;Zgjedhur;IdThemeFrames;IdThemeFrameKryesor;IdThemeJQuery;IdBgImage;IdPerdorues;IdStatusDok;DtKrijimi;DtModifikimi', OnGetRowValuesMod);
}

function OnGetRowValuesMod(values) {
    if ($('#hfShtimModifikim').val() == "shtim")
        return;
    $('#hfId')[0].value = values[0];
    hapPreview = false;
    merrTeDhenaThemeSelektuar(values);
    pastrofusha();
    txtKodi.SetText(values[1]);
    txtPershkrimi.SetText(values[2]);
    cbDefault.SetChecked(values[3]);
    cbZgjedhur.SetChecked(values[4]);
    if (values[5] != null && values[5] != 0) ASPxClientControl.GetControlCollection().GetByName(values[5] + 'framet').SetChecked(true);
    if (values[6] != null && values[6] != 0) ASPxClientControl.GetControlCollection().GetByName(values[6] + 'ambjKr').SetChecked(true);
    if (values[7] != null && values[7] != 0) ASPxClientControl.GetControlCollection().GetByName(values[7] + 'jQuery').SetChecked(true);
    DataThemes.PerformCallback();
    //if (values[8] != null && values[8] != 0) ASPxClientControl.GetControlCollection().GetByName(values[8] + 'bgImg').SetChecked(true);

    if (values[3] == true && klonim == false) {
        enableControls('disable');
    }
    else {
        if (klonim == true) {
            cbDefault.SetChecked(false);
            txtKodi.SetEnabled(true);
        }
        else
            txtKodi.SetEnabled(false);
        enableControls('enable');
    }

    if (kaloTab) {
        PageControl.SetActiveTabIndex(1);
        myMenu.PercaktoMenuSipasTabit(PageControl.GetActiveTabIndex(), hfTeDrejta, $('#hfShtimModifikim'));
    }
}

function enableControls(veprimi) {
    var collection = ASPxClientControl.GetControlCollection();
    cbDefault.SetEnabled(false);
    cbZgjedhur.SetEnabled(false);

    if (veprimi == 'disable') {
        txtKodi.SetEnabled(false);
        txtPershkrimi.SetEnabled(false);
        for (var key in collection.elements) {
            var control = collection.elements[key];
            if (control != null && control != undefined && (control.name.indexOf('dataView') != -1 || control.name.indexOf('DataThemes') != -1) && (control.name.indexOf('cbZgjidh') != -1 || control.name.indexOf('ImgTheme') != -1 || control.name.indexOf('imgCover') != -1))
                control.SetEnabled(false);
        }
    }
    else if (veprimi == 'enable') {
        txtPershkrimi.SetEnabled(true);
        for (var key in collection.elements) {
            var control = collection.elements[key];
            if (control != null && control != undefined && (control.name.indexOf('dataView') != -1 || control.name.indexOf('DataThemes') != -1) && (control.name.indexOf('cbZgjidh') != -1 || control.name.indexOf('ImgTheme') != -1 || control.name.indexOf('imgCover') != -1))
                control.SetEnabled(true);
        }
    }
}

//funksioni qe merr te dhenat e temes se selektuar ne gride
function merrTeDhenaThemeSelektuar(vlerat) {
    var id = vlerat[0];
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "KtheEmratThemeSelektuar"),
        data: JSON.stringify({ id: id })
    }).done(plotesoVleraHf);
}

//ky funksion mbush hidden fieldet me vlerat fillestare te temes se selektuar ne gride
function plotesoVleraHf(result) {
    if (result[0] != null)
        $('#hfThemeFramet')[0].value = result[0];
    else $('#hfThemeFramet')[0].value = "";

    if (result[1] != null)
        $('#hfThemeAmbjKr')[0].value = result[1];
    else $('#hfThemeAmbjKr')[0].value = "";

    if (result[2] != null)
        $('#hfThemeJQuery')[0].value = result[2];
    else $('#hfThemeJQuery')[0].value = "";

    if (result[3] != null)
        $('#hfBgImage')[0].value = result[3];
    else $('#hfBgImage')[0].value = "";

    $('#hfId')[0].value = result[4];
    if (hapPreview == true) {
        popPreview.SetContentUrl('PreviewThemeAmbjente.aspx?ThemeFramet=' + $('#hfThemeFramet')[0].value + '&ThemeAmbjKr=' + $('#hfThemeAmbjKr')[0].value + '&ThemeJQuery=' + $('#hfThemeJQuery')[0].value + '&BackImg=' + $('#hfBgImage')[0].value);
        popPreview.Show();
    }
}
var emriTheme;
function checkUncheckCheckboxin(s, e, ku) {
    emriTheme = s.GetMainElement().title;
    if (ku == 'bgImg') {
        $.ajax({
            url: Utils.getServerApiUrl("Rregjistrime", "KtheIDThemeBgImg"),
            data: JSON.stringify({ input: emriTheme })
        }).done(checkoCheckBoxin);
    }
    else if (ku == 'framet' || ku == 'ambjKr' || ku == 'jQuery') {
        $.ajax({
            url: Utils.getServerApiUrl("Rregjistrime", "KtheIDThemeDevExJQuery"),
            data: JSON.stringify({ input: emriTheme })
        }).done(checkoCheckBoxin);
    }
}

function checkoCheckBoxin(idTheme) {
    var checkuar = ASPxClientControl.GetControlCollection().GetByName(idTheme + ku).GetChecked();
    var theme;
    if (ku == 'framet') {
        theme = $('#hfThemeFramet');
        veprimiCbFrames = !checkuar;
    }
    else if (ku == 'ambjKr') {
        theme = $('#hfThemeAmbjKr');
        veprimiCbAmbjKr = !checkuar;
    }
    else if (ku == 'jQuery') {
        theme = $('#hfThemeJQuery');
        veprimiCbJQuery = !checkuar;
    }
    else if (ku == 'bgImg') {
        theme = $('#hfBgImage');
        veprimiCbBgImg = !checkuar;
    }

    if (checkuar == false)
        theme[0].value = emriTheme;
    else if (checkuar == true) {
        theme[0].value = "";
    }
    uncheckOthers(idTheme);
}

/*
Function: zgjidhThemes

Perdoret per te marre emrin e temes se zgjedhur brenda nje dataview te caktuar. 
Therret dhe funksionin uncheckOthers, per te c'selektuar checkboxet e tjera brenda te njejtes dataview.

Parameters: 

ku -> ruan dataview ku eshte selektuar theme

e-eventi
*/
function zgjidhThemes(s, e, ku) {
    if (ku == 'framet') {
        var theme = $('#hfThemeFramet');
        veprimiCbFrames = s.GetChecked();
    }
    else if (ku == 'ambjKr') {
        var theme = $('#hfThemeAmbjKr');
        veprimiCbAmbjKr = s.GetChecked();
    }
    else if (ku == 'jQuery') {
        var theme = $('#hfThemeJQuery');
        veprimiCbJQuery = s.GetChecked();
    }
    else if (ku == 'bgImg') {
        var theme = $('#hfBgImage');
        veprimiCbBgImg = s.GetChecked();
    }

    var emerTheme = s.GetMainElement().title;
    if (s.GetChecked() == true) {
        theme[0].value = emerTheme;
        if (ku == 'bgImg') {
            $.ajax({
                url: Utils.getServerApiUrl("Rregjistrime", "KtheIDThemeBgImg"),
                data: JSON.stringify({ input: emerTheme })
            }).done(uncheckOthers);
        }
        else
            $.ajax({
            url: Utils.getServerApiUrl("Rregjistrime", "KtheIDThemeDevExJQuery"),
            data: JSON.stringify({ input: emerTheme })
        }).done(uncheckOthers);
    }
    else if (s.GetChecked() == false) {
        theme[0].value = "";
    }
}

/*
Function: uncheckOthers

Perdoret per te c'selektuar checkboxet e tjera brenda nje dataview

Parameters: 
idThemeResult -> eshte id e temes qe ka selektuar perdoruesi.         
*/
var veprimiCb;
function uncheckOthers(idThemeResult) {
    var collection = ASPxClientControl.GetControlCollection();
    var dataview;
    if (ku == 'framet') {
        dataview = 'dataViewFrames';
        veprimiCb = veprimiCbFrames;
    }
    else if (ku == 'ambjKr') {
        dataview = 'dataViewAmbjKryesor';
        veprimiCb = veprimiCbAmbjKr;
    }
    else if (ku == 'jQuery') {
        dataview = 'dataViewJQuery';
        veprimiCb = veprimiCbJQuery;
    }
    else if (ku == 'bgImg') {
        dataview = 'DataThemes';
        veprimiCb = veprimiCbBgImg;
    }

    for (var key in collection.elements) {
        var control = collection.elements[key];
        if (control != null && control != undefined && control.name.indexOf(dataview) != -1 && control.name.indexOf('cbZgjidh') != -1)
            control.SetChecked(false);
    }

    if (veprimiCb == true)
        ASPxClientControl.GetControlCollection().GetByName(idThemeResult + ku).SetChecked(true);
    else
        ASPxClientControl.GetControlCollection().GetByName(idThemeResult + ku).SetChecked(false);
}
