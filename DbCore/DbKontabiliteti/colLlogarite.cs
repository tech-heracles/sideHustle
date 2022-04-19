using System;
using System.Collections.Generic;
using System.Data;

namespace DbCore.DbKontabiliteti
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsLlogari
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colLlogarite : List<clsLlogari>
    {
        #region Konstruktoret

        public colLlogarite()
        {

        }
        //public colLlogarite(int capacity)
        //    : base(capacity)
        //{

        //}
        public colLlogarite(IEnumerable<clsLlogari> collection)
            : base(collection)
        {

        }
        public colLlogarite(int idndermarje, clsDatabaseKontabilitet db)
        {
            mbushLlogarite(db.ktheLlogariTeArdhuraDheShpenzime(idndermarje));
        }
        public colLlogarite(int  idKokeShitje)
        {
            if (idKokeShitje>0)
            using (clsDatabaseKontabilitet db = new clsDatabaseKontabilitet())
            {
                mbushLlogarite(db.merrLlogariFature(idKokeShitje));
            }         
        }
        public colLlogarite(List<int> idLlogarish)
        {
            if (idLlogarish.Count <= 0)
                return;
            using (clsDatabaseKontabilitet db = new clsDatabaseKontabilitet())
            {
                mbushLlogarite(db.merrLlogariSipasIdve(idLlogarish));
            }
        }

        public static object merrNrLlogariSipasIdLlogarive(List <int> idLlogarish)
        {
           
            using (clsDatabaseKontabilitet db = new clsDatabaseKontabilitet())
            {
               return db.merrNrLlogariSipasIdLlogarive(idLlogarish);
            }
        }

        public colLlogarite(int idNdermarrje,bool perQK, clsDatabaseKontabilitet db) 
        {
            if(perQK)
            mbushLlogarite(db.TransCache.GetLlogariTeMundshmePerQk(idNdermarrje,db));
        }
        #endregion

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbKontabiliteti.clsLlogari"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        #region Metoda Publike

        public new clsLlogari this[int index]
        {
            get { return ((clsLlogari)base[index]); }
        }        
        public static DataRow merrSipasLlogariteNdermarrjesAndAutorizimeDR(int idnderm, int idperdorues, int idllogari, int idGjuha)
        {
            clsDatabaseKontabilitet dbartikuj = new clsDatabaseKontabilitet();
            DataRow rreshti = dbartikuj.ktheLlogariNdermarrjesAndAutorizimeDR(idnderm, idperdorues, idllogari, idGjuha);
            dbartikuj.Dispose();
            return rreshti;
        }

        public static DataTable merrSipasLlogariteNdermarrjesAndAutorizimeDT(int idnderm, int idperdorues, int idGjuha)
        {
            clsDatabaseKontabilitet dbartikuj = new clsDatabaseKontabilitet();
            DataTable tabela = dbartikuj.ktheLlogariNdermarrjesAndAutorizimeDT(idnderm, idperdorues, idGjuha);
            dbartikuj.Dispose();
            return tabela;
        }

        public static DataTable merrSipasLlogariteNdermarrjesAndAutorizimeExport(int idnderm, int idperdorues, int lloji, string emerTabKoka, string emerFusheID)
        {
            clsDatabaseKontabilitet dbKontabiliteti = new clsDatabaseKontabilitet();
            DataTable tabela = dbKontabiliteti.ktheLlogariSipasNdermarjesAndAutorizimExport(idnderm, idperdorues, lloji, emerTabKoka, emerFusheID);
            dbKontabiliteti.Dispose();
            return tabela;
        }

        public static DataTable merrSipasLlogariteNdermarrjesAndAutorizimeAktivDT(int idnderm, int idperdorues)
        {
            clsDatabaseKontabilitet dbartikuj = new clsDatabaseKontabilitet();
            DataTable tabela = dbartikuj.ktheLlogariNdermarrjesAndAutorizimeAktivDT(idnderm, idperdorues);
            dbartikuj.Dispose();
            return tabela;
        }
        public static DataTable merrSipasLlogariteNdermarrjesAndAutorizimeAktivDTRaportuese(int idnderm, int idperdorues)
        {
            clsDatabaseKontabilitet dbartikuj = new clsDatabaseKontabilitet();
            DataTable tabela = dbartikuj.ktheLlogariNdermarrjesAndAutorizimeAktivDTRaportuese(idnderm, idperdorues);
            dbartikuj.Dispose();
            return tabela;
        }
        public static DataTable ktheLlogariNdermarrjesAndAutorizimeDTDheKokaFletekontabelPerQK(int idnderm, int idperdorues, int idkokafletekontabel)
        {
            clsDatabaseKontabilitet dbartikuj = new clsDatabaseKontabilitet();
            DataTable tabela = dbartikuj.ktheLlogariNdermarrjesAndAutorizimeDTDheKokaFletekontabelPerQK(idnderm, idperdorues, idkokafletekontabel);
            dbartikuj.Dispose();
            return tabela;
        }

        public static DataTable ktheLlogariNdermarrjesAndAutorizimeDTTeMundshmePerQK(int idnderm, int idperdorues)
        {
            clsDatabaseKontabilitet dbartikuj = new clsDatabaseKontabilitet();
            DataTable tabela = dbartikuj.ktheLlogariNdermarrjesAndAutorizimeDTTeMundshmePerQK(idnderm, idperdorues);
            dbartikuj.Dispose();
            return tabela;
        }


        /// <summary>
        /// metoda per shtimin e nje objekti clsLlogari ne nje arraylist
        /// </summary>
        public bool shtoLlogari(clsLlogari llogari)
        {
            base.Add(llogari);
            if (base.Contains(llogari))
                return true;
            else return false;
        }

        /// <summary>
        /// metoda per fshirjen e nje objekti clsLlogari ne nje arraylist
        /// </summary>
        public bool fshiLlogari(clsLlogari llogari)
        {
            base.Remove(llogari);
            if (base.Contains(llogari))
                return false;
            else return true;
        }

        /// <summary>
        /// metoda per fshirjen e gjithe objekteve clsLlogari ne nje arraylist
        /// </summary>
        public bool fshiGjitheLlogarite()
        {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// metoda per fshirjen e nje objekti clsLlogari ne nje indeks te caktuar ne nje arraylist
        /// </summary>
        public void fshiKeteLlogari(int index)
        {
            base.RemoveAt(index);
        }

        /// <summary>
        /// metoda per shtimin e nje objekti clsLlogari ne nje indeks te caktuar ne nje arraylist
        /// </summary>
        public void shtoLlogariNeIndeksin(int index, clsLlogari llogari)
        {
            base.Insert(index, llogari);
        }

        /// <summary>
        /// metoda kthen indeksin e nje objekti clsLlogari ne nje arraylist
        /// </summary>
        public int indeksiLlogarise(clsLlogari llogari)
        {//metoda per te marre indeksin e nje llogarie
            return base.IndexOf(llogari);
        }

        /// <summary>
        /// metoda kontrollon nese ekziston nje objekt clsLlogari ne nje arraylist
        /// </summary>
        public bool ekzistonLlogaria(clsLlogari llogari)
        {// metoda per te pare nqs llogaria ekziston ne nje arraylist
            if (base.Contains(llogari))
                return true;
            else return false;
        }

        /// <summary>
        /// metoda kthen numrin e objekteve clsLlogari ne nje arraylist
        /// </summary>
        public int numriLlogarive()
        {// metoda per te marre nr e llogarive ne arrayList
            return base.Count;
        }

        /// <summary>
        /// Metoda kthen nje collection me objekte te tipit <see cref="DbCore.DbKontabiliteti.clsLlogari"/> 
        /// duke filtruar sipas ID-se se ndermarrjes dhe ID-se se perdoruesit
        /// Thirret funksioni <see cref="DbCore.DbKontabiliteti.clsLogarite.mbushLLogariteNdermarrjesAndAutorizime"/>
        /// </summary>
        public colLlogarite merrLlogarite(int idndermarje, int idperdoruesi)
        {
            //clsDatabaseKontabilitet data = new clsDatabaseKontabilitet();
            //return data.merrLLogariteNdermarrjesAndAutorizime(idndermarje ,idperdoruesi );
            colLlogarite data = new colLlogarite();
            data.mbushLLogariteNdermarrjesAndAutorizime(idndermarje, idperdoruesi);
            return data;
        }

        public colLlogarite merrLlogaritePerAzhornim(int idndermarje, int idperdoruesi)
        {
            colLlogarite llog = new colLlogarite();
            llog.mbushLLogariteNdermarrjesAndAutorizimePerAzhornim(idndermarje, idperdoruesi);
            return llog;
        }

        public bool merrLLogariteLikeKodOsePershkAng(int idNdermarrje, int idPerdorues, string likeKodOsePershk)
        {
            clsDatabaseKontabilitet data = new clsDatabaseKontabilitet();
            bool sukses = mbushLlogarite(data.merrLlogariteLikeKodOsePershkAng(idNdermarrje, idPerdorues, likeKodOsePershk));
            data.Dispose();
            return sukses;
        }
        public bool merrLLogariteLikeKodOsePershk(int idNdermarrje, int idPerdorues, string likeKodOsePershk)
        {
            clsDatabaseKontabilitet data = new clsDatabaseKontabilitet();
            bool sukses = mbushLlogarite(data.merrLlogariteLikeKodOsePershk(idNdermarrje, idPerdorues, likeKodOsePershk));
            data.Dispose();
            return sukses;
        }
        public static DataTable merrLLogariteLikeKodOsePershkDT(int idNdermarrje, int idPerdoruesi, string likeKodOsePershk, int pershk, string klasa = "")
        {
            clsDatabaseKontabilitet data = new clsDatabaseKontabilitet();
            DataTable tabela = data.merrLlogariteLikeKodOsePershkDT(idNdermarrje, idPerdoruesi, likeKodOsePershk, pershk, klasa);
            data.Dispose();
            return tabela;
        }

        public static DataTable merrLLogariteNdermarrjesAndAutorizimeLikeNew(int idNdermarje, int idperdorues, string nrLlogarie)
        {
            clsDatabaseKontabilitet data = new clsDatabaseKontabilitet();
            DataTable tabela = data.ktheLLogariteNdermarrjesAndAutorizimeLikeNew(idNdermarje, idperdorues, nrLlogarie);
            data.Dispose();
            return tabela;
        }

        /// <summary>
        /// Metoda kthen nje collection me objekte te tipit <see cref="DbCore.DbKontabiliteti.clsLlogari"/> 
        /// duke filtruar sipas ID-se se ndermarrjes, ID-se se perdoruesit dhe numrit te llogarise
        /// Thirret funksioni <see cref="DbCore.DbKontabiliteti.colLlogarite.mbushLLogariteNdermarrjesAndAutorizimeLike"/>
        /// </summary>
        public colLlogarite merrLLogariteNdermarrjesAndAutorizimeLike(int idNdermarje, int idperdorues, string nrLlogarie)
        {
            colLlogarite data = new colLlogarite();
            data.mbushLLogariteNdermarrjesAndAutorizimeLike(idNdermarje, idperdorues, nrLlogarie);
            return data;
        }


        /// <summary>
        /// mbush llogarite nga kpf
        /// </summary>
        /// <param name="idKPF">id e kpf-se</param>
        /// <returns>kthen true nese mbushja kryhet me sukses ne te kundert false</returns>
        public static DataTable mbushLlogariteNgaKPF(int idKPF)
        {
            clsDatabaseKontabilitet dbLlogari = new clsDatabaseKontabilitet();
            DataTable tabela = dbLlogari.ktheLlogariteNgaKPF(idKPF);
            dbLlogari.Dispose();
            return tabela;
        }

        /// <summary>
        /// mbush llogarite nga kpf
        /// </summary>
        /// <param name="idKPF">id e kpf-se</param>
        /// <returns>kthen true nese mbushja kryhet me sukses ne te kundert false</returns>
        public bool mbushLlogariteNgaKPFLike(string kodikpf, int idndermarje)
        {
            clsDatabaseKontabilitet dbLlogari = new clsDatabaseKontabilitet();
            bool sukses = mbushLlogarite(dbLlogari.ktheLlogariteNgaKPFLike(kodikpf, idndermarje));
            dbLlogari.Dispose();
            return sukses;
        }

        /// <summary>
        /// mbush llogarite e ndermarrjes
        /// </summary>
        /// <param name="idNdermarje">id e ndermarrjes</param>
        /// <returns>kthen true nese mbushja kryhet me sukses ne te kundert false</returns>
        public bool mbushLLogariteNdermarrjes(int idNdermarje)
        {
            clsDatabaseKontabilitet dbLlogari = new clsDatabaseKontabilitet();
            bool sukses = mbushLlogarite(dbLlogari.ktheLLogariteNdermarrjes(idNdermarje));
            dbLlogari.Dispose();
            return sukses;
        }

        public bool ktheLLogariteNdermarrjesTeMundshmePerQK(int idNdermarje)
        {
            clsDatabaseKontabilitet dbLlogari = new clsDatabaseKontabilitet();
            bool sukses = mbushLlogarite(dbLlogari.ktheLLogariteNdermarrjesTeMundshmePerQK(idNdermarje));
            dbLlogari.Dispose();
            return sukses;
        }

        public bool ktheLLogariteNdermarrjesTeMundshmePerQK(int idNdermarje, clsDatabaseKontabilitet dbLlogari)
        {            
            return mbushLlogarite(dbLlogari.ktheLLogariteNdermarrjesTeMundshmePerQK(idNdermarje));
        }

        /// <summary>
        /// mbush llogarite e azhornuara sipas ndermarrjes dhe monedhes
        /// </summary>
        /// <param name="idNdermarje">id e ndermarrjes</param>
        /// <param name="idNdermarrjeVit">id e lidhjes mes ndermarrjes dhe vitit</param>
        /// <param name="idMonedha">id e monedhes</param>
        /// <returns>kthen true nese mbushja kryhet me sukses ne te kundert false</returns>
        public bool mbushLLogariteAzhornimit(int idNdermarje, int idMonedha)
        {
            clsDatabaseKontabilitet dbLlogari = new clsDatabaseKontabilitet();
            bool sukses = mbushLlogarite(dbLlogari.ktheLLogariteAzhornimit(idNdermarje, idMonedha));
            dbLlogari.Dispose();
            return sukses;
        }

        /// <summary>
        /// mbush llogarite e ndermarrjes sipas ndermarrjes dhe autorizimit
        /// </summary>
        /// <param name="idNdermarje">id e ndermarrjes</param>
        /// <param name="idperdorues">id e perdoruesit</param>
        /// <returns>kthen true nese mbushja kryhet me sukses ne te kundert false</returns>
        public bool mbushLLogariteNdermarrjesAndAutorizime(int idNdermarje, int idperdorues)
        {
            clsDatabaseKontabilitet dbLlogari = new clsDatabaseKontabilitet();
            bool sukses = mbushLlogarite(dbLlogari.ktheLLogariteNdermarrjesAndAutorizime(idNdermarje, idperdorues));
            dbLlogari.Dispose();
            return sukses;
        }

        public bool mbushLLogariteNdermarrjesAndAutorizimePerAzhornim(int idNdermarje, int idperdorues)
        {
            clsDatabaseKontabilitet dbLlogari = new clsDatabaseKontabilitet();
            bool sukses = mbushLlogarite(dbLlogari.ktheLLogariteNdermarrjesAndAutorizimePerAzhornim(idNdermarje, idperdorues));
            dbLlogari.Dispose();
            return sukses;
        } 
        public bool mbushLLogariteNdermarrjesAndAutorizimePerAzhornim(int idNdermarje, int idperdorues,    clsDatabaseKontabilitet dbLlogari)
        {
         
            bool sukses = mbushLlogarite(dbLlogari.ktheLLogariteNdermarrjesAndAutorizimePerAzhornim(idNdermarje, idperdorues));
            return sukses;
        }

        /// <summary>
        /// mbush llogarite sipas nr te llogarise ndermarrjes dhe autorizimit
        /// </summary>
        /// <param name="idNdermarje">id e ndermarrjes</param>
        /// <param name="idperdorues">id e perdoruesit</param>
        /// <param name="nrLlogarie">nr i llogarise</param>
        /// <returns>kthen true nese mbushja kryhet me sukses ne te kundert false</returns>
        public bool mbushLLogariteNdermarrjesAndAutorizimeLike(int idNdermarje, int idperdorues, string nrLlogarie)
        {
            clsDatabaseKontabilitet dbLlogari = new clsDatabaseKontabilitet();
            bool sukses = mbushLlogarite(dbLlogari.ktheLLogariteNdermarrjesAndAutorizimeLike(idNdermarje, idperdorues, nrLlogarie));
            dbLlogari.Dispose();
            return sukses;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbKontabiliteti.clsLlogari"/> 
        /// </summary> 
        private bool mbushLlogarite(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsLlogari llogari = new clsLlogari();
                    //llogari.mbushLlogari(rreshti);
                    Add(new clsLlogari(rreshti));
                }
            //}
            //catch (Exception)
            //{
            //    return false;
            //    //throw;
            //}
            return true;
        }
        private void mbushLlogarite(colLlogarite colLlog)
        {
            this.AddRange(colLlog);

        }
        #endregion
        [Obsolete("Perdor: bool mbushLlogarite(DataTable dt)", true)]
        public colLlogarite mbushArrayListLlogarite(DataSet ds)
        {// metoda per te mbushur nje arraylist me llogari nga nje dataset
            colLlogarite llogarite = new colLlogarite();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsLlogari llogari = new clsLlogari();
                llogari.IdLlogari = int.Parse(rreshti[0].ToString());
                llogari.NrLlogari = rreshti[1].ToString();
                llogari.EmerLlogari1 = rreshti[2].ToString();
                llogari.EmerLlogari2 = rreshti[3].ToString();
                llogari.QendraKostos = int.Parse(rreshti[4].ToString());
                llogari.KPF1 = int.Parse(rreshti[5].ToString());
                llogari.KPF2 = int.Parse(rreshti[6].ToString());
                llogari.KPF3 = int.Parse(rreshti[7].ToString());
                llogari.NivelTakse = int.Parse(rreshti[8].ToString());

                llogari.IdMonedha = int.Parse(rreshti[9].ToString());
                llogari.Grupi = int.Parse(rreshti[10].ToString());
                llogari.Nengrupi = int.Parse(rreshti[11].ToString());
                llogari.LlogariKonsoliduese = int.Parse(rreshti[12].ToString());
                llogari.LlogariKoresponduese = int.Parse(rreshti[13].ToString());
                llogari.IdNdermarja = int.Parse(rreshti[14].ToString());
                //llogari.IdNderViti = int.Parse(rreshti[15].ToString());
                llogari.IdPerdoruesi = int.Parse(rreshti[15].ToString());
                llogari.IdKonfig = int.Parse(rreshti[16].ToString());
                llogari.PershkrimiGrupiLlogaria = rreshti[17].ToString();
                llogari.PershkrimiNenGrupiLlogaria = rreshti[18].ToString();
                llogari.KodiMonedha = rreshti[19].ToString();
                llogari.KodiKPF1 = rreshti[20].ToString();
                llogari.KodiKPF2 = rreshti[21].ToString();
                llogari.KodiKPF3 = rreshti[22].ToString();
                llogari.PershkrimTaksa = rreshti[23].ToString();

                llogarite.Add(llogari);
            }
            return llogarite;
        }
    }
}