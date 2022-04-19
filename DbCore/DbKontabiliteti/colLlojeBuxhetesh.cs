using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;

namespace DbCore.DbKontabiliteti
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsLlojBuxheti
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colLlojeBuxhetesh : System.Collections.Generic.List<clsLlojBuxheti>
    {
        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbKontabiliteti.clsLlojBuxheti"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsLlojBuxheti this[int index]
        {
            get { return ((clsLlojBuxheti)base[index]); }
        }

        /// <summary>
        /// mbush gjithe llojet e buxheteve
        /// </summary>
        /// <returns>kthen true nese mbushja kryhet me sukses ne te kundert false</returns>
        public bool mbushGjitheLlojeBuxhetesh()
        {
            clsDatabaseKontabilitet dbLlojBuxheti = new clsDatabaseKontabilitet();
            bool sukses = mbushLlojeBuxhetesh(dbLlojBuxheti.ktheGjitheLlojeBuxhetesh());
            dbLlojBuxheti.Dispose();
            return sukses;
        }

        /// <summary>
        /// mbush llojet e buxheteve qe jane per autorizim
        /// </summary>
        /// <returns>kthen true nese mbushja kryhet me sukses ne te kundert false</returns>
        public bool mbushLlojeBuxheteshPerAutorizim()
        {
            clsDatabaseKontabilitet dbLlojBuxheti = new clsDatabaseKontabilitet();
            bool sukses = mbushLlojeBuxhetesh(dbLlojBuxheti.ktheLlojeBuxheteshPerAutorizim());
            dbLlojBuxheti.Dispose();
            return sukses;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbKontabiliteti.clsLlojBuxheti"/> 
        /// </summary>
        private bool mbushLlojeBuxhetesh(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsLlojBuxheti llojBuxheti = new clsLlojBuxheti();
                    //llojBuxheti.mbushLlojBuxheti(rreshti);
                    Add(new clsLlojBuxheti(rreshti));
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
        [Obsolete("Perdor: bool mbushLlojeBuxhetesh(DataTable dt)", true)]
        public colLlojeBuxhetesh mbushArrayListLlojeBuxhetesh(DataSet ds)
        {
            colLlojeBuxhetesh llojeBuxhetesh = new colLlojeBuxhetesh();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsLlojBuxheti llojBuxheti = new clsLlojBuxheti();
                llojBuxheti.IdLlojBuxheti = Convert.ToInt32(rreshti[0]);
                llojBuxheti.KodLlojBuxheti =rreshti[1].ToString();
                llojBuxheti.PerAutorizim = Convert.ToBoolean(rreshti[2]);
                llojeBuxhetesh.Add(llojBuxheti);
            }
            return llojeBuxhetesh;
        }
    }
}
