using DbCore.DbAdmin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using EO.Web.Internal;

namespace DbCore.DbListPagesat
{
    public class clsGrupimeLocaleGlobale
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
        /// mesazh gabimi per ekzistencen e nje burimi me kete kod
        /// </summary>
        private const string STR_EkzistonNjeBurimMeKeteKodJuLutemShenoniNjeKodTje = "Ekziston nje grupim me kete kod. Ju lutem shenoni nje kod tjeter!";

        /// <summary>
        /// mesazh kur kontrollet kalohen me sukses
        /// </summary>
        private const string STR_KontrolletEBurimitUKaluanMeSukses = "Kontrollet u kaluan me sukses";
        /// <summary>
        /// mesazh kur burimi u mbush me sukses
        /// </summary>
        public static string mbushjeSukses = "Grupimi u mbush me sukses";
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
        /// tipi 
        /// <example> 1-locale,2-globale</example>
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
        private int idPrindi;
        private DataRow rreshti;


        #endregion

        #region Konstruktoret
        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public clsGrupimeLocaleGlobale()
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
        public clsGrupimeLocaleGlobale(int id, string kodi, string pershkrimi, int idkrijuesi, bool aktiv, int lloji, int idPerdoruesi, int idNdermarje, int idKonfig, int idStatusDok, int idprindi)
        {
            this.id = id;
            this.kodi = kodi;
            this.pershkrimi = pershkrimi;
            idKrijuesi = idkrijuesi;
            this.aktiv = aktiv;
            this.lloji = lloji;
            this.idPerdoruesi = idPerdoruesi;
            this.idNdermarje = idNdermarje;
            this.idKonfig = idKonfig;
            this.idStatusDok = idStatusDok;
            this.idPrindi = idprindi;
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
        public clsGrupimeLocaleGlobale(int id, string kodi, string pershkrimi, int idkrijuesi, bool aktiv, int lloji, int idPerdoruesi, int idNdermarje, int idKonfig, int idStatusDok, bool shtim,int idprindi)
        {
            try
            {
                this.id = id;
                this.kodi = kodi;
                this.pershkrimi = pershkrimi;
                idKrijuesi = idkrijuesi;
                this.aktiv = aktiv;
                this.lloji = lloji;
                this.idPerdoruesi = idPerdoruesi;
                this.idNdermarje = idNdermarje;
                this.idKonfig = idKonfig;
                this.idStatusDok = idStatusDok;
                this.idPrindi = idprindi;

                clsMesazh mesazh = kontrolloGrupime(shtim);
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
        public clsGrupimeLocaleGlobale(string kodi, int idNderm)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            if (!mbushGrupime(db.ktheGrupimeLocaleGlobaleSipasKodit(kodi, idNderm, 1)).Status)
                id = -1;
            db.Dispose();
        }
        public clsGrupimeLocaleGlobale(string kodi, int idNderm,  clsDatabazeListPagesa db)
        {
            if (!mbushGrupime(db.ktheGrupimeLocaleGlobaleSipasKodit(kodi, idNderm, 1)).Status)
                id = -1;

        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="id">id e burimit</param>
        public clsGrupimeLocaleGlobale(int id)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            mbushGrupime(db.ktheGrupimeLocaleGlobale(id));
            db.Dispose();
        }
        public clsGrupimeLocaleGlobale(int id, clsDatabazeListPagesa db)
        {
            mbushGrupime(db.ktheGrupimeLocaleGlobale(id));

        }

        public clsGrupimeLocaleGlobale(DataRow rreshti)
        {
            
            mbushGrupime(rreshti);
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
        public int IdPrindi
        {
            get
            {
                return idPrindi;
            }
            set
            {
                idPrindi = value;
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
        /// tipi i burimit
        /// <example> 1-locale,2-globale</example>
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
        private clsMesazh kontrolloGrupime(bool shtim)
        {
            if (kodi == "")
                return new clsMesazh(false, STR_PlotesoniKodinEBurimit);
            if (pershkrimi == "")
                return new clsMesazh(false, STR_PlotesoniEmertiminEBurimit);
            if (shtim && ekzistonGrupimeLocaleGlobale(kodi, idNdermarje))
                return new clsMesazh(false, STR_EkzistonNjeBurimMeKeteKodJuLutemShenoniNjeKodTje);


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
                if (db.ekzistonGrupimeLocaleGlobale(kodi, idNdermarje, 1))
                    return new clsMesazh(false, STR_EkzistonNjeBurimMeKeteKodJuLutemShenoniNjeKodTje);
                return new clsMesazh(mes.Status, mes.PershkrimMesazhi);
            }
            return new clsMesazh(true, "Kontrollet u kaluan me sukses");
        }
        private clsMesazh kontrolloNrAutoArt(out bool kaNdryshimNumri, clsDatabazeListPagesa db, IDictionary<string, object> hfNrAutoKf)
        {
            clsDatabaseAdmin dbadm = new clsDatabaseAdmin(db );

            List<NrAuto> list = clsNrAutom.kontrollogjithenumrat(dbadm, hfNrAutoKf, DateTime.Today);
            if (NrAuto.ktheVlerenEre(list, "txtKodi") != "")
                this.Kodi = NrAuto.ktheVlerenEre(list, "txtKodi");
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
            mesazh = db.ruajGrupimeLocaleGlobale(out id, kodi, pershkrimi, lloji, idKrijuesi, aktiv, idKonfig, idPerdoruesi, idNdermarje, idStatusDok, idPrindi);
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
            mesazh = db.modifikoGrupimeLocaleGlobale(id, kodi, pershkrimi, lloji, aktiv, idKonfig, idPerdoruesi, idNdermarje, idStatusDok,idPrindi);
            db.Dispose();
            return mesazh;
        }
        public clsMesazh modifiko(clsDatabazeListPagesa db)
        {
            clsMesazh mesazh = new clsMesazh();

            mesazh = db.modifikoGrupimeLocaleGlobale(id, kodi, pershkrimi, lloji, aktiv, idKonfig, idPerdoruesi, idNdermarje, idStatusDok,idPrindi);

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
            clsMesazh u_fshi = db.fshiGrupimeLocaleGlobaleStatus(id, idPerdoruesi);
            return u_fshi;
        }

        /// <summary>
        /// Merr objektin burim nga tabela perkatese ne databaze.
        /// </summary>
        public void merr()
        {
            using (clsDatabazeListPagesa db = new clsDatabazeListPagesa())
            {
                db.ktheGrupimeLocaleGlobale(id);
            }
        }

        /// <summary>
        /// merr objektin burimin sipas kodi dhe ndermarjes
        /// </summary>
        /// <param name="kodi">kodi </param>
        /// <param name="idndermarje">ndermarja</param>
        public bool merrGrupimSipasKodit(string kodi, int idndermarje, int lloji)
        {
            using (clsDatabazeListPagesa db = new clsDatabazeListPagesa())
            {
                bool sukses = mbushGrupime(db.ktheGrupimeLocaleGlobaleSipasKodit(kodi, idndermarje, lloji)).Status;
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
            DataTable dt = db.ktheGjitheGrupimeLocaleGlobaletSipasNdermarjesDheLlojit(IdNdermarje, lloji);
            db.Dispose();
            return dt;
        }

        /// <summary>
        /// kontrollon nese ekziston burimi me kete kod ne kete ndermarje 
        /// </summary>
        /// <param name="kodi">kodi</param>
        /// <param name="idndermarje">idndermarje</param>
        /// <returns> true ose false</returns>
        public static bool ekzistonGrupimeLocaleGlobale(string kodi, int idndermarje)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            bool ekziston = db.ekzistonGrupimeLocaleGlobale(kodi, idndermarje, 1);
            db.Dispose();
            return ekziston;
        }
        public static bool kaVeprimeGrupime(int id)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            return db.kaVeprimeGrupime(id);
        }
        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush burimet me te dhenat nga databaza
        /// </summary>
        /// <param name="dbDataRow">rreshti me te dhena</param>
        /// <returns> kthen ne se mbushja u be ne rregull apo jo</returns>
        internal clsMesazh mbushGrupime(DataRow dbDataRow)
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
                    int.TryParse(dbDataRow["LLOJI"].ToString(), out lloji);
                    int.TryParse(dbDataRow["IDNDERMARJE"].ToString(), out idNdermarje);
                    int.TryParse(dbDataRow["IDKONFIG"].ToString(), out idKonfig);
                    int.TryParse(dbDataRow["IDSTATUSDOK"].ToString(), out idStatusDok); 
                    int.TryParse(dbDataRow["idprindi"].ToString(), out idPrindi);
                    DateTime.TryParse(dbDataRow["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRow["DTMODIFIKIMI"].ToString(), out dtModifikimi);

                    return new clsMesazh(true, clsGrupimeLocaleGlobale.mbushjeSukses);
                }
                catch (InvalidCastException)
                {
                    throw new Exception(clsGrupimeLocaleGlobale.gabimNeTeDhena);
                }
            }
            else
                return new clsMesazh(false, clsGrupimeLocaleGlobale.drbosh);
        }

        #endregion
    }
}
