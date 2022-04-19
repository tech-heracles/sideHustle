using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DbCore.DbAsete
{
    public class colNormaAmortizimiRezerva : colAseteNormaAmortizimiAbstract
    {
        public override enumObjekteAmortizimi objektiKod => enumObjekteAmortizimi.REZERVA;

        #region Konstruktore

        /// <summary>
        /// Konstruktore bosh
        /// </summary>
        public colNormaAmortizimiRezerva() : base()
        {

        }

        #endregion

        #region Metoda Publike

        public static List<string> merrDataArtikujNorma(int idkoka)
        {
            return colAseteNormaAmortizimiAbstract.merrDataArtikujNorma(idkoka, enumObjekteAmortizimi.REZERVA);
        }

        public static DataTable ktheArtikujNormaAmortizimiDTExport(int idnderm)
        {
            return colAseteNormaAmortizimiAbstract.ktheArtikujNormaAmortizimiDTExport(idnderm, enumObjekteAmortizimi.REZERVA);
        }

        public static DataTable ktheNormaperEkport (int idnderm)
        {
            using(clsDatabazeAsete moduliAsete = new clsDatabazeAsete()){
                return moduliAsete.ktheNormaperEkport(idnderm);
            }
           
        }
        /// <summary>
        /// MODULI ASETE:
        /// Metode qe sherben per te marre te dhenat nga store procedura te lidhura me tabelen T_ASETE_LIDHJE_ARTIKULL_LLOJAMORT ne nje list objektesh clsAseteNormaAmortizimi.
        /// </summary>
        /// <param name="dt">(DataTable) Merr si parameter nje datatable qe kthehet nga store procedura per te mbushur nje list me objekte.</param>
        /// <returns>(bool) Kthen true nese nuk ndodh asnje gabim gjate leximit te te dhenave, ne te kundert false.</returns>
        public override bool mbushNormaAmortizimiList(DataTable dt)
        {
            foreach (DataRow rreshti in dt.Rows)
                Add(new clsNormaAmortizimiRezerva(rreshti));
            return true;
        }

        #endregion

    }
}
