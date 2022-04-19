using DbCore.DbAdmin;
using DbCore.DbKontabiliteti;
using DbCore.DbListPagesat;
using DbCore.DbRegjistrim;
using DbCore.DbShare;
using DbCore.IMBUtils;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Web;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.Security;
using DbCore.IMBUtils.Messages;
using DbCore.Raporte;

namespace DbCore
{
    public class EmailComposer
    {
        private static string postFixlinkToFollow = "Shto_RegjistrimDokumentash.aspx?shitje_blerje=#shitjeBlerje#&id=#idDok#&numer=#nrDok#&indexrow=0&shtim_modifikim=modifikim&vjenNga=aprovim&idetapa=#idEtapa#&nrprocesi=#idProcesi#&idNderm=#idNderm#&idVitNdermarrje=#IdVitNdermarrje#";
        private static string postFixlinkToFollowList = "Shto_ListPagesa.aspx?id=#idDok#&numer=#nrDok#&indexrow=0&shtim_modifikim=modifikim&vjenNga=aprovim&idetapa=#idEtapa#&nrprocesi=#idProcesi#&idNderm=#idNderm#&idVitNdermarrje=#IdVitNdermarrje#";
        private static string postfixlista = "ListeAprovimi.aspx?status=aprovim&idNderm=#idNderm#&idVitNdermarrje=#IdVitNdermarrje#";
        private static Logger logu = LogManager.GetCurrentClassLogger();
        public static string IdNdermVit;
        public string ktheHtmlEmail()
        {
            throw new NotImplementedException();
        }
        public static void DergoEmailStandart(int idNdermarrje, string[] toEmail, string subject, string bodyMesazh, int idPerdoruesi, bool isBodyHtml = false)
        {
            if (toEmail.Length < 1)
            {
                ImbLogger.Error("E-mail nuk mund te dergohet sepse mungon adresa e e-mailit!");
                return;
            }
            try
            {
                StringBuilder body = new StringBuilder("");

                body.Append(bodyMesazh);

                clsMailSender mail = new clsMailSender(idNdermarrje, subject);

                mail.SendPlainAsyncEmail(toEmail, subject, body + "", idNdermarrje, idPerdoruesi, isBodyHtml);

                ImbLogger.Info("E-mail u dergua me sukses!");
            }
            catch (Exception ex)
            {
                ImbLogger.Error(ex);
            }
        }


        /// <summary>
        /// Per te gjitha rastet e work flow
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="serverUrl"></param>
        /// <param name="shitjeBlerje"></param>
        /// <param name="ci"></param>
        /// <param name="llojDok"></param>
        /// <param name="nrDok"></param>
        /// <param name="idDok"></param>
        /// <param name="idEtapa"></param>
        /// <param name="nrProcesi"></param>
        /// <param name="dtDok"></param>
        /// <param name="statusAprovimi"></param>
        /// <param name="emerPerdoruesi"></param>
        /// <param name="aprovuesi"></param>
        /// <param name="krijuesit"></param>
        /// <returns></returns>
        private static void sendPlainTextEmail(int idNdermarrje, string[] toEmail, string[] ccEmail, string serverUrl, string shitjeBlerje, System.Globalization.CultureInfo ci, string llojiDok, string nrDok, int idDok, int idEtapa, int nrProcesi, string dtDok, string formatAprovimi, string emerPerdoruesi, string aprovuesi, int idkategoria, int idPerdoruesi)
        {
            try
            {
                string listpagesa = postFixlinkToFollowList;
                ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                string aprovimTitullEmail = rm.GetString("aprovimTitullEmail", ci);
                string aprovimDokStatus;
                switch (formatAprovimi)
                {
                    case "aprovimDokModifikuarKrijuesit": //Formati 4: Dërgimi i emailit krijuesit qe dokumenti u modifikua
                        if (string.IsNullOrEmpty(emerPerdoruesi)) throw new MyException("Tek formati i modifikimit duhet te specifikohet emri i perdoruesit");
                        aprovimDokStatus = rm.GetString("aprovimDokModifikuarKrijuesit", ci);     //Rasti 4: Dërgimi i emailit krijuesit qe dokumenti u modifikua
                        aprovimDokStatus = aprovimDokStatus.Replace("#llojiDok#", llojiDok);
                        aprovimDokStatus = aprovimDokStatus.Replace("#nrDok#", nrDok);
                        aprovimDokStatus = aprovimDokStatus.Replace("#dtDok#", dtDok);
                        aprovimDokStatus = aprovimDokStatus.Replace("#emerPerdoruesi#", emerPerdoruesi);
                        break;

                    case "aprovimDokModifikuarParaleleve":  //Formati 5: Dërgimi i emailit perdoruesve paralel per modifikimin e dokumentit
                        if (string.IsNullOrEmpty(emerPerdoruesi)) throw new MyException("Tek formati i modifikimit duhet te specifikohet emri i perdoruesit");
                        aprovimDokStatus = rm.GetString("aprovimDokModifikuarParaleleve", ci);
                        aprovimDokStatus = aprovimDokStatus.Replace("#llojiDok#", llojiDok);
                        aprovimDokStatus = aprovimDokStatus.Replace("#nrDok#", nrDok);
                        aprovimDokStatus = aprovimDokStatus.Replace("#dtDok#", dtDok);
                        aprovimDokStatus = aprovimDokStatus.Replace("#emerPerdoruesi#", emerPerdoruesi);

                        break;

                    case "aprovimDokPerAprovim": //Formati 1: Dërgimi i email për aprovim
                        if (string.IsNullOrEmpty(emerPerdoruesi)) throw new MyException("Tek formati i modifikimit duhet te specifikohet emri i perdoruesit");
                        aprovimDokStatus = rm.GetString("aprovimDokPerAprovim", ci);
                        aprovimDokStatus = aprovimDokStatus.Replace("#llojiDok#", llojiDok);
                        aprovimDokStatus = aprovimDokStatus.Replace("#nrDok#", nrDok);
                        aprovimDokStatus = aprovimDokStatus.Replace("#dtDok#", dtDok);
                        aprovimDokStatus = aprovimDokStatus.Replace("#emerPerdoruesi#", emerPerdoruesi);
                        listpagesa = postfixlista;
                        break;

                    case "aprovimDokAprovuar": //Formati 2: Dërgimi i emailit krijuesit te dokumentit në rastin e aprovimit perfundimtar të dokumentit
                        if (string.IsNullOrEmpty(aprovuesi)) throw new MyException("Tek formati i modifikimit duhet te specifikohet emri i perdoruesit");
                        aprovimDokStatus = rm.GetString("aprovimDokAprovuar", ci);
                        aprovimDokStatus = aprovimDokStatus.Replace("#llojiDok#", llojiDok);
                        aprovimDokStatus = aprovimDokStatus.Replace("#nrDok#", nrDok);
                        aprovimDokStatus = aprovimDokStatus.Replace("#dtDok#", dtDok);
                        aprovimDokStatus = aprovimDokStatus.Replace("#emerPerdoruesi#", aprovuesi);
                        break;

                    case "aprovimDokRefuzim": //Formati 3: Dërgimi i emailit krijuesit në rastin e refuzimit
                        if (string.IsNullOrEmpty(emerPerdoruesi)) throw new MyException("Tek formati i modifikimit duhet te specifikohet emri i perdoruesit");
                        aprovimDokStatus = rm.GetString("aprovimDokRefuzim", ci);
                        aprovimDokStatus = aprovimDokStatus.Replace("#llojiDok#", llojiDok);
                        aprovimDokStatus = aprovimDokStatus.Replace("#nrDok#", nrDok);
                        aprovimDokStatus = aprovimDokStatus.Replace("#dtDok#", dtDok);
                        aprovimDokStatus = aprovimDokStatus.Replace("#emerPerdoruesi#", emerPerdoruesi);
                        break;

                    case "aprovimDokDelegim": //Formati 6: Dërgimi i emailit krijuesit dhe të deleguarit për delegimin e dokumentit
                        if (string.IsNullOrEmpty(emerPerdoruesi)) throw new MyException("Tek formati i delegimit duhet te specifikohet emri i perdoruesit");
                        if (string.IsNullOrEmpty(aprovuesi)) throw new MyException("Tek formati i delegimit duhet te specifikohet emri i te deleguarit");
                        aprovimDokStatus = rm.GetString("aprovimDokDelegim", ci);
                        aprovimDokStatus = aprovimDokStatus.Replace("#llojiDok#", llojiDok);
                        aprovimDokStatus = aprovimDokStatus.Replace("#nrDok#", nrDok);
                        aprovimDokStatus = aprovimDokStatus.Replace("#dtDok#", dtDok);
                        aprovimDokStatus = aprovimDokStatus.Replace("#emerPerdoruesi#", emerPerdoruesi);
                        aprovimDokStatus = aprovimDokStatus.Replace("#iDeleguari#", aprovuesi);
                        break;

                    case "aprovimDokAprovuarRol": //Formati 7: Dërgimi i emailit te roli pas aprovimit nga nje element i rolit
                        if (string.IsNullOrEmpty(emerPerdoruesi)) throw new MyException("Tek formati i aprovuarRol duhet te specifikohet emri i perdoruesit");
                        aprovimDokStatus = rm.GetString("aprovimDokAprovuarRol", ci);
                        aprovimDokStatus = aprovimDokStatus.Replace("#llojiDok#", llojiDok);
                        aprovimDokStatus = aprovimDokStatus.Replace("#nrDok#", nrDok);
                        aprovimDokStatus = aprovimDokStatus.Replace("#dtDok#", dtDok);
                        aprovimDokStatus = aprovimDokStatus.Replace("#emerPerdoruesi#", emerPerdoruesi);
                        listpagesa = postfixlista;
                        break;

                    default: throw new MyException("formati aprovimi i paparashikuar per dergimin e email-it");
                }
                string finalPostFixLinkToFollow = "";
                if (idkategoria == 1)
                    finalPostFixLinkToFollow = postFixlinkToFollow.Replace("#llojiDok#", llojiDok);
                else finalPostFixLinkToFollow = listpagesa;
                finalPostFixLinkToFollow = finalPostFixLinkToFollow.Replace("#nrDok#", nrDok);
                finalPostFixLinkToFollow = finalPostFixLinkToFollow.Replace("#dtDok#", dtDok);
                finalPostFixLinkToFollow = finalPostFixLinkToFollow.Replace("#shitjeBlerje#", shitjeBlerje);
                finalPostFixLinkToFollow = finalPostFixLinkToFollow.Replace("#idDok#", idDok.ToString());
                finalPostFixLinkToFollow = finalPostFixLinkToFollow.Replace("#idEtapa#", idEtapa.ToString());
                finalPostFixLinkToFollow = finalPostFixLinkToFollow.Replace("#idProcesi#", nrProcesi.ToString());
                finalPostFixLinkToFollow = finalPostFixLinkToFollow.Replace("#idNderm#", idNdermarrje.ToString());
                finalPostFixLinkToFollow = finalPostFixLinkToFollow.Replace("#IdVitNdermarrje#", IdNdermVit);
                finalPostFixLinkToFollow = $"{rm.GetString("aprovimLinkDok", ci)} {serverUrl}/{finalPostFixLinkToFollow}{Environment.NewLine}";
                
                string plainTextBodyEmail = aprovimTitullEmail + Environment.NewLine + aprovimDokStatus + Environment.NewLine + finalPostFixLinkToFollow + Environment.NewLine + rm.GetString("aprovimDokPostScriptum", ci);
                clsMailSender mailSender = new clsMailSender(idNdermarrje, rm.GetString("aprovimDokEmerEmali", ci));
                mailSender.SendPlainAsyncWfEmail(toEmail, aprovimTitullEmail, plainTextBodyEmail, idNdermarrje, nrProcesi, idPerdoruesi);
            }
            catch (Exception ex)
            {
                ImbLogger.Error(ex);
            }
        }

