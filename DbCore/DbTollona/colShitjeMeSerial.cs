using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using DbCore.DbAdmin;
using DbCore.DbKontabiliteti;
using System.Globalization;
using System.Resources;
using DbCore.IMBUtils.DataBase;

namespace DbCore.DbTollona
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsBurime
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    /// <seealso cref="clsArtikullTolloni"/>
    public class colShitjeMeSerial : List<clsShitjeMeSerial>
    {
        #region Konstruktori

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public colShitjeMeSerial()
        {
        }


        /// <summary>
        /// konstruktori qe implementon klasen baze
        /// </summary>
        /// <param name="collection">koleksion me clsShitjeMeSerial</param>
        public colShitjeMeSerial(IEnumerable<clsShitjeMeSerial> collection)
            : base(collection)
        {

        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="clsShitjeMeSerial"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsShitjeMeSerial this[int index]
        {
            get
            {
                return ((clsShitjeMeSerial)base[index]);
            }
        }

        ///// <summary>
        ///// merr burimet sipas id ne formen e data row
        ///// </summary>
        ///// <param name="idburim"> id burimit</param>
        ///// <returns> kthen data row me kete burim</returns>
        //public static DataRow merrBurimSipasIdDR(int idburim)
        //{
        //    clsDatabazeTollona db = new clsDatabazeTollona();
        //   DataRow dr = db.merrBurimSipasIdDR(idburim);
        //    db.Dispose();
        //    return dr;
        //}

        /// <summary>
        /// merr burim sipas ndermarje  ne forme data table
        /// </summary>
        /// <param name="idnderm"> idndermarje</param>
        /// <returns> kthen data table me keto burime</returns>
        public static DataTable merrShitjeMeSerialKonsumuaraPerImport(int idnderm, DateTime date)
        {
            using (clsDatabazeTollona db = new clsDatabazeTollona())
            {
                return db.merrShitjeMeSerialKonsumuaraPerImport(idnderm, date);
            }
        }
        public static clsMesazh merrShitjeMeSerialKonsumuarandryshoCmimin( DateTime date)
        {
            using (clsDatabazeTollona db = new clsDatabazeTollona())
            {
                return db.merrShitjeMeSerialKonsumuarandryshoCmimin(date);
            }
        }
        public static DataTable merrShitjeMeSerialPerRaport(int idnderm)
        {
            using (clsDatabazeTollona db = new clsDatabazeTollona())
            {
                return db.merrShitjeMeSerialPerRaport(idnderm);
            }
        }
        public static DataTable merrShitjeMeSerialTrupiKonsumuaraPerImport(DateTime dtdok, string pikeshitje)
        {
            using (clsDatabazeTollona db = new clsDatabazeTollona())
            {
                return db.merrShitjeMeSerialTrupiKonsumuaraPerImport(dtdok, pikeshitje);
            }
        }
        private static string kontrolloTrupShitjeTollona(int idshitje, string pikashitje, DateTime data, int idllogariparapagimi, DbCore.DbRegjistrim.colTrupiShitje trupi, int idNdermarrje, int idPerdorues, out bool isMagENjejte)
        {
            DataTable dt = DbCore.DbTollona.colShitjeMeSerial.merrShitjeMeSerialTrupiKonsumuaraPerImport(data, pikashitje);
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

                idtaksa = art.IdTvsh;
                DbCore.DbRegjistrim.clsTaksa taks = new DbCore.DbRegjistrim.clsTaksa(idtaksa);
                DbCore.DbInventari.clsNivelCmimi niv = new DbInventari.clsNivelCmimi();
                niv.ktheNivelCmimiSipasKodit(pikashitje, idNdermarrje);
                if(niv.BrutoNetoNivelCmimi==0)

                vleftametvsh = vleftapatvsh * (1 + double.Parse(taks.NormaPerqindje.ToString()) / 100);
                else
                {
                    vleftametvsh = vleftapatvsh;
                    vleftapatvsh = vleftametvsh / (1 + double.Parse(taks.NormaPerqindje.ToString()) / 100);
                }
                tvsh = vleftametvsh - vleftapatvsh;
                cmime = (vleftapatvsh) / sasia;

                totalimetvsh += vleftametvsh;
                totalipatvsh += vleftapatvsh;

                DbCore.DbRegjistrim.clsTrupiShitje trupiurdherbij = new DbCore.DbRegjistrim.clsTrupiShitje();

                int idllojveprimi = 1;//importohen vetem artikuj
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
                i++;
            }
            int niveltakse = 0;
            DbCore.DbRegjistrim.clsTrupiShitje trllog = new DbCore.DbRegjistrim.clsTrupiShitje();
            DbCore.DbKontabiliteti.clsLlogari llog = new clsLlogari(idllogariparapagimi);
            if (llog.NivelTakse == 0)
            {
                clsNdermarrje nderm = new clsNdermarrje(idNdermarrje);
                niveltakse = nderm.IdTakse;
            }
            else niveltakse = llog.NivelTakse;
            DbCore.clsMesazh mesazhllog = trllog.krijoTrupShitje(i, idshitje, 3, llog.NrLlogari, llog.EmerLlogari1, -1, 0, 1, -totalipatvsh, 0, -totalimetvsh, niveltakse, -totalipatvsh, llog.IdLlogari, njesiadm.IdNjesiAdministrative, 0, 0, 0, "", data, data, 0, 0, 0, 0, 0, idNdermarrje, 0, -1, llog, false, false, 0, "", 1, 0, 0, 0, 0,0);
            if (mesazhllog.Status)
                trupi.Add(trllog);
            else error = mesazhllog.PershkrimMesazhi;
            return error;
        }

        public static void kontrolloShitjeTollona(int idNdermarrje, DataTable dt, DataTable gabime, DataTable tePaImportuara, bool importo, int pozicionkodi, CultureInfo ci, ResourceManager rm, int idGjuha, bool eshteOwn, int idperdoruesi, int idndermvit)
        {
            DbData dbData = new DbData();
            DbShare.clsDatabaseShare dbS = new DbShare.clsDatabaseShare(dbData);
            clsDatabaseAdmin dbA = new clsDatabaseAdmin(dbData);
            clsDatabaseKontabilitet dbK = new clsDatabaseKontabilitet(dbData);
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti("FSHPT", idNdermarrje, dbS);            
            string error = "";
            //System.Globalization.CultureInfo ci = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo(idGjuha);
            
            clsNdermarrje ndermarje = new clsNdermarrje(idNdermarrje, dbA);

            int i = 1;
            int idstatusdok = 1;
            foreach (DataRow dr in dt.Rows)
            {
                DbCore.DbRegjistrim.clsKokaShitje koka = new DbCore.DbRegjistrim.clsKokaShitje();
                int idshitje;

                string pikeshitje, nrshitje;
                //  double vleftapatvsh;
                DateTime dateshitje;
                try
                {
                    idshitje = int.Parse(dr["id"].ToString());

                    nrshitje = dr["nrdok"].ToString();

                    pikeshitje = dr["Pikeshitje"].ToString();
                    dateshitje = DateTime.Parse(dr["dtdok"].ToString());
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

                clsPeriudhaKontabel per = new clsPeriudhaKontabel(dateshitje, idNdermarrje);
                clsViti viti = new clsViti(idNdermarrje, dateshitje.Year.ToString());
                clsKlientFurnitor kf = new clsKlientFurnitor(pikeshitje, idNdermarrje,dbK);                

                DbCore.DbRegjistrim.colTrupiShitje trupi = new DbCore.DbRegjistrim.colTrupiShitje();
                bool isMagENjejte = true;
                error = kontrolloTrupShitjeTollona(i, pikeshitje, dateshitje, kf.IdLlogariDytesore, trupi, idNdermarrje, idperdoruesi, out isMagENjejte);
                if (error != "")
                {
                    object[] arr = { dr[pozicionkodi], error, i };
                    gabime.Rows.Add(arr);

                    tePaImportuara.ImportRow(dr);
                    gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                    continue;
                }
                DbCore.DbRegjistrim.colKonvertimi colkonv = new DbCore.DbRegjistrim.colKonvertimi();

                clsLlogari llogkf = new clsLlogari(kf.IdLlogari);
                //clsMonedha mon = new clsMonedha(llogkf.IdMonedha);
                int idMonedha = llogkf.IdMonedha;
                string monedha = clsMonedha.ktheKodMonedheSipasId(idMonedha); //mon.KodiMonedha;
                
                double kursi = 1;
                clsKurset kurs = new clsKurset(idMonedha, dateshitje);
                if (kurs.VleraKursi != 0)
                    kursi = kurs.VleraKursi;
                DbCore.DbShare.clsAtributeTrupi atrib = new DbCore.DbShare.clsAtributeTrupi();
                atrib.mbushAtributSipasKompKonfDheKontrollit(idGjuha, konf.IdKonfigAmbjente, "cmbMenyrePagese", 506);
                string kodmenyra = "";
                kodmenyra = clsFunksione.ktheMenyrePageseSipasID(Convert.ToInt32(atrib.VlereDefault)) == "" ? "Me mirebesim" : clsFunksione.ktheMenyrePageseSipasID(Convert.ToInt32(atrib.VlereDefault));
                DbShare.clsAtributeTrupi atribrap = new DbShare.clsAtributeTrupi();
                atribrap.mbushAtributSipasKompKonfDheKontrollit(idGjuha, konf.IdKonfigAmbjente, "cmbFormatiPrintimit", 506);
                DbRegjistrim.clsNjesiAdministrative mag = new DbRegjistrim.clsNjesiAdministrative(pikeshitje, idNdermarrje);
                DbRegjistrim.clsDegeAdministrative dege = new DbRegjistrim.clsDegeAdministrative(mag.IdDegeAdministrative);
                int idMag = -1;
                string kodMag = "";
                if (isMagENjejte)
                {
                    idMag = trupi[0].IdMagazina;
                    kodMag = DbRegjistrim.clsNjesiAdministrative.ktheKodiNjesiAdministrativeSipasiDPaAutorizime(idMag);
                }
                DbShare.clsKonfigurimAmbjenti konfmag = new DbShare.clsKonfigurimAmbjenti(konf.IdKonfigurimi);
                bool gjenerodokmag;
                clsMesazh mesazh = koka.krijoShitjePerImport("FSH", "FSHPT", kf, "", dateshitje, nrshitje, "", dateshitje, monedha, idMonedha, kursi, "", kodmenyra, 0, DateTime.Now, idstatusdok, idNdermarrje, "", "", "Nga transferimet e tollonave", false, dege.Kodi, "", idperdoruesi, trupi, true, "", "", "", dateshitje, idperdoruesi, 0, "", 0, "", 0, "", "", false, false, dateshitje, dateshitje, "", 0, false, "", idndermvit, int.Parse(atribrap.VlereDefault), per, out gjenerodokmag, konf, idMag, kodMag, false, rm, ci, dateshitje, 0, 0, 0, konfmag, "", "", "", "", "", DateTime.Today, "", dbData, string.Empty, string.Empty, string.Empty, idGjuha, false, viti.IdViti, 0, false, false, new DbRegjistrim.clsKokaShitje(), "", "", "", "", "", DateTime.Now, null, DateTime.Now, "", "");
               
                if (!mesazh.Status)
                {
                    object[] arr = { dr[pozicionkodi], mesazh.PershkrimMesazhi, i };
                    gabime.Rows.Add(arr);

                    tePaImportuara.ImportRow(dr);
                    gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                    continue;
                }
                string mesazhmevonshem = "";
                string shfaqmesazhapolupe;
                if (importo)
                {
                    try
                    {
                        DbArkaBanka.clsVeprimBankaKoka vep = new DbArkaBanka.clsVeprimBankaKoka();
                        string mesazhvdk, mesazhmag, mesazhbanka;
                        bool printofature, printogarancifature, pagesefature;
                        clsMesazh mesazhinv = koka.ruaj(idGjuha, "", true, null, per.IdPeriudha, colkonv, gjenerodokmag, out vep, 0, DbRegjistrim.StatusAprovimi.Undefined, 0, out mesazhmag, out mesazhbanka,
                            out mesazhvdk, new DbRegjistrim.clsKokaShitje(), 0, 0, false, eshteOwn, false, "", new DbAsete.colSerialetMagazine(), new DbShare.clsKonfigurimAmbjenti(), new DbRegjistrim.clsKokaShitje(),
                            out printofature, out printogarancifature, out pagesefature, koka.IdStatusDok == 0 ? false : true, out shfaqmesazhapolupe, "FSHPT", false, "", 0, false, false, false, false, "", "", "",
                            false, false, false, "", false, false, false, false, false, new DbRegjistrim.colKokaShitje(), false, false, false, ref dbData, false, "", "", false, out mesazhmevonshem, importo, true, "", "");

                        if (!mesazhinv.Status)
                        {
                            object[] arr = {dr[pozicionkodi], mesazhinv.PershkrimMesazhi, i};
                            gabime.Rows.Add(arr);

                            tePaImportuara.ImportRow(dr);
                            gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                            continue;
                        }
                    }
                    catch (Exception) {}
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
                    //clsShitjeMeSerial artikull = new clsShitjeMeSerial();
                    //artikull.mbushShitje(rreshti);
                    Add(new clsShitjeMeSerial(rreshti));
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
