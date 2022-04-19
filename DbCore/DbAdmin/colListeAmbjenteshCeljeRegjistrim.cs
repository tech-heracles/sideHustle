using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;

namespace DbCore.DbAdmin
{
    public class colListeAmbjenteshCeljeRegjistrim : System.Collections.Generic.List<clsListeAmbjenteCeljeRegjistrim>
    {
        #region Metoda Publike

        public new clsListeAmbjenteCeljeRegjistrim this[int index]
        {
            get { return ((clsListeAmbjenteCeljeRegjistrim)base[index]); }
        }

        public bool shtoListeAmbjenteshCeljeRegjistrim(clsListeAmbjenteCeljeRegjistrim listeAmbjenteshCeljeRegjistrim)
        {// metoda per shtimin e nje listeAmbjenteshCeljeRegjistrim ne nje arraylist
            base.Add(listeAmbjenteshCeljeRegjistrim);
            if (base.Contains(listeAmbjenteshCeljeRegjistrim))
                return true;
            else return false;
        }

        public bool fshiListeAmbjenteshCeljeRegjistrim(clsListeAmbjenteCeljeRegjistrim listeAmbjenteshCeljeRegjistrim)
        {// metoda per heqjen e nje listeAmbjenteshCeljeRegjistrim ne nje arraylist
            base.Remove(listeAmbjenteshCeljeRegjistrim);
            if (base.Contains(listeAmbjenteshCeljeRegjistrim))
                return false;
            else return true;
        }

        public bool fshiGjitheListeAmbjenteshCeljeRegjistrim()
        {// metoda per heqjen e te gjithe listeAmbjenteshCeljeRegjistrim nga arraylist
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }

        public void fshiKeteListeAmbjenteshCeljeRegjistrim(int index)
        {// metoda per heqjen e nje listeAmbjenteshCeljeRegjistrim ne nje arraylist ne nje pozicion te caktuar
            base.RemoveAt(index);
        }

        public void shtoListeAmbjenteshCeljeRegjistrimNeIndeksin(int index, clsListeAmbjenteCeljeRegjistrim listeAmbjenteshCeljeRegjistrim)
        {// metoda per shtimin e nje listeAmbjenteshCeljeRegjistrim ne nje arraylist ne nje pozicion te caktuar
            base.Insert(index, listeAmbjenteshCeljeRegjistrim);
        }

        public int indeksiListeAmbjenteshCeljeRegjistrim(clsListeAmbjenteCeljeRegjistrim listeAmbjenteshCeljeRegjistrim)
        {//metoda per te marre indeksin e nje listeAmbjenteshCeljeRegjistrim
            return base.IndexOf(listeAmbjenteshCeljeRegjistrim);
        }

        public bool ekzistonListeAmbjenteshCeljeRegjistrim(clsListeAmbjenteCeljeRegjistrim listeAmbjenteshCeljeRegjistrim)
        {// metoda per te pare nqs listeAmbjenteshCeljeRegjistrim ekziston ne nje arraylist
            if (base.Contains(listeAmbjenteshCeljeRegjistrim))
                return true;
            else return false;
        }

        public int numriListeAmbjenteshCeljeRegjistrim()
        {// metoda per te marre nr e listeAmbjenteshCeljeRegjistrim ne arrayList
            return base.Count;
        }

        public bool mbushGjitheListeAmbjenteshCeljeRegjistrim()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool sukses = mbushListaAmbjenteshCeljeRegjistrimesh(data.ktheGjitheListeAmbjenteshCeljeRegjistrim());
            data.Dispose();
            return sukses;
        }

        #endregion

        #region Metoda Private

        private bool mbushListaAmbjenteshCeljeRegjistrimesh(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsListeAmbjenteCeljeRegjistrim listeAmbjenteCeljeRegjistrim = new clsListeAmbjenteCeljeRegjistrim();
                    //listeAmbjenteCeljeRegjistrim.mbushListAmbjenteCeljeReg(rreshti);
                    Add(new clsListeAmbjenteCeljeRegjistrim(rreshti));
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }

        #endregion
        [Obsolete("Perdor: bool mbushListaAmbjenteshCeljeRegjistrimesh(DataTable dt)", true)]
        public colListeAmbjenteshCeljeRegjistrim mbushArrayListListeAmbjenteshCeljeRegjistrim(DataSet ds)
        {// metoda per te mbushur nje arraylist me listeAmbjenteshCeljeRegjistrim nga nje dataset
            colListeAmbjenteshCeljeRegjistrim listeAmbjenteshCeljeRegjistrim = new colListeAmbjenteshCeljeRegjistrim();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsListeAmbjenteCeljeRegjistrim listeAmbjenteCeljeRegjistrim = new clsListeAmbjenteCeljeRegjistrim();
                listeAmbjenteCeljeRegjistrim.IdCR = int.Parse(rreshti[0].ToString());
                listeAmbjenteCeljeRegjistrim.KodiCR = rreshti[1].ToString();
                listeAmbjenteCeljeRegjistrim.PershkrimCR= rreshti[2].ToString();


                listeAmbjenteshCeljeRegjistrim.Add(listeAmbjenteCeljeRegjistrim);
            }
            return listeAmbjenteshCeljeRegjistrim;
        }
    }
}
