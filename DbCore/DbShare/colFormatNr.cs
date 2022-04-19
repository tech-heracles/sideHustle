using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbShare
{
    public class colFormatNr : System.Collections.Generic.List<clsFormatNr>
    {
        #region Metoda Publike

        public new clsFormatNr this[int index]
        {
            get { return ((clsFormatNr)base[index]); }
        }

        public bool mbushFormatNr()
        {
            clsDatabaseShare data = new clsDatabaseShare();
            bool mbush = mbushFormatetNumra(data.ktheFormatNr());
            data.Dispose();
            return mbush;
        }

        #endregion

        #region Metoda Private

        private bool mbushFormatetNumra(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsFormatNr koka = new clsFormatNr();
                    //koka.mbushFormatNr(rreshti);
                    Add(new clsFormatNr(rreshti));
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }

        #endregion
        [Obsolete("Perdor: bool mbushFormatetNumra(DataTable dt)", true)]
        public colFormatNr mbushArrayListFormatNumrash(DataSet ds)
        {
            colFormatNr col = new colFormatNr();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsFormatNr koka = new clsFormatNr();

                koka.IdFormatNr = int.Parse(rreshti[0].ToString());
                koka.KodFormati = rreshti[1].ToString();
                koka.VlereFormati = rreshti[2].ToString();                
                col.Add(koka);
            }
            return col;
        }
    }
}
