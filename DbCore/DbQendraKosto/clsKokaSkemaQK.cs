using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbQendraKosto
{

    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne nje aktivitet koka
    ///  (Te dhenat  merren nga tabela : T_KOKASKEMAQK)
    /// </summary>
    public class clsKokaSkemaQK
    {
        
        /// <summary>
        /// konstante per gabimin e mos plotesimit te kodit
        /// </summary>
        private const string STR_PlotesoniKodinEAktivitetit = "Plotesoni kodin e skemen!";
        private const string STR_TotaliIPerqindjeveNukMundTeKaloje100 = "Totali i perqindjeve nuk mund te kaloje 100%";
        /// <summary>
        /// konstante kur trupi eshte bosh
        /// </summary>
        private const string STR_DuhetTeZgjidhniTePaktenNjeBurim = "Duhet te zgjidhni te pakten nje qender kosto!";
        /// <summary>
        /// konstante per gabimin e mos plotesimit te emertimit
        /// </summary>
        private const string STR_PlotesoniEmertiminEAktivitetit = "Plotesoni pershkrimin e skemes!";
 
        /// <summary>
        /// konstante per gabimin e ekzistimit te kodit
        /// </summary>
        private const string STR_EkzistonNjeAktivitetMeKeteKodJuLutemShenoniNjeKo = "Ekziston nje skeme me kete kod. Ju lutem shenoni nje kod tjeter!";

        /// <summary>
        /// konstante per kalimin e kontrolleve me sukses
        /// </summary>
        private const string STR_KontrolletEAktivitetitUKaluanMeSukses = "Kontrollet e skemes u kaluan me sukses";
        /// <summary>
        /// konstante kur nuk ruhet aktiviteti
        /// </summary>
        private const string STR_AktivitetiNukURuajt = "Skema nuk u ruajt!";
        /// <summary>
        /// konstante kur nuk ruhet nje nga rreshtat e trupit
        /// </summary>
        private const string STR_NjeNgaRreshtatETrupitNukURuajt = "Nje nga rreshtat e trupit nuk u ruajt!";
        /// <summary>
        /// konstante per mesazhin e gabimit kur nuk modifikohet aktiviteti
        /// </summary>
        private const string STR_AktivitetiNukUModifikua = "Skema nuk u modifikua!";
        /// <summary>
        ///  kur aktiviteti mbushet me sukses
        /// </summary>
        public static string mbushjeSukses = "Skema koka u mbush me sukses";
        /// <summary>
        ///  kur ndodh gabim ne marrjen e te dhenave
        /// </summary>
        public static string gabimNeTeDhena = "ERROR: Gabim gjate marrjes se Skemes nga db-ja";
        /// <summary>
        ///  kur nuk kthen gje db
        /// </summary>
        public static string drbosh = "Mbushja nuk u krye sepse nuk u morr asgje nga db-ja";
        #region Atribute
        /// <summary>
        /// id e kokes se skemes
        /// </summary>
        private int idKoka;
        /// <summary>
        /// kodi i skemes
        /// </summary>
        private string kodi;

        /// <summary>
        /// pershkrimi i skemes
        /// </summary>
        private string pershkrimi;
        
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
        /// koleksioni me qendrat e kostos
        /// </summary>
        private colTrupiSkemaQK colTrupi;
        private DataRow rreshti;
        #endregion

        #region Konstruktoret
        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public clsKokaSkemaQK()
        {
        }

        /// <summary>
        /// konstruktori me parameter
        /// </summary>
        /// <param name="idkoka">id e kokes</param>
        /// <param name="kodi"> kodi</param>
        /// <param name="pershkrimi"> pershkrimi</param>
        /// <param name="idPerdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNdermarje">id e ndermarjes</param>
        /// <param name="idKonfig"> id e konfigurimit</param>
        /// <param name="idStatusDok">id e statusit te dokumentit</param>
        public clsKokaSkemaQK(int idkoka, string kodi, string pershkrimi,  int idPerdoruesi, int idNdermarje, int idKonfig, int idStatusDok)
        {
            idKoka = idkoka;
            this.kodi = kodi;
            this.pershkrimi = pershkrimi;
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
        /// <param name="idPerdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNdermarje">id e ndermarjes</param>
        /// <param name="idKonfig"> id e konfigurimit</param>
        /// <param name="idStatusDok">id e statusit te dokumentit</param>
        /// <param name="trupi"> trupi i aktivitetit</param>
        /// <param name="shtim"> tregon nese po shtojme apo po modifikojme nje burim</param>
        public clsKokaSkemaQK(int idkoka, string kodi, string pershkrimi,  int idPerdoruesi, int idNdermarje, int idKonfig, int idStatusDok, colTrupiSkemaQK trupi, bool shtim)
        {
            try
            {
                idKoka = idkoka;
                this.kodi = kodi;
                this.pershkrimi = pershkrimi;
                this.idPerdoruesi = idPerdoruesi;
                this.idNdermarje = idNdermarje;
                this.idKonfig = idKonfig;
                this.idStatusDok = idStatusDok;
                colTrupi = trupi;
                clsMesazh mesazh = kontrolloSkemen(shtim);
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
        /// <param name="kodi">kodi i skemes</param>
        /// <param name="idNderm">id e ndermarrjes</param>
        public clsKokaSkemaQK(string kodi, int idNderm)
        {
            clsDatabaseQendraKosto db = new clsDatabaseQendraKosto();            
            if (!mbushSkeme(db.ktheKokaSkemaSipasKodit(kodi, idNderm)).Status)
                idKoka = -1;           
            db.Dispose();
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idkoka">id e aktivitetit</param>
        public clsKokaSkemaQK(int idkoka)
        {
            clsDatabaseQendraKosto db = new clsDatabaseQendraKosto();
            mbushSkeme(db.ktheKokaSkema(idkoka));
            db.Dispose();
        }

        public clsKokaSkemaQK(DataRow rreshti)
        {
            
            mbushSkeme(rreshti);
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
        /// kodi i skemes
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
        /// trupi i skems
        /// </summary>
        public colTrupiSkemaQK ColTrupi
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
        /// kontroll nese objekti i skemes i ka te dhenat e sakta 
        /// </summary>
        /// <param name="shtim">tregon nese eshte shtim apo modifikim</param>
        /// <returns> clsMesazh  me statusin nese te dhenat jane te sakta apo jo</returns>
        private clsMesazh kontrolloSkemen(bool shtim)
        {
            if (kodi == "")
                return new clsMesazh(false, STR_PlotesoniKodinEAktivitetit);
            if (pershkrimi == "")
                return new clsMesazh(false, STR_PlotesoniEmertiminEAktivitetit);

           
            if (shtim && ekzistonSkeme(kodi, idNdermarje))
                return new clsMesazh(false, STR_EkzistonNjeAktivitetMeKeteKodJuLutemShenoniNjeKo);
            if( colTrupi.Count==0)
                return new clsMesazh(false, STR_DuhetTeZgjidhniTePaktenNjeBurim);
            decimal shuma = 0;
            foreach (clsTrupiSkemaQK t in colTrupi)
            {
                if(t.Perqindja<=0)
                    return new clsMesazh(false, "Perqindja nuk mund te jete zero ose nr negativ!");
                shuma += t.Perqindja;
            }
      
            if (shuma > 100)
            {
                return new clsMesazh(false, STR_TotaliIPerqindjeveNukMundTeKaloje100);
            }
            return new clsMesazh(true, STR_KontrolletEAktivitetitUKaluanMeSukses);
        }

        /// <summary>
        /// ruan skemen
        /// </summary>
        /// <returns> cls mesazh qe tregon nqs eshte ruajtur apo jo skema</returns>
        public clsMesazh ruaj()
        {
            clsMesazh mesazh = new clsMesazh();
            clsDatabaseQendraKosto db = new clsDatabaseQendraKosto();
            //db.krijoManager();
            db.beginTransaksion();
            int idKoka = 0;
            
            mesazh = db.ruajKokaSkemaQK(out idKoka, kodi,  pershkrimi, idKonfig, idPerdoruesi, idNdermarje, idStatusDok);
            if (!mesazh.Status)
            {
                db.rollbackTransaksion();
                return new clsMesazh(false, STR_AktivitetiNukURuajt);
            }
            IdKoka = idKoka;
            foreach (clsTrupiSkemaQK trupi in colTrupi)
            {
                trupi.IdKoka = IdKoka;
                mesazh = trupi.ruaj(db);
                if (!mesazh.Status)
                {
                    db.rollbackTransaksion();
                    return new clsMesazh(false, STR_NjeNgaRreshtatETrupitNukURuajt);
                }
            }
           
            if (mesazh.Status)
                db.commitTransaksion();
            else db.rollbackTransaksion();
            return mesazh;           
        }

        /// <summary>
        /// modifikon skemen
        /// </summary>
        /// <returns> cls mesazh qe tregon nqs eshte ruajtur apo jo skemen</returns>   
        public clsMesazh modifiko()
        {
            clsMesazh mesazh = new clsMesazh();
            clsDatabaseQendraKosto db = new clsDatabaseQendraKosto();
            //db.krijoManager();
            db.beginTransaksion();
            mesazh = db.modifikoKokaSkemaQK(idKoka, kodi,  pershkrimi, idKonfig, idPerdoruesi, idNdermarje, idStatusDok);
            if (!mesazh.Status)
            {
                db.rollbackTransaksion();
                return new clsMesazh(false, STR_AktivitetiNukUModifikua);
            }
             
                
                    mesazh = db.fshiTrupiSkemaQKSipasIdKoka(idKoka);
                    if (!mesazh.Status)
                    {
                        db.rollbackTransaksion();
                        return new clsMesazh(false, STR_NjeNgaRreshtatETrupitNukURuajt);
                    }
               
            
            foreach (clsTrupiSkemaQK trupi in colTrupi)
            {
                trupi.IdKoka = IdKoka;
                mesazh = trupi.ruaj(db);
                if (!mesazh.Status)
                {
                    db.rollbackTransaksion();
                    return new clsMesazh(false, STR_NjeNgaRreshtatETrupitNukURuajt);
                }
            }

            if (mesazh.Status)
                db.commitTransaksion();
            else db.rollbackTransaksion();
            return mesazh;

        }

        /// <summary>
        /// therret funksionin fshi te kesaj klase
        /// </summary>
        /// <returns>mesazh: true nese fshirja eshte kryer me sukses, false perndryshe.</returns>
        public clsMesazh fshi()
        {
            clsDatabaseQendraKosto db = new clsDatabaseQendraKosto();
            clsMesazh mesazh = fshi(db);
            db.Dispose();
            return mesazh;
        }

        /// <summary>
        /// Fshin objektin skema ne tabelen perkatese ne databaze
        /// </summary>
        /// <param name="db">db nqs ben pjese ne nje transaksion</param>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh fshi(clsDatabaseQendraKosto db)
        {
            clsMesazh u_fshi = db.fshiKokaSkemaQKStatus(idKoka, idPerdoruesi);            
            return u_fshi;
        }

        /// <summary>
        /// Merr objektin skemen nga tabela perkatese ne databaze.
        /// </summary>
        public void merr()
        {
            clsDatabaseQendraKosto db = new clsDatabaseQendraKosto();
            db.ktheKokaSkema(idKoka);
            db.Dispose();
        }

        /// <summary>
        /// merr objektin skemen sipas kodi dhe ndermarjes
        /// </summary>
        /// <param name="kodi">kodi </param>
        /// <param name="idndermarje">ndermarja</param>
        public static void merrSkemenSipasKodit(string kodi, int idndermarje)
        {
            clsDatabaseQendraKosto db = new clsDatabaseQendraKosto();
            db.ktheKokaSkemaSipasKodit(kodi, idndermarje);
            db.Dispose();
        }

        /// <summary>
        /// Merr datatable skemat  te nje ndermarje nga tabela perkatese ne databaze
        /// </summary>
        /// <returns > nje datatable me te gjithe skeamt te kesaj ndermarje</returns>
        public DataTable merriTeGjithe()
        {
            clsDatabaseQendraKosto db = new clsDatabaseQendraKosto();
            DataTable dt = db.ktheGjitheSkematSipasNdermarjes(IdNdermarje);
            db.Dispose();
            return dt;            
        }

        /// <summary>
        /// kontrollon nese ekziston skeme me kete kod ne kete ndermarje 
        /// </summary>
        /// <param name="kodi">kodi</param>
        /// <param name="idndermarje">idndermarje</param>
        /// <returns> true ose false</returns>
        public static bool ekzistonSkeme(string kodi, int idndermarje)
        {
            clsDatabaseQendraKosto db = new clsDatabaseQendraKosto();
            bool ekziston = db.ekzistonKokaSkema(kodi, idndermarje);
            db.Dispose();
            return ekziston;
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush skemen me te dhenat nga databaza
        /// </summary>
        /// <param name="dbDataRow">rreshti me te dhena</param>
        /// <returns> kthen ne se mbushja u be ne rregull apo jo</returns>
        internal clsMesazh mbushSkeme(DataRow dbDataRow)
        {
            if (dbDataRow != null)
            {
                try
                {
                    int.TryParse(dbDataRow["IDKOKA"].ToString(), out idKoka);
                    kodi = dbDataRow["KODI"].ToString();
                    pershkrimi = dbDataRow["PERSHKRIMI"].ToString();
                   
                    int.TryParse(dbDataRow["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    int.TryParse(dbDataRow["IDNDERMARJE"].ToString(), out idNdermarje);
                    int.TryParse(dbDataRow["IDKONFIG"].ToString(), out idKonfig);
                    int.TryParse(dbDataRow["IDSTATUSDOK"].ToString(), out idStatusDok);
                    DateTime.TryParse(dbDataRow["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRow["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    colTrupi = new colTrupiSkemaQK();
                    return new clsMesazh(true, mbushjeSukses);
                }
                catch (InvalidCastException)
                {
                    throw new Exception(gabimNeTeDhena);
                }
            }
            else
                return new clsMesazh(false, drbosh);
        }

        #endregion
    }
}
