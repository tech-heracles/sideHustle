using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Resources;
using System.Web.SessionState;
using DbCore;
using DbCore.DbAdmin;
using DbCore.DbArkaBanka;
using DbCore.DbAsete;
using DbCore.DbBuxheti;
using DbCore.DbInventari;
using DbCore.DbKontabiliteti;
using DbCore.DbListPagesat;
using DbCore.DbProdhimi;
using DbCore.DbRegjistrim;
using DbCore.DbShare;
using DbCore.IMBUtils.Extensions;
using DevExpress.Web;
using DevExpress.Web.ASPxTreeList;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using DbCore.IMBUtils.Messages;
using DbCore.CustomEntites;

namespace PlatinumWeb.ApplicationUtils.ASPxControlUtils
{
    public static class KonfigurimComboGride
    {
        #region static combo items

        public static ListEditItemCollection MerrMuajTePerkthyer(ResourceManager rm, CultureInfo ci)
        {
            var lista = new ListEditItemCollection();
            lista.Add("", 0);
            var emerMuaji = ci.Name == "sq-AL" ? Muajt.Janar.ToString() : Months.January.ToString();
            lista.Add(emerMuaji, Convert.ToInt32(Muajt.Janar));
            emerMuaji = ci.Name == "sq-AL" ? Muajt.Shkurt.ToString() : Months.February.ToString();
            lista.Add(emerMuaji, Convert.ToInt32(Muajt.Shkurt));
            emerMuaji = ci.Name == "sq-AL" ? Muajt.Mars.ToString() : Months.March.ToString();
            lista.Add(emerMuaji, Convert.ToInt32(Muajt.Mars));
            emerMuaji = ci.Name == "sq-AL" ? Muajt.Prill.ToString() : Months.April.ToString();
            lista.Add(emerMuaji, Convert.ToInt32(Muajt.Prill));
            emerMuaji = ci.Name == "sq-AL" ? Muajt.Maj.ToString() : Months.May.ToString();
            lista.Add(emerMuaji, Convert.ToInt32(Muajt.Maj));
            emerMuaji = ci.Name == "sq-AL" ? Muajt.Qershor.ToString() : Months.June.ToString();
            lista.Add(emerMuaji, Convert.ToInt32(Muajt.Qershor));
            emerMuaji = ci.Name == "sq-AL" ? Muajt.Korrik.ToString() : Months.July.ToString();
            lista.Add(emerMuaji, Convert.ToInt32(Muajt.Korrik));
            emerMuaji = ci.Name == "sq-AL" ? Muajt.Gusht.ToString() : Months.August.ToString();
            lista.Add(emerMuaji, Convert.ToInt32(Muajt.Gusht));
            emerMuaji = ci.Name == "sq-AL" ? Muajt.Shtator.ToString() : Months.September.ToString();
            lista.Add(emerMuaji, Convert.ToInt32(Muajt.Shtator));
            emerMuaji = ci.Name == "sq-AL" ? Muajt.Tetor.ToString() : Months.October.ToString();
            lista.Add(emerMuaji, Convert.ToInt32(Muajt.Tetor));
            emerMuaji = ci.Name == "sq-AL" ? Muajt.Nentor.ToString() : Months.November.ToString();
            lista.Add(emerMuaji, Convert.ToInt32(Muajt.Nentor));
            emerMuaji = ci.Name == "sq-AL" ? Muajt.Dhjetor.ToString() : Months.December.ToString();
            lista.Add(emerMuaji, Convert.ToInt32(Muajt.Dhjetor));
            lista.Add("", 0);
            return lista;
        }

        public static ListEditItemCollection MerrItemsPerStatusDokumenti(ResourceManager rm, CultureInfo ci)
        {
            var lista = new ListEditItemCollection
            {
              //  {"",0},
                {rm.GetString("cmbStatusiListaLidhjaDokDraft", ci), 0},
                {rm.GetString("cmbStatusiListaLidhjaDokRuajtur", ci), 1},
                {rm.GetString("msgStatusRefuzuar", ci), 4},
                {rm.GetString("msgStatusPezulluar", ci), 8}
            };
            return lista;
        }

        public static ListEditItemCollection MerrItemsPerStatusDokumentBuxheti(ResourceManager rm, CultureInfo ci, int idKatDok)
        {
            var lista = new ListEditItemCollection() {
                {rm.GetString("cmbStatusiListaLidhjaDokDraft", ci), 0},
                {rm.GetString("cmbStatusiListaLidhjaDokRuajtur", ci), 1},
            };
            if (idKatDok == 170 || idKatDok == 175 || idKatDok == 172 || idKatDok == 179)
                lista.Add(rm.GetString("msgStatusPostuar", ci), 9);
            if (idKatDok == 175 || idKatDok == 179)
                lista.Add(rm.GetString("msgStatusRefuzuar", ci), 8);
            return lista;
        }
        public static ListEditItemCollection MerrItemsPerLlojVeprimiDokumentBuxheti(int idKatDok)
        {
            var lista = new ListEditItemCollection();
            foreach (EnumBLlojeVeprimi item in Enum.GetValues(typeof(EnumBLlojeVeprimi)))
            {
                if (idKatDok == 177 && item.ToString().EqualsAnyIgnoreCase("Shitje", "Arketim"))
                    lista.Add(item.ToString(), (int)item);
                else
                    if (item.ToString().EqualsAnyIgnoreCase("Blerje", "Pagese"))
                    lista.Add(item.ToString(), (int)item);
            }
            return lista;
        }

        public static ListEditItemCollection MerrItemsPerStatusAprovimi(ResourceManager rm, CultureInfo ci)
        {
            var lista = new ListEditItemCollection
            {
                {"", 0},
                {rm.GetString("msgStatusPerAprovim", ci), Convert.ToInt32(StatusAprovimi.Per_Aprovim)},
                {rm.GetString("reportWatermarkDeleguar",ci),Convert.ToInt32(StatusAprovimi.Deleguar)},
                {rm.GetString("msgStatusAprovuar", ci), Convert.ToInt32(StatusAprovimi.Aprovuar)},
                {rm.GetString("msgStatusRefuzuar", ci), Convert.ToInt32(StatusAprovimi.Refuzuar)}
            };

            return lista;
        }

        public static ListEditItemCollection MerrItemsPer_shtoStatusAprovimi_Perd(ResourceManager rm, CultureInfo ci)
        {
            var lista = new ListEditItemCollection
            {
                {"", 0},
                {rm.GetString("msgStatusPerAprovim", ci), 1},
                {rm.GetString("msgStatusAprovuar", ci), 2},
                {rm.GetString("msgStatusRefuzuar", ci), 3}
            };
            return lista;
        }
        public static ListEditItemCollection MerrItems_shtoGjini(ResourceManager rm, CultureInfo ci)
        {
            var lista = new ListEditItemCollection
            {
                {"", 0},
                  {rm.GetString("cmbGjiniaFemer", ci), 1},
                {rm.GetString("cmbGjiniaMashkull", ci), 2}
            };
            return lista;
        }
        public static ListEditItemCollection MerrItems_shtoGjuhe(ResourceManager rm, CultureInfo ci)
        {
            var lista = new ListEditItemCollection
            {
                {"Shqip", 0},
                {"Anglisht", 1}
            };
            return lista;
        }


        public static ListEditItemCollection MerrItems_shtoISigururar(ResourceManager rm, CultureInfo ci)
        {
            var lista = new ListEditItemCollection
            {
                {"", 0},
                {rm.GetString("cmbboxItemFilterAvancPo", ci), 1},
                {rm.GetString("cmbboxItemFilterAvancJo", ci), 2}
            };
            return lista;
        }

        public static ListEditItemCollection MerrItemsPerFormule(ResourceManager rm, CultureInfo ci)
        {
            var lista = new ListEditItemCollection
            {
                {"", Convert.ToInt32(Formula.Undefined)},
                {rm.GetString("cmbFormuleDite", ci), Convert.ToInt32(Formula.Dite)},
                {rm.GetString("cmbFormuleJave", ci), Convert.ToInt32(Formula.Jave)},
                {rm.GetString("cmbFormuleMuaj", ci), Convert.ToInt32(Formula.Muaj)}
            };
            return lista;
        }

        public static ListEditItemCollection MerrItemPerLlojinKlientFurnitor(ResourceManager rm, CultureInfo ci)
        {
            var lista = new ListEditItemCollection
            {
              //  {"",0},
                {rm.GetString("cmbLlojiKlient", ci), true},
                {rm.GetString("cmbLlojiFurnitor", ci), false}
            };

            return lista;
        }

        public static ListEditItemCollection MerrItemPerStatusTransferimi(ResourceManager rm, CultureInfo ci)
        {
            var lista = new ListEditItemCollection
            {
                {"",-1},
                {rm.GetString("cmbStatusiJoTransferuar", ci), 0},
                {rm.GetString("cmbStatusiTransferuar", ci), 1},
                {rm.GetString("cmbStatusiKonvertuar", ci), 2}
            };

            return lista;
        }

        public static ListEditItemCollection MerrItemPershtoProdhuar(ResourceManager rm, CultureInfo ci)
        {
            var lista = new ListEditItemCollection
            {
                {"",-1},
                {rm.GetString("cmbboxItemFilterAvancJo", ci), 0},
                {rm.GetString("cmbboxItemFilterAvancPjeserisht", ci), 1},
                {rm.GetString("cmbboxItemFilterAvancPo", ci), 2}
            };
            return lista;
        }

        public static ListEditItemCollection MerrItemsPerMenyrePagese(ResourceManager rm, CultureInfo ci)
        {
            var lista = new ListEditItemCollection
            {
              //{"",0},
                {"", -1},
                {rm.GetString("cmbMenyrePageseMeMirebesim", ci), Convert.ToInt32(MenyrePagese.Me_mirebesim)},
                {rm.GetString("cmbMenyrePagesePagese", ci), Convert.ToInt32(MenyrePagese.Pagese)},
                {rm.GetString("cmbMenyrePageseAutomatike", ci), Convert.ToInt32(MenyrePagese.Pagese_Automatike)},
                {rm.GetString("cmbMenyrePageseMeParapagim", ci), Convert.ToInt32(MenyrePagese.Me_parapagim)},
                {rm.GetString("cmbMenyrePageseArke", ci), Convert.ToInt32(MenyrePagese.Arke)},
                {rm.GetString("cmbMenyrePageseKarteKrediti", ci), Convert.ToInt32(MenyrePagese.Karte_krediti)},
                {rm.GetString("cmbMenyrePagesePezull", ci), Convert.ToInt32(MenyrePagese.Pezull)},
                {rm.GetString("cmbMenyrePageseBanke", ci), Convert.ToInt32(MenyrePagese.Banke)},
            };
            return lista;
        }

        public static ListEditItemCollection MerrItemsPerDorezime(ResourceManager rm, CultureInfo ci)
        {
            var lista = new ListEditItemCollection
            {
                {"",0},
                {rm.GetString("cmbboxItemFilterAvancPo", ci), 1},
                {rm.GetString("cmbDorezuarDhePaguar", ci), 2},
                {rm.GetString("cmbDorezuarDheJoPaguar", ci), 3}
            };

            return lista;
        }

        public static ListEditItemCollection MerrItemsPershtoTitull(ResourceManager rm, CultureInfo ci)
        {
            var lista = new ListEditItemCollection
            {
                {"",0},
                {rm.GetString("lblComboKompani", ci), 1},
                {rm.GetString("lblComboPersonFizik", ci), 2},
                {rm.GetString("lblComboKlientRastesishem", ci), 3},
                {rm.GetString("lblComboInstitucionBuxhetor", ci), 4},
                {rm.GetString("lblComboSHPK", ci), 5}
            };
            return lista;
        }

