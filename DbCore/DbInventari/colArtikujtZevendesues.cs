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
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsArtikullZevendesues
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colArtikujtZevendesues : System.Collections.Generic.List<clsArtikullZevendesues >
    {

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbInventari.clsArtikullZevendesues"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsArtikullZevendesues this[int index]
        {
            get { return ((clsArtikullZevendesues)base[index]); }
        }
        /// <summary>
        /// metoda per shtimin e nje obj clsArtikullZevendesues ne nje arraylist
        /// </summary>
        public bool shtoArtikullZevendesues(clsArtikullZevendesues artikullZevendesues)
        {
            base.Add(artikullZevendesues);
            if (base.Contains(artikullZevendesues))
                return true;
            else return false;
        }
        /// <summary>
        /// metoda per heqjen e nje obj clsArtikullZevendesues ne nje arraylist
        /// </summary>
        public bool fshiArtikullZevendesues(clsArtikullZevendesues artikullZevendesues)
        {
            base.Remove(artikullZevendesues);
            if (base.Contains(artikullZevendesues))
                return false;
            else return true;
        }
        /// <summary>
        /// metoda per heqjen e nje obj clsArtikullZevendesues ne nje arraylist
        /// </summary>
        public bool fshiGjitheArtikujtZevendesues()
        {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// metoda per heqjen e nje obj clsArtikullZevendesues ne nje arraylist
        /// </summary>
        public void fshiKeteArtikullZevendesues(int index)
        {
            base.RemoveAt(index);
        }
        /// <summary>
        /// metoda shton nje obj te ri ne array list ne pozicionin e percaktuar nga index-i
        /// </summary>
        public void shtoArtikullZevendesuesNeIndeksin(int index, clsArtikullZevendesues artikullZevendesues)
        {
            base.Insert(index, artikullZevendesues);
        }
        /// <summary>
        /// metoda gjen index-in ne liste ne te cilin ndodhet nje obj i caktuar
        /// </summary>
        public int indeksiArtikullitZevendesues(clsArtikullZevendesues artikullZevendesues)
        {
            return base.IndexOf(artikullZevendesues);
        }
        /// <summary>
        /// metoda kontrollon nqs nje obj ndodhet ne arraylist
        /// </summary>
        public bool ekzistonArtikullZevendesues(clsArtikullZevendesues artikullZevendesues)
        {
            if (base.Contains(artikullZevendesues))
                return true;
            else return false;
        }
        /// <summary>
        /// metoda qe kthen nr e obj qe ka arraylist
        /// </summary>
        public int numriArtikujveZevendesues()
        {
            return base.Count;
        }

        /// <summary>
        /// merr artikujt zevendesues sipas id se artikullit kryesor
        /// </summary>
        /// <param name="idartikulli">id e artikullit kryesore</param>
        /// <returns>kthen true nese kryhet me sukses, ne te kundert false</returns>
        public bool merrArtZevendesuesSipasIdArtikulli(int idartikulli)
        {
            clsDatabaseInventari dbArtikujZevendesues = new clsDatabaseInventari();
            bool sukses = mbushArtikujtZevendesues(dbArtikujZevendesues.ktheArtikujZevendesuesSipasIdArtikulli(idartikulli));
            dbArtikujZevendesues.Dispose();
            return sukses;
        }  
        public bool merrArtZevendesuesSipasIdArtikulli(int idartikulli, clsDatabaseInventari dbArtikujZevendesues)
        {  
            bool sukses = mbushArtikujtZevendesues(dbArtikujZevendesues.ktheArtikujZevendesuesSipasIdArtikulli(idartikulli));
           
            return sukses;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbInventari.clsArtikullZevendesues"/> 
        /// </summary>
        private bool mbushArtikujtZevendesues(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsArtikullZevendesues artikullZevendesues = new clsArtikullZevendesues();
                    //artikullZevendesues.mbushArtikullZevendesues(rreshti);
                    this.Add(new clsArtikullZevendesues(rreshti));
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //    //throw;
            //}
            return true;

        }

        #endregion
        [Obsolete("Perdor: bool mbushArtikujtZevendesues(DataTable dt)", true)]
        public colArtikujtZevendesues mbushArrayListArtikujshZevendesues(DataSet ds)
        {
            colArtikujtZevendesues artikujZevendesues = new colArtikujtZevendesues();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsArtikullZevendesues artikullZevendesues = new clsArtikullZevendesues();

                artikullZevendesues.IdArtikulliZevendesues = int.Parse(rreshti[0].ToString());
                artikullZevendesues.IdArtikulliKryesor = int.Parse(rreshti[1].ToString());
                artikullZevendesues.IdArtikulliZevend = int.Parse(rreshti[2].ToString());
                artikullZevendesues.Prioriteti = rreshti[3].ToString();
                artikullZevendesues.KodArtikulli = rreshti[4].ToString();
                artikullZevendesues.PershkrimArtikulli = rreshti[5].ToString();
                artikujZevendesues.Add(artikullZevendesues);
            }
            return artikujZevendesues;
        }
    }
}

