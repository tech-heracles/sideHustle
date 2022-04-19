using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Net;
using System.Web.Configuration;
using System.Web.Services.Protocols;
using DbCore.BRMAdapterServices;
using DbCore.DbAdmin;
using DbCore.DbArkaBanka;
using DbCore.IMBUtils.Logging;
using Newtonsoft.Json;
using NLog;
using System.Linq;
using System.Xml;

namespace DbCore.Integrime
{
    /// <summary>
    /// klasa wrapper
    /// </summary>
    public class BrmAdapter
    {

        #region FUSHA
        private const string Poid = "0.0.0.1 /account -1 0";
        private const string Poid2 = "0.0.0.1 /account 1662833347 0";
        private const string TransId = "PD";
        private const string PayType = "10011";

        private const string xmlPerMarrjenEFaturave = "<soap:Envelope xmlns:soap=\"http://www.w3.org/2003/05/soap-envelope\" xmlns:web=\"http://webservices.brmadapter.vodafone.al\" xmlns:xsd=\"http://businessopcodes.schemas.brm.xmlns.oracle.com/xsd\"><soap:Header /><soap:Body><web:VF_OP_BAL_RETRIEVE_BAL_PROXY><web:VF_OP_BAL_RETRIEVE_BAL>#ACCOUNT_MSISDN<xsd:PINFLDLASTNAME>#EMERKLIENTI</xsd:PINFLDLASTNAME><xsd:POID>#POID</xsd:POID></web:VF_OP_BAL_RETRIEVE_BAL></web:VF_OP_BAL_RETRIEVE_BAL_PROXY></soap:Body></soap:Envelope>";
        private const bool RunMode = true;
        private string _url;
        private string _username;
        private string _password;
        private BRMAdapterServicesSoap12BindingQSService _brmAdapter;
        #endregion FUSHA

        #region PROPERTY

        public string Url
        {
            get
            {
                if (string.IsNullOrEmpty(_url))
                {
                    _url = WebConfigurationManager.AppSettings["brmUrl"];
                }
                return _url;
            }

            set
            {
                _url = value;
            }
        }

        public string Username
        {
            get
            {
                if (string.IsNullOrEmpty(_username))
                    _username = WebConfigurationManager.AppSettings["brmUsername"];
                return _username;
            }

            set
            {
                _username = value;
            }
        }

        public string Password
        {
            get
            {
                if (string.IsNullOrEmpty(_password))
                    _password = WebConfigurationManager.AppSettings["brmPassword"];
                return _password;
            }

            set
            {
                _password = value;
            }
        }

        #endregion PROPERTY

        #region KONSTRUKTORET

        public BrmAdapter()
        {
            InicializoBrmAdapter();
        }

        public BrmAdapter(string url, string userName, string password)
        {
            _url = url;
            _username = userName;
            _password = password;
            InicializoBrmAdapter();
        }

        #endregion KONSTRUKTORET

        #region METODA PUBLIKE

