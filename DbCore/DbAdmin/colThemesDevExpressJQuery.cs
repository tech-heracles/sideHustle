using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbAdmin
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsThemesDevExpressJQuery
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colThemesDevexpressJQuery : System.Collections.Generic.List<clsThemesDevExpressJQuery>
    {       

        #region Metoda Publike
        /// <summary>
        /// kthen objektin <see cref="DbCore.DbAdmin.clsThemesDevexpress"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsThemesDevExpressJQuery this[int index]
        {
            get { return ((clsThemesDevExpressJQuery)base[index]); }
        }
        /// <summary>
        /// metoda per shtimin e nje obj clsThemesDevexpress ne nje arraylist
        /// </summary>
        public bool shtoThemeDevExpress(clsThemesDevExpressJQuery theme)
        {
            base.Add(theme);
            if (base.Contains(theme))
                return true;
            else return false;
        }
        /// <summary>
        /// metoda per heqjen e nje obj clsThemesDevexpress ne nje arraylist
        /// </summary>
        public bool fshiThemeDevExpress(clsThemesDevExpressJQuery theme)
        {
            base.Remove(theme);
            if (base.Contains(theme))
                return false;
            else return true;
        }
        /// <summary>
        /// metoda per heqjen e nje obj clsThemesDevexpress ne nje arraylist
        /// </summary>
        public bool fshiGjitheThemesDevExpress()
        {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// metoda per heqjen e nje obj clsThemesDevexpress ne nje arraylist
        /// </summary>
        public void fshiKeteThemeDevExpress(int index)
        {
            base.RemoveAt(index);
        }
        /// <summary>
        /// metoda shton nje obj clsThemesDevexpress te ri ne array list ne pozicionin e percaktuar nga index-i
        /// </summary>
        public void shtoThemeDevExpressiNeIndeksin(int index, clsThemesDevExpressJQuery theme)
        {
            base.Insert(index, theme);
        }
        /// <summary>
        /// metoda gjen index-in ne liste ne te cilin ndodhet nje obj clsThemesDevexpress i caktuar
        /// </summary>
        public int indeksiThemeDevExpress(clsThemesDevExpressJQuery theme)
        {
            return base.IndexOf(theme);
        }
        /// <summary>
        /// metoda kontrollon nqs nje obj clsThemesDevexpress ndodhet ne arraylist
        /// </summary>
        public bool ekzistonThemeDevExpress(clsThemesDevExpressJQuery theme)
        {
            if (base.Contains(theme))
                return true;
            else return false;
        }
        /// <summary>
        /// metoda qe kthen nr e obj clsThemesDevexpress qe ka arraylist
        /// </summary>
        public int numriThemeDevExpress()
        {
            return base.Count;
        }

        /// <summary>
        /// mbush te gjitha temat e devexpressit
        /// </summary>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushGjitheThemesDevExpressJQuery()
        {
            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();
            bool sukses = mbushThemesDevExpressJQuery(dbAdmin.ktheGjitheThemesDevExpressJQuery());
            dbAdmin.Dispose();
            return sukses;
        }

        public bool mbushThemesDevExpressJQuerysSipasLloji(bool lloji)
        {
            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();
            bool sukses = mbushThemesDevExpressJQuery(dbAdmin.ktheThemesDevExpressJQuerySipasLloji(lloji));
            dbAdmin.Dispose();
            return sukses;
        }

        #endregion

        #region Metoda Private

        private bool mbushThemesDevExpressJQuery(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsThemesDevExpressJQuery theme = new clsThemesDevExpressJQuery();
                    //theme.mbushThemeDevExpressJQuery(rreshti);
                    this.Add(new clsThemesDevExpressJQuery(rreshti));
                }
            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }

        #endregion
    }
}
