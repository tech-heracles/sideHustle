
var indexModifiko;//indexi i zgjedhur per modifikim
var mbush = false;//perdoret per te treguar nese duhet te mbushen tabet apo jo kjo perdoret qe tabet te mbushen vetem heren e pare dhe jo pas cdo here qe  kalohet nga nje tab tek tjetri
var kaloTab = false;//perdoret per te daluar rastet kur bejme double click apo thjesht click ne menyre qe ne double click te kaloje tek tabi i dyte
var lista = true;//perdoret per griden e llogarive ne menyre qe te lodohet vetem ne momentin kur shkon ne tabin e saj
var idSeleketuar = -1;
var btnFiltrat;
var gridaTrupi;
var ruaj = false;
var timeout;
var colTeDrejta = [];//dataSet per te drejtat
var widthCol = '4%'

$(window).load(function () {
    try {
        Init();
    }
    catch (e) {
    }
});

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
                break;
            default:
                break;
        }
    });
});

function Init() {
    changeName();
    PageControl.SetActiveTabIndex(0);
    myMenu.PercaktoMenuSipasTabit(0, hfTeDrejta, $('#hfShtimModifikim'));
    ASPxMenu1.GetItemByName('KlonoTeDrejta').SetVisible(false);
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(EndRequestHandler);
    $("#hfNdermarjeChanged")[0].value = 'false';
    $('#hfNdryshuarTeDhenaRoli').val("false");
    widthCol = (!hfState.Get("invisibleVodafone")) ? '4.3%' : '7.5%';
}

