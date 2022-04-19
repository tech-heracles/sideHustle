using DbCore.DbAdmin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;


namespace DbCore.DbListPagesat
{
    public class clsKodeProfesione
    {  ///<summary>
        /// mesazh gabimi per mos plotesimin e kodit
        /// </summary>
        private const string STR_PlotesoniKodinEBurimit = "Plotesoni kodin!";
        /// <summary>
        /// mesazh gabimi per mospletesimin e emertimit
        /// </summary>
        private const string STR_PlotesoniEmertiminEBurimit = "Plotesoni pershkrimin!";

        /// <summary>
        /// mesazh gabimi per ekzistencen e nje burimi me kete kod
        /// </summary>
        private const string STR_EkzistonNjeBurimMeKeteKodJuLutemShenoniNjeKodTje = "Ekziston nje kod profesioni me kete kod. Ju lutem shenoni nje kod tjeter!";

        /// <summary>
        /// mesazh kur kontrollet kalohen me sukses
        /// </summary>
        private const string STR_KontrolletEBurimitUKaluanMeSukses = "Kontrollet u kaluan me sukses";
        /// <summary>
        /// mesazh kur burimi u mbush me sukses
        /// </summary>
        public static string mbushjeSukses = "Kode profesioni u mbushen me sukses";
        /// <summary>
        /// mesazh gabimi kur merren te dhenat
        /// </summary>
        public static string gabimNeTeDhena = "ERROR: Gabim gjate marrjes  nga db-ja";
        /// <summary>
        /// mesazh gabimi kur nuk merret asnje e dhene
        /// </summary>
        public static string drbosh = "Mbushja nuk u krye sepse nuk u morr asgje nga db-ja";
        #region Atribute
        /// <summary>
        /// id e burimit
        /// </summary>
        private int id;
        /// <summary>
        /// kodi
        /// </summary>
        private string kodi;
        /// <summary>
        /// emertimi
        /// </summary>
        private string pershkrimi;
        /// <summary>
        /// id e llogarise
        /// </summary>
        private int idKrijuesi;
        /// <summary>
        /// aktiv
        /// </summary>
        private bool aktiv;
        /// <summary>
        /// id e perdoruesit
        /// </summary>
        private int idPerdoruesi;
        /// <summary>
        /// id e ndermarjes
        /// </summary>
        private int idNdermarje;

        /// <summary>
        /// id e statusit te burimit
        /// </summary>
        private int idStatusDok;
        /// <summary>
        /// data e krijimi te burimit
        /// </summary>
        private DateTime dtKrijimi;
        /// <summary>
        /// data e fundit e modifikimit
        /// </summary>
        private DateTime dtModifikimi;
        private DataRow rreshti;


        #endregion

        #region Konstruktoret
        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public clsKodeProfesione()
        {
        }

        /// <summary>
        /// konstruktori me parameter
        /// </summary>
        /// <param name="id">id e burimit</param>
        /// <param name="kodi"> kodi</param>
        /// <param name="pershkrimi"> pershkrimi</param>
        /// <param name="pershkrimiang">kostoja e planifikuar</param>
        /// <param name="idkrijuesi"> id e llogarise </param>
        /// <param name="aktiv">aktive apo inaktive</param>
        /// <param name="lloji"> tipi  makineri,mjet apo punonjes</param>
        /// <param name="idPerdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNdermarje">id e ndermarjes</param>
        /// <param name="idKonfig"> id e konfigurimit</param>
        /// <param name="idStatusDok">id e statusit te dokumentit</param>
        public clsKodeProfesione(int id, string kodi, string pershkrimi, int idkrijuesi, bool aktiv, int idPerdoruesi, int idNdermarje,  int idStatusDok)
        {
            this.id = id;
            this.kodi = kodi;
            this.pershkrimi = pershkrimi;
            idKrijuesi = idkrijuesi;
            this.aktiv = aktiv;
            this.idPerdoruesi = idPerdoruesi;
            this.idNdermarje = idNdermarje;
               this.idStatusDok = idStatusDok;
        }

