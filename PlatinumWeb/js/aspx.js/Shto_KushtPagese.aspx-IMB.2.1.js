; var editorValues = new Object();
var identikuesPerPopupKushtePagese = "KushtePagese";
var editorAutorizim;
var grida;
var numerReshtashQeShtohen;
var keyGlobal;
var arr = new Array();
var editorIntervali;
var editorPeriudha;
var editorDite;
var editorZbritje;
var editorKushtPagese;

function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ? evt.keyCode : evt.charCode;
}

/*
Function: changeName

Perdoret per te thirrur nje web service tek faqja prind, per te marre emrin e faqes qe do te shfaqet, si dhe per te ruajtur ne cookie faqen ku ndodhemi
*/
function changeName() {
    myFaqeCelje.shtoHandlerSession();
    window.parent.callWebServiceKtheInfoLart('Shto_KushtPagese.aspx',0);
    window.parent.createCookie('adresa', 'Shto_KushtPagese.aspx', 1);
}

function Init() {
    editorValues["KodiKushtPagese"] = "";
    editorValues["EmertimiKushtPagese"] = "";
    editorValues["LlojiKushtPagese"] = "";
    editorValues["IdNivelAutorizimi"] = "";

    changeName();

    for (i = 0; i < 6; i++)
        arr[i] = new Array();

    numerReshtashQeShtohen = 5;
    shtoVleraFillestareNeMatrice(0);

    var hf = document.getElementById("HiddenField1");
    hf.value = '';
    var hfTrupi = document.getElementById("HiddenFieldTrupi");
    hfTrupi.value = '';
}

function InitAutorizim() {
    var hf = document.getElementById('HiddenField1');

    if (hf.value == '') {
        editorValues["KodiKushtPagese"] = "";
        editorValues["EmertimiKushtPagese"] = "";
        editorValues["LlojiKushtPagese"] = "";
        editorValues["IdNivelAutorizimi"] = "";

        hf.value = editorValues["KodiKushtPagese"] + ";" + editorValues["EmertimiKushtPagese"] + ";"
                + editorValues["LlojiKushtPagese"] + ";" + editorValues["IdNivelAutorizimi"];
    }
    var listeFushash = hf.value.split(';');
    editorAutorizim = Utils.ktheKontroll('IdNivelAutorizimi');
    KodiKushtPagese.SetText(listeFushash[0]);
    EmertimiKushtPagese.SetText(listeFushash[1]);
    LlojiKushtPagese.SetText(listeFushash[2]);
    editorAutorizim.SetText(listeFushash[3]);
}

/*
Function: Autorizimi_Click

Hap lupen e autorizimeve.
*/
function Autorizimi_Click() {
    popupUniversal.SetHeaderText('Zgjidh autorizimet');

    popupUniversal.Show();
}

/*
Function: ProcessTextChanged

Vendos ne hidden field vlerat e editoreve.
*/
function ProcessTextChanged(fieldName, value) {
    editorValues[fieldName] = value;
    var hf = document.getElementById("HiddenField1");
    hf.value = editorValues["KodiKushtPagese"] + ";" + editorValues["EmertimiKushtPagese"] + ";"
                + editorValues["LlojiKushtPagese"] + ";" + editorValues["IdNivelAutorizimi"];
}

/*
Function: KeyPresAutorizimi

Therret funksionin <Autorizimi_Click> per te zgjedhur autorizimin dhe vendos ne hidden field vlerat e editoreve.
*/
function KeyPresAutorizimi(kodi, editor, key) {
    if (kodi == 13) {
        indeksi = key;
        editorAutorizim = Utils.ktheKontroll('IdNivelAutorizimi');
        grida = true;
        Autorizimi_Click();
        editorValues["IdNivelAutorizimi"] = editorAutorizim.GetValue();
        var hf = document.getElementById("HiddenField1");
        hf.value = editorValues["KodiKushtPagese"] + ";" + editorValues["EmertimiKushtPagese"] + ";"
                + editorValues["LlojiKushtPagese"] + ";" + editorValues["IdNivelAutorizimi"];
    }
}

/*
Function: LostFocusAutorizimi

Vendos ne hidden field vlerat e editoreve.
*/
function LostFocusAutorizimi(key) {
    indeksi = key;
    editorAutorizim = Utils.ktheKontroll('IdNivelAutorizimi');
    editorValues["IdNivelAutorizimi"] = editorAutorizim.GetText();
    var hf = document.getElementById("HiddenField1");
    hf.value = editorValues["KodiKushtPagese"] + ";" + editorValues["EmertimiKushtPagese"] + ";"
                + editorValues["LlojiKushtPagese"] + ";" + editorValues["IdNivelAutorizimi"];
}

