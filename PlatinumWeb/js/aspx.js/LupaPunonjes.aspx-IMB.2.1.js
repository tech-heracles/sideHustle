


function checkText(s, e) {
    myMenu.checkText(s, e);
}
//duhen ri pare keto

var btnFiltrat;

function aplikoFiltra(s, e) {
    btnFiltrat = s;
    myMenu.aplikoFiltra(s, e, gvLupaPunonjes, "671", "");
}

function textChanged(s, e) {
    myMenu.textChanged(s, e);
}

function BeginCallback(s, e) {
    if (e.command == "APPLYFILTER" && btnFiltrat != undefined)
        btnFiltrat.SetText("");
}

function gup(name) {
    name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regexS = "[\\?&]" + name + "=([^&#]*)";
    var regex = new RegExp(regexS);
    var results = regex.exec(window.location.href);
    if (results === null)
        return "";
    else
        return results[1];
}

function LupaPunonjesApp() {
    this.runMode = true;
    this.view = undefined;
    //deklarohet ketu per te mbajtur nje reference mbi this,per tu aksesuar brenda moduleve te tjera
    //qellimi eshte aksesimi i objekteve qe behen share
    var app = this;
    //variabla global ne faqe te cilet ruajne gjendjen e faqes
    this.pageState = {
        init: function () {
            this.idNdermarrje = app.view.hfState.Get("idNdermarrje");
            this.idPerdoruesi = app.view.hfState.Get("idPerdoruesi");
            switch (this.ngaVjen) {
                case "ListPagesa":
                    this.dteDtDok = window.parent.dteDtDok.GetDate();
                    this.viti = this.dteDtDok.getFullYear();
                    this.nrPersonal = Utils.getUrlVar("nrPersonal");
                    this.muaji = Utils.getUrlVar("muaj");
                    this.mesazhnjepernje = Utils.getUrlVar("mesazhnjepernje") === "true";
                    this.llogaritDiteLejeNgaImporti = Utils.getUrlVar("llogaritDiteLejeNgaImporti") === "true";
                    this.ditemuajindryshueshme = Utils.getUrlVar("ditemuajindryshueshme") === "true";
                    //mer vlerat nga hfState
                    this.msgLupaPunonjesPunonjesi = app.view.hfState.Get("msgLupaPunonjesPunonjesi");
                    this.msgLupaPunonjesNukKaKomponentePerKeteDate = app.view.hfState.Get("msgLupaPunonjesNukKaKomponentePerKeteDate");
                    this.msgEkzistonPunonjesi = app.view.hfState.Get("msgLupaPunonjesekzistonPunonjesi");
                    this.msgPunonjesiNeGrid = app.view.hfState.Get("msgLupaPunonjesNeGride");
                    this.idKokaLp = Utils.getUrlVar("idKokaLp");
                    break;
                case "KomponentePage":
                    this.llojKomponentePage = Utils.getUrlVar("lloji");
                    this.dtAktivizimi = Utils.getUrlVar("dtAktivizimiKomp");
                    break;                
                
            }

        },
        nrPersonal: undefined,
        ngaVjen: Utils.getUrlVar("vjenNga"),
        ditemuajindryshueshme: undefined,
        mesazhnjepernje: undefined,
        llogaritDiteLejeNgaImporti: undefined,
        dteDtDok: undefined,
        viti: undefined,
        muaji: undefined,
        msgLupaPunonjesPunonjesi: undefined,
        msgLupaPunonjesNukKaKomponentePerKeteDate: undefined,
        msgPunonjesiNeGrid: undefined,
        msgEkzistonPunonjesi: undefined,
        idPerdoruesi: 0,
        idNdermarrje: 0,
        dtAktivizimi: undefined,
        llojKomponentePage: undefined

    };


    function View() {
        var pageState = app.pageState;
        var runMode = app.runMode;
        return {
            grida: runMode ? window.gvLupaPunonjes : ASPxClientGridView.Cast(window.gvLupaPunonjes),
            popupUniversal: runMode ? window.parent.popupUniversal : ASPxClientPopupControl.Cast(window.parent.popupUniversal),
            loadingPanel: runMode ? window.LoadingPanel : ASPxClientLoadingPanel.Cast(LoadingPanel),
            jqGrida: window.parent.jQuery("#rowed5"),
            editorGlobal: window.parent.editorGlobal,
            txtNrDitesh: runMode ? window.txtNrDitesh : ASPxClientTextBox.Cast(window.txtNrDitesh),
            cbApliko: runMode ? window.cbApliko : ASPxClientCheckBox.Cast(window.cbApliko),
            timeoutControll: window.parent.window.parent.SessionTimeout,
            gridaHeader: $("#div"),
            menuELupes: $("#div1"),
            //vetem kur lupa hapet nga struktura administrative
            personi: (pageState.ngaVjen === "Struktura") ? window.parent.Personi : undefined,
            hfState: runMode ? window.hfState : ASPxClientHiddenField.Cast(window.hfState)
        };
    }
    var models = (function () {
        //permban funksione me logjiken mbi te dhenat
        //perpunohet rezultati qe do vendoset ne view per perdoruesi
        //arsyeja qe eshte lene ne kete forme eshte qe te ruhet gjendja e modelit,mund te kete objekte me vlera
        var punonjesitModel = {
            llogaritPagen: function (colFillestare, colKomp, arrKodi, arrvleraParam, vlerat, coltat, kursinderm, od) {
                return ListPagesaUtils.llogaritPagen(colFillestare, colKomp, arrKodi, arrvleraParam, vlerat, coltat, kursinderm, undefined, od);
            },
            merrIdPunonjesish: function (rreshtaTeSelektuar) {
                if (rreshtaTeSelektuar == undefined)
                    return new Array();
                var ids = new Array(rreshtaTeSelektuar.length);
                for (var i = 0; i < rreshtaTeSelektuar.length; i++) {
                    ids[i] = rreshtaTeSelektuar[i][0];
                }
                return ids;
            }
        };

        return {
            Punonjesit: punonjesitModel
        };
    })();

    var controllers = (function () {
        if (!app.runMode)
            app.view = new View();

        ///mbajme referencat lokale per ti kapur me me pak kosto dhe lehtesi
        var model = models.Punonjesit;
        var pageState = app.pageState;

        //permban funksione qe nderveprojne me elementet e view dhe me metodat e modelit
        function punonjesController() {
            var controllerContext = this;
            this.hidePopupUniversal = function () {
                app.view.popupUniversal.Hide();
            },
                this.shfaqMesazhPopup = function (mesazhi, type) {
                    //ky funksion duhet te behet i pergjithshem
                    //ku mund te perdoret nje popup custom jo alert
                    //window.alert(mesazhi);
                    myMesazh.ShtoMesazh({ text: mesazhi, type: "error", timeout: 0, modal: true });
                },
                this.shfaqMesazhPopupv2 = function (mesazhi) {
                    if (!mesazhi || Object.keys(mesazhi) == 0) throw new Error(mesazhi);
                    switch (mesazhi.Tipi) {
                        case 0:
                            myMesazh.ShtoMesazhGabimi(mesazhi.PershkrimMesazhi);
                            break;
                        case 1:
                            myMesazh.ShtoMesazhSuksesi(mesazhi.PershkrimMesazhi);
                            break;
                        case 2:
                            myMesazh.ShtoMesazhInformues(mesazhi.PershkrimMesazhi);
                            break;
                    }
                },
                this.vendosTextNeEditorGlobal = function (vlera) {
                    app.view.editorGlobal.SetText(vlera);
                    app.view.editorGlobal.SetFocus(true);
                },
                this.vendosPunonjesNeEditorGlobal = function (kodi, id) {
                    if (!app.view.editorGlobal.FindItemByValue(id))
                        app.view.editorGlobal.AddItem(kodi, id);
                    app.view.editorGlobal.SetValue(id);
                    app.view.editorGlobal.SetText(kodi);
                    app.view.editorGlobal.SetFocus(true);
                },
                this.vendosPersoninTekStruktura = function (vlera) {
                    app.view.personi.SetText(vlera);
                    window.parent.$("#hfPersoni").val(vlera);
                    app.view.personi.SetFocus();
                },
                this.hapLupenEKomponenteveTePages = function (punonjesit) {
                    //hapim lupen per te parin
                    window.parent.LupKomponente(punonjesit.shift());
                    //ruajme ne ls per ti kapur nga lupa e komponenteve 
                    localStorage.setItem("punonjesitEZgjedhur", JSON.stringify(punonjesit));
                },
                this.ShtoPunonjesNeGrid = function (teDhenaPerGriden) {
                    window.parent.shtoPunonjesNeGrid(teDhenaPerGriden);
                },
                this.doneCallbackLlogaritPagen = function (results) {

                    var mesazhet = results.mesazhetPerPunonjes; //merr mesazhet per secilin punonjes
                    var data = results.data; //lista e komponenteve te llogaritura per te gjithe punonjesit
                    var colFillestare = results.colKompListPagesa; //te gjitha komponentet e per kete punonjes ne kete date
                    var vleratELlogaritura = {};
                    var vleraPerPunonjes = {};
                    var vleratPerTuShtuarNeGride = new Array();
                    var nrRendor = 0
                    for (var key in Object.keys(data)) {
                        var colFillestarePunonjesi = $.extend(true, [], colFillestare);
                        //vlerat e llogaritura per secilin punonjes
                        vleraPerPunonjes = new ListPagesaUtils.LlogaritPagenModel(data[key]);
                        if (vleraPerPunonjes.NrKomponenteve === 0) {
                            controllerContext.shfaqMesazhPopup(pageState.msgLupaPunonjesPunonjesi + vleraPerPunonjes.Emri + pageState.msgLupaPunonjesNukKaKomponentePerKeteDate);
                        } else {
                            //llogarit vlerat per punonjesin
                            vleratELlogaritura = model.llogaritPagen(colFillestarePunonjesi, vleraPerPunonjes.ColKomponente, vleraPerPunonjes.Kodet, vleraPerPunonjes.VleraParam, vleraPerPunonjes.Vlerat, vleraPerPunonjes.Tatimet, vleraPerPunonjes.KursiNdermarrjes, vleraPerPunonjes.OrePuneNeDite);

                            if (vleratELlogaritura.dite > 0) {
                                controllerContext.shfaqMesazhPopup("Kujdes! Ditet e punes, ditet e raportit dhe ditet e lejes i kalojne ditet e punes ne muaj per punonjesin " + vleraPerPunonjes.Emri + "!");
                            } else if (vleratELlogaritura.dite < -vleratELlogaritura.ditemuaji) {
                                controllerContext.shfaqMesazhPopup("Kujdes! Ditet e punes, ditet e raportit dhe ditet e lejes nuk mund te jene negative!");
                            } else {
                                var rreshtiPerGride = $.extend(true, {
                                    txtNrRendor: 0,
                                    name: 0,
                                    id: 0,
                                    txtNrPersonal: vleraPerPunonjes.NrPersonal,
                                    txtPunonjes: vleraPerPunonjes.Emri,
                                    txtDep: vleraPerPunonjes.Departamenti
                                }, vleratELlogaritura.rreshtTrupi);

                                vleratPerTuShtuarNeGride.push({
                                    NrPersonal: vleraPerPunonjes.NrPersonal,
                                    RreshtiPerGride: rreshtiPerGride,
                                    ColKomponente: vleratELlogaritura.colKomponentePerPunonjes
                                });

                            }

                        }
                    }
                    controllerContext.ShtoPunonjesNeGrid(vleratPerTuShtuarNeGride);
                    if (mesazhet == undefined || Object.keys(mesazhet).length === 0)
                        controllerContext.hidePopupUniversal();
                    else
                        ListPagesaUtils.shfaqMesazhGabimiNgaLLogaritjaEPages(mesazhet);
                },
                this.callWsAplikoNdryshimeKomponenteshPerPunonjesit = function (param, callback) {
                    var paramsDefault = {
                        dtAktivizimi: pageState.dtAktivizimi,
                        idPunonjesish: [],
                        lloji: pageState.llojKomponentePage,
                        idPerdoruesi: pageState.idPerdoruesi,
                        idNdermarrje: pageState.idNdermarrje
                    };
                    var params = $.extend(paramsDefault, param);
                    $.ajax({
                        showLoading: true,
                        url: Utils.getServerApiUrl("ListPagesa", "AplikoNdryshiminPerPunonjesit"),
                        data: JSON.stringify(params)
                    }).done(callback);
                },
                this.callWebServiceLlogaritPagen = function (paramsOverwrite, doneCallback) {
                    var paramsDefault = {
                        data: pageState.dteDtDok,
                        nrDitesh: pageState.ditemuajindryshueshme ? 0 : app.view.txtNrDitesh.GetText(),
                        punonjesitIDs: "",
                        muaji: pageState.muaji,
                        ditemuajindryshueshme: pageState.ditemuajindryshueshme,
                        muajitjeter: pageState.muaji,
                        vititjeter: pageState.viti,
                        idNdermarrje: pageState.idNdermarrje,
                        idGjuha: hfState.Get("idGjuha"),
                        llogaritDiteLejeNgaImporti: pageState.llogaritDiteLejeNgaImporti
                    };
                    var params = $.extend(paramsDefault, paramsOverwrite);
                    $.ajax({
                        showLoading: true,
                        url: Utils.getServerApiUrl("ListPagesa", "LlogaritPagePerShumePunonjes"),
                        data: JSON.stringify(params)
                    }).done(doneCallback);

                };
        };

        function handlersController() {
            var controller = new punonjesController();
            var controllerContext = this;
            var merrPunonjesitQeNukEkzistojne = function (punonjesit) {
                /// <summary>
                /// heq nga lista punonjesit qe ekzistojne ne grid
                /// </summary>
                /// <param name="punonjesit" type="type"></param>
                var newPunonjes = new Array();
                for (var prop in punonjesit) {
                    if (punonjesit[prop].ekziston)
                        continue;
                    newPunonjes.push(punonjesit[prop]);
                }
                return newPunonjes;
            };
            var merrPunonjesitMeEkzistence = function (values, rreshtaTeGrides) {
                /// <summary>
                /// kontrollon ne gride nese ekzistojne punonjesit
                /// </summary>
                /// <param name="values" type="type"></param>
                /// <param name="rreshtaTeGrides" type="type"></param>
                /// <returns type=""></returns>
                var punonjesitEZgjedhur = new Array(values.length);
                for (var i = 0; i < (values.length); i++) {
                    var punonjesi = { id: values[i][0], nrPersonal: values[i][1], ekziston: false };
                    for (var j = 0; j < rreshtaTeGrides.length; j++) {
                        if (rreshtaTeGrides[j].txtNrPersonal === punonjesi.nrPersonal) {
                            punonjesi.ekziston = true;
                            break;
                        }
                    }
                    punonjesitEZgjedhur[i] = punonjesi;
                }
                return punonjesitEZgjedhur;
            };
            this.resizePage = function () {
                try {
                    //panel.SetWidth(document.documentElement.clientWidth - 20);
                    app.view.grida.SetWidth(document.documentElement.clientWidth - 50);
                } catch (e) {
                    console.log(e);
                }
            },
                this.processKeyPress = function (event) {
                    var currentIndex = app.view.grida.GetFocusedRowIndex();
                    if (event.keyCode === 40) {
                        if (currentIndex === app.view.grida.GetVisibleRowsOnPage() - 1) {
                            app.view.grida.SetFocusedRowIndex(0);
                        } else {
                            app.view.grida.SetFocusedRowIndex(currentIndex + 1);
                        }
                    }
                    if (event.keyCode === 38) {
                        if (currentIndex === 0) {
                            return;
                        } else {
                            app.view.grida.SetFocusedRowIndex(currentIndex - 1);
                        }
                    }
                    if (event.keyCode === 13) {
                        this.OnGridSelectionChanged();
                    }

                },
                this.cbAplikoCheckedChanged = function (s, e) {

                    app.view.txtNrDitesh.SetEnabled(s.GetChecked());
                    if (!s.GetChecked())
                        app.view.txtNrDitesh.SetText("");
                },
                this.menuClick = function (s, e) {
                    if (e.item.name === "OK") {
                        e.processOnServer = false;
                        controllerContext.onGridSelectionChanged();

                    } else if (e.item.name === "Anullo") {
                        controller.hidePopupUniversal();
                        e.processOnServer = false;
                    }
                },
                this.onGridSelectionChanged = function () {
                    app.view.grida.GetSelectedFieldValues("IdPunonjes;NrPersonal;Emer;Atesia;Mbiemer", function (values) {
                        if (values.length === 0) {
                            controller.hidePopupUniversal();
                            return;
                        }
                        var i;
                        var ids;
                        switch (pageState.ngaVjen) {
                            case "raporti":
                                ids = new Array();
                                for (i = 0; i < (values.length); i++)
                                    ids[i] = values[i][1];
                                controller.vendosTextNeEditorGlobal(ids);
                                controller.hidePopupUniversal();
                                return;
                                break;
                            case "Import":
                                controller.vendosTextNeEditorGlobal(values[0][1]);
                                controller.hidePopupUniversal();
                                return;
                                break;
                            case "Struktura":
                                controller.vendosPersoninTekStruktura(values[0][2] + " " + values[0][4]);
                                controller.hidePopupUniversal();
                                return;
                                break;
                            case "VeprimeBanka":
                                window.parent.vendosSubjektNgaLupa(values, "Punonjes");
                                window.parent.popupUniversal.Hide();
                                return;
                                break;
                            case "ListPagesa":  //eshte hapur nga listpagesa
                                var rreshtaTeGrides = app.view.jqGrida.getRowData();

                                var punonjesit = merrPunonjesitMeEkzistence(values, rreshtaTeGrides);
                                var nrekzistues = 0;
                                var punonjesi;

                                if (app.view.cbApliko.GetChecked() || (values.length > 1 && pageState.ditemuajindryshueshme)) {
                                    //eshte rasti kur aplikohet i njejti numer ditesh per punonjesit
                                    //llogaritja do te behet pernjeheresh dhe mesazhi do te dale per te gjithe njeheresh
                                    ids = new Array();
                                    for (var key in punonjesit) {
                                        if (!punonjesit.hasOwnProperty(key))
                                            continue;

                                        punonjesi = punonjesit[key];
                                        if (punonjesi.ekziston) {
                                            if (pageState.mesazhnjepernje) {
                                                controller.shfaqMesazhPopup(pageState.msgEkzistonPunonjesi + punonjesi.nrPersonal + pageState.msgPunonjesiNeGrid);
                                            }
                                            nrekzistues++;
                                            continue;
                                        }
                                        ids.push(punonjesi.id);
                                    }

                                    if (nrekzistues !== 0 && !pageState.mesazhnjepernje)
                                        controller.shfaqMesazhPopup(nrekzistues + " punonjes ekzistojne ne gride!");
                                    if (ids.length > 0) {
                                        controller.callWebServiceLlogaritPagen({ punonjesitIDs: ids }, controller.doneCallbackLlogaritPagen);
                                    }
                                    return;
                                }
                                var mesazhEkzistence = false;
                                //hap lupen e komponenteve per secilin punonjes
                                for (var key in punonjesit) {
                                    if (!punonjesit.hasOwnProperty(key))
                                        continue;
                                    punonjesi = punonjesit[key];
                                    if (punonjesi.ekziston) {
                                        mesazhEkzistence = true;
                                        controller.shfaqMesazhPopup(pageState.msgEkzistonPunonjesi + punonjesi.nrPersonal + pageState.msgPunonjesiNeGrid);
                                    }

                                }
                                punonjesit = merrPunonjesitQeNukEkzistojne(punonjesit);
                                if (punonjesit.length === 0) {
                                    localStorage.removeItem("punonjesitEZgjedhur");
                                    if (!mesazhEkzistence) controller.hidePopupUniversal();
                                    return;
                                }
                                controller.hapLupenEKomponenteveTePages(punonjesit);
                                break;
                            case "KomponentePage":
                                var punonjesit = model.merrIdPunonjesish(values);
                                controller.callWsAplikoNdryshimeKomponenteshPerPunonjesit({ idPunonjesish: punonjesit }, function (result) {
                                    controller.shfaqMesazhPopupv2(result);
                                });
                                break;
                            case "Konfigurimi":
                                controller.vendosPunonjesNeEditorGlobal(values[0][1], values[0][0]);
                                controller.hidePopupUniversal();
                                break;
                            default:
                                console.log("pageState.ngaVjen {" + pageState.ngaVjen + "} eshte e papercaktuar");

                        }
                    });
                },
                this.init = function () {
                    try {

                        app.view.timeoutControll.sendKeepAlive();
                        myFaqeCelje.shtoHandlerSession();
                        app.view.grida.SetFocusedRowIndex(0);
                        app.view.gridaHeader.show();
                        if (!Utils.BenPjeseNeNjeNgaKeto(pageState.ngaVjen, ["ListPagesa"]))
                        {
                           cbApliko.SetVisible(false);
                            txtNrDitesh.SetVisible(false);
                            lblApliko.SetVisible(false);
                            lblNrDitesh.SetVisible(false);
                        }
                        
                        app.view.menuELupes.show();
                    } catch (err) {
                        console.log(err);
                    }
                };
        };


        return {
            Punonjes: punonjesController,
            Handlers: handlersController
        };
    })();
    //krijojme nje instance te handlerave sepse duhet te behet publike qe te lidhet me kontrollet
    var handlers = new controllers.Handlers();

    //keto jane objektet me minimale qe duhet te behen share,
    //ne rastet qe ka objekte te tjera (funksione share) te behen return po ashtu si handlerat
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

var LupaPunonjes = new LupaPunonjesApp();

$(document).on("ready", LupaPunonjes.initApp)
    .on("resize", LupaPunonjes.handlers.resizePage)
    .on("keypress", LupaPunonjes.handlers.processKeyPress);