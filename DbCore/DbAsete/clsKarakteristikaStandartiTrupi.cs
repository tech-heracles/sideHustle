using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DbCore.DbAsete
{
    /// <summary>
    /// MODULI ASETE:
    /// Klase e ndertuar per te mbajtur objektin e karakteristikave te trupit te standarteve ne modulin e aseteve.
    /// Te dhenat merret nga tabela T_ASETE_LIDHJE_STANDART_STATUSMAG_TRUPI
    /// </summary>
    public class clsKarakteristikaStandartiTrupi
    {
        #region Atribute

        private int idTrupiKarakteristikStandart;
        private int idKokaKarakteristikStandart;
        private int idStatusMagazine;
        private bool llogaritAmortizim;
        private int filloAmortiziminPas;
        private DataRow rreshti;

        #endregion

        #region Properties

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id automatike te trupit te karakteristikave te standartit
        /// </summary>
        public int IdTrupiKarakteristikStandart
        {
            get { return idTrupiKarakteristikStandart; }
            set { idTrupiKarakteristikStandart = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se kokes se standartit per te cilat do te jane karakteristikat
        /// </summary>
        public int IdKokaKarakteristikStandart
        {
            get { return idKokaKarakteristikStandart; }
            set { idKokaKarakteristikStandart = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se per cfare statusi te magazines jane karakteristikat e standartit
        /// </summary>
        public int IdStatusMagazine
        {
            get { return idStatusMagazine; }
            set { idStatusMagazine = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (bool) Merr ose jep vlere nese duhet te llogaritet amortizimi mujor apo jo.
        /// </summary>
        public bool LlogaritAmortizim
        {
            get { return llogaritAmortizim; }
            set { llogaritAmortizim = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere se pas sa kohesh do te filloj te llogaritet amortizimi mujor
        /// </summary>
        public int FilloAmortiziminPas
        {
            get { return filloAmortiziminPas; }
            set { filloAmortiziminPas = value; }
        }

        #endregion

        #region Konstruktori

        /// <summary>
        /// MODULI ASETE:
        /// Krijon objektin bosh te klases clsKarakteristikaStandartiTrupi per karakteristikat e standarteve ne lidhje me llojet e magazinave te ndryshme.
        /// </summary>
        public clsKarakteristikaStandartiTrupi()
        {
            
        }

        /// <summary>
        /// MODULI ASETE:
        /// Krijon objektin e plote te klases clsKarakteristikaStandartiTrupi per karakteristikat e standarteve ne lidhje me llojet e magazinave te ndryshme.
        /// </summary>
        /// <param name="idTrupiKarakteristikStandart">(int) Id automatike e trupit te karakteristikave te standartit.</param>
        /// <param name="idKokaKarakteristikStandart">(int) Id e kokes se karakteristikave te standartit.</param>
        /// <param name="idStatusMagazine">(int) Id e statusit te magazines per te cilen eshte kryer konfigurimi.</param>
        /// <param name="llogaritAmortizim">(bool) True nese duhet qe te llogaritet amortizimin kete kete konfigurim te bere ose False ne te kundert.</param>
        /// <param name="filloAmortiziminPas">(int) Muajt pas sa kohes nga blerja ose hyrja ne magazine do te filloje amortizimi.</param>
        public clsKarakteristikaStandartiTrupi(int idTrupiKarakteristikStandart, int idKokaKarakteristikStandart, int idStatusMagazine, bool llogaritAmortizim, int filloAmortiziminPas)
        {
            this.idTrupiKarakteristikStandart = idTrupiKarakteristikStandart;
            this.idKokaKarakteristikStandart = idKokaKarakteristikStandart;
            this.idStatusMagazine = idStatusMagazine;
            this.llogaritAmortizim = llogaritAmortizim;
            this.filloAmortiziminPas = filloAmortiziminPas;
        }

        public clsKarakteristikaStandartiTrupi(DataRow rreshti)
        {
            
            mbushKarakteristikaTrupiObjekt(rreshti);
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// MODULI ASETE:
        /// Metoda perdoret per te mbushur te gjitha atributet e objektit clsKarakteristikaStandartiTrupi sipas te dhenave qe vijne nga cdo rresht i databazes nga tabela T_ASETE_LIDHJE_STANDART_STATUSMAG_TRUPI.
        /// </summary>
        /// <param name="dbDataRowKarakteristikaTrupi">(DataRow) Merr si parameter vetem nje rresht te kthyer nga tabelat ne databaze per te mbushur nje objekt.</param>
        /// <returns>(bool) Kthen true nese nuk ndodh asnje gabim gjate leximit te te dhenave, ne te kundert false.</returns>
        internal bool mbushKarakteristikaTrupiObjekt(DataRow dbDataRowKarakteristikaTrupi)
        {
            if (dbDataRowKarakteristikaTrupi == null)
                return false;
            try
            {
                int.TryParse(dbDataRowKarakteristikaTrupi["ID_TRUPI_STAND_STATUS"].ToString(), out idTrupiKarakteristikStandart);
                int.TryParse(dbDataRowKarakteristikaTrupi["ID_KOKA_STAND_STATUS"].ToString(), out idKokaKarakteristikStandart);
                int.TryParse(dbDataRowKarakteristikaTrupi["IDSTATUSMAGAZINE"].ToString(), out idStatusMagazine);
                bool.TryParse(dbDataRowKarakteristikaTrupi["LLOGARIT_AMORTIZIM"].ToString(), out llogaritAmortizim);
                int.TryParse(dbDataRowKarakteristikaTrupi["FILLO_AMORTIZIM_PAS"].ToString(), out filloAmortiziminPas);
                return true;
            }
            catch (InvalidCastException)
            {
                throw new Exception("ERROR: Gabim gjate marrjes se karakteristikave te trupit te standartit nga db-ja");
            }
        }
        internal bool mbushKarakteristikaTrupiObjekt(clsKarakteristikaStandartiTrupi karakteristikaStandartiTrupi)
        {
            idTrupiKarakteristikStandart = karakteristikaStandartiTrupi.IdTrupiKarakteristikStandart;
            idKokaKarakteristikStandart = karakteristikaStandartiTrupi.IdKokaKarakteristikStandart;
            idStatusMagazine = karakteristikaStandartiTrupi.IdStatusMagazine;
            llogaritAmortizim = karakteristikaStandartiTrupi.LlogaritAmortizim;
            filloAmortiziminPas = karakteristikaStandartiTrupi.FilloAmortiziminPas;
            return true;
        }
        #endregion

        #region Metoda Publike

        /// <summary>
        /// MODULI ASETE:
        /// Ruan konfigurimin trupit te standartit sipas karakteristikave nese konfigurimi me ato karakteristika nuk eshte i krijuar njehere.
        /// </summary>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen True nese ruajtja perfundon me sukses, ose False nese ruajtja jep gabim ose ekziston njehere konfigurimi me ato karakteristika per standartin.</returns>
        public clsMesazh ruaj(clsDatabazeAsete moduliAsete)
        {
            clsMesazh pergjigja = moduliAsete.ruajKonfigurimTrupiStandarti(out idTrupiKarakteristikStandart, idKokaKarakteristikStandart, idStatusMagazine, llogaritAmortizim, filloAmortiziminPas);
            return pergjigja;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Modifikon objektin e trupit te konfigurimit te standartit nese konfigurimi me ato karakteristika eshte i krijuar njehere.
        /// </summary>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen True nese modifikimi perfundon me sukses, ose False nese modifikimi jep gabim ose nuk ekziston njehere konfigurimi me ato karakteristika per standartin.</returns>
        public clsMesazh modifiko(clsDatabazeAsete moduliAsete)
        {
            clsMesazh pergjigja = moduliAsete.modifikimiKonfigurimTrupiStandarti(idKokaKarakteristikStandart, idStatusMagazine, llogaritAmortizim, filloAmortiziminPas);
            return pergjigja;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Fshin objektin e trupit te konfigurimit te standartit nese karakteristika eshte i krijuar njehere dhe nuk ka lidhje me objekte te tjera qe varen nga ekzistenca e objektit qe po fshihet.
        /// </summary>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen True nese fshirja perfundon me sukses, ose False nese fshirja jep gabim ose nuk ekziston njehere konfigurimi me ato karakteristika per standartin.</returns>
        public clsMesazh fshi(clsDatabazeAsete moduliAsete)
        {
            clsMesazh pergjigja = moduliAsete.fshiKonfigurimTrupiStandarti(idKokaKarakteristikStandart);
            return pergjigja;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr objektin e trupit te konfigurimit te standartit sipas statusit qe kerkojme.
        /// </summary>
        /// <param name="idKokaKarakteristikStandart">(int) Id e kokes se karakteristikave te standartit.</param>
        /// <param name="idStatusMagazine">(int) Id e statusit te magazines.</param>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate marrjes se trupit te konfigurimit te standartit ose False ne te kundert.</returns>
        public bool merrKonfigurimStandartiTrupiSipasIDKokaIDStatus(int idKokaKarakteristikStandart, int idStatusMagazine)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            bool pergjigje = mbushKarakteristikaTrupiObjekt(moduliAsete.ktheKonfigurimStandartiTrupiSipasIDKokaIDStatus(idKokaKarakteristikStandart, idStatusMagazine));
            return pergjigje;
        }
        public bool merrKonfigurimStandartiTrupiSipasIDKokaIDStatus(int idKokaKarakteristikStandart, int idStatusMagazine, clsDatabazeAsete dbAsete)
        {
            return mbushKarakteristikaTrupiObjekt(dbAsete.TransCache.getKarakteristikaStandartiTrupi(idKokaKarakteristikStandart, idStatusMagazine, dbAsete));
        }

        #endregion
    }
}
