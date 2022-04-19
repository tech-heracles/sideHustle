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
    public class clsKasaKokaDLL : clsKasaKoka
    {
        #region Atribute

        private string username;
        private string password;
        private string ip;
        private int port;
        private int tentativeLidhje;
        private int tentativeExe;

        #endregion

        #region Properties

        public string Username
        {
            get { return username; }
        }

        public string Password
        {
            get { return password; }
        }

        public string IP
        {
            get { return ip; }
        }

        public int Port
        {
            get { return port; }
        }

        public int TentativeLidhje
        {
            get { return tentativeLidhje; }
        }

        public int TentativeExe
        {
            get { return tentativeExe; }
        }

        #endregion

        public clsKasaKokaDLL() : base()
        {

        }

        public clsKasaKokaDLL(string llojiKases, string kompania, string idTransaksioni, int idshop, string advertisement, string menyrePagese, bool fatureTatimore,
    string username, string password, string ip, int port, int tentativeLidhje, int tentativeExe, string kodNdermarrja, string pathWebService, int idUser, bool printoKodArtikulli, bool printoBarKod, bool kthim, bool meShifraDhjetore, bool meTVSH, double kursi) : base(llojiKases, kompania, idTransaksioni, idshop,
        true, advertisement, menyrePagese, fatureTatimore, kodNdermarrja, pathWebService, idUser, printoKodArtikulli, printoBarKod, kthim, meShifraDhjetore, meTVSH, kursi)
        {
            this.username = username;
            this.password = password;
            this.ip = ip;
            this.port = port;
            this.tentativeLidhje = tentativeLidhje;
            this.tentativeExe = tentativeExe;
        }

        public clsKasaKokaDLL(string llojiKases, string kompania, string idTransaksioni, int idshop, string advertisement, string menyrePagese, bool fatureTatimore,
            double zbritjeTotale, string username, string password, string ip, int port, int tentativeLidhje, int tentativeExe, string kodNdermarrja, string pathWebService, int idUser, bool printoKodArtikulli, bool printoBarKod, bool kthim, bool meShifraDhjetore, bool meTVSH, double kursi) : base(llojiKases, kompania, idTransaksioni, idshop,
                true, advertisement, menyrePagese, fatureTatimore, zbritjeTotale, kodNdermarrja, pathWebService, idUser, printoKodArtikulli, printoBarKod, kthim, meShifraDhjetore, meTVSH, kursi)
        {
            this.username = username;
            this.password = password;
            this.ip = ip;
            this.port = port;
            this.tentativeLidhje = tentativeLidhje;
            this.tentativeExe = tentativeExe;
        }

        public clsKasaKokaDLL(string llojiKases, string kompania, string idTransaksioni, int idshop, string advertisement, string nrFature, string menyrePagese, bool fatureTatimore,
            string username, string password, string ip, int port, int tentativeLidhje, int tentativeExe, string kodNdermarrja, string pathWebService, int idUser, bool printoKodArtikulli, bool printoBarKod, bool kthim, bool meShifraDhjetore, bool meTVSH, double kursi) : base(llojiKases, kompania, idTransaksioni, idshop,
                true, advertisement, nrFature, menyrePagese, fatureTatimore, kodNdermarrja, pathWebService, idUser, printoKodArtikulli, printoBarKod, kthim, meShifraDhjetore, meTVSH, kursi)
        {
            this.username = username;
            this.password = password;
            this.ip = ip;
            this.port = port;
            this.tentativeLidhje = tentativeLidhje;
            this.tentativeExe = tentativeExe;
        }

        public clsKasaKokaDLL(string llojiKases, string kompania, string idTransaksioni, int idshop, string advertisement, string nrFature, string menyrePagese, bool fatureTatimore,
            double zbritjeTotale, string username, string password, string ip, int port, int tentativeLidhje, int tentativeExe, string kodNdermarrja, string pathWebService, int idUser, bool printoKodArtikulli, bool printoBarKod, bool kthim, bool meShifraDhjetore, bool meTVSH, double kursi) : base(llojiKases, kompania, idTransaksioni, idshop,
                true, advertisement, nrFature, menyrePagese, fatureTatimore, zbritjeTotale, kodNdermarrja, pathWebService, idUser, printoKodArtikulli, printoBarKod, kthim, meShifraDhjetore, meTVSH, kursi)
        {
            this.username = username;
            this.password = password;
            this.ip = ip;
            this.port = port;
            this.tentativeLidhje = tentativeLidhje;
            this.tentativeExe = tentativeExe;
        }

        public override clsMesazh printoNeKase(bool ruajPergjigje, bool derguar, int idShitje, bool veprimBanke)
        {
            DbInventari.clsPrintimeKase printimiKase;
            DbInventari.clsPrintimeKase printimiKaseMbetur = new DbInventari.clsPrintimeKase();
            clsMesazh mesazh = dergoKerkesePrintimi();
            clsMesazh mesazhRuajtje = new clsMesazh();
            if (!ruajPergjigje) return mesazh;
            if (mesazh.Status)
                printimiKase = new DbInventari.clsPrintimeKase(veprimBanke ? 0 : idShitje, true, derguar, IP, Idshop, "U printua ne kase!", DateTime.Now, IdUser, veprimBanke ? idShitje : 0);
            else
                printimiKase = new DbInventari.clsPrintimeKase(veprimBanke ? 0 : idShitje, false, derguar, IP, Idshop, "Nuk u printua ne kase!", DateTime.Now, IdUser, veprimBanke ? idShitje : 0);


            //Nese ekziston idShitjes ne tabele, ath kryen thjesht modifikim te statusit te printimit.
            bool ekziston = veprimBanke ? printimiKaseMbetur.ktheDergimeKaseSipasIdBanka(idShitje) : printimiKaseMbetur.merrDergimeKaseSipasIdShitje(idShitje);
            if (!ekziston)
                mesazhRuajtje = printimiKase.ruajPrintimKase();
            else
            {
                printimiKase.IdKasa = printimiKaseMbetur.IdKasa;
                mesazhRuajtje = printimiKase.modifikoPrintimKase();
            }
            return mesazh.Status ? new clsMesazh(true, "U printua ne kase!") : new clsMesazh(false, "Nuk u printua ne kase!"); 
        }
    }
}
