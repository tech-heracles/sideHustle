using DbCore.IMBUtils.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
namespace AlphaWebCommon.Logging
{
    public class LogHelper
    {
        public static void ShtoRresht(string line, DataTable loget, string moduli, string verbosity)
        {
            if (!line.StartsWith("20"))
            {
                if (loget.Rows.Count > 0)
                    loget.Rows[loget.Rows.Count - 1]["Pershkrimi"] = loget.Rows[loget.Rows.Count - 1]["Pershkrimi"].ToString() + "<br>" + line;
                return;
            }
            DataRow dr = loget.NewRow();
            dr["Data"] = Convert.ToDateTime(line.Substring(0, line.IndexOf(' '))).ToString("yyyy-MM-dd");
            line = line.Substring(line.IndexOf(' ') + 1);
            dr["Ora"] = Convert.ToDateTime(line.Substring(0, line.IndexOf(' '))).ToString("HH:mm:ss");
            line = line.Substring(line.IndexOf(' ') + 1);
            dr["Server Name"] = (line[0] == '[') ? line.Substring(1, line.IndexOf("]") - 1) : "";
            line = line.Substring(line.IndexOf(": ") + 2);
            dr["Moduli"] = moduli == string.Empty ? "Te Pergjithshme" : moduli.Contains("\\") ? moduli.Remove(moduli.IndexOf("\\"), 1) : moduli;
            dr["Verbosity"] = verbosity;
            dr["Pershkrimi"] = MerrTekstPaEmraLogerash(line, dr);
            loget.Rows.Add(dr);
        }
        public static string MerrTekstPaEmraLogerash(string line, DataRow dr)
        {
            return line.Replace("WebApi ", "").Replace("importi ", "").Replace("brm ", "").Replace("promocionet ", "").Replace("Rivleresimet ", "")
                   .Replace("shitje ", "").Replace("buxhetimi ", "").Replace("Traces ", "").Replace("mbylljePeriudhe ", "");
        }

        public static List<string> MerrSkedaret(DateTime dataNga, DateTime dataDeri, string verbosity, string folderi, bool teGjitha)
        {
            List<string> dirs = Directory.GetFiles(folderi, dataNga.ToString("yyyy-MM-dd") + "." + verbosity + "*txt", teGjitha ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly).ToList();

            for (DateTime dt = dataNga.AddDays(1); dt <= dataDeri; dt = dt.AddDays(1))
            {
                List<string> dir = Directory.GetFiles(folderi, dt.ToString("yyyy-MM-dd") + "." + verbosity + "*txt", teGjitha ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly).ToList();
                dirs.AddRange(dir);
            }

            return dirs;
        }

        public static DataTable KrijoTabeleLogesh()
        {
            DataTable loget = new DataTable();

            loget.AddColumns(new DataColumn("Server Name", Type.GetType("System.String"))
                , new DataColumn("Data", Type.GetType("System.String"))
                , new DataColumn("Ora", Type.GetType("System.String"))
                , new DataColumn("Moduli", Type.GetType("System.String"))
                , new DataColumn("Verbosity", Type.GetType("System.String"))
                , new DataColumn("Pershkrimi", Type.GetType("System.String"))
                );
            return loget;
        }

        public static string MerrFolderPerLoget(string serverpath, string moduli)
        {
            if (moduli != "Te Pergjithshme")
                serverpath += @"\" + moduli;
            return serverpath;
        }

        public static void DeleteOldLogFiles(string folderPath, int maximumAgeInDays)
        {
            DateTime minimumDate = DateTime.Now.AddDays(-maximumAgeInDays);
            bool exists = System.IO.Directory.Exists(folderPath);

            if (!exists)
                System.IO.Directory.CreateDirectory(folderPath);
            foreach (var eligibleFileToDelete in Directory.EnumerateFiles(folderPath))
                DeleteFileIfOlderThan(eligibleFileToDelete, minimumDate);

            foreach (var subDirectory in Directory.EnumerateDirectories(folderPath))
            {
                var filesOfSubDirectory = Directory.EnumerateFiles(subDirectory);
                foreach (var eligibleFileToDelete in filesOfSubDirectory)
                    DeleteFileIfOlderThan(eligibleFileToDelete, minimumDate);
            }
        }

        private static void DeleteFileIfOlderThan(string path, DateTime date)
        {
            for (int i = 0; i < 2; ++i) // 2 Retries for Delete On Error
            {
                try
                {
                    var file = new FileInfo(path);
                    if (file.CreationTime < date)
                        file.Delete();
                }
                catch (IOException)
                {
                    System.Threading.Thread.Sleep(1000);
                }
                catch (UnauthorizedAccessException)
                {
                    System.Threading.Thread.Sleep(1000);
                }
            }
        }

    }
}
