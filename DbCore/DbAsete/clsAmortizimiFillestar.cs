using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DbCore.DbAsete
{
    /// <summary>
    /// MODULI ASETE:
    /// Klase e ndertuar per te mbajtur objektin e serialeve te aqt-ve. Ruajtja e gjithe serialeve ne momentin qe gjenerohen.
    /// Te dhenat merret nga tabela T_ASETE_AMORT_FILLESTAR.
    /// </summary>
    public class clsAmortizimiFillestar
    {
        #region Atribute

        private int id;
        private int idSerial;
        private int idStandarti;
        private double amortizimiFillestar;
        private int idDokNga;
        private int idStatusDok;
        private DataRow rreshti;

        #endregion

        #region Properties

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id automatike te serialit te aqt-se.
        /// </summary>
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (string) Merr ose jep vlere kodit te serialit te aqt-se.
        /// </summary>
        public int IdSerial
        {
            get { return idSerial; }
            set { idSerial = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (string) Merr ose jep vlere pershkrimit te serialit te aqt-se.
        /// </summary>
        public int IdStandarti
        {
            get { return idStandarti; }
            set { idStandarti = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (double) Merr ose jep vlere e amortizimit fillestar.
        /// </summary>
        public double AmortizimiFillestar
        {
            get { return amortizimiFillestar; }
            set { amortizimiFillestar = value; }

        }

        /// <summary>
        /// MODULI ASETE:
        /// (string) Merr ose jep vlere id-se se dokumentit te amortizimit, ku eshte bere veprimi.
        /// </summary>
        public int IdDokNga
        {
            get { return idDokNga; }
            set { idDokNga = value; }
        }

        public int IdStatusDok
        {
            get { return idStatusDok; }
            set { idStatusDok = value; }
        }

        #endregion

        #region Konstruktori

        /// <summary>
        /// MODULI ASETE:
        /// Krijon objektin bosh te klases clsAmortizimiFillestar per serialet e aqt-ve .
        /// </summary>
        public clsAmortizimiFillestar()
        {
        }

        /// <summary>
        /// MODULI ASETE:
        /// Krijon objektin plote te klases clsAmortizimiFillestar per serialet e aqt-ve .
        /// </summary>
        /// <param name="rreshtDokuKlient">(Dictionary) Objekti i cili do te mbushe klasen.</param>
        public clsAmortizimiFillestar(Dictionary<string, object> rreshtDokuKlient)
        {
            id = Convert.ToInt32(rreshtDokuKlient["IdAQTSerial"]);
            amortizimiFillestar = double.Parse(rreshtDokuKlient["AmortizimiFillestar"].ToString());
        }

        public clsAmortizimiFillestar(DataRow rreshti)
        {
            
            mbushAmortizimFillestar(rreshti);
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// MODULI ASETE:
        /// Metoda perdoret per te mbushur te gjitha atributet e objektit clsAmortizimiFillestar sipas te dhenave qe vijne nga cdo rresht i databazes nga tabela T_ASETE_AQTSERIALE.
        /// </summary>
        /// <param name="dbDataRowAQTSerial">(DataRow) Merr si parameter vetem nje rresht te kthyer nga tabelat ne databaze per te mbushur nje objekt.</param>
        /// <returns>(bool) Kthen true nese nuk ndodh asnje gabim gjate leximit te te dhenave, ne te kundert false.</returns>
        internal bool mbushAmortizimFillestar(DataRow dbDataRowAQTSerial)
        {
            if (dbDataRowAQTSerial == null)
                return false;
            try
            {
                int.TryParse(dbDataRowAQTSerial["ID"].ToString(), out id);
                int.TryParse(dbDataRowAQTSerial["IDSERIAL"].ToString(), out idSerial);
                int.TryParse(dbDataRowAQTSerial["IDSTANDARTI"].ToString(), out idStandarti);
                int.TryParse(dbDataRowAQTSerial["IDDOKNGA"].ToString(), out idDokNga);
                double.TryParse(dbDataRowAQTSerial["AMORTIZIMIFILLESTAR"].ToString(), out amortizimiFillestar);
                int.TryParse(dbDataRowAQTSerial["IDSTATUSDOK"].ToString(), out idStatusDok);
                return true;
            }
            catch (InvalidCastException)
            {
                throw new Exception("ERROR: Gabim gjate marrjes se amortizimit fillestar te aqt-se nga db-ja");
            }
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// MODULI ASETE:
        /// Krijon objektin e plote per klasen clsAmortizimiFillestar
        /// </summary>
        /// <param name="idserial">(string) Kodi i serialit aqt.</param>
        /// <param name="idstandart">(string) Pershkrimi i kodit te serialit aqt.</param>
        public void krijoObjekt(int idserial, int idstandart, int idDokNga, double amortizimifillestar)
        {
            this.idSerial = idserial;
            this.idStandarti = idstandart;
            this.idDokNga = idDokNga;
            amortizimiFillestar = amortizimifillestar;

        }

        /// <summary>
        /// MODULI ASETE:
        /// Ruan objektin e serialit te aqt-se nese seriali nuk ekziston.
        /// </summary>
        /// <returns>Kthen clsMesazh me atribut statusi True nese ruajtja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        public clsMesazh ruaj()
        {
            clsDatabazeAsete dbasete = new clsDatabazeAsete();
            dbasete.beginTransaksion();
            clsMesazh mesazh = ruaj(dbasete);
            if (mesazh.Status)
                dbasete.commitTransaksion();
            else dbasete.rollbackTransaksion();
            return mesazh;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Modifikon objektin e serialit te aqt-se nese e gjen te regjistruar.
        /// </summary>
        /// <returns>Kthen clsMesazh me atribut statusi True nese modifikimi kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        public clsMesazh modifiko()
        {
            clsDatabazeAsete dbasete = new clsDatabazeAsete();
            dbasete.beginTransaksion();
            clsMesazh mesazh = modifiko(dbasete);
            if (mesazh.Status)
                dbasete.commitTransaksion();
            else dbasete.rollbackTransaksion();
            return mesazh;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Fshin objektin e serialit te aqt-se duke i modifikuar statusin nese eshte i krijuar njehere.
        /// </summary>
        /// <returns>Kthen clsMesazh me atribut statusi True nese fshirja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        public clsMesazh fshi()
        {
            clsDatabazeAsete dbasete = new clsDatabazeAsete();
            dbasete.beginTransaksion();
            clsMesazh mesazh = fshi(dbasete);
            if (mesazh.Status)
                dbasete.commitTransaksion();
            else dbasete.rollbackTransaksion();
            return mesazh;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Ruan objektin e serialit te aqt-se nese seriali nuk ekziston.
        /// </summary>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese ruajtja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        public clsMesazh ruaj(clsDatabazeAsete moduliAsete)
        {
            clsMesazh pergjigja = ruajAmortizimFillestar(moduliAsete);
            return pergjigja;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Modifikon objektin e serialit te aqt-se nese e gjen te regjistruar.
        /// </summary>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese modifikimi kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        public clsMesazh modifiko(clsDatabazeAsete moduliAsete)
        {

            clsMesazh pergjigja = modifikoAmortizimFillestar(moduliAsete);
            return pergjigja;


        }



        /// <summary>
        /// MODULI ASETE:
        /// Fshin objektin e serialit te aqt-se duke i modifikuar statusin nese eshte i krijuar njehere.
        /// </summary>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese fshirja kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        public clsMesazh fshi(clsDatabazeAsete moduliAsete)
        {

            clsMesazh pergjigja = fshiAmortizimiFillestar(moduliAsete);
            return pergjigja;

        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr objektin e serialit te aqt-se sipas id se serialit.
        /// </summary>
        /// <param name="idAQTSerial">(int) Id automatike e serialit te aqt-se qe kerkojme.</param>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate marrjes se serialit te aqt-se ose False ne te kundert.</returns>
        public bool merrAmortizimFillestarSipasSerialitDheStandartit(int idAQTSerial, int idstandart, int idDokNga)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            bool pergjigje = mbushAmortizimFillestar(moduliAsete.ktheAmortizimFillestarSipasIdSerialDheIdStandarti(idAQTSerial, idstandart, idDokNga));
            moduliAsete.Dispose();
            return pergjigje;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr objektin e serialit te aqt-se sipas id se serialit ne transaksion.
        /// </summary>
        /// <param name="idAQTSerial">(int) Id automatike e serialit te aqt-se qe kerkojme.</param>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate marrjes se serialit te aqt-se ose False ne te kundert.</returns>
        public bool merrAmortizimFillestarSipasSerialitDheStandartit(int idAQTSerial, int idstandart, int idDokNga, clsDatabazeAsete moduliAsete)
        {
            bool pergjigje = mbushAmortizimFillestar(moduliAsete.ktheAmortizimFillestarSipasIdSerialDheIdStandarti(idAQTSerial, idstandart, idDokNga));
            return pergjigje;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// MODULI ASETE:
        /// Ruan objektin e serialit te aqt-se.
        /// </summary>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese ruajtja ne transaksion kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        private clsMesazh ruajAmortizimFillestar(clsDatabazeAsete moduliAsete)
        {
            clsMesazh pergjigja = moduliAsete.ruajAmortizimFillestar(out id, idSerial, idStandarti, idDokNga, amortizimiFillestar);

            return pergjigja;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Modifikon objektin e serialit te aqt-se.
        /// </summary>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese modifikimi ne transaksion kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        private clsMesazh modifikoAmortizimFillestar(clsDatabazeAsete moduliAsete)
        {
            clsMesazh pergjigja = fshiAmortizimiFillestar(moduliAsete);
            if (pergjigja.Status)
                pergjigja = ruajAmortizimFillestar(moduliAsete);
            return pergjigja;
        }



        /// <summary>
        /// MODULI ASETE:
        /// Fshin objektin e serialit te aqt-se duke i modifikuar statusin.
        /// </summary>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen clsMesazh me atribut statusi True nese fshirja ne transaksion kryhet me sukses ose False dhe mesazhin e gabimit ne te kundert.</returns>
        private clsMesazh fshiAmortizimiFillestar(clsDatabazeAsete moduliAsete)
        {
            clsMesazh pergjigja = moduliAsete.fshiAmortizimFillestarSipasIdSerialidheIdStandarti(idSerial, idStandarti, idDokNga);
            return pergjigja;
        }

        #endregion

    }
}
