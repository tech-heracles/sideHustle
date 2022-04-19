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
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsDokumenti
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colDokumentat : System.Collections.Generic.List<clsDokumenti>
    {
        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbRegjistrim.clsDokumenti"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary> 
        public new clsDokumenti this[int index]
        {
            get { return ((clsDokumenti)base[index]); }
        }

        /// <summary>
        /// mbush dokumentat e trupit sipas kokes
        /// </summary>
        /// <param name="id">id e kokes</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushDokumentaTrupiSipasKoka(int id)
        {
            clsDatabaseRegjistrim dbDokumenti = new clsDatabaseRegjistrim();
            bool mbush = mbushDokumenta(dbDokumenti.ktheDokumentaTrupiSipasKoka(id));
            dbDokumenti.Dispose();
            return mbush;
        }

        /// <summary>
        /// mbush gjithe dokumentat lidhes Sipas Klientit dhe kahut
        /// </summary>
        /// <param name="idKlientFurnitor">id e klient/furnitorit</param>
        /// <param name="idndermarje">id qe lidh ndermarrjen me vitin</param>
        /// <param name="kahu">kahu</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushGjitheDokumentatLidhesSipasKlientitDheKahut(int idKlientFurnitor, int idndermarje, string kahu)
        {
            clsDatabaseRegjistrim dbDokumenti = new clsDatabaseRegjistrim();
            bool mbush = mbushDokumenta(dbDokumenti.ktheGjitheDokumentatLidhesSipasKlientitDheKahut(idKlientFurnitor, idndermarje, kahu));
            dbDokumenti.Dispose();
            return mbush;
        }

        /// <summary>
        /// mbush gjithe dokumentat lidhes sipas Klientit
        /// </summary>
        /// <param name="idKlientFurnitor">id e klient/furnitorit</param>
        /// <param name="idndermarje">id e ndermarrjes dhe vitit</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushGjitheDokumentatLidhesSipasKlientit(int idKlientFurnitor, int idndermarje)
        {
            clsDatabaseRegjistrim dbDokumenti = new clsDatabaseRegjistrim();
            bool mbush = mbushDokumenta(dbDokumenti.ktheGjitheDokumentatLidhesSipasKlientit(idKlientFurnitor, idndermarje));
            dbDokumenti.Dispose();
            return mbush;
        }
        public static DataTable mbushGjitheDokumentatVeprimeBankaPaPrintuar(int idndermarje, string datanga, string dataderi, int idperdorues, bool status, int idshop, int idkatdok)
        {
            clsDatabaseRegjistrim dbDokumenti = new clsDatabaseRegjistrim();
            DataTable dt = dbDokumenti.mbushGjitheDokumentatVeprimeBankaPaPrintuar(idndermarje, datanga, dataderi, idperdorues, status, idshop, idkatdok);
            dbDokumenti.Dispose();
            return dt;
        }
        public static DataTable mbushGjitheDokumentatRegjistrimDokumentash(int idndermarje, int idperdorues)
        {
            clsDatabaseRegjistrim dbDokumenti = new clsDatabaseRegjistrim();
            DataTable dt = dbDokumenti.ktheGjitheDokumentatRegjistrimDokumentash(idndermarje, idperdorues);
            dbDokumenti.Dispose();
            return dt;
        } 
        /// <summary>
        /// mbush gjithe Dokumentat sipas klientit
        /// </summary>
        /// <param name="idKlientFurnitor">id e klient/furnitorit</param>
        /// <param name="idndermarje">id e ndermarrjes</param>
        /// <param name="idndermviti">id qe lidh ndermarrjen me vitin</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushGjitheDokumentatSipasKlientit(int idKlientFurnitor, int idndermarje, string datanga, string dataderi)
        {
            using (var dbDokumenti = new clsDatabaseRegjistrim())
                return mbushDokumenta(dbDokumenti.ktheGjitheDokumentatSipasKlientit(idKlientFurnitor, idndermarje, datanga, dataderi));
        }

        /// <summary>
        /// mbush gjithe dokumentat kryesore
        /// </summary>
        /// <param name="idndermarje">id qe lidh ndermarrjen me vitin</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public bool MbushGjitheDokumentatKryesore(int idndermarje, string datanga, string dataderi)
        {
            using (var dbDokumenti = new clsDatabaseRegjistrim())
                return mbushDokumenta(dbDokumenti.KtheGjitheDokumentatKryesore(idndermarje, datanga, dataderi));
        }

        /// <summary>
        /// mbush kokat e shitjes sipas klientit
        /// </summary>
        /// <param name="idKlientFurnitor">id e klientit/furnitorit</param>
        /// <param name="idndermarje">id e ndermarrjes</param>
        /// <param name="idndermviti">id qe lidh ndermarrjen me vitin</param>
        /// <param name="idKatDok">id e kategorise se dokumenti</param>
        /// <param name="idLidhese">id lidhesit</param>
        /// <param name="idKatDokvk"></param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public static DataTable mbushKokaShitjeSipasKlientit(int idKlientFurnitor, int idndermarje)
        {
            clsDatabaseRegjistrim dbDokumenti = new clsDatabaseRegjistrim();
            DataTable dt = dbDokumenti.ktheKokaShitjeSipasKlientit(idKlientFurnitor, idndermarje);
            dbDokumenti.Dispose();
            return dt;
        }
        public static DataTable mbushKokaShitjePaLikuiduar(int idndermarje, int idKatDok, string teDrejtaGjitheDokPerTuLikujduar, int idPerdoruesi,int idkonfigurim)
        {
            clsDatabaseRegjistrim dbDokumenti = new clsDatabaseRegjistrim();
            DataTable dt = dbDokumenti.ktheKokaShitjePalikuiduar(idndermarje, idKatDok, teDrejtaGjitheDokPerTuLikujduar, idPerdoruesi, idkonfigurim);
            dbDokumenti.Dispose();
            return dt;
        }
        public static DataTable merrKursFatureOseAzhornimiPerIdDok(string IdDocs)
        {
            clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();
            DataTable dt = db.merrKursFatureOseAzhornimiPerIdDok(IdDocs);
            dt.Dispose();
            return dt;
        }

        public static DataTable mbushKokaShitjePaLikuiduarSipasFaturave(int idNdermarrje, int idKatDok, int idPerdoruesi, DataTable dtFaturat, int idDokumenti, int idNiveli, int idkonfigurim)
        {
            DataTable faturatPaLikuiduarDt = new DataTable();
            using (clsDatabaseRegjistrim dbDokumenti = new clsDatabaseRegjistrim())
            {
                faturatPaLikuiduarDt = dbDokumenti.ktheKokaShitjePalikuiduarSipasFaturave(idNdermarrje, idKatDok, idPerdoruesi, dtFaturat, idDokumenti, idNiveli, idkonfigurim);
            }
            return faturatPaLikuiduarDt;
        }

        public bool MbushKokaShitjeSipasKlientit(int idKlientFurnitor, int idndermarje, int idLidhese)
        {
            using (var dbDokumenti = new clsDatabaseRegjistrim())
                return mbushDokumenta(dbDokumenti.KtheKokaShitjeSipasKlientit(idKlientFurnitor, idndermarje, idLidhese));
        }

        /// <summary>
        /// mbush kokat e shitjes sipas klientit
        /// </summary>
        /// <param name="idKlientFurnitor">id e klientit/furnitorit</param>
        /// <param name="idndermarje">id e ndermarrjes</param>
        /// <param name="idndermviti">id qe lidh ndermarrjen me vitin</param>
        /// <param name="idKatDok">id e kategorise se dokumenti</param>
        /// <param name="idLidhese">id lidhesit</param>
        
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushVeprimeBankaSipasKlientit(int idKlientFurnitor, int idndermarje,  int idLidhese)
        {
            clsDatabaseRegjistrim dbDokumenti = new clsDatabaseRegjistrim();
            bool mbush = mbushDokumenta(dbDokumenti.ktheVeprimeBankaSipasKlientit(idKlientFurnitor, idndermarje,  idLidhese));
            dbDokumenti.Dispose();
            return mbush;
        }

        /// <summary>
        /// mbush gjithe dokumentat me veprime banke
        /// </summary>
        /// <param name="idndermarje">id qe lidh ndermarrjen me vitin</param>
        /// <param name="datanga">data nga</param>
        /// <param name="dataderi">data deri ne</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public static DataTable mbushGjitheDokumentatVeprimeBanka(int idndermarje, string datanga, string dataderi, int arkabanka, int idperdoruesi, int niveli)
        {
            clsDatabaseRegjistrim dbDokumenti = new clsDatabaseRegjistrim();
            DataTable dt = dbDokumenti.ktheGjitheDokumentatVeprimeBanka(idndermarje, datanga, dataderi,arkabanka, idperdoruesi, niveli);
            dbDokumenti.Dispose();
            return dt;
        }


        /// <summary>
        /// mbush gjithe dokumentat qe bejne regjistrime dokumentash
        /// </summary>
        /// <param name="idndermarje">id qe lidh ndermarrjen me vitin</param>
        /// <param name="datanga">data nga</param>
        /// <param name="dataderi">data deri ne</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public static DataTable mbushGjitheDokumentatRegjistrimDokumentash(int idndermarje, string datanga, string dataderi, int idperdorues, int idkategoria)
        {
            clsDatabaseRegjistrim dbDokumenti = new clsDatabaseRegjistrim();
            DataTable dt = dbDokumenti.ktheGjitheDokumentatRegjistrimDokumentash(idndermarje, datanga, dataderi,idperdorues, idkategoria);
            dbDokumenti.Dispose();
            return dt;
        }

   
        public static DataTable mbushGjitheDokumentatRegjistrimDokumentashPaPrintuar(int idndermarje, string datanga, string dataderi, int idperdorues, bool status, int idshop)
        {
            clsDatabaseRegjistrim dbDokumenti = new clsDatabaseRegjistrim();
            DataTable dt = dbDokumenti.mbushGjitheDokumentatRegjistrimDokumentashPaPrintuar(idndermarje, datanga, dataderi, idperdorues, status,idshop);
            dbDokumenti.Dispose();
            return dt;
        }
 
        /// <summary>
        /// mbush gjithe dokumentat qe bejne regjistrime dokumentash
        /// </summary>
        /// <param name="idndermarje">id qe lidh ndermarrjen me vitin</param>
        /// <param name="datanga">data nga</param>
        /// <param name="dataderi">data deri ne</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public static DataTable ktheGjitheDokumentatRegjistrimDokumentashMeKushtUPPDheIntervalDate(int idndermarje, string datanga, string dataderi, int idperdorues)
        {
            clsDatabaseRegjistrim dbDokumenti = new clsDatabaseRegjistrim();
            DataTable dt = dbDokumenti.ktheGjitheDokumentatRegjistrimDokumentashMeKushtUPPDheIntervalDate(idndermarje, datanga, dataderi, idperdorues);
            dbDokumenti.Dispose();
            return dt;
        }  
        ///// <summary>
        ///// mbush gjithe dokumentat qe bejne regjistrime dokumentash sipas filtrave
        ///// </summary>
        ///// <param name="idndermarje">id qe lidh ndermarrjen me vitin</param>
        ///// <param name="datanga">data nga</param>
        ///// <param name="dataderi">data deri ne</param>
        ///// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        //public static DataTable ktheGjitheDokumentatRegjistrimDokumentashSipasFiltrave(int idndermarje, string datanga, string dataderi, int idperdorues, string kodkonfigurimi, string kodartikulli, string grupi, string nengrupi, string klienti, string nrdok)
        //{
        //    clsDatabaseRegjistrim dbDokumenti = new clsDatabaseRegjistrim();
        //    DataTable dt = dbDokumenti.ktheGjitheDokumentatRegjistrimDokumentashSipasFiltrave(idndermarje, datanga, dataderi, idperdorues, kodkonfigurimi,kodartikulli,grupi,nengrupi,klienti,nrdok);
        //    dbDokumenti.Dispose();
        //    return dt;
        //}

        /// <summary>
        /// mbush gjithe dokumentat qe bejne regjistrime dokumentash sipas filtrave
        /// </summary>
        /// <param name="idndermarje">id qe lidh ndermarrjen me vitin</param>
        /// <param name="datanga">data nga</param>
        /// <param name="dataderi">data deri ne</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public static DataTable ktheGjitheDokumentatRegjistrimDokumentashSipasFiltrave(int idndermarje, string datanga, string dataderi, int idperdorues, string kodkonfigurimi, string kodartikulli, string grupi, string nengrupi, string klienti, string nrdok, bool gjithedok)
        {
            clsDatabaseRegjistrim dbDokumenti = new clsDatabaseRegjistrim();
            DataTable dt = dbDokumenti.ktheGjitheDokumentatRegjistrimDokumentashSipasFiltrave(idndermarje, datanga, dataderi, idperdorues, kodkonfigurimi, kodartikulli, grupi, nengrupi, klienti, nrdok, gjithedok);
            dbDokumenti.Dispose();
            return dt;
        }
        public static DataTable ktheDokumentaRegjistrimDokumentashMeFiltra(string nrdokfilter, int start, int end, int idndermarje, string datanga, string dataderi, int idperdorues, string kodkonfigurimi, string kodartikulli, string grupi, string nengrupi, string klienti)
        {
            clsDatabaseRegjistrim dbDokumenti = new clsDatabaseRegjistrim();
            DataTable dt = dbDokumenti.ktheDokumentaRegjistrimDokumentashMeFiltra(nrdokfilter, start, end, idndermarje, datanga, dataderi, idperdorues, kodkonfigurimi, kodartikulli, grupi, nengrupi, klienti);
            dbDokumenti.Dispose();
            return dt;
        }
        public static DataTable ktheDokumentaRegjistrimDokumentashsipasId(string id)
        {
            clsDatabaseRegjistrim dbDokumenti = new clsDatabaseRegjistrim();
            DataTable dt = dbDokumenti.merrKokaShitjeSipasIdve(id);
            dbDokumenti.Dispose();
            return dt;
        }
        /// <summary>
        /// mbush gjithe dokumentat qe bejne regjistrime dokumentash sipas kodkonfigurimi
        /// </summary>
        /// <param name="idndermarje">id qe lidh ndermarrjen </param>
        /// <param name="kodkonfigurimi">kodkonfigurimi</param>
        /// <param name="idperdorues">id e perdoruesit per autorizimet</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public static DataTable ktheGjitheDokumentatRegjistrimDokumentashSipasKodKonfigurimi(int idndermarje, string kodkonfigurimi, int idperdorues)
        {
            clsDatabaseRegjistrim dbDokumenti = new clsDatabaseRegjistrim();
            DataTable dt = dbDokumenti.ktheGjitheDokumentatRegjistrimDokumentashSipasKodKonfigurimi(idndermarje, kodkonfigurimi,  idperdorues);
            dbDokumenti.Dispose();
            return dt;
        } 
        /// <summary>
        /// mbush gjithe dokumentat qe bejne regjistrime dokumentash
        /// </summary>
        /// <param name="idndermarje">id qe lidh ndermarrjen me vitin</param>
        /// <param name="datanga">data nga</param>
        /// <param name="dataderi">data deri ne</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public static DataTable mbushGjitheDokumentatRegjistrimDokumentashShperndarjeShpenzimesh(int idndermarje, string datanga, string dataderi)
        {
            clsDatabaseRegjistrim dbDokumenti = new clsDatabaseRegjistrim();
            DataTable dt = dbDokumenti.ktheGjitheDokumentatRegjistrimDokumentashShperndarjeShpenzimesh(idndermarje, datanga, dataderi);
            dbDokumenti.Dispose();
            return dt;
        }
         /// <summary>
        /// mbush gjithe dokumentat qe bejne regjistrime dokumentash
        /// </summary>
        /// <param name="idndermarje">id qe lidh ndermarrjen me vitin</param>
        /// <param name="datanga">data nga</param>
        /// <param name="dataderi">data deri ne</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public static DataTable mbushGjitheDokumentatAzhornimDokumentash(int idndermarje, string datanga, string dataderi, int idperdoruesi)
        {
            clsDatabaseRegjistrim dbDokumenti = new clsDatabaseRegjistrim();
            DataTable dt = dbDokumenti.ktheGjitheDokumentatAzhornimDokumentash(idndermarje, datanga, dataderi, idperdoruesi);
            dbDokumenti.Dispose();
            return dt;
        }
        
        public static DataTable mbushGjitheDokumentatMbylljeDokumentash(int idndermarje, string datanga, string dataderi, int idperdoruesi)
        {
            clsDatabaseRegjistrim dbDokumenti = new clsDatabaseRegjistrim();
            DataTable dt = dbDokumenti.ktheGjitheDokumentatMbylljeDokumentash(idndermarje, datanga, dataderi, idperdoruesi);
            dbDokumenti.Dispose();
            return dt;
        }
        public static DataTable ktheGjitheDokumentatQendraKostoDokumentash(int idndermarje, string datanga, string dataderi, int idperdoruesi)
        {
            clsDatabaseRegjistrim dbDokumenti = new clsDatabaseRegjistrim();
            DataTable dt = dbDokumenti.ktheGjitheDokumentatQendraKostoDokumentash(idndermarje, datanga, dataderi, idperdoruesi);
            dbDokumenti.Dispose();
            return dt;
        }
        public static DataTable ktheGjitheDokumentatAmortizimiDokumentash(int idndermarje, string datanga, string dataderi, int idperdoruesi)
        {
            clsDatabaseRegjistrim dbDokumenti = new clsDatabaseRegjistrim();
            DataTable dt = dbDokumenti.ktheGjitheDokumentatAmortizimiDokumentash(idndermarje, datanga, dataderi, idperdoruesi);
            dbDokumenti.Dispose();
            return dt;
        }
        public static DataTable ktheGjitheDokumentatRivleresimAmortizimiDokumentash(int idndermarje, string datanga, string dataderi, int idperdoruesi, string kodnivel)
        {
            clsDatabaseRegjistrim dbDokumenti = new clsDatabaseRegjistrim();
            DataTable dt = dbDokumenti.ktheGjitheDokumentatRivleresimAmortizimiDokumentash(idndermarje, datanga, dataderi, idperdoruesi, kodnivel);
            dbDokumenti.Dispose();
            return dt;
        }
    
        public static DataTable ktheGjitheDokPerRecetaOptike(int idndermarje, string datanga, string dataderi, int idperdoruesi)
        {
            using (var dbDokumenti = new clsDatabaseRegjistrim())
                return dbDokumenti.ktheGjitheDokPerRecetaOptike(idndermarje, datanga, dataderi, idperdoruesi);
            
        }
        /// <summary>
        /// mbush gjithe dokumentat me veprime kf
        /// </summary>
        /// <param name="idndermarje">id qe lidh ndermarrjen me vitin</param>
        /// <param name="datanga">data nga</param>
        /// <param name="dataderi">data deri ne</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public static DataTable mbushGjitheDokumentatVeprimeKF(int idndermarje, string datanga, string dataderi, int idperdoruesi)
        {
            clsDatabaseRegjistrim dbDokumenti = new clsDatabaseRegjistrim();
            DataTable dt = dbDokumenti.ktheGjitheDokumentatVeprimeKF(idndermarje, datanga, dataderi, idperdoruesi);
            dbDokumenti.Dispose();
            return dt;
        }
        
        /// <summary>
        /// mbush gjithe dokumentat me veprime kf
        /// </summary>
        /// <param name="idndemarje">id qe lidh ndermarrjen me vitin</param>
        /// <param name="datanga">data nga</param>
        /// <param name="dataderi">data deri ne</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public static DataTable mbushGjitheDokumentatLidhesdok(int idndemarje, string datanga, string dataderi, int idperdoruesi)
        {
            clsDatabaseRegjistrim dbDokumenti = new clsDatabaseRegjistrim();
            DataTable dt = dbDokumenti.ktheGjitheDokumentatLidhesdok(idndemarje, datanga, dataderi, idperdoruesi);
            dbDokumenti.Dispose();
            return dt;
        }
        /// <summary>
        /// mbush gjithe dokumentat flete kontabel
        /// </summary>
        /// <param name="idndermarje">id qe lidh ndermarrjen me vitin</param>
        /// <param name="datanga">data nga</param>
        /// <param name="dataderi">data deri ne</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public static DataTable mbushGjitheDokumentatFleteKontabel(int idndermarje, string datanga, string dataderi, int idperdorues)
        {
            clsDatabaseRegjistrim dbDokumenti = new clsDatabaseRegjistrim();
            DataTable dt = dbDokumenti.ktheGjitheDokumentatFleteKontabel(idndermarje, datanga, dataderi, idperdorues);
            dbDokumenti.Dispose();
            return dt;
        }

        /// <summary>
        /// mbush gjithe regjistrimet e dokumentave sipas kategorise
        /// </summary>
        /// <param name="idndermarje">id qe lidh ndermarrjen me vitin</param>
        /// <param name="datanga">data nga</param>
        /// <param name="dataderi">data deri ne</param>
        /// <param name="idkategori">id e kategorise</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public static DataTable mbushGjitheDokumentatFleteDoganore(int idndermarje, string datanga, string dataderi, int idkategori, int idperdorues)
        {
            clsDatabaseRegjistrim dbDokumenti = new clsDatabaseRegjistrim();
            DataTable dt = dbDokumenti.ktheGjitheRegjistrimDokumentashSipasKategori(idndermarje, datanga, dataderi, idkategori, idperdorues);
            dbDokumenti.Dispose();
            return dt;
        }
        public static DataTable mbushGjitheDokumentatFleteDoganoreKerkimi(int idndermarje, string datanga, string dataderi, int idperdoruesi, int niveli)
        {
            clsDatabaseRegjistrim dbDokumenti = new clsDatabaseRegjistrim();
            DataTable dt = dbDokumenti.ktheGjitheDokumentatFleteDoganore(idndermarje, datanga, dataderi,idperdoruesi, niveli);
            dbDokumenti.Dispose();
            return dt;
        }
  
        public static DataTable ktheGjitheDokumentatListPagesa(int idndermarje, string datanga, string dataderi, int idperdoruesi)
        {
            clsDatabaseRegjistrim dbDokumenti = new clsDatabaseRegjistrim();
            DataTable dt = dbDokumenti.ktheGjitheDokumentatListPagesa(idndermarje, datanga, dataderi,idperdoruesi);
            dbDokumenti.Dispose();
            return dt;
        } 
        public static DataTable ktheGjitheDokumentatPlanifikim(int idndermarje, string datanga, string dataderi, int idperdoruesi)
        {
            clsDatabaseRegjistrim dbDokumenti = new clsDatabaseRegjistrim();
            DataTable dt = dbDokumenti.ktheGjitheDokumentatPlanifikim(idndermarje, datanga, dataderi,idperdoruesi);
            dbDokumenti.Dispose();
            return dt;
        } 

        public static DataTable ktheGjitheDokumentatEkzekutim(int idndermarje, string datanga, string dataderi, int idperdoruesi)
        {
            clsDatabaseRegjistrim dbDokumenti = new clsDatabaseRegjistrim();
            DataTable dt = dbDokumenti.ktheGjitheDokumentatEkzekutim(idndermarje, datanga, dataderi,idperdoruesi);
            dbDokumenti.Dispose();
            return dt;
        } 
        public static DataTable ktheGjitheDokumentatPlanifikimTePalidhura(int idndermarje, string datanga, string dataderi, int idperdoruesi)
        {
            clsDatabaseRegjistrim dbDokumenti = new clsDatabaseRegjistrim();
            return (dbDokumenti.ktheGjitheDokumentatPlanifikimTePalidhura(idndermarje, datanga, dataderi,idperdoruesi));
        }
        public static DataTable ktheGjitheDokumentatUrdherPagesa(int idndermarje, string datanga, string dataderi, int idperdoruesi)
        {
            clsDatabaseRegjistrim dbDokumenti = new clsDatabaseRegjistrim();
            DataTable dt = dbDokumenti.ktheGjitheDokumentatUrdherPagesa(idndermarje, datanga, dataderi,idperdoruesi);
            dbDokumenti.Dispose();
            return dt;
        }
           /// <summary>
        /// mbush gjithe regjistrimet e dokumentave sipas kategorise
        /// </summary>
        /// <param name="idndermarje">id qe lidh ndermarrjen me vitin</param>
        /// <param name="datanga">data nga</param>
        /// <param name="dataderi">data deri ne</param>
        /// <param name="idkategori">id e kategorise</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public static DataTable mbushGjitheRegjistrimDokumentashKerkimiSHSH(int idndermarje, string datanga, string dataderi)
        {
            clsDatabaseRegjistrim dbDokumenti = new clsDatabaseRegjistrim();
            DataTable dt = dbDokumenti.ktheGjitheDokumentatRegjistrimDokumentashKerkoShperndarjeShpenzimesh(idndermarje, datanga, dataderi);
            dbDokumenti.Dispose();
            return dt;
        }

        /// <summary>
        /// mbush gjithe dokumentat qe kane konvertim dokumentash
        /// </summary>
        /// <param name="idndermarje">id qe lidh ndermarrjen me vitin</param>
        /// <param name="idnivel">id e nivelit</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public static DataTable mbushGjitheDokumentatRegjistrimDokumentashKonvertim(int idndermarje,string datanga, string dataderi, int idperdoruesi, int idnivel, bool merrstatusporosi, bool merrtegjitha)
        {
            clsDatabaseRegjistrim dbDokumenti = new clsDatabaseRegjistrim();
            DataTable dt = dbDokumenti.ktheGjitheDokumentatRegjistrimDokumentashKonvertim(idndermarje,datanga, dataderi, idperdoruesi, idnivel, merrstatusporosi,merrtegjitha);
            dbDokumenti.Dispose();
            return dt;
        }

        /// <summary>
        /// mbush gjithe dokumentat qe kane konvertim dokumentash
        /// </summary>
        /// <param name="idndermarje">id qe lidh ndermarrjen me vitin</param>
        /// <param name="idnivel">id e nivelit</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public static DataTable mbushGjitheDokumentatRegjistrimDokumentashKonvertimSipasKrijuesit(int idndermarje, string datanga, string dataderi, int idperdoruesi, int idnivel, bool gjithedok)
        {
            clsDatabaseRegjistrim dbDokumenti = new clsDatabaseRegjistrim();
            DataTable dt = new DataTable();            
            dt = dbDokumenti.ktheGjitheDokumentatRegjistrimDokumentashKonvertimSipasKrijuesit(idndermarje, datanga, dataderi, idperdoruesi, idnivel, gjithedok);
            dbDokumenti.Dispose();
            return dt;
        }


        /// <summary>
        /// mbush gjithe dokumetat e magazines
        /// </summary>
        /// <param name="idndermarje">id qe lidh ndermarrjen me vitin</param>
        /// <param name="datanga">data nga</param>
        /// <param name="dataderi">data deri ne</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public static DataTable mbushGjitheDokumentatMagazines(int idndermarje, string datanga, string dataderi, int idperdoruesi, int idllojdokmag)
        {
            clsDatabaseRegjistrim dbDokumenti = new clsDatabaseRegjistrim();
            DataTable dt = dbDokumenti.ktheGjitheDokumentatMagazines(idndermarje, datanga, dataderi,idperdoruesi, idllojdokmag);
            dbDokumenti.Dispose();
            return dt;
        }
    
        public static DataTable ktheGjitheDokumentatNdryshimCmimSasi(int idndermarje, string datanga, string dataderi, int idperdoruesi)
        {
            clsDatabaseRegjistrim dbDokumenti = new clsDatabaseRegjistrim();
            DataTable dt = dbDokumenti.ktheGjitheDokumentatNdryshimCmimSasi(idndermarje, datanga, dataderi, idperdoruesi);
            dbDokumenti.Dispose();
            return dt;
        }
 public static DataTable ktheGjitheDokumentatInventarizimi(int idndermarje, string datanga, string dataderi, int idperdoruesi)
        {
            clsDatabaseRegjistrim dbDokumenti = new clsDatabaseRegjistrim();
            DataTable dt = dbDokumenti.ktheGjitheDokumentatInventarizimi(idndermarje, datanga, dataderi, idperdoruesi);
            dbDokumenti.Dispose();
            return dt;
        }


        public static DataTable mbushGjitheDokumentatRezervimit(int idndermarje, string datanga, string dataderi, int idperdoruesi, int niveli)
        {
            clsDatabaseRegjistrim dbDokumenti = new clsDatabaseRegjistrim();
            DataTable dt = dbDokumenti.ktheGjitheDokumentatRezervimit(idndermarje, datanga, dataderi, idperdoruesi, niveli);
            dbDokumenti.Dispose();
            return dt;
        }
     
        public static DataTable mbushGjitheDokumentatRiparimi(int idndermarje, string datanga, string dataderi, int idperdoruesi)
        {
            clsDatabaseRegjistrim dbDokumenti = new clsDatabaseRegjistrim();
            DataTable dt = dbDokumenti.ktheGjitheDokumentatRiparime(idndermarje, datanga, dataderi, idperdoruesi);
            dbDokumenti.Dispose();
            return dt;
        }
        public static DataTable ktheGjitheDokumentatSkedulim(int idndermarje, string datanga, string dataderi, int idperdoruesi)
        {
            clsDatabaseRegjistrim dbDokumenti = new clsDatabaseRegjistrim();
            DataTable dt = dbDokumenti.ktheGjitheDokumentatSkedulim(idndermarje, datanga, dataderi, idperdoruesi);
            dbDokumenti.Dispose();
            return dt;
        }
        /// <summary>
        /// mbush gjithe dokumentat me shperndarje shpenzimesh
        /// </summary>
        /// <param name="idndermarje">id qe lidh ndermarrjen me vitin</param>
        /// <param name="datanga">data nga</param>
        /// <param name="dataderi">data deri ne</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public static DataTable mbushGjitheDokumentatShperndarjeShpenzimesh(int idndermarje, string datanga, string dataderi, int idperdoruesi)
        {
            clsDatabaseRegjistrim dbDokumenti = new clsDatabaseRegjistrim();
            return dbDokumenti.ktheGjitheDokumentatShperndarjeShpenzimesh(idndermarje, datanga, dataderi,idperdoruesi);
        }

        public void mbushDokumentaNgaDokumentaBanka(int idkokashitje)
        {
            using (DbCore.DbArkaBanka.clsDatabaseArkaBanka db = new DbArkaBanka.clsDatabaseArkaBanka())
            {
                mbushDokumentaNgaBanka(db.ktheGjitheVeprimetBankaPerTuLidhur(idkokashitje));
            }

        }
        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje dataseti
        /// nepermjet kesaj metode informacioni kalohet nga dataset-i ne nje liste me objekte te tipit 
        /// perdoret per lidhjen e dokumentave
        ///  <see cref="DbCore.DbRegjistrim.clsDokumenti"/> 
        /// </summary>
        private bool mbushDokumenta(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsDokumenti doc = new clsDokumenti();
                    //doc.mbushDokument(rreshti);
                    Add(new clsDokumenti(rreshti));
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //    //throw;
            //}
            return true;
        }
        private bool mbushDokumentaNgaBanka(DataTable dt)
        {
         

                foreach (DataRow rreshti in dt.Rows)
                {

                clsDokumenti doc = new clsDokumenti();
                doc.mbushDokumentNgaBanka(rreshti);
                Add(doc);
            }

           
            return true;
        }

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje dataseti
        /// nepermjet kesaj metode informacioni kalohet nga dataset-i ne nje liste me objekte te tipit 
        /// perdoret per popupin e kerkimit te dokumentave
        ///  <see cref="DbCore.DbRegjistrim.clsDokumenti"/> 
        /// </summary>
        private bool mbushDokumentaKerkimi(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    clsDokumenti doc = new clsDokumenti();
                    doc.mbushDokumentKerkimi(rreshti);
                    Add(doc);
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
