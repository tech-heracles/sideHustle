using DbCore.DbAdmin;
using IMB.Transactions.FileManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Logging;

namespace DbCore.Raporte
{
    public class VeprimtariaDitoreExporter
    {


        private DataTable data;
        private DataTable dataPerTuEksportuar;
        private string rootPath;
        private string ndermarrjeKodi;
        private string preffix;
        private int idNdermarrje;
        System.Resources.ResourceManager Rm;
        System.Globalization.CultureInfo Ci;

        public VeprimtariaDitoreExporter(DataTable data, string rootPath,int idNdermarrje,System.Resources.ResourceManager rm, System.Globalization.CultureInfo ci)
        {

            clsNdermarrje ndermarrje = new clsNdermarrje(idNdermarrje);
            this.data = data;
            this.rootPath = rootPath;
            this.ndermarrjeKodi = ndermarrje.NdermarrjeKodi;
            this.idNdermarrje = idNdermarrje;
            this.Rm = rm;
            this.Ci = ci;
            preffix = $"{rootPath}{ndermarrjeKodi}_{DateTime.Now.ToString("yyyyMMdd")}";

            dataPerTuEksportuar = colRekordeTeEksportuar.merrKolonaPerTuEksportuar(data.Select("K1 = 200 OR K1 = 910").GetDataTable(data));
        }
        public clsMesazh Eksporto()
        {
            clsMesazh mesazh = new clsMesazh(true, Rm.GetString("msgRaportiUEksportuaMeSukses", Ci));
            using (var scope = new MyTransactionScope())
            {
                try
                {
                    TxFileManager fileManager = new TxFileManager();
                    if (!fileManager.DirectoryExists(rootPath))
                        fileManager.CreateDirectory(rootPath);

                    FileInfo skedarEkzistues = merrSkedarinEDitesSeSotmeNeseEkziston();
                    string emerIRiSkedari = percaktoEmrinESkedaritTeRi(skedarEkzistues);

                    DbShare.clsDatabaseShare dbShare = new DbShare.clsDatabaseShare();

                    fileManager.WriteAllText(emerIRiSkedari, merrGjitheRekordet(emerIRiSkedari, dbShare));

                    colRekordeTeEksportuar.updateRekordeTeEksportuar(data.Select("K1 = 200 OR K1 = 910").GetDataTable(data), dbShare);

                    FshiSkedaretEVjeter(fileManager, emerIRiSkedari);

                    Ftp.clsFtpManager ftpManager = new Ftp.clsFtpManager(idNdermarrje, 0); //idMetoda 0 per manuale

                    ftpManager.sendFile(new FileInfo(emerIRiSkedari));
                    scope.Complete();
                } catch (Exception ex)
                {

                    ImbLogger.Error(ex);
                    mesazh = new clsMesazh(false, ex.Message);
                }
            }
            return mesazh;
        }

        private void FshiSkedaretEVjeter(TxFileManager fileManager,string emerIRiSkedari)
        {
            foreach (FileInfo file in (new DirectoryInfo(rootPath)).GetFiles())
            {
                if (file.FullName != emerIRiSkedari && file.Name.StartsWith(ndermarrjeKodi) && file.Name.EndsWith("txt"))
                {
                    fileManager.Delete(file.FullName);
                }
            }
        }

        private string merrGjitheRekordet(string emerIRiSkedari, DbShare.clsDatabaseShare dbShare)
        {
            StringBuilder lines = new StringBuilder();
            string line;
            DataTable rekordeTeDetyrueshme = colRekordeTeEksportuar.merrRekordeTeDetyrueshme(dbShare);
            foreach (DataRow row in data.Rows)
            {
                line = string.Empty;
                int nr = Convert.ToInt32(row["NRKOLONASH"]);
                int kodi = Convert.ToInt32(row["K1"].ToString());
                if ((kodi <= 510 && !dataPerTuEksportuar.ColumnContainsValue("IDSHITJEKOKA", row["IDSHITJEKOKA"].ToString()))
                    ||
                    (kodi == 910 && !dataPerTuEksportuar.ColumnContainsValue("IDSHITJETRUPI", row["IDSHITJETRUPI"].ToString()))
                  )
                {
                    continue;
                }
                for (int i = 1; i <= nr; i++)
                {
                    string fusha = row[$"K{i}"].ToString();
                    if (GetFirstValue(rekordeTeDetyrueshme, "KODI", kodi.ToString(), "FUSHATEDETYRUESHME").Contains($"K{i}") && String.IsNullOrEmpty(fusha))
                        throw new MyException(Rm.GetString("msgFushaJoTePlotesuara", Ci));

                    line += $"{fusha}|";
                }
                lines.AppendLine(line);
                
            }

            return lines.ToString();
        }

        private FileInfo merrSkedarinEDitesSeSotmeNeseEkziston()
        {
            DirectoryInfo dir = new DirectoryInfo(rootPath);
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                if (file.FullName.Contains(preffix))
                    return file;
            }
            return null;
        }

        private string percaktoEmrinESkedaritTeRi(FileInfo skedarEkzistues)
        {
            int nrSerial = 1;
            if (skedarEkzistues != null)
            {
                string[] nameParts = skedarEkzistues.FullName.Split('\\');
                nrSerial += Convert.ToInt32(nameParts[nameParts.Length - 1].Split('_')[1].Substring(DateTime.Now.ToString("yyyyMMdd").Length).Split('.')[0]);
            }
            string nr = nrSerial < 10 ? $"0{nrSerial}" : nrSerial.ToString();
            return $"{preffix}{nr}.txt";
        }
        
         public string GetFirstValue(DataTable dt, string fieldName, string value, string column)
        {
            if (!dt.Columns.Contains(fieldName))
                throw new Exception($"Datatable doesn't contain column {fieldName}");
            if (!dt.Columns.Contains(column))
                throw new Exception($"Datatable doesn't contain column {column}");
    
            DataRow[] rows = dt.Select($"{fieldName} = '{value}'");
            if (rows.Length == 0)
                throw new Exception($"Column {fieldName} doesn't contain {value}");
    
            return rows[0][column].ToString();
        }
        
    }
}