function inicializoDxTreeList(teDhenat) {
    krijoDatasourcePerTreeGrid(teDhenat);
    gridaTrupi = new myDxTreeList("rowed5", {
        dataSource: colTeDrejta,
        keyExpr: "id",
        parentIdExpr: "parent",
        showRowLines: true,
        showBorders: true,
        wordWrapEnabled: true,
        autoExpandAll: false,
        expandNodesOnFiltering: false,
        filterRow: {
            visible: true
        },
        editing: {
            mode: "cell",
            allowUpdating: true,
        },
        width: function () { return window.innerWidth / 1.03; },
        columnAutoWidth: true,
        columns: [
            { dataField: 'id', visible: false },
            { dataField: 'parent', visible: false },
            { caption: hfState.Get("colHeaderModuli")/*'Moduli'*/, dataType: "string", dataField: 'TextModuli', width: '19%', allowSorting: true, allowEditing: false },
            { caption: hfState.Get("colHeaderPershkrimKomponente")/*'Pershkrim Komponente'*/, dataType: "string", dataField: 'PershkrimKomponente', width: '17%', allowSorting: true, allowEditing: false },
            { caption: hfState.Get("cmbTeGjitha")/*'Te Gjitha'*/, dataType: "string", alignment: 'center', dataField: 'DPlot', width: widthCol, allowSorting: false, cellTemplate: function (container, options) { container.append("<div>" + cboxFormatter(options.data["DPlot"], 'DPlot', options.data) + "</div>"); }, setCellValue: function (newData, value, currentRowData) { SetCellValue(this, newData, value, currentRowData); } },
            {
                cellTemplate: function (container, options) { container.append("<div>" + cboxFormatter(options.data["DAmb"], 'DAmb', options.data) + "</div>"); },
                caption: 'Menu', dataType: "string", alignment: 'center', dataField: 'DAmb', width: widthCol, allowSorting: false,
                setCellValue: function (newData, value, currentRowData) { SetCellValue(this, newData, value, currentRowData); }
            },
            {
                cellTemplate: function (container, options) { container.append("<div>" + cboxFormatter(options.data["DFsh"], 'DFsh', options.data) + "</div>"); },
                caption: hfState.Get("colHeaderFshirje") /*'Fshirje'*/, dataType: "string", alignment: 'center', dataField: 'DFsh', width: widthCol, allowSorting: false,
                setCellValue: function (newData, value, currentRowData) { SetCellValue(this, newData, value, currentRowData); }
            },
            {
                cellTemplate: function (container, options) { container.append("<div>" + cboxFormatter(options.data["DMod"], 'DMod', options.data) + "</div>"); },
                caption: hfState.Get("colHeaderModifikimi")/*'Modifikimi'*/, dataType: "string", alignment: 'center', dataField: 'DMod', width: widthCol, allowSorting: false,
                setCellValue: function (newData, value, currentRowData) { SetCellValue(this, newData, value, currentRowData); }
            },
            {
                cellTemplate: function (container, options) { container.append("<div>" + cboxFormatter(options.data["DModifikimDraft"], 'DModifikimDraft', options.data) + "</div>"); },
                caption: hfState.Get("colHeaderModifikimiDraft")/*'Modifikimi Draft'*/, dataType: "string", alignment: 'center', dataField: 'DModifikimDraft', width: widthCol, allowSorting: false,
                setCellValue: function (newData, value, currentRowData) { SetCellValue(this, newData, value, currentRowData); }
            },
            {
                cellTemplate: function (container, options) { container.append("<div>" + cboxFormatter(options.data["DShtim"], 'DShtim', options.data) + "</div>"); },
                caption: hfState.Get("colHeaderShtimi")/*'Shtimi'*/, dataType: "string", alignment: 'center', dataField: 'DShtim', width: widthCol, allowSorting: false,
                setCellValue: function (newData, value, currentRowData) { SetCellValue(this, newData, value, currentRowData); }
            },
            {
                cellTemplate: function (container, options) { container.append("<div>" + cboxFormatter(options.data["DShtimDraft"], 'DShtimDraft', options.data) + "</div>"); },
                caption: hfState.Get("colHeaderShtimiDraft")/*'Shtimi Draft'*/, dataType: "string", alignment: 'center', dataField: 'DShtimDraft', width: widthCol, allowSorting: false,
                setCellValue: function (newData, value, currentRowData) { SetCellValue(this, newData, value, currentRowData); }
            },
            {
                cellTemplate: function (container, options) { container.append("<div>" + cboxFormatter(options.data["DGjitheDok"], 'DGjitheDok', options.data) + "</div>"); },
                caption: hfState.Get("colHeaderShikoGjitheDok")/*'Shiko Gjithe Dok'*/, dataType: "string", alignment: 'center', dataField: 'DGjitheDok', width: widthCol, allowSorting: false,
                setCellValue: function (newData, value, currentRowData) { SetCellValue(this, newData, value, currentRowData); }
            },
            {
                cellTemplate: function (container, options) { container.append("<div>" + cboxFormatter(options.data["DKerko"], 'DKerko', options.data) + "</div>"); },
                caption: hfState.Get("colKerkim")/*'Kerkim'*/, dataType: "string", alignment: 'center', dataField: 'DKerko', visible: !hfState.Get("invisibleVodafone"), width: widthCol, allowSorting: false,
                setCellValue: function (newData, value, currentRowData) { SetCellValue(this, newData, value, currentRowData); }
            },
            {
                cellTemplate: function (container, options) { container.append("<div>" + cboxFormatter(options.data["DEksporto"], 'DEksporto', options.data) + "</div>"); },
                caption: hfState.Get("colEksportim")/*'Eksportim'*/, dataType: "string", alignment: 'center', dataField: 'DEksporto', visible: !hfState.Get("invisibleVodafone"), width: widthCol, allowSorting: false,
                setCellValue: function (newData, value, currentRowData) { SetCellValue(this, newData, value, currentRowData); }
            },
            {
                cellTemplate: function (container, options) { container.append("<div>" + cboxFormatter(options.data["DPrinto"], 'DPrinto', options.data) + "</div>"); },
                caption: hfState.Get("colPrintim")/*'Printim'*/, dataType: "string", alignment: 'center', dataField: 'DPrinto', visible: !hfState.Get("invisibleVodafone"), width: widthCol, allowSorting: false,
                setCellValue: function (newData, value, currentRowData) { SetCellValue(this, newData, value, currentRowData); }
            },
            {
                cellTemplate: function (container, options) { container.append("<div>" + cboxFormatter(options.data["DArkiva"], 'DArkiva', options.data) + "</div>"); },
                caption: hfState.Get("colArkiva")/*'Arkiva'*/, dataType: "string", alignment: 'center', dataField: 'DArkiva', visible: !hfState.Get("invisibleVodafone"), width: widthCol, allowSorting: false,
                setCellValue: function (newData, value, currentRowData) { SetCellValue(this, newData, value, currentRowData); }
            },
            {
                cellTemplate: function (container, options) { container.append("<div>" + cboxFormatter(options.data["DKonverto"], 'DKonverto', options.data) + "</div>"); },
                caption: hfState.Get("colKonverto")/*'Konverto'*/, dataType: "string", alignment: 'center', dataField: 'DKonverto', visible: !hfState.Get("invisibleVodafone"), width: widthCol, allowSorting: false,
                setCellValue: function (newData, value, currentRowData) { SetCellValue(this, newData, value, currentRowData); }
            },
            {
                cellTemplate: function (container, options) { container.append("<div>" + cboxFormatter(options.data["DPezullo"], 'DPezullo', options.data) + "</div>"); },
                caption: hfState.Get("colPezullo")/*'Pezullo'*/, dataType: "string", alignment: 'center', dataField: 'DPezullo', visible: !hfState.Get("invisibleVodafone"), width: widthCol, allowSorting: false,
                setCellValue: function (newData, value, currentRowData) { SetCellValue(this, newData, value, currentRowData); }
            },
            {
                cellTemplate: function (container, options) { container.append("<div>" + cboxFormatter(options.data["DAutoKonverto"], 'DAutoKonverto', options.data) + "</div>"); },
                caption: hfState.Get("colAutoKonverto")/*'Auto Konverto'*/, dataType: "string", alignment: 'center', dataField: 'DAutoKonverto', visible: !hfState.Get("invisibleVodafone"), width: widthCol, allowSorting: false,
                setCellValue: function (newData, value, currentRowData) { SetCellValue(this, newData, value, currentRowData); }
            },
        ],
        loadPanel: { enabled: true },
        onContentReady: function (e) {
            this.collapseAdaptiveDetailRow();
        },
        onEditorPreparing: function (e) {
            if (e.parentType == "filterRow")
                return;
            if (e.row.data.Tipi == "2") {
                if ((e.row.data.IdRaport !== -1 && e.dataField !== "DGjitheDok" && e.dataField !== "DPlot" && e.dataField !== "DAmb") || (e.row.data.IdRaport == -1 && (e.dataField == "DAmb" || e.dataField == "DFsh" || e.dataField == "DMod" || e.dataField == "DShtim" || e.dataField == "DGjitheDok" || e.dataField == "DModifikimDraft" || e.dataField == "DShtimDraft" || e.dataField == "DKerko" || e.dataField == "DEksporto" || e.dataField == "DPrinto" || e.dataField == "DArkiva" || e.dataField == "DKonverto" || e.dataField == "DPezullo" || e.dataField == "DAutoKonverto")))

                    e.editorOptions.disabled = true;
                else
                    e.editorOptions.disabled = false;
            }
            else if (e.row.data.Tipi == "1") {
                if (e.dataField == "DModifikimDraft" || e.dataField == "DShtimDraft")

                    e.editorOptions.disabled = true;
                else
                    e.editorOptions.disabled = false;
            }
            else
                e.editorOptions.disabled = false;
        },
        onCellClick: function (e) {
            if (e.rowType == "data" && e.columnIndex > 1) {
                this.cellValue(e.rowIndex, e.column.dataField, !e.value);
            }
        },
    });
}

function cboxFormatter(cellvalue, dataField, rowObject) {
    if (rowObject.Tipi == '2') {
        if ((rowObject.IdRaport !== -1 && dataField !== 'DGjitheDok' && dataField !== 'DPlot' && dataField !== 'DAmb') || (rowObject.IdRaport == -1 && (dataField == 'DAmb' || dataField == 'DFsh' || dataField == 'DMod' || dataField == 'DShtim' || dataField == 'DGjitheDok' || dataField == 'DModifikimDraft' || dataField == 'DShtimDraft' || dataField == 'DKerko' || dataField == 'DEksporto' || dataField == 'DPrinto' || dataField == 'DArkiva' || dataField == 'DKonverto' || dataField == 'DPezullo' || dataField == 'DAutoKonverto')))
            return '<input type="checkbox" disabled="true" id="' + dataField + rowObject.id + '"' + (cellvalue ? ' checked="checked"' : '') + '/>';
        else
            return '<input type="checkbox" id="' + dataField + rowObject.id + '"' + (cellvalue ? ' checked="checked"' : '') + '/>';
    }
    else if (rowObject.Tipi == '1') {
        if (dataField == 'DModifikimDraft' || dataField == 'DShtimDraft')
            return '<input type="checkbox" disabled="true" id="' + dataField + rowObject.id + '"' + (cellvalue ? ' checked="checked"' : '') + '/>';
        else
            return '<input type="checkbox" id="' + dataField + rowObject.id + '"' + (cellvalue ? ' checked="checked"' : '') + '/>';
    }
    else if (rowObject.Tipi == '4' && dataField !== 'DAmb')//Tipi=4 eshte per ato komponente qe kane vetem te drejten "Menu"
        return '<input type="checkbox" disabled="true" id="' + dataField + rowObject.id + '"' + (cellvalue ? ' checked="no"' : '') + '/>';
    else
        return '<input type="checkbox" id="' + dataField + rowObject.id + '"' + (cellvalue ? ' checked="checked"' : '') + '/>';
}


