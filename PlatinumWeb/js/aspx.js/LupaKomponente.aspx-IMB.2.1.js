
function LupaKomponenteApp() {
    this.runMode = true; //per intellisense behet false
    this.view = undefined;
    var app = this;
    function View() {
        var runMode = app.runMode;
        return {
            jqGrida: window.parent.$("#rowed5"),
            grida: runMode ? window.gvLupaK : new ASPxClientGridView(),
            popupUniversal: runMode ? window.popupUniversal : new ASPxClientPopupControl(),
            popupUniversalPrindi: runMode ? window.parent.popupUniversal : new ASPxClientPopupControl(),
            loadingPanel: runMode ? window.LoadingPanel : new ASPxClientLoadingPanel.Cast(),
            txtPunonjesi: runMode ? window.txtPunonjesi : new ASPxClientTextBox(),
            txtPaguar: runMode ? window.txtPaguar : new ASPxClientTextBox(),
            btnImporti: runMode ? window.btnImporti : new ASPxClientButton(),
            btnKtheu: runMode ? window.btnKtheu : new ASPxClientButton(),
            btnOk: runMode ? window.btnOk : new ASPxClientButton(),
            timeoutControll: window.parent.window.parent ? window.parent.window.parent.SessionTimeout : undefined,
            hflejomod: runMode ? window.hflejomod : new ASPxClientHiddenField(),
            hfdetyrueshme: runMode ? window.hfdetyrueshme : new ASPxClientHiddenField(),
            hfState: runMode ? window.hfState : new ASPxClientHiddenField(),
            hfKompFill: $("#hfKompFill", document),
            hfVeprimi: $("#hfVeprimi", document)
        };
    }

    if (!app.runMode)
        app.view = new View();

    //variabla global ne faqe te cilet ruajne gjendjen e faqes
    this.pageState = {
        janeBereRillogaritje: false,
        veprimiMbiGrid: "",
        llogaritDP: false,
        focusedColumn: undefined,
        kolonaNeProcess: { vleraParam: undefined, vlera: undefined, shenime: undefined },

        ngaVjen: Utils.getUrlVar("vjenNga"),
        ditemuajindryshueshme: window.parent.ditemuajindryshueshme,
        mesazhnjepernje: window.parent.mesazhnjepernje,
        llogaritDiteLejeNgaImporti: window.parent.llogaritDiteLejeNgaImporti,
        dteDtDok: new Date(window.parent.dteDtDok.GetDate()),
        viti: undefined,
        idGjuha:0,
        idNdermarrje : undefined,
        idNdermarrjeVit : undefined,
        muaji: window.parent.cmbMuaji.GetValue(),
        lidhur: window.parent.lidhur,
        shfaqlupemuaji: window.parent.shfaqlupemuaji,
        idpunonjesi: '', //GTOCHECK me multiple punonjes
        nrPersonal: '',
        punonjesi: "",
        formatNumri: undefined,
        lejoModifikim: {},
        komponenteTeDetyrueshme: {},
        colFillestare: undefined,
        newrecord: Utils.getUrlVar("newrecord") === "true",
        ekzistonNeLp: false,
        kaNdryshimeTePaRuajtura: false,
        idKokaLp: 0,
        initState: function () {
            this.viti = this.dteDtDok.getFullYear();
            this.formatNumri = JSON.parse(app.view.hfState.Get("formatMonedhe"));
            this.punonjesi = JSON.parse(app.view.hfState.Get("punonjesi"));
            this.idpunonjesi = this.punonjesi.IdPunonjes;
            this.nrPersonal = this.punonjesi.NrPersonal;
            this.lastsel2 = app.view.jqGrida.getLastSel2();
            this.idKokaLp = Utils.getNumberOrDefaultFromUrl("idKokaLp");
            var rreshtiNeLp = undefined;
            this.idGjuha = parseInt(hfState.Get("idGjuha"));
            this.idNdermarrje = hfState.Get("idNdermarrje");
            this.idNdermarrjeVit = hfState.Get("idNdermarrjeVit");
            if (window.parent && window.parent["pageState"] && window.parent.pageState["colKompListPagese"]) {
                rreshtiNeLp = (!this.newrecord) ? window.parent.pageState["colKompListPagese"][this.nrPersonal] : undefined;
            }
            if (rreshtiNeLp != undefined) {
                //marrim komponentet fillestare  nga trupi i lp
                this.colFillestare = $.extend(true, [], rreshtiNeLp);//per te mos mbajtur te njejten reference
                this.ekzistonNeLp = true;
            } else {
                //eshte punonjes i ri per kete lp dhe komponentet i marrim nga hf
                this.colFillestare = JSON.parse(app.view.hfKompFill.val());
                this.ekzistonNeLp = false;

            }

            //merr gjithe vlerat nga hf i modifikimit te komponenteve dhe ruaj ne nje objekt per mos i kapur gjthm nga hiddenfieldi
            var properties = app.view.hflejomod["properties"];
            for (var key in properties) {
                if (properties.hasOwnProperty(key)) {
                    this.lejoModifikim[key.replace(ASPxClientHiddenField.TopLevelKeyPrefix, "")] = properties[key];

                }
            }
            //mer gjtihe vlerat nga hf qe mban kodet e kompoenenteve bashke me statusin e detyrueshem apo jo
            var propertiesDetyrueshme = app.view.hfdetyrueshme["properties"];
            for (var key in propertiesDetyrueshme) {
                if (propertiesDetyrueshme.hasOwnProperty(key)) {
                    this.komponenteTeDetyrueshme[key.replace(ASPxClientHiddenField.TopLevelKeyPrefix, "")] = propertiesDetyrueshme[key];

                }
            }

        }
    };

    var models = (function () {

        var komponenteModel = {
            dite: 0,
            ditemuaji: 0,
            ditepune: 0,
            dep: "",
            nrPersonal: "",
            emri: "",
            paguar: 0,
            colarrmuaji: new Array(),
            colarrparmuaji: new Array(),
            arrvlera: new Array(),
            arrvleraParam: new Array(),
            arrKodi: new Array(),
            arrMosNdrysho: new Array(),
            colFillestare: new Array(),
            rreshtiTrupit: {},
            shtoKomponenteTeRejaQeJaneAktivizuarTani: function (colFillestare, teVjetraDheTeReja) {

                for (var i = 0; i < colFillestare.length; i++) {
                    for (var j = 0; j < teVjetraDheTeReja.length; j++)
                        if (colFillestare[i].KodKomponente === teVjetraDheTeReja[j].KodKomponente) {
                            teVjetraDheTeReja.splice(j, 1);
                            break;
                        }
                }
                //shton komponentet qe jane shtuar punonjesit
                for (var sh = 0; sh < teVjetraDheTeReja.length; sh++)
                    colFillestare.push(teVjetraDheTeReja[sh]);
            },
            updateModelMeTeDhenaEKthyeraNgaWs: function (vleratEPunonjesit) {
                //mbajme ato qe na duhen me vone
                this.emri = vleratEPunonjesit.Emri;
                this.dep = vleratEPunonjesit.Departamenti;
                this.nrPersonal = vleratEPunonjesit.NrPersonal;
                this.arrvlera = vleratEPunonjesit.Vlerat;
                this.arrvleraParam = vleratEPunonjesit.VleraParam;
                this.arrKodi = vleratEPunonjesit.Kodet;

                //this.ndryshoColFillestareSipasKesajDate(vleratEPunonjesit.ColKomponente);

            },
            vendosVleraFillestareNeModel: function (arrkodi, arrvlera, arrVleraParam, arrmosNdrysho, colFillestare, paguar) {
                this.arrKodi = arrkodi;
                this.arrvlera = arrvlera;
                this.arrvleraParam = arrVleraParam;
                this.arrMosNdrysho = arrmosNdrysho;
                this.colFillestare = colFillestare;
                this.paguar = paguar;
            },
            llogaritPagen: function (vleratEPunonjesit) {

                //vleratEPunonjesit = new ListPagesaUtils.LlogaritPagenModel();
                var vleraTeLlogaritura = ListPagesaUtils.llogaritPagen(this.colFillestare, vleratEPunonjesit.ColKomponente, this.arrKodi, this.arrvleraParam, this.arrvlera, vleratEPunonjesit.Tatimet, vleratEPunonjesit.KursiNdermarrjes, undefined, vleratEPunonjesit.OrePuneNeDite);

                this.dite = vleraTeLlogaritura.dite;
                this.ditemuaji = vleraTeLlogaritura.ditemuaji;
                this.colFillestare = vleraTeLlogaritura.colKomponentePerPunonjes;
                this.paguar = vleraTeLlogaritura.rreshtTrupi.txtPaguar;
                this.ditepune = vleraTeLlogaritura.ditepune;
                this.rreshtiTrupit = vleraTeLlogaritura.rreshtTrupi;
                return vleraTeLlogaritura;

            }

        };

        return {
            Komponentet: komponenteModel
        };
    })();

    var controllers = (function () {


        var model = models.Komponentet;
        var pageState = app.pageState;

        function lupaKomponenteController() {


            // private
            var controllerContext = this;
            var idreshti = 0;
            var succededRowValues = function (result) {
                if (result[3] != undefined && result[3] != 1) {
                    var queryStr = Utils.KonvertoObjectQueryString(
                    {
                        idtrupi: result[0],
                        komp: result[2],
                        idpun: pageState.idpunonjesi,
                        idreshti: idreshti,
                        data: JSON.stringify(pageState.dteDtDok),
                        vleraparam: result[4],
                        vlera: result[5],
                        idKokaLp: pageState.idKokaLp
                    });
                    myButtonClickLupa.LupaUniversal_Click(hfState.Get("msgTeDhenaPerMuajin"), "LupaKomponenteMuaj.aspx?" + queryStr, 700, 500);

                }

            };
            var vendosVleratELlogarituraNeGrid = function (colKomp) {

                var grida = app.view.grida;
                var colFillestare = $.extend([], colKomp);
                for (var m = 0, count = grida.pageRowCount ; m < count; m++) {
                    for (var i = 0; i < colFillestare.length; i++) {
                        if (colFillestare[i].KodKomponente === grida.batchEditApi.GetCellValue(m, "KodKomponente")) {
                            grida.batchEditApi.SetCellValue(m, "Vlera", colFillestare[i].Vlera);
                            grida.batchEditApi.SetCellValue(m, "VleraParam", colFillestare[i].VleraParam);
                            colFillestare.splice(i, 1);
                            break;
                        }
                    }

                }
            };


            // publike
            this.merrKolonePerValidim = function (s, e, fusha) {
                var kolona = s.GetColumnByField(fusha).index;
                if (kolona == undefined) {
                    console.log("kolona " + fusha + "nuk ekziston!");
                    return undefined;
                }
                var kolonaVlera = e.validationInfo[kolona];
                if (kolonaVlera.value == undefined || kolonaVlera.value == null) kolonaVlera.value = "0";
                return kolonaVlera;
            },
            this.formatoFushaDevi = function () {
                Utils.formatoShumeTextBox(app.view.txtPaguar);
            },
            this.unformatoFushaDevi = function () {
                Utils.unFormatoShumeTextBox(app.view.txtPaguar);
            },
            this.ndryshoKonfigFormatNumri = function () {
                if (pageState.formatNumri == undefined)
                    pageState.formatNumri = $.parseJSON(hfState.Get("formatMonedhe"));

                Utils.setFormatNumri(app.view.txtPaguar, pageState.formatNumri.ShifraPasPresjesVlefta);
            },
            this.hapLupaMuaj = function (s, e) {
                if (pageState.shfaqlupemuaji) {
                    idreshti = e.visibleIndex;
                    //nese grida ka ndryshime client side ath bejme callback per te ruajtur gjendjen,dhe ne kete callback marrim edhe vlerat e rreshtin qe kemi fokusin,
                    //ne kete menyre kursejme nje callback te grides :)
                    if (s.batchEditApi.HasChanges()) {
                        s.UpdateEdit();
                        pageState.veprimiMbiGrid = "getRowValues";
                        return;
                    }
                    pageState.veprimiMbiGrid = "";
                    app.view.grida.GetRowValues(e.visibleIndex, "IdKompListPagese;IdKomponentePage;KodKomponente;Njesia;VleraParam;Vlera", succededRowValues);
                }
            },
            this.veprimeServerSide = function (s, e) {
                if (pageState.veprimiMbiGrid === "getRowValues")
                    succededRowValues(JSON.parse(s.cpRowValues));
            },
            this.hidePopupUniversal = function () {
                app.view.popupUniversal.Hide();
            },
            this.shfaqMesazhPopup = function (mesazhi) {
                //ky funksion duhet te behet i pergjithshem
                //ku mund te perdoret nje popup custom jo alert
                myMesazh.ShtoMesazhGabimi(mesazhi);
            },
            this.hapLupenEKomponenteveTePages = function (punonjesi) {
                window.parent.LupKomponente(punonjesi);
            },
            this.shtoPunonjesNeTrupinLp = function () {

                if (pageState.janeBereRillogaritje) {
                    var rreshtiPerGride = model.rreshtiTrupit;
                    rreshtiPerGride["txtNrPersonal"] = model.nrPersonal;
                    rreshtiPerGride["txtDep"] = model.dep;
                    rreshtiPerGride["txtPunonjes"] = model.emri;
                    window.parent.shtoPunonjesNeGrid([
                        {
                            NrPersonal: model.nrPersonal,
                            RreshtiPerGride: rreshtiPerGride,
                            ColKomponente: model.colFillestare
                        }]);
                }
                controllerContext.ndryshoFlamurinKaNdryshimeTePaRuajtura(false);
            },
            this.doneCallbackLlogaritPagen = function (results) {

                var mesazhet = results.mesazhetPerPunonjes; //merr mesazhet per secilin punonjes
                if (results.data == undefined) {
                    controllerContext.shfaqMesazhPopup(mesazhet[pageState.punonjesi.NrPersonal]);
                    return;
                }
                pageState.janeBereRillogaritje = true;
                var vleratEPunonjesit = new ListPagesaUtils.LlogaritPagenModel(results.data);
                var emriPlote = pageState.punonjesi.Emer + pageState.punonjesi.Mbiemer;
                if (vleratEPunonjesit.NrKomponenteve === 0) {
                    controllerContext.shfaqMesazhPopup(hfState.Get("msgLupaPunonjesPunonjesi") + " " + emriPlote + " " + hfState.Get("msgLupaPunonjesNukKaKomponentePerKeteDate"));
                    return;
                }
                model.updateModelMeTeDhenaEKthyeraNgaWs(vleratEPunonjesit);
                var vleratELlogaritura = model.llogaritPagen(vleratEPunonjesit);
                vendosVleratELlogarituraNeGrid(vleratELlogaritura.colKomponentePerPunonjes);
                ListPagesaUtils.RillogaritTotaletPerGrup(app.view.grida, pageState.formatNumri.ShifraPasPresjesVlefta, "Vlera");
                txtPaguar.SetText(vleratELlogaritura.rreshtTrupi.txtPaguar);
                ListPagesaUtils.shfaqMesazhGabimiNgaLLogaritjaEPages(mesazhet);
                controllerContext.ndryshoFlamurinKaNdryshimeTePaRuajtura(true);
                controllerContext.ndryshoFlamurinKaNdryshime(true);
            },
            this.callWebServiceLlogaritPagen = function (parametrat, doneCallback) {

                //parametrat default te ws per llogaritjen e pages
                var parametraDefaultPerLlogaritjePage = {
                    arrvlera: model.arrvlera,
                    arrvleraParam: model.arrvleraParam,
                    arrKodi: model.arrKodi,
                    arrMosNdrysho: model.arrMosNdrysho,
                    data: pageState.dteDtDok,
                    idpunonjesi: pageState.idpunonjesi,
                    llogaritDP: pageState.llogaritDP,
                    muaji: pageState.muaji,
                    ditemuajindryshueshme: pageState.ditemuajindryshueshme,
                    vjenNgaMuajt: false,
                    ditemuaji: "False",
                    pagemuaji: "False",
                    muajitjeter: pageState.muaji,
                    vititjeter: pageState.viti,
                    vjenNgaKomponentja: true,
                    newrecord: false,
                    llogaritDiteLejeNgaImporti: pageState.llogaritDiteLejeNgaImporti,
                    merrimporte: false,
                    kodimporti: "",
                    kodi: "PP",
                    idNdermarrjeVit: pageState.idNdermarrjeVit,
                    idGjuha: pageState.idGjuha,
                    idNdermarrje: pageState.idNdermarrje,
                    idKokaLp: pageState.idKokaLp,
                    merrVlereDefault: !pageState.ekzistonNeLp && !pageState.janeBereRillogaritje,
                    buttonClickNgaKomponente: false
                };
                //ben override parametrat default
                var params = $.extend(parametraDefaultPerLlogaritjePage, parametrat);
                Utils.shfaqLoadingGif();;
                $.ajax({
                    pritPergjigje: true,
                    url: Utils.getServerApiUrl("ListPagesa", "LlogaritPage"),
                    data: JSON.stringify(params)
                }).done(function (result) {
                    doneCallback(result);
                }).always(function () {
                    Utils.hiqLoadingGif();;
                });
            },
            this.BatchEnd = function (s, index, vleratPara, vleratPas) {

                var kodi = s.batchEditApi.GetCellValue(index, "KodKomponente");
                s = app.view.grida;
                //sepse grida nuk e ka ruajtur gjendjen akoma
                setTimeout(function () {
                    for (var i = 0, count = s.pageRowCount ; i < count; i++) {
                        var kodiKomp = s.batchEditApi.GetCellValue(i, "KodKomponente");
                        if (kodiKomp != undefined) {
                            for (var j = 0, length = model.colFillestare.length; j < length; j++) {
                                if (model.colFillestare[j].KodKomponente === kodiKomp) {
                                    if (kodiKomp == "PP" && !Utils.JaneEkuivalentObjektet(vleratPara, vleratPas)) {
                                        //eshte rasti kur vendos me dore vleren 0 per ditet e punes
                                        model.arrMosNdrysho[j] = true;
                                        model.colFillestare[j].Modifikuar = true;
                                    }
                                    else if (pageState.lejoModifikim[kodi] && kodi === kodiKomp && !Utils.JaneEkuivalentObjektet(vleratPara, vleratPas)) {
                                        model.arrMosNdrysho[j] = true;
                                        model.colFillestare[j].Modifikuar = true;
                                    }
                                    model.arrvlera[j] = (s.batchEditApi.GetCellValue(i, "Vlera") || 0).toString();
                                    model.arrvleraParam[j] = (s.batchEditApi.GetCellValue(i, "VleraParam") || 0).toString();
                                    if (model.arrvleraParam[j].search(":") != -1) {
                                        var arrtime = model.arrvleraParam[j].split(":");
                                        model.arrvleraParam[j] = parseFloat(arrtime[0]) + arrtime[1] / 60;
                                    }
                                    if (model.arrvleraParam[i] === "")
                                        model.arrvleraParam[i] = 0;
                                    if (model.arrvlera[i] === "")
                                        model.arrvlera[i] = 0;
                                    model.colFillestare[j].VleraParam = model.arrvleraParam[j];
                                    model.colFillestare[j].Shenime = s.batchEditApi.GetCellValue(i, "Shenime");

                                    break;
                                }
                            }
                        }
                    }

                    if (kodi === "PR" || kodi === "PRN" || kodi === "DLK")
                        pageState.llogaritDP = true;
                    else pageState.llogaritDP = false;
                    //nese ka ndryshime thirr ws per update-im te vlerave
                    if (!Utils.JaneEkuivalentObjektet(vleratPara, vleratPas)) {
                        var params = {
                            kodi: kodi
                        };
                        controllerContext.callWebServiceLlogaritPagen(params, controllerContext.doneCallbackLlogaritPagen);
                    }
                }, 0);

            },
            this.rimerVlera = function () {
                var params = {
                    merrimporte: true,
                    kodimporti: "",
                    buttonClickNgaKomponente: true
                };
                controllerContext.callWebServiceLlogaritPagen(params, controllerContext.doneCallbackLlogaritPagen);
            },
            //rikthen vlerat mujore perpara se te beheshin ndryshimet e fundit te lupa e muajve
            this.callWebServiceDiscardNdryshimetPerMuajt = function () {
                $.ajax({
                    url: Utils.getServerApiUrl("ListPagesa", "DiscardNdryshimetPerMuajt"),
                    data: JSON.stringify({ idPunonjesi: pageState.idpunonjesi, })
                }).done(function (result) {
                    console.log("Vlerat u fshine!");
                })
            },
            //ruan ne cache  vlerat mujore qe jane ne trup te lp
            this.callWsRuajNdryshimetPerMuajt = function (callback) {
                $.ajax({
                    url: Utils.getServerApiUrl("ListPagesa", "RuajNdryshimetPerMuajt"),
                    data: JSON.stringify({ idPunonjesi: pageState.idpunonjesi, })
                }).done(function (result) {
                    console.log("Vlerat u ruajten!");
                    if (callback && typeof callback == 'function') callback();
                })
            },
            this.lejoEditimQelize = function (grida, focusedColumnName, focusedColumnIndex) {
                if (pageState.lidhur)
                    return false;

                pageState.focusedColumn = focusedColumnName;
                var njesia = parseInt(grida.batchEditApi.GetCellValue(focusedColumnIndex, "Njesia") || 0);

                if (pageState.focusedColumn !== "Vlera" && pageState.focusedColumn !== "VleraParam" && pageState.focusedColumn !== "Shenime")
                    return false;
                if (njesia === 0 && pageState.focusedColumn === "VleraParam")
                    return false;
                if (njesia === 1)
                    return false;
                if (njesia === 2 && !pageState.lejoModifikim[grida.batchEditApi.GetCellValue(focusedColumnIndex, "KodKomponente")] && pageState.focusedColumn == "Vlera")
                    return false;

                return true;

            }
            this.ndryshoFlamurinKaNdryshimeTePaRuajtura = function (kaNdryshime) {
                pageState.kaNdryshimeTePaRuajtura = kaNdryshime;
            }
            this.ndryshoFlamurinKaNdryshime = function (kaNdryshime) {
                window.parent.pageState.kaNdryshime = kaNdryshime;
            }
        };

        function handlersController() {
            //private
            var controller = new lupaKomponenteController();
            var handlersContext = this;
            
            var okClickPasPasiKaneMbaruarLlogaritjet = function () {
                var grida = app.view.grida;
                if (pageState.llogaritDiteLejeNgaImporti && model.dite > 0) { //nestila u hoq sepse ka klient qe nuk e duan fikse ditet
                    controller.shfaqMesazhPopup(hfState.Get("msgPerDitetEPunes"));
                    return;
                }
                if (model.dite < -model.ditemuaji) {
                    controller.shfaqMesazhPopup(hfState.Get("msgPerDitetEPunesJoNegative"));
                    return;
                }
                for (var i = 0, count = grida.pageRowCount ; i < count; i++) {
                    var kodi = grida.batchEditApi.GetCellValue(i, "KodKomponente");
                    if (pageState.komponenteTeDetyrueshme[kodi] && grida.batchEditApi.GetCellValue(i, "Vlera") === 0) {
                        controller.shfaqMesazhPopup(hfState.Get("msgPlotesoVlerenEKomponentes") + " " + kodi + "!");
                        return;
                    }
                }
                controller.formatoFushaDevi();
                controller.callWsRuajNdryshimetPerMuajt(function () {
                    //if (model.paguar !== 0) {
                    //nese ka page shtoje ne listpagese
                    controller.shtoPunonjesNeTrupinLp();
                    //}

                    var punonjesitStr = localStorage.getItem("punonjesitEZgjedhur");
                    var punonjesit = [];
                    if (!Utils.IsNullOrEmpty(punonjesitStr))
                        punonjesit = JSON.parse(punonjesitStr);
                    if (punonjesit.length > 0) {
                        controller.hapLupenEKomponenteveTePages(punonjesit.shift());
                        localStorage.setItem("punonjesitEZgjedhur", JSON.stringify(punonjesit));
                    } else {
                        app.view.popupUniversalPrindi.Hide();
                        localStorage.removeItem("punonjesitEZgjedhur");
                        
                    }
                })
            };
            //publike

            this.init = function () {
                try {
                    controller.callWsRuajNdryshimetPerMuajt(function () {

                        var colFillestare = pageState.colFillestare;
                        var grida = app.view.grida;
                        if(app.view.timeoutControll)
                            app.view.timeoutControll.sendKeepAlive();
                        myFaqeCelje.shtoHandlerSession();

                        app.view.btnImporti.SetEnabled(!pageState.lidhur);

                        var paguar = 0;
                        var nrKomponenteve = colFillestare.length;
                        var arrvlera = new Array(nrKomponenteve);
                        var arrvleraParam = new Array(nrKomponenteve);
                        var arrKodi = new Array(nrKomponenteve);
                        var arrMosNdrysho = new Array(nrKomponenteve);

                        ///vendos vlerat fillestare ne grid
                        setTimeout(function () {
                            for (var j = 0; j < nrKomponenteve; j++) {
                                arrvlera[j] = colFillestare[j].Vlera;
                                arrvleraParam[j] = colFillestare[j].VleraParam ? colFillestare[j].VleraParam : 0;
                                arrKodi[j] = colFillestare[j].KodKomponente;
                                arrMosNdrysho[j] = colFillestare[j].Modifikuar;

                                if (colFillestare[j].Tipi === 1)
                                    paguar = paguar + parseFloat(colFillestare[j].Vlera);
                                else if (colFillestare[j].Tipi === 2)
                                    paguar -= parseFloat(colFillestare[j].Vlera);

                                for (var i = 0, count = grida.pageRowCount ; i < count; i++) {
                                    var kodi = grida.batchEditApi.GetCellValue(i, "KodKomponente");
                                    if (colFillestare[j].KodKomponente === kodi) {
                                        grida.batchEditApi.SetCellValue(i, "Vlera", colFillestare[j].Vlera);
                                        grida.batchEditApi.SetCellValue(i, "VleraParam", colFillestare[j].VleraParam);
                                        grida.batchEditApi.SetCellValue(i, "Shenime", colFillestare[j].Shenime);
                                        colFillestare[j].EDetyrueshme = pageState.komponenteTeDetyrueshme[kodi];

                                        break;
                                    }
                                }
                            }

                            model.vendosVleraFillestareNeModel(arrKodi, arrvlera, arrvleraParam, arrMosNdrysho, colFillestare, paguar);
                            app.view.txtPaguar.SetText(paguar);

                            controller.ndryshoKonfigFormatNumri();
                            controller.formatoFushaDevi();

                            if (!pageState.ekzistonNeLp) {
                                //nese eshte punonjes i ri rillogarit komponentet
                                var params = {
                                    merrimporte: true,
                                    kodimporti: "",
                                    newrecord: true
                                };
                                controller.callWebServiceLlogaritPagen(params, controller.doneCallbackLlogaritPagen);
                            } else {
                                ListPagesaUtils.RillogaritTotaletPerGrup(app.view.grida, pageState.formatNumri.ShifraPasPresjesVlefta, "Vlera");
                            }
                        }, 200);//sepse jep problem ne mozilla batchedit

                    });
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
            this.processKeyPress = function (event) {
                var currentIndex = app.view.grida.GetFocusedRowIndex();
                if (event.keyCode === 40) {//ArrowDown
                    if (currentIndex === app.view.grida.pageRowCount - 1) {
                        app.view.grida.SetFocusedRowIndex(0);
                    } else {
                        app.view.grida.SetFocusedRowIndex(currentIndex + 1);
                    }
                }
                if (event.keyCode === 38) {//ArrowUp
                    if (currentIndex === 0) {
                        return false;
                    } else {
                        app.view.grida.SetFocusedRowIndex(currentIndex - 1);
                    }
                }
                if (event.keyCode === 13) {//Enter
                    return false;
                }

            },
            this.btnImportiClick = function (s, e) {
                controller.rimerVlera();
            },
            this.btnOkClick = function (s, e) {
                e.processOnServer = false;
                setTimeout(function() {
                    app.view.grida.SetFocusedRowIndex(-1); //qe te bej llogaritje;
                    if (!Utils.KanePerfunduarWs()) {
                        Utils.shtoFunksionNeRadheMeParametra(okClickPasPasiKaneMbaruarLlogaritjet, handlersContext, parseInt(hfState.Get("idGjuha")));
                        return;
                    }
                    okClickPasPasiKaneMbaruarLlogaritjet();
                }, 0);
            },
            this.endCallbackGrida = function (s, e) {
                controller.veprimeServerSide(s, e);
            },
            this.BatchEditStartEditingGrida = function (s, e) {
                e.cancel = !controller.lejoEditimQelize(s, e.focusedColumn.fieldName, e.visibleIndex);
            },
            this.BatchEditEndEditingGrida = function (s, e) {
                if (e.ngaMuajt) {
                    controller.BatchEnd(s, e.visibleIndex, e.vleratPara, e.vleratPas);
                    if (e.callback != undefined)
                        e.callback();
                } else {

                    var vleratPara = {
                        vlera: s.batchEditApi.GetCellValue(e.visibleIndex, "Vlera"),
                        vleraParam: s.batchEditApi.GetCellValue(e.visibleIndex, "VleraParam")
                    };
                    var vleratPas = {
                        vlera: e.rowValues[(s.GetColumnByField("Vlera").index)].value,
                        vleraParam: e.rowValues[(s.GetColumnByField("VleraParam").index)].value
                    };
                    controller.BatchEnd(s, e.visibleIndex, vleratPara, vleratPas);
                }


            },
            this.BatchEditRowValidatingGrida = function (s, e) {

                var kolonaVlera = controller.merrKolonePerValidim(s, e, "Vlera");
                var kolonaVleraParam = controller.merrKolonePerValidim(s, e, "VleraParam");
                if (kolonaVlera == undefined || kolonaVleraParam == undefined) return;

                if (kolonaVlera.value.toString().search(":") == -1) {

                    if (isNaN(kolonaVlera.value)) {
                        kolonaVlera.isValid = false;
                        controller.shfaqMesazhPopup(hfState.Get("msgVleraDuhetJeteNumer"));
                        s.batchEditApi.SetCellValue(e.visibleIndex, "Vlera", 0);
                        return;
                    }

                }

                if (kolonaVleraParam.value.toString().search(":") == -1) {
                    if (isNaN(kolonaVleraParam.value)) {
                        kolonaVleraParam.isValid = false;
                        controller.shfaqMesazhPopup(hfState.Get("msgParametriNumer"));
                        s.batchEditApi.SetCellValue(e.visibleIndex, "VleraParam", 0);

                        return;
                    }
                    if (parseFloat(kolonaVleraParam.value) < 0) {
                        kolonaVleraParam.isValid = false;
                        controller.shfaqMesazhPopup(hfState.Get("msgParametriPozitive"));
                        s.batchEditApi.SetCellValue(e.visibleIndex, "VleraParam", 0);
                        return;
                    }
                }
            },
            this.RowCollapsingGrida = function (s, e) {
                s.UpdateEdit();
            },
            this.RowDblClickGrida = function (s, e) {
                controller.hapLupaMuaj(s, e);
            },
            this.btnKthehuInit = function (s, e) {
                var contentUrl = localStorage.getItem("lupaPunonjesitURL");
                s.SetVisible((contentUrl != undefined && contentUrl !== ""));
            },
            this.btnKthehuClick = function (s, e) {
                var contentUrl = localStorage.getItem("lupaPunonjesitURL");
                if (contentUrl != undefined && contentUrl !== "") {
                    app.view.popupUniversalPrindi.SetContentUrl(contentUrl);
                    app.view.popupUniversalPrindi.SetHeaderText(localStorage.getItem("lupaPunonjesitHeaderText"));
                }
                e.processOnServer = false;
            },
            this.windowBeforeUnloaded = function (s, e) {
                if (pageState.kaNdryshimeTePaRuajtura)
                    controller.callWebServiceDiscardNdryshimetPerMuajt();
            }
            this.popupUniversalCloseUp = function (s, e) {

                app.view.popupUniversal.SetContentUrl("");
            };
            this.onKeyPressed = function (event) {

                if (!(event.key == "ArrowDown") && !(event.key == "ArrowUp"))
                    return;

                var currentColumn = app.view.grida.GetFocusedCell().column;
                var currentIndex = app.view.grida.GetFocusedRowIndex();
                var newRowIndex;

                /*perdorim cikel pasi ne rastet kur kemi nje komponente ku nuk plotesohet asnje nga fushat
                  ose eshte nje rresht i cili shfaq total (llogaritet si visible index por nuk ke cte editosh)
                  duhet te kontrollojme deri sa te gjejme rreshtin e rradhes se editueshem
                */
                while (true) {
                    if (event.key == "ArrowDown") {

                        if (currentIndex === app.view.grida.pageRowCount - 1)
                            newRowIndex = 0;
                        else
                            newRowIndex = currentIndex + 1;

                    } else if (event.key == "ArrowUp") {
                        if (currentIndex === 0)
                            newRowIndex = app.view.grida.pageRowCount - 1;
                        else
                            newRowIndex = currentIndex - 1;

                    }
                    currentIndex = newRowIndex;//per kur te rikthehet ne cikel qe te vazhdoje ne rreshtin e rradhes
                    if (!app.view.grida.batchEditApi.GetCellValue(newRowIndex, "Njesia"))
                        //rreshtat e totaleve grupues do kene vlere null dhe grida do focusohet ne nje qelize te padukshme
                        continue;

                    var newColumn;
                    if (controller.lejoEditimQelize(app.view.grida, "Vlera", newRowIndex))
                        newColumn = app.view.grida.GetColumnByField("Vlera")
                    else if (controller.lejoEditimQelize(app.view.grida, "VleraParam", newRowIndex))
                        newColumn = app.view.grida.GetColumnByField("VleraParam")

                    if (!newColumn)
                        continue;
                    app.view.grida.batchEditApi.EndEdit();
                    app.view.grida.batchEditApi.StartEdit(newRowIndex, newColumn.index);
                    return;
                }


            }
        };

        return {
            Komponente: lupaKomponenteController,
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
        publicFunc: {
            vendosKaNdryshimeTePaRuajtura: new controllers.Komponente().ndryshoFlamurinKaNdryshimeTePaRuajtura
        },
        pageState: app.pageState
    };

};

var lupaKomponente = new LupaKomponenteApp();
$(document).ready(lupaKomponente.initApp)
    .resize(lupaKomponente.handlers.resizePage)
    .keypress(lupaKomponente.handlers.processKeyPress);
$(window).on("beforeunload", lupaKomponente.handlers.windowBeforeUnloaded);
