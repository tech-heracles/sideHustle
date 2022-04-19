using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbAdmin
{
    public class colKategoriNrAuto : System.Collections.Generic.List<clsKategoriNrAuto>
    {
        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbRegjistrim.clsKategoriNivelDok"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary> 
        public new clsKategoriNrAuto this[int index]
        {
            get { return ((clsKategoriNrAuto)base[index]); }
        }

        /// <summary>
        /// mbush gjithe kategorite e niveleve te dokumentit
        /// </summary>
        /// <param name="idNdermVit">id qe lidh ndermarrjen me vitin</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushGjitheKategoriNrAuto()
        {
            clsDatabaseAdmin dbKatNivelDok = new clsDatabaseAdmin();
            bool mbush = mbushKategori(dbKatNivelDok.ktheGjitheKategori());
            dbKatNivelDok.Dispose();
            return mbush;
        }
     
        #endregion

        #region Metoda Private


        private bool mbushKategori(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsKategoriNrAuto kategori = new clsKategoriNrAuto();
                    //kategori.mbushKat(rreshti);
                    Add(new clsKategoriNrAuto(rreshti));
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

