using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbKontabiliteti
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsSkemaKontabelKoka
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colSkemaKontabelKoka : System.Collections.Generic.List<clsSkemaKontabelKoka>
    {
        #region Metoda Publike
        
        /// <summary>
        /// kthen objektin <see cref="DbCore.DbKontabiliteti.clsSkemaKontabelKoka"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsSkemaKontabelKoka this[int index]
        {
            get { return ((clsSkemaKontabelKoka)base[index]); }
        }

        /// <summary>
        /// mbush gjithe skemat kontabel
        /// </summary>
        /// <param name="idNderm">id e ndermarrjes</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushGjitheSkematKontabel(int idNderm)
        {
            clsDatabaseKontabilitet dbSkemaKontKoka = new clsDatabaseKontabilitet();
            bool sukses = mbushSkematKontKokat(dbSkemaKontKoka.ktheGjitheSkematKontabel(idNderm));
            dbSkemaKontKoka.Dispose();
            return sukses;
        }

        /// <summary>
        /// mbush gjithe skemat kontabel sipas autorizimit
        /// </summary>
        /// <param name="idNderm">id e ndermarrjes</param>
        /// <param name="perdorues">perdorues</param>
        /// <returns>>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushGjitheSkematKontabelAndAutorizim(int idNderm, int perdorues)
        {
            clsDatabaseKontabilitet dbSkemaKontKoka = new clsDatabaseKontabilitet();
            bool sukses = mbushSkematKontKokat(dbSkemaKontKoka.ktheGjitheSkematKontabelAndAutorizim(idNderm, perdorues));
            dbSkemaKontKoka.Dispose();
            return sukses;
        }

        /// <summary>
        /// mbush gjithe skemat kontabel default
        /// </summary>
        /// <param name="idNderm">id e ndermarrjes</param>
        /// <returns>>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushGjitheSkematKontabelDefault(int idNderm)
        {
            clsDatabaseKontabilitet dbSkemaKontKoka = new clsDatabaseKontabilitet();
            bool sukses = mbushSkematKontKokat(dbSkemaKontKoka.ktheGjitheSkematKontabelDefault(idNderm));
            dbSkemaKontKoka.Dispose();
            return sukses;
        }

        #endregion

        #region Metoda Private
        
        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbKontabiliteti.clsSkemaKontabelKoka"/> 
        /// </summary>
        private bool mbushSkematKontKokat(DataTable dt)
        {
            try
            {

                foreach (DataRow rreshti in dt.Rows)
                {
                    clsSkemaKontabelKoka koka = new clsSkemaKontabelKoka();
                    koka.mbushSkemaKontabelKoka(rreshti);
                    Add(koka);
                }

            }
            catch (Exception)
            {
                return false;
                //throw;
            }
            return true;
        }

        #endregion
        [Obsolete("Perdor: mbushSkematKontKokat(DataTable dt)", true)]        
        public colSkemaKontabelKoka mbushArrayListSkemaKontKokat(DataSet ds)
        {
            colSkemaKontabelKoka kokat = new colSkemaKontabelKoka();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsSkemaKontabelKoka koka = new clsSkemaKontabelKoka();
                koka.IdSkemaKontabelKoka = int.Parse(rreshti[0].ToString());
                koka.KodiSkemaKontabelKoka = rreshti[1].ToString();
                koka.PershkrimiSkemaKontabelKoka = rreshti[2].ToString();
                koka.AktivSkemaKontabelKoka = (bool)(rreshti[3]);
                //colLlojeBuxhetesh colLloj = new DbCore.DbKontabiliteti.clsDatabaseKontabilitet().merrLlojBuxhetiSipasKodit("SkematKontabel");
                DbAdmin.colLidhjetAutorizim lidhje = new DbAdmin.colLidhjetAutorizim(koka.IdSkemaKontabelKoka, "SkematKontabel");
                //DbAdmin.colLidhjetAutorizim lidhje = new DbAdmin.clsDatabaseAdmin().merrLidhjeAutorizimSipasIdLidheseIdLloji(koka.IdSkemaKontabelKoka, DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("SkematKontabel"));
                if (lidhje.Count != 0)
                {
                    koka.IdAutorizimSkemaKontabelKoka = DbAdmin.clsAutorizimKoka.ktheKodAutorizim(lidhje[0].IdAutorizimeKoka);
                    //koka.IdAutorizimSkemaKontabelKoka = new DbAdmin.clsDatabaseAdmin().ktheAutorizim(lidhje[0].IdAutorizimeKoka)[0].KodiAutorizim;
                    for (int i = 1; i < lidhje.Count; i++)
                        koka.IdAutorizimSkemaKontabelKoka += "," + DbAdmin.clsAutorizimKoka.ktheKodAutorizim(lidhje[i].IdAutorizimeKoka);
                        //koka.IdAutorizimSkemaKontabelKoka += "," + new DbAdmin.clsDatabaseAdmin().ktheAutorizim(lidhje[i].IdAutorizimeKoka)[0].KodiAutorizim;
                }
                else
                    koka.IdAutorizimSkemaKontabelKoka = "";
                // koka.IdAutorizimSkemaKontabelKoka = int.Parse(rreshti[4].ToString());
                koka.IdKursiSkemaKontabelKoka = int.Parse(rreshti[4].ToString());
                koka.NrAutoSkemaKontabelKoka = int.Parse(rreshti[5].ToString());
                koka.IdNderViti = int.Parse(rreshti[7].ToString());
                koka.IdPerdoruesi = int.Parse(rreshti[8].ToString());
                kokat.Add(koka);   
            }
            return kokat;
        }
    }
}
