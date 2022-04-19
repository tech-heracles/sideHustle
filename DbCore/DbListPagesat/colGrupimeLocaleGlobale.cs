using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DbCore.DbListPagesat
{
  public  class colGrupimeLocaleGlobale: List<clsGrupimeLocaleGlobale>
    {
        #region Konstruktori

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public colGrupimeLocaleGlobale()
        {
        }

        /// <summary>
        /// konstruktori me 1 parametra
        /// merr burimet te ndermarjes 
        /// </summary>
        /// <param name="idndermarje">id e ndermarrjes</param>
        public colGrupimeLocaleGlobale(int idndermarje, int lloji)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            mbushGrupime(db.ktheGjitheGrupimeLocaleGlobaletSipasNdermarjesDheLlojit(idndermarje,lloji));
            db.Dispose();
        }

        /// <summary>
        /// konstruktori qe implementon klasen baze
        /// </summary>
        /// <param name="collection">koleksion me clsGrupimeLocaleGlobale</param>
        public colGrupimeLocaleGlobale(IEnumerable<clsGrupimeLocaleGlobale> collection)
            : base(collection)
        {

        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="clsGrupimeLocaleGlobale"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsGrupimeLocaleGlobale this[int index]
        {
            get
            {
                return ((clsGrupimeLocaleGlobale)base[index]);
            }
        }

        /// <summary>
        /// merr burimet sipas id ne formen e data row
        /// </summary>
        /// <param name="id"> id burimit</param>
        /// <returns> kthen data row me kete burim</returns>
        public static DataRow merrGrupimeLocaleGlobaleSipasIdDR(int id)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            DataRow dr = db.merrGrupimeLocaleGlobaleSipasIdDR(id);
            db.Dispose();
            return dr;
        }

        /// <summary>
        /// merr burim sipas ndermarje  ne forme data table
        /// </summary>
        /// <param name="idnderm"> idndermarje</param>
        /// <returns> kthen data table me keto burime</returns>
        public static DataTable merrGrupimeLocaleGlobaleDT(int idnderm)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            DataTable dt = db.merrGrupimeLocaleGlobaleDT(idnderm);
            db.Dispose();
            return dt;
        }

        /// <summary>
        /// merr burim sipas ndermarje aktive  ne forme data table
        /// </summary>
        /// <param name="idnderm"> idndermarje</param>
        /// <returns> kthen data table me keto burime</returns>
        public static DataTable merrGrupimeLocaleGlobaleDTAktive(int idnderm, int lloji)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            DataTable dt = db.merrGrupimeLocaleGlobaleDTAktive(idnderm,lloji);
            db.Dispose();
            return dt;
        }
        public static DataTable merrGrupimeLocaleGlobaleDTAktiveSipasPrindit(int idnderm, int idprindi)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            DataTable dt = db.merrGrupimeLocaleGlobaleDTAktiveSipasPrindit(idnderm, idprindi);
            db.Dispose();
            return dt;
        }

        /// <summary>
        /// mbush te gjitha burimet sipas ndermarrjes 
        /// </summary>
        /// <param name="idnder">id e ndermarrjes</param>
        /// <returns>kthen true kur mbushja kryhet me sukses, ne te kundert false</returns>
        public bool ktheGjitheGrupimeLocaleGlobaletSipasNdermarjesDheLlojit(int idnder, int lloji)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            bool mbush = mbushGrupime(db.ktheGjitheGrupimeLocaleGlobaletSipasNdermarjesDheLlojit(idnder, lloji));
            db.Dispose();
            return mbush;            
        }

        /// <summary>
        /// mbush te gjitha burimet sipas ndermarrjes  aktive
        /// </summary>
        /// <param name="idnder">id e ndermarrjes</param>
        /// <returns>kthen true kur mbushja kryhet me sukses, ne te kundert false</returns>
        public bool ktheGjitheGrupimeLocaleGlobaletSipasNdermarjesDheLlojitAktiv(int idnder, int lloji)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            bool mbush = mbushGrupime(db.ktheGjitheGrupimeLocaleGlobaletSipasNdermarjesDheLlojitAktiv(idnder, lloji));
            db.Dispose();
            return mbush;
        }
        public bool ktheGjitheGrupimeLocaleGlobaletSipasNdermarjesDhePrinditAktiv(int idnder, int idprindi)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            bool mbush = mbushGrupime(db.ktheGjitheGrupimeLocaleGlobaletSipasNdermarjesDhePrinditAktiv(idnder, idprindi));
            db.Dispose();
            return mbush;
        }
        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        /// </summary>
        /// <param name="dt">data table me te dhena te tipit burim</param>
        /// <returns>true ose false nqs objekti u mbush ne rregull me te dhena</returns>
        private bool mbushGrupime(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsGrupimeLocaleGlobale grupim = new clsGrupimeLocaleGlobale();
                    //grupim.mbushGrupime(rreshti);
                    Add(new clsGrupimeLocaleGlobale(rreshti));
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
