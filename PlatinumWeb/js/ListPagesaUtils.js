var ListPagesaUtils = {

    LlogaritPagenModel: function (response) {

        ///model i kthimit te te dhenave nga ws per llogaritjen e pages per cdo punonjes
        //keto fusha i permban cdo rresht i LP per punonjes
        this.Emri = response["Emri"],
            this.NrPersonal = response["NrPersonal"],
            this.ColKomponente = response["ColKomponente"],
            this.Tatimet = response["Tatimet"],
            this.Kursi = response["Kursi"],
            this.KursiNdermarrjes = response["KursiNdermarrjes"],
            this.Departamenti = response["Departamenti"],
            this.VleraParam = response["VleraParam"],
            this.Vlerat = response["Vlerat"],
            this.OrePuneNeDite = response["OrePuneNeDite"],
            this.NrKomponenteve = response["NrKomponenteve"],
            this.Kodet = response["Kodet"]; //this.ColpagaTeGjitha = response.ColpagaTeGjitha;
        return this;
    },
    krijoKompListpageseNgaKomponentePage: function (idKompListPagese, kompPage) {


        return {
            IdKompListPagese: idKompListPagese,
            IdTrupi: 0,
            IdKomponentePage: kompPage.IdKomponentePage,
            KodKomponente: kompPage.Kodi,
            Komponente: kompPage.Pershkrimi,
            LejoModVlere: kompPage.LejoModVlere,
            Modifikuar: false,
            Njesia: kompPage.Njesi,
            Tipi: kompPage.Tipi,
            VleraParam: undefined,
            Vlera: kompPage.Vlera,
            Shenime: "",
        };

    },
    krijoColKompListPagese: function (colKompPage) {
        var nrKomp = colKompPage.length;
        var colKompListPagese = new Array(nrKomp);
        var idKompPage = -1;
        for (var i = 0; i < nrKomp; i++) {
            colKompListPagese[i] = this.krijoKompListpageseNgaKomponentePage(idKompPage, colKompPage[i]);
            idKompPage--;
        }
        return colKompListPagese;
    },
    llogaritPagen: function (colFillestare, colKomp, arrKodi, arrvleraParam, vlerat, coltat, kursinderm, kursi, od) {
        var pt = 0;
        var formula = 0;
        var dite = 0;
        var ditepune = 0;
        var ditemuaji = 0;
        var idKompListPageseTeRejaCounter = -1;
        var rreshtTrupi = {
            txtPP: 0,
            txtPR: 0,
            txtOD: 0,
            txtDM: 0,
            txtPJ: 0,
            txtTP: 0,
            txtSP: 0,
            txtSN: 0,
            txtSPS: 0,
            txtSPD: 0,
            txtSNS: 0,
            txtSND: 0,
            txtST: 0,
            txtNT: 0,
            txtPT: 0,
            txtPPS: 0,
            txtPRN: 0,
            txtPaguar: 0,
            txtPagaShtesa: 0,
            txtCosto: 0
        };
        for (var i = 0; i < colKomp.length; i++) {
            formula = 0;
            var kompPage = colKomp[i];
            //arrvleraParam = vleraPerPunonjes.VleraParam;
            for (var h = 0; h < arrKodi.length; h++) {
                if (arrKodi[h] === kompPage.Kodi) {
                    if (typeof (colFillestare[h]) == "undefined") {
                        //shtoje ne list e komponenteve te LP nese eshte komponente e re
                        colFillestare[h] = this.krijoKompListpageseNgaKomponentePage(idKompListPageseTeRejaCounter, kompPage);
                        idKompListPageseTeRejaCounter--;
                    }

                    //nese eshte komponente formule vlereso formulen
                    if (kompPage.Formula !== "") {
                        //eshte komponente formule
                        try {
                            formula = parseFloat(eval(kompPage.Formula)); //.toFixed(10);
                            if (isNaN(formula) || formula < 0)
                                formula = 0;
                        } catch (ee) {
                            console.error("Kjo formule nuk mund te vlersohet! " + kompPage.Formula);
                            formula = 0;
                            console.log(ee);
                        }
                        try {
                            if (kompPage.ParamKodi.search("\-") !== -1) {
                                kompPage.ParamKodi = parseFloat(eval(kompPage.ParamKodi)); //.toFixed(10);
                                arrvleraParam[h] = kompPage.ParamKodi;
                                colFillestare[h].VleraParam = arrvleraParam[h];
                            }
                        } catch (ec) {
                            console.log(ec);
                        }

                        colFillestare[h].VleraParam = arrvleraParam[h];

                        //if (kompPage.Kodi === "TP") { //tatimet
                        //    pt = parseFloat(formula).toFixed(4);
                        //    for (var k = 0, lengthTatimet = coltat.length; k < lengthTatimet; k++) {
                        //        //nqs paga per tatim eshte midis kufirit min dhe max marrim kete perqindje tatimi
                        //        if (coltat[k].Min / (kursinderm.VleraKursi) <= pt && coltat[k].Max / (kursinderm.VleraKursi) >= pt) {
                        //            //menyra totale
                        //            if (coltat[k].Menyra === 1) {
                        //                formula = (pt * coltat[k].Norma / 100);
                        //            }
                        //            else {
                        //                //menyra progesive
                        //                var tatimi = (pt - (coltat[k].Min - 1) / (kursinderm.VleraKursi)) * coltat[k].Norma / 100;
                        //                if (coltat[k].Norma == 0) pt = coltat[k].Min;
                        //                for (var j = k - 1; j >= 0; j--) {
                        //                    //  if (coltat[j].Norma == 0) //nestila u hoq se ekonomistet thone qe duhet te dali vetem paga per nivelin e fundit te tatimit
                        //                    //   pt = pt - (coltat[j].Max - ((j - 1) < 0 ? coltat[j].Min : coltat[j - 1].Max));
                        //                    //tatimi += coltat[j].Max / (kursinderm.VleraKursi) * coltat[j].Norma / 100;
                        //                    tatimi = tatimi + parseFloat((coltat[j].Max - coltat[j].Min + 1) / (kursinderm.VleraKursi) * coltat[j].Norma / 100);
                        //                }
                        //                formula = tatimi;
                        //            }
                        //            break;
                        //        }
                        //    }
                        //}
                        kompPage.Formula = formula;
                    } else {
                        //eshte komponente vlere
                        formula = vlerat[h];
                    }

                    var formulaFloat = parseFloat(formula);
                    if (kompPage.Tipi === 1) {
                        rreshtTrupi.txtPaguar = rreshtTrupi.txtPaguar + formulaFloat;
                        rreshtTrupi.txtCosto = rreshtTrupi.txtCosto + formulaFloat;
                        rreshtTrupi.txtPagaShtesa = rreshtTrupi.txtPagaShtesa + formulaFloat;
                    }
                    if (kompPage.Kodi == "SN" || kompPage.Kodi == "COMPPENS")
                        rreshtTrupi.txtCosto = rreshtTrupi.txtCosto + formulaFloat;
                    else if (kompPage.Kodi == "SICKLEAVES" || kompPage.Kodi == "UNPAIDLEAVES") {
                        rreshtTrupi.txtPagaShtesa = rreshtTrupi.txtPagaShtesa - formulaFloat;
                        rreshtTrupi.txtCosto = rreshtTrupi.txtCosto - formulaFloat;
                    }
                    if (kompPage.Tipi === 2)
                        rreshtTrupi.txtPaguar -= formulaFloat;

                    colFillestare[h].Vlera = formula;

                    var vleraParam = parseFloat(arrvleraParam[h]);
                    switch ((kompPage.Kodi)) {
                        case "PP":
                            rreshtTrupi.txtPP = formula;
                            ditepune = vleraParam / od;
                            dite = dite + vleraParam / od;
                            break;
                        case "PR":
                            rreshtTrupi.txtPR = formula;
                            dite = dite + vleraParam;
                            break;
                        case "OD":
                            rreshtTrupi.txtOD = formula;
                            break;
                        case "DM":
                            ditemuaji = formula;
                            rreshtTrupi.txtDM = ditepune;
                            dite -= formula;
                            break;
                        case "PJ":
                            rreshtTrupi.txtPJ = formula;
                            break;
                        case "TP":
                            rreshtTrupi.txtTP = formula;
                            break;
                        case "SP":
                            rreshtTrupi.txtSP = formula;
                            break;
                        case "SN":
                            rreshtTrupi.txtSN = formula;
                            break;
                        case "SPS":
                            rreshtTrupi.txtSPS = formula;
                            break;
                        case "SPD":
                            rreshtTrupi.txtSPD = formula;
                            break;
                        case "SNS":
                            rreshtTrupi.txtSNS = formula;
                            break;
                        case "SND":
                            rreshtTrupi.txtSND = formula;
                            break;
                        case "ST":
                            rreshtTrupi.txtST = formula;
                            break;
                        case "NT":
                            rreshtTrupi.txtNT = formula;
                            break;
                        case "PT":
                            rreshtTrupi.txtPT = pt;
                            break;
                        case "PPS":
                            rreshtTrupi.txtPPS = formula;
                            break;
                        case "PRN":
                            rreshtTrupi.txtPRN = formula;
                            dite = dite + vleraParam;
                            break;
                        case "DLK":
                            //dite += formulaFloat;
                            dite = dite + formulaFloat;
                            break;
                        default:
                            break;
                    }
                    break;
                }
            }
        }
        return {
            colKomponentePerPunonjes: colFillestare,
            rreshtTrupi: rreshtTrupi,
            dite: dite,
            ditemuaji: ditemuaji,
            ditepune: ditepune
        };
    },

    shfaqMesazhGabimiNgaLLogaritjaEPages: function (mesazhet) {
        var keys = Object.keys(mesazhet);
        if (keys.length === 0)
            return;
        //ka punonjes me probleme
        var punonjesitMeProbleme = "Te dhenat per punonjesit :";

        for (var prop in keys) {
            if (keys.hasOwnProperty(prop)) {
                var nrPersonal=keys[prop];
                myMesazh.ShtoMesazhGabimi('Punonjesi :'+nrPersonal +'; Problemi : '+mesazhet[nrPersonal]);
                punonjesitMeProbleme += (nrPersonal + ";");
                console.log(JSON.stringify(mesazhet));
            }
        }
        //punonjesitMeProbleme += " nuk u llogariten sepse kane probleme,kontrolloni te dhenat e tyre!";
        //myMesazh.ShtoMesazhGabimi(punonjesitMeProbleme);

    },

    LlogaritGrupinPerKeteRresht: function (grida, rowIndex, kolona) {
        var firstGroupIndex = grida.GetTopVisibleIndex();
        var lastGroupIndex = grida.pageRowCount - 1;
        var groupIndex;
        for (var i = firstGroupIndex; i < lastGroupIndex; ++i) {
            if (grida.IsGroupRow(i)) {
                if (i < firstGroupIndex)
                    continue;
                if (i < rowIndex)
                    groupIndex = i;
                else {
                    break;
                }
            }
        }
        this.LlogaritShumenEGrupin(grida, groupIndex, kolona);
    },

    LlogaritShumenPerGrupin: function (grid, firstGroupIndex, shifraPasPresjes, kolona) {

        var lastGroupIndex = grid.pageRowCount;
        var total = 0;
        var tipi;
        var niveliGrupit = grid.GetGroupLevel(firstGroupIndex);
        for (var r = firstGroupIndex + 1; r < lastGroupIndex; r++) {
            if (grid.IsGroupRow(r) && grid.GetGroupLevel(r) == niveliGrupit) 
                break;
            

            if (hfState.Get("eshteGrupuarSipasTipit") == true)
                tipi = grid["cpTipi_" + grid.batchEditApi.GetCellValue(r, "KodKomponente")];
            else
                tipi = grid.batchEditApi.GetCellValue(r, "Tipi");
            tipi = parseInt(tipi);
            //llogarit vetem nese tipi 1,2
            var vleraNeGrid = grid.batchEditApi.GetCellValue(r, kolona);

            if (vleraNeGrid == undefined || vleraNeGrid == null) vleraNeGrid = 0;

            else vleraNeGrid = parseFloat(vleraNeGrid.toString());

            switch (tipi) {
                case 1:
                    total += vleraNeGrid;
                    break;
                case 2:
                    total -= vleraNeGrid;
                    break;
                default:
                    break;
            }
        }
        var lblGroupTotal = Utils.ktheKontroll("group_" + kolona + firstGroupIndex);
        // lblGroupTotal.SetValue(Utils.FormatoNumberMePresje(total));// Lori thote hiqe formatin
        var totali = Utils.formatoPresje(total, shifraPasPresjes);
        lblGroupTotal.SetValue(totali);
    },

    RillogaritTotaletPerGrup: function (grid, shifraPasPresjes, kolona) {
        ///llogarit gjithe grouptotalet per kolonene e dhene
        var firstGroupIndex = grid.GetTopVisibleIndex();
        var lastGroupIndex = grid.pageRowCount;

        for (var i = firstGroupIndex; i < lastGroupIndex; i++) {
            if (grid.IsGroupRow(i) && grid.IsGroupRowExpanded(i))
                this.LlogaritShumenPerGrupin(grid, i, shifraPasPresjes, kolona);
        }
    },
    KrijoRreshtMeVleraSipasKompListpagese: function (colFillestare) {
        var rreshtTrupi = {
            txtPP: '', txtPR: '', txtOD: '', txtDM: '', txtPJ: '', txtTP: '', txtSP: '', txtSN: '', txtSPS: '', txtSPD: '', txtSNS: '', txtSND: '', txtST: '', txtNT: '', txtPT: '', txtPPS: '', txtPRN: '', txtPagaShtesa: ''
        };
        var ditepune = 0;
        for (var j = 0; j < colFillestare.length; j++) {
            if (colFillestare[j].Tipi == 1)
                rreshtTrupi.txtPagaShtesa += parseFloat(colFillestare[j].Vlera);
            if (colFillestare[j].KodKomponente == "SICKLEAVES" || colFillestare[j].KodKomponente == "UNPAIDLEAVES")
                rreshtTrupi.txtPagaShtesa = rreshtTrupi.txtPagaShtesa - parseFloat(colFillestare[j].Vlera);
            switch ((colFillestare[j].KodKomponente)) {
                case 'PP':
                    rreshtTrupi.txtPP = colFillestare[j].Vlera;
                    ditepune = colFillestare[j].VleraParam;
                    break;
                case 'PR':
                    rreshtTrupi.txtPR = colFillestare[j].Vlera;
                    break;
                case 'OD':
                    rreshtTrupi.txtOD = colFillestare[j].Vlera;
                    break;
                case 'DM':
                    rreshtTrupi.txtDM = ditepune;//colFillestare[j].Vlera;
                    break;
                case 'PJ':
                    rreshtTrupi.txtPJ = colFillestare[j].Vlera;
                    break;
                case 'TP':
                    rreshtTrupi.txtTP = colFillestare[j].Vlera;
                    break;
                case 'SP':
                    rreshtTrupi.txtSP = colFillestare[j].Vlera;
                    break;
                case 'SN':
                    rreshtTrupi.txtSN = colFillestare[j].Vlera;
                    break;
                case 'SPS':
                    rreshtTrupi.txtSPS = colFillestare[j].Vlera;
                    break;
                case 'SPD':
                    rreshtTrupi.txtSPD = colFillestare[j].Vlera;
                    break;
                case 'SNS':
                    rreshtTrupi.txtSNS = colFillestare[j].Vlera;
                    break;
                case 'SND':
                    rreshtTrupi.txtSND = colFillestare[j].Vlera;
                    break;
                case 'ST':
                    rreshtTrupi.txtST = colFillestare[j].Vlera;
                    break;
                case 'NT':
                    rreshtTrupi.txtNT = colFillestare[j].Vlera;
                    break;
                case 'PT':
                    rreshtTrupi.txtPT = colFillestare[j].Vlera;
                    break;
                case 'PPS':
                    rreshtTrupi.txtPPS = colFillestare[j].Vlera;
                    break;
                case 'PRN':
                    rreshtTrupi.txtPRN = colFillestare[j].Vlera;
                    break;
                default: break;
            }
            rreshtTrupi.txtDM = ditepune;
        }
        return rreshtTrupi;
    }
};