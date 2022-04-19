using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
namespace DbCore.DbAdmin
{
    public class colAutorizimetKoka : System.Collections.Generic.List<clsAutorizimKoka>
    {
        /// <summary>
        /// klasa permban te gjitha veprimet qe mund te behem mbi nje arraylist me obj te tipit clsAutorizimKoka
        /// </summary>

        #region Konstruktoret

        public colAutorizimetKoka()
        {
        }

        public colAutorizimetKoka(int idperdoruesi)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            mbushAutorizimeKoka(data.ktheAutorizimKokaPerPerdorues(idperdoruesi));
            data.Dispose();
        }
        public colAutorizimetKoka(int idperdoruesi, clsDatabaseAdmin data)
         {
            mbushAutorizimeKoka(data.ktheAutorizimKokaPerPerdorues(idperdoruesi));
         }
        #endregion

        #region Metoda Publike

        public new clsAutorizimKoka this[int index]
        {
            get { return ((clsAutorizimKoka)base[index]); }
        }

        /// <summary>
        ///  metoda per shtimin e nje autorizim koka ne nje arraylist
        /// </summary>    
        public bool shtoAutorizimKoka(clsAutorizimKoka autorizimKoka)
        {
            base.Add(autorizimKoka);
            if (base.Contains(autorizimKoka))
                return true;
            else return false;
        }

        /// <summary>
        ///metoda per heqjen e nje autorizim koka ne nje arraylist
        /// </summary>  
        public bool fshiAutorizimKoka(clsAutorizimKoka autorizimKoka)
        {
            base.Remove(autorizimKoka);
            if (base.Contains(autorizimKoka))
                return false;
            else return true;
        }

        /// <summary>
        /// metoda per heqjen e te gjithe autorizimet koka nga arraylist
        /// </summary>  
        public bool fshiGjitheAutorzimKoka()
        {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// metoda per heqjen e nje autorizim koke ne nje arraylist ne nje pozicion te caktuar
        /// </summary>  
        public void fshiKeteAutorizimKoke(int index)
        {
            base.RemoveAt(index);
        }

        /// <summary>
        /// metoda per shtimin e nje autorizim koke ne nje arraylist ne nje pozicion te caktuar
        /// </summary>  
        public void shtoAutorizimKokeNeIndeksin(int index, clsAutorizimKoka autorizimKoka)
        {
            base.Insert(index, autorizimKoka);
        }

        /// <summary>
        ///metoda per te marre indeksin e nje autorizim koker
        /// </summary>  
        public int indeksiAutorizimKoke(clsAutorizimKoka autorizimKoka)
        {
            return base.IndexOf(autorizimKoka);
        }

        /// <summary>
        /// metoda per te pare nqs autorizim koka ekziston ne nje arraylist
        /// </summary>  
        public bool ekzistonAutorizimKoka(clsAutorizimKoka autorizimKoka)
        {
            if (base.Contains(autorizimKoka))
                return true;
            else return false;
        }

        /// <summary>
        /// metoda per te marre nr e autorizimeve koke ne arrayList
        /// </summary> 
        public int numriAutorizimeveKoka()
        {
            return base.Count;
        }

        /// <summary>
        /// metoda per te mbushur nje arraylist me autorizim koka nga nje dataset
        /// </summary> 

        public bool mbushGjitheAutorizimet(int idndermarje, int idperdoruesi)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool sukses = mbushAutorizimeKoka(data.ktheGjitheAutorizimet(idndermarje, idperdoruesi));
            data.Dispose();
            return sukses;
        }
        public static DataTable merrGjitheAutorizimet(int idnderm,int idperdoruesi)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            DataTable tabela = data.ktheGjitheAutorizimetDT(idnderm, idperdoruesi);
            data.Dispose();
            return tabela;
        }
        public static DataRow merrAutorizimDR(int idautorizim)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            DataRow rreshti = data.merrAutorizimDR(idautorizim);
            data.Dispose();
            return rreshti;
        }
        public static string merrAutorizimeArt(int idArtikulli){
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                return data.merrAutorizimeArt(idArtikulli);
            }               
        }

        public static string merrAutorizimeSipasIdLidheseDheLloj(int idLidhese, string kodLloj,int idPerdorues)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                return data.merrAutorizimeSipasIdLidheseDheLloj(idLidhese, kodLloj, idPerdorues);
            }
        }
        #endregion

        #region Metoda Private

        private bool mbushAutorizimeKoka(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsAutorizimKoka autorizim = new clsAutorizimKoka();
                    //autorizim.mbushAutorizimKoka(rreshti);
                    Add(new clsAutorizimKoka(rreshti));
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }

        #endregion
        [Obsolete("Perdor: bool mbushAutorizimeKoka(DataTable dt)", true)]
        public colAutorizimetKoka mbushArrayListAutorizimKoka(DataSet ds)
        {
            colAutorizimetKoka autorizimet = new colAutorizimetKoka();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsAutorizimKoka autorizim = new clsAutorizimKoka();
                autorizim.IdAutorizimKoka = int.Parse(rreshti[0].ToString());
                autorizim.KodiAutorizim = rreshti[1].ToString();
                autorizim.PershkrimAutorizim = rreshti[2].ToString();
                autorizim.IdPerdoruesi = int.Parse(rreshti[3].ToString());

                autorizimet.Add(autorizim);
            }
            return autorizimet;
        }
    }
}
