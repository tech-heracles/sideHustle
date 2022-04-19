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
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsNivelZbritje
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colNiveleZbritjesh : System.Collections.Generic.List<clsNivelZbritje>
    {
        #region Konstruktoret

        /// <summary>
        /// konstruktor pa parameter
        /// </summary>
        public colNiveleZbritjesh()
        {
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbInventari.clsNivelZbritje"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsNivelZbritje this[int index]
        {
            get { return ((clsNivelZbritje)base[index]); }
        }
        /// <summary>
        /// metoda per shtimin e nje obj clsNivelZbritje ne nje arraylist
        /// </summary>
        public bool shtoNivelZbritje(clsNivelZbritje nivelZbritje)
        {
            base.Add(nivelZbritje);
            if (base.Contains(nivelZbritje))
                return true;
            else return false;
        }
        /// <summary>
        /// metoda per heqjen e nje obj clsNivelZbritje ne nje arraylist
        /// </summary>
        public bool fshiNivelZbritje(clsNivelZbritje nivelZbritje)
        {
            base.Remove(nivelZbritje);
            if (base.Contains(nivelZbritje))
                return false;
            else return true;
        }
        /// <summary>
        /// metoda per heqjen e nje obj clsNivelZbritje ne nje arraylist
        /// </summary>
        public bool fshiGjitheNiveleZbritjesh()
        {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// metoda per heqjen e nje obj clsNivelZbritje ne nje arraylist
        /// </summary>
        public void fshiKeteNivelZbritje(int index)
        {
            base.RemoveAt(index);
        }
        /// <summary>
        /// metoda shton nje obj te ri ne array list ne pozicionin e percaktuar nga index-i
        /// </summary>
        public void shtoNivelZbritjeNeIndeksin(int index, clsNivelZbritje nivelZbritje)
        {
            base.Insert(index, nivelZbritje);
        }
        /// <summary>
        /// metoda gjen index-in ne liste ne te cilin ndodhet nje obj i caktuar
        /// </summary>
        public int indeksiNivelZbritje(clsNivelZbritje nivelZbritje)
        {
            return base.IndexOf(nivelZbritje);
        }
        /// <summary>
        /// metoda kontrollon nqs nje obj ndodhet ne arraylist
        /// </summary>
        public bool ekzistonNivelZbritje(clsNivelZbritje nivelZbritje)
        {
            if (base.Contains(nivelZbritje))
                return true;
            else return false;
        }
        /// <summary>
        /// metoda qe kthen nr e obj qe ka arraylist
        /// </summary>
        public int numriNiveleveZbritje()
        {
            return base.Count;
        }


        /// <summary>
        /// mbush te gjitha nivelet e zbritjeve sipas ndermarrjes
        /// </summary>
        /// <param name="idnderm">id e ndermarrjes</param>
        /// <returns>kthen true kur mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushGjitheNiveleZbritjeshSipasNdermarjes(int idnderm)
        {
            clsDatabaseInventari dbNiveleZbritjes = new clsDatabaseInventari();
            bool sukses = mbushNiveleZbritjesh(dbNiveleZbritjes.ktheGjitheNiveleZbritjeshSipasNdermarjes(idnderm));
            dbNiveleZbritjes.Dispose();
            return sukses;
        }

        /// <summary>
        /// mbush te gjitha nivelet e zbritjeve prind sipas ndermarrjes
        /// </summary>
        /// <param name="idnderm">id e ndermarrjes</param>
        /// <returns>kthen true kur mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushGjitheNiveleZbritjeshPrindiSipasNdermarjes(int idnderm)
        {
            clsDatabaseInventari dbNiveleZbritjes = new clsDatabaseInventari();
            bool sukses = mbushNiveleZbritjesh(dbNiveleZbritjes.ktheGjitheNiveleZbritjeshPrindiSipasNdermarjes(idnderm));
            dbNiveleZbritjes.Dispose();
            return sukses;
        }

        /// <summary>
        /// mbush nivelin e zbritjes sipas prindit
        /// </summary>
        /// <param name="idprindi">id e prindit</param>
        /// <returns>kthen true kur mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushNiveleZbritjeshSipasPrindit(int idprindi)
        {
            clsDatabaseInventari dbNiveleZbritjes = new clsDatabaseInventari();
            bool sukses = mbushNiveleZbritjesh(dbNiveleZbritjes.ktheNivelZbritjeSipasPrindit(idprindi));
            dbNiveleZbritjes.Dispose();
            return sukses;
        }
        public static DataRow merrNivelZbritjeSipasNdermarjesDR(int idnderm, int idnivel)
            {
            clsDatabaseInventari dbartikuj = new clsDatabaseInventari();
            DataRow rreshti = dbartikuj.merrNivelZbritjeSipasNdermarjesDR(idnderm, idnivel);
            dbartikuj.Dispose();
            return rreshti;
            }
        public static DataTable merrNiveleZbritjeshNdermarjeDT(int idnderm)
            {
            clsDatabaseInventari dbartikuj = new clsDatabaseInventari();
            DataTable tabela = dbartikuj.merrNiveleZbritjeshNdermarjeDT(idnderm);
            dbartikuj.Dispose();
            return tabela;
            }
               
        public static DataTable MerrNivelZbritjeSipasNdermarrjesAc(int idNdermarrje, string kodi)
        {
            using (var databaseInventari = new clsDatabaseInventari())
                return databaseInventari.MerrNivelZbritjeSipasNdermarrjesAc(idNdermarrje, kodi);
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbInventari.clsNivelZbritje"/> 
        /// </summary>
        private bool mbushNiveleZbritjesh(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsNivelZbritje nivelZbritje = new clsNivelZbritje();
                    //nivelZbritje.mbushNivelZbritje(rreshti);
                    this.Add(new clsNivelZbritje(rreshti));
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }

        #endregion
        [Obsolete("Perdor: bool mbushNiveleZbritjesh(DataTable dt)", true)]
        public colNiveleZbritjesh mbushArrayListNiveleZbritjesh(DataSet ds)
        {
            colNiveleZbritjesh niveleZbritjesh = new colNiveleZbritjesh();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsNivelZbritje nivelZbritje = new clsNivelZbritje();

                nivelZbritje.IdNivelZbritje = int.Parse(rreshti[0].ToString());
                nivelZbritje.KodNivelZbritje = rreshti[1].ToString();
                nivelZbritje.PershkrimNivelZbritje = rreshti[2].ToString();
                nivelZbritje.IdPrindi = int.Parse(rreshti[3].ToString());               
                nivelZbritje.PrioritetiNivelZbritje = int.Parse(rreshti[4].ToString());
                nivelZbritje.IdPerdoruesi = int.Parse(rreshti[5].ToString());
                //nivelZbritje.IdNderViti = int.Parse(rreshti[6].ToString());
                nivelZbritje.IdNdermarje = int.Parse(rreshti[6].ToString());

                niveleZbritjesh.Add(nivelZbritje);
            }
            return niveleZbritjesh;
        }
    }
}