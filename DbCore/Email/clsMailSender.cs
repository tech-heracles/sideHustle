using DbCore.DbRegjistrim;
using DbCore.DbShare;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading;
using System.Linq;
using System.Diagnostics;
using DbCore.IMBUtils.Logging;
using System.Web.Configuration;
using DbCore.IMBUtils.Messages;

namespace DbCore
{
    public class clsMailSender
    {
        static Logger log = LogManager.GetCurrentClassLogger();

        //private string _mailServer = "smtp.imb.al";
        //private int _port = 587;
        //private string _mail = "wfes@alphaweb.al";
        //private string _passw = "Fjale4Kalim&Auto";
        //private string _emerEmali;
        //private bool _enableSsl = false;
        private string _mailServer;
        private int _port;
        private string _mail;
        private string _passw;
        private string _emerEmali;
        private bool _enableSsl;
        public Dictionary<string, MailMessage> listEmail = new Dictionary<string, MailMessage>();

        ///' <summary>
        ///' The starting function of console application.
        ///' </summary>
        ///' <remarks>Need to set the mail server details from console</remarks>

        //'EmbeddedImages()


        /// <summary>
        /// 
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="emerEmali"></param>
        public clsMailSender(int idNdermarrje, string emerEmali)
        {

            DbAdmin.clsKonfigurimEmail emailKonfig = new DbAdmin.clsKonfigurimEmail(idNdermarrje);
            _mailServer = emailKonfig.OutgoingSmtp;
            _mail = emailKonfig.DergoEmailNga;
            _passw = emailKonfig.Password;
            _port = emailKonfig.PortaSmtp;
            _enableSsl = emailKonfig.EnableSsl;

            _emerEmali = emerEmali; //duhet ne te dyja rastet
        }

        /// <summary>
        /// konstruktori qe sherben per dergimin e email-eve nga nje account gmail i alpha web 
        /// </summary>
        /// <param name="emerEmali"></param>
        public clsMailSender(string emerEmali)
        {
            _mailServer = "smtp.gmail.com";
            _port = 587;
            _mail = System.Web.Configuration.WebConfigurationManager.AppSettings["emailAWeb"].ToString();//"info.imb2009@gmail.com";
            _passw = System.Web.Configuration.WebConfigurationManager.AppSettings["passEmailAweb"].ToString();  //"Tirana123";
            _emerEmali = emerEmali;
            _enableSsl = true;
        }

