;
/* Klasa e popup e personalizuar
 * 
 */
function Popup() {
    var app = this;

    this.model = function () {
        model = this;

        this.IDs = {
            dxPopupID: null
        };

        this.templates = {
            dxPopupContent: null
        };

        this.options = null;        
    };

    this.view = function () {
        var view = this;

        //#region containers
        this.popupContainer = null;
        //#endregion

        //#region Popup
        this.popup = null;
        //#endregion

        //#region events
        //#endregion

        this.renderPopup = function (dxPopupID, template, options) {
            var dxPopupDefaultConfiguration = {
                width: "50%",
                height: "60%",
                contentTemplate: function (container) {
                    var scrollView = $("<div id='scrollView'></div>");
                    var content = $("<div />").append(template);

                    scrollView.append(content);
                    scrollView.dxScrollView({ height: '100%', width: '100%' });

                    container.append(scrollView);
                    return container;
                },
                visible: false,
                dragEnabled: true,
                resizeEnabled: true,
                closeOnOutsideClick: true
            };
            var dxPopupConfiguration = Object.assign(options, dxPopupDefaultConfiguration);

            this.popupContainer = $("<div />").addClass("popup").appendTo($(dxPopupID)); 
            this.popup = this.popupContainer.dxPopup(dxPopupConfiguration).dxPopup("instance");

            $("#showButton").dxButton({
                text: "Show the Popup",
                onClick: function () {
                    $("#popup").dxPopup("show");
                    // === or ===
                    $("#popup").dxPopup("toggle", true);
                }
            });
            $("#hideButton").dxButton({
                text: "Hide the Popup",
                onClick: function () {
                    $("#popup").dxPopup("hide");
                    // === or ===
                    $("#popup").dxPopup("toggle", false);
                }
            });
        };

        this.renderPopupContentTemplate = function (container) {
            var scrollView = $("<div id='scrollView'></div>");
            var content = $("<div />").append(template);

            scrollView.append(content);
            scrollView.dxScrollView({ height: '100%', width: '100%' });

            container.append(scrollView);
            return container;
        } 

        this.dispose = function () {
            this.popup.dispose();
        };
    };

    this.controller = function (model, view) {
        var controller = this;

        this.model = model;
        this.view = view;

        this.events = {
            onDispose: function () { console.log("onDispose not implemented"); }
        };
        
        this.initializePopup = function () {
            this.view.renderPopup(this.model.IDs.dxPopupID, this.model.templates.dxPopupContent, this.model.options);
        };   

        this.getElement = function () {
            return this.view.popup;
        };

        this.dispose = function () {
            controller.view.dispose();
            controller.model.dispose();
            controller.events.onDispose();
        };
                
        (function (controller) {
            controller.initializePopup();
            controller.view.popup.show();
        }(controller));
    };

    return {
        init: function (IDs, templates, options) {
            var model = new app.model(); //inicializohet model
            model.IDs.dxPopupID = IDs;
            model.templates.dxPopupContent = templates;
            model.options = options ? options : {};

            var view = new app.view(); //inicializohet view
            var controller = new app.controller(model, view); //inicializohet controller

            return {
                getElement: function () { return controller.getElement(); }
            };
        }
    };
}