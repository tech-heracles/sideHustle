using DbCore.DbAdmin;
using DbCore.DbKontabiliteti;
using DbCore.DbShare;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne  koken e nje dokumenti te inventarizim
    ///  (Te dhenat  merren nga tabela : T_KOKAINVENTARIZIM)
    /// </summary>
    public class clsKokaInventarizim
    {
        #region Attributet
        private int idKoka;
        private int idNivel;
        private int idKonfigAmbjente;
        private int idMag;
        private DateTime dtDok;
        private String nrDok;
        private int idDokNga;
        private int idStatusDok;
        private int idNdermarje;
        private int idNderViti;
        private int idPerdoruesi;
        private DateTime dtRegj;
        private int idNivelGjenerues;
        private int idKonfigGjenerues;
        private int idGjenerues;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private colTrupiInventarizim ocolTrupiInventarizim;
        private string kodMagazina;
        private string pershkrimi;
        private int idKrijuesi;
        private int idRaportDesing;

        private DataRow rreshti;
        private clsDatabaseRegjistrim db;
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
        /// Kthen/Vendos ID-ne e magazines
        /// </summary>
        public int IdMag
        {
            get { return idMag; }
            set { idMag = value; }
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
        /// Kthen/Vendos ID-ne  e dokumentit nga i cili gjenerohet ne rastet e modifikimit
        /// </summary>
        public int IdDokNga
        {
            get { return idDokNga; }
            set { idDokNga = value; }
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
        public DateTime DtRegj
        {
            get { return dtRegj; }
            set { dtRegj = value; }
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
        /// Kthen/Vendos  nje koleksion me trupin e dokumentit te magazines
        /// </summary>
        public colTrupiInventarizim OcolTrupiInventarizimi
        {
            get { return ocolTrupiInventarizim; }
            set { ocolTrupiInventarizim = value; }
        }

        public DateTime DtKrijimi
        {
            get { return dtKrijimi; }

        }
        public DateTime DtModifikimi
        {
            get { return dtModifikimi; }

        }


        public String Pershkrimi
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

        public int IdKrijuesi
        {
            get { return idKrijuesi; }
            set { idKrijuesi = value; }

        }

        public int IdRaportDesing
        {
            get { return idRaportDesing; }
            set { idRaportDesing = value; }

        }

        //  private int idRaportDesing;
        #endregion

        #region Konstruktoret
        /// <summary>
        /// Konstruktori i klases me parametra
        /// </summary>
        /// <param name="dtDk"> data e dokumentit te magazines</param>
        /// <param name="dtRegj"> data e regjistrimit te dokumentit te magazines</param>
        /// <param name="idKDok"> id kategorise se dokumentit me te cilin lidhet.</param>
        /// <param name="idKlFurn"> id e klient furnitorit me te cilin lidhet</param>
        /// <param name="idKonf"> id e konfigurimit te dokumentit</param>
        /// <param name="idLidhes">id e dokumentit nga e cila gjenerohet ne rastet e modifikimit </param>
        /// <param name="idllojdokmag"> id e llojit te dokumentit</param>
        /// <param name="idMag">id e magazines</param>
        /// <param name="idMagKoka">id ritese e kokes se magazines</param>
        /// <param name="idNder"> id e ndermarjes</param>
        /// <param name="idNdVt"> id e ndermarje vitit</param>
        /// <param name="idNiv"> id e nivelit te regjistrimit</param>
        /// <param name="idPer"> id e perdoruesit se e ben regjistrimin</param>
        /// <param name="idProj"> id e projektit ne te cilin ben pjese</param>
        /// <param name="idrenditjes"> id e renditjes per te renditur dokumentat e te njejtes date sipas kohes se regjistrimit</param>
        /// <param name="idSt"> id e statusit te dokumentit</param>
        /// <param name="nrDk">nr i dokumentit</param>
        /// <param name="nrProj"> nr i projektit</param>
        /// <param name="shenim"> shenime </param>
        /// <param name="vl"> vlera totale e dokumentit</param>
        /// <param name="idGjenerues">id e dokumentit nga gjenerohet magazina nga nje ambjent tjeter</param>
        /// <param name="idKonfigGjenerues">id e konfigurimit nga gjenerohet dokumenti</param>
        /// <param name="idNivelGjenerues">id e nivelit nga gjenerohet dokumenti</param>  
        /// <param name="idgrup1">id e grupimit te pare</param>
        /// <param name="idgrup2">id e grupimit te dyte</param>
        /// <param name="idgrup3">id e grupimit te trete</param>

        public clsKokaInventarizim(int idKoka, int idNiv, int idKonf, int idMag, DateTime dtDk, string nrDk, int idLidhes, int idSt, int idNder, int idNdVt, int idPer, DateTime dtRegj, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues, String pershkrimi, int idkrijuesi,int idrap)
        {
            this.idKoka = idKoka;
            idNivel = idNiv;
            idKonfigAmbjente = idKonf;
            this.idMag = idMag;
            nrDok = nrDk;
            dtDok = dtDk;
            idDokNga = idLidhes;
            idStatusDok = idSt;
            idNdermarje = idNder;
            idNderViti = idNdVt;
            idPerdoruesi = idPer;
            this.dtRegj = dtRegj;
            this.pershkrimi = pershkrimi;
            this.idNivelGjenerues = idNivelGjenerues;
            this.idKonfigGjenerues = idKonfigGjenerues;
            this.idGjenerues = idGjenerues;
            idKrijuesi = idkrijuesi;
            this.idRaportDesing = idrap;
        }

        /// <summary>
        /// konstruktor me 3 parametra
        /// </summary>
        /// <param name="idNivel">id e nivelit</param>
        /// <param name="nrdok">nr i dokumentit</param>
        /// <param name="dtdok">data e dokumentit</param>
        public clsKokaInventarizim(int idNivel, string nrdok, DateTime dtdok)
        {
            clsDatabaseRegjistrim dbKokaMagazina = new clsDatabaseRegjistrim();
            mbushKokaInventarizim(dbKokaMagazina.ktheKokaInventarizimSipasIdNivelNrDokDtDok(idNivel, nrdok, dtdok));
            dbKokaMagazina.Dispose();
        }

        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsKokaInventarizim()
        {
            ocolTrupiInventarizim = new  colTrupiInventarizim ();
        }

        public clsKokaInventarizim(DataRow rreshti)
        {
            
            mbushKokaInventarizim(rreshti);
        }
        #endregion

        #region Metoda Publike

        public clsMesazh krijoInventarizim(int idekzistuese, int idNiv, int idKonf,   int idMag, string kodmag, DateTime dtDk, string nrDk,  int idSt, int idNder, int idNdVt, int idPer, DateTime dtRegj, string pershkrimi,colTrupiInventarizim coltrupi,  out string mesazhinformues, bool meautorizim,  int idkrijuesi, bool shtim,int idrap)
        {

             mesazhinformues = "";
            return krijoInventarizim(idekzistuese, idNiv, idKonf,  idMag, kodmag, dtDk, nrDk,  0,  idSt, idNder, idNdVt, idPer, dtRegj,  0, 0, 0, pershkrimi,
                coltrupi,  new  clsDatabaseRegjistrim(), meautorizim,true,  idkrijuesi, shtim,idrap);
        }

        public clsMesazh krijoInventarizim(int idekzistuese, int idNiv, int idKonf, int idMag, string kodmag, DateTime dtDk, string nrDk, int idLidhes, int idSt, int idNder, int idNdVt, int idPer, DateTime dtRegj, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues, string pershkrimi,  colTrupiInventarizim coltrupi, clsDatabaseRegjistrim db, bool meautorizm, bool kontrolloEkzistence, int idkrijuesi, bool shtim,int idrap)
        {
            idKoka = idekzistuese;
            idNivel = idNiv;
            idKonfigAmbjente = idKonf;
           this.idMag = idMag;
            kodMagazina = kodmag;
            nrDok = nrDk;
            dtDok = dtDk;
            idDokNga = idLidhes;
             idStatusDok = idSt;
            idNdermarje = idNder;
            idNderViti = idNdVt;
            idPerdoruesi = idPer;
           this. dtRegj = dtRegj;
            this.pershkrimi = pershkrimi;
            this.idNivelGjenerues = idNivelGjenerues;
            this.idKonfigGjenerues = idKonfigGjenerues;
            this.idGjenerues = idGjenerues;
            ocolTrupiInventarizim = coltrupi;
            this.idKrijuesi = idkrijuesi;
            this.idRaportDesing = idrap;
           
            clsMesazh mesazh = this.kontrollo( db, meautorizm);
            if (!mesazh.Status)
                return mesazh;
            if (kontrolloEkzistence) //kontroll ekzistence qe ketu per rastin e importit
            {
                bool kaNdryshimNumri;
                mesazh = kontrolloInventarizim(out kaNdryshimNumri, db, null, shtim);
                if (!mesazh.Status)
                    return mesazh;
            }
           
            return new clsMesazh(true, "Inventarizimi u krijua me sukses!");
        }

        private clsMesazh kontrollo( clsDatabaseRegjistrim db, bool meautorizim)
        {
            if (nrDok == "")
                return new clsMesazh(false, "Numri i dokumentit nuk mund të jetë bosh");
            if (dtDok == null || dtDok.ToShortDateString() == "01/01/0100")
                return new clsMesazh(false, "Zgjidhni datën e dokumentit!");
            if (dtRegj == null || dtRegj.ToShortDateString() == "01/01/0100")
                return new clsMesazh(false, "Zgjidhni datën e regjistrimit!");
           
            if (kodMagazina != "")
            {
                if (!clsNjesiAdministrative.ekziston(kodMagazina, idNdermarje, db))
                    return new clsMesazh(false, "Magazina nuk ekziston!");
                clsNjesiAdministrative mag;
                if (meautorizim)
                    mag = new clsNjesiAdministrative(kodMagazina, idNdermarje, idPerdoruesi, db);
                else
                    mag = new clsNjesiAdministrative(kodMagazina, idNdermarje, db);
                if (mag.IdNjesiAdministrative < 1)
                    return new clsMesazh(false, "Nuk keni autorizime ne kete magazine!");
                if (kodMagazina != "" && !mag.Aktiv)
                    return new clsMesazh(false, "Magazina nuk eshte aktive!");
            }
           
          
            return new clsMesazh(true, "Kontrollet  u kaluan me sukses!");
        }

        public clsMesazh krijoInventarizimPerImport(string kodNiv, string kodKonf, int idMag, string kodMag, DateTime dtDk, string nrDk,int idDokNga, int idSt, int idNder, int idNdVt, int idPer, DateTime dtRegj, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues,  colTrupiInventarizim coltrupi, clsKonfigurimAmbjenti konfigAmbjenti, int idkrijuesi,  ResourceManager rm, CultureInfo ci )
        {
            int idNivel;
            if (kodNiv == "")
                throw new Exception("Nenkategoria nuk mund te jete bosh!");
            clsNivelRegjistrimi nivelRegj = new clsNivelRegjistrimi();
            nivelRegj.Kodi = kodNiv; nivelRegj.IdNdermarje = idNder;
            nivelRegj.merrNivelRegjSipasKodi();
            idNivel = nivelRegj.IdNivel;
            if (kodKonf == "")
                throw new Exception("Lloji i dokumentit nuk mund te jete bosh!");
            colKonfigurimAmbjenti colKonfig = new colKonfigurimAmbjenti();
            colKonfig.mbushKonfigAmbjSipasIdKategoriIdNivelMeLloj(nivelRegj.IdKategori, idNivel, idPer);
            if (colKonfig.Find(kodKonfig => kodKonfig.KodKonfigAmbjente.Contains(kodKonf)) == null)
            {
                throw new Exception("Nuk ekziston ky lloj dokumenti per kete kategori!");
            }
           
            clsNjesiAdministrative mag = new clsNjesiAdministrative(kodMag, idNder, idPer);
            idMag = mag.IdNjesiAdministrative;
            if (kodNiv == "IAAGJ" && mag.IdLlojMagazine == 1)
                return new clsMesazh(false, "Kjo magazine eshte per artikujt afatshkurter dhe nuk mund te perdoret per artikujt afatgjate!");
            else  if (kodNiv == "IAASH" && mag.IdLlojMagazine == 2)
                return new clsMesazh(false, "Kjo magazine eshte per artikujt afatgjate dhe nuk mund te perdoret per artikujt afatshkurter!");
                if (idkrijuesi == 0)
                return new clsMesazh(false, "Perdoruesi nuk ekziston!");

            clsPeriudhaKontabel periudha = new clsPeriudhaKontabel(dtDk, idNder);

            String mesazhGabimi;
            if (!DbCore.clsFunksione.checkPeriudheKontabel(out mesazhGabimi, dtDk, periudha, idSt))
            {
                return new clsMesazh(false, mesazhGabimi);
            }

          

            return krijoInventarizim(0, idNivel, konfigAmbjenti.IdKonfigAmbjente,  idMag, kodMag, dtDk, nrDk, idDokNga,  idSt, idNder, idNdVt, idPer, dtRegj,idNivelGjenerues, idKonfigGjenerues, idNivelGjenerues,  "", coltrupi, new clsDatabaseRegjistrim(), false, true, idkrijuesi, true,0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool eshteILidhur()
        {
            DbAdmin.clsDatabaseAdmin dbAdmin = new DbAdmin.clsDatabaseAdmin();
            bool lidhur = dbAdmin.eshteDokumentiILidhur(idKoka, idNivel, "T_KOKAINVENTARIZIM", "IDKOKA");
            dbAdmin.Dispose();
            return lidhur;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable merrIdsDokLidhur()
        {
            DbAdmin.clsDatabaseAdmin dbAdmin = new DbAdmin.clsDatabaseAdmin();
            DataTable dt = dbAdmin.MerrDokLidhur(idKoka, idNivel, "T_KOKAINVENTARIZIM", "IDKOKA");
            dbAdmin.Dispose();
            return dt;
        }

        /// <summary>
        /// Ruan nje objekt dokumenti magazine sebashku me trupin  dhe kontabilitetin perkates
        /// Nje objekt koka dokumenti magazine ka nje koleksion me trupin e dokumentit  dhe kontabilitetin perkates , 
        /// ruajtja e nje dokumenti imponon ruajtjen edhe te nje colection-i me trupin dhe kontabilitetin kur kontabilizohet
        /// Mqs cdo rresht i ri qe shtohet ne DB kerkon thirrjen e nje SP-je me parametra dhe koka e dokumentit bashke me trupin dhe kontabilitetin konsiderohet si nje regjistrim,
        /// perdoret nje transaksion qe imponon regjistrimin e rregullt te nje te nje dokumenti magazine sebashku me trupin dhe kontabilitetin perkates
        /// ne rastin e flete daljeve behet kontrolli i gjendjes nqs eshte zgjedhur kontrolli i gjendes tek regjistrimi i artikullit dhe nuk lejohet transaksioni nqs gjendja ne magazine eshte me e vogel sesa gjendja qe duhet te dale
        /// ne rastet e transferimit ruhet edhe nje dokument tjeter hyrje per transferimin ne magazinen e dyte
        /// dokumentat e te njejtes date renditen sipas kohes kur jane ruajtur
        /// </summary>
        /// <param name="eshteTrasferim"> tregon nqs dokumenti eshte transferim ne menyre qe te kryhet ruajtja e dokumentit te dyte te transferimit</param>
        /// <param name="dtDk"> data e dokumentit te magazines</param>
        /// <param name="dtRegj"> data e regjistrimit te dokumentit te magazines</param>
        /// <param name="idKDok"> id kategorise se dokumentit me te cilin lidhet.</param>
        /// <param name="idKlFurn"> id e klient furnitorit me te cilin lidhet</param>
        /// <param name="idKonf"> id e konfigurimit te dokumentit</param>
        /// <param name="idLidhes">id e dokumentit nga e cila gjenerohet ne rastet e modifikimit </param>
        /// <param name="idllojdokmag"> id e llojit te dokumentit</param>
        /// <param name="idMag">id e magazines</param>
        /// <param name="idMagKoka">id ritese e kokes se magazines</param>
        /// <param name="idNder"> id e ndermarjes</param>
        /// <param name="idNdVt"> id e ndermarje vitit</param>
        /// <param name="idNiv"> id e nivelit te regjistrimit</param>
        /// <param name="idPer"> id e perdoruesit se e ben regjistrimin</param>
        /// <param name="idProj"> id e projektit ne te cilin ben pjese</param>
        /// <param name="idrenditjes"> id e renditjes per te renditur dokumentat e te njejtes date sipas kohes se regjistrimit</param>
        /// <param name="idSt"> id e statusit te dokumentit</param>
        /// <param name="nrDk">nr i dokumentit</param>
        /// <param name="nrProj"> nr i projektit</param>
        /// <param name="shenim"> shenime </param>
        /// <param name="vl"> vlera totale e dokumentit</param>
        /// <param name="idGjenerues">id e dokumentit nga gjenerohet magazina nga nje ambjent tjeter</param>
        /// <param name="idKonfigGjenerues">id e konfigurimit nga gjenerohet dokumenti</param>
        /// <param name="idNivelGjenerues">id e nivelit nga gjenerohet dokumenti</param>
        /// <param name="ocolTrupi">kolektion i trupit te magazines</param>
        /// <param name="oFleteKontabel">kolektion i fleteve kontabel</param>
        /// <param name="oMagazinaTransferim">kolektion i transferimit ne magazine</param>
        /// <param name="modifikim"> tregon nqs dokumenti po modifikohet ne menyre qe te mos behet kontrolli nqs ekziston ky dokument per kete ndermarje</param>
        /// <param name="meKontabilizim"> tregon nese dokumenti i magazines do te kontabilizohet apo jo</param>
        /// <param name="iddoktransferimi"> id e dokumentit te transferimit</param>
        /// <param name="idgrup1">id e grupimit te pare </param>
        /// <param name="idgrup2">id e grupimit te dyte</param>
        /// <param name="idgrup3">id e grupimit te trete</param>
        /// <returns> kthen nje obj clsMesazh per te identifikuar statusin e ruajtjes se te dhenave ne DB</returns>
        /// <param name="pershkrimDokKontabiliteti"></param>
        /// <param name="idLlojDok"></param>
        /// <param name="idDokNga"></param>
        /// <param name="kontabilizioamortizim"> perdoret per rastet kur kemi kontabilizim magazine brenda shitjes dhe nuk kontabilizohet magazina por duhet te kontabilizohet amortizimi</param>
        /// <param name="mosLlogaritAmortizimShtese"></param>

        public clsMesazh ruajInventarizim(out int idMagKoka, int idNiv, int idKonf,  int idMag, DateTime dtDk, string nrDk,  int idLidhes,  int idSt, int idNder, int idNdVt, int idPer, DateTime dtRegj,  int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues,  colTrupiInventarizim ocolTrupi,   clsDatabaseRegjistrim dbRegj, string pershkrimi,  out string shfaqmesazhapolupe, ResourceManager rm, CultureInfo ci, int idkrijuesi, bool modifikim,int idrap)
        {
            //transaksioni per te ruajtur 
            //bool connectionIRi = false;
            clsMesazh mesazh;
            
            shfaqmesazhapolupe = "jo";
            idMagKoka = 0;

            //   clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();
            idMagKoka = dbRegj.ruajKokaInventarizim(idMagKoka, idNiv, idKonf,  idMag, dtDk, nrDk,    idLidhes,  idSt, idNder, idNdVt, idPer, dtRegj,   idNivelGjenerues, idKonfigGjenerues, idGjenerues,  pershkrimi, idkrijuesi,idrap);

            if (idMagKoka == 0)
                return new clsMesazh(false, "Ndodhi një Gabim gjatë ruajtjes së Kokës së Magazinës");
           
            System.Diagnostics.Stopwatch myWatchRuajTrupin = System.Diagnostics.Stopwatch.StartNew();
            #region Ruajtrupin
            //int count = 0;
            int iii = 0;
            System.Diagnostics.Stopwatch myWatchRuajTrupinOcolTrupiMagazina = System.Diagnostics.Stopwatch.StartNew();
          
            foreach (clsTrupiInventarizim trup in ocolTrupi)
            {//behet ruajtja e trupit te magazines
                trup.IdKoka = idMagKoka;
               int idt=0;
               mesazh = dbRegj.ruajTrupiInventarizim(out idt, trup.IdKoka, trup.Barkod, trup.Sasi, trup.Serial, trup.Shenime);
               if(!mesazh.Status)
                   return mesazh;
                iii++;

            }
            myWatchRuajTrupinOcolTrupiMagazina.Stop();
            if (myWatchRuajTrupinOcolTrupiMagazina.Elapsed > new TimeSpan(0, 0, 1))
                System.Diagnostics.Trace.WriteLine("myWatchRuajTrupinOcolTrupiMagazina: " + myWatchRuajTrupinOcolTrupiMagazina.Elapsed);
            #endregion
            myWatchRuajTrupin.Stop();
            if (myWatchRuajTrupin.Elapsed > new TimeSpan(0, 0, 1))
                System.Diagnostics.Trace.WriteLine("myWatchRuajTrupin: " + myWatchRuajTrupin.Elapsed);

            if (modifikim)
            {
                mesazh = dbRegj.modifikoIdInventarizimi(idLidhes, idMagKoka);
                if (!mesazh.Status)
                {
                    return mesazh;
                }

            }

            mesazh = new clsMesazh(true, "Ruajtja përfundoi me sukses!");
            return mesazh;
        }

        /// <summary>
        /// Ruan objektin e  kokes se dokumentit te magazines ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsKokaMagazine.ruajMagazina"/> 
        /// </summary>
        /// <param name="eshteTransferim">tregon nese dokumenti i regjistruar eshte transferim apo jo</param>
        /// <param name="meKontabilizim">tregon nese te konfigurimet magazina eshte me kontabilizim apo jo</param>
        /// <param name="hfNrAutoregjistrime">hiddenField i Devit qe mban numrat automatike,null nese nuk perodren</param>
        /// <param name="idPeriudha"></param>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
        public clsMesazh ruaj(IDictionary<string, object> hfNrAutoregjistrime,  out string shfaqmesazhapolupe,  ResourceManager rm, CultureInfo ci, bool vjenNgaImportSQL, string idDokImporti, string ndermarrjeKey, string emerTabKoka, string primaryKey, bool shtim)
        {
           
            clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();
            //db.krijoManager();
            db.beginTransaksion();
            clsMesazh u_ruajt;
            shfaqmesazhapolupe = "";
            try
            {
                u_ruajt = ruaj( hfNrAutoregjistrime,   db, out shfaqmesazhapolupe,   rm, ci, shtim); //perdor ruajtjen me transaksion
                if (!u_ruajt.Status)
                {
                    db.rollbackTransaksion();
                    return u_ruajt;
                }
                if (vjenNgaImportSQL && idDokImporti != string.Empty)
                {
                    string[] idte = idDokImporti.Split(';');
                    for (var i = 0; i < idte.Length; i++)
                    {
                        if (idte[i] == string.Empty)
                            continue;
                        int statusi = 1;

                        clsMesazh mesazh = db.updateDokTabeleTemportal(idte[i], idNdermarje, statusi, emerTabKoka, primaryKey, ndermarrjeKey);

                        if (!mesazh.Status)
                        {
                            db.rollbackTransaksion();
                            return mesazh;
                        }
                    }
                }
            }
            catch (Exception)
            {
                db.rollbackTransaksion();
                return new clsMesazh("Ndodhi nje gabim gjate ruajtjes se dokumentit te inventarizimit");
            }
            db.commitTransaksion();
            return u_ruajt;
        }

        /// <summary>
        /// Ruan objektin e  kokes se dokumentit te magazines ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsKokaMagazine.ruajMagazina"/> 
        /// </summary>
        /// <param name="eshteTransferim">tregon nese dokumenti i regjistruar eshte transferim apo jo</param>
        /// <param name="meKontabilizim">tregon nese te konfigurimet magazina eshte me kontabilizim apo jo</param>
        /// <param name="hfNrAutoregjistrime">hiddenField i Devit qe mban numrat automatike,null nese nuk perodren</param>
        /// <param name="idPeriudha"></param>
        /// <param name="kontabilizioamortizim"> perdoret per rastet kur kemi kontabilizim magazine brenda shitjes dhe nuk kontabilizohet magazina por duhet te kontabilizohet amortizimi</param>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
        public clsMesazh ruaj(IDictionary<string, object> hfNrAutoregjistrime,clsDatabaseRegjistrim db, out string shfaqmesazhapolupe,  ResourceManager rm, CultureInfo ci, bool shtim)
        {
           
           
            bool kaNdryshimNumri;
            clsMesazh mesazhKontrolli = kontrolloInventarizim(out kaNdryshimNumri, db, hfNrAutoregjistrime, shtim); //TODO KEVI duhet hequr kontrolli qe ekziston doku apo jo per dokumentat e gjeneruar nga shitja
            if (!mesazhKontrolli.Status)
            {
                shfaqmesazhapolupe = "jo";
                //db.rollbackTransaksion();
                return mesazhKontrolli;
            }
            int idkoka = 0;
            //duhet kapur per cdo dokument nese po modifikohet apo eshte i ri fare

            clsMesazh u_ruajt = this.ruajInventarizim(out idkoka, this.IdNivel, this.IdKonfigAmbjente,  this.IdMag, this.DtDok, this.NrDok,  this.IdDokNga, this.IdStatusDok, this.IdNdermarje, this.IdNderViti, this.IdPerdoruesi, this.DtRegj,  this.IdNivelGjenerues, this.IdKonfigGjenerues, this.IdGjenerues, this.OcolTrupiInventarizimi, db,this.Pershkrimi, out shfaqmesazhapolupe,  rm, ci, this.IdKrijuesi, true,this.IdRaportDesing);
            this.idKoka = idkoka;
            if (!u_ruajt.Status)
            {
                //db.rollbackTransaksion();
                return u_ruajt;
            }

            //db.commitTransaksion();
            if (kaNdryshimNumri)
                return mesazhKontrolli;
            return u_ruajt;
        }

        private clsMesazh kontrolloInventarizim(out bool kaNdryshimNumri, clsDatabaseRegjistrim dbRegj, IDictionary<string, object> hfNrAutoregjistrime, bool shtim)
        {
            kaNdryshimNumri = false;
            if (this.ocolTrupiInventarizim.Count == 0)
                return new clsMesazh("Trupi inventarizm nuk mund te jete bosh!");
            foreach (clsTrupiInventarizim t in this.ocolTrupiInventarizim)
            {
                if (t.Barkod == "" && t.Serial == "")
                {
                    return new clsMesazh("Te dhënat nuk janë të sakta");
                }
               
            }

            clsMesazh mes = new clsMesazh();

            if (hfNrAutoregjistrime != null)
            {
                mes = kontrolloNrAutoInventarizim(out kaNdryshimNumri, dbRegj, hfNrAutoregjistrime);
                if (!mes.Status)
                    return mes;
            }
            if (dbRegj.ekzistonRegjistrimInventarizim(idKonfigAmbjente, nrDok, idMag, dtDok, idNdermarje)&&shtim)
                return new clsMesazh(false, "Ekziston një regjistrim me të njëjtin numër dokumenti!");
            if (kaNdryshimNumri)
                return mes;
            return new clsMesazh(true, "Kontrolli i inventarizim u krye me sukses!");
        }

        private clsMesazh kontrolloNrAutoInventarizim(out bool kaNdryshimNumri, clsDatabaseRegjistrim dbRegj, IDictionary<string, object> hfregjistrime)
        {
            clsDatabaseAdmin db = new clsDatabaseAdmin(dbRegj );
            List<NrAuto> list = DbAdmin.clsNrAutom.kontrollogjithenumrat(db, hfregjistrime, this.dtDok);
            if (NrAuto.ktheVlerenEre(list, "NrDok") != "")
                this.nrDok = NrAuto.ktheVlerenEre(list, "NrDok");
           
            DbCore.clsMesazh mes = NrAuto.ruajvlera(out kaNdryshimNumri, list, this.dtDok, this.idPerdoruesi, this.idNdermarje, db);
            return new clsMesazh(mes.Status, mes.PershkrimMesazhi);
        }

        /// <summary>
        /// Modifikon nje objekt dokument magazine sebashku me te trupin dhe kontabilitetin
        /// Nje objekt dokument magazine ka nje koleksion me trupin dhe kontabilitetin perkates , 
        /// modifikimi e nje dokumenti imponon modifikimin edhe te nje colection-i me trupin dhe kontabilitetin
        /// Mqs cdo rresht i ri qe modifikon ne DB kerkon thirrjen e nje SP-je me parametra dhe dokumenti i magazines bashke me trupin dhe kontabilitetin konsiderohet si nje regjistrim,
        /// perdoret nje transaksion qe imponon modifikimin e rregullt te nje te nje dokumenti magazine sebashku me trupin dhe kontabilitetin
        /// 1. merret dokumenti eksistues  i magazines dhe kalohet ne gjendjen 20 te modifikimit
        /// 2. ruhet dokumenti i ri i magzines se bashku me trupin dhe kontabilitetin
        /// 3. stornohen kontabiliteti i dokumentave eksistues  te magazines
        /// </summary>
        /// <param name="transferim"> tregon nqs dokumenti eshte transferim ne menyre qe te kryhet ruajtja e dokumentit te dyte te transferimit</param>
        /// <param name="dtDk"> data e dokumentit te magazines</param>
        /// <param name="dtRegj"> data e regjistrimit te dokumentit te magazines</param>
        /// <param name="idKDok"> id kategorise se dokumentit me te cilin lidhet.</param>
        /// <param name="idKlFurn"> id e klient furnitorit me te cilin lidhet</param>
        /// <param name="idKonf"> id e konfigurimit te dokumentit</param>
        /// <param name="idLidhes">id e dokumentit nga e cila gjenerohet ne rastet e modifikimit </param>
        /// <param name="idllojdokmag"> id e llojit te dokumentit</param>
        /// <param name="idMag">id e magazines</param>
        /// <param name="idMagKoka">id ritese e kokes se magazines</param>
        /// <param name="idNder"> id e ndermarjes</param>
        /// <param name="idNdVt"> id e ndermarje vitit</param>
        /// <param name="idNiv"> id e nivelit te regjistrimit</param>
        /// <param name="idPer"> id e perdoruesit se e ben regjistrimin</param>
        /// <param name="idProj"> id e projektit ne te cilin ben pjese</param>
        /// <param name="idrenditjes"> id e renditjes per te renditur dokumentat e te njejtes date sipas kohes se regjistrimit</param>
        /// <param name="idSt"> id e statusit te dokumentit</param>
        /// <param name="nrDk">nr i dokumentit</param>
        /// <param name="nrProj"> nr i projektit</param>
        /// <param name="shenim"> shenime </param>
        /// <param name="vl"> vlera totale e dokumentit</param>
        /// <param name="idGjenerues">id e dokumentit nga gjenerohet magazina nga nje ambjent tjeter</param>
        /// <param name="idKonfigGjenerues">id e konfigurimit nga gjenerohet dokumenti</param>
        /// <param name="idNivelGjenerues">id e nivelit nga gjenerohet dokumenti</param>
        /// <param name="ocolTrupiMagazina">kolektion i trupit te magazines</param>
        /// <param name="oFleteKontabel">kolektion i fleteve kontabel</param>
        /// <param name="oMagazinaTransferim">kolektion i transferimit ne magazine</param>
        /// <param name="idgrup1">id e grupimit te pare </param>
        /// <param name="idgrup2">id e grupimit te dyte</param>
        /// <param name="idgrup3">id e grupimit te trete</param>
        /// <param name="meKontabilizim"> tregon nese dokumenti i magazines do te kontabilizohet apo jo</param>
        /// <returns> kthen nje obj clsMesazh per te identifikuar statusin e modifikimit se te dhenave ne DB</returns>
        /// <param name="mosLlogaritAmortizimShtese"></param>
        public clsMesazh modifikoInventarizim(int idMagKoka, int idNiv, int idKonf,  int idMag, DateTime dtDk, string nrDk, 
            int idLidhes,  int idSt, int idNder, int idNdVt, int idPer, DateTime dtRegj,  int idNivelGjenerues,
            int idKonfigGjenerues, int idGjenerues,colTrupiInventarizim ocolTrupi,  string pershkrimi, clsDatabaseRegjistrim dbRegj, out  string shfaqmesazhapolupe, out int idkokare,  ResourceManager rm, CultureInfo ci, int idkrijuesi,int idrap)
        {
            idkokare = 0;
            shfaqmesazhapolupe = "jo";
            
            clsMesazh mesazh;
            try
            {   //obj koka qe i kalohet si parameter eshte si duhet te modifikohet
                clsKokaInventarizim kokaEkzistuese = new clsKokaInventarizim();
               
                kokaEkzistuese.mbushKokaInventarizimSipasID(idMagKoka, dbRegj);
                this.idKrijuesi = kokaEkzistuese.idKrijuesi;//dokumentit te ri i vendosim id e krijuesit te dokumentit te vjeter
                //clsKokaInventarizim kokaEkzistuese = this.merrKokaMagazinaSipasID(koka)[0];
                if (string.IsNullOrEmpty(kokaEkzistuese.NrDok) || kokaEkzistuese.IdStatusDok == 2)
                {
                    return new clsMesazh(false, rm.GetString("msgDokumentiKaNdryshuarHapeniPerseri", ci));
                }

                kokaEkzistuese.mbushTrupInventarizim(dbRegj);
                kokaEkzistuese.IdStatusDok = 2;  //duhet vendosur nje status i pershtatshem per kete modifikim
                idLidhes = kokaEkzistuese.IdKoka;
               
                mesazh = dbRegj.modifikoKokaInventarizim(kokaEkzistuese.IdKoka, kokaEkzistuese.IdNivel, kokaEkzistuese.IdKonfigAmbjente,  kokaEkzistuese.IdMag, kokaEkzistuese.DtDok, kokaEkzistuese.NrDok, kokaEkzistuese.IdStatusDok,kokaEkzistuese.idPerdoruesi, kokaEkzistuese.DtRegj,  kokaEkzistuese.Pershkrimi,kokaEkzistuese.idRaportDesing);
                if (!mesazh.Status)
                    return mesazh;

                mesazh = ruajInventarizim(out idMagKoka, idNiv, idKonf,  idMag, dtDk, nrDk, idLidhes,  idSt, idNder, idNdVt, idPer, dtRegj,  idNivelGjenerues, idKonfigGjenerues, idGjenerues,  ocolTrupi, dbRegj,  pershkrimi,  out shfaqmesazhapolupe,  rm, ci, idkrijuesi, true,idrap);
                idkokare = idMagKoka;
                if (!mesazh.Status)
                    return mesazh;
               
                return new clsMesazh(true, rm.GetString("regjMagModifikimiPerfundoiMeSukses", ci));
            }
            catch (Exception ce)
            {
                return new clsMesazh(false, ce.Message);
            }
        }

        /// <summary>
        /// Modifikon objektin e  kokes se dokumentit te magazines ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsKokaInventarizim.modifikoMagazina"/> 
        /// </summary>
        /// <param name="meKontabilizim">tregon nese te konfigurimet magazina eshte me kontabilizim apo jo</param>
        /// <param name="transferim">nese eshte i transferuar ose jo</param>
        /// <param name="lidhur">tregon nese eshte i lidhur ose jo</param>
        /// <returns > nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo</returns>
        public clsMesazh modifiko(bool lidhur,  out  string shfaqmesazhapolupe, ResourceManager rm, CultureInfo ci)
        {
            clsMesazh u_modifikua;
          
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            //data.krijoManager();
            shfaqmesazhapolupe = "jo";

            try
            {
                if (!lidhur)
                {

                    data.beginTransaksion();
                    int idkokare = 0;
                    u_modifikua = modifikoInventarizim(this.IdKoka, this.IdNivel, this.IdKonfigAmbjente,  this.IdMag, this.DtDok, this.NrDok,this.IdDokNga,  this.IdStatusDok, this.IdNdermarje, this.IdNderViti, this.IdPerdoruesi, this.DtRegj, this.IdNivelGjenerues, this.IdKonfigGjenerues, this.IdGjenerues, this.OcolTrupiInventarizimi,  this.Pershkrimi, data, out shfaqmesazhapolupe, out idkokare,  rm, ci, this.idKrijuesi,this.idRaportDesing);
                    this.idKoka = idkokare;
                    if (!u_modifikua.Status)
                    {
                        data.rollbackTransaksion();
                        return u_modifikua;
                    }
                    data.commitTransaksion();
                    return u_modifikua;
                }

                data.beginTransaksion();
               
                clsKokaInventarizim kokavjeter = new clsKokaInventarizim();
                kokavjeter.mbushKokaInventarizimSipasID(this.idKoka, data);
                this.idKrijuesi = kokavjeter.idKrijuesi;//dokumentit te ri i vendosim id e krijuesit te dokumentit te vjeter
                u_modifikua = data.modifikoKokaInventarizim(this.IdKoka, this.IdNivel, this.IdKonfigAmbjente, this.IdMag, this.DtDok, this.NrDok,
                 this.IdStatusDok, this.idPerdoruesi, this.DtRegj, this.Pershkrimi,this.IdRaportDesing);
                if (!u_modifikua.Status)
                {
                    data.rollbackTransaksion();
                    return u_modifikua;
                }
              
                data.commitTransaksion();
                return u_modifikua;
            }
            catch (Exception)
            {
                data.rollbackTransaksion();
                throw;
            }
        }

        /// <summary>
        /// fshin nje objekt dokument magazine sebashku me te trupin dhe kontabilitetin perkates
        /// Nje objekt dokument magazine ka nje koleksion me trupin dhe kontabilitetin , 
        /// fshirja e nje dokumenti magazine imponon fshirjen edhe te nje colection-i me trupin dhe kontabilitetin
        /// Mqs cdo rresht i ri qe fshihet ne DB kerkon thirrjen e nje SP-je me parametra dhe dokumenti i magazines bashke me trupin dhe kontabilitetin konsiderohet si nje regjistrim,
        /// perdoret nje transaksion qe imponon fshirjen e rregullt te nje te nje dokumenti sebashku me trupin dhe kontabilitetin e tij
        ///1. merr kontabilitetet eksistuese te dokumentit te shitjes dhe magazines dhe i stornon
        ///2. ben fshirjen e trupit dhe me pas te kokes se dokumentit te magazines
        /// </summary>
        ///<param name="idMagKoka"> koka e dokumentit te magazines i cili do te fshihet</param>
        ///<param name="dbRegj"> clsDatabase regjistrimi kur eshte pjese e nje trasaksioni</param>
        ///<param name="idperdoruesi"> id e perdoruesit qe po kryen veprimin</param>
        /// <returns> kthen nje obj clsMesazh per te identifikuar statusin e fshirjes se te dhenave ne DB</returns>
        public clsMesazh fshiInventarizim(int idMagKoka, int idperdoruesi, clsDatabaseRegjistrim dbRegj)
        {
            clsMesazh mesazh;
            try
            {
                  clsKokaInventarizim kokaEkzistuese = new clsKokaInventarizim();
                kokaEkzistuese.mbushKokaInventarizimSipasID(idMagKoka, dbRegj);
              
                kokaEkzistuese.IdStatusDok = 2; ///fshijme dokumentin e magazines duke e kaluar me status fshire                
                mesazh = dbRegj.modifikoKokaInventarizim(kokaEkzistuese.IdKoka, kokaEkzistuese.IdNivel, kokaEkzistuese.IdKonfigAmbjente,kokaEkzistuese.IdMag, kokaEkzistuese.DtDok, kokaEkzistuese.NrDok,  kokaEkzistuese.IdStatusDok, idperdoruesi, kokaEkzistuese.DtRegj, kokaEkzistuese.Pershkrimi,kokaEkzistuese.IdRaportDesing);
                if (!mesazh.Status)
                    return mesazh;
                mesazh = dbRegj.fshiLidhjeMagInvSipasInv(kokaEkzistuese.idKoka);///fshijme lidhjet tek konvertimet
                if (!mesazh.Status)
                    return mesazh;
                return new clsMesazh(true, "Fshirja përfundoi me sukses!");

            }
            catch (Exception ce)
            {
                return new clsMesazh(false, ce.Message);
            }
        }
     
      
    
        /// <summary>
        /// Fshin objektin e  kokes se dokumentit te magazines ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.fshiKokaMagazina"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh fshi()
        {
            clsKokaInventarizim data = new clsKokaInventarizim();
            clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim();
            //dbRegj.krijoManager();
            dbRegj.beginTransaksion();
            clsMesazh u_fshi = data.fshiInventarizim(this.IdKoka, this.idPerdoruesi, dbRegj);
            if (u_fshi.Status)
                dbRegj.commitTransaksion();
            else
                dbRegj.rollbackTransaksion();
            return u_fshi;
        }


    
        /// <summary>
        /// Merr trupin  e  nje dokumenti te magazines nga tabela perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.ktheTrupiMagazina"/> 
        /// </summary>
        /// <returns > nje object colTrupiMagazina qe permban nje koleksion me trupin e dokumentit te magazines</returns>
        public bool mbushTrupInventarizim(clsDatabaseRegjistrim db)
        {
            ocolTrupiInventarizim = new   colTrupiInventarizim();
            return ocolTrupiInventarizim.mbushTrupiInventarizim(IdKoka, db);
        }
      
        /// <summary>
        /// Merr trupin  e  nje dokumenti te magazines nga tabela perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.ktheTrupiMagazina"/> 
        /// </summary>
        /// <returns > nje object colTrupiMagazina qe permban nje koleksion me trupin e dokumentit te magazines</returns>
        public bool mbushTrupInventarizim()
        {
            ocolTrupiInventarizim = new  colTrupiInventarizim ();
            return ocolTrupiInventarizim.mbushTrupiInventarizim(IdKoka);
        }
        /// <summary>
        /// mbush koken e magazines sipas id gjenerues
        /// </summary>
        /// <param name="idGjenerues">id e gjeneruesit</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        /// <param name="lloji">lloji 1-hyrje 2-dalje</param>
        /// <param name="idkonfiggjenerues"></param>
        public bool mbushKokaInventarizimSipasIDGjenerues(int idGjenerues,  int idkonfiggjenerues, clsDatabaseRegjistrim db)
        {
            return mbushKokaInventarizim(db.ktheKokaInventarizimSipasIDGjenerues(idGjenerues,  idkonfiggjenerues));
        }
        /// <summary>
        /// mbush koken e magazines sipas id gjenerues
        /// </summary>
        /// <param name="idGjenerues">id e gjeneruesit</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        /// <param name="lloji">lloji 1-hyrje 2-dalje</param>
        /// <param name="idkonfiggjenerues"></param>
        public bool mbushKokaInventarizimSipasIDGjenerues(int idGjenerues,  int idkonfiggjenerues)
        {
            clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();
            bool sukses = mbushKokaInventarizim(db.ktheKokaInventarizimSipasIDGjenerues(idGjenerues,  idkonfiggjenerues));
            db.Dispose();
            return sukses;
        }
        /// <summary>
        /// mbush koken e magazines sipas id dokNga
        /// </summary>
        /// <param name="idDokNga">id dokumenti nga</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        public bool mbushKokaInventarizimSipasIDDokNga(int iddokNga)
        {
            clsDatabaseRegjistrim dbKokaMagazina = new clsDatabaseRegjistrim();
            bool sukses = mbushKokaInventarizim(dbKokaMagazina.ktheKokaInventarizimSipasIDDokNga(iddokNga));
            dbKokaMagazina.Dispose();
            return sukses;
        }

        /// <summary>
        /// mbush koken e magazines sipas id se kokes
        /// </summary>
        /// <param name="idKokaMagazina">id e kokes se magazines</param>
        /// <returns>kthen true nese mbushja kryhet me sukses ne te kundert kthen false</returns>
        /// <param name="dbKokaMagazina"></param>
        public bool mbushKokaInventarizimSipasID(int idKokaMagazina, clsDatabaseRegjistrim dbKokaMagazina)
        {
            return mbushKokaInventarizim(dbKokaMagazina.ktheKokaInventarizimSipasID(idKokaMagazina));
        }
        /// <summary>
        /// mbush koken e magazines sipas id se kokes
        /// </summary>
        /// <param name="idKokaMagazina">id e kokes se magazines</param>
        /// <returns>kthen true nese mbushja kryhet me sukses ne te kundert kthen false</returns>
        /// <param name="dbKokaMagazina"></param>
        public bool mbushKokaInventarizimSipasID(int idKokaMagazina)
        {
            clsDatabaseRegjistrim dbKokaMagazina = new clsDatabaseRegjistrim();
            bool sukses = mbushKokaInventarizim(dbKokaMagazina.ktheKokaInventarizimSipasID(idKokaMagazina));
            dbKokaMagazina.Dispose();
            return sukses;
        }
      
        public static bool kaAutorizime(int idkokamagazina, int idperdoruesi)
        {
            clsDatabaseRegjistrim dbKokaMagazina = new clsDatabaseRegjistrim();
            bool sukses = dbKokaMagazina.kaAutorizimKokaInventarizim(idkokamagazina, idperdoruesi);
            dbKokaMagazina.Dispose();
            return sukses;
        }

        #endregion

        #region Metoda Internal

        internal bool mbushKokaInventarizim(DataRow rreshti)
        {
            if (rreshti != null)
            {
                try
                {
                    int.TryParse(rreshti["IDKOKA"].ToString(), out idKoka);
                    int.TryParse(rreshti["IDNIVEL"].ToString(), out idNivel);
                    int.TryParse(rreshti["IDKONFIGAMBJENTE"].ToString(), out idKonfigAmbjente);
                    int.TryParse(rreshti["IDMAG"].ToString(), out idMag);
                    nrDok = rreshti["NRDOK"].ToString();
                    DateTime.TryParse(rreshti["DTDOK"].ToString(), out dtDok);
                   int.TryParse(rreshti["IDDOKNGA"].ToString(), out idDokNga);
                    int.TryParse(rreshti["IDSTATUSDOK"].ToString(), out idStatusDok);
                    int.TryParse(rreshti["IDNDERMarje"].ToString(), out idNdermarje);
                    int.TryParse(rreshti["IDNDERVITi"].ToString(), out idNderViti);
                    int.TryParse(rreshti["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    DateTime.TryParse(rreshti["DTREGJ"].ToString(), out dtRegj);
                   int.TryParse(rreshti["IDNIVELGJENERUES"].ToString(), out idNivelGjenerues);
                    int.TryParse(rreshti["IDKONFIGGJENERUES"].ToString(), out idKonfigGjenerues);
                    int.TryParse(rreshti["IDGJENERUES"].ToString(), out idGjenerues);
                    int.TryParse(rreshti["IDKRIJUESI"].ToString(), out idKrijuesi);
                    DateTime.TryParse(rreshti["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(rreshti["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    Pershkrimi = rreshti["PERSHKRIMI"].ToString();
                    int.TryParse(rreshti["IDRAPORTDESIGN"].ToString(), out idRaportDesing);

                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se kategorive te nivelit te dokumentit nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}
