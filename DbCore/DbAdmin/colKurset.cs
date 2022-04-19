using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbAdmin
{
    public class colKurset : System.Collections.Generic.List<clsKurset>
    {
        public colKurset() {
        }
        public colKurset(int idNdermarrje, DateTime data,  clsDatabaseAdmin db) {
            mbushKurset(db.ktheGjitheKurset(idNdermarrje, data));
        }
        #region Metoda Publike

        public new clsKurset this[int index]
        {
            get { return ((clsKurset)base[index]); }
        }

        public bool mbushKursetFunditMonedhes(int id)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool sukses = mbushKurset(data.ktheKursetFunditMonedhes(id));
            data.Dispose();
            return sukses;
        }
        public bool mbushKursetFunditMonedhesSipasLlojit(int id, int lloji)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool sukses = mbushKursetFunditMonedhesSipasLlojit(id, lloji, data);
            data.Dispose();
            return sukses;
        }
        public bool mbushKursetFunditMonedhesSipasLlojit(int id, int lloji, clsDatabaseAdmin data)
        {           
            return mbushKurset(data.ktheKursetFunditMonedhesSipasLlojit(id, lloji));
        }

        public bool mbushKursetMonedhes(int id)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool sukses = mbushKurset(data.ktheKursetMonedhes(id));
            data.Dispose();
            return sukses;
        }

        public static DataTable ktheKursetMonedhes(int idMonedha)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            DataTable dt = data.ktheKursetMonedhesDt(idMonedha);
            dt.Dispose();
            return dt;
        }

        public static DataTable ktheGjitheKursetMonedhesDheLlojit(int idMonedha, int llojKursi)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            DataTable dt = data.ktheKursetMonedhesSipasLlojit(idMonedha, llojKursi);
            dt.Dispose();
            return dt;
        }

        #endregion

        #region Metoda Private

        private bool mbushKurset(DataTable dt)
        {
                foreach (DataRow rreshti in dt.Rows)
                {
                    Add(new clsKurset(rreshti));
                }
            return true;
        }

        #endregion
        
    }
}