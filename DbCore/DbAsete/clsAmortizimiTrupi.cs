using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.DbAsete
{
    public class clsAmortizimiTrupi : clsAmortizimiTrupiAbstract
    {
        public override enumObjekteAmortizimi objektiKod => enumObjekteAmortizimi.ASETE;

        #region Konstruktori

        /// <summary>
        /// MODULI ASETE:
        /// Krijon objektin bosh te klases clsAmortizimiTrupi per trupin e dokumentit te amortizimit .
        /// </summary>
        public clsAmortizimiTrupi() : base()
        {
        }

        public clsAmortizimiTrupi(DataRow dbDataRowAmortizimTrupi) : base(dbDataRowAmortizimTrupi)
        {

        }

        /// <summary>
        /// MODULI ASETE:
        /// Sherben per te krijuar clsAmortizimiTrupi nga grida client
        /// </summary>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes.</param>
        /// <param name="idPerdorues">(int) Id e perdoruesit.</param>
        /// <param name="kontrolloserial">(bool) True nes duhet te kontrolloj serialin dhe ne te kundert False.</param>
        /// <param name="rreshtDokuKlient">(Dictionary) Rreshti i dokumentit te klientit.</param>
        public clsAmortizimiTrupi(int idNdermarrje, int idPerdorues, bool kontrolloserial, Dictionary<string, object> rreshtDokuKlient, string lloj, List<double> amortizimfillestar) : base(idNdermarrje, idPerdorues, kontrolloserial, rreshtDokuKlient, lloj, amortizimfillestar)
        {
            
        }

        /// <summary>
        /// MODULI ASETE:
        /// Krijon objektin me parametra te klases clsAmortizimiTrupi per trupin e dokumentit te amortizimit .
        /// </summary>
        /// <param name="nrRreshti">(int) Numri i rreshtit te trupit.</param>
        /// <param name="idArtikulli">(int) Id e artikullit per te cilen po rregjistrohet rreshti.</param>
        /// <param name="emertim">(string) Merr emertimin e artikullit.</param>
        /// <param name="llojAmortizimi">(int) Id e llojit te amortizimit.</param>
        /// <param name="dataMePareAmortizimi">(DateTime) Data e amortizimit me pare.</param>
        /// <param name="dataAmortizimi">(DateTime) Data e amortizimit.</param>
        /// <param name="dateNdryshimiMagazine">(DateTime) Data e ndryshimit te magazines.</param>
        /// <param name="idNjesiAdministrative">(int) Id e njesi administrative ku eshte seriali.</param>
        /// <param name="idAQTSerial">(int) Id e serialit te aqt-se.</param>
        public clsAmortizimiTrupi(int nrRreshti, int idArtikulli, string emertim, int llojAmortizimi, DateTime dataMePareAmortizimi, DateTime dataAmortizimi, DateTime dateNdryshimiMagazine, int idNjesiAdministrative, int idAQTSerial, string serial, DbInventari.clsArtikulli elem, double vleftaShteseRivleresim) : 
            base(nrRreshti, idArtikulli, emertim, llojAmortizimi, dataMePareAmortizimi, dataAmortizimi, dateNdryshimiMagazine, idNjesiAdministrative, idAQTSerial, serial, elem, vleftaShteseRivleresim)
        {

        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// MODULI ASETE:
        /// Metoda perdoret per te mbushur te gjitha atributet e objektit clsAmortizimiTrupi sipas te dhenave qe vijne nga cdo rresht i databazes nga tabela T_ASETE_AMORTIZIMI_TRUPI.
        /// </summary>
        /// <param name="dbDataRowAmortizimTrupi">(DataRow) Merr si parameter vetem nje rresht te kthyer nga tabelat ne databaze per te mbushur nje objekt.</param>
        /// <returns>(bool) Kthen true nese nuk ndodh asnje gabim gjate leximit te te dhenave, ne te kundert false.</returns>
        internal override bool mbushAmortizimTrupiObjekt(DataRow dbDataRowAmortizimTrupi)
        {
            if (dbDataRowAmortizimTrupi == null)
                return false;
            try
            {
                int idAmortizimiTrupi;
                int idAmortizimKoka;
                int nrRendor;
                int idArtikulli;
                int idArtikull_LlojAmortizimi;
                DateTime dateAmortizimi;
                DateTime dateMePareAmortizimi;
                DateTime dateNdryshimStatusMagazine;
                int idNjesiAdministrative;
                double normaAmortizimi;
                double amortizimiShtese;
                double amortizimiGjithsej;
                double amortizimiVjetor;
                double vleftaPlusMinus;
                double hdAmortizimGjithsej;
                double hdAmortizimVjetor;
                double vleftaGjendje;
                double diteAmortizimi;
                int idAQTSeriali;
                double vleftaShteseRivleresim;

                int.TryParse(dbDataRowAmortizimTrupi["ID_AMORTIZIMI_TRUPI"].ToString(), out idAmortizimiTrupi);
                int.TryParse(dbDataRowAmortizimTrupi["IDAMORTIZIMIKOKA"].ToString(), out idAmortizimKoka);
                int.TryParse(dbDataRowAmortizimTrupi["NRRENDOR"].ToString(), out nrRendor);
                int.TryParse(dbDataRowAmortizimTrupi["IDARTIKULLI"].ToString(), out idArtikulli);
                int.TryParse(dbDataRowAmortizimTrupi["IDARTIKULL_LLOJAMORT"].ToString(), out idArtikull_LlojAmortizimi);
                DateTime.TryParse(dbDataRowAmortizimTrupi["DATE_AMORTIZIMI"].ToString(), out dateAmortizimi);
                DateTime.TryParse(dbDataRowAmortizimTrupi["DATE_MEPARE_AMORTIZIMI"].ToString(), out dateMePareAmortizimi);
                DateTime.TryParse(dbDataRowAmortizimTrupi["DATE_NDRYSHIM_STATUSMAGAZINE"].ToString(), out dateNdryshimStatusMagazine);
                int.TryParse(dbDataRowAmortizimTrupi["IDNJESIADMINISTRATIVE"].ToString(), out idNjesiAdministrative);
                double.TryParse(dbDataRowAmortizimTrupi["NORMAAMORTIZIMI"].ToString(), out normaAmortizimi);
                double.TryParse(dbDataRowAmortizimTrupi["AMORTIZIMISHTESE"].ToString(), out amortizimiShtese);
                double.TryParse(dbDataRowAmortizimTrupi["AMORTIZIMIGJITHSEJ"].ToString(), out amortizimiGjithsej);
                double.TryParse(dbDataRowAmortizimTrupi["AMORTIZIMIVJETOR"].ToString(), out amortizimiVjetor);
                double.TryParse(dbDataRowAmortizimTrupi["VLEFTAPLUSMINUS"].ToString(), out vleftaPlusMinus);
                double.TryParse(dbDataRowAmortizimTrupi["HD_AMORTGJITHSEJ"].ToString(), out hdAmortizimGjithsej);
                double.TryParse(dbDataRowAmortizimTrupi["HD_AMORTVJETOR"].ToString(), out hdAmortizimVjetor);
                double.TryParse(dbDataRowAmortizimTrupi["VLEFTAGJENDJE"].ToString(), out vleftaGjendje);
                double.TryParse(dbDataRowAmortizimTrupi["DITEAMORTIZIMI"].ToString(), out diteAmortizimi);
                double.TryParse(dbDataRowAmortizimTrupi["VLEFTASHTESERIVLERESIM"].ToString(), out vleftaShteseRivleresim);
                int.TryParse(dbDataRowAmortizimTrupi["IDSERIALI"].ToString(), out idAQTSeriali);
                Emertimi = dbDataRowAmortizimTrupi["Emertimi"].ToString();
                Serial = dbDataRowAmortizimTrupi["Serial"].ToString();

                IdAmortizimiTrupi = idAmortizimiTrupi;
                IdAmortizimKoka = idAmortizimKoka;
                NrRendor = nrRendor;
                IdArtikulli = idArtikulli;
                IdArtikull_LlojAmortizimi = idArtikull_LlojAmortizimi;
                DateAmortizimi = dateAmortizimi;
                DateMePareAmortizimi = dateMePareAmortizimi;
                DateNdryshimStatusMagazine = dateNdryshimStatusMagazine;
                IdNjesiAdministrative = idNjesiAdministrative;
                NormaAmortizimi = normaAmortizimi;
                AmortizimiShtese = amortizimiShtese;
                AmortizimiGjithsej = amortizimiGjithsej;
                AmortizimiVjetor = amortizimiVjetor;
                VleftaPlusMinus = vleftaPlusMinus;
                HdAmortizimGjithsej = hdAmortizimGjithsej;
                HdAmortizimVjetor = hdAmortizimVjetor;
                VleftaGjendje = vleftaGjendje;
                DiteAmortizimi = diteAmortizimi;
                IdAQTSeriali = idAQTSeriali;
                VleftaShteseRivleresim = vleftaShteseRivleresim;

                return true;
            }
            catch (InvalidCastException)
            {
                throw new Exception("ERROR: Gabim gjate marrjes se trupit te dokumentit te amortizimit nga db-ja");
            }
        }

        /// <summary>
        /// MODULI ASETE:
        /// Metoda perdoret per te mbushur te gjitha atributet e objektit clsAmortizimiTrupi sipas te dhenave qe vijne nga cdo rresht i databazes nga tabela T_ASETE_AMORTIZIMI_TRUPI per rillogaritjet e amortizimeve ne artikujt pa serial.
        /// </summary>
        /// <param name="dbDataRowAmortizimTrupi">(DataRow) Merr si parameter vetem nje rresht te kthyer nga tabelat ne databaze per te mbushur nje objekt.</param>
        /// <returns>(bool) Kthen true nese nuk ndodh asnje gabim gjate leximit te te dhenave, ne te kundert false.</returns>
        internal override bool mbushAmortizimTrupiObjektPerRillogaritjeArtikujPaSerial(DataRow dbDataRowAmortizimTrupi)
        {
            if (dbDataRowAmortizimTrupi == null)
                return false;
            try
            {
                int idAmortizimiTrupi;
                int idAmortizimKoka;
                int nrRendor;
                int idArtikulli;
                int idArtikull_LlojAmortizimi;
                DateTime dateAmortizimi;
                DateTime dateMePareAmortizimi;
                DateTime dateNdryshimStatusMagazine;
                int idNjesiAdministrative;
                double normaAmortizimi;
                double amortizimiShtese;
                double amortizimiGjithsej;
                double amortizimiVjetor;
                double vleftaPlusMinus;
                double hdAmortizimGjithsej;
                double hdAmortizimVjetor;
                double vleftaGjendje;
                double diteAmortizimi;
                int idAQTSeriali;
                double vleftaShteseRivleresim;
                int idPrind;
                int idPrindFillestar;

                int.TryParse(dbDataRowAmortizimTrupi["ID_AMORTIZIMI_TRUPI"].ToString(), out idAmortizimiTrupi);
                int.TryParse(dbDataRowAmortizimTrupi["IDAMORTIZIMIKOKA"].ToString(), out idAmortizimKoka);
                int.TryParse(dbDataRowAmortizimTrupi["NRRENDOR"].ToString(), out nrRendor);
                int.TryParse(dbDataRowAmortizimTrupi["IDARTIKULLI"].ToString(), out idArtikulli);
                int.TryParse(dbDataRowAmortizimTrupi["IDARTIKULL_LLOJAMORT"].ToString(), out idArtikull_LlojAmortizimi);
                DateTime.TryParse(dbDataRowAmortizimTrupi["DATE_AMORTIZIMI"].ToString(), out dateAmortizimi);
                DateTime.TryParse(dbDataRowAmortizimTrupi["DATE_MEPARE_AMORTIZIMI"].ToString(), out dateMePareAmortizimi);
                DateTime.TryParse(dbDataRowAmortizimTrupi["DATE_NDRYSHIM_STATUSMAGAZINE"].ToString(), out dateNdryshimStatusMagazine);
                int.TryParse(dbDataRowAmortizimTrupi["IDNJESIADMINISTRATIVE"].ToString(), out idNjesiAdministrative);
                double.TryParse(dbDataRowAmortizimTrupi["NORMAAMORTIZIMI"].ToString(), out normaAmortizimi);
                double.TryParse(dbDataRowAmortizimTrupi["AMORTIZIMISHTESE"].ToString(), out amortizimiShtese);
                double.TryParse(dbDataRowAmortizimTrupi["AMORTIZIMIGJITHSEJ"].ToString(), out amortizimiGjithsej);
                double.TryParse(dbDataRowAmortizimTrupi["AMORTIZIMIVJETOR"].ToString(), out amortizimiVjetor);
                double.TryParse(dbDataRowAmortizimTrupi["VLEFTAPLUSMINUS"].ToString(), out vleftaPlusMinus);
                double.TryParse(dbDataRowAmortizimTrupi["HD_AMORTGJITHSEJ"].ToString(), out hdAmortizimGjithsej);
                double.TryParse(dbDataRowAmortizimTrupi["HD_AMORTVJETOR"].ToString(), out hdAmortizimVjetor);
                double.TryParse(dbDataRowAmortizimTrupi["VLEFTAGJENDJE"].ToString(), out vleftaGjendje);
                double.TryParse(dbDataRowAmortizimTrupi["DITEAMORTIZIMI"].ToString(), out diteAmortizimi);
                double.TryParse(dbDataRowAmortizimTrupi["VLEFTASHTESERIVLERESIM"].ToString(), out vleftaShteseRivleresim);
                int.TryParse(dbDataRowAmortizimTrupi["IDSERIALI"].ToString(), out idAQTSeriali);
                Emertimi = dbDataRowAmortizimTrupi["Emertimi"].ToString();
                Serial = dbDataRowAmortizimTrupi["Serial"].ToString();
                int.TryParse(dbDataRowAmortizimTrupi["IDPRINDI"].ToString(), out idPrind);
                int.TryParse(dbDataRowAmortizimTrupi["IDPRINDIFILLESTAR"].ToString(), out idPrindFillestar);

                IdAmortizimiTrupi = idAmortizimiTrupi;
                IdAmortizimKoka = idAmortizimKoka;
                NrRendor = nrRendor;
                IdArtikulli = idArtikulli;
                IdArtikull_LlojAmortizimi = idArtikull_LlojAmortizimi;
                DateAmortizimi = dateAmortizimi;
                DateMePareAmortizimi = dateMePareAmortizimi;
                DateNdryshimStatusMagazine = dateNdryshimStatusMagazine;
                IdNjesiAdministrative = idNjesiAdministrative;
                NormaAmortizimi = normaAmortizimi;
                AmortizimiShtese = amortizimiShtese;
                AmortizimiGjithsej = amortizimiGjithsej;
                AmortizimiVjetor = amortizimiVjetor;
                VleftaPlusMinus = vleftaPlusMinus;
                HdAmortizimGjithsej = hdAmortizimGjithsej;
                HdAmortizimVjetor = hdAmortizimVjetor;
                VleftaGjendje = vleftaGjendje;
                DiteAmortizimi = diteAmortizimi;
                IdAQTSeriali = idAQTSeriali;
                VleftaShteseRivleresim = vleftaShteseRivleresim;
                IdPrind = idPrind;
                IdPrindFillestar = idPrindFillestar;

                return true;
            }
            catch (InvalidCastException)
            {
                throw new Exception("ERROR: Gabim gjate marrjes se trupit te dokumentit te amortizimit nga db-ja");
            }
        }

        #endregion

    }
}
