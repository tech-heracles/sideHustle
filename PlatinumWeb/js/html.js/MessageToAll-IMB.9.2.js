;
function MessageToAll() {
    var app = this;

    this.model = function () {
        model = this;

        this.emerKomponente = "MessageToAll.html";
        this.IDs = {
        }

        this.getPageConfiguration = function () {
            $.ajax({
                showLoading: true,
                url: Utils.getServerApiUrl("Konfigurime", "GetPageConfigurations"),
                data: JSON.stringify({
                    emerKomponente: this.emerKomponente
                })
            }).done(function (result) {
                model.setPageConfigurations(result);
            });
        };

        this.setPageConfigurations = function (result) {
            model.messages = result.Messages;
        };
    };

    this.view = function () {
        var view = this;

        //#region containers
        this.htmlEditorContainer = null;
        //#endregion

        //#region Html Editor
        this.htmlEditor = null;
        //#endregion

        //#region events
        //#endregion

        this.renderHtmlEditor = function () {
            this.htmlEditorContainer = $("<div />").addClass(".html-editor").appendTo($("#pageContainer"));
            this.htmlEditor = this.htmlEditorContainer.dxHtmlEditor({
                toolbar: {
                    items: [
                        "undo", "redo", "separator",
                        {
                            formatName: "size",
                            formatValues: ["8pt", "10pt", "12pt", "14pt", "18pt", "24pt", "36pt"]
                        }, {
                            formatName: "font",
                            formatValues: ["Arial", "Courier New", "Georgia", "Impact", "Lucida Console", "Tahoma", "Times New Roman", "Verdana"]
                        }, "separator",
                         "bold", "italic", "strike", "underline", "separator",
                        "alignLeft", "alignCenter", "alignRight", "alignJustify", "separator",
                        {
                            formatName: "header",
                            formatValues: [false, 1, 2, 3, 4, 5]
                        }, "separator",
                        "orderedList", "bulletList", "separator",
                        "color", "background", "separator",
                        "image", "separator",
                        "clear", "codeBlock", "blockquote"
                    ]
                },
                mediaResizing: {
                    enabled: true
                },
                height: function () {
                    return window.innerHeight / 1.2;
                },
                placeholder: "Shkruani mesazhin ketu!",
            }).dxHtmlEditor("instance");
        };

        this.dispose = function () {
            //this.wizard.dispose();
        };
    };

    this.controller = function (model, view) {
        var controller = this;

        this.model = model;
        this.view = view;

        this.events = {
            onDispose: function () { console.log("onDispose not implemented"); }
        };

        this.initializePage = function () {
            this.model.getPageConfiguration();
            this.view.renderHtmlEditor();
        };
        
        this.menuToolbarClick = function (item) {
            switch (item.itemData.name) {
                case "Sinkronizo":
                    controller.sendToAllOpenConnection();
                    break;
                case "Preview":
                    controller.previewSendToAllOpenConnection();
                    break;
            }
        }

        this.validoTemplate = function () {
            var isValid = true;

            if (!window.parent.pageState.signalR.isActiv) {
                isValid = false;
                myMesazh.ShtoMesazhGabimi(this.model.messages.MTA_msgAktivizoSignalR);
            }

            var templateSize = Utils.getMemorySizeOf(this.view.htmlEditor.option("value"), true);
            if (templateSize.byte > 30000) {
                isValid = false;
                myMesazh.ShtoMesazhGabimi('KUJDES! Madhesia e mesazhit ka kaluar limitin prej 30KB! Mesazhi i shkruar eshte ' + templateSize.string + '.');
            }

            return isValid;
        }

        this.previewSendToAllOpenConnection = function () {
            if (this.validoTemplate())
                window.parent.showPopup(this.view.htmlEditor.option("value"), this.model.messages.MTA_lblInformacion );
        };

        this.sendToAllOpenConnection = function () {
            if (this.validoTemplate())
                window.parent.doneHub(controller.model.emerKomponente, this.view.htmlEditor.option("value"), this.model.messages.MTA_lblInformacion);
        };


        this.getElement = function () {
            return this.view.htmlEditor;
        };

        this.dispose = function () {
            controller.view.dispose();
            controller.model.dispose();
            controller.events.onDispose();
        };

        (function (controller) {
            window.parent.callWebServiceKtheInfoLart(controller.model.emerKomponente, 0);
            $("#pageContainer").prepend(new MenuToolbar().init(controller.model.emerKomponente, controller.menuToolbarClick).getElement());
            controller.initializePage();
        }(controller));

    };

    return {
        init: function () {
            var model = new app.model(); //inicializohet model-i
            var view = new app.view(); //inicializohet viewe-ri
            var controller = new app.controller(model, view); //inicializohet controller-i

            return {
                getElement: function () { return controller.getElement(); }
            };
        }
    };
}

//Inicializohet aplikacioni i faqes ne document ready
$(window).on("load", function () {
    DevExpress.localization.locale(Utils.getApplicationLanguageId() == 0 ? "al" : "en");
    if (screen.width <= 1366 || screen.width <= 768)
        DevExpress.ui.themes.current("generic.alphaweb.compact");
    new MessageToAll().init();
});
