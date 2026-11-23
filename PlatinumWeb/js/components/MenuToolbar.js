;
/**Aplikacioni i menuse se ambjenteve */
function MenuToolbar() {
    var app = this;

    /**Klasa model e menuse qe eshte pergjegjese per ndertimin e entiteteve te menuse si dhe lidhjet me serverin (thirrjet ajax) */
    this.model = function () {
        var model = this;

        this.emerKomponente = "";

        this.getMenuItems = function (doneFunction) {
            $.ajax({
                url: Utils.getServerApiUrl("Konfigurime", "GetMenuToolbarItems"),
                data: JSON.stringify({
                    emerKomponente: this.emerKomponente
                })
            }).done(function (result) {
                doneFunction(model.buildMenuItems(result));
            });
        };

        this.buildMenuItems = function (menuItems) {
            return menuItems.map(function (menuItem) { return { icon: menuItem.ImageUrl, text: menuItem.Text, name: menuItem.Name, position: menuItem.Position ? "right" : "left" }; });
        }
    };

    /**Klasa view qe eshte pergjegjese per renderizimet e elementeve te DOM te menuse */
    this.view = function View() {
        var view = this;
        this.onMenuItemClick = null;
        this.menu = null;

        this.onMenuItemOver = function () {
            itemTemplate = $(this);
            itemTemplate.find("span").addClass("whiteText");
            itemTemplate.find("div").css("background-image", "url(" + itemTemplate.data("whiteIcon") + ")");
        }

        this.onMenuItemOut = function () {
            itemTemplate = $(this);
            itemTemplate.find("span").removeClass("whiteText");
            itemTemplate.find("div").css("background-image", "url(" + itemTemplate.data("coloredIcon") + ")");
        }

        this.renderMenu = function () {           
            this.menu = $("<div class='menuToolbar'>").dxMenu({
                items: [],
                focusStateEnabled: false,
                onItemClick: this.onMenuItemClick,
                itemTemplate: function (itemData, itemIndex, itemElement) {
                    var itemTemplate = $("<div>");
                    itemTemplate.mouseenter(view.onMenuItemOver);
                    itemTemplate.mouseleave(view.onMenuItemOut);
                    itemTemplate.data("coloredIcon","images/theme/Avec/menu/" + itemData.icon);
                    itemTemplate.data("whiteIcon", "images/theme/Avec/menu/" + itemData.icon.replace(".png", "_W.png"));
                    var itemTemplateImage = $("<div class='menuItemImage " + itemData.position + "'></div>").css("background-image", "url(images/theme/Avec/menu/" + itemData.icon + ")");
                    var itemTemplateText = $("<span>" + itemData.text + "</span>");
                    itemTemplate.append(itemTemplateImage).append(itemTemplateText);
                    itemElement.append(itemTemplate);
                },
                onItemRendered: function (e) {
                    e.itemElement.parent().addClass(e.itemData.position == "right" ? "rightMenuItem" : "");
                }
            }).dxMenu('instance');
        };

        this.expandAll = function () {
            // pa panelin e navigimit (hequr 2026-11-23): ekran i plote = palosen koka dhe fundi
            var s = window.parent && window.parent.splitter;
            if (!s || !s.GetPane(0) || !s.GetPane(1) || !s.GetPane(2)) return;
            s.GetPane(0).Collapse(s.GetPane(1));
            s.GetPane(2).Collapse(s.GetPane(1));
        };

        this.collapseAll = function () {
            var s = window.parent && window.parent.splitter;
            if (!s || !s.GetPane(0) || !s.GetPane(1) || !s.GetPane(2)) return;
            s.GetPane(0).Expand(s.GetPane(1));
            s.GetPane(2).Expand(s.GetPane(1));
        };
    };

    /**
     * Klasa e controller qe eshte pergjegjese per lidhjen e view me model-in e aplikacionit
     * @param {any} model -> instanca e model-it te aplikacionit
     * @param {any} view -> instanca e view-s se aplikacionit
     */
    this.controller = function (model, view) {
        var controller = this;
        this.menuItemClickEvent = null;
        this.view = view;
        this.model = model;

        this.initializeMenuEvents = function () {
            this.view.onMenuItemClick = this.onMenuItemClick;
        };


        this.onMenuItemClick = function (item) {
            switch (item.itemData.name) {
                case "Expand":
                    controller.view.expandAll();
                    break;
                case "Collapse":
                    controller.view.collapseAll();
                    break;
                default:
                    controller.menuItemClickEvent(item);
                    break;
            }
        };

        this.initializeMenu = function () {
            this.view.renderMenu();
            this.model.getMenuItems(this.applyMenuItems);
        };

        this.applyMenuItems = function (items) {
            controller.view.menu.option("items", items);
        };

        this.getElement = function () {
            return this.view.menu.element();
        };


        (function (controller) {
            controller.initializeMenuEvents();
            controller.initializeMenu();
        }(controller));
    };

    return {
        init: function (emerKomponente, onMenuClick) {
            var model = new app.model(); //inicializohet model-i
            model.emerKomponente = emerKomponente;
            var view = new app.view(); //inicializohet viewe-ri
            var controller = new app.controller(model, view); //inicializohet controller-i
            controller.menuItemClickEvent = onMenuClick;

            return {
                getElement: function () { return controller.getElement(); }
            };
        }
    };
};