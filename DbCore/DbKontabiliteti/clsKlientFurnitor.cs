using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Resources;
using DbCore.DbAdmin;
using DbCore.DbArkaBanka;
using DbCore.DbImporte;
using DbCore.DbInventari;
using DbCore.DbQendraKosto;
using DbCore.DbRegjistrim;
using DbCore.DbShare;
using DbCore.IMBUtils;
using DbCore.IMBUtils.Fiskalizimi.Controls;
using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.Messages;
using DbCore.IMBUtils.Validation;
using static System.Convert;

namespace DbCore.DbKontabiliteti
{
    /// <remarks>
    /// Kjo eshte klasa qe sherben per objektet qe perfaqesojne nje klient/furnitor
    ///  (Te dhenat  merren nga tabela : T_KLIENTFURNITOR)
    ///</remarks>
    public class clsKlientFurnitor
    {

        #region Atributet

        private string _pershkrimMetoda;
        private string _kodMaturimiKf;
        private string _kodKushtDergimi;
        private string _kodMenyraTransportit;
        private string _monedha;
        private int _idMonedha;
        private string _monedhaZbritje;
        private DateTime _dtKrijimi;
        private DateTime _dtModifikimi;
        private string _pershkrimTitulliKf;
        private string _kodBanke;
        private colArkiva _oArkiva;
        private colMarreveshjetPerKlient _colMarreveshjet;
        private string _kodPerfaqesuesShitje3;

        #endregion

        #region Kontruktoret

        /// <summary>
        /// Konstruktor i klases
        /// </summary>
        /// <param name="idKlientFurnitor">Id e klientit/furnitorit</param>
        /// <param name="kodKliFurn">Kodi i  klientit/furnitorit</param>
        /// <param name="idLlog"> Id e llogarise se  klientit/furnitorit</param>
        /// <param name="llojiKF">Lloji - klient apo furnitor</param>
        /// <param name="titulliKF">Titulli <example>Kompani, person fizik</example></param>
        /// <param name="aktivitetiKF">Aktiviteti i Kompani, person fizik</param>
        /// <param name="emertimiKF">Emertimi i klientit/furnitorit</param>
        /// <param name="emerKerkimiKF">Emri i kerkimit i klientit/furnitorit</param>
        /// <param name="niptKF">NIPT-i i klientit/furnitorit</param>
        /// <param name="qytetiKF">Qyteti i klientit/furnitorit</param>
        /// <param name="shtetiKF">Shteti i klientit/furnitorit</param>
        /// <param name="telKF">Nr i telefonit i klientit/furnitorit</param>
        /// <param name="faxKF">Nr i fax-it i klientit/furnitorit</param>
        /// <param name="celKF">Nr i celularit i klientit/furnitorit</param>
        /// <param name="emailKF">Adresa e-mail e klientit/furnitorit</param>
        /// <param name="webpageKF">Web page e klientit/furnitorit</param>
        /// <param name="ibanKF">IBAN e klientit/furnitorit</param>
        /// <param name="llogariBankareKF">Llogaria bankare e klientit/furnitorit</param>
        /// <param name="aktivKF">Tregues nese klienti/furnitori eshte aktiv</param>
        /// <param name="idLlogZbritje">Llogaria e zbritjes se klientit/furnitorit</param>
        /// <param name="idLlogariDytesore">Llogaria dytesore e klientit/furnitorit</param>
        /// <param name="idKushtePagese">Kushti i pageses se klientit/furnitorit</param>
        /// <param name="idMetoda">Metoda</param>
        /// <param name="maturimiKF">Maturimi</param>
        /// <param name="idKatZbritje">Kategoria e zbritjes</param>
        /// <param name="limitParalajmerues">Limiti paralajmerues</param>
        /// <param name="limitBllokues">Limiti bllokues</param>
        /// <param name="idKategoriKlienti">Kategoria e klientit/furnitorit</param>
        /// <param name="kushteDergimi">Kushti i dergimit</param>
        /// <param name="menyraTransportit">Menyra e transportit</param>
        /// <param name="ofertaAutomatike">Oferta automatike</param>
        /// <param name="vleraLimitPorositur">Vlera limit  e porositur</param>
        /// <param name="prioriteti">Prioriteti</param>
        /// <param name="cmimUlet">Cmimi i ulet</param>
        /// <param name="idPerfaqesuesShitje">Agjenti i shitjes</param>
        /// <param name="idQenderKosto">Qendra e kostos</param>
        /// <param name="idFushata">Fushata</param>
        /// <param name="zbritjeAnalitike">Zbritja analitike</param>
        /// <param name="zbritjeTotal">Zbritja totale</param>
        /// <param name="idndermarja">ndermarrja</param>
        /// <param name="vit">Viti</param>
        /// <param name="idperdoruesi">Id e perdosruesit</param>
        /// <param name="idKonfig">id e konfigurimit</param>
        /// <param name="llogaritKomision">Llogaritje komisoni ne fature shitje</param>
        /// <param name="kodIntegrimi">Llogaritje komisoni ne fature shitje</param>
        public clsKlientFurnitor(string kodKliFurn, int idLlog, string nrLlog, bool llojiKF, int titulliKF, string pershkrimtitulliKF, string aktivitetiKF, string emertimiKF, string emerKerkimiKF, string niptKF, int qytetiKF, string pershkrimqytetiKF, string shtetiKF, string telKF, string faxKF, string celKF, string emailKF, string webpageKF, string ibanKF, string llogariBankareKF, bool aktivKF, int idLlogZbritje, string nrLlogZbritje, int idLlogariDytesore, string nrLlogariDytesore, int idKushtePagese, string kodKushtePagese, int idMetoda, string pershkrimMetoda, int maturimiKF, string kodmaturimiKF, int idKatZbritje, string kodKatZbritje, int limitParalajmerues, int limitBllokues, int idKategoriKlienti, string kushteDergimi, string kodkushteDergimi, string menyraTransportit, string kodmenyraTransportit, bool ofertaAutomatike, decimal vleraLimitPorositur, int prioriteti, decimal cmimUlet, string pershkrimNivelCmimi, int idPerfaqesuesShitje, string kodPerfaqesuesShitje, int idQenderKosto, int idFushata, int zbritjeAnalitike, string pershkrimzbritjeAnalitike, decimal zbritjeTotal, int idndermarja, int vit, int idperdoruesi, int idKonfig, string licenca, string swift, int emriBanka, string kodemriBanka, string adresaBanka, int grupim1kf, int grupim2kf, int grupim3kf, string kodgrupim1kf, string kodgrupim2kf, string kodgrupim3kf, string nrtvsh, colAdresatKlientFurnitor colAdresa, colKontaktiKlientFurnitor colKontaktet, colBuxhetet colBuxhetet, colVleraFushaShtese colVleraFushaShtese, colLidhjetAutorizim collidhjesaut, bool shtim, int monndermarje, bool kontrolloekzistence, int idobjektivakosto, string objektiva, int idkrijuesi, int idndermarjebij, int llojporosie, bool kupon, IDictionary<string, object> hfArkiva, ResourceManager rm, CultureInfo ci, string koordinata, int idPerfaqesuesShitje2, string kodPerfaqesuesShitje2, bool klientspecifik, bool fermer, bool autongarkese, bool shitjepatvsh, decimal perqindjeAgjent, decimal perqindjeAgjent2, bool prospekt, string kodiMobile, string emailPerPajisje, colMarreveshjetPerKlient colMarreveshjet, int idkfkryesor, string shenime, DateTime dteDatelindjaKF, int idPerfaqesuesShitje3, decimal perqindjeAgjent3, string kodPerfaqesuesShitje3, int idtvsh, string emertimFature, bool llogaritKomision, string kodIntegrimi, string kodiisksh, bool meDogane, string tipiId)
        {
            KodKlientFurnitor = kodKliFurn;
            IdLlogari = idLlog;
            NrLlogKlientFurnitor = nrLlog;
            LlojiKF = llojiKF;
            TitulliKF = titulliKF;
            _pershkrimTitulliKf = pershkrimtitulliKF;
            AktivitetiKF = aktivitetiKF;
            EmertimiKF = emertimiKF;
            EmerKerkimiKF = emerKerkimiKF;
            NiptiKF = niptKF;
            QytetiKF = qytetiKF;
            EmriQytetitKF = pershkrimqytetiKF;
            ShtetiKF = shtetiKF;
            TelKF = telKF;
            FaxKF = faxKF;
            CelKF = celKF;
            EmailKF = emailKF;
            WebPageKF = webpageKF;
            IBANKF = ibanKF;
            KlientSpecifik = klientspecifik;
            LlogariBankareKF = llogariBankareKF;
            AktivKF = aktivKF;
            IdLlogZbritje = idLlogZbritje;
            NrLlogZbritje = nrLlogZbritje;
            IdLlogariDytesore = idLlogariDytesore;
            NrLlogDytesor = nrLlogariDytesore;
            IdKushtePagese = idKushtePagese;
            KodKushtePagese = kodKushtePagese;
            IdMetoda = idMetoda;
            _pershkrimMetoda = pershkrimMetoda;
            MaturimiKF = maturimiKF;
            _kodMaturimiKf = kodmaturimiKF;
            IdKatZbritje = idKatZbritje;
            KodKatZbritje = kodKatZbritje;
            LimitParalajmerues = limitParalajmerues;
            LimitBllokues = limitBllokues;
            IdKategoriKlienti = idKategoriKlienti;
            KushteDergimi = kushteDergimi;
            _kodKushtDergimi = kodkushteDergimi;
            _kodMenyraTransportit = kodmenyraTransportit;
            MenyraTransportit = menyraTransportit;
            OfertaAutomatike = ofertaAutomatike;
            VleraLimitPorositur = vleraLimitPorositur;
            Prioriteti = prioriteti;
            CmimUlet = cmimUlet;
            IdNivelCmimi = clsNivelCmimi.ktheIdNivelCmimiNgaPershkrimi(pershkrimNivelCmimi, idndermarja);
            PershkrimNivelCmimi = pershkrimNivelCmimi;
            IdPerfaqesuesShitje = idPerfaqesuesShitje;
            KodPerfaqesuesShitje = kodPerfaqesuesShitje;
            IdPerfaqesuesShitje2 = idPerfaqesuesShitje2;
            KodPerfaqesuesShitje2 = kodPerfaqesuesShitje2;
            IdQenderKosto = idQenderKosto;
            IdFushata = idFushata;
            ZbritjeAnalitike = zbritjeAnalitike;
            PershkrimNivelZbritje = pershkrimzbritjeAnalitike;
            ZbritjeTotal = zbritjeTotal;
            IdKonfig = idKonfig;
            IdStatusDok = 1;
            Licenca = licenca;
            Swift = swift;
            EmriBanka = emriBanka;
            _kodBanke = kodemriBanka;
            AdresaBanka = adresaBanka;
            Grupim1KF = kodgrupim1kf;
            Grupim2KF = kodgrupim2kf;
            Grupim3KF = kodgrupim3kf;
            Idgrupim1kf = grupim1kf;
            Idgrupim2kf = grupim2kf;
            Idgrupim3kf = grupim3kf;
            IdObjektivaKosto = idobjektivakosto;
            Objektiva = objektiva;
            NrTVSH = nrtvsh;
            Kupon = kupon;
            LlojPorosie = llojporosie;
            IdNdermarja = idndermarja;
            Viti = vit;
            IdPerdoruesi = idperdoruesi;
            OColBuxhetet = colBuxhetet;
            OColAdresat = colAdresa;
            OColKontaktet = colKontaktet;
            OColVleratFushatShtese = colVleraFushaShtese;
            OColLidhjetAutorizim = collidhjesaut;
            IdKrijuesi = idkrijuesi;
            IdNdermarjeBij = idndermarjebij;
            Fermer = fermer;
            AutoNgarkese = autongarkese;
            ShitjePaTvsh = shitjepatvsh;
            Koordinata = koordinata;
            PerqindjeAgjenti = perqindjeAgjent;
            PerqindjeAgjenti2 = perqindjeAgjent2;
            Prospekt = prospekt;
            KodiMobile = kodiMobile;
            EmailPerPajisje = emailPerPajisje;
            _colMarreveshjet = colMarreveshjet;
            Idklientfurnitorkryesor = idkfkryesor;
            DteDatelindjaKF = dteDatelindjaKF;
            IdPerfaqesuesShitje3 = idPerfaqesuesShitje3;
            PerqindjeAgjenti3 = perqindjeAgjent3;
            _kodPerfaqesuesShitje3 = kodPerfaqesuesShitje3;
            IdTvsh = idtvsh;
            Shenime = shenime;
            EmertimFature = emertimFature;
            LlogaritKomision = llogaritKomision;
            KodIntegrimi = kodIntegrimi;
            KodiISKSH = kodiisksh;
            MeDogane = meDogane;
            HfArkiva = hfArkiva;
            TipiId = tipiId;
            var mesazh = KontrolloKf(shtim, prospekt, monndermarje, kontrolloekzistence, rm, ci);
            if (!mesazh.Status)
                throw new Exception(mesazh.PershkrimMesazhi);
        }

        /// <summary>
        /// Kontruktor i klases
        /// </summary>
        public clsKlientFurnitor()
        {
            ImbLogger.LogTraceShitje("Krijohet nje klase KlientFurnitor.");
        }

        /// <summary>
        /// Krijon objektin duke lexuar the dhenat nga db-ja per idKf-ne perkates
        /// </summary>
        /// <param name="idKf">id-ja e dhene ne input</param>
        public clsKlientFurnitor(int idKf)
        {
            using (var dbKlientFurnitore = new clsDatabaseKontabilitet())
            {
                MbushKlientFurnitor(dbKlientFurnitore.merrKlientFurnitorSipasID(idKf));
            }
        }
        
        public clsKlientFurnitor(int idKf, bool linear)
        {
            using (var dbKlientFurnitore = new clsDatabaseKontabilitet())
            {
                MbushKlientFurnitorLinear(dbKlientFurnitore.merrKlientFurnitorSipasID(idKf));
            }
        }

        public clsKlientFurnitor(int idKf, clsDatabaseKontabilitet dbKlientFurnitore)
        {
            MbushKlientFurnitor(dbKlientFurnitore.TransCache.getKlientFurnitor(idKf, dbKlientFurnitore));
        }

        public clsKlientFurnitor(string kodi, int idndermarje, int idPerdoruesi)
        {
            using (var dbKlientFurnitore = new clsDatabaseKontabilitet())
                MbushKlientFurnitor(dbKlientFurnitore.merrKlientFurnitorSipasKoditDheNdermarjes(kodi, idndermarje, idPerdoruesi));
        }

        public clsKlientFurnitor(string kodi, int idndermarje, clsDatabaseKontabilitet dbKlientFurnitore)
        {
            MbushKlientFurnitor(dbKlientFurnitore.TransCache.getKlientFurnitor(kodi, idndermarje, false, 0, dbKlientFurnitore));
        }

        public clsKlientFurnitor(string kodi, int idndermarje, int idPerdorues, clsDatabaseKontabilitet dbKlientFurnitore)
        {
            MbushKlientFurnitor(dbKlientFurnitore.TransCache.getKlientFurnitor(kodi, idndermarje, true, idPerdorues, dbKlientFurnitore));
        }
        public clsKlientFurnitor(string kodi, int idndermarje)
        {
            using (var dbKf = new clsDatabaseKontabilitet())
                MbushKlientFurnitor(dbKf.ktheKlientFurnitorSipasKodit(kodi, idndermarje));
        }

