using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbAdmin
{
    /// <summary>
    /// 
    /// </summary>
    class colKlaseObjektesh:List<clsKlaseObjektesh>
    {
           
        #region Konstruktoret

        /// <summary>
        /// Konstruktor Bosh qe mbush coleciton-in me te dhena nga db-ja 
        /// </summary>
        public colKlaseObjektesh()
        {
            
        }

        #endregion
    
        #region Metodat publike

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public new clsKlaseObjektesh this[int index]
        {
            get { return ((clsKlaseObjektesh)base[index]); }
        }

        /// <summary>
        /// mbush klasat nga databaza
        /// </summary>
        public void mbushKlasat()
        { 
            clsDatabaseAdmin admin = new clsDatabaseAdmin();
            mbushKlasat(admin.merrKlaseObjektet());
            admin.Dispose();
        }

        /// <summary>
        /// Kjo metode kthen nje colection vetem me klasat e ambientit te modulit idAmbientModuli
        /// </summary>
        /// <param name="idAmbientModuli">id-ja e ambient modulit per te filtruar</param>
        /// <returns>colection i klasave filtruar per ambient moduli</returns>
        public colKlaseObjektesh getKlasat(int idAmbientModuli)
        {
            colKlaseObjektesh klasat = new colKlaseObjektesh();
            foreach (clsKlaseObjektesh klasa in this)
            {
                if (klasa.IdAmbientModuli == idAmbientModuli)
                    klasat.Add(klasa);
            }
            return klasat;
        }

        #endregion

        #region Metodat private

        /// <summary>
        /// merr te dhenat nga nje data table i dhene dhe krijon colectionin
        /// </summary>
        /// <param name="dt">datatable me te dhenat</param>
        /// <returns>True nese mbushja e colectionit behet me sukses, False perndryshe</returns>
        private bool mbushKlasat(DataTable dt)
        {
            
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    clsKlaseObjektesh klasa = new clsKlaseObjektesh(Convert.ToInt32(rreshti["IDKLASEOBJEKTESH"]), Convert.ToString(rreshti["PERSHKRIMKLASEOBJEKTESH"]),
                        Convert.ToInt32(rreshti["IDAMBJMODULI"]));
                    this.Add(klasa);
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
        private colKlaseObjektesh konvKlasat(DataTable dt)
        {
            colKlaseObjektesh klasat = new colKlaseObjektesh();
            try
            {
                foreach (DataRow rreshti in dt.Rows)
                {
                    clsKlaseObjektesh klasa = new clsKlaseObjektesh(Convert.ToInt32(rreshti["IDKLASEOBJEKTESH"]), Convert.ToString(rreshti["PERSHKRIMKLASEOBJEKTESH"]),
                        Convert.ToInt32(rreshti["IDAMBJMODULI"]));
                    klasat.Add(klasa);
                }
            }
            catch (Exception)
            {
                return klasat;
                //throw;
            }
            return klasat;
        }
    
        #endregion

    }
}
