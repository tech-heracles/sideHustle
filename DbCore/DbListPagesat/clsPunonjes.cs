using System;
using System.Data;
using System.Globalization;
using System.Resources;
using DbCore.DbAdmin;
using DbCore.DbQendraKosto;
using DbCore.DbShare;
using NLog;
using DbCore.IMBUtils;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.Types;
using DbCore.DbKontabiliteti;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbListPagesat
{
    /// <summary>
    ///     Kjo eshte klasa qe sherben per objektet qe perfaqsojne nje punonjes
    ///     (Te dhenat  merren nga tabela : T_PUNONJES)
    /// </summary>
    public class clsPunonjes
    {
      

        public static string mbushjeSukses = "Punonjesi u mbush me sukses";
        public static string gabimNeTeDhena = "ERROR: Gabim gjate marrjes se punonjesit nga db-ja";
        public static string drbosh = "Mbushja nuk u krye sepse nuk u morr asgje nga db-ja";



        #region Atribute

        private int idPunonjes;
        private DateTime datelindja;
        private int idQyteti;
        private bool aktiv;
        private int idGrupPunonjesish;
        private int llojPagese;
        private int idMonedha;
        private int idDepartament;
        private int idNenDepartament;
        private int idPerdoruesi;
        private int idNdermarje;
        private int idKonfig;
        private int idStatusDok;
        private int idObjektivaKosto;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private bool llogaritNgaListorare;
        private int idKrijuesi;
        private bool gjinia;
        private int kombesia;
        private bool kryefamiliar;
        private int edukimi;
        private int punaMeparshme;
        private int vendndodhja;
        private int nrRendor;
        private DataRow rreshti;
        private colArkiva oArkiva;
        private int idLlogari;
        #endregion

        #region Konstruktoret

        /// <summary>
        ///     konstruktori pa parametra
        /// </summary>
        public clsPunonjes()
        {
        }

        /// <summary>
        ///     kontruktori me parametra
        /// </summary>
        /// <param name="idPunonjes"> id e punonjesit</param>
        /// <param name="nrPersonal">nr personal</param>
        /// <param name="emer">emer</param>
        /// <param name="mbiemer">mbiemer</param>
        /// <param name="atesia">atesia</param>
        /// <param name="datelindja">datelindja</param>
        /// <param name="nrSig">nr sigurimesh</param>
        /// <param name="idQyteti">id qyteti</param>
        /// <param name="adresa">adresa</param>
        /// <param name="telefon">telefon</param>
        /// <param name="email">email</param>
        /// <param name="aktiv"> aktiv</param>
        /// <param name="emerKontatakti">emer kontakti</param>
        /// <param name="mbiemerKontakti">mbiemer kontakti</param>
        /// <param name="telKontakti">tel kontakti</param>
        /// <param name="adresaKontakti">adresa kontakti</param>
        /// <param name="emailKontakti"> email kontakti</param>
        /// <param name="shenimeKontakti">shenime kontakti</param>
        /// <param name="idGrupPunonjesish">id e grupit te punonjesit</param>
        /// <param name="llojPagese">lloj pagese 1- mujore 2- ditore, 3-orare</param>
        /// <param name="idMonedha"> id monedha</param>
        /// <param name="idPerdoruesi"> id perdoruesi</param>
        /// <param name="idNdermarje">id nderamrje</param>
        /// <param name="idKonfig">idkonfigambjente</param>
        /// string password
        /// <param name="idStatusDok"> id e statusit te dokumentit</param>
        public clsPunonjes(string nrPersonal, string emer, string mbiemer, string atesia, DateTime datelindja, string nrSig, string qyteti, string adresa, string telefon, string email, bool aktiv, string emerKontatakti, string mbiemerKontakti, string telKontakti, string adresaKontakti, string emailKontakti, string shenimeKontakti, string grup, int llojpagese, int monedha, int idPerdoruesi, int idNdermarje, string konfigurim, int idStatusDok, string objektiv, bool llogaritNgaListorare, int idkrijuesi, string sapid, string nrpashaporte, string gjinia, string kombesia, bool kryefamiljari, string edukimi, string punameparshme, string vendndodhja, string nrjupiter, string username, string shenime, int nrrendor, string lejepune, string nrllogari, string password, ResourceManager rm, CultureInfo ci, int idgjuha, bool shtimModifikim, IDictionary<string, object> hfArkiva)
        {
            var qyt = new clsQyteti(qyteti, idNdermarje);

            var konfig = new clsKonfigurimAmbjenti();
            konfig.mbushKonfigAmbjSipasKod(konfigurim, idNdermarje);
            var grupi = new clsGrupPunonjesish(grup, idNdermarje);

            var objektiva = new clsObjektivaKosto(objektiv, idNdermarje);
            var llogari = new DbCore.DbKontabiliteti.clsLlogari(nrllogari, idNdermarje);
            var gjini = (gjinia != "Mashkull" && gjinia != "Male");
            var komb = MerrKombesiSipasGjuhes(kombesia, idgjuha);
            var education = clsPunonjes.merrIdEdukimeSipasIdNdermarjeDhePershkrimit(idNdermarje, edukimi, idgjuha);
            if (education == 0) throw new MyException("Ky edukim nuk ekziston!");

            var puna = MerrPunaMeparshmeId(punameparshme);
            var vendodh = new clsVendndodhjet();
            if (!string.IsNullOrWhiteSpace(vendndodhja))
            {
                var indexofKllap = vendndodhja.IndexOf('(');
                var kodi = "";
                if (indexofKllap != -1)
                    kodi = vendndodhja.Substring(0, indexofKllap);
                else
                    kodi = vendndodhja;

                vendodh = new clsVendndodhjet(kodi.TrimEnd(), idNdermarje);
                if (vendodh.Id == 0)
                    Vendndodhja = -1;
            }
            this.HfArkiva = hfArkiva;


            #region mbushja e objektit

            NrPersonal = nrPersonal;
            Emer = emer;
            Mbiemer = mbiemer;
            Atesia = atesia;
            this.datelindja = datelindja;
            NrSig = nrSig;
            idQyteti = qyt.IdQyteti;
            Adresa = adresa;
            Telefon = telefon;
            Email = email;
            this.aktiv = aktiv;
            EmerKontakti = emerKontatakti;
            MbiemerKontakti = mbiemerKontakti;
            TelKontakti = telKontakti;
            AdresaKontakti = adresaKontakti;
            EmailKontakti = emailKontakti;
            ShenimeKontakti = shenimeKontakti;
            idGrupPunonjesish = grupi.IdGrupPunonjesish;
            llojPagese = llojpagese;
            idMonedha = monedha;
            this.idPerdoruesi = idPerdoruesi;
            this.idNdermarje = idNdermarje;
            idKonfig = konfig.IdKonfigAmbjente;
            this.idStatusDok = idStatusDok;
            idObjektivaKosto = objektiva.Id;
            this.llogaritNgaListorare = llogaritNgaListorare;
            idKrijuesi = idkrijuesi;
            SapId = sapid;
            NrPashaporte = nrpashaporte;
            this.gjinia = gjini;
            this.kombesia = komb;
            kryefamiliar = kryefamiljari;
            this.edukimi = education;
            punaMeparshme = puna;
            this.vendndodhja = vendodh.Id;
            NrJupiter = nrjupiter;
            Username = username;
            Shenime = shenime;
            nrRendor = nrrendor;
            LejePune = lejepune;
            IdLlogari = llogari.IdLlogari;
            Password = password;
            OColKomponente = new colKomponenteListPagesePunonjesi();
            OColPagaShtesa = new colPagaShtesa();
            OColPunesimet = new colPunesim();
            OColSkemat = new colSkemaSigurimi();
            BankaPunonjes = new clsBankaPunonjes();
            QendraKostoPunonjes = new clsQendraKostoPunonjes();
            BandaPunonjes = new clsBandaPunonjes();
            #endregion

            var mesazh = kontrollo(objektiv, qyteti,  grup, kombesia, nrllogari, rm, ci, shtimModifikim);
            if (!mesazh.Status)
                throw new Exception(mesazh.PershkrimMesazhi);
        }

        private static int MerrKombesiSipasGjuhes(string kombesia, int idgjuha)
        {
            int komb;
            switch (idgjuha)
            {
                case 0:

                    komb = merrIdKombesieSHQ(kombesia);
                    break;
                case 1:
                case 2:
                    komb = merrIdKombesie(kombesia);
                    break;
                default:
                    throw new MyException($"gjuha me id {idgjuha} nuk ekziston!");

            }

            return komb;
        }

        private static int MerrPunaMeparshmeId(string punameparshme)
        {
            int puna;
            switch (punameparshme)
            {
                case "Publik":
                case "Public":
                case "Publique":
                    puna = 1;
                    break;
                case "Privat":
                    puna = 2;
                    break;
                case "Eksperienca e Pare":
                    puna = 3;
                    break;
                case "Private":
                    puna = 2;
                    break;
                case "First Experiences":
                    puna = 3;
                    break;
                case "Papunesia":
                    puna = 4;
                    break;
                case "Unemployment":
                    puna = 4;
                    break;
                case "Other":
                    puna = 5;
                    break;
                case "Te Tjera":
                    puna = 5;
                    break;
                case "Pagese Papunesie":
                    puna = 6;
                    break;
                case "Unemployment salary":
                    puna = 6;
                    break;
                default:
                    throw new MyException("Kjo pune e meparshme nuk ekziston!");
            }

            return puna;
        }

        private clsMesazh kontrollo(string objektiva, string qyteti, string grupi, string kombesia, string nrllogari, ResourceManager rm, CultureInfo ci, bool shtimModifikim)
        {
            if (NrPersonal == "")
                return new clsMesazh(false, rm.GetString("msgNrPersonalBosh", ci));
            if (shtimModifikim) {
                using (clsDatabazeListPagesa db = new clsDatabazeListPagesa())
                {
                    if (db.ekzistonPunonjes(NrPersonal, idNdermarje))
                        return new clsMesazh(false, rm.GetString("msgPunonjesNrPersonal", ci));
                }
            }
            if (SapId != "")
            {
                var kusht = new clsKusht(idKonfig, "GJSI");
                if (kusht.Vlera != 0)
                    if (SapId.Length != kusht.Vlera)
                        return new clsMesazh(false, rm.GetString("msgGjatesiSapId", ci) + " " + kusht.Vlera + " " + rm.GetString("msgKaraktere", ci));

                double sap = 0;
                if (!double.TryParse(SapId, out sap))
                    return new clsMesazh(false, rm.GetString("msgSapId", ci));
            }

            if (objektiva != "")
            {
                if (!clsObjektivaKosto.ekzistonOK(objektiva, idNdermarje))
                    return new clsMesazh(false, rm.GetString("msgShtoLlogariObjektivaEKostosNukEkziston", ci));

                var obj = new clsObjektivaKosto(objektiva, idNdermarje);
                if (!obj.Aktiv)
                    return new clsMesazh(false, rm.GetString("msgShtoLlogariObjektivaEKostosNukEsteAktive", ci));
            }
            if (qyteti != "")
            {
                if (!clsQyteti.ekzistonQytetiSipasEmer(qyteti, idNdermarje))
                    return new clsMesazh(false, rm.GetString("msgQytetiNukEkziston", ci));
            }
            if (grupi != "")
            {
                if (!clsGrupPunonjesish.ekziston(grupi, idNdermarje))
                    return new clsMesazh(false, rm.GetString("msgPunonjesiGrupiIPunonjesveNukEkziston", ci));
            }
            if (Vendndodhja == -1)
                return new clsMesazh(false, rm.GetString("msgVendndodhja", ci));
            if (kombesia != "")
            {
                if (this.kombesia < 1)
                    return new clsMesazh(false, "Kombesia nuk ekziston!");
            }

            if (nrllogari != "" && nrllogari != string.Empty)
            {

                var llog = new DbCore.DbKontabiliteti.clsLlogari(nrllogari, idNdermarje);
                if (llog.IdLlogari == 0)
                    return new clsMesazh(false, rm.GetString("msgCeljeArkaBankaLlogariaNukEkziston", ci));
                if (!llog.Aktiv)
                    return new clsMesazh(false, rm.GetString("msgCeljeArkaBankaLlogariaNukEshteAktive", ci));
               


            }

            return new clsMesazh(true, "Kontrollet u kaluan me sukses!");

        }

        /// <summary>
        ///     konstruktor me 2 parametra
        /// </summary>
        /// <param name="nrpersonal">nr personal</param>
        /// <param name="idNderm">id e ndermarrjes</param>
        public clsPunonjes(string nrpersonal, int idNderm)
        {
            using (var db = new clsDatabazeListPagesa())
            {
                db.kthePunonjesSipasNrPersonal(nrpersonal, idNderm, this);
                
            }
        }

        public clsPunonjes(string nrpersonal, int idNderm, clsDatabazeListPagesa db)
        {
            db.kthePunonjesSipasNrPersonal(nrpersonal, idNderm, this);
        }

        /// <summary>
        ///     konstruktor me 1 parameter
        /// </summary>
        /// <param name="idpunonjes">id e punonjes</param>
        public clsPunonjes(int idpunonjes)
        {
            var db = new clsDatabazeListPagesa();
            db.kthePunonjes(idpunonjes, this);
            db.Dispose();
        }
        
        /// <summary>
        ///     konstruktor me 1 parameter
        /// </summary>
        /// <param name="idpunonjes">id e punonjes</param>
        public clsPunonjes(int idpunonjes, clsDatabazeListPagesa db)
        {
            db.kthePunonjes(idpunonjes, this);
        }


        /// <summary>
        ///     konstruktor qe mbush objektin e clsPerdorues ne baze te username
        /// </summary>
        /// <param name="userName"></param>
        public clsPunonjes(string userName)
        {
            using (var db = new clsDatabazeListPagesa())
            {
                db.ktheGjithePunonjesitSipasUserName(userName, this);
            }
        }

        #endregion

        #region Properties

        public clsBankaPunonjes BankaPunonjes { get; set; }

        /// <summary>
        ///     edukimi i punonjesit 1-shkolle e meseme, 2-shkolle e larte,3-master
        /// </summary>
        public int Edukimi
        {
            get { return edukimi; }
            set { edukimi = value; }
        }

        /// <summary>
        ///     gjinia false -mashkull, true femer
        /// </summary>
        public bool Gjinia
        {
            get { return gjinia; }
            set { gjinia = value; }
        }

        /// <summary>
        ///     id e krijuesit qe e ka krijuar punonjesin
        /// </summary>
        public int IdKrijuesi
        {
            get { return idKrijuesi; }
            set { idKrijuesi = value; }
        }

        /// <summary>
        ///     id e punonjesit
        /// </summary>
        public int IdPunonjes
        {
            get { return idPunonjes; }
            set { idPunonjes = value; }
        }

        public int IdLlogari
        { 
            get {return idLlogari; }
            set { idLlogari = value;}
        }

        /// <summary>
        ///     kombesia e punonjesit, psh shqiptare
        /// </summary>
        public int Kombesia
        {
            get { return kombesia; }
            set { kombesia = value; }
        }

        /// <summary>
        ///     kryefamiliar po apo jo
        /// </summary>
        public bool Kryefamiliar
        {
            get { return kryefamiliar; }
            set { kryefamiliar = value; }
        }

        /// <summary>
        ///     kthen/vendos fushen leje pune
        /// </summary>
        public string LejePune { get; set; }

        /// <summary>
        ///     tregon nese llogaritja e pages se punonjesit do merret nga importi i list orave apo do shenohet me dore gjate
        ///     rregjistrimit te listpagesave
        /// </summary>
        public bool LlogaritNgaListorare
        {
            get { return llogaritNgaListorare; }
            set { llogaritNgaListorare = value; }
        }

        /// <summary>
        ///     nr qe lidhet me pagesen e internetit dhe telefonit nga kompania
        /// </summary>
        public string NrJupiter { get; set; }

        /// <summary>
        ///     nr i pashaportes
        /// </summary>
        public string NrPashaporte { get; set; }

        /// <summary>
        ///     nr personal
        /// </summary>
        public string NrPersonal { get; set; }

        /// <summary>
        ///     emri i punonjesit
        /// </summary>
        public string Emer { get; set; }

        /// <summary>
        ///     mbiemri
        /// </summary>
        public string Mbiemer { get; set; }

        /// <summary>
        ///     atesia
        /// </summary>
        public string Atesia { get; set; }

        /// <summary>
        ///     datelindja
        /// </summary>
        public DateTime Datelindja
        {
            get { return datelindja; }
            set { datelindja = value; }
        }

        public int NrRendor
        {
            get { return nrRendor; }
            set { nrRendor = value; }
        }

        /// <summary>
        ///     nr i sigurimeve
        /// </summary>
        public string NrSig { get; set; }

        /// <summary>
        ///     id e  qytetit
        /// </summary>
        public int IdQyteti
        {
            get { return idQyteti; }
            set { idQyteti = value; }
        }

        /// <summary>
        ///     adresa
        /// </summary>
        public string Adresa { get; set; }

        /// <summary>
        ///     puna e meparshme 1-sektori publik, 2- privat,3- eksperienca e pare
        /// </summary>
        public int PunaMeparshme
        {
            get { return punaMeparshme; }
            set { punaMeparshme = value; }
        }

        public clsQendraKostoPunonjes QendraKostoPunonjes { get; set; }
        public clsBandaPunonjes BandaPunonjes { get; set; }
        /// <summary>
        ///     id e sapit
        /// </summary>
        public string SapId { get; set; }

        /// <summary>
        ///     shenime
        /// </summary>
        public string Shenime { get; set; }

        /// <summary>
        ///     telefon
        /// </summary>
        public string Telefon { get; set; }

        /// <summary>
        ///     email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     aktiv
        /// </summary>
        public bool Aktiv
        {
            get { return aktiv; }
            set { aktiv = value; }
        }

        /// <summary>
        ///     emeri i kontaktit
        /// </summary>
        public string EmerKontakti { get; set; }

        /// <summary>
        ///     mbiemer kontakti
        /// </summary>
        public string MbiemerKontakti { get; set; }

        /// <summary>
        ///     telefon kontakti
        /// </summary>
        public string TelKontakti { get; set; }

        /// <summary>
        ///     adresa kontakti
        /// </summary>
        public string AdresaKontakti { get; set; }

        /// <summary>
        ///     email i kontaktit
        /// </summary>
        public string EmailKontakti { get; set; }

        /// <summary>
        ///     shenime kontakti
        /// </summary>
        public string ShenimeKontakti { get; set; }

        /// <summary>
        ///     id e grupit te punonjesit
        /// </summary>
        public int IdGrupPunonjesish
        {
            get { return idGrupPunonjesish; }
            set { idGrupPunonjesish = value; }
        }

        /// <summary>
        ///     lloji i pageses
        ///     <example>1-mujore,2-ditore,3-orare</example>
        /// </summary>
        public int LlojPagese
        {
            get { return llojPagese; }
            set { llojPagese = value; }
        }

        /// <summary>
        ///     id e monedhes
        /// </summary>
        public int IdMonedha
        {
            get { return idMonedha; }
            set { idMonedha = value; }
        }

        /// <summary>
        ///     id e perdoruesit qe ka kryer veprimin
        /// </summary>
        public int IdPerdoruesi
        {
            get { return idPerdoruesi; }
            set { idPerdoruesi = value; }
        }

        /// <summary>
        ///     id e ndermarjes
        /// </summary>
        public int IdNdermarje
        {
            get { return idNdermarje; }
            set { idNdermarje = value; }
        }

        /// <summary>
        ///     id e konfigurimit te dokumentit
        /// </summary>
        public int IdKonfig
        {
            get { return idKonfig; }
            set { idKonfig = value; }
        }

        /// <summary>
        ///     id e status te dok
        ///     <example> 0 draft, 1-ruajtur,2 -fshire</example>
        /// </summary>
        public int IdStatusDok
        {
            get { return idStatusDok; }
            set { idStatusDok = value; }
        }

        /// <summary>
        ///     data e krijimit te kompoentes
        /// </summary>
        public DateTime DtKrijimi
        {
            get { return dtKrijimi; }
        }

        /// <summary>
        ///     data e modifikimi te fundit te komponentes
        /// </summary>
        public DateTime DtModifikimi
        {
            get { return dtModifikimi; }
        }

        /// <summary>
        ///     id e objektivit te kostos
        /// </summary>
        public int IdObjektivaKosto
        {
            get { return idObjektivaKosto; }
            set { idObjektivaKosto = value; }
        }

        /// <summary>
        ///     objektiva e kostos
        /// </summary>
        public string Objektiva { get; private set; }

        /// <summary>
        ///     kthen emrin e qytetit te punonjesit
        /// </summary>
        public string Qyteti { get; private set; }

        /// <summary>
        ///     kthen grupin e punonjesit
        /// </summary>
        public string GrupPunonjesish { get; private set; }

        /// <summary>
        ///     kthen kodin e monedhes
        /// </summary>
        public string Monedha { get; private set; }

        /// <summary>
        ///     username per programin tjeter
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     vendndodhja e punojesit
        /// </summary>
        public int Vendndodhja
        {
            get { return vendndodhja; }
            set { vendndodhja = value; }
        }

        /// <summary>
        ///     kthen id e departamentit
        /// </summary>
        public int IdDepartament
        {
            get { return idDepartament; }
        }

        /// <summary>
        ///     kthen id e nendepartamentit
        /// </summary>
        public int IdNenDepartament
        {
            get { return idNenDepartament; }
        }

        /// <summary>
        ///     kthen vendos punesimet e punonjesit
        /// </summary>
        public colPunesim OColPunesimet { get; set; }

        /// <summary>
        ///     kthen vendos skemat e sigurimit
        /// </summary>
        public colSkemaSigurimi OColSkemat { get; set; }

        /// <summary>
        ///     kthen vendos pagat dhe shtesat
        /// </summary>
        public colPagaShtesa OColPagaShtesa { get; set; }

        /// <summary>
        ///     kthen vendos komponentet e list pagesave
        /// </summary>
        public colKomponenteListPagesePunonjesi OColKomponente { get; set; }

        public string Password { get; set; }

        [JsonIgnore]
        public StringBuilder Errors { get; set; } = new StringBuilder();

        public IDictionary<string, object> HfArkiva { get; set; }

        #endregion

        #region Metoda Publike
        public static string merrNumerTelefoneSipasUsername(string username)
        {
            using (var db = new clsDatabazeListPagesa())
                return Convert.ToString(db.merrNrTelSipasUsername(username)["TELEFON"]);
        }


        public static bool eshteLidhurPunMeVepArkeBanke(int idpunonjes)
        {
            using (var db = new clsDatabazeListPagesa())
                return  db.eshteLidhurPunMeVepArkeBanke(idpunonjes);
        }
        /// <summary>
        ///     ruan punonjesin
        /// </summary>
        /// <param name="db"> clsDatabaseListPagese per te qene pjese e trasaksionit</param>
        /// <returns> cls mesazh qe tregon nqs eshte ruajtur apo jo punonjesi</returns>
        public clsMesazh ruaj(IDictionary<string, object> hfNrAutoKF, CultureInfo ci, System.Resources.ResourceManager rm)
        {
            var mesazh = new clsMesazh();
            var kontrollonrsigurimesh = clsAlternativaKushti.getAlternativa(IdKonfig, "NRSIGI") == "Po" ? true : false; //
            var kontrolloSap = clsAlternativaKushti.getAlternativa(IdKonfig, "SAPI") == "Po" ? true : false; //
            var kontrolloLlogBankare = clsAlternativaKushti.getAlternativa(IdKonfig, "LLOGBANI") == "Po" ? true : false; //

            var aktivpun = clsAlternativaKushti.getAlternativa(idKonfig, "KPA") == "Po";
            using (var scope = new MyTransactionScope())
            {
                var db = new clsDatabazeListPagesa();
                mesazh = ruaj(hfNrAutoKF, kontrollonrsigurimesh, kontrolloSap, kontrolloLlogBankare, aktivpun, db, ci, rm);

                if (!mesazh) return mesazh;

                scope.Complete();
                return mesazh;
            }
        }


        private clsMesazh ruaj(clsDatabazeListPagesa data)
        {
            int id;
            var u_ruajt = data.ruajPunonjes(out id, NrPersonal, Emer, Mbiemer, Atesia, datelindja, NrSig, idQyteti, Adresa, Telefon, Email, aktiv, EmerKontakti, MbiemerKontakti, TelKontakti, AdresaKontakti, EmailKontakti, ShenimeKontakti, idGrupPunonjesish, llojPagese, idMonedha, idKonfig, idPerdoruesi, idNdermarje, idStatusDok, idObjektivaKosto, LlogaritNgaListorare, idKrijuesi, SapId, NrPashaporte, gjinia, kombesia, kryefamiliar, edukimi, punaMeparshme, vendndodhja, NrJupiter, Username, Shenime, nrRendor, LejePune, Password, IdLlogari);
            IdPunonjes = id;
            return u_ruajt;
        }

        private clsMesazh kontrolloEkzistenca(bool kontrollonrsigurimesh, bool kontrolloSap, bool kontrolloLlogBankare, bool aktivpun, int idpunonjes, clsDatabazeListPagesa db, CultureInfo ci, ResourceManager rm)
        {
            string gabimekzistimi = rm.GetString("msgPunonjesNrPersonal", ci);
            string gabimekzistimi1 = rm.GetString("msgPunonjesNrSigurimesh", ci);
            string gabimekzistimi2 = rm.GetString("msgPunonjesSapId", ci);
            string gabimekzistimi3 = rm.GetString("msgPunonjesLlogBank", ci);
            if (db.ekzistonPunonjesPerKontrolloEkzisto(NrPersonal, idNdermarje, idpunonjes))
                return new clsMesazh(false, gabimekzistimi);
            if (NrSig != "" && kontrollonrsigurimesh && db.ekzistonPunonjesNrSig(NrSig, idNdermarje, aktivpun, idpunonjes))
            {
                return new clsMesazh(false, gabimekzistimi1);
            }
            if (SapId != "" && kontrolloSap && db.ekzistonPunonjesSap(SapId, idNdermarje, idpunonjes))
            {
                return new clsMesazh(false, gabimekzistimi2);
            }
            if (BankaPunonjes.LlogBankare != "" && BankaPunonjes.LlogBankare != null && kontrolloLlogBankare && db.ekzistonPunonjesnrllog(BankaPunonjes.LlogBankare, idNdermarje, idpunonjes))
            {
                return new clsMesazh(false, gabimekzistimi3);
            }
            return new clsMesazh(true, "Kontrollet u kaluan me sukses!");
        }

        internal static clsMesazh ruajLog(int idpunonjes, int idkrijuesi, int llojveprimi, string fushat, string vlerat, clsDatabazeListPagesa db)
        {
            var id = 0;
            var mesazh = db.ruajLogPunonjes(out id, idpunonjes, idkrijuesi, llojveprimi, fushat, vlerat);
            return mesazh;
        }

        private clsMesazh ruaj(IDictionary<string, object> hfNrAutoKF, bool kontrollonrsigurimesh, bool kontrolloSap, bool kontrolloLlogBankare, bool aktivpun, clsDatabazeListPagesa db, CultureInfo ci, System.Resources.ResourceManager rm)
        {
            var mesazh = new clsMesazh();
            bool kaNdryshimNumri;
            var mesazhKontrolli = kontrolloNrAutoPunonjes(out kaNdryshimNumri, db, hfNrAutoKF, false);
            if (!mesazhKontrolli.Status)
                return mesazhKontrolli;

            mesazh = kontrolloEkzistenca(kontrollonrsigurimesh, kontrolloSap, kontrolloLlogBankare, aktivpun, 0, db, ci, rm);
            if (!mesazh.Status)
                return mesazh;

            mesazh = ruaj(db);
            mesazh = colArkiva.RuajArkiven(IdPunonjes, 37, idPerdoruesi, idNdermarje, HfArkiva);
            if (!mesazh.Status)
                return new clsMesazh(false, rm.GetString("msgPunonjesRuajFail", ci));
            mesazh = ruajLog(IdPunonjes, idKrijuesi, 0, "U shtua punonjesi me kod " + NrPersonal, "U shtua punonjesi me kod " + NrPersonal, db);
            if (!mesazh.Status)
                return new clsMesazh(false, rm.GetString("msgPunonjesRuajFail", ci));

            foreach (var punesim in OColPunesimet)
            {
                punesim.IdPunonjes = idPunonjes;
                mesazh = punesim.ruaj(db);
                if (!mesazh.Status)
                    return new clsMesazh(false, rm.GetString("msgNjePunesimFail", ci));
            }

            if (QendraKostoPunonjes.IdQendraKosto1 != 0 || QendraKostoPunonjes.IdQendraKosto2 != 0)
            {
                QendraKostoPunonjes.IdPunonjes = idPunonjes;
                mesazh = QendraKostoPunonjes.ruaj(db);
                if (!mesazh.Status)
                    return new clsMesazh(false, rm.GetString("msgNdodhiNjeGabimGjateRuajtjesSeQendraKosto", ci));

            }
            if (BandaPunonjes.IdGrupimGlobal != 0 || BandaPunonjes.IdGrupimLokal != 0)
            {
                BandaPunonjes.IdPunonjes = idPunonjes;
                mesazh = BandaPunonjes.ruaj(db);
                if (!mesazh.Status)
                    return new clsMesazh(false, "Gabim gjate ruatjes se bandave");
            }

            if (BankaPunonjes.DtAktivizimi.ToShortDateString() != "01/01/0001")
            {
                BankaPunonjes.IdPunonjes = idPunonjes;
                mesazh = BankaPunonjes.ruaj(db);
                if (!mesazh.Status)
                    return new clsMesazh(false, rm.GetString("msgNdodhiNjeGabimGjateRuajtjesSeQendraKosto", ci));
            }

            foreach (var skema in OColSkemat)
            {
                skema.IdPunonjes = idPunonjes;
                mesazh = skema.ruaj(db);
                if (!mesazh) return new clsMesazh(false, rm.GetString("msgPunonjesSkemaSig", ci));
            }
            if (OColPagaShtesa != null)
            {
                mesazh = OColPagaShtesa.Ruaj(idPunonjes, rm, ci);
                if (!mesazh) return mesazh;
            }
            if (OColKomponente != null)
            {
                mesazh = OColKomponente.Ruaj(idPunonjes, rm, ci);
                if (!mesazh) return mesazh;
            }

            if (kaNdryshimNumri) return mesazhKontrolli;       
            return new clsMesazh(true, rm.GetString("msgPunonjesRuajtjeSukses", ci)); 
        }

        private clsMesazh kontrolloNrAutoPunonjes(out bool kaNdryshimNrAuto, clsDatabazeListPagesa db, IDictionary<string, object> hfNrAutoKF, bool modifikim)
        {
            kaNdryshimNrAuto = false;

            if (!modifikim)
            {
                var mes = new clsMesazh();
                if (hfNrAutoKF != null)
                {
                    mes = kontrolloNrAutoPun(out kaNdryshimNrAuto, db, hfNrAutoKF);
                    if (!mes.Status)
                        return mes;
                }
                return new clsMesazh(mes.Status, mes.PershkrimMesazhi);
            }
            return new clsMesazh(true, "Kontrollet u kaluan me sukses");
        }

        private clsMesazh kontrolloNrAutoPun(out bool kaNdryshimNumri, clsDatabazeListPagesa db, IDictionary<string, object> hfNrAutoKf)
        {
            var dbadm = new clsDatabaseAdmin(db);

            var list = clsNrAutom.kontrollogjithenumrat(dbadm, hfNrAutoKf, DateTime.Today);
            if (NrAuto.ktheVlerenEre(list, "NrPersonal") != "")
                NrPersonal = NrAuto.ktheVlerenEre(list, "NrPersonal");
            if (NrAuto.ktheVlerenEre(list, "NrKontrate") != "")
                OColPunesimet[0].NrKontrate = NrAuto.ktheVlerenEre(list, "NrKontrate");
            var mes = NrAuto.ruajvlera(out kaNdryshimNumri, list, DateTime.Today, idPerdoruesi, idNdermarje, dbadm);
            return new clsMesazh(mes.Status, mes.PershkrimMesazhi);
        }

        public clsMesazh modifiko(CultureInfo ci, ResourceManager rm)
        {
            var mesazh = new clsMesazh();
            var punonjevjeter = new clsPunonjes();
            var qendravjetra = new colQendraKostoPunonjes();
            var bandavjeter = new colBandaPunonjes();
            var bankavjeter = new colBankaPunonjes();
            var skemaVjeter = new clsSkemaSigurimi();
            var pagaefundit = new colPagaShtesa();
            var kompfundit = new colKomponenteListPagesePunonjesi();
            merrTeDhenatEVjetra(OColSkemat[0].DtAktivizimi, OColPagaShtesa[0].DtAktivizimi, OColKomponente[0].DtAktivizimi, ref punonjevjeter, ref qendravjetra, ref bankavjeter, ref skemaVjeter, ref pagaefundit, kompfundit, ref bandavjeter);
            const int nrkomponente = 708;
            var kontrollonrsigurimesh = clsAlternativaKushti.getAlternativa(IdKonfig, "NRSIGI") == "Po" ? true : false; //clsAtributeTrupi.merrIdentifikuesSipasKontrollitDheKonfigurimit(IdKonfig, "txtNrSig", nrkomponente) == 1 ? true : false;
            var kontrolloSap = clsAlternativaKushti.getAlternativa(IdKonfig, "SAPI") == "Po" ? true : false; //clsAtributeTrupi.merrIdentifikuesSipasKontrollitDheKonfigurimit(IdKonfig, "txtSap", nrkomponente) == 1 ? true : false;
            var kontrolloLlogBankare = clsAlternativaKushti.getAlternativa(IdKonfig, "LLOGBANI") == "Po" ? true : false; // clsAtributeTrupi.merrIdentifikuesSipasKontrollitDheKonfigurimit(IdKonfig, "txtLlogBankare", nrkomponente) == 1 ? true : false;
            var aktivpun = clsAlternativaKushti.getAlternativa(idKonfig, "KPA") == "Po";

            using (var scope = new MyTransactionScope())
            {
                var db = new clsDatabazeListPagesa();
                mesazh = kontrolloEkzistenca(kontrollonrsigurimesh, kontrolloSap, kontrolloLlogBankare, aktivpun, IdPunonjes, db, ci, rm);
                if (!mesazh) return mesazh;
                mesazh = modifiko(db, punonjevjeter, qendravjetra, bankavjeter, skemaVjeter, pagaefundit, kompfundit, bandavjeter,ci);
                if (!mesazh) return mesazh;

                scope.Complete();
                return mesazh;
            }
        }

        private void merrTeDhenatEVjetra(DateTime skemaaktivizim, DateTime pagaaktivizim, DateTime komponenteaktivizim, ref clsPunonjes punonjesvjeter, ref colQendraKostoPunonjes qendravjetra, ref colBankaPunonjes bankavjeter, ref clsSkemaSigurimi skemaVjeter, ref colPagaShtesa pagaefundit, colKomponenteListPagesePunonjesi kompfundit, ref colBandaPunonjes bandavjeter)
        {
            punonjesvjeter = new clsPunonjes(idPunonjes);
            punonjesvjeter.OColPunesimet = new colPunesim(idPunonjes);
            punonjesvjeter.OColPagaShtesa = new colPagaShtesa(idPunonjes, pagaaktivizim);
            punonjesvjeter.OColKomponente = new colKomponenteListPagesePunonjesi(idPunonjes, komponenteaktivizim);
            qendravjetra = new colQendraKostoPunonjes(idPunonjes);
            bandavjeter = new DbListPagesat.colBandaPunonjes(idPunonjes);
            bankavjeter = new colBankaPunonjes(idPunonjes);
            skemaVjeter = new clsSkemaSigurimi(idPunonjes, skemaaktivizim);
            pagaefundit = colPagaShtesa.merrPagaShtesaSipasPunonjesiDheDatesMeTeFundit(DateTime.Today, idPunonjes);
            kompfundit.ktheKomponenteListPagesePunonjesiSipasPunonjesitDatesMeTeFundit(DateTime.Today, idPunonjes);
        }

        private clsMesazh modifiko(clsDatabazeListPagesa data)
        {
            var u_ruajt = data.modifikoPunonjes(idPunonjes, NrPersonal, Emer, Mbiemer, Atesia, datelindja, NrSig, idQyteti, Adresa, Telefon, Email, aktiv, EmerKontakti, MbiemerKontakti, TelKontakti, AdresaKontakti, EmailKontakti, ShenimeKontakti, idGrupPunonjesish, llojPagese, idMonedha, idKonfig, idPerdoruesi, idNdermarje, idStatusDok, idObjektivaKosto, LlogaritNgaListorare, SapId, NrPashaporte, gjinia, kombesia, kryefamiliar, edukimi, punaMeparshme, vendndodhja, NrJupiter, Username, Shenime, nrRendor, LejePune, IdLlogari);

            return u_ruajt;
        }

        /// <summary>
        ///     modifikon Komponente page
        /// </summary>
        /// <returns> cls mesazh qe tregon nqs eshte ruajtur apo jo komponentja</returns>
        private clsMesazh modifiko(clsDatabazeListPagesa db, clsPunonjes punonjesvjeter, colQendraKostoPunonjes qendravjetra, colBankaPunonjes bankavjeter, clsSkemaSigurimi skemaVjeter, colPagaShtesa pagaefundit, colKomponenteListPagesePunonjesi kompfundit, colBandaPunonjes bandavjeter, CultureInfo ci)
        {
            var rm = new System.Resources.ResourceManager("Resources.Strings", Assembly.Load("App_GlobalResources"));
            var mesazh = new clsMesazh();
            try
            {
                #region punonjes

                var fushatmod = "";
                var mesazhmod = kontrollopunonjes(punonjesvjeter, this, out fushatmod, db);
                if (mesazhmod != "U modifikuan fushat:")
                {
                    mesazh = ruajLog(idPunonjes, idPerdoruesi, 1, mesazhmod, fushatmod, db);
                    if (!mesazh.Status)
                        return new clsMesazh(false, rm.GetString("msgPunonjesRuajFail", ci));
                }
                mesazh = modifiko(db);
                if (!mesazh.Status)
                    return new clsMesazh(false, rm.GetString("msgPunonjesModifikimFail", ci));

                #endregion

                #region punesimi

                mesazh = OColPunesimet.modifikoPunesime(db, punonjesvjeter, idPunonjes, IdPerdoruesi, rm, ci);
                if (!mesazh.Status)
                    return mesazh;

                #endregion

                #region qendra

                mesazh = QendraKostoPunonjes.modifikoQendra(db, qendravjetra, idPunonjes, idPerdoruesi,rm,ci);
                if (!mesazh.Status)
                    return mesazh;

                #endregion
                #region banda

                mesazh = BandaPunonjes.modifikoBanda(db, bandavjeter, idPunonjes, idPerdoruesi);
                if (!mesazh.Status)
                    return mesazh;

                #endregion

                #region banka

                mesazh = BankaPunonjes.modifikoBanke(db, bankavjeter, idPunonjes, idPerdoruesi, rm, ci);
                if (!mesazh.Status)
                    return mesazh;

                #endregion

                #region skema

                mesazh = OColSkemat.modifikoSkeme(db, skemaVjeter, idPunonjes, idPerdoruesi, rm, ci);
                if (!mesazh.Status)
                    return mesazh;

                #endregion

                #region paga

                mesazh = OColPagaShtesa.modifikoPagaDheShtesa(db, punonjesvjeter, pagaefundit, idPunonjes, idPerdoruesi, rm, ci);
                if (!mesazh) return mesazh;

                #endregion

                #region komp

                mesazh = OColKomponente.modifikoKomponente(db, punonjesvjeter, kompfundit, idPunonjes, idPerdoruesi, rm, ci);
                if (!mesazh) return mesazh;
                return new clsMesazh(true, rm.GetString("msgPunonjesModifikimSukses", ci));

                #endregion
            }
            catch (Exception ex)
            {
                ImbLogger.Error(ex);
                return new clsMesazh(false, ex.Message);
            }
        }

        /// <summary>
        ///     modifikon passwordin e punonjesit
        /// </summary>
        /// <param name="newPass"></param>
        /// <returns></returns>
        public clsMesazh modifikoPassword(string newPass, int idPunonjes)
        {
            using (var scope = new MyTransactionScope())
            {
                var db = new clsDatabaseAdmin();
                var mesazh = modifikoPassword(newPass, db, idPunonjes);
                if (!mesazh) return mesazh;
                scope.Complete();
                return mesazh;
            }
        }

        public static clsMesazh modifikoPassword(string newPass, clsDatabaseAdmin db, int idPunonjes)
        {
            var mesazh = new clsMesazh();
            mesazh = db.modifikoPassowrdPunonjes(idPunonjes, newPass, false);
            var suksesRuajPassHistorik = true;
            if (!mesazh.Status)
                return mesazh;
            var konfigPass = new clsKonfigurimeFjalekalimi(idPunonjes, db, true);
            if (konfigPass.RuajHistorikunPass)
            {
                suksesRuajPassHistorik = konfigPass.shtoPassNeHistorikPunonjes(idPunonjes, newPass, idPunonjes, db);
            }
            mesazh.Status = suksesRuajPassHistorik;
            if (mesazh.Status)
                mesazh.PershkrimMesazhi = MessagesResource.Messages["mesazhRuajtjeMeSukses"];
            else
                mesazh.PershkrimMesazhi = "Ndodhi nje gabim gjate ruajtjes!";
            return mesazh;
        }

        private string kontrollopunonjes(clsPunonjes punonjesvjeter, clsPunonjes punonjes, out string fushatmod, clsDatabazeListPagesa db)
        {
            var mesazh = "U modifikuan fushat:";
            fushatmod = "U modifikuan fushat:";
            var dbadmin = new clsDatabaseAdmin(db);
            var dbqendra = new clsDatabaseQendraKosto(db);
            var dbLlog = new clsDatabaseKontabilitet(db);
            if (punonjesvjeter.NrPersonal != punonjes.NrPersonal)
            {
                mesazh += " Nr. Personal,";
                fushatmod += string.Format(" Nr. Personal nga {0} ne {1},", punonjesvjeter.NrPersonal, punonjes.NrPersonal);
            }
            if (punonjesvjeter.nrRendor != punonjes.nrRendor)
            {
                mesazh += " Nr. Rendor,";
                fushatmod += string.Format(" Nr. Rendor nga {0} ne {1},", punonjesvjeter.nrRendor, punonjes.nrRendor);
            }
            if (punonjesvjeter.LejePune != punonjes.LejePune)
            {
                mesazh += " Leje pune,";
                fushatmod += string.Format(" Leje pune nga {0} ne {1},", punonjesvjeter.LejePune, punonjes.LejePune);
            }
            if (punonjesvjeter.Emer != punonjes.Emer)
            {
                mesazh += " Emri,";
                fushatmod += string.Format(" Emri nga {0} ne {1},", punonjesvjeter.Emer, punonjes.Emer);
            }
            if (punonjesvjeter.Mbiemer != punonjes.Mbiemer)
            {
                mesazh += " Mbiemri,";
                fushatmod += string.Format(" Mbiemri nga {0} ne {1},", punonjesvjeter.Mbiemer, punonjes.Mbiemer);
            }
            if (punonjesvjeter.Atesia != punonjes.Atesia)
            {
                mesazh += " Atesia,";
                fushatmod += string.Format(" Atesia nga {0} ne {1},", punonjesvjeter.Atesia, punonjes.Atesia);
            }
            if (punonjesvjeter.datelindja.ToShortDateString() != punonjes.datelindja.ToShortDateString())
            {
                mesazh += " Datelindja,";
                fushatmod += string.Format(" Datelindja nga {0} ne {1},", punonjesvjeter.datelindja.ToShortDateString(), punonjes.datelindja.ToShortDateString());
            }
            if (punonjesvjeter.NrSig != punonjes.NrSig)
            {
                mesazh += " Nr. Sig,";
                fushatmod += string.Format(" Nr. Sig nga {0} ne {1},", punonjesvjeter.NrSig, punonjes.NrSig);
            }
            if (punonjesvjeter.idQyteti != punonjes.idQyteti)
            {
                mesazh += " Qyteti,";
                var qytvj = new clsQyteti(punonjesvjeter.idQyteti, dbadmin);
                var qytri = new clsQyteti(punonjes.idQyteti, dbadmin);
                fushatmod += string.Format(" Qyteti nga {0} ne {1},", qytvj.EmriQyteti, qytri.EmriQyteti);
            }
            if (punonjesvjeter.Adresa != punonjes.Adresa)
            {
                mesazh += " Adresa,";
                fushatmod += string.Format(" Adresa nga {0} ne {1},", punonjesvjeter.Adresa, punonjes.Adresa);
            }
            if (punonjesvjeter.Telefon != punonjes.Telefon)
            {
                mesazh += " Telefoni,";
                fushatmod += string.Format(" Telefoni nga {0} ne {1},", punonjesvjeter.Telefon, punonjes.Telefon);
            }
            if (punonjesvjeter.Email != punonjes.Email)
            {
                mesazh += " Emaili,";
                fushatmod += string.Format(" Emaili nga {0} ne {1},", punonjesvjeter.Email, punonjes.Email);
            }
            if (punonjesvjeter.aktiv != punonjes.aktiv)
            {
                mesazh += " Aktiv,";
                fushatmod += string.Format(" Aktiv nga {0} ne {1},", punonjesvjeter.aktiv, punonjes.aktiv);
            }
            if (punonjesvjeter.EmerKontakti != punonjes.EmerKontakti)
            {
                mesazh += " Emri i Kontaktit,";
                fushatmod += string.Format(" Emri i Kontaktit nga {0} ne {1},", punonjesvjeter.EmerKontakti, punonjes.EmerKontakti);
            }
            if (punonjesvjeter.MbiemerKontakti != punonjes.MbiemerKontakti)
            {
                mesazh += " Mbiemri i Kontaktit,";
                fushatmod += string.Format(" Mbiemri i Kontaktit nga {0} ne {1},", punonjesvjeter.MbiemerKontakti, punonjes.MbiemerKontakti);
            }
            if (punonjesvjeter.TelKontakti != punonjes.TelKontakti)
            {
                mesazh += " Telefoni i Kontaktit,";
                fushatmod += string.Format(" Telefoni i Kontaktit nga {0} ne {1},", punonjesvjeter.TelKontakti, punonjes.TelKontakti);
            }
            if (punonjesvjeter.AdresaKontakti != punonjes.AdresaKontakti)
            {
                mesazh += " Adresa e Kontaktit,";
                fushatmod += string.Format(" Adresa e Kontaktit nga {0} ne {1},", punonjesvjeter.AdresaKontakti, punonjes.AdresaKontakti);
            }
            if (punonjesvjeter.EmailKontakti != punonjes.EmailKontakti)
            {
                mesazh += " Emaili i Kontaktit,";
                fushatmod += string.Format(" Emaili i Kontaktit nga {0} ne {1},", punonjesvjeter.EmailKontakti, punonjes.EmailKontakti);
            }
            if (punonjesvjeter.ShenimeKontakti != punonjes.ShenimeKontakti)
            {
                mesazh += " Shenime Kontakti,";
                fushatmod += string.Format(" Shenime Kontakti nga {0} ne {1},", punonjesvjeter.ShenimeKontakti, punonjes.ShenimeKontakti);
            }
            if (punonjesvjeter.idGrupPunonjesish != punonjes.idGrupPunonjesish)
            {
                mesazh += " Grupi,";
                var gruvj = new clsGrupPunonjesish(punonjesvjeter.idGrupPunonjesish, db);
                var gruri = new clsGrupPunonjesish(punonjes.idGrupPunonjesish, db);
                fushatmod += string.Format(" Grupi nga {0} ne {1},", gruvj.Nr, gruri.Nr);
            }
            if (punonjesvjeter.llojPagese != punonjes.llojPagese)
            {
                mesazh += " Lloji i pageses,";
                string llojvj, llojri;
                if (punonjesvjeter.llojPagese == 1)
                    llojvj = "Mujore";
                else if (punonjesvjeter.llojPagese == 2)
                    llojvj = "Ditore";
                else llojvj = "Orare";
                if (punonjes.llojPagese == 1)
                    llojri = "Mujore";
                else if (punonjes.llojPagese == 2)
                    llojri = "Ditore";
                else llojri = "Orare";
                if (llojvj != llojri)
                    fushatmod += string.Format(" Lloji i pageses nga {0} ne {1},", llojvj, llojri);
            }
            if (punonjesvjeter.idMonedha != punonjes.idMonedha)
            {
                mesazh += " Monedha,";
                var monvj = new clsMonedha(punonjesvjeter.idMonedha, dbadmin);
                var monri = new clsMonedha(punonjes.idMonedha, dbadmin);
                fushatmod += string.Format(" Monedha nga {0} ne {1},", monvj.KodiMonedha, monri.KodiMonedha);
            }
            if (punonjesvjeter.idObjektivaKosto != punonjes.idObjektivaKosto && (punonjesvjeter.idObjektivaKosto != 0 || punonjes.idObjektivaKosto != -1))
            {
                mesazh += " Objektiva e kostos,";
                var objvj = new clsObjektivaKosto(punonjesvjeter.idObjektivaKosto, dbqendra);
                var objri = new clsObjektivaKosto(punonjes.idObjektivaKosto, dbqendra);
                fushatmod += string.Format(" Objektiva e kostos nga {0} ne {1},", objvj.Kodi, objri.Kodi);
            }
        
            if (punonjesvjeter.idLlogari != punonjes.idLlogari && (punonjesvjeter.idLlogari != 0))
            {
                mesazh += "Nr. llogari pagese,";
                var nrllog1 = new clsLlogari(punonjesvjeter.idLlogari, dbLlog);
                var nrllog2 = new clsLlogari(punonjes.idLlogari,  dbLlog);
                fushatmod += string.Format(" Nr. llogari pagese nga {0} ne {1},", nrllog1.NrLlogari, nrllog2.NrLlogari);
            }
            if (punonjesvjeter.llogaritNgaListorare != punonjes.llogaritNgaListorare)
            {
                mesazh += " Llogarit nga listoraret,";
                fushatmod += string.Format(" Llogarit nga listoraret nga {0} ne {1},", punonjesvjeter.llogaritNgaListorare, punonjes.llogaritNgaListorare);
            }
            if (punonjesvjeter.SapId != punonjes.SapId)
            {
                mesazh += " Sap id,";
                fushatmod += string.Format(" Sap id nga {0} ne {1},", punonjesvjeter.SapId, punonjes.SapId);
            }
            if (punonjesvjeter.NrPashaporte != punonjes.NrPashaporte)
            {
                mesazh += " Nr. Pashaporte,";
                fushatmod += string.Format(" Nr. Pashaporte nga {0} ne {1},", punonjesvjeter.NrPashaporte, punonjes.NrPashaporte);
            }
            if (punonjesvjeter.gjinia != punonjes.gjinia)
            {
                mesazh += " Gjinia,";
                fushatmod += string.Format(" Gjinia nga {0} ne {1},", punonjesvjeter.gjinia ? "Femer" : "Mashkull", punonjes.gjinia ? "Femer" : "Mashkull");
            }
            if (punonjesvjeter.kombesia != punonjes.kombesia)
            {
                mesazh += " Kombesia,";
                var komvj = merrPershkrimKombesie(punonjesvjeter.kombesia, db);
                var komri = merrPershkrimKombesie(punonjes.kombesia, db);
                if (komvj != komri)
                    fushatmod += string.Format(" Kombesia nga {0} ne {1},", komvj, komri);
            }
            if (punonjesvjeter.kryefamiliar != punonjes.kryefamiliar)
            {
                mesazh += " Kryefamiliari,";
                fushatmod += string.Format(" Kryefamiliari nga {0} ne {1},", punonjesvjeter.kryefamiliar, punonjes.kryefamiliar);
            }
            if (punonjesvjeter.edukimi != punonjes.edukimi)
            {
                mesazh += " Edukimi,";
                string eduvj, eduri;
                eduvj = clsPunonjes.merrPershkrimEdukimeSipasId(punonjesvjeter.edukimi, db);
                eduri = clsPunonjes.merrPershkrimEdukimeSipasId(punonjes.edukimi, db);
                if (eduvj != eduri)
                    fushatmod += string.Format(" Edukimi nga {0} ne {1},", eduvj, eduri);
            }
            if (punonjesvjeter.punaMeparshme != punonjes.punaMeparshme)
            {
                mesazh += " Puna e meparshme,";
                string eduvj, eduri;
                if (punonjesvjeter.punaMeparshme == 1)
                    eduvj = "Publik";
                else if (punonjesvjeter.punaMeparshme == 2)
                    eduvj = "Privat";
                else if (punonjesvjeter.punaMeparshme == 3)
                    eduvj = "Eksperienca e Pare";
                else if (punonjesvjeter.punaMeparshme == 4)
                    eduvj = "Papunesia";
                else if (punonjesvjeter.punaMeparshme == 5)
                    eduvj = "Te Tjera";
                else
                    eduvj = "Pagese Papunesie";

                if (punonjes.punaMeparshme == 1)
                    eduri = "Publik";
                else if (punonjes.punaMeparshme == 2)
                    eduri = "Privat";
                else if (punonjes.punaMeparshme == 3)
                    eduri = "Eksperienca e Pare";
                else if (punonjes.punaMeparshme == 4)
                    eduri = "Papunesia";
                else if (punonjes.punaMeparshme == 5)
                    eduri = "Te Tjera";
                else
                    eduri = "Pagese Papunesie";
                if (eduvj != eduri)
                    fushatmod += string.Format(" Puna e meparshme nga {0} ne {1},", eduvj, eduri);
            }
            if (punonjesvjeter.vendndodhja != punonjes.vendndodhja)
            {
                mesazh += " Vendndodhja,";
                var venvj = new clsVendndodhjet(punonjesvjeter.vendndodhja, db);
                var venri = new clsVendndodhjet(punonjes.vendndodhja, db);
                fushatmod += string.Format(" Vendndodhja nga {0} ne {1},", venvj.Pershkrimi, venri.Pershkrimi);
            }
            if (punonjesvjeter.NrJupiter != punonjes.NrJupiter)
            {
                mesazh += " Nr. Jupier,";
                fushatmod += string.Format(" Nr. Jupier nga {0} ne {1},", punonjesvjeter.NrJupiter, punonjes.NrJupiter);
            }
            if (punonjesvjeter.Username != punonjes.Username)
            {
                mesazh += " Username,";
                fushatmod += string.Format(" Username nga {0} ne {1},", punonjesvjeter.Username, punonjes.Username);
            }
            if (punonjesvjeter.Shenime != punonjes.Shenime)
            {
                mesazh += " Shenime,";
                fushatmod += string.Format(" Shenime nga {0} ne {1},", punonjesvjeter.Shenime, punonjes.Shenime);
            }
            if (mesazh.Substring(mesazh.Length - 1, 1) == ",")
            {
                mesazh = mesazh.Substring(0, mesazh.Length - 1) + ".";
                fushatmod = fushatmod.Substring(0, fushatmod.Length - 1) + ".";
            }
            return mesazh;
        }

        /// <summary>
        ///     Fshin objektin punonjesin ne tabelen perkatese ne databaze.Therret funksionin
        /// </summary>
        /// <param name="db">db nqs ben pjese ne nje transaksion</param>
        /// <returns> nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh fshi()
        {
            using (var scope = new MyTransactionScope())
            {
                var db = new clsDatabazeListPagesa();
                var u_fshi = fshi(db);
                if (!u_fshi) return u_fshi;
                scope.Complete();
                return u_fshi;
            }
        }

        private clsMesazh fshi(clsDatabazeListPagesa db)
        {
            var u_fshi = db.fshiPunonjesStatus(idPunonjes, idPerdoruesi);
            if (!u_fshi.Status)
                return u_fshi;
            u_fshi = ruajLog(idPunonjes, idPerdoruesi, 2, "U fshi punonjesi me kod" + NrPersonal, "U fshi punonjesi me kod" + NrPersonal, db);
            return u_fshi;
        }


        /// <summary>
        ///     kontrollon nese ekziston punonjesi me kete nr personal ne kete ndermarje
        /// </summary>
        /// <param name="nrpersonal">nr personal</param>
        /// <param name="idndermarje">idndermarje</param>
        /// <returns> true ose false</returns>
        public static bool ekzistonPunonjes(string nrpersonal, int idndermarje)
        {
            using (var db = new clsDatabazeListPagesa())
                return db.ekzistonPunonjes(nrpersonal, idndermarje);

        }

        public static bool ekzistonUsername(string username, int idndermarje)
        {
            var db = new clsDatabazeListPagesa();
            var ekziston = db.ekzistonUsername(username, idndermarje);
            db.Dispose();
            return ekziston;
        }

        public static DataTable merrKombesiaDT()
        {
            using (var db = new clsDatabazeListPagesa())
            {
                return db.merrKombesiaDT();
            }
        }

        public static string merrPershkrimKombesie(int id, clsDatabazeListPagesa db)
        {
            return db.merrKombesiaSipasID(id);
        }

        public static int merrIdKombesie(string pershkrim)
        {
            using (var db = new clsDatabazeListPagesa())
            {
                return db.merrKombesiaSipasPershkrimin(pershkrim);
            }
        }
        public static int merrIdKombesieSHQ(string pershkrim)
        {
            using (var db = new clsDatabazeListPagesa())
            {
                return db.merrKombesiaSipasPershkriminSHQ(pershkrim);
            }
        }

        public static DataTable merrNdryshimPozicioniDT(int idgjuha)
        {
            var db = new clsDatabazeListPagesa();
            return db.merrNdryshimPozicioniDT(idgjuha);
        }

        public static string MerrNrPersonalSipasID(int idPunonjes)
        {

            ///GTODO implemento sp

            throw new NotImplementedException();
        }
        public static DataTable merrEdukimeSipasIdNdermarje(int idndermarje, int idgjuha)
        {
            using (var db = new clsDatabazeListPagesa())
            {
                return db.merrEdukimeSipasIdNdermarje(idndermarje, idgjuha);
            }
        }
        public static int merrIdEdukimeSipasIdNdermarjeDhePershkrimit(int idndermarje, string pershkrimi, int idgjuha)
        {
            using (var db = new clsDatabazeListPagesa())
            {
                return merrIdEdukimeSipasIdNdermarjeDhePershkrimit(idndermarje, pershkrimi, idgjuha, db);
            }
        }
        public static int merrIdEdukimeSipasIdNdermarjeDhePershkrimit(int idndermarje, string pershkrimi, int idgjuha, clsDatabazeListPagesa db)
        {

            DataRow dr = db.merrEdukimeSipasIdNdermarjeDhePershkrimit(idndermarje, pershkrimi, idgjuha);
            if (dr == null)
                return 0;
            else return int.Parse(dr["ID"].ToString());

        }
        public static string merrPershkrimEdukimeSipasId(int id, clsDatabazeListPagesa db)
        {

            DataRow dr = db.merrEdukimeSipasId(id);
            if (dr == null)
                return "";
            else return dr["PERSHKRIMI"].ToString();

        }
        #endregion

        #region Metoda Internal

        public static clsPunonjes Krijo(IDataRecord record)
        {
            var punonjes = new clsPunonjes();
            punonjes.mbushPunonjes(record);
            return punonjes;
        }

        internal void mbushPunonjes(IDataRecord dbDataRow)
        {
            try
            {
                Converter.ParseExact(dbDataRow["IDPUNONJES"].ToString(), out idPunonjes, "idPunonjes");
                NrPersonal = dbDataRow["NRPERSONAL"].ToString();
                Emer = dbDataRow["EMER"].ToString();
                Mbiemer = dbDataRow["MBIEMER"].ToString();
                Atesia = dbDataRow["ATESIA"].ToString();
                Username = dbDataRow["USERNAME"].ToString();
                Converter.Parse(dbDataRow["DATELINDJA"].ToString(), out datelindja, "datelindja");
                NrSig = dbDataRow["NRSIG"].ToString();
                MbiemerKontakti = dbDataRow["MBIEMERKONTAKTI"].ToString();
                Telefon = dbDataRow["TELEFON"].ToString();
                Converter.Parse(dbDataRow["IDQYTETI"].ToString(), out idQyteti, "idQyteti");
                Adresa = dbDataRow["ADRESA"].ToString();
                TelKontakti = dbDataRow["TELKONTAKTI"].ToString();
                AdresaKontakti = dbDataRow["ADRESAKONTAKTI"].ToString();
                EmailKontakti = dbDataRow["EMAILKONTAKTI"].ToString();
                ShenimeKontakti = dbDataRow["SHENIMEKONTAKTI"].ToString();
                Converter.Parse(dbDataRow["IDGRUPPUNONJESISH"].ToString(), out idGrupPunonjesish, "idGrupPunonjesish");
                Converter.Parse(dbDataRow["NRRENDOR"].ToString(), out nrRendor, "nrRendor");
                Converter.Parse(dbDataRow["LLOJPAGESE"].ToString(), out llojPagese, "llojPagese");
                Converter.Parse(dbDataRow["IDMONEDHA"].ToString(), out idMonedha, "idMonedha");
                Converter.Parse(dbDataRow["IDOBJEKTIVAKOSTO"].ToString(), out idObjektivaKosto, "idObjektivaKosto");
                Objektiva = dbDataRow["OBJEKTIVA"].ToString();
                Converter.ParseExact(dbDataRow["IDPERDORUESI"].ToString(), out idPerdoruesi, "idPerdoruesi");
                Email = dbDataRow["EMAIL"].ToString();
                EmerKontakti = dbDataRow["EMERKONTAKTI"].ToString();
                Converter.ParseExact(dbDataRow["IDNDERMARJE"].ToString(), out idNdermarje, "idNdermarje");
                Converter.Parse(dbDataRow["AKTIV"].ToString(), out aktiv, "aktiv");
                Converter.Parse(dbDataRow["LlogaritNgaListorare"].ToString(), out llogaritNgaListorare, "llogaritNgaListorare");
                Converter.Parse(dbDataRow["IDKONFIG"].ToString(), out idKonfig, "idKonfig");
                Converter.Parse(dbDataRow["IDSTATUSDOK"].ToString(), out idStatusDok, "idStatusDok");
                Converter.Parse(dbDataRow["DTKRIJIMI"].ToString(), out dtKrijimi, "dtKrijimi");
                Converter.Parse(dbDataRow["DTMODIFIKIMI"].ToString(), out dtModifikimi, "dtModifikimi");
                Qyteti = dbDataRow["QYTETIEMRI"].ToString();
                GrupPunonjesish = dbDataRow["NR"].ToString();
                Monedha = dbDataRow["MONEDHAKOD"].ToString();
                Converter.Parse(dbDataRow["IdDepartament"].ToString(), out idDepartament, "idDepartament");
                Converter.Parse(dbDataRow["IdNenDepartament"].ToString(), out idNenDepartament, "idNenDepartament");
                Converter.Parse(dbDataRow["IDKRIJUESI"].ToString(), out idKrijuesi, "idKrijuesi");
                SapId = dbDataRow["SAPID"].ToString();
                NrPashaporte = dbDataRow["NRPASHAPORTE"].ToString();
                Converter.Parse(dbDataRow["GJINIA"].ToString(), out gjinia, "gjinia");
                Converter.Parse(dbDataRow["KOMBESIA"].ToString(), out kombesia, "kombesia");
                Converter.Parse(dbDataRow["EDUKIMI"].ToString(), out edukimi, "edukimi");
                Converter.Parse(dbDataRow["PUNAMEPARSHME"].ToString(), out punaMeparshme, "punaMeparshme");
                Converter.Parse(dbDataRow["VENDNDODHJA"].ToString(), out vendndodhja, "vendndodhja");
                NrJupiter = dbDataRow["NRJUPITER"].ToString();
                Shenime = dbDataRow["SHENIME"].ToString();
                LejePune = dbDataRow["LEJEPUNE"].ToString();
                Password = dbDataRow["PASSWORD"].ToString();
                Converter.Parse(dbDataRow["KRYEFAMILIAR"].ToString(), out kryefamiliar, "kryefamiliar");
                Converter.Parse(dbDataRow["IDLLOGARI"].ToString(), out idLlogari, "idLlogari");
                OColKomponente = new colKomponenteListPagesePunonjesi();
                OColPagaShtesa = new colPagaShtesa();
                OColPunesimet = new colPunesim();
                OColSkemat = new colSkemaSigurimi();
            }
            catch (MyWarnException warn)
            {
                ImbLogger.Warn(warn);
            }
            catch (MyException e)
            {
                throw new MyException( "gabim ne mbushje te punonjesit me nr personal: {0} {1}", NrPersonal, e.Message);
            }
        }

        /// <summary>
        ///     mbush punonjesin me te dhenat nga databaza
        /// </summary>
        /// <param name="dbDataRow">rreshti me te dhena</param>
        /// <returns> kthen ne se mbushja u be ne rregull apo jo</returns>
        /// <summary>
        ///     Mbush punonjesin vetem per People finder
        /// </summary>
        /// <param name="dbDataRow">Rreshti me te dhena</param>
        /// <returns> Kthen nese mbushja u be ne rregull apo jo</returns>
        internal clsMesazh mbushPunonjesPeopleFinder(DataRow dbDataRow)
        {
            if (dbDataRow != null)
            {
                try
                {
                    Emer = dbDataRow["EMER"].ToString();
                    Mbiemer = dbDataRow["MBIEMER"].ToString();
                    Telefon = dbDataRow["TELEFON"].ToString();
                    return new clsMesazh(true, mbushjeSukses);
                }
                catch (InvalidCastException)
                {
                    throw new Exception(gabimNeTeDhena);
                }
            }
            return new clsMesazh(false, drbosh);
        }
        public static clsPunonjes KrijoPunonjesSLim(IDataRecord record)
        {
            var punonjes = new clsPunonjes();
            punonjes.mbushPunonjesSLim(record);
            return punonjes;
        }
        /// <summary>
        /// merr vetem disa fusha te punonjesit
        /// </summary>
        /// <param name="dbDataRow"></param>
        internal void mbushPunonjesSLim(IDataRecord dbDataRow)
        {
            try
            {
                Converter.ParseExact(dbDataRow["IDPUNONJES"].ToString(), out idPunonjes, "idPunonjes");
                NrPersonal = dbDataRow["NRPERSONAL"].ToString();
                Emer = dbDataRow["EMER"].ToString();
                Mbiemer = dbDataRow["MBIEMER"].ToString();
                Atesia = dbDataRow["ATESIA"].ToString();
                Converter.Parse(dbDataRow["DATELINDJA"].ToString(), out datelindja, "datelindja");
                Username = dbDataRow["USERNAME"].ToString();
                Password = dbDataRow["PASSWORD"].ToString();
                Converter.Parse(dbDataRow["IDMONEDHA"].ToString(), out idMonedha, "idMonedha");
                Monedha = dbDataRow["MONEDHAKOD"].ToString();
                Converter.Parse(dbDataRow["IdDepartament"].ToString(), out idDepartament, "idDepartament");
                Converter.Parse(dbDataRow["IdNenDepartament"].ToString(), out idNenDepartament, "idNenDepartament");
                Converter.Parse(dbDataRow["NRRENDOR"].ToString(), out nrRendor, "nrRendor");
                Converter.Parse(dbDataRow["LlogaritNgaListorare"].ToString(), out llogaritNgaListorare, "llogaritNgaListorare");
                Converter.Parse(dbDataRow["Aktiv"].ToString(), out aktiv, "Aktiv");
            }
            catch (Exception ex)
            {
                throw new MyException($"Gabim ne mbushjen e punonjesit me NRPERSONAL {NrPersonal} dhe ID {IdPunonjes}", ex);
            }
        }

        #endregion
    }
}