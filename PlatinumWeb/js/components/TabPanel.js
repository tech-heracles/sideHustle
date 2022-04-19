;
function TabPanel() {
    var app = this;

    /**Klasa e model qe eshte pergjegjese per logjiken e entiteteve ne aplikacion (faqe) si dhe per lidhjen me serverin (thirrjet ajax) */
    this.model = function () {
        this.tabs = [];
    };

    /**Klasa e view qe eshte pergjegjese per renderizimet e elementeve te DOM te aplikacionit (faqes) */
    this.view = function View() {
        var view = this;
        this.tabPanel = null;

        this.closeButtonOnClick = null;

        this.renderTabPanel = function (tabs) {
            this.tabPanelContainer = $("<div>");
            this.tabPanel = view.tabPanelContainer.dxTabPanel({
                selectedIndex: 0,
                animationEnabled: true,
                loop: false,
                scrollByContent: true,
                showNavButtons: true,
                repaintChangesOnly: true,
                itemTitleTemplate: this.renderTabPanelTitleTemplate,
                dataSource: tabs
            }).dxTabPanel("instance");
        };

        this.renderTabPanelTitleTemplate = function (itemData) {           
            var div = $('<div>');
            var span = $('<span>' + itemData.name + '</span>');
            var btn = $('<span class="dx-icon dx-icon-close"</span>').addClass('close-icon').on('click', function () { view.closeButtonOnClick(itemData); });
            div.append(span).append(btn);
            return div;
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

        this.initializeEvents = function () {
            this.view.closeButtonOnClick = this.removeTab;
        };

        this.initializeTabPanel = function () {
            this.view.renderTabPanel(this.model.tabs);
        };

        this.removeTab = function (itemData) {
            var index = controller.model.tabs.indexOf(itemData);
            controller.model.tabs.splice(index, 1);
            controller.view.tabPanel.getDataSource().reload();
            controller.view.tabPanel.repaint();
        }

        this.addTab = function (tabKey, tabName, contentFunction) {
            if (this.existTabByKey(tabKey))
                return;

            var item = {
                key: tabKey,
                name: tabName,
                template: function () {
                    var container = $("<div>").append(contentFunction());
                    container.dxScrollView({ height: '95%' });
                    return container;
                }
            };
            this.model.tabs.push(item);
            this.view.tabPanel.getDataSource().reload();
            this.view.tabPanel.option('selectedItem', item);
        };
        
        this.existTabByKey = function (tabKey) {
            if (!tabKey)
                return false;

            var item = this.getTabByKey(tabKey);
            if (item) {
                this.view.tabPanel.option('selectedItem', item);
                return true;  
            }
            return false;
        };

        this.getTabByKey = function (tabKey) {
            var item = this.model.tabs.filter(function (item) { return item.key == tabKey })[0];
            return item;
        };

        this.getElement = function () {
            return this.view.tabPanelContainer;
        };

        (function (controller) {
            controller.initializeEvents();
            controller.initializeTabPanel();
        }(controller));
    };

    return {
        init: function () {
            var model = new app.model(); //inicializohet model-i
            var view = new app.view(); //inicializohet viewe-ri
            var controller = new app.controller(model, view); //inicializohet controller-i
       
            return {
                getElement: function () { return controller.getElement() },
                addNewElementItem: function (tabKey, tabName, contentFunction) { controller.addTab(tabKey, tabName, contentFunction); }
            };
        }
    }
};