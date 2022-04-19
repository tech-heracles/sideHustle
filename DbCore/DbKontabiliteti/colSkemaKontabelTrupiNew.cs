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
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsSkemaKontabelTrupiNew
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colSkemaKontabelTrupiNew : System.Collections.Generic.List<clsSkemaKontabelTrupiNew>
    {
        #region Konstruktoret

        /// <summary>
        /// konstruktore pa parametra
        /// </summary>
        public colSkemaKontabelTrupiNew()
        {
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="id">id e skemes se kontabilitetit</param>
        public colSkemaKontabelTrupiNew(string id)
        {
            clsDatabaseKontabilitet dbSkemaKontTrupNew = new clsDatabaseKontabilitet();
            mbushSkematTrupiNew(dbSkemaKontTrupNew.ktheSkemaTrupiNewSipasID(id));
            dbSkemaKontTrupNew.Dispose();
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbKontabiliteti.clsSkemaKontabelTrupiNew"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>       
        public new clsSkemaKontabelTrupiNew this[int index]
        {
            get { return ((clsSkemaKontabelTrupiNew)base[index]); }
        }

        /// <summary>
        /// Metoda kthen nje collection me objekte te tipit <see cref="DbCore.DbKontabiliteti.clsSkemaKontabelTrupiNew"/> 
        /// duke filtruar sipas ID-se se skemes kontabel
        /// Thirret funksioni <see cref="DbCore.DbKontabiliteti.clsDatabaseKontabilitet.ktheSkemaTrupiNewSipasID"/>
        /// </summary>
        public colSkemaKontabelTrupiNew merrSkemaTrupiNew(string id)
        {
            //clsDatabaseKontabilitet data = new clsDatabaseKontabilitet();
            //return data.merrSkemaTrupiNewSipasID(id);
            colSkemaKontabelTrupiNew data = new colSkemaKontabelTrupiNew(id);
            return data;
        }

        #endregion

        #region Metoda Private
        
        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbKontabiliteti.clsSkemaKontabelTrupiNew"/> 
        /// </summary> 
        private bool mbushSkematTrupiNew(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsSkemaKontabelTrupiNew skematrupi = new clsSkemaKontabelTrupiNew();
                    //skematrupi.mbushSkemKontTrupiNew(rreshti);
                    Add(new clsSkemaKontabelTrupiNew(rreshti));
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
        [Obsolete("Perdor: bool mbushSkematTrupiNew(DataTable dt)", true)]
        public colSkemaKontabelTrupiNew mbushArrayListSkemaTrupiNew(DataSet ds)
        {// metoda per te mbushur nje arraylist me llogari nga nje dataset
            colSkemaKontabelTrupiNew skematrupinew = new colSkemaKontabelTrupiNew();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsSkemaKontabelTrupiNew skematrupi = new clsSkemaKontabelTrupiNew();                
                skematrupi.DebikrediSkemeKontTrupi =Convert.ToInt16(rreshti[0].ToString());
                skematrupi.FormulaSkemeKontTrupi = rreshti[1].ToString();
                skematrupi.IdLlogarieFikse = rreshti[2].ToString();
                skematrupi.IdLlojLlogarise = rreshti[3].ToString();
                skematrupi.IdNenLlojLlogarie = rreshti[4].ToString();
                skematrupi.IdSkemeKontTrupi = rreshti[5].ToString();
                skematrupi.KodSkemeKontTrupi = rreshti[6].ToString();
                skematrupi.KushtiSkemeKontTrupi =Convert.ToInt16(rreshti[7].ToString());
                skematrupi.PershkrimSkemeKontTrupi = rreshti[8].ToString();
                skematrupi.IdSkemeKont = rreshti[9].ToString();
                skematrupi.IndeksGrupimi = Convert.ToInt16(rreshti[10].ToString());
                skematrupinew.Add(skematrupi);
            }
            return skematrupinew;
        }
    }
}