        /// <summary>
        /// therret ws qe kthen te gjitha faturat e pa paguara nga BRM
        /// </summary>
        /// <param name="poid"></param>
        /// <param name="msisdn"></param>
        /// <param name="pinfldms"></param>
        /// <param name="nrLlogarie"></param>
        /// <param name="billNumber"></param>
        /// <returns></returns>
        public (clsMesazh, Tuple<string, string, string, string, string, DataTable>, string) MerrBalancenBashkeMeFaturatNgaBrm(string msisdn, string njePjeseEmri, string nrLlogarie, string billNumber, int idNdermarrje)
        {
            clsMesazh mesazhi = new clsMesazh(false);

            int idMonedha = clsMonedha.ktheIdMonedhenENdermarrjes(idNdermarrje);
            var kerkesa = new VF_OP_BAL_RETRIEVE_BAL_RQST
            {
                POID = Poid,
                MSISDN = string.IsNullOrWhiteSpace(msisdn) ? "" : msisdn.Trim(),
                PINFLDLASTNAME = string.IsNullOrWhiteSpace(njePjeseEmri) ? "" : njePjeseEmri.ToUpperInvariant(),
                ACCOUNTNO = string.IsNullOrWhiteSpace(nrLlogarie) ? "" : nrLlogarie.Trim()
            };


            VF_OP_BAL_RETRIEVE_BAL_RPLY pergjigja;
            string accountNo = string.Empty;
            try
            {
                ImbLogger.LogInfoBrm($"Po dergohet nje kerkese per te marr faturat nga brm  kerkesa {JsonConvert.SerializeObject(kerkesa)}");
                (pergjigja, accountNo) = KonfigurimeStatikeIntegrimi.FakeResponse ? MerrPergjigjeFaturashShembull(njePjeseEmri, nrLlogarie) : MerrFaturat(kerkesa);

                pergjigja.RESULTS.BILLS = pergjigja.RESULTS.BILLS.OrderByDescending(bill => bill.BILLINFO_STATUS).ThenBy(bill => bill.ENDT).ToArray();

                if (pergjigja.ERRORDESCR != "Success")
                {
                    mesazhi.Status = false;
                    mesazhi.PershkrimMesazhi = "Te dhenat nuk jane te sakta!";
                    ImbLogger.LogInfoBrm(mesazhi.PershkrimMesazhi);
                    return (mesazhi, null, accountNo);
                }
                ImbLogger.LogInfoBrm("Pergjigja per kerkesen {0} erdhi!pergjigja :{1}", JsonConvert.SerializeObject(
                    kerkesa), JsonConvert.SerializeObject(pergjigja));
            }
            catch (Exception ex)
            {
                mesazhi.Status = false;
                mesazhi.PershkrimMesazhi = "Validimi nuk mund te kryhet sepse BRM nuk mund te kontaktohet :(";
                ImbLogger.LogErrorBrm("nuk mund te kontakohet  brm kerkesa {0} error{1}", JsonConvert.SerializeObject(kerkesa), ex.Message);
                return (mesazhi, null, accountNo);
            }
            var tupleFaturat = KrijoDataTablePerFaturat(pergjigja, idMonedha);

            if (tupleFaturat.Item1.Status)
            {  //u krijua dt me faturat me sukses
                var mbipagesa = Math.Abs(decimal.Parse(pergjigja.SUSPENDEDAMOUNT ?? "0"));
                var tupleVlera = new Tuple<string, string, string, string, string, DataTable>(msisdn, njePjeseEmri, nrLlogarie, mbipagesa.ToString(), pergjigja.RESULTS.VFAFLDCURRENTTOTAL, tupleFaturat.Item2);
                mesazhi.Status = true;
                mesazhi.PershkrimMesazhi = "Validimi i klientit u krye me sukses!";
                return (mesazhi, tupleVlera, accountNo);
            }
            else
            {
                //pati problem ne krijimin e dt te faturave
                return (mesazhi, null, accountNo);
            }
        }


        #region SKEDULERI 

