using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DbCore.DbImporte
{
    /// <summary>
    /// IMPORTI FLETEVE KONTABEL:
    /// Klase e ndertuar per te mbajtur objektin e trupit te dokumentit per import te fleteve kontabel.
    /// Te dhenat merret nga tabela T_TEMP_TRUPI_IMPORT_FLETEKONTABEL.
    /// </summary>
    public class clsImportTrupiFleteKontabel
    {
        #region Atribute

        private int idTrupiImport;
        private int idKokaImport;
        private int idDokumentImporti;
        private string nrLlogarise;
        private string pershkrimTrupFleteKontabel;
        private string monedha;
        private double kursi;
        private double vleftaDebiMonHuaj;
        private double vleftaKrediMonHuaj;
        private double vleftaDebiMonBaze;
        private double vleftaKrediMonBaze;
        private DataRow rreshti;

        #endregion

        #region Properties

        /// <summary>
        /// IMPORTI FLETEVE KONTABEL:
        /// (int) Merr ose jep vlere id automatike te trupit te dokumentit te importit.
        /// </summary>
        public int IdTrupiImport
        {
            get { return idTrupiImport; }
            set { idTrupiImport = value; }
        }

        /// <summary>
        /// IMPORTI FLETEVE KONTABEL:
        /// (int) Merr ose jep vlere id automatike te kokes se dokumentit te importit 
        /// </summary>
        public int IdKokaImport
        {
            get { return idKokaImport; }
            set { idKokaImport = value; }
        }

        /// <summary>
        /// IMPORTI FLETEVE KONTABEL:
        /// (int) Merr ose jep vlere id se dokumentit te importit, id e dokumentit nga perdoruesi. 
        /// </summary>
        public int IdDokumentImporti
        {
            get { return idDokumentImporti; }
            set { idDokumentImporti = value; }
        }

        /// <summary>
        /// IMPORTI FLETEVE KONTABEL:
        /// (string) Merr ose jep vlere numrit te llogarise. 
        /// </summary>
        public string NrLlogarise
        {
            get { return nrLlogarise; }
            set { nrLlogarise = value; }
        }

        /// <summary>
        /// IMPORTI FLETEVE KONTABEL:
        /// (string) Merr ose jep vlere pershkrimit e trupit te fletes kontabel. 
        /// </summary>
        public string PershkrimTrupFleteKontabel
        {
            get { return pershkrimTrupFleteKontabel; }
            set { pershkrimTrupFleteKontabel = value; }
        }

        /// <summary>
        /// IMPORTI FLETEVE KONTABEL:
        /// (string) Merr ose jep vlere monedhes qe eshte dokumenti i fletes kontabel.
        /// </summary>
        public string Monedha
        {
            get { return monedha; }
            set { monedha = value; }
        }

        /// <summary>
        /// IMPORTI FLETEVE KONTABEL:
        /// (float) Merr ose jep vlere kursit te monedhes se huaj ne monedhe shqiptare.
        /// </summary>
        public double Kursi
        {
            get { return kursi; }
            set { kursi = value; }
        }

        /// <summary>
        /// IMPORTI FLETEVE KONTABEL:
        /// (float) Merr ose jep vlere vleftes debi ne monedhe te huaj.
        /// </summary>
        public double VleftaDebiMonHuaj
        {
            get { return vleftaDebiMonHuaj; }
            set { vleftaDebiMonHuaj = value; }
        }

        /// <summary>
        /// IMPORTI FLETEVE KONTABEL:
        /// (DateTime) Merr ose jep vlere vleftes kredi ne monedhe te huaj.
        /// </summary>
        public double VleftaKrediMonHuaj
        {
            get { return vleftaKrediMonHuaj; }
            set { vleftaKrediMonHuaj = value; }
        }

        /// <summary>
        /// IMPORTI FLETEVE KONTABEL:
        /// (int) Merr ose jep vlere vleftes debi ne monedhe baze.
        /// </summary>
        public double VleftaDebiMonBaze
        {
            get { return vleftaDebiMonBaze; }
            set { vleftaDebiMonBaze = value; }
        }

        /// <summary>
        /// IMPORTI FLETEVE KONTABEL:
        /// (int) Merr ose jep vlere vleftes kredi ne monedhe baze.
        /// </summary>
        public double VleftaKrediMonBaze
        {
            get { return vleftaKrediMonBaze; }
            set { vleftaKrediMonBaze = value; }
        }

        #endregion

        #region Kontruktori

        /// <summary>
        /// IMPORTI FLETEVE KONTABEL:
        /// Trupi i dokumentit te fletes kontabel qe do importohet.
        /// </summary>
        public clsImportTrupiFleteKontabel()
        {
        }

        public clsImportTrupiFleteKontabel(DataRow rreshti)
        {
            
            mbushTrupFleteKontabelImport(rreshti);
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// IMPORTI FLETEVE KONTABEL:
        /// Metoda perdoret per te mbushur te gjitha atributet e objektit clsImportTrupiFleteKontabel sipas te dhenave qe vijne nga cdo rresht i databazes nga tabela T_TEMP_TRUPI_IMPORT_FLETEKONTABEL.
        /// </summary>
        /// <param name="dbDataRowImportKokaFleteKontabel">(DataRow) Merr si parameter vetem nje rresht te kthyer nga tabelat ne databaze per te mbushur nje objekt.</param>
        /// <returns>(bool) Kthen true nese nuk ndodh asnje gabim gjate leximit te te dhenave, ne te kundert false.</returns>
        internal bool mbushTrupFleteKontabelImport(DataRow dbDataRowImportKokaFleteKontabel)
        {
            if (dbDataRowImportKokaFleteKontabel == null)
                return false;
            try
            {
                int.TryParse(dbDataRowImportKokaFleteKontabel["ID_TRUPIIMPORT"].ToString(), out idTrupiImport);
                int.TryParse(dbDataRowImportKokaFleteKontabel["ID_KOKA_IMPORT"].ToString(), out idKokaImport);
                int.TryParse(dbDataRowImportKokaFleteKontabel["ID_DOKUMENTIMPORTUAR"].ToString(), out idDokumentImporti);
                nrLlogarise = dbDataRowImportKokaFleteKontabel["NRLLOGARI"].ToString();
                pershkrimTrupFleteKontabel = dbDataRowImportKokaFleteKontabel["PERSHKRIMITRUPFLETEKONTABEL"].ToString();
                monedha = dbDataRowImportKokaFleteKontabel["MONEDHA"].ToString();
                double.TryParse(dbDataRowImportKokaFleteKontabel["KURSI"].ToString(), out kursi);
                double.TryParse(dbDataRowImportKokaFleteKontabel["VLEFTADEBIMONHUAJ"].ToString(), out vleftaDebiMonHuaj);
                double.TryParse(dbDataRowImportKokaFleteKontabel["VLEFTAKREDIMONHUAJ"].ToString(), out vleftaKrediMonHuaj);
                double.TryParse(dbDataRowImportKokaFleteKontabel["VLEFTADEBIMONBAZE"].ToString(), out vleftaDebiMonBaze);
                double.TryParse(dbDataRowImportKokaFleteKontabel["VLEFTAKREDIMONBAZE"].ToString(), out vleftaKrediMonBaze);
                return true;
            }
            catch (InvalidCastException)
            {
                throw new Exception("ERROR: Gabim gjate marrjes se trupit te fleteve kontabel per import nga db-ja");
            }
        }

        /// <summary>
        /// IMPORTI FLETEVE KONTABEL:
        /// Metoda perdoret per te mbushur te gjitha atributet e objektit clsImportTrupiFleteKontabel sipas te dhenave qe vijne nga cdo rresht i databazes nga tabela T_TEMP_IMPORTPOLICA.
        /// </summary>
        /// <param name="dbDataRowImportKokaFleteKontabelAlbsig">(DataRow) Merr si parameter vetem nje rresht te kthyer nga tabelat ne databaze per te mbushur nje objekt.</param>
        /// <param name="vlefteFK">(float) Vlefta shumatore e gjithe trupit.</param>
        /// <param name="idDokumentImporti">(int) Id e dokumentit qe po i krijohet trupi.</param>
        /// <returns>(bool) Kthen true nese nuk ndodh asnje gabim gjate leximit te te dhenave, ne te kundert false.</returns>
        internal bool mbushTrupFleteKontabelImportAlbsig(DataRow dbDataRowImportKokaFleteKontabelAlbsig, out double vlefteFK, int idDokumentImporti)
        {
            vlefteFK = -1;
            if (dbDataRowImportKokaFleteKontabelAlbsig == null)
                return false;
            try
            {
                this.idDokumentImporti = idDokumentImporti;
                nrLlogarise = dbDataRowImportKokaFleteKontabelAlbsig["NRLLOG"].ToString();
                pershkrimTrupFleteKontabel = dbDataRowImportKokaFleteKontabelAlbsig["PERSHKRIMI"].ToString();
                monedha = dbDataRowImportKokaFleteKontabelAlbsig["MONEDHA"].ToString();
                int idVeprimi = 0;
                int.TryParse(dbDataRowImportKokaFleteKontabelAlbsig["VEPRIMI"].ToString(), out idVeprimi);
                if (idVeprimi == 1)
                {
                    double.TryParse(dbDataRowImportKokaFleteKontabelAlbsig["VLEFTA"].ToString(), out vleftaDebiMonBaze);
                    double.TryParse(dbDataRowImportKokaFleteKontabelAlbsig["VLEFTAMONLLOG"].ToString(), out vleftaDebiMonHuaj);
                    if (dbDataRowImportKokaFleteKontabelAlbsig["VLEFTAMONLLOG"].ToString() == "")
                        vleftaDebiMonHuaj = vleftaDebiMonBaze;
                    if (vleftaDebiMonHuaj != 0)
                        kursi = (double)vleftaDebiMonBaze / vleftaDebiMonHuaj;
                    else kursi = (double)1;
                    //Kthen vleften ne leke te rreshtit per tu mbledhur me vone per shume e kokes.
                    vlefteFK = vleftaDebiMonBaze;
                }
                else
                {
                    double.TryParse(dbDataRowImportKokaFleteKontabelAlbsig["VLEFTA"].ToString(), out vleftaKrediMonBaze);
                    double.TryParse(dbDataRowImportKokaFleteKontabelAlbsig["VLEFTAMONLLOG"].ToString(), out vleftaKrediMonHuaj);
                    if (dbDataRowImportKokaFleteKontabelAlbsig["VLEFTAMONLLOG"].ToString() == "")
                        vleftaKrediMonHuaj = vleftaKrediMonBaze;
                    if (vleftaKrediMonHuaj != 0)
                        kursi = (double)vleftaKrediMonBaze / vleftaKrediMonHuaj;
                    else kursi = (double)1;
                    vlefteFK = 0;
                }
                return true;
            }
            catch (InvalidCastException)
            {
                throw new Exception("ERROR: Gabim gjate marrjes se trupit te fleteve kontabel per import nga db-ja");
            }
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// IMPORTI FLETEVE KONTABEL:
        /// Ruan objektin e rreshtit te trupit te dokumentit te fletes kontabel qe do vendosen ne tabelat e importit.
        /// </summary>
        /// <param name="moduliImporte">(clsDatabazeImporte) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen True nese ruajtja perfundon me sukses, ose False nese ruajtja jep gabim.</returns>
        public clsMesazh ruajTrupiPerImport(clsDatabazeImporte moduliImporte)
        {
            clsMesazh pergjigja = ruajTrupiFleteKontabelPerImport(moduliImporte);
            return pergjigja;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// IMPORTI FLETEVE KONTABEL:
        /// Ruan objektin e rreshtit te trupit te dokumentit te fletes kontabel qe do vendoset ne tabelat e importit.
        /// </summary>
        /// <param name="moduliImporte">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen True nese ruajtja perfundon me sukses, ose False nese ruajtja jep gabim.</returns>
        private clsMesazh ruajTrupiFleteKontabelPerImport(clsDatabazeImporte moduliImporte)
        {
            clsMesazh pergjigja = moduliImporte.ruajTrupiFleteKontabelPerImport(idTrupiImport, IdKokaImport, idDokumentImporti, nrLlogarise, pershkrimTrupFleteKontabel, monedha, kursi, vleftaDebiMonHuaj,
                vleftaKrediMonHuaj, vleftaDebiMonBaze, vleftaKrediMonBaze);
            return pergjigja;
        }

        #endregion
    }
}