function SetCellValue(col, newData, value, currentRowData) {
    var data2 = gridaTrupi.GetData();
    var key = currentRowData.id;
    var row2 = data2.filter(function (e) { return e.id === key })[0];
    var emriKolonesModifikuar = col.dataField;
    row2[emriKolonesModifikuar] = value;
    var ndryshuar = false;
    var kaBije = data2.filter(function (e) { return e.parent === key }).length != 0;

    if (emriKolonesModifikuar == "DPlot") {
        row2 = modifikoGjitheRreshtinSipasVleres(row2, value);
        if (kaBije)
            ndryshuar = modifikoBijatSipasVleres(data2, data2.filter(function (e) { return e.parent === key }), value);
        if (row2.parent != '0')
            ndryshuar = modifikoPrindPerKolonen(data2, row2.parent, 'DPlot', ndryshuar);
    }
    else {
        row2["DPlot"] = merrVlerePerDPlot(row2);
        if (kaBije)
            ndryshuar = modifikoKoloneTeBijatSipasVleres(data2, data2.filter(function (e) { return e.parent === key }), emriKolonesModifikuar, value);
        if (row2.parent != '0')
            ndryshuar = modifikoPrindPerKolonen(data2, row2.parent, emriKolonesModifikuar, ndryshuar);
    }
    if (ndryshuar)
        gridaTrupi.Refresh();
}

function modifikoGjitheRreshtinSipasVleres(row, value) {
    row.DAmb = value;
    row.DFsh = value;
    row.DMod = value;
    row.DModifikimDraft = value;
    row.DShtim = value;
    row.DShtimDraft = value;
    row.DGjitheDok = value;
    row.DKerko = value;
    row.DEksporto = value;
    row.DPrinto = value;
    row.DArkiva = value;
    row.DKonverto = value;
    row.DPezullo = value;
    row.DAutoKonverto = value;
    row.DPlot = value;
    return row;
}

function modifikoGjitheRreshtinSipasVleresPlot(row, resultPlot) {
    row.DAmb = resultPlot.DAmb;
    row.DFsh = resultPlot.DFsh;
    row.DMod = resultPlot.DMod;
    row.DModifikimDraft = resultPlot.DModifikimDraft;
    row.DShtim = resultPlot.DShtim;
    row.DShtimDraft = resultPlot.DShtimDraft;
    row.DGjitheDok = resultPlot.DGjitheDok;
    row.DKerko = resultPlot.DKerko;
    row.DEksporto = resultPlot.DEksporto;
    row.DPrinto = resultPlot.DPrinto;
    row.DArkiva = resultPlot.DArkiva;
    row.DKonverto = resultPlot.DKonverto;
    row.DPezullo = resultPlot.DPezullo;
    row.DAutoKonverto = resultPlot.DAutoKonverto;
}

function modifikoBijatSipasVleres(tedhenat, bijat, vlera) {
    bijat.forEach(function (element) {
        modifikoGjitheRreshtinSipasVleres(element, vlera);
        var b = tedhenat.filter(function (e) { return e.parent === element.id });
        if (b.length > 0)
            modifikoBijatSipasVleres(tedhenat, b, vlera);
    });
    return true;
}

function modifikoKoloneTeBijatSipasVleres(tedhenat, bijat, kolona, vlera) {
    bijat.forEach(function (nd) {
        if (nd[kolona] != vlera)
            nd[kolona] = vlera;
        var b = tedhenat.filter(function (e) { return e.parent === nd.id });//var b = tedhenat.filter(x => x.parent == nd.id);
        if (b.length > 0)
            modifikoKoloneTeBijatSipasVleres(tedhenat, b, kolona, vlera);
        nd["DPlot"] = merrVlerePerDPlot(nd);
    });
    return true;
}

function modifikoPrindPerKolonen(tedhenat, parentKey, kolona, result) {
    var kolVlera = true;
    var node = tedhenat.filter(function (e) { return e.id === parentKey })[0];
    var bijat = tedhenat.filter(function (e) { return e.parent === parentKey });
    var resultPlot = { DAmb: true, DFsh: true, DMod: true, DModifikimDraft: true, DShtim: true, DShtimDraft: true, DGjitheDok: true, DKerko: true, DEksporto: true, DPrinto: true, DArkiva: true, DKonverto: true, DPezullo: true, DAutoKonverto: true };

    bijat.forEach(function (nd) {
        kolVlera = kolVlera && nd[kolona];
        resultPlot.DAmb = resultPlot.DAmb && nd["DAmb"];
        resultPlot.DFsh = resultPlot.DFsh && nd["DFsh"];
        resultPlot.DMod = resultPlot.DMod && nd["DMod"];
        resultPlot.DModifikimDraft = resultPlot.DModifikimDraft && nd["DModifikimDraft"];
        resultPlot.DShtim = resultPlot.DShtim && nd["DShtim"];
        resultPlot.DShtimDraft = resultPlot.DShtimDraft && nd["DShtimDraft"];
        resultPlot.DGjitheDok = resultPlot.DGjitheDok && nd["DGjitheDok"];
        resultPlot.DKerko = resultPlot.DKerko && nd["DKerko"];
        resultPlot.DEksporto = resultPlot.DEksporto && nd["DEksporto"];
        resultPlot.DPrinto = resultPlot.DPrinto && nd["DPrinto"];
        resultPlot.DArkiva = resultPlot.DArkiva && nd["DArkiva"];
        resultPlot.DKonverto = resultPlot.DKonverto && nd["DKonverto"];
        resultPlot.DPezullo = resultPlot.DPezullo && nd["DPezullo"];
        resultPlot.DAutoKonverto = resultPlot.DAutoKonverto && nd["DAutoKonverto"];
    });

    result = (node[kolona] != kolVlera) ? true : result;
    node[kolona] = kolVlera;

    if (kolona == 'DPlot') {
        modifikoGjitheRreshtinSipasVleresPlot(node, resultPlot);
        result = true;
    }
    else
        node["DPlot"] = merrVlerePerDPlot(node);

    if (node.parent != "0")
        result = modifikoPrindPerKolonen(tedhenat, node.parent, kolona, result);

    return result;
}

