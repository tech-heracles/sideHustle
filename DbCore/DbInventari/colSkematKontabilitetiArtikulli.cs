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
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsSkemaKontabilitetiArtikulli
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colSkematKontabilitetiArtikulli : System.Collections.Generic.List<clsSkemaKontabilitetiArtikulli>
    {
        #region Konstruktoret

        /// <summary>
        /// konstruktor pa parametra
        /// </summary>
        public colSkematKontabilitetiArtikulli()
        {
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idndermarje">id e ndermarrjes</param>
        public colSkematKontabilitetiArtikulli(int idndermarje)
        {
            clsDatabaseInventari dbSkemaKontabilitetArtikull = new clsDatabaseInventari();
            mbushSkematKontabilitetArtikulli(dbSkemaKontabilitetArtikull.ktheSkemaKontabilitetiArtikulliTeGjithaSipasNdermarjes(idndermarje));
            dbSkemaKontabilitetArtikull.Dispose();
        }

        /// <summary>
        /// konstruktor me 2 parametra
        /// </summary>
        /// <param name="idndermarje">id e ndermarrjes</param>
        /// <param name="llojiArt">lloji i artikullit</param>
        public colSkematKontabilitetiArtikulli(int idndermarje, bool llojiArt, int idklasa)
        {
            using (var dbSkemaKontabilitetArtikull = new clsDatabaseInventari())
                mbushSkematKontabilitetArtikulli(dbSkemaKontabilitetArtikull.ktheSkemaKontabilitetiArtikulliTeGjithaSipasNdermarjesDheLlojit(idndermarje, llojiArt, idklasa));

        }


        /// <summary>
        /// konstruktor me 2 parametra
        /// </summary>
        /// <param name="klasa">klasa</param>
        /// <param name="idnderm">id e ndermarrjes</param>
        public colSkematKontabilitetiArtikulli(int klasa, int idnderm)
        {
            clsDatabaseInventari dbSkemaKontabilitetArtikull = new clsDatabaseInventari();
            mbushSkematKontabilitetArtikulli(dbSkemaKontabilitetArtikull.ktheSkemaKontabilitetiArtikulliSipasKlases(klasa, idnderm));
            dbSkemaKontabilitetArtikull.Dispose();
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbInventari.clsSkemaKontabilitetiArtikulli"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsSkemaKontabilitetiArtikulli this[int index]
        {
            get { return ((clsSkemaKontabilitetiArtikulli)base[index]); }
        }
        /// <summary>
        /// metoda per shtimin e nje obj clsSkemaKontabilitetiArtikulli ne nje arraylist
        /// </summary>
        public bool shtoSkemaKontabilitetiArtikulli(clsSkemaKontabilitetiArtikulli skemaKontabilitetiArtikulli)
        {
            base.Add(skemaKontabilitetiArtikulli);
            if (base.Contains(skemaKontabilitetiArtikulli))
                return true;
            else return false;
        }
        /// <summary>
        /// metoda per heqjen e nje obj clsSkemaKontabilitetiArtikulli ne nje arraylist
        /// </summary>
        public bool fshiSkemaKontabilitetiArtikulli(clsSkemaKontabilitetiArtikulli skemaKontabilitetiArtikulli)
        {
            base.Remove(skemaKontabilitetiArtikulli);
            if (base.Contains(skemaKontabilitetiArtikulli))
                return false;
            else return true;
        }
        /// <summary>
        /// metoda per heqjen e nje obj clsSkemaKontabilitetiArtikulli ne nje arraylist
        /// </summary>
        public bool fshiGjitheSkematKontabilitetiArtikulli()
        {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// metoda per heqjen e nje obj clsSkemaKontabilitetiArtikulli ne nje arraylist
        /// </summary>
        public void fshiKeteSkemaKontabilitetiArtikulli(int index)
        {
            base.RemoveAt(index);
        }
        /// <summary>
        /// metoda shton nje obj te ri ne array list ne pozicionin e percaktuar nga index-i
        /// </summary>
        public void shtoSkemaKontabilitetiArtikulliNeIndeksin(int index, clsSkemaKontabilitetiArtikulli skemaKontabilitetiArtikulli)
        {
            base.Insert(index, skemaKontabilitetiArtikulli);
        }
        /// <summary>
        /// metoda gjen index-in ne liste ne te cilin ndodhet nje obj i caktuar
        /// </summary>
        public int indeksiSkemaKontabilitetiArtikulli(clsSkemaKontabilitetiArtikulli skemaKontabilitetiArtikulli)
        {
            return base.IndexOf(skemaKontabilitetiArtikulli);
        }
        /// <summary>
        /// metoda kontrollon nqs nje obj ndodhet ne arraylist
        /// </summary>
        public bool ekzistonSkemaKontabilitetiArtikulli(clsSkemaKontabilitetiArtikulli skemaKontabilitetiArtikulli)
        {
            if (base.Contains(skemaKontabilitetiArtikulli))
                return true;
            else return false;
        }
        /// <summary>
        /// metoda qe kthen nr e obj qe ka arraylist
        /// </summary>
        public int numriSkemaKontabilitetiArtikulli()
        {
            return base.Count;
        }

        /// <summary>
        /// mbush te gjitha skemate e kontabilitett te artikullit
        /// </summary>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushSkemaKontabilitetiArtikulliTeGjitha()
        {
            clsDatabaseInventari dbSkemaKontabilitetArtikull = new clsDatabaseInventari();
            bool sukses = mbushSkematKontabilitetArtikulli(dbSkemaKontabilitetArtikull.ktheSkemaKontabilitetiArtikulliTeGjitha());
            dbSkemaKontabilitetArtikull.Dispose();
            return sukses;
        }
         
        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbInventari.clsSkemaKontabilitetiArtikulli"/> 
        /// </summary>
        private bool mbushSkematKontabilitetArtikulli(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsSkemaKontabilitetiArtikulli skemaKontabilitetiArtikulli = new clsSkemaKontabilitetiArtikulli();
                    //skemaKontabilitetiArtikulli.mbushSkemaKontabilitetiArtikull(rreshti);
                    this.Add(new clsSkemaKontabilitetiArtikulli(rreshti));
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }

        #endregion
        [Obsolete("Perdor: bool mbushSkematKontabilitetArtikulli(DataTable dt)", true)]
        public colSkematKontabilitetiArtikulli mbushArrayListSkemashKontabilitetiArtikulli(DataSet ds)
        {
            colSkematKontabilitetiArtikulli skematKontabilitetiArtikulli = new colSkematKontabilitetiArtikulli();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsSkemaKontabilitetiArtikulli skemaKontabilitetiArtikulli = new clsSkemaKontabilitetiArtikulli();

                skemaKontabilitetiArtikulli.IdSkemaKontabilitetiArtikulli = int.Parse(rreshti[0].ToString());
                skemaKontabilitetiArtikulli.KodiSkemaKontabilitetiArtikulli = rreshti[1].ToString();
                skemaKontabilitetiArtikulli.PershkrimiSkemaKontabilitetiArtikulli= rreshti[2].ToString();
                skemaKontabilitetiArtikulli.Klasa = int.Parse(rreshti[3].ToString());
                skemaKontabilitetiArtikulli.IdLlogariInventari= int.Parse(rreshti[4].ToString());
                skemaKontabilitetiArtikulli.IdLlogariBlerje = int.Parse(rreshti[5].ToString());
                skemaKontabilitetiArtikulli.IdLlogariShitje = int.Parse(rreshti[6].ToString());
                skemaKontabilitetiArtikulli.IdLlogariTekTeTretet = int.Parse(rreshti[7].ToString());
                skemaKontabilitetiArtikulli.IdLlogariShpenzimi = int.Parse(rreshti[8].ToString());
                skemaKontabilitetiArtikulli.IdNdermarje = int.Parse(rreshti[9].ToString());
                skemaKontabilitetiArtikulli.NrLlogariInventari = rreshti[10].ToString();
                skemaKontabilitetiArtikulli.NrLlogariBlerje = rreshti[11].ToString();
                skemaKontabilitetiArtikulli.NrLlogariShitje = rreshti[12].ToString();
                skemaKontabilitetiArtikulli.NrLlogariTekTeTretet = rreshti[13].ToString();
                skemaKontabilitetiArtikulli.NrLlogariShpenzimi = rreshti[14].ToString();
                skemaKontabilitetiArtikulli.LlojiArt = bool.Parse(rreshti[15].ToString());
                skematKontabilitetiArtikulli.Add(skemaKontabilitetiArtikulli);
            }
            return skematKontabilitetiArtikulli;
        }
    }
}