        public static ListEditItemCollection MerrItemsPerLlojPorosie(ResourceManager rm, CultureInfo ci)
        {
            var lista = new ListEditItemCollection
            {
                {"",0},
                {rm.GetString("msgLlojPorosieAparate", ci), Convert.ToInt32(LlojPorosie.Aparate)},
                {rm.GetString("msgLlojPorosieKarta", ci), Convert.ToInt32(LlojPorosie.Karta)},
                {rm.GetString("msgLlojPorosieLoan", ci), Convert.ToInt32(LlojPorosie.Loan)},
                {rm.GetString("msgLlojPorosieDhurate", ci), Convert.ToInt32(LlojPorosie.Dhurate)},
                {"Te gjitha", Convert.ToInt32(LlojPorosie.TeGjitha)},
                  {"Aparate ekspozitore", Convert.ToInt32(LlojPorosie.Aparate_ekspozitore)}
            };
            return lista;
        }

        public static ListEditItemCollection MerrItemsPerShtoProspekt(ResourceManager rm, CultureInfo ci)
        {
            var lista = new ListEditItemCollection
            {
              //  {"",0},
                {rm.GetString("draftKF", ci), true},
                {rm.GetString("draftFinanciar", ci), false}
            };
            return lista;
        }


        public static ListEditItemCollection MerrItemsPerStatusRezervimi(ResourceManager rm, CultureInfo ci)
        {
            var lista = new ListEditItemCollection
            {
                //{"",0},
                {rm.GetString("cmbNeProces", ci), 0},
                {rm.GetString("cmbEkzekutuar", ci), 1},
                {rm.GetString("cmbAnulluar", ci), 2}
            };
            return lista;
        }

        public static ListEditItemCollection MerrItemsLlojHyrjeDaje(ResourceManager rm, CultureInfo ci)
        {
            var lista = new ListEditItemCollection
            {
              //  {"",0},
                {rm.GetString("lblHyrje", ci), 1},
                {rm.GetString("lblDalje", ci), 2}
            };
            return lista;
        }

        public static ListEditItemCollection MerrItemsPerMenyreTatimi(ResourceManager rm, CultureInfo ci)
        {
            var lista = new ListEditItemCollection
            {
                {"", Convert.ToInt32(Menyra.Undefined)},
                {rm.GetString("cmbMenyreProgresive", ci), Convert.ToInt32(Menyra.Progresive)},
                {rm.GetString("cmbMenyreTotale", ci), Convert.ToInt32(Menyra.Totale)}
            };
            return lista;
        }

        public static ListEditItemCollection MerrItemSipasPeriudhesSeVitit(ResourceManager rm, CultureInfo ci)
        {
            var lista = new ListEditItemCollection
            {
                {"",0},
                {rm.GetString("cmbItemNjeMujore", ci), 1},
                {rm.GetString("cmbItemDyMujore", ci), 2},
                {rm.GetString("cmbItemTreMujore", ci), 3},
                {rm.GetString("cmbItemKaterMujore", ci), 4},
                {rm.GetString("cmbItemGjashteMujore", ci), 5}
            };

            return lista;
        }

        public static ListEditItemCollection MerrItemSipasQendresKosto(ResourceManager rm, CultureInfo ci)
        {
            var lista = new ListEditItemCollection
            {
                {rm.GetString("cmbVleraQendraKosto", ci), 1},
                {rm.GetString("cmbVleraSkemaQendraKosto", ci), 2}
            };
            return lista;
        }

        public static ListEditItemCollection MerrTreMujoret()
        {
            var lista = new ListEditItemCollection
            {
                {"Tremujori 1", 1},
                {"Tremujori 2", 2},
                {"Tremujori 3", 3},
                {"Tremujori 4", 4}
            };
            return lista;
        }
        public static ListEditItemCollection MerrStatuse()
        {
            var lista = new ListEditItemCollection
            {
                {"Draft", 0},
                {"Ruajtur", 1}
            };
            return lista;
        }

        /// <summary>
        /// Creates the items from enumeration.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentException">T must be an enumerated type</exception>
        public static ListEditItemCollection CreateItemsFromEnumeration<T>() where T : struct, IComparable
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("T must be an enumerated type");

            var list = new ListEditItemCollection();

            foreach (T e in Enum.GetValues(typeof(T)))
            {
                if (Convert.ToInt32(e) < 0 || e.ToString() == "Undefined")
                    continue;

                list.Add(MessagesResource.Messages[e.ToString()], Convert.ToInt32(e));
            }

            return list;
        }

        public static ListEditItemCollection MerrItemsPerGjine(ResourceManager rm, CultureInfo ci)
        {

            var lista = new ListEditItemCollection
            {
              //  {"",0},
                {rm.GetString("cmbGjiniaFemer", ci), Convert.ToBoolean(Gjinia.Femer)},
                {rm.GetString("cmbGjiniaMashkull", ci), Convert.ToBoolean(Gjinia.Mashkull)}
            };
            return lista;
        }

        public static ListEditItemCollection MerrItemsPerPunen(ResourceManager rm, CultureInfo ci)
        {

            var lista = new ListEditItemCollection
            {
                {"",0},
                {rm.GetString("cmbPunaMeparshmePublik",  ci), Convert.ToInt32(PunaMeparshme.Publik)},
                {rm.GetString("cmbPunaMeparshmePrivat", ci), Convert.ToInt32(PunaMeparshme.Privat)},
                {rm.GetString("cmbPunaMeparshmeEkspPare", ci), Convert.ToInt32(PunaMeparshme.Eksperienca_Pare)},
                {rm.GetString("Papunesia",  ci), Convert.ToInt32(PunaMeparshme.Papunesia)},
                {rm.GetString("Te_tjera", ci), Convert.ToInt32(PunaMeparshme.Te_tjera)},
                {rm.GetString("Page_Papunesie", ci), Convert.ToInt32(PunaMeparshme.Page_Papunesie)}
            };
            return lista;
        }
        public static ListEditItemCollection MerrItemsPerPikeShitjeFurnizimi(ResourceManager rm, CultureInfo ci)
        {

            var lista = new ListEditItemCollection
            {
                {rm.GetString("pikeShitjeTab", ci), true},
                {rm.GetString("msgPikeFurnizimi", ci), false}

            };
            return lista;
        }
        public static ListEditItemCollection MerrItemsPerPrioritetNivelZbritje(ResourceManager rm, CultureInfo ci)
        {

            var lista = new ListEditItemCollection
            {
                {"",0},
                {"1", 1},
                {"2", 2},
                {"3", 3},
                {"4", 4},
                {"5", 5},
                {"6", 6},
                {"7", 7},
                {"8", 8},
                {"9", 9},
                {"10", 10},
                {"11", 11},
                {"12", 12},
                {"13", 13},
                {"14", 14},
                {"15", 15},
                {"16", 16},
                {"17", 17},
                {"18", 18},
                {"19", 19},
                {"20", 20}
            };
            return lista;

        }

        public static ListEditItemCollection MerrItemsPerShtoLlojeKursesh(ResourceManager rm, CultureInfo ci)
        {

            var lista = new ListEditItemCollection
            {
                {"Kursi1", 1},
                {"Kursi2", 2},
                {"Kursi3", 3},
                {"Kursi4", 4},
                {"Kursi5", 5},
                {"Kursi6", 6},
                {"Kursi7", 7},
                {"Kursi8", 8},
                {"Kursi9", 9},
                {"Kursi10", 10},
                {"Kursi11", 11},
                {"Kursi12", 12},
                {"Kursi13", 13},
                {"Kursi14", 14},
                {"Kursi15", 15},
                {"Kursi16", 16},
                {"Kursi17", 17},
                {"Kursi18", 18},
                {"Kursi19", 19},
                {"Kursi20", 20}
            };
            return lista;

        }
        public static ListEditItemCollection MerrItemsPerShto_Njesi(ResourceManager rm, CultureInfo ci)
        {

            var lista = new ListEditItemCollection
            {
              //  {"",0},
                {rm.GetString("cmbItemBlerjeShitjevlere", ci), 1},
                {rm.GetString("cmbItemBlerjeShitjeperqindje", ci), 2}

            };
            return lista;
        }
        public static ListEditItemCollection MerrItemsPerKlientFurnitorArtikull(ResourceManager rm, CultureInfo ci)
        {

            var lista = new ListEditItemCollection
            {
                {"", Convert.ToInt32(DbCore.DbAdmin.LlojInfo.Undefined)},
                {rm.GetString("cmbArtikulli", ci), Convert.ToInt32(LlojInfo.Artikulli)},
                {rm.GetString("cmbKlientFurnitor", ci), Convert.ToInt32(LlojInfo.KlientFurnitori)},
                {rm.GetString("cmbLlogari", ci), Convert.ToInt32(LlojInfo.Llogari)}
            };
            return lista;
        }
        public static ListEditItemCollection MerrItemsPerTipeBurimesh(ResourceManager rm, CultureInfo ci)
        {

            var lista = new ListEditItemCollection
            {
                {"", Convert.ToInt32(TipBurimi.Undefined)},
                {rm.GetString("cmbTipBurimiMakineri", ci), Convert.ToInt32(TipBurimi.Makineri)},
                {rm.GetString("cmbTipBurimiMjet", ci), Convert.ToInt32(TipBurimi.Mjet)},
                {rm.GetString("cmbTipBurimiPunonjes", ci), Convert.ToInt32(TipBurimi.Punonjes)}
            };
            return lista;
        }
        public static ListEditItemCollection MerrItemsPerLlojeSeriale(ResourceManager rm, CultureInfo ci)
        {

            var lista = new ListEditItemCollection
            {
                {"", Convert.ToInt32(enumSerialeUnike_Lloje.PA_KUFIZIM)},
                {"Alfanumerike", Convert.ToInt32(enumSerialeUnike_Lloje.ALFANUMERIKE)},
                {"Numerike", Convert.ToInt32(enumSerialeUnike_Lloje.NUMERIKE)},
                {"Karaktere Speciale", Convert.ToInt32(enumSerialeUnike_Lloje.KARAKTERE_SPECIALE)}
            };
            return lista;
        }

