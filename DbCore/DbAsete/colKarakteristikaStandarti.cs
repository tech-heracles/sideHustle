using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DbCore.DbAsete
{
    /// <summary>
    /// MODULI ASETE:
    /// Mban nje list objektesh clsKarakteristikaStandarti (objekte per karakteristikat e ndryshme te standarteve) ne modulin e Aseteve.
    /// Te dhenat merret nga tabela T_ASETE_LIDHJE_STANDART_STATUSMAG
    /// </summary>
    public class colKarakteristikaStandarti : List<clsKarakteristikaStandarti>
    {
        #region Konstruktore

        /// <summary>
        /// MODULI ASETE:
        /// Krijon nje list objektesh bosh te klases clsKarakteristikaStandarti per karakteristikat e standarteve.
        /// </summary>
        public colKarakteristikaStandarti()
        {

        }
        public colKarakteristikaStandarti(int idNdermarrje,clsDatabazeAsete db) : base(db.merrKarakteristikaSipasNdermarrjesMeKontabilizim(idNdermarrje))
        {

        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// MODULI ASETE:
        /// Mbush te gjitha konfigurimet e standarteve per ndermarrjen ne perdorim.
        /// </summary>
        /// <param name="idNderm">(int) Id e ndermarrjes ne perdorim per te cilen eshte specifikuar konfigurimi i standartit.</param>
        /// <returns>Kthen True nese mbushja kryhet me sukses, ne te kundert kthen false.</returns>
        public bool mbushGjitheNjesiAdministrative(int idNderm)
        {
            clsDatabazeAsete db = new clsDatabazeAsete();
            bool mbush = mbushKarakteristikaList(db.ktheGjitheKonfigurimeStandartit(idNderm));
            db.Dispose();
            return mbush;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Mbush nje DataTable me te gjitha konfigurimet e standarteve per ndermarrjen ne perdorim. Perdorur me emra konvencional ne store procedure.
        /// </summary>
        /// <param name="idnderm">(int) Id e ndermarrjes ne perdorim per te cilen eshte specifikuar konfigurimi i standartit.</param>
        /// <returns>Kthen nje DataTable me konfigurimet e standarteve.</returns>
        public static DataTable merrSipasNjesiNdermarrjesDT(int idnderm)
        {
            clsDatabazeAsete db = new clsDatabazeAsete();
            DataTable dt = db.merrSipasKonfigurimiStandartitDT(idnderm);
            db.Dispose();
            return dt;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// MODULI ASETE:
        /// Metode qe sherben per te marre te dhenat nga store procedura te lidhura me tabelen T_ASETE_LIDHJE_STANDART_STATUSMAG ne nje list objektesh clsKarakteristikaStandarti.
        /// </summary>
        /// <param name="dt">(DataTable) Merr si parameter nje datatable qe kthehet nga store procedura per te mbushur nje list me objekte.</param>
        /// <returns>(bool) Kthen true nese nuk ndodh asnje gabim gjate leximit te te dhenave, ne te kundert false.</returns>
        private bool mbushKarakteristikaList(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsKarakteristikaStandarti karakteristika = new clsKarakteristikaStandarti();
                    //karakteristika.mbushKarakteristikaObjekt(rreshti);
                    Add(new clsKarakteristikaStandarti(rreshti));
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
