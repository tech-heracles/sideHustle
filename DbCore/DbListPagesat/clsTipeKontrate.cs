using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace DbCore.DbListPagesat
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne nje tip kontrate
    ///  (Te dhenat  merren nga tabela : T_TipeKontrate)
    /// </summary>
    public class clsTipeKontrate
    {
        private const string gabimNeTeDhena = "ERROR: Gabim gjate marrjes se tipeve te kontratave nga db-ja";
        private const string gabimEkzistence = "Ekziston nje tip kontrate me kete kod. Ju lutem shenoni nje tjeter!";
        #region Atributet

        private int idTipKontrate;
        private string kodi;
        private int idPerdoruesi;
        private int idNdermarje;
        private int idStatusDok;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private string kodiAng;
        private DataRow rreshti;
       
        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsTipeKontrate(int idtipkontrate,  string kodi, int idperdoruesi, int idndermarje, int idstatusdok, string kodiang)
        {
            idTipKontrate = idtipkontrate;
            this.kodi = kodi;
            idPerdoruesi = idperdoruesi;
            idNdermarje = idndermarje;
            idStatusDok = idstatusdok;
            this.kodiAng = kodiang;
        }

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsTipeKontrate( string kodi, int idperdoruesi, int idndermarje, int idstatusdok, string kodiang)
        {
            this.kodi = kodi;
            idPerdoruesi = idperdoruesi;
            idNdermarje = idndermarje;
            idStatusDok = idstatusdok;
            kodiAng = kodiang;
        }

        /// <summary>
        /// konstruktor me 2 parametra
        /// </summary>
        /// <param name="kodi">kodi i tipit te kontrates</param>
        /// <param name="idnderm">id e ndermarrjes</param>
        public clsTipeKontrate(string kodi, int idnderm, int idgjuha)
        {
            clsDatabazeListPagesa data = new clsDatabazeListPagesa();
            if(idgjuha==0)
            mbushTipKontrate(data.ktheTipeKontrateSipasKodit(kodi, idnderm));
            else
                mbushTipKontrate(data.ktheTipeKontrateSipasKoditAng(kodi, idnderm));
            data.Dispose();
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="id">id e tipit te kontrates</param>
        public clsTipeKontrate(int id)
        {
            clsDatabazeListPagesa data = new clsDatabazeListPagesa();
            mbushTipKontrate(data.merrTipeKontrateSipasId(id));
            data.Dispose();
        } public clsTipeKontrate(int id,clsDatabazeListPagesa data)
        {
            mbushTipKontrate(data.merrTipeKontrateSipasId(id));
           
        }

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsTipeKontrate()
        {
        }

        public clsTipeKontrate(DataRow rreshti)
        {
            
            mbushTipKontrate(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdTipKontrate
        {
            get { return idTipKontrate; }
            set { idTipKontrate = value; }
        }
       
        /// <summary>
        /// Kthen/Vendos Kodin e tipit te kontrates
        /// </summary>
        public String Kodi
        {
            get { return kodi; }
            set { kodi = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e perdoruesit qe celi tipin e kontrates.
        /// </summary>
        public int IdPerdoruesi
        {
            get { return idPerdoruesi; }
            set { idPerdoruesi = value; }
        }
        /// <summary>
        /// Kthen/Vendos id e ndermarjes.
        /// </summary>
        public int IdNdermarje
        {
            get { return idNdermarje; }
            set { idNdermarje = value; }
        }
        /// <summary>
        /// kthen vendos id e statusit te dokumentit
        /// </summary>
        public int IdStatusDok
        {
            get { return idStatusDok; }
            set { idStatusDok = value; }
        }
        /// <summary>
        /// kthen  dt e krijimit 
        /// </summary>
        public DateTime DtKrijimi
        {
            get { return dtKrijimi; }

        }
        /// <summary>
        /// kthen daten e modifikimit
        /// </summary>
        public DateTime DtModifikimi
        {
            get { return dtModifikimi; }

        }

        public string KodiAng
        {
            get
            {
                return kodiAng;
            }

            set
            {
                kodiAng = value;
            }
        }
        #endregion

        #region Metoda Publike

        /// <summary>
        /// Ruan objektin e tip kontrate ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="clsDatabazeListPagesa.ruajTipKontrate"/> 
        /// </summary>
        public clsMesazh ruaj()
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            if (db.ekzistonTipKontrate(kodi, idNdermarje))
                return new clsMesazh(false, gabimEkzistence);
            if (kodiAng!="" &&db.ekzistonTipKontrateAng(kodiAng, idNdermarje))
                return new clsMesazh(false, gabimEkzistence);
            clsMesazh u_ruajt = db.ruajTipKontrate(IdTipKontrate, Kodi, IdPerdoruesi, IdNdermarje, idStatusDok, kodiAng);
            db.Dispose();
            return u_ruajt;
        }

        /// <summary>
        /// Modifikon objektin e tipin e kontrates ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="clsDatabazeListPagesa.modifikoTipKontrate"/> 
        /// </summary>
        public clsMesazh modifiko()
        {
            clsDatabazeListPagesa data = new clsDatabazeListPagesa();
            clsMesazh u_modifikua = data.modifikoTipKontrate(IdTipKontrate, Kodi, IdPerdoruesi, IdNdermarje, idStatusDok, kodiAng);
            data.Dispose();
            return u_modifikua;
        }

        /// <summary>
        /// Fshin objektin e  tip kontrate ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="clsDatabazeListPagesa.fshiTipKontrateStatus"/> 
        /// </summary>
        public clsMesazh fshi()
        {
            clsDatabazeListPagesa data = new clsDatabazeListPagesa();
            clsMesazh u_fshi = data.fshiTipKontrateStatus(idTipKontrate, idPerdoruesi);
            data.Dispose();
            return u_fshi;
        }

        /// <summary>
        /// kontrollon nese ka veprime ke kete tip kontrate
        /// </summary>
        /// <returns></returns>
        public bool kaPunesim()
        {
            clsDatabazeListPagesa data = new clsDatabazeListPagesa();
            bool kaPunesim = data.kaPunesim(idTipKontrate);
            data.Dispose();
            return kaPunesim;            
        }

        /// <summary>
        ///  kontrollon nese ekziston nje tip kontrate me kete kod
        /// </summary>
        /// <param name="kodi">kodi</param>
        /// <param name="idndermarje">id ndermarje</param>
        /// <returns> true ose false</returns>
        public static bool ekziston(string kodi, int idndermarje)
        {
            clsDatabazeListPagesa data = new clsDatabazeListPagesa();
            bool ekziston = data.ekzistonTipKontrate(kodi, idndermarje);
            data.Dispose();
            return ekziston;
        }
        public static bool ekzistonAng(string kodiang, int idndermarje)
        {
            clsDatabazeListPagesa data = new clsDatabazeListPagesa();
            bool ekziston = data.ekzistonTipKontrateAng(kodiang, idndermarje);
            data.Dispose();
            return ekziston;
        }
        #endregion

        #region Metoda Internal
        /// <summary>
        /// mbush tipet e kontrates nga databaza
        /// </summary>
        /// <param name="dbDataRowTipKontrate"></param>
        /// <returns></returns>
        internal bool mbushTipKontrate(DataRow dbDataRowTipKontrate)
        {
            if (dbDataRowTipKontrate != null)
            {
                try
                {
                    int.TryParse(dbDataRowTipKontrate["IDTIPKONTRATE"].ToString(), out idTipKontrate);
                    kodi = dbDataRowTipKontrate["KODI"].ToString();
                    kodiAng = dbDataRowTipKontrate["KODIANG"].ToString();
                    int.TryParse(dbDataRowTipKontrate["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    int.TryParse(dbDataRowTipKontrate["IDNDERMARJE"].ToString(), out idNdermarje);
                    int.TryParse(dbDataRowTipKontrate["IDSTATUSDOK"].ToString(), out idStatusDok);
                    DateTime.TryParse(dbDataRowTipKontrate["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRowTipKontrate["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception(gabimNeTeDhena);
                }
            }
            else
                return false;
        }

        #endregion
    }
}

