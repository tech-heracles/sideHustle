function LupaKomponenteMuaji() {
    this.runMode = true;
    this.view = undefined;

    var app = this;

    function View() {
        var runMode = app.runMode;
        return {
            jqGrida: window.parent.$("#rowed5"),
            grida: runMode ? window.parent.gvLupaK : new ASPxClientGridView(),
            gvLupaKomp: runMode ? window.gvLupaKomp : new ASPxClientGridView(),
            loadingPanel: runMode ? window.LoadingPanel : new ASPxClientLoadingPanel(),
            popupUniversal: runMode ? window.popupUniversal : new ASPxClientPopupControl(),
            popupUniversalPrindi: runMode ? window.parent.popupUniversal : new ASPxClientPopupControl(),
            popFshi: runMode ? window.popFshi : new ASPxClientPopupControl(),
            txtVleraParametri: runMode ? window.txtVleraParametri : new ASPxClientTextBox(),
            txtKodi: runMode ? window.txtKodi : new ASPxClientTextBox(),
            txtVlera: runMode ? window.txtVlera : new ASPxClientTextBox(),
            cmbMuaji: runMode ? window.cmbMuaji : new ASPxClientComboBox(),
            cmbViti: runMode ? window.cmbViti : new ASPxClientComboBox(),
            ASPxMenu1: runMode ? window.ASPxMenu1 : new ASPxClientMenu()
        };
    }
    //variabla global ne faqe te cilet tregojne gjendjes e faqes
    this.pageState = {
        muajiNgaLista: undefined,
        llogaritDP: undefined,
        dtDok: undefined,
        diteMuajiNdryshueshme: undefined,
        llogaritDiteLejeNgaImporti: undefined,
        lidhur: false,
        idPunonjesi: Utils.getUrlVar("idpun"),
        idRreshti: Utils.getUrlVar("idreshti"),
        idKokaLp: 0,
        init: function () {
            /// <summary>
            /// inicializojme fushat qe marrin vlera nga prindi ose nga hidden field e kesaj faqe
            /// </summary>
            var pageStatePrind = window.parent.lupaKomponente.pageState;
            this.muajiNgaLista = pageStatePrind.muaji;
            this.llogaritDP = pageStatePrind.llogaritDP;
            this.dtDok = pageStatePrind.dteDtDok;
            this.diteMuajiNdryshueshme = pageStatePrind.ditemuajindryshueshme;
            this.llogaritDiteLejeNgaImporti = pageStatePrind.llogaritDiteLejeNgaImporti;
            this.lidhur = pageStatePrind.lidhur;
            this.idKokaLp = Utils.getNumberOrDefaultFromUrl("idKokaLp");
        }

    };



    var models = (function () {
        //mbajme referencen e prindit per arsye performance
        var modeliPrind = window.parent.lupaKomponente.models.Komponentet;

        var komponenteMuajiModel = {
            colFillestare: modeliPrind.colFillestare,
            arrMosNdrysho: modeliPrind.arrMosNdrysho,
            arrvleraParam: modeliPrind.arrvleraParam,
            arrvlera: modeliPrind.arrvlera,
            arrKodi: modeliPrind.arrKodi,

            llogaritVlerenPerKomponente: function (colKomp, kodi) {

                if (colKomp.length === 0) {
                    console.log("Nuk ka komponente per kete muaj!")
                    return 0;
                }
                var formula = 0;
                for (var i = 0; i < colKomp.length; i++) {
                    if (colKomp[i].Kodi === kodi) {
                        if (colKomp[i].Formula !== "") {
                            try {
                                formula = parseFloat(eval(colKomp[i].Formula));
                                if (isNaN(formula) || formula < 0)
                                    formula = 0;
                            } catch (ee) {
                                formula = 0;
                                console.log(ee);
                            }
                            break;
                        }
                    }
                }
                return formula;
            }
        };

        return {
            komponenteMuaji: komponenteMuajiModel
        };
    })();

    var controllers = (function () {
        //per intellisense
        if (!app.runMode)
            app.view = new View();

        var model = models.komponenteMuaji;

        var pageState = app.pageState;

        function komponenteMuajiController() {
            var shefi = this;
            var funcPrindi = window.parent.lupaKomponente.publicFunc;
            //funksione publike

            this.EndRequestHandler = function (sender, args) {
                var hf = $("#hfStatusi", document);
                var hfShtimModifikim = $("#hfShtimModifikim");
                if (hf.val() == "true") {
                    funcPrindi.vendosKaNdryshimeTePaRuajtura();
                    window.mbush = false;
                    hfShtimModifikim.val("shtim"); //hidden fieldi qe ruan nese veprimi eshte shtim apo modifikim
                    shefi.pastrofusha();
                    shefi.UpdateTotalet(false);
                }

                Utils.hiqLoadingGif();;
            },
                this.pastrofusha = function () {
                    app.view.txtVlera.SetText("0.00");
                    app.view.txtVleraParametri.SetText("0.00");
                    app.view.cmbMuaji.SetValue(pageState.dtDok.getMonth() + 1);
                    app.view.cmbViti.SetValue(pageState.dtDok.getFullYear());
                },
                this.shfaqMesazhPopup = function (mesazhi) {
                    window.alert(mesazhi);
                },
                this.VleraParametritChanged = function (kodi, vleraparam, ditemuaji, pagemuaji, muaji, viti) {
                    var grida = app.view.grida; //grida e komponenteve
                    var llogaritDp;
                    if (pageState.muajiNgaLista != muaji && pagemuaji == "True") {
                        for (var j = 0, count = model.colFillestare.length; j < count; j++) {
                            if (model.colFillestare[j].KodKomponente === kodi)
                                model.arrvleraParam[j] = vleraparam;
                            else
                                model.arrvleraParam[j] = 0;
                            model.arrvlera[j] = 0;
                        }
                    } else {
                        for (var j = 0, count = model.colFillestare.length; j < count; j++) {
                            for (var i = 0, countGrida = grida.GetVisibleRowsOnPage() ; i < countGrida; i++) {
                                if (model.colFillestare[j].KodKomponente === grida.batchEditApi.GetCellValue(i, "KodKomponente")) {
                                    if (model.colFillestare[j].KodKomponente === kodi)
                                        model.arrvleraParam[j] = vleraparam;
                                    else
                                        model.arrvleraParam[j] = grida.batchEditApi.GetCellValue(i, "VleraParam").toString();
                                    model.arrvlera[j] = grida.batchEditApi.GetCellValue(i, "Vlera").toString();

                                    if (model.arrvleraParam[j].search(":") != -1) {
                                        var arrtime = model.arrvleraParam[j].split(":");
                                        model.arrvleraParam[j] = parseFloat(arrtime[0]) + arrtime[1] / 60;
                                    }
                                    if (model.arrvleraParam[i] === "")
                                        model.arrvleraParam[i] = 0;
                                    if (model.arrvlera[i] === "")
                                        model.arrvlera[i] = 0;
                                    model.colFillestare[j].VleraParam = model.arrvleraParam[j];
                                    model.colFillestare[j].Shenime = grida.batchEditApi.GetCellValue(i, "Shenime");
                                    break;
                                }
                            }
                        }
                    }
                    if (kodi === "PR" || kodi === "PRN" || kodi === "DLK")
                        llogaritDp = true;
                    else
                        llogaritDp = false;
                    var params = {
                        llogaritDP: llogaritDp,
                        kodi: kodi,
                        ditemuaji: ditemuaji,
                        pagemuaji: pagemuaji,
                        muajitjeter: muaji,
                        vititjeter: viti
                    };

                    shefi.callWebServiceLlogaritPagen(params, function (result) {
                        app.view.txtVlera.SetText(model.llogaritVlerenPerKomponente(result, kodi));
                    });
                },
                this.textChanged = function () {
                    var vleraParametri = txtVleraParametri.GetValue();
                    if (vleraParametri != 0) {
                        var hfDiteMuaji = $("#hfDiteMuaji").val();
                        var hfPageMuaji = $("#hfPageMuaji").val();
                        shefi.VleraParametritChanged(app.view.txtKodi.GetText(), vleraParametri, hfDiteMuaji, hfPageMuaji, app.view.cmbMuaji.GetValue(), app.view.cmbViti.GetValue());
                    }
                },
                this.callWebServiceLlogaritPagen = function (parametrat, callback) {
                    var paramsDefault = {
                        arrvlera: model.arrvlera,
                        arrvleraParam: model.arrvleraParam,
                        arrKodi: model.arrKodi,
                        arrMosNdrysho: model.arrMosNdrysho,

                        data: pageState.dtDok,
                        idpunonjesi: pageState.idPunonjesi,
                        llogaritDP: false,
                        muaji: pageState.muajiNgaLista,
                        ditemuajindryshueshme: pageState.diteMuajiNdryshueshme,
                        kodi: "",
                        vjenNgaMuajt: true,
                        ditemuaji: "",
                        pagemuaji: "",
                        muajitjeter: "",
                        vititjeter: "",
                        merrimporte: false,
                        kodimporti: "",
                        vjenNgaKomponentja: true,
                        newrecord: false,
                        llogaritDiteLejeNgaImporti: pageState.llogaritDiteLejeNgaImporti,
                        idNdermarrjeVit: hfState.Get("idNdermarrjeVit"),
                        idGjuha: hfState.Get("idGjuha"),
                        idNdermarrje: hfState.Get("idNdermarrje"),
                        idKokaLp: pageState.idKokaLp,
                        merrVlereDefault: true
                    };

                    var params = $.extend({}, paramsDefault, parametrat);
                    $.ajax({
                        pritPergjigje: true,
                        showLoading: true,
                        url: Utils.getServerApiUrl("ListPagesa", "LlogaritKomponenteMuaji"),
                        data: JSON.stringify(params)
                    }).done(function (result) {
                        shefi.doneCallbackLlogaritPagen(result, callback);
                    });
                },
                this.doneCallbackLlogaritPagen = function (result, callback) {

                    console.log(result.Mesazhet);
                    for (var mesazhKey in result.Mesazhet) {
                        myMesazh.ShtoMesazhGabimi(result.Mesazhet[mesazhKey]);
                    }
                    callback(result.Komponentet);
                },
                this.callWebServiceVendosVlereDefault = function () {

                    var paramsDefult = {
                        muaj: app.view.cmbMuaji.GetValue(),
                        vit: app.view.cmbViti.GetValue(),
                        idpunonjes: pageState.idPunonjesi,
                        idNdermarrje: hfState.Get("idNdermarrje"),
                        kodKomponente: app.view.txtKodi.GetText(),
                        heraPare: true
                    };

                    $.ajax({
                        pritPergjigje: true,
                        showLoading: true,
                        url: Utils.getServerApiUrl("ListPagesa", "MerrVlereDefault"),
                        data: JSON.stringify(paramsDefult)
                    }).done(function (result) {
                        shefi.SucceededVendosVlereDefault(result, function (result) {
                            app.view.txtVlera.SetText(result);
                        });
                    });
                },
                this.UpdateTotalet = function (mbyll) {
                    var grida = app.view.grida;
                    var totaletString = gvLupaKomp["cpTotaliKomponentes"];
                    if (totaletString == undefined || totaletString == '') {
                        return;
                    }
                    totalet = JSON.parse(totaletString);
                    var colFillestare = model.colFillestare;
                    var vleratPara = {
                        vlera: grida.batchEditApi.GetCellValue(pageState.idRreshti, "Vlera"),
                        vleraParam: grida.batchEditApi.GetCellValue(pageState.idRreshti, "VleraParam")
                    };
                    var vleratPas = {
                        vlera: totalet.Vlera,
                        vleraParam: totalet.VleraParam
                    }; //vendosi ne grid
                    grida.batchEditApi.SetCellValue(pageState.idRreshti, "VleraParam", totalet.VleraParam);
                    grida.batchEditApi.SetCellValue(pageState.idRreshti, "Vlera", totalet.Vlera);

                    for (var k = 0, count = colFillestare.length; k < count; k++) {
                        if (colFillestare[k].KodKomponente === app.view.txtKodi.GetText()) {
                            colFillestare[k].Vlera = totalet.Vlera;
                            colFillestare[k].VleraParam = totalet.VleraParam;
                            break;
                        }
                    }
                    grida.BatchEditEndEditing.FireEvent(grida, {
                        visibleIndex: pageState.idRreshti,
                        ngaMuajt: true,
                        vleratPara: vleratPara,
                        vleratPas: vleratPas,
                        callback: function () {
                            //to check
                            if (app.view.txtVleraParametri.GetValue() != 0 || app.view.txtVlera.GetText() != 0)
                                app.view.popFshi.Show();
                            else if (mbyll)
                                app.view.popupUniversalPrindi.Hide();

                        }
                    });
                },
            this.SucceededVendosVlereDefault = function (result, callback) {
                if (result == undefined) {
                    console.log("rezultati nuk eshte i rregullt!");
                    return;
                }
                callback(result);
            };
        };


        function handlersController() {
            var controller = new komponenteMuajiController();

            this.init = function () {
                try {
                    app.view.popupUniversalPrindi.UpdatePosition();
                    myFaqeCelje.shtoHandlerSession();
                    var prm = window.Sys.WebForms.PageRequestManager.getInstance();
                    prm.add_endRequest(controller.EndRequestHandler);
                    prm.add_endRequest(myMesazh.EndRequestTimer);

                    app.view.gvLupaKomp.SetFocusedRowIndex(0);

                    if (pageState.lidhur == true) {
                        app.view.txtVlera.SetEnabled(false);
                        app.view.txtVleraParametri.SetEnabled(false);
                        app.view.cmbMuaji.SetEnabled(false);
                        app.view.cmbViti.SetEnabled(false);
                        app.view.ASPxMenu1.GetItemByName("Ruaj").SetEnabled(false);
                        app.view.ASPxMenu1.GetItemByName("Fshi").SetEnabled(false);
                        app.view.ASPxMenu1.GetItemByName("Pastro").SetEnabled(false);
                    }
                    controller.callWebServiceVendosVlereDefault();

                } catch (err) {
                    console.log(err);
                }
            },
            this.resizePage = function () {
                try {
                    app.view.grida.SetWidth(document.documentElement.clientWidth - 50);
                } catch (e) {
                    console.log(e);
                }
            },
            this.MenuClick = function (s, e) {

                if (e.item.name == "Ruaj") {
                    Utils.shfaqLoadingGif();;
                    myFaqeCelje.validim(s, e);
                }
                if (e.item.name == "Pastro") {
                    controller.pastrofusha();
                    e.processOnServer = false;
                }
                if (e.item.name == "Fshi") {
                    Utils.shfaqLoadingGif();;
                }
                if (e.item.name == "Anullo") {
                    controller.UpdateTotalet(true);
                    //app.view.popupUniversalPrindi.Hide();
                    e.processOnServer = false;
                }
            },
            this.menu_click = function (s, e) {

                if (!Utils.KanePerfunduarWs()) {
                    Utils.shtoFunksionNeRadhe(function () {
                        app.view.ASPxMenu1.ItemClick.FireEvent(s, e);
                    }, pageState.idGjuha, handlers);
                    e.processOnServer = false;
                }
                handlers.MenuClick(s, e);
            },
            this.TextChanged = function (s, e) {
                //GIMPROVE nuk funksionon textChanged ne kur perdoret menyra e ndryshimit te GetText,SetText
                setTimeout(controller.textChanged, 20);
            },
            this.KeyPresVleraParam = function (s, e) {
                if (e.htmlEvent.keyCode == 13) {//largon focusin me qellim
                    //GIMPROVE largojme focusin qe te funksionojne eventet e kotnrollit
                    var editor = app.view.gvLupaKomp.GetAutoFilterEditor("KodKomponente");
                    editor.SetFocus();
                }
            },
            this.vitiMuajiIndexChanged = function (s, e) {
                controller.textChanged();
            },
            this.popupFshiOkClick = function () {
                app.view.popFshi.Hide();
                app.view.popupUniversalPrindi.Hide();
            },
            this.popupFshiCancelClick = function () {
                app.view.popFshi.Hide();
            };
        };

        return {
            komponenteMuaji: komponenteMuajiController,
            Handlers: handlersController
        };

    })();
    //krijojme nje instance te handlerave sepse duhet te behet publike qe te lidhet me kontrollet
    var handlers = new controllers.Handlers();

    return {
        initApp: function () {
            app.view = new View();
            app.pageState.init();
            handlers.init();
        },
        models: models,
        handlers: handlers,
        pageState: app.pageState
    };


};

var lupa = new LupaKomponenteMuaji();

$(document).on("ready", lupa.initApp)
    .on("resize", lupa.handlers.resizePage);