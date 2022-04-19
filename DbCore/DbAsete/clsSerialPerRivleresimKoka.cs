using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.DbAsete
{
    /// <summary>
    /// MODULI ASETE:
    /// Klase e ndertuar per te mbajtur objektin e dokumentit te serialeve per rivleresim.
    /// Te dhenat merret nga tabela T_ASETE_SERIALxRIVLERESIM_KOKA.
    /// </summary>
    public class clsSerialPerRivleresimKoka
    {

        #region Atribute

        private int idKokaRivlersim;
        private string nrDok;
        private int idNiveli;
        private int idKonfigurimAmbjenti;
        private DateTime dateDokumenti;
        private DateTime dateRegjistrimi;
        private int idNjesiAdministrative;
        private string pershkrimi;
        private int idNderViti;
        private int idLlojStandarti;
        private int idStatusDokumenti;
        private int idNdermarrje;
        private int idPerdoruesi;
        private int idKrijuesi;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private int idDokGjenerues;
        private int idNivelGjenerues;
        private int idKonfigGjenerues;
        private int idDokNga;
        private int idLlogKunderparti;
        private colSerialetPerRivleresim colTrupi;

        #endregion

        #region Properties

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id automatike te kokes se dokumentit te rivleresimit.
        /// </summary>
        public int IdKokaRivlersim
        {
            get { return idKokaRivlersim; }
            set { idKokaRivlersim = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (string) Merr ose jep vlere numrit te dokumentit te rivleresimit.
        /// </summary>
        public string NrDok
        {
            get { return nrDok; }
            set { nrDok = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se nenkategorise qe do te perdoret per rivleresimin.
        /// </summary>
        public int IdNiveli
        {
            get { return idNiveli; }
            set { idNiveli = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se llojit te dokumentit te perdorur ne dokumentin e rivleresimit.
        /// </summary>
        public int IdKonfigurimAmbjenti
        {
            get { return idKonfigurimAmbjenti; }
            set { idKonfigurimAmbjenti = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (DateTime) Merr ose jep vlere date se dokumentit te rivleresimit.
        /// </summary>
        public DateTime DateDokumenti
        {
            get { return dateDokumenti; }
            set { dateDokumenti = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (DateTime) Merr ose jep vlere dates kur po behet regjistrimi i dokumentit te rivleresimit.
        /// </summary>
        public DateTime DateRegjistrimi
        {
            get { return dateRegjistrimi; }
            set { dateRegjistrimi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se njesi administrative ku po behet dokumenti i rivleresimit.
        /// </summary>
        public int IdNjesiAdministrative
        {
            get { return idNjesiAdministrative; }
            set { idNjesiAdministrative = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (string) Merr ose jep vlere pershkrimit qe mund te vendosen ne dokumentin e rivleresimit.
        /// </summary>
        public string Pershkrimi
        {
            get { return pershkrimi; }
            set { pershkrimi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se lidhjes se ndermarrjes me nje vit kalendarik te caktuar.
        /// </summary>
        public int IdNderViti
        {
            get { return idNderViti; }
            set { idNderViti = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se standartit te perdorur ne dokumentin e rivleresimit.
        /// </summary>
        public int IdLlojStandarti
        {
            get { return idLlojStandarti; }
            set { idLlojStandarti = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere se ne cfare gjendje eshte dokumenti i rivleresimit, i ruajtur, fshire, apo modifikuar.
        /// </summary>
        public int IdStatusDokumenti
        {
            get { return idStatusDokumenti; }
            set { idStatusDokumenti = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se ndermarrjes ne te cilen eshte dokumenti i rivleresimit.
        /// </summary>
        public int IdNdermarrje
        {
            get { return idNdermarrje; }
            set { idNdermarrje = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (DateTime) Merr ose jep vlere dates se krijimit te dokumentit te rivleresimit.
        /// </summary>
        public DateTime DtKrijimi
        {
            get { return dtKrijimi; }
            set { dtKrijimi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (DateTime) Merr ose jep vlere dates se modifikimit te dokumentit te rivleresimit.
        /// </summary>
        public DateTime DtModifikimi
        {
            get { return dtModifikimi; }
            set { dtModifikimi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se perdoruesit qe modifikon dokumentin e rivleresimit.
        /// </summary>
        public int IdPerdoruesi
        {
            get { return idPerdoruesi; }
            set { idPerdoruesi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se krijuesit qe e krijon per here te pare dokumentin e rivleresimit.
        /// </summary>
        public int IdKrijuesi
        {
            get { return idKrijuesi; }
            set { idKrijuesi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se dokumentit nga po gjenerohet dokumenti i rivleresimit.
        /// </summary>
        public int IdDokGjenerues
        {
            get { return idDokGjenerues; }
            set { idDokGjenerues = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se nenkategorise te dokumentit qe e ka gjeneruar dokumentin e rivleresimit.
        /// </summary>
        public int IdNivelGjenerues
        {
            get { return idNivelGjenerues; }
            set { idNivelGjenerues = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se llojit te dokumentit qe ka gjeneruar dokumentin e rivleresimit.
        /// </summary>
        public int IdKonfigGjenerues
        {
            get { return idKonfigGjenerues; }
            set { idKonfigGjenerues = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se dokumentit nga vjen dokumenti i rivleresimit.
        /// </summary>
        public int IdDokNga
        {
            get { return idDokNga; }
            set { idDokNga = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se llogarise kunderparti.
        /// </summary>
        public int IdLlogKunderparti
        {
            get { return idLlogKunderparti; }
            set { idLlogKunderparti = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (colSerialetPerRivleresim) Merr ose jep vlere trupit te dokumentit te rivleresimit.
        /// </summary>
        public colSerialetPerRivleresim ColTrupi
        {
            get { return colTrupi; }
            set { colTrupi = value; }
        }

        #endregion

        #region Konstruktori

        /// <summary>
        /// MODULI ASETE:
        /// Krijon objektin bosh te klases clsSerialPerRivleresimKoka per koken e dokumentit te rivleresimit.
        /// </summary>
        public clsSerialPerRivleresimKoka()
        {
            colTrupi = new colSerialetPerRivleresim();
        }

        public clsSerialPerRivleresimKoka(string nrDok, int idNiveli, int idKonfigurimAmbjenti, DateTime dateDokumenti, DateTime dateRegjistrimi, int idNjesiAdministrative, string pershkrimi, int idNderViti, int idLlojStandarti, int idStatusDokumenti, int idndermarrje, int idPerdoruesi, int idkrijuesi, DateTime dtKrijimi, int idDokGjenerues, int idNivelGjenerues, int idKonfigGjenerues, int idDokNga, int idLlogKunderparti, colAmortizimiTrupiAbstract colAmortTrupi, clsDatabazeAsete dbasete)
        {
            this.nrDok = nrDok;
            this.idNiveli = idNiveli;
            this.idKonfigurimAmbjenti = idKonfigurimAmbjenti;
            this.dateDokumenti = dateDokumenti;
            this.dateRegjistrimi = dateRegjistrimi;
            this.idNjesiAdministrative = idNjesiAdministrative;
            this.pershkrimi = pershkrimi;
            this.idNderViti = idNderViti;
            this.idLlojStandarti = idLlojStandarti;
            this.idStatusDokumenti = idStatusDokumenti;
            this.idNdermarrje = idndermarrje;
            this.idPerdoruesi = idPerdoruesi;
            this.idKrijuesi = idkrijuesi;
            this.dtKrijimi = dtKrijimi;
            this.idDokGjenerues = idDokGjenerues;
            this.idNivelGjenerues = idNivelGjenerues;
            this.idKonfigGjenerues = idKonfigGjenerues;
            this.idDokNga = idDokNga;
            this.idLlogKunderparti = idLlogKunderparti;
            this.colTrupi = new colSerialetPerRivleresim(colAmortTrupi, dateDokumenti, idNdermarrje, idPerdoruesi, idLlojStandarti, idStatusDokumenti, dbasete);
        }

        public clsSerialPerRivleresimKoka(DataRow rreshti)
        {
            
            mbushRivlersimKoka(rreshti);
            colTrupi = new colSerialetPerRivleresim();
        }

        public clsSerialPerRivleresimKoka(int idGjenerues)
        {
            clsDatabazeAsete dbasete = new clsDatabazeAsete();
            mbushRivlersimKoka(dbasete.ktheKokaRivleresimSipasIDGjenerues(idGjenerues));
        }

        #endregion

        #region Metoda Internal


        /// <summary>
        /// MODULI ASETE:
        /// Metoda perdoret per te mbushur te gjitha atributet e objektit clsSerialPerRivleresimKoka sipas te dhenave qe vijne nga cdo rresht i databazes nga tabela T_ASETE_SERIALxRIVLERESIM_KOKA.
        /// </summary>
        /// <param name="dbDataRowRivleresimKoka">(DataRow) Merr si parameter vetem nje rresht te kthyer nga tabelat ne databaze per te mbushur nje objekt.</param>
        /// <returns>(bool) Kthen true nese nuk ndodh asnje gabim gjate leximit te te dhenave, ne te kundert false.</returns>
        internal bool mbushRivlersimKoka(DataRow dbDataRowRivleresimKoka)
        {
            if (dbDataRowRivleresimKoka == null)
                return false;
            try
            {
                int.TryParse(dbDataRowRivleresimKoka["IDKOKARIVLERSIM"].ToString(), out idKokaRivlersim);
                nrDok = dbDataRowRivleresimKoka["NR_DOK"].ToString();
                int.TryParse(dbDataRowRivleresimKoka["IDNIVELI"].ToString(), out idNiveli);
                int.TryParse(dbDataRowRivleresimKoka["IDKONFIGURIMAMBJENTI"].ToString(), out idKonfigurimAmbjenti);
                DateTime.TryParse(dbDataRowRivleresimKoka["DATE_DOKUMENTI"].ToString(), out dateDokumenti);
                DateTime.TryParse(dbDataRowRivleresimKoka["DATE_REGJISTRIMI"].ToString(), out dateRegjistrimi);
                pershkrimi = dbDataRowRivleresimKoka["PERSHKRIMI"].ToString();
                int.TryParse(dbDataRowRivleresimKoka["IDNJESIADMINISTRATIVE"].ToString(), out idNjesiAdministrative);
                int.TryParse(dbDataRowRivleresimKoka["IDLLOJSTANDARTI"].ToString(), out idLlojStandarti);
                int.TryParse(dbDataRowRivleresimKoka["IDNDERVITI"].ToString(), out idNderViti);
                int.TryParse(dbDataRowRivleresimKoka["IDSTATUSDOK"].ToString(), out idStatusDokumenti);
                int.TryParse(dbDataRowRivleresimKoka["IDNDERMARJE"].ToString(), out idNdermarrje);
                int.TryParse(dbDataRowRivleresimKoka["IDPERDORUESI"].ToString(), out idPerdoruesi);
                int.TryParse(dbDataRowRivleresimKoka["IDKRIJUESI"].ToString(), out idKrijuesi);
                DateTime.TryParse(dbDataRowRivleresimKoka["DTKRIJIMI"].ToString(), out dtKrijimi);
                DateTime.TryParse(dbDataRowRivleresimKoka["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                int.TryParse(dbDataRowRivleresimKoka["IDNIVELGJENERUES"].ToString(), out idNivelGjenerues);
                int.TryParse(dbDataRowRivleresimKoka["IDKONFIGGJENERUES"].ToString(), out idKonfigGjenerues);
                int.TryParse(dbDataRowRivleresimKoka["IDGJENERUES"].ToString(), out idDokGjenerues);
                int.TryParse(dbDataRowRivleresimKoka["IDDOKNGA"].ToString(), out idDokNga);
                int.TryParse(dbDataRowRivleresimKoka["ID_LLOGKUNDERPARTI"].ToString(), out idLlogKunderparti);
                return true;
            }
            catch (InvalidCastException)
            {
                throw new Exception("ERROR: Gabim gjate marrjes se kokes te dokumentit te rivleresimit nga db-ja");
            }
        }

        #endregion

        #region Metoda Publike

        public clsMesazh ruaj(int idKokaAmortizim)
        {
            clsMesazh pergjigja = ruajKokaRivleresim(idKokaAmortizim);
            return pergjigja;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// MODULI ASETE:
        /// Ruan objektin e kokes se dokumentit te rivleresuar.
        /// </summary>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen True nese ruajtja perfundon me sukses, ose False nese ruajtja jep gabim.</returns>
        private clsMesazh ruajKokaRivleresim(int idKokaAmortizim)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            clsMesazh pergjigja = new clsMesazh(true, "Ruajtja e rivleresimit u krye me sukses!");
            pergjigja = moduliAsete.ruajKokenERivleresimit(out idKokaRivlersim, nrDok, idNiveli, idKonfigurimAmbjenti, dateDokumenti, dateRegjistrimi, idNjesiAdministrative, pershkrimi, idNderViti, idLlojStandarti, idStatusDokumenti, idNdermarrje, idPerdoruesi, idKrijuesi, dtKrijimi, idKokaAmortizim, idNivelGjenerues, idKonfigGjenerues, idDokNga, idLlogKunderparti);
            if (pergjigja.Status)
                pergjigja = colTrupi.ruajSerialetXRivleresim(idKokaRivlersim, idStatusDokumenti);
            return pergjigja;
        }

        #endregion

    }
}
