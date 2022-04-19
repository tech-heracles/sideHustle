using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    /// Kjo klase permban metodat e nevojshme per te punuar me te dhenat e tabeles : T_KONFIG_PIVOTGRIDA_TRUPI
    /// </summary>

    public class clsKonfigPivotGridaTrupi
    {
        #region Atribute
        
        private int idKonfigPGTrupi;
        private int idKonfigPGKoka;
        private int idKolonaPG;
        private string emerKoloneShfaq;
        private string emerTableDB;
        private string emerKoloneDB;
        private string grupimKolone;
        private string tipiKolones;
        private bool analitikTotal;
        private bool visibility;
        private string zona;
        private int rendi;
        private double width;
        private int llojGrupimi;
        private DataRow row;
        #endregion

        #region Properties

        /// <summary>
        /// Kthen/vendos id e trupit se konfigrimit te pivot grides
        /// </summary>
        public int IdKonfigPGTrupi
        {
            get { return idKonfigPGTrupi;  }
            set { idKonfigPGTrupi = value; }
        }
        

        /// <summary>
        /// Kthen/vendos id e kokes se konfigurimit te pivot grides
        /// </summary>
        public int IdKonfigPGKoka
        {
            get { return idKonfigPGKoka; }
            set { idKonfigPGKoka = value; }
        }


        /// <summary>
        /// Kthen/vendos id e kolones se konfigurimit te pivot grides
        /// </summary>
        public int IdKolonaPG
        {
            get { return idKolonaPG; }
            set { idKolonaPG = value; }
        }

        /// <summary>
        /// Kthen emrin me te cilin do shfaqet kolona se konfigurimit te pivot grides
        /// </summary>
        public string EmerKoloneShfaq
        {
            get { return emerKoloneShfaq; }
        }

        /// <summary>
        /// Kthen emrin e tabeles se DB se ciles i perket kolona e konfigurimit te pivot grides
        /// </summary>
        public string EmerTabeleDB
        {
            get { return emerTableDB; }
            set { emerTableDB = value; }
        }

        /// <summary>
        /// Kthenemrin e kolones se DB se ciles i referohet kolona e konfigurimit te pivot grides
        /// </summary>
        public string EmerKoloneDB
        {
            get { return emerKoloneDB; }
        }

        /// <summary>
        /// Kthen emrin e grupimit te kolones se konfigurimit te pivot grides
        /// </summary>
        public string GrupimKolone
        {
            get { return grupimKolone; }
        }

        /// <summary>
        /// Kthen tipin e kolones se konfigurimit te pivot grides
        /// </summary>
        public string TipiKolones
        {
            get { return tipiKolones; }
        }

        /// <summary>
        /// Kthen vleren e analitik/total te kolones.
        /// True - nese kolona perfaqeson nje vlere totale.
        /// False - nese kolona perfaqeson nje vlere analitike.
        /// </summary>
        public bool AnalitikTotal
        {
            get { return analitikTotal; }
        }

        /// <summary>
        /// Kthen/vendos nese kolona do te shfaqet ose jo ne konfigurimin e pivot grides
        /// </summary>
        public bool Visibility
        {
            get { return visibility; }
            set { visibility = value; }
        }

        /// <summary>
        /// Kthen/vendos zonen ne te cilen do shfaqet kolona ne raport(row, column, filter ose data)
        /// </summary>
        public string Zona
        {
            get { return zona; }
            set { zona = value; }
        }

        /// <summary>
        /// Kthen/vendos rendin e kolones ne zonen perkatese ne raport
        /// </summary>
        public int Rendi
        {
            get { return rendi; }
            set { rendi = value; }
        }

        /// <summary>
        /// Kthen/vendos gjeresine e kolones ne raport
        /// </summary>
        public double Width
        {
            get { return width; }
            set { width = value; }
        }
        /// <summary>
        /// kthen/vendos llojin e grupimit per kete kolone (max,min,sum,count)
        /// </summary>
        public int LlojGrupimi
        {
            get { return llojGrupimi; }
            set { llojGrupimi = value; }
        }

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori default i klases
        /// </summary>
        public clsKonfigPivotGridaTrupi()
        {

        }

        /// <summary>
        /// Konstruktori me parametra i klases
        /// </summary>
        /// <param name="idKonfigPGTr">id konfig trupi</param>
        /// <param name="idKonfigPGK">id konfig koka</param>
        /// <param name="idKolPG">id kolona</param>
        /// <param name="visible">visbility i kolones</param>
        /// <param name="zonaKol">zona ku do shfaqet kolona</param>
        /// <param name="rendiKol">rendi i shfaqjes</param>
        /// <param name="widthKol">gjersia e kolones</param>
        /// <param name="llojGrupimi">lloj i grupimit per kolonen</param>
        /// 
        public clsKonfigPivotGridaTrupi(int idKonfigPGTr, int idKonfigPGK, int idKolPG, bool visible, string zonaKol, int rendiKol, double widthKol,int llojGrupimi)
        {
            idKonfigPGTrupi = idKonfigPGTr;
            idKonfigPGKoka = idKonfigPGK;
            idKolonaPG = idKolPG;
            visibility = visible;
            zona = zonaKol;
            rendi = rendiKol;
            width = widthKol;
           this.llojGrupimi = llojGrupimi;
        }

        /// <summary>
        /// Konstruktori qe kthen objektin e trupit te konfigurimit te pivot grides sipas id
        /// </summary>
        /// <param name="idKonfigPGTrupi">id e trupit te konfigurimit</param>
        public clsKonfigPivotGridaTrupi(int idKonfPGTrupi)
        {
            DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegj = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
            mbushKonfigPGTrupi(dbRegj.merrKonfigPGTrupiSipasID(idKonfPGTrupi));
            dbRegj.Dispose();
        }

        public clsKonfigPivotGridaTrupi(DataRow row)
        {
            
            mbushKonfigPGTrupi(row);
        }

        #endregion

        #region Metoda publike

        #endregion

        #region Metoda internal

        internal bool mbushKonfigPGTrupi(DataRow konfigPGTrupiDataRow)
        {
            if (konfigPGTrupiDataRow != null)
            {
                try
                {
                    int.TryParse(konfigPGTrupiDataRow["IDKONFPIVOTGRIDATRUPI"].ToString(), out idKonfigPGTrupi);
                    int.TryParse(konfigPGTrupiDataRow["IDPIVOTGRIDAKOKA"].ToString(), out idKonfigPGKoka);
                    int.TryParse(konfigPGTrupiDataRow["IDKOLONAPIVOTGRID"].ToString(), out idKolonaPG);
                    emerKoloneShfaq = konfigPGTrupiDataRow["EMERKOLONESHFAQ"].ToString();
                    emerTableDB = konfigPGTrupiDataRow["EMERTABELEDB"].ToString();
                    emerKoloneDB = konfigPGTrupiDataRow["EMERKOLONEDB"].ToString();
                    Boolean.TryParse(konfigPGTrupiDataRow["VISIBILITY"].ToString(), out visibility);
                    zona = konfigPGTrupiDataRow["ZONA"].ToString();
                    int.TryParse(konfigPGTrupiDataRow["RENDI"].ToString(), out rendi);
                    double.TryParse(konfigPGTrupiDataRow["WIDTH"].ToString(), out width);
                    grupimKolone = konfigPGTrupiDataRow["GRUPIMKOLONE"].ToString();
                    tipiKolones = konfigPGTrupiDataRow["TIPIKOLONES"].ToString();
                    int.TryParse(konfigPGTrupiDataRow["LLOJGRUPIMI"].ToString(),out llojGrupimi);
                    bool.TryParse(konfigPGTrupiDataRow["ANALITIKTOTAL"].ToString(), out analitikTotal);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se konfigurimit te raportit nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion

    }
}