/*
Function: ButtonClickedAutorizimi

Therret funksionin <Autorizimi_Click> per te zgjedhur autorizimin.
*/
function ButtonClickedAutorizimi(editor, key) {
    indeksi = key;
    editorAutorizim = editor;
    grida = true;
    Autorizimi_Click();
}

/*
Function: TextChangedAutorizimi

Vendos autorizimin e zgjedhur ne editorin e autorizimit dhe vendos ne hidden field vlerat e editoreve.
*/
function TextChangedAutorizimi(key) {
    indeksi = key;
    var a = new Array();
    editorAutorizim = Utils.ktheKontroll('IdNivelAutorizimi');
    a = editorAutorizim.GetText().toString().split(',');
    editorAutorizim.SetText(a[0]);
    editorValues["IdNivelAutorizimi"] = editorAutorizim.GetText();
    var hf = document.getElementById("HiddenField1");
    hf.value = editorValues["KodiKushtPagese"] + ";" + editorValues["EmertimiKushtPagese"] + ";"
                + editorValues["LlojiKushtPagese"] + ";" + editorValues["IdNivelAutorizimi"];
}

/*
Function: boshatisGriden

Fshin vlerat e grides (i ben editoren bosh).
*/
function boshatisGriden() {
    shtoVleraFillestareNeMatrice(0);
    for (i = 0; i < grid_trupi.cpNoRows; i++) {
        editorKushtPagese = document.getElementById('ASPxPageControl1_grid_trupi_cell' + i + '_6_KushtPagese' + i);
        butonKushtPagese = document.getElementById('ASPxPageControl1_grid_trupi_cell' + i + '_6_ButonKushtPagese' + i);
        editorDite = Utils.ktheKontroll('txtDite' + i);
        editorZbritje = Utils.ktheKontroll('txtZbritje' + i);
        editorPeriudha = Utils.ktheKontroll('cmbPeriudha' + i);
        editorIntervali = Utils.ktheKontroll('cmbIntervali' + i);

        editorDite.SetText('');
        editorZbritje.SetText('');
        editorPeriudha.SetText('');
        editorIntervali.SetText('');
        editorKushtPagese.value = '';
        butonKushtPagese.enabled = true;
    }
}

/*
Function: TextChangedPagesa

Ben visible/invisible disa nga fushat e faqes ne varesi te llojit te pageses se zgjedhur.
*/
function TextChangedPagesa() {
    boshatisGriden();
    LlojiKushtPagese.SetText(pagesa_ASPxComboBox.GetText());
    if (pagesa_ASPxComboBox.GetSelectedItem().text == "E plote") {
        lblNdarja.SetVisible(false);
        ndarja_ASPxComboBox.SetVisible(false);
        lblIntervali.SetVisible(false);
        intervali_ASPxComboBox.SetVisible(false);
        lblNrNdarjeve.SetVisible(false);
        nrNdarjeve_TextBox.SetVisible(false);
        lblAfati.SetVisible(true);
        afati_TextBox.SetVisible(true);
    }
    else if (pagesa_ASPxComboBox.GetSelectedItem().text == "Me pjese") {
        lblAfati.SetVisible(false);
        afati_TextBox.SetVisible(false);
        lblNdarja.SetVisible(true);
        ndarja_ASPxComboBox.SetVisible(true);
    }

    ProcessTextChanged('LlojiKushtPagese', pagesa_ASPxComboBox.GetText());
    ndarja_ASPxComboBox.SetText('');
    enable();
}

/*
Function: TextChangedNdarja

Ben visible/invisible disa nga fushat e faqes ne varesi te ndarjes se zgjedhur (kur pagesa eshte me pjese).
*/
function TextChangedNdarja() {
    boshatisGriden();
    if (pagesa_ASPxComboBox.GetSelectedItem().text == "Me pjese") {
        if (ndarja_ASPxComboBox.GetText() == "Perqindje") {
            lblIntervali.SetVisible(false);
            intervali_ASPxComboBox.SetVisible(false);
            lblNrNdarjeve.SetVisible(false);
            nrNdarjeve_TextBox.SetVisible(false);
        }
        else if (ndarja_ASPxComboBox.GetText() == "Interval") {
            lblIntervali.SetVisible(true);
            intervali_ASPxComboBox.SetVisible(true);
            lblNrNdarjeve.SetVisible(true);
            nrNdarjeve_TextBox.SetVisible(true);
        }
    }
    enable();
}

