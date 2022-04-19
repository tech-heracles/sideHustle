using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;

namespace DbCore.DbAdmin
{
    public class colGrupetPerdoruesve: System.Collections.Generic.List<clsGrupiPerdorues>
    {
        #region Metoda Publike

        public new clsGrupiPerdorues this[int index]
        {
            get { return ((clsGrupiPerdorues)base[index]); }
        }

        public bool shtoGrupPerdoruesish(clsGrupiPerdorues grup)
        {
            base.Add(grup);
            if (base.Contains(grup))
                return true;
            else return false;
        }

        public bool fshiGrupPerdoruesish(clsGrupiPerdorues grup)
        {
            base.Remove(grup);
            if (base.Contains(grup))
                return false;
            else return true;
        }

        public bool fshiGjitheGrupetPerdoruesve()
        {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }

        public void fshiKetegrupPerdoruesish(int index)
        {
            base.RemoveAt(index);
        }

        public void shtoGrupPerdoruesishNeIndeksin(int index, clsGrupiPerdorues grup)
        {
            base.Insert(index, grup);
        }

        public int indeksiGrupitPerdoruesve(clsGrupiPerdorues grup)
        {
            return base.IndexOf(grup);
        }

        public bool ekzistonGrupiPerdoruesve(clsGrupiPerdorues grup)
        {
            if (base.Contains(grup))
                return true;
            else return false;
        }

        public int numriGrupevePerdoruesve()
        {
            return base.Count;
        }

        public colTeDrejtat merriGjitheTeDrejtatPerGjitheNdermarrjet(clsGrupiPerdorues o)
        {
            DbCore.DbAdmin.colTeDrejtat db = new colTeDrejtat();
            db.mbushTeDrejteGrupPerdoruesGjitheNdermarrjet(o.IdGrupiPerdorues);
            return db;
        }

        public colTeDrejtat merriGjitheTeDrejtat(clsGrupiPerdorues o, int idndermarrjevit)
        {
            DbCore.DbAdmin.colTeDrejtat db = new colTeDrejtat();
            db.mbushTeDrejteGrupPerdorues(o.IdGrupiPerdorues, idndermarrjevit);
            return db;
        }

        public bool mbushGrupPerdoruesGjitheNdermarrjet(int id)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool sukses = mbushGrupetPerdoruesveGjitheNdermarrje(data.merrGrupPerdoruesGjitheNdermarrjet(id));
            data.Dispose();
            return sukses;
        }

        public bool mbushGjitheGrupetPerdoruesve()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool sukses = mbushGrupetPerdoruesveGjitheNdermarrje(data.ktheGjitheGrupetPerdoruesve());
            data.Dispose();
            return sukses;
        }

        public bool mbushGjitheGrupetPerdoruesvePozitive()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool sukses = mbushGrupetPerdoruesveGjitheNdermarrje(data.ktheGjitheGrupetPerdoruesvePozitive());
            data.Dispose();
            return sukses;
        }

        #endregion

        #region Metoda Private

        private bool mbushGrupetPerdoruesveGjitheNdermarrje(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsGrupiPerdorues grup = new clsGrupiPerdorues();
                    //grup.mbushGrupPerdoruesGjitheNdermarrje(rreshti);
                    Add(new clsGrupiPerdorues(rreshti));
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }

        private bool mbushGrupetPerdoruesve(DataTable dt, int idndermarrjeviti)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsGrupiPerdorues grup = new clsGrupiPerdorues();
                    //grup.mbushGrupPerdorues(rreshti, idndermarrjeviti);
                    Add(new clsGrupiPerdorues(rreshti, idndermarrjeviti));
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }

        #endregion
        [Obsolete("Perdor: bool mbushGrupetPerdoruesveGjitheNdermarrje(DataTable dt)", true)]
        public colGrupetPerdoruesve mbushArrayListGrupetPerdoruesveGjitheNdermarrjet(DataSet ds)
        {
            colGrupetPerdoruesve grupetPerdoruesve = new colGrupetPerdoruesve();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsGrupiPerdorues grup = new clsGrupiPerdorues();

                grup.IdGrupiPerdorues = int.Parse(rreshti["IDGRUPIPERD"].ToString());
                grup.GrupiPerdoruesKodi = rreshti["GRUPIKODI"].ToString();
                grup.GrupiPerdoruesPershkrimi = rreshti["GRUPIPERDPERSHK"].ToString();
                grup.GrupiAktivPerdorues = (bool)(rreshti["GRUPIPERDAKTIV"]);
                grup.GrupiPerdoruesData = (DateTime)rreshti["GRUPIPERDDATA"];
                grup.IdPerdoruesi = int.Parse(rreshti["IDPERDORUESI"].ToString());
                grup.OColTeDrejtat = merriGjitheTeDrejtatPerGjitheNdermarrjet(grup);

                grupetPerdoruesve.Add(grup);
            }
            return grupetPerdoruesve;
        }
        [Obsolete("Perdor: bool mbushGrupetPerdoruesve(DataTable dt, int idndermarrjeviti)", true)]
        public colGrupetPerdoruesve mbushArrayListGrupetPerdoruesve(DataSet ds, int idndermarrjeviti)
        {
            colGrupetPerdoruesve grupetPerdoruesve = new colGrupetPerdoruesve();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsGrupiPerdorues grup = new clsGrupiPerdorues();

                grup.IdGrupiPerdorues = int.Parse(rreshti["IDGRUPIPERD"].ToString());
                grup.GrupiPerdoruesKodi = rreshti["GRUPIKODI"].ToString();
                grup.GrupiPerdoruesPershkrimi = rreshti["GRUPIPERDPERSHK"].ToString();
                grup.GrupiAktivPerdorues = (bool)(rreshti["GRUPIPERDAKTIV"]);
                grup.GrupiPerdoruesData = (DateTime)rreshti["GRUPIPERDDATA"];
                grup.IdPerdoruesi = int.Parse(rreshti["IDPERDORUESI"].ToString());
                grup.OColTeDrejtat = merriGjitheTeDrejtat(grup, idndermarrjeviti);
                grupetPerdoruesve.Add(grup);
            }
            return grupetPerdoruesve;
        }
    }
}