        public static ListEditItemCollection MerrItemsPerNjesiKohe(ResourceManager rm, CultureInfo ci)
        {
            var lista = new ListEditItemCollection
            {
                {"", Convert.ToInt32(NjesiKohe.Undefined)},
                {rm.GetString("listItemSekonda", ci), Convert.ToInt32(NjesiKohe.Sekonda)},
                {rm.GetString("listItemMinuta", ci), Convert.ToInt32(NjesiKohe.Minuta)},
                {rm.GetString("listItemOre", ci), Convert.ToInt32(NjesiKohe.Ore)},
                {rm.GetString("cmbFormuleDite", ci), Convert.ToInt32(NjesiKohe.Dite)}
        };
            return lista;
        }
        public static ListEditItemCollection MerrItemsPerNjesiPagese(ResourceManager rm, CultureInfo ci)
        {

            var lista = new ListEditItemCollection
            {
                {"", Convert.ToInt32(DbCore.DbListPagesat.NjesiPagese.Undefined)},
                {DbCore.DbListPagesat.NjesiPagese.Nr.ToString(), Convert.ToInt32(DbCore.DbListPagesat.NjesiPagese.Nr)},
                {DbCore.DbListPagesat.NjesiPagese.Tab.ToString(), Convert.ToInt32(DbCore.DbListPagesat.NjesiPagese.Tab)},
                {DbCore.DbListPagesat.NjesiPagese.For.ToString(), Convert.ToInt32(DbCore.DbListPagesat.NjesiPagese.For)}
            };
            return lista;

        }
        public static ListEditItemCollection MerrItemsPerTipPagese(ResourceManager rm, CultureInfo ci)
        {

            var lista = new ListEditItemCollection
            {
                {"", Convert.ToInt32(DbCore.DbListPagesat.TipPagese.Undefined)},
                { rm.GetString("lblPagese", ci), Convert.ToInt32(TipPagese.Pagese)},
                { rm.GetString("lblNdalese", ci), Convert.ToInt32(DbCore.DbListPagesat.TipPagese.Ndalese)},
                {rm.GetString("lblLlogaritese", ci), Convert.ToInt32(DbCore.DbListPagesat.TipPagese.Llogaritese)}
            };
            return lista;
        }
        public static ListEditItemCollection MerrItemsPerLlojKomponentePage(ResourceManager rm, CultureInfo ci)
        {

            var lista = new ListEditItemCollection
            {
                {DbCore.DbListPagesat.LlojKomponentePage.Page.ToString(), false},
                {DbCore.DbListPagesat.LlojKomponentePage.ListPagese.ToString(), true}

            };
            return lista;
        }
        public static ListEditItemCollection MerrItemsPershtoAktiv(ResourceManager rm, CultureInfo ci)
        {

            var lista = new ListEditItemCollection
            {
                {rm.GetString("Aktive", ci), true},
                {rm.GetString("Inaktive", ci), false}

            };
            return lista;
        }
        public static ListEditItemCollection MerrItemsPershtoNjesiParam(ResourceManager rm, CultureInfo ci)
        {

            var lista = new ListEditItemCollection
            {
                {"Nr", 0},
                {"Nr", 1}

            };
            return lista;
        }
        public static ListEditItemCollection MerrItemsPerKategoriDetajimi(ResourceManager rm, CultureInfo ci)
        {

            var lista = new ListEditItemCollection
            {

                {"Detajim", 1},
                {"Serial", 2},
                {"Date skadence", 3},
                {"Seri", 4}


            };
            return lista;
        }
        public static ListEditItemCollection MerrItemsPerLlojDetajimArtikulli(ResourceManager rm, CultureInfo ci)
        {

            var lista = new ListEditItemCollection
            {

                {"Alfanumerik", 1},
                {"Numerik", 2},
                {"Date", 3}

            };
            return lista;
        }
        public static ListEditItemCollection MerrItemsPerAktivePoOseJo(ResourceManager rm, CultureInfo ci)
        {

            var lista = new ListEditItemCollection
            {

                {"Po", true},
                {"Jo", false}

            };
            return lista;
        }
        public static ListEditItemCollection MerrItemsPerFormatNumrash(ResourceManager rm, CultureInfo ci)
        {

            var lista = new ListEditItemCollection
            {
                 {"0.0", 1},
                 {"0.00", 2},
                 {"0.000", 3},
                 {"0.0000", 4},
                 {"0.00000", 5},
                 {"0.000000", 6},
                 {"0.0000000", 7},
                 {"0.00000000", 8},
                 {"0.000000000", 9},
                 {"0.0000000000", 10}

            };
            return lista;
        }
        public static ListEditItemCollection MerrStatusPerPozicionPune(ResourceManager rm, CultureInfo ci)
        {
            var lista = new ListEditItemCollection
            {
                {"",0},
                {rm.GetString("Shef_departamenti", ci), 1},
                {rm.GetString("Punonjes", ci), 2},
            };
            return lista;
        }
        public static ListEditItemCollection MerrItemsPerNjesiKomponenteLP(ResourceManager rm, CultureInfo ci)
        {
            var lista = new ListEditItemCollection
            {
                // {"",0},
                {rm.GetString("labelRaportNr", ci), Convert.ToInt32(NjesiPagese.Nr)},
                {rm.GetString("listItemTab", ci), Convert.ToInt32(NjesiPagese.Tab)},
                {rm.GetString("listItemFormula", ci), Convert.ToInt32(NjesiPagese.For)}
            };
            return lista;
        }
        #endregion static combo items

        public static void shtoAktivPoOseJo(ASPxGridView grid, ResourceManager rm, CultureInfo ci, string fieldName = "Aktiv")
        {
            grid.KonfiguroComboMeItems(fieldName, () => MerrItemsPerAktivePoOseJo(rm, ci));
        }

        public static void shtoFormatNr(ASPxGridView grid, ResourceManager rm, CultureInfo ci, string fieldName)
        {
            grid.KonfiguroComboMeItems(fieldName, () => MerrItemsPerFormatNumrash(rm, ci));
        }
        public static void shto_LlojDetajimArtikulli(ASPxGridView grid, ResourceManager rm, CultureInfo ci, string fieldName = "LlojDetajimArtikulli")
        {
            grid.KonfiguroComboMeItems(fieldName, () => MerrItemsPerLlojDetajimArtikulli(rm, ci));
        }
        public static void shto_KategoriDetajimi(ASPxGridView grid, ResourceManager rm, CultureInfo ci, string fieldName = "KategoriDetajimi")
        {
            grid.KonfiguroComboMeItems(fieldName, () => MerrItemsPerKategoriDetajimi(rm, ci));
        }
        public static void shtoNjesiKohe(ASPxGridView grid, ResourceManager rm, CultureInfo ci, string fieldName = "NjesiKohe")
        {
            grid.KonfiguroComboMeItems(fieldName, () => MerrItemsPerNjesiKohe(rm, ci));
        }
        public static void shtoNjesiParam(ASPxGridView grid, ResourceManager rm, CultureInfo ci, string fieldName = "ParamNjesi")
        {
            grid.KonfiguroComboMeItems(fieldName, () => MerrItemsPershtoNjesiParam(rm, ci));
        }

        public static void shtoAktiv(ASPxGridView grid, ResourceManager rm, CultureInfo ci, string fieldName = "Aktivizimi")
        {
            grid.KonfiguroComboMeItems(fieldName, () => MerrItemsPershtoAktiv(rm, ci));
        }
        public static void shtoLlojKomponentePage(ASPxGridView grid, ResourceManager rm, CultureInfo ci, string fieldName = "Lloji")
        {
            grid.KonfiguroComboMeItems(fieldName, () => MerrItemsPerLlojKomponentePage(rm, ci));
        }

        public static void shtoTipPagese(ASPxGridView grid, ResourceManager rm, CultureInfo ci, string fieldName = "Tipi")
        {
            grid.KonfiguroComboMeItems(fieldName, () => MerrItemsPerTipPagese(rm, ci));
        }
        public static void shtoNjesiPagese(ASPxGridView grid, ResourceManager rm, CultureInfo ci, string fieldName = "Njesi")
        {
            grid.KonfiguroComboMeItems(fieldName, () => MerrItemsPerNjesiPagese(rm, ci));
        }
        public static void shtoTipBurimi(ASPxGridView grid, ResourceManager rm, CultureInfo ci, string fieldName = "Tipi")
        {
            grid.KonfiguroComboMeItems(fieldName, () => MerrItemsPerTipeBurimesh(rm, ci));
        }
        public static void shtoLlojSeriali(ASPxGridView grid, ResourceManager rm, CultureInfo ci, string fieldName = "LlojSeriali")
        {
            grid.KonfiguroComboMeItems(fieldName, () => MerrItemsPerLlojeSeriale(rm, ci));
        }
        public static void shtoLlojiKFArtikull(ASPxGridView grid, ResourceManager rm, CultureInfo ci, string fieldName = "Lloji")
        {
            grid.KonfiguroComboMeItems(fieldName, () => MerrItemsPerKlientFurnitorArtikull(rm, ci));
        }
        public static void shtoLlojeKursesh(ASPxGridView grid, ResourceManager rm, CultureInfo ci, string fieldName = "LlojKursi")
        {
            grid.KonfiguroComboMeItems(fieldName, () => MerrItemsPerShtoLlojeKursesh(rm, ci));
        }

        public static void shto_Njesi(ASPxGridView grid, ResourceManager rm, CultureInfo ci, string fieldName = "Njesia")
        {
            grid.KonfiguroComboMeItems(fieldName, () => MerrItemsPerShto_Njesi(rm, ci));
        }
        public static void shtoPrioritet(ASPxGridView grid, ResourceManager rm, CultureInfo ci, string fieldName = "PrioritetiNivelZbritje")
        {
            grid.KonfiguroComboMeItems(fieldName, () => MerrItemsPerPrioritetNivelZbritje(rm, ci));
        }
        public static void ShtoStatus(ASPxGridView grid, ResourceManager rm, CultureInfo ci, string fieldName = "IdStatusDok")
        {
            grid.KonfiguroComboMeItems(fieldName, () => MerrItemsPerStatusDokumenti(rm, ci));
        }
        public static void ShtoStatusDokBuxheti(ASPxGridView grid, ResourceManager rm, CultureInfo ci, int idKatDok, string fieldName = "IdStatusDok")
        {
            grid.KonfiguroComboMeItems(fieldName, () => MerrItemsPerStatusDokumentBuxheti(rm, ci, idKatDok));
        }
        public static void ShtoLlojVeprimiDokBuxheti(ASPxGridView grid, int idKatDok, string fieldName = "LlojVeprimiGjenerimi")
        {
            grid.KonfiguroComboMeItems(fieldName, () => MerrItemsPerLlojVeprimiDokumentBuxheti(idKatDok));
        }
        public static void shtoPikeShitjeFurnizimi(ASPxGridView grid, ResourceManager rm, CultureInfo ci, string fieldName = "ShitjeFurnizimi")
        {
            grid.KonfiguroComboMeItems(fieldName, () => MerrItemsPerPikeShitjeFurnizimi(rm, ci));
        }
        public static void shtoSeks(ASPxGridView grid, ResourceManager rm, CultureInfo ci, string fieldName = "Gjinia")
        {
            grid.KonfiguroComboMeItems(fieldName, () => MerrItemsPerGjine(rm, ci));
        }
        public static void shtoTreMujoret(ASPxGridView grid, string fieldName = "TreMujori")
        {
            grid.KonfiguroComboMeItems(fieldName, () => MerrTreMujoret());
        }
        public static void shtoStatuse(ASPxGridView grid, string fieldName = "IdStatusDok")
        {
            grid.KonfiguroComboMeItems(fieldName, () => MerrStatuse());
        }
        public static void shtoPuna(ASPxGridView grid, ResourceManager rm, CultureInfo ci, string fieldName = "PunaMeparshme")
        {
            grid.KonfiguroComboMeItems(fieldName, () => MerrItemsPerPunen(rm, ci));
        }
        public static void ShtoStatusAprovimi(ASPxGridView grid, ResourceManager rm, CultureInfo ci, string fieldName = "StatusAprovimi")
        {
            grid.KonfiguroComboMeItems(fieldName, () => MerrItemsPerStatusAprovimi(rm, ci));
        }
        public static void ShtoStatusMarreveshje(ASPxGridView grid, string fieldName = "StatusMarreveshje")
        {
            grid.KonfiguroComboMeItems(fieldName, () =>
            {
                var list = new ListEditItemCollection
                {
                    {"", 0}
                };
                list.AddRange(CreateItemsFromEnumeration<StatusMarreveshje>());
                list.Add(MessagesResource.Messages["Skaduar"], 4);
                return list;
            });
        }
        public static void ShtoStatusAprovimi_Perd(ASPxGridView grid, ResourceManager rm, CultureInfo ci, string fieldName = "StatusAprovimi")
        {
            grid.KonfiguroComboMeItems(fieldName, () => MerrItemsPer_shtoStatusAprovimi_Perd(rm, ci));
        }