/*
Function: valido

Validon faqen kur kalojme nga nje tab ne tjetrin.
*/
function valido(s, e) {
    var activeTabIndex = PageControl.GetActiveTab().index;
    var tabPageCount = PageControl.GetTabCount();

    for (var i = 1; i < tabPageCount; i++) {
        PageControl.SetActiveTab(PageControl.GetTab(i));
        isvalid = ASPxClientEdit.ValidateGroup("entries");
        if (isvalid == false) {
            e.processOnServer = false;
            PageControl.SetActiveTab(PageControl.GetTab(i));
            break;
        }
        else
            PageControl.SetActiveTab(PageControl.GetTab(activeTabIndex));
    }
}

/*
Function: formoStringPerCallback

Formon nje string me vlerat e array-t qe permban vlerat e editoreve.
*/
function formoStringPerCallback() {
    var str = "";
    for (i = 0; i < arr.length; i++) {
        str = str + arr[i] + ';';
    }
    return str;
}

/*
Function: shtoVleraFillestareNeMatrice

Inicializon arrayn me vlera boshe.
*/
function shtoVleraFillestareNeMatrice(indeksFillimi) {
    for (i = indeksFillimi; i < indeksFillimi + numerReshtashQeShtohen; i++) {
        arr[0][i] = i + ': ';
        arr[1][i] = i + ': ';
        arr[2][i] = i + ': ';
        arr[3][i] = i + ': ';
        arr[4][i] = i + ': ';
    }
}

function _getKeyCode(evt) {
    return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ?
												            evt.keyCode : evt.charCode;
}

/*
Function: TextChangedIntervali

Ben enable/disable disa nga fushat e faqes ne varesi te intervalit te zgjedhur.
*/
function TextChangedIntervali(editor, field, key) {
    keyGlobal = key;
    editorIntervali = editor;
    editorPeriudha = Utils.ktheKontroll('cmbPeriudha' + key);
    editorDite = Utils.ktheKontroll('txtDite' + key);
    if (editorIntervali.GetSelectedItem().text == "Dite") {
        editorPeriudha.ClearItems();
        editorPeriudha.SetEnabled(false);
        editorDite.SetEnabled(true);
    }
    else if (editorIntervali.GetSelectedItem().text == "Jave") {
        editorPeriudha.SetEnabled(true);
        editorDite.SetEnabled(false);
        editorPeriudha.ClearItems();
        editorPeriudha.AddItem('E Hene', 0);
        editorPeriudha.AddItem('E Marte', 1);
        editorPeriudha.AddItem('E Merkure', 2);
        editorPeriudha.AddItem('E Enjte', 3);
        editorPeriudha.AddItem('E Premte', 4);
        editorPeriudha.AddItem('E Shtune', 5);
    }
    else if (editorIntervali.GetSelectedItem().text == "Muaj") {
        editorPeriudha.SetEnabled(true);
        editorPeriudha.ClearItems();
        editorPeriudha.AddItem('Fillim muaji', 0);
        editorPeriudha.AddItem('Mbarim muaji', 1);
        editorPeriudha.AddItem('Dite e caktuar', 2);
    }
    editorPeriudha.SetText('');
    arr[0][key] = key.toString() + ":" + editorIntervali.GetSelectedItem().text;

    if (key == grid_trupi.cpNoRows - 1) {
        shtoVleraFillestareNeMatrice(key + 1);
        grid_trupi.PerformCallback(formoStringPerCallback());
    }
}

/*
Function: TextChangedPeriudha

Ben enable/disable editorin e dites dhe ruan vleren e re te periudhes ne array.
*/
function TextChangedPeriudha(editor, field, key) {
    keyGlobal = key;
    editorPeriudha = editor;
    editorDite = Utils.ktheKontroll('txtDite' + key);
    editorDite.SetEnabled(false);
    if (editorPeriudha.GetSelectedItem().text == "Dite e caktuar") {
        editorDite.SetEnabled(true);
    }
    arr[1][key] = key.toString() + ":" + editorPeriudha.GetSelectedItem().text;
}

