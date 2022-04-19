using DbCore.DbAdmin;
using DbCore.DbInventari;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.DbRegjistrim
{
    public abstract class clsKasaKoka
    {
        #region Atribute

        private string llojiKases;
        private string module;
        private string idTransaksioni;
        private int idshop;
        private bool printimMeIp;
        private string advertisement;
        private string nrFature;
        private string menyrePagese;
        private bool fatureTatimore;
        private double zbritjeTotale;
        private string kodNdermarrja;
        private string pathWebService;
        private int idUser;
        private bool printoKodArtikulli;
        private bool printoBarKod;
        private bool kthim;
        private bool meShifraDhjetore;
        private bool meTVSH;
        private double kursi;
        private bool eshtePrinterFiskal;
        private bool kopjeFature;
        private bool printimManual;

        private List<clsKasaTrupi> trupi;

        #endregion

        #region Properties

        public string LlojiKases
        {
            get { return llojiKases; }
        }

        public string Module
        {
            get { return module; }
        }

        public string IdTransaksioni
        {
            get { return idTransaksioni; }
        }

        public int Idshop
        {
            get { return idshop; }
        }

        public bool PrintimMeIp
        {
            get { return printimMeIp; }
        }

        public string Advertisement
        {
            get { return advertisement; }
        }

        public string NrFature
        {
            get { return nrFature; }
        }

        public string MenyrePagese
        {
            get { return menyrePagese; }
        }

        public bool FatureTatimore
        {
            get { return fatureTatimore; }
        }

        public double ZbritjeTotale
        {
            get { return zbritjeTotale; }
        }

        public List<clsKasaTrupi> Trupi
        {
            get { return trupi; }
            set { trupi = value; }
        }

        public string KodNdermarrja
        {
            get { return kodNdermarrja; }
        }

        public string PathWebService
        {
            get { return pathWebService; }
        }

        public int IdUser
        {
            get { return idUser; }
        }

        public bool PrintoKodArtikulli
        {
            get { return printoKodArtikulli; }
        }

        public bool PrintoBarKod
        {
            get { return printoBarKod; }
        }

        public bool Kthim
        {
            get { return kthim; }
        }

        public bool MeShifraDhjetore
        {
            get { return meShifraDhjetore; }
        }

        public bool MeTVSH
        {
            get { return meTVSH; }
        }

        public double Kursi
        {
            get { return kursi; }
        }
        public bool EshtePrinterFiskal
        {
            get { return eshtePrinterFiskal; }
        }

        public bool KopjeFature
        {
            get { return kopjeFature; }
        }
        public bool PrintimManual
        {
            get { return printimManual; }
        }

        #endregion

        #region Konstruktor

        public clsKasaKoka()
        {

        }

        public clsKasaKoka(string llojiKases, string kompania, string idTransaksioni, bool printimMeIp, string menyrePagese, bool fatureTatimore, string kodNdermarrja, string pathWebService, int idUser, bool printoKodArtikulli, bool printoBarKod, bool kthim, bool meShifraDhjetore, bool meTVSH, double kursi)
        {
            this.llojiKases = llojiKases;
            this.module = kompania;
            this.idTransaksioni = idTransaksioni;
            this.printimMeIp = printimMeIp;
            this.menyrePagese = menyrePagese;
            this.fatureTatimore = fatureTatimore;
            this.kodNdermarrja = kodNdermarrja;
            this.pathWebService = pathWebService;
            this.idUser = idUser;
            this.printoKodArtikulli = printoKodArtikulli;
            this.printoBarKod = printoBarKod;
            this.kthim = kthim;
            this.meShifraDhjetore = meShifraDhjetore;
            this.meTVSH = meTVSH;
            this.kursi = kursi;
        }

        public clsKasaKoka(string llojiKases, string kompania, string idTransaksioni, bool printimMeIp, string menyrePagese, bool fatureTatimore, double zbritjeTotale, string kodNdermarrja, string pathWebService, int idUser, bool printoKodArtikulli, bool printoBarKod, bool kthim, bool meShifraDhjetore, bool meTVSH, double kursi)
        {
            this.llojiKases = llojiKases;
            this.module = kompania;
            this.idTransaksioni = idTransaksioni;
            this.printimMeIp = printimMeIp;
            this.menyrePagese = menyrePagese;
            this.fatureTatimore = fatureTatimore;
            this.zbritjeTotale = zbritjeTotale;
            this.kodNdermarrja = kodNdermarrja;
            this.pathWebService = pathWebService;
            this.idUser = idUser;
            this.printoKodArtikulli = printoKodArtikulli;
            this.printoBarKod = printoBarKod;
            this.kthim = kthim;
            this.meShifraDhjetore = meShifraDhjetore;
            this.meTVSH = meTVSH;
            this.kursi = kursi;
        }

        public clsKasaKoka(string llojiKases, string kompania, string idTransaksioni, bool printimMeIp, string nrFature, string menyrePagese, bool fatureTatimore, string kodNdermarrja, string pathWebService, int idUser, bool printoKodArtikulli, bool printoBarKod, bool kthim, bool meShifraDhjetore, bool meTVSH, double kursi)
        {
            this.llojiKases = llojiKases;
            this.module = kompania;
            this.idTransaksioni = idTransaksioni;
            this.printimMeIp = printimMeIp;
            this.nrFature = nrFature;
            this.menyrePagese = menyrePagese;
            this.fatureTatimore = fatureTatimore;
            this.kodNdermarrja = kodNdermarrja;
            this.pathWebService = pathWebService;
            this.idUser = idUser;
            this.printoKodArtikulli = printoKodArtikulli;
            this.printoBarKod = printoBarKod;
            this.kthim = kthim;
            this.meShifraDhjetore = meShifraDhjetore;
            this.meTVSH = meTVSH;
            this.kursi = kursi;
        }

        public clsKasaKoka(string llojiKases, string kompania, string idTransaksioni, bool printimMeIp, string nrFature, string menyrePagese, bool fatureTatimore, double zbritjeTotale, string kodNdermarrja, string pathWebService, int idUser, bool printoKodArtikulli, bool printoBarKod, bool kthim, bool meShifraDhjetore, bool meTVSH, double kursi)
        {
            this.llojiKases = llojiKases;
            this.module = kompania;
            this.idTransaksioni = idTransaksioni;
            this.printimMeIp = printimMeIp;
            this.nrFature = nrFature;
            this.menyrePagese = menyrePagese;
            this.fatureTatimore = fatureTatimore;
            this.zbritjeTotale = zbritjeTotale;
            this.kodNdermarrja = kodNdermarrja;
            this.pathWebService = pathWebService;
            this.idUser = idUser;
            this.printoKodArtikulli = printoKodArtikulli;
            this.printoBarKod = printoBarKod;
            this.kthim = kthim;
            this.meShifraDhjetore = meShifraDhjetore;
            this.meTVSH = meTVSH;
            this.kursi = kursi;
        }

        public clsKasaKoka(string llojiKases, string kompania, string idTransaksioni, int idshop, bool printimMeIp, string advertisement, string menyrePagese, bool fatureTatimore, string kodNdermarrja, string pathWebService, int idUser, bool printoKodArtikulli, bool printoBarKod, bool kthim, bool meShifraDhjetore, bool meTVSH, double kursi)
        {
            this.llojiKases = llojiKases;
            this.module = kompania;
            this.idTransaksioni = idTransaksioni;
            this.printimMeIp = printimMeIp;
            this.advertisement = advertisement;
            this.menyrePagese = menyrePagese;
            this.fatureTatimore = fatureTatimore;
            this.kodNdermarrja = kodNdermarrja;
            this.pathWebService = pathWebService;
            this.idUser = idUser;
            this.printoKodArtikulli = printoKodArtikulli;
            this.printoBarKod = printoBarKod;
            this.kthim = kthim;
            this.meShifraDhjetore = meShifraDhjetore;
            this.meTVSH = meTVSH;
            this.kursi = kursi;
            this.idshop = idshop;
        }

        public clsKasaKoka(string llojiKases, string kompania, string idTransaksioni, int idshop, bool printimMeIp, string advertisement, string menyrePagese, bool fatureTatimore, double zbritjeTotale, string kodNdermarrja, string pathWebService, int idUser, bool printoKodArtikulli, bool printoBarKod, bool kthim, bool meShifraDhjetore, bool meTVSH, double kursi)
        {
            this.llojiKases = llojiKases;
            this.module = kompania;
            this.idTransaksioni = idTransaksioni;
            this.printimMeIp = printimMeIp;
            this.advertisement = advertisement;
            this.menyrePagese = menyrePagese;
            this.fatureTatimore = fatureTatimore;
            this.zbritjeTotale = zbritjeTotale;
            this.kodNdermarrja = kodNdermarrja;
            this.pathWebService = pathWebService;
            this.idUser = idUser;
            this.printoKodArtikulli = printoKodArtikulli;
            this.printoBarKod = printoBarKod;
            this.kthim = kthim;
            this.meShifraDhjetore = meShifraDhjetore;
            this.meTVSH = meTVSH;
            this.kursi = kursi;
            this.idshop = idshop;
        }

        public clsKasaKoka(string llojiKases, string kompania, string idTransaksioni, int idshop, bool printimMeIp, string advertisement, string nrFature, string menyrePagese, bool fatureTatimore, string kodNdermarrja, string pathWebService, int idUser, bool printoKodArtikulli, bool printoBarKod, bool kthim, bool meShifraDhjetore, bool meTVSH, double kursi)
        {
            this.llojiKases = llojiKases;
            this.module = kompania;
            this.idTransaksioni = idTransaksioni;
            this.printimMeIp = printimMeIp;
            this.advertisement = advertisement;
            this.nrFature = nrFature;
            this.menyrePagese = menyrePagese;
            this.fatureTatimore = fatureTatimore;
            this.kodNdermarrja = kodNdermarrja;
            this.pathWebService = pathWebService;
            this.idUser = idUser;
            this.printoKodArtikulli = printoKodArtikulli;
            this.printoBarKod = printoBarKod;
            this.kthim = kthim;
            this.meShifraDhjetore = meShifraDhjetore;
            this.meTVSH = meTVSH;
            this.kursi = kursi;
            this.idshop = idshop;
        }

        public clsKasaKoka(string llojiKases, string kompania, string idTransaksioni, int idshop, bool printimMeIp, string advertisement, string nrFature, string menyrePagese, bool fatureTatimore, double zbritjeTotale, string kodNdermarrja, string pathWebService, int idUser, bool printoKodArtikulli, bool printoBarKod, bool kthim, bool meShifraDhjetore, bool meTVSH, double kursi)
        {
            this.llojiKases = llojiKases;
            this.module = kompania;
            this.idTransaksioni = idTransaksioni;
            this.printimMeIp = printimMeIp;
            this.advertisement = advertisement;
            this.nrFature = nrFature;
            this.menyrePagese = menyrePagese;
            this.fatureTatimore = fatureTatimore;
            this.zbritjeTotale = zbritjeTotale;
            this.kodNdermarrja = kodNdermarrja;
            this.pathWebService = pathWebService;
            this.idUser = idUser;
            this.printoKodArtikulli = printoKodArtikulli;
            this.printoBarKod = printoBarKod;
            this.kthim = kthim;
            this.meShifraDhjetore = meShifraDhjetore;
            this.meTVSH = meTVSH;
            this.kursi = kursi;
            this.idshop = idshop;
        }

        public clsKasaKoka(string llojiKases, string module, string idTransaksioni, bool printimMeIp, string advertisement, string nrFature, string menyrePagese, bool fatureTatimore, double zbritjeTotale, string kodNdermarrja, string pathWebService, int idUser, bool printoKodArtikulli, bool printoBarKod, bool kthim, bool meShifraDhjetore, bool meTVSH, double kursi, bool kopjeFature, bool printimManual, bool eshtePrinterFiskal, int idShop)
        {
            this.llojiKases = llojiKases;
            this.module = module;
            this.idTransaksioni = idTransaksioni;
            this.printimMeIp = printimMeIp;
            this.advertisement = advertisement;
            this.nrFature = nrFature;
            this.menyrePagese = menyrePagese;
            this.fatureTatimore = fatureTatimore;
            this.zbritjeTotale = zbritjeTotale;
            this.kodNdermarrja = kodNdermarrja;
            this.pathWebService = pathWebService;
            this.idUser = idUser;
            this.printoKodArtikulli = printoKodArtikulli;
            this.printoBarKod = printoBarKod;
            this.kthim = kthim;
            this.meShifraDhjetore = meShifraDhjetore;
            this.meTVSH = meTVSH;
            this.kursi = kursi;
            this.kopjeFature = kopjeFature;
            this.printimManual = printimManual;
            this.eshtePrinterFiskal = eshtePrinterFiskal;
            this.idshop = idShop;
        }

        #endregion

        #region Metoda Publike Abstrakte
        /// <summary>
        /// /
        /// </summary>
        /// <param name="ruajPergjigje"></param>
        /// <param name="derguar"></param>
        /// <param name="idShitje">nese eshte true veprimBanke atehere idShitje eshte ne fakt idkoka e arkes/bankes</param>
        /// <param name="veprimBanke">true nese eshte vep banke dhe false nese eshte shitje, aktualisht perdoret vetem per vodafone, i cili perdor webservice per kasen</param>
        /// <returns></returns>
        public abstract clsMesazh printoNeKase(bool ruajPergjigje, bool derguar, int idShitje, bool veprimBanke);

        #endregion

        #region Metoda Publike

        /// <summary>
        /// Krijon trupin nga objketi i colTrupiShitje
        /// </summary>
        /// <param name="artKase">Objekti i trupit te shitjes qe po behet.</param>
        /// <param name="idNdermarrje">Id e ndermarrjes</param>
        public void krijoKaseTrupi(string kodikasa, DbRegjistrim.colTrupiShitje artKase, int idNdermarrje, clsKonfigurimKase kasa, int plu, int idKonfigurimiKases)
        {
            trupi = new List<clsKasaTrupi>();
            // per bnt electronics duhet te dergohet edhe plu number.
            clsVleraKonfigurimiKasa vlKonf = new clsVleraKonfigurimiKasa();
            foreach (DbRegjistrim.clsTrupiShitje artikullShitje in artKase)
            {

                clsKasaTrupi artikulliKase = new clsKasaTrupi();
                var reparti = kasa.OColVlerat.ktheVlereTakse(artikullShitje.Tvsh);
                if (string.IsNullOrWhiteSpace(reparti))
                    throw new MyException("Reparti i TVSH per kasen nuk eshte konfiguruar!");
                else
                {
                    int departamenti = Convert.ToInt32(reparti);
                   
                    if (departamenti == 0)
                        departamenti = 1;
                    double tvsh = (double)clsTaksa.ktheNormePerqindjeMeId(artikullShitje.Tvsh);
                    string kodBari = string.Empty;
                    if (printoBarKod != false)
                    {
                        colKodbare kodBare = new colKodbare(artikullShitje.IdKodi);
                        foreach (clsKodbari kodBar in kodBare)
                        {
                            kodBari += kodBar.Pershkrimi + ",";
                        }
                    }
                    //vendosja e pershkrimit ne baze te cilit konfigurim eshte zgjedh ne kase// default pershkrimi i artikullit
                    string pershkrimArtikulli = clsFunksione.kthePershkrimArtikullPerKasen(1, PrintoKodArtikulli, artikullShitje.Kodi, bool.Parse(kasa.OColVlerat.ktheVlereOpsioni("PRINTOPERSHKRIM2")), artikullShitje.Pershkrimi, artikullShitje.Pershkrim2, 0);
                    trupi.Add(new clsKasaTrupi(departamenti, pershkrimArtikulli, double.Parse(artikullShitje.Sasia.ToString()), double.Parse(artikullShitje.Cmimi.ToString()), double.Parse(artikullShitje.Zbritje.ToString()), double.Parse(artikullShitje.VleftaMeTvsh.ToString()), artikullShitje.Pershkrimi, kodBari, plu, tvsh));
                    //Plu do  inkrementhet vetem ne rastin kur printohet nga BNTElectronics
                    if (kodikasa == "BNTElectronics")
                        plu++;
                }
            }
            vlKonf.updatePLU(idNdermarrje, plu, idKonfigurimiKases);
        }
        public void krijoKaseTrupi(DbArkaBanka.clsVeprimBankaKoka arkaBanka, int idNdermarrje)
        {
            trupi = new List<clsKasaTrupi>();
            string kodiTakses = System.Web.Configuration.WebConfigurationManager.AppSettings["reparti"];
            //Default 20% ==> 3
            int departamenti = 1;
            int.TryParse(kodiTakses, out departamenti);
            clsKasaTrupi artikulliKase = new clsKasaTrupi();
            trupi.Add(new clsKasaTrupi(departamenti, System.Web.Configuration.WebConfigurationManager.AppSettings["mesazhkasepagesa"], 1,
               float.Parse(arkaBanka.VleraMonedhaBaze.ToString()), double.Parse("0")));
        }

        public clsMesazh dergoKerkesePrintimi()
        {
            clsMesazh mesazh = new clsMesazh();

            WebRequest req;
            try
            {
                req = WebRequest.Create(PathWebService);
                req.ContentType = "application/json";
                req.Method = "POST";
                req.Headers["ndermarrjaserver"] = this.KodNdermarrja;
            }
            catch (Exception e)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(e.Message);
                mesazh = new clsMesazh(false, "Krijimi i kerkeses per service-in e kases nuk u krijua!");
                return mesazh;
            }

            var serialize = JsonConvert.SerializeObject(this);
            try
            {
                using (var writer = new StreamWriter(req.GetRequestStream()))
                {
                    writer.WriteLine(serialize);
                }
            }
            catch (Exception e)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(e.Message);
                mesazh = new clsMesazh(false, "Service i kases nuk eshte i ngritur ose duhet te download-oni programin e kases!");
                return mesazh;
            }

            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)req.GetResponse();
            }
            catch (WebException e)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(e.Message);
                mesazh = new clsMesazh(false, "Ndodhi nje gabim gjate marrjes se pergjigjes nga kasa!");
                return mesazh;
            }

            StreamReader streamReader = new StreamReader(response.GetResponseStream(), true);
            string body = streamReader.ReadToEnd();

            if ((body == "") || (body == "null"))
            {
                mesazh = new clsMesazh(false, "Trupi erdhi bosh nga serveri!");
                return mesazh;
            }
            else
            {
                var definition = new { StatusMesazhi = "" , PershkrimMesazhi="" };
                var vlerat = JsonConvert.DeserializeAnonymousType(body,definition);
                mesazh = new clsMesazh(bool.Parse(vlerat.StatusMesazhi), vlerat.PershkrimMesazhi);
                return mesazh;
            }
        }

        #endregion

        #region Metoda Private

        private static string kriposPass(string pass)
        {
            byte[] utf16Data = Encoding.Unicode.GetBytes(pass);
            char[] charPasUtf16 = System.Text.Encoding.ASCII.GetString(utf16Data).ToCharArray();

            HashAlgorithm hash = new SHA256Managed();
            byte[] plainTextBytes = System.Text.Encoding.UTF8.GetBytes(charPasUtf16);
            byte[] hashBytes = hash.ComputeHash(plainTextBytes);

            var hexString = BitConverter.ToString(hashBytes);
            hexString = hexString.Replace("-", "").ToLower();
            return hexString;

        }

        private static string EncodeTo64(string toEncode)
        {
            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);
            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }

        #endregion
    }
}