        public List<clsMesazh> DergoArketimeBrm()
        {
            List<clsMesazh> pergjigjet = new List<clsMesazh>();

            DataTable arketimetPerDergim = MerrArketimeBrm(false);
            ImbLogger.LogInfoBrm("Arketimet per tu derguar ne kete thirrje te ws nga skeduleri  jane {0} ne total!", arketimetPerDergim.Rows.Count);

            foreach (DataRow arketimi in arketimetPerDergim.Rows)
            {
                VF_OP_PYMT_APPLY_ERP_PAYMENT_RQST kerkesa;
                try
                {
                    bool inProgressAlready = UpdateStatusInProgressArketime(arketimi["ID"].ToString());
                    if (inProgressAlready)
                        continue;
                    if (string.IsNullOrEmpty(arketimi["KODIBANKA"].ToString()))
                        throw new MyException("Kodi i bankes eshte bosh!");
                    var kodiBankes = arketimi["KODIBANKA"].ToString();
                    string kodiFatures = arketimi["KOD_FATURE"] == null ? "" : arketimi["KOD_FATURE"].ToString();
                    kerkesa = new VF_OP_PYMT_APPLY_ERP_PAYMENT_RQST
                    {
                        TRANSID = arketimi["TransType"].ToString(),
                        POID = Poid,
                        PAYTYPE = PayType,
                        MSISDN = arketimi["MSISDN"] == null || !string.IsNullOrWhiteSpace(kodiFatures) ? "" : arketimi["MSISDN"].ToString(),
                        EFFECTIVET = DateTime.Parse(arketimi["DATE_ARKETIMI"].ToString()).ToString("yyyy-MM-ddTHH:mm:ssZ"),
                        DESCR = arketimi["SHENIMET"] == null ? "" : arketimi["SHENIMET"].ToString(),
                        BILLNO = arketimi["KOD_FATURE"] == null ? "" : arketimi["KOD_FATURE"].ToString(),
                        AMOUNT = arketimi["VLERE_PAGESE_PER_FATURE"].ToString(),
                        ACCOUNTNO = arketimi["NR_LLOGARIE"] == null || !string.IsNullOrWhiteSpace(kodiFatures) ? "" : arketimi["NR_LLOGARIE"].ToString()
                    };
                    if (kodiBankes.StartsWith("EXP"))
                        kerkesa.BANKCODE = kodiBankes;
                    else if (kodiBankes.StartsWith("OWN"))
                        kerkesa.BANKCODE = kodiBankes.Substring(3);//heqim own per te marre kodin e arkes se own ne brm
                    else
                        kerkesa.BANKCODE = $"D{kodiBankes}";

                }
                catch (Exception e)
                {
                    var message = new clsMesazh(false, $"Gabim gjate krijmit te kerkeses per arketimin me ID {arketimi["ID"]}");
                    ImbLogger.LogErrorBrm(e, message.PershkrimMesazhi);
                    pergjigjet.Add(message);
                    return pergjigjet;
                }
                //kerkesa u krijua me sukses

                VF_OP_PYMT_APPLY_ERP_PAYMENT_RPLY pergjigja;
                try
                {
                    ImbLogger.LogInfoBrm("Filloi dergimi i pageses  =>kerkesa : {0}", JsonConvert.SerializeObject(kerkesa));
                    pergjigja = _brmAdapter.VF_OP_PYMT_APPLY_ERP_PAYMENT_PROXY(kerkesa);
                    ImbLogger.LogInfoBrm("dergim pagese  =>kerkesa : {0} pergjigja : {1} ", JsonConvert.SerializeObject(kerkesa), JsonConvert.SerializeObject(pergjigja));
                    pergjigjet.Add(UpdateRreshtinBrmSipasPergjigjes(arketimi, pergjigja));
                }
                catch (Exception e)
                {
                    var message = new clsMesazh(false, $"per kete kerkese nuk erdhi pergjigje => kerkesa {JsonConvert.SerializeObject(kerkesa)}  error :{e.Message}!");
                    UpdateStatusErrorArketime(arketimi["ID"].ToString(), ((int)StatuseNgaBrm.GeneralException).ToString(), e.Message);
                    ImbLogger.LogErrorBrm(message.PershkrimMesazhi);
                    pergjigjet.Add(message);
                    return pergjigjet;
                }


            }
            return pergjigjet;
        }



        public List<clsMesazh> AnulloArketimeBrm()
        {

            //GTODO bej skriptet ne db
            VF_OP_PYMT_REVERSE_PAYMENT_RQST kerkesa;
            VF_OP_PYMT_REVERSE_PAYMENT_RPLY pergjigja;

            List<clsMesazh> pergjigjet = new List<clsMesazh>();

            DataTable arketimetPerDergim = MerrArketimeBrm(true);
            ImbLogger.LogInfoBrm("Arketimet per tu anulluar jane {0}!", arketimetPerDergim.Rows.Count);

            foreach (DataRow arketimi in arketimetPerDergim.Rows)
            {
                try
                {
                    kerkesa = new VF_OP_PYMT_REVERSE_PAYMENT_RQST
                    {
                        TRANSID = arketimi["TRANSID"].ToString(),
                        POID = Poid
                    };
                }
                catch (Exception e)
                {
                    var mesazh = new clsMesazh(false, $"Gabim gjate krijimit te kerkeses per anullim transID {arketimi["TRANSID"]} ");
                    ImbLogger.LogErrorBrm(e, mesazh.PershkrimMesazhi);
                    pergjigjet.Add(mesazh);
                    return pergjigjet;
                }

                try
                {
                    pergjigja = _brmAdapter.VF_OP_PYMT_REVERSE_PAYMENT_PROXY(kerkesa);
                    ImbLogger.LogInfoBrm("anullim pagese =>kerkesa : {0} pergjigja : {1} ", JsonConvert.SerializeObject(kerkesa), JsonConvert.SerializeObject(pergjigja));
                }
                catch (Exception e)
                {
                    //ndodhi nje problem ne dergimin e anullimit
                    UpdateStatusErrorArketime(arketimi["ID"].ToString(), ((int)StatuseNgaBrm.GeneralException).ToString(), e.Message);
                    var mesazh = new clsMesazh(false, $"per kerkesen (anullim) nuk erdhi pergjigje => kerkesa:{JsonConvert.SerializeObject(kerkesa)} error :{e.Message} ");
                    ImbLogger.LogErrorBrm(e, mesazh.PershkrimMesazhi);
                    pergjigjet.Add(mesazh);
                    return pergjigjet;
                }

                pergjigjet.Add(new clsMesazh(pergjigja.STATUSSTR == "0", pergjigja.ERRORDESCR));
                ImbLogger.LogInfoBrm(pergjigja.ERRORDESCR);


                try
                {

                    if (pergjigja.STATUSSTR == "0" && UpdateArketimDerguar(arketimi["ID"].ToString(), kerkesa.TRANSID, true, pergjigja.STATUSSTR, pergjigja.ERRORDESCR))
                    {
                        var mesazh = new clsMesazh(true, $"update  i dokument te arkes me transid {pergjigja.TRANSID} u be me sukses!");
                        pergjigjet.Add(mesazh);
                        ImbLogger.LogInfoBrm(mesazh.PershkrimMesazhi);
                    }
                    else
                    {
                        UpdateStatusErrorArketime(arketimi["ID"].ToString(), pergjigja.STATUSSTR, pergjigja.ERRORDESCR);
                    }
                }
                catch (Exception ex)
                {

                    pergjigjet.Add(new clsMesazh(false, $"Ndodhi nje gabim gjate update te statusit te dokumentit {pergjigja.TRANSID}!"));
                    ImbLogger.LogErrorBrm(ex, "Ndodhi nje gabim gjate update te statusit te dokumentit {0}!", pergjigja.TRANSID);

                }
            }
            return pergjigjet;
        }