        /// <summary>
        /// Rasti1: Dërgimi i email për aprovim
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="serverUrl"></param>
        /// <param name="shitjeBlerje"></param>
        /// <param name="ci"></param>
        /// <param name="llojDok"></param>
        /// <param name="nrDok"></param>
        /// <param name="idDok"></param>
        /// <param name="idEtapa"></param>
        /// <param name="idProcesi"></param>
        /// <param name="dtDok"></param>
        /// <returns></returns>
        public static void sendPlainTextEmailPerAprovim(int idNdermarrje, string[] toEmail, string serverUrl, string shitjeBlerje, System.Globalization.CultureInfo ci, string llojDok, string nrDok, int idDok, int idEtapa, int idProcesi, string dtDok, string emerPerdoruesiiPlote, int idkategoria, int idPerdoruesi)
        {
            sendPlainTextEmail(idNdermarrje, toEmail, null, serverUrl, shitjeBlerje, ci, llojDok, nrDok, idDok, idEtapa, idProcesi, dtDok, "aprovimDokPerAprovim", emerPerdoruesiiPlote, null, idkategoria, idPerdoruesi);
        }

        /// <summary>
        /// Rasti 2: Dërgimi i emailit krijuesit te dokumentit në rastin e aprovimit perfundimtar të dokumentit
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="serverUrl"></param>
        /// <param name="shitjeBlerje"></param>
        /// <param name="ci"></param>
        /// <param name="llojDok"></param>
        /// <param name="nrDok"></param>
        /// <param name="idDok"></param>
        /// <param name="idEtapa"></param>
        /// <param name="idProcesi"></param>
        /// <param name="dtDok"></param>
        /// <returns></returns>
        public static void sendPlainTextEmailAprovuar(int idNdermarrje, string[] toEmail, string serverUrl, string shitjeBlerje, System.Globalization.CultureInfo ci, string llojDok, string nrDok, int idDok, int idEtapa, int idProcesi, string dtDok, string emerPerdoruesiiPlote, int idkategoria, int idPerdoruesi)
        {
            sendPlainTextEmail(idNdermarrje, toEmail, null, serverUrl, shitjeBlerje, ci, llojDok, nrDok, idDok, idEtapa, idProcesi, dtDok, "aprovimDokAprovuar", null, emerPerdoruesiiPlote, idkategoria, idPerdoruesi);
        }

        /// <summary>
        /// Formati 3: Dërgimi i emailit krijuesit në rastin e refuzimit
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="serverUrl"></param>
        /// <param name="shitjeBlerje"></param>
        /// <param name="ci"></param>
        /// <param name="llojDok"></param>
        /// <param name="nrDok"></param>
        /// <param name="idDok"></param>
        /// <param name="idEtapa"></param>
        /// <param name="idProcesi"></param>
        /// <param name="dtDok"></param>
        /// <param name="emerPerdoruesi"></param>
        /// <returns></returns>
        public static void sendPlainTextEmailRefuzuar(int idNdermarrje, string[] toEmail, string serverUrl, string shitjeBlerje, System.Globalization.CultureInfo ci, string llojDok, string nrDok, int idDok, int idEtapa, int idProcesi, string dtDok, string emerPerdoruesiiPlote, int idkategoria, int idPerdoruesi)
        {
            sendPlainTextEmail(idNdermarrje, toEmail, null, serverUrl, shitjeBlerje, ci, llojDok, nrDok, idDok, idEtapa, idProcesi, dtDok, "aprovimDokRefuzim", emerPerdoruesiiPlote, null, idkategoria, idPerdoruesi);
        }
       
        /// <summary>
        /// Formati 4: Dërgimi i emailit krijuesit  qe dokumenti u modifikua
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="serverUrl"></param>
        /// <param name="shitjeBlerje"></param>
        /// <param name="ci"></param>
        /// <param name="llojDok"></param>
        /// <param name="nrDok"></param>
        /// <param name="idDok"></param>
        /// <param name="idEtapa"></param>
        /// <param name="idProcesi"></param>
        /// <param name="dtDok"></param>
        /// <param name="emerPerdoruesi"></param>
        /// <returns></returns>
        public static void sendPlainTextEmailKrijuesiPerModifikim(int idNdermarrje, string[] toEmail, string serverUrl, string shitjeBlerje, System.Globalization.CultureInfo ci, string llojDok, string nrDok, int idDok, int idEtapa, int idProcesi, string dtDok, string emerPerdoruesiPlote, int idkategoria, int idPerdoruesi)
        {
            sendPlainTextEmail(idNdermarrje, toEmail, null, serverUrl, shitjeBlerje, ci, llojDok, nrDok, idDok, idEtapa, idProcesi, dtDok, "aprovimDokModifikuarKrijuesit", emerPerdoruesiPlote, null, idkategoria, idPerdoruesi);
        }

        /// <summary>
        /// Formati 5: Dërgimi i emailit perdoruesve paralel per modifikimin e dokumentit
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="serverUrl"></param>
        /// <param name="shitjeBlerje"></param>
        /// <param name="ci"></param>
        /// <param name="llojDok"></param>
        /// <param name="nrDok"></param>
        /// <param name="idDok"></param>
        /// <param name="idEtapa"></param>
        /// <param name="idProcesi"></param>
        /// <param name="dtDok"></param>
        /// <param name="emerPerdoruesi"></param>
        /// <returns></returns>
        public static void sendPlainTextEmailParalelPerModifikim(int idNdermarrje, string[] toEmail, string serverUrl, string shitjeBlerje, System.Globalization.CultureInfo ci, string llojDok, string nrDok, int idDok, int idEtapa, int idProcesi, string dtDok, string emerPerdoruesiPlote, int idkategoria, int idPerdoruesi)
        {
            sendPlainTextEmail(idNdermarrje, toEmail, null, serverUrl, shitjeBlerje, ci, llojDok, nrDok, idDok, idEtapa, idProcesi, dtDok, "aprovimDokModifikuarParaleleve", emerPerdoruesiPlote, null, idkategoria, idPerdoruesi);
        }

        /// <summary>
        /// Formati 6: Dërgimi i emailit krijuesit dhe të deleguarit për delegimin e dokumentit
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="serverUrl"></param>
        /// <param name="shitjeBlerje"></param>
        /// <param name="ci"></param>
        /// <param name="llojDok"></param>
        /// <param name="nrDok"></param>
        /// <param name="idDok"></param>
        /// <param name="idEtapa"></param>
        /// <param name="idProcesi"></param>
        /// <param name="dtDok"></param>
        /// <param name="emerPerdoruesi"></param>
        /// <param name="iDeleguari">emri i te deleguarit</param>
        /// <returns></returns>
        public static void sendPlainTextEmailKrijuesiDeleguesit(int idNdermarrje, string[] toEmail, string serverUrl, string shitjeBlerje, System.Globalization.CultureInfo ci, string llojDok, string nrDok, int idDok, int idEtapa, int idProcesi, string dtDok, string emerPerdoruesiPlote, string iDeleguari, int idkagetoria, int idPerdoruesi)
        {
            sendPlainTextEmail(idNdermarrje, toEmail, null, serverUrl, shitjeBlerje, ci, llojDok, nrDok, idDok, idEtapa, idProcesi, dtDok, "aprovimDokDelegim", emerPerdoruesiPlote, iDeleguari, idkagetoria, idPerdoruesi);
        }

        /// <summary>
        /// //Formati 7: Dërgimi i emailit te roli pas aprovimit nga nje element i rolit
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="serverUrl"></param>
        /// <param name="shitjeBlerje"></param>
        /// <param name="ci"></param>
        /// <param name="llojDok"></param>
        /// <param name="nrDok"></param>
        /// <param name="idDok"></param>
        /// <param name="idEtapa"></param>
        /// <param name="idProcesi"></param>
        /// <param name="dtDok"></param>
        /// <param name="emerPerdoruesi"></param>
        /// <returns></returns>
        public static void sendPlainTextEmailAprovuarRol(int idNdermarrje, string[] toEmail, string serverUrl, string shitjeBlerje, System.Globalization.CultureInfo ci, string llojDok, string nrDok, int idDok, int idEtapa, int idProcesi, string dtDok, string emerPerdoruesiPlote, int idkategoria, int idPerdoruesi)
        {
            sendPlainTextEmail(idNdermarrje, toEmail, null, serverUrl, shitjeBlerje, ci, llojDok, nrDok, idDok, idEtapa, idProcesi, dtDok, "aprovimDokAprovuarRol", emerPerdoruesiPlote, null, idkategoria, idPerdoruesi);
        }

        public static clsMesazh dergoEmailFaturenTeKlietFurnitoret(int idGjuha, int idPerdoruesi, int idNderViti, int idNdermarrje, int idDok, string NrDok, string DtDok, int idRap, int idDesign, string[] toEmail, string llojdok, string subjekt)
        {
            if (String.IsNullOrEmpty(toEmail[0]))
                return new clsMesazh(false, "Emaili nuk mund te dergohet, mungon adresa e emailit ne konfigurimin e klientit/furnitorit!");
            if (idDesign <= 0)
                return new clsMesazh(false, "Emaili nuk mund te dergohet, fatura nuk eshte e lidhur me format printimi!");
            
            string pathRaport;

            try
            {
                pathRaport = ReportFunctions.GetExportedReportPath(idPerdoruesi, idGjuha, idNdermarrje, idNderViti, idDok, idDesign, idRap);
            }
            catch (Exception ex)
            {
                return new clsMesazh(false, ex.Message);
            }

            string body = MessagesResource.Messages["emailBodyMeLlojDok"].Replace("#nrdok", NrDok).Replace("#dtdok", DtDok).Replace("#llojdok", llojdok);

            clsMailSender mail = new clsMailSender(idNdermarrje, subjekt);
            mail.SendPlainAsyncEmail(toEmail, subjekt, MessagesResource.Messages["emailHeader"] + body + MessagesResource.Messages["emailFooter"] + clsNdermarrje.merrPershkrimNdermarrje(idNdermarrje), idNdermarrje, pathRaport, idPerdoruesi);

            return new clsMesazh(true, "Emaili u dergua me sukses ne posten elektronike te klientit/furnitorit!");
        }

        /// <summary>
        /// krijon raportin me id 129 per faturen idFatura, e exporton dhe e dergon me email.
        /// </summary>
        /// <param name="idGjuha"></param>
        /// <param name="ci"></param>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idNderViti"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="idFatura"></param>
        /// <param name="IdKlientFurnitor"></param>
        /// <param name="NrDok"></param>
        /// <param name="DtDok"></param>
        /// <returns></returns>
        public static clsMesazh dergoEmailFaturen(int idGjuha, int idPerdoruesi, int idNderViti, int idNdermarrje, int idFatura, int IdKlientFurnitor, string NrDok, DateTime DtDok)
        {
            
            string adreseKf = DbCore.DbKontabiliteti.clsKlientFurnitor.KtheEmail(IdKlientFurnitor);
            string[] toEmail = { adreseKf };
            if (String.IsNullOrEmpty(toEmail[0]))
                return new clsMesazh(false, "Emaili nuk mund te dergohet, mungon adresa e emailit ne konfigurimin e klientit");

            var rap = new clsRaporti(idGjuha, "porosiVodafone");
            var pdfStream = new MemoryStream();

            ReportFunctions.ExportXtraReportToPdf(idPerdoruesi, idGjuha, idNdermarrje, idNderViti, idFatura, 0, rap.IdRaporti, pdfStream);

            var pdfAttachment = KrijoAttachmentNgaStream(pdfStream, rap.RaportiEmri, AttachmentType.Pdf);

            string subject = MessagesResource.Messages["njoftimDokEmerEmaili"];
            DbCore.clsMailSender mail = new DbCore.clsMailSender(idNdermarrje, subject);
            string body = MessagesResource.Messages["njoftimDokBodyEmail"];
            body = body.Replace("#porosia#", NrDok);
            body = body.Replace("#dtModifikuar#", DtDok.ToShortDateString());
            string headerbody = MessagesResource.Messages["njoftimDokHeaderEmail"];
            string footerbody = MessagesResource.Messages["njoftimDokFooterEmail"];
            mail.SendPlainAsyncEmail(toEmail, subject, headerbody + body + footerbody, idNdermarrje, "", idPerdoruesi, new[] { pdfAttachment });
            return new clsMesazh(true, "Emaili u vendos per dergim");
        }

