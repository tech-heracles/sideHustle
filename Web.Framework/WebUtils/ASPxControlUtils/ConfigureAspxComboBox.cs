using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Web;
using System.Web.SessionState;
using System.Web.UI.WebControls;
using DbCore;
using DbCore.DbAdmin;
using DbCore.DbArkaBanka;
using DbCore.DbAsete;
using DbCore.DbBuxheti;
using DbCore.DbInventari;
using DbCore.DbKontabiliteti;
using DbCore.DbListPagesat;
using DbCore.DbQendraKosto;
using DbCore.DbRegjistrim;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Messages;
using DevExpress.Web;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using DbCore.DbProdhimi;
using DbCore.DbShare;
using NLog;
using DbCore.DbGIS;
using DbCore.DbImporte;
using DbCore.IMBUtils.Logging;

namespace PlatinumWeb.ApplicationUtils.ASPxControlUtils
{
    public static class ConfigureAspxComboBox
    {
        #region Mbushja e Combobox

        public static void KonfiguroComboBoxKonfigurimeshSipasKategorise(ASPxComboBox comboBox, bool modifikuar, int idPerdoruesi, int idNdermarrja, int idGjuha, int idKategoria)
        {
            ShtoKolonaPerKonfigurimet(comboBox);
            comboBox.SelectedIndex = 0;

            comboBox.ConfigureAndFill(() =>
            {
                var colKonfig = new colKonfigurimAmbjenti();
                colKonfig.mbushKonfigAmbjSipasIdKategori(idKategoria, idNdermarrja, idPerdoruesi, idGjuha);


                var konfVarura = new colKonfigurimAmbjenti();
                konfVarura.AddRange(colKonfig
                    .Select(konfi => new
                    {
                        konfi,
                        alternativa = clsAlternativaKushti.getAlternativa(konfi.IdKonfigAmbjente, "V")
                    })
                    .Where(t => t.alternativa == "Po" && !modifikuar)
                    .Select(t => t.konfi));

                foreach (var konfi in konfVarura)
                {
                    colKonfig.Remove(konfi);
                }

                return colKonfig;
            }, "KodKonfigAmbjente", "IdKonfigAmbjente");
        }

        public static void KonfiguroComboBoxKonfigurimeshSipasKategorise(ASPxComboBox comboBox, int idPerdoruesi, int idNdermarrja, int idGjuha, int idKategoria, bool meKolona)
        {
            if (meKolona)
                ShtoKolonaPerKonfigurimet(comboBox);

            comboBox.SelectedIndex = 0;
            comboBox.ConfigureAndFill(() =>
            {
                var col = new colKonfigurimAmbjenti();

                if (idKategoria != 0)
                    col.mbushKonfigAmbjSipasIdKategori(idKategoria, idNdermarrja, idPerdoruesi, idGjuha);
                else
                    col.mbushGjitheKonfigurimeAmbjentesh(idNdermarrja, idPerdoruesi, 1, idGjuha);

                return col;
            }, "KodKonfigAmbjente", "IdKonfigAmbjente");
        }

        public static void KonfiguroComboBoxKonfigurimeshSipasKategoriseDheNivelit(ASPxComboBox comboBox, object llojiValue, int idKategori, bool mod, int idGjuha, int idNdermarje, int idPerdoruesi)
        {
            ShtoKolonaPerKonfigurimet(comboBox);
            comboBox.ConfigureAndFill(() =>
            {
                var colKonfig = new colKonfigurimAmbjenti();
                if (llojiValue != null)
                    colKonfig.mbushKonfigAmbjSipasIdKategoriIdNivelMeLloj(idKategori, Int32.Parse(llojiValue.ToString()), idPerdoruesi);
                else
                    colKonfig.mbushKonfigAmbjSipasIdKategori(idKategori, idNdermarje, idPerdoruesi, idGjuha);

                var konfVarura = new colKonfigurimAmbjenti();
                konfVarura.AddRange(colKonfig
                    .Select(konfi => new
                    {
                        konfi,
                        alternativa = clsAlternativaKushti.getAlternativa(konfi.IdKonfigAmbjente, "V")
                    })
                    .Where(t => t.alternativa == "Po" && !mod)
                    .Select(t => t.konfi));

                foreach (var konfi in konfVarura)
                {
                    colKonfig.Remove(konfi);
                }

                return colKonfig;
            }, "KodKonfigAmbjente", "IdKonfigAmbjente");

            comboBox.SelectedIndex = 0;
        }

        public static void KonfiguroComboBoxKonfigurimeshSipasKategoriseDheNivelit(ASPxComboBox comboBox, int idPerdoruesi, int idNdermarrja, int idGjuha, int idKategoria, string kodNiveli)
        {
            ShtoKolonaPerKonfigurimet(comboBox);
            comboBox.SelectedIndex = 0;
            comboBox.ConfigureAndFill(() =>
            {
                var col = new colKonfigurimAmbjenti();

                if (idKategoria != 0)
                    col.mbushKonfigAmbjSipasIdKategoriIdNivel(idKategoria, clsNivelRegjistrimi.ktheIdNivelRegjistrimiSipasKodi(kodNiveli, idNdermarrja), idPerdoruesi, idGjuha, true);
                else
                    col.mbushGjitheKonfigurimeAmbjentesh(idNdermarrja, idPerdoruesi, 1, idGjuha);

                return col;
            }, "KodKonfigAmbjente", "IdKonfigAmbjente");
        }

        public static void KonfiguroComboBoxKonfigurimeshSipasKategoriseDheNivelitMeLloj(ASPxComboBox comboBox, int idPerdoruesi, int idNdermarrja, int idGjuha, int idKategoria, string kodNiveli)
        {
            ShtoKolonaPerKonfigurimet(comboBox);
            comboBox.SelectedIndex = 0;
            comboBox.ConfigureAndFill(() =>
            {
                var col = new colKonfigurimAmbjenti();

                if (idKategoria != 0)
                    col.mbushKonfigAmbjSipasIdKategoriIdNivelMeLloj(idKategoria, clsNivelRegjistrimi.ktheIdNivelRegjistrimiSipasKodi(kodNiveli, idNdermarrja), idPerdoruesi);
                else
                    col.mbushGjitheKonfigurimeAmbjentesh(idNdermarrja, idPerdoruesi, 1, idGjuha);

                return col;
            }, "KodKonfigAmbjente", "IdKonfigAmbjente");
        }

        public static void KonfiguroComboBoxNivelesh(ASPxComboBox comboBox, int idkategoria, int idNdermarrja, int idPerdoruesi, bool shtoRreshtBosh)
        {
            comboBox.ConfigureAndFill(() => colNivelRegjistrimi.ktheGjitheNivelRegjistrimiSipasKategoriDtCombo(idkategoria, idNdermarrja, idPerdoruesi, shtoRreshtBosh), "Kodi", "IdNivel");
            comboBox.SelectedIndex = 0;
        }

        public static void KonfiguroComboBoxLlogaria(ASPxComboBox comboBox, int idPerdoruesi, int idNdermarrja, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(e?.Filter)) return;

            comboBox.ConfigureAndFill(() =>
            {
                var dt = colLlogarite.merrSipasLlogariteNdermarrjesAndAutorizimeAktivDT(idNdermarrja, idPerdoruesi);
                return dt.Select($"NrLlogari like '%{e.Filter}%' or EmerLlogari1 like '%{e.Filter}%' or EmerLlogari2 like '%{e.Filter}%'")
                    .AsEnumerable()
                    .Skip(e.BeginIndex)
                    .Take(e.EndIndex - e.BeginIndex + 1)
                    .GetDataTable(dt);
            }, "NrLlogari", "IdLlogari");
        }

        public static void KonfiguroComboBoxLlogaria(ASPxComboBox comboBox, int idPerdoruesi, int idNdermarrja, ListEditItemRequestedByValueEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(e?.Value?.ToString())) return;

