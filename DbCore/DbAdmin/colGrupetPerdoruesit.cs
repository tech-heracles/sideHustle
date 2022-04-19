using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;

namespace DbCore.DbAdmin
{
    public class colGrupetPerdoruesit
    {
        #region Atributet

        private ArrayList grupetPerdoruesve = new ArrayList();

        #endregion

        #region Metoda Publike

        public bool shtoGrupPerdoruesish(colGrupetPerdoruesit grup)
        {
            grupetPerdoruesve.Add(grup);
            if (grupetPerdoruesve.Contains(grup))
                return true;
            else return false;
        }

        public bool fshiGrupPerdoruesish(colGrupetPerdoruesit grup)
        {
            grupetPerdoruesve.Remove(grup);
            if (grupetPerdoruesve.Contains(grup))
                return false;
            else return true;
        }

        public bool fshiGjitheGrupetPerdoruesve()
        {
            grupetPerdoruesve.Clear();
            if (grupetPerdoruesve.Count == 0)
                return true;
            else
                return false;
        }

        public void fshiKetegrupPerdoruesish(int index)
        {
            grupetPerdoruesve.RemoveAt(index);
        }

        public void shtoGrupPerdoruesishNeIndeksin(int index, colGrupetPerdoruesit grup)
        {
            grupetPerdoruesve.Insert(index, grup);
        }

        public int indeksiGrupitPerdoruesve(colGrupetPerdoruesit grup)
        {
            return grupetPerdoruesve.IndexOf(grup);
        }

        public bool ekzistonGrupiPerdoruesve(colGrupetPerdoruesit grup)
        {
            if (grupetPerdoruesve.Contains(grup))
                return true;
            else return false;
        }

        public int numriGrupevePerdoruesve()
        {
            return grupetPerdoruesve.Count;
        }

        #endregion

        #region Metoda Private

        private bool mbushGrupetPerdoruesve(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsGrupetPerdoruesit grup = new clsGrupetPerdoruesit();
                    //grup.mbushGrupetPerdoruesit(rreshti);
                    grupetPerdoruesve.Add(new clsGrupetPerdoruesit(rreshti));
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }

        #endregion
        [Obsolete("Perdor: bool mbushGrupetPerdoruesve(DataTable dt)", true)]
        public void mbushArrayListGrupetPerdoruesve(DataSet ds)
        {

            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsGrupetPerdoruesit grup = new clsGrupetPerdoruesit();

                grup.IdGrupiPerdorues = int.Parse(rreshti["IDGRUPIPERD"].ToString());
                grup.GrupiPerdoruesPershkrimi = rreshti["GRUPIPERDPERSHK"].ToString();
                grup.GrupiAktivPerdorues = int.Parse(rreshti["GRUPIPERDAKTIV"].ToString());
                grup.GrupiPerdoruesData = (DateTime)rreshti["GRUPIPERDDATA"];

                grupetPerdoruesve.Add(grup);
            }
        }
    }
}
