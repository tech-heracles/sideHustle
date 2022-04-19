using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;

namespace DbCore.DbShare
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsFormatKonfigTrup
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colFormatKonfigTrupi : System.Collections.Generic.List<clsFormatKonfigTrup>
    {
        #region Konstruktoret

        /// <summary>
        /// kthen objektin <see cref="clsFormatKonfigTrup"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsFormatKonfigTrup this[int index]
        {
            get { return ((clsFormatKonfigTrup)base[index]); }
        }

        public colFormatKonfigTrupi()
        {
        }

        public colFormatKonfigTrupi(int idKoka)
        {
            clsDatabaseShare data = new clsDatabaseShare();
            mbushKonfigurimFormateshTrupi(data.ktheFormatTrupiSipasIdKoka(idKoka));
            data.Dispose();
        }

        #endregion

        #region Metoda Publike

        public static DataTable merrFormatTrupiSipasIdKoka(int idKoka)
        {
            clsDatabaseShare data = new clsDatabaseShare();
            DataTable table = data.ktheFormatTrupiSipasIdKoka(idKoka);
            data.Dispose();
            return table;
        }

        public bool mbushFormatTrupiSipasIdKoka(int idKoka)
        {
            clsDatabaseShare data = new clsDatabaseShare();
            bool sukses = mbushKonfigurimFormateshTrupi(data.ktheFormatTrupiSipasIdKoka(idKoka));
            data.Dispose();
            return sukses;
        }

        public clsFormatKonfigTrup merrFormatSipasMonedhes(int idMon)
        {            
            foreach (clsFormatKonfigTrup formati in this)
            {
                if (formati.IdMonedha == idMon)
                    return formati;                
            }
            return null; //nuk gjendet formati per kete monedhe
        }

        #endregion

        #region Metoda Private

        private bool mbushKonfigurimFormateshTrupi(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsFormatKonfigTrup konfig = new clsFormatKonfigTrup();
                    //konfig.mbushFormatKonfigTrupi(rreshti);
                    Add(new clsFormatKonfigTrup(rreshti));
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