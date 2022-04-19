using DbCore.DbAdmin;
using DbCore.DbOTC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DbCore.DbRegjistrim;
using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.Extensions;
namespace DbCore.Integrime.OTC
{
    public static class SMSHelper
    {

        public static string KrijoSMSipasPageses(OTCPagesa pagesa)
        {
            string mesazhi = MerrSmsTemplate(pagesa.StatusTransaksioni);
            var fatura = pagesa.Faturat[0];

            if (OTCLlojSherbimi.UKT == pagesa.LlojPagese)
                mesazhi = ShtoNeMesazhTarifenESherbimit(mesazhi);
            if (fatura.LlojFatureOSHEE == LlojFatureOSHEE.AktMarreveshje)
                mesazhi = ModifikoMesazhPerAktMarreveshje(mesazhi);
            mesazhi = mesazhi.Replace("[llojFature]", pagesa.LlojPagese.ToString());
            mesazhi = mesazhi.Replace("[nrFature]", fatura.NrFature);
            mesazhi = mesazhi.Replace("[nrKontrate]", fatura.NrKontrate);
            mesazhi = mesazhi.Replace("[principal]", fatura.VleraFillestareFatures.ToString("n2"));
            mesazhi = mesazhi.Replace("[kamate]", fatura.Interesi.ToString("n2"));
            mesazhi = mesazhi.Replace("[dataPageses]", pagesa.DtPagese.ToString("dd.MM.yyyy"));
            mesazhi = mesazhi.Replace("[oraPageses]", pagesa.DtPagese.ToString("HH:mm"));
            mesazhi = mesazhi.Replace("[nrTransaksioni]", pagesa.NrSerial);
            mesazhi = mesazhi.Replace("[totali]", pagesa.MerrTotalTePaguarPerSMS().ToString("n2"));
            mesazhi = mesazhi.Replace("[komision]", pagesa.Komisioni.ToString("n2"));
            return mesazhi;
        }
        /// <summary>
        /// Merr nga db template e sms sipas llojit te statusit
        /// </summary>
        /// <param name="pagesa"></param>
        /// <returns></returns>
        public static string MerrSmsTemplate(StatusOTC statusi)
        {
            var mesazhi = "";
            switch (statusi)
            {
                case StatusOTC.Completed:
                    mesazhi = clsServerConfiguration.LexoKonfigurimSipasKey<string>(ServerKonfigKey.MPESA_MESAZH_SUKSESI);
                    break;
                case StatusOTC.Fail:
                    mesazhi = clsServerConfiguration.LexoKonfigurimSipasKey<string>(ServerKonfigKey.MPESA_MESAZH_DESHTIMI);
                    break;
                default:
                    throw new MyException($"Per statusin {statusi.ToString()} nuk ekziston mesazh i konfiguruar ne db");
            }
            if (string.IsNullOrEmpty(mesazhi))
                throw new MyException($"Nuk ekziston mesazhi i {statusi.ToString()} konfiguruar ne db!");
            return mesazhi;
        }

        public static string ShtoNeMesazhTarifenESherbimit(string mesazhi)
        {
            var stringuParaSherbimit = "kamate: [kamate],";
            var indexOfKamate = mesazhi.IndexOf(stringuParaSherbimit);
            var indexiKuDoShtohetSherbimi = indexOfKamate + stringuParaSherbimit.Length;
            return mesazhi.Insert(indexiKuDoShtohetSherbimi, " komision: [komision],");
        }

        public static string ModifikoMesazhPerAktMarreveshje(string mesazhi)
        {
            mesazhi = mesazhi.Replace("Fatura", "Kesti per Akt-Marreveshjen me");
            mesazhi = mesazhi.Replace(" principal: [principal], kamate: [kamate],", "");
            mesazhi = mesazhi.Replace(":", "");

            return mesazhi;
        }



        public static string KrijoMesazhPerKodin(string kodiFitues, LlojPromocionMsisdn lloji)
        {
            string mesazh = "";
            if (lloji == LlojPromocionMsisdn.Golden)
                mesazh = clsServerConfiguration.LexoKonfigurimSipasKey<string>(ServerKonfigKey.GOLDEN_MESAZH_FITUES);
            else
                mesazh = clsServerConfiguration.LexoKonfigurimSipasKey<string>(ServerKonfigKey.PLUS_MESAZH_FITUES);
            return mesazh.Replace("[kodi]", kodiFitues);
        }

        public static clsMesazh DergoSms(string url, string nrKontakti, string mesazhi)
        {
            var urlToSend = NdertoUrl(url, nrKontakti, mesazhi);
            try
            {
                ImbLogger.LogInfoPromocione($"Po tentohet te dergohet nje sms ('{mesazhi}') ne numerin {nrKontakti} nepermjet url {urlToSend}");
                var pergjigja = clsSocket.DergoKerkese(urlToSend);
                ImbLogger.LogInfoPromocione($"Mesazhi u dergua me sukses ne numrin {nrKontakti}", pergjigja);
                return new clsMesazh(true, "Mesazhi u dergua me sukses!");
            }
            catch (Exception ex)
            {
                ImbLogger.LogInfoPromocione($"Deshtoi dergimi me sms tek URL => {urlToSend}", ex);
                return new clsMesazh(false, $"Deshtoi dergimi i mesazhit per klientin {nrKontakti}");
            }
        }

        public static string NdertoUrl(string url, string nrKontakti, string mesazhi)
        {
            return url.Replace("[NUMBER]", nrKontakti).Replace("[MESSAGE]", mesazhi.Encode());
        }
    }
}
