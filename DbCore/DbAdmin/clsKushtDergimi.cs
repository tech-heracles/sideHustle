using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DbCore.IMBUtils.Logging;

namespace DbCore.DbAdmin
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne kushtet e dergimit.
    ///  (Te dhenat  merren nga tabela : T_KUSHTEDERGIMI)
    /// </summary>
    public class clsKushtDergimi
    {
        #region Atribute

        private int idKushtDergimi;
        private String kodiKushtDergimi;
        private String pershkrimiKushtDergimi;
        private int idNdermarje;
        private int idPerdoruesi;
        private int idStatusDok;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private DataRow rreshti;

        #endregion

        #region Konstruktor

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsKushtDergimi(int id, String kodi, String pershkrimi, int idnderm, int idstatusdok, int idperdoruesi)
        {
            idKushtDergimi = id;
            kodiKushtDergimi = kodi;
            pershkrimiKushtDergimi = pershkrimi;
            idNdermarje = idnderm;
            idStatusDok = idstatusdok;
            idPerdoruesi = idperdoruesi;

        }

        /// <summary>
        /// konstruktor me 2 parametra
        /// </summary>
        /// <param name="kodi">kodi i kushtit te dergimit</param>
        /// <param name="idndermarje">id e ndermarrjes</param>
        public clsKushtDergimi(string kodi, int idndermarje)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            mbushKushtDergimi(data.merrKushtDergimiSipaKodit(kodi, idndermarje));
            data.Dispose();
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="id">id e kusht dergimit</param>
        public clsKushtDergimi(int id)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            mbushKushtDergimi(data.merrKushtDergimi(id));
            data.Dispose();
        }

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsKushtDergimi()
        {
        }

        public clsKushtDergimi(DataRow rreshti)
        {
            
            mbushKushtDergimi(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdKushtDergimi
        {
            get { return idKushtDergimi; }
            set { idKushtDergimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos kodin e kushtit te dergimit.
        /// </summary>
        public String KodiKushtDergimi
        {
            get { return kodiKushtDergimi; }
            set { kodiKushtDergimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos pershkrimin e kushtit te dergimit.
        /// </summary>
        public String PershkrimiKushtDergimi
        {
            get { return pershkrimiKushtDergimi; }
            set { pershkrimiKushtDergimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e ndermarrjes se ciles i perket ky kusht dergimi
        /// </summary>
        public int IdNdermarje
        {
            get { return idNdermarje; }
            set { idNdermarje = value; }
        }
        public int IdStatusDok
            {
            get { return idStatusDok; }
            set { idStatusDok = value; }
            }      
        public int IdPerdoruesi
            {
            get { return idPerdoruesi; }
            set { idPerdoruesi = value; }
            }
        public DateTime DtKrijimi
            {
            get { return dtKrijimi; }

            }
        public DateTime DtModifikimi
            {
            get { return dtModifikimi; }

            }
        #endregion

        #region Metoda Publike

        /// <summary>
        /// Ruan objektin e kushtit te dergimit ne tabelen perkatese ne databaze.
        /// </summary>
        public clsMesazh ruaj()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            clsMesazh u_ruajt = data.ruajKushtDergimi(this.IdKushtDergimi, this.KodiKushtDergimi, this.PershkrimiKushtDergimi, this.IdNdermarje, this.idPerdoruesi, this.idStatusDok);
            data.Dispose();
            return u_ruajt;
        }

        /// <summary>
        /// Modifikon rreshtin perkates ne databaze duke perdorur te dhenat qe jane tek objekti i kushtit te dergimit.
        /// </summary>
        public clsMesazh modifiko()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            clsMesazh u_modifikua = data.modifikoKushtDergimi(this.IdKushtDergimi, this.KodiKushtDergimi, this.PershkrimiKushtDergimi, this.IdNdermarje, this.idPerdoruesi, this.idStatusDok);
            data.Dispose();
            return u_modifikua;
        }

        /// <summary>
        /// Fshin rreshtin perkates nga tabela perkatese ne databaze sipas ID-se qe i eshte caktuar objektit te 
        /// kushtit te dergimit.
        /// </summary>
        public clsMesazh fshi()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            clsMesazh u_fshi = data.fshiKushtDergimiStatus(this.IdKushtDergimi, this.idPerdoruesi);
            data.Dispose();
            return u_fshi;
        }
        public static bool ekziston(string kodi, int idnderm)
        {
            clsDatabaseAdmin db = new clsDatabaseAdmin();
            bool ekziston = db.ekzistonKushtDergimiMeKeteKod(kodi, idnderm);
            db.Dispose();
            return ekziston;
        }
        #endregion

        #region Internal

        internal bool mbushKushtDergimi(DataRow dbDataRowKushtDergimi)
        {
            ImbLogger.LogTraceShitje("Filloi metoda mbush kusht dergimi!");
            if (dbDataRowKushtDergimi != null)
            {
                try
                {
                    int.TryParse(dbDataRowKushtDergimi["IDKUSHTDERGIMI"].ToString(), out idKushtDergimi);
                    kodiKushtDergimi = dbDataRowKushtDergimi["KODIKUSHTDERGIMI"].ToString();
                    pershkrimiKushtDergimi = dbDataRowKushtDergimi["PERSHKRIMIKUSHTDERGIMI"].ToString();
                    int.TryParse(dbDataRowKushtDergimi["IDNDERMARJE"].ToString(), out idNdermarje);
                    int.TryParse(dbDataRowKushtDergimi["IDSTATUSDOK"].ToString(), out idStatusDok);
                    int.TryParse(dbDataRowKushtDergimi["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    DateTime.TryParse(dbDataRowKushtDergimi["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRowKushtDergimi["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    ImbLogger.LogTraceShitje("Mbaroi metoda mbush kusht dergimi!");
                    return true;
                }
                catch (InvalidCastException)
                {
                    ImbLogger.LogErrorShitje("ERROR: Gabim gjate marrjes se kusht dergimit nga db-ja");
                    throw new Exception("ERROR: Gabim gjate marrjes se kusht dergimit nga db-ja");
                }
            }
            else
            {
                ImbLogger.LogTraceShitje("Mbaroi metoda mbush kusht dergimi!");
                return false;
            }

        }

        #endregion
    }
}