/*
Function: LostFocusDite

Kontrollon nese numri i diteve i vendosur eshte i vlefshem dhe ruan numrin e diteve ne array.
*/
function LostFocusDite(editor, field, key) {
    keyGlobal = key;
    var afati = 0;
    if (parseInt(editor.GetText()) < 0) {
        alert('Nr i diteve duhet te jete numer pozitiv');
        editor.SetText('0');
        editor.SetFocus(true);
    }
    if (afati_TextBox.GetText() == "")
        afati = 0;
    else afati = afati_TextBox.GetText();

    var totaliDiteve = 0;
    for (i = 0; i < grid_trupi.cpNoRows; i++) {
        if (Utils.ktheKontroll('txtDite' + i).GetText() != "")

            totaliDiteve = totaliDiteve + parseInt(Utils.ktheKontroll('txtDite' + i).GetText());
    }
    if (parseInt(totaliDiteve) > parseInt(afati)) {
        alert('Numri i diteve nuk mund te jete me i madh se afati!');
        for (i = 0; i < grid_trupi.cpNoRows; i++) {
            if (Utils.ktheKontroll('txtDite' + i).GetText() != "")
                Utils.ktheKontroll('txtDite' + i).SetText(0);
        }
    }
    if (editor.GetText() != '') {
        arr[2][key] = key.toString() + ":" + editor.GetText();
    }
}

/*
Function: LostFocusZbritje

Kontrollon nese zbritja e vendosur eshte e vlefsheme dhe ruan zbritjen ne array.
*/
function LostFocusZbritje(editor, field, key) {
    keyGlobal = key;
    if (parseFloat(editor.GetText()) < 0 || parseFloat(editor.GetText()) > 100) {
        alert('Zbritja duhet te jete numur pozitiv midis 0 dhe 100');
        editor.SetText('0.00');
        editor.SetFocus(true);
    }
    if (editor.GetText() != '') {
        arr[3][key] = key.toString() + ":" + editor.GetText();
    }
}

/*
Function: ButtonClickKushtPagese

Hap lupen e kushteve te pageses.
*/
function ButtonClickKushtPagese(editor, key) {
    editorKushtPagese = document.getElementById(editor);
    keyGlobal = key;
    popupUniversal.SetHeaderText('Zgjidh kushtet e pageses');
    // document.getElementById.src = 'LupaKushtePagese.aspx';
    popupUniversal.Show();
}

/*
Function: TextChangedKushtPagese

Ruan kushtin e pageses ne array.
*/
function TextChangedKushtPagese(editor, key) {
    var value = document.getElementById(editor).value;
    keyGlobal = key;
    arr[4][key] = key.toString() + ":" + value;
}

/*
Function: KeyPress

Hap lupen e kushteve te pageses.
*/
function KeyPress(editor, key) {
    if (event.keyCode == 13) {
        editorKushtPagese = document.getElementById(editor);
        keyGlobal = key;
        popupUniversal.SetHeaderText('Zgjidh kushtet e pageses');
        //  document.getElementById.src = 'LupaKushtePagese.aspx';
        popupUniversal.Show();
        event.returnValue = false;
        event.cancel = true;
    }
}

/*
Function: LostFocusKushtPagese

Ruan kushtin e pageses ne array.
*/
function LostFocusKushtPagese(editor, key) {
    var value = document.getElementById(editor).value;
    keyGlobal = key;

    if (value != '') {
        arr[4][key] = key.toString() + ":" + value;
    }
}

/*
Function: enable

Ben enable/disable disa nga fushat e faqes ne varesi te llojit te pageses se zgjedhur dhe ndarjes.
*/
function enable() {
    for (i = 0; i < grid_trupi.cpNoRows; i++) {
        editorKushtPagese = document.getElementById('ASPxPageControl1_grid_trupi_cell' + i + '_6_KushtPagese' + i);
        butonKushtPagese = document.getElementById('ASPxPageControl1_grid_trupi_cell' + i + '_6_ButonKushtPagese' + i);
        editorDite = Utils.ktheKontroll('txtDite' + i);
        editorZbritje = Utils.ktheKontroll('txtZbritje' + i);
        editorPeriudha = Utils.ktheKontroll('cmbPeriudha' + i);
        editorIntervali = Utils.ktheKontroll('cmbIntervali' + i);

        if (pagesa_ASPxComboBox.GetSelectedItem().text == "E plote") {
            editorDite.SetEnabled(true);
            editorZbritje.SetEnabled(true);
            editorPeriudha.SetEnabled(true);
            editorIntervali.SetEnabled(true);
            editorKushtPagese.disabled = true;
            butonKushtPagese.disabled = true;
        }
        else if (pagesa_ASPxComboBox.GetSelectedItem().text == "Me pjese") {
            if (ndarja_ASPxComboBox.GetText() == "Perqindje") {
                editorDite.SetEnabled(false);
                editorZbritje.SetEnabled(true);
                editorPeriudha.SetEnabled(false);
                editorIntervali.SetEnabled(false);
                editorKushtPagese.disabled = false;
                butonKushtPagese.disabled = false;
            }
            else if (ndarja_ASPxComboBox.GetText() == "Interval") {
                editorDite.SetEnabled(false);
                editorZbritje.SetEnabled(false);
                editorPeriudha.SetEnabled(true);
                editorIntervali.SetEnabled(true);
                editorKushtPagese.disabled = true;
                butonKushtPagese.disabled = true;
            }

        }
    }
}

