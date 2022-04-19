using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DbCore.DbImporte
{
    /// <summary>
    /// IMPORTI FLETEVE KONTABEL:
    /// Mban nje list objektesh clsImportKokaFleteKontabel (objekte kokash fletesh kontabel).
    /// Te dhenat merret nga tabela T_TEMP_KOKA_IMPORT_FLETEKONTABEL
    /// </summary>
    public class colImportKokaFleteKontabel : List<clsImportKokaFleteKontabel>
    {
        #region Konstruktori

        /// <summary>
        /// IMPORTI FLETEVE KONTABEL:
        /// Krijon nje list objektesh bosh te klases clsImportKokaFleteKontabel per koken e fleteve kontabel.
        /// </summary>
        public colImportKokaFleteKontabel()
        {
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// IMPORTI FLETEVE KONTABEL:
        /// Ruan te gjitha fletet kontabel per import ne tabelat temporale per import nga tabela te jashtme (2).
        /// </summary>
        /// <returns>Kthen clsMesazh me atribut statusi True nese ruajtja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        public clsMesazh ruajImportiKokaFleteKontabelNeTabelaTemporale(clsDatabazeImporte moduliImporte, int idNdermarrje)
        {
            bool transaksionIRi = false;
            if (moduliImporte == null)
            {
                moduliImporte = new clsDatabazeImporte();
                moduliImporte.beginTransaksion();
                transaksionIRi = true;
            }
            clsMesazh pergjigja = new clsMesazh(true, "Ruajtja u krye me sukses!");
            foreach (clsImportKokaFleteKontabel kokaPerInsert in this)
            {
                pergjigja = kokaPerInsert.ruajKokaPerImport(moduliImporte, idNdermarrje);
                if (!pergjigja.Status)
                    break;
            }

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
        /// Modifikon te gjitha kokat e importuara si te importuara qe te mos terhiqen prape.
        /// </summary>
        /// <returns>Kthen clsMesazh me atribut statusi True nese modifikimi kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        public clsMesazh modifikoStatusImportiKokaFleteKontabel(clsDatabazeImporte moduliImporte)
        {
            bool transaksionIRi = false;
            if (moduliImporte == null)
            {
                moduliImporte = new clsDatabazeImporte();
                moduliImporte.beginTransaksion();
                transaksionIRi = true;
            }
            clsMesazh pergjigja = new clsMesazh(true, "Modifikimi u krye me sukses!");
            foreach (clsImportKokaFleteKontabel kokaModifikuar in this)
            {
                pergjigja = kokaModifikuar.modifikoKokaPasImport(moduliImporte);
                if (!pergjigja.Status)
                    break;
            }

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
        /// Merr te gjithe kokat e dokumenteve per importim te fleteve kontabel (3).
        /// </summary>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate marrjes se kokave te dokumenteve ose False ne te kundert.</returns>
        public bool merrKokaFleteKontabelTePaImportuara()
        {
            clsDatabazeImporte moduleImporti = new clsDatabazeImporte();
            bool pergjigja = mbushKokaFleteKontabelPerImport(moduleImporti.ktheKokaFleteKontabelTePaImportuara());
            moduleImporti.Dispose();
            return pergjigja;
        }

        /// <summary>
        /// IMPORTI FLETEVE KONTABEL:
        /// Merr te gjithe kokat e dokumenteve per importim te fleteve kontabel si DataTable.
        /// </summary>        
        public static DataTable merrKokaFleteKontabelTePaImportuaraDt()
        {
            clsDatabazeImporte moduleImporti = new clsDatabazeImporte();
            DataTable dt = moduleImporti.ktheKokaFleteKontabelTePaImportuara();
            moduleImporti.Dispose();
            return dt;
        }

        /// <summary>
        /// IMPORTI FLETEVE KONTABEL:
        /// Merr te gjithe dokumentat per importim te fleteve kontabel (1).
        /// </summary>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate marrjes se dokumenteve ose False ne te kundert.</returns>
        public bool merrFleteKontabelAlbSigTePaImportuara(int idNdermarrje)
        {
            clsDatabazeImporte moduleImporti = new clsDatabazeImporte();
            bool pergjigja = organizoKokaTrupTeDataTable(moduleImporti.ktheFleteKontabelTePaImportuaraAlbSig(), idNdermarrje);
            moduleImporti.Dispose();
            return pergjigja;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// IMPORTI FLETEVE KONTABEL:
        /// Metode qe sherben per te marre te dhenat nga store procedura ne kod te lidhura me tabelen T_TEMP_KOKA_IMPORT_FLETEKONTABEL ne nje list objektesh clsImportKokaFleteKontabel.
        /// </summary>
        /// <param name="dt">(DataTable) Merr si parameter nje datatable qe kthehet nga store procedura ne kod per te mbushur nje list me objekte.</param>
        /// <returns>(bool) Kthen true nese nuk ndodh asnje gabim gjate leximit te te dhenave, ne te kundert false.</returns>
        private bool mbushKokaFleteKontabelPerImport(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsImportKokaFleteKontabel kokaFleteKontabelPerImport = new clsImportKokaFleteKontabel();
                    //kokaFleteKontabelPerImport.mbushKokaFleteKontabelImport(rreshti);
                    Add(new clsImportKokaFleteKontabel(rreshti));
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
        /// Metode qe sherben per te marre te dhenat nga store procedura ne kod te lidhura me tabelen T_TEMP_IMPORTPOLICA ne nje list objektesh clsImportKokaFleteKontabel.
        /// </summary>
        /// <param name="dt">(DataTable) Merr si parameter nje datatable qe kthehet nga store procedura ne kod per te mbushur nje list me objekte.</param>
        /// <returns>(bool) Kthen true nese nuk ndodh asnje gabim gjate leximit te te dhenave, ne te kundert false.</returns>
        private bool mbushFleteKontabelPerImportAlbSig(DataTable dt, int idNdermarrje)
        {
            try
            {
                clsImportKokaFleteKontabel fleteKontabelPerImportAlbSig = new clsImportKokaFleteKontabel();
                fleteKontabelPerImportAlbSig.mbushFleteKontabelImportAlbSig(dt, idNdermarrje);
                Add(fleteKontabelPerImportAlbSig);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// IMPORTI FLETEVE KONTABEL:
        /// Metode qe organizon rreshtat e tabeles temporale te AlbSig-ut ne rreshtat per import te trupit dhe kokes.
        /// </summary>
        /// <param name="dt">(DataTable) Merr si parameter nje datatable qe kthehet nga store procedura ne kod per te mbushur nje list me objekte.</param>
        /// <returns>(bool) Kthen true nese nuk ndodh asnje gabim gjate leximit te te dhenave, ne te kundert false.</returns>
        private bool organizoKokaTrupTeDataTable(DataTable dt, int idNdermarrje)
        {
            //Merr vlerat distinct te dates.
            DataTable dataGrupime = dt.DefaultView.ToTable(true, "DATA_DOKUMENTIT");
            //Krijimi i nje flete kontabel per date.
            foreach (DataRow dataUnike in dataGrupime.Rows)
            {
                //Mbush koken dhe trupin te fletes kontabel te importit.
                DataTable teDhenatPerFK = dt.Select(String.Format("DATA_DOKUMENTIT = '{0}'", dataUnike["DATA_DOKUMENTIT"])).CopyToDataTable();
                bool pergjigje = mbushFleteKontabelPerImportAlbSig(teDhenatPerFK, idNdermarrje);
                if (!pergjigje)
                    return pergjigje;
            }
            return true;
        }

        #endregion
    }
}