        public static void shto_Gjini(ASPxGridView grid, ResourceManager rm, CultureInfo ci, string fieldName = "Gjinia")
        {
            grid.KonfiguroComboMeItems(fieldName, () => MerrItems_shtoGjini(rm, ci));
        }
        public static void Shto_Gjuhe(ASPxGridView grid, ResourceManager rm, CultureInfo ci, string fieldName = "IdGjuha")
        {
            grid.KonfiguroComboMeItems(fieldName, () => MerrItems_shtoGjuhe(rm, ci));
        }
        public static void shtoISigururar(ASPxGridView grid, ResourceManager rm, CultureInfo ci, string fieldName = "IsInsured")
        {
            grid.KonfiguroComboMeItems(fieldName, () => MerrItems_shtoISigururar(rm, ci));
        }

        public static void ShtoStatusRezervimi(ASPxGridView grid, ResourceManager rm, CultureInfo ci, string fieldName = "Statusi")
        {
            grid.KonfiguroComboMeItems(fieldName, () => MerrItemsPerStatusRezervimi(rm, ci));
        }

        public static void ShtoMuaj(ASPxGridView grid, ResourceManager rm, CultureInfo ci, string fieldName = "Muaji")
        {
            grid.KonfiguroComboMeItems(fieldName, () => MerrMuajTePerkthyer(rm, ci));
        }
        public static void shtoStatusPunesimi(ASPxGridView grid, ResourceManager rm, CultureInfo ci, string fieldName = "Statusi")
        {
            grid.KonfiguroComboMeItems(fieldName, () => MerrStatusPerPozicionPune(rm, ci));
        }

        public static void shtoLlojNivelCmimi(ASPxGridView grid, ResourceManager rm, CultureInfo ci)
        {
            grid.KonfiguroComboMeItems("LlojiNivelCmimi", () => new ListEditItemCollection
            {
                {"", -1},
                {rm.GetString("cmbCmimShitje", ci), 0},
                {rm.GetString("cmbCmimBlerje", ci), 1}
            });
        }
        public static void ShtoImportExport(ASPxGridView grid, ResourceManager rm, CultureInfo ci)
        {
            grid.KonfiguroComboMeItems("ImportExport", () => new ListEditItemCollection
            {
                {rm.GetString("cmbImport", ci), 1},
                {rm.GetString("cmbExport", ci), 2}
            });
        }

        public static void shtoBrutoNeto(ASPxGridView grid, ResourceManager rm, CultureInfo ci)
        {
            grid.KonfiguroComboMeItems("BrutoNetoNivelCmimi", () => new ListEditItemCollection
            {
               {"",0},
                {rm.GetString("cmbTVSH", ci), 1}
            });
        }
        public static void shtoDetajim(ASPxGridView grid)
        {
            grid.KonfiguroComboMeItems("Detajim", () => new ListEditItemCollection
            {
                {"",(int)Detajim.PaDetajim},
                {MessagesResource.Messages["lblDetajim1"], (int)Detajim.Detajim1},
                {MessagesResource.Messages["lblDetajim2"], (int)Detajim.Detajim2}
            });
        }
        public static void shtoLlojPagese(ASPxGridView grid, ResourceManager rm, CultureInfo ci)
        {
            grid.KonfiguroComboMeItems("LlojiKushtPagese", () => new ListEditItemCollection
            {
                {rm.GetString("cmbKushtPagesePlote", ci), true},
                {rm.GetString("cmbKushtPagesePjese",ci), false}
            });
        }
        public static void shtoKolonePeridhaInfoArtikulli(ASPxGridView grid, ResourceManager rm, CultureInfo ci)
        {
            grid.KonfiguroComboMeItems("Periudha", () => new ListEditItemCollection
            {
                {"", -1},
                {rm.GetString("cmbDataFatures", ci), 0},
                {rm.GetString("cmbVitiUshtrimor", ci), 1}
            });
        }


        public static void shtoLlojiArtikullitLup(ASPxGridView grid, ResourceManager rm, CultureInfo ci)
        {
            grid.KonfiguroComboMeItems("LlojiArt", () => new ListEditItemCollection
            {
                {"Afatgjate", 1},
                {"Afatshkurter", 0}
            });
        }
        public static void shtoLlojiArtikullit(ASPxGridView grid, ResourceManager rm, CultureInfo ci)
        {
            grid.KonfiguroComboMeItems("LlojiArt", () => new ListEditItemCollection
            {
                {"Afatgjate", true},
                {"Afatshkurter",false}
            });
        }

        public static void shtoMetodeKonfigurimiFTP(ASPxGridView grid)
        {
            grid.KonfiguroComboMeItems("Metoda", () => new ListEditItemCollection
            {
                {"Manuale", 0},
                {"Automatike", 1},
                {"Automatike per Gjendje Ditore Aparate", 2},
                {"Automatike per Gjendje Mujore Aparate", 3},
                {"Manuale per Karta Old", 4},
                {"Automatike per Gjendje Ditore Aparate 2", 5},
                {"Automatike per Gjendje Mujore Aparate 2", 6},
            });
        }
        public static void shtoKategoriWebhook(ASPxGridView grid)
        {
            grid.KonfiguroComboMeItems("Kategoria", () => new ListEditItemCollection
            {
                {"Blerje", 0},
                {"Shitje", 1},
                {"Arketime", 2},
                {"Pagesa", 3},
                {"Login", 4},

            });
        }
        public static void shtoEventWebhook(ASPxGridView grid)
        {
            grid.KonfiguroComboMeItems("Eventi", () => new ListEditItemCollection
            {
                {"Te gjitha", 0},
                {"Shtim", 1},
                {"Modifikim", 2},
                {"Fshirje", 3},

            });
        }
        public static void shto_AplikimDhurate(ASPxGridView grid, ResourceManager rm, CultureInfo ci)
        {
            grid.KonfiguroComboMeItems("AplikimDhurate", () => new ListEditItemCollection
            {
                {"Me Pike", 1},
                {"Pike + Pagese", 2}
            });
        }
        public static void shtoLlojBankeArke(ASPxGridView grid, ResourceManager rm, CultureInfo ci)
        {
            grid.KonfiguroComboMeItems("LlojArkaBanka", () => new ListEditItemCollection
            {
                {"Banka", true},
                {"Arka", false}
            });
        }
        public static void shtoNjesiKomponenteLP(ASPxGridView grid, ResourceManager rm, CultureInfo ci, string fieldName = "Njesia")
        {
            grid.KonfiguroComboMeItems(fieldName, () => MerrItemsPerNjesiKomponenteLP(rm, ci));
        }
        public static void ShtoNivel(ASPxGridView grid, int idKategoria, int idndermarje, int idperdoruesi, int idGjuha, HttpSessionState session, string komponente, string guidString, string fieldName = "IdNivel")
        {
            ShtoNivelMeDataSource(grid, () => colNivelRegjistrimi.ktheGjitheNivelRegjistrimiSipasKategoriDtCombo(idKategoria, idndermarje, idperdoruesi, true), session, komponente, guidString, fieldName);
        }

        public static void ShtoNivelMeDataSource<TDataSource>(ASPxGridView grid, Func<TDataSource> ds, HttpSessionState session, string komponente, string guidString, string fieldName = "IdNivel", string caption = "Pershkrimi")
        {
            grid.KonfiguroCombo(fieldName, "IdNivel", caption, ds, session, komponente, guidString);
        }

        public static void ShtoZeratAnalizeBuxheti(ASPxGridView grid, int idNdermarrjeRaportuese, int idAmbjenti, HttpSessionState Session, string komponente, string guidString, string fieldName)
        {
            grid.KonfiguroCombo(fieldName, "RreshtiId", "PershkrimiRreshtit", () =>
            {
                return new DbCore.DbAnalizeBuxheti.colRreshtaAmbjenti(idNdermarrjeRaportuese, 11);
            }, Session, komponente, guidString);
        }
        public static void ShtoModel(ASPxGridView grid, int idKategoria, int idndermarje, int idperdoruesi, int idGjuha, HttpSessionState session, string komponente, string guidString, string fieldName = "IdKonfigAmbjente")
        {
            ShtoModelMeDataSource(grid, () =>
            {
                var colKonfig = new colKonfigurimAmbjenti();
                colKonfig.mbushKonfigAmbjSipasIdKategori(idKategoria, idndermarje, idperdoruesi, idGjuha);
                return colKonfig;
            }, session, komponente, guidString, fieldName);
        }

        public static void ShtoDoktor(ASPxGridView grid, int idndermarje, HttpSessionState session, string komponente, string guidString, string fieldName = "Doktori")
        {
            grid.KonfiguroCombo(fieldName, "IdAutomjeti", "Targa", () =>
            {
                var colAutomjete = new colAutomjete();
                colAutomjete.AddIfNotExists(new clsAutomjete());
                colAutomjete.mbushAutomjetetSipasNdermarrjes(idndermarje);
                return colAutomjete;
            }, session, komponente, guidString);
        }

        public static void ShtoModelMeDataSource<TDataSource>(ASPxGridView grid, Func<TDataSource> ds, HttpSessionState session, string komponente, string guidString, string fieldName = "IdKonfigAmbjente")
        {
            grid.KonfiguroCombo(fieldName, "IdKonfigAmbjente", "KodKonfigAmbjente", ds, session, komponente, guidString);
        }

        public static void ShtoMonedhe(ASPxGridView grid, int idndermarje, int idperdoruesi, HttpSessionState session, string komponente, string guidString, string fieldName = "IdMonedha")
        {
            grid.KonfiguroCombo(fieldName, "IdMonedha", "KodiMonedha", () =>
            {
                var colMonedhat = new colMonedhat();
                colMonedhat.mbushGjitheMonedhatAktive(idndermarje, idperdoruesi);
                return colMonedhat;
            }, session, komponente, guidString);
        }

        public static void shtoKPF_D(ASPxGridView grid, int idndermarje, int grupikpf, int idperdoruesi, HttpSessionState session, string komponente, string guidString, string fieldName = "KPF1")
        {
            grid.KonfiguroCombo(fieldName, "IdKPF", "KodiKPF", () =>
            {
                var colKPF = new DbCore.DbKontabiliteti.colKPFte();
                colKPF.mbushGjitheKPFteSipasGrupitPozitiveAndAutorizimeAktive(grupikpf, idndermarje, idperdoruesi);
                return colKPF;
            }, session, komponente, guidString);
        }
        public static void shtoGrupetLlogaria(ASPxGridView grid, int idNdermarrje, int idGjuha, HttpSessionState session, string komponente, string guidString, string fieldName = "Grupi")
        {
            grid.KonfiguroCombo(fieldName, "IdGrupiLlogaria", "PershkrimiGrupiLlogaria", () =>
            {
                var colGrupeLlogaria = new DbCore.DbKontabiliteti.colGrupetLlogaria(idNdermarrje, idGjuha);
                colGrupeLlogaria[0].PershkrimiGrupiLlogaria = "";
                colGrupeLlogaria[0].IdGrupiLlogaria = -3;
                return colGrupeLlogaria;
            }, session, komponente, guidString);
        }

        public static void shtoNenGrupetLlogaria(ASPxGridView grid, int idNdermarrje, int idGjuha, HttpSessionState session, string komponente, string guidString, string fieldName = "Nengrupi")
        {
            grid.KonfiguroCombo(fieldName, "IdNenGrupiLlogaria", "PershkrimiNenGrupiLlogaria", () =>
            {
                var colNenGrupeLlogaria = new DbCore.DbKontabiliteti.colNenGrupetLlogaria();
                colNenGrupeLlogaria.mbushGjitheNenGrupetLlogaria(idNdermarrje, idGjuha);
                colNenGrupeLlogaria[0].PershkrimiNenGrupiLlogaria = "";
                colNenGrupeLlogaria[0].IdNenGrupiLlogaria = -3;
                return colNenGrupeLlogaria;
            }, session, komponente, guidString);
        }

