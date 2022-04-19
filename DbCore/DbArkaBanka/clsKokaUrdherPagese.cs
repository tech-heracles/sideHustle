using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using DbCore.DbKontabiliteti;
using DbCore.DbAdmin;

namespace DbCore.DbArkaBanka
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne  koken e nje dokumenti te Urdher pageses
    ///  (Te dhenat  merren nga tabela : T_KOKAUrdherPAGESA)
    /// </summary>
    public class clsKokaUrdherPagese
    {
        private const string gabimTeDhena = "ERROR: Gabim gjate marrjes se kokes se Urdher pagesave nga db-ja";
        private const string STR_NdodhiNjeGabimGjateRuajtjesSeKokesSeUrdherPageses = "Ndodhi nje Gabim gjate ruajtjes se Kokes se Urdher Pageses";
        private const string STR_DokumentiKaNdryshuarJuLutemRihapeniPerseri = "Dokumenti ka ndryshuar! Ju lutem rihapeni perseri!";
        private const string STR_NumriIDokumentitNukMundTeJeteBosh = "Numri i dokumentit nuk mund te jete bosh";
        private const string STR_EkzistonNjeRegjistrimMeTeNjejtinNumerDokumenti = "Ekziston nje regjistrim me te njejtin numer dokumenti!";
        private const string STR_KontrolliIUrdherPagesesUKryeMeSukses = "Kontrolli i Urdher pageses u krye me sukses!";
        #region Attributet
        private int idKoka;
        private int idNivel;
        private int idKonfigAmbjente;
        private string nrKuponi;
        private string nrPunonjesve;
        private DateTime dtDok;
        private String nrDok;
        private string emriPerfituesit;
        private decimal totali;
        private string nipti;
        private int idDokNga;
        private string emriBankes;
        private int idStatusDok;
        private int idNdermarje;
        private int idNderViti;
        private int idPerdoruesi;
        private DateTime dtDokNgjitur;
        private string nrLlogBankare;
        private string adresa;
        private int llojDokNgjitur;
        private string nrDokNgjitur;
        private DateTime dtDokAprovimi;
        private int idNivelGjenerues;
        private int idKonfigGjenerues;
        private int idGjenerues;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private colTrupiUrdherPagese oColTrupi;
        private int idRaportDesing;
        private string urdheruesi;
        private string kontabilisti;
        private DataRow rreshti;
        private clsDatabaseArkaBanka db;
        private string nenpunesThesari;
    
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
        /// Kthen/Vendos nr kuponi.
        /// </summary>
        public string NrKuponi
        {
            get { return nrKuponi; }
            set { nrKuponi = value; }
        }
        /// <summary>
        /// Kthen/Vendos nr punonjesve
        /// </summary>
        public string NrPunonjesve
        {
            get { return nrPunonjesve; }
            set { nrPunonjesve = value; }
        }
        /// <summary>
        /// Kthen/Vendos Emri i perfituesit
        /// </summary>
        public string EmriPerfituesit
        {
            get { return emriPerfituesit; }
            set { emriPerfituesit = value; }
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
        /// Kthen/Vendos nrLlogBankare.
        /// </summary>
        public String NrLlogBankare
        {
            get { return nrLlogBankare; }
            set { nrLlogBankare = value; }
        }
        /// <summary>
        /// Kthen/Vendos nipti.
        /// </summary>
        public string Nipti
        {
            get { return nipti; }
            set { nipti = value; }
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
        /// Kthen/Vendos emriBankes
        /// </summary>
        public string EmriBankes
        {
            get { return emriBankes; }
            set { emriBankes = value; }
        }
        /// <summary>
        /// kthen/vendos adresen
        /// </summary>
        public string Adresa
        {
            get
            {
                return adresa;
            }
            set
            {
                adresa = value;
            }
        }
        /// <summary>
        /// kthen vendos llojin e dokumentit ngjitur
        /// <example>1-cek,2-xhirim,3-te tjera</example>
        /// </summary>
        public int LlojDokNgjitur
        {
            get
            {
                return llojDokNgjitur;
            }
            set
            {
                llojDokNgjitur = value;
            }
        }
        /// <summary>
        /// nr i dokumentit ngjitur
        /// </summary>
        public string NrDokNgjitur
        {
            get
            {
                return nrDokNgjitur;
            }
            set
            {
                nrDokNgjitur = value;
            }
        }
        /// <summary>
        /// kthen vendos dt e aprovimit
        /// </summary>
        public DateTime DtDokAprovimi
        {
            get
            {
                return dtDokAprovimi;
            }
            set
            {
                dtDokAprovimi = value;
            }
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
        /// Kthen/Vendos dt e dokumentit ngjitur.
        /// </summary>
        public DateTime DtDokNgjitur
        {
            get { return dtDokNgjitur; }
            set { dtDokNgjitur = value; }
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
        /// Kthen/Vendos  nje koleksion me trupin e dokumentit te Urdherpageses
        /// </summary>
        public colTrupiUrdherPagese OColTrupi
        {
            get { return oColTrupi; }
            set { oColTrupi = value; }
        }

        /// <summary>
        /// data e krijimit
        /// </summary>
        public DateTime DtKrijimi
        {
            get { return dtKrijimi; }

        }
        /// <summary>
        /// data e fundit e modifikimit
        /// </summary>
        public DateTime DtModifikimi
        {
            get { return dtModifikimi; }

        }

        public int IdRaportDesing
        {
            get { return idRaportDesing; }
            set { idRaportDesing = value; }
        }
        /// <summary>
        /// kthen/vendos urdheruesin
        /// </summary>
        public string Urdheruesi
        {
            get { return urdheruesi; }
            set { urdheruesi = value; }
        }  /// <summary>
           /// kthen/vendos nenpunesin e thesarit
           /// </summary>
        public string NenpunesThesari
        {
            get { return nenpunesThesari; }
            set { nenpunesThesari = value; }
        }
        /// <summary>
        /// kthen/vendos kontabilistin
        /// </summary>
        public string Kontabilisti
        {
            get { return kontabilisti; }
            set { kontabilisti = value; }
        }
        #endregion

        #region Konstruktoret
        /// <summary>
        /// Konstruktori i klases me parametra
        /// </summary>
        /// <param name="dtDk"> data e dokumentit </param>
        /// <param name="dtdokngjitur"> data e dokumentit ngjitur </param>
        /// <param name="nipt"> nipti</param>
        /// <param name="nrkuponi"> nr kuponi</param>
        /// <param name="idKonf"> id e konfigurimit te dokumentit</param>
        /// <param name="iddoknga">id e dokumentit nga e cila gjenerohet ne rastet e modifikimit </param>
        /// <param name="nrpunonjesve">nr i punonjesve</param>
        /// <param name="idkoka">id ritese e kokes </param>
        /// <param name="idNder"> id e ndermarjes</param>
        /// <param name="idNdVt"> id e ndermarje vitit</param>
        /// <param name="idNiv"> id e nivelit te regjistrimit</param>
        /// <param name="idPer"> id e perdoruesit se e ben regjistrimin</param>
        /// <param name="emriperfituesit"> emri i perfituesit</param>
        /// <param name="idSt"> id e statusit te dokumentit</param>
        /// <param name="nrDk">nr i dokumentit</param>
        /// <param name="total">totali i dok</param>
        /// <param name="nrllogbankare"> nrllogbankare </param>
        /// <param name="emribankes"> emri i bankes</param>
        /// <param name="idGjenerues">id e dokumentit nga gjenerohet magazina nga nje ambjent tjeter</param>
        /// <param name="idKonfigGjenerues">id e konfigurimit nga gjenerohet dokumenti</param>
        /// <param name="idNivelGjenerues">id e nivelit nga gjenerohet dokumenti</param>
        /// <param name="adres"> adresa</param>
        /// <param name="dtdokaprovimi"> dt e aprovimit</param>
        /// <param name="llojdok">lloj dokumentit ngjitur</param>
        /// <param name="nrdokngjitur"> nr i dokumentit ngjitur</param>
        /// <param name="idRaportDesing"></param>
        public clsKokaUrdherPagese(int idkoka, int idNiv, int idKonf, string nrkuponi, string nrpunonjesve, DateTime dtDk, string nrDk, string emriperfituesit, decimal total, string nipt, int iddoknga, string emribankes, int idSt, int idNder, int idNdVt, int idPer, DateTime dtdokngjitur, string nrllogbankare, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues, string adres, int llojdok, string nrdokngjitur, DateTime dtdokaprovimi, int idRaportDesing, string urdheruesi, string kontabilisti, string nenpunesThesari)
        {
            idKoka = idkoka;
            idNivel = idNiv;
            idKonfigAmbjente = idKonf;
            nrKuponi = nrkuponi;
            nrPunonjesve = nrpunonjesve;
            nrDok = nrDk;
            dtDok = dtDk;
            emriPerfituesit = emriperfituesit;
            totali = total;
            nipti = nipt;
            idDokNga = iddoknga;
            emriBankes = emribankes;
            idStatusDok = idSt;
            idNdermarje = idNder;
            idNderViti = idNdVt;
            idPerdoruesi = idPer;
            dtDokNgjitur = dtdokngjitur;
            nrLlogBankare = nrllogbankare;
            adresa = adres;
            llojDokNgjitur = llojdok;
            nrDokNgjitur = nrdokngjitur;
            dtDokNgjitur = dtdokngjitur;
            dtDokAprovimi = dtdokaprovimi;
            this.idRaportDesing = idRaportDesing;
            this.idNivelGjenerues = idNivelGjenerues;
            this.idKonfigGjenerues = idKonfigGjenerues;
            this.idGjenerues = idGjenerues;
            this.urdheruesi = urdheruesi;
            this.kontabilisti = kontabilisti;
            this.nenpunesThesari = nenpunesThesari;
            oColTrupi = new colTrupiUrdherPagese();
        }

        /// <summary>
        /// konstruktor me 3 parametra
        /// </summary>
        /// <param name="idNivel">id e nivelit</param>
        /// <param name="nrdok">nr i dokumentit</param>
        /// <param name="dtdok">data e dokumentit</param>
        public clsKokaUrdherPagese(int idNivel, string nrdok, DateTime dtdok)
        {
            clsDatabaseArkaBanka dbKokaMagazina = new clsDatabaseArkaBanka();
            mbushKokaUrdherPagese(dbKokaMagazina.ktheKokaUrdherPageseSipasIdNivelNrDokDtDok(idNivel, nrdok, dtdok), dbKokaMagazina);
            dbKokaMagazina.Dispose();
        }

        /// <summary>
        /// konstruktori me nje param
        /// </summary>
        /// <param name="idkoka">id koka</param>
        public clsKokaUrdherPagese(int idkoka)
        {
            clsDatabaseArkaBanka dbKokaMagazina = new clsDatabaseArkaBanka();
            mbushKokaUrdherPagese(dbKokaMagazina.ktheKokaUrdherPageseSipasID(idkoka), dbKokaMagazina);
            dbKokaMagazina.Dispose();
        }

        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsKokaUrdherPagese()
        {
        }

        public clsKokaUrdherPagese(DataRow rreshti, clsDatabaseArkaBanka db)
        {
            
            mbushKokaUrdherPagese(rreshti, db);
        }
        #endregion

        #region Metoda Publike

        public clsMesazh krijoUrdherPagese(int idNiv, int idKonf, string nrkuponi, string nrpunonjesve, DateTime dtDk, string nrDk, string emriperfituesit, decimal total, string nipt, int iddoknga, string emribankes, int idSt, int idNder, int idNdVt, int idPer, DateTime dtdokngjitur, string nrllogbankare, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues, string adres, int llojdok, string nrdokngjitur, DateTime dtdokaprovimi, int idRaportDesing, string urdheruesi, string kontabilisti, string nenpunesThesari, colTrupiUrdherPagese coltrupi)
        {
            idNivel = idNiv;
            idKonfigAmbjente = idKonf;
            nrKuponi = nrkuponi;
            nrPunonjesve = nrpunonjesve;
            nrDok = nrDk;
            dtDok = dtDk;
            emriPerfituesit = emriperfituesit;
            totali = total;
            nipti = nipt;
            idDokNga = iddoknga;
            emriBankes = emribankes;
            idStatusDok = idSt;
            idNdermarje = idNder;
            idNderViti = idNdVt;
            idPerdoruesi = idPer;
            dtDokNgjitur = dtdokngjitur;
            nrLlogBankare = nrllogbankare;
            adresa = adres;
            llojDokNgjitur = llojdok;
            nrDokNgjitur = nrdokngjitur;
            dtDokNgjitur = dtdokngjitur;
            dtDokAprovimi = dtdokaprovimi;
            this.idRaportDesing = idRaportDesing;
            this.idNivelGjenerues = idNivelGjenerues;
            this.idKonfigGjenerues = idKonfigGjenerues;
            this.idGjenerues = idGjenerues;
            this.urdheruesi = urdheruesi;
            this.kontabilisti = kontabilisti;
            this.nenpunesThesari = nenpunesThesari;
            oColTrupi = coltrupi;
            return new clsMesazh(true, "Dokumenti u krijua me sukses!");

        }

        /// <summary>
        /// tregon nese eshte i lidhur apo jo Urdher pagesa
        /// </summary>
        /// <returns></returns>
        public bool eshteILidhur()
        {
            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();
            bool eshteILidhur = dbAdmin.eshteDokumentiILidhur(idKoka, idNivel, "T_KOKAURDHERPAGESA", "IDKOKA");
            dbAdmin.Dispose();
            return eshteILidhur;
        }

        /// <summary>
        /// merr dokumentat qe e kane lidhur kete Urdherpagese
        /// </summary>
        /// <returns></returns>
        public DataTable merrIdsDokLidhur()
        {
            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();
            DataTable tabela = dbAdmin.MerrDokLidhur(idKoka, idNivel, "T_KOKAURDHERPAGESA", "IDKOKA");
            dbAdmin.Dispose();
            return tabela;
        }

        /// <summary>
        /// Ruan nje objekt dokumenti Urdherpagese sebashku me trupin  
        /// Nje objekt koka dokumenti Urdherpagese ka nje koleksion me trupin e dokumentit   
        /// ruajtja e nje dokumenti imponon ruajtjen edhe te nje colection-i me trupin 
        /// Mqs cdo rresht i ri qe shtohet ne DB kerkon thirrjen e nje SP-je me parametra dhe koka e dokumentit bashke me trupin  si nje regjistrim,
        /// perdoret nje transaksion qe imponon regjistrimin e rregullt te nje te nje dokumenti Urdherpagese sebashku me trupin 
        /// </summary>
        /// <param name="dtDk"> data e dokumentit te Urdherpagese</param>
        /// <param name="dtdokngjitur"> data e dokumentit ngjitur</param>
        /// <param name="nipti"> nipti.</param>
        /// <param name="nrkuponi"> nr i kuponit</param>
        /// <param name="idKonf"> id e konfigurimit te dokumentit</param>
        /// <param name="iddoknga">id e dokumentit nga e cila gjenerohet ne rastet e modifikimit </param>
        /// <param name="nrpunonjesve">nr i punonjesve</param>
        /// <param name="idkoka">id ritese e kokes se Urdher pageses</param>
        /// <param name="idNder"> id e ndermarjes</param>
        /// <param name="idNdVt"> id e ndermarje vitit</param>
        /// <param name="idNiv"> id e nivelit te regjistrimit</param>
        /// <param name="idPer"> id e perdoruesit se e ben regjistrimin</param>
        /// <param name="emriPerfituesit"> emri i perfituesit</param>
        /// <param name="idSt"> id e statusit te dokumentit</param>
        /// <param name="nrDk">nr i dokumentit</param>
        /// <param name="totali"> totali</param>
        /// <param name="nrllogbankare"> nr llog bankare </param>
        /// <param name="emribankes"> emri i bankes</param>
        /// <param name="idGjenerues">id e dokumentit nga gjenerohet magazina nga nje ambjent tjeter</param>
        /// <param name="idKonfigGjenerues">id e konfigurimit nga gjenerohet dokumenti</param>
        /// <param name="idNivelGjenerues">id e nivelit nga gjenerohet dokumenti</param>
        /// <param name="oColTrupi">kolektion i trupit te Urdherpageses</param>
        /// <param name="modifikim"> tregon nqs dokumenti po modifikohet ne menyre qe te mos behet kontrolli nqs ekziston ky dokument per kete ndermarje</param>
        /// <param name="adresa"> adresa</param>
        /// <param name="dbRegj"> clsDatabaseAkraBanka</param>
        /// <param name="dtdokaprovimi">dt e aprovimit</param>
        /// <param name="llojdok"> lloji i dokumentit ngjitur</param>
        /// <param name="nrdokngjitur">nr i dokumentit ngjitur</param>
        /// <returns> kthen nje obj clsMesazh per te identifikuar statusin e ruajtjes se te dhenave ne DB</returns>
        public static clsMesazh ruajUrdherPagese(out int idkoka, int idNiv, int idKonf, string nrkuponi, string nrpunonjesve, DateTime dtDk, string nrDk, string emriPerfituesit, decimal totali, string nipti, int iddoknga, string emribankes, int idSt, int idNder, int idNdVt, int idPer, DateTime dtdokngjitur, string nrllogbankare, string adresa, int llojdok, string nrdokngjitur, DateTime dtdokaprovimi, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues, int idRaportDesing, string urdheruesi, string kontabilisti, string nenpunesThesari, colTrupiUrdherPagese oColTrupi, clsDatabaseArkaBanka dbRegj)
        {
            idkoka = -1;
            clsMesazh mesazh;
            try
            {
                mesazh = dbRegj.ruajKokaUrdherPagese(out idkoka, idNiv, idKonf, nrkuponi, nrpunonjesve, dtDk, nrDk, emriPerfituesit, totali, nipti, iddoknga, emribankes, idSt, idNder, idNdVt, idPer, dtdokngjitur, nrllogbankare, adresa, llojdok, nrdokngjitur, dtdokaprovimi, idNivelGjenerues, idKonfigGjenerues, idGjenerues, idRaportDesing, urdheruesi, kontabilisti, nenpunesThesari);
                if (!mesazh.Status)
                    return new clsMesazh(false, STR_NdodhiNjeGabimGjateRuajtjesSeKokesSeUrdherPageses);
                foreach (clsTrupiUrdherPagese o in oColTrupi)
                {
                    o.IdKoka = idkoka;
                    int idM;
                    mesazh = dbRegj.ruajTrupiUrdherPagese(out idM, o.IdKoka, o.IdGrupi, o.IdTitulli, o.IdKapitulli, o.IdLlogArtikulli, o.IdLlogAnaliza, o.KodProjekti, o.Shuma, o.Objekti);
                    if (!mesazh.Status)
                    {
                        return mesazh;
                    }

                }


                return mesazh;
            }
            catch (Exception ce)
            {
                idkoka = -1;
                return new clsMesazh(false, ce.Message);
            }
        }

        /// <summary>
        /// Ruan objektin e  kokes se dokumentit te Urdher pagess ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsKokaUrdherpagese.ruajUrdherPagese"/> 
        /// </summary>
        /// <param name="hfNrAutoregjistrime">hiddenField i Devit qe mban numrat automatike,null nese nuk perodren</param>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
        public clsMesazh ruaj(IDictionary<string, object> hfNrAutoregjistrime)
        {
            clsDatabaseArkaBanka db = new clsDatabaseArkaBanka();
            db.beginTransaksion();
            clsMesazh u_ruajt = ruaj(hfNrAutoregjistrime, db);
            if (!u_ruajt.Status)
                db.rollbackTransaksion();
            else
                db.commitTransaksion();
            return u_ruajt;
        }

        /// <summary>
        /// Ruan objektin e  kokes se dokumentit te Urdher pageses ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsKokaUrdherpagese.ruajMagazina"/> 
        /// </summary>
        /// <param name="hfNrAutoregjistrime">hiddenField i Devit qe mban numrat automatike,null nese nuk perodren</param>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
        public clsMesazh ruaj(IDictionary<string, object> hfNrAutoregjistrime, clsDatabaseArkaBanka db)
        {
            bool kaNdryshimNumri;
            clsMesazh mesazhKontrolli = kontrolloUrdherPagese(out kaNdryshimNumri, db, hfNrAutoregjistrime);
            if (!mesazhKontrolli.Status)
            {
                return mesazhKontrolli;
            }

            clsMesazh u_ruajt = clsKokaUrdherPagese.ruajUrdherPagese(out idKoka, IdNivel, IdKonfigAmbjente, nrKuponi, NrPunonjesve, DtDok, NrDok, EmriPerfituesit, Totali, Nipti, IdDokNga, EmriBankes, IdStatusDok, IdNdermarje, IdNderViti, IdPerdoruesi
                              , DtDokNgjitur, NrLlogBankare, Adresa, LlojDokNgjitur, NrDokNgjitur, DtDokAprovimi, IdNivelGjenerues, IdKonfigGjenerues, IdGjenerues, idRaportDesing, urdheruesi, kontabilisti, nenpunesThesari, OColTrupi, db);
            if (!u_ruajt.Status)
            {
                return u_ruajt;
            }
            if (kaNdryshimNumri)
                return mesazhKontrolli;
            return u_ruajt;
        }

        /// <summary>
        /// kontrollon te dhenat e Urdherpageses
        /// </summary>
        /// <param name="kaNdryshimNumri">tregon ne eshte ndryshuar nr per te shfaqur mesazhin</param>
        /// <param name="dbRegj"> data baze regjistrimi</param>
        /// <param name="hfNrAutoregjistrime"> nr auomatik</param>
        /// <returns></returns>
        private clsMesazh kontrolloUrdherPagese(out bool kaNdryshimNumri, clsDatabaseArkaBanka dbRegj, IDictionary<string, object> hfNrAutoregjistrime)
        {
            kaNdryshimNumri = false;
            if (nrDok == "")
                return new clsMesazh(false, STR_NumriIDokumentitNukMundTeJeteBosh);
            clsMesazh mes = new clsMesazh();
            if (hfNrAutoregjistrime != null)
            {
                mes = kontrolloNrAutoUrdherPagese(out kaNdryshimNumri, dbRegj, hfNrAutoregjistrime);
                if (!mes.Status)
                    return mes;
            }
            if (dbRegj.ekzistonRegjistrimUrdherPagese(nrDok, dtDok, idNdermarje))
                return new clsMesazh(false, STR_EkzistonNjeRegjistrimMeTeNjejtinNumerDokumenti);
            if (kaNdryshimNumri)
                return mes;
            return new clsMesazh(true, STR_KontrolliIUrdherPagesesUKryeMeSukses);
        }

        /// <summary>
        /// kontrollon nr automatik te Urdherpageses
        /// </summary>
        /// <param name="kaNdryshimNumri"></param>
        /// <param name="dbRegj"></param>
        /// <param name="hfregjistrime"></param>
        /// <returns></returns>
        private clsMesazh kontrolloNrAutoUrdherPagese(out bool kaNdryshimNumri, clsDatabaseArkaBanka dbRegj, IDictionary<string, object> hfregjistrime)
        {
            clsDatabaseAdmin db = new clsDatabaseAdmin(dbRegj);
            List<NrAuto> list = clsNrAutom.kontrollogjithenumrat(db, hfregjistrime, dtDok);
            if (NrAuto.ktheVlerenEre(list, "NrDok") != "")
                nrDok = NrAuto.ktheVlerenEre(list, "NrDok");
            DbCore.clsMesazh mes = NrAuto.ruajvlera(out kaNdryshimNumri, list, dtDok, idPerdoruesi, idNdermarje, db);
            return new clsMesazh(mes.Status, mes.PershkrimMesazhi);
        }

        /// <summary>
        /// Modifikon nje objekt dokument Urdherpagese sebashku me te trupin 
        /// Nje objekt dokument Urdherpagese ka nje koleksion me trupin  , 
        /// modifikimi e nje dokumenti imponon modifikimin edhe te nje colection-i me trupin 
        /// Mqs cdo rresht i ri qe modifikon ne DB kerkon thirrjen e nje SP-je me parametra dhe dokumenti i Urdherpageses bashke me trupin konsiderohet si nje regjistrim,
        /// perdoret nje transaksion qe imponon modifikimin e rregullt te nje te nje dokumenti Urdherpagese sebashku me trupin 
        /// 1. merret dokumenti eksistues  i Urdherpageses dhe kalohet ne gjendjen 20 te modifikimit
        /// 2. ruhet dokumenti i ri i Urdherpageses se bashku me trupin 
        /// </summary>
        /// <param name="dtDk"> data e dokumentit te Urdherpagese</param>
        /// <param name="dtdokngjitur"> data e dokumentit ngjitur</param>
        /// <param name="nipti"> nipti.</param>
        /// <param name="nrkuponi"> nr i kuponit</param>
        /// <param name="idKonf"> id e konfigurimit te dokumentit</param>
        /// <param name="iddoknga">id e dokumentit nga e cila gjenerohet ne rastet e modifikimit </param>
        /// <param name="nrpunonjesve">nr i punonjesve</param>
        /// <param name="idkoka">id ritese e kokes se Urdher pageses</param>
        /// <param name="idNder"> id e ndermarjes</param>
        /// <param name="idNdVt"> id e ndermarje vitit</param>
        /// <param name="idNiv"> id e nivelit te regjistrimit</param>
        /// <param name="idPer"> id e perdoruesit se e ben regjistrimin</param>
        /// <param name="emriPerfituesit"> emri i perfituesit</param>
        /// <param name="idSt"> id e statusit te dokumentit</param>
        /// <param name="nrDk">nr i dokumentit</param>
        /// <param name="totali"> totali</param>
        /// <param name="nrllogbankare"> nr llog bankare </param>
        /// <param name="emribankes"> emri i bankes</param>
        /// <param name="idGjenerues">id e dokumentit nga gjenerohet magazina nga nje ambjent tjeter</param>
        /// <param name="idKonfigGjenerues">id e konfigurimit nga gjenerohet dokumenti</param>
        /// <param name="idNivelGjenerues">id e nivelit nga gjenerohet dokumenti</param>
        /// <param name="oColTrupi">kolektion i trupit te Urdherpageses</param>
        /// <param name="modifikim"> tregon nqs dokumenti po modifikohet ne menyre qe te mos behet kontrolli nqs ekziston ky dokument per kete ndermarje</param>
        /// <param name="adresa"> adresa</param>
        /// <param name="dbRegj"> clsDatabaseAkraBanka</param>
        /// <param name="dtdokaprovimi">dt e aprovimit</param>
        /// <param name="llojdok"> lloji i dokumentit ngjitur</param>
        /// <param name="nrdokngjitur">nr i dokumentit ngjitur</param>
        /// <returns> kthen nje obj clsMesazh per te identifikuar statusin e ruajtjes se te dhenave ne DB</returns>
        public static clsMesazh modifikoUrdherPagese(ref int idkoka, int idNiv, int idKonf, string nrkuponi, string nrpunonjesve, DateTime dtDk, string nrDk, string emriPerfituesit, decimal totali, string nipti, int iddoknga, string emribankes, int idSt, int idNder, int idNdVt, int idPer, DateTime dtdokngjitur, string nrllogbankare, string adresa, int llojdok, string nrdokngjitur, DateTime dtdokaprovimi, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues, int idRaportDesing, string urdheruesi, string kontabilisti, string nenpunesThesari, colTrupiUrdherPagese oColTrupi, clsDatabaseArkaBanka dbRegj)
        {
            colTrupiUrdherPagese trupat = new colTrupiUrdherPagese();
            trupat.mbushTrupiUrdherPagese(idkoka, dbRegj);
            clsMesazh mesazh = new clsMesazh();
            try
            {   //obj koka qe i kalohet si parameter eshte si duhet te modifikohet
                clsKokaUrdherPagese kokaEkzistuese = new clsKokaUrdherPagese(idkoka);
                if (string.IsNullOrEmpty(kokaEkzistuese.NrDok) || kokaEkzistuese.IdStatusDok == 2)
                {
                    return new clsMesazh(false, STR_DokumentiKaNdryshuarJuLutemRihapeniPerseri);
                }

                kokaEkzistuese.IdStatusDok = 2;  //duhet vendosur nje status i pershtatshem per kete modifikim
                iddoknga = kokaEkzistuese.idDokNga;

                mesazh = dbRegj.modifikoKokaUrdherPagese(kokaEkzistuese.IdKoka, kokaEkzistuese.IdNivel, kokaEkzistuese.IdKonfigAmbjente, kokaEkzistuese.NrKuponi, kokaEkzistuese.NrPunonjesve, kokaEkzistuese.DtDok, kokaEkzistuese.NrDok, kokaEkzistuese.EmriPerfituesit, kokaEkzistuese.Totali, kokaEkzistuese.Nipti, kokaEkzistuese.IdDokNga, kokaEkzistuese.EmriBankes, kokaEkzistuese.IdStatusDok, kokaEkzistuese.IdNdermarje, kokaEkzistuese.IdNderViti, kokaEkzistuese.IdPerdoruesi, kokaEkzistuese.DtDokNgjitur, kokaEkzistuese.NrLlogBankare, kokaEkzistuese.Adresa, kokaEkzistuese.LlojDokNgjitur, kokaEkzistuese.NrDokNgjitur, kokaEkzistuese.DtDokAprovimi, kokaEkzistuese.IdNivelGjenerues, kokaEkzistuese.IdKonfigGjenerues, kokaEkzistuese.IdGjenerues, kokaEkzistuese.IdRaportDesing, kokaEkzistuese.Urdheruesi, kokaEkzistuese.Kontabilisti, kokaEkzistuese.nenpunesThesari);

                if (!mesazh.Status)
                {
                    return mesazh;
                }
                int idKokaRe = -1;
                mesazh = clsKokaUrdherPagese.ruajUrdherPagese(out idKokaRe, idNiv, idKonf, nrkuponi, nrpunonjesve, dtDk, nrDk, emriPerfituesit, totali, nipti, iddoknga, emribankes, idSt, idNder, idNdVt, idPer, dtdokngjitur, nrllogbankare, adresa, llojdok, nrdokngjitur, dtdokaprovimi, idNivelGjenerues, idKonfigGjenerues, idGjenerues, idRaportDesing, urdheruesi, kontabilisti, nenpunesThesari, oColTrupi, dbRegj);
                if (!mesazh.Status)
                {
                    return mesazh;
                }
                idkoka = idKokaRe;
                return mesazh;
            }
            catch (Exception ce)
            {
                return new clsMesazh(false, ce.Message);
            }
        }

        /// <summary>
        /// Modifikon objektin e  kokes se dokumentit te Urdherpageses ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsKokaUrdherPagese.modifikoMagazina"/> 
        /// </summary>
        /// <param name="lidhur">tregon nese eshte i lidhur ose jo</param>
        /// <returns > nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo</returns>
        public clsMesazh modifiko(bool lidhur)
        {
            clsMesazh u_modifikua;
            clsDatabaseArkaBanka data = new clsDatabaseArkaBanka();
            if (!lidhur)
            {
                data.beginTransaksion();
                u_modifikua = clsKokaUrdherPagese.modifikoUrdherPagese(ref idKoka, IdNivel, IdKonfigAmbjente, nrKuponi, NrPunonjesve, DtDok, NrDok, EmriPerfituesit, Totali, Nipti, IdDokNga, EmriBankes, IdStatusDok, IdNdermarje, IdNderViti, IdPerdoruesi
                          , DtDokNgjitur, NrLlogBankare, Adresa, LlojDokNgjitur, NrDokNgjitur, DtDokAprovimi, IdNivelGjenerues, IdKonfigGjenerues, IdGjenerues, IdRaportDesing, Urdheruesi, Kontabilisti, NenpunesThesari, OColTrupi, data);
                if (u_modifikua.Status)
                    data.commitTransaksion();
                else
                    data.rollbackTransaksion();
            }
            else
            {
                u_modifikua = data.modifikoKokaUrdherPagese(idKoka, IdNivel, IdKonfigAmbjente, nrKuponi, NrPunonjesve, DtDok, NrDok, EmriPerfituesit, Totali, Nipti, IdDokNga, EmriBankes, IdStatusDok, IdNdermarje, IdNderViti, IdPerdoruesi
                          , DtDokNgjitur, NrLlogBankare, Adresa, LlojDokNgjitur, NrDokNgjitur, DtDokAprovimi, IdNivelGjenerues, IdKonfigGjenerues, IdGjenerues, IdRaportDesing, Urdheruesi, Kontabilisti, NenpunesThesari);
                data.Dispose();
            }
            return u_modifikua;
        }

        /// <summary>
        /// fshin nje objekt dokument Urdherpagese sebashku me te trupin 
        /// Nje objekt dokument Urdherpagese ka nje koleksion me trupin , 
        /// fshirja e nje dokumenti Urdherpagese imponon fshirjen edhe te nje colection-i me trupin 
        /// Mqs cdo rresht i ri qe fshihet ne DB kerkon thirrjen e nje SP-je me parametra dhe dokumenti i Urdherpageses bashke me trupin  konsiderohet si nje regjistrim,
        /// perdoret nje transaksion qe imponon fshirjen e rregullt te nje te nje dokumenti sebashku me trupin 
        ///2. ben fshirjen e trupit dhe me pas te kokes se dokumentit te Urdherpageses
        /// </summary>
        ///<param name="idkoka"> koka e dokumentit te Urdherpageses i cili do te fshihet</param>
        /// <returns> kthen nje obj clsMesazh per te identifikuar statusin e fshirjes se te dhenave ne DB</returns>
        public static clsMesazh fshiUrdherPagese(int idkoka, clsDatabaseArkaBanka db)
        {
            clsMesazh mesazh = new clsMesazh(true);
            try
            {
                clsKokaUrdherPagese kokaEkzistuese = new clsKokaUrdherPagese(idkoka);
                kokaEkzistuese.IdStatusDok = 2;  //duhet vendosur nje status i pershtatshem per kete modifikim
                mesazh = db.modifikoKokaUrdherPagese(kokaEkzistuese.IdKoka, kokaEkzistuese.IdNivel, kokaEkzistuese.IdKonfigAmbjente, kokaEkzistuese.NrKuponi, kokaEkzistuese.NrPunonjesve, kokaEkzistuese.DtDok, kokaEkzistuese.NrDok, kokaEkzistuese.EmriPerfituesit, kokaEkzistuese.Totali, kokaEkzistuese.Nipti, kokaEkzistuese.IdDokNga, kokaEkzistuese.EmriBankes, kokaEkzistuese.IdStatusDok, kokaEkzistuese.IdNdermarje, kokaEkzistuese.IdNderViti, kokaEkzistuese.IdPerdoruesi, kokaEkzistuese.DtDokNgjitur, kokaEkzistuese.NrLlogBankare, kokaEkzistuese.Adresa, kokaEkzistuese.LlojDokNgjitur, kokaEkzistuese.NrDokNgjitur, kokaEkzistuese.DtDokAprovimi, kokaEkzistuese.IdNivelGjenerues, kokaEkzistuese.IdKonfigGjenerues, kokaEkzistuese.IdGjenerues, kokaEkzistuese.IdRaportDesing, kokaEkzistuese.Urdheruesi, kokaEkzistuese.Kontabilisti, kokaEkzistuese.NenpunesThesari);
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
            clsDatabaseArkaBanka db = new clsDatabaseArkaBanka();
            //db.krijoManager();
            db.beginTransaksion();
            clsMesazh u_fshi = clsKokaUrdherPagese.fshiUrdherPagese(idKoka, db);
            if (u_fshi.Status)
                db.commitTransaksion();
            else
                db.rollbackTransaksion();
            return u_fshi;
        }



        /// <summary>
        /// Merr objektin e  kokes se dokumentit te Urdher pageses sipas nr dhe dt dokumenti nga tabela perkatese ne databaze.Therret funksionin
        /// :  <see cref="merrSipasIdNivelNrDokDtDok"/> 
        /// </summary>
        /// <returns > nje objekt clsKokaUrdherPagese qe permban objektin e kerkuar</returns>
        public static clsKokaUrdherPagese merrSipasIdNivelNrDokDtDok(int idnivel, string nrdok, DateTime dtdok)
        {
            clsKokaUrdherPagese data = new clsKokaUrdherPagese(idnivel, nrdok, dtdok);
            return data;

        }

        /// <summary>
        /// Merr gjithe e  kokat e dokumentave te Urdherpageses sipas ndermarjevitit nga tabela perkatese ne databaze.Therret funksionin
        /// :  <see cref="ktheGjitheKokaUrdherPagese"/> 
        /// </summary>
        /// <param name="idNdermVit">id e ndermarje vitit</param>
        /// <returns > nje object colKokaMagazina qe permban nje koleksion me gjithe kokat e dokumentave te Urdherpageses te nje ndermarje ne nje vit te caktuar</returns>
        public static colKokaUrdherPagese merriTeGjithe(int idNderViti)
        {
            colKokaUrdherPagese data = new colKokaUrdherPagese(idNderViti);
            return data;

        }

        /// <summary>
        /// Merr trupin  e  nje dokumenti te Urdherpageses nga tabela perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseArkaBanka.ktheTrupiUrdherPagesa"/> 
        /// </summary>
        /// <returns > nje object colTrupiUrdherPagese qe permban nje koleksion me trupin e dokumentit te Urdherpageses</returns>
        public colTrupiUrdherPagese merrTrupi(clsDatabaseArkaBanka db)
        {
            colTrupiUrdherPagese trupi = new colTrupiUrdherPagese();
            trupi.mbushTrupiUrdherPagese(IdKoka, db);
            return trupi;
        }

        /// <summary>
        /// mbush koken e Urdherpageses sipas id se kokes
        /// </summary>
        /// <param name="idkoka">id e kokes se Urdherpageses</param>
        /// <returns>kthen true nese mbushja kryhet me sukses ne te kundert kthen false</returns>
        public bool mbushKokaUrdherPagesesSipasID(int idkoka)
        {
            using (clsDatabaseArkaBanka db = new clsDatabaseArkaBanka())
            {
                return mbushKokaUrdherPagese(db.ktheKokaUrdherPageseSipasID(idkoka), db);
            }
        }
     
        #endregion

        #region Metoda Internal
        /// <summary>
        /// mbush koken sipas te dhenat nga db
        /// </summary>
        /// <param name="rreshti"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        internal bool mbushKokaUrdherPagese(DataRow rreshti, clsDatabaseArkaBanka db)
        {
            if (rreshti != null)
            {
                try
                {
                    int.TryParse(rreshti["IDKOKA"].ToString(), out idKoka);
                    int.TryParse(rreshti["IDNIVEL"].ToString(), out idNivel);
                    int.TryParse(rreshti["IDKONFIGAMBJENTE"].ToString(), out idKonfigAmbjente);
                    nrKuponi = rreshti["NRKUPONI"].ToString();
                    nrPunonjesve = rreshti["NRPUNONJESVE"].ToString();
                    nrDok = rreshti["NRDOK"].ToString();
                    DateTime.TryParse(rreshti["DTDOK"].ToString(), out dtDok);
                    emriPerfituesit = rreshti["EMRIPERFITUESIT"].ToString();
                    decimal.TryParse(rreshti["TOTALI"].ToString(), out  totali);
                    nipti = rreshti["NIPTI"].ToString();
                    int.TryParse(rreshti["IDDOKNGA"].ToString(), out idDokNga);
                    emriBankes = rreshti["EMRIBANKES"].ToString();

                    int.TryParse(rreshti["IDSTATUSDOK"].ToString(), out idStatusDok);
                    int.TryParse(rreshti["IDNDERMARJE"].ToString(), out idNdermarje);
                    int.TryParse(rreshti["IDNDERVITI"].ToString(), out idNderViti);
                    int.TryParse(rreshti["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    DateTime.TryParse(rreshti["DTDOKNG"].ToString(), out dtDokNgjitur);
                    nrLlogBankare = rreshti["NRLLOGBANKARE"].ToString();
                    adresa = rreshti["ADRESA"].ToString();
                    int.TryParse(rreshti["LLOJDOKNG"].ToString(), out llojDokNgjitur);
                    nrDokNgjitur = rreshti["NRDOKNG"].ToString();
                    DateTime.TryParse(rreshti["DTDOKAPROVIMI"].ToString(), out dtDokAprovimi);
                    int.TryParse(rreshti["IDNIVELGJENERUES"].ToString(), out idNivelGjenerues);
                    int.TryParse(rreshti["IDKONFIGGJENERUES"].ToString(), out idKonfigGjenerues);
                    int.TryParse(rreshti["IDGJENERUES"].ToString(), out idGjenerues);
                    DateTime.TryParse(rreshti["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(rreshti["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    int.TryParse(rreshti["IDRAPORTDESING"].ToString(), out idRaportDesing);
                    urdheruesi = Convert.ToString(rreshti["URDHERUESI"]);
                    kontabilisti = Convert.ToString(rreshti["KONTABILISTI"]);
                    nenpunesThesari = Convert.ToString(rreshti["NENPUNESTHESARI"]);
                    oColTrupi = new colTrupiUrdherPagese();
                    oColTrupi = merrTrupi(db);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception(gabimTeDhena);
                }
            }
            else
                return false;
        }

        #endregion
    }
}

