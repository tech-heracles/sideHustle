using DbCore.DbAdmin;
using DbCore.DbArkaBanka;
using DbCore.DbInventari;
using DbCore.DbKontabiliteti;
using DbCore.DbRegjistrim;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Fiskalizimi.Controls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Xml;
using DbCore.IMBUtils.DataBase;
using DevExpress.Xpo;

namespace DbCore.IMBUtils.Fiskalizimi.API
{
    public static class clsFunksioneFiskalizimi
    {

        public static string GjeneroIIC(clsNdermarrje nderm, string txtNumer, string txtTotal1, string cashRegister, string softNum, DateTime dateKrijimi)
        {

            string DateServeriOffset = clsKontrollePerFiskalizimin.ktheDatenEServeritOffset();
            DateServeriOffset = DateServeriOffset.Replace("+02", "+00");
            DateTimeOffset dt = DateTimeOffset.Parse(DateServeriOffset);
            TimeSpan t = dt.Offset;
            DateTimeOffset sourceDate = new DateTimeOffset(dateKrijimi,
                         t);

            DateTimeOffset timezoneIShqiperise = TimeZoneInfo.ConvertTime(sourceDate,
                          TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time"));


            if (nderm.Pathname.ToString() == "")
                throw new Exception("Ju lutem ngarkoni certifikaten e sigurise!");

            string passpath = nderm.Pathname + $"/password.txt";
            String KEYSTORE_PASS = "";

            byte[] encrypted;
            String KEYSTORE_LOCATION = System.Web.Hosting.HostingEnvironment.MapPath(nderm.Pathname) + @"certifikata.p12";
            if (File.Exists(System.Web.Hosting.HostingEnvironment.MapPath(nderm.Pathname) + @"password.txt"))
                KEYSTORE_PASS = File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath(nderm.Pathname) + @"password.txt");
            else throw new Exception("Ju lutem ngarkoni filen e passwordit!");

            String iicInput = "";
            string iicString = "";
            // issuerNuis
            iicInput += "|" + nderm.NdermarrjeNipt;
            // dateTimeCreated
            iicInput += "|" + timezoneIShqiperise.ToString("yyyy-MM-ddTHH\\:mm\\:sszzz");
            // invoiceNumber
            iicInput += "|" + txtNumer;
            // busiUnit
            iicInput += "|" + nderm.Kodbiznesi;
            // cashRegister
            iicInput += "|" + cashRegister;
            // softNum
            iicInput += "|" + softNum;
            // totalPrice
            iicInput += "|" + Math.Round((Convert.ToDouble(txtTotal1)), 2).ToString();

            using (X509Certificate2 keyStore = new X509Certificate2(KEYSTORE_LOCATION, KEYSTORE_PASS))
            {
                try
                {
                    // Load a private from a key store
                    RSA privateKey = keyStore.GetRSAPrivateKey();
                    // Create IIC signature according to RSASSA-PKCS-v1_5
                    byte[] iicSignature = privateKey.SignData(Encoding.ASCII.GetBytes(iicInput), HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                    string iicSignatureString = BitConverter.ToString(iicSignature).Replace("-", string.Empty);
                    Console.WriteLine("The IIC signature is: " + iicSignatureString);
                    // Hash IIC signature with MD5 to create IIC
                    byte[] iicb = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(iicSignature);
                    iicString = BitConverter.ToString(iicb).Replace("-", string.Empty);
                    Console.WriteLine("The IIC is: " + iicString);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return iicString;
        }
        public static string kontrolloNeseCertifikataKaSkaduar(int idNderm)
        {
            try
            {
                clsNdermarrje nderm = new clsNdermarrje(idNderm);
                if (!nderm.Fiskalizimi) return "";
                if (nderm.Pathname.ToString() == "") return "";

                string passpath = nderm.Pathname + $"/password.txt";
                String KEYSTORE_PASS = "";
                String KEYSTORE_LOCATION = System.Web.Hosting.HostingEnvironment.MapPath(nderm.Pathname) + @"certifikata.p12";
                if (File.Exists(System.Web.Hosting.HostingEnvironment.MapPath(nderm.Pathname) + @"password.txt"))
                    KEYSTORE_PASS = File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath(nderm.Pathname) + @"password.txt");
                else throw new Exception("Ju lutem ngarkoni filen e passwordit!");
                DateTime now = DateTime.Now.AddDays(16);
                string certDate = new X509Certificate2(KEYSTORE_LOCATION, KEYSTORE_PASS).GetExpirationDateString();
                if (DateTime.Parse(certDate) < now)
                {
                    certDate = certDate.Split(' ')[0];
                    return certDate;
                }
                return "";
            }
            catch(Exception ex)
            {
                return "";
            }
        }
        public static bool ktheNeseCertifikataEFiskalizimitKaSkaduar(int idNderm)
        {
            try
            {
                clsNdermarrje nderm = new clsNdermarrje(idNderm);
                if (!nderm.Fiskalizimi) return false;
                if (nderm.Pathname.ToString() == "") return false;
                string passpath = nderm.Pathname + $"/password.txt";
                String KEYSTORE_PASS = "";
                String KEYSTORE_LOCATION = System.Web.Hosting.HostingEnvironment.MapPath(nderm.Pathname) + @"certifikata.p12";
                if (File.Exists(System.Web.Hosting.HostingEnvironment.MapPath(nderm.Pathname) + @"password.txt"))
                    KEYSTORE_PASS = File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath(nderm.Pathname) + @"password.txt");
                else throw new Exception("Ju lutem ngarkoni filen e passwordit!");
                if(DateTime.Now > DateTime.Parse(new X509Certificate2(KEYSTORE_LOCATION, KEYSTORE_PASS).GetExpirationDateString())) return true;

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static string[] gjeneroFatureShoqeruese(clsNdermarrje nderm, string wtnic, string wtnicSignature, string shoqerimIKerkuar, string mallraTeDjegshme, string adresaFillimit, string vendiFillimit, string targa, colTrupiMagazina trupi, string totali, string kodi, int transportuesi, string tipiMagazina, string qyteti, string dtTransporti, bool eshteGrup, string kodBiznesi,string tipi,string transaksioni, clsNjesiAdministrative njesiAdministrativeDestinacion, clsNjesiAdministrative njesiAdministrative,string kodOperatori, bool lista)
        {
            var uuid = Guid.NewGuid().ToString();
            var viti = DateTime.Now.Year;
            string adresaDestinacion = njesiAdministrativeDestinacion.Adresa;
            adresaFillimit = njesiAdministrative.Adresa;
            uuid = $"\"{uuid}\"";
            tipi = $"\"{tipi}\"";
            transaksioni = $"\"{transaksioni}\"";
            string dataDergimit = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz");
            dataDergimit = $"\"{dataDergimit}\"";
            wtnic = $"\"{wtnic}\"";
            wtnicSignature = $"\"{wtnicSignature}\"";
            kodOperatori = $"\"{kodOperatori}\"";
            shoqerimIKerkuar = $"\"{shoqerimIKerkuar.ToLower()}\"";
            mallraTeDjegshme = $"\"{mallraTeDjegshme.ToLower()}\"";
            adresaFillimit = $"\"{adresaFillimit}\"";
            vendiFillimit = $"\"{vendiFillimit}\"";
            dtTransporti = $"\"{dtTransporti}\"";
            targa = $"\"{targa}\"";
            adresaDestinacion = $"\"{adresaDestinacion}\"";
            string trupiItems = "";
            var WTNNum = $"\"{kodi}/{viti}\"";
            var kodiNum = $"\"{kodi}\"";
            var qytetiDestinacion = new clsQyteti(njesiAdministrativeDestinacion.Qyteti).EmriQyteti;
            string qytetiINisjes = new clsQyteti(njesiAdministrative.Qyteti).EmriQyteti;
            qytetiINisjes = $"\"{qytetiINisjes}\"";
            qytetiDestinacion = $"\"{qytetiDestinacion}\"";
            var vehOwnership = "";
            clsTransportues transportues = new clsTransportues(transportuesi);
            string niptTransportuesi = transportues.NIPT;
            string emerTransportuesi = transportues.Emertimi;
            string adresTransportuesi = transportues.Adresa;
            string tipiIdTransportuesi = transportues.TipiId;
            niptTransportuesi = $"\"{niptTransportuesi}\"";
            emerTransportuesi = $"\"{emerTransportuesi}\"";
            adresTransportuesi = $"\"{adresTransportuesi}\"";
            tipiIdTransportuesi = $"\"{tipiIdTransportuesi}\"";
            string carrierDetails = "";
            if (transportues.NIPT == nderm.NdermarrjeNipt)
                vehOwnership = "OWNER";
            else
            {
                vehOwnership = "THIRDPARTY";
                carrierDetails = "<Carrier IDType=" + $"{tipiIdTransportuesi}" + " IDNum=" + $"{niptTransportuesi}" + " Name=" + $"{emerTransportuesi}" + " Address=" + $"{adresTransportuesi}" + "/>";
            }
            var tipiIdMagazinaDestinacion = njesiAdministrativeDestinacion.TipiMag;
            switch (tipiIdMagazinaDestinacion)
            {
                case "1":
                    tipiIdMagazinaDestinacion = "WAREHOUSE";
                    break;
                case "2":
                    tipiIdMagazinaDestinacion = "EXHIBITION";
                    break;
                case "3":
                    tipiIdMagazinaDestinacion = "STORE";
                    break;
                case "4":
                    tipiIdMagazinaDestinacion = "SALE";
                    break;
                case "5":
                    tipiIdMagazinaDestinacion = "OTHER";
                    break;
            }
            var tipiIdMagazinaFillim = njesiAdministrative.TipiMag;
            switch (tipiIdMagazinaFillim)
            {
                case "1":
                    tipiIdMagazinaFillim = "WAREHOUSE";
                    break;
                case "2":
                    tipiIdMagazinaFillim = "EXHIBITION";
                    break;
                case "3":
                    tipiIdMagazinaFillim = "STORE";
                    break;
                case "4":
                    tipiIdMagazinaFillim = "SALE";
                    break;
                case "5":
                    tipiIdMagazinaFillim = "OTHER";
                    break;
            }
            tipiIdMagazinaFillim = $"\"{tipiIdMagazinaFillim}\"";
            tipiIdMagazinaDestinacion = $"\"{tipiIdMagazinaDestinacion}\"";
            vehOwnership = $"\"{vehOwnership}\"";
            double shumeArtikujsh = 0;
            if (lista)
            {
                for (var i = 0; i < trupi.Count; i++)
                {
                    var art = new clsArtikulli(trupi[i].IdArtikulli);
                    var barKodi = clsKodbari.ktheKodbarSipasIdArtikulliNjesiaKodbariIPare(art.KodArtikulli, nderm.IdNdermarrje);
                    var kodiArtikulli = art.KodArtikulli;
                    var pershkrimArtikulli = art.PershkrimArtikulli;
                    var Q = trupi[i].Sasia;
                    shumeArtikujsh += trupi[i].Vlefta;
                    if (barKodi == null)
                        barKodi = " ";
                    if (kodiArtikulli == null)
                        kodiArtikulli = " ";
                    clsNjesiArtikulli njesiArtikulli = new clsNjesiArtikulli(trupi[i].IdNjesia);
                    var U = njesiArtikulli.KodNjesia;
                    var Q2 = String.Format("{0:0.00}", Q);
                    barKodi = $"\"{barKodi}\"";
                    pershkrimArtikulli = $"\"{pershkrimArtikulli}\"";
                    kodiArtikulli = $"\"{kodiArtikulli}\"";
                    Q2 = $"\"{Q2}\"";
                    U = $"\"{U}\"";

                    trupiItems += "<I C =" + $"{barKodi}" + " N=" + $"{pershkrimArtikulli}" + " Q=" + $"{Q2}" + " U=" + $"{U}" + " />";
                }

            }
            else
            {
                for (var i = 0; i < trupi.Count; i++)
                {

                    var barKodi = clsKodbari.ktheKodbarSipasIdArtikulliNjesiaKodbariIPare(trupi[i].KodiArtikull, nderm.IdNdermarrje);
                    var kodiArtikulli = trupi[i].KodiArtikull;
                    var pershkrimArtikulli = trupi[i].PershkrimArtikull;
                    var Q = trupi[i].Sasia;
                    shumeArtikujsh += trupi[i].Vlefta;
                    if (barKodi == null)
                        barKodi = " ";
                    if (kodiArtikulli == null)
                        kodiArtikulli = " ";
                    clsNjesiArtikulli njesiArtikulli = new clsNjesiArtikulli(trupi[i].IdNjesia);
                    var U = njesiArtikulli.KodNjesia;
                    var Q2 = String.Format("{0:0.00}", Q);
                    barKodi = $"\"{barKodi}\"";
                    pershkrimArtikulli = $"\"{pershkrimArtikulli}\"";
                    kodiArtikulli = $"\"{kodiArtikulli}\"";
                    Q2 = $"\"{Q2}\"";
                    U = $"\"{U}\"";

                    trupiItems += "<I C =" + $"{barKodi}" + " N=" + $"{pershkrimArtikulli}" + " Q=" + $"{Q2}" + " U=" + $"{U}" + " />";
                }
            }
            
            totali = shumeArtikujsh.ToString();
            var totali2 = String.Format("{0:0.00}", Convert.ToDouble(totali));
            totali2 = $"\"{totali2}\"";
            trupiItems = "<Items>" + trupiItems + "</Items>";
            clsQyteti qytetiNdermarrjes = new clsQyteti(nderm.NdermarrjeQytetiPershkrimi, nderm.IdNdermarrje);
            string ndermarrjeQyteti = qytetiNdermarrjes.KodiQyteti;
            string ndermarrjeEmri = nderm.NdermarrjePershkrimi;
            string ndermarrjeVendi = nderm.NdermarrjeVendi;
            string ndermarjeNIPT = nderm.NdermarrjeNipt;
            string ndermarjeKodBiznesi = kodBiznesi;
            ndermarrjeQyteti = $"\"{ndermarrjeQyteti}\"";
            ndermarjeKodBiznesi = $"\"{ndermarjeKodBiznesi}\"";
            ndermarrjeEmri = $"\"{ndermarrjeEmri}\"";
            Regex rgx = new Regex("[^a-zA-Z0-9 -]");
            ndermarrjeVendi = rgx.Replace(ndermarrjeVendi, "");
            ndermarrjeVendi = $"\"{ndermarrjeVendi}\"";
            ndermarjeNIPT = $"\"{ndermarjeNIPT}\"";
            string kodSoftueri = WebConfigurationManager.AppSettings["kodSoftueri"];
            kodSoftueri = $"\"{kodSoftueri}\"";
            var objekti = new string[2];

            const string XML_SCHEMA_NS = "https://eFiskalizimi.tatime.gov.al/FiscalizationService/schema";
            const String XML_REQUEST_ID = "Request";
            const String XML_SIG_METHOD = "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256";
            const String XML_DIG_METHOD = "http://www.w3.org/2001/04/xmlenc#sha256";
            String REQUEST_TO_SIGN =
            "<RegisterWTNRequest " +
            " xmlns=\"https://eFiskalizimi.tatime.gov.al/FiscalizationService/schema\" " +
            " xmlns:ns2=\"http://www.w3.org/2000/09/xmldsig#\" " +
            " Id=\"Request\" " +
            " Version=\"3\">\r\n" +
            " <Header SendDateTime=" + $"{dataDergimit}" + " UUID=" + $"{uuid}" + "/>\r\n" +
            " <WTN BusinUnitCode= " + $"{ndermarjeKodBiznesi}" + " DestinAddr= " + $"{adresaDestinacion}" + " StartAddr= " + $"{adresaFillimit}" + " DestinCity= " + $"{qytetiDestinacion}" + " DestinDateTime=" + $"{dtTransporti}" + " DestinPoint= " + $"{tipiIdMagazinaDestinacion}" + " IsEscortRequired= " + $"{shoqerimIKerkuar}" + " IsGoodsFlammable= " + $"{mallraTeDjegshme}" + " ValueOfGoods=" + $"{totali2}" + " IssueDateTime= " + $"{dataDergimit}" + " OperatorCode=" + $"{kodOperatori}" + " SoftCode=" + $"{kodSoftueri}" + " StartCity=" + $"{qytetiINisjes}" + " StartDateTime=" + $"{dtTransporti}" + " StartPoint=" + $"{tipiIdMagazinaFillim}" + " Transaction=" + $"{transaksioni}" + " Type=" + $"{tipi}" + " VehOwnership=" + $"{vehOwnership}" + " VehPlates=" + $"{targa}" + " WTNIC= " + $"{wtnic}" + " WTNICSignature= " + $"{wtnicSignature}" + " WTNNum=" + $"{WTNNum}" + " WTNOrdNum=" + $"{kodiNum}" + "> <Issuer Address= " + $"{ndermarrjeVendi}" + " NUIS= " + $"{ndermarjeNIPT}" + " Name= " + $"{ndermarrjeEmri}" + " Town= " + $"{ndermarrjeQyteti}" + "/>" + carrierDetails + trupiItems + " </WTN>\r\n" +
            "</RegisterWTNRequest>";
            REQUEST_TO_SIGN = REQUEST_TO_SIGN.Replace("&", "&amp;");
            string passpath = nderm.Pathname + $"/password.txt";
            String KEYSTORE_PASS = "";
            byte[] encrypted;
            String KEYSTORE_LOCATION = System.Web.Hosting.HostingEnvironment.MapPath(nderm.Pathname) + @"certifikata.p12";
            if (File.Exists(System.Web.Hosting.HostingEnvironment.MapPath(nderm.Pathname) + @"password.txt"))
                KEYSTORE_PASS = File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath(nderm.Pathname) + @"password.txt");
            else throw new Exception("Ju lutem ngarkoni filen e passwordit!");
            using (X509Certificate2 keyStore = new X509Certificate2(KEYSTORE_LOCATION, KEYSTORE_PASS))
            {
                try
                {
                    // Load a private from a key store
                    RSA privateKey = keyStore.GetRSAPrivateKey();
                    // Convert string XML to object
                    XmlDocument request = new XmlDocument();
                    request.LoadXml(REQUEST_TO_SIGN);
                    // Create key info element
                    KeyInfo keyInfo = new KeyInfo();
                    KeyInfoX509Data keyInfoData = new KeyInfoX509Data();
                    keyInfoData.AddCertificate(keyStore);
                    keyInfo.AddClause(keyInfoData);
                    // Create signature reference
                    Reference reference = new Reference("");
                    reference.AddTransform(new XmlDsigEnvelopedSignatureTransform(false));
                    reference.AddTransform(new XmlDsigExcC14NTransform(false));
                    reference.DigestMethod = XML_DIG_METHOD;
                    reference.Uri = "#" + XML_REQUEST_ID;
                    // Create signature
                    SignedXml xml = new SignedXml(request);
                    xml.SigningKey = privateKey;
                    xml.SignedInfo.CanonicalizationMethod = SignedXml.XmlDsigExcC14NTransformUrl;
                    xml.SignedInfo.SignatureMethod = XML_SIG_METHOD;
                    xml.KeyInfo = keyInfo;
                    xml.AddReference(reference);
                    xml.ComputeSignature();
                    //= Add signature element to the request
                    XmlElement signature = xml.GetXml();
                    request.DocumentElement.AppendChild(signature);
                    // Convert signed request to string and print
                    StringWriter sw = new StringWriter();
                    XmlTextWriter xw = new XmlTextWriter(sw);
                    request.WriteTo(xw);
                    var signedDoc = sw.ToString();
                    if (eshteGrup)
                        signedDoc = signedDoc;
                    else
                        signedDoc = "<SOAP-ENV:Envelope xmlns:SOAP-ENV=\"http://schemas.xmlsoap.org/soap/envelope/\"><SOAP-ENV:Header/><SOAP-ENV:Body>" + signedDoc + "</SOAP-ENV:Body></SOAP-ENV:Envelope>";
                    objekti[0] = signedDoc;
                    objekti[1] = REQUEST_TO_SIGN;
                    return objekti;

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return objekti;
        }
        public static string GjeneroWTNIC(clsNdermarrje nderm, string txtNumer, string txtTotal1, string cashRegister, string softNum)
        {


            if (nderm.Pathname.ToString() == "")
                throw new Exception("Ju lutem ngarkoni certifikaten e sigurise!");

            string passpath = nderm.Pathname + $"/password.txt";
            String KEYSTORE_PASS = "";

            byte[] encrypted;
            String KEYSTORE_LOCATION = System.Web.Hosting.HostingEnvironment.MapPath(nderm.Pathname) + @"certifikata.p12";
            if (File.Exists(System.Web.Hosting.HostingEnvironment.MapPath(nderm.Pathname) + @"password.txt"))
                KEYSTORE_PASS = File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath(nderm.Pathname) + @"password.txt");
            else throw new Exception("Ju lutem ngarkoni filen e passwordit!");

            String wtnicInput = "";
            string wtnicString = "";
            // issuerNuis
            wtnicInput += "|" + nderm.NdermarrjeNipt;
            // dateTimeCreated
            wtnicInput += "|" + DateTime.Now.ToString();
            // invoiceNumber
            wtnicInput += "|" + txtNumer;
            // busiUnit
            wtnicInput += "|" + nderm.Kodbiznesi;
            // cashRegister
            wtnicInput += "|" + cashRegister;
            // softNum
            wtnicInput += "|" + softNum;

            using (X509Certificate2 keyStore = new X509Certificate2(KEYSTORE_LOCATION, KEYSTORE_PASS))
            {
                try
                {
                    // Load a private from a key store
                    RSA privateKey = keyStore.GetRSAPrivateKey();
                    // Create IIC signature according to RSASSA-PKCS-v1_5
                    byte[] wtnicSignature = privateKey.SignData(Encoding.ASCII.GetBytes(wtnicInput), HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                    string wtnivSignatureString = BitConverter.ToString(wtnicSignature).Replace("-", string.Empty);
                    // Hash IIC signature with MD5 to create IIC
                    byte[] wtnicb = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(wtnicSignature);
                    wtnicString = BitConverter.ToString(wtnicb).Replace("-", string.Empty);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return wtnicString;
        }
        public static string GjeneroWTNICSignature(clsNdermarrje nderm, string txtNumer, string txtTotal1, string cashRegister, string softNum)
        {


            if (nderm.Pathname.ToString() == "")
                throw new Exception("Ju lutem ngarkoni certifikaten e sigurise!");

            string passpath = nderm.Pathname + $"/password.txt";
            String KEYSTORE_PASS = "";

            byte[] encrypted;
            String KEYSTORE_LOCATION = System.Web.Hosting.HostingEnvironment.MapPath(nderm.Pathname) + @"certifikata.p12";
            if (File.Exists(System.Web.Hosting.HostingEnvironment.MapPath(nderm.Pathname) + @"password.txt"))
                KEYSTORE_PASS = File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath(nderm.Pathname) + @"password.txt");
            else throw new Exception("Ju lutem ngarkoni filen e passwordit!");

            String wtnicInput = "";
            string wtnicString = "";
            string wtnicSignatureString = "";
            // issuerNuis
            wtnicInput += "|" + nderm.NdermarrjeNipt;
            // dateTimeCreated
            wtnicInput += "|" + DateTime.Now.ToString();
            // invoiceNumber
            wtnicInput += "|" + txtNumer;
            // busiUnit
            wtnicInput += "|" + nderm.Kodbiznesi;
            // cashRegister
            wtnicInput += "|" + cashRegister;
            // softNum
            wtnicInput += "|" + softNum;

            using (X509Certificate2 keyStore = new X509Certificate2(KEYSTORE_LOCATION, KEYSTORE_PASS))
            {
                try
                {
                    // Load a private from a key store
                    RSA privateKey = keyStore.GetRSAPrivateKey();
                    // Create IIC signature according to RSASSA-PKCS-v1_5
                    byte[] wtnicSignature = privateKey.SignData(Encoding.ASCII.GetBytes(wtnicInput), HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                    wtnicSignatureString = BitConverter.ToString(wtnicSignature).Replace("-", string.Empty);
                    // Hash IIC signature with MD5 to create IIC
                    byte[] wtnicb = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(wtnicSignature);
                    wtnicString = BitConverter.ToString(wtnicb).Replace("-", string.Empty);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return wtnicSignatureString;
        }
        public static string ktheIICSignature(clsNdermarrje nderm, string txtNumer, string txtTotal1, string cashRegister, string softNum, DateTime dateKrijimi)
        {
            //string DateServeriOffset = clsKontrollePerFiskalizimin.ktheDatenEServeritOffset();
            //DateTimeOffset dt = DateTimeOffset.Parse(DateServeriOffset);
            //TimeSpan t = dt.Offset;
            //DateTimeOffset sourceDate = new DateTimeOffset(dateKrijimi,
            //             t);

            //DateTimeOffset timezoneIShqiperise = TimeZoneInfo.ConvertTime(sourceDate,
            //              TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time"));

            string iicSignatureString = "";
            if (nderm.Pathname.ToString() == "")
                throw new Exception("Ju lutem ngarkoni certifikaten e sigurise!");

            string passpath = nderm.Pathname + $"/password.txt";
            String KEYSTORE_PASS = "";

            byte[] encrypted;
            String KEYSTORE_LOCATION = System.Web.Hosting.HostingEnvironment.MapPath(nderm.Pathname) + @"certifikata.p12";
            if (File.Exists(System.Web.Hosting.HostingEnvironment.MapPath(nderm.Pathname) + @"password.txt"))
                KEYSTORE_PASS = File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath(nderm.Pathname) + @"password.txt");
            else throw new Exception("Ju lutem ngarkoni filen e passwordit!");

            String iicInput = "";
            string iicString = "";
            // issuerNuis
            iicInput += "|" + nderm.NdermarrjeNipt;
            // dateTimeCreated
            //iicInput += "|" + timezoneIShqiperise.ToString("yyyy-MM-ddTHH\\:mm\\:sszzz");
            // invoiceNumber
            iicInput += "|" + txtNumer;
            // busiUnit
            iicInput += "|" + nderm.Kodbiznesi;
            // cashRegister
            iicInput += "|" + cashRegister;
            // softNum
            iicInput += "|" + softNum;
            // totalPrice
            iicInput += "|" + Math.Round((Convert.ToDouble(txtTotal1)), 2).ToString();

            using (X509Certificate2 keyStore = new X509Certificate2(KEYSTORE_LOCATION, KEYSTORE_PASS))
            {
                try
                {
                    // Load a private from a key store
                    RSA privateKey = keyStore.GetRSAPrivateKey();
                    // Create IIC signature according to RSASSA-PKCS-v1_5
                    byte[] iicSignature = privateKey.SignData(Encoding.ASCII.GetBytes(iicInput), HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                    iicSignatureString = BitConverter.ToString(iicSignature).Replace("-", string.Empty);
                    // Hash IIC signature with MD5 to create IIC
                    byte[] iicb = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(iicSignature);
                    iicString = BitConverter.ToString(iicb).Replace("-", string.Empty);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return iicSignatureString;
        }

        public static string[] GjeneroMesazhInvoice(clsNdermarrje nderm, string txtNumer, string txtTotal1, string cashRegister, string softNum, string iic, string iicSignature,
                    string adresaFaturimit, string pershkrimi, string toatli, string dtMaturimi, string totali, string tvsh, string perqindjeZbritja, string emerKlienti, string niptK
                    , string vleraPaTvsh, string shteti, string qyteti, string pershkrimArtikulli, string kodArtikulli, colTrupiShitje artikujt, string tcr, string menyrePagese, clsNdermarrje ndermarje,
                    string kodKlienti, string zbritjaPaTvsh, string nenTotali, string nivfKthim, DateTime dtRegjistrimi, string operatoriEmri, bool eshteGrup, string kursi, bool einvoice, string kodBiznesi,
                    string tipiAutongarkes, string kodOperator, bool isShitje, bool isModifikim, bool dogane,string dateMbarimi,string dateFillimi,string dateFature,DateTime dateKrijimiFature, int idNdem,int idPerdoruesi,string viti)
        {
            //Kodet e artikujve
            //N = Emri
            //C = Kodi
            //U = Kodi i njesise matese
            //Q = Sasia
            //UPB = Vlera e njesise pa TVSH
            //UPA = Vlera e njesise me TVSH
            //PB = Cmimi para TVSH-se
            //PA = Cmimi pas TVSH-se

            string iicFatureOrigjinale = "";
            string nivfFatureOrigjinale = "";
            string dateOrigjinaleKrijimi = "";
            double tot = 0;
            if (nivfKthim != "")
            {
                var drow = colKokaShitje.MerrShitjeSipasNivf(nivfKthim);
                iicFatureOrigjinale = drow.ItemArray[1].ToString();
                nivfFatureOrigjinale = drow.ItemArray[0].ToString();
                var dataOrigjinale = drow.ItemArray[2].ToString();
                var dataFatureOrigjinale = drow.ItemArray[3].ToString();
                DateTimeOffset dtKrijimiPajisjeOffset = new DateTimeOffset(DateTime.Parse(dataOrigjinale));
                var dateOrigjinale = Convert.ToDateTime(dataFatureOrigjinale.Split(' ')[0] + ' ' + dtKrijimiPajisjeOffset.ToString().Split(' ')[1]);
                nivfFatureOrigjinale = $"{nivfFatureOrigjinale}";
                DateTimeOffset timezoneIShqiperiseOrigjinale = TimeZoneInfo.ConvertTime(dateOrigjinale,
                              TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time"));
                dateOrigjinaleKrijimi = $"\"{timezoneIShqiperiseOrigjinale.ToString("yyyy-MM-ddTHH\\:mm\\:sszzz")}\"";

            }
            double vleraTaxFreeAmt = 0;
            double vleramarkUpAmt = 0;
            decimal vleraPaTvshEArtikujve = 0;
            string currency = "";
            var uuid = Guid.NewGuid().ToString();
            uuid = $"\"{uuid}\"";
            string[] emriMbiemriOperatori = operatoriEmri.Split(' ');
            var dr = colKokaShitje.MerrShitjeSipasNivf(nivfKthim);
            int done = 0;
            double vleftaTvsh = 0;
            double tvshEVlerave = 0;
            var tvshArray = new int[artikujt.Count];
            string artikujtarray = "";
            var kodOperatori = kodOperator;

            clsKlientFurnitor klienti = new clsKlientFurnitor();
            klienti.mbushKlientFurnitorSipasKodit(kodKlienti, nderm.IdNdermarrje);

            clsArtikulli artikull = new clsArtikulli();
            artikull.mbushArtikull(artikujt[0].Kodi, nderm.IdNdermarrje);
            var kodiArtikulli = artikujt[0].Kodi;
            var emertimArtikulli = artikujt[0].Pershkrimi;
            var zbritjaPaTvsh2 = Convert.ToDouble(zbritjaPaTvsh);
            var nenTotali2 = Convert.ToDouble(nenTotali);
            double totalFature = 0;
            var PA = artikujt[0].VleftaMeTvsh * (1 - zbritjaPaTvsh2 / nenTotali2);
            var PA2 = String.Format("{0:0.00}", PA * Double.Parse(kursi));
            if (PA2 == "NaN")
                PA2 = "0.00";
            PA2 = $"\"{PA2}\"";
            var PB = artikujt[0].VleftaPaTvsh * (1 - zbritjaPaTvsh2 / nenTotali2);
            var PB2 = String.Format("{0:0.00}", PB * Double.Parse(kursi));
            if (PB2 == "NaN")
                PB2 = "0.00";
            PB2 = $"\"{PB2}\"";
            var sasiCmimi = artikujt[0].Cmimi * artikujt[0].Sasia;
            var R = 100 * (sasiCmimi - PB) / sasiCmimi;
            var R2 = String.Format("{0:0.00}", R);
            if (R2 == "NaN")
                R2 = "0.00";
            R2 = $"\"{R2}\"";
            var UPA = PA / artikujt[0].Sasia;
            var UPA2 = String.Format("{0:0.00}", UPA * Double.Parse(kursi));
            if (UPA2 == "NaN")
                UPA2 = "0.00";
            UPA2 = $"\"{UPA2}\"";
            var sasia = artikujt[0].Sasia.ToString();

            var VR = artikujt[0].Tvsh;
            var VR2 = new clsTaksa(VR).NormaPerqindje.ToString();
            var VR3 = String.Format("{0:0.00}", Convert.ToDouble(VR2));
            VR3 = $"\"{VR3}\"";

            var VA = PB * Convert.ToDouble(VR2) / 100;
            var VA2 = String.Format("{0:0.00}", VA * Double.Parse(kursi));

            if (VA2 == "NaN")
                VA2 = "0.00";
            VA2 = $"\"{VA2}\"";
            var U = artikull.KodNjesia1;
            U = $"\"{U}\"";
            var UPB = PB / artikujt[0].Sasia;
            var UPB2 = String.Format("{0:0.00}", UPB * Double.Parse(kursi));
            if (UPB2 == "NaN")
                UPB2 = "0.00";
            UPB2 = $"\"{UPB2}\"";
            var zbritje = String.Format("{0:0.00}", artikujt[0].Zbritje);
            var vleftaMeTvshPerSasi = artikujt[0].VleftaMeTvsh / artikujt[0].Sasia;
            var vleftaPaTvshPerSasi = artikujt[0].VleftaPaTvsh / artikujt[0].Sasia;
            var tvshPerSasi = vleftaMeTvshPerSasi - vleftaPaTvshPerSasi;
            var vleftaMeTvshPerSasi2 = String.Format("{0:0.00}", vleftaMeTvshPerSasi);
            var vleftaPaTvshPerSasi2 = String.Format("{0:0.00}", vleftaPaTvshPerSasi);
            var tvshPerSasi2 = String.Format("{0:0.00}", tvshPerSasi);
            var perqindjeSasia = (tvshPerSasi / vleftaPaTvshPerSasi) * 100;
            var perqindjeSasia2 = String.Format("{0:0.00}", perqindjeSasia);
            zbritje = $"\"{zbritje}\"";
            vleftaMeTvshPerSasi2 = $"\"{vleftaMeTvshPerSasi2}\"";
            vleftaPaTvshPerSasi2 = $"\"{vleftaPaTvshPerSasi2}\"";
            tvshPerSasi2 = $"\"{tvshPerSasi2}\"";
            kodiArtikulli = $"\"{kodiArtikulli}\"";
            emertimArtikulli = $"\"{emertimArtikulli}\"";
            var sasiaNumber = Double.Parse(String.Format("{0:0.00}", artikujt[0].Sasia.ToString()));
            if ((sasiaNumber - (int)sasiaNumber) != 0)
                sasia = $"\"{sasia}\"";
            else
                sasia = $"\"{sasia}.0\"";
            if (perqindjeSasia2 != "NaN")
                perqindjeSasia2 = $"\"{perqindjeSasia2}\"";
            else
                perqindjeSasia2 = $"\0.00\"";
            string taxFreeAmt = "";
            string markUpAmt = "";
            List<string> arrayTvsh = new List<string>();
            List<string> perjashtimiTvsh = new List<string>();
            string taxArray = "";
            List<int> artikujtInt = new List<int>();
            List<string> derguarTax = new List<string>();
            nenTotali2 = 0;
            List<int> listTaksash = new List<int>();
            List<clsTrupiShitje> listaTrupiShitje = new List<clsTrupiShitje>();
            List<clsTrupiShitje> listaTrupiShitjePaTvsh = new List<clsTrupiShitje>();
            List<clsTrupiShitje> listaTrupiShitjeMarginScheme= new List<clsTrupiShitje>();
            List<clsTrupiShitje> listaTrupiShitjeTaxFree= new List<clsTrupiShitje>();
            List<clsTrupiShitje> listaTrupiShitjeType1 = new List<clsTrupiShitje>();
            List<clsTrupiShitje> listaTrupiShitjeType2 = new List<clsTrupiShitje>();
            List<clsTaksa> listaETaksave = new List<clsTaksa>();
            double taksa20 = 0;
            for (var i = 0; i < artikujt.Count; i++)
            {
                nenTotali2 += artikujt[i].VleftaMeTvsh;
            }
            
            colTaksa colTaksat = new colTaksa(idNdem, idPerdoruesi);
            colTaksat.Add(new clsTaksa());
            colTaksat.FirstOrDefault(x=>x.IdTaksa == 0).NormaPerqindje = 0.0000000000M;
            colTaksat.FirstOrDefault(x=>x.IdTaksa == 0).TipiIPerjashtimit = "";
            colNjesiteArtikulli njesiteEArtikujve = new colNjesiteArtikulli(idNdem);
            clsTaksa norma = new clsTaksa();

            for (var i = 0; i < artikujt.Count; i++)
            {
                if (dogane)
                {
                    if (artikujt[i].Tvsh == 0)
                        listaTrupiShitjePaTvsh.Add(artikujt[i]);
                    else
                        listaTrupiShitje.Add(artikujt[i]);
                }
                else
                {
                    if(colTaksat.FirstOrDefault(z => z.IdTaksa == artikujt[i].Tvsh).TipiIPerjashtimit == "TYPE_1")
                    {
                        listaTrupiShitjeType1.Add(artikujt[i]);
                    }
                    else if (colTaksat.FirstOrDefault(z => z.IdTaksa == artikujt[i].Tvsh).TipiIPerjashtimit == "TYPE_2")
                    {
                        listaTrupiShitjeType2.Add(artikujt[i]);
                    }
                    else
                    {
                        if (artikujt[i].Tvsh == 0)
                            listaTrupiShitjePaTvsh.Add(artikujt[i]);
                        else if (colTaksat.FirstOrDefault(z => z.IdTaksa == artikujt[i].Tvsh).TipiIPerjashtimit == "MARGIN_SCHEME")
                            listaTrupiShitjeMarginScheme.Add(artikujt[i]);
                        else if (colTaksat.FirstOrDefault(z => z.IdTaksa == artikujt[i].Tvsh).TipiIPerjashtimit == "TAX_FREE")
                            listaTrupiShitjeTaxFree.Add(artikujt[i]);
                        else
                            listaTrupiShitje.Add(artikujt[i]);
                    }

                }
                vleftaTvsh = 0;
                foreach (var item in njesiteEArtikujve.ToList()
                                                               .Where(x => x.IdNjesia == artikujt[i].IdNjesia))
                {
                    U = item.KodNjesia;
                }
                foreach (var item in colTaksat
                                                .Where(x => x.IdTaksa == artikujt[i].Tvsh))
                {
                    norma = item;
                }
                kodiArtikulli = artikujt[i].Kodi;
                emertimArtikulli = artikujt[i].Pershkrimi;
                zbritjaPaTvsh2 = Convert.ToDouble(zbritjaPaTvsh);
                if(nenTotali2 == 0)
                {
                    PA = artikujt[i].VleftaMeTvsh * (1 - zbritjaPaTvsh2);
                    PB = artikujt[i].VleftaPaTvsh * (1 - zbritjaPaTvsh2);

                }
                else
                {
                    PA = artikujt[i].VleftaMeTvsh * (1 - zbritjaPaTvsh2 / nenTotali2);
                    PB = artikujt[i].VleftaPaTvsh * (1 - zbritjaPaTvsh2 / nenTotali2);
                }
                PA2 = String.Format("{0:0.00}", PA * Double.Parse(kursi));
                if (PA2 == "NaN")
                    PA2 = "0.00";
                PA2 = $"\"{PA2}\"";
                PB2 = String.Format("{0:0.00}", PB * Double.Parse(kursi));
                if (PB2 == "NaN") 
                    PB2 = "0.00";
                PB2 = $"\"{PB2}\"";
                sasiCmimi = artikujt[i].Cmimi * artikujt[i].Sasia;
                R = 100 * (sasiCmimi - PB) / sasiCmimi;
                R2 = String.Format("{0:0.00}", R);
                if (R2 == "NaN")
                    R2 = "0.00";
                R2 = $"\"{R2}\"";
                UPA = PA / artikujt[i].Sasia;
                UPA2 = String.Format("{0:0.00}", UPA * Double.Parse(kursi));
                if (UPA2 == "NaN")
                    UPA2 = "0.00";
                UPA2 = $"\"{UPA2}\"";
                sasia = artikujt[i].Sasia.ToString();
                VR = artikujt[i].Tvsh;
                VR2 = norma.NormaPerqindje.ToString();
                VR3 = String.Format("{0:0.00}", Convert.ToDouble(VR2));
                VR3 = $"\"{VR3}\"";

                VA = PB * Convert.ToDouble(VR2) / 100;
                VA2 = String.Format("{0:0.00}", VA * Double.Parse(kursi));
                totalFature += PB + VA;
                if (VA2 == "NaN")
                    VA2 = "0.00";
                VA2 = $"\"{VA2}\"";
                U = $"\"{U}\"";
                UPB = PB / artikujt[i].Sasia;
                UPB2 = String.Format("{0:0.00}", UPB * Double.Parse(kursi));
                if (UPB2 == "NaN")
                    UPB2 = "0.00";
                UPB2 = $"\"{UPB2}\"";
                zbritje = String.Format("{0:0.00}", artikujt[i].Zbritje);
                vleftaMeTvshPerSasi = artikujt[i].VleftaMeTvsh / artikujt[i].Sasia;
                vleftaPaTvshPerSasi = artikujt[i].VleftaPaTvsh / artikujt[i].Sasia;
                tvshPerSasi = vleftaMeTvshPerSasi - vleftaPaTvshPerSasi;
                vleftaMeTvshPerSasi2 = String.Format("{0:0.00}", vleftaMeTvshPerSasi);
                vleftaPaTvshPerSasi2 = String.Format("{0:0.00}", vleftaPaTvshPerSasi);
                tvshPerSasi2 = String.Format("{0:0.00}", tvshPerSasi);
                perqindjeSasia = (tvshPerSasi / vleftaPaTvshPerSasi) * 100;
                perqindjeSasia2 = String.Format("{0:0.00}", perqindjeSasia);
                zbritje = $"\"{zbritje}\"";
                vleftaMeTvshPerSasi2 = $"\"{vleftaMeTvshPerSasi2}\"";
                vleftaPaTvshPerSasi2 = $"\"{vleftaPaTvshPerSasi2}\"";
                tvshPerSasi2 = $"\"{tvshPerSasi2}\"";
                kodiArtikulli = $"\"{kodiArtikulli}\"";
                emertimArtikulli = $"\"{emertimArtikulli}\"";
                sasiaNumber = Double.Parse(String.Format("{0:0.00}", artikujt[i].Sasia.ToString()));
                sasia = (Math.Truncate(artikujt[i].Sasia * 100) / 100).ToString();
                if ((sasiaNumber - (int)sasiaNumber) != 0)
                    sasia = $"\"{sasia}\"";
                else
                    sasia = $"\"{sasia}.0\"";
                if (perqindjeSasia2 != "NaN")
                    perqindjeSasia2 = $"\"{perqindjeSasia2}\"";
                else
                    perqindjeSasia2 = $"\0.00\"";

                tvshArray[i] = artikujt[i].Tvsh;
                if (artikujt.Count == 1)
                {
                    tvshEVlerave = Convert.ToDouble(VR2);
                    vleftaTvsh = PB;
                }
                if (artikujt.Count == 1)
                {
                    tvshEVlerave = Convert.ToDouble(VR2);
                    vleftaTvsh = PB;
                }
                bool vazhdimTagu = false;
                if(norma.TipiIPerjashtimit != "" && norma.NormaPerqindje.ToString() == "0.0000000000")
                {
                    done = 0;
                }
                
                tot += Double.Parse(String.Format("{0:0.00}", PA));
                string taguPerjashtimi = "";
                if(norma.NormaPerqindje.ToString() == "0.0000000000" && !dogane && norma.TipiIPerjashtimit != "")
                {
                    string perjashtimi = norma.TipiIPerjashtimit;
                    perjashtimi = $"\"{perjashtimi}\"";
                    taguPerjashtimi = " EX=" + $"{perjashtimi}" + "";
                }
                if(norma.IdTaksa == 0)
                {
                    string perjashtimi = $"\"TAX_FREE\"";
                    taguPerjashtimi = " EX=" + $"{perjashtimi}" + "";
                }
                if (dogane)
                    artikujtarray += "<I C = " + $"{kodiArtikulli}" + " N=" + $"{emertimArtikulli}" + " PA=" + $"{PA2}" + " PB=" + $"{PB2}" + " Q=" + $"{sasia}" + " R=" + $"{R2}" + " RR=\"false\" EX=\"EXPORT_OF_GOODS\" U=" + $"{U}" + " UPA=" + $"{UPA2}" + " UPB=" + $"{UPB2}" + " VA=" + $"{VA2}" + "/>";
                else
                    if(taguPerjashtimi == "")
                        artikujtarray += "<I C = " + $"{kodiArtikulli}" + " N=" + $"{emertimArtikulli}" + " PA=" + $"{PA2}" + " PB=" + $"{PB2}" + " Q=" + $"{sasia}" + " R=" + $"{R2}" + " RR=\"false\" "+ taguPerjashtimi + " U=" + $"{U}" + " UPA=" + $"{UPA2}" + " UPB=" + $"{UPB2}" + " VA=" + $"{VA2}" + " VR=" + $"{VR3}" + "/>";
                    else
                        artikujtarray += "<I C = " + $"{kodiArtikulli}" + " N=" + $"{emertimArtikulli}" + " PA=" + $"{PA2}" + " PB=" + $"{PB2}" + " Q=" + $"{sasia}" + " R=" + $"{R2}" + " RR=\"false\" " + taguPerjashtimi + " U=" + $"{U}" + " UPA=" + $"{UPA2}" + " UPB=" + $"{UPB2}" + " VA=" + $"{VA2}" + "/>";
                //}


            }
            foreach (var item in listaTrupiShitje
                                                .Where(x => !listTaksash.Contains(x.Tvsh)))
            {
                listaETaksave.Add(colTaksat.FirstOrDefault(z => z.IdTaksa == item.Tvsh));
                listTaksash.AddIfNotExists(item.Tvsh);

            }
            foreach (var taks in listaETaksave)
            {
                if (!taxArray.Contains("VATRate="+ $"\"{String.Format("{0:0.00}", taks.NormaPerqindje)}\""))
                {
                    foreach (var item in listaTrupiShitje
                                                .Where(x => colTaksat.FirstOrDefault(z => z.IdTaksa == x.Tvsh).NormaPerqindje.ToString() == taks.NormaPerqindje.ToString()))
                    {
                        taksa20 = taksa20 + item.VleftaPaTvsh * Double.Parse(String.Format("{0:0.00}", (1 - zbritjaPaTvsh2 / nenTotali2)));
                        done = done + 1;
                    }
                    string doneString = $"\"{done}\"";
                    taksa20 = Math.Round(taksa20, 2);
                    var PriceBefVAT = String.Format("{0:0.00}", (taksa20 * Double.Parse(kursi)));
                    if (PriceBefVAT == "NaN")
                        vleraPaTvshEArtikujve += 0;
                    else
                        vleraPaTvshEArtikujve += Decimal.Parse(PriceBefVAT);
                    var VATAmt = taksa20 * Double.Parse(taks.NormaPerqindje.ToString()) / 100;
                    if (VATAmt.ToString() == "NaN")
                        VATAmt = 0;
                    if (PriceBefVAT == "NaN")
                        PriceBefVAT = "0.00";
                    var VATAmt2 = String.Format("{0:0.00}", (VATAmt * Double.Parse(kursi)));
                    var VatRate = String.Format("{0:0.00}", taks.NormaPerqindje);
                    VatRate = $"\"{VatRate}\"";
                    PriceBefVAT = $"\"{PriceBefVAT}\"";
                    VATAmt2 = $"\"{VATAmt2}\"";
                    taxArray += "<SameTax NumOfItems =" + $"{doneString}" + " PriceBefVAT=" + $"{PriceBefVAT}" + " VATAmt=" + $"{VATAmt2}" + " VATRate=" + $"{VatRate}" + "/>";
                    taksa20 = 0;
                    done = 0;

                }
                       

            }
            if (listaTrupiShitjePaTvsh.Count > 0 && !dogane)
            {
                foreach (var item in listaTrupiShitjePaTvsh)
                {
                    taksa20 = taksa20 + item.VleftaPaTvsh * Double.Parse(String.Format("{0:0.00}", (1 - zbritjaPaTvsh2 / nenTotali2)));
                    done = done + 1;
                }
                string doneStringPaTvsh = $"\"{done}\"";
                taksa20 = Math.Truncate(taksa20 * 100) / 100;
                var PriceBefVATPaTvsh = String.Format("{0:0.00}", (taksa20 * Double.Parse(kursi)));
                if (PriceBefVATPaTvsh == "NaN")
                    vleraPaTvshEArtikujve += 0;
                else
                    vleraPaTvshEArtikujve += Decimal.Parse(PriceBefVATPaTvsh);
                vleraTaxFreeAmt += Double.Parse(PriceBefVATPaTvsh);
                taksa20 = 0;
                done = 0;
            }
            if (listaTrupiShitjeTaxFree.Count > 0)
            {
                foreach (var item in listaTrupiShitjeTaxFree)
                {
                    taksa20 = taksa20 + item.VleftaPaTvsh * Double.Parse(String.Format("{0:0.00}", (1 - zbritjaPaTvsh2 / nenTotali2)));
                    done = done + 1;
                }
                string doneStringPaTvsh = $"\"{done}\"";
                taksa20 = Math.Truncate(taksa20 * 100) / 100;
                var PriceBefVATPaTvsh = String.Format("{0:0.00}", (taksa20 * Double.Parse(kursi)));
                if (PriceBefVATPaTvsh == "NaN")
                    vleraPaTvshEArtikujve += 0;
                else
                    vleraPaTvshEArtikujve += Decimal.Parse(PriceBefVATPaTvsh);
                vleraTaxFreeAmt += Double.Parse(PriceBefVATPaTvsh);
                taksa20 = 0;
                done = 0;
            }
            if (listaTrupiShitjeMarginScheme.Count > 0)
            {
                foreach (var item in listaTrupiShitjeMarginScheme)
                {
                    taksa20 = taksa20 + item.VleftaPaTvsh * Double.Parse(String.Format("{0:0.00}", (1 - zbritjaPaTvsh2 / nenTotali2)));
                    done = done + 1;
                }
                string doneStringPaTvsh = $"\"{done}\"";
                taksa20 = Math.Truncate(taksa20 * 100) / 100;
                var PriceBefVATPaTvsh = String.Format("{0:0.00}", (taksa20 * Double.Parse(kursi)));
                if (PriceBefVATPaTvsh == "NaN")
                    vleraPaTvshEArtikujve += 0;
                else
                    vleraPaTvshEArtikujve += Decimal.Parse(PriceBefVATPaTvsh);
                vleramarkUpAmt += Double.Parse(PriceBefVATPaTvsh);
                taksa20 = 0;
                done = 0;
            }
            vleramarkUpAmt = Math.Round(vleramarkUpAmt, 2);
            vleraTaxFreeAmt = Math.Round(vleraTaxFreeAmt, 2);
            if (vleraTaxFreeAmt.ToString() == "NaN")
                vleraTaxFreeAmt = 0.00;
            if (vleramarkUpAmt.ToString() == "NaN")
                vleramarkUpAmt = 0.00;
            string vleramarkUpAmtString = $"\"{String.Format("{0:0.00}", vleramarkUpAmt)}\"";
            string vleraTaxFreeAmtString = $"\"{String.Format("{0:0.00}", vleraTaxFreeAmt)}\"";
            if (listaTrupiShitjeMarginScheme.Count > 0)
                markUpAmt = " MarkUpAmt=" + $"{vleramarkUpAmtString}" + "";
            if(listaTrupiShitjeTaxFree.Count > 0 || listaTrupiShitjePaTvsh.Count > 0)
                taxFreeAmt = " TaxFreeAmt=" + $"{vleraTaxFreeAmtString}" + "";
            if (listaTrupiShitjeType1.Count > 0)
            {
                string tipiIPerjashtimit = "TYPE_1";
                foreach (var item in listaTrupiShitjeType1)
                {
                    taksa20 = taksa20 + item.VleftaPaTvsh * Double.Parse(String.Format("{0:0.00}", (1 - zbritjaPaTvsh2 / nenTotali2)));
                    done = done + 1;
                }
                string doneStringPaTvsh = $"\"{done}\"";
                taksa20 = Math.Truncate(taksa20 * 100) / 100;
                var VATAmt = taksa20 * Double.Parse("0.00") / 100;
                if (VATAmt.ToString() == "NaN")
                    VATAmt = 0;
                var VATAmt2 = String.Format("{0:0.00}", (VATAmt * Double.Parse(kursi)));
                var PriceBefVATPaTvsh = String.Format("{0:0.00}", (taksa20 * Double.Parse(kursi)));
                if (PriceBefVATPaTvsh == "NaN")
                    vleraPaTvshEArtikujve += 0;
                else
                    vleraPaTvshEArtikujve += Decimal.Parse(PriceBefVATPaTvsh);
                if (PriceBefVATPaTvsh == "NaN")
                    PriceBefVATPaTvsh = "0.00";
                PriceBefVATPaTvsh = $"\"{PriceBefVATPaTvsh}\"";
                tipiIPerjashtimit = $"\"{tipiIPerjashtimit}\"";
                VATAmt2 = $"\"{VATAmt2}\"";
                taxArray += "<SameTax NumOfItems =" + $"{doneStringPaTvsh}" + " PriceBefVAT=" + $"{PriceBefVATPaTvsh}" + " ExemptFromVAT=" + $"{tipiIPerjashtimit}" + "/>"; 
                taksa20 = 0;
                done = 0;
            }
            if (listaTrupiShitjeType2.Count > 0)
            {
                string tipiIPerjashtimit = "TYPE_2";
                foreach (var item in listaTrupiShitjeType2)
                {
                    taksa20 = taksa20 + item.VleftaPaTvsh * Double.Parse(String.Format("{0:0.00}", (1 - zbritjaPaTvsh2 / nenTotali2)));
                    done = done + 1;
                }
                var VATAmt = taksa20 * Double.Parse("0.00") / 100;
                var VATAmt2 = String.Format("{0:0.00}", (VATAmt * Double.Parse(kursi)));
                VATAmt2 = $"\"{VATAmt2}\"";
                string doneStringPaTvsh = $"\"{done}\"";
                taksa20 = Math.Truncate(taksa20 * 100) / 100;
                var PriceBefVATPaTvsh = String.Format("{0:0.00}", (taksa20 * Double.Parse(kursi)));
                if (PriceBefVATPaTvsh == "NaN")
                    vleraPaTvshEArtikujve += 0;
                else
                    vleraPaTvshEArtikujve += Decimal.Parse(PriceBefVATPaTvsh);
                PriceBefVATPaTvsh = $"\"{PriceBefVATPaTvsh}\"";
                tipiIPerjashtimit = $"\"{tipiIPerjashtimit}\"";
                taxArray += "<SameTax NumOfItems =" + $"{doneStringPaTvsh}" + " PriceBefVAT=" + $"{PriceBefVATPaTvsh}" + " ExemptFromVAT=" + $"{tipiIPerjashtimit}" + "/>"; 
                taksa20 = 0;
                done = 0;
            }

            
            string supplyDateOrPeriod = "";
            if(dateMbarimi != dateFature || dateFillimi != dateFature)
            {
                dateMbarimi = Convert.ToDateTime(dateMbarimi).ToString("yyyy-MM-dd");
                dateFillimi = Convert.ToDateTime(dateFillimi).ToString("yyyy-MM-dd");
                dateMbarimi = $"\"{dateMbarimi}\"";
                dateFillimi = $"\"{dateFillimi}\"";
                supplyDateOrPeriod = "<SupplyDateOrPeriod Start=" + $"{dateFillimi}" + " End=" + $"{dateMbarimi}" + "/>";
            }
            
            string fatureNeModifikim = "";
            if(isModifikim)
                fatureNeModifikim = " SubseqDelivType=\"TECHNICALERROR\"";
            if (dateFature.Split(' ')[0] != DateTime.Now.ToString("dd/MM/yyyy") && !isModifikim)
                fatureNeModifikim = " SubseqDelivType=\"BOUNDBOOK\"";
            string DateServeriOffset = clsKontrollePerFiskalizimin.ktheDatenEServeritOffset();
            DateTimeOffset dt = DateTimeOffset.Parse(DateServeriOffset);
            TimeSpan t = dt.Offset;
            DateTimeOffset sourceDate = new DateTimeOffset(dtRegjistrimi,
                         t);

            DateTimeOffset timezoneIShqiperise = TimeZoneInfo.ConvertTime(sourceDate,
                          TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time"));


            //string DateServeriOffsetAktuale = clsKontrollePerFiskalizimin.ktheDatenEServeritOffset();
            //DateTimeOffset dtAktuale = DateTimeOffset.Parse(DateServeriOffsetAktuale);
            //TimeSpan tAktuale = dtAktuale.Offset;
            //DateTimeOffset sourceDateAktuale = new DateTimeOffset(dateKrijimiFature.ToString("yyyy-MM-ddTHH\\:mm\\:sszzz"),
            //             tAktuale);

            //DateTimeOffset timezoneIShqiperiseAktuale = TimeZoneInfo.ConvertTime(sourceDateAktuale,
            //              TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time"));

            vleraPaTvsh = String.Format("{0:0.00}", Convert.ToDouble(vleraPaTvsh));
            nenTotali2 = Math.Truncate(100 * nenTotali2) / 100;
            tvsh = String.Format("{0:0.00}", Convert.ToDouble(tvsh) * Convert.ToDouble(kursi));
            if (totalFature.ToString() == "NaN")
                totalFature = 0;
            totali = String.Format("{0:0.00}", totalFature * Double.Parse(kursi));
            var dtRegjistrimiOffset = $"\"{timezoneIShqiperise.ToString("yyyy-MM-ddTHH\\:mm\\:sszzz")}\"";
            var dtRegjistrimiOffsetAktuale = $"\"{dateKrijimiFature.ToString("yyyy-MM-ddTHH\\:mm\\:sszzz")}\"";

            iic = $"\"{iic}\"";
            iicSignature = $"\"{iicSignature}\"";
            softNum = $"\"{softNum}\"";
            if (adresaFaturimit == "")
                adresaFaturimit = " ";
            pershkrimi = $"\"{pershkrimi}\"";
            totali = $"\"{totali}\"";
            dtMaturimi = $"\"{dtMaturimi}\"";

            vleraPaTvsh = $"\"{vleraPaTvsh}\"";
            perqindjeZbritja = $"\"{perqindjeZbritja}\"";
            if (emerKlienti == "")
                emerKlienti = " ";
            emerKlienti = $"\"{emerKlienti}\"";
            if (niptK == "")
                niptK = " ";
            niptK = $"\"{niptK}\"";
            tvsh = $"\"{tvsh}\"";
            string txtNumer2 = $"\"{txtNumer}\"";
            kodOperatori = $"\"{kodOperatori}\"";
            string menyrePag = "";
            string ndermarrjeQyteti = "";
            if (ndermarje.NdermarrjeQytetiPershkrimi != "")
            {
                clsQyteti qytetiNdermarrjes = new clsQyteti(ndermarje.NdermarrjeQytetiPershkrimi, ndermarje.IdNdermarrje);
                ndermarrjeQyteti = qytetiNdermarrjes.EmriQyteti;
            }
            string ndermarrjeEmri = ndermarje.NdermarrjePershkrimi;
            string ndermarjeNIPT = ndermarje.NdermarrjeNipt;
            string ndermarjeKodBiznesi = kodBiznesi;
            string ndermarrjeVendi = ndermarje.NdermarrjeVendi;
            string klientiQyteti = "";
            if (klienti.EmriQytetitKF != "")
            {
                clsQyteti qytetiKlientit = new clsQyteti(klienti.EmriQytetitKF, ndermarje.IdNdermarrje);
                klientiQyteti = qytetiKlientit.EmriQyteti;
            }
            string klientiAutongarkes = klienti.AutoNgarkese.ToString();
            string einvoicechecked = einvoice.ToString();
            string klientiEmri = klienti.EmertimiKF;
            string klientiVendi = "";
            if (klienti.ShtetiKF != "")
            {
                klientiVendi = klienti.ShtetiKF;
            }
            string klientiNIPT = klienti.NiptiKF;
            string KlientiTipi = klienti.TipiId;
            //string klientiAdresa = klienti.OColAdresat[0].Adresa.ToString().Replace("\"", "");
            iicFatureOrigjinale = $"\"{iicFatureOrigjinale}\"";
            nivfFatureOrigjinale = $"{nivfFatureOrigjinale}";
            //dtRegjistrimiFatureOrigjinale = $"{dtRegjistrimi.ToString("yyyy-MM-ddTHH\\:mm\\:sszzz")}";
            string vitiTani = DateTime.Now.ToString("yyyy");
            var metodPagimi = "";
            if (menyrePagese == "Pagese Automatike")
            {
                txtNumer = $"{txtNumer}/{viti}/{tcr}";
                menyrePag = $"\"CASH\"";
                metodPagimi = $"\"BANKNOTE\"";
            }
            else if(menyrePagese == "Karte krediti")
            {
                txtNumer = $"{txtNumer}/{viti}/{tcr}";
                menyrePag = $"\"CASH\"";
                metodPagimi = $"\"CARD\"";  
            }
            else
            {
                txtNumer = $"{txtNumer}/{viti}";
                menyrePag = $"\"NONCASH\"";
                metodPagimi = $"\"ACCOUNT\"";
            }
            switch (KlientiTipi)
            {
                case "":
                    KlientiTipi = "";
                    break;
                case "NUIS":
                    KlientiTipi = "NUIS";
                    break;
                case "ID":
                    KlientiTipi = "ID";
                    break;
                case "PASS":
                    KlientiTipi = "PASS";
                    break;
                case "VAT":
                    KlientiTipi = "VAT";
                    break;
                case "TAX":
                    KlientiTipi = "TAX";
                    break;
                case "SOC":
                    KlientiTipi = "SOC";
                    break;
            }
            ndermarrjeQyteti = $"\"{ndermarrjeQyteti}\"";
            ndermarrjeEmri = $"\"{ndermarrjeEmri}\"";
            ndermarrjeVendi = $"\"{ndermarrjeVendi}\"";
            ndermarjeNIPT = $"\"{ndermarjeNIPT}\"";
            ndermarjeKodBiznesi = $"\"{ndermarjeKodBiznesi}\"";
            klientiQyteti = $"\"{klientiQyteti}\"";
            klientiEmri = $"\"{klientiEmri}\"";
            klientiAutongarkes = $"\"{klientiAutongarkes.ToLower()}\"";
            einvoicechecked = $"\"{einvoicechecked.ToLower()}\"";
            klientiVendi = $"\"{klientiVendi}\"";
            klientiNIPT = $"\"{klientiNIPT}\"";
            KlientiTipi = $"\"{KlientiTipi}\"";
            //klientiAdresa = $"\"{klientiAdresa}\"";
            txtNumer = $"\"{txtNumer}\"";
            tcr = $"\"{tcr}\"";
            var buyerInfo = "";
            Regex rgx = new Regex("[^a-zA-Z0-9 -]");
            adresaFaturimit = rgx.Replace(adresaFaturimit, "");

            adresaFaturimit = $"\"{adresaFaturimit}\"";

            clsMonedha monedhaKlientit = new clsMonedha(klienti.idMonedha);
            if (monedhaKlientit.KodiMonedha == "LEK")
                currency = currency;
            else
            {
                string kodiMonedhes = monedhaKlientit.KodiMonedha;
                kursi = $"\"{kursi}\"";
                kodiMonedhes = $"\"{kodiMonedhes}\"";

                currency += "<Currency Code=" + $"{kodiMonedhes}" + " ExRate=" + $"{kursi}" + "></Currency>";
            }
            if (KlientiTipi == "\"\"")
                buyerInfo = buyerInfo;
            else
            {
                string qytetiKlientiTag = "";
                string shtetiKlientiTag = "";
                if (klienti.EmriQytetitKF != "")
                    qytetiKlientiTag = "Town=" + $"{klientiQyteti}" + "";
                if (klienti.ShtetiKF != "")
                    shtetiKlientiTag = "Country =" + $"{klientiVendi}" + "";
                if ((tipiAutongarkes == "ABROAD" || tipiAutongarkes == "") && isShitje)
                    buyerInfo = "<Buyer Address=" + $"{adresaFaturimit}" + " " + qytetiKlientiTag + " " + shtetiKlientiTag + " IDNum=" + $"{klientiNIPT}" + " IDType=" + $"{KlientiTipi}" + " Name= " + $"{klientiEmri}" + "/>";
                else
                    buyerInfo = "<Seller Address=" + $"{adresaFaturimit}" + " " + qytetiKlientiTag + " " + shtetiKlientiTag + " IDNum=" + $"{klientiNIPT}" + " IDType=" + $"{KlientiTipi}" + " Name= " + $"{klientiEmri}" + "/>";
            }
            string clientInfo = "";

            string tipiIVetfaturimit = "";
            tipiAutongarkes = $"\"{tipiAutongarkes}\"";
            if (tipiAutongarkes == "\"\"")
                tipiIVetfaturimit = "";
            else
                tipiIVetfaturimit = " TypeOfSelfIss=" + $"{tipiAutongarkes}" + "";

            string qytetiNdermarrjeTag = "";
            ndermarrjeVendi = rgx.Replace(ndermarrjeVendi, "");
           
            ndermarrjeVendi = $"\"{ndermarrjeVendi}\"";
            if (ndermarrjeQyteti != "\"\"")
            {
                qytetiNdermarrjeTag = " Town=" + $"{ndermarrjeQyteti}" + "";
            }
            if ((tipiAutongarkes == "\"ABROAD\"" || tipiAutongarkes == "\"\"") && isShitje)
                clientInfo = "<Seller Address=" + $"{ndermarrjeVendi}" + " IDNum=" + $"{ndermarjeNIPT}" + qytetiNdermarrjeTag + " IDType =\"NUIS\" Country=\"ALB\" Name= " + $"{ndermarrjeEmri}" + "/>";
            else
                clientInfo = "<Buyer Address=" + $"{ndermarrjeVendi}" + " IDNum=" + $"{ndermarjeNIPT}" + qytetiNdermarrjeTag + " IDType =\"NUIS\" Country=\"ALB\" Name= " + $"{ndermarrjeEmri}" + "/>";
            var vleraPaTvshEArtikujve2 = String.Format("{0:0.00}", Math.Truncate(vleraPaTvshEArtikujve * 100) / 100);
            vleraPaTvshEArtikujve2 = $"\"{vleraPaTvshEArtikujve2}\"";
            string GoodsExAmt = "";
            string sameTaxes = "<SameTaxes> " + taxArray + " </SameTaxes>";
            if (taxArray == "")
                sameTaxes = "";
            if (dogane)
            {
                GoodsExAmt = $" GoodsExAmt="+ $"{totali}" +"";
                vleraPaTvshEArtikujve2 = totali;
                sameTaxes = "";
            }
            if (menyrePag == "\"CASH\"" && fatureNeModifikim == "")
                dtRegjistrimiOffsetAktuale = dtRegjistrimiOffset;
            const string XML_SCHEMA_NS = "https://eFiskalizimi.tatime.gov.al/FiscalizationService/schema";
            const String XML_REQUEST_ID = "Request";
            const String XML_SIG_METHOD = "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256";
            const String XML_DIG_METHOD = "http://www.w3.org/2001/04/xmlenc#sha256";
            string REQUEST_TO_SIGN = "";
            if (nderm.MeTvsh)
            {
                if (!string.IsNullOrEmpty(nivfKthim))
                {
                    REQUEST_TO_SIGN =
                   "<RegisterInvoiceRequest " +
                   " xmlns=\"https://eFiskalizimi.tatime.gov.al/FiscalizationService/schema\" " +
                   " xmlns:ns2=\"http://www.w3.org/2000/09/xmldsig#\" " +
                   " Id=\"Request\" " +
                   " Version=\"3\">\r\n" +
                   " <Header SendDateTime=" + $"{dtRegjistrimiOffsetAktuale}" + fatureNeModifikim + " UUID=" + $"{uuid}" + "/>\r\n" +
                   " <Invoice BusinUnitCode=" + $"{ndermarjeKodBiznesi}" + " IIC=" + $"{iic}" + " IICSignature=" + $"{iicSignature}" + " InvNum= " + $"{txtNumer}" + " InvOrdNum=" + $"{txtNumer2}" + "  IsIssuerInVAT=\"true\" IsEinvoice =" + $"{einvoicechecked}" + " IsReverseCharge=" + $"{klientiAutongarkes}" + GoodsExAmt + tipiIVetfaturimit + " IsSimplifiedInv=\"false\" IssueDateTime=" + $"{dtRegjistrimiOffset}" + " OperatorCode=" + $"{kodOperatori}" + "  PayDeadline= " + $"{dtMaturimi}" + " SoftCode=" + $"{softNum}" + " TCRCode=" + $"{tcr}" + " TotPrice=" + $"{totali}" + " TotPriceWoVAT=" + $"{vleraPaTvshEArtikujve2}" + taxFreeAmt + markUpAmt+" TotVATAmt=" + $"{tvsh}" + " TypeOfInv=" + $"{menyrePag}" + "><CorrectiveInv IICRef=" + $"{iicFatureOrigjinale}" + " IssueDateTime=" + $"{dateOrigjinaleKrijimi}" + " Type=\"CORRECTIVE\"/>"+ supplyDateOrPeriod + "<PayMethods><PayMethod Amt=" + $"{totali}" + " Type=" + $"{metodPagimi}" + "/></PayMethods>" + $"{currency}" + clientInfo + buyerInfo + "<Items>" + artikujtarray + "</Items>"+ sameTaxes +"</Invoice>\r\n" + "</RegisterInvoiceRequest>";
                }
                else if (!string.IsNullOrEmpty(nivfKthim) && tot < 0)
                {
                    REQUEST_TO_SIGN =
                    "<RegisterInvoiceRequest " +
                    " xmlns=\"https://eFiskalizimi.tatime.gov.al/FiscalizationService/schema\" " +
                    " xmlns:ns2=\"http://www.w3.org/2000/09/xmldsig#\" " +
                    " Id=\"Request\" " +
                    " Version=\"3\">\r\n" +
                    " <Header SendDateTime=" + $"{dtRegjistrimiOffsetAktuale}" + fatureNeModifikim + " UUID =" + $"{uuid}" + "/>\r\n" +
                   " <Invoice BusinUnitCode=" + $"{ndermarjeKodBiznesi}" + " IIC=" + $"{iic}" + " IICSignature=" + $"{iicSignature}" + " InvNum= " + $"{txtNumer}" + " InvOrdNum=" + $"{txtNumer2}" + "  IsIssuerInVAT=\"true\" IsEinvoice =" + $"{einvoicechecked}" + " IsReverseCharge=" + $"{klientiAutongarkes}" + GoodsExAmt + tipiIVetfaturimit + " IsSimplifiedInv=\"false\" IssueDateTime=" + $"{dtRegjistrimiOffset}" + " OperatorCode=" + $"{kodOperatori}" + "  PayDeadline= " + $"{dtMaturimi}" + " SoftCode=" + $"{softNum}" + " TCRCode=" + $"{tcr}" + " TotPrice=" + $"{totali}" + " TotPriceWoVAT=" + $"{vleraPaTvshEArtikujve2}" + taxFreeAmt + markUpAmt+" TotVATAmt=" + $"{tvsh}" + " TypeOfInv=" + $"{menyrePag}" + "><CorrectiveInv IICRef=" + $"{iicFatureOrigjinale}" + "IssueDateTime=" + $"{dateOrigjinaleKrijimi}" + "Type=\"CORRECTIVE\"/>" + supplyDateOrPeriod + "<PayMethods><PayMethod Amt=" + $"{totali}" + " Type=" + $"{metodPagimi}" + "/></PayMethods>" + $"{currency}" + clientInfo + buyerInfo + "<Items>" + artikujtarray + "</Items>" + sameTaxes + "</Invoice>\r\n" +
                    "</RegisterInvoiceRequest>";
                }
                else
                {

                    REQUEST_TO_SIGN =
                    "<RegisterInvoiceRequest " +
                    " xmlns=\"https://eFiskalizimi.tatime.gov.al/FiscalizationService/schema\" " +
                    " xmlns:ns2=\"http://www.w3.org/2000/09/xmldsig#\" " +
                    " Id=\"Request\" " +
                    " Version=\"3\">\r\n" +
                    " <Header SendDateTime=" + $"{dtRegjistrimiOffsetAktuale}" + fatureNeModifikim + " UUID =" + $"{uuid}" + "/>\r\n" +
                   " <Invoice BusinUnitCode=" + $"{ndermarjeKodBiznesi}" + " IIC=" + $"{iic}" + " IICSignature=" + $"{iicSignature}" + " InvNum= " + $"{txtNumer}" + " InvOrdNum=" + $"{txtNumer2}" + "  IsIssuerInVAT=\"true\" IsEinvoice =" + $"{einvoicechecked}" + " IsReverseCharge=" + $"{klientiAutongarkes}" + GoodsExAmt + tipiIVetfaturimit + " IsSimplifiedInv=\"false\" IssueDateTime=" + $"{dtRegjistrimiOffset}" + " OperatorCode=" + $"{kodOperatori}" + "  PayDeadline= " + $"{dtMaturimi}" + " SoftCode=" + $"{softNum}" + " TCRCode=" + $"{tcr}" + " TotPrice=" + $"{totali}" + " TotPriceWoVAT=" + $"{vleraPaTvshEArtikujve2}" + taxFreeAmt + markUpAmt+" TotVATAmt=" + $"{tvsh}" + " TypeOfInv=" + $"{menyrePag}" + ">" + supplyDateOrPeriod + "<PayMethods><PayMethod Amt=" + $"{totali}" + " Type=" + $"{metodPagimi}" + "/></PayMethods>" + $"{currency}" + clientInfo + buyerInfo + "<Items>" + artikujtarray + "</Items>" + sameTaxes + "</Invoice>\r\n" +
                    "</RegisterInvoiceRequest>";
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(nivfKthim))
                {
                    REQUEST_TO_SIGN =
                   "<RegisterInvoiceRequest " +
                   " xmlns=\"https://eFiskalizimi.tatime.gov.al/FiscalizationService/schema\" " +
                   " xmlns:ns2=\"http://www.w3.org/2000/09/xmldsig#\" " +
                   " Id=\"Request\" " +
                   " Version=\"3\">\r\n" +
                   " <Header SendDateTime=" + $"{dtRegjistrimiOffsetAktuale}" + fatureNeModifikim +" UUID=" + $"{uuid}" + "/>\r\n" +
                   " <Invoice BusinUnitCode=" + $"{ndermarjeKodBiznesi}" + " IIC=" + $"{iic}" + " IICSignature=" + $"{iicSignature}" + " InvNum= " + $"{txtNumer}" + " InvOrdNum=" + $"{txtNumer2}" + "  IsIssuerInVAT=\"false\" IsEinvoice =" + $"{einvoicechecked}" + " IsReverseCharge=" + $"{klientiAutongarkes}" + GoodsExAmt + tipiIVetfaturimit + " IsSimplifiedInv=\"false\" IssueDateTime=" + $"{dtRegjistrimiOffset}" + " OperatorCode=" + $"{kodOperatori}" + "  PayDeadline= " + $"{dtMaturimi}" + " SoftCode=" + $"{softNum}" + " TCRCode=" + $"{tcr}" + " TotPrice=" + $"{totali}" + " TotPriceWoVAT=" + $"{vleraPaTvshEArtikujve2}" + " TaxFreeAmt=" + $"{vleraPaTvshEArtikujve2}" + markUpAmt+" TypeOfInv=" + $"{menyrePag}" + "><CorrectiveInv IICRef=" + $"{iicFatureOrigjinale}" + " IssueDateTime=" + $"{dateOrigjinaleKrijimi}" + " Type=\"CORRECTIVE\"/>" + supplyDateOrPeriod + "<PayMethods><PayMethod Amt=" + $"{totali}" + " Type=" + $"{metodPagimi}" + "/></PayMethods>" + $"{currency}" + clientInfo + buyerInfo + "<Items>" + artikujtarray + "</Items></Invoice>\r\n" + "</RegisterInvoiceRequest>";
                }
                else if (!string.IsNullOrEmpty(nivfKthim) && tot < 0)
                {
                    REQUEST_TO_SIGN =
                    "<RegisterInvoiceRequest " +
                    " xmlns=\"https://eFiskalizimi.tatime.gov.al/FiscalizationService/schema\" " +
                    " xmlns:ns2=\"http://www.w3.org/2000/09/xmldsig#\" " +
                    " Id=\"Request\" " +
                    " Version=\"3\">\r\n" +
                    " <Header SendDateTime=" + $"{dtRegjistrimiOffsetAktuale}" + fatureNeModifikim + " UUID =" + $"{uuid}" + "/>\r\n" +
                   " <Invoice BusinUnitCode=" + $"{ndermarjeKodBiznesi}" + " IIC=" + $"{iic}" + " IICSignature=" + $"{iicSignature}" + " InvNum= " + $"{txtNumer}" + " InvOrdNum=" + $"{txtNumer2}" + "  IsIssuerInVAT=\"false\" IsEinvoice =" + $"{einvoicechecked}" + " IsReverseCharge=" + $"{klientiAutongarkes}" + GoodsExAmt + tipiIVetfaturimit + " IsSimplifiedInv=\"false\" IssueDateTime=" + $"{dtRegjistrimiOffset}" + " OperatorCode=" + $"{kodOperatori}" + "  PayDeadline= " + $"{dtMaturimi}" + " SoftCode=" + $"{softNum}" + " TCRCode=" + $"{tcr}" + " TotPrice=" + $"{totali}" + " TotPriceWoVAT=" + $"{vleraPaTvshEArtikujve2}" + " TaxFreeAmt=" + $"{vleraPaTvshEArtikujve2}" + markUpAmt+" TypeOfInv=" + $"{menyrePag}" + "><CorrectiveInv IICRef=" + $"{iicFatureOrigjinale}" + "IssueDateTime=" + $"{dateOrigjinaleKrijimi}" + "Type=\"CORRECTIVE\"/>" + supplyDateOrPeriod + "<PayMethods><PayMethod Amt=" + $"{totali}" + " Type=" + $"{metodPagimi}" + "/></PayMethods>" + $"{currency}" + clientInfo + buyerInfo + "<Items>" + artikujtarray + "</Items></Invoice>\r\n" +
                    "</RegisterInvoiceRequest>";
                }
                else
                {
                    REQUEST_TO_SIGN =
                    "<RegisterInvoiceRequest " +
                    " xmlns=\"https://eFiskalizimi.tatime.gov.al/FiscalizationService/schema\" " +
                    " xmlns:ns2=\"http://www.w3.org/2000/09/xmldsig#\" " +
                    " Id=\"Request\" " +
                    " Version=\"3\">\r\n" +
                    " <Header SendDateTime=" + $"{dtRegjistrimiOffsetAktuale}" + fatureNeModifikim + " UUID =" + $"{uuid}" + "/>\r\n" +
                   " <Invoice BusinUnitCode=" + $"{ndermarjeKodBiznesi}" + " IIC=" + $"{iic}" + " IICSignature=" + $"{iicSignature}" + " InvNum= " + $"{txtNumer}" + " InvOrdNum=" + $"{txtNumer2}" + "  IsIssuerInVAT=\"false\" IsEinvoice =" + $"{einvoicechecked}" + " IsReverseCharge=" + $"{klientiAutongarkes}" + GoodsExAmt + tipiIVetfaturimit + " IsSimplifiedInv=\"false\" IssueDateTime=" + $"{dtRegjistrimiOffset}" + " OperatorCode=" + $"{kodOperatori}" + "  PayDeadline= " + $"{dtMaturimi}" + " SoftCode=" + $"{softNum}" + " TCRCode=" + $"{tcr}" + " TotPrice=" + $"{totali}" + " TotPriceWoVAT=" + $"{vleraPaTvshEArtikujve2}" + " TaxFreeAmt=" + $"{vleraPaTvshEArtikujve2}" + markUpAmt+" TypeOfInv=" + $"{menyrePag}" + ">" + supplyDateOrPeriod + "<PayMethods><PayMethod Amt=" + $"{totali}" + " Type=" + $"{metodPagimi}" + "/></PayMethods>" + $"{currency}" + clientInfo + buyerInfo + "<Items>" + artikujtarray + "</Items></Invoice>\r\n" +
                    "</RegisterInvoiceRequest>";
                }
            }
            REQUEST_TO_SIGN = REQUEST_TO_SIGN.Replace("&","&amp;");
            string[] result = new string[2];
            string passpath = nderm.Pathname + $"/password.txt";
            String KEYSTORE_PASS = "";
            byte[] encrypted;
            String KEYSTORE_LOCATION = System.Web.Hosting.HostingEnvironment.MapPath(nderm.Pathname) + @"certifikata.p12";
            if (File.Exists(System.Web.Hosting.HostingEnvironment.MapPath(nderm.Pathname) + @"password.txt"))
                KEYSTORE_PASS = File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath(nderm.Pathname) + @"password.txt");
            else throw new Exception("Ju lutem ngarkoni filen e passwordit!");
            using (X509Certificate2 keyStore = new X509Certificate2(KEYSTORE_LOCATION, KEYSTORE_PASS))
            {
                try
                {
                    // Load a private from a key store
                    RSA privateKey = keyStore.GetRSAPrivateKey();
                    // Convert string XML to object
                    XmlDocument request = new XmlDocument();
                    request.LoadXml(REQUEST_TO_SIGN);
                    // Create key info element
                    KeyInfo keyInfo = new KeyInfo();
                    KeyInfoX509Data keyInfoData = new KeyInfoX509Data();
                    keyInfoData.AddCertificate(keyStore);
                    keyInfo.AddClause(keyInfoData);
                    // Create signature reference
                    Reference reference = new Reference("");
                    reference.AddTransform(new XmlDsigEnvelopedSignatureTransform(false));
                    reference.AddTransform(new XmlDsigExcC14NTransform(false));
                    reference.DigestMethod = XML_DIG_METHOD;
                    reference.Uri = "#" + XML_REQUEST_ID;
                    // Create signature
                    SignedXml xml = new SignedXml(request);
                    xml.SigningKey = privateKey;
                    xml.SignedInfo.CanonicalizationMethod = SignedXml.XmlDsigExcC14NTransformUrl;
                    xml.SignedInfo.SignatureMethod = XML_SIG_METHOD;
                    xml.KeyInfo = keyInfo;
                    xml.AddReference(reference);
                    xml.ComputeSignature();
                    //= Add signature element to the request
                    XmlElement signature = xml.GetXml();
                    request.DocumentElement.AppendChild(signature);
                    // Convert signed request to string and print
                    StringWriter sw = new StringWriter();
                    XmlTextWriter xw = new XmlTextWriter(sw);
                    request.WriteTo(xw);
                    var signedDoc = sw.ToString();
                    if (eshteGrup)
                        signedDoc = signedDoc;
                    else
                        signedDoc = "<SOAP-ENV:Envelope xmlns:SOAP-ENV=\"http://schemas.xmlsoap.org/soap/envelope/\"><SOAP-ENV:Header/><SOAP-ENV:Body>" + signedDoc + "</SOAP-ENV:Body></SOAP-ENV:Envelope>";
                    result[0] = signedDoc;
                    result[1] = REQUEST_TO_SIGN;
                    return result;

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return result;
        }
        public static string gjeneroMesazhEInvoice(clsNdermarrje nderm, string fatureUBL, DateTime dtRegjistrimi)
        {
            var uuid = Guid.NewGuid().ToString();
            string DateServeriOffset = clsKontrollePerFiskalizimin.ktheDatenEServeritOffset();
            DateTimeOffset dt = DateTimeOffset.Parse(DateServeriOffset);
            TimeSpan t = dt.Offset;
            DateTimeOffset sourceDate = new DateTimeOffset(dtRegjistrimi,
                         t);

            DateTimeOffset timezoneIShqiperise = TimeZoneInfo.ConvertTime(sourceDate,
                          TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time"));

            var dateDergimi = $"\"{dtRegjistrimi.ToString("yyyy-MM-ddTHH\\:mm\\:sszzz")}\"";
            uuid = $"\"{uuid}\"";
            const String XML_SCHEMA_NS = "https://Einvoice.tatime.gov.al/ EinvoiceService/schema";
            const String XML_SIG_METHOD = "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256";
            const String XML_REQUEST_ID = "Request";
            const String XML_DIG_METHOD = "http://www.w3.org/2001/04/xmlenc#sha256";
            String REQUEST_TO_SIGN =
            "<RegisterEinvoiceRequest " +
            " xmlns=\"https://Einvoice.tatime.gov.al/EinvoiceService/schema\" " +
            " xmlns:ns2=\"http://www.w3.org/2000/09/xmldsig#\" " +
            " Id=\"Request\" " +
            " Version=\"1\">\r\n" +
            " <Header SendDateTime=" + $"{dateDergimi}" + " UUID=" + $"{uuid}" + "/>\r\n" +
            " <EinvoiceEnvelope>" +
                "<UblInvoice>" + $"{fatureUBL}" + "</UblInvoice>" +
              "</EinvoiceEnvelope>\r\n" +
            "</RegisterEinvoiceRequest>";
            REQUEST_TO_SIGN = REQUEST_TO_SIGN.Replace("&", "&amp;");
            string passpath = nderm.Pathname + $"/password.txt";
            String KEYSTORE_PASS = "";
            byte[] encrypted;
            String KEYSTORE_LOCATION = System.Web.Hosting.HostingEnvironment.MapPath(nderm.Pathname) + @"certifikata.p12";
            if (File.Exists(System.Web.Hosting.HostingEnvironment.MapPath(nderm.Pathname) + @"password.txt"))
                KEYSTORE_PASS = File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath(nderm.Pathname) + @"password.txt");
            else throw new Exception("Ju lutem ngarkoni filen e passwordit!");

            using (X509Certificate2 keyStore = new X509Certificate2(KEYSTORE_LOCATION, KEYSTORE_PASS))
            {
                try
                {
                    // Load a private from a key store
                    RSA privateKey = keyStore.GetRSAPrivateKey();
                    // Convert string XML to object
                    XmlDocument request = new XmlDocument();
                    request.LoadXml(REQUEST_TO_SIGN);
                    // Create key info element
                    KeyInfo keyInfo = new KeyInfo();
                    KeyInfoX509Data keyInfoData = new KeyInfoX509Data();
                    keyInfoData.AddCertificate(keyStore);
                    keyInfo.AddClause(keyInfoData);
                    // Create signature reference
                    Reference reference = new Reference("");
                    reference.AddTransform(new XmlDsigEnvelopedSignatureTransform(false));
                    reference.AddTransform(new XmlDsigExcC14NTransform(false));
                    reference.DigestMethod = XML_DIG_METHOD;
                    reference.Uri = "#" + XML_REQUEST_ID;
                    // Create signature
                    SignedXml xml = new SignedXml(request);
                    xml.SigningKey = privateKey;
                    xml.SignedInfo.CanonicalizationMethod = SignedXml.XmlDsigExcC14NTransformUrl;
                    xml.SignedInfo.SignatureMethod = XML_SIG_METHOD;
                    xml.KeyInfo = keyInfo;
                    xml.AddReference(reference);
                    xml.ComputeSignature();
                    // Add signature element to the request
                    XmlElement signature = xml.GetXml();
                    request.DocumentElement.AppendChild(signature);
                    // Convert signed request to string and print
                    StringWriter sw = new StringWriter();
                    XmlTextWriter xw = new XmlTextWriter(sw);
                    request.WriteTo(xw);
                    var signedDoc = sw.ToString();
                    signedDoc = "<SOAP-ENV:Envelope xmlns:SOAP-ENV=\"http://schemas.xmlsoap.org/soap/envelope/\"><SOAP-ENV:Header/><SOAP-ENV:Body>" + signedDoc + "</SOAP-ENV:Body></SOAP-ENV:Envelope>";
                    return signedDoc;
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            return "Ndodhi nje gabim!";
        }
        public static string[] gjeneroFatureUBL(clsNdermarrje nderm, string nrDok, string vitiTani, string dateDergimi, string dateMaturimi, string iic, string iicSignature, string nivf, DateTime dateDergimiTimeZone, string kodOperatori, string kodBiznesi, string kodSoftueri, string kodMonedha, string niptNdermarrje, string emerNdermarrje, string adresNdermarrje, string qytetiNdermarrje, string shtetNdermarrje, string kodIdnetifikimi, double totali, decimal vleftaMeTvsh, string vleftaPaTvsh, double tvsh, string pershkrimArtikulli, string emerArtikulli, int sasia, string emerKlienti, string niptKlienti, string adresKlienti, string qytetiKlienti, string shtetiKlienti, string menyrePagese, string vleraTvsh, double cmimiIPaguar, double cmimArtikulli, string kodTvsh, string kodTvshReason, double zbritje, double zbritjePerqindje, colTrupiShitje trupShitje, double shumaEParapaguar, double roundingShumaEParapaguar, string llojDokumenti, string procesi, double kursi,string dateFillimi,string dateMbarimi,string dateFature,int idPerdoruesi,int idNderm, string QueryString,clsKlientFurnitor klientFurnitor,string pershkrimFature)
        {
            string DateServeriOffset = clsKontrollePerFiskalizimin.ktheDatenEServeritOffset();
            DateTimeOffset dt = DateTimeOffset.Parse(DateServeriOffset);
            TimeSpan t = dt.Offset;
            DateTimeOffset sourceDate = new DateTimeOffset(dateDergimiTimeZone,
                         t);

            DateTimeOffset timezoneIShqiperise = TimeZoneInfo.ConvertTime(sourceDate,
                          TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time"));

            var dateDergimiSpecifike = timezoneIShqiperise.ToString("yyyy-MM-ddTHH\\:mm\\:sszzz");
            dateDergimi = dateDergimi.Split(' ')[0];
            dateDergimi = dateDergimi.Replace("/", "-");
            dateMaturimi = dateMaturimi.Split(' ')[0];
            dateMaturimi = dateMaturimi.Replace("/", "-");
            string[] dateDergimiList = dateDergimi.Split(new[] { '-' }, 3);
            string[] dateMaturimiList = dateMaturimi.Split(new[] { '-' }, 3);
            dateDergimi = string.Format("{0}-{1}-{2}",
                            dateDergimiList[2], dateDergimiList[1], dateDergimiList[0]);
            dateMaturimi = string.Format("{0}-{1}-{2}",
                            dateMaturimiList[2], dateMaturimiList[1], dateMaturimiList[0]);
            string klientiIdTax = "";
            zbritje = 0;
            string ndermarrjeIdTax = "";
            decimal taxableamount;
            string idProfili = nrDok + "/" + vitiTani;
            string taxId = $"\"{kodMonedha}\"";
            string taxCategory = "";
            string allowanceCharge = "";
            string allowanceTotalAmount = "";
            string taxExclusiveAmount = "";
            string taxInclusiveAmount = "";
            string payableAlternativeAmount = "";
            var vleftaPaTvsh2 = String.Format("{0:0.00}", vleftaPaTvsh);
            zbritje = Convert.ToDouble(String.Format("{0:0.00}", zbritje));
            var tvsh2 = decimal.Parse(tvsh.ToString());
            string taxableamount2 = "";
            string payableRoundingAmount = "";
            string prepaidAmount = "";
            string chargeTotalAmount = "";
            string payableAmount = "";
            string percent = "";
            string items = "";
            string lineExtensionAmount = "";
            decimal vleraMeTvsh2;
            decimal vleftaPaTvshPaZbritje = Decimal.Parse(vleftaPaTvsh) - Decimal.Parse(zbritje.ToString());
            vleftaPaTvshPaZbritje = decimal.Parse(String.Format("{0:0.00}", vleftaPaTvshPaZbritje));
            decimal shumaEPagueshme;
            double vleraEArtikujve = 0.00;
            double chargeAmount = 0.00;
            string InvoicePeriod = "";
            string taxCurrencyCode = "";
            if (kodMonedha != "ALL")
            {
                taxCurrencyCode = "<TaxCurrencyCode>ALL</TaxCurrencyCode>";
            }
            if (dateMbarimi != dateFature || dateFillimi != dateFature)
            {
                dateMbarimi = Convert.ToDateTime(dateMbarimi).ToString("yyyy-MM-dd");
                dateFillimi = Convert.ToDateTime(dateFillimi).ToString("yyyy-MM-dd");
                InvoicePeriod = "<ns3:InvoicePeriod><StartDate>"+dateFillimi+ "</StartDate><EndDate>" + dateMbarimi + "</EndDate></ns3:InvoicePeriod>";
            }
            if (kodTvsh == "S" && kodTvshReason == "vatex-eu-s")
            {
                payableRoundingAmount = "<PayableRoundingAmount currencyID=" + $"{taxId}" + ">" + $"{roundingShumaEParapaguar}" + "</PayableRoundingAmount>";
                prepaidAmount = "<PrepaidAmount currencyID=" + $"{taxId}" + ">" + $"{shumaEParapaguar}" + "</PrepaidAmount>";
                klientiIdTax = "VAT";
                ndermarrjeIdTax = "VAT";
            }
            else if (kodTvsh == "G" && kodTvshReason == "vatex-eu-g")
            {
                payableRoundingAmount = "<PayableRoundingAmount currencyID=" + $"{taxId}" + ">" + $"{roundingShumaEParapaguar}" + "</PayableRoundingAmount>";
                prepaidAmount = "<PrepaidAmount currencyID=" + $"{taxId}" + ">" + $"{shumaEParapaguar}" + "</PrepaidAmount>";
                klientiIdTax = "VAT";
                ndermarrjeIdTax = "VAT";
            }
            else if (kodTvsh == "Z" && kodTvshReason == "vatex-eu-z")
            {
                payableRoundingAmount = "<PayableRoundingAmount currencyID=" + $"{taxId}" + ">" + $"{roundingShumaEParapaguar}" + "</PayableRoundingAmount>";
                prepaidAmount = "<PrepaidAmount currencyID=" + $"{taxId}" + ">" + $"{shumaEParapaguar}" + "</PrepaidAmount>";
                klientiIdTax = "VAT";
                ndermarrjeIdTax = "VAT";
            }
            else if (kodTvsh == "AE" && kodTvshReason == "vatex-eu-ae")
            {
                payableRoundingAmount = "<PayableRoundingAmount currencyID=" + $"{taxId}" + ">" + $"{roundingShumaEParapaguar}" + "</PayableRoundingAmount>";
                prepaidAmount = "<PrepaidAmount currencyID=" + $"{taxId}" + ">" + $"{shumaEParapaguar}" + "</PrepaidAmount>";
                klientiIdTax = "VAT";
                ndermarrjeIdTax = "VAT";
            }
            else if (kodTvsh == "O" && kodTvshReason == "vatex-eu-o")
            {
                payableRoundingAmount = "<PayableRoundingAmount currencyID=" + $"{taxId}" + ">" + $"{roundingShumaEParapaguar}" + "</PayableRoundingAmount>";
                prepaidAmount = "<PrepaidAmount currencyID=" + $"{taxId}" + ">" + $"{shumaEParapaguar}" + "</PrepaidAmount>";
                klientiIdTax = "FRE";
                ndermarrjeIdTax = "FRE";
            }
            List<string> arrayTvsh = new List<string>();
            string taxArray = "";
            List<int> artikujtInt = new List<int>();
            List<string> derguarTax = new List<string>();
            for (var i = 0; i < trupShitje.Count; i++)
                artikujtInt.Add(i);
            int done = 1;
            double vleftaTvsh = 0;
            double vleftaTvshMeZbritje = 0;
            double tvshEVlerave = 0;
            double zbritjeArtikujsh = 0;
            bool vazhdimTagu = false;
            int VR;
            string VR2 = "";
            string VR3 = "";
            string taxSubtotal = "";
            double taxAmount = 0;
            double zbritjeTotaleArtikulli = 0;
            string perjashtimiArt = "";
            List<string> perjashtimiTvsh = new List<string>();
            List<int> listTaksash = new List<int>();
            List<clsTrupiShitje> listaTrupiShitje = new List<clsTrupiShitje>();
            List<clsTrupiShitje> listaTrupiShitjePaTvsh = new List<clsTrupiShitje>();
            List<clsTrupiShitje> listaTrupiShitjePerjashtim = new List<clsTrupiShitje>();
            List<clsTaksa> listaETaksave = new List<clsTaksa>();
            colTaksa colTaksat = new colTaksa(idNderm, idPerdoruesi);
            colTaksat.Add(new clsTaksa());
            colTaksat.FirstOrDefault(x => x.IdTaksa == 0).NormaPerqindje = 0.0000000000M;
            colTaksat.FirstOrDefault(x => x.IdTaksa == 0).TipiIPerjashtimit = "";
            string TaxExemptionReasonCodePerjashtimi = "";
            colNjesiteArtikulli njesiteEArtikujve = new colNjesiteArtikulli(idNderm);
            bool perjashtimi = false;
            for (var i = 0; i < trupShitje.Count; i++)
            {
                perjashtimiArt = "";
                if (colTaksat.FirstOrDefault(z => z.IdTaksa == trupShitje[i].Tvsh).TipiIPerjashtimit != "")
                {
                    listaTrupiShitjePerjashtim.Add(trupShitje[i]);
                    perjashtimiArt = "E";
                    if (perjashtimi == false)
                    {
                        switch(new clsTaksa(trupShitje[i].Tvsh).TipiIPerjashtimit)
                        {
                            case "TYPE_1":
                                TaxExemptionReasonCodePerjashtimi = "VATEX-EU-132";
                                break;
                            case "TYPE_2":
                                TaxExemptionReasonCodePerjashtimi = "VATEX-EU-132";
                                break;
                            case "MARGIN_SCHEME":
                                TaxExemptionReasonCodePerjashtimi = "VATEX-EU-D";
                                break;
                            case "TAX_FREE":
                                TaxExemptionReasonCodePerjashtimi = "VATEX-EU-143";
                                break;
                        }
                        perjashtimi = true;
                    }
                }
                else
                {
                    if (trupShitje[i].Tvsh == 0)
                        listaTrupiShitjePaTvsh.Add(trupShitje[i]);
                    else
                        listaTrupiShitje.Add(trupShitje[i]);
                }
                
                vleftaTvsh = 0;
                zbritjeTotaleArtikulli = 0;
                done = 1;
                int ID;
                if (i == 0)
                    ID = 1;
                else
                    ID = i + 1;
                zbritjeArtikujsh = trupShitje[i].ZbritjeVlere;
                var cmimi = trupShitje[i].VleftaPaTvsh;
                cmimi = Math.Round(double.Parse(String.Format("{0:0.00}" ,cmimi)), 2);
                chargeAmount += 0;
                vleraEArtikujve += cmimi;
                string njesiArtikulliEinvoice = "";
                VR = trupShitje[i].Tvsh;
                var normaPerqindjeArtikulli1 = "";
                foreach (var item in njesiteEArtikujve.ToList()
                                                .Where(x => x.IdNjesia == trupShitje[i].IdNjesia))
                {
                    njesiArtikulliEinvoice = item.KodEinvoice;
                }
                foreach (var item in colTaksat
                                                .Where(x => x.IdTaksa == trupShitje[i].Tvsh))
                {
                    normaPerqindjeArtikulli1 = item.NormaPerqindje.ToString();
                }
                njesiArtikulliEinvoice = $"\"{njesiArtikulliEinvoice}\"";
                var kodTvshPerArtikullin = "";
                if (normaPerqindjeArtikulli1 == "")
                    normaPerqindjeArtikulli1 = "0.0000000000";
                normaPerqindjeArtikulli1 = String.Format("{0:0.00}", Double.Parse(normaPerqindjeArtikulli1));
                if(trupShitje[i].Tvsh == 0)
                {
                    
                    kodTvshPerArtikullin = "O";
                    items += "<ns3:InvoiceLine><ID>" + $"{ID}" + "</ID><InvoicedQuantity unitCode=" + $"{njesiArtikulliEinvoice}" + ">" + $"{trupShitje[i].Sasia}" + "</InvoicedQuantity><LineExtensionAmount currencyID=" + $"{taxId}" + ">" + $"{String.Format("{0:0.00}", cmimi)}" + "</LineExtensionAmount><ns3:Item><Description>" + $"{trupShitje[i].Pershkrimi}" + "</Description><Name>" + $"{trupShitje[i].Pershkrimi}" + "</Name><ns3:ClassifiedTaxCategory><ID>" + $"{kodTvshPerArtikullin}" + "</ID><ns3:TaxScheme><ID>VAT</ID></ns3:TaxScheme></ns3:ClassifiedTaxCategory></ns3:Item><ns3:Price><PriceAmount currencyID=" + $"{taxId}" + ">" + $"{trupShitje[i].VleftaPaTvsh / trupShitje[i].Sasia}" + "</PriceAmount><BaseQuantity>" + $"{trupShitje[0].Sasia}" + "</BaseQuantity></ns3:Price></ns3:InvoiceLine>";

                }
                else if(perjashtimiArt != "")
                {
                    kodTvshPerArtikullin = "E";
                    items += "<ns3:InvoiceLine><ID>" + $"{ID}" + "</ID><InvoicedQuantity unitCode=" + $"{njesiArtikulliEinvoice}" + ">" + $"{trupShitje[i].Sasia}" + "</InvoicedQuantity><LineExtensionAmount currencyID=" + $"{taxId}" + ">" + $"{String.Format("{0:0.00}", cmimi)}" + "</LineExtensionAmount><ns3:Item><Description>" + $"{trupShitje[i].Pershkrimi}" + "</Description><Name>" + $"{trupShitje[i].Pershkrimi}" + "</Name><ns3:ClassifiedTaxCategory><ID>" + $"{kodTvshPerArtikullin}" + "</ID><Percent>" + $"{normaPerqindjeArtikulli1}" + "</Percent><ns3:TaxScheme><ID>VAT</ID></ns3:TaxScheme></ns3:ClassifiedTaxCategory></ns3:Item><ns3:Price><PriceAmount currencyID=" + $"{taxId}" + ">" + $"{trupShitje[i].VleftaPaTvsh / trupShitje[i].Sasia}" + "</PriceAmount><BaseQuantity>" + $"{trupShitje[0].Sasia}" + "</BaseQuantity></ns3:Price></ns3:InvoiceLine>";

                }
                else
                {
                    if (normaPerqindjeArtikulli1 == "0.00" && kodTvsh == "G")
                    {
                        kodTvshPerArtikullin = "G";
                        items += "<ns3:InvoiceLine><ID>" + $"{ID}" + "</ID><InvoicedQuantity unitCode=" + $"{njesiArtikulliEinvoice}" + ">" + $"{trupShitje[i].Sasia}" + "</InvoicedQuantity><LineExtensionAmount currencyID=" + $"{taxId}" + ">" + $"{String.Format("{0:0.00}", cmimi)}" + "</LineExtensionAmount><ns3:Item><Description>" + $"{trupShitje[i].Pershkrimi}" + "</Description><Name>" + $"{trupShitje[i].Pershkrimi}" + "</Name><ns3:ClassifiedTaxCategory><ID>" + $"{kodTvshPerArtikullin}" + "</ID><Percent>" + $"{normaPerqindjeArtikulli1}" + "</Percent><ns3:TaxScheme><ID>VAT</ID></ns3:TaxScheme></ns3:ClassifiedTaxCategory></ns3:Item><ns3:Price><PriceAmount currencyID=" + $"{taxId}" + ">" + $"{trupShitje[i].VleftaPaTvsh / trupShitje[i].Sasia}" + "</PriceAmount><BaseQuantity>" + $"{trupShitje[0].Sasia}" + "</BaseQuantity></ns3:Price></ns3:InvoiceLine>";
                    }
                    else if (normaPerqindjeArtikulli1 == "0.00" && kodTvsh != "O" && kodTvsh != "AE")
                    {
                        kodTvshPerArtikullin = "Z";
                        items += "<ns3:InvoiceLine><ID>" + $"{ID}" + "</ID><InvoicedQuantity unitCode=" + $"{njesiArtikulliEinvoice}" + ">" + $"{trupShitje[i].Sasia}" + "</InvoicedQuantity><LineExtensionAmount currencyID=" + $"{taxId}" + ">" + $"{String.Format("{0:0.00}", cmimi)}" + "</LineExtensionAmount><ns3:Item><Description>" + $"{trupShitje[i].Pershkrimi}" + "</Description><Name>" + $"{trupShitje[i].Pershkrimi}" + "</Name><ns3:ClassifiedTaxCategory><ID>" + $"{kodTvshPerArtikullin}" + "</ID><Percent>" + $"{normaPerqindjeArtikulli1}" + "</Percent><ns3:TaxScheme><ID>VAT</ID></ns3:TaxScheme></ns3:ClassifiedTaxCategory></ns3:Item><ns3:Price><PriceAmount currencyID=" + $"{taxId}" + ">" + $"{trupShitje[i].VleftaPaTvsh / trupShitje[i].Sasia}" + "</PriceAmount><BaseQuantity>" + $"{trupShitje[0].Sasia}" + "</BaseQuantity></ns3:Price></ns3:InvoiceLine>";
                    }
                    else if (kodTvsh == "AE")
                    {
                        kodTvshPerArtikullin = "AE";
                        items += "<ns3:InvoiceLine><ID>" + $"{ID}" + "</ID><InvoicedQuantity unitCode=" + $"{njesiArtikulliEinvoice}" + ">" + $"{trupShitje[i].Sasia}" + "</InvoicedQuantity><LineExtensionAmount currencyID=" + $"{taxId}" + ">" + $"{String.Format("{0:0.00}", cmimi)}" + "</LineExtensionAmount><ns3:Item><Description>" + $"{trupShitje[i].Pershkrimi}" + "</Description><Name>" + $"{trupShitje[i].Pershkrimi}" + "</Name><ns3:ClassifiedTaxCategory><ID>" + $"{kodTvshPerArtikullin}" + "</ID><Percent>" + $"{normaPerqindjeArtikulli1}" + "</Percent><ns3:TaxScheme><ID>VAT</ID></ns3:TaxScheme></ns3:ClassifiedTaxCategory></ns3:Item><ns3:Price><PriceAmount currencyID=" + $"{taxId}" + ">" + $"{trupShitje[i].VleftaPaTvsh / trupShitje[i].Sasia}" + "</PriceAmount><BaseQuantity>" + $"{trupShitje[0].Sasia}" + "</BaseQuantity></ns3:Price></ns3:InvoiceLine>";
                    }
                    else if (kodTvsh == "O")
                    {
                        kodTvshPerArtikullin = "O";
                        items += "<ns3:InvoiceLine><ID>" + $"{ID}" + "</ID><InvoicedQuantity unitCode=" + $"{njesiArtikulliEinvoice}" + ">" + $"{trupShitje[i].Sasia}" + "</InvoicedQuantity><LineExtensionAmount currencyID=" + $"{taxId}" + ">" + $"{String.Format("{0:0.00}", cmimi)}" + "</LineExtensionAmount><ns3:Item><Description>" + $"{trupShitje[i].Pershkrimi}" + "</Description><Name>" + $"{trupShitje[i].Pershkrimi}" + "</Name><ns3:ClassifiedTaxCategory><ID>" + $"{kodTvshPerArtikullin}" + "</ID><ns3:TaxScheme><ID>FRE</ID></ns3:TaxScheme></ns3:ClassifiedTaxCategory></ns3:Item><ns3:Price><PriceAmount currencyID=" + $"{taxId}" + ">" + $"{trupShitje[i].VleftaPaTvsh / trupShitje[i].Sasia}" + "</PriceAmount><BaseQuantity>" + $"{trupShitje[0].Sasia}" + "</BaseQuantity></ns3:Price></ns3:InvoiceLine>";
                    }
                    else
                    {
                        kodTvshPerArtikullin = "S";
                        items += "<ns3:InvoiceLine><ID>" + $"{ID}" + "</ID><InvoicedQuantity unitCode=" + $"{njesiArtikulliEinvoice}" + ">" + $"{trupShitje[i].Sasia}" + "</InvoicedQuantity><LineExtensionAmount currencyID=" + $"{taxId}" + ">" + $"{String.Format("{0:0.00}", cmimi)}" + "</LineExtensionAmount><ns3:Item><Description>" + $"{trupShitje[i].Pershkrimi}" + "</Description><Name>" + $"{trupShitje[i].Pershkrimi}" + "</Name><ns3:ClassifiedTaxCategory><ID>" + $"{kodTvshPerArtikullin}" + "</ID><Percent>" + $"{normaPerqindjeArtikulli1}" + "</Percent><ns3:TaxScheme><ID>VAT</ID></ns3:TaxScheme></ns3:ClassifiedTaxCategory></ns3:Item><ns3:Price><PriceAmount currencyID=" + $"{taxId}" + ">" + $"{trupShitje[i].VleftaPaTvsh / trupShitje[i].Sasia}" + "</PriceAmount><BaseQuantity>" + $"{trupShitje[0].Sasia}" + "</BaseQuantity></ns3:Price></ns3:InvoiceLine>";
                    }
                }
                

            }
            foreach (var item in listaTrupiShitje
                                               .Where(x => !listTaksash.Contains(x.Tvsh)))
            {

                listaETaksave.Add(colTaksat.FirstOrDefault(z => z.IdTaksa == item.Tvsh));
                listTaksash.AddIfNotExists(item.Tvsh);
                
            }
            int tvshja = 0;
            string arsyePerjashtimi = "";
            foreach (var taks in listaETaksave)
            {
                if (!taxSubtotal.Contains("<Percent>"+ float.Parse(taks.NormaPerqindje.ToString()).ToString() + "</Percent>"))
                {
                    double cmimiArtikulli = 0;
                    string normaPerqindjeArtikulli = "";
                        
                    foreach (var item in listaTrupiShitje
                                                    .Where(x => colTaksat.FirstOrDefault(z => z.IdTaksa == x.Tvsh).NormaPerqindje.ToString() == taks.NormaPerqindje.ToString()))
                    {
                        vleftaTvsh = vleftaTvsh + Math.Round(item.VleftaPaTvsh,2) * (1 - Double.Parse(vleraTvsh) / totali);
                        zbritjeArtikujsh = zbritjeArtikujsh + Double.Parse(String.Format("{0:0.00}", Math.Truncate(item.ZbritjeVlere * 100) / 100));
                        zbritjeTotaleArtikulli += (zbritjePerqindje / 100) * Math.Round(item.VleftaPaTvsh, 2);
                        cmimiArtikulli = item.VleftaPaTvsh * (1 - Double.Parse(vleraTvsh) / totali);
                        tvshja = item.Tvsh;
                        done = done + 1;
                        arsyePerjashtimi = taks.TipiIPerjashtimit;
                    }

                        
                    normaPerqindjeArtikulli = taks.NormaPerqindje.ToString();
                    zbritjeTotaleArtikulli = Math.Round(zbritjeTotaleArtikulli, 2);
                    string doneString = $"\"{done}\"";
                    var VatRate = String.Format("{0:0.00}", taks.NormaPerqindje);
                    VatRate = $"\"{VatRate}\"";
                    zbritje += zbritjeTotaleArtikulli;
                    VR2 = float.Parse(normaPerqindjeArtikulli).ToString();
                    tvshEVlerave = Convert.ToDouble(normaPerqindjeArtikulli);
                    var VATAmt = vleftaTvsh * tvshEVlerave / 100;
                    //var VATAmt2 = String.Format("{0:0.00}", Math.Round(VATAmt, 2));
                    //var PriceBefVAT = String.Format("{0:0.00}", Math.Round(vleftaTvsh, 2));
                    var VATAmt2 = String.Format("{0:0.00}", VATAmt);
                    var PriceBefVAT = String.Format("{0:0.00}", vleftaTvsh);
                    var VATRate = String.Format("{0:0.00}", tvshEVlerave);
                    vleftaTvsh = 0;
                    if (PriceBefVAT == "NaN")
                        PriceBefVAT = "0.00";
                    if (VATRate == "NaN")
                        VATRate = "0.00";
                    if (VATAmt2 == "NaN")
                        VATAmt2 = "0.00";
                    string idTax = "";
                    if (VR2 != "0")
                        idTax = "S";
                    VATAmt = Math.Round(double.Parse(VATAmt2), 2);
                    taxAmount += Math.Round(double.Parse(VATAmt2), 2);
                    var kodTvshPerTvsh = "";
                    if (normaPerqindjeArtikulli == "0" && kodTvsh == "G")
                    {
                        kodTvshPerTvsh = "G";
                        taxSubtotal += "<ns3:TaxSubtotal><TaxableAmount currencyID=" + $"{taxId}" + ">" + $"{(PriceBefVAT)}" + "</TaxableAmount><TaxAmount currencyID=" + $"{taxId}" + ">" + $"{VATAmt2}" + "</TaxAmount><ns3:TaxCategory><ID>" + $"{kodTvshPerTvsh}" + "</ID><TaxExemptionReasonCode>vatex-eu-o</TaxExemptionReasonCode><TaxExemptionReason>Jashtë mbulimit të TVSH-së</TaxExemptionReason><ns3:TaxScheme><ID>VAT</ID></ns3:TaxScheme></ns3:TaxCategory></ns3:TaxSubtotal>";
                        if (String.Format("{0:0.00}", zbritjeTotaleArtikulli) != "0.00")
                            allowanceCharge += "<ns3:AllowanceCharge><ChargeIndicator>false</ChargeIndicator><AllowanceChargeReasonCode>41</AllowanceChargeReasonCode><Amount currencyID= " + $"{taxId}" + ">" + $"{String.Format("{0:0.00}", zbritjeTotaleArtikulli)}" + "</Amount><ns3:TaxCategory><ID>" + $"{kodTvshPerTvsh}" + "</ID><Percent>" + $"{VR2}" + "</Percent><ns3:TaxScheme><ID>VAT</ID></ns3:TaxScheme></ns3:TaxCategory></ns3:AllowanceCharge>";
                    }
                    else if ((normaPerqindjeArtikulli == "0.0000000000" || normaPerqindjeArtikulli == "0") && kodTvsh != "O" && kodTvsh != "AE")
                    {
                        kodTvshPerTvsh = "Z";
                        taxSubtotal += "<ns3:TaxSubtotal><TaxableAmount currencyID=" + $"{taxId}" + ">" + $"{(PriceBefVAT)}" + "</TaxableAmount><TaxAmount currencyID=" + $"{taxId}" + ">" + $"{VATAmt2}" + "</TaxAmount><ns3:TaxCategory><ID>" + $"{kodTvshPerTvsh}" + "</ID><Percent>" + $"{VR2}" + "</Percent><ns3:TaxScheme><ID>VAT</ID></ns3:TaxScheme></ns3:TaxCategory></ns3:TaxSubtotal>";
                        if (String.Format("{0:0.00}", zbritjeTotaleArtikulli) != "0.00")
                            allowanceCharge += "<ns3:AllowanceCharge><ChargeIndicator>false</ChargeIndicator><AllowanceChargeReasonCode>41</AllowanceChargeReasonCode><Amount currencyID= " + $"{taxId}" + ">" + $"{String.Format("{0:0.00}", zbritjeTotaleArtikulli)}" + "</Amount><ns3:TaxCategory><ID>" + $"{kodTvshPerTvsh}" + "</ID><Percent>" + $"{VR2}" + "</Percent><ns3:TaxScheme><ID>VAT</ID></ns3:TaxScheme></ns3:TaxCategory></ns3:AllowanceCharge>";
                    }
                    else if (kodTvsh == "AE")
                    {
                        kodTvshPerTvsh = "AE";
                        taxAmount = 0;
                        VATAmt2 = "0.00";
                        taxSubtotal += "<ns3:TaxSubtotal><TaxableAmount currencyID=" + $"{taxId}" + ">" + $"{(PriceBefVAT)}" + "</TaxableAmount><TaxAmount currencyID=" + $"{taxId}" + ">" + $"{VATAmt2}" + "</TaxAmount><ns3:TaxCategory><ID>" + $"{kodTvshPerTvsh}" + "</ID><Percent>" + $"{VR2}" + "</Percent><TaxExemptionReasonCode>vatex-eu-ae</TaxExemptionReasonCode><TaxExemptionReason>auto ngarkesa</TaxExemptionReason><ns3:TaxScheme><ID>VAT</ID></ns3:TaxScheme></ns3:TaxCategory></ns3:TaxSubtotal>";
                        if (String.Format("{0:0.00}", zbritjeTotaleArtikulli) != "0.00")
                            allowanceCharge += "<ns3:AllowanceCharge><ChargeIndicator>false</ChargeIndicator><AllowanceChargeReasonCode>41</AllowanceChargeReasonCode><Amount currencyID= " + $"{taxId}" + ">" + $"{String.Format("{0:0.00}", zbritjeTotaleArtikulli)}" + "</Amount><ns3:TaxCategory><ID>" + $"{kodTvshPerTvsh}" + "</ID><Percent>" + $"{VR2}" + "</Percent><ns3:TaxScheme><ID>VAT</ID></ns3:TaxScheme></ns3:TaxCategory></ns3:AllowanceCharge>";
                    }
                    else if (kodTvsh == "O")
                    {
                        kodTvshPerTvsh = "O";
                        taxAmount = 0;
                        VATAmt2 = "0.00";
                        taxSubtotal += "<ns3:TaxSubtotal><TaxableAmount currencyID=" + $"{taxId}" + ">" + $"{(PriceBefVAT)}" + "</TaxableAmount><TaxAmount currencyID=" + $"{taxId}" + ">" + $"{VATAmt2}" + "</TaxAmount><ns3:TaxCategory><ID>" + $"{kodTvshPerTvsh}" + "</ID><TaxExemptionReasonCode>vatex-eu-o</TaxExemptionReasonCode><TaxExemptionReason>Jashtë mbulimit të TVSH-së</TaxExemptionReason><ns3:TaxScheme><ID>FRE</ID></ns3:TaxScheme></ns3:TaxCategory></ns3:TaxSubtotal>";
                        if (String.Format("{0:0.00}", zbritjeTotaleArtikulli) != "0.00")
                            allowanceCharge += "<ns3:AllowanceCharge><ChargeIndicator>false</ChargeIndicator><AllowanceChargeReasonCode>41</AllowanceChargeReasonCode><Amount currencyID= " + $"{taxId}" + ">" + $"{String.Format("{0:0.00}", zbritjeTotaleArtikulli)}" + "</Amount><ns3:TaxCategory><ID>" + $"{kodTvshPerTvsh}" + "</ID><Percent>" + $"{VR2}" + "</Percent><ns3:TaxScheme><ID>FRE</ID></ns3:TaxScheme></ns3:TaxCategory></ns3:AllowanceCharge>";
                    }
                    else
                    {
                        kodTvshPerTvsh = "S";
                        taxSubtotal += "<ns3:TaxSubtotal><TaxableAmount currencyID=" + $"{taxId}" + ">" + $"{(PriceBefVAT)}" + "</TaxableAmount><TaxAmount currencyID=" + $"{taxId}" + ">" + $"{VATAmt2}" + "</TaxAmount><ns3:TaxCategory><ID>" + $"{kodTvshPerTvsh}" + "</ID><Percent>" + $"{VR2}" + "</Percent><ns3:TaxScheme><ID>VAT</ID></ns3:TaxScheme></ns3:TaxCategory></ns3:TaxSubtotal>";
                        if (String.Format("{0:0.00}", zbritjeTotaleArtikulli) != "0.00")
                            allowanceCharge += "<ns3:AllowanceCharge><ChargeIndicator>false</ChargeIndicator><AllowanceChargeReasonCode>41</AllowanceChargeReasonCode><Amount currencyID= " + $"{taxId}" + ">" + $"{String.Format("{0:0.00}", zbritjeTotaleArtikulli)}" + "</Amount><ns3:TaxCategory><ID>" + $"{kodTvshPerTvsh}" + "</ID><Percent>" + $"{VR2}" + "</Percent><ns3:TaxScheme><ID>VAT</ID></ns3:TaxScheme></ns3:TaxCategory></ns3:AllowanceCharge>";
                    }
                    done = 0;
                    zbritjeTotaleArtikulli = 0;
                }
            }
            if(listaTrupiShitjePaTvsh.Count > 0)
            {
                if (!taxSubtotal.Contains("<ID>O</ID>"))
                {
                    double cmimiArtikulli = 0;
                    string normaPerqindjeArtikulli = "";

                    foreach (var item in listaTrupiShitjePaTvsh)
                    {
                        vleftaTvsh = vleftaTvsh + Math.Round(item.VleftaPaTvsh, 2) * (1 - Double.Parse(vleraTvsh) / totali);
                        zbritjeArtikujsh = zbritjeArtikujsh + Double.Parse(String.Format("{0:0.00}", Math.Truncate(item.ZbritjeVlere * 100) / 100));
                        zbritjeTotaleArtikulli += (zbritjePerqindje / 100) * Math.Round(item.VleftaPaTvsh, 2);
                        cmimiArtikulli = item.VleftaPaTvsh * (1 - Double.Parse(vleraTvsh) / totali);
                        tvshja = item.Tvsh;
                        done = done + 1;
                    }
                    vleftaTvsh = vleftaTvsh;
                    zbritjeTotaleArtikulli = Math.Round(zbritjeTotaleArtikulli, 2);
                    string doneString = $"\"{done}\"";
                    zbritje += zbritjeTotaleArtikulli;
                    var VATAmt = vleftaTvsh * 0 / 100;
                    var VATAmt2 = String.Format("{0:0.00}", VATAmt);
                    var PriceBefVAT = String.Format("{0:0.00}", vleftaTvsh);
                    var VATRate = String.Format("{0:0.00}", tvshEVlerave);
                    vleftaTvsh = 0;
                    if (PriceBefVAT == "NaN")
                        PriceBefVAT = "0.00";
                    if (VATRate == "NaN")
                        VATRate = "0.00";
                    if (VATAmt2 == "NaN")
                        VATAmt2 = "0.00";
                    string idTax = "";
                    if (VR2 != "0")
                        idTax = "S";
                    VATAmt = Math.Round(double.Parse(VATAmt2), 2);
                    taxAmount += Math.Round(double.Parse(VATAmt2), 2);
                    var kodTvshPerTvsh = "";
                    kodTvshPerTvsh = "O";
                    VATAmt2 = "0.00";
                    taxSubtotal += "<ns3:TaxSubtotal><TaxableAmount currencyID=" + $"{taxId}" + ">" + $"{(PriceBefVAT)}" + "</TaxableAmount><TaxAmount currencyID=" + $"{taxId}" + ">" + $"{VATAmt2}" + "</TaxAmount><ns3:TaxCategory><ID>" + $"{kodTvshPerTvsh}" + "</ID><TaxExemptionReasonCode>vatex-eu-o</TaxExemptionReasonCode><TaxExemptionReason>Jashtë mbulimit të TVSH-së</TaxExemptionReason><ns3:TaxScheme><ID>VAT</ID></ns3:TaxScheme></ns3:TaxCategory></ns3:TaxSubtotal>";
                    if (String.Format("{0:0.00}", zbritjeTotaleArtikulli) != "0.00")
                        allowanceCharge += "<ns3:AllowanceCharge><ChargeIndicator>false</ChargeIndicator><AllowanceChargeReasonCode>41</AllowanceChargeReasonCode><Amount currencyID= " + $"{taxId}" + ">" + $"{String.Format("{0:0.00}", zbritjeTotaleArtikulli)}" + "</Amount><ns3:TaxCategory><ID>" + $"{kodTvshPerTvsh}" + "</ID><ns3:TaxScheme><ID>VAT</ID></ns3:TaxScheme></ns3:TaxCategory></ns3:AllowanceCharge>";

                    done = 0;
                    zbritjeTotaleArtikulli = 0;
                    klientiIdTax = "FRE";
                    ndermarrjeIdTax = "FRE";
                }
            }
            if(listaTrupiShitjePerjashtim.Count > 0)
            {
                if (!taxSubtotal.Contains("<ID>E</ID>"))
                {
                    double cmimiArtikulli = 0;
                    string normaPerqindjeArtikulli = "";

                    foreach (var item in listaTrupiShitjePerjashtim)
                    {
                        vleftaTvsh = vleftaTvsh + Math.Round(item.VleftaPaTvsh, 2) * (1 - Double.Parse(vleraTvsh) / totali);
                        zbritjeArtikujsh = zbritjeArtikujsh + Double.Parse(String.Format("{0:0.00}", Math.Truncate(item.ZbritjeVlere * 100) / 100));
                        zbritjeTotaleArtikulli += (zbritjePerqindje / 100) * Math.Round(item.VleftaPaTvsh, 2);
                        cmimiArtikulli = item.VleftaPaTvsh * (1 - Double.Parse(vleraTvsh) / totali);
                        tvshja = item.Tvsh;
                        done = done + 1;
                    }
                    
                    vleftaTvsh = vleftaTvsh;
                    zbritjeTotaleArtikulli = Math.Round(zbritjeTotaleArtikulli, 2);
                    string doneString = $"\"{done}\"";
                    zbritje += zbritjeTotaleArtikulli;
                    var VATAmt = vleftaTvsh * 0 / 100;
                    var VATAmt2 = String.Format("{0:0.00}", VATAmt);
                    var PriceBefVAT = String.Format("{0:0.00}", vleftaTvsh);
                    var VATRate = String.Format("{0:0.00}", tvshEVlerave);
                    vleftaTvsh = 0;
                    if (PriceBefVAT == "NaN")
                        PriceBefVAT = "0.00";
                    if (VATRate == "NaN")
                        VATRate = "0.00";
                    if (VATAmt2 == "NaN")
                        VATAmt2 = "0.00";
                    string idTax = "";
                    if (VR2 != "0")
                        idTax = "S";
                    VATAmt = Math.Round(double.Parse(VATAmt2), 2);
                    taxAmount += Math.Round(double.Parse(VATAmt2), 2);
                    var kodTvshPerTvsh = "";
                    kodTvshPerTvsh = "E";
                    VATAmt2 = "0.00";
                    kodTvshPerTvsh = "E";
                    
                    taxSubtotal += "<ns3:TaxSubtotal><TaxableAmount currencyID=" + $"{taxId}" + ">" + $"{(PriceBefVAT)}" + "</TaxableAmount><TaxAmount currencyID=" + $"{taxId}" + ">" + $"{VATAmt2}" + "</TaxAmount><ns3:TaxCategory><ID>" + $"{kodTvshPerTvsh}" + "</ID><Percent>0.00</Percent><TaxExemptionReasonCode>" + TaxExemptionReasonCodePerjashtimi + "</TaxExemptionReasonCode><TaxExemptionReason>Exempt from VAT</TaxExemptionReason><ns3:TaxScheme><ID>VAT</ID></ns3:TaxScheme></ns3:TaxCategory></ns3:TaxSubtotal>";
                    if (String.Format("{0:0.00}", zbritjeTotaleArtikulli) != "0.00")
                        allowanceCharge += "<ns3:AllowanceCharge><ChargeIndicator>false</ChargeIndicator><AllowanceChargeReasonCode>41</AllowanceChargeReasonCode><Amount currencyID= " + $"{taxId}" + ">" + $"{String.Format("{0:0.00}", zbritjeTotaleArtikulli)}" + "</Amount><ns3:TaxCategory><ID>" + $"{kodTvshPerTvsh}" + "</ID><Percent>0.00</Percent><ns3:TaxScheme><ID>VAT</ID></ns3:TaxScheme></ns3:TaxCategory></ns3:AllowanceCharge>";

                    done = 0;
                    zbritjeTotaleArtikulli = 0;
                }
            }
            if (listaTrupiShitjePaTvsh.Count > 0)
            {
                ndermarrjeIdTax = "FRE";
                klientiIdTax = "FRE";
            }
            else
            {
                ndermarrjeIdTax = "VAT";
                klientiIdTax = "VAT";

            }
            var bankat = colBankat.merrBankatENdermarrjesPerEinvoice(idPerdoruesi,nderm.IdNdermarrje);
            Regex rgx = new Regex("[^a-zA-Z0-9 -]");
            adresKlienti = rgx.Replace(adresKlienti,"");
            string taguIBankave = String.Empty;
            foreach(var bank in bankat)
            {
                string kodiIMetodesSePaguar = "";
                if (bank.ShfaqNeEinvoice)
                {
                    if (menyrePagese == "Pagese Automatike" || menyrePagese == "Karte krediti")
                        kodiIMetodesSePaguar = "10";
                    else
                        kodiIMetodesSePaguar = "30";
                    taguIBankave += "<ns3:PaymentMeans><PaymentMeansCode>"+kodiIMetodesSePaguar+"</PaymentMeansCode><ns3:PayeeFinancialAccount><ID>"+bank.NrLlogariBanka+ "</ID><Name>"+bank.EmerBanka+ "</Name></ns3:PayeeFinancialAccount></ns3:PaymentMeans>";
                }
            }
            vleraEArtikujve = Math.Round(double.Parse(String.Format("{0:0.00}", vleraEArtikujve)), 2);
            //sherben per leximin e fileve qe ruhen ne folderin DokumenteEinvoice
            //therret klasen DokumentShteseEinvoice ku jane krijuar modelet
            var connString = MyConnectionsManager.GetSelectedConNameServer();

            StringBuilder documentetEinvoieceXml = new StringBuilder();
            var sourceDicertory = System.Web.Hosting.HostingEnvironment.MapPath($"~/DocumentEinvoice/{connString}/{nderm.IdNdermarrje}/{QueryString}/");
            if (Directory.Exists(sourceDicertory))
            {
                foreach (var filePath in Directory.EnumerateFiles(sourceDicertory, "*.*"))
                {
                    var dokumentShteseModel = DokumentShteseEinvoice.Create(filePath);
                    documentetEinvoieceXml.Append(dokumentShteseModel.ToXml());
                }
                //kryen fshirjen e folderit pas fiskalizimit te fatures
                Directory.Delete(sourceDicertory, true);
            }
            adresNdermarrje = rgx.Replace(adresNdermarrje, "");
            if (allowanceCharge != "")
                allowanceTotalAmount = "<AllowanceTotalAmount currencyID=" + $"{taxId}" + ">" + $"{String.Format("{0:0.00}", zbritje)}" + "</AllowanceTotalAmount>";
            else
                allowanceTotalAmount = "";
            var vleftaPaTvsh3 = vleraEArtikujve - zbritje + chargeAmount;
            vleftaPaTvsh2 = String.Format("{0:0.00}", vleftaPaTvsh3);
            chargeTotalAmount = "<ChargeTotalAmount currencyID=" + $"{taxId}" + ">" + $"{chargeAmount}" + "</ChargeTotalAmount>";
            var vleraEArtikujve2 = String.Format("{0:0.00}", vleraEArtikujve);
            lineExtensionAmount = "<LineExtensionAmount currencyID=" + $"{taxId}" + ">" + $"{(vleraEArtikujve2)}" + "</LineExtensionAmount>";
            vleftaPaTvshPaZbritje = Decimal.Parse(vleraEArtikujve.ToString()) - Decimal.Parse(zbritje.ToString());
            if (taxAmount.ToString() == "NaN")
                taxAmount = 0;
            taxableamount2 = String.Format("{0:0.00}", taxAmount);
            var vleraMeTvsh3 = String.Format("{0:0.00}", Decimal.Parse(vleftaPaTvsh2.ToString()) + Decimal.Parse(String.Format("{0:0.00}", taxAmount)));
            taxInclusiveAmount = "<TaxInclusiveAmount currencyID=" + $"{taxId}" + ">" + $"{Decimal.Parse(vleraMeTvsh3.ToString())}" + "</TaxInclusiveAmount>";
            taxExclusiveAmount = "<TaxExclusiveAmount currencyID=" + $"{taxId}" + ">" + $"{(vleftaPaTvsh2)}" + "</TaxExclusiveAmount>";
            var shumaEPagueshme2 = Decimal.Parse(vleraMeTvsh3.ToString()) - Decimal.Parse(shumaEParapaguar.ToString()) + Decimal.Parse(roundingShumaEParapaguar.ToString());
            payableAmount = "<PayableAmount currencyID=" + $"{taxId}" + ">" + $"{String.Format("{0:0.00}", shumaEPagueshme2)}" + "</PayableAmount>";
            string tagNoteKursi = "";
            string bleresiTaxScheme = "<ns3:PartyTaxScheme><CompanyID>" + $"{klientiIdTax}" + "</CompanyID><ns3:TaxScheme><ID>" + $"{klientiIdTax}" + "</ID></ns3:TaxScheme></ns3:PartyTaxScheme>";
            string shitesiTaxSCheme = "<ns3:PartyTaxScheme><CompanyID>AL" + $"{niptNdermarrje}" + "</CompanyID><ns3:TaxScheme><ID>" + $"{ndermarrjeIdTax}" + "</ID></ns3:TaxScheme></ns3:PartyTaxScheme>";
            string taxTotalAll = "";
            string taxCurrencyCodeTag = "";
            if(kodMonedha != "ALL")
            {
                taxTotalAll = "<ns3:TaxTotal><TaxAmount currencyID=\"ALL\">" + $"{String.Format("{0:0.00}", Double.Parse(taxableamount2) * kursi)}" + "</TaxAmount></ns3:TaxTotal>";
                taxCurrencyCodeTag = "<TaxCurrencyCode>ALL</TaxCurrencyCode>";
            }
            if(kursi != 1)
            {
                tagNoteKursi = $"<Note>CurrencyExchangeRate=" + kursi + "#AAI#</Note>";
            }
            if (pershkrimFature != "") pershkrimFature = $"<Note>Pershkrimi i fatures: " + pershkrimFature + "#AAI#</Note>";
            if (klientFurnitor.ShitjePaTvsh)
            {
                bleresiTaxScheme = "";
                shitesiTaxSCheme = "";
            }

            const String XML_SCHEMA_NS = "https://Einvoice.tatime.gov.al/ EinvoiceService/schema";
            const String XML_SIG_METHOD = "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256";
            const String XML_DIG_METHOD = "http://www.w3.org/2001/04/xmlenc#sha256";
            string REQUEST_TO_SIGN =
            "<ns8:Invoice " +
            "xmlns:ns8=\"urn:oasis:names:specification:ubl:schema:xsd:Invoice-2\"" +
            " xmlns=\"urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2\" " +
            "xmlns:ns2=\"urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2\" " +
            "xmlns:ns3=\"urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2\"" +
            " xmlns:ns4=\"urn:oasis:names:specification:ubl:schema:xsd:SignatureBasicComponents-2\" " +
            "xmlns:ns5=\"http://www.w3.org/2000/09/xmldsig#\"" +
            " xmlns:ns6=\"urn:oasis:names:specification:ubl:schema:xsd:SignatureAggregateComponents-2\" " +
            "xmlns:ns7=\"urn:oasis:names:specification:ubl:schema:xsd:CommonSignatureComponents-2\">" +
            "<ns2:UBLExtensions><ns2:UBLExtension><ns2:ExtensionContent><ns7:UBLDocumentSignatures><ns6:SignatureInformation></ns6:SignatureInformation></ns7:UBLDocumentSignatures></ns2:ExtensionContent></ns2:UBLExtension></ns2:UBLExtensions><CustomizationID xmlns='urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2'>urn:cen.eu:en16931:2017</CustomizationID><ProfileID>" + $"{procesi}" + "</ProfileID><ID>" + $"{idProfili}" + "</ID><IssueDate>" + $"{dateDergimi}" + "</IssueDate><DueDate>" + $"{dateMaturimi}" + "</DueDate><InvoiceTypeCode>" + $"{llojDokumenti}" + "</InvoiceTypeCode><Note>IIC=" + $"{iic}" + "#AAI#</Note><Note>IICSignature=" + $"{iicSignature}" + "#AAI#</Note><Note>FIC=" + $"{nivf}" + "#AAI#</Note><Note>IssueDateTime=" + $"{dateDergimiSpecifike}" + "#AAI#</Note><Note>OperatorCode=" + $"{kodOperatori}" + "#AAI#</Note><Note>BusinessUnitCode=" + $"{kodBiznesi}" + "#AAI#</Note><Note>SoftwareCode=" + $"{kodSoftueri}" + "#AAI#</Note>" + pershkrimFature + tagNoteKursi + "<DocumentCurrencyCode>" + $"{kodMonedha}" + "</DocumentCurrencyCode>" + taxCurrencyCodeTag + InvoicePeriod + documentetEinvoieceXml.ToString() +"<ns3:AccountingSupplierParty><ns3:Party><EndpointID schemeID=\"9923\">" + $"{niptNdermarrje}" + "</EndpointID><ns3:PartyName><Name>" + $"{emerNdermarrje}" + "</Name></ns3:PartyName><ns3:PostalAddress><StreetName>" + $"{adresNdermarrje}" + "</StreetName><CityName>" + $"{qytetiNdermarrje}" + "</CityName><ns3:Country><IdentificationCode>" + $"{shtetNdermarrje}" + "</IdentificationCode></ns3:Country></ns3:PostalAddress>"+shitesiTaxSCheme+"<ns3:PartyLegalEntity><RegistrationName>" + $"{emerNdermarrje}" + "</RegistrationName><CompanyID>" + $"{niptNdermarrje}" + "</CompanyID></ns3:PartyLegalEntity><ns3:Contact/></ns3:Party></ns3:AccountingSupplierParty><ns3:AccountingCustomerParty><ns3:Party><EndpointID schemeID=\"9923\">" + $"{niptKlienti}" + "</EndpointID><ns3:PartyName><Name>" + $"{emerKlienti}" + "</Name></ns3:PartyName><ns3:PostalAddress><StreetName>" + $"{adresKlienti}" + "</StreetName><CityName>" + $"{qytetiKlienti}" + "</CityName><ns3:Country><IdentificationCode>" + $"{shtetiKlienti}" + "</IdentificationCode></ns3:Country></ns3:PostalAddress>"+bleresiTaxScheme+"<ns3:PartyLegalEntity><RegistrationName>" + $"{emerKlienti}" + "</RegistrationName><CompanyID>" + $"{niptKlienti}" + "</CompanyID></ns3:PartyLegalEntity><ns3:Contact/></ns3:Party></ns3:AccountingCustomerParty>" + taguIBankave + allowanceCharge + "<ns3:TaxTotal><TaxAmount currencyID=" + $"{taxId}" + ">" + $"{taxableamount2}" + "</TaxAmount>" + taxSubtotal + "</ns3:TaxTotal>"+ taxTotalAll + "<ns3:LegalMonetaryTotal>" + lineExtensionAmount + taxExclusiveAmount + taxInclusiveAmount + allowanceTotalAmount + chargeTotalAmount + prepaidAmount + payableRoundingAmount + payableAmount + payableAlternativeAmount + "</ns3:LegalMonetaryTotal>" + $"{items}" + "</ns8:Invoice>";
            string passpath = nderm.Pathname + $"/password.txt";
            string[] result = new string[2];
            String KEYSTORE_PASS = "";
            REQUEST_TO_SIGN = REQUEST_TO_SIGN.Replace("&", "&amp;");
            byte[] encrypted;
            String KEYSTORE_LOCATION = System.Web.Hosting.HostingEnvironment.MapPath(nderm.Pathname) + @"certifikata.p12";
            if (File.Exists(System.Web.Hosting.HostingEnvironment.MapPath(nderm.Pathname) + @"password.txt"))
                KEYSTORE_PASS = File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath(nderm.Pathname) + @"password.txt");
            else throw new Exception("Ju lutem ngarkoni filen e passwordit!");

            using (X509Certificate2 keyStore = new X509Certificate2(KEYSTORE_LOCATION, KEYSTORE_PASS))
            {
                try
                {
                    // Load a private from a key store
                    RSA privateKey = keyStore.GetRSAPrivateKey();
                    // Convert string XML to object
                    XmlDocument request = new XmlDocument();
                    request.LoadXml(REQUEST_TO_SIGN);
                    // Create key info element
                    KeyInfo keyInfo = new KeyInfo();
                    KeyInfoX509Data keyInfoData = new KeyInfoX509Data();
                    keyInfoData.AddCertificate(keyStore);
                    keyInfo.AddClause(keyInfoData);
                    // Create signature reference
                    Reference reference = new Reference("");
                    reference.AddTransform(new XmlDsigEnvelopedSignatureTransform(false));
                    reference.AddTransform(new XmlDsigExcC14NTransform(false));
                    reference.DigestMethod = XML_DIG_METHOD;
                    // Create signature
                    SignedXml xml = new SignedXml(request);
                    xml.SigningKey = privateKey;
                    xml.SignedInfo.CanonicalizationMethod = SignedXml.XmlDsigExcC14NTransformUrl;
                    xml.SignedInfo.SignatureMethod = XML_SIG_METHOD;
                    xml.KeyInfo = keyInfo;
                    xml.AddReference(reference);
                    xml.ComputeSignature();
                    // Add signature element to the request
                    XmlElement signature = xml.GetXml();
                    request.FirstChild.FirstChild.FirstChild.FirstChild.FirstChild.FirstChild.AppendChild(signature);
                    // Convert signed request to string and print
                    StringWriter sw = new StringWriter();
                    XmlTextWriter xw = new XmlTextWriter(sw);
                    request.WriteTo(xw);
                    var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(sw.ToString());
                    var mesazhUBLBase64 = System.Convert.ToBase64String(plainTextBytes);
                    result[0] = mesazhUBLBase64;
                    result[1] = REQUEST_TO_SIGN;
                    return result;
                }
                catch (Exception ex)
                {
                    string[] error = new string[2];
                    error[0] = ex.Message;
                    return error;
                }
            }
            return result;
        }
        public static bool ndryshoStatusinEinvoice(string eics, string statusi, int idNdermarrje)
        {
            clsNdermarrje nderm = new clsNdermarrje(idNdermarrje);
            var uuid = Guid.NewGuid().ToString();
            uuid = $"\"{uuid}\"";
            switch (statusi)
            {
                case "Refuzuar":
                    statusi = "REFUSED";
                    break;
                case "Aprovuar":
                    statusi = "ACCEPTED";
                    break;
            }
            DateTimeOffset sourceDate = new DateTimeOffset(DateTime.UtcNow);
            DateTimeOffset timezoneIShqiperise = TimeZoneInfo.ConvertTime(sourceDate,
                          TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time"));
            var dateDergimiSpecifike = timezoneIShqiperise.ToString("yyyy-MM-ddTHH\\:mm\\:sszzz");
            dateDergimiSpecifike = $"\"{dateDergimiSpecifike}\"";
            string eicsString = "";
            string[] eic = eics.Split(' ');
            foreach (string e in eic)
            {
                eicsString += "<EIC>" + $"{e}" + "</EIC>";
            }
            const String XML_SCHEMA_NS = "https://Einvoice.tatime.gov.al/ EinvoiceService/schema";
            const String XML_SIG_METHOD = "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256";
            const String XML_REQUEST_ID = "Request";
            const String XML_DIG_METHOD = "http://www.w3.org/2001/04/xmlenc#sha256";
            String REQUEST_TO_SIGN =
            "<EinvoiceChangeStatusRequest " +
            " xmlns=\"https://Einvoice.tatime.gov.al/EinvoiceService/schema\" " +
            " xmlns:ns2=\"http://www.w3.org/2000/09/xmldsig#\" " +
            " Id=\"Request\" " +
            " Version=\"1\">\r\n" +
            "<Header SendDateTime=" + $"{dateDergimiSpecifike}" + " UUID=" + $"{uuid}" + "/>\r\n" +
            "<EICs>\r\n" +
            "" + $"{eicsString}" + "\r\n" +
            "</EICs>\r\n" +
            "<EinStatus>" + $"{statusi}" + "</EinStatus>\r\n" +
            "</EinvoiceChangeStatusRequest>";
            REQUEST_TO_SIGN = REQUEST_TO_SIGN.Replace("&", "&amp;");
            string passpath = nderm.Pathname + $"/password.txt";
            String KEYSTORE_PASS = "";
            byte[] encrypted;
            String KEYSTORE_LOCATION = System.Web.Hosting.HostingEnvironment.MapPath(nderm.Pathname) + @"certifikata.p12";
            if (File.Exists(System.Web.Hosting.HostingEnvironment.MapPath(nderm.Pathname) + @"password.txt"))
                KEYSTORE_PASS = File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath(nderm.Pathname) + @"password.txt");
            else throw new Exception("Ju lutem ngarkoni filen e passwordit!");

            using (X509Certificate2 keyStore = new X509Certificate2(KEYSTORE_LOCATION, KEYSTORE_PASS))
            {
                try
                {
                    // Load a private from a key store
                    RSA privateKey = keyStore.GetRSAPrivateKey();
                    // Convert string XML to object
                    XmlDocument request = new XmlDocument();
                    request.LoadXml(REQUEST_TO_SIGN);
                    // Create key info element
                    KeyInfo keyInfo = new KeyInfo();
                    KeyInfoX509Data keyInfoData = new KeyInfoX509Data();
                    keyInfoData.AddCertificate(keyStore);
                    keyInfo.AddClause(keyInfoData);
                    // Create signature reference
                    Reference reference = new Reference("");
                    reference.AddTransform(new XmlDsigEnvelopedSignatureTransform(false));
                    reference.AddTransform(new XmlDsigExcC14NTransform(false));
                    reference.DigestMethod = XML_DIG_METHOD;
                    reference.Uri = "#" + XML_REQUEST_ID;
                    // Create signature
                    SignedXml xml = new SignedXml(request);
                    xml.SigningKey = privateKey;
                    xml.SignedInfo.CanonicalizationMethod = SignedXml.XmlDsigExcC14NTransformUrl;
                    xml.SignedInfo.SignatureMethod = XML_SIG_METHOD;
                    xml.KeyInfo = keyInfo;
                    xml.AddReference(reference);
                    xml.ComputeSignature();
                    // Add signature element to the request
                    XmlElement signature = xml.GetXml();
                    request.DocumentElement.AppendChild(signature);
                    // Convert signed request to string and print
                    StringWriter sw = new StringWriter();
                    XmlTextWriter xw = new XmlTextWriter(sw);
                    request.WriteTo(xw);
                    var signedDoc = sw.ToString();
                    signedDoc = "<SOAP-ENV:Envelope xmlns:SOAP-ENV=\"http://schemas.xmlsoap.org/soap/envelope/\"><SOAP-ENV:Header/><SOAP-ENV:Body>" + signedDoc + "</SOAP-ENV:Body></SOAP-ENV:Envelope>";
                    string[] responseCode = InvokeService(signedDoc, "ResponseCode", true);
                    if (responseCode[1] != null)
                    {
                        return false;
                    }
                    else
                        return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
        public static string gjeneroArkenDitore(clsNdermarrje nderm, string vlera, string data, string arka, string kodiTCR)
        {
            var uuid = Guid.NewGuid().ToString();
            uuid = $"\"{uuid}\"";
            string dataDergimit = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz");
            dataDergimit = $"\"{dataDergimit}\"";
            vlera = String.Format("{0:0.00}", Convert.ToDouble(vlera));
            vlera = $"\"{vlera}\"";
            arka = $"\"{arka}\"";
            var nipt = nderm.NdermarrjeNipt;
            nipt = $"\"{nipt}\"";
            kodiTCR = $"\"{kodiTCR}\"";
            const string XML_SCHEMA_NS = "https://eFiskalizimi.tatime.gov.al/FiscalizationService/schema";
            const String XML_REQUEST_ID = "Request";
            const String XML_SIG_METHOD = "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256";
            const String XML_DIG_METHOD = "http://www.w3.org/2001/04/xmlenc#sha256";
            String REQUEST_TO_SIGN =
            "<RegisterCashDepositRequest " +
            " xmlns=\"https://eFiskalizimi.tatime.gov.al/FiscalizationService/schema\" " +
            " xmlns:ns2=\"http://www.w3.org/2000/09/xmldsig#\" " +
            " Id=\"Request\" " +
            " Version=\"3\">\r\n" +
            " <Header SendDateTime=" + $"{dataDergimit}" + " UUID=" + $"{uuid}" + "/>\r\n" +
            " <CashDeposit CashAmt=" + $"{vlera}" + " ChangeDateTime=" + $"{dataDergimit}" + " IssuerNUIS=" + $"{nipt}" + " Operation= \"INITIAL\" TCRCode=" + $"{kodiTCR}" + "/>\r\n" +
            "</RegisterCashDepositRequest>";
            REQUEST_TO_SIGN = REQUEST_TO_SIGN.Replace("&", "&amp;");
            string passpath = nderm.Pathname + $"/password.txt";
            String KEYSTORE_PASS = "";
            byte[] encrypted;
            String KEYSTORE_LOCATION = System.Web.Hosting.HostingEnvironment.MapPath(nderm.Pathname) + @"certifikata.p12";
            if (File.Exists(System.Web.Hosting.HostingEnvironment.MapPath(nderm.Pathname) + @"password.txt"))
                KEYSTORE_PASS = File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath(nderm.Pathname) + @"password.txt");
            else throw new Exception("Ju lutem ngarkoni filen e passwordit!");
            using (X509Certificate2 keyStore = new X509Certificate2(KEYSTORE_LOCATION, KEYSTORE_PASS))
            {
                try
                {
                    // Load a private from a key store
                    RSA privateKey = keyStore.GetRSAPrivateKey();
                    // Convert string XML to object
                    XmlDocument request = new XmlDocument();
                    request.LoadXml(REQUEST_TO_SIGN);
                    // Create key info element
                    KeyInfo keyInfo = new KeyInfo();
                    KeyInfoX509Data keyInfoData = new KeyInfoX509Data();
                    keyInfoData.AddCertificate(keyStore);
                    keyInfo.AddClause(keyInfoData);
                    // Create signature reference
                    Reference reference = new Reference("");
                    reference.AddTransform(new XmlDsigEnvelopedSignatureTransform(false));
                    reference.AddTransform(new XmlDsigExcC14NTransform(false));
                    reference.DigestMethod = XML_DIG_METHOD;
                    reference.Uri = "#" + XML_REQUEST_ID;
                    // Create signature
                    SignedXml xml = new SignedXml(request);
                    xml.SigningKey = privateKey;
                    xml.SignedInfo.CanonicalizationMethod = SignedXml.XmlDsigExcC14NTransformUrl;
                    xml.SignedInfo.SignatureMethod = XML_SIG_METHOD;
                    xml.KeyInfo = keyInfo;
                    xml.AddReference(reference);
                    xml.ComputeSignature();
                    //= Add signature element to the request
                    XmlElement signature = xml.GetXml();
                    request.DocumentElement.AppendChild(signature);
                    // Convert signed request to string and print
                    StringWriter sw = new StringWriter();
                    XmlTextWriter xw = new XmlTextWriter(sw);
                    request.WriteTo(xw);
                    var signedDoc = sw.ToString();
                    signedDoc = "<SOAP-ENV:Envelope xmlns:SOAP-ENV=\"http://schemas.xmlsoap.org/soap/envelope/\"><SOAP-ENV:Header/><SOAP-ENV:Body>" + signedDoc + "</SOAP-ENV:Body></SOAP-ENV:Envelope>";
                    return signedDoc;

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return "string";
        }
        public static object kontrolloVleratPerTCR(clsNdermarrje ndermarrje, string kodBiznesi)
        {
            if(kodBiznesi == "")
                return new { Pershkrim = "Plotesoni degen administrative!" };
            if (ndermarrje.NdermarrjeNipt == "")
                return new { Pershkrim = "Plotesoni Nipt te ndermarrja!" };
            if (new clsDegeAdministrative(kodBiznesi, ndermarrje.IdNdermarrje).KodNjesieBiznesi == "")
                return new { Pershkrim = "Mungon kodi i njesise se biznesit per degen administrative!" };
            else
                return new { Pershkrim = "Sukses" };
        }
        public static string gjeneroKodinTCR(clsNdermarrje nderm, string TRCName, string kodBiznesi)
        {
            try
            {
                string[] kodiDegaAdministrative = kodBiznesi.Split(' ');
                var degaAdministrative = new clsDegeAdministrative(kodiDegaAdministrative[0], nderm.IdNdermarrje).KodNjesieBiznesi;
                kodBiznesi = degaAdministrative;
                var uuid = Guid.NewGuid().ToString();
                uuid = $"\"{uuid}\"";
                string IssuerNUIS = nderm.NdermarrjeNipt;
                string buissnesUnitCode = kodBiznesi;
                string dataDergimit = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz");
                string dataValid = DateTime.Now.ToString("yyyy-MM-dd");
                dataDergimit = $"\"{dataDergimit}\"";
                dataValid = $"\"{dataValid}\"";
                buissnesUnitCode = $"\"{buissnesUnitCode}\"";
                IssuerNUIS = $"\"{IssuerNUIS}\"";
                TRCName = $"\"{TRCName}\"";
                string kodSoftueri = WebConfigurationManager.AppSettings["kodSoftueri"];
                string kodMirembajtesi = WebConfigurationManager.AppSettings["kodMirembajtesi"];
                kodSoftueri = $"\"{kodSoftueri}\"";
                kodMirembajtesi = $"\"{kodMirembajtesi}\"";

                const string XML_SCHEMA_NS = "https://eFiskalizimi.tatime.gov.al/FiscalizationService/schema";
                const String XML_REQUEST_ID = "Request";
                const String XML_SIG_METHOD = "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256";
                const String XML_DIG_METHOD = "http://www.w3.org/2001/04/xmlenc#sha256";
                String REQUEST_TO_SIGN =
                "<RegisterTCRRequest " +
                " xmlns=\"https://eFiskalizimi.tatime.gov.al/FiscalizationService/schema\" " +
                " xmlns:ns2=\"http://www.w3.org/2000/09/xmldsig#\" " +
                " Id=\"Request\" " +
                " Version=\"3\">\r\n" +
                " <Header SendDateTime=" + $"{dataDergimit}" + " UUID=" + $"{uuid}" + "/>\r\n" +
                " <TCR BusinUnitCode=" + $"{buissnesUnitCode}" + " IssuerNUIS =" + $"{IssuerNUIS}" + " MaintainerCode=" + $"{kodMirembajtesi}" + " SoftCode=" + $"{kodSoftueri}" + " TCRIntID=" + $"{TRCName}" + " ValidFrom=" + $"{dataValid}" + " Type=\"REGULAR\"/>\r\n" +
                "</RegisterTCRRequest>";
                REQUEST_TO_SIGN = REQUEST_TO_SIGN.Replace("&", "&amp;");
                string passpath = nderm.Pathname + $"/password.txt";
                String KEYSTORE_PASS = "";
                byte[] encrypted;
                String KEYSTORE_LOCATION = System.Web.Hosting.HostingEnvironment.MapPath(nderm.Pathname) + @"certifikata.p12";
                if (File.Exists(System.Web.Hosting.HostingEnvironment.MapPath(nderm.Pathname) + @"password.txt"))
                    KEYSTORE_PASS = File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath(nderm.Pathname) + @"password.txt");
                else throw new Exception("Ju lutem ngarkoni filen e passwordit!");
                using (X509Certificate2 keyStore = new X509Certificate2(KEYSTORE_LOCATION, KEYSTORE_PASS))

                    try
                    {
                        // Load a private from a key store
                        RSA privateKey = keyStore.GetRSAPrivateKey();
                        // Convert string XML to object
                        XmlDocument request = new XmlDocument();
                        request.LoadXml(REQUEST_TO_SIGN);
                        // Create key info element
                        KeyInfo keyInfo = new KeyInfo();
                        KeyInfoX509Data keyInfoData = new KeyInfoX509Data();
                        keyInfoData.AddCertificate(keyStore);
                        keyInfo.AddClause(keyInfoData);
                        // Create signature reference
                        Reference reference = new Reference("");
                        reference.AddTransform(new XmlDsigEnvelopedSignatureTransform(false));
                        reference.AddTransform(new XmlDsigExcC14NTransform(false));
                        reference.DigestMethod = XML_DIG_METHOD;
                        reference.Uri = "#" + XML_REQUEST_ID;
                        // Create signature
                        SignedXml xml = new SignedXml(request);
                        xml.SigningKey = privateKey;
                        xml.SignedInfo.CanonicalizationMethod = SignedXml.XmlDsigExcC14NTransformUrl;
                        xml.SignedInfo.SignatureMethod = XML_SIG_METHOD;
                        xml.KeyInfo = keyInfo;
                        xml.AddReference(reference);
                        xml.ComputeSignature();
                        //= Add signature element to the request
                        XmlElement signature = xml.GetXml();
                        request.DocumentElement.AppendChild(signature);
                        // Convert signed request to string and print
                        StringWriter sw = new StringWriter();
                        XmlTextWriter xw = new XmlTextWriter(sw);
                        request.WriteTo(xw);
                        var signedDoc = sw.ToString();
                        signedDoc = "<SOAP-ENV:Envelope xmlns:SOAP-ENV=\"http://schemas.xmlsoap.org/soap/envelope/\"><SOAP-ENV:Header/><SOAP-ENV:Body>" + signedDoc + "</SOAP-ENV:Body></SOAP-ENV:Envelope>";
                        return signedDoc;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                return "string";
            }
            catch(Exception ex)
            {
                return "Problem certifikate";
            }
        }
        public static bool kontrolloCertifikat(clsNdermarrje nderm)
        {
            nderm = new clsNdermarrje(nderm.NdermarrjeKodi);
            if (nderm.Pathname != "")
                return true;
            else
                return false;
        }
        public static string merrFaturatEinvoice(clsNdermarrje nderm, string veprimi, DateTime dtRegjistrimi)
        {
            nderm = new clsNdermarrje(nderm.NdermarrjeKodi);
            if (veprimi == "shitje")
                veprimi = "SELLER";
            else
                veprimi = "BUYER";
            var uuid = Guid.NewGuid().ToString();
            DateTimeOffset sourceDate = new DateTimeOffset(dtRegjistrimi);

            DateTimeOffset timezoneIShqiperise = TimeZoneInfo.ConvertTime(sourceDate,
                          TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time"));

            var dateDergimi = $"\"{timezoneIShqiperise.ToString("yyyy-MM-ddTHH\\:mm\\:sszzz")}\"";
            uuid = $"\"{uuid}\"";
            const String XML_SCHEMA_NS = "https://Einvoice.tatime.gov.al/ EinvoiceService/schema";
            const String XML_SIG_METHOD = "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256";
            const String XML_REQUEST_ID = "Request";
            const String XML_DIG_METHOD = "http://www.w3.org/2001/04/xmlenc#sha256";
            String REQUEST_TO_SIGN =
            "<GetEinvoicesRequest " +
            " xmlns=\"https://Einvoice.tatime.gov.al/EinvoiceService/schema\" " +
            " xmlns:ns2=\"http://www.w3.org/2000/09/xmldsig#\" " +
            " Id=\"Request\" " +
            " Version=\"1\">\r\n" +
            " <Header SendDateTime=" + $"{dateDergimi}" + " UUID=" + $"{uuid}" + "/>\r\n" +
            "<PartyType>" + $"{veprimi}" + "</PartyType>\r\n" +
            "</GetEinvoicesRequest>";
            REQUEST_TO_SIGN = REQUEST_TO_SIGN.Replace("&", "&amp;");
            string passpath = nderm.Pathname + $"/password.txt";
            String KEYSTORE_PASS = "";
            byte[] encrypted;
            String KEYSTORE_LOCATION = System.Web.Hosting.HostingEnvironment.MapPath(nderm.Pathname) + @"certifikata.p12";
            if (File.Exists(System.Web.Hosting.HostingEnvironment.MapPath(nderm.Pathname) + @"password.txt"))
                KEYSTORE_PASS = File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath(nderm.Pathname) + @"password.txt");
            else throw new Exception("Ju lutem ngarkoni filen e passwordit!");

            using (X509Certificate2 keyStore = new X509Certificate2(KEYSTORE_LOCATION, KEYSTORE_PASS))
            {
                try
                {
                    // Load a private from a key store
                    RSA privateKey = keyStore.GetRSAPrivateKey();
                    // Convert string XML to object
                    XmlDocument request = new XmlDocument();
                    request.LoadXml(REQUEST_TO_SIGN);
                    // Create key info element
                    KeyInfo keyInfo = new KeyInfo();
                    KeyInfoX509Data keyInfoData = new KeyInfoX509Data();
                    keyInfoData.AddCertificate(keyStore);
                    keyInfo.AddClause(keyInfoData);
                    // Create signature reference
                    Reference reference = new Reference("");
                    reference.AddTransform(new XmlDsigEnvelopedSignatureTransform(false));
                    reference.AddTransform(new XmlDsigExcC14NTransform(false));
                    reference.DigestMethod = XML_DIG_METHOD;
                    reference.Uri = "#" + XML_REQUEST_ID;
                    // Create signature
                    SignedXml xml = new SignedXml(request);
                    xml.SigningKey = privateKey;
                    xml.SignedInfo.CanonicalizationMethod = SignedXml.XmlDsigExcC14NTransformUrl;
                    xml.SignedInfo.SignatureMethod = XML_SIG_METHOD;
                    xml.KeyInfo = keyInfo;
                    xml.AddReference(reference);
                    xml.ComputeSignature();
                    // Add signature element to the request
                    XmlElement signature = xml.GetXml();
                    request.DocumentElement.AppendChild(signature);
                    // Convert signed request to string and print
                    StringWriter sw = new StringWriter();
                    XmlTextWriter xw = new XmlTextWriter(sw);
                    request.WriteTo(xw);
                    var signedDoc = sw.ToString();
                    signedDoc = "<SOAP-ENV:Envelope xmlns:SOAP-ENV=\"http://schemas.xmlsoap.org/soap/envelope/\"><SOAP-ENV:Header/><SOAP-ENV:Body>" + signedDoc + "</SOAP-ENV:Body></SOAP-ENV:Envelope>";
                    return signedDoc;
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }
        //func eic
        public static string merrVleratEFaturaveEinvoice(string xml, string elementi, bool Einvoice)
        {
            string soapResult = string.Empty;

            string linkFiskalizimi = WebConfigurationManager.AppSettings["urlFiskalizimi"];
            string urlEinvoice = WebConfigurationManager.AppSettings["urlEinvoice"];

            try
            {
                WebRequest webRequest;
                if (Einvoice)
                {
                    webRequest = CreateSOAPWebRequest(urlEinvoice);
                }
                else
                {
                    webRequest = CreateSOAPWebRequest(linkFiskalizimi);
                }
                using (Stream stream = webRequest.GetRequestStream())
                {
                    using (StreamWriter stmw = new StreamWriter(stream))
                    {
                        stmw.Write(xml);
                    }
                }
                using (WebResponse webResponse = webRequest.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                    {

                        //reading stream    
                        var ServiceResult = rd.ReadToEnd();
                        XmlDocument xmldoc = new XmlDocument();
                        xmldoc.LoadXml(ServiceResult);
                        XmlNodeList nodeList;
                        if (xmldoc.GetElementsByTagName("ns2:Einvoices").Count != 0)
                            nodeList = xmldoc.GetElementsByTagName("ns2:Einvoices");
                        else
                            nodeList = xmldoc.GetElementsByTagName("Einvoices");
                        string responseString = "";
                        foreach (XmlNode node in nodeList)
                        {
                            responseString = "<Einvoices>" + node.InnerXml + "</Einvoices>";
                            responseString = responseString.Replace("ns2:", "");
                        }
                        return responseString;
                    }
                }
            }
            catch (WebException ex)
            {
                using (var stream = ex.Response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    var ServiceResult = reader.ReadToEnd();
                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.LoadXml(ServiceResult);
                    XmlNodeList nodeList = xmldoc.GetElementsByTagName("faultstring");
                    string responseString = "";
                    foreach (XmlNode node in nodeList)
                    {
                        responseString = node.InnerText;
                    }
                    if (responseString != "Buyer TIN doesn't exist in RTP." && responseString != "Buyers TIN is not in the correct format." && responseString != "Buyer is not active in the RTP.")
                        responseString = "Ndodhi Nje Gabim!";
                    return responseString;
                }
            }
        }
        public static string merrEinvoice(clsNdermarrje nderm, string eic, DateTime dtRegjistrimi)
        {
            nderm = new clsNdermarrje(nderm.NdermarrjeKodi);
            var uuid = Guid.NewGuid().ToString();
            DateTimeOffset sourceDate = new DateTimeOffset(dtRegjistrimi);

            DateTimeOffset timezoneIShqiperise = TimeZoneInfo.ConvertTime(sourceDate,
                          TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time"));

            var dateDergimi = $"\"{timezoneIShqiperise.ToString("yyyy-MM-ddTHH\\:mm\\:sszzz")}\"";
            uuid = $"\"{uuid}\"";
            const String XML_SCHEMA_NS = "https://Einvoice.tatime.gov.al/ EinvoiceService/schema";
            const String XML_SIG_METHOD = "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256";
            const String XML_REQUEST_ID = "Request";
            const String XML_DIG_METHOD = "http://www.w3.org/2001/04/xmlenc#sha256";
            String REQUEST_TO_SIGN =
            "<GetEinvoicesRequest " +
            " xmlns=\"https://Einvoice.tatime.gov.al/EinvoiceService/schema\" " +
            " xmlns:ns2=\"http://www.w3.org/2000/09/xmldsig#\" " +
            " Id=\"Request\" " +
            " Version=\"1\">\r\n" +
            " <Header SendDateTime=" + $"{dateDergimi}" + " UUID=" + $"{uuid}" + "/>\r\n" +
            "<EIC>" + $"{eic}" + "</EIC>\r\n" +
            "</GetEinvoicesRequest>";
            REQUEST_TO_SIGN = REQUEST_TO_SIGN.Replace("&", "&amp;");
            string passpath = nderm.Pathname + $"/password.txt";
            String KEYSTORE_PASS = "";
            byte[] encrypted;
            String KEYSTORE_LOCATION = System.Web.Hosting.HostingEnvironment.MapPath(nderm.Pathname) + @"certifikata.p12";
            if (File.Exists(System.Web.Hosting.HostingEnvironment.MapPath(nderm.Pathname) + @"password.txt"))
                KEYSTORE_PASS = File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath(nderm.Pathname) + @"password.txt");
            else throw new Exception("Ju lutem ngarkoni filen e passwordit!");

            using (X509Certificate2 keyStore = new X509Certificate2(KEYSTORE_LOCATION, KEYSTORE_PASS))
            {
                try
                {
                    // Load a private from a key store
                    RSA privateKey = keyStore.GetRSAPrivateKey();
                    // Convert string XML to object
                    XmlDocument request = new XmlDocument();
                    request.LoadXml(REQUEST_TO_SIGN);
                    // Create key info element
                    KeyInfo keyInfo = new KeyInfo();
                    KeyInfoX509Data keyInfoData = new KeyInfoX509Data();
                    keyInfoData.AddCertificate(keyStore);
                    keyInfo.AddClause(keyInfoData);
                    // Create signature reference
                    Reference reference = new Reference("");
                    reference.AddTransform(new XmlDsigEnvelopedSignatureTransform(false));
                    reference.AddTransform(new XmlDsigExcC14NTransform(false));
                    reference.DigestMethod = XML_DIG_METHOD;
                    reference.Uri = "#" + XML_REQUEST_ID;
                    // Create signature
                    SignedXml xml = new SignedXml(request);
                    xml.SigningKey = privateKey;
                    xml.SignedInfo.CanonicalizationMethod = SignedXml.XmlDsigExcC14NTransformUrl;
                    xml.SignedInfo.SignatureMethod = XML_SIG_METHOD;
                    xml.KeyInfo = keyInfo;
                    xml.AddReference(reference);
                    xml.ComputeSignature();
                    // Add signature element to the request
                    XmlElement signature = xml.GetXml();
                    request.DocumentElement.AppendChild(signature);
                    // Convert signed request to string and print
                    StringWriter sw = new StringWriter();
                    XmlTextWriter xw = new XmlTextWriter(sw);
                    request.WriteTo(xw);
                    var signedDoc = sw.ToString();
                    signedDoc = "<SOAP-ENV:Envelope xmlns:SOAP-ENV=\"http://schemas.xmlsoap.org/soap/envelope/\"><SOAP-ENV:Header/><SOAP-ENV:Body>" + signedDoc + "</SOAP-ENV:Body></SOAP-ENV:Envelope>";
                    return signedDoc;
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }
        public static bool dergoWebhookNotify(object objekti, bool einvoice)
        {
            string result = string.Empty;
            string linkFiskalizimiWebhook = WebConfigurationManager.AppSettings["SuksesFiskalizimi"];
            string linkEinvoiceWebhook = WebConfigurationManager.AppSettings["SuksesFiskalizimi"];            
            try
            {
                WebRequest webRequest;
                if (einvoice)
                {
                    webRequest = CreateJSONWebRequest(linkEinvoiceWebhook);
                }
                else
                {
                    webRequest = CreateJSONWebRequest(linkFiskalizimiWebhook);
                }
                using (Stream stream = webRequest.GetRequestStream())
                {
                    using (StreamWriter stmw = new StreamWriter(stream))
                    {
                        stmw.Write(JsonConvert.SerializeObject(objekti));
                    }
                }
                using (WebResponse webResponse = webRequest.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                    {

                        var ServiceResult = rd.ReadToEnd();

                        return true;
                    }
                }
            }
            catch(WebException ex)
            {
                return false;
            }
            
        }
        public static string[] InvokeService(string xml, string elementi, bool Einvoice)
        {
            string soapResult = string.Empty;
            string linkFiskalizimi = WebConfigurationManager.AppSettings["urlFiskalizimi"];
            string linkEinvoice = WebConfigurationManager.AppSettings["urlEinvoice"];
            if (linkFiskalizimi == null)
            {
                string[] fiskalizimiError = new string[1];
                fiskalizimiError[0] = "Fiskalizimi Nuk Pergjigjet!";
                return fiskalizimiError;

            }

            try
            {
                WebRequest webRequest;
                if (Einvoice)
                {
                    webRequest = CreateSOAPWebRequest(linkEinvoice);
                }
                else
                {
                    webRequest = CreateSOAPWebRequest(linkFiskalizimi);
                }
                using (Stream stream = webRequest.GetRequestStream())
                {
                    using (StreamWriter stmw = new StreamWriter(stream))
                    {
                        stmw.Write(xml);
                    }
                }
                using (WebResponse webResponse = webRequest.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                    {

                        //reading stream    
                        var ServiceResult = rd.ReadToEnd();
                        XmlDocument xmldoc = new XmlDocument();
                        xmldoc.LoadXml(ServiceResult);
                        XmlNodeList nodeList = xmldoc.GetElementsByTagName(elementi);
                        string[] responseString = new string[4];
                        responseString[2] = ServiceResult;
                        foreach (XmlNode node in nodeList)
                        {
                            responseString[0] = node.InnerText;
                        }
                        return responseString;
                    }
                }
            }
            catch (WebException ex)
            {
                if(ex.Message == "The underlying connection was closed: An unexpected error occurred on a receive.")
                {
                    string[] responseString = new string[4];
                    responseString[2] = "";
                    responseString[1] = "Fatura nuk u be Einvoice";
                    return responseString;
                }
                using (var stream = ex.Response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    var ServiceResult = reader.ReadToEnd();
                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.LoadXml(ServiceResult);
                    XmlNodeList nodeList = xmldoc.GetElementsByTagName("faultstring");
                    string[] responseString = new string[4];
                    responseString[2] = ServiceResult;
                    foreach (XmlNode node in nodeList)
                    {
                        responseString[0] = node.InnerText;


                    }
                    if (responseString[0] != "Buyer TIN doesn't exist in RTP." && responseString[0] != "Buyers TIN is not in the correct format." && responseString[0] != "Buyer is not active in the RTP.")
                    {
                        responseString[0] = responseString[0];
                        if (!Einvoice)
                        {
                            XmlNodeList nodeListCode = xmldoc.GetElementsByTagName("code");
                            foreach (XmlNode nodeCode in nodeListCode)
                            {
                                responseString[1] = $"({nodeCode.InnerText})";
                            }
                        }
                        else
                        {
                            XmlNodeList nodeListCode = xmldoc.GetElementsByTagName("code");
                            foreach (XmlNode nodeCode in nodeListCode)
                            {
                                responseString[1] = $"({nodeCode.InnerText})";
                            }
                        }
                    }

                    return responseString;
                }
            }
        }
        public static HttpWebRequest CreateSOAPWebRequest(string url)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create($@"{url}");
            webRequest.Headers.Add(@"SOAP:Action");
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }
        public static HttpWebRequest CreateJSONWebRequest(string url)
        {
            string Token = WebConfigurationManager.AppSettings["webhookAuthToken"];
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Headers.Add("Authorization", "Bearer " + Token);
            httpWebRequest.Method = "POST";
            return httpWebRequest;
        }
        public static string ruajZipFatura(string xml, string IIC, string data, bool eshteRidergim, HttpResponse Response)
        {
            var pathDir = HttpContext.Current.Server.MapPath(null) + @"/faturat/";
            DirectoryExtension.CreateDirIfNotExists(pathDir);
            var data2 = DateTime.Now.ToString("yyyyMMddHHmmss");
            using (var memoryStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    var zipFile = archive.CreateEntry($"{data2}_{IIC}_request.xml");

                    using (var entryStream = zipFile.Open())
                    using (var streamWriter = new StreamWriter(entryStream))
                    {
                        streamWriter.Write(xml);
                    }
                }
                if (!eshteRidergim)
                {
                    using (var fileStream = new FileStream($"{pathDir}{data}_request.zip", FileMode.Create))
                    {
                        memoryStream.Seek(0, SeekOrigin.Begin);
                        memoryStream.CopyTo(fileStream);
                    }
                }
                else
                {
                    using (FileStream zipToOpen = new FileStream($"{pathDir}{data}_request.zip", FileMode.Open))
                    {
                        using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update))
                        {
                            ZipArchiveEntry faturatXml = archive.CreateEntry($"{data2}_{IIC}_request.xml");
                            using (StreamWriter writer = new StreamWriter(faturatXml.Open()))
                            {
                                writer.Write(xml);
                            }
                        }
                    }
                }
                return $"{pathDir}{data}_request.zip";
            }
        }
        
        public static void downloadFileToClientZip(string filePathToOpen, HttpResponse Response, int idPerdoruesi)
        {
            FileInfo myfile = new FileInfo(filePathToOpen);
            if (myfile.Exists)
            {
                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Charset = "";
                Response.AddHeader("Connection", "Keep-Alive");
                Response.ContentType = "application/zip";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(myfile.Name, Encoding.UTF8));
                Response.WriteFile(myfile.FullName);
                Response.Flush();
                File.Delete(filePathToOpen);
                HttpContext.Current.Response.SuppressContent = true;
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                //Response.End();

                //return File.ReadAllBytes(filePathToOpen);
                
                

            }
            return;


        }
    }
}
