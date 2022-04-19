using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using DbCore.DbKontabiliteti;
using DbCore.IMBUtils.Extensions;

namespace DbCore.DbInventari
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsCmimArtikulli
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colCmimeArtikujsh : System.Collections.Generic.List<clsCmimArtikulli>
    {

        public colCmimeArtikujsh()
        {

        } 
        public colCmimeArtikujsh(int idNdermarrje, int idPerdoruesi, int shitjeApoBlerje, bool llogaritKosto, bool vetemDetajimet, int idNivelCmimi) : base(new clsDatabaseInventari().merrCmimetAllPaFiltra(idNdermarrje, idPerdoruesi, shitjeApoBlerje, llogaritKosto ? 1 : 0, vetemDetajimet, idNivelCmimi))
        {

        }

        public colCmimeArtikujsh(IEnumerable<clsCmimArtikulli> collection) : base(collection)
        {
        }


        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbInventari.clsCmimArtikulli"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsCmimArtikulli this[int index]
        {
            get { return ((clsCmimArtikulli)base[index]); }
        }

        /// <summary>
        /// metoda per shtimin e nje obj clsCmimArtikulli ne nje arraylist
        /// </summary>
        public bool shtoCmimArtikull(clsCmimArtikulli cmimArtikulli)
        {
            base.Add(cmimArtikulli);
            if (base.Contains(cmimArtikulli))
                return true;
            else return false;
        }
        /// <summary>
        /// metoda per heqjen e nje obj clsCmimArtikulli ne nje arraylist
        /// </summary>
        public bool fshiCmimArtikull(clsCmimArtikulli cmimArtikulli)
        {
            base.Remove(cmimArtikulli);
            if (base.Contains(cmimArtikulli))
                return false;
            else return true;
        }
        /// <summary>
        /// metoda per heqjen e nje obj clsCmimArtikulli ne nje arraylist
        /// </summary>
        public bool fshiGjitheCmimArtikujt()
        {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// metoda per heqjen e nje obj clsCmimArtikulli ne nje arraylist
        /// </summary>
        public void fshiKeteCmimArtikull(int index)
        {
            base.RemoveAt(index);
        }
        /// <summary>
        /// metoda shton nje obj te ri ne array list ne pozicionin e percaktuar nga index-i
        /// </summary>
        public void shtoCmimArtikullinNeIndeksin(int index, clsCmimArtikulli cmimArtikulli)
        {
            base.Insert(index, cmimArtikulli);
        }
        /// <summary>
        /// metoda gjen index-in ne liste ne te cilin ndodhet nje obj i caktuar
        /// </summary>
        public int indeksiCmimArtikullit(clsCmimArtikulli cmimArtikulli)
        {
            return base.IndexOf(cmimArtikulli);
        }

        public static DataTable KtheCmimArtikujshSipasNivelit(string artIds, int idNivelCmimi)
        {
            using(clsDatabaseInventari dbCmimArtikulli = new clsDatabaseInventari())
            {
                return dbCmimArtikulli.KtheCmimArtikujshSipasNivelit(artIds, idNivelCmimi);
            }
        }

        /// <summary>
        /// metoda kontrollon nqs nje obj ndodhet ne arraylist
        /// </summary>
        public bool ekzistonCmimArtikull(clsCmimArtikulli cmimArtikulli)
        {
            if (base.Contains(cmimArtikulli))
                return true;
            else return false;
        }

        /// <summary>
        /// metoda qe kthen nr e obj qe ka arraylist
        /// </summary>
        public int numriCmimArtikujve()
        {
            return base.Count;
        }

        /// <summary>
        /// mbush cmimet e artikujve me kete id
        /// </summary>
        /// <param name="idCmimArtikulli">id e artikullit</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushCmimArtikulli(int idCmimArtikulli)
        {
            clsDatabaseInventari dbCmimArtikulli = new clsDatabaseInventari();
            bool sukses = mbushCmimeArtikujsh(dbCmimArtikulli.ktheCmimArtikulli(idCmimArtikulli));
            dbCmimArtikulli.Dispose();
            return sukses;
        }

        /// <summary>
        /// mbush cmimet e artikujve sipas filtrit te dhene
        /// </summary>
        /// <param name="kodartikulli">kod artikulli</param>
        /// <param name="kodbar">kodibar</param>
        /// <param name="pershkrimi1">pershkrimi i pare</param>
        /// <param name="pershkrimi2">pershkrimi i dyte</param>
        /// <param name="kodifikimi1">kodifikimi i pare</param>
        /// <param name="kodifikimi2">kodifikimi i dyte</param>
        /// <param name="furnitori">furnitori</param>
        /// <param name="njesia">njesia e artikullit</param>
        /// <param name="datafillimit">data e fillimit</param>
        /// <param name="datambarimit">data e mbarimit</param>
        /// <param name="idndervit">id e ndermarrjes sipas vitit</param>
        /// <param name="idnivelcmimi">id e nivelit te cmimit</param>
        /// <param name="idperdorues">id e perdoruesit</param>
        /// <param name="idndermarje">id e ndermarrjes</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        //public static DataTable mbushCmimArtikulliSipasFiltrit(string kodartikulli, string kodbar, string pershkrimi1, string pershkrimi2, string kodifikimi1, string kodifikimi2, string furnitori, string njesia, string datafillimit, string datambarimit, int idnivelcmimi, int idperdorues, int idndermarje, int shitjeblerje, string llogaritkosto)
        //{
        //    clsDatabaseInventari dbCmimArtikulli = new clsDatabaseInventari();
        //    DataTable tabela = dbCmimArtikulli.ktheCmimArtikulliSipasFiltritAll(kodartikulli, kodbar, pershkrimi1, pershkrimi2, kodifikimi1, kodifikimi2, furnitori, njesia, datafillimit, datambarimit, idnivelcmimi, idperdorues, idndermarje, shitjeblerje, llogaritkosto);
        //    dbCmimArtikulli.Dispose();
        //    return tabela;
        //}


        /// <summary>
        /// mbush cmimet e artikujve sipas nivelit te cmimit
        /// </summary>
        /// <param name="kodartikulli">kod i artikullit</param>
        /// <param name="idnivelcmimi">id e nivelit te cmimit</param>
        /// <param name="idperdorues">id e perdoruesit</param>
        /// <param name="idndermarje">id e ndermarrjes</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        public bool mbushCmimArtikulliSipasNivelit(int idartikulli, string idnivelcmimi, int idperdorues, int idndermarje,int iddetajim=0)
        {
            clsDatabaseInventari dbCmimArtikulli = new clsDatabaseInventari();
            bool sukses = mbushCmimeArtikujsh(dbCmimArtikulli.ktheCmimArtikulliSipasNivelit(idartikulli, idnivelcmimi, idperdorues, idndermarje,iddetajim));
            dbCmimArtikulli.Dispose();
            return sukses;
        }

        public bool mbushCmimArtikulliSipasNivelitMeDhePaDetajim(string kodartikulli, string idnivelcmimi, int idperdorues, int idndermarje, clsDatabaseInventari dbCmimArtikulli, int iddetajim)
        {
            bool sukses = mbushCmimeArtikujsh(dbCmimArtikulli.ktheCmimArtikulliSipasNivelitMeDhePaDetajim(kodartikulli, idnivelcmimi, idperdorues, idndermarje, iddetajim));
            return sukses;
        }

        public bool mbushCmimArtikulliSipasArtikullit(int idartikulli, int idndermarje)//per t'u bere me kosto
        {
            clsDatabaseInventari dbCmimArtikulli = new clsDatabaseInventari();
            bool sukses = mbushCmimeArtikujsh(dbCmimArtikulli.ktheCmimArtikulliSipasArtikullit(idartikulli, idndermarje));
            dbCmimArtikulli.Dispose();
            return sukses;
        }

        public bool mbushCmimArtikulliSipasArtikullit(int idartikulli, int idndermarje, clsDatabaseInventari dbCmimArtikulli)//per t'u bere me kosto
        {

            bool sukses = mbushCmimeArtikujsh(dbCmimArtikulli.ktheCmimArtikulliSipasArtikullit(idartikulli, idndermarje));

            return sukses;
        }
        public bool mbushCmimArtikulliSipasArtikullitMeKostoMeAutorizime(int idArtikulli, int idNdermarje, int idPerdorues, bool meCmimeBlerje)
        {
            return meCmimeBlerje ? mbushCmimArtikulliShitjeDheBlerjeSipasArtikullitMeKostoMeAutorizime(idArtikulli, idNdermarje, idPerdorues)
                : mbushCmimArtikulliSipasArtikullitMeKostoMeAutorizime(idArtikulli, idNdermarje, idPerdorues);
        }
        public bool mbushCmimArtikulliSipasArtikullitMeKostoMeAutorizime(int idArtikulli, int idNdermarje, int idPerdorues)
        {
            clsDatabaseInventari dbCmimArtikulli = new clsDatabaseInventari();
            bool sukses = mbushCmimeArtikujshMeKosto(dbCmimArtikulli.ktheCmimArtikulliSipasArtikullitMeKostoMeAutorizime(idArtikulli, idNdermarje, idPerdorues));
            dbCmimArtikulli.Dispose();
            return sukses;
        }

        public bool mbushCmimArtikulliShitjeDheBlerjeSipasArtikullitMeKostoMeAutorizime(int idArtikulli, int idNdermarje, int idPerdorues)
        {
            clsDatabaseInventari dbCmimArtikulli = new clsDatabaseInventari();
            bool sukses = mbushCmimeArtikujshMeKosto(dbCmimArtikulli.ktheCmimArtikulliShitjeDheBlerjeSipasArtikullitMeKostoMeAutorizime(idArtikulli, idNdermarje, idPerdorues));
            dbCmimArtikulli.Dispose();
            return sukses;
        }
        public static DataTable ktheCmimArtikulliDtExport(int idndermarje)
        {
            using (clsDatabaseInventari db = new clsDatabaseInventari())
            {
                return db.ktheCmimArtikulliDtExport(idndermarje);
            }
        }

        public  void ShtoCmimeRetailNeKoleksion(colCmimeArtikujsh colAllCmimet )
        {
            colCmimeArtikujsh colCmimetRetail = new colCmimeArtikujsh();
            var clsretail = colAllCmimet.Find(x => x.IdCmimRetail > 0);
            if (clsretail == null || clsretail.IdCmimRetail == 0)
                return;

            foreach (clsCmimArtikulli cmimemodifikuara in this)
            {
                var rows = colAllCmimet.AsEnumerable()
                        .Where(r => r.IdNivelCmimi != cmimemodifikuara.IdNivelCmimi && r.IdCmimRetail == cmimemodifikuara.IdNivelCmimi && r.IdCmimArtikulli == 0 && r.Cmimi == 0  && r.IdArtikulli == cmimemodifikuara.IdArtikulli)
                        .ToList();
                if (rows.Count > 0)
                {
                    rows.ForEach(x => {
                        x.Cmimi = cmimemodifikuara.Cmimi;
                        x.IdKonfig = cmimemodifikuara.IdKonfig;
                        x.IdNjesia = cmimemodifikuara.IdNjesia;
                        x.IdNjesia2 = cmimemodifikuara.IdNjesia2;
                        x.IdStatusDok = 1;
                        x.IdPerdoruesi = cmimemodifikuara.IdPerdoruesi;
                        x.IdNdermarje = cmimemodifikuara.IdNdermarje;
                        x.Cmimi2 = cmimemodifikuara.Cmimi2;
                        x.CmimiTvsh = cmimemodifikuara.CmimiTvsh;
                        x.Cmimi2Tvsh = cmimemodifikuara.Cmimi2Tvsh;
                        x.SasiMin = cmimemodifikuara.SasiMin;
                        x.SasiMax = cmimemodifikuara.SasiMax;
                    });
                    colCmimetRetail.AddRange(rows);
                }
            }
 
            this.AddRange(colCmimetRetail);
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbInventari.clsCmimArtikulli"/> 
        /// </summary>
        public bool mbushCmimeArtikujsh(DataTable dt)
        {
            foreach (DataRow rreshti in dt.Rows)
            {
                //clsCmimArtikulli cmimArtikullit = new clsCmimArtikulli();
                //cmimArtikullit.mbushCmimArtikulli(rreshti);
                this.Add(new clsCmimArtikulli(rreshti));
            }
            return true;
        }

        public bool mbushCmimeArtikujshMeKosto(DataTable dt)
        {

            foreach (DataRow rreshti in dt.Rows)
            {
                clsCmimArtikulli cmimArtikullit = new clsCmimArtikulli();
                cmimArtikullit.mbushCmimArtikulliMeKosto(rreshti);
                this.Add(cmimArtikullit);
            }
            return true;
        }

        #endregion

        public static DataTable merrCmimetAllPaFiltraDT(int idNdermarrje, int idPerdoruesi, int shitjeApoBlerje, bool llogaritKosto, bool vetemDetajimet)
        {
            using (var db = new clsDatabaseInventari())
                return db.merrCmimetAllPaFiltraDT(idNdermarrje, idPerdoruesi, shitjeApoBlerje, llogaritKosto ? 1 : 0, vetemDetajimet);
        }

        public static clsMesazh RuajRreshtaTeModifikuar(IEnumerable<clsCmimArtikulli> cmimetPerTuRuajtur)
        {
            clsMesazh mesazh;
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            using (var scope = new MyTransactionScope())
            {
                mesazh = RuajTeGjitheMeDT(cmimetPerTuRuajtur);
                if(mesazh)
                    scope.Complete();
            }
            System.Diagnostics.Trace.Write(watch.ElapsedMilliseconds);
            return mesazh;
        }

        private static clsMesazh RuajNjePerNje(IEnumerable<clsCmimArtikulli> cmimetPerTuRuajtur)
        {

            clsDatabaseInventari dbInventar = new clsDatabaseInventari();
            clsMesazh mesazh;
            try
            {
                dbInventar.beginTransaksion();
                foreach (clsCmimArtikulli c in cmimetPerTuRuajtur)
                {
                    int idcmim = 0;
                    if (!dbInventar.ekzistonCmimArtikulli(c.IdArtikulli, c.IdNivelCmimi, out idcmim))
                    {
                        int idC;
                        mesazh = dbInventar.ruajCmimArtikulli(out idC, c.IdArtikulli, c.IdNivelCmimi, c.IdNjesia, c.IdMonedha, c.DateFillimi, c.DateMbarimi, c.SasiMin,
                            c.SasiMax, c.Cmimi, c.IdPerdoruesi, c.IdNdermarje, c.IdKonfig, c.IdStatusDok, c.IdNjesia2, c.Cmimi2, c.KoheFillimi, c.KoheMbarimi,c.IdDetajim);//, c.formula
                        if (!mesazh.Status)
                        {
                            dbInventar.rollbackTransaksion();
                            return mesazh;
                        }
                    }
                    else
                    {
                        mesazh = dbInventar.modifikoCmimArtikulli(idcmim, c.IdArtikulli, c.IdNivelCmimi, c.IdNjesia, c.IdMonedha, c.DateFillimi, c.DateMbarimi, c.SasiMin,
                            c.SasiMax, c.Cmimi, c.IdPerdoruesi, c.IdNdermarje, c.IdKonfig, c.IdStatusDok, c.IdNjesia2, c.Cmimi2, c.KoheFillimi, c.KoheMbarimi, c.IdDetajim);//, c.formula
                        if (!mesazh.Status)
                        {
                            dbInventar.rollbackTransaksion();
                            return mesazh;
                        }
                    }
                }
                dbInventar.commitTransaksion();

             mesazh = new clsMesazh(true, IMBUtils.Messages.MessagesResource.Messages["labelRaportMesazhRuajtjaPerfundoiSukses"]);
             
                return mesazh;
            }
            catch (Exception ce)
            {
                dbInventar.rollbackTransaksion();
                return new clsMesazh(false, ce.Message);
            }
        }

        public static clsMesazh RuajTeGjitheMeDT(IEnumerable<clsCmimArtikulli> cmimetPerTuRuajtur)
        {
            
            try
            {
                
                var dt = cmimetPerTuRuajtur.ToDataTable("IdCmimArtikulli", "IdArtikulli", "IdNivelCmimi", "IdNjesia", "IdMonedha", "DateFillimi", "DateMbarimi", "SasiMin", "SasiMax", "Cmimi", "IdPerdoruesi", "IdNdermarje", "IdKonfig", "IdStatusDok", "IdNjesia2", "Cmimi2", "KoheFillimi", "KoheMbarimi","IdTvsh","IdDetajim");
                   
                clsDatabaseInventari dbInventar = new clsDatabaseInventari();
                dbInventar.ruajCmimArtikulliDT(dt);
                   
                return new clsMesazh(true, IMBUtils.Messages.MessagesResource.Messages["labelRaportMesazhRuajtjaPerfundoiSukses"]);


            }
            catch (Exception ex)
            {
                return new clsMesazh(false, ex.Message);
            }

        }
    }
}