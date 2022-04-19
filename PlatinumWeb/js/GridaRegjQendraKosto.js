;

function dxQendraKosto() {

    this.view = undefined;
    var app = this;

    function View() {
        return { QKDataGrid: null };
    }

    this.pageState = {
        defaultObject: [{ IdTrupi: 0, IdKoka: 0, IdQK: null, Qendra: null, PershkrimiQendra: null, IdOk: null, Objektiva: null, PershkrimiObj: null, IdLlog: null, Llogaria: null, PershkrimiLlog: null, DebiKredi: 1, MonedhaLlog: null, VleftaLlog: 0, VleftaQK: 0, VleftaMonBaze: 0, KursiLlog: 1, Pershkrimi: null }],
        ambjenti: '',
        qendraKosto: null,
        objektiva: null,
        llogari: null,
        idGjenerues: 0,
        idKonfigGjenerues: 0,
        idKoka: 0,
        idKonfig: 0,
        columnsKonfig: null,
        llogariFK: null,
        dteDtDok: null,
        dteDtRegj: null,
        hfShtimModifikim: null,
        formatNr: 2,
        emerGrida: 'gvQendra',
        emerKomponente: 'LupaRegjistrimQendraKosto.aspx',
        rreshtIndex: null,
        PershkrimiKokes: ""
    };

    var controllers = (function () {

        var pageState = app.pageState;

        function QKController() {

            var controllerContext = this;

            this.InitGridQK = function () {
                app.view.QKDataGrid = controllerContext.CreateGridQK();
                controllerContext.GetLookupColsDataSource();
            };

            this.SaveDocQK = function (kontrolloShperndare, nrdok, nrRef, shenime, idStatusDok, komponenteNga, kontrolloLidhur, dteDtDok, dteDtRegj) {
                var data = app.view.QKDataGrid.GetData().filter(function (e) { return e.Qendra != null && e.Llogaria != null; });
                if (data.length == 0) {
                    myMesazh.ShtoMesazhGabimi(hfState.Get("msgTrupiDokNukDuhetBosh"));
                }
                else
                    if (this.ValidateSaveDocQK(data)) {
                        try {
                            $.ajax({
                                showLoading: true,
                                url: Utils.getServerApiUrl("QendraKosto", "RuajTrupRegjQendraKosto"),
                                data: JSON.stringify({
                                    idKoka: pageState.idKoka,
                                    trupi: data,
                                    nrDok: nrdok,
                                    dteDtDok: dteDtDok,
                                    nrRef: nrRef,
                                    dteDtRegj: dteDtRegj,
                                    shenime: shenime,
                                    idStatusDok: idStatusDok,
                                    idKonfig: pageState.idKonfig,
                                    idGjenerues: pageState.idGjenerues,
                                    idKonfigGjenerues: pageState.idKonfigGjenerues,
                                    komponenteNga: komponenteNga,
                                    hfShtimModifikim: pageState.hfShtimModifikim,
                                    kontrolloLidhur: kontrolloLidhur,
                                    kontrolloShperndare: kontrolloShperndare
                                })
                            }).done(function (result) {
                                if (result.PershkrimMesazhi.indexOf('?') != -1) {
                                    myMesazh.ShtoMesazh({
                                        type: "confirm", modal: true, text: result.PershkrimMesazhi,
                                        cancelClick: function () {
                                            controllerContext.SaveDocQK(false, nrdok, nrRef, shenime, idStatusDok, komponenteNga, kontrolloLidhur, dteDtDok, dteDtRegj);
                                        },
                                        okClick: function () { }
                                    });
                                }
                                else
                                    if (result.Status) {
                                        myMesazh.ShtoMesazhSuksesi(result.PershkrimMesazhi);
                                        controllerContext.ClearGrid();
                                        if (pageState.ambjenti != 'lupa') {
                                            window.$("#hfStatusQendra").val(true);
                                            window.$("#hfStatusRuajtje").val(true);
                                            window.EndRequestHandler();
                                        }
                                        if (window.parent.identifikuesPerPopupBanka) {
                                            if (window.parent.dokRradhes == 0)
                                                window.parent.popupUniversal.Hide();
                                        }
                                        else
                                            window.parent.popupUniversal.Hide();
                                    }
                                    else {
                                        myMesazh.ShtoMesazhGabimi(result.PershkrimMesazhi);
                                        if (window.click != undefined)
                                            window.click = false;
                                    }
                            });
                        } catch (e) {
                            myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimGjateTransferimitTeTeDhenave"));
                            if (window.click != undefined)
                                window.click = false;
                        }
                    }
                    else {
                        myMesazh.ShtoMesazhGabimi(hfState.Get("msgVeprimiNukEshteIKuadruar"));
                        if (window.click != undefined)
                            window.click = false;
                    }
            };

            /**
             * Validon trupin e dokumentit para se te vijoje me ruajtjen e tij
             * @param {Array} data trupi i grides Array Objektesh
             * @return {Boolean} Nese kalon apo jo me sukses validimin e te dhenave 
             */
            this.ValidateSaveDocQK = function (data) {
                var validate = true;

                //Objekti Objektiva nuk duhet te jete null
                data.map(function (rresht) {
                    if (!rresht.IdOk)
                        rresht.IdOk = 0;
                });


                // Ne ambjentin e Regjistrimi te QK, duhet qe vlera debitore dhe kreditore per cdo llogari te jete 0 ne total pra per te gjithe dokumentat qe nuk kane gjenerues
                if (pageState.idGjenerues == 0) {
                    var llogarite = [];
                    data.map(function (rresht) {
                        var uGjet = llogarite.filter(function (e) { return e.IdLlog == rresht.IdLlog; });
                        if (uGjet.length == 0)
                            llogarite.push({ IdLlog: rresht.IdLlog, Total: (rresht.VleftaMonBaze * (rresht.DebiKredi == 1 ? (-1) : 1)) });
                        else
                            uGjet[0].Total = uGjet[0].Total + (rresht.VleftaMonBaze * (rresht.DebiKredi == 1 ? (-1) : 1));
                    });
                    validate = (llogarite.filter(function (e) { return e.Total != 0; }).length == 0 ? true : false);
                }
                return validate;
            };
            /**
             * Krijon Objektin e Grides se Qendrave te Kostos
             * @return {Object} myDxDataGrid 
             */
            this.CreateGridQK = function () {
                var dataGrid = new myDxDataGrid("QKDataGrid", {
                    dataSource: Utils.CloneObject(pageState.defaultObject),
                    keyExpr: 'IdTrupi',
                    showRowLines: true,
                    allowColumnResizing: false,
                    addDeleteRowCommand: true,
                    cancelEditDataCommand: true,
                    focusStateEnabled: true,
                    editing: { mode: "cell", allowUpdating: true, texts: { confirmDeleteMessage: '' } },
                    sorting: { mode: 'none' },
                    setColumnWidth: true,
                    export: {
                        enabled: true,
                        fileName: "ShperndarjaNeQendraKosto",
                        customizeExcelCell: function (e) {
                            e.wrapTextEnabled = true;
                        }
                    },
                    onRowPrepared: function (row) {
                        if (row.rowType == 'data') {
                            if (Math.abs(row.rowIndex) % 2 == 1)
                                row.rowElement.css("background", row.data.Update ? "rgba(255,255,125, 0.2)" : "#ffffff");
                            else
                                row.rowElement.css("background", row.data.Update ? "rgba(255,255,125, 0.4)" : "#f5f5f5");
                        }
                    },
                    onRowUpdated: function (e) {
                        if (e.data.IdTrupi === 0)
                            e.data.IdTrupi = Math.max.apply(Math, app.view.QKDataGrid.GetData().map(function (o) { return o.IdTrupi; })) + 1;
                    },
                    onContentReady: function (e) {                        
                        dataGrid.ShtoRreshtBosh(Utils.CloneObject(pageState.defaultObject)[0], "Llogaria");
                        e.component.columnOption("command:edit", "visible", false);  
                    },
                    onCellPrepared: function (e) {
                        if (e.rowType === "data") {
                            switch (e.column.dataField) {
                                case 'PershkrimiQendra':
                                case 'PershkrimiObj':
                                case 'PershkrimiLlog':
                                case 'MonedhaLlog':
                                    e.cellElement.css("color", "#cccccc");
                                    break;
                                case 'IdLlog':
                                    e.element[0].onchange = function (el) { controllerContext.onChangeDblClickLlogaria(el, e.row.rowIndex, e.column.dataField); };
                                    e.element[0].ondblclick = function (el) { controllerContext.onChangeDblClickLlogaria(el, e.row.rowIndex, e.column.dataField); };
                                    break;
                                case 'IdQK':
                                    e.element[0].onchange = function (el) { controllerContext.onChangeDblClickQendra(el, e.row.rowIndex, e.column.dataField); };
                                    e.element[0].ondblclick = function (el) { controllerContext.onChangeDblClickQendra(el, e.row.rowIndex, e.column.dataField); };
                                    break;
                                case 'IdOk':
                                    e.element[0].onchange = function (el) {
                                        var ob = pageState.objektiva.filter(function (e) { return e.Kodi == el.target.value; });
                                        if (ob[0] != undefined)
                                            app.view.QKDataGrid.Grida.cellValue(e.row.rowIndex, e.column.dataField, ob[0].Id);
                                    };
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                });
                controllerContext.ConfigureQKDataGrid(dataGrid);
                return dataGrid;
            };
            /**
             * Merr dhe percakton konfigurimin qe do te kene kolonat e grides
             * @param {Object} dataGrid gridQK
             * @return {void}
             */
            this.ConfigureQKDataGrid = function (dataGrid) {

                dataGrid.SetColumnsFromConfig(pageState.columnsKonfig);
                dataGrid.AddCustomOptionToColumns(["IdQK"], "setCellValue", function (newData, value, currentRowData) { controllerContext.OnChangeLookUpCols(newData, value, currentRowData, "Qendra"); });
                dataGrid.AddCustomOptionToColumns(["IdOk"], "setCellValue", function (newData, value, currentRowData) { controllerContext.OnChangeLookUpCols(newData, value, currentRowData, "Objektiva"); });
                dataGrid.AddCustomOptionToColumns(["IdLlog"], "setCellValue", function (newData, value, currentRowData) { controllerContext.OnChangeLookUpCols(newData, value, currentRowData, "Llogaria"); });

                //dataGrid.AddCustomOptionToColumns(["IdQK", "IdOk", "IdLlog"], "customizeText", function (cellInfo) { return cellInfo && cellInfo.value ? cellInfo.value.substring(0, cellInfo.value.indexOf(" (")) : ""; });
                //dataGrid.AddCustomOptionToColumns(["IdQK", "IdOk"], "customizeText", function (cellInfo) { return cellInfo && cellInfo.value ? cellInfo.value.substring(0, cellInfo.value.indexOf(" (")) : ""; });
                //dataGrid.AddCustomOptionToColumns(["IdOk"], "customizeText", function (cellInfo) { return cellInfo && cellInfo.value ? cellInfo.value.substring(0, cellInfo.value.indexOf(" (")) : ""; });

                dataGrid.AddCustomOptionToColumns(["VleftaLlog"], "calculateDisplayValue", function (rowData) { return controllerContext.ApplyFormatNumberAccordingCurrency(rowData.MonedhaLlog, rowData.VleftaLlog); });
                dataGrid.AddCustomOptionToColumns(["VleftaMonBaze"], "calculateDisplayValue", function (rowData) { return controllerContext.ApplyFormatNumberAccordingCurrency(rowData.MonedhaLlog, rowData.VleftaMonBaze); });
                dataGrid.AddCustomOptionToColumns(["VleftaQK"], "calculateDisplayValue", function (rowData) { return controllerContext.ApplyFormatNumberAccordingCurrency(rowData.MonedhaLlog, rowData.VleftaQK); });

                dataGrid.AddCustomOptionToColumns(["VleftaLlog", "VleftaQK", "VleftaMonBaze"], "visible", true);
                dataGrid.AddCustomOptionToColumns(["DebiKredi"], "setCellValue", controllerContext.OnChangeDebiKredi);
                switch (pageState.ambjenti) {
                    case 'regj':
                        dataGrid.AddCustomOptionToColumns(["VleftaLlog"], "setCellValue", function (newData, value, currentRowData) { controllerContext.OnChangeVleftat(newData, value, currentRowData, "vleftallog"); });
                        dataGrid.AddCustomOptionToColumns(["VleftaQK"], "setCellValue", function (newData, value, currentRowData) { controllerContext.OnChangeVleftat(newData, value, currentRowData, "vleftaQK"); });
                        dataGrid.AddCustomOptionToColumns(["VleftaMonBaze"], "setCellValue", function (newData, value, currentRowData) { controllerContext.OnChangeVleftat(newData, value, currentRowData, "vleftamon"); });
                        break;
                    case 'fk':
                        dataGrid.AddCustomOptionToColumns(["VleftaLlog"], "setCellValue", function (newData, value, currentRowData) { controllerContext.OnChangeVleftatMeKurs(newData, value, currentRowData, "vleftallog"); });
                        dataGrid.AddCustomOptionToColumns(["VleftaMonBaze"], "setCellValue", function (newData, value, currentRowData) { controllerContext.OnChangeVleftatMeKurs(newData, value, currentRowData, "vleftamon"); });
                        dataGrid.AddCustomOptionToColumns(["VleftaQK"], "setCellValue", function (newData, value, currentRowData) { controllerContext.OnChangeVleftatMeKurs(newData, value, currentRowData, "vleftaQK"); });
                        break;
                    case 'lupa':
                        dataGrid.AddCustomOptionToColumns(["VleftaQK", "VleftaMonBaze"], "visible", false);
                        break;
                    default:
                        break;
                }

            };

            /** Merr me API datasource qe i duhet per konfiguruar griden e Qendrave te Kostos */
            this.GetGridConfigurationsDataSource = function () {
                if (pageState.columnsKonfig == null)
                    $.ajax({
                        showLoading: true,
                        async: false,
                        url: Utils.getServerApiUrl("Konfigurime", "GetGridColumnsByIdKonfig"),
                        data: JSON.stringify({
                            emerGrida: pageState.emerGrida,
                            emerKomponente: pageState.emerKomponente,
                            idKonfig: pageState.idKonfig
                        })
                    }).done(function (result) {
                        pageState.columnsKonfig = result;
                    });
            };

            /** Merr te API datasource qe i duhet per konfiguruar kolonat si lookup ne griden e Qendrave te Kostos*/
            this.GetLookupColsDataSource = function () {
                $.ajax({
                    showLoading: true,
                    url: Utils.getServerApiUrl("QendraKosto", "KtheDataSourceKolonash"),
                    data: JSON.stringify({
                        idKokaFleteKontabel: pageState.idGjenerues,
                        gjitheLlogarite: pageState.ambjenti == 'regj'
                    })
                }).done(function (result) {
                    controllerContext.ConfigureLookupCols(result);
                });
            };
            /**
             * Konfiguron ne Griden e Qendrave te Kostos kolonat si lookup dhe i jep vlere
             * @param {any} ds objekt me datasource per qendrat e kostos, objektivat dhe llogarite
             */
            this.ConfigureLookupCols = function (ds) {

                function HapLupeLlogarie(rowIndex) {
                    pageState.rreshtIndex = rowIndex;
                    popupUniversal.SetHeaderText("Zgjidh Llogarine");
                    var url = "";
                    switch (pageState.ambjenti) {
                        case 'regj':
                            url = 'LupaLlogaria.aspx?vjennga=RegjistrimQendraKostoDXDATAGRID';
                            break;
                        default:
                            url = 'LupaLlogaria.aspx?vjennga=RegjistrimQendraKostoDXDATAGRID&id=' + pageState.idGjenerues.toString();
                            break;
                    }
                    popupUniversal.SetContentUrl(url);
                    popupUniversal.SetSize(750, 600);
                    popupUniversal.Show();
                }
                function merrLlogari(value, rowIndex, perAcList) {
                    var objektet = pageState.llogari.filter(function (el) { return el.NrLlogari.toLowerCase().includes(value.toString().toLowerCase()) || el.EmerLlogari1.toLowerCase().includes(value.toString().toLowerCase()); });
                    if (!perAcList) {
                        var objekti = objektet.filter(function (el) { return el.NrLlogari.toLowerCase()== value.toString().toLowerCase() || el.EmerLlogari1.toLowerCase()== value.toString().toLowerCase(); });
                        if (!objekti) return [];
                        app.view.QKDataGrid.Grida.cellValue(rowIndex, "IdLlog", objekti.IdLlogari);
                    }
                    else 
                        return objektet;
                }
                function onValueChangedLlogaria(rowIndex, objekti) {
                    if (objekti && objekti.IdLlogari) {
                        var currentRowData = app.view.QKDataGrid.GetData()[rowIndex];
                        controllerContext.OnChangeLookUpCols({}, objekti.IdLlogari, currentRowData, "Llogaria", rowIndex);
                    }
                }
                function HapLupeQendre(rowIndex) {
                    pageState.rreshtIndex = rowIndex;
                    popupUniversal.SetHeaderText("Zgjidh qendren e kostos");
                    var url = "Shto_QendraKosto.aspx?lupe=true&llojLupe=plote&vjenNga=RegjistrimQendraKostoDXDATAGRID";
                    popupUniversal.SetContentUrl(url);
                    popupUniversal.SetSize(750, 600);
                    popupUniversal.Show();
                }
                function merrQendra(value, rowIndex, perAcList) {
                    var objektet = pageState.qendraKosto.filter(function (el) { return el.Kodi.toLowerCase().includes(value.toString().toLowerCase()) || el.Pershkrimi.toLowerCase().includes(value.toString().toLowerCase()); });
                    if (!perAcList) {
                        var objekti = objektet[0];
                        if (!objekti) return [];
                        app.view.QKDataGrid.Grida.cellValue(rowIndex, "IdQK", objekti.Id);
                    }
                    else return objektet;
                }
                function onValueChangedQendra(rowIndex, objekti) {
                    if (objekti && objekti.Id) {
                        var currentRowData = app.view.QKDataGrid.GetData()[rowIndex];
                        controllerContext.OnChangeLookUpCols({}, objekti.Id, currentRowData, "Qendra", rowIndex);
                    }
                }
                function HapLupeObjektive(rowIndex) {
                    pageState.rreshtIndex = rowIndex;
                    popupUniversal.SetHeaderText("Zgjidh objektiven e kostos");
                    var url = "LupaObjektivaKosto.aspx?vjenNga=RegjistrimQendraKostoDXDATAGRID";
                    popupUniversal.SetContentUrl(url);
                    popupUniversal.SetSize(750, 600);
                    popupUniversal.Show();
                }
                function merrObjektiva(value, rowIndex, perAcList) {
                    if (value == "") return [];
                    var objektet = pageState.objektiva.filter(function (el) { return el.Kodi.toLowerCase().includes(value.toString().toLowerCase()) || el.Pershkrimi.toLowerCase().includes(value.toString().toLowerCase()); });
                    if (!perAcList) {
                        var objekti = objektet[0];
                        if (!objekti) return [];
                        app.view.QKDataGrid.Grida.cellValue(rowIndex, "IdOk", objekti.Id);
                    }
                    else return objektet;
                }
                function onValueChangedObjektive(rowIndex, objekti) {
                    if (objekti && objekti.Id) {
                        var currentRowData = app.view.QKDataGrid.GetData()[rowIndex];
                        controllerContext.OnChangeLookUpCols({}, objekti.Id, currentRowData, "Objektiva", rowIndex);
                    }
                }
                
                pageState.qendraKosto = ds.qendraKosto;
                pageState.objektiva = ds.objektivaKosto;
                pageState.llogari = ds.llogari;

                app.view.QKDataGrid.AddCustomOptionToColumns(["DebiKredi"], "lookup", { dataSource: [{ value: 1, name: "Debi" }, { value: 2, name: "Kredi" }], displayExpr: "name", valueExpr: "value" });
                //app.view.QKDataGrid.AddCustomOptionToColumns(["IdQK"], "lookup", { dataSource: ds.qendraKosto, valueExpr: "Id", displayExpr: function (item) { return item.Kodi + ' (' + item.Pershkrimi + ')'; } });
                //app.view.QKDataGrid.AddCustomOptionToColumns(["IdOk"], "lookup", { dataSource: ds.objektivaKosto, allowClearing: true, valueExpr: "Id", displayExpr: function (item) { return item.Kodi + ' (' + item.Pershkrimi + ')'; } });
                //app.view.QKDataGrid.AddCustomOptionToColumns(["IdLlog"], "lookup", { dataSource: ds.llogari, valueExpr: "IdLlogari", displayExpr: function (item) { return item.NrLlogari + ' (' + item.EmerLlogari1 + ')'; } });
                
                var llogariaAutoCompleteColumns = [{ dataField: "NrLlogari", capField: "Llogaria", width: 2 }, { dataField: "EmerLlogari1", capField: "Pershkrimi", width: 7 }, { dataField: "KodiMonedha", capField: "Monedha", width: 1 }];
                app.view.QKDataGrid.AddAutocompleteToColumn("Llogaria", "IdLlog", "PershkrimiLlog", merrLlogari, "Zgjidhni llogarine...", "IdLlogari", "NrLlogari", "EmerLlogari1", HapLupeLlogarie, onValueChangedLlogaria, llogariaAutoCompleteColumns);

                var qendraAutoCompleteColumns = [{ dataField: "Kodi", capField: "Qendra", width: 4 }, { dataField: "Pershkrimi", capField: "Pershkrimi", width: 8 }];
                app.view.QKDataGrid.AddAutocompleteToColumn("Qendra", "IdQK", "PershkrimiQendra", merrQendra, "Zgjidhni qendren e kostos...", "Id", "Kodi", "Pershkrimi", HapLupeQendre, onValueChangedQendra, qendraAutoCompleteColumns);

                var objektivaAutoCompleteColumns = [{ dataField: "Kodi", capField: "Objektiva", width: 4 }, { dataField: "Pershkrimi", capField: "Pershkrimi", width: 8 }];
                app.view.QKDataGrid.AddAutocompleteToColumn("Objektiva", "IdOk", "PershkrimiObj", merrObjektiva, "Zgjidhni objektiven...", "Id", "Kodi", "Pershkrimi", HapLupeObjektive, onValueChangedObjektive, objektivaAutoCompleteColumns);

                controllerContext.GetGridQKDataSource();
            };

            /** Merr me API dokumentin ekzitues te Qendrave te kostos duke u mbeshtetur mbi informacionin se nga po hapet ambjenti i regjistrimit */
            this.GetGridQKDataSource = function () {
                switch (pageState.hfShtimModifikim) {
                    case 'shtim':
                        app.view.QKDataGrid.SetDataSource(Utils.CloneObject(pageState.defaultObject));
                        app.view.QKDataGrid.Refresh();
                        break;
                    default:
                        $.ajax({
                            showLoading: true,
                            url: Utils.getServerApiUrl("QendraKosto", "KtheKokaQKSipasIDGjeneruesDheKonfig"),
                            data: JSON.stringify({
                                idGjenerues: pageState.idGjenerues,
                                idKonfig: (pageState.ambjenti != 'regj') ? pageState.idKonfigGjenerues : pageState.idKonfig,
                                idKoka: pageState.idKoka,
                                sipasKokes: pageState.ambjenti == 'regj'
                            })
                        }).done(function (result) {
                            controllerContext.SetGridDataSource(result.trupi);
                        });
                        break;
                }
            };
            /**
             * Vendos datasource per griden. Ben update totalet e Debi Kredi nese ndodhen ne ambjent.
             * @param {any} data    array me te dhena
             */
            this.SetGridDataSource = function (data) {
                pageState.idKoka = (pageState.ambjenti != 'regj' && data.length > 0 && data[0].IdKoka != 0) ? data[0].IdKoka : pageState.idKoka;

                data = data.length == 0 ? Utils.CloneObject(pageState.defaultObject) : data.map(function (row) { if (row.IdOk == 0) row.IdOk = null; return row; });

                //data.sort(function (a, b) { return (a.IdTrupi > b.IdTrupi) ? 1 : -1 });

                app.view.QKDataGrid.SetDataSource(data);

                app.view.QKDataGrid.Refresh();
                if (pageState.ambjenti == 'regj')
                    window.llogaritTotalet(controllerContext.KtheTotaleDebiKrediMonBaze(data.length == 0 ? Utils.CloneObject(pageState.defaultObject) : data));
            };

            /** Ploteson objektin perkates per trupin e dokumentit gjenerues (FK) qe e merr me WS */
            this.GetBodyOfGeneratorDok = function () {
                try {
                    $.ajax({
                        async: true,
                        url: Utils.getServerApiUrl("QendraKosto", "MerrTrupFKSipasKokes"),
                        data: JSON.stringify({
                            idKoka: pageState.idGjenerues
                        })
                    }).done(function (result) {
                        pageState.llogariFK = result;
                    });
                } catch (e) {
                    myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimGjateTransferimitTeTeDhenave"));
                }
            };

            /**
             * Ne varesi te monedhes se llogarise qe eshte zgjedhur ne rresht, gjendet formati i vendosur per te
             * @param {any} monedha kodi i monedhes se rreshtit
             * @param {any} vlera   vlera qe duhet shfaqur e 
             * @returns {any} kthen formatin
             */
            this.ApplyFormatNumberAccordingCurrency = function (monedha, vlera) {
                if (!monedha) return vlera;
                var konf = pageState.formatNr.KonfigTrupi.filter(function (el) { return el.KodMonedhe == monedha; })[0];
                if (!konf || !konf.ShifraPasPresjesVlefta) return vlera;
                var textToDisplay = vlera.toFixed(konf.ShifraPasPresjesVlefta).split('.');
                textToDisplay[0] = textToDisplay[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                return textToDisplay.join('.');
            };
            /**
             * Ploteson fushat e kodit dhe pershkrimit sipas kolones Look-up te zgjedhur.
             * Ne rastet kur fusha plotesohet Llogaria, sugjerohen fushat e Vlefta, Vlefta QK dhe Vlefta Mon Baze (per vleren e mbetur)
             * @param {any} newData         rreshti me vlerat e reja per tu ruajtur
             * @param {any} value           vlera e plotesuar e kolones Look-up 
             * @param {any} currentRowData  rreshti aktual i ruajtur (pa ndryshimet e bera)
             * @param {any} fusha           emertimi i fushesh qe po modifikohet
             * @param {any} rreshtIndex     indeksi i rreshtit te grides qe po modifikohet; nuk duhet ti jepet vlere kur metoda therritet nga trigerimi i ndryshimit te kolonave.
             */
            this.OnChangeLookUpCols = function (newData, value, currentRowData, fusha, rreshtIndex) {
                if (rreshtIndex != undefined && rreshtIndex != null) {
                    currentRowData = app.view.QKDataGrid.GetData()[rreshtIndex];
                    newData = currentRowData;
                }
                switch (fusha) {
                    //#region Qendra
                    case "Qendra":
                        var qendra = pageState.qendraKosto.filter(function (x) { return x.Id == value; })[0];
                        if (!qendra) {
                            myMesazh.ShtoMesazhGabimi('Kjo qender kosto nuk ekziston!');
                            break; 
                        }
                        newData.PershkrimiQendra = qendra.Pershkrimi;
                        newData.Qendra = qendra.Kodi;
                        newData.IdQK = value;

                        if (currentRowData.Llogaria != null)
                            app.view.QKDataGrid.Grida.cellValue(app.view.QKDataGrid.Grida.getRowIndexByKey(currentRowData.IdTrupi), "IdLlog", currentRowData.IdLlog);

                        break;
                    //#endregion
                    //#region Objektiva
                    case "Objektiva":
                        var objektiva = pageState.objektiva.filter(function (x) { return x.Id == value; })[0];
                        if (objektiva && (new Date(objektiva.Nga) > new Date(pageState.dteDtDok) || (objektiva.Deri.substring(0, 10) != '0001-01-01' && new Date(objektiva.Deri) < new Date(pageState.dteDtDok)))) {
                            newData.PershkrimiObj = null;
                            newData.Objektiva = null;
                            newData.IdOk = null;
                            myMesazh.ShtoMesazhGabimi('Ky objektiv nuk eshte aktiv ne kete date');
                        }
                        else {
                            newData.PershkrimiObj = value ? objektiva.Pershkrimi : null;
                            newData.Objektiva = value ? objektiva.Kodi : null;
                            newData.IdOk = value;
                        }
                        break;
                    //#endregion
                    //#region Llogaria
                    case "Llogaria":
                        if (pageState.ambjenti == 'lupa') {
                            var llog = pageState.llogariFK.filter(function (e) { return e.IdLlogari == value; });
                            if (llog[0] == undefined) return;
                            newData.PershkrimiLlog = llog[0].EmerLlogari;
                            newData.Llogaria = llog[0].NrLlogari;
                            newData.IdLlog = value;
                            newData.DebiKredi = (llog[0].DK == 'D') ? 1 : 2;
                            newData.MonedhaLlog = llog[0].KodMonedha;

                            var vlerat = controllerContext.MerrVleratPerLlogarine(llog, currentRowData.IdTrupi);
                            if (vlerat.ndryshoDebiKredi) {
                                newData.DebiKredi = (newData.DebiKredi == 1) ? 2 : 1;
                            }
                            newData.VleftaLlog = vlerat.vleraLlog;
                            newData.VleftaQK = vlerat.vleraQk;
                            newData.VleftaMonBaze = vlerat.vleraMonbaze;
                            break;
                        }

                        var index = app.view.QKDataGrid.Grida.getRowIndexByKey(currentRowData.IdTrupi);
                        var llog = pageState.llogari.filter(function (e) { return e.IdLlogari == value; });
                        newData.IdLlog = value;
                        if (llog[0] == undefined) return;
                        newData.Llogaria = llog[0].NrLlogari;
                        newData.PershkrimiLlog = llog[0].EmerLlogari1;
                        newData.MonedhaLlog = llog[0].KodiMonedha;

                        if (pageState.ambjenti == 'fk') {
                            $.ajax({
                                async: false,
                                url: Utils.getServerApiUrl("QendraKosto", "merrPershkrimLlog"),//idLlog, idGjenerues, idMonedhaQendra, data
                                data: JSON.stringify({ idLlog: value, idGjenerues: pageState.idGjenerues, idMonedhaQendra: pageState.qendraKosto.filter(function (x) { return x.Kodi == currentRowData.Qendra; })[0] != undefined ? pageState.qendraKosto.filter(function (x) { return x.Kodi == currentRowData.Qendra; })[0].IdMonedha : 0, data: pageState.dteDtDok })
                            }).done(function (result) {
                                if (result.DebiKredi != null) {
                                    var data = app.view.QKDataGrid.GetData();
                                    var totLlog = result.VleftaDebiTrupiFK;
                                    var totMonBaze = result.VleftaDebiTrupiFKMonBaze;
                                    var kursiqk = result.kursiQK;
                                    var i = 0;
                                    data.map(function (e) {
                                        if (e.IdLlog == value && index != i) //duhen perjashtuar vlerat e rreshtit ku po behet modifikimi => ecin me : currentRowData.VleftaLlog = 0; currentRowData.VleftaMonBaze = 0; ??
                                            if (e.DebiKredi == (result.DebiKredi == 'D' ? 1 : 2)) {
                                                totLlog -= e.VleftaLlog;
                                                totMonBaze -= e.VleftaMonBaze;
                                            }
                                            else {
                                                totLlog += e.VleftaLlog;
                                                totMonBaze += e.VleftaMonBaze;
                                            }
                                        i++;
                                    });
                                    if (data[index] == undefined) return;
                                    data[index].DebiKredi = (totLlog < 0) ? ((result.DebiKredi == 'D' ? 1 : 2) == 1 ? 2 : 1) : (result.DebiKredi == 'D' ? 1 : 2);
                                    data[index].VleftaLlog = Math.abs(totLlog);
                                    data[index].VleftaMonBaze = Math.abs(totMonBaze);
                                    data[index].VleftaQK = Math.abs(totMonBaze) / kursiqk;
                                    app.view.QKDataGrid.Refresh();
                                }
                            });
                        }
                        else
                            if (hfShtimModifikim.value == 'shtim' && pageState.ambjenti == 'regj') {
                                $.ajax({
                                    async: false,
                                    url: Utils.getServerApiUrl("QendraKosto", "merrPershkrimLlogQKRe"),
                                    data: JSON.stringify({ idLlog: value, vlefta: currentRowData.VleftaLlog, kodqendra: currentRowData.Qendra, data: pageState.dteDtDok })
                                }).done(function (result) {
                                    if (result.nrLlog == '') {
                                        myMesazh.ShtoMesazhGabimi(hfState.Get("msgKjoLlogariNukEkziston") + " / " + hfState.Get("msgLlogariaNukEshtePerTuShperndareNeQendraKosto"));
                                        return;
                                    }
                                    newData.DebiKredi = 1;
                                    newData.MonedhaLlog = result.kodiMonedha;
                                    newData.VleftaLlog = currentRowData.VleftaLlog;
                                    newData.VleftaMonBaze = result.vlera;
                                    newData.VleftaQK = result.vleraQK;
                                });
                            }
                        break;
                    //#endregion
                    default:
                        break;
                }
                app.view.QKDataGrid.SaveCurrentValues();
            };
            /**
             * Kthen vlerat e sugjeruara per llogarine e zgjedhur
             * @param {any} llog        array me vlerat vlerat e llogarive te prekura (psh. trupi i FK)
             * @param {any} IdTrupi     key i rreshtit qe po modifikohet ne gride
             * @returns {object} vlerat e sugjeruara
             */
            this.MerrVleratPerLlogarine = function (llog, IdTrupi) {
                var data = app.view.QKDataGrid.GetData().filter(function (e) { return e.Llogaria == llog[0].NrLlogari && e.IdTrupi != IdTrupi; });
                var totali = 0;
                llog.map(function (e) { return totali = e.VleftaDebiTrupiFleteKontabel; });
                data.map(function (e) { if (e.DebiKredi == ((llog[0].DK == 'D') ? 1 : 2)) return totali -= e.VleftaLlog; else return totali += e.VleftaLlog; });
                var ndryshoDebiKredi = (totali < 0);
                totali = Math.abs(totali);
                return { vleraMonbaze: totali, vleraQk: totali, vleraLlog: totali, ndryshoDebiKredi: ndryshoDebiKredi };
            };
            /**
             * Menaxhon rillogaritjen e vlerave per llogarine e zgjedhur ne rastet kur plotesohet shume shpejt nr i llogarise dhe kalohet me tab ne elementin tjeter, ose kur bejm double click tek llogaria.
             * @param {any} el          elementi perkates
             * @param {any} rowIndex    indeksi i rreshtit ku po behet modifikimi
             * @param {any} dataField   emertimi i fushes qe po modifikohet
             */
            this.onChangeDblClickLlogaria = function (el, rowIndex, dataField) {
                var llog = pageState.llogari.filter(function (e) { return e.NrLlogari == el.target.value; });
                if (llog[0] != undefined)
                    app.view.QKDataGrid.Grida.cellValue(rowIndex, dataField, llog[0].IdLlogari);
            };
            /**
             * Menaxhon vendosjen e qendres se kostos per kodin qe eshte shkruar ose rimerr dhe njehere te dhenat ne double click te saj
             * @param {any} el          elementi perkates
             * @param {any} rowIndex    indeksi i rreshtit ku po behet modifikimi
             * @param {any} dataField   emertimi i fushes qe po modifikohet
             */
            this.onChangeDblClickQendra = function (el, rowIndex, dataField) {
                var qk = pageState.qendraKosto.filter(function (e) { return e.Kodi == el.target.value; });
                if (qk[0] != undefined)
                    app.view.QKDataGrid.Grida.cellValue(rowIndex, dataField, qk[0].Id);
            };

            /**
             * Vendos per rreshtat e ruajtur te grides (ne rast modifikimi) ose rreshta qe jane shtuar (rast regjistrimi dok qk) te njejten qender kosto qe zgjidhet ose skemen (ne kete rast ndahen vlerat sipas qendrave te kostos dhe perqindjeve te vendosura ne konfigurimin e skemes)
             * Therret nje WS
             * @param {any} lloji   percakton nese do vendoset Qender Kosto apo Skeme Kosto
             * @param {any} kodi    kodi i llojit te zgjedhur me lart
             * @param {any} shtim   boolean per te treguar nese jemi ne shtim apo modifikim dok QK
             */
            this.VendosQKapoSkeme = function (lloji, kodi, shtim) {
                if (shtim && lloji == 1) {
                    var data = app.view.QKDataGrid.GetData();
                    var ob = pageState.qendraKosto.filter(function (e) { return e.Kodi == kodi; })[0];
                    data = data.map(function (e) {
                        e.IdQK = ob.Id;
                        e.Qendra = ob.Kodi;
                        e.PershkrimiQendra = ob.Pershkrimi;
                        return e;
                    });
                    app.view.QKDataGrid.Refresh();
                }
                else
                    $.ajax({
                        showLoading: true,
                        url: Utils.getServerApiUrl("QendraKosto", "VendosQKoseSkemeNeGride"),
                        data: JSON.stringify({ idGjenerues: pageState.idGjenerues, lloji: lloji, kodi: kodi })
                    }).done(function (result) {
                        pageState.defaultObject[0].Pershkrimi = pageState.PershkrimiKokes;
                        controllerContext.SetGridDataSource(result.length < 1 ? Utils.CloneObject(pageState.defaultObject) : result);
                        if (pageState.PershkrimiKokes != "")
                            controllerContext.VendosPershkrim(pageState.PershkrimiKokes);
                        app.view.QKDataGrid.Refresh();
                        Utils.hiqLoadingGif();
                    });
            };
            /**
             * Vendos objektiven e kostos se zgjedhur per te gjithe rreshtat qe ka grida
             * @param {any} kodi    kodi i Objektives se Kostos
             */
            this.VendosObjektive = function (kodi) {
                var data = app.view.QKDataGrid.GetData().filter(function (e) { return e.IdQK != 0; });
                var ob = pageState.objektiva.filter(function (e) { return e.Kodi == kodi; })[0];
                data = data.map(function (e) {
                    e.IdOk = ob.Id;
                    e.Objektiva = ob.Kodi;
                    e.PershkrimiObj = ob.Pershkrimi;
                    return e;
                });
                app.view.QKDataGrid.Refresh();
            };
            /**
             * Vendos pershkrimin e kokes qe vendos perdoruesi ne te gjithe rreshtat qe ka grida
             * @param {any} text    kodi i Objektives se Kostos
             */
            this.VendosPershkrim = function (text) {
                var data = app.view.QKDataGrid.GetData();
                data = data.map(function (e) {
                    e.Pershkrimi = text;
                    return e;
                });
                app.view.QKDataGrid.Refresh();
            };
            /**
             * Perditeson fushat e totaleve Debi Kredi nese ka te tilla nga ambjenti
             * @param {any} newData             rreshti me vlerat e reja per tu ruajtur
             * @param {any} value               vlera e vendosur tek njera nga fushat per modifikim
             * @param {any} currentRowData      rreshti aktual i ruajtur (pa ndryshimet e bera)
             */
            this.OnChangeDebiKredi = function (newData, value, currentRowData) {
                newData.DebiKredi = value;
                var data = app.view.QKDataGrid.GetData();
                data[app.view.QKDataGrid.Grida.getRowIndexByKey(currentRowData.IdTrupi)].DebiKredi = value;
                if (pageState.ambjenti == 'regj')
                    window.llogaritTotalet(controllerContext.KtheTotaleDebiKrediMonBaze(data));
            };
            /**
             * Llogarit fushat e vleftave ne varesi te modifikimit te ndonjeres prej tyre
             * @param {any} newData             rreshti me vlerat e reja per tu ruajtur
             * @param {any} value               vlera e vendosur tek njera nga fushat per modifikim
             * @param {any} currentRowData      rreshti aktual i ruajtur (pa ndryshimet e bera)
             * @param {any} kolona              emertimi i fushes qe po modifikohet
             */
            this.OnChangeVleftat = function (newData, value, currentRowData, kolona) {
                var index = app.view.QKDataGrid.Grida.getRowIndexByKey(currentRowData.IdTrupi);
                try {
                    $.ajax({
                        async: true,
                        url: Utils.getServerApiUrl("QendraKosto", "LlogaritVleraQK"),
                        data: JSON.stringify({
                            nrllogari: currentRowData.Llogaria, kodqendra: currentRowData.Qendra, data: pageState.dteDtDok, vlefta: value, lloji: kolona
                        })
                    }).done(function (result) {
                        controllerContext.ChangeVleftat(result, index);
                    });
                } catch (e) {
                    myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimGjateTransferimitTeTeDhenave"));
                }
            };
            /**
             * Llogarit fushat e vleftave ne varesi te modifikimit te ndonjeres prej tyre sipas kursit te fundit ose te trupit
             * @param {any} newData             rreshti me vlerat e reja per tu ruajtur
             * @param {any} value               vlera e vendosur tek njera nga fushat per modifikim
             * @param {any} currentRowData      rreshti aktual i ruajtur (pa ndryshimet e bera)
             * @param {any} kolona              emertimi i fushes qe po modifikohet
             */
            this.OnChangeVleftatMeKurs = function (newData, value, currentRowData, kolona) {
                var index = app.view.QKDataGrid.Grida.getRowIndexByKey(currentRowData.IdTrupi);
                var llog = pageState.llogariFK.filter(function (e) { return e.IdLlogari == currentRowData.IdLlog; });
                var kursi = llog[0].Kursi;
                try {
                    $.ajax({
                        pritPergjigje: true,
                        url: Utils.getServerApiUrl("QendraKosto", "LlogaritVleraQKMeKursTrupi"),
                        data: JSON.stringify({ nrllogari: currentRowData.Llogaria, kodqendra: currentRowData.Qendra, data: pageState.dteDtDok, vlefta: value, lloji: kolona, kursiTrupi: kursi })
                    }).done(function (result) {
                        controllerContext.ChangeVleftat(result, index);
                    });
                } catch (e) {
                    myMesazh.ShtoMesazhGabimi(hfState.Get("msgGabimGjateTransferimitTeTeDhenave"));
                }
            };
            /**
             * Perditeson fushat e Vleftave sipas parametrit te kaluar ne indeksin perkates
             * @param {any} input       objekti me 3 fushat e vleftave
             * @param {any} rowIndex    indeksi i rreshtit ne gride
             */
            this.ChangeVleftat = function (input, rowIndex) {
                var row = app.view.QKDataGrid.GetData()[rowIndex];
                row.VleftaLlog = input.vleftallog;
                row.VleftaMonBaze = input.vleftamonbaze;
                row.VleftaQK = input.vleftaqk;
                app.view.QKDataGrid.Refresh();
                if (pageState.ambjenti == 'regj')
                    window.llogaritTotalet(controllerContext.KtheTotaleDebiKrediMonBaze());
            };
            /**
             * Llogarit totalet e Debi Kredi per rreshtat e grides
             * @param {any} dt  array - datasource i grides
             * @returns {object} me debi dhe kredi te llogaritura
             */
            this.KtheTotaleDebiKrediMonBaze = function (dt) {
                var data = (dt != null && dt != undefined) ? dt : app.view.QKDataGrid.GetData();
                var debi = 0, kredi = 0;
                data.map(function (e) { if (e.DebiKredi == 1) debi += e.VleftaMonBaze; else kredi += e.VleftaMonBaze; });
                return { Debi: debi, Kredi: kredi };
            };
            /**
             * Ben griden te editueshme apo jo ne varesi te parametrit qe i kalohet
             * @param {any} vlera   boolean - true nese grida do te jete disable - false nese do te jete editable
             */
            this.SetGridEditableOrNot = function (vlera) {
                app.view.QKDataGrid.Grida._options.disabled = vlera;
                app.view.QKDataGrid.Grida.option("editing").allowDeleting = !vlera;
            };
            /** Pastron griden - vendos datasource default */
            this.ClearGrid = function () {
                if (app.view != undefined && app.view.QKDataGrid != undefined) {
                    app.view.QKDataGrid.Grida.option('dataSource', Utils.CloneObject(pageState.defaultObject));
                    app.view.QKDataGrid.Refresh();
                }
            };
            /** Kontrollon per ndryshime kursi ne ndryshimin e dates */
            this.KontrolloNdryshimeKursiNgaNdryshimiDates = function () {
                var data = app.view.QKDataGrid.GetData();
                data = data.map(function (e) {
                    controllerContext.OnChangeVleftat(e, e.VleftaLlog, e, 'vleftallog')
                    return e;
                });
            };
            this.VendosQKdheLlogariNeGride = function (idQK, idLlog) {
                app.view.QKDataGrid.ShtoRreshtBosh(Utils.CloneObject(pageState.defaultObject)[0], "Llogaria");
                if (idQK > 0)
                    app.view.QKDataGrid.Grida.cellValue(app.view.QKDataGrid.GetData().length - 1, "IdQK", idQK);
                if (idLlog > 0)
                    app.view.QKDataGrid.Grida.cellValue(app.view.QKDataGrid.GetData().length - 1, "IdLlog", idLlog);
            };
            this.VendosQKdheLlogariDheObjektiveNeGrideNgaLupat = function (idQk, idLlog, idOk) {
                var currentRowData = app.view.QKDataGrid.GetData()[pageState.rreshtIndex];
                if (idQk > 0) {
                    controllerContext.OnChangeLookUpCols({}, idQk, currentRowData, "Qendra", pageState.rreshtIndex);
                    var cell = app.view.QKDataGrid.Grida.getCellElement(pageState.rreshtIndex, "Qendra");
                    app.view.QKDataGrid.Grida.focus(cell);
                    pageState.rreshtIndex = null;
                }
                if (idLlog > 0) {
                    controllerContext.OnChangeLookUpCols({}, idLlog, currentRowData, "Llogaria", pageState.rreshtIndex);
                    var cell = app.view.QKDataGrid.Grida.getCellElement(pageState.rreshtIndex, "Llogaria");
                    app.view.QKDataGrid.Grida.focus(cell);
                    pageState.rreshtIndex = null;
                }
                if (idOk > 0) {
                    controllerContext.OnChangeLookUpCols({}, idOk, currentRowData, "Objektiva", pageState.rreshtIndex);
                    var cell = app.view.QKDataGrid.Grida.getCellElement(pageState.rreshtIndex, "Objektiva");
                    app.view.QKDataGrid.Grida.focus(cell);
                    pageState.rreshtIndex = null;
                }
            };
        }

        function handlersController() {

            var controllerQK = new QKController();

            /**
            * Krijon Griden e Qendrave te Kostos.
            * Mbush me vlera objektin e te dhenave pageState.
            * @param {object} objAmbjenti Konfigurime nga ambjenti qe e therret // { ngaThirret: {String}, formatNr: {Numeric}, konfigurimGride: {Object} }
            * @param {string} veprimi Po hapet per modifikim apo shtim // shtim, modifikim
            * @param {object} objDokumentiQK Te dhena per objektin e QK // { idKoka: {Numeric}, dteDtDok: {Date}, dteDtRegj: {Date}, idKonfig: {Numeric}}
            * @param {object} objDokumentiGjenerues Te dhena per objektin gjenerues // { idDokGjenerues: {Numeric}, idKonfigGjenerues: {Numeric}, llogariFK: {Object}}
            * @return {void}  
            */
            this.initGridQK = function (objAmbjenti, veprimi, objDokumentiQK, objDokumentiGjenerues) {

                pageState.ambjenti = objAmbjenti.ngaThirret;
                pageState.PershkrimiKokes = objAmbjenti.PershkrimiKokes;
                pageState.defaultObject[0].Pershkrimi = pageState.PershkrimiKokes;
                pageState.hfShtimModifikim = veprimi;
                pageState.formatNr = (objAmbjenti.formatNr != null && objAmbjenti.formatNr != undefined) ? objAmbjenti.formatNr : 2;
                pageState.idKoka = (objDokumentiQK.idKoka) ? objDokumentiQK.idKoka : 0;
                pageState.dteDtDok = objDokumentiQK.dteDtDok;
                pageState.dteDtRegj = objDokumentiQK.dteDtRegj;
                pageState.idKonfig = objDokumentiQK.idKonfig ? objDokumentiQK.idKonfig : 0;
                pageState.idGjenerues = objDokumentiGjenerues.idDokGjenerues ? objDokumentiGjenerues.idDokGjenerues : 0;
                pageState.idKonfigGjenerues = objDokumentiGjenerues.idKonfigGjenerues ? objDokumentiGjenerues.idKonfigGjenerues : 0;

                if (objAmbjenti.konfigurimGride)
                    pageState.columnsKonfig = objAmbjenti.konfigurimGride;
                else
                    controllerQK.GetGridConfigurationsDataSource();

                if (objDokumentiGjenerues.llogariFK == null && objAmbjenti.ngaThirret == 'fk')
                    controllerQK.GetBodyOfGeneratorDok();
                else
                    pageState.llogariFK = objDokumentiGjenerues.llogariFK;

                controllerQK.InitGridQK();
            };
            this.SetGridEditableOrNot = function (vlera) {
                controllerQK.SetGridEditableOrNot(vlera);
            };
            this.ClearGrid = function () {
                controllerQK.ClearGrid();
            };
            this.ruajGride = function (kontrolloShperndare, nrdok, nrRef, shenime, idStatusDok, komponenteNga, kontrolloLidhur, dteDtDok, dteDtRegj) {
                controllerQK.SaveDocQK(kontrolloShperndare, nrdok, nrRef, shenime, idStatusDok, komponenteNga, kontrolloLidhur, dteDtDok, dteDtRegj);
            };
            this.vendosQKapoSkemeNeGride = function (lloji, kodi, shtim) {
                controllerQK.VendosQKapoSkeme(lloji, kodi, shtim);
            };
            this.vendosObjektiveNeGride = function (Kodi) {
                controllerQK.VendosObjektive(Kodi);
            };
            this.vendosPershkrimNeGride = function (text) {
                pageState.PershkrimiKokes = text;
                controllerQK.VendosPershkrim(text);
            };
            this.ktheTotaleDebiKrediMonBaze = function () {
                return controllerQK.KtheTotaleDebiKrediMonBaze();
            };
            this.kontrolloNdryshimeKursiNgaNdryshimiDates = function (dteDtDok) {
                pageState.dteDtDok = dteDtDok;
                controllerQK.KontrolloNdryshimeKursiNgaNdryshimiDates();
            };
            this.vendosQKdheLlogariNeGride = function (idQK, idLlog) {
                controllerQK.VendosQKdheLlogariNeGride(idQK, idLlog);
            };
            this.VendosQKdheLlogariDheObjektiveNeGrideNgaLupat = function (idQK, idLlog, idOk) {
                controllerQK.VendosQKdheLlogariDheObjektiveNeGrideNgaLupat(idQK, idLlog, idOk);
            };
        }

        return {
            QK: QKController,
            Handlers: handlersController
        };
    })();

    var handlers = new controllers.Handlers();

    return {
        /**
        * Public: Thirr funksionin publik te clases se Qendrave te Kostos per te Krijuar grides sipas ambjentit qe e therret
        * @param {Object} objAmbjenti Konfigurime nga ambjenti qe e therret // { ngaThirret: {String}, formatNr: {Numeric}, konfigurimGride: {Object} }
        * @param {String} veprimi Po hapet per modifikim apo shtim // shtim, modifikim
        * @param {Object} objDokumentiQK Te dhena per objektin e QK // { idKoka: {Numeric}, dteDtDok: {Date}, dteDtRegj: {Date}, idKonfig: {Numeric}}
        * @param {Object} objDokumentiGjenerues Te dhena per objektin gjenerues // { idDokGjenerues: {Numeric}, idKonfigGjenerues: {Numeric}, llogariFK: {Object}}
        * @return {void}  
        */
        initGridQK: function (objAmbjenti, veprimi, objDokumentiQK, objDokumentiGjenerues) {
            app.view = new View();
            handlers.initGridQK(objAmbjenti, veprimi, objDokumentiQK, objDokumentiGjenerues);
            return app.view;
        },
        setGridEditableOrNot: function (vlera) {
            handlers.SetGridEditableOrNot(vlera);
        },
        clearGrid: function () {
            handlers.ClearGrid();
        },
        ruajRegjQK: function (kontrolloShperndare, nrdok, nrRef, shenime, idStatusDok, komponenteNga, kontrolloLidhur, dteDtDok, dteDtRegj) {
            handlers.ruajGride(kontrolloShperndare, nrdok, nrRef, shenime, idStatusDok, komponenteNga, kontrolloLidhur, dteDtDok, dteDtRegj);
        },
        vendosObjektiveNeGride: function (Kodi) {
            handlers.vendosObjektiveNeGride(Kodi);
        },
        vendosQKapoSkemeNeGride: function (lloji, kodi, shtim) {
            handlers.vendosQKapoSkemeNeGride(lloji, kodi, shtim);
        },
        ktheTotaleDebiKrediMonBaze: function () {
            return handlers.ktheTotaleDebiKrediMonBaze();
        },
        kontrolloNdryshimeKursiNgaNdryshimiDates: function (dteDtDok) {
            handlers.kontrolloNdryshimeKursiNgaNdryshimiDates(dteDtDok);
        },
        vendosQKdheLlogariNeGride: function (idQK, idLlog) {
            handlers.vendosQKdheLlogariNeGride(idQK, idLlog);
        },
        VendosQKdheLlogariDheObjektiveNeGrideNgaLupat: function (idQK, idLlog, idOk) {
            handlers.VendosQKdheLlogariDheObjektiveNeGrideNgaLupat(idQK, idLlog, idOk);
        },
        vendosPershkrimNeGride: function (text) {
            handlers.vendosPershkrimNeGride(text);
        }
    };
}