        /// <summary>
        /// konstruktori me parameter
        /// </summary>
        /// <param name="id">id e burimit</param>
        /// <param name="kodi"> kodi</param>
        /// <param name="pershkrimi"> pershkrimi</param>
        /// <param name="pershkrimiang">kostoja e planifikuar</param>
        /// <param name="idkrijuesi"> id e llogarise </param>
        /// <param name="aktiv">aktive apo inaktive</param>
        /// <param name="lloji"> tipi  makineri,mjet apo punonjes</param>
        /// <param name="idPerdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNdermarje">id e ndermarjes</param>
        /// <param name="idKonfig"> id e konfigurimit</param>
        /// <param name="idStatusDok">id e statusit te dokumentit</param>
        /// <param name="nrLlogari">nr i llogarise</param>
        /// <param name="shtim"> tregon nese po shtojme apo po modifikojme nje burim</param>
        public clsKodeProfesione(int id, string kodi, string pershkrimi, int idkrijuesi, bool aktiv, int idPerdoruesi, int idNdermarje, int idStatusDok, bool shtim)
        {
            try
            {
                this.id = id;
                this.kodi = kodi;
                this.pershkrimi = pershkrimi;
                idKrijuesi = idkrijuesi;
                this.aktiv = aktiv;
                this.idPerdoruesi = idPerdoruesi;
                this.idNdermarje = idNdermarje;
               
                this.idStatusDok = idStatusDok;

                clsMesazh mesazh = kontrolloKodeProfesione(shtim);
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
        /// <param name="kodi">kodi i burimit</param>
        /// <param name="idNderm">id e ndermarrjes</param>
        public clsKodeProfesione(string kodi, int idNderm)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            if (!mbushKodeProfesione(db.ktheKodeProfesioneSipasKodit(kodi, idNderm)).Status)
                id = -1;
            db.Dispose();
        }
        public clsKodeProfesione(string kodi, int idNderm, clsDatabazeListPagesa db)
        {
            if (!mbushKodeProfesione(db.ktheKodeProfesioneSipasKodit(kodi, idNderm)).Status)
                id = -1;

        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="id">id e burimit</param>
        public clsKodeProfesione(int id)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            mbushKodeProfesione(db.ktheKodeProfesione(id));
            db.Dispose();
        }
        public clsKodeProfesione(int id, clsDatabazeListPagesa db)
        {
            mbushKodeProfesione(db.ktheKodeProfesione(id));

        }

        public clsKodeProfesione(DataRow rreshti)
        {
            
            mbushKodeProfesione(rreshti);
        }
        #endregion

        #region Properties
        /// <summary>
        /// id e burimit
        /// </summary>
        public int Id
        {
            get
            {
                return
                    id;
            }
            set
            {
                id = value;
            }
        }
        /// <summary>
        /// kodi i burimit
        /// </summary>
        public string Kodi
        {
            get
            {
                return kodi;
            }
            set
            {
                kodi = value;
            }
        }
        /// <summary>
        /// emertimi
        /// </summary>
        public string Pershkrimi
        {
            get
            {
                return pershkrimi;
            }
            set
            {
                pershkrimi = value;
            }
        }

        /// <summary>
        /// id e krijuesit
        /// </summary>
        public int IdKrijuesi
        {
            get { return idKrijuesi; }
            set { idKrijuesi = value; }
        }
        /// <summary>
        /// aktive ose inaktive
        /// </summary>
        public bool Aktiv
        {
            get
            {
                return aktiv;
            }
            set
            {
                aktiv = value;
            }
        }


        /// <summary>
        /// id e perdoruesit qe ka kryer veprimin
        /// </summary>
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
        /// <summary>
        /// id e ndermarjes 
        /// </summary>
        public int IdNdermarje
        {
            get
            {
                return idNdermarje;
            }
            set
            {
                idNdermarje = value;
            }
        }

        /// <summary>
        /// id e status te dok
        /// <example> 0 draft, 1-ruajtur,2 -fshire</example>
        /// </summary>
        public int IdStatusDok
        {
            get
            {
                return idStatusDok;
            }
            set
            {
                idStatusDok = value;
            }
        }
        /// <summary>
        /// data e krijimit te burimit
        /// </summary>
        public DateTime DtKrijimi
        {
            get
            {
                return dtKrijimi;
            }
        }
        /// <summary>
        /// data e modifikimi te fundit te burimit
        /// </summary>
        public DateTime DtModifikimi
        {
            get
            {
                return dtModifikimi;
            }
        }
        #endregion

        #region Metoda Publike

        /// <summary>
        /// kontroll nese objekti i burimit i ka te dhenat e sakta 
        /// </summary>
        /// <param name="shtim">tregon nese eshte shtim apo modifikim</param>
        /// <returns> clsMesazh  me statusin nese te dhenat jane te sakta apo jo</returns>
        private clsMesazh kontrolloKodeProfesione(bool shtim)
        {
            if (kodi == "")
                return new clsMesazh(false, STR_PlotesoniKodinEBurimit);
            if (pershkrimi == "")
                return new clsMesazh(false, STR_PlotesoniEmertiminEBurimit);


            if (shtim && ekzistonKodeProfesione(kodi, idNdermarje))
                return new clsMesazh(false, STR_EkzistonNjeBurimMeKeteKodJuLutemShenoniNjeKodTje);


            return new clsMesazh(true, STR_KontrolletEBurimitUKaluanMeSukses);
        }

        public clsMesazh ruaj()
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            db.beginTransaksion();
          

            clsMesazh mesazh = ruaj(db);
            if (!mesazh.Status)
            {
                db.rollbackTransaksion();
                return mesazh;
            }
            db.commitTransaksion();

           
            return mesazh;
        }

