using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbInventari
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne formulat
    ///  (Te dhenat  merren nga tabela : T_FORMULA)
    ///mban te dhenat mbi detajimet
    /// </summary>
    public class clsFormula
    {
        #region Atribute

        private int idFormula;
        private string kodFormula;
        private string pershkrimFormula;
        private int idNdermarrje;
        private int idPerdorues;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private int idStatusDok;
        private DataRow rreshti;

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne e formules
        /// </summary>
        public int IdFormula        {
            get { return idFormula; }
            set { idFormula = value; }
        }

        /// <summary>
        /// Kthen/Vendos kodin e formules.
        /// </summary>
        public string KodFormula
        {
            get { return kodFormula; }
            set { kodFormula = value; }
        }

        /// <summary>
        /// Kthen/Vendos pershkrimin e formules
        /// </summary>
        public string PershkrimFormula
        {
            get { return pershkrimFormula; }
            set { pershkrimFormula = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e ndermarrjes
        /// </summary>
        public int IdNdermarrje
        {
            get { return idNdermarrje; }
            set { idNdermarrje = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e perdoruesit
        /// </summary>
        public int IdPerdorues
        {
            get { return idPerdorues; }
            set { idPerdorues = value; }
        }

        /// <summary>
        /// Kthen daten e krijimit
        /// </summary>
        public DateTime DtKrijimi
        {
            get { return dtKrijimi; }

        }

        /// <summary>
        /// Kthen daten e modifikimit
        /// </summary>
        public DateTime DtModifikimi
        {
            get { return dtModifikimi; }

        }

        /// <summary>
        /// Kthen/Vendos idstatusdok
        /// </summary>
        public int IdStatusDok
        {
            get { return idStatusDok; }
            set { idStatusDok = value; }
        }

        #endregion

        #region Konstruktoret
       
        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public clsFormula()
        {            
        }

        /// <summary>
        /// Konstruktori sipas id se formules.
        /// </summary>
        /// <param name="idFormula">Id e formules</param>
        public clsFormula(int idFormula)
        {
            clsDatabaseInventari data = new clsDatabaseInventari();
            mbushFormulen(data.merrFormulen(idFormula));
            data.Dispose();
        }

        public clsFormula(int idFormula, string kodFormula, string pershkrimFormula, int idNdermarrje, int idPerdorues, int idStatusDok)
        {
            this.idFormula = idFormula;
            this.kodFormula = kodFormula;
            this.pershkrimFormula = pershkrimFormula;
            this.idNdermarrje = idNdermarrje;
            this.idPerdorues = idPerdorues;
            this.idStatusDok = idStatusDok;
        }

        public clsFormula(DataRow rreshti)
        {
            
            mbushFormulen(rreshti);
        }
        
        #endregion

        #region Metoda Publike

        public clsMesazh ruaj()
        {
            clsDatabaseInventari data = new clsDatabaseInventari();
            int idFormule = 0;
            clsMesazh u_ruajt = data.ruajFormule(out idFormule, this.kodFormula, this.pershkrimFormula, this.idNdermarrje, this.idPerdorues, this.idStatusDok);
            this.idFormula = idFormule;
            data.Dispose();
            return u_ruajt;
        }

        public clsMesazh modifiko()
        {
            clsDatabaseInventari data = new clsDatabaseInventari();
            clsMesazh u_modifikua = data.modifikoFormule(this.idFormula, this.kodFormula, this.pershkrimFormula, this.idNdermarrje, this.idPerdorues, this.idStatusDok);
            data.Dispose();
            return u_modifikua;
        }

        public clsMesazh fshi()
        {
            clsDatabaseInventari dbInv = new clsDatabaseInventari();
            clsMesazh u_fshi = dbInv.fshiFormule(this.idFormula);
            dbInv.Dispose();
            return u_fshi;
        }

        public static bool ekzistonFormuleSipasKodit(string kodFormula, int idNdermarje)
        {
            clsDatabaseInventari dbInv = new clsDatabaseInventari();
            bool ekziston = dbInv.ekzistonFormuleSipasKodit(kodFormula, idNdermarje);
            dbInv.Dispose();
            return ekziston;
        }

        public static bool ekzistonFormuleSipasPershkrimit(string pershkrimFormula, int idNdermarje)
        {
            clsDatabaseInventari dbInv = new clsDatabaseInventari();
            bool ekziston = dbInv.ekzistonFormuleSipasPershkrimit(pershkrimFormula, idNdermarje);
            dbInv.Dispose();
            return ekziston;
        }

        #endregion

        #region Metoda Private

        internal bool mbushFormulen(DataRow dbDataRowFormula)
        {
            if (dbDataRowFormula != null)
            {
                try
                {
                    int.TryParse(dbDataRowFormula["IDFORMULA"].ToString(), out idFormula);
                    kodFormula = dbDataRowFormula["KODFORMULA"].ToString();
                    pershkrimFormula = dbDataRowFormula["PERSHKRIMFORMULA"].ToString();                    
                    int.TryParse(dbDataRowFormula["IDNDERMARJE"].ToString(), out idNdermarrje);
                    int.TryParse(dbDataRowFormula["IDPERDORUES"].ToString(), out idPerdorues);
                    DateTime.TryParse(dbDataRowFormula["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRowFormula["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    int.TryParse(dbDataRowFormula["IDSTATUSDOK"].ToString(), out idStatusDok);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new MyException("ERROR: Gabim gjate cast-it!");
                }
                catch (Exception)
                {
                    throw new MyException("ERROR: Gabim gjate marrjes se formules nga db-ja!");
                }
            }
            else
                return false;
        }

        #endregion
    }
}
