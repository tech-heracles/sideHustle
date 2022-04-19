using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.DbAsete
{
    public class colAseteNormaAmortizimi : colAseteNormaAmortizimiAbstract
    {
        public override enumObjekteAmortizimi objektiKod => enumObjekteAmortizimi.ASETE;

        #region Konstruktore

        /// <summary>
        /// Konstruktore bosh
        /// </summary>
        public colAseteNormaAmortizimi() : base()
        {

        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// MODULI ASETE:
        /// Merr normen e artikullit sipas id se kodifikimit.
        /// </summary>
        /// <param name="idkodifikimi">(int) Id e kodifikimit (grupit).</param>
        /// <param name="idndermarje">(int) Id e ndermarrjes.</param>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate marrjes se normave te artikullit ose False ne te kundert.</returns>
 

  

        /// <summary>
        /// MODULI ASETE:
        /// Metode qe sherben per te marre te dhenat nga store procedura te lidhura me tabelen T_ASETE_LIDHJE_ARTIKULL_LLOJAMORT ne nje list objektesh clsAseteNormaAmortizimi.
        /// </summary>
        /// <param name="dt">(DataTable) Merr si parameter nje datatable qe kthehet nga store procedura per te mbushur nje list me objekte.</param>
        /// <returns>(bool) Kthen true nese nuk ndodh asnje gabim gjate leximit te te dhenave, ne te kundert false.</returns>
        public override bool mbushNormaAmortizimiList(DataTable dt)
        {
            foreach (DataRow rreshti in dt.Rows)
                Add( new clsAseteNormaAmortizimi(rreshti));
            return true;
        }

        public static List<string> merrDataArtikujNorma(int idkoka)
        {
            return colAseteNormaAmortizimiAbstract.merrDataArtikujNorma(idkoka, enumObjekteAmortizimi.ASETE);
        }

        public static DataTable ktheArtikujNormaAmortizimiDTExport(int idnderm)
        {
            return colAseteNormaAmortizimiAbstract.ktheArtikujNormaAmortizimiDTExport(idnderm, enumObjekteAmortizimi.ASETE);
        }

        #endregion
    }
}