function merrVlerePerDPlot(row) {
    var vlera = row.DAmb && row.DFsh && row.DMod && row.DModifikimDraft && row.DShtim && row.DShtimDraft && row.DGjitheDok && row.DKerko && row.DEksporto && row.DPrinto && row.DArkiva && row.DKonverto && row.DPezullo && row.DAutoKonverto;
    return (vlera == undefined) ? false : vlera;
}

function krijoDatasourcePerTreeGrid(teDhena) {
    var coli = [];
    if (teDhena != null && teDhena.length != null) {
        coli = teDhena.map(function (elem) {
            return {
                "id": parseInt(elem.IdPeme.toString()),
                "parent": parseInt((elem.IdPrindPeme == null) ? "0" : elem.IdPrindPeme.toString()),
                "IdDrejta": elem.IdDrejta.toString(),
                "IdPrindi": (elem.IdPrindi == null) ? "0" : elem.IdPrindi.toString(),
                "TextModuli": elem.TextModuli.toString(),
                "PershkrimKomponente": elem.PershkrimKomponente.toString(),
                "DPlot": elem.DPlot,
                "DAmb": elem.DAmb,
                "DFsh": elem.DFsh,
                "DMod": elem.DMod,
                "DModifikimDraft": elem.DModifikimDraft,
                "DShtim": elem.DShtim,
                "DShtimDraft": elem.DShtimDraft,
                "DGjitheDok": elem.DGjitheDok,
                "DNdryshoCmimBlerje": elem.DNdryshoCmimBlerje,
                "DNdryshoCmimShitje": elem.DNdryshoCmimShitje,
                "DNdryshoZbritjeAnalitike": elem.DNdryshoZbritjeAnalitike,
                "DNdryshoZbritjeTotale": elem.DNdryshoZbritjeTotale,
                "DKonvertimSipasUrdherShitje": elem.DKonvertimSipasUrdherShitje,
                "IdKomponente": elem.IdKomponente.toString(),
                "IdRaport": elem.IdRaport.toString(),
                "IdViti": elem.IdViti.toString(),
                "IdNdermarrje": elem.IdNdermarrje.toString(),
                "IdRoli": elem.IdRoli.toString(),
                "IdModul": elem.IdModul.toString(),
                "Tipi": elem.Tipi.toString(),
                "Niveli": elem.Niveli.toString(),
                "isLeaf": elem.EshteGjethe,
                "expanded": elem.Expanded,
                "DKonvertimi": elem.DKonvertimi,
                "DKerko": elem.DKerko,
                "DEksporto": elem.DEksporto,
                "DPrinto": elem.DPrinto,
                "DArkiva": elem.DArkiva,
                "DKonverto": elem.DKonverto,
                "DPezullo": elem.DPezullo,
                "DAutoKonverto": elem.DAutoKonverto,
                "IdDrejtaKoka": elem.IdDrejtaKoka.toString(),
                "IdLayer": elem.IdLayer.toString(),
                "IdNivelRegjistrimi": elem.IdNivelRegjistrimi.toString(),
                "IdKategoria": elem.IdKategoria.toString()
            };
        });
        colTeDrejta = coli;
    }
}

function mbushCheckBoxe() {
    if (colTeDrejta == null)
        return;
    cbNdryshoCmimeShitje.SetChecked(colTeDrejta[1].DNdryshoCmimShitje);
    cbNdryshoCmimeBlerje.SetChecked(colTeDrejta[1].DNdryshoCmimBlerje);
    cbNdryshoZbritjeAnalitike.SetChecked(colTeDrejta[1].DNdryshoZbritjeAnalitike);
    cbNdryshoZbritjeTotale.SetChecked(colTeDrejta[1].DNdryshoZbritjeTotale);
    cbVetemKonvertim.SetChecked(colTeDrejta[1].DKonvertimi);
    if (colTeDrejta[1].DKonvertimi) {
        cbKonvertimSipasUrdherShitje.SetEnabled(true);
        cbKonvertimSipasUrdherShitje.SetChecked(colTeDrejta[1].DKonvertimSipasUrdherShitje);
    }
    else {
        cbKonvertimSipasUrdherShitje.SetEnabled(false);
        cbKonvertimSipasUrdherShitje.SetChecked(false);
    }
}

function checkText(s, e) {
    myMenu.checkText(s, e);
}

function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gridaRoli, "", "");
}

function textChanged(s, e) {
    myMenu.textChanged(s, e);
}

function changeName() {
    var hf = $("#hfKonffillestar")[0];
    myFaqeCelje.changeName('ShtoModifiko_Grup_Perdoruesish.aspx', 0, hf);
}

function PageControl_ActiveTabChanging(s, e) {
    if (e.tab.index == 3 && $("#hfShtimModifikim").val() == "shtim" && !ruaj) {
        e.cancel = true;
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgRuaniRolinPeraparaSeTeKaloniTeDrejtat"));
        return;
    }
    if (e.tab.index == 3 && $("#hfShtimModifikim").val() == "modifikim" && ($("#hfNdermarjeChanged")[0].value == 'true' || $("#hfNdryshuarTeDhenaRoli").val() == "true") && !ruaj) {
        e.cancel = true;
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgRuaniRolinPeraparaSeTeKaloniTeDrejtat"));
        return;
    }
    if (gridaRoli.GetFocusedRowIndex() < 0 && e.tab.index != 0 && $("#hfShtimModifikim").val() != "shtim") {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgDuhetTeZgjidhniTePaktenNjeRol"));
        e.processOnServer = false;
        e.cancel = true;
        return;
    }
}