        #endregion 


        /// <summary>
        /// verifikon klientinkur jepet msisdn
        /// </summary>
        /// <param name="msisdn"></param>
        /// <param name="poid"></param>
        /// <returns></returns>
        public Tuple<PCM_OP_VF_BALANCE_INQUIRY_RPLY, clsMesazh> MerrTeDhenaTePlotaKlienti(string msisdn)
        {
            try
            {
                ImbLogger.LogInfoBrm("Po dergohet nje kerkese per te validuar klientin me te dhenat : msisdn {0}  ", msisdn);
                var rqst = new PCM_OP_VF_BALANCE_INQUIRY_RQST
                {
                    MSISDN = msisdn,
                    POID = Poid

                };

                var response = _brmAdapter.VF_OP_BALANCE_INQUIRY_RPLY_PROXY(rqst);
                ImbLogger.LogInfoBrm("Pergjigja e kthyer per komanden e validimit  : {0} ", response);

                return new Tuple<PCM_OP_VF_BALANCE_INQUIRY_RPLY, clsMesazh>(response, new clsMesazh(true, "Nuk ndodhi error"));
            }
            catch (Exception ex)
            {
                ImbLogger.LogErrorBrm(ex.Message, "Ndodhi nje gabim gjate validimit te klientit : msisdn {0}  ", msisdn);
                return new Tuple<PCM_OP_VF_BALANCE_INQUIRY_RPLY, clsMesazh>(null, new clsMesazh(false, ex.Message));
            }
        }


        #endregion METODA PUBLIKE

        #region METODA PRIVATE

        private void InicializoBrmAdapter()
        {

            _brmAdapter = new BRMAdapterServicesSoap12BindingQSService
            {
                Credentials = new NetworkCredential(Username, Password),
                Url = Url,
                PreAuthenticate = true,
                SoapVersion = SoapProtocolVersion.Soap12
            };
        }