            comboBox.ConfigureAndFill(() =>
            {
                var dt = colLlogarite.merrSipasLlogariteNdermarrjesAndAutorizimeAktivDT(idNdermarrja, idPerdoruesi);
                return dt.Select($"IdLlogari = {e.Value}").GetDataTable(dt);
            }, "NrLlogari", "IdLlogari");
        }

        public static void KonfiguroComboBoxKushteDergimi(ASPxComboBox comboBox, int idNdermarrja) =>
            comboBox.ConfigureAndFill(() => new colKushteDergimi(idNdermarrja), "KodiKushtDergimi", "IdKushtDergimi");

        public static void KonfiguroComboBoxMenyraTransporti(ASPxComboBox comboBox, int idNdermarrja) =>
            comboBox.ConfigureAndFill(() => new colMenyraTransporti(idNdermarrja), "KodiMenyreTransporti", "IdMenyreTransporti");

        public static void KonfiguroComboBoxKlientFurnitor(string filter, long startIndex, long endIndex,
            int idPerdorues, int idNdermarrje, ASPxComboBox comboBox, int kf, int kfkryesor = 0)
        {
            comboBox.ConfigureAndFill(() =>
            {
                switch (kf)
                {
                    case 0:
                        return colKlienteFurnitore.MbushKlienteOseFurnitore(filter, startIndex, endIndex, idNdermarrje,
                            idPerdorues, kfkryesor);
                    case 1:
                        return colKlienteFurnitore.MbushKlienteOseFurnitore(filter, startIndex, endIndex, true,
                            idNdermarrje, idPerdorues, kfkryesor);
                    case 2:
                        return colKlienteFurnitore.MbushKlienteOseFurnitore(filter, startIndex, endIndex, false,
                            idNdermarrje, idPerdorues, kfkryesor);
                    default:
                        return new DataTable();
                }
            }, "KodKlientFurnitor", "IdKlientFurnitor");
        }

        public static void KonfiguroComboBoxKlientFurnitor(string filter, long startIndex, long endIndex, ASPxComboBox comboBox, string kodModeli, int idPerdoruesi, int idNdermarrje, int idGjuha, int kfkryesor)
        {
            var konfigurimi = new clsKonfigurimAmbjenti();
            konfigurimi.mbushKonfiguriminMeKod(kodModeli, idNdermarrje, idGjuha);

            var alternativa = clsAlternativaKushti.getAlternativa(konfigurimi.IdKonfigAmbjente, "KF");
            if (alternativa == "Klient")
                KonfiguroComboBoxKlientFurnitor(filter, startIndex, endIndex, idPerdoruesi, idNdermarrje, comboBox, 1, kfkryesor);
            else if (alternativa == "Furnitor")
                KonfiguroComboBoxKlientFurnitor(filter, startIndex, endIndex, idPerdoruesi, idNdermarrje, comboBox, 2, kfkryesor);
            else
                KonfiguroComboBoxKlientFurnitor(filter, startIndex, endIndex, idPerdoruesi, idNdermarrje, comboBox, 0, kfkryesor);
        }

        public static void KonfiguroComboBoxNiveleCmimeshPrind(ASPxComboBox comboBox, int idNdermarrja, object vlera) =>
            comboBox.ConfigureAndFill(() =>
            {
                var niveleCmimesh = new colNiveleCmimesh();
                niveleCmimesh.mbushGjitheNiveleCmimeshPrindiMeMonedheSipasNdermarjes(idNdermarrja);
                return niveleCmimesh.Where(x => x.IdNivelCmimi == Convert.ToInt32(vlera));
            }, "PershkrimNivelCmimi", "IdNivelCmimi");

        public static void KonfiguroComboBoxNiveleCmimeshPrind(int idNdermarrja, ASPxComboBox comboBox, string filter, int beginIndex, int endIndex) =>
            comboBox.ConfigureAndFill(() =>
            {
                var niveleCmimesh = new colNiveleCmimesh();
                niveleCmimesh.mbushGjitheNiveleCmimeshPrindiMeMonedheSipasNdermarjes(idNdermarrja);
                return niveleCmimesh.Where(x => x.PershkrimNivelCmimi
                                                 .IndexOf(filter, StringComparison.InvariantCultureIgnoreCase) > -1)
                                                 .Skip(beginIndex)
                                                 .Take(endIndex - beginIndex + 1);
            }, "PershkrimNivelCmimi", "IdNivelCmimi");

        public static void KonfiguroComboBoxTaksat(int idPerdoruesi, int idNdermarrje, ASPxComboBox comboBox, LlojTakse lloj, bool kodi, bool nivelPaTvsh = true, bool bosh = false)
        {
            comboBox.Columns.Add(new ListBoxColumn
            {
                FieldName = "KodTaksa",
                Caption = MessagesResource.Messages["lblKodi"]
            });
            comboBox.Columns.Add(new ListBoxColumn
            {
                FieldName = "NormaPerqindje",
                Caption = MessagesResource.Messages["labelRaportNorma"]
            });

            comboBox.TextFormatString = kodi ? "{0}" : "{1}";

            comboBox.ConfigureAndFill(() =>
            {
                var col = new colTaksa(idNdermarrje, lloj, idPerdoruesi);

                if (nivelPaTvsh)
                    col.Insert(0, clsTaksa.krijoTaksePaTVSH());

                if (bosh)
                    col.Insert(0, new clsTaksa());

                for (var i = 1; i < col.Count; i++)
                    col[i].NormaPerqindje = Convert.ToDecimal($"{col[i].NormaPerqindje:0.##}");

                return col.Select(x => new
                {
                    x.KodTaksa,
                    NormaPerqindje = x.KodTaksa == null ? "" : x.NormaPerqindje.ToString(CultureInfo.InvariantCulture),
                    x.IdTaksa
                });
            }, "KodTaksa", "IdTaksa");
        }

        public static void KonfiguroComboBoxPrioriteti(ASPxComboBox comboBox)
        {
            for (var i = 1; i <= 20; i++)
                comboBox.Items.Add($"{i}", i);
            comboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        }

        public static void KonfiguroComboBoxLlojAdresa(ASPxComboBox comboBox) =>
            comboBox.ConfigureAndFill(() =>
            {
                var colTipet = new colTipeAdresash();
                colTipet.MbushGjitheTipetAdresave();
                return colTipet;
            }, "PershkrimTipAdrese", "IdTipAdrese");

        public static void KonfiguroComboBoxTitulliKlientFurnitor(ASPxComboBox comboBox)
        {
            comboBox.Items.Add(MessagesResource.Messages["lblComboKompani"], (int)TitulliKlientFurnitor.Kompani);
            comboBox.Items.Add(MessagesResource.Messages["lblComboPersonFizik"], (int)TitulliKlientFurnitor.PersonFizik);
            comboBox.Items.Add(MessagesResource.Messages["lblComboKlientRastesishem"], (int)TitulliKlientFurnitor.KlientRastesishem);
            comboBox.Items.Add(MessagesResource.Messages["lblComboInstitucionBuxhetor"], (int)TitulliKlientFurnitor.InstitucionBuxhetor);
            comboBox.Items.Add(MessagesResource.Messages["lblComboSHPK"], (int)TitulliKlientFurnitor.SHPK);

            comboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        }

        public static void KonfiguroComboBoxKategoriZbritje(int idNdermarrje, ASPxComboBox comboBox) =>
            comboBox.ConfigureAndFill(() => colKokatKategoriteZbritjes.merrSipasKategoriteNdermarrjesDT(idNdermarrje, 0), "KodKategoriZbritje", "IdKokaKategoriZbritje");

        public static void KonfiguroComboBoxComboQytete(int idNdermarrje, ASPxComboBox comboBox, bool vendosQytetBosh = true) =>
            comboBox.ConfigureAndFill(() =>
            {
                var colQyt = new colQytetet();
                colQyt.mbushGjitheQytetetPozitive(idNdermarrje);

                if (vendosQytetBosh)
                    colQyt.Insert(0, new clsQyteti(0, "", "", 0, 0, 0));

                return colQyt;
            }, "EmriQyteti", "IdQyteti");

        public static void KonfiguroComboBoxMaturimi(int idNdermarrje, ASPxComboBox comboBox) =>
            comboBox.ConfigureAndFill(() =>
            {
                var colMaturimet = new colMaturimet(idNdermarrje);
                if (!comboBox.ClientInstanceName.StartsWith("txtVlera"))
                    colMaturimet.Insert(0, new clsMaturimi());
                return colMaturimet;
            }, "KodMaturimi", "IdMaturimi");

        public static void KonfiguroComboBoxLlojPorosie(ASPxComboBox comboBox)
        {
            comboBox.Items.Add(LlojPorosie.Aparate.ToString(), Convert.ToInt32(LlojPorosie.Aparate));
            comboBox.Items.Add(LlojPorosie.Karta.ToString(), Convert.ToInt32(LlojPorosie.Karta));
            comboBox.Items.Add(LlojPorosie.Loan.ToString(), Convert.ToInt32(LlojPorosie.Loan));
            comboBox.Items.Add(LlojPorosie.Dhurate.ToString(), Convert.ToInt32(LlojPorosie.Dhurate));
            comboBox.Items.Add(MessagesResource.Messages["cmbTeGjitha"], Convert.ToInt32(LlojPorosie.TeGjitha));
            comboBox.Items.Add(MessagesResource.Messages["cmbAparateEkspozitore"], Convert.ToInt32(LlojPorosie.Aparate_ekspozitore));

            comboBox.SelectedIndex = 0;
            comboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        }
        public static void KonfiguroComboBoxNivelVerbosity(ASPxComboBox comboBox)
        {
            comboBox.Items.Add("", -1);
            foreach (var item in LogLevel.AllLoggingLevels)
            {
                comboBox.Items.Add(item.Name, item.Ordinal);
            }
            comboBox.SelectedIndex = LogLevel.Error.Ordinal + 1;
            comboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        }
        public static void KonfiguroComboBoxLlojLogesh(ASPxComboBox comboBox)
        {
            comboBox.Items.Add("", 0);
            comboBox.Items.Add("buxhetimi", 1);
            comboBox.Items.Add("Rivleresimet", 2);
            comboBox.Items.Add("shitje", 3);
            comboBox.Items.Add("importi", 4);
            comboBox.Items.Add("brm", 5);
            comboBox.Items.Add("promocionet", 6);
            comboBox.Items.Add("WebApi", 7);
            comboBox.Items.Add("Traces", 8);
            comboBox.Items.Add("mbylljePeriudhe", 9);
            comboBox.Items.Add("Te Pergjithshme", 10);

            comboBox.SelectedIndex = 10;
            comboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        }
        public static void KonfiguroComboBoxMetoda(ASPxComboBox combo, bool meOpsionBosh, bool meOpsionNull = false)
        {
            if (meOpsionBosh)
                combo.Items.Add("", -1);

            if (meOpsionNull)
                combo.Items.Add("", null);

            combo.Items.Add(MenyrePagese.Me_mirebesim.ToString().Replace('_', ' '), Convert.ToInt32(MenyrePagese.Me_mirebesim));
            combo.Items.Add(MenyrePagese.Pagese.ToString(), Convert.ToInt32(MenyrePagese.Pagese));
            combo.Items.Add(MenyrePagese.Pagese_Automatike.ToString().Replace('_', ' '), Convert.ToInt32(MenyrePagese.Pagese_Automatike));
            combo.Items.Add(MenyrePagese.Me_parapagim.ToString().Replace('_', ' '), Convert.ToInt32(MenyrePagese.Me_parapagim));
            combo.Items.Add(MenyrePagese.Arke.ToString(), Convert.ToInt32(MenyrePagese.Arke));
            combo.Items.Add(MenyrePagese.Karte_krediti.ToString().Replace('_', ' '), Convert.ToInt32(MenyrePagese.Karte_krediti));
            combo.Items.Add(MenyrePagese.Pezull.ToString(), Convert.ToInt32(MenyrePagese.Pezull));
            combo.Items.Add(MenyrePagese.Banke.ToString(), Convert.ToInt32(MenyrePagese.Banke));

            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        }

        public static void KonfiguroComboBoxNiveleZbritjePrind(int idNdermarrje, ASPxComboBox combo)
        {
            combo.Columns.Clear();
            combo.Columns.Add(new ListBoxColumn { FieldName = "KodNivelZbritje" });
            combo.Columns.Add(new ListBoxColumn { FieldName = "PershkrimNivelZbritje" });

            combo.TextFormatString = "{0},{1}";

            combo.ConfigureAndFill(() =>
            {
                var colNiveleZbritjesh = new colNiveleZbritjesh();
                colNiveleZbritjesh.mbushGjitheNiveleZbritjeshPrindiSipasNdermarjes(idNdermarrje);
                return colNiveleZbritjesh;
            }, "KodNivelZbritje", "IdNivelZbritje");
        }

        public static void KonfiguroComboBoxObjektivaKosto(ASPxComboBox combo, int idndermarje) =>
            combo.ConfigureAndFill(() =>
            {
                var col = new colObjektivaKosto();
                col.mbushGjitheObjketivatKostoSipasNdermarjesAktiv(idndermarje);
                return col;
            }, "Kodi", "Id");

        public static void KonfiguroComboBoxAgjentesh(int idNdermarrja, int idPerdoruesi = 0, params ASPxComboBox[] combot)
        {
            for (int i = 0; i < combot.Length; i++)
            {
                var combo = combot[i];
                combo.ConfigureAndFill(() => idPerdoruesi == 0
                            ? new colAgjenteShitje(idNdermarrja)
                            : new colAgjenteShitje(idNdermarrja, idPerdoruesi), "KodiAgjentShitje", "IdAgjentShitje");
            }
        }

        public static void KonfiguroComboBoxGrupKf(int idNdermarrje, ASPxComboBox combo, int grupi, bool llojikf) =>
            combo.ConfigureAndFill(() =>
            {
                var grupe = new colGrupeKF();
                grupe.ShtoObjektBosh();
                grupe.MbushGjitheGrupetKfSipasNdermarrjesJoPrindDheLlojit(idNdermarrje, grupi, llojikf);
                return grupe;
            }, "KodGrupi", "IdGrupi");

        public static void KonfiguroComboBoxGrupKf(int idNdermarrje, ASPxComboBox combo, int grupi) =>
            combo.ConfigureAndFill(() =>
            {
                var grupe = new colGrupeKF();
                grupe.MbushGjitheGrupetKfSipasNdermarrjesJoPrind(idNdermarrje, grupi);
                return grupe;
            }, "KodGrupi", "IdGrupi");

        public static void KonfiguroComboBoxMenyreTransporti(int idNdermarrje, ASPxComboBox combo) =>
            combo.ConfigureAndFill(() => new colMenyraTransporti(idNdermarrje), "KodiMenyreTransporti", "IdMenyreTransporti");

        public static void KonfiguroComboBoxKushteDergimi(int idNdermarrje, ASPxComboBox combo) =>
            combo.ConfigureAndFill(() => new colKushteDergimi(idNdermarrje), "KodiKushtDergimi", "IdKushtDergimi");

        public static void KonfiguroComboBoxNdermarjeBij(int idNdermarrje, ASPxComboBox combo) =>
            combo.ConfigureAndFill(() => colNdermarrjet.ktheNdermarjeBijSipasMemeDheOwnDT(idNdermarrje), "NdermarrjeKodi", "IdNdermarrje");

        public static void KonfiguroComboBoxKushtPagese(int idNdermarrje, ASPxComboBox combo) =>
            combo.ConfigureAndFill(() => new colKushtPageseKoka(idNdermarrje), "KodiKushtPagese", "IdKoka");

        public static void KonfiguroComboBoxBankatSipasFiltrit(ASPxComboBox combo, int idPerdoruesi, int idNdermarrje, int idmonedhaklienti, object vlera)
        {
            if (String.IsNullOrWhiteSpace((string)vlera))
                return;

            combo.ConfigureAndFill(() =>
            {
                var dt = idmonedhaklienti == 0
                    ? colBankat.merrSipasABNdermarrjesAndAutorizimeDTLupa(idNdermarrje, idPerdoruesi, false, false)
                    : colBankat.merrSipasABNdermarrjesAndAutorizimeDTSipaMonedhes(idNdermarrje, idPerdoruesi, clsNdermarrje.ktheIdMonedheNdermSipasID(idNdermarrje), idmonedhaklienti);

                return dt.Select($"KodiBanka = {vlera}").GetDataTable(dt);
            }, "KodiBanka", "IdBanka");
            combo.DropDownStyle = DropDownStyle.DropDownList;
        }

        public static void KonfiguroComboBoxComboBankatSipasFiltrit(int idPerdoruesi, int idNdermarrje, ASPxComboBox combo, int idmonedhaklienti, string filter, int beginIndex, int endIndex)
        {
            combo.ConfigureAndFill(() =>
            {
                var dt = idmonedhaklienti == 0
                    ? colBankat.merrSipasABNdermarrjesAndAutorizimeDTLupa(idNdermarrje, idPerdoruesi, false, false)
                    : colBankat.merrSipasABNdermarrjesAndAutorizimeDTSipaMonedhes(idNdermarrje, idPerdoruesi, clsNdermarrje.ktheIdMonedheNdermSipasID(idNdermarrje), idmonedhaklienti);

                var rowSources = dt.Select($"KodiBanka like '%{filter}%'");
                if (rowSources.Any())
                {
                    dt = rowSources.CopyToDataTable();
                }
                return dt;
            }, "KodiBanka", "IdBanka");
            combo.DropDownStyle = DropDownStyle.DropDownList;
        }

        public static void KonfiguroComboBoxKlientFurnitoriById(int idPerdoruesi, int idNdermarrja, ASPxComboBox combo, int idKf) =>
            combo.ConfigureAndFill(() => clsKlientFurnitor.MbushKlienteOseFurnitoreMeId(idKf, idNdermarrja, idPerdoruesi), "KodKlientFurnitor", "IdKlientFurnitor");

        public static void KonfiguroComboBoxKlientFurnitoriById(ASPxComboBox combo, int idKf, bool shtoRreshtBosh = false)
        {
            combo.ConfigureAndFill(() =>
            {
                var dt = clsKlientFurnitor.MbushKlienteOseFurnitoreMeId(idKf);
                if (shtoRreshtBosh)
                    dt.AddRow();
                return dt;
            }, "KodKlientFurnitor", "IdKlientFurnitor");
            if (combo.Items.Count != 0)
                combo.Items[0].Selected = true;
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me kategorite e dokumentave
        /// </summary>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void KonfiguroComboBoxLlojEksporti(ASPxComboBox combo)
        {
            combo.Items.Add("File", 1);
            combo.Items.Add("SQL", 2);
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
            combo.SelectedIndex = 0;
        }

        public static void KonfiguroComboBoxFiltra(ASPxComboBox combo, int formati, int idNdermarrja) =>
            combo.ConfigureAndFill(() =>
            {
                var col = new colFiltraExporti(formati, idNdermarrja);
                col.Insert(0, new clsFiltraExporti());
                return col;
            }, "Kodi", "Id");

        public static void KonfiguroComboBoxKonfigurimExporti(ASPxComboBox combo, int idndermarje, int idViti, int idPerdoruesi, string komponente)
        {
            combo.ConfigureAndFill(() =>
            {
                var col = clsKonfigExporti.KtheKonfigExportiNdermarrjesSipasTeDrejtave(idndermarje, idViti, idPerdoruesi, komponente);
                col.Insert(0, new clsKonfigExporti(0, "", "", 0, 0, "", 0, 0, 0, "", 0, DateTime.MinValue, "", "", "", "", "", 1, "", false, false));
                return col;
            }, "Emer", "Id");
            combo.DropDownStyle = DropDownStyle.DropDown;
        }

        /// <summary>
        /// Perdoret per te mbushur nje combobox  me kategorite e dokumentave
        /// </summary>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void KonfiguroComboBoxKategoriPerImport(ASPxComboBox combo, bool eksportSql, int idNdermarrje, int idViti, int idPerdoruesi, string komponente) =>
            combo.ConfigureAndFill(() =>
            {
                var colKategori = new colKategoriNiveleDok
                {
                    new clsKategoriNivelDok(0, "", 0, 0, false, 0, false)
                };
                if (!eksportSql)
                    colKategori.merriTeGjitheKategoritePerImportSipasTeDrejtave(idNdermarrje, idViti, idPerdoruesi, komponente);
                else
                    colKategori.Add(new clsKategoriNivelDok(0, "Eksport Demesh", 0, 0, true, 0, false));
                return colKategori;
            }, "Pershkrimi", "IdKategori");

        public static void KonfiguroComboBoxPolitika(ASPxComboBox combo, int idNdermarrje, bool meKolona)
        {
            if (meKolona)
                ShtoKolonaPerPolitikat(combo);

            combo.ConfigureAndFill(() => clsPolitikeKarta.MerrPolitikeKarteSipasNdermarrjes(idNdermarrje), "Kodi", "IdPolitike");
        }

        public static void KonfiguroComboBoxPolitika(ASPxComboBox combo, int idNdermarrje, string filter)
        {
            combo.ConfigureAndFill(() =>
            {
                var dt = clsPolitikeKarta.MerrPolitikeKarteSipasNdermarrjes(idNdermarrje);
                return dt.Select($" Kodi like '%{filter}%' or Lloji like '{filter}'").GetDataTable(dt);
            }, "Kodi", "IdPolitike");
        }

        public static void KonfiguroComboBoxKategoriShpenzimi(ASPxComboBox combo, int idNdermarrje, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(e?.Filter)) return;

            combo.ConfigureAndFill(() =>
            {
                var colKategori = new colKategoriShpenzimi { new clsKategoriShpenzimi() };
                colKategori.MbushGjitheKategoriSipasNdermarjes(idNdermarrje);

                var dt = colKategori.ToDataTable();
                return dt.Select($"Kodi like '%{e.Filter}%' or Pershkrimi like '%{e.Filter}%' ")
                    .AsEnumerable()
                    .Skip(e.BeginIndex)
                    .Take(e.EndIndex - e.BeginIndex + 1)
                    .GetDataTable(dt);
            }, "", "Id");
        }

        public static void KonfiguroComboBoxKategoriShpenzimi(ASPxComboBox combo, int idNdermarrje, ListEditItemRequestedByValueEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(e?.Value?.ToString())) return;

            combo.ConfigureAndFill(() =>
            {
                var colKategori = new colKategoriShpenzimi { new clsKategoriShpenzimi() };
                colKategori.MbushGjitheKategoriSipasNdermarjes(idNdermarrje);

                var dt = colKategori.ToDataTable();
                return dt.Select($"Id = {e.Value}").GetDataTable(dt);
            }, "", "Id");
        }

        public static void KonfiguroComboBoxKategoriShpenzimi(ASPxComboBox combo, int idNdermarrje, bool meKolona)
        {
            if (meKolona)
                ShtoKolonaKategoriShpenzimi(combo);

            combo.ConfigureAndFill(() =>
            {
                var colKategori = new colKategoriShpenzimi { new clsKategoriShpenzimi() };
                colKategori.MbushGjitheKategoriSipasNdermarjes(idNdermarrje);

                return ListExtensions.ToDataTable(colKategori);
            }, "Kodi", "Id");
        }

        public static void KonfiguroComboBoxSipasLlojitKonfigUrdherPagese(int idNdermarrje, ASPxComboBox combo, int lloji)
        {
            combo.ConfigureAndFill(() => new colKonfigUrdherPagese(idNdermarrje, lloji), "Kodi", "Id");
        }

        public static void KonfiguroComboBoxNivelet(ASPxComboBox cmbNiveli, int idNdermarrje, int idPerdorues, int idKategori, int idViti, string komponente, bool ngaKonfigImporti, bool shtoKolona)
        {
            if (shtoKolona)
                ShtoKolonaPerNiveleRegjistrimi(cmbNiveli);
            cmbNiveli.ConfigureAndFill(() => colNivelRegjistrimi.ktheGjitheNivelRegjistrimiSipasKategoriDtComboSipasTeDrejtave(idKategori, idNdermarrje, idPerdorues, idViti, komponente, false), ngaKonfigImporti ? "Kodi" : "Pershkrimi", "IdNivel");
            cmbNiveli.SelectedIndex = 0;
        }
        public static void KonfiguroComboBoxNiveletSipasKategoriDtCombo(ASPxComboBox cmbNiveli, int idNdermarrje, int idPerdorues, int idKategori, bool ngaKonfigImporti, bool selektoIndeksin0, bool shtoKolona)
        {
            if (shtoKolona)
                ShtoKolonaPerNiveleRegjistrimi(cmbNiveli);
            cmbNiveli.ConfigureAndFill(() => colNivelRegjistrimi.ktheGjitheNivelRegjistrimiSipasKategoriDtCombo(idKategori, idNdermarrje, idPerdorues, false), ngaKonfigImporti ? "Kodi" : "Pershkrimi", "IdNivel");
            if (selektoIndeksin0)
                cmbNiveli.SelectedIndex = 0;
        }

        public static void KonfiguroComboBoxFormula(ASPxComboBox combo) =>
            combo.ConfigureAndFill<Formula>(0);

        public static void KonfiguroComboBoxNjesiPagese(ASPxComboBox combo) =>
            combo.ConfigureAndFill<NjesiPagese>(0);

        public static void KonfiguroComboBoxPunaMeparshme(ASPxComboBox combo) =>
            combo.ConfigureAndFill<PunaMeparshme>(0);

        public static void KonfiguroComboBoxNjesiKohe(ASPxComboBox combo, int selectedIndex) =>
            combo.ConfigureAndFill<NjesiKohe>(selectedIndex);

        public static void KonfiguroComboBoxStatusMarreveshje(ASPxComboBox combo, bool expired, bool emptyRow = false, int selectedIndex = 0)
        {
            if (emptyRow)
                combo.Items.Add("", 4);
            combo.ConfigureAndFill<StatusMarreveshje>(selectedIndex);
            if (expired)
                combo.Items.Add(MessagesResource.Messages["Skaduar"], 0);
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me datat e fillimit
        /// </summary>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void KonfiguroComboBoxDateFillimiMaturiteti(ASPxComboBox combo)
        {
            combo.ConfigureAndFill<DateFillimiMaturiteti>(0);
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me periudhat e maturimit
        /// </summary>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void KonfiguroComboBoxPeriudheMaturiteti(ASPxComboBox combo)
        {
            combo.ConfigureAndFill<PeriudheMaturiteti>(0);
        }

        public static void KonfiguroComboBoxDegeAdministrative(int idNdermarrje, ASPxComboBox combo, bool merrEdheJoAktive)
        {
            combo.ConfigureAndFill(() =>
            {
                var colDege = new colDegeAdministrative { new clsDegeAdministrative() };
                if (merrEdheJoAktive)
                    colDege.mbushGjitheDegeAdministrative(idNdermarrje);
                else
                    colDege.mbushGjitheDegeAdministrativeAktive(idNdermarrje);

                return colDege;
            }, "Kodi", "IdDegeAdministrative");
        }

        public static void mbushComboLayers(ASPxComboBox cmbLayers, int idPerdorues, int gjuha, int idNdermarrje, int idViti)
        {
            using (clsDatabaseGIS db = new clsDatabaseGIS())
            {
                cmbLayers.DataSource = db.merrLlojLayeriPerCombo(idPerdorues, gjuha, idNdermarrje, idViti, true);
                cmbLayers.ValueField = "IdType_IdLayer";
                cmbLayers.TextField = "Title";
                cmbLayers.ValueType = typeof(string);

                cmbLayers.DataBind();
            }
        }

        public static void KonfiguroComboBoxKartatPerDhurata(ASPxComboBox comboBox, int idNdermarrje)
        {
            comboBox.ConfigureAndFill(() => colKarta.MerrKartatSipasNdermarrjesPerDhurata(idNdermarrje), "Kodi", "IdKarta");
        }

        public static void KonfiguroComboBoxDhurata(ASPxComboBox comboBox, int idPolitike)
        {
            comboBox.ConfigureAndFill(() => new colTrupiPolitikeKarta(idPolitike), "Pike", "IdKategoria");
        }

        #endregion

        #region Shtimi i kolonave ne ComboBox

        public static void ShtoKolonaPerKonfigurimet(ASPxComboBox comboBox)
        {
            comboBox.TextFormatString = "{0}";
            comboBox.Columns.Clear();
            comboBox.Columns.Add(new ListBoxColumn
            {
                FieldName = "KodKonfigAmbjente",
                Caption = MessagesResource.Messages["cmbCmimeArtikulliCaptionKodi"]
            });
            comboBox.Columns.Add(new ListBoxColumn
            {
                FieldName = "PershkrimKonfigAmbjente",
                Caption = MessagesResource.Messages["cmbCmimeArtikulliCaptionPershkrimi"],
                Width = 250
            });
        }

        public static void ShtoKolonaPerKategoriZbritje(ASPxComboBox combo)
        {
            combo.TextFormatString = "{0};{1};{2}";

            combo.Columns.Add(new ListBoxColumn { FieldName = "KodKategoriZbritje" });
            combo.Columns.Add(new ListBoxColumn { FieldName = "PershkrimKategoriZbritje" });
            combo.Columns.Add(new ListBoxColumn { FieldName = "Zbritja" });
        }

        public static void ShtoKolonaPerNivelZbritjesh(ASPxComboBox combo)
        {
            combo.TextFormatString = "{0},{1}";

            combo.Columns.Add(new ListBoxColumn { FieldName = "KodNivelZbritje" });
            combo.Columns.Add(new ListBoxColumn { FieldName = "PershkrimNivelZbritje" });
        }

        public static void ShtoKolonaPerLlogarine(ASPxComboBox combo)
        {
            combo.TextFormatString = "{0};{1};{2}";

            combo.Columns.Add(new ListBoxColumn
            {
                FieldName = "NrLlogari",
                Caption = MessagesResource.Messages["labelNrLlogarie"]
            });
            combo.Columns.Add(new ListBoxColumn
            {
                FieldName = "EmerLlogari1",
                Caption = MessagesResource.Messages["labelEmerLlogarie"]
            });
            combo.Columns.Add(new ListBoxColumn
            {
                FieldName = "KodiMonedha",
                Caption = MessagesResource.Messages["labelMonedha"]
            });
        }

        public static void ShtoKolonaPerNivelCmimesh(ASPxComboBox combo)
        {
            combo.TextFormatString = "{0},{1},{2},{3}";

            combo.Columns.Add(new ListBoxColumn
            {
                FieldName = "KodNivelCmimi",
            });
            combo.Columns.Add(new ListBoxColumn
            {
                FieldName = "PershkrimNivelCmimi",
            });
            combo.Columns.Add(new ListBoxColumn
            {
                FieldName = "Lloji",
            });
            combo.Columns.Add(new ListBoxColumn
            {
                FieldName = "KodMonedha",
            });
        }

        public static void ShtoKolonaPerPolitikat(ASPxComboBox combo)
        {
            combo.Columns.Clear();
            combo.Columns.Add(new ListBoxColumn { FieldName = "Kodi" });
            combo.Columns.Add(new ListBoxColumn { FieldName = "Lloji" });
        }

        public static void ShtoKolonaKategoriShpenzimi(ASPxComboBox combo)
        {
            combo.TextFormatString = "{0}";
            combo.Columns.Add(new ListBoxColumn
            {
                FieldName = "Kodi",
                Caption = MessagesResource.Messages["cmbCmimeArtikulliCaptionKodi"]
            });
            combo.Columns.Add(new ListBoxColumn
            {
                FieldName = "Pershkrimi",
                Caption = MessagesResource.Messages["cmbCmimeArtikulliCaptionPershkrimi"]
            });
            combo.Columns.Add(new ListBoxColumn
            {
                FieldName = "NivelKategorie",
                Caption = MessagesResource.Messages["lblNivel"]
            });
        }

        public static void ShtoKolonaPerKf(ASPxComboBox combo)
        {
            combo.TextFormatString = "{0} ({1})";
            combo.Columns.Clear();
            combo.Columns.Add(new ListBoxColumn
            {
                FieldName = "KodKlientFurnitor",
                Caption = "Kodi"
            });
            combo.Columns.Add(new ListBoxColumn
            {
                FieldName = "EmertimiKF",
                Caption = "Emri"
            });
            combo.ValueField = "IdKlientFurnitor";
        }

        public static void ShtoKolonaKodiDhePershkrimi(ASPxComboBox combo, string fusha)
        {
            combo.TextFormatString = "{0} ({1})";
            combo.Columns.Clear();
            combo.Columns.Add(new ListBoxColumn
            {
                FieldName = "Kodi",
                Caption = "Kodi"
            });
            combo.Columns.Add(new ListBoxColumn
            {
                FieldName = "Pershkrimi",
                Caption = "Pershkrimi"
            });
            combo.ValueField = fusha;
        }

        public static void ShtoKolonaEmriDheMbiemri(ASPxComboBox combo, string fusha)
        {
            combo.TextFormatString = "{0} {1}";
            combo.Columns.Clear();
            combo.Columns.Add(new ListBoxColumn
            {
                FieldName = "Emri",
                Caption = "Emri"
            });
            combo.Columns.Add(new ListBoxColumn
            {
                FieldName = "Mbiemri",
                Caption = "Mbiemri"
            });
            combo.ValueField = fusha;
        }

        public static void ShtoKolonaPerNiveleRegjistrimi(ASPxComboBox comboBox)
        {
            comboBox.TextFormatString = "{1}";
            comboBox.Columns.Clear();
            comboBox.Columns.Add(new ListBoxColumn
            {
                FieldName = "Kodi",
                Caption = MessagesResource.Messages["cmbCmimeArtikulliCaptionKodi"]
            });
            comboBox.Columns.Add(new ListBoxColumn
            {
                FieldName = "Pershkrimi",
                Caption = MessagesResource.Messages["cmbCmimeArtikulliCaptionPershkrimi"],
                Width = 250
            });
        }

        public static void mbushComboLlojArt(ASPxComboBox cmbLloji)
        {
            cmbLloji.Items.Add(clsArtikulli.AfatGjateLabel, true.ToString().ToLower());
            cmbLloji.Items.Add(clsArtikulli.AfatShkurterLabel, false.ToString().ToLower());
        }

        #endregion

        public static void mbushComboMetodaFtp(ASPxComboBox cmbMetoda)
        {
            DataTable dt = clsFunksione.mbushMetodaTransferimiPerSerialeUnike(); 
            foreach(DataRow r in dt.Rows)
            {
                cmbMetoda.Items.Add(r[1].ToString(), r[0]);
            }
            cmbMetoda.SelectedIndex = 0;
            cmbMetoda.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        }
        public static void mbushComboKategoriaWebhooks(ASPxComboBox cmbKategoria)
        {
           

            cmbKategoria.Items.Add("Blerje", 0);
            cmbKategoria.Items.Add("Shitje", 1);
            cmbKategoria.Items.Add("Arketime", 2);
            cmbKategoria.Items.Add("Pagesa", 3);
            cmbKategoria.Items.Add("Login", 4);
            cmbKategoria.SelectedIndex = -1;
            cmbKategoria.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        }
        public static void mbushComboEventi(ASPxComboBox cmbEventi)
        {


            cmbEventi.Items.Add("Te gjitha", 0);
            cmbEventi.Items.Add("Shtim", 1);
            cmbEventi.Items.Add("Modifikim", 2);
            cmbEventi.Items.Add("Fshirje", 3);
            cmbEventi.SelectedIndex = -1;
            cmbEventi.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        }

        public static void mbushLlojiCombo(ASPxComboBox combo)
        {
            combo.Items.Add("AlphaWeb", 1);
            combo.Items.Add("AlphaMobile", 2);
            combo.Items.Add("Te dyja", 3);
        }
        public static void mbushComboTipiMag(ASPxComboBox combo)
        {//mbush kombon e llogaritjesh KMSH
            combo.Items.Add("WAREHOUSE", 1);
            combo.Items.Add("EXHIBITION", 2);
            combo.Items.Add("STORE", 3);
            combo.Items.Add("SALE", 4);
            combo.Items.Add("OTHER", 5);
            combo.SelectedIndex = 0;
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        }
        /// <summary>
        /// mbush nje kombobox me aktiv jo aktiv
        /// </summary>
        /// <param name="combo"> comboboxi qe do mbushet</param>
        public static void mbushComboAktivJoaktiv(ASPxComboBox combo)
        {
            combo.Items.Clear();
            combo.Items.Add("");
            combo.Items.Add("Aktiv", true);
            combo.Items.Add("Jo Aktiv", false);
        }

        /// <summary>
        /// perdoret per te shfaqur tek komboboxin butonin per hapjen e lupave
        /// </summary>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void percaktoTemplateCombo(bool meLupe = true, bool enableCallback = false, params ASPxComboBox[] combo)
        {
            for (int i = 0; i < combo.Length; i++)
            {
                if (meLupe)
                {
                    combo[i].DropDownButton.Visible = false;
                    if (combo[i].Buttons.Count == 0)
                        combo[i].Buttons.Add();
                }
                combo[i].EnableCallbackMode = enableCallback;
                combo[i].IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                combo[i].FilterMinLength = 0;
                combo[i].DropDownStyle = DropDownStyle.DropDownList;
                combo[i].ClientSideEvents.KeyUp = @"function(s,e) {var keyCode = e.htmlEvent.keyCode; if((keyCode==46 || keyCode==8) && s.GetInputElement().value == '')  s.SetSelectedIndex(-1); }";
            }
        }

        public static void percaktoTemplateComboMeLupe(params ASPxComboBox[] combo)
        {
            percaktoTemplateCombo(true, true, combo);
        }

        public static void percaktoTemplateComboJoListePaLupe(params ASPxComboBox[] combo)
        {
            percaktoTemplateCombo(false, false, combo);
        }

        public static void percaktoTemplateComboJoList(ASPxComboBox combo)
        {
            combo.DropDownButton.Visible = false;
            if (combo.Buttons.Count == 0)
                combo.Buttons.Add();
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.FilterMinLength = 0;
            combo.DropDownStyle = DropDownStyle.DropDown;
            combo.ClientSideEvents.KeyUp = @"function(s,e) {var keyCode = e.htmlEvent.keyCode; if((keyCode==46 || keyCode==8) && s.GetInputElement().value == '')  s.SetSelectedIndex(-1); }";
        }

        /// <summary>
        /// perdoret per te shfaqur tek komboboxin butonin per hapjen e lupave
        /// </summary>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void percaktoTemplateComboMeEnableCallback(params ASPxComboBox[] combot)
        {
            foreach (ASPxComboBox combo in combot)
            {
                //ASPxComboBox comboKodTemplate = combo as ASPxComboBox;
                combo.DropDownButton.Visible = false;
                combo.Buttons.Add();
                combo.EnableCallbackMode = true;
                combo.CallbackPageSize = 10;
                combo.SettingsLoadingPanel.Enabled = false;
                combo.SettingsLoadingPanel.ShowImage = false;
                combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                combo.DropDownStyle = DropDownStyle.DropDown;
            }
        }

        /// <summary>
        /// perdoret per te shfaqur tek komboboxin butonin per hapjen e kombove
        /// </summary>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void percaktoTemplateComboMeEnableCallbackPaButon(ASPxComboBox combo)
        {
            ASPxComboBox comboKodTemplate = combo as ASPxComboBox;

            //     combo.EnableCallbackMode = true;
            //combo.CallbackPageSize = 20;
            combo.SettingsLoadingPanel.Enabled = false;
            combo.SettingsLoadingPanel.ShowImage = false;
            comboKodTemplate.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            comboKodTemplate.DropDownStyle = DropDownStyle.DropDown;
        }

        public static void mbushComboNrUrdherShitje(string id, ASPxComboBox combo)
        {

            combo.DataSource = colDokumentat.ktheDokumentaRegjistrimDokumentashsipasId(id);
            combo.ValueField = "IdShitjeKoka";
            combo.TextField = "NrDok";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        public static void mbushComboNrUrdherShitje(string filter, int start, int end, ASPxComboBox combo, int idndermarje, string datanga, string dataderi, int idperdorues, string kodkonfigurimi, string kodartikulli, string grupi, string nengrupi, string klienti)
        {

            combo.DataSource = colDokumentat.ktheDokumentaRegjistrimDokumentashMeFiltra(filter, start, end, idndermarje, datanga, dataderi, idperdorues, kodkonfigurimi, kodartikulli, grupi, nengrupi, klienti);
            combo.ValueField = "IdDokumenti";
            combo.TextField = "NrDokumenti";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        public static void mbushComboKLlojElementesh(int idndermarje, ASPxComboBox combo, int idgjuha)
        {

            clsDatabaseRegjistrim regj = new clsDatabaseRegjistrim();
            //DbCore.DbAdmin.colAgjenteShitje colAgjentet = dbAdmin.merrGjitheAgjentetShitjes(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            combo.DataSource = regj.merrLlojElementesh(idndermarje, idgjuha);
            combo.ValueField = "LLOJIGLOBALELEMENT";
            combo.TextField = "LLOJI";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();

        }

        public static void mbushComboStatusiElem(int idndermarje, ASPxComboBox combo, int idgjuha)
        {

            clsDatabaseRegjistrim regj = new clsDatabaseRegjistrim();
            //DbCore.DbAdmin.colAgjenteShitje colAgjentet = dbAdmin.merrGjitheAgjentetShitjes(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            combo.DataSource = regj.merrstatusElementesh(idndermarje, idgjuha);
            combo.ValueField = "LLOJIGLOBALELEMENT";
            combo.TextField = "LLOJI";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();

        }

        public static void mbushComboNrAutomatik(int idNdermarrje, int idkategoria, ASPxComboBox combo)
        {//mbush griden e popupit me te dhena
            colNrAutom nraut = new colNrAutom();
            nraut.mbushGjitheNumratAutomatikeSipasKategorise(idNdermarrje, idkategoria);
            combo.DataSource = nraut;
            combo.ValueField = "IdNrAutom";
            combo.TextField = "KodiNrAutom";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        public static void mbushComboLicencat(ASPxComboBox cmbLicenca, bool selektoteparen)
        {
            DataTable licencat = clsLicenca.merrLicencaAktive();
            cmbLicenca.DataSource = licencat;
            cmbLicenca.TextFormatString = "{0}";

            ListBoxColumn colprove = new ListBoxColumn();
            colprove.FieldName = "KODLICENCA";
            colprove.Caption = "Kodi";
            ListBoxColumn colemer = new ListBoxColumn();
            colemer.FieldName = "kodicrm";
            colemer.Caption = "Kodi CRM";
            ListBoxColumn colmonedha = new ListBoxColumn();
            colmonedha.FieldName = "pershkrimi";
            colmonedha.Caption = "Pershkrimi";
            cmbLicenca.Columns.Add(colprove);

            cmbLicenca.Columns.Add(colmonedha);
            cmbLicenca.Columns.Add(colemer);
            //cmbLicenca.TextField = "KODLICENCA";
            cmbLicenca.ValueField = "IDLICENCA";
            cmbLicenca.DataBind();
            if (selektoteparen)
                cmbLicenca.SelectedIndex = 0;
        }

        public static void mbushComboTransportues(int idNdermarrje, ASPxComboBox combo)
        {//mbush griden e popupit me te dhena
            DataTable dt = new DataTable();
            dt = colTransportues.merrTransportuesSipasNdermarrjes(idNdermarrje, true);
            combo.DataSource = dt;
            combo.ValueField = "IdTransportues";
            combo.TextField = "Emertimi";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        public static void mbushComboTransportuesMeFilter(string filter, long startIndex, long endIndex, ASPxComboBox combo, int idNdermarrje)
        {//mbush griden e popupit me te dhena
            DataTable dt = new DataTable();
            dt = colTransportues.ktheTransportuesSipasNdermMeFilter(filter, startIndex, endIndex, idNdermarrje);
            combo.DataSource = dt;
            combo.ValueField = "IdTransportues";
            combo.TextField = "Emertimi";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        public static void shtoKolonaPerAgjentin(ASPxComboBox combo)
        {
            combo.TextFormatString = "{0} ({1})";

            ListBoxColumn colprove = new ListBoxColumn();
            colprove.FieldName = "KodiAgjentShitje";
            ListBoxColumn colemer = new ListBoxColumn();
            colemer.FieldName = "EmriAgjentShitje";

            combo.Columns.Add(colprove);
            combo.Columns.Add(colemer);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="combo"></param>
        /// <param name="coleksion"></param>
        /// <param name="ValueField"></param>
        /// <param name="TextField"></param>
        public static void mbushComboFiltra(ASPxComboBox combo, Object coleksion, String ValueField, String TextField, string value = "")
        {//mbush griden e popupit me te dhena
            combo.DataSource = coleksion;
            combo.ValueField = ValueField;
            combo.TextField = TextField;
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
            if (!String.IsNullOrEmpty(value))
                combo.Value = ((colFiltratGrida)coleksion).FirstOrDefault(x => x.FiltraKodi == value).IdFiltra.ToString();
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me arkat
        /// </summary>
        /// <param name="idNdermarrja"></param>
        /// <param name="idPerdoruesi"></param>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboArkat(int idNdermarrje, int idPerdoruesi, ASPxComboBox combo, bool plotesodefault)
        {
            colBankat colBankat = new colBankat();
            DataTable dt = colBankat.ktheGjitheBankatSipasAutorizimeveSipasLlojit(idNdermarrje, idPerdoruesi, plotesodefault, false);
            //colBankat = dbArkaBanka.merrGjitheBankatSipasAutorizimeveSipasLlojit(DbCore.clsFunksione.merrIdNdermarrjeSesioni(Session), DbCore.clsFunksione.ktheIdPerdoruesi(Session), true);
            combo.DataSource = dt;
            combo.ValueField = "IdBanka";
            combo.TextField = "KodiBanka";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
            if (plotesodefault && dt.Rows.Count == 1)
                combo.SelectedIndex = 1;
            dt.Dispose();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="combo"></param>
        /// <param name="dt"></param>
        public static void mbushComboArtikulli(ASPxComboBox combo, DataTable dt)
        {// shton kombobox tek grida per artikullin
            combo.DataSource = dt;
            combo.TextField = "KodArtikulli";
            combo.ValueField = "IdArtikulli";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
            dt.Dispose();
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me kodet e artikujve
        /// </summary>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboArtikulli(int idPerdoruesi, int idNdermarrje, string postStringTvsh, params ASPxComboBox[] combos)
        {
            DataTable dt = colArtikujt.merrSipasArtikujNdermarrjesAndAutorizimeDT(idNdermarrje, idPerdoruesi, false, false, postStringTvsh);
            mbushComboArtikulli(idPerdoruesi, idNdermarrje, postStringTvsh, dt, combos);

            dt.Dispose();
        }

        public static void mbushComboArtikulli(int idPerdoruesi, int idNdermarrje, string postStringTvsh, DataTable dt, params ASPxComboBox[] combos)
        {
            foreach (var combo in combos)
            {
                combo.DataSource = dt;
                combo.TextField = "KodArtikulli";
                combo.ValueField = "IdArtikulli";
                combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                combo.DataBind();
            }
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me emertimet e para te artikujve
        /// </summary>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboArtikulliEmertimi(int idPerdoruesi, int idNdermarrje, ASPxComboBox combo, string postStringTvsh)
        {// shton kombobox tek grida per artikullin
            //   DbCore.DbInventari.clsDatabaseInventari dbInventari = new DbCore.DbInventari.clsDatabaseInventari();
            //   DbCore.DbInventari.colArtikujt colArtikujt = new DbCore.DbInventari.colArtikujt();
            //   DbCore.DbAdmin.clsPerdorues oPerdorues = (DbCore.DbAdmin.clsPerdorues)(Session["oClsPerdoruesi"]);
            ////   colArtikujt.merrSipasArtikujAktivNdermarrjesAndAutorizime(DbCore.clsFunksione.merrIdNdermarrjeSesioni(Session), oPerdorues.IdPerdorues);
            //   //colArtikujt = dbInventari.merrArtikujAktivNdermarrjesAndAutorizime(DbCore.clsFunksione.merrIdNdermarrjeSesioni(Session), oPerdorues.IdPerdorues);
            //   DataTable dt = DbCore.DbInventari.colArtikujt.merrSipasArtikujNdermarrjesAndAutorizimePerLupeArtikulli(DbCore.clsFunksione.merrIdNdermarrjeSesioni(Session), DbCore.clsFunksione.ktheIdPerdoruesi(Session));
            //   combo.DataSource = dt;// colArtikujt;
            DataTable dt = colArtikujt.merrSipasArtikujNdermarrjesAndAutorizimeDT(idNdermarrje, idPerdoruesi, false, false, postStringTvsh);
            combo.DataSource = dt;
            combo.TextField = "PershkrimArtikulli";
            combo.ValueField = "IdArtikulli";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
            dt.Dispose();
        }

        public static void mbushComboArtikulliEmertimi(ASPxComboBox combo, DataTable dt)
        {// shton kombobox tek grida per artikullin
            combo.DataSource = dt;
            combo.TextField = "PershkrimArtikulli";
            combo.ValueField = "IdArtikulli";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
            dt.Dispose();
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me autorizimet
        /// </summary>
        /// <param name="idPerdoresi"></param>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboAutorizime(int idPerdoresi, ASPxComboBox combo)
        {
            colAutorizimetKoka colAutorizim = new colAutorizimetKoka(idPerdoresi);
            colAutorizim.shtoAutorizimKokeNeIndeksin(0, new clsAutorizimKoka());
            combo.DataSource = colAutorizim;
            combo.TextField = "KodiAutorizim";
            combo.ValueField = "KodiAutorizim";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox formatet e numrave (0, 0.0, 0.00, 0.000, etj.)
        /// </summary>
        /// <param name="idPerdoresi"></param>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboFormateNumrash(ASPxComboBox combo)
        {
            colFormatNr colFormat = new colFormatNr();
            colFormat.mbushFormatNr();
            combo.DataSource = colFormat;
            combo.TextField = "VlereFormati";
            combo.ValueField = "IdFormatNr";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox formatet e numrave (0, 0.0, 0.00, 0.000, etj.)
        /// </summary>
        /// <param name="idPerdoresi"></param>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboFormateNumrashNew(ASPxComboBox combo)
        {
            combo.Items.Add("0", 0);
            combo.Items.Add("0.0", 1);
            combo.Items.Add("0.00", 2);
            combo.Items.Add("0.000", 3);
            combo.Items.Add("0.0000", 4);
            combo.Items.Add("0.00000", 5);
            combo.Items.Add("0.000000", 6);
            combo.Items.Add("0.0000000", 7);
            combo.Items.Add("0.00000000", 8);
            combo.Items.Add("0.000000000", 9);
            combo.Items.Add("0.0000000000", 10);
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox llojet e kurseve
        /// </summary>
        /// <param name="idPerdoresi"></param>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboLlojeKursi(ASPxComboBox combo)
        {
            combo.Items.Add("Kursi 1", 1);
            combo.Items.Add("Kursi 2", 2);
            combo.Items.Add("Kursi 3", 3);
            combo.Items.Add("Kursi 4", 4);
            combo.Items.Add("Kursi 5", 5);
            combo.Items.Add("Kursi 6", 6);
            combo.Items.Add("Kursi 7", 7);
            combo.Items.Add("Kursi 8", 8);
            combo.Items.Add("Kursi 9", 9);
            combo.Items.Add("Kursi 10", 10);
            combo.Items.Add("Kursi 11", 11);
            combo.Items.Add("Kursi 12", 12);
            combo.Items.Add("Kursi 13", 13);
            combo.Items.Add("Kursi 14", 14);
            combo.Items.Add("Kursi 15", 15);
            combo.Items.Add("Kursi 16", 16);
            combo.Items.Add("Kursi 17", 17);
            combo.Items.Add("Kursi 18", 18);
            combo.Items.Add("Kursi 19", 19);
            combo.Items.Add("Kursi 20", 20);
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me autorizimet
        /// </summary>
        /// <param name="idPerdoresi"></param>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboKonfigFormatiNumrash(int idNdermarrje, ASPxComboBox combo)
        {
            DataTable dt = colFormatKonfig.merrGjitheFormatetSipasNdermarrjes(idNdermarrje);
            combo.DataSource = dt;
            dt.Rows.InsertAt(dt.NewRow(), 0);
            combo.TextField = "Kodi";
            combo.ValueField = "IdFormatKonfig";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me autorizimet
        /// </summary>
        /// <param name="idPerdoresi"></param>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboKonfigFormatiNumrashSipasKategorise(int idNdermarrje, int idKategoria, ASPxComboBox combo)
        {
            DataTable dt = colFormatKonfig.merrGjitheFormatetSipasKategoriseDheNdermarrjes(idNdermarrje, idKategoria);
            combo.DataSource = dt;
            dt.Rows.InsertAt(dt.NewRow(), 0);
            combo.TextField = "Kodi";
            combo.ValueField = "IdFormatKonfig";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me bankat
        /// </summary>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboBankat(int idPerdoruesi, int idNdermarrje, ASPxComboBox combo, bool plotesodefault)
        {
            colBankat colBankat = new colBankat();
            DataTable dt = colBankat.ktheGjitheBankatSipasAutorizimeveSipasLlojit(idNdermarrje, idPerdoruesi, true, false);
            //colBankat = dbArkaBanka.merrGjitheBankatSipasAutorizimeveSipasLlojit(DbCore.clsFunksione.merrIdNdermarrjeSesioni(Session), DbCore.clsFunksione.ktheIdPerdoruesi(Session), true);
            combo.DataSource = dt;
            combo.ValueField = "IdBanka";
            combo.TextField = "KodiBanka";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
            if (plotesodefault && dt.Rows.Count == 1)
                combo.SelectedIndex = 1;
            dt.Dispose();
        }

        public static void mbushComboBurimet(int idNdermarrje, ASPxComboBox combo)
        {
            colBurimet burimet = new colBurimet(idNdermarrje);

            combo.DataSource = burimet;
            combo.ValueField = "IdBurimi";
            combo.TextField = "Kodi";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        public static void mbushComboArkaBankaById(ASPxComboBox combo, int idArkaBanka)
        {//mbush griden e popupit me te dhena
            DataTable dt = clsBanka.ktheArkaBankaSipasIdDt(idArkaBanka);
            combo.DataSource = dt;
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
            if (combo.Items.Count != 0)
                combo.Items[0].Selected = true;
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me bankat jo sipas llojit
        /// </summary>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboBankatJoSipasLlojit(int idPerdoruesi, int idNdermarrje, ASPxComboBox combo, int idmonedhaklienti, bool plotesodefault)
        {
            //clsNdermarrje nderm = new clsNdermarrje(idNdermarrje);
            //clsMonedha monnderm = new clsMonedha(nderm.NdermarrjeMonedha);
            //clsMonedha mon = new clsMonedha();

            //mon.mbushMonedhen(monnderm.KodiMonedha, idNdermarrje); //TOCHECK Nestila - seriozisht e mbushin 2 here kot monedhen se ne fund i duhet id-ja qe e ka qe tek ndermarrja?!?!?!?!?!
            colBankat colBankat = new colBankat();

            DataTable dt = new DataTable();
            if (idmonedhaklienti == 0)
                dt = colBankat.merrSipasABNdermarrjesAndAutorizimeDTLupa(idNdermarrje, idPerdoruesi, false, false);
            else dt = colBankat.merrSipasABNdermarrjesAndAutorizimeDTSipaMonedhes(idNdermarrje, idPerdoruesi, clsNdermarrje.ktheIdMonedheNdermSipasID(idNdermarrje), idmonedhaklienti);

            //colBankat = dbArkaBanka.merrGjitheBankatSipasAutorizimeveSipasLlojit(DbCore.clsFunksione.merrIdNdermarrjeSesioni(Session), DbCore.clsFunksione.ktheIdPerdoruesi(Session), true);
            combo.DataSource = dt;
            combo.ValueField = "IdBanka";
            combo.TextField = "EmerBanka";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;

            combo.DataBind();
            if (plotesodefault && dt.Rows.Count == 1)
                combo.SelectedIndex = 1;
            dt.Dispose();
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me datat e komponenteve
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboDataKomponente(int idNdermarrje, ASPxComboBox combo, bool lloji)
        {
            combo.DataSource = colKomponentePage.merrDataKomponente(idNdermarrje, lloji)?.OrderByDescending(x => x).Select(dt => dt.ToShortDateString()).ToList();
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
            combo.SelectedIndex = 0;
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me datat e tatimeve
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboDataTatime(int idNdermarrje, ASPxComboBox combo)
        {
            combo.DataSource = colTatimet.merrDataKomponente(idNdermarrje);
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
            combo.SelectedIndex = 0;
        }

        public static void mbushComboNjesiAdm(int idNdermarrje, int idPerdoruesi, ASPxComboBox combo)
        {
            DbCore.DbRegjistrim.colNjesiAdministrative colNjesiAdm = new DbCore.DbRegjistrim.colNjesiAdministrative();
            colNjesiAdm.mbushGjitheNjesiAdministrative(idNdermarrje, idPerdoruesi);

            combo.DataSource = colNjesiAdm;
            combo.TextField = "Kodi";
            combo.ValueField = "IdDegeAdministrative";
            combo.DataBind();
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        }

        public static void mbushComboNdermarrjePerGIS(ASPxComboBox combo, bool tePalidhura)
        {
            DbCore.DbAdmin.colNdermarrjet colNdermarrjePerGIS = new DbCore.DbAdmin.colNdermarrjet();
            colNdermarrjePerGIS.mbushNdermarrjePerGIS(tePalidhura);
            combo.DataSource = colNdermarrjePerGIS;
            combo.ValueField = "IdNdermarrje";
            combo.ValueType = typeof(int);
            combo.TextField = "NdermarrjeKodi";
            combo.DataBind();
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        }

        public static void mbushComboKonfigurimeshSipasKategorise(ASPxComboBox combo, colKonfigurimAmbjenti colKonfigAmbjenti)
        {
            combo.DataSource = colKonfigAmbjenti;
            combo.ValueField = "IdKonfigAmbjente";
            combo.IncrementalFilteringMode = DevExpress.Web.IncrementalFilteringMode.Contains;
            combo.DataBind();

            combo.SelectedIndex = 0;
        }

        public static void mbushComboKonfigurimeshSipasKategoriseKoontabilizim(ASPxComboBox combo, colKonfigurimAmbjenti colKonfigAmbjenti)
        {
            combo.DataSource = colKonfigAmbjenti;
            combo.ValueField = "IdKonfigAmbjente";
            combo.TextField = "KodKonfigAmbjente";
            combo.IncrementalFilteringMode = DevExpress.Web.IncrementalFilteringMode.Contains;
            combo.DataBind();

            // combo.SelectedIndex = 0;
        }

        public static void mbushComboStatusMagazine(int idNdermarrje, ASPxComboBox combo)
        {
            colStatusMagazine_Asete col = new colStatusMagazine_Asete();
            col.merrStatusMagazineTePerdorshmeTeNdermarrjes(idNdermarrje);

            combo.DataSource = col;
            combo.TextField = "Emertimi";
            combo.ValueField = "IdStatusMagazine";
            combo.SelectedIndex = 0;
            combo.DataBind();
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        }

        public static void mbushComboLlojMagazine(ASPxComboBox combo)
        {
            colLlojNjesiAdministrative col = new colLlojNjesiAdministrative();
            col.merrLlojNjesiAdministrative();
            combo.DataSource = col;
            combo.TextField = "Emertimi";
            combo.ValueField = "IdLlojNjesiAdministrative";
            combo.DataBind();
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        }

        public static void mbushComboLlojLayerMagazine(ASPxComboBox combo)
        {
            clsDatabaseGIS dbGis = new clsDatabaseGIS();
            combo.DataSource = dbGis.ktheGjitheLlojLayerMagazine();
            dbGis.Dispose();
            combo.TextField = "PERSHKRIMI";
            combo.ValueField = "IDLLOJLAYER";
            combo.DataBind();
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        }

        public static void mbushComboDateAmortizimi(ASPxComboBox combo, bool fillimi, int idGjuha)
        {
            colPeriudhaLlogaritje lloj = new colPeriudhaLlogaritje();
            if (fillimi)
                lloj.merrPeriudhaLlogaritjeFillim(idGjuha);
            else lloj.merrPeriudhaLlogaritjeFund(idGjuha);

            combo.DataSource = lloj;
            combo.TextField = "Emertimi";
            combo.ValueField = "IdPeriudheLlogAmortizimi";
            combo.DataBind();
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        }

        public static void mbushComboStandartAmortizimi(ASPxComboBox combo, int idndermarje)
        {
            colStandarteAmortizimi lloj = new colStandarteAmortizimi(idndermarje);

            combo.DataSource = lloj;
            combo.TextField = "Pershkrimi";
            combo.ValueField = "IdStandarti";
            combo.DataBind();
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        }

        public static void mbushComboStandartAmortizimiKodi(ASPxComboBox combo, int idndermarje)
        {
            colStandarteAmortizimi lloj = new colStandarteAmortizimi(idndermarje);

            combo.DataSource = lloj;
            combo.TextField = "Emertimi";
            combo.ValueField = "IdStandarti";
            combo.DataBind();
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        }

        public static void mbushComboPoJo(ASPxComboBox combo)
        {
            combo.Items.Add("Jo", 0);
            combo.Items.Add("Po", 1);
            combo.SelectedIndex = 0;
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        }

        public static void mbushComboStatusMagazine(ASPxComboBox combo, int idndermarje)
        {
            colStatusMagazine_Asete statusMag = new colStatusMagazine_Asete();
            //statusMag.Add(new DbCore.DbAsete.clsStatusMagazine_Asete());
            statusMag.merrStatusMagazineTePerdorshmeTeNdermarrjes(idndermarje);
            statusMag.Insert(0, new clsStatusMagazine_Asete());
            combo.DataSource = statusMag;
            combo.TextField = "emertimi";
            combo.ValueField = "idStatusMagazine";
            combo.DataBind();
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        }

        public static void mbushComboStatusRiparimi(int idNdermarrje, ASPxComboBox combo)
        {
            colStatusRiparimi col = new colStatusRiparimi(idNdermarrje);
            combo.DataSource = col;
            combo.TextField = "Pershkrimi";
            combo.ValueField = "Id";
            combo.DataBind();
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        }

        public static void mbushComboLlojDifekti(int idNdermarrje, ASPxComboBox combo)
        {
            colLlojDifekti col = new colLlojDifekti(idNdermarrje);

            combo.DataSource = col;
            combo.TextField = "Kodi";
            combo.ValueField = "Id";
            combo.DataBind();

            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me faturat
        /// </summary>
        /// <param name="idNderViti"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        /// <param name="kat">kategoria e dokumentit</param>
        public static void mbushComboFaturat(int idNderViti, int idNdermarrje, ASPxComboBox combo)
        {
            DbCore.DbRegjistrim.colDokumentat col = new DbCore.DbRegjistrim.colDokumentat();
            clsNdermarrjeViti nder = new clsNdermarrjeViti(idNderViti);
            DataTable dt = DbCore.DbRegjistrim.colDokumentat.mbushGjitheDokumentatRegjistrimDokumentashShperndarjeShpenzimesh(idNdermarrje, new DateTime().ToShortDateString(), nder.NdermarrjeVitiFund.ToShortDateString());

            //DbCore.DbRegjistrim.colKokaShitje col = regj.merrGjitheKokaShitje(new DbCore.clsFunksione().ktheNdermarrjeVit(), kat);
            combo.DataSource = dt;
            combo.ValueField = "IdDokumenti";
            combo.TextField = "NrDokumenti";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
            dt.Dispose();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <param name="combo"></param>
        /// <param name="llojDok"></param>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idNdermarrje"></param>
        public static void mbushComboArkaBankaSipasFilter(string filter, long startIndex, long endIndex, ASPxComboBox combo, string llojDok, int idPerdoruesi, int idNdermarrje)
        {
            DataTable dt = new DataTable();
            if (llojDok == "derdhje" || llojDok == "terheqje")
                dt = colBankat.mbushArkaBankaSipasFilter(filter, startIndex, endIndex, true, idNdermarrje, idPerdoruesi);
            else
                dt = colBankat.mbushArkaBankaSipasFilter(filter, startIndex, endIndex, false, idNdermarrje, idPerdoruesi);
            combo.DataSource = dt;
            combo.ValueField = "IdBanka";
            combo.TextField = "KodiBanka";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
            dt.Dispose();
        }

        public static void mbushComboNiveletSipasTeDrejtaveMeKolona(ASPxComboBox cmbNiveli, int idNdermarrje, int idPerdorues, int idKategori, int idViti, string komponente, bool ngaKonfigImporti, bool selektoIndeksin0)
        {
            ListBoxColumn colKodi = new ListBoxColumn();
            colKodi.FieldName = "Kodi";
            colKodi.Caption = MessagesResource.Messages["cmbCmimeArtikulliCaptionKodi"];
            ListBoxColumn colPershkrim = new ListBoxColumn();
            colPershkrim.FieldName = "Pershkrimi";
            colPershkrim.Caption = MessagesResource.Messages["cmbCmimeArtikulliCaptionPershkrimi"];
            cmbNiveli.TextFormatString = "{1}";
            if (cmbNiveli.Columns.Count != 0)
            {
                cmbNiveli.Columns.Clear();
            }
            cmbNiveli.Columns.Add(colKodi);
            cmbNiveli.Columns.Add(colPershkrim);
            cmbNiveli.DataSource = DbCore.DbRegjistrim.colNivelRegjistrimi.ktheGjitheNivelRegjistrimiSipasKategoriDtComboSipasTeDrejtave(idKategori, idNdermarrje, idPerdorues, idViti, komponente, false);
            cmbNiveli.ValueField = "IdNivel";
            cmbNiveli.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            cmbNiveli.DataBind();
            if (selektoIndeksin0)
                cmbNiveli.SelectedIndex = 0;
        }

        /// <summary>
        /// </summary>
        /// <param name="combo"></param>
        /// <param name="idkat"></param>
        public static void mbushComboFormatPrintimi(ASPxComboBox combo, int idkat, int idNderm, bool lejoBosh = false)
        {
            colRaporteDesign col = new colRaporteDesign();
            if (lejoBosh)
                ListExtensions.AddIfNotExists(col, new clsRaportDesign());
            col.merrSipasKategorise(idkat, idNderm);
            combo.DataSource = col;
            combo.TextField = "Pershkrim";
            combo.ValueField = "IdRaportDesign";
            combo.DataBind();
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        }

        public static void mbushComboOperatori(ASPxComboBox combo, int idNderm)
        {
            combo.DataSource = clsOperator.MerrOperatoretAktive(idNderm);
            combo.TextField = "Kodi";
            combo.ValueField = "IdOperator";
            combo.DataBind();
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        }

        public static void mbushComboKonfigurimKase(ASPxComboBox combo, int idNdermarje)
        {
            combo.Columns.Clear();
            ListBoxColumn colprove = new ListBoxColumn();
            colprove.FieldName = "Kodi";
            ListBoxColumn colLloji = new ListBoxColumn();
            colLloji.FieldName = "Lloji";
            ListBoxColumn colemer = new ListBoxColumn();
            colemer.FieldName = "Url";
            combo.TextFormatString = "{0}";
            combo.Columns.Add(colprove);
            combo.Columns.Add(colLloji);
            combo.Columns.Add(colemer);
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.AllowNull = true;
            combo.DropDownStyle = DropDownStyle.DropDownList;
            var dt = colKonfigurimeKase.merrKonfigurimetSipasNdermarjesMeUrl(idNdermarje);
            dt.Rows.InsertAt(dt.NewRow(), 0);
            combo.DataSource = dt;
            combo.ValueField = "IdKonfigurimi";
            combo.DataBind();
            combo.SelectedIndex = 0;
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me grupet e llogarive
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboGrupeLlogarish(int idNdermarrje, ASPxComboBox combo, int idGjuha)
        {//mbush combon e grupeve me te dhena nga databasa
            //DbCore.DbKontabiliteti.clsDatabaseKontabilitet dbKontabiliteti = new DbCore.DbKontabiliteti.clsDatabaseKontabilitet();
            //DbCore.DbKontabiliteti.colGrupetLlogaria colGrupetLlogaria = dbKontabiliteti.merrGjitheGrupetLlogariapozitive(DbCore.clsFunksione.merrIdNdermarrjeSesioni(Session));
            colGrupetLlogaria colGrupetLlogaria = new colGrupetLlogaria(idNdermarrje, idGjuha);
            combo.DataSource = colGrupetLlogaria;
            combo.TextField = "PershkrimiGrupiLlogaria";
            combo.ValueField = "IdGrupiLlogaria";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me grupet e bankave
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboGrupeBankash(int idNdermarrje, ASPxComboBox combo, bool lloji)
        {//mbush combon e grupeve me te dhena nga databasa
            colGrupeBanke col = new colGrupeBanke(lloji, idNdermarrje);
            //DbCore.DbArkaBanka.colGrupeBanke col = db.merrGjitheGrupetBankeSipasLlojit(lloji, ktheIdNdermarrje());
            combo.DataSource = col;
            combo.TextField = "NrGrupBanke";
            combo.ValueField = "IdGrupBanke";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        public static void mbushComboGrupeNdermarrjesh(int idlicenca, ASPxComboBox combo)
        {//mbush combon e grupeve me te dhena nga databasa
            colGrupNdermarje col = new colGrupNdermarje(idlicenca);
            combo.DataSource = col;
            combo.TextField = "Kodi";
            combo.ValueField = "Id";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        public static void mbushComboNdermarjeMeme(int idlicenca, ASPxComboBox combo)
        {//mbush combon e grupeve me te dhena nga databasa
            colNdermarrjet col = new colNdermarrjet();
            col.merrNdermarrjetMeme(idlicenca);
            combo.DataSource = col;
            combo.TextField = "NdermarrjeKodi";
            combo.ValueField = "IdNdermarrje";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        public static void mbushComboNdermarjeRaportues(colNdermarrjet ndermarrjet, ASPxComboBox combo)
        {//mbush combon e ndermarrjeve me te dhena nga databasa
            ndermarrjet.Insert(0, new clsNdermarrje());
            combo.DataSource = ndermarrjet;
            combo.TextField = "NdermarrjeKodi";
            combo.ValueField = "IdNdermarrje";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me grupet e punonjsve
        /// </summary>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboGrupePunonjesish(ASPxComboBox combo, int idndermarje)
        {//mbush combon e grupeve me te dhena nga databasa
            colGrupePunonjesish col = new colGrupePunonjesish(idndermarje);
            combo.DataSource = col;
            combo.TextField = "Nr";
            combo.ValueField = "IdGrupPunonjesish";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me grupet e dokumentave sipas konfigurimit
        /// </summary>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboGrupeDokumentashSipasKonfigurimit(ASPxComboBox combo, int idndermarje, int grupi, int idkonfig, int idPerdoruesi)
        {//mbush combon e grupeve me te dhena nga databasa
            DbCore.DbRegjistrim.colGrupimDokumentiKoka col = new DbCore.DbRegjistrim.colGrupimDokumentiKoka();
            // col.Add(new DbRegjistrim.clsGrupimDokumentiKoka());
            col.merrGrupeSipasKonfigurimit(grupi, idndermarje, idkonfig, idPerdoruesi);
            combo.DataSource = col;
            combo.TextField = "Kodi";
            combo.ValueField = "IdGrupimKoka";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DropDownStyle = DropDownStyle.DropDown;
            combo.DataBind();
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me grupet e dokumentave sipas konfigurimit
        /// </summary>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboGrupeDokumentashSipasKonfigurimit(ASPxComboBox combo, colGrupimDokumentiKoka col, int grupi)
        {//mbush combon e grupeve me te dhena nga databasa


            combo.DataSource = col.FindAll(x => x.Grupi == grupi);
            combo.TextField = "Kodi";
            combo.ValueField = "IdGrupimKoka";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DropDownStyle = DropDownStyle.DropDown;
            combo.DataBind();
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me grupet e dokumentave sipas konfigurimit
        /// </summary>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboGrupeDokumentashSipasKategorise(ASPxComboBox combo, int idndermarje, int grupi, string idKatDok, int idPerdoruesi)
        {//mbush combon e grupeve me te dhena nga databasa
            DbCore.DbRegjistrim.colGrupimDokumentiKoka col = new DbCore.DbRegjistrim.colGrupimDokumentiKoka();
            // col.Add(new DbRegjistrim.clsGrupimDokumentiKoka());
            col.merrGrupeSipasKategorise(grupi, idndermarje, idKatDok, idPerdoruesi);
            combo.DataSource = col;
            combo.TextField = "Kodi";
            combo.ValueField = "IdGrupimKoka";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DropDownStyle = DropDownStyle.DropDown;
            combo.DataBind();
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me grupet e kontabilizimit
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboGrupKontabilizimi(int idNdermarrje, ASPxComboBox combo)
        {
            //DbCore.DbKontabiliteti.clsDatabaseKontabilitet dbKontabiliteti = new DbCore.DbKontabiliteti.clsDatabaseKontabilitet();

            //dbKontabiliteti = new DbCore.DbKontabiliteti.clsDatabaseKontabilitet();
            //DbCore.DbKontabiliteti.colGrupeKontabilizimi colGrupe = dbKontabiliteti.merrGjitheGrupetKontabilizimi(DbCore.clsFunksione.merrIdNdermarrjeSesioni(Session));
            colGrupeKontabilizimi colGrupe = new colGrupeKontabilizimi(idNdermarrje);
            colGrupe.Insert(0, new clsGrupKontabilizimi(0, "", "", 0, 0));
            combo.DataSource = colGrupe;
            combo.TextField = "NrGrupKontabilizimi";
            combo.ValueField = "IdGrupKontabilizimi";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me gjendje te komponenteve te pageses
        /// </summary>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboGjendja(ASPxComboBox combo)
        {

            combo.Items.Add(MessagesResource.Messages["Aktive"], true);
            combo.Items.Add(MessagesResource.Messages["Inaktive"], false);
            combo.SelectedIndex = 0;
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        }

        public static void mbushComboDorezuar(ASPxComboBox combo)
        {
            combo.Items.Add("Po", 1);
            combo.Items.Add("Jo, por u pagua", 2);
            combo.Items.Add("Jo, nuk u pagua", 3);
            combo.SelectedIndex = 3;
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me gjuhet
        /// </summary>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboGjuha(ASPxComboBox combo)
        {
            combo.Items.Add("Shqip", 0);
            combo.Items.Add("Anglisht", 1);
            combo.DataBind();
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me modelet e infos se artikullit
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboInfoArtikulli(int idNdermarrje, ASPxComboBox combo, int lloji)
        {
            DataTable dt = clsInfoKoka.ktheInfoPerGrideDheLlojit(idNdermarrje, lloji);
            object[] vlerat = { 0, "JO", "", 0, 0, 0, 0 };

            dt.Rows.Add(vlerat);
            combo.DataSource = dt;
            combo.ValueField = "IdInfoKoka";
            combo.TextField = "Kodi";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
            dt.Dispose();
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me kategorite e dokumentave
        /// </summary>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboKategori(ASPxComboBox combo, int idsuperkat)
        {
            DbCore.DbRegjistrim.colKategoriNiveleDok colKategori = new DbCore.DbRegjistrim.colKategoriNiveleDok();
            DbCore.DbRegjistrim.clsKategoriNivelDok oKategori = new DbCore.DbRegjistrim.clsKategoriNivelDok();
            colKategori = oKategori.merriTeGjithePaSipasSuperKat(idsuperkat); //nuk e marr parasysh ne SP ndermarjen mqs kategorite nuk jane ne nivel ndermarje
            combo.DataSource = colKategori;
            combo.ValueField = "IdKategori";
            combo.TextField = "Pershkrimi";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me kategorite e dokumentave
        /// </summary>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboKategoriPerFormatNumrash(ASPxComboBox combo, int idViti)
        {
            DbCore.DbRegjistrim.colKategoriNiveleDok colKategori = new DbCore.DbRegjistrim.colKategoriNiveleDok();
            colKategori.Add(new DbCore.DbRegjistrim.clsKategoriNivelDok(0, "", 0, 0, false, 0, false));
            colKategori.mbushKategoriNivelDokPerFormatNumrash();
            combo.DataSource = colKategori;
            combo.ValueField = "IdKategori";
            combo.TextField = "Pershkrimi";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        public static void mbushComboKonfigurimImporti(ASPxComboBox combo, int idNdermarje, int idViti, int idPerdoruesi, string komponente)
        {
            var col = clsKonfigImporti.KtheKonfigImportiNdermarrjesSipasTeDrejtave(idNdermarje, idViti, idPerdoruesi, komponente);
            col.Insert(0, new clsKonfigImporti("", "", 0, 0, "", 0, 0, 0, "", "", false, "", false, false, false, "", "", "", false, 0));
            combo.DataSource = col;
            combo.ValueField = "Id";
            combo.TextField = "Emer";
            combo.DataBind();
            combo.DropDownStyle = DropDownStyle.DropDown;
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me kategorite e detajimeve
        /// </summary>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboKategoriDetajimesh(ASPxComboBox combo)
        {
            colKategoriDetajimArtikulli katDetArt = new colKategoriDetajimArtikulli();
            katDetArt.mbushGjitheKategoriteDetajimit();
            katDetArt.shtoKategoriDetajimiNeIndeksin(0, new clsKategoriDetajimArtikulli());
            combo.DataSource = katDetArt;
            combo.ValueField = "IdKategoriDetajimi";
            combo.TextField = "KodiKategoriDetajimi";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me klasat e artikujve
        /// </summary>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboKlasa(ASPxComboBox combo, bool selekto)
        {//mbush combon e klasave
            combo.TextField = "PershkrimKlasa";
            combo.ValueField = "IdKlasa";
            colKlasaArtikulli klasaArt = new colKlasaArtikulli();
            klasaArt.mbushGjitheKlasaArtikulli();
            combo.DataSource = klasaArt;
            //combo.DataSource = new DbCore.DbInventari.clsDatabaseInventari().merrGjitheKlasaArtikulli();
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;

            combo.DataBind();
            if (selekto) combo.SelectedIndex = 0;
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me klasat e artikujve
        /// </summary>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboKlasaArtikullit(ASPxComboBox combo)
        {//mbush combon e klasave
            combo.TextField = "PershkrimKlasa";
            combo.ValueField = "IdKlasa";
            colKlasaArtikulli klasaArt = new colKlasaArtikulli();
            klasaArt.mbushGjitheKlasaArtikulli();
            clsKlasaArtikulli klasaBosh = new clsKlasaArtikulli(0, "");
            klasaArt.Insert(0, klasaBosh);
            combo.DataSource = klasaArt;
            //combo.DataSource = new DbCore.DbInventari.clsDatabaseInventari().merrGjitheKlasaArtikulli();
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;

            combo.DataBind();
            combo.SelectedIndex = 0;
        }

        public static void mbushComboLlojKosto(ASPxComboBox combo, CultureInfo ci)
        {
            DataTable dt = null;
            using (clsDatabaseInventari dbInv = new clsDatabaseInventari())
            {
                dt = dbInv.merrLlojKosto();
            }
            combo.DataSource = dt;
            combo.ValueField = "GRUPKOSTO";
            combo.TextField = ci.Name == "sq-AL" ? "GRKPERSHK_AL" : "GRKPERSHK_EN";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
            if (combo.Items.Count != 0)
                combo.Items[0].Selected = true;
            dt.Dispose();
        }

        public static void mbushComboModelAutomjetiByID(ASPxComboBox combo, int idModelAuto)
        {
            DataTable dt = clsModelAutomjeti.ktheModelAutomjetiSipasIdDt(idModelAuto);
            combo.DataSource = dt;
            combo.ValueField = "IdModeli";
            combo.TextField = "PershkrimModeli";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
            if (combo.Items.Count != 0)
                combo.Items[0].Selected = true;
        }

        public static void mbushComboDegaAdminByID(ASPxComboBox combo, int idDega)
        {
            DataTable dt = DbCore.DbRegjistrim.clsDegeAdministrative.ktheDegeAdministrativeSipasIDDt(idDega);
            combo.DataSource = dt;
            combo.ValueField = "IdDegeAdministrative";
            combo.TextField = "Kodi";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
            if (combo.Items.Count != 0)
                combo.Items[0].Selected = true;
        }

        public static void mbushComboDegeAdministrative(string filter, long startIndex, long endIndex, int idNdermarrje, ASPxComboBox combo)
        {
            DataTable dt = new DataTable();
            dt = DbCore.DbRegjistrim.colDegeAdministrative.merrDegeAdministrativeSipasNdermarrjesPerKombo(filter, startIndex, endIndex, idNdermarrje);
            combo.DataSource = dt;
            combo.ValueField = "IdDegeAdministrative";
            combo.TextField = "Kodi";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
            dt.Dispose();
        }

        public static void mbushComboAutomjetiByID(ASPxComboBox combo, int idAuto)
        {
            ImbLogger.LogTraceShitje("Filloi metoda mbushComboAutomjetiByID");
            DataTable dt = clsAutomjete.ktheAutomjetSipasIdDt(idAuto);
            combo.DataSource = dt;
            combo.ValueField = "IdAutomjeti";
            combo.TextField = "NrShasie";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
            if (combo.Items.Count != 0)
                combo.Items[0].Selected = true;
            ImbLogger.LogTraceShitje("Mbaroi metoda mbushComboAutomjetiByID");
        }

        public static void mbushComboModelAutomjetesh(string filter, long startIndex, long endIndex, int idNdermarrje, ASPxComboBox combo)
        {
            DataTable dt = new DataTable();
            dt = colModeleAutomjetesh.merrModeleAutomjeteshSipasNdermarrjesPerKombo(filter, startIndex, endIndex, idNdermarrje);
            combo.DataSource = dt;
            combo.ValueField = "IdModeli";
            combo.TextField = "PershkrimModeli";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
            dt.Dispose();
        }

        public static void mbushComboAutomjetesh(int idNdermarrje, ASPxComboBox combo, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = colAutomjete.merrAutomjetetSipasNdermarrjesPerKombo(idNdermarrje, e.Filter, e.BeginIndex, e.EndIndex);

            IEnumerable<DataRow> d = dt.Select($"NrShasie like '%{e.Filter}%'").AsEnumerable().Skip(e.BeginIndex).Take(e.EndIndex - e.BeginIndex + 1);
            combo.DataSource = d.GetDataTable(dt);
            combo.ValueField = "IdAutomjeti";
            combo.TextField = "NrShasie";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
            dt.Dispose();
        }

        public static void mbushComboAutomjete(int idNdermarrje, ASPxComboBox combo)
        {
            DataTable dt = new DataTable();
            dt = colAutomjete.merrAutomjetetSipasNdermarrjes(idNdermarrje);
            combo.DataSource = dt;
            combo.ValueField = "IdAutomjeti";
            combo.TextField = "NrShasie";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
            dt.Dispose();
        }

        public static void mbushComboAutomjeteshMeKlient(int idNdermarrje, int idKlienti, ASPxComboBox combo, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = colAutomjete.merrAutomjetetSipasNdermarrjesDheKlientitPerKombo(idNdermarrje, idKlienti, e.Filter, e.BeginIndex, e.EndIndex);
            IEnumerable<DataRow> d = dt.Select($"NrShasie like '%{e.Filter}%'").AsEnumerable().Skip(e.BeginIndex).Take(e.EndIndex - e.BeginIndex + 1);
            combo.DataSource = d.GetDataTable(dt);
            combo.ValueField = "IdAutomjeti";
            combo.TextField = "NrShasie";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
            dt.Dispose();
        }

        public static void mbushComboLlojiSubjekti(ASPxComboBox combo)
        {
            combo.Items.Add("Klient", 1);
            combo.Items.Add("Furnitor", 2);
            combo.Items.Add("Llogari", 3);
            combo.Items.Add("Punonjes", 4);
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me klient furnitoret
        /// </summary>
        /// <param name="idPerdorues"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        /// <param name="kf">1-klient, 2-furnitor,0-te gjitha</param>
        public static void mbushComboKlientFurnitori(int idPerdorues, int idNdermarrje, ASPxComboBox combo, int kf, bool meProspekt = false)
        {
            DataTable dt = merrDataTablePerKlientFurnitor(idPerdorues, idNdermarrje, kf, meProspekt);
            combo.DataSource = dt;
            combo.ValueField = "IdKlientFurnitor";
            combo.TextField = "KodKlientFurnitor";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
            dt.Dispose();
        }

        public static DataTable merrDataTablePerKlientFurnitor(int idPerdorues, int idNdermarrje, int kf, bool meProspekt = false)
        {
            DataTable dt = new DataTable();
            if (kf == 1)
                dt = colKlienteFurnitore.mbushKlienteOseFurnitore(true, idNdermarrje, idPerdorues, meProspekt);
            else if (kf == 2)
                dt = colKlienteFurnitore.mbushKlienteOseFurnitore(false, idNdermarrje, idPerdorues, meProspekt);
            else
                dt = colKlienteFurnitore.merrSipasKFNdermarrjesAndAutorizimeDT(idNdermarrje, idPerdorues);

            return dt;
        }

        public static void ShtoKolonaPerKartaKlienti(ASPxComboBox combo)
        {
            combo.TextFormatString = "{0} ({1})";
            ListBoxColumn colprove = new ListBoxColumn();
            colprove.FieldName = "Kodi";
            colprove.Caption = "Kodi";
            ListBoxColumn colemer = new ListBoxColumn();
            colemer.FieldName = "Emri";
            colemer.Caption = "Emertimi";
            combo.Columns.Clear();
            combo.Columns.Add(colprove);
            combo.Columns.Add(colemer);
            combo.ValueField = "IdKarta";
        }

        public static void shtoKolonaPerGrupim(ASPxComboBox combo)
        {
            combo.TextFormatString = "{0}";
            ListBoxColumn colKodi = new ListBoxColumn();
            colKodi.FieldName = "Kodi";
            colKodi.Caption = "Kodi";
            ListBoxColumn colPershk = new ListBoxColumn();
            colPershk.FieldName = "Pershkrimi";
            colPershk.Caption = "Pershkrimi";
            combo.Columns.Clear();
            combo.Columns.Add(colKodi);
            combo.Columns.Add(colPershk);
            combo.ValueField = "IdGrupimKoka";
        }

        public static void shtoKolonaPerMagazina(ASPxComboBox combo)
        {
            combo.TextFormatString = "{0} ({1})";
            ListBoxColumn colprove = new ListBoxColumn();
            colprove.FieldName = "Kodi";
            colprove.Caption = "Kodi";
            ListBoxColumn colemer = new ListBoxColumn();
            colemer.FieldName = "Pershkrimi";
            colemer.Caption = "Pershkrimi";
            combo.Columns.Clear();
            combo.Columns.Add(colprove);
            combo.Columns.Add(colemer);
            combo.ValueField = "IdNjesiAdministrative";
        }

        public static void shtoKolonaPerArkaBanka(ASPxComboBox combo)
        {
            combo.TextFormatString = "{0} ({1})";
            ListBoxColumn colprove = new ListBoxColumn();
            colprove.FieldName = "KodiBanka";
            colprove.Caption = "Kodi";
            ListBoxColumn colemer = new ListBoxColumn();
            colemer.FieldName = "EmerBanka";
            colemer.Caption = "Emri";
            combo.Columns.Clear();
            combo.Columns.Add(colprove);
            combo.Columns.Add(colemer);
            combo.ValueField = "IdBanka";
        }

        public static void shtoKolonaPerNivelCmimi(ASPxComboBox combo)
        {
            combo.TextFormatString = "{0} ({1})";
            ListBoxColumn colprove = new ListBoxColumn();
            colprove.FieldName = "KODNIVELCMIMI";
            colprove.Caption = "Kodi";
            ListBoxColumn colemer = new ListBoxColumn();
            colemer.FieldName = "PERSHKRIMNIVELCMIMI";
            colemer.Caption = "Emertimi";
            combo.Columns.Clear();
            combo.Columns.Add(colprove);
            combo.Columns.Add(colemer);
        }

        public static void shtoKolonaPerKlientFurnitor(ASPxComboBox combo)
        {
            combo.TextFormatString = "{0}";
            ListBoxColumn colprove = new ListBoxColumn();
            colprove.FieldName = "KodKlientFurnitor";
            colprove.Caption = "Kodi";
            ListBoxColumn colemer = new ListBoxColumn();
            colemer.FieldName = "EmertimiKF";
            colemer.Caption = "Emri";
            combo.Columns.Add(colprove);
            combo.Columns.Add(colemer);
        }

        public static void shtoKolonaPerDegeAdministrative(ASPxComboBox combo)
        {
            combo.TextFormatString = "{0}";
            ListBoxColumn colprove = new ListBoxColumn();
            colprove.FieldName = "Kodi";
            colprove.Caption = "Kodi";
            ListBoxColumn colemer = new ListBoxColumn();
            colemer.FieldName = "Pershkrimi";
            colemer.Caption = "Pershkrimi";
            combo.Columns.Add(colprove);
            combo.Columns.Add(colemer);
        }

        public static void shtoKolonaPerModelAutomjeti(ASPxComboBox combo)
        {
            combo.TextFormatString = "{0} ({1})";
            ListBoxColumn colprove = new ListBoxColumn();
            colprove.FieldName = "KodModeli";
            colprove.Caption = "Kodi";
            ListBoxColumn colemer = new ListBoxColumn();
            colemer.FieldName = "PershkrimModeli";
            colemer.Caption = "Pershkrimi";
            combo.Columns.Add(colprove);
            combo.Columns.Add(colemer);
        }

        public static void shtoKolonaPerCombo(ASPxComboBox combo, string cmbTextFormatString, string firstFieldName, string firstCaption, bool meSecondCol, string secondFieldName, string secondCaption, bool meHiddenIdCol, string idFieldName)
        {
            combo.TextFormatString = cmbTextFormatString;
            combo.Columns.Clear();

            ListBoxColumn firstCol = new ListBoxColumn();
            firstCol.FieldName = firstFieldName;
            firstCol.Caption = firstCaption;
            combo.Columns.Add(firstCol);

            if (meSecondCol)
            {
                ListBoxColumn secondCol = new ListBoxColumn();
                secondCol.FieldName = secondFieldName;
                secondCol.Caption = secondCaption;
                combo.Columns.Add(secondCol);
            }

            if (meHiddenIdCol)
            {
                ListBoxColumn idCol = new ListBoxColumn();
                idCol.FieldName = idFieldName;
                idCol.Visible = false;
                combo.Columns.Add(idCol);
            }
        }

        public static void shtoKolonaPerCombo(ASPxComboBox combo, string cmbTextFormatString, string[] visibleFields, string[] hiddenFields)
        {
            combo.TextFormatString = cmbTextFormatString;
            combo.Columns.Clear();

            for (int i = 0; i < visibleFields.Length; i++)
            {
                ListBoxColumn col = new ListBoxColumn();
                col.FieldName = visibleFields[i].Split(';')[0];
                col.Caption = visibleFields[i].Split(';')[1];
                combo.Columns.Add(col);
            }
            for (int i = 0; i < hiddenFields.Length; i++)
            {
                ListBoxColumn col = new ListBoxColumn();
                col.FieldName = hiddenFields[i];
                col.Visible = false;
                combo.Columns.Add(col);
            }

        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me KMSH
        /// </summary>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboKMSH(ASPxComboBox combo)
        {//mbush kombon e llogaritjesh KMSH
            combo.Items.Add("Periodike", 1);
            combo.Items.Add("Vazhduar", 2);
            combo.SelectedIndex = 1;
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me kodifikimet e artikullit
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboKodifikim(int idNdermarrje, ASPxComboBox combo, int grupi, bool llojartikulli, bool shtoRreshtBosh)
        {
            //clsFunksione funk = new DbCore.clsFunksione();
            //DbCore.DbInventari.clsDatabaseInventari dbInventari = new DbCore.DbInventari.clsDatabaseInventari();

            colKodifikimeArtikulli kodifikimet = new colKodifikimeArtikulli();
            if (shtoRreshtBosh)
                kodifikimet.Add(new clsKodifikimArtikulli());
            kodifikimet.mbushGjitheKodifikimetArtikulliSipasNdermarrjesJoPrindDheLlojit(idNdermarrje, grupi, llojartikulli);
            //kodifikimet = dbInventari.merrGjitheKodifikimetArtikulliSipasNdermarrjesJoPrind(DbCore.clsFunksione.merrIdNdermarrjeSesioni(Session));

            combo.DataSource = kodifikimet;
            combo.TextField = "KodKodifikimi";
            combo.ValueField = "IdKodifikimi";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        public static void mbushComboKodifikimPrind(int idNdermarrje, ASPxComboBox combo, int grupi, bool llojartikulli, bool shtoRreshtBosh)
        {
            colKodifikimeArtikulli kodifikimet = new colKodifikimeArtikulli();
            if (shtoRreshtBosh)
                kodifikimet.Add(new clsKodifikimArtikulli());
            kodifikimet.merrKodifikimArtikulliSipasLlojit(grupi, idNdermarrje, llojartikulli);
            combo.DataSource = kodifikimet;
            combo.TextField = "KodKodifikimi";
            combo.ValueField = "IdKodifikimi";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        public static void mbushComboKodifikimNiveli1(int idNdermarrje, ASPxComboBox combo, int grupi, bool llojartikulli)
        {
            //clsFunksione funk = new DbCore.clsFunksione();
            //DbCore.DbInventari.clsDatabaseInventari dbInventari = new DbCore.DbInventari.clsDatabaseInventari();

            colKodifikimeArtikulli kodifikimet = new colKodifikimeArtikulli();
            kodifikimet.mbushGjitheKodifikimetArtikulliSipasNdermarrjesLlojitNiveli1(idNdermarrje, grupi, llojartikulli);
            //kodifikimet = dbInventari.merrGjitheKodifikimetArtikulliSipasNdermarrjesJoPrind(DbCore.clsFunksione.merrIdNdermarrjeSesioni(Session));

            combo.DataSource = kodifikimet;
            combo.TextField = "KodKodifikimi";
            combo.ValueField = "IdKodifikimi";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        public static void shtokolonakodifikime(ASPxComboBox combo)
        {
            combo.TextFormatString = "{0}";
            ListBoxColumn colprove = new ListBoxColumn();
            colprove.FieldName = "KodKodifikimi";
            ListBoxColumn colemer = new ListBoxColumn();
            colemer.FieldName = "PershkrimKodifikimi";
            ListBoxColumn colNiveli = new ListBoxColumn();
            colNiveli.FieldName = "NivelKodifikimi";
            combo.Columns.Add(colprove);
            combo.Columns.Add(colemer);
            combo.Columns.Add(colNiveli);
        }

        public static void shtoKolonaKategoriShpenzimi(ASPxComboBox combo, ResourceManager rm, CultureInfo ci)
        {
            combo.TextFormatString = "{0}";
            ListBoxColumn colprove = new ListBoxColumn();
            colprove.FieldName = "Kodi";
            colprove.Caption = rm.GetString("cmbCmimeArtikulliCaptionKodi", ci);
            ListBoxColumn colemer = new ListBoxColumn();
            colemer.FieldName = "Pershkrimi";
            colemer.Caption = rm.GetString("cmbCmimeArtikulliCaptionPershkrimi", ci);
            ListBoxColumn colNiveli = new ListBoxColumn();
            colNiveli.FieldName = "NivelKategorie";
            colNiveli.Caption = rm.GetString("lblNivel", ci);
            combo.DropDownStyle = DropDownStyle.DropDownList;
            combo.Columns.Add(colprove);
            combo.Columns.Add(colemer);
            combo.Columns.Add(colNiveli);
        }

        public static void mbushComboKodifikimGjitha(int idNdermarrje, ASPxComboBox combo, int grupi, bool llojartikulli)
        {
            //clsFunksione funk = new DbCore.clsFunksione();
            //DbCore.DbInventari.clsDatabaseInventari dbInventari = new DbCore.DbInventari.clsDatabaseInventari();

            colKodifikimeArtikulli kodifikimet = new colKodifikimeArtikulli();
            kodifikimet.Add(new clsKodifikimArtikulli());
            kodifikimet.merrKodifikimArtikulliSipasLlojit(grupi, idNdermarrje, llojartikulli);
            //kodifikimet = dbInventari.merrGjitheKodifikimetArtikulliSipasNdermarrjesJoPrind(DbCore.clsFunksione.merrIdNdermarrjeSesioni(Session));
            combo.DataSource = kodifikimet;
            //combo.DropDownStyle = DropDownStyle.DropDownLis
            combo.TextField = "PershkrimKodifikimi";
            combo.ValueField = "IdKodifikimi";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.StartsWith;
            combo.DataBind();
        }

        public static void mbushComboKodifikimGjitha(int idNdermarrje, ASPxComboBox combo, int grupi, bool llojartikulli, string filter)
        {
            colKodifikimeArtikulli kodifikimet = new colKodifikimeArtikulli();
            kodifikimet.merrKodifikimArtikulliSipasLlojit(grupi, idNdermarrje, llojartikulli);
            combo.DataSource = kodifikimet.Where(x => x.PershkrimKodifikimi.IndexOf(filter, StringComparison.InvariantCultureIgnoreCase) > -1);
            combo.TextField = "PershkrimKodifikimi";
            combo.ValueField = "IdKodifikimi";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.StartsWith;
            combo.DataBind();
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me konfigurimet sipas kategorise
        /// </summary>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        /// <param name="kat"> id e kategorise</param>
        public static void mbushComboKonfigurimeshSipasKategorise(int idPerdoruesi, int idNdermarrje, ASPxComboBox combo, int kat, ResourceManager rm, CultureInfo ci, int idGjuha)
        {//mbush griden e popupit me te dhena
            colKonfigurimAmbjenti col = new colKonfigurimAmbjenti();

            if (kat != 0)
                col.mbushKonfigAmbjSipasIdKategori(kat, idNdermarrje, idPerdoruesi, idGjuha);
            else
                col.mbushGjitheKonfigurimeAmbjentesh(idNdermarrje, idPerdoruesi, 1, idGjuha); //celje

            konfiguroTemplateComboKonfigurimesh(combo, col);
        }

        public static void mbushComboKonfigurimeshSipasIdKonfigurimi(ASPxComboBox combo, int idKonfigurimi)
        {//mbush griden e popupit me te dhena
            colKonfigurimAmbjenti col = new colKonfigurimAmbjenti();
            col.Add(new clsKonfigurimAmbjenti(idKonfigurimi));
            konfiguroTemplateComboKonfigurimesh(combo, col);
        }


        public static void konfiguroTemplateComboKonfigurimesh(ASPxComboBox combo, colKonfigurimAmbjenti col)
        {//mbush griden e popupit me te dhena
            ListBoxColumn colprove = new ListBoxColumn();
            colprove.FieldName = "KodKonfigAmbjente";
            colprove.Caption = MessagesResource.Messages["cmbCmimeArtikulliCaptionKodi"];
            ListBoxColumn colemer = new ListBoxColumn();
            colemer.FieldName = "PershkrimKonfigAmbjente";
            colemer.Caption = MessagesResource.Messages["cmbCmimeArtikulliCaptionPershkrimi"];
            combo.TextFormatString = "{0}";
            combo.TextField = "KodKonfigAmbjente";
            if (combo.Columns.Count != 0)
            {
                combo.Columns.Clear();
            }
            combo.Columns.Add(colprove);
            combo.Columns.Add(colemer);
            combo.DataSource = col;
            combo.ValueField = "IdKonfigAmbjente";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }



        public static void mbushComboKonfigurimeshSipasKategorise(int idPerdoruesi, int idNdermarrje, ASPxComboBox combo, int kat, string kodNiveli, ResourceManager rm, CultureInfo ci, int idGjuha)
        {//mbush griden e popupit me te dhena
            int idNiveli = DbCore.DbRegjistrim.clsNivelRegjistrimi.ktheIdNivelRegjistrimiSipasKodi(kodNiveli, idNdermarrje);
            mbushComboKonfigurimeshSipasKategoriseDheNivelit(idPerdoruesi, idNdermarrje, combo, kat, idNiveli, rm, ci, idGjuha);
        }

        public static void mbushComboKonfigurimeshSipasKategoriseDheNivelit(int idPerdoruesi, int idNdermarrje, ASPxComboBox combo, int kat, int idNiveli, ResourceManager rm, CultureInfo ci, int idGjuha)
        {
            colKonfigurimAmbjenti col = new colKonfigurimAmbjenti();
            clsKonfigurimAmbjenti konf = new clsKonfigurimAmbjenti();

            if (kat != 0)
            {
                konf.IdKategori = kat;
                konf.IdNdermarje = idNdermarrje;
                col.mbushKonfigAmbjSipasIdKategoriIdNivel(konf.IdKategori, idNiveli, idPerdoruesi, idGjuha, true);
            }
            else
                col.mbushGjitheKonfigurimeAmbjentesh(idNdermarrje, idPerdoruesi, 1, idGjuha);

            konfiguroTemplateComboKonfigurimesh(combo, col);
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me konfigurimet sipas kategorise
        /// </summary>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        /// <param name="kat"> id e kategorise</param>
        public static void mbushComboKonfigurimeshSipasKategorisePaKolona(int idPerdoruesi, int idNdermarrje, ASPxComboBox combo, int kat, int idsuperkat, int idGjuha)
        {//mbush griden e popupit me te dhena
            colKonfigurimAmbjenti col = new colKonfigurimAmbjenti();
            //if (Request.QueryString.ToString() == "")
            //{
            clsKonfigurimAmbjenti konf = new clsKonfigurimAmbjenti();
            if (kat != 0)
            {
                konf.IdKategori = kat;
                konf.IdNdermarje = idNdermarrje;
                col.mbushKonfigAmbjSipasIdKategori(konf.IdKategori, konf.IdNdermarje, idPerdoruesi, idGjuha);
                //col = dbShare.merrKonfigAmbjSipasIdKategori(konf, DbCore.clsFunksione.ktheIdPerdoruesi(Session));
            }
            else
            {
                col.mbushGjitheKonfigurimeAmbjentesh(idNdermarrje, idPerdoruesi, idsuperkat, idGjuha);
                //col = dbShare.merrGjitheKonfigurimeAmbjentesh(DbCore.clsFunksione.merrIdNdermarrjeSesioni(Session), DbCore.clsFunksione.ktheIdPerdoruesi(Session));
            }

            combo.DataSource = col;
            combo.ValueField = "IdKonfigAmbjente";
            combo.TextField = "KodKonfigAmbjente";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }
        /// <summary>
        /// perdoret per te mbushur nje combobox  me konfigurimet sipas komponentes
        /// </summary>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        /// <param name="komponente"> id e komponentes</param>
        public static void mbushComboKonfigurimet(int idPerdoruesi, int idNdermarrje, ASPxComboBox combo, int komponente, bool mod, int idGjuha)
        {
            colKonfigurimAmbjenti colKonfig = new colKonfigurimAmbjenti();
            colKonfig.mbushGjitheKonfigurimetKomponentes(komponente, idNdermarrje, idPerdoruesi, idGjuha);
            colKonfigurimAmbjenti konfVarura = new colKonfigurimAmbjenti();
            foreach (clsKonfigurimAmbjenti konfi in colKonfig)
            {
                var alternativa = clsAlternativaKushti.getAlternativa(konfi.IdKonfigAmbjente, "V");
                if (alternativa == "Po" && !mod)
                    konfVarura.Add(konfi);
            }
            foreach (clsKonfigurimAmbjenti konfi in konfVarura)
            {
                colKonfig.Remove(konfi);
            }

            ListBoxColumn colprove = new ListBoxColumn();
            colprove.FieldName = "KodKonfigAmbjente";
            colprove.Caption = MessagesResource.Messages["cmbCmimeArtikulliCaptionKodi"];
            ListBoxColumn colemer = new ListBoxColumn();
            colemer.FieldName = "PershkrimKonfigAmbjente";
            colemer.Caption = MessagesResource.Messages["cmbCmimeArtikulliCaptionPershkrimi"];
            colemer.Width = 250;
            combo.TextFormatString = "{0};{1}";
            combo.Columns.Add(colprove);
            combo.Columns.Add(colemer);

            combo.DataSource = colKonfig;
            combo.ValueField = "IdKonfigAmbjente";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }
        /// <summary>
        /// perdoret per te mbushur nje combobox  me konfigurimet sipas komponentes
        /// </summary>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        /// <param name="komponente"> id e komponentes</param>
        public static void mbushComboKonfigurimetVetemKodiSiTekst(int idPerdoruesi, int idNdermarrje, ASPxComboBox combo, int komponente, bool mod, int idGjuha)
        {
            colKonfigurimAmbjenti colKonfig = new colKonfigurimAmbjenti();
            colKonfig.mbushGjitheKonfigurimetKomponentes(komponente, idNdermarrje, idPerdoruesi, idGjuha);
            colKonfigurimAmbjenti konfVarura = new colKonfigurimAmbjenti();
            foreach (clsKonfigurimAmbjenti konfi in colKonfig)
            {
                var alternativa = clsAlternativaKushti.getAlternativa(konfi.IdKonfigAmbjente, "V");
                if (alternativa == "Po" && !mod)
                    konfVarura.Add(konfi);
            }
            foreach (clsKonfigurimAmbjenti konfi in konfVarura)
            {
                colKonfig.Remove(konfi);
            }
            konfiguroTemplateComboKonfigurimesh(combo, colKonfig);
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me kpf(llogarite standarte)
        /// </summary>
        /// <param name="idPerdoruesi"></param>
        /// <param name="name"></param>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        /// <param name="grup"> grupi i kpf</param>
        /// <example> 1,2,3</example>
        public static void mbushComboKPF(int idPerdoruesi, int idNdermarrje, ASPxComboBox combo, int grup)
        {
            DataTable dt = colKPFte.merrSipasKPFNdermarrjesAndAutorizimeDTGrupit(grup, idNdermarrje, idPerdoruesi);
            combo.DataSource = dt;// colKPF;
            combo.TextFormatString = "{0};{1};{2}";

            ListBoxColumn colprove = new ListBoxColumn();
            colprove.FieldName = "KodiKPF";
            ListBoxColumn colemer = new ListBoxColumn();
            colemer.FieldName = "EmertimiKPF";
            ListBoxColumn colmonedha = new ListBoxColumn();
            colmonedha.FieldName = "NiveliKPF";

            combo.Columns.Add(colprove);
            combo.Columns.Add(colemer);
            combo.Columns.Add(colmonedha);
            combo.TextField = "KodiKPF";
            combo.ValueField = "IdKPF";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
            dt.Dispose();
        }

        public static void mbushComboKPFMeKolona(int idPerdorues, int idNdermarrje, ASPxComboBox combo, int grup, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (e == null || String.IsNullOrWhiteSpace(e.Filter)) return;
            DataTable dt = colKPFte.merrSipasKPFNdermarrjesAndAutorizimeDTGrupit(grup, idNdermarrje, idPerdorues);
            IEnumerable<DataRow> d = dt.Select($"KodiKPF like '%{e.Filter}%' or EmertimiKPF like '%{e.Filter}%' ").AsEnumerable().Skip(e.BeginIndex).Take(e.EndIndex - e.BeginIndex + 1);
            combo.DataSource = d.GetDataTable(dt);

            combo.TextField = "KodiKPF";
            combo.ValueField = "IdKPF";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();


        }

        public static void mbushComboKPFMeKolona(int idPerdorues, int idNdermarrje, int grup, ASPxComboBox combo, ListEditItemRequestedByValueEventArgs e)
        {
            if (e == null || e.Value == null || String.IsNullOrWhiteSpace(e.Value.ToString())) return;
            DataTable dt = colKPFte.merrSipasKPFNdermarrjesAndAutorizimeDTGrupit(grup, idNdermarrje, idPerdorues);
            combo.DataSource = dt.Select($"IdKPF = {e.Value}").GetDataTable(dt);

            combo.TextField = "KodiKPF";
            combo.ValueField = "IdKPF";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
            dt.Dispose();
        }

        public static void mbushKomboPerdoruesish(int idNdermarrje, ASPxComboBox combo)
        {
            DataTable dt = DbCore.DbAdmin.colPerdoruesit.merrPerdoruesitSipasLicencesDTPerNdermaje(idNdermarrje);
            combo.DataSource = dt;
            combo.TextField = "PerdoruesUsername";
            combo.ValueField = "IdPerdorues";
            combo.DataBind();
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me kpf(llogarite standarte)
        /// </summary>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        /// <param name="grup"> grupi i kpf</param>
        /// <example> 1,2,3</example>
        public static void mbushComboKPFBij(int idPerdoruesi, int idNdermarrje, ASPxComboBox combo, int grup)
        {
            //DbCore.DbKontabiliteti.clsDatabaseKontabilitet dbKontabilitet = new DbCore.DbKontabiliteti.clsDatabaseKontabilitet();

            colKPFte colKPF = new colKPFte();
            //colKPF = dbKontabilitet.merrGjitheKPFteSipasGrupitPozitiveAndAutorizimeAktive(grup, funk.ktheNdermarrjeVit(), DbCore.clsFunksione.ktheIdPerdoruesi(Session));
            DataTable dt = colKPFte.mbushGjitheKPFteSipasGrupitPozitiveAndAutorizimeAktiveBij(grup, idNdermarrje, idPerdoruesi);
            combo.DataSource = dt;
            combo.TextField = "KodiKPF";
            combo.ValueField = "IdKPF";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
            dt.Dispose();
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me kpf(llogarite standarte)
        /// </summary>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>

        public static void shtoKPFPrind(ASPxComboBox combo)
        {
            combo.TextFormatString = "{0};{1};{2}";

            ListBoxColumn colprove = new ListBoxColumn();
            colprove.FieldName = "KodiKPF";
            ListBoxColumn colemer = new ListBoxColumn();
            colemer.FieldName = "EmertimiKPF";
            ListBoxColumn colmonedha = new ListBoxColumn();
            colmonedha.FieldName = "NiveliKPF";

            combo.Columns.Add(colprove);
            combo.Columns.Add(colemer);
            combo.Columns.Add(colmonedha);
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me llogarite
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="idPerdoruesi"></param>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboLlogaria(int idNdermarrje, int idPerdoruesi, ASPxComboBox combo, bool vetemNr = false)
        {
            //DbCore.DbKontabiliteti.clsDatabaseKontabilitet dbKontabilitet = new DbCore.DbKontabiliteti.clsDatabaseKontabilitet();

            DataTable dt = colLlogarite.merrSipasLlogariteNdermarrjesAndAutorizimeAktivDT(idNdermarrje, idPerdoruesi);
            // DbCore.DbKontabiliteti.colLlogarite colLlogarite = new DbCore.DbKontabiliteti.colLlogarite();
            //  colLlogarite.mbushLLogariteNdermarrjesAndAutorizime(DbCore.clsFunksione.merrIdNdermarrjeSesioni(Session), DbCore.clsFunksione.ktheIdPerdoruesi(Session));
            //colLlogarite = dbKontabilitet.merrLLogariteNdermarrjesAndAutorizime(DbCore.clsFunksione.merrIdNdermarrjeSesioni(Session), DbCore.clsFunksione.ktheIdPerdoruesi(Session));
            combo.DataSource = dt;// colLlogarite;
            combo.TextFormatString = vetemNr ? "{0}" : "{0};{1};{2}";

            ListBoxColumn colprove = new ListBoxColumn();
            colprove.FieldName = "NrLlogari";
            ListBoxColumn colemer = new ListBoxColumn();
            colemer.FieldName = "EmerLlogari1";
            ListBoxColumn colmonedha = new ListBoxColumn();
            colmonedha.FieldName = "PershkrimiMonedha";

            combo.Columns.Add(colprove);
            combo.Columns.Add(colemer);
            combo.Columns.Add(colmonedha);
            combo.TextField = "NrLlogari";
            combo.ValueField = "IdLlogari";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;

            combo.DataBind();
            dt.Dispose();
        }

        public static void mbushComboLlogariaPaKolona(int idPerdorues, int idNdermarrje, ASPxComboBox combo)
        {
            DataTable dt = colLlogarite.merrSipasLlogariteNdermarrjesAndAutorizimeAktivDT(idNdermarrje, idPerdorues);
            combo.DataSource = dt;
            combo.TextField = "NrLlogari";
            combo.ValueField = "IdLlogari";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
            dt.Dispose();
        }

        public static void mbushComboLlogariaPaKolona(int idPerdorues, int idNdermarrje, ASPxComboBox combo, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (e == null || String.IsNullOrWhiteSpace(e.Filter)) return;
            DataTable dt = colLlogarite.merrSipasLlogariteNdermarrjesAndAutorizimeAktivDT(idNdermarrje, idPerdorues);
            IEnumerable<DataRow> d = dt.Select($"NrLlogari like '%{e.Filter}%' or EmerLlogari1 like '%{e.Filter}%' or EmerLlogari2 like '%{e.Filter}%' ").AsEnumerable().Skip(e.BeginIndex).Take(e.EndIndex - e.BeginIndex + 1);
            combo.DataSource = d.GetDataTable(dt);
            combo.TextField = "NrLlogari";
            combo.ValueField = "IdLlogari";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();


        }

        public static void mbushComboArtikulli(int idPerdorues, int idNdermarrje, ASPxComboBox combo, ListEditItemsRequestedByFilterConditionEventArgs e, string postStringTvsh)
        {
            if (e == null || String.IsNullOrWhiteSpace(e.Filter)) return;
            DataTable dt = colArtikujt.merrSipasArtikujNdermarrjesAndAutorizimeDT(idNdermarrje, idPerdorues, false,false, postStringTvsh);
            IEnumerable<DataRow> d = dt.Select($"KodArtikulli like '%{e.Filter}%' or PershkrimArtikulli like '%{e.Filter}%' or PershkrimiAngArtikulli like '%{e.Filter}%' ").AsEnumerable().Skip(e.BeginIndex).Take(e.EndIndex - e.BeginIndex + 1);
            combo.DataSource = d.GetDataTable(dt);
            combo.TextField = "KodArtikulli";
            combo.ValueField = "IdArtikulli";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
            dt.Dispose();
        }

        public static void mbushComboLlogariaPaKolona(int idPerdorues, int idNdermarrje, ASPxComboBox combo, ListEditItemRequestedByValueEventArgs e)
        {
            if (e == null || e.Value == null || String.IsNullOrWhiteSpace(e.Value.ToString())) return;
            DataTable dt = colLlogarite.merrSipasLlogariteNdermarrjesAndAutorizimeAktivDT(idNdermarrje, idPerdorues);
            combo.DataSource = dt.Select($"IdLlogari = {e.Value}").GetDataTable(dt);
            combo.TextField = "NrLlogari";
            combo.ValueField = "IdLlogari";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
            dt.Dispose();
        }

        public static void mbushComboArtikulli(int idPerdorues, int idNdermarrje, ASPxComboBox combo, ListEditItemRequestedByValueEventArgs e, string postStringTvsh)
        {
            if (e == null || e.Value == null || String.IsNullOrWhiteSpace(e.Value.ToString())) return;
            DataTable dt = colArtikujt.merrSipasArtikujNdermarrjesAndAutorizimeDT(idNdermarrje, idPerdorues, false, false, postStringTvsh);


            combo.DataSource = dt.Select($"IdArtikulli = {e.Value}").GetDataTable(dt);
            combo.TextField = "KodArtikulli";
            combo.ValueField = "IdArtikulli";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
            dt.Dispose();
        }

        public static void shtoKolonaPerLlogarineSelectVetemNr(ASPxComboBox combo)
        {
            combo.TextFormatString = "{0}";

            ListBoxColumn colNrLlog = new ListBoxColumn();
            colNrLlog.FieldName = "NrLlogari";
            colNrLlog.Caption = "Nr. Llogarie";
            ListBoxColumn colEmer = new ListBoxColumn();
            colEmer.FieldName = "EmerLlogari1";
            colEmer.Caption = "Emer Llogarie";
            ListBoxColumn colMonedha = new ListBoxColumn();
            colMonedha.FieldName = "KodiMonedha";
            colMonedha.Caption = "Monedha";
            combo.Columns.Add(colNrLlog);
            combo.Columns.Add(colEmer);
            combo.Columns.Add(colMonedha);
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me llojin klient/furnitor
        /// </summary>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboLloji(ASPxComboBox combo)
        {//mbush combon e llojeve
            combo.Items.Add("Klient", true);
            combo.Items.Add("Furnitor", false);
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me llojet arka/banka
        /// </summary>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboLlojiBanka(ASPxComboBox combo)
        {
            combo.Items.Add("Banka", true);
            combo.Items.Add("Arka", false);
            combo.SelectedIndex = 0;
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me lloj ndermarje model/eksistuese
        /// </summary>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboLlojiNdermarje(ASPxComboBox combo)
        {
            combo.Items.Add("Model", 0);
            combo.Items.Add("Ekzistuese", 1);
            combo.SelectedIndex = 0;
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me llojet e modelit te fushave shtese
        /// </summary>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboLlojModeli(ASPxComboBox combo, bool meItemBosh)
        {//mbush kombon e llojit te modelit me te dhena nga databasa
            colLlojModeleshFushaShtese colLloji = new colLlojModeleshFushaShtese();
            if (meItemBosh)
                colLloji.Add(new clsLlojModeliFushaShtese());
            colLloji.mbushGjitheLlojModeleshFushaShtesePozitive();
            //DbCore.DbAdmin.colLlojModeleshFushaShtese colLloji = dbAdmin.merrGjitheLlojModeleshFushaShtesePozitive();
            combo.DataSource = colLloji;
            combo.TextField = "PershkrimiLlojModeliFushaShtese";
            combo.ValueField = "IdLlojModeliFushaShtese";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me lloj pagese
        /// </summary>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboLlojPagese(ASPxComboBox combo)
        {
            combo.Items.Add(LlojPagese.Mujore.ToString(), Convert.ToInt32(LlojPagese.Mujore));
            combo.Items.Add(LlojPagese.Ditore.ToString(), Convert.ToInt32(LlojPagese.Ditore));
            combo.Items.Add(LlojPagese.Orare.ToString(), Convert.ToInt32(LlojPagese.Orare));

            combo.SelectedIndex = 0;
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        }

        public static void mbushComboLlojQendre(ASPxComboBox combo, bool skema, ResourceManager rm, CultureInfo ci)
        {
            combo.Items.Add(rm.GetString("cmbVleraQendraKosto", ci), 1);
            if (skema)
                combo.Items.Add(rm.GetString("cmbVleraSkemaQendraKosto", ci), 2);
            combo.SelectedIndex = 0;
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        }

        public static void mbushComboLlojQendrePerImport(ASPxComboBox combo, bool skema, ResourceManager rm, CultureInfo ci)
        {
            combo.Items.Add(rm.GetString("cmbVleraQendraKosto", ci), 1);
            if (skema)
                combo.Items.Add(rm.GetString("cmbVleraSkemaQendraKosto", ci), 2);
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me magazinat
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboMagazinat(int idNdermarrje, ASPxComboBox combo, int idPerdorues, bool plotesodefault, int lloji, bool kushtMerrMagMeAutorizim)
        {
            DataTable dt;
            if (lloji == 0)
                dt = DbCore.DbRegjistrim.colNjesiAdministrative.merrSipasNjesiNdermarjePerLupeDege(idNdermarrje, idPerdorues, false, -1, kushtMerrMagMeAutorizim);
            else
                dt = DbCore.DbRegjistrim.colNjesiAdministrative.merrSipasNjesiNdermarjePerLupeDegeSipasLlojArtikulli(idNdermarrje, idPerdorues, lloji);
            combo.DataSource = dt;
            combo.ValueField = "IdNjesiAdministrative";
            combo.TextField = "Kodi";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
            if (plotesodefault && dt.Rows.Count == 1)
                combo.SelectedIndex = 1;
            dt.Dispose();
        }

        public static void mbushComboMagazinatMeID(int idNdermarrje, ASPxComboBox combo, int idPerdorues, bool plotesodefault, int lloji, bool kushtMerrMagMeAutorizim, int idNjesiAdministrative)
        {
            ImbLogger.LogTraceShitje("Filloi metoda mbushComboMagazinatMeID");
            DataTable dt;
            dt = DbCore.DbRegjistrim.colNjesiAdministrative.merrSipasNjesiNdermarjePerLupeDegeMeID(idNdermarrje, idPerdorues, false, -1, kushtMerrMagMeAutorizim, idNjesiAdministrative);
            combo.DataSource = dt;
            combo.ValueField = "IdNjesiAdministrative";
            combo.TextField = "Kodi";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
            if (plotesodefault && dt.Rows.Count == 1)
                combo.SelectedIndex = 1;
            dt.Dispose();
            ImbLogger.LogTraceShitje("Mbaroi metoda mbushComboMagazinatMeID");
        }

        public static void mbushComboMagazinatMeFilter(string filter, long startIndex, long endIndex, ASPxComboBox combo, int idPerdorues, int idNdermarrje, bool magNeHarte, int lloji, bool kushtMerrMagMeAutorizim)
        {//mbush griden e popupit me te dhena
            DataTable dt;
            dt = DbCore.DbRegjistrim.colNjesiAdministrative.merrSipasNjesiNdermarjePerLupeDegeMeFilter(filter, startIndex, endIndex, idNdermarrje, idPerdorues, magNeHarte, lloji, kushtMerrMagMeAutorizim);
            combo.DataSource = dt;
            combo.ValueField = "IdNjesiAdministrative";
            combo.TextField = "Kodi";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
            dt.Dispose();
        }

        public static void mbushComboElementeshPerIntegrim(int idNdermarrje, ASPxComboBox combo, int lloji, bool tePalidhur)
        {
            colElementePerIntegrim col = new colElementePerIntegrim(idNdermarrje, lloji, tePalidhur);
            combo.DataSource = col;
            combo.ValueField = "IdElementi";
            combo.TextField = "Kodi";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();

        }

        public static void mbushComboMuaji(ASPxComboBox combo, bool shtoItemBosh = false)
        {
            combo.Items.Clear();
            if (shtoItemBosh)
                combo.Items.Add("", 0);
            combo.Items.Add("Janar", 1);
            combo.Items.Add("Shkurt", 2);
            combo.Items.Add("Mars", 3);
            combo.Items.Add("Prill", 4);
            combo.Items.Add("Maj", 5);
            combo.Items.Add("Qershor", 6);
            combo.Items.Add("Korrik", 7);
            combo.Items.Add("Gusht", 8);
            combo.Items.Add("Shtator", 9);
            combo.Items.Add("Tetor", 10);
            combo.Items.Add("Nentor", 11);
            combo.Items.Add("Dhjetor", 12);
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        public static void mbushComboMuajiEng(ASPxComboBox combo, bool shtoItemBosh = false)
        {
            combo.Items.Clear();
            if (shtoItemBosh)
                combo.Items.Add("", 0);
            combo.Items.Add("January", 1);
            combo.Items.Add("February", 2);
            combo.Items.Add("March", 3);
            combo.Items.Add("April", 4);
            combo.Items.Add("May", 5);
            combo.Items.Add("June", 6);
            combo.Items.Add("July", 7);
            combo.Items.Add("August", 8);
            combo.Items.Add("September", 9);
            combo.Items.Add("October", 10);
            combo.Items.Add("November", 11);
            combo.Items.Add("December", 12);
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        public static void mbushComboPeriudhaTreMujore(ASPxComboBox combo)
        {
            combo.Items.Add("Tremujori 1", 1);
            combo.Items.Add("Tremujori 2", 2);
            combo.Items.Add("Tremujori 3", 3);
            combo.Items.Add("Tremujori 4", 4);
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;

        }

        public static void mbushComboPeriudhaKaterMujore(ASPxComboBox combo, bool shtoPeriudheVjetore = false, bool zgjidhPeriudheVjetore = false)
        {
            combo.Items.Add("4-Mujori i I", 1);
            combo.Items.Add("4-Mujori i II", 2);
            combo.Items.Add("4-Mujori i III", 3);
            if (shtoPeriudheVjetore)
            {
                combo.Items.Add("Vjetore", 4);
                if (zgjidhPeriudheVjetore)
                    combo.SelectedIndex = 3;
            }
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;

        }

        public static void mbushComboViti(ASPxComboBox combo)
        {
            int viti = DateTime.Today.Year;
            combo.Items.Add(viti.ToString(), viti);
            combo.Items.Add((viti - 1).ToString(), viti - 1);
            combo.Items.Add((viti - 2).ToString(), viti - 2);
            combo.Items.Add((viti - 3).ToString(), viti - 3);
            combo.Items.Add((viti - 4).ToString(), viti - 4);
            combo.Items.Add((viti - 5).ToString(), viti - 5);
            combo.Items.Add((viti - 6).ToString(), viti - 6);
            combo.Items.Add((viti - 7).ToString(), viti - 7);
            combo.Items.Add((viti - 8).ToString(), viti - 8);
            combo.Items.Add((viti - 9).ToString(), viti - 9);
            combo.Items.Add((viti - 10).ToString(), viti - 10);
            combo.Items.Add((viti - 11).ToString(), viti - 11);
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        public static void mbushComboNjesiProdhimi(int idNdermarrje, ASPxComboBox combo, bool plotesodefault)
        {
            DataTable dt = DbCore.DbRegjistrim.colNjesiProdhimi.merrNjesiProdhimiSipasNdermarrjeAktiveDt(idNdermarrje);
            combo.DataSource = dt;
            combo.ValueField = "IdNjesiProdhimi";
            combo.TextField = "Kodi";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
            if (plotesodefault && dt.Rows.Count == 1)
                combo.SelectedIndex = 1;
            dt.Dispose();
        }

        public static void mbushComboNjesiVarteseMeFilter(string filter, long startIndex, long endIndex, int idNdermarrje, ASPxComboBox combo)
        {
            DataTable dt = colNjesiVartese.merrSipasNjesiNdermarrjesDTMeFilter(filter, startIndex, endIndex, idNdermarrje);
            combo.DataSource = dt;
            combo.ValueField = "IdNjesiVartese";
            combo.TextField = "Kodi";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
            dt.Dispose();
        }

        public static void mbushComboNjesiVarteseMeID(int idNdermarrje, bool plotesoDefault, ASPxComboBox combo, int value)
        {
            DataTable dt = colNjesiVartese.merrSipasNjesiNdermarrjesDTMeID(idNdermarrje, value);
            combo.DataSource = dt;
            combo.ValueField = "IdNjesiVartese";
            combo.TextField = "Kodi";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
            if (plotesoDefault && dt.Rows.Count == 1)
                combo.SelectedIndex = 1;
            dt.Dispose();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="combo"></param>
        public static void mbushComboDogana(ASPxComboBox combo)
        {
            combo.Items.Add("Po", true);
            combo.Items.Add("Jo", false);
            combo.DataBind();
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me menyrat e llogaritjes se tatimeve
        /// </summary>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboMenyraTatime(ASPxComboBox combo)
        {
            combo.Items.Add(Menyra.Progresive.ToString(), Convert.ToInt32(Menyra.Progresive));
            combo.Items.Add(Menyra.Totale.ToString(), Convert.ToInt32(Menyra.Totale));
            combo.SelectedIndex = 0;
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me llojet e marreveshjeve
        /// </summary>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboLlojMarreveshje(ASPxComboBox combo)
        {
            combo.Items.Add("", 0);
            combo.Items.Add(LlojMarreveshje.Retention.ToString().Replace('_', ' '), Convert.ToInt32(LlojMarreveshje.Retention));
            combo.Items.Add(LlojMarreveshje.Acquisition.ToString(), Convert.ToInt32(LlojMarreveshje.Acquisition));
            combo.Items.Add(LlojMarreveshje.EBU_Benefit.ToString().Replace('_', ' '), Convert.ToInt32(LlojMarreveshje.EBU_Benefit));
            combo.Items.Add(LlojMarreveshje.Agreement_Tenure_Reward.ToString().Replace('_', ' '), Convert.ToInt32(LlojMarreveshje.Agreement_Tenure_Reward));

            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me menyrat e pageses
        /// </summary>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboLlojZbritje(ASPxComboBox combo)
        {
            combo.Items.Add("Perqindje", 0);
            combo.Items.Add("Vlere", 1);
            combo.SelectedIndex = 0;
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        }

        public static void mbushComboAplikim(ASPxComboBox combo)
        {
            combo.Items.Add("Me Pike", 1);
            combo.Items.Add("Pike + Pagese", 2);

            combo.SelectedIndex = 0;
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me metodat e kostos
        /// </summary>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboMetodeKostoje(ASPxComboBox combo, bool select)
        {//mbush combon e metodeKostos
            //DbCore.DbInventari.clsDatabaseInventari dbInventari = new DbCore.DbInventari.clsDatabaseInventari();
            colMetodeKostoje metode = new colMetodeKostoje();
            metode.mbushGjitheMetodeKostoje();
            combo.TextFormatString = "{0}";

            ListBoxColumn colprove = new ListBoxColumn() { FieldName = "Kodi", Caption = "", Width = Unit.Percentage(100) };
            ListBoxColumn colemer = new ListBoxColumn() { FieldName = "Pershkrimi", Width = Unit.Percentage(0) };
            ListBoxColumn colmonedha = new ListBoxColumn() { FieldName = "Shpjegimi", Width = Unit.Percentage(0) };
            combo.Columns.Add(colprove);

            combo.Columns.Add(colemer);
            combo.Columns.Add(colmonedha);
            combo.DataSource = metode;
            combo.ValueField = "IdMetodeKostoje";
            combo.TextField = "Kodi";
            combo.DataBind();
            if (@select) combo.SelectedIndex = 0;
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me monedhat e ndermarjes
        /// </summary>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        /// <param name="bosh"> shto rresht bosh</param>
        public static void mbushComboMonedha(int idPerdoruesi, int idNdermarrje, bool bosh, params ASPxComboBox[] combot)
        {
            for (int i = 0; i < combot.Length; i++)
            {
                var combo = combot[i];
                combo.ConfigureAndFill(() =>
                    {
                        var colMonedha = new colMonedhat();
                        colMonedha.mbushGjitheMonedhatAktive(idNdermarrje, idPerdoruesi);

                        if (bosh)
                        {
                            colMonedha.Insert(0, new clsMonedha("", "", false, idPerdoruesi, 0, 0, idNdermarrje, 0, new colLidhjetAutorizim(), 0));
                        }

                        return colMonedha;
                    }, "KodiMonedha", "IdMonedha");
            }
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me titujt per klient/furnitoret
        /// </summary>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboLlojeKonvertimesh(ASPxComboBox combo)
        {//mbush combon e titullit
            DataTable dt = colPajisjet.merrLlojeKonvertimeshPerPajisje();
            combo.DataSource = dt;
            combo.TextField = "Lloji";
            combo.ValueField = "LlojiID";
            combo.DataBind();
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        }

        public static void mbushComboMonedha(int idPerdoruesi, int idNdermarrje, ASPxComboBox combo, bool bosh, bool zgjidhMonedheNdermarrje)
        {
            mbushComboMonedha(idPerdoruesi, idNdermarrje, bosh, combo);
            if (zgjidhMonedheNdermarrje)
            {
                string monedhaNdermarjes = DbCore.DbAdmin.clsMonedha.ktheMonedhenENdermarrjes(idNdermarrje);
                combo.SelectedIndex = combo.Items.IndexOf(combo.Items.FindByText(monedhaNdermarjes));
            }

        }

        public static void mbushComboStatusPerfunduar(ASPxComboBox combo, ResourceManager rm, CultureInfo ci)
        {
            combo.Items.Add("", 0);
            combo.Items.Add(rm.GetString("cmbboxItemFilterAvancPo", ci), 1);
            combo.Items.Add(rm.GetString("cmbboxItemFilterAvancJo", ci), 2);
            combo.Items.Add(rm.GetString("cmbboxItemFilterAvancPjeserisht", ci), 3);
            combo.SelectedIndex = 0;
            combo.DataBind();
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me monedhat e ndermarjes default
        /// </summary>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboMonedhatNdermarje(ASPxComboBox combo)
        {
            //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();

            //clsFunksione funk = new DbCore.clsFunksione();
            //DbCore.DbAdmin.colMonedhat colMonedhat = new DbCore.DbAdmin.colMonedhat();
            DataTable dt = colMonedhat.merrMonedhaNdermarjeDTAktiv(-1, 0);
            //DbCore.DbAdmin.colMonedhat colMonedhat = dbAdmin.merrGjitheMonedhatAktive(-1, 0);
            combo.DataSource = dt;// colMonedhat;
            combo.TextField = "PershkrimiMonedha";
            combo.ValueField = "IdMonedha";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
            dt.Dispose();
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me ndermarjet sipas llojit
        /// </summary>
        /// <param name="idPerdoruesi"></param>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        /// <param name="lloji"> lloji i ndermarjes</param>
        public static void mbushComboNdermarrje(int idPerdoruesi, ASPxComboBox combo, int lloji, bool merrVitet = false)
        {
            combo.Columns.Clear();
            colNdermarrjet nderm = new colNdermarrjet();
            if (lloji == 0)
                nderm.mbushNdermarrjeDefault();
            //nderm = dbAdmin.ktheNdermarrjeDefault();
            else
            {
                using (clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin())
                {
                    nderm.mbushNdermarrjeList(dbAdmin.merrNdermarrjetEPerdoruesitDataTable(idPerdoruesi, merrVitet));
                }
            }
            //nderm = nderm.mbushArrayListNdermarrjetList(dbAdmin.merrNdermarrjetPerdoruesit(ktheIdPerdoruesi().ToString()));
            combo.DataSource = nderm;
            combo.TextFormatString = "{0}";

            ListBoxColumn colprove = new ListBoxColumn();
            colprove.FieldName = "NdermarrjeKodi";
            ListBoxColumn colemer = new ListBoxColumn();
            colemer.FieldName = "NdermarrjePershkrimi";

            combo.Columns.Add(colprove);
            combo.Columns.Add(colemer);
            combo.TextField = "NdermarrjeKodi";
            combo.ValueField = "IdNdermarrje";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;

            combo.DataBind();
            if (lloji == 0)
                combo.SelectedIndex = 2;
            else
                combo.SelectedIndex = 0;
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me nengrupet e llogarive
        /// </summary>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        /// <param name="idGrupiLlogari"> id e grupit te llogarive</param>
        public static void mbushComboNenGrupe(ASPxComboBox combo, int idGrupiLlogari, int idGjuha)
        {//mbush combon e nengrupeve me te dhena nga databasa
            //DbCore.DbKontabiliteti.clsDatabaseKontabilitet dbKontabiliteti = new DbCore.DbKontabiliteti.clsDatabaseKontabilitet();
            colNenGrupetLlogaria colNenGrupetLlogaria = new colNenGrupetLlogaria();
            colNenGrupetLlogaria.mbushNenGrupetLlogariaSipasGrupitPozitive(idGrupiLlogari, idGjuha);
            //colNenGrupetLlogaria = dbKontabiliteti.merrNenGrupetLlogariaSipasGrupitPozitive(idGrupiLlogari);
            combo.DataSource = colNenGrupetLlogaria;
            combo.TextField = "PershkrimiNenGrupiLlogaria";
            combo.ValueField = "IdNenGrupiLlogaria";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
            combo.Text = "";
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me nivelet e cmimeve
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboNiveleCmimesh(int idNdermarrje, int idperd, ASPxComboBox combo)
        {//mbush griden e popupit me te dhena
            colNiveleCmimesh colNiveleZbritjesh = new colNiveleCmimesh();
            //  colNiveleZbritjesh.mbushGjitheNiveleCmimeshSipasNdermarjes(DbCore.clsFunksione.merrIdNdermarrjeSesioni(Session));
            DataTable dt = colNiveleCmimesh.merrNiveleNdermarjeDT(idNdermarrje, idperd);
            //DbCore.DbInventari.colNiveleCmimesh colNiveleZbritjesh = dbInventari.merrGjitheNiveleCmimeshSipasNdermarjes(DbCore.clsFunksione.merrIdNdermarrjeSesioni(Session));
            combo.DataSource = dt;// colNiveleZbritjesh;
            combo.ValueField = "IdNivelCmimi";
            combo.TextField = "PershkrimNivelCmimi";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
            dt.Dispose();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="combo"></param>
        /// <param name="lloj"></param>
        public static void mbushComboNiveleCmimeshSipasLlojit(int idNdermarrje, ASPxComboBox combo, int lloj, int idperd)
        {//mbush griden e popupit me te dhena
            colNiveleCmimesh colNiveleZbritjesh = new colNiveleCmimesh();
            //  colNiveleZbritjesh.mbushGjitheNiveleCmimeshSipasNdermarjes(DbCore.clsFunksione.merrIdNdermarrjeSesioni(Session));
            DataTable dt = colNiveleCmimesh.merrNiveleNdermarjeDTBlerjeShitje(idNdermarrje, lloj, idperd);
            //DbCore.DbInventari.colNiveleCmimesh colNiveleZbritjesh = dbInventari.merrGjitheNiveleCmimeshSipasNdermarjes(DbCore.clsFunksione.merrIdNdermarrjeSesioni(Session));
            combo.DataSource = dt;// colNiveleZbritjesh;
            combo.ValueField = "IdNivelCmimi";
            combo.TextField = "PershkrimNivelCmimi";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
            dt.Dispose();
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me nivelet e cmimeve prind
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboNiveleCmimeshPrind(int idNdermarrje, ASPxComboBox combo)
        {//mbush griden e popupit me te dhena
            colNiveleCmimesh colNiveleZbritjesh = new colNiveleCmimesh();
            colNiveleZbritjesh.mbushGjitheNiveleCmimeshPrindiMeMonedheSipasNdermarjes(idNdermarrje);
            combo.DataSource = colNiveleZbritjesh;
            combo.TextFormatString = "{0},{1},{2},{3},{4}";
            ListBoxColumn colprove = new ListBoxColumn() { FieldName = "KodNivelCmimi" };
            ListBoxColumn colemer = new ListBoxColumn() { FieldName = "PershkrimNivelCmimi" };
            ListBoxColumn colemer1 = new ListBoxColumn() { FieldName = "Lloji" };
            ListBoxColumn colMonedha = new ListBoxColumn() { FieldName = "KodMonedha" };
            ListBoxColumn colBrutoNeto = new ListBoxColumn() { FieldName = "KodBrutoNeto", Caption = "Me TVSH/Pa TVSH" };
            combo.Columns.Clear();
            combo.Columns.Add(colprove);
            combo.Columns.Add(colemer);
            combo.Columns.Add(colemer1);
            combo.Columns.Add(colMonedha);
            combo.Columns.Add(colBrutoNeto);
            combo.ValueField = "IdNivelCmimi";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me nivelet e KPF(llogarive standarte)
        /// </summary>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboNivelesh(ASPxComboBox combo)
        {//mbush combon e niveleve me te dhena
            List<int> colNivelKPF = new List<int>();
            colNivelKPF.Add(1);
            colNivelKPF.Add(2);
            colNivelKPF.Add(3);
            colNivelKPF.Add(4);
            colNivelKPF.Add(5);
            colNivelKPF.Add(6);
            colNivelKPF.Add(7);
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataSource = colNivelKPF;
            combo.DataBind();
        }

        public static void mbushComboEdukimi(ASPxComboBox combo, int idndermarje, int idgjuha)
        {
            DataTable dt = clsPunonjes.merrEdukimeSipasIdNdermarje(idndermarje, idgjuha);
            combo.DataSource = dt;
            combo.TextField = "PERSHKRIMI";
            combo.ValueField = "ID";
            combo.DataBind();

            combo.SelectedIndex = 0;
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        }

        public static void mbushComboNjesiParamPagese(ASPxComboBox combo)
        {
            combo.Items.Add("Nr", 0);
            combo.Items.Add("Kohe", 1);

            combo.SelectedIndex = 0;
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        }

        public static void mbushComboGjinia(ASPxComboBox combo, CultureInfo ci, ResourceManager rm)
        {
            combo.Items.Add(rm.GetString(Gjinia.Mashkull.ToString(), ci), false);
            combo.Items.Add(rm.GetString(Gjinia.Femer.ToString(), ci), true);

            combo.SelectedIndex = 0;
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        }

        public static void mbushComboStatusi(ASPxComboBox combo, CultureInfo ci, ResourceManager rm)
        {
            combo.Items.Add(rm.GetString("Shef_departamenti", ci), 1);
            combo.Items.Add(rm.GetString("Punonjes", ci), 2);

            combo.SelectedIndex = 1;
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        }

        public static void mbushComboStatusiHr(ASPxComboBox combo, ResourceManager rm, CultureInfo ci)
        {
            combo.Items.Add(rm.GetString("cmbItemShefDepartamenti", ci), 1);
            combo.Items.Add(rm.GetString("MenuItemPunonjes", ci), 2);
            combo.Items.Add(rm.GetString("cmbboxItemFilterAvancTeGjithe", ci), 3);
            combo.SelectedIndex = 3;
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me njesite e artikullit
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboNjesi(int idNdermarrje, ASPxComboBox combo)
        {
            colNjesiteArtikulli col = new colNjesiteArtikulli(idNdermarrje);
            //DbCore.DbInventari.colNjesiteArtikulli col = new DbCore.DbInventari.colNjesiteArtikulli();
            //col = dbInventari.merrGjitheNjesiteArtikulliSipasNdermarrjes(DbCore.clsFunksione.merrIdNdermarrjeSesioni(Session));
            combo.DataSource = col;
            combo.ValueField = "IdNjesia";
            combo.TextField = "KodNjesia";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        public static void mbushComboKategoriSeriali(int idNdermarrje, ASPxComboBox combo)
        {
            DbCore.DbInventari.colSerialeUnikeKategori col = new DbCore.DbInventari.colSerialeUnikeKategori(idNdermarrje);

            combo.DataSource = col;
            combo.ValueField = "ID";
            combo.TextField = "Kategori";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        public static void mbushComboFormateSeriali(int idNdermarrje, ASPxComboBox combo)
        {
            colSerialeUnikeFormate col = new colSerialeUnikeFormate(idNdermarrje);

            combo.DataSource = col;
            combo.ValueField = "id";
            combo.TextField = "kod";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me llojet e garancive
        /// </summary>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboGarancite(ASPxComboBox combo)
        {
            colGarancite col = new colGarancite();
            col.shtoGaranciNeIndeksin(0, new clsGarancia());
            combo.DataSource = col;
            combo.ValueField = "IdLlojGarancia";
            combo.TextField = "KodGarancia";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        public static void mbushComboISiguruar(ASPxComboBox combo, CultureInfo ci, ResourceManager rm)
        {

            combo.Items.Add("", 0);
            combo.Items.Add(rm.GetString("cmbFilterPo", ci), 1);
            combo.Items.Add(rm.GetString("cmbFilterJo", ci), 2);
            combo.DataBind();
        }

        public static void mbushComboStatusAprovimi(ASPxComboBox combo, CultureInfo ci, ResourceManager rm)
        {
            combo.Items.Add("", 0);
            combo.Items.Add(rm.GetString("msgStatusPerAprovim", ci), 1);
            combo.Items.Add(rm.GetString("msgStatusAprovuar", ci), 2);
            combo.Items.Add(rm.GetString("msgStatusRefuzuar", ci), 3);
            combo.DataBind();
        }

        public static void mbushComboGjini(ASPxComboBox combo, CultureInfo ci, ResourceManager rm)
        {
            combo.Items.Add("", 0);
            combo.Items.Add(rm.GetString("Femer", ci), 1);
            combo.Items.Add(rm.GetString("Mashkull", ci), 2);
            combo.DataBind();
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me periudhat e nje viti ushtrimor
        /// </summary>
        /// <param name="idViti"></param>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboPeriudhatAktuale(int idViti, ASPxComboBox combo)
        {
            // int idViti = KtheNeObjektPeriudha(Session["oPeriudhaAktuale"].ToString()).IdViti;

            colPeriudhaKontabel colPeriudha = new colPeriudhaKontabel();
            colPeriudha.merrSipasViti(idViti);
            combo.DataSource = colPeriudha;

            combo.TextField = "nrPeriudha";
            combo.ValueField = "IdPeriudha";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;

            combo.DataBind();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="combo"></param>
        /// <param name="shitjefurnizim"></param>
        public static void mbushComboPikeShitjeFurnizimi(int idNdermarrje, ASPxComboBox combo, bool shitjefurnizim, bool merrPikeJoAktive)
        {
            DbCore.DbRegjistrim.colPikaShitjeFurnizimi colDege = new DbCore.DbRegjistrim.colPikaShitjeFurnizimi();
            colDege.Add(new DbCore.DbRegjistrim.clsPikeShitjeFurnizimi());
            colDege.mbushGjithePikatSipasLlojitDheAktiveOseJo(idNdermarrje, shitjefurnizim, merrPikeJoAktive);
            combo.DataSource = colDege;
            combo.TextField = "Kodi";
            combo.ValueField = "IdPikeShitjeFurnizimi";
            combo.DataBind();
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me prioritetet
        /// </summary>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboPrioriteti(ASPxComboBox combo)
        {//mbush combon e prioriteteve
            combo.Items.Add("1", 1);
            combo.Items.Add("2", 2);
            combo.Items.Add("3", 3);
            combo.Items.Add("4", 4);
            combo.Items.Add("5", 5);
            combo.Items.Add("6", 6);
            combo.Items.Add("7", 7);
            combo.Items.Add("8", 8);
            combo.Items.Add("9", 9);
            combo.Items.Add("10", 10);
            combo.Items.Add("11", 11);
            combo.Items.Add("12", 12);
            combo.Items.Add("13", 13);
            combo.Items.Add("14", 14);
            combo.Items.Add("15", 15);
            combo.Items.Add("16", 16);
            combo.Items.Add("17", 17);
            combo.Items.Add("18", 18);
            combo.Items.Add("19", 19);
            combo.Items.Add("20", 20);
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        }

        public static void mbushComboPrioritetiMeFillim(ASPxComboBox combo, int fillo)
        {//mbush combon e prioriteteve
            combo.Items.Add("Aprovuar", 0);
            for (int i = fillo; i <= 20; i++)
                combo.Items.Add(i + "", i);

            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        }

        public static void mbushShopsHierarkiStatus(ASPxComboBox combo)
        {
            colShopsHierarkiStatus colStat = new DbCore.DbAdmin.colShopsHierarkiStatus();
            combo.DataSource = colStat;
            combo.TextField = "PershkrimStatusi";
            combo.ValueField = "IdStatus";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        public static void mbushShopsHierarkiUniform(ASPxComboBox combo)
        {

            DbCore.DbAdmin.colShopsHierarkiUniform colUni = new DbCore.DbAdmin.colShopsHierarkiUniform();
            combo.DataSource = colUni;
            combo.TextField = "PershkrimUniform";
            combo.ValueField = "IdUniform";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        public static void mbushShopsHierarkiLeaveReason(ASPxComboBox combo)
        {

            DbCore.DbAdmin.colShopsHierarkiLeaveReason colLeavereason = new DbCore.DbAdmin.colShopsHierarkiLeaveReason();
            combo.DataSource = colLeavereason;
            combo.TextField = "PershkrimLeaveReason";
            combo.ValueField = "IdLeaveReason";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        /// <summary>
        /// perdoret per te mbushur combon e llojit te veprimit te raporti i historikut te veprimeve
        /// </summary>
        /// <param name="combo"></param>
        public static void mbushComboLlojVeprimi(ASPxComboBox combo, ResourceManager rm, CultureInfo ci, string rapEmriReal)
        {
            combo.Items.Add(rm.GetString("cmbTeGjitha", ci), "Te gjitha");
            combo.Items.Add(rm.GetString("cmbRegjistrim", ci), "Regjistrim");
            combo.Items.Add(rm.GetString("cmbModifikim", ci), "Modifikim");
            combo.Items.Add(rm.GetString("cmbFshirje", ci), "Fshirje");
            if (rapEmriReal.ContainsAnyIgnoreCase("Rap_HistorikuVeprimeveBuxhetimi"))
            {
                combo.Items.Add(rm.GetString("cmbPostim", ci), "Postim");
                combo.Items.Add(rm.GetString("cmbAprovim", ci), "Aprovim");
                combo.Items.Add(rm.GetString("cmbRefuzim", ci), "Refuzim");
            }
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.SelectedIndex = 0;
        }

        public static void mbushComboLlojCmimi(ASPxComboBox combo)
        {
            combo.Items.Add("Cmime Shitje", 0);
            combo.Items.Add("Cmime Blerje", 1);
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.SelectedIndex = 0;
        }

        /// <summary>
        /// perdoret per te mbushur combon e menuve te raporti i historikut te veprimeve
        /// </summary>
        /// <param name="combo"></param>
        public static void mbushComboMenuja(ASPxComboBox combo)
        {
            combo.Items.Add(" ", "Te gjitha");
            combo.Items.Add("Administrimi", "Administrimi");
            combo.Items.Add("Konfigurime", "Konfigurime");
            combo.Items.Add("Celje", "Celje");
            combo.Items.Add("Regjistrime", "Regjistrime");
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.SelectedIndex = 0;
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me qytetet
        /// </summary>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboQytetedef(ASPxComboBox combo)
        {
            colQytetet colQyt = new colQytetet();
            colQyt.mbushGjitheQytetetPozitive(-1);
            //DbCore.DbAdmin.colQytetet colQyt = dbAdmin.merrGjitheQytetetPozitive(DbCore.clsFunksione.merrIdNdermarrjeSesioni(Session));
            combo.DataSource = colQyt;
            combo.TextField = "EmriQyteti";
            combo.ValueField = "IdQyteti";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        public static void mbushComboAmbjente(ASPxComboBox combo, int idllojlicence, int idGjuha, bool ambjentpermobile)
        {
            colAmbjent colAmb = new colAmbjent();
            colAmb.Add(new clsAmbjent());
            colAmb.mbushGjitheAmbjente(idllojlicence, idGjuha, ambjentpermobile);
            combo.DataSource = colAmb;
            combo.TextField = "Pershkrimi";
            combo.ValueField = "Id";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me pajisjet
        /// </summary>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboPajisje(ASPxComboBox combo, int idNdermarrje)
        {
            colPajisjet colPaj = new colPajisjet(idNdermarrje, true);
            //    colPaj.mbushGjithePajisjet();
            colPaj.Insert(0, new clsPajisje("", "", "", 0, false, false, 0, 0, 0, 0, 0));
            combo.DataSource = colPaj;
            combo.TextField = "kodi";
            combo.ValueField = "IdPajisje";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        public static void mbushComboMuajt(ASPxComboBox combo, int idGjuha, string muajiSelektuar, clsPeriudhaKontabel periudha)
        {

            var muaj = clsFunksione.MerrListMuajsh(idGjuha);

            if (!string.IsNullOrWhiteSpace(muajiSelektuar))
            {
                if (int.TryParse(muajiSelektuar, out int x) && x >= 1 && x <= 12)
                {//e kam bere nga halli,se duhet ta zgjidhja per 10 min 
                    muajiSelektuar = muaj[x - 1];
                }
            }
            else muajiSelektuar = clsFunksione.MerrMuajinSipasPeriudhesDheGjuhes(periudha, idGjuha);

            var indeksi = 0;
            //shtojme muajin bosh
            //combo.Items.Add("", 0);
            foreach (var item in muaj)
                combo.Items.Add(item, ++indeksi);

            if (!String.IsNullOrWhiteSpace(muajiSelektuar))
                combo.SelectedItem = combo.Items.FindByText(muajiSelektuar);
            else combo.SelectedIndex = 0;
            combo.DataBind();
        }

        public static void mbushComboRolet(ASPxComboBox combo, int idPerdorues, bool plotesodefault)
        {
            DataTable dt = DbCore.DbAdmin.colRoli.merrRoletDTJoSuper(DbCore.DbAdmin.clsLicenca.merrIdLicencePerdoruesi(idPerdorues), idPerdorues);
            combo.DataSource = dt;
            combo.ValueField = "IdRoli";
            combo.TextField = "KodRoli";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
            if (plotesodefault && dt.Rows.Count == 1)
                combo.SelectedIndex = 1;
            dt.Dispose();
        }

        public static void mbushComboMenyrePagese(ASPxComboBox combo, bool select)
        {
            combo.Items.Add("", -1);
            combo.Items.Add(MenyrePagese.Me_mirebesim.ToString().Replace('_', ' '), Convert.ToInt32(MenyrePagese.Me_mirebesim));
            combo.Items.Add(MenyrePagese.Pagese.ToString(), Convert.ToInt32(MenyrePagese.Pagese));
            combo.Items.Add(MenyrePagese.Pagese_Automatike.ToString().Replace('_', ' '), Convert.ToInt32(MenyrePagese.Pagese_Automatike));
            combo.Items.Add(MenyrePagese.Me_parapagim.ToString().Replace('_', ' '), Convert.ToInt32(MenyrePagese.Me_parapagim));
            combo.Items.Add(MenyrePagese.Arke.ToString(), Convert.ToInt32(MenyrePagese.Arke));
            combo.Items.Add(MenyrePagese.Karte_krediti.ToString().Replace('_', ' '), Convert.ToInt32(MenyrePagese.Karte_krediti));
            combo.Items.Add(MenyrePagese.Pezull.ToString(), Convert.ToInt32(MenyrePagese.Pezull));
            combo.Items.Add(MenyrePagese.Banke.ToString(), Convert.ToInt32(MenyrePagese.Banke));
            if (@select) combo.SelectedIndex = 0;
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        }

        public static void mbushComboArsye(ASPxComboBox combo)
        {
            DataTable dt = clsPunesim.merrArsye();

            //DbCore.DbAdmin.colQytetet colQyt = dbAdmin.merrGjitheQytetetPozitive(DbCore.clsFunksione.merrIdNdermarrjeSesioni(Session));
            combo.DataSource = dt;
            combo.TextField = "Pershkrimi";
            combo.ValueField = "Id";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        public static void mbushComboVendndodhjet(int idNdermarrje, ASPxComboBox combo)
        {
            colVendndodhjet colQyt = new colVendndodhjet();
            colQyt.ktheGjitheVendndodhjetSipasNdermarjesAktiv(idNdermarrje);

            colQyt.Insert(0, new clsVendndodhjet(0, "", "", 0, true, 0, 0, 0, 0));
            //DbCore.DbAdmin.colQytetet colQyt = dbAdmin.merrGjitheQytetetPozitive(DbCore.clsFunksione.merrIdNdermarrjeSesioni(Session));
            combo.DataSource = colQyt;

            //   combo.TextField = "Pershkrimi";
            combo.ValueField = "Id";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        public static void shtokolonavendodhje(ASPxComboBox combo)
        {
            combo.TextFormatString = "{0} ({1})";
            ListBoxColumn colprove = new ListBoxColumn();
            colprove.FieldName = "Kodi";
            ListBoxColumn colemer = new ListBoxColumn();
            colemer.FieldName = "Pershkrimi";
            combo.Columns.Add(colprove);
            combo.Columns.Add(colemer);
        }

        public static void mbushComboKombesi(ASPxComboBox combo, int idgjuha)
        {
            DataTable dt = clsPunonjes.merrKombesiaDT();

            combo.DataSource = dt;
            if (idgjuha == 0)
                combo.TextField = "PershkrimiShq";
            else combo.TextField = "Pershkrimi";
            combo.ValueField = "Id";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        public static void mbushComboNdryshimPozicioni(ASPxComboBox combo, int idgjuha)
        {
            DataTable dt = clsPunonjes.merrNdryshimPozicioniDT(idgjuha);

            combo.DataSource = dt;
            combo.TextField = "Pershkrimi";
            combo.ValueField = "Id";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        public static void mbushComboQendraKostoPrindKolona(ASPxComboBox combo, ResourceManager rm, CultureInfo cultinf)
        {
            ListBoxColumn colNrLlog = new ListBoxColumn();
            colNrLlog.FieldName = "Kodi";
            colNrLlog.Caption = rm.GetString("cmbCmimeArtikulliCaptionKodi", cultinf);
            ListBoxColumn colEmer = new ListBoxColumn();
            colEmer.FieldName = "Pershkrimi";
            colEmer.Caption = rm.GetString("cmbCmimeArtikulliCaptionPershkrimi", cultinf);
            ListBoxColumn colMonedha = new ListBoxColumn();
            colMonedha.FieldName = "Monedha";
            colMonedha.Caption = "Monedha";
            combo.Columns.Add(colNrLlog);
            combo.Columns.Add(colEmer);
            combo.Columns.Add(colMonedha);
            combo.TextFormatString = "{0}";
        }

        public static void mbushComboQendraKostoPrind(int idNdermarrje, ASPxComboBox combo)
        {
            colQendraKosto col = new colQendraKosto();
            col.Insert(0, new clsQendraKosto(0, "", "", 0, true, 0, 0, 0, 0, 0, 0, new colBuxhetet()));
            col.mbushGjitheQendraKostoPrindiSipasNdermarjesAktiv(idNdermarrje);
            combo.DataSource = col;

            combo.TextField = "Kodi";
            combo.ValueField = "Id";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        public static void mbushComboQendraKostoPrindNiveli1(int idNdermarrje, ASPxComboBox combo)
        {
            colQendraKosto col = new colQendraKosto();

            col.ktheGjitheQendraKostoPrindiNiveli1SipasNdermarjesAktiv(idNdermarrje);
            combo.DataSource = col;

            combo.TextField = "Kodi";
            combo.ValueField = "Id";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        public static void mbushComboQendraKostoBij(int idNdermarrje, ASPxComboBox combo)
        {
            colQendraKosto col = new colQendraKosto();

            col.mbushGjitheQendraKostoBijSipasNdermarjesAktiv(idNdermarrje);
            combo.DataSource = col;

            combo.TextField = "Kodi";
            combo.ValueField = "Id";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        public static void mbushComboQendraKostoBijSipasPrindit(int idprindi, ASPxComboBox combo)
        {
            colQendraKosto col = new colQendraKosto();

            col.mbushQendraSipasPrindit(idprindi);
            combo.DataSource = col;

            combo.TextField = "Kodi";
            combo.ValueField = "Id";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        public static void mbushComboSkemaQendraKosto(int idNdermarrje, ASPxComboBox combo)
        {
            colKokaSkemaQK col = new colKokaSkemaQK();

            col.mbushGjitheSkematSipasNdermarjes(idNdermarrje);
            combo.DataSource = col;

            combo.TextField = "Kodi";
            combo.ValueField = "IdKoka";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        public static void mbushComboPunonjes(int idNdermarrje, ASPxComboBox combo)
        {
            colPunonjes col = new colPunonjes(idNdermarrje, true);
            combo.DataSource = col;

            ListBoxColumn colNrLlog = new ListBoxColumn();
            colNrLlog.FieldName = "Emer";
            colNrLlog.Caption = "Emer";
            ListBoxColumn colEmer = new ListBoxColumn();
            colEmer.FieldName = "Mbiemer";
            colEmer.Caption = "Mbiemer";

            combo.Columns.Add(colNrLlog);
            combo.Columns.Add(colEmer);

            combo.TextFormatString = "{0} {1}";
            //  combo.TextField = "NrPersonal";
            combo.ValueField = "NrPersonal";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me  rritje/zbritje
        /// </summary>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboRritjeZbritje(ASPxComboBox combo, ResourceManager rm, CultureInfo ci)
        {
            combo.Items.Add(rm.GetString("cmbCmimeArtikulliRritje", ci), rm.GetString("cmbCmimeArtikulliRritje", ci));
            combo.Items.Add(rm.GetString("cmbCmimeArtikulliZbritje", ci), rm.GetString("cmbCmimeArtikulliZbritje", ci));
            combo.Items.Add(rm.GetString("cmbCmimeArtikulliBarazim", ci), rm.GetString("cmbCmimeArtikulliBarazim", ci));
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.SelectedIndex = 0;
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me skemat per artikujt
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static string mbushComboSkemaKontabilitetiArtikulli(int idNdermarrje, ASPxComboBox combo, string llojiArt, string filter, int idklasa = 0)
        {
            string skema = "";
            bool lloji;
            if (llojiArt == "afatshkurter")
                lloji = false;
            else
                lloji = true;

            colSkematKontabilitetiArtikulli col = new colSkematKontabilitetiArtikulli(idNdermarrje, lloji, idklasa);

            //DbCore.DbInventari.colSkematKontabilitetiArtikulli col = new DbCore.DbInventari.colSkematKontabilitetiArtikulli();
            //col = dbInventari.merrSkemaKontabilitetiArtikulliTeGjithaSipasNdermarjes(DbCore.clsFunksione.merrIdNdermarrjeSesioni(Session));
            combo.DataSource = col.Where(x => x.KodiSkemaKontabilitetiArtikulli.IndexOf(filter, StringComparison.InvariantCultureIgnoreCase) > -1);

            foreach (clsSkemaKontabilitetiArtikulli s in col)
            {
                skema += s.KodiSkemaKontabilitetiArtikulli + ":" + s.Klasa + ";";
            }
            combo.TextField = "KodiSkemaKontabilitetiArtikulli";
            combo.ValueField = "IdSkemaKontabilitetiArtikulli";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;

            combo.DataBind();
            return skema;
        }

        public static string mbushComboSkemaKontabilitetiArtikulli(int idNdermarrje, ASPxComboBox combo, string llojiArt, object vlera, int idklasa = 0)
        {
            string skema = "";
            bool lloji;
            if (llojiArt == "afatshkurter")
                lloji = false;
            else
                lloji = true;

            colSkematKontabilitetiArtikulli col = new colSkematKontabilitetiArtikulli(idNdermarrje, lloji, idklasa);

            //DbCore.DbInventari.colSkematKontabilitetiArtikulli col = new DbCore.DbInventari.colSkematKontabilitetiArtikulli();
            //col = dbInventari.merrSkemaKontabilitetiArtikulliTeGjithaSipasNdermarjes(DbCore.clsFunksione.merrIdNdermarrjeSesioni(Session));
            combo.DataSource = col.Where(x => x.IdSkemaKontabilitetiArtikulli == Convert.ToInt32(vlera));

            foreach (clsSkemaKontabilitetiArtikulli s in col)
            {
                skema += s.KodiSkemaKontabilitetiArtikulli + ":" + s.Klasa + ";";
            }
            combo.TextField = "KodiSkemaKontabilitetiArtikulli";
            combo.ValueField = "IdSkemaKontabilitetiArtikulli";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;

            combo.DataBind();
            return skema;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="combo"></param>
        /// <param name="lloji"></param>
        /// <param name="idndermarje"></param>
        public static void shtoKolonaSkemaArtikulli(ASPxComboBox combo, bool lloji, int idndermarje)
        {
            clsNdermarrje nderm = new clsNdermarrje(idndermarje);

            //combo.TextFormatString = "{0},{2},{3},{4},{5},{6}";
            combo.TextFormatString = "{0}";
            ListBoxColumn colprove = new ListBoxColumn() { FieldName = "KodiSkemaKontabilitetiArtikulli", Caption = "Kodi" };
            ListBoxColumn colemer = new ListBoxColumn() { FieldName = "PershkrimiSkemaKontabilitetiArtikulli", Caption = "Pershkrimi" };
            ListBoxColumn colemer1 = new ListBoxColumn() { FieldName = "NrLlogariInventari" };
            if (nderm.Lloji == 2 && lloji)
                colemer1.Caption = "Llog.Inventari AA";
            else
                colemer1.Caption = "Llog.Inventari";
            ListBoxColumn colemer2 = new ListBoxColumn() { FieldName = "NrLlogariBlerje" };
            if (nderm.Lloji == 2 && lloji)
                colemer2.Caption = "Llog. pakesim vl. shitje";
            else colemer2.Caption = (lloji) ? "Llog. Vlere Kontabel" : "Llog. Blerje";
            ListBoxColumn colemer3 = new ListBoxColumn() { FieldName = "NrLlogariShitje" };
            if (nderm.Lloji == 2 && lloji)
                colemer3.Caption = "Llog. shitje AA";
            else
                colemer3.Caption = "Llog. Shitje";
            ListBoxColumn colemer4 = new ListBoxColumn() { FieldName = "NrLlogariTekTeTretet" };
            if (nderm.Lloji == 2)
                colemer4.Caption = (lloji) ? "Llog. shpenzimi rritje AA" : "Llog. ndryshim gjendje";
            else
                colemer4.Caption = (lloji) ? "Llog. AA ne proces" : "Llog. Tek te Tretet";
            ListBoxColumn colemer5 = new ListBoxColumn() { FieldName = "NrLlogariShpenzimi", Caption = (lloji) ? "Llog. Shpenzimi Amortizimi" : "Llog. Shpenzimi" };
            ListBoxColumn colemer6 = new ListBoxColumn() { FieldName = "NrLlogariAmortizimi", Caption = "Llog. Amortizimi" };
            ListBoxColumn colemer7 = new ListBoxColumn() { FieldName = "NrLlogariPakesimi", Caption = "Llog. Pakesim Vlere Dalje" };

            combo.Columns.Add(colprove);
            combo.Columns.Add(colemer);
            combo.Columns.Add(colemer1);
            combo.Columns.Add(colemer2);
            combo.Columns.Add(colemer3);
            combo.Columns.Add(colemer4);
            combo.Columns.Add(colemer5);
            if (lloji) combo.Columns.Add(colemer6);
            combo.Columns.Add(colemer7);
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me strukturat administrative
        /// </summary>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboStrukturaAdm(ASPxComboBox combo, int idprindi, int idNdermarrje, bool merrTeGjithe = true)
        {
            colStrukturatAdministrative col = new colStrukturatAdministrative();
            if (idprindi == 0)
                col.mbushGjitheStrukturaAdmPrindiSipasNdermarjes(idNdermarrje, merrTeGjithe);
            else col.mbushStrukturaAdmSipasPrindit(idprindi, merrTeGjithe);
            combo.DataSource = col;
            combo.TextField = "Emri";
            combo.ValueField = "IdStrukturaAdm";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me skema sigurimi
        /// </summary>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboSkemaSigurimi(ASPxComboBox combo, int idNdermarrje, DateTime data)
        {
            colSigurimet col = new colSigurimet(idNdermarrje, data);
            combo.DataSource = col;
            combo.TextField = "Kodi";
            combo.ValueField = "IdSigurime";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me skemat per fletet kontabel
        /// </summary>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idNderViti"></param>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboSkemaFleteKontabel(int idPerdoruesi, int idNderViti, ASPxComboBox combo)
        {

            colKokatSkematFletetKontabel colskema = new colKokatSkematFletetKontabel(idNderViti, idPerdoruesi);

            combo.DataSource = colskema;

            combo.TextField = "KodiKokaSkemaFK";
            combo.ValueField = "IdKokaSkemaFK";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;

            combo.DataBind();
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me skemat kontabel per regjistrimet
        /// </summary>
        /// <param name="idNderViti"></param>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboSkemaKontabelRegjistrime(int idNderViti, ASPxComboBox combo)
        {
            colSkemaKontabelNew colSkema = new colSkemaKontabelNew(idNderViti);
            combo.DataSource = colSkema;
            combo.ValueField = "IdSkemeKont";
            combo.TextField = "KodSkemeKont";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        /// <summary>
        /// perdoret per te mbushur nje kombo me formulat sipas ndermarrjes
        /// </summary>
        /// <param name="idNderViti"></param>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboFormulat(int idNdermarrje, ASPxComboBox combo)
        {//mbush griden e popupit me te dhena
            //colSkemaKontabelNew colSkema = new colSkemaKontabelNew(idNderViti);
            DataTable dt = colFormulat.merrFormulatSipasNdermarrjes(idNdermarrje);
            combo.DataSource = dt;
            combo.ValueField = "IdFormula";
            combo.TextField = "PershkrimFormula";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me tip kontrate
        /// </summary>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboTipKontrate(ASPxComboBox combo, int idndermarje, int idgjuha)
        {
            colTipeKontrate col = new colTipeKontrate(idndermarje);
            combo.DataSource = col;
            if (idgjuha == 0)
                combo.TextField = "Kodi";
            else combo.TextField = "KodiAng";
            combo.ValueField = "IdTipKontrate";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        public static void mbushComboProfesioneTituj(ASPxComboBox combo, int idndermarje, int lloj, int idgjuha)
        {
            colProfesioneTitujPune col = new colProfesioneTitujPune();
            col.ktheGjitheProfesioneTitujPunetSipasNdermarjesDheLlojitAktiv(idndermarje, lloj);
            combo.DataSource = col;
            if (idgjuha == 1)
                combo.TextField = "PershkrimiAng";
            else
                combo.TextField = "Pershkrimi";
            combo.ValueField = "Id";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        public static void mbushComboKodeProfesione(ASPxComboBox combo, int idndermarje)
        {
            colKodeProfesione col = new colKodeProfesione();
            col.ktheGjitheKodeProfesioneSipasNdermarjesAktiv(idndermarje);
            combo.DataSource = col;
            combo.TextField = "Kodi";
            combo.ValueField = "Id";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        public static void mbushComboGrupimeGlobalLocal(ASPxComboBox combo, int idndermarje, int lloj, string filter)
        {
            colGrupimeLocaleGlobale col = new colGrupimeLocaleGlobale();
            col.ShtoObjektBosh<clsGrupimeLocaleGlobale>();//shtuar sepse nese ka vetem nje grupim, ai selektohet sa here hapet dropdown edhe nese tenton ta fshish
            col.ktheGjitheGrupimeLocaleGlobaletSipasNdermarjesDheLlojitAktiv(idndermarje, lloj);
            combo.DataSource = col;
            combo.TextField = "Kodi";
            combo.ValueField = "Id";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        public static void mbushComboGrupimeGlobalLocal(ASPxComboBox combo, int idndermarje, int lloj, int vlera)
        {
            colGrupimeLocaleGlobale col = new colGrupimeLocaleGlobale();
            col.ktheGjitheGrupimeLocaleGlobaletSipasNdermarjesDheLlojitAktiv(idndermarje, lloj);
            combo.DataSource = col;
            combo.TextField = "Kodi";
            combo.ValueField = "Id";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        public static void mbushComboGrupimeGlobalLocalSipasPrindit(ASPxComboBox combo, int idndermarje, int idprindi, string filter)
        {
            colGrupimeLocaleGlobale col = new colGrupimeLocaleGlobale();
            col.ktheGjitheGrupimeLocaleGlobaletSipasNdermarjesDhePrinditAktiv(idndermarje, idprindi);
            combo.DataSource = col.Where(x => x.Kodi.IndexOf(filter, StringComparison.InvariantCultureIgnoreCase) > -1);
            combo.TextField = "Kodi";
            combo.ValueField = "Id";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        public static void mbushComboGrupimeGlobalLocalSipasPrindit(ASPxComboBox combo, int idndermarje, int idprindi, int vlera)
        {
            colGrupimeLocaleGlobale col = new colGrupimeLocaleGlobale();
            col.ktheGjitheGrupimeLocaleGlobaletSipasNdermarjesDhePrinditAktiv(idndermarje, idprindi);
            combo.DataSource = col.Where(x => x.Id == vlera);
            combo.TextField = "Kodi";
            combo.ValueField = "Id";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me kategorite e detyrave per crm
        /// </summary>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboKategoriDetyrash(ASPxComboBox combo)
        {//mbush combon e titullit
            combo.Items.Add("Agjent", 1);
            combo.Items.Add("Klient", 2);
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me nivelin e rendesise se detyrave per crm
        /// </summary>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboRendesiDetyrash(ASPxComboBox combo)
        {//mbush combon e titullit
            combo.Items.Add("Niveli 1", 1);
            combo.Items.Add("Niveli 2", 2);
            combo.Items.Add("Niveli 3", 3);
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me tipet te komponenteve te pageses
        /// </summary>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboTipePagese(ASPxComboBox combo)
        {
            combo.Items.Add(MessagesResource.Messages["lblPagese"], Convert.ToInt32(TipPagese.Pagese));
            combo.Items.Add(MessagesResource.Messages["lblNdalese"], Convert.ToInt32(TipPagese.Ndalese));
            combo.Items.Add(MessagesResource.Messages["lblLlogaritese"], Convert.ToInt32(TipPagese.Llogaritese));
            combo.SelectedIndex = 0;
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        }

        public static void mbushComboPrioritetShperndarje(ASPxComboBox combo, CultureInfo ci, ResourceManager rm)
        {
            combo.Items.Add(rm.GetString("labelFilterAvancuarDegeAdministrative", ci), Convert.ToInt32(PrioritetShperndarje.Dege_Administrative));
            combo.Items.Add(rm.GetString("labelFilterAvancuarNendepartamenti", ci), Convert.ToInt32(PrioritetShperndarje.Nendepartamenti));
            combo.Items.Add(rm.GetString("labelFilterAvancuarDepartamenti", ci), Convert.ToInt32(PrioritetShperndarje.Departamenti));
            combo.Items.Add(rm.GetString("cmbLlogariKontabel", ci), Convert.ToInt32(PrioritetShperndarje.Llogari_Kontabel));
            combo.Items.Add(rm.GetString("cmbMagazine", ci), Convert.ToInt32(PrioritetShperndarje.Magazine));
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me tipet e burimeve
        /// </summary>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboTipeBurimesh(ASPxComboBox combo)
        {
            combo.Items.Add(TipBurimi.Makineri.ToString(), Convert.ToInt32(TipBurimi.Makineri));
            combo.Items.Add(TipBurimi.Mjet.ToString(), Convert.ToInt32(TipBurimi.Mjet));
            combo.Items.Add(TipBurimi.Punonjes.ToString(), Convert.ToInt32(TipBurimi.Punonjes));

            combo.SelectedIndex = 0;
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        }

        public static void mbushComboLlojeSeriale(ASPxComboBox combo)
        {
            combo.Items.Add("", Convert.ToInt32(enumSerialeUnike_Lloje.PA_KUFIZIM));
            combo.Items.Add("Alfanumerike", Convert.ToInt32(enumSerialeUnike_Lloje.ALFANUMERIKE));
            combo.Items.Add("Numerike", Convert.ToInt32(enumSerialeUnike_Lloje.NUMERIKE));
            combo.Items.Add("Karaktere speciale", Convert.ToInt32(enumSerialeUnike_Lloje.KARAKTERE_SPECIALE));

            combo.SelectedIndex = 0;
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        }

        public static void mbushComboKrahasimi(ASPxComboBox combo)
        {
            combo.Items.Add("=", 0);
            combo.Items.Add("<", 1);
            combo.Items.Add(">", 2);
            combo.Items.Add("<=", 3);
            combo.Items.Add(">=", 4);

            combo.SelectedIndex = 0;
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me vitet
        /// </summary>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboVitet(ASPxComboBox combo, int idNdermarje = -1, bool shtoVitBosh = false)
        {
            //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            colVitet col = new colVitet();
            col.merrGjitheVitetENdermarjes(idNdermarje);
            if (shtoVitBosh)
                col.Insert(0, new clsViti());
            //col = dbAdmin.merrGjitheVitet();
            combo.DataSource = col;
            combo.TextField = "KodiViti";
            combo.ValueField = "IdViti";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me  vlere/ perqindje
        /// </summary>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboVlerePerqidje(ASPxComboBox combo, ResourceManager rm, CultureInfo ci)
        {
            combo.Items.Add("Perqindje", 0);
            combo.Items.Add("Vlere", 1);
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.SelectedIndex = 0;
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me zevendesimet per artikujt
        /// </summary>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushComboZevendesim(ASPxComboBox combo)
        {//mbush kombon e llojit te adreses me te dhena nga databasa
            combo.Items.Add("0", 1);
            combo.Items.Add("1", 2);
            combo.Items.Add("2", 3);
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me drejtimet
        /// </summary>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushMeTeDhenaDrejtimi(ASPxComboBox combo)
        {
            List<clsDrejtim> liste = new List<clsDrejtim>();
            clsDrejtim drejtimi = new clsDrejtim();
            drejtimi.IdDrejtimi = 0;
            drejtimi.DrejtimiPershkrimi = "Rrites";
            liste.Add(drejtimi);
            drejtimi = new clsDrejtim();
            drejtimi.IdDrejtimi = 1;
            drejtimi.DrejtimiPershkrimi = "Zbrites";
            liste.Add(drejtimi);

            combo.DataSource = liste;
            combo.TextField = "DrejtimiPershkrimi";
            combo.ValueField = "IdDrejtimi";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        /// <summary>
        /// Perdoret per te mbushur nje combobox  me peridhat e infos se artikullit
        /// </summary>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushMeTeDhenaPeridhenInfoArtikulli(ASPxComboBox combo)
        {
            List<clsPeriudhaInfoArtikulli> liste = new List<clsPeriudhaInfoArtikulli>();
            clsPeriudhaInfoArtikulli periudha = new clsPeriudhaInfoArtikulli();
            periudha.IdPeriudha = 0;
            periudha.PeriudhaPershkrim = "Data Fatures";
            liste.Add(periudha);
            periudha = new clsPeriudhaInfoArtikulli();
            periudha.IdPeriudha = 1;
            periudha.PeriudhaPershkrim = "Viti Ushtrimor";
            liste.Add(periudha);

            combo.DataSource = liste;
            combo.TextField = "PeriudhaPershkrim";
            combo.ValueField = "IdPeriudha";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me llojet e kodit
        /// </summary>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushMeTeDhenaLlojKodi(ASPxComboBox combo)
        {
            colLlojKodi colLlojiKodi = new colLlojKodi();
            colLlojiKodi.mbushGjithellojKodiPozitive();
            //colLlojiKodi = dbAdmin.merrGjithellojKodiPozitive();

            combo.DataSource = colLlojiKodi;
            combo.TextField = "LlojKodiPershkrimi";
            combo.ValueField = "IdLlojKodi";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me kategorite e niveleve te dokumentave
        /// </summary>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushMeTeDhenaKategoriNrAuto(ASPxComboBox combo)
        {
            colKategoriNrAuto colKategorite = new colKategoriNrAuto();
            colKategorite.mbushGjitheKategoriNrAuto();

            combo.DataSource = colKategorite;
            combo.TextField = "Pershkrimi";
            combo.ValueField = "Id";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        /// <summary>
        /// perdoret per te mbushur nje combobox  me llojet e periudhave
        /// </summary>
        /// <param name="combo"> comboboxi qe do te mbushet me te dhena</param>
        public static void mbushMeTeDhenaLlojPeriudhe(ASPxComboBox combo, int idKategoria)
        {
            colLlojPeriudhe colLlojPeriudhe = new colLlojPeriudhe();
            colLlojPeriudhe = colLlojPeriudhe.merrLlojPeriudhashSipasIdKatNrAuto(idKategoria);

            combo.DataSource = colLlojPeriudhe;
            combo.TextField = "LlojPeriudhePershkrimi";
            combo.ValueField = "IdLlojPeriudhe";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        public static void mbushComboKategoriBuxhetiSipasFiltrimit(int idNdermarrje, ASPxComboBox combo, string gridID, bool meFilter, ListEditItemRequestedByValueEventArgs eValue, ListEditItemsRequestedByFilterConditionEventArgs eFilter)
        {
            if (!meFilter && (eValue == null || eValue.Value == null || String.IsNullOrWhiteSpace(eValue.Value.ToString()))) return;

            var colKategoriBuxhetimi = mySessionObjects.MerrNgaSession<ColBKategoriBuxhetimi>(HttpContext.Current.Session, gridID);

            if (!(colKategoriBuxhetimi.Count > 0))
            {
                colKategoriBuxhetimi = ColBKategoriBuxhetimi.merrKategoriBuxhetimiAktiveSipasNdermarrjes(idNdermarrje);
            }
            var dt = colKategoriBuxhetimi.ToDataTable();
            IEnumerable<DataRow> d;
            if (meFilter)
                d = dt.Select($"Kodi like '%{eFilter.Filter}%' or Pershkrimi like '%{eFilter.Filter}%' ").AsEnumerable().Skip(eFilter.BeginIndex).Take(eFilter.EndIndex - eFilter.BeginIndex + 1);
            else
                d = dt.Select($"IdKategoriBuxhetimi = {eValue.Value}");
            combo.DataSource = d.GetDataTable(dt);
            combo.AllowNull = true;
            combo.ValueField = "IdKategoriBuxhetimi";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        public static void mbushComboViteBuxheti(HttpSessionState Session, int idModeli, ASPxComboBox cmbViti)
        {
            if (idModeli == 0 || idModeli == null)
                return;
            var periudhaKontabel = mySessionObjects.merrPeriudheKontabel(Session);
            var viti = periudhaKontabel.FillimiPeriudha.Year;
            cmbViti.Items.Add(viti.ToString(), viti);
            int nrVitesh = clsKusht.kthevlereSipasKushtitDheIdKonfig(Convert.ToInt32(idModeli), "NVB");
            nrVitesh = nrVitesh == 0 ? 3 : nrVitesh;
            for (int i = 1; i < nrVitesh; i++)
            {
                cmbViti.Items.Add((viti + i).ToString(), (viti + i));
            }
            cmbViti.SelectedIndex = 0;
            cmbViti.DropDownStyle = DropDownStyle.DropDownList;
        }

        public static void mbushComboEnumeration<T>(ASPxComboBox combo, bool shtoBosh)
        {
            var list = new List<object>();
            if (shtoBosh)
                list.Add(new { text = "", value = (int?)null });
            foreach (T item in Enum.GetValues(typeof(T)))
            {
                list.Add(new { text = item.ToString(), value = Convert.ToInt32(item) });
            }
            combo.DataSource = list;
            combo.ValueField = "value";
            combo.TextField = "text";
            combo.DataBind();
            combo.SelectedIndex = 0;
        }

        public static void mbushComboLlojeBurimi(ASPxComboBox combo)
        {
            DataTable llojeBuxheti = new DataTable();
            using (var db = new ClsDatabaseBuxheti())
                llojeBuxheti = db.merrLlojeBurimi();
            combo.DataSource = llojeBuxheti;
            combo.ValueField = "ID";
            combo.TextField = "BURIMI";
            combo.DataBind();
            combo.SelectedIndex = 0;
        }

        public static void mbushComboLlojeBuxhetiSipasFiltrimit(int idNdermarrje, ASPxComboBox combo, string comboId, bool meFilter, ListEditItemRequestedByValueEventArgs eValue, ListEditItemsRequestedByFilterConditionEventArgs eFilter)
        {
            if (!meFilter && (eValue == null || eValue.Value == null || String.IsNullOrWhiteSpace(eValue.Value.ToString()))) return;

            var colLlojeBuxheti = mySessionObjects.MerrNgaSession<ColBLlojBuxheti>(HttpContext.Current.Session, comboId);

            if (colLlojeBuxheti == null || !(colLlojeBuxheti.Count > 0))
            {
                colLlojeBuxheti = ColBLlojBuxheti.KtheSipasNdermarrjes(idNdermarrje);
                mySessionObjects.RuajNeSession<ColBLlojBuxheti>(HttpContext.Current.Session, colLlojeBuxheti, comboId);
            }
            var dt = colLlojeBuxheti.ToDataTable();
            IEnumerable<DataRow> d;
            if (meFilter)
                d = dt.Select($"Kodi like '%{eFilter.Filter}%' or Pershkrimi like '%{eFilter.Filter}%' ").AsEnumerable().Skip(eFilter.BeginIndex).Take(eFilter.EndIndex - eFilter.BeginIndex + 1);
            else
                d = dt.Select($"IdLlojBuxheti = {eValue.Value}");
            combo.DataSource = d.GetDataTable(dt);
            combo.AllowNull = true;
            combo.ValueField = "IdLlojBuxheti";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        /// <summary>
        /// shto kolonat per combon e detyrave ose te klienteve
        /// </summary>
        /// <param name="combo"></param>
        public static void shtoKolonaPerDetyraOseKlient(ASPxComboBox combo)
        {
            combo.TextFormatString = "{0} ({1})";
            ListBoxColumn colprove = new ListBoxColumn();
            colprove.FieldName = "Kodi";
            colprove.Caption = "Kodi";
            ListBoxColumn colemer = new ListBoxColumn();
            colemer.FieldName = "Emertimi";
            colemer.Caption = "Pershkrimi";
            combo.Columns.Clear();
            combo.Columns.Add(colprove);
            combo.Columns.Add(colemer);

            combo.ValueField = "Id";
            combo.ValueType = typeof(int);
        }

        public static void mbushComboStatusFaturuar(ASPxComboBox combo, ResourceManager rm, CultureInfo ci)
        {
            combo.Items.Add("", 0);
            combo.Items.Add(rm.GetString("cmbboxItemFilterAvancPo", ci), 1);
            combo.Items.Add(rm.GetString("cmbboxItemFilterAvancJo", ci), 2);
            combo.Items.Add(rm.GetString("cmbboxItemFilterAvancPjeserisht", ci), 3);
            combo.SelectedIndex = 0;
            combo.DataBind();
        }

        public static void KonfiguroComboBoxKartaById(ASPxComboBox combo, int idKarta)
        {
            ShtoKolonaPerKartaKlienti(combo);
            combo.ConfigureAndFill(() => clsKarta.MerrKarteSipasIdDt(idKarta), "Kodi", "IdKarta");
            if (combo.Items.Count != 0)
                combo.Items[0].Selected = true;
        }

        public static void KonfiguroComboBoxKartaMeFilter(string filter, long startIndex, long endIndex, ASPxComboBox combo, int idNdermarrje, int idKlient)
        {
            ShtoKolonaPerKartaKlienti(combo);
            combo.ConfigureAndFill(() => colKarta.KtheKarteSipasNdermMeFilter(filter, startIndex, endIndex, idNdermarrje, idKlient), "Kodi", "IdKarta");
        }

        public static void mbushComboFazatById(ASPxComboBox combo, int idFazat)
        {
            DataTable dt = clsFazaKontrate.ktheFazeSipasIdDt(idFazat);
            combo.DataSource = dt;
            combo.ValueField = "IdFaza";
            combo.TextField = "Pershkrimi";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
            if (combo.Items.Count != 0)
                combo.Items[0].Selected = true;
        }

        public static void mbushComboFormat(ASPxComboBox combo, int idNdermarrje, int idkategori, ListEditItemRequestedByValueEventArgs e)
        {
            if (e == null || e.Value == null || string.IsNullOrWhiteSpace(e.Value.ToString())) return;
            if (idkategori == 0) return;

            combo.SelectedItem = combo.Items.FindByValue(idkategori.ToString());
            DataTable dt = colKokaFormatImporti.MerrFormatetsipasNdermarjesDheKategorise(idNdermarrje, idkategori);
            combo.DataSource = dt.Select($"IdKoka = {e.Value}").GetDataTable(dt);
            combo.ValueField = "IdKoka";
            combo.TextField = "Kodi";
            combo.DataBind();
            combo.SelectedIndex = 0;
            dt.Dispose();
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;

        }
        public static void mbushComboFormat(ASPxComboBox combo, int idNdermarrje, int idkategori, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (e == null || string.IsNullOrWhiteSpace(e.Filter)) return;
            combo.SelectedItem = combo.Items.FindByValue(idkategori.ToString());
            DataTable dt = colKokaFormatImporti.MerrFormatetsipasNdermarjesDheKategorise(idNdermarrje, idkategori);
            IEnumerable<DataRow> d = dt.Select($"Kodi like '%{e.Filter}%'").AsEnumerable().Skip(e.BeginIndex).Take(e.EndIndex - e.BeginIndex + 1);
            if (idkategori == 0) return;
            combo.DataSource = d.GetDataTable(dt);
            combo.TextField = "Kodi";
            combo.ValueField = "IdKoka";
            combo.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            combo.DataBind();
            combo.SelectedIndex = 0;

        }
    }
}