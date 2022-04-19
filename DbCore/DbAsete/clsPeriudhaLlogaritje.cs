using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DbCore.DbAsete
{
    /// <summary>
    /// MODULI ASETE:
    /// Klase e ndertuar per te mbajtur periudhat e ndryshme te llogaritjes se amortizimit ne modulin e aseteve. Kjo do te thote qe per nje kompani te ndryshme fillimi i llogaritjes se amortizimit eshte i ndryshem ne varesi te fillimit dhe fundit te muajit.
    /// Te dhenat merret nga tabela T_ASETE_PERIUDHA_LLOGARITJE
    /// </summary>
    public class clsPeriudhaLlogaritje
    {
        #region Atribute

        private int idPeriudheLlogAmortizimi;
        private string emertimi;
        private string pershkrimi;
        private int llojPeriudhe;
        private string vleraPeriudhe;

        #endregion

        #region Properties

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id automatike te periudhes se llogaritjes
        /// </summary>
        public int IdPeriudheLlogAmortizimi
        {
            get { return idPeriudheLlogAmortizimi; }
            set { idPeriudheLlogAmortizimi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (string) Merr ose jep vlere emrit te periudhes se llogaritjes.
        /// </summary>
        public string Emertimi
        {
            get { return emertimi; }
            set { emertimi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (string) Merr ose jep vlere pershkrimit te periudhes se llogaritjes.
        /// </summary>
        public string Pershkrimi
        {
            get { return pershkrimi; }
            set { pershkrimi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere nese periudha e llogaritjes e regjistruar eshte per fillim muaji ose perfundim muaji.
        /// Me 0 tregon qe eshte fillim muaji, me 1 tregon qe eshte perfundim muaji.
        /// </summary>
        public int LlojPeriudhe
        {
            get { return llojPeriudhe; }
            set { llojPeriudhe = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (string) Merr ose jep vlere periudhes se llogaritjes.
        /// </summary>
        public string VleraPeriudhe
        {
            get { return vleraPeriudhe; }
            set { vleraPeriudhe = value; }
        }

        /// <summary>
        /// String default per marrjen e dates se dokumentit.
        /// </summary>
        public static readonly string DATEDOK = "Date dokumenti";

        /// <summary>
        /// String default per marrjen e dates per diten e pare te muajit korrent.
        /// </summary>
        public static readonly string DITAPAREMUAJKORRENT = "Dita e pare e muajit korrent";

        /// <summary>
        /// String default per marrjen e dates per diten e pare te muajit pasardhes.
        /// </summary>
        public static readonly string DITAPAREMUAJIPASARDHES = "Dita e pare e muajit pasardhes";

        /// <summary>
        /// String default per marrjen e dates per diten e fundit te muajit korrent.
        /// </summary>
        public static readonly string DITAFUNDITMUAJIKORRENT = "Dita e fundit e muajit korrent";

        /// <summary>
        /// String default per marrjen e dates per diten e fundit te muajit pasardhes.
        /// </summary>
        public static readonly string DITAFUNDITMUAJIPARAARDHES = "Dita e fundit e muajit paraardhes";
        private DataRow rreshti;

        #endregion

        #region Konstruktori

        /// <summary>
        /// MODULI ASETE:
        /// Krijon objektin bosh te klases clsPeriudhaLlogaritje per periudhat e llogaritjeve.
        /// </summary>
        public clsPeriudhaLlogaritje()
        {
        }

        public clsPeriudhaLlogaritje(DataRow rreshti)
        {
            
            mbushPeriudhaLlogaritjeObjekt(rreshti);
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// MODULI ASETE:
        /// Metoda perdoret per te mbushur te gjitha atributet e objektit clsPeriudhaLlogaritje sipas te dhenave qe vijne nga cdo rresht i databazes nga tabela T_ASETE_PERIUDHA_LLOGARITJE.
        /// </summary>
        /// <param name="dbDataRowPeriudhaLlogaritje">(DataRow) Merr si parameter vetem nje rresht te kthyer nga tabelat ne databaze per te mbushur nje objekt.</param>
        /// <returns>(bool) Kthen true nese nuk ndodh asnje gabim gjate leximit te te dhenave, ne te kundert false.</returns>
        internal bool mbushPeriudhaLlogaritjeObjekt(DataRow dbDataRowPeriudhaLlogaritje)
        {
            if (dbDataRowPeriudhaLlogaritje == null)
                return false;
            try
            {
                int.TryParse(dbDataRowPeriudhaLlogaritje["ID_PERIUDHE_LLOG_AMORT"].ToString(), out idPeriudheLlogAmortizimi);
                emertimi = dbDataRowPeriudhaLlogaritje["EMERTIMI"].ToString();
                pershkrimi = dbDataRowPeriudhaLlogaritje["PERSHKRIMI"].ToString();
                int.TryParse(dbDataRowPeriudhaLlogaritje["LLOJPERIUDHE"].ToString(), out llojPeriudhe);
                vleraPeriudhe = dbDataRowPeriudhaLlogaritje["VLERA_PERIUDHES"].ToString();
                return true;
            }
            catch (InvalidCastException)
            {
                throw new Exception("ERROR: Gabim gjate marrjes se periudhave te llogaritjes nga db-ja");
            }
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// MODULI ASETE:
        /// Merr objektin e periudhes se llogaritjes sipas id automatike te periudhes.
        /// </summary>
        /// <param name="idPeriudhaLlogaritjes">(int) Id e periudhes se llogaritjes qe kerkojme.</param>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate marrjes se periudhes se llogaritjes ose False ne te kundert.</returns>
        public bool merrPeriudhaLlogaritjeSipasID(int idPeriudhaLlogaritjes)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            bool pergjigje = mbushPeriudhaLlogaritjeObjekt(moduliAsete.kthePeriudhaLlogaritjeSipasID(idPeriudhaLlogaritjes));
            moduliAsete.Dispose();
            return pergjigje;
        } 
        public bool merrPeriudhaLlogaritjeSipasID(int idPeriudhaLlogaritjes,clsDatabazeAsete moduliAsete) 
        {
             bool pergjigje = mbushPeriudhaLlogaritjeObjekt(moduliAsete.kthePeriudhaLlogaritjeSipasID(idPeriudhaLlogaritjes));
         
            return pergjigje;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr objektin e periudhes se llogaritjes sipas emertimit te periudhes se llogaritjes.
        /// </summary>
        /// <param name="emertimi">(string) Emertimi i periudhes se llogaritjes qe kerkojme.</param>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate marrjes se periudhes se llogaritjes ose False ne te kundert.</returns>
        public bool merrPeriudhaLlogaritjeSipasEmertimit(string emertimi)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            bool pergjigje = mbushPeriudhaLlogaritjeObjekt(moduliAsete.kthePeriudhaLlogaritjeSipasEmertimit(emertimi));
            moduliAsete.Dispose();
            return pergjigje;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr id e periudhes se llogaritjes sipas emertimit te periudhes.
        /// </summary>
        /// <param name="emertimi">(string) Emertimi i periudhes se llogaritjes qe kerkojme.</param>
        /// <returns>Kthen id e periudhes se llogaritjes nese gjendet, ne te kundert kthen -1.</returns>
        public static int merrIDPeriudhLlogaritjeSipasEmertimit(string emertimi)
        {
            clsDatabazeAsete moduliAsete = new clsDatabazeAsete();
            int pergjigje = moduliAsete.ktheIDPeriudhLlogaritjeSipasEmertimit(emertimi);
            moduliAsete.Dispose();
            return pergjigje;
        }

        #endregion
    }
}
