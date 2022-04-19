using DbCore.DbAdmin;
using DbCore.DbShare;
using DbCore.IMBUtils.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.DbRegjistrim
{
    public class KrijuesKasash
    {
        public static (clsMesazh, string, clsMesazh) printoNeKase(int idKonfigurimiKases, bool kasaCheck, string perqindjeText, clsKokaShitje shitje, int idPerdoruesi, int idNdermarrje, string ip, bool meTVSH, bool fshiPLU)
        {
            clsMesazh mesazhInfo = new clsMesazh(false);
            clsKasaKoka printimKase;
            string alternativa = "KasaIMB";
            clsNdermarrje ndermarrja = new clsNdermarrje(idNdermarrje);
            ndermarrja.merrSipasID();
            clsPerdorues per = new clsPerdorues(idPerdoruesi);
            clsVleraKonfigurimiKasa vlKonf = new clsVleraKonfigurimiKasa();
            int plu = int.Parse(vlKonf.merrPLUActualNumberPerKase(idNdermarrje, idKonfigurimiKases));
            if (plu == -1)
                plu = 1;
            if (plu > 9000)
                mesazhInfo.Status = true;

            if (!fshiPLU)
             alternativa = clsAlternativaKushti.getAlternativa(shitje.IdKonfigAmbjente, "DK");

            clsKonfigurimKase kasa = new clsKonfigurimKase(idKonfigurimiKases);
            if (kasaCheck && kasa.IdKonfigurimi == 0)
                return (new clsMesazh(false, "Fatura nuk u printua ne kase. Ju lutemi, zgjidhni kasen!"), null, mesazhInfo);
            if (kasa.IdKonfigurimi == 0)
            {
                return (new clsMesazh(false, "Nuk keni konfiguruar kase per kete ndermarje"), null, mesazhInfo);
            }
            if (shitje.Totali < 0)
            {
                return (new clsMesazh(false, "Kasa nuk pranon fature me total negativ. Fatura nuk u regjistrua ne kase!"), null, mesazhInfo);
            }

            double perqindje;
            if (!double.TryParse(perqindjeText, out perqindje))
                perqindje = 0;

            string llojkase = kasa.Pershkrimi.Substring(0, kasa.Pershkrimi.Length - ndermarrja.IdNdermarrje.ToString().Length);

            if (ip == "")// nese ska ardhur ip do merret nga konfigurimi
                ip = kasa.OColVlerat.ktheVlereOpsioni("IPKASE");

            if (alternativa == "WebService")
                printimKase = new clsKasaKokaDLL(kasa.Kodi, "AlphaWEB", per.PerdoruesUsername + "_" + DateTime.Now.ToString("yyyyMMddhhmmss"),
                     shitje.IdDegeAdministrative, "www.vodafone.al", shitje.NrSerial, MenyrePagese.Pagese.ToString(), shitje.Kupon, perqindje, "admin", "admin", ip, 80, 1, 1,
                     ndermarrja.NdermarrjeKodi, kasa.OColVlerat.ktheVlereOpsioni("URL"), idPerdoruesi, kasa.OColVlerat.ktheVlereOpsioniKasaBOOL("printoKodArtikulli"),
                     false, kasa.OColVlerat.ktheVlereOpsioniKasaBOOL("PRINTBARKOD"), kasa.OColVlerat.ktheVlereOpsioniKasaBOOL("MESHIFRADHJETORE"), meTVSH, shitje.Kursi);
            else
            {

                bool printoKodArtikulli = kasa.OColVlerat.ktheVlereOpsioniKasaBOOL("printoKodArtikulli");
                bool PRINTBARKOD = kasa.OColVlerat.ktheVlereOpsioniKasaBOOL("PRINTBARKOD");
                bool kopjeFature = kasa.OColVlerat.ktheVlereOpsioniKasaBOOL("NRKOPJESH");
                bool printimManual = kasa.OColVlerat.ktheVlereOpsioniKasaBOOL("PRINTOMANUALISHTNGAKASA");
                bool eshtePrinteFiskal = (kasa.OColVlerat.ktheVlereOpsioni("KASEAPOPRINTER") == "False");
                string pathi = kasa.OColVlerat.ktheVlereOpsioni("KASEFISKALEPATH");
                if (fshiPLU)
                {
                    int index = pathi.LastIndexOf("\\");
                    if (index > 0)
                    pathi = pathi.Substring(0, index+1) +"pludel.txt";
                }
                //duhet ndryshuar si menyre per ta bere ruajtjen e konfigurimeve ne sql per llojkase qe shkon si parameter ne nodekasa.. 
                //per momentin po behet me switch per te mos prishur kasen e vjeter tek ambjenti konfigurim kase
                llojkase = ktheLlojKase(llojkase);                

                printimKase = new clsKasaKokaFile(llojkase, "AlphaWEB", per.PerdoruesUsername + "_" + DateTime.Now.ToString("yyyyMMddhhmmss"), false,
                "", shitje.NrSerial, ((MenyrePagese)shitje.IdMenyrePagese).ToString(), shitje.Kupon, perqindje, pathi, true,
                ndermarrja.NdermarrjeKodi, kasa.OColVlerat.ktheVlereOpsioni("URL"), idPerdoruesi, printoKodArtikulli,
               PRINTBARKOD, false, kasa.OColVlerat.ktheVlereOpsioniKasaBOOL("MESHIFRADHJETORE"), meTVSH, shitje.Kursi, kopjeFature, printimManual, eshtePrinteFiskal, fshiPLU, shitje.IdDegeAdministrative);

            }
            try
            {
                if (!fshiPLU)
                {
                    if (shitje.OColTrupiShitje == null)
                        shitje.mbushTrupShitje();
                    printimKase.krijoKaseTrupi(llojkase, shitje.OColTrupiShitje, idNdermarrje, kasa, plu, idKonfigurimiKases);
                }
            }
            catch (Exception Ex)
            {
                return (new clsMesazh(false, Ex.Message.ToString()), null, mesazhInfo);
            }

            //kerkesa ne Client Side
            bool printoServer = kasa.OColVlerat.ktheVlereOpsioniKasaBOOL("PRINTONESERVER");
            if (printoServer)
                return (printimKase.printoNeKase(true, true, shitje.IdShitjeKoka,false), null, mesazhInfo);

            //string mesazhi = string.Empty;
            //if (plu > 9000)

            //    mesazhi =  new MesazhInformimi(MessagesResource.Messages["df"]);
            return (new clsMesazh(true, "ok"), Newtonsoft.Json.JsonConvert.SerializeObject(printimKase), mesazhInfo);
        }

        public static clsMesazh printoNeKase(clsKonfigurimKase _kasa, string perqindjeText, DbArkaBanka.clsVeprimBankaKoka arkaBanka, int idPerdoruesi, int idNdermarrje, string ip)
        {
            clsKasaKoka printimKase;
            clsNdermarrje ndermarrja = new clsNdermarrje(idNdermarrje);
            ndermarrja.merrSipasID();
            clsPerdorues per = new clsPerdorues(idPerdoruesi);

            double perqindje;
            if (!double.TryParse(perqindjeText, out perqindje))
                perqindje = 0;
            //if (_kasa != null)
            string llojkase = _kasa.Pershkrimi.Substring(0, _kasa.Pershkrimi.Length - ndermarrja.IdNdermarrje.ToString().Length);
            printimKase = new clsKasaKokaDLL("DITRONZIP", "AlphaWEB", per.PerdoruesUsername + "_" + DateTime.Now.ToString("yyyyMMddhhmmss"),
                    arkaBanka.IdDegeAdministrative, "www.vodafone.al", arkaBanka.NrSerial, MenyrePagese.Pagese.ToString(), false, perqindje, "admin", "admin", ip, 80, 1, 1,
                    ndermarrja.NdermarrjeKodi, _kasa.OColVlerat.ktheVlereOpsioni("URL"), idPerdoruesi, false,
                    false, false, _kasa.OColVlerat.ktheVlereOpsioniKasaBOOL("MESHIFRADHJETORE"), false, arkaBanka.Kursi);

            printimKase.krijoKaseTrupi(arkaBanka, idNdermarrje);
            return printimKase.printoNeKase(true, true, arkaBanka.IdKoka, true);
        }

        private static string ktheLlojKase(string llojKase)
        {
            switch (llojKase)
            {
                case "IVA":
                    return "KasaIVA";
                case "AED":
                    return "KasaAED";
                case "BTN":
                    return "BNTElectronics";
                case "CKVNOKI":
                    return "KasaCKV";
                case "PKP":
                    return "KasaPKP";
                case "GEKOS":
                    return "KasaGEKOS";
                case "BNTAClassSkedar":
                    return "BntAClass";
                case "BNTAClass":
                    return "BntAClass";
                default:
                    return llojKase;
            }
        }
    }
}
