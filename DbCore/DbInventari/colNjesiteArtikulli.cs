using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;

namespace DbCore.DbInventari
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsNjesiArtikulli
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colNjesiteArtikulli : System.Collections.Generic.List<clsNjesiArtikulli >
    {
        #region Konstruktore

        /// <summary>
        /// konstruktor bosh
        /// </summary>
        public colNjesiteArtikulli()
        {
        }

        public colNjesiteArtikulli(List<int> idNjesiArtikujsh)
        {
            if (idNjesiArtikujsh.Count <= 0)
                return;
            using (clsDatabaseInventari db = new clsDatabaseInventari())
            {
                mbushNjesiArtikujsh(db.merrNjesiArtikujshSipasIdve(idNjesiArtikujsh));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idkokeshitje"></param>
        /// <param name="idnderm"></param>
        public colNjesiteArtikulli(int idkokeshitje, int idnderm)
        {
            if (idkokeshitje > 0)
                using (clsDatabaseInventari db = new clsDatabaseInventari())
                {
                    mbushNjesiArtikujsh(db.merrNjesiArtikulliFature(idkokeshitje));
                }
        }
        public colNjesiteArtikulli(int idkokeshitje, int idnderm,colNjesiteArtikulli colNjesiArt)
        {
            if (idkokeshitje > 0)
                using (clsDatabaseInventari db = new clsDatabaseInventari())
                {
                    mbushNjesiArtikujsh(db.merrNjesiArtikulliFature(idkokeshitje),colNjesiArt);
                }
        }

        /// <summary>
        /// konstruktor me 1 parameter integer
        /// </summary>
        /// <param name="idnder">id e ndermarrjes</param>
        public colNjesiteArtikulli(int idnder)
        {
            clsDatabaseInventari dbNjesiArtikujsh = new clsDatabaseInventari();
            mbushNjesiArtikujsh(dbNjesiArtikujsh.ktheGjitheNjesiteArtikulliSipasNdermarrjes(idnder));
            dbNjesiArtikujsh.Dispose();

        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbInventari.clsNjesiArtikulli"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsNjesiArtikulli this[int index]
        {
            get { return ((clsNjesiArtikulli)base[index]); }
        }

        /// <summary>
        /// metoda per shtimin e nje obj clsNjesiArtikulli ne nje arraylist
        /// </summary>
        public bool shtoNjesiArtikulli(clsNjesiArtikulli njesiArtikulli)
        {
            base.Add(njesiArtikulli);
            if (base.Contains(njesiArtikulli))
                return true;
            else return false;
        }
        /// <summary>
        /// metoda per heqjen e nje obj clsNjesiArtikulli ne nje arraylist
        /// </summary>
        public bool fshiNjesiArtikulli(clsNjesiArtikulli njesiArtikulli)
        {
            base.Remove(njesiArtikulli);
            if (base.Contains(njesiArtikulli))
                return false;
            else return true;
        }
        /// <summary>
        /// metoda per heqjen e nje obj clsNjesiArtikulli ne nje arraylist
        /// </summary>
        public bool fshiGjitheNjesiteArtikulli()
        {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// metoda per heqjen e nje obj clsNjesiArtikulli ne nje arraylist
        /// </summary>
        public void fshiKeteNjesiArtikulli(int index)
        {
            base.RemoveAt(index);
        }
        /// <summary>
        /// metoda shton nje obj te ri ne array list ne pozicionin e percaktuar nga index-i
        /// </summary>
        public void shtoNjesiArtikullinNeIndeksin(int index, clsNjesiArtikulli njesiArtikulli)
        {
            base.Insert(index, njesiArtikulli);
        }
        /// <summary>
        /// metoda gjen index-in ne liste ne te cilin ndodhet nje obj i caktuar
        /// </summary>
        public int indeksiNjesiseArtikulli(clsNjesiArtikulli njesiArtikulli)
        {
            return base.IndexOf(njesiArtikulli);
        }
        /// <summary>
        /// metoda kontrollon nqs nje obj ndodhet ne arraylist
        /// </summary>
        public bool ekzistonNjesiArtikulli(clsNjesiArtikulli njesiArtikulli)
        {
            if (base.Contains(njesiArtikulli))
                return true;
            else return false;
        }
        /// <summary>
        /// metoda qe kthen nr e obj qe ka arraylist
        /// </summary>
        public int numriNjesiveArtikulli()
        {
            return base.Count;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbInventari.clsNjesiArtikulli"/> 
        /// </summary>
        private bool mbushNjesiArtikujsh(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsNjesiArtikulli njesiArtikulli = new clsNjesiArtikulli();
                    //njesiArtikulli.mbushNjesiArtikulli(rreshti);
                    this.Add(new clsNjesiArtikulli(rreshti));
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }
        private bool mbushNjesiArtikujsh(DataTable dt, colNjesiteArtikulli colNjesiArt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                //clsNjesiArtikulli njesiArtikulli = new clsNjesiArtikulli();
                //njesiArtikulli.mbushNjesiArtikulli(rreshti);
                    if (!rreshti.IsNull("IDNJESIA"))
                        this.Add(colNjesiArt.Where(x => x.IdNjesia == int.Parse(rreshti["IDNJESIA"].ToString())).FirstOrDefault());
                    else
                        this.Add(new clsNjesiArtikulli());
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }

        #endregion
        [Obsolete("Perdor: bool mbushNjesiArtikujsh(DataTable dt)", true)]
        public colNjesiteArtikulli mbushArrayListNjesishArtikulli(DataSet ds)
        {
            colNjesiteArtikulli njesiteArtikulli = new colNjesiteArtikulli();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsNjesiArtikulli njesiArtikulli = new clsNjesiArtikulli();

                njesiArtikulli.IdNjesia = int.Parse(rreshti[0].ToString());
                njesiArtikulli.KodNjesia =rreshti[1].ToString();
                njesiArtikulli.PershkrimNjesia= rreshti[2].ToString();
                njesiArtikulli.IdPerdoruesi = int.Parse(rreshti[3].ToString());
                //njesiArtikulli.IdNderViti = int.Parse(rreshti[4].ToString());
                njesiArtikulli.IdNdermarje = int.Parse(rreshti[4].ToString());
                njesiteArtikulli.Add(njesiArtikulli);
            }
            return njesiteArtikulli;
        }
    }
}
