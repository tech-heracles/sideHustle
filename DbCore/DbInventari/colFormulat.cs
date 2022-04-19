using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbInventari
{

    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsFormula
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colFormulat : System.Collections.Generic.List<clsFormula>
    {
        #region Konstruktoret

        /// <summary>
        /// konstruktori pa parametra i klases
        /// </summary>
        public colFormulat()
        {
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbInventari.clsFormula"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsFormula this[int index]
        {
            get { return ((clsFormula)base[index]); }
        }

        /// <summary>
        /// metoda per shtimin e nje obj clsFormula ne nje arraylist
        /// </summary>
        public bool shtoFormule(clsFormula formula)
        {
            Add(formula);
            if (base.Contains(formula))
                return true;
            else return false;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsFormula ne nje arraylist
        /// </summary>
        public bool fshiFormulen(clsFormula formula)
        {
            base.Remove(formula);
            if (base.Contains(formula))
                return false;
            else return true;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsFormula ne nje arraylist
        /// </summary>
        public bool fshiGjitheFormulat()
        {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsFormula ne nje arraylist
        /// </summary>
        public void fshiKeteFormule(int index)
        {
            base.RemoveAt(index);
        }

        /// <summary>
        /// metoda shton nje obj te ri ne array list ne pozicionin e percaktuar nga index-i
        /// </summary>
        public void shtoFormuleNeIndeksin(int index, clsFormula formula)
        {
            base.Insert(index, formula);
        }

        /// <summary>
        /// metoda gjen index-in ne liste ne te cilin ndodhet nje obj i caktuar
        /// </summary>
        public int indeksiFormules(clsFormula formula)
        {
            return base.IndexOf(formula);
        }

        /// <summary>
        /// metoda kontrollon nqs nje obj ndodhet ne arraylist
        /// </summary>
        public bool ekzistonFormula(clsFormula formula)
        {
            if (base.Contains(formula))
                return true;
            else return false;
        }

        /// <summary>
        /// metoda qe kthen nr e obj qe ka arraylist
        /// </summary>
        public int numriFormulave()
        {
            return base.Count;
        }

        /// <summary>
        /// mbush formulat sipas id se ndermarrjes
        /// </summary>
        /// <param name="idndermarje">id e ndermarrjes</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        public static DataTable merrFormulatSipasNdermarrjes(int idndermarje)
        {
            clsDatabaseInventari dbInv = new clsDatabaseInventari();
            DataTable tabela = dbInv.ktheFormulatENdermarrjes(idndermarje);
            dbInv.Dispose();
            return tabela;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// Metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera nga nje stored procedure.
        /// </summary>
        /// <param name="dt">Merr si parameter nje DataTable.</param>
        /// <returns>Kthen True nese kryhet me sukses, ne te kundert False.</returns>
        private bool mbushFormulat(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsFormula formula = new clsFormula();
                    //formula.mbushFormulen(rreshti);
                    this.Add(new clsFormula(rreshti));
                }
            //}
            //catch (Exception)
            //{
            //    return false;
            //    //throw;
            //}
            return true;
        }

        #endregion
    }
}