        public static void ShtoBanke(ASPxGridView grid, int idndermarje, int idKategoria, HttpSessionState session, string komponente, string guidString, string fieldName = "IdBanka")
        {
            grid.KonfiguroCombo(fieldName, "IdBanka", "KodiBanka", () =>
            {
                var colBankat = new colBankat();
                colBankat.mbushGjitheBankatSipasAutorizimeveSipasLlojitAll(idndermarje, idKategoria);
                return colBankat;
            }, session, komponente, guidString);
        }

        public static void ShtoLlojVeprimiArketime(ASPxGridView grid, HttpSessionState session, string komponente, string guidString, string fieldName = "LlojiVeprimit")
        {

            grid.KonfiguroCombo(fieldName, "LlojiVeprimit", "LlojiVeprimit", () =>
            {
                var data = new clsDatabaseArkaBanka();
                var ds = data.merrLlojetVeprimeveBanka();
                data.Dispose();
                var dr = ds.Tables[0].NewRow();
                var rowArray = new object[1];
                rowArray[0] = null;
                dr.ItemArray = rowArray;
                ds.Tables[0].Rows.InsertAt(dr, 0);
                return ds;
            }, session, komponente, guidString);
        }

        public static void ShtoBurim(ASPxGridView grid, int idndermarrje, HttpSessionState session, string komponente, string guidString, string fieldName = "IdBurimi")
        {
            grid.KonfiguroCombo(fieldName, "IdBurimi", "Kodi", () =>
            {
                var colMagazinat = new colBurimet(idndermarrje);

                return colMagazinat;
            }, session, komponente, guidString);
        }

        public static void ShtoSkemaKontabel(ASPxGridView grid, int idNdermarrje, int idPerdoruesi, HttpSessionState session, string komponente, string guidString, string fieldName = "IdKokaFleteKontabel")
        {
            grid.KonfiguroCombo(fieldName, "IdKokaSkemaFK", "KodiKokaSkemaFK", () =>
            {
                var ds = new colKokatSkematFletetKontabel(idNdermarrje, idPerdoruesi);
                return ds;
            }, session, komponente, guidString);
        }

        public static void ShtoGrupetKontabilizimit(ASPxGridView grid, int idNdermarrje, HttpSessionState session, string komponente, string guidString, string fieldName = "IdGrupKontabilizimi")
        {
            grid.KonfiguroCombo(fieldName, "IdGrupKontabilizimi", "NrGrupKontabilizimi", () =>
            {
                var colGrupe = new colGrupeKontabilizimi(idNdermarrje);
                return colGrupe;
            }, session, komponente, guidString);
        }

        public static void shto_DegeAdministrative(ASPxGridView grid, int idNdermarrje, HttpSessionState session, string komponente, string guidString, string fieldName = "IdDegeAdministrative")
        {
            grid.KonfiguroCombo(fieldName, "IdDegeAdministrative", "Kodi", () =>
            {
                var dege = new colDegeAdministrative();
                dege.mbushGjitheDegeAdministrative(idNdermarrje);
                return dege;
            }, session, komponente, guidString);
        }

        public static void shto_Operatore(ASPxGridView grid, int idNdermarrje, HttpSessionState session, string komponente, string guidString, string fieldName = "IdOperator")
        {
            grid.KonfiguroCombo(fieldName, "IdOperator", "EmerMbiemer", () =>
            {
                return clsOperator.MerrOperatoretAktive(idNdermarrje);
            }, session, komponente, guidString);
        }

        public static void ShtoGrupimDokumentash(ASPxGridView grid, int idNdermarrje, int idPerdoruesi, HttpSessionState session, string komponente, string guidString, string fieldName, int grupi)
        {
            ShtoGrupimDokumentashMeDataSource(grid, () =>
            {
                var colDege = new colGrupimDokumentiKoka();
                colDege.merrGrupeSipasGrupit(grupi, idNdermarrje, idPerdoruesi);
                return colDege;
            }, session, komponente, guidString, fieldName, grupi);
        }

        public static void ShtoGrupimDokumentashMeDataSource<TDataSource>(ASPxGridView grid, Func<TDataSource> funcDs, HttpSessionState session, string komponente, string guidString, string fieldName, int grupi)
        {
            grid.KonfiguroCombo(fieldName, "IdGrupimKoka", "Kodi", funcDs, session, komponente, guidString);
        }

        public static void ShtoMagazinaNdermarrje(ASPxGridView grid, HttpSessionState session, string komponente, string guidString, string fieldName = "IdMagazina")
        {
            grid.KonfiguroCombo(fieldName, "IdNjesiAdministrative", "Kodi", () =>
            {
                var colMagazinat = new colNjesiAdministrative();
                colMagazinat.mbushGjitheNjesiAdministrative(mySessionObjects.merrIdNdermarrjeSesioni(session));
                return colMagazinat;
            }, session, komponente, guidString);
        }

        public static void ShtoMagazinaSipasPerdoruesitDheNdermarrjes(ASPxGridView grid, int idndermarje, int idperdoruesi, HttpSessionState session, string komponente, string guidString, string fieldName = "IdMagazina")
        {
            grid.KonfiguroCombo(fieldName, "IdNjesiAdministrative", "Kodi", () =>
            {
                var colMagazinat = new colNjesiAdministrative();
                colMagazinat.mbushGjitheNjesiAdministrativeAktive(idndermarje, idperdoruesi);
                return colMagazinat;
            }, session, komponente, guidString);
        }

        public static void ShtoLlojDokumentMagazine(ASPxGridView grid, HttpSessionState session, string komponente, string guidString, string fieldName = "IdLlojDokumentiMagazine")
        {
            grid.KonfiguroCombo(fieldName, "IdLlojDokumentiMagazine", "Pershkrimi", () =>
            {
                var col = new colLlojDokumentashMagazine();
                col.mbushGjitheLlojDokumentashMagazine();
                return col;
            }, session, komponente, guidString);
        }

        public static void ShtoKodifikim(ASPxGridView grid, int idNdermarrje, HttpSessionState session, string komponente, string guidString, string fieldName)
        {
            grid.KonfiguroCombo(fieldName, "IdKodifikimi", "KodKodifikimi", () =>
            {
                var kodifikimet = new colKodifikimeArtikulli();
                kodifikimet.mbushGjitheKodifikimetArtikulliSipasNdermarrjes(idNdermarrje);
                return kodifikimet;
            }, session, komponente, guidString);
        }
        public static void ShtoDetajimet(ASPxGridView grid, int idNdermarrje, int idPerdoruesi, HttpSessionState session, string komponente, string guidString, string fieldName)
        {
            grid.KonfiguroCombo(fieldName, "IdDetajimArtikulli", "KodDetajimArtikulli", () =>
            {
                var detajimet = new colDetajimeArtikulli();
                detajimet.Add(new clsDetajimArtikulli());
                detajimet.mbushDetajimeSipasNdermarrjesAndAutorizim(idNdermarrje, idPerdoruesi);
                return detajimet;
            }, session, komponente, guidString);
        }
        public static void ShtoLlojZbritje(ASPxGridView grid, HttpSessionState session, string komponente, string guidString)
        {
            grid.KonfiguroCombo("LlojZbritje", "LlojZbritje", "Pershkrimi", () =>
            {
                var zbritja = new DataTable();
                zbritja.Columns.Add("LlojZbritje");
                zbritja.Columns.Add("Pershkrimi");
                zbritja.Rows.Add(0, "Perqindje");
                zbritja.Rows.Add(1, "Vlere");
                return zbritja;
            }, session, komponente, guidString);
        }

        public static void ShtoLlogari(ASPxGridView grid, int idPerdoruesi, HttpSessionState session, string komponente, string guidString, string fieldName)
        {
            grid.KonfiguroCombo(fieldName, "IdLlogari", "NrLlogari", () =>
            {
                var colLlog = new colLlogarite();
                colLlog.mbushLLogariteNdermarrjesAndAutorizime(mySessionObjects.merrIdNdermarrjeSesioni(session), idPerdoruesi);
                return colLlog;
            }, session, komponente, guidString);
        }

        public static void ShtoStandart(ASPxGridView grid, int idndermarje, HttpSessionState session, string komponente, string guidString, string fieldName)
        {
            grid.KonfiguroCombo(fieldName, "IdStandarti", "Emertimi", () =>
            {
                var standart = new colStandarteAmortizimi(idndermarje);

                return standart;
            }, session, komponente, guidString);
        }

        public static void ShtoPikeShitjeFurnizim(ASPxGridView grid, int idNdermarrje, HttpSessionState session, string komponente, string guidString, string fieldName, string veprimi)
        {
            grid.KonfiguroCombo(fieldName, "IdPikeShitjeFurnizimi", "Kodi", () =>
            {
                DataTable dt;
                switch (veprimi)
                {
                    case "shitje":
                    case "shitjediscount":
                    case "bazaar":
                        dt = colPikaShitjeFurnizimi.mbushGjithePikaShitjeDtSmall(idNdermarrje);
                        break;
                    case "blerje":
                        dt = colPikaShitjeFurnizimi.mbushGjithePikaFurnizimiDtSmall(idNdermarrje);
                        break;
                    default:
                        dt = colPikaShitjeFurnizimi.mbushGjithePikeShitjeFurnizimiDtSmall(idNdermarrje);
                        break;
                }
                dt.Rows.InsertAt(dt.NewRow(), 0);
                return dt;
            }, session, komponente, guidString);
        }

        public static void ShtoTransportues(ASPxGridView grid, int idNdermarrje, HttpSessionState session, string komponente, string guidString, string fieldName)
        {
            grid.KonfiguroCombo(fieldName, "IdTransportues", "Emertimi", () =>
            {
                var dt = colTransportues.merrTransportuesSipasNdermarrjesDtSmall(idNdermarrje);

                return dt;
            }, session, komponente, guidString);
        }

        public static void ShtoPerdoruesSipasKrijuesit(ASPxGridView grid, int idPerdoruesi, HttpSessionState session, string komponente, string guidString, string fieldName)
        {
            grid.KonfiguroCombo(fieldName, "IdPerdorues", "PerdoruesUsername", () =>
            {
                var col = new colPerdoruesit();
                col.mbushGjithePerdoruesit(idPerdoruesi);
                return col;
            }, session, komponente, guidString);
        }

        public static void ShtoPerdorues(this ASPxGridView grid, int idPerdoruesi, HttpSessionState session, string komponente, string guidString, string fieldName = "IdPerdoruesi")
        {
            grid.KonfiguroCombo(fieldName, "IdPerdorues", "PerdoruesUsername", () =>
            {
                var idLicenca = clsLicenca.merrIdLicencePerdoruesi(idPerdoruesi);
                return colPerdoruesit.merrPerdoruesitSipasLicencesDT(idPerdoruesi, idLicenca);
            }, session, komponente, guidString);
        }

        public static void ShtoDepartament(this ASPxGridView grid, int idndermarje, HttpSessionState session, string komponente, string guidString, string fieldName = "IdDepartamenti")
        {
            grid.KonfiguroCombo(fieldName, "IdStrukturaAdm", "Emri", () =>
            {
                var col = new colStrukturatAdministrative();
                col.mbushGjitheStrukturaAdmPrindiSipasNdermarjes(idndermarje,true);

                return col;
            }, session, komponente, guidString);
        }

