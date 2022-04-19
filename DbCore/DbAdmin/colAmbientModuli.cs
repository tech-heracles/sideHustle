using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbAdmin
{
    /// <summary>
    /// colection-i i AmbientModulit
    /// </summary>
    class colAmbientModuli : List<clsAmbientModuli>
    {
        
        #region Konstruktoret

        /// <summary>
        /// Konstruktor Bosh qe mbush colectionin me te dhena nga db-ja 
        /// </summary>
        public colAmbientModuli()
        {
            
        }

        #endregion
    
        #region Metodat publike

        /// <summary>
        /// mbush ambientet nga databaza
        /// </summary>
        public void mbushAmbientet()
        { 
            clsDatabaseAdmin admin = new clsDatabaseAdmin();
            mbushAmbientet(admin.merrAmbientModulet());
            admin.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public new clsAmbientModuli this[int index]
        {
            get { return ((clsAmbientModuli)base[index]); }
        }

        /// <summary>
        /// Kjo metode kthen nje colection vetem me ambientet e modulit idModuli
        /// </summary>
        /// <param name="idModuli">id-ja e modulit per te filtruar</param>
        /// <returns>colection i Ambienteve filtruar per modul</returns>
        public colAmbientModuli getAmbientet(int idModuli)
        {
            colAmbientModuli ambientet = new colAmbientModuli();
            foreach (clsAmbientModuli ambient in this)
            {
                if (ambient.IdModuli == idModuli)
                    ambientet.Add(ambient);
            }
            return ambientet;
        }

        #endregion

        #region Metodat private

        /// <summary>
        /// merr te dhenat nga nje data table i dhene dhe krijon colectionin
        /// </summary>
        /// <param name="dt">datatable me te dhenat</param>
        /// <returns>True nese mbushja e colectionit behet me sukses, False perndryshe</returns>
        private bool mbushAmbientet(DataTable dt)
        {            
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    clsAmbientModuli ambienti = new clsAmbientModuli(Convert.ToInt32(rreshti["IDAMBJMODULI"]), Convert.ToString(rreshti["AMBJKODI"]),
                        Convert.ToString(rreshti["AMBJPERSHKRIMI"]), Convert.ToInt32(rreshti["IDMODULI"]));
                    this.Add(ambienti);
                }
            //}
            //catch (Exception)
            //{
            //    return false;
            //    //throw;
            //}
            return true;
        }

        /// <summary>
        /// krijon nje colection te ri me te dhenat e marra nga nje datatable i dhene
        /// </summary>
        /// <param name="dt">datatable me te dhenat e colection-it</param>
        /// <returns>colectioni i mbushur nga datatable-i i dhene </returns>
        /// <seealso cref="mbushAmbientet"/>
        private colAmbientModuli konvAmbientet(DataTable dt)
        {
            colAmbientModuli ambientet = new colAmbientModuli();
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    clsAmbientModuli ambienti = new clsAmbientModuli(Convert.ToInt32(rreshti["IDAMBJMODULI"]), Convert.ToString(rreshti["AMBJKODI"]),
                        Convert.ToString(rreshti["AMBJPERSHKRIMI"]), Convert.ToInt32(rreshti["IDMODULI"]));
                    ambientet.Add(ambienti);
                }
            //}
            //catch (Exception)
            //{
            //    return ambientet;
            //    //throw;
            //}
            return ambientet;
        }
    
        #endregion

    }
}
