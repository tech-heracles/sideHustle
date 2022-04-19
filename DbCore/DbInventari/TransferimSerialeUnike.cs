using DbCore.DbAdmin;
using DbCore.DbRegjistrim;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.Messages;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.DbInventari
{
    public class TransferimSerialeUnike
    {

        /// <summary>
        /// Dergon serialet unike tek teknika per aktivizim
        /// </summary>
        /// <param name="idNdermarje"></param>
        /// <param name="kategoriSeriali"></param>
        /// <param name="data"></param>
        /// <param name="idLlojDokumentMag"></param>
        /// <param name="idMetoda"></param>
        /// <returns></returns>
        public static (clsMesazh, clsMesazh) dergoFileTransferimSerialeUnike(int idNdermarje, string kategoriSeriali, string data, int idLlojDokumentMag, int idMetoda)
        {
            clsMesazh sukses = new clsMesazh(true, "");
            clsMesazh error = new clsMesazh(false, "");
            string textFile = "";
            string mesazh = "";
            bool gabim;
            DataTable dt;
            using (var scope = new MyTransactionScope())
            {
                try
                {
                    using (clsDatabaseAdmin db = new clsDatabaseAdmin())
                        dt = db.merrSerialeUnikePerTransferim(idNdermarje, kategoriSeriali, data, idLlojDokumentMag, idMetoda, out gabim);
                    ImbLogger.Info("U moren serialet unike per transferim.");
                    if (gabim)
                    {
                        ImbLogger.Info($"Ka seriale qe gjenden me shume se nje here ne file. Transferimi nuk u krye.");
                        return (new clsMesazh(true, ""), new clsMesazh(false, "Ka seriale qe gjenden me shume se nje here ne file. Transferimi nuk mund te kryhet."));
                    }
                    textFile = krijoFileSerialeUnikeTransferimNgaDataTable(dt);
                    ImbLogger.Info("U krijua file per transferimin e serialeve unike.");

                    Ftp.clsFtpManager ftpManager = new Ftp.clsFtpManager(idNdermarje, idMetoda, kategoriSeriali);
                    ImbLogger.Info($"U moren konfigurimet FTP te ndermarrjes me id {idNdermarje} per metoden {idMetoda}.");

                    byte[] txtFileBytes = Encoding.ASCII.GetBytes(textFile);
                    ImbLogger.Info("Skedari u kodua.");
                    string emerFile = "";
                    clsDatabaseAdmin dataAdm = new clsDatabaseAdmin();
                    switch (kategoriSeriali)
                    {
                        case "APARATE":
                            emerFile = dataAdm.merrEmerPerSkedarTransferimi((idLlojDokumentMag == 1) ? "APARATEBLERJE" : "APARATESHITJE", data, dt.Rows.Count, idMetoda, out emerFile);
                            break;
                        case "KARTA":
                        case "RINGARKUES":
                            emerFile = dataAdm.merrEmerPerSkedarTransferimi(kategoriSeriali, data, dt.Rows.Count, idMetoda, out emerFile);
                            break;
                    }

                    (sukses, error) = ftpManager.sendFile(txtFileBytes, emerFile);
                    mesazh = string.Format(MessagesResource.Messages["msgTransferimSeriale"], dt.Rows.Count, sukses.PershkrimMesazhi, ((SerialeUnike_MetodeTransferimi)idMetoda).ToString());

                    if (sukses.PershkrimMesazhi != "")
                        sukses.PershkrimMesazhi = mesazh;
                    if (error.PershkrimMesazhi != "" && sukses.PershkrimMesazhi == "")
                    {
                        ImbLogger.Info(error.PershkrimMesazhi);
                        return (sukses, error);
                    }
                    if (error.PershkrimMesazhi != "")
                        ImbLogger.Error(error.PershkrimMesazhi);
                        ImbLogger.Info(mesazh);

                    if (dt.Rows.Count != 0)
                    {
                        DataTable transferuar = new DataTable();
                        transferuar.Columns.Add("ID", typeof(Int32));
                        transferuar.Columns.Add("IDSERIALUNIK_LIDHJEMAG", typeof(Int32));
                        transferuar.Columns.Add("IDMETODETRANSFERIMI", typeof(Int32));
                        foreach (DataRow row in dt.Rows)
                        {
                            transferuar.AddRow(0, row[0], idMetoda);
                        }
                        using (clsDatabaseRegjistrim dr = new clsDatabaseRegjistrim())
                            dr.ruajSerialeUnikeTransferuar(transferuar);
                        ImbLogger.Info("U ruajt transferimi i serialeve.");
                    }

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    ImbLogger.Error(ex);
                    error.PershkrimMesazhi += ex.Message;
                }
            }
            return (sukses, error);
        }

        public static (clsMesazh, clsMesazh) dergoFileTransferimGjendjeAparate(int idNdermarje, string data, int idMetodeTransferimi)
        {
            clsMesazh mesazhi = new clsMesazh(true, "File u transferua me sukses");
            clsMesazh sukses = new clsMesazh(true, "File u dergua me sukses!");
            clsMesazh error = new clsMesazh(false, "");
            string textFile = "";
            string mesazh = "";
            DataTable dt;
            using (var scope = new MyTransactionScope())
            {
                try
                {
                    using (clsDatabaseAdmin db = new clsDatabaseAdmin())
                        dt = db.merrGjendjeSerialeUnikeAparte(idNdermarje, data, (idMetodeTransferimi == 2) ? "GJENDJEDITOREAPARATE" : "GJENDJEMUJOREAPARATE");
                    ImbLogger.Info("U moren gjendjet per serialet unike te Aparateve.");
                    textFile = krijoFileSerialeUnikeTransferimNgaDataTable(dt);
                    ImbLogger.Info("U krijua file per transferimin e serialeve unike.");
                    Ftp.clsFtpManager ftpManager = new Ftp.clsFtpManager(idNdermarje, idMetodeTransferimi); 
                    ImbLogger.Info($"U moren konfigurimet FTP te ndermarrjes me id {idNdermarje} per metoden me id {idMetodeTransferimi}.");
                    byte[] txtFileBytes = Encoding.ASCII.GetBytes(textFile);
                    ImbLogger.Info("Skedari u kodua.");
                    string emerFile = "";
                    using (clsDatabaseAdmin dataAdm = new clsDatabaseAdmin())
                        emerFile = dataAdm.merrEmerPerSkedarTransferimi((idMetodeTransferimi == 2) ? "GJENDJEDITOREAPARATE" : "GJENDJEMUJOREAPARATE", data, dt.Rows.Count, idMetodeTransferimi, out emerFile);
                        (sukses, error) = ftpManager.sendFile(txtFileBytes, emerFile);
                        mesazh = string.Format(MessagesResource.Messages["msgTransferimSeriale"], dt.Rows.Count, sukses.PershkrimMesazhi, ((SerialeUnike_MetodeTransferimi)idMetodeTransferimi).ToString());

                    if (sukses.PershkrimMesazhi != "")
                        sukses.PershkrimMesazhi = mesazh;

                    if (error.PershkrimMesazhi != "" && sukses.PershkrimMesazhi == "")
                    {
                        ImbLogger.Info(error.PershkrimMesazhi);
                        return (sukses, error);
                    }
                    if (error.PershkrimMesazhi != "")
                        ImbLogger.Error(error.PershkrimMesazhi);
                        ImbLogger.Info(mesazh);
                        scope.Complete();
                }
                catch (Exception ex)
                {
                    ImbLogger.Error(ex);
                    mesazhi.PershkrimMesazhi = ex.Message;
                    mesazhi.Status = false;
                }
            }
            return (sukses, error);
        }

        /// <summary>
        /// Krijon tekstin e file-it me serialet unike qe do transferohen
        /// </summary>
        /// <param name="emerIRiSkedari"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        private static string krijoFileSerialeUnikeTransferimNgaDataTable(DataTable dt)
        {
            StringBuilder lines = new StringBuilder();
            string line;
            int nrKol = dt.Columns.Count;
            foreach (DataRow row in dt.Rows)
            {
                line = string.Empty;
                for (int i = 1; i < nrKol; i++)
                {
                    string fusha = row[i]?.ToString();
                    //if (!String.IsNullOrEmpty(fusha))
                    line += (i == nrKol - 1) ? $"{fusha}" : $"{fusha}|";
                        //line += $"{fusha}|";
                }
                //lines.AppendLine(line.TrimEnd('|'));
                lines.Append(line + '\n');
            }
            ImbLogger.Info("U krijuan rreshtat e permbajtjes se filet te transferimit te serialeve unike.");
            return lines.ToString();
        }
    }
}