        public static void ShtoNenDepartament(this ASPxGridView grid, int idndermarje, HttpSessionState session, string komponente, string guidString, string fieldName = "IdNenDepartamenti")
        {
            grid.KonfiguroCombo(fieldName, "IdStrukturaAdm", "Emri", () =>
            {
                var col = new colStrukturatAdministrative();
                col.mbushGjitheStrukturaAdmSipasNdermarjes(idndermarje);

                return col;
            }, session, komponente, guidString);
        }

        public static void ShtoStatusStransferimi(ASPxGridView grid, ResourceManager rm, CultureInfo ci, string fieldName = "StatusTransferimi")
        {
            grid.KonfiguroComboMeItems(fieldName, () => MerrItemPerStatusTransferimi(rm, ci));
        }

        public static void ShtoLlojiKlientFurnitor(ASPxGridView grid, ResourceManager rm, CultureInfo ci, string fieldName = "LlojiKf")
        {
            grid.KonfiguroComboMeItems(fieldName, () => MerrItemPerLlojinKlientFurnitor(rm, ci));
        }

        public static void ShtoProdhuar(ASPxGridView grid, ResourceManager rm, CultureInfo ci, string fieldName = "Prodhuar")
        {
            grid.KonfiguroComboMeItems(fieldName, () => MerrItemPershtoProdhuar(rm, ci));
        }

        public static void ShtoMenyrePagese(ASPxGridView grid, ResourceManager rm, CultureInfo ci, string fieldName = "IdMenyrePagese")
        {
            grid.KonfiguroComboMeItems(fieldName, () => MerrItemsPerMenyrePagese(rm, ci));
        }

        public static void ShtoDateFillimi(ASPxGridView grid, string fieldName = "IdDateFillimi")
        {
            grid.KonfiguroComboMeItems(fieldName, CreateItemsFromEnumeration<DateFillimiMaturiteti>);
        }

        public static void ShtoPeriudhe(ASPxGridView grid, string fieldName = "IdPeriudha")
        {
            grid.KonfiguroComboMeItems(fieldName, CreateItemsFromEnumeration<PeriudheMaturiteti>);
        }

        public static void ShtoAutorizim(ASPxGridView grid, HttpSessionState session, string komponente, string guidString, string fieldName)
        {
            grid.KonfiguroCombo(fieldName, "IdAutorizimKoka", "KodiAutorizim", () =>
            {
                var colAutorizim = new colAutorizimetKoka();
                colAutorizim.mbushGjitheAutorizimet(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(session), DbCore.mySessionObjects.ktheIdPerdoruesi(session));
                return colAutorizim;
            }, session, komponente, guidString);
        }
        public static void ShtoAutorizimSipasPerdoruesit(ASPxGridView grid, int idPerdoruesi, HttpSessionState session, string komponente, string guidString, string fieldName)
        {
            grid.KonfiguroCombo(fieldName, "KodiAutorizim", "KodiAutorizim", () =>
            {
                var colAutorizim = new DbCore.DbAdmin.colAutorizimetKoka(idPerdoruesi);
                return colAutorizim;
            }, session, komponente, guidString);
        }
        public static void shto_Taksa(ASPxGridView grid, int idNdermarrje, int idPerdoruesi, HttpSessionState session, string komponente, string guidString, string fieldName = "KodTaksa")
        {
            grid.KonfiguroCombo(fieldName, "IdTaksa", "KodTaksa", () =>
            {
                var col = new colTaksa(idNdermarrje, DbCore.DbRegjistrim.LlojTakse.Takse_Doganore, idPerdoruesi);
                return col;
            }, session, komponente, guidString);
        }

        public static void shtoLlojModeliFushaShtese(ASPxGridView grid, HttpSessionState session, string komponente, string guidString, string fieldName = "IdLlojModeliFushaShtese")
        {
            grid.KonfiguroCombo(fieldName, "IdLlojModeliFushaShtese", "PershkrimiLlojModeliFushaShtese", () =>
            {
                var colLloji = new DbCore.DbAdmin.colLlojModeleshFushaShtese();
                colLloji.mbushGjitheLlojModeleshFushaShtesePozitive();
                return colLloji;
            }, session, komponente, guidString);
        }

        public static void Shto_NivelCmimi(ASPxGridView grid, int idndermarje, HttpSessionState session, string komponente, string guidString, string fieldName = "IdNivelCmimi")
        {
            grid.KonfiguroCombo(fieldName, "IdNivelCmimi", "PershkrimNivelCmimi", () =>
            {
                var colNivele = new colNiveleCmimesh();
                colNivele.mbushGjitheNiveleCmimeshPrindiSipasNdermarjes(idndermarje);
                return colNivele;
            }, session, komponente, guidString);
        }

        public static void ShtoQytetet(ASPxGridView grid, int idndermarje, HttpSessionState session, string komponente, string guidString, string fieldName = "QytetiKF")
        {
            grid.KonfiguroCombo(fieldName, "IdQyteti", "EmriQyteti", () =>
            {
                var colQyt = new colQytetet();
                colQyt.mbushGjitheQytetetPozitive(idndermarje);
                colQyt.Add(new DbCore.DbAdmin.clsQyteti());
                return colQyt;
            }, session, komponente, guidString);
        }

        public static void Shto_maturimi(ASPxGridView grid, int idndermarje, int idperdoruesi, bool lloji, HttpSessionState session, string komponente, string guidString, string fieldName = "MaturimiKF")
        {
            grid.KonfiguroCombo(fieldName, "IdMaturimi", "KodMaturimi", () =>
            {
                var colMaturime = new colMaturimet(lloji, idndermarje);

                return colMaturime;
            }, session, komponente, guidString);
        }

        public static void ShtoTitull(ASPxGridView grid, ResourceManager rm, CultureInfo ci, string fieldName = "TitulliKF")
        {
            grid.KonfiguroComboMeItems(fieldName, () => MerrItemsPershtoTitull(rm, ci));
        }

        public static void ShtoLlojPorosie(ASPxGridView grid, ResourceManager rm, CultureInfo ci, string fieldName = "LlojPorosie")
        {
            grid.KonfiguroComboMeItems(fieldName, () => MerrItemsPerLlojPorosie(rm, ci));
        }

        public static void ShtoLlojSipasPeriudhesSeVitit(ASPxGridView grid, ResourceManager rm, CultureInfo ci, string fieldName = "PeriudhaLloji")
        {
            grid.KonfiguroComboMeItems(fieldName, () => MerrItemSipasPeriudhesSeVitit(rm, ci));
        }

        public static void ShtoLlojHyrjeDaje(ASPxGridView grid, ResourceManager rm, CultureInfo ci, string fieldName = "Lloji")
        {
            grid.KonfiguroComboMeItems(fieldName, () => MerrItemsLlojHyrjeDaje(rm, ci));
        }

        public static void ShtoProspekt(ASPxGridView grid, ResourceManager rm, CultureInfo ci, string fieldName = "Prospekt")
        {
            grid.KonfiguroComboMeItems(fieldName, () => MerrItemsPerShtoProspekt(rm, ci));
        }

        public static void ShtoDorezuar(ASPxGridView grid, ResourceManager rm, CultureInfo ci, string fieldName = "Dorezuar")
        {
            grid.KonfiguroComboMeItems(fieldName, () => MerrItemsPerDorezime(rm, ci));
        }

        public static void ShtoLlogariSipasNdermarrjes(ASPxGridView grid, int idNdermarje, HttpSessionState session, string komponente, string guidString, bool shtoLlogariBosh, string fieldName = "IdLlogari")
        {
            grid.KonfiguroCombo(fieldName, "IdLlogari", "NrLlogari", () =>
            {
                var col = new colLlogarite();
                col.mbushLLogariteNdermarrjes(idNdermarje);
                if (shtoLlogariBosh)
                    col.Insert(0, new clsLlogari());
                return col;
            }, session, komponente, guidString);
        }

        public static void ShtoKushtPagese(ASPxGridView grid, int idNdermarje, HttpSessionState session, string komponente, string guidString, string fieldName = "IdKushtPagese")
        {
            grid.KonfiguroCombo(fieldName, "IdKoka", "EmertimiKushtPagese", () =>
            {
                var colKushtePagese = new colKushtPageseKoka(idNdermarje);
                return colKushtePagese;
            }, session, komponente, guidString);
        }

        public static void ShtoMenyreSipasTatimit(ASPxGridView grid, ResourceManager rm, CultureInfo ci, string fieldName = "Menyra")
        {
            grid.KonfiguroComboMeItems(fieldName, () => MerrItemsPerMenyreTatimi(rm, ci));
        }

        public static void shto_Formule(ASPxGridView grid, ResourceManager rm, CultureInfo ci, string fieldName = "Formula")
        {
            grid.KonfiguroComboMeItems(fieldName, () => MerrItemsPerFormule(rm, ci));
        }

        public static void ShtoMonedheSipasIdNdermarrje(ASPxGridView grid, int idNdermarrje, HttpSessionState session, string komponente, string guidString, string fieldName = "IdMonedha")
        {
            grid.KonfiguroCombo(fieldName, "IdMonedha", "KodiMonedha", () =>
            {
                var mon = new DbCore.DbAdmin.clsMonedha(DbCore.DbAdmin.clsNdermarrje.ktheIdMonedheNdermSipasID(idNdermarrje));
                mon.IdMonedha = 0;
                var colMonedhat = new DbCore.DbAdmin.colMonedhat();
                colMonedhat.Add(mon);
                return colMonedhat;
            }, session, komponente, guidString);
        }

        public static void Shto_DateFillimiAmortizimi(ASPxGridView grid, int idGjuha, HttpSessionState session, string komponente, string guidString, string fieldName = "IdFillimAmortizimi")
        {
            grid.KonfiguroCombo(fieldName, "IdPeriudheLlogAmortizimi", "Emertimi", () =>
            {
                var col = new DbCore.DbAsete.colPeriudhaLlogaritje();
                //col.Add(new DbCore.DbAsete.clsPeriudhaLlogaritje());
                col.merrPeriudhaLlogaritjeFillim(idGjuha);
                return col;
            }, session, komponente, guidString);
        }
        public static void shto_DateMbarimiAmortizimi(ASPxGridView grid, int idGjuha, HttpSessionState session, string komponente, string guidString, string fieldName = "IdMbarimAmortizimi")
        {
            grid.KonfiguroCombo(fieldName, "IdPeriudheLlogAmortizimi", "Emertimi", () =>
            {
                var col = new DbCore.DbAsete.colPeriudhaLlogaritje();
                //col.Add(new DbCore.DbAsete.clsPeriudhaLlogaritje());
                col.merrPeriudhaLlogaritjeFund(idGjuha);
                return col;
            }, session, komponente, guidString);
        }
        public static void ShtoMagazinaSipasSipasLlojit(ASPxGridView grid, int idndermarje, int idperdoruesi, int idllojmagazine, HttpSessionState session, string komponente, string guidString, string fieldName = "IdNjesiAdministrative")
        {
            grid.KonfiguroCombo(fieldName, "IdNjesiAdministrative", "Kodi", () =>
            {
                var dt = DbCore.DbRegjistrim.colNjesiAdministrative.merrSipasNjesiNdermarjePerLupeDegeSipasLlojArtikulli(idndermarje, idperdoruesi, idllojmagazine);
                return dt;
            }, session, komponente, guidString);
        }

