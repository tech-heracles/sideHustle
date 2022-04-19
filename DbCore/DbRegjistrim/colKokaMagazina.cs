using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Resources;
using DbCore.DbInventari;
using DbCore.DbShare;
using DbCore.DbKontabiliteti;
using DbCore.DbAsete;
using DbCore.IMBUtils.Extensions;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsKokaMagazina
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colKokaMagazina : System.Collections.Generic.List<clsKokaMagazina>
    {
        private static string pershkrimDaljeFK = "Nga daljet e magazinës";
        private static string pershkrimHyrjeFK = "Nga hyrjet e magazinës";

        #region Konstruktor
        public colKokaMagazina()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public colKokaMagazina(string idte) 
        {
            using (clsDatabaseRegjistrim db = new clsDatabaseRegjistrim())
            {
                mbushKokatMagazina(db.merrKokaMagazinaSipasIdve(idte), db);
            }
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idNdermVit">id qe lidh ndermarrjen me vitin</param>
        //public colKokaMagazina(int idNdermVit)
        //{
        //    clsDatabaseRegjistrim dbKokaMagazina = new clsDatabaseRegjistrim();
        //    mbushKokatMagazina(dbKokaMagazina.ktheGjitheKokaMagazina(idNdermVit), dbKokaMagazina);
        //    dbKokaMagazina.Dispose();
        //}
        public static DataTable merrKokaMagazinaDT(int idndermvit, int idperdoruesi, string datanga, string dataderi, bool gjithedok, bool meautorizim, int eshteHyrje)
        {
            using (clsDatabaseRegjistrim dbartikuj = new clsDatabaseRegjistrim())
            {
                return dbartikuj.merrKokaMagazinaDT(idndermvit, idperdoruesi, datanga, dataderi, gjithedok, meautorizim, eshteHyrje);
            }
        }

        //[Obsolete("Perdor: DataRow ktheKokaMagazinaSipasIDGjenerues(int idGjenerues)", false)]  
        //public colKokaMagazina(int idGjenerues)
        //{
        //    clsDatabaseRegjistrim regjDb = new clsDatabaseRegjistrim();
        // this.AddRange(  mbushArrayListKokaMagazina(regjDb.merrKokaMagazinaSipasIDGjenerues(idGjenerues)));
        //}
        //   /// <summary>
        ///// konstruktor me 3 parametra
        ///// </summary>
        ///// <param name="filtrat">filtrat</param>
        ///// <param name="idNderm">id e ndermarrjes</param>
        ///// <param name="idNdermVit">id e ndermarrjes qe lidhet me vitin</param>
        //public colKokaMagazina(string filtrat, string shpenz)
        //{
        //    clsDatabaseRegjistrim dbKokeShitje = new clsDatabaseRegjistrim();
        //    mbushKokatMagazina(dbKokeShitje.ktheFaturatSipasFiltraveShpez(filtrat), dbKokeShitje);
        //    dbKokeShitje.Dispose();
        //}

        /// <summary>
        /// mbush koken e magazines sipas id gjenerues
        /// </summary>
        /// <param name="idKonfigGjen"></param>
        /// <param name="idGjenerues">id e gjeneruesit</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        public void mbushKokaMagazinaSipasIDGjenerues(int idKonfigGjen, int idGjenerues)
        {
            using(clsDatabaseRegjistrim db = new clsDatabaseRegjistrim())
                mbushKokaMagazinaSipasIDGjenerues(idKonfigGjen, idGjenerues, db);
        }

        /// <summary>
        /// mbush koken e magazines sipas id gjenerues
        /// </summary>
        /// <param name="idKonfigGjen"></param>
        /// <param name="idGjenerues">id e gjeneruesit</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        public void mbushKokaMagazinaSipasIDGjenerues(int idKonfigGjen, int idGjenerues, clsDatabaseRegjistrim db)
        {
            mbushKokatMagazina(db.ktheKokaMagazinaSipasIDGjenerues(idKonfigGjen, idGjenerues), db);//, "shper"
        }
        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbRegjistrim.clsKokaMagazina"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary> 
        public new clsKokaMagazina this[int index]
        {
            get { return ((clsKokaMagazina)base[index]); }
        }

        public static DataTable merrDokMagazinePerEksport(int idnderm, int idperdorues, int idNdermViti, int lloji, string emerTabKoka, string emerFusheId, string idPerEksport, bool serialeUnike)
        {
            clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim();
            DataTable tabela = dbRegj.ktheDokMagazinePerExport(idnderm, idperdorues, idNdermViti, lloji, emerTabKoka, emerFusheId, idPerEksport, serialeUnike);
            dbRegj.Dispose();
            return tabela;
        }
        public clsMesazh ruaj(ResourceManager rm,CultureInfo ci )
        {
            clsMesazh mesazh = new clsMesazh();
            string shfaqmesazhapolupe = "";
            clsDatabaseRegjistrim dbregj = new clsDatabaseRegjistrim();
            dbregj.beginTransaksion();
            string mesazhmevonshem = "";
            foreach (clsKokaMagazina kok in this)
            {
                DbAdmin.clsPeriudhaKontabel per = new DbAdmin.clsPeriudhaKontabel(kok.DtDok, kok.IdNdermarrje, new DbAdmin.clsDatabaseAdmin(dbregj));
                mesazh = kok.ruaj(false, 1, null, per.IdPeriudha, "Nga ndryshimi i cmimit", 6, 0, dbregj, out shfaqmesazhapolupe, 0, new DbQendraKosto.colTrupiQendraKosto(), false,new DbCore.DbAsete.colSerialetMagazine(),new DbCore.DbAsete.colSerialetMagazine(),new DbCore.DbShare.clsKonfigurimAmbjenti(), new DbCore.DbShare.clsKonfigurimAmbjenti(),0,0, new DbQendraKosto.colTrupiQendraKosto(), false, new clsKokaShitje(), false, false, new colAmortizimiKoka(),1,false,false, new colTrupiMagazina(),new colAmortizimiKoka(),new int [0],false,false,false, out mesazhmevonshem, null, false, false);
               
                if (!mesazh.Status)
                {
                    dbregj.rollbackTransaksion();
                    return mesazh;
                }
            }
            dbregj.commitTransaksion();
            return mesazh;
        }
        public static DataTable ktheRreshtaMagazineArtikulliPerRiruajtje(int idnderm, List<object> idartikuj, DateTime datanga, DateTime dataderi)
        {
            using (clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim())
            {
                return dbRegj.ktheRreshtaMagazineArtikulliPerRiruajtje(idartikuj, idnderm, datanga, dataderi);

            }
        }

        public static DataTable merrDokMagazinePerImport(string emerTabKoka, string emerTabTrupi, string ndermarrjeKod, string ndermarjeKodi, string nenkategoria, int lloji, string primaryKey, bool merrTePaImportuara, bool rimerrTeImportuara, int? nrDokumentash)
        {
            clsDatabaseRegjistrim dbImportMagazina = new clsDatabaseRegjistrim();
            DataTable dt = dbImportMagazina.merrDokMagazinePerImport(emerTabKoka, emerTabTrupi, ndermarrjeKod, ndermarjeKodi, nenkategoria, lloji, primaryKey, merrTePaImportuara, rimerrTeImportuara, nrDokumentash);
            dbImportMagazina.Dispose();
            return dt;
        }
        /// <summary>
        /// kthen tablen me FAF e lidhura me dok per te gjeneruar raportin e gabimeve
        /// </summary>
        /// <param name="rreshtat"></param>
        /// <returns></returns>
        public static DataTable merrFAFdokLidhur(List<object> rreshtat)
        {
            DataTable err = new DataTable();
            err.Columns.Add("Kodi");
            err.Columns.Add("Gabimi");
            err.Columns.Add("Rreshti");
            err.Columns.Add("NrGabimesh");
            int nrreshta = 0;
            string iddok = "(" + String.Join(",", rreshtat) + ")";
            DataTable faf = new DbCore.DbRegjistrim.clsDatabaseRegjistrim().MerrFAFSeriali(iddok);
            foreach (object id in rreshtat)
            {
                nrreshta++;
                for (int a=0; a<faf.Rows.Count; a++)
                {
                    if (id.ToString() == faf.Rows[a]["IDKOKAMAGAZINA"].ToString())
                    {
                        //err.Columns["Gabimi"].ExtendedProperties["WrapMode"] = true;
                        object[] arr = { faf.Rows[a]["MAGKODKONFIGAMBJENTE"] + " " + faf.Rows[a]["NRDOK"] + " " + faf.Rows[a]["DTDOK"], Environment.NewLine + "Ky dokument eshte i lidhur me FAF dhe nuk mund te fshihet! " + Environment.NewLine + faf.Rows[a]["DOKFAF"].ToString().Replace("&lt;/br&gt;", Environment.NewLine), nrreshta, faf.Rows[a]["NRFAF"] };
                        err.AddRow(arr);
                        break;
                    }
                }
            }
            return err;
        }
        /// <summary>
        /// riruaj nga ambjenti i listes se dokumentave te magazines
        /// </summary>
        /// <param name="ci"></param>
        /// <param name="rm"></param>
        /// <param name="mesazh"></param>
        /// <param name="rreshtat"></param>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="idNdermarrjeVit"></param>
        /// <param name="idGjuha"></param>
        /// <param name="eshteOwn"></param>
        /// <param name="hfArkiva"></param>
        /// <param name="stopojeTekErroriIPare"></param>
        /// <returns></returns>
        public static DataTable riruajMag(CultureInfo ci, ResourceManager rm, ref clsMesazh mesazh, List<object> rreshtat, int idPerdoruesi, int idNdermarrje, int idNdermarrjeVit, int idGjuha, bool eshteOwn, IDictionary<string, object> hfArkiva,  bool stopojeTekErroriIPare)
        {
            DataTable err = new DataTable();
            err.Columns.Add("Kodi");
            err.Columns.Add("Gabimi");
            err.Columns.Add("Rreshti");
            int nrreshta = 0;
            int max = rreshtat.Count;
            foreach (object id in rreshtat)
            {
                nrreshta++;
                clsKokaMagazina clsKoka = new clsKokaMagazina();
                clsKoka.mbushKokaMagazinaSipasID(Convert.ToInt32(id));
                if (clsKoka.IdStatusDok == 2)
                    continue;
                clsKonfigurimAmbjenti konfig = new clsKonfigurimAmbjenti(clsKoka.IdKonfigAmbjente);

                if (DbCore.MbylljePeriudhe.PeriodClosing.IsPeriodClosed(clsKoka.DtDok, IMBUtils.DataBase.MyConnectionsManager.GetSelectedConNameServer(), idNdermarrje, KategoriDokumenti.Magazina, clsKoka.IdKonfigAmbjente))
                {
                    object[] arr = { konfig.KodKonfigAmbjente + " " + clsKoka.NrDok + " " + clsKoka.DtDok.ToShortDateString(), IMBUtils.Messages.MessagesResource.Messages["msgPeriodIsClosed"], nrreshta };
                    err.Rows.Add(arr);
                    continue;
                }


                if (clsAlternativaKushti.getAlternativa(clsKoka.IdKonfigAmbjente, "V") == "Po")
                {
                    object[] arr = { konfig.KodKonfigAmbjente + " " + clsKoka.NrDok + " " + clsKoka.DtDok.ToShortDateString(), "Ky dokument eshte dokument vartes dhe nuk mund te riruhet!", nrreshta };
                    err.Rows.Add(arr);
                    continue;
                }

                colSerialeUnikeMagazina serialeUnike = new colSerialeUnikeMagazina(clsKoka.IdKokaMagazina, idNdermarrje);

                colSerialeUnikeKategori kategorite = new colSerialeUnikeKategori(idNdermarrje);
                mesazh = riruajMag(ci, rm, clsKoka, idPerdoruesi, idNdermarrje, idNdermarrjeVit, idGjuha, eshteOwn, hfArkiva,  stopojeTekErroriIPare,  konfig, serialeUnike, kategorite);

                if (!mesazh.Status)
                {
                    object[] arr = { konfig.KodKonfigAmbjente + " " + clsKoka.NrDok + " " + clsKoka.DtDok.ToShortDateString(), mesazh.PershkrimMesazhi, nrreshta };
                    err.Rows.Add(arr);
                    continue;
                }

            }
            return err;
        }
        /// <summary>
        /// riruatje per rivleresim
        /// </summary>
        /// <param name="ci"></param>
        /// <param name="rm"></param>
        /// <param name="rreshtat"></param>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="idNdermarrjeVit"></param>
        /// <param name="idGjuha"></param>
        /// <param name="eshteOwn"></param>
        /// <param name="hfArkiva"></param>
        /// <param name="e"></param>
        /// <param name="stopojeTekErroriIPare"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public static clsMesazh riruajMag(CultureInfo ci, ResourceManager rm, List<object> rreshtat, int idPerdoruesi, int idNdermarrje, int idNdermarrjeVit, int idGjuha, bool eshteOwn, IDictionary<string, object> hfArkiva, EO.Web.ProgressTaskEventArgs e, bool stopojeTekErroriIPare, DbCore.DbAdmin.clsLogRivleresimInventari log)
        {

            int nrreshta = 0;
            int max = rreshtat.Count;
            clsMesazh mesazh = new clsMesazh();

            colSerialeUnikeKategori kategorite = new colSerialeUnikeKategori(idNdermarrje);
            foreach (object id in rreshtat)
            {
                nrreshta++;
                clsKokaMagazina clsKoka = new clsKokaMagazina();
                clsKoka.mbushKokaMagazinaSipasID(Convert.ToInt32(id));
                if (clsKoka.IdStatusDok == 2)
                    continue;
                clsKonfigurimAmbjenti konfig = new clsKonfigurimAmbjenti(clsKoka.IdKonfigAmbjente);
                if (e.IsStopped)
                {
                    log.logStopRivleresim(DateTime.Now, "rivleresimi stoped", clsKoka.NrDok, clsKoka.DtDok);
                    e.UpdateProgress(100, " " + rm.GetString("msgRivleresimMagazineTekArtikulliMeKod", ci) + " " + clsKoka.NrDok + "  dhe date " + clsKoka.DtDok);
                    return new clsMesazh(false, "I stopuar nga perdoruesi");
                }
                colSerialeUnikeMagazina serialetUnike = new colSerialeUnikeMagazina(clsKoka.IdKokaMagazina, idNdermarrje);
                
                mesazh = riruajMag(ci, rm, clsKoka, idPerdoruesi, idNdermarrje, idNdermarrjeVit, idGjuha, eshteOwn, hfArkiva,  stopojeTekErroriIPare,  konfig, serialetUnike, kategorite);
                if (!mesazh.Status)
                {
                    log.logStopRivleresim(DateTime.Now, mesazh.PershkrimMesazhi, clsKoka.NrDok, clsKoka.DtDok);
                    return mesazh;
                }

                log.logRivleresim(clsKoka.NrDok, clsKoka.DtDok);
                //ritja e scrollit

                int roundedPosition = (int)Math.Round((double)((nrreshta / (double)max) * 100));
                if (e.Value != roundedPosition)
                    e.UpdateProgress(roundedPosition, " " + rm.GetString("msgRivleresimMagazineTekArtikulliMeKod", ci) + " " + clsKoka.NrDok + " dhe date " + clsKoka.DtDok);



            }
            return mesazh;
        }
        public static clsMesazh riruajMag(CultureInfo ci, ResourceManager rm, clsKokaMagazina clsKoka, int idPerdoruesi, int idNdermarrje, int idNdermarrjeVit, int idGjuha, bool eshteOwn, IDictionary<string, object> hfArkiva,  bool stopojeTekErroriIPare,  clsKonfigurimAmbjenti konfig, colSerialeUnikeMagazina serialetUnike, colSerialeUnikeKategori kategorite)
        {
            string mesazhmevonshem = "";
            List<object[]> mesazheInformueseAsete = null;
            clsMesazh mesazh = new clsMesazh();
            colTrupiMagazina col = new colTrupiMagazina();
            int idKategoria = DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKategori(clsKoka.IdKonfigAmbjente);
            bool ruajBarkod = clsAlternativaKushti.getAlternativa(clsKoka.IdKonfigAmbjente, "RBART") == "Po";
            col.mbushGjitheTrupiMagazinaNgaKoka(clsKoka.IdKokaMagazina);
            foreach (var trup in col)
                trup.MerrSerialetUnike(serialetUnike, clsKoka.IdLlojDokumentiMagazine == 2);
            DbCore.DbAsete.colSerialetMagazine colseriale = new DbCore.DbAsete.colSerialetMagazine();
            colseriale.merrSerialetMagazineSipasIDDokumenti(clsKoka.IdKokaMagazina, idNdermarrje, clsKoka.IdKonfigAmbjente);
            if (!stopojeTekErroriIPare)//gjate rivleresimit te mos ndryshoje statusi
            {
                foreach (DbCore.DbAsete.clsSerialetMagazine s in colseriale)
                {
                    s.IdStatusDokumenti = 1;
                }
            }
            DbCore.DbRegjistrim.clsKokaMagazina kokatra = new clsKokaMagazina();
            kokatra.mbushKokaMagazinaSipasIDGjenerues(clsKoka.IdKokaMagazina, 1, clsKoka.IdKonfigAmbjente);
            kokatra.OcolTrupiMagazina.mbushGjitheTrupiMagazinaNgaKoka(kokatra.IdKokaMagazina);
            if(kokatra.OcolTrupiMagazina.Count > 0 && serialetUnike.Count > 0)
            {
                var serialeClone = serialetUnike.Clone();
                serialeClone.ForEach(x => x.IdLlojDokumentMagazine = 1);
                foreach (var trupTr in kokatra.OcolTrupiMagazina)
                    trupTr.MerrSerialetUnike(serialeClone, false);
            }
            colArtikujt coleksistues1 = new colArtikujt(kokatra.IdKokaMagazina, new clsDatabaseInventari());
            int i2 = 0;
            foreach (clsTrupiMagazina trup in kokatra.OcolTrupiMagazina)
            {
                if (trup.IdLlojVeprimi == 1)
                    trup.Element = coleksistues1[i2];
                i2++;
            }
            colArtikujt colArt = new colArtikujt(clsKoka.IdKokaMagazina, new clsDatabaseInventari());
            int j2 = 0;
            foreach (clsTrupiMagazina trup in col)
            {
                if (!ruajBarkod)
                    trup.IdBarkodi = 0;
                if (trup.IdLlojVeprimi == 1)
                    trup.Element = colArt[j2];
                j2++;
            }
            DbCore.DbAsete.colSerialetMagazine colserialetransf = new DbCore.DbAsete.colSerialetMagazine();
            colserialetransf.merrSerialetMagazineSipasIDDokumenti(kokatra.IdKokaMagazina, idNdermarrje, kokatra.IdKonfigAmbjente);
            if (clsAlternativaKushti.getAlternativa(kokatra.IdKonfigAmbjente, "DOKMEKONF") == "Jo")//nqs nuk eshte dokument hyrje me konfirmim i veme statusdok 1
            {
                if (!stopojeTekErroriIPare)
                {
                    kokatra.IdStatusDok = 1;
                }
                int i = 0;
                foreach (DbCore.DbAsete.clsSerialetMagazine s in colserialetransf)
                {
                    if (!stopojeTekErroriIPare)
                        s.IdStatusDokumenti = 1;
                    s.IdAQTSeriali = colseriale[i].IdAQTSeriali;
                    i++;
                }
            }
            else
            {
                int i = 0;
                foreach (DbCore.DbAsete.clsSerialetMagazine s in colserialetransf)
                {
                    s.IdAQTSeriali = colseriale[i].IdAQTSeriali;
                    i++;
                }
            }
            kokatra.IdPerdoruesi = idPerdoruesi;

            clsKonfigurimAmbjenti konfamortizimihyrje = new clsKonfigurimAmbjenti();
            DbCore.DbShare.clsKusht kushtamorh = new DbCore.DbShare.clsKusht(kokatra.IdKonfigAmbjente, "ZDAM");
            konfamortizimihyrje.mbushKonfigAmbjSipasId(kushtamorh.Vlera, idGjuha);
            bool lidhur = clsKoka.eshteILidhur();
            bool autorizimet = DbCore.DbRegjistrim.clsKokaMagazina.kaAutorizime(clsKoka.IdKokaMagazina, idPerdoruesi);
            if (!autorizimet)
                lidhur = true;
            if (clsKoka.IdStatusDok == 1 && clsAlternativaKushti.getAlternativa(clsKoka.IdKonfigAmbjente, "LMD") == "Jo")
                lidhur = true;

            clsKokaRezervime rez = new clsKokaRezervime();
            rez.mbushKokaRezervimiSipasIDGjenerues(clsKoka.IdKokaMagazina, 2, clsKoka.IdKonfigAmbjente);
            clsKokaFleteKontabel kokfk = new clsKokaFleteKontabel(clsKoka.IdKokaMagazina, idKategoria);
            DbCore.DbQendraKosto.clsKokaQendraKosto qend = new DbCore.DbQendraKosto.clsKokaQendraKosto();
            qend.KtheKokaQKSipasIDGjeneruesDheKonfig(kokfk.IdKokaFleteKontabel, kokfk.IdKonfigAmbjente);
            #region validime
            if (clsKoka.IdGrup1 != 0)
            {
                DbCore.DbRegjistrim.clsGrupimDokumentiKoka grup = new clsGrupimDokumentiKoka(clsKoka.IdGrup1);
                if (grup.IdGrupimKoka < 1)
                    return new clsMesazh(false, rm.GetString("msgGrupimiIPareNukEkziston", ci));
            }
            if (clsKoka.IdGrup2 != 0)
            {
                DbCore.DbRegjistrim.clsGrupimDokumentiKoka grup = new clsGrupimDokumentiKoka(clsKoka.IdGrup2);
                if (grup.IdGrupimKoka < 1)
                    return new clsMesazh(false, rm.GetString("msgGrupimiDyteNukEkziston", ci));
            }
            if (clsKoka.IdGrup3 != 0)
            {
                DbCore.DbRegjistrim.clsGrupimDokumentiKoka grup = new clsGrupimDokumentiKoka(clsKoka.IdGrup3);
                if (grup.IdGrupimKoka < 1)
                    return new clsMesazh(false, rm.GetString("msgGrupimiITreteNukEkziston", ci));
            }

            if (clsKoka.IdLlogari != 0)
            {
                clsLlogari llogari = new clsLlogari(clsKoka.IdLlogari);

                if (llogari.IdLlogari < 1)
                    return new clsMesazh(false, rm.GetString("msgKjoLlogariNukEkziston", ci));

                else if (!clsLlogari.eshteLlogariAktive(llogari.NrLlogari, idNdermarrje))
                    return new clsMesazh(false, rm.GetString("msgKjoLlogariNukEshteAktive", ci));

            }
            if (clsKoka.IdNjesiVartese != 0)
            {
                if (konfig.KodKonfigAmbjente == "FDNV" || konfig.KodKonfigAmbjente == "FHNV")
                {
                    DbCore.DbInventari.clsNjesiVartese njesivartese = new DbCore.DbInventari.clsNjesiVartese(clsKoka.IdNjesiVartese);
                    if (njesivartese.IdNjesiVartese < 1)
                        return new clsMesazh(false, rm.GetString("msgNjesiaVarteseNukEkziston", ci));

                }
            }
            if (clsKoka.IdMagazina != 0)
            {
                DbCore.DbRegjistrim.clsNjesiAdministrative mag = new clsNjesiAdministrative(clsKoka.IdMagazina);
                if (mag.IdNjesiAdministrative == -1)
                    return new clsMesazh(false, rm.GetString("msgMagazinaNukEkziston", ci));

                else
                {
                    if (mag.Aktiv == false)
                        return new clsMesazh(false, rm.GetString("msgMagazinaNukEshteAktive", ci));
                }
            }
            if (clsKoka.IdDegeAdministrative != 0)
            {
                DbCore.DbRegjistrim.clsDegeAdministrative dege = new clsDegeAdministrative(clsKoka.IdDegeAdministrative);
                if (dege.IdDegeAdministrative == -1)
                    return new clsMesazh(false, rm.GetString("msgKjoDegeAdministrativeNukEkziston", ci));

                else
                {
                    if (dege.Aktiv == false)
                        return new clsMesazh(false, rm.GetString("msgKjoDegeAdministrativeNukEshteAktive", ci));

                }
            }
            if (kokatra.IdMagazina != 0)
            {
                DbCore.DbRegjistrim.clsNjesiAdministrative mag = new clsNjesiAdministrative(kokatra.IdMagazina);
                if (mag.IdNjesiAdministrative == -1)
                    return new clsMesazh(false, rm.GetString("msgMagazinaNukEkziston", ci));

                else
                {
                    if (mag.Aktiv == false)
                        return new clsMesazh(false, rm.GetString("msgMagazinaNukEshteAktive", ci));

                }

            }
            #endregion
            #region krijimi i kokes se re
            clsKokaMagazina kokare = new clsKokaMagazina();
            int meKontabilizim = clsAlternativaKushti.getAlternativa(clsKoka.IdKonfigAmbjente, "GJK") != "Jo" ? 1 : 0;
            string kodklienti = "", nrllogari = "", kodnjesivartese = "", koddege = "", kodmagazina = "", kodgrup1 = "";
            if (clsKoka.IdDegeAdministrative != 0)
            {
                clsDegeAdministrative dege = new clsDegeAdministrative(clsKoka.IdDegeAdministrative);
                koddege = dege.Kodi;
            }
            DbCore.DbShare.clsKonfigurimAmbjenti konfmag = new DbCore.DbShare.clsKonfigurimAmbjenti(konfig.IdKonfigurimi, idGjuha);
            if (clsKoka.IdMagazina != 0)
            {
                clsNjesiAdministrative magazina = new clsNjesiAdministrative(clsKoka.IdMagazina);
                kodmagazina = magazina.Kodi;
            }
            string shfaqmesazhapolupe = "Jo", mesazhinformues = "";
            if (clsKoka.IdGrup1 != 0)
            {
                clsGrupimDokumentiKoka grup1 = new clsGrupimDokumentiKoka(clsKoka.IdGrup1);
                kodgrup1 = grup1.Kodi;
            }
            if (clsKoka.IdLlogari != 0)
            {
                clsLlogari llogari = new clsLlogari(clsKoka.IdLlogari);
                nrllogari = llogari.NrLlogari;
            }
            if (clsKoka.IdNjesiVartese != 0)
            {
                DbCore.DbInventari.clsNjesiVartese njesivartese = new DbCore.DbInventari.clsNjesiVartese(clsKoka.IdNjesiVartese);
                kodnjesivartese = njesivartese.Kodi;
            }
            int idstatusdok = 1;
            if (clsAlternativaKushti.getAlternativa(clsKoka.IdKonfigAmbjente, "DOKMEKONF") == "Po" && clsKoka.IdLlojDokumentiMagazine == 1)
                idstatusdok = clsKoka.IdStatusDok;

            bool serialeNeDetajim = clsAlternativaKushti.getAlternativa(clsKoka.IdKonfigAmbjente, "TSD1") == "Po";
            mesazh = kokare.krijoMagazine(clsKoka.IdKokaMagazina, clsKoka.IdNivel, clsKoka.IdKonfigAmbjente, clsKoka.IdKlientFurnitor, kodklienti, clsKoka.IdMagazina, kodmagazina, clsKoka.DtDok, clsKoka.NrDok, clsKoka.IdProjekt, clsKoka.NrProjekt, idKategoria, clsKoka.Vlefta, idstatusdok, idNdermarrje, idNdermarrjeVit, idPerdoruesi, clsKoka.DtRegjistrimi, clsKoka.IdLlojDokumentiMagazine, clsKoka.Shenime, clsKoka.IdDegeAdministrative, koddege, clsKoka.IdLlogari, nrllogari, clsKoka.IdNjesiVartese, kodnjesivartese, clsKoka.MeKonfirmim, clsKoka.IdGrup1, clsKoka.IdGrup2, clsKoka.IdGrup3, clsKoka.Pershkrimi, clsKoka.Magazinieri, clsKoka.Adresa, col, kokatra, new clsKokaFleteKontabel(), rez.IdKokaRezervimi, out mesazhinformues, true, clsKoka.IdAutomjet, clsKoka.Targa, clsKoka.IdRaportDesing, idPerdoruesi, clsKoka.DtTransporti, clsKoka.Shoferi, clsKoka.TargaShoferi, clsKoka.NIVFSH, clsKoka.WTNIC, clsKoka.IdOperator, hfArkiva, false, clsKoka.IdKategoriSeriali, serialetUnike,false,clsKoka.NrSerial, new clsKokaRezervime(),clsKoka.MallraTeDjeghsme,clsKoka.ShoqerimIKerkuar,clsKoka.Tipi,clsKoka.Transaksioni,clsKoka.Transportuesi);
            if (!mesazh.Status)
            {
                return mesazh;
            }
            kokare.IdKokaMagazina = clsKoka.IdKokaMagazina;
            #endregion
            bool transferim = false;
            if (clsAlternativaKushti.getAlternativa(clsKoka.IdKonfigAmbjente, "DMT") == "Po")
                transferim = true;
            string pershkrimFK;
            if (kokare.Shenime != String.Empty)
                pershkrimFK = kokare.Shenime;
            else
                if (kokare.IdLlojDokumentiMagazine == 1)
                pershkrimFK = colKokaMagazina.pershkrimHyrjeFK;
            else
                pershkrimFK = colKokaMagazina.pershkrimDaljeFK;

            DbCore.DbShare.clsKusht kushtamor = new DbCore.DbShare.clsKusht(clsKoka.IdKonfigAmbjente, "ZDAM");
            DbCore.DbShare.clsKonfigurimAmbjenti konfamortizimi = new clsKonfigurimAmbjenti(kushtamor.Vlera, idGjuha);
            bool gjithmone = clsAlternativaKushti.getAlternativa(kokare.IdKonfigAmbjente, "GJKGJ") == "Po";
            bool bashkoArtikujt = clsAlternativaKushti.getAlternativa(kokare.IdKonfigAmbjente, "TD1S") == "Po";
            bool ruajRenditje = clsAtributeTrupi.merrVleredefaultSipasKontrollitDheKonfigurimit(kokare.IdKonfigAmbjente, "cbRenditje", 510).ToLower()== "true";
            if (lidhur == true)
                mesazh = kokare.modifiko(transferim, meKontabilizim, true, pershkrimFK, out shfaqmesazhapolupe, eshteOwn, colseriale, colserialetransf, konfamortizimi, konfamortizimihyrje, gjithmone, meKontabilizim, rm, ci, ruajRenditje, false,false,false, out mesazhmevonshem, false, ref mesazheInformueseAsete, kategorite, serialeNeDetajim, bashkoArtikujt,false);
            else
                mesazh = kokare.modifiko(transferim, meKontabilizim, false, pershkrimFK, out shfaqmesazhapolupe, eshteOwn, colseriale, colserialetransf, konfamortizimi, konfamortizimihyrje, gjithmone, meKontabilizim, rm, ci, ruajRenditje, false,false,false, out mesazhmevonshem, false, ref mesazheInformueseAsete, kategorite, serialeNeDetajim, bashkoArtikujt,false);
           
                return mesazh;
        }
        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbRegjistrim.clsKokaMagazina"/> 
        /// </summary>
        private void mbushKokatMagazina(DataTable dt, clsDatabaseRegjistrim db)
        {
            if (dt == null)
                return;
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsKokaMagazina koka = new clsKokaMagazina();
                    //koka.mbushKokaMagazina(rreshti, db);
                    Add(new clsKokaMagazina(rreshti, db));
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //    //throw;
            //}
        }

        #endregion


    }
}