/*
Function: pastro

Fshin llojin e zgjedhur te pageses dhe therret funksionin <shtoVleraFillestareNeMatrice> per te boshatisur dhe array-n e vlerave.
*/
function pastro() {
    pagesa_ASPxComboBox.SetText('');
    shtoVleraFillestareNeMatrice(0);
}

/*
Function: isValid

Kontrollon nese koleksioni i kushteve te pageses eshte i sakte(a jane plotesuar te gjitha vlerat e detyrueshme)
*/
function isValid(vlerat) {
    if (keyGlobal < 0 || keyGlobal == undefined)
        return false;
    else {
        if (pagesa_ASPxComboBox.GetSelectedItem().text == "Me pjese") {
            if (ndarja_ASPxComboBox.GetText() == "Perqindje") {
                for (i = 0; i <= keyGlobal; i++)
                    if (arr[4][i] == i + ": ")// arr[3][i] == i + ": " ||
                        return false;
                    else
                        return true;
            }
            else if (ndarja_ASPxComboBox.GetText() == "Interval") {
                for (i = 0; i <= keyGlobal; i++)
                    if (arr[0][i] == i + ": " || arr[1][i] == i + ": ")
                        return false;
                    else
                        return true;
            }
        }
        else if (pagesa_ASPxComboBox.GetSelectedItem().text == "E plote") {
            for (i = 0; i <= keyGlobal; i++)
                if (arr[0][i] == i + ": ")// || arr[3][i] == i + ": "
                    return false;
                else
                    return true;
        }
        else return true;
    }
}

/*
Function: RuajClick

Therret funksionin <formoStringPerCallback> per te formuar stringun me vlera qe do perdoren per ruajtje.
*/
function RuajClick(s, e) {
    var hfTrupi = document.getElementById("HiddenFieldTrupi");
    if (isValid(arr) == true) {
        arr[5] = 'ruaj';
        hfTrupi.value = formoStringPerCallback();
    }
    else {
        hfTrupi.value = 'Gabim';
        alert('Plotesoni te gjitha fushat');
        e.processOnServer = false;
    }
}

/*
Function: RuajOverview_Click

Therret funksionin <formoStringPerCallback> per te formuar stringun me vlera qe do perdoren per ruajtje.
*/
function RuajOverview_Click(s, e) {
    var hfTrupi = document.getElementById("HiddenFieldTrupi");
    if (isValid(arr) == true) {
        arr[5] = 'ruaj';
        hfTrupi.value = formoStringPerCallback();
    }
    else {
        hfTrupi.value = 'Gabim';
        alert('Plotesoni trupin');
        e.processOnServer = false;
        //PageControl.SetActiveTab(PageControl.GetTab(2))
    }
}
function TextChanged_kodi_TextBox(s, e) {
    kodi1_TextBox.SetText(kodi_TextBox.GetText()); 
    KodiKushtPagese.SetText(kodi_TextBox.GetText());
    ProcessTextChanged('KodiKushtPagese', kodi_TextBox.GetText()) ;
}
function TextChanged_emertimi_TextBox(s, e) {
    EmertimiKushtPagese.SetText(emertimi_TextBox.GetText());
    emertimi1_TextBox.SetText(emertimi_TextBox.GetText());
    ProcessTextChanged('EmertimiKushtPagese', emertimi_TextBox.GetText()) ;
}
function Lost_Focus(s, e) {
    editorAutorizimi = Utils.ktheKontroll('IdNivelAutorizimi');
    editorAutorizimi.SetText(txtAutorizimi.GetText());
    ProcessTextChanged('IdNivelAutorizimi',txtAutorizimi.GetText() );  
}
function TextChanged_kodi1_TextBox(s, e) {
    KodiKushtPagese.SetText(kodi1_TextBox.GetText());
    kodi_TextBox.SetText(kodi1_TextBox.GetText());
    ProcessTextChanged('KodiKushtPagese', kodi1_TextBox.GetText()) ;
}
function TextChanged_emertimi1_TextBox(s, e) {
    EmertimiKushtPagese.SetText(emertimi1_TextBox.GetText());
    emertimi_TextBox.SetText(emertimi1_TextBox.GetText());
    ProcessTextChanged('EmertimiKushtPagese', emertimi1_TextBox.GetText()) ;
}

