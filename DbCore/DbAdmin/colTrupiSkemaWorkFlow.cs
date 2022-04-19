using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;

namespace DbCore.DbAdmin
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsTrupiSkemaWorkFlow
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colTrupiSkemaWorkFlow : System.Collections.Generic.List<clsTrupiSkemaWorkFlow>
    {

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="clsTrupiSkemaWorkFlow"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsTrupiSkemaWorkFlow this[int index]
        {
            get { return ((clsTrupiSkemaWorkFlow)base[index]); }
        }
   

        /// <summary>
        /// mbush te gjithe trupat sipas id se kokes
        /// </summary>
        /// <param name="idkoka">id e kokes se skemes</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushTrupatSkemeWorkFlowSipasKokes(int idkoka)
        {
            using (clsDatabaseAdmin db = new clsDatabaseAdmin())
            {
                bool sukses = mbushTrupatSkemaWorkFlow(db.ktheTrupiSkemaWorkFlowSipasKokes(idkoka));
                return sukses;
            }
        } public bool mbushTrupatSkemeWorkFlowSipasKokes(int idkoka,clsDatabaseAdmin db)
        {
           
            bool sukses = mbushTrupatSkemaWorkFlow(db.ktheTrupiSkemaWorkFlowSipasKokes(idkoka));
         
            return sukses;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="clsTrupiSkemaWorkFlow"/> 
        /// </summary>
        private bool mbushTrupatSkemaWorkFlow(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsTrupiSkemaWorkFlow skema = new clsTrupiSkemaWorkFlow();
                    //skema.mbushTrupiSkeme(rreshti);
                    this.Add(new clsTrupiSkemaWorkFlow(rreshti));
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
