using System;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;

namespace DbCore.Integrime
{
    /// <summary>
    /// Sherben per te thirrur webservice me envelope
    /// </summary>
    public class clsWebRequest : WebRequest
    {
        private string _url;
        private string _action;
        private string _userAgent;
        private static string _soapEnvelope = @"<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/'
xmlns:inp='http://www.vodafone.al/common/services/Inputs'>
<soapenv:Header/>
<soapenv:Body>
<inp:RequestObject identifier='?'>
</inp:RequestObject>
</soapenv:Body>
</soapenv:Envelope>";

        /// <summary>
        /// merr si paramter url dhe action-in
        /// </summary>
        /// <param name="url"></param>
        /// <param name="action"></param>
        public clsWebRequest(string url, string action)
        {
            _url = url;
            _action = action;
            _userAgent = "AlphaWeb";
        }

        /// <summary>
        /// therret komanden per aktivizimin e bundle
        /// </summary>
        /// <param name="msisdn"></param>
        /// <param name="PTP_ID"></param>
        /// <param name="bundleCost"></param>
        /// <param name="reason"></param>
        /// <returns></returns>
        public clsMesazh SubscribeBundle(string msisdn, string PTP_ID, string bundleCost, string reason, int idPerdoruesi)
        {
            clsMesazh mesazhi = new clsMesazh(false);
            try
            {
                NLog.LogManager.GetCurrentClassLogger().Warn($"Po dergohet komanda per aktivizimin e bundle-it me kodin:{PTP_ID} per msisdn :{msisdn} nga perdoruesi :{idPerdoruesi}");

                XmlDocument soapEnvelopeXml = CreateSoapEnvelope(CreateContentSubsribeBundle(msisdn, PTP_ID, bundleCost, reason));
                HttpWebRequest webRequest = CreateWebRequest();
                InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);
                IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);
                asyncResult.AsyncWaitHandle.WaitOne();
                string soapResult = string.Empty;
                using (WebResponse webResponse = webRequest.EndGetResponse(asyncResult))
                using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                {
                    soapResult = rd.ReadToEnd();
                }

                XmlDocument document = new XmlDocument();
                document.LoadXml(soapResult);  //loading soap message as string 
                XmlNamespaceManager manager = new XmlNamespaceManager(document.NameTable);
                manager.AddNamespace("out", "http://www.vodafone.al/common/services/Outputs");

                XmlNodeList xnList = document.SelectNodes("//out:ResponseObject", manager);
                string responseCode = "", responseDescription = "", responseDetails = "";

