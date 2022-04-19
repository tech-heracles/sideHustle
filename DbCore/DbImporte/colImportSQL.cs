using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using AlphaWeb.Core.SharedKernel;
using DbCore.IMBUtils.Logging;
using DbCore.DbAdmin;

namespace DbCore.DbImporte
{
    public class colImportSQL
    {

        private enum Dokumenta { Koka = 1, Trupi = 2, Receptura = 4 };

        #region metoda publike

        public static DataTable merrObjektePerImportSQL(string emerTabEkz, string ndermarrjeKey, string ndermarjeKodi, bool merrTePaImportuara, bool rimerrTeImportuara, int? nrDokumentash)
        {
            clsDatabazeImporte db = new clsDatabazeImporte();
            DataTable dt = db.merrObjektePerImportSQL(emerTabEkz, ndermarrjeKey, ndermarjeKodi, merrTePaImportuara, rimerrTeImportuara, nrDokumentash);
            db.Dispose();
            return dt;
        }

        public static clsMesazh updateDokTabeleTemportal(string idDokImporti, int idNdermarrje, int statusi, string emerTabKoka, string emerFushePrimaryKey, string emerFusheNdermarrje, clsDatabazeImporte db)
        {
            return db.updateDokTabeleTemportal(idDokImporti, idNdermarrje, statusi, emerTabKoka, emerFushePrimaryKey, emerFusheNdermarrje);
        }

        public static clsMesazh ruajDokumentaNeTabeleImporti(DataTable dtKoka, DataTable dtTrupi, DbAdmin.colTrupiFormatImporti col, string emerTabKoka, string emerTabTrupi, int kategoria)
        {
            clsDatabazeImporte moduliImporte = new clsDatabazeImporte();
            moduliImporte.beginTransaksion();
            clsMesazh mesazh;
            mesazh = moduliImporte.shtoFaturaNeTabeleImporti(dtKoka, col, emerTabKoka, 1, kategoria, true);
            if (!mesazh.Status)
            {
                moduliImporte.rollbackTransaksion();
                return mesazh;
            }
            mesazh = moduliImporte.shtoFaturaNeTabeleImporti(dtTrupi, col, emerTabTrupi, 2, kategoria, true);
            if (!mesazh.Status)
            {
                moduliImporte.rollbackTransaksion();
                return mesazh;
            }
            moduliImporte.commitTransaksion();
            return new clsMesazh(true, "Shkrimi ne tabelen e importit u krye me sukses!");
        }

        public static clsMesazh transferoPolicaNeTabelaImporti(string emerTabeleKoka, string emerTabeleTrupi, int idNdermarrje, int idPerdoruesi, out int idKokaError)
        {
            idKokaError = 0;

            clsDatabazeImporte dbImport = new clsDatabazeImporte();
            try
            {
                dbImport.beginTransaksion(0);
                idKokaError = dbImport.transferoPolicaNeTabelaImporti(emerTabeleKoka, emerTabeleTrupi, idNdermarrje, idPerdoruesi);
                dbImport.commitTransaksion();
                return new clsMesazh(true, "Transferimi perfundoi me sukses!");
            }
            catch (Exception ex)
            {
                dbImport.rollbackTransaksion();
                ImbLogger.Error(ex.Message);
                return new clsMesazh(false, "Ndodhi nje gabim gjate transferimit!");
            }
        }

        public static void FshiDokumentatTeDuplikuar(string emerTabele, string primarykey)
        {
            using (var db = new clsDatabazeImporte())
            {
                db.FshiDokumentatTeDuplikuar(emerTabele, primarykey);
            }
        }

        public static bool ekzistonTabele(string emerTabele)
        {
            using (clsDatabazeImporte dbImport = new clsDatabazeImporte())
            {
                return dbImport.ekzistonTabele(emerTabele);
            }
        }

        public static clsMesazh modifikoDokumentNeTabeleImporti(List<string> fushaKoke, DataTable dtKoka, DataTable dtTrupi, colTrupiFormatImporti col, string emerTabKoka, string emerTabTrupi, int kategoria, string emerTabRec, DataTable dtRec, int idSuperKategori)
        {
            clsDatabazeImporte moduliImporte = new clsDatabazeImporte();
            try
            {
                moduliImporte.beginTransaksion();

                clsMesazh mesazh = moduliImporte.shtoFaturaNeTabeleImporti(dtKoka, col, emerTabKoka, 1, kategoria, false);
                if (!mesazh.Status)
                {
                    moduliImporte.rollbackTransaksion();
                    return mesazh;
                }
                if (idSuperKategori == (int)SuperKategori.Regjistrime)
                {
                    mesazh = moduliImporte.shtoFaturaNeTabeleImporti(dtTrupi, col, emerTabTrupi, 2, kategoria, false);
                    if (!mesazh.Status)
                    {
                        moduliImporte.rollbackTransaksion();
                        return mesazh;
                    }
                    if (kategoria == 45)
                    {
                        mesazh = moduliImporte.shtoFaturaNeTabeleImporti(dtRec, col, emerTabRec, 4, kategoria, false);
                        if (!mesazh.Status)
                        {
                            moduliImporte.rollbackTransaksion();
                            return mesazh;
                        }
                    }
                }
                moduliImporte.commitTransaksion();
                return new clsMesazh(true, "Shkrimi ne tabelen e importit u krye me sukses!");
            }
            catch (Exception ex)
            {
                moduliImporte.rollbackTransaksion();
                return new MesazhGabimi(ex.Message);
            }

            #endregion
        }

        public static clsMesazh fshiDokumentNgaTabelaTemporale(List<string> fushaKoke, DataTable dtKoka, DataTable dtTrupi, colTrupiFormatImporti col, string emerTabKoka, string emerTabTrupi, int kategoria, string emerTabRec, DataTable dtRec, int idSuperKategori)
        {
            clsDatabazeImporte moduliImporte = new clsDatabazeImporte();
            try
            {
                moduliImporte.beginTransaksion();

                clsMesazh mesazh = moduliImporte.fshiFaturaNgaTabeleImporti(dtKoka, col, emerTabKoka, 1, kategoria);
                if (!mesazh.Status)
                {
                    moduliImporte.rollbackTransaksion();
                    return mesazh;
                }
                if (idSuperKategori == (int)SuperKategori.Regjistrime)
                {
                    mesazh = moduliImporte.fshiFaturaNgaTabeleImporti(dtTrupi, col, emerTabTrupi, 2, kategoria);
                    if (!mesazh.Status)
                    {
                        moduliImporte.rollbackTransaksion();
                        return mesazh;
                    }
                    if (kategoria == 45)
                    {
                        mesazh = moduliImporte.fshiFaturaNgaTabeleImporti(dtRec, col, emerTabRec, 4, kategoria);
                        if (!mesazh.Status)
                        {
                            moduliImporte.rollbackTransaksion();
                            return mesazh;
                        }
                    }
                }
                moduliImporte.commitTransaksion();
                return new clsMesazh(true, "Shkrimi ne tabelen e importit u krye me sukses!");
            }
            catch (Exception ex)
            {
                moduliImporte.rollbackTransaksion();
                return new MesazhGabimi(ex.Message);
            }
        }
    }
}