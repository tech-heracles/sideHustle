using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;

namespace DbCore.DbAdmin
{
    public class colLlojPeriudhe : System.Collections.Generic.List<clsLlojPeriudhe>
    {
        enum Periudha
        {
            Ditore = 0,
            Mujore = 1,
            Vjetore = 2,
            PaLimit = 3
        }

        #region Metoda Publike

        public new clsLlojPeriudhe this[int index]
        {
            get { return ((clsLlojPeriudhe)base[index]); }
        }

        public bool shtoLlojPeriudhe(clsLlojPeriudhe llojperiudhe)
        {
            base.Add(llojperiudhe);
            if (base.Contains(llojperiudhe))
                return true;
            else return false;
        }

        public bool fshiLlojPeriudhe(clsLlojPeriudhe llojperiudhe)
        {
            base.Remove(llojperiudhe);
            if (base.Contains(llojperiudhe))
                return false;
            else return true;
        }

        public bool fshiGjitheLlojPeriudhe()
        {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }

        public void fshiKeteLlojPeriudhe(int index)
        {
            base.RemoveAt(index);
        }

        public void shtoLlojPeriudheNeIndeksin(int index, clsLlojPeriudhe llojperiudhe)
        {
            base.Insert(index, llojperiudhe);
        }

        public int indeksiLlojPeriudhe(clsLlojPeriudhe llojperiudhe)
        {
            return base.IndexOf(llojperiudhe);
        }

        public bool ekzistonLlojKodi(clsLlojPeriudhe llojperiudhe)
        {
            if (base.Contains(llojperiudhe))
                return true;
            else return false;
        }

        public int numriLlojiPeridhave()
        {
            return base.Count;
        }

        public colLlojPeriudhe merrGjithLlojPeridhe()
        {
            colLlojPeriudhe dbAdmin = new colLlojPeriudhe();
            dbAdmin.mbushGjitheLlojPeriudhash();
            return dbAdmin;
        }

        public colLlojPeriudhe merrGjithLlojPeridhepozitive()
        {
            colLlojPeriudhe dbAdmin = new colLlojPeriudhe();
            dbAdmin.mbushGjitheLlojPeriudhashPozitive();
            return dbAdmin;
        }

        public bool mbushGjitheLlojPeriudhash()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool sukses = mbushLlojePeriudhash(data.ktheGjitheLlojPeriudhash());
            data.Dispose();
            return sukses;
        }

        public bool mbushGjitheLlojPeriudhashPozitive()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool sukses = mbushLlojePeriudhash(data.ktheGjitheLlojPeriudhashPozitive());
            data.Dispose();
            return sukses;
        }

        public bool mbushLlojPeriudhashSipasSuperKat(int idSuperkat)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool sukses = mbushLlojePeriudhash(data.ktheGjitheLlojPeriudhashSipasSuperKategorise(idSuperkat));
            data.Dispose();
            return sukses;
        }
        public bool mbushLlojPeriudhashSipasIdKatNrAuto(int idkatnrauto)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool sukses = mbushLlojePeriudhash(data.ktheGjitheLlojPeriudhashSipasIdKatNrAuto(idkatnrauto));
            data.Dispose();
            return sukses;
        }
        public colLlojPeriudhe merrLlojPeriudhashSipasSuperKat(int idSuperkat)
        {
            colLlojPeriudhe colPeriudhat = new colLlojPeriudhe();
            colPeriudhat.mbushLlojPeriudhashSipasSuperKat(idSuperkat);
            return colPeriudhat;
        }
        public colLlojPeriudhe merrLlojPeriudhashSipasIdKatNrAuto(int idkatnrauto)
        {
            colLlojPeriudhe colPeriudhat = new colLlojPeriudhe();
            colPeriudhat.mbushLlojPeriudhashSipasIdKatNrAuto(idkatnrauto);
            return colPeriudhat;
        }

        public static object KtheTeDhenaLlojPeriudheNew(int kategoria, int selectedIdLlojPeriudhe)
        {
            DbCore.DbAdmin.colLlojPeriudhe colLlojPeriudhe = new DbCore.DbAdmin.colLlojPeriudhe();
            colLlojPeriudhe = colLlojPeriudhe.merrLlojPeriudhashSipasIdKatNrAuto(kategoria);
            
            object[] colLlojPeriudhenew = new object[colLlojPeriudhe.Count];
            for (int i = 0; i < colLlojPeriudhe.Count; i++)
            {
                colLlojPeriudhenew[i] = new { IdLlojPeriudhe = colLlojPeriudhe[i].IdLlojPeriudhe, LlojPeriudhePershkrimi = colLlojPeriudhe[i].LlojPeriudhePershkrimi };
            }
            return new { colPeriudha = colLlojPeriudhenew, selectedIdLlojPeriudhe = selectedIdLlojPeriudhe };
        }

        #endregion

        #region Metoda Private

        private bool mbushLlojePeriudhash(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsLlojPeriudhe oLlojPeriudhe = new clsLlojPeriudhe();
                    //oLlojPeriudhe.mbushLlojPeriudhe(rreshti);
                    Add(new clsLlojPeriudhe(rreshti));
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }

        #endregion
        [Obsolete("Perdor: bool mbushLlojePeriudhash(DataTable dt)", true)]
        public colLlojPeriudhe mbushArrayListLlojiPeriudhe(DataSet ds)
        {
            colLlojPeriudhe colLlojiPeriudhe = new colLlojPeriudhe();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsLlojPeriudhe oLlojPeriudhe = new clsLlojPeriudhe();

                oLlojPeriudhe.IdLlojPeriudhe = int.Parse(rreshti[0].ToString());
                oLlojPeriudhe.LlojPeriudheLloji = int.Parse(rreshti[1].ToString());
                oLlojPeriudhe.LlojPeriudheSasia = int.Parse(rreshti[2].ToString());
                if (oLlojPeriudhe.IdLlojPeriudhe == -3)
                {
                    oLlojPeriudhe.LlojPeriudhePershkrimi = "(...)";
                }
                else
                    if (oLlojPeriudhe.IdLlojPeriudhe == 4)
                {
                    oLlojPeriudhe.LlojPeriudhePershkrimi = "Pa Limit";
                }
                    else
                {
                    oLlojPeriudhe.LlojPeriudhePershkrimi = Enum.GetName(typeof(Periudha), int.Parse(rreshti[1].ToString()));
                }
                
                colLlojiPeriudhe.Add(oLlojPeriudhe);
            }
            return colLlojiPeriudhe;
        }
    }
}
