using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;


namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsTabPerKontroll
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colTabPerKontroll : System.Collections.Generic.List<clsTabPerKontroll >
    {
        #region Konstruktoret

        /// <summary>
        /// konstruktor pa parametra
        /// </summary>
        public colTabPerKontroll()
        {
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idNiveli">id e nivelit</param>
        public colTabPerKontroll(int idNiveli)
        {
            clsDatabaseRegjistrim dbTab = new clsDatabaseRegjistrim();
            mbushTabetperKontroll(dbTab.ktheTabPerKontrollSipasIdNivelit(idNiveli));
            dbTab.Dispose();
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbRegjistrim.clsTabPerKontroll"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsTabPerKontroll this[int index]
        {
            get { return ((clsTabPerKontroll)base[index]); }
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        /// <see cref="DbCore.DbRegjistrim.clsTabPerKontroll"/> 
        /// </summary>
        private bool mbushTabetperKontroll(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsTabPerKontroll tab = new clsTabPerKontroll();
                    //tab.mbushTabPerKontroll(rreshti);
                    Add(new clsTabPerKontroll(rreshti));
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
        [Obsolete("Perdor: bool mbushTabetperKontroll(DataTable dt)", true)]
        public colTabPerKontroll mbushArrayListTabPerKontroll(DataSet ds)
        {
            colTabPerKontroll tabPerKontroll = new colTabPerKontroll();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsTabPerKontroll tab = new clsTabPerKontroll();

                tab.Id = int.Parse(rreshti[0].ToString());
                tab.Kodi = rreshti[1].ToString();
                tab.EmerTabele = rreshti[2].ToString();
                tab.Pershkrimi = rreshti[3].ToString();

                tabPerKontroll.Add(tab);
            }
            return tabPerKontroll;
        }
    }
}
