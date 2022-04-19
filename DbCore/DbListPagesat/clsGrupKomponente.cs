using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace DbCore.DbListPagesat
{
  public  class clsGrupKomponente
    { 
        private const string gabimNeTeDhena = "ERROR: Gabim gjate marrjes se grupit te komponenteve nga db-ja";
        private const string gabimEkzistence = "Ekziston nje grup komponentesh me kete kod. Ju lutem shenoni nje tjeter!";
        #region Atributet

        private int id;
        private string kodi;
        private string pershkrimi;
        private int idPerdoruesi;
        private int idNdermarje;
        private int idStatusDok;
        private int idKrijuesi;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private int idRenditje;
        private DataRow rreshti;


        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsGrupKomponente(int id, string nr, string pershkrim, int idperdoruesi, int idndermarje, int idstatusdok, int idkrijuesi, int idrenditje)
        {
            this.id = id;
            this.kodi = nr;
            this.pershkrimi = pershkrim;
            idPerdoruesi = idperdoruesi;
            idNdermarje = idndermarje;
            idStatusDok = idstatusdok;
            this.idKrijuesi = idkrijuesi;
            idRenditje = idrenditje;
        }

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsGrupKomponente(string nr, string pershkrim, int idperdoruesi, int idndermarje, int idstatusdok, int idkrijuesi, int idrenditje)
        {
            this.kodi = nr;
            this.pershkrimi = pershkrim;
            idPerdoruesi = idperdoruesi;
            idNdermarje = idndermarje;
            idStatusDok = idstatusdok;
            this.idKrijuesi = idkrijuesi;
            this.idRenditje = idrenditje;
        }

        /// <summary>
        /// konstruktor me 2 parametra
        /// </summary>
        /// <param name="kodi">kodi i grupit</param>
        /// <param name="idnderm">id e ndermarrjes</param>
        public clsGrupKomponente(string kodi, int idnderm)
        {
            using (clsDatabazeListPagesa data = new clsDatabazeListPagesa())
            {
                mbushGrupKomponente(data.ktheGrupKomponenteSipasKodit(kodi, idnderm));
            }          
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="id">id e grupit</param>
        public clsGrupKomponente(int id)
        {
            using (clsDatabazeListPagesa data = new clsDatabazeListPagesa())
            {
                mbushGrupKomponente(data.merrGrupKomponenteSipasId(id));
            }
        }public clsGrupKomponente(int id,clsDatabazeListPagesa data)
        {
              mbushGrupKomponente(data.merrGrupKomponenteSipasId(id));
     
        }

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsGrupKomponente()
        {
        }

        public clsGrupKomponente(DataRow rreshti)
        {
            
            mbushGrupKomponente(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public int IdKrijuesi
        {
            get
            {
                return idKrijuesi;
            }
            set
            {
                idKrijuesi = value;
            }
        }
        public int IdRenditje
        {
            get
            {
                return idRenditje;
            }
            set
            {
                idRenditje = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos kodin e grupit te Punonjesit.
        /// </summary>
        public string Kodi
        {
            get { return kodi; }
            set { kodi = value; }
        }
        /// <summary>
        /// Kthen/Vendos pershkrimin e grupit te Punonjesit.
        /// </summary>
        public String Pershkrimi
        {
            get { return pershkrimi; }
            set { pershkrimi = value; }
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
            using (clsDatabazeListPagesa db = new clsDatabazeListPagesa())
            {
                if (db.ekzistonGrupKomponente(kodi, idNdermarje))
                    return new clsMesazh(false, gabimEkzistence);
                clsMesazh u_ruajt = db.ruajGrupKomponente(Id, Kodi, Pershkrimi, IdPerdoruesi, idKrijuesi, IdNdermarje, idStatusDok, IdRenditje);
                return u_ruajt;
            }
        }

        /// <summary>
        /// Modifikon objektin e grupit te Punonjesishs ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="clsDatabazeListPagesa.modifikoGrupPunonjesish"/> 
        /// </summary>
        public clsMesazh modifiko()
        {
            using (clsDatabazeListPagesa data = new clsDatabazeListPagesa())
            {
                clsMesazh u_modifikua = data.modifikoGrupKomponente(Id, Kodi, Pershkrimi, IdPerdoruesi, IdNdermarje, idStatusDok);
                return u_modifikua;
            }
        }

        /// <summary>
        /// Fshin objektin e grupit te Punonjesishs ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="clsDatabazeListPagesa.fshiGrupPunonjesish"/> 
        /// </summary>
        public clsMesazh fshi()
        {
            clsDatabazeListPagesa data = new clsDatabazeListPagesa();
            clsMesazh u_fshi = data.fshiGrupKomponenteStatus(id, idPerdoruesi);
            data.Dispose();
            return u_fshi;
        }

        /// <summary>
        /// kontrollon nese ka veprime ke kete grup
        /// </summary>
        /// <returns></returns>
        public bool kaKomponente()
        {
            clsDatabazeListPagesa data = new clsDatabazeListPagesa();
            bool kaPunonjes = data.kaKomponente(id);
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
            using (clsDatabazeListPagesa data = new clsDatabazeListPagesa())
            {
                bool ekziston = data.ekzistonGrupKomponente(kodi, idndermarje);
                return ekziston;
            }
        }
        #endregion

        #region Metoda Internal
        /// <summary>
        /// mbush grupin e punonjesve nga databaza
        /// </summary>
        /// <param name="dbDataRowGrup"></param>
        /// <returns></returns>
        internal bool mbushGrupKomponente(DataRow dbDataRowGrup)
        {
            if (dbDataRowGrup != null)
            {
                try
                {
                    int.TryParse(dbDataRowGrup["ID"].ToString(), out id);
                    kodi = dbDataRowGrup["KODI"].ToString();
                    pershkrimi = dbDataRowGrup["PERSHKRIMI"].ToString();
                    int.TryParse(dbDataRowGrup["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    int.TryParse(dbDataRowGrup["IDKRIJUESI"].ToString(), out idKrijuesi);
                    int.TryParse(dbDataRowGrup["IDNDERMARJE"].ToString(), out idNdermarje);
                    int.TryParse(dbDataRowGrup["IDSTATUSDOK"].ToString(), out idStatusDok);
                    DateTime.TryParse(dbDataRowGrup["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRowGrup["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    int.TryParse(dbDataRowGrup["IDRENDITJE"].ToString(), out idRenditje);
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