function tabsActiveTabChanged(s, e) {
    indexModifiko = gridaRoli.GetFocusedRowIndex();
    if (mbush) {
        if (indexModifiko !== -1) {
            OnGridDoubleClick(indexModifiko);
        }
        else {
            mbush = false;
            $('#hfShtimModifikim').val('shtim');
            $('#hfId').val(0);
            pastrofusha();
        }
    }
    if (e.tab.index == 2) {
        ASPxMenu1.GetItemByName('ZgjidhNdermarrje').SetVisible(true);
    }
    else
        ASPxMenu1.GetItemByName('ZgjidhNdermarrje').SetVisible(false);
    if (e.tab.index == 3) {
        ASPxMenu1.GetItemByName('KlonoTeDrejta').SetVisible(true);
        resetAltRows.call($('#rowed5'));
    }
    else
        ASPxMenu1.GetItemByName('KlonoTeDrejta').SetVisible(false);
    if (e.tab.index == 1) {
        if ($('#hfShtimModifikim').val() == 'modifikim')
            kodiASPxTextBox.SetEnabled(false);
        else
            kodiASPxTextBox.SetEnabled(true);
    }
    kaloTab = false;
    myMenu.PercaktoMenuSipasTabit(e.tab.index, hfTeDrejta, $('#hfShtimModifikim'));
}

function buttonModifiko_Click(s, e) {
    if (gridaRoli.GetFocusedRowIndex() < 0 && PageControl.GetActiveTabIndex() != 0 && $("#hfShtimModifikim").val() != "shtim") {
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgDuhetTeZgjidhniTePaktenNjeRol"));
        e.processOnServer = false;
    }
}

function OnGridDoubleClick(index) {
    /*metodat per te hapur faqen e modifikimit me double click*/

    indexModifiko = index;
    gridaRoli.SetFocusedRowIndex(index);
    lista = true;
    mbushfusha();
}

function mbushfusha() {
    /*merr te dhenat e rreshtit te selektuar*/

    mbush = false;
    $('#hfShtimModifikim').val("modifikim");
    indexModifiko = gridaRoli.GetFocusedRowIndex();
    if (indexModifiko == -1)
        myMesazh.ShtoMesazhGabimi(hfState.Get("msgDuhetTeZgjidhniNjeRol"));
    else
        gridaRoli.GetRowValues(indexModifiko, 'IdRoli;KodRoli;PershkrimRoli;AktivRoli;DateKrijimi;DateModifikimi;IdKrijuesi;Model;IdLicenca;IdStatusDok;DtKrijimi;DtModifikimi;PerdoruesUsername', OnGetRowValuesMod);
}

function OnGetRowValuesMod(values) {
    if ($('#hfShtimModifikim').val() == "shtim")
        return;

    if (Utils.Contains(hfState.Get("roletEPerdoruesit").split(';'), values[1]))
        myMesazh.ShtoMesazhInformues(hfState.Get("msgMosModifikoRolinTend"));

    $('#hfId').val(values[0]);
    kodiASPxTextBox.SetEnabled(false);
    dateKrijimiASPxDateEdit.SetEnabled(false);
    dateModifikimiASPxDateEdit.SetEnabled(false);
    idKrijuesiASPxTextBox.SetEnabled(false);
    $("#hfNdermarjeChanged").val('false');
    $('#hfNdryshuarTeDhenaRoli').val("false");
    kodiASPxTextBox.SetText(values[1]);
    pershkrimiASPxTextBox.SetText(values[2]);
    aktivASPxCheckBox.SetChecked(values[3]);
    idKrijuesiASPxTextBox.SetText(values[12]);
    dateKrijimiASPxDateEdit.SetDate(values[4]);
    dateModifikimiASPxDateEdit.SetDate(values[5]);
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "ktheNdermarrjetDheVitet"),
        data: JSON.stringify({ idRoli: values[0] })
    }).done(SucceededCallbackNdermarrjetDheVitet);
    gridLidhjeRole.PerformCallback(values[0]);
    cmbLicenca.SetValue(values[8]);
    cmbLicenca.SetEnabled(false);
    $("#hfVitetSel").val('');
    gvNderRol.PerformCallback(values[0]);
    if (kaloTab) {
        PageControl.SetActiveTabIndex(1);
        myMenu.PercaktoMenuSipasTabit(1, hfTeDrejta, $('#hfShtimModifikim'));
    }
}

function SucceededCallbackNdermarrjetDheVitet(result) {
    var ndermarrjet = new Array();
    ndermarrjet = result.ndermarrjet;
    var vitetPerNderm = new Array();
    vitetPerNderm = result.vitPerNderm;

    Utils.MbushCombo(ASPxComboBoxNdermarrje, ndermarrjet, "IdNdermarrje", "NdermarrjeKodi");
    ASPxComboBoxNdermarrje.SetSelectedIndex(0);
    $('#HiddenFieldNdermarrje').val(ASPxComboBoxNdermarrje.GetValue());

    ASPxComboBoxViti.BeginUpdate()
    ASPxComboBoxViti.ClearItems();
    var vitetDheNdermarrjet = new Array();
    vitetDheNdermarrjet = vitetPerNderm.split(';');
    for (i = 0; i < vitetDheNdermarrjet.length; i++) {
        if (vitetDheNdermarrjet[i].split(',')[0] != ASPxComboBoxNdermarrje.GetValue()) //mbushim kombon e viteve vetem me vitet e ndermarrjes se selektuar te komboja e ndermarrjes
            continue;
        else {
            var kodViti = vitetDheNdermarrjet[i].split(',')[1];
            var idViti = vitetDheNdermarrjet[i].split(',')[3];
            ASPxComboBoxViti.AddItem(kodViti, idViti);
        }
    }
    ASPxComboBoxViti.EndUpdate();
    ASPxComboBoxViti.SetSelectedIndex(0);
    $('#HiddenFieldViti').val(ASPxComboBoxViti.GetValue());
    $('#hfVitetSel').val(vitetPerNderm);
    if (ndermarrjet.length > 0)
        $.ajax({
            url: Utils.getServerApiUrl("Rregjistrime", "ktheRolet"),
            data: JSON.stringify({ idGjuha: hfState.Get("idGjuha"), idNdermarrje: ASPxComboBoxNdermarrje.GetValue(), idRoli: $('#hfId').val(), idViti: ASPxComboBoxViti.GetValue(), idlicence: cmbLicenca.GetValue() })
        }).done(SucceededCallbackTreeGrid);
}

