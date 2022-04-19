using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbQendraKosto
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne  qendrat e kostos
    ///  (Te dhenat  merren nga tabela : T_OBJEKTIVAKOSTO)
    /// </summary>
    public class clsObjektivaKosto
    {
           private const string gabimNeTeDhena = "ERROR: Gabim gjate marrjes se objektivave te kostos nga db-ja";
        private const string gabimEkzistimi = "Ekziston nje objektiv kostoje me kete kod. Ju lutem shenoni nje tjeter!";
        private const string STR_PlotesoniDatenEFillimit = "Plotesoni daten e fillimit!";
        private const string STR_DataEMbarimitNukDuhetTeJeteMeEVogelSeDataEFillim = "Data e mbarimit nuk duhet te jete me e vogel se data e fillimit!";
      /// <summary>
        /// mesazh gabimi per mos plotesimin e kodit
        /// </summary>
        private const string STR_PlotesoniKodin = "Plotesoni kodin e objektives se kostos!";
        /// <summary>
        /// mesazh gabimi per mospletesimin e emertimit
        /// </summary>
        private const string STR_PlotesoniEmertimin = "Plotesoni pershkrimin e objektives se kostos!";
        /// <summary>
        /// mesazh kur kontrollet kalohen me sukses
        /// </summary>
        private const string STR_KontrolletUKaluanMeSukses = "Kontrollet e objektives se kostos u kaluan me sukses";
        #region Atribute

        private int id;
        private string kodi;
        private string pershkrimi;
        private DateTime nga;
        private bool aktiv;
        private int idKonfig;
        private DateTime deri;
        private int idNdermarje;
        private int idPerdoruesi;
        private int idStatusDok;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private DataRow rreshti;

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases me parametra
        /// </summary>
        /// <param name="nga"> kohezgjatja nga</param>
        /// <param name="deri">kohezgjatja deri</param>
        /// <param name="aktiv">aktiv</param>
        /// <param name="id"> id ritese </param>
        /// <param name="idPerd">id e perdoruesit qe e ka ruajtur</param>
        /// <param name="kod">kodi i struktures administrative</param>
        /// <param name="nderm">id e ndermarjes</param>
        /// <param name="idKonfig"> shenime </param>
        /// <param name="pershkrimi"> emri i struktures</param>
        public clsObjektivaKosto(int id, string kod, string pershkrimi, DateTime nga, bool aktiv, int idKonfig, DateTime deri, int nderm, int idPerd, int idstatusdok)
        {
            this.id = id;
            kodi = kod;
            this.pershkrimi = pershkrimi;
            this.nga = nga;
            this.aktiv = aktiv;
            this.idKonfig = idKonfig;
            this.deri = deri;
            idNdermarje = nderm;
            idPerdoruesi = idPerd;
            idStatusDok = idstatusdok;
        }

        public clsObjektivaKosto(int id, string kod, string pershkrimi, DateTime nga, bool aktiv, int idKonfig, DateTime deri, int nderm, int idPerd, int idstatusdok, bool isshtim)
        {
            try
            {
            this.id = id;
            kodi = kod;
            this.pershkrimi = pershkrimi;
            this.nga = nga;
            this.aktiv = aktiv;
            this.idKonfig = idKonfig;
          this.  deri = deri;
            idNdermarje = nderm;
            idPerdoruesi = idPerd;
            idStatusDok = idstatusdok;

            clsMesazh mesazh = kontrolloOK(isshtim);
                if (!mesazh.Status)
                    throw new Exception(mesazh.PershkrimMesazhi);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// konstruktor me 2 parametra
        /// </summary>
        /// <param name="kodi">kodi i struktures administrative</param>
        /// <param name="idNderm">id e ndermarrjes</param>
        public clsObjektivaKosto(string kodi, int idNderm)
        {
            using (clsDatabaseQendraKosto db = new clsDatabaseQendraKosto())
            {
                if (!mbushObjektivKosto(db.ktheObjektivKostoSipasKodit(kodi, idNderm)))
                    id = -1;
            }
        }
        public clsObjektivaKosto(string kodi, int idNderm, clsDatabaseQendraKosto db)
        {
            if (!mbushObjektivKosto(db.ktheObjektivKostoSipasKodit(kodi, idNderm)))
                id = -1;
        }
        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="id">id e qendres</param>
        public clsObjektivaKosto(int id):this (id,new clsDatabaseQendraKosto())
        {
          
        } 
        public clsObjektivaKosto(int id,clsDatabaseQendraKosto db)
        {
            if (id>0)
            mbushObjektivKosto(db.TransCache.getObjektiva(id,db));
        }

        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsObjektivaKosto()
        {
        }

        public clsObjektivaKosto(DataRow rreshti)
        {
            
            mbushObjektivKosto(rreshti);
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

        /// <summary>
        /// Kthen/Vendos id e konfigurimit.
        /// </summary>
        public int IdKonfig
        {
            get { return idKonfig; }
            set { idKonfig = value; }
        }

        /// <summary>
        /// Kthen/Vendos kodi .
        /// </summary>
        public string Kodi
        {
            get { return kodi; }
            set { kodi = value; }
        }

        /// <summary>
        /// Kthen/Vendos pershkrimi.
        /// </summary>
        public string Pershkrimi
        {
            get { return pershkrimi; }
            set { pershkrimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos kohezgjatjen deri
        /// </summary>
        public DateTime Deri
        {
            get { return deri; }
            set { deri = value; }
        }

        /// <summary>
        /// Kthen/Vendos aktiv
        /// </summary>
        public bool Aktiv
        {
            get { return aktiv; }
            set { aktiv = value; }
        }

        /// <summary>
        /// Kthen/Vendos  kohezgjatjen nga
        /// </summary>
        public DateTime Nga
        {
            get { return nga; }
            set { nga = value; }
        }
     
        /// <summary>
        /// Kthen/Vendos ID-ne e ndermarjes
        /// </summary>
        public int IdNdermarje
        {
            get { return idNdermarje; }
            set { idNdermarje = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e perdoruesit qe e ka kryer veprimin.
        /// </summary>
        public int IdPerdoruesi
        {
            get { return idPerdoruesi; }
            set { idPerdoruesi = value; }
        }

        /// <summary>
        /// kthen/vendos id e statusit te dokumentit
        /// <example>0- draft, 1-ruajtur, 2-fshire</example>
        /// </summary>
        public int IdStatusDok
        {
            get { return idStatusDok; }
            set { idStatusDok = value; }
        }

        /// <summary>
        /// kthen daten e krijimit te kesaj strukture
        /// </summary>
        public DateTime DtKrijimi
        {
            get { return dtKrijimi; }

        }

        /// <summary>
        /// kthen daten e modifikimit te fundit te kesaj stukture
        /// </summary>
        public DateTime DtModifikimi
        {
            get { return dtModifikimi; }

        }
        #endregion

        #region Metoda Publike

        /// <summary>
        /// kontroll nese objekti i objektiva e kostos i ka te dhenat e sakta 
        /// </summary>
        /// <param name="shtim">tregon nese eshte shtim apo modifikim</param>
        /// <returns> clsMesazh  me statusin nese te dhenat jane te sakta apo jo</returns>
        private clsMesazh kontrolloOK(bool shtim)
        {
            if (kodi == "")
                return new clsMesazh(false, STR_PlotesoniKodin);
            if (pershkrimi == "")
                return new clsMesazh(false, STR_PlotesoniEmertimin);
            if (nga == new DateTime())
                return new clsMesazh(false, STR_PlotesoniDatenEFillimit);
            if (shtim && ekzistonOK(kodi, idNdermarje))
                return new clsMesazh(false, gabimEkzistimi);

           if(deri !=new  DateTime())
               if(deri<nga)
                   return new clsMesazh(false, STR_DataEMbarimitNukDuhetTeJeteMeEVogelSeDataEFillim);
            return new clsMesazh(true, STR_KontrolletUKaluanMeSukses);
        }
        
        /// <summary>
        /// ruan objektiven e kostos
        /// </summary>
        /// <returns> cls mesazh qe tregon nqs eshte ruajtur apo jo objektiva</returns>
        public clsMesazh ruajObjektivKosto()
        {
            clsMesazh mesazh = new clsMesazh();
            using (clsDatabaseQendraKosto db = new clsDatabaseQendraKosto())
            {
                int idq = 0;
                if (db.ekzistonObjektivKostoje(kodi, idNdermarje))
                    return new clsMesazh(false, gabimEkzistimi);
                mesazh = db.ruajObjektivKosto(out idq, kodi, pershkrimi, nga, deri, aktiv, idKonfig, idPerdoruesi, idNdermarje, idStatusDok);
                id = idq;
                return mesazh;
            }
        }
        /// <summary>
        /// kontrollon nese ekziston objektiv me kete kod ne kete ndermarje 
        /// </summary>
        /// <param name="kodi">kodi</param>
        /// <param name="idndermarje">idndermarje</param>
        /// <returns> true ose false</returns>
        public static bool ekzistonOK(string kodi, int idndermarje)
        {
            using (clsDatabaseQendraKosto db = new clsDatabaseQendraKosto())
            {
                return db.ekzistonObjektivKostoje(kodi, idndermarje);
            }
        } 
        public static bool ekzistonOK(string kodi, int idndermarje,clsDatabaseQendraKosto db)
        {
            return db.ekzistonObjektivKostoje(kodi, idndermarje);
        }
        /// <summary>
        /// modifikon objektiv
        /// </summary>
        /// <returns> cls mesazh qe tregon nqs eshte ruajtur apo jo objektiv</returns>   
        public clsMesazh modifikoObjektivKosto()
        {
            clsMesazh mesazh = new clsMesazh();
            using (clsDatabaseQendraKosto db = new clsDatabaseQendraKosto())
            {
                db.beginTransaksion();
                mesazh = db.modifikoObjektivKosto(id, kodi, pershkrimi, nga, deri, aktiv, idKonfig, idPerdoruesi, idNdermarje, idStatusDok);
                if (!mesazh.Status)
                {
                    db.rollbackTransaksion();
                    return mesazh;
                }
                db.commitTransaksion();
            }
            return mesazh;
        }

        /// <summary>
        /// Fshin objektin objektiva ne tabelen perkatese ne databaze.Therret funksionin
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh fshi()
        {
            using (clsDatabaseQendraKosto db = new clsDatabaseQendraKosto())
            {
                return db.fshiObjektivKostoStatus(id, idPerdoruesi);
            }
        }
       
        /// <summary>
        /// Merr datatable objektivat  te nje ndermarjenga tabela perkatese ne databaze.Therret funksionin
        /// </summary>
        /// <returns > nje datatable me te gjithe objektivat te kesaj ndermarje</returns>
        public DataTable merriTeGjithe()
        {
            using (clsDatabaseQendraKosto db = new clsDatabaseQendraKosto())
            {
                return db.merrObjektivKostoDT(IdNdermarje);
            }          
        }
       
        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush qendrat nga databaza
        /// </summary>
        /// <param name="db">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushObjektivKosto(DataRow db)
        {
            if (db != null)
            {
                try
                {
                    int.TryParse(db["ID"].ToString(), out id);
                    kodi = db["KODI"].ToString();
                    pershkrimi = db["PERSHKRIMI"].ToString(); 
                   
                   DateTime.TryParse( db["NGA"].ToString() , out nga);
                    int.TryParse(db["IDKONFIG"].ToString(), out idKonfig);
                    bool.TryParse(db["AKTIV"].ToString(), out aktiv);
                    DateTime.TryParse(db["DERI"].ToString(), out deri);
                    int.TryParse(db["IDNDERMARJE"].ToString(), out idNdermarje);
                    int.TryParse(db["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    int.TryParse(db["IDSTATUSDOK"].ToString(), out idStatusDok);
                    DateTime.TryParse(db["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(db["DTMODIFIKIMI"].ToString(), out dtModifikimi);

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
        internal void mbushObjektivKosto(clsObjektivaKosto ob)
        {
            this.id = ob.id;
            this.kodi = ob.kodi;
            this.pershkrimi = ob.pershkrimi;
            this.nga = ob.nga;
            this.IdKonfig = ob.IdKonfig;
            this.aktiv = ob.aktiv;
            this.deri = ob.deri;
            this.IdNdermarje = ob.IdNdermarje;
            this.idPerdoruesi = ob.idPerdoruesi;
            this.idStatusDok = ob.idStatusDok;
            this.dtKrijimi = ob.dtKrijimi;
            this.dtModifikimi = ob.DtModifikimi;
            
      
        }

        #endregion
    }
}
