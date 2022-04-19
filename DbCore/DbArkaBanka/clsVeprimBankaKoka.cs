using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Resources;
using DbCore.DbAdmin;
using DbCore.DbInventari;
using DbCore.DbKontabiliteti;
using DbCore.DbQendraKosto;
using DbCore.DbRegjistrim;
using DbCore.DbShare;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbArkaBanka
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne koken e nje dokumenti banke
    ///  (Te dhenat  merren nga tabela : T_VEPRIMBANKAKOKA)
    /// </summary>
    public class clsVeprimBankaKoka
    {
        #region Atribute

        private int idKoka;
        private int idBanka;
        private double kursi;
        private DateTime dateDokumenti;
        private DateTime dateRegjistrimi;
        private String nrDokumenti;
        private int nrReference;
        private String nrSerial;
        private String pershkrimiKoka;
        private int idMenyrePagese;
        private double vlera;
        private double vleraMonedhaBaze;
        private double komisioniBankar;
        private double komisioniMonedhaBaze;
        private string llojiVeprimit;
        private int idPerdoruesi;
        private int idLlojDokumenti;
        private int idStatusDokumenti;
        private int idNderViti;
        private int idKonfigAmbjente;
        private int idNivelGjenerues;
        private int idKonfigGjenerues;
        private int idGjenerues;
        private int idNivel;
        private int idDokNga;
        private int idDegeAdministrative;
        private int idNdermarje;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        public colVeprimBankaTrupi oColTrupi;
        private clsKokaFleteKontabel oKkokaFleteKontabel;

        //private clsKokaFleteKontabel oKokaFleteKontabelVDK;//flete kontabel me diferencat nga kursi
        private colDokumentLidhesKoka oDokumentLidhes;

        private colGjendjeKlientFurnitor oGjendjeKF;
        private colArkiva oArkiva;
        private string kodDegeAdministrative;
        private string kodBanka;
        private string kodMenyrePagese;
        private string arsyeAnullimi;
        private int idDokAnullimi;
        private string nrLlogari;

        /// <summary>
        /// id e grupimit te pare
        /// </summary>
        private int idGrup1;

        /// <summary>
        /// id e grupimit te dyte
        /// </summary>
        private int idGrup2;

        /// <summary>
        /// id e grupimit te trete
        /// </summary>
        private int idGrup3;

        private String nrKredite;
        private int idLlogKredite;
        private string shoqeria;
        private string customerNumber;
        private int idAutomjet;
        private string targa;
        private int idRaportDesing;
        private string financieri;
        private string dhenesiMarresi;
        private string arketari;
        private DataRow rreshti;
        private bool kase;
        private StatusAprovimi statusAprovimi;
        private bool printo;
        private int idKrijuesi;
        #endregion Atribute

        #region Konstruktoret

        /// <summary>
        /// konstruktore me 1 parameter
        /// </summary>
        /// <param name="idKoka">id e kokes</param>
        public clsVeprimBankaKoka(int idKoka)
        {
            var data = new clsDatabaseArkaBanka();
            mbushVeprimBankaKoka(data.merrVeprimBanke(idKoka));
            data.Dispose();
        }

        public clsVeprimBankaKoka(int idKoka, clsDatabaseArkaBanka data)
        {
            mbushVeprimBankaKoka(data.merrVeprimBanke(idKoka));
        }

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsVeprimBankaKoka()
        {
        }

        public clsVeprimBankaKoka(DataRow rreshti)
        {
            
            mbushVeprimBankaKoka(rreshti);
        }

        #endregion Konstruktoret

        #region Properties

        /// <summary>
        /// nr i klientit
        /// </summary>
        public string CustomerNumber
        {
            get
            {
                return customerNumber;
            }
            set
            {
                customerNumber = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdKoka
        {
            get { return idKoka; }
            set { idKoka = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e bankes ne te cilen po behet ky veprim.
        /// </summary>
        public int IdBanka
        {
            get { return idBanka; }
            set { idBanka = value; }
        }
        public bool Kase
        {
            get
            {
                return kase;
            }
            set
            {
                kase = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e kursit.
        /// </summary>
        public double Kursi
        {
            get { return kursi; }
            set { kursi = value; }
        }
               
        /// <summary>
        /// Kthen/Vendos daten e dokumentit.
        /// </summary>
        public DateTime DateDokumenti
        {
            get { return dateDokumenti; }
            set { dateDokumenti = value; }
        }

        /// <summary>
        /// Kthen/Vendos daten e regjistrimit te dokumentit.
        /// </summary>
        public DateTime DateRegjistrimi
        {
            get { return dateRegjistrimi; }
            set { dateRegjistrimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos numrin e dokumentit.
        /// </summary>
        public String NrDokumenti
        {
            get { return nrDokumenti; }
            set { nrDokumenti = value; }
        }

        /// <summary>
        /// Kthen/Vendos numrin e references.
        /// </summary>
        public int NrReference
        {
            get { return nrReference; }
            set { nrReference = value; }
        }

        /// <summary>
        /// id e grupimit te pare
        /// </summary>
        public int IdGrup1
        {
            get
            {
                return idGrup1;
            }
            set
            {
                idGrup1 = value;
            }
        }

        /// <summary>
        /// id e grupimit te dyte
        /// </summary>
        public int IdGrup2
        {
            get
            {
                return idGrup2;
            }
            set
            {
                idGrup2 = value;
            }
        }

        /// <summary>
        /// id e grupimit te trete
        /// </summary>
        public int IdGrup3
        {
            get
            {
                return idGrup3;
            }
            set
            {
                idGrup3 = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos numrin serial.
        /// </summary>
        public String NrSerial
        {
            get { return nrSerial; }
            set { nrSerial = value; }
        }

        /// <summary>
        /// Kthen/Vendos nje tekst pershkrues te veprimit qe po behet.
        /// </summary>
        public String PershkrimiKoka
        {
            get { return pershkrimiKoka; }
            set { pershkrimiKoka = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e menyres se pageses.
        /// </summary>
        public int IdMenyrePagese
        {
            get { return idMenyrePagese; }
            set { idMenyrePagese = value; }
        }

        // emri i klientit ose i shoqerise
        public string Shoqeria
        {
            get
            {
                return shoqeria;
            }
            set
            {
                shoqeria = value;
            }
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
        /// Kthen/Vendos vleren totale te veprimit bankar.
        /// </summary>
        public double Vlera
        {
            get { return vlera; }
            set { vlera = value; }
        }

        /// <summary>
        /// Kthen/Vendos vleren totale te konvertuar ne monedhe baze.
        /// </summary>
        public double VleraMonedhaBaze
        {
            get { return vleraMonedhaBaze; }
            set { vleraMonedhaBaze = value; }
        }

        /// <summary>
        /// Kthen/Vendos vleren e komisionit bankar.
        /// </summary>
        public double KomisioniBankar
        {
            get { return komisioniBankar; }
            set { komisioniBankar = value; }
        }

        /// <summary>
        /// Kthen/Vendos vleren e komisionit bankar te konvertuar ne monedhe baze.
        /// </summary>
        public double KomisioniMonedhaBaze
        {
            get { return komisioniMonedhaBaze; }
            set { komisioniMonedhaBaze = value; }
        }

        /// <summary>
        /// Kthen/Vendos llojin e veprimit qe po kryhet (terheqje, derdhje etj).
        /// </summary>
        public String LlojiVeprimit
        {
            get { return llojiVeprimit; }
            set { llojiVeprimit = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e perdoruesit qe po kryen kete veprim bankar.
        /// </summary>
        public int IdPerdoruesi
        {
            get { return idPerdoruesi; }
            set { idPerdoruesi = value; }
        }



        public int IdKrijuesi {
            get { return idKrijuesi; }
            set { idKrijuesi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e llojit te dokumentit.
        /// </summary>
        public int IdLlojDokumenti
        {
            get { return idLlojDokumenti; }
            set { idLlojDokumenti = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e statusit te dokumentit (i ruajtur, draft etj).
        /// </summary>
        public int IdStatusDokumenti
        {
            get { return idStatusDokumenti; }
            set { idStatusDokumenti = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e ndermarje vititi.
        /// </summary>
        public int IdNderViti
        {
            get { return idNderViti; }
            set { idNderViti = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e ndermarje .
        /// </summary>
        public int IdNdermarje
        {
            get { return idNdermarje; }
            set { idNdermarje = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e konfigurimit.
        /// </summary>
        public int IdKonfigAmbjente
        {
            get { return idKonfigAmbjente; }
            set { idKonfigAmbjente = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e nivelit te dokumentit qe e gjeneroi kete dokument.
        /// </summary>
        public int IdNivelGjenerues
        {
            get { return idNivelGjenerues; }
            set { idNivelGjenerues = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e konfigurimit te dokumentit qe e gjeneroi kete dokument.
        /// </summary>
        public int IdKonfigGjenerues
        {
            get { return idKonfigGjenerues; }
            set { idKonfigGjenerues = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e dokumentit qe e gjeneroi kete dokument kur behet nga nje ambjent tjeter.
        /// </summary>
        public int IdGjenerues
        {
            get { return idGjenerues; }
            set { idGjenerues = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e nivelit te dokumentit.
        /// </summary>
        public int IdNivel
        {
            get { return idNivel; }
            set { idNivel = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e dokumentit qe e gjeneroi kete dokument ne rastet e modifikimit.
        /// </summary>
        public int IdDokNga
        {
            get { return idDokNga; }
            set { idDokNga = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne deges administrative
        /// </summary>
        public int IdDegeAdministrative
        {
            get { return idDegeAdministrative; }
            set { idDegeAdministrative = value; }
        }

        public DateTime DtKrijimi
        {
            get { return dtKrijimi; }
            set { dtKrijimi = value; }
        }

        public DateTime DtModifikimi
        {
            get { return dtModifikimi; }
        }

        public String NrKredite
        {
            get
            {
                return nrKredite;
            }
            set
            {
                nrKredite = value;
            }
        }

        public int IdLlogKredite
        {
            get
            {
                return idLlogKredite;
            }
            set
            {
                idLlogKredite = value;
            }
        }
        public string ArsyeAnullimi
        {
            get
            {
                return arsyeAnullimi;
            }
            set
            {
                arsyeAnullimi = value;
            }
        }
        public string Arketari
        {
            get { return arketari; }
            set { arketari = value; }
        }
        public string NrLlogari
        {
            get
            {
                return nrLlogari;
            }
            set
            {
                nrLlogari = value;
            }
        }
        public string DhenesiMarresi
        {
            get { return dhenesiMarresi; }
            set { dhenesiMarresi = value; }
        }

        public string Financieri
        {
            get { return financieri; }
            set { financieri = value; }
        }

        /// <summary>
        /// Kthen/Vendos nje objekt te tipit <see cref="DbCore.DbKontabiliteti.clsKokaFleteKontabel"/>
        /// </summary>
        public clsKokaFleteKontabel OKokaFleteKontabel
        {
            get { return oKkokaFleteKontabel; }
            set { oKkokaFleteKontabel = value; }
        }

        /// <summary>
        /// Kthen/Vendos nje objekt te tipit <see cref="DbCore.DbRegjistrim.clsDokumentLidhesKoka"/>
        /// </summary>
        public colDokumentLidhesKoka ODokumentLidhes
        {
            get { return oDokumentLidhes; }
            set { oDokumentLidhes = value; }
        }

        /// <summary>
        /// Kthen/Vendos nje objekt te tipit <see cref="DbCore.DbRegjistrim.clsGjendjeKlientFurnitor"/>
        /// </summary>
        public colGjendjeKlientFurnitor OGjendjeKF
        {
            get { return oGjendjeKF; }
            set { oGjendjeKF = value; }
        }
        public int IdDokAnullimi
        {
            get
            {
                return idDokAnullimi;
            }
            set
            {
                idDokAnullimi = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e automjetit.
        /// </summary>
        public int IdAutomjet
        {
            get { return idAutomjet; }
            set { idAutomjet = value; }
        }

        /// <summary>
        /// Kthen/Vendos targen.
        /// </summary>
        public string Targa
        {
            get { return targa; }
            set { targa = value; }
        }

        public int IdRaportDesing
        {
            get { return idRaportDesing; }
            set { idRaportDesing = value; }
        }

        public string KodDegeAdministrative
        {
            get
            {
                return kodDegeAdministrative;
            }
        }
        public bool Printo { get => printo; set => printo = value; }
        public IDictionary<string, object> HfArkiva { get; set; }

        #endregion Properties

        #region Metoda Publike

        public clsMesazh krijoVeprimeBanke(int idbanka, string kodbanka, double kurs, DateTime datedokumenti, DateTime dateregjistrimi, String nrdokumenti, int nrreference, String nrserial, String pershkrimikoka, int idmenyrepagese, string kodmenyrepagese, double vlerakoka, double vleramonedhabaze, double komisionibankar, double komisionimonedhabaze, String llojiveprimit, int idperdoruesi, int idllojdokumenti, int idstatusdokumenti, int idnderviti, int idkonfigambjente, int idnivelgjenerues, int idkonfiggjenerues, int idgjenerues, int idnivel, int iddoknga, int idDegeAdministrative, string koddege, int idndermarje, int idllogkrediti, colVeprimBankaTrupi trupi, bool mekontabilizim, int idperiudha, int idmonedha, string nrkredite, int idgrup1, int idgrup2, int idgrup3, clsKonfigurimAmbjenti konfigdokLidhes, object[] nivele, clsDatabaseArkaBanka db, clsKokaShitje kokashitje, out string shfaqmesazhapolupe, out string shfaqmesazhapolupeVDK, colTrupiQendraKosto trupivjeterqendra, int idkokaeksistuese, string shoqeria, string customernumber, int idAuto, string targa, int idrap, string financieri, string dhenesiMarresi, string arketari, bool kase, StatusAprovimi statusapp, string arsyeAnullimi, int iddokanullimi, string nrllogari, IDictionary<string, object> hfArkiva, int idkategori, int idPerdoruesPerKontroll, int idkrijuesi)
        {
            bool printo = clsAtributeTrupi.merrVleredefaultSipasKontrollitDheKonfigurimit(idkonfigambjente, "cbPrinto", 301) == "true";
            
            return krijoVeprimeBanke(idbanka, kodbanka, kurs, datedokumenti, dateregjistrimi, nrdokumenti, nrreference, nrserial, pershkrimikoka, idmenyrepagese, kodmenyrepagese, vlerakoka, vleramonedhabaze, komisionibankar, komisionimonedhabaze, llojiveprimit, idperdoruesi, idllojdokumenti, idstatusdokumenti, idnderviti, idkonfigambjente, idnivelgjenerues, idkonfiggjenerues, idgjenerues, idnivel, iddoknga, idDegeAdministrative, koddege, idndermarje, idllogkrediti, trupi, mekontabilizim, idperiudha, idmonedha, nrkredite, idgrup1, idgrup2, idgrup3, konfigdokLidhes, nivele, db, kokashitje, out shfaqmesazhapolupe, out shfaqmesazhapolupeVDK, trupivjeterqendra, idkokaeksistuese, shoqeria, customernumber, idAuto, targa, idrap, financieri, dhenesiMarresi, arketari, kase, statusapp, arsyeAnullimi, iddokanullimi, nrllogari, hfArkiva, idkategori, idPerdoruesPerKontroll, printo, idkrijuesi);
        }

        public clsMesazh krijoVeprimeBanke(int idbanka, string kodbanka, double kurs, DateTime datedokumenti, DateTime dateregjistrimi, String nrdokumenti, int nrreference, String nrserial, String pershkrimikoka, int idmenyrepagese, string kodmenyrepagese, double vlerakoka, double vleramonedhabaze, double komisionibankar, double komisionimonedhabaze, String llojiveprimit, int idperdoruesi, int idllojdokumenti, int idstatusdokumenti, int idnderviti, int idkonfigambjente, int idnivelgjenerues, int idkonfiggjenerues, int idgjenerues, int idnivel, int iddoknga, int idDegeAdministrative, string koddege, int idndermarje, int idllogkrediti, colVeprimBankaTrupi trupi, bool mekontabilizim, int idperiudha, int idmonedha, string nrkredite, int idgrup1, int idgrup2, int idgrup3, clsKonfigurimAmbjenti konfigdokLidhes, object[] nivele, clsDatabaseArkaBanka db, clsKokaShitje kokashitje, out string shfaqmesazhapolupe, out string shfaqmesazhapolupeVDK, colTrupiQendraKosto trupivjeterqendra, int idkokaeksistuese, string shoqeria, string customernumber, int idAuto, string targa, int idrap, string financieri, string dhenesiMarresi, string arketari, bool kase, StatusAprovimi statusapp, string arsyeAnullimi, int iddokanullimi, string nrllogari, IDictionary<string, object> hfArkiva, int idkategori, int idPerdoruesPerKontroll, bool printo, int idkrijuesi)
        {
            shfaqmesazhapolupe = "jo";
            shfaqmesazhapolupeVDK = "jo";
            var dbregj = new clsDatabaseRegjistrim(db);
            var dbshare = new clsDatabaseShare(db);
            var dbkont = new clsDatabaseKontabilitet(db);
            //dbshare.vendosManager(db );
            idBanka = idbanka;
            kodBanka = kodbanka;
            kursi = kurs;
            dateDokumenti = datedokumenti;
            dateRegjistrimi = dateregjistrimi;
            nrDokumenti = nrdokumenti;
            nrReference = nrreference;
            nrSerial = nrserial;
            pershkrimiKoka = pershkrimikoka;
            idMenyrePagese = idmenyrepagese;
            kodMenyrePagese = kodmenyrepagese;
            vlera = vlerakoka;
            this.kase = kase;
            idGrup1 = idgrup1;
            idGrup2 = idgrup2;
            idGrup3 = idgrup3;
            this.targa = targa;
            vleraMonedhaBaze = vleramonedhabaze;
            komisioniBankar = komisionibankar;
            komisioniMonedhaBaze = komisionimonedhabaze;
            llojiVeprimit = llojiveprimit;
            idPerdoruesi = idperdoruesi;
            idLlojDokumenti = idllojdokumenti;
            idStatusDokumenti = idstatusdokumenti;
            idNderViti = idnderviti;
            idKonfigAmbjente = idkonfigambjente;
            idNivelGjenerues = idnivelgjenerues;
            idKonfigGjenerues = idkonfiggjenerues;
            idGjenerues = idgjenerues;
            idNivel = idnivel;
            idDokNga = iddoknga;
            idNdermarje = idndermarje;
            nrKredite = nrkredite;
            customerNumber = customernumber;
            this.shoqeria = shoqeria;
            idLlogKredite = idllogkrediti;
            this.idDegeAdministrative = idDegeAdministrative;
            kodDegeAdministrative = koddege;
            idAutomjet = idAuto;
            idRaportDesing = idrap;
            oColTrupi = trupi;
            this.financieri = financieri;
            this.dhenesiMarresi = dhenesiMarresi;
            this.arketari = arketari;
            nrLlogari = nrllogari;
            statusAprovimi = statusapp;            
            this.arsyeAnullimi = arsyeAnullimi;
            idDokAnullimi = iddokanullimi;
            HfArkiva = hfArkiva;
            this.printo = printo;
            IdKrijuesi = idkrijuesi;
            var mesazh = kontrollo(nrkredite, idPerdoruesPerKontroll, db);
            if (!mesazh.Status)
                return mesazh;
            ODokumentLidhes = new colDokumentLidhesKoka();
            var idkategoriavdk = 10;
            
            var idLlojDok = 68;//NKLD
            if (mekontabilizim)
            {
                var pershkrim = "";
                if (pershkrimiKoka != String.Empty)
                {
                    pershkrim = pershkrimiKoka;
                }
                else
                {
                    switch (llojiVeprimit.ToLower())
                    {
                        case "terheqje":
                            pershkrim = "Nga terheqjet ne banke";
                            break;
                        case "pagese":
                            pershkrim = "Nga pagesat ne arke";
                            break;
                        case "derdhje":
                            pershkrim = "Nga derdhjet ne banke";
                            break;
                        case "arketim":
                            pershkrim = "Nga arketimet ne arke";
                            break;
                    }
                }
           
                var dbqendra = new clsDatabaseQendraKosto(dbkont);
                colObjektivaKosto objektivat;
                List<double> vleratobjektiva; List<double> vleratobjektivamonbaze;
                List<int> idllogobj;
                List<(int, double)> emratkf;
                colTrupatFletetKontabel trupatperGjendjekf;
                List<string> rreshtakf;
                oKkokaFleteKontabel = clsKokaFleteKontabel.gjeneroKontabilizimBanka(IdKoka, idNivel, idKonfigAmbjente, dateDokumenti, nrDokumenti, idNdermarje, idNderViti, idperdoruesi, dateRegjistrimi, trupi, pershkrim, idDokNga, idLlojDokumenti, idperiudha, idkategori, vlera, kursi, idmonedha, out emratkf, out rreshtakf, out trupatperGjendjekf, komisioniBankar, idbanka, nrkredite, db, out objektivat, out vleratobjektiva, out vleratobjektivamonbaze, out idllogobj, idDegeAdministrative, 0, 0, out shfaqmesazhapolupe, trupivjeterqendra);
                // gjenerimi i gjendjes kf
                oGjendjeKF = gjeneroGjendjeKf(idNivel, nrDokumenti, dateDokumenti, dateRegjistrimi, emratkf.Select(x => x.Item1).ToList(), rreshtakf, trupatperGjendjekf);
                if (konfigdokLidhes.IdKonfigAmbjente == 0) return new clsMesazh(true, "Dokumenti u krijua me sukses!");
                var cls = new colDokumentLidhesKoka(idkokaeksistuese, idLlojDokumenti, dbregj);
                var kontablidhes = clsAlternativaKushti.getAlternativa(konfigdokLidhes.IdKonfigAmbjente, "GJK", dbshare) == "Jo"? false: true;
                var njihDifKursi = clsAlternativaKushti.getAlternativa(konfigdokLidhes.IdKonfigAmbjente, "NFHNDK", dbshare) == "Po";
                var pershk = "";
                if (pershkrimiKoka != String.Empty)
                {
                    pershk = pershkrimiKoka;
                }
                else
                {
                    switch (llojiVeprimit)
                    {
                        case "Terheqje":
                            pershk = "Diferenca nga kursi (Nga terheqjet ne banke)";
                            break;
                        case "Pagese":
                            pershk = "Diferenca nga kursi (Nga pagesat ne arke)";
                            break;
                        case "Derdhje":
                            pershk = "Diferenca nga kursi (Nga derdhjet ne banke)";
                            break;
                        case "Arketim":
                            pershk = "Diferenca nga kursi (Nga arketimet ne arke)";
                            break;
                    }
                }
                foreach (var KfKurs in emratkf.Select(x=>x.Item1).Distinct().ToList())
                {
                    var dokumentiKoka = new clsDokumentLidhesKoka();
                    var coltrupidok = krijoTrupDokumentLidhes(kursi, KfKurs, trupi, idnivel, nivele, dbregj);

                    dokumentiKoka.krijoDokumentLidhesKoka(nrDokumenti, dateDokumenti, dateRegjistrimi, KfKurs, idKoka, idLlojDokumenti, idNdermarje, idNderViti, konfigdokLidhes.IdNivel, konfigdokLidhes.IdKonfigAmbjente, 0, IdNivel, IdKonfigAmbjente, idStatusDokumenti, coltrupidok, idperdoruesi);

                    if (dokumentiKoka.OColTrupi.Count > 1 && idStatusDokumenti == 1 && kontablidhes && njihDifKursi)//nese eshte bere lidhje dokumentash te gjenerohen diferencat nga kursi
                    {
                        var kokaqendra = new clsKokaQendraKosto();
                        if (cls.Count != 0)
                            if (cls.Any(k => k.IdKlientFurnitor == KfKurs))
                            {
                                var newclsKokaFleteKontabel = new clsKokaFleteKontabel(IdKoka, idkategoriavdk, dbkont);
                                if (!string.IsNullOrEmpty(newclsKokaFleteKontabel.NrDukumentiKokaFleteKontabel))
                                {
                                    kokaqendra.KtheKokaQKSipasIDGjeneruesDheKonfigMeTrup(newclsKokaFleteKontabel.IdKokaFleteKontabel, newclsKokaFleteKontabel.IdKonfigAmbjente, dbqendra);
                                }
                            }
                        List<int> emratkfvdk;
                        colTrupatFletetKontabel trupatperGjendjekfvdk;
                        List<string> rreshtakfvdk;
                        dokumentiKoka.OFleteKontabel = clsKokaFleteKontabel.gjeneroKontabilizimVDK(dokumentiKoka.IdKoka, dokumentiKoka.IdNivel, dokumentiKoka.IdKonfigAmbjente, dokumentiKoka.DateDokumenti, dokumentiKoka.NrLidhje, dokumentiKoka.IdNdermarje, dokumentiKoka.IdNderViti, IdPerdoruesi, dokumentiKoka.DateRegjistrimi, oColTrupi, pershk, 0, idLlojDok, idperiudha, idkategoriavdk, Kursi, idmonedha, out emratkfvdk, out rreshtakfvdk, out trupatperGjendjekfvdk, dokumentiKoka.IdKlientFurnitor, dokumentiKoka.OColTrupi, idLlojDokumenti, db, kokashitje, out objektivat, out vleratobjektiva, out vleratobjektivamonbaze, out idllogobj, idDegeAdministrative, 0, 0, out shfaqmesazhapolupeVDK, kokaqendra.ColTrupi);
                        if (dokumentiKoka.OFleteKontabel.OColTrupi.Count > 0)
                            dokumentiKoka.OGjendjeKF = colGjendjeKlientFurnitor.KrijoGjendjetKlientFurnitor(emratkfvdk, dokumentiKoka.IdNivel, dokumentiKoka.NrLidhje, dokumentiKoka.DateDokumenti, dokumentiKoka.DateRegjistrimi, trupatperGjendjekfvdk, rreshtakfvdk);
                    }
                    ODokumentLidhes.Add(dokumentiKoka);
                }
            }
            else
            {
                oKkokaFleteKontabel = new clsKokaFleteKontabel();
                oGjendjeKF = new colGjendjeKlientFurnitor();
            }
            //   kontrollo(nrkredite,db);
            return new clsMesazh(true, "Dokumenti u krijua me sukses!");
        }

        /// <summary>
        /// kthen llojin e Veprimit: Arketim,Derdhje,Pagese,Terheqje ne baze te input
        /// </summary>
        /// <param name="isShitje"></param>
        /// <param name="arkabanka"></param>
        /// <param name="totali"></param>
        /// <returns></returns>
        public static string llojVeprimi(bool isShitje, string arkabanka, double totali)
        {
            var isVleraTotalPozitive = totali >= 0;
            var isArketimDerdhje = isVleraTotalPozitive ? isShitje : !isShitje;
            if (isArketimDerdhje)
            {
                switch (arkabanka)
                {
                    case "arka":
                        return "Arketim";
                    case "banka":
                        return "Derdhje";
                }
                throw new MyException("arkabanka mund te jete ose arka ose banka");
            }
            switch (arkabanka)
            {
                case "arka":
                    return "Pagese";
                case "banka":
                    return "Terheqje";
            }
            throw new MyException("arkabanka mund te jete ose arka ose banka");
        }

        private static colGjendjeKlientFurnitor gjeneroGjendjeKf(int idniveli, string nrdok, DateTime data, DateTime dtregj, List<int> emrakf, List<string> rreshtakf, colTrupatFletetKontabel trupiPerGjendjeKF)
        {
            return colGjendjeKlientFurnitor.KrijoGjendjetKlientFurnitor(emrakf, idniveli, nrdok, data, dtregj, trupiPerGjendjeKF, rreshtakf);
        }

        private colDokumentLidhesTrupi krijoTrupDokumentLidhes(double kursi, int idklientfurnitor, colVeprimBankaTrupi trupibanka, int idnivelbanka, object[] nivele, clsDatabaseRegjistrim db)
        {
            var coltrup = new colDokumentLidhesTrupi();
            double vleraLidhjes = 0;
            var j = 0;
            var llojivepBankes = "1";
            var dokLidhes = new clsDokumentLidhesTrupi
            {
                IdDokumenti = -5,
                LlojDokumenti = idnivelbanka.ToString()
            };
            foreach (var t in trupibanka)
            {
                if ((t.Lloji == "Furnitor" || t.Lloji == "Klient"))//nese ka zgjedhur klient apo furnitor
                {
                    if (t.IdSubjekti == idklientfurnitor/* && t.KMK == kursi*/)
                    {
                        var idFatura = t.IdFatura;
                        vleraLidhjes = t.VleraPaguar;

                        if (idFatura != 0)//nese eshte zgjedhur nje fature per tu likujduar
                        {
                            if ((t.Lloji == "Klient" && t.DebiKredi == "Kredi") || (t.Lloji == "Furnitor" && t.DebiKredi == "Debi"))// dokumenti kryesor apo i lidhur varet nese e rrit apo e zvogelon detyrimin e kf
                                llojivepBankes = "1";
                            else llojivepBankes = "0";
                            //dokumenti kryesor eshte fatura
                            var dokKryesor = new clsDokumentLidhesTrupi
                            {
                                IdDokumenti = idFatura,
                                LlojDokumenti = nivele[j].ToString(),
                                Statusi = llojivepBankes == "1" ? "0" : "1",
                                VleraLidhjes = (t.VleraPaguar*kursi)/t.KMK
                            };
                            //dokumentat kryesore i ruajme me status 0
                            //vlera ne monedhen e pageses ne fillim kthehet ne vlere ne monedhe baze pastaj ne vlere ne monedhen e fatures
                            coltrup.Add(dokKryesor);

                            //dokumenti lidhes eshte veprimi i bankes

                            dokLidhes.VleraLidhjes += t.VleraPaguar;//vlera ne monedhen e pageses
                        }
                    }
                }
                j++;
            }
            dokLidhes.Statusi = llojivepBankes; //dokumentat lidhes i ruajme me status 1
            coltrup.Add(dokLidhes);
            return coltrup;
        }

   

        public clsMesazh krijoVeprimeBankePerImport(string kodbanka, double kurs, DateTime datedokumenti, DateTime dateregjistrimi, string nrdokumenti, int nrreference, string nrserial, string pershkrimikoka, string kodmenyrepagese, double vlerakoka, double vleramonedhabaze, double komisionibankar, double komisionimonedhabaze, string nenkategoria, int idperdoruesi, int idllojdokumenti, int idstatusdokumenti, int idnderviti, int idnivelgjenerues, int idkonfiggjenerues, int idgjenerues, int iddoknga, string kodDegeAdministrative, int idndermarje, colVeprimBankaTrupi trupi, bool mekontabilizim, int idMonArkaBanka, string nrkredite, string grup1, string grup2, string grup3, clsKonfigurimAmbjenti konfigdokLidhes, object[] nivele, string shoqeria, string customernumber, int idAutomjet, string targa, int idrap, int idGjuha, ResourceManager rm, CultureInfo ci, clsPeriudhaKontabel periudha, int idStatusDok, int llojKursi, clsKonfigurimAmbjenti konfigAmbjenti, clsMonedha monedhaNderm, int idkategori, int idPerdoruesPerKontroll,
            int idkrijuesi, DbData dbData)
        {
            if (MbylljePeriudhe.PeriodClosing.IsPeriodClosed(datedokumenti, dbData.MyScopeDbManager.ConnectionName, idndermarje, (llojiVeprimit == "derdhje" || llojiVeprimit == "terheqje") ? KategoriDokumenti.VeprimeBanke : KategoriDokumenti.VeprimeArke, konfigAmbjenti.IdKonfigAmbjente))
                return new MesazhGabimi(MessagesResource.Messages["msgPeriodIsClosed"]);

            if (trupi.Count == 0)
                return new clsMesazh(false, "Trupi i dokumentit nuk duhet te jete bosh!");
            if (nenkategoria == "")
                return new clsMesazh(false, "Plotesoni nivelin e regjistrimit!");

            //int idNivel = clsNivelRegjistrimi.ktheIdNivelRegjistrimiSipasKodi(nenkategoria, idndermarje);
            var nivel = new clsNivelRegjistrimi();
            nivel.mbushNivelRegjistrimiSipasKodit(nenkategoria, idndermarje.ToString());
            if (nivel.IdNivel == 0)
                return new clsMesazh(false, "Nenkategoria " + nenkategoria + " nuk ekziston!");

            if (nivel.IdKategori != konfigAmbjenti.IdKategori || nivel.IdNivel != konfigAmbjenti.IdNivel)
                return new clsMesazh(false, "Lloji i dokumentit " + konfigAmbjenti.KodKonfigAmbjente + " nuk i perket nenkategorise " + nivel.Kodi + "!");

            if (datedokumenti.Year != clsNdermarrjeViti.ktheKodVitiSipasIdNdermViti(idnderviti))
                return new clsMesazh(false, rm.GetString("msgDataNukPerketVititUshtrimor", ci));

            var banka = new clsBanka();
            banka.mbushBankeSipasKodit(kodbanka, idndermarje);
            if (banka.IdBanka == 0)
                return new clsMesazh(false, "Arka/Banka " + kodbanka + " nuk ekziston!");

            if (!banka.AktivBanka)
                return new clsMesazh(false, "Arka/Banka " + kodbanka + " nuk eshte aktive!");

            if (!banka.LlojArkaBanka && idkategori == 4)
            {
                return new clsMesazh(false, "Nuk mund te zgjidhni arke per veprimet me banken!");
            }
            if (banka.LlojArkaBanka && idkategori == 3)
            {
                return new clsMesazh(false, "Nuk mund te zgjidhni banke per veprimet me arken!");
            }
            if (monedhaNderm.IdMonedha == banka.IdMonedhaBanka && kurs != 1)
            {
                return new clsMesazh(false, "Per arka ne monedhen e ndermarrjes, kursi duhet te jete i barabarte me 1!");
            }
            idDegeAdministrative = 0;
            this.kodDegeAdministrative = "";
            if (kodDegeAdministrative != "")
            {
                var deg = new clsDegeAdministrative(kodDegeAdministrative, idndermarje);
                if (deg.IdDegeAdministrative == 0)
                    return new clsMesazh(false, "Dega administrative " + kodDegeAdministrative + " nuk ekziston!");
                if (!deg.Aktiv)
                    return new clsMesazh(false, "Dega administrative " + kodDegeAdministrative + " nuk eshte aktive!");
                idDegeAdministrative = deg.IdDegeAdministrative;
                this.kodDegeAdministrative = deg.Kodi;
            }

            idMenyrePagese = clsFunksione.ktheMenyrePageseSipasLlojit(kodmenyrepagese);

            String mesazhGabimi;
            if (!clsFunksione.checkPeriudheKontabel(out mesazhGabimi, datedokumenti, periudha, idStatusDok))
                return new clsMesazh(false, mesazhGabimi);

            if (datedokumenti.Date.Year != clsNdermarrjeViti.ktheKodVitiSipasIdNdermViti(idnderviti))
                return new clsMesazh(false, "Data nuk i perket vitit ushtrimor te zgjedhur!");
            if (komisionibankar != 0 && banka.Komisioni == 0)
                return new clsMesazh(false, "Banka " + kodbanka + " nuk ka llogari komisioni!");
            komisioniBankar = komisionibankar;

            int idGrup1 = 0, idGrup2 = 0, idGrup3 = 0;
            if (grup1 != "")
            {
                if (!clsGrupimDokumentiKoka.ekzistonGrup(grup1, idndermarje, 1))
                    return new clsMesazh(false, "Grupi " + grup1 + " nuk ekziston!");
                idGrup1 = clsGrupimDokumentiKoka.ktheIdGrupDokumentash(grup1, idndermarje, 1);
            }

            if (grup2 != "")
            {
                if (!clsGrupimDokumentiKoka.ekzistonGrup(grup2, idndermarje, 2))
                    return new clsMesazh(false, "Grupi " + grup2 + " nuk ekziston!");
                idGrup2 = clsGrupimDokumentiKoka.ktheIdGrupDokumentash(grup2, idndermarje, 2);
            }

            if (grup3 != "")
            {
                if (!clsGrupimDokumentiKoka.ekzistonGrup(grup3, idndermarje, 3))
                    return new clsMesazh(false, "Grupi " + grup3 + " nuk ekziston!");
                idGrup3 = clsGrupimDokumentiKoka.ktheIdGrupDokumentash(grup3, idndermarje, 3);
            }
            vleramonedhabaze = vlerakoka * kurs;
            komisioniBankar = komisionibankar;
            komisioniMonedhaBaze = komisionibankar * kurs;

            using (var db = new clsDatabaseArkaBanka())
            {
                if (db.ekzistonVeprimBanke(banka.IdBanka, datedokumenti, nrdokumenti, konfigAmbjenti.IdKonfigAmbjente))
                    return new clsMesazh(false, "Ekziston nje regjistrim me te njejtin numer dokumenti!");
            }

            var mesazh = kontrolloTotalet(konfigAmbjenti.IdKonfigAmbjente, monedhaNderm.IdMonedha, trupi, nivel.Kodi, vlerakoka);
            if (!mesazh.Status)
                return mesazh;

            var shfaqmesazhapolupe = "jo"; var shfaqmesazhapolupeVDK = "jo";
            return krijoVeprimeBanke(banka.IdBanka, banka.KodiBanka, kurs, datedokumenti, dateregjistrimi, nrdokumenti, nrreference, nrserial, pershkrimikoka, idMenyrePagese, kodmenyrepagese, vlerakoka, vleramonedhabaze, komisioniBankar, komisioniMonedhaBaze, nenkategoria, idperdoruesi, idllojdokumenti, idstatusdokumenti, idnderviti, konfigAmbjenti.IdKonfigAmbjente, idnivelgjenerues, idkonfiggjenerues, idgjenerues, nivel.IdNivel, iddoknga, idDegeAdministrative, kodDegeAdministrative, idndermarje, idLlogKredite, trupi, mekontabilizim, periudha.IdPeriudha, idMonArkaBanka, nrkredite, idGrup1, idGrup2, idGrup3, konfigdokLidhes, nivele, new clsDatabaseArkaBanka(), null, out shfaqmesazhapolupe, out shfaqmesazhapolupeVDK, new colTrupiQendraKosto(), 0, shoqeria, customernumber, idAutomjet, targa, idrap, "", "", "", false, StatusAprovimi.Undefined, "", 0, "",null, idkategori, idPerdoruesPerKontroll,false, idperdoruesi);




        }

        private clsMesazh kontrolloTotalet(int idKonfigAmbjenti, int idMonNderm, colVeprimBankaTrupi colTrupi, string kodNiveli, double vleraKoka)
        {
            var formatNrPerKonfig = new clsFormatiKonfig();
            formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(idKonfigAmbjenti);//te gjeneruarat
            var formatMonedhe = new clsFormatKonfigTrup();
            if (formatNrPerKonfig.IdFormatKonfig > 0)
                formatMonedhe = formatNrPerKonfig.KonfigTrupi.merrFormatSipasMonedhes(idMonNderm);
            else
                formatMonedhe = new clsFormatKonfigTrup(clsFormatKonfigTrup.defaultFormatSasia, clsFormatKonfigTrup.defaultFormatCmimi, clsFormatKonfigTrup.defaultFormatVlefta, clsFormatKonfigTrup.defaultFormatZbritja, clsFormatKonfigTrup.defaultFormatStringSasia, clsFormatKonfigTrup.defaultFormatStringCmimi, clsFormatKonfigTrup.defaultFormatStringVlefta, clsFormatKonfigTrup.defaultFormatStringZbritja);
            double shumadebi = 0;
            double shumakredi = 0;
            foreach (var rreshTrupi in colTrupi)
            {
                if (rreshTrupi.DebiKredi == "Debi")
                    shumadebi += rreshTrupi.VleraPaguar;
                else
                    shumakredi += rreshTrupi.VleraPaguar;
            }

            var maxSingleErr = double.Parse((clsFunksione.krijoNumer(formatMonedhe.ShifraPasPresjesZbritja, "0") + "5"), CultureInfo.InvariantCulture);

            if ((kodNiveli.ToLower() == "terheqje" || kodNiveli.ToLower() == "pagese"))
            {
                if (!(Math.Round(Math.Abs(shumadebi - shumakredi), formatMonedhe.ShifraPasPresjesVlefta) == Math.Round(Math.Abs(vleraKoka), formatMonedhe.ShifraPasPresjesVlefta) && shumadebi - shumakredi >= maxSingleErr))
                    return new clsMesazh(false, "Veprimi nuk eshte i kuadruar!");
            }

            if ((kodNiveli.ToLower() == "derdhje" || kodNiveli.ToLower() == "arketim"))
            {
                if (!(Math.Round(Math.Abs(shumadebi - shumakredi), formatMonedhe.ShifraPasPresjesVlefta) == Math.Round(Math.Abs(vleraKoka), formatMonedhe.ShifraPasPresjesVlefta) && shumadebi - shumakredi <= maxSingleErr))
                    return new clsMesazh(false, "Veprimi nuk eshte i kuadruar!");
            }
            return new clsMesazh(true, "Kontrollet u kaluan me sukses!");
        }
        public static bool kontrolloAnulluar(int idkoka)
        {
            using (var dbKokaMagazina = new clsDatabaseArkaBanka())
            {
                var sukses = dbKokaMagazina.KontrolloAnulluar(idkoka);
                return sukses;
            }
        }
        private clsMesazh kontrollo(string nrkredite, int idPerdoruesPerKontroll, clsDatabaseArkaBanka db)
        {
            var dbregj = new clsDatabaseRegjistrim(db);
            var dbkont = new clsDatabaseKontabilitet(db);
            if (nrDokumenti == "")
                return new clsMesazh(false, "Numri i dokumentit nuk mund te jete bosh");
            if (dateDokumenti.ToShortDateString() == "01/01/0100")
                return new clsMesazh(false, "Zgjidhni daten e dokumentit!");
            if (dateRegjistrimi == null || dateRegjistrimi.ToShortDateString() == "01/01/0100")
                return new clsMesazh(false, "Zgjidhni daten e regjistrimit!");

            if (idMenyrePagese == -1)
                return new clsMesazh(false, "Menyra e pageses nuk ekziston!");
            if (vlera < 0)
                return new clsMesazh(false, "Vlera e dokumentit te bankes nuk mund te jete negative!");
            if (!String.IsNullOrEmpty(kodDegeAdministrative))
            {
                var deg = new clsDegeAdministrative(kodDegeAdministrative, idNdermarje, dbregj);
                if (deg.IdDegeAdministrative == 0)
                    return new clsMesazh(false, $"Dega administrative {kodDegeAdministrative} nuk ekziston!");
                if (!deg.Aktiv)
                    return new clsMesazh(false, $"Dega administrative {kodDegeAdministrative} nuk eshte aktive!");
            }
            if (string.IsNullOrEmpty(kodBanka))
                return new clsMesazh(false, "Zgjidhni arka/banken!");
            if (!clsBanka.ekziston(kodBanka, idNdermarje, db).Status)
                return new clsMesazh(false, "Arka/Banka nuk ekziston!");
            var banka = new clsBanka();
            banka.mbushBankeSipasKoditMeAutorizime(kodBanka, idNdermarje, idPerdoruesPerKontroll, db);
            if (banka.IdBanka < 1)
                return new clsMesazh(false, "Ju nuk keni autorizim ne kete arke/banke");
            if (!banka.AktivBanka)
                return new clsMesazh(false, "Arka/Banka nuk eshte aktive!");
            if (!string.IsNullOrEmpty(nrkredite))
            {
                if (!clsLlogari.ekzistonLlogari(nrkredite, idNdermarje, dbkont))
                    return new clsMesazh(false, "Llogaria nuk ekziston!");
            }
            if (!string.IsNullOrEmpty(nrkredite) && !clsLlogari.ekzistonLlogari(nrkredite, idNdermarje, dbkont))
                return new clsMesazh(false, "Llogaria e kreditit nuk ekziston!");
            if (!string.IsNullOrEmpty(targa) && !clsAutomjete.ekzistonAutomjetSipasTarges(idNdermarje, targa, new clsDatabaseInventari(db)))
                return new clsMesazh(false, "Automjeti nuk ekziston!");
            return new clsMesazh(true, "Kontrollet u kaluan me sukses!");
        }

        /// <summary>
        /// Ekzekuton nje transaksion per te ruajtur nje objekt clsVeprimBankaKoka dhe trupin e tij ne DB.
        ///<param name="koka">Objekt i tipit clsVeprimBankaKoka qe do te ruhet ne DB</param>
        ///<param name="eshteModifikim">Tregon nese funksioni po therritet gjate modifikimit te nje veprimi banke apo jo</param>
        ///Therret funksionin <see cref="DbCore.DbArkaBanka.clsDatabaseArkaBanka.ruajVeprimBankeKoka"/>
        ///Therret funksionin <see cref="DbCore.DbArkaBanka.clsDatabaseArkaBanka.ruajVeprimBankeTrupi"/>
        ///Therret funksionin <see cref="DbCore.DbKontabiliteti.clsKokaFleteKontabel.Ruaj"/> per te ruajtur kontabilitetin (bashke me diferencat nga kursi)
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te transaksionit (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        public clsMesazh ruajVeprimBanke(clsVeprimBankaKoka koka, bool eshteModifikim, clsDatabaseArkaBanka dbArkaBanka, bool vjenNgaImportSQL, string idDokImport, string emerTabKoka, string primaryKeyEmerFushe, string emerFusheNdermarrje, int idskema, StatusAprovimi statusAprovimi, int idetapa, string serverUrl, bool perBRM)
        {
            clsMesazh mesazh = null;
            var kontMesazh = new clsMesazh(true);
            var mesazhRegjistrimi = new clsMesazh(true);
            clsPerdorues perdoruesi = null;
            clsKonfigurimAmbjenti konf = null;
            if (!string.IsNullOrEmpty(this.NrSerial))
            {
                var nivelRegjistrimi = new clsNivelRegjistrimi();
                nivelRegjistrimi.mbushNivelRegjistrimiSipasIdPaKonvertime(this.idNivel);
                if (nivelRegjistrimi.NrSerialUnik && dbArkaBanka.ekzistonNumerSerialUnikPerKeteNivelDheNdermarrjeKokaArkaBanka(eshteModifikim ? this.IdKoka : 0, this.IdNivel, this.nrSerial, this.idNdermarje))
                    return new clsMesazh(false, "Ky numer serial dokumenti ekziston! Ju lutem vendosni nje numer tjeter!");
            }
            if (!eshteModifikim && dbArkaBanka.ekzistonVeprimBanke(IdBanka, DateDokumenti, NrDokumenti, IdKonfigAmbjente))
            {
                return new clsMesazh(false, MessagesResource.Messages["msgEkziston1VeprimBankarMeTeNjejtatTeDhena"]);
            }
            
            int idk;
            mesazh = dbArkaBanka.ruajVeprimBankeKoka(out idk, koka.IdBanka, koka.Kursi, koka.DateDokumenti, koka.DateRegjistrimi, koka.NrDokumenti, koka.NrReference, koka.NrSerial, koka.PershkrimiKoka, koka.IdMenyrePagese, koka.Vlera, koka.VleraMonedhaBaze, koka.KomisioniBankar, koka.KomisioniMonedhaBaze, koka.LlojiVeprimit, koka.IdPerdoruesi, koka.IdLlojDokumenti, koka.IdStatusDokumenti, koka.IdNderViti, koka.IdKonfigAmbjente, koka.IdNivelGjenerues, koka.IdKonfigGjenerues, koka.IdGjenerues, koka.IdNivel, koka.IdDokNga, koka.idDegeAdministrative, koka.idNdermarje, koka.IdGrup1, koka.IdGrup2, koka.IdGrup3, koka.idLlogKredite, shoqeria, customerNumber, koka.idAutomjet, koka.idRaportDesing, koka.Financieri, koka.DhenesiMarresi, koka.Arketari, koka.Kase, koka.statusAprovimi, koka.arsyeAnullimi, koka.idDokAnullimi, koka.nrLlogari, koka.printo, koka.idKrijuesi, koka.DtKrijimi);
            //id e kokes se dok te bankes qe u ruajt i vihet si vlere id-se lidhese te dokumentit lidhes qe eshte gjeneruar
            if (!mesazh.Status) return mesazh;
            koka.IdKoka = idk;
            foreach (var o in koka.oColTrupi)
            {
                o.IdKoka = koka.IdKoka;
                if (o.Lloji == " ") continue;
                mesazh = dbArkaBanka.ruajVeprimBankeTrupi(o.IdTrupi, o.IdKoka, o.Lloji, o.IdSubjekti, o.PershkrimiTrupi, o.DebiKredi, o.IdFatura, o.Zbritja, o.Kreditet, o.VleraPaguar, o.VleraPaguarMonedhaBaze, o.VleraPaArketueshme, o.KMK, o.IdNivel, o.IdOpsionePagese, o.NrTel, o.Muaji, o.Kodi, o.VleraFillestare, o.VleraMbetur, o.StatusFature, o.MeKursFature);
                if (!mesazh.Status)
                {
                    break;
                }

                if (!perBRM) continue;
                if (idStatusDokumenti != 1 || statusAprovimi != StatusAprovimi.Undefined) continue;
                if (!string.IsNullOrEmpty(o.Kodi) && o.Kodi.Contains("Permbledhese"))
                    o.Kodi = string.Empty;
                if (konf == null) konf = new clsKonfigurimAmbjenti(koka.idKonfigAmbjente);
                if (perdoruesi == null) perdoruesi = new clsPerdorues(koka.IdPerdoruesi);
                dbArkaBanka.ruajArketimPerTransferimNeBRM(konf.KodKonfigAmbjente, koka.CustomerNumber, koka.Shoqeria, koka.NrLlogari, koka.NrSerial, koka.DateDokumenti, perdoruesi.EmriPerdorues, koka.PershkrimiKoka, o.Muaji, o.Kodi, o.VleraPaguar, koka.Vlera, koka.IdKoka);
            }

            mesazh = ruajVeprimBankeNeHistorik(dbArkaBanka, koka.IdKoka, koka.IdStatusDokumenti, koka.IdPerdoruesi);

            foreach (var k in koka.oDokumentLidhes)
            {
                if (!k.OColTrupi.EshteVlefshemTrupiDokumentitLidhes(new clsDatabaseRegjistrim(dbArkaBanka)))
                    return new clsMesazh(false, MessagesResource.Messages["msgDokumentiKaNdryshuarHapeniPerseri"]);
                if (mesazh.Status && k.OColTrupi.Count > 1)
                {
                    k.NrLidhje = koka.nrDokumenti;

                    k.IdGjenerues = idk;
                    foreach (var t in k.OColTrupi.Where(t => t.IdDokumenti == -5))
                    {
                        t.IdDokumenti = koka.IdKoka; //si id e dokumentit kryesor vendoset Id e kokes se veprimit te bankes
                    }
                    mesazhRegjistrimi = k.ruaj(koka.idStatusDokumenti != 0, dbArkaBanka);
                }
                else
                {
                    mesazhRegjistrimi = new clsMesazh(true);
                }
            }
            if (mesazh.Status && mesazhRegjistrimi.Status)
            {
                if (koka.OKokaFleteKontabel.NrDukumentiKokaFleteKontabel != null)
                {
                    koka.OKokaFleteKontabel.NrDukumentiKokaFleteKontabel = koka.nrDokumenti;

                    koka.OKokaFleteKontabel.IdGjenerues = koka.IdKoka;
                    var dbKont = new clsDatabaseKontabilitet(dbArkaBanka);
                    kontMesazh = koka.OKokaFleteKontabel.Ruaj(dbKont);
                }
                if (kontMesazh.Status)
                {
                    foreach (var gj in koka.oGjendjeKF.Where(gj => gj != null))
                    {
                        gj.NrDok = koka.nrDokumenti;
                        gj.IdDok = koka.IdKoka;
                        mesazhRegjistrimi = gj.Ruaj(dbArkaBanka);
                        if (!mesazhRegjistrimi.Status)
                        {
                            return mesazh;
                        }
                    }
                    var dbr = new clsDatabaseRegjistrim(dbArkaBanka);
                    var etape = new clsEtapeAprovimi();
                    var dite = 0;
                    if (koka.idDokAnullimi > 0)
                    {
                        var anull = new clsVeprimBankaKoka(koka.idDokAnullimi, dbArkaBanka);
                        dite = (koka.dateDokumenti - anull.dateDokumenti).Days;
                        if (koka.statusAprovimi == StatusAprovimi.Undefined && statusAprovimi == StatusAprovimi.Undefined && !eshteModifikim && koka.idStatusDokumenti == 1) // pa skeme fare, direkt ruajtje
                            dbArkaBanka.ShtoRreshtTeRiPerAnullim(koka.idDokAnullimi, true);
                    }
                    if (!(statusAprovimi == StatusAprovimi.Undefined && this.StatusAprovimi == StatusAprovimi.Aprovuar))
                    {
                        mesazh = etape.ruaj(koka.IdPerdoruesi, serverUrl, idskema, dbr, koka.IdDokNga, koka.IdPerdoruesi, statusAprovimi, idetapa, 3, koka.IdStatusDokumenti, koka.idNdermarje, koka.statusAprovimi, 0, koka.IdKoka, koka.IdPerdoruesi, koka.vlera, DateTime.Now, koka.IdKonfigAmbjente, koka.nrDokumenti, koka.dateDokumenti, dite);
                        if (!mesazh)
                            return mesazh;
                    }
                    if (eshteModifikim && koka.IdStatusDokumenti == 1) //vetem nqs aprovohet
                    {


                        mesazh = dbr.modifikoEtapeIdkoka(koka.IdDokNga, koka.IdKoka); //modifikojme id e kokes se shitjes tek etapat pasi aprovohet dhe ruhet
                        if (!mesazh)
                        {
                            return mesazh;
                        }
                        if (koka.statusAprovimi == StatusAprovimi.Aprovuar)
                            dbArkaBanka.ShtoRreshtTeRiPerAnullim(koka.idDokAnullimi, true);
                    }
                    if (mesazhRegjistrimi.Status)
                    {
                        if (vjenNgaImportSQL && idDokImport != string.Empty)
                        {
                            var dbRegj = new clsDatabaseRegjistrim(dbArkaBanka);
                            var statusi = 1;
                            mesazh = dbRegj.updateDokTabeleTemportal(idDokImport, idNdermarje, statusi, emerTabKoka, primaryKeyEmerFushe, emerFusheNdermarrje);
                            if (!mesazh.Status)   return mesazh;

                        }
                    }

                    if (!mesazhRegjistrimi.Status) return mesazh;
                    mesazh = new clsMesazh(true, MessagesResource.Messages["labelRaportMesazhRuajtjaPerfundoiSukses"]);
                    return mesazh;
                }
                mesazh.Status = false;
                mesazh.PershkrimMesazhi = kontMesazh.PershkrimMesazhi;
                return mesazh;
            }
            mesazh.Status = false;
            mesazh.PershkrimMesazhi = mesazhRegjistrimi.PershkrimMesazhi;
            return mesazh;
        }
        public clsMesazh modifikoVeprimBanke(clsVeprimBankaKoka koka, clsDatabaseArkaBanka dbArkaBanka, ResourceManager rm, CultureInfo ci, int idskema, StatusAprovimi statusAprovimi, int idetapa, string serverUrl)
        {
            clsMesazh mesazh;

            try
            {
                var kokaEkzistuese = new clsVeprimBankaKoka(koka.IdKoka, dbArkaBanka);
                if (string.IsNullOrEmpty(kokaEkzistuese.NrDokumenti) || kokaEkzistuese.IdStatusDokumenti == 2)
                    return new clsMesazh(false, rm.GetString("msgDokumentiKaNdryshuarHapeniPerseri", ci));
                kokaEkzistuese.OKokaFleteKontabel = new clsKokaFleteKontabel();
                mesazh = dbArkaBanka.fshiVeprimBankeKoka(kokaEkzistuese.IdKoka);
                if (!mesazh.Status)
                    return mesazh;
                koka.IdDokNga = kokaEkzistuese.IdKoka;
                koka.DtKrijimi = kokaEkzistuese.DtKrijimi;
                this.idKrijuesi = kokaEkzistuese.idKrijuesi;

                var dbregj = new clsDatabaseRegjistrim(dbArkaBanka);
                kokaEkzistuese.OGjendjeKF = new colGjendjeKlientFurnitor(kokaEkzistuese.IdKoka, kokaEkzistuese.IdNivel, dbregj);
                var dbRegjistrim = new clsDatabaseRegjistrim(dbArkaBanka);
                var cls = new colDokumentLidhesKoka(kokaEkzistuese.IdKoka, 4, dbRegjistrim);
                if (cls.Count == 0)
                    cls = new colDokumentLidhesKoka(kokaEkzistuese.IdKoka, 3, dbRegjistrim);
                if (cls.Count != 0)
                {
                    foreach (var k in koka.ODokumentLidhes)
                        k.IdDokNga = cls[0].IdKoka;
                }
                foreach (var k in cls)
                {
                    mesazh = k.fshiDokumentDheKontabilitet(dbRegjistrim);
                    //Tani per tani kur modifikohet nje veprim banke fshihet lidhja e meparsheme e dok dhe ruhet lidhja e re. Kjo do ndryshohet me vone dhe lidhjes se vjeter do i vihet nje status dallues.
                    if (!mesazh.Status)
                        return mesazh;
                }
                foreach (var gj in kokaEkzistuese.OGjendjeKF.Where(gj => gj.IdGjendjeKf != 0))
                {
                    gj.IdStatusGjendjeKf = 2; //rasti kur ndrysheohet statusi per treguar qe dokumenti eshte i modifikuar dhe nuk duhet marre parasysh
                    mesazh = gj.Modifiko(dbArkaBanka);
                    if (!mesazh.Status)
                        return mesazh;
                }
                var dbkontab = new clsDatabaseKontabilitet(dbArkaBanka);
                var fk = new clsKokaFleteKontabel(kokaEkzistuese.IdKoka, 3, dbkontab);
                if (fk.NrDukumentiKokaFleteKontabel == null)
                    fk = new clsKokaFleteKontabel(kokaEkzistuese.IdKoka, 4, dbkontab);
                if (fk.NrDukumentiKokaFleteKontabel != null)
                    kokaEkzistuese.OKokaFleteKontabel = fk;
                var kokaqendra = new clsKokaQendraKosto();
                var dbqendra = new clsDatabaseQendraKosto(dbkontab);
                kokaqendra.KtheKokaQKSipasIDGjeneruesDheKonfig(fk.IdKokaFleteKontabel, fk.IdKonfigAmbjente, dbqendra);
                if (kokaqendra.IdKoka != 0 && kokaqendra.IdKoka != -1)
                {
                    kokaEkzistuese.OKokaFleteKontabel.KokaQendraKosto = kokaqendra;
                }
                else kokaEkzistuese.OKokaFleteKontabel.KokaQendraKosto = new clsKokaQendraKosto();
                koka.OKokaFleteKontabel.IdDokNga = kokaEkzistuese.OKokaFleteKontabel.IdKokaFleteKontabel;
                koka.OKokaFleteKontabel.KokaQendraKosto.IdDokNga = kokaEkzistuese.OKokaFleteKontabel.KokaQendraKosto.IdKoka;
                mesazh = ruajVeprimBanke(koka, true, dbArkaBanka, false, "", "", "", "", idskema, statusAprovimi, idetapa, serverUrl, false);
                if (!mesazh.Status)
                    return mesazh;
                if (kokaEkzistuese.OKokaFleteKontabel.IdKokaFleteKontabel == 0) return new clsMesazh(true, rm.GetString("msgModifikimiMeSukses", ci));
                mesazh = kokaEkzistuese.OKokaFleteKontabel.ModifikoFleteKontabel(true, dbkontab);
                return !mesazh.Status ? mesazh : new clsMesazh(true, rm.GetString("msgModifikimiMeSukses", ci));
            }
            catch (Exception ce)
            {
                return new clsMesazh(false, ce.Message);
            }
        }

        /// <summary>
        /// Ekzekuton nje transaksion per te fshire nje objekt clsVeprimBankaKoka dhe trupin e tij ne DB.
        ///<param name="koka">Objekt i tipit clsVeprimBankaKoka qe do te fshihet</param>
        ///Therret funksionin <see cref="clsKokaFleteKontabel.StornimFleteKontabel"/> nese eshte ruajtur kontabilitet per kete veprim banke
        ///Therret funksionin <see cref="DbCore.DbArkaBanka.clsDatabaseArkaBanka.fshiVeprimBankeTrupi"/>
        ///Therret funksionin <see cref="DbCore.DbArkaBanka.clsDatabaseArkaBanka.fshiVeprimBankeKoka"/>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te transaksionit (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        public clsMesazh fshiVeprimBankeKokaDheTrupi(clsVeprimBankaKoka koka, clsDatabaseArkaBanka dbArkaBanka)
        {
            using (var scope=new MyTransactionScope(dbArkaBanka))
            {
                try
                {
                    //dbArkaBanka.beginTransaksion();
                    clsMesazh mesazh;
                    var dbkontab = new clsDatabaseKontabilitet(dbArkaBanka);
                    var dbregj = new clsDatabaseRegjistrim(dbArkaBanka);
                    var kokaEkzistuese = new clsVeprimBankaKoka(koka.IdKoka, dbArkaBanka)
                    {
                        OKokaFleteKontabel = new clsKokaFleteKontabel()

                    };
                    //clsVeprimBankaKoka kokaEkzistuese = this.ktheVeprimBanke(koka.IdKoka)[0];
                    kokaEkzistuese.OGjendjeKF = new colGjendjeKlientFurnitor(kokaEkzistuese.IdKoka, kokaEkzistuese.IdNivel, dbregj);
                    //DbCore.DbRegjistrim.clsNivelRegjistrimi nivel = new clsNivelRegjistrimi();
                    //nivel.mbushNivelRegjistrimiSipasID(kokaEkzistuese.IdNivel);
                    var idKategori = clsNivelRegjistrimi.ktheIdKategoriaNivelRegjistrimi(kokaEkzistuese.IdNivel);
                    //koka.oArkiva = new DbShare.colArkiva(koka.idKoka, idKategori);
                    var dbRegjistrim = new clsDatabaseRegjistrim(dbArkaBanka);
                    //dbRegjistrim.vendosManager(dbArkaBanka );
                    var cls = new colDokumentLidhesKoka(kokaEkzistuese.IdKoka, 4, dbRegjistrim);
                    if (cls.Count == 0)
                    {
                        cls = new colDokumentLidhesKoka(kokaEkzistuese.IdKoka, 3, dbRegjistrim);

                    }
                    foreach (var k in cls)
                    {
                        if (k.IdKoka != 0)
                        {
                            var lidhja = k;
                            //DbCore.DbRegjistrim.clsDokumentLidhesKoka lidhja = dbRegjistrim.ktheLidhjenDokSipasIdLidheseDheLlojit(kokaEkzistuese.IdKoka, 4)[0];//merr lidhjen ekzistuese (qe ishte bere para se te modifikohej vep i bankes)
                            // koka.ODokumentLidhes.IdDokNga = lidhja.IdKoka;
                            mesazh = lidhja.fshiDokumentDheKontabilitet(dbRegjistrim);//Tani per tani kur modifikohet nje veprim banke fshihet lidhja e meparsheme e dok dhe ruhet lidhja e re. Kjo do ndryshohet me vone dhe lidhjes se vjeter do i vihet nje status dallues.
                            if (!mesazh.Status)
                            {
                                return mesazh;
                            }
                        }
                    }
                    foreach (var gj in kokaEkzistuese.OGjendjeKF)
                    {
                        if (gj.IdGjendjeKf != 0)
                        {
                            gj.IdStatusGjendjeKf = 2; //rasti kur ndrysheohet statusi per treguar qe dokumenti eshte i modifikuar dhe nuk duhet marre parasysh
                            mesazh = gj.Modifiko(dbArkaBanka);
                            if (!mesazh.Status)
                            {
                                //      dbArkaBanka.rollbackTransaksion();
                                return mesazh;
                            }
                        }
                    }
                    //fshirja e Arkives

                    mesazh = colArkiva.UpdateStatusDokFshi(koka.IdKoka, idKategori, idPerdoruesi);
                    if (!mesazh.Status)
                    {
                        //  dbArkaBanka.rollbackTransaksion();
                        return mesazh;
                    }
                    var newclsKokaFleteKontabel = new clsKokaFleteKontabel(kokaEkzistuese.IdKoka, 3, dbkontab);
                    if (newclsKokaFleteKontabel.NrDukumentiKokaFleteKontabel != null)
                        kokaEkzistuese.OKokaFleteKontabel = newclsKokaFleteKontabel;
                    else
                    {
                        newclsKokaFleteKontabel = new clsKokaFleteKontabel(kokaEkzistuese.IdKoka, 4, dbkontab);
                        if (newclsKokaFleteKontabel.NrDukumentiKokaFleteKontabel != null)
                            kokaEkzistuese.OKokaFleteKontabel = newclsKokaFleteKontabel;
                    }
                    kokaEkzistuese.OKokaFleteKontabel.fshiupd(dbkontab);

                    kokaEkzistuese.IdStatusDokumenti = 2;  //duhet vendosur nje status i pershtatshem per kete modifikim
                    clsMesazh msg = ruajVeprimBankeNeHistorik(dbArkaBanka, IdKoka, 2, idPerdoruesi);
                    mesazh = dbArkaBanka.fshiVeprimBankeKoka(kokaEkzistuese.IdKoka);
                    if (!mesazh.Status)
                    {
                        return mesazh;
                    }
                    scope.Complete();
                    return new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
                }
                catch (Exception ce)
                {
                    return new clsMesazh(false, ce.Message);
                }
            }
        }

        /// <summary>
        /// Ruan objektin e veprimit te bankes ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbArkaBanka.clsVeprimBankaKoka.ruajVeprimBanke"/>
        /// </summary>
        public clsMesazh ruaj(IDictionary<string, object> hfregjistrime, bool vjenNgaImportSQL, string idDokImporti, string emerTabKoka, string primaryKeyEmerFushe, string ndermarrjeKey, int idskema, StatusAprovimi statusAprovimi, int idetapa, string serverUrl, bool perBRM)
        {
            using (var scope=new MyTransactionScope())
            {
                var db = new clsDatabaseArkaBanka();
                bool kaNdryshimNumri;
                var mesazhkontrolli = kontrolloBanka(out kaNdryshimNumri, db, hfregjistrime);
                if (!mesazhkontrolli.Status)
                    return mesazhkontrolli;
                var uRuajt = ruajVeprimBanke(this, false, db, vjenNgaImportSQL, idDokImporti, emerTabKoka, primaryKeyEmerFushe, ndermarrjeKey, idskema, statusAprovimi, idetapa, serverUrl, perBRM);
                if (!uRuajt.Status) return uRuajt;
                uRuajt = colArkiva.RuajArkiven(IdKoka, ktheIdkatDokArkiva(this.llojiVeprimit), idPerdoruesi, idNdermarje, HfArkiva);
                if (!uRuajt.Status) return uRuajt;
                scope.Complete();
                if (kaNdryshimNumri) return mesazhkontrolli;
                return uRuajt;
            }
        }

        public clsMesazh ruaj(IDictionary<string, object> hfregjistrime, clsDatabaseArkaBanka db, int idskema, StatusAprovimi statusAprovimi, int idetapa, string serverUrl, bool perBRM)
        {
            bool kaNdryshimNumri;
            var mesazhkontrolli = kontrolloBanka(out kaNdryshimNumri, db, hfregjistrime);
            if (!mesazhkontrolli.Status)
            {
                return mesazhkontrolli;
            }

            var u_ruajt = ruajVeprimBanke(this, false, db, false, "", "", "", "", idskema, statusAprovimi, idetapa, serverUrl, perBRM);
            if (!u_ruajt.Status)
            {
                return u_ruajt;
            }

            if (kaNdryshimNumri)
                return mesazhkontrolli;
            return u_ruajt;
        }

        private clsMesazh kontrolloBanka(out bool kaNdryshimNumri, clsDatabaseArkaBanka db, IDictionary<string, object> hfregjistrime)
        {
            kaNdryshimNumri = false;
            if (nrDokumenti == "")
                return new clsMesazh(false, "Numri i dokumentit nuk mund te jete bosh");
            var mes = new clsMesazh();
            if (hfregjistrime != null)
            {
                mes = kontrolloNrAutoBanka(out kaNdryshimNumri, db, hfregjistrime);
                if (!mes.Status)
                    return mes;
            }
            if (db.ekzistonVeprimBanke(idBanka, dateDokumenti, nrDokumenti, idKonfigAmbjente))
                return new clsMesazh(false, "Ekziston nje regjistrim me te njejtin numer dokumenti!");
            if (kaNdryshimNumri)
                return mes;
            return new clsMesazh(true, "Kontrolli i Bankes u krye me sukses!");
        }

        private clsMesazh kontrolloNrAutoBanka(out bool kaNdryshimNumri, clsDatabaseArkaBanka db, IDictionary<string, object> hfregjistrime)
        {
            var dbadm = new clsDatabaseAdmin(db);
            var list = clsNrAutom.kontrollogjithenumrat(dbadm, hfregjistrime, dateDokumenti);
            if (NrAuto.ktheVlerenEre(list, "NrDokumenti") != "")
                nrDokumenti = NrAuto.ktheVlerenEre(list, "NrDokumenti");
            if (NrAuto.ktheVlerenEre(list, "NrSerial") != "")
                nrSerial = NrAuto.ktheVlerenEre(list, "NrSerial");
            var ndervit = new clsNdermarrjeViti(idNderViti, dbadm);

            var mes = NrAuto.ruajvlera(out kaNdryshimNumri, list, dateDokumenti, idPerdoruesi, ndervit.IdNdermarrje, dbadm);
            return new clsMesazh(mes.Status, mes.PershkrimMesazhi);
        }

        private clsMesazh ruajVeprimBankeNeHistorik(clsDatabaseArkaBanka db, int idKoka, int idStatusDokumenti, int idPerdoruesi)
        {
            return db.ruajVeprimBankeNeHistorik(idKoka, idStatusDokumenti, idPerdoruesi);
        }

        /// <summary>
        /// Modifikon objektin e veprimit te bankes ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbArkaBanka.clsVeprimBankaKoka.modifikoVeprimBanke"/>
        /// </summary>
        public clsMesazh modifiko(bool lidhur, ResourceManager rm, CultureInfo ci, int idskema, StatusAprovimi statusAprovimi, int idetapa, string serverUrl)
        {
            using (var scope=new MyTransactionScope())
            {
                var data = new clsDatabaseArkaBanka();
                var kokaEkzistuese = new clsVeprimBankaKoka(idKoka, data);
                this.idKrijuesi = kokaEkzistuese.idKrijuesi;

                var dite = 0;
                if (kokaEkzistuese.idDokAnullimi > 0)
                {
                    var anull = new clsVeprimBankaKoka(kokaEkzistuese.idDokAnullimi, data);
                    dite = (kokaEkzistuese.dateDokumenti - anull.dateDokumenti).Days;
                }
                clsMesazh uModifikua = new clsMesazh(true, "Dokumenti u modifikua me sukses!");

                if (!lidhur)
                {
                    var idVjeter = idKoka;
                    uModifikua = modifikoVeprimBanke(this, data, rm, ci,idskema,statusAprovimi,idetapa,serverUrl);
                    if (!uModifikua.Status)
                        return uModifikua;
                    uModifikua = colArkiva.ModifikoArkiven(idVjeter, idKoka, ktheIdkatDokArkiva(this.LlojiVeprimit), IdNdermarje, idPerdoruesi);
                    if (!uModifikua.Status)
                        return uModifikua;
                }
                else 
                {
                    if (statusAprovimi != StatusAprovimi)
                    {
                        uModifikua = data.modifikoVeprimBankeKokaLidh(IdKoka, IdBanka, Kursi,
                        DateDokumenti, DateRegjistrimi, NrDokumenti, NrReference, NrSerial,
                        PershkrimiKoka, IdMenyrePagese, Vlera, VleraMonedhaBaze,
                        KomisioniBankar, KomisioniMonedhaBaze, LlojiVeprimit, IdPerdoruesi,
                        IdLlojDokumenti, IdStatusDokumenti, IdDegeAdministrative, IdGrup1,
                        IdGrup2, IdGrup3, idLlogKredite, shoqeria, customerNumber,
                        idAutomjet, idRaportDesing, Financieri, DhenesiMarresi, Arketari, kase, statusAprovimi, arsyeAnullimi, IdDokAnullimi, nrLlogari,printo, IdKrijuesi);

                       if (!uModifikua)
                        return uModifikua;

                       clsMesazh msg = ruajVeprimBankeNeHistorik(data, IdKoka, IdStatusDokumenti, IdPerdoruesi);

                    }
                 

                    if (statusAprovimi != StatusAprovimi.Undefined)
                    {
                        uModifikua = new clsEtapeAprovimi().ruaj(idPerdoruesi, serverUrl, idskema, new clsDatabaseRegjistrim(data), idKoka, IdPerdoruesi, statusAprovimi, idetapa, 3, kokaEkzistuese.IdStatusDokumenti, kokaEkzistuese.idNdermarje, kokaEkzistuese.statusAprovimi, 0, kokaEkzistuese.idKoka, kokaEkzistuese.idPerdoruesi, Double.Parse(kokaEkzistuese.vlera.ToString()), kokaEkzistuese.dtKrijimi, kokaEkzistuese.idKonfigAmbjente, kokaEkzistuese.nrDokumenti, kokaEkzistuese.dateDokumenti, dite);
                        if (!uModifikua)
                            return uModifikua;
                    }
                }
                scope.Complete();
                return uModifikua;
            }
        }

        /// <summary>
        /// Fshin objektin e veprimit te bankes (koken e dokumentit dhe trupin e tij) ne tabelat perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbArkaBanka.clsVeprimBankaKoka.fshiVeprimBankeKokaDheTrupi"/>
        /// </summary>
        public clsMesazh fshi(ResourceManager rm, CultureInfo ci)
        {
            clsDatabaseArkaBanka data = new clsDatabaseArkaBanka();
            var u_fshi = fshiVeprimBankeKokaDheTrupi(this, data);
            return u_fshi;
        }
        public clsMesazh fshi(clsDatabaseArkaBanka db)
        {
            var u_fshi = fshiVeprimBankeKokaDheTrupi(this, db);
            return u_fshi;
        }

        //funksion qe duhet per te kontrolluar nese eshte zgjedhur klient\furnitor apo jo, ne menyre qe te percaktohet
        //nese duhet gjeneruar gjendja e klient furnistori apo jo
        public bool kaKlientVeprimi()
        {
            var i = 0;
            for (i = 0; i < oColTrupi.Count; i++)
            {
                if (oColTrupi[i].Lloji == "Klient" || oColTrupi[i].Lloji == "Furnitor")
                {
                    return true;
                }
            }
            return false;
        }

        //duke qene se klienti nuk caktohet tek koka e dokumentit te bankes, duhet gjetur nga trupi i veprimit te bankes
        public int ktheIdKlienti()
        {
            var i = 0;
            for (i = 0; i < oColTrupi.Count; i++)
            {
                if (oColTrupi[i].Lloji == "Klient" || oColTrupi[i].Lloji == "Furnitor")
                {
                    return oColTrupi[i].IdSubjekti;
                }
            }
            return 0;
        }

        public clsBanka ktheObjektBanke()
        {
            var banka = new clsBanka();
            banka.IdBanka = IdBanka;
            return banka.merr();
        }

        public double gjejVlerePerGjendjeKF()
        {
            var i = 0;
            double vlera = 0;
            for (i = 0; i < oColTrupi.Count; i++)
            {
                if (oColTrupi[i].Lloji == "Klient" || oColTrupi[i].Lloji == "Furnitor")
                {
                    vlera = vlera + oColTrupi[i].Zbritja + oColTrupi[i].VleraPaguar + oColTrupi[i].Kreditet;
                }
            }
            return vlera;
        }

        public clsGjendjeKlientFurnitor krijoObjektGjendjeKF()
        {
            if (kaKlientVeprimi())
            {
                var gjendje = new clsGjendjeKlientFurnitor();
                gjendje.DateDok = DateDokumenti;
                gjendje.DateRegj = DateRegjistrimi;
                gjendje.IdDok = 0;
                gjendje.IdGjendjeKf = 0;
                gjendje.IdKlientGjendjeKf = ktheIdKlienti();
                gjendje.IdMonedhaDok = ktheObjektBanke().IdMonedhaBanka;
                gjendje.KursiDok = Kursi;
                gjendje.NivelDok = -1;
                gjendje.NrDok = NrDokumenti;
                gjendje.VlMinus = gjejVlerePerGjendjeKF();
                gjendje.VlMinusMonedheBaze = gjendje.VlMinus * gjendje.KursiDok;
                return gjendje;
            }
            return new clsGjendjeKlientFurnitor();
        }

        public static bool kaAutorizime(int idkoka, int idperdoruesi)
        {
            var dbKokaMagazina = new clsDatabaseArkaBanka();
            var sukses = dbKokaMagazina.kaAutorizimKokaBanka(idkoka, idperdoruesi);
            dbKokaMagazina.Dispose();
            return sukses;
        }

        public static int ktheIdRaportDesign(int idkoka)
        {
            using (var data = new clsDatabaseArkaBanka())
            {
                return data.ktheIdRaportDesignSipasIdkokes(idkoka);
            }
        }

        public static int ktheIdStatusDokumenti(int idkoka)
        {
            using (var data = new clsDatabaseArkaBanka())
            {
                return data.ktheIdStatusDokumentiSipasIdkokes(idkoka);
            }
        }

        public clsMesazh PrintoArketimeNeKase(clsPerdorues perdoruesi, int idNdermarje, string merrIpKasaNgaWebServisi)
        {
            var idkonfigurimKase = perdoruesi.IdKonfigKasa != 0 ? perdoruesi.IdKonfigKasa.ToString() : clsAtributeTrupi.merrVleredefaultSipasKontrollitDheKonfigurimit(IdKonfigAmbjente, "cmbKonfigurimKase", 301);
            if (idkonfigurimKase == "")
                return new MesazhGabimi("Dokumenti nuk u printua pasi nuk keni konfiguruar kase per kete lloj dokumenti.");

            return PrintoArketimeNeKase(perdoruesi.IdPerdoruesi, idNdermarje, merrIpKasaNgaWebServisi, Convert.ToInt32(idkonfigurimKase));
        }

        public clsMesazh PrintoArketimeNeKase(int idPerdoruesi, int idNdermarje, string merrIpKasaNgaWebServisi, int idKonfigurimKase)
        {
            System.Diagnostics.Stopwatch myWatch = System.Diagnostics.Stopwatch.StartNew();
            ImbLogger.Info($"PrintoArketimNeKase: IdKoka: ${IdKoka} Numer Dokumenti: {NrDokumenti}, Date dokumenti: {DateDokumenti}, Lloj Dokumenti: {new clsKonfigurimAmbjenti(IdKonfigAmbjente).KodKonfigAmbjente}");
            clsMesazh mesazh = new clsMesazh();
            if (idKonfigurimKase == 0)
                return new MesazhGabimi("Dokumenti nuk u printua pasi nuk keni konfiguruar kase per kete lloj dokumenti.");

            clsKonfigurimKase _kasa = new clsKonfigurimKase(idKonfigurimKase);
            string IP;
            if (merrIpKasaNgaWebServisi == "")
            {
                IP = _kasa.OColVlerat.ktheVlereOpsioni("IPKASE");
                ImbLogger.Info($"ruajRegjistrim: merrIpKasaNgaWebServisi returned empty string! U vendos Ip e konfiguruar te kasa {IP}");
            }
            else
            {
                IP = merrIpKasaNgaWebServisi;
                ImbLogger.Info($"ruajRegjistrim: merrIpKasaNgaWebServisi ktheu {IP}");
            }

            mesazh = KrijuesKasash.printoNeKase(_kasa, "0", this, idPerdoruesi, idNdermarje, IP);

            myWatch.Stop();
            ImbLogger.Info($"Mbaroi Procesi i printimit ne kase per arketimin me id {IdKoka}. {mesazh.PershkrimMesazhi} Kohezgjatja: {myWatch.ElapsedMilliseconds / 1000.0} sekonda.");
            return mesazh;
        }
        public int ktheIdkatDokArkiva(string llojVeprimi) {
            int idKategoria;
            switch (llojVeprimi.ToLower()) {
                case "arketim":
                case "pagese":
                    idKategoria = 3;
                    break;
                case "terheqje":
                case "derdhje":
                    idKategoria = 4;
                    break;
                default:
                    idKategoria = 0;
                    break;
            }
            return idKategoria;
        }
        public bool eshteDokumentiILidhur()
        {
            using (var dbAdmin = new clsDatabaseAdmin())
                return dbAdmin.eshteDokumentiILidhur(IdKoka, IdNivel, "T_VEPRIMBANKAKOKA", "IDKOKA");
        }

        public static string ktheKonfigurimDok(int idKoka, string lloji)
        {
            using (var dbAdmin = new clsDatabaseAdmin())
                return dbAdmin.ktheKonfigurimDok(idKoka, lloji);
        }
        public object[] krijoObjektPerWebhook(clsVeprimBankaKoka koka, string eventi, string objekti)
        {
            clsPerdorues perdorues = new clsPerdorues(koka.IdPerdoruesi);
            clsNdermarrje ndermarrje = new clsNdermarrje(koka.idNdermarje);
            object[] kokaWebhook = new object[2];
            kokaWebhook[0] = new
            {
                eventi = eventi,
                objekti = objekti,
                idKoka = koka.IdKoka,
                perdoruesi = perdorues.PerdoruesUsername,
                idPerdoruesi = perdorues.IdPerdorues
            };
            kokaWebhook[1] = new
            {
                nrDok = koka.nrDokumenti,
                kodNdermarje = ndermarrje.NdermarrjeKodi,
                kursi = koka.kursi,
                kase = koka.kase,
                pershkrimi = koka.pershkrimiKoka,
                dtKrijimi = koka.DtKrijimi.ToString(),
                idNdermarrje = koka.idNdermarje,
                dtRegjistrimi = koka.dateRegjistrimi.ToString(),
                idStatusDok = koka.idStatusDokumenti,
                idDegeAdministrative = koka.idDegeAdministrative,
                idPerdoruesi = koka.IdPerdoruesi,
                idKrijuesi = koka.idKrijuesi,
                idRaportDesing = koka.IdRaportDesing,
            };
            return kokaWebhook;
        }
        #endregion Metoda Publike

        #region Metoda Internal

        internal bool mbushVeprimBankaKoka(DataRow dbDataRowVeprimBankaKoka)
        {
            if (dbDataRowVeprimBankaKoka != null)
            {
                try
                {
                    int.TryParse(dbDataRowVeprimBankaKoka["IDKOKA"].ToString(), out idKoka);
                    int.TryParse(dbDataRowVeprimBankaKoka["IDBANKA"].ToString(), out idBanka);
                    double.TryParse(dbDataRowVeprimBankaKoka["IDKURSI"].ToString(), out kursi);
                    DateTime.TryParse(dbDataRowVeprimBankaKoka["DATEDOKUMENTI"].ToString(), out dateDokumenti);
                    DateTime.TryParse(dbDataRowVeprimBankaKoka["DATEREGJISTRIMI"].ToString(), out dateRegjistrimi);
                    nrDokumenti = dbDataRowVeprimBankaKoka["NRDOKUMENTI"].ToString();
                    int.TryParse(dbDataRowVeprimBankaKoka["NRREFERENCE"].ToString(), out nrReference);
                    nrSerial = dbDataRowVeprimBankaKoka["NRSERIAL"].ToString();
                    pershkrimiKoka = dbDataRowVeprimBankaKoka["PERSHKRIMIKOKA"].ToString();
                    int.TryParse(dbDataRowVeprimBankaKoka["IDMENYREPAGESE"].ToString(), out idMenyrePagese);
                    double.TryParse(dbDataRowVeprimBankaKoka["VLERA"].ToString(), out vlera);
                    double.TryParse(dbDataRowVeprimBankaKoka["VLERAMONADHABAZE"].ToString(), out vleraMonedhaBaze);
                    double.TryParse(dbDataRowVeprimBankaKoka["KOMISIONI"].ToString(), out komisioniBankar);
                    double.TryParse(dbDataRowVeprimBankaKoka["KOMISIONIMONADHABAZE"].ToString(), out komisioniMonedhaBaze);
                    llojiVeprimit = dbDataRowVeprimBankaKoka["LLOJIVEPRIMIT"].ToString();
                    int.TryParse(dbDataRowVeprimBankaKoka["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    int.TryParse(dbDataRowVeprimBankaKoka["IDLLOJDOK"].ToString(), out idLlojDokumenti);
                    int.TryParse(dbDataRowVeprimBankaKoka["IDSTATUSDOK"].ToString(), out idStatusDokumenti);
                    int.TryParse(dbDataRowVeprimBankaKoka["IDNDERVITI"].ToString(), out idNderViti);
                    int.TryParse(dbDataRowVeprimBankaKoka["IDNDERMARJE"].ToString(), out idNdermarje);
                    int.TryParse(dbDataRowVeprimBankaKoka["IDKONFIGAMBJENTE"].ToString(), out idKonfigAmbjente);
                    int.TryParse(dbDataRowVeprimBankaKoka["IDNIVELGJENERUES"].ToString(), out idNivelGjenerues);
                    int.TryParse(dbDataRowVeprimBankaKoka["IDKONFIGGJENERUES"].ToString(), out idKonfigGjenerues);
                    int.TryParse(dbDataRowVeprimBankaKoka["IDGJENERUES"].ToString(), out idGjenerues);
                    int.TryParse(dbDataRowVeprimBankaKoka["IDNIVEL"].ToString(), out idNivel);
                    int.TryParse(dbDataRowVeprimBankaKoka["IDDOKNGA"].ToString(), out idDokNga);
                    int.TryParse(dbDataRowVeprimBankaKoka["IDDEGEADMINISTRATIVE"].ToString(), out idDegeAdministrative);
                    DateTime.TryParse(dbDataRowVeprimBankaKoka["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRowVeprimBankaKoka["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    int.TryParse(dbDataRowVeprimBankaKoka["IDGRUP1"].ToString(), out idGrup1);
                    int.TryParse(dbDataRowVeprimBankaKoka["IDGRUP2"].ToString(), out idGrup2);
                    int.TryParse(dbDataRowVeprimBankaKoka["IDGRUP3"].ToString(), out idGrup3);
                    int.TryParse(dbDataRowVeprimBankaKoka["IDLLOGKREDITI"].ToString(), out idLlogKredite);
                    bool.TryParse(dbDataRowVeprimBankaKoka["KASE"].ToString(), out kase);
                    shoqeria = dbDataRowVeprimBankaKoka["SHOQERIA"].ToString();
                    customerNumber = dbDataRowVeprimBankaKoka["CUSTOMERNUMBER"].ToString();
                    int.TryParse(dbDataRowVeprimBankaKoka["IDAUTOMJET"].ToString(), out idAutomjet);
                    int.TryParse(dbDataRowVeprimBankaKoka["IDRAPORTDESING"].ToString(), out idRaportDesing);
                    financieri = Convert.ToString(dbDataRowVeprimBankaKoka["FINANCIERI"]);
                    dhenesiMarresi = Convert.ToString(dbDataRowVeprimBankaKoka["DHENESI_MARRESI"]);
                    arketari = Convert.ToString(dbDataRowVeprimBankaKoka["ARKETARI"]);
                    var stap = 0;
                    int.TryParse(dbDataRowVeprimBankaKoka["STATUSAPROVIMI"].ToString(), out stap);
                    statusAprovimi = (StatusAprovimi)stap;
                    arsyeAnullimi = dbDataRowVeprimBankaKoka["ARSYEANULLIMI"].ToString();
                    int.TryParse(dbDataRowVeprimBankaKoka["IDDOKANULLIMI"].ToString(), out idDokAnullimi);
                    nrLlogari = dbDataRowVeprimBankaKoka["NRLLOGARI"].ToString();
                    bool.TryParse(dbDataRowVeprimBankaKoka["PRINTO"].ToString(), out printo);
                    int.TryParse(dbDataRowVeprimBankaKoka["IDKRIJUESI"].ToString(), out idKrijuesi);

                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se kokes se veprimit banka nga db-ja");
                }
            }
            return false;
        }

        #endregion Metoda Internal
    }
}