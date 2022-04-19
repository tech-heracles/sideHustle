using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;

namespace DbCore.DbAdmin
{
    public class colQytetet : System.Collections.Generic.List<clsQyteti>
    {

        #region Metoda Publike

        public new clsQyteti this[int index]
        {
            get { return ((clsQyteti)base[index]); }
        }

        public bool shtoQytet(clsQyteti qyteti)
        {
            base.Add(qyteti);
            if (base.Contains(qyteti))
                return true;
            else return false;
        }

        public bool fshiQytet(clsQyteti qyteti)
        {
            base.Remove(qyteti);
            if (base.Contains(qyteti))
                return false;
            else return true;
        }

        public bool fshiGjitheQytetet()
        {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }

        public void fshiKeteQytet(int index)
        {
            base.RemoveAt(index);
        }

        public void shtoQytetinNeIndeksin(int index, clsQyteti qyteti)
        {
            base.Insert(index, qyteti);
        }

        public int indeksiQytetit(clsQyteti qyteti)
        {
            return base.IndexOf(qyteti);
        }

        public bool ekzistonQyteti(clsQyteti qyteti)
        {
            if (base.Contains(qyteti))
                return true;
            else return false;
        }

        public int numriQyteteve()
        {
            return base.Count;
        }

        public bool mbushGjitheQytetetPozitive(int idndermarje)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool sukses = mbushQytetet(data.ktheGjitheQytetetPozitive(idndermarje));
            data.Dispose();
            return sukses;
        }

        public static DataTable ktheQytetePerEksport(int idndermarje)
        {
            using (clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin())
            {
                return dbAdmin.ktheQytetePerEksport(idndermarje);
            }
        }



        public bool mbushQytetetNdermarrjes(int idNdermarje)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool sukses = mbushQytetet(data.ktheQytetetNdermarrjes(idNdermarje));
            data.Dispose();
            return sukses;
        }

        #endregion

        #region Metoda Private

        private bool mbushQytetet(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsQyteti qyteti = new clsQyteti();
                    //qyteti.mbushQyteti(rreshti);
                    Add(new clsQyteti(rreshti));
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }

        #endregion
        [Obsolete("Perdor: bool mbushQytetet(DataTable dt)", true)]
        public colQytetet mbushArrayListQytetet(DataSet ds)
        {
            colQytetet qytetet = new colQytetet();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsQyteti qyteti = new clsQyteti();

                qyteti.IdQyteti = int.Parse(rreshti[0].ToString());
                qyteti.KodiQyteti = rreshti[1].ToString();
                qyteti.EmriQyteti = rreshti[2].ToString();
                qyteti.IdNdermarja = int.Parse(rreshti[3].ToString());
                qyteti.IdPerdoruesi =int.Parse (rreshti[5].ToString ());
                qytetet.Add(qyteti);
            }
            return qytetet;
        }

       
    }
}
