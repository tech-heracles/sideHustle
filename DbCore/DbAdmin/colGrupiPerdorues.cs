using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;

namespace DbCore.DbAdmin
{
    public class colGrupiPerdorues
    {
        private ArrayList grupetPerdoruesve = new ArrayList();

        public bool shtoGrupPerdoruesish(colGrupiPerdorues grup)
        {
            grupetPerdoruesve.Add(grup);
            if (grupetPerdoruesve.Contains(grup))
                return true;
            else return false;
        }

        public bool fshiGrupPerdoruesish(colGrupiPerdorues grup)
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

        public void shtoGrupPerdoruesishNeIndeksin(int index, colGrupiPerdorues grup)
        {
            grupetPerdoruesve.Insert(index, grup);
        }

        public int indeksiGrupitPerdoruesve(colGrupiPerdorues grup)
        {
            return grupetPerdoruesve.IndexOf(grup);
        }

        public bool ekzistonGrupiPerdoruesve(colGrupiPerdorues grup)
        {
            if (grupetPerdoruesve.Contains(grup))
                return true;
            else return false;
        }

        public int numriGrupevePerdoruesve()
        {
            return grupetPerdoruesve.Count;
        }

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