        /// <summary>
        /// ruan burimin
        /// </summary>
        /// <param name="db"> clsDatabazeListPagesa per te qene pjese e trasaksionit</param>
        /// <returns> cls mesazh qe tregon nqs eshte ruajtur apo jo burimi</returns>
        public clsMesazh ruaj(clsDatabazeListPagesa db)
        {
            clsMesazh mesazh = new clsMesazh();
            //if (db == null)
            //    db = new clsDatabazeListPagesa();
            int id = 0;
            mesazh = db.ruajKodeProfesione(out id, kodi, pershkrimi, idKrijuesi, aktiv, idPerdoruesi, idNdermarje, idStatusDok);
            this.id = id;
            return mesazh;
        }

        /// <summary>
        /// modifikon burimin
        /// </summary>
        /// <returns> cls mesazh qe tregon nqs eshte ruajtur apo jo burimi</returns>   
        public clsMesazh modifiko()
        {
            clsMesazh mesazh = new clsMesazh();
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            mesazh = db.modifikoKodeProfesione(id, kodi, pershkrimi, aktiv, idPerdoruesi, idNdermarje, idStatusDok);
            db.Dispose();
            return mesazh;
        }
        public clsMesazh modifiko(clsDatabazeListPagesa db)
        {
            clsMesazh mesazh = new clsMesazh();

            mesazh = db.modifikoKodeProfesione(id, kodi, pershkrimi, aktiv, idPerdoruesi, idNdermarje, idStatusDok);

            return mesazh;
        }

        public clsMesazh fshi()
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            clsMesazh mesazh = fshi(db);
            db.Dispose();
            return mesazh;
        }

        /// <summary>
        /// Fshin objektin burimin ne tabelen perkatese ne databaze
        /// </summary>
        /// <param name="db">db nqs ben pjese ne nje transaksion</param>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh fshi(clsDatabazeListPagesa db)
        {
            //if (db == null)
            //    db = new clsDatabazeListPagesa();
            clsMesazh u_fshi = db.fshiKodeProfesioneStatus(id, idPerdoruesi);
            return u_fshi;
        }

