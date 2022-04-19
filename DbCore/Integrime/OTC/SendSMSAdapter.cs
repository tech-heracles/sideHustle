using DbCore.DbAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DbCore.SendSMSProxy;
using Newtonsoft.Json.Linq;
using DbCore.IMBUtils.Logging;

namespace DbCore.Integrime.OTC
{
    public class SendSMSAdapter : IDisposable
    {

        private string _password;
        private string _username;
        private TimeSpan _timeout;
        private string _originator;
        private string _mesazhiDefaultPagesa;
        private JObject konfigurime;
        public string Username
        {
            get
            {
                if (string.IsNullOrEmpty(_username)) _username = konfigurime["username"].ToString();
                return _username;
            }
        }
        public string Password
        {
            get
            {
                if (string.IsNullOrEmpty(_password)) _password = konfigurime["password"].ToString();
                return _password;
            }
        }

        public string Originator
        {
            get
            {
                if (string.IsNullOrEmpty(_originator)) _originator = konfigurime["originator"].ToString();
                return _originator;
            }
        }
        public TimeSpan Timeout
        {
            get
            {
                if (_timeout == TimeSpan.Zero) _timeout = TimeSpan.FromSeconds(double.Parse(konfigurime["timeout"].ToString()));
                return _timeout;
            }
        }

        public string MesazhiDefaultPagesa
        {
            get
            {
               // if (string.IsNullOrEmpty(_mesazhiDefaultPagesa)) _mesazhiDefaultPagesa = konfigurime["mesazhi"].ToString();
                return _mesazhiDefaultPagesa;
            }


        }

        SendSMSProxy.VFALSendSMSGateWayPortTypeClient sendSmsClient;
        public SendSMSAdapter()
        {
            var konfigStringDefault = clsServerConfiguration.LexoKonfigurimSipasKey<string>(ServerKonfigKey.OTC_SMS_KONFIG);
            konfigurime = JObject.Parse(konfigStringDefault);
            sendSmsClient = new VFALSendSMSGateWayPortTypeClient("VFALSendSMSGateWayHttpSoap12Endpoint");
            sendSmsClient.Open();
            sendSmsClient.InnerChannel.OperationTimeout = Timeout;

        }


        public clsMesazh SendSms(string nrKontakti,string mesazhi)
        {
            try
            {
                ImbLogger.LogInfoOTC($"Po tentohet te dergohet nje sms ('{mesazhi}') ne numerin {nrKontakti}");
                var pergjigja = sendSmsClient.SendSMS(Username, Password, Originator, nrKontakti, mesazhi);
                ImbLogger.LogOTC($"Mesazhi u dergua me sukses ne numrin {nrKontakti}", pergjigja);
                return new clsMesazh(true, "Mesazhi u dergua me sukses!");
            }
            catch (Exception ex)
            {
                ImbLogger.LogOTC("Deshtoi dergimi me sms =>", new { nrKontakti, mesazhi, Username, Password }, ex);
                return new clsMesazh(false, $"Deshtoi dergimi i mesazhit per klientin {nrKontakti}");
            }
        }

        public void Dispose()
        {
            sendSmsClient.Close();
        }

    }
}