function SucceededCallbackTreeGrid(result) {
    inicializoDxTreeList(result);
    mbushCheckBoxe();
}

function Poshte_click(e) {
    /*perdoret per te selektuar rreshtin me poshte*/

    indexSel = myMenu.JSlevizNeGride.Poshte_click(e, ASPxGridView_Artikull, indexSel);
}

function Lart_click(e) {
    /*perdoret per te selektuar rreshtin me lart*/
    indexSel = myMenu.JSlevizNeGride.Lart_click(e, ASPxGridView_Artikull, indexSel);
}

function Fillim_click(e) {
    /*perdoret per te shkuar ne fillim te faqes*/
    indexSel = myMenu.JSlevizNeGride.Fillim_click(e, ASPxGridView_Artikull, indexSel);
}

function Fund_click(e) {
    /*perdoret per te shkuar ne fund te faqes*/
    indexSel = myMenu.JSlevizNeGride.Fund_click(e, ASPxGridView_Artikull, indexSel);
}

function menu_click(s, e) {
    /*perdoret per veprimet e menuse ne javascript*/

    var hfShtimModifikim = $('#hfShtimModifikim'); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
    var hfId = $('#hfId'); //hidden fieldi qe ruan id  e rreshtit te selektuar
    if (e.item.name == "Ruaj") {
        if (Utils.Contains(hfState.Get("roletEPerdoruesit").split(';'), kodiASPxTextBox.GetText())) {
            e.processOnServer = false;
            myMesazh.ShtoMesazhGabimi(hfState.Get("msgMosModifikoRolinTend"));
            return;
        }
        ruaj = true;
        merrTeDrejtat();
    }
    myMenu.menu_click(s, e, hfShtimModifikim, hfId, PageControl, false, false, indexModifiko, pastrofusha, null, null, undefined, undefined, undefined);
    if (e.item.name === "Ruaj") {
        ruaj = false;
    }

    if (e.item.name === "KlonoTeDrejta") {
        popKlono.Show();
        e.processOnServer = false;
    }
    if (e.item.name === "ZgjidhNdermarrje") {
        shfaqPopUpNdermarrje();
        e.processOnServer = false;
    }
}

function merrTeDrejtat() {
    var teDhenat = ($('#hfShtimModifikim').val() == "shtim") ? [] : gridaTrupi.GetData().filter(function (e) { return e.parent != "0" });
    var reja = [];
    reja = teDhenat.map(function (elem) {
        return {
            IdPeme: elem.id,
            IdDrejta: elem.IdDrejta,
            TextModuli: elem.TextModuli,
            PershkrimKomponente: elem.PershkrimKomponente,
            DPlot: elem.DPlot,
            DAmb: elem.DAmb,
            DFsh: elem.DFsh,
            DMod: elem.DMod,
            DModifikimDraft: elem.DModifikimDraft,
            DShtim: elem.DShtim,
            DShtimDraft: elem.DShtimDraft,
            DGjitheDok: elem.DGjitheDok,
            IdViti: elem.IdViti,
            IdNdermarrje: elem.IdNdermarrje,
            IdRoli: elem.IdRoli,
            IdKomponente: elem.IdKomponente,
            IdRaport: elem.IdRaport,
            DNdryshoCmimBlerje: cbNdryshoCmimeBlerje.GetChecked(),
            DNdryshoCmimShitje: cbNdryshoCmimeShitje.GetChecked(),
            DNdryshoZbritjeAnalitike: cbNdryshoZbritjeAnalitike.GetChecked(),
            DNdryshoZbritjeTotale: cbNdryshoZbritjeTotale.GetChecked(),
            IdPrindi: elem.IdPrindi,
            IdPrindPeme: elem.IdPrindPeme,
            IdModul: elem.IdModul,
            Tipi: elem.Tipi,
            DKonvertimi: cbVetemKonvertim.GetChecked(),
            DKonvertimSipasUrdherShitje: cbKonvertimSipasUrdherShitje.GetChecked(),
            DKerko: elem.DKerko,
            DEksporto: elem.DEksporto,
            DPrinto: elem.DPrinto,
            DArkiva: elem.DArkiva,
            DKonverto: elem.DKonverto,
            DPezullo: elem.DPezullo,
            DAutoKonverto: elem.DAutoKonverto,
            IdDrejtaKoka: elem.IdDrejtaKoka,
            IdLayer: elem.IdLayer,
            IdNivelRegjistrimi: elem.IdNivelRegjistrimi,
            IdKategoria: elem.IdKategoria
        };
    });
    colTeDrejta = reja;
    $('#HfColTeDrejtat').val(JSON.stringify(colTeDrejta));
}

function valido(s, e) {
    myFaqeCelje.valido(s, e, PageControl, hfTeDrejta, $('#hfShtimModifikim'));
}

function CheckedChanged(s, id, column) {
    $("#hfNdermarjeChanged")[0].value = 'true';
    var check = s.GetChecked().toString();
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "ktheIdVitiSipasKodVitiDheIdNdermarrje"),
        data: JSON.stringify({ idNdermarrje: gvNderRol.GetRowKey(id), kodViti: column, check: check.toLowerCase() })
    }).done(SucceededCallbackIdViti);
}

function SucceededCallbackIdViti(result) {
    ShtoVitPerNdermarrje(result.idNdermarrje, result.kodViti, result.checkuar, result.idViti);
}

function ShtoVitPerNdermarrje(idndermarrje, kodviti, checkuar, idviti) {
    $("#hfVitetSel")[0].value = $("#hfVitetSel")[0].value.split(";").filter(function (val) { return val.toLowerCase() != idndermarrje + "," + kodviti + ',' + (checkuar != false).toString() + ',' + idviti && val.toLowerCase() != idndermarrje + "," + kodviti + ',' + (checkuar == false).toString() + ',' + idviti }).join(";") + ";" + idndermarrje + "," + kodviti + ',' + checkuar + ',' + idviti;
}

function EndRequestHandler(sender, args) {
    /*  Function: EndRequestHandler   */

    myMenu.PercaktoMenuSipasTabit(PageControl.GetActiveTabIndex(), hfTeDrejta, $('#hfShtimModifikim'));
    if ($('#hfShtimModifikim').val() == 'modifikim')
        mbush = false;
    if (PageControl.GetActiveTabIndex() == 3) {
        ASPxMenu1.GetItemByName('KlonoTeDrejta').SetVisible(true);
        inicializoDxTreeList(JSON.parse($('#HfColTeDrejtat').val()));
        mbushCheckBoxe();
    }
    else
        ASPxMenu1.GetItemByName('KlonoTeDrejta').SetVisible(false);

    try {
        gridaRoli.ClearFilter();
    }
    catch (e) {
    }
    $("#dvRolet").show();
    myMesazh.EndRequestTimer(sender, args);
}

