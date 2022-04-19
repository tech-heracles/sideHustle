using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DbCore.DbAsete
{
    /// <summary>
    /// MODULI ASETE:
    /// Klase e ndertuar per te mbajtur objektin e normave te amortizimit sipas grupeve dhe llojeve te amortizimit.
    /// Te dhenat merret nga tabela T_ASETE_LIDHJE_GRUP_LLOJAMORT
    /// </summary>
    public class clsGrupNormaAmortizimi
    {
        #region Atribute

        private int idLidhjeGrupLlojAmort;
        private int idKonfigurimArtikujsh;
        private int idLlojAmortizimi;
        private int idStandartAmortizimi;
        private bool normeMagazine;
        private double norme;
        private int idStatusDokumenti;
        private int idNdermarrje;
        private int idPerdoruesi;
        private int idKrijuesi;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private DataRow rreshti;

        #endregion

        #region Properties

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id automatike e lidhjes se grupit me llojin e amortizimit per normat e amortizimit.
        /// </summary>
        public int IdLidhjeGrupLlojAmort
        {
            get { return idLidhjeGrupLlojAmort; }
            set { idLidhjeGrupLlojAmort = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se grupit per te cilen eshte i vlefshem konfigurimi i normes.
        /// </summary>
        public int IdKonfigurimArtikujsh
        {
            get { return idKonfigurimArtikujsh; }
            set { idKonfigurimArtikujsh = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se llojit te amortizimit.
        /// </summary>
        public int IdLlojAmortizimi
        {
            get { return idLlojAmortizimi; }
            set { idLlojAmortizimi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id e standartit te amortizimit.
        /// </summary>
        public int IdStandartAmortizimi
        {
            get { return idStandartAmortizimi; }
            set { idStandartAmortizimi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (bool) Merr ose jep vlere nese norma do te merret per magazine apo thjesht per artikull.
        /// </summary>
        public bool NormeMagazine
        {
            get { return normeMagazine; }
            set { normeMagazine = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (float) Merr ose jep vlere normes qe do te llogaritet amortizimi ne grup.
        /// </summary>
        public double Norme
        {
            get { return norme; }
            set { norme = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere se ne cfare gjendje eshte norma e vendosur per grup, e ruajtur, fshire, apo modifikuar.
        /// </summary>
        public int IdStatusDokumenti
        {
            get { return idStatusDokumenti; }
            set { idStatusDokumenti = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se ndermarrjes ku do te zbatohet norma e amortizimit te grupeve.
        /// </summary>
        public int IdNdermarrje
        {
            get { return idNdermarrje; }
            set { idNdermarrje = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (DateTime) Merr ose jep vlere dates se krijimit te normes se amortizimit te grupeve.
        /// </summary>
        public DateTime DtKrijimi
        {
            get { return dtKrijimi; }
            set { dtKrijimi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (DateTime) Merr ose jep vlere dates se modifikimit te normes se amortizimit te grupeve.
        /// </summary>
        public DateTime DtModifikimi
        {
            get { return dtModifikimi; }
            set { dtModifikimi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se perdoruesit qe modifikon normen e amortizimit te grupeve.
        /// </summary>
        public int IdPerdoruesi
        {
            get { return idPerdoruesi; }
            set { idPerdoruesi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se krijuesit qe e krijon per here te pare normen e amortizimit te grupeve.
        /// </summary>
        public int IdKrijuesi
        {
            get { return idKrijuesi; }
            set { idKrijuesi = value; }
        }

        #endregion

        #region Konstruktori

        /// <summary>
        /// MODULI ASETE:
        /// Krijon objektin bosh te klases clsGrupNormaAmortizimi per normat e amortizimit te grupeve.
        /// </summary>
        public clsGrupNormaAmortizimi()
        {
        }

        /// <summary>
        /// MODULI ASETE:
        /// Krijon objektin me parametra te klases clsGrupNormaAmortizimi per normat e amortizimit te grupeve.
        /// </summary>
        /// <param name="idndermarje">(int) Id e ndermarrjes.</param>
        /// <param name="idperdoruesi">(int) Id e perdoruesit.</param>
        /// <param name="rresht">(Dictionary) Rreshti me te cilen do te mbushet objekti i klases</param>
        public clsGrupNormaAmortizimi(int idndermarje, int idperdoruesi, Dictionary<string, object> rresht)
        {
            string standart = rresht["Standart"].ToString();
            idLlojAmortizimi = int.Parse(rresht["IdLlojAmortizimi"].ToString());
            string normemag = rresht["NormeMagazine"].ToString();

            norme = double.Parse(rresht["Norme"].ToString());
            idStandartAmortizimi = clsStandarteAmortizim.merrIDStandartinAmortizimitTeNdermarrjesSipasEmertimit(standart, idndermarje);
            if (normemag == "Artikull")
                normeMagazine = false;
            else normeMagazine = true;
            idStatusDokumenti = 1;
            idNdermarrje = idndermarje;
            IdPerdoruesi = idperdoruesi;
            IdKrijuesi = idperdoruesi;
            if (IdLlojAmortizimi != 1 && !normeMagazine && norme == 0)
                throw new Exception("Zgjidhni normen per standartin " + standart);
        }

        public clsGrupNormaAmortizimi(DataRow rreshti)
        {
            
            mbushNormaAmortizimiGrupObjekt(rreshti);
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// MODULI ASETE:
        /// Metoda perdoret per te mbushur te gjitha atributet e objektit clsGrupNormaAmortizimi sipas te dhenave qe vijne nga cdo rresht i databazes nga tabela T_ASETE_LIDHJE_GRUP_LLOJAMORT.
        /// </summary>
        /// <param name="dbDataRowNormaAmortizimi">(DataRow) Merr si parameter vetem nje rresht te kthyer nga tabelat ne databaze per te mbushur nje objekt.</param>
        /// <returns>(bool) Kthen true nese nuk ndodh asnje gabim gjate leximit te te dhenave, ne te kundert false.</returns>
        internal bool mbushNormaAmortizimiGrupObjekt(DataRow dbDataRowNormaAmortizimi)
        {
            if (dbDataRowNormaAmortizimi == null)
                return false;
            try
            {
                int.TryParse(dbDataRowNormaAmortizimi["ID_GRUP_LLOJAMORT"].ToString(), out idLidhjeGrupLlojAmort);
                int.TryParse(dbDataRowNormaAmortizimi["IDKONFIGURIMARTIKULLI"].ToString(), out idKonfigurimArtikujsh);
                int.TryParse(dbDataRowNormaAmortizimi["IDLLOJAMORTIZIMI"].ToString(), out idLlojAmortizimi);
                int.TryParse(dbDataRowNormaAmortizimi["IDSTANDARTAMORT"].ToString(), out idStandartAmortizimi);
                bool.TryParse(dbDataRowNormaAmortizimi["NORMEMAGAZINE"].ToString(), out normeMagazine);
                double.TryParse(dbDataRowNormaAmortizimi["NORMA"].ToString(), out norme);
                int.TryParse(dbDataRowNormaAmortizimi["IDNDERMARJE"].ToString(), out idNdermarrje);
                int.TryParse(dbDataRowNormaAmortizimi["IDSTATUSDOK"].ToString(), out idStatusDokumenti);
                DateTime.TryParse(dbDataRowNormaAmortizimi["DTKRIJIMI"].ToString(), out dtKrijimi);
                DateTime.TryParse(dbDataRowNormaAmortizimi["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                int.TryParse(dbDataRowNormaAmortizimi["IDPERDORUESI"].ToString(), out idPerdoruesi);
                int.TryParse(dbDataRowNormaAmortizimi["IDKRIJUESI"].ToString(), out idKrijuesi);
                return true;
            }
            catch (InvalidCastException)
            {
                throw new Exception("ERROR: Gabim gjate marrjes se normave te amortizimit te grupeve nga db-ja");
            }
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// MODULI ASETE:
        /// Ruan objektin e normes per grupin dhe standartin nese norma ne ndermarrjen ne perdorim nuk eshte ruajtur njehere.
        /// </summary>
        /// <returns>Kthen clsMesazh me atribut statusi True nese ruajtja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        public clsMesazh ruaj()
        {
            if (!kontrolloGrupNormaAmortizimit(idKonfigurimArtikujsh, idStandartAmortizimi))
            {
                clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
                clsMesazh pergjigja = moduliAsete.ruajGrupNormaAmortizimi(out idLidhjeGrupLlojAmort, idKonfigurimArtikujsh, idLlojAmortizimi, idStandartAmortizimi, normeMagazine, norme, idStatusDokumenti, idNdermarrje, idPerdoruesi, idKrijuesi);
                moduliAsete.Dispose();
                return pergjigja;
            }
            return new clsMesazh(false, "Ekziston nje norme per kete grup dhe standart!");
        }

        /// <summary>
        /// MODULI ASETE:
        /// Modifikon objektin e normes per grupin dhe standartin nese norma ne ndermarrjen ne perdorim eshte ruajtur njehere.
        /// </summary>
        /// <returns>Kthen clsMesazh me atribut statusi True nese modifikimi kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        public clsMesazh modifiko()
        {
            if (kontrolloGrupNormaAmortizimit(idKonfigurimArtikujsh, idStandartAmortizimi))
            {
                clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
                clsMesazh pergjigja = moduliAsete.modifikimiGrupNormaAmortizimi(idLidhjeGrupLlojAmort, idLlojAmortizimi, normeMagazine, norme, idPerdoruesi);
                moduliAsete.Dispose();
                return pergjigja;
            }
            return new clsMesazh(false, "Nuk ekziston nje norme per kete grup dhe standart!");
        }

        /// <summary>
        /// MODULI ASETE:
        /// Fshin objektin e normes per grupin dhe standartin nese norma ne ndermarrjen ne perdorim eshte ruajtur njehere dhe nuk ka lidhje me objekte te tjera qe varen nga ekzistenca e objektit qe po fshihet.
        /// </summary>
        /// <returns>Kthen clsMesazh me atribut statusi True nese fshirja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        public clsMesazh fshi()
        {
            if (kontrolloGrupNormaAmortizimit(idKonfigurimArtikujsh, idStandartAmortizimi))
            {
                clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
                clsMesazh pergjigja = moduliAsete.fshiGrupNormaAmortizimi(idLidhjeGrupLlojAmort);
                moduliAsete.Dispose();
                return pergjigja;
            }
            return new clsMesazh(false, "Nuk ekziston nje norme per kete grup dhe standart!");
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr objektin e normes se grupit te amortizimit sipas kerkimit ne ndermarrjen ne perdorim.
        /// </summary>
        /// <param name="idKonfigArtikulli">(int) Id e grupit qe ka amortizimin.</param>
        /// <param name="idStandarti">(int) Id e standartit qe ka amortizimin.</param>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate marrjes se normes se grupit te amortizimit ose False ne te kundert.</returns>
        public bool merrGrupNormaAmortizimiSipasIDKonfig(int idKonfigArtikulli, int idStandarti)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            bool pergjigje = mbushNormaAmortizimiGrupObjekt(moduliAsete.ktheGrupNormaAmortizimiSipasIDKonfig(idKonfigArtikulli, idStandarti));
            moduliAsete.Dispose();
            return pergjigje;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr id e normes se grupit te amortizimit sipas kerkimit ne ndermarrjen ne perdorim.
        /// </summary>
        /// <param name="idKonfigArtikulli">(int) Id e grupit qe ka amortizimin.</param>
        /// <param name="idStandarti">(int) Id e standartit qe ka amortizimin.</param>
        /// <returns>Kthen id numrit automatik te normes se grupit te amortizimit nese gjendet, ne te kundert kthen -1.</returns>
        public static int merrIDGrupNormaAmortizimiSipasIDKonfigStandart(int idKonfigArtikulli, int idStandarti)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            int pergjigje = moduliAsete.ktheIDGrupNormaAmortizimiSipasIDKonfigStandart(idKonfigArtikulli, idStandarti);
            moduliAsete.Dispose();
            return pergjigje;
        }

        /// <summary>
        /// Kontrollon nese norma per grupin dhe standartin e kerkuar ekziston njehere i ruajtur ne bazen e te dhenave.
        /// </summary>
        /// <param name="idKonfigArtikulli">(int) Id e grupit qe ka amortizimin.</param>
        /// <param name="idStandarti">(int) Id e standartit qe ka amortizimin.</param>
        /// <returns>Kthen True nese ekziston njehere norma per grupin dhe standartin e kerkuar dhe False ne te kundert.</returns>
        public static bool kontrolloGrupNormaAmortizimit(int idKonfigArtikulli, int idStandarti)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            bool pergjigje = moduliAsete.ekzistonGrupNormaAmortizimit(idKonfigArtikulli, idStandarti);
            moduliAsete.Dispose();
            return pergjigje;
        }

        #endregion
    }
}
