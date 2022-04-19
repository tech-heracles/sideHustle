using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;

namespace DbCore.DbAdmin
{
    public class colLlojKodi : System.Collections.Generic.List<clsLlojKodi>
    {
        #region Metoda Publike

        public new clsLlojKodi this[int index]
        {
            get { return ((clsLlojKodi)base[index]); }
        }

        public bool shtoLlojKodi(clsLlojKodi llojkodi)
        {
            base.Add(llojkodi);
            if (base.Contains(llojkodi))
                return true;
            else return false;
        }

        public bool fshiLlojKodi(clsLlojKodi llojkodi)
        {
            base.Remove(llojkodi);
            if (base.Contains(llojkodi))
                return false;
            else return true;
        }

        public bool fshiGjitheLlojKodi()
        {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }

        public void fshiKeteLlojKodi(int index)
        {
            base.RemoveAt(index);
        }

        public void shtoLlojKodiNeIndeksin(int index, clsLlojKodi llojkodi)
        {
            base.Insert(index, llojkodi);
        }

        public int indeksiLlojKodit(clsLlojKodi llojkodi)
        {
            return base.IndexOf(llojkodi);
        }

        public bool ekzistonLlojKodi(clsLlojKodi llojkodi)
        {
            if (base.Contains(llojkodi))
                return true;
            else return false;
        }

        public int numriLlojiKodeve()
        {
            return base.Count;
        }

        public bool mbushGjithellojKodi()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool sukses = mbushLlojetKodeve(data.ktheGjithellojKodi());
            data.Dispose();
            return sukses;
        }

        public bool mbushGjithellojKodiPozitive()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool sukses = mbushLlojetKodeve(data.ktheGjithellojKodiPozitive());
            data.Dispose();
            return sukses;
        }

        #endregion

        #region Metoda Private

        private bool mbushLlojetKodeve(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsLlojKodi oLojKodi = new clsLlojKodi();
                    //oLojKodi.mbushLlojKodi(rreshti);
                    Add(new clsLlojKodi(rreshti));
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }

        #endregion
        [Obsolete("Perdor: bool mbushLlojetKodeve(DataTable dt)", true)]
        public colLlojKodi mbushArrayListLlojiKodeve(DataSet ds)
        {
            colLlojKodi colLlojiKodet = new colLlojKodi();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsLlojKodi oLojKodi = new clsLlojKodi();

                oLojKodi.IdLlojKodi = int.Parse(rreshti[0].ToString());
                oLojKodi.LlojKodiPershkrimi = rreshti[1].ToString();

                colLlojiKodet.Add(oLojKodi);
            }
            return colLlojiKodet;
        }
    }
}
