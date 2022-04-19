using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using DbCore.DbKontabiliteti;
using DbCore.DbAdmin;
using System.Resources;
using System.Globalization;
using DbCore.DbRegjistrim;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Logging;
using DbCore.DbShare;


//GIMPROVE hiq gjerat teper

namespace DbCore.DbListPagesat
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne  koken e nje dokumenti te list pageses
    ///  (Te dhenat  merren nga tabela : T_KOKALISTPAGESA)
    /// </summary>
    public class clsKokaListPagese
    {
        private const string gabimTeDhena = "ERROR: Gabim gjate marrjes se kokes se list pagesave nga db-ja";
        private const string STR_NdodhiNjeGabimGjateRuajtjesSeKokesSeListPageses = "Ndodhi nje Gabim gjate ruajtjes se Kokes se List Pageses";
        private const string STR_DokumentiKaNdryshuarJuLutemRihapeniPerseri = "Dokumenti ka ndryshuar! Ju lutem rihapeni perseri!";
        #region Attributet
        private int idKoka;
        private int idNivel;
        private int idKonfigAmbjente;
        private int idDepartamenti;
        private int idNenDepartamenti;
        private DateTime dtDok;
        private String nrDok;
        private int muaji;
        private decimal totali;
        private int idMonedha;
        private int idDokNga;
        private decimal kursi;
        private int idStatusDok;
        private int idNdermarje;
        private int idNderViti;
        private int idPerdoruesi;
        private DateTime dtRegjistrimi;
        private string shenime;
        private int idNivelGjenerues;
        private int idKonfigGjenerues;
        private int idGjenerues;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private colTrupiListPagese oColTrupi;
        private clsKokaFleteKontabel oFleteKontabel;
        private colArkiva oArkiva;

        public IDictionary<string, object> HfArkiva { get; set; }

        private string kodMonedha;
        private StatusAprovimi statusAprovimi;
        private decimal totaliNdermarje;
        private DataRow rreshti;
        private clsDatabazeListPagesa db;
        private int idKategoria;
        #endregion

        #region Properties
        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdKoka
        {
            get { return idKoka; }
            set { idKoka = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne  e nivelit te regjistrimit.
        /// </summary>
        public int IdNivel
        {
            get { return idNivel; }
            set { idNivel = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e konfigurimit te ambjentit
        /// </summary>
        public int IdKonfigAmbjente
        {
            get { return idKonfigAmbjente; }
            set { idKonfigAmbjente = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e departamenti.
        /// </summary>
        public int IdDepartamenti
        {
            get { return idDepartamenti; }
            set { idDepartamenti = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e NenDepartamenti
        /// </summary>
        public int IdNenDepartamenti
        {
            get { return idNenDepartamenti; }
            set { idNenDepartamenti = value; }
        }
        /// <summary>
        /// Kthen/Vendos Muajin
        /// </summary>
        public int Muaji
        {
            get { return muaji; }
            set { muaji = value; }
        }
        public StatusAprovimi StatusAprovimi
        {
            get
            {
                return statusAprovimi;
            }
            set
            {
                statusAprovimi = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos totalin.
        /// </summary>
        public decimal Totali
        {
            get { return totali; }
            set { totali = value; }
        }
        /// <summary>
        /// Kthen/Vendos nr i dokumentit.
        /// </summary>
        public String NrDok
        {
            get { return nrDok; }
            set { nrDok = value; }
        }
        /// <summary>
        /// Kthen/Vendos dt e dokumentit.
        /// </summary>
        public DateTime DtDok
        {
            get { return dtDok; }
            set { dtDok = value; }
        }
        /// <summary>
        /// Kthen/Vendos shenime.
        /// </summary>
        public String Shenime
        {
            get { return shenime; }
            set { shenime = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne  e monedhes.
        /// </summary>
        public int IdMonedha
        {
            get { return idMonedha; }
            set { idMonedha = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne  e dokumentit nga i cili gjenerohet ne rastet e modifikimit
        /// </summary>
        public int IdDokNga
        {
            get { return idDokNga; }
            set { idDokNga = value; }
        }
        /// <summary>
        /// Kthen/Vendos kursin
        /// </summary>
        public decimal Kursi
        {
            get { return kursi; }
            set { kursi = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e gjendjes se dokumentit
        /// <example> ruajtur, draft etj.</example>
        /// </summary>
        public int IdStatusDok
        {
            get { return idStatusDok; }
            set { idStatusDok = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne  e ndermarjes.
        /// </summary>
        public int IdNdermarje
        {
            get { return idNdermarje; }
            set { idNdermarje = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e ndermarje vitit.
        /// </summary>
        public int IdNderViti
        {
            get { return idNderViti; }
            set { idNderViti = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne  e perdoruesit qe e ka ruajtur.
        /// </summary>
        public int IdPerdoruesi
        {
            get { return idPerdoruesi; }
            set { idPerdoruesi = value; }
        }
        /// <summary>
        /// Kthen/Vendos dt e regjistrimit te dokumentit.
        /// </summary>
        public DateTime DtRegjistrimi
        {
            get { return dtRegjistrimi; }
            set { dtRegjistrimi = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e nivelit te dokumentit nga eshte gjeneruar magazina
        /// </summary>
        public int IdNivelGjenerues
        {
            get { return idNivelGjenerues; }
            set { idNivelGjenerues = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e konfigurimit te dokumentit nga eshte gjeneruar magazina
        /// </summary>
        public int IdKonfigGjenerues
        {
            get { return idKonfigGjenerues; }
            set { idKonfigGjenerues = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e dokumentit nga eshte gjeneruar magazina nga nje ambjent tjeter
        /// </summary>
        public int IdGjenerues
        {
            get { return idGjenerues; }
            set { idGjenerues = value; }
        }
        /// <summary>
        /// Kthen/Vendos  nje koleksion me trupin e dokumentit te listpageses
        /// </summary>
        public colTrupiListPagese OColTrupi
        {
            get { return oColTrupi; }
            set { oColTrupi = value; }
        }
      
        /// <summary>
        /// Kthen/Vendos nje  flete kontabel te gjeneruar nga dokumenti i list pageses kur kontabilizohet.
        /// </summary>
        public clsKokaFleteKontabel OFleteKontabel
        {
            get { return oFleteKontabel; }
            set { oFleteKontabel = value; }
        }
        /// <summary>
        /// data e krijimit
        /// </summary>
        public DateTime DtKrijimi
        {
            get { return dtKrijimi; }

        }
        public decimal TotaliNdermarje
        {
            get
            {
                return totaliNdermarje;
            }
            set
            {
                totaliNdermarje = value;
            }
        }
        /// <summary>
        /// data e fundit e modifikimit
        /// </summary>
        public DateTime DtModifikimi
        {
            get { return dtModifikimi; }

        }

        #endregion

        #region Konstruktoret
        /// <summary>
        /// Konstruktori i klases me parametra
        /// </summary>
        /// <param name="dtDk"> data e dokumentit </param>
        /// <param name="dtRegj"> data e regjistrimit te dokumentit </param>
        /// <param name="idmon"> id e monedhes.</param>
        /// <param name="iddep"> id e departamentit</param>
        /// <param name="idKonf"> id e konfigurimit te dokumentit</param>
        /// <param name="iddoknga">id e dokumentit nga e cila gjenerohet ne rastet e modifikimit </param>
        /// <param name="idnendep">id e nendepartamentit</param>
        /// <param name="idkoka">id ritese e kokes </param>
        /// <param name="idNder"> id e ndermarjes</param>
        /// <param name="idNdVt"> id e ndermarje vitit</param>
        /// <param name="idNiv"> id e nivelit te regjistrimit</param>
        /// <param name="idPer"> id e perdoruesit se e ben regjistrimin</param>
        /// <param name="muaj"> muaji</param>
        /// <param name="idSt"> id e statusit te dokumentit</param>
        /// <param name="nrDk">nr i dokumentit</param>
        /// <param name="total">totali i dok</param>
        /// <param name="shenim"> shenime </param>
        /// <param name="kurs"> kursi</param>
        /// <param name="idGjenerues">id e dokumentit nga gjenerohet magazina nga nje ambjent tjeter</param>
        /// <param name="idKonfigGjenerues">id e konfigurimit nga gjenerohet dokumenti</param>
        /// <param name="idNivelGjenerues">id e nivelit nga gjenerohet dokumenti</param>
        public clsKokaListPagese(int idkoka, int idNiv, int idKonf, int iddep, int idnendep, DateTime dtDk, string nrDk, int muaj, decimal total, int idmon, int iddoknga, decimal kurs, int idSt, int idNder, int idNdVt, int idPer, DateTime dtRegj, string shenim, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues, StatusAprovimi statusapp)
        {
            idKoka = idkoka;
            idNivel = idNiv;
            idKonfigAmbjente = idKonf;
            idDepartamenti = iddep;
            idNenDepartamenti = idnendep;
            nrDok = nrDk;
            dtDok = dtDk;
            muaji = muaj;
            totali = total;
            idMonedha = idmon;
            idDokNga = iddoknga;
            kursi = kurs;
            idStatusDok = idSt;
            idNdermarje = idNder;
            idNderViti = idNdVt;
            idPerdoruesi = idPer;
            dtRegjistrimi = dtRegj;
            shenime = shenim;
            this.idNivelGjenerues = idNivelGjenerues;
            this.idKonfigGjenerues = idKonfigGjenerues;
            this.idGjenerues = idGjenerues;
            statusAprovimi = statusapp;
            oColTrupi = new colTrupiListPagese();
            oFleteKontabel = new clsKokaFleteKontabel();

        }

        ///// <summary>
        ///// konstruktor me 3 parametra
        ///// </summary>
        ///// <param name="idNivel">id e nivelit</param>
        ///// <param name="nrdok">nr i dokumentit</param>
        ///// <param name="dtdok">data e dokumentit</param>
        //public clsKokaListPagese(int idNivel, string nrdok, DateTime dtdok)
        //{
        //    clsDatabazeListPagesa dbListPagesa = new clsDatabazeListPagesa();
        //    mbushKokaListPagese(dbListPagesa.ktheKokaListPageseSipasIdNivelNrDokDtDok(idNivel, nrdok, dtdok), dbListPagesa);
        //    dbListPagesa.Dispose();
        //}

        /// <summary>
        /// konstruktori me nje param
        /// </summary>
        /// <param name="idkoka">id koka</param>
        public clsKokaListPagese(int idkoka, bool meTrup)
        {
            clsDatabazeListPagesa dbListPagesa = new clsDatabazeListPagesa();
            mbushKokaListPagese(dbListPagesa.ktheKokaListPageseSipasID(idkoka), meTrup, dbListPagesa);
            dbListPagesa.Dispose();
        }
        public clsKokaListPagese(int idkoka, bool meTrup, clsDatabazeListPagesa dbListPagesa)
        {
            mbushKokaListPagese(dbListPagesa.ktheKokaListPageseSipasID(idkoka), meTrup, dbListPagesa);

        }

        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsKokaListPagese()
        {
        }

        public clsKokaListPagese(DataRow rreshti, clsDatabazeListPagesa db)
        {
            
            mbushKokaListPagese(rreshti, false, db);
        }
        #endregion

        #region Metoda Publike

        public clsMesazh krijoListPagese(int idNiv, int idKonf, int iddep, int idnendep, DateTime dtDk, string nrDk, int muaj, decimal total, int idmon, string monedha, int iddoknga, decimal kurs, int idSt, int idNder, int idNdVt, int idPer, DateTime dtRegj, string shenim, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues, colTrupiListPagese trupi, int idperiudha, bool mekontabilizim, out string shfaqmesazhapolupe, DbQendraKosto.colTrupiQendraKosto trupivjeterqendra, int idGjuha, ResourceManager rm, CultureInfo ci, StatusAprovimi statusAprovimi, decimal totalindermarje, IDictionary<string, object> hfArkiva)
        {
            shfaqmesazhapolupe = "jo";
            idNivel = idNiv;
            idKonfigAmbjente = idKonf;
            idDepartamenti = iddep;
            idNenDepartamenti = idnendep;
            kodMonedha = monedha;
            nrDok = nrDk;
            dtDok = dtDk;
            muaji = muaj;
            totali = total;
            idMonedha = idmon;
            idDokNga = iddoknga;
            kursi = kurs;
            idStatusDok = idSt;
            idNdermarje = idNder;
            idNderViti = idNdVt;
            idPerdoruesi = idPer;
            dtRegjistrimi = dtRegj;
            shenime = shenim;
            totaliNdermarje = totalindermarje;
            this.statusAprovimi = statusAprovimi;
            this.idNivelGjenerues = idNivelGjenerues;
            this.idKonfigGjenerues = idKonfigGjenerues;
            this.idGjenerues = idGjenerues;
            oColTrupi = trupi;
            string pershkrimFK;
            if (Shenime != String.Empty)
                pershkrimFK = Shenime;
            else pershkrimFK = "Nga regjistrimet e listpageses";
            const int idllojdok = 38;
            if (mekontabilizim)
                oFleteKontabel = clsKokaFleteKontabel.gjeneroKontabilizimListPagese(idKoka, idNivel, idKonfigAmbjente, dtDok, nrDok, idNdermarje, idNderViti, idPerdoruesi, dtRegj, trupi, pershkrimFK, 0, idllojdok, idperiudha, kursi, idMonedha, out shfaqmesazhapolupe, trupivjeterqendra, idGjuha, rm, ci);
            else oFleteKontabel = new clsKokaFleteKontabel();
            oArkiva = new colArkiva();
            this.HfArkiva = hfArkiva;
            clsMesazh mesazh = kontrollo(rm, ci);
            if (!mesazh.Status)
                return mesazh;
            return new clsMesazh(true, "Dokumenti  u krijua me sukses!");
        }

        private clsMesazh kontrollo(ResourceManager rm, CultureInfo ci)
        {
            if (nrDok == "")
                return new clsMesazh(false, rm.GetString("nrDokSmundTeJeteBosh", ci));
            if (dtDok == null || dtDok.ToShortDateString() == "01/01/0100")
                return new clsMesazh(false, rm.GetString("msgZgjidhniDatenEDokumentit", ci));
            if (dtRegjistrimi == null || dtRegjistrimi.ToShortDateString() == "01/01/0100")
                return new clsMesazh(false, rm.GetString("msgZgjidhniDatenERegjistrimit", ci));
            if (!clsMonedha.ekziston(kodMonedha, idNdermarje))
                return new clsMesazh(false, rm.GetString("msgMonedhaNukEkziston", ci));

            return new clsMesazh(true, "Kontrollet u kaluan me sukses!");
        }

        /// <summary>
        /// tregon nese eshte i lidhur apo jo list pagesa
        /// </summary>
        /// <returns></returns>
        public bool eshteILidhur()
        {
            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();
            bool lidhur = dbAdmin.eshteDokumentiILidhur(idKoka, idNivel, "T_KOKALISTPAGESE", "IDKOKA");
            dbAdmin.Dispose();
            return lidhur;
        }

        /// <summary>
        /// merr dokumentat qe e kane lidhur kete listpagese
        /// </summary>
        /// <returns></returns>
        public DataTable merrIdsDokLidhur()
        {
            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();
            DataTable dt = dbAdmin.MerrDokLidhur(idKoka, idNivel, "T_KOKALISTPAGESE", "IDKOKA");
            dbAdmin.Dispose();
            return dt;
        }

        public static clsMesazh ruajListPageseNew(out int idkoka, int idNiv, int idKonf, int iddep, int idnendep,
            DateTime dtDk, string nrDk, int muaji, decimal totali, int idmonedha, int iddoknga, decimal kursi, int idSt,
            int idNder, int idNdVt, int idPer, DateTime dtRegj, string shenim, int idNivelGjenerues,
            int idKonfigGjenerues, int idGjenerues, colTrupiListPagese oColTrupi, clsKokaFleteKontabel oFleteKontabel,
            int meKontabilizim, clsDatabazeListPagesa dbRegj, int idDokNgaFK, ResourceManager rm, CultureInfo ci,
            int idskema, StatusAprovimi statusAprovimi, int idetapa, string serverUrl, bool modifikim,
            decimal totalindermarjes)
        {

            idkoka = 0;
            try
            {
                clsMesazh mesazh = dbRegj.ruajKokaListPagese(out idkoka, idNiv, idKonf, iddep, idnendep, dtDk, nrDk,
                    muaji, totali, idmonedha, iddoknga, kursi, idSt, idNder, idNdVt, idPer, dtRegj, shenim,
                    idNivelGjenerues, idKonfigGjenerues, idGjenerues, statusAprovimi, totalindermarjes);
                if (!mesazh)
                    return new clsMesazh(false, rm.GetString("msgNdodhiNjeGabimGjateRuajtjesSeListpageses", ci));

                oColTrupi.VendosIdKokeNeTrup(idkoka);
                mesazh = RuajTrupListPagesa(oColTrupi, dbRegj);
                if (!mesazh.Status)
                    return mesazh;

                if (idSt == 1 && meKontabilizim != 0)
                {
                    oFleteKontabel.IdDokNga = idDokNgaFK;
                    oFleteKontabel.IdGjenerues = idkoka;
                    oFleteKontabel.NrDukumentiKokaFleteKontabel = nrDk;

                    oFleteKontabel.Kontabilizuar = (meKontabilizim == 1);
                    clsDatabaseKontabilitet dbKont = new clsDatabaseKontabilitet(dbRegj);
                    clsMesazh kontMesazh = oFleteKontabel.Ruaj(dbKont);
                    if (!kontMesazh.Status)
                    {
                        return new clsMesazh(kontMesazh.Status, kontMesazh.PershkrimMesazhi);
                    }
                }
                clsDatabaseRegjistrim dbr = new clsDatabaseRegjistrim(dbRegj);
                clsEtapeAprovimi etape = new clsEtapeAprovimi();
                mesazh = etape.ruaj(idPer, serverUrl, idskema, dbr, iddoknga, idPer, statusAprovimi, idetapa, 38,
                    idSt, idNder, statusAprovimi, 0, idkoka, idPer, double.Parse(totali.ToString()), DateTime.Now,
                    idKonf, nrDk, dtDk);
                if (!mesazh.Status)
                    return mesazh;
                if (!modifikim || idSt != 1) return mesazh;
                mesazh = dbr.modifikoEtapeIdkoka(iddoknga, idkoka);
                //modifikojme id e kokes se shitjes tek etapat pasi aprovohet dhe ruhet
                return mesazh;
            }
            catch (Exception ce)
            {
                return new clsMesazh(false, ce.Message);
            }
        }

        private static clsMesazh RuajTrupListPagesa(colTrupiListPagese trupi, clsDatabazeListPagesa dbRegj)
        {
            clsMesazh mesazh = new clsMesazh();
            colKompListPagese colKomp = trupi.MerrListenKompListPagese();
            colKomponenteMuaji colKompMuaji = colKomp.MerrListenEPloteKomponenteMuaji();

            //krijojme tre dt nga collection-et dhe i kalojme si parameter ne sp
            DataTable dtTrupi = trupi.ToDataTable("IdTrupi", "IdKoka", "IdPunonjes", "Paguar", "Shenime", "Cost");
            DataTable dtKomp = colKomp.ToDataTable("IdKompListPagese", "IdTrupi", "IdKomponentePage", "VleraParam", "Vlera", "Shenime", "Modifikuar");
            DataTable dtKompMuaj = colKompMuaji.ToDataTable("Id", "IdKompListPagese", "VleraParam", "Vlera", "Muaji", "Viti");
            mesazh = dbRegj.RuajTrupListPagese(dtTrupi, dtKomp, dtKompMuaj);
            return mesazh;
        }

        /// <summary>
        /// Ruan objektin e  kokes se dokumentit te list pagess ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbRegjistrim.clsKokalistpagese.ruajListPagese"/> 
        /// </summary>
        /// <param name="meKontabilizim">tregon nese te konfigurimet  eshte me kontabilizim apo jo</param>
        /// <param name="hfNrAutoregjistrime">hiddenField i Devit qe mban numrat automatike,null nese nuk perodren</param>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
        public clsMesazh ruaj(int meKontabilizim, IDictionary<string, object> hfNrAutoregjistrime, ResourceManager rm, CultureInfo ci, int idskema, StatusAprovimi statusAprovimi, int idetapa, string serverUrl)
        {
            using (var scope = new MyTransactionScope())
            using (var db = new clsDatabazeListPagesa())
            {
                int idDokNgaFK = -1;
                clsMesazh u_ruajt = ruaj(meKontabilizim, hfNrAutoregjistrime, idDokNgaFK, db, rm, ci, idskema,
                    statusAprovimi, idetapa, serverUrl);
                if (!u_ruajt)
                    return u_ruajt;

                clsMesazh msg = db.kaloDokumentListPageseNeHistorik(IdKoka, IdPerdoruesi, IdStatusDok);
                if (!msg)
                    return msg;
                if (HfArkiva != null)
                    u_ruajt = colArkiva.RuajArkiven(IdKoka, 38, IdPerdoruesi, IdNdermarje, HfArkiva);
                scope.Complete();                
                return u_ruajt;
            }
        }

        /// <summary>
        /// Ruan objektin e  kokes se dokumentit te list pageses ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbRegjistrim.clsKokalistpagese.ruajMagazina"/> 
        /// </summary>
        /// <param name="eshteTransferim">tregon nese dokumenti i regjistruar eshte transferim apo jo</param>
        /// <param name="meKontabilizim">tregon nese te konfigurimet magazina eshte me kontabilizim apo jo</param>
        /// <param name="hfNrAutoregjistrime">hiddenField i Devit qe mban numrat automatike,null nese nuk perodren</param>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
        public clsMesazh ruaj(int meKontabilizim, IDictionary<string, object> hfNrAutoregjistrime, int idDokNgaFK, clsDatabazeListPagesa db, ResourceManager rm, CultureInfo ci, int idskema, StatusAprovimi statusAprovimi, int idetapa, string serverUrl)
        {
            bool kaNdryshimNumri;
            clsMesazh mesazhKontrolli = kontrolloListPagese(out kaNdryshimNumri, db, hfNrAutoregjistrime,ci,rm);
            if (!mesazhKontrolli.Status)
            {
                return mesazhKontrolli;
            }
            int id = 0;
            clsMesazh uRuajt = ruajListPageseNew(out id, IdNivel, IdKonfigAmbjente, idDepartamenti, IdNenDepartamenti, DtDok, NrDok, Muaji, Totali, IdMonedha, IdDokNga, Kursi, IdStatusDok, IdNdermarje, IdNderViti, IdPerdoruesi, DtRegjistrimi, Shenime, IdNivelGjenerues, IdKonfigGjenerues, IdGjenerues, OColTrupi, OFleteKontabel, meKontabilizim, db, idDokNgaFK, rm, ci, idskema, statusAprovimi, idetapa, serverUrl, false, totaliNdermarje);
            idKoka = id;

            if (!uRuajt.Status)
            {
                return uRuajt;
            }
            if (kaNdryshimNumri)
                return mesazhKontrolli;
            return uRuajt;
        }

        /// <summary>
        /// kontrollon te dhenat e listpageses
        /// </summary>
        /// <param name="kaNdryshimNumri">tregon ne eshte ndryshuar nr per te shfaqur mesazhin</param>
        /// <param name="dbRegj"> data baze regjistrimi</param>
        /// <param name="hfNrAutoregjistrime"> nr auomatik</param>
        /// <returns></returns>
        private clsMesazh kontrolloListPagese(out bool kaNdryshimNumri, clsDatabazeListPagesa dbRegj, IDictionary<string, object> hfNrAutoregjistrime,CultureInfo ci, System.Resources.ResourceManager rm)
        {
            
            kaNdryshimNumri = false;
            if (nrDok == "")
                return new clsMesazh(false, rm.GetString("nrDokSmundTeJeteBosh", ci));
            clsMesazh mes = new clsMesazh();
            if (hfNrAutoregjistrime != null)
            {
                mes = kontrolloNrAutoListPagese(out kaNdryshimNumri, dbRegj, hfNrAutoregjistrime);
                if (!mes.Status)
                    return mes;
            }
            if (dbRegj.ekzistonRegjistrimListPagese(nrDok, dtDok, idNdermarje))
                return new clsMesazh(false, rm.GetString("msgEkziston1LpNumerDokumenti", ci));
            if (kaNdryshimNumri)
                return mes;
            return new clsMesazh(true, "Kontrolli i list pageses u krye me sukses!");
        }

        public static bool KaTeDhenaPerTeMarre(int idkoka)
        {
            using (clsDatabazeListPagesa db = new clsDatabazeListPagesa())
            {
                return db.KaTeDhenaPerTeMarre(idkoka);
            }
        }
        public static int ktheIdStatusDok(int idkoka)
        {
            using (clsDatabazeListPagesa dbRegj = new clsDatabazeListPagesa())
            {
                return dbRegj.ktheIdStatusDokKokaListpag(idkoka);
            }
        }

        public static bool ekzistonListePagesa(DateTime dtdok, DateTime dtpasaredhese, int idndermarrje)
        {
            using (clsDatabazeListPagesa dbRegj = new clsDatabazeListPagesa())
            {
                return dbRegj.ekzistonListepagesa(dtdok, dtpasaredhese, idndermarrje);
            }
        }
        /// <summary>
        /// kontrollo nese per punonejsi jane bere listpagesa midsi datave ne parameter
        /// </summary>
        /// <param name="dtdok"></param>
        /// <param name="dtpasaredhese"></param>
        /// <param name="idPunonjesi"></param>
        /// <returns></returns>
        public static bool ekzistonListePagesaPerPunonjes(DateTime dtdok, DateTime dtpasaredhese, int idPunonjesi)
        {
            using (clsDatabazeListPagesa dbRegj = new clsDatabazeListPagesa())
            {
                return dbRegj.ekzistonListePagesaPerPunonjes(dtdok, dtpasaredhese, idPunonjesi);
            }
        }
        public static bool ekzistonListePagesaPerPunonjes(string nrPersonal, int idNdermarrje, int viti, int muaji)
        {
            using (var db = new clsDatabazeListPagesa())
                return db.ekzistonListePagesaPerPunonjesSipasNrPersonal(nrPersonal, idNdermarrje, viti, muaji);
            
        }
        /// <summary>
        /// kontrollon nr automatik te listpageses
        /// </summary>
        /// <param name="kaNdryshimNumri"></param>
        /// <param name="dbRegj"></param>
        /// <param name="hfregjistrime"></param>
        /// <returns></returns>
        private clsMesazh kontrolloNrAutoListPagese(out bool kaNdryshimNumri, clsDatabazeListPagesa dbRegj, IDictionary<string, object> hfregjistrime)
        {
            clsDatabaseAdmin db = new clsDatabaseAdmin(dbRegj);
            List<NrAuto> list = clsNrAutom.kontrollogjithenumrat(db, hfregjistrime, dtDok);
            if (NrAuto.ktheVlerenEre(list, "NrDok") != "")
                nrDok = NrAuto.ktheVlerenEre(list, "NrDok");
            clsMesazh mes = NrAuto.ruajvlera(out kaNdryshimNumri, list, dtDok, idPerdoruesi, idNdermarje, db);
            return new clsMesazh(mes.Status, mes.PershkrimMesazhi);
        }

        /// <summary>
        /// Modifikon nje objekt dokument listpagese sebashku me te trupin dhe kontabilitetin
        /// Nje objekt dokument listpagese ka nje koleksion me trupin dhe kontabilitetin perkates , 
        /// modifikimi e nje dokumenti imponon modifikimin edhe te nje colection-i me trupin dhe kontabilitetin
        /// Mqs cdo rresht i ri qe modifikon ne DB kerkon thirrjen e nje SP-je me parametra dhe dokumenti i listpageses bashke me trupin dhe kontabilitetin konsiderohet si nje regjistrim,
        /// perdoret nje transaksion qe imponon modifikimin e rregullt te nje te nje dokumenti listpagese sebashku me trupin dhe kontabilitetin
        /// 1. merret dokumenti eksitues  i listpageses dhe kalohet ne gjendjen 20 te modifikimit
        /// 2. ruhet dokumenti i ri i listpageses se bashku me trupin dhe kontabilitetin
        /// 3. stornohen kontabiliteti i dokumentave eksistues  te listpageses
        /// </summary>
        /// <param name="dtDk"> data e dokumentit te listpageses</param>
        /// <param name="dtRegj"> data e regjistrimit te dokumentit te listpageses</param>
        /// <param name="idKDok"> id kategorise se dokumentit me te cilin lidhet.</param>
        /// <param name="idKlFurn"> id e klient furnitorit me te cilin lidhet</param>
        /// <param name="idKonf"> id e konfigurimit te dokumentit</param>
        /// <param name="idLidhes">id e dokumentit nga e cila gjenerohet ne rastet e modifikimit </param>
        /// <param name="idMag">id e listpageses</param>
        /// <param name="idMagKoka">id ritese e kokes se listpageses</param>
        /// <param name="idNder"> id e ndermarjes</param>
        /// <param name="idNdVt"> id e ndermarje vitit</param>
        /// <param name="idNiv"> id e nivelit te regjistrimit</param>
        /// <param name="idPer"> id e perdoruesit se e ben regjistrimin</param>
        /// <param name="idProj"> id e projektit ne te cilin ben pjese</param>
        /// <param name="idSt"> id e statusit te dokumentit</param>
        /// <param name="nrDk">nr i dokumentit</param>
        /// <param name="nrProj"> nr i projektit</param>
        /// <param name="shenim"> shenime </param>
        /// <param name="vl"> vlera totale e dokumentit</param>
        /// <param name="idGjenerues">id e dokumentit nga gjenerohet magazina nga nje ambjent tjeter</param>
        /// <param name="idKonfigGjenerues">id e konfigurimit nga gjenerohet dokumenti</param>
        /// <param name="idNivelGjenerues">id e nivelit nga gjenerohet dokumenti</param>
        /// <param name="oColTrupiLP">kolektion i trupit te listpageses</param>
        /// <param name="oFleteKontabel">kolektion i fleteve kontabel</param>
        /// <param name="oMagazinaTransferim">kolektion i transferimit ne listpagese</param>
        ///<param name="meKontabilizim"> tregon nese dokumenti i listpageses do te kontabilizohet apo jo</param>
        /// <returns> kthen nje obj clsMesazh per te identifikuar statusin e modifikimit se te dhenave ne DB</returns>
        public static clsMesazh modifikoListPagese(int idkoka, int idNiv, int idKonf, int iddep, int idnendep, DateTime dtDk, string nrDk, int muaji, decimal totali, int idmonedha, int iddoknga, decimal kursi, int idSt, int idNder, int idNdVt, int idPer, DateTime dtRegj, string shenim, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues, colTrupiListPagese oColTrupiLP, clsKokaFleteKontabel oFleteKontabel, int meKontabilizim, clsDatabazeListPagesa dbListPagesa, out int idre, ResourceManager rm, CultureInfo ci, int idskema, StatusAprovimi statusAprovimi, int idetapa, string serverUrl, decimal totalindermarje)
        {
            idre = 0;
            clsMesazh mesazh = new clsMesazh();
            clsMesazh mesazhKont = new clsMesazh(true);
            try
            {
                clsKokaListPagese kokaEkzistuese = new clsKokaListPagese(idkoka, false, dbListPagesa);
                if (string.IsNullOrEmpty(kokaEkzistuese.NrDok) || kokaEkzistuese.IdStatusDok == 2)
                {
                    return new clsMesazh(false, rm.GetString("msgDokumentiKaNdryshuarHapeniPerseri", ci));
                }
                DbQendraKosto.clsDatabaseQendraKosto dbqendra = new DbQendraKosto.clsDatabaseQendraKosto(dbListPagesa);
                /*       kokaEkzistuese.IdStatusDok = 2; */ //duhet vendosur nje status i pershtatshem per kete modifikim
                iddoknga = kokaEkzistuese.idKoka;

                clsDatabaseKontabilitet dbkontab = new clsDatabaseKontabilitet(dbListPagesa);
                clsKokaFleteKontabel newclsKokaFleteKontabel = new clsKokaFleteKontabel(kokaEkzistuese.IdKoka, 38, dbkontab);
                //nese ka kontabilitet merr qendrat e kostos
                if (!string.IsNullOrEmpty(newclsKokaFleteKontabel.NrDukumentiKokaFleteKontabel))
                {
                    kokaEkzistuese.OFleteKontabel = newclsKokaFleteKontabel;
                    DbQendraKosto.clsKokaQendraKosto kokaqendra = new DbQendraKosto.clsKokaQendraKosto();
                    kokaqendra.KtheKokaQKSipasIDGjeneruesDheKonfigMeTrup(newclsKokaFleteKontabel.IdKokaFleteKontabel, newclsKokaFleteKontabel.IdKonfigAmbjente, dbqendra);
                    kokaEkzistuese.OFleteKontabel.KokaQendraKosto = (kokaqendra.IdKoka > 0) ? kokaqendra : new DbQendraKosto.clsKokaQendraKosto();
                }
                else
                    kokaEkzistuese.OFleteKontabel = new clsKokaFleteKontabel();

                oFleteKontabel.IdDokNga = kokaEkzistuese.OFleteKontabel.IdKokaFleteKontabel;
                oFleteKontabel.KokaQendraKosto.IdDokNga = kokaEkzistuese.OFleteKontabel.KokaQendraKosto.IdKoka;

                mesazh = dbListPagesa.fshiKokaListPagese(idkoka);

                if (!mesazh.Status)
                    return mesazh;

                mesazh = ruajListPageseNew(out idkoka, idNiv, idKonf, iddep, idnendep, dtDk, nrDk, muaji, totali,
                    idmonedha, kokaEkzistuese.idKoka, kursi, idSt, idNder, idNdVt, idPer, dtRegj, shenim, idNivelGjenerues,
                    idKonfigGjenerues, idGjenerues, oColTrupiLP, oFleteKontabel, meKontabilizim, dbListPagesa,
                    kokaEkzistuese.OFleteKontabel.IdKokaFleteKontabel, rm, ci, idskema, statusAprovimi, idetapa, serverUrl, true, totalindermarje);

                if (!mesazh.Status)
                    return mesazh;

                idre = idkoka;

                if (kokaEkzistuese.OFleteKontabel.IdKokaFleteKontabel != 0)
                    mesazhKont = kokaEkzistuese.OFleteKontabel.ModifikoFleteKontabel(true, dbkontab);
                if (mesazhKont.Status)
                    return mesazh;

                mesazh.Status = false;
                mesazh.PershkrimMesazhi = mesazhKont.PershkrimMesazhi;
                return mesazh;
            }
            catch (Exception ce)
            {
                return new clsMesazh(false, ce.Message);
            }
        }

        /// <summary>
        /// Modifikon objektin e  kokes se dokumentit te listpageses ne tabelen perkatese ne databaze.Therret funksionin
        /// </summary>
        /// <param name="meKontabilizim">tregon nese te konfigurimet magazina eshte me kontabilizim apo jo</param>
        /// <param name="lidhur">tregon nese eshte i lidhur ose jo</param>
        /// <returns > nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo</returns>
        public clsMesazh modifiko(int meKontabilizim, bool lidhur, ResourceManager rm, CultureInfo ci, int idskema, StatusAprovimi statusAprovimi, int idetapa, string serverUrl)
        {
            clsMesazh u_modifikua;
            using (var scope = new MyTransactionScope()) {

                
                try
                {
                    clsDatabazeListPagesa data = new clsDatabazeListPagesa();
                    clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim(data);
                    int idKokaEkzistuese = IdKoka;
                    if (!lidhur)
                    {

                        int idkokare = 0;
                        u_modifikua = modifikoListPagese(IdKoka, IdNivel, IdKonfigAmbjente, idDepartamenti, idNenDepartamenti, DtDok, NrDok, Muaji, Totali, IdMonedha, IdDokNga, Kursi, IdStatusDok, idNdermarje, idNderViti, idPerdoruesi, DtRegjistrimi, Shenime, IdNivelGjenerues, IdKonfigGjenerues, IdGjenerues, OColTrupi, OFleteKontabel, meKontabilizim, data, out idkokare, rm, ci, idskema, StatusAprovimi, idetapa, serverUrl, totaliNdermarje);
                        idKoka = idkokare;
                        if (!u_modifikua)
                            throw new MyException(u_modifikua.PershkrimMesazhi);
                    }
                    else
                    {
                        u_modifikua = data.modifikoKokaListPagese(IdKoka, IdNivel, IdKonfigAmbjente, idDepartamenti, idNenDepartamenti, DtDok, NrDok, Muaji, Totali, IdMonedha, IdDokNga, Kursi, IdStatusDok, idNdermarje, idNderViti, idPerdoruesi, DtRegjistrimi, Shenime, IdNivelGjenerues, IdKonfigGjenerues, IdGjenerues, StatusAprovimi, totaliNdermarje);
                        if (!u_modifikua)
                            throw new MyException(u_modifikua.PershkrimMesazhi);
                        else
                            u_modifikua.PershkrimMesazhi = rm.GetString("msgModifikimiMeSukses", ci);

                        if (statusAprovimi != StatusAprovimi.Undefined)

                        {
                            u_modifikua = new clsEtapeAprovimi().ruaj(idPerdoruesi, serverUrl, idskema, dbRegj, idKoka, IdPerdoruesi, statusAprovimi, idetapa, 38, this.idStatusDok, this.idNdermarje, this.statusAprovimi, 0, this.idKoka, this.idPerdoruesi, Double.Parse(this.totali.ToString()), this.dtKrijimi, this.idKonfigAmbjente, this.nrDok, this.dtDok);

                            if (!u_modifikua)
                                throw new MyException(u_modifikua.PershkrimMesazhi);
                        }
                    }

                    clsMesazh msg = data.kaloDokumentListPageseNeHistorik(IdKoka, IdPerdoruesi, IdStatusDok);
                    if (!msg)
                        return msg;
                    if (HfArkiva != null)
                    {
                        u_modifikua = colArkiva.ModifikoArkiven(idKokaEkzistuese, idKoka, 38, idNdermarje, idPerdoruesi);
                        if (!u_modifikua.Status)
                        {
                            return u_modifikua;
                        }
                    }
                    scope.Complete();

                    return u_modifikua;
                }
                catch(MyException ex)
                {
                    return new clsMesazh(false, ex.Message);
                }
                catch (Exception ex)
                {
                    ImbLogger.Error(ex);
                    return new clsMesazh(false, rm.GetString("msgListpagesaNukURuajt", ci));
                }
            }
        }

        /// <summary>
        /// fshin nje objekt dokument listpagese sebashku me te trupin dhe kontabilitetin perkates
        /// Nje objekt dokument listpagese ka nje koleksion me trupin dhe kontabilitetin , 
        /// fshirja e nje dokumenti listpagese imponon fshirjen edhe te nje colection-i me trupin dhe kontabilitetin
        /// Mqs cdo rresht i ri qe fshihet ne DB kerkon thirrjen e nje SP-je me parametra dhe dokumenti i listpageses bashke me trupin dhe kontabilitetin konsiderohet si nje regjistrim,
        /// perdoret nje transaksion qe imponon fshirjen e rregullt te nje te nje dokumenti sebashku me trupin dhe kontabilitetin e tij
        ///1. merr kontabilitetet eksistuese te dokumentit te shitjes dhe listpageses dhe i stornon
        ///2. ben fshirjen e trupit dhe me pas te kokes se dokumentit te listpageses
        /// </summary>
        ///<param name="idkoka"> koka e dokumentit te listpageses i cili do te fshihet</param>
        /// <returns> kthen nje obj clsMesazh per te identifikuar statusin e fshirjes se te dhenave ne DB</returns>
        public static clsMesazh fshiListPagese(int idkoka, int idperdoruesi, clsDatabazeListPagesa db)
        {
            clsMesazh mesazh = new clsMesazh(true);
            clsMesazh mesazhKont = new clsMesazh(true);
            try
            {
                clsDatabaseKontabilitet dbkontab = new clsDatabaseKontabilitet(db);

                clsKokaListPagese kokaEkzistuese = new clsKokaListPagese(idkoka, false);
                clsKokaFleteKontabel newclsKokaFleteKontabel = new clsKokaFleteKontabel(kokaEkzistuese.IdKoka, 38, dbkontab);
                if (newclsKokaFleteKontabel.NrDukumentiKokaFleteKontabel != null)
                {
                    kokaEkzistuese.OFleteKontabel = newclsKokaFleteKontabel;

                }
                else
                    kokaEkzistuese.OFleteKontabel = new clsKokaFleteKontabel();

                if (kokaEkzistuese.OFleteKontabel.IdKokaFleteKontabel != 0)
                {
                    mesazh = kokaEkzistuese.OFleteKontabel.fshiupd(dbkontab);
                    if (!mesazh.Status)
                        return mesazh;
                }

                kokaEkzistuese.IdStatusDok = 2;  //duhet vendosur nje status i pershtatshem per kete modifikim
                mesazh = db.kaloDokumentListPageseNeHistorik(idkoka, idperdoruesi, kokaEkzistuese.IdStatusDok);
                if (!mesazh)
                    return mesazh;
                db.fshiKokaListPagese(idkoka);
                return mesazh;
            }
            catch (Exception ce)
            {
                return new clsMesazh(false, ce.Message);
            }
        }

        /// <summary>
        /// Fshin objektin e  kokes se dokumentit te  ne tabelen perkatese ne databaze.Therret funksionin
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh fshi()
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            //db.krijoManager();
            db.beginTransaksion();
            clsMesazh u_fshi = fshiListPagese(idKoka, idPerdoruesi, db);
            if (u_fshi.Status)
                db.commitTransaksion();
            else db.rollbackTransaksion();
            return u_fshi;
        }



        #endregion

        #region Metoda Internal
        /// <summary>
        /// mbush koken sipas te dhenat nga db
        /// </summary>
        /// <param name="rreshti"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        internal bool mbushKokaListPagese(DataRow rreshti, bool meTrup, clsDatabazeListPagesa db)
        {
            int.TryParse(rreshti["IDKOKA"].ToString(), out idKoka);
            int.TryParse(rreshti["IDNIVEL"].ToString(), out idNivel);
            int.TryParse(rreshti["IDKONFIGAMBJENTE"].ToString(), out idKonfigAmbjente);
            int.TryParse(rreshti["IDDEPARTAMENTI"].ToString(), out idDepartamenti);
            int.TryParse(rreshti["IDNENDEPARTAMENTI"].ToString(), out idNenDepartamenti);
            nrDok = rreshti["NRDOK"].ToString();
            DateTime.TryParse(rreshti["DTDOK"].ToString(), out dtDok);
            int.TryParse(rreshti["MUAJI"].ToString(), out muaji);
            decimal.TryParse(rreshti["TOTALI"].ToString(), out totali);
            decimal.TryParse(rreshti["TOTALINDERMARJE"].ToString(), out totaliNdermarje);
            int.TryParse(rreshti["IDMONEDHA"].ToString(), out idMonedha);
            int.TryParse(rreshti["IDDOKNGA"].ToString(), out idDokNga);
            decimal.TryParse(rreshti["KURSI"].ToString(), out kursi);

            int.TryParse(rreshti["IDSTATUSDOK"].ToString(), out idStatusDok);
            int.TryParse(rreshti["IDNDERMARJE"].ToString(), out idNdermarje);
            int.TryParse(rreshti["IDNDERVITI"].ToString(), out idNderViti);
            int.TryParse(rreshti["IDPERDORUESI"].ToString(), out idPerdoruesi);
            DateTime.TryParse(rreshti["DTREGJ"].ToString(), out dtRegjistrimi);
            shenime = rreshti["SHENIME"].ToString();
            int.TryParse(rreshti["IDNIVELGJENERUES"].ToString(), out idNivelGjenerues);
            int.TryParse(rreshti["IDKONFIGGJENERUES"].ToString(), out idKonfigGjenerues);
            int.TryParse(rreshti["IDGJENERUES"].ToString(), out idGjenerues);
            DateTime.TryParse(rreshti["DTKRIJIMI"].ToString(), out dtKrijimi);
            DateTime.TryParse(rreshti["DTMODIFIKIMI"].ToString(), out dtModifikimi);
            int stap = 0;
            int.TryParse(rreshti["STATUSAPROVIMI"].ToString(), out stap);
            statusAprovimi = (StatusAprovimi)stap;
            oColTrupi = meTrup ? new colTrupiListPagese(idKoka, db) : new colTrupiListPagese();
            //oColTrupi = merrTrupi(db);
            oFleteKontabel = new clsKokaFleteKontabel();
            return true;


        }

        #endregion
    }
}