        public static void shtoArtikuj(ASPxGridView grid, int idNdermarrje, int idPerdoruesi, bool llojiart, bool kostoSasi, HttpSessionState session, string komponente, string guidString, string fieldName = "IdArtikulli")
        {
            grid.KonfiguroCombo(fieldName, "IdArtikulli", "KodArtikulli", () =>
            {
                var dt = DbCore.DbInventari.colArtikujt.ktheArtikujNdermarrjesAndAutorizimeDheLlojDT(idNdermarrje, idPerdoruesi, llojiart, kostoSasi);

                return dt;
            }, session, komponente, guidString);
        }
        public static void shtoGrupPunonjes(ASPxGridView grid, int idndermarje, HttpSessionState session, string komponente, string guidString, string fieldName = "IdGrupPunonjesish")
        {
            grid.KonfiguroCombo(fieldName, "IdGrupPunonjesish", "Nr", () =>
            {
                var col = new colGrupePunonjesish(idndermarje);

                return col;
            }, session, komponente, guidString);
        }
        public static void shtoEdukimi(ASPxGridView grid, int idndermarje, int idgjuha, HttpSessionState session, string komponente, string guidString, string fieldName = "Edukimi")
        {
            grid.KonfiguroCombo(fieldName, "ID", "PERSHKRIMI", () =>
            {
                var dt = clsPunonjes.merrEdukimeSipasIdNdermarje(idndermarje, idgjuha);

                return dt;
            }, session, komponente, guidString);
        }
        public static void shtoKoloneKategoria(ASPxGridView grid, HttpSessionState session, string komponente, string guidString, string fieldName = "KategoriaNrAutom")
        {
            grid.KonfiguroCombo(fieldName, "Id", "Pershkrimi", () =>
            {
                var colKategorite = new colKategoriNrAuto();
                colKategorite.mbushGjitheKategoriNrAuto();
                return colKategorite;
            }, session, komponente, guidString);
        }
        public static void shtoKoloneDrejtimi(ASPxGridView grid, HttpSessionState session, string komponente, string guidString, string fieldName = "DrejtimiNrAutom")
        {
            grid.KonfiguroCombo(fieldName, "IdDrejtimi", "DrejtimiPershkrimi", () =>
            {

                var liste = new List<DbCore.DbAdmin.clsDrejtim>();
                var drejtimi = new DbCore.DbAdmin.clsDrejtim();
                drejtimi.IdDrejtimi = 0;
                drejtimi.DrejtimiPershkrimi = "Rrites";
                liste.Add(drejtimi);
                drejtimi = new DbCore.DbAdmin.clsDrejtim();
                drejtimi.IdDrejtimi = 1;
                drejtimi.DrejtimiPershkrimi = "Zbrites";
                liste.Add(drejtimi);

                return liste;
            }, session, komponente, guidString);
        }
        public static void shtoKoloneLlojPeriudhe(ASPxGridView grid, HttpSessionState session, string komponente, string guidString, string fieldName = "PeriudhaNrAutom")
        {
            grid.KonfiguroCombo(fieldName, "IdLlojPeriudhe", "LlojPeriudhePershkrimi", () =>
            {
                var colLlojPeriudhe = new DbCore.DbAdmin.colLlojPeriudhe();
                colLlojPeriudhe = colLlojPeriudhe.merrGjithLlojPeridhe();
                return colLlojPeriudhe;
            }, session, komponente, guidString);
        }

        public static void shtoInventarizime(ASPxGridView grid, HttpSessionState session, string komponente, string guidString, string fieldName = "IdInventarizimi")
        {
            grid.KonfiguroCombo(fieldName, "IdInventarizimi", "Pershkrimi", () =>
            {
                var colInventarizim = new DbCore.DbRegjistrim.colInventarizim();
                colInventarizim.mbushGjitheInventarizime();
                return colInventarizim;
            }, session, komponente, guidString);
        }
        public static void shto_LlojNjesiAdministrative(ASPxGridView grid, HttpSessionState session, string komponente, string guidString, string fieldName = "IdLlojMagazine")
        {
            grid.KonfiguroCombo(fieldName, "IdLlojNjesiAdministrative", "Emertimi", () =>
            {
                var col = new DbCore.DbAsete.colLlojNjesiAdministrative();

                col.merrLlojNjesiAdministrative();
                return col;
            }, session, komponente, guidString);
        }
        public static void shtoElementePerIntegrim(ASPxGridView grid, int idNdermarrje, int idLloji, HttpSessionState session, string komponente, string guidString, string fieldName = "IdElementiPerIntegrim")
        {
            grid.KonfiguroCombo(fieldName, "IdElementi", "Emertimi", () =>
            {
                var colElementePerIntegrim = new DbCore.DbInventari.colElementePerIntegrim(idNdermarrje, idLloji);
                return colElementePerIntegrim;
            }, session, komponente, guidString);
        }
        public static void shto_LlojLayeri(ASPxGridView grid, HttpSessionState session, string komponente, string guidString, string fieldName = "LlojLayeri")
        {
            grid.KonfiguroCombo(fieldName, "IDLLOJLAYER", "PERSHKRIMI", () =>
            {
                var dbGis = new DbCore.DbGIS.clsDatabaseGIS();
                var dt = dbGis.ktheGjitheLlojLayerMagazine();
                dbGis.Dispose();
                dt.Dispose();
                return dt;
            }, session, komponente, guidString);
        }
        public static void shto_StatusMagazine(ASPxGridView grid, int idNdermarrje, HttpSessionState session, string komponente, string guidString, string fieldName = "IdStatusMagazine")
        {
            grid.KonfiguroCombo(fieldName, "IdStatusMagazine", "Emertimi", () =>
            {
                var col = new DbCore.DbAsete.colStatusMagazine_Asete();
                col.merrStatusMagazineTePerdorshmeTeNdermarrjes(idNdermarrje);
                return col;
            }, session, komponente, guidString);
        }
        public static void shtoPrindSipasNivelZbritje(ASPxGridView grid, int idNdermarrje, HttpSessionState session, string komponente, string guidString, string fieldName = "IdPrindi")
        {
            grid.KonfiguroCombo(fieldName, "IdNivelZbritje", "PershkrimNivelZbritje", () =>
            {
                var nivelet = new DbCore.DbInventari.colNiveleZbritjesh();

                nivelet.mbushGjitheNiveleZbritjeshPrindiSipasNdermarjes(idNdermarrje);
                return nivelet;
            }, session, komponente, guidString);
        }
        public static void shto_AutorizimSipasNdermarrjes(ASPxGridView grid, int idNdermarrje, HttpSessionState session, string komponente, string guidString, string fieldName = "IdNivelAutorizimi")
        {
            grid.KonfiguroCombo(fieldName, "KodiAutorizim", "KodiAutorizim", () =>
            {
                var colAutorizim = new DbCore.DbAdmin.colAutorizimetKoka(idNdermarrje);
                return colAutorizim;
            }, session, komponente, guidString);
        }
        public static void shtoLlojTakse(ASPxGridView grid, HttpSessionState session, string komponente, string guidString, string fieldName = "IdLlojTakse")
        {
            grid.KonfiguroCombo(fieldName, "IdLlojTakse", "Pershkrim", () =>
            {
                var lloj = new DbCore.DbRegjistrim.colLlojTakse();
                lloj.mbushGjitheLlojeTaksash();
                return lloj;
            }, session, komponente, guidString);
        }

        public static void shto_Llogari(ASPxGridView grid, int idNdermarrje, int idPerdoruesi, HttpSessionState session, string komponente, string guidString, string fieldName)
        {
            grid.KonfiguroCombo(fieldName, "NrLlogari", "NrLlogari", () =>
            {
                var colLlog = new DbCore.DbKontabiliteti.colLlogarite();
                colLlog.mbushLLogariteNdermarrjesAndAutorizime(idNdermarrje, idPerdoruesi);
                return colLlog;
            }, session, komponente, guidString);
        }

        public static void shtoPrindSipasNivelCmimi(ASPxGridView grid, int idNdermarrje, HttpSessionState session, string komponente, string guidString, string fieldName = "IdPrindi")
        {
            grid.KonfiguroCombo(fieldName, "IdNivelCmimi", "PershkrimNivelCmimi", () =>
            {
                var nivelet = new DbCore.DbInventari.colNiveleCmimesh();
                //var niveli = new DbCore.DbInventari.clsNivelCmimi();
                //niveli.IdNivelCmimi = 0;
                //nivelet.Add(niveli);
                nivelet.mbushGjitheNiveleCmimeshSipasNdermarjes(idNdermarrje);
                return nivelet;
            }, session, komponente, guidString);
        }
        public static void ShtoLlogariSipasNdermarrjesDhePerdoruesit(ASPxGridView grid, int idNdermarrje, int idPerdoruesi, HttpSessionState session, string komponente, string guidString, string fieldName)
        {
            grid.KonfiguroCombo(fieldName, "IdLlogari", "NrLlogari", () =>
            {
                var colLlog = new DbCore.DbKontabiliteti.colLlogarite();
                colLlog.mbushLLogariteNdermarrjesAndAutorizime(idNdermarrje, idPerdoruesi);
                return colLlog;
            }, session, komponente, guidString);
        }
        public static void shtoAutorizimSipasKushtePagese(ASPxGridView grid, int idPerdoruesi, HttpSessionState session, string komponente, string guidString, string fieldName)
        {
            grid.KonfiguroCombo(fieldName, "IdAutorizimKoka", "KodiAutorizim", () =>
            {
                var colAutorizimet = new DbCore.DbAdmin.colAutorizimetKoka(idPerdoruesi);
                //var aut = new DbCore.DbAdmin.clsAutorizimKoka();
                //aut.IdAutorizimKoka = 0;
                //colAutorizimet.Add(aut);
                return colAutorizimet;
            }, session, komponente, guidString);
        }

        public static void shtogrupKomponente(ASPxGridView grid, int idNdermarrje, HttpSessionState session, string komponente, string guidString, string fieldName = "IdGrupKomponente")
        {
            grid.KonfiguroCombo(fieldName, "Id", "Kodi", () =>
            {
                var col = new DbCore.DbListPagesat.colGrupKomponente(idNdermarrje);
                return col;
            }, session, komponente, guidString);
        }
        public static void ShtoNivelCmimeshSipasNdermarjesDheLlojit(ASPxGridView grid, int idNdermarrje, int lloji, int IdPerdoruesi, HttpSessionState session, string komponente, string guidString, string fieldName)
        {
            grid.KonfiguroCombo(fieldName, "IdNivelCmimi", "PershkrimNivelCmimi", () =>
            {
                var colNiveleCmimesh = new colNiveleCmimesh();
                colNiveleCmimesh.ShtoObjektBosh();
                colNiveleCmimesh.mbushGjitheNiveleCmimeshSipasNdermarjesDheLlojit(idNdermarrje, lloji, IdPerdoruesi);
                return colNiveleCmimesh;
            }, session, komponente, guidString);
        }
        public static void ShtoTVSH(ASPxGridView grid, int idNdermarrje, int IdPerdoruesi, HttpSessionState session, string komponente, string guidString, string fieldName)
        {
            grid.KonfiguroCombo(fieldName, "IdTaksa", "KodTaksa", () =>
            {
                var colTaksa = new colTaksa { new clsTaksa { IdTaksa = -1, KodTaksa = MessagesResource.Messages["txtPaTVSH"] } };
                colTaksa.AddRange(new colTaksa(idNdermarrje, LlojTakse.Nivel_Tvsh, IdPerdoruesi));
                return colTaksa;
            }, session, komponente, guidString);
        }
        public static void ShtoKategoriNivelDokPerFormatNumrash(ASPxGridView grid, HttpSessionState session, string komponente, string guidString, string fieldName)
        {
            grid.KonfiguroCombo(fieldName, "IdKategori", "Pershkrimi", () =>
            {
                var colKategori = new DbCore.DbRegjistrim.colKategoriNiveleDok();
                colKategori.Add(new DbCore.DbRegjistrim.clsKategoriNivelDok());
                colKategori.mbushKategoriNivelDokPerFormatNumrash();
                return colKategori;
            }, session, komponente, guidString);
        }
        public static void shtoKategoriaNivelDok(ASPxGridView grid, HttpSessionState session, string komponente, string guidString, string fieldName)
        {
            grid.KonfiguroCombo(fieldName, "IdKategori", "Pershkrimi", () =>
            {
                var colKategori = new DbCore.DbRegjistrim.colKategoriNiveleDok();
                colKategori.mbushGjitheKategoriNivelDok();
                return colKategori;
            }, session, komponente, guidString);
        }
        public static void shtoKonvertim(ASPxGridView grid, int idNderm, int idPerdoruesi, HttpSessionState session, string komponente, string guidString, string fieldName)
        {
            grid.KonfiguroCombo(fieldName, "Kodi", "Kodi", () =>
            {
                var colNivele = new DbCore.DbRegjistrim.colNivelRegjistrimi();
                colNivele.mbushGjitheNivelRegjistrimi(idNderm, idPerdoruesi);
                return colNivele;
            }, session, komponente, guidString);
        }