        public static (clsMesazh, clsMesazh) dergoEmailFaturenNgaPerdoruesiLoguar(int idGjuha, System.Globalization.CultureInfo ci, int idPerdoruesi, int idNderViti, int idNdermarrje, int idFatura, int IdKlientFurnitor, string NrDok, DateTime DtDok, int idDesign, int idKonfigAmbjente, int[] idMags, bool meMag)
        {

            clsMesazh gabimKl = new clsMesazh(false, "");
            clsMesazh gabimMag = new clsMesazh(false, "");
            string adreseEmail = DbCore.DbKontabiliteti.clsKlientFurnitor.KtheEmail(IdKlientFurnitor);
            string[] toEmail = { adreseEmail };

            if (String.IsNullOrEmpty(adreseEmail))
                gabimKl.PershkrimMesazhi = "Emaili nuk mund te dergohet, mungon adresa e emailit ne konfigurimin e klientit/furnitorit!";

            if (meMag)
            {
                string emailMags = (idMags.Length > 0)?clsNjesiAdministrative.ktheEmailSipasIds(string.Join(",", idMags)):"";
                if (!string.IsNullOrEmpty(emailMags))
                    toEmail = (emailMags + ((!String.IsNullOrEmpty(adreseEmail)) ?("," + adreseEmail) :"")).Trim().Split(',');
                else
                    gabimMag.PershkrimMesazhi += " \n Emaili nuk mund te dergohet per magazinen, mungon adresa e emailit ne konfigurimin e saj!";
            }
            else if (gabimKl.PershkrimMesazhi != "")
                return (gabimKl, new clsMesazh(true, ""));

            if (meMag && gabimMag.PershkrimMesazhi !="" && gabimKl.PershkrimMesazhi !="")
                return (new clsMesazh(false, "Emaili nuk mund te dergohet, mungojne adresat e emailit ne konfigurimet e klientit/furnitorit dhe magazines!"), new clsMesazh(true, ""));

            if (idDesign <= 0)
                return (new clsMesazh(false, "Emaili nuk mund te dergohet, fatura nuk eshte e lidhur me format printimi!"), new clsMesazh(true,""));
            int idRap = DbCore.DbShare.clsRaporti.KtheIdRaporti(idGjuha, idDesign);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            string pathRaport;
            try
            {
                pathRaport = ReportFunctions.GetExportedReportPath(idPerdoruesi, idGjuha, idNdermarrje, idNderViti, idFatura, idDesign, idRap);
            }
            catch (Exception ex)
            {
                return (new clsMesazh(false, ex.Message), new clsMesazh(true, ""));            }


            string llojdok = DbCore.DbShare.clsKonfigurimAmbjenti.kthePershkriminSipasID(idKonfigAmbjente);
            string subject = (rm.GetString("emailSubjectFature", ci)).Replace("#llojdok#", llojdok);
            clsMailSender mail = new DbCore.clsMailSender(idNdermarrje, subject);
            string body = rm.GetString("emailBodyFature", ci).Replace("#nrDok#", NrDok).Replace("#dtDok#", DtDok.ToShortDateString());
            if (!mail.SendPlainAsyncEmail(toEmail, subject, rm.GetString("emailHeader", ci) + body + rm.GetString("emailFooter", ci) + clsNdermarrje.merrPershkrimNdermarrje(idNdermarrje), idNdermarrje, pathRaport, idPerdoruesi))
                return (new clsMesazh(false, "Emaili nuk u dergua ne posten elektronike."), new clsMesazh(true, ""));
            
            if (meMag && gabimMag.PershkrimMesazhi == "" && gabimKl.PershkrimMesazhi == "") 
                    return (new clsMesazh(false, ""), new clsMesazh(true, "Emaili u dergua me sukses ne postat elektronike te klientit/furnitorit dhe magazines!"));
            else if (meMag)
            {
                if (gabimKl.PershkrimMesazhi != "")
                    return (gabimKl, new clsMesazh(true, "Emaili u dergua me sukses ne posten elektronike te magazines!"));
                else
                    return (gabimMag, new clsMesazh(true, "Emaili u dergua me sukses ne posten elektronike te klientit/furnitorit!"));
            }
            else
                return (new clsMesazh(false, ""), new clsMesazh(true, "Emaili u dergua me sukses ne posten elektronike te klientit/furnitorit!"));

        }

        public static clsMesazh dergoEmailFaturenNgaPerdoruesiLoguarNgaMagazina(int idGjuha, System.Globalization.CultureInfo ci, int idPerdoruesi, int idNderViti, int idNdermarrje, int idFatura, int IdKlientFurnitor, string NrDok, DateTime DtDok, int idDesign, int idKonfigAmbjente, string emailMagazina, string kodiMag, string pershkrimiDok, string virtualPath = "", bool dergoMeAttachDheDokArkives = false, colArkiva arkiva = null)
        {
            List<string> toEmailList = new List<string>();
            string emailKF = "";
            if (IdKlientFurnitor != 0)
                emailKF = DbCore.DbKontabiliteti.clsKlientFurnitor.KtheEmail(IdKlientFurnitor);
            if (string.IsNullOrEmpty(emailKF) && string.IsNullOrEmpty(emailMagazina))
                return new clsMesazh(false, "Emaili nuk mund te dergohet, mungon adresa e emailit ne konfigurimin e klientit/furnitorit dhe te magazines!");
            if (!string.IsNullOrEmpty(emailKF))
            {
                foreach (string email in emailKF.Split(';'))
                    ListExtensions.AddIfNotExists(toEmailList, email);
            }
            if (!string.IsNullOrEmpty(emailMagazina))
            {
                foreach (string email in emailMagazina.Split(';'))
                    ListExtensions.AddIfNotExists(toEmailList, email);
            }
            int idRap = DbCore.DbShare.clsRaporti.KtheIdRaporti(idGjuha, idDesign);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));


            var pathRaport = ReportFunctions.GetExportedReportPath(idPerdoruesi, idGjuha, idNdermarrje, idNderViti,
                idFatura, idDesign, idRap);

