using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using DbCore.DbKontabiliteti;

namespace DbCore.DbProdhimi
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne nje burim
    ///  (Te dhenat  merren nga tabela : T_BURIME)
    /// </summary>
    public class clsBurime
    {
        /// <summary>
        /// mesazh gabimi per mos plotesimin e kodit
        /// </summary>
        private const string STR_PlotesoniKodinEBurimit = "Plotesoni kodin e burimit!";
        /// <summary>
        /// mesazh gabimi per mospletesimin e emertimit
        /// </summary>
        private const string STR_PlotesoniEmertiminEBurimit = "Plotesoni emertimin e burimit!";
        /// <summary>
        /// mesazh gabimi per mos plotesimin e tipit te burimit
        /// </summary>
        private const string STR_PlotesoniTipinEBurimit = "Plotesoni tipin e burimit!";
        /// <summary>
        /// mesazh gabimi per mos plotesimin e llogarive
        /// </summary>
        private const string STR_PlotesoniLlogarineEBurimit = "Plotesoni llogarine e burimit!";
        /// <summary>
        /// mesazh gabimi per ekzistencen e nje burimi me kete kod
        /// </summary>
        private const string STR_EkzistonNjeBurimMeKeteKodJuLutemShenoniNjeKodTje = "Ekziston nje burim me kete kod. Ju lutem shenoni nje kod tjeter!";
        /// <summary>
        /// mesazh gabimi per koston
        /// </summary>
        private const string STR_KostojaDuhetTeJeteNumerPozitiv = "Kostoja duhet te jete numer pozitiv!";
        /// <summary>
        /// mesazh gabimi kur llogaria nuk ekziston
        /// </summary>
        private const string STR_LlogariaNukEkziston = "Llogaria nuk ekziston!";
        /// <summary>
        /// mesazh kur kontrollet kalohen me sukses
        /// </summary>
        private const string STR_KontrolletEBurimitUKaluanMeSukses = "Kontrollet e burimit u kaluan me sukses";
        /// <summary>
        /// mesazh kur burimi u mbush me sukses
        /// </summary>
        public static string mbushjeSukses = "Burimi u mbush me sukses";
        /// <summary>
        /// mesazh gabimi kur merren te dhenat
        /// </summary>
        public static string gabimNeTeDhena = "ERROR: Gabim gjate marrjes se burimit nga db-ja";
        /// <summary>
        /// mesazh gabimi kur nuk merret asnje e dhene
        /// </summary>
        public static string drbosh = "Mbushja nuk u krye sepse nuk u morr asgje nga db-ja";
        #region Atribute
        /// <summary>
        /// id e burimit
        /// </summary>
        private int idBurimi;
        /// <summary>
        /// kodi
        /// </summary>
        private string kodi;
        /// <summary>
        /// emertimi
        /// </summary>
        private string emertimi;
        /// <summary>
        /// kostoja e planifikuar per nje ore
        /// </summary>
        private decimal kostoPlan;
        /// <summary>
        /// id e llogarise
        /// </summary>
        private int idLlogari;
        /// <summary>
        /// aktiv
        /// </summary>
        private bool aktiv;
        /// <summary>
        /// tipi 
        /// <example> 1-makineri,2-mjet,3 -punonjes</example>
        /// </summary>
        private int tipi;
        /// <summary>
        /// nr i llogarise
        /// </summary>
        private string nrLlogari;
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
        public clsBurime()
        {
        }

        /// <summary>
        /// konstruktori me parameter
        /// </summary>
        /// <param name="idburim">id e burimit</param>
        /// <param name="kodi"> kodi</param>
        /// <param name="pershkrimi"> pershkrimi</param>
        /// <param name="kostoPlan">kostoja e planifikuar</param>
        /// <param name="idllogari"> id e llogarise </param>
        /// <param name="aktiv">aktive apo inaktive</param>
        /// <param name="tipi"> tipi  makineri,mjet apo punonjes</param>
        /// <param name="idPerdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNdermarje">id e ndermarjes</param>
        /// <param name="idKonfig"> id e konfigurimit</param>
        /// <param name="idStatusDok">id e statusit te dokumentit</param>
        public clsBurime(int idburim, string kodi, string pershkrimi, decimal kostoPlan, int idllogari, bool aktiv, int tipi, int idPerdoruesi, int idNdermarje, int idKonfig, int idStatusDok)
        {
            idBurimi = idburim;
            this.kodi = kodi;
            emertimi = pershkrimi;
            this.kostoPlan = kostoPlan;
            idLlogari = idllogari;
            this.aktiv = aktiv;
            this.tipi = tipi;
            this.idPerdoruesi = idPerdoruesi;
            this.idNdermarje = idNdermarje;
            this.idKonfig = idKonfig;
            this.idStatusDok = idStatusDok;
        }

        /// <summary>
        /// konstruktori me parameter
        /// </summary>
        /// <param name="idburim">id e burimit</param>
        /// <param name="kodi"> kodi</param>
        /// <param name="pershkrimi"> pershkrimi</param>
        /// <param name="kostoPlan">kostoja e planifikuar</param>
        /// <param name="idllogari"> id e llogarise </param>
        /// <param name="aktiv">aktive apo inaktive</param>
        /// <param name="tipi"> tipi  makineri,mjet apo punonjes</param>
        /// <param name="idPerdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNdermarje">id e ndermarjes</param>
        /// <param name="idKonfig"> id e konfigurimit</param>
        /// <param name="idStatusDok">id e statusit te dokumentit</param>
        /// <param name="nrLlogari">nr i llogarise</param>
        /// <param name="shtim"> tregon nese po shtojme apo po modifikojme nje burim</param>
        public clsBurime(int idburim, string kodi, string pershkrimi, decimal kostoPlan, int idllogari, bool aktiv, int tipi, int idPerdoruesi, int idNdermarje, int idKonfig, int idStatusDok, string nrLlogari, bool shtim)
        {
            try
            {
                idBurimi = idburim;
                this.kodi = kodi;
                emertimi = pershkrimi;
                this.kostoPlan = kostoPlan;
                idLlogari = idllogari;
                this.aktiv = aktiv;
                this.tipi = tipi;
                this.idPerdoruesi = idPerdoruesi;
                this.idNdermarje = idNdermarje;
                this.idKonfig = idKonfig;
                this.idStatusDok = idStatusDok;
                this.nrLlogari = nrLlogari;
                clsMesazh mesazh = kontrolloBurim(shtim);
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
        public clsBurime(string kodi, int idNderm)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            if (!mbushBurim(db.ktheBurimSipasKodit(kodi, idNderm)).Status)
                idBurimi = -1;
            db.Dispose();
        }
        public clsBurime(string kodi, int idNderm, clsDatabazeProdhimi db)
        {
            if (!mbushBurim(db.ktheBurimSipasKodit(kodi, idNderm)).Status)
                idBurimi = -1;

        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idburim">id e burimit</param>
        public clsBurime(int idburim)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            mbushBurim(db.ktheBurim(idburim));
            db.Dispose();
        }
        public clsBurime(int idburim, clsDatabazeProdhimi db)
        {
            mbushBurim(db.ktheBurim(idburim));

        }

        public clsBurime(DataRow rreshti)
        {
            
            mbushBurim(rreshti);
        }
        #endregion

        #region Properties
        /// <summary>
        /// id e burimit
        /// </summary>
        public int IdBurimi
        {
            get
            {
                return
                    idBurimi;
            }
            set
            {
                idBurimi = value;
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
        public string Emertimi
        {
            get
            {
                return emertimi;
            }
            set
            {
                emertimi = value;
            }
        }
        /// <summary>
        /// kostoja e planifikuar
        /// </summary>
        public decimal KostoPlan
        {
            get
            {
                return kostoPlan;
            }
            set
            {
                kostoPlan = value;
            }
        }
        /// <summary>
        /// id e llogarise 
        /// </summary>
        public int IdLlogari
        {
            get { return idLlogari; }
            set { idLlogari = value; }
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
        /// <seealso cref="TipBurimi.cs"/>
        public int Tipi
        {
            get
            {
                return tipi;
            }
            set
            {
                tipi = value;
            }
        }
        /// <summary>
        /// nr e llogarise 
        /// </summary>
        public string NrLlogari
        {
            get
            {
                return nrLlogari;
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
        private clsMesazh kontrolloBurim(bool shtim)
        {
            if (kodi == "")
                return new clsMesazh(false, STR_PlotesoniKodinEBurimit);
            if (emertimi == "")
                return new clsMesazh(false, STR_PlotesoniEmertiminEBurimit);
            if (tipi == 0)
                return new clsMesazh(false, STR_PlotesoniTipinEBurimit);
            //if (nrLlogari == "")
            //    return new clsMesazh(false, STR_PlotesoniLlogarineEBurimit);

            if (shtim && ekzistonBurim(kodi, idNdermarje))
                return new clsMesazh(false, STR_EkzistonNjeBurimMeKeteKodJuLutemShenoniNjeKodTje);

            if (kostoPlan < 0)
                return new clsMesazh(false, STR_KostojaDuhetTeJeteNumerPozitiv);

            if (nrLlogari != "")
            {
                if (!clsLlogari.ekzistonLlogari(nrLlogari, idNdermarje))
                    return new clsMesazh(false, STR_LlogariaNukEkziston);
            }

            return new clsMesazh(true, STR_KontrolletEBurimitUKaluanMeSukses);
        }

        public clsMesazh ruaj()
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            clsMesazh mesazh = ruaj(db);
            db.Dispose();
            return mesazh;
        }

        public clsMesazh kontrollotransferim(clsBurime kod, int idndermarje, clsDatabazeProdhimi db, int idperdoruesi)
        {
            DbShare.clsDatabaseShare dbshare = new DbShare.clsDatabaseShare(db );
            clsMesazh mesazh = new clsMesazh(true, "Transferimi mbaroi me sukses!");
            if (!db.ekzistonBurim(kod.Kodi, idndermarje))
            {

                DbShare.clsKonfigurimAmbjenti konf = new DbShare.clsKonfigurimAmbjenti();


                konf.mbushKonfigAmbjSipasKod("BUR", idndermarje, dbshare);

                kod.idKonfig = konf.IdKonfigAmbjente;
                kod.idPerdoruesi = idperdoruesi;
                kod.idNdermarje = idndermarje;
                mesazh = kod.ruaj(db);
                if (!mesazh.Status)
                    return mesazh;

            }
            else
            {
                clsBurime kodnderm = new clsBurime(kod.kodi, idndermarje, db);
                kod.IdBurimi = kodnderm.idBurimi;
                DbShare.clsKonfigurimAmbjenti konf = new DbShare.clsKonfigurimAmbjenti();
                konf.mbushKonfigAmbjSipasKod("BUR", idndermarje, dbshare);

                kod.idKonfig = konf.IdKonfigAmbjente;
                kod.idPerdoruesi = idperdoruesi;
                kod.idNdermarje = idndermarje;


                mesazh = kod.modifiko(db);
                if (!mesazh.Status)
                    return mesazh;
            }
            return mesazh;
        }
        /// <summary>
        /// ruan burimin
        /// </summary>
        /// <param name="db"> clsDatabazeProdhimi per te qene pjese e trasaksionit</param>
        /// <returns> cls mesazh qe tregon nqs eshte ruajtur apo jo burimi</returns>
        public clsMesazh ruaj(clsDatabazeProdhimi db)
        {
            clsMesazh mesazh = new clsMesazh();
            //if (db == null)
            //    db = new clsDatabazeProdhimi();
            int idburim = 0;
            mesazh = db.ruajBurim(out idburim, kodi, emertimi, tipi, kostoPlan, idLlogari, aktiv, idKonfig, idPerdoruesi, idNdermarje, idStatusDok);
            idBurimi = idburim;
            return mesazh;
        }

        /// <summary>
        /// modifikon burimin
        /// </summary>
        /// <returns> cls mesazh qe tregon nqs eshte ruajtur apo jo burimi</returns>   
        public clsMesazh modifiko()
        {
            clsMesazh mesazh = new clsMesazh();
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            mesazh = db.modifikoBurim(idBurimi, kodi, emertimi, tipi, kostoPlan, idLlogari, aktiv, idKonfig, idPerdoruesi, idNdermarje, idStatusDok);
            db.Dispose();
            return mesazh;
        }
        public clsMesazh modifiko(clsDatabazeProdhimi db)
        {
            clsMesazh mesazh = new clsMesazh();

            mesazh = db.modifikoBurim(idBurimi, kodi, emertimi, tipi, kostoPlan, idLlogari, aktiv, idKonfig, idPerdoruesi, idNdermarje, idStatusDok);

            return mesazh;
        }

        public clsMesazh fshi()
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            clsMesazh mesazh = fshi(db);
            db.Dispose();
            return mesazh;
        }

        /// <summary>
        /// Fshin objektin burimin ne tabelen perkatese ne databaze
        /// </summary>
        /// <param name="db">db nqs ben pjese ne nje transaksion</param>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh fshi(clsDatabazeProdhimi db)
        {
            //if (db == null)
            //    db = new clsDatabazeProdhimi();
            clsMesazh u_fshi = db.fshiBurimStatus(idBurimi, idPerdoruesi);
            return u_fshi;
        }

        /// <summary>
        /// Merr objektin burim nga tabela perkatese ne databaze.
        /// </summary>
        public void merr()
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            db.ktheBurim(idBurimi);
            db.Dispose();
        }

        /// <summary>
        /// merr objektin burimin sipas kodi dhe ndermarjes
        /// </summary>
        /// <param name="kodi">kodi </param>
        /// <param name="idndermarje">ndermarja</param>
        public bool merrBurimSipasKodit(string kodi, int idndermarje)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            bool sukses = mbushBurim(db.ktheBurimSipasKodit(kodi, idndermarje)).Status;
            db.Dispose();
            return sukses;
        }

        /// <summary>
        /// Merr datatable burim  te nje ndermarje nga tabela perkatese ne databaze.Therret funksionin
        /// </summary>
        /// <returns > nje datatable me te gjithe burimet te kesaj ndermarje</returns>
        public DataTable merriTeGjithe()
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            DataTable dt = db.ktheGjitheBurimetSipasNdermarjes(IdNdermarje);
            db.Dispose();
            return dt;
        }

        /// <summary>
        /// kontrollon nese ekziston burimi me kete kod ne kete ndermarje 
        /// </summary>
        /// <param name="kodi">kodi</param>
        /// <param name="idndermarje">idndermarje</param>
        /// <returns> true ose false</returns>
        public static bool ekzistonBurim(string kodi, int idndermarje)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            bool ekziston = db.ekzistonBurim(kodi, idndermarje);
            db.Dispose();
            return ekziston;
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush burimet me te dhenat nga databaza
        /// </summary>
        /// <param name="dbDataRow">rreshti me te dhena</param>
        /// <returns> kthen ne se mbushja u be ne rregull apo jo</returns>
        internal clsMesazh mbushBurim(DataRow dbDataRow)
        {
            if (dbDataRow != null)
            {
                try
                {
                    int.TryParse(dbDataRow["IDBURIMI"].ToString(), out idBurimi);
                    kodi = dbDataRow["KODI"].ToString();
                    emertimi = dbDataRow["EMERTIMI"].ToString();
                    decimal.TryParse(dbDataRow["KOSTOPLAN"].ToString(), out     kostoPlan);
                    Boolean.TryParse(dbDataRow["AKTIV"].ToString(), out aktiv);
                    int.TryParse(dbDataRow["IDLLOGARI"].ToString(), out idLlogari);
                    nrLlogari = dbDataRow["NRLLOGARI"].ToString();
                    int.TryParse(dbDataRow["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    int.TryParse(dbDataRow["TIPI"].ToString(), out tipi);
                    int.TryParse(dbDataRow["IDNDERMARJE"].ToString(), out idNdermarje);
                    int.TryParse(dbDataRow["IDKONFIG"].ToString(), out idKonfig);
                    int.TryParse(dbDataRow["IDSTATUSDOK"].ToString(), out idStatusDok);
                    DateTime.TryParse(dbDataRow["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRow["DTMODIFIKIMI"].ToString(), out dtModifikimi);

                    return new clsMesazh(true, clsBurime.mbushjeSukses);
                }
                catch (InvalidCastException)
                {
                    throw new Exception(clsBurime.gabimNeTeDhena);
                }
            }
            else
                return new clsMesazh(false, clsBurime.drbosh);
        }

        #endregion
    }
}
