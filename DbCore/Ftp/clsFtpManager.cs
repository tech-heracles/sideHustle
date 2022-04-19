using DbCore.DbAdmin;
using DbCore.DbInventari;
using DbCore.DbRegjistrim;
using DbCore.IMBUtils.Logging;
using IMB.Transactions.FileManager;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DbCore.Ftp
{
    public class clsFtpManager
    {
        private colKonfigurimFtp konfigurime;

        public clsFtpManager(int idNdermarrje, int idMetoda)
        {
            konfigurime = new colKonfigurimFtp(idNdermarrje, idMetoda);
            if (konfigurime.Count == 0)
                throw new MyException($"Nuk ka nje konfigurim ftp-je te metodes {((SerialeUnike_MetodeTransferimi)idMetoda).ToString()}!");
        }

        public clsFtpManager(int idNdermarrje, int idMetoda, string kategoria)
        {
            konfigurime = new colKonfigurimFtp(idNdermarrje, idMetoda, kategoria);
            if (konfigurime.Count == 0)
                throw new MyException($"Nuk ka nje konfigurim ftp-je per kategorine {kategoria} dhe metoden {((SerialeUnike_MetodeTransferimi)idMetoda).ToString()}!");
        }

        public (clsMesazh, clsMesazh) sendFile(FileInfo file)
        {
            byte[] fileContents = getFileContents(file.FullName);
            return sendFile(fileContents, file.Name);
        }

        public (clsMesazh, clsMesazh) sendFile(byte[] fileContents, string filename)
        {
            clsMesazh sukses = new clsMesazh(true, "");
            clsMesazh error = new clsMesazh(false, "");
            foreach (clsKonfigurimFtp k in konfigurime)
            {
                try
                {
                    if ((k.Kodi.ToLower().Contains("shitje") && filename.ToLower().Contains("blerje")) || (k.Kodi.ToLower().Contains("blerje") && filename.ToLower().Contains("devices")))
                        continue;
                    if (k.EshteSFTP)
                    {
                        string dirFilename = (String.IsNullOrEmpty(k.FolderPath)) ? filename : k.FolderPath + ((k.FolderPath[k.FolderPath.Length - 1] == '/') ? "" : "/") + filename;
                        SFtpDownloadFile(fileContents, k.HostName, k.Port, k.Username, k.Password, dirFilename);
                    }
                    else
                    {
                        FtpWebRequest request = createRequest(filename, fileContents.Length, k);

                        using (Stream requestStream = request.GetRequestStream())
                            requestStream.Write(fileContents, 0, fileContents.Length);
                    }
                    sukses.PershkrimMesazhi += $"{k.Kodi}, ";
                }
                catch (WebException ex)
                {
                    ImbLogger.Info(ex.Message);
                    error.PershkrimMesazhi += $"Dergimi i skedarit deshtoi per konfigurimin me kod {k.Kodi}. Error code: {((FtpWebResponse)ex.Response).StatusDescription}" + "<br>";
                }
                catch (Exception ex)
                {
                    error.PershkrimMesazhi += $"Dergimi i skedarit deshtoi per konfigurimin me kod {k.Kodi}. " + ex.Message + "<br>";
                }
            }
            return (sukses, error);
        }

        public void SFtpDownloadFile(byte[] fileByte, string Host, int Port, string Username, string Password, string FileName)
        {
            using (SftpClient sftp = new SftpClient(Host, Port, Username, Password))
            {
                ImbLogger.Info($"U krijua objekti SftpClient({Host}, {Port}, {Username}, *****).");
                sftp.Connect();
                ImbLogger.Info($"U be lidhja me serverin SFTP {Host}.");
                using (MemoryStream ms = new MemoryStream(fileByte))
                {
                    sftp.UploadFile(ms, FileName);
                    ImbLogger.Info($"U ruajt file ne direktorine {FileName}.");
                }
                sftp.Disconnect();
                ImbLogger.Info($"U shkeput lidhja me serverin SFTP {Host}.");
            }
        }


        #region metoda private
        private FtpWebRequest createRequest(string filename, int contentLength, clsKonfigurimFtp konfFTP)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(generateURI(filename, konfFTP));
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = new NetworkCredential(konfFTP.Username, konfFTP.Password);
            request.ContentLength = contentLength;
            request.EnableSsl = konfFTP.EnableSSL;
            return request;
        }

        private string generateURI(string fileName, clsKonfigurimFtp konfFTP)
        {
            string port = konfFTP.Port == 0 ? "" : $":{konfFTP.Port}";
            if(konfFTP.EnableSSL)
                return $"sftp://{konfFTP.HostName}{port}/{fileName}";
            else
                return $"ftp://{konfFTP.HostName}{port}/{fileName}";
        }

        private byte[] getFileContents(string fileName)
        {
            using (StreamReader sourceStream = new StreamReader(fileName))
                return Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
        }

        #endregion
    }
}
