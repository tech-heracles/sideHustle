using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;

namespace DbCore.DbAdmin
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne numrat e fundit automatik te perdorur per nje numer te dhene (shtim, modifikim, fshirje etj)
    ///  (Te dhenat  merren nga tabela : T_NRAUTOMATIKFUNDIT)
    /// </summary>
    public class colNrAutomatikFundit : System.Collections.Generic.List<clsNrAutomatikFundit>
    {
        #region Konstruktoret

        public colNrAutomatikFundit()
        {
        }

        public colNrAutomatikFundit(int idNrAuto)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            mbushNumratAutomatikeFundit(data.ktheGjitheNrAutomatikeFundit(idNrAuto));
            data.Dispose();
        }

        #endregion

        #region Metoda Publike

        public new clsNrAutomatikFundit this[int index]
        {
            get { return ((clsNrAutomatikFundit)base[index]); }
        }

        public bool shtoNrAutomatikeFundit(clsNrAutomatikFundit nrAutoFundit)
        {// metoda per shtimin e nje ACRNumraAutomatike ne nje arraylist
            base.Add(nrAutoFundit);
            if (base.Contains(nrAutoFundit))
                return true;
            else return false;
        }

        public bool fshiNrAutomatikeFundit(clsNrAutomatikFundit nrAutoFundit)
        {// metoda per heqjen e nje ACRNumraAutomatike ne nje arraylist
            base.Remove(nrAutoFundit);
            if (base.Contains(nrAutoFundit))
                return false;
            else return true;
        }

        public bool fshiGjitheNrAutomatikeFundit()
        {// metoda per heqjen e te gjithe ACRNumraAutomatike nga arraylist
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }

        public static DataTable merrGjithNrAutoFunditSipasNrAuto(int idNrAuto)
        {
            DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            DataTable tabela = dbAdmin.ktheGjitheNrAutomatikeFundit(idNrAuto);
            dbAdmin.Dispose();
            return tabela;
        }

        public static colNrAutomatikFundit merrNrAutoFunditSipasNrAuto(int idNrAuto, clsDatabaseAdmin dbAdmin)
        {
            colNrAutomatikFundit nrFundit = new colNrAutomatikFundit();
            nrFundit.mbushNumratAutomatikeFundit(dbAdmin.merrNumraAutomatikeFunditSipasIDNrAuto(idNrAuto));
            return nrFundit;
        }
        public static colNrAutomatikFundit merrNrAutoFunditSipasNrAuto(int idNrAuto)
        {
            clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            colNrAutomatikFundit nrFundit = new colNrAutomatikFundit();
            nrFundit.mbushNumratAutomatikeFundit(dbAdmin.merrNumraAutomatikeFunditSipasIDNrAuto(idNrAuto));
            dbAdmin.Dispose();
            return nrFundit;
        }
        public void fshiKeteNrAutomatikeFundit(int index)
        {// metoda per heqjen e nje ACRNumraAutomatike ne nje arraylist ne nje pozicion te caktuar
            base.RemoveAt(index);
        }

        public void shtoNrAutomatikeFunditNeIndeksin(int index, clsNrAutomatikFundit nrAutoFundit)
        {// metoda per shtimin e nje ACRNumraAutomatike ne nje arraylist ne nje pozicion te caktuar
            base.Insert(index, nrAutoFundit);
        }

        public int indeksiNrAutomatikeFundit(clsNrAutomatikFundit nrAutoFundit)
        {//metoda per te marre indeksin e nje ACRNumraAutomatike
            return base.IndexOf(nrAutoFundit);
        }

        public bool ekzistonNrAutomatikeFundit(clsNrAutomatikFundit nrAutoFundit)
        {// metoda per te pare nqs ACRNumraAutomatike ekziston ne nje arraylist
            if (base.Contains(nrAutoFundit))
                return true;
            else return false;
        }

        public int numriNrAutomatikeFundit()
        {// metoda per te marre nr e ACRNumraAutomatike ne arrayList
            return base.Count;
        }

        #endregion

        #region Metoda Private
        private bool mbushNumratAutomatikeFundit(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsNrAutomatikFundit nrAutoFundit = new clsNrAutomatikFundit();
                    //nrAutoFundit.mbushNumraAutomatikeFundit(rreshti);
                    Add(new clsNrAutomatikFundit(rreshti));
                }

            //}
            //catch (InvalidCastException)
            //{
            //    return false;
            //}
            return true;
        }

      
        #endregion

        public colNrAutomatikFundit mbushArrayListeNrAutomatikeFundit(DataTable ds)
        {// metoda per te mbushur nje arraylist me ACRNumraAutomatike nga nje dataset
            colNrAutomatikFundit numratAutomatikeFundit = new colNrAutomatikFundit();
            foreach (DataRow rreshti in ds.Rows)
            {
                clsNrAutomatikFundit nrAutoFundit = new clsNrAutomatikFundit();
                nrAutoFundit.IdNrFunditAutomatik = int.Parse(rreshti[0].ToString());
                nrAutoFundit.IdNrAutom = int.Parse(rreshti[1].ToString());
                nrAutoFundit.Data = DateTime.Parse(rreshti[2].ToString());
                nrAutoFundit.Vlera = rreshti[3].ToString();
                nrAutoFundit.IdPerdoruesi = int.Parse(rreshti[4].ToString());
                nrAutoFundit.IdNdermarje = int.Parse(rreshti[5].ToString());
                numratAutomatikeFundit.Add(nrAutoFundit);
            }
            return numratAutomatikeFundit;
        }
    }
}

