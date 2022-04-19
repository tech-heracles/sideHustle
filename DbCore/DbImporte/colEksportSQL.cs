using DbCore.DbAdmin;
using DbCore.IMBUtils;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using AlphaWeb.Core.SharedKernel;

namespace DbCore.DbImporte
{
    /// <summary>
    /// EKSPORTI NE TABELA SQL:
    /// Klase eshte ndertuar per te mbajtur funksione qe shkruajne te dhenat ne tabelat e eksportit.    
    /// </summary>
    public class colEksportSQL
    {
        /// <summary>
        /// EKSPORT I DOKUMENTEVE:
        /// Ruan tek tabelat T_TEMP_EKSPORTSHITJE te gjithe kokat e dokumenteve te tabeles qe i kalohet. Dhe me tej shton tek tabela T_TEMP_TRUPIEKSPORTSHITJE trupat e dokumenteve.
        /// </summary>
        /// <param name="dt">(DataTable) DataTable ku jane dokumentat qe duhen eksportuar.</param>
        /// <returns>Kthen True nese ruajtja ne tabelat e eksportit perfundon me sukses, ose False nese ndodh ndonje gabim.</returns>
        public static clsMesazh ruajDokumentaNeTabeleEksporti(DataTable dtKoka, DataTable dtTrupi, DbAdmin.colTrupiFormatImporti col, string emerTabKoka, string emerTabTrupi, int kategoria, string emerTabRec, DataTable dtRec, int idSuperKategori)
        {
            using (var scope = new MyTransactionScope())
            using (clsDatabazeImporte moduliImporte = new clsDatabazeImporte())
            {
                if (idSuperKategori == (int)SuperKategori.Regjistrime)
                    HiqDokumentaTeEksportuarNgaTrupi(dtKoka, dtTrupi, dtRec, col, emerTabKoka, kategoria == 45, moduliImporte);


                clsMesazh mesazh;
                mesazh = moduliImporte.shtoFaturaNeTabeleEksporti(dtKoka, col, emerTabKoka, 1, kategoria);
                if (!mesazh.Status)
                {
                    return mesazh;
                }
                if (idSuperKategori == (int)SuperKategori.Regjistrime)
                {
                    mesazh = moduliImporte.shtoFaturaNeTabeleEksporti(dtTrupi, col, emerTabTrupi, 2, kategoria);
                    if (!mesazh.Status)
                    {
                        return mesazh;
                    }
                    if (kategoria == 45)
                    {
                        mesazh = moduliImporte.shtoFaturaNeTabeleEksporti(dtRec, col, emerTabRec, 4, kategoria);
                        if (!mesazh.Status)
                        {
                            return mesazh;
                        }
                    }
                }
                scope.Complete();
                return new MesazhSuksesi(IMBUtils.Messages.MessagesResource.Messages["msgEksportimiMeSukses"]);

            }
        }

        private static void HiqDokumentaTeEksportuarNgaTrupi(DataTable dtKoka, DataTable dtTrupi, DataTable dtRec, colTrupiFormatImporti col, string emerTabKoka, bool meReceptura, clsDatabazeImporte moduliImporte)
        {
            var primaryKey = col.FirstOrDefault(x => x.FusheKokeApoTrupi == 3).EmerImporti;
            var ids = string.Join(",", dtKoka.GetColumnAsList(primaryKey));

            var result = moduliImporte.GjejDokumentaEkzistues(emerTabKoka, primaryKey, ids);
            if (result == null)
                return;

            var existingIds = result.GetColumnAsList(primaryKey);

            dtKoka.RemoveRows(primaryKey, existingIds);

            if (meReceptura)
            {
                string idTrupiPrimaryKey = col.FirstOrDefault(x => x.FusheKokeApoTrupi == 5).EmerImporti;
                var idTrupi = MerrIdteETrupit(dtTrupi, primaryKey, idTrupiPrimaryKey, existingIds);
                dtRec.RemoveRows(idTrupiPrimaryKey, idTrupi);
            }

            dtTrupi.RemoveRows(primaryKey, existingIds);


        }

        private static List<string> MerrIdteETrupit(DataTable source, string headerName, string bodyColumnName, List<string> headerIds)
        {
            if (!source.Columns.Contains(headerName))
                throw new Exception($"DataTable does not contain column {headerName}");
            if (!source.Columns.Contains(bodyColumnName))
                throw new Exception($"DataTable does not contain column {bodyColumnName}");
            List<string> values = new List<string>();
            foreach (DataRow row in source.Rows)
            {
                foreach (var id in headerIds)
                {
                    if (row[headerName].ToString() == id)
                        values.Add(row[bodyColumnName].ToString());
                }
            }
            return values;
        }
    }
}
