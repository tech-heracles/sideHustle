using DbCore.DbInventari;
using DbCore.DbShare;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Resources;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Logging;
using DbCore.DbAsete;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne  trupin e nje dokumenti magazine
    ///  (Te dhenat  merren nga tabela : T_TRUPIMAGAZINA)
    /// </summary>
    public class clsTrupiMagazina
    {

        #region Attributet
        private int idTrupiMagazina;
        private int idKokaMagazina;
        private int idLlojVeprimi;
        private int idArtikull;
        private string kodArtikull;
        private string pershkrimArtikull;
        private int idNjesia;
        private double sasia;
        private double cmimi;
        private double vlefta;
        private double koeficenti;
        private int shenja;
        private double sasiaProgresive;
        private double vleftaProgresive;
        private int idMag;
        private DateTime data;
        private int idStatusDok;
        private int idRenditjes;
        private int idDetajimi;
        private int idDetajimi2;
        private string kodDetajimi1;
        private string kodDetajimi2;
        private double sasiProgresiveDetajimi;
        private double vlefteProgresiveDetajimi;
        private int idTrupiRezervimi;
        private int idTrupiKonvertimFSH;
        private int idTrupiKonvertimUSH;
        private int idTrupiKonvertimUD;
        private double sasimbetur;
        private object element;
        private int rreshtiShitjes;
        private int idKthimi;
        private int idTrupiShitjeGjenerimi;
        private string shenime;
        private int idTrupiMagazinaOld;
        private colSerialeUnikeMagazina oColSerialeUnikeMagazina;
        private int idArtikullSet;
        private int idBarkodi;

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases me parametra
        /// </summary>
        /// <param name="cm">cmimi i artikullit</param>
        /// <param name="dt"> data e dokumentit</param>
        /// <param name="idArt"> id e artikullit</param>
        /// <param name="iddetajimi">id e detajimit te artikullit</param>
        /// <param name="idLlojVep"> id e llojit te veprimit</param>
        /// <param name="idmag">id e magazines</param>
        /// <param name="idMagKoka">id e kokes se dokumentit te magazines</param>
        /// <param name="idMagTrup">id ritese e trupit te dokumentit te magazines</param>
        /// <param name="idNjes">id e njesise se artikullit</param>
        /// <param name="idrenditjes"> id e renditjes se dokumentave te nje date</param>
        /// <param name="idstatusdok">id e gjendjes se dokumentit</param>
        /// <param name="KodArt">kodi i artikullit</param>
        /// <param name="koefic">koeficienti midis dy njesive te artikullit</param>
        /// <param name="pershkArt">pershkrimi i artikullit</param>
        /// <param name="sas"> sasia e artikullit</param>
        /// <param name="sasiaProg"> sasia progresive e artikullit</param>
        /// <param name="sasiprogdet"> sasia progresive e detajimit te artikullit</param>
        /// <param name="sgn"> shenja e veprimit</param>
        /// <param name="vl"> vlefta e artikullit</param>
        /// <param name="vlefteprogdet"> vlefta progresive e detajimit te artikullit</param>
        /// <param name="vlProg"> vlefta progresive e artikullit</param>
        /// <param name="iddet2">detajimi i dyte i trupit te magazines</param>
        public clsTrupiMagazina(int idMagTrup, int idMagKoka, int idLlojVep, int idArt, string KodArt, string pershkArt, int idNjes, double sas, double cm,
          double vl, double koefic, int sgn, double sasiaProg, double vlProg, int idmag, DateTime dt, int idstatusdok, int idrenditjes, int iddetajimi, double sasiprogdet, double vlefteprogdet, int iddet2, int idtrupirez, int idtrupikonvfsh, int idtrupikonvush, int idtrupikonvud, int idkthimi, int idtrupishitjegjenerimi, 
          object elem, string shenime, int idartikullset, int idBarkodi)
        {
            idTrupiMagazina = idMagTrup;
            idKokaMagazina = idMagKoka;
            idLlojVeprimi = idLlojVep;
            idArtikull = idArt;
            kodArtikull = KodArt;
            pershkrimArtikull = pershkArt;
            idNjesia = idNjes;
            sasia = sas;
            cmimi = cm;
            vlefta = vl;
            koeficenti = koefic;
            shenja = sgn;
            sasiaProgresive = sasiaProg;
            vleftaProgresive = vlProg;
            idMag = idmag;
            data = dt;
            idStatusDok = idstatusdok;
            idRenditjes = idrenditjes;
            idDetajimi = iddetajimi;
            idDetajimi2 = iddet2;
            this.idTrupiRezervimi = idtrupirez;
            this.idTrupiKonvertimFSH = idtrupikonvfsh;
            this.idTrupiKonvertimUSH = idtrupikonvush;
            this.idTrupiKonvertimUD = idtrupikonvud;
            sasiProgresiveDetajimi = sasiprogdet;
            vlefteProgresiveDetajimi = vlefteprogdet;
            element = elem;
            this.idKthimi = idkthimi;
            this.idTrupiShitjeGjenerimi = idtrupishitjegjenerimi;
            this.shenime = shenime;
            this.idArtikullSet = idartikullset;
            this.idBarkodi = idBarkodi;
        }

        public clsTrupiMagazina ShallowCopy()
        {
            return (clsTrupiMagazina)this.MemberwiseClone();
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idTrupiMagazina"></param>
        public clsTrupiMagazina(int idTrupiMagazina)
        {
            using (clsDatabaseRegjistrim dbtrupMagazine = new clsDatabaseRegjistrim())
            {
                mbushTrupMagazine(dbtrupMagazine.ktheTrupiMagazinaSipasID(idTrupiMagazina));
            }
        }
        public clsTrupiMagazina(DataRow dbDataRowTrupMagazine)
        {
            mbushTrupMagazine(dbDataRowTrupMagazine);
        }     

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idTrupiMagazina"></param>
        /// <param name="dbtrupMagazine"></param>
        public bool krijoTrupMagazinaNgaGrida(int idNdermarrje, DateTime date, double kursi, int lloji, int idkodi, string emertimi, int detajimi, int njesia, double sasia, double cmimi, double VleftaPaTVSH, int magazina, int shenja, int iddetajim2, string kodi, string kodDetajimi1, string kodDetajimi2, int idkonvertimifsh, int idkonvertimiush, int idkonvertimiud, int rreshti, clsArtikulli art, string shenime, int idartikulliset, clsDatabaseInventari db, bool blerjedealer, int idBarkod)
        {
            ImbLogger.LogWarningShitje("Filloi metoda krijotrupMagazinaNgaGrida");
            if (idkodi == 0 || idkodi == -1)
                return false;
            this.idLlojVeprimi = lloji;
            if (lloji != 1)
                return false;
            if (art.IdArtikulli == 0)
            {
                ImbLogger.LogErrorShitje($"Nje nga artikujt nuk ekziston! art.IdArtikulli = {art.IdArtikulli}");
                {
                    ImbLogger.LogErrorShitje("Nje nga artikujt nuk ekziston!");
                    throw new Exception("Nje nga artikujt nuk ekziston!");
                }
                
            }
                
            if (art.Klasa == 2 || art.Klasa == 3)
                return false;
            if (art.Klasa == 4)
            {
                ImbLogger.LogErrorShitje("Artikull i perbere!");
                throw new Exception("Artikull i perbere!");
            }
                
            else
            {
                idArtikull = idkodi;
                kodArtikull = kodi;
                pershkrimArtikull = emertimi;
                idNjesia = njesia; ;
                if (art.Njesi1Artikulli == idNjesia)//nese njesia e zgjedhuer eshte njesia 1 e artikullit ath koeficienti vendoset 1
                    this.koeficenti = 1;
                else
                    this.koeficenti = double.Parse(art.KoeficientArtikulli.ToString());//nese njesia e zgjedhur nuk eshte njesia e pare e artikullit ath koeficienti vendoset sa koeficienti i percaktuar tek artikulli     
                this.sasia = blerjedealer ? -sasia : sasia;
                this.cmimi = cmimi * kursi;
                this.vlefta = blerjedealer ? -(VleftaPaTVSH * kursi) : VleftaPaTVSH * kursi;


                idMag = magazina;
                data = date;
                this.shenja = shenja;
                idDetajimi = detajimi;
                idDetajimi2 = iddetajim2;
                this.kodDetajimi1 = kodDetajimi1;
                this.kodDetajimi2 = kodDetajimi2;
                this.idTrupiKonvertimFSH = idkonvertimifsh;
                this.idTrupiKonvertimUSH = idkonvertimiush;
                this.idTrupiKonvertimUD = idkonvertimiud;
                this.element = art;
                this.rreshtiShitjes = rreshti;
                this.shenime = shenime;
                this.idArtikullSet = idartikulliset;
                this.idBarkodi = idBarkod;
            }
            ImbLogger.LogWarningShitje("Mbaroi metoda krijotrupMagazinaNgaGrida");
            return true;
        }
      

       
        public bool krijoTrupMagazinaNgaGrida(int idNdermarrje, DateTime date, double kursi, int lloji, int idkodi, string emertimi, int detajimi, int njesia, double sasia, double cmimi, double VleftaPaTVSH, int magazina, int shenja, int iddetajim2, string kodi, string kodDetajimi1, string kodDetajimi2, int idkonvertimifsh, int idkonvertimiush, int idkonvertimiud, string shenime, int idartikullset,clsDatabaseInventari db, bool blerjedealer, int idBarkodi, int rreshti)
        {
            if (idkodi == 0 || idkodi == -1)
                return false;
            this.idLlojVeprimi = lloji;
            if (lloji != 1)
                return false;
            clsArtikulli art = new clsArtikulli(idkodi, db);
            return krijoTrupMagazinaNgaGrida(idNdermarrje, date, kursi, lloji, idkodi, emertimi, detajimi, njesia, sasia, cmimi, VleftaPaTVSH, magazina, shenja, iddetajim2, kodi, kodDetajimi1, kodDetajimi2, idkonvertimifsh, idkonvertimiush, idkonvertimiud, rreshti, art, shenime,idartikullset, db,blerjedealer, idBarkodi);
        }

        /// <summary>
        /// Sherben per te krijuar trupMagazinen nga grida client
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="eshteTransferim"></param>
        /// <param name="rreshtDokuKlient"></param>
        public clsTrupiMagazina(int idNdermarrje, int idPerdorues, bool eshteTransferim, DateTime date, Dictionary<string, object> rreshtDokuKlient, bool ownshop, string kodkonfigurimi, bool klonim, bool meAutorizim, bool ruajBarkod)
        {
            string kategoria = rreshtDokuKlient["txtKategoria"].ToString();
            string kodi = rreshtDokuKlient["txtKodi"].ToString();
            if (kodi == "" || kodi == null || kodi == "null")
            {
                idArtikull = -1;
                return;
            }
            int idKodi = 0;
            int.TryParse(rreshtDokuKlient["txtIdKodi"].ToString(), out idKodi);
            string artset = rreshtDokuKlient["txtIdArtikullSet"].ToString();
        
            string emertimi = rreshtDokuKlient["txtEmertimi"].ToString();
            string detajimet = rreshtDokuKlient["txtDetajimi"].ToString();
            string detajimet2 = rreshtDokuKlient["txtDetajimi2t"].ToString();
            string njesia = rreshtDokuKlient["txtNjesia"].ToString();
            string sasia = rreshtDokuKlient["txtSasia"].ToString();
            string cmimi = rreshtDokuKlient["txtCmimi"].ToString();
            string vlefta = rreshtDokuKlient["txtVlefta"].ToString();
            string magazina = rreshtDokuKlient["txtMagazina"].ToString();
            string magazina2 = rreshtDokuKlient["txtMagazina2"].ToString();
            int idtrupirez = 0;
            int.TryParse(rreshtDokuKlient["txtIdTrupiRezervimi"].ToString(), out idtrupirez);
            int idtrupi = 0;
            int.TryParse(rreshtDokuKlient["txtIdTrupi"].ToString(), out idtrupi);
            int idtrupikonv = 0;
            int.TryParse(rreshtDokuKlient["txtIdTrupiKonvertimi"].ToString(), out idtrupikonv);
            int idtrupikonvush = 0;
            int.TryParse(rreshtDokuKlient["txtIdTrupiKonvertimiUSH"].ToString(), out idtrupikonvush);
            int idtrupikonvud = 0;
            int.TryParse(rreshtDokuKlient["txtIdTrupiKonvertimiUD"].ToString(), out idtrupikonvud);
            int idkthimi = 0;
            int.TryParse(rreshtDokuKlient["txtIdKthimi"].ToString(), out idkthimi);
            int idTrupiShitjeGjenerimi = 0;
            int.TryParse(rreshtDokuKlient["txtIdTrupiShitjeGjenerimi"].ToString(), out idTrupiShitjeGjenerimi);
            string shenime = rreshtDokuKlient["txtShenime"].ToString();
            string barkodi = ruajBarkod ? rreshtDokuKlient["txtKodbari"].ToString() : String.Empty; 

            if (!(kategoria == "Artikull" || kategoria == "Makro"))
                throw new Exception("Kategoria nuk mund te jete e ndryshme nga Makro dhe Artikull te rregjistrim dokument magazine");
            idLlojVeprimi = DbShare.clsKonfLlojRreshtiVlere.ktheIdLlojRreshtiVlere(kategoria, "Shitje");
            clsArtikulli art = new clsArtikulli();
            //if (!performante)
            //    if (!clsArtikulli.ekziston(kodi, idNdermarrje))
            //    {
            //        throw new Exception("Artikulli me kod: " + kodi + " nuk ekziston!");
            //    }
            art.ktheArtikullSipasKoditDheAutorizime(kodi, idNdermarrje, idPerdorues);
            if (art.IdArtikulli == 0)
            {
                throw new Exception("Artikulli nuk ekziston ose nuk keni autorizime per artikullin " + kodi + "!");
            }
            if (!art.Aktiv)
            {
                throw new Exception("Artikulli me kod:" + art.KodArtikulli + " nuk eshte aktiv!");
            }
            
            if (idKodi != art.IdArtikulli)
                throw new Exception("Artikulli me kod: " + art.KodArtikulli + " eshte marre gabimisht!");
            idArtikull = art.IdArtikulli;

            clsArtikulli artper = new clsArtikulli();
            if (artset != "")
            {
                artper.ktheArtikullSipasKoditDheAutorizime(artset, idNdermarrje, idPerdorues);
                if (artper.IdArtikulli == 0)
                {
                    throw new Exception("Artikulli i perbere nuk ekziston ose nuk keni autorizime per artikullin " + artset + "!");
                }
                if (!artper.Aktiv)
                {
                    throw new Exception("Artikulli me kod:" + artper.KodArtikulli + " nuk eshte aktiv!");
                }
                if (artper.Klasa != 4)
                    throw new Exception("Artikulli me kod: " + artper.KodArtikulli + " nuk i perket klases se perbere!");
                idArtikullSet = artper.IdArtikulli;
            }
            //trupi.IdArtikulli = dbInventari.merrArtikullSipasKodit(art).IdArtikulli;
            kodArtikull = kodi;
            if (!klonim)
            {
                this.idTrupiRezervimi = idtrupirez;
                this.idTrupiKonvertimFSH = idtrupikonv;
                this.idTrupiKonvertimUSH = idtrupikonvush;
                this.idTrupiKonvertimUD = idtrupikonvud;
                this.idKthimi = idkthimi;
                this.idTrupiShitjeGjenerimi = idTrupiShitjeGjenerimi;
                this.IdTrupiMagazina = idtrupi;
            }
            else
            {
                this.idTrupiRezervimi = 0;
                this.idTrupiKonvertimFSH = 0;
                this.idTrupiKonvertimUSH = 0;
                this.idTrupiKonvertimUD = 0;
                this.idKthimi = 0;
                this.idTrupiShitjeGjenerimi = 0;
                this.IdTrupiMagazina = 0;
            }

            if (emertimi != null && emertimi != "null")
                pershkrimArtikull = emertimi;
            if (detajimet != null && detajimet != "null" && detajimet != "*" && detajimet != "" && detajimet != "Pa detajime" && detajimet != "Pa detajim")
            {
                //clsDetajimArtikulli detArtikulli = new clsDetajimArtikulli();
                //detArtikulli.mbushDetajimArtikulli(detajimet, idNdermarrje);

                this.idDetajimi = clsDetajimArtikulli.ktheIdDetajimi(detajimet, idNdermarrje);
                if (idDetajimi > 0)
                    this.kodDetajimi1 = detajimet;
                if (ownshop) //nqs eshte ownshop do shohim nqs id = 0 ndersa kodi != "", ne kete rast do krijohet detajimi me kete kod.
                {
                    if (this.idDetajimi == 0 && !String.IsNullOrEmpty(detajimet))
                    {
                        int llojDetajim = 0;
                        if (art.IdKategoriDetajimi == 3)
                            llojDetajim = 3;
                        else if (art.IdKategoriDetajimi == 2)
                            llojDetajim = 2;
                        else if (art.IdKategoriDetajimi == 4)
                            llojDetajim = 1;

                        clsDetajimArtikulli detajimRi = new clsDetajimArtikulli(detajimet, llojDetajim, "", idPerdorues, art.IdKategoriDetajimi, idNdermarrje, "", 1, 1);
                        clsMesazh mesazh = detajimRi.ruajShpejte(art, 1);
                        if (mesazh.Status)
                        {
                            this.kodDetajimi1 = detajimet;
                            this.idDetajimi = detajimRi.IdDetajimArtikulli;
                        }
                        else
                            throw new Exception("Gabim gjate krijimit te detajimit te pare!");
                    }
                    else if (this.idDetajimi > 0 && !String.IsNullOrEmpty(detajimet)) //nqs o.IdDetajimArt=-1 ath nuk do krijohet as nuk do lidhet ndonje detajim
                    {
                        bool lidhurMeArt = clsDetajimArtikulli.ekzistonDetajimLidhurMeArtikullin(detajimet, idNdermarrje, art.KodArtikulli, 1);
                        if (!lidhurMeArt)
                        {
                            this.kodDetajimi1 = detajimet;
                            clsMesazh mesazh = clsDetajimPerArt.ruajLidhje(art, this.idDetajimi, 1, idNdermarrje, idPerdorues);
                            if (!mesazh.Status)
                                throw new Exception("Gabim gjate lidhjes se detajimit te pare me artikullin!");
                        }
                    }
                }
            }
            else
                idDetajimi = -1;
            if (detajimet2 != null && detajimet2 != "null" && detajimet2 != "*" && detajimet2 != "" && detajimet2 != "Pa detajime" && detajimet2 != "Pa detajim")
            {
                idDetajimi2 = clsDetajimArtikulli.ktheIdDetajimi(detajimet2, idNdermarrje);
                if (idDetajimi2 > 0)
                    this.kodDetajimi2 = detajimet2;
            }
            else
                idDetajimi2 = -1;
            if (njesia != null && njesia != "null" && njesia != "")
            {
                //clsNjesiArtikulli njesi = new clsNjesiArtikulli();
                //njesi.mbushNjesiArtikulliMeKod(njesia, idNdermarrje); //kevi ndryshim nga me pershk me kod                
                idNjesia = clsNjesiArtikulli.ktheIdNjesiArtikulli(njesia, idNdermarrje); //njesi.IdNjesia;
                if (art.Njesi1Artikulli == idNjesia)//nese njesia e zgjedhuer eshte njesia 1 e artikullit ath koeficienti vendoset 1
                    koeficenti = 1;
                else
                    koeficenti = double.Parse(art.KoeficientArtikulli.ToString());
                //nese njesia e zgjedhur nuk eshte njesia e pare e artikullit ath koeficienti vendoset sa koeficienti i percaktuar tek artikulli
            }
            if (sasia != null && sasia != "null" && sasia != "")
                this.sasia = double.Parse(sasia);
            if (cmimi != null && cmimi != "null" && cmimi != "")
                this.cmimi = double.Parse(cmimi);
            if (vlefta != null && vlefta != "null" && vlefta != "")
                this.vlefta = double.Parse(vlefta);
            if (!String.IsNullOrEmpty(shenime))
                this.shenime = shenime;
            else
                this.shenime = String.Empty;
            int idMagHyrje = -1;
            int idMagDestinacion = -1;
            if (magazina != null && magazina != "null" && magazina != "")
            {
                DbCore.DbRegjistrim.clsNjesiAdministrative njesiadm;
                njesiadm = new clsNjesiAdministrative(magazina, idNdermarrje, idPerdorues);

                if (njesiadm.IdNjesiAdministrative < 1)
                    throw new Exception("Magazina nuk ekziston ose nuk keni autorizime per magazinen: " + magazina + "!");
                if (magazina != "" && !njesiadm.Aktiv)
                    throw new Exception("Magazina: " + magazina + " nuk eshte aktive!");
                idMagHyrje = njesiadm.IdNjesiAdministrative;
            }

            if (magazina2 != null && magazina2 != "null" && magazina2 != "" && kodkonfigurimi != "FHNV" && kodkonfigurimi != "FDNV")
            {
                DbCore.DbRegjistrim.clsNjesiAdministrative njesiadm = new DbCore.DbRegjistrim.clsNjesiAdministrative(magazina2, idNdermarrje);
                if (njesiadm.IdNjesiAdministrative <= 0)
                    throw new Exception("Magazina destinacion: " + magazina2 + " nuk ekziston!");
                if (meAutorizim)
                {
                    njesiadm = new clsNjesiAdministrative(magazina2, idNdermarrje, idPerdorues);
                    if (njesiadm.IdNjesiAdministrative < 1)
                        throw new Exception("Nuk keni autorizime per magazinen " + magazina2 + "!");
                }
                if (magazina2 != "" && !njesiadm.Aktiv)
                    throw new Exception("Magazina: " + magazina2 + " nuk eshte aktive!");
                idMagDestinacion = njesiadm.IdNjesiAdministrative;
            }

            if (eshteTransferim == true)
                idMag = idMagDestinacion;
            else
                idMag = idMagHyrje;
            data = date;
            this.idBarkodi = clsKodbari.MerrIdBarkodiSipasPershkrimDheArtikulli(barkodi, this.IdArtikulli);
            element = art;
        }

        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsTrupiMagazina()
        {
        }


        

        #endregion

        #region Properties
        /// <summary>
        /// id e kthimit nga shitje blerja
        /// </summary>
        public int IdKthimi
        {
            get
            {
                return idKthimi;
            }
            set
            {
                idKthimi = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdTrupiMagazina
        {
            get { return idTrupiMagazina; }
            set { idTrupiMagazina = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e kokes se dokumentit te magazines.
        /// </summary>
        public int IdKokaMagazina
        {
            get { return idKokaMagazina; }
            set { idKokaMagazina = value; }
        }
        public int IdTrupiKonvertimUSH
        {
            get
            {
                return idTrupiKonvertimUSH;
            }
            set
            {
                idTrupiKonvertimUSH = value;
            }
        }
        public int IdTrupiKonvertimUD
        {
            get
            {
                return idTrupiKonvertimUD;
            }
            set
            {
                idTrupiKonvertimUD = value;
            }
        }
        /// <summary>
        /// id e trupit te shitjes nga eshte gjeneruar kur fatura gjeneron dokument magaizne
        /// </summary>
        public int IdTrupiShitjeGjenerimi
        {
            get
            {
                return idTrupiShitjeGjenerimi;
            }
            set
            {
                idTrupiShitjeGjenerimi = value;
            }
        }
        /// <summary>
        /// mban nr e rreshtit te shitjes nga ka ardhur gjate krijimit te magazines nga shitja/blerja
        /// </summary>
        public int RreshtiShitjes
        {
            get
            {
                return rreshtiShitjes;
            }
        }
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
        /// mbahet artikulli qe te mos aksesohet disa here nga db
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
        /// id e trupit te shitjes nga eshte bere konvertimi
        /// </summary>
        public int IdTrupiKonvertimFSH
        {
            get
            {
                return idTrupiKonvertimFSH;
            }
            set
            {
                idTrupiKonvertimFSH = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e llojit te veprimit.
        /// <example>Artikull,Makro</example>
        /// </summary>
        public int IdLlojVeprimi
        {
            get { return idLlojVeprimi; }
            set { idLlojVeprimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e artikullit .
        /// </summary>
        public int IdArtikulli
        {
            get { return idArtikull; }
            set { idArtikull = value; }
        }

        /// <summary>
        /// Kthen/Vendos Kodi i artikullit.
        /// </summary>
        public String KodiArtikull
        {
            get { return kodArtikull; }
            set { kodArtikull = value; }
        }

        /// <summary>
        /// Kthen/Vendos pershkrimi i artikullit.
        /// </summary>
        public String PershkrimArtikull
        {
            get { return pershkrimArtikull; }
            set { pershkrimArtikull = value; }
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
        /// Kthen/Vendos sasia e artikullit.
        /// </summary>
        public double Sasia
        {
            get { return sasia; }
            set { sasia = value; }
        }

        /// <summary>
        /// Kthen/Vendos cmimi i artikullit.
        /// </summary>
        public double Cmimi
        {
            get { return cmimi; }
            set { cmimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos vlefta e artikullit.
        /// </summary>
        public double Vlefta
        {
            get { return vlefta; }
            set { vlefta = value; }
        }

        /// <summary>
        /// Kthen/Vendos koeficienti midis dy njesive te artikullit.
        /// </summary>
        public double Koeficenti
        {
            get { return koeficenti; }
            set { koeficenti = value; }
        }

        /// <summary>
        /// Kthen/Vendos shenja e veprimit.
        /// <example> 1 kur kemi hyrje , -1 kur kemi dalje</example>
        /// </summary>
        public int Shenja
        {
            get { return shenja; }
            set { shenja = value; }
        }

        /// <summary>
        /// Kthen/Vendos sasia progresive e artikullit.
        /// </summary>
        public double SasiProgresive
        {
            get { return sasiaProgresive; }
            set { sasiaProgresive = value; }
        }

        /// <summary>
        /// Kthen/Vendos vlefta progresive e artikullit.
        /// </summary>
        public double VleftaProgresive
        {
            get { return vleftaProgresive; }
            set { vleftaProgresive = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e magazines.
        /// </summary>
        public int IdMag
        {
            get { return idMag; }
            set { idMag = value; }
        }

        /// <summary>
        /// Kthen/Vendos data e dokumentit.
        /// </summary>
        public DateTime Data
        {
            get { return data; }
            set { data = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e gjendjes se dokumentit.
        /// <example>ruajtur, draft etj</example>
        /// </summary>
        public int IdStatusDok
        {
            get { return idStatusDok; }
            set { idStatusDok = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e renditjes se dokumentit te nje date.
        /// </summary>
        public int IdRenditjes
        {
            get { return idRenditjes; }
            set { idRenditjes = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e detajimit te artikullit.
        /// </summary>
        public int IdDetajimi
        {
            get { return idDetajimi; }
            set { idDetajimi = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e detajimit te dyte te artikullit.
        /// </summary>
        public int IdDetajimi2
        {
            get
            {
                return idDetajimi2;
            }
            set
            {
                idDetajimi2 = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos sasia progresive e detajimit te artikullit.
        /// </summary>
        public double SasiProgresiveDetajimi
        {
            get { return sasiProgresiveDetajimi; }
            set { sasiProgresiveDetajimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos vlefta progresive e detajimit te artikullit.
        /// </summary>
        public double VlefteProgresiveDetajimi
        {
            get { return vlefteProgresiveDetajimi; }
            set { vlefteProgresiveDetajimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos pershkrimi i artikullit.
        /// </summary>
        public string KodDetajimi1
        {
            get { return kodDetajimi1; }
            set { kodDetajimi1 = value; }
        }

        /// <summary>
        /// Kthen/Vendos pershkrimi i artikullit.
        /// </summary>
        public string KodDetajimi2
        {
            get { return kodDetajimi2; }
            set { kodDetajimi2 = value; }
        }

        /// <summary>
        /// Kthen/Vendos shenimet.
        /// </summary>
        public String Shenime
        {
            get { return shenime; }
            set { shenime = value; }
        }

       
        public int IdBarkodi
        {
            get { return idBarkodi; }
            set { idBarkodi = value; }
        }

        /// <summary>
        /// permban id e vjeter qe ka trupimagazines
        /// </summary>
        public int IdTrupiMagazinaOld { get { return idTrupiMagazinaOld; } }

        public colSerialeUnikeMagazina OColSerialeUnikeMagazina
        {
            get { return oColSerialeUnikeMagazina; }
            set { oColSerialeUnikeMagazina = value; }
        }

        public int IdArtikullSet
        {
            get
            {
                return idArtikullSet;
            }

            set
            {
                idArtikullSet = value;
            }
        }
        #endregion

        #region Metoda Publike

        public clsMesazh krijoTrupMagazineNgaImporti(string kodi, string njesia, double sasia, double cmimi, double vlefta, string magazina, bool hyrje_dalje, string kodDetajimi1, string kodDetajimi2, int idNdermarrje, int idPerdorues, string magazina2, bool transferim, DateTime data, int shenja, string seriali, int indeksiTrupit, DbShare.clsKonfigurimAmbjenti konfigAmbjente, double kursi, int idStatusDok, ref DbCore.DbAsete.colSerialetMagazine colSerialet, int idTrupiKonvUd, string shenime, string artikullset, string kodbari, bool lejoCmimZero)
        {
            this.idLlojVeprimi = 1;
            this.shenja = shenja;
            int idkodbari = 0;
            clsMesazh mesazh = new clsMesazh();
            clsArtikulli art = new clsArtikulli();
            if (String.IsNullOrEmpty(kodi) && String.IsNullOrEmpty(kodbari))
                return new clsMesazh(false, "Vendosni kodin ose kodbarin e artikullit!");

            if (!String.IsNullOrEmpty(kodi))// kodi
            {
                mesazh = KontrolloArtikullPerImport(art, kodi, idNdermarrje, idPerdorues);
                if (!mesazh.Status)
                    return mesazh;
                if (!String.IsNullOrEmpty(kodbari))//kodi dhe kodbari
                {
                    idkodbari = clsKodbari.MerrIdBarkodiSipasPershkrimDheArtikulli(kodbari, art.IdArtikulli);
                    if (idkodbari <= 0)
                        return new clsMesazh(false, String.Format("Barkodi {0} nuk ekziston!", kodbari));
                }
            }
            else //vetem barkodi
            {
                //merr kodin e artikullit sipas barkodit.
                idkodbari = clsKodbari.MerrIdBarkodiSipasPershkrimit(kodbari);
                if (idkodbari <= 0)
                    return new clsMesazh(false, String.Format("Barkodi {0} nuk ekziston!", kodbari));
                art.merrSipasKodbarit(kodbari, idNdermarrje);
                if (art.IdArtikulli < 1)
                    return new clsMesazh(false, $"Nuk ekziston artikull me kodbarin {kodbari}!");
            }
        
            this.idArtikull = art.IdArtikulli;
            this.kodArtikull = art.KodArtikulli;
            this.pershkrimArtikull = art.PershkrimArtikulli;
            this.idTrupiRezervimi = 0;
            this.idTrupiKonvertimFSH = 0;
            this.idTrupiKonvertimUD = 0;
            this.idTrupiKonvertimUSH = 0;
            this.idKthimi = 0;
            this.idTrupiShitjeGjenerimi = 0;
            this.element = art;
            this.idBarkodi = clsAlternativaKushti.getAlternativa(konfigAmbjente.IdKonfigAmbjente, "RBART") == "Po" ? idkodbari : 0;
            

            if (!string.IsNullOrEmpty(artikullset))
            {
                var artSet = new clsArtikulli();
                mesazh = KontrolloArtikullPerImport(artSet, artikullset, idNdermarrje, idPerdorues);
                if (!mesazh)
                    return mesazh;
                this.idArtikullSet = artSet.IdArtikulli;

            }
            else
            {
                this.IdArtikullSet = 0;
            }
            
            if (njesia == "")
                return new clsMesazh(false, "Njesia e artikullit nuk duhet te jete bosh!");
            if (!clsNjesiArtikulli.ekzistonNjesiArtikulliMeKod(njesia, idNdermarrje))
                return new clsMesazh(false, "Njesia e artikullit me kod " + njesia + " nuk ekziston!");

            clsNjesiArtikulli njesi = new clsNjesiArtikulli();
            njesi.mbushNjesiArtikulliMeKod(njesia, idNdermarrje);
            if (art.Njesi1Artikulli != njesi.IdNjesia && art.Njesi2Artikulli != njesi.IdNjesia)
                return new clsMesazh(false, "Artikulli " + art.KodArtikulli + " nuk eshte i lidhur me njesine " + njesi.KodNjesia + "!");
            this.idNjesia = njesi.IdNjesia;
            if (String.IsNullOrEmpty(kodi) && !String.IsNullOrEmpty(kodbari))
            {
                // Nqs ka vendosur barkod dhe jo kodin e artikullit, kontrollo nese njesia e vendosur perket me njesine e barkodit.
                int njesiKobari = clsKodbari.MerrNjesiKodbari(art.IdArtikulli, kodbari); 
                if (art.Njesi1Artikulli != art.Njesi2Artikulli && ((art.Njesi1Artikulli == idNjesia && njesiKobari != 1) || (art.Njesi2Artikulli == idNjesia && njesiKobari != 2)))
                    return new clsMesazh(false, $"Barkodi {kodbari} nuk perket me njesine matese te zgjedhur {njesi.KodNjesia} !");
            }
            if (art.Njesi1Artikulli == idNjesia)//nese njesia e zgjedhuer eshte njesia 1 e artikullit ath koeficienti vendoset 1
                this.koeficenti = 1;
            else
                this.koeficenti = double.Parse(art.KoeficientArtikulli.ToString());//nese njesia e zgjedhur nuk eshte njesia e pare e artikullit ath koeficienti vendoset sa koeficienti i percaktuar tek artikulli
            if (sasia == 0)
                return new clsMesazh(false, "Nuk lejohet sasia 0!");
            if (Math.Round(cmimi, 10) != Math.Round((vlefta / sasia), 10))
                return new clsMesazh(false, "Vlerat nuk jane te sakta!");

            this.sasia = sasia;
            if (art.LlojiArt && art.MeSerial && sasia * koeficenti != Math.Truncate(sasia * koeficenti))
                return new clsMesazh(false, "Nuk lejohet sasi me presje dhjetore per artikullin " + KodiArtikull + " sepse eshte me serial!");

            this.cmimi = cmimi;
            if (!lejoCmimZero && cmimi == 0)
                return new clsMesazh(false, "Nuk lejohet cmim zero per kete lloj dokumenti!");

            this.vlefta = vlefta;

            if (!transferim && magazina == "")
                return new clsMesazh(false, "Magazina nuk duhet te jete bosh!");
            if (transferim && String.IsNullOrEmpty(magazina2))
                return new clsMesazh(false, "Magazina destinacion nuk duhet te jete bosh!");
            int idMagHyrje = -1;
            int idMagDestinacion = -1;
            if (!String.IsNullOrEmpty(magazina))
            {
                DbCore.DbRegjistrim.clsNjesiAdministrative njesiadm = new DbCore.DbRegjistrim.clsNjesiAdministrative(magazina, idNdermarrje);
                if (njesiadm.IdNjesiAdministrative <= 0)
                    return new clsMesazh(false, "Magazina me kod " + magazina + " nuk ekziston!");
                njesiadm = new clsNjesiAdministrative(magazina, idNdermarrje, idPerdorues);

                if (njesiadm.IdNjesiAdministrative < 1)
                    return new clsMesazh(false, "Nuk keni autorizime ne magazinen me kod " + magazina + "!");
                if (magazina != "" && !njesiadm.Aktiv)
                    return new clsMesazh(false, "Magazina me kod " + magazina + " nuk eshte aktive!");
                if (art.LlojiArt && njesiadm.IdLlojMagazine == 1)
                    return new clsMesazh(false, "Nuk lejohet te behet hyrje/dalje per artikuj afatgjate ne magazine per artikuj qarkullues!");
                idMagHyrje = njesiadm.IdNjesiAdministrative;
            }

            if (!String.IsNullOrEmpty(magazina2))
            {
                DbCore.DbRegjistrim.clsNjesiAdministrative njesiadm = new DbCore.DbRegjistrim.clsNjesiAdministrative(magazina2, idNdermarrje);
                if (njesiadm.IdNjesiAdministrative <= 0)
                    return new clsMesazh(false, "Magazina destinacion me kod " + magazina2 + " nuk ekziston!");
                njesiadm = new clsNjesiAdministrative(magazina2, idNdermarrje, idPerdorues);

                if (njesiadm.IdNjesiAdministrative < 1)
                    return new clsMesazh(false, "Nuk keni autorizime ne magazinen me kod " + magazina2 + "!");
                if (magazina2 != "" && !njesiadm.Aktiv)
                    return new clsMesazh(false, "Magazina me kod " + magazina2 + " nuk eshte aktive!");
                if (idMagHyrje != -1 && njesiadm.IdNjesiAdministrative == idMagHyrje)
                    return new clsMesazh(false, "Magazina nuk duhet te jete e njejte me magazinen destinacion!");
                idMagDestinacion = njesiadm.IdNjesiAdministrative;
            }

            if (transferim)
                this.idMag = idMagDestinacion;
            else
                this.idMag = idMagHyrje;
            
            if (kodDetajimi1 != "")
            {
                clsDetajimArtikulli detajimiPare = clsDetajimArtikulli.MerrDetajimOseCelDheLidhNeseNukEkziston(kodDetajimi1, idNdermarrje, idPerdorues, art, 1);
                this.idDetajimi = detajimiPare.IdDetajimArtikulli;
                this.kodDetajimi1 = detajimiPare.KodDetajimArtikulli;
            }
            else
            {
                this.idDetajimi = 0;
                this.kodDetajimi1 = "";
            }
            if (kodDetajimi2 != "")
            {
                clsDetajimArtikulli detajimiDyte = clsDetajimArtikulli.MerrDetajimOseCelDheLidhNeseNukEkziston(kodDetajimi2, idNdermarrje, idPerdorues, art, 2);
                this.idDetajimi2 = detajimiDyte.IdDetajimArtikulli;
                this.kodDetajimi2 = detajimiDyte.KodDetajimArtikulli;
            }
            else
            {
                this.idDetajimi2 = 0;
                this.kodDetajimi2 = "";
            }

            if (idTrupiKonvUd > 0 && !clsTrupiMagazina.ekzistonTrupiMagazina(idTrupiKonvUd))
                return new clsMesazh(false, "Rreshti magazines nuk ekziston!");
            this.idTrupiKonvertimUD = idTrupiKonvUd;

            if (!String.IsNullOrEmpty(seriali))
            {
                mesazh = RuajSerialeArtikulli(ref colSerialet, seriali, idNdermarrje, idPerdorues, art, hyrje_dalje, transferim, indeksiTrupit, konfigAmbjente);
                if (!mesazh)
                    return mesazh;
            }
            this.data = data;
            this.shenime = shenime;
            return new clsMesazh(true, "Trupi i magazines u krijua me sukses!");
        }

        private clsMesazh RuajSerialeArtikulli(ref colSerialetMagazine colSerialet, string seriali, int idNdermarrje, int idPerdorues, clsArtikulli art, bool hyrje_dalje, bool transferim, int indeksiTrupit, clsKonfigurimAmbjenti konfigAmbjente)
        {
            string[] serialet = seriali.Split(',');
            for (int ind = 0; ind < serialet.Length; ind++)
            {
                if (colSerialet.Find(x => x.KodSerialAqt == serialet[ind]) != null)
                    return new MesazhGabimi($"Seriali {serialet[ind]} ekziston njehere per kete dokument!");

                DbCore.DbAsete.clsAQTSeriale serialRi = new DbCore.DbAsete.clsAQTSeriale();
                if (!DbAsete.clsAQTSeriale.kontrolloEkzistonAQTSerial(serialet[ind], idNdermarrje))
                {
                    if (!hyrje_dalje)
                        return new clsMesazh(false, "Seriali " + serialet[ind] + " nuk ekziston!");

                    serialRi.AqtSerialKod = serialet[ind];
                    serialRi.AqtSerialPershkrim = serialet[ind];
                    serialRi.IdPerdoruesi = idPerdorues;
                    serialRi.IdNdermarrje = idNdermarrje;
                    serialRi.IdStatusDokumenti = 1;
                    serialRi.IdKrijuesi = idPerdorues;
                    serialRi.IdAQTArt = art.IdArtikulli;
                    serialRi.MeSerialPerCope = !art.MeSerial;
                    serialRi.IdNjesiAdministrativeAktuale = 0;
                    serialRi.IdHistorikAktualPaSerial = 0;
                    serialRi.HistorikSeriali = new clsHistorikAQTSeriale();

                    if (serialRi.MeSerialPerCope)
                        serialRi.HistorikSeriali = new clsHistorikAQTSeriale(serialRi.IdAQTSerial, String.Empty, 0, 0, 0, 1, 0, 0, 1, serialRi.IdNdermarrje, serialRi.IdPerdoruesi, serialRi.IdPerdoruesi, serialRi.DtKrijimi, serialRi.DtModifikimi);
                    clsMesazh mesazh = serialRi.ruaj();
                    if (!mesazh.Status)
                        return new clsMesazh(false, "Ndodhi nje gabim gjate celjes se serialit " + serialet[ind] + "!");
                }
                else
                {
                    serialRi.merrAQTSerialSipasKodAQT(serialet[ind], idNdermarrje);
                    if (serialRi.IdAQTArt != idArtikull)
                        return new clsMesazh(false, "Seriali " + serialRi.AqtSerialKod + " nuk i perket ketij artikulli! Ju lutem zgjidhni nje serial tjeter!");

                    if (!transferim)
                    {

                        if (hyrje_dalje)
                        {
                            if (serialRi.IdNjesiAdministrativeAktuale != 0)
                                return new clsMesazh(false, "Seriali " + serialRi.AqtSerialKod + " eshte perdorur ne veprime! Ju lutem zgjidhni nje serial tjeter!");
                        }
                        else
                        {
                            if (serialRi.IdNjesiAdministrativeAktuale != idMag)
                                return new clsMesazh(false, "Seriali " + serialRi.AqtSerialKod + " nuk i perket kesaj magazine! Ju lutem zgjidhni nje serial tjeter!");
                        }
                    }
                }
                clsSerialetMagazine serialmag = new DbCore.DbAsete.clsSerialetMagazine(0, 0, indeksiTrupit, art.IdArtikulli, serialRi.IdAQTSerial, serialRi.AqtSerialKod, konfigAmbjente.IdNivel, konfigAmbjente.IdKonfigAmbjente, idMag, art.MeSerial ? 1 : (double)(sasia * koeficenti), double.Parse((cmimi).ToString()), double.Parse((cmimi).ToString()) * (art.MeSerial ? 1 : (double)(sasia * koeficenti)), idStatusDok, idNdermarrje, idPerdorues, idPerdorues, 0);
                colSerialet.Add(serialmag);
            }
            return new MesazhSuksesi();
        }

        public clsMesazh KontrolloArtikullPerImport(clsArtikulli art, string kodi, int idNdermarrje, int idPerdorues)
        {
            if (!clsArtikulli.ekziston(kodi, idNdermarrje))
            {
                return new clsMesazh(false, "Artikulli me kod " + kodi + " nuk ekziston!");
            }
            art.ktheArtikullSipasKoditDheAutorizime(kodi, idNdermarrje, idPerdorues);
            if (art.IdArtikulli == 0)
            {
                return new clsMesazh(false, "Nuk keni autorizime per artikullin " + kodi + "!");
            }
            if (!art.Aktiv)
            {
                return new clsMesazh(false, "Artikulli me kod:" + art.KodArtikulli + " nuk eshte aktiv!");
            }
            if (art.Klasa == 3)
                return new clsMesazh(false, "Artikulli me kod: " + art.KodArtikulli+" i perket klases Sherbim dhe dokumenti nuk mund te importohet!");
            if (art.Klasa == 2)
                return new clsMesazh(false, "Artikulli me kod: " + art.KodArtikulli + " i perket klases Pakostueshem dhe dokumenti nuk mund te importohet!");

            return new MesazhSuksesi();
        }

        public void MerrSerialetUnike(colSerialeUnikeMagazina serialetUnike, bool dalje, int idTvsh = 0)
        {
            ImbLogger.LogTraceShitje("Filloi metoda MerrSerialeUnike!");
            if (serialetUnike == null)
                return;

            colSerialeUnikeMagazina colserialeunike;
            if (dalje)
                colserialeunike = new colSerialeUnikeMagazina(serialetUnike.FindAll(x => x.IdArtikulli == this.IdArtikulli && x.IdSeti == this.IdArtikullSet && (x.IdMag == IdMag || x.IdMag == 0)));
            else
                colserialeunike = new colSerialeUnikeMagazina(serialetUnike.FindAll(x => x.IdArtikulli == this.IdArtikulli && x.IdSeti == this.IdArtikullSet));

            colserialeunike.ForEach(x => x.IdTVSH = idTvsh);
            this.OColSerialeUnikeMagazina = colserialeunike;

            ImbLogger.LogTraceShitje("Mbaroi metoda MerrSerialeUnike!");
        }

        public bool kaVeprimePas()
        {
            clsDatabaseRegjistrim dat = new clsDatabaseRegjistrim();
            bool kaVeprime = dat.kaVeprimePas(idArtikull, idMag, data, idRenditjes, idKokaMagazina);
            dat.Dispose();
            return kaVeprime;
        }

        public bool kaVeprimePas(clsDatabaseRegjistrim dat)
        {
            return dat.kaVeprimePas(idArtikull, idMag, data, idRenditjes, idKokaMagazina);
        }

        public static bool kaVeprimeMagPerArtikull(int idArt, int idNderm)
        {
            clsDatabaseRegjistrim dat = new clsDatabaseRegjistrim();
            bool kaVeprime = dat.kaVeprimeMagArtikulli(idArt, idNderm);
            dat.Dispose();
            return kaVeprime;
        }

        public static colTrupiMagazina mbushReshtaPerFifo(int idArt, int iddetajimi, int idmag, DateTime dt, int idRenditjes, bool merrSipasDetajimit, clsDatabaseRegjistrim dbRegj, bool fifo2)
        {
            colTrupiMagazina trupat = new colTrupiMagazina();
            trupat.mbushReshtaPerFifoArtikulliDetajim(idArt, merrSipasDetajimit ? iddetajimi : 0, idmag, dt, idRenditjes, dbRegj, fifo2);
            return trupat;
        }

        /// <summary>
        /// ky funksion perdoret per te llogaritur koston e daljes te nje artikulli, per nje magazine te caktuar ne nje date te caktuar.
        /// Fifo(first in first out) eshte metoda ne te cilen artikujt dalin nga magazine sipas radhes qe kane hyre.
        /// nqs per kete artikull nuk kemi gjendje dhe po marrim cmimin fifo per nje rresht trupi magazine te ardhur nga shperndarja e shpenzimeve 
        /// pra, sasia = 0 dalim nga funksioni, sepse nuk kemi cfare te llogarisim.
        /// Ne fillim marrim gjithe rreshtat e hyrjeve pozitive dhe daljet negative te bera per kete artikull, ne kete magazine, perpara kesaj 
        /// date, ose te kesaj date me idrenditje me te vogel (ose per fifo 2 hyrjet para dalje brenda dates pavaresisht idrenditje) te renditura 
        /// nga veprimi me i vonshem tek veprimi me i hershem
        /// I bredhim rreshtat nje nga nje dhe mbledhim sasite e tyre derisa arrijme gjendjen. Kjo behet per te gjetur rreshtin nga i cili duhet 
        /// te fillohet llogaritja e fifos. Nqs nuk gjendjet rreshti kthejme cmimin 1 dhe dalim nga funksioni.
        /// Gjejme sasine e mbetur per kete rresht sipas formules sasi e rreshtit - diferenca e gjendje aktuale dhe totalit te sasise se rreshtave 
        /// te llogaritur me pare.
        /// Nqs sasia e mbetur e ketij rreshti eshte me e madhe ose e barabarte se sasia per te cilen po llogarisim koston atehere e gjithe sasia 
        /// qe na duhet merret nga ky rresht, por cmimit te saj duhet t'i shtojme te gjitha shperndarjet e shpenzimeve eksituese per kete rresht
        /// (pra, rreshtat me cmim e sasi 0). Per kete bredhim rreshtat duke filluar nga rreshti para ketij rreshti derisa arrijme nje rresht
        /// qe e ka cmim te ndryshem nga 0 dhe vleften e shperndarjes ia shtojme vleftes te rreshtit te gjetur. Ne fund shumen e ketyre 
        /// vlerave e pjesetojme me sasine e ketij rreshti dhe nxjerrim cmimin.
        /// Nqs sasia e ketij rreshti eshte me e vogel se sasia e kerkuar atehere vleftes totale i shtojme vleften e ketij rreshti duke e pjestuar 
        /// me sasine e tij dhe duke e shumezuar me pjesen e mbetur. Nqs ky rresht eshte shperndarje shpenzimi pra nuk kemi sasi, gjejme sasine e 
        /// rreshtit te fh per te cilen eshte bere kjo shperndarje dhe veprojme po njesoj.
        /// Me pas gjejme pjesen qe mbetet nga sasia (nga sasia qe i vjen si parameter funksionit) qe nuk e ploteson ky rresht. Per kete sasi 
        /// fillojme bredhim rreshtat duke filluar nga rreshti 
        /// para ketij dhe veprojme njesoj si me lart. Nqs sasia e ketij rreshti te ri eshte me e madhe se sasia e mbetur, gjejme shperndarjet
        /// e tij dhe ia shtojme vleftes se rreshtit dhe me pas vleftes totale i shtojme vleften per pjesen e mbetur per kete rresht bashke me 
        /// shperndarjet. Me pas dalim nga cikli. Perndryshe i shtojme vleftes totale gjithe vleften e ketij rreshti, dhe nqs rreshti
        /// eshte shperndarje shpenzimi gjejme rreshtin e fh nga e cila ka ardhur. Vazhdojme ciklin derisa sasia e mbetur behet 0 
        /// ose mbarojne rreshtat.
        /// Nqs mbarojne rreshtat atehere per sasine e mbetur marrim cmimin e hyrjes me te fundit dhe e shumezojme me pjesen e mbetur.
        /// Ne fund vleften totale e pjestojme me sasine e kerkuar dhe nxjerrim cmimin.
        /// </summary>
        /// <param name="idArt">id e artikullit qe do i llogaritet cmimi fifo</param>
        /// <param name="iddetajimi"> id e detajimit te pare per te cilin do llogaritet cmimi fifo</param>
        /// <param name="idmag">id e magazines ne te cilen ndodhet artikulli</param>
        /// <param name="dt"> data per te cilen do llogaritet cmimi</param>
        /// <param name="idrenditjes">id e renditjes te rreshtit te artikullit ne databaze</param>
        /// <param name="sas"> sasia per te cilen do llogaritet cmimi fifo</param>
        /// <param name="cm"> parameter output qe kthen cmimin fifo</param>
        /// <param name="koefic"> koeficienti me te cilen eshte ruajtur artikulli nqs artikulli eshte ne njesine e pare koeficienti eshte 1 nqs 
        /// artikulli eshte ne njesine e dyte koeficienti eshte sa koeficienti i artikullit. perdoret per te mare sasine e ketij rreshti pra
        /// sasi * koeficient</param>
        /// <param name="gjendje"> gjendja e artikullit ne kete magazine ne kete moment</param>
        /// <param name="merrSipasDetajimit">tregon nqs do marrim cmimin mesatar per detajimin apo per artikullin</param>
        /// <param name="dbRegj"> clsDatabazeRegjistrimi qe perdoret per transaksionet</param>
        /// <param name="fifo2"> tregon nese do marrim cmimin mesatar sipas fifo 1 apo sipas fifo 2</param>
        public static double llogaritCmimMesatarFifo(int idArt, int iddetajimi, int idmag, DateTime dt, int idrenditjes, double sas, double koefic, double gjendje, bool merrSipasDetajimit, clsDatabaseRegjistrim dbRegj, bool fifo2, double cmimiartnjejte)
        {
            double totalsasi = 0;
            int idreshti = -1;
            double vleftatotale = 0;
         
            if (sas == 0 && gjendje == 0)            
                return 1;
            
            if (gjendje == 0 && cmimiartnjejte != 0)
                return cmimiartnjejte;
            colTrupiMagazina trupat = clsTrupiMagazina.mbushReshtaPerFifo(idArt, iddetajimi, idmag, dt, idrenditjes, merrSipasDetajimit, dbRegj, fifo2);
            for (int i = 0, nrTrupa = trupat.Count; i < nrTrupa; i++)
            {
                totalsasi += trupat[i].Sasia * trupat[i].Koeficenti;
                totalsasi = Math.Round(totalsasi, 10);
                if (totalsasi >= gjendje)
                {
                    idreshti = i;
                    break;
                }
            }

            double sasidaljembetur = 0;
            if (idreshti == -1)
                return 1;
            double sasimbeturrreshti = trupat[idreshti].Sasia * trupat[idreshti].Koeficenti - totalsasi + gjendje;
            if (sasimbeturrreshti >= sas * koefic)
            {
                for (int k = idreshti - 1; k >= 0; k--)//shperndarje shpenzimesh
                {
                    if (trupat[k].cmimi == 0)
                        trupat[idreshti].vlefta += trupat[k].Vlefta;// /(trupat[idreshti].Sasia*trupat[idreshti].koeficenti);
                    else
                        break;
                }
                return trupat[idreshti].Vlefta / (trupat[idreshti].Sasia * trupat[idreshti].koeficenti);
            }
            if (trupat[idreshti].Sasia != 0)
                vleftatotale += (sasimbeturrreshti) * trupat[idreshti].Vlefta / (trupat[idreshti].Sasia * trupat[idreshti].koeficenti);
            else
            {
                clsTrupiMagazina trupiGjenerues = clsTrupiMagazina.ktheRreshtMagazineGjeneruesFHSS(trupat[idreshti].idTrupiMagazina, dbRegj);//per shperndarjet e shpenzimit
                double sasishper = (trupiGjenerues.sasia * trupiGjenerues.koeficenti);
                if (trupiGjenerues.idTrupiMagazina == 0)//per fhnc
                    sasishper = dbRegj.ktheSasineTotaleSipasArtikullitHPD(idArt, idmag, trupat[idreshti].data, trupat[idreshti].idRenditjes);
                if (sasishper != 0)
                    vleftatotale += trupat[idreshti].Vlefta * sasimbeturrreshti / sasishper;
            }
            sasidaljembetur = sas * koefic - sasimbeturrreshti;
            for (int i = idreshti - 1; i >= 0; i--)
            {
                if (trupat[i].Sasia * trupat[i].Koeficenti >= sasidaljembetur)
                {
                    for (int k = i - 1; k >= 0; k--)
                    {
                        if (trupat[k].cmimi == 0)
                            trupat[i].vlefta += trupat[k].Vlefta;// / (trupat[i].Sasia*trupat[i].Koeficenti);
                        else
                            break;
                    }
                    vleftatotale += sasidaljembetur * trupat[i].Vlefta / (trupat[i].Sasia * trupat[i].Koeficenti);
                    sasidaljembetur = 0;
                    break;
                }
                else
                {
                    if (trupat[i].sasia == 0)
                    {
                        clsTrupiMagazina trupiGjenerues = clsTrupiMagazina.ktheRreshtMagazineGjeneruesFHSS(trupat[i].idTrupiMagazina, dbRegj);//per shperndarjet e shpenzimit
                        double sasishper = (trupiGjenerues.sasia * trupiGjenerues.koeficenti);
                        if (trupiGjenerues.idTrupiMagazina == 0)//per fhnc
                            sasishper = dbRegj.ktheSasineTotaleSipasArtikullitHPD(idArt, idmag, trupat[i].data, trupat[i].idRenditjes);
                        vleftatotale += trupat[i].Vlefta * sasimbeturrreshti / sasishper;
                    }
                    else
                    {
                        vleftatotale += trupat[i].Vlefta;
                        sasimbeturrreshti = trupat[i].Sasia;

                    }
                    sasidaljembetur -= trupat[i].Sasia * trupat[i].Koeficenti;
                }
            }
            double VleftaHyrjePerSS = 0;
            ktheVleftenPerSasineEMbetur(trupat, ref vleftatotale, ref sasidaljembetur, dbRegj, idArt, idmag, ref VleftaHyrjePerSS, gjendje, 0);
            //if (sasidaljembetur != 0)
            //{
            //    if (trupat[0].Sasia != 0)
            //        vleftatotale += sasidaljembetur * trupat[0].Vlefta / (trupat[0].Sasia * trupat[0].Koeficenti);
            //    else
            //    {
            //        clsTrupiMagazina trupiGjenerues = clsTrupiMagazina.ktheRreshtMagazineGjeneruesFHSS(trupat[0].idTrupiMagazina, dbRegj);//per shperndarjet e shpenzimit
            //        double sasishper = (trupiGjenerues.sasia * trupiGjenerues.koeficenti);
            //        if (trupiGjenerues.idTrupiMagazina == 0)
            //        {//per fhnc
            //            sasishper = dbRegj.ktheSasineTotaleSipasArtikullitHPD(idArt, idmag, trupat[0].data, trupat[0].idRenditjes);
            //            if (sasishper == 0)
            //            {
            //                ktheVleftenPerSasineEMbetur(trupat, ref vleftatotale, ref sasidaljembetur, dbRegj, idArt, idmag, ref VleftaHyrjePerSS, gjendje, 1);
            //            }
            //        }
            //        if (gjendje <= 0)
            //            if (sasishper == 0)
            //                VleftaHyrjePerSS = 0;
            //            else
            //                VleftaHyrjePerSS = trupiGjenerues.Vlefta / sasishper;
            //        if(sasishper!=0)
            //            vleftatotale += sasidaljembetur * trupat[0].Vlefta / sasishper;
            //    }
            //}
            return vleftatotale / (sas * koefic)+ VleftaHyrjePerSS;
        }

        public static void ktheVleftenPerSasineEMbetur(colTrupiMagazina trupat, ref double vleftatotale, ref double sasidaljembetur, clsDatabaseRegjistrim dbRegj, int idArt, int idmag, ref double VleftaHyrjePerSS, double gjendje, int indexPerLlogaritje)
        {
            if (trupat[indexPerLlogaritje].Sasia != 0)
                vleftatotale += sasidaljembetur * trupat[indexPerLlogaritje].Vlefta / (trupat[indexPerLlogaritje].Sasia * trupat[indexPerLlogaritje].Koeficenti);
            else
            {
                clsTrupiMagazina trupiGjenerues = clsTrupiMagazina.ktheRreshtMagazineGjeneruesFHSS(trupat[indexPerLlogaritje].idTrupiMagazina, dbRegj);//per shperndarjet e shpenzimit
                double sasishper = (trupiGjenerues.sasia * trupiGjenerues.koeficenti);
                if (trupiGjenerues.idTrupiMagazina == 0)
                {//per fhnc
                    sasishper = dbRegj.ktheSasineTotaleSipasArtikullitHPD(idArt, idmag, trupat[indexPerLlogaritje].data, trupat[indexPerLlogaritje].idRenditjes);
                    if (sasishper == 0)
                    {
                        ktheVleftenPerSasineEMbetur(trupat, ref vleftatotale, ref sasidaljembetur, dbRegj, idArt, idmag, ref VleftaHyrjePerSS, gjendje, indexPerLlogaritje + 1);
                        return;
                    }
                }
                if (gjendje <= 0)
                    if (sasishper == 0)
                        VleftaHyrjePerSS = 0;
                    else
                        VleftaHyrjePerSS = trupiGjenerues.Vlefta / sasishper;
                if (sasishper != 0)
                    vleftatotale += sasidaljembetur * trupat[indexPerLlogaritje].Vlefta / sasishper;
            }
        }

        /// <summary>
        /// kthen rreshtin e fh qe eshte lidhur rreshtin e  fhss
        /// </summary>
        /// <param name="idArt">id e artikullit</param>
        /// <param name="iddetajimi">id e detajimit</param>
        /// <param name="idmag">id e magazines</param>
        /// <param name="dt">data</param>
        /// <param name="idrenditjes">id e renditjes</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        public static clsTrupiMagazina ktheRreshtMagazineGjeneruesFHSS(int idTrupiMag, clsDatabaseRegjistrim dbRegj)
        {
            clsTrupiMagazina trupiGjenerues = new clsTrupiMagazina();
            trupiGjenerues.mbushTrupMagazine(dbRegj.ktheRreshtinGjeneruesTeTrupitFHSS(idTrupiMag));
            return trupiGjenerues;
        }

        public static clsTrupiMagazina ktheTrupiMagazinaSipasReceptureProdhimi(int idDok, int idArtikull, int idMag)
        {
            using (clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim())
            {
                clsTrupiMagazina trupiMag = new clsTrupiMagazina();
                trupiMag.mbushTrupMagazine(dbRegj.ktheTrupiMagazinaSipasReceptureProdhimi(idDok, idArtikull, idMag));
                return trupiMag;
            }
        }

        /// <summary>
        /// perdoret per te bere rivleresimin sipas fifos
        /// </summary>
        /// <param name="kontrollCmimPerDetajim"></param>
        /// <param name="idartikulli">id e artikullit</param>
        /// <param name="idmagazina">id e magazines</param>
        /// <param name="datanga">data nga</param>
        /// <param name="dbRegj">kur eshte me transaksion</param>
        /// <returns></returns>
        /// <param name="maxTs"></param>
        /// <param name="fileLogPath"></param>
        public static clsMesazh bejRivleresim(bool kontrollCmimPerDetajim, int idartikulli, int idmagazina, int idmetodekostoje, DateTime datanga, DbAdmin.clsLogRivleresimInventari log, CultureInfo ci, ResourceManager rm,  int idndermarje, int idperdorues)
        {
            DbData dbData = new DbData();
            int maxRetry = 500;
            //int i = 0;
            //int fillim = 0;
            try
            {
                clsMesazh mesazh;
                colTrupiMagazina trupat = new colTrupiMagazina();
                DbCore.DbQendraKosto.colKokaQendraKosto kokaQendra;
                //System.Diagnostics.Stopwatch myWatchMulti = new System.Diagnostics.Stopwatch();
                //myWatchMulti.Start();
                //System.Threading.Tasks.Parallel.Invoke(() => kokaQendra = new DbCore.DbQendraKosto.colKokaQendraKosto(idartikulli, idmagazina, datanga, datanga, maxRetry, log), ()=> trupat.mbushReshtaPerRivleresimFifoArtikulli(idartikulli, idmagazina, datanga, datanga, maxRetry, log));
                //myWatchMulti.Stop();
                //System.Diagnostics.Trace.WriteLine("myWatchMulti: " + myWatchMulti.Elapsed);
                System.Diagnostics.Stopwatch myWatchSingel = new System.Diagnostics.Stopwatch();
                myWatchSingel.Start();
                kokaQendra = new DbCore.DbQendraKosto.colKokaQendraKosto(idartikulli, idmagazina, datanga, datanga, maxRetry, log);
                trupat.mbushReshtaPerRivleresimFifoArtikulli(idartikulli, idmagazina, datanga, datanga, maxRetry, log);
                myWatchSingel.Stop();
                System.Diagnostics.Trace.WriteLine("myWatchSingel: " + myWatchSingel.Elapsed);
                if (trupat.Count == 0 && kokaQendra.Count == 0)
                    return new clsMesazh(true, "Rivleresimi i keti artikulli u be me sukses");
                //trupat = merrReshtaPerRivleresimFifoArtikulli(idartikulli, idmagazina, datanga, dataderi);
                IEnumerable<IGrouping<int, clsTrupiMagazina>> kokat = trupat.GroupBy(trupi => trupi.IdKokaMagazina);
                for (int i = 0, count = kokat.Count(); i < count; i++)//hiqet thirrja e metodes count sepse te bredh elementet ne cdo iterim 
                {              
                    clsRetryTrans retryTrans = new clsRetryTrans("Rivleresim");
                    do
                    {
                        try
                        {
                            clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim();
                            dbData = dbRegj;
                            clsDatabaseInventari dbInv = new clsDatabaseInventari(dbData);
                            IGrouping<int, clsTrupiMagazina> koke = kokat.ElementAt(i);
                            IEnumerable<clsTrupiMagazina> trupDoku = trupat.Where(trupi => trupi.idKokaMagazina == koke.Key);
                            dbData.beginTransaksion(0);
                            System.Diagnostics.Stopwatch myWatch = new System.Diagnostics.Stopwatch();
                            myWatch.Start();
                            foreach (clsTrupiMagazina t in trupDoku)
                            {
                                double cm = 0;
                                bool merrSipasDetajimit = t.IdDetajimi != 0 && kontrollCmimPerDetajim;
                                switch (idmetodekostoje)
                                {
                                    case 1:   //cmim mesatar hyrje para dalje                                
                                        if (t.IdDetajimi != -1 && merrSipasDetajimit)//nqs rreshti eshte me detajim marrim cmimin mesatar per ate detajim prn cmimin mesatar te artikullit
                                            cm = dbRegj.llogaritCmimMesatarMeTotalDetajimitHPD(t.IdArtikulli, t.IdDetajimi, t.IdMag, t.Data, t.IdRenditjes, 0, 0);
                                        else
                                            cm = dbRegj.llogaritCmimMesatarMeTotalHPD(t.IdArtikulli, t.IdMag, t.Data, t.IdRenditjes, 0, 0, t.idTrupiMagazina);
                                        break;
                                    case 2: //cmim mesatar sipas id se renditjes
                                    case 6:
                                        double cmimireferues = 0;
                                        if (idmetodekostoje == 6)
                                            cmimireferues = clsKokaMagazina.merrCmimReferues(t.Data, idndermarje, idperdorues, t.idArtikull, t.IdNjesia, dbInv);

                                        if (t.IdDetajimi != -1 && merrSipasDetajimit)//nqs rreshti eshte me detajim marrim cmimin mesatar per ate detajim ndryshe cmimin mesatar te artikullit
                                            cm = dbRegj.llogaritCmimMesatarMeTotalDetajimitSipasRadhes(t.IdArtikulli, t.IdDetajimi, t.IdMag, t.Data, t.IdRenditjes, 0, 0, cmimireferues);
                                        else
                                            cm = dbRegj.llogaritCmimMesatarMeTotalSipasRadhes(t.Cmimi, t.IdArtikulli, t.IdMag, t.Data, t.IdRenditjes, 0, 0, cmimireferues);
                                        break;

                                    case 3: // cmim mesatar sipas progresivit                                
                                        if (t.IdDetajimi != -1 && merrSipasDetajimit)//nqs rreshti eshte me detajim marrim cmimin mesatar per ate detajim prn cmimin mesatar te artikullit
                                            cm = dbRegj.llogaritCmimMesatarMeProgresivDetajimit(t.Cmimi, t.IdArtikulli, t.IdDetajimi, t.IdMag, t.Data, t.IdRenditjes, 0, 0);
                                        else
                                            cm = dbRegj.llogaritCmimMesatarMeProgresiv(t.Cmimi, t.IdArtikulli, t.IdMag, t.Data, t.IdRenditjes, 0, 0);
                                        break;
                                    case 4: //fifo
                                        int idDetajimi = merrSipasDetajimit ? t.IdDetajimi : 0;
                                        cm = llogaritCmimMesatarFifo(t.IdArtikulli, t.IdDetajimi, t.IdMag, t.Data, t.IdRenditjes, Math.Abs(t.Sasia), t.Koeficenti, dbRegj.ktheSasineProgresiveArtikullitEdheSipasDetajimit(t.IdArtikulli, idDetajimi, t.IdMag, t.Data, t.IdRenditjes), merrSipasDetajimit, dbRegj, false, 0);
                                        break;
                                    case 5: //fifo2
                                        if (merrSipasDetajimit)
                                            cm = llogaritCmimMesatarFifo(t.IdArtikulli, t.IdDetajimi, t.IdMag, t.Data, t.IdRenditjes, Math.Abs(t.Sasia), t.Koeficenti, dbRegj.ktheSasineTotaleSipasDetajimitHPD(t.IdArtikulli, t.IdDetajimi, t.IdMag, t.Data, t.IdRenditjes), true, dbRegj, true, 0);//- gjendjeArtikullDetajimNjejte                                            
                                        else
                                            cm = llogaritCmimMesatarFifo(t.IdArtikulli, t.IdDetajimi, t.IdMag, t.Data, t.IdRenditjes, Math.Abs(t.Sasia), t.Koeficenti, dbRegj.ktheSasineTotaleSipasArtikullitHPD(t.IdArtikulli, t.IdMag, t.Data, t.IdRenditjes), false, dbRegj, true, 0);//- gjendjeArtikulliTeNjejte
                                        break;
                                    default:
                                        new MyException("Metode kostoje e panjohur:" + idmetodekostoje.ToString());
                                        break;
                                }
                                mesazh = dbRegj.rivleresimCmimMesatarFIFO(idartikulli, t.idMag, t.IdTrupiMagazina, cm);
                                if (!mesazh.Status)
                                {
                                    dbData.rollbackTransaksion();
                                    return mesazh;
                                }
                            }
                            dbData.commitTransaksion();
                            if (kokaQendra.Count(x => x.IdKokaMagazina == koke.Key) == 0) //nese qk nuk ka ekzistuar, krijoe
                            {
                                mesazh = DbQendraKosto.clsKokaQendraKosto.KrijoDheRuajQKTeReja(koke.Key);
                                if (!mesazh.Status)
                                    return mesazh;
                            }
                            myWatch.Stop();
                            System.Diagnostics.Trace.WriteLine("myWatch: " + myWatch.Elapsed);
                            log.MaxTs = myWatch.Elapsed > log.MaxTs ? myWatch.Elapsed : log.MaxTs;
                            retryTrans.stopRetrying(); //dil se e bone
                        }
                        catch (System.Data.SqlClient.SqlException ex)
                        {
                            if(!retryTrans.checkRetry(ex, log, i))
                            {
                                dbData.rollbackTransaksion();
                                log.logError(ex.Number, ex.Message);
                                throw ex;
                            }                                                        
                        }
                        catch (Exception ex)
                        {
                            dbData.rollbackTransaksion();
                            log.logError(ex.Message);
                            throw ex;
                        }
                    } while (retryTrans.isRetrying());
                }
                for (int k = 0, nrDokQk = kokaQendra.Count; k < nrDokQk; k++)
                {
                    clsRetryTrans retryTrans = new clsRetryTrans("RivleresimQK");
                    do
                    {
                        
                        try
                        {
                            System.Diagnostics.Stopwatch myWatchQK = new System.Diagnostics.Stopwatch();
                            myWatchQK.Start();
                            DbCore.DbQendraKosto.clsDatabaseQendraKosto dbQK = new DbCore.DbQendraKosto.clsDatabaseQendraKosto();
                            dbData = dbQK;
                            dbData.beginTransaksion();
                            mesazh = kokaQendra[k].rillogarit(dbQK, 7);
                            if (!mesazh.Status)
                            {
                                dbData.rollbackTransaksion();
                                return mesazh;
                            }
                            dbData.commitTransaksion();
                            myWatchQK.Stop();
                            log.MaxTs = myWatchQK.Elapsed > log.MaxTs ? myWatchQK.Elapsed : log.MaxTs;
                            retryTrans.stopRetrying();
                        }
                        catch (System.Data.SqlClient.SqlException ex)
                        {
                            if (!retryTrans.checkRetry(ex,log,k))
                            {
                                //log.logError(ex.Number, ex.Message);
                                dbData.rollbackTransaksion();                                
                                throw ex;
                            }
                        }
                        catch (Exception ex)
                        {
                            //log.logError(ex.Message);
                            dbData.rollbackTransaksion();                            
                            throw ex;
                        }
                    } while (retryTrans.isRetrying());
                }
                return new clsMesazh(true, "Rivleresimi i keti artikulli u be me sukses");
            }
            catch (Exception ex)
            {
                log.logError(ex.Message);
                return new clsMesazh(false, "Rivleresimi nuk u krye per shkak te nje gabimi!");
            }
        }

        public colTrupiMagazina ShperndaTrupinSipasSerialeve(int idperdoruesi, int idndermarje, bool lejoModifikimDetajimi, int loan)
        {
            colTrupiMagazina col = new colTrupiMagazina();
            for (int i = 0; i < this.sasia; i++)
            {
                var newTrup = this.ShallowCopy();
                newTrup.Sasia = 1;
                newTrup.Vlefta = Cmimi;
                if(this.OColSerialeUnikeMagazina != null && this.OColSerialeUnikeMagazina.Count > i)
                {
                    var ser = this.OColSerialeUnikeMagazina[i];
                    var detajim = clsDetajimArtikulli.KrijoDetajimNeseNukEkziston(ser.SerialiKryesore, idperdoruesi, idndermarje, loan, (clsArtikulli)element);
                    if (!lejoModifikimDetajimi)
                    {
                        newTrup.idDetajimi = detajim.IdDetajimArtikulli;
                        newTrup.KodDetajimi1 = detajim.KodDetajimArtikulli;
                    }

                }

                col.Add(newTrup);
            }
            if(this.OColSerialeUnikeMagazina != null)
                this.OColSerialeUnikeMagazina.Clear();
            return col;
        }                

        public double llogaritCmimMesatar(clsArtikulli artikulli, int idmag, DateTime data, int iddetajim, double sasia, int idPerdorues, bool llogaritCmimReferuesSipasNjesiTeDyte = false, int idTrupiMagazina = -1, int idRenditjes = -1)
        {
            double cm = 0;
            using (clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim())
            {
                if (idmag <= 0)
                    idRenditjes = int.MaxValue;
                else
                    if (idRenditjes == -1)
                    idRenditjes = dbRegj.vendosIdRenditjesTrupi(idmag, data);

                switch (artikulli.MetodeKostojeArtikulli)
                {
                    case 1:
                        if (idmag > 0)
                        {
                            if (iddetajim == -1)
                                cm = dbRegj.llogaritCmimMesatarMeTotalHPD(artikulli.IdArtikulli, idmag, data, idRenditjes, 0, 0, idTrupiMagazina);
                            else
                                cm = dbRegj.llogaritCmimMesatarMeTotalDetajimitHPD(artikulli.IdArtikulli, iddetajim, idmag, data, idRenditjes, 0, 0);
                        }
                        else
                        {
                            if (iddetajim == -1)
                                cm = dbRegj.llogaritCmimMesatarMeTotalHPDGjitheMag(artikulli.IdArtikulli, data, idRenditjes);
                            else
                                cm = dbRegj.llogaritCmimMesatarMeTotalDetajimitHPDGjitheMag(artikulli.IdArtikulli, iddetajim, data, idRenditjes);
                        }
                        break;
                    case 2:
                    case 6:
                        double cmimireferues = artikulli.MetodeKostojeArtikulli == 6 ? clsKokaMagazina.merrCmimReferues(data, artikulli.IdNdermarje, idPerdorues, artikulli.IdArtikulli, artikulli.Njesi1Artikulli) : 0;
                        if (iddetajim == -1)
                            cm = dbRegj.llogaritCmimMesatarMeTotalSipasRadhes(cm, artikulli.IdArtikulli, idmag, data, idRenditjes, 0, 0, cmimireferues);
                        else
                            cm = dbRegj.llogaritCmimMesatarMeTotalDetajimitSipasRadhes(artikulli.IdArtikulli, iddetajim, idmag, data, idRenditjes, 0, 0, cmimireferues);
                        break;
                    case 3:
                        if (idmag > 0)
                        {
                            if (iddetajim == -1)
                                cm = dbRegj.llogaritCmimMesatarMeProgresiv(cm, artikulli.IdArtikulli, idmag, data, idRenditjes, 0, 0);
                            else
                                cm = dbRegj.llogaritCmimMesatarMeProgresivDetajimit(cm, artikulli.IdArtikulli, iddetajim, idmag, data, idRenditjes, 0, 0);
                        }
                        else
                        {
                            if (iddetajim == -1)
                                cm = dbRegj.llogaritCmimMesatarMeTotalHPDGjitheMag(artikulli.IdArtikulli, data, idRenditjes);
                            else
                                cm = dbRegj.llogaritCmimMesatarMeTotalDetajimitHPDGjitheMag(artikulli.IdArtikulli, iddetajim, data, idRenditjes);
                        }
                        break;
                    case 4:
                        double gjendja = 0;
                        if (idmag > 0)
                            gjendja = dbRegj.ktheSasineProgresiveArtikullitEdheSipasDetajimit(artikulli.IdArtikulli, iddetajim == -1 ? 0 : iddetajim, idmag, data, idRenditjes);
                        else
                            gjendja = iddetajim == -1 ? dbRegj.ktheSasineTotaleSipasArtikullitHPD(artikulli.IdArtikulli, -1, data, idRenditjes) : dbRegj.ktheSasineTotaleSipasDetajimitHPD(artikulli.IdArtikulli, iddetajim, -1, data, idRenditjes);
                        cm = llogaritCmimMesatarFifo(artikulli.IdArtikulli, iddetajim, idmag > 0 ? idmag : -1, data, idRenditjes, sasia, double.Parse(artikulli.KoeficientArtikulli.ToString()), gjendja, iddetajim == -1 ? false : true, dbRegj, false, 0);
                        break;
                    case 5:
                        double gjendje = iddetajim == -1 ? dbRegj.ktheSasineTotaleSipasArtikullitHPD(artikulli.IdArtikulli, idmag > 0 ? idmag : -1, data, idRenditjes) : dbRegj.ktheSasineTotaleSipasDetajimitHPD(artikulli.IdArtikulli, iddetajim, idmag, data, idRenditjes);

                        cm = llogaritCmimMesatarFifo(artikulli.IdArtikulli, iddetajim, idmag > 0 ? idmag : -1, data, idRenditjes, sasia, double.Parse(artikulli.KoeficientArtikulli.ToString()), gjendje, iddetajim == -1 ? false : true, dbRegj, true, 0);
                        break;
                    default:
                        throw new DbCore.MyException("Metode kostoje e panjohur!");
                }
            }
            return cm;
        }

        public double llogaritCmimMesatarDetajimDyte(clsArtikulli artikulli, decimal KoeficientArtikulli, int idmag, DateTime data, int iddetajim, double sasia, int idPerdorues)
        {
            double cm = 0;
            clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim();
            int idRenditjes = dbRegj.vendosIdRenditjesTrupi(idmag, data);

            switch (artikulli.MetodeKostojeArtikulli)
            {
                case 1:
                    if (iddetajim == -1)
                        cm = dbRegj.llogaritCmimMesatarMeTotalHPD(artikulli.IdArtikulli, idmag, data, idRenditjes, 0, 0, -1);
                    else
                        cm = dbRegj.llogaritCmimMesatarMeTotalDetajimitTeDyteHPD(artikulli.IdArtikulli, iddetajim, idmag, data, idRenditjes);
                    break;
                case 2:
                case 6:
                    double cmimireferues = artikulli.MetodeKostojeArtikulli == 6 ? clsKokaMagazina.merrCmimReferues(data, artikulli.IdNdermarje, idPerdorues, artikulli.IdArtikulli, artikulli.Njesi1Artikulli) : 0;
                    if (iddetajim == -1)
                    {

                        cm = dbRegj.llogaritCmimMesatarMeTotalSipasRadhes(cm, artikulli.IdArtikulli, idmag, data, idRenditjes, 0, 0, cmimireferues);
                    }
                    else
                        cm = dbRegj.llogaritCmimMesatarMeTotalDetajimitTeDyteSipasRadhes(artikulli.IdArtikulli, iddetajim, idmag, data, idRenditjes, 0, 0, cmimireferues);
                    break;
                case 3:
                    if (iddetajim == -1)
                        cm = dbRegj.llogaritCmimMesatarMeProgresiv(cm, artikulli.IdArtikulli, idmag, data, idRenditjes, 0, 0);
                    break;
                case 4:
                    if (iddetajim == -1)
                        cm = llogaritCmimMesatarFifo(artikulli.IdArtikulli, iddetajim, idmag, data, idRenditjes, sasia, double.Parse(artikulli.KoeficientArtikulli.ToString()), dbRegj.ktheSasineProgresiveArtikullitEdheSipasDetajimit(artikulli.IdArtikulli, 0, idmag, data, idRenditjes), false, dbRegj, false, 0);
                    break;
                case 5:
                    if (iddetajim == -1)
                        cm = llogaritCmimMesatarFifo(artikulli.IdArtikulli, iddetajim, idmag, data, idRenditjes, sasia, double.Parse(artikulli.KoeficientArtikulli.ToString()), dbRegj.ktheSasineTotaleSipasArtikullitHPD(artikulli.IdArtikulli, idmag, data, idRenditjes), false, dbRegj, true, 0);
                    break;
                default:
                    break;
            }

            dbRegj.Dispose();
            return cm;
        }


        public static double merrSasiSipasDetajimit(clsArtikulli artikulli, int idmag, DateTime data, int iddetajim, int lloji, clsDatabaseRegjistrim dbRegj)
        {
            double sasia = 0;
            if (lloji == 1)
                sasia = merrSasi(artikulli, idmag, data, iddetajim, dbRegj);
            else
                sasia = merrSasiDetajimDyte(artikulli, idmag, data, iddetajim);
            return sasia;
        }
        public static DataTable merrSasiSipasDetajimeve(clsArtikulli artikulli, int idmag, DateTime data, int iddetajim, int lloji,string detajime)
        {
            using (clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim())
            {
                return merrSasiDetajimeve(artikulli, idmag, data, iddetajim, dbRegj, detajime);
            }
        }
        public static DataTable merrSasiSipasDetajimeve2(clsArtikulli artikulli, int idmag, DateTime data, int iddetajim, int lloji,string detajime)
        {
            using (clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim())
            {
                return merrSasiDetajimeve2(artikulli, idmag, data, iddetajim, dbRegj, detajime);
            }
        }

        public static double merrSasiSipasDetajimit(clsArtikulli artikulli, int idmag, DateTime data, int iddetajim, int lloji)
        {
            using (clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim())
            {
                return merrSasiSipasDetajimit(artikulli, idmag, data, iddetajim, lloji, dbRegj);
            }
        }
        //public static DataTable merrSasiSipasDetajimitTest(clsArtikulli artikulli, int idmag, DateTime data, int iddetajim, int lloji, string test)
        //{
        //    using (clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim())
        //    {
        //        return merrSasiSipasDetajimitTest(artikulli, idmag, data, iddetajim, lloji, dbRegj,test);
        //    }
        //}
        //public static DataTable merrSasiSipasDetajimitTest2(clsArtikulli artikulli, int idmag, DateTime data, int iddetajim, int lloji, string test)
        //{
        //    using (clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim())
        //    {
        //        return merrSasiSipasDetajimitTest2(artikulli, idmag, data, iddetajim, lloji, dbRegj,test);
        //    }
        //}

        public static double merrSasiDetajimitDyteDheDetajimPare(clsArtikulli artikulli, int idmag, DateTime data, int idDetajimi1, int iddetajim2)
        {
            using (clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim())
            {
                return merrSasiDetajimitDyteDheDetajimPare(artikulli, idmag, data, idDetajimi1, iddetajim2, dbRegj);
            }
        }

        public static double merrSasiDetajimitDyteDheDetajimPare(clsArtikulli artikulli, int idmag, DateTime data, int idDetajimi1, int iddetajim2, clsDatabaseRegjistrim dbRegj)
        {
            double sasia;
            if (idmag > 0)
                sasia = merrSasiDetajimDyteNeVaresiDetajimPare(artikulli, idmag, data, iddetajim2, idDetajimi1, dbRegj);
            else
                sasia = merrSasineGjitheMagDetajimDyteSipasDetajimPare(artikulli, data, idDetajimi1, iddetajim2, dbRegj);
            return sasia;
        }

        public static double ktheVleftenTotaleSipasArtikullitHPD(int idArt, int idmag, DateTime dt, int idrenditjes)
        {
            clsDatabaseRegjistrim dbregj = new clsDatabaseRegjistrim();
            double vlefta = dbregj.ktheVleftenTotaleSipasArtikullitHPD(idArt, idmag, dt, idrenditjes);
            return vlefta;
        }

        public static double merrSasi(clsArtikulli artikulli, int idmag, DateTime data, int iddetajim)
        {
            using (clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim())
            {
                return merrSasi(artikulli, idmag, data, iddetajim, dbRegj);
            }
        }

        public static double merrSasi(clsArtikulli artikulli, int idmag, DateTime data, int iddetajim, clsDatabaseRegjistrim dbRegj, bool disponibilitetiArtikullit = false)
        {
            int idRenditjes = dbRegj.vendosIdRenditjesTrupi(idmag, data);
            if(artikulli.MetodeKostojeArtikulli == 2 || artikulli.MetodeKostojeArtikulli == 6)
                return dbRegj.ktheSasineTotaleSipasArtikullitSipasRadhes(artikulli.IdArtikulli, idmag, data, idRenditjes,disponibilitetiArtikullit, iddetajim);
            if (artikulli.MetodeKostojeArtikulli != 1 && artikulli.MetodeKostojeArtikulli != 5)   //progresiv apo fifo
                return dbRegj.ktheSasineProgresiveArtikullitEdheSipasDetajimit(artikulli.IdArtikulli, iddetajim, idmag, data, idRenditjes, disponibilitetiArtikullit);
            //cmim mesatar hyrje para dalje dhe fifo2
            return dbRegj.ktheSasineTotaleSipasArtikullitHPD(artikulli.IdArtikulli, idmag, data, idRenditjes, disponibilitetiArtikullit, iddetajim);
        }
        public static DataTable merrSasiDetajimeve(clsArtikulli artikulli, int idmag, DateTime data, int iddetajim, clsDatabaseRegjistrim dbRegj, string detajime, bool disponibilitetiArtikullit = false)
        {
            int idRenditjes = dbRegj.vendosIdRenditjesTrupi(idmag, data);
            return dbRegj.ktheSasineTotaleSipasArtikullitHPDDetajimeve(artikulli.IdArtikulli, idmag, data, idRenditjes, detajime, disponibilitetiArtikullit, iddetajim);
        }
        public static DataTable merrSasiDetajimeve2(clsArtikulli artikulli, int idmag, DateTime data, int iddetajim, clsDatabaseRegjistrim dbRegj, string detajime, bool disponibilitetiArtikullit = false)
        {
            int idRenditjes = dbRegj.vendosIdRenditjesTrupi(idmag, data);
            return dbRegj.ktheSasineTotaleSipasArtikullitHPDDetajimeve2(artikulli.IdArtikulli, idmag, data, idRenditjes, detajime, disponibilitetiArtikullit, iddetajim);
        }

        public static double merrSasiDetajimDyte(clsArtikulli artikulli, int idmag, DateTime data, int iddetajim, clsDatabaseRegjistrim dbRegj)
        {
            int idRenditjes = dbRegj.vendosIdRenditjesTrupi(idmag, data);
            if (iddetajim == -1)
            {
                if (artikulli.MetodeKostojeArtikulli == 2 || artikulli.MetodeKostojeArtikulli == 6) //cmim mesatar sipas id se renditjes
                    return dbRegj.ktheSasineTotaleSipasArtikullitSipasRadhes(artikulli.IdArtikulli, idmag, data, idRenditjes);
                if (artikulli.MetodeKostojeArtikulli != 1 || artikulli.MetodeKostojeArtikulli != 5)
                    return dbRegj.ktheSasineProgresiveArtikullitEdheSipasDetajimit(artikulli.IdArtikulli, 0, idmag, data, idRenditjes);
                //cmim mesatar hyrje para dalje
                return dbRegj.ktheSasineTotaleSipasArtikullitHPD(artikulli.IdArtikulli, idmag, data, idRenditjes);
            }
            else
            {
                if (artikulli.MetodeKostojeArtikulli == 2 || artikulli.MetodeKostojeArtikulli == 6) //cmim mesatar sipas id se renditjes
                    return dbRegj.ktheSasineTotaleSipasDetajimitTeDyteSipasRadhes(artikulli.IdArtikulli, iddetajim, idmag, data, idRenditjes);
                if (artikulli.MetodeKostojeArtikulli == 1 || artikulli.MetodeKostojeArtikulli == 5)   //cmim mesatar hyrje para dalje
                    return dbRegj.ktheSasineTotaleSipasDetajimitTeDyteHPD(artikulli.IdArtikulli, iddetajim, idmag, data, idRenditjes);
            }
            return 0;
        }

        public static double merrSasiDetajimDyte(clsArtikulli artikulli, int idmag, DateTime data, int iddetajim)
        {
            using (clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim())
            {
                return merrSasiDetajimDyte(artikulli, idmag, data, iddetajim, dbRegj);
            }
        }

        public static double merrSasineNgaDokumenti(int idDok, int IdArtikulli, int IdDetajimArtikulli, bool dokMag)
        {
            double gjendjaArtikullitNeDokument = 0;
            clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim();
            gjendjaArtikullitNeDokument = dbRegj.merrSasiNgaDokMagazine(idDok, IdArtikulli, IdDetajimArtikulli, dokMag);
            dbRegj.Dispose();
            return gjendjaArtikullitNeDokument;
        }

 


        public static double merrSasineGjitheMagDetajimDyteSipasDetajimPare(clsArtikulli artikulli, DateTime data, int iddetajim, int idDetajim2, clsDatabaseRegjistrim dbRegj)
        {
            double gjendjaArtikullitNeMagazine = 0;

            int idRenditjes = int.MaxValue;
            if (iddetajim == -1)
            {
                if (artikulli.MetodeKostojeArtikulli == 2 || artikulli.MetodeKostojeArtikulli == 6) //cmim mesatar sipas id se renditjes
                    gjendjaArtikullitNeMagazine = dbRegj.ktheSasineTotaleSipasArtikullitSipasRadhes(artikulli.IdArtikulli, -1, data, idRenditjes);
                else
                    gjendjaArtikullitNeMagazine = dbRegj.ktheSasineTotaleSipasArtikullitHPD(artikulli.IdArtikulli, -1, data, idRenditjes);
            }
            else
            {
                if (artikulli.MetodeKostojeArtikulli == 2 || artikulli.MetodeKostojeArtikulli == 6) //cmim mesatar sipas id se renditjes
                    gjendjaArtikullitNeMagazine = dbRegj.ktheSasineTotaleSipasDetajimitTeDyteSipasDetajimTePareRadhesGjitheMag(artikulli.IdArtikulli, iddetajim, data, idRenditjes, idDetajim2);
                else
                    gjendjaArtikullitNeMagazine = dbRegj.ktheSasineTotaleSipasDetajimitTePareDheTeDyteHPDGjitheMag(artikulli.IdArtikulli, iddetajim, data, idRenditjes, idDetajim2);
            }
            return gjendjaArtikullitNeMagazine;
        }

        public static double merrSasiDetajimDyteNeVaresiDetajimPare(clsArtikulli artikulli, int idmag, DateTime data, int iddetajim2, int idDetajim1, clsDatabaseRegjistrim dbRegj)
        {
            double gjendjaArtikullitNeMagazine = 0;

            int idRenditjes = dbRegj.vendosIdRenditjesTrupi(idmag, data);
            if (iddetajim2 == -1 || iddetajim2 == 0)
            {
                if (artikulli.MetodeKostojeArtikulli == 1 || artikulli.MetodeKostojeArtikulli == 5)   //cmim mesatar hyrje para dalje dhe fifo2
                    gjendjaArtikullitNeMagazine = dbRegj.ktheSasineTotaleSipasArtikullitHPD(artikulli.IdArtikulli, idmag, data, idRenditjes);
                else if (artikulli.MetodeKostojeArtikulli == 2 || artikulli.MetodeKostojeArtikulli == 6) //cmim mesatar sipas id se renditjes
                    gjendjaArtikullitNeMagazine = dbRegj.ktheSasineTotaleSipasArtikullitSipasRadhes(artikulli.IdArtikulli, idmag, data, idRenditjes);
                else //progresiv apo fifo
                {
                    gjendjaArtikullitNeMagazine = dbRegj.ktheSasineProgresiveArtikullitEdheSipasDetajimit(artikulli.IdArtikulli, 0, idmag, data, idRenditjes);
                }
            }
            else
            {
                if (artikulli.MetodeKostojeArtikulli == 1 || artikulli.MetodeKostojeArtikulli == 5)   //cmim mesatar hyrje para dalje
                    gjendjaArtikullitNeMagazine = dbRegj.ktheSasineTotaleSipasDetajimit1Dhe2HPD(artikulli.IdArtikulli, iddetajim2, idmag, data, idRenditjes, idDetajim1);

                else if (artikulli.MetodeKostojeArtikulli == 2 || artikulli.MetodeKostojeArtikulli == 6) //cmim mesatar sipas id se renditjes
                    gjendjaArtikullitNeMagazine = dbRegj.ktheSasineTotaleSipasDetajimit1Dhe2SipasRadhes(artikulli.IdArtikulli, iddetajim2, idmag, data, idRenditjes, idDetajim1);
                else    //fifo    apo progresiv
                {
                    gjendjaArtikullitNeMagazine = dbRegj.ktheSasineProgresiveSipasDetajimit1Dhe2(artikulli.IdArtikulli, iddetajim2, idmag, data, idRenditjes, idDetajim1);
                }
            }
            return gjendjaArtikullitNeMagazine;
        }

        public static DateTime merrDatePerMag(clsArtikulli artikulli, int idMag, int iddetajimi, DateTime dt, int lloji, clsDatabaseRegjistrim dbRegj)
        {
            DateTime data = DateTime.Now.ToLocalTime();
            if (lloji == 1)
                data = merrDateSipasDetajimitParePerMag(artikulli, idMag, iddetajimi, dt, dbRegj);
            else if (lloji == 2)
                data = merrDateSipasDetajimitDytePerMag(artikulli, idMag, iddetajimi, dt, dbRegj);
            return data;
        }

        public static DateTime merrDateSipasDetajimPareDheDyte(clsArtikulli artikulli, int idMag, int iddetajimi, DateTime dt, int iddetajimi2)
        {
            using (clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim())
            {
                return merrDateSipasDetajimPareDheDyte(artikulli, idMag, iddetajimi, dt, iddetajimi2, dbRegj);
            }
        }

        public static DateTime merrDateSipasDetajimPareDheDyte(clsArtikulli artikulli, int idMag, int iddetajimi, DateTime dt, int iddetajimi2, clsDatabaseRegjistrim dbRegj)
        {
            DateTime data = DateTime.Now.ToLocalTime();
            data = merrDateSipasDetajimitPareDheDytePerMag(artikulli, idMag, iddetajimi, dt, iddetajimi2, dbRegj);
            return data;
        }

        public static DateTime merrDateSipasDetajimitParePerMag(clsArtikulli artikulli, int idMag, int iddetajimi, DateTime dt, clsDatabaseRegjistrim dbRegj)
        {
            int idRenditjes = int.MaxValue;
            var data = dbRegj.ktheDateSipasDetajimitPerMag(artikulli.IdArtikulli, idMag, iddetajimi, dt, idRenditjes);
            if (string.IsNullOrEmpty(data)) return new DateTime(9999, 12, 30);

            DateTime date;
            bool dateVlefshme = DateTime.TryParse(data, out date);
            return date;
        }

        public static DateTime merrDateSipasDetajimitPareDheDytePerMag(clsArtikulli artikulli, int idMag, int iddetajimi, DateTime dt, int idDetajimi2, clsDatabaseRegjistrim dbRegj)
        {
            int idRenditjes = int.MaxValue;
            var data = dbRegj.ktheDateSipasDetajimitPareDheDytePerMag(artikulli.IdArtikulli, idMag, iddetajimi, dt, idRenditjes, idDetajimi2);
            if (string.IsNullOrEmpty(data))
                return new DateTime(9999, 12, 30);
            DateTime date;
            bool dateVlefshme = DateTime.TryParse(data, out date);
            return date;
        }

        public static DateTime merrDateSipasDetajimitDytePerMag(clsArtikulli artikulli, int idMag, int iddetajimi, DateTime dt, clsDatabaseRegjistrim dbRegj)
        {
            int idRenditjes = int.MaxValue;
            var data = dbRegj.ktheDateSipasDetajimitDytePerMag(artikulli.IdArtikulli, idMag, iddetajimi, dt, idRenditjes);
            if (string.IsNullOrEmpty(data)) return new DateTime(9999, 12, 30);

            DateTime date;
            bool dateVlefshme = DateTime.TryParse(data, out date);
            return date;
        }

        public static bool ekzistonTrupiMagazina(int idTrupiMag)
        {
            clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim();
            bool ekziston = dbRegj.ekzistonRreshtTrupiMagazina(idTrupiMag);
            dbRegj.Dispose();
            return ekziston;
        }

        public static bool ekzistonIMEIneDokKthimiDraft(int iddetajim, int idmag, DateTime datedok)
        {
            using (var dbRegj = new clsDatabaseRegjistrim())
            {
                return dbRegj.ekzistonIMEIneDokKthimiDraft(iddetajim, idmag, datedok);
            }
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush trupin e magazines nga databaza
        /// </summary>
        /// <param name="dbDataRowTrupMagazine">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushTrupMagazine(DataRow dbDataRowTrupMagazine)
        {
            if (dbDataRowTrupMagazine != null)
            {
                try
                {
                    int.TryParse(dbDataRowTrupMagazine["IDTRUPIMAGAZINA"].ToString(), out idTrupiMagazina);
                    int.TryParse(dbDataRowTrupMagazine["IDKOKAMAGAZINA"].ToString(), out idKokaMagazina);
                    int.TryParse(dbDataRowTrupMagazine["IDLLOJVEPRIMI"].ToString(), out idLlojVeprimi);
                    int.TryParse(dbDataRowTrupMagazine["IDARTIKULL"].ToString(), out idArtikull);
                    int.TryParse(dbDataRowTrupMagazine["IDNJESIA"].ToString(), out idNjesia);
                    double.TryParse(dbDataRowTrupMagazine["SASIA"].ToString(), out sasia);
                    double.TryParse(dbDataRowTrupMagazine["CMIMI"].ToString(), out cmimi);
                    double.TryParse(dbDataRowTrupMagazine["VLEFTA"].ToString(), out vlefta);
                    double.TryParse(dbDataRowTrupMagazine["KOEFICENTI"].ToString(), out koeficenti);
                    int.TryParse(dbDataRowTrupMagazine["SHENJA"].ToString(), out shenja);
                    double.TryParse(dbDataRowTrupMagazine["SASIAPROGRESIVE"].ToString(), out sasiaProgresive);
                    double.TryParse(dbDataRowTrupMagazine["VLEFTAPROGRESIVE"].ToString(), out vleftaProgresive);
                    int.TryParse(dbDataRowTrupMagazine["IDMAG"].ToString(), out idMag);
                    DateTime.TryParse(dbDataRowTrupMagazine["DATA"].ToString(), out data);
                    int.TryParse(dbDataRowTrupMagazine["IDSTATUSDOK"].ToString(), out idStatusDok);
                    int.TryParse(dbDataRowTrupMagazine["IDRENDITJES"].ToString(), out idRenditjes);
                    int.TryParse(dbDataRowTrupMagazine["IDDETAJIMI"].ToString(), out idDetajimi);
                    int.TryParse(dbDataRowTrupMagazine["IDDETAJIMI2"].ToString(), out idDetajimi2);
                    double.TryParse(dbDataRowTrupMagazine["SASIPROGRESIVEDETAJIMI"].ToString(), out sasiProgresiveDetajimi);
                    double.TryParse(dbDataRowTrupMagazine["VLEFTEPROGRESIVEDETAJIMI"].ToString(), out vlefteProgresiveDetajimi);
                    int.TryParse(dbDataRowTrupMagazine["IDTRUPIKONVERTIMFSH"].ToString(), out idTrupiKonvertimFSH);
                    int.TryParse(dbDataRowTrupMagazine["IDTRUPIKONVERTIMUSH"].ToString(), out idTrupiKonvertimUSH);
                    int.TryParse(dbDataRowTrupMagazine["IDTRUPIKONVERTIMUD"].ToString(), out idTrupiKonvertimUD);
                    int.TryParse(dbDataRowTrupMagazine["IDTRUPIREZERVIMI"].ToString(), out idTrupiRezervimi);
                    int.TryParse(dbDataRowTrupMagazine["IDKTHIMI"].ToString(), out idKthimi);
                    int.TryParse(dbDataRowTrupMagazine["IDTRUPISHITJEGJENERIMI"].ToString(), out idTrupiShitjeGjenerimi);
                    double.TryParse(dbDataRowTrupMagazine["sasiambetur"].ToString(), out sasimbetur);
                    shenime = dbDataRowTrupMagazine["SHENIME"].ToString();
                    int.TryParse(dbDataRowTrupMagazine["IDARTIKULLSET"].ToString(), out idArtikullSet);
                    int.TryParse(dbDataRowTrupMagazine["IDBARKODI"].ToString(), out idBarkodi);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se trupit te magazines nga db-ja");
                }
            }
            else
                return false;
        }
       
        #endregion
    }
}