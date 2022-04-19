using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DbCore.DbInventari;
using DbCore.DbKontabiliteti;
using DbCore.IMBUtils.DataBase;
using Newtonsoft.Json;
using DbCore.DbShare;
using DbCore.IMBUtils;
using DbCore.IMBUtils.Logging;
using System.Collections;
using DbCore.DbAsete;
using DbCore.IMBUtils.Messages;
using DbCore.IMBUtils.Extensions;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne  trupin e nje dokumenti  shitje ose blerje
    ///  (Te dhenat  merren nga tabela : T_TRUPISHITJE)
    /// </summary>
    public sealed class clsTrupiShitje
    {
        private static NLog.Logger logu = NLog.LogManager.GetCurrentClassLogger();
        #region Atribute

        private int idShitjeTrupi;
        private int idShitjeKoka;
        private int idLlojVeprimi;
        private String kodi;
        private String pershkrimi;
        private String pershkrim2;
        private int idDetajimArt;
        private int idDetajimArt2;
        private int idNjesia;
        private double sasia;
        private double cmimi;
        private double zbritje;
        private double vleftaPaTvsh;
        private int tvsh;
        private double vleftaMeTvsh;
        private int idMagazina;
        private int idBarkodi;
        private int idTrupiTransferimNga;
        //private String barkodi;
        /// <summary>
        /// id e kodit te artikullit/llogarise/macros ect
        /// </summary>
        private int idKodi;
        /// <summary>
        /// gjeresia e artikullit per te dhene sasine
        /// </summary>
        private double gjeresi;
        /// <summary>
        /// gjatesia e artikullit per te dhene sasine
        /// </summary>
        private double gjatesi;
        /// <summary>
        /// sasia e artikullit per kete gjatesi dhe kete gjeresi
        /// </summary>
        private double sasiPermasa;
        private String kodDetajim1;
        private String kodDetajim2;
        private String shenime;
        private DateTime dtFillimi;
        private DateTime dtMbarimi;
        /// <summary>
        /// sasia e mbetur pas ekzekutimeve
        /// </summary>
        private double sasimbetur;
        private double sasiRez;
        /// <summary>
        /// id e trupit te dokumentit nga eshte konvertuar
        /// </summary>
        private int idTrupiKonvertimi;
        private int idTrupiRezervimi;
        private int idTrupiTransferimi;
        private int idLlogShpenzimi;
        private string nrLlogShpenzimi;
        private object element;
        private int idTrupiKthim;
        private int idTrupiKonvertimBlerje;
        private int zbritjeNeVlere;
        private double zbritjeVlere;
        public DbCore.DbRegjistrim.colDetajimeArtikulliRegjistrim OColDetajimet;
        private DataRow rreshti;
        private string shenime2;
        private int idKategoriShpenzimi;
        private double sasiaEMbetur;
        private double vleraKomisionit;
        private bool meKomision;
        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases me parametra
        /// </summary>
        /// <param name="cm">cmimi </param>
        /// <param name="IdDetaj"> id e detajimit te artikullit</param>
        /// <param name="idLlojVep"> id e llojit te veprimit</param>
        /// <param name="idMagazina"> id e magazines</param>
        /// <param name="idNjes"> id e njesise se artikullit</param>
        /// <param name="idShiKoka"> id e kokes se dokumentit te shitje blerjes</param>
        /// <param name="idShiTrup">id ritese e trupit te dokumentit te shitjes blerjes</param>
        /// <param name="kod"> kodi i artikullit, llogarise etj</param>
        /// <param name="pershkr"> pershkrimi i artikullit, llogarise etj</param>
        /// <param name="sas"> sasia</param>
        /// <param name="tv"> tvsh</param>
        /// <param name="vlMeTv">vlefta me tvsh</param>
        /// <param name="vlPaTv"> vlefta pa tvsh</param>
        /// <param name="idkodi">kodi i artikullit/llogarise/makro ect</param>
        /// <param name="zbr">zbritja analitike</param>
        public clsTrupiShitje(int idShiTrup, int idShiKoka, int idLlojVep, string kod, string pershkr, int IdDetaj, int idNjes, double sas, double cm,
            double zbr, double vlMeTv, int tv, double vlPaTv, int idkodi, int idMagazina, double gjeresi, double gjatesi, double sasipermasa, string shenime,
            DateTime dtfill, DateTime dtmb, int idtrupikonvertimi, double sasirez, int idtrupirez, int idtrupitransferim, int idLlogariShpenz, int idtrupikthimi,
            int idtrupikonvertimblerje, object elem, string shenime2, int llojzbritje, double vlerazbritje, int idBarkodi, int idTrupiTransferimNga, string kodDetajim1, string KodDetajim2)
        {
            idShitjeTrupi = idShiTrup;
            idShitjeKoka = idShiKoka;
            idLlojVeprimi = idLlojVep;
            kodi = kod;
            pershkrimi = pershkr;
            idDetajimArt = IdDetaj;
            idNjesia = idNjes;
            sasia = sas;
            cmimi = cm;
            zbritje = zbr;
            vleftaPaTvsh = vlPaTv;
            tvsh = tv;
            vleftaMeTvsh = vlMeTv;
            idKodi = idkodi;
            this.gjatesi = gjatesi;
            this.gjeresi = gjeresi;
            this.sasiPermasa = sasipermasa;
            this.idMagazina = idMagazina;
            this.shenime = shenime;
            dtFillimi = dtfill;
            dtMbarimi = dtmb;
            this.sasiRez = sasirez;
            idTrupiKonvertimi = idtrupikonvertimi;
            this.idTrupiRezervimi = idtrupirez;
            this.idTrupiTransferimi = idtrupitransferim;
            this.idLlogShpenzimi = idLlogariShpenz;
            this.idTrupiKthim = idtrupikthimi;
            this.shenime2 = shenime2;
            this.idTrupiKonvertimBlerje = idtrupikonvertimblerje;
            this.element = elem;
            this.zbritjeNeVlere = llojzbritje;
            this.zbritjeVlere = vlerazbritje;
            this.idBarkodi = idBarkodi;
            this.idTrupiTransferimNga = idTrupiTransferimNga;
            this.kodDetajim1 = kodDetajim1;
            this.kodDetajim2  = KodDetajim2;


            OColDetajimet = new colDetajimeArtikulliRegjistrim();
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idtrupi">id e kokes</param>
        public clsTrupiShitje(int idtrupi)
        {
            clsDatabaseRegjistrim dbTrupShitje = new clsDatabaseRegjistrim();
            mbushTrupShitje(dbTrupShitje.ktheTrupiShitjeSipasID(idtrupi));
            dbTrupShitje.Dispose();
        }

        public clsTrupiShitje(int idtrupishitje, clsDatabaseRegjistrim dbTrupShitje)
        {
            mbushTrupShitje(dbTrupShitje.ktheTrupiShitjeSipasID(idtrupishitje));
        }

        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsTrupiShitje()
        {
        }

        public clsTrupiShitje(int idNdermarrje, int idPerdoruesi, Dictionary<string, string> rreshtDokuKlient, bool isShitje, bool konvertim, bool meme, bool merrSipasGrupit, string kodgrupi, bool merrDhurata, bool ownshop, bool gjenerodokumentmagazine, bool klonim, bool kthim, string veprimi, bool tollon, int rreshti, IDictionary<string, object> hfSeriale, IDictionary<string, object> hfIdGride, double perqindjeZbritje, DbCore.DbAsete.colSerialetMagazine colserialemag, bool kontrollosasi, double kursi, int statusDokumenti, DbCore.DbShare.clsKonfigurimAmbjenti konfmag, bool tollonkastrati, bool konvertimblerje, bool zevendesimtollonakastrati, bool kthimVod, bool blerjengadealer, bool shitjevodafone, int nrRreshtit, bool ruajbarkod,bool meKomision,bool lejoMagNdryshme, DateTime dtDok, bool lejoSasiPozitiveKthim, ref int nrRendorSerial)
        {
            //  DbAdmin.colGridaTrupi colGrida = new DbAdmin.colGridaTrupi(idKomp, idKonfigurimAmbiente);    
            string kodi = rreshtDokuKlient["txtKodi"].ToString();
            if (kodi == string.Empty || kodi == null || kodi == "null")
                return;
            string lloji = rreshtDokuKlient["cmbLloji"].ToString();

            //int idtrupitransferim;
            int.TryParse(rreshtDokuKlient["txtIdKodi"].ToString(), out this.idKodi);
            int.TryParse(rreshtDokuKlient["txtIdTrupi"].ToString(), out this.idShitjeTrupi);
            int.TryParse(rreshtDokuKlient["txtIdTrupiKonvertimi"].ToString(), out this.idTrupiKonvertimi);
            int.TryParse(rreshtDokuKlient["txtIdTrupiKonvertimBlerje"].ToString(), out this.idTrupiKonvertimBlerje);
            int.TryParse(rreshtDokuKlient["txtIdTrupiRezervimi"].ToString(), out this.idTrupiRezervimi);
            int.TryParse(rreshtDokuKlient["txtIdTrupiTransferimi"].ToString(), out this.idTrupiTransferimi);
            int.TryParse(rreshtDokuKlient["txtIdTrupiKthim"].ToString(), out this.idTrupiKthim);
            string pershkr = rreshtDokuKlient["txtPershkrimi"].ToString();
            string detajimi = rreshtDokuKlient["txtDetajimi"].ToString();
            string detajimi2 = rreshtDokuKlient["txtDetajimi2t"].ToString();
            string njesia = rreshtDokuKlient["cmbNjesia"].ToString();
            double.TryParse(rreshtDokuKlient["txtSasia"].ToString(), out this.sasia);
            double.TryParse(rreshtDokuKlient["txtSasiMbetur"].ToString(), out this.sasimbetur);
            double.TryParse(rreshtDokuKlient["txtSasiaRez"].ToString(), out this.sasiRez);
            double.TryParse(rreshtDokuKlient["txtGjeresi"].ToString(), out this.gjeresi);
            double.TryParse(rreshtDokuKlient["txtGjatesi"].ToString(), out this.gjatesi);
            double.TryParse(rreshtDokuKlient["txtSasiPermase"].ToString(), out this.sasiPermasa);
            double.TryParse(rreshtDokuKlient["txtCmimi"].ToString(), out this.cmimi);
            double.TryParse(rreshtDokuKlient["txtZbritja"].ToString(), out this.zbritje);
            if (rreshtDokuKlient["txtLlojZbritje"].ToString() == "Perqindje")
                this.zbritjeNeVlere = 1;
            else this.zbritjeNeVlere = 2;
            double.TryParse(rreshtDokuKlient["txtZbritjaVlere"].ToString(), out this.zbritjeVlere);
            double.TryParse(rreshtDokuKlient["txtVleftaTVSH"].ToString(), out this.vleftaPaTvsh);
            string tvsh = rreshtDokuKlient["cbTVSH"].ToString();
            double.TryParse(rreshtDokuKlient["txtVlefta"].ToString(), out this.vleftaMeTvsh);
            string magazina = rreshtDokuKlient["txtMagazina"].ToString();
            string shenime = rreshtDokuKlient["txtShenime"].ToString();
            //string seriale = Convert.ToString(rreshtDokuKlient["txtSerial"]);
            string nrLlogShpenz = rreshtDokuKlient["txtIdLlogShpenzimi"].ToString();
            DateTime.TryParse(rreshtDokuKlient["txtDtFillimi"].ToString()!=""?rreshtDokuKlient["txtDtFillimi"].ToString():DateTime.Now.ToString(), out this.dtFillimi);
            DateTime.TryParse(rreshtDokuKlient["txtDtMbarimi"].ToString()!=""? rreshtDokuKlient["txtDtMbarimi"].ToString() : DateTime.Now.ToString(), out this.dtMbarimi);
            string shenime2 = rreshtDokuKlient["txtShenime2"].ToString();
            string barkodi = ruajbarkod ? rreshtDokuKlient["txtKodbari"].ToString() : String.Empty;
            clsKategoriShpenzimi kat = new clsKategoriShpenzimi(rreshtDokuKlient["txtKategoriShpenzimi"].ToString(), idNdermarrje);
            idKategoriShpenzimi = kat.Id;
             double.TryParse(rreshtDokuKlient["txtVleraKomisionit"].ToString(),out this.vleraKomisionit);
            this.meKomision = meKomision;

            clsArtikulli art = new clsArtikulli();
            clsLlogari llog = new clsLlogari();
           
            int idTvsh = clsTaksa.ktheIdTakse(tvsh, idNdermarrje);
            int idBarkod = clsKodbari.MerrIdBarkodiSipasPershkrimDheArtikulli(barkodi, this.IdKodi);
            int idNjesi = 0;
            if (njesia != string.Empty && njesia != null && njesia != "null")
                idNjesi = clsNjesiArtikulli.ktheIdNjesiArtikulli(njesia, idNdermarrje);
            
            kontrolloTrupShitje(lloji, art, llog, idNdermarrje, idPerdoruesi, veprimi, gjenerodokumentmagazine, merrSipasGrupit, kodgrupi, merrDhurata, konvertim, klonim, kthim, konvertimblerje, kthimVod, blerjengadealer, pershkr, detajimi, isShitje, ownshop, shitjevodafone, meme, detajimi2, idTvsh, magazina, nrLlogShpenz, tollon, tollonkastrati, zevendesimtollonakastrati, idBarkod, kodi, shenime, shenime2, idNjesi, nrRreshtit, lejoMagNdryshme, dtDok, lejoSasiPozitiveKthim);
            if (idLlojVeprimi == 1)
                nrRendorSerial++;
            if (art.LlojiArt)
            {
                int serialCounter = 0;
                if (hfSeriale.ContainsKey(this.idKodi + "_" + hfIdGride[rreshti.ToString()]))
                {
                    var dokumenti = JsonConvert.DeserializeObject<Dictionary<string, object>[]>(hfSeriale[this.idKodi + "_" + hfIdGride[rreshti.ToString()]].ToString());
                    for (int i = 0; i < dokumenti.Length; i++)
                    {
                        DbCore.DbAsete.clsAQTSeriale serial = new DbCore.DbAsete.clsAQTSeriale(dokumenti[i]);
                        //col.Add(serial);
                        serialCounter++;
                        if (serial.IdAQTArt != this.idKodi)
                            throw new Exception("Serialet e artikullit " + this.Kodi + " jane marre gabim!");
                        DbCore.DbAsete.clsSerialetMagazine serialmag = new DbCore.DbAsete.clsSerialetMagazine(0, 0, nrRendorSerial, this.idKodi, serial.IdAQTSerial, konfmag.IdNivel, konfmag.IdKonfigAmbjente, this.idMagazina, art.MeSerial ? 1 : (float)(this.sasia * (double)(this.idNjesia == art.Njesi1Artikulli ? 1 : art.KoeficientArtikulli)), float.Parse((this.vleftaPaTvsh * (1 - perqindjeZbritje) / (this.sasia * (double)(this.idNjesia == art.Njesi1Artikulli ? 1 : art.KoeficientArtikulli))).ToString()) * Convert.ToSingle(kursi), float.Parse((this.vleftaPaTvsh * (1 - perqindjeZbritje) / (this.sasia * (double)(this.idNjesia == art.Njesi1Artikulli ? 1 : art.KoeficientArtikulli))).ToString()) * (art.MeSerial ? 1 : (float)((this.sasia * (double)(this.idNjesia == art.Njesi1Artikulli ? 1 : art.KoeficientArtikulli)))) * Convert.ToSingle(kursi), statusDokumenti, idNdermarrje, idPerdoruesi, idPerdoruesi, 0);
                        colserialemag.Add(serialmag);
                    }
                }
                if (kontrollosasi && ((this.sasia != serialCounter && art.MeSerial && serialCounter != 0)))
                    throw new DbCore.MyException(MessagesResource.Messages["msgSasiaArtikullit"] + this.kodi + MessagesResource.Messages["msgEshteENdryshmeNgaSeriaESerialeve"]);
                if (art.MeSerial && this.sasia != Math.Truncate(this.sasia))
                    throw new DbCore.MyException("Nuk lejohet sasi me presje dhjetore per artikullin " + this.kodi + " sepse eshte me serial!");


            }
            switch ((llojRreshtiShitje)idLlojVeprimi)
            {
                case llojRreshtiShitje.Artikull:
                    if (art.IdArtikulli == 0)
                        art = new clsArtikulli(kodi, idNdermarrje);
                    this.element = art;
                    break;
                case llojRreshtiShitje.Llogari:
                    if (llog.IdLlogari == 0)
                        llog = new clsLlogari(kodi, idNdermarrje);
                    this.element = llog;
                    break;
            }
        }

        internal clsTrupiShitje ShallowCopy()
        {
            return (clsTrupiShitje)this.MemberwiseClone();
        }

        public clsTrupiShitje(int idNdermarrje, int idPerdoruesi, bool isShitje, bool konvertim, bool meme, bool merrSipasGrupit, string kodgrupi, bool merrDhurata, bool ownshop, bool gjenerodokumentmagazine, bool klonim, bool kthim, string veprimi, bool tollon, bool tollonkastrati, bool konvertimblerje, bool zevendesimtollonakastrati, bool kthimVod, bool blerjengadealer, string kodi, string lloji, int idKodi, int idShitjeTrupi, int idTrupiKonvertimi, int idTrupiKonvertimBlerje, int idTrupiRezervimi, int idTrupiTransferimi, int idTrupiKthim, string pershkr, string detajimi, string detajimi2, int idNjesia, double sasia, double sasiRez, double gjeresi, double gjatesi, double sasiPermasa, double cmimi, double zbritje, int llojZbritje, double zbritjeVlere, double vleftaPaTvsh, int idTvsh, double vleftaMeTvsh, int idMagazina, string shenime, string nrLlogShpenz, DateTime dtFillimi, DateTime dtMbarimi, string shenime2, int idBarkodi, int idKategoriShpenzimi, string kodKonfigKVDN, int nrRreshtit, bool lejoMagNdryshme, DateTime dtDok,bool lejoSasiPozitiveKthim)
        {
            bool shitjevodafone = false;
            string magazina = clsNjesiAdministrative.ktheKodiNjesiAdministrativeSipasiDPaAutorizime(idMagazina);
            if (magazina.Length == 3 && kodKonfigKVDN.EndsWith(magazina))
                shitjevodafone = true;

            if (kodi == string.Empty || kodi == null || kodi == "null")
                return;

            this.idKodi = idKodi;
            this.idShitjeTrupi = idShitjeTrupi;
            this.idTrupiKonvertimi = idTrupiKonvertimi;
            this.idTrupiKonvertimBlerje = idTrupiKonvertimBlerje;
            this.idTrupiRezervimi = idTrupiRezervimi;
            this.idTrupiTransferimi = idTrupiTransferimi;
            this.idTrupiKthim = idTrupiKthim;
            this.sasia = sasia;
            this.sasiRez = sasiRez;
            this.gjeresi = gjeresi;
            this.gjatesi = gjatesi;
            this.sasiPermasa = sasiPermasa;
            this.cmimi = cmimi;
            this.zbritje = zbritje;
            this.zbritjeNeVlere = llojZbritje;
            this.zbritjeVlere = zbritjeVlere;
            this.vleftaPaTvsh = vleftaPaTvsh;
            this.vleftaMeTvsh = vleftaMeTvsh;
            this.dtFillimi = dtFillimi;
            this.dtMbarimi = dtMbarimi;
            this.idKategoriShpenzimi = idKategoriShpenzimi;


            clsArtikulli art = new clsArtikulli();
            clsLlogari llog = new clsLlogari();

            kontrolloTrupShitje(lloji, art, llog, idNdermarrje, idPerdoruesi, veprimi, gjenerodokumentmagazine, merrSipasGrupit, kodgrupi, merrDhurata, konvertim, klonim, kthim, konvertimblerje, kthimVod, blerjengadealer, pershkr, detajimi, isShitje, ownshop, shitjevodafone, meme, detajimi2, idTvsh, magazina, nrLlogShpenz, tollon, tollonkastrati, zevendesimtollonakastrati, idBarkodi, kodi, shenime, shenime2, idNjesia, nrRreshtit, lejoMagNdryshme, dtDok, lejoSasiPozitiveKthim);

            switch ((llojRreshtiShitje)idLlojVeprimi)
            {
                case llojRreshtiShitje.Artikull:
                    this.element = art;
                    break;
                case llojRreshtiShitje.Llogari:
                    this.element = llog;
                    break;
            }
        }

        public void kontrolloTrupShitje(string lloji, clsArtikulli art, clsLlogari llog, int idNdermarrje, int idPerdoruesi, string veprimi, bool gjenerodokumentmagazine, bool merrSipasGrupit, string kodgrupi, bool merrDhurata, bool konvertim, bool klonim, bool kthim, bool konvertimblerje, bool kthimVod, bool blerjengadealer, string pershkr, string detajimi, bool isShitje, bool ownshop, bool shitjevodafone, bool meme, string detajimi2, int idTvsh, string magazina, string nrLlogShpenz, bool tollon, bool tollonkastrati, bool zevendesimtollonakastrati, int idBarkod, string kodArt, string shenim, string shenim2, int idNjesi, int nrRreshtit, bool lejoMagNdryshme, DateTime dtDok, bool lejoSasiPozitiveKthim)
        {
            if (lloji == "Artikull")
            {

                art.ktheArtikullSipasKoditDheAutorizime(kodArt, idNdermarrje, idPerdoruesi);
                if (art.IdArtikulli == 0)
                {
                    throw new MyException("Artikulli me kod: " + kodArt + " nuk ekziston ose nuk keni autorizime!");
                }

                if (!art.Aktiv)
                {
                    throw new MyException("Artikulli me kod:" + art.KodArtikulli + " nuk eshte aktiv!");
                }
                if (veprimi == "shitje" && !art.IShitshem)
                    throw new MyException("Artikulli me kod:" + art.KodArtikulli + " nuk eshte i shitshem!");
                if (this.idKodi != art.IdArtikulli)
                    throw new MyException("Artikulli me kod: " + art.KodArtikulli + " eshte marre gabimisht!");
                if (art.LlojiArt && !gjenerodokumentmagazine)
                    throw new MyException("Ky dokument nuk gjeneron dokument magazine dhe nuk mund te perdoret per veprime me artikujt afatgjate!");
                if (art.LlojiArt && Sasia<0)
                    throw new MyException("Nuk mund te kryeni kthim blerje/shitje me artikuj afatgjate!");
                
                if (gjenerodokumentmagazine)
                {
                    colArtikujt artPerberes = new colArtikujt();
                    artPerberes.merrArtikujtPerberesSipasIdArtKryesordheDate(art.IdArtikulli, dtDok);
                    for (int i = 0; i < artPerberes.Count(); i++)
                    {
                        if (!artPerberes[i].Aktiv)
                            throw new MyException($"Artikulli perberes me kod {artPerberes[i].KodArtikulli} eshte inaktiv!");
                    }
                }

                if (merrSipasGrupit)
                {
                    if ((kodgrupi == "Karta" || kodgrupi == "Loan") && art.KodKodifikimi2 != kodgrupi)
                    {
                        throw new MyException("Artikulli me kod:" + art.KodArtikulli + " nuk i perket ketij grupi dokumenti!");
                    }
                    else if ((kodgrupi == "Aparate" || kodgrupi == "Aparate ekspozitore") && (art.KodKodifikimi2 != "Aparate" && art.KodKodifikimi2 != "Loan"))
                    {
                        throw new MyException("Artikulli me kod:" + art.KodArtikulli + " nuk i perket ketij grupi dokumenti!");

                    }
                }
                if (merrDhurata)
                {
                    if (!art.Dhurate)
                    {
                        throw new MyException("Artikulli me kod:" + art.KodArtikulli + " nuk eshte artikull per vodafone one!");
                    }
                }
                this.idBarkodi = idBarkod;
            }
            else if (lloji == "Llogari")
            {
                llog = new clsLlogari(kodArt, idNdermarrje);
                if (llog.IdLlogari == 0)
                {
                    throw new MyException("Nje nga llogarite nuk ekziston!");
                }
                if (this.idKodi != llog.IdLlogari)
                    throw new MyException("Llogaria me kod: " + llog.IdLlogari + " eshte marre gabimisht!");

                if (this.dtFillimi > this.dtMbarimi)
                    throw new MyException("Data e fillimit duhet te jete me e vogel se data e mbarimit ");
            }
            //this.idShitjeTrupi = idtrupi;
            if (konvertim)
            {
                this.idTrupiKonvertimi = this.idShitjeTrupi;
                this.idTrupiKonvertimBlerje = 0;
            }
            else if (klonim)
            {
                this.idTrupiKonvertimi = 0;
                this.idTrupiKonvertimBlerje = 0;
            }
            else if (kthim)
            {
                this.idTrupiKonvertimi = 0;// this.idShitjeTrupi; ;///ruhet id qe te bejme lidhjen per rivleresimin
                this.idTrupiKonvertimBlerje = 0;
            }
            else if (konvertimblerje)
            {
                this.idTrupiKonvertimBlerje = this.idShitjeTrupi;
                this.idTrupiKonvertimi = 0;
            }
            if (kthimVod || blerjengadealer)
            {
                this.IdTrupiKthim = this.IdShitjeTrupi;
                this.idTrupiTransferimi = 0;
                this.idTrupiKonvertimi = 0;

            }

            this.kodi = kodArt;
            if (this.dtMbarimi < this.dtFillimi)
                throw new MyException("Dt e mbarimit duhet te jete me e madhe se data e fillimit!");

            this.pershkrimi = String.IsNullOrWhiteSpace(pershkr) ? "" : pershkr;
            this.pershkrim2 = String.IsNullOrWhiteSpace(art.PershkrimiAngArtikulli) ? "" : art.PershkrimiAngArtikulli;

            if (detajimi == string.Empty || detajimi == "*" || detajimi == "Pa detajime" || detajimi == "Pa detajim" || detajimi == null || detajimi == "null")
                this.idDetajimArt = -1;
            else
            {
                if (art.IdArtikulli > 0)
                {
                    clsDetajimArtikulli detArt = new clsDetajimArtikulli();
                    detArt.mbushDetajimArtikulli(detajimi, idNdermarrje);
                    if (detArt.IdDetajimArtikulli==0)
                        throw new MyException($"Detajimi i pare me kod {detajimi} nuk ekziston!");
                    if (art.IdKategoriDetajimi == 0)
                        throw new MyException("Artikulli nuk ka nje kategori te zgjedhur per detajimin e pare.");
                    if (art.IdKategoriDetajimi != detArt.KategoriDetajimi)
                        throw new MyException("Kategoria e detajimit nuk perkon me kategorine e zgjedhur ne kartelen e artikullit.");
                    this.IdDetajimArt = detArt.IdDetajimArtikulli;
                    this.KodDetajim1 = detajimi;
                    if (!isShitje || ownshop) //nqs eshte blerje do shohim nqs id = 0 ndersa kodArt != string.Empty, ne kete rast do krijohet detajimi me kete kod.
                    {
                        if (this.IdDetajimArt == 0 && detajimi != string.Empty)
                        {
                            int llojDetajim = 0;
                            if (art.IdKategoriDetajimi == 3)
                                llojDetajim = 3;
                            else if (art.IdKategoriDetajimi == 2)
                                llojDetajim = 2;
                            else if (art.IdKategoriDetajimi == 4)
                                llojDetajim = 1;

                            clsDetajimArtikulli detajimRi = new clsDetajimArtikulli(detajimi, llojDetajim, string.Empty, idPerdoruesi, art.IdKategoriDetajimi, idNdermarrje, string.Empty, 1, 0);
                            if (detajimRi.ruajShpejte(art, 1).Status)
                                this.IdDetajimArt = detajimRi.IdDetajimArtikulli;
                            else
                            {
                                throw new MyException("Gabim gjate krijimit te detajimit te pare!");
                            }
                        }
                    }
                    else
                        if (this.IdDetajimArt > 0) //nqs o.IdDetajimArt=-1 ath nuk do krijohet as nuk do lidhet ndonje detajim
                    {
                        if (!clsDetajimArtikulli.ekzistonDetajimLidhurMeArtikullin(this.KodDetajim1, idNdermarrje, art.KodArtikulli, 1) && !shitjevodafone)
                        {

                            if (!clsDetajimPerArt.ruajLidhje(art, this.IdDetajimArt, 1, idNdermarrje, idPerdoruesi).Status)
                                throw new MyException("Gabim gjate lidhjes se detajimit te pare me artikullin!");
                        }
                        clsDetajimArtikulli detajim = new clsDetajimArtikulli(idDetajimArt);
                        if (detajim.Loan == 1 && !meme)
                        {
                            throw new MyException("Ky serial artikulli mund te perdoret vetem per Loan!");
                        }
                    }
                }
                else
                    this.IdDetajimArt = -1;


            }

            if (detajimi2 == string.Empty || detajimi2 == "*" || detajimi2 == "Pa detajime" || detajimi2 == "Pa detajim" || detajimi2 == "null" || detajimi2 == null)
                this.idDetajimArt2 = -1;
            else
            {
                if (art.IdArtikulli > 0)
                {
                    clsDetajimArtikulli detArt = new clsDetajimArtikulli();
                    detArt.mbushDetajimArtikulli(detajimi2, idNdermarrje);
                    if (detArt.IdDetajimArtikulli == 0)
                        throw new MyException($"Detajimi i dyte me kod {detajimi2} nuk ekziston!");
                    if (art.IdKategoriDetajimi2 == 0)
                        throw new MyException("Artikulli nuk ka nje kategori te zgjedhur per detajimin e dyte.");
                    if (art.IdKategoriDetajimi2 != detArt.KategoriDetajimi)
                        throw new MyException("Kategoria e detajimit nuk perkon me kategorine e zgjedhur ne kartelen e artikullit.");
                    this.IdDetajimArt2 = detArt.IdDetajimArtikulli;
                    this.KodDetajim2 = detajimi2;
                    if (!isShitje)//nqs eshte blerje do shohim nqs id = 0 ndersa kodi != string.Empty, ne kete rast do krijohet detajimi me kete kod.
                    {
                        if (this.IdDetajimArt2 == 0 && this.KodDetajim2 != string.Empty)
                        {
                            int llojDetajim = 0;
                            if (art.IdKategoriDetajimi2 == 3)
                                llojDetajim = 3;
                            else if (art.IdKategoriDetajimi2 == 4)
                                llojDetajim = 1;

                            clsDetajimArtikulli detajimRi = new clsDetajimArtikulli(this.KodDetajim2, llojDetajim, string.Empty, idPerdoruesi, art.IdKategoriDetajimi2, idNdermarrje, string.Empty, 1, 0);
                            clsMesazh mesazh = detajimRi.ruajShpejte(art, 2);
                            if (mesazh.Status)
                                this.IdDetajimArt2 = detajimRi.IdDetajimArtikulli;
                            else
                            {
                                throw new MyException("Gabim gjate krijimit te detajimit te dyte!");
                            }
                        }
                    }
                    else
                        if (this.IdDetajimArt2 > 0) //nqs o.IdDetajimArt=-1 ath nuk do krijohet as nuk do lidhet ndonje detajim
                    {
                        bool lidhurMeArt = clsDetajimArtikulli.ekzistonDetajimLidhurMeArtikullin(this.KodDetajim2, idNdermarrje, art.KodArtikulli, 2);
                        if (!lidhurMeArt && !shitjevodafone)
                        {
                            clsMesazh mesazh = clsDetajimPerArt.ruajLidhje(art, this.IdDetajimArt2, 2, idNdermarrje, idPerdoruesi);
                            if (!mesazh.Status)
                                throw new MyException("Gabim gjate lidhjes se detajimit te dyte me artikullin!");
                        }
                    }
                }
                else
                    this.IdDetajimArt2 = -1;

            }

            this.idNjesia = idNjesi;

           
            //KontrolloZbritjeAnalitike(lejoZbritjeNegative);

            this.tvsh = idTvsh;
            if (this.VleftaMeTvsh > 0 && kthim && art.Klasa != 3 && !lejoSasiPozitiveKthim)
                throw new MyException($"Vlera e rreshtit {nrRreshtit} nuk mund te jete pozitive!");
            this.idLlojVeprimi = DbShare.clsKonfLlojRreshtiVlere.ktheIdLlojRreshtiVlere(lloji, "Shitje");
            
            if (magazina == string.Empty || magazina == null || magazina == "null")
            {
                this.IdMagazina = 0;
            }
            else
            {
                if (!clsNjesiAdministrative.ekziston(magazina, idNdermarrje))
                    throw new MyException($"Magazina {magazina} nuk ekziston!");
                clsNjesiAdministrative njesiadm = new clsNjesiAdministrative(magazina, idNdermarrje, idPerdoruesi);
                if (njesiadm.IdNjesiAdministrative < 1)
                    throw new MyException($"Nuk keni autorizime ne magazinen {magazina}!");
                if (magazina != string.Empty && !njesiadm.Aktiv)
                    throw new MyException(String.Format("Magazina {0} nuk eshte aktive!", magazina));
                if (idLlojVeprimi == 1 && art.LlojiArt && njesiadm.IdLlojMagazine == 1)
                    throw new MyException($"Magazina {njesiadm.Kodi} nuk i perket llojit per artikullin {art.KodArtikulli}!");
                this.IdMagazina = njesiadm.IdNjesiAdministrative;
                if(!(isShitje || lejoMagNdryshme))//duhet blerje dhe lejomagtendryshme false
                {
                    if (magazina.Length>3 && magazina.Substring(magazina.Length-3).ToUpper() == "EXP") {
                        if (kodgrupi.EqualsIgnoreCase("Aparate") ) //mag exp me kod grupi aparate
                            throw new MyException($"Kujdes! Magazina {njesiadm.Kodi} nuk i perket ketij grupimi blerje");
                    }
                    else
                    {
                        if (kodgrupi.EqualsIgnoreCase("Aparate ekspozitore"))
                            throw new MyException($"Kujdes! Magazina {njesiadm.Kodi} nuk i perket ketij grupimi blerje");
                    }
                }

            }

            if (nrLlogShpenz == string.Empty || nrLlogShpenz == null || nrLlogShpenz == "null")
            {
                this.IdLlogShpenzimi = 0;
                this.nrLlogShpenzimi = string.Empty;
            }
            else
            {
                if (!clsLlogari.ekzistonLlogari(nrLlogShpenz, idNdermarrje))
                    throw new MyException("Llogaria e shpenzimit nuk ekziston!");
                clsLlogari llogari = new clsLlogari(nrLlogShpenz, idNdermarrje);
                if (!llogari.Aktiv)
                    throw new MyException("Llogaria e shpenzimit nuk eshte aktive!");
                this.nrLlogShpenzimi = llogari.NrLlogari;
                this.IdLlogShpenzimi = llogari.IdLlogari;
            }

            if (shenim != string.Empty && shenim != null && shenim != "null")
                this.shenime = shenim;
            else
                this.shenime = "";

            if (shenim2 != string.Empty && shenim2 != null && shenim2 != "null")
                this.shenime2 = shenim2;
            else
                this.shenime2 = "";

            if ((tollon || tollonkastrati) && idLlojVeprimi == 1)
            {
                if (shenime == string.Empty)
                    throw new MyException("Duhet te shenoni serialin fillestar te tollonave!");
                if (zevendesimtollonakastrati)
                {
                    if (shenime != "0" && tollon)
                    {
                        long serialitollon = 0;
                        long.TryParse(shenime, out serialitollon);
                        if (serialitollon == 0)
                            throw new MyException("Seriali fillestar i tollonave duhet te jete numer!");


                    }
                    int length = shenime.Length;
                    if (tollonkastrati && length != 19)
                        throw new MyException("Gjatesia e tollonit nuk eshte e sakte!");
                    if (tollonkastrati)
                    {
                        long serialitollon1 = 0;
                        long.TryParse(shenime.Substring(10, 9), out serialitollon1);
                        if (serialitollon1 == 0)
                            throw new MyException("Seriali fillestar i tollonave nuk eshte ne formatin e duhur!");
                    }
                }
            }
        }

        //private void KontrolloZbritjeAnalitike(bool lejoZbritjeNegative)
        //{
        //    if (!lejoZbritjeNegative) //"Jo".Equals(lejoZbritjeNegative, StringComparison.OrdinalIgnoreCase))
        //    {
        //        if (!(Zbritje >= 0 && Zbritje <= 100))
        //            throw new MyException("Zbritja duhet te jete numer >= 0 dhe <= 100 !");
        //    }
        //    if (!(Zbritje >= -100 && Zbritje <= 100))
        //        throw new MyException("Zbritja duhet te jete numer >= -100 dhe <= 100 !");
        //}

        public clsTrupiShitje(DataRow rreshti)
        {
            mbushTrupShitje(rreshti);
        }
        public clsTrupiShitje(DataRow rreshti, bool webhook)
        {
            mbushTrupShitjeWebhook(rreshti);
        }
        public clsTrupiShitje(DataRow rreshti,int idKokaShitje)
        {
            mbushTrupShitjePerWebhook(rreshti, idKokaShitje);
        }
       

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdShitjeTrupi
        {
            get { return idShitjeTrupi; }
            set { idShitjeTrupi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e kokes se dokumentit te shitjes.
        /// </summary>
        public int IdShitjeKoka
        {
            get { return idShitjeKoka; }
            set { idShitjeKoka = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne  e llojit te veprimit.
        /// <example> artikull, makro, llogari, tekst, credit note, nentotali</example>
        /// </summary>
        public int IdLlojVeprimi
        {
            get { return idLlojVeprimi; }
            set { idLlojVeprimi = value; }
        }
        /// <summary>
        /// id e trupit te konvertuar blerje
        /// </summary>
        public int IdTrupiKonvertimBlerje
        {
            get
            {
                return idTrupiKonvertimBlerje;
            }
            set
            {
                idTrupiKonvertimBlerje = value;
            }
        }
        /// <summary>
        /// id e trupit te kthimit
        /// </summary>
        public int IdTrupiKthim
        {
            get
            {
                return idTrupiKthim;
            }
            set
            {
                idTrupiKthim = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos kodi i artikullit, llogarise etj.
        /// </summary>
        public String Kodi
        {
            get { return kodi; }
            set { kodi = value; }
        }
        /// <summary>
        /// 1-perqindje ,2- vlere
        /// </summary>
        public int LlojZbritje
        {
            get
            {
                return zbritjeNeVlere;
            }
            set
            {
                zbritjeNeVlere = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos pershkrimi i artikullit, llogarise etj.
        /// </summary>
        public String Pershkrimi
        {
            get { return pershkrimi; }
            set { pershkrimi = value; }
        }

        public String Pershkrim2
        {
            get { return pershkrim2; }
            set { pershkrim2 = value; }
        }

        public int IdBarkodi
        {
            get { return idBarkodi; }
            set { idBarkodi = value; }
        }
        /// <summary>
        /// mban elementin e artikullit ose llogarise per te mos e marre 700 here nga db
        /// </summary>
        public object Element
        {
            get
            {
                return element;
            }
            set
            {
                element = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e detajimit te artikullit.
        /// </summary>
        public int IdDetajimArt
        {
            get { return idDetajimArt; }
            set { idDetajimArt = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e njesise se artikullit.
        /// </summary>
        public int IdNjesia
        {
            get { return idNjesia; }
            set { idNjesia = value; }
        }

        /// <summary>
        /// gjeresia e artikullit per te dhene sasine
        /// </summary>
        public double Gjeresi
        {
            get
            {
                return gjeresi;
            }
            set
            {
                gjeresi = value;
            }
        }

        /// <summary>
        /// gjatesia e artikullit per te dhene sasine
        /// </summary>
        public double Gjatesi
        {
            get
            {
                return gjatesi;
            }
            set
            {
                gjatesi = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos sasia.
        /// </summary>
        public double Sasia
        {
            get { return sasia; }
            set { sasia = value; }
        }

        /// <summary>
        /// Kthen/Vendos cmimi.
        /// </summary>
        public double Cmimi
        {
            get { return cmimi; }
            set { cmimi = value; }
        }

        /// <summary>
        /// sasia e artikullit per kete gjatesi dhe kete gjeresi
        /// </summary>
        public double SasiPermasa
        {
            get
            {
                return sasiPermasa;
            }
            set
            {
                sasiPermasa = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos zbritja analitike.
        /// </summary>
        public double Zbritje
        {
            get { return zbritje; }
            set { zbritje = value; }
        }

        /// <summary>
        /// Kthen/Vendos vlefta pa tvsh.
        /// </summary>
        public double VleftaPaTvsh
        {
            get { return vleftaPaTvsh; }
            set { vleftaPaTvsh = value; }
        }

        /// <summary>
        /// Kthen/Vendos tvsh.
        /// </summary>
        public int Tvsh
        {
            get { return tvsh; }
            set { tvsh = value; }
        }

        /// <summary>
        /// Kthen/Vendos vlefta me tvsh.
        /// </summary>
        public double VleftaMeTvsh
        {
            get { return vleftaMeTvsh; }
            set { vleftaMeTvsh = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne  magazina.
        /// </summary>
        public int IdMagazina
        {
            get { return idMagazina; }
            set { idMagazina = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e detajimit E DYTE te artikullit.
        /// </summary>
        public int IdDetajimArt2
        {
            get
            {
                return idDetajimArt2;
            }
            set
            {
                idDetajimArt2 = value;
            }
        }

        /// <summary>
        /// id e kodit te artikullit/llogarise/macros ect
        /// </summary>
        public int IdKodi
        {
            get
            {
                return idKodi;
            }
            set
            {
                idKodi = value;
            }
        }

        /// <summary>
        /// sasia e mbetur pas ekzekutimeve
        /// </summary>
        public double Sasimbetur
        {
            get
            {
                return sasimbetur;
            }
            set
            {
                sasimbetur = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos kodi i artikullit, llogarise etj.
        /// </summary>
        public String KodDetajim1
        {
            get { return kodDetajim1; }
            set { kodDetajim1 = value; }
        }

        /// <summary>
        /// Kthen/Vendos pershkrimi i artikullit, llogarise etj.
        /// </summary>
        public String KodDetajim2
        {
            get { return kodDetajim2; }
            set { kodDetajim2 = value; }
        }

        /// <summary>
        /// Kthen/Vendos pershkrimi i artikullit, llogarise etj.
        /// </summary>
        public String Shenime
        {
            get { return shenime; }
            set { shenime = value; }
        }

        /// <summary>
        /// Kthen/Vendos  dt e fillimit.
        /// </summary>
        public DateTime DtFillimi
        {
            get { return dtFillimi; }
            set { dtFillimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos  dt e mbarimit.
        /// </summary>
        public DateTime DtMbarimi
        {
            get { return dtMbarimi; }
            set { dtMbarimi = value; }
        }

        /// <summary>
        /// id e trupit te rezervimit kur behet konvertim nga rezervimi
        /// </summary>
        public int IdTrupiRezervimi
        {
            get
            {
                return idTrupiRezervimi;
            }
            set
            {
                idTrupiRezervimi = value;
            }
        }

        /// <summary>
        /// id e trupit nga eshte transferuar nga bija tek mema
        /// </summary>
        public int IdTrupiTransferimi
        {
            get
            {
                return idTrupiTransferimi;
            }
            set
            {
                idTrupiTransferimi = value;
            }
        }

        /// <summary>
        /// sasia qe shkon per rezervim
        /// </summary>
        public double SasiRez
        {
            get
            {
                return sasiRez;
            }
            set
            {
                sasiRez = value;
            }
        }

        /// <summary>
        /// id e trupit te dokumentit nga eshte konvertuar
        /// </summary>
        public int IdTrupiKonvertimi
        {
            get
            {
                return idTrupiKonvertimi;
            }
            set
            {
                idTrupiKonvertimi = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e llogarise se shpenzimit
        /// </summary>
        public int IdLlogShpenzimi
        {
            get { return idLlogShpenzimi; }
            set { idLlogShpenzimi = value; }
        }

        public double ZbritjeVlere
        {
            get
            {
                return zbritjeVlere;
            }
            set
            {
                zbritjeVlere = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos nr e llogarise se shpenzimit
        /// </summary>
        public String NrLlogShpenzimi
        {
            get { return nrLlogShpenzimi; }
            set { nrLlogShpenzimi = value; }
        }

        public string TvshKod { get; set; }

        /// <summary>
        /// Kthen/Vendos shenime
        /// </summary>
        public String Shenime2
        {
            get { return shenime2; }
            set { shenime2 = value; }
        }

        /// <summary>
        /// kthen/vendos kategorine e shpenzimit
        /// </summary>
        /// <param name="idTrupi"></param>
        /// <returns></returns>
        public int IdKategoriShpenzimi
        {
            get
            {
                return idKategoriShpenzimi;
            }
            set
            {
                idKategoriShpenzimi = value;
            }

        }

        public int IdTrupiTransferimNga 
        {
            get { return idTrupiTransferimNga; }
            set { idTrupiTransferimNga = value; }
        }

        public double VleraKomisionit
        {
            get
            {
                return vleraKomisionit;
            }

            set
            {
                vleraKomisionit = value;
            }
        }

        public bool MeKomision
        {
            get
            {
                return meKomision;
            }

            set
            {
                meKomision = value;
            }
        }

        #endregion

        #region Metoda publike

        public static int ktheIdKoka(int idTrupi)
        {
            using (clsDatabaseRegjistrim dbTrupShitje = new clsDatabaseRegjistrim())
            {
                return dbTrupShitje.ktheIdKokaShitjeSipasIdTrupi(idTrupi);
            }
        }
        public static int ktheIdKoka(int idTrupi, clsDatabaseRegjistrim dbTrupShitje)
        {
                return dbTrupShitje.ktheIdKokaShitjeSipasIdTrupi(idTrupi);

        }
        public clsMesazh krijoTrupShitje(int idShiTrup, int idShiKoka, int idLlojVep, string kod, string pershkr, int IdDetaj, int idNjes, double sas, double cm,
            double zbr, double vlMeTv, int tv, double vlPaTv, int idkodi, int idMagazina, double gjeresi, double gjatesi, double sasipermasa, string shenime, DateTime dtfill, DateTime dtmb, int idtrupikonvertimi, double sasirez, int idtrupirez, int idtrupitransferim, int idtrupikthim, int idndermarje, int idLlogShpenzimi, int idDetajim2, object elem, bool tollona, bool tollonkastrati, int idtrupikonvertimblerje, string shenime2, int llojzbritje, double zbritjavlere, int idKategoriShpenzimi, int idBarkodi, int idTrupiTransferimNga, int sasiambetur)
        {
            using (clsDatabaseKontabilitet dbk = new clsDatabaseKontabilitet())
            {
                return krijoTrupShitje(idShiTrup, idShiKoka, idLlojVep, kod, pershkr, IdDetaj, idNjes, sas, cm,
                 zbr, vlMeTv, tv, vlPaTv, idkodi, idMagazina, gjeresi, gjatesi, sasipermasa, shenime, dtfill, dtmb, idtrupikonvertimi, sasirez, idtrupirez, idtrupitransferim, idtrupikthim, idndermarje, idLlogShpenzimi, idDetajim2, elem, tollona, tollonkastrati, idtrupikonvertimblerje, shenime2, llojzbritje, zbritjavlere, idKategoriShpenzimi, idBarkodi, dbk, idTrupiTransferimNga, 0, false, sasiambetur);
            }
        }
        public clsMesazh krijoTrupShitje(int idShiTrup, int idShiKoka, int idLlojVep, string kod, string pershkr, int IdDetaj, int idNjes, double sas, double cm,
            double zbr, double vlMeTv, int tv, double vlPaTv, int idkodi, int idMagazina, double gjeresi, double gjatesi, double sasipermasa, string shenime, DateTime dtfill, DateTime dtmb, int idtrupikonvertimi, double sasirez, int idtrupirez, int idtrupitransferim, int idtrupikthim, int idndermarje, int idLlogShpenzimi, int idDetajim2, object elem, bool tollona, bool tollonkastrati, int idtrupikonvertimblerje, string shenime2, int llojzbritje,
            double zbritjavlere, int idKategoriShpenzimi, int idBarkodi, clsDatabaseKontabilitet dbK, int idTrupiTransferimNga, double vleraKomisionit, bool MeKomision, int sasiambetur)
        {
            idShitjeTrupi = idShiTrup;
            idShitjeKoka = idShiKoka;
            idLlojVeprimi = idLlojVep;
            kodi = kod;
            pershkrimi = pershkr;
            idDetajimArt = IdDetaj;
            idDetajimArt2 = idDetajim2;
            if (IdDetaj == 0)
                kodDetajim1 = "";
            if (idDetajim2 == 0)
                kodDetajim2 = "";
            idNjesia = idNjes;
            sasia = sas;
            cmimi = cm;
            zbritje = zbr;
            vleftaPaTvsh = vlPaTv;
            tvsh = tv;
            vleftaMeTvsh = vlMeTv;
            idKodi = idkodi;
            this.idTrupiKonvertimBlerje = idtrupikonvertimblerje;
            this.gjatesi = gjatesi;
            this.gjeresi = gjeresi;
            this.sasiPermasa = sasipermasa;
            this.idMagazina = idMagazina;
            this.shenime = shenime;
            dtFillimi = dtfill;
            dtMbarimi = dtmb;
            this.sasiRez = sasirez;
            idTrupiKonvertimi = idtrupikonvertimi;
            this.idTrupiRezervimi = idtrupirez;
            this.idTrupiTransferimi = idtrupitransferim;
            this.idLlogShpenzimi = idLlogShpenzimi;
            this.idTrupiKthim = idtrupikthim;
            this.sasimbetur = sasiambetur;
            this.element = elem;
            this.shenime2 = shenime2;
            this.zbritjeNeVlere = llojzbritje;
            this.zbritjeVlere = zbritjavlere;
            this.idKategoriShpenzimi = idKategoriShpenzimi;
            this.idBarkodi = idBarkodi;
            this.idTrupiTransferimNga = idTrupiTransferimNga;
            this.vleraKomisionit = vleraKomisionit;
            this.meKomision = MeKomision;//shtuar dy fushat e reja 
            OColDetajimet = new colDetajimeArtikulliRegjistrim();
            clsMesazh mesazh = kontrollo(idndermarje, tollona, tollonkastrati, dbK);
            if (!mesazh.Status)
                return mesazh;
            return new clsMesazh(true, "Dokumenti i trupit shitje/blerjes u krijua me sukses!");
        }

        public clsMesazh krijoTrupShitjeImportWK(int idShiTrup, int idShiKoka, int idLlojVep, string kod, string pershkr, string Detaj, int idNjes, double sas, double cm, double zbr, double vlMeTv, int tv, double vlPaTv, int idMagazina, double gjeresi, double gjatesi, double sasipermasa, string shenime, DateTime dtfill, DateTime dtmb, int idtrupikonvertimi, double sasirez, int idtrupirez, int idtrupitransferim, int idtrupikthim, int idndermarje, int idperdoruesi, int loan, int idLlogShpenz, int idtrupikonvertimblerje, string shenime2, int llojzbritje, double zbritjevlere, int idkategoriShpenzimi, int idBarkodi)
        {
            int idkodi = 0;
            int idDetaj = 0;
            object elem = new object();
            if (idLlojVep == 1)
            {
                clsArtikulli art = new clsArtikulli(kod, idndermarje);
                idkodi = art.IdArtikulli;
                if (Detaj != string.Empty)
                    if (!clsDetajimArtikulli.ekziston(Detaj, idndermarje))
                    {
                        clsDetajimArtikulli detajim = new clsDetajimArtikulli(Detaj, 1, Detaj, idperdoruesi, 2, idndermarje, string.Empty, 1, loan); //TOCHECK EGI
                        clsMesazh mesazh = detajim.ruaj();
                        clsDetajimPerArt.ruajLidhje(art, detajim.IdDetajimArtikulli, 1, idndermarje, idperdoruesi);
                        idDetaj = detajim.IdDetajimArtikulli;
                    }
                    else
                    {
                        clsDetajimArtikulli detajim = new clsDetajimArtikulli();
                        detajim.mbushDetajimArtikulli(Detaj, idndermarje);
                        detajim.Loan = loan;
                        detajim.modifikoLoan(detajim.IdDetajimArtikulli, idperdoruesi, loan);
                        idDetaj = detajim.IdDetajimArtikulli;
                    }
                elem = art;
            }
            else if (idLlojVep == 3)
            {
                clsLlogari llog = new clsLlogari(kod, idndermarje);
                idkodi = llog.IdLlogari;
                elem = llog;
            }
            return krijoTrupShitje(idShiTrup, idShiKoka, idLlojVep, kod, pershkr, idDetaj, idNjes, sas, cm, zbr, vlMeTv, tv, vlPaTv, idkodi, idMagazina, gjeresi, gjatesi, sasipermasa, shenime, dtfill, dtmb, idtrupikonvertimi, sasirez, idtrupirez, idtrupitransferim, idtrupikthim, idndermarje, idLlogShpenz, 0, elem, false, false, idtrupikonvertimblerje, shenime2, llojzbritje, zbritjevlere, idkategoriShpenzimi, idBarkodi, idTrupiTransferimNga,0);
           
        }

        public clsMesazh krijoTrupShitjeImport(int idShiTrup, int idShiKoka, string LlojVep, string kod, string pershkr, string detajim1, string detajim2, string njesia, double sas, double cm, double zbr, double vlMeTvsh, string tvsh, double vlPaTvsh, string magazina, double gjeresi, double gjatesi, double sasipermasa, string shenime, DateTime dtfill, DateTime dtmb, double sasirez, int idtrupirez, int idtrupitransferim, int idndermarje, int idperdoruesi, string llogShpenz, bool cmimZero, bool faturePermb, string seriali, int indeksiTrupit, DbShare.clsKonfigurimAmbjenti konfigMagazina, double kursi, int idStatusDok, ref DbCore.DbAsete.colSerialetMagazine colSerialet, bool shitje_blerje, string kodbari, int llojzbritje, double zbritjevlere, string kategoriShpenzimi, DbData dbData, double cmMeTvsh, string serialiUnikKryesor, int loan, DateTime dtDok, string shenime2trupi, double VleraKomisionit, bool klientKomision, int idTrupiKthimi, ref colTrupiShitje colTrupiKomision, clsTrupiShitje trupiurdher, bool klientKomisionPerColTrupiKomision, bool shtoRreshtKomisioni, int id, int idTrupiKonvertim, int sasiambetur)
        {
            int idkodi = 0;
            int idDetaj1 = 0;
            int idDetaj2 = 0;
            int idNjesia = 0;
            int idLlogshpenz = 0;
            int idKategoriShpenzimi = 0;
            int idLlojVep = 0;
            int idBarkodi = 0;
            int idSerialiUnikKryesor = 0;
            bool ruajKomision = false;
            switch (LlojVep)
            {
                case "Artikull":
                    idLlojVep = 1;
                    break;
                case "Makro":
                    idLlojVep = 2;
                    break;
                case "Llogari":
                    idLlojVep = 3;
                    break;
                default:
                    return new clsMesazh(false, $"Lloji i veprimit: {LlojVep} nuk eshte i sakte!");
            }
            if (llojzbritje == 1 && zbritjevlere == 0 && zbr != 0)
                zbritjevlere = sas * cm * zbr / 100;
            else if (llojzbritje == 2 && zbr == 0 && zbritjevlere != 0)
                zbr = Math.Round(zbritjevlere * 100 / (sas * cm), 10);
            clsArtikulli art = new clsArtikulli();
            clsLlogari llog = new clsLlogari();
            object elem = new object();
            clsDatabaseRegjistrim dbR = new clsDatabaseRegjistrim(dbData);
            clsDatabaseInventari dbI = new clsDatabaseInventari(dbData);
            clsDatabaseKontabilitet dbK = new clsDatabaseKontabilitet(dbData);
            clsNjesiAdministrative mag = new clsNjesiAdministrative();



            if (!String.IsNullOrEmpty(magazina))
            {
                if (magazina != Constants.MAGAZINE_DEFAULT_IMPORTI)
                {
                    mag = new clsNjesiAdministrative(magazina, idndermarje, dbR);
                    if (mag.IdNjesiAdministrative < 1)
                        return new clsMesazh(false, $"Magazina: {magazina} nuk ekziston!");
                }
                else
                {
                    clsArtikulli artikulli = new clsArtikulli(kod, idndermarje, dbI);
                    if (artikulli.Magazina != "")
                        mag = new clsNjesiAdministrative(artikulli.Magazina, idndermarje, dbR);
                    else
                    {
                        colNjesiAdministrative col = new colNjesiAdministrative();
                        col.mbushGjitheNjesiAdministrative(idndermarje, idperdoruesi, 1);
                        if (col.Count == 0)
                            return new clsMesazh(false, "Nuk u gjet asnje magazine!");
                        mag = col[0];
                    }
                }
            }
            clsTaksa taksa = new clsTaksa();
            if (String.IsNullOrEmpty(tvsh))
                return new clsMesazh(false, "Plotesoni tvsh!");
            switch (idLlojVep)
            {
                case 1:
                    {
                        //Ne import ka prioritet kodi i vendosur. Nqs nuk eshte vendosur kodi, atehere do merret kodbari. Nqs te dyja jane bosh duhet te dale mesazh gabimi.
                        if (String.IsNullOrEmpty(kod) && String.IsNullOrEmpty(kodbari))
                            return new clsMesazh(false, "Vendosni kodin ose kodbarin e artikullit!");
                        if (String.IsNullOrEmpty(njesia))
                            return new clsMesazh(false, "Vendosni njesine!");
                        if (!String.IsNullOrEmpty(kod))
                        {
                            art = new clsArtikulli(kod, idndermarje, dbI);
                            idkodi = art.IdArtikulli;
                            pershkr = art.PershkrimArtikulli;
                            if (!String.IsNullOrEmpty(kodbari))
                            {
                                idBarkodi = clsKodbari.MerrIdBarkodiSipasPershkrimDheArtikulli(kodbari, art.IdArtikulli);
                                if (idBarkodi <= 0)
                                    return new clsMesazh(false, String.Format("Barkodi {0} nuk ekziston!", kodbari));
                            }
                        }
                        else
                        {
                            //merr kodin e artikullit sipas barkodit.
                            idBarkodi = clsKodbari.MerrIdBarkodiSipasPershkrimit(kodbari);
                            if (idBarkodi <= 0)
                                return new clsMesazh(false, String.Format("Barkodi {0} nuk ekziston!", kodbari));
                            art.merrSipasKodbarit(kodbari, idndermarje, dbI); //no cache //todo cache
                            if (art.IdArtikulli < 1)
                                return new clsMesazh(false, $"Artikulli {kod} nuk ekziston!");
                            idkodi = art.IdArtikulli;
                            pershkr = art.PershkrimArtikulli;
                            kod = art.KodArtikulli;
                        }
                        clsNjesiArtikulli nj = new clsNjesiArtikulli(njesia, idndermarje, dbI);

                        if (art.Njesi1Artikulli != nj.IdNjesia && art.Njesi2Artikulli != nj.IdNjesia)
                            return new clsMesazh(false, "Artikulli " + art.KodArtikulli + " nuk eshte i lidhur me njesine " + nj.KodNjesia + "!");
                        if (art.LlojiArt && mag.IdLlojMagazine == 1)
                            return new clsMesazh(false, "Nuk lejohet te behet hyrje/dalje per artikuj afatgjate ne magazine per artikuj qarkullues!");
                        if (String.IsNullOrEmpty(kod) && !String.IsNullOrEmpty(kodbari))
                        {
                            // Nqs ka vendosur barkod dhe jo kodin e artikullit, kontrollo nese njesia e vendosur perket me njesine e barkodit.
                            int njesiKobari = clsKodbari.MerrNjesiKodbari(art.IdArtikulli, kodbari); //todo cache
                            if (art.Njesi1Artikulli != art.Njesi2Artikulli && ((art.Njesi1Artikulli == nj.IdNjesia && njesiKobari != 1) || (art.Njesi2Artikulli == nj.IdNjesia && njesiKobari != 2)))
                                return new clsMesazh(false, "Barkodi " + kodbari + " nuk perket me njesine matese te zgjedhur " + nj.KodNjesia + "!");
                        }
                        idNjesia = nj.IdNjesia;
                        if (!String.IsNullOrEmpty(detajim1))
                        {
                            if(art.IdKategoriDetajimi== 0)
                            throw new MyException($"Artikulli me kod {kod} nuk ka detajim te lidhur ne kartele!");

                            clsDetajimArtikulli detajim = clsDetajimArtikulli.MerrDetajimOseCelDheLidhNeseNukEkziston(detajim1, idndermarje, idperdoruesi, art, 1);
                            idDetaj1 = detajim.IdDetajimArtikulli;
                            kodDetajim1 = detajim.KodDetajimArtikulli;
                        }
                        if (!String.IsNullOrEmpty(detajim2))
                        {
                           
                            if (art.IdKategoriDetajimi2 == 0)
                                throw new MyException($"Artikulli me kod {kod} nuk ka detajim te lidhur ne kartele!");

                            clsDetajimArtikulli detajim = clsDetajimArtikulli.MerrDetajimOseCelDheLidhNeseNukEkziston(detajim2, idndermarje, idperdoruesi, art, 2);
                            idDetaj2 = detajim.IdDetajimArtikulli;
                            kodDetajim2 = detajim.KodDetajimArtikulli;
                        }

                        if (!String.IsNullOrEmpty(serialiUnikKryesor))
                        {
                            if (!clsDetajimArtikulli.ekziston(serialiUnikKryesor, idndermarje))
                            {
                                clsDetajimArtikulli serialiUnik = new clsDetajimArtikulli(serialiUnikKryesor, 1, serialiUnikKryesor, idperdoruesi, art.IdKategoriDetajimi, idndermarje, string.Empty, 1, loan); //TOCHECK EGI
                                clsMesazh mesazh = serialiUnik.ruaj();
                                clsDetajimPerArt.ruajLidhje(art, serialiUnik.IdDetajimArtikulli, 1, idndermarje, idperdoruesi);
                                idSerialiUnikKryesor = serialiUnik.IdDetajimArtikulli;
                            }
                            else
                            {
                                clsDetajimArtikulli serialiUnik = new clsDetajimArtikulli();
                                serialiUnik.mbushDetajimArtikulli(serialiUnikKryesor, idndermarje);
                                serialiUnik.Loan = loan;
                                serialiUnik.modifikoLoan(serialiUnik.IdDetajimArtikulli, idperdoruesi, loan);
                                idSerialiUnikKryesor = serialiUnik.IdDetajimArtikulli;
                            }
                        }

                        if (tvsh.ToLower() != clsTaksa.kodTaksaPaTVSH.ToLower())
                        {
                            taksa = new clsTaksa(tvsh, idndermarje, dbR);
                        }
                        if (!faturePermb && !cmimZero && (cm == 0 && cmMeTvsh == 0))
                            return new clsMesazh(false, "Nuk lejohet cmim zero per kete lloj dokumenti!");

                        double norma = (double)taksa.NormaPerqindje;
                        if (vlMeTvsh == 0 && vlPaTvsh == 0 && (cm != 0 || cmMeTvsh != 0) && zbr != 100)
                            return new clsMesazh(false, "Plotesoni vleren/vleren me tvsh!");

                        if (cm != 0 && cmMeTvsh != 0 && Math.Abs(cmMeTvsh - cm * (1 + norma / 100)) > Math.Abs(0.005))
                            return new clsMesazh(false, "Cmimi me tvsh ose cmimi nuk eshte i sakte!");

                        if (cm != 0 && cmMeTvsh != 0 && norma == 0 && cm != cmMeTvsh)
                            return new clsMesazh(false, "Cmimi me tvsh ose cmimi nuk eshte i sakte!");

                        if (cm != 0 && cmMeTvsh == 0)
                            cmMeTvsh = cm * (1 + norma / 100);
                        else if (cm == 0 && cmMeTvsh != 0)
                            cm = cmMeTvsh / (1 + norma / 100);

                        if (vlMeTvsh != 0 && vlPaTvsh != 0)
                        {
                            if (Math.Abs(vlPaTvsh - (sas * cm * (1 - zbr / 100))) > Math.Abs(0.005))
                                return new clsMesazh(false, "Vlera pa tvsh nuk eshte e sakte!");

                            if (Math.Abs(vlMeTvsh - (sas * cm * (1 - zbr / 100) * (1 + norma / 100))) > Math.Abs(0.005))
                                return new clsMesazh(false, "Vlera me tvsh nuk eshte e sakte!");

                            if (norma != 0 && vlMeTvsh - (sas * cm * (1 - zbr / 100)) == 0)
                                return new clsMesazh(false, "Cmimi ka vleren e cmimit me tvsh!");
                        }

                        if (vlMeTvsh == 0 && vlPaTvsh != 0)
                            vlMeTvsh = sas * cm * (1 - zbr / 100) * (1 + norma / 100);

                        if (vlMeTvsh != 0 && vlPaTvsh == 0)
                            vlPaTvsh = sas * cm * (1 - zbr / 100);

                        if (!String.IsNullOrEmpty(seriali))
                        {
                            string[] serialet = seriali.Split(',');
                            foreach (string tmpSerial in serialet)
                            {
                                DbCore.DbAsete.clsAQTSeriale serialRi = new DbCore.DbAsete.clsAQTSeriale();
                                if (!DbAsete.clsAQTSeriale.kontrolloEkzistonAQTSerial(tmpSerial, idndermarje))
                                {
                                    if (shitje_blerje)//shitje
                                        return new clsMesazh(false, "Seriali " + tmpSerial + " nuk ekziston!");
                                    serialRi.AqtSerialKod = tmpSerial;
                                    serialRi.AqtSerialPershkrim = tmpSerial;
                                    serialRi.IdPerdoruesi = idperdoruesi;
                                    serialRi.IdNdermarrje = idndermarje;
                                    serialRi.IdStatusDokumenti = 1;
                                    serialRi.IdKrijuesi = idperdoruesi;
                                    serialRi.IdAQTArt = art.IdArtikulli;
                                    serialRi.MeSerialPerCope = !art.MeSerial;
                                    serialRi.IdNjesiAdministrativeAktuale = 0;
                                    serialRi.IdHistorikAktualPaSerial = 0;

                                    serialRi.HistorikSeriali = new DbCore.DbAsete.clsHistorikAQTSeriale();
                                    if (serialRi.MeSerialPerCope)
                                        serialRi.HistorikSeriali = new DbCore.DbAsete.clsHistorikAQTSeriale(serialRi.IdAQTSerial,String.Empty, 0, 0, 0, 1, 0, 0, 1, serialRi.IdNdermarrje, serialRi.IdPerdoruesi, serialRi.IdPerdoruesi, serialRi.DtKrijimi, serialRi.DtModifikimi);
                                    DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                                    mesazh = serialRi.ruaj();
                                    if (!mesazh.Status)
                                    {
                                        return new clsMesazh(false, "Ndodhi nje gabim gjate celjes se serialit " + tmpSerial + "!");
                                    }
                                }
                                else
                                {
                                    serialRi.merrAQTSerialSipasKodAQT(tmpSerial, idndermarje);
                                    if (!shitje_blerje)//blerje
                                    {
                                        if (DbCore.DbAsete.colSerialetMagazine.ekzistonBlerjePerKeteSerial(serialRi.IdAQTSerial, serialRi.IdNdermarrje))
                                            return new clsMesazh(false, "Ekziston nje blerje me kete serial " + serialRi.AqtSerialKod);
                                        if (colSerialet.Find(x => x.IdAQTSeriali == serialRi.IdAQTSerial) != null)
                                            return new clsMesazh(false, "Seriali " + serialRi.AqtSerialKod + " ekziston njehere ne dokument");
                                    }
                                }
                                DbCore.DbAsete.clsSerialetMagazine serialmag = new DbCore.DbAsete.clsSerialetMagazine(0, 0, indeksiTrupit, art.IdArtikulli, serialRi.IdAQTSerial, konfigMagazina.IdNivel, konfigMagazina.IdKonfigAmbjente, mag.IdNjesiAdministrative, art.MeSerial ? 1 : (float)(sas * (double)(idNjesia == art.Njesi1Artikulli ? 1 : art.KoeficientArtikulli)), float.Parse((vlPaTvsh * (1 - zbr) / (sas * (double)(idNjesia == art.Njesi1Artikulli ? 1 : art.KoeficientArtikulli))).ToString()) * kursi, float.Parse((vlPaTvsh * (1 - zbr) / (sas * (double)(idNjesia == art.Njesi1Artikulli ? 1 : art.KoeficientArtikulli))).ToString()) * (art.MeSerial ? 1 : (float)((sas * (double)(idNjesia == art.Njesi1Artikulli ? 1 : art.KoeficientArtikulli)))) * kursi, idStatusDok, idndermarje, idperdoruesi, idperdoruesi, 0);
                                colSerialet.Add(serialmag);
                            }
                        }
                        if (String.IsNullOrEmpty(magazina))
                            return new clsMesazh(false, "Magazina nuk duhet te jete bosh!");
                        if (shtoRreshtKomisioni)
                            KtheColKomision(ref colTrupiKomision, trupiurdher, idndermarje, klientKomisionPerColTrupiKomision, VleraKomisionit, kod, idperdoruesi, ref ruajKomision);
                        elem = art;
                        break;
                    }
                case 3:
                    {
                        llog = new clsLlogari(kod, idndermarje, dbK);
                        idkodi = llog.IdLlogari;
                        if (pershkr == string.Empty)
                            pershkr = llog.NrLlogari;
                        if (!shtoRreshtKomisioni)
                            pershkr = llog.EmerLlogari1;
                        if (tvsh != clsTaksa.kodTaksaPaTVSH)
                        {
                            taksa = new clsTaksa(tvsh, idndermarje, dbR);
                        }
                        sas = 1;
                        cm = vlPaTvsh;
                        idNjesia = 0;
                        elem = llog;
                    }
                    break;
                default: throw new MyException($"Lloji veprimit {idLlojVep} eshte i panjohur");
            }
            if (!string.IsNullOrEmpty(llogShpenz))
            {
                clsLlogari llogShpenzimi = new clsLlogari(llogShpenz, idndermarje, dbK);
                if (llogShpenzimi.IdLlogari < 1)
                    return new clsMesazh(false, "Llogaria e shpenzimit nuk ekziston!");
                idLlogshpenz = llogShpenzimi.IdLlogari;
                nrLlogShpenzimi = llogShpenzimi.NrLlogari;
            }
            if (!String.IsNullOrWhiteSpace(kategoriShpenzimi) && kategoriShpenzimi != String.Empty)
            {
                idKategoriShpenzimi = clsKategoriShpenzimi.KtheIdSipasKodit(kategoriShpenzimi, idndermarje); //todo cache
                if (idKategoriShpenzimi == 0)
                    return new clsMesazh(false, "Kategoria e shpenzimit nuk ekziston!");
                if (clsKategoriShpenzimi.KontrolloEshtePrindKategoriaShpenzimit(idKategoriShpenzimi))//todo cache
                    return new clsMesazh(false, "Kategoria e shpenzimit nuk duhet te jete prind!");
            }
            if (dtfill == DateTime.MinValue)
                dtfill = dtDok;
            if (dtmb == DateTime.MinValue)
                dtmb = dtDok;
            if (dtfill > dtmb)
                return new clsMesazh(false, "Data e fillimit duhet te jete me e vogel se data e mbarimit!");

            if (sas == 0)
                return new clsMesazh(false, "Plotesoni sasine!");

            if (!ruajKomision && !klientKomision)
                VleraKomisionit = 0;
            return krijoTrupShitje(idShiTrup, idShiKoka, idLlojVep, kod, pershkr, !String.IsNullOrEmpty(serialiUnikKryesor) ? idSerialiUnikKryesor : idDetaj1, idNjesia, sas, cm, zbr, vlMeTvsh, taksa.IdTaksa, vlPaTvsh, idkodi, mag.IdNjesiAdministrative, gjeresi, gjatesi, sasipermasa, shenime, dtfill, dtmb, idTrupiKonvertim, sasirez, 0, idtrupitransferim, idTrupiKthimi, idndermarje, idLlogshpenz, idDetaj2, elem, false, false, 0, shenime2trupi, llojzbritje, zbritjevlere, idKategoriShpenzimi, idBarkodi, dbK, idTrupiTransferimNga, VleraKomisionit, klientKomision, sasiambetur);
        }

        public static void KtheColKomision(ref colTrupiShitje colTrupiKomision, clsTrupiShitje trupiurdher, int idNdermarrje, bool klientKomision, double vleraKomisionit, string kodi, int idPerdorues, ref bool ruajKomision)
        {
            clsTrupiShitje clsKomision = new clsTrupiShitje();
            clsMesazh mesazh = new clsMesazh();
            colSerialetMagazine ser = new colSerialetMagazine();
            clsArtikulli artikulli = new clsArtikulli(trupiurdher != null ? trupiurdher.Kodi : kodi, idNdermarrje, new clsDatabaseInventari(new DbData()));
            if (klientKomision && artikulli.LlogaritKomision)
            {
                ruajKomision = true;
                var ekzistues = colTrupiKomision.FirstOrDefault(a => a.IdKodi == artikulli.IdLlogariKomisioni);
                if (ekzistues != null)
                {
                    ekzistues.VleraKomisionit = ekzistues.VleraKomisionit + vleraKomisionit;
                    ekzistues.cmimi = ekzistues.cmimi  -  vleraKomisionit;
                    ekzistues.vleftaMeTvsh = ekzistues.vleftaMeTvsh -  vleraKomisionit;
                    ekzistues.vleftaPaTvsh = ekzistues.vleftaPaTvsh   - vleraKomisionit;
                }
                else
                {
                    mesazh = clsKomision.krijoTrupShitjeImport(trupiurdher != null ? trupiurdher.IdShitjeTrupi : 0, trupiurdher != null ? trupiurdher.IdShitjeKoka : 0, "Llogari", artikulli.NrLlogKomision, artikulli.NrLlogKomision, "", "", "", 1, 0 - vleraKomisionit, 0, 0 - vleraKomisionit, "Pa TVSH", 0 - vleraKomisionit, "", 0, 0, 0, "", DateTime.Now, DateTime.Now, 0, 0, 0, idNdermarrje, idPerdorues, "", false, false, "", 0, null, 0, 1, ref ser, false, "", 1, 0, "", new DbData(), 0, "", 0, DateTime.Now, "", vleraKomisionit, true, 0, ref colTrupiKomision, trupiurdher, klientKomision, false, 0,0,0);
                    if (mesazh.Status)
                        colTrupiKomision.Add(clsKomision);
                }

            }
            else
                ruajKomision = false;
        }

        private clsMesazh kontrollo(int idndermarje, bool tollon, bool tollonkastrati, clsDatabaseKontabilitet dbK)
        {
            if (idLlojVeprimi == 1)
            {
                clsArtikulli art = (clsArtikulli)this.element;
                if (art.IdArtikulli < 1)
                    return new clsMesazh(false, "Artikulli nuk ekziston");
                if (art.LlojiArt && art.MeSerial && sasia != Math.Truncate(sasia))
                    return new clsMesazh(false, "Nuk lejohet sasi me presje dhjetore per artikullin " + Kodi + " sepse eshte me serial!");
            }
            else if (idLlojVeprimi == 3)
            {
                clsLlogari llog = (clsLlogari)this.element;
                if (llog.IdLlogari < 1)
                    return new clsMesazh(false, "Llogaria nuk ekziston!");
                //if (! clsKategoriShpenzimi.ekzistonKategori(kodi,idndermarje))
                //    return new clsMesazh(false, "Kategoria nuk ekziston!");
            }
            if (nrLlogShpenzimi != null && nrLlogShpenzimi != string.Empty)
            {
                clsLlogari LLogShpenz = new clsLlogari(nrLlogShpenzimi, idndermarje, dbK); //behet throw nese llogaria nuk ekziston
                //if (!clsLlogari.ekzistonLlogari(nrLlogShpenzimi, idndermarje))
                //    return new clsMesazh(false, "Llogaria e shpenzimit nuk ekziston!");
            }
            if ((tollon || tollonkastrati) && idLlojVeprimi == 1)
            {
                if (shenime == string.Empty)
                    return new clsMesazh(false, "Duhet te shenoni serialin fillestar te tollonave!");
                if (shenime != "0" && tollon)
                {
                    long serialitollon = 0;
                    long.TryParse(shenime, out serialitollon);
                    if (serialitollon == 0)
                        return new clsMesazh(false, "Seriali fillestar i tollonave duhet te jete numer!");
                }
                if (shenime != "0" && tollonkastrati)
                {
                    int length = shenime.Length;
                    if (length != 19)
                        return new clsMesazh(false, "Gjatesia e tollonit nuk eshte e sakte!");
                    long serialitollon = 0;
                    long.TryParse(shenime.Substring(10, 9), out serialitollon);
                    if (serialitollon == 0)
                        return new clsMesazh(false, "Seriali fillestar i tollonave nuk eshte ne formatin e duhur!");
                }
            }
            return new clsMesazh(true, "Kontrollet u kaluan me sukses!");
        }


        //public clsMesazh ruaj()
        //{
        //    clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
        //    int id;
        //    clsMesazh u_ruajt = data.ruajTrupiShitje(out id, this.IdShitjeKoka, this.IdLlojVeprimi, this.Kodi, this.Pershkrimi, this.IdDetajimArt, this.IdNjesia, this.Sasia, this.Cmimi, this.Zbritje, this.VleftaMeTvsh, this.Tvsh, this.VleftaPaTvsh, this.IdMagazina, this.IdKodi, this.IdDetajimArt2, this.gjeresi, this.gjatesi, this.sasiPermasa, this.shenime, this.dtFillimi, this.dtMbarimi, this.idTrupiKonvertimi, this.sasiRez, this.idTrupiRezervimi, this.idTrupiTransferimi);
        //    this.IdShitjeTrupi = id;
        //    data.Dispose();
        //    return u_ruajt;
        //}

        /// <summary>
        /// Modifikon objektin e  trupit te dokumentit ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.modifikoTrupiShitje"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo</returns>      
        public clsMesazh modifiko()
        {
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            clsMesazh u_modifikua = data.modifikoTrupiShitje(this.IdShitjeTrupi, this.IdShitjeKoka, this.IdLlojVeprimi, this.Kodi, this.Pershkrimi, this.IdDetajimArt, this.IdNjesia, this.Sasia, this.Cmimi, this.Zbritje, this.VleftaMeTvsh, this.Tvsh, this.VleftaPaTvsh, this.IdMagazina, this.IdKodi, this.IdDetajimArt2, this.gjeresi, this.gjatesi, this.sasiPermasa, this.shenime, this.dtFillimi, this.dtMbarimi, this.idTrupiKonvertimi, this.sasiRez, this.idTrupiRezervimi, this.idTrupiTransferimi, this.idLlogShpenzimi, this.idTrupiKthim, this.idTrupiKonvertimBlerje, this.shenime2, this.zbritjeNeVlere, this.zbritjeVlere, this.idKategoriShpenzimi, this.IdBarkodi, this.IdTrupiTransferimNga);
            data.Dispose();
            return u_modifikua;
        }

        public static double merrSasiPorositur(int idartikulli)
        {
            using (clsDatabaseRegjistrim data = new clsDatabaseRegjistrim())
            {
                return data.merrSasiPorositur(idartikulli);
            }
        }

        /// <summary>
        /// merr sasine e porositur qe eshte regjistruar si urdher blerje sipas magazines
        /// </summary>
        /// <param name="idartikulli"></param>
        /// <param name="idmag"></param>
        /// <returns></returns>
        public static double merrSasiPorositurSipasMagazines(int idartikulli, int idmag)
        {
            using (clsDatabaseRegjistrim data = new clsDatabaseRegjistrim())
            {
                return data.merrSasiPorositurSipasMagazines(idartikulli, idmag);
            }
        }

        /// <summary>
        /// Fshin objektin e  trupit te dokumentit ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.fshiTrupishitjeSipasID"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>      
        public clsMesazh fshi()
        {
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            clsMesazh u_fshi = data.fshiTrupishitjeSipasID(this.IdShitjeTrupi);
            data.Dispose();
            return u_fshi;
        }

        public bool merrSipasIdTrasferimi(int idtransferimi)
        {
            using (clsDatabaseRegjistrim dbTrupShitje = new clsDatabaseRegjistrim())
            {
                return mbushTrupShitje(dbTrupShitje.ktheTrupiShitjeSipasIDTransferimi(idtransferimi));
            }
        }
        public static int merrSasineMbetur(int idtrupi)
        {
            using (var db = new clsDatabaseRegjistrim())
                return db.merrSasineMbetur(idtrupi);

        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush trupin e shitjes nga databaza
        /// </summary>
        /// <param name="dbDataRowTrupShitje">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushTrupShitje(DataRow dbDataRowTrupShitje)
        {
            if (dbDataRowTrupShitje != null)
            {
                try
                {
                    int.TryParse(dbDataRowTrupShitje["IDSHITJETRUPI"].ToString(), out idShitjeTrupi);
                    int.TryParse(dbDataRowTrupShitje["IDSHITJEKOKA"].ToString(), out idShitjeKoka);
                    int.TryParse(dbDataRowTrupShitje["IDLLOJVEPRIMI"].ToString(), out idLlojVeprimi);
                    int.TryParse(dbDataRowTrupShitje["IDKODI"].ToString(), out idKodi);
                    kodi = dbDataRowTrupShitje["KODI"].ToString();
                    pershkrimi = dbDataRowTrupShitje["PERSHKRIMI"].ToString();
                    int.TryParse(dbDataRowTrupShitje["IDDETAJIMART"].ToString(), out idDetajimArt);
                    int.TryParse(dbDataRowTrupShitje["IDDETAJIMART2"].ToString(), out idDetajimArt2);
                    int.TryParse(dbDataRowTrupShitje["IDNJESIA"].ToString(), out idNjesia);
                    double.TryParse(dbDataRowTrupShitje["SASIA"].ToString(), out sasia);
                    double.TryParse(dbDataRowTrupShitje["SASIREZ"].ToString(), out sasiRez);
                    double.TryParse(dbDataRowTrupShitje["GJERESI"].ToString(), out gjeresi);
                    double.TryParse(dbDataRowTrupShitje["GJATESI"].ToString(), out gjatesi);
                    double.TryParse(dbDataRowTrupShitje["SASIPERMASE"].ToString(), out sasiPermasa);
                    double.TryParse(dbDataRowTrupShitje["CMIMI"].ToString(), out cmimi);
                    double.TryParse(dbDataRowTrupShitje["ZBRITJE"].ToString(), out zbritje);
                    int.TryParse(dbDataRowTrupShitje["LLOJZBRITJE"].ToString(), out zbritjeNeVlere);
                    double.TryParse(dbDataRowTrupShitje["ZBRITJEVLERE"].ToString(), out zbritjeVlere);
                    double.TryParse(dbDataRowTrupShitje["VLEFTAPATVSH"].ToString(), out vleftaPaTvsh);
                    int.TryParse(dbDataRowTrupShitje["TVSH"].ToString(), out tvsh);
                    double.TryParse(dbDataRowTrupShitje["VLEFTAMETVSH"].ToString(), out vleftaMeTvsh);
                    int.TryParse(dbDataRowTrupShitje["IDMAGAZINA"].ToString(), out idMagazina);
                    shenime = dbDataRowTrupShitje["SHENIME"].ToString();
                    dtFillimi = DateTime.Parse(dbDataRowTrupShitje["DTFILLIMI"].ToString());
                    dtMbarimi = DateTime.Parse(dbDataRowTrupShitje["DTMBARIMI"].ToString());
                    double.TryParse(dbDataRowTrupShitje["sasiambetur"].ToString(), out sasimbetur);
                    int.TryParse(dbDataRowTrupShitje["IDTRUPIKONVERTIMI"].ToString(), out idTrupiKonvertimi);
                    int.TryParse(dbDataRowTrupShitje["IDTRUPIKONVERTIMBLERJE"].ToString(), out idTrupiKonvertimBlerje);
                    int.TryParse(dbDataRowTrupShitje["IDTRUPIREZERVIMI"].ToString(), out idTrupiRezervimi);
                    int.TryParse(dbDataRowTrupShitje["IDTRUPITRANSFERIMI"].ToString(), out idTrupiTransferimi);
                    int.TryParse(dbDataRowTrupShitje["IDTRUPIKTHIMI"].ToString(), out idTrupiKthim);
                    int.TryParse(dbDataRowTrupShitje["IDLLOGSHPENZIMI"].ToString(), out idLlogShpenzimi);
                    nrLlogShpenzimi = dbDataRowTrupShitje["NRLLOGSHPENZIMI"].ToString();
                    shenime2 = dbDataRowTrupShitje["SHENIME2"].ToString();
                    int.TryParse(dbDataRowTrupShitje["IDBARKODI"].ToString(), out idBarkodi);
                    int.TryParse(dbDataRowTrupShitje["IDTRUPITRANSFERIMNGA"].ToString(), out idTrupiTransferimNga);
                    if (!(dbDataRowTrupShitje["VleraKomision"] is System.DBNull))
                        vleraKomisionit = Convert.ToDouble(dbDataRowTrupShitje["VleraKomision"]);
                    if (!(dbDataRowTrupShitje["LLOGARITKOMISION"] is System.DBNull))
                        meKomision = Convert.ToBoolean(dbDataRowTrupShitje["LLOGARITKOMISION"]);


                    return true;

                }
                catch (InvalidCastException)
                {
                    ImbLogger.LogErrorShitje("ERROR: Gabim gjate marrjes se trupit te shitjes nga db-ja");
                    throw new Exception("ERROR: Gabim gjate marrjes se trupit te shitjes nga db-ja");
                }
            }
            else
                return false;
        }
        internal bool mbushTrupShitjeWebhook(DataRow dbDataRowTrupShitje)
        {
            if (dbDataRowTrupShitje != null)
            {
                try
                {
                    int.TryParse(dbDataRowTrupShitje["IDSHITJETRUPI"].ToString(), out idShitjeTrupi);
                    int.TryParse(dbDataRowTrupShitje["IDSHITJEKOKA"].ToString(), out idShitjeKoka);
                    int.TryParse(dbDataRowTrupShitje["IDLLOJVEPRIMI"].ToString(), out idLlojVeprimi);
                    int.TryParse(dbDataRowTrupShitje["IDKODI"].ToString(), out idKodi);
                    kodi = dbDataRowTrupShitje["KODI"].ToString();
                    pershkrimi = dbDataRowTrupShitje["PERSHKRIMI"].ToString();
                    int.TryParse(dbDataRowTrupShitje["IDDETAJIMART"].ToString(), out idDetajimArt);
                    int.TryParse(dbDataRowTrupShitje["IDDETAJIMART2"].ToString(), out idDetajimArt2);
                    int.TryParse(dbDataRowTrupShitje["IDNJESIA"].ToString(), out idNjesia);
                    double.TryParse(dbDataRowTrupShitje["SASIA"].ToString(), out sasia);
                    double.TryParse(dbDataRowTrupShitje["SASIREZ"].ToString(), out sasiRez);
                    double.TryParse(dbDataRowTrupShitje["GJERESI"].ToString(), out gjeresi);
                    double.TryParse(dbDataRowTrupShitje["GJATESI"].ToString(), out gjatesi);
                    double.TryParse(dbDataRowTrupShitje["SASIPERMASE"].ToString(), out sasiPermasa);
                    double.TryParse(dbDataRowTrupShitje["CMIMI"].ToString(), out cmimi);
                    double.TryParse(dbDataRowTrupShitje["ZBRITJE"].ToString(), out zbritje);
                    int.TryParse(dbDataRowTrupShitje["LLOJZBRITJE"].ToString(), out zbritjeNeVlere);
                    double.TryParse(dbDataRowTrupShitje["ZBRITJEVLERE"].ToString(), out zbritjeVlere);
                    double.TryParse(dbDataRowTrupShitje["VLEFTAPATVSH"].ToString(), out vleftaPaTvsh);
                    int.TryParse(dbDataRowTrupShitje["TVSH"].ToString(), out tvsh);
                    double.TryParse(dbDataRowTrupShitje["VLEFTAMETVSH"].ToString(), out vleftaMeTvsh);
                    int.TryParse(dbDataRowTrupShitje["IDMAGAZINA"].ToString(), out idMagazina);
                    shenime = dbDataRowTrupShitje["SHENIME"].ToString();
                    dtFillimi = DateTime.Parse(dbDataRowTrupShitje["DTFILLIMI"].ToString());
                    dtMbarimi = DateTime.Parse(dbDataRowTrupShitje["DTMBARIMI"].ToString());
                    double.TryParse(dbDataRowTrupShitje["sasiambetur"].ToString(), out sasimbetur);
                    int.TryParse(dbDataRowTrupShitje["IDTRUPIKONVERTIMI"].ToString(), out idTrupiKonvertimi);
                    int.TryParse(dbDataRowTrupShitje["IDTRUPIKONVERTIMBLERJE"].ToString(), out idTrupiKonvertimBlerje);
                    int.TryParse(dbDataRowTrupShitje["IDTRUPIREZERVIMI"].ToString(), out idTrupiRezervimi);
                    int.TryParse(dbDataRowTrupShitje["IDTRUPITRANSFERIMI"].ToString(), out idTrupiTransferimi);
                    int.TryParse(dbDataRowTrupShitje["IDTRUPIKTHIMI"].ToString(), out idTrupiKthim);
                    int.TryParse(dbDataRowTrupShitje["IDLLOGSHPENZIMI"].ToString(), out idLlogShpenzimi);
                    nrLlogShpenzimi = dbDataRowTrupShitje["NRLLOGSHPENZIMI"].ToString();
                    shenime2 = dbDataRowTrupShitje["SHENIME2"].ToString();
                    int.TryParse(dbDataRowTrupShitje["IDBARKODI"].ToString(), out idBarkodi);
                    int.TryParse(dbDataRowTrupShitje["IDTRUPITRANSFERIMNGA"].ToString(), out idTrupiTransferimNga);
                    if (!(dbDataRowTrupShitje["VleraKomision"] is System.DBNull))
                        vleraKomisionit = Convert.ToDouble(dbDataRowTrupShitje["VleraKomision"]);
                    if (!(dbDataRowTrupShitje["LLOGARITKOMISION"] is System.DBNull))
                        meKomision = Convert.ToBoolean(dbDataRowTrupShitje["LLOGARITKOMISION"]);


                    return true;

                }
                catch (InvalidCastException)
                {
                    ImbLogger.LogErrorShitje("ERROR: Gabim gjate marrjes se trupit te shitjes nga db-ja");
                    throw new Exception("ERROR: Gabim gjate marrjes se trupit te shitjes nga db-ja");
                }
            }
            else
                return false;
        }
        
        internal bool mbushTrupShitjePerWebhook(DataRow dbDataRowTrupShitje, int idKokaShitje)
        {
            if (dbDataRowTrupShitje != null)
            {
                try
                {
                    int.TryParse(dbDataRowTrupShitje["IDSHITJETRUPI"].ToString(), out idShitjeTrupi);
                    int.TryParse(dbDataRowTrupShitje["IDSHITJEKOKA"].ToString(), out idShitjeKoka);
                    int.TryParse(dbDataRowTrupShitje["IDLLOJVEPRIMI"].ToString(), out idLlojVeprimi);
                    int.TryParse(dbDataRowTrupShitje["IDKODI"].ToString(), out idKodi);
                    kodi = dbDataRowTrupShitje["KODI"].ToString();
                    pershkrimi = dbDataRowTrupShitje["PERSHKRIMI"].ToString();
                    int.TryParse(dbDataRowTrupShitje["IDDETAJIMART"].ToString(), out idDetajimArt);
                    int.TryParse(dbDataRowTrupShitje["IDDETAJIMART2"].ToString(), out idDetajimArt2);
                    int.TryParse(dbDataRowTrupShitje["IDNJESIA"].ToString(), out idNjesia);
                    double.TryParse(dbDataRowTrupShitje["SASIA"].ToString(), out sasia);
                    double.TryParse(dbDataRowTrupShitje["SASIREZ"].ToString(), out sasiRez);
                    double.TryParse(dbDataRowTrupShitje["GJERESI"].ToString(), out gjeresi);
                    double.TryParse(dbDataRowTrupShitje["GJATESI"].ToString(), out gjatesi);
                    double.TryParse(dbDataRowTrupShitje["SASIPERMASE"].ToString(), out sasiPermasa);
                    double.TryParse(dbDataRowTrupShitje["CMIMI"].ToString(), out cmimi);
                    double.TryParse(dbDataRowTrupShitje["ZBRITJE"].ToString(), out zbritje);
                    int.TryParse(dbDataRowTrupShitje["LLOJZBRITJE"].ToString(), out zbritjeNeVlere);
                    double.TryParse(dbDataRowTrupShitje["ZBRITJEVLERE"].ToString(), out zbritjeVlere);
                    double.TryParse(dbDataRowTrupShitje["VLEFTAPATVSH"].ToString(), out vleftaPaTvsh);
                    int.TryParse(dbDataRowTrupShitje["TVSH"].ToString(), out tvsh);
                    double.TryParse(dbDataRowTrupShitje["VLEFTAMETVSH"].ToString(), out vleftaMeTvsh);
                    int.TryParse(dbDataRowTrupShitje["IDMAGAZINA"].ToString(), out idMagazina);
                    shenime = dbDataRowTrupShitje["SHENIME"].ToString();
                    dtFillimi = DateTime.Parse(dbDataRowTrupShitje["DTFILLIMI"].ToString());
                    dtMbarimi = DateTime.Parse(dbDataRowTrupShitje["DTMBARIMI"].ToString());
                    double.TryParse(dbDataRowTrupShitje["sasiambetur"].ToString(), out sasimbetur);
                    int.TryParse(dbDataRowTrupShitje["IDTRUPIKONVERTIMI"].ToString(), out idTrupiKonvertimi);
                    int.TryParse(dbDataRowTrupShitje["IDTRUPIKONVERTIMBLERJE"].ToString(), out idTrupiKonvertimBlerje);
                    int.TryParse(dbDataRowTrupShitje["IDTRUPIREZERVIMI"].ToString(), out idTrupiRezervimi);
                    int.TryParse(dbDataRowTrupShitje["IDTRUPITRANSFERIMI"].ToString(), out idTrupiTransferimi);
                    int.TryParse(dbDataRowTrupShitje["IDTRUPIKTHIMI"].ToString(), out idTrupiKthim);
                    int.TryParse(dbDataRowTrupShitje["IDLLOGSHPENZIMI"].ToString(), out idLlogShpenzimi);
                    nrLlogShpenzimi = dbDataRowTrupShitje["NRLLOGSHPENZIMI"].ToString();
                    shenime2 = dbDataRowTrupShitje["SHENIME2"].ToString();
                    int.TryParse(dbDataRowTrupShitje["IDBARKODI"].ToString(), out idBarkodi);
                    int.TryParse(dbDataRowTrupShitje["IDTRUPITRANSFERIMNGA"].ToString(), out idTrupiTransferimNga);
                    if (!(dbDataRowTrupShitje["VleraKomision"] is System.DBNull))
                        vleraKomisionit = Convert.ToDouble(dbDataRowTrupShitje["VleraKomision"]);
                    if (!(dbDataRowTrupShitje["LLOGARITKOMISION"] is System.DBNull))
                        meKomision = Convert.ToBoolean(dbDataRowTrupShitje["LLOGARITKOMISION"]);
                    return true;

                }
                catch (InvalidCastException)
                {
                    ImbLogger.LogErrorShitje("ERROR: Gabim gjate marrjes se trupit te shitjes nga db-ja");
                    throw new Exception("ERROR: Gabim gjate marrjes se trupit te shitjes nga db-ja");
                }
            }
            else
                return false;
        }

        public void mbushTrupShitjeNew(IDataRecord dbDataRowTrupShitje)
        {

            try
            {

                int.TryParse(dbDataRowTrupShitje["IDSHITJETRUPI"].ToString(), out idShitjeTrupi);
                int.TryParse(dbDataRowTrupShitje["IDSHITJEKOKA"].ToString(), out idShitjeKoka);
                int.TryParse(dbDataRowTrupShitje["IDLLOJVEPRIMI"].ToString(), out idLlojVeprimi);
                switch (idLlojVeprimi)
                {
                    case (int)llojRreshtiShitje.Artikull:
                    case (int)llojRreshtiShitje.Llogari:
                        break;
                    default:
                        throw new MyException($"u gjet nje lloj i panjohur ne trupin me idKoka{idShitjeKoka}");
                }
                int.TryParse(dbDataRowTrupShitje["IDKODI"].ToString(), out idKodi);
                kodi = dbDataRowTrupShitje["KODI"].ToString();
                pershkrimi = dbDataRowTrupShitje["PERSHKRIMI"].ToString();
                int.TryParse(dbDataRowTrupShitje["IDDETAJIMART"].ToString(), out idDetajimArt);
                int.TryParse(dbDataRowTrupShitje["IDDETAJIMART2"].ToString(), out idDetajimArt2);
                int.TryParse(dbDataRowTrupShitje["IDNJESIA"].ToString(), out idNjesia);
                double.TryParse(dbDataRowTrupShitje["SASIA"].ToString(), out sasia);
                double.TryParse(dbDataRowTrupShitje["SASIREZ"].ToString(), out sasiRez);
                double.TryParse(dbDataRowTrupShitje["GJERESI"].ToString(), out gjeresi);
                double.TryParse(dbDataRowTrupShitje["GJATESI"].ToString(), out gjatesi);
                double.TryParse(dbDataRowTrupShitje["SASIPERMASE"].ToString(), out sasiPermasa);
                double.TryParse(dbDataRowTrupShitje["CMIMI"].ToString(), out cmimi);
                double.TryParse(dbDataRowTrupShitje["ZBRITJE"].ToString(), out zbritje);
                int.TryParse(dbDataRowTrupShitje["LLOJZBRITJE"].ToString(), out zbritjeNeVlere);
                double.TryParse(dbDataRowTrupShitje["ZBRITJEVLERE"].ToString(), out zbritjeVlere);
                double.TryParse(dbDataRowTrupShitje["VLEFTAPATVSH"].ToString(), out vleftaPaTvsh);
                int.TryParse(dbDataRowTrupShitje["TVSH"].ToString(), out tvsh);
                double.TryParse(dbDataRowTrupShitje["VLEFTAMETVSH"].ToString(), out vleftaMeTvsh);
                int.TryParse(dbDataRowTrupShitje["IDMAGAZINA"].ToString(), out idMagazina);
                shenime = dbDataRowTrupShitje["SHENIME"].ToString();
                dtFillimi = DateTime.Parse(dbDataRowTrupShitje["DTFILLIMI"].ToString());
                dtMbarimi = DateTime.Parse(dbDataRowTrupShitje["DTMBARIMI"].ToString());
                double.TryParse(dbDataRowTrupShitje["sasiambetur"].ToString(), out sasimbetur);
                int.TryParse(dbDataRowTrupShitje["IDTRUPIKONVERTIMI"].ToString(), out idTrupiKonvertimi);
                int.TryParse(dbDataRowTrupShitje["IDTRUPIKONVERTIMBLERJE"].ToString(), out idTrupiKonvertimBlerje);
                int.TryParse(dbDataRowTrupShitje["IDTRUPIREZERVIMI"].ToString(), out idTrupiRezervimi);
                int.TryParse(dbDataRowTrupShitje["IDTRUPITRANSFERIMI"].ToString(), out idTrupiTransferimi);
                int.TryParse(dbDataRowTrupShitje["IDTRUPIKTHIMI"].ToString(), out idTrupiKthim);
                int.TryParse(dbDataRowTrupShitje["IDLLOGSHPENZIMI"].ToString(), out idLlogShpenzimi);
                nrLlogShpenzimi = dbDataRowTrupShitje["NRLLOGSHPENZIMI"].ToString();
                shenime2 = dbDataRowTrupShitje["SHENIME2"].ToString();
                int.TryParse(dbDataRowTrupShitje["IDKATEGORISHPENZIMI"].ToString(), out idKategoriShpenzimi);
                int.TryParse(dbDataRowTrupShitje["IDBARKODI"].ToString(), out idBarkodi);
                int.TryParse(dbDataRowTrupShitje["IDTRUPITRANSFERIMNGA"].ToString(), out idTrupiTransferimNga);
                sasiaEMbetur = sasia;
            }
            catch (Exception myex)
            {
                //logo errorin ketu
                throw new MyException(logu, $"Ndodhi nje gabim ne mbushjen  e trupit te shitjes {idShitjeTrupi}");
            }

        }

        public static clsTrupiShitje KrijoTrupShitje(IDataRecord dbDataRowTrupShitje)
        {
            clsTrupiShitje trupi = new clsTrupiShitje();
            trupi.mbushTrupShitjeNew(dbDataRowTrupShitje);
            return trupi;
        }

        public double getSasiTeMbetur()
        {
            return sasiaEMbetur;
        }
        public bool kaSasiTeMbetur()
        {
            return sasiaEMbetur > 0;
        }
        public bool ulSasiTeMbetur(double sasi)
        {
            if (sasiaEMbetur - sasi < 0)
                return false;
            sasiaEMbetur -= sasi;

            return true;

        }

        #endregion

        internal  clsTrupiShitje Clone()
        {
            return (clsTrupiShitje)this.MemberwiseClone();
        }

    }
}