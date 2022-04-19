using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace DbCore.DbProdhimi
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne nje aktivitet koka
    ///  (Te dhenat  merren nga tabela : T_AKTIVITETEKOKA)
    /// </summary>
    public class clsAktiviteteKoka
    {
        /// <summary>
        /// konstante per gabimin e mos plotesimit te kodit
        /// </summary>
        private const string STR_PlotesoniKodinEAktivitetit = "Plotesoni kodin e aktivitetit!";
        /// <summary>
        /// konstante kur trupi eshte bosh
        /// </summary>
        private const string STR_DuhetTeZgjidhniTePaktenNjeBurim = "Duhet te zgjidhni te pakten nje burim!";
        /// <summary>
        /// konstante per gabimin e mos plotesimit te emertimit
        /// </summary>
        private const string STR_PlotesoniEmertiminEAktivitetit = "Plotesoni emertimin e aktivitetit!";
        /// <summary>
        /// konstante per gabimin e mos plotesimit te njesise se kohes
        /// </summary>
        private const string STR_PlotesoniNjesineEKohes = "Plotesoni njesine e kohes!";
        /// <summary>
        /// konstante per gabimin e ekzistimit te kodit
        /// </summary>
        private const string STR_EkzistonNjeAktivitetMeKeteKodJuLutemShenoniNjeKo = "Ekziston nje aktivitet me kete kod. Ju lutem shenoni nje kod tjeter!";
        /// <summary>
        /// konstante per gabimin qe koha e ekzekutimit te jete me e vogel se zero
        /// </summary>
        private const string STR_KohaDuhetTeJeteNumerPozitiv = "Koha duhet te jete numer pozitiv!";
        /// <summary>
        /// konstante per kalimin e kontrolleve me sukses
        /// </summary>
        private const string STR_KontrolletEAktivitetitUKaluanMeSukses = "Kontrollet e aktivitetit u kaluan me sukses";
        /// <summary>
        /// konstante kur nuk ruhet aktiviteti
        /// </summary>
        private const string STR_AktivitetiNukURuajt = "Aktiviteti nuk u ruajt!";
        /// <summary>
        /// konstante kur nuk ruhet nje nga rreshtat e trupit
        /// </summary>
        private const string STR_NjeNgaRreshtatETrupitNukURuajt = "Nje nga rreshtat e trupit nuk u ruajt!";
        /// <summary>
        /// konstante per mesazhin e gabimit kur nuk modifikohet aktiviteti
        /// </summary>
        private const string STR_AktivitetiNukUModifikua = "Aktiviteti nuk u modifikua!";
        /// <summary>
        ///  kur aktiviteti mbushet me sukses
        /// </summary>
        public static string mbushjeSukses = "Aktivitet koka u mbush me sukses";
        /// <summary>
        ///  kur ndodh gabim ne marrjen e te dhenave
        /// </summary>
        public static string gabimNeTeDhena = "ERROR: Gabim gjate marrjes se aktivitetit nga db-ja";
        /// <summary>
        ///  kur nuk kthen gje db
        /// </summary>
        public static string drbosh = "Mbushja nuk u krye sepse nuk u morr asgje nga db-ja";
        #region Atribute
        /// <summary>
        /// id e kokes se aktivitetit
        /// </summary>
        private int idKoka;
        /// <summary>
        /// kodi i aktivitetit
        /// </summary>
        private string kodi;
        /// <summary>
        /// emertimi i aktivitetit
        /// </summary>
        private string emertimi;
        /// <summary>
        /// pershkrimi i aktivitetit
        /// </summary>
        private string pershkrimi;
        /// <summary>
        /// koha e planifikuar e ekzekutimit te aktivitetit
        /// </summary>
        private decimal kohaPlan;
        /// <summary>
        /// njesia e kohes 
        /// <example>1-sekonde,2-minuta,3-dite,4-ore</example>
        /// </summary>
        private int njesiKohe;
        /// <summary>
        /// id e perdoruesit qe ka kryer veprimin
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
        /// id e statusit te dokumentit 
        /// <example>0-draft,1-ruajtur,2-fshire,3 -stronim</example>
        /// </summary>
        private int idStatusDok;
        /// <summary>
        /// data e krijimit te dokumentit
        /// </summary>
        private DateTime dtKrijimi;
        /// <summary>
        /// data e modifikimit te fundit te rreshtit
        /// </summary>
        private DateTime dtModifikimi;
        /// <summary>
        /// koleksioni me burimet e aktivitetit
        /// </summary>
        private colAktiviteteTrupi colTrupi;
        private DataRow rreshti;
        #endregion

        #region Konstruktoret
        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public clsAktiviteteKoka()
        {
        }

        /// <summary>
        /// konstruktori me parameter
        /// </summary>
        /// <param name="idkoka">id e kokes</param>
        /// <param name="kodi"> kodi</param>
        /// <param name="pershkrimi"> pershkrimi</param>
        /// <param name="emertimi">emertimi</param>
        /// <param name="kohaPlan">koha e planifikuar</param>
        /// <param name="njesikohe"> njesi kohe sek,min,ore ,dite</param>
        /// <param name="idPerdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNdermarje">id e ndermarjes</param>
        /// <param name="idKonfig"> id e konfigurimit</param>
        /// <param name="idStatusDok">id e statusit te dokumentit</param>
        public clsAktiviteteKoka(int idkoka, string kodi,string emertimi, string pershkrimi, decimal kohaPlan,  int njesikohe, int idPerdoruesi, int idNdermarje, int idKonfig, int idStatusDok)
        {
            idKoka = idkoka;
            this.kodi = kodi;
            this.emertimi = emertimi;
            this.pershkrimi = pershkrimi;
            this.kohaPlan = kohaPlan;
            njesiKohe = njesikohe;
            this.idPerdoruesi = idPerdoruesi;
            this.idNdermarje = idNdermarje;
            this.idKonfig = idKonfig;
            this.idStatusDok = idStatusDok;
        }

        /// <summary>
        /// konstruktori me parameter
        /// </summary>
        /// <param name="idkoka">id e kokes</param>
        /// <param name="kodi"> kodi</param>
        /// <param name="pershkrimi"> pershkrimi</param>
        /// <param name="emertimi">emertimi</param>
        /// <param name="kohaPlan">koha e planifikuar</param>
        /// <param name="njesikohe"> njesi kohe sek,min,ore ,dite</param>
        /// <param name="idPerdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNdermarje">id e ndermarjes</param>
        /// <param name="idKonfig"> id e konfigurimit</param>
        /// <param name="idStatusDok">id e statusit te dokumentit</param>
        /// <param name="trupi"> trupi i aktivitetit</param>
        /// <param name="shtim"> tregon nese po shtojme apo po modifikojme nje burim</param>
        public clsAktiviteteKoka(int idkoka, string kodi,string emertimi, string pershkrimi, decimal kohaPlan,  int njesikohe, int idPerdoruesi, int idNdermarje, int idKonfig, int idStatusDok, colAktiviteteTrupi trupi, bool shtim)
        {
            try
            {
                idKoka = idkoka;
                this.kodi = kodi;
                this.emertimi = emertimi;
                this.pershkrimi = pershkrimi;
                this.kohaPlan = kohaPlan;
                njesiKohe = njesikohe;
                this.idPerdoruesi = idPerdoruesi;
                this.idNdermarje = idNdermarje;
                this.idKonfig = idKonfig;
                this.idStatusDok = idStatusDok;
                colTrupi = trupi;
                clsMesazh mesazh = kontrolloAktivitet(shtim);
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
        /// <param name="kodi">kodi i aktivitetit</param>
        /// <param name="idNderm">id e ndermarrjes</param>
        public clsAktiviteteKoka(string kodi, int idNderm)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();            
            if (!mbushAktivitet(db.ktheAktivitetinSipasKodit(kodi, idNderm)).Status)
                idKoka = -1;           
            db.Dispose();
        }
        public clsAktiviteteKoka(string kodi, int idNderm, clsDatabazeProdhimi db )
        {
           if (!mbushAktivitet(db.ktheAktivitetinSipasKodit(kodi, idNderm)).Status)
                idKoka = -1;           
        
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idkoka">id e aktivitetit</param>
        public clsAktiviteteKoka(int idkoka)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            mbushAktivitet(db.ktheAkivitetKoka(idkoka));
            db.Dispose();
        } 
        public clsAktiviteteKoka(int idkoka,clsDatabazeProdhimi db)
        {
             mbushAktivitet(db.ktheAkivitetKoka(idkoka));
    
        }

        public clsAktiviteteKoka(DataRow rreshti)
        {
            
            mbushAktivitet(rreshti);
        }
        #endregion

        #region Properties
        /// <summary>
        /// id e kokes
        /// </summary>
        public int IdKoka
        {
            get
            {
                return
                    idKoka;
            }
            set
            {
                idKoka = value;
            }
        }
        /// <summary>
        /// kodi i aktivitetit
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
        /// koha e planifikuar
        /// </summary>
        public decimal KohaPlan
        {
            get
            {
                return kohaPlan;
            }
            set
            {
                kohaPlan = value;
            }
        }

        /// <summary>
        /// pershkrimi
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
        /// njesi kohe
        /// <example> 1-sek,2-minute,3 -ore, 4-dite</example>
        /// </summary>
        /// <seealso cref="NjesiKohe.cs"/>
        public int NjesiKohe
        {
            get
            {
                return njesiKohe;
            }
            set
            {
                njesiKohe = value;
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

        /// <summary>
        /// trupi i aktivitetit
        /// </summary>
        public colAktiviteteTrupi ColTrupi
        {
            get
            {
                return colTrupi;
            }
            set
            {
                colTrupi = value;
            }
        }
        #endregion

        #region Metoda Publike

        /// <summary>
        /// kontroll nese objekti i aktivitete i ka te dhenat e sakta 
        /// </summary>
        /// <param name="shtim">tregon nese eshte shtim apo modifikim</param>
        /// <returns> clsMesazh  me statusin nese te dhenat jane te sakta apo jo</returns>
        private clsMesazh kontrolloAktivitet(bool shtim)
        {
            if (kodi == "")
                return new clsMesazh(false, STR_PlotesoniKodinEAktivitetit);
            if (emertimi == "")
                return new clsMesazh(false, STR_PlotesoniEmertiminEAktivitetit);
            if (njesiKohe == 0)
                return new clsMesazh(false, STR_PlotesoniNjesineEKohes);
           
            if (shtim && ekzistonAktivitet(kodi, idNdermarje))
                return new clsMesazh(false, STR_EkzistonNjeAktivitetMeKeteKodJuLutemShenoniNjeKo);

            if (kohaPlan < 0)
                return new clsMesazh(false, STR_KohaDuhetTeJeteNumerPozitiv);
            if( colTrupi.Count==0)
                return new clsMesazh(false, STR_DuhetTeZgjidhniTePaktenNjeBurim);
            return new clsMesazh(true, STR_KontrolletEAktivitetitUKaluanMeSukses);
        }
        public clsMesazh ruaj()
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            db.beginTransaksion();
            clsMesazh ruajtur = ruaj(db);
            if (!ruajtur.Status)
                db.rollbackTransaksion();
            else db.commitTransaksion();
            return ruajtur;

        }
      
        public clsMesazh modifiko()
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            db.beginTransaksion();
            clsMesazh ruajtur = modifiko(db);
            if (!ruajtur.Status)
                db.rollbackTransaksion();
            else db.commitTransaksion();
            return ruajtur;

        }
        

        /// <summary>
        /// ruan aktivitetin
        /// </summary>
        /// <returns> cls mesazh qe tregon nqs eshte ruajtur apo jo aktiviteti</returns>
        public clsMesazh ruaj(clsDatabazeProdhimi db)
        {
            clsMesazh mesazh = new clsMesazh();
           
            int idKoka = 0;
            
            mesazh = db.ruajAktiviteteKoka(out idKoka, kodi, emertimi, pershkrimi, njesiKohe, kohaPlan, idKonfig, idPerdoruesi, idNdermarje, idStatusDok);
            if (!mesazh.Status)
            {
               
                return new clsMesazh(false, STR_AktivitetiNukURuajt);
            }
            IdKoka = idKoka;
            foreach (clsAktiviteteTrupi trupi in colTrupi)
            {
                trupi.IdKoka = IdKoka;
                mesazh = trupi.ruaj(db);
                if (!mesazh.Status)
                {
                    
                    return new clsMesazh(false, STR_NjeNgaRreshtatETrupitNukURuajt);
                }
            }
           
          
            return mesazh;           
        }

        /// <summary>
        /// modifikon burimin
        /// </summary>
        /// <returns> cls mesazh qe tregon nqs eshte ruajtur apo jo burimi</returns>   
        public clsMesazh modifiko(  clsDatabazeProdhimi db)
        {
            colAktiviteteTrupi coltrupivjeter = new colAktiviteteTrupi(idKoka, colTrupi[0].DtNdryshimi,db);
            List<string> data = colAktiviteteTrupi.merrDataAktiviteti(idKoka,db);
            clsMesazh mesazh = new clsMesazh();
          
            mesazh = db.modifikoAktiviteteKoka(idKoka, kodi, emertimi, pershkrimi, njesiKohe, kohaPlan, idKonfig, idPerdoruesi, idNdermarje, idStatusDok);
            if (!mesazh.Status)
            {
                return new clsMesazh(false, STR_AktivitetiNukUModifikua);
            }
            if (data.Contains(colTrupi[0].DtNdryshimi.ToShortDateString()))
            {
                
                foreach (clsAktiviteteTrupi t in coltrupivjeter)
                {
                    mesazh = db.fshiAktiviteteTrupi(t.IdTrupi);
                    if (!mesazh.Status)
                    {
                        return new clsMesazh(false, STR_NjeNgaRreshtatETrupitNukURuajt);
                    }
                }
            }
           

            foreach (clsAktiviteteTrupi trupi in colTrupi)
            {
                trupi.IdKoka = IdKoka;
                mesazh = trupi.ruaj(db);
                if (!mesazh.Status)
                {
                   
                    return new clsMesazh(false, STR_NjeNgaRreshtatETrupitNukURuajt);
                }
            }

           
            return mesazh;

        }

        /// <summary>
        /// therret funksionin fshi te kesaj klase
        /// </summary>
        /// <returns>mesazh: true nese fshirja eshte kryer me sukses, false perndryshe.</returns>
        public clsMesazh fshi()
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            clsMesazh mesazh = fshi(db);
            db.Dispose();
            return mesazh;
        }

        /// <summary>
        /// Fshin objektin aktivitet ne tabelen perkatese ne databaze
        /// </summary>
        /// <param name="db">db nqs ben pjese ne nje transaksion</param>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh fshi(clsDatabazeProdhimi db)
        {
            //if (db == null)
            //    db = new clsDatabazeProdhimi();
            clsMesazh u_fshi = db.fshiAktiviteteKokaStatus(idKoka, idPerdoruesi);            
            return u_fshi;
        }

        /// <summary>
        /// Merr objektin aktivitet nga tabela perkatese ne databaze.
        /// </summary>
        public void merr()
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            db.ktheAkivitetKoka(idKoka);
            db.Dispose();
        }

        /// <summary>
        /// merr objektin aktivitet sipas kodi dhe ndermarjes
        /// </summary>
        /// <param name="kodi">kodi </param>
        /// <param name="idndermarje">ndermarja</param>
        public static void merrAktivitetSipasKodit(string kodi, int idndermarje)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            db.ktheAktivitetinSipasKodit(kodi, idndermarje);
            db.Dispose();
        }

        /// <summary>
        /// Merr datatable aktivitete  te nje ndermarje nga tabela perkatese ne databaze
        /// </summary>
        /// <returns > nje datatable me te gjithe aktivitetet te kesaj ndermarje</returns>
        public DataTable merriTeGjithe()
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            DataTable dt = db.ktheGjitheAktivitetetSipasNdermarjes(IdNdermarje);
            db.Dispose();
            return dt;            
        }

        /// <summary>
        /// kontrollon nese ekziston aktiviteti me kete kod ne kete ndermarje 
        /// </summary>
        /// <param name="kodi">kodi</param>
        /// <param name="idndermarje">idndermarje</param>
        /// <returns> true ose false</returns>
        public static bool ekzistonAktivitet(string kodi, int idndermarje)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            bool ekziston = db.ekzistonAktivitet(kodi, idndermarje);
            db.Dispose();
            return ekziston;
        }

        public clsMesazh kontrollotransferim(clsAktiviteteKoka kod, int idndermarje, clsDatabazeProdhimi db, int idperdoruesi)
        {        DbShare.clsDatabaseShare dbshare = new DbShare.clsDatabaseShare(db );
            clsMesazh mesazh = new clsMesazh(true, "Transferimi mbaroi me sukses!");
            if (!db.ekzistonAktivitet(kod.kodi, idndermarje))
            {
                foreach (clsAktiviteteTrupi per in kod.colTrupi)
                {

                    DbProdhimi.clsBurime akt = new DbProdhimi.clsBurime(per.IdBurimi, db);
                    mesazh = akt.kontrollotransferim(akt, idndermarje, db, idperdoruesi);
                    if (!mesazh.Status)
                    {

                        return mesazh;
                    }
                  
                    per.IdBurimi = akt.IdBurimi;
                }

                DbShare.clsKonfigurimAmbjenti konf = new DbShare.clsKonfigurimAmbjenti();
        
          
                konf.mbushKonfigAmbjSipasKod("AKT", idndermarje,dbshare);

                kod.idKonfig = konf.IdKonfigAmbjente;
                kod.idPerdoruesi = idperdoruesi;
                kod.idNdermarje = idndermarje;
                mesazh = kod.ruaj(db);
                if (!mesazh.Status)
                    return mesazh;

            }
            else
            {
                clsAktiviteteKoka kodnderm = new clsAktiviteteKoka(kod.kodi, idndermarje, db);        
                kod.idKoka = kodnderm.idKoka;
                foreach (clsAktiviteteTrupi per in kod.colTrupi)
                {

                    DbProdhimi.clsBurime akt = new DbProdhimi.clsBurime(per.IdBurimi, db);
                    mesazh = akt.kontrollotransferim(akt, idndermarje, db, idperdoruesi);
                    if (!mesazh.Status)
                    {

                        return mesazh;
                    }
                   
                    per.IdBurimi = akt.IdBurimi;
                }

                DbShare.clsKonfigurimAmbjenti konf = new DbShare.clsKonfigurimAmbjenti();
                konf.mbushKonfigAmbjSipasKod("AKT", idndermarje, dbshare);

                kod.idKonfig = konf.IdKonfigAmbjente;
                kod.idPerdoruesi = idperdoruesi;
                kod.idNdermarje = idndermarje;
       

                mesazh = kod.modifiko(db);
                if (!mesazh.Status)
                    return mesazh;
            }
            return mesazh;
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush aktivitetin me te dhenat nga databaza
        /// </summary>
        /// <param name="dbDataRow">rreshti me te dhena</param>
        /// <returns> kthen ne se mbushja u be ne rregull apo jo</returns>
        internal clsMesazh mbushAktivitet(DataRow dbDataRow)
        {
            if (dbDataRow != null)
            {
                try
                {
                    int.TryParse(dbDataRow["IDKOKA"].ToString(), out idKoka);
                    kodi = dbDataRow["KODI"].ToString();
                    emertimi = dbDataRow["EMERTIMI"].ToString(); 
                    pershkrimi = dbDataRow["PERSHKRIMI"].ToString();
                    decimal.TryParse(dbDataRow["KOHAPLAN"].ToString(), out     kohaPlan);
                   
                    int.TryParse(dbDataRow["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    int.TryParse(dbDataRow["NJESIKOHE"].ToString(), out njesiKohe);
                    int.TryParse(dbDataRow["IDNDERMARJE"].ToString(), out idNdermarje);
                    int.TryParse(dbDataRow["IDKONFIG"].ToString(), out idKonfig);
                    int.TryParse(dbDataRow["IDSTATUSDOK"].ToString(), out idStatusDok);
                    DateTime.TryParse(dbDataRow["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRow["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    colTrupi = new colAktiviteteTrupi();
                    return new clsMesazh(true, clsAktiviteteKoka.mbushjeSukses);
                }
                catch (InvalidCastException)
                {
                    throw new Exception(clsAktiviteteKoka.gabimNeTeDhena);
                }
            }
            else
                return new clsMesazh(false, clsAktiviteteKoka.drbosh);
        }

        #endregion
    }
}