        /// <summary>
        /// Sends mail using SMTP client
        /// </summary>
        /// <param name="mail">The SMTP server (MailServer) as String</param>
        /// <remarks>It can use the IP of Server also</remarks>
        private void SendMail(System.Net.Mail.MailMessage mail)
        {
            //send the message using SMTP client
            using (SmtpClient smtp = new SmtpClient(_mailServer) { /*mail Server IP or NAME */ Credentials = CredentialCache.DefaultNetworkCredentials })
            {
                smtp.Send(mail);
            }
        }
        // End SendMail
        /// <summary>
        /// Sets the content of MailMessage for default(plain text)
        /// </summary>
        /// <remarks>This is accessible by all Mail Clients</remarks>
        public void SendPlainMail(string toemail, string subjekt, string body)
        {
            //create the mail message
            MailMessage mail = new MailMessage(_mail, toemail);
            //set the message content
            mail.Subject = subjekt;
            mail.Body = body;
            //send the mail using SMTP Client
            //string certificate="Certificate.cer";
            //System.Security.Cryptography.X509Certificates.X509Certificate cer=new System.Security.Cryptography.X509Certificates.X509Certificate(certificate);
            SmtpClient smtp = new SmtpClient(_mailServer, _port);
            // smtp.EnableSsl = true;
            //smtp.Port = 25;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential(_mail, _passw);
            //mail Server IP or NAME 
            smtp.Send(mail);

        }
        public void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            SendCompletedCallback(sender, e, "");
        }
        public void SendCompletedCallback(object sender, AsyncCompletedEventArgs e, string userResetPassword)
        {
            // Get the unique identifier for this asynchronous operation.
            int idEmail = Convert.ToInt32(e.UserState);
            if (e.Cancelled)
            {
                if (idEmail != -1)
                {
                    DbCore.DbRegjistrim.clsEmail.ndryshoStatus(idEmail, DbRegjistrim.statusEmail.gabim, e.Error.Message);
                }
            }
            if (e.Error != null)
            {
                try
                {
                    if (idEmail == -1 && e.Error.Message.Contains("timed out")) //rasti kur serveri nuk eshte i disponueshem, te ridergohet email-i per resetim passwordi 
                    {
                        SmtpClient smtp = sender as SmtpClient;
                        MailMessage email = null;
                        listEmail.TryGetValue(idEmail.ToString(), out email);
                        smtp.SendAsync(email, idEmail.ToString());
                    }
                    else
                    {
                        //System.Diagnostics.Trace.WriteLine(String.Format("[{0}] {1}", idEmail, e.Error.ToString()));
                        DbCore.DbRegjistrim.clsEmail.ndryshoStatus(idEmail, DbRegjistrim.statusEmail.gabim, e.Error.Message + Environment.NewLine + ((e.Error.InnerException != null) ? Convert.ToString(e.Error.InnerException) : ""));
                    }
                    log.Error(e.Error, Environment.NewLine + (userResetPassword == "" ? "SendCompletedCallback" : "Error nga resetimi i fjalekalimit per userin : " + userResetPassword) + Environment.NewLine);
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                }

            }
            else if (idEmail != -1)
            {
                try
                {
                    Trace.WriteLine(String.Format("[{0}] Message sent.", idEmail));

                    DbCore.DbRegjistrim.clsEmail.ndryshoStatus(idEmail, DbRegjistrim.statusEmail.derguar, "");
                    string fullPath = DbCore.DbRegjistrim.clsEmail.merrPathRaport(idEmail);
                    if (!String.IsNullOrEmpty(fullPath))
                    {
                        MailMessage email = null;
                        listEmail.TryGetValue(fullPath, out email);
                        if (email != null)
                            email.Dispose();
                        System.IO.File.Delete(fullPath);
                    }
                }
                catch (Exception ex)
                {
                    log.Error(ex, "SendCompletedCallback");
                }
            }
        }
        public bool SendPlainAsyncEmailMultiple(string[] toEmails, string subjekt, string body, int idNdermarrje, int idPerdorues, bool isBodyHtml)
        {
            try
            {
                DbCore.DbRegjistrim.clsEmail email = new DbCore.DbRegjistrim.clsEmail(idNdermarrje,0, String.Join(";", toEmails), DateTime.Now, subjekt, body, idPerdorues);
                
                //SendPlainAsyncMail(toEmail, subjekt, body, email.IdEmail.ToString());
                //object emailObj = new { toEmail = toEmail, subjekt = subjekt, body = body, idEmail = email.IdEmail.ToString() };
                ThreadPool.QueueUserWorkItem(o => SendPlainAsyncMailMultiple(toEmails, subjekt, body, email.IdEmail.ToString(), isBodyHtml));
                return true;
            }
            catch (MyException)
            {
                return false;
            }
        }
        public bool SendAsyncEmailMultipleAttach(string[] toEmails, string subjekt, string body, int idNdermarrje, int idPerdorues, params Attachment[] atachements)
        {
            try
            {
               
                DbCore.DbRegjistrim.clsEmail email = new DbCore.DbRegjistrim.clsEmail(idNdermarrje,0, String.Join(";", toEmails), DateTime.Now, subjekt, body,idPerdorues);
                ThreadPool.QueueUserWorkItem(o => SendHtmlMailMeAttach(toEmails, subjekt, body, email.IdEmail.ToString(), atachements));
                return true;
            }
            catch (MyException)
            {
                return false;
            }
        }
        /// <summary>
        /// dergon me email faturen duke perdorur thread-et, rasti kur nuk eshte work flow. Per workflow eshte metoda SendPlainAsyncWfEmail
        /// </summary>
        /// <param name="toEmails"></param>
        /// <param name="subjekt"></param>
        /// <param name="body"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="Pathi"></param>
        /// <returns></returns>
        public bool SendPlainAsyncEmail(string[] toEmails, string subjekt, string body, int idNdermarrje, string Pathi, int idPerdoruesi, params Attachment[] attachments)
        {
            try
            {
                if (attachments.Length == 0)
                {
                    for (int i = 0; i < toEmails.Length; i++)
                    {
                        string toEmail = toEmails[i];
                        DbCore.DbRegjistrim.clsEmail email = new DbCore.DbRegjistrim.clsEmail(idNdermarrje, toEmail, DateTime.Now, subjekt, body, Pathi, idPerdoruesi);                      

                        //create the mail message
                        MailMessage mail = new MailMessage(_mail, toEmail);
                        if (!listEmail.ContainsKey(Pathi))
                            listEmail.Add(Pathi, mail);

                        ThreadPool.QueueUserWorkItem(o => SendHtmlMailMeAttach(mail, toEmail, subjekt, body, email.IdEmail.ToString(), Pathi));
                    }
                }
                else
                {
                    var email = new DbCore.DbRegjistrim.clsEmail(idNdermarrje, String.Join(";", toEmails), DateTime.Now, subjekt, body, idPerdoruesi);
                    ThreadPool.QueueUserWorkItem(o => SendHtmlMailMeAttach(toEmails, subjekt, body, email.IdEmail.ToString(), attachments));
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool SendPlainAsyncEmailAttach(string[] toEmails, string subjekt, string body, int idNdermarrje, string Pathi, int idPerdoruesi, colArkiva arkiva = null, string virtualPath = "")
        {
            try
            {
                DbCore.DbRegjistrim.clsEmail email = new DbCore.DbRegjistrim.clsEmail(idNdermarrje, String.Join(";", toEmails), DateTime.Now, subjekt, body, Pathi, idPerdoruesi);
                ThreadPool.QueueUserWorkItem(o => SendHtmlMailMeAttachZip(toEmails, subjekt, body, email.IdEmail.ToString(), Pathi, arkiva, idNdermarrje, virtualPath));
                return true;
            }
            catch (MyException)
            {
                return false;
            }
        }
        public bool ReSendPlainAsyncEmailAttach(int idEmail, string[] toEmails, string subjekt, string body, int idNdermarrje, string Pathi, int idPerdoruesi, colArkiva arkiva = null, string virtualPath = "")
        {
            try
            {
                ThreadPool.QueueUserWorkItem(o => SendHtmlMailMeAttachZip(toEmails, subjekt, body, idEmail.ToString(), Pathi, arkiva, idNdermarrje, virtualPath));
                return true;
            }
            catch (MyException)
            {
                return false;
            }
        }
        public bool SendPlainAsyncEmail(string[] toEmails, string subjekt, string body, string usernameResetPassword)
        {
            try
            {
                for (int i = 0; i < toEmails.Length; i++)
                {
                    string toEmail = toEmails[i];
                    ThreadPool.QueueUserWorkItem(o => SendPlainAsyncMail(toEmail, subjekt, body, "-1", usernameResetPassword, true, Convert.ToBoolean(System.Web.Configuration.WebConfigurationManager.AppSettings["MailServerWithCredentials"])));
                }
                return true;
            }
            catch (MyException)
            {
                return false;
            }
        }
        /// <summary>
        /// dergo email me threade, rasti kur eshte work flow
        /// </summary>
        /// <param name="toEmails"></param>
        /// <param name="subjekt"></param>
        /// <param name="body"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="nrProcesi">-1 kur nuk eshte per work flow, pernd eshte > se 0</param>
        /// <returns></returns>
        public bool SendPlainAsyncWfEmail(string[] toEmails, string subjekt, string body, int idNdermarrje, int nrProcesi, int idPerdoruesi)
        {
            try
            {
                for (int i = 0; i < toEmails.Length; i++)
                {
                    string toEmail = toEmails[i];
                    DbCore.DbRegjistrim.clsEmail email = new DbCore.DbRegjistrim.clsEmail(idNdermarrje, nrProcesi, toEmail, DateTime.Now, subjekt, body, idPerdoruesi);
                    //SendPlainAsyncMail(toEmail, subjekt, body, email.IdEmail.ToString());
                    //object emailObj = new { toEmail = toEmail, subjekt = subjekt, body = body, idEmail = email.IdEmail.ToString() };
                    ThreadPool.QueueUserWorkItem(o => SendPlainAsyncMail(toEmail, subjekt, body, email.IdEmail.ToString()));
                }
                return true;
            }
            catch (MyException)
            {
                return false;
            }
        }
        /// <summary>
        /// dergo email me threade
        /// </summary>
        /// <param name="toEmails"></param>
        /// <param name="subjekt"></param>
        /// <param name="body"></param>
        /// <param name="idNdermarrje"></param>
        /// <returns></returns>
        public bool SendPlainAsyncEmail(string[] toEmails, string subjekt, string body, int idNdermarrje, int idPerdoruesi, bool isBodyHtml = true)
        {
            try
            {
                for (int i = 0; i < toEmails.Length; i++)
                {
                    string toEmail = toEmails[i];
                    DbCore.DbRegjistrim.clsEmail email = new DbCore.DbRegjistrim.clsEmail(idNdermarrje, toEmail, DateTime.Now, subjekt, body, idPerdoruesi);
                    //SendPlainAsyncMail(toEmail, subjekt, body, email.IdEmail.ToString());
                    //object emailObj = new { toEmail = toEmail, subjekt = subjekt, body = body, idEmail = email.IdEmail.ToString() };
                    ThreadPool.QueueUserWorkItem(o => SendPlainAsyncMail(toEmail, subjekt, body, email.IdEmail.ToString(), isBodyHtml));
                }
                return true;
            }
            catch (MyException)
            {
                return false;
            }
        }
        /// <summary>
        /// rifergon emailet qe nuk jane derguar
        /// </summary>
        /// <param name="emailet"></param>
        /// <returns></returns>
        public bool ReSendPlainAsyncEmail(List<clsEmail> emailet)
        {
            try
            {
                foreach (var email in emailet)
                {

                    ThreadPool.QueueUserWorkItem(o => SendPlainAsyncMail(email.MarresEmail, email.SubjektEmail, email.TrupEmail, email.IdEmail.ToString(), true));
                }
                return true;
            }
            catch (MyException)
            {
                return false;
            }
        }
        /// <summary>
        /// dergon email me threade por nuk e ruan ne db email-in si rasti i resetimit te passwordit
        /// </summary>
        /// <param name="toEmails"></param>
        /// <param name="subjekt"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public bool SendPlainAsyncEmail(string[] toEmails, string subjekt, string body)
        {
            try
            {
                for (int i = 0; i < toEmails.Length; i++)
                {
                    string toEmail = toEmails[i];
                    ThreadPool.QueueUserWorkItem(o => SendPlainAsyncMail(toEmail, subjekt, body));
                }
                return true;
            }
            catch (MyException)
            {
                return false;
            }
        }
        /// <summary>
        /// dergo simple email me threade, rasti kur nuk eshte work flow
        /// </summary>
        /// <param name="toEmails"></param>
        /// <param name="subjekt"></param>
        /// <param name="body"></param>
        /// <param name="idNdermarrje"></param>
        public bool SendPlainAsyncMail(string[] toEmails, string subjekt, string body, int idNdermarrje, int idPerdoruesi)
        {
            return SendPlainAsyncWfEmail(toEmails, subjekt, body, idNdermarrje, -1, idPerdoruesi);
        }
        /// <summary>
        /// dergon email ne menyre asinkrone
        /// </summary>
        /// <param name="toemail"></param>
        /// <param name="subjekt"></param>
        /// <param name="body"></param>
        /// <param name="idEmail">-1 vetem ne rastin kur dergimi i email behet per resetim passwordi te perdoruesit qe logohet ne program</param>
        /// <param name="isBodyHtml">true kur trupi i email do jete html, false perndryshe</param>
        public void SendPlainAsyncMail(string toemail, string subjekt, string body, string idEmail, bool isBodyHtml = false)
        {
            SendPlainAsyncMail(toemail, subjekt, body, idEmail, "", isBodyHtml, Convert.ToBoolean(System.Web.Configuration.WebConfigurationManager.AppSettings["MailServerWithCredentials"]));
        }
        /// <summary>
        /// dergon email ne menyre asinkrone
        /// </summary>
        /// <param name="toemail"></param>
        /// <param name="subjekt"></param>
        /// <param name="body"></param>
        /// <param name="idEmail"></param>
        /// <param name="isBodyHtml"></param>
        /// <param name="withCredentials">true kur plotesohet kredencialet e accountit dergues, false kur nuk plotesohen</param>
        public void SendPlainAsyncMail(string toemail, string subjekt, string body, string idEmail, string usernametoResetPass, bool isBodyHtml = false, bool withCredentials = true)
        {
            try
            {
                //create the mail message
                MailMessage mail = new MailMessage() { From = new MailAddress(_mail, _emerEmali) };
                mail.To.Add(toemail);
                //set the message content
                mail.Subject = subjekt;
                mail.Body = body;
                mail.IsBodyHtml = isBodyHtml;
                //send the mail using SMTP Client
                //string certificate="Certificate.cer";
                //System.Security.Cryptography.X509Certificates.X509Certificate cer=new System.Security.Cryptography.X509Certificates.X509Certificate(certificate);
                SmtpClient smtp = new SmtpClient(_mailServer, _port);
                smtp.EnableSsl = _enableSsl;
                //smtp.Port = 25;
                

                if (withCredentials)
                    smtp.Credentials = new System.Net.NetworkCredential(mail.From.Address, _passw);
                else
                    smtp.UseDefaultCredentials = Convert.ToBoolean(System.Web.Configuration.WebConfigurationManager.AppSettings["UseDefaultCredentials"]);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                listEmail.Add(idEmail + toemail, mail);
                smtp.SendCompleted += new SendCompletedEventHandler((s, e) => SendCompletedCallback(s, e, usernametoResetPass));
                //mail Server IP or NAME 

                smtp.SendAsync(mail, idEmail.ToString());
            }
            catch (Exception ex)
            {
                log.Error(ex, Environment.NewLine + (usernametoResetPass == "" ? "SendPlainAsyncMail" : " Error nga resetimi i fjalekalimit per userin : " + usernametoResetPass) + Environment.NewLine);
                //clsLogError log;
                //if (Convert.ToInt32(System.Web.Configuration.WebConfigurationManager.AppSettings["Verbosity"]) < 5)
                //    log = new clsLogError("~/log/Errors/log.txt", " Error nga resetimi i fjalekalimit per userin : " + usernametoResetPass + Environment.NewLine + ex.Message);
            }
        }
        public void SendPlainAsyncMailMultiple(string[] toemail, string subjekt, string body, string idEmail, bool isBodyHtml = false)
        {
            //create the mail message
            MailMessage mail = new MailMessage();
            mail.To.Add(String.Join(",", toemail));
            mail.From = new MailAddress(_mail, _emerEmali);
            mail.Subject = subjekt;
            mail.Body = body;
            mail.IsBodyHtml = isBodyHtml;
            SmtpClient smtp = new SmtpClient(_mailServer, _port);
            smtp.EnableSsl = _enableSsl;
            if (Convert.ToBoolean(System.Web.Configuration.WebConfigurationManager.AppSettings["MailServerWithCredentials"]))
                smtp.Credentials = new System.Net.NetworkCredential(_mail, _passw);
            else
                smtp.UseDefaultCredentials = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            listEmail.Add(idEmail + toemail, mail);
            smtp.SendCompleted += SendCompletedCallback;
            try
            {
                smtp.SendAsync(mail, idEmail.ToString());
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void ReSendPlainAsyncMail(string toemail, string subjekt, string body, string idEmail, bool isBodyHtml = false)
        {
            //create the mail message
            MailMessage mail = new MailMessage() { From = new MailAddress(_mail, _emerEmali) };
            mail.To.Add(toemail);
            //set the message content
            mail.Subject = subjekt;
            mail.Body = body;
            mail.IsBodyHtml = isBodyHtml;
            //send the mail using SMTP Client
            //string certificate="Certificate.cer";
            //System.Security.Cryptography.X509Certificates.X509Certificate cer=new System.Security.Cryptography.X509Certificates.X509Certificate(certificate);
            SmtpClient smtp = new SmtpClient(_mailServer, _port);
            smtp.EnableSsl = _enableSsl;
            //smtp.Port = 25;
            smtp.Credentials = new System.Net.NetworkCredential(mail.From.Address, _passw);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            listEmail.Add(idEmail + toemail, mail);
            smtp.SendCompleted += SendCompletedCallback;
            //mail Server IP or NAME 
            try
            {
                smtp.SendAsync(mail, idEmail.ToString());
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// dergon email ne menyre asinkrone. Email-et nuk ruhen ne db dhe jane me permbajtje email html(si rasti i resetim passwordit)
        /// </summary>
        /// <param name="toemail"></param>
        /// <param name="subjekt"></param>
        /// <param name="body"></param>
        public void SendPlainAsyncMail(string toemail, string subjekt, string body)
        {
            SendPlainAsyncMail(toemail, subjekt, body, "-1", true);
        }
        public void SendPlainMail(string[] toemail, string subjekt, string body)
        {
            //create the mail message
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(_mail, _emerEmali);
            mail.To.Add(String.Join(",", toemail));
            //set the message content
            mail.Subject = subjekt;
            mail.Body = body;
            //send the mail using SMTP Client
            //string certificate="Certificate.cer";
            //System.Security.Cryptography.X509Certificates.X509Certificate cer=new System.Security.Cryptography.X509Certificates.X509Certificate(certificate);
            SmtpClient smtp = new SmtpClient(_mailServer, _port);
            // smtp.EnableSsl = _enableSsl;
            //smtp.Port = 25;
            smtp.Credentials = new System.Net.NetworkCredential(_mail, _passw);
            //mail Server IP or NAME 
            smtp.Send(mail);
        }
        // End SendPlainMail
        /// <summary>
        /// dergon email me attachment per raportet
        /// </summary>
        /// <param name="toemail"></param>
        /// <param name="subjekt"></param>
        /// <param name="body"></param>
        /// <param name="idEmail"></param>
        /// <param name="pathi">pathi i raportit qe do dergohet ne attach</param>
        public void SendHtmlMailMeAttach(MailMessage mail, string toemail, string subjekt, string body, string idEmail, string pathi)
        {
            //set the message content
            mail.Subject = subjekt;
            mail.Body = body;
            mail.IsBodyHtml = true;
            //send the mail using SMTP Client
            //string certificate="Certificate.cer";
            //System.Security.Cryptography.X509Certificates.X509Certificate cer=new System.Security.Cryptography.X509Certificates.X509Certificate(certificate);
            SmtpClient smtp = new SmtpClient(_mailServer, _port);
            smtp.EnableSsl = _enableSsl;
            smtp.Credentials = new System.Net.NetworkCredential(_mail, _passw);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            mail.Attachments.Clear();
            if (System.IO.File.Exists(pathi))
            {
                Attachment attachment1 = new Attachment(pathi);
                mail.Attachments.Add(attachment1);
                smtp.SendCompleted += SendCompletedCallback;
                //mail Server IP or NAME 
                try
                {
                    smtp.SendAsync(mail, idEmail.ToString());
                }
                catch (Exception ex)
                {
                    ImbLogger.Error(ex);
                    throw;
                }
            }
        }
        public void SendHtmlMailMeAttach(string[] toemail, string subjekt, string body, string idEmail,params Attachment[] atachements)
        {

            try
            {
                MailMessage mail = new MailMessage
                {
                    From = new MailAddress(_mail),
                };

                mail.To.Add(string.Join(",", toemail));
                mail.Subject = subjekt;
                mail.Body = body;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient(_mailServer, _port);
                smtp.EnableSsl = _enableSsl;
       
                if (Convert.ToBoolean(WebConfigurationManager.AppSettings["MailServerWithCredentials"]))
                    smtp.Credentials = new NetworkCredential(_mail, _passw);
                else
                    smtp.UseDefaultCredentials = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                mail.Attachments.Clear();
                foreach (var attach in atachements)
                    mail.Attachments.Add(attach);
                smtp.SendCompleted += SendCompletedCallback;

                smtp.SendAsync(mail, idEmail.ToString());
            }
            catch (Exception ex)
            {
                throw new MyException(ex.Message);
            }

        }
        public void SendHtmlMailMeAttach(string[] toEmails, int idNdermarrje, int idPerdoruesi, string subjekt, string body, params Attachment[] atachements)
        {
            try
            {
                var emails = string.Join(";", toEmails);

                DbCore.DbRegjistrim.clsEmail email = new DbCore.DbRegjistrim.clsEmail(idNdermarrje, emails, DateTime.Now, subjekt, body, idPerdoruesi);
                SendHtmlMailMeAttach(toEmails, subjekt, body, email.IdEmail.ToString(), atachements);


            }
            catch (Exception ex)
            {
                throw new MyException(ex.Message);
            }
        }        
        public void SendHtmlMailMeAttachZip(string[] toemail, string subjekt, string body, string idEmail, string pathi, colArkiva arkiva, int idNdermarrje, string virtualPath)
        {
            //create the mail message
            MailMessage mail = new MailMessage();
            mail.To.Add(String.Join(",", toemail));
            mail.From = new MailAddress(_mail, _emerEmali);
            //set the message content
            mail.Subject = subjekt;
            mail.Body = body;
            mail.IsBodyHtml = true;
            //send the mail using SMTP Client
            //string certificate="Certificate.cer";
            //System.Security.Cryptography.X509Certificates.X509Certificate cer=new System.Security.Cryptography.X509Certificates.X509Certificate(certificate);
            SmtpClient smtp = new SmtpClient(_mailServer, _port);
            smtp.EnableSsl = _enableSsl;
            if (Convert.ToBoolean(WebConfigurationManager.AppSettings["MailServerWithCredentials"]))
                smtp.Credentials = new System.Net.NetworkCredential(_mail, _passw);
            else
                smtp.UseDefaultCredentials = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            mail.Attachments.Clear();
            if (arkiva != null)
            {
                foreach (clsArkiva dok in arkiva)
                {
                    Attachment attachment1 = new Attachment(virtualPath + dok.Path.Replace("~", string.Empty));
                    attachment1.Name = dok.FileName;
                    mail.Attachments.Add(attachment1);
                }
            }
           
            if (System.IO.File.Exists(pathi))
            {
                Attachment attach = new Attachment(pathi);
                mail.Attachments.Add(attach);
                if (!listEmail.ContainsKey(pathi))
                    listEmail.Add(pathi, mail);
                //Attachment attachment1 = new Attachment(zipStream, new ContentType("application/zip"));
                //mail.Attachments.Add(attachment1);
                smtp.SendCompleted += SendCompletedCallback;
                //mail Server IP or NAME 
                try
                {
                    smtp.SendAsync(mail, idEmail.ToString());
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        // End SendHtmlMailMeAttach        
        public void SendHtmlMail(string toemail, string subjekt, string body)
        {
            //create the mail message
            MailMessage mail = new MailMessage(_mail, toemail);
            //set the message content
            mail.Subject = subjekt;
            mail.Body = body;
            mail.IsBodyHtml = true;
            //send the mail using SMTP Client
            //string certificate="Certificate.cer";
            //System.Security.Cryptography.X509Certificates.X509Certificate cer=new System.Security.Cryptography.X509Certificates.X509Certificate(certificate);
            SmtpClient smtp = new SmtpClient(_mailServer);
            // smtp.EnableSsl = true;
            //smtp.Port = 25;
            smtp.Credentials = new System.Net.NetworkCredential(_mail, _passw);
            //mail Server IP or NAME 
            smtp.Send(mail);
        }
        /// <summary>
        /// Sets the MailMessage content with multiple body parts
        /// (e.g a Html part and a PlainText part..)
        /// </summary>
        /// <remarks>Plain body is for Mail Clients, 
        /// those don't support Html</remarks>
        public void MultiPartMailBody(string fromemail, string toemail, string subjekt, string plaintext, string htmltext)
        {
            //create the mail message
            MailMessage mail = new MailMessage(fromemail, toemail);
            //set the message header
            mail.Subject = subjekt;
            mail.Priority = MailPriority.High;
            //first we create the Plain Text part
            AlternateView plainView = AlternateView.CreateAlternateViewFromString(plaintext, null, "text/plain");
            mail.AlternateViews.Add(plainView);
            //then we create the Html part
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(htmltext, null, "text/html");
            mail.AlternateViews.Add(htmlView);
            //send mail
            SendMail(mail);
        }
        // End MultiPartMailBody
        /// <summary>
        /// Set the MailMessage instance for multiple recipients with attachment
        /// </summary>
        /// <remarks>From address can be customized to show a Display Name</remarks>
        public void MultipleRecipients(string fromemail, string toemail, string emriqeshfaqet)
        {
            //create the mail message
            MailMessage mail = new MailMessage();
            //set the addresses
            //-----------------------------------
            //to specify a friendly 'from' name, we use a different display name
            mail.From = new MailAddress(fromemail, emriqeshfaqet);
            //since the To, Cc, and Bcc properties are collections, to add multiple
            //addreses, we simply call .Add(...) multple times
            mail.To.Add("to@todomain.com");
            mail.To.Add("to2@to2domain.com");
            mail.CC.Add("cc1@cc1domain.com");
            mail.CC.Add("cc2@cc2domain.com");
            mail.Bcc.Add("bcc1@bcc1domain.com");
            mail.Bcc.Add("bcc2@bcc2domain.com");
            //-----------------------------------
            //set the mail content
            mail.Subject = "This is an email with attachment";
            mail.Body = "This is the body content of the email.";
            //set the attachment
            mail.Attachments.Clear();
            Attachment attachment1 = new Attachment("c:\\attachment\\image1.jpg");
            mail.Attachments.Add(attachment1);
            mail.Attachments.Add(new Attachment("c:\\attachment\\text1.txt"));
            //send mail
            SendMail(mail);
        }
        // End MultipleRecipients
        /// <summary>
        /// Embeds an image in a Html body of MailMessage
        /// </summary>
        /// <remarks>The standard image tag must be there in html body with 'cid' 
        /// in src value</remarks>
        public void EmbeddedImages(string fromemail, string toemail, string emrishfaqet, string subjekt, string plaintext, string htmltext, string imgpath)
        {
            //create the mail message
            MailMessage mail = new MailMessage();
            //set the addresses
            mail.From = new MailAddress(fromemail, emrishfaqet);
            mail.To.Add(toemail);
            //set the content
            mail.Subject = subjekt;
            //first we create the Plain Text part
            AlternateView plainView = AlternateView.CreateAlternateViewFromString(plaintext, null, "text/plain");
            //then we create the Html part
            //to embed images, we need to use the prefix 'cid' in the img src value
            string htmlBody = htmltext;
            htmlBody += "<img alt=\"\" hspace=0 src=\"cid:uniqueId\" align=baseline border=0 >";
            htmlBody += "<DIV>&nbsp;</DIV><b>Fundi i email-it</b>";
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(htmlBody, null, "text/html");
            //create the AlternateView for embedded image
            AlternateView imageView = new AlternateView(imgpath, MediaTypeNames.Image.Jpeg);
            imageView.ContentId = "uniqueId";
            imageView.TransferEncoding = TransferEncoding.Base64;
            //add the views
            mail.AlternateViews.Add(plainView);
            mail.AlternateViews.Add(htmlView);
            mail.AlternateViews.Add(imageView);
            //send mail
            SendMail(mail);
        }
        // End EmbedImages

        public static clsMesazh DergoEmailTest(string email, string password, int port, string server, bool enableSSL, string subjekt, string permbajtje)
        {
            MailMessage mail = new MailMessage(email, email, subjekt, permbajtje);
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient(server, port);
            smtp.EnableSsl = enableSSL;
            if (Convert.ToBoolean(WebConfigurationManager.AppSettings["MailServerWithCredentials"]))
                smtp.Credentials = new NetworkCredential(email, password);
            else
                smtp.UseDefaultCredentials = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            try
            {
                smtp.Send(mail);
            }
            catch (SmtpFailedRecipientsException ex)
            {
                ImbLogger.Error(ex.Message);
                return new clsMesazh(false, MessagesResource.Messages["msgEmailNukMundTeDergohetNeAdresenEDhene"]);
            }
            catch (SmtpException ex)
            {
                ImbLogger.Error(ex.Message);
                return new clsMesazh(false, MessagesResource.Messages["msgProblemMeDergiminOseLidhjenMeServerinSMTP"] + " " + MessagesResource.Messages["msgEmailTestNukUDergua"]);
            }
            catch (Exception ex)
            {
                ImbLogger.Error(ex.Message);
                return new clsMesazh(false, MessagesResource.Messages["msgEmailTestNukUDergua"]);
            }

            return new clsMesazh(TipMesazhi.Informim, MessagesResource.Messages["msgEmailTestEshteDerguarNePostenElektronike"]);
        }

    }
}
