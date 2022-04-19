;
function ZbritjeAnalitikeApp() {
    this.runMode = true; //per intellisense behet false
    this.view = undefined;
    var app = this;
    function View() {
        var runMode = app.runMode;
        return {
            hfState: runMode ? window.hfState : new ASPxClientHiddenField(),
            gvZbritjeAnalitike: runMode ? window.gvZbritjeAnalitike : new ASPxClientGridView(),
            popupUniversal: runMode ? window.popupUniversal : new ASPxClientPopupControl(),
            loadingPanel: runMode ? window.LoadingPanel : new ASPxClientLoadingPanel(),
            timeoutControll: window.parent.window.parent.SessionTimeout,
            lblSelektuar: runMode ? window.lblSelektuar : new ASPxClientLabel(),
            cmbKonfigurimi: runMode ? window.cmbKonfigurimi : new ASPxClientComboBox(),
            lblKonfigurimiP: runMode ? window.lblKonfigurimiP : new ASPxClientLabel(),
            lblMsgbox: runMode ? window.lblMsgbox : new ASPxClientLabel(),
            dteDtFillimi2: runMode ? window.dteDtFillimi2 : new ASPxClientDateEdit(),
            dteDtMbarimi2: runMode ? window.dteDtMbarimi2 : new ASPxClientDateEdit(),
            cmbVlerePerqindje: runMode ? window.cmbVlerePerqindje : new ASPxClientComboBox(),
            txtVlera: runMode ? window.txtVlera : new ASPxClientTextBox(),
            lblVlera: runMode ? window.lblVlera : new ASPxClientLabel()
        };
    }

    if (!app.runMode)
        app.view = new View();

    //variabla global ne faqe te cilet ruajne gjendjen e faqes
    this.pageState = {
        
        konfigFillestar: "",
        focusedColumn: undefined,
        idPerdoruesi: 0,
        idNdermarrje: 0,
        idGjuha: 0,
        formatNumriZgjedhur: "",
       
        msgCmimeArtikulliArtikujTeNdryshuarPorTePaSelektuar: "",
        msgCmimeArtikulliDoniTeVazhdoni: "",
        initState: function () {
            this.idPerdoruesi = app.view.hfState.Get("idPerdoruesi");
            this.idNdermarrje = hfState.Get("idNdermarrje");
            this.idGjuha = hfState.Get("idGjuha");
            this.msgCmimeArtikulliArtikujTeNdryshuarPorTePaSelektuar = hfState.Get("msgCmimeArtikulliArtikujTeNdryshuarPorTePaSelektuar");
            this.msgCmimeArtikulliDoniTeVazhdoni = hfState.Get("msgCmimeArtikulliDoniTeVazhdoni");
            this.konfigFillestar = JSON.parse(hfState.Get("konfigFillestar"));
            
        }
    };

    var models = (function () {

        var ZbritjeModel = {
            numriTeSelektuar: 0,
            numriTeNdryshuar: 0,
            merrRreshtaTeNdryshuar: function () {
                var teNdryshuar = app.view.gvZbritjeAnalitike["cpTeNdryshuar"];
                if (typeof (teNdryshuar) == "undefined")
                    teNdryshuar = new Array();
                return JSON.parse(teNdryshuar);
            },
            merrRreshtaTeSelektuar: function () {
                var teSelektuar = app.view.gvZbritjeAnalitike["cpTeSelektuar"];
                if (typeof (teSelektuar) == "undefined")
                    return new Array();
                return JSON.parse(teSelektuar);
            },
            merrRreshtaTeNdryshuarJoTeSelektuar: function () {

                var TeNdryshuarJoTeSelektuar = app.view.gvZbritjeAnalitike["cpTeNdryshuarJoTeSelektuar"];
                if (typeof (TeNdryshuarJoTeSelektuar) == "undefined")
                    return new Array();
                return JSON.parse(TeNdryshuarJoTeSelektuar);
            }

        };

        return {
            zbritjet: ZbritjeModel
        };
    })();

    var controllers = (function () {

        var model = models.zbritjet;

        var pageState = app.pageState;

        function ZbritjeController() {


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
            },
            this.SucceededCallbackKonfig = function (result) {
                app.view.timeoutControll.sendKeepAlive();
                if (typeof (result) !== "undefined") {
                    var colKontrollet = result.colKontrollet;
                    var colAtrTrupi = result.colAtrTrupi;
                    var hf = $("#hfKontrollet");
                    $("#divgride1").show();
                    var arrPrind = ["dvVlera"];
                    var arrTabela = ["tblVlera"];
                    myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, hf, undefined, "", arrTabela, undefined, undefined, undefined, arrPrind);
                    gvZbritjeAnalitike.PerformCallback("ShtoCmBazeNeGride");
                    app.view.txtVlera.SetVisible(true);
                    pageState.formatNumriZgjedhur = result.formatNumriZgjedhur;
                }
            },
            this.callWebserviceKonfigurimi = function (idKomp, kodKonf) {

                $.ajax({
                    url: Utils.getServerApiUrl("Konfigurime", "ktheKonfig"),
                    data: JSON.stringify({
                        idKomp: idKomp,
                        kodKonf: kodKonf,
                        idNdermarrje: pageState.idNdermarrje,
                        idGjuha: pageState.idGjuha
                    })
                }).done(controllerContext.SucceededCallbackKonfig);


            },
            this.ndryshoKonfigurimin = function () {
                var konfigText = app.view.cmbKonfigurimi.GetText();

                if (konfigText.split(";").length > 1)
                    app.view.lblKonfigurimiP.SetText(konfigText.split(";")[1]);

                app.view.cmbKonfigurimi.SetText(konfigText.split(";")[0]);
                controllerContext.callWebserviceKonfigurimi("423", konfigText);
            },

            this.changeName = function () {

                window.parent.callWebServiceKtheInfoLart('ZbritjeAnalitike.aspx', 0);


            },
            this.NdryshoCmimetESelektuar = function () {

                if (model.numriTeSelektuar < 1) {
                    myMesazh.ShtoMesazhGabimi("Ju nuk keni rreshta te selektuar!");
                    return;
                }
                app.view.gvZbritjeAnalitike.PerformCallback("ndryshoZbritjet");
            },
            this.Ruaj = function (kontrolluar) {
                app.view.gvZbritjeAnalitike.PerformCallback("Ruaj;" + kontrolluar);
                //mund ta bejme dhe me WS
            },
            this.Pastro = function () {
                app.view.cmbVlerePerqindje.SetSelectedIndex(0);
                app.view.txtVlera.SetText(0);
                app.view.dteDtFillimi2.SetText();
                app.view.dteDtMbarimi2.SetText();
                app.view.gvZbritjeAnalitike.PerformCallback("Pastro");
            },
            this.UpdateRowSelected = function (grida) {
                model.numriTeSelektuar = grida.GetSelectedRowCount();
                app.view.lblSelektuar.SetText(model.numriTeSelektuar);
            };
        };

        function handlersController() {
            //private
            var controller = new ZbritjeController();
            var handlersContext = this;
            var onRuajClick = function () {
                    controller.Ruaj(false);
            };
            //publike

            this.init = function () {
                app.view.txtVlera.SetVisible(false);


                if (pageState.konfigFillestar !== null) {
                    app.view.lblKonfigurimiP.SetText(pageState.konfigFillestar.Pershkrimi);
                    app.view.cmbKonfigurimi.SetText(pageState.konfigFillestar.Kodi);
                    controller.ndryshoKonfigurimin();
                    controller.changeName();
                }

            },
           
            this.cmbVlerePerqindjeIndexChanged = function (s, e) {
                app.view.lblVlera.SetText(app.view.cmbVlerePerqindje.GetText());
            },
            this.cmbKonfigurimiChanged = function (s, e) {
                controller.ndryshoKonfigurimin();
            },
            this.NdryshoClicked = function (s, e) {
                controller.NdryshoCmimetESelektuar();
                e.processOnServer = false;
            },
            this.keyDown = function (e) {
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
                    default:
                        break;
                }
            },
            //eventet e grides
            this.beginCallbackGrida = function (s, e) {
                Utils.shfaqLoadingGif();;
            },
            this.endCallbackGrida = function (s, e) {
                if (Utils.KaFunksionPendingGrida("gvZbritjeAnalitike")) {
                    Utils.execFunksionNeRadhe("gvZbritjeAnalitike");
                    return;
                }
                ///callback normal
                Utils.hiqLoadingGif();
                if (typeof (s["cpShowPopUp"]) != "undefined") {
                    app.view.lblMsgbox.SetText(s["cpShowPopUp"]);
                    popFshi.Show();
                    delete s["cpShowPopUp"];
                    return;
                }
                var mesazhi = Utils.MerrMesazhNgaGrida(s);
                if (mesazhi.Kodi !== 1000) {
                    if (mesazhi.Status) {
                        //ruajta u be me sukses
                        myMesazh.ShtoMesazhSuksesi(mesazhi.PershkrimMesazhi);
                    } else {
                        myMesazh.ShtoMesazhGabimi(mesazhi.PershkrimMesazhi);
                    }
                }
            },
            this.BatchEditStartEditingGrida = function (s, e) {
                pageState.focusedColumn = e.focusedColumn;
                var fushaNeProcess = pageState.focusedColumn.fieldName;
                //ndalo modifikimin nese fusha ne proces nuk eshte asnjera nga keto
                if (!Utils.BenPjeseNeNjeNgaKeto(fushaNeProcess, ["DateFillimi", "LlojZbritje", "Zbritja", "Zbritja2"]))
                    e.cancel = true;

            },
            this.BatchEditEndEditingGrida = function (s, e) {

                //PERDOR setCellValue per fushat ku nuk eshte fokusi
                //rowValues per rastin kur doni te ndryshoni vleren e fushes ku ka fokus
                var zbritjaEditor = e.rowValues[(s.GetColumnByField("Zbritja").index)];
                var zbritja2Editor = e.rowValues[(s.GetColumnByField("Zbritja2").index)];
                var llojzbritjeEditor = e.rowValues[(s.GetColumnByField("LlojZbritje").index)];
                var koeficientEditor = e.rowValues[(s.GetColumnByField("Koeficent").index)];
                var dtfillimiEditor = e.rowValues[(s.GetColumnByField("DateFillimi").index)];
                var dtmbarimiEditor = e.rowValues[(s.GetColumnByField("DateMbarimi").index)];
              
                if (pageState.focusedColumn.fieldName === "LlojZbritje" && llojzbritjeEditor != undefined && zbritja2Editor != undefined) {
                    if(llojzbritjeEditor.value === "Vlere")
                        s.batchEditApi.SetCellValue(e.visibleIndex, "Zbritja2", zbritjaEditor.value * koeficientEditor.value);
                    else
                        s.batchEditApi.SetCellValue(e.visibleIndex, "Zbritja2", zbritjaEditor.value);
                }
                if (pageState.focusedColumn.fieldName === "Zbritja") {

                    if (zbritjaEditor != undefined && zbritjaEditor.value < 0) {

                        myMesazh.ShtoMesazhGabimi("Zbritja duhet te jete numer pozitiv!");
                        // s.batchEditApi.SetCellValue(e.visibleIndex, "Cmimi", 0);
                        zbritjaEditor.value = 0;
                    } else if (zbritjaEditor != undefined && zbritjaEditor.value > 0) {
                        if (llojzbritjeEditor != undefined && llojzbritjeEditor.value === "Vlere")
                            s.batchEditApi.SetCellValue(e.visibleIndex, "Zbritja2", zbritjaEditor.value * koeficientEditor.value);
                        else
                            s.batchEditApi.SetCellValue(e.visibleIndex, "Zbritja2", zbritjaEditor.value);
                    }
                }
                else if (pageState.focusedColumn.fieldName === "Zbritja2") {

                    if (zbritja2Editor != undefined && zbritja2Editor.value < 0) {
                        myMesazh.ShtoMesazhGabimi("Zbritja e dyte duhet te jete numer pozitiv!");
                        zbritja2Editor.value = 0;
                    } else if (zbritja2Editor != undefined && zbritja2Editor.value > 0) {
                        if (llojzbritjeEditor != undefined && llojzbritjeEditor.value === "Vlere")
                            s.batchEditApi.SetCellValue(e.visibleIndex, "Zbritja", zbritja2Editor.value / koeficientEditor.value);
                        else
                            s.batchEditApi.SetCellValue(e.visibleIndex, "Zbritja", zbritja2Editor.value);
                    }
                }
              
                if (dtmbarimiEditor != undefined && dtfillimiEditor != undefined && dtmbarimiEditor.value < dtfillimiEditor.value) {
                    myMesazh.ShtoMesazhInformues("Data e fillimit nuk mund te jete me e madhe se data e mbarimit!");
                    if (pageState.focusedColumn.fieldName === "DateFillimi")
                        dtfillimiEditor.value = dtmbarimiEditor.value;
                    else
                        s.batchEditApi.SetCellValue(e.visibleIndex, "DateFillimi", dtmbarimiEditor.value);
                }
               
            },
            this.BatchEditRowValidatingGrida = function (s, e) {
                console.log(e);
            },
            this.BatchEditConfirmUpdating = function (s, e) {
                //ndalo shfaqjen e alertit per konfirmim te ruajtjes se ndryshimeve
                e.cancel = true;
                //ruaj gjendjen
                s.UpdateEdit();
            },
            this.SelectionChangedGrida = function (s, e) {
                controller.UpdateRowSelected(s);
            },
            //Fund evetet e grides
            this.popupUniversalCloseUp = function (s, e) {
                app.view.popupUniversal.SetContentUrl("");
            };
            this.menu_click = function (s, e) {
                switch (e.item.name) {
                    case "Ruaj":
                        if (app.view.gvZbritjeAnalitike.batchEditApi.HasChanges()) {
                            Utils.shtoFunksionNeRadhe(onRuajClick, pageState.idGjuha, handlersContext, "gvZbritjeAnalitike");
                            app.view.gvZbritjeAnalitike.UpdateEdit();
                            //nese ben return jep problem
                        }
                        else
                            onRuajClick();
                        break;

                    case "Pastro":
                        //reset vlerat
                        controller.Pastro();
                        break;

                    default:
                        break;
                }

                e.processOnServer = false;
            },
            this.okClick = function (s, e) {
                popFshi.Hide();
                controller.Ruaj(true);
            }

        };

        return {
            Zbritje: ZbritjeController,
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
var zbritjet = new ZbritjeAnalitikeApp();
$(document).on("ready", zbritjet.initApp).on("keydown", zbritjet.handlers.keyDown);

function cbCmimBazeZbAnalitikeChecked(s, e) {
    gvZbritjeAnalitike.PerformCallback("ShtoCmBazeNeGride");
}







