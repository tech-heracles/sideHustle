using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;

namespace DbCore.DbAdmin
{
    public class colModulet : System.Collections.Generic.List<clsModuli>
    {
        #region Metoda Publike

        public colModulet()
        { 

        }

        public colModulet(int idGjuha)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
                mbushModulet(idGjuha, data.merrModulet());
        }

        public new clsModuli this[int index]
        {
            get { return ((clsModuli)base[index]); }
        }

        public bool shtoModul(clsModuli mod)
        {
            base.Add(mod);
            if (base.Contains(mod))
                return true;
            else return false;
        }

        public bool fshiModul(clsModuli mod)
        {
            base.Remove(mod);
            if (base.Contains(mod))
                return false;
            else return true;
        }

        public bool fshiGjitheModulet()
        {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }

        public void fshiKeteModul(int index)
        {
            base.RemoveAt(index);
        }

        public void shtoModulNeIndeksin(int index, clsModuli mod)
        {
            base.Insert(index, mod);
        }

        public int indeksiModulit(clsModuli mod)
        {
            return base.IndexOf(mod);
        }

        public bool ekzistonModuli(clsModuli mod)
        {
            if (base.Contains(mod))
                return true;
            else return false;
        }

        public int numriModuleve()
        {
            return base.Count;
        }

        public void mbushModulet(int  idGjuha) 
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            mbushModulet(idGjuha, data.merrModulet());
            data.Dispose();
        }

        public void mbushModulet(int idGjuha, clsDatabaseAdmin data)
        {
            if(data == null)
                data = new clsDatabaseAdmin();
            mbushModulet(idGjuha, data.merrModulet());
        }

        public bool mbushGjitheModulet(int  idGjuha)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool sukses = mbushModulet(idGjuha, data.ktheGjitheModulet());
            data.Dispose();
            return sukses;
        }

        public bool mbushGjitheModuleteRaporteve(int  idGjuha)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool sukses = mbushModulet(idGjuha, data.ktheGjitheModuleteRaporteve());
            data.Dispose();
            return sukses;
        }

        #endregion

        #region Metoda Private

        private bool mbushModulet(int  idGjuha,   DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    clsModuli mod = new clsModuli(idGjuha);
                    mod.mbushModul(idGjuha, rreshti);
                    this.Add(mod);
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
        //[Obsolete("Perdor: bool mbushModulet(DataTable dt)", true)]
        //public colModulet mbushArrayListModulet(DataSet ds)
        //{
        //    colModulet modulet = new colModulet();
        //    foreach (DataRow rreshti in ds.Tables[0].Rows)
        //    {
        //        clsModuli mod = new clsModuli(idGjuha);

        //        mod.IdModuli = int.Parse(rreshti[0].ToString());
        //        mod.KodiModuli = rreshti[1].ToString();
        //        mod.PershkrimiModuli = rreshti[2].ToString();

        //        modulet.Add(mod);
        //    }
        //    return modulet;
        //}
    }
}
