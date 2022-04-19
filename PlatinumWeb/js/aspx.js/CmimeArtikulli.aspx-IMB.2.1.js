;
function CmimeApp() {
    this.runMode = true; //per intellisense behet false
    this.view = undefined;
    var app = this;
    function View() {
        var runMode = app.runMode;
        return {
            hfState: runMode ? window.hfState : new ASPxClientHiddenField(),
            popupUniversal: runMode ? window.popupUniversal : new ASPxClientPopupControl(),
            loadingPanel: runMode ? window.LoadingPanel : new ASPxClientLoadingPanel(),
            timeoutControll: window.parent.window.parent.SessionTimeout,
            cmbKonfigurimi: runMode ? window.cmbKonfigurimi : new ASPxClientComboBox(),
            lblKonfigurimiP: runMode ? window.lblKonfigurimiP : new ASPxClientLabel(),
            lblMsgbox: runMode ? window.lblMsgbox : new ASPxClientLabel(),
            cmbRritjeZbritje: runMode ? window.cmbRritjeZbritje : new ASPxClientComboBox(),
            dteDtFillimi2: runMode ? window.dteDtFillimi2 : new ASPxClientDateEdit(),
            dteDtMbarimi2: runMode ? window.dteDtMbarimi2 : new ASPxClientDateEdit(),
            dteKoheFillimi2: runMode ? window.dteKoheFillimi2 : new ASPxClientTimeEdit(),
            dteKoheMbarimi2: runMode ? window.dteKoheMbarimi2 : new ASPxClientTimeEdit(),
            cmbVlerePerqindje: runMode ? window.cmbVlerePerqindje : new ASPxClientComboBox(),
            cbKosto: runMode ? window.cbKosto : new ASPxClientCheckBox(),
            cbGjendje: runMode ? window.cbGjendje : new ASPxClientCheckBox(),
            txtVlera: runMode ? window.txtVlera : new ASPxClientTextBox(),
            lblVlera: runMode ? window.lblVlera : new ASPxClientLabel(),
            PricesDataGrid: null,
            cmbNivelCmimi: null,
            timeout: null
        };
    }

    if (!app.runMode)
        app.view = new View();

    //variabla global ne faqe te cilet ruajne gjendjen e faqes
    this.pageState = {
        lupaNdermarrjeBijaURL: "LupaNdermarjeBij.aspx?vjenNga=Cmime",
        konfigFillestar: "",
        focusedColumn: undefined,
        idPerdoruesi: 0,
        idNdermarrje: 0,
        idGjuha: 0,
        idViti: 0,
        idKonfigAmbjente: 0,
        pricesDataGridId: 0,
        formatNumriZgjedhur: "",
        headerPopUpTextZgjidhNdermarrjet: "",
        msgCmimeArtikulliArtikujTeNdryshuarPorTePaSelektuar: "",
        msgCmimeArtikulliDoniTeVazhdoni: "",
        merrKosto: true,
        rifreskoGride: false,
        initState: function () {
            this.headerPopUpTextZgjidhNdermarrjet = hfState.Get("headerPopUpTextZgjidhNdermarrjet");
            this.idPerdoruesi = app.view.hfState.Get("idPerdoruesi");
            this.idNdermarrje = hfState.Get("idNdermarrje");
            this.idGjuha = hfState.Get("idGjuha");
            this.idViti = hfState.Get("idViti");
            this.msgCmimeArtikulliArtikujTeNdryshuarPorTePaSelektuar = hfState.Get("msgCmimeArtikulliArtikujTeNdryshuarPorTePaSelektuar");
            this.msgCmimeArtikulliDoniTeVazhdoni = hfState.Get("msgCmimeArtikulliDoniTeVazhdoni");
            this.konfigFillestar = JSON.parse(hfState.Get("konfigFillestar"));
            this.merrKosto = true;
            this.veprimi = "";
            this.cmbCmimeArtikulliRritje = app.view.hfState.Get("cmbCmimeArtikulliRritje");
            this.cmbCmimeArtikulliZbritje = app.view.hfState.Get("cmbCmimeArtikulliZbritje");
            this.cmbCmimeArtikulliBarazim = app.view.hfState.Get("cmbCmimeArtikulliBarazim");
            this.cmbCmimeArtikulliVlere = app.view.hfState.Get("cmbCmimeArtikulliVlere");
            this.cmbCmimeArtikulliPerqidje = app.view.hfState.Get("cmbCmimeArtikulliPerqidje");
            this.cmbCmimeArtikulliKosto = app.view.hfState.Get("cmbCmimeArtikulliKosto");
        },
        artikullCmimNenZero: 0,
        cmimePaKosto: true,
        niveleCmimi: null
    };

    var controllers = (function () {

        var pageState = app.pageState;

        function CmimeController() {
            // private
            var controllerContext = this;

            // publike
            this.hidePopupUniversal = function () {
                app.view.popupUniversal.Hide();
            },
            this.shfaqMesazhPopup = function (mesazhi) {
                //ky funksion duhet te behet i pergjithshem
                //ku mund te perdoret nje popup custom jo alert
                window.alert(mesazhi);
            },
            this.SucceededCallbackKonfig = function (result) {
                if (typeof (result) !== "undefined") {
                    var colKontrollet = result.colKontrollet;
                    var colAtrTrupi = result.colAtrTrupi;
                    var hf = $("#hfKontrollet");
                    $("#divgride1").show();
                    var arrPrind = ["dvVlera"];
                    var arrTabela = ["tblVlera"];
                    myFaqeCelje.SucceededCallbackKonfigurimPergjithshem(colKontrollet, colAtrTrupi, hf, undefined, "", arrTabela, undefined, undefined, undefined, arrPrind);
                    app.view.txtVlera.SetVisible(true);
                    pageState.formatNumriZgjedhur = result.formatNumriZgjedhur;
                    controllerContext.ChangeCmbRritjeZbritje(app.view.cmbRritjeZbritje.GetText());
                    app.view.PricesDataGrid = null;
                    app.view.PricesDataGrid = controllerContext.CreatePricesDataGrid(result.colGrida, result.colFiltraGrida.filter(function (item) { return item.FiltraKodi == "FilterDefault"})[0]);
                    pageState.pricesDataGridId = result.colGrida[0].IdKoka;
                    pageState.idKonfigAmbjente = result.colAtrTrupi[0].IdKonfigAmbjente;
                    controllerContext.callWebServiceKtheHapjeFillestare();
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
                controllerContext.callWebserviceKonfigurimi("411", konfigText);
            },
            this.changeName = function () {
                if (window.parent.callWebServiceKtheInfoLart)
                    window.parent.callWebServiceKtheInfoLart('CmimeArtikulli.aspx?lloji=' + Utils.getUrlVar('lloji'), 0);
            },
            this.RuajKonfigurimGride = function () {
                var gridId = app.pageState.pricesDataGridId;
                var idGjuha = app.pageState.idGjuha;
                var idNdermarrje = app.pageState.idNdermarrje;
                var idViti = app.pageState.idViti;
                var idPerdoruesi = app.pageState.idPerdoruesi;
                var idKonfigAmbjente = app.pageState.idKonfigAmbjente;
                app.view.PricesDataGrid.SaveGridConfiguration(gridId, idGjuha, idNdermarrje, idViti, idPerdoruesi, "FilterDefault", "");
                $.ajax({
                    pritPergjigje: true,
                    url: Utils.getServerApiUrl("Cmime", "RuajFilterNivelCmimi"),
                    data: JSON.stringify({
                        idKonfigAmbjente: idKonfigAmbjente, idNivelCmimi: app.view.cmbNivelCmimi.option("value"), idGjuha: idGjuha,
                        idNdermarrje: idNdermarrje, idViti: idViti, idPerdoruesi: idPerdoruesi
                    })
                }).done(function (result) { myMesazh.ShtoMesazhSesioni(result); });
            },
            this.MerrGjendjeKosto = function () {
                var itemCountRritjeZbr = app.view.cmbRritjeZbritje.GetItemCount();
                var meKosto = app.view.cbKosto.GetChecked();
                var meGjendje = app.view.cbGjendje.GetChecked();
                var idNivelCmimi = app.view.cmbNivelCmimi.option("value");
                if ((meKosto || meGjendje) && pageState.cmimePaKosto && idNivelCmimi != null) {
                    this.GetPriceDataGridDataSource(idNivelCmimi);
                }
                pageState.cmimePaKosto = !meKosto && !meGjendje;

                if (meKosto && itemCountRritjeZbr === 2) {
                    app.view.cmbRritjeZbritje.AddItem(pageState.cmbCmimeArtikulliBarazim, pageState.cmbCmimeArtikulliBarazim);
                }
                if (!meKosto && itemCountRritjeZbr === 3) {
                    app.view.cmbRritjeZbritje.RemoveItem(2);
                }
                app.view.PricesDataGrid.AddCustomOptionToColumns(["Kosto"], "visible", meKosto);
                app.view.PricesDataGrid.AddCustomOptionToColumns(["Gjendje"], "visible", meGjendje);
            },
            this.NdryshoCmimetESelektuar = function () {
                var selectedData = app.view.PricesDataGrid.Grida.getSelectedRowsData();
                if (selectedData.length < 1) {
                    myMesazh.ShtoMesazhGabimi("Nuk ka rreshta te selektuar!");
                    return;
                }
                if (app.view.cmbRritjeZbritje.GetText() !== "Barazim") {
                    if (Utils.IsNullOrWhiteSpace(app.view.txtVlera.GetText())) {
                        myMesazh.ShtoMesazhGabimi("Vlera nuk mund te jete bosh!");
                        return;
                    }
                    if (isNaN(app.view.txtVlera.GetText())) {
                        myMesazh.ShtoMesazhGabimi("Vlera duhet te jete numer!");
                        return;
                    }
                }
                var rritjeZbritje = app.view.cmbRritjeZbritje.GetText();
                var perqindje = app.view.cmbVlerePerqindje.GetText();
                var vlera = parseFloat(app.view.txtVlera.GetText());
                var dtFillimi = app.view.dteDtFillimi2.GetDate();
                var dtMbarimi = app.view.dteDtMbarimi2.GetDate();
                var koheFillimi = app.view.dteKoheFillimi2.GetDate();
                var koheMbarimi = app.view.dteKoheMbarimi2.GetDate();
                var idNivelCmimi = app.view.cmbVlerePerqindje.GetValue();
                var idPerdoruesi = app.view.hfState.Get("idPerdoruesi");
                pageState.artikullCmimNenZero = 0;
                var barazimNiveli = (rritjeZbritje == pageState.cmbCmimeArtikulliBarazim && idNivelCmimi != 0);

                var selectedArtikullIds = new Array();
                selectedData.map(function (cmimArtikulli) {
                    cmimArtikulli.Update = true;//modifikuar
                    cmimArtikulli.IdPerdoruesi = idPerdoruesi;
                    if (barazimNiveli) {
                        selectedArtikullIds.push(cmimArtikulli.IdArtikulli);
                        return;
                    }
                    var cmimi = controllerContext.KalkuloDheKtheCmim(cmimArtikulli, rritjeZbritje, perqindje, vlera);
                    controllerContext.AplikoNdryshimeNeCmimArtikulli(cmimArtikulli, cmimi, dtFillimi, dtMbarimi, koheFillimi, koheMbarimi);
                });


                if (barazimNiveli && selectedArtikullIds.length > 0) {
                    controllerContext.AplikoBarazimCmimArtikulli(selectedData, selectedArtikullIds, idNivelCmimi, dtFillimi, dtMbarimi, koheFillimi, koheMbarimi);
                    return;
                }

                if (pageState.artikullCmimNenZero > 0)
                    myMesazh.ShtoMesazhSuksesi("Ndryshimi u aplikua me sukses! Per nje ose disa artikuj cmimi ju be 0 (zero) sepse nga zbritja ai behej negativ!");
                else
                    myMesazh.ShtoMesazhSuksesi("Ndryshimi u aplikua me sukses!");

                app.view.PricesDataGrid.Refresh();
            },
            this.Ruaj = function () {
                var dataSource = app.view.PricesDataGrid.GetData().filter(function (item) { return item.Update == true; });
                if (dataSource.length < 1) {
                    myMesazh.ShtoMesazhInformues("Ju nuk keni ndryshime per te ruajtur.");
                    return;
                }
                $.ajax({
                    url: Utils.getServerApiUrl("Cmime", "RuajListeCmimesh"),
                    showLoading: true,
                    data: JSON.stringify({
                        cmimeObject: dataSource,
                        shitjeApoBlerje: Utils.getUrlVar('lloji') == "shitje" ? 0 : 1,
                        lupe: (Utils.getUrlVar('lupe') && (Utils.getUrlVar('lupe') == true || Utils.getUrlVar('lupe') == 'true')),
                        merrKosto: (app.view.cbGjendje.GetChecked() || app.view.cbKosto.GetChecked()),
                        idNivelCmimi: app.view.cmbNivelCmimi.option("value")
                    })
                }).done(function (result) {
                    if (result.Status) {
                        myMesazh.ShtoMesazhSuksesi(result.PershkrimMesazhi);
                        dataSource.map(function (item) { item.Update = false; });
                        app.view.PricesDataGrid.Grida.clearSelection();
                        app.view.PricesDataGrid.Refresh();
                        if (Utils.getUrlVar('lupe') && (Utils.getUrlVar('lupe') == true || Utils.getUrlVar('lupe') == 'true'))
                            window.parent.cmimet.pageState.rifreskoGride = true;
                    }
                    else
                        myMesazh.ShtoMesazhGabimi(result.PershkrimMesazhi);
                });
            },
            this.KalkuloDheKtheCmim = function (cmimArtikulli, rritjeZbritje, perqindje, vlera) {
                var cmimi = cmimArtikulli.Cmimi;
                var vleraShtuar = perqindje == pageState.cmbCmimeArtikulliPerqidje ? (cmimi * (vlera/100)) : vlera;
                switch (rritjeZbritje) {
                    case pageState.cmbCmimeArtikulliRritje:
                        cmimi += vleraShtuar;
                        break;
                    case pageState.cmbCmimeArtikulliZbritje:
                        if (cmimi < vleraShtuar) {
                            cmimi = 0;
                            pageState.artikullCmimNenZero++;
                        }
                        else
                            cmimi -= vleraShtuar;
                        break;
                    case pageState.cmbCmimeArtikulliBarazim:
                        if (perqindje == pageState.cmbCmimeArtikulliKosto && cmimArtikulli.Kosto != -50000000)
                            cmimi = cmimArtikulli.Kosto;
                        break;
                }
                return cmimi;
            },
            this.AplikoNdryshimeNeCmimArtikulli = function (cmimArtikulli, cmimi, dtFillimi, dtMbarimi, koheFillimi, koheMbarimi) {
                cmimArtikulli.Cmimi = cmimi;
                cmimArtikulli.Cmimi2 = cmimi * cmimArtikulli.Koeficent;
                cmimArtikulli.CmimiTvsh = cmimi * (1 + cmimArtikulli.Norme / 100);
                cmimArtikulli.Cmimi2Tvsh = (cmimi * cmimArtikulli.Koeficent) * (1 + cmimArtikulli.Norme / 100);

                if (dtFillimi)
                    cmimArtikulli.DateFillimi = dtFillimi;
                if (dtMbarimi)
                    cmimArtikulli.DateMbarimi = dtMbarimi;
                if (koheFillimi.getTime() != new Date(2016, 1, 1, 0, 0, 0).getTime())
                    cmimArtikulli.KoheFillimi = koheFillimi;
                if (koheMbarimi.getTime() != new Date(2016, 1, 1, 23, 59, 0).getTime())
                    cmimArtikulli.KoheMbarimi = koheMbarimi;
            },
            this.AplikoBarazimCmimArtikulli = function (selectedData, artIds, idNivelCmimi, dtFillimi, dtMbarimi, koheFillimi, koheMbarimi) {
                $.ajax({
                    url: Utils.getServerApiUrl("Cmime", "KtheCmimArtikujshSipasNivelit"),
                    data: JSON.stringify({
                        artIds: artIds.join(','),
                        idNivelCmimi: idNivelCmimi
                    })
                }).done(function (cmimet) {
                    if (cmimet.length > 0) {
                        selectedData.map(function (cmimArtikulli) {
                            var item = cmimet.filter(function (cmimArt) { return cmimArt.IdArtikulli == cmimArtikulli.IdArtikulli; })[0];
                            if (item)
                                controllerContext.AplikoNdryshimeNeCmimArtikulli(cmimArtikulli, item.Cmimi, dtFillimi, dtMbarimi, koheFillimi, koheMbarimi);
                        });
                        app.view.PricesDataGrid.Refresh();
                    }
                    myMesazh.ShtoMesazhSuksesi("Ndryshimi u aplikua me sukses!");
                });
            },
            this.Pastro = function () {
                app.view.cmbRritjeZbritje.SetSelectedIndex(0);
                app.view.cmbVlerePerqindje.SetSelectedIndex(0);
                app.view.txtVlera.SetText(0);
                app.view.dteDtFillimi2.SetText();
                app.view.dteDtMbarimi2.SetText();
                app.view.dteKoheFillimi2.SetDate(new Date(2016, 1, 1, 0, 0, 0));
                app.view.dteKoheMbarimi2.SetDate(new Date(2016, 1, 1, 23, 59, 0));
                app.view.PricesDataGrid.Grida.cancelEditData();
                var idNivelCmimi = app.view.cmbNivelCmimi.option("value");
                if (idNivelCmimi != null)
                    this.GetPriceDataGridDataSource(idNivelCmimi);
            },
            this.vendosVleraComboVlerePerqindje = function (result) {
                app.view.cmbVlerePerqindje.ClearItems();
                app.view.cmbVlerePerqindje.BeginUpdate();
                if (app.view.cbKosto.GetChecked())
                    app.view.cmbVlerePerqindje.AddItem('Kosto', 0);
                for (var i = 0; i < result.length; i++) {
                    app.view.cmbVlerePerqindje.AddItem(result[i].PershkrimNivelCmimi, result[i].IdNivelCmimi);
                }
                app.view.cmbVlerePerqindje.EndUpdate();
                app.view.cmbVlerePerqindje.SetSelectedIndex(0);
                if (cbKosto.GetChecked())
                    app.view.cmbVlerePerqindje.SetText('Kosto');
            },
            this.ChangeCmbRritjeZbritje = function (vleraKombo) {
                var cmbVlerePerqindje = app.view.cmbVlerePerqindje;
                switch (vleraKombo) {
                    case "Barazim":
                        $.ajax({
                            pritPergjigje: true,
                            url: Utils.getServerApiUrl("Konfigurime", "merrNiveleCmimeshNdermarrjeSipasLlojit"),
                            data: JSON.stringify({ idNdermarrje: pageState.idNdermarrje, lloji: Utils.getUrlVar('lloji') == "shitje" ? 0 : 1 })
                        }).done(function (result) { controllerContext.vendosVleraComboVlerePerqindje(result); });
                        break;
                    case "Rritje":
                    case "Zbritje":
                        cmbVlerePerqindje.ClearItems();
                        cmbVlerePerqindje.AddItem("Perqindje", 0);
                        cmbVlerePerqindje.AddItem("Vlere", 1);
                        cmbVlerePerqindje.SetSelectedIndex(0);
                        app.view.lblVlera.SetText(cmbVlerePerqindje.GetText());
                        break;
                }
            },
            this.callWebServiceKtheHapjeFillestare = function () {
                Utils.shfaqLoadingGif();
                $.ajax({
                    url: Utils.getServerApiUrl("Cmime", "KtheDataSourceKolonash"),
                    data: JSON.stringify({
                        shitjeApoBlerje: Utils.getUrlVar('lloji') == "shitje" ? 0 : 1,
                        lupe: (Utils.getUrlVar('lupe') && (Utils.getUrlVar('lupe') == true || Utils.getUrlVar('lupe') == 'true')),
                        merrKosto: (app.view.cbGjendje.GetChecked() || app.view.cbKosto.GetChecked())
                    })
                }).done(function (result) {
                    app.view.PricesDataGrid.AddCustomOptionToColumns(["KodifikimArtikulli1", "KodifikimArtikulli2"], "lookup", { dataSource: result.kodifikimeArtikulli, displayExpr: "KodKodifikimi", valueExpr: "IdKodifikimi" });
                    app.view.PricesDataGrid.AddCustomOptionToColumns(["IdMonedha"], "lookup", { dataSource: result.monedha, displayExpr: "KodiMonedha", valueExpr: "IdMonedha" });
                    app.view.PricesDataGrid.AddCustomOptionToColumns(["IdTvsh"], "lookup", { dataSource: result.niveleTvsh, displayExpr: "KodTaksa", valueExpr: "IdTaksa" });
                    app.view.PricesDataGrid.AddCustomOptionToColumns(["IdDetajim"], "lookup", { dataSource: result.detajimeArtikulli, displayExpr: "KodDetajimArtikulli", valueExpr: "IdDetajimArtikulli" });
                    app.pageState.niveleCmimi = Utils.CloneObject(result.niveleCmimi);
                    result.niveleCmimi.unshift({ IdNivelCmimi: -1, PershkrimNivelCmimi: "Te gjithe" });
                    app.view.cmbNivelCmimi.option('dataSource', result.niveleCmimi);
                    if (hfState.Get("nivelCmimiDefault") != 0)
                        app.view.cmbNivelCmimi.option("value", hfState.Get("nivelCmimiDefault"));
                    else
                        Utils.hiqLoadingGif();
                });  
            },
            this.GetPriceDataGridDataSource = function (idNivelCmimi) {
                $.ajax({
                    showLoading: true,
                    url: Utils.getServerApiUrl("Cmime", "KtheListeCmimesh"),
                    data: JSON.stringify({
                        shitjeApoBlerje: Utils.getUrlVar('lloji') == "shitje" ? 0 : 1,
                        lupe: (Utils.getUrlVar('lupe') && (Utils.getUrlVar('lupe') == true || Utils.getUrlVar('lupe') == 'true')),
                        merrKosto: (app.view.cbGjendje.GetChecked() || app.view.cbKosto.GetChecked()),
                        idNivelCmimi: idNivelCmimi
                    })
                }).done(function (result) {
                    app.view.PricesDataGrid.SetDataSource(result);
                    app.view.PricesDataGrid.Refresh();
                    Utils.hiqLoadingGif();
                });
            },
            this.GetCmbNivelCmimi = function () {
                return {
                    widget: "dxSelectBox",
                    cssClass: "blue-border-toolbar-widget",
                    options: {
                        placeholder: "Zgjidh Nivelin...", displayExpr: "PershkrimNivelCmimi", valueExpr: "IdNivelCmimi", searchEnabled: true,
                        onInitialized: function (e) { app.view.cmbNivelCmimi = e.component; },
                        onValueChanged: function (item) {
                            if (pageState.cmbNiveliValueReseted) {
                                pageState.cmbNiveliValueReseted = false;
                                return;
                            }
                            if (item.value == null) {
                                pageState.cmbNiveliValueReseted = true;
                                this._setValue(item.previousValue);
                                return;
                            }
                            if (app.view.PricesDataGrid.Grida.hasEditData() || app.view.PricesDataGrid.GetData().filter(function (item) { return item.Update == true; }).length > 0) {
                                var widget = this;
                                window.parent.myMesazh.ShtoMesazh({
                                    type: "confirm",
                                    UseCancelButton: true,
                                    text: "Keni ndryshime te paruajtura, deshironi te vazhdoni?",
                                    modal: true,
                                    idGjuha: pageState.idGjuha,
                                    okClick: function () { app.view.PricesDataGrid.Grida.cancelEditData(); controllerContext.GetPriceDataGridDataSource(item.value); },
                                    cancelClick: function () { pageState.cmbNiveliValueReseted = true; widget._setValue(item.previousValue); return; }
                                });
                                return;
                            }
                            app.view.PricesDataGrid.AddCustomOptionToColumns(["IdNivelCmimi"], "filterValue", null);
                            app.view.PricesDataGrid.AddCustomOptionToColumns(["IdNivelCmimi"], "lookup", { dataSource: app.pageState.niveleCmimi.filter(function (nivel) { return (nivel.IdNivelCmimi == item.value || item.value == -1); }), displayExpr: "PershkrimNivelCmimi", valueExpr: "IdNivelCmimi" });
                            controllerContext.GetPriceDataGridDataSource(item.value);
                        }
                    },
                    location: "before"
                };
            },
            this.OnGridToolbarPreparing = function (e) {
                var toolbarItems = e.toolbarOptions.items;
                $.each(toolbarItems, function (_, item) {
                    if (item.name == "saveButton" || item.name == "revertButton") {
                        item.visible = false;
                    }
                });
                toolbarItems.push({ widget: "dxButton", options: { icon: "images/theme/MetropolisBlue/grida/wrench.png", text: "", onClick: function () { app.view.PricesDataGrid.Grida.showColumnChooser(); } }, location: "before" });
                toolbarItems.push({ widget: "dxButton", options: { icon: "images/theme/MetropolisBlue/grida/disk_blue (3).png", text: "", onClick: function () { controllerContext.RuajKonfigurimGride(); } }, location: "before" });
                toolbarItems.push({ widget: "dxButton", options: { icon: "images/theme/MetropolisBlue/grida/check2.png", text: "", onClick: function () { app.view.PricesDataGrid.Grida.option("selection.selectAllMode", "page"); app.view.PricesDataGrid.Grida.selectAll(); } }, location: "before" });
                toolbarItems.push({ widget: "dxButton", options: { icon: "images/theme/MetropolisBlue/grida/checks.png", text: "", onClick: function () { app.view.PricesDataGrid.Grida.option("selection.selectAllMode", "allPages"); app.view.PricesDataGrid.Grida.selectAll(); } }, location: "before" });
                toolbarItems.push({ widget: "dxButton", options: { icon: "images/theme/MetropolisBlue/grida/uncheck2.png", text: "", onClick: function () { app.view.PricesDataGrid.Grida.clearSelection(); } }, location: "before" });
                toolbarItems.push({ widget: "dxButton", options: { icon: "images/theme/MetropolisBlue/grida/xlsx24.png", text: "", onClick: function () { app.view.PricesDataGrid.Grida.exportToExcel(true); } }, location: "before" });
                toolbarItems.push(controllerContext.GetCmbNivelCmimi());
                toolbarItems.unshift({ location: "after", template: $("<div class='label'> rreshta te selektuar</div>") });
                toolbarItems.unshift({ location: "after", template: $("<div class='label' id='nrRreshtaTeSelektuar'>0</div>") });
            },
            this.OnGridRowPrepared = function (row) {
                if (row.rowType == 'data') {
                    if (Math.abs(row.rowIndex) % 2 == 1)
                        row.rowElement.css("background", row.data.Update ? "rgba(255,255,125, 0.2)" : "#ffffff");
                    else
                        row.rowElement.css("background", row.data.Update ? "rgba(255,255,125, 0.4)" : "#f5f5f5");
                }
            },
            this.OnGridSelectionChanged = function (e) {
                $("#nrRreshtaTeSelektuar").text(app.view.PricesDataGrid.Grida.getSelectedRowKeys().length);
            },
            this.OnGridContentReady = function (e) {
                var columnChooserView = e.component.getView("columnChooserView");
                if (!columnChooserView._popupContainer) {
                    columnChooserView._initializePopupContainer();
                    columnChooserView.render();
                    columnChooserView._popupContainer.option("position", { of: e.element, my: "center", at: "center" });
                }
            },
            this.PricesDataGridSelectRow = function(key){
                var selectedRowKeys = app.view.PricesDataGrid.Grida.getSelectedRowKeys();
                selectedRowKeys.push(key);
                app.view.PricesDataGrid.Grida.selectRows(selectedRowKeys);
            },
            this.CellValueIdTvsh = function (newData, value, row) {
                controllerContext.PricesDataGridSelectRow(row.IdCmimArtikulli);
                newData[this.dataField] = value;
                newData.Update = true;
                var norma = app.view.PricesDataGrid.Grida.columnOption("IdTvsh").lookup.dataSource.filter(function (item) { return item.IdTaksa == value })[0].NormaPerqindje;
                var cmimiTvsh = row.Cmimi * (1 + norma / 100);
                var cmimi2Tvsh = row.Cmimi2 * (1 + norma / 100);
                if (cmimiTvsh != 0)
                    newData.CmimiTvsh = cmimiTvsh;
                if (cmimi2Tvsh != 0)
                    newData.Cmimi2Tvsh = cmimi2Tvsh;
                newData.Norme = norma;
            },
            this.CellValueCmimet = function (newData, value, row) {
                controllerContext.PricesDataGridSelectRow(row.IdCmimArtikulli);
                newData[this.dataField] = value;
                newData.Update = true;
                switch (this.dataField) {
                    case "Cmimi":
                        newData.CmimiTvsh = value * (1 + row.Norme / 100);
                        newData.Cmimi2 = value * row.Koeficent;
                        newData.Cmimi2Tvsh = newData.Cmimi2 * (1 + row.Norme / 100);
                        break;
                    case "Cmimi2":
                        newData.Cmimi2Tvsh = value * (1 + row.Norme / 100);
                        if (row.NjesiTeVarura) {
                            newData.Cmimi = value / row.Koeficent;
                            newData.CmimiTvsh = newData.Cmimi * (1 + row.Norme / 100);
                        }
                        break;
                    case "CmimiTvsh":
                        newData.Cmimi = value / (1 + row.Norme / 100);
                        newData.Cmimi2 = newData.Cmimi * row.Koeficent;
                        newData.Cmimi2Tvsh = newData.Cmimi2 * (1 + row.Norme / 100);
                        break;
                    case "Cmimi2Tvsh":
                        newData.Cmimi2 = value / (1 + row.Norme / 100);
                        if (row.NjesiTeVarura) {
                            newData.Cmimi = newData.Cmimi2 / row.Koeficent;
                            newData.CmimiTvsh = newData.Cmimi * (1 + row.Norme / 100);
                        }
                        break;
                }
            },
            this.CellValueSasite = function (newData, value, row) {
                controllerContext.PricesDataGridSelectRow(row.IdCmimArtikulli);
                newData[this.dataField] = value;
                newData.Update = true;
                clearTimeout(app.view.timeout);
                switch (this.dataField) {
                    case "SasiMin":
                        if (value > row.SasiMax) {
                            newData.SasiMax = value;
                            app.view.timeout = setTimeout(function () { myMesazh.ShtoMesazhGabimi("Sasia Min nuk mund te jete me e madhe sesa sasia Max!"); }, 250);
                        }
                        break;
                    case "SasiMax":
                        if (value < row.SasiMin) {
                            newData.SasiMax = row.SasiMin;
                            app.view.timeout = setTimeout(function () { myMesazh.ShtoMesazhGabimi("Sasia Max nuk mund te jete me e vogel sesa sasia Min!"); }, 250);
                        }
                        break;
                }
            },
            this.CellValueDatat = function (newData, value, row) {
                controllerContext.PricesDataGridSelectRow(row.IdCmimArtikulli);
                newData[this.dataField] = value;
                newData.Update = true;
                switch (this.dataField) {
                    case "DateFillimi":
                        if (value > new Date(row.DateMbarimi)) {
                            newData.DateMbarimi = value;
                            app.view.timeout = setTimeout(function () { myMesazh.ShtoMesazhGabimi("Data e fillimit nuk mund te jete me e madhe se data e mbarimit!"); }, 250);
                        }
                        break;
                    case "DateMbarimi":
                        if (value < new Date(row.DateFillimi)) {
                            newData.DateMbarimi = row.DateFillimi;
                            app.view.timeout = setTimeout(function () { myMesazh.ShtoMesazhGabimi("Data e mbarimit nuk mund te jete me e vogel se data e fillimit!"); }, 250);
                        }
                        break;
                    case "KoheFillimi":
                        if (value > new Date(row.KoheMbarimi)) {
                            newData.KoheMbarimi = value;
                            app.view.timeout = setTimeout(function () { myMesazh.ShtoMesazhGabimi("Koha e fillimit nuk mund te jete me e madhe se koha e mbarimit!"); }, 250);
                        }
                        break;
                    case "KoheMbarimi":
                        if (value < new Date(row.KoheFillimi)) {
                            newData.KoheMbarimi = row.KoheFillimi;
                            app.view.timeout = setTimeout(function () { myMesazh.ShtoMesazhGabimi("Koha e mbarimit nuk mund te jete me e vogel se koha e fillimit!"); }, 250);
                        }
                        break;
                }
            },
            this.KonfiguroPriceDataGrid = function (dataGrid, columnsKonfig, filterDefault) {
                dataGrid.SetColumnsFromConfig(columnsKonfig); //percakton kolonat e grides
                dataGrid.AddCustomOptionToColumns(["Cmimi", "Cmimi2", "SasiMin", "SasiMax", "CmimiTvsh", "Cmimi2Tvsh"], "dataType", "number");
                dataGrid.AddCustomOptionToColumns(["Cmimi", "Cmimi2", "SasiMin", "SasiMax", "CmimiTvsh", "Cmimi2Tvsh"], "editorOptions", { showSpinButtons: true, min: 0 });
                dataGrid.AddCustomOptionToColumns(["DateFillimi", "DateMbarimi", "DtKrijimi", "DtModifikimi"], "dataType", "datetime");
                dataGrid.AddCustomOptionToColumns(["DateFillimi", "DateMbarimi", "DtKrijimi", "DtModifikimi"], "format", "dd/MM/yyyy");
                dataGrid.AddCustomOptionToColumns(["DateFillimi", "DateMbarimi", "DtKrijimi", "DtModifikimi"], "editorOptions", { type: "date", displayFormat: "dd/MM/yyyy", useMaskBehavior: true });
                dataGrid.AddCustomOptionToColumns(["KoheFillimi", "KoheMbarimi"], "dataType", "datetime");
                dataGrid.AddCustomOptionToColumns(["KoheFillimi", "KoheMbarimi"], "format", "HH:mm");   
                dataGrid.AddCustomOptionToColumns(["KoheFillimi", "KoheMbarimi"], "editorOptions", { type: "time", displayFormat: "HH:mm", pickerType: "rollers", useMaskBehavior: true });

                dataGrid.AddCustomOptionToColumns(["Cmimi", "Cmimi2", "CmimiTvsh", "Cmimi2Tvsh"], "setCellValue", controllerContext.CellValueCmimet);
                dataGrid.AddCustomOptionToColumns(["SasiMin", "SasiMax"], "setCellValue", controllerContext.CellValueSasite);
                dataGrid.AddCustomOptionToColumns(["DateFillimi", "DateMbarimi", "KoheFillimi", "KoheMbarimi"], "setCellValue", controllerContext.CellValueDatat);
                dataGrid.AddCustomOptionToColumns(["IdTvsh"], "setCellValue", controllerContext.CellValueIdTvsh);
                dataGrid.AddCustomOptionToColumns(["Kosto"], "visible", app.view.cbKosto.GetChecked());
                dataGrid.AddCustomOptionToColumns(["Gjendje"], "visible", app.view.cbGjendje.GetChecked());

                if (filterDefault)
                    dataGrid.AddCustomSortingToColumns(filterDefault.KoloneRenditje, "sortOrder");
            },
            this.CreatePricesDataGrid = function (columnsKonfig, filterDefault) {
                var dataGrid = new myDxDataGrid("PricesDataGrid", {
                    keyExpr: 'IdCmimArtikulli',
                    showRowLines: true,
                    scrolling: { mode: "none" },
                    paging: { pageSize: 20 },
                    pager: {visible : true, showInfo: true },
                    columnResizingMode: "nextColumn",
                    allowColumnReordering: true,
                    allowColumnResizing: true,
                    setColumnWidth: true,
                    editorColumnOption: { step: 1, showSpinButtons: true },
                    editing: { mode: "batch", allowUpdating: true },
                    selection: { selectAllMode: "allPages", mode: "multiple", showCheckBoxesMode: "always" },
                    filterRow: { visible: true, showAllText: "" },
                    loadPanel: { enabled: false },
                    onToolbarPreparing: controllerContext.OnGridToolbarPreparing,
                    onRowPrepared: controllerContext.OnGridRowPrepared,
                    onSelectionChanged: controllerContext.OnGridSelectionChanged,
                    onContentReady: controllerContext.OnGridContentReady
                });
                controllerContext.KonfiguroPriceDataGrid(dataGrid, columnsKonfig, filterDefault);          
                return dataGrid;
            }
        };

        function handlersController() {
            //private
            var controller = new CmimeController();
            var handlersContext = this;
            //publike
            this.init = function () {
                app.view.txtVlera.SetVisible(false);
                if (pageState.konfigFillestar !== null) {
                    app.view.lblKonfigurimiP.SetText(pageState.konfigFillestar.Pershkrimi);
                    app.view.cmbKonfigurimi.SetText(pageState.konfigFillestar.Kodi);
                    app.view.dteKoheFillimi2.SetDate(new Date(2016, 1, 1, 0, 0, 0));
                    app.view.dteKoheMbarimi2.SetDate(new Date(2016, 1, 1, 23, 59, 0));
                    controller.ndryshoKonfigurimin();
                    controller.changeName();
                }
            },
            this.cbKostoChecked = function (s, e) {
                if (app.view.cmbRritjeZbritje.GetText() == "Barazim" && !app.view.cmbRritjeZbritje.FindItemByText("Kosto"))
                    app.view.cmbVlerePerqindje.AddItem("Kosto", 0);
                controller.MerrGjendjeKosto();
            },
            this.cbGjendjeChecked = function (s, e) {
                controller.MerrGjendjeKosto();
            },
            this.cmbRritjeZbritjeValueChanged = function (s, e) {
                controller.ChangeCmbRritjeZbritje(s.GetText());
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
            this.endCallbackGrida = function (s, e) {
                Utils.hiqLoadingGif();
                if (typeof (s["cpShowPopUp"]) != "undefined") {

                    delete s["cpShowPopUp"];
                    return;
                }
            },
            //Fund evetet e grides
            this.popupUniversalCloseUp = function (s, e) {
                app.view.popupUniversal.SetContentUrl("");
                if (app.pageState.rifreskoGride) {//nuk rifreskohet grida nqs nuk jane bere ndryshime.
                    app.pageState.rifreskoGride = false;
                    var idNivelCmimi = app.view.cmbNivelCmimi.option("value");
                    if (idNivelCmimi != null)
                        controller.GetPriceDataGridDataSource(idNivelCmimi);
                }
            },

            this.menu_click = function (s, e) {
                switch (e.item.name) {
                    case "Ruaj":
                        if (app.view.PricesDataGrid.Grida.hasEditData()) {
                            app.view.PricesDataGrid.Grida.saveEditData();
                        }
                        var cmimeTeSelektuar = app.view.PricesDataGrid.Grida.getSelectedRowKeys();
                        var teNdryshuarPorTePaselektuar = app.view.PricesDataGrid.GetData().filter(function (item) { return item.Update == true && !cmimeTeSelektuar.includes(item.IdCmimArtikulli); });
                        if (teNdryshuarPorTePaselektuar.length > 0) {
                            var teNdryshuar = teNdryshuarPorTePaselektuar.slice(0, 4).map(function (item) { return item.KodArtikulli; }).join(',');
                            window.parent.myMesazh.ShtoMesazh({
                                type: "confirm",
                                UseCancelButton: true,
                                text: "Keni keta artikujt te ndryshuar por te pa selektuar: " + teNdryshuar + "...Doni te vazhdoni ?",
                                modal: true,
                                idGjuha: pageState.idGjuha,
                                okClick: function () { controller.Ruaj(); },
                                cancelClick: function () { return; }
                            });
                            return;
                        }
                        controller.Ruaj();
                        break;

                    case "Pastro":
                        //reset vlerat
                        controller.Pastro();
                        break;

                    case "Sinkronizo":
                        myButtonClickLupa.LupaUniversal_Click(app.pageState.headerPopUpTextZgjidhNdermarrjet, pageState.lupaNdermarrjeBijaURL, 500, 500);
                        break;
                    case "CmimDetajim":
                        if (app.view.PricesDataGrid.Grida.hasEditData() || app.view.PricesDataGrid.GetData().filter(function (item) { return item.Update == true; }).length > 0)
                            myMesazh.ShtoMesazhGabimi("Ju lutem ruani ndryshimet perpara se te hapni cmimet e detajuara");
                        else
                            myButtonClickLupa.LupaUniversal_Click("Shto Cmim me detajim", "CmimeArtikulli.aspx?lloji=" + Utils.getUrlVar('lloji') + "&lupe=true", 1400, 700);
                        break;
                    case "Anullo":
                        window.parent.popupUniversal.Hide();
                        break;
                    default:
                        break;
                }

                e.processOnServer = false;
            },
            this.getView = function () {
                return app.view;
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
            DevExpress.localization.locale(hfState.Get("idGjuha") == 0 ? "al" : "en");
            app.view = new View();
            app.pageState.initState();
            handlers.init();
        },
        handlers: handlers,
        pageState: app.pageState
    };

};
var cmimet = new CmimeApp();

$(document).on("ready", cmimet.initApp).on("keydown", cmimet.handlers.keyDown);