            string llojdok = DbCore.DbShare.clsKonfigurimAmbjenti.kthePershkriminSipasID(idKonfigAmbjente);
            string subject = (rm.GetString("emailSubjectDokMag", ci)).Replace("#llojDok#", llojdok).Replace("#nrDok#", NrDok);
            if (!String.IsNullOrEmpty(kodiMag))
                subject = subject.Replace("#kodMag#", kodiMag);
            else
                subject = subject.Split(',')[0];
            DbCore.clsMailSender mail = new DbCore.clsMailSender(idNdermarrje, subject);
            string body = rm.GetString("emailBodyDokMag", ci).Replace("#nrDok#", NrDok).Replace("#dtDok#", DtDok.ToShortDateString()).Replace("#pershkrimDok#", pershkrimiDok).Replace("#enter#", "<br />");
            if (dergoMeAttachDheDokArkives)
                mail.SendPlainAsyncEmailAttach(toEmailList.ToArray(), subject, rm.GetString("emailHeader", ci) + body + rm.GetString("emailFooter", ci) + clsNdermarrje.merrPershkrimNdermarrje(idNdermarrje), idNdermarrje, pathRaport, idPerdoruesi, arkiva, virtualPath);
            else
                mail.SendPlainAsyncEmailAttach(toEmailList.ToArray(), subject, rm.GetString("emailHeader", ci) + body + rm.GetString("emailFooter", ci) + clsNdermarrje.merrPershkrimNdermarrje(idNdermarrje), idNdermarrje, pathRaport, idPerdoruesi);
            return new clsMesazh(true, "Emaili u dergua me sukses ne posten elektronike te klientit/furnitorit!");
        }

        public static clsMesazh riDergoEmailFaturen(int idEmail)
        {
            clsEmail emailPerDergim = new clsEmail(idEmail);
            DbCore.clsMailSender mail = new DbCore.clsMailSender(emailPerDergim.IdNdermarrje, emailPerDergim.SubjektEmail);
            if (String.IsNullOrEmpty(emailPerDergim.Pathi))
                return new clsMesazh(false, "Kujdes! Nuk ekziston asnje file per attach ne email");
            mail.ReSendPlainAsyncEmailAttach(emailPerDergim.IdEmail, emailPerDergim.MarresEmail.Split(';'), emailPerDergim.SubjektEmail, emailPerDergim.TrupEmail, emailPerDergim.IdNdermarrje, emailPerDergim.Pathi, emailPerDergim.IdPerdoruesi);
            return new clsMesazh(true, "Emaili u dergua me sukses!");
        }

        /// <summary>
        /// Dergim me email te rezervimit qe ben shitesi per nje artikull vodafone One kur dyqani nuk ka gjendje
        /// </summary>
        /// <param name="ci"></param>
        /// <param name="idNdermarje"></param>
        /// <param name="idPerdoruesi"></param>
        /// <param name="oColTrupiShitje"></param>
        /// <param name="nrdok"></param>
        /// <param name="dateDok"></param>
        /// <returns></returns>
        public static clsMesazh dergoEmailRezervimVodOne(int idNdermarje, int idPerdoruesi, DbCore.DbRegjistrim.colTrupiShitje oColTrupiShitje, String nrdok, DateTime dateDok, int idKonfigAmbjente, bool dergoEmailVfOne, bool dergoEmailPorosi)
        {
            try
            {
                List<string> toEmailList = new List<string>();
                string subject = MessagesResource.Messages["DokEmerEmailVFONE"];
                if (dergoEmailVfOne)
                {
                    int IdPerdoruesiTeKushtiPEMAIL = clsKusht.kthevlereSipasKushtitDheIdKonfig(idKonfigAmbjente, "PEMAIL");
                    if (IdPerdoruesiTeKushtiPEMAIL == 0)
                        return new clsMesazh(false, "Emaili nuk mund te dergohet, duhet caktuar perdoruesi ku do dergohet te kushti Perdorues per email!");
                    string toEmailPerdorues = clsPerdorues.merrEmail(IdPerdoruesiTeKushtiPEMAIL);
                    if (String.IsNullOrEmpty(toEmailPerdorues))
                        return new clsMesazh(false, "Emaili nuk mund te dergohet, mungon adresa e-mail per perdoruesin e zgjedhur te kushti Perdorues per email!");
                    foreach (string email in toEmailPerdorues.Split(';'))
                        ListExtensions.AddIfNotExists(toEmailList, email);
                }
                if (dergoEmailPorosi)
                {
                    int IdPerdoruesiTeKushtiMAILVODAFONE = clsKusht.kthevlereSipasKushtitDheIdKonfig(idKonfigAmbjente, "MAILVODAFONE");
                    int IdPerdoruesiTeKushtiMAILDEALER = clsKusht.kthevlereSipasKushtitDheIdKonfig(idKonfigAmbjente, "MAILDEALER");
                    if (IdPerdoruesiTeKushtiMAILVODAFONE == 0 && IdPerdoruesiTeKushtiMAILDEALER == 0)
                        return new clsMesazh(false, "Emaili nuk mund te dergohet, duhet caktuar perdoruesi ku do dergohet email per porosine te kushtet Dergo email per porosi te user vodafone ose dealer!");
                    string toEmailPerdoruesVodafone = clsPerdorues.merrEmail(IdPerdoruesiTeKushtiMAILVODAFONE);
                    string toEmailPerdoruesDealer = clsPerdorues.merrEmail(IdPerdoruesiTeKushtiMAILDEALER);
                    if (string.IsNullOrEmpty(toEmailPerdoruesVodafone) && string.IsNullOrEmpty(toEmailPerdoruesDealer))
                        return new clsMesazh(false, "Emaili nuk mund te dergohet, mungon adresa e-mail per perdoruesin e zgjedhur te kushtet Dergo email per porosi te user vodafone dhe dealer!");
                    if (!string.IsNullOrEmpty(toEmailPerdoruesVodafone))
                    {
                        foreach (string email in toEmailPerdoruesVodafone.Split(';'))
                            ListExtensions.AddIfNotExists(toEmailList, email);
                    }
                    if (!string.IsNullOrEmpty(toEmailPerdoruesDealer))
                    {
                        foreach (string email in toEmailPerdoruesDealer.Split(';'))
                            ListExtensions.AddIfNotExists(toEmailList, email);
                    }
                    subject = clsKonfigurimAmbjenti.kthePershkriminSipasID(idKonfigAmbjente);
                }
                string emriPerdorues = "", mbiemriPerdorues = ""; String kodArtikulli, PershkrimArtikulli;
                //clsKonfigurimEmail konfigEmailMeme = new clsKonfigurimEmail(clsNdermarrje.ktheIdNdermarrjeMeme(idNdermarje));
                String emriNder = clsNdermarrje.merrPershkrimNdermarrje(idNdermarje);
                //string toEmailMeme = konfigEmailMeme.DergoEmailNga;
                clsDatabaseAdmin db = new clsDatabaseAdmin();
                System.Data.DataRow dr = db.merrPerdorues(idPerdoruesi);
                if (dr != null && dr["PERDORUESEMRI"] != null)
                    emriPerdorues = dr["PERDORUESEMRI"].ToString();
                if (dr != null && dr["PERDORUESMBIEMRI"] != null)
                    mbiemriPerdorues = dr["PERDORUESMBIEMRI"].ToString();
                kodArtikulli = oColTrupiShitje[0].Kodi; //merret vetem artikulli i pare nga trupi sepse do behet nje kerkese vecante per cdo artikull
                PershkrimArtikulli = oColTrupiShitje[0].Pershkrimi;
                DbCore.DbRegjistrim.clsNjesiAdministrative magazina = new DbCore.DbRegjistrim.clsNjesiAdministrative(oColTrupiShitje[0].IdMagazina, idPerdoruesi);
                string shitesi = emriPerdorues + " " + mbiemriPerdorues;
                clsMailSender mailSender = new clsMailSender(idNdermarje, subject);
                string headerbody = MessagesResource.Messages["njoftimDokHeaderEmail"];
                headerbody = headerbody.Replace("<br />", Environment.NewLine);
                string footerbody = MessagesResource.Messages["njoftimDokFooterEmail"];
                footerbody = footerbody.Replace("<br />", Environment.NewLine);
                string body = MessagesResource.Messages["njoftimDokBodyEmailVFONE"];
                body = body.Replace("#llojDok#", (dergoEmailPorosi ? subject : "Vodafone ONE"));
                body = body.Replace("#PershkNderm#", emriNder);
                body = body.Replace("#dyqani#", magazina.Pershkrimi);
                body = body.Replace("#kodDyqani#", magazina.Kodi);
                body = body.Replace("#NrDok#", nrdok);
                body = body.Replace("#DtDok#", dateDok.ToShortDateString());
                body = body.Replace("#shitesi#", shitesi);
                body = body.Replace("#KodiProd#", kodArtikulli);
                body = body.Replace("#Produkti#", PershkrimArtikulli);
                body = body.Replace("#Enter#", Environment.NewLine);
                mailSender.SendPlainAsyncEmailMultiple(toEmailList.ToArray(), subject, headerbody + body + footerbody, idNdermarje,idPerdoruesi, true);
                return new clsMesazh(true, "Emaili u vendos per dergim");
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, $"dergoEmailRezervimVodOne idNdermarje: {idNdermarje}, perdoruesi: {idPerdoruesi}, nrdok: {nrdok}, dtdok: {dateDok}, idKonfigAmbjente: {idKonfigAmbjente}, dergoEmailVfOne : {dergoEmailVfOne}, dergoEmailPorosi:{dergoEmailPorosi} ");
                return new clsMesazh(false, ex.Message);
            }
        }
        
        public static clsMesazh dergoEmailFailAuthPeopleFinder(string mesazhiFail)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            int idNdermarje = clsNdermarrje.ktheIdNdermPareNeListeSipasLicences();
            if (idNdermarje == 0)
                return new clsMesazh(false, "Nuk ekziston konfigurimi i email për asnjë nga ndërmarrjet e licencës. Ju lutemi, kontakoni me administratorin!");
            clsMailSender mailSender = new clsMailSender(idNdermarje, "People Finder");
            string subject = "AUTHENTICATION PEOPLE FINDER ERROR";
            string headerbody = rm.GetString("njoftimPeopleFinderHeaderEmail", new CultureInfo("sq-AL"));
            //headerbody = headerbody.Replace("<br />", Environment.NewLine);
            string footerbody = rm.GetString("njoftimPeopleFinderFooterEmail", new CultureInfo("sq-AL"));
            //footerbody = footerbody.Replace("<br />", Environment.NewLine);
            string toEmail = System.Web.Configuration.WebConfigurationManager.AppSettings["adreseDergimiVodafoneAdmin"].ToString();
            mailSender.SendPlainAsyncEmail(toEmail.Split(';'), subject, headerbody + mesazhiFail + footerbody);
            return new clsMesazh(true, "Emaili u vendos per dergim");
        }

        public static clsMesazh dergoEmailFailDergimMesazhPeopleFinder(string mesazhiFail)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            int idNdermarje = clsNdermarrje.ktheIdNdermPareNeListeSipasLicences();
            if (idNdermarje == 0)
                return new clsMesazh(false, "Nuk ekziston konfigurimi i email për asnjë nga ndërmarrjet e licencës. Ju lutemi, kontakoni me administratorin!");
            clsMailSender mailSender = new clsMailSender(idNdermarje, "People Finder");
            string subject = "DERGIM ME SMS PEOPLE FINDER ERROR";
            string headerbody = rm.GetString("njoftimPeopleFinderHeaderEmail", new CultureInfo("sq-AL"));
            //headerbody = headerbody.Replace("<br />", Environment.NewLine);
            string footerbody = rm.GetString("njoftimPeopleFinderFooterEmail", new CultureInfo("sq-AL"));
            //footerbody = footerbody.Replace("<br />", Environment.NewLine);
            string toEmail = System.Web.Configuration.WebConfigurationManager.AppSettings["adreseDergimiVodafoneAdmin"].ToString();
            mailSender.SendPlainAsyncEmail(toEmail.Split(';'), subject, headerbody + mesazhiFail + footerbody);
            return new clsMesazh(true, "Emaili u vendos per dergim");

        }

        /// <summary>
        /// Dergon me email raportet te plotesuara te tabela T_EMAILRAPORT per te gjitha adresat e email te specifikuara ne fushen EMAIL 
        /// </summary>
        /// <param name="idGjuha"></param>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idNderViti"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="idFatura"></param>
        /// <returns></returns>
        public static clsMesazh dergoEmailRaportet(int idGjuha, int idPerdoruesi, int idNderViti, int idNdermarrje)
        {
            try
            {
                using (clsDatabaseShare db = new clsDatabaseShare())
                {
                    DataTable dt = db.ktheRaportetDheAdresatEmail();

                    DateTime dataPerUpdate = DateTime.Now;
                    string idRaportePerTuUpdituar = "";

                    if (dt != null && dt.Rows != null && dt.Rows.Count == 0)
                    {
                        string err1 = "Nuk ka asnje raport per t'u derguar me email!";
                        logu.Error(err1);
                        return new clsMesazh(false, err1);
                    }
                    foreach (DataRow row in dt.Rows)
                    {
                        CultureInfo ci = MessagesResource.KtheCultureInfo(idGjuha);
                        clsRaporti rap = new clsRaporti(idGjuha, Convert.ToInt32(row["IDRAPORTI"]));
                        var rm = new ResourceManager("Resources.Strings", Assembly.Load("App_GlobalResources"));
                        clsRaportDesign design = new clsRaportDesign();
                        design.merrSipasNdermarjedheRaport(idNdermarrje, rap.IdRaporti);
                        string orientimi = "";
                        if (design.FileName != "")
                            orientimi = clsRaportDesign._rap_landscape;
                        else if (design.FileNamePortrait != "")
                            orientimi = clsRaportDesign._rap_portrait;

                        if (!DateTime.TryParse(row["DATE_DERGIMI"].ToString(), out var date)) date = new DateTime(1900, 1, 1);

                        var raporti = ReportFunctions.CreateXtraReportInvoice(orientimi, idPerdoruesi, idNderViti, idNdermarrje, 0, design.IdRaportDesign, date, idGjuha, Convert.ToInt32(row["IDRAPORTI"]));

                        string[] toEmail = row["EMAIL"].ToString().Split(';');
                        if (String.IsNullOrEmpty(toEmail[0]))
                        {
                            logu.Error("Emaili nuk mund te dergohet per raportin me id {0}, mungon adresa e emailit!", rap.IdRaporti);
                            continue;
                        }
                        if (rap.RaportiEmriReal == "ArketimetOrare" && (((DataSet)raporti.DataSource).Tables[0].Rows.Count == 0 || !clsFunksione.EshteOrarPune(clsServerConfiguration.LexoKonfigurimSipasKey<string>(ServerKonfigKey.OrariPunesVod), dataPerUpdate)))
                            continue;
                        Attachment excelAttach;
                        var excelStream = new MemoryStream();
                        raporti.ExportToXlsx(excelStream);
                        excelAttach = KrijoAttachmentNgaStream(excelStream, rap.RaportiEmri, AttachmentType.Excel);

                        string subject = rm.GetString("subjectEmailRaport", ci);
                        clsMailSender mail = new clsMailSender(idNdermarrje, subject);
                        string body = rm.GetString("bodyEmailRaport", ci);
                        mail.SendAsyncEmailMultipleAttach(toEmail, subject, body, idNdermarrje, idPerdoruesi, new[] { excelAttach });
                        idRaportePerTuUpdituar = $"{idRaportePerTuUpdituar}{rap.IdRaporti};";
                    }
                    db.updateDateDergimiPerEmaileRaporti(dataPerUpdate, idRaportePerTuUpdituar.Trim(';'));
                }
                return new clsMesazh(true, "Emaili u vendos per dergim");
            }
            catch (Exception ex)
            {
                logu.Error(ex.Message);
                return new clsMesazh(false, "Ndodhi nje gabim gjate dergimit te raporteve me email!");
            }
        }
        
        /// <summary>
         /// dergon me email nje link verifikimi per kerkesen e resetimit te passwordit qe ka bere perdoruesi
         /// </summary>
         /// <param name="username">emri i perdoruesit qe do i resetohet fjalekalimi</param>
         /// <param name="request"></param>
         /// <returns></returns>
        public static clsMesazh DergoEmailVerificationLink(clsPerdorues user, HttpRequest request, clsPunonjes punonjes, int idgjuha)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            string username = ""; String email = "";string url;
            System.Globalization.CultureInfo ci = MessagesResource.KtheCultureInfo(idgjuha);
            string linku = "";
            bool eshtePunonjes = punonjes != null;
            if (eshtePunonjes)
            {
                username = punonjes.Username;
                email = punonjes.Email;
                url = "E-PaySlip/NdryshoFjalekalim.aspx?em=";
            }
            else
            {
                username = user.PerdoruesUsername;
                email = user.PerdoruesEmail;
                //   gjuha = user.IdGjuha;
                url = "NdryshimFjalekalimi.aspx?em=";
            }

            string encQS = RijndaelSimple.EncryptImbString("idGjuha=" + idgjuha + "&username=" + username + "&expirationDate=" + DateTime.Now.ToString(new CultureInfo("en-us")));
            linku = DbCore.clsFunksione.ktheServerUrl(request) + url + HttpUtility.UrlEncode(encQS);
            //DataRow[] rreshtaPerdorues = dbAdmin.ktheUserNgaLogin(username).Select("PERDORUESUSERNAME = '" + username + "'");
            //DataRow rreshtiUserNgaLogin = rreshtaPerdorues.Length == 1 ? rreshtaPerdorues[0] : null;
            //if (rreshtiUserNgaLogin == null)
            //   return new clsMesazh(false, "Nuk ekziston përdoruesi me këtë emër!");
            //clsPerdorues user = new clsPerdorues();
            //user.mbushPerdorues(rreshtiUserNgaLogin);
            //clsKonfigurimeFjalekalimi konfigPass = new clsKonfigurimeFjalekalimi(user.IdPerdorues);
            //if (konfigPass.RuajHistorikunPass)
            //{
            //}
            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(email))
                return new clsMesazh(false, rm.GetString("lblFjalekalimiNukMundTeRivendoset", ci) + Environment.NewLine + rm.GetString("lblKontaktAdm", ci));


            string[] toEmail = { email }; //emaili i perdoruesit qe ka bere kerkesen

            //linku qe i bashkangjitet ne email perdoruesit, i cili nqs klikohet verifikon qe ka qene vet perdoruesi qe ka bere kerkesen per resetim passwordi dhe jo ndonje perdorues tjeter
            DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            dbAdmin.shtoKerkeseResetPass(username, encQS);
            int idNdermarje = 0;
            try
            {

                string adresaip = HttpContext.Current.Request.UserHostAddress;
                string subject = rm.GetString("ResetPasswordSubjectEmail", ci);
                string body = rm.GetString("ResetPasswordVerifikimKerkeseEmail", ci);
                body = body.Replace("#enter#", "<br />");
                body = body.Replace("#bold#", "<b>");
                body = body.Replace("#/bold#", "</b>");
                body = body.Replace("#linku#", "<a href=" + linku + ">" + linku + "</a>");
                body = body.Replace("#adresaIP#", adresaip);
                body = body.Replace("#Data#", DateTime.Now.ToString());
                string headerbody = rm.GetString("njoftimDokHeaderEmail", ci);
                string footerbody = rm.GetString("njoftimDokFooterEmail", ci) + "<br />Alpha Web";
                string htmlBody = headerbody + body + footerbody;
                /*merret id ndermarrje nga e cila do merret konfigurimi i email. Kur eshte punonjes do merret ndermarrja se ciles i perket punonjesi, kur eshte perdorues do merret ndermarrja e pare ne listen e ndermarrjeve qe i perkasin licences se perdoruesit qe ka harruar fjalekalimin. Kur nuk ekziston konfigurimi per ndermarrjen ne fjale, ath merret idndermarrje -1. */
                idNdermarje = clsNdermarrje.ktheIdNdermPareNeListeSipasIdPerdoruesit(eshtePunonjes ? punonjes.IdPunonjes : user.IdPerdorues, eshtePunonjes);
                if (idNdermarje == 0)
                    return new clsMesazh(false, rm.GetString("nukEkzistonKonfigurimEmailNdermarrjeLicense", ci));
                DbCore.clsMailSender mail = new DbCore.clsMailSender(idNdermarje, subject);
                mail.SendPlainAsyncEmail(toEmail, subject, htmlBody, username);
                return new clsMesazh(true, rm.GetString("msgDergimEmail", ci));
            }
            catch (Exception ex)
            {

                NLog.LogManager.GetCurrentClassLogger().Error(ex, "Dergimi i linkut te resetimit te passwordit per perdoruesin username={0}, konfigurimi i nderm={1}", username, idNdermarje);
                return new clsMesazh(false, rm.GetString("ndodhiNjeGabimGjateDergimitTeTeDhenave", ci));
            }
        }

        public static clsMesazh verifikoLinkResetPassword(string url, string myQueryStrEm, ref string username, ref int idGjuha, bool punonjes = false)
        {
            try
            {
                string decryptedQuery = RijndaelSimple.DecryptImbString(myQueryStrEm);//dekriptohet linku qe ehste klikuar te email i kerkeses per resetim
                System.Collections.Specialized.NameValueCollection myQuery = HttpUtility.ParseQueryString(decryptedQuery);
                if (!String.IsNullOrEmpty(myQuery["idGjuha"]))
                    idGjuha = Convert.ToInt32(myQuery["idGjuha"]);//merret id gjuha nga querystringu u linkut
                ResourceManager rm = new ResourceManager("Resources.Strings", Assembly.Load("App_GlobalResources"));
                CultureInfo ci = MessagesResource.KtheCultureInfo(idGjuha);
                Dictionary<string, int> nrDergimeEmail = (Dictionary<string, int>)HttpContext.Current.Application["maxNrDergimEmailPerUser"];
                if (String.IsNullOrEmpty(myQuery["username"]) || myQuery["expirationDate"] == "")
                {
                    NLog.LogManager.GetCurrentClassLogger().Info($"HIGH SECURITY ALERT! verifikoLinkResetPassword(myQueryStrEm:{myQueryStrEm}, username: {username}, eshtePunonjes: {punonjes})" + "; Ip address: " + HttpContext.Current.Request.UserHostAddress + "; Url: " + url + Environment.NewLine + "Linku nuk permban username ose expiration date!");
                    return new clsMesazh(false, rm.GetString("linkJoIVlefshem", ci));
                }
                if (nrDergimeEmail.ContainsKey(myQuery["username"]))
                    nrDergimeEmail[myQuery["username"]] = 0;
                TimeSpan diffTime = DateTime.Now - Convert.ToDateTime(myQuery["expirationDate"], new System.Globalization.CultureInfo("en-us"));
                int diteTeToleruara = Convert.ToInt32(System.Web.Configuration.WebConfigurationManager.AppSettings["skadoLinkEmail"]);
                if (diffTime.TotalDays > diteTeToleruara)
                {
                    NLog.LogManager.GetCurrentClassLogger().Info($"LOW SECURITY ALERT! verifikoLinkResetPassword(myQueryStrEm:{myQueryStrEm}, username: {username}, eshtePunonjes: {punonjes})" + "; Ip address: " + HttpContext.Current.Request.UserHostAddress + "; Url: " + url + Environment.NewLine + "Linku eshte i pavlefshem sepse ka kaluar afatin prej " + diteTeToleruara + "ditesh! Resetoni serish fjalekalimin!");
                    return new clsMesazh(false, rm.GetString("linkJoIVlefshem", ci));
                }
                DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
                DataRow[] rreshtaPerdorues;
                string perdorues;
                if (punonjes)
                {
                    DbCore.DbListPagesat.clsDatabazeListPagesa listpagesa = new DbCore.DbListPagesat.clsDatabazeListPagesa();

                    perdorues = new clsPunonjes(myQuery["username"].ToString()).Username;
                }
                else
                {
                    /* ne rastin kur eshte perdorues, ne momentin qe behet kerkesa per resetim fjalekalimi, ath behet nje update ne tabelen T_PERDORUESI te fusha KerkeseResetPass per ate username duke vendosur linkun qe i dergohet perdoruesit me email, dhe kur klikohet ky link, do verifikohet nese ky link nuk eshte klikuar me pare. Kete e verifikojme nqs fusha  KerkeseResetPass per kete username perdoruesi eshte njesoj me linkun qe na vjen, perndryshe ose linku eshte modifikuar manualisht ne url, ose eshte klikuar me pare nga perdoruesi. Ne rastin kur linku klikohet nga perdoruesi behet update fusha  KerkeseResetPass me tekstin "expired". Shiko proceduren prc_T_PERDORUESI_shtoKerkeseResetPass */
                    var dtUsers = dbAdmin.ktheUserNgaLogin(myQuery["username"]);
                    rreshtaPerdorues = dtUsers.Select("PERDORUESUSERNAME = '" + myQuery["username"] + "' AND KerkeseResetPass = '" + myQueryStrEm + "'");
                    perdorues = rreshtaPerdorues.Length == 1 ? rreshtaPerdorues[0]["PERDORUESUSERNAME"].ToString() : null;
                }
                if (string.IsNullOrEmpty(perdorues))
                {
                    NLog.LogManager.GetCurrentClassLogger().Info($"MEDIUM SECURITY ALERT! verifikoLinkResetPassword(myQueryStrEm:{myQueryStrEm}, username: {username}, eshtePunonjes: {punonjes})" + "; Ip address: " + HttpContext.Current.Request.UserHostAddress + "; Url: " + url +
                         Environment.NewLine + "Nuk ka asnje " + (punonjes ? "punonjes" : "perdorues") + " me kete username!");
                    return new clsMesazh(false, rm.GetString("linkJoIVlefshem", ci));
                }
                username = myQuery["username"];
                //linku eshte klikuar dhe ne kete moment behet i pavlefshem, prandaj behet update fusha  KerkeseResetPass me tekstin "expired"
                dbAdmin.shtoKerkeseResetPass(myQuery["username"], "expired");
                return new clsMesazh(true);
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, $"HIGH SECURITY ALERT! verifikoLinkResetPassword(myQueryStrEm:{myQueryStrEm}, username: {username}, eshtePunonjes: {punonjes})" + "; Ip address: " + HttpContext.Current.Request.UserHostAddress + "; Url: " + url);
                return new clsMesazh(false, "Linku i resetimit te fjalekalimit nuk eshte i sakte!");

            }
        }

        /// <summary>
        /// dergon me email fjalekalimin e ri perdoruesit dhe e reseton ate
        /// </summary>
        /// <param name="myQueryStrEm">stringu i enkriptuar qe eshte pjese e linkut te verifikimit te kerkeses per resetim passwordi</param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static clsMesazh DergoEmailNewPassword(String myQueryStrEm, HttpRequest request, bool punonjes)
        {
            string decryptedQuery = RijndaelSimple.DecryptImbString(myQueryStrEm);
            System.Collections.Specialized.NameValueCollection myQuery = HttpUtility.ParseQueryString(decryptedQuery);
            Dictionary<string, int> nrDergimeEmail = (Dictionary<string, int>)HttpContext.Current.Application["maxNrDergimEmailPerUser"];
            if (nrDergimeEmail.ContainsKey(myQuery["username"]))
            {
                nrDergimeEmail[myQuery["username"]] = 0;
            }
            int diteTeToleruara = Convert.ToInt32(System.Web.Configuration.WebConfigurationManager.AppSettings["skadoLinkEmail"]);
            if (myQuery["username"] == "" || myQuery["expirationDate"] == "")
                return new clsMesazh(false, "Ky link nuk është i saktë!");
            TimeSpan diffTime = DateTime.Now - Convert.ToDateTime(myQuery["expirationDate"], new System.Globalization.CultureInfo("en-us"));
            if (diffTime.TotalDays > diteTeToleruara)
                return new clsMesazh(false, "Linku që jeni përpjekur të përdorni nuk është i vlefshëm!");
            int idPerdorues; string fjalekalimi = ""; string fjalekalimiHashuar; DataRow[] rreshtaPerdorues; DataRow rreshtiUserNgaLogin; string username;
            string email = ""; bool PasswordIPerkohshem; string url;
            int gjuha = mySessionObjects.ktheGjuhe(System.Web.HttpContext.Current.Session);

            DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            if (!punonjes)
            {
                rreshtaPerdorues = dbAdmin.ktheUserNgaLogin(myQuery["username"]).Select("PERDORUESUSERNAME = '" + myQuery["username"] + "' AND KerkeseResetPass = '" + myQueryStrEm + "'");
                rreshtiUserNgaLogin = rreshtaPerdorues.Length == 1 ? rreshtaPerdorues[0] : null;
                if (rreshtiUserNgaLogin == null)
                    return new clsMesazh(false, "Ky link nuk është i saktë!");
                clsPerdorues user = new clsPerdorues();
                user.mbushPerdorues(rreshtiUserNgaLogin);
                //user.mbushPerdorues(rreshtiUserNgaLogin, false);
                if (user.PerdoruesEmail == null)
                    return new clsMesazh(false, "Fjalëkalimi nuk mund të rivendoset, mungon adresa e emailit në konfigurimin e përdoruesit." + Environment.NewLine + "Ju lutemi, kontaktoni me administratorin!");
                idPerdorues = user.IdPerdorues;
                DbCore.DbAdmin.clsKonfigurimeFjalekalimi konfig = new DbCore.DbAdmin.clsKonfigurimeFjalekalimi(idPerdorues);
                fjalekalimi = PasswordHelper.GjeneroPassword(konfig.GjatesiaMinPassword, konfig.SpecialChars, konfig.UppercaseChars, konfig.NumbersChars);
                email = user.PerdoruesEmail;
                fjalekalimiHashuar = PasswordHelper.HashLogin(user.PerdoruesUsername, fjalekalimi);
                username = user.PerdoruesUsername;
                PasswordIPerkohshem = user.PasswordIPerkohshem;
                //gjuha = user.IdGjuha;
            }
            else
            {
                if (string.IsNullOrEmpty(new clsPunonjes(myQuery["username"]).Username))
                    return new clsMesazh(false, "Ky link nuk është i saktë!");
                clsPunonjes p = new clsPunonjes(myQuery["username"]);
                if (p.Email == null)
                    return new clsMesazh(false, "Fjalëkalimi nuk mund të rivendoset, mungon adresa e emailit në konfigurimin e përdoruesit." + Environment.NewLine + "Ju lutemi, kontaktoni me administratorin!");

                idPerdorues = p.IdPunonjes;
                DbCore.DbAdmin.clsKonfigurimeFjalekalimi konfig = new DbCore.DbAdmin.clsKonfigurimeFjalekalimi(idPerdorues, true);
                fjalekalimi = PasswordHelper.GjeneroPassword(konfig.GjatesiaMinPassword, konfig.SpecialChars, konfig.UppercaseChars, konfig.NumbersChars);
                email = p.Email;
                fjalekalimiHashuar = PasswordHelper.HashLogin(p.Username, fjalekalimi);
                username = p.Username;
                {
                    PasswordIPerkohshem = false;
                    //gjuha = 0;
                }
            }
            string[] toEmail = { email };

            try
            {
                dbAdmin.beginTransaksion();


                url = punonjes ? Paths.loginPathEpaySlip : Paths.defaultLoginPath;
                DbCore.DbAdmin.clsPerdorues.modifikoPassword(idPerdorues, fjalekalimiHashuar, dbAdmin, PasswordIPerkohshem, idPerdorues, punonjes);
                dbAdmin.shtoKerkeseResetPass(username, "expired");
                dbAdmin.commitTransaksion();
                ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                System.Globalization.CultureInfo ci = MessagesResource.KtheCultureInfo(gjuha);
                string subject = rm.GetString("ResetPasswordSubjectEmail", ci);
                string adresaip = HttpContext.Current.Request.UserHostAddress;
                string linku = System.Web.HttpContext.Current.Server.HtmlEncode(DbCore.clsFunksione.ktheServerUrl(request) + url);
                string body = rm.GetString("ResetPasswordBodyEmail", ci);
                body = body.Replace("#enter#", "<br />");
                body = body.Replace("#bold#", "<b>");
                body = body.Replace("#/bold#", "</b>");
                body = body.Replace("#newPass#", fjalekalimi);
                body = body.Replace("#username#", username);
                body = body.Replace("#linku#", "<a href=" + linku + ">" + linku + "</a>");
                string headerbody = rm.GetString("njoftimDokHeaderEmail", ci);
                string footerbody = rm.GetString("njoftimDokFooterEmail", ci) + "<br />Alpha Web";
                int idNdermarje = clsNdermarrje.ktheIdNdermPareNeListeSipasIdPerdoruesit(idPerdorues, punonjes);
                if (idNdermarje == 0)
                    return new clsMesazh(false, rm.GetString("nukEkzistonKonfigurimEmailNdermarrjeLicense", ci));
                DbCore.clsMailSender mail = new DbCore.clsMailSender(idNdermarje, subject);
                mail.SendPlainAsyncEmail(toEmail, subject, headerbody + body + footerbody);
                return new clsMesazh(true, "Një email me fjalëkalimin e ri po dërgohet në postën tuaj elektronike!");
            }
            catch (Exception)
            {
                dbAdmin.rollbackTransaksion();
                return new clsMesazh(false, "Ndodhi një gabim gjatë dërgimit të fjalëkalimit të ri në postën tuaj elektronike. Ju lutemi, provoni përsëri!");
            }
        }

        public static clsMesazh DergoEmailFjalekaliminEGjeneruar(string emailPerdoruesit, string username, string passwordiIRi, int idPerdorues, DbCore.DbAdmin.clsDatabaseAdmin dbAdmin)
        {
            if (String.IsNullOrEmpty(emailPerdoruesit))
                return new clsMesazh(false, "Fjalëkalimi nuk mund të rivendoset, mungon adresa e emailit në konfigurimin e përdoruesit." + Environment.NewLine + "Ju lutemi, kontaktoni me administratorin!");
            string[] toEmail = { emailPerdoruesit };
            try
            {
                ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                System.Globalization.CultureInfo ci = mySessionObjects.ktheCultureInfo(System.Web.HttpContext.Current.Session);
                string subject = rm.GetString("subjectEmailGeneratedPassword", ci);
                string adresaip = HttpContext.Current.Request.UserHostAddress;
                string body = rm.GetString("msgBodyDergoEmailGjenerimPassword", ci);
                body = body.Replace("#enter#", "<br />");
                body = body.Replace("#newPass#", passwordiIRi);
                body = body.Replace("#username#", username);
                string headerbody = rm.GetString("njoftimDokHeaderEmail", ci);
                string footerbody = rm.GetString("njoftimDokFooterEmail", ci) + "<br />Alpha Web";
                int idNdermarje = clsNdermarrje.ktheIdNdermPareNeListeSipasIdPerdoruesit(idPerdorues, false, dbAdmin);
                if (idNdermarje == 0)
                    return new clsMesazh(false, rm.GetString("nukEkzistonKonfigurimEmailNdermarrjeLicense", ci));
                DbCore.clsMailSender mail = new DbCore.clsMailSender(idNdermarje, subject);
                mail.SendPlainAsyncEmail(toEmail, subject, headerbody + body + footerbody);
                return new clsMesazh(true, "Një email me fjalëkalimin e ri po dërgohet në postën tuaj elektronike!");
            }
            catch (Exception)
            {
                return new clsMesazh(false, "Ndodhi një gabim gjatë dërgimit të fjalëkalimit të ri në postën tuaj elektronike. Ju lutemi, provoni përsëri!");
            }
        }

        //TODO PATI -> Te kalohen ne nje funksion te pergjithshem te mesipermit, sepse dallojne nga njeri-tjetri nga ndryshime te vogla.
        public static clsMesazh DergoEmailFjalekaliminERi(string emailPerdoruesit, string username, string passwordiIRi, int idPerdorues, DbCore.DbAdmin.clsDatabaseAdmin dbAdmin)
        {
            if (String.IsNullOrEmpty(emailPerdoruesit))
                return new clsMesazh(false, "Fjalëkalimi nuk mund të rivendoset, mungon adresa e emailit në konfigurimin e klientit." + Environment.NewLine + "Ju lutemi, kontaktoni me administratorin!");
            string[] toEmail = { emailPerdoruesit };
            try
            {
                ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                System.Globalization.CultureInfo ci = mySessionObjects.ktheCultureInfo(System.Web.HttpContext.Current.Session);
                string subject = "Alpha Web Fjalekalimi i Pajisjes";
                string adresaip = HttpContext.Current.Request.UserHostAddress;
                string body = "Fjalëkalimi për t'u loguar në pajisjen me kod " + username + " është: " + passwordiIRi + " !";
                string headerbody = rm.GetString("njoftimDokHeaderEmail", ci);
                string footerbody = rm.GetString("njoftimDokFooterEmail", ci) + "<br />Alpha Web";
                DbCore.clsMailSender mail = new DbCore.clsMailSender(subject);
                mail.SendPlainAsyncEmail(toEmail, subject, headerbody + body + footerbody);
                return new clsMesazh(true, "Një email me fjalëkalimin e ri po dërgohet në postën tuaj elektronike!");
            }
            catch (Exception)
            {
                return new clsMesazh(false, "Ndodhi një gabim gjatë dërgimit të fjalëkalimit të ri në postën tuaj elektronike. Ju lutemi, provoni përsëri!");
            }
        }

        public static clsMesazh emailRoletENdryshuaraTePerdoruesit(int idNdermarje, int idPerdoruesi, int IdLicenca, string emriPerdorues, string MbiemriPerdorues, string roletOLD)
        {
            try
            {
                string[] toEmail = clsPerdorues.merrEmailPerRoletRASipasPerdoruesDheKodRol(idPerdoruesi, "RA").Split(';');
                if (toEmail.Length < 1)
                    return new clsMesazh(false, "Emaili nuk mund te dergohet sepse mungon adresa e emailit!");
                string roletNew = clsPerdorues.merrKodeteRolevesipasPerdoruesit(idPerdoruesi, IdLicenca);
                clsMailSender mail = new clsMailSender(idNdermarje, "Notification, change in user role");
                mail.SendPlainAsyncEmail(toEmail, "Notification, change in user role", "Dear Admin, <br/><br/>" + "Please be informed that the role of the user " + emriPerdorues + " " + MbiemriPerdorues + " has been changed from the role " + roletOLD + " to the role " + roletNew + " <br /><br/>" + "Thank you!" + "<br /><br/>Alpha Web");
                return new clsMesazh(true, "Emaili per njoftimin e ndryshimit te rolit per perdoruesin u dergua me sukses");
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "Dergimi i email per ndryshimin e rolit per perdoruesin {0} {1}", emriPerdorues, MbiemriPerdorues);
                return new clsMesazh(false, "Ndodhi nje gabim gjate dergimit te emailit per njoftimin e ndryshimit te rolit!");
            }
        }
        public static void DergoEmaileMeAttach(string[] emails, string subjekt, string trupEmail, int idNdermarrje, int idPerdorues, MemoryStream[] raportet, string[] emraRap)
        {
            try
            {
                if (emails.Length == 0)
                {
                    logu.Error("Mungojne email-et per dergim!");
                    //return new clsMesazh(false, "Mungojne email-et per dergim!");
                }

                int nrRap = raportet.Length;
                Attachment[] pdfAttachRaporte = new Attachment[nrRap];
                for (int i = 0; i < nrRap; i++)
                {
                    var pdfAttach = KrijoAttachmentNgaStream(raportet[i], emraRap[i], AttachmentType.Pdf);
                    pdfAttachRaporte[i] = pdfAttach;
                }

                clsMailSender mail = new clsMailSender(subjekt);
                mail.SendAsyncEmailMultipleAttach(emails, subjekt, trupEmail, idNdermarrje, idPerdorues, pdfAttachRaporte);
                logu.Info("Email-et u shtuan ne radhe per dergim!");
                //return new clsMesazh(true, "Email-et u derguan me sukses!");
            }
            catch (Exception ex)
            {
                logu.Error(ex.Message);
                //return new clsMesazh(false, "Ndodhi nje gabim gjate dergimit te raporteve me email!");
            }
        }

        public static clsMesazh DergoEmailNjoftuesPerMiratimBuxheti(int idNdermarrje, int vitDokumenti, int idPerdoruesi)
        {
            CultureInfo ci = new System.Globalization.CultureInfo("sq-AL");
            ResourceManager rm = new ResourceManager("Resources.Strings", Assembly.Load("App_GlobalResources"));

            string emails = clsNdermarrje.merrEmailTeBijave(idNdermarrje);

            string subject = rm.GetString("njoftimEmailMiratimBuxhetiSubject", ci);

            string body = rm.GetString("njoftimEmailMiratimBuxhetiBody", ci);
            body = body.Replace("#viti#", vitDokumenti.ToString());

            clsMailSender mailSender = new clsMailSender(idNdermarrje, subject);
            //mailSender.SendPlainAsyncEmail(emails.Split(','), subject, body);
            mailSender.SendPlainAsyncWfEmail(emails.Split(','), subject, body, idNdermarrje, -1, idPerdoruesi);

            return new clsMesazh(true, "Emaili u vendos per dergim.");
        }
        /// <summary>
        /// Dergon email njoftues per veprimet me dokumentat e alokimit
        /// </summary>
        /// <param name="idNdermNga">id e ndermarrjes nga do dergohet email (ku po behet veprimi i postimit/ruajtjes se dokumentit)</param>
        /// <param name="idNdermPer">id e ndermarrjes ku do te postohet dok (prindit) ose per cilen ndermarrje eshte ruajtur dok (bijen)</param>
        /// <param name="vitDok">kolona Viti te koka e dokumentit te alokimit</param>
        /// <param name="kodNdermBije">duhet te plotesohet vetem kur postohet dok nga bija te prindi dhe duhet te lihet ""(bosh) kur dok e miraton prindi</param>
        /// <returns></returns>
        public static clsMesazh DergoEmailNjoftuesPerAlokimBuxheti(int idPerdoruesi, int idNdermNga, int idNdermPer, int vitDok, string kodNdermBije = "")
        {
            CultureInfo ci = new System.Globalization.CultureInfo("sq-AL");
            ResourceManager rm = new ResourceManager("Resources.Strings", Assembly.Load("App_GlobalResources"));

            string email = "";
            using (clsDatabaseAdmin dba = new clsDatabaseAdmin())
                email = dba.merrEmailKonfigurimiSipasIdNdermarrje(idNdermPer);

            string subject = rm.GetString("njoftimEmailAlokiminBuxhetiSubject", ci);

            string body = "";
            if (kodNdermBije != "")
            {
                body = rm.GetString("njoftimEmailAlokimBuxhetiNgaBijaBody", ci);
                body = body.Replace("#qendra#", kodNdermBije);
            }
            else
                body = rm.GetString("njoftimEmailAlokimBuxhetiNgaPrindiBody", ci);
            body = body.Replace("#viti#", vitDok.ToString());

            clsMailSender mailSender = new clsMailSender(idNdermNga, subject);
            //mailSender.SendPlainAsyncEmail(email.Split(','), subject, body);
            mailSender.SendPlainAsyncWfEmail(email.Split(','), subject, body, idNdermNga, -1, idPerdoruesi);

            return new clsMesazh(true, "Emaili u vendos per dergim.");
        }
        public static clsMesazh DergoEmailAprovimRefuzimRialokimBuxheti(int idPerdoruesi, int idNdermNga, int idNdermPer, DateTime? dtDok, int idKonfigAmbjente, int idDokBije, int idStatusDok, string nrDok)
        {
            var data = (DateTime)dtDok;
            string email = "";
            string miratimRefuzim = idStatusDok == 8 ? MessagesResource.Messages["refuzua"] : MessagesResource.Messages["miratua"];

            using (clsDatabaseAdmin dba = new clsDatabaseAdmin())
                email = dba.merrEmailKonfigurimiSipasIdNdermarrje(idNdermPer);

            string subject = MessagesResource.Messages["miratimRefuzimRialokimiBuxhetiEmailSubject"];

            clsKonfigurimAmbjenti konf = new clsKonfigurimAmbjenti(idKonfigAmbjente);

            string body = "";
            body = MessagesResource.Messages["miratimRefuzimRialokimiBuxhetiEmailContent"];
            body = body.Replace("#llojDok", MessagesResource.Messages.IdGjuha == 0 ? konf.PershkrimKonfigAmbjente : konf.PershkrimKonfigAmbjenteEng).Replace("#dtDok", data.ToShortDateString()).Replace("#miratuaRefuzua#", miratimRefuzim).Replace("#nrDok", nrDok);

            clsMailSender mailSender = new clsMailSender(idNdermNga, subject);
            //mailSender.SendPlainAsyncEmail(email.Split(','), subject, body);
            mailSender.SendPlainAsyncWfEmail(email.Split(','), subject, body, idNdermNga, -1, idPerdoruesi);

            DbBuxheti.ClsBKokaBuxheti dokBije = new DbBuxheti.ClsBKokaBuxheti(idDokBije);
            if (dokBije.IdNdermPostuesi > 0)
                DergoEmailAprovimRefuzimRialokimBuxheti(idPerdoruesi, idNdermNga, dokBije.IdNdermPostuesi, dokBije.DtDok, dokBije.IdKonfigAmbjente, 0, dokBije.IdStatusDok, dokBije.NrDok);

            return new clsMesazh(true, "Emaili u vendos per dergim.");
        }

        public static clsMesazh DergoEmailKerkeseRialokimBuxheti(int idPerdoruesi, int idNdermNga, int idNdermPer, DateTime? dtDok, int idKonfigAmbjente)
        {
            var data = (DateTime)dtDok;
            string email = "";
            using (clsDatabaseAdmin dba = new clsDatabaseAdmin())
                email = dba.merrEmailKonfigurimiSipasIdNdermarrje(idNdermPer);

            string subject = MessagesResource.Messages["kerkeseRialokimiBuxhetiEmailSubject"];

            clsKonfigurimAmbjenti konf = new clsKonfigurimAmbjenti(idKonfigAmbjente);
            clsNdermarrje nderm = new clsNdermarrje(idNdermNga);
            string qenderDrejtori = nderm.Nivelstrukture == 2 ? MessagesResource.Messages["drejtori"] : MessagesResource.Messages["qenderShendetesore"];

            string body = "";
            body = MessagesResource.Messages["kerkeseRialokimiBuxhetiEmailContent"];
            body = body.Replace("#llojDok", MessagesResource.Messages.IdGjuha == 0 ? konf.PershkrimKonfigAmbjente : konf.PershkrimKonfigAmbjenteEng).Replace("#dtDok#", data.ToShortDateString()).Replace("#kodNderm", nderm.NdermarrjePershkrimi).Replace("#QendraDrejtoria", qenderDrejtori);

            clsMailSender mailSender = new clsMailSender(idNdermNga, subject);
            //mailSender.SendPlainAsyncEmail(email.Split(','), subject, body);
            mailSender.SendPlainAsyncWfEmail(email.Split(','), subject, body, idNdermNga, -1, idPerdoruesi);

            return new clsMesazh(true, "Emaili u vendos per dergim.");
        }

        public static clsMesazh DergoEmailKerkesePlanifikimInvestimBuxheti(int idPerdoruesi, int idNdermNga, int idNdermPer, DateTime? dtDok, int idKonfigAmbjente)
        {
            var data = (DateTime)dtDok;
            string email = "";
            using (clsDatabaseAdmin dba = new clsDatabaseAdmin())
                email = dba.merrEmailKonfigurimiSipasIdNdermarrje(idNdermPer);

            string subject = MessagesResource.Messages["PlanifikimInvestimBuxhetiEmailSubject"];

            clsKonfigurimAmbjenti konf = new clsKonfigurimAmbjenti(idKonfigAmbjente);
            clsNdermarrje nderm = new clsNdermarrje(idNdermNga);
            string qenderDrejtori = nderm.Nivelstrukture == 2 ? MessagesResource.Messages["drejtori"] : MessagesResource.Messages["qenderShendetesore"];

            string body = "";
            body = MessagesResource.Messages["kerkeseRialokimiBuxhetiEmailContent"];
            body = body.Replace("#llojDok", MessagesResource.Messages.IdGjuha == 0 ? konf.PershkrimKonfigAmbjente : konf.PershkrimKonfigAmbjenteEng).Replace("#dtDok#", data.ToShortDateString()).Replace("#kodNderm", nderm.NdermarrjePershkrimi).Replace("#QendraDrejtoria", qenderDrejtori);

            clsMailSender mailSender = new clsMailSender(idNdermNga, subject);
            //mailSender.SendPlainAsyncEmail(email.Split(','), subject, body);
            mailSender.SendPlainAsyncWfEmail(email.Split(','), subject, body, idNdermNga, -1, idPerdoruesi);

            return new clsMesazh(true, "Emaili u vendos per dergim.");
        }

        public static clsMesazh DergoEmailPlanifikimEkzekutimBuxheti(int idPerdoruesi, int idNdermNga, int idNdermPer, int vitDok, int idStatusDok, string kodNdermBije = "")
        {
            CultureInfo ci = new System.Globalization.CultureInfo("sq-AL");
            ResourceManager rm = new ResourceManager("Resources.Strings", Assembly.Load("App_GlobalResources"));

            string email = "";
            using (clsDatabaseAdmin dba = new clsDatabaseAdmin())
                email = dba.merrEmailKonfigurimiSipasIdNdermarrje(idNdermPer);

            string subject = rm.GetString("njoftimEmailPlanifikimEkzekutimBuxhetiSubject", ci);

            string body = "";
            if (kodNdermBije != "")
            {
                body = rm.GetString("njoftimEmailPlanifikimEkzekutimBuxhetiNgaBijaBody", ci);
                body = body.Replace("#qendra#", kodNdermBije);
            }
            else
            {
                if (idStatusDok == 8)
                    body = rm.GetString("njoftimEmailRefuzimPlanifikimEkzekutimBuxhetiNgaPrindiBody", ci);
                else
                    body = rm.GetString("njoftimEmailPlanifikimEkzekutimBuxhetiNgaPrindiBody", ci);
            }
            body = body.Replace("#viti#", vitDok.ToString());

            clsMailSender mailSender = new clsMailSender(idNdermNga, subject);
            //mailSender.SendPlainAsyncEmail(email.Split(','), subject, body);
            mailSender.SendPlainAsyncWfEmail(email.Split(','), subject, body, idNdermNga, -1, idPerdoruesi);

            return new clsMesazh(true, "Emaili u vendos per dergim.");
        }
        public static clsMesazh DergoEmailAprovimRefuzimPlanifikimInvestimBuxheti(int idPerdoruesi, int idNdermNga, int idNdermPer, DateTime? dtDok, int idKonfigAmbjente, int idDokBije, int idStatusDok, string nrDok)
        {
            var data = (DateTime)dtDok;
            string email = "";
            string miratimRefuzim = idStatusDok == 8 ? MessagesResource.Messages["refuzua"] : MessagesResource.Messages["miratua"];

            using (clsDatabaseAdmin dba = new clsDatabaseAdmin())
                email = dba.merrEmailKonfigurimiSipasIdNdermarrje(idNdermPer);

            string subject = MessagesResource.Messages["miratimRefuzimPlanifikimInvestimiBuxhetiEmailSubject"];

            clsKonfigurimAmbjenti konf = new clsKonfigurimAmbjenti(idKonfigAmbjente);

            string body = "";
            body = MessagesResource.Messages["miratimRefuzimRialokimiBuxhetiEmailContent"];
            body = body.Replace("#llojDok", MessagesResource.Messages.IdGjuha == 0 ? konf.PershkrimKonfigAmbjente : konf.PershkrimKonfigAmbjenteEng).Replace("#dtDok", data.ToShortDateString()).Replace("#miratuaRefuzua#", miratimRefuzim).Replace("#nrDok", nrDok);

            clsMailSender mailSender = new clsMailSender(idNdermNga, subject);
            //mailSender.SendPlainAsyncEmail(email.Split(','), subject, body);
            mailSender.SendPlainAsyncWfEmail(email.Split(','), subject, body, idNdermNga, -1, idPerdoruesi);

            DbBuxheti.ClsBKokaBuxheti dokBije = new DbBuxheti.ClsBKokaBuxheti(idDokBije);
            if (dokBije.IdNdermPostuesi > 0)
                DergoEmailAprovimRefuzimRialokimBuxheti(idPerdoruesi, idNdermNga, dokBije.IdNdermPostuesi, dokBije.DtDok, dokBije.IdKonfigAmbjente, 0, dokBije.IdStatusDok, dokBije.NrDok);

            return new clsMesazh(true, "Emaili u vendos per dergim.");
        }
        #region CRM

        public static clsMesazh DergoEmailListAgjenteshProblematik(string[] toEmail, HttpRequest request, colAgjenteShitje panisurTakimin, colAgjenteShitje tePakontaktuar, colAgjenteShitje agjentJashteRrezesh, string subject, int idNdermarrje)
        {
            if (toEmail.Length < 1)
                return new clsMesazh(false, "Emaili nuk mund te dergohet sepse mungon adresa e emailit!");

            try
            {
                //ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                //System.Globalization.CultureInfo ci = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo(idGjuha);
                // string subject = "Lajmerim per agjentet"; //rm.GetString("subjectEmailGeneratedPassword", ci);
                string adresaip = request.UserHostAddress;
                string tePakontaktuarHeader = "Lisa e agjenteve te pa kontaktuar : <br/>";
                string jashteRrezesHeader = "<br/>Lista e agjenteve qe e kane kryer takimin jashte rrezes se percaktuar:<br/>";
                string paNisurAkomaHeader = "<br/>Lista e agjenteve qe nuk e kane nisur takimin akoma:<br/>";
                string footerBody = "<br /><br />Ju faleminderit!" + " <br/>Alpha Web";
                string headerBody = " Pershendetje! <br/>";

                StringBuilder body = new StringBuilder("");

                if (tePakontaktuar.Count > 0)
                    body.Append(tePakontaktuarHeader);
                foreach (var agjent in tePakontaktuar)
                {
                    body.Append($"{agjent.EmriAgjentShitje} {agjent.MbiemriAgjentShitje} <br/>");
                }

                if (panisurTakimin.Count > 0)
                    body.Append(paNisurAkomaHeader);
                foreach (var agjent in panisurTakimin)
                {
                    body.Append($"{agjent.EmriAgjentShitje} {agjent.MbiemriAgjentShitje} <br/>");
                }
                if (agjentJashteRrezesh.Count > 0)
                    body.Append(jashteRrezesHeader);
                foreach (var agjent in agjentJashteRrezesh)
                {
                    body.Append($"{agjent.EmriAgjentShitje} {agjent.MbiemriAgjentShitje} <br/>");
                }

                //string footerbody = rm.GetString("njoftimDokFooterEmail", ci) + "<br />Alpha Web";
                clsMailSender mail = new clsMailSender(subject);
                mail.SendPlainAsyncEmail(toEmail, subject, headerBody + body + footerBody, idNdermarrje, 0);

                return new clsMesazh(true, "Emaili me listen e agjenteve u dergua me sukses");
            }
            catch (Exception)
            {

                return new clsMesazh(false, "Ndodhi nje gabim gjate dergimit te listes se agjenteve");
            }
        }

        public static clsMesazh RidergoEmailListAgjenteshProblematik(List<clsEmail> emails)
        {
            try
            {
                clsMailSender mail = new clsMailSender(emails[0].SubjektEmail);

                mail.ReSendPlainAsyncEmail(emails);
                return new clsMesazh(true, "Emaili me listen e agjenteve u vendos per ridergim");
            }
            catch (Exception)
            {
                return new clsMesazh(false, "Ndodhi nje gabim gjate ridergimit te listes se agjenteve");
            }
        }

        public static clsMesazh dergoEmailRaportinCRM(HttpRequest request, int idPerdoruesi, int idNdermarrje, int idGjuha, ResourceManager rm, CultureInfo ci, DateTime DtDok)
        {
            clsAgjentShitje agjenti = clsAgjentShitje.MerrAgjentShitjeSipasIdPerdoruesModile(idNdermarrje, idPerdoruesi);
            if (agjenti.IdAgjentShitje == 0) throw new MyException($"agjenti me id perdoruesi {idPerdoruesi} nuk ekziston!");
            var emailet = agjenti.merrEmailAgjentesh().ToArray();
            ImbLogger.Info($"Emailet per dergimin e veprimtarise ditore per agjentin {agjenti.EmriAgjentShitje} jane {string.Join(";", emailet)}");
            if (emailet.Length == 0) throw new MyException("Nuk ka asnje email ku mund te dergohet raporti i anketave!");
            var subject = $"Agjenti {agjenti.EmriAgjentShitje } {agjenti.MbiemriAgjentShitje}  {DtDok.ToString("dd/MM/yyyy")} ";
            
            var mail = new clsMailSender(idNdermarrje, subject);
            var headerbody = "Pershendetje,<br/><br/>";
            var footerbody = "<br/><br/>Faleminderit!";
            var body = $"{headerbody} Veprimtaria e agjentit {agjenti.EmriAgjentShitje } {agjenti.MbiemriAgjentShitje} per daten {DtDok.ToString("dd/MM/yyyy")}  eshte si ne dokumentin attach.{footerbody}";

            var emerFile = "VeprimtariaDitore";

            Attachment excelAttach;
            Attachment htmlAttach;
            //nuk funksionon nese e vendos ne using sepse mbyllet stream-i perpara se te procesohet attachementi ngaqe eshte async
            
            var excelStream = new MemoryStream();
            var htmlStream = new MemoryStream();
            ReportFunctions.CreateReportSurveyCrm(excelStream, htmlStream, idPerdoruesi, idNdermarrje, DtDok, agjenti, idGjuha);

            excelAttach = KrijoAttachmentNgaStream(excelStream, emerFile, AttachmentType.Excel);
            htmlAttach = KrijoAttachmentNgaStream(htmlStream, emerFile, AttachmentType.Image);

            mail.SendHtmlMailMeAttach(emailet, idNdermarrje, idPerdoruesi, subject, body, excelAttach, htmlAttach);
            return new clsMesazh(true, "Emaili u vendos per dergim");
        }
        
        /// <summary>
        /// Metode qe sherben per te derguar me email raportin kartolina e datelindjes.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="idGjuha"></param>
        /// <param name="rm"></param>
        /// <param name="ci"></param>
        /// <returns></returns>
        public static clsMesazh dergoEmailRaportinKartolinaDitelindjes(HttpRequest request, int idPerdoruesi, int idNdermarrje, int idGjuha, ResourceManager rm, CultureInfo ci)
        {
            DateTime dataSot = DateTime.Now;
            colKlienteFurnitore klientetePerDergimEmail = colKlienteFurnitore.mbushKlienteQeKaneDitelindjenSipasNdermarrjesDheDates(idNdermarrje, dataSot);
            if (klientetePerDergimEmail.Count == 0)
            {
                ImbLogger.Info($"Nuk ka asnje klient qe ka datelindjen sot ne ndermarrjen me id {idNdermarrje}!");
                return new clsMesazh(true, "Nuk ka asnje klient qe ka datelindjen sot ne ndermarrjen me id:" + idNdermarrje + "!");
            }
            foreach (var klienti in klientetePerDergimEmail)
            {
                if (String.IsNullOrWhiteSpace(klienti.EmailKF))
                {
                    ImbLogger.Info($"Klienti me kod {klienti.KodKlientFurnitor} dhe idNdermarrje {klienti.IdNdermarja} nuk ka email te specifikuar!");
                    continue;
                }
                var subject = $"Gezuar Datelindjen!";
                
                var mail = new clsMailSender(idNdermarrje, subject);
                var emerFile = "Kartolina";
                Attachment pdfAttach;
                //nuk funksionon nese e vendos ne using sepse mbyllet stream-i perpara se te procesohet attachementi ngaqe eshte async
                var pdfStream = new MemoryStream();
                ReportFunctions.CreateReportBirthdayCard(pdfStream,idPerdoruesi, idNdermarrje, 0, dataSot, klienti.KodKlientFurnitor, idGjuha);

                pdfAttach = KrijoAttachmentNgaStream(pdfStream, emerFile, AttachmentType.Pdf);
                mail.SendHtmlMailMeAttach(new[] { klienti.EmailKF }, idNdermarrje, idPerdoruesi, subject, "", pdfAttach);
                ImbLogger.Info($"Email per kartolinen e ditelindes per klientin me kod {klienti.KodKlientFurnitor} , idNdermarrje {klienti.IdNdermarja} dhe email {string.Join(";", klienti.EmailKF)} u vendos per dergim");
            }
            return new clsMesazh(true, "Email-et e kartolines se ditelindes u vendosen per dergim!");
        }

        /// <summary>
        /// Metode qe sherben per te derguar me email raportin gjenjda e artikujve min/max sipas magazines.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="idGjuha"></param>
        /// <param name="rm"></param>
        /// <param name="ci"></param>
        /// <returns></returns>
        public static clsMesazh dergoEmailRaportinGjenjdaArtMinMaxsipasMag(HttpRequest request, int idPerdoruesi, int idNdermarrje, int idGjuha)
        {
            try
            {
                using (clsDatabaseShare db = new clsDatabaseShare())
                {
                    DataTable dt = db.ktheRaportetDheAdresatEmail();
                    DateTime dataSot = DateTime.Now;
                    string idRaportePerTuUpdituar = "";


                    if (dt != null && dt.Rows != null && dt.Rows.Count == 0)
                    {
                        string err1 = "Nuk ka asnje raport per t'u derguar me email!";
                        logu.Error(err1);
                        return new clsMesazh(false, err1);
                    }
                    foreach (DataRow row in dt.Rows)
                    {
                        clsRaporti rap = new clsRaporti(idGjuha, Convert.ToInt32(row["IDRAPORTI"]));
                        string[] toEmail = row["EMAIL"].ToString().Split(';');
                        if (String.IsNullOrEmpty(toEmail[0]))
                        {
                            logu.Error("Emaili nuk mund te dergohet per raportin me id {0}, mungon adresa e emailit!", rap.IdRaporti);
                            continue;
                        }
                        var subject = $"Raporti Gjendja e artikujve min/max sipas magazines!";

                        var mail = new clsMailSender(idNdermarrje, subject);
                        Attachment pdfAttach;
                        //nuk funksionon nese e vendos ne using sepse mbyllet stream-i perpara se te procesohet attachementi ngaqe eshte async
                        var pdfStream = new MemoryStream();
                        ReportFunctions.CreateReportGjendjaArtMinMaxSipasMag(pdfStream, idPerdoruesi, idNdermarrje, 0, dataSot, idGjuha);

                         pdfAttach = KrijoAttachmentNgaStream(pdfStream, rap.RaportiEmri, AttachmentType.Pdf);
                         mail.SendHtmlMailMeAttach( toEmail , idNdermarrje, idPerdoruesi, subject, "", pdfAttach);
                         idRaportePerTuUpdituar = $"{idRaportePerTuUpdituar}{rap.IdRaporti};";
                    }
                db.updateDateDergimiPerEmaileRaporti(dataSot, idRaportePerTuUpdituar.Trim(';'));
            }
            return new clsMesazh(true, "Email-et e gjenjdes se artikujve min/max sipas magazines u vendosen per dergim!");
            }
            catch (Exception ex)
            {
                logu.Error(ex.Message);
                return new clsMesazh(false, "Ndodhi nje gabim gjate dergimit te raporteve me email!");
            }
        }


        public static Attachment KrijoAttachmentNgaStream(MemoryStream stream, string emerFile, AttachmentType attachmentType)
        {
            if (stream == null && !stream.CanRead) throw new MyException("Stream nuk eshte i rregullt!");
            stream.Position = 0;
            switch (attachmentType)
            {
                case AttachmentType.Excel:
                    return new Attachment(stream, $"{emerFile}.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                case AttachmentType.Pdf:
                    return new Attachment(stream, $"{emerFile}.pdf", MediaTypeNames.Application.Pdf);
                case AttachmentType.Html:
                    return new Attachment(stream, $"{emerFile}.html", MediaTypeNames.Text.Html);
                case AttachmentType.Image:
                    return new Attachment(stream, $"{emerFile}.png", MediaTypeNames.Image.Jpeg);
                default:
                    throw new MyException($"Attachemnt type {attachmentType} nuk eshte i sakte!");
            }
        }
        
        #endregion

        #region GIS

        /// <summary>
        /// Metode per te derguar emailet nga nderfaqe e GIS
        /// </summary>
        /// <param name="idNdermarrje">Ndermarrja nga do te behet dergimi i mesazhit</param>
        /// <param name="idPerdoruesi">Perdoruesi i loguar qe po kryen dergimin e mesazhit</param>
        /// <param name="toEmail">Emaile ku do te dergohet</param>
        /// <param name="fromName">Perdoruesi qe po e dergon</param>
        /// <param name="fromEmail">Adresa e perdoruesit qe po e dergon</param>
        /// <param name="subject">Titulli i mesazhit</param>
        /// <param name="bodyMesazh">Permbatja</param>
        /// <returns></returns>
        public static clsMesazh DergoEmailNgaGIS(CultureInfo ci, int idNdermarrje, int idPerdoruesi, string[] toEmail, string fromName, string fromEmail, string subject, string bodyMesazh)
        {
            if (toEmail.Length < 1)
                return new clsMesazh(false, "Emaili nuk mund te dergohet sepse mungon adresa e emailit!");

            try
            {
                ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                string path = HttpContext.Current.Server.MapPath("/images") + "\\GIS\\";
                System.IO.File.Copy(path + "attachEmail.png", path + "image.png", true);

                StringBuilder body = new StringBuilder("");
                body.Append("<body style='margin: 5px;'><div style='font-family: Arial; font-size: 13px;'>");
                body.Append(rm.GetString("emailHeader", ci));
                body.Append(bodyMesazh + "<br/><br/><br/>");
                body.Append(rm.GetString("labelRaportDergoi", ci) + " : <br/>");
                body.Append("<strong>" + fromName + "</strong><br/>");
                body.Append(rm.GetString("labelRaportEmail", ci) + " : <a href='mailto:" + fromEmail + "?subject=RE: " + subject + "'>" + fromEmail + "</a>");
                body.Append("</div></body>");

                clsMailSender mail = new clsMailSender(subject);
                mail.SendPlainAsyncEmail(toEmail, subject, body + "", idNdermarrje, path + "image.png", idPerdoruesi);

                return new clsMesazh(true, "Emaili u dergua me sukses!");
            }
            catch (Exception)
            {
                return new clsMesazh(false, "Ndodhi nje gabim gjate dergimit te emailit");
            }
        }
        
        #endregion

    }
}