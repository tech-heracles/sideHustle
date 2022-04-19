using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using DbCore.DbKontabiliteti;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbInventari
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsZbritjeAnalitike
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colZbritjetAnalitike : System.Collections.Generic.List<clsZbritjeAnalitike>
    {
        #region Konstruktoret

        /// <summary>
        /// konstruktor pa parametra
        /// </summary>
        public colZbritjetAnalitike()
        {
        }
        public colZbritjetAnalitike(int idNdermarje)
        {
            DataTable tabela;
            clsDatabaseInventari dbCmimArtikulli = new clsDatabaseInventari();
            tabela = (dbCmimArtikulli.ktheZbritjeAnalitikeDt(idNdermarje));
            dbCmimArtikulli.Dispose();

            foreach (DataRow row in tabela.Rows)
            {
                clsZbritjeAnalitike zbritje = new clsZbritjeAnalitike();
                zbritje.mbushZbritjeAnalitikePerGriden(row);
                Add(zbritje);
            }
            
        }
        /// <summary>
        /// konstruktori i plote
        /// </summary>
        /// <param name="kodartikulli"> kodin e artikullit</param>
        /// <param name="kodbar"> kodbarin</param>
        /// <param name="pershkrimi1">pershkrimin 1 te artikullit</param>
        /// <param name="pershkrimi2"> pershkrimin 2 te artikullit</param>
        /// <param name="kodifikimi1"> kodifikimin 1 te artikullit</param>
        /// <param name="kodifikimi2">kodifikimin 2 te artikullit</param>
        /// <param name="furnitori"> furnitorin e artikullit</param>
        /// <param name="njesia"> njesine e pare ose te dyte  te artikullit</param>
        /// <param name="datafillimit"> daten e fillimit te cmimit</param>
        /// <param name="datambarimit"> daten e mbarimit te cmimit</param>
        /// <param name="idndervit"> id e ndermarje vitit</param>
        /// <param name="idnivelzbritje"> id e nivelit te zbritjes</param>
        /// <param name="idperdorues"> id e perdoruesit</param>
        /// <param name="idndermarje"> id e ndermarjes</param>
        //public colZbritjetAnalitike(string kodartikulli, string kodbar, string pershkrimi1, string pershkrimi2, string kodifikimi1, string kodifikimi2, string furnitori, string njesia, string datafillimit, string datambarimit, int idndervit, string idnivelzbritje, int idperdorues, int idndermarje)
        //{
        //    clsDatabaseInventari dbZbritjeAnalitike = new clsDatabaseInventari();
        //    mbushZbritjeAnalitike(dbZbritjeAnalitike.ktheZbritjeAnalitikeSipasFiltrit(kodartikulli, kodbar, pershkrimi1, pershkrimi2, kodifikimi1, kodifikimi2, furnitori, njesia, datafillimit, datambarimit, idndervit, idnivelzbritje, idperdorues, idndermarje));
        //}
        public static DataTable merrZbritjeAnalitikeSipasFiltrit(string kodartikulli, string kodbar, string pershkrimi1, string pershkrimi2, string kodifikimi1, string kodifikimi2, string furnitori, string njesia, string datafillimit, string datambarimit, string idnivelcmimi, int idperdorues, int idndermarje)
            {
            clsDatabaseInventari dbCmimArtikulli = new clsDatabaseInventari();
            DataTable tabela =(dbCmimArtikulli.ktheZbritjeAnalitikeSipasFiltrit(kodartikulli, kodbar, pershkrimi1, pershkrimi2, kodifikimi1, kodifikimi2, furnitori, njesia, datafillimit, datambarimit, idnivelcmimi, idperdorues, idndermarje));
            dbCmimArtikulli.Dispose();
            return tabela;
            }

      
        /// <summary>
        /// konstruktori me 4 parametra
        /// </summary>
        /// <param name="kodartikulli"> kodi i artikullit</param>
        /// <param name="idnivelzbritje"> id e nivelit te zbritjes</param>
        /// <param name="idperdorues"> id e perdoruesit</param>
        /// <param name="idndermarje"> id e ndermarjes</param>
        public colZbritjetAnalitike(string kodartikulli, string idnivelzbritje, int idperdorues, int idndermarje)
        {
            clsDatabaseInventari dbZbritjeAnalitike = new clsDatabaseInventari();
            mbushZbritjeAnalitike(dbZbritjeAnalitike.ktheZbritjeAnalitikeSipasNivelit(kodartikulli, idnivelzbritje, idperdorues, idndermarje));
            dbZbritjeAnalitike.Dispose();
        }
        public colZbritjetAnalitike(string kodartikulli, int idnivelzbritje, int idperdorues, int idndermarje)
        {
            clsDatabaseInventari dbZbritjeAnalitike = new clsDatabaseInventari();
            mbushZbritjeAnalitike(dbZbritjeAnalitike.ktheZbritjeAnalitikeSipasNivelit(kodartikulli, idnivelzbritje, idperdorues, idndermarje));
            dbZbritjeAnalitike.Dispose();
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbInventari.clsZbritjeAnalitike"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsZbritjeAnalitike this[int index]
        {
            get { return ((clsZbritjeAnalitike)base[index]); }
        }
        /// <summary>
        /// metoda per shtimin e nje obj clsZbritjeAnalitike ne nje arraylist
        /// </summary>
        public bool shtoZbritjeAnalitike(clsZbritjeAnalitike zbritjeAnalitike)
        {
            base.Add(zbritjeAnalitike);
            if (base.Contains(zbritjeAnalitike))
                return true;
            else return false;
        }
        /// <summary>
        /// metoda per heqjen e nje obj clsZbritjeAnalitike ne nje arraylist
        /// </summary>
        public bool fshiZbritjeAnalitike(clsZbritjeAnalitike zbritjeAnalitike)
        {
            base.Remove(zbritjeAnalitike);
            if (base.Contains(zbritjeAnalitike))
                return false;
            else return true;
        }
        /// <summary>
        /// metoda per heqjen e nje obj clsZbritjeAnalitike ne nje arraylist
        /// </summary>
        public bool fshiGjitheZbritjetAnalitike()
        {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// metoda per heqjen e nje obj clsZbritjeAnalitike ne nje arraylist
        /// </summary>
        public void fshiKeteZbritjeAnalitike(int index)
        {
            base.RemoveAt(index);
        }
        /// <summary>
        /// metoda shton nje obj te ri ne array list ne pozicionin e percaktuar nga index-i
        /// </summary>
        public void shtoZbritjeAnalitikenNeIndeksin(int index, clsZbritjeAnalitike zbritjeAnalitike)
        {
            base.Insert(index, zbritjeAnalitike);
        }
        /// <summary>
        /// metoda gjen index-in ne liste ne te cilin ndodhet nje obj i caktuar
        /// </summary>
        public int indeksiZbritjeAnalitikes(clsZbritjeAnalitike zbritjeAnalitike)
        {
            return base.IndexOf(zbritjeAnalitike);
        }
        /// <summary>
        /// metoda kontrollon nqs nje obj ndodhet ne arraylist
        /// </summary>
        public bool ekzistonZbritjeAnalitike(clsZbritjeAnalitike zbritjeAnalitike)
        {
            if (base.Contains(zbritjeAnalitike))
                return true;
            else return false;
        }
        /// <summary>
        /// metoda qe kthen nr e obj qe ka arraylist
        /// </summary>
        public int numriZbritjeveAnalitike()
        {
            return base.Count;
        }

        public static DataTable ktheZbritjeAnalitikeDtExport(int idNdermarrje)
        {
            using (clsDatabaseInventari db = new clsDatabaseInventari())
            {
                return db.ktheZbritjeAnalitikeDtExport(idNdermarrje);
            }
        }

        public static clsMesazh RuajRreshtaTeModifikuar(IEnumerable<clsZbritjeAnalitike> zbritjetPerTuRuajtur)
        {
            clsMesazh mesazh;
            mesazh = RuajTeGjitheMeDT(zbritjetPerTuRuajtur);
            return mesazh;
        }

        public static clsMesazh RuajTeGjitheMeDT(IEnumerable<clsZbritjeAnalitike> zbritjetPerTuRuajtur)
        {

            try
            {
                using (var scope=new MyTransactionScope())
                {
                    var dt = zbritjetPerTuRuajtur.ToDataTable("IdZbritjeAnalitike", "IdArtikulli", "IdNivelZbritje", "IdNjesia", "DateFillimi", "DateMbarimi", "SasiMin", "SasiMax", "VleftaMin", "VleftaMax", "LlojZbritje", "Zbritja", "IdPerdoruesi", "IdNdermarje", "IdKonfig", "IdStatusDok", "IdNjesia2", "Zbritja2");

                    clsDatabaseInventari dbInventar = new clsDatabaseInventari();
                    dbInventar.ruajZbritjeAnalitikeDT(dt);
                    scope.Complete();

                    return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
                }
            }
            catch (Exception ex)
            {
                return new clsMesazh(false, ex.Message);
            }

        }
        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbInventari.clsZbritjeAnalitike"/> 
        /// </summary>
        private bool mbushZbritjeAnalitike(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsZbritjeAnalitike zbritjeAnalitike = new clsZbritjeAnalitike();
                    //zbritjeAnalitike.mbushZbritjeAnalitike(rreshti);
                    this.Add(new clsZbritjeAnalitike(rreshti));
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }

        #endregion
        [Obsolete("Perdor: bool mbushZbritjeAnalitike(DataTable dt)", true)]
        public colZbritjetAnalitike mbushArrayListZbritjeshAnalitike(DataSet ds)
        {
            colZbritjetAnalitike zbritjetAnalitike = new colZbritjetAnalitike();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsZbritjeAnalitike zbritjeAnalitike = new clsZbritjeAnalitike();
                zbritjeAnalitike.IdZbritjeAnalitike = int.Parse(rreshti[0].ToString());
                zbritjeAnalitike.IdArtikulli = int.Parse(rreshti[1].ToString());
                zbritjeAnalitike.IdNivelZbritje = int.Parse(rreshti[2].ToString());
                zbritjeAnalitike.IdNjesia = int.Parse(rreshti[3].ToString());
               
                zbritjeAnalitike.DateFillimi = DateTime.Parse(rreshti[4].ToString());
                zbritjeAnalitike.DateMbarimi = DateTime.Parse(rreshti[5].ToString());
                zbritjeAnalitike.SasiMin = decimal.Parse(rreshti[6].ToString());
                zbritjeAnalitike.SasiMax = decimal.Parse(rreshti[7].ToString());
                zbritjeAnalitike.VleftaMin = decimal.Parse(rreshti[8].ToString());
                zbritjeAnalitike.VleftaMax = decimal.Parse(rreshti[9].ToString());
                zbritjeAnalitike.LlojZbritje = int.Parse(rreshti[10].ToString());
                zbritjeAnalitike.Zbritja = decimal.Parse(rreshti[11].ToString());

              
                zbritjeAnalitike.IdPerdoruesi = int.Parse(rreshti[12].ToString());
                //zbritjeAnalitike.IdNderViti = int.Parse(rreshti[13].ToString());
                zbritjeAnalitike.IdNdermarje = int.Parse(rreshti[13].ToString());
                zbritjeAnalitike.IdKonfig = int.Parse(rreshti[14].ToString());
                zbritjeAnalitike.KodbarArtikulli = rreshti[15].ToString();
                zbritjeAnalitike.EmerArtikulli1 = rreshti[16].ToString();
                zbritjeAnalitike.EmerArtikulli2 = rreshti[17].ToString();
                zbritjeAnalitike.KodifikimArtikulli1 = rreshti[18].ToString();
                zbritjeAnalitike.KodifikimArtikulli2 = rreshti[19].ToString();
                zbritjeAnalitike.NrLlogariFurnitori = rreshti[20].ToString();
                zbritjeAnalitike.KodArtikulli = rreshti[21].ToString();
                zbritjetAnalitike.Add(zbritjeAnalitike);
            }
            return zbritjetAnalitike;
        }
    }
}
