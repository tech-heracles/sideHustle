using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.DbAsete
{
    /// <summary>
    /// MODULI ASETE:
    /// Mban nje list objektesh colSerialetPerRivleresim (objekte per ruajtjen e serialeve me rivleresim) ne modulin e Aseteve.
    /// </summary>
    public class colSerialetPerRivleresim : List<clsSerialPerRivleresim>
    {
        #region Konstruktore

        /// <summary>
        /// MODULI ASETE:
        /// Krijon nje list objektesh bosh te klases clsSerialPerRivleresim per serialet me rivleresim.
        /// </summary>
        public colSerialetPerRivleresim()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="colTrupi">(colAmortizimiTrupi) Trupi i serialeve per te cilat do krijohen serialet per rivleresim</param>
        /// <param name="dateDokumenti">(DateTime) Data e dokumentit te rivleresimit</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes</param>
        /// <param name="idPerdoruesi">(int) Id e perdoruesit</param>
        /// <param name="idLlojStandarti">(int) Id e llojit te standartit</param>
        /// <param name="dbasete">clsdatabase asete per rastet e transaksionit</param>
        public colSerialetPerRivleresim(colAmortizimiTrupiAbstract colTrupi, DateTime dateDokumenti, int idNdermarrje, int idPerdoruesi, int idLlojStandarti, int idStatusDokumenti, clsDatabazeAsete dbasete)
        {
            foreach(clsAmortizimiTrupiAbstract trupi in colTrupi)
            {
                double sasia;
                DbInventari.clsArtikulli artikulli = new DbInventari.clsArtikulli(trupi.IdArtikulli);
                if (!artikulli.MeSerial)
                    sasia = clsHistorikAQTSeriale.merrHistorikuAQTSerialSasiSipasIDAQTSerialit(trupi.IdAQTSeriali, idNdermarrje);
                else
                    sasia = 1;
                Add(new clsSerialPerRivleresim(trupi.IdAQTSeriali, trupi.IdNjesiAdministrative, idLlojStandarti, sasia, trupi.VleftaPlusMinus + trupi.VleftaShteseRivleresim, dateDokumenti, idNdermarrje, idPerdoruesi, idStatusDokumenti));
            }
        }

        #endregion

        #region Metoda Publike

        public clsMesazh ruajSerialetXRivleresim(int idKoka, int idStatusDokumenti)
        {
            clsMesazh pergjigja = new clsMesazh(true, "Ruajtja e rivleresimit u krye me sukses!");
            foreach (clsSerialPerRivleresim serialRivleresimi in this)
            {
                serialRivleresimi.IdKoka = idKoka;
                serialRivleresimi.IdStatusDokumenti = idStatusDokumenti;
                pergjigja = serialRivleresimi.ruaj();
                if (!pergjigja.Status)
                    return pergjigja;
            }
            return pergjigja;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// MODULI ASETE:
        /// Metode qe sherben per te marre te dhenat nga store procedura te lidhura me tabelen T_ASETE_AQTSERIALE ne nje list objektesh clsAmortizimiFillestar.
        /// </summary>
        /// <param name="dt">(DataTable) Merr si parameter nje datatable qe kthehet nga store procedura per te mbushur nje list me objekte.</param>
        /// <returns>(bool) Kthen true nese nuk ndodh asnje gabim gjate leximit te te dhenave, ne te kundert false.</returns>
        private bool mbushSerialeMeRivlersim(DataTable dt)
        {
            foreach (DataRow rreshti in dt.Rows)
            {
                Add(new clsSerialPerRivleresim(rreshti));
            }
            return true;
        }

        #endregion
    }
}