        public clsKlientFurnitor(DataRow rreshti)
        {
            MbushKlientFurnitor(rreshti);
        }
        public clsKlientFurnitor(DataRow rreshti, bool linear)
        {
            MbushKlientFurnitorLinear(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// autongarkese
        /// </summary>
        public bool AutoNgarkese { get; set; }

        /// <summary>
        /// furnitori eshte fermer
        /// </summary>
        public bool Fermer { get; set; }

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht
        /// </summary>
        public int IdKlientFurnitor { get; set; }

        /// <summary>
        /// kthen vendos id e ndermarjes bij te ciles i perket ky klient
        /// </summary>
        public int IdNdermarjeBij { get; set; }

        /// <summary>
        /// klienti specifik per kliente te caktuar te kastratit qe behet importi per secilin klient vecan
        /// </summary>
        public bool KlientSpecifik { get; set; }

        /// <summary>
        /// Kthen/Vendos kodin e klientit/furnitorit
        /// </summary>
        public string KodKlientFurnitor { get; set; }

        /// <summary>
        /// tregon nese do printohet kupon per fature tatimore
        /// </summary>
        public bool Kupon { get; set; }

        /// <summary>
        /// llojji i porosise karta, aparate , loan ,retention
        /// </summary>
        public int LlojPorosie { get; set; }

        /// <summary>
        /// Kthen/Vendos numrin e llogarise se klientit/furnitorit
        /// </summary>
        public string NrLlogKlientFurnitor { get; set; }

        /// <summary>
        /// Kthen/Vendos id-ne e llogarise se klientit/furnitorit
        /// </summary>
        public int IdLlogari { get; set; }

        /// <summary>
        /// Kthen/Vendos llojin, klient apo furnitor
        /// </summary>
        public bool LlojiKF { get; set; }

        /// <summary>
        /// shitje klientesh pa tvsh
        /// 
        /// </summary>
        public bool ShitjePaTvsh { get; set; }

        /// <summary>
        /// Kthen/Vendos titullin e klientit/furnitorit
        /// </summary>
        public int TitulliKF { get; set; }

        /// <summary>
        /// Kthen/Vendos aktivitetin e klientit/furnitorit
        /// </summary>
        public string AktivitetiKF { get; set; }

        /// <summary>
        /// Kthen/Vendos emertimin e klientit/furnitorit
        /// </summary>
        public string EmertimiKF { get; set; }

        /// <summary>
        /// Kthen/Vendos emrin e kerkimit te klientit/furnitorit
        /// </summary>
        public string EmerKerkimiKF { get; set; }

        /// <summary>
        /// Kthen/Vendos NIPT-in e klientit/furnitorit
        /// </summary>
        public string NiptiKF { get; set; }

        /// <summary>
        /// Kthen/Vendos id e qytetit e klientit/furnitorit
        /// </summary>
        public int QytetiKF { get; set; }

        /// <summary>
        /// Kthen/Vendos emrin e qytetit te klientit/furnitorit
        /// </summary>
        public string EmriQytetitKF { get; set; }

        /// <summary>
        /// Kthen/Vendos shtetin e klientit/furnitorit
        /// </summary>
        public string ShtetiKF { get; set; }

        /// <summary>
        /// Kthen/Vendos numrin e telefonit te klientit/furnitorit
        /// </summary>
        public string TelKF { get; set; }

        /// <summary>
        /// Kthen/Vendos numrin e fax-it te klientit/furnitorit
        /// </summary>
        public string FaxKF { get; set; }

        /// <summary>
        /// Kthen/Vendos numrin e celularit te klientit/furnitorit
        /// </summary>
        public string CelKF { get; set; }

        /// <summary>
        /// Kthen/Vendos adresen e-mail te klientit/furnitorit
        /// </summary>
        public string EmailKF { get; set; }

        /// <summary>
        /// Kthen/Vendos web page-n e klientit/furnitorit
        /// </summary>
        public string WebPageKF { get; set; }

        /// <summary>
        /// Kthen/Vendos IBAN e klientit/furnitorit
        /// </summary>
        public string IBANKF { get; set; }

        /// <summary>
        /// Kthen/Vendos llogarine e klientit/furnitorit
        /// </summary>
        public string LlogariBankareKF { get; set; }

        /// <summary>
        /// Kthen/Vendos nese klienti/furnitori eshte aktiv apo jo
        /// </summary>
        public bool AktivKF { get; set; }

        /// <summary>
        /// Kthen/Vendos llogarine e zbrtjes se klientit/furnitorit
        /// </summary>
        public int IdLlogZbritje { get; set; }

        /// <summary>
        /// Kthen/Vendos numrin e llogarise e zbrtjes se klientit/furnitorit
        /// </summary>
        public string NrLlogZbritje { get; set; }

        /// <summary>
        /// Kthen/Vendos llogarine dytesore te klientit/furnitorit
        /// </summary>
        public int IdLlogariDytesore { get; set; }

        public string NrLlogDytesor { get; set; }

        /// <summary>
        /// Kthen/Vendos kushtin e pageses se klientit/furnitorit
        /// </summary>
        public int IdKushtePagese { get; set; }

        /// <summary>
        /// Kthen/Vendos kodin e kushtit te pageses se klientit/furnitorit
        /// </summary>
        public string KodKushtePagese { get; set; }

        /// <summary>
        /// Kthen/Vendos metoden
        /// </summary>
        public int IdMetoda { get; set; }

        /// <summary>
        /// Kthen/Vendos maturimin e klientit/furnitorit
        /// </summary>
        public int MaturimiKF { get; set; }

        /// <summary>
        /// Kthen/Vendos kategorine e zbritjes se klientit/furnitorit
        /// </summary>
        public int IdKatZbritje { get; set; }

        /// <summary>
        /// Kthen/Vendos kodin e kategorise se zbritjes se klientit/furnitorit
        /// </summary>
        public string KodKatZbritje { get; set; }

        /// <summary>
        /// Kthen/Vendos perqindjen e kategorise se zbritjes se klientit/furnitorit
        /// </summary>
        public decimal PerqindjeKatZbritje { get; set; }

        /// <summary>
        /// Kthen/Vendos limitin paralajmerues te klientit/furnitorit
        /// </summary>
        public int LimitParalajmerues { get; set; }

        /// <summary>
        /// Kthen/Vendos limitin bllokues te klientit/furnitorit
        /// </summary>
        public int LimitBllokues { get; set; }

        /// <summary>
        /// Kthen/Vendos kategorine e klientit/furnitorit
        /// </summary>
        public int IdKategoriKlienti { get; set; }

        /// <summary>
        /// Kthen/Vendos kushtin e dergimit
        /// </summary>
        public string KushteDergimi { get; set; }

        /// <summary>
        /// Kthen/Vendos menyren e transportit
        /// </summary>
        public string MenyraTransportit { get; set; }

        /// <summary>
        /// Kthen/Vendos oferten automatike
        /// </summary>
        public bool OfertaAutomatike { get; set; }

        /// <summary>
        /// Kthen/Vendos vleren limit te porositur
        /// </summary>
        public decimal VleraLimitPorositur { get; set; }

        /// <summary>
        /// Kthen/Vendos prioritetitin
        /// </summary>
        public int Prioriteti { get; set; }

        /// <summary>
        /// Kthen/Vendos cmimin e ulet
        /// </summary>
        public decimal CmimUlet { get; set; }

        /// <summary>
        /// Kthen/Vendos nivelin e cmimit
        /// </summary>
        public int IdNivelCmimi { get; set; }

        /// <summary>
        /// Kthen/Vendos pershkrimin nivelit e cmimit
        /// </summary>
        public string PershkrimNivelCmimi { get; set; }

        /// <summary>
        /// Kthen/Vendos pershkrimin nivelit e zbritje
        /// </summary>
        public string PershkrimNivelZbritje { get; set; }

        /// <summary>
        /// Kthen/Vendos agjentin e shitjes
        /// </summary>
        public int IdPerfaqesuesShitje { get; set; }

        /// <summary>
        /// Kthen/Vendos kodin agjentit e shitjes
        /// </summary>
        public string KodPerfaqesuesShitje { get; set; }

        /// <summary>
        /// Kthen/Vendos agjentin e shitjes
        /// </summary>
        public int IdPerfaqesuesShitje2 { get; set; }

        /// <summary>
        /// Kthen/Vendos agjentin e shitjes
        /// </summary>
        public int IdPerfaqesuesShitje3 { get; set; }

        /// <summary>
        /// Kthen/Vendos kodin agjentit e shitjes
        /// </summary>
        public string KodPerfaqesuesShitje2 { get; set; }

        /// <summary>
        /// Kthen/Vendos qendren e kostos
        /// </summary>
        public int IdQenderKosto { get; set; }

        /// <summary>
        /// Kthen/Vendos fushaten
        /// </summary>
        public int IdFushata { get; set; }

        /// <summary>
        /// Kthen/Vendos zbritjen analitike
        /// </summary>
        public int ZbritjeAnalitike { get; set; }

        /// <summary>
        /// Kthen/Vendos zbritjen totale
        /// </summary>
        public decimal ZbritjeTotal { get; set; }

        /// <summary>
        /// Kthen/Vendos ID-ne e ndermarrjes
        /// </summary>
        public int IdNdermarja { get; set; }

        /// <summary>
        /// Kthen/Vendos vitin
        /// </summary>
        public int Viti { get; set; }

        /// <summary>
        /// Kthen/Vendos ID-ne e perdoruesit
        /// </summary>
        public int IdPerdoruesi { get; set; }

        /// <summary>
        /// Kthen/Vendos ID-ne e konfigurimit
        /// </summary>
        public int IdKonfig { get; set; }

        /// <summary>
        /// Kthen/Vendos monedhen e llogarise
        /// </summary>
        public string Monedha
        {
            get
            {
                return new clsLlogari(IdLlogari).PershkrimiMonedha;
            }
            set
            {
                _monedha = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos monedhen e llogarise
        /// </summary>
        public int idMonedha
        {
            get
            {
                return new clsLlogari(IdLlogari).IdMonedha;
            }
            set
            {
                _idMonedha = value;
            }
        }

        public DateTime DtAzhornimi { get; set; }

        public DateTime DtLidhje { get; set; }

        /// <summary>
        /// Kthen/Vendos monedhen e llogarise  se zbritjes
        /// </summary>
        public string MonedhaZbritje
        {
            get
            {
                return new clsLlogari(IdLlogZbritje).PershkrimiMonedha;
            }
            set
            {
                _monedhaZbritje = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos datelindjen e klientit/furnitorit
        /// </summary>
        public DateTime DteDatelindjaKF { get; set; }

        /// <summary>
        /// Kthen/Vendos nje collection me objekte te tipit <see cref="DbCore.DbKontabiliteti.clsAdresaKlientFurnitor"/>
        /// </summary>
        public colAdresatKlientFurnitor OColAdresat { get; set; }

        ///<summary>
        /// Kthen/Vendos idtvsh
        /// </summary>
        public int IdTvsh { get; set; }

        /// <summary>
        /// Kthen/Vendos nje collection me objekte te tipit <see cref="DbCore.DbKontabiliteti.clsKontaktiKlientFurnitor"/>
        /// </summary>
        public colKontaktiKlientFurnitor OColKontaktet { get; set; }

        /// <summary>
        /// Kthen/Vendos nje collection me objekte te tipit <see cref="DbCore.DbKontabiliteti.clsBuxheti"/>
        /// </summary>
        public colBuxhetet OColBuxhetet { get; set; }

        /// <summary>
        /// Kthen/Vendos nje collection me objekte te tipit <see cref="DbAdmin.clsVleraFushaShtese"/>
        /// </summary>
        public colVleraFushaShtese OColVleratFushatShtese { get; set; }

        /// <summary>
        /// Kthen/Vendos nje collection me objekte te tipit <see cref="DbCore.DbKontabiliteti.clsLidhjeAutorizim"/>
        /// </summary>
        public colLidhjetAutorizim OColLidhjetAutorizim { get; set; }

        public int Idgrupim1kf { get; set; }

        public int Idgrupim2kf { get; set; }

        public int Idgrupim3kf { get; set; }

        public int IdStatusDok { get; set; }

        public DateTime DtKrijimi { get; private set; }

        public DateTime DtModifikimi { get; private set; }

        /// <summary>
        /// Kthen/Vendos licencen
        /// </summary>
        public string Licenca { get; set; }

        /// <summary>
        /// Kthen/Vendos swift
        /// </summary>
        public string Swift { get; set; }

        /// <summary>
        /// Kthen/Vendos kodin e bankes
        /// </summary>
        public int EmriBanka { get; set; }

        /// <summary>
        /// Kthen/Vendos adresen e bankes
        /// </summary>
        public string AdresaBanka { get; set; }

        public string Grupim1KF { get; set; }

        public string Grupim2KF { get; set; }

        public string Grupim3KF { get; set; }

        public string NrTVSH { get; set; }

        public decimal Gjendja { get; set; }

        /// <summary>
        /// id e objektives se kostos
        /// </summary>
        public int IdObjektivaKosto { get; set; }

        /// <summary>
        /// objektiva kosto
        /// </summary>
        public string Objektiva { get; private set; }

        /// <summary>
        /// id e perdoruesit qe ka krijuar dokumentin
        /// </summary>
        public int IdKrijuesi { get; set; }

        /// <summary>
        /// username i perdoruesit qe ka krijuar dokumentin
        /// </summary>
        public string Krijuesi { get; private set; }

        public decimal GjendjaMonBaze { get; set; }

        /// <summary>
        /// Kthen/Vendos koordinaten e klientit/furnitorit
        /// </summary>
        public string Koordinata { get; set; }

        /// <summary>
        /// Kthen/Vendos perqindjen e agjentit te shitjes.
        /// </summary>
        public decimal PerqindjeAgjenti { get; set; }

        /// <summary>
        /// Kthen/Vendos perqindjen e agjentit te shitjes.
        /// </summary>
        public decimal PerqindjeAgjenti2 { get; set; }

        /// <summary>
        /// Kthen/Vendos perqindjen e agjentit te shitjes.
        /// </summary>
        public decimal PerqindjeAgjenti3 { get; set; }


        public bool Prospekt { get; set; }

        public string KodiMobile { get; set; }

        /// <summary>
        /// Kthen/Vendos adresen e-mailit ku do dergohet fjalekalimi per pajisjen e tollonit dhe portokalleve (gulf)
        /// </summary>
        public string EmailPerPajisje { get; set; }

        public IDictionary<string, object> HfArkiva { get; set; }

        public int Idklientfurnitorkryesor { get; set; }

        public string Shenime { get; set; }

        public string EmertimFature { get; set; }

        public bool LlogaritKomision { get; set; }

        public string KodIntegrimi { get; set; }

        public string KodiISKSH { get; set; }

        public bool MeDogane { get; set; }
        public string TipiId { get; set; }


        #endregion

        #region Metoda Publike

        public static DataTable MbushKlienteOseFurnitoreMeId(int idKf, int idNdermarrje, int idPerdoruesi)
        {
            using (var dbKlientFurnitore = new clsDatabaseKontabilitet())
                return dbKlientFurnitore.merrKlientFurnitorSipasID(idKf, idNdermarrje, idPerdoruesi);
        }

        public static DataTable MbushKlienteOseFurnitoreMeId(int idKf)
        {
            using (var dbKlientFurnitore = new clsDatabaseKontabilitet())
                return dbKlientFurnitore.merrKlientFurnitorSipasIDPaAutorizim(idKf);
        }

        public static DataTable ktheDTKlientFurnitorSipasIdAzhornim(string kodKF, int idNdermarrja, DateTime dtdok)
        {
            using (var dbKlientFurnitore = new clsDatabaseKontabilitet())
                return dbKlientFurnitore.ktheDisaKlientFurnitorSipasIDAzhornim(kodKF, idNdermarrja, dtdok);
        }

        public clsKlientFurnitor KrijoKlientFurnitorPerImport(clsKlientFurnitor kfEkzistues, string kodKliFurn, string nrLlog, bool? llojiKf, string pershkrimtitulliKf, string aktivitetiKf, string emertimiKf, string emerKerkimiKf, string niptKf, string pershkrimqytetiKf, string shtetiKf, string telKf, string faxKf, string celKf, string emailKf, string webpageKf, string ibanKf, string llogariBankareKf, bool? aktivKf, string nrLlogZbritje, string nrLlogariDytesore, string kodKushtePagese, string pershkrimMetoda, string kodmaturimiKf, string kodKatZbritje, int? limitParalajmerues, int? limitBllokues, int idKategoriKlienti, string kodkushteDergimi, string kodmenyraTransportit, bool ofertaAutomatike, decimal vleraLimitPorositur, int prioriteti, decimal cmimUlet, string pershkrimNivelCmimi, string kodPerfaqesuesShitje, int idQenderKosto, int idFushata, string pershkrimzbritjeAnalitike, decimal zbritjeTotal, int idndermarja, int vit, int idperdoruesi, int? idKonfig, string licenca, string swift, string kodemriBanka, string adresaBanka, string kodgrupim1Kf, string kodgrupim2Kf, string kodgrupim3Kf, string nrtvsh, colAdresatKlientFurnitor colAdresa, colKontaktiKlientFurnitor colKontaktet, colBuxhetet colBuxhetet, colVleraFushaShtese colVleraFushaShtese, bool shtim, int monndermarje, bool kontrolloekzistence, string objektiva, int idkrijuesi, int idndermarjebij, int llojporosie, bool? kupon, ResourceManager rm, CultureInfo ci, string kodPerfaqesuesShitje2, decimal perqindjeAgjenti, decimal perqindjeAgjenti2, bool? prospekt, string kodiMobile, bool? fermer, bool? shitjePaTvsh, bool? autongarkese, string autorizime, string emailPerPajisje, string kodklientfurnitorkryesor, string shenime, string kodPerfaqesuesShitje3, string emertimFature, bool? llogaritKomision, string kodIntegrimi, string kodISKSH, string tipiId)
        {
            try
            {
                kfEkzistues.EmptyObject();

                clsDatabaseRegjistrim dbRegjistrim = new clsDatabaseRegjistrim();
                IdKonfig = idKonfig == null ? kfEkzistues.IdKonfig : (int)idKonfig;
                LlojiKF = llojiKf == null ? kfEkzistues.LlojiKF : (bool)llojiKf;

                bool kaVeprimeKf = kfEkzistues.IdKlientFurnitor > 0 ? dbRegjistrim.eshteDokumentiILidhurCelje(kfEkzistues.IdKlientFurnitor.ToString(), clsKonfigurimAmbjenti.ktheIdNiveliSipasIdKonfigurimi(IdKonfig).ToString()) : false;

                Prospekt = prospekt == null ? kfEkzistues.Prospekt : (bool)prospekt;
                NrLlogKlientFurnitor = string.IsNullOrEmpty(nrLlog) ? kfEkzistues.NrLlogKlientFurnitor : nrLlog;

                if (!Prospekt && NrLlogKlientFurnitor == "")
                    throw new Exception("Plotesoni llogarine e klientit!");
                IdLlogari = string.IsNullOrEmpty(nrLlog) ? kfEkzistues.IdLlogari : clsLlogari.mbushIDLlogariSipasKodit(nrLlog, idndermarja);
                if (IdLlogari != kfEkzistues.IdLlogari && kaVeprimeKf)
                    throw new Exception("Llogaria e klientit/furnitorit nuk mund te ndryshohet pasi ka veprime me klient/furnitorin.");
                _pershkrimTitulliKf = string.IsNullOrEmpty(pershkrimtitulliKf) ? kfEkzistues._pershkrimTitulliKf ?? string.Empty : pershkrimtitulliKf; 

                switch (_pershkrimTitulliKf.ToLower())
                {
                    case "kompani":
                        TitulliKF = (int)TitulliKlientFurnitor.Kompani;
                        break;
                    case "person fizik":
                        TitulliKF = (int)TitulliKlientFurnitor.PersonFizik;
                        break;
                    case "klient i rastesishem":
                        TitulliKF = (int)TitulliKlientFurnitor.KlientRastesishem;
                        break;
                    case "institucion buxhetor":
                        TitulliKF = (int)TitulliKlientFurnitor.InstitucionBuxhetor;
                        break;
                    case "shpk":
                        TitulliKF = (int)TitulliKlientFurnitor.SHPK;
                        break;
                    default:
                        TitulliKF = (int)TitulliKlientFurnitor.Other;
                        break;
                }

                QytetiKF = string.IsNullOrEmpty(pershkrimqytetiKf) ? kfEkzistues.QytetiKF : new clsQyteti(pershkrimqytetiKf, idndermarja).IdQyteti;
                IdLlogZbritje = string.IsNullOrEmpty(nrLlogZbritje) ? kfEkzistues.IdLlogZbritje : clsLlogari.mbushIDLlogariSipasKodit(nrLlogZbritje, idndermarja);
                if (IdLlogZbritje != kfEkzistues.IdLlogZbritje && kaVeprimeKf)
                    throw new Exception("Llogaria zbritjes e klientit/furnitorit nuk mund te ndryshohet pasi ka veprime me klient/furnitorin.");

                IdLlogariDytesore = string.IsNullOrEmpty(nrLlogariDytesore) ? kfEkzistues.IdLlogariDytesore : clsLlogari.mbushIDLlogariSipasKodit(nrLlogariDytesore, idndermarja);
                if (IdLlogariDytesore != kfEkzistues.IdLlogariDytesore && kaVeprimeKf)
                    throw new Exception("Llogaria dytesore e klientit/furnitorit nuk mund te ndryshohet pasi ka veprime me klient/furnitorin.");

                IdKushtePagese = string.IsNullOrEmpty(kodKushtePagese) ? kfEkzistues.IdKushtePagese : new clsKushtPageseKoka(kodKushtePagese, idndermarja).IdKoka;
                _pershkrimMetoda = string.IsNullOrEmpty(pershkrimMetoda) ? kfEkzistues._pershkrimMetoda?? string.Empty : pershkrimMetoda;
                switch (_pershkrimMetoda.ToLower())
                {
                    case "me mirebesim":
                        IdMetoda = (int)MenyrePagese.Me_mirebesim;
                        break;
                    case "pagese":
                        IdMetoda = (int)MenyrePagese.Pagese;
                        break;
                    case "pagese automatike":
                        IdMetoda = (int)MenyrePagese.Pagese_Automatike;
                        break;
                    case "cash & bank":
                        IdMetoda = (int)MenyrePagese.Cash_1_Bank;
                        break;
                    case "me parapagim":
                        IdMetoda = (int)MenyrePagese.Me_parapagim;
                        break;
                    case "pezull":
                        IdMetoda = (int)MenyrePagese.Pezull;
                        break;
                    case "arke":
                        IdMetoda = (int)MenyrePagese.Arke;
                        break;
                    case "karte krediti":
                        IdMetoda = (int)MenyrePagese.Karte_krediti;
                        break;
                    case "banke":
                        IdMetoda = (int)MenyrePagese.Banke;
                        break;

                    default:
                        IdMetoda = -1;
                        break;
                }

                MaturimiKF = string.IsNullOrEmpty(kodmaturimiKf) ? kfEkzistues.MaturimiKF : clsMaturimi.KtheIdMaturimi(kodmaturimiKf, idndermarja);
                IdKatZbritje = string.IsNullOrEmpty(kodKatZbritje) ? kfEkzistues.IdKatZbritje : clsKokaKategoriZbritje.ktheIdKokaKategoriZbritje(kodKatZbritje, idndermarja);
                KushteDergimi = string.IsNullOrEmpty(kodkushteDergimi) ? kfEkzistues.KushteDergimi : new clsKushtDergimi(kodkushteDergimi, idndermarja).IdKushtDergimi.ToString();
                MenyraTransportit = string.IsNullOrEmpty(kodmenyraTransportit) ? kfEkzistues.MenyraTransportit : new clsMenyreTransporti(kodmenyraTransportit, idndermarja).IdMenyreTransporti.ToString();
                if (KushteDergimi == "0")
                    KushteDergimi = "";
                if (MenyraTransportit == "0")
                    MenyraTransportit = "";
                IdNivelCmimi = string.IsNullOrEmpty(pershkrimNivelCmimi) ? kfEkzistues.IdNivelCmimi : clsNivelCmimi.ktheIdNivelCmimiNgaPershkrimi(pershkrimNivelCmimi, idndermarja);
                IdPerfaqesuesShitje = string.IsNullOrEmpty(kodPerfaqesuesShitje) ? kfEkzistues.IdPerfaqesuesShitje : new clsAgjentShitje(kodPerfaqesuesShitje, idndermarja).IdAgjentShitje;
                IdPerfaqesuesShitje2 = string.IsNullOrEmpty(kodPerfaqesuesShitje2) ? kfEkzistues.IdPerfaqesuesShitje2 : new clsAgjentShitje(kodPerfaqesuesShitje2, idndermarja).IdAgjentShitje;
                IdPerfaqesuesShitje3 = string.IsNullOrEmpty(kodPerfaqesuesShitje3) ? kfEkzistues.IdPerfaqesuesShitje3 : new clsAgjentShitje(kodPerfaqesuesShitje3, idndermarja).IdAgjentShitje;
                ZbritjeAnalitike = string.IsNullOrEmpty(pershkrimzbritjeAnalitike) ? kfEkzistues.ZbritjeAnalitike : clsNivelZbritje.ktheIdNivelZbritjeSipasPershkrimit(pershkrimzbritjeAnalitike, idndermarja);

                if (string.IsNullOrEmpty(kodemriBanka))
                    EmriBanka = kfEkzistues.EmriBanka;
                else
                {
                    var banka = new clsBanka();
                    banka.mbushBankeSipasKodit(kodemriBanka, idndermarja);
                    EmriBanka = banka.IdBanka;
                }

                Idgrupim1kf = string.IsNullOrEmpty(kodgrupim1Kf) ? kfEkzistues.Idgrupim1kf : new clsGrupeKF(kodgrupim1Kf, idndermarja, 1, LlojiKF ? 0 : 1).IdGrupi;
                Idgrupim2kf = string.IsNullOrEmpty(kodgrupim2Kf) ? kfEkzistues.Idgrupim2kf : new clsGrupeKF(kodgrupim2Kf, idndermarja, 2, LlojiKF ? 0 : 1).IdGrupi;
                Idgrupim3kf = string.IsNullOrEmpty(kodgrupim3Kf) ? kfEkzistues.Idgrupim3kf : new clsGrupeKF(kodgrupim3Kf, idndermarja, 3, LlojiKF ? 0 : 1).IdGrupi;
                IdObjektivaKosto = string.IsNullOrEmpty(objektiva) ? kfEkzistues.IdObjektivaKosto : new clsObjektivaKosto(objektiva, idndermarja).Id;

                colLidhjetAutorizim colLidhje = kfEkzistues.IdKlientFurnitor > 0 ? new colLidhjetAutorizim(kfEkzistues.IdKlientFurnitor, "KlientFurnitor") : new colLidhjetAutorizim();
                if (!string.IsNullOrEmpty(autorizime))
                {
                    var colLidhjet = new colLidhjetAutorizim();
                    var autorizimet = autorizime.Split(',');

                    foreach (var autorizim in autorizimet)
                    {
                        if (!clsAutorizimKoka.kaAutorizimSipasPerdoruesit(autorizim, idkrijuesi))
                            throw new MyException("Ju nuk keni te drejta te ky autorizim!");

                        colLidhjet.Add(new clsLidhjeAutorizim
                        {
                            IdAutorizimeKoka = clsAutorizimKoka.ktheIDAutorizim(autorizim)
                        });
                    }

                    colLidhje = colLidhjet;
                }

                Idklientfurnitorkryesor = string.IsNullOrEmpty(kodklientfurnitorkryesor) ? kfEkzistues.Idklientfurnitorkryesor : 0;
                if (!string.IsNullOrEmpty(kodklientfurnitorkryesor))
                {
                    Idklientfurnitorkryesor = MerrIdKlientFurnitor(kodklientfurnitorkryesor, idndermarja);
                    if (Idklientfurnitorkryesor <= 0)
                        throw new MyException("Klient/Furnitori kryesor nuk ekziston!");
                }

                EmertimiKF = string.IsNullOrEmpty(emertimiKf) ? kfEkzistues.EmertimiKF : emertimiKf;
                EmerKerkimiKF = string.IsNullOrEmpty(emerKerkimiKf) ? kfEkzistues.EmerKerkimiKF : emerKerkimiKf;
                NiptiKF = string.IsNullOrEmpty(niptKf) ? kfEkzistues.NiptiKF : niptKf;
                EmriQytetitKF = string.IsNullOrEmpty(pershkrimqytetiKf) ? kfEkzistues.EmriQytetitKF : pershkrimqytetiKf;
                ShtetiKF = string.IsNullOrEmpty(shtetiKf) ? kfEkzistues.ShtetiKF : shtetiKf;
                TelKF = string.IsNullOrEmpty(telKf) ? kfEkzistues.TelKF : telKf;
                FaxKF = string.IsNullOrEmpty(faxKf) ? kfEkzistues.FaxKF : faxKf;
                CelKF = string.IsNullOrEmpty(celKf) ? kfEkzistues.CelKF : celKf;
                EmailKF = string.IsNullOrEmpty(emailKf) ? kfEkzistues.EmailKF : emailKf;
                WebPageKF = string.IsNullOrEmpty(webpageKf) ? kfEkzistues.WebPageKF : webpageKf;
                LlogariBankareKF = string.IsNullOrEmpty(llogariBankareKf) ? kfEkzistues.LlogariBankareKF : llogariBankareKf;
                AktivKF = aktivKf == null ? kfEkzistues.AktivKF : (bool)aktivKf;
                AktivitetiKF = string.IsNullOrEmpty(aktivitetiKf) ? kfEkzistues.AktivitetiKF : aktivitetiKf;
                _kodMaturimiKf = string.IsNullOrEmpty(kodmaturimiKf) ? kfEkzistues._kodMaturimiKf ?? string.Empty : kodmaturimiKf;
                KodKatZbritje = string.IsNullOrEmpty(kodKatZbritje) ? kfEkzistues.KodKatZbritje : kodKatZbritje;
                LimitParalajmerues = limitParalajmerues == null ? kfEkzistues.LimitParalajmerues : (int)limitParalajmerues;
                LimitBllokues = limitBllokues == null ? kfEkzistues.LimitBllokues : (int)limitBllokues;
                PershkrimNivelCmimi = string.IsNullOrEmpty(pershkrimNivelCmimi) ? kfEkzistues.PershkrimNivelCmimi : pershkrimNivelCmimi;
                KodPerfaqesuesShitje = string.IsNullOrEmpty(kodPerfaqesuesShitje) ? kfEkzistues.KodPerfaqesuesShitje : kodPerfaqesuesShitje;
                PershkrimNivelZbritje = string.IsNullOrEmpty(pershkrimzbritjeAnalitike) ? kfEkzistues.PershkrimNivelZbritje : pershkrimzbritjeAnalitike;
                Licenca = string.IsNullOrEmpty(licenca) ? kfEkzistues.Licenca : licenca;
                Swift = string.IsNullOrEmpty(swift) ? kfEkzistues.Swift : swift;
                _kodBanke = string.IsNullOrEmpty(kodemriBanka) ? kfEkzistues._kodBanke ?? string.Empty : kodemriBanka;
                Grupim1KF = string.IsNullOrEmpty(kodgrupim1Kf) ? kfEkzistues.Grupim1KF : kodgrupim1Kf;
                Grupim2KF = string.IsNullOrEmpty(kodgrupim2Kf) ? kfEkzistues.Grupim2KF : kodgrupim2Kf;
                Grupim3KF = string.IsNullOrEmpty(kodgrupim3Kf) ? kfEkzistues.Grupim3KF : kodgrupim3Kf;
                NrTVSH = string.IsNullOrEmpty(nrtvsh) ? kfEkzistues.NrTVSH : nrtvsh;
                Objektiva = string.IsNullOrEmpty(objektiva) ? kfEkzistues.Objektiva : objektiva;
                Kupon = kupon == null ? kfEkzistues.Kupon : (bool)kupon;
                KodPerfaqesuesShitje2 = string.IsNullOrEmpty(kodPerfaqesuesShitje2) ? kfEkzistues.KodPerfaqesuesShitje2 : kodPerfaqesuesShitje2;
                Fermer = fermer == null ? kfEkzistues.Fermer : (bool)fermer;
                AutoNgarkese = autongarkese == null ? kfEkzistues.AutoNgarkese : (bool)autongarkese;
                ShitjePaTvsh = shitjePaTvsh == null ? kfEkzistues.ShitjePaTvsh : (bool)shitjePaTvsh;
                Shenime = string.IsNullOrEmpty(shenime) ? kfEkzistues.Shenime : shenime;
                _kodPerfaqesuesShitje3 = string.IsNullOrEmpty(kodPerfaqesuesShitje3) ? kfEkzistues._kodPerfaqesuesShitje3 ?? string.Empty : kodPerfaqesuesShitje3;
                EmertimFature = string.IsNullOrEmpty(emertimFature) ? kfEkzistues.EmertimFature : emertimFature;
                KodIntegrimi = string.IsNullOrEmpty(kodIntegrimi) ? kfEkzistues.KodIntegrimi : kodIntegrimi;
                IBANKF = string.IsNullOrEmpty(ibanKf) ? kfEkzistues.IBANKF : ibanKf;
                LlogaritKomision = llogaritKomision == null ? kfEkzistues.LlogaritKomision : (bool)llogaritKomision;
                NrLlogZbritje = string.IsNullOrEmpty(nrLlogZbritje) ? kfEkzistues.NrLlogZbritje : nrLlogZbritje;
                NrLlogDytesor = string.IsNullOrEmpty(nrLlogariDytesore) ? kfEkzistues.NrLlogDytesor : nrLlogariDytesore;
                colMarreveshjetPerKlient marreveshjetPerKlient = new colMarreveshjetPerKlient();
                if (kfEkzistues.IdKlientFurnitor > 0)
                    marreveshjetPerKlient.MbushVetemMarreveshjetEKlientit(kfEkzistues.IdKlientFurnitor);
                IDictionary<string, object> hfArkiva = new Dictionary<string, object>();
                Koordinata = kfEkzistues.IdKlientFurnitor > 0 ? kfEkzistues.Koordinata : string.Empty;
                KodiISKSH = kfEkzistues.Idklientfurnitorkryesor > 0 ? kfEkzistues.KodiISKSH : kodISKSH;
                MeDogane = kfEkzistues.MeDogane;
                KlientSpecifik = kfEkzistues.Idklientfurnitorkryesor > 0 ? kfEkzistues.KlientSpecifik : false;
                TipiId = string.IsNullOrEmpty(tipiId) ? kfEkzistues.TipiId : tipiId;

                return new clsKlientFurnitor(kodKliFurn, IdLlogari, NrLlogKlientFurnitor, LlojiKF, TitulliKF, _pershkrimTitulliKf, AktivitetiKF, EmertimiKF, EmerKerkimiKF, NiptiKF, QytetiKF, EmriQytetitKF, ShtetiKF, TelKF, FaxKF, CelKF, EmailKF, WebPageKF, IBANKF, LlogariBankareKF, AktivKF, IdLlogZbritje, NrLlogZbritje, IdLlogariDytesore, NrLlogDytesor, IdKushtePagese, kodKushtePagese, IdMetoda, _pershkrimMetoda, MaturimiKF, kodmaturimiKf, IdKatZbritje, KodKatZbritje, LimitParalajmerues, LimitBllokues, idKategoriKlienti, KushteDergimi, kodkushteDergimi, MenyraTransportit, kodmenyraTransportit, ofertaAutomatike, vleraLimitPorositur, prioriteti, cmimUlet, PershkrimNivelCmimi, IdPerfaqesuesShitje, KodPerfaqesuesShitje, idQenderKosto, idFushata, ZbritjeAnalitike, PershkrimNivelZbritje, zbritjeTotal, idndermarja, vit, idperdoruesi, IdKonfig, Licenca, Swift, EmriBanka, _kodBanke, adresaBanka, Idgrupim1kf, Idgrupim2kf, Idgrupim3kf, Grupim1KF, Grupim2KF, Grupim3KF, NrTVSH, colAdresa, colKontaktet, colBuxhetet, colVleraFushaShtese, colLidhje, shtim, monndermarje, kontrolloekzistence, IdObjektivaKosto, Objektiva, idkrijuesi, idndermarjebij, llojporosie, Kupon, hfArkiva, rm, ci, Koordinata, IdPerfaqesuesShitje2, KodPerfaqesuesShitje2, KlientSpecifik, Fermer, AutoNgarkese, ShitjePaTvsh, perqindjeAgjenti, perqindjeAgjenti2, Prospekt, kodiMobile, emailPerPajisje, marreveshjetPerKlient, Idklientfurnitorkryesor, Shenime, DteDatelindjaKF, IdPerfaqesuesShitje3, PerqindjeAgjenti3, _kodPerfaqesuesShitje3, IdTvsh, EmertimFature, LlogaritKomision, KodIntegrimi, KodiISKSH, MeDogane, TipiId);
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Ekzekuton nje transaksion per te ruajtur nje klient dhe gjithe te dhenat lidhur me te
        /// Therret funksionin <see cref="DbCore.DbKontabiliteti.clsDatabaseKontabilitet.ruajKF"/>
        /// Therret funksionin <see cref="DbCore.DbKontabiliteti.clsDatabaseKontabilitet.ruajBuxhet"/>
        /// Therret funksionin <see cref="DbCore.DbKontabiliteti.clsDatabaseKontabilitet.ruajKontakt"/>
        /// Therret funksionin <see cref="DbAdmin.clsDatabaseAdmin.ruajVlera"/>       
        /// <param name="klientFurnitor">Objekt i tipit clsKlientFurnitor qe do te ruhet ne DB</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te transaksionit (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        public clsMesazh ruajKlientFurnitor(out int idKlientFurnitor, string kodKliFurn, int idLlog, bool llojiKF, int titulliKF, string aktivitetiKF, string emertimiKF, string emerKerkimiKF, string niptKF, int qytetiKF, string shtetiKF, string telKF, string faxKF, string celKF, string emailKF, string webpageKF, string ibanKF, string llogariBankareKF, bool aktivKF, int idLlogZbritje, int idLlogariDytesore, int idKushtePagese, int idMetoda, int maturimiKF, int idKatZbritje, int limitParalajmerues, int limitBllokues, int idKategoriKlienti, string kushteDergimi, string menyraTransportit, bool ofertaAutomatike, decimal vleraLimitPorositur, int prioriteti, decimal cmimUlet, int idNivelCmimi, int idPerfaqesuesShitje, int idQenderKosto, int idFushata, int zbritjeAnalitike, decimal zbritjeTotal, int idndermarja, int vit, int idperdoruesi, int idKonfig, colAdresatKlientFurnitor oColAdresat, colKontaktiKlientFurnitor oColKontaktet, colBuxhetet oColBuxhete, colVleraFushaShtese oColVleratFushatShtese, colLidhjetAutorizim oColLidhjetAutorizim, int idstatusdok, string licenca, string swift, int emriBanka, string adresaBanka, clsDatabaseKontabilitet dbKont, int grupimi1kf, int grupimi2kf, int grupimi3kf, string nrtvsh, int idobjektivakosto, int idkrijuesi, int idndermarjebij, int llojporosie, bool kupon, colArkiva oColArkiva, string koordinata, int idPerfaqesuesShitje2, bool klientspecifik, bool fermer, bool autongarkese, bool shitjepatvsh, decimal perqindjeagjenti, decimal perqindjeagjenti2, bool prospekt, string emailPerPajisje, colMarreveshjetPerKlient marreveshjet, string shenime, int idkfkryesor, DateTime dteDatelindjaKF, int idPerfaqesuesShitje3, decimal perqindjeagjenti3, int idtvsh, string emertimFature, bool llogaritKomision, string kodIntegrimi, string kodiisksh, bool meDogane,string tipiId ,string kodiMobile = null)
        {
            //ruan klient furnitorin bashke me adresat, kontaktet, buxhetin dhe vlerat e fushave shtese     
            idKlientFurnitor = -1;
            try
            {
                idKlientFurnitor = dbKont.ruajKF(idKlientFurnitor, kodKliFurn, idLlog, llojiKF, titulliKF, aktivitetiKF, emertimiKF, emerKerkimiKF, niptKF, qytetiKF, shtetiKF, telKF, faxKF, celKF, emailKF, webpageKF, ibanKF, llogariBankareKF, aktivKF, idLlogZbritje, idLlogariDytesore, idKushtePagese, idMetoda, maturimiKF, idKatZbritje, limitParalajmerues, limitBllokues, idKategoriKlienti, kushteDergimi, menyraTransportit, ofertaAutomatike, vleraLimitPorositur, prioriteti, cmimUlet, idNivelCmimi, idPerfaqesuesShitje, idQenderKosto, idFushata, zbritjeAnalitike, zbritjeTotal, idndermarja, vit, idperdoruesi, idKonfig, idstatusdok, licenca, swift, emriBanka, adresaBanka, grupimi1kf, grupimi2kf, grupimi3kf, nrtvsh, idobjektivakosto, idkrijuesi, idndermarjebij, llojporosie, kupon, koordinata, idPerfaqesuesShitje2, klientspecifik, fermer, autongarkese, shitjepatvsh, perqindjeagjenti, perqindjeagjenti2, prospekt, emailPerPajisje, shenime, idkfkryesor, dteDatelindjaKF, idPerfaqesuesShitje3, perqindjeagjenti3, idtvsh, emertimFature, llogaritKomision, kodIntegrimi, kodiisksh, meDogane, tipiId, clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizim());
                IdKlientFurnitor = idKlientFurnitor;
                if (idKlientFurnitor == 0)
                    return new clsMesazh(false, "Ndodhi nje gabim gjate ruajtes se KF!");

                clsMesazh mesazh;

                foreach (var o in oColBuxhete)
                {
                    o.IdLlojBuxheti = clsLlojBuxheti.mbushIDLlojBuxheti("KlientFurnitor", dbKont);
                    o.IdLidhese = idKlientFurnitor;
                    int idB;
                    mesazh = dbKont.ruajBuxhet(out idB, o.IdLlojBuxheti, o.IdLidhese, o.Muaj, o.Buxheti_1, o.Buxheti_2, o.DtAktivizimi, o.IdKonfigUrdherPagese);
                    if (!mesazh.Status)
                        return mesazh;
                }
                var dbAdmin = new clsDatabaseAdmin(dbKont);
                foreach (var o in oColLidhjetAutorizim)
                {
                    o.IdLloji = clsLlojBuxheti.mbushIDLlojBuxheti("KlientFurnitor", dbKont);
                    o.IdLidhese = idKlientFurnitor;
                    mesazh = dbAdmin.ruajLidhjeAutorizim(o.IdLidhjeAutorizim, o.IdLidhese, o.IdLloji, o.IdAutorizimeKoka, 1);
                    if (!mesazh.Status)
                        return mesazh;
                }
                foreach (var o in oColAdresat)
                {
                    if (!string.IsNullOrEmpty(o.Adresa) || !string.IsNullOrEmpty(o.KodiPostar))
                    {
                        o.IdKlientFurnitor = idKlientFurnitor;
                        int idA;
                        mesazh = dbKont.ruajAdrese(out idA, o.IdKlientFurnitor, o.IdTipAdrese, o.Adresa, o.KodiPostar);
                        if (!mesazh.Status)
                            return mesazh;
                    }
                }
                foreach (var o in oColKontaktet)
                {
                    o.IdKlientFurnitor = idKlientFurnitor;
                    int idK;
                    mesazh = dbKont.ruajKontakt(out idK, o.IdKlientFurnitor, o.EmerKontakti, o.MbiemerKontakti, o.TelKontakti, o.FaxKontakti, o.CelKontakti, o.EmailKontakti);
                    if (!mesazh.Status)
                        return mesazh;
                }

                if (oColVleratFushatShtese != null && oColVleratFushatShtese.Count > 0)
                {
                    oColVleratFushatShtese.ForEach(x => x.IdLidhese = IdKlientFurnitor);
                    mesazh = oColVleratFushatShtese.Ruaj();
                }

                mesazh = colMarreveshjetPerKlient.FshiMarreveshjetEKlientit(idKlientFurnitor, dbKont);
                if (!mesazh.Status)
                    return mesazh;

                foreach (var m in marreveshjet)
                {
                    m.IdKlient = idKlientFurnitor;
                    mesazh = m.RuajMarreveshjePerKlient(dbKont);
                    if (!mesazh.Status)
                        return mesazh;
                }

                return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            }
            catch (Exception ce)
            {
                return new clsMesazh(false, ce.Message);
            }
        }

        /// <summary>
        /// Ruan objektin klient/furnitor ne tabelen perkatese ne databaze.Therret funksionin
        /// <see cref="DbCore.DbKontabiliteti.clsKlientFurnitor.ruajKlientFurnitor"/>
        /// </summary>
        /// <returns>Kthen true nese ruajtja perfundoi me sukses</returns>
        public clsMesazh Ruaj(IDictionary<string, object> hfNrAutoKf, bool ngaImport, string idTemp, string emerTabKoka, string primaryKey, string emerFusheNdermarrje)
        {
            int idkf;
            bool kaNdryshimNumri;
            using (var scope = new MyTransactionScope())
            {
                var db = new clsDatabaseKontabilitet();
                var mesazhKontrolli = KontrolloKf(out kaNdryshimNumri, db, hfNrAutoKf, false);
                if (!mesazhKontrolli.Status)
                {
                    return mesazhKontrolli;
                }

                var mesazh = ruajKlientFurnitor(out idkf, KodKlientFurnitor, IdLlogari, LlojiKF, TitulliKF, AktivitetiKF, EmertimiKF, EmerKerkimiKF, NiptiKF, QytetiKF, ShtetiKF, TelKF, FaxKF, CelKF, EmailKF, WebPageKF, IBANKF, LlogariBankareKF, AktivKF, IdLlogZbritje, IdLlogariDytesore, IdKushtePagese, IdMetoda, MaturimiKF, IdKatZbritje, LimitParalajmerues, LimitBllokues, IdKategoriKlienti, KushteDergimi, MenyraTransportit, OfertaAutomatike, VleraLimitPorositur, Prioriteti, CmimUlet, IdNivelCmimi, IdPerfaqesuesShitje, IdQenderKosto, IdFushata, ZbritjeAnalitike, ZbritjeTotal, IdNdermarja, Viti, IdPerdoruesi, IdKonfig, OColAdresat, OColKontaktet, OColBuxhetet, OColVleratFushatShtese, OColLidhjetAutorizim, IdStatusDok, Licenca, Swift, EmriBanka, AdresaBanka, db, Idgrupim1kf, Idgrupim2kf, Idgrupim3kf, NrTVSH, IdObjektivaKosto, IdKrijuesi, IdNdermarjeBij, LlojPorosie, Kupon, _oArkiva, Koordinata, IdPerfaqesuesShitje2, KlientSpecifik, Fermer, AutoNgarkese, ShitjePaTvsh, PerqindjeAgjenti, PerqindjeAgjenti2, Prospekt, EmailPerPajisje, _colMarreveshjet, Shenime, Idklientfurnitorkryesor, DteDatelindjaKF, IdPerfaqesuesShitje3, PerqindjeAgjenti3, IdTvsh, EmertimFature, LlogaritKomision, KodIntegrimi, KodiISKSH, MeDogane, TipiId);

                IdKlientFurnitor = idkf;
                if (!mesazh.Status)
                {
                    return mesazh;
                }

                if (ngaImport)
                {
                    var dbImport = new clsDatabazeImporte(db);
                    mesazh = colImportSQL.updateDokTabeleTemportal(idTemp, IdNdermarja, 1, emerTabKoka, primaryKey, emerFusheNdermarrje, dbImport);

                    if (!mesazh.Status)
                    {
                        return mesazh;
                    }
                }

                if (HfArkiva != null)
                    mesazh = colArkiva.RuajArkiven(IdKlientFurnitor, 12, IdPerdoruesi, IdNdermarja, HfArkiva);

                if (!mesazh.Status)
                    return mesazh; 
                PubSub ps = new PubSub("alphaweb", "alpha_clients", "AlphaToFatura_Clients", "https://aso.alpha.al/rest/importClientsFromAlphaToFirebase");
                ps.PublishPubSub(3, 1, 2, 1, krijoObjektPerPubSub());
                scope.Complete();

                if (kaNdryshimNumri)
                    return mesazhKontrolli;

                return mesazh;
            }
        }
        public object krijoObjektPerPubSub()
        {
            if(this.OColAdresat == null)
            {
                return new
                {
                    clientAddress = "",
                    clientCode = this.KodKlientFurnitor,
                    clientEmail = this.EmailKF,
                    clientIdType = this.TipiId,
                    clientName = this.EmertimiKF,
                    clientNipt = this.NiptiKF,
                    clientPhone = this.TelKF,
                    clientTown = new clsQyteti(this.QytetiKF).KodiQyteti,
                    clientCountry = this.ShtetiKF,
                    currency = new clsMonedha(this.idMonedha).KodiMonedha,
                    organization = clsKontrollePerFiskalizimin.ktheInitialCatalogTeLoguar(),
                    ndermarrja = new clsNdermarrje(IdNdermarja).NdermarrjeKodi
                };
            }
            else
            {
                return new
                {
                    clientAddress = this.OColAdresat.Count == 0 ? "" : this.OColAdresat[0].Adresa,
                    clientCode = this.KodKlientFurnitor,
                    clientEmail = this.EmailKF,
                    clientIdType = this.TipiId,
                    clientName = this.EmertimiKF,
                    clientNipt = this.NiptiKF,
                    clientPhone = this.TelKF,
                    clientTown = new clsQyteti(this.QytetiKF).KodiQyteti,
                    clientCountry = this.ShtetiKF,
                    currency = new clsMonedha(this.idMonedha).KodiMonedha,
                    organization = clsKontrollePerFiskalizimin.ktheInitialCatalogTeLoguar(),
                    ndermarrja = new clsNdermarrje(IdNdermarja).NdermarrjeKodi
                };
            }
            
        }
        /// <summary>
        /// Modifikon objektin klient/furnitor ne tabelen perkatese ne databaze.Therret funksionin
        /// </summary>
        /// <returns>Kthen true nese modifikimi perfundoi me sukses</returns>
        public clsMesazh Modifiko(DateTime dateFunditModKlienti, bool vjenNgaImportSQL, string idArtikulliImp, string emerTabele, string primaryKey, string ndermarrjeKey)
        {
            //modifikon klientin furnitorin bashke me buxhetet, adresat, kontaktet,vlerat e fushave shtese
            var colAdresat = new colAdresatKlientFurnitor(IdKlientFurnitor);
            var colKontaktet = new colKontaktiKlientFurnitor(IdKlientFurnitor);
            var colLidhjetAutorizim = new colLidhjetAutorizim(IdKlientFurnitor, "KlientFurnitor");

            using (var scope = new MyTransactionScope())
            {
                var dbKont = new clsDatabaseKontabilitet();
                try
                {
                    if (Modifikuar(IdKlientFurnitor, dateFunditModKlienti))
                        return new MesazhGabimi(MessagesResource.Messages["msgKlientiEshteModifikuarPerditesojeni"]);
                    var mesazh = dbKont.modifikoKF(IdKlientFurnitor, KodKlientFurnitor, IdLlogari, LlojiKF, TitulliKF, AktivitetiKF, EmertimiKF, EmerKerkimiKF, NiptiKF, QytetiKF, ShtetiKF, TelKF, FaxKF, CelKF, EmailKF, WebPageKF, IBANKF, LlogariBankareKF, AktivKF, IdLlogZbritje, IdLlogariDytesore, IdKushtePagese, IdMetoda, MaturimiKF, IdKatZbritje, LimitParalajmerues, LimitBllokues, IdKategoriKlienti, KushteDergimi, MenyraTransportit, OfertaAutomatike, VleraLimitPorositur, Prioriteti, CmimUlet, IdNivelCmimi, IdPerfaqesuesShitje, IdQenderKosto, IdFushata, ZbritjeAnalitike, ZbritjeTotal, IdPerdoruesi, IdKonfig, IdStatusDok, Licenca, Swift, EmriBanka, AdresaBanka, Idgrupim1kf, Idgrupim2kf, Idgrupim3kf, NrTVSH, IdObjektivaKosto, IdKrijuesi, IdNdermarjeBij, LlojPorosie, Kupon, Koordinata, IdPerfaqesuesShitje2, KlientSpecifik, Fermer, AutoNgarkese, ShitjePaTvsh, PerqindjeAgjenti, PerqindjeAgjenti2, Prospekt, EmailPerPajisje, Shenime, DteDatelindjaKF, IdPerfaqesuesShitje3, PerqindjeAgjenti3, IdTvsh, EmertimFature, LlogaritKomision, KodIntegrimi, KodiISKSH, MeDogane,TipiId, clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizim(), "", Idklientfurnitorkryesor);
                    if (!mesazh.Status)
                        return mesazh;

                    foreach (var o in OColBuxhetet)
                    {
                        if (!mesazh.Status)
                            return mesazh;

                        mesazh = dbKont.modifikoBuxhet(o.IdBuxheti, o.IdLlojBuxheti, o.IdLidhese, o.Muaj, o.Buxheti_1, o.Buxheti_2, o.DtAktivizimi, o.IdKonfigUrdherPagese);
                    }
                    if (!mesazh.Status)
                        return mesazh;

                    var dbAdmin = new clsDatabaseAdmin(dbKont);
                    if (OColVleratFushatShtese.Count > 0)
                    {
                        OColVleratFushatShtese.ForEach(x => x.IdLidhese = IdKlientFurnitor);
                        mesazh = OColVleratFushatShtese.Ruaj();
                    }

                    if (!mesazh.Status)
                    {
                        dbKont.rollbackTransaksion();
                        mesazh.Status = false;
                        mesazh.PershkrimMesazhi = mesazh.PershkrimMesazhi;
                        return mesazh;
                    }

                    if (colKontaktet.Count < OColKontaktet.Count)//rasti kur jane shtuar rreshta trupi
                    {
                        for (int i = 0; i < OColKontaktet.Count; i++)
                        {
                            OColKontaktet[i].IdKlientFurnitor = IdKlientFurnitor;
                            if (i < colKontaktet.Count)
                            {
                                OColKontaktet[i].IdKontaktiKlientFurnitor = colKontaktet[i].IdKontaktiKlientFurnitor;
                                mesazh = dbKont.modifikoKontakt(OColKontaktet[i].IdKontaktiKlientFurnitor, OColKontaktet[i].IdKlientFurnitor,
                                    OColKontaktet[i].EmerKontakti, OColKontaktet[i].MbiemerKontakti, OColKontaktet[i].TelKontakti,
                                    OColKontaktet[i].FaxKontakti, OColKontaktet[i].CelKontakti, OColKontaktet[i].EmailKontakti);
                            }
                            else
                            {
                                int idK;
                                mesazh = dbKont.ruajKontakt(out idK, OColKontaktet[i].IdKlientFurnitor,
                                    OColKontaktet[i].EmerKontakti, OColKontaktet[i].MbiemerKontakti, OColKontaktet[i].TelKontakti,
                                    OColKontaktet[i].FaxKontakti, OColKontaktet[i].CelKontakti, OColKontaktet[i].EmailKontakti);
                            }
                            if (!mesazh.Status)
                            {
                                dbKont.rollbackTransaksion();
                                return mesazh;
                            }
                        }
                    }
                    else//rasti kur jane fshire rreshta
                    {
                        int count = 0;
                        for (int i = 0; i < colKontaktet.Count; i++)
                        {
                            if (count < OColKontaktet.Count)
                            {
                                OColKontaktet[i].IdKlientFurnitor = IdKlientFurnitor;
                                OColKontaktet[i].IdKontaktiKlientFurnitor = colKontaktet[i].IdKontaktiKlientFurnitor;
                                mesazh = dbKont.modifikoKontakt(OColKontaktet[i].IdKontaktiKlientFurnitor, OColKontaktet[i].IdKlientFurnitor,
                                    OColKontaktet[i].EmerKontakti, OColKontaktet[i].MbiemerKontakti, OColKontaktet[i].TelKontakti,
                                    OColKontaktet[i].FaxKontakti, OColKontaktet[i].CelKontakti, OColKontaktet[i].EmailKontakti);
                            }
                            else
                                mesazh = dbKont.fshiKontakt(colKontaktet[i].IdKontaktiKlientFurnitor);

                            count++;
                            if (!mesazh.Status)
                                return mesazh;
                        }
                    }

                    mesazh = clsFunksione.modifikoLidhjeAutorizimSipasLlojitTeBuxhetit(OColLidhjetAutorizim, "KlientFurnitor", IdKlientFurnitor, colLidhjetAutorizim, dbKont, dbAdmin, true,IdPerdoruesi);
                    if (!mesazh.Status)
                        return mesazh;

                    if (colAdresat.Count < OColAdresat.Count)//rasti kur jane shtuar rreshta trupi
                    {
                        for (int i = 0; i < OColAdresat.Count; i++)
                        {
                            OColAdresat[i].IdKlientFurnitor = IdKlientFurnitor;
                            if (i < colAdresat.Count)
                            {
                                OColAdresat[i].IdAdresaKlientFurnitor = colAdresat[i].IdAdresaKlientFurnitor;
                                mesazh = dbKont.modifikoAdrese(OColAdresat[i].IdAdresaKlientFurnitor, OColAdresat[i].IdKlientFurnitor, OColAdresat[i].IdTipAdrese, OColAdresat[i].Adresa, OColAdresat[i].KodiPostar);
                            }
                            else if (!string.IsNullOrEmpty(OColAdresat[i].Adresa) || !string.IsNullOrEmpty(OColAdresat[i].KodiPostar))
                            {
                                int idA;
                                mesazh = dbKont.ruajAdrese(out idA, OColAdresat[i].IdKlientFurnitor, OColAdresat[i].IdTipAdrese, OColAdresat[i].Adresa, OColAdresat[i].KodiPostar);
                            }
                            if (!mesazh.Status)
                                return mesazh;
                        }
                    }
                    else//rasti kur jane fshire rreshta
                    {
                        int count = 0;
                        for (int i = 0; i < colAdresat.Count; i++)
                        {
                            if (count < OColAdresat.Count)
                            {
                                OColAdresat[i].IdKlientFurnitor = IdKlientFurnitor;
                                OColAdresat[i].IdAdresaKlientFurnitor = colAdresat[i].IdAdresaKlientFurnitor;
                                mesazh = dbKont.modifikoAdrese(OColAdresat[i].IdAdresaKlientFurnitor, OColAdresat[i].IdKlientFurnitor, OColAdresat[i].IdTipAdrese, OColAdresat[i].Adresa, OColAdresat[i].KodiPostar);
                            }
                            else
                                mesazh = dbKont.fshiAdrese(colAdresat[i].IdAdresaKlientFurnitor);

                            count++;

                            if (!mesazh.Status)
                                return mesazh;
                        }
                    }

                    mesazh = colMarreveshjetPerKlient.FshiMarreveshjetEKlientit(IdKlientFurnitor, dbKont);
                    if (!mesazh.Status)
                        return mesazh;

                    foreach (var m in _colMarreveshjet)
                    {
                        m.IdKlient = IdKlientFurnitor;
                        mesazh = m.RuajMarreveshjePerKlient(dbKont);
                        if (!mesazh.Status)
                            return mesazh;
                    }

                    if (!mesazh.Status)
                        return mesazh;

                    if (vjenNgaImportSQL)
                    {
                        mesazh = (new clsDatabazeImporte(dbKont)).updateDokTabeleTemportal(idArtikulliImp, IdNdermarja, 1, emerTabele, primaryKey, ndermarrjeKey);
                        if (!mesazh.Status)
                            return mesazh;
                    }

                    mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
                    PubSub ps = new PubSub("alphaweb", "alpha_clients", "AlphaToFatura_Clients", "https://aso.alpha.al/rest/importClientsFromAlphaToFirebase");
                    ps.PublishPubSub(3, 1, 2, 1, krijoObjektPerPubSub());
                    scope.Complete();


                    return mesazh;
                }
                catch (Exception ce)
                {
                    return new clsMesazh(false, ce.Message);
                }
            }
        }

        public clsMesazh FshiStatus()
        {
            using (var data = new clsDatabaseKontabilitet())
                return data.fshiKFStatus(IdKlientFurnitor, IdPerdoruesi);
        }

        /// <summary>
        /// Merr llogarine e klientit/furnitorit sipas kodit te nenllojit te llogarise
        /// </summary>
        /// <param name="kodnenllojllogarie">Kodi i nenllojit te llogarise <see cref="DbCore.DbKontabiliteti.clsNenLlojLlogarish"/></param>
        /// <returns>Kthen nje objekt te tipit <see cref="DbCore.DbKontabiliteti.clsLlogari"/></returns>
        public clsLlogari merrLlogariKlientFurnitori(string kodnenllojllogarie, clsDatabaseKontabilitet db)
        {

            int idLlogari;
            switch (kodnenllojllogarie)
            {
                case "KKL":
                case "KFR":
                    idLlogari = IdLlogari;
                    if (idLlogari == 0)
                        throw new MyException("IdLlogari 0 per kf me kod: " + KodKlientFurnitor);
                    break;
                case "ZKL":
                    idLlogari = IdLlogZbritje;
                    if (idLlogari == 0)
                    {// mund te kete raste qe nuk ka llogari zbritje sepse nuk perdoret akoma tamam por tek skema jane vendosur
                        ImbLogger.Error("clsKlientFurnitor" + Environment.NewLine + "nuk ka llogari zbritje per klientin " + KodKlientFurnitor);
                        return new clsLlogari();
                    }
                    break;
                case "DKL":
                case "DFR":
                    idLlogari = IdLlogariDytesore;
                    if (idLlogari == 0)
                        throw new MyException("Klienti me kod: " + KodKlientFurnitor + " nuk ka te percaktuar Llogari Parapagimi!");
                    break;
                default:
                    throw new MyException("Lloj i panjohur kodNenLlojLlogarie: " + kodnenllojllogarie);
            }

            return new clsLlogari(idLlogari, db);
        }

        public clsLlogari merrLlogariKlientFurnitori(string kodnenllojllogarie)
        {
            using (var db = new clsDatabaseKontabilitet())
            {
                return merrLlogariKlientFurnitori(kodnenllojllogarie, db);
            }
        }

        public clsMesazh mbushKlientFurnitorLikeKodi(string kodKf, int idNdermarrja)
        {
            var dbKlientFurnitore = new clsDatabaseKontabilitet();
            var mesazhi = MbushKlientFurnitor(dbKlientFurnitore.merrKlientFurnitorLikeKodi(kodKf, idNdermarrja));
            dbKlientFurnitore.Dispose();
            if (mesazhi.Status)
                mesazhi.PershkrimMesazhi = "KlientFurnitori u mbush me sukses nga db-ja";
            else
                mesazhi.PershkrimMesazhi = "KlientFurnitori me kod qe permban: " + kodKf + " dhe idNdermarrje: " + IdNdermarja + " nuk ekziston";
            return mesazhi;
        }

        /// <summary>
        /// mbush klient furnitoret sipas kodit
        /// </summary>
        /// <param name="kodKF">kodi i klient furnitorit</param>
        /// <param name="idNdermarrja">id e ndermarrjes</param>        
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        public clsMesazh mbushKlientFurnitorSipasKodit(string kodKF, int idNdermarrja)
        {
            clsDatabaseKontabilitet dbKlientFurnitore = new clsDatabaseKontabilitet();
            clsMesazh mesazhi = MbushKlientFurnitor(dbKlientFurnitore.ktheKlientFurnitorSipasKodit(kodKF, idNdermarrja));
            dbKlientFurnitore.Dispose();
            if (mesazhi.Status)
                mesazhi.PershkrimMesazhi = "KlientFurnitori u mbush me sukses nga db-ja";
            else
                mesazhi.PershkrimMesazhi = "KlientFurnitori me kod: " + kodKF + " dhe idNdermarrje: " + IdNdermarja + " nuk ekziston";
            return mesazhi;
        }

        public clsMesazh mbushKlientFurnitorSipasKodit(string kodKF, int idNdermarrja, int idperdoruesi)
        {
            clsDatabaseKontabilitet dbKlientFurnitore = new clsDatabaseKontabilitet();
            clsMesazh mesazhi = MbushKlientFurnitor(dbKlientFurnitore.merrKlientFurnitor(kodKF, idNdermarrja, idperdoruesi));
            dbKlientFurnitore.Dispose();
            if (mesazhi.Status)
                mesazhi.PershkrimMesazhi = "KlientFurnitori u mbush me sukses nga db-ja";
            else
                mesazhi.PershkrimMesazhi = "KlientFurnitori me kod: " + kodKF + " dhe idNdermarrje: " + IdNdermarja + " nuk ekziston";
            return mesazhi;
        }

        public clsMesazh mbushKlientFurnitorSipasKodit(string kodKF, int idNdermarrja, int idperdoruesi, clsDatabaseKontabilitet dbKlientFurnitore)
        {
            clsMesazh mesazhi = MbushKlientFurnitor(dbKlientFurnitore.merrKlientFurnitor(kodKF, idNdermarrja, idperdoruesi));

            if (mesazhi.Status)
            {
                ImbLogger.LogTraceShitje("KlientFurnitori u mbush me sukses nga db-ja");
                mesazhi.PershkrimMesazhi = "KlientFurnitori u mbush me sukses nga db-ja";
            }

            else
            {
                ImbLogger.LogTraceShitje($"KlientFurnitori me kod: {kodKF} dhe idNdermarrje:{IdNdermarja} nuk ekziston");
                mesazhi.PershkrimMesazhi = "KlientFurnitori me kod: " + kodKF + " dhe idNdermarrje: " + IdNdermarja + " nuk ekziston";
            }

            return mesazhi;
        }

        public static bool eshteNdermarrjeKlientiOwn(int idKlientFurnitor)
        {
            using (var db = new clsDatabaseKontabilitet())
                return db.eshteNdermarrjeKlientiOwn(idKlientFurnitor);
        }

        public clsMesazh KtheKlientFurnitorSipasNdermarjeBij(int idndermarjebij, int idNdermarrja, int llojporosie)
        {
            ImbLogger.LogWarningShitje("Fillon mbushja e KlientFurnitor.");
            clsDatabaseKontabilitet dbKlientFurnitore = new clsDatabaseKontabilitet();
            clsMesazh mesazhi = MbushKlientFurnitor(dbKlientFurnitore.ktheKlientFurnitorSipasNdermarjeBij(idndermarjebij, idNdermarrja, llojporosie));
            dbKlientFurnitore.Dispose();
            if (mesazhi.Status)
            {
                mesazhi.PershkrimMesazhi = "KlientFurnitori u mbush me sukses nga db-ja";
                ImbLogger.LogWarningShitje("KlientFurnitor u mbush me sukses.");
            }
            else
            {
                mesazhi.PershkrimMesazhi = "KlientFurnitori per ndermarjen bij: " + idndermarjebij + " dhe idNdermarrje: " + IdNdermarja + " nuk ekziston";
                ImbLogger.LogWarningShitje("KlientFurnitori per ndermarjen bij: " + idndermarjebij + " dhe idNdermarrje: " + IdNdermarja + " nuk ekziston");
            }
            return mesazhi;
        }

        public void mbushKlientFurnitorSipasKoditAzhornim(string kodKF, int idNdermarrja, DateTime dtdok)
        {
            using (var dbKlientFurnitore = new clsDatabaseKontabilitet())
                MbushKlientFurnitorAzhornimGjendje(dbKlientFurnitore.ktheKlientFurnitorSipasKoditAzhornim(kodKF, idNdermarrja, dtdok));
        }
        
        /// <summary>
        /// todo patricia
        /// </summary>
        /// <param name="idKf"></param>
        /// <param name="dtdok"></param>
        /// <returns></returns>
        public void mbushKlientFurnitorSipasKoditAzhornim(int idKf, DateTime dtdok)
        {
            using (var dbKlientFurnitore = new clsDatabaseKontabilitet())
                MbushKlientFurnitorAzhornim(dbKlientFurnitore.ktheKlientFurnitorSipasKoditAzhornim(idKf, dtdok));
        }

        /// <summary>
        /// mbush klient furnitoret sipas nr te llogarise dhe id ndermarrjes
        /// </summary>
        /// <param name="nr">nr i llogarise</param>
        /// <param name="idnderm">id e ndermarrjes</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        public void MbushKlientFurnitor(string nr, int idnderm)
        {
            ImbLogger.LogTraceShitje("Fillon mbushja e KlientFurnitor sipas nr te llogarise:" + Convert.ToString(nr) + " dhe id ndermarrje: " + Convert.ToString(idnderm));
            var dbKlientFurnitore = new clsDatabaseKontabilitet();
            MbushKlientFurnitor(dbKlientFurnitore.merrKlientFurnitor(nr, idnderm));
            dbKlientFurnitore.Dispose();
            ImbLogger.LogTraceShitje("U mbush me sukses KlientFurnitor sipas nr te llogarise:" + Convert.ToString(nr) + " dhe id ndermarrje: " + Convert.ToString(idnderm));
        }

        /// <summary>
        /// mbush klientet dhe furnitoret sipas id se klient furnitorit
        /// </summary>
        /// <param name="idKlientFurnitor">id e klient furnitorit</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        public void MbushKlientFurnitorSipasId(int idKlientFurnitor)
        {
            ImbLogger.LogTraceShitje("Fillon mbushja e KlientFurnitor sipas id se klient furnitorit : " + Convert.ToString(idKlientFurnitor));
            var dbKlientFurnitore = new clsDatabaseKontabilitet();
            MbushKlientFurnitor(dbKlientFurnitore.merrKlientFurnitorSipasID(idKlientFurnitor));
            dbKlientFurnitore.Dispose();
            ImbLogger.LogTraceShitje("U mbush me sukses KlientFurnitor sipas id se klient furnitorit : " + Convert.ToString(idKlientFurnitor));
        }

        /// <summary>
        /// Shikon ne db nese ekziston klientfurnitori me kod dhe me idNdermarrje
        /// </summary>
        /// <param name="kodKlientFurnitor"></param>
        /// <param name="idNdermarrja"></param>
        /// <returns>True nese ekziston, false perndryshe</returns>
        public static bool EkzistonKlientFurnitor(string kodKlientFurnitor, int idNdermarrja)
        {
            using (var dbKontab = new clsDatabaseKontabilitet())
                return dbKontab.ekzistonKlientFurnitor(kodKlientFurnitor, idNdermarrja);
        }

        public static bool EkzistonKlientFurnitorNipt(string nipt, string klient, int idNdermarrje)
        {
            using (var dbKontab = new clsDatabaseKontabilitet())
                return dbKontab.ekzistonKlientFurnitorMeKeteNipt(nipt, klient, idNdermarrje);
        }

        /// <summary>
        /// Metode e klases, jo e objektit. Kthen id e klient furnitorit sipas nr te llogarise dhe idndermarrjes
        /// </summary>
        /// <param name="nr">nr i llogarise</param>
        /// <param name="idnderm">id e ndermarrjes</param>
        /// <returns>id e klient furnitorit</returns>
        public static int MerrIdKlientFurnitor(string kodi, int idnderm)
        {
            using (var dbKlientFurnitore = new clsDatabaseKontabilitet())
                return dbKlientFurnitore.merrIDKlientFurnitorSipasKodit(kodi, idnderm);
        }

        public static string MerrKodKlientFurnitorSipasKodIntegrimi(string kod, int idnderm)
        {
            using (var dbKlientFurnitore = new clsDatabaseKontabilitet())
                return dbKlientFurnitore.merrKodKlientiSipasKodIntegrimi(kod, idnderm);
        }

        /// <summary>
        /// Metode e klases, jo e objektit. Kthen emertimin e klient furnitorit sipas kodit 
        /// </summary>
        /// <param name="nr">kodi i klient\furnitorit</param>
        /// <param name="idnderm">id e ndermarrjes</param>
        /// <returns>emertimin e klient furnitorit</returns>
        public static string mbushEmertimiKlientFurnitor(string kodKF, int idnderm)
        {
            using (var dbKlientFurnitore = new clsDatabaseKontabilitet())
                return dbKlientFurnitore.merrEmertiminKF(kodKF, idnderm);
        }

        public static string merrKodKlientFurnitorSipasId(int idKlientFurnitor)
        {
            using (var dbKlientFurnitore = new clsDatabaseKontabilitet())
                return dbKlientFurnitore.merrKodKlientFurnitorSipasId(idKlientFurnitor);
        }

        public static decimal MerrDetyrimKf(int idkf, DateTime data)
        {
            using (var dbKlientFurnitore = new clsDatabaseKontabilitet())
                return dbKlientFurnitore.merrDetyrimKF(idkf, data);
        }

        public static DataRow MerrDetyrimiKfMeparshem(int idkf, int idKokaShitje, DateTime data)
        {
            using (var dbKlientFurnitore = new clsDatabaseKontabilitet())
                return dbKlientFurnitore.merrDetyrimKFMeparshem(idkf, idKokaShitje, data);
        }

        public static bool EshteAzhornimVeprimiFunditKlientFurnitor(int idKlientFurnitor, DateTime dt, int idNdermarrje)
        {
            using (var dbKlientFurnitore = new clsDatabaseKontabilitet())
            {
                var veprimiFundit =
                    dbKlientFurnitore.merrVeprimTeFunditKlientFurnitor(idNdermarrje, idKlientFurnitor, dt);
                if (veprimiFundit == null)
                    return false;
                return veprimiFundit == "AKF";
            }
        }

        public static bool kaVeprimeKlientFurnitor(int idKf, int idNdermarrje)
        {
            using (var dbKlientFurnitore = new clsDatabaseKontabilitet())
            {
                var veprimiFundit =
                    dbKlientFurnitore.merrVeprimTeFunditKlientFurnitor(idNdermarrje, idKf, new DateTime());
                if (veprimiFundit == null || veprimiFundit == string.Empty)
                    return false;
                return true;
            }
        }

        public ListeVleraInfo KtheInfoKf(DateTime data, int idInfo)
        {
            var lista = new ListeVleraInfo();
            if (IdKlientFurnitor <= 0)
                return lista;
            data = data.ToLocalTime();
            var info = new clsInfoKoka(idInfo);
            var col = colInfoTrupi.merrInfoSipasIdKokaDheVisibleNew(info.IdInfoKoka, true, IdNdermarja);
            lista.colInfoTrupi = col;
            var vlerat = new ArrayList();
            lista.vlerat = vlerat;
            int shifraPasPresjes = info.IdFormatNumri;
            foreach (var trup in col)
            {
                try
                {
                    switch (trup.EmerKolone)
                    {
                        case "Emertimi":
                            vlerat.Add(EmertimiKF);
                            break;
                        case "KodKF":
                            vlerat.Add(KodKlientFurnitor);
                            break;
                        case "Monedha":
                            vlerat.Add(Monedha);
                            break;
                        case "Detyrimi":
                            var detyrimi = MerrDetyrimKf(IdKlientFurnitor, data);
                            if (detyrimi != 0)
                                vlerat.Add(detyrimi.ToString("F" + shifraPasPresjes));
                            else
                                vlerat.Add(detyrimi.ToString("F" + shifraPasPresjes));
                            break;
                        case "DetyrimiMonBaze":
                            var detyrimimonbaze = MerrDetyrimkfMonBaze(IdKlientFurnitor, data);
                            if (detyrimimonbaze != 0)
                                vlerat.Add(detyrimimonbaze.ToString("F" + shifraPasPresjes));
                            else
                                vlerat.Add(detyrimimonbaze.ToString("F" + shifraPasPresjes));
                            break;
                        case "LimitiParalajmerues":
                            if (LimitParalajmerues != 0)
                                vlerat.Add(LimitParalajmerues.ToString("F" + shifraPasPresjes));
                            else
                                vlerat.Add(LimitParalajmerues.ToString("F" + shifraPasPresjes));
                            break;
                        case "LimitiBllokues":
                            if (LimitBllokues != 0)
                                vlerat.Add(LimitBllokues.ToString("F" + shifraPasPresjes));
                            else
                                vlerat.Add(LimitBllokues.ToString("F" + shifraPasPresjes));
                            break;
                        case "KategoriZbritje":
                            vlerat.Add(KodKatZbritje);
                            break;
                        case "Zbritje":
                            vlerat.Add(PerqindjeKatZbritje);
                            break;
                        case "NivelCmimi":
                            vlerat.Add(PershkrimNivelCmimi);
                            break;
                        case "Grupimi1":
                            vlerat.Add(Grupim1KF);
                            break;
                        case "Grupimi2":
                            vlerat.Add(Grupim2KF);
                            break;
                        case "Grupimi3":
                            vlerat.Add(Grupim3KF);
                            break;
                        case "Llogaria":
                            vlerat.Add(NrLlogKlientFurnitor);
                            break;
                        case "Arka/Banka":
                            var banka = new clsBanka();
                            banka.mbushBanke(EmriBanka);
                            vlerat.Add(banka.KodiBanka ?? "");
                            break;
                        case "Telefoni":
                            vlerat.Add(TelKF);
                            break;
                        case "Email":
                            vlerat.Add(EmailKF);
                            break;
                        case "Qyteti":
                            vlerat.Add(EmriQytetitKF);
                            break;
                        case "Nipt":
                            vlerat.Add(NiptiKF);
                            break;
                        case "EmerKerkimiKf":
                            vlerat.Add(EmerKerkimiKF);
                            break;
                        case "PerfaqesuesShitje1":
                            var agjenti1 = new clsAgjentShitje(IdPerfaqesuesShitje);
                            vlerat.Add(agjenti1.EmriAgjentShitje + " " + agjenti1.MbiemriAgjentShitje);
                            break;
                        case "Shteti":
                            vlerat.Add(ShtetiKF);
                            break;
                        case "EmertimFature":
                            vlerat.Add(EmertimFature);
                            break;
                        default:
                            vlerat.Add("Konfigurimi gabim!");
                            break;
                    }
                }catch (Exception ex)
                {
                    vlerat.Add("Vlerat gabim!");
                }
            }
            return lista;
        }

        public static clsKlientFurnitor KtheKlientFurnitorSipasIdNeseEkziston(int idKlientFurnitor)
        {
            var kf = new clsKlientFurnitor(idKlientFurnitor);
            return kf.IdKlientFurnitor <= 0 ? null : kf;
        }

        public static int MerrIdBanke(int idKf)
        {
            using (var dbKont = new clsDatabaseKontabilitet())
                return MerrIdBanke(idKf, dbKont);
        }

        public static int MerrIdBanke(int idKf, clsDatabaseKontabilitet dbKont) => dbKont.merrIdBankeKF(idKf);

        public static bool MerrLlojin(int idKf)
        {
            using (var dbKont = new clsDatabaseKontabilitet())
                return MerrLlojin(idKf, dbKont);
        }

        public static bool Modifikuar(int idKlientFurnitor, DateTime dateFunditModKlienti)
        {
            using (var dbKont = new clsDatabaseKontabilitet())
                return dbKont.modifikuarKlient(idKlientFurnitor, dateFunditModKlienti);
        }

        public static bool EshteKlientSpecifik(int idKf, clsDatabaseKontabilitet dbKont)
        {
            ImbLogger.LogWarningShitje($"Kontrolli nqs eshte klient specifik per idKf:{idKf}");
            return dbKont.TransCache.eshteKlientSpecifik(idKf, dbKont);
        }

        public static bool MerrLlojin(int idKf, clsDatabaseKontabilitet dbKont)
        {
            return dbKont.merrLlojinKF(idKf);
        }

        public static string KtheEmailPerPajisje(int idKlientFurnitor, clsDatabaseKontabilitet dbKont)
        {
            return dbKont.ktheEmailPerPajisje(idKlientFurnitor);
        }

        public static clsKlientFurnitor KrijoKlientFurnitorVartes(IDataRecord dataRecord)
        {
            var klientFurnitorVartes = new clsKlientFurnitor();
            klientFurnitorVartes.MbushKlientFurnitor(dataRecord);
            return klientFurnitorVartes;
        }

        #endregion

        #region Metoda Private

        private clsMesazh KontrolloKf(bool shtim, bool prospekt, int monndermarje, bool kontrolloekzistence, ResourceManager rm, CultureInfo ci)
        {
            if (KodKlientFurnitor == "")
                return new clsMesazh(false, MessagesResource.Messages["msgPlotesoniKodinkf"]);

            var kontrollKodKf = clsFunksione.kontrolloKaraktereMeMesazh(KodKlientFurnitor, FusheKontrolli.Kodi, false);
            if (!kontrollKodKf.Status)
                return kontrollKodKf;

            if (EmertimiKF == "")
                return new clsMesazh(false, MessagesResource.Messages["msgPlotesoniEmertimkf"]);

            var kontrollemertimiKf = clsFunksione.kontrolloKaraktereMeMesazh(EmertimiKF, FusheKontrolli.Pershkrimi, true);
            if (!kontrollemertimiKf.Status)
                return kontrollemertimiKf;

            if (!prospekt)
            {

                if (NrLlogKlientFurnitor == "")
                    return new clsMesazh(false, rm.GetString("msgPlotesoniLlogarinekf", ci));

                if (!clsLlogari.ekzistonLlogari(NrLlogKlientFurnitor, IdNdermarja))
                    return new clsMesazh(false, rm.GetString("msgLlogariakfNukEkziston", ci));

                if (!clsLlogari.eshteLlogariAktive(NrLlogKlientFurnitor, IdNdermarja))
                    return new clsMesazh(false, rm.GetString("msgLlogkfAktive", ci));

                if (NrLlogZbritje != "" && !clsLlogari.ekzistonLlogari(NrLlogZbritje, IdNdermarja))
                    return new clsMesazh(false, rm.GetString("msgLlogariZbritjekfNukEkziston", ci));

                if (NrLlogZbritje != "" && !clsLlogari.eshteLlogariAktive(NrLlogZbritje, IdNdermarja))
                    return new clsMesazh(false, rm.GetString("msgLlogariZbritjekfJoAktive", ci));

                if (NrLlogDytesor != "" && !clsLlogari.ekzistonLlogari(NrLlogDytesor, IdNdermarja))
                    return new clsMesazh(false, rm.GetString("msgLlogDytesorekfNukEkziston", ci));

                if (NrLlogDytesor != "" && !clsLlogari.eshteLlogariAktive(NrLlogDytesor, IdNdermarja))
                    return new clsMesazh(false, rm.GetString("msgLlogDytesorekfJoAktive", ci));

                if (NrLlogDytesor != "")
                {
                    if (new clsLlogari(NrLlogKlientFurnitor, IdNdermarja).IdMonedha != new clsLlogari(NrLlogDytesor, IdNdermarja).IdMonedha)
                        return new clsMesazh(false, rm.GetString("msgMonLlogParapNjejte", ci));
                }
            }

            if (shtim && kontrolloekzistence && EkzistonKlientFurnitor(KodKlientFurnitor, IdNdermarja))
                return new clsMesazh(false, rm.GetString("msgEkzistonkfMeKod", ci));

            if (!string.IsNullOrEmpty(Grupim1KF))
            {
                if (!clsGrupeKF.EkzistonGrupKfSipasKodLloje(Grupim1KF, IdNdermarja, 1, LlojiKF ? 0 : 1))
                    return new clsMesazh(rm.GetString("msgGrupimParekfNukEkziston", ci));

                if (clsGrupeKF.EshtePrind(Idgrupim1kf, IdNdermarja))
                    return new clsMesazh(false, rm.GetString("msgGrupimiParekfPrind", ci));
            }

            if (!string.IsNullOrEmpty(Grupim2KF))
            {
                if (!clsGrupeKF.EkzistonGrupKfSipasKodLloje(Grupim2KF, IdNdermarja, 2, LlojiKF ? 0 : 1))
                    return new clsMesazh(rm.GetString("msgGrupimiDytekfNukEkziston", ci));

                if (clsGrupeKF.EshtePrind(Idgrupim2kf, IdNdermarja))
                    return new clsMesazh(false, rm.GetString("msgGrupimDytekfPrind", ci));
            }

            if (!string.IsNullOrEmpty(Grupim3KF))
            {
                if (!clsGrupeKF.EkzistonGrupKfSipasKodLloje(Grupim3KF, IdNdermarja, 3, LlojiKF ? 0 : 1))
                    return new clsMesazh(rm.GetString("msgGrupimTretekfNukEkziston", ci));

                if (clsGrupeKF.EshtePrind(Idgrupim3kf, IdNdermarja))
                    return new clsMesazh(false, rm.GetString("msgGrupimiTretekfPrind", ci));
            }

            if (!string.IsNullOrEmpty(_kodBanke))
            {
                if (!clsBanka.ekziston(_kodBanke, IdNdermarja).Status)
                    return new clsMesazh(false, rm.GetString("msgBankaNukEkziston", ci));

                var banka = new clsBanka();
                banka.mbushBankeSipasKoditMeAutorizime(_kodBanke, IdNdermarja, IdPerdoruesi);

                if (banka.IdBanka < 1)
                    return new clsMesazh(false, rm.GetString("msgNukKeniAutorizimAB", ci));

                if (!banka.AktivBanka)
                    return new clsMesazh(false, $"Arka me kod {_kodBanke} nuk eshte aktive!");

                var llog = new clsLlogari(IdLlogari);
                if (llog.IdMonedha != banka.IdMonedhaBanka && banka.IdMonedhaBanka != monndermarje)
                    return new clsMesazh(false, rm.GetString("msgMonedhaNjejtekfnd", ci));
            }

            if (KodKatZbritje != "" && !clsKokaKategoriZbritje.ekziston(KodKatZbritje, IdNdermarja))
                return new clsMesazh(false, rm.GetString("msgKategoriZbritjeNukEkzist", ci));

            if (!string.IsNullOrEmpty(_kodMaturimiKf) && !clsMaturimi.Ekziston(_kodMaturimiKf, IdNdermarja))
                return new clsMesazh(false, rm.GetString("msgAfatiMaturimitNukEkziston", ci));

            if (MaturimiKF != 0 && MaturimiKF != -1)
            {
                var mat = new clsMaturimi(MaturimiKF);
                if (mat.LlojMaturimi && !LlojiKF)
                    return new clsMesazh(false, rm.GetString("msgAfatiMaturimitJoLlojFurn", ci));

                if (!mat.LlojMaturimi && LlojiKF)
                    return new clsMesazh(false, rm.GetString("msgAfatiMaturimitJoLLojKlient", ci));
            }

            if (EmriQytetitKF != "" && QytetiKF == 0)
                return new clsMesazh(false, rm.GetString("msgQytetiNukEkziston", ci));

            if (KodKushtePagese != "" && clsKushtPageseKoka.ekziston(KodKushtePagese, IdNdermarja))
                return new clsMesazh(false, rm.GetString("msgKushtPageseNukEkziston", ci));

            if (LimitParalajmerues < 0)
                return new clsMesazh(false, rm.GetString("msgLimitiParaljmPozitiv", ci));

            if (LimitBllokues < 0)
                return new clsMesazh(false, rm.GetString("msgLimitiBllokuesPozitiv", ci));

            if (LimitBllokues != 0 && LimitParalajmerues != 0)
            {
                if (LimitParalajmerues > LimitBllokues)
                    return new clsMesazh(false, rm.GetString("msgLimitParalajmVogelBllok", ci));
            }

            if (_kodKushtDergimi != "" && clsKushtDergimi.ekziston(_kodKushtDergimi, IdNdermarja))
                return new clsMesazh(false, rm.GetString("msgKushtDergimiNukEkziston", ci));

            if (_kodMenyraTransportit != "" && clsMenyreTransporti.ekziston(_kodMenyraTransportit, IdNdermarja))
                return new clsMesazh(false, rm.GetString("msgMenyraTranspNukEkziston", ci));

            if (VleraLimitPorositur < 0)
                return new clsMesazh(false, rm.GetString("msgVlLimitPozitiv", ci));

            if (CmimUlet < 0)
                return new clsMesazh(false, rm.GetString("msgCmimUletPozitiv", ci));

            if (PershkrimNivelCmimi != "" && IdNivelCmimi == 0)
                return new clsMesazh(false, rm.GetString("msgNukEkzistonNivelCmimiMePershk", ci).Replace("{0}", PershkrimNivelCmimi));

            if (IdNivelCmimi != 0)
            {
                var nc = new clsNivelCmimi(IdNivelCmimi);
                if (nc.LlojiNivelCmimi == 0 && !LlojiKF)
                    return new clsMesazh(false, rm.GetString("msgNivelCmimiNukPerdFurn", ci));

                if (nc.LlojiNivelCmimi == 1 && LlojiKF)
                    return new clsMesazh(false, rm.GetString("msgNivelCmimiNukPerdKl", ci));
            }

            if (KodPerfaqesuesShitje != "" && !clsAgjentShitje.ekziston(KodPerfaqesuesShitje, IdNdermarja))
                return new clsMesazh(false, rm.GetString("msgAgjentiNukEkzist", ci));

            if (KodPerfaqesuesShitje2 != "" && !clsAgjentShitje.ekziston(KodPerfaqesuesShitje2, IdNdermarja))
                return new clsMesazh(false, rm.GetString("msgAgjentiNukEkzist", ci));

            if (PershkrimNivelZbritje != "" && ZbritjeAnalitike == -1)
                return new clsMesazh(false, rm.GetString("msgZbritjaNukEkzist", ci));

            if (ZbritjeTotal < 0 || ZbritjeTotal > 100)
                return new clsMesazh(false, rm.GetString("msgZbritjaTotMidis", ci));

            if (Objektiva != "")
            {
                if (!clsObjektivaKosto.ekzistonOK(Objektiva, IdNdermarja))
                    return new clsMesazh(false, rm.GetString("msgShtoLlogariObjektivaEKostosNukEkziston", ci));

                if (!new clsObjektivaKosto(Objektiva, IdNdermarja).Aktiv)
                    return new clsMesazh(false, rm.GetString("msgShtoLlogariObjektivaEKostosNukEsteAktive", ci));
            }

            if (OColLidhjetAutorizim.Count != 0)
            {
                if (OColLidhjetAutorizim.Any(o => o.IdAutorizimeKoka == -1))
                {
                    return new clsMesazh(false, rm.GetString("msgCeljeArkaBankaNiveliAutorizimitNukEkziston", ci));
                }
            }

            return new clsMesazh(true, rm.GetString("msgKontrolletkfSukses", ci));
        }

        private clsMesazh KontrolloKf(out bool kaNdryshimNrAuto, clsDatabaseKontabilitet db, IDictionary<string, object> hfNrAutoKf, bool modifikim)
        {
            kaNdryshimNrAuto = false;
            if (KodKlientFurnitor == "")
                return new clsMesazh(false, "Kodi i klient/furnitorit nuk mund te jete bosh");

            if (!modifikim)
            {
                if (hfNrAutoKf != null)
                {
                    var mes = KontrolloNrAutoKf(out kaNdryshimNrAuto, db, hfNrAutoKf);
                    if (!mes.Status)
                        return mes;
                }

                if (db.ekzistonKlientFurnitor(KodKlientFurnitor, IdNdermarja))
                    return new clsMesazh(false, "Ekziston nje klient/furnitor me kete kod!");
            }

            return new clsMesazh(true, "Kontrollet u kaluan me sukses");
        }

        private static decimal MerrDetyrimkfMonBaze(int idkf, DateTime data)
        {
            using (var dbKlientFurnitore = new clsDatabaseKontabilitet())
                return dbKlientFurnitore.merrDetyrimKFMonBaze(idkf, data);
        }

        private clsMesazh KontrolloNrAutoKf(out bool kaNdryshimNumri, clsDatabaseKontabilitet db, IDictionary<string, object> hfNrAutoKf)
        {
            var dbadm = new clsDatabaseAdmin(db);
            var list = clsNrAutom.kontrollogjithenumrat(dbadm, hfNrAutoKf, DateTime.Today);
            if (NrAuto.ktheVlerenEre(list, "KodKlientFurnitor") != "")
                KodKlientFurnitor = NrAuto.ktheVlerenEre(list, "KodKlientFurnitor");
            return NrAuto.ruajvlera(out kaNdryshimNumri, list, DateTime.Today, IdPerdoruesi, IdNdermarja, dbadm);
        }

        public static string KontrolloKlientMeMarreveshjeAktive(string kodet, DateTime dtdok, string idmarreveshje)
        {
            using (var dbKlientFurnitore = new clsDatabaseKontabilitet())
                return dbKlientFurnitore.KontrolloKlientMeMarreveshjeAktive(kodet, dtdok, idmarreveshje);
        }

        #endregion

        #region Metoda Internal

        internal void MbushKlientFurnitor(clsKlientFurnitor kf)
        {
            ImbLogger.LogTraceShitje("Filloi metoda mbush klientfurnitor");
            IdKlientFurnitor = kf.IdKlientFurnitor;
            KodKlientFurnitor = kf.KodKlientFurnitor;
            NrLlogKlientFurnitor = kf.NrLlogKlientFurnitor;
            IdLlogari = kf.IdLlogari;
            LlojiKF = kf.LlojiKF;
            TitulliKF = kf.TitulliKF;
            AktivitetiKF = kf.AktivitetiKF;
            EmertimiKF = kf.EmertimiKF;
            EmerKerkimiKF = kf.EmerKerkimiKF;
            NiptiKF = kf.NiptiKF;
            QytetiKF = kf.QytetiKF;
            ShtetiKF = kf.ShtetiKF;
            TelKF = kf.TelKF;
            FaxKF = kf.FaxKF;
            CelKF = kf.CelKF;
            EmailKF = kf.EmailKF;
            WebPageKF = kf.WebPageKF;
            IBANKF = kf.IBANKF;
            LlogariBankareKF = kf.LlogariBankareKF;
            AktivKF = kf.AktivKF;
            IdLlogZbritje = kf.IdLlogZbritje;
            NrLlogZbritje = kf.NrLlogZbritje;
            IdLlogariDytesore = kf.IdLlogariDytesore;
            NrLlogDytesor = kf.NrLlogDytesor;
            IdKushtePagese = kf.IdKushtePagese;
            KodKushtePagese = kf.KodKushtePagese;
            IdMetoda = kf.IdMetoda;
            _pershkrimMetoda = kf._pershkrimMetoda;
            MaturimiKF = kf.MaturimiKF;
            _kodMaturimiKf = kf._kodMaturimiKf;
            IdKatZbritje = kf.IdKatZbritje;
            KodKatZbritje = kf.KodKatZbritje;
            PerqindjeKatZbritje = kf.PerqindjeKatZbritje;
            LimitParalajmerues = kf.LimitParalajmerues;
            LimitBllokues = kf.LimitBllokues;
            IdKategoriKlienti = kf.IdKategoriKlienti;
            KushteDergimi = kf.KushteDergimi;
            _kodKushtDergimi = kf._kodKushtDergimi;
            MenyraTransportit = kf.MenyraTransportit;
            _kodMenyraTransportit = kf._kodMenyraTransportit;
            OfertaAutomatike = kf.OfertaAutomatike;
            VleraLimitPorositur = kf.VleraLimitPorositur;
            Prioriteti = kf.Prioriteti;
            CmimUlet = kf.CmimUlet;
            IdNivelCmimi = kf.IdNivelCmimi;
            PershkrimNivelCmimi = kf.PershkrimNivelCmimi;
            IdPerfaqesuesShitje = kf.IdPerfaqesuesShitje;
            KodPerfaqesuesShitje = kf.KodPerfaqesuesShitje;
            IdPerfaqesuesShitje2 = kf.IdPerfaqesuesShitje2;
            KodPerfaqesuesShitje2 = kf.KodPerfaqesuesShitje2;
            IdQenderKosto = kf.IdQenderKosto;
            IdFushata = kf.IdFushata;
            ZbritjeAnalitike = kf.ZbritjeAnalitike;
            PershkrimNivelZbritje = kf.PershkrimNivelZbritje;
            ZbritjeTotal = kf.ZbritjeTotal;
            OColAdresat = kf.OColAdresat;
            OColKontaktet = kf.OColKontaktet;
            OColBuxhetet = kf.OColBuxhetet;
            OColVleratFushatShtese = kf.OColVleratFushatShtese;
            OColLidhjetAutorizim = kf.OColLidhjetAutorizim;
            IdNdermarja = kf.IdNdermarja;
            Viti = kf.Viti;
            IdPerdoruesi = kf.IdPerdoruesi;
            IdKonfig = kf.IdKonfig;
            _monedha = kf._monedha;
            _monedhaZbritje = kf._monedhaZbritje;
            EmriQytetitKF = kf.EmriQytetitKF;
            DtAzhornimi = kf.DtAzhornimi;
            DtLidhje = kf.DtLidhje;
            IdStatusDok = kf.IdStatusDok;
            _dtKrijimi = kf._dtKrijimi;
            _dtModifikimi = kf._dtModifikimi;
            Licenca = kf.Licenca;
            Swift = kf.Swift;
            EmriBanka = kf.EmriBanka;
            AdresaBanka = kf.AdresaBanka;
            Grupim1KF = kf.Grupim1KF;
            Grupim2KF = kf.Grupim2KF;
            Grupim3KF = kf.Grupim3KF;
            NrTVSH = kf.NrTVSH;
            _pershkrimTitulliKf = kf._pershkrimTitulliKf;
            _kodBanke = kf._kodBanke;
            Idgrupim1kf = kf.Idgrupim1kf;
            Idgrupim2kf = kf.Idgrupim2kf;
            Idgrupim3kf = kf.Idgrupim3kf;
            Gjendja = kf.Gjendja;
            IdObjektivaKosto = kf.IdObjektivaKosto;
            Objektiva = kf.Objektiva;
            IdKrijuesi = kf.IdKrijuesi;
            Krijuesi = kf.Krijuesi;
            GjendjaMonBaze = kf.GjendjaMonBaze;
            IdNdermarjeBij = kf.IdNdermarjeBij;
            LlojPorosie = kf.LlojPorosie;
            _oArkiva = kf._oArkiva;
            Kupon = kf.Kupon;
            Koordinata = kf.Koordinata;
            KlientSpecifik = kf.KlientSpecifik;
            Fermer = kf.Fermer;
            AutoNgarkese = kf.AutoNgarkese;
            ShitjePaTvsh = kf.ShitjePaTvsh;
            PerqindjeAgjenti = kf.PerqindjeAgjenti;
            PerqindjeAgjenti2 = kf.PerqindjeAgjenti2;
            Prospekt = kf.Prospekt;
            KodiMobile = kf.KodiMobile;
            EmailPerPajisje = kf.EmailPerPajisje;
            _colMarreveshjet = kf._colMarreveshjet;
            HfArkiva = kf.HfArkiva;
            Idklientfurnitorkryesor = kf.Idklientfurnitorkryesor;
            DteDatelindjaKF = kf.DteDatelindjaKF;
            IdPerfaqesuesShitje3 = kf.IdPerfaqesuesShitje3;
            IdTvsh = kf.IdTvsh;
            EmertimFature = kf.EmertimFature;
            LlogaritKomision = kf.LlogaritKomision;
            KodIntegrimi = kf.KodIntegrimi;
            ImbLogger.LogTraceShitje("Mbaroi metoda mbush klientfurnitor");
        }

        /// <summary>
        /// mbush klient furnitor nga databaza
        /// </summary>
        /// <param name="dbDataRowKlientFurnitor">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal clsMesazh MbushKlientFurnitor(DataRow dbDataRowKlientFurnitor)
        {
            if (dbDataRowKlientFurnitor != null)
            {
                ImbLogger.LogTraceShitje("Filloi mbushja e KlientFurnitor nga DB-ja");
                try
                {
                    IdKlientFurnitor = !IsDBNull(dbDataRowKlientFurnitor["IDKLIENTFURNITOR"])
                       ? ToInt32(dbDataRowKlientFurnitor["IDKLIENTFURNITOR"])
                       : 0;

                    KodKlientFurnitor = dbDataRowKlientFurnitor["KODKLIENTFURNITOR"].ToString();
                    NrLlogKlientFurnitor = dbDataRowKlientFurnitor["NRLLOGARI"].ToString();

                    IdLlogari = !IsDBNull(dbDataRowKlientFurnitor["IDLLOGARI"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDLLOGARI"])
                        : 0;
                    LlojiKF = !IsDBNull(dbDataRowKlientFurnitor["LLOJIKF"]) &&
                          ToBoolean(dbDataRowKlientFurnitor["LLOJIKF"]);
                    TitulliKF = !IsDBNull(dbDataRowKlientFurnitor["TITULLIKF"])
                        ? ToInt32(dbDataRowKlientFurnitor["TITULLIKF"])
                        : 0;
                    AktivitetiKF = dbDataRowKlientFurnitor["AKTIVITETIKF"].ToString();
                    EmertimiKF = dbDataRowKlientFurnitor["EMERTIMIKF"].ToString();
                    EmerKerkimiKF = dbDataRowKlientFurnitor["EMERKERKRIMIKF"].ToString();
                    NiptiKF = dbDataRowKlientFurnitor["NIPTKF"].ToString();

                    QytetiKF = !IsDBNull(dbDataRowKlientFurnitor["QYTETIKF"])
                        ? ToInt32(dbDataRowKlientFurnitor["QYTETIKF"])
                        : 0;
                    ShtetiKF = dbDataRowKlientFurnitor["SHTETIKF"].ToString();
                    TelKF = dbDataRowKlientFurnitor["TELKF"].ToString();
                    FaxKF = dbDataRowKlientFurnitor["FAXKF"].ToString();
                    CelKF = dbDataRowKlientFurnitor["CELKF"].ToString();
                    EmailKF = dbDataRowKlientFurnitor["EMAILKF"].ToString();
                    WebPageKF = dbDataRowKlientFurnitor["WEBPAGEKF"].ToString();
                    IBANKF = dbDataRowKlientFurnitor["IBANKF"].ToString();
                    LlogariBankareKF = dbDataRowKlientFurnitor["LLOGARIBANKAREKF"].ToString();

                    AktivKF = !IsDBNull(dbDataRowKlientFurnitor["AKTIVKF"]) &&
                          ToBoolean(dbDataRowKlientFurnitor["AKTIVKF"]);
                    IdLlogZbritje = !IsDBNull(dbDataRowKlientFurnitor["IDLLOGZBRITJE"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDLLOGZBRITJE"])
                        : 0;
                    IdLlogariDytesore = !IsDBNull(dbDataRowKlientFurnitor["IDLLOGARIDYTESORE"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDLLOGARIDYTESORE"])
                        : 0;
                    NrLlogDytesor = dbDataRowKlientFurnitor["NRLLOGDYTESOR"].ToString();

                    IdKushtePagese = !IsDBNull(dbDataRowKlientFurnitor["IDKUSHTEPAGESE"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDKUSHTEPAGESE"])
                        : 0;
                    IdMetoda = !IsDBNull(dbDataRowKlientFurnitor["IDMETODA"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDMETODA"])
                        : 0;
                    MaturimiKF = !IsDBNull(dbDataRowKlientFurnitor["MATURIMIKF"])
                        ? ToInt32(dbDataRowKlientFurnitor["MATURIMIKF"])
                        : 0;
                    IdKatZbritje = !IsDBNull(dbDataRowKlientFurnitor["IDKATZBRITJE"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDKATZBRITJE"])
                        : 0;
                    LimitParalajmerues = !IsDBNull(dbDataRowKlientFurnitor["LIMITPARALAJMERUES"])
                        ? ToInt32(dbDataRowKlientFurnitor["LIMITPARALAJMERUES"])
                        : 0;
                    LimitBllokues = !IsDBNull(dbDataRowKlientFurnitor["LIMITBLLOKUES"])
                        ? ToInt32(dbDataRowKlientFurnitor["LIMITBLLOKUES"])
                        : 0;
                    IdKategoriKlienti = !IsDBNull(dbDataRowKlientFurnitor["IDKATEGORIKLIENTI"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDKATEGORIKLIENTI"])
                        : 0;
                    KushteDergimi = dbDataRowKlientFurnitor["KUSHTEDERGIMI"].ToString();
                    MenyraTransportit = dbDataRowKlientFurnitor["MENYRATRASPORTIT"].ToString();

                    OfertaAutomatike = !IsDBNull(dbDataRowKlientFurnitor["OFERTAAUTOMATIKE"]) && ToBoolean(dbDataRowKlientFurnitor["OFERTAAUTOMATIKE"]);
                    KlientSpecifik = !IsDBNull(dbDataRowKlientFurnitor["KLIENTSPECIFIK"]) && ToBoolean(dbDataRowKlientFurnitor["KLIENTSPECIFIK"]);
                    Fermer = !IsDBNull(dbDataRowKlientFurnitor["FERMER"]) && ToBoolean(dbDataRowKlientFurnitor["FERMER"]);
                    AutoNgarkese = !IsDBNull(dbDataRowKlientFurnitor["AUTONGARKESE"]) && ToBoolean(dbDataRowKlientFurnitor["AUTONGARKESE"]);

                    ShitjePaTvsh = !IsDBNull(dbDataRowKlientFurnitor["SHITJEPATVSH"]) && ToBoolean(dbDataRowKlientFurnitor["SHITJEPATVSH"]);

                    VleraLimitPorositur = !IsDBNull(dbDataRowKlientFurnitor["VLERALIMITPOROSITUR"])
                        ? ToDecimal(dbDataRowKlientFurnitor["VLERALIMITPOROSITUR"])
                        : 0;
                    Prioriteti = !IsDBNull(dbDataRowKlientFurnitor["PRIORITETI"])
                        ? ToInt32(dbDataRowKlientFurnitor["PRIORITETI"])
                        : 0;
                    CmimUlet = !IsDBNull(dbDataRowKlientFurnitor["CMIMULET"])
                        ? ToDecimal(dbDataRowKlientFurnitor["CMIMULET"])
                        : 0;
                    IdNivelCmimi = !IsDBNull(dbDataRowKlientFurnitor["IDNIVELCMIMI"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDNIVELCMIMI"])
                        : 0;
                    IdPerfaqesuesShitje = !IsDBNull(dbDataRowKlientFurnitor["IDPERFAQESUESSHITJE"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDPERFAQESUESSHITJE"])
                        : 0;
                    IdQenderKosto = !IsDBNull(dbDataRowKlientFurnitor["IDQENDERKOSTO"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDQENDERKOSTO"])
                        : 0;
                    IdFushata = !IsDBNull(dbDataRowKlientFurnitor["IDFUSHATA"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDFUSHATA"])
                        : 0;
                    ZbritjeAnalitike = !IsDBNull(dbDataRowKlientFurnitor["ZBRITJEANALITIKE"])
                        ? ToInt32(dbDataRowKlientFurnitor["ZBRITJEANALITIKE"])
                        : 0;
                    ZbritjeTotal = !IsDBNull(dbDataRowKlientFurnitor["ZBRITJETOTAL"])
                        ? ToDecimal(dbDataRowKlientFurnitor["ZBRITJETOTAL"])
                        : 0;
                    IdNdermarja = !IsDBNull(dbDataRowKlientFurnitor["IDNDERMARJE"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDNDERMARJE"])
                        : 0;
                    Viti = !IsDBNull(dbDataRowKlientFurnitor["VITI"])
                        ? ToInt32(dbDataRowKlientFurnitor["VITI"])
                        : 0;
                    IdPerdoruesi = !IsDBNull(dbDataRowKlientFurnitor["IDPERDORUESI"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDPERDORUESI"])
                        : 0;
                    IdKonfig = !IsDBNull(dbDataRowKlientFurnitor["IDKONFIG"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDKONFIG"])
                        : 0;
                    IdStatusDok = !IsDBNull(dbDataRowKlientFurnitor["IDSTATUSDOK"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDSTATUSDOK"])
                        : 0;
                    DtKrijimi = !IsDBNull(dbDataRowKlientFurnitor["DTKRIJIMI"])
                        ? ToDateTime(dbDataRowKlientFurnitor["DTKRIJIMI"])
                        : DateTime.MinValue;
                    DtModifikimi = !IsDBNull(dbDataRowKlientFurnitor["DTMODIFIKIMI"])
                        ? ToDateTime(dbDataRowKlientFurnitor["DTMODIFIKIMI"])
                        : DateTime.MinValue;
                    NrLlogZbritje = dbDataRowKlientFurnitor["NRLLOGARIZBRITJE"].ToString();
                    KodKatZbritje = dbDataRowKlientFurnitor["KODKATEGORIZBRITJE"].ToString();
                    if (KodKatZbritje != "")
                        PerqindjeKatZbritje = !IsDBNull(dbDataRowKlientFurnitor["ZBRITJA"])
                            ? ToDecimal(dbDataRowKlientFurnitor["ZBRITJA"])
                            : 0;
                    PershkrimNivelCmimi = dbDataRowKlientFurnitor["PERSHKRIMNIVELCMIMI"].ToString();
                    KodKushtePagese = dbDataRowKlientFurnitor["KODIKUSHTPAGESE"].ToString();
                    KodPerfaqesuesShitje = dbDataRowKlientFurnitor["KODIAGJENTSHITJE"].ToString();
                    PershkrimNivelZbritje = dbDataRowKlientFurnitor["PERSHKRIMNIVELZBRITJE"].ToString();
                    EmriQytetitKF = dbDataRowKlientFurnitor["QYTETIEMRI"].ToString();
                    Licenca = dbDataRowKlientFurnitor["LICENCA"].ToString();
                    Swift = dbDataRowKlientFurnitor["SWIFT"].ToString();
                    EmriBanka = !IsDBNull(dbDataRowKlientFurnitor["EMRIBANKA"])
                        ? ToInt32(dbDataRowKlientFurnitor["EMRIBANKA"])
                        : 0;
                    AdresaBanka = dbDataRowKlientFurnitor["ADRESABANKA"].ToString();
                    Grupim1KF = dbDataRowKlientFurnitor["GRUPIM1KF"].ToString();
                    Grupim2KF = dbDataRowKlientFurnitor["GRUPIM2KF"].ToString();
                    Grupim3KF = dbDataRowKlientFurnitor["GRUPIM3KF"].ToString();
                    if (dbDataRowKlientFurnitor.Table.Columns.Contains("IDGRUPIM1KF"))
                        Idgrupim1kf = !IsDBNull(dbDataRowKlientFurnitor["IDGRUPIM1KF"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDGRUPIM1KF"])
                        :0;
                    if (dbDataRowKlientFurnitor.Table.Columns.Contains("IDGRUPIM2KF"))
                        Idgrupim2kf = !IsDBNull(dbDataRowKlientFurnitor["IDGRUPIM2KF"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDGRUPIM2KF"])
                        :0;
                    if (dbDataRowKlientFurnitor.Table.Columns.Contains("IDGRUPIM3KF"))
                        Idgrupim3kf = !IsDBNull(dbDataRowKlientFurnitor["IDGRUPIM3KF"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDGRUPIM3KF"])
                        :0;
                    IdMetoda = !IsDBNull(dbDataRowKlientFurnitor["IDMETODA"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDMETODA"])
                        : 0;
                    NrTVSH = dbDataRowKlientFurnitor["NRTVSH"].ToString();
                    IdObjektivaKosto = !IsDBNull(dbDataRowKlientFurnitor["IDOBJEKTIVAKOSTO"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDOBJEKTIVAKOSTO"])
                        : 0;
                    Objektiva = dbDataRowKlientFurnitor["OBJEKTIVA"].ToString();
                    IdKrijuesi = !IsDBNull(dbDataRowKlientFurnitor["IDKRIJUESI"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDKRIJUESI"])
                        : 0;
                    Krijuesi = dbDataRowKlientFurnitor["KRIJUESI"].ToString();
                    IdNdermarjeBij = !IsDBNull(dbDataRowKlientFurnitor["IDNDERMARJEBIJ"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDNDERMARJEBIJ"])
                        : 0;
                    LlojPorosie = !IsDBNull(dbDataRowKlientFurnitor["LLOJPOROSIE"])
                        ? ToInt32(dbDataRowKlientFurnitor["LLOJPOROSIE"])
                        : 0;
                    Kupon = !IsDBNull(dbDataRowKlientFurnitor["KUPON"]) && ToBoolean(dbDataRowKlientFurnitor["KUPON"]);

                    Prospekt = !IsDBNull(dbDataRowKlientFurnitor["PROSPEKT"]) && ToBoolean(dbDataRowKlientFurnitor["PROSPEKT"]);
                    Koordinata = dbDataRowKlientFurnitor["KOORDINATA"].ToString();
                    IdPerfaqesuesShitje2 = !IsDBNull(dbDataRowKlientFurnitor["IDPERFAQESUESSHITJE2"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDPERFAQESUESSHITJE2"])
                        : 0;
                    KodPerfaqesuesShitje2 = dbDataRowKlientFurnitor["KODIAGJENTSHITJE2"].ToString();
                    PerqindjeAgjenti = !IsDBNull(dbDataRowKlientFurnitor["PERQINDJEAGJENTI"])
                        ? ToDecimal(dbDataRowKlientFurnitor["PERQINDJEAGJENTI"])
                        : 0;
                    PerqindjeAgjenti2 = !IsDBNull(dbDataRowKlientFurnitor["PERQINDJEAGJENTI2"])
                        ? ToDecimal(dbDataRowKlientFurnitor["PERQINDJEAGJENTI2"])
                        : 0;

                    EmailPerPajisje = dbDataRowKlientFurnitor["EMAILPERPAJISJE"].ToString();
                    Idklientfurnitorkryesor = !IsDBNull(dbDataRowKlientFurnitor["IDKLIENTFURNITORKRYESOR"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDKLIENTFURNITORKRYESOR"])
                        : 0;
                    Shenime = dbDataRowKlientFurnitor["Shenime"].ToString();

                    DteDatelindjaKF = !IsDBNull(dbDataRowKlientFurnitor["DteDatelindjaKF"])
                        ? ToDateTime(dbDataRowKlientFurnitor["DteDatelindjaKF"])
                        : DateTime.MinValue;
                    IdPerfaqesuesShitje3 = !IsDBNull(dbDataRowKlientFurnitor["IDPERFAQESUESSHITJE3"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDPERFAQESUESSHITJE3"])
                        : 0;
                    PerqindjeAgjenti3 = !IsDBNull(dbDataRowKlientFurnitor["PERQINDJEAGJENTI3"])
                        ? ToDecimal(dbDataRowKlientFurnitor["PERQINDJEAGJENTI3"])
                        : 0;

                    IdTvsh = !IsDBNull(dbDataRowKlientFurnitor["IDTVSH"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDTVSH"])
                        : 0;
                    EmertimFature = dbDataRowKlientFurnitor["EMERTIMFATURE"].ToString();
                    LlogaritKomision = !IsDBNull(dbDataRowKlientFurnitor["LLOGARITKOMISION"]) && ToBoolean(dbDataRowKlientFurnitor["LLOGARITKOMISION"]);
                    KodIntegrimi = dbDataRowKlientFurnitor["KodIntegrimi"].ToString();
                    KodiISKSH = dbDataRowKlientFurnitor["KodiISKSH"].ToString();
                    MeDogane = !IsDBNull(dbDataRowKlientFurnitor["MeDogane"]) && ToBoolean(dbDataRowKlientFurnitor["MeDogane"]);
                    if (dbDataRowKlientFurnitor.Table.Columns.Contains("TIPIID"))
                        TipiId = dbDataRowKlientFurnitor["TIPIID"].ToString();
                    switch (IdMetoda)
                    {
                        case 0:
                            _pershkrimMetoda = "me mirebesim";
                            break;
                        case 4:
                            _pershkrimMetoda = "pagese";
                            break;
                        case 5:
                            _pershkrimMetoda = "pagese automatike";
                            break;
                        case 6:
                            _pershkrimMetoda = "cash & bank";
                            break;
                        case 7:
                            _pershkrimMetoda = "me parapagim";
                            break;
                        case 8:
                            _pershkrimMetoda = "pezull";
                            break;
                        case 9:
                            _pershkrimMetoda = "arke";
                            break;
                        case 10:
                            _pershkrimMetoda = "karte krediti";
                            break;
                        case 11:
                            _pershkrimMetoda = "banke";
                            break;
                        default:
                            _pershkrimMetoda = "";
                            break;
                    }
                    if (dbDataRowKlientFurnitor.Table.Columns.Contains("ADRESA"))
                    {
                        if (OColAdresat == null)
                            OColAdresat = new colAdresatKlientFurnitor();

                        OColAdresat.Add(new clsAdresaKlientFurnitor
                        {
                            IdKlientFurnitor = IdKlientFurnitor,
                            Adresa = dbDataRowKlientFurnitor["ADRESA"].ToString()
                        });

                    }
                    ImbLogger.LogTraceShitje("Mbushja e klientfurnitorit u krye me sukses");
                    return new clsMesazh(true, "Mbushja e klientfurnitorit u krye me sukses");
                }
                catch (InvalidCastException)
                {
                    ImbLogger.LogErrorShitje("invalid cast exception triggered: Gabim gjate marrjes se klient furnitoreve nga db - ja");
                    throw new Exception("ERROR: Gabim gjate marrjes se klient furnitoreve nga db-ja");
                }
            }
            ImbLogger.LogWarningShitje("Mbushja e KlientFurnitor nuk u krye sepse nuk u morr asgje nga db-ja");
            return new clsMesazh(false, "Mbushja nuk u krye sepse nuk u morr asgje nga db-ja");
        }
        internal clsMesazh MbushKlientFurnitorLinear(DataRow dbDataRowKlientFurnitor)
        {
            if (dbDataRowKlientFurnitor != null)
            {
                ImbLogger.LogTraceShitje("Filloi mbushja e KlientFurnitor nga DB-ja");
                try
                {
                    IdKlientFurnitor = !IsDBNull(dbDataRowKlientFurnitor["IDKLIENTFURNITOR"])
                       ? ToInt32(dbDataRowKlientFurnitor["IDKLIENTFURNITOR"])
                       : 0;

                    KodKlientFurnitor = dbDataRowKlientFurnitor["KODKLIENTFURNITOR"].ToString();
                    EmertimiKF = dbDataRowKlientFurnitor["EMERTIMIKF"].ToString();
                    EmerKerkimiKF = dbDataRowKlientFurnitor["EMERKERKRIMIKF"].ToString();
                    ImbLogger.LogTraceShitje("Mbushja e klientfurnitorit u krye me sukses");
                    return new clsMesazh(true, "Mbushja e klientfurnitorit u krye me sukses");
                }
                catch (InvalidCastException)
                {
                    ImbLogger.LogErrorShitje("invalid cast exception triggered: Gabim gjate marrjes se klient furnitoreve nga db - ja");
                    throw new Exception("ERROR: Gabim gjate marrjes se klient furnitoreve nga db-ja");
                }
            }
            ImbLogger.LogWarningShitje("Mbushja e KlientFurnitor nuk u krye sepse nuk u morr asgje nga db-ja");
            return new clsMesazh(false, "Mbushja nuk u krye sepse nuk u morr asgje nga db-ja");
        }

        internal void MbushKlientFurnitor(IDataRecord dbDataRowKlientFurnitor)
        {
            if (dbDataRowKlientFurnitor != null)
            {
                ImbLogger.LogTraceShitje("Filloi mbushja e KlientFurnitor nga DB-ja");
                try
                {
                    IdKlientFurnitor = !IsDBNull(dbDataRowKlientFurnitor["IDKLIENTFURNITOR"])
                       ? ToInt32(dbDataRowKlientFurnitor["IDKLIENTFURNITOR"])
                       : 0;

                    KodKlientFurnitor = dbDataRowKlientFurnitor["KODKLIENTFURNITOR"].ToString();
                    NrLlogKlientFurnitor = dbDataRowKlientFurnitor["NRLLOGARI"].ToString();

                    IdLlogari = !IsDBNull(dbDataRowKlientFurnitor["IDLLOGARI"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDLLOGARI"])
                        : 0;
                    LlojiKF = !IsDBNull(dbDataRowKlientFurnitor["LLOJIKF"]) &&
                          ToBoolean(dbDataRowKlientFurnitor["LLOJIKF"]);
                    TitulliKF = !IsDBNull(dbDataRowKlientFurnitor["TITULLIKF"])
                        ? ToInt32(dbDataRowKlientFurnitor["TITULLIKF"])
                        : 0;
                    AktivitetiKF = dbDataRowKlientFurnitor["AKTIVITETIKF"].ToString();
                    EmertimiKF = dbDataRowKlientFurnitor["EMERTIMIKF"].ToString();
                    EmerKerkimiKF = dbDataRowKlientFurnitor["EMERKERKRIMIKF"].ToString();
                    NiptiKF = dbDataRowKlientFurnitor["NIPTKF"].ToString();

                    QytetiKF = !IsDBNull(dbDataRowKlientFurnitor["QYTETIKF"])
                        ? ToInt32(dbDataRowKlientFurnitor["QYTETIKF"])
                        : 0;
                    ShtetiKF = dbDataRowKlientFurnitor["SHTETIKF"].ToString();
                    TelKF = dbDataRowKlientFurnitor["TELKF"].ToString();
                    FaxKF = dbDataRowKlientFurnitor["FAXKF"].ToString();
                    CelKF = dbDataRowKlientFurnitor["CELKF"].ToString();
                    EmailKF = dbDataRowKlientFurnitor["EMAILKF"].ToString();
                    WebPageKF = dbDataRowKlientFurnitor["WEBPAGEKF"].ToString();
                    IBANKF = dbDataRowKlientFurnitor["IBANKF"].ToString();
                    LlogariBankareKF = dbDataRowKlientFurnitor["LLOGARIBANKAREKF"].ToString();

                    AktivKF = !IsDBNull(dbDataRowKlientFurnitor["AKTIVKF"]) &&
                          ToBoolean(dbDataRowKlientFurnitor["AKTIVKF"]);
                    IdLlogZbritje = !IsDBNull(dbDataRowKlientFurnitor["IDLLOGZBRITJE"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDLLOGZBRITJE"])
                        : 0;
                    IdLlogariDytesore = !IsDBNull(dbDataRowKlientFurnitor["IDLLOGARIDYTESORE"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDLLOGARIDYTESORE"])
                        : 0;
                    NrLlogDytesor = dbDataRowKlientFurnitor["NRLLOGDYTESOR"].ToString();

                    IdKushtePagese = !IsDBNull(dbDataRowKlientFurnitor["IDKUSHTEPAGESE"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDKUSHTEPAGESE"])
                        : 0;
                    IdMetoda = !IsDBNull(dbDataRowKlientFurnitor["IDMETODA"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDMETODA"])
                        : 0;
                    MaturimiKF = !IsDBNull(dbDataRowKlientFurnitor["MATURIMIKF"])
                        ? ToInt32(dbDataRowKlientFurnitor["MATURIMIKF"])
                        : 0;
                    IdKatZbritje = !IsDBNull(dbDataRowKlientFurnitor["IDKATZBRITJE"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDKATZBRITJE"])
                        : 0;
                    LimitParalajmerues = !IsDBNull(dbDataRowKlientFurnitor["LIMITPARALAJMERUES"])
                        ? ToInt32(dbDataRowKlientFurnitor["LIMITPARALAJMERUES"])
                        : 0;
                    LimitBllokues = !IsDBNull(dbDataRowKlientFurnitor["LIMITBLLOKUES"])
                        ? ToInt32(dbDataRowKlientFurnitor["LIMITBLLOKUES"])
                        : 0;
                    IdKategoriKlienti = !IsDBNull(dbDataRowKlientFurnitor["IDKATEGORIKLIENTI"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDKATEGORIKLIENTI"])
                        : 0;
                    KushteDergimi = dbDataRowKlientFurnitor["KUSHTEDERGIMI"].ToString();
                    MenyraTransportit = dbDataRowKlientFurnitor["MENYRATRASPORTIT"].ToString();

                    OfertaAutomatike = !IsDBNull(dbDataRowKlientFurnitor["OFERTAAUTOMATIKE"]) && ToBoolean(dbDataRowKlientFurnitor["OFERTAAUTOMATIKE"]);
                    KlientSpecifik = !IsDBNull(dbDataRowKlientFurnitor["KLIENTSPECIFIK"]) && ToBoolean(dbDataRowKlientFurnitor["KLIENTSPECIFIK"]);
                    Fermer = !IsDBNull(dbDataRowKlientFurnitor["FERMER"]) && ToBoolean(dbDataRowKlientFurnitor["FERMER"]);
                    AutoNgarkese = !IsDBNull(dbDataRowKlientFurnitor["AUTONGARKESE"]) && ToBoolean(dbDataRowKlientFurnitor["AUTONGARKESE"]);

                    ShitjePaTvsh = !IsDBNull(dbDataRowKlientFurnitor["SHITJEPATVSH"]) && ToBoolean(dbDataRowKlientFurnitor["SHITJEPATVSH"]);

                    VleraLimitPorositur = !IsDBNull(dbDataRowKlientFurnitor["VLERALIMITPOROSITUR"])
                        ? ToDecimal(dbDataRowKlientFurnitor["VLERALIMITPOROSITUR"])
                        : 0;
                    Prioriteti = !IsDBNull(dbDataRowKlientFurnitor["PRIORITETI"])
                        ? ToInt32(dbDataRowKlientFurnitor["PRIORITETI"])
                        : 0;
                    CmimUlet = !IsDBNull(dbDataRowKlientFurnitor["CMIMULET"])
                        ? ToDecimal(dbDataRowKlientFurnitor["CMIMULET"])
                        : 0;
                    IdNivelCmimi = !IsDBNull(dbDataRowKlientFurnitor["IDNIVELCMIMI"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDNIVELCMIMI"])
                        : 0;
                    IdPerfaqesuesShitje = !IsDBNull(dbDataRowKlientFurnitor["IDPERFAQESUESSHITJE"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDPERFAQESUESSHITJE"])
                        : 0;
                    IdQenderKosto = !IsDBNull(dbDataRowKlientFurnitor["IDQENDERKOSTO"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDQENDERKOSTO"])
                        : 0;
                    IdFushata = !IsDBNull(dbDataRowKlientFurnitor["IDFUSHATA"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDFUSHATA"])
                        : 0;
                    ZbritjeAnalitike = !IsDBNull(dbDataRowKlientFurnitor["ZBRITJEANALITIKE"])
                        ? ToInt32(dbDataRowKlientFurnitor["ZBRITJEANALITIKE"])
                        : 0;
                    ZbritjeTotal = !IsDBNull(dbDataRowKlientFurnitor["ZBRITJETOTAL"])
                        ? ToDecimal(dbDataRowKlientFurnitor["ZBRITJETOTAL"])
                        : 0;
                    IdNdermarja = !IsDBNull(dbDataRowKlientFurnitor["IDNDERMARJE"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDNDERMARJE"])
                        : 0;
                    Viti = !IsDBNull(dbDataRowKlientFurnitor["VITI"])
                        ? ToInt32(dbDataRowKlientFurnitor["VITI"])
                        : 0;
                    IdPerdoruesi = !IsDBNull(dbDataRowKlientFurnitor["IDPERDORUESI"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDPERDORUESI"])
                        : 0;
                    IdKonfig = !IsDBNull(dbDataRowKlientFurnitor["IDKONFIG"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDKONFIG"])
                        : 0;
                    IdStatusDok = !IsDBNull(dbDataRowKlientFurnitor["IDSTATUSDOK"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDSTATUSDOK"])
                        : 0;
                    DtKrijimi = !IsDBNull(dbDataRowKlientFurnitor["DTKRIJIMI"])
                        ? ToDateTime(dbDataRowKlientFurnitor["DTKRIJIMI"])
                        : DateTime.MinValue;
                    DtModifikimi = !IsDBNull(dbDataRowKlientFurnitor["DTMODIFIKIMI"])
                        ? ToDateTime(dbDataRowKlientFurnitor["DTMODIFIKIMI"])
                        : DateTime.MinValue;
                    NrLlogZbritje = dbDataRowKlientFurnitor["NRLLOGARIZBRITJE"].ToString();
                    KodKatZbritje = dbDataRowKlientFurnitor["KODKATEGORIZBRITJE"].ToString();
                    if (KodKatZbritje != "")
                        PerqindjeKatZbritje = !IsDBNull(dbDataRowKlientFurnitor["ZBRITJA"])
                            ? ToDecimal(dbDataRowKlientFurnitor["ZBRITJA"])
                            : 0;
                    PershkrimNivelCmimi = dbDataRowKlientFurnitor["PERSHKRIMNIVELCMIMI"].ToString();
                    KodKushtePagese = dbDataRowKlientFurnitor["KODIKUSHTPAGESE"].ToString();
                    KodPerfaqesuesShitje = dbDataRowKlientFurnitor["KODIAGJENTSHITJE"].ToString();
                    PershkrimNivelZbritje = dbDataRowKlientFurnitor["PERSHKRIMNIVELZBRITJE"].ToString();
                    EmriQytetitKF = dbDataRowKlientFurnitor["QYTETIEMRI"].ToString();
                    Licenca = dbDataRowKlientFurnitor["LICENCA"].ToString();
                    Swift = dbDataRowKlientFurnitor["SWIFT"].ToString();
                    EmriBanka = !IsDBNull(dbDataRowKlientFurnitor["EMRIBANKA"])
                        ? ToInt32(dbDataRowKlientFurnitor["EMRIBANKA"])
                        : 0;
                    AdresaBanka = dbDataRowKlientFurnitor["ADRESABANKA"].ToString();
                    Grupim1KF = dbDataRowKlientFurnitor["GRUPIM1KF"].ToString();
                    Grupim2KF = dbDataRowKlientFurnitor["GRUPIM2KF"].ToString();
                    Grupim3KF = dbDataRowKlientFurnitor["GRUPIM3KF"].ToString();
                    NrTVSH = dbDataRowKlientFurnitor["NRTVSH"].ToString();
                    IdObjektivaKosto = !IsDBNull(dbDataRowKlientFurnitor["IDOBJEKTIVAKOSTO"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDOBJEKTIVAKOSTO"])
                        : 0;
                    Objektiva = dbDataRowKlientFurnitor["OBJEKTIVA"].ToString();
                    IdKrijuesi = !IsDBNull(dbDataRowKlientFurnitor["IDKRIJUESI"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDKRIJUESI"])
                        : 0;
                    Krijuesi = dbDataRowKlientFurnitor["KRIJUESI"].ToString();
                    IdNdermarjeBij = !IsDBNull(dbDataRowKlientFurnitor["IDNDERMARJEBIJ"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDNDERMARJEBIJ"])
                        : 0;
                    LlojPorosie = !IsDBNull(dbDataRowKlientFurnitor["LLOJPOROSIE"])
                        ? ToInt32(dbDataRowKlientFurnitor["LLOJPOROSIE"])
                        : 0;
                    Kupon = !IsDBNull(dbDataRowKlientFurnitor["KUPON"]) && ToBoolean(dbDataRowKlientFurnitor["KUPON"]);

                    Prospekt = !IsDBNull(dbDataRowKlientFurnitor["PROSPEKT"]) && ToBoolean(dbDataRowKlientFurnitor["PROSPEKT"]);
                    Koordinata = dbDataRowKlientFurnitor["KOORDINATA"].ToString();
                    IdPerfaqesuesShitje2 = !IsDBNull(dbDataRowKlientFurnitor["IDPERFAQESUESSHITJE2"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDPERFAQESUESSHITJE2"])
                        : 0;
                    KodPerfaqesuesShitje2 = dbDataRowKlientFurnitor["KODIAGJENTSHITJE2"].ToString();
                    PerqindjeAgjenti = !IsDBNull(dbDataRowKlientFurnitor["PERQINDJEAGJENTI"])
                        ? ToDecimal(dbDataRowKlientFurnitor["PERQINDJEAGJENTI"])
                        : 0;
                    PerqindjeAgjenti2 = !IsDBNull(dbDataRowKlientFurnitor["PERQINDJEAGJENTI2"])
                        ? ToDecimal(dbDataRowKlientFurnitor["PERQINDJEAGJENTI2"])
                        : 0;

                    EmailPerPajisje = dbDataRowKlientFurnitor["EMAILPERPAJISJE"].ToString();
                    Idklientfurnitorkryesor = !IsDBNull(dbDataRowKlientFurnitor["IDKLIENTFURNITORKRYESOR"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDKLIENTFURNITORKRYESOR"])
                        : 0;
                    Shenime = dbDataRowKlientFurnitor["Shenime"].ToString();

                    DteDatelindjaKF = !IsDBNull(dbDataRowKlientFurnitor["DteDatelindjaKF"])
                        ? ToDateTime(dbDataRowKlientFurnitor["DteDatelindjaKF"])
                        : DateTime.MinValue;
                    IdPerfaqesuesShitje3 = !IsDBNull(dbDataRowKlientFurnitor["IDPERFAQESUESSHITJE3"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDPERFAQESUESSHITJE3"])
                        : 0;
                    PerqindjeAgjenti3 = !IsDBNull(dbDataRowKlientFurnitor["PERQINDJEAGJENTI3"])
                        ? ToDecimal(dbDataRowKlientFurnitor["PERQINDJEAGJENTI3"])
                        : 0;

                    IdTvsh = !IsDBNull(dbDataRowKlientFurnitor["IDTVSH"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDTVSH"])
                        : 0;
                    EmertimFature = dbDataRowKlientFurnitor["EMERTIMFATURE"].ToString();
                    LlogaritKomision = !IsDBNull(dbDataRowKlientFurnitor["LLOGARITKOMISION"]) && ToBoolean(dbDataRowKlientFurnitor["LLOGARITKOMISION"]);
                    KodIntegrimi = dbDataRowKlientFurnitor["KodIntegrimi"].ToString();
                    KodiISKSH = dbDataRowKlientFurnitor["KodiISKSH"].ToString();
                    MeDogane = !IsDBNull(dbDataRowKlientFurnitor["MeDogane"]) && ToBoolean(dbDataRowKlientFurnitor["MeDogane"]);
                    ImbLogger.LogTraceShitje("Mbushja e klientfurnitorit u krye me sukses");
                }
                catch (InvalidCastException)
                {
                    ImbLogger.LogErrorShitje("invalid cast exception triggered: Gabim gjate marrjes se klient furnitoreve nga db - ja");
                    throw new Exception("ERROR: Gabim gjate marrjes se klient furnitoreve nga db-ja");
                }
            }

            ImbLogger.LogWarningShitje("Mbushja e KlientFurnitor nuk u krye sepse nuk u morr asgje nga db-ja");
        }

        internal void MbushKlientFurnitorAzhornim(DataRow dbDataRowKlientFurnitor)
        {
            if (dbDataRowKlientFurnitor == null)
                throw new Exception("Mbushja nuk u krye sepse nuk u morr asgje nga db-ja");
            try
            {
                IdKlientFurnitor = !IsDBNull(dbDataRowKlientFurnitor["IDKLIENTFURNITOR"])
                       ? ToInt32(dbDataRowKlientFurnitor["IDKLIENTFURNITOR"])
                       : 0;
                KodKlientFurnitor = dbDataRowKlientFurnitor["KODKLIENTFURNITOR"].ToString();
                NrLlogKlientFurnitor = dbDataRowKlientFurnitor["NRLLOGARI"].ToString();
                IdLlogari = !IsDBNull(dbDataRowKlientFurnitor["IDLLOGARI"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDLLOGARI"])
                        : 0;
                LlojiKF = !IsDBNull(dbDataRowKlientFurnitor["LLOJIKF"]) &&
                          ToBoolean(dbDataRowKlientFurnitor["LLOJIKF"]);
                TitulliKF = !IsDBNull(dbDataRowKlientFurnitor["TITULLIKF"])
                        ? ToInt32(dbDataRowKlientFurnitor["TITULLIKF"])
                        : 0;
                AktivitetiKF = dbDataRowKlientFurnitor["AKTIVITETIKF"].ToString();
                EmertimiKF = dbDataRowKlientFurnitor["EMERTIMIKF"].ToString();
                EmerKerkimiKF = dbDataRowKlientFurnitor["EMERKERKRIMIKF"].ToString();
                NiptiKF = dbDataRowKlientFurnitor["NIPTKF"].ToString();
                QytetiKF = !IsDBNull(dbDataRowKlientFurnitor["QYTETIKF"])
                        ? ToInt32(dbDataRowKlientFurnitor["QYTETIKF"])
                        : 0;
                ShtetiKF = dbDataRowKlientFurnitor["SHTETIKF"].ToString();
                TelKF = dbDataRowKlientFurnitor["TELKF"].ToString();
                FaxKF = dbDataRowKlientFurnitor["FAXKF"].ToString();
                CelKF = dbDataRowKlientFurnitor["CELKF"].ToString();
                EmailKF = dbDataRowKlientFurnitor["EMAILKF"].ToString();
                KlientSpecifik = !IsDBNull(dbDataRowKlientFurnitor["KLIENTSPECIFIK"]) && ToBoolean(dbDataRowKlientFurnitor["KLIENTSPECIFIK"]);
                Fermer = !IsDBNull(dbDataRowKlientFurnitor["FERMER"]) && ToBoolean(dbDataRowKlientFurnitor["FERMER"]);
                AutoNgarkese = !IsDBNull(dbDataRowKlientFurnitor["AUTONGARKESE"]) && ToBoolean(dbDataRowKlientFurnitor["AUTONGARKESE"]);
                ShitjePaTvsh = !IsDBNull(dbDataRowKlientFurnitor["SHITJEPATVSH"]) && ToBoolean(dbDataRowKlientFurnitor["SHITJEPATVSH"]);
                WebPageKF = dbDataRowKlientFurnitor["WEBPAGEKF"].ToString();
                IBANKF = dbDataRowKlientFurnitor["IBANKF"].ToString();
                LlogariBankareKF = dbDataRowKlientFurnitor["LLOGARIBANKAREKF"].ToString();
                AktivKF = !IsDBNull(dbDataRowKlientFurnitor["AKTIVKF"]) &&
                          ToBoolean(dbDataRowKlientFurnitor["AKTIVKF"]);
                IdLlogZbritje = !IsDBNull(dbDataRowKlientFurnitor["IDLLOGZBRITJE"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDLLOGZBRITJE"])
                        : 0;
                IdLlogariDytesore = !IsDBNull(dbDataRowKlientFurnitor["IDLLOGARIDYTESORE"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDLLOGARIDYTESORE"])
                        : 0;
                IdKushtePagese = !IsDBNull(dbDataRowKlientFurnitor["IDKUSHTEPAGESE"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDKUSHTEPAGESE"])
                        : 0;
                IdMetoda = !IsDBNull(dbDataRowKlientFurnitor["IDMETODA"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDMETODA"])
                        : 0;
                MaturimiKF = !IsDBNull(dbDataRowKlientFurnitor["MATURIMIKF"])
                        ? ToInt32(dbDataRowKlientFurnitor["MATURIMIKF"])
                        : 0;
                IdKatZbritje = !IsDBNull(dbDataRowKlientFurnitor["IDKATZBRITJE"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDKATZBRITJE"])
                        : 0;
                LimitParalajmerues = !IsDBNull(dbDataRowKlientFurnitor["LIMITPARALAJMERUES"])
                        ? ToInt32(dbDataRowKlientFurnitor["LIMITPARALAJMERUES"])
                        : 0;
                LimitBllokues = !IsDBNull(dbDataRowKlientFurnitor["LIMITBLLOKUES"])
                        ? ToInt32(dbDataRowKlientFurnitor["LIMITBLLOKUES"])
                        : 0;
                IdKategoriKlienti = !IsDBNull(dbDataRowKlientFurnitor["IDKATEGORIKLIENTI"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDKATEGORIKLIENTI"])
                        : 0;
                KushteDergimi = dbDataRowKlientFurnitor["KUSHTEDERGIMI"].ToString();
                MenyraTransportit = dbDataRowKlientFurnitor["MENYRATRASPORTIT"].ToString();
                OfertaAutomatike = !IsDBNull(dbDataRowKlientFurnitor["OFERTAAUTOMATIKE"]) && ToBoolean(dbDataRowKlientFurnitor["OFERTAAUTOMATIKE"]);
                VleraLimitPorositur = !IsDBNull(dbDataRowKlientFurnitor["VLERALIMITPOROSITUR"])
                        ? ToDecimal(dbDataRowKlientFurnitor["VLERALIMITPOROSITUR"])
                        : 0;
                Prioriteti = !IsDBNull(dbDataRowKlientFurnitor["PRIORITETI"])
                        ? ToInt32(dbDataRowKlientFurnitor["PRIORITETI"])
                        : 0;
                CmimUlet = !IsDBNull(dbDataRowKlientFurnitor["CMIMULET"])
                        ? ToDecimal(dbDataRowKlientFurnitor["CMIMULET"])
                        : 0;
                IdNivelCmimi = !IsDBNull(dbDataRowKlientFurnitor["IDNIVELCMIMI"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDNIVELCMIMI"])
                        : 0;
                IdPerfaqesuesShitje = !IsDBNull(dbDataRowKlientFurnitor["IDPERFAQESUESSHITJE"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDPERFAQESUESSHITJE"])
                        : 0;
                IdQenderKosto = !IsDBNull(dbDataRowKlientFurnitor["IDQENDERKOSTO"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDQENDERKOSTO"])
                        : 0;
                IdFushata = !IsDBNull(dbDataRowKlientFurnitor["IDFUSHATA"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDFUSHATA"])
                        : 0;
                ZbritjeAnalitike = !IsDBNull(dbDataRowKlientFurnitor["ZBRITJEANALITIKE"])
                        ? ToInt32(dbDataRowKlientFurnitor["ZBRITJEANALITIKE"])
                        : 0;
                ZbritjeTotal = !IsDBNull(dbDataRowKlientFurnitor["ZBRITJETOTAL"])
                        ? ToDecimal(dbDataRowKlientFurnitor["ZBRITJETOTAL"])
                        : 0;
                IdNdermarja = !IsDBNull(dbDataRowKlientFurnitor["IDNDERMARJE"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDNDERMARJE"])
                        : 0;
                Viti = !IsDBNull(dbDataRowKlientFurnitor["VITI"])
                        ? ToInt32(dbDataRowKlientFurnitor["VITI"])
                        : 0;
                IdPerdoruesi = !IsDBNull(dbDataRowKlientFurnitor["IDPERDORUESI"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDPERDORUESI"])
                        : 0;
                IdKonfig = !IsDBNull(dbDataRowKlientFurnitor["IDKONFIG"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDKONFIG"])
                        : 0;
                IdStatusDok = !IsDBNull(dbDataRowKlientFurnitor["IDSTATUSDOK"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDSTATUSDOK"])
                        : 0;
                DtKrijimi = !IsDBNull(dbDataRowKlientFurnitor["DTKRIJIMI"])
                        ? ToDateTime(dbDataRowKlientFurnitor["DTKRIJIMI"])
                        : DateTime.MinValue;
                DtModifikimi = !IsDBNull(dbDataRowKlientFurnitor["DTMODIFIKIMI"])
                    ? ToDateTime(dbDataRowKlientFurnitor["DTMODIFIKIMI"])
                    : DateTime.MinValue;
                NrLlogZbritje = dbDataRowKlientFurnitor["NRLLOGARIZBRITJE"].ToString();
                KodKatZbritje = dbDataRowKlientFurnitor["KODKATEGORIZBRITJE"].ToString();
                if (KodKatZbritje != "")
                    PerqindjeKatZbritje = !IsDBNull(dbDataRowKlientFurnitor["ZBRITJA"])
                            ? ToDecimal(dbDataRowKlientFurnitor["ZBRITJA"])
                            : 0;
                PershkrimNivelCmimi = dbDataRowKlientFurnitor["PERSHKRIMNIVELCMIMI"].ToString();
                KodKushtePagese = dbDataRowKlientFurnitor["KODIKUSHTPAGESE"].ToString();
                KodPerfaqesuesShitje = dbDataRowKlientFurnitor["KODIAGJENTSHITJE"].ToString();
                PershkrimNivelZbritje = dbDataRowKlientFurnitor["PERSHKRIMNIVELZBRITJE"].ToString();
                EmriQytetitKF = dbDataRowKlientFurnitor["QYTETIEMRI"].ToString();
                DtAzhornimi = !IsDBNull(dbDataRowKlientFurnitor["dtAzhornimi"])
                    ? ToDateTime(dbDataRowKlientFurnitor["dtAzhornimi"])
                    : DateTime.MinValue;
                DtLidhje = !IsDBNull(dbDataRowKlientFurnitor["dtLidhje"])
                    ? ToDateTime(dbDataRowKlientFurnitor["dtLidhje"])
                    : DateTime.MinValue;
                Gjendja = !IsDBNull(dbDataRowKlientFurnitor["gjendja"])
                    ? ToDecimal(dbDataRowKlientFurnitor["gjendja"])
                    : 0;
                Licenca = dbDataRowKlientFurnitor["LICENCA"].ToString();
                Swift = dbDataRowKlientFurnitor["SWIFT"].ToString();
                EmriBanka = !IsDBNull(dbDataRowKlientFurnitor["EMRIBANKA"])
                        ? ToInt32(dbDataRowKlientFurnitor["EMRIBANKA"])
                        : 0;
                AdresaBanka = dbDataRowKlientFurnitor["ADRESABANKA"].ToString();
                Grupim1KF = dbDataRowKlientFurnitor["GRUPIM1KF"].ToString();
                Grupim2KF = dbDataRowKlientFurnitor["GRUPIM2KF"].ToString();
                Grupim3KF = dbDataRowKlientFurnitor["GRUPIM3KF"].ToString();
                NrTVSH = dbDataRowKlientFurnitor["NRTVSH"].ToString();
                IdObjektivaKosto = !IsDBNull(dbDataRowKlientFurnitor["IDOBJEKTIVAKOSTO"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDOBJEKTIVAKOSTO"])
                        : 0;
                Objektiva = dbDataRowKlientFurnitor["OBJEKTIVA"].ToString();
                IdKrijuesi = !IsDBNull(dbDataRowKlientFurnitor["IDKRIJUESI"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDKRIJUESI"])
                        : 0;
                Krijuesi = dbDataRowKlientFurnitor["KRIJUESI"].ToString();
                IdNdermarjeBij = !IsDBNull(dbDataRowKlientFurnitor["IDNDERMARJEBIJ"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDNDERMARJEBIJ"])
                        : 0;
                Kupon = !IsDBNull(dbDataRowKlientFurnitor["KUPON"]) && ToBoolean(dbDataRowKlientFurnitor["KUPON"]);
                Koordinata = dbDataRowKlientFurnitor["KOORDINATA"].ToString();
                IdPerfaqesuesShitje2 = !IsDBNull(dbDataRowKlientFurnitor["IDPERFAQESUESSHITJE2"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDPERFAQESUESSHITJE2"])
                        : 0;
                KodPerfaqesuesShitje2 = dbDataRowKlientFurnitor["KODIAGJENTSHITJE2"].ToString();
                PerqindjeAgjenti = !IsDBNull(dbDataRowKlientFurnitor["PERQINDJEAGJENTI"])
                        ? ToDecimal(dbDataRowKlientFurnitor["PERQINDJEAGJENTI"])
                        : 0;
                PerqindjeAgjenti2 = !IsDBNull(dbDataRowKlientFurnitor["PERQINDJEAGJENTI2"])
                        ? ToDecimal(dbDataRowKlientFurnitor["PERQINDJEAGJENTI2"])
                        : 0;
                Prospekt = !IsDBNull(dbDataRowKlientFurnitor["PROSPEKT"]) && ToBoolean(dbDataRowKlientFurnitor["PROSPEKT"]);
                EmailPerPajisje = dbDataRowKlientFurnitor["EMAILPERPAJISJE"].ToString();
                Idklientfurnitorkryesor = !IsDBNull(dbDataRowKlientFurnitor["IDKLIENTFURNITORKRYESOR"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDKLIENTFURNITORKRYESOR"])
                        : 0;
                DteDatelindjaKF = !IsDBNull(dbDataRowKlientFurnitor["DteDatelindjaKF"])
                        ? ToDateTime(dbDataRowKlientFurnitor["DteDatelindjaKF"])
                        : DateTime.MinValue;
                IdPerfaqesuesShitje3 = !IsDBNull(dbDataRowKlientFurnitor["IDPERFAQESUESSHITJE3"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDPERFAQESUESSHITJE3"])
                        : 0;
                PerqindjeAgjenti3 = !IsDBNull(dbDataRowKlientFurnitor["PERQINDJEAGJENTI3"])
                        ? ToDecimal(dbDataRowKlientFurnitor["PERQINDJEAGJENTI3"])
                        : 0;
                IdTvsh = !IsDBNull(dbDataRowKlientFurnitor["IDTVSH"])
                        ? ToInt32(dbDataRowKlientFurnitor["IDTVSH"])
                        : 0;
                EmertimFature = dbDataRowKlientFurnitor["EMERTIMFATURE"].ToString();
                LlogaritKomision = !IsDBNull(dbDataRowKlientFurnitor["LLOGARITKOMISION"]) && ToBoolean(dbDataRowKlientFurnitor["LLOGARITKOMISION"]);
                KodIntegrimi = dbDataRowKlientFurnitor["KodIntegrimi"].ToString();
            }
            catch (InvalidCastException)
            {
                throw new Exception("ERROR: Gabim gjate marrjes se klient furnitoreve nga db-ja");
            }
        }

        internal void MbushKlientFurnitorAzhornimGjendje(DataRow dbDataRowKlientFurnitor)
        {
            if (dbDataRowKlientFurnitor == null)
                throw new Exception("Mbushja nuk u krye sepse nuk u morr asgje nga db - ja");
            try
            {
                IdKlientFurnitor = !IsDBNull(dbDataRowKlientFurnitor["IDKLIENTFURNITOR"])
                    ? ToInt32(dbDataRowKlientFurnitor["IDKLIENTFURNITOR"])
                    : 0;
                KodKlientFurnitor = dbDataRowKlientFurnitor["KODKLIENTFURNITOR"].ToString();
                NrLlogKlientFurnitor = dbDataRowKlientFurnitor["NRLLOGARI"].ToString();
                IdLlogari = !IsDBNull(dbDataRowKlientFurnitor["IDLLOGARI"])
                    ? ToInt32(dbDataRowKlientFurnitor["IDLLOGARI"])
                    : 0;
                LlojiKF = !IsDBNull(dbDataRowKlientFurnitor["LLOJIKF"]) &&
                          ToBoolean(dbDataRowKlientFurnitor["LLOJIKF"]);
                TitulliKF = !IsDBNull(dbDataRowKlientFurnitor["TITULLIKF"])
                    ? ToInt32(dbDataRowKlientFurnitor["TITULLIKF"])
                    : 0;
                AktivitetiKF = dbDataRowKlientFurnitor["AKTIVITETIKF"].ToString();
                EmertimiKF = dbDataRowKlientFurnitor["EMERTIMIKF"].ToString();
                EmerKerkimiKF = dbDataRowKlientFurnitor["EMERKERKRIMIKF"].ToString();
                NiptiKF = dbDataRowKlientFurnitor["NIPTKF"].ToString();
                QytetiKF = !IsDBNull(dbDataRowKlientFurnitor["QYTETIKF"])
                    ? ToInt32(dbDataRowKlientFurnitor["QYTETIKF"])
                    : 0;
                ShtetiKF = dbDataRowKlientFurnitor["SHTETIKF"].ToString();
                TelKF = dbDataRowKlientFurnitor["TELKF"].ToString();
                FaxKF = dbDataRowKlientFurnitor["FAXKF"].ToString();
                CelKF = dbDataRowKlientFurnitor["CELKF"].ToString();
                EmailKF = dbDataRowKlientFurnitor["EMAILKF"].ToString();
                WebPageKF = dbDataRowKlientFurnitor["WEBPAGEKF"].ToString();
                IBANKF = dbDataRowKlientFurnitor["IBANKF"].ToString();
                LlogariBankareKF = dbDataRowKlientFurnitor["LLOGARIBANKAREKF"].ToString();
                AktivKF = !IsDBNull(dbDataRowKlientFurnitor["AKTIVKF"]) &&
                          ToBoolean(dbDataRowKlientFurnitor["AKTIVKF"]);
                IdLlogZbritje = !IsDBNull(dbDataRowKlientFurnitor["IDLLOGZBRITJE"])
                    ? ToInt32(dbDataRowKlientFurnitor["IDLLOGZBRITJE"])
                    : 0;
                IdLlogariDytesore = !IsDBNull(dbDataRowKlientFurnitor["IDLLOGARIDYTESORE"])
                    ? ToInt32(dbDataRowKlientFurnitor["IDLLOGARIDYTESORE"])
                    : 0;
                IdKushtePagese = !IsDBNull(dbDataRowKlientFurnitor["IDKUSHTEPAGESE"])
                    ? ToInt32(dbDataRowKlientFurnitor["IDKUSHTEPAGESE"])
                    : 0;
                IdMetoda = !IsDBNull(dbDataRowKlientFurnitor["IDMETODA"])
                    ? ToInt32(dbDataRowKlientFurnitor["IDMETODA"])
                    : 0;
                MaturimiKF = !IsDBNull(dbDataRowKlientFurnitor["MATURIMIKF"])
                    ? ToInt32(dbDataRowKlientFurnitor["MATURIMIKF"])
                    : 0;
                IdKatZbritje = !IsDBNull(dbDataRowKlientFurnitor["IDKATZBRITJE"])
                    ? ToInt32(dbDataRowKlientFurnitor["IDKATZBRITJE"])
                    : 0;
                LimitParalajmerues = !IsDBNull(dbDataRowKlientFurnitor["LIMITPARALAJMERUES"])
                    ? ToInt32(dbDataRowKlientFurnitor["LIMITPARALAJMERUES"])
                    : 0;
                LimitBllokues = !IsDBNull(dbDataRowKlientFurnitor["LIMITBLLOKUES"])
                    ? ToInt32(dbDataRowKlientFurnitor["LIMITBLLOKUES"])
                    : 0;
                IdKategoriKlienti = !IsDBNull(dbDataRowKlientFurnitor["IDKATEGORIKLIENTI"])
                    ? ToInt32(dbDataRowKlientFurnitor["IDKATEGORIKLIENTI"])
                    : 0;
                KushteDergimi = dbDataRowKlientFurnitor["KUSHTEDERGIMI"].ToString();
                MenyraTransportit = dbDataRowKlientFurnitor["MENYRATRASPORTIT"].ToString();
                OfertaAutomatike = !IsDBNull(dbDataRowKlientFurnitor["OFERTAAUTOMATIKE"]) && ToBoolean(dbDataRowKlientFurnitor["OFERTAAUTOMATIKE"]);

                VleraLimitPorositur = !IsDBNull(dbDataRowKlientFurnitor["VLERALIMITPOROSITUR"])
                    ? ToDecimal(dbDataRowKlientFurnitor["VLERALIMITPOROSITUR"])
                    : 0;
                Prioriteti = !IsDBNull(dbDataRowKlientFurnitor["PRIORITETI"])
                    ? ToInt32(dbDataRowKlientFurnitor["PRIORITETI"])
                    : 0;
                CmimUlet = !IsDBNull(dbDataRowKlientFurnitor["CMIMULET"])
                    ? ToDecimal(dbDataRowKlientFurnitor["CMIMULET"])
                    : 0;
                IdNivelCmimi = !IsDBNull(dbDataRowKlientFurnitor["IDNIVELCMIMI"])
                    ? ToInt32(dbDataRowKlientFurnitor["IDNIVELCMIMI"])
                    : 0;
                IdPerfaqesuesShitje = !IsDBNull(dbDataRowKlientFurnitor["IDPERFAQESUESSHITJE"])
                    ? ToInt32(dbDataRowKlientFurnitor["IDPERFAQESUESSHITJE"])
                    : 0;
                IdQenderKosto = !IsDBNull(dbDataRowKlientFurnitor["IDQENDERKOSTO"])
                    ? ToInt32(dbDataRowKlientFurnitor["IDQENDERKOSTO"])
                    : 0;
                IdFushata = !IsDBNull(dbDataRowKlientFurnitor["IDFUSHATA"])
                    ? ToInt32(dbDataRowKlientFurnitor["IDFUSHATA"])
                    : 0;
                ZbritjeAnalitike = !IsDBNull(dbDataRowKlientFurnitor["ZBRITJEANALITIKE"])
                    ? ToInt32(dbDataRowKlientFurnitor["ZBRITJEANALITIKE"])
                    : 0;
                ZbritjeTotal = !IsDBNull(dbDataRowKlientFurnitor["ZBRITJETOTAL"])
                    ? ToDecimal(dbDataRowKlientFurnitor["ZBRITJETOTAL"])
                    : 0;
                IdNdermarja = !IsDBNull(dbDataRowKlientFurnitor["IDNDERMARJE"])
                    ? ToInt32(dbDataRowKlientFurnitor["IDNDERMARJE"])
                    : 0;
                Viti = !IsDBNull(dbDataRowKlientFurnitor["VITI"])
                    ? ToInt32(dbDataRowKlientFurnitor["VITI"])
                    : 0;
                IdPerdoruesi = !IsDBNull(dbDataRowKlientFurnitor["IDPERDORUESI"])
                    ? ToInt32(dbDataRowKlientFurnitor["IDPERDORUESI"])
                    : 0;
                IdKonfig = !IsDBNull(dbDataRowKlientFurnitor["IDKONFIG"])
                    ? ToInt32(dbDataRowKlientFurnitor["IDKONFIG"])
                    : 0;
                IdStatusDok = !IsDBNull(dbDataRowKlientFurnitor["IDSTATUSDOK"])
                    ? ToInt32(dbDataRowKlientFurnitor["IDSTATUSDOK"])
                    : 0;
                DtKrijimi = !IsDBNull(dbDataRowKlientFurnitor["DTKRIJIMI"])
                    ? ToDateTime(dbDataRowKlientFurnitor["DTKRIJIMI"])
                    : DateTime.MinValue;
                DtModifikimi = !IsDBNull(dbDataRowKlientFurnitor["DTMODIFIKIMI"])
                    ? ToDateTime(dbDataRowKlientFurnitor["DTMODIFIKIMI"])
                    : DateTime.MinValue;
                NrLlogZbritje = dbDataRowKlientFurnitor["NRLLOGARIZBRITJE"].ToString();
                KodKatZbritje = dbDataRowKlientFurnitor["KODKATEGORIZBRITJE"].ToString();
                if (KodKatZbritje != "")
                    PerqindjeKatZbritje = !IsDBNull(dbDataRowKlientFurnitor["ZBRITJA"])
                        ? ToDecimal(dbDataRowKlientFurnitor["ZBRITJA"])
                        : 0;
                PershkrimNivelCmimi = dbDataRowKlientFurnitor["PERSHKRIMNIVELCMIMI"].ToString();
                KodKushtePagese = dbDataRowKlientFurnitor["KODIKUSHTPAGESE"].ToString();
                KodPerfaqesuesShitje = dbDataRowKlientFurnitor["KODIAGJENTSHITJE"].ToString();
                PershkrimNivelZbritje = dbDataRowKlientFurnitor["PERSHKRIMNIVELZBRITJE"].ToString();
                EmriQytetitKF = dbDataRowKlientFurnitor["QYTETIEMRI"].ToString();

                DtAzhornimi = !IsDBNull(dbDataRowKlientFurnitor["dtAzhornimi"])
                    ? ToDateTime(dbDataRowKlientFurnitor["dtAzhornimi"])
                    : DateTime.MinValue;
                DtLidhje = !IsDBNull(dbDataRowKlientFurnitor["dtLidhje"])
                    ? ToDateTime(dbDataRowKlientFurnitor["dtLidhje"])
                    : DateTime.MinValue;
                Gjendja = !IsDBNull(dbDataRowKlientFurnitor["gjendja"])
                    ? ToDecimal(dbDataRowKlientFurnitor["gjendja"])
                    : 0;

                Licenca = dbDataRowKlientFurnitor["LICENCA"].ToString();
                Swift = dbDataRowKlientFurnitor["SWIFT"].ToString();
                EmriBanka = !IsDBNull(dbDataRowKlientFurnitor["EMRIBANKA"])
                    ? ToInt32(dbDataRowKlientFurnitor["EMRIBANKA"])
                    : 0;
                AdresaBanka = dbDataRowKlientFurnitor["ADRESABANKA"].ToString();
                Grupim1KF = dbDataRowKlientFurnitor["GRUPIM1KF"].ToString();
                Grupim2KF = dbDataRowKlientFurnitor["GRUPIM2KF"].ToString();
                Grupim3KF = dbDataRowKlientFurnitor["GRUPIM3KF"].ToString();
                NrTVSH = dbDataRowKlientFurnitor["NRTVSH"].ToString();
                KlientSpecifik = !IsDBNull(dbDataRowKlientFurnitor["KLIENTSPECIFIK"]) && ToBoolean(dbDataRowKlientFurnitor["KLIENTSPECIFIK"]);
                Fermer = !IsDBNull(dbDataRowKlientFurnitor["FERMER"]) && ToBoolean(dbDataRowKlientFurnitor["FERMER"]);
                AutoNgarkese = !IsDBNull(dbDataRowKlientFurnitor["AUTONGARKESE"]) && ToBoolean(dbDataRowKlientFurnitor["AUTONGARKESE"]);
                IdObjektivaKosto = !IsDBNull(dbDataRowKlientFurnitor["IDOBJEKTIVAKOSTO"])
                    ? ToInt32(dbDataRowKlientFurnitor["IDOBJEKTIVAKOSTO"])
                    : 0;
                Objektiva = dbDataRowKlientFurnitor["OBJEKTIVA"].ToString();
                IdKrijuesi = !IsDBNull(dbDataRowKlientFurnitor["IDKRIJUESI"])
                    ? ToInt32(dbDataRowKlientFurnitor["IDKRIJUESI"])
                    : 0;
                Krijuesi = dbDataRowKlientFurnitor["KRIJUESI"].ToString();
                GjendjaMonBaze = !IsDBNull(dbDataRowKlientFurnitor["gjendjaMonBaze"])
                    ? ToDecimal(dbDataRowKlientFurnitor["gjendjaMonBaze"])
                    : 0;
                IdNdermarjeBij = !IsDBNull(dbDataRowKlientFurnitor["IDNDERMARJEBIJ"])
                    ? ToInt32(dbDataRowKlientFurnitor["IDNDERMARJEBIJ"])
                    : 0;
                Koordinata = dbDataRowKlientFurnitor["KOORDINATA"].ToString();
                IdPerfaqesuesShitje2 = !IsDBNull(dbDataRowKlientFurnitor["IDPERFAQESUESSHITJE2"])
                    ? ToInt32(dbDataRowKlientFurnitor["IDPERFAQESUESSHITJE2"])
                    : 0;
                KodPerfaqesuesShitje2 = dbDataRowKlientFurnitor["KODIAGJENTSHITJE2"].ToString();
                Prospekt = !IsDBNull(dbDataRowKlientFurnitor["PROSPEKT"]) && ToBoolean(dbDataRowKlientFurnitor["PROSPEKT"]);
                EmailPerPajisje = dbDataRowKlientFurnitor["EMAILPERPAJISJE"].ToString();
                Idklientfurnitorkryesor = !IsDBNull(dbDataRowKlientFurnitor["IDKLIENTFURNITORKRYESOR"])
                    ? ToInt32(dbDataRowKlientFurnitor["IDKLIENTFURNITORKRYESOR"])
                    : 0;
                DteDatelindjaKF = !IsDBNull(dbDataRowKlientFurnitor["DteDatelindjaKF"])
                    ? ToDateTime(dbDataRowKlientFurnitor["DteDatelindjaKF"])
                    : DateTime.MinValue;
                IdPerfaqesuesShitje3 = !IsDBNull(dbDataRowKlientFurnitor["IDPERFAQESUESSHITJE3"])
                    ? ToInt32(dbDataRowKlientFurnitor["IDPERFAQESUESSHITJE3"])
                    : 0;
                IdTvsh = !IsDBNull(dbDataRowKlientFurnitor["IDTVSH"])
                    ? ToInt32(dbDataRowKlientFurnitor["IDTVSH"])
                    : 0;
                EmertimFature = dbDataRowKlientFurnitor["EMERTIMFATURE"].ToString();
                LlogaritKomision = !IsDBNull(dbDataRowKlientFurnitor["LLOGARITKOMISION"]) && ToBoolean(dbDataRowKlientFurnitor["LLOGARITKOMISION"]);
                KodIntegrimi = dbDataRowKlientFurnitor["KodIntegrimi"].ToString();
                KodiISKSH = dbDataRowKlientFurnitor["KodiISKSH"].ToString();
                MeDogane = !IsDBNull(dbDataRowKlientFurnitor["MeDogane"]) && ToBoolean(dbDataRowKlientFurnitor["MeDogane"]);

            }
            catch (InvalidCastException)
            {
                ImbLogger.LogErrorShitje("INVALID CAST EXCEPTION TRIGGERED: Gabim gjate marrjes se klient furnitoreve nga db-ja");
                throw new Exception("ERROR: Gabim gjate marrjes se klient furnitoreve nga db-ja");
            }
        }

        internal static string KtheEmail(int idKlientFurnitor)
        {
            using (var dbKont = new clsDatabaseKontabilitet())
                return dbKont.ktheEmail(idKlientFurnitor);
        }

        #endregion
    }
}