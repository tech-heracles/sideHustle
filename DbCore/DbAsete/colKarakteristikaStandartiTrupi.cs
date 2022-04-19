using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DbCore.DbAsete
{
    /// <summary>
    /// MODULI ASETE:
    /// Mban nje list objektesh clsKarakteristikaStandartiTrupi (objekte per trupin karakteristikave te ndryshme te standarteve) ne modulin e Aseteve.
    /// Te dhenat merret nga tabela T_ASETE_LIDHJE_STANDART_STATUSMAG_TRUPI
    /// </summary>
    public class colKarakteristikaStandartiTrupi : List<clsKarakteristikaStandartiTrupi>
    {
        #region Konstruktore

        /// <summary>
        /// MODULI ASETE:
        /// Krijon nje list objektesh bosh te klases clsKarakteristikaStandartiTrupi per trupin e karakteristikave te standarteve.
        /// </summary>
        public colKarakteristikaStandartiTrupi()
        {
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// MODULI ASETE:
        /// Merr te gjithe trupin e karakteristikave se standarteve te amortizimit.
        /// </summary>
        /// <param name="idKokaKarakteristikStandart">(int) Id e kokes se karakteristikave te standartit.</param>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate marrjes se trupit te karakteristikave te standarteve ose False ne te kundert.</returns>
        public bool merrTrupinSipasKokes(int idKokaKarakteristikStandart)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            bool pergjigja = mbushKarakteristikaTrupiList(moduliAsete.ktheKonfigurimStandartiTrupiSipasIDKoka(idKokaKarakteristikStandart));
            moduliAsete.Dispose();
            return pergjigja;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// MODULI ASETE:
        /// Metode qe sherben per te marre te dhenat nga store procedura te lidhura me tabelen T_ASETE_LIDHJE_STANDART_STATUSMAG_TRUPI ne nje list objektesh clsKarakteristikaStandartiTrupi.
        /// </summary>
        /// <param name="dt">(DataTable) Merr si parameter nje datatable qe kthehet nga store procedura per te mbushur nje list me objekte.</param>
        /// <returns>(bool) Kthen true nese nuk ndodh asnje gabim gjate leximit te te dhenave, ne te kundert false.</returns>
        private bool mbushKarakteristikaTrupiList(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsKarakteristikaStandartiTrupi karakteristikaTrupi = new clsKarakteristikaStandartiTrupi();
                    //karakteristikaTrupi.mbushKarakteristikaTrupiObjekt(rreshti);
                    Add(new clsKarakteristikaStandartiTrupi(rreshti));
                }
            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }

        #endregion
    }
}
