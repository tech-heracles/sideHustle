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
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsSkemaKontabelNew
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colSkemaKontabelNew : System.Collections.Generic.List<clsSkemaKontabelNew>
    {

        #region Konstruktor

        /// <summary>
        /// konstruktor pa parametra
        /// </summary>
        public colSkemaKontabelNew()
        {
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idNdermVit">id qe lidh ndermarrjen me vitin</param>
        public colSkemaKontabelNew(int idNdermVit)
        {
            clsDatabaseKontabilitet dbSkemaKontNew = new clsDatabaseKontabilitet();
            mbushSkematKontNew(dbSkemaKontNew.ktheGjitheSkemaKontabelRegjistrim());
            dbSkemaKontNew.Dispose();
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbKontabiliteti.clsSkemaKontabelNew"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsSkemaKontabelNew this[int index]
        {
            get { return ((clsSkemaKontabelNew)base[index]); }
        }

        ///// <summary>
        ///// Metoda kthen nje collection me objekte te tipit <see cref="DbCore.DbKontabiliteti.clsSkemaKontabelNew"/> 
        ///// duke filtruar sipas ID-se se skemes kontabel
        ///// Thirret funksioni <see cref="DbCore.DbKontabiliteti.clsDatabaseKontabilitet.ktheSkemaKontabelNewSipasID"/>
        ///// </summary>
        //[Obsolete("Perdor: clsSkemaKontabelNew ktheSkemeKontabelNewSipasID(int id)", true)]
        //public colSkemaKontabelNew merrSkemeKontabelNewSipasID(int id)
        //{
        //    clsDatabaseKontabilitet data = new clsDatabaseKontabilitet();
        //    return data.merrSkemaKontabelNewSipasID(id);
        //    //clsSkemaKontabelNew data = new clsSkemaKontabelNew(id);
        //    //return data;
        //}

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbKontabiliteti.clsSkemaKontabelNew"/> 
        /// </summary>
        private bool mbushSkematKontNew(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsSkemaKontabelNew skema = new clsSkemaKontabelNew();
                    //skema.mbushSkemaKontNew(rreshti);
                    Add(new clsSkemaKontabelNew(rreshti));
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
        //[Obsolete("Perdor: bool mbushSkematKontNew(DataTable dt)", true)]
        //public colSkemaKontabelNew mbushArrayListSkemeKontabelNew(DataSet ds)
        //{// metoda per te mbushur nje arraylist me llogari nga nje dataset
        //    colSkemaKontabelNew skemakontabel = new colSkemaKontabelNew();
        //    foreach (DataRow rreshti in ds.Tables[0].Rows)
        //    {
        //        clsSkemaKontabelNew skema = new clsSkemaKontabelNew();
        //        skema.IdSkemeKont = rreshti[0].ToString();
        //        skema.KodSkemeKont = rreshti[1].ToString();
        //        skema.PershkrimSkemeKont = rreshti[2].ToString();
        //        skema.IdLlojDok = rreshti[3].ToString();
        //        skema.OColSkemakontabelTrupiNew = new colSkemaKontabelTrupiNew();
        //        skema.OColSkemakontabelTrupiNew  =skema.OColSkemakontabelTrupiNew.merrSkemaTrupiNew(skema.IdSkemeKont);
        //        skemakontabel.Add(skema);
        //    }
        //    return skemakontabel;
        //}
       
    }
}
