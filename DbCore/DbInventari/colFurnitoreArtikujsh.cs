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
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsFurnitoreArtikulli
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colFurnitoreArtikujsh : System.Collections.Generic.List<clsFurnitoreArtikulli >
    {    
        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbInventari.clsFurnitoreArtikulli"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsFurnitoreArtikulli this[int index]
        {
            get { return ((clsFurnitoreArtikulli)base[index]); }
        }
        /// <summary>
        /// metoda per shtimin e nje obj clsFurnitoreArtikulli ne nje arraylist
        /// </summary>
        public bool shtoFurnitoreArtikulli(clsFurnitoreArtikulli furnitoreArtikulli)
        {
            base.Add(furnitoreArtikulli);
            if (base.Contains(furnitoreArtikulli))
                return true;

            else return false;
        }
        /// <summary>
        /// metoda per heqjen e nje obj clsFurnitoreArtikulli ne nje arraylist
        /// </summary>
        public bool fshiFurnitoreArtikulli(clsFurnitoreArtikulli furnitoreArtikulli)
        {
            base.Remove(furnitoreArtikulli);
            if (base.Contains(furnitoreArtikulli))
                return false;
            else return true;
        }
        /// <summary>
        /// metoda per heqjen e nje obj clsFurnitoreArtikulli ne nje arraylist
        /// </summary>
        public bool fshiGjitheFurnitoreArtikujsh()
        {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// metoda per heqjen e nje obj clsFurnitoreArtikulli ne nje arraylist
        /// </summary>
        public void fshiKeteFurnitoreArtikulli(int index)
        {
            base.RemoveAt(index);
        }
        /// <summary>
        /// metoda shton nje obj te ri ne array list ne pozicionin e percaktuar nga index-i
        /// </summary>
        public void shtoFurnitoreArtikulliNeIndeksin(int index, clsFurnitoreArtikulli furnitoreArtikulli)
        {
            base.Insert(index, furnitoreArtikulli);
        }
        /// <summary>
        /// metoda gjen index-in ne liste ne te cilin ndodhet nje obj i caktuar
        /// </summary>
        public int indeksiFurnitoreArtikullit(clsFurnitoreArtikulli furnitoreArtikulli)
        {
            return base.IndexOf(furnitoreArtikulli);
        }
        /// <summary>
        /// metoda kontrollon nqs nje obj ndodhet ne arraylist
        /// </summary>
        public bool ekzistonFurnitoreArtikulli(clsFurnitoreArtikulli furnitoreArtikulli)
        {
            if (base.Contains(furnitoreArtikulli))
                return true;
            else return false;
        }
        /// <summary>
        /// metoda qe kthen nr e obj qe ka arraylist
        /// </summary>
        public int numriFurnitoreArtikujve()
        {
            return base.Count;
        }

        /// <summary>
        /// mbush furnitoret e nje artikulli i cili filtrohet sipas id se artikullit
        /// </summary>
        /// <param name="idartikulli">id e artikullit</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushFurnitoreArtikulliSipasIdArtikulli(int idartikulli)
        {
            clsDatabaseInventari dbFurnitoreArtikulli = new clsDatabaseInventari();
            return mbushFurnitoreArtikujsh(dbFurnitoreArtikulli.ktheFurnitoreArtikulliSipasIdArtikulli(idartikulli));
        }  public bool mbushFurnitoreArtikulliSipasIdArtikulli(int idartikulli,clsDatabaseInventari dbFurnitoreArtikulli)
        {
           
            return mbushFurnitoreArtikujsh(dbFurnitoreArtikulli.ktheFurnitoreArtikulliSipasIdArtikulli(idartikulli));
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbInventari.clsFurnitoreArtikulli"/> 
        /// </summary>
        private bool mbushFurnitoreArtikujsh(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsFurnitoreArtikulli furnitoreArtikulli = new clsFurnitoreArtikulli();
                    //furnitoreArtikulli.mbushFurnitoreArtikulli(rreshti);
                    this.Add(new clsFurnitoreArtikulli(rreshti));
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }

        #endregion
        [Obsolete("Perdor: bool mbushFurnitoreArtikujsh(DataTable dt)", true)]
        public colFurnitoreArtikujsh mbushArrayListFurnitoreArtikujsh(DataSet ds)
        {
            colFurnitoreArtikujsh furnitoreArtikujsh = new colFurnitoreArtikujsh();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsFurnitoreArtikulli furnitoreArtikulli = new clsFurnitoreArtikulli();

                furnitoreArtikulli.IdFurnitoreArtikulli = int.Parse(rreshti[0].ToString());
                furnitoreArtikulli.IdArtikulli = int.Parse(rreshti[1].ToString());
                furnitoreArtikulli.IdFurnitori = int.Parse(rreshti[2].ToString());
                furnitoreArtikulli.Prioriteti = rreshti[3].ToString();
                furnitoreArtikulli.KodiKF = rreshti[4].ToString();
                furnitoreArtikulli.EmertimiKF = rreshti[5].ToString();

                furnitoreArtikujsh.Add(furnitoreArtikulli);
            }
            return furnitoreArtikujsh;
        }
    }
}
