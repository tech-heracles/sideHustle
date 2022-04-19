using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Xml.Linq;
using DbCore.IMBUtils.Logging;

namespace DbCore.Integrime
{
    public class clsSocket
    {

        /// <summary>
        /// Metode vetem per integrimin me VODAFONE. Sherben per te marre piket e klientit dhe listen e te gjithe artikujve qe mund te marre si dhurate
        /// </summary>
        /// <param name="klient">Socketin e klientit te hapur.</param>
        /// <param name="nrAbonenti">Nr e telefonit te abonentin te cilit i eshte bere autentifikimi.</param>
        /// <returns>Kthen listen e dhuratave qe mund te marre me piket qe ka.</returns>
        [Obsolete("Perdor metoden ekuivalente ne PromocioneAdapter")]
        public static List<String> merrInfoPerNumrinSocket(Socket klient, string nrAbonenti, out clsMesazh mesazh, out int pike)
        {
            mesazh = new clsMesazh();
            pike = 0;
            string buffersize = System.Web.Configuration.WebConfigurationManager.AppSettings["buffersize"];
            string sleeptime = System.Web.Configuration.WebConfigurationManager.AppSettings["sleeptime"];
            byte[] bytes = new byte[int.Parse(buffersize)];
            String pergjigjaInfo = "";
            //int piketKthyera = 0;
            List<String> dhuratat = new List<string>();

            //Dergimi metodes

            //TO DO duhet ndryshuar me nr fleksibel, por provat duhen bere vetem me kete nr 355692201969
            // byte[] msg = Encoding.ASCII.GetBytes("<Command><getLoyaltyInfo><Msisdn>" + 355692201968 + "</Msisdn></getLoyaltyInfo></Command>");//test
            byte[] msg = Encoding.ASCII.GetBytes("<Command><getLoyaltyInfo><Msisdn>" + nrAbonenti + "</Msisdn></getLoyaltyInfo></Command>");//live
            ImbLogger.LogInfoPromocione(" Po dergohet komanda per te marre infoPerNumrinSocket: <Command><getLoyaltyInfo><Msisdn>" + nrAbonenti + "</Msisdn></getLoyaltyInfo></Command>");
            int bytesSent = klient.Send(msg);
            //Marrja e pergjigjes
            klient.ReceiveTimeout = 0;
            klient.ReceiveBufferSize = int.Parse(buffersize);
            //Duhet vendosur pasi nuk arrihet te merret pergjigja e plote nga socket-i. Eshte vendosur nje pritje prej 2 sekondash.
            //Thread.Sleep(int.Parse(sleeptime));
            int bytesRec = 1;// klient.Receive(bytes);
            string answer = "";
            // answer += Encoding.ASCII.GetString(bytes, 0, bytesRec);
            for (int i = 0; i < 25; i++)
            {
                bytes = new byte[int.Parse(buffersize)];
                bytesRec = klient.Receive(bytes);
                answer += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                if (answer.Contains("</CommandResponse>"))
                    break;
            }
            //  Console.WriteLine("Echoed test = {0}", Encoding.ASCII.GetString(bytes, 0, bytesRec));
            // return dhuratat;

            try
            {
                ImbLogger.LogInfoPromocione("Pergjigja nga socket: " + answer);
                //Marrja e pjeses qe na intereson nga XML qe kthehet mbrapa
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
                            mesazh = new clsMesazh(true, String.Format("Piket e grumbulluara jane {0}!", pike));

                            ImbLogger.LogInfoPromocione(String.Format("Piket e grumbulluara jane {0}!", pike));

                            foreach (XElement elementDhurate in xdocPiket.Descendants("reward"))
                            {
                                if (elementDhurate.Element("rewardType").Value == "Gift Bonus")

                                    dhuratat.Add(elementDhurate.Element("rewardCode").Value);
                            }
                            return dhuratat;
                        }
                        else
                        {
                            mesazh = new clsMesazh(false, "Numri nuk eshte i regjistruar ne VF ONE!");
                            ImbLogger.LogInfoPromocione("Numri nuk eshte i regjistruar ne VF ONE!");
                            //   mbyllSocket(klient);
                            return dhuratat;
                        }
                    }
                    else if (String.Compare(pergjigjaInfo, "0") > 0)
                    {
                        XElement arsyeMosDergimInfo = xdocPiket.Descendants("errorDesc").FirstOrDefault();
                        if (arsyeMosDergimInfo.Value.Contains("Subscriber is not registered"))
                        {
                            mesazh = new clsMesazh(false, String.Format("Numri nuk eshte i regjistruar ne VF ONE!"));
                            ImbLogger.LogInfoPromocione("Numri nuk eshte i regjistruar ne VF ONE!");
                        }
                        else
                        {
                            mesazh = new clsMesazh(false, String.Format("Marrja e pikeve nuk u krye me sukses ({0})!", arsyeMosDergimInfo.Value));
                            ImbLogger.LogInfoPromocione(String.Format("Marrja e pikeve nuk u krye me sukses ({0})!", arsyeMosDergimInfo.Value));
                        }
                        //  mbyllSocket(klient);
                        return dhuratat;
                    }
                    else
                    {
                        mesazh = new clsMesazh(false, "Pergjigje e pa identifikuar!");
                        ImbLogger.LogInfoPromocione("Pergjigje e pa identifikuar!");
                        //    mbyllSocket(klient);
                        return dhuratat;
                    }
                }
                else
                {
                    mesazh = new clsMesazh(false, String.Format("Marrja e pikeve nuk u krye me sukses ({0})!", badCommand.Value));
                    ImbLogger.LogInfoPromocione(String.Format("Marrja e pikeve nuk u krye me sukses ({0})!", badCommand.Value));
                    //  mbyllSocket(klient);
                    return dhuratat;
                }
            }
            catch (Exception error)
            {
                mesazh = new clsMesazh(false, error.Message);
                ImbLogger.LogInfoPromocione(error.Message);
                //  mbyllSocket(klient);
                return dhuratat;
            }
        }
        /// <summary>
        /// Metode vetem per integrimin me VODAFONE. Sherben per te mbylluar socketin e krijuar ne momentin qe perfundojme te gjitha komandat qe duhen ekzekutuar ne socket.
        /// </summary>
        /// <param name="klient">Merr si parameter socket-in qe duhet mbyllur.</param>
        public static void mbyllSocket(Socket klient)
        {
            klient.Shutdown(SocketShutdown.Both);
            klient.Close();
            ImbLogger.LogInfoPromocione("Socket u mbyll me sukses!");
        }


        /// <summary>
        /// Metode vetem per integrimin me VODAFONE. Sherben per tu loguar ne socketin e Vodafone-it.
        /// </summary>
        /// <returns>Kthen Socket-in e autentifikuar ose nje Socket bosh nese autentifikimi nuk eshte i sakte.</returns>
        public static Socket loginNeSocket(out clsMesazh mesazh, string ip)
        {
            mesazh = new clsMesazh();
            string buffersize = System.Web.Configuration.WebConfigurationManager.AppSettings["buffersize"];
            byte[] bytes = new byte[int.Parse(buffersize)];
            String pergjigjaLogim = "";
            Socket klient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //return klient;
            try
            {
                //Krijimi i lidhjes me serverin qe degjon 
                string ipvfone = ip;
                ImbLogger.LogInfoPromocione("Po tenton te logohet ne socket! IP:" + ipvfone);
                klient.Connect(ipvfone, 6666);//live
                // klient.Connect("10.5.98.10", 6666);//live
                //klient.Connect("10.5.13.197", 6666);//test
                //IAsyncResult rezultati = klient.BeginConnect("10.5.13.197", 6666, null, null);
                //rezultati.AsyncWaitHandle.WaitOne(50000, true);
                //Dergimi autentifikimit
                byte[] msg = Encoding.ASCII.GetBytes("<Command><Login><Username>WebLoyalty</Username><Password>W3bL0y@lty</Password></Login></Command>");
                ImbLogger.LogInfoPromocione(" Po dergohet komanda per login: <Command><Login><Username>WebLoyalty</Username><Password>W3bL0y@lty</Password></Login></Command>");
                int bytesSent = klient.Send(msg);
                //Marrja e pergjigjes nga autetifikimi
                klient.ReceiveTimeout = 0;
                klient.ReceiveBufferSize = int.Parse(buffersize);
                int bytesRec = klient.Receive(bytes);
                //Marrja e pjeses qe na intereson nga XML qe kthehet mbrapa
                XDocument xdoc = XDocument.Parse(Encoding.ASCII.GetString(bytes, 0, bytesRec));
                XElement response = xdoc.Descendants("CommandResult").FirstOrDefault();
                pergjigjaLogim = response.Value;
                ImbLogger.LogInfoPromocione("Pergjigje logini: " + pergjigjaLogim);
                if (pergjigjaLogim.Equals("Success"))
                {
                    ImbLogger.LogInfoPromocione("Autentifikimi i userit u krye me sukses!");
                    mesazh = new clsMesazh(true, "Autentifikimi i userit u krye me sukses!");
                    return klient;
                }
                else if (pergjigjaLogim.Equals("Fail"))
                {
                    XElement arsyeMosLogimiPergjigje = xdoc.Descendants("Message").FirstOrDefault();
                    mesazh = new clsMesazh(false, String.Format("Autentifikimi i userit socket nuk u krye me sukses ({0})!", arsyeMosLogimiPergjigje.Value));
                    ImbLogger.LogInfoPromocione(String.Format("Autentifikimi i userit socket nuk u krye me sukses ({0})!", arsyeMosLogimiPergjigje.Value));

                    mbyllSocket(klient);
                    return new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                }
                else
                {
                    mesazh = new clsMesazh(false, "Pergjigje e pa identifikuar!");
                    ImbLogger.LogInfoPromocione("Pergjigje e pa identifikuar!");
                    mbyllSocket(klient);
                    return new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                }
            }
            catch (Exception error)
            {
                String gabimi = error.Message;
                mesazh = new clsMesazh(false, String.Format("Autentifikimi i userit socket nuk u krye me sukses ({0})!", gabimi));
                ImbLogger.LogInfoPromocione(String.Format("Autentifikimi i userit socket nuk u krye me sukses ({0})!", gabimi));
                mbyllSocket(klient);
                return new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            }
        }



        /// <summary>
        /// Metode vetem per integrimin me VODAFONE. Sherben per te zbritur piket per klientin pasi ka zgjedhur dhuraten.
        /// </summary>
        /// <param name="klient">Socketin e klientit te hapur.</param>
        /// <param name="nrAbonenti">Nr e telefonit te abonentin te cilit i eshte bere autentifikimi.</param>
        /// <param name="kodArtikulli">Kodi i artikullit te cilin do marre si dhurate.</param>
        /// <returns>Kthen nese zbritja e pikeve u krye me sukses ose jo.</returns>
        [Obsolete("Perdor metoden ekuivalente tek promoAdapter")]
        public static clsMesazh degoDhuratenZgjedhurSocket(Socket klient, string nrAbonenti, string kodArtikulli)
        {
            string buffersize = System.Web.Configuration.WebConfigurationManager.AppSettings["buffersize"];
            //string sleeptime = System.Web.Configuration.WebConfigurationManager.AppSettings["sleeptime"];

            byte[] bytes = new byte[int.Parse(buffersize)];
            String pergjigjaInfo = "";
            //Dergimi metodes
            //Dergimi metodes
            string xmlRequest = @"<Command><SubscribeLoyaltyBonus><Msisdn>" + nrAbonenti + @"</Msisdn><DestinationMsisdn>" + nrAbonenti + @"</DestinationMsisdn><BonusCode>" + kodArtikulli + @"</BonusCode></SubscribeLoyaltyBonus></Command>";
            ImbLogger.LogInfoPromocione("Po dergohet komanda per te derguar dhuraten e zgjedhur: " + xmlRequest);
            byte[] msg = Encoding.ASCII.GetBytes(xmlRequest);

            //                byte[] msg = Encoding.ASCII.GetBytes(@"<SubscribeLoyaltyBonus>
            //                                                        <Msisdn>" + nrAbonenti + @"</Msisdn>
            //                                                        <DestinationMsisdn>" + nrAbonenti + @"</DestinationMsisdn>
            //                                                        <BonusCode>" + kodArtikulli + @"</BonusCode>
            //                                                    </SubscribeLoyaltyBonus>");
            int bytesSent = klient.Send(msg);
            //Marrja e pergjigjes
            klient.ReceiveTimeout = 0;
            klient.ReceiveBufferSize = int.Parse(buffersize);
            //Duhet vendosur pasi nuk arrihet te merret pergjigja e plote nga socket-i. Eshte vendosur nje pritje prej 2 sekondash.
            //Thread.Sleep(int.Parse(sleeptime));
            int bytesRec = klient.Receive(bytes);
            //Marrja e pjeses qe na intereson nga XML qe kthehet mbrapa
            XDocument xdocDhurata = XDocument.Parse(Encoding.ASCII.GetString(bytes, 0, bytesRec));
            ImbLogger.LogInfoPromocione("Pergjigja e kerkeses per marrjen e dhurates " + Encoding.ASCII.GetString(bytes, 0, bytesRec));
            XElement badCommand = xdocDhurata.Descendants("BadCommand").FirstOrDefault();
            if (badCommand == null)
            {
                XElement commandResult = xdocDhurata.Descendants("CommandResult").FirstOrDefault();
                pergjigjaInfo = commandResult.Value;
                ImbLogger.LogInfoPromocione("Pergjigja e kerkeses per marrjen e dhurates " + pergjigjaInfo);
                if (pergjigjaInfo.Equals("Success"))
                {
                    ImbLogger.LogInfoPromocione("Dhurata u morr me sukses!");
                    return new clsMesazh(true, "Dhurata u morr me sukses!");
                }
                if (pergjigjaInfo.Equals("Fail"))
                {
                    XElement arsyeMosDergimDhurata = xdocDhurata.Descendants("ResultMessage").FirstOrDefault();
                    ImbLogger.LogInfoPromocione(String.Format("Marrja e dhurates nga socket-i nuk u krye me sukses! ({0})!", arsyeMosDergimDhurata.Value));
                    return new clsMesazh(false, String.Format("Marrja e dhurates nga socket-i nuk u krye me sukses! ({0})!", arsyeMosDergimDhurata.Value));


                }
                ImbLogger.LogInfoPromocione("Pergjigje e pa identifikuar!");
                return new clsMesazh(false, "Pergjigje e pa identifikuar!");
            }
            ImbLogger.LogInfoPromocione(String.Format("Marrja e dhurates nga socket-i nuk u krye me sukses! ({0})!", badCommand.Value));
            return new clsMesazh(false, String.Format("Marrja e dhurates nga socket-i nuk u krye me sukses! ({0})!", badCommand.Value));

        }
        /// <summary>
        /// dergon komand per gjenerimin e pinit per vfone
        /// </summary>
        /// <param name="nrkontakti"></param>
        /// <returns></returns>
        public static clsMesazh GjeneroPinVfOne(string nrkontakti)
        { //Gjenerimi i PIN-it ne nr e klientit per vod one
            string ipvfone = System.Web.Configuration.WebConfigurationManager.AppSettings["ipsmsvfone"];
            string strRedirect = string.Format("http://{1}/vfone/pin2.php?str_var={0}|gjenero|", "3556" + nrkontakti, ipvfone);
            return GjeneroPin(strRedirect);
        }

        /// <summary>
        /// dergon komand per gjenerimin e pinit per bazaar
        /// </summary>
        /// <param name="nrkontakti"></param>
        /// <returns></returns>
        public static clsMesazh GjeneroPinPerBazaar(string nrkontakti)
        { //Gjenerimi i PIN-it ne nr e klientit per bazaar
            string ipvfone = System.Web.Configuration.WebConfigurationManager.AppSettings["ipsmsvfone"];
            string strRedirect = string.Format("http://{1}/bazaar/pin.php?str_var={0}|gjenero|", "3556" + nrkontakti, ipvfone);
            return GjeneroPin(strRedirect);
        }
        /// <summary>
        /// gjeneron pinin per promocionin device with discount per krishtlindje
        /// </summary>
        /// <param name="nrkontakti"></param>
        /// <returns></returns>
        public static clsMesazh GjeneroPinPerDD(string nrkontakti)
        { //Gjenerimi i PIN-it ne nr e klientit per bazaar
            string ipvfone = System.Web.Configuration.WebConfigurationManager.AppSettings["ipsmsvfone"];
            string strRedirect = string.Format("http://{1}/christmas/pin.php?str_var={0}|gjenero|", "3556" + nrkontakti, ipvfone);
            return GjeneroPin(strRedirect);
        }
        /// <summary>
        /// dergon komanden per gjenerimin e pinit
        /// </summary>
        /// <param name="strRedirect"></param>
        /// <returns></returns>
        private static clsMesazh GjeneroPin(string strRedirect)
        {
            if (KonfigurimeStatikeIntegrimi.FakePin) return new clsMesazh(true);
            ImbLogger.LogInfoPromocione("Po dergohet pini: " + strRedirect);
            String pergjigja = DergoKerkese(strRedirect);

            ImbLogger.LogInfoPromocione("Pergjigje dergim pini: " + pergjigja);
            if (pergjigja.Contains("Mesg_SUCCESFULPIN"))
            {
                ImbLogger.LogInfoPromocione("Pini u dergua me sukses!");
                return new clsMesazh(true, "Pini u dergua me sukses!");

            }
            else
            {

                ImbLogger.LogErrorPromocione("Pati probleme gjate dergimit te pinit! " + pergjigja);
                return new clsMesazh(false, "Pati probleme gjate dergimit te pinit! " + pergjigja);
            }
        }

        public static string DergoKerkese(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Timeout = 30000;
            request.ReadWriteTimeout = 50000;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream receiveStream = response.GetResponseStream();
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
            return readStream.ReadToEnd();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="strRedirect"></param>
        /// <returns></returns>
        private static clsMesazh VerifikoPin(string strRedirect)
        { //Konfirmimi i PIN-it nga perderuesi
            if (KonfigurimeStatikeIntegrimi.FakePin) return new clsMesazh(true);
            ImbLogger.LogInfoPromocione("Po dergohet verifikimi i pinit: " + strRedirect);
            String pergjigja = DergoKerkese(strRedirect);
            ImbLogger.LogInfoPromocione("Pergjigje verifikim pini: " + pergjigja);
            clsMesazh mesazh = new clsMesazh();
            if (pergjigja.Contains("Mesg_CONFIRMEDPIN"))
            {

                ImbLogger.LogInfoPromocione("Pini eshte i sakte!");
                mesazh.PershkrimMesazhi = "Pini eshte i sakte!";
                mesazh.Status = true;

            }
            else
            {

                ImbLogger.LogInfoPromocione("Pini eshte i gabuar!");
                mesazh.PershkrimMesazhi = "Pini eshte i gabuar!";
                mesazh.Status = false;

            }

            return mesazh;
        }
        /// <summary>

        /// </summary>
        /// <param name="nrKontakti"></param>
        /// <param name="pin"></param>
        /// <returns></returns>
        public static clsMesazh VerifikoPinVfOne(string nrKontakti, string pin)
        {
            string ipvfone = System.Web.Configuration.WebConfigurationManager.AppSettings["ipsmsvfone"];
            String strRedirect = String.Format("http://{2}/vfone/pin2.php?str_var={0}|konfirmo|{1}", "3556" + nrKontakti, pin, ipvfone);
            return VerifikoPin(strRedirect);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nrKontakti"></param>
        /// <param name="pin"></param>
        /// <returns></returns>
        public static clsMesazh VerifikoPinBazaar(string nrKontakti, string pin)
        {
            string ipvfone = System.Web.Configuration.WebConfigurationManager.AppSettings["ipsmsvfone"];
            String strRedirect = String.Format("http://{2}/bazaar/pin.php?str_var={0}|konfirmo|{1}", "3556" + nrKontakti, pin, ipvfone);
            return VerifikoPin(strRedirect);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nrKontakti"></param>
        /// <param name="pin"></param>
        /// <returns></returns>
        public static clsMesazh VerifikoPinDD(string nrKontakti, string pin)
        {
            string ipvfone = System.Web.Configuration.WebConfigurationManager.AppSettings["ipsmsvfone"];
            String strRedirect = String.Format("http://{2}/christmas/pin.php?str_var={0}|konfirmo|{1}", "3556" + nrKontakti, pin, ipvfone);
            return VerifikoPin(strRedirect);
        }

    }
}
