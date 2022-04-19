using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.DbAsete
{
    public class colAmortizimiTrupi : colAmortizimiTrupiAbstract
    {


        public override enumObjekteAmortizimi objektiKod => enumObjekteAmortizimi.ASETE;

        #region Konstruktore

        /// <summary>
        /// MODULI ASETE:
        /// Krijon nje list objektesh bosh te klases clsAmortizimiTrupi per trupat e dokumentave te amortizimit.
        /// </summary>
        public colAmortizimiTrupi() : base()
        {
        }

  

        #endregion

        #region Metoda Publike

        /// <summary>
        /// MODULI ASETE:
        /// Metode qe sherben per te marre te dhenat nga store procedura te lidhura me tabelen T_ASETE_LIDHJE_ARTIKULL_LLOJAMORT ne nje list objektesh clsAseteNormaAmortizimi.
        /// </summary>
        /// <param name="dt">(DataTable) Merr si parameter nje datatable qe kthehet nga store procedura per te mbushur nje list me objekte.</param>
        /// <returns>(bool) Kthen true nese nuk ndodh asnje gabim gjate leximit te te dhenave, ne te kundert false.</returns>
        public override bool mbushAmortizimiTrupiList(DataTable dt)
        {
            foreach (DataRow rreshti in dt.Rows)
                Add(new clsAmortizimiTrupi(rreshti));
            return true;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Metode qe sherben per te marre te dhenat nga store procedura te lidhura me tabelen T_ASETE_AMORTIZIMI_TRUPI ne nje list objektesh clsAmortizimiTrupi per rillogaritjen e artikujve pa serial.
        /// </summary>
        /// <param name="dt">(DataTable) Merr si parameter nje datatable qe kthehet nga store procedura per te mbushur nje list me objekte.</param>
        /// <returns>(bool) Kthen true nese nuk ndodh asnje gabim gjate leximit te te dhenave, ne te kundert false.</returns>
        public override bool mbushAmortizimiTrupiListPerRillogaritjeArtikujPaSerial(DataTable dt)
        {
            try
            {
                foreach (DataRow rreshti in dt.Rows)
                {
                    clsAmortizimiTrupiAbstract amortizimiTrupi = new clsAmortizimiTrupi();
                    amortizimiTrupi.mbushAmortizimTrupiObjektPerRillogaritjeArtikujPaSerial(rreshti);
                    Add(amortizimiTrupi);
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        #endregion
    }
}
