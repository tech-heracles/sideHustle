using DbCore.PromocioneProxy;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Configuration;
using System.Xml.Linq;
using DbCore.IMBUtils.Logging;

namespace DbCore.Integrime
{
    /// <summary>
    /// klase per integrimin e promocioneve
    /// </summary>
    public class PromocioneAdapter
    {


        #region FUSHA
        const string usernameFix = "VfOneLoyality";
        private const bool RunMode = true;
        private string _url;
        private string _username;
        private string _password;
        private VFALSelfCareOperationsNewSoap12BindingQSService _selfCareOperationsAdapter;
        #endregion FUSHA

        #region PROPERTY

        public string Url
        {
            get
            {
                if (string.IsNullOrEmpty(_url))
                {
                    _url = WebConfigurationManager.AppSettings["URLSelfCare"];
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
                    _username = WebConfigurationManager.AppSettings["selfCareUsername"];
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
                    _password = WebConfigurationManager.AppSettings["PromoPassword"];
                return _password;
            }

            set
            {
                _password = value;
            }
        }

        #endregion PROPERTY

        #region KONSTRUKTORET

        public PromocioneAdapter()
        {
            InicializoPromocioneAdapter();
        }

        private void InicializoPromocioneAdapter()
        {
            _selfCareOperationsAdapter = new VFALSelfCareOperationsNewSoap12BindingQSService();
            _selfCareOperationsAdapter.Url = Url;

        }

        public PromocioneAdapter(string url, string userName, string password)
        {
            _url = url;
            _username = userName;
            _password = password;
        }
        #endregion

        #region VFONE 

        public Tuple<clsMesazh, List<string>> MerrInfoPerNumrin(string msisdn, out int pike)
        {
            try
            {
                ImbLogger.LogInfoPromocione(string.Format("Po dergohet nje kerkese per te marr informacion per numrin {0}", msisdn));
                var pergjigja = KonfigurimeStatikeIntegrimi.FakeResponse ? null : _selfCareOperationsAdapter.VFALGetLoyaltyInfo(usernameFix, msisdn);
                 ImbLogger.LogInfoPromocione(string.Format("Komunikimi u kryes me sukse,po tentohet te lexohet pergjigja per numrin {0} pergjigja : {1}", msisdn, pergjigja));
                var xmlResponse = KonfigurimeStatikeIntegrimi.FakeResponse ? merrXmlResponseInfoNumri() : ((System.Xml.XmlNode[])pergjigja)[0].InnerXml;
                ImbLogger.LogInfoPromocione(string.Format("U lexua nje pergjigje e vlefshme xml per numrin  {0} pergjigja : {1}", msisdn, xmlResponse));
                return LexoInfoPerNumrinSipasPergjigjesXMLVfOne(xmlResponse, msisdn, out pike);

            }
            catch (Exception ex)
            {
                pike = 0;
                ImbLogger.LogErrorPromocione(string.Format("Nodhi nje gabim i pergjithshem per numrin '{0}' error :'{1}'", msisdn, ex.Message));
                return new Tuple<clsMesazh, List<string>>(new clsMesazh(false, string.Format("Per numrin {0} eshte e pamundur te merret informacion ne kete moment!", msisdn)), new List<string>());
            }

        }
        public clsMesazh DergoDhuratenEZgjedhur(string msisdn, string kodArtikulli)
        {
            clsMesazh mesazhi;
            if (KonfigurimeStatikeIntegrimi.FakeResponse) return new clsMesazh(true);
            try
            {
                ImbLogger.LogInfoPromocione(string.Format("Po dergohet nje kerkese per te derguar dhuraten {0} per numrin {1}", kodArtikulli, msisdn));
                var pergjigja = _selfCareOperationsAdapter.VFALSubscribeLoyaltyBonus("", msisdn, msisdn, kodArtikulli);//
                ImbLogger.LogInfoPromocione(string.Format("Komunikimi u kryes me sukses,po tentohet te lexohet pergjigja e dergimit te dhurates per numrin {0} pergjigja : {1}", msisdn, JsonConvert.SerializeObject(pergjigja)));

                ImbLogger.LogInfoPromocione(string.Format("U lexua nje pergjigje e vlefshme xml per numrin  {0} pergjigja : {1}", msisdn, JsonConvert.SerializeObject(pergjigja)));


                if (pergjigja.commandResult.Contains("Success"))
                {
                    mesazhi = new clsMesazh(true, string.Format("Dhurata u morr me sukses per numrin {0}!", msisdn));
                    ImbLogger.LogInfoPromocione(mesazhi.PershkrimMesazhi);
                    return mesazhi;
                }
                else
                {
                    mesazhi = new clsMesazh(false, string.Format("Dhurata nuk u morr me sukses per numrin {0}", msisdn));
                    ImbLogger.LogInfoPromocione(mesazhi.PershkrimMesazhi);
                    return mesazhi;
                }
            }
            catch (Exception ex)
            {

                ImbLogger.LogErrorPromocione(string.Format("Nodhi nje gabim i pergjithshem gjate dergimit te dhurates '{0}' per numrin '{1}' error :'{2}'", kodArtikulli, msisdn, ex.Message));
                return new clsMesazh(false, string.Format("Per numrin {0} eshte e pamundur te merret informacion ne kete moment!", msisdn));
            }

        }

        public clsMesazh SubscribeBundle(string msisdn, string kodPromocioni, string cost, string discount)
        {
            clsMesazh mesazhi;
            if (KonfigurimeStatikeIntegrimi.FakeResponse)
                return new clsMesazh(true);
            try
            {
                ImbLogger.LogInfoPromocione($"Po dergohet nje kerkese per te derguar dhuraten {kodPromocioni} per numrin {msisdn}");

                var pergjigja = _selfCareOperationsAdapter.VFALSubscrbeBundle(Username, msisdn, kodPromocioni, "", "", "", "", discount, "", cost, "", "", "");

                ImbLogger.LogInfoPromocione(string.Format("Komunikimi u kryes me sukses,po tentohet te lexohet pergjigja e dergimit te dhurates per numrin {0} pergjigja : {1}", msisdn, JsonConvert.SerializeObject(pergjigja)));

                ImbLogger.LogInfoPromocione(string.Format("U lexua nje pergjigje e vlefshme xml per numrin  {0} pergjigja : {1}", msisdn, JsonConvert.SerializeObject(pergjigja)));


                if (pergjigja.commandResult.Contains("Success"))
                {
                    mesazhi = new clsMesazh(true, string.Format("Dhurata u morr me sukses per numrin {0}!", msisdn));
                    ImbLogger.LogInfoPromocione(mesazhi.PershkrimMesazhi);
                    return mesazhi;
                }
                else
                {
                    mesazhi = new clsMesazh(false, string.Format("Dhurata nuk u morr me sukses per numrin {0}", msisdn));
                    ImbLogger.LogInfoPromocione(mesazhi.PershkrimMesazhi);
                    return mesazhi;
                }
            } catch (Exception ex)
            {

                ImbLogger.LogErrorPromocione(string.Format("Nodhi nje gabim i pergjithshem gjate dergimit te dhurates '{0}' per numrin '{1}' error :'{2}'", kodPromocioni, msisdn, ex.Message));
                return new clsMesazh(false, string.Format("Per numrin {0} eshte e pamundur te merret informacion ne kete moment!", msisdn));
            }



        }

        /// <summary>
        /// lexon pergjigjen sipas specifikave te dhena per marrjen e informacionit per nje numer qe ben pjese ne VFONE
        /// </summary>
        /// <param name="answer"></param>
        /// <param name="nrAbonenti"></param>
        /// <param name="pike"></param>
        /// <returns></returns>
        private static Tuple<clsMesazh, List<string>> LexoInfoPerNumrinSipasPergjigjesXMLVfOne(string answer, string nrAbonenti, out int pike)
        {
            var mesazh = new clsMesazh();
            pike = 0;
            string pergjigjaInfo = "";
            //int piketKthyera = 0;
            List<string> dhuratat = new List<string>();
            try
            {
                XDocument xdocPiket = XDocument.Parse(answer);

                XElement badCommand = xdocPiket.Descendants("BadCommand").FirstOrDefault();
                if (badCommand == null)
                {
                    XElement commandResult = xdocPiket.Descendants("error").FirstOrDefault();
                    pergjigjaInfo = commandResult.Value;
                    if (pergjigjaInfo.Equals("0"))
                    {
                        XElement regjistruarElement = xdocPiket.Descendants("status").FirstOrDefault();
                        if (regjistruarElement.Value.ToString().Equals("Registered"))
                        {
                            XElement pointBalance = xdocPiket.Descendants("points").FirstOrDefault();
                            pike = int.Parse(pointBalance.Value.ToString());
                            mesazh = new clsMesazh(true, string.Format("Piket e grumbulluara jane {0}! per numrin {1} ", pike, nrAbonenti));
                            ImbLogger.LogInfoPromocione(mesazh.PershkrimMesazhi);

                            foreach (XElement elementDhurate in xdocPiket.Descendants("reward"))
                            {
                                if (elementDhurate.Element("rewardType").Value == "Gift Bonus")

                                    dhuratat.Add(elementDhurate.Element("rewardCode").Value);
                            }
                            return new Tuple<clsMesazh, List<string>>(mesazh, dhuratat);
                        }
                        else
                        {
                            mesazh = new clsMesazh(false, string.Format("Numri {0} nuk eshte i regjistruar ne VF ONE!", nrAbonenti));
                            ImbLogger.LogErrorPromocione(mesazh.PershkrimMesazhi);
                            return new Tuple<clsMesazh, List<string>>(mesazh, dhuratat);
                        }
                    }
                    else if (string.Compare(pergjigjaInfo, "0") > 0)
                    {
                        XElement arsyeMosDergimInfo = xdocPiket.Descendants("errorDesc").FirstOrDefault();
                        if (arsyeMosDergimInfo.Value.Contains("Subscriber is not registered"))
                        {
                            mesazh = new clsMesazh(false, string.Format("Numri {0} nuk eshte i regjistruar ne VF ONE!", nrAbonenti));
                            ImbLogger.LogErrorPromocione(mesazh.PershkrimMesazhi);
                        }
                        else
                        {
                            mesazh = new clsMesazh(false, String.Format("Marrja e pikeve nuk u krye me sukses ({0})! per numrin {1}", arsyeMosDergimInfo.Value, nrAbonenti));
                            ImbLogger.LogErrorPromocione(mesazh.PershkrimMesazhi);
                        }
                        return new Tuple<clsMesazh, List<string>>(mesazh, dhuratat);
                    }
                    else
                    {
                        mesazh = new clsMesazh(false, string.Format("Pergjigje e pa identifikuar! per numrin {0}", nrAbonenti));
                        ImbLogger.LogErrorPromocione(mesazh.PershkrimMesazhi);
                        return new Tuple<clsMesazh, List<string>>(mesazh, dhuratat);
                    }
                }
                else
                {
                    mesazh = new clsMesazh(false, String.Format("Marrja e pikeve nuk u krye me sukses ({0})!", badCommand.Value));
                    ImbLogger.LogErrorPromocione(mesazh.PershkrimMesazhi);
                    return new Tuple<clsMesazh, List<string>>(mesazh, dhuratat);
                }
            }
            catch (Exception error)
            {
                mesazh = new clsMesazh(false, string.Format("error i pergjithshem {0} per numrin {q}", error.Message, nrAbonenti));
                ImbLogger.LogInfoPromocione(mesazh.PershkrimMesazhi);
                return new Tuple<clsMesazh, List<string>>(mesazh, dhuratat);
            }
        }

        /// <summary>
        /// lexon pergjigjen qe kthehet sipas specifikave te dhena kur zgjidhet te nje dhurate per nje klient te caktuar
        /// </summary>
        /// <param name="pergjigja"></param>
        /// <param name="nrAbonenti"></param>
        /// <param name="kodArtikulli"></param>
        /// <returns></returns>
        private static clsMesazh LexoPergjigjeDergimiNgaXMLVfOne(System.Xml.XmlNode[] pergjigja, string nrAbonenti, string kodArtikulli)
        {
            clsMesazh mesazhi;
            string pergjigjaInfo = "";

            ImbLogger.LogInfoPromocione(string.Format("Pergjigja e kerkeses per marrjen e dhurates {0}", pergjigja));
            var badCommand = pergjigja.Where(x => x.Name.IndexOf("badcommand", StringComparison.InvariantCultureIgnoreCase) >= 0).FirstOrDefault();


            if (badCommand == null)
            {
                var commandResult = pergjigja.Where(x => x.Name.IndexOf("commandResult", StringComparison.InvariantCultureIgnoreCase) >= 0).FirstOrDefault();
                pergjigjaInfo = commandResult.InnerText;
                if (pergjigjaInfo.Equals("Success"))
                {


                    mesazhi = new clsMesazh(true, string.Format("Dhurata u morr me sukses per numrin {0}!", nrAbonenti));
                    ImbLogger.LogInfoPromocione(mesazhi.PershkrimMesazhi);
                    return mesazhi;
                }
                if (pergjigjaInfo.Equals("Fail"))
                {
                    var arsyeMosDergimDhurata = pergjigja.Where(x => x.Name.IndexOf("ResultMessage", StringComparison.InvariantCultureIgnoreCase) >= 0).FirstOrDefault();
                    mesazhi = new clsMesazh(false, String.Format("Marrja e dhurates  nuk u krye me sukses per numrin {0} error: ({1})!", nrAbonenti, arsyeMosDergimDhurata.InnerText));
                    ImbLogger.LogInfoPromocione(mesazhi.PershkrimMesazhi);
                    return mesazhi;
                }
                mesazhi = new clsMesazh(false, string.Format("Pergjigje e pa identifikuar per numrin {0}!", nrAbonenti));
                ImbLogger.LogErrorPromocione(mesazhi.PershkrimMesazhi);
                return mesazhi;
            }

            mesazhi = new clsMesazh(false, string.Format("Marrja e dhurates nga socket-i nuk u krye me sukses per numrin {0} err: ({1})!", nrAbonenti, badCommand.Value));
            ImbLogger.LogErrorPromocione(mesazhi.PershkrimMesazhi);
            return mesazhi;

        }

        private string merrXmlResponseInfoNumri()
        {
            return "<AuthedLoyaltyInfo><authenticated>true</authenticated><error>0</error><errorDesc>Success</errorDesc><loyaltyInfo><registrationDate>11/04/2016 16:58</registrationDate><status>Registered</status><eligible>true</eligible><points>91010</points><lastEvents><event><eventType>EtopUpRecharge</eventType><eventDate>18/05/2016 10:18</eventDate><points>+10</points></event><event><eventType>Aparat Celular Samsung Galaxy Core Prime VE</eventType><eventDate>13/05/2016 12:43</eventDate><points>-10000</points></event><event><eventType>VMB</eventType><eventDate>13/05/2016 12:38</eventDate><points>-3000</points></event><event><eventType>Aparat Celular iPhone 6s 16GB </eventType><eventDate>13/05/2016 09:10</eventDate><points>-35000</points></event><event><eventType>Aparat Celular Samsung Galaxy Core Prime VE</eventType><eventDate>12/05/2016 21:09</eventDate><points>-10000</points></event><event><eventType>Aparat Celular Samsung Galaxy Core Prime VE</eventType><eventDate>12/05/2016 20:49</eventDate><points>-10000</points></event><event><eventType>VMB</eventType><eventDate>12/05/2016 20:09</eventDate><points>-3000</points></event><event><eventType>Aparat Celular iPhone 6s Plus 16GB </eventType><eventDate>12/05/2016 19:49</eventDate><points>-40000</points></event><event><eventType>VMB</eventType><eventDate>12/05/2016 15:51</eventDate><points>-3000</points></event><event><eventType>EtopUpRecharge</eventType><eventDate>12/05/2016 10:17</eventDate><points>+10</points></event></lastEvents><rewards><reward><rewardCode>117</rewardCode><rewardType>Gift Bonus</rewardType><pointsNeeded>40000</pointsNeeded><rewardTitle>Aparat Celular</rewardTitle><rewardDesc>iPhone 6s Plus 16GB </rewardDesc></reward><reward><rewardCode>116</rewardCode><rewardType>Gift Bonus</rewardType><pointsNeeded>35000</pointsNeeded><rewardTitle>Aparat Celular</rewardTitle><rewardDesc>iPhone 6s 16GB </rewardDesc></reward><reward><rewardCode>017</rewardCode><rewardType>Gift Bonus</rewardType><pointsNeeded>25000</pointsNeeded><rewardTitle>Aparat Celular</rewardTitle><rewardDesc>Samsung Galaxy S5 Neo</rewardDesc></reward><reward><rewardCode>115</rewardCode><rewardType>Gift Bonus</rewardType><pointsNeeded>20000</pointsNeeded><rewardTitle>Aparat Celular</rewardTitle><rewardDesc>Huawei Ascend P8 Lite</rewardDesc></reward><reward><rewardCode>014</rewardCode><rewardType>Gift Bonus</rewardType><pointsNeeded>15000</pointsNeeded><rewardTitle>Aparat Celular</rewardTitle><rewardDesc>SmartTab 48 inch</rewardDesc></reward><reward><rewardCode>114</rewardCode><rewardType>Gift Bonus</rewardType><pointsNeeded>10000</pointsNeeded><rewardTitle>Aparat Celular</rewardTitle><rewardDesc>Samsung Galaxy Core Prime VE</rewardDesc></reward><reward><rewardCode>0032</rewardCode><rewardType>Gift Bonus</rewardType><pointsNeeded>8000</pointsNeeded><rewardTitle>Aparat Celular</rewardTitle><rewardDesc>Smartphone Samsung Chat B5330</rewardDesc></reward><reward><rewardCode>113</rewardCode><rewardType>Gift Bonus</rewardType><pointsNeeded>6500</pointsNeeded><rewardTitle>Aparat Celular</rewardTitle><rewardDesc>Smart 6 Speed</rewardDesc></reward><reward><rewardCode>055</rewardCode><rewardType>Gift Bonus</rewardType><pointsNeeded>4500</pointsNeeded><rewardTitle>Aparat Celular</rewardTitle><rewardDesc>Aparat Celular Vodafone Smart 6 </rewardDesc></reward><reward><rewardCode>112</rewardCode><rewardType>Gift Bonus</rewardType><pointsNeeded>4000</pointsNeeded><rewardTitle>USB Modem</rewardTitle><rewardDesc>Modem R216 4G WIFI CAT4</rewardDesc></reward><reward><rewardCode>2500</rewardCode><rewardType>Airtime Bonus</rewardType><pointsNeeded>3200</pointsNeeded><rewardTitle>Kohë bisede</rewardTitle><rewardDesc>Përfitoni 2500 Lekë kohë bisede drejtë te gjithe operatoreve kombëtarë e ndërkombëtarë, të vlefshme për  30 ditë nga momenti I përfitimit.</rewardDesc></reward><reward><rewardCode>111</rewardCode><rewardType>Gift Bonus</rewardType><pointsNeeded>3000</pointsNeeded><rewardTitle>USB Modem</rewardTitle><rewardDesc>VMB</rewardDesc></reward><reward><rewardCode>1200</rewardCode><rewardType>Airtime Bonus</rewardType><pointsNeeded>2200</pointsNeeded><rewardTitle>Kohë bisede</rewardTitle><rewardDesc>Përfitoni 1200 Lekë kohë bisede drejtë te gjithe operatoreve kombëtarë e ndërkombëtarë, të vlefshme për  30 ditë nga momenti i përfitimit.</rewardDesc></reward><reward><rewardCode>Club1K</rewardCode><rewardType>Minutes</rewardType><pointsNeeded>1600</pointsNeeded><rewardTitle>Kohë bisede</rewardTitle><rewardDesc>Përfitoni paketën me 1000  minuta kombetare të vlefshme per 7 ditë nga momenti i dhurimit (vetëm për Vodafone Club)</rewardDesc></reward><reward><rewardCode>DATA2GBI</rewardCode><rewardType>Data Bonus</rewardType><pointsNeeded>1600</pointsNeeded><rewardTitle>Internet</rewardTitle><rewardDesc>Përfitoni paketën me 2GB internet  të vlefshme për  7 ditë nga momenti i dhurimit.</rewardDesc></reward><reward><rewardCode>DATA200MB</rewardCode><rewardType>Data Bonus</rewardType><pointsNeeded>1200</pointsNeeded><rewardTitle>Internet</rewardTitle><rewardDesc>Përfitoni paketën me 1 GB internet  të vlefshme për  7 ditë nga momenti i dhurimit.</rewardDesc></reward><reward><rewardCode>Club1000</rewardCode><rewardType>Minutes</rewardType><pointsNeeded>1200</pointsNeeded><rewardTitle>Kohë bisede</rewardTitle><rewardDesc>Përfitoni paketën me 500 minuta kombetare të vlefshme per 7 ditë nga momenti i dhurimit (vetëm për klientët Vodafone Club)</rewardDesc></reward><reward><rewardCode>Club300</rewardCode><rewardType>Minutes</rewardType><pointsNeeded>1000</pointsNeeded><rewardTitle>Kohë bisede</rewardTitle><rewardDesc>Përfitoni paketën me 300  minuta kombetare të vlefshme per 7 ditë nga momenti i dhurimit (vetëm për Vodafone Club)</rewardDesc></reward><reward><rewardCode>DATA750MB</rewardCode><rewardType>Data Bonus</rewardType><pointsNeeded>1000</pointsNeeded><rewardTitle>Internet</rewardTitle><rewardDesc>Përfitoni paketën me 750MB internet  të vlefshme për  7 ditë nga momenti i dhurimit.</rewardDesc></reward><reward><rewardCode>DATA100MB</rewardCode><rewardType>Data Bonus</rewardType><pointsNeeded>700</pointsNeeded><rewardTitle>Internet</rewardTitle><rewardDesc>Përfitoni paketën me 500MB internet  të vlefshme për  7 ditë nga momenti i dhurimit.</rewardDesc></reward><reward><rewardCode>Club500</rewardCode><rewardType>Minutes</rewardType><pointsNeeded>700</pointsNeeded><rewardTitle>Kohë bisede</rewardTitle><rewardDesc>Përfitoni paketën me 150  minuta kombetare të vlefshme per 7 ditë nga momenti i dhurimit (vetëm për Vodafone Club)</rewardDesc></reward><reward><rewardCode>Club100</rewardCode><rewardType>Minutes</rewardType><pointsNeeded>500</pointsNeeded><rewardTitle>Kohë bisede</rewardTitle><rewardDesc>Përfitoni paketën me 100  minuta kombetare të vlefshme per 7 ditë nga momenti i dhurimit (vetëm për Vodafone Club)</rewardDesc></reward><reward><rewardCode>DATA250MB</rewardCode><rewardType>Data Bonus</rewardType><pointsNeeded>500</pointsNeeded><rewardTitle>Internet</rewardTitle><rewardDesc>Përfitoni paketën me 250MB internet  të vlefshme për  7 ditë nga momenti i dhurimit.</rewardDesc></reward></rewards><expiringPoints /></loyaltyInfo></AuthedLoyaltyInfo>";

        }

        #endregion
    }
}
