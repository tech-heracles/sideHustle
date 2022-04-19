using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;

namespace DbCore.DbArkaBanka
{
    public class colGrupeBanke : System.Collections.Generic.List<clsGrupBanke>
    {
        #region Konstruktoret

        public colGrupeBanke()
        {

        }

        public colGrupeBanke(int idnderm)
        {
           clsDatabaseArkaBanka data = new clsDatabaseArkaBanka();
           mbushGrupeBanke(data.ktheGjitheGrupetBankeSipasNdermarjes(idnderm));
        }

        public colGrupeBanke(bool lloji, int idnderm)
        {
           clsDatabaseArkaBanka data = new clsDatabaseArkaBanka();
           mbushGrupeBanke(data.ktheGjitheGrupetBankeSipasLlojit(lloji, idnderm));
        }

        #endregion

        #region Metoda Publike

        public new clsGrupBanke this[int index]
        {
            get { return ((clsGrupBanke)base[index]); }
        }

        public bool shtoGrupBanke(clsGrupBanke grupBanke)
        {
            base.Add(grupBanke);
            if (base.Contains(grupBanke))
                return true;
            else return false;
        }

        public bool fshiGrupBanke(clsGrupBanke grupBanke)
        {
            base.Remove(grupBanke);
            if (base.Contains(grupBanke))
                return false;
            else return true;
        }

        public bool fshiGrupBanke()
        {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }

        public void fshiKeteGrupBanke(int index)
        {
            base.RemoveAt(index);
        }

        public void shtoGrupBankeNeIndeksin(int index, clsGrupBanke grupBanke)
        {
            base.Insert(index, grupBanke);
        }

        public int indeksiGrupBanket(clsGrupBanke grupBanke)
        {
            return base.IndexOf(grupBanke);
        }

        public bool ekzistonGrupBanke(clsGrupBanke grupBanke)
        {
            if (base.Contains(grupBanke))
                return true;
            else return false;
        }

        public int numriGrupeveBanke()
        {
            return base.Count;
        }

      

        #endregion

        #region Metoda Private

        private bool mbushGrupeBanke(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsGrupBanke grupBanke = new clsGrupBanke();
                    //grupBanke.mbushGrupBanke(rreshti);
                    Add(new clsGrupBanke(rreshti));
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }

        #endregion
        [Obsolete("Perdor: bool mbushGrupeBanke(DataTable dt)", true)]
        public colGrupeBanke mbushArrayListGrupeBanke(DataSet ds)
        {
            colGrupeBanke grupeBanke = new colGrupeBanke();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsGrupBanke grupBanke = new clsGrupBanke();

                grupBanke.IdGrupBanke = int.Parse(rreshti[0].ToString());
                grupBanke.NrGrupBanke = rreshti[1].ToString();
                grupBanke.PershkrimGrupBanke = rreshti[2].ToString();
                grupBanke.IdPerdoruesi = int.Parse(rreshti[3].ToString());
                grupBanke.LlojArkaBanka = bool.Parse(rreshti[4].ToString());
                grupBanke.IdNdermarje = int.Parse(rreshti[5].ToString());
                grupeBanke.Add(grupBanke);
            }
            return grupeBanke;
        }
    }
}