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
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsDetajimArt
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colDetajimePerArt : System.Collections.Generic.List<clsDetajimPerArt>
    {
        #region Metoda publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbInventari.clsDetajimPerArt"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsDetajimPerArt this[int index]
        {
            get { return ((clsDetajimPerArt)base[index]); }
        }
        /// <summary>
        /// metoda per shtimin e nje obj clsDetajimArt ne nje arraylist
        /// </summary>
        public bool shtoDetajimArt(clsDetajimPerArt detajimArt)
        {
            base.Add(detajimArt);
            if (base.Contains(detajimArt))
                return true;
            else return false;
        }
        /// <summary>
        /// metoda per heqjen e nje obj clsDetajimArt ne nje arraylist
        /// </summary>
        public bool fshiDetajimArt(clsDetajimPerArt detajimArt)
        {
            base.Remove(detajimArt);
            if (base.Contains(detajimArt))
                return false;
            else return true;
        }
        /// <summary>
        /// metoda per heqjen e nje obj clsDetajimArt ne nje arraylist
        /// </summary>
        public bool fshiGjitheDetajimeArt()
        {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// metoda per heqjen e nje obj clsDetajimArt ne nje arraylist
        /// </summary>
        public void fshiKeteDetajimArt(int index)
        {
            base.RemoveAt(index);
        }
        /// <summary>
        /// metoda shton nje obj te ri ne array list ne pozicionin e percaktuar nga index-i
        /// </summary>
        public void shtoDetajimArtNeIndeksin(int index, clsDetajimPerArt detajimArt)
        {
            base.Insert(index, detajimArt);
        }
        /// <summary>
        /// metoda gjen index-in ne liste ne te cilin ndodhet nje obj i caktuar
        /// </summary>
        public int indeksiDetajimArtt(clsDetajimPerArt detajimArt)
        {
            return base.IndexOf(detajimArt);
        }
        /// <summary>
        /// metoda kontrollon nqs nje obj ndodhet ne arraylist
        /// </summary>
        public bool ekzistonDetajimArt(clsDetajimPerArt detajimArt)
        {
            if (base.Contains(detajimArt))
                return true;
            else return false;
        }
        /// <summary>
        /// metoda qe kthen nr e obj qe ka arraylist
        /// </summary>
        public int numriDetajimeveArt()
        {
            return base.Count;
        }

        /// <summary>
        /// mbush detajimet e artikullit sipas id se artikullit
        /// </summary>
        /// <param name="idartikulli">id e artikullit</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        /// <param name="dbDetajimPerArt"></param>
        public bool mbushDetajimArtSipasIdArtikulli(int idartikulli, clsDatabaseInventari dbDetajimPerArt)
        {          
            return mbushDetajimePerArt(dbDetajimPerArt.ktheDetajimArtSipasIdArtikulli(idartikulli));
        }

        /// <summary>
        /// mbush detajimet e artikullit sipas id se artikullit
        /// </summary>
        /// <param name="idartikulli">id e artikullit</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        /// <param name="dbDetajimPerArt"></param>
        public bool mbushDetajimArtSipasIdArtikulli(int idartikulli)
        {
            clsDatabaseInventari dbDetajimPerArt = new clsDatabaseInventari();
            bool sukses = mbushDetajimArtSipasIdArtikulli(idartikulli,dbDetajimPerArt);
            dbDetajimPerArt.Dispose();
            return sukses;
        }
        
        /// <summary>
        /// mbush detajimet e artikullit sipas id se artikullit dhe sipas llojit te detajimit
        /// </summary>
        /// <param name="idartikulli">id e artikullit</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        /// <param name="dbDetajimPerArt"></param>
        public bool mbushDetajimArtSipasIdArtikulliDheLlojit(int idartikulli, int lloji)
        {
            clsDatabaseInventari dbDetajimPerArt = new clsDatabaseInventari();
            bool sukses = mbushDetajimArtSipasIdArtikulliDheLlojit(idartikulli, lloji, dbDetajimPerArt);
            dbDetajimPerArt.Dispose();
            return sukses;
        }


            /// <summary>
        /// mbush detajimet e artikullit sipas id se artikullit dhe llojit
        /// </summary>
        /// <param name="idartikulli">id e artikullit</param>
        /// <param name="lloji">lloji i detajimit i pare apo i dyte</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        /// <param name="dbDetajimPerArt">dbinvetari ne raste transaksioni</param>
        public bool mbushDetajimArtSipasIdArtikulliDheLlojit(int idartikulli, int lloji, clsDatabaseInventari dbDetajimPerArt)
        {
          //if(dbDetajimPerArt==null) dbDetajimPerArt = new clsDatabaseInventari();
          return mbushDetajimePerArt(dbDetajimPerArt.ktheDetajimArtSipasIdArtikulliDheLlojit(idartikulli, lloji));
        }


        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje Datatable
        ///  <see cref="DbCore.DbInventari.clsDetajimPerArt"/> 
        /// </summary>
        private bool mbushDetajimePerArt(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsDetajimPerArt detajimArt = new clsDetajimPerArt();
                    //detajimArt.mbushDetajimPerArtikull(rreshti);
                    this.Add(new clsDetajimPerArt(rreshti));
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }

        #endregion
        [Obsolete("Perdor: bool mbushDetajimePerArt(DataTable dt)", true)]
        public colDetajimePerArt mbushArrayListDetajimeshArt(DataSet ds)
        {
            colDetajimePerArt detajimeArtikullit = new colDetajimePerArt();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsDetajimPerArt detajimArt = new clsDetajimPerArt();

                detajimArt.IdDetajimArt = int.Parse(rreshti[0].ToString());
                detajimArt.IdArtikulli =int.Parse ( rreshti[1].ToString());
                detajimArt.IdDetajimArtikulli = int.Parse(rreshti[2].ToString());
              
                detajimeArtikullit.Add(detajimArt);
            }
            return detajimeArtikullit;
        }
    }
}
