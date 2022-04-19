;
var gridaTrupi;

jQuery(document).ready(function () {
    $(window).on('load', function () {
        Init();
    });  
});

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
            break;
        default:
            break;
    }  
});


function Init() {
    changeName();
    bejGatiGride();
}

function changeName() {
    myFaqeCelje.changeName('LogeSistemi.aspx', 0, null);
}


//Lidhja e komponenteve me ndermarrjet
function bejGatiGride(dataSource) {
    gridaTrupi = new myDxDataGrid("grida", {
        dataSource: new Array(),
        editing: {
            allowUpdating: false
        },
        paging: {
            enabled: true,
            pageSize: 10
        },
        scrolling: {
            mode: "standard"
        },
        showRowLines: true,
        allowColumnReordering: true,
        filterRow: {
            visible: true
        },
        wordWrapEnabled: true
    });

    gridaTrupi.SetColumnsFromConfig(pergatitKolona());
    gridaTrupi.AddCustomOptionToColumns(["Server Name", "Data", "Ora", "Moduli", "Verbosity"], "width", 100);
    gridaTrupi.AddCustomOptionToColumns(["Ora"], "format", "HH:mm:ss");
    gridaTrupi.AddCustomOptionToColumns(["Data"], "format", "dd/MM/yyyy");
}

function pergatitKolona() {
    var columns = new Array();
    columns.push({ KodiTrupi: "Server Name", PershkrimiTrupi: "Server Name", ReadonlyTrupi: false, VisibleTrupi: true });
    columns.push({ KodiTrupi: "Data", PershkrimiTrupi: "Data", ReadonlyTrupi: true, VisibleTrupi: true, TipiFushes: "date", sortOrder:"asc" });
    columns.push({ KodiTrupi: "Ora", PershkrimiTrupi: "Ora", ReadonlyTrupi: true, VisibleTrupi: true, sortOrder: "asc" });
    columns.push({ KodiTrupi: "Moduli", PershkrimiTrupi: "Lloji", ReadonlyTrupi: true, VisibleTrupi: true });
    columns.push({ KodiTrupi: "Verbosity", PershkrimiTrupi: "Verbosity", ReadonlyTrupi: true, VisibleTrupi: true });
    columns.push({ KodiTrupi: "Pershkrimi", PershkrimiTrupi: "Pershkrimi", ReadonlyTrupi: true, VisibleTrupi: true });
    return columns;
}

function menu_click(s, e) {
    switch (e.item.name) {
        case "Pastro":
            PastroFusha();
            e.processOnServer = false;
            break;
        case "Shiko":
            e.processOnServer = false;            
            callWebserviceNgarkoLoget();
            break;
        default:
            break;
    }
}

function PastroFusha() {
    var data = new Date();
    dataNga.SetDate(data);
    dataDeri.SetDate(data);
    cbModuli.SetSelectedIndex(10);    // Te pergjithshme
    cbVerbosity.SetSelectedIndex(5); // Error
    PastroGride();
}

function PastroGride() {
    gridaTrupi.Clean();
    gridaTrupi.Grida.clearFilter();
    gridaTrupi.AddCustomOptionToColumns(["Data", "Ora"], "sortOrder", "asc");
}

function callWebserviceNgarkoLoget() {
    PastroGride();
    Utils.shfaqLoadingGif();
    $.ajax({
        url: Utils.getServerApiUrl("Automatizim", "MerrLogeSistemiDataTable"),
        data: JSON.stringify({ dataNga: dataNga.GetText(), dataDeri: dataDeri.GetText(), moduli: cbModuli.GetText(), verbosity: cbVerbosity.GetText() })
    }).done(SucceededCallbackNgarkoLoget);
}

function SucceededCallbackNgarkoLoget(result) {
    Utils.hiqLoadingGif();
    if (result.kaTeDhena == false || (result.kaTeDhena == true && result.mesazh != ""))
        myMesazh.ShtoMesazhInformues(result.mesazh);
    else 
        mbushGride(result.dataSource);
}

function mbushGride(datasource) {
    gridaTrupi.SetDataSource(datasource);
}

function RuajNivelVerbosity(s, e) {
    e.processOnServer = false;
    Utils.shfaqLoadingGif();
    $.ajax({
        url: Utils.getServerApiUrl("Automatizim", "RuajNivelVerbosity"),
        data: JSON.stringify({ verbosity: cbVerbosity.GetValue(), moduli: cbModuli.GetText() })
    }).done(SucceededCallbackRuajNivelVerbosity);
}

function SucceededCallbackRuajNivelVerbosity(result) {
    Utils.hiqLoadingGif();
    if (result.error)
        myMesazh.ShtoMesazhGabimi(result.mesazh);
    else
        myMesazh.ShtoMesazhSuksesi(result.mesazh);
}

function moduliSelectedIndexChanged(s, e) {
    btnRuajVerbosity.SetEnabled(s.GetText() != '' && cbVerbosity.GetText() != '');
}

function verbositySelectedIndexChanged(s, e) {
    btnRuajVerbosity.SetEnabled(s.GetText() != '' && cbModuli.GetText() != '');
}