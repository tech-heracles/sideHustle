using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    /// Kjo klase perdoret per te menaxhuar te dhenat e tabeles T_GRUPIMKOLONE_PIVOTGRID
    /// </summary>
    public class clsGrupimeKolone_PivotGrid
    {
        #region Atribute

        private int idGrupimKolone;
        private string emerGrupimi;
        private string pershkrimGrupimi;
        private string moduli;
        private DataRow row;

        #endregion

        #region Konstruktore

        /// <summary>
        /// Konstruktori Default i klases
        /// </summary>
        public clsGrupimeKolone_PivotGrid()
        {

        }

        /// <summary>
        /// Konstruktori me parametra i klases
        /// </summary>
        /// <param name="idGrup">id e grupimit</param>
        /// <param name="emerGrup">emri i grupimit</param>
        /// <param name="pershkrimGrup">pershkrimi qe shfaqe te koka e grides</param>
        public clsGrupimeKolone_PivotGrid(int idGrup, string emerGrup, string pershkrimGrup, string mod)
        {
            this.idGrupimKolone = idGrup;
            this.emerGrupimi = emerGrup;
            this.pershkrimGrupimi = pershkrimGrup;
            this.moduli = mod;
        }

        public clsGrupimeKolone_PivotGrid(DataRow row)
        {
            
            mbushGrupimKolone(row);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos id e grupimit te kolones
        /// </summary>
        public int IdGrupimKolone
        {
            get { return idGrupimKolone; }
            set { idGrupimKolone = value; }
        }

        /// <summary>
        /// Kthen/Vendos emrin e grupimit te kolones
        /// </summary>
        public String EmerGrupimKolone
        {
            get { return emerGrupimi; }
            set { emerGrupimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos pershkrimin e grupimit te kolones
        /// </summary>
        public String PershkrimGrupimKolone
        {
            get { return pershkrimGrupimi; }
            set { pershkrimGrupimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos emrin e modulit te cilit i perket grupimi i kolones
        /// </summary>
        public String Moduli
        {
            get { return moduli; }
            set { moduli = value; }
        }

        #endregion

        #region Metoda Publike



        #endregion

        #region Metoda Private

        internal bool mbushGrupimKolone(DataRow dbDataRowGrupimKolona)
        {
            if (dbDataRowGrupimKolona != null)
            {
                try
                {
                    int.TryParse(dbDataRowGrupimKolona["IDGRUPIMKOLONE"].ToString(), out idGrupimKolone);
                    emerGrupimi = dbDataRowGrupimKolona["EMERGRUPMI"].ToString();
                    pershkrimGrupimi = dbDataRowGrupimKolona["PERSHKRIMGRUPIMI"].ToString();
                    moduli = dbDataRowGrupimKolona["MODULI"].ToString();
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se te dhenave te grupimit te kolonave nga DB!");
                }
            }
            else
                return false;
        }

        #endregion

    }
}
