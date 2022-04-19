function CmimeApp() {
    this.runMode = false; //per intellisense behet false
    this.view = undefined;
    var app = this;
    function View() {
        var runMode = app.runMode;
        return {
            grida: runMode ? window.gvCmimArtikulli : new ASPxClientGridView(),
            popupUniversal: runMode ? window.popupUniversal : new ASPxClientPopupControl(),
            loadingPanel: runMode ? window.LoadingPanel : new ASPxClientLoadingPanel.Cast(),
            timeoutControll: window.parent.window.parent.SessionTimeout
        };
    }

    if (!app.runMode)
        app.view = new View();

    //variabla global ne faqe te cilet ruajne gjendjen e faqes
    this.pageState = {
        initState: function () {
            /// <summary>
            /// ketu inicializohet gjendja e faqes,lexohen vlera nga hidden field
            /// </summary>

        }
    };

    var models = (function () {

        var CmimeModel = {
            //ketu deklarohen funksione dhe variabla qe manipulojne business rules (imagjino nje klase C#)

        };

        return {
            cmimet: CmimeModel
        };
    })();

    var controllers = (function () {


        var model = models.cmimet;
        var pageState = app.pageState;

        function CmimeController() {
            /// <summary>
            /// ketu shkruhet flow i faqes,psh funksionet qe manipulojen modelin e mesiperm dhe bejne ndryshimet mbi variablat e gjendjes se faqes (pagestate)  dhe  mbi kontrollet e view-se 
            /// </summary>

            // private
            var controllerContext = this;

            // publike

            this.formatoFushaDevi = function () {

            },
                this.unformatoFushaDevi = function () {

                },
                this.ndryshoKonfigFormatNumri = function () {

                },
                this.hidePopupUniversal = function () {
                    app.view.popupUniversal.Hide();
                },
                this.shfaqMesazhPopup = function (mesazhi) {
                    //ky funksion duhet te behet i pergjithshem
                    //ku mund te perdoret nje popup custom jo alert
                    window.alert(mesazhi);
                }
        };

        function handlersController() {
            //private
            var controller = new CmimeController();
            var handlersContext = this;

            //publike

            this.init = function () {
                /// <summary>
                /// ketu inicializohet flow i faqes
                /// </summary>

            }
           
        };

        return {
            Cmime: CmimeController,
            Handlers: handlersController
        };
    })();
    //krijojme nje instance te handlerave sepse duhet te behet publike qe te lidhet me kontrollet
    var handlers = new controllers.Handlers();
    return {
        initApp: function () {
            app.view = new View();
            app.pageState.initState();
            handlers.init();
        },
        models: models,
        handlers: handlers,
        pageState: app.pageState
    };

};

var cmimet = new CmimeApp();
$(document).ready(cmimet.initApp);