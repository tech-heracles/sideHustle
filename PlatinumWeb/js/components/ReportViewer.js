;
function ReportViewer() {
    var app = this;
    /**Klasa e model qe eshte pergjegjese per logjiken e entiteteve ne aplikacion (faqe) si dhe per lidhjen me serverin (thirrjet ajax) */
    this.model = function () {
        var model = this;

        this.parameters = {
            entity: "",
            entityCode: "",
            year: 1000,
            month: "",
            monthId: 0
        };

        this.getReportTemplates = function (filterTemplate, gridTemplate) {
            var templates = new Array();
            if (filterTemplate)
                templates.push({ name: "Filtra", template: function () { return filterTemplate; } });

            if (gridTemplate)
                templates.push({ name: "Raport", template: function () { return gridTemplate; } });

            return templates;
        };

        this.getPeriodSummaryDetailsReport = function (entityCode, year, monthId, doneFunction) {
            $.ajax({
                url: Utils.getServerApiUrl("MbylljePeriudhe", "GetPeriodSummaryDetailsReport"),
                data: JSON.stringify({
                    entityCode: entityCode,
                    year: year,
                    month: monthId
                })
            }).done(function (result) {
                doneFunction(model.buildReportData(result));
            });
        };

        this.buildReportData = function (result) {
            var i = 0;
            result.dataSource.map(function (item) { item.index = ++i; });
            return {
                dataSource: result.dataSource,
                columnNames: result.columnNames
            };
        }
    };

    /**Klasa e view qe eshte pergjegjese per renderizimet e elementeve te DOM te aplikacionit (faqes) */
    this.view = function View() {
        var view = this;

        this.reportContainer = null;             

        this.renderReportAccordation = function (templates) {
            view.reportContainer = $("<div>");
            view.reportContainer.dxAccordion({
                animationDuration: 300,
                collapsible: true,
                multiple: true,
                selectedItems: [templates[0]],
                itemTitleTemplate: function (e) { return $("<div>").html(e.name); },
                items: templates
            });
        };

        this.renderReportGrid = function (exportFileName) {
            view.gridContainer = $("<div>");
            view.reportGrid = new myDxDataGrid(view.gridContainer, {
                showRowNumber: true,
                editing: {
                    allowUpdating: false
                },
                scrolling: {
                    mode: "standart"
                },
                height: '100%',
                paging: {
                    pageSize: 10
                },
                groupPanel: {
                    visible: true
                },
                grouping: {
                    autoExpandAll: true,
                },
                pager: {
                    showPageSizeSelector: true,
                    allowedPageSizes: [10, 20, 50],
                    showInfo: true
                },
                hoverStateEnabled: true,
                showRowLines: true,
                filterRow: {
                    visible: true
                },
                filterPanel: {
                    visible: true
                },
                columnChooser: {
                    enabled: true,
                    mode: "select"
                },
                export: {
                    enabled: true,
                    fileName: exportFileName
                }
            }, true);
        };
    };

    /**
     * Klasa e controller qe eshte pergjegjese per lidhjen e view me model-in e aplikacionit (faqes)
     * @param {any} model -> instanca e model-it te popup-it
     * @param {any} view -> instanca e view-s se popup-it
     */
    this.controller = function (model, view) {
        var controller = this;

        this.view = view;
        this.model = model;
        
        this.initializeReport = function () {
            this.view.renderReportGrid(this.model.parameters.entityCode);
            this.view.renderReportAccordation(this.model.getReportTemplates(null, this.view.gridContainer));
            this.triggerReportAccordionSelectionChanged();
        };
        
        this.triggerReportAccordionSelectionChanged = function () {
            this.view.reportGrid.Grida.beginCustomLoading();
            this.model.getPeriodSummaryDetailsReport(this.model.parameters.entityCode, this.model.parameters.year, this.model.parameters.monthId, function (result) { controller.applyReportDataSource(result); });
        }

        this.applyReportDataSource = function (data) {
            this.view.reportGrid.SetDataSource(data.dataSource);
            this.view.reportGrid.SetNativeColumns(this.CreateColumns(data.columnNames));
            this.view.reportGrid.Grida.option('summary.totalItems', this.CreateTotalItems()); 
            this.view.reportGrid.Grida.endCustomLoading();
        };
        
        this.CreateColumns = function (columnNames) {
            switch (this.model.parameters.entityCode) {
                case "M_KONTABILITETI":
                    return this.CreateColumnsKontabiliteti(columnNames);
            }
        };

        this.CreateColumnsKontabiliteti = function (columnNames) {
            return [{
                caption: columnNames.nrLlogarie,
                dataField: "NrLlogarie"
            }, {
                caption: columnNames.emerLlogarie,
                dataField: "EmerLlogarie"
            }, {
                caption: columnNames.monedha,
                dataField: "Monedha"
            }, {
                caption: columnNames.gjendjaMonBaze,
                alignment: "center",
                columns: [{
                    caption: columnNames.debi,
                    dataField: "GjendjaDebiMonBaze",
                    format: { type: "fixedPoint", precision: 2 }
                }, {
                    caption: columnNames.kredi,
                    dataField: "GjendjaKrediMonBaze",
                    format: { type: "fixedPoint", precision: 2 }
                }]
            }, {
                caption: columnNames.gjendjaMonLlogarie,
                alignment: "center",
                columns: [{
                    caption: columnNames.debi,
                    dataField: "GjendjaDebi",
                    format: { type: "fixedPoint", precision: 2 }
                }, {
                    caption: columnNames.kredi,
                    dataField: "GjendjaKredi",
                    format: { type: "fixedPoint", precision: 2 }
                }]
            }];
        };

        this.CreateTotalItems = function () {
            return [{
                column: "GjendjaDebiMonBaze",
                summaryType: "sum",
                displayFormat: "{0}",
                valueFormat: { type: "fixedPoint", precision: 2 }
            }, {
                column: "GjendjaKrediMonBaze",
                summaryType: "sum",
                displayFormat: "{0}",
                valueFormat: { type: "fixedPoint", precision: 2 }
            }];
        };

        this.getElement = function () {
            return this.view.reportContainer;
        };

        (function (controller) {
            controller.initializeReport();
        }(controller));
    };

    return {
        init: function (parameters) {
            var model = new app.model(); //inicializohet model-i
            model.parameters = parameters;
            var view = new app.view(); //inicializohet viewe-ri
            var controller = new app.controller(model, view); //inicializohet controller-i

            return {
                getElement: function () { return controller.getElement(); }
            };
        }
    }
};