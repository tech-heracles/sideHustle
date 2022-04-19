using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DbCore.DbImporte
{
    /// <summary>
    /// IMPORTI FLETEVE KONTABEL:
    /// Mban nje list objektesh clsImportTrupiFleteKontabel (objekte trupash fletesh kontabel).
    /// Te dhenat merret nga tabela T_TEMP_TRUPI_IMPORT_FLETEKONTABEL
    /// </summary>
    public class colImportTrupiFleteKontabel : List<clsImportTrupiFleteKontabel>
    {
        #region Konstruktori

        /// <summary>
        /// IMPORTI FLETEVE KONTABEL:
        /// Krijon nje list objektesh bosh te klases clsImportTrupiFleteKontabel per trupin e fleteve kontabel.
        /// </summary>
        public colImportTrupiFleteKontabel()
        {
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// IMPORTI FLETEVE KONTABEL:
        /// Ruan te gjitha trupat e fleteve fletet kontabel per import ne tabelat temporale per import nga tabela te jashtme.
        /// </summary>
        /// <returns>Kthen clsMesazh me atribut statusi True nese ruajtja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        public clsMesazh ruajImportiTrupiFleteKontabelNeTabelaTemporale(clsDatabazeImporte moduliImporte, int idKokaImport)
        {
            clsMesazh pergjigja = new clsMesazh(true, "Ruajtja u krye me sukses!");
            foreach (clsImportTrupiFleteKontabel importiTrupi in this)
            {
                importiTrupi.IdKokaImport = idKokaImport;
                pergjigja = importiTrupi.ruajTrupiPerImport(moduliImporte);
                if (!pergjigja.Status)
                    return pergjigja;
            }
            return pergjigja;
        }

        /// <summary>
        /// IMPORTI FLETEVE KONTABEL:
        /// Merr te gjithe trupat e dokumenteve per importim te fleteve kontabel.
        /// </summary>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate marrjes se trupave te dokumenteve ose False ne te kundert.</returns>
        public bool merrTrupiFleteKontabelSipasIdDokumentiImportuar(int idDokumentiKoka, int idKokaImport)
        {
            clsDatabazeImporte moduleImporti = new clsDatabazeImporte();
            bool pergjigja = mbushTrupFleteKontabelPerImport(moduleImporti.ktheTrupiFleteKontabelSipasIdDokumentiImportuar(idDokumentiKoka, idKokaImport));
            moduleImporti.Dispose();
            return pergjigja;
        }

        /// <summary>
        /// IMPORTI FLETEVE KONTABEL:
        /// Merr te gjithe trupat e dokumenteve per importim te fleteve kontabel.
        /// </summary>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate marrjes se trupave te dokumenteve ose False ne te kundert.</returns>
        public static DataTable merrTrupiFleteKontabelSipasIdDokumentiImportuarDt(int idDokumentiKoka, int idKokaImport)
        {
            clsDatabazeImporte moduleImporti = new clsDatabazeImporte();
            DataTable dt = moduleImporti.ktheTrupiFleteKontabelSipasIdDokumentiImportuar(idDokumentiKoka, idKokaImport);
            moduleImporti.Dispose();
            return dt;
        }

        /// <summary>
        /// IMPORTI FLETEVE KONTABEL:
        /// Krjion trupin e fleteve kontabel.
        /// </summary>
        /// <param name="dt">(DataTable) Te dhenat qe duhet te shnderrohen ne nje koleksion trupi fletesh kontabel nga tabela e albsigut.</param>
        /// <param name="vleftaShumatore">(float) Vlefta shumatore e gjithe trupit.</param>
        /// <param name="idDokumentImport">(int) Id e dokumentit qe po i krijohet trupi.</param>
        /// <returns>Kthen nje list objektesh clsImportTrupiFleteKontabel duke krijuar nje koleksion.</returns>
        public static colImportTrupiFleteKontabel krijoTrupFleteKontabelPerImportAlbsig(DataTable dt, out double vleftaShumatore, int idDokumentImport)
        {
            colImportTrupiFleteKontabel trupiPerImport = new colImportTrupiFleteKontabel();
            trupiPerImport.mbushTrupFleteKontabelPerImportAlbsig(dt, out vleftaShumatore, idDokumentImport);
            return trupiPerImport;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// IMPORTI FLETEVE KONTABEL:
        /// Metode qe sherben per te marre te dhenat nga store procedura ne kod te lidhura me tabelen T_TEMP_TRUPI_IMPORT_FLETEKONTABEL ne nje list objektesh clsImportTrupiFleteKontabel.
        /// </summary>
        /// <param name="dt">(DataTable) Merr si parameter nje datatable qe kthehet nga store procedura ne kod per te mbushur nje list me objekte.</param>
        /// <returns>(bool) Kthen true nese nuk ndodh asnje gabim gjate leximit te te dhenave, ne te kundert false.</returns>
        private bool mbushTrupFleteKontabelPerImport(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsImportTrupiFleteKontabel trupFleteKontabelPerImport = new clsImportTrupiFleteKontabel();
                    //trupFleteKontabelPerImport.mbushTrupFleteKontabelImport(rreshti);
                    Add(new clsImportTrupiFleteKontabel(rreshti));
                }
            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }

        /// <summary>
        /// IMPORTI FLETEVE KONTABEL:
        /// Metode per krijimin e objektit colImportTrupiFleteKontabel nga tabela e AlbSig per import.
        /// </summary>
        /// <param name="dt">(DataTable) Te dhenat qe duhet te shnderrohen ne nje koleksion trupi fletesh kontabel nga tabela e albsigut.</param>
        /// <param name="vleftaShumatore">(float) Vlefta shumatore e gjithe trupit.</param>
        /// <param name="idDokumentImport">(int) Id e dokumentit qe po i krijohet trupi.</param>
        /// <returns>(bool) Kthen true nese nuk ndodh asnje gabim gjate leximit te te dhenave, ne te kundert false.</returns>
        private bool mbushTrupFleteKontabelPerImportAlbsig(DataTable dt, out double vleftaShumatore, int idDokumentImport)
        {
            vleftaShumatore = 1;
            try
            {
                double vleftaShumatoreAlbSig = 0;
                foreach (DataRow rreshti in dt.Rows)
                {
                    double vleftaAktuale = 0;
                    clsImportTrupiFleteKontabel trupFleteKontabelPerImport = new clsImportTrupiFleteKontabel();
                    trupFleteKontabelPerImport.mbushTrupFleteKontabelImportAlbsig(rreshti, out vleftaAktuale, idDokumentImport);
                    vleftaShumatoreAlbSig = vleftaShumatoreAlbSig + vleftaAktuale;
                    Add(trupFleteKontabelPerImport);
                }
                vleftaShumatore = vleftaShumatoreAlbSig;
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        #endregion
    }
}