        /// <summary>
        /// krijon nje datatable per griden e faturava duke u bazuar tek pergjigja qe vjen nga BRM
        /// </summary>
        /// <param name="bills"></param>
        /// <param name="idMonedhe"></param>
        /// <returns></returns>
        private static Tuple<clsMesazh, DataTable> KrijoDataTablePerFaturat(VF_OP_BAL_RETRIEVE_BAL_RPLY pergjigja, int idMonedhe)
        {
            clsMesazh mesazhi = new clsMesazh(true);
            DataTable dt;
            try
            {
                dt = KrijoDataTableBoshPerArkaBanka();
                var i = 0;
                //nese ka fatura mbush dt me faturat ,nje rresht per cdo fature
                if (pergjigja.RESULTS.BILLS != null)
                {
                    //gjithe faturat e nje viti
                    foreach (var fatura in pergjigja.RESULTS.BILLS)
                    {
                        DataRow dr = dt.NewRow();
                        DateTime formatedDate = default(DateTime);
                        try
                        {
                            if (!DateTime.TryParse(fatura.ENDT.ToString(), out formatedDate))
                            {
                                System.Xml.XmlNode[] date = (System.Xml.XmlNode[])fatura.ENDT;
                                formatedDate = DateTime.ParseExact(date[0].Value, "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture).ToUniversalTime();

                            }
                            else
                            {
                                if (!KonfigurimeStatikeIntegrimi.FakeResponse)
                                    formatedDate = formatedDate.ToUniversalTime();
                            }
                            string status = string.IsNullOrWhiteSpace(fatura.BILLINFO_STATUS) ? "Active" : fatura.BILLINFO_STATUS;
                            var periudha = $"{clsFunksione.ktheMuaj(formatedDate.Month)} {formatedDate.Year}";
                            dr.ItemArray = new object[] { ++i, 0, 0, fatura.BILLNO, "", 0, "", periudha, idMonedhe, 1, fatura.CURRENTTOTAL, fatura.DUE, 0, DateTime.Now, DateTime.Now, 0, 0, default(DateTime), 0, 0, 0, status, "", "" };

                            dt.Rows.Add(dr);
                            mesazhi.Status = true;

                        }
                        catch (Exception ex)
                        {
                            mesazhi.Status = false;
                            mesazhi.PershkrimMesazhi = ex.Message;
                            ImbLogger.LogErrorBrm("ndodhi nje gabim ne parsimin e dates! {0}", ex.Message);
                            break;//dil nga cikli 
                        }


                    }
                }
                if (null != pergjigja.TOTALDUE && "0" != pergjigja.TOTALDUE)
                {
                    try
                    {

                        dt.Rows.Add(KrijoRreshtMeTePrapambetura(dt, pergjigja.TOTALDUE, pergjigja.TOTALDUE, idMonedhe));
                        mesazhi.Status = true;
                        ImbLogger.LogInfoBrm("u shtua rreshti bosh ne dt {0}", pergjigja.RESULTS);
                    }
                    catch (Exception ex)
                    {
                        mesazhi.Status = false;
                        mesazhi.PershkrimMesazhi = ex.Message;
                        ImbLogger.LogErrorBrm("ndodhi nje problem ne krijimin e rreshtit bosh! {0} rreshti bosh {1}", ex.Message, pergjigja);
                    }
                }

            }
            catch (Exception ex)
            {
                mesazhi.Status = false;
                mesazhi.PershkrimMesazhi = ex.Message;
                dt = null;
            }
            return new Tuple<clsMesazh, DataTable>(mesazhi, dt);
        }


        private HttpWebRequest KrijoKerkese()
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(Url);
            webRequest.Headers.Add("SOAP:Action");
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            webRequest.Timeout = 400000;
            return webRequest;

        }

        private XmlDocument MerrFaturatXML(VF_OP_BAL_RETRIEVE_BAL_RQST parameters)
        {
            HttpWebRequest webRequest = KrijoKerkese();
            XmlDocument soapEnvelopeXml = new XmlDocument();
            string xmlRequest = xmlPerMarrjenEFaturave.Replace("#POID", parameters.POID).Replace("#EMERKLIENTI", parameters.PINFLDLASTNAME);
            if (!string.IsNullOrWhiteSpace(parameters.ACCOUNTNO))
                xmlRequest = xmlRequest.Replace("#ACCOUNT_MSISDN", $"<xsd:ACCOUNTNO>{parameters.ACCOUNTNO}</xsd:ACCOUNTNO>");
            else
                xmlRequest = xmlRequest.Replace("#ACCOUNT_MSISDN", $"<xsd:MSISDN>{parameters.MSISDN}</xsd:MSISDN>");

            soapEnvelopeXml.LoadXml(xmlRequest);
            using (System.IO.Stream stream = webRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }

            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

            asyncResult.AsyncWaitHandle.WaitOne();

            string soapResult;
            using (WebResponse webResponse = webRequest.EndGetResponse(asyncResult))
            using (System.IO.StreamReader rd = new System.IO.StreamReader(webResponse.GetResponseStream()))
            {
                soapResult = rd.ReadToEnd();
            }
            ImbLogger.LogInfoBrm("Pergjigja per kerkesen {0} ne format XML erdhi!pergjigja :{1}", JsonConvert.SerializeObject(
                   parameters), soapResult);
            XmlDocument resp = new XmlDocument();

            resp.LoadXml(soapResult);
            return resp;
        }

