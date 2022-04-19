using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DbCore.DbAsete
{
    /// <summary>
    /// MODULI ASETE:
    /// Mban nje list objektesh clsHistorikAQTSeriale (objekte per ruajtjen e historikut te serialeve grup te aqt-ve) ne modulin e Aseteve.
    /// Te dhenat merret nga tabela T_ASETE_SERIALE_HISTORIKU.
    /// </summary>
    public class colHistorikAQTSeriale : List<clsHistorikAQTSeriale>
    {
        #region Konstruktore

        /// <summary>
        /// MODULI ASETE:
        /// Krijon nje list objektesh bosh te klases clsHistorikAQTSeriale per historikut te serialeve grup te aqt-ve.
        /// </summary>
        public colHistorikAQTSeriale()
        {
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// MODULI ASETE:
        /// Merr te gjithe historikun e serialeve te aqt-ve sipas id se prindit qe kerkojme dhe mbush nje koleksion me keto objekte.
        /// </summary>
        /// <param name="idPrind">(int) Id e prindit nje shkalle me siper nga eshte krijuar seriali.</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes ne perdorim per te cilen do te merret historiku i aqt-se.</param>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate marrjes se historikut te serialeve te aqt-ve ose False ne te kundert.</returns>
        public bool merrHistorikAQTSerialSipasIDPrind(int idPrind, int idNdermarrje)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            bool pergjigja = mbushHistorikAQTSerialeList(moduliAsete.ktheHistorikAQTSerialSipasIDPrind(idPrind, idNdermarrje));
            moduliAsete.Dispose();
            return pergjigja;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr te gjithe historikun e serialeve te aqt-ve sipas id se prindit fillestar dhe mbush nje koleksion me keto objekte.
        /// </summary>
        /// <param name="idPrindFillestar">(int) Id e prindit fillestar nga ka rrjedhur seriali.</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes ne perdorim per te cilen do te merret historiku i aqt-se.</param>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate  marrjes se historikut te serialeve te aqt-ve ose False ne te kundert.</returns>
        public bool merrHistorikAQTSerialSipasIDPrindFillestar(int idPrindFillestar, int idNdermarrje)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            bool pergjigja = mbushHistorikAQTSerialeList(moduliAsete.ktheHistorikAQTSerialSipasIDPrindFillestar(idPrindFillestar, idNdermarrje));
            moduliAsete.Dispose();
            return pergjigja;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr te gjithe historikun e serialeve te aqt-ve sipas dokumentit te magazines dhe mbush nje koleksion me keto objekte.
        /// </summary>
        /// <param name="idDok">(int) Id e dokumentit qe ka shkaktuar krijimin e serialit.</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes ne perdorim per te cilen do te merret historiku i aqt-se.</param>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate  marrjes se historikut te serialeve te aqt-ve ose False ne te kundert.</returns>
        public bool merrHistorikAQTSerialSipasIDDok(int idDok, int idNdermarrje)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            bool pergjigja = mbushHistorikAQTSerialeList(moduliAsete.ktheHistorikAQTSerialSipasIDDok(idDok, idNdermarrje));
            moduliAsete.Dispose();
            return pergjigja;
        }
        public bool ktheHistorikAQTSerialSipasIDDokTeFshira(int idDok, int idNdermarrje, clsDatabazeAsete moduliAsete)
        {

            bool pergjigja = mbushHistorikAQTSerialeList(moduliAsete.ktheHistorikAQTSerialSipasIDDokTeFshira(idDok, idNdermarrje));
        
            return pergjigja;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// MODULI ASETE:
        /// Metode qe sherben per te marre te dhenat nga store procedura te lidhura me tabelen T_ASETE_SERIALE_HISTORIKU ne nje list objektesh clsHistorikAQTSeriale.
        /// </summary>
        /// <param name="dt">(DataTable) Merr si parameter nje datatable qe kthehet nga store procedura per te mbushur nje list me objekte.</param>
        /// <returns>(bool) Kthen true nese nuk ndodh asnje gabim gjate leximit te te dhenave, ne te kundert false.</returns>
        private bool mbushHistorikAQTSerialeList(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsHistorikAQTSeriale historikAQTSeriale = new clsHistorikAQTSeriale();
                    //historikAQTSeriale.mbushHistorikAQTSerialObjekt(rreshti);
                    Add(new clsHistorikAQTSeriale(rreshti));
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
