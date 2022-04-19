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
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsArtikulli
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colArtikujt : System.Collections.Generic.List<clsArtikulli>
    {

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbInventari.clsArtikulli"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsArtikulli this[int index]
        {
            get { return ((clsArtikulli)base[index]); }
        }
        public colArtikujt()
        {
        }
        public colArtikujt(int idKokeShitje)
        {
            if (idKokeShitje > 0)
                using (clsDatabaseInventari db = new clsDatabaseInventari())
                {
                    mbushArtikujt(db.merrArtikujSipasIdve(idKokeShitje));
                }
        }
        public colArtikujt(int idAmortizimi, string lloj)
        {
            if (idAmortizimi > 0 && lloj == "Amortizim")
                using (clsDatabaseInventari db = new clsDatabaseInventari())
                {
                    mbushArtikujt(db.merrArtikujSipasIdAmortizimi(idAmortizimi));
                }
        }
        public colArtikujt(List<int> idArtikuj)
        {
            if (idArtikuj.Count <= 0)
                return;
            using (clsDatabaseInventari db = new clsDatabaseInventari())
            {
                mbushArtikujt(db.merrArtikujSipasIdve(idArtikuj));
            }
        }
        public colArtikujt(List<string> kodArtikuj, int idndermarje)
        {
            if (kodArtikuj.Count <= 0)
                return;
            using (clsDatabaseInventari db = new clsDatabaseInventari())
            {
                mbushArtikujt(db.merrArtikuj(kodArtikuj.Join(';', x => x.ToString()), idndermarje));
            }
        }
        public colArtikujt(int idkokamag, clsDatabaseInventari db)
        {
            mbushArtikujt(db.merrArtikujFatureMag(idkokamag));

        }
        
        public static DataRow merrSipasArtikujNdermarrjesAndAutorizimeDR(int idnderm, int idperdorues, int idArtikull, bool kosto, bool gjendje)
        {
            using (clsDatabaseInventari dbartikuj = new clsDatabaseInventari())
            {
                return dbartikuj.ktheArtikullNdermarrjesAndAutorizimeDR(idnderm, idperdorues, idArtikull, kosto, gjendje);
            }
        }

        public static DataTable merrSipasArtikujNdermarrjesAndAutorizimeDT(int idnderm, int idperdorues, bool kosto, bool gjendje, bool cmime, bool cmimeMeTvsh, string postStringTvsh) //nese nuk perdoret cmime dhe cmimemetvsh nuk te duhet poststringtvsh
        {
            using (clsDatabaseInventari dbartikuj = new clsDatabaseInventari())
            {
                return dbartikuj.ktheArtikujNdermarrjesAndAutorizimeDT(idnderm, idperdorues, kosto, gjendje, cmime, cmimeMeTvsh, postStringTvsh);
            }
        }

        public static DataTable merrSipasArtikujNdermarrjesAndAutorizimeDT(int idnderm, int idperdorues, bool kostoSasih, bool gjendje, string postStringTvsh)
        {
            return merrSipasArtikujNdermarrjesAndAutorizimeDT(idnderm, idperdorues, kostoSasih,gjendje, false, false, postStringTvsh);
        }

        public static DataTable ktheArtikujNdermarrjesAndAutorizimeDTfilter(int idnderm, int idperdorues, bool kostoSasi, string filter)
        {
            using (clsDatabaseInventari dbartikuj = new clsDatabaseInventari())
            {
                return dbartikuj.ktheArtikujNdermarrjesAndAutorizimeDTfilter(idnderm, idperdorues, kostoSasi, filter);
            }
        }
        public static DataTable ktheArtikujNdermarrjesAndAutorizimeDheLlojDT(int idnderm, int idperdorues, bool llojart, bool kostoSasi)
        {
            clsDatabaseInventari dbartikuj = new clsDatabaseInventari();
            DataTable tabela = dbartikuj.ktheArtikujNdermarrjesAndAutorizimeDheLlojDT(idnderm, idperdorues, llojart, kostoSasi);
            dbartikuj.Dispose();
            return tabela;
        }

        public static DataTable ktheArtikujNdermarrjesAndAutorizimeDTExport(int idnderm, int idperdorues, bool llojart, int lloji, string emerTabKoka, string emerFusheID)
        {
            clsDatabaseInventari dbartikuj = new clsDatabaseInventari();
            DataTable tabela = dbartikuj.ktheArtikujNdermarrjesAndAutorizimeDTExport(idnderm, idperdorues, llojart, lloji, emerTabKoka, emerFusheID);
            dbartikuj.Dispose();
            return tabela;
        }
        public static DataTable ktheArtikujNdermarrjesAndAutorizimeDTGjeneroKodbar(int idnderm, int idperdorues, bool llojart)
        {
            clsDatabaseInventari dbartikuj = new clsDatabaseInventari();
            DataTable tabela = dbartikuj.ktheArtikujNdermarrjesAndAutorizimeDTGjeneroKodbar(idnderm, idperdorues, llojart);
            dbartikuj.Dispose();
            return tabela;
        }
        public static DataTable merrSipasArtikujAktivNdermarrjesAndAutorizimeDT(int idnderm, int idperdorues)
        {
            clsDatabaseInventari dbartikuj = new clsDatabaseInventari();
            DataTable tabela = dbartikuj.ktheArtikujAktivNdermarrjesAndAutorizimeDT(idnderm, idperdorues);
            dbartikuj.Dispose();
            return tabela;
        }

        public static DataTable MerrSipasArtikujAktivNdermarrjesAndAutorizimeAc(int idNdermarrje, int idPerdorues, string kodi)
        {
            using (var databaseInventari = new clsDatabaseInventari())
                return databaseInventari.MerrSipasArtikujAktivNdermarrjesAndAutorizimeAc(idNdermarrje, idPerdorues, kodi);
        }

        public static DataTable ktheArtikujLoanDT(int idnderm, int idmagazina, int idartikulli, DateTime date)
        {
            clsDatabaseInventari dbartikuj = new clsDatabaseInventari();
            DataTable tabela = dbartikuj.ktheArtikujLoanDT(idnderm, idmagazina, idartikulli, date);
            dbartikuj.Dispose();
            return tabela;
        }

        /// <summary>
        /// kthen te gjithe artikujt qe popullojne griden tek lupa e artikullit
        /// </summary>
        /// <param name="idnderm">Id e ndermarrjes</param>
        /// <param name="idperdorues">Id e perdoruesit</param>
        /// <param name="iShitshem">true per te marre vetem artikujt e shitshem, false per t'i marre te gjithe</param>
        /// <returns></returns>
        public static DataTable merrSipasArtikujNdermarrjesAndAutorizimePerLupeArtikulli(int idnderm, int idperdorues, bool kostoSasi, int idMag, bool cmime, bool cmimeMeTvsh, string postStringTvsh, bool iShitshem, bool filtro, string filter, string dategjendje, int kodifikim1, int kodifikim2, int kodifikim3, bool dhurataVFOne)
        {
            using (var dbartikuj = new clsDatabaseInventari())
            {
                DataTable tabela;
                if (filtro)
                    tabela = dbartikuj.ktheArtikujNdermarrjesAndAutorizimePerLupeArtikulliFilter(idnderm, idperdorues,
                        kostoSasi, idMag, cmime, cmimeMeTvsh, postStringTvsh, iShitshem, dategjendje, filter, false,
                        kodifikim1, kodifikim2, kodifikim3, dhurataVFOne);
                else if (filter == "" && idMag == -1 && kodifikim1 == -1 && kodifikim2 == -1 && kodifikim3 == -1)
                    tabela = dbartikuj.ktheArtikujNdermarrjesAndAutorizimePerLupeArtikulli(idnderm, idperdorues,
                        kostoSasi, idMag, cmime, cmimeMeTvsh, postStringTvsh, iShitshem, dategjendje, kodifikim1,
                        kodifikim2, kodifikim3, dhurataVFOne);
                else
                    tabela = dbartikuj.ktheArtikujNdermarrjesAndAutorizimePerLupeArtikulliFilter(idnderm, idperdorues,
                        kostoSasi, idMag, cmime, cmimeMeTvsh, postStringTvsh, iShitshem, dategjendje, filter, true,
                        kodifikim1, kodifikim2, kodifikim3, dhurataVFOne);
                return tabela;
            }
        }

        /// <summary>
        /// merr te gjithe artikujt perberes te nje artikulli sipas id se tij
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DataTable merrArtikujtPerberesSipasIdArtikullitKryesor(int id, DateTime data)
        {
            using (clsDatabaseInventari dbartikuj = new clsDatabaseInventari())
                return dbartikuj.ktheArtikujPerberesSipasIdArtikullitKryesorLupa(id, data);


        }
        public bool merrArtikujtPerberesSipasIdArtKryesordheDate(int id, DateTime data)
        {
            clsDatabaseInventari dbInventari = new clsDatabaseInventari();
            bool sukses = mbushArtikujt(dbInventari.ktheArtikujPerberesSipasIdArtikullitKryesorDheDate(id, data));
            dbInventari.Dispose();
            return sukses;
        }
        

        /// <summary>
        /// merr te gjithe artikujt e perbere sipas id se nje artikulli perberes
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DataTable merrArtikujtEPerbereSipasIdArtikullitPerberes(int id, DateTime data)
        {
            using (clsDatabaseInventari dbartikuj = new clsDatabaseInventari())
                return dbartikuj.ktheArtikujtEPerbereSipasIdArtikullitPerberes(id, data);



        }

        /// <summary>
        /// merr te gjithe grupimet e  perberesve te nje artikulli sipas id se tij
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DataTable merrGrupimPerberesishSipasIdArtikullitKryesor(string id, DateTime data)
        {

            clsDatabaseInventari dbartikuj = new clsDatabaseInventari();
            DataTable tabela = dbartikuj.ktheGrupimPerberesishSipasIdArtikullitKryesorLupa(id, data);
            dbartikuj.Dispose();
            return tabela;

        }


        public static DataTable ktheArtikujNdermarrjesAndAutorizimeSipasPikeve(int idnderm, int idperdorues, decimal pike, int idmagazina, DateTime data)
        {
            clsDatabaseInventari dbartikuj = new clsDatabaseInventari();
            DataTable tabela = dbartikuj.ktheArtikujNdermarrjesAndAutorizimeSipasPikeve(idnderm, idperdorues, pike, idmagazina, data);
            dbartikuj.Dispose();
            return tabela;
        }
        public static DataTable ktheArtikujNdermarrjesAndAutorizimeSipasKodit(int idnderm, int idperdorues, string kodi, int idmagazina, DateTime data)
        {
            clsDatabaseInventari dbartikuj = new clsDatabaseInventari();
            DataTable tabela = dbartikuj.ktheArtikujNdermarrjesAndAutorizimeSipasKodit(idnderm, idperdorues, kodi, idmagazina, data);
            dbartikuj.Dispose();
            return tabela;
        }
        public static DataTable ktheArtikujNdermarrjesAndAutorizimeSipasBazaarit(int idnderm, int idperdorues, int idmagazina, DateTime data)
        {
            clsDatabaseInventari dbartikuj = new clsDatabaseInventari();
            DataTable tabela = dbartikuj.ktheArtikujNdermarrjesAndAutorizimeSipasBazaarit(idnderm, idperdorues, idmagazina, data);
            dbartikuj.Dispose();
            return tabela;
        }

        /// <summary>
        /// kthen te gjithe artikujt (pervec atyre te klasave perbere dhe prodhim) qe popullojne griden tek lupa e artikullit
        /// </summary>
        /// <param name="idnderm">Id e ndermarrjes</param>
        /// <param name="idperdorues">Id e perdoruesit</param>
        /// <param name="iShitshem">true per te marre vetem artikujt e shitshem, false per t'i marre te gjithe</param>
        /// <returns></returns>
        public static DataTable merrSipasArtikujNdermarrjesAndAutorizimePerLupeArtikulliJoPerbProdhim(int idnderm, int idperdorues, bool kostoSasi, int idMag, bool cmime, bool cmimeMeTvsh, string postStringTvsh, bool iShitshem, bool filtro, string filter, int kodifikim1, int kodifikim2, int kodifikim3)
        {
            using (var dbartikuj = new clsDatabaseInventari())
            {
                DataTable tabela;
                if (filtro)
                    tabela = dbartikuj.ktheArtikujNdermarrjesAndAutorizimePerLupeArtikulliJoPerbProdhimFilter(idnderm,
                        idperdorues, kostoSasi, idMag, cmime, cmimeMeTvsh, postStringTvsh, iShitshem, filter, false,
                        kodifikim1, kodifikim2, kodifikim3);
                else if (filter == "" && idMag == -1 && kodifikim1 == -1 && kodifikim2 == -1 && kodifikim3 == -1)
                    tabela = dbartikuj.ktheArtikujNdermarrjesAndAutorizimePerLupeArtikulliJoPerbProdhim(idnderm,
                        idperdorues, kostoSasi, idMag, cmime, cmimeMeTvsh, postStringTvsh, iShitshem, kodifikim1,
                        kodifikim2, kodifikim3);
                else
                    tabela = dbartikuj.ktheArtikujNdermarrjesAndAutorizimePerLupeArtikulliJoPerbProdhimFilter(idnderm,
                        idperdorues, kostoSasi, idMag, cmime, cmimeMeTvsh, postStringTvsh, iShitshem, filter, true,
                        kodifikim1, kodifikim2, kodifikim3);
                
                return tabela;
            }
        }

        public static DataTable merrSipasArtikujNdermarrjesAndAutorizimePerLupeArtikulliPerbere(int idnderm,
            int idperdorues, bool kostoSasi, int idMag, bool cmime, bool cmimeMeTvsh, string postStringTvsh,
            bool iShitshem, bool filtro, string filter, int kodifikim1, int kodifikim2, int kodifikim3)
        {
            using (var dbartikuj = new clsDatabaseInventari())
            {
                DataTable tabela;
                if (filtro)
                    tabela = dbartikuj.ktheArtikujNdermarrjesAndAutorizimePerLupeArtikulliPerbereFilter(idnderm,
                        idperdorues, kostoSasi, idMag, cmime, cmimeMeTvsh, postStringTvsh, iShitshem, filter, false,
                        kodifikim1, kodifikim2, kodifikim3);
                else if (filter == "" && idMag == -1 && kodifikim1 == -1 && kodifikim2 == -1 && kodifikim3 == -1)
                    tabela = dbartikuj.ktheArtikujNdermarrjesAndAutorizimePerLupeArtikulliPerbere(idnderm, idperdorues,
                        kostoSasi, idMag, cmime, cmimeMeTvsh, postStringTvsh, iShitshem, kodifikim1, kodifikim2,
                        kodifikim3);
                else
                    tabela = dbartikuj.ktheArtikujNdermarrjesAndAutorizimePerLupeArtikulliPerbereFilter(idnderm,
                        idperdorues, kostoSasi, idMag, cmime, cmimeMeTvsh, postStringTvsh, iShitshem, filter, true,
                        kodifikim1, kodifikim2, kodifikim3);
                
                return tabela;
            }
        }

        /// <summary>
        /// kthen te gjithe artikujt per prodhim (inventar, prodhim ne proces dhe prodhim) qe popullojne griden tek lupa e artikullit
        /// </summary>
        /// <param name="idnderm">Id e ndermarrjes</param>
        /// <param name="idperdorues">Id e perdoruesit</param>
        /// <param name="iShitshem">true per te marre vetem artikujt e shitshem, false per t'i marre te gjithe</param>
        /// <returns>data table me keto te dhena</returns>
        public static DataTable merrSipasArtikujNdermarrjesAndAutorizimePerLupeArtikulliPerProdhim(int idnderm,
            int idperdorues, bool kostoSasi, int idMag, bool cmime, bool cmimeMeTvsh, string postStringTvsh,
            bool iShitshem, bool filtro, string filter, int kodifikim1, int kodifikim2, int kodifikim3)
        {
            using (var dbartikuj = new clsDatabaseInventari())
            {
                DataTable tabela;
                if (filtro)
                    tabela = dbartikuj.ktheArtikujNdermarrjesAndAutorizimePerLupeArtikulliPerProdhimFilter(idnderm,
                        idperdorues, kostoSasi, idMag, cmime, cmimeMeTvsh, postStringTvsh, iShitshem, filter, false,
                        kodifikim1, kodifikim2, kodifikim3);
                else if (filter == "" && idMag == -1 && kodifikim1 == -1 && kodifikim2 == -1 && kodifikim3 == -1)
                    tabela = dbartikuj.ktheArtikujNdermarrjesAndAutorizimePerLupeArtikulliPerProdhim(idnderm, idperdorues,
                        kostoSasi, idMag, cmime, cmimeMeTvsh, postStringTvsh, iShitshem, kodifikim1, kodifikim2,
                        kodifikim3);
                else
                    tabela = dbartikuj.ktheArtikujNdermarrjesAndAutorizimePerLupeArtikulliPerProdhimFilter(idnderm,
                        idperdorues, kostoSasi, idMag, cmime, cmimeMeTvsh, postStringTvsh, iShitshem, filter, true,
                        kodifikim1, kodifikim2, kodifikim3);
            
                return tabela;
            }
        }

        /// <summary>
        /// kthen te gjithe artikujt per prodhim (prodhim ne proces dhe prodhim) qe popullojne griden tek lupa e artikullit
        /// </summary>
        /// <param name="idnderm">Id e ndermarrjes</param>
        /// <param name="idperdorues">Id e perdoruesit</param>
        /// <param name="iShitshem">true per te marre vetem artikujt e shitshem, false per t'i marre te gjithe</param>
        /// <returns>data table me keto te dhena</returns>
        public static DataTable merrSipasArtikujNdermarrjesAndAutorizimePerLupeArtikulliProdhimi(int idnderm, int idperdorues, bool kostoSasi, int idMag, bool cmime, bool cmimeMeTvsh, string postStringTvsh, bool iShitshem, bool filtro, string filter, int kodifikim1, int kodifikim2, int kodifikim3)
        {
            using (var dbartikuj = new clsDatabaseInventari())
            {
                DataTable tabela;
                if (filtro)
                    tabela = dbartikuj.ktheArtikujNdermarrjesAndAutorizimePerLupeArtikulliProdhimiFilter(idnderm,
                        idperdorues, kostoSasi, idMag, cmime, cmimeMeTvsh, postStringTvsh, iShitshem, filter, false,
                        kodifikim1, kodifikim2, kodifikim3);
                else if (filter == "" && idMag == -1 && kodifikim1 == -1 && kodifikim2 == -1 && kodifikim3 == -1)
                    tabela = dbartikuj.ktheArtikujNdermarrjesAndAutorizimePerLupeArtikulliProdhimi(idnderm, idperdorues,
                        kostoSasi, idMag, cmime, cmimeMeTvsh, postStringTvsh, iShitshem, kodifikim1, kodifikim2,
                        kodifikim3);
                else
                    tabela = dbartikuj.ktheArtikujNdermarrjesAndAutorizimePerLupeArtikulliProdhimiFilter(idnderm,
                        idperdorues, kostoSasi, idMag, cmime, cmimeMeTvsh, postStringTvsh, iShitshem, filter, true,
                        kodifikim1, kodifikim2, kodifikim3);
                
                return tabela;
            }
        }
        public static DataTable merrSipasArtikujNdermarrjesAndAutorizimePerLupeArtikulliProdhimiPlanifikimi(int idnderm, int idperdorues, bool kostoSasi, int idMag, bool cmime, bool cmimeMeTvsh, string postStringTvsh, bool iShitshem, bool filtro, string filter, int idplanifikimi, int kodifikim1, int kodifikim2, int kodifikim3)
        {
            using (var dbartikuj = new clsDatabaseInventari())
            {
                DataTable tabela;
                if (filtro)
                    tabela = dbartikuj.ktheArtikujNdermarrjesAndAutorizimePerLupeArtikulliProdhimiFilterPlanifikimi(
                        idnderm, idperdorues, kostoSasi, idMag, cmime, cmimeMeTvsh, postStringTvsh, iShitshem, filter,
                        idplanifikimi, false, kodifikim1, kodifikim2, kodifikim3);
                else if (filter == "" && idMag == -1 && kodifikim1 == -1 && kodifikim2 == -1 && kodifikim3 == -1)
                    tabela = dbartikuj.ktheArtikujNdermarrjesAndAutorizimePerLupeArtikulliProdhimiPlanifikimi(idnderm,
                        idperdorues, kostoSasi, idMag, cmime, cmimeMeTvsh, postStringTvsh, iShitshem, idplanifikimi,
                        kodifikim1, kodifikim2, kodifikim3);
                else
                    tabela = dbartikuj.ktheArtikujNdermarrjesAndAutorizimePerLupeArtikulliProdhimiFilterPlanifikimi(
                        idnderm, idperdorues, kostoSasi, idMag, cmime, cmimeMeTvsh, postStringTvsh, iShitshem, filter,
                        idplanifikimi, true, kodifikim1, kodifikim2, kodifikim3);

                return tabela;
            }
        }

        /// <summary>
        /// merr artikujt sipas ndermarrjes dhe autorizimit   per rivleresim
        /// </summary>
        /// <param name="idnderm">idnderm</param>
        /// <param name="idperdorues">idperdorues</param>
        /// <returns>kthen true nese kalimi ndodh me sukses, ne te kundert false.</returns>
        public static DataTable merrSipasArtikujNdermarrjesAndAutorizimePerRivleresim(int idnderm, int idperdorues)
        {
            clsDatabaseInventari dbInventari = new clsDatabaseInventari();
            DataTable tabela = dbInventari.ktheArtikujNdermarrjesAndAutorizimePerRivleresim(idnderm, idperdorues);
            dbInventari.Dispose();
            return tabela;
        }
        /// <summary>
        /// merr artikujt sipas magazinave
        /// </summary>
        /// <param name="idmag">id e magazinave</param>
        /// <returns>kthen true nese kalimi ndodh me sukses, ne te kundert false.</returns>
        public static DataTable merrSipasMagazinave(string idmag)
        {
            clsDatabaseInventari dbInventari = new clsDatabaseInventari();
            DataTable tabela = dbInventari.ktheArtikujSipasMagazinave(idmag);
            dbInventari.Dispose();
            return tabela;
        }

        public static DataTable merrArtikujLikeKodPershkKodbarDT(int p, int idPerdoruesi, string infixText, int pershk, string grup, bool iShitshem, bool merrVetemAfatgjate, bool merrSipasDetajimit, string klasa)
        {
            clsDatabaseInventari dbInventari = new clsDatabaseInventari();
            DataTable tabela = dbInventari.merrArtikujLikeKodPershkKodbarDT(p, idPerdoruesi, infixText, pershk, grup, iShitshem, merrVetemAfatgjate, merrSipasDetajimit, klasa, false);
            dbInventari.Dispose();
            return tabela;
        }
        public static colArtikujt merrArtikujLikeKodPershkKodbarDTFull(int p, int idPerdoruesi, string infixText, int pershk, string grup, bool iShitshem, bool merrVetemAfatgjate, bool merrSipasDetajimit, string klasa)
        {
            using(var dbInventari = new clsDatabaseInventari())
            {
                colArtikujt artikujt = new colArtikujt();
                artikujt.mbushArtikujt(dbInventari.merrArtikujLikeKodPershkKodbarDT(p, idPerdoruesi, infixText, pershk, grup, iShitshem, merrVetemAfatgjate, merrSipasDetajimit, klasa, true));
                return artikujt;
            }
        }

        public static DataTable merrArtikujLikeKodPershkKodbarDTJoProdhim(int p, int idPerdoruesi, string infixText, int pershk)
        {
            clsDatabaseInventari dbInventari = new clsDatabaseInventari();
            DataTable tabela = dbInventari.merrArtikujLikeKodPershkKodbarDTJoProdhim(p, idPerdoruesi, infixText, pershk);
            dbInventari.Dispose();
            return tabela;
        }
        public static DataTable merrArtikujLikeKodPershkKodbarDTPerbere(int p, int idPerdoruesi, string infixText, int pershk)
        {
            clsDatabaseInventari dbInventari = new clsDatabaseInventari();
            DataTable tabela = dbInventari.merrArtikujLikeKodPershkKodbarDTPerbere(p, idPerdoruesi, infixText, pershk);
            dbInventari.Dispose();
            return tabela;
        }

        public static DataTable merrArtikujLikeKodPershkKodbarDTArtProdhim(int p, int idPerdoruesi, string infixText, int pershk)
        {
            clsDatabaseInventari dbInventari = new clsDatabaseInventari();
            DataTable tabela = dbInventari.merrArtikujLikeKodPershkKodbarDTArtProdhim(p, idPerdoruesi, infixText, pershk);
            dbInventari.Dispose();
            return tabela;
        }
        public static DataTable merrArtikujLikeKodPershkKodbarDTArtProdhimPlanifikim(int p, int idPerdoruesi, string infixText, int pershk, int idplanifikimi)
        {
            clsDatabaseInventari dbInventari = new clsDatabaseInventari();
            DataTable tabela = dbInventari.merrArtikujLikeKodPershkKodbarDTArtProdhimPlanifikim(p, idPerdoruesi, infixText, pershk, idplanifikimi);
            dbInventari.Dispose();
            return tabela;
        }

        public static DataTable merrArtikujLikeKodPershkKodbarDTProdhim(int p, int idPerdoruesi, string infixText, int pershk)
        {
            clsDatabaseInventari dbInventari = new clsDatabaseInventari();
            DataTable tabela = dbInventari.merrArtikujLikeKodPershkKodbarDTProdhim(p, idPerdoruesi, infixText, pershk);
            dbInventari.Dispose();
            return tabela;
        }

        public static DataTable merrArtikujLikeKodOsePershkDT(int idNder, int idperdorues, string kodOsePershkArtikulli, int pershk)
        {
            clsDatabaseInventari dbInventari = new clsDatabaseInventari();
            DataTable tabela = dbInventari.ktheArtikujLikeKodOsePershkDT(idNder, idperdorues, kodOsePershkArtikulli, pershk);
            dbInventari.Dispose();
            return tabela;
        }

        public static bool kaGjendjeArtikulli(int idArtikulli)
        {
            using (clsDatabaseInventari dbInventari = new clsDatabaseInventari())
                return dbInventari.kaGjendjeArtikulliApoJo(idArtikulli);
        }

        /// <summary>
        /// merr artikujt sipas ndermarrjes dhe autorizimit dhe sipas kodit te artikullit
        /// </summary>
        /// <param name="idNder">idnder</param>
        /// <param name="idperdorues">idperdorues</param>
        /// <param name="kodArtikulli">kodArtikulli</param>
        /// <returns>kthen true nese kryhet me sukses marrja, ne te kundert false</returns>
        public bool merrSipasArtikujNdermarrjesAndAutorizimeLike(int idNder, int idperdorues, string kodArtikulli)
        {
            clsDatabaseInventari dbInventari = new clsDatabaseInventari();
            bool sukses = mbushArtikujt(dbInventari.ktheArtikujNdermarrjesAndAutorizimeLike(idNder, idperdorues, kodArtikulli));
            dbInventari.Dispose();
            return sukses;
        }

        /// <summary>
        /// merr artikujt sipas ndermarrjes dhe autorizimit dhe sipas kodit te artikullit dhe qe nuk bejne pjese ne klasat perbere dhe prodhim
        /// </summary>
        /// <param name="idNder">idnder</param>
        /// <param name="idperdorues">idperdorues</param>
        /// <param name="kodArtikulli">kodArtikulli</param>
        /// <returns>kthen true nese kryhet me sukses marrja, ne te kundert false</returns>
        public bool merrSipasArtikujNdermarrjesAndAutorizimeLikeJoPerbProdhim(int idNder, int idperdorues, string kodArtikulli)
        {
            clsDatabaseInventari dbInventari = new clsDatabaseInventari();
            bool sukses = mbushArtikujt(dbInventari.ktheArtikujNdermarrjesAndAutorizimeLikeJoPerbProdhim(idNder, idperdorues, kodArtikulli));
            dbInventari.Dispose();
            return sukses;
        }

        /// <summary>
        /// merr artikujt sipas ndermarrjes dhe autorizimit dhe sipas kodit te artikullit per prodhim klasa inventar, prodhim, prodhim ne proces
        /// </summary>
        /// <param name="idNder">idnder</param>
        /// <param name="idperdorues">idperdorues</param>
        /// <param name="kodArtikulli">kodArtikulli</param>
        /// <returns>kthen true nese kryhet me sukses marrja, ne te kundert false</returns>
        public bool merrSipasArtikujNdermarrjesAndAutorizimeLikePerProdhim(int idNder, int idperdorues, string kodArtikulli)
        {
            clsDatabaseInventari dbInventari = new clsDatabaseInventari();
            bool sukses = mbushArtikujt(dbInventari.ktheArtikujNdermarrjesAndAutorizimeLikePerProdhim(idNder, idperdorues, kodArtikulli));
            dbInventari.Dispose();
            return sukses;
        }
        /// <summary>
        /// Kerkon nese gjendet artikulli ne collection. Nese nuk gjendet e merr nga databaza, e shton ne collection dhe e kthen
        /// </summary>
        /// <param name="idArtikulli"></param>
        /// <returns></returns>
        public clsArtikulli MerrArtikull(int idArtikulli)
        {
            clsArtikulli artikull = null;
            artikull = this.Find(art => art.IdArtikulli == idArtikulli);
            if (artikull != null)
                return artikull;

            artikull = new clsArtikulli(idArtikulli);
            if (artikull.IdArtikulli == 0) return null;

            this.Add(artikull);

            return artikull;

        }

        /// <summary>
        /// merr artikujt sipas ndermarrjes dhe autorizimit dhe qe fillojne me kodbar te caktuar
        /// </summary>
        /// <param name="idNder">id e ndermarrjes</param>
        /// <param name="idperdorues">id e perdoruesit</param>
        /// <param name="kodbar">kodbarin e artikullit</param>
        /// <returns>kthen nje vlere True nese mbushja kryhet me sukses, ne te kundert false</returns>
        public bool merrSipasArtikujNdermarrjesAndAutorizimeLikeKodbar(int idNder, int idperdorues, string kodbar)
        {
            clsDatabaseInventari dbInventari = new clsDatabaseInventari();
            bool sukses = mbushArtikujt(dbInventari.ktheArtikujNdermarrjesAndAutorizimeLikeKodbar(idNder, idperdorues, kodbar));
            dbInventari.Dispose();
            return sukses;
        }


        public static DataTable MerrRaportinGjendjaEArtikujveMeImei(string rapEmriReal, int idNdermarrje, int idPerdoruesi, string dtFillimi, string dtMbarimi, string dtDok, string dtRegjistrimi, string filter)
        {
            using (clsDatabaseInventari dbInventar = new clsDatabaseInventari())
                return dbInventar.MerrRaportinGjendjaEArtikujveMeImei(rapEmriReal, idNdermarrje, idPerdoruesi, dtFillimi, dtMbarimi, dtDok, dtRegjistrimi, filter);
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// Metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera nga nje store procedure.
        /// </summary>
        /// <param name="dt">Merr si parameter nje DataTable.</param>
        /// <returns>Kthen True nese kryhet me sukses, ne te kundert False.</returns>
        private bool mbushArtikujt(DataTable dt)
        {
            if (dt != null)
            {
                foreach (DataRow rreshti in dt.Rows)
                {
                    Add(new clsArtikulli(rreshti));
                }
                return true;
            }
            else return false;
        }

        #endregion
    }
}