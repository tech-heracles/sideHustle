using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;

namespace DbCore.DbAdmin
{

    //shpjegim:
    //Collectioni perdorueseve derivon nga System.Collections.Generic.List . 
    //System.Collections.Generic.List nga ana e tij derivon System.Collections.ArrayList
    //perdoret klasa perkatese generic, sepse eshte type safe (edhe n.q.s ne perdorim
    //gabimisht funksionin Add te ArrayList ne vend te ShtoPerdorues, dhe i japim nje 
    //objekt te gabuar, kompilatori do jap error, sepse ne kemi specifikuar objektet
    //nga e cila do perbehet ky collection(<clsPerdorues>)
    public class colPerdoruesit : System.Collections.Generic.List<clsPerdorues>
    {
        #region Metoda Publike

        public new clsPerdorues this[int index]
        {
            get { return ((clsPerdorues)base[index]); }
        }

        public bool shtoPerdorues(clsPerdorues perdorues)
        {
            base.Add(perdorues);
            if (base.Contains(perdorues))
                return true;
            else return false;
        }

        public bool fshiPerdorues(clsPerdorues perdorues)
        {
            base.Remove(perdorues);
            if (base.Contains(perdorues))
                return false;
            else return true;
        }

        public bool fshiGjithePerdoruesit()
        {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }

        public void fshiKetePerdorues(int index)
        {
            base.RemoveAt(index);
        }

        public void shtoPerdoruesNeIndeksin(int index, clsPerdorues perdorues)
        {
            base.Insert(index, perdorues);
        }

        public int indeksiPerdoruesit(clsPerdorues perdorues)
        {
            return base.IndexOf(perdorues);
        }

        public bool ekzistonPerdoruesi(clsPerdorues perdorues)
        {
            if (base.Contains(perdorues))
                return true;
            else return false;
        }

        public int numriPerdoruesve()
        {
            return base.Count;
        }

        public colTeDrejtat merriGjitheTeDrejtat(clsPerdorues o, int idndermarrjeviti)
        {
            DbCore.DbAdmin.colTeDrejtat db = new colTeDrejtat();
            db.mbushTeDrejtePerdorues(o.IdPerdorues, idndermarrjeviti);
            return db;
        }

        public colTeDrejtat merriGjitheTeDrejtatPerGjitheNdermarrjet(clsPerdorues o)
        {
            DbCore.DbAdmin.colTeDrejtat db = new colTeDrejtat();
            db.mbushTeDrejtePerdoruesGjitheNdermarrjet(o.IdPerdorues);
            return db;

        }
        public static DataRow merrPerdoruesDR(int idperdorues)
        {
            clsDatabaseAdmin dbartikuj = new clsDatabaseAdmin();
            DataRow rreshti = dbartikuj.merrPerdoruesDR(idperdorues);
            dbartikuj.Dispose();
            return rreshti;
        }
        public static DataTable merrPerdoruesitSipasLicencesDT(int idperdorues, int idlicenca)
        {
            clsDatabaseAdmin dbartikuj = new clsDatabaseAdmin();
            DataTable tabela = dbartikuj.merrPerdoruesitSipasLicencesDT(idperdorues, idlicenca);
            dbartikuj.Dispose();
            return tabela;
        }
        public static DataTable MerrSipasLicencesDTPerExport(int idperdorues, int idlicenca)
        {
            using (clsDatabaseAdmin db = new clsDatabaseAdmin())
                return db.merrSipasLicencesDTPerExport(idperdorues, idlicenca);
        }




        public static int merrNrPerdoruesishSipasLicences(int idperdorues, int idlicenca)
        {
            using (clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin())
            {
                return dbAdmin.merrNrPerdoruesishSipasLicences(idperdorues, idlicenca);               
            }
        }
        public static DataTable merrPerdoruesitSipasLicencesDTJoSuper(int idperdorues, int idlicenca)
        {
            clsDatabaseAdmin dbartikuj = new clsDatabaseAdmin();
            DataTable tabela = dbartikuj.merrPerdoruesitSipasLicencesDTJoSuper(idperdorues, idlicenca);
            dbartikuj.Dispose();
            return tabela;
        }
        public static DataTable merrPerdoruesitSipasLicencesDTPerNdermaje(int idndermarje)
        {
            clsDatabaseAdmin dbartikuj = new clsDatabaseAdmin();
            DataTable tabela = dbartikuj.merrPerdoruesitSipasLicencesDTPerNdermaje(idndermarje);
            dbartikuj.Dispose();
            return tabela;
        }

        


        /// <summary>
        /// mbush gjithe perdoruesit e licences se perdoruesit
        /// </summary>
        /// <param name="idperdorues"></param>
        /// <returns></returns>
        public bool mbushGjithePerdoruesit(int idperdorues)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool sukses = mbushPerdoruesit(data.ktheGjithePerdoruesit(idperdorues));
            data.Dispose();
            return sukses;
        }

        public bool mbushGjithePerdoruesitSipasAutorizimit(int idperdorues, int idautorizimi)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool sukses = mbushPerdoruesit(data.ktheGjithePerdoruesitSipasAutorizimit(idperdorues, idautorizimi));
            data.Dispose();
            return sukses;
        }

        public bool mbushGjithePerdoruesitLike(int idperdorues, int idlicenca, string text)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool sukses = mbushPerdoruesit(data.ktheGjithePerdoruesitLike(idperdorues, idlicenca, text));
            data.Dispose();
            return sukses;
        }
        public bool mbushUserNgaLogin(string perdoruesUsername)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool sukses = mbushPerdoruesit(data.ktheUserNgaLogin(perdoruesUsername));
            data.Dispose();
            return sukses;
        }

        public bool mbushPerdoruesGjitheNdermarrjet(int idperdorues)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool sukses = mbushPerdoruesitGjitheNdermarrje(data.merrPerdoruesGjitheNdermarrjet(idperdorues));
            data.Dispose();
            return sukses;
        }

        public string[] merrEmailSipasGjuhesPerdoruesit(int idGjuha)
        {
            return this.Where(elem => elem.IdGjuha == idGjuha).Select(cls => cls.PerdoruesEmail).ToArray();
        }

        #endregion

        #region Metoda Private

        internal bool mbushPerdoruesit(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsPerdorues perdorues = new clsPerdorues();
                    //perdorues.mbushPerdorues(rreshti);
                    Add(new clsPerdorues(rreshti));
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }

        private bool mbushPerdoruesitGjitheNdermarrje(DataTable dt)
        {
            try
            {
                foreach (DataRow rreshti in dt.Rows)
                {
                    clsPerdorues perdorues = new clsPerdorues();
                    perdorues.mbushPerdoruesGjitheNder(rreshti);
                    Add(perdorues);
                }

            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        #endregion
    }
}