function Active_TabChanged(s, e) {
     var hf = document.getElementById('HiddenField1');	                   
    var listeFushash = hf.value.split(';');
    if (e.tab.index == 1)
    {    
        if (listeFushash[0]!='' && listeFushash[0]!=undefined && listeFushash[0]!='undefined') 
        {                                                      
            kodi_TextBox.SetText (listeFushash[0]);
            kodi1_TextBox.SetText (listeFushash[0]);
            KodiKushtPagese.SetText(listeFushash[0]);
        }
                          
        if (listeFushash[1]!='' && listeFushash[1]!=undefined && listeFushash[1]!='undefined')     
        { 
            emertimi_TextBox.SetText (listeFushash[1]);
            emertimi1_TextBox.SetText (listeFushash[1]);
            EmertimiKushtPagese.SetText(listeFushash[1]);
        }
                            
        if (listeFushash[2]!='' && listeFushash[2]!=undefined && listeFushash[2]!='undefined')
        {    
            pagesa_ASPxComboBox.SetText (listeFushash[2]);
            if (pagesa_ASPxComboBox.GetText() == 'E plote') 
            {
                lblNdarja.SetVisible(false);
                ndarja_ASPxComboBox.SetVisible(false);
                lblIntervali.SetVisible(false);
                intervali_ASPxComboBox.SetVisible(false);
                lblNrNdarjeve.SetVisible(false);
                nrNdarjeve_TextBox.SetVisible(false);
                lblAfati.SetVisible(true);
                afati_TextBox.SetVisible(true);
            }
            else if (pagesa_ASPxComboBox.GetText() == 'Me pjese')
            {
                lblAfati.SetVisible(false);
                afati_TextBox.SetVisible(false);
                lblNdarja.SetVisible(true);
                ndarja_ASPxComboBox.SetVisible(true);
            }
            enable();
        }
        if (listeFushash[3]!='' && listeFushash[3]!=undefined && listeFushash[3]!='undefined')                  
            txtAutorizimi.SetText (listeFushash[3]);                                                     
    }
    else if  (e.tab.index == 2)
    { 
        if (listeFushash[2] == '')
        {
            PageControl.SetActiveTab(PageControl.GetTab(1));
            alert('Zgjidhni llojin e pageses');
        }
             
        if (listeFushash[0]!='' && listeFushash[0]!=undefined && listeFushash[0]!='undefined') 
        {                                                      
            kodi_TextBox.SetText (listeFushash[0]);
            kodi1_TextBox.SetText (listeFushash[0]);
            KodiKushtPagese.SetText(listeFushash[0]);
        }
              
        if (listeFushash[1]!='' && listeFushash[1]!=undefined && listeFushash[1]!='undefined')     
        { 
            emertimi_TextBox.SetText (listeFushash[1]);
            emertimi1_TextBox.SetText (listeFushash[1]);
            EmertimiKushtPagese.SetText(listeFushash[1]);
        }
                    
        if (listeFushash[4]!='' && listeFushash[4]!=undefined && listeFushash[4]!='undefined')                
            afati_TextBox.SetText (listeFushash[4]);
        if (listeFushash[5]!='' && listeFushash[5]!=undefined && listeFushash[5]!='undefined')                
            ndarja_ASPxComboBox.SetText (listeFushash[5]);
        if (listeFushash[6]!='' && listeFushash[6]!=undefined && listeFushash[6]!='undefined')                
            intervali_ASPxComboBox.SetText (listeFushash[6]);
        if (listeFushash[7]!='' && listeFushash[7]!=undefined && listeFushash[7]!='undefined')                
            NrNdarjeve_TextBox.SetText (listeFushash[7]);
                                           
    }}