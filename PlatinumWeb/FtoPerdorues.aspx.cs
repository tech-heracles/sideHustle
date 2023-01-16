using DbCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using DbCore.IMBUtils.Fiskalizimi.Controls;

namespace PlatinumWeb
{

    public partial class FtoPerdorues : System.Web.UI.Page
    {
        UnicodeEncoding ByteConverter = new UnicodeEncoding();
        RSACryptoServiceProvider RSA;
        byte[] plaintext;
        byte[] encryptedtext;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void button_click(object sender, EventArgs e)
        {
            try
            {
                string publicKey = @"-----BEGIN PUBLIC KEY-----
MIICIjANBgkqhkiG9w0BAQEFAAOCAg8AMIICCgKCAgEAoDen7UdHQuEqz5dlUhpZ
sB7bBjSlo/xEbJqT1994jNAi39/d3Twd8BNg87o16Yrrhce5TwY+IEl8kHvdUNXY
rkLzZDxoBSGWV05kOW+bDd52ClGTCpJfatvtn/S7nTbdSlNBJJZ2xajFg8l9T6ST
twD49dxh8MMVK/6xzJRUyXTBBUU4d+x9PiPY70O4/q66cUUeUg7OhWKyUElYrnxg
vhETRginB+/lNxjbCQcJzGTRq36+g9A+MiWip2SQ1zdeV3A6ssWtdvuMPtijIPhX
423yoZnOC+eRW7zxEbIhcmTOJP8rF2pXruvgFEOhadIZQvgiD4PSdIEkaDrCSwXX
vlqUdr+gib3UIypfzKmRRO3drrvE6HiaPN2n/2+kOQ3zzKI7PwcXsFJIESOPV9xo
VThkDMtjqaKf9/VR1BK+wZXcMEo6pwbTmrggIRpHHQIicNfyeIGWSwEZYqgp9dLE
GXZMEEsJdZbLkmn3lRcRGeWfNF/0hokVd5Bf7SUradNZI2F2YJeM5u/PTKr2wbaJ
ZMIlG6AHVXCg3EB1Osh5W+OLE2qZBSx70hN41m85te9/X0O0377S6WiGRSpSfBjO
RCTzQHbk4zG6TcrYp/uR77BfUC91mqAH+OA4YiZZv1YKyP8+O+E0lIcMehn4UWXW
JwhY6kCDqCIFqgK6rktEaOMCAwEAAQ==
-----END PUBLIC KEY-----
";
                if (email_inline.Text == "") return;
                RSA = ImportPublicKey(publicKey);
                string timeStamp = DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
                string password = generateRandomPassword();
                string organizata = clsKontrollePerFiskalizimin.ktheInitialCatalogTeLoguar();
                object json = new
                {
                    timestamp = timeStamp,
                    organization = organizata,
                    email = email_inline.Text,
                    password = password,
                };
                plaintext = ByteConverter.GetBytes(JsonConvert.SerializeObject(json));
                encryptedtext = clsFunksione.encrypt(plaintext, RSA.ExportParameters(false), false);
                string base64 = Convert.ToBase64String(encryptedtext);
                clsFunksione.gjeneroLinkPerKonfirmimEmaili(email_inline.Text, base64);
                email_inline.Text = "";
            }
            catch(Exception ex)
            {
                return;
            }
            
            
        }
        public static string generateRandomPassword()
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()_+=-";
            StringBuilder stringBuilder = new StringBuilder();
            Random random = new Random();
            for(var i = 0; i <= 10; i++)
            {
                stringBuilder.Append(valid[random.Next(valid.Length)]);
            }
            return stringBuilder.ToString();
        }
        public static RSACryptoServiceProvider ImportPublicKey(string pem)
        {
            PemReader pr = new PemReader(new StringReader(pem));
            AsymmetricKeyParameter publicKey = (AsymmetricKeyParameter)pr.ReadObject();
            RSAParameters rsaParams = DotNetUtilities.ToRSAParameters((RsaKeyParameters)publicKey);

            RSACryptoServiceProvider csp = new RSACryptoServiceProvider();// cspParams);
            csp.ImportParameters(rsaParams);
            return csp;
        }
    }
}