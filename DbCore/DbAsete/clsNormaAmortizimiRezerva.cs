using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.DbAsete
{
    public class clsNormaAmortizimiRezerva : clsAseteNormaAmortizimiAbstract
    {
        public override enumObjekteAmortizimi objektiKod => enumObjekteAmortizimi.REZERVA;

        #region Konstruktore


        /// <summary>
        /// Konstruktore bosh
        /// </summary>
        public clsNormaAmortizimiRezerva() : base()
        {

        }

        public clsNormaAmortizimiRezerva(DataRow rreshti) : base(rreshti)
        {

        }


        public clsNormaAmortizimiRezerva(int idartikulli, int idllojamortizimi, int idstandarte, bool normeMagazine, double norme, string kodartikulli, string llojamortizimi, string standart, int idndermarje, bool shtim, bool vjenNgaImporti, DateTime dtaktvizimi) : base(idartikulli, idllojamortizimi, idstandarte, normeMagazine, norme, kodartikulli, llojamortizimi, standart, idndermarje, shtim, vjenNgaImporti, dtaktvizimi)
        {

        }

        /// <summary>
        /// MODULI ASETE:
        /// Krijon objektin me parametra te klases clsAseteNormaAmortizimi per normat e amortizimit te artikujve.
        /// </summary>
        /// <param name="idndermarje">(int) Id e ndermarrjes.</param>
        /// <param name="rresht">(Dictionary) Rreshti i cili do te mbushe objektin.</param>
        public clsNormaAmortizimiRezerva(int idndermarje, Dictionary<string, object> rresht, DateTime dtaktivizimi) : base(idndermarje, rresht, dtaktivizimi)
        {
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// MODULI ASETE:
        /// Metoda perdoret per te mbushur te gjitha atributet e objektit clsAseteNormaAmortizimiAbstract sipas te dhenave qe vijne nga cdo rresht i databazes nga tabela T_ASETE_LIDHJE_ARTIKULL_LLOJAMORT.
        /// </summary>
        /// <param name="dbDataRowNormaAmortizimi">(DataRow) Merr si parameter vetem nje rresht te kthyer nga tabelat ne databaze per te mbushur nje objekt.</param>
        /// <returns>(bool) Kthen true nese nuk ndodh asnje gabim gjate leximit te te dhenave, ne te kundert false.</returns>
        internal override bool mbushArtikullNormaAmortizimiObjekt(DataRow dbDataRowNormaAmortizimi)
        {
            if (dbDataRowNormaAmortizimi == null)
                return false;
            try
            {
                int idLidhjeArtikullLlojAmort = 0;
                int idArtikulli = 0;
                int idLlojAmortizimi = 0;
                int idStandartAmortizimi = 0;
                bool normeMagazine = false;
                double norme = 0;
                DateTime dtAktivizimi;
                int.TryParse(dbDataRowNormaAmortizimi["ID_REZERVA_LLOJAMORTIZIMI"].ToString(), out idLidhjeArtikullLlojAmort);
                int.TryParse(dbDataRowNormaAmortizimi["ID_ARTIKULLI"].ToString(), out idArtikulli);
                int.TryParse(dbDataRowNormaAmortizimi["IDLLOJAMORTIZIMI"].ToString(), out idLlojAmortizimi);
                int.TryParse(dbDataRowNormaAmortizimi["IDLLOJSTANDARTI"].ToString(), out idStandartAmortizimi);
                bool.TryParse(dbDataRowNormaAmortizimi["NORMEMAGAZINE"].ToString(), out normeMagazine);
                double.TryParse(dbDataRowNormaAmortizimi["NORMA"].ToString(), out norme);
                DateTime.TryParse(dbDataRowNormaAmortizimi["DTAKTIVIZIMI"].ToString(), out dtAktivizimi);
                IdLidhjeArtikullLlojAmort = idLidhjeArtikullLlojAmort;
                IdArtikulli = idArtikulli;
                IdLlojAmortizimi = idLlojAmortizimi;
                IdStandartAmortizimi = idStandartAmortizimi;
                NormeMagazine = normeMagazine;
                Norme = norme;
                DtAktivizimi = dtAktivizimi;

                return true;
            }
            catch (InvalidCastException)
            {
                throw new Exception("ERROR: Gabim gjate marrjes se normave te amortizimit te artikujve nga db-ja");
            }
        }

        #endregion

    }

}
