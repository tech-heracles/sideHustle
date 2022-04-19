using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.DbRegjistrim
{
    public class clsKasaKokaFile : clsKasaKoka
    {
        #region Atribute

        private string path;
        private bool mosFshiOrigjine;
        private bool fshiPLU;

        #endregion

        #region Properties

        public string Path
        {
            get { return path; }
        }

        public bool MosFshiOrigjine
        {
            get { return mosFshiOrigjine; }
        }

        public bool FshiPLU
        {
            get
            {
                return fshiPLU;
            }

            set
            {
                fshiPLU = value;
            }
        }

        #endregion

        #region Konstruktori

        public clsKasaKokaFile() : base()
        {

        }


         public clsKasaKokaFile(string llojiKases, string module, string idTransaksioni, bool printimMeIp, string advertisement,string nrFature, string menyrePagese, bool fatureTatimore, double zbritjeTotale,string path, bool mosFshiOrigjine,  string kodNdermarrja, string pathWebService, int idUser, bool printoKodArtikulli, bool printoBarKod, bool kthim, bool meShifraDhjetore, bool meTVSH, double kursi, bool kopjeFature, bool printimManual, bool eshtePrinterFiskal, bool fshiPLU, int idShop) : base(llojiKases, module,
      idTransaksioni, printimMeIp, advertisement, nrFature, menyrePagese, fatureTatimore, zbritjeTotale, kodNdermarrja, pathWebService, idUser, printoKodArtikulli, printoBarKod, kthim, meShifraDhjetore, meTVSH, kursi, kopjeFature, printimManual, eshtePrinterFiskal, idShop)
        {
            this.path = path;
            this.mosFshiOrigjine = mosFshiOrigjine;
            this.fshiPLU = fshiPLU;
        }

  //      public clsKasaKokaFile (string llojiKases, string kompania, string idTransaksioni, int idshop, string nrFature, string menyrePagese, bool fatureTatimore,
  //double zbritjeTotale, string path, bool mosFshiOrigjine, string kodNdermarrja, string pathWebService, int idUser, bool printoKodArtikulli, bool printoBarKod, bool kthim, bool meShifraDhjetore, bool meTVSH, double kursi, bool kopjeFature, bool printimManual, bool eshtePrinterFiskal) :    base (llojiKases,  kompania,
  //    idTransaksioni,  idshop,  printimMeIp,  advertisement,  menyrePagese,  fatureTatimore,  zbritjeTotale,  kodNdermarrja,  pathWebService,  idUser,  printoKodArtikulli,  printoBarKod,  kthim,  meShifraDhjetore,  meTVSH,  kursi,  kopjeFature,  printimManual,  eshtePrinterFiskal)
  //      {
  //          this.path = path;
  //          this.mosFshiOrigjine = mosFshiOrigjine;
  //      }
        #endregion

        public override clsMesazh printoNeKase(bool ruajPergjigje, bool derguar, int idShitje, bool veprimBanke)
        {
            DbInventari.clsPrintimeKase printimiKase;
            clsMesazh mesazh = dergoKerkesePrintimi();
            DbInventari.clsPrintimeKase printimiKaseMbetur = new DbInventari.clsPrintimeKase();
            clsMesazh mesazhRuajtje = new clsMesazh();
            if (ruajPergjigje)
            {
                if (mesazh.Status)
                {
                    printimiKase = new DbInventari.clsPrintimeKase(veprimBanke ? 0 : idShitje, true, derguar, "File", Idshop, "U printua ne kase!", DateTime.Now, IdUser, veprimBanke ? idShitje : 0);
                }
                else
                {
                    printimiKase = new DbInventari.clsPrintimeKase(veprimBanke ? 0 : idShitje, false, derguar, "File", Idshop, "Nuk u printua ne kase!", DateTime.Now, IdUser, veprimBanke ? idShitje : 0);
                }

                //Nese ekziston idShitjes ne tabele, ath kryen thjesht modifikim te statusit te printimit.
                bool ekziston = veprimBanke ? printimiKaseMbetur.ktheDergimeKaseSipasIdBanka(idShitje) : printimiKaseMbetur.merrDergimeKaseSipasIdShitje(idShitje);
                if (!ekziston)
                    mesazhRuajtje = printimiKase.ruajPrintimKase();
                else
                {
                    printimiKase.IdKasa = printimiKaseMbetur.IdKasa;
                    mesazhRuajtje = printimiKase.modifikoPrintimKase();
                }
            }
            return mesazh;
        }
    }
}
