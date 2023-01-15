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
            string publicKey = @"-----BEGIN PUBLIC KEY-----
                MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA8QcGe1gJSuzEHA38Z74O
                hsfe1prG9J6grAC1ulxErU6P+XXlGMQZL+cnNe/GuaLgedyaYVV0YuXE2sSzPCxD
                lV4ceP0AsFottNaitCbXTUG6jTcOMdNNICTG34yqtgXS8zPkj05aJyNAFynY5NvV
                z+k69GFN49uCoXU69FApoMLJfuKGP77ZdsevZJGmjUcIavWuk7lzuqtjAPTDnCjf
                Hdiu0OgHPJEJchumaM2aQmkAHg2utPjB/GhgjL90Hq9DHM7+Wy0vcf5wyoI8UyGS
                vE7x29Pv1AdUvqxFopoI+fo6ok68qnlbu2ODJ72uhhQa03rZStlgeGE6V0UwEKWB
                iwIDAQAB
                -----END PUBLIC KEY-----";
            if (email_inline.Text == "") return;
            RSA = ImportPublicKey(publicKey);
            string timeStamp = DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
            string password = generateRandomPassword();
            string organizata = clsKontrollePerFiskalizimin.ktheInitialCatalogTeLoguar();
            object json = new
            {
                timestamp = timeStamp,
                email = "toshikimeriku@gmail.com",
                password = password,
                organization =organizata
            };
            plaintext = ByteConverter.GetBytes(JsonConvert.SerializeObject(json));
            encryptedtext = clsFunksione.encrypt(plaintext, RSA.ExportParameters(false), false);

            string base64 = Convert.ToBase64String(encryptedtext);
            

            clsFunksione.gjeneroLinkPerKonfirmimEmaili("henrik.balla@imb.al",base64);

        }
        public static string generateRandomPassword()
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()_+=-";
            StringBuilder stringBuilder = new StringBuilder();
            Random random = new Random();
            for(var i = 0; i <= 20; i++)
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