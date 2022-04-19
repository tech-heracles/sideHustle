using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbInventari
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsGarancia
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
  public  class colGarancite : System.Collections.Generic.List<clsGarancia>
    {
        #region Konstruktoret       

        /// <summary>
        /// konstruktori pa parametra i klases, merr te gjitha llojet e garncive
        /// </summary>
        public colGarancite()
        {
            clsDatabaseInventari dbInventari = new clsDatabaseInventari();
            mbushGarancite(dbInventari.merrGarrancite());
            dbInventari.Dispose();
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbInventari.clsGarancia"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsGarancia this[int index]
        {
            get { return ((clsGarancia)base[index]); }
        }

        /// <summary>
        /// metoda per shtimin e nje obj clsGarancia ne nje arraylist
        /// </summary>
        public bool shtoGaranci(clsGarancia garanci)
        {
            Add(garanci);
            if (base.Contains(garanci))
                return true;
            else return false;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsGarancia ne nje arraylist
        /// </summary>
        public bool fshiGarancine(clsGarancia garanci)
        {
            base.Remove(garanci);
            if (base.Contains(garanci))
                return false;
            else return true;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsGarancia ne nje arraylist
        /// </summary>
        public bool fshiGjitheGarancite()
        {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsGarancia ne nje arraylist
        /// </summary>
        public void fshiKeteGaranci(int index)
        {
            base.RemoveAt(index);
        }

        /// <summary>
        /// metoda shton nje obj te ri ne array list ne pozicionin e percaktuar nga index-i
        /// </summary>
        public void shtoGaranciNeIndeksin(int index, clsGarancia garanci)
        {
            base.Insert(index, garanci);
        }

        /// <summary>
        /// metoda gjen index-in ne liste ne te cilin ndodhet nje obj i caktuar
        /// </summary>
        public int indeksiGarancise(clsGarancia garanci)
        {
            return base.IndexOf(garanci);
        }

        /// <summary>
        /// metoda kontrollon nqs nje obj ndodhet ne arraylist
        /// </summary>
        public bool ekzistonGarancia(clsGarancia garanci)
        {
            if (base.Contains(garanci))
                return true;
            else return false;
        }

        /// <summary>
        /// metoda qe kthen nr e obj qe ka arraylist
        /// </summary>
        public int numriGarancive()
        {
            return base.Count;
        }

        /// <summary>
        /// merr artikujt sipas ndermarrjes dhe autorizimit dhe qe fillojne me kodbar te caktuar
        /// </summary>
        /// <param name="idNder">id e ndermarrjes</param>
        /// <param name="idperdorues">id e perdoruesit</param>
        /// <param name="kodbar">kodbarin e artikullit</param>
        /// <returns>kthen nje vlere True nese mbushja kryhet me sukses, ne te kundert false</returns>
        public bool merrGjitheGarancite()
        {
            clsDatabaseInventari dbInventari = new clsDatabaseInventari();
            bool sukses = mbushGarancite(dbInventari.merrGarrancite());
            dbInventari.Dispose();
            return sukses;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// Metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera nga nje store procedure.
        /// </summary>
        /// <param name="dt">Merr si parameter nje DataTable.</param>
        /// <returns>Kthen True nese kryhet me sukses, ne te kundert False.</returns>
        /// <param name="dbInventari"></param>
        public bool mbushGarancite(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsGarancia garanci = new clsGarancia();
                    //garanci.mbushGaranci(rreshti);
                    this.Add(new clsGarancia(rreshti));
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