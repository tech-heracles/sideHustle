using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace DbCore.DbListPagesat
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne nje grup Punonjesish
    ///  (Te dhenat  merren nga tabela : T_GRUPPunonjesish)
    /// </summary>
    public class clsGrupPunonjesish
    {
        private const string gabimNeTeDhena = "ERROR: Gabim gjate marrjes se grupit te Punonjesishs nga db-ja";
        private const string gabimEkzistence = "Ekziston nje grup punonjesish me kete kod. Ju lutem shenoni nje tjeter!";
        #region Atributet

        private int idGrupPunonjesish;
        private string nr;
        private string pershkrim;
        private int idPerdoruesi;
        private int idNdermarje;
        private int idStatusDok;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private DataRow rreshti;


        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsGrupPunonjesish(int idgrupPunonjesish, string nr, string pershkrim, int idperdoruesi, int idndermarje, int idstatusdok)
        {
            idGrupPunonjesish = idgrupPunonjesish;
            this.nr = nr;
            this.pershkrim = pershkrim;
            idPerdoruesi = idperdoruesi;
            idNdermarje = idndermarje;
            idStatusDok = idstatusdok;
        }

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsGrupPunonjesish(string nr, string pershkrim, int idperdoruesi, int idndermarje, int idstatusdok)
        {
            this.nr = nr;
            this.pershkrim = pershkrim;
            idPerdoruesi = idperdoruesi;
            idNdermarje = idndermarje;
            idStatusDok = idstatusdok;
        }

        /// <summary>
        /// konstruktor me 2 parametra
        /// </summary>
        /// <param name="kodi">kodi i grupit</param>
        /// <param name="idnderm">id e ndermarrjes</param>
        public clsGrupPunonjesish(string kodi, int idnderm)
        {
            clsDatabazeListPagesa data = new clsDatabazeListPagesa();
            mbushGrupPunonjesish(data.ktheGrupPunonjesishSipasKodit(kodi, idnderm));
            data.Dispose();          
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="id">id e grupit</param>
        public clsGrupPunonjesish(int id)
        {
            clsDatabazeListPagesa data = new clsDatabazeListPagesa();
            mbushGrupPunonjesish(data.merrGrupPunonjesishSipasId(id));
            data.Dispose();
        }public clsGrupPunonjesish(int id,clsDatabazeListPagesa data)
        {
              mbushGrupPunonjesish(data.merrGrupPunonjesishSipasId(id));
     
        }

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsGrupPunonjesish()
        {
        }

        public clsGrupPunonjesish(DataRow rreshti)
        {
            
            mbushGrupPunonjesish(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdGrupPunonjesish
        {
            get { return idGrupPunonjesish; }
            set { idGrupPunonjesish = value; }
        }
        /// <summary>
        /// Kthen/Vendos kodin e grupit te Punonjesit.
        /// </summary>
        public string Nr
        {
            get { return nr; }
            set { nr = value; }
        }
        /// <summary>
        /// Kthen/Vendos pershkrimin e grupit te Punonjesit.
        /// </summary>
        public String Pershkrim
        {
            get { return pershkrim; }
            set { pershkrim = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e perdoruesit qe celi grupin e Punonjesit.
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
        #endregion

        #region Metoda Publike

        /// <summary>
        /// Ruan objektin e grupit te Punonjesishs ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="clsDatabazeListPagesa.ruajGrupPunonjesish"/> 
        /// </summary>
        public clsMesazh ruaj()
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            if (db.ekzistonGrupPunonjesish(nr, idNdermarje))
                return new clsMesazh(false, gabimEkzistence);
            clsMesazh u_ruajt = db.ruajGrupPunonjesish(IdGrupPunonjesish, Nr, Pershkrim, IdPerdoruesi, IdNdermarje, idStatusDok);
            db.Dispose();
            return u_ruajt;
        }

        /// <summary>
        /// Modifikon objektin e grupit te Punonjesishs ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="clsDatabazeListPagesa.modifikoGrupPunonjesish"/> 
        /// </summary>
        public clsMesazh modifiko()
        {
            clsDatabazeListPagesa data = new clsDatabazeListPagesa();
            clsMesazh u_modifikua = data.modifikoGrupPunonjesish(IdGrupPunonjesish, Nr, Pershkrim, IdPerdoruesi, IdNdermarje, idStatusDok);
            data.Dispose();
            return u_modifikua;
        }

        /// <summary>
        /// Fshin objektin e grupit te Punonjesishs ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="clsDatabazeListPagesa.fshiGrupPunonjesish"/> 
        /// </summary>
        public clsMesazh fshi()
        {
            clsDatabazeListPagesa data = new clsDatabazeListPagesa();
            clsMesazh u_fshi = data.fshiGrupPunonjesishStatus(idGrupPunonjesish, idPerdoruesi);
            data.Dispose();
            return u_fshi;
        }

        /// <summary>
        /// kontrollon nese ka veprime ke kete grup
        /// </summary>
        /// <returns></returns>
        public bool kaPunonjes()
        {
            clsDatabazeListPagesa data = new clsDatabazeListPagesa();
            bool kaPunonjes = data.kaPunonjes(idGrupPunonjesish);
            data.Dispose();
            return kaPunonjes;            
        }

        /// <summary>
        /// kontrollon nese eksiton nje grup punonjesish me kete kodi
        /// </summary>
        /// <param name="kodi">kodi</param>
        /// <param name="idndermarje">ndermarja</param>
        /// <returns></returns>
        public static bool ekziston(string kodi, int idndermarje)
        {
            clsDatabazeListPagesa data = new clsDatabazeListPagesa();
            bool ekziston = data.ekzistonGrupPunonjesish(kodi,idndermarje);
            data.Dispose();
            return ekziston;
        }
        #endregion

        #region Metoda Internal
        /// <summary>
        /// mbush grupin e punonjesve nga databaza
        /// </summary>
        /// <param name="dbDataRowGrupPunonjesish"></param>
        /// <returns></returns>
        internal bool mbushGrupPunonjesish(DataRow dbDataRowGrupPunonjesish)
        {
            if (dbDataRowGrupPunonjesish != null)
            {
                try
                {
                    int.TryParse(dbDataRowGrupPunonjesish["IDGRUPPunonjesish"].ToString(), out idGrupPunonjesish);
                    nr = dbDataRowGrupPunonjesish["NR"].ToString();
                    pershkrim = dbDataRowGrupPunonjesish["PERSHKRIM"].ToString();
                    int.TryParse(dbDataRowGrupPunonjesish["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    int.TryParse(dbDataRowGrupPunonjesish["IDNDERMARJE"].ToString(), out idNdermarje);
                    int.TryParse(dbDataRowGrupPunonjesish["IDSTATUSDOK"].ToString(), out idStatusDok);
                    DateTime.TryParse(dbDataRowGrupPunonjesish["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRowGrupPunonjesish["DTMODIFIKIMI"].ToString(), out dtModifikimi);
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

