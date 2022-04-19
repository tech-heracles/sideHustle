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
    /// Klase e ndertuar per te mbajtur objektin e serialeve per rivleresim. Ruajtja e gjithe serialeve te cilet kane rivleresime te ndryshme ne standarte te ndryshme.
    /// Te dhenat merret nga tabela T_ASETE_SERIALxRIVLERESIM.
    /// </summary>
    public class clsSerialPerRivleresim
    {
        #region Atribute

        private int idSerialxRivleresim;
        private int idKoka;
        private int idSeriali;
        private int idStandarti;
        private double sasiaProgresive;
        private double vleftaShtese;
        private DateTime dataRivlersimi;
        private int idNjesiAdministrative;
        private int idStatusDokumenti;
        private int idNdermarrje;
        private int idPerdoruesi;
        private int idKrijuesi;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;

        #endregion

        #region Properties

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se serialit qe i vendoset vlera e rivleresuar per standartin.
        /// </summary>
        public int IdSerialxRivleresim
        {
            get { return idSerialxRivleresim; }
            set { idSerialxRivleresim = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id-se se kokes se dokumentit
        /// </summary>
        public int IdKoka
        {
            get { return idKoka; }
            set { idKoka = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id-se se serialit qe po rivleresohet.
        /// </summary>
        public int IdSeriali
        {
            get { return idSeriali; }
            set { idSeriali = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se standartit per te cilen po rivleresohet.
        /// </summary>
        public int IdStandarti
        {
            get { return idStandarti; }
            set { idStandarti = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (double) Merr ose jep vlere sasise progresive te cilen ka seriali ne momenti qe po kryhet veprimi.
        /// </summary>
        public double SasiaProgresive
        {
            get { return sasiaProgresive; }
            set { sasiaProgresive = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (double) Merr ose jep vlere vleftes shtese per te cilen po rivleresohet seriali. Mund te jete pozitive ose negative.
        /// </summary>
        public double VleftaShtese
        {
            get { return vleftaShtese; }
            set { vleftaShtese = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (DateTime) Merr ose jep vlere dates ne te cilen po behet rivleresimi.
        /// </summary>
        public DateTime DataRivlersimi
        {
            get { return dataRivlersimi; }
            set { dataRivlersimi = value; }
        }
            
        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se magazines se trupit.
        /// </summary>
        public int IdNjesiAdministrative
        {
            get { return idNjesiAdministrative; }
            set { idNjesiAdministrative = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere se ne cfare gjendje eshte seriali i aqt-se, i ruajtur, fshire, apo modifikuar.
        /// </summary>
        public int IdStatusDokumenti
        {
            get { return idStatusDokumenti; }
            set { idStatusDokumenti = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se ndermarrjes ne te cilen eshte seriali i aqt-se.
        /// </summary>
        public int IdNdermarrje
        {
            get { return idNdermarrje; }
            set { idNdermarrje = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se perdoruesit qe ka kryer modifikimi e serialit te aqt-se.
        /// </summary>
        public int IdPerdoruesi
        {
            get { return idPerdoruesi; }
            set { idPerdoruesi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se krijuesit te serialit te aqt-se.
        /// </summary>
        public int IdKrijuesi
        {
            get { return idKrijuesi; }
            set { idKrijuesi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (DateTime) Merr ose jep vlere dates se krijimit te serialit te aqt-se.
        /// </summary>
        public DateTime DtKrijimi
        {
            get { return dtKrijimi; }
            set { dtKrijimi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (DateTime) Merr ose jep vlere dates se modifikimit te serialit te aqt-se.
        /// </summary>
        public DateTime DtModifikimi
        {
            get { return dtModifikimi; }
            set { dtModifikimi = value; }
        }

        #endregion

        #region Konstruktori
                
        /// <summary>
        /// MODULI ASETE:
        /// Krijon objektin bosh te klases clsSerialPerRivleresim per serialet qe rivleresohen sipas standarteve .
        /// </summary>
        public clsSerialPerRivleresim()
        {
        }

        /// <summary>
        /// MODULI ASETE:
        /// Krijon objektin e clsSerialPerRivleresim sipas parametrave:
        /// </summary>
        /// <param name="idSeriali">(int) Id e serialit qe po rivleresohet</param>
        /// <param name="idNjesiAdministrative">(int) Id e magazines ku po rivleresohet</param>
        /// <param name="idStandarti">(int) Id e standartit te amortizimit</param>
        /// <param name="sasiaProgresive">(Double) Sasia progresive e serialit ne momentin e rivleresimit</param>
        /// <param name="vleftaShtese">(Double) Vlefta shtese e rivleresuar</param>
        /// <param name="dataRivlersimi">(DateTime) Data e dokumentit te rivleresuar</param>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes</param>
        /// <param name="idPerdoruesi">(int) Id e perdoruesit</param>
        public clsSerialPerRivleresim(int idSeriali, int idNjesiAdministrative, int idStandarti, double sasiaProgresive, double vleftaShtese, DateTime dataRivlersimi, int idNdermarrje, int idPerdoruesi, int idStatusDokumenti)
        {
            idSerialxRivleresim = 0;
            this.idSeriali = idSeriali;
            this.idStandarti = idStandarti;
            this.sasiaProgresive = sasiaProgresive;
            this.vleftaShtese = vleftaShtese;
            this.dataRivlersimi = dataRivlersimi;
            this.idNjesiAdministrative = idNjesiAdministrative;
            this.idNdermarrje = idNdermarrje;
            this.idPerdoruesi = idPerdoruesi;
            this.idKrijuesi = idPerdoruesi;
            this.idStatusDokumenti = idStatusDokumenti;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Krijon objektin plote te klases clsSerialPerRivleresim per serialet qe rivleresohen sipas standarteve .
        /// </summary>
        /// <param name="rreshtSerialRivlersim">(Dictionary) Objekti i cili do te mbushe klasen.</param>
        public clsSerialPerRivleresim(Dictionary<string, object> rreshtSerialRivlersim)
        {
            idSerialxRivleresim = Convert.ToInt32(rreshtSerialRivlersim["IDSERIALxSTANDART"]);
            idSeriali = Convert.ToInt32(rreshtSerialRivlersim["IDSERIALI"].ToString());
            idStandarti = Convert.ToInt32(rreshtSerialRivlersim["IDSTANDARTI"].ToString());
            sasiaProgresive = Convert.ToDouble(rreshtSerialRivlersim["SASIAPROGRESIVE"]);
            vleftaShtese = Convert.ToDouble(rreshtSerialRivlersim["VLEFTASHTESE"]);
            dataRivlersimi = Convert.ToDateTime(rreshtSerialRivlersim["DATARIVLERESIM"]);
            idStatusDokumenti = Convert.ToInt32(rreshtSerialRivlersim["IDSTATUSDOK"]);
            idNdermarrje = Convert.ToInt32(rreshtSerialRivlersim["IDNDERMARRJE"]);
            idPerdoruesi = Convert.ToInt32(rreshtSerialRivlersim["IDPERDORUESI"]);
            idKrijuesi = Convert.ToInt32(rreshtSerialRivlersim["IDKRIJUESI"]);
            dtKrijimi = Convert.ToDateTime(rreshtSerialRivlersim["DTKRIJIMI"]);
            dtModifikimi = Convert.ToDateTime(rreshtSerialRivlersim["DTMODIFIKIMI"]);         
        }

        public clsSerialPerRivleresim(DataRow rreshti)
        {
            
            mbushSerialPerRivlersimObjekt(rreshti);
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// MODULI ASETE:
        /// Metoda perdoret per te mbushur te gjitha atributet e objektit clsSerialPerRivleresim sipas te dhenave qe vijne nga cdo rresht i databazes nga tabela T_ASETE_SERIALxRIVLERESIM.
        /// </summary>
        /// <param name="dbDataRowSerialPerRivleresim">(DataRow) Merr si parameter vetem nje rresht te kthyer nga tabelat ne databaze per te mbushur nje objekt.</param>
        /// <returns>(bool) Kthen true nese nuk ndodh asnje gabim gjate leximit te te dhenave, ne te kundert false.</returns>
        internal bool mbushSerialPerRivlersimObjekt(DataRow dbDataRowSerialPerRivleresim)
        {
            if (dbDataRowSerialPerRivleresim == null)
                return false;
            try
            {
                int.TryParse(dbDataRowSerialPerRivleresim["IDSERIALxSTANDART"].ToString(), out idSerialxRivleresim);
                int.TryParse(dbDataRowSerialPerRivleresim["IDSERIALI"].ToString(), out idSeriali);
                int.TryParse(dbDataRowSerialPerRivleresim["IDSTANDARTI"].ToString(), out idStandarti);
                double.TryParse(dbDataRowSerialPerRivleresim["SASIAPROGRESIVE"].ToString(), out sasiaProgresive);
                double.TryParse(dbDataRowSerialPerRivleresim["VLEFTASHTESE"].ToString(), out vleftaShtese);
                DateTime.TryParse(dbDataRowSerialPerRivleresim["DATARIVLERESIM"].ToString(), out dataRivlersimi);
                int.TryParse(dbDataRowSerialPerRivleresim["IDSTATUSDOK"].ToString(), out idStatusDokumenti);
                int.TryParse(dbDataRowSerialPerRivleresim["IDNDERMARJE"].ToString(), out idNdermarrje);
                int.TryParse(dbDataRowSerialPerRivleresim["IDPERDORUESI"].ToString(), out idPerdoruesi);
                int.TryParse(dbDataRowSerialPerRivleresim["IDKRIJUESI"].ToString(), out idKrijuesi);
                DateTime.TryParse(dbDataRowSerialPerRivleresim["DTKRIJIMI"].ToString(), out dtKrijimi);
                DateTime.TryParse(dbDataRowSerialPerRivleresim["DTMODIFIKIMI"].ToString(), out dtModifikimi);

                return true;
            }
            catch (InvalidCastException)
            {
                throw new Exception("ERROR: Gabim gjate marrjes se serialit me rivleresim");
            }
        }

        #endregion

        #region Metoda Publike

        public clsMesazh ruaj()
        {
            clsMesazh pergjigja = ruajSerialRivleresim();
            return pergjigja;
        }
        
        #endregion

        #region Metoda Private

        /// <summary>
        /// MODULI ASETE:
        /// Ruan objektin e rreshtit te trupit te serialit te rivleresuar.
        /// </summary>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen True nese ruajtja perfundon me sukses, ose False nese ruajtja jep gabim.</returns>
        private clsMesazh ruajSerialRivleresim()
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            clsMesazh pergjigja = moduliAsete.ruajSerialinERivleresuar(out idSerialxRivleresim, idKoka, idSeriali, idStandarti, sasiaProgresive, vleftaShtese, dataRivlersimi, idNjesiAdministrative, idNdermarrje, idPerdoruesi, idKrijuesi, IdStatusDokumenti);
            return pergjigja;
        }

        #endregion

    }
}