function gvNderRol_SelectionChanged(s, e) {
    $('#hfNdermSel')[0].value = s.GetSelectedValues()[0];
}

function BeginCallback(s, e) {
    if (e.command == 'APPLYFILTER' && btnFiltrat != undefined)
        btnFiltrat.SetText('');
}

function ndryshoKonfigurimin() {
    lblKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[1]);
    cmbKonfigurimi.SetText(cmbKonfigurimi.GetText().split(';')[0]);
    callWebserviceKonfigurimi(160, cmbKonfigurimi.GetText());
}

function ndryshoKonfiguriminInit() {
    callWebserviceKonfigurimiInit(160, cmbKonfigurimi.GetText());
}

function callWebserviceKonfigurimi(idKomp, kodKonf) {
    var idGjuha = hfState.Get('idGjuha');
    gridaRoli.PerformCallback(idKomp + ";" + cmbKonfigurimi.GetText());
    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
        data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: hfState.Get('idNdermarrje'), idGjuha: idGjuha })
    }).done(SucceededCallbackKonfig);
}

function callWebserviceKonfigurimiInit(idKomp, kodKonf) {
    $("#dvRolet").show();
    var idGjuha = hfState.Get('idGjuha');

    $.ajax({
        url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
        data: JSON.stringify({ idKomp: idKomp, kodKonf: kodKonf, idNdermarrje: hfState.Get('idNdermarrje'), idGjuha: idGjuha })
    }).done(SucceededCallbackKonfig);
}

function SucceededCallbackKonfig(result) {
}

function onNdryshimFokusi() {
    //    try { if (PageControl.GetActiveTabIndex() === 0) $('#newHiddenField').val('modifikim'); } catch (e) { } //si ishte
    try {
        if (PageControl.GetActiveTabIndex() == 0)
            mbush = true;
    }
    catch (e) { }
}

function pastrofusha() {
    /*  pastron fushat per shtim dhe ben aktive fushat  */

    $('#hfShtimModifikim').val("shtim");
    $('#hfVitetSel').val("");
    $('#hfNdermSel').val("");
    $('#hfNderm').val("");
    $("#hfNdermarjeChanged").val('false');
    $('#hfNdryshuarTeDhenaRoli').val("false");
    $('#HiddenFieldVitiDestinacion').val("0");
    $('#HiddenFieldNdermarrjeDestinacion').val("0");
    kodiASPxTextBox.SetEnabled(true);
    kodiASPxTextBox.SetText('');
    pershkrimiASPxTextBox.SetText('');
    idKrijuesiASPxTextBox.SetText(hfState.Get('usernamePerdoruesLoguar'));
    aktivASPxCheckBox.SetChecked(true);
    dateKrijimiASPxDateEdit.SetEnabled(true);
    vendosDateDefault(dateKrijimiASPxDateEdit);
    dateModifikimiASPxDateEdit.SetEnabled(true);
    vendosDateDefault(dateModifikimiASPxDateEdit);
    ASPxComboBoxNdermarrje.SetSelectedIndex(-1);
    cmbLicenca.SetEnabled(true);

    ASPxComboBoxViti.SetSelectedIndex(-1);
    cbNdryshoCmimeShitje.SetChecked(false);
    cbNdryshoCmimeBlerje.SetChecked(false);
    cbNdryshoZbritjeAnalitike.SetChecked(false);
    cbNdryshoZbritjeTotale.SetChecked(false);
    cbVetemKonvertim.SetChecked(false);
    cbKonvertimSipasUrdherShitje.SetChecked(false);
    cbKonvertimSipasUrdherShitje.SetEnabled(false);
    gvNderRol.PerformCallback(-2);
}

function vendosDateDefault(kontrolli) {
    if ($("input[id$='hfShtimModifikim']").val() == "shtim") {
        try {
            var dataSot = Utils.zeroOren(new Date());
            kontrolli.SetDate(dataSot);
        }
        catch (e) { }
    }
}

function gvNderRolEndCallbcak(s, e) {
    Utils.hiqLoadingGif();
    $("#dvRolet").show();
}

function gvNderRol_BeginCallback(s, e) {
    Utils.shfaqLoadingGif();
}

function ASPxComboBoxNdermarrje_SelectedIndexChanged(s, e) {
    $('#HiddenFieldNdermarrje').val(ASPxComboBoxNdermarrje.GetValue());
    ASPxComboBoxViti.BeginUpdate();
    ASPxComboBoxViti.ClearItems();
    var vitetDheNdermarrjet = new Array();
    vitetDheNdermarrjet = $('#hfVitetSel').val().split(';');
    for (i = 0; i < vitetDheNdermarrjet.length; i++) {
        if (vitetDheNdermarrjet[i].split(',')[0] !== ASPxComboBoxNdermarrje.GetValue().toString())
            continue;
        else {
            if (vitetDheNdermarrjet[i].split(',')[2] == false || vitetDheNdermarrjet[i].split(',')[2] == "false")
                continue;
            else {
                var kodViti = vitetDheNdermarrjet[i].split(',')[1];
                var idViti = vitetDheNdermarrjet[i].split(',')[3];
                ASPxComboBoxViti.AddItem(kodViti, idViti);
            }
        }
    }
    ASPxComboBoxViti.EndUpdate();
    ASPxComboBoxViti.SetSelectedIndex(0);
    $('#HiddenFieldViti').val(ASPxComboBoxViti.GetValue());
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "ktheRolet"),
        data: JSON.stringify({ idGjuha: hfState.Get("idGjuha"), idNdermarrje: ASPxComboBoxNdermarrje.GetValue(), idRoli: $('#hfId').val(), idViti: ASPxComboBoxViti.GetValue(), idlicence: cmbLicenca.GetValue() })
    }).done(SucceededCallbackTreeGrid);
}

