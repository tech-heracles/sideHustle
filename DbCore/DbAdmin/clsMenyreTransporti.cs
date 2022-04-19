using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DbCore.IMBUtils.Logging;

namespace DbCore.DbAdmin
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne menyrat e transportit 
    ///  te cilat i caktohen klientit.
    ///  (Te dhenat  merren nga tabela : T_LLOJPERIUDHE)
    /// </summary>
    public class clsMenyreTransporti
    {
        #region Atribute

        private int idMenyreTransporti;
        private String kodiMenyreTransporti;
        private String pershkrimiMenyreTransporti;
        private int idNdermarje;
        private int idPerdoruesi;
  
        private int idStatusDok;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private DataRow rreshti;

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases
        /// </summary> 
        public clsMenyreTransporti(int id, String kodi, String pershkrimi, int idnderm, int idperdoruesi, int idstatusdok)
        {
            idMenyreTransporti = id;
            kodiMenyreTransporti = kodi;
            pershkrimiMenyreTransporti = pershkrimi;
            idNdermarje = idnderm;
            idPerdoruesi = idperdoruesi;
            idStatusDok = idstatusdok;

        }

        /// <summary>
        /// konstruktor me 2 parametra
        /// </summary>
        /// <param name="kodi">kodi i menyres se transportit</param>
        /// <param name="idndermarje">id e ndermarrjes</param>
        public clsMenyreTransporti(string kodi, int idndermarje)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            mbushMenyreTransport(data.merrMenyreTransportiSipasKodit(kodi, idndermarje));
            data.Dispose();
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="id">id e menyres se transportit</param>
        public clsMenyreTransporti(int id)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            mbushMenyreTransport(data.merrMenyreTransporti(id));
            data.Dispose();
        }

        /// <summary>
        /// Konstruktori default i klases
        /// </summary>
        public clsMenyreTransporti()
        {
        }

        public clsMenyreTransporti(DataRow rreshti)
        {
            
            mbushMenyreTransport(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdMenyreTransporti
        {
            get { return idMenyreTransporti; }
            set { idMenyreTransporti = value; }
        }

        /// <summary>
        /// Kthen/Vendos kodin e menyres se transportit.
        /// </summary>
        public String KodiMenyreTransporti
        {
            get { return kodiMenyreTransporti; }
            set { kodiMenyreTransporti = value; }
        }

        /// <summary>
        /// Kthen/Vendos pershkrimin e menyres se transportit.
        /// </summary>
        public String PershkrimiMenyreTransporti
        {
            get { return pershkrimiMenyreTransporti; }
            set { pershkrimiMenyreTransporti = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e ndermarrjes se ciles i perket kjo menyre transporti.
        /// </summary>
        public int IdNdermarje
        {
            get { return idNdermarje; }
            set { idNdermarje = value; }
        }
        public int IdPerdoruesi
            {
            get
                {
                return idPerdoruesi;
                }
            set
                {
                idPerdoruesi = value;
                }
            }
        public int IdStatusDok
            {
            get { return idStatusDok; }
            set { idStatusDok = value; }
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
        /// Ruan objektin menyres se transportit ne tabelen perkatese ne databaze.
        /// </summary>
        public clsMesazh ruaj()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            clsMesazh u_ruajt = data.ruajMenyreTransporti(this.IdMenyreTransporti, this.KodiMenyreTransporti, this.PershkrimiMenyreTransporti, this.IdNdermarje, this.idPerdoruesi, this.idStatusDok);
            data.Dispose();
            return u_ruajt;
        }

        /// <summary>
        /// Modifikon objektin menyres se transportit ne tabelen perkatese ne databaze.
        /// </summary>
        public clsMesazh modifiko()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            clsMesazh u_modifikua = data.modifikoMenyreTransporti(this.IdMenyreTransporti, this.KodiMenyreTransporti, this.PershkrimiMenyreTransporti, this.IdNdermarje, this.idPerdoruesi, this.idStatusDok);
            data.Dispose();
            return u_modifikua;
        }

        /// <summary>
        /// Fshin objektin menyres se transportit nga tabela perkatese ne databaze.
        /// </summary>
        public clsMesazh fshi()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            clsMesazh u_fshi = data.fshiMenyreTransportiStatus(this.IdMenyreTransporti, this.idPerdoruesi);
            data.Dispose();
            return u_fshi;
        }
        public static bool ekziston(string kodi, int idndermarje)
        {
            clsDatabaseAdmin db = new clsDatabaseAdmin();
            bool ekziston = db.ekzistonMenyreTransportiMeKeteKod(kodi, idndermarje);
            db.Dispose();
            return ekziston;
        }
        #endregion

        #region Metoda Internal

        internal bool mbushMenyreTransport(DataRow dbDataRowMenyreTransport)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda mbush menyre transporti.");
            if (dbDataRowMenyreTransport != null)
            {

                try
                {
                    int.TryParse(dbDataRowMenyreTransport["IDMENYRETRANSPORTI"].ToString(), out idMenyreTransporti);
                    kodiMenyreTransporti = dbDataRowMenyreTransport["KODIMENYRETRANSPORTI"].ToString();
                    pershkrimiMenyreTransporti = dbDataRowMenyreTransport["PERSHKRIMIMENYRETRANSPORTI"].ToString();
                    int.TryParse(dbDataRowMenyreTransport["IDNDERMARJE"].ToString(), out idNdermarje);
                    int.TryParse(dbDataRowMenyreTransport["IDSTATUSDOK"].ToString(), out idStatusDok);
                    int.TryParse(dbDataRowMenyreTransport["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    DateTime.TryParse(dbDataRowMenyreTransport["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRowMenyreTransport["DTMODIFIKIMI"].ToString(), out dtModifikimi);
              
                    return true;
                }
                catch (InvalidCastException)
                {
                    ImbLogger.LogErrorShitje("ERROR: Gabim gjate marrjes se menyres se transportit nga db-ja");
                    throw new Exception("ERROR: Gabim gjate marrjes se menyres se transportit nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}
