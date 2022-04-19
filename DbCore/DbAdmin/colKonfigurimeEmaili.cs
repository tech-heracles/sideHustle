using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbAdmin
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsKonfigurimEmail
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    class colKonfigurimeEmaili : System.Collections.Generic.List<clsKonfigurimEmail>
    {
        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbAdmin.clsKonfigurimEmail"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsKonfigurimEmail this[int index]
        {
            get { return ((clsKonfigurimEmail)base[index]); }
        }

        /// <summary>
        /// metoda per shtimin e nje obj clsKonfigurimEmail ne nje arraylist
        /// </summary>
        public bool shtoKonrigurimEmail(clsKonfigurimEmail konf)
        {
            base.Add(konf);
            if (base.Contains(konf))
                return true;
            else return false;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsKonfigurimEmail ne nje arraylist
        /// </summary>
        public bool fshiKonfigurimEmail(clsKonfigurimEmail konf)
        {
            base.Remove(konf);
            if (base.Contains(konf))
                return false;
            else return true;
        }

        /// <summary>
        /// metoda per heqjen e te gjithe obj clsKonfigurimEmail ne nje arraylist
        /// </summary>
        public bool fshiGjitheKonfigurimEmail()
        {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsKonfigurimEmail ne nje arraylist ne nje index te caktuar
        /// </summary>
        public void fshiKeteKonfigurimEmail(int index)
        {
            base.RemoveAt(index);
        }

        /// <summary>
        /// metoda shton nje obj te ri ne array list ne pozicionin e percaktuar nga index-i
        /// </summary>
        public void shtoKonfigurimEmailNeIndeksin(int index, clsKonfigurimEmail konf)
        {
            base.Insert(index, konf);
        }

        /// <summary>
        /// metoda gjen index-in ne liste ne te cilin ndodhet nje obj i caktuar
        /// </summary>
        public int indeksiKonfigurimEmail(clsKonfigurimEmail konf)
        {
            return base.IndexOf(konf);
        }

        /// <summary>
        /// metoda kontrollon nqs nje obj ndodhet ne arraylist
        /// </summary>
        public bool ekzistonKonfigurimEmail(clsKonfigurimEmail konf)
        {
            if (base.Contains(konf))
                return true;
            else return false;
        }

        /// <summary>
        /// metoda qe kthen nr e obj qe ka arraylist
        /// </summary>
        public int numriKonfigurimEmail()
        {
            return base.Count;
        }

        #endregion
    }
}
