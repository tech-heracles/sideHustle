using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using DbCore.DbAdmin;
using DbCore.DbKontabiliteti;
using System.Globalization;
using System.Resources;
using DbCore.DbShare;
using DbCore.IMBUtils.DataBase;

namespace DbCore.DbTollona
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsBurime
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    /// <seealso cref="clsArtikullTolloni"/>
    public class colTollonaLeter : List<clsTollonaLeter>
    {
        #region Konstruktori

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public colTollonaLeter()
        {
        }


        /// <summary>
        /// konstruktori qe implementon klasen baze
        /// </summary>
        /// <param name="collection">koleksion me clsShitjeMeSerial</param>
        public colTollonaLeter(IEnumerable<clsTollonaLeter> collection)
            : base(collection)
        {

        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="clsShitjeMeSerial"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsTollonaLeter this[int index]
        {
            get
            {
                return ((clsTollonaLeter)base[index]);
            }
        }


        /// <summary>
        /// merr burim sipas ndermarje  ne forme data table
        /// </summary>
        /// <param name="idnderm"> idndermarje</param>
        /// <returns> kthen data table me keto burime</returns>
        public static DataTable merrTollonaLeterKonsumuaraPerImport(int idnderm, DateTime date)
        {
            using (clsDatabazeTollona db = new clsDatabazeTollona())
            {
                DataTable dt = db.merrTollonaLeterKonsumuaraPerImport(idnderm, date);
                return dt;
            }
        }
        public static DataTable merrTollonaLeterPerRaport(int idnderm, DateTime dtfillimi, DateTime dtmbarimi, DateTime dtfillimiexe, DateTime dtmbarimiexe)
        {
            using (clsDatabazeTollona db = new clsDatabazeTollona())
            {
                return db.merrTollonaLeterPerRaport(idnderm, dtfillimi, dtmbarimi, dtfillimiexe, dtmbarimiexe);
            }
        }
        public static DataTable merrTollonaLeterTrupiKonsumuaraPerImport(DateTime dtdok, string pikeshitje, int idndermarje)
        {
            using (clsDatabazeTollona db = new clsDatabazeTollona())
            {
                return db.merrTollonaLeterTrupiKonsumuaraPerImport(dtdok, pikeshitje, idndermarje);
            }
        }
        private static string kontrolloTrupShitjeTollona(int idshitje, string pikashitje, DateTime data, int idllogariparapagimi, DbCore.DbRegjistrim.colTrupiShitje trupi, int idNdermarrje, int idPerdorues, out bool isMagENjejte, bool shpk, bool eshteSpecifik)
        {
            DataTable dt = DbCore.DbTollona.colTollonaLeter.merrTollonaLeterTrupiKonsumuaraPerImport(data, pikashitje, idNdermarrje);
            string error = "";

            double totalimetvsh = 0, totalipatvsh = 0;
            int i = 1;
            DbCore.DbRegjistrim.clsNjesiAdministrative njesiadm = new DbCore.DbRegjistrim.clsNjesiAdministrative(pikashitje, idNdermarrje);
            int idMagTemp = -1;
            isMagENjejte = true;
            foreach (DataRow dr in dt.Rows)
            {
                DbCore.DbRegjistrim.clsTrupiShitje tr = new DbCore.DbRegjistrim.clsTrupiShitje();
                int artikulli = 0;
                double sasia = 0, cmime = 0, vleftapatvsh = 0, vleftametvsh = 0, vleftazbritje = 0;
                try
                {
                    artikulli = int.Parse(dr["idartikulli"].ToString());
                    sasia = double.Parse(dr["sasia"].ToString());
                    vleftapatvsh = double.Parse(dr["vlefta"].ToString());
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                }

                if (error != "")
                    continue;

                int idtaksa = 0; double tvsh = 0;

                DbCore.DbInventari.clsArtikulli art = new DbCore.DbInventari.clsArtikulli(artikulli);
                if (!shpk )
                    art .mbushArtikull(art.KodArtikulli, idNdermarrje);

                idtaksa = art.IdTvsh;
                DbCore.DbRegjistrim.clsTaksa taks = new DbCore.DbRegjistrim.clsTaksa(idtaksa);
             vleftametvsh    =vleftapatvsh *( (1 + double.Parse(taks.NormaPerqindje.ToString()) / 100));
                tvsh = vleftametvsh - vleftapatvsh;
            
                cmime = (vleftapatvsh) / sasia;
              

                totalimetvsh += vleftametvsh;
                totalipatvsh += vleftapatvsh;

                DbCore.DbRegjistrim.clsTrupiShitje trupiurdherbij = new DbCore.DbRegjistrim.clsTrupiShitje();

                int idllojveprimi = 1;//importohen vetem artikuj
                if (!eshteSpecifik)//nqs eshte klient specifik nuk na interesojne artikujt
                {
                    DbCore.clsMesazh mesazh = tr.krijoTrupShitje(i, idshitje, idllojveprimi, art.KodArtikulli, art.PershkrimArtikulli, -1, art.Njesi1Artikulli, sasia, cmime, vleftazbritje, vleftametvsh, idtaksa, vleftapatvsh, art.IdArtikulli, njesiadm.IdNjesiAdministrative, 0, 0, 0, "", data, data, 0, 0, 0, 0, 0, idNdermarrje, 0, -1, art, false, false, 0, "", 1, 0, 0, 0, 0,0);
                    if (mesazh.Status)
                    {
                        if (idMagTemp == -1)
                            idMagTemp = tr.IdMagazina;
                        else
                            if (isMagENjejte && tr.IdMagazina != idMagTemp)
                            isMagENjejte = false;
                        trupi.Add(tr);
                    }
                    else error = mesazh.PershkrimMesazhi;
                }
                i++;
            }
            if (shpk ||eshteSpecifik)
            {
                int niveltakse = 0;
                DbCore.DbRegjistrim.clsTrupiShitje trllog = new DbCore.DbRegjistrim.clsTrupiShitje();
                DbCore.DbKontabiliteti.clsLlogari llog = new clsLlogari(idllogariparapagimi);
                if (llog.NivelTakse == 0)
                {
                    clsNdermarrje nderm = new clsNdermarrje(idNdermarrje);
                    niveltakse = nderm.IdTakse;
                }
                else niveltakse = llog.NivelTakse;
                DbCore.clsMesazh mesazhllog = trllog.krijoTrupShitje(i, idshitje, 3, llog.NrLlogari, llog.EmerLlogari1, -1, 0, 1, eshteSpecifik ? totalipatvsh : -totalipatvsh, 0, eshteSpecifik ? totalimetvsh : -totalimetvsh, niveltakse, eshteSpecifik ? totalipatvsh : -totalipatvsh, llog.IdLlogari, njesiadm.IdNjesiAdministrative, 0, 0, 0, "", data, data, 0, 0, 0, 0, 0, idNdermarrje, 0, -1, llog, false, false, 0, "", 1, 0, 0, 0, 0,0);
                if (mesazhllog.Status)
                    trupi.Add(trllog);
                else error = mesazhllog.PershkrimMesazhi;
            }
            return error;
        }

        public static void kontrolloShitjeTollona(int idNdermarrje, DataTable dt, DataTable gabime, DataTable tePaImportuara, bool importo, int pozicionkodi, CultureInfo ci, ResourceManager rm, int idGjuha, bool eshteOwn, int idperdoruesi, int idndermvit)
        {
            DbData dbData = new DbData();
            DbShare.clsDatabaseShare dbS = new DbShare.clsDatabaseShare(dbData);
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti("FSHPTL", idNdermarrje,dbS);            
            DbCore.DbShare.clsKonfigurimAmbjenti konfblerje = new DbCore.DbShare.clsKonfigurimAmbjenti("FBPTL", idNdermarrje,dbS);            
            DbCore.DbShare.clsKonfigurimAmbjenti konfsha = new DbCore.DbShare.clsKonfigurimAmbjenti("FSHPT", idNdermarrje,dbS);            
            string error = "";
            //System.Globalization.CultureInfo ci = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo(idGjuha);

            clsNdermarrje ndermarje = new clsNdermarrje(idNdermarrje);

            int i = 1;

            int idstatusdok = 1;
            foreach (DataRow dr in dt.Rows)
            {
                DbCore.DbRegjistrim.clsKokaShitje koka = new DbCore.DbRegjistrim.clsKokaShitje();
                int idshitje;
                error = "";
                string pikeshitje, nrshitje;
                //  double vleftapatvsh;
                DateTime dateshitje;
                int idndermarje;
                try
                {
                    idshitje = int.Parse(dr["id"].ToString());

                    nrshitje = dr["nrdok"].ToString();

                    pikeshitje = dr["Pikeshitje"].ToString();
                    dateshitje = DateTime.Parse(dr["dtdok"].ToString());
                    idndermarje = int.Parse(dr["idndermarje"].ToString());
                    //  vleftapatvsh = double.Parse(dr["totali"].ToString());

                }
                catch (Exception ex)
                {
                    error = ex.Message;
                    object[] arr = { dr[pozicionkodi], ex.Message, i };
                    gabime.Rows.Add(arr);
                    if (importo)
                    {
                        tePaImportuara.ImportRow(dr);
                        gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                    }
                    continue;
                }

                if (error != "")
                    continue;
                clsKlientFurnitor kf = new clsKlientFurnitor();
                if (idndermarje == ndermarje.IdNdermarrje)
                    kf.mbushKlientFurnitorSipasKodit(pikeshitje, idNdermarrje);
                else kf.mbushKlientFurnitorSipasKodit("KastratiSHA", idNdermarrje);
                bool eshteSpecifik = kf.KlientSpecifik;

                bool gjenerodokmag;
                clsPeriudhaKontabel per = new clsPeriudhaKontabel(dateshitje, idNdermarrje);
                clsViti viti = new clsViti(idNdermarrje, dateshitje.Year.ToString());
                DbCore.DbRegjistrim.colTrupiShitje trupi = new DbCore.DbRegjistrim.colTrupiShitje();
                bool isMagENjejte = true;
                error = kontrolloTrupShitjeTollona(i, pikeshitje, dateshitje, kf.IdLlogariDytesore, trupi, idNdermarrje, idperdoruesi, out isMagENjejte, idndermarje == ndermarje.IdNdermarrje, eshteSpecifik);
                if (error != "")
                {
                    object[] arr = { dr[pozicionkodi], error, i };
                    gabime.Rows.Add(arr);

                    tePaImportuara.ImportRow(dr);
                    gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                    continue;
                }
                DbRegjistrim.colKonvertimi colkonv = new DbRegjistrim.colKonvertimi();

                clsLlogari llogkf = new clsLlogari(kf.IdLlogari);
                //clsMonedha mon = new clsMonedha(llogkf.IdMonedha);
                int idMonedha = llogkf.IdMonedha;
                string monedha = clsMonedha.ktheKodMonedheSipasId(idMonedha); //mon.KodiMonedha;

                double kursi = 1;
                clsKurset kurs = new clsKurset(idMonedha, dateshitje);
                if (kurs.VleraKursi != 0)
                    kursi = kurs.VleraKursi;
                clsAtributeTrupi atrib = new clsAtributeTrupi();
                atrib.mbushAtributSipasKompKonfDheKontrollit(idGjuha, eshteSpecifik ? konfblerje.IdKonfigAmbjente : (idndermarje == idNdermarrje ? konf.IdKonfigAmbjente : konfsha.IdKonfigAmbjente), "cmbMenyrePagese", 506);
                string kodmenyra = "";
                kodmenyra = clsFunksione.ktheMenyrePageseSipasID(Convert.ToInt32(atrib.VlereDefault)) == "" ? "Me mirebesim" : clsFunksione.ktheMenyrePageseSipasID(Convert.ToInt32(atrib.VlereDefault));
                clsAtributeTrupi atribrap = new clsAtributeTrupi();
                atribrap.mbushAtributSipasKompKonfDheKontrollit(idGjuha, eshteSpecifik ? konfblerje.IdKonfigAmbjente : (idndermarje == idNdermarrje ? konf.IdKonfigAmbjente : konfsha.IdKonfigAmbjente), "cmbFormatiPrintimit", 506);
                DbRegjistrim.clsNjesiAdministrative mag = new DbRegjistrim.clsNjesiAdministrative(pikeshitje, idNdermarrje);
                DbRegjistrim.clsDegeAdministrative dege = new DbRegjistrim.clsDegeAdministrative(mag.IdDegeAdministrative);
                int idMag = -1;
                string kodMag = "";
                if (isMagENjejte)
                {
                    idMag = trupi[0].IdMagazina;
                    kodMag = DbRegjistrim.clsNjesiAdministrative.ktheKodiNjesiAdministrativeSipasiDPaAutorizime(idMag);
                }
                clsKonfigurimAmbjenti konfmag = new clsKonfigurimAmbjenti(eshteSpecifik ? konfblerje.IdKonfigurimi : (idndermarje == idNdermarrje ? konf.IdKonfigurimi : konfsha.IdKonfigurimi));
                clsMesazh mesazh;
                if (!eshteSpecifik)
                    mesazh = koka.krijoShitjePerImport("FSH", idndermarje == idNdermarrje ? "FSHPTL" : "FSHPT", kf, "", dateshitje, nrshitje, "", dateshitje, monedha, idMonedha, kursi, "", kodmenyra, 0, DateTime.Now, idstatusdok, idNdermarrje, "", "", "Nga transferimet e tollonave", false, dege.Kodi, "", idperdoruesi, trupi, true, "", "", "", dateshitje, idperdoruesi, 0, "", 0, "", 0, "", "", false, false, dateshitje, dateshitje, "", 0, false, "", idndermvit, int.Parse(atribrap.VlereDefault), per, out gjenerodokmag, idndermarje == idNdermarrje ? konf : konfsha, idMag, kodMag, false, rm, ci, dateshitje, 0, 0, 0, konfmag, "", "", "", "", "", DateTime.Today, "", dbData,string.Empty, string.Empty, string.Empty, idGjuha, false, viti.IdViti, 0, false, false, new DbRegjistrim.clsKokaShitje(), "", "", "", "", "", DateTime.Now, null, DateTime.Now, "", "");
                else
                    mesazh = koka.krijoShitjePerImport("FB", "FBPTL", kf, "", dateshitje, nrshitje, "", dateshitje, monedha, idMonedha, kursi, "", kodmenyra, 0, DateTime.Now, idstatusdok, idNdermarrje, "", "", "Nga transferimet e tollonave", false, dege.Kodi, "", idperdoruesi, trupi, false, "", "", "", dateshitje, idperdoruesi, 0, "", 0, "", 0, "", "", false, false, dateshitje, dateshitje, "", 0, false, "", idndermvit, int.Parse(atribrap.VlereDefault), per, out gjenerodokmag, konfblerje, idMag, kodMag, false, rm, ci, dateshitje, 0, 0, 0, konfmag, "", "", "", "", "", DateTime.Today, "",dbData,string.Empty,string.Empty, string.Empty, idGjuha, false, viti.IdViti, 0, false, false, new DbRegjistrim.clsKokaShitje(), "","" ,"","", "", DateTime.Now, null, DateTime.Now, "", "");

                if (!mesazh.Status)
                {
                    object[] arr = { dr[pozicionkodi], mesazh.PershkrimMesazhi, i };
                    gabime.Rows.Add(arr);

                    tePaImportuara.ImportRow(dr);
                    gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                    continue;
                }

                string shfaqmesazhapolupe;
                string mesazhmevonshem = "";
                if (importo)
                {
                    try
                    {
                        DbArkaBanka.clsVeprimBankaKoka vep = new DbArkaBanka.clsVeprimBankaKoka();
                        string mesazhvdk, mesazhmag, mesazhbanka;
                        bool printofature, printogarancifature, pagesefature;
                        clsMesazh mesazhinv = koka.ruaj(idGjuha, "", true, null, per.IdPeriudha, colkonv, gjenerodokmag, out vep, 0, DbRegjistrim.StatusAprovimi.Undefined, 0, out mesazhmag, out mesazhbanka, out mesazhvdk, new DbRegjistrim.clsKokaShitje(), 0, 0, false, eshteOwn, false, "", new DbAsete.colSerialetMagazine(), new clsKonfigurimAmbjenti(), new DbRegjistrim.clsKokaShitje(), out printofature, out printogarancifature, out pagesefature, koka.IdStatusDok == 0 ? false : true, out shfaqmesazhapolupe, "FSHPT", false, "", 0, false, false, false, false, "", "", "", false, false, true, pikeshitje, false, false, false, false, false, new DbRegjistrim.colKokaShitje(), false, false, false, ref dbData, false, "","", false, out mesazhmevonshem, importo, true, "", "");

                        if (!mesazhinv.Status)
                        {
                            object[] arr = { dr[pozicionkodi], mesazhinv.PershkrimMesazhi, i };
                            gabime.Rows.Add(arr);

                            tePaImportuara.ImportRow(dr);
                            gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                            continue;
                        }
                    }
                    catch (Exception)
                    { }
                }
                i++;
            }
            return;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        /// </summary>
        /// <param name="dt">data table me te dhena te tipit burim</param>
        /// <returns>true ose false nqs objekti u mbush ne rregull me te dhena</returns>
        private bool mbushShitje(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsTollonaLeter artikull = new clsTollonaLeter();
                    //artikull.mbushShitje(rreshti);
                    Add(new clsTollonaLeter(rreshti));
                }
            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }

        #endregion
    }
}