        public static void shtoNiveleZbritjeshSipasNdermarjes(ASPxGridView grid, int idNdermarrje, HttpSessionState session, string komponente, string guidString, string fieldName)
        {
            grid.KonfiguroCombo(fieldName, "IdNivelZbritje", "PershkrimNivelZbritje", () =>
            {
                var colNiveleZbritjesh = new DbCore.DbInventari.colNiveleZbritjesh();
                colNiveleZbritjesh.mbushGjitheNiveleZbritjeshSipasNdermarjes(idNdermarrje);
                return colNiveleZbritjesh;
            }, session, komponente, guidString);
        }
        public static void shtoNivelDok(ASPxGridView grid, HttpSessionState session, string komponente, string guidString, string fieldName)
        {
            grid.KonfiguroCombo(fieldName, "IdKategori", "Pershkrimi", () =>
            {
                var col = new DbCore.DbRegjistrim.colKategoriNiveleDok();
                col.mbushGjitheKategoriNivelDok();
                return col;
            }, session, komponente, guidString);
        }
        public static void shto_NjesiArtikulli(ASPxGridView grid, int idNdermarrje, HttpSessionState session, string komponente, string guidString, string fieldName)
        {
            grid.KonfiguroCombo(fieldName, "IdNjesia", "KodNjesia", () =>
            {
                var col = new DbCore.DbInventari.colNjesiteArtikulli(idNdermarrje);
                return col;
            }, session, komponente, guidString);
        }
        public static void shto_KlasaArtikulli(ASPxGridView grid, HttpSessionState session, string komponente, string guidString, string fieldName)
        {
            grid.KonfiguroCombo(fieldName, "IdKlasa", "PershkrimKlasa", () =>
            {
                var col = new DbCore.DbInventari.colKlasaArtikulli();
                col.mbushGjitheKlasaArtikulli();
                return col;
            }, session, komponente, guidString);
        }
        public static void shto_MetodeKostoje(ASPxGridView grid, HttpSessionState session, string komponente, string guidString, string fieldName)
        {
            grid.KonfiguroCombo(fieldName, "IdMetodeKostoje", "Kodi", () =>
            {
                var col = new DbCore.DbInventari.colMetodeKostoje();
                col.mbushGjitheMetodeKostoje();
                return col;
            }, session, komponente, guidString);
        }
        public static void shto_SkemeKontabilitetiArtikulli(ASPxGridView grid, int idNdermarrje, HttpSessionState session, string komponente, string guidString, string fieldName)
        {
            grid.KonfiguroCombo(fieldName, "IdSkemaKontabilitetiArtikulli", "KodiSkemaKontabilitetiArtikulli", () =>
            {
                var skemat = new DbCore.DbInventari.colSkematKontabilitetiArtikulli(idNdermarrje);
                return skemat;
            }, session, komponente, guidString);
        }
        public static void shto_LlojGarancia(ASPxGridView grid, HttpSessionState session, string komponente, string guidString, string fieldName = "IdLlojGarancia")
        {
            grid.KonfiguroCombo(fieldName, "IdLlojGarancia", "KodGarancia", () =>
            {
                var col = new DbCore.DbInventari.colGarancite();
                return col;
            }, session, komponente, guidString);
        }
        public static void shto_KodifikimArtikulli(ASPxGridView grid, int idNdermarrje, bool llojartikulli, HttpSessionState session, string komponente, string guidString, string fieldName = "Kodifikimi1Artikulli")
        {
            grid.KonfiguroCombo(fieldName, "IdKodifikimi", "KodKodifikimi", () =>
            {
                var kodifikimet = new DbCore.DbInventari.colKodifikimeArtikulli();
                var kod = new DbCore.DbInventari.clsKodifikimArtikulli();
                kodifikimet.Add(kod);
                kodifikimet.mbushGjitheKodifikimetArtikulliSipasNdermarrjesJoPrind(idNdermarrje, llojartikulli);
                return kodifikimet;
            }, session, komponente, guidString);
        }
        public static void shtoLlojBuxhet(ASPxGridView grid, HttpSessionState session, string komponente, string guidString, string fieldName = "Lloji")
        {
            grid.KonfiguroCombo(fieldName, "KODLLOJBUXHETI", "KODLLOJBUXHETI", () =>
            {
                var colBuxhetet = new DbCore.DbKontabiliteti.colLlojeBuxhetesh();
                colBuxhetet.mbushLlojeBuxheteshPerAutorizim();
                return colBuxhetet;
            }, session, komponente, guidString);
        }
        public static void shtoPikeShitjeFurnizim(ASPxGridView grid, int idNdermarrje, HttpSessionState session, string komponente, string guidString, string fieldName = "IdPikeShitjeFurnizimi")
        {
            grid.KonfiguroCombo(fieldName, "IdPikeShitjeFurnizimi", "Kodi", () =>
            {
                var colPikat = new colPikaShitjeFurnizimi();
                colPikat.mbushGjithePikaShitje(idNdermarrje);
                return colPikat;
            }, session, komponente, guidString);
        }
        public static void ShtoTransportuesSipasNdermarrrjes(ASPxGridView grid, int idNdermarrje, HttpSessionState session, string komponente, string guidString, string fieldName = "IdTransportues")
        {
            grid.KonfiguroCombo(fieldName, "IdTransportues", "Emertimi", () =>
            {
                var col = new DbCore.DbInventari.colTransportues();
                col.mbushTransportuesitSipasNdermarrjes(idNdermarrje);
                return col;
            }, session, komponente, guidString);
        }

        public static void shtoLlojKase(ASPxGridView grid, HttpSessionState session, string komponente, string guidString, string fieldName = "Vlera")
        {
            grid.KonfiguroCombo(fieldName, "Vlera", "Lloji", () =>
            {
                var col = colVleratKonfigurimiKasa.ktheLlojeKasashDhePeshoreshAll();
                return col;
            }, session, komponente, guidString);
        }
        public static void shtoLlojObjekti(ASPxGridView grid, ResourceManager rm, CultureInfo ci)
        {
            grid.KonfiguroComboMeItems("Lloji", () => new ListEditItemCollection
            {
                {"Kase", 1},
                {"Peshore", 2}
            });
        }


        public static void shtoKlient(ASPxGridView grid, int idNdermarrje, int IdPerdoruesi, HttpSessionState session, string komponente, string guidString, string fieldName)
        {
            grid.KonfiguroCombo(fieldName, "IdKlientFurnitor", "KodKlientFurnitor", () =>
            {
                var klientet = new DbCore.DbKontabiliteti.colKlienteFurnitore();
                klientet.Add(new DbCore.DbKontabiliteti.clsKlientFurnitor());
                klientet.mbushKlienteFurnitoreNdermarrjes(idNdermarrje);
                return klientet;
            }, session, komponente, guidString);
        }
        public static void shtoKlientLinear(ASPxGridView grid, int idNdermarrje, int IdPerdoruesi, HttpSessionState session, string komponente, string guidString, string fieldName)
        {
            grid.KonfiguroCombo(fieldName, "IdKlientFurnitor", "KodKlientFurnitor", () =>
            {
                var klientet = new DbCore.DbKontabiliteti.colKlienteFurnitore();
                klientet.Add(new DbCore.DbKontabiliteti.clsKlientFurnitor());
                klientet.mbushKlienteFurnitoreNdermarrjesLinear(idNdermarrje);
                return klientet;
            }, session, komponente, guidString);
        }
        public static void shtoFormatSeriali(ASPxGridView grid, int idNdermarrje, HttpSessionState session, string komponente, string guidString, string fieldName)
        {//idKategoriSeriali
            grid.KonfiguroCombo(fieldName, "Id", "Kod", () =>
            {
                return new DbCore.DbInventari.colSerialeUnikeFormate(idNdermarrje);
            }, session, komponente, guidString);
        }

        public static void shtoNdryshimPozicioni(ASPxGridView grid, int idNdermarrje, int idgjuha, HttpSessionState session, string komponente, string guidString, string fieldName = "NdryshimPozicioni")
        {
            grid.KonfiguroCombo(fieldName, "Id", "Pershkrimi", () =>
            {
                var dt = clsPunonjes.merrNdryshimPozicioniDT(idgjuha);
                return dt;
            }, session, komponente, guidString);
        }
        public static void shtoQK(ASPxGridView grid, int idNdermarrje, HttpSessionState session, string komponente, string guidString, string fieldName = "IdQenderKosto1")
        {
            grid.KonfiguroCombo(fieldName, "Id", "Kodi", () =>
            {
                var col = new DbCore.DbQendraKosto.colQendraKosto(idNdermarrje);
                //  col.Insert(0, new DbCore.DbQendraKosto.clsQendraKosto());
                return col;
            }, session, komponente, guidString);
        }
        public static void shtoGlobalLocal(ASPxGridView grid, int idNdermarrje, HttpSessionState session, string komponente, string guidString, string fieldName)
        {
            grid.KonfiguroCombo(fieldName, "Id", "Kodi", () =>
            {
                var col = new colGrupimeLocaleGlobale(idNdermarrje, 1);
                // col.Insert(0, new clsGrupimeLocaleGlobale());
                return col;
            }, session, komponente, guidString);
        }


        public static void Shto_Prind(ASPxTreeList grid, int idNdermarrje, int Lloji, HttpSessionState session, string komponente, string guidString, string fieldName)
        {
            grid.KonfiguroCombo_TreeListe(fieldName, "Id", "Kodi", () =>
            {
                var col = new DbCore.DbListPagesat.colGrupimeLocaleGlobale(idNdermarrje, Lloji);
                return col;
            }, session, komponente, guidString);
        }


        public static void ShtoPrindKategoriBuxhetimi(ASPxGridView grid, int idndermarje, HttpSessionState session, string komponente, string guidString, string fieldName)
        {
            grid.KonfiguroCombo(fieldName, "IdKategoriBuxhetimi", "Kodi", () =>
            {
                var colKategoriBuxhetimi = new ColBKategoriBuxhetimi(idndermarje);
                colKategoriBuxhetimi.Insert(0, new ClsBKategoriBuxhetimi(MessagesResource.Messages));
                return colKategoriBuxhetimi;
            }, session, komponente, guidString);
        }

        public static void ShtoKategoriSerileshUnike(ASPxGridView grid, int idndermarje, HttpSessionState session, string komponente, string guidString, string fieldName)
        {
            grid.KonfiguroCombo(fieldName, "ID", "Kategori", () =>
            {
                var colSerialeUnikeKategori = new colSerialeUnikeKategori(idndermarje);
                //colSerialeUnikeKategori.Insert(0, new clsSerialeUnikeKategori());
                return colSerialeUnikeKategori;
            }, session, komponente, guidString);
        }
    }
}