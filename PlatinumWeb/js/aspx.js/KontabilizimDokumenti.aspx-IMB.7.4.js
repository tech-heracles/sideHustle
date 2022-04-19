;

$(document).ready(function () {
    $(document).keydown(function (e) {
        switch (e.which) {
            case 13:
                e.preventDefault();
                break;
        }
    });
    changeName();
});


function checkText(s, e) {
    myMenu.checkText(s, e);
}
var btnFiltrat;
function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvKontabilizimDokumenti, "623", '');
}

function textChanged(s, e) {
    myMenu.textChanged(s, e);
}

function BeginCallback(s, e) {
    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}

function EndCallback(s,e)
{
    Utils.hiqLoadingGif();
}

function initNgaDok(s, e) {
    //$(s.GetInputElement()).css('zIndex', 3000);
}

function initDateRegjistrimi(s, e) {
    var dataSot = Utils.zeroOren(Utils.ktheDateServeriFromCookies());
    txtDateRegjistrimi.SetDate(dataSot);
}

function ngaDokDateChanged(s, e) {

    if (txtDeriDok.GetDate() < txtNgaDok.GetDate())
        txtNgaDok.SetDate(txtDeriDok.GetDate());
    else
        txtNgaDok.SetDate(txtNgaDok.GetDate());
}

function deriDokDateChanged(s, e) {
    txtDeriDok.SetDate(txtDeriDok.GetDate());
}

function changeName() {
    var hf = $("#hfKonffillestar")[0];
    myFaqeCelje.changeName('KontabilizimDokumenti.aspx', 0, hf);
}

function menu_click(s, e) {
    switch (e.item.name) {
        case "Kontabilizo":
            Utils.shfaqLoadingGif();
            break;
        case "Anullo":
            e.processOnServer = false;
            window.parent.popupUniversal.Hide();
            break;

    }
}



function ButtonClickedLlojDok(editor) {
    var headerText = "Zgjidh llojin e dokumentit"; //shtohet te string
    var contentUrl = 'LupaKonfigurime.aspx?idKonfigAmbjente=30933&vjenNgaRaporti=true';
            
    editorGlobal = editor;
    identikuesPerPopupLlojDokumenti = "raportllojdokumenti";
    myButtonClickLupa.LupaUniversal_Click(headerText, contentUrl, '900', '700');
}

function ButtonClickedBtnePerdoruesi(editor) {
    var headerText = "Zgjidh Perdoruesin";
    var contentUrl = 'LupaPerdorues.aspx?vjenNgaRaporti=false';
    var widthLupa = '900';
    var heightLupa = '600';
    editorGlobal = editor;
    identikuesPerPopupPerdoruesi = "raporti";
    myButtonClickLupa.LupaUniversal_Click(headerText, contentUrl, widthLupa, heightLupa);
}

//metoda per te shfaqur popupin e filtrave
function Filtra_Click() {
    document.getElementById('<%= Container.ClientID %>').src = 'LupaFiltra.aspx?grida=gvKontabilizimDokumenti&page=KontabilizimDokumenti.aspx&idKonfigAmbjente=577';
    popFiltra.Show();
}

function kerko() {
    gvKontabilizimDokumenti.PerformCallback("kerko");
}

$(window).load(function () {
    try {
        gvKontabilizimDokumenti.SetFocusedRowIndex(0);
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvKontabilizimDokumenti.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
});
$(window).bind('resize', function () {//po

    try {
        panel.SetWidth(document.documentElement.clientWidth - 20);
        gvKontabilizimDokumenti.SetWidth(document.documentElement.clientWidth - 50);
    }
    catch (e) {
    }
}).trigger('resize');


function ndryshoKonfiguriminInit() {
 
}
