using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DbCore.DbImporte
{
    /// <summary>
    /// IMPORTI FLETEVE KONTABEL:
    /// Klase e ndertuar per te mbajtur objektin e kokes se dokumentit per import te fleteve kontabel.
    /// Te dhenat merret nga tabela T_TEMP_KOKA_IMPORT_FLETEKONTABEL.
    /// </summary>
    public class clsImportKokaFleteKontabel
    {
        #region Atribute

        private int idKokaImport;
        private int idKokaDokumentit;
        private string nrKokaFleteKontabel;
        private DateTime dateDokumentiFleteKontabel;
        private string pershkrimiFleteKontabel;
        private double vleftaFleteKontabel;
        private string konfigAmbjenti;
        private bool importuar;
        private DateTime dateEksporti;
        private DateTime dateImporti;
        private colImportTrupiFleteKontabel oColTrupi;
        private int idNdermarrje;
        private DataRow rreshti;

        #endregion

        #region Properties

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id automatike te kokes se dokumentit te importit.
        /// </summary>
        public int IdKokaImport
        {
            get { return idKokaImport; }
            set { idKokaImport = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id automatike te kokes se dokumentit nga importuesi. 
        /// Eshte nje vlere per te patur lidhjen mes trupit dhe kokes me te lehte dhe per dergimin nga useri qe po importon.
        /// </summary>
        public int IdKokaDokumentit
        {
            get { return idKokaDokumentit; }
            set { idKokaDokumentit = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (string) Merr ose jep vlere nr te fletes kontabel. 
        /// </summary>
        public string NrKokaFleteKontabel
        {
            get { return nrKokaFleteKontabel; }
            set { nrKokaFleteKontabel = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (DateTime) Merr ose jep vlere dates se dokumentit te fletes kontabel. 
        /// </summary>
        public DateTime DateDokumentiFleteKontabel
        {
            get { return dateDokumentiFleteKontabel; }
            set { dateDokumentiFleteKontabel = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (string) Merr ose jep vlere pershkrimit te fletes kontabel. 
        /// </summary>
        public string PershkrimiFleteKontabel
        {
            get { return pershkrimiFleteKontabel; }
            set { pershkrimiFleteKontabel = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (float) Merr ose jep vlere vleftes se fletes kontabel.
        /// </summary>
        public double VleftaFleteKontabel
        {
            get { return vleftaFleteKontabel; }
            set { vleftaFleteKontabel = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (string) Merr ose jep vlere llojin e dokumentit qe po importohet.
        /// </summary>
        public string KonfigAmbjenti
        {
            get { return konfigAmbjenti; }
            set { konfigAmbjenti = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (bool) Merr ose jep vlere nese transaksioni eshte i importuar apo jo. True nese eshte i importuar, False ne te kundert.
        /// </summary>
        public bool Importuar
        {
            get { return importuar; }
            set { importuar = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (DateTime) Merr ose jep vlere dates se kur perdoruesi e ka derguar ne tabelat e importit.
        /// </summary>
        public DateTime DateEksporti
        {
            get { return dateEksporti; }
            set { dateEksporti = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere dates se kur perdoruesi e ka kryer suksesshme importin ne alpha web.
        /// </summary>
        public DateTime DateImporti
        {
            get { return dateImporti; }
            set { dateImporti = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (colImportTrupiFleteKontabel) Merr ose jep vlere trupit te kokes se amortizimit.
        /// </summary>
        public colImportTrupiFleteKontabel OColTrupi
        {
            get { return oColTrupi; }
            set { oColTrupi = value; }
        }

        /// <summary>        
        /// (int) Merr ose jep vlere id automatike te kokes se dokumentit te importit.
        /// </summary>
        public int IdNdermarrje
        {
            get { return idNdermarrje; }
            set { idNdermarrje = value; }
        }

        #endregion

        #region Kontruktori

        /// <summary>
        /// Koka e dokumentit te fletes kontabel qe do importohet.
        /// </summary>
        public clsImportKokaFleteKontabel()
        {
        }

        public clsImportKokaFleteKontabel(DataRow rreshti)
        {
            
            mbushKokaFleteKontabelImport(rreshti);
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// IMPORTI FLETEVE KONTABEL:
        /// Metoda perdoret per te mbushur te gjitha atributet e objektit clsImportKokaFleteKontabel sipas te dhenave qe vijne nga cdo rresht i databazes nga tabela T_TEMP_KOKA_IMPORT_FLETEKONTABEL.
        /// </summary>
        /// <param name="dbDataRowImportKokaFleteKontabel">(DataRow) Merr si parameter vetem nje rresht te kthyer nga tabelat ne databaze per te mbushur nje objekt.</param>
        /// <returns>(bool) Kthen true nese nuk ndodh asnje gabim gjate leximit te te dhenave, ne te kundert false.</returns>
        internal bool mbushKokaFleteKontabelImport(DataRow dbDataRowImportKokaFleteKontabel)
        {
            if (dbDataRowImportKokaFleteKontabel == null)
                return false;
            try
            {
                int.TryParse(dbDataRowImportKokaFleteKontabel["ID_KOKAIMPORT"].ToString(), out idKokaImport);
                int.TryParse(dbDataRowImportKokaFleteKontabel["ID_DOKUMENTIMPORTUAR"].ToString(), out idKokaDokumentit);
                nrKokaFleteKontabel = dbDataRowImportKokaFleteKontabel["NRKOKAFLETEKONTABEL"].ToString();
                DateTime.TryParse(dbDataRowImportKokaFleteKontabel["DATEDOKUMENTIKOKAFLETEKONTABEL"].ToString(), out dateDokumentiFleteKontabel);
                pershkrimiFleteKontabel = dbDataRowImportKokaFleteKontabel["PERSHKRIMIFLETEKONTABEL"].ToString();
                double.TryParse(dbDataRowImportKokaFleteKontabel["VLEFTAFLETEKONTABEL"].ToString(), out vleftaFleteKontabel);
                konfigAmbjenti = dbDataRowImportKokaFleteKontabel["KONFIGAMBJENTI"].ToString();
                bool.TryParse(dbDataRowImportKokaFleteKontabel["IMPORTUAR"].ToString(), out importuar);
                DateTime.TryParse(dbDataRowImportKokaFleteKontabel["DATE_EKSPORTI"].ToString(), out dateEksporti);
                DateTime.TryParse(dbDataRowImportKokaFleteKontabel["DATE_IMPORTIMI"].ToString(), out dateImporti);
                int.TryParse(dbDataRowImportKokaFleteKontabel["IDNDERMARRJE"].ToString(), out idNdermarrje);
                return true;
            }
            catch (InvalidCastException)
            {
                throw new Exception("ERROR: Gabim gjate marrjes se kokes se fleteve kontabel per import nga db-ja");
            }
        }

        public static int merrNrFunditPerImportFleteKontAlbsig(int idNdermarrje, DateTime dt)
        {
            clsDatabazeImporte data = new clsDatabazeImporte();
            string nrKoka = data.ktheNrFunditKokaFleteKontabel(dt, idNdermarrje);
            int nr = 1;
            if (nrKoka != null)
                nr = int.Parse(nrKoka.Substring(nrKoka.LastIndexOf("_") + 1)) + 1;
            data.Dispose();
            return nr;
        }

        /// <summary>
        /// IMPORTI FLETEVE KONTABEL:
        /// Metoda perdoret per te mbushur te gjitha atributet e objektit clsImportKokaFleteKontabel sipas te dhenave qe vijne nga cdo rresht i databazes nga tabela T_TEMP_IMPORTPOLICA.
        /// </summary>
        /// <param name="rreshtatPerImportTrupi">(DataTable) Merr si parameter DataTable te kthyer nga tabelat ne databaze per te mbushur nje objekt.</param></param>
        /// <returns>(bool) Kthen true nese nuk ndodh asnje gabim gjate leximit te te dhenave, ne te kundert false.</returns>
        internal bool mbushFleteKontabelImportAlbSig(DataTable rreshtatPerImportTrupi, int idNdermarrje)
        {
            if (rreshtatPerImportTrupi == null)
                return false;
            try
            {
                idKokaDokumentit = merrIdKokaImportKokaFleteKontabelTeFundit() + 1;
                //Mund te behet me nje numer automatik
                //nrKokaFleteKontabel = "ASI" + (merrIdKokaImportKokaFleteKontabelTeFundit() + 1);
                
                DateTime.TryParse(rreshtatPerImportTrupi.Rows[0]["DATA_DOKUMENTIT"].ToString(), out dateDokumentiFleteKontabel);
                string data =  dateDokumentiFleteKontabel.Day + "/" + dateDokumentiFleteKontabel.Month + "/" + dateDokumentiFleteKontabel.Year;
                nrKokaFleteKontabel = "Shitja_" + data + "_" + merrNrFunditPerImportFleteKontAlbsig(idNdermarrje, dateDokumentiFleteKontabel);
                pershkrimiFleteKontabel = "Import flete kontabel date " + dateDokumentiFleteKontabel;
                konfigAmbjenti = "FleteKontabel";
                bool.TryParse(rreshtatPerImportTrupi.Rows[0]["STATUSI"].ToString(), out importuar);
                dateEksporti = DateTime.Now;
                dateImporti = new DateTime(9999, 12, 31);
                this.idNdermarrje = idNdermarrje;
                double shumatoreVlefte = 0;
                //Mbush trupin e fletes kontabel.
                oColTrupi = colImportTrupiFleteKontabel.krijoTrupFleteKontabelPerImportAlbsig(rreshtatPerImportTrupi, out shumatoreVlefte, idKokaDokumentit);
                //Merr shumatoren e trupit.
                vleftaFleteKontabel = shumatoreVlefte;
                return true;
            }
            catch (InvalidCastException)
            {
                throw new Exception("ERROR: Gabim gjate marrjes se fleteve kontabel AlbSig per import nga db-ja");
            }
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// IMPORTI FLETEVE KONTABEL:
        /// Ruan objektin e rreshtit te kokes se dokumentit te fletes kontabel qe do vendosen ne tabelat e importit.
        /// </summary>
        /// <param name="moduliImporte">(clsDatabazeImporte) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen True nese ruajtja perfundon me sukses, ose False nese ruajtja jep gabim.</returns>
        public clsMesazh ruajKokaPerImport(clsDatabazeImporte moduliImporte, int idNdermarrje)
        {
            bool transaksionIRi = false;
            if (moduliImporte == null)
            {
                moduliImporte = new clsDatabazeImporte();
                moduliImporte.beginTransaksion();
                transaksionIRi = true;
            }            
            clsMesazh pergjigja = ruajKokaFleteKontabelPerImport(moduliImporte, idNdermarrje);

            if (transaksionIRi)
            {
                if (!pergjigja.Status)
                {
                    moduliImporte.rollbackTransaksion();
                    return pergjigja;
                }
                moduliImporte.commitTransaksion();
            }
            return pergjigja;
        }

        /// <summary>
        /// IMPORTI FLETEVE KONTABEL:
        /// Modifikon objektin e rreshtit te kokes se dokumentit te fletes kontabel qe po importohet per statusin e importimit.
        /// </summary>
        /// <param name="moduliImporte">(clsDatabazeImporte) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen True nese modifikimi perfundon me sukses, ose False nese modifikimi jep gabim.</returns>
        public clsMesazh modifikoKokaPasImport(clsDatabazeImporte moduliImporte)
        {
            bool transaksionIRi = false;
            if (moduliImporte == null)
            {
                moduliImporte = new clsDatabazeImporte();
                moduliImporte.beginTransaksion();
                transaksionIRi = true;
            }

            clsMesazh pergjigja = modifikoKokaFleteKontabelPasImport(moduliImporte);

            if (transaksionIRi)
            {
                if (!pergjigja.Status)
                {
                    moduliImporte.rollbackTransaksion();
                    return pergjigja;
                }
                moduliImporte.commitTransaksion();
            }

            return pergjigja;
        }

        /// <summary>
        /// IMPORTI FLETEVE KONTABEL:
        /// Merr id e kokes se dokumentit te importit te fleteve kontabel te fundit te regjistruar.
        /// </summary>
        /// <returns>Kthen id e kokes se dokumentit te importit te fleteve kontabel te fundit te regjistruar, ose -1 ne te kundert.</returns>
        public static int merrIdKokaImportKokaFleteKontabelTeFundit()
        {
            clsDatabazeImporte moduleImport = new clsDatabazeImporte();
            int pergjigja = moduleImport.ktheKokaFleteKontabelTeFundit();
            moduleImport.Dispose();
            return pergjigja;
        }

        /// <summary>
        /// IMPORTI FLETEVE KONTABEL:
        /// Merr id e kokes se dokumentit te importit te fleteve kontabel te fundit te regjistruar.
        /// </summary>
        /// <returns>Kthen id e kokes se dokumentit te importit te fleteve kontabel te fundit te regjistruar, ose -1 ne te kundert.</returns>
        public static clsMesazh fshiKokaFleteKont(int idKokaFleteKont)
        {
            clsDatabazeImporte moduleImport = new clsDatabazeImporte();
            clsMesazh pergjigja = moduleImport.fshiKokaFleteKontabel(idKokaFleteKont);
            moduleImport.Dispose();
            return pergjigja;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// IMPORTI FLETEVE KONTABEL:
        /// Ruan objektin e rreshtit te kokes se dokumentit te fletes kontabel qe do vendoset ne tabelat e importit.
        /// </summary>
        /// <param name="moduliImporte">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen True nese ruajtja perfundon me sukses, ose False nese ruajtja jep gabim.</returns>
        private clsMesazh ruajKokaFleteKontabelPerImport(clsDatabazeImporte moduliImporte, int idNdermarrje)
        {
            clsMesazh pergjigja = moduliImporte.ruajKokaFleteKontabelPerImport(out idKokaImport, idKokaDokumentit, nrKokaFleteKontabel, dateDokumentiFleteKontabel, pershkrimiFleteKontabel, vleftaFleteKontabel, konfigAmbjenti, importuar, dateEksporti, dateImporti, idNdermarrje);
            if (!pergjigja.Status)
                return pergjigja;

            pergjigja = oColTrupi.ruajImportiTrupiFleteKontabelNeTabelaTemporale(moduliImporte, idKokaImport);
            if (!pergjigja.Status)
                return pergjigja;

            pergjigja = moduliImporte.modifiFleteKontabelTeImportuaraAlbsig(dateDokumentiFleteKontabel);

            return pergjigja;
        }

        /// <summary>
        /// IMPORTI FLETEVE KONTABEL:
        /// Modifikon objektin e rreshtit te kokes se dokumentit te fletes kontabel qe po importohet per statusin e importimit.
        /// </summary>
        /// <param name="moduliImporte">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen True nese modifikimi perfundon me sukses, ose False nese modifikimi jep gabim.</returns>
        private clsMesazh modifikoKokaFleteKontabelPasImport(clsDatabazeImporte moduliImporte)
        {
            clsMesazh pergjigja = moduliImporte.modifikoKokaFleteKontabelTeImportuara(idKokaDokumentit, idKokaImport);
            return pergjigja;
        }

        public static clsMesazh modifikoKokaFleteKontabelPasImportitStatike(clsDatabazeImporte moduliImporte, int idKokaDokumentit, int idKokaImport)
        {
            clsMesazh pergjigja = moduliImporte.modifikoKokaFleteKontabelTeImportuara(idKokaDokumentit, idKokaImport);
            return pergjigja;
        }

        #endregion
    }
}