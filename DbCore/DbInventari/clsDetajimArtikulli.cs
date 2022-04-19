using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DbCore.DbKontabiliteti;
using System.Globalization;
using System.Resources;
using DbCore.IMBUtils.Logging;
using DbCore.DbRegjistrim;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbInventari
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne detajimet e artikujve
    ///  (Te dhenat  merren nga tabela : T_DETAJIMARTIKULLI)
    ///mban te dhenat mbi detajimet
    /// </summary>
    public class clsDetajimArtikulli
    {

        #region Atribute

        private int idDetajimArtikulli;
        private string kodDetajimArtikulli;
        private int llojDetajimArtikulli;
        private string pershkrimDetajimArtikulli;
        private string idNivelAutorizimi;
        //private int idNderViti;
        private int idPerdoruesi;
        private int kategoriDetajimi;
        private int idNdermarje;
        private int idStatusDok;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private int loan;
        private DataRow rreshti;       
        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdDetajimArtikulli
        {
            get { return idDetajimArtikulli; }
            set { idDetajimArtikulli = value; }
        }
        /// <summary>
        /// Kthen/Vendos kodi i detajimit.
        /// </summary>
        public string KodDetajimArtikulli
        {
            get { return kodDetajimArtikulli; }
            set { kodDetajimArtikulli = value; }
        }
        /// <summary>
        ///  tregon nese ky detajim eshte detajim normal apo loan (ndoshta dhe retention)
        /// </summary>
        public int Loan
        {
            get
            {
                return loan;
            }
            set
            {
                loan = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos pershkrimi i detajimit
        /// </summary>
        public string PershkrimDetajimArtikulli
        {
            get { return pershkrimDetajimArtikulli; }
            set { pershkrimDetajimArtikulli = value; }
        }
        /// <summary>
        /// Kthen/Vendos lloji i detajimit.
        /// </summary>
        public int LlojDetajimArtikulli
        {
            get { return llojDetajimArtikulli; }
            set { llojDetajimArtikulli = value; }
        }
        /// <summary>
        /// Kthen/Vendos niveli i autorizimit.
        /// </summary>
        public string IdNivelAutorizimi
        {
            get { return idNivelAutorizimi; }
            set { this.idNivelAutorizimi = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e ndermarje vitit.
        /// </summary>
        //public int IdNderViti
        //{
        //    get { return idNderViti; }
        //    set { idNderViti = value; }
        //}
        /// <summary>
        /// Kthen/Vendos ID-ne e perdoruesit.
        /// </summary>
        public int IdPerdoruesi
        {
            get { return idPerdoruesi; }
            set { idPerdoruesi = value; }
        }
        /// <summary>
        /// Kthen/Vendos kategoria e detajimit.
        /// </summary>
        public int KategoriDetajimi
        {
            get { return kategoriDetajimi; }
            set { kategoriDetajimi = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e ndermarjes.
        /// </summary>
        public int IdNdermarje
        {
            get { return idNdermarje; }
            set { idNdermarje = value; }
        }
        public int IdStatusDok
        {
            get { return idStatusDok; }
            set { idStatusDok = value; }
        }
        public DateTime DtKrijimi
        {
            get { return dtKrijimi; }

        }
        public DateTime DtModifikimi
        {
            get { return dtModifikimi; }

        }
        #endregion

        #region Konstruktoret

        /// <summary>
        /// konstruktori me parametra
        /// </summary>
        /// <param name="idDetajimArtikulli"> id ritese e detajimit</param>
        /// <param name="kodDetajimArtikulli">kodi i detajimit</param>
        /// <param name="llojDetajimArtikulli">lloji i detajimit</param>
        /// <param name="pershkrimDetajimArtikulli">peshkrimi i detajimit</param>
        /// <param name="idPerdoruesi"> id e perdoruesit qe e ka kryer veprimin</param>
        /// <param name="idNderViti"> id e ndermarje vitit</param>
        /// <param name="kategoriDetajimi"> kategoria e detajimit</param>
        /// <param name="idndermrje">id e ndermarjes</param>
        public clsDetajimArtikulli(int idDetajimArtikulli, string kodDetajimArtikulli, int llojDetajimArtikulli, string pershkrimDetajimArtikulli, int idPerdoruesi, int kategoriDetajimi, int idndermrje, string idnivelautorizimi, int idstatusdok, int loan)
        {
            this.idDetajimArtikulli = idDetajimArtikulli;
            this.kodDetajimArtikulli = kodDetajimArtikulli;
            this.llojDetajimArtikulli = llojDetajimArtikulli;
            this.pershkrimDetajimArtikulli = pershkrimDetajimArtikulli;
            //this.idNderViti = idNderViti;
            this.idPerdoruesi = idPerdoruesi;
            this.kategoriDetajimi = kategoriDetajimi;
            this.idNdermarje = idndermrje;
            this.idNivelAutorizimi = idnivelautorizimi;
            this.idStatusDok = idstatusdok;
            this.loan = loan;
        }

        /// <summary>
        /// konstruktori me parametra
        /// </summary>        
        /// <param name="kodDetajimArtikulli">kodi i detajimit</param>
        /// <param name="llojDetajimArtikulli">lloji i detajimit</param>
        /// <param name="pershkrimDetajimArtikulli">peshkrimi i detajimit</param>
        /// <param name="idPerdoruesi"> id e perdoruesit qe e ka kryer veprimin</param>
        /// <param name="idNderViti"> id e ndermarje vitit</param>
        /// <param name="kategoriDetajimi"> kategoria e detajimit</param>
        /// <param name="idndermrje">id e ndermarjes</param>
        public clsDetajimArtikulli(string kodDetajimArtikulli, int llojDetajimArtikulli, string pershkrimDetajimArtikulli, int idPerdoruesi, int kategoriDetajimi, int idndermrje, string idnivelautorizimi, int idstatusdok, int loan)
        {
            this.kodDetajimArtikulli = kodDetajimArtikulli;
            this.llojDetajimArtikulli = llojDetajimArtikulli;
            this.pershkrimDetajimArtikulli = pershkrimDetajimArtikulli;
            this.idPerdoruesi = idPerdoruesi;
            this.kategoriDetajimi = kategoriDetajimi;
            this.idNdermarje = idndermrje;
            this.idNivelAutorizimi = idnivelautorizimi;
            this.idStatusDok = idstatusdok;
            this.loan = loan;
        }

        public clsDetajimArtikulli(string kodDetajimArtikulli, int llojDetajimArtikulli, string pershkrimDetajimArtikulli, int idPerdoruesi, int kategoriDetajimi, int idndermrje, string idnivelautorizimi, int idstatusdok, int loan, bool shtim)
        {
            this.kodDetajimArtikulli = kodDetajimArtikulli;
            this.llojDetajimArtikulli = llojDetajimArtikulli;
            this.pershkrimDetajimArtikulli = pershkrimDetajimArtikulli;            
            this.idPerdoruesi = idPerdoruesi;
            this.kategoriDetajimi = kategoriDetajimi;
            this.idNdermarje = idndermrje;
            this.idNivelAutorizimi = idnivelautorizimi;
            this.idStatusDok = idstatusdok;
            this.loan = loan;           
            clsMesazh mesazh = kontrolloDetajim(shtim);
            if (!mesazh.Status)
                throw new MyException(mesazh.PershkrimMesazhi);
        }

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public clsDetajimArtikulli(int idDetajimArtikulli)
        {
            clsDatabaseInventari data = new clsDatabaseInventari();
            mbushDetajimArtikulli(data.merrDetajimArtikulli(idDetajimArtikulli));
            data.Dispose();
        }
     
        public clsDetajimArtikulli(int idDetajimArtikulli, clsDatabaseInventari data)
        {
            mbushDetajimArtikulli(data.merrDetajimArtikulli(idDetajimArtikulli));

        }

        public clsDetajimArtikulli(string kodDetajimArtikulli, int idNdermarrje)
        {
            mbushDetajimArtikulli(kodDetajimArtikulli, idNdermarrje);
        }

        public clsDetajimArtikulli()
        {
        }

        public clsDetajimArtikulli(DataRow rreshti)
        {
            
            mbushDetajimArtikulli(rreshti);
        }



        public static object[] ktheRowVleraDetajimMeKod(string kodi, int rreshti, int lloji, clsArtikulli artikulli, int idkokamagazina, int idNdermarrje, int idPerdorues, string magazina, DateTime dateDok, bool kontrolloImeiFifo, string[] listeIMEI, bool promocione, bool DokumentTransferimiOwn, bool date, bool merrDetajimPerSet)
        {
            object[] result = new object[8];
            result[0] = rreshti;
            result[2] = lloji;
            result[5] = artikulli.IdKategoriDetajimi2;
            clsDetajimArtikulli detArtikulli = new clsDetajimArtikulli();
            int idMag = clsNjesiAdministrative.ktheIdMagazine(magazina, idNdermarrje);
            detArtikulli.mbushDetajimArtikulliSipasKoditDheArtikullit(kodi, idNdermarrje, idPerdorues, artikulli.KodArtikulli, lloji);
            if (detArtikulli.IdDetajimArtikulli > 0)
            {
                if (idkokamagazina != 0)
                {
                    detArtikulli = new clsDetajimArtikulli();
                    if (!DokumentTransferimiOwn)
                        detArtikulli.ktheDetajimeSipasArtikullitDheKoditEkzistonTekRegjistrimi(kodi, idNdermarrje, idPerdorues, artikulli.KodArtikulli, lloji, idkokamagazina);
                    else
                        detArtikulli.ktheDetajimeSipasArtikullitDheKoditEkzistonTekRegjistrimiSiSerial(kodi, idNdermarrje, idPerdorues, artikulli.KodArtikulli, lloji, idkokamagazina);
                    if (detArtikulli.IdDetajimArtikulli > 0)
                    {
                        result[1] = detArtikulli;
                        result[3] = detArtikulli.KategoriDetajimi;

                        return result;
                    }
                    else
                    {
                        result[1] = new clsDetajimArtikulli();
                        result[3] = 2;
                        result[7] = kodi;
                        return result;
                    }
                }
                result[1] = detArtikulli;
                result[3] = detArtikulli.KategoriDetajimi;
                result[4] = new clsDetajimArtikulli(-1, "", 1, "", 0, 0, 0, "", 0, 0);

                result[6] = new clsMesazh(true);
                if (kontrolloImeiFifo)
                {
                    string kodmag = clsNjesiAdministrative.ktheKodiNjesiAdministrativeSipasiD(idMag, idPerdorues);
                    result[6] = clsDetajimArtikulli.KontrolloDetajimFundit(kodi, dateDok, kodmag, artikulli.KodArtikulli, idNdermarrje, listeIMEI, promocione);
                }
                if (lloji == 1 && (detArtikulli.KategoriDetajimi == 3 || detArtikulli.KategoriDetajimi == 4))
                {//ne kete rast duhet sugjeruar detajimi i dyte

                    colDetajimeArtikulli colDet2 = new colDetajimeArtikulli();
                    colDet2.mbushDetajimeSipasArtikullitNdermarrjesDheLlojit(artikulli.KodArtikulli, idNdermarrje, idPerdorues, 2);
                    double sasi = 0; DateTime dtMeHershme = new DateTime(9999, 12, 30); //dt maks

                    if (!date)
                    {
                        result[4] = new clsDetajimArtikulli(-1, "", 1, "", 0, 0, 0, "", 0, 0);
                        return result;
                    }
                    if (detArtikulli.KategoriDetajimi == 3 && artikulli.IdKategoriDetajimi2 == 4)
                    {
                        DateTime dtSeria;
                        foreach (clsDetajimArtikulli det in colDet2)
                        {
                            sasi = clsTrupiMagazina.merrSasiDetajimitDyteDheDetajimPare(artikulli, idMag, dateDok, detArtikulli.IdDetajimArtikulli, det.IdDetajimArtikulli);
                            dtSeria = clsTrupiMagazina.merrDateSipasDetajimPareDheDyte(artikulli, idMag, detArtikulli.IdDetajimArtikulli, dateDok, det.IdDetajimArtikulli);
                            if (sasi > 0 && dtSeria < dtMeHershme)
                            {
                                dtMeHershme = dtSeria;
                                result[4] = det;
                            }
                        }
                    }
                    if (detArtikulli.KategoriDetajimi == 4 && artikulli.IdKategoriDetajimi2 == 3)
                    {
                        foreach (clsDetajimArtikulli det in colDet2)
                        {
                            DateTime dtSkadence;
                            bool dateVlefshme = DateTime.TryParseExact(det.KodDetajimArtikulli, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtSkadence);
                            if (!dateVlefshme)
                                continue;
                            sasi = clsTrupiMagazina.merrSasiDetajimitDyteDheDetajimPare(artikulli, idMag, dateDok, detArtikulli.IdDetajimArtikulli, det.IdDetajimArtikulli);
                            if (sasi > 0 && dateVlefshme && dtSkadence < dtMeHershme)
                            {
                                dtMeHershme = dtSkadence;
                                result[4] = det;
                            }
                        }
                    }
                }
                return result;
            }
            else
            {
                string kodartikullitjeter = "";
                if (artikulli.KodArtikulli.EndsWith("_blere_nga_dealer"))
                    kodartikullitjeter = artikulli.KodArtikulli.Substring(0, artikulli.KodArtikulli.Length - "_blere_nga_dealer".Length);
                else kodartikullitjeter = artikulli.KodArtikulli + "_blere_nga_dealer";
                clsArtikulli artbd = new clsArtikulli(kodartikullitjeter, idNdermarrje);
                if (artbd.IdArtikulli > 0)
                {
                    DbCore.DbInventari.clsDetajimArtikulli detArtikulliTjeter = new DbCore.DbInventari.clsDetajimArtikulli();
                    detArtikulliTjeter.mbushDetajimArtikulliSipasKoditDheArtikullit(kodi, idNdermarrje, idPerdorues, kodartikullitjeter, lloji);

                    if (detArtikulliTjeter.IdDetajimArtikulli > 0)
                        result[6] = new clsMesazh(false, "IMEI " + detArtikulliTjeter.KodDetajimArtikulli + " nuk i perket artikullit " + artikulli.KodArtikulli + ". Ju lutem zgjidhni artikullin tjeter " + kodartikullitjeter + "!");
                    else result[6] = new clsMesazh(true);
                    result[1] = new clsDetajimArtikulli(-1, "", 1, "", 0, 0, 0, "", 0, 0);
                    result[4] = new clsDetajimArtikulli(-1, "", 1, "", 0, 0, 0, "", 0, 0);
                }
                else
                {
                    if (merrDetajimPerSet)
                        detArtikulli.mbushDetajimArtikulliSipasKoditDheArtikullit(kodi, idNdermarrje, idPerdorues, artikulli.KodArtikulli, lloji);
                    else
                        detArtikulli.mbushDetajimArtikulli(kodi, idNdermarrje);
                    if (detArtikulli.IdDetajimArtikulli > 0)
                    {
                        result[1] = detArtikulli;
                        result[4] = new clsDetajimArtikulli(-1, "", 1, "", 0, 0, 0, "", 0, 0);
                        result[6] = new clsMesazh((int)KodMesazhi.DetajimJoILidhur, false, "Detajimi nuk eshte i lidhur me artikullin.");
                    }
                    else {
                        result[1] = new clsDetajimArtikulli(-1, "", 1, "", 0, 0, 0, "", 0, 0);
                        result[4] = new clsDetajimArtikulli(-1, "", 1, "", 0, 0, 0, "", 0, 0);
                    }
                }
                
                if (lloji == 1)
                    result[3] = artikulli.IdKategoriDetajimi;
                else if (lloji == 2)
                    result[3] = artikulli.IdKategoriDetajimi2;
                else result[3] = 0;
                result[5] = artikulli.IdKategoriDetajimi2;
                return result;
            }
        }

        public static object[] ktheRowVleraDetajimMeID(int idja, int rreshti, int lloji, clsArtikulli artikulli, int idkokamagazina, int idNdermarrje, int idPerdorues, string magazina, DateTime dateDok, bool kontrolloImeiFifo, string[] listeIMEI, bool promocione, bool DokumentTransferimiOwn, bool date)
        {
            object[] result = new object[8];
            result[0] = rreshti;
            result[2] = lloji;
            result[5] = artikulli.IdKategoriDetajimi2;
            clsDetajimArtikulli detArtikulli = new clsDetajimArtikulli(idja);
            int idMag = clsNjesiAdministrative.ktheIdMagazine(magazina, idNdermarrje);
            string koddetajimi = detArtikulli.KodDetajimArtikulli;
            if (detArtikulli.IdDetajimArtikulli > 0)
            {
                if (idkokamagazina != 0)
                {
                    detArtikulli = new clsDetajimArtikulli();
                    detArtikulli.ktheDetajimeSipasArtikullitDheKoditEkzistonTekRegjistrimi(koddetajimi, idNdermarrje, idPerdorues, artikulli.KodArtikulli, lloji, idkokamagazina);

                    if (!DokumentTransferimiOwn)
                        detArtikulli.ktheDetajimeSipasArtikullitDheKoditEkzistonTekRegjistrimi(koddetajimi, idNdermarrje, idPerdorues, artikulli.KodArtikulli, lloji, idkokamagazina);
                    else
                        detArtikulli.ktheDetajimeSipasArtikullitDheKoditEkzistonTekRegjistrimiSiSerial(koddetajimi, idNdermarrje, idPerdorues, artikulli.KodArtikulli, lloji, idkokamagazina);

                    if (detArtikulli.IdDetajimArtikulli > 0)
                    {
                        result[1] = detArtikulli;
                        result[3] = detArtikulli.KategoriDetajimi;
                        return result;
                    }
                    else
                    {
                        result[1] = new clsDetajimArtikulli();
                        result[3] = 2;
                        result[7] = koddetajimi;
                        return result;
                    }
                }
                result[1] = detArtikulli;
                result[3] = detArtikulli.KategoriDetajimi;
                result[4] = new clsDetajimArtikulli(-1, "", 1, "", 0, 0, 0, "", 0, 0);
                result[6] = new clsMesazh(true);
                if (kontrolloImeiFifo)
                {
                    string kodimag = clsNjesiAdministrative.ktheKodiNjesiAdministrativeSipasiD(idMag, idPerdorues);
                    result[6] = clsDetajimArtikulli.KontrolloDetajimFundit(koddetajimi, dateDok, kodimag, artikulli.KodArtikulli, artikulli.IdNdermarje, listeIMEI, promocione);


                    string kodartikullitjeter = "";
                    if (artikulli.KodArtikulli.EndsWith("_blere_nga_dealer"))
                        kodartikullitjeter = artikulli.KodArtikulli.Substring(0, artikulli.KodArtikulli.Length - "_blere_nga_dealer".Length);
                    colDetajimePerArt coldet = new colDetajimePerArt();
                    coldet.mbushDetajimArtSipasIdArtikulli(artikulli.IdArtikulli);
                    if (!coldet.Exists(x => x.IdDetajimArtikulli == detArtikulli.IdDetajimArtikulli))
                        result[6] = new clsMesazh(false, "IMEI " + detArtikulli.KodDetajimArtikulli + " nuk i perket artikullit " + artikulli.KodArtikulli + ". Ju lutem zgjidhni artikullin tjeter " + kodartikullitjeter + "!");
                }
                if (lloji == 1 && (detArtikulli.KategoriDetajimi == 3 || detArtikulli.KategoriDetajimi == 4))
                {//ne kete rast duhet sugjeruar detajimi i dyte

                    colDetajimeArtikulli colDet2 = new colDetajimeArtikulli();
                    colDet2.mbushDetajimeSipasArtikullitNdermarrjesDheLlojit(artikulli.KodArtikulli, idNdermarrje, idPerdorues, 2);
                    double sasi = 0; DateTime dtMeHershme = new DateTime(9999, 12, 30); //dt maks

                    if (!date)
                    {
                        result[4] = new clsDetajimArtikulli(-1, "", 1, "", 0, 0, 0, "", 0, 0);
                        return result;
                    }
                    if (detArtikulli.KategoriDetajimi == 3 && artikulli.IdKategoriDetajimi2 == 4)
                    {
                        DateTime dtSeria;
                        foreach (clsDetajimArtikulli det in colDet2)
                        {
                            sasi = clsTrupiMagazina.merrSasiDetajimitDyteDheDetajimPare(artikulli, idMag, dateDok, detArtikulli.IdDetajimArtikulli, det.IdDetajimArtikulli);
                            dtSeria = clsTrupiMagazina.merrDateSipasDetajimPareDheDyte(artikulli, idMag, detArtikulli.IdDetajimArtikulli, dateDok, det.IdDetajimArtikulli);
                            if (sasi > 0 && dtSeria < dtMeHershme)
                            {
                                dtMeHershme = dtSeria;
                                result[4] = det;
                            }
                        }
                    }
                    if (detArtikulli.KategoriDetajimi == 4 && artikulli.IdKategoriDetajimi2 == 3)
                    {
                        foreach (clsDetajimArtikulli det in colDet2)
                        {
                            DateTime dtSkadence;
                            bool dateVlefshme = DateTime.TryParseExact(det.KodDetajimArtikulli, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtSkadence);
                            if (!dateVlefshme)
                                continue;
                            sasi = clsTrupiMagazina.merrSasiDetajimitDyteDheDetajimPare(artikulli, idMag, dateDok, detArtikulli.IdDetajimArtikulli, det.IdDetajimArtikulli);
                            if (sasi > 0 && dateVlefshme && dtSkadence < dtMeHershme)
                            {
                                dtMeHershme = dtSkadence;
                                result[4] = det;
                            }
                        }
                    }
                }
                return result;
            }
            else
            {
                result[1] = new clsDetajimArtikulli(-1, "", 0, "", 0, 0, 0, "", 0, 0);
                result[4] = new clsDetajimArtikulli(-1, "", 1, "", 0, 0, 0, "", 0, 0);
                result[6] = new clsMesazh(true);
                if (lloji == 1)
                    result[3] = artikulli.IdKategoriDetajimi;
                else if (lloji == 2)
                    result[3] = artikulli.IdKategoriDetajimi2;
                else result[3] = 0;
                return result;
            }
        }
        #endregion

        #region Metoda Publike



        /// <summary>
        /// mbush detajimet e artikjullit sipas kodit dhe ndermarrjes
        /// </summary>
        /// <param name="kod">kodi i ndermarrjes</param>
        /// <param name="idndermarje">id e ndermarrjes</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushDetajimArtikulli(string kod, int idndermarje)
        {
            using (clsDatabaseInventari dbDetajimArtikulli = new clsDatabaseInventari())
            {
                return mbushDetajimArtikulli(kod, idndermarje, dbDetajimArtikulli);
            }
        }

        public bool mbushDetajimArtikulli(string kod, int idndermarje, clsDatabaseInventari dbDetajimArtikulli)
        {
            return mbushDetajimArtikulli(dbDetajimArtikulli.merrDetajimArtikulli(kod, idndermarje));
        }

        public clsMesazh kontrollotransferim(clsDetajimArtikulli kod, int idndermarje, clsDatabaseInventari db, int idperdoruesi)
        {
            clsMesazh mesazh = new clsMesazh(true, "Transferimi mbaroi me sukses!");
            if (!db.ekzistonDetajim(kod.KodDetajimArtikulli, idndermarje))
            {
                //mesazh = kod.kontrolloVetemDateSkadence();
                // if (!mesazh.Status)
                //    return mesazh;
                kod.idPerdoruesi = idperdoruesi;
                kod.idNdermarje = idndermarje;
                mesazh = kod.ruaj(db);
                if (!mesazh.Status)
                    return mesazh;
            }
            else
            {
                clsDetajimArtikulli kodnderm = new clsDetajimArtikulli();
                kodnderm.mbushDetajimArtikulli(kod.kodDetajimArtikulli, idndermarje, db);
                kod.idPerdoruesi = idperdoruesi;
                kod.idNdermarje = idndermarje;
                kod.idDetajimArtikulli = kodnderm.idDetajimArtikulli;

                mesazh = kod.modifikoDetajim(kod.idDetajimArtikulli, kod.kodDetajimArtikulli, kod.llojDetajimArtikulli, kod.pershkrimDetajimArtikulli, kod.idPerdoruesi, kod.kategoriDetajimi, kod.idNdermarje, kod.idNivelAutorizimi, kod.idStatusDok, kod.loan, db);
                if (!mesazh.Status)
                    return mesazh;
            }
            return mesazh;
        }
        public bool mbushDetajimArtikulliSipasId(int idDetajimi)
        {
            clsDatabaseInventari dbDetajimArtikulli = new clsDatabaseInventari();
            bool sukses = mbushDetajimArtikulli(dbDetajimArtikulli.merrDetajimArtikulli(idDetajimi));
            dbDetajimArtikulli.Dispose();
            return sukses;
        }

        public bool mbushDetajimArtikulliSipasId(int idDetajimi, clsDatabaseInventari dbDetajimArtikulli)
        {
            bool sukses = mbushDetajimArtikulli(dbDetajimArtikulli.merrDetajimArtikulli(idDetajimi));
            return sukses;
        }

        public static DataRow merrDetajiminSipasId(int idDetajimi)
        {
            clsDatabaseInventari dbDetajimArtikulli = new clsDatabaseInventari();
            DataRow rreshti = dbDetajimArtikulli.merrDetajimArtikulli(idDetajimi);
            dbDetajimArtikulli.Dispose();
            return rreshti;
        }

        public static DataRow merrDetajimArtikulliSipasIdDr(int idDetajimi)
        {
            using (clsDatabaseInventari dbDetajimArtikulli = new clsDatabaseInventari())
                return dbDetajimArtikulli.merrDetajimArtikulliSipasIdDr(idDetajimi);
        }

        /// <summary>
        /// mbush detajimet e artikjullit sipas kodit dhe ndermarrjes
        /// </summary>
        /// <param name="kod">kodi i detajimit</param>
        /// <param name="idndermarje">id e ndermarrjes</param>
        /// <param name="idperdoruesi">id e perdoruesit</param>
        /// <param name="kodartikulli"> kodi i artikullit</param>
        /// <param name="lloji"> lloji i detajimit i pare apo i dyte</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushDetajimArtikulliSipasKoditDheArtikullit(string kod, int idndermarje, int idperdoruesi, string kodartikulli, int lloji)
        {
            clsDatabaseInventari dbDetajimArtikulli = new clsDatabaseInventari();
            bool sukses = mbushDetajimArtikulli(dbDetajimArtikulli.ktheDetajimeSipasArtikullitDheKodit(idndermarje, idperdoruesi, kod, kodartikulli, lloji));
            dbDetajimArtikulli.Dispose();
            return sukses;
        }
        public bool ktheDetajimeSipasArtikullitDheKoditEkzistonTekRegjistrimi(string kod, int idndermarje, int idperdoruesi, string kodartikulli, int lloji, int idkokamag)
        {
            clsDatabaseInventari dbDetajimArtikulli = new clsDatabaseInventari();
            bool sukses = mbushDetajimArtikulli(dbDetajimArtikulli.ktheDetajimeSipasArtikullitDheKoditEkzistonTekRegjistrimi(idndermarje, idperdoruesi, kod, kodartikulli, lloji, idkokamag));
            dbDetajimArtikulli.Dispose();
            return sukses;
        }
        public bool ktheDetajimeSipasArtikullitDheKoditEkzistonTekRegjistrimiSiSerial(string kod, int idndermarje, int idperdoruesi, string kodartikulli, int lloji, int idkokamag)
        {
            clsDatabaseInventari dbDetajimArtikulli = new clsDatabaseInventari();
            bool sukses = mbushDetajimArtikulli(dbDetajimArtikulli.ktheDetajimeSipasArtikullitDheKoditEkzistonTekRegjistrimiSiSerial(idndermarje, idperdoruesi, kod, kodartikulli, lloji, idkokamag));
            dbDetajimArtikulli.Dispose();
            return sukses;
        }
        public bool ktheDetajimeSipasArtikullitDheIDEkzistonTekRegjistrimi(int iddet, int idndermarje, int idperdoruesi, int idart, int lloji, int idkokamag, clsDatabaseInventari dbDetajimArtikulli)
        {

            bool sukses = mbushDetajimArtikulli(dbDetajimArtikulli.ktheDetajimeSipasArtikullitDheIDEkzistonTekRegjistrimi(idndermarje, idperdoruesi, iddet, idart, lloji, idkokamag));

            return sukses;
        }

        public clsMesazh ruaj()
        {
            clsDatabaseInventari db = new clsDatabaseInventari();
            db.beginTransaksion();

            clsMesazh mesazh = ruaj(db);
            if (!mesazh.Status)
                db.rollbackTransaksion();
            else
                db.commitTransaksion();

            return mesazh;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lidh">kur eshte true dmth qe do ruhet edhe lidhja e detajimit me artikujt</param>
        /// <returns></returns>       
        public clsMesazh ruaj(clsDatabaseInventari data)
        {
            int idDetajim = -1;
            try
            {
                clsMesazh mesazh = ruajDetajim(out idDetajim, this.KodDetajimArtikulli, this.LlojDetajimArtikulli, this.PershkrimDetajimArtikulli, this.IdPerdoruesi, this.KategoriDetajimi, this.IdNdermarje, this.IdNivelAutorizimi, this.idStatusDok, this.loan, data);

                this.idDetajimArtikulli = idDetajim;

                return mesazh;
            }
            catch
            {
                return new clsMesazh(false, "Ndodhi nje gabim gjate ruajtjes!");
            }
        }

        public clsMesazh ruajShpejte(clsArtikulli artikulli, int lloji)
        {
            //veprim: nqs eshte true do krijohet dhe do lidhet, nqs eshte false vetem do lidhet
            using (var scope = new MyTransactionScope())
            using (var data = new clsDatabaseInventari())
            {
                clsMesazh mesazh = ruajShpejte(artikulli, lloji, data);
                if (mesazh)
                {
                    scope.Complete();
                }
                return mesazh;
               
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lidh">kur eshte true dmth qe do ruhet edhe lidhja e detajimit me artikujt</param>
        /// <returns></returns>       
        public clsMesazh ruajShpejte(clsArtikulli artikulli, int lloji, clsDatabaseInventari data)
        {
            try
            {
                clsMesazh mesazh = ruaj(data);
                if (!mesazh.Status)
                    return mesazh;
                mesazh = clsDetajimPerArt.ruajLidhje(artikulli, this.idDetajimArtikulli, lloji, idNdermarje, idPerdoruesi, data);
                if (!mesazh.Status)
                    return mesazh;

                return mesazh;
            }
            catch
            {
                return new clsMesazh(false, "Ndodhi nje gabim gjate ruajtjes!");
            }
        }

        public clsMesazh kontrolloVetemDateSkadence()
        {
            if (this.KategoriDetajimi == (int)DbInventari.KategoriDetajimi.Date_skadence)
            {
                DateTime date;
                if (!DateTime.TryParseExact(this.KodDetajimArtikulli, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                    return new clsMesazh(false, String.Format("Kodi {0} nuk eshte ne formatin date!", this.KodDetajimArtikulli));
            }
            return new DbCore.clsMesazh(true, "Kontrolli u kalua me sukses!");
        }

        private clsMesazh kontrolloDetajim(bool shtim)
        {
            if (String.IsNullOrEmpty(kodDetajimArtikulli))
                return new clsMesazh(false, "Kodi nuk duhet te jete bosh!");

            if (String.IsNullOrEmpty(pershkrimDetajimArtikulli) && (kategoriDetajimi == (int)DbInventari.KategoriDetajimi.Detajim || kategoriDetajimi == (int)DbInventari.KategoriDetajimi.Serial))
                return new clsMesazh(false, "Emertimi nuk duhet te jete bosh!");

            if (kategoriDetajimi == (int)DbInventari.KategoriDetajimi.Date_skadence)
            {
                if (llojDetajimArtikulli != (int)LlojDetajimi.Date)
                    return new clsMesazh(false, "Kategoria Date skadence mund te kete vetem lloj Date!");
            }

            //if (kategoriDetajimi == (int)DbInventari.KategoriDetajimi.Seri)
            //{
            //    if (llojDetajimArtikulli != (int)LlojDetajimi.Numerik)
            //        return new clsMesazh(false, "Kategoria Seri mund te kete vetem lloj Numerik!");
            //}

            if (llojDetajimArtikulli == (int)LlojDetajimi.Numerik)
            {
                long numer = 0;
                if (!Int64.TryParse(kodDetajimArtikulli, out numer))
                    return new clsMesazh(false, String.Format("Kodi {0} nuk eshte ne formatin numerik!", kodDetajimArtikulli));

                if (!Int64.TryParse(pershkrimDetajimArtikulli, out numer)
                    &&(kategoriDetajimi == (int)DbInventari.KategoriDetajimi.Serial))
                    return new clsMesazh(false, String.Format("Emertimi {0} nuk eshte ne formatin numerik!", pershkrimDetajimArtikulli));
            }

            if (llojDetajimArtikulli == (int)LlojDetajimi.Date)
            {
                DateTime date;
                string[] formatet = new string[] { "dd/MM/yyyy", "dd/MM/yyyy HH:mm:ss"};
                if (!DateTime.TryParseExact(kodDetajimArtikulli, formatet, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                    return new clsMesazh(false, String.Format("Kodi {0} nuk eshte ne formatin date!", kodDetajimArtikulli));
                
                if (!DateTime.TryParseExact(pershkrimDetajimArtikulli, formatet, CultureInfo.InvariantCulture, DateTimeStyles.None, out date)
                    && (kategoriDetajimi == (int)DbInventari.KategoriDetajimi.Detajim || kategoriDetajimi == (int)DbInventari.KategoriDetajimi.Serial))
                    return new clsMesazh(false, String.Format("Emertimi {0} nuk eshte ne formatin date!", pershkrimDetajimArtikulli));

                kodDetajimArtikulli = Convert.ToDateTime(kodDetajimArtikulli).ToString("dd/MM/yyyy");
                switch (kategoriDetajimi)
                {
                    case (int)DbInventari.KategoriDetajimi.Detajim:
                    case (int)DbInventari.KategoriDetajimi.Serial:
                        pershkrimDetajimArtikulli = Convert.ToDateTime(pershkrimDetajimArtikulli).ToString("dd/MM/yyyy");
                        break;
                    case (int)DbInventari.KategoriDetajimi.Date_skadence:
                        if (DateTime.TryParseExact(pershkrimDetajimArtikulli, formatet, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                            pershkrimDetajimArtikulli = Convert.ToDateTime(pershkrimDetajimArtikulli).ToString("dd/MM/yyyy");
                        break;
                    default:
                        break;
                }
            }

            if (shtim && ekziston(kodDetajimArtikulli, idNdermarje))
                return new clsMesazh(false, String.Format("Ekziston detajimi me kod {0}!", kodDetajimArtikulli));

            return new clsMesazh(true, "Kontrollet u kaluan me sukses!");
        }

        /// <summary>
        /// Ruan nje objekt detajim artikulli sebashku me  autorizimet
        /// Nje objekt detajim artikulli ka nje koleksion me  autorizimet, 
        /// ruajtja e nje detajim artikulli imponon ruajtjen edhe te nje colection-i me  autorizimet
        /// Mqs cdo rresht i ri qe shtohet ne DB kerkon thirrjen e nje SP-je me parametra dhe detajimi bashke me  autorizimet konsiderohet si nje regjistrim,
        /// perdoret nje transaksion qe imponon regjistrimin e rregullt te nje detajimi sebashku me  autorizimet
        /// </summary>
        /// <param name="idDetajimArtikulli"> id ritese e detajimit</param>
        /// <param name="kodDetajimArtikulli">kodi i detajimit</param>
        /// <param name="llojDetajimArtikulli">lloji i detajimit</param>
        /// <param name="pershkrimDetajimArtikulli">peshkrimi i detajimit</param>
        /// <param name="idPerdoruesi"> id e perdoruesit qe e ka kryer veprimin</param>
        /// <param name="idNderViti"> id e ndermarje vitit</param>
        /// <param name="kategoriDetajimi"> kategoria e detajimit</param>
        /// <param name="idndermrje">id e ndermarjes</param>
        ///  <returns> kthen nje obj clsMesazh per te identifikuar statusin e ruajtjes se te dhenave ne DB</returns>     
        public clsMesazh ruajDetajim(out int idDetajim, string kodDetajimArtikulli, int llojDetajimArtikulli, string pershkrimDetajimArtikulli, int idPerdoruesi, int kategoriDetajimi, int idndermrje, string idnivelautorizimi, int idstatusdok, int loan, clsDatabaseInventari dbInv)
        {//ruan detajimArtikulli             
            idDetajim = 0;
            clsDatabaseKontabilitet dbkont = new clsDatabaseKontabilitet(dbInv);

            clsMesazh mesazh = dbInv.ruajDetajimArtikulli(out idDetajim, kodDetajimArtikulli, llojDetajimArtikulli, pershkrimDetajimArtikulli, idPerdoruesi, kategoriDetajimi, idndermrje, idstatusdok, loan);
            if (!mesazh.Status)
                return mesazh;

            DbAdmin.clsDatabaseAdmin dbAdmin = new DbAdmin.clsDatabaseAdmin(dbInv);
            if (!string.IsNullOrEmpty(idnivelautorizimi))
            {
                DbAdmin.colLidhjetAutorizim colLidhjet = new DbAdmin.colLidhjetAutorizim();
                string[] pars1 = idnivelautorizimi.Split(',');
                for (int i = 0; i < pars1.Length; i++)
                {
                    DbAdmin.clsLidhjeAutorizim lidhje = new DbAdmin.clsLidhjeAutorizim();
                    lidhje.IdAutorizimeKoka = DbAdmin.clsAutorizimKoka.ktheIDAutorizim(pars1[i], dbAdmin);
                    colLidhjet.Add(lidhje);
                }

                foreach (DbAdmin.clsLidhjeAutorizim o in colLidhjet)
                {
                    o.IdLloji = DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("DetajimArtikulli", dbkont);
                    o.IdLidhese = idDetajimArtikulli;
                    mesazh = dbAdmin.ruajLidhjeAutorizim(o.IdLidhjeAutorizim, o.IdLidhese, o.IdLloji, o.IdAutorizimeKoka, 1);
                    if (!mesazh.Status)
                        return mesazh;
                }
            }
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }

        public static clsMesazh KontrolloDetajimFundit(string kodKodBarArt, DateTime data, string kodMag, string kodartikulli, int idndermarje, string[] listeIMEI, bool promocione)
        {
            using (DbInventari.clsDatabaseInventari dbregj = new DbInventari.clsDatabaseInventari())
            {
                return KontrolloDetajimFundit(kodKodBarArt, data, kodMag, kodartikulli, idndermarje, listeIMEI, promocione, dbregj);
            }
        }
        public static clsMesazh KontrolloDetajimFundit(string kodKodBarArt, DateTime data, string kodMag, string kodartikulli, int idndermarje, string[] listeIMEI, bool promocione, DbInventari.clsDatabaseInventari dbregj)
        {
            promocione = false;///u be false sepse u futen artikujt e dealerit dhe ne promocione. u be ndryshimi vetem ketu qe nqs vodafoni ndryshon mendje ndryshimi te jete sa me i vogel pra thjesht te hiqet ky rresht
            DataTable dt = colDetajimeArtikulli.merrDetajimeSipasArtikullitMagazinesDheRadhesSeHyrjes(kodartikulli, idndermarje, kodMag, data, promocione, dbregj);
            if (dt.Rows.Count == 0 || dt.Select("detajim1='" + kodKodBarArt + "'").Length == 0)
                return new clsMesazh(-1, false, "Sasia e daljes është më e madhe se gjendja disponibel 0  e artikullit: " + kodartikulli + "!");

            if (listeIMEI.Length > 0)
            {
                foreach (string imei in listeIMEI)
                {
                    DataRow[] drekzistuese = dt.Select("detajim1='" + imei + "'");
                    if (drekzistuese.Length > 0)
                        dt.Rows.Remove(drekzistuese[0]);
                }
            }
            DateTime DataMeEHershme = DateTime.Parse(dt.Rows[0]["data"].ToString());
            DataRow[] drr = dt.Select("detajim1='" + kodKodBarArt + "'");
            if (drr.Length > 0)
                if (DateTime.Parse(drr[0]["data"].ToString()) == DataMeEHershme)
                    return new clsMesazh(1, true, "OK");
            return new clsMesazh(0, false, "Kujdes: ky IMEI nuk mund te shitet. Ju keni IMEI te tjere me te vjeter per kete aparat. Vendosni nje IMEI tjeter");
        }
        /// <summary>
        /// Ruan objektin detajim artikullit ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbInventari.clsDatabaseInventari.ruajDetajim"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>       
        public clsMesazh modifiko()
        {
            clsMesazh mesazh = new clsMesazh();
            clsDatabaseInventari db = new clsDatabaseInventari();
            db.beginTransaksion();
            mesazh = modifikoDetajim(idDetajimArtikulli, kodDetajimArtikulli, llojDetajimArtikulli, pershkrimDetajimArtikulli, idPerdoruesi, kategoriDetajimi, idNdermarje, idNivelAutorizimi, idStatusDok, loan, db);
            if (!mesazh.Status)
                db.rollbackTransaksion();
            else db.commitTransaksion();
            return mesazh;
        }
        public clsMesazh modifikoDetajim(int idDetajimArtikulli, string kodDetajimArtikulli, int llojDetajimArtikulli, string pershkrimDetajimArtikulli, int idPerdoruesi, int kategoriDetajimi, int idndermrje, string idnivelautorizimi, int idstatusdok, int loan, clsDatabaseInventari dbInv)
        {
            ImbLogger.LogTraceShitje("Filloi metoda modifikoDetajim!");
            
                clsDetajimArtikulli detajimArtikulli = new clsDetajimArtikulli(idDetajimArtikulli, kodDetajimArtikulli, llojDetajimArtikulli, pershkrimDetajimArtikulli, idPerdoruesi, kategoriDetajimi, idndermrje, idnivelautorizimi, idstatusdok, loan);
                DbAdmin.clsDatabaseAdmin dbAdmin = new DbAdmin.clsDatabaseAdmin(dbInv);
                
                clsDatabaseKontabilitet dbkont = new clsDatabaseKontabilitet(dbInv);

                clsMesazh mesazh;

                mesazh = dbInv.modifikoDetajimArtikulli(idDetajimArtikulli, kodDetajimArtikulli, llojDetajimArtikulli, pershkrimDetajimArtikulli, idPerdoruesi, kategoriDetajimi, idndermrje, idstatusdok, loan);
                //mesazh = modifikoDetajimArtikulli(detajimArtikulli);
                if (!mesazh.Status)
                {
                    return mesazh;
                }
                DbAdmin.colLidhjetAutorizim colLidhjet = new DbAdmin.colLidhjetAutorizim();
                if (detajimArtikulli.IdNivelAutorizimi != "" && detajimArtikulli.IdNivelAutorizimi != null)
                {
                    string[] pars1 = detajimArtikulli.IdNivelAutorizimi.Split(',');
                    for (int i = 0; i < pars1.Length; i++)
                    {
                        DbAdmin.clsLidhjeAutorizim lidhje = new DbAdmin.clsLidhjeAutorizim();
                        lidhje.IdAutorizimeKoka = DbAdmin.clsAutorizimKoka.ktheIDAutorizim(pars1[i],dbAdmin);
                        //lidhje.IdAutorizimeKoka = new DbAdmin.clsDatabaseAdmin().ktheAutorizim(pars1[i])[0].IdAutorizimKoka;
                        colLidhjet.Add(lidhje);
                    }
                }
                DbAdmin.colLidhjetAutorizim colLidhjetAutorizim = new DbAdmin.colLidhjetAutorizim(idDetajimArtikulli, "DetajimArtikulli", dbAdmin);
                for (int i = 0; i < colLidhjet.Count; i++)
                {
                    if (mesazh.Status)
                    {
                        int idAutorizimKoka = colLidhjet[i].IdAutorizimeKoka;
                        if (idAutorizimKoka == -1)
                            continue;
                        colLidhjet[i].IdLloji = DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("DetajimArtikulli", dbkont);
                        colLidhjet[i].IdLidhese = idDetajimArtikulli;
                        DbCore.DbAdmin.clsLidhjeAutorizim lidhjeNjejte = colLidhjetAutorizim.Find(x => x.IdAutorizimeKoka == idAutorizimKoka);
                        if (lidhjeNjejte != null)
                        {   //i heqim nga collectioni autorizimet qe sjane ndryshuar sepse ne te do ngelen vetem autorizimet qe do te fshihen(vendosen status 2) 
                            colLidhjetAutorizim.Remove(lidhjeNjejte);
                            continue;
                        }
                        mesazh = dbAdmin.ruajLidhjeAutorizim(colLidhjet[i].IdLidhjeAutorizim, colLidhjet[i].IdLidhese, colLidhjet[i].IdLloji, colLidhjet[i].IdAutorizimeKoka, 1);
                    }
                    else
                        return mesazh;
                }
                //fshihen autorizimet e vjetra. Ps: Jo ato qe kane ngelur njesoj, ato do ngelen si jane
                for (int j = 0; j < colLidhjetAutorizim.Count; j++)
                {
                    if (mesazh.Status)
                        mesazh = dbAdmin.fshiLidhjeAutorizim(colLidhjetAutorizim[j].IdLidhjeAutorizim);
                    else
                        return mesazh;
                }

                mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            ImbLogger.LogTraceShitje("Modifikimi perfundoi me sukses!");
            ImbLogger.LogTraceShitje("Mbaroi metoda modifikoDetajim!");
            return mesazh;
           
        }
        public clsMesazh fshiDetajim()
        {//fshin detajimArtikulli            
            return fshiDetajim(idDetajimArtikulli);
        }
        public static clsMesazh kaVeprimeDetajimRegj(int iddetajim, int idartikulli)
        {
            using (clsDatabaseInventari dbinv = new clsDatabaseInventari())
            {
              return   dbinv.kaVeprimeDetajimRegj(iddetajim, idartikulli);
            }
        }
        public static bool kontrolloDetajimLidhur(int idartikulli)
        {
            using (clsDatabaseInventari dbinv = new clsDatabaseInventari())
            {
                return dbinv.kontrolloDetajimLidhur(idartikulli);
            }
        }
        public static bool KontrolloDetajimLidhurSipasLlojit(int idartikulli, int lloji)
        {
            using (clsDatabaseInventari dbinv = new clsDatabaseInventari())
            {
                return dbinv.KontrolloDetajimLidhurSipasLlojit(idartikulli, lloji);
            }
        }
        /// <summary>
        /// fshin nje objekt detajimet sebashku me  autorizimet
        /// Nje objekt detajim artikulli ka nje koleksion me  autorizimet, 
        /// fshirja e nje detajim artikulli imponon fshirjen edhe te nje colection-i me  autorizimet
        /// Mqs cdo rresht i ri qe fshihet ne DB kerkon thirrjen e nje SP-je me parametra dhe detajim artikulli bashke me  autorizimet konsiderohet si nje regjistrim,
        /// perdoret nje transaksion qe imponon fshirjen e rregullt te nje detajimi sebashku me  autorizimet
        /// behet dhe kontrolli nese me kete detajim ka veprimi per te mos lejuar te fshihet nje detajim me te cilin ka veprime
        /// </summary>
        /// <param name="idDetajimArtikulli"> id ritese e detajimit</param>
        /// <returns> kthen nje obj clsMesazh per te identifikuar statusin e fshirjes se te dhenave ne DB</returns>
        public clsMesazh fshiDetajim(int idDetajimArtikulli)
        {//fshin detajimArtikulli            
            DbCore.DbInventari.clsDatabaseInventari detArtikulli = new clsDatabaseInventari();


            clsMesazh mesazh = new clsMesazh();
            try
            {
                detArtikulli.beginTransaksion();
                mesazh = detArtikulli.kaVeprimeDetajim(idDetajimArtikulli);
                if (mesazh.Status)
                {
                    detArtikulli.rollbackTransaksion();
                    return mesazh;
                }
                //colLlojeBuxhetesh colLloj = db.merrLlojBuxhetiSipasKodit("DetajimArtikulli");
                DbAdmin.colLidhjetAutorizim colLidhjeAutorizim = new DbAdmin.colLidhjetAutorizim(idDetajimArtikulli, "DetajimArtikulli");
                //DbAdmin.colLidhjetAutorizim colLidhjeAutorizim = new DbAdmin.clsDatabaseAdmin().merrLidhjeAutorizimSipasIdLidheseIdLloji(idDetajimArtikulli, DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("DetajimArtikulli"));
                DbAdmin.clsDatabaseAdmin dbAdmin = new DbAdmin.clsDatabaseAdmin(detArtikulli);
                foreach (DbAdmin.clsLidhjeAutorizim o in colLidhjeAutorizim)
                {
                    mesazh = dbAdmin.fshiLidhjeAutorizim(o.IdLidhjeAutorizim);
                    if (!mesazh.Status)
                    {
                        detArtikulli.rollbackTransaksion();
                        return mesazh;
                    }
                }
                mesazh = detArtikulli.fshiDetajimArtikulli(idDetajimArtikulli);
                if (!mesazh.Status)
                {
                    detArtikulli.rollbackTransaksion();
                    return mesazh;
                }
                detArtikulli.commitTransaksion();
                return new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            }
            catch (Exception ce)
            {
                detArtikulli.rollbackTransaksion();
                return new clsMesazh(false, ce.Message);
            }
        }
        /// <summary>
        /// Fshin objektin detajim artikullit ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbInventari.clsDatabaseInventari.fshiDetajim"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh fshi()
        {
            clsDatabaseInventari data = new clsDatabaseInventari();
            clsMesazh u_fshi = data.fshiDetajimArtikulliStatus(this.IdDetajimArtikulli, this.idPerdoruesi);
            data.Dispose();
            //clsMesazh u_fshi = data.fshiDetajim(this);
            return u_fshi;
        }
        public clsMesazh modifikoLoan(int iddetajim, int idperdorues, int loan)
        {
            clsDatabaseInventari db = new clsDatabaseInventari();
            clsMesazh mesazh = db.ndryshoLoanDetajimArt(iddetajim, idperdorues, loan);
            db.Dispose();
            return mesazh;
        }
        public static bool ekziston(string kodi, int idndermarje)
        {
            if (kodi == null || kodi == "")
                return false;

            clsDatabaseInventari data = new clsDatabaseInventari();
            bool ekziston = data.ekzistonDetajim(kodi, idndermarje);
            data.Dispose();
            //clsMesazh u_fshi = data.fshiDetajim(this);
            return ekziston;
        }
        
        public static bool ekziston(string kodi, int idndermarje, clsDatabaseInventari data)
        {
            if (kodi == null || kodi == "")
                return false;
            bool ekziston = data.ekzistonDetajim(kodi, idndermarje);

            //clsMesazh u_fshi = data.fshiDetajim(this);
            return ekziston;
        }

        /// <summary>
        /// Gjen nese ekziston nje detajim i lidhur me kete artikull
        /// </summary>
        /// <param name="kodi">kodi i detajimit</param>
        /// <param name="idndermarje">id e ndermarrjes</param>
        /// <param name="kodArt">kodi i artikullit</param>
        /// <param name="lloji">detajim i pare, apo i dyte</param>
        /// <returns>true nqs ekziston, false ne rast te kundert</returns>
        public static bool ekzistonDetajimLidhurMeArtikullin(string kodi, int idndermarje, string kodArt, int lloji)
        {
            using (clsDatabaseInventari data = new clsDatabaseInventari())
            {   if(kodi!=null && kodi!="")
                return  data.ekzistonDetajimLidhurMeArtikullin(kodi, idndermarje, kodArt, lloji);
                //clsMesazh u_fshi = data.fshiDetajim(this);
                return false;
            }
        }
        public static bool ekzistonDetajimLidhurMeArtikullin(string kodi, int idndermarje, string kodArt, int lloji, clsDatabaseInventari data)
        {
            if(kodi!=null && kodi!="")
            return data.ekzistonDetajimLidhurMeArtikullin(kodi, idndermarje, kodArt, lloji);

            //clsMesazh u_fshi = data.fshiDetajim(this);
            return false;
        }

        public static bool ekzistonDetajimLidhurMeArtikullinSipasId(int id, int idndermarje, string kodArt, int lloji, clsDatabaseInventari data)
        {
            if (id > 0)
                return data.ekzistonDetajimLidhurMeArtikullinSipasId(id, idndermarje, kodArt, lloji);

            //clsMesazh u_fshi = data.fshiDetajim(this);
            return false;
        }

        /// <summary>
        /// Gjen nese detajimi eshte i lidhur me ndonje artikull
        /// </summary>
        /// <param name="kodi">kodi i detajimit</param>
        /// <param name="idndermarje">id e ndermarrjes</param>
        /// <returns>true nqs ekziston, false ne rast te kundert</returns>
        public static bool eshteDetajimLidhurMeArtikull(string kodi, int idndermarje)
        {
            clsDatabaseInventari data = new clsDatabaseInventari();
            bool lidhur = data.eshteDetajimLidhurMeArtikull(kodi, idndermarje);
            data.Dispose();
            //clsMesazh u_fshi = data.fshiDetajim(this);
            return lidhur;
        }
        public static int ktheIdDetajimi(string kodi, int idNdermarrje)
        {
            clsDatabaseInventari data = new clsDatabaseInventari();
            return ktheIdDetajimi(kodi, idNdermarrje, data);
        }

        public static int ktheIdDetajimi(string kodi, int idNdermarrje, clsDatabaseInventari data)
        {
            if (!String.IsNullOrEmpty(kodi))
                return data.merrIdDetajimArtikulliSipasKodit(kodi, idNdermarrje);
            else
                return 0;
        }

        public static clsDetajimArtikulli krijoDetajimPerImport(string kategori, string lloji, string kodi, string emertimi, int idPerdoruesi, int idNdermarrje, int idStatusDok)
        {
            int idKategoria = ktheIdKategoriDetajimi(kategori);
            if (idKategoria == 0)
                throw new MyException(String.Format("Kategoria me vlere {0} nuk eshte e sakte!", kategori));

            int idLloji = ktheIdLlojDetajimi(lloji);
            if (idLloji == 0)
                throw new MyException(String.Format("Lloji me vlere {0} nuk eshte i sakte!", lloji));

            bool shtim = true;
            return new clsDetajimArtikulli(kodi, idLloji, emertimi, idPerdoruesi, idKategoria, idNdermarrje, "", idStatusDok, 0, shtim);
        }

        public static int ktheIdKategoriDetajimi(string kategori)
        {
            int idKategoria = 0;            
            switch (kategori.ToLower())
            {
                case "detajim":
                    idKategoria = (int)DbInventari.KategoriDetajimi.Detajim;
                    break;
                case "serial":
                    idKategoria = (int)DbInventari.KategoriDetajimi.Serial;
                    break;
                case "date skadence":
                    idKategoria = (int)DbInventari.KategoriDetajimi.Date_skadence;
                    break;
                case "seri":
                    idKategoria = (int)DbInventari.KategoriDetajimi.Seri;
                    break;
                default:
                    idKategoria = 0;
                    break;
            }
            return idKategoria;
        }

        public static int ktheIdLlojDetajimi(string lloji)
        {
            int idLloji = 0;
            switch (lloji.ToLower())
            {
                case "alfanumerik":
                    idLloji = (int)LlojDetajimi.Alfanumerik;
                    break;
                case "numerik":
                    idLloji = (int)LlojDetajimi.Numerik;
                    break;
                case "date":
                    idLloji = (int)LlojDetajimi.Date;
                    break;
                default:
                    idLloji = 0;
                    break;
            }
            return idLloji;
        }


        public static clsDetajimArtikulli KrijoDetajimNeseNukEkziston(string Detaj, int idperdoruesi, int idndermarje, int loan, clsArtikulli artikulli)
        {
            if (!clsDetajimArtikulli.ekziston(Detaj, idndermarje))
            {
                int llojDetajim = 0;
                if (artikulli.IdKategoriDetajimi == 3)
                    llojDetajim = 3;
                else if (artikulli.IdKategoriDetajimi == 2)
                    llojDetajim = 2;
                else if (artikulli.IdKategoriDetajimi == 4)
                    llojDetajim = 1;


                clsDetajimArtikulli detajim = new clsDetajimArtikulli(Detaj, 1, Detaj, idperdoruesi, artikulli.IdKategoriDetajimi, idndermarje, string.Empty, 1, loan); //TOCHECK EGI
                clsMesazh mesazh = detajim.ruaj();
                clsDetajimPerArt.ruajLidhje(artikulli, detajim.IdDetajimArtikulli, 1, idndermarje, idperdoruesi);
                return detajim;
            }
            else
            {
                clsDetajimArtikulli detajim = new clsDetajimArtikulli();
                detajim.mbushDetajimArtikulli(Detaj, idndermarje);
                detajim.Loan = loan;
                detajim.modifikoLoan(detajim.IdDetajimArtikulli, idperdoruesi, loan);
                return detajim;
            }
        }

        public static clsDetajimArtikulli MerrDetajimOseCelDheLidhNeseNukEkziston(string kodDetajimi, int idNdermarrje, int idPerdorues, clsArtikulli artikull, int lloji)
        {
            clsDetajimArtikulli detajim = new clsDetajimArtikulli();
            detajim.mbushDetajimArtikulli(kodDetajimi, idNdermarrje);
            int idKategoriDetajimi = (lloji == 1 ? artikull.IdKategoriDetajimi : artikull.IdKategoriDetajimi2);
            clsMesazh mesazh;
            if (detajim.IdDetajimArtikulli < 1)
            {
                if (idKategoriDetajimi == 3 || idKategoriDetajimi == 4)
                {
                    detajim = new clsDetajimArtikulli(kodDetajimi, idKategoriDetajimi == 3 ? 3 : 1, "", idPerdorues, idKategoriDetajimi, idNdermarrje, "", 1, 0, true);
                    mesazh = detajim.ruajShpejte(artikull, lloji);
                    if (!mesazh)
                        throw new MyException(mesazh.PershkrimMesazhi);
                }
                else
                    throw new MyException($"Detajimi i {(lloji == 1 ? "pare" : "dyte")} me kod " + kodDetajimi + " nuk ekziston!");
            }
            else
                if (!clsDetajimArtikulli.ekzistonDetajimLidhurMeArtikullinSipasId(detajim.IdDetajimArtikulli, idNdermarrje, artikull.KodArtikulli, lloji, new clsDatabaseInventari()))
            {
                mesazh = clsDetajimPerArt.ruajLidhje(artikull, detajim.IdDetajimArtikulli, lloji, idNdermarrje, idPerdorues);
                if (!mesazh)
                    throw new MyException(mesazh.PershkrimMesazhi);
            }
            return detajim;
        }
        #endregion

        #region Metoda Internal
        public bool mbushAutorizime() {
            DbAdmin.colLidhjetAutorizim lidhje = new DbAdmin.colLidhjetAutorizim(idDetajimArtikulli, "DetajimArtikulli");
            //DbAdmin.colLidhjetAutorizim lidhje = new DbAdmin.clsDatabaseAdmin().merrLidhjeAutorizimSipasIdLidheseIdLloji(idDetajimArtikulli, DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("DetajimArtikulli"));
            if (lidhje.Count != 0)
            {
                idNivelAutorizimi = DbAdmin.clsAutorizimKoka.ktheKodAutorizim(lidhje[0].IdAutorizimeKoka);
                //idNivelAutorizimi = new DbAdmin.clsDatabaseAdmin().ktheAutorizim()[0].KodiAutorizim;
                for (int i = 1; i < lidhje.Count; i++)
                    idNivelAutorizimi += "," + DbAdmin.clsAutorizimKoka.ktheKodAutorizim(lidhje[i].IdAutorizimeKoka);
                //idNivelAutorizimi += "," + new DbAdmin.clsDatabaseAdmin().ktheAutorizim(lidhje[i].IdAutorizimeKoka)[0].KodiAutorizim;
            }
            else
                idNivelAutorizimi = "";
            return true;
        }
        /// <summary>
        /// mbush detajimin e artikullit nga databaza
        /// </summary>
        /// <param name="dbDataRowDetajimArtikulli">merr nje datarow qe duhet mbushur</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushDetajimArtikulli(DataRow dbDataRowDetajimArtikulli)
        {
            ImbLogger.LogTraceShitje("Filloi metoda mbushDetajimArtikull!");
            if (dbDataRowDetajimArtikulli != null)
            {
                try
                {
                    int.TryParse(dbDataRowDetajimArtikulli["IDDETAJIMARTIKULLI"].ToString(), out idDetajimArtikulli);
                    kodDetajimArtikulli = dbDataRowDetajimArtikulli["KODDETAJIMARTIKULLI"].ToString();
                    int.TryParse(dbDataRowDetajimArtikulli["LLOJDETAJIMARTIKULLI"].ToString(), out llojDetajimArtikulli);
                    pershkrimDetajimArtikulli = dbDataRowDetajimArtikulli["PERSHKRIMDETAJIMARTIKULLI"].ToString();
                    int.TryParse(dbDataRowDetajimArtikulli["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    int.TryParse(dbDataRowDetajimArtikulli["LOAN"].ToString(), out loan);
                    int.TryParse(dbDataRowDetajimArtikulli["KATEGORIDETAJIMI"].ToString(), out kategoriDetajimi);
                    int.TryParse(dbDataRowDetajimArtikulli["IDNDERMARJE"].ToString(), out idNdermarje);
                    int.TryParse(dbDataRowDetajimArtikulli["IDSTATUSDOK"].ToString(), out idStatusDok);
                    DateTime.TryParse(dbDataRowDetajimArtikulli["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRowDetajimArtikulli["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    ImbLogger.LogTraceShitje("Mbaroi metoda mbushDetajimArtikull!");
                    return true;
                }
                catch (InvalidCastException)
                {
                    ImbLogger.LogErrorShitje("ERROR: Gabim gjate marrjes se detajimit te artikullit nga db-ja");
                    throw new Exception("ERROR: Gabim gjate marrjes se detajimit te artikullit nga db-ja");
                }
            }
            else
            {
                ImbLogger.LogTraceShitje("Mbaroi metoda mbushDetajimArtikull!");
                return false;
            }
        }

        public void mbushDetajimArtikulli(clsDetajimArtikulli clsDetajimArtikulli)
        {
            if (clsDetajimArtikulli != null)
            {
                IdDetajimArtikulli = clsDetajimArtikulli.IdDetajimArtikulli;
                kodDetajimArtikulli = clsDetajimArtikulli.kodDetajimArtikulli;
                llojDetajimArtikulli = clsDetajimArtikulli.llojDetajimArtikulli;
                pershkrimDetajimArtikulli = clsDetajimArtikulli.pershkrimDetajimArtikulli;
                idPerdoruesi = clsDetajimArtikulli.idPerdoruesi;
                loan = clsDetajimArtikulli.loan;
                kategoriDetajimi = clsDetajimArtikulli.kategoriDetajimi;
                idNdermarje = clsDetajimArtikulli.idNdermarje;
                idStatusDok = clsDetajimArtikulli.idStatusDok;
                dtKrijimi = clsDetajimArtikulli.dtKrijimi;
                dtModifikimi = clsDetajimArtikulli.dtModifikimi;
            }
        }

        #endregion

    }
}