        private (VF_OP_BAL_RETRIEVE_BAL_RPLY, string) LexoPergjigje(XmlDocument dokument)
        {
            VF_OP_BAL_RETRIEVE_BAL_RPLY reply = new VF_OP_BAL_RETRIEVE_BAL_RPLY();
            string accountNo = string.Empty;
            if (dokument == null)
                return (reply, accountNo);

            XmlNodeList poid = dokument.GetElementsByTagName("xsd:POID");
            if (poid.Count > 0)
                reply.POID = poid[poid.Count - 1].InnerText;//nese kemi disa tage result do kemi shume tage poid, ku i pari eshte poid qe dergojme ne
                                                            //te tjeret ( te cilet jane brenda tagut result kane poid e ketij accounti, ne rastin e nje tagu result kemi vetem 1 poid
            XmlNodeList accNo = dokument.GetElementsByTagName("xsd:ACCOUNTNO");
            if (accNo.Count > 0)
                accountNo = accNo[accNo.Count - 1].InnerText;
            XmlNodeList errordescr = dokument.GetElementsByTagName("xsd:ERRORDESCR");
            if (errordescr.Count == 1)
                reply.ERRORDESCR = errordescr[0].InnerText;
            double suspended = 0;

            XmlNodeList suspendedAmount = dokument.GetElementsByTagName("xsd:SUSPENDEDAMOUNT");
            foreach (XmlElement amount in suspendedAmount)
                suspended += Convert.ToDouble(amount.InnerText);
            reply.SUSPENDEDAMOUNT = suspended.ToString();

            double totalDue = 0;
            XmlNodeList totalDueNodes = dokument.GetElementsByTagName("xsd:TOTALDUE");
            foreach (XmlElement total in totalDueNodes)
                totalDue += Convert.ToDouble(total.InnerText);
            reply.TOTALDUE = totalDue.ToString();

            reply.RESULTS = new VF_OP_BAL_RETRIEVE_BAL_RPLY_RESULTS();
            double vfalCurrentTotal = 0;
            XmlNodeList vfalCurrentTotalNodes = dokument.GetElementsByTagName("xsd:VFAFLDCURRENTTOTAL");
            foreach (XmlElement currentTotalNodes in vfalCurrentTotalNodes)
                vfalCurrentTotal += Convert.ToDouble(currentTotalNodes.InnerText);
            reply.RESULTS.VFAFLDCURRENTTOTAL = vfalCurrentTotal.ToString();

            XmlNodeList bills = dokument.GetElementsByTagName("xsd:BILLS");
            reply.RESULTS.BILLS = new VF_OP_BAL_RETRIEVE_BAL_RPLY_RESULTS_BILLS[bills.Count];
            for (int i = 0; i < bills.Count; i++)
            {
                XmlNode bill = bills[i];
                reply.RESULTS.BILLS[i] = new VF_OP_BAL_RETRIEVE_BAL_RPLY_RESULTS_BILLS();
                if (!bill.HasChildNodes)
                    continue;
                foreach (XmlNode billsChild in bill.ChildNodes)
                {
                    switch (billsChild.LocalName)
                    {
                        case "BILLNO":
                            reply.RESULTS.BILLS[i].BILLNO = billsChild.InnerText;
                            break;
                        case "CURRENTTOTAL":
                            reply.RESULTS.BILLS[i].CURRENTTOTAL = billsChild.InnerText;
                            break;
                        case "DUE":
                            reply.RESULTS.BILLS[i].DUE = billsChild.InnerText;
                            break;
                        case "BILLINFO_STATUS":
                            reply.RESULTS.BILLS[i].BILLINFO_STATUS = billsChild.InnerText;
                            break;
                        case "ENDT":
                            reply.RESULTS.BILLS[i].ENDT = billsChild.InnerText;
                            break;
                    }
                }
                if (string.IsNullOrWhiteSpace(reply.RESULTS.BILLS[i].BILLINFO_STATUS))
                    reply.RESULTS.BILLS[i].BILLINFO_STATUS = "Active";
            }

            return (reply, accountNo);
        }

        private (VF_OP_BAL_RETRIEVE_BAL_RPLY,string) MerrFaturat(VF_OP_BAL_RETRIEVE_BAL_RQST parameters)
        {
            return LexoPergjigje(MerrFaturatXML(parameters));
        }

        private static DataRow KrijoRreshtMeTePrapambetura(DataTable dt, string vleraFillestare, string vleraPerTuPaguar, int idMonedhe)
        {
            DataRow dr = dt.NewRow();
            var idRreshti = dt.Rows.Count + 1;
            int nrFaturaTerminated = dt.AsEnumerable().Sum(x => x.Field<string>("StatusFature") == "Terminated" ? 1 : 0);
            dr.ItemArray = new object[] { idRreshti, 0, 0, "Permbledhese","", 0, "", "Me te vjetra se nje vit", idMonedhe, 1, vleraFillestare, vleraPerTuPaguar, 0, DateTime.Now, DateTime.Now, 0, 0, default(DateTime), 0, 0, 0, (nrFaturaTerminated == 0 || nrFaturaTerminated < dt.Rows.Count) ? "Active" : "Terminated","", ""};

            return dr;

        }

