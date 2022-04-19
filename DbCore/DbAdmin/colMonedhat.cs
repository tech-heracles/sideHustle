using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;

namespace DbCore.DbAdmin
{
    public class colMonedhat : System.Collections.Generic.List<clsMonedha>
    {
        public colMonedhat()
        {

        }
        public colMonedhat(int idNdermarrje, clsDatabaseAdmin db)
        {
            mbushMonedhat(db.merrMonedha(idNdermarrje));
        }

        #region Metoda Publike

        public new clsMonedha this[int index]
        {
            get { return ((clsMonedha)base[index]); }
        }

        public bool shtoMonedhe(clsMonedha monedha)
        {
            base.Add(monedha);
            if (base.Contains(monedha))
                return true;
            else return false;
        }

        public bool fshiMonedhe(clsMonedha monedha)
        {
            base.Remove(monedha);
            if (base.Contains(monedha))
                return false;
            else return true;
        }

        public bool fshiGjitheMonedhat()
        {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }

        public void fshiKeteMonedhe(int index)
        {
            base.RemoveAt(index);
        }

        public void shtoMonedheNeIndeksin(int index, clsMonedha monedha)
        {
            base.Insert(index, monedha);
        }

        public int indeksiMonedhes(clsMonedha monedha)
        {
            return base.IndexOf(monedha);
        }

        public bool ekzistonMonedha(clsMonedha monedha)
        {
            if (base.Contains(monedha))
                return true;
            else return false;
        }

        public int numriMonedhave()
        {
            return base.Count;
        }

        public bool mbushGjitheMonedhat(int id, int idperdorues)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool sukses = mbushMonedhat(data.ktheGjitheMonedhat(id, idperdorues));
            data.Dispose();
            return sukses;
        }

        public static DataTable GetMonedhaLookupSimpleTable(int idNdermarrje, int idPerdoruesi)
        {
            using (clsDatabaseAdmin db = new clsDatabaseAdmin())
                return db.GetMonedhaLookupSimpleTable(idNdermarrje, idPerdoruesi);
        }

        public bool mbushGjitheMonedhatAktive(int id, int idperdorues)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool sukses = mbushMonedhat(data.ktheGjitheMonedhatAktive(id, idperdorues));
            data.Dispose();
            return sukses;
        }
        public static DataTable ktheGjitheMonedhatAktiveDtSmall(int id, int idperdorues)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            return data.ktheGjitheMonedhatAktiveDtSmall(id, idperdorues);
           
        }
        public bool mbushGjitheMonedhatPozitive(int id, int idperdorues)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool sukses = mbushMonedhat(data.ktheGjitheMonedhatPozitive(id, idperdorues));
            data.Dispose();
            return sukses;
        } 
        public bool mbushGjitheMonedhatPozitive(int id, int idperdorues, clsDatabaseAdmin data)
        {

            bool sukses = mbushMonedhat(data.ktheGjitheMonedhatPozitive(id, idperdorues));
        
            return sukses;
        }
        public static DataRow merrMonedhaSipasNdermarjesDR(int idnderm, int idmonedha)
        {
            clsDatabaseAdmin dbartikuj = new clsDatabaseAdmin();
            DataRow rreshti = dbartikuj.merrMonedhaSipasNdermarjesDR(idnderm, idmonedha);
            dbartikuj.Dispose();
            return rreshti;
        }
        public static DataTable merrMonedhaNdermarjeDT(int idnderm, int idperdorues)
        {
            clsDatabaseAdmin dbartikuj = new clsDatabaseAdmin();
            DataTable tabela = dbartikuj.merrMonedhaNdermarjeDT(idnderm, idperdorues);
            dbartikuj.Dispose();
            return tabela;
        }

        public static DataTable merrMonedhaNdermarjeDTAktiv(int idnderm, int idperdorues)
        {
            clsDatabaseAdmin dbartikuj = new clsDatabaseAdmin();
            DataTable tabela = dbartikuj.merrMonedhaNdermarjeDTAktiv(idnderm, idperdorues);
            dbartikuj.Dispose();
            return tabela;
        }

        public int merrFormatKursiSipasMonedhes(int idMon)
        {
            foreach (clsMonedha monedha in this)
            {
                if (monedha.IdMonedha == idMon)

                    return monedha.IdFormatNrKursi = clsFunksione.MerrVleraFormatKursi(idMon);
            }
            return 2; //nuk gjendet formati per kete monedhe
        }

        #endregion

        #region Metoda Private

        private bool mbushMonedhat(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsMonedha monedha = new clsMonedha();
                    //monedha.mbushMonedha(rreshti);
                    Add(new clsMonedha(rreshti));
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }

        #endregion
       
    }
}