                foreach (XmlNode xn in xnList)
                {
                    responseCode = xn["out:ResponseCode"].InnerText;
                    responseDescription = xn["out:ResponseDescription"].InnerText;
                    responseDetails = xn["out:ResponseDetails"].InnerText;
                }
                if (responseDescription == "PASS")
                {
                    mesazhi.PershkrimMesazhi = responseDetails;
                    mesazhi.Status = true;
                    mesazhi.KodMesazhi = 1;
                }
                else if (responseDescription == "FAIL")
                {

                    mesazhi.PershkrimMesazhi = responseDetails;
                    mesazhi.Status = true;
                    mesazhi.KodMesazhi = 0;//info
                }
                else
                {
                    mesazhi.PershkrimMesazhi = "Pergjigje e panjohur!";
                    mesazhi.Status = false;
                    mesazhi.KodMesazhi = -1;
                }
                NLog.LogManager.GetCurrentClassLogger().Warn($"komanda per aktivizimin e bundle-it me kodin:{PTP_ID} per msisdn :{msisdn} nga perdoruesi :{idPerdoruesi} u dergua me sukses!");
                NLog.LogManager.GetCurrentClassLogger().Warn(string.Format("komanda per aktivizimin e bundle-it me kodin:{0} per msisdn :{1} nga perdoruesi :{2} ktheu pergjigjen !", PTP_ID, msisdn, idPerdoruesi, responseDescription));
            }
            catch (Exception ex)
            {
                mesazhi.Status = false;
                mesazhi.PershkrimMesazhi = ex.Message;
                mesazhi.KodMesazhi = -1;
                NLog.LogManager.GetCurrentClassLogger().Warn($"Ndodhi nje gabim gjate dergimit te komandes per  aktivizimin e bundle-it me kodin:{PTP_ID} per msisdn :{msisdn} mesazhi: {ex.Message}");
            }
            return mesazhi;
        }

        /// <summary>
        /// therret komanden per aktivizimin e bundle
        /// </summary>
        /// <param name="msisdn"></param>
        /// <param name="PTP_ID"></param>
        /// <param name="bundleCost"></param>
        /// <param name="reason"></param>
        /// <param name="idPerdoruesi"></param>
        /// <returns></returns>
        public clsMesazh RemoveBundle(string msisdn, string PTP_ID, string bundleCost, string reason, int idPerdoruesi)
        {
            clsMesazh mesazhi = new clsMesazh(false);
            try
            {
                XmlDocument soapEnvelopeXml = CreateSoapEnvelope(CreateContentRemoveBundle(msisdn, PTP_ID, bundleCost, reason));
                HttpWebRequest webRequest = CreateWebRequest();
                InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);
                IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);
                asyncResult.AsyncWaitHandle.WaitOne();
                string soapResult = string.Empty;
                using (WebResponse webResponse = webRequest.EndGetResponse(asyncResult))
                using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                {
                    soapResult = rd.ReadToEnd();
                }
                XmlDocument document = new XmlDocument();
                document.LoadXml(soapResult);  //loading soap message as string 
                XmlNamespaceManager manager = new XmlNamespaceManager(document.NameTable);
                manager.AddNamespace("out", "http://www.vodafone.al/common/services/Outputs");

                XmlNodeList xnList = document.SelectNodes("//out:ResponseObject", manager);
                dynamic answer = new System.Dynamic.ExpandoObject();
                foreach (XmlNode xn in xnList)
                {
                    answer.responseCode = xn["out:ResponseCode"].InnerText;
                    answer.responseDecsription = xn["out:ResponseDescription"].InnerText;
                    answer.responseDetails = xn["out:ResponseDetails"].InnerText;
                }
                if (answer.ResponseDescription == "PASS")
                {
                    mesazhi.PershkrimMesazhi = answer.ResponseDetails;
                    mesazhi.Status = true;
                    mesazhi.KodMesazhi = 1;//suksess
                }
                else if (answer.ResponseDescription == "FAIL")
                {
                    mesazhi.PershkrimMesazhi = answer.ResponseDetails;
                    mesazhi.Status = false;
                    mesazhi.KodMesazhi = 0;//info
                }
                else
                {
                    mesazhi.PershkrimMesazhi = "Pergjigje e panjohur!";
                    mesazhi.Status = false;
                    mesazhi.KodMesazhi = -1;//error
                }
            }
            catch (Exception ex)
            {
                mesazhi.Status = false;
                mesazhi.PershkrimMesazhi = ex.Message;
                mesazhi.KodMesazhi = -1;//error
            }
            return mesazhi;
        }

        /// <summary>
        /// shembull thirrje
        /// </summary>
        /// <returns></returns>
        private string CreateContentSample()
        {
            return @"<inp:OperationServices>SubscribeBundle</inp:OperationServices>
<inp:MSISDN>355692223906</inp:MSISDN>
<inp:PTP_ID>500MBMonthly</inp:PTP_ID>
<inp:BUNDLE_COST>0</inp:BUNDLE_COST>
<inp:REASON>8888</inp:REASON>";
        }

        private string CreateContentSubsribeBundle(string msisdn, string PTP_ID, string bundleCost, string reason)
        {
            return CreateCommand("SubscribeBundle", msisdn, PTP_ID, bundleCost, reason);
        }

        private string CreateContentRemoveBundle(string msisdn, string PTP_ID, string bundleCost, string reason)
        {
            return CreateCommand("RemoveBundle", msisdn, PTP_ID, bundleCost, reason);
        }

        private string CreateCommand(string command, string msisdn, string PTP_ID, string bundleCost, string reason)
        {
            return $@"<inp:OperationServices>{command}</inp:OperationServices>
<inp:MSISDN>{msisdn}</inp:MSISDN>
<inp:PTP_ID>{PTP_ID}</inp:PTP_ID>
<inp:BUNDLE_COST>{bundleCost}</inp:BUNDLE_COST>
<inp:REASON>{reason}</inp:REASON>";
        }

        private XmlDocument CreateSoapEnvelope(string content)
        {
            StringBuilder sb = new StringBuilder(_soapEnvelope);
            sb.Insert(sb.ToString().IndexOf("</inp:RequestObject>"), content);

            // create an empty soap envelope
            XmlDocument soapEnvelopeXml = new XmlDocument();
            soapEnvelopeXml.LoadXml(sb.ToString());

            return soapEnvelopeXml;
        }

        private HttpWebRequest CreateWebRequest()
        {
            HttpWebRequest webRequest = (HttpWebRequest)Create(_url);
            webRequest.Headers.Add("SOAPAction", _action);
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            webRequest.UserAgent = _userAgent;

            return webRequest;
        }

        private void InsertSoapEnvelopeIntoWebRequest(XmlDocument soapEnvelopeXml, HttpWebRequest webRequest)
        {
            using (Stream stream = webRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }
        }
    }
}