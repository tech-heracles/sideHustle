using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;


namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsNivelRegjistrimi
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colNivelRegjistrimi : System.Collections.Generic.List<clsNivelRegjistrimi>
    {
        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbRegjistrim.clsNivelRegjistrimi"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsNivelRegjistrimi this[int index]
        {
            get { return ((clsNivelRegjistrimi)base[index]); }
        }

        /// <summary>
        /// metoda per shtimin e nje obj clsNivelRegjistrimi ne nje arraylist
        /// </summary>
        public bool shtoNivelRegjistrimi(clsNivelRegjistrimi regj)
        {
            base.Add(regj);
            if (base.Contains(regj))
                return true;
            else return false;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsNivelRegjistrimi ne nje arraylist
        /// </summary>
        public bool fshiNivelRegjistrimi(clsNivelRegjistrimi regj)
        {
            base.Remove(regj);
            if (base.Contains(regj))
                return false;
            else return true;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsNivelRegjistrimi ne nje arraylist
        /// </summary>
        public bool fshiGjitheNivelRegjistrimi()
        {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsNivelRegjistrimi ne nje arraylist
        /// </summary>
        public void fshiKeteNivelRegjistrimi(int index)
        {
            base.RemoveAt(index);
        }

        /// <summary>
        /// metoda shton nje obj te ri ne array list ne pozicionin e percaktuar nga index-i
        /// </summary>
        public void shtoNivelRegjistrimiNeIndeksin(int index, clsNivelRegjistrimi regj)
        {
            base.Insert(index, regj);
        }

        /// <summary>
        /// metoda gjen index-in ne liste ne te cilin ndodhet nje obj i caktuar
        /// </summary>
        public int indeksiNivelRegjistrimi(clsNivelRegjistrimi regj)
        {
            return base.IndexOf(regj);
        }

        /// <summary>
        /// metoda kontrollon nqs nje obj ndodhet ne arraylist
        /// </summary>
        public bool ekzistonNivelRegjistrimi(clsNivelRegjistrimi regj)
        {
            if (base.Contains(regj))
                return true;
            else return false;
        }

        /// <summary>
        /// metoda qe kthen nr e obj qe ka arraylist
        /// </summary>
        public int numriNivelRegjistrimi()
        {
            return base.Count;
        }



        /// <summary>
        /// mbush gjithe nivelet e regjistrimit
        /// </summary>
        /// <param name="idNderm">id e ndermarrjes</param>
        /// <param name="idPerdorues">id e perdoruesit</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        public bool mbushGjitheNivelRegjistrimi(int idNderm, int idPerdorues)
        {
            clsDatabaseRegjistrim dbNivelRegj = new clsDatabaseRegjistrim();
            bool mbush = mbushNivelRegjistrimiPaKonvertime(dbNivelRegj.ktheGjitheNivelRegjistrimi(idNderm, idPerdorues));
            dbNivelRegj.Dispose();
            return mbush;
        }
        public bool mbushNivelRegjistrimiMeKonvertime(int idNderm, int idPerdorues)
        {
            clsDatabaseRegjistrim dbNivelRegj = new clsDatabaseRegjistrim();
            bool mbush = mbushNivelRegjistrimiMeKonvertime(dbNivelRegj.ktheGjitheNivelRegjistrimi(idNderm, idPerdorues));
            dbNivelRegj.Dispose();
            return mbush;
        }
        public bool mbushGjitheNivelRegjistrimiSipasSuperKat(int idNderm, int idPerdorues, int idsuperkat)
        {
            using (clsDatabaseRegjistrim dbNivelRegj = new clsDatabaseRegjistrim())
            {
                bool mbush = mbushNivelRegjistrimiPaKonvertime(dbNivelRegj.ktheGjitheNivelRegjistrimiSipasSuperKategorise(idNderm, idPerdorues, idsuperkat));
                return mbush;
            }
        }
        /// <summary>
        /// mbush gjiht nvielet e regjistimit sipas kategorise
        /// </summary>
        /// <param name="idKat">id e kategorise</param>
        /// <param name="idNderm">id e ndermarrjes</param>
        /// <param name="idPerdorues">id e perdoruesit</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        public bool mbushGjitheNivelRegjistrimiSipasKategoriPlus(int idKat, int idNderm, int idPerdorues)
        {
            clsDatabaseRegjistrim dbNivelRegj = new clsDatabaseRegjistrim();
            bool mbush = mbushNivelRegjistrimiPaKonvertime(dbNivelRegj.ktheGjitheNivelRegjistrimiSipasKategoriPlus(idKat, idNderm, idPerdorues));
            dbNivelRegj.Dispose();
            return mbush;
        }

        /// <summary>
        /// mbush gjithe nivelet e regjistrimit sipas kategorise
        /// </summary>
        /// <param name="idkateg">id e kategorise</param>
        /// <param name="idNderm">id e ndermarrjes</param>
        /// <param name="idPerdorues">id e perdoruesit</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        public bool mbushGjitheNivelRegjistrimiSipasKategoriMeKonvertime(int idkateg, int idNderm, int idPerdorues)
        {
            clsDatabaseRegjistrim dbNivelRegj = new clsDatabaseRegjistrim();
            bool mbush = mbushNivelRegjistrimiMeKonvertime(dbNivelRegj.ktheGjitheNivelRegjistrimiSipasKategori(idkateg, idNderm, idPerdorues));
            dbNivelRegj.Dispose();
            return mbush;
        }
        /// <summary>
        /// mbush gjithe nivelet e regjistrimit sipas kategorise pa mbushur koleksionin e konvertimeve
        /// </summary>
        /// <param name="idkateg">id e kategorise</param>
        /// <param name="idNderm">id e ndermarrjes</param>
        /// <param name="idPerdorues">id e perdoruesit</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        public bool mbushGjitheNivelRegjistrimiSipasKategoriPaKonvertime(int idkateg, int idNderm, int idPerdorues)
        {
            clsDatabaseRegjistrim dbNivelRegj = new clsDatabaseRegjistrim();
            bool mbush = mbushNivelRegjistrimiPaKonvertime(dbNivelRegj.ktheGjitheNivelRegjistrimiSipasKategori(idkateg, idNderm, idPerdorues));
            dbNivelRegj.Dispose();
            return mbush;
        }

        /// <summary>
        /// kthen gjithe nivelet e regjistrimit sipas kategorise
        /// </summary>
        /// <param name="idkateg"></param>
        /// <param name="idNderm"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="shtoRreshtBosh">True ne se do ti shtohet rreshti bosh (psh te filtri grides), false perndryshe</param>
        /// <returns></returns>
        public static DataView ktheGjitheNivelRegjistrimiSipasKategoriDtCombo(int idkateg, int idNderm, int idPerdorues, bool shtoRreshtBosh)
        {
            using (clsDatabaseRegjistrim dbNivelRegj = new clsDatabaseRegjistrim())
            {
                DataTable nivele = dbNivelRegj.ktheGjitheNivelRegjistrimiSipasKategoriSmall(idkateg, idNderm, idPerdorues);
                if (shtoRreshtBosh)
                    nivele.Rows.InsertAt(nivele.NewRow(), 0);
                return new DataView(nivele, "", "RADHA asc", DataViewRowState.CurrentRows);
            }
        }

        public static DataView ktheGjitheNivelRegjistrimiSipasKategoriDtComboSipasTeDrejtave(int idkateg, int idNderm, int idPerdorues, int idViti, string komponente, bool shtoRreshtBosh)
        {
            using (clsDatabaseRegjistrim dbNivelRegj = new clsDatabaseRegjistrim())
            {
                DataTable nivele = dbNivelRegj.ktheGjitheNivelRegjistrimiSipasKategoriSmallSipasTeDrejtave(idkateg, idNderm, idPerdorues, idViti, komponente);
                if (shtoRreshtBosh)
                    nivele.Rows.InsertAt(nivele.NewRow(), 0);
                return new DataView(nivele, "", "RADHA asc", DataViewRowState.CurrentRows);
            }
        }

        /// <summary>
        /// kthen gjithe nivelet e regjistrimit sipas kategorise
        /// </summary>
        /// <param name="idkateg"></param>
        /// <param name="idNderm"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="shtoRreshtBosh">True ne se do ti shtohet rreshti bosh (psh te filtri grides), false perndryshe</param>
        /// <returns></returns>
        public static DataView ktheGjitheNivelRegjistrimiSipasKategoriQeKaneKonfigAmbjenteshDtCombo(int idkateg, int idNderm, int idPerdorues, bool shtoRreshtBosh, string lloji, bool kushtVartes)
        {
            using (clsDatabaseRegjistrim dbNivelRegj = new clsDatabaseRegjistrim())
            {
                DataTable nivele = dbNivelRegj.ktheGjitheNivelRegjistrimiSipasKategoriQeKaneKonfigAmbSmall(idkateg, idNderm, idPerdorues, lloji, kushtVartes);
                if (shtoRreshtBosh)
                    nivele.Rows.InsertAt(nivele.NewRow(), 0);
                return new DataView(nivele, "", "RADHA asc", DataViewRowState.CurrentRows);
            }
        }

        /// <summary>
        /// merr te gjithe nivelet sipas idndermarjes dhe id perdoruesi nga tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.ktheGjitheNivelRegjistrimi"/> 
        /// </summary>
        /// <param name="idNd"> id e ndermarjes</param>
        /// <param name="user"> id e perdoruesit</param>
        /// <returns > nje object colNivelRegjistrimi me te gjithe nivelet e regjistrimit te ndermarjes</returns>
        public static DataView ktheGjitheNivelRegjistrimi(int idNd, int user, bool shtoRreshtBosh)
        {
            //clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            //return data.merrGjitheNivelRegjistrimi(idNd, user);
            using (clsDatabaseRegjistrim data = new clsDatabaseRegjistrim())
            {
                DataTable nivele = data.ktheGjitheNivelRegjistrimiSmall(idNd, user);
                if (shtoRreshtBosh)
                    nivele.Rows.Add(-1, "", "", 0);
                return new DataView(nivele, "", "RADHA asc", DataViewRowState.CurrentRows);
            }
        }

        /// <summary>
        /// mbush gjithe nivelet e regjistrimit te gjeneruar ne kontabilitet
        /// </summary>
        /// <param name="idNderm">id e ndermarrjes</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        public bool mbushGjitheNivelRegjistrimiGjeneruarKont(int idNderm)
        {
            clsDatabaseRegjistrim dbNivelRegj = new clsDatabaseRegjistrim();
            bool mbush = mbushNivelRegjistrimiPaKonvertime(dbNivelRegj.ktheGjitheNivelRegjistrimiGjeneruarKont(idNderm));
            dbNivelRegj.Dispose();
            return mbush;
        }

        /// <summary>
        /// mbush gjithe nivelet e regjistrimit sipas kategorise 
        /// </summary>
        /// <param name="idKat">id e katergorise</param>
        /// <param name="idNderm">id e ndermarrjes</param>
        /// <param name="idPerdorues">id e perdoruesit</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        public bool mbushGjitheNivelRegjistrimiSipasKategoriPaAll(int idKat, int idNderm, int idPerdorues)
        {
            clsDatabaseRegjistrim dbNivelRegj = new clsDatabaseRegjistrim();
            bool mbush = mbushNivelRegjistrimiPaKonvertime(dbNivelRegj.ktheGjitheNivelRegjistrimiSipasKategoriPaAll(idKat, idNderm, idPerdorues));
            dbNivelRegj.Dispose();
            return mbush;
        }

        /// <summary>
        /// mbush gjithe niveletRegjistrimit sipas kodeve
        /// </summary>
        /// <param name="parameter">parametrat</param>
        /// <param name="idNderm">id e ndermarrjes</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        public bool mbushDataTableNivelRegjistrimiSipasKodeve(string parameter, string idNderm)
        {
            clsDatabaseRegjistrim dbNivelRegj = new clsDatabaseRegjistrim();
            bool mbush = mbushNivelRegjistrimiPaKonvertime(dbNivelRegj.ktheDataTableNivelRegjistrimiSipasKodeve(parameter, idNderm));
            dbNivelRegj.Dispose();
            return mbush;
        }

        /// <summary>
        /// mbush nivelet e regjistrimit
        /// </summary>
        /// <param name="idKategori">id e kategorise</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        public bool mbushNivelRegjistrimi(int idKategori, int idndermarje)
        {
            clsDatabaseRegjistrim dbNivelRegj = new clsDatabaseRegjistrim();
            bool mbush = mbushNivelRegjistrimiMeKonvertime(dbNivelRegj.ktheNivelRegjistrimi(idKategori, idndermarje));
            dbNivelRegj.Dispose();
            return mbush;
        }

        /// <summary>
        /// mbush konvertimet e nivelit
        /// </summary>
        /// <param name="idNivel">id e nivelit</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        public bool mbushKonvertimeNiveli(int idNivel)
        {
            clsDatabaseRegjistrim dbNivelRegj = new clsDatabaseRegjistrim();
            bool mbush = mbushNivelRegjistrimiPaKonvertime(dbNivelRegj.ktheKonvertimeNiveli(idNivel));
            dbNivelRegj.Dispose();
            return mbush;
        }
        /// <summary>
        /// mbush konvertimet e nivelit
        /// </summary>
        /// <param name="idNivel">id e nivelit</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        public bool mbushKonvertimeNiveliNew(int idNivel)
        {
            clsDatabaseRegjistrim dbNivelRegj = new clsDatabaseRegjistrim();
            bool mbush = mbushNivelRegjistrimiNew(dbNivelRegj.ktheKonvertimeNiveliNew(idNivel));
            dbNivelRegj.Dispose();
            return mbush;
        }
   public bool mbushKonvertimeNiveli(int idNivel, int idperdoruesi)
        {
            clsDatabaseRegjistrim dbNivelRegj = new clsDatabaseRegjistrim();
            bool mbush = mbushNivelRegjistrimiPaKonvertime(dbNivelRegj.ktheKonvertimeNiveli(idNivel, idperdoruesi));
            dbNivelRegj.Dispose();
            return mbush;
        }
        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbRegjistrim.clsNivelRegjistrimi"/> 
        /// </summary>
        private bool mbushNivelRegjistrimiMeKonvertime(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsNivelRegjistrimi regj = new clsNivelRegjistrimi();
                    //regj.mbushNivelRegjMeKonvertime(rreshti);
                    Add(new clsNivelRegjistrimi(rreshti));
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //    //throw;
            //}
            return true;
        }
        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbRegjistrim.clsNivelRegjistrimi"/> 
        /// </summary>
        private bool mbushNivelRegjistrimiPaKonvertime(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    clsNivelRegjistrimi regj = new clsNivelRegjistrimi();
                    regj.mbushNivelRegjPaKonvertime(rreshti);
                    Add(regj);
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //    //throw;
            //}
            return true;
        }
        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbRegjistrim.clsNivelRegjistrimi"/> 
        /// </summary>
        private bool mbushNivelRegjistrimiNew(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    clsNivelRegjistrimi regj = new clsNivelRegjistrimi();
                    regj.mbushNivelRegjNew(rreshti);
                    Add(regj);
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
    }
}
