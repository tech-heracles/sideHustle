using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.DbAsete
{
    public class clsAseteNormaAmortizimi : clsAseteNormaAmortizimiAbstract
    {
        public override enumObjekteAmortizimi objektiKod => enumObjekteAmortizimi.ASETE;
        #region Konstruktore

        /// <summary>
        /// Konstruktore bosh
        /// </summary>
        public clsAseteNormaAmortizimi() : base()
        {

        }

        public clsAseteNormaAmortizimi(DataRow rreshti) : base(rreshti) 
        {

        }


        public clsAseteNormaAmortizimi(int idartikulli, int idllojamortizimi, int idstandarte, bool normeMagazine, double norme, string kodartikulli, string llojamortizimi, string standart, int idndermarje, bool shtim, bool vjenNgaImporti, DateTime dtaktvizimi) : base(idartikulli, idllojamortizimi, idstandarte, normeMagazine, norme, kodartikulli, llojamortizimi, standart, idndermarje, shtim, vjenNgaImporti, dtaktvizimi)
        {

        }

        /// <summary>
        /// MODULI ASETE:
        /// Krijon objektin me parametra te klases clsAseteNormaAmortizimi per normat e amortizimit te artikujve.
        /// </summary>
        /// <param name="idndermarje">(int) Id e ndermarrjes.</param>
        /// <param name="rresht">(Dictionary) Rreshti i cili do te mbushe objektin.</param>
        public clsAseteNormaAmortizimi(int idndermarje, Dictionary<string, object> rresht, DateTime dtaktivizimi) : base(idndermarje, rresht, dtaktivizimi)
        {
        }

        #endregion

        //public clsAseteNormaAmortizimi krijoPerImport(string kodartikulli, string llojamortizimi, string standart, string normemagazine, double norme, int indermarje, bool shtim, DateTime dtkativizimi)
        //{
        //    try
        //    {
        //        IdArtikulli = DbInventari.clsArtikulli.ktheIdArtikulli(kodartikulli, indermarje);
        //        if (normemagazine == "Artikull")
        //            NormeMagazine = false;
        //        else if (normemagazine == "Magazine")
        //            NormeMagazine = true;
        //        else throw new MyException("Kjo lloj norme nuk ekziston!");
        //        IdStandartAmortizimi = clsStandarteAmortizim.merrIDStandartinAmortizimitTeNdermarrjesSipasEmertimit(standart, indermarje);
        //        IdLlojAmortizimi = clsAseteLlojAmortizimi.ktheIdLlojAmortizimiSipasEmertimit(llojamortizimi);
        //        return new clsAseteNormaAmortizimi(IdArtikulli, IdLlojAmortizimi, IdStandartAmortizimi, NormeMagazine, norme, kodartikulli, llojamortizimi, standart, indermarje, shtim, true, dtkativizimi);
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}

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
                int.TryParse(dbDataRowNormaAmortizimi["ID_ARTIKULL_LLOJAMORTIZIM"].ToString(), out idLidhjeArtikullLlojAmort);
                int.TryParse(dbDataRowNormaAmortizimi["IDARTIKULLI"].ToString(), out idArtikulli);
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
