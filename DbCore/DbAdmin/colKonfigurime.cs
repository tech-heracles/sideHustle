using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;

namespace DbCore.DbAdmin
{

    public class colKonfigurime : System.Collections.Generic.List<clsKonfigurime>
    {
        #region Metoda Publike

        public new clsKonfigurime this[int index]
        {
            get { return ((clsKonfigurime)base[index]); }
        }

        public bool shtoKonfigurim(clsKonfigurime konfig)
        {
            base.Add(konfig);
            if (base.Contains(konfig))
                return true;
            else return false;
        }

        public bool fshiKonfigurim(clsKonfigurime konfig)
        {
            base.Remove(konfig);
            if (base.Contains(konfig))
                return false;
            else return true;
        }

        public bool fshiGjitheKonfigurimet()
        {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }

        public void fshiKeteKonfigurim(int index)
        {
            base.RemoveAt(index);
        }

        public void shtoKonfigurimNeIndeksin(int index, clsKonfigurime konfig)
        {
            base.Insert(index, konfig);
        }

        public int indeksiKonfigurim(clsKonfigurime konfig)
        {
            return base.IndexOf(konfig);
        }

        public bool ekzistonKonfigurimi(clsKonfigurime konfig)
        {
            if (base.Contains(konfig))
                return true;
            else return false;
        }

        public bool mbushGjitheKonfigurimet()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool sukses = mbushListaKonfigurime(data.ktheGjitheKonfigurimet());
            data.Dispose();
            return sukses;
        }
        #endregion

        #region Metoda Private

        private bool mbushListaKonfigurime(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsKonfigurime konfig = new clsKonfigurime();
                    //konfig.mbushKonfigurime(rreshti);
                    Add(new clsKonfigurime(rreshti));
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }

        #endregion
        [Obsolete("Perdor: bool mbushListKonfigurime(DataTable dt)", true)]
        public colKonfigurime mbushArrayListKonfigurime(DataSet ds)
        {
            colKonfigurime konfigurimet = new colKonfigurime();

            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsKonfigurime konfig = new clsKonfigurime();
                konfig.IdNdermarje = int.Parse(rreshti[0].ToString());
                konfig.EmriNdermarje = rreshti[1].ToString();
                konfig.RaportPath = rreshti[2].ToString();
                konfig.Perdorues = rreshti[3].ToString();
                konfig.Password = rreshti[4].ToString();
                konfig.RaportDomain = rreshti[5].ToString();
                konfig.RaportFolder = rreshti[6].ToString();
                //konfig.MultipleUser = Convert.ToBoolean(rreshti[7].ToString());
                konfigurimet.Add(konfig);
            }
            return konfigurimet;
        }

    }
}
