using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;

namespace DbCore.DbAdmin
{
    public class colLlojModeleshFushaShtese : System.Collections.Generic.List<clsLlojModeliFushaShtese>
    {

        #region Metoda Publike

        public new clsLlojModeliFushaShtese this[int index]
        {
            get { return ((clsLlojModeliFushaShtese)base[index]); }
        }

        public bool shtoLlojModeliFushaShtese(clsLlojModeliFushaShtese llojModeliFushaShtese)
        {
            base.Add(llojModeliFushaShtese);
            if (base.Contains(llojModeliFushaShtese))
                return true;
            else return false;
        }

        public bool fshiLlojModeliFushaShtese(clsLlojModeliFushaShtese llojModeliFushaShtese)
        {
            base.Remove(llojModeliFushaShtese);
            if (base.Contains(llojModeliFushaShtese))
                return false;
            else return true;
        }

        public bool fshiGjitheLlojModeleshFushaShtese()
        {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }

        public void fshiKeteLlojModeliFushaShtese(int index)
        {
            base.RemoveAt(index);
        }

        public void shtoLlojModelinFushaShteseNeIndeksin(int index, clsLlojModeliFushaShtese llojModeliFushaShtese )
        {
            base.Insert(index, llojModeliFushaShtese );
        }

        public int indeksiLlojModelitFushaShtese(clsLlojModeliFushaShtese llojModeliFushaShtese)
        {
            return base.IndexOf(llojModeliFushaShtese);
        }

        public bool ekzistonLlojModeliFushaShtese(clsLlojModeliFushaShtese llojModeliFushaShtese)
        {
            if (base.Contains(llojModeliFushaShtese))
                return true;
            else return false;
        }

        public int numriLlojeveModelitFushaShtese()
        {
            return base.Count;
        }

        public bool mbushGjitheLlojModeleshFushaShtese()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool sukses = mbushLlojeModeleshFushaShtese(data.ktheGjitheLlojModeleshFushaShtese());
            data.Dispose();
            return sukses;
        }

        public bool mbushGjitheLlojModeleshFushaShtesePozitive()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool sukses = mbushLlojeModeleshFushaShtese(data.ktheGjitheLlojModeleshFushaShtesePozitive());
            data.Dispose();
            return sukses;
        }

        #endregion

        #region Metoda Private

        private bool mbushLlojeModeleshFushaShtese(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsLlojModeliFushaShtese llojModeliFushaShtese = new clsLlojModeliFushaShtese();
                    //llojModeliFushaShtese.mbushLlojModelFushaShtese(rreshti);
                    Add(new clsLlojModeliFushaShtese(rreshti));
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }

        #endregion
        [Obsolete("Perdor: bool mbushLlojeModeleshFushaShtese(DataTable dt)", true)]
        public colLlojModeleshFushaShtese mbushArrayListLlojModeleshFushaShtese(DataSet ds)
        {
            colLlojModeleshFushaShtese llojModeleshFushaShtese = new colLlojModeleshFushaShtese();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsLlojModeliFushaShtese llojModeliFushaShtese = new clsLlojModeliFushaShtese();

                llojModeliFushaShtese.IdLlojModeliFushaShtese = int.Parse(rreshti[0].ToString());
                llojModeliFushaShtese.PershkrimiLlojModeliFushaShtese = rreshti[1].ToString();


                llojModeleshFushaShtese.Add(llojModeliFushaShtese);
            }
            return llojModeleshFushaShtese;
        }

    }
}