        /// <summary>
        /// Merr objektin burim nga tabela perkatese ne databaze.
        /// </summary>
        public void merr()
        {
            using (clsDatabazeListPagesa db = new clsDatabazeListPagesa())
            {
                db.ktheKodeProfesione(id);
            }
        }

        /// <summary>
        /// merr objektin burimin sipas kodi dhe ndermarjes
        /// </summary>
        /// <param name="kodi">kodi </param>
        /// <param name="idndermarje">ndermarja</param>
        public bool merrKodeProfesioneSipasKodit(string kodi, int idndermarje)
        {
            using (clsDatabazeListPagesa db = new clsDatabazeListPagesa())
            {
                bool sukses = mbushKodeProfesione(db.ktheKodeProfesioneSipasKodit(kodi, idndermarje)).Status;
                return sukses;
            }
        }
        public bool ktheKodeProfesioneSipasPershkrimi(string pershkrim, int idndermarje)
        {
            using (clsDatabazeListPagesa db = new clsDatabazeListPagesa())
            {
                bool sukses = mbushKodeProfesione(db.ktheKodeProfesioneSipasPershkrimi(pershkrim, idndermarje)).Status;
                return sukses;
            }
        }

        /// <summary>
        /// Merr datatable burim  te nje ndermarje nga tabela perkatese ne databaze.Therret funksionin
        /// </summary>
        /// <returns > nje datatable me te gjithe burimet te kesaj ndermarje</returns>
        public DataTable merriTeGjithe()
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            DataTable dt = db.ktheGjitheKodeProfesioneSipasNdermarjes(IdNdermarje);
            db.Dispose();
            return dt;
        }

        /// <summary>
        /// kontrollon nese ekziston burimi me kete kod ne kete ndermarje 
        /// </summary>
        /// <param name="kodi">kodi</param>
        /// <param name="idndermarje">idndermarje</param>
        /// <returns> true ose false</returns>
        public static bool ekzistonKodeProfesione(string kodi, int idndermarje)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            bool ekziston = db.ekzistonKodeProfesione(kodi, idndermarje);
            db.Dispose();
            return ekziston;
        }
        public static bool kaVeprimeKodeProfesione(int id)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            return db.kaVeprimeKodeProfesione(id);
        }
        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush burimet me te dhenat nga databaza
        /// </summary>
        /// <param name="dbDataRow">rreshti me te dhena</param>
        /// <returns> kthen ne se mbushja u be ne rregull apo jo</returns>
        internal clsMesazh mbushKodeProfesione(DataRow dbDataRow)
        {
            if (dbDataRow != null)
            {
                try
                {
                    int.TryParse(dbDataRow["ID"].ToString(), out id);
                    kodi = dbDataRow["KODI"].ToString();
                    pershkrimi = dbDataRow["PERSHKRIMI"].ToString();
                    Boolean.TryParse(dbDataRow["AKTIV"].ToString(), out aktiv);
                    int.TryParse(dbDataRow["IDKRIJUESI"].ToString(), out idKrijuesi);

                    int.TryParse(dbDataRow["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    int.TryParse(dbDataRow["IDNDERMARJE"].ToString(), out idNdermarje);
                    int.TryParse(dbDataRow["IDSTATUSDOK"].ToString(), out idStatusDok);
                    DateTime.TryParse(dbDataRow["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRow["DTMODIFIKIMI"].ToString(), out dtModifikimi);

                    return new clsMesazh(true, clsKodeProfesione.mbushjeSukses);
                }
                catch (InvalidCastException)
                {
                    throw new Exception(clsKodeProfesione.gabimNeTeDhena);
                }
            }
            else
                return new clsMesazh(false, clsKodeProfesione.drbosh);
        }

        #endregion
    }
}


