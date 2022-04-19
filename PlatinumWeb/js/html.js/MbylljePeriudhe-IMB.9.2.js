;
function MbylljePeriudhe() {
    var app = this;

    /**Klasa e model qe eshte pergjegjese per logjiken e entiteteve ne aplikacion (faqe) si dhe per lidhjen me serverin (thirrjet ajax) */
    this.model = function () {
        var model = this;

        this.emerKomponente = "MbylljePeriudhe.html";
        this.messages = null;
        this.periodSummaries = null;
        this.months = null;
        this.entities = null;

        this.getPageConfiguration = function (doneFunction) {
            $.ajax({
                showLoading: true,
                url: Utils.getServerApiUrl("MbylljePeriudhe", "GetPageConfigurations")
            }).done(function (result) {
                model.setPageConfigurations(result);
                doneFunction();
            });
        };

        this.getPeriodSummaries = function (doneFunction) {
            $.ajax({
                url: Utils.getServerApiUrl("MbylljePeriudhe", "getPeriodSummaries")
            }).done(function (result) {
                doneFunction(result);
            });
        };

        this.setPageConfigurations = function (result) {
            model.messages = result.Messages;
            model.periodSummaries = result.PeriodSummaries;
            model.months = result.Months;
            model.entities = result.Entities;
        };

    };

    /**Klasa e view qe eshte pergjegjese per renderizimet e elementeve te DOM te aplikacionit (faqes) */    
    this.view = function View() {
        var view = this;
        this.gridContainer = null;
        this.grid = null;
        this.toolbar = null;
        this.renderReportDrawer = null;
        this.reportDrawer = null;
        this.tabPanel = null;

        this.renderGrid = function () {
            view.gridContainer = $("<div>");
            view.grid = new myDxDataGrid(view.gridContainer, {
                dataSource: [{IdPeriodSummary:0}],
                keyExpr: "IdPeriodSummary",
                editing: {
                    allowUpdating: false
                },
                paging: {
                    pageSize: 25
                },
                height: '100%',
                pager: {
                    showPageSizeSelector: true,
                    allowedPageSizes: [10, 25, 50],
                    showInfo: true
                },
                scrolling: {
                    mode: "Standard"
                },
                filterRow: {
                    visible: true
                },
                selection: {
                    mode: "multiple",
                    showCheckBoxesMode: "always"
                },
                export: {
                    enabled: true,
                    fileName: "PeriodClosing",
                    allowExportSelectedData: true
                },
                showRowLines: true
            }, true);
        };

        this.renderReportDrawer = function (template) {
            view.reportDrawer = $("#drawer").dxDrawer({
                opened: true,
                position: 'left',
                openedStateMode: 'push',
                animationDuration: 1000,
                height: 'inherit',
                template: function () { return template; }
            }).dxDrawer("instance");
        }

        this.renderToolbar = function () {
            view.toolbar = $("#toolbar").dxToolbar({
                items: [{
                    widget: "dxButton",
                    location: "before",
                    options: {
                        icon: "chevrondoubleleft",
                        onClick: function (e) {
                            var option = view.toolbar.option("items[0].options.icon");

                            if (option == "chevrondoubleleft") {
                                view.toolbar.option("items[0].options.icon", "chevrondoubleright");
                            } else {
                                view.toolbar.option("items[0].options.icon", "chevrondoubleleft");
                            };

                            view.reportDrawer.toggle();
                        }
                    }
                }]
            }).dxToolbar("instance");
        }

        this.renderTabPanel = function () {
            view.tabPanel = new TabPanel().init();
            $("#tabPanel").addClass("mbylljePeriudheTab").append(view.tabPanel.getElement());
        }

    };

    /**
    * Klasa e controller qe eshte pergjegjese per lidhjen e view me model-in e aplikacionit (faqes)
    * @param {any} model -> instanca e model-it te faqes
    * @param {any} view -> instanca e view-s se faqes
    */
    this.controller = function (model, view) {
        var controller = this;

        this.view = view;
        this.model = model;
                
        this.initializePage = function () {
            this.view.renderGrid();
            this.view.renderReportDrawer(this.view.gridContainer);
            this.view.renderToolbar();
            this.view.renderTabPanel();
            this.model.getPageConfiguration(this.applyPageConfigurations);
        };

        this.menuToolbarClick = function (item) {
            switch (item.itemData.name) {
                case "MbyllPeriudhe":
                    controller.initMbylljePeriudheWizard();
                    break;
                case "Rifresko":
                    controller.refreshGridDataSource();
                    break;
            }
        }

        this.applyPageConfigurations = function () {
            controller.view.grid.SetColumnsFromConfig(controller.CreateColumns(), controller.CreateCommandColumn());
            controller.view.grid.AddDataSourceToColumn("IdModuli", controller.model.entities, "IdModuli", "PershkrimiModuli", false);
            controller.view.grid.AddDataSourceToColumn("Muaji", controller.model.months, "IdMuaji", "KodiMuaji", false);
            controller.setGridDataSource(controller.model.periodSummaries);
        };

        this.refreshGridDataSource = function () {
            this.view.grid.Grida.beginCustomLoading();
            this.model.getPeriodSummaries(function (dataSource) { controller.setGridDataSource(dataSource); });
        };

        this.setGridDataSource = function (dataSource) {
            this.view.grid.SetDataSource(dataSource);
            this.view.grid.Grida.endCustomLoading();
        }

        this.openPeriodSummaryReport = function (rowData) {
            if (rowData) {
                var entity = this.model.entities.filter(function (item) { return item.IdModuli == rowData.IdModuli; })[0];
                var month = this.model.months.filter(function (item) { return item.IdMuaji == rowData.Muaji; })[0];
                var parameters = {
                    entity: entity.PershkrimiModuli,
                    entityCode: entity.KodiModuli,
                    year: rowData.Viti,
                    month: month.KodiMuaji,
                    monthId: month.IdMuaji
                };

                var tabName = parameters.year + " - " + parameters.month;
                this.view.tabPanel.addNewElementItem(tabName, tabName, function () { return new ReportViewer().init(parameters).getElement(); });

                view.toolbar.option("items[0].options.icon", "chevrondoubleright");
                this.view.reportDrawer.hide();
            }
        };

        this.initMbylljePeriudheWizard = function () {
            new MbylljePeriudheWizard().init(this.model.messages, function () { controller.refreshGridDataSource(); });
        };


        this.CreateColumns = function() {
            var columns = new Array();
            columns.push({ KodiTrupi: "IdModuli", PershkrimiTrupi: this.model.messages.MP_colEntiteti, ReadonlyTrupi: true, VisibleTrupi: true, TipiFushes: "string" });
            columns.push({ KodiTrupi: "DtFillimi", PershkrimiTrupi: this.model.messages.MP_colDataMbylljes, ReadonlyTrupi: false, VisibleTrupi: true, TipiFushes: "date" });
            columns.push({ KodiTrupi: "Krijuesi", PershkrimiTrupi: this.model.messages.MP_colKrijuesi, ReadonlyTrupi: false, VisibleTrupi: true, TipiFushes: "string" });
            columns.push({ KodiTrupi: "Viti", PershkrimiTrupi: this.model.messages.MP_colViti, ReadonlyTrupi: false, VisibleTrupi: true, TipiFushes: "string" });
            columns.push({ KodiTrupi: "Muaji", PershkrimiTrupi: this.model.messages.MP_colMuaji, ReadonlyTrupi: false, VisibleTrupi: true, TipiFushes: "string" });
            return columns;
        };

        this.CreateCommandColumn = function () {
            return {
                width: 65,
                visibleIndex: 0,
                type: "buttons",
                buttons: [{
                    hint: "Raport",
                    icon: "doc",
                    onClick: function (e) {
                        controller.openPeriodSummaryReport(e.row.data);
                    }
                }]
            };
        };

        (function (controller) {
            window.parent.callWebServiceKtheInfoLart("MbylljePeriudhe.html", 0);
            $("#pageContainer").prepend(new MenuToolbar().init(controller.model.emerKomponente, controller.menuToolbarClick).getElement());
            controller.initializePage();
        }(controller));
    };

    return {
        init: function () {
            var model = new app.model(); //inicializohet model-i
            var view = new app.view(); //inicializohet viewe-ri
            var controller = new app.controller(model, view); //inicializohet controller-i
        }
    };
};

//Inicializohet aplikacioni i faqes ne document ready
$(window).on("load", function () {
    DevExpress.localization.locale(Utils.getApplicationLanguageId() == 0 ? "al" : "en");
    if (screen.width <= 1366 || screen.width <= 768)
        DevExpress.ui.themes.current("generic.alphaweb.compact");
    new MbylljePeriudhe().init();
});
