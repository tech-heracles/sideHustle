using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;

namespace DbCore.DbAdmin
{
    public class colLlojDokumenti : System.Collections.Generic.List<clsLlojDokumenti>
    {
        #region Konstruktoret

        public colLlojDokumenti()
        {
        }

        public colLlojDokumenti(int moduli)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            mbushLlojDokumenti(data.ktheGjitheDokumentat(moduli));
            data.Dispose();
        }

        public colLlojDokumenti(string komponente)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            mbushLlojDokumenti(data.ktheGjitheDokumentatSipasKomponentes(komponente));
            data.Dispose();
        }

        #endregion

        #region Metoda Publike

        public new clsLlojDokumenti this[int index]
        {
            get { return ((clsLlojDokumenti)base[index]); }
        }

        public bool shtoLlojDokumenti(clsLlojDokumenti llojdok)
        {
            base.Add(llojdok);
            if (base.Contains(llojdok))
                return true;
            else return false;
        }

        public bool fshiLlojDokumenti(clsLlojDokumenti llojdok)
        {
            base.Remove(llojdok);
            if (base.Contains(llojdok))
                return false;
            else return true;
        }

        public bool fshiGjitheLlojDokumenti()
        {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }

        public void fshiKeteLlojDokumenti(int index)
        {
            base.RemoveAt(index);
        }

        public void shtoLlojDokumentiNeIndeksin(int index, clsLlojDokumenti llojdok)
        {
            base.Insert(index, llojdok);
        }

        public int indeksiLlojKodit(clsLlojDokumenti llojdok)
        {
            return base.IndexOf(llojdok);
        }

        public bool ekzistonLlojDok(clsLlojDokumenti llojdok)
        {
            if (base.Contains(llojdok))
                return true;
            else return false;
        }

        public int numriLlojiDokumenta()
        {
            return base.Count;
        }

        public bool mbushGjitheLlojetDokumentave()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool sukses = mbushLlojDokumenti(data.ktheGjitheLlojetDokumentave());
            data.Dispose();
            return sukses;
        }

        #endregion

        #region Metoda Private

        private bool mbushLlojDokumenti(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsLlojDokumenti oLlojDok = new clsLlojDokumenti();
                    //oLlojDok.mbushLlojDokumenti(rreshti);
                    Add(new clsLlojDokumenti(rreshti));
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }

        #endregion
        [Obsolete("Perdor: bool mbushLlojDokumenti(DataTable dt)", true)]
        public colLlojDokumenti mbushArrayListLlojiDokumenta(DataSet ds)
        {
            colLlojDokumenti colLlojiDokumenta = new colLlojDokumenti();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsLlojDokumenti oLlojDok = new clsLlojDokumenti();

                oLlojDok.IdLlojDokumenti = int.Parse(rreshti[0].ToString());
                oLlojDok.LlojDokKodi = rreshti[1].ToString();
                oLlojDok.LlojDokPershkrimi = rreshti[2].ToString();
                oLlojDok.IdModuli = int.Parse(rreshti[3].ToString());
                oLlojDok.IdKomponente = int.Parse(rreshti[4].ToString());

                colLlojiDokumenta.Add(oLlojDok);
            }
            return colLlojiDokumenta;
        }
    }
}
