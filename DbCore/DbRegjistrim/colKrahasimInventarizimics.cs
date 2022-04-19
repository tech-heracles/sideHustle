using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.DbRegjistrim
{
    public class colKrahasimInventarizimics : System.Collections.Generic.List<clsKrahasimInventarizimi>
    {

        #region Konstruktor

        /// <summary>
        /// 
        /// </summary>
        public colKrahasimInventarizimics()
        {
        }






        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbRegjistrim.clsKokaInventarizim"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary> 
        public new clsKrahasimInventarizimi this[int index]
        {
            get { return ((clsKrahasimInventarizimi)base[index]); }
        }


        #endregion

        #region Metoda Private

        ///// <summary>
        ///// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        /////  <see cref="DbCore.DbRegjistrim.clsKokaInventarizim"/> 
        ///// </summary>
        //private bool mbushKokaInventarizm(DataTable dt)
        //{
        //    //try
        //    //{

        //        foreach (DataRow rreshti in dt.Rows)
        //        {
        //            //clsKokaInventarizim koka = new clsKokaInventarizim();
        //            //koka.mbushKokaMagazina(rreshti, db);
        //            Add(new clsKokaInventarizim(rreshti));
        //        }

        //    //}
        //    //catch (Exception)
        //    //{
        //    //    return false;
        //    //    //throw;
        //    //}
        //    return true;
        //}

        #endregion


    }
}