        /// <summary>
        /// update statusin derguar ose errorin qe vjen per kete rresht fature
        /// </summary>
        /// <param name="arketimi"></param>
        /// <param name="pergjigja"></param>
        /// <returns></returns>
        private static clsMesazh UpdateRreshtinBrmSipasPergjigjes(DataRow arketimi, VF_OP_PYMT_APPLY_ERP_PAYMENT_RPLY pergjigja)
        {
            clsMesazh mesazhi = new clsMesazh(false);

            if (Convert.ToInt32(StatuseNgaBrm.Sukses).ToString().Equals(pergjigja.STATUSSTR, StringComparison.InvariantCulture))
            {
                //pagesa eshte bere me sukses ne brm
                //update statusin derguar dhe vendosi idtransaksioni
                try
                {
                    UpdateArketimDerguar(arketimi["ID"].ToString(), pergjigja.TRANSID, false, pergjigja.STATUSSTR, pergjigja.ERRORDESCR);
                    mesazhi = new clsMesazh(true, $"Shkrimi i dokument te arkes me id {pergjigja.TRANSID} u be me sukses!");
                }
                catch (Exception ex)
                {
                    mesazhi = new clsMesazh(false, $"Ndodhi nje gabim gjate update te statusit te dokumentit per dergim ,transID :{pergjigja.TRANSID} Error:{ex.Message}!");
                }

            }
            else
            {
                try
                {
                    UpdateStatusErrorArketime(arketimi["ID"].ToString(), pergjigja.STATUSSTR, pergjigja.ERRORDESCR);
                    mesazhi = new clsMesazh(true, $"Shkrimi i errorit per dokumentin e   arkes me transID {pergjigja.TRANSID} u be me sukses!");
                }
                catch (Exception ex)
                {
                    mesazhi = new clsMesazh(false, $"Ndodhi nje gabim gjate update te statusit te dokumentit ,transID :{pergjigja.TRANSID} Error:{ex.Message}!");
                }
            }
            if (mesazhi.Status)
                ImbLogger.LogInfoBrm(mesazhi.PershkrimMesazhi);
            else
                ImbLogger.LogErrorBrm(mesazhi.PershkrimMesazhi);
            return mesazhi;
        }
        private static DataTable MerrArketimeBrm(bool anullim)
        {
            DataTable dt = new DataTable();
            using (clsDatabaseArkaBanka arka = new clsDatabaseArkaBanka())
            {
                dt = arka.merrTempArketimePerBRMCon(anullim);
            }
            return dt;
        }

        private static bool UpdateArketimDerguar(string id, string transId, bool anullim, string statusStr, string errorDescr)
        {
            using (clsDatabaseArkaBanka arka = new clsDatabaseArkaBanka())
            {
                return arka.updateArketimDerguarCon(id, transId, anullim, statusStr, errorDescr);
            }
        }
        private static bool UpdateStatusErrorArketime(string id, string statusStr, string errorDescr)
        {
            using (clsDatabaseArkaBanka arka = new clsDatabaseArkaBanka())
            {
                return arka.updateArketimDerguarGabim(id, statusStr, errorDescr);
            }
        }
        private static bool UpdateStatusInProgressArketime(string id)
        {
            using (clsDatabaseArkaBanka arka = new clsDatabaseArkaBanka())
            {
                return arka.updateArketimDerguarInProgress(id);
            }
        }
        public static DataTable KrijoDataTableBoshPerArkaBanka()
        {
            DataTable dt = new DataTable();

            dt.Columns.AddRange(new[] { new DataColumn("IdDokumenti"), new DataColumn("IdNiveli"), new DataColumn("IdKonfigAmbjente"), new DataColumn("NrDokumenti"), new DataColumn("NrSerial"), new DataColumn("IdKlientFurnitori"), new DataColumn("EmertimiKf"), new DataColumn("Pershkrimi"), new DataColumn("IdMonedha"), new DataColumn("Kursi"), new DataColumn("Vlefta", typeof(decimal)), new DataColumn("VleftaPaLikujduar", typeof(decimal)), new DataColumn("Status"), new DataColumn("DtDokumenti"), new DataColumn("DtMaturimi"), new DataColumn("Zbritja"), new DataColumn("IdKushtPagese"), new DataColumn("DtAzhornimi"), new DataColumn("KursAzhornimi"), new DataColumn("VleftaLikuiduar"), new DataColumn("VleftaLikuiduarMon"), new DataColumn("StatusFature"), new DataColumn("Grupimi1"), new DataColumn("KodiKlientit")});
            return dt;
        }
        //public static DataTable rollbackPayment(){}

