using DbCore;
using DbCore.DbAdmin;
using DbCore.DbOTC;
using DbCore.DbRegjistrim;
using DbCore.DbShare;
using DbCore.IMBUtils.Logging;
using DbCore.Integrime;
using DbCore.Integrime.OTC;
using Newtonsoft.Json;
using RestApi.WebAPI.Models;
//using SautinSoft;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.SessionState;

namespace RestApi.Models
{
    public class OTCRepository
    {
   
        public static clsMesazh DergoKodMeSms(string msisdn)
        {
            clsKlientMeMSISDN klient = new clsKlientMeMSISDN(msisdn);
            var mesazh = klient.Valido();

            if (!mesazh)
                return mesazh;

            if (KonfigurimeStatikeIntegrimi.FakeResponse)
                return new clsMesazh(true, "SMS u dergua me sukses!");

            var mesazhi = SMSHelper.KrijoMesazhPerKodin(klient.KodiFitues, klient.Lloji);
            try
            {
                return SMSHelper.DergoSms(clsServerConfiguration.LexoKonfigurimSipasKey<string>(ServerKonfigKey.SMS_SENDER_CONFIG), msisdn, mesazhi);
            } catch (Exception ex)
            {
                ImbLogger.LogErrorOTC($"Deshtoi dergimi i sms {ex.ToString()}");
                return new clsMesazh(false, "Dergimi mesazhit deshtoi!");
            }
        }

        public static object[] ValidoMSISDN(string msisdn, string kodiFitues, int lloji)
        {
            object[] result = new object[2];
            DbCore.DbRegjistrim.LlojPromocionMsisdn llojMsisdn;
            if (lloji == 1)
                llojMsisdn = DbCore.DbRegjistrim.LlojPromocionMsisdn.Golden;
            else if (lloji == 2)
                llojMsisdn = DbCore.DbRegjistrim.LlojPromocionMsisdn.Normal;
            else
                llojMsisdn = DbCore.DbRegjistrim.LlojPromocionMsisdn.Undefined;

            clsKlientMeMSISDN kl = new clsKlientMeMSISDN()
            {
                Msisdn = msisdn,
                KodiFitues = kodiFitues,
                Lloji = llojMsisdn
            };
            clsMesazh mesazh = kl.ValidoMsisdn();
            result[0] = mesazh.Status;
            result[1] = mesazh.PershkrimMesazhi;

            return result;
        }
    }
}