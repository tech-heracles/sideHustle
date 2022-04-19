using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
namespace DbCore.DbAdmin
{
    public class colAutorizimetTrupi : System.Collections.Generic.List<clsAutorizimTrupi>
    {
        #region Konstruktoret

        public colAutorizimetTrupi()
        {
        }

        public colAutorizimetTrupi(int id)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            mbushAutorizimetTrupi(data.ktheAutorizimTrupiNgaIdAutorizimKoka(id));
            data.Dispose();
        }

        #endregion

        #region Metoda Publike

        public new clsAutorizimTrupi this[int index]
        {
            get { return ((clsAutorizimTrupi)base[index]); }
        }

        public bool shtoAutorizimTrupi(clsAutorizimTrupi autorizimTrupi)
        {// metoda per shtimin e nje autorizim trupi ne nje arraylist
            base.Add(autorizimTrupi);
            if (base.Contains(autorizimTrupi))
                return true;
            else return false;
        }

        public bool fshiAutorizimTrupi(clsAutorizimTrupi autorizimTrupi)
        {// metoda per heqjen e nje autorizim trupi ne nje arraylist
            base.Remove(autorizimTrupi);
            if (base.Contains(autorizimTrupi))
                return false;
            else return true;
        }

        public bool fshiGjitheAutorzimTrupi()
        {// metoda per heqjen e te gjithe autorizimet trupi nga arraylist
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }

        public void fshiKeteAutorizimTrupi(int index)
        {// metoda per heqjen e nje autorizim trupi ne nje arraylist ne nje pozicion te caktuar
            base.RemoveAt(index);
        }

        public void shtoAutorizimTrupiNeIndeksin(int index, clsAutorizimTrupi autorizimTrupi)
        {// metoda per shtimin e nje autorizim trupi ne nje arraylist ne nje pozicion te caktuar
            base.Insert(index, autorizimTrupi);
        }

        public int indeksiAutorizimTrupi(clsAutorizimTrupi autorizimTrupi)
        {//metoda per te marre indeksin e nje autorizim trupi
            return base.IndexOf(autorizimTrupi);
        }

        public bool ekzistonAutorizimTrupi(clsAutorizimTrupi autorizimTrupi)
        {// metoda per te pare nqs autorizim trupi ekziston ne nje arraylist
            if (base.Contains(autorizimTrupi))
                return true;
            else return false;
        }

        public int numriAutorizimeveTrupi()
        {// metoda per te marre nr e autorizimeve trupi ne arrayList
            return base.Count;
        }

        #endregion

        #region Metoda Private

        private bool mbushAutorizimetTrupi(DataTable dt)
        {
            //try
            //{
            foreach (DataRow rreshti in dt.Rows)
            {
                //clsAutorizimTrupi autorizim = new clsAutorizimTrupi();
                //autorizim.mbushAutorizimTrupi(rreshti);
                Add(new clsAutorizimTrupi(rreshti));
            }

            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }

        #endregion
        [Obsolete("Perdor: bool mbushAutorizimetTrupi(DataTable dt)", true)]
        public colAutorizimetTrupi mbushArrayListAutorizimTrupi(DataSet ds)
        {// metoda per te mbushur nje arraylist me autorizim trupi nga nje dataset
            colAutorizimetTrupi autorizimet = new colAutorizimetTrupi();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsAutorizimTrupi autorizim = new clsAutorizimTrupi();
                autorizim.IdAutorizimTrupi = int.Parse(rreshti[0].ToString());
                autorizim.IdAutorizimKoka = int.Parse(rreshti[1].ToString());
                autorizim.IdPerdorues = int.Parse(rreshti[2].ToString());


                autorizimet.Add(autorizim);
            }
            return autorizimet;
        }


        public clsMesazh fshiAutorizimetELidhurPervecTeRinjve(int idPerdoruesi, string idAutorizimetRi)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                return fshiAutorizimetELidhurPervecTeRinjve(idPerdoruesi, idAutorizimetRi, data);
            }
        }

        public clsMesazh fshiAutorizimetELidhurPervecTeRinjve(int idPerdoruesi, string idAutorizimetRi, clsDatabaseAdmin data)
        {
            return data.fshiAutorizimSipasPerdoruesPervecTeRinjve(idPerdoruesi, idAutorizimetRi);
        }

        public clsMesazh Ruaj(int idPerdoruesi)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                return Ruaj(idPerdoruesi, data);
            }
        }

        public clsMesazh Ruaj(int idPerdoruesi, clsDatabaseAdmin data)
        {
            foreach (clsAutorizimTrupi at in this)
            {
                clsMesazh mesazh = at.ruajAutorizimTrupiNeseNukEkziston(at.IdAutorizimKoka, at.IdPerdorues, data);
                if (!mesazh.Status) return mesazh;
            }
            return fshiAutorizimetELidhurPervecTeRinjve(idPerdoruesi, String.Join(",", (from tr in this select tr.IdAutorizimKoka.ToString()).ToArray()), data);
        }
    }
}