        #endregion METODA PRIVATE


        #region TEST CASES



        private static Tuple<PCM_OP_VF_BALANCE_INQUIRY_RPLY, clsMesazh> MerrBalcancenTest()
        {

            try
            {
                var msisdn = "355692224905"; //per msisdn level
                var accountNumber = "267801253";//account level
                var lastName = "FUP";//per msisdn bashke me emer

                VF_OP_BAL_RETRIEVE_BAL_RQST rqst = new VF_OP_BAL_RETRIEVE_BAL_RQST
                {
                    MSISDN = msisdn,
                    POID = Poid,
                    PINFLDLASTNAME = lastName,
                    ACCOUNTNO = accountNumber
                };

                PCM_OP_VF_BALANCE_INQUIRY_RPLY response = new PCM_OP_VF_BALANCE_INQUIRY_RPLY
                {

                    MSISDN = "355696011312",
                    POID = Poid,
                    ACCOUNTNO = "futja kot",
                    INVOICETOTAL = 124567.ToString(),
                    INVOICECUR = "LEK"
                };


                return new Tuple<PCM_OP_VF_BALANCE_INQUIRY_RPLY, clsMesazh>(response, new clsMesazh(true, "Nuk ndodhi error"));
            }
            catch (Exception ex)
            {
                return new Tuple<PCM_OP_VF_BALANCE_INQUIRY_RPLY, clsMesazh>(null, new clsMesazh(false, ex.Message));
            }
        }
        private static (VF_OP_BAL_RETRIEVE_BAL_RPLY, string) MerrPergjigjeFaturashShembull(string emri, string accountNo)
        {

            var reply = new VF_OP_BAL_RETRIEVE_BAL_RPLY
            {
                RESULTS = new VF_OP_BAL_RETRIEVE_BAL_RPLY_RESULTS()
            };
            if (!emri.Contains("Zero"))
            {
                reply.RESULTS.BILLS = new VF_OP_BAL_RETRIEVE_BAL_RPLY_RESULTS_BILLS[4];
                reply.RESULTS.BILLS[0] = new VF_OP_BAL_RETRIEVE_BAL_RPLY_RESULTS_BILLS
                {
                    BILLNO = "4",
                    DUE = "0",
                    CURRENTTOTAL = "2000.0",
                    ENDT = new DateTime(2017, 1, 1),
                    BILLINFO_STATUS = emri.Contains("Active") ? "Active" : "Terminated"
                };
                reply.RESULTS.BILLS[1] = new VF_OP_BAL_RETRIEVE_BAL_RPLY_RESULTS_BILLS
                {
                    BILLNO = "5",
                    DUE = "125.39",
                    CURRENTTOTAL = "500.0",
                    ENDT = new DateTime(2017, 2, 1),
                    BILLINFO_STATUS = emri.Contains("Active") ? "Active" : "Terminated"
                };
                reply.RESULTS.BILLS[2] = new VF_OP_BAL_RETRIEVE_BAL_RPLY_RESULTS_BILLS
                {
                    BILLNO = "6",
                    DUE = "0",
                    CURRENTTOTAL = "600.0",
                    ENDT = new DateTime(2017, 3, 1),
                    BILLINFO_STATUS = emri.Contains("Terminated") ? "Terminated" : "Active"
                };
                reply.RESULTS.BILLS[3] = new VF_OP_BAL_RETRIEVE_BAL_RPLY_RESULTS_BILLS
                {
                    BILLNO = "7",
                    DUE = "225.39",
                    CURRENTTOTAL = "600.0",
                    ENDT = DateTime.Now,
                    BILLINFO_STATUS = emri.Contains("Terminated") ? "Terminated" : "Active"
                };
                reply.RESULTS.VFAFLDCURRENTTOTAL = "1200";
                reply.TOTALDUE = "2000.15";
            }
            else
            {
                reply.RESULTS.BILLS = new VF_OP_BAL_RETRIEVE_BAL_RPLY_RESULTS_BILLS[0];
                reply.RESULTS.VFAFLDCURRENTTOTAL = "0";
                reply.TOTALDUE = "0";
            }
            reply.ERRORDESCR = "Success";
            return (reply, string.IsNullOrWhiteSpace(accountNo) ? "AccountName" : accountNo);
        }
        #endregion




    }
}