using DbCore.DbAdmin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using EO.Web.Internal;

namespace DbCore.DbListPagesat
{
    public class clsProfesioneTitujPune
    {
        ///<summary>
        /// mesazh gabimi per mos plotesimin e kodit
        /// </summary>
        private const string STR_PlotesoniKodinEBurimit = "Plotesoni kodin!";
        /// <summary>
        /// mesazh gabimi per mospletesimin e emertimit
        /// </summary>
        private const string STR_PlotesoniEmertiminEBurimit = "Plotesoni pershkrimin!";
        /// <summary>
        /// mesazh gabimi per mospletesimin e emertimit anglisht
        /// </summary>
        private const string STR_PlotesoniEmertiminEBurimitAng = "Plotesoni pershkrimin anglisht!";

        /// <summary>
        /// mesazh gabimi per ekzistencen e nje burimi me kete kod
        /// </summary>
        private const string STR_EkzistonNjeBurimMeKeteKodJuLutemShenoniNjeKodTje = "Ekziston nje profesion me kete kod. Ju lutem shenoni nje kod tjeter!";
        private const string STR_EkzistonNjeBurimMeKeteKodJuLutemShenoniNjeKodTje1 = "Ekziston nje profesion me kete pershkrim. Ju lutem shenoni nje pershkrim tjeter!";
        // / <summary>
        /// mesazh kur kontrollet kalohen me sukses
        /// </summary>
        private const string STR_KontrolletEBurimitUKaluanMeSukses = "Kontrollet u kaluan me sukses";
        /// <summary>
        /// mesazh kur burimi u mbush me sukses
        /// </summary>
        public static string mbushjeSukses = "Profesioni u mbush me sukses";
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
        /// pershkrimi anglisht
        /// </summary>
        private string pershkrimiAng;
        /// <summary>
        /// id e llogarise
        /// </summary>
        private int idKrijuesi;
        /// <summary>
        /// aktiv
        /// </summary>
        private bool aktiv;
        /// <summary>
        /// tipi 
        /// <example> 1-profesion,2-titull pune</example>
        /// </summary>
        private int lloji;

        /// <summary>
        /// id e perdoruesit
        /// </summary>
        private int idPerdoruesi;
        /// <summary>
        /// id e ndermarjes
        /// </summary>
        private int idNdermarje;
        /// <summary>
        /// id e konfigurimit
        /// </summary>
        private int idKonfig;
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
        public clsProfesioneTitujPune()
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
        public clsProfesioneTitujPune(int id, string kodi, string pershkrimi, string pershkrimiang, int idkrijuesi, bool aktiv, int lloji, int idPerdoruesi, int idNdermarje, int idKonfig, int idStatusDok)
        {
            this.id = id;
            this.kodi = kodi;
            this.pershkrimi = pershkrimi;
            this.pershkrimiAng = pershkrimiang;
            idKrijuesi = idkrijuesi;
            this.aktiv = aktiv;
            this.lloji = lloji;
            this.idPerdoruesi = idPerdoruesi;
            this.idNdermarje = idNdermarje;
            this.idKonfig = idKonfig;
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
        public clsProfesioneTitujPune(int id, string kodi, string pershkrimi, string pershkrimiang, int idkrijuesi, bool aktiv, int lloji, int idPerdoruesi, int idNdermarje, int idKonfig, int idStatusDok, bool shtim)
        {
            try
            {
                this.id = id;
                this.kodi = kodi;
                this.pershkrimi = pershkrimi;
                this.pershkrimiAng = pershkrimiang;
                idKrijuesi = idkrijuesi;
                this.aktiv = aktiv;
                this.lloji = lloji;
                this.idPerdoruesi = idPerdoruesi;
                this.idNdermarje = idNdermarje;
                this.idKonfig = idKonfig;
                this.idStatusDok = idStatusDok;

                clsMesazh mesazh = kontrolloProfesioneTitujPune(shtim);
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
        /// <param name="perhskrimi">kodi i burimit</param>
        /// <param name="idNderm">id e ndermarrjes</param>
        public clsProfesioneTitujPune(string pershkrimi, int idNderm, int lloji, int idgjuha)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            if (!mbushProfesioneTitujPune(db.ktheProfesioneTitujPuneSipasPershkrimit(pershkrimi, idNderm, lloji, idgjuha)).Status)
                id = -1;
            db.Dispose();
        }
        public clsProfesioneTitujPune(string kodi, int idNderm, int lloji, clsDatabazeListPagesa db)
        {
            if (!mbushProfesioneTitujPune(db.ktheProfesioneTitujPuneSipasKodit(kodi, idNderm, lloji)).Status)
                id = -1;

        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="id">id e burimit</param>
        public clsProfesioneTitujPune(int id)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            mbushProfesioneTitujPune(db.ktheProfesioneTitujPune(id));
            db.Dispose();
        }
        public clsProfesioneTitujPune(int id, clsDatabazeListPagesa db)
        {
            mbushProfesioneTitujPune(db.ktheProfesioneTitujPune(id));

        }

        public clsProfesioneTitujPune(DataRow rreshti)
        {
            
            mbushProfesioneTitujPune(rreshti);
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
        /// pershkrimi ang
        /// </summary>
        public string PershkrimiAng
        {
            get
            {
                return pershkrimiAng;
            }
            set
            {
                pershkrimiAng = value;
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
        /// tipi i burimit
        /// <example> 1-makineri,2-mjet,3 -punonjes</example>
        /// </summary>
        /// <seealso cref="cs"/>
        public int Lloji
        {
            get
            {
                return lloji;
            }
            set
            {
                lloji = value;
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
        /// id e konfigurimit te dokumentit
        /// </summary>
        public int IdKonfig
        {
            get
            {
                return idKonfig;
            }
            set
            {
                idKonfig = value;
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
        private clsMesazh kontrolloProfesioneTitujPune(bool shtim)
        {
            if (kodi == "")
                return new clsMesazh(false, STR_PlotesoniKodinEBurimit);
            if (pershkrimi == "")
                return new clsMesazh(false, STR_PlotesoniEmertiminEBurimit);
            if (pershkrimiAng == "")
                return new clsMesazh(false, STR_PlotesoniEmertiminEBurimitAng);
            if (shtim && ekzistonProfesionTitujPune(kodi, idNdermarje, lloji))
                return new clsMesazh(false, STR_EkzistonNjeBurimMeKeteKodJuLutemShenoniNjeKodTje);
            if (shtim && ekzistonProfesionTitujPunePershkrimiAng(pershkrimiAng, idNdermarje, lloji, id))
                return new clsMesazh(false, STR_EkzistonNjeBurimMeKeteKodJuLutemShenoniNjeKodTje1);


            return new clsMesazh(true, STR_KontrolletEBurimitUKaluanMeSukses);
        }

        public clsMesazh ruaj(IDictionary<string, object> hfNrAutoKF)
        {
            bool kaNdryshimNumri;

            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            db.beginTransaksion();
            clsMesazh mesazhkont = kontrolloArtikull(out kaNdryshimNumri, db, hfNrAutoKF, false);
            if (!mesazhkont.Status)
            {
                db.rollbackTransaksion();
                return mesazhkont;
            }

            clsMesazh mesazh = ruaj(db);
            if (!mesazh.Status)
            {
                db.rollbackTransaksion();
                return mesazh;
            }
            db.commitTransaksion();

            if (kaNdryshimNumri)
                return mesazhkont;
            return mesazh;
        }
        private clsMesazh kontrolloArtikull(out bool kaNdryshimNrAuto, clsDatabazeListPagesa db, IDictionary<string, object> hfNrAutoKF, bool modifikim)
        {
            kaNdryshimNrAuto = false;
            if (kodi == "")
                return new clsMesazh(false, "Kodi nuk mund te jete bosh");
            if (!modifikim)
            {
                clsMesazh mes = new clsMesazh();
                if (hfNrAutoKF != null)
                {
                    mes = kontrolloNrAutoArt(out kaNdryshimNrAuto, db, hfNrAutoKF);
                    if (!mes.Status)
                        return mes;
                }
                if (db.ekzistonProfesioneTitujPune(kodi, idNdermarje, lloji))
                    return new clsMesazh(false, STR_EkzistonNjeBurimMeKeteKodJuLutemShenoniNjeKodTje);
                return new clsMesazh(mes.Status, mes.PershkrimMesazhi);
            }
            return new clsMesazh(true, "Kontrollet u kaluan me sukses");
        }
        private clsMesazh kontrolloNrAutoArt(out bool kaNdryshimNumri, clsDatabazeListPagesa db, IDictionary<string, object> hfNrAutoKf)
        {
            clsDatabaseAdmin dbadm = new clsDatabaseAdmin(db );

            List<NrAuto> list = clsNrAutom.kontrollogjithenumrat(dbadm, hfNrAutoKf, DateTime.Today);
            if (NrAuto.ktheVlerenEre(list, "Kodi") != "")
                this.Kodi = NrAuto.ktheVlerenEre(list, "Kodi");
            DbCore.clsMesazh mes = NrAuto.ruajvlera(out kaNdryshimNumri, list, DateTime.Today, this.idPerdoruesi, this.idNdermarje, dbadm);
            return new clsMesazh(mes.Status, mes.PershkrimMesazhi);

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
            mesazh = db.ruajProfesioneTitujPune(out id, kodi, pershkrimi, lloji, pershkrimiAng, idKrijuesi, aktiv, idKonfig, idPerdoruesi, idNdermarje, idStatusDok);
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
            mesazh = db.modifikoProfesioneTitujPune(id, kodi, pershkrimi, lloji, pershkrimiAng, aktiv, idKonfig, idPerdoruesi, idNdermarje, idStatusDok);
            db.Dispose();
            return mesazh;
        }
        public clsMesazh modifiko(clsDatabazeListPagesa db)
        {
            clsMesazh mesazh = new clsMesazh();

            mesazh = db.modifikoProfesioneTitujPune(id, kodi, pershkrimi, lloji, pershkrimiAng, aktiv, idKonfig, idPerdoruesi, idNdermarje, idStatusDok);

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
            clsMesazh u_fshi = db.fshiProfesioneTitujPuneStatus(id, idPerdoruesi);
            return u_fshi;
        }

        /// <summary>
        /// Merr objektin burim nga tabela perkatese ne databaze.
        /// </summary>
        public void merr()
        {
            using (clsDatabazeListPagesa db = new clsDatabazeListPagesa())
            {
                db.ktheProfesioneTitujPune(id);
            }
        }

        /// <summary>
        /// merr objektin burimin sipas kodi dhe ndermarjes
        /// </summary>
        /// <param name="kodi">kodi </param>
        /// <param name="idndermarje">ndermarja</param>
        public bool merrBurimSipasKodit(string kodi, int idndermarje, int lloji)
        {
            using (clsDatabazeListPagesa db = new clsDatabazeListPagesa())
            {
                bool sukses = mbushProfesioneTitujPune(db.ktheProfesioneTitujPuneSipasKodit(kodi, idndermarje, lloji)).Status;
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
            DataTable dt = db.ktheGjitheProfesioneTitujPunetSipasNdermarjesDheLlojit(IdNdermarje, lloji);
            db.Dispose();
            return dt;
        }

        /// <summary>
        /// kontrollon nese ekziston burimi me kete kod ne kete ndermarje 
        /// </summary>
        /// <param name="pershkrimi">kodi</param>
        /// <param name="idndermarje">idndermarje</param>
        /// <returns> true ose false</returns>
        public static bool ekzistonProfesionTitujPune(string kodi, int idndermarje, int lloji)
        {
            using (clsDatabazeListPagesa db = new clsDatabazeListPagesa())
                return db.ekzistonProfesioneTitujPune(kodi, idndermarje, lloji);
            
        }
        public static bool ekzistonProfesionTitujPunePershkrimi(string pershkrimi, int idndermarje, int lloji)
        {
            return ekzistonProfesionTitujPunePershkrimi(pershkrimi, idndermarje, lloji, 0);
        }
        public static bool ekzistonProfesionTitujPunePershkrimi(string pershkrimi, int idndermarje, int lloji, int id)
        {
            using (clsDatabazeListPagesa db = new clsDatabazeListPagesa())
                return db.ekzistonProfesioneTitujPunePershkrimi(pershkrimi, idndermarje, lloji, id);
            
        }
        public static bool ekzistonProfesionTitujPunePershkrimiAng(string pershkrimi, int idndermarje, int lloji, int id)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            bool ekziston = db.ekzistonProfesioneTitujPunePershkrimiAng(pershkrimi, idndermarje, lloji, id);
            db.Dispose();
            return ekziston;
        }
        public static bool kaVeprimi(int id)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            return db.kaVeprimeProfesione(id);
        }
        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush burimet me te dhenat nga databaza
        /// </summary>
        /// <param name="dbDataRow">rreshti me te dhena</param>
        /// <returns> kthen ne se mbushja u be ne rregull apo jo</returns>
        internal clsMesazh mbushProfesioneTitujPune(DataRow dbDataRow)
        {
            if (dbDataRow != null)
            {
                try
                {
                    int.TryParse(dbDataRow["ID"].ToString(), out id);
                    kodi = dbDataRow["KODI"].ToString();
                    pershkrimi = dbDataRow["PERSHKRIMI"].ToString();
                    pershkrimiAng = dbDataRow["PERSHKRIMIANG"].ToString();
                    Boolean.TryParse(dbDataRow["AKTIV"].ToString(), out aktiv);
                    int.TryParse(dbDataRow["IDKRIJUESI"].ToString(), out idKrijuesi);

                    int.TryParse(dbDataRow["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    int.TryParse(dbDataRow["LLOJI"].ToString(), out lloji);
                    int.TryParse(dbDataRow["IDNDERMARJE"].ToString(), out idNdermarje);
                    int.TryParse(dbDataRow["IDKONFIG"].ToString(), out idKonfig);
                    int.TryParse(dbDataRow["IDSTATUSDOK"].ToString(), out idStatusDok);
                    DateTime.TryParse(dbDataRow["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRow["DTMODIFIKIMI"].ToString(), out dtModifikimi);

                    return new clsMesazh(true, clsProfesioneTitujPune.mbushjeSukses);
                }
                catch (InvalidCastException)
                {
                    throw new Exception(clsProfesioneTitujPune.gabimNeTeDhena);
                }
            }
            else
                return new clsMesazh(false, clsProfesioneTitujPune.drbosh);
        }

        #endregion
    }
}
