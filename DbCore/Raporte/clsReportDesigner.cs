using DevExpress.XtraReports.UI;
using IMB.Transactions.FileManager;
using System;
using System.Data.SqlClient;
using System.IO;
using DbCore.IMBUtils.Logging;

namespace DbCore.Raporte
{
    /// <summary>
    /// kjo klase eshte pergjegjese per menaxhimin e layout-it te raporteve
    ///
    /// </summary>
    public class clsReportDesigner
    {
        #region Attributet

        public static string RootPath;
        public const string RepxReportsFolder = "Reports";

        #endregion Attributet

        #region Konstruktoret

        public clsReportDesigner()
        {

            // rootPath = HttpContext.Current.Server.MapPath(null) + @"\RaportetLayout\";
            //if (!Directory.Exists(rootPath))
            //    Directory.CreateDirectory(rootPath);
        }

        #endregion Konstruktoret

        /// <summary>
        /// kthen nje Report ne rastin qe ekziston file-i
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns>XtraReport</returns>
        public static void NgarkoLayoutRaporti(XtraReport rep, string fileName, string folder)
        {
            try
            {
                rep.LoadLayout(Path.Combine(RootPath, folder, fileName + ".repx"));
            }
            catch (Exception ex)
            {
                throw new MyException($"Nuk u ngarkua layouti i raportit nga file {fileName} per ndermarrjen {folder}", ex);
            }
        }

        public static void NgarkoLayoutRaporti(XtraReport rep, string fileName, int idNdermarrje)
        {
            NgarkoLayoutRaporti(rep, fileName, idNdermarrje.ToString());
        }

        public void SaveReportLayoutToFile(XtraReport rep, string fileName, string folder)
        {

            TxFileManager fileMgr = new TxFileManager();

            if (!fileMgr.DirectoryExists(RootPath))
                fileMgr.CreateDirectory(RootPath);
            if (!fileMgr.DirectoryExists(Path.Combine(RootPath, folder)))
                fileMgr.CreateDirectory(Path.Combine(RootPath, folder));
            if (fileMgr.DirectoryExists(Path.Combine(RootPath, folder)))
            {
                rep.SaveLayoutToXml(Path.Combine(RootPath, folder, fileName + ".repx"));
            }

        }

        /// <summary>
        /// perdoret nga reportDesigneri per te ruajtur layoutin ne file repx
        /// </summary>
        /// <param name="bytes">layouti ne byte</param>
        /// <param name="filename">emri file-it ku do ruhet raporti</param>
        /// <returns>statusi i ruajtjes</returns>
        public XtraReport SaveReportToFile(Byte[] bytes, string filename, string folder)
        {
            try
            {
                XtraReport rep = new XtraReport();
                rep.LoadLayout(new MemoryStream(bytes));
                SaveReportLayoutToFile(rep, filename, folder);
                return rep;

            }
            catch (Exception ex)
            {
                ImbLogger.Error(ex);
                throw new MyException($"Design-i {filename} nuk u ruajt! ", ex);
            }
        }

        /// <summary>
        /// kontrollon nese ekziston file me emrin e raportit
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool EkzistonRaporti(string fileName, int idNdermarrje)
        {
            return EkzistonRaporti(fileName, idNdermarrje.ToString());
        }

        public static bool EkzistonRaporti(string fileName)
        {
            return EkzistonRaporti(fileName, RepxReportsFolder);
        }

        public static bool EkzistonRaporti(string fileName, string folder)
        {
            try
            {
                return File.Exists(Path.Combine(RootPath, folder, fileName + ".repx"));
            }
            catch (Exception ex)
            {
                ImbLogger.Info($"Nuk u kontrollua dot ekzistenca e nje folderi per custom reports: {ex.Message}");
                return false;
            }
        }

        public void KonfiguroDataSourceRaporti(XtraReport report, SqlParameter[] parametra)
        {
            try
            {

                var spName = string.IsNullOrEmpty(report.DataMember) ? (report.DataAdapter as SqlDataAdapter).SelectCommand.CommandText : report.DataMember;
                ReportFunctions.konfigDataSetRaporti(report, spName, true, parametra);
            }
            catch (Exception ex)
            {
                ImbLogger.Error(ex);
            }
        }

        public static void InitializeCustomReportsPath(string path)
        {
            RootPath = path;
        }
    }
}