function ASPxComboBoxViti_SelectedIndexChanged(s, e) {
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "ktheRolet"),
        data: JSON.stringify({ idGjuha: hfState.Get("idGjuha"), idNdermarrje: ASPxComboBoxNdermarrje.GetValue(), idRoli: $('#hfId').val(), idViti: ASPxComboBoxViti.GetValue(), idlicence: cmbLicenca.GetValue() })
    }).done(SucceededCallbackTreeGrid);
}

function cmbNdermarjaKlonim_SelectedIndexChanged(s, e) {
    var idNderm = cmbNdermarja.GetValue();
    $('#HiddenFieldNdermarrjeDestinacion').val(idNderm);
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "ktheVitetPerNdermarrjen"),
        data: JSON.stringify({ idNdermarrje: idNderm })
    }).done(SucceededCallbackNdermarrjeKlonim);
}

function SucceededCallbackNdermarrjeKlonim(result) {
    var vitet = new Array();
    vitet = result.vitet;
    cmbViti.ClearItems();
    for (i = 0; i < vitet.length; i++) {
        cmbViti.AddItem(vitet[i].KodiViti, vitet[i].IdViti);
    }
    cmbViti.SetSelectedIndex(0);
    $('#HiddenFieldVitiDestinacion').val(vitet[0].IdViti);
}

function ndryshuarTeDhenaRoli() {
    if ($('#hfShtimModifikim').val() == "modifikim")
        $('#hfNdryshuarTeDhenaRoli').val("true");
}

function cmbViti_SelectedIndexChanged(s, e) {
    var idViti = cmbViti.GetValue();
    $('#HiddenFieldVitiDestinacion').val(idViti);
}

function InitKonvSipasURdherShitje(s, e) {
    if (cbVetemKonvertim.GetChecked())
        cbKonvertimSipasUrdherShitje.SetEnabled(true);
    else {
        cbKonvertimSipasUrdherShitje.SetEnabled(false);
        cbKonvertimSipasUrdherShitje.SetChecked(false);
    }
}

function cbVetemKonvertimClick(s, e) {
    if (cbVetemKonvertim.GetChecked())
        cbKonvertimSipasUrdherShitje.SetEnabled(true);
    else {
        cbKonvertimSipasUrdherShitje.SetEnabled(false);
        cbKonvertimSipasUrdherShitje.SetChecked(false);
    }
}

function shfaqPopUpNdermarrje() {
    window.parent.Utils.shfaqLoadingGif();
    ndertoZgjidhNdermarrje();
}

function ndertoZgjidhNdermarrje() {
    var opsionMbyllje = hfState.Get("buttonMbyll");
    var opsionRuajtje = hfState.Get("buttonRuajNdryshimet");
    var titulliModal = hfState.Get("labelTitulli");
    var popUpOptions = {
        prependSelector: "#backDiv1",
        dialogClass: "dialog-select-menu",
        contentClass: "content-select-menu",
        titulli: titulliModal,
        text: {
            mbyll: opsionMbyllje,
            ruaj: opsionRuajtje
        },
        saveClick: ruajNdermarrjeRol,
        lang: hfState.Get("idGjuha")
    };
    var myPopup = Utils.ndertoPopup(popUpOptions);
    myPopup.modal("show");
    $("." + popUpOptions.contentClass).html("");
    $("." + popUpOptions.contentClass).prepend("<select class='form-control multiselect' multiple='multiple'></select>");
    $.ajax({
        url: Utils.getServerApiUrl("Rregjistrime", "MerrNdermarrjetIdRoli"),
        data: JSON.stringify({
            idRoli: $('#hfShtimModifikim').val() == "modifikim"
                ? gridaRoli.GetRowKey(gridaRoli.GetFocusedRowIndex())
                : 0,
            guidString: hfState.Get("guidString")
        })
    }).done(mbushNdermarrje);
};

function mbushNdermarrje(ndermarrjet) {
    if ($(".ui-multiselect").children().length != 0) {
        $(".multiselect").multiselect("destroy");
        $(".multiselect").html("");
    }
    $.each(ndermarrjet, function (index, item) {
        var tmpOption = new Option(item["Text"], item["IDNDERMARJE"]);
        if (item["Visible"] === 'TRUE')
            $(tmpOption).attr("selected", "selected"); //select it
        $(".multiselect").append(tmpOption);
    });
    $(".multiselect").multiselect({
        sortable: false,
        dividerLocation: 0.5,
        lang: hfState.Get("idGjuha"),
        nodeComparator: function (node1, node2) {
            var text1 = node1.val(),
                text2 = node2.val();
            return text1 == text2 ? 0 : (text1 < text2 ? -1 : 1);
        }
    });
    $(".ui-multiselect").addClass("panel panel-default");
    window.parent.Utils.hiqLoadingGif();
};

function ruajNdermarrjeRol() {
    window.parent.Utils.shfaqLoadingGif();
    console.debug("saving work in progress");
    $(".dialog-select-menu").modal("hide");
    var selected = $(".multiselect").val() || [];
    $.ajax({
        pritPergjigje: true,
        url: Utils.getServerApiUrl("Rregjistrime", "RuajNdermarrjeRolNeSession"),
        data: JSON.stringify({
            selected: selected,
            guidString: hfState.Get("guidString"),
            idRoli: $('#hfShtimModifikim').val() == "modifikim"
                ? gridaRoli.GetRowKey(gridaRoli.GetFocusedRowIndex())
                : 0
        })
    }).done(function (result) {
        $("#hfNdermarjeChanged")[0].value = true;
        var resultString = new Array();
        for (var i = 0; i < result.length; i++) {
            // do bere kontrolli per ekzistuest qe mos te ndryshoje check ????
            var rreshti = result[i];
            ShtoVitPerNdermarrje(rreshti.IDNDERMARJE, rreshti.KODIVITI, "true", rreshti.IDVITI);
        }
        resultString = $("#hfVitetSel")[0].value.split(";");
        var hfArray = new Array();
        for (var i = 0; i < selected.length; i++) {
            var perShtim = resultString.filter(function (item) {
                return item.split(",")[0] == selected[i];
            });
            if (perShtim && perShtim.length > 0)
                hfArray = hfArray.concat(perShtim) // = resultString[i].value.split(",");
        }
        $("#hfVitetSel")[0].value = hfArray.join(";") + ";";

        window.parent.Utils.hiqLoadingGif();
        gvNderRol.PerformCallback($('#hfShtimModifikim').val() == "modifikim"
            ? gridaRoli.GetRowKey(gridaRoli.GetFocusedRowIndex())
            : 0);
    });
};

