using DbCore.DbAdmin;
using DbCore.DbAsete;
using DbCore.DbInventari;
using DbCore.DbKontabiliteti;
using DbCore.DbShare;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Resources;


namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne  koken e nje dokumenti te magazine
    ///  (Te dhenat  merren nga tabela : T_KOKANDRYSHIMCMIMSASI)
    /// </summary>
    public class clsKokaNdryshimCmimSasi
    {
        #region Attributet
        private int idKoka;
        private int idNivel;
        private int idKonfigAmbjente;
        private int idMagazina;
        private DateTime dtDok;
        private String nrDok;
        private int idDokNga;
        private double vlefta;
        private int idStatusDok;
        private int idNdermarje;
        private int idNderVit;
        private int idKrijues;
        private int idPerdoruesi;
        private DateTime dtRegj;
        private int idNivelGjenerues;
        private int idKonfigGjenerues;
        private int idGjenerues;
        private int idDegeAdministrative;
        private int idLlogKunderparti;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private colTrupiNdryshimCmimSasi ocolTrupiNdryshimCmimSasi;
        private clsKokaFleteKontabel oFleteKontabel;
        private clsKokaMagazina oKokaMagazina;
        private string kodMagazina;
        private string kodDegeAdministrative;
        private string nrLlogari;
        private string pershkrimi;

        /// <summary>
        /// id e grupimit te pare
        /// </summary>
        private int idGrup1;
        /// <summary>
        /// id e grupimit te dyte
        /// </summary>
        private int idGrup2;
        /// <summary>
        /// id e grupimit te trete
        /// </summary>
        private int idGrup3;
        private DataRow rreshti;


        #endregion

        #region Properties
        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdKoka
        {
            get { return idKoka; }
            set { idKoka = value; }
        }
        public int IdKrijues
        {
            get
            {
                return idKrijues;
            }
            set
            {
                idKrijues = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne  e nivelit te regjistrimit.
        /// </summary>
        public int IdNivel
        {
            get { return idNivel; }
            set { idNivel = value; }
        }


        /// <summary>
        /// Kthen/Vendos ID-ne e konfigurimit te ambjentit
        /// </summary>
        public int IdKonfigAmbjente
        {
            get { return idKonfigAmbjente; }
            set { idKonfigAmbjente = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e magazines
        /// </summary>
        public int IdMagazina
        {
            get { return idMagazina; }
            set { idMagazina = value; }
        }

        /// <summary>
        /// id e grupimit te pare
        /// </summary>
        public int IdGrup1
        {
            get
            {
                return idGrup1;
            }
            set
            {
                idGrup1 = value;
            }
        }
        /// <summary>
        /// id e grupimit te dyte
        /// </summary>
        public int IdGrup2
        {
            get
            {
                return idGrup2;
            }
            set
            {
                idGrup2 = value;
            }
        }
        /// <summary>
        /// id e grupimit te trete
        /// </summary>
        public int IdGrup3
        {
            get
            {
                return idGrup3;
            }
            set
            {
                idGrup3 = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos nr i dokumentit.
        /// </summary>
        public String NrDok
        {
            get { return nrDok; }
            set { nrDok = value; }
        }
        /// <summary>
        /// Kthen/Vendos dt e dokumentit.
        /// </summary>
        public DateTime DtDok
        {
            get { return dtDok; }
            set { dtDok = value; }
        }


        /// <summary>
        /// Kthen/Vendos ID-ne  e dokumentit nga i cili gjenerohet ne rastet e modifikimit
        /// </summary>
        public int IdDokNga
        {
            get { return idDokNga; }
            set { idDokNga = value; }
        }
        /// <summary>
        /// Kthen/Vendos vleften totale te dokumentit.
        /// </summary>
        public double Vlefta
        {
            get { return vlefta; }
            set { vlefta = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e gjendjes se dokumentit
        /// <example> ruajtur, draft etj.</example>
        /// </summary>
        public int IdStatusDok
        {
            get { return idStatusDok; }
            set { idStatusDok = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne  e ndermarjes.
        /// </summary>
        public int IdNdermarje
        {
            get { return idNdermarje; }
            set { idNdermarje = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e ndermarje vitit.
        /// </summary>
        public int IdNderVit
        {
            get { return idNderVit; }
            set { idNderVit = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne  e perdoruesit qe e ka ruajtur.
        /// </summary>
        public int IdPerdoruesi
        {
            get { return idPerdoruesi; }
            set { idPerdoruesi = value; }
        }
        /// <summary>
        /// Kthen/Vendos dt e regjistrimit te dokumentit.
        /// </summary>
        public DateTime DtRegjistrimi
        {
            get { return dtRegj; }
            set { dtRegj = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e nivelit te dokumentit nga eshte gjeneruar ndryshim cmim sasi
        /// </summary>

        public int IdNivelGjenerues
        {
            get { return idNivelGjenerues; }
            set { idNivelGjenerues = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e konfigurimit te dokumentit nga eshte gjeneruar ndryshim cmim sasi
        /// </summary>

        public int IdKonfigGjenerues
        {
            get { return idKonfigGjenerues; }
            set { idKonfigGjenerues = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e dokumentit nga eshte gjeneruar ndryshim cmim sasi nga nje ambjent tjeter
        /// </summary>

        public int IdGjenerues
        {
            get { return idGjenerues; }
            set { idGjenerues = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e deges administrative
        /// </summary>

        public int IdDegeAdministrative
        {
            get { return idDegeAdministrative; }
            set { idDegeAdministrative = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e llogarise
        /// </summary>

        public int IdLlogKunderparti
        {
            get { return idLlogKunderparti; }
            set { idLlogKunderparti = value; }
        }

        /// <summary>
        /// Kthen/Vendos  nje koleksion me trupin e dokumentit te magazines
        /// </summary>
        public colTrupiNdryshimCmimSasi OcolTrupiNdryshimCmimSasi
        {
            get { return ocolTrupiNdryshimCmimSasi; }
            set { ocolTrupiNdryshimCmimSasi = value; }
        }

        /// <summary>
        /// Kthen/Vendos nje  flete kontabel te gjeneruar nga dokumenti i magazines kur kontabilizohet.
        /// </summary>
        public clsKokaFleteKontabel OFleteKontabel
        {
            get { return oFleteKontabel; }
            set { oFleteKontabel = value; }
        }

        public clsKokaMagazina OKokaMagazina
        {
            get { return oKokaMagazina; }
            set { oKokaMagazina = value; }
        }
        public DateTime DtKrijimi
        {
            get { return dtKrijimi; }

        }
        public DateTime DtModifikimi
        {
            get { return dtModifikimi; }

        }


        public String Pershkrimi
        {
            get
            {
                return pershkrimi;
            }
            set
            {
                pershkrimi = value;
            }

        }
        #endregion

        #region Konstruktoret
        /// <summary>
        /// Konstruktori i klases me parametra
        /// </summary>
        /// <param name="dtDk"> data e dokumentit te ndryshim cmim sasi</param>
        /// <param name="dtRegj"> data e regjistrimit te dokumentit te ndryshim cmim sasi</param>
        /// <param name="idKonf"> id e konfigurimit te dokumentit</param>
        /// <param name="idLidhes">id e dokumentit nga e cila gjenerohet ne rastet e modifikimit </param>
        /// <param name="idMag">id e magazines</param>
        /// <param name="idKoka">id ritese e kokes se ndryshim cmim sasi</param>
        /// <param name="idNder"> id e ndermarjes</param>
        /// <param name="idNdVt"> id e ndermarje vitit</param>
        /// <param name="idNiv"> id e nivelit te regjistrimit</param>
        /// <param name="idPer"> id e perdoruesit se e ben regjistrimin</param>
        /// <param name="idSt"> id e statusit te dokumentit</param>
        /// <param name="nrDk">nr i dokumentit</param>
        /// <param name="vl"> vlera totale e dokumentit</param>
        /// <param name="idGjenerues">id e dokumentit nga gjenerohet ndryshim cmim sasi nga nje ambjent tjeter</param>
        /// <param name="idKonfigGjenerues">id e konfigurimit nga gjenerohet dokumenti</param>
        /// <param name="idNivelGjenerues">id e nivelit nga gjenerohet dokumenti</param>  
        /// <param name="idgrup1">id e grupimit te pare</param>
        /// <param name="idgrup2">id e grupimit te dyte</param>
        /// <param name="idgrup3">id e grupimit te trete</param>

        public clsKokaNdryshimCmimSasi(int idKoka, int idNiv, int idKonf, int idMag, DateTime dtDk, string nrDk, int idLidhes, double vl, int idSt, int idNder, int idNdVt, int idPer, DateTime dtRegj, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues, int idDegeAdministrative, int idllogari, int idgrup1, int idgrup2, int idgrup3, String pershkrimi, int idkrijues)
        {
            this.idKoka = idKoka;
            idNivel = idNiv;
            idKonfigAmbjente = idKonf;

            idMagazina = idMag;
            nrDok = nrDk;
            dtDok = dtDk;

            idDokNga = idLidhes;
            vlefta = vl;
            idStatusDok = idSt;
            idNdermarje = idNder;
            idNderVit = idNdVt;
            idPerdoruesi = idPer;
            this.dtRegj = dtRegj;
            idLlogKunderparti = idllogari;
            this.idKrijues = idkrijues;
            idGrup1 = idgrup1;
            idGrup2 = idgrup2;
            idGrup3 = idgrup3;
            this.pershkrimi = pershkrimi;

            this.idNivelGjenerues = idNivelGjenerues;
            this.idKonfigGjenerues = idKonfigGjenerues;
            this.idGjenerues = idGjenerues;
            this.idDegeAdministrative = idDegeAdministrative;

        }

        /// <summary>
        /// konstruktor me 3 parametra
        /// </summary>
        /// <param name="idNivel">id e nivelit</param>
        /// <param name="nrdok">nr i dokumentit</param>
        /// <param name="dtdok">data e dokumentit</param>
        public clsKokaNdryshimCmimSasi(int idNivel, string nrdok, DateTime dtdok)
        {
            clsDatabaseRegjistrim dbKoka = new clsDatabaseRegjistrim();
            mbushKokaNdryshimCmimSasi(dbKoka.ktheKokaNdryshimCmimSasiSipasIdNivelNrDokDtDok(idNivel, nrdok, dtdok));
            dbKoka.Dispose();
        }

        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsKokaNdryshimCmimSasi()
        {
            ocolTrupiNdryshimCmimSasi = new colTrupiNdryshimCmimSasi();
        }

        public clsKokaNdryshimCmimSasi(DataRow rreshti)
        {
            
            mbushKokaNdryshimCmimSasi(rreshti);
        }
        #endregion

        #region Metoda Publike

        public clsMesazh krijoNdryshimCmimSasi(int idekzistuese, int idNiv, int idKonf, int idMag, string kodmag, DateTime dtDk, string nrDk, double vl, int idSt, int idNder, int idNdVt, int idPer, int idkrijuesi, DateTime dtRegj, int idDegeAdministrative, string koddege, int idllogari, string nrllogari, int idgrup1, int idgrup2, int idgrup3, string pershkrimi, colTrupiNdryshimCmimSasi coltrupi, clsKokaFleteKontabel fletekont, out string mesazhinformues, bool meautorizim, ref bool gjeneroDokMag, DbShare.clsKonfigurimAmbjenti konfmag)
        {
            mesazhinformues = "";
            clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();
            return krijoNdryshimCmimSasi(idekzistuese, idNiv, idKonf, idMag, kodmag, dtDk, nrDk, 0, vl, idSt, idNder, idNdVt, idPer, idkrijuesi, dtRegj, 0, 0, 0, idDegeAdministrative, koddege, idllogari, nrllogari, idgrup1, idgrup2, idgrup3, pershkrimi, coltrupi, fletekont, db, meautorizim, ref gjeneroDokMag, konfmag, out mesazhinformues);
        }

        public clsMesazh krijoNdryshimCmimSasi(int idekzistuese, int idNiv, int idKonf, int idMag, string kodmag, DateTime dtDk, string nrDk, int idLidhes, double vl, int idSt, int idNder, int idNdVt, int idPer, int idkrijues, DateTime dtRegj, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues, int idDegeAdministrative, string koddege, int idllogari, string nrllogari, int idgrup1, int idgrup2, int idgrup3, string pershkrimi, colTrupiNdryshimCmimSasi coltrupi, clsKokaFleteKontabel fletekont, clsDatabaseRegjistrim db, bool meautorizm, ref bool gjeneroDokMag, DbShare.clsKonfigurimAmbjenti konfmag, out string mesazhinformues)
        {
            mesazhinformues = "";
            idKoka = idekzistuese;
            idNivel = idNiv;
            idKonfigAmbjente = idKonf;

            idMagazina = idMag;
            kodMagazina = kodmag;
            nrDok = nrDk;
            dtDok = dtDk;

            idDokNga = idLidhes;
            vlefta = vl;
            idStatusDok = idSt;
            idNdermarje = idNder;
            idNderVit = idNdVt;
            idPerdoruesi = idPer;
            this.dtRegj = dtRegj;
            idLlogKunderparti = idllogari;
            idGrup1 = idgrup1;
            idGrup2 = idgrup2;
            idGrup3 = idgrup3;
            this.pershkrimi = pershkrimi;
            this.idKrijues = idkrijues;
            this.idNivelGjenerues = idNivelGjenerues;
            this.idKonfigGjenerues = idKonfigGjenerues;
            this.idGjenerues = idGjenerues;
            this.idDegeAdministrative = idDegeAdministrative;
            kodDegeAdministrative = koddege;
            nrLlogari = nrllogari;

            ocolTrupiNdryshimCmimSasi = coltrupi;

            oFleteKontabel = fletekont;

            clsMesazh mesazh = this.kontrollo(db, meautorizm);
            if (!mesazh.Status)
                return mesazh;

            #region magazina
            oKokaMagazina = new clsKokaMagazina();

            oKokaMagazina.OFleteKontabel = new clsKokaFleteKontabel();

            if (gjeneroDokMag)
            {
                //gjenerohet dokumenti i magazines po pa fleten kontabel te magazines sepse varet nga cmimi i daljes dhe gjenerohet kur ruhet dokumenti i magazines
                mesazh = krijoMagazineNgaNdryshimCmimSasi(ref gjeneroDokMag, this, konfmag, idMag, kodmag, out mesazhinformues);
                if (!mesazh.Status)
                    return mesazh;
            }
            #endregion

            return new clsMesazh(true, "Ndryshim sasi/cmim u krijua me sukses!");
        }
        private clsMesazh krijoMagazineNgaNdryshimCmimSasi(ref bool gjeneroDokMag, clsKokaNdryshimCmimSasi koka, DbShare.clsKonfigurimAmbjenti konfmag, int idmag, string kodmag, out string mesazhinformues)
        {
            mesazhinformues = "";
            string shenime = "";
            if (koka.pershkrimi != String.Empty)
                shenime = koka.pershkrimi;
            else
            {
                shenime = "Nga ndryshim sasi/cmim";
            }
            clsDatabaseInventari dbinv = new clsDatabaseInventari();
            colTrupiMagazina coltrupi;

            coltrupi = ruajTrupinEMagazines(koka, 1, 0, dbinv, false);
            dbinv.Dispose();
            if (coltrupi.Count == 0)
            {
                gjeneroDokMag = false;
                return new clsMesazh(true, "Dokumenti i ndryshim cmim sasi nuk ka rreshta per te krijuar dokument magazine");
            }
            clsKokaRezervime kokadalje = new clsKokaRezervime();
            clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();
            int idRaportDesign = Convert.ToInt32(clsAtributeTrupi.merrVleredefaultSipasKontrollitKodKonfigDheNderm(konfmag.KodKonfigAmbjente, koka.idNdermarje, "cmbFormatiPrintimit"));

            return koka.OKokaMagazina.krijoMagazine(false, koka.oKokaMagazina.IdDokNga, konfmag.IdNivel, konfmag.IdKonfigAmbjente, 0, "", idmag, kodmag, koka.DtDok, koka.NrDok, 0, "", konfmag.IdKategori, koka.oKokaMagazina.IdDokNga, 0, koka.IdStatusDok, koka.idNdermarje, koka.IdNderVit, koka.IdPerdoruesi, koka.DtRegjistrimi, 1, shenime, koka.idNivel, koka.idKonfigAmbjente, koka.idKoka, koka.idDegeAdministrative, koka.kodDegeAdministrative, 0, "", 0, "", false, koka.IdGrup1, koka.IdGrup2, koka.IdGrup3, "", "", "", coltrupi, new clsKokaMagazina(), new clsKokaFleteKontabel(), kokadalje, db, true, 0, "", idRaportDesign, false, koka.IdKrijues, koka.dtDok, 0, "", "", "", 0, null,false,false,"","",0);
        }


        private colTrupiMagazina ruajTrupinEMagazines(clsKokaNdryshimCmimSasi koka, int shenja, int idmag, clsDatabaseInventari dbInv, bool konvertim)
        {
            colTrupiMagazina trupat = new colTrupiMagazina();
            foreach (clsTrupiNdryshimCmimSasi tsh in koka.ocolTrupiNdryshimCmimSasi)
            {
                if (idmag != 0 && tsh.IdMag != idmag)
                    continue;
                clsTrupiMagazina trupMag = new clsTrupiMagazina();


                bool krijuar = false;

                clsArtikulli art = new clsArtikulli(tsh.IdArtikulli, dbInv);
                if (art.Klasa != 4)
                    krijuar = trupMag.krijoTrupMagazinaNgaGrida(koka.idNdermarje, koka.DtDok, 1, 1, tsh.IdArtikulli, tsh.PershkrimArtikull, -1, tsh.IdNjesia, tsh.SasiaRe - tsh.SasiaGjendje, (tsh.VleftaRe - tsh.VleftaGjendje) / ((tsh.SasiaRe - tsh.SasiaGjendje) == 0 ? 1 : (tsh.SasiaRe - tsh.SasiaGjendje)), tsh.VleftaRe - tsh.VleftaGjendje, tsh.IdMag, 1, konvertim ? 0 : -1, tsh.KodiArtikull, "", "", 0, 0, 0, 0, art, "",0, dbInv,false, 0);
                else
                {
                    DbInventari.colArtikulliPerberes artper = new DbInventari.colArtikulliPerberes();
                    artper.ktheArtikujPerberesSipasIdArtikullitKryesorDheDatesMeTeAfert(art.IdArtikulli,koka.dtDok, dbInv);
                    foreach (DbInventari.clsArtikulliPerberes aper in artper)
                    {
                        if (aper.Lloji == 1)    //artikull
                        {
                            trupMag = new clsTrupiMagazina();

                            int idNjesia = tsh.IdNjesia;
                            DbInventari.clsArtikulli a = new DbInventari.clsArtikulli(aper.IdLidheseArt, dbInv);
                            krijuar = trupMag.krijoTrupMagazinaNgaGrida(koka.idNdermarje, koka.dtDok, 1, 1, a.IdArtikulli, a.PershkrimArtikulli, -1, a.Njesi1Artikulli, (tsh.SasiaRe - tsh.SasiaGjendje) * (double)aper.Koeficienti * ((art.Njesi1Artikulli == idNjesia) ? 1 : Convert.ToDouble(art.KoeficientArtikulli)), (tsh.VleftaRe - tsh.VleftaGjendje) / ((tsh.SasiaRe - tsh.SasiaGjendje) == 0 ? 1 : (tsh.SasiaRe - tsh.SasiaGjendje)), tsh.VleftaRe - tsh.VleftaGjendje, tsh.IdMag, 1, -1, a.KodArtikulli, "", "", 0, 0, 0, 0, a, "", art.IdArtikulli,dbInv,false, 0);

                            if (trupMag.IdArtikulli != -1 && krijuar) //ky kusht duhet pare kur te shtohen makrot
                            {
                                if (trupMag.IdMag == -1)
                                {
                                    throw new Exception("Magazina nuk ekziston!");
                                }
                                trupMag.Shenja = shenja;
                                trupat.Add(trupMag);
                            }
                        }
                    }
                    continue;
                }
                if (trupMag.IdArtikulli != -1 && krijuar) //ky kusht duhet pare kur te shtohen makrot kevi
                {
                    if (trupMag.IdMag == -1)
                    {
                        throw new Exception("Magazina nuk ekziston!");
                    }
                    trupMag.Shenja = shenja;
                    trupat.Add(trupMag);
                }
            }
            return trupat;
        }

        private clsMesazh kontrollo(clsDatabaseRegjistrim db, bool meautorizim)
        {
            clsDatabaseKontabilitet dbkont = new clsDatabaseKontabilitet(db );
            DbInventari.clsDatabaseInventari dbinv = new DbInventari.clsDatabaseInventari(db );
            if (nrDok == "")
                return new clsMesazh(false, "Numri i dokumentit nuk mund të jetë bosh");
            if (dtDok == null || dtDok.ToShortDateString() == "01/01/0100")
                return new clsMesazh(false, "Zgjidhni datën e dokumentit!");
            if (dtRegj == null || dtRegj.ToShortDateString() == "01/01/0100")
                return new clsMesazh(false, "Zgjidhni datën e regjistrimit!");

            if (kodMagazina != "")
            {
                if (!clsNjesiAdministrative.ekziston(kodMagazina, idNdermarje, db))
                    return new clsMesazh(false, "Magazina nuk ekziston!");
                clsNjesiAdministrative mag;
                if (meautorizim) mag = new clsNjesiAdministrative(kodMagazina, idNdermarje, idPerdoruesi, db);
                else mag = new clsNjesiAdministrative(kodMagazina, idNdermarje, db);
                if (mag.IdNjesiAdministrative < 1)
                    return new clsMesazh(false, "Nuk keni autorizime ne kete magazine!");
                if (kodMagazina != "" && !mag.Aktiv)
                    return new clsMesazh(false, "Magazina nuk eshte aktive!");
            }
            if (kodDegeAdministrative != "")
            {
                clsDegeAdministrative deg = new clsDegeAdministrative(kodDegeAdministrative, idNdermarje, db);
                if (!deg.Aktiv)
                    return new clsMesazh(false, "Dega administrative nuk është aktive!");
            }
                
            if (nrLlogari != "" && !DbKontabiliteti.clsLlogari.ekzistonLlogari(nrLlogari, idNdermarje, dbkont))
                return new clsMesazh(false, "Llogaria nuk ekziston!");
            clsDatabaseShare dbshare = new clsDatabaseShare(db );
            //clsKusht kushtgj = new clsKusht(IdKonfigAmbjente, "LLN", dbshare);
            //clsAlternativaKushti alt = new clsAlternativaKushti(kushtgj.Vlera, dbshare);
            foreach (clsTrupiNdryshimCmimSasi trup in ocolTrupiNdryshimCmimSasi)
            {
                if (ocolTrupiNdryshimCmimSasi.FindAll(x => x.IdArtikulli == trup.IdArtikulli).Count > 1)
                    return new clsMesazh(false, "Artikulli " + trup.KodiArtikull + " ndodhet dy here ne gride!");
                if (clsAlternativaKushti.getAlternativa(IdKonfigAmbjente, "LLN", dbshare) == "Ndryshim cmimi" && trup.SasiaGjendje == 0 && trup.CmimiRi != 0)
                    return new clsMesazh(false, "Nuk mund te ndryshohet cmimi per nje artikull qe nuk ka gjendje");
            }
            return new clsMesazh(true, "Kontrollet  u kaluan me sukses!");
        }

        public clsMesazh krijoNdryshimCmimSasiPerImport(string kodNiv, string kodKonf, string kodMag, DateTime dtDk, string nrDk, int idLidhes, double vl, int idSt, int idNder, int idNdVt, int idPer, DateTime dtRegj, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues, string kodDegeAdministrative, string nrllogari, string grup1, string grup2, string grup3, colTrupiNdryshimCmimSasi coltrupi, clsKokaFleteKontabel fletekont, clsKonfigurimAmbjenti konfigAmbjenti, ref bool gjeneroDokMag, DbShare.clsKonfigurimAmbjenti konfmag, out string mesazhinformues)
        {
            mesazhinformues = "";
            int idNivel;
            if (kodNiv == "")
                throw new Exception("Nenkategoria nuk mund te jete bosh!");
            clsNivelRegjistrimi nivelRegj = new clsNivelRegjistrimi();
            nivelRegj.Kodi = kodNiv; nivelRegj.IdNdermarje = idNder;
            nivelRegj.merrNivelRegjSipasKodi();
            idNivel = nivelRegj.IdNivel;

            if (kodKonf == "")
                throw new Exception("Lloji i dokumentit nuk mund te jete bosh!");


            clsNjesiAdministrative mag = new clsNjesiAdministrative(kodMag, idNder, idPer);
            idMagazina = mag.IdNjesiAdministrative;

            clsDegeAdministrative deg = new clsDegeAdministrative(kodDegeAdministrative, idNder);
            idDegeAdministrative = deg.IdDegeAdministrative;

            DbKontabiliteti.clsLlogari llog = new clsLlogari(nrllogari, idNder);
            idLlogKunderparti = llog.IdLlogari;

            int idgrup1 = 0, idgrup2 = 0, idgrup3 = 0;
            if (grup1 != "")
            {
                if (!clsGrupimDokumentiKoka.ekzistonGrup(grup1, idNder, 1))
                    return new clsMesazh(false, "Grupimi i pare i dokumentit nuk ekziston!");
                else
                {
                    clsGrupimDokumentiKoka grupimi1 = new clsGrupimDokumentiKoka(grup1, idNder, 1, idPer);
                    idGrup1 = grupimi1.IdGrupimKoka;
                }
            }

            if (grup2 != "")
            {
                if (!clsGrupimDokumentiKoka.ekzistonGrup(grup2, idNder, 2))
                    return new clsMesazh(false, "Grupimi i dyte i dokumentit nuk ekziston!");
                else
                {
                    clsGrupimDokumentiKoka grupimi2 = new clsGrupimDokumentiKoka(grup2, idNder, 2, idPer);
                    idGrup2 = grupimi2.IdGrupimKoka;
                }
            }

            if (grup3 != "")
            {
                if (!clsGrupimDokumentiKoka.ekzistonGrup(grup3, idNder, 3))
                    return new clsMesazh(false, "Grupimi i trete i dokumentit nuk ekziston!");
                else
                {
                    clsGrupimDokumentiKoka grupimi3 = new clsGrupimDokumentiKoka(grup3, idNder, 3, idPer);
                    idGrup3 = grupimi3.IdGrupimKoka;
                }
            }

            return krijoNdryshimCmimSasi(0, idNivel, konfigAmbjenti.IdKonfigAmbjente, idMagazina, kodMag, dtDk, nrDk, idLidhes, vl, idSt, idNder, idNdVt, idPer, idPer, dtRegj, 0, 0, 0, idDegeAdministrative, kodDegeAdministrative, idLlogKunderparti, nrllogari, idgrup1, idgrup2, idgrup3, "", coltrupi, fletekont, new clsDatabaseRegjistrim(), false, ref gjeneroDokMag, konfmag, out mesazhinformues);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool eshteILidhur()
        {
            DbAdmin.clsDatabaseAdmin dbAdmin = new DbAdmin.clsDatabaseAdmin();
            bool lidhur = dbAdmin.eshteDokumentiILidhur(idKoka, idNivel, "T_KOKANDRYSHIMCMIMSASI", "IDKOKA");
            dbAdmin.Dispose();
            return lidhur;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable merrIdsDokLidhur()
        {
            DbAdmin.clsDatabaseAdmin dbAdmin = new DbAdmin.clsDatabaseAdmin();
            DataTable dt = dbAdmin.MerrDokLidhur(idKoka, idNivel, "T_KOKANDRYSHIMCMIMSASI", "IDKOKA");
            dbAdmin.Dispose();
            return dt;
        }


        /// <summary>
        /// Ruan nje objekt dokumenti magazine sebashku me trupin  dhe kontabilitetin perkates
        /// Nje objekt koka dokumenti magazine ka nje koleksion me trupin e dokumentit  dhe kontabilitetin perkates , 
        /// ruajtja e nje dokumenti imponon ruajtjen edhe te nje colection-i me trupin dhe kontabilitetin kur kontabilizohet
        /// Mqs cdo rresht i ri qe shtohet ne DB kerkon thirrjen e nje SP-je me parametra dhe koka e dokumentit bashke me trupin dhe kontabilitetin konsiderohet si nje regjistrim,
        /// perdoret nje transaksion qe imponon regjistrimin e rregullt te nje te nje dokumenti magazine sebashku me trupin dhe kontabilitetin perkates
        /// ne rastin e flete daljeve behet kontrolli i gjendjes nqs eshte zgjedhur kontrolli i gjendes tek regjistrimi i artikullit dhe nuk lejohet transaksioni nqs gjendja ne magazine eshte me e vogel sesa gjendja qe duhet te dale
        /// ne rastet e transferimit ruhet edhe nje dokument tjeter hyrje per transferimin ne magazinen e dyte
        /// dokumentat e te njejtes date renditen sipas kohes kur jane ruajtur
        /// </summary>
        /// <param name="eshteTrasferim"> tregon nqs dokumenti eshte transferim ne menyre qe te kryhet ruajtja e dokumentit te dyte te transferimit</param>
        /// <param name="dtDk"> data e dokumentit te magazines</param>
        /// <param name="dtRegj"> data e regjistrimit te dokumentit te magazines</param>
        /// <param name="idKDok"> id kategorise se dokumentit me te cilin lidhet.</param>
        /// <param name="idKlFurn"> id e klient furnitorit me te cilin lidhet</param>
        /// <param name="idKonf"> id e konfigurimit te dokumentit</param>
        /// <param name="idLidhes">id e dokumentit nga e cila gjenerohet ne rastet e modifikimit </param>
        /// <param name="idllojdokmag"> id e llojit te dokumentit</param>
        /// <param name="idMag">id e magazines</param>
        /// <param name="idKoka">id ritese e kokes se magazines</param>
        /// <param name="idNder"> id e ndermarjes</param>
        /// <param name="idNdVt"> id e ndermarje vitit</param>
        /// <param name="idNiv"> id e nivelit te regjistrimit</param>
        /// <param name="idPer"> id e perdoruesit se e ben regjistrimin</param>
        /// <param name="idProj"> id e projektit ne te cilin ben pjese</param>
        /// <param name="idrenditjes"> id e renditjes per te renditur dokumentat e te njejtes date sipas kohes se regjistrimit</param>
        /// <param name="idSt"> id e statusit te dokumentit</param>
        /// <param name="nrDk">nr i dokumentit</param>
        /// <param name="nrProj"> nr i projektit</param>
        /// <param name="shenim"> shenime </param>
        /// <param name="vl"> vlera totale e dokumentit</param>
        /// <param name="idGjenerues">id e dokumentit nga gjenerohet magazina nga nje ambjent tjeter</param>
        /// <param name="idKonfigGjenerues">id e konfigurimit nga gjenerohet dokumenti</param>
        /// <param name="idNivelGjenerues">id e nivelit nga gjenerohet dokumenti</param>
        /// <param name="ocolTrupiNdryshimCmimSasi">kolektion i trupit te magazines</param>
        /// <param name="oFleteKontabel">kolektion i fleteve kontabel</param>
        /// <param name="oMagazinaTransferim">kolektion i transferimit ne magazine</param>
        /// <param name="meKontabilizim"> tregon nese dokumenti i magazines do te kontabilizohet apo jo</param>
        /// <param name="iddoktransferimi"> id e dokumentit te transferimit</param>
        /// <param name="idgrup1">id e grupimit te pare </param>
        /// <param name="idgrup2">id e grupimit te dyte</param>
        /// <param name="idgrup3">id e grupimit te trete</param>
        /// <returns> kthen nje obj clsMesazh per te identifikuar statusin e ruajtjes se te dhenave ne DB</returns>
        /// <param name="pershkrimDokKontabiliteti"></param>
        /// <param name="idLlojDok"></param>
        /// <param name="idDokNga"></param>
        /// <param name="kontabilizioamortizim"> perdoret per rastet kur kemi kontabilizim magazine brenda shitjes dhe nuk kontabilizohet magazina por duhet te kontabilizohet amortizimi</param>
        /// <param name="mosLlogaritAmortizimShtese"></param>

        public clsMesazh ruajNdryshimCmimSasi(out int idKoka, int idNiv, int idKonf, int idMag, DateTime dtDk, string nrDk,
            int idLidhes, double vl, int idSt, int idNder, int idNdVt, int idPer, int idkrijues, DateTime dtRegj, int idNivelGjenerues,
            int idKonfigGjenerues, int idGjenerues, int iddegeadministrative, colTrupiNdryshimCmimSasi ocolTrupiNdryshimCmimSasi, clsKokaFleteKontabel oFleteKontabel, int meKontabilizim, int idPeriudha, clsDatabaseRegjistrim dbRegj, string pershkrimDokKontabiliteti, int idLlojDok,
            int idDokNgaFK, int idllogari, int idgrup1, int idgrup2, int idgrup3, string pershkrimi,
            out string shfaqmesazhapolupe, int iddokngaQKFK, DbQendraKosto.colTrupiQendraKosto trupivjeterqendra, bool eshteOwn,
           clsKokaNdryshimCmimSasi koka, DbQendraKosto.colTrupiQendraKosto colTrupiQendramagvejter, out  string shfaqmesazhapolupemag, bool gjenerodokmagazine, ResourceManager rm, CultureInfo ci)
        {
            //transaksioni per te ruajtur 

            clsMesazh mesazh;
            DbCore.clsMesazh kontMesazh = new DbCore.clsMesazh(true);
            shfaqmesazhapolupe = "jo";
            shfaqmesazhapolupemag = "jo";
            idKoka = 0;
            try
            {


                idKoka = dbRegj.ruajKokaNdryshimCmimSasi(idKoka, idNiv, idKonf, idMag, dtDk, nrDk, idLidhes, vl, idSt, idNder, idNdVt, idPer,
                    dtRegj, idNivelGjenerues, idKonfigGjenerues, idGjenerues, iddegeadministrative, idllogari, idgrup1, idgrup2, idgrup3, pershkrimi, idkrijues);

                if (idKoka == 0)
                    return new clsMesazh(false, "Ndodhi një Gabim gjatë ruajtjes së Kokës së ndryshim cmim sasi");

                #region trupi
                DbInventari.clsDatabaseInventari dbinv = new DbInventari.clsDatabaseInventari(dbRegj );
                double vleftallog = 0;
                foreach (clsTrupiNdryshimCmimSasi o in ocolTrupiNdryshimCmimSasi)
                {//behet ruajtja e trupit 
                    o.IdKoka = idKoka;

                    int idM;
                    DbInventari.clsArtikulli artikulli = new DbInventari.clsArtikulli();

                    artikulli.mbushArtikull(o.IdArtikulli, dbinv);
                    vleftallog += (o.VleftaRe - o.VleftaGjendje);

                    mesazh = dbRegj.ruajTrupiNdryshimCmimSasi(out idM, o.IdKoka, o.IdArtikulli, o.IdNjesia, o.SasiaGjendje, o.VleftaGjendje, o.CmimiGjendje, o.SasiaRe, o.VleftaRe, o.IdMag, o.CmimiRi);
                    if (!mesazh.Status)
                    {

                        return mesazh;
                    }


                }
                #endregion
                #region magazina
                if (gjenerodokmagazine)
                {

                    if (koka.OKokaMagazina.OFleteKontabel.IdDokNga == 0)
                        idDokNgaFK = -1;
                    else
                        idDokNgaFK = koka.OKokaMagazina.OFleteKontabel.IdDokNga;

                    koka.OKokaMagazina.NrDok = nrDk;
                    koka.OKokaMagazina.IdGjenerues = idKoka;
                    clsDatabaseShare dbshare = new clsDatabaseShare(dbRegj );
                    //KEVI testim
                    string pershkrimMagFK;
                    if (koka.OKokaMagazina.Shenime != String.Empty)
                    {
                        pershkrimMagFK = koka.OKokaMagazina.Shenime;
                    }
                    else
                        pershkrimMagFK = "Nga ndryshim sasi/cmim";

                    bool gjithmone = false;
                    //clsKusht kushtgj = new clsKusht(koka.oKokaMagazina.IdKonfigAmbjente, "GJKGJ", dbshare);
                    //clsAlternativaKushti alt = new clsAlternativaKushti(kushtgj.Vlera, dbshare);
                    if (clsAlternativaKushti.getAlternativa(koka.oKokaMagazina.IdKonfigAmbjente, "GJKGJ", dbshare) == "Po")
                        gjithmone = true;
                    int kontabalizimmag = 0;
                    string mesazhmevonshem = "";
                    mesazh = koka.OKokaMagazina.ruaj(false, kontabalizimmag, null, idPeriudha, pershkrimMagFK, idLlojDok, idDokNgaFK, dbRegj, out shfaqmesazhapolupemag, koka.oKokaMagazina.OFleteKontabel.KokaQendraKosto.IdDokNga, colTrupiQendramagvejter, eshteOwn, new colSerialetMagazine(), new DbAsete.colSerialetMagazine(), new clsKonfigurimAmbjenti(), new DbShare.clsKonfigurimAmbjenti(), 0, 0, new DbQendraKosto.colTrupiQendraKosto(), false, new clsKokaShitje(), false, gjithmone, new colAmortizimiKoka(), 0, false, false, new colTrupiMagazina(), new colAmortizimiKoka(), new int[0],false,false,false, out mesazhmevonshem, null, false, false);

                    if (!mesazh.Status)
                        return mesazh;

                }
                #endregion

                if (idSt == 1 && meKontabilizim != 0)
                {//nese eshte dalje fleta kontabel krijohet ketu pasi vlefta percaktohet sipas cmimit mesatar me progresiv

                    clsDatabaseKontabilitet dbkont = new clsDatabaseKontabilitet(dbRegj );
                    DbQendraKosto.colObjektivaKosto objektivat;
                    List<double> vleratobjektiva; List<double> vleratobjektivamonbaze;
                    List<int> idllogobj;
                    oFleteKontabel = DbKontabiliteti.clsKokaFleteKontabel.gjeneroKontabilizimNdryshimCmimSasi(idKoka, idNiv, idKonf, dtDk, nrDk, vleftallog, idNder, idNdVt, idPer, dtRegj, ocolTrupiNdryshimCmimSasi, pershkrimDokKontabiliteti, idDokNgaFK, idLlojDok, idPeriudha, idllogari, 95, out objektivat, out vleratobjektiva, out vleratobjektivamonbaze, out idllogobj, iddegeadministrative, 0, 0, out  shfaqmesazhapolupe, iddokngaQKFK, trupivjeterqendra, dbkont, rm, ci);
                    if (oFleteKontabel.OColTrupi.Count > 0)
                    {
                        oFleteKontabel.IdGjenerues = idKoka;
                        oFleteKontabel.NrDukumentiKokaFleteKontabel = nrDk;

                        if (meKontabilizim == 1)
                            oFleteKontabel.Kontabilizuar = true;
                        else
                            oFleteKontabel.Kontabilizuar = false;
                        kontMesazh = oFleteKontabel.Ruaj(dbkont);
                        if (!kontMesazh.Status)
                        {

                            return new clsMesazh(kontMesazh.Status, kontMesazh.PershkrimMesazhi);
                        }
                    }
                }


                mesazh = new clsMesazh(true, "Ruajtja përfundoi me sukses!");
                return mesazh;
            }
            catch (Exception ce)
            {

                return new clsMesazh(false, ce.Message);
            }
        }

        /// <summary>
        /// Ruan objektin e  kokes se dokumentit te magazines ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsKokaMagazine.ruajMagazina"/> 
        /// </summary>
        /// <param name="eshteTransferim">tregon nese dokumenti i regjistruar eshte transferim apo jo</param>
        /// <param name="meKontabilizim">tregon nese te konfigurimet magazina eshte me kontabilizim apo jo</param>
        /// <param name="hfNrAutoregjistrime">hiddenField i Devit qe mban numrat automatike,null nese nuk perodren</param>
        /// <param name="idPeriudha"></param>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
        public clsMesazh ruaj(int meKontabilizim, IDictionary<string, object> hfNrAutoregjistrime, int idPeriudha, string pershkrimFK, out string shfaqmesazhapolupe, bool eshteOwn, out  string shfaqmesazhapolupemag, bool gjenerodokmagazine, ResourceManager rm, CultureInfo ci)
        {
            DbShare.clsKonfigurimAmbjenti konf = new DbShare.clsKonfigurimAmbjenti();
            konf.mbushKonfiguriminMeID(this.IdKonfigAmbjente);

            int idLlojDokFK;
            if (konf != null)
                idLlojDokFK = 51;
            else
                idLlojDokFK = -1;
            int idDokNgaFK = -1;
            clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();
            //db.krijoManager();
            db.beginTransaksion();
            clsMesazh u_ruajt = ruaj(meKontabilizim, hfNrAutoregjistrime, idPeriudha, pershkrimFK, idLlojDokFK, idDokNgaFK, db, out shfaqmesazhapolupe, 0, new DbQendraKosto.colTrupiQendraKosto(), eshteOwn, out shfaqmesazhapolupemag, gjenerodokmagazine, rm, ci); //perdor ruajtjen me transaksion

            if (!u_ruajt.Status)
            {
                db.rollbackTransaksion();
                return u_ruajt;
            }
            db.commitTransaksion();
            return u_ruajt;
        }

        /// <summary>
        /// Ruan objektin e  kokes se dokumentit te magazines ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsKokaMagazine.ruajMagazina"/> 
        /// </summary>
        /// <param name="eshteTransferim">tregon nese dokumenti i regjistruar eshte transferim apo jo</param>
        /// <param name="meKontabilizim">tregon nese te konfigurimet magazina eshte me kontabilizim apo jo</param>
        /// <param name="hfNrAutoregjistrime">hiddenField i Devit qe mban numrat automatike,null nese nuk perodren</param>
        /// <param name="idPeriudha"></param>
        /// <param name="kontabilizioamortizim"> perdoret per rastet kur kemi kontabilizim magazine brenda shitjes dhe nuk kontabilizohet magazina por duhet te kontabilizohet amortizimi</param>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
        public clsMesazh ruaj(int meKontabilizim, IDictionary<string, object> hfNrAutoregjistrime, int idPeriudha, string pershkrimFK, int idLlojDokFK, int idDokNgaFK, clsDatabaseRegjistrim db, out string shfaqmesazhapolupe, int iddokngaqk, DbQendraKosto.colTrupiQendraKosto trupivjeterqendra, bool eshteOwn, out  string shfaqmesazhapolupemag, bool gjenerodokmagazine, ResourceManager rm, CultureInfo ci)
        {
            shfaqmesazhapolupemag = "";

            DbShare.clsDatabaseShare dbshare = new DbShare.clsDatabaseShare(db );


            bool kaNdryshimNumri;
            clsMesazh mesazhKontrolli = kontrolloNdryshimCmimSasi(out kaNdryshimNumri, db, hfNrAutoregjistrime); //TODO KEVI duhet hequr kontrolli qe ekziston doku apo jo per dokumentat e gjeneruar nga shitja
            if (!mesazhKontrolli.Status)
            {
                shfaqmesazhapolupe = "jo";
                //db.rollbackTransaksion();
                return mesazhKontrolli;
            }
            int idkoka = 0;
            clsMesazh u_ruajt = this.ruajNdryshimCmimSasi(out idkoka, this.IdNivel, this.IdKonfigAmbjente, this.IdMagazina, this.DtDok, this.NrDok, this.IdDokNga, this.Vlefta, this.IdStatusDok, this.IdNdermarje, this.idNderVit, this.IdPerdoruesi, this.idKrijues,
                               this.DtRegjistrimi, this.IdNivelGjenerues, this.IdKonfigGjenerues, this.IdGjenerues, this.IdDegeAdministrative, this.OcolTrupiNdryshimCmimSasi, this.OFleteKontabel, meKontabilizim, idPeriudha, db, pershkrimFK, idLlojDokFK, idDokNgaFK, this.idLlogKunderparti, this.IdGrup1, this.IdGrup2, this.IdGrup3, this.Pershkrimi, out shfaqmesazhapolupe, iddokngaqk, trupivjeterqendra, eshteOwn, this, new DbQendraKosto.colTrupiQendraKosto(), out shfaqmesazhapolupemag, gjenerodokmagazine, rm, ci);
            this.idKoka = idkoka;
            if (!u_ruajt.Status)
            {
                //db.rollbackTransaksion();
                return u_ruajt;
            }
            //db.commitTransaksion();
            if (kaNdryshimNumri)
                return mesazhKontrolli;
            return u_ruajt;
        }

        private clsMesazh kontrolloNdryshimCmimSasi(out bool kaNdryshimNumri, clsDatabaseRegjistrim dbRegj, IDictionary<string, object> hfNrAutoregjistrime)
        {
            kaNdryshimNumri = false;
            if (this.ocolTrupiNdryshimCmimSasi.Count == 0)
                return new clsMesazh("Trupi ndryshim cmim sasi nuk mund te jete bosh!");
            foreach (clsTrupiNdryshimCmimSasi t in this.ocolTrupiNdryshimCmimSasi)
            {
                if (t.KodiArtikull != "" && t.PershkrimArtikull == "")
                {
                    return new clsMesazh("Te dhënat nuk janë të sakta");
                }
                if (t.KodiArtikull != "" && t.IdNjesia == 0)
                {
                    return new clsMesazh("Te dhënat nuk janë të sakta");
                }
                if (t.KodiArtikull != "" && t.IdMag == 0)
                {
                    return new clsMesazh("Të dhënat nuk janë të sakta");
                }
            }

            clsDatabaseAdmin db = new clsDatabaseAdmin(dbRegj );
            clsMesazh mes = new clsMesazh();

            if (hfNrAutoregjistrime != null)
            {
                mes = kontrolloNrAutoNdryshimCmimSasi(out kaNdryshimNumri, dbRegj, hfNrAutoregjistrime);
                if (!mes.Status)
                    return mes;
            }
            if (dbRegj.ekzistonRegjistrimNdryshimCmimSasi(idKonfigAmbjente, nrDok, idMagazina, dtDok, idNdermarje))
                return new clsMesazh(false, "Ekziston një regjistrim me të njëjtin numër dokumenti!");
            if (kaNdryshimNumri)
                return mes;
            return new clsMesazh(true, "Kontrolli i ndryshim cmim sasi u krye me sukses!");
        }

        private clsMesazh kontrolloNrAutoNdryshimCmimSasi(out bool kaNdryshimNumri, clsDatabaseRegjistrim dbRegj, IDictionary<string, object> hfregjistrime)
        {
            clsDatabaseAdmin db = new clsDatabaseAdmin(dbRegj );
            List<NrAuto> list = DbAdmin.clsNrAutom.kontrollogjithenumrat(db, hfregjistrime, this.dtDok);
            if (NrAuto.ktheVlerenEre(list, "NrDok") != "")
                this.nrDok = NrAuto.ktheVlerenEre(list, "NrDok");

            DbCore.clsMesazh mes = NrAuto.ruajvlera(out kaNdryshimNumri, list, this.dtDok, this.idPerdoruesi, this.idNdermarje, db);
            return new clsMesazh(mes.Status, mes.PershkrimMesazhi);
        }

        /// <summary>
        /// Modifikon nje objekt dokument magazine sebashku me te trupin dhe kontabilitetin
        /// Nje objekt dokument magazine ka nje koleksion me trupin dhe kontabilitetin perkates , 
        /// modifikimi e nje dokumenti imponon modifikimin edhe te nje colection-i me trupin dhe kontabilitetin
        /// Mqs cdo rresht i ri qe modifikon ne DB kerkon thirrjen e nje SP-je me parametra dhe dokumenti i magazines bashke me trupin dhe kontabilitetin konsiderohet si nje regjistrim,
        /// perdoret nje transaksion qe imponon modifikimin e rregullt te nje te nje dokumenti magazine sebashku me trupin dhe kontabilitetin
        /// 1. merret dokumenti eksistues  i magazines dhe kalohet ne gjendjen 20 te modifikimit
        /// 2. ruhet dokumenti i ri i magzines se bashku me trupin dhe kontabilitetin
        /// 3. stornohen kontabiliteti i dokumentave eksistues  te magazines
        /// </summary>
        /// <param name="transferim"> tregon nqs dokumenti eshte transferim ne menyre qe te kryhet ruajtja e dokumentit te dyte te transferimit</param>
        /// <param name="dtDk"> data e dokumentit te magazines</param>
        /// <param name="dtRegj"> data e regjistrimit te dokumentit te magazines</param>
        /// <param name="idKDok"> id kategorise se dokumentit me te cilin lidhet.</param>
        /// <param name="idKlFurn"> id e klient furnitorit me te cilin lidhet</param>
        /// <param name="idKonf"> id e konfigurimit te dokumentit</param>
        /// <param name="idLidhes">id e dokumentit nga e cila gjenerohet ne rastet e modifikimit </param>
        /// <param name="idllojdokmag"> id e llojit te dokumentit</param>
        /// <param name="idMag">id e magazines</param>
        /// <param name="idKoka">id ritese e kokes se magazines</param>
        /// <param name="idNder"> id e ndermarjes</param>
        /// <param name="idNdVt"> id e ndermarje vitit</param>
        /// <param name="idNiv"> id e nivelit te regjistrimit</param>
        /// <param name="idPer"> id e perdoruesit se e ben regjistrimin</param>
        /// <param name="idProj"> id e projektit ne te cilin ben pjese</param>
        /// <param name="idrenditjes"> id e renditjes per te renditur dokumentat e te njejtes date sipas kohes se regjistrimit</param>
        /// <param name="idSt"> id e statusit te dokumentit</param>
        /// <param name="nrDk">nr i dokumentit</param>
        /// <param name="nrProj"> nr i projektit</param>
        /// <param name="shenim"> shenime </param>
        /// <param name="vl"> vlera totale e dokumentit</param>
        /// <param name="idGjenerues">id e dokumentit nga gjenerohet magazina nga nje ambjent tjeter</param>
        /// <param name="idKonfigGjenerues">id e konfigurimit nga gjenerohet dokumenti</param>
        /// <param name="idNivelGjenerues">id e nivelit nga gjenerohet dokumenti</param>
        /// <param name="ocolTrupiNdryshimCmimSasi">kolektion i trupit te magazines</param>
        /// <param name="oFleteKontabel">kolektion i fleteve kontabel</param>
        /// <param name="oMagazinaTransferim">kolektion i transferimit ne magazine</param>
        /// <param name="idgrup1">id e grupimit te pare </param>
        /// <param name="idgrup2">id e grupimit te dyte</param>
        /// <param name="idgrup3">id e grupimit te trete</param>
        /// <param name="meKontabilizim"> tregon nese dokumenti i magazines do te kontabilizohet apo jo</param>
        /// <returns> kthen nje obj clsMesazh per te identifikuar statusin e modifikimit se te dhenave ne DB</returns>
        /// <param name="mosLlogaritAmortizimShtese"></param>
        public clsMesazh modifikoNdryshimCmimSasi(int idKoka, int idNiv, int idKonf, int idMag, DateTime dtDk, string nrDk,
            int idLidhes, double vl, int idSt, int idNder, int idNdVt, int idPer, int idkrijues, DateTime dtRegj, int idNivelGjenerues,
            int idKonfigGjenerues, int idGjenerues, int iddegeadministrative, int idllogari, colTrupiNdryshimCmimSasi ocolTrupiNdryshimCmimSasi, clsKokaFleteKontabel oFleteKontabel, int meKontabilizim, string pershkrimFK, int idgrup1, int idgrup2, int idgrup3, string pershkrimi, clsDatabaseRegjistrim dbRegj, out  string shfaqmesazhapolupe, out int idkokare, bool eshteOwn, clsKokaNdryshimCmimSasi koka, DbQendraKosto.colTrupiQendraKosto trupivjeterqendra, out string shfaqmesazhapolupemag, bool gjenerodokmag, ResourceManager rm, CultureInfo ci)
        {
            idkokare = 0;
            shfaqmesazhapolupe = "jo"; shfaqmesazhapolupemag = "jo";
            colTrupiNdryshimCmimSasi trupat = new colTrupiNdryshimCmimSasi();
            trupat.mbushTrupiNdryshimCmimSasi(idKoka, dbRegj);

            clsMesazh mesazh;
            try
            {   //obj koka qe i kalohet si parameter eshte si duhet te modifikohet
                clsKokaNdryshimCmimSasi kokaEkzistuese = new clsKokaNdryshimCmimSasi();
                clsDatabaseInventari dbinv = new clsDatabaseInventari(dbRegj );
                kokaEkzistuese.mbushKokaNdryshimCmimSasiSipasID(idKoka, dbRegj);
                if (string.IsNullOrEmpty(kokaEkzistuese.NrDok) || kokaEkzistuese.IdStatusDok == 2)
                {
                    return new clsMesazh(false, rm.GetString("msgDokumentiKaNdryshuarHapeniPerseri", ci));
                }
                kokaEkzistuese.mbushTrupNdryshimCmimSasi(dbRegj);

                kokaEkzistuese.IdStatusDok = 2;  //duhet vendosur nje status i pershtatshem per kete modifikim
                clsKokaMagazina kokaEkzistueseMag = new clsKokaMagazina();
                kokaEkzistuese.OFleteKontabel = new clsKokaFleteKontabel();
                idLidhes = kokaEkzistuese.IdKoka;
                kokaEkzistuese.OKokaMagazina = new clsKokaMagazina();
                kokaEkzistuese.OKokaMagazina.IdGjenerues = kokaEkzistuese.IdKoka;
                kokaEkzistuese.OKokaMagazina.mbushKokaMagazinaSipasIDGjenerues(kokaEkzistuese.OKokaMagazina.IdGjenerues, 1, kokaEkzistuese.IdKonfigAmbjente, dbRegj);
                clsDatabaseKontabilitet dbkontab = new clsDatabaseKontabilitet(dbRegj );
                clsKokaFleteKontabel newclsKokaFleteKontabel = new clsKokaFleteKontabel(kokaEkzistuese.IdKoka, 95, dbkontab);
                DbQendraKosto.clsKokaQendraKosto kokaqendra = new DbQendraKosto.clsKokaQendraKosto();
                if (newclsKokaFleteKontabel.NrDukumentiKokaFleteKontabel != null)
                {
                    kokaEkzistuese.OFleteKontabel = newclsKokaFleteKontabel;

                    DbQendraKosto.clsDatabaseQendraKosto dbqendra = new DbQendraKosto.clsDatabaseQendraKosto(dbkontab );
                    kokaqendra.KtheKokaQKSipasIDGjeneruesDheKonfigMeTrup(newclsKokaFleteKontabel.IdKokaFleteKontabel, newclsKokaFleteKontabel.IdKonfigAmbjente, dbqendra);
                    if (kokaqendra.IdKoka != 0 && kokaqendra.IdKoka != -1)
                    {
                        kokaEkzistuese.OFleteKontabel.KokaQendraKosto = kokaqendra;
                    }
                    else kokaEkzistuese.OFleteKontabel.KokaQendraKosto = new DbQendraKosto.clsKokaQendraKosto();
                }

                if (kokaEkzistuese.OKokaMagazina != null)
                {
                    kokaEkzistueseMag = kokaEkzistuese.OKokaMagazina;
                }
                if (kokaEkzistueseMag.IdKokaMagazina != 0)
                {
                    kokaEkzistueseMag.mbushTrupMagazine(dbRegj);
                    colArtikujt coleksistues1 = new colArtikujt(kokaEkzistueseMag.IdKokaMagazina, dbinv);
                    int i2 = 0;
                    foreach (clsTrupiMagazina trup in kokaEkzistueseMag.OcolTrupiMagazina)
                    {
                        if (trup.IdLlojVeprimi == 1)
                            trup.Element = coleksistues1[i2];
                        i2++;
                    }
                    mesazh = kokaEkzistueseMag.kontrolloGjendjeNeFshirje(dbRegj, koka.OKokaMagazina.OcolTrupiMagazina, koka.OKokaMagazina.IdStatusDok); //TOCHECK
                    
                    if (!mesazh.Status)
                    {
                        return mesazh;
                    }
                    bool kaveprimepas = false;
                    mesazh = kokaEkzistueseMag.fshiMagazina(kokaEkzistueseMag.IdKokaMagazina, idPer, dbRegj,false,false,false, new colSerialetMagazine (), out kaveprimepas,true);
                   
                    if (!mesazh.Status)
                    {
                        return mesazh;
                    }
                    
                    koka.OKokaMagazina.IdDokNga = kokaEkzistueseMag.IdKokaMagazina;
                   
                }
                oFleteKontabel.IdDokNga = kokaEkzistuese.OFleteKontabel.IdKokaFleteKontabel;
                oFleteKontabel.KokaQendraKosto.IdDokNga = kokaEkzistuese.oFleteKontabel.KokaQendraKosto.IdKoka;
                int idDokNgaFK = kokaEkzistuese.OFleteKontabel.IdKokaFleteKontabel;

                mesazh = dbRegj.modifikoKokaNdryshimCmimSasi(kokaEkzistuese.IdKoka, kokaEkzistuese.IdNivel, kokaEkzistuese.IdKonfigAmbjente, kokaEkzistuese.IdMagazina, kokaEkzistuese.DtDok, kokaEkzistuese.NrDok, kokaEkzistuese.Vlefta, kokaEkzistuese.IdStatusDok, kokaEkzistuese.DtRegjistrimi, kokaEkzistuese.IdDegeAdministrative, kokaEkzistuese.idLlogKunderparti, kokaEkzistuese.IdGrup1, kokaEkzistuese.IdGrup2, kokaEkzistuese.IdGrup3, kokaEkzistuese.Pershkrimi,
                    kokaEkzistuese.idPerdoruesi);
                if (!mesazh.Status)
                    return mesazh;


                DbAdmin.clsDatabaseAdmin data = new DbAdmin.clsDatabaseAdmin(dbRegj );
                int idPeriudheDoku = new DbAdmin.clsPeriudhaKontabel(dtDk, idNder, data).IdPeriudha;
                DbShare.clsKonfigurimAmbjenti konf = new DbShare.clsKonfigurimAmbjenti();
                konf.mbushKonfiguriminMeID(idKonf);
                int idLlojDok;
                if (konf != null)
                    idLlojDok = 51;
                else
                    idLlojDok = -1;
                mesazh = ruajNdryshimCmimSasi(out idKoka, idNiv, idKonf, idMag, dtDk, nrDk, idLidhes, vl, idSt, idNder, idNdVt, idPer, idkrijues, dtRegj, idNivelGjenerues, idKonfigGjenerues, idGjenerues, iddegeadministrative, ocolTrupiNdryshimCmimSasi, oFleteKontabel, meKontabilizim, idPeriudheDoku, dbRegj, pershkrimFK, idLlojDok, idDokNgaFK, idllogari, idgrup1, idgrup2, idgrup3, pershkrimi, out shfaqmesazhapolupe, OFleteKontabel.KokaQendraKosto.IdDokNga, kokaqendra.ColTrupi, eshteOwn, koka, trupivjeterqendra, out shfaqmesazhapolupemag, gjenerodokmag, rm, ci);
                idkokare = idKoka;
                if (!mesazh.Status)
                    return mesazh;
                clsDatabaseKontabilitet dbkont = new clsDatabaseKontabilitet(dbRegj );
                if (kokaEkzistuese.OFleteKontabel.IdKokaFleteKontabel != 0)
                {
                    mesazh = kokaEkzistuese.OFleteKontabel.ModifikoFleteKontabel(true, dbkont);
                    if (!mesazh.Status)
                        return mesazh;
                }
                return new clsMesazh(true, rm.GetString("regjMagModifikimiPerfundoiMeSukses", ci));
            }
            catch (Exception ce)
            {
                return new clsMesazh(false, ce.Message);
            }
        }

        /// <summary>
        /// Modifikon objektin e  kokes se dokumentit te magazines ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsKokaNdryshimCmimSasi.modifikoNdryshimCmimSasi"/> 
        /// </summary>
        /// <param name="meKontabilizim">tregon nese te konfigurimet magazina eshte me kontabilizim apo jo</param>
        /// <param name="transferim">nese eshte i transferuar ose jo</param>
        /// <param name="lidhur">tregon nese eshte i lidhur ose jo</param>
        /// <returns > nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo</returns>
        public clsMesazh modifiko(int meKontabilizim, bool lidhur, string pershkrimFK,
            out  string shfaqmesazhapolupe, bool eshteOwn, DbQendraKosto.colTrupiQendraKosto trupivjeterqendra, out  string shfaqmesazhapolupemag, bool gjenerodokmag, ResourceManager rm, CultureInfo ci)
        {
            clsMesazh u_modifikua;

            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            //data.krijoManager();
            shfaqmesazhapolupe = "jo"; shfaqmesazhapolupemag = "jo";
            if (lidhur == false)
            {
                try
                {
                    data.beginTransaksion();
                    int idkokare = 0;
                    u_modifikua = modifikoNdryshimCmimSasi(this.IdKoka, this.IdNivel, this.IdKonfigAmbjente, this.IdMagazina, this.DtDok, this.NrDok, this.IdDokNga, this.Vlefta, this.IdStatusDok, this.IdNdermarje, this.idNderVit, this.IdPerdoruesi, this.idKrijues, this.DtRegjistrimi, this.IdNivelGjenerues, this.IdKonfigGjenerues, this.IdGjenerues, this.IdDegeAdministrative, this.idLlogKunderparti, this.OcolTrupiNdryshimCmimSasi, this.OFleteKontabel, meKontabilizim, pershkrimFK, this.IdGrup1, this.IdGrup2, this.IdGrup3, this.Pershkrimi, data, out shfaqmesazhapolupe, out idkokare, eshteOwn, this, trupivjeterqendra, out shfaqmesazhapolupemag, gjenerodokmag, rm, ci);
                    this.idKoka = idkokare;
                    if (!u_modifikua.Status)
                    {
                        data.rollbackTransaksion();
                        return u_modifikua;
                    }
                    data.commitTransaksion();
                    return u_modifikua;
                }
                catch (Exception)
                {
                    data.rollbackTransaksion();
                    throw;
                }
            }
            data.beginTransaksion();



            u_modifikua = data.modifikoKokaNdryshimCmimSasi(this.IdKoka, this.IdNivel, this.IdKonfigAmbjente, this.IdMagazina, this.DtDok, this.NrDok,
           this.Vlefta, this.IdStatusDok, this.DtRegjistrimi, this.IdDegeAdministrative, this.idLlogKunderparti, this.idGrup1, this.IdGrup2, this.IdGrup3, this.Pershkrimi,
               this.idPerdoruesi);
            if (!u_modifikua.Status)
            {
                data.rollbackTransaksion();
                return u_modifikua;
            }



            data.commitTransaksion();
            return u_modifikua;
        }

        /// <summary>
        /// fshin nje objekt dokument magazine sebashku me te trupin dhe kontabilitetin perkates
        /// Nje objekt dokument magazine ka nje koleksion me trupin dhe kontabilitetin , 
        /// fshirja e nje dokumenti magazine imponon fshirjen edhe te nje colection-i me trupin dhe kontabilitetin
        /// Mqs cdo rresht i ri qe fshihet ne DB kerkon thirrjen e nje SP-je me parametra dhe dokumenti i magazines bashke me trupin dhe kontabilitetin konsiderohet si nje regjistrim,
        /// perdoret nje transaksion qe imponon fshirjen e rregullt te nje te nje dokumenti sebashku me trupin dhe kontabilitetin e tij
        ///1. merr kontabilitetet eksistuese te dokumentit te shitjes dhe magazines dhe i stornon
        ///2. ben fshirjen e trupit dhe me pas te kokes se dokumentit te magazines
        /// </summary>
        ///<param name="idKoka"> koka e dokumentit te magazines i cili do te fshihet</param>
        ///<param name="dbRegj"> clsDatabase regjistrimi kur eshte pjese e nje trasaksioni</param>
        ///<param name="idperdoruesi"> id e perdoruesit qe po kryen veprimin</param>
        /// <returns> kthen nje obj clsMesazh per te identifikuar statusin e fshirjes se te dhenave ne DB</returns>
        public clsMesazh fshiNdryshimCmimSasi(int idKoka, int idperdoruesi, clsDatabaseRegjistrim dbRegj)
        {
            clsMesazh mesazh;
            try
            {
                clsDatabaseKontabilitet dbkontab = new clsDatabaseKontabilitet(dbRegj );
                clsKokaNdryshimCmimSasi kokaEkzistuese = new clsKokaNdryshimCmimSasi();
                clsKokaMagazina kokaEkzistueseMag = new clsKokaMagazina();
                kokaEkzistuese.mbushKokaNdryshimCmimSasiSipasID(idKoka, dbRegj);
                clsKokaFleteKontabel newclsKokaFleteKontabel = new clsKokaFleteKontabel(kokaEkzistuese.IdKoka, 95, dbkontab);///fleta kontabel e dokumentit
                if (newclsKokaFleteKontabel.NrDukumentiKokaFleteKontabel != null)
                    kokaEkzistuese.OFleteKontabel = newclsKokaFleteKontabel;
                else
                    kokaEkzistuese.OFleteKontabel = new clsKokaFleteKontabel();
                kokaEkzistuese.OKokaMagazina = new clsKokaMagazina();
                kokaEkzistuese.OKokaMagazina.IdGjenerues = kokaEkzistuese.IdKoka;
                kokaEkzistuese.OKokaMagazina.mbushKokaMagazinaSipasIDGjenerues(kokaEkzistuese.OKokaMagazina.IdGjenerues, 1, kokaEkzistuese.IdKonfigAmbjente, dbRegj);
               
                if (kokaEkzistuese.OKokaMagazina != null)
                {
                    kokaEkzistueseMag = kokaEkzistuese.OKokaMagazina;
                }
                if (kokaEkzistueseMag.IdKokaMagazina != 0)
                {
                    bool kaveprimepas = false;
                    mesazh = kokaEkzistueseMag.fshiMagazina(kokaEkzistueseMag.IdKokaMagazina, idperdoruesi, dbRegj,false,false,false, new colSerialetMagazine (), out kaveprimepas,true);
                    if (!mesazh.Status)
                    {
                        return mesazh;
                    }
                  
                }
                if (kokaEkzistuese.OFleteKontabel.IdKokaFleteKontabel != 0)/// nqs dokumenti eshte i kontabilizuar kalojme dokumentin me status fshire dhe nqs eshte me stornim krijojme dokumentin e kundert
                {
                    mesazh = kokaEkzistuese.OFleteKontabel.fshiupd(dbkontab);
                    if (!mesazh.Status)
                        return mesazh;
                }

                kokaEkzistuese.IdStatusDok = 2; ///fshijme dokumentin e magazines duke e kaluar me status fshire                
                mesazh = dbRegj.modifikoKokaNdryshimCmimSasi(kokaEkzistuese.IdKoka, kokaEkzistuese.IdNivel, kokaEkzistuese.IdKonfigAmbjente, kokaEkzistuese.IdMagazina, kokaEkzistuese.DtDok, kokaEkzistuese.NrDok, kokaEkzistuese.Vlefta, kokaEkzistuese.IdStatusDok, kokaEkzistuese.DtRegjistrimi, kokaEkzistuese.IdDegeAdministrative, kokaEkzistuese.idLlogKunderparti, kokaEkzistuese.IdGrup1, kokaEkzistuese.IdGrup2, kokaEkzistuese.IdGrup3, kokaEkzistuese.Pershkrimi, idperdoruesi);
                if (!mesazh.Status)
                    return mesazh;

                return new clsMesazh(true, "Fshirja përfundoi me sukses!");

            }
            catch (Exception ce)
            {
                return new clsMesazh(false, ce.Message);
            }
        }


        /// <summary>
        /// Fshin objektin e  kokes se dokumentit te magazines ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.fshiKokaMagazina"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh fshi()
        {
            clsKokaNdryshimCmimSasi data = new clsKokaNdryshimCmimSasi();
            clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim();
            //dbRegj.krijoManager();
            dbRegj.beginTransaksion();
            clsMesazh u_fshi = data.fshiNdryshimCmimSasi(this.IdKoka, this.idPerdoruesi, dbRegj);
            if (u_fshi.Status)
                dbRegj.commitTransaksion();
            else
                dbRegj.rollbackTransaksion();
            return u_fshi;
        }


        /// <summary>
        /// Merr objektin e  kokes se dokumentit te magazines sipas id nga tabela perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.ktheKokaMagazinaSipasID"/> 
        /// </summary>
        /// <returns > nje objekt clsKokaNdryshimCmimSasi qe permban objektin e kerkuar</returns>
        public clsKokaNdryshimCmimSasi merrSipasId()
        {
            clsKokaNdryshimCmimSasi data = new clsKokaNdryshimCmimSasi();
            data.mbushKokaNdryshimCmimSasiSipasID(this.IdKoka);
            return data;
        }

        /// <summary>
        /// Merr objektin e  kokes se dokumentit te magazines sipas nr dhe dt dokumenti nga tabela perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.merrSipasIdNivelNrDokDtDok"/> 
        /// </summary>
        /// <returns > nje objekt clsKokaNdryshimCmimSasi qe permban objektin e kerkuar</returns>
        public clsKokaNdryshimCmimSasi merrSipasIdNivelNrDokDtDok()
        {
            clsKokaNdryshimCmimSasi data = new clsKokaNdryshimCmimSasi(this.IdNivel, this.NrDok, this.DtDok);
            return data;
        }

        /// <summary>
        /// Merr gjithe e  kokat e dokumentave te magazines sipas ndermarjevitit nga tabela perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.ktheGjitheKokaMagazina"/> 
        /// </summary>
        /// <param name="idNdermVit">id e ndermarje vitit</param>
        /// <returns > nje object colKokaNdryshimCmimSasi qe permban nje koleksion me gjithe kokat e dokumentave te magazines te nje ndermarje ne nje vit te caktuar</returns>
        public colKokaNdryshimCmimSasi merriTeGjithe(int idNdermVit)
        {
            colKokaNdryshimCmimSasi data = new colKokaNdryshimCmimSasi(idNdermVit);
            return data;
        }

        /// <summary>
        /// Merr trupin  e  nje dokumenti te magazines nga tabela perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.ktheTrupiMagazina"/> 
        /// </summary>
        /// <returns > nje object colTrupiNdryshimCmimSasi qe permban nje koleksion me trupin e dokumentit te magazines</returns>
        public colTrupiNdryshimCmimSasi merrTrupiNdryshimCmimSasi(clsDatabaseRegjistrim db)
        {
            colTrupiNdryshimCmimSasi trupi = new colTrupiNdryshimCmimSasi();
            trupi.mbushTrupiNdryshimCmimSasi(IdKoka, db);
            return trupi;
        }
        /// <summary>
        /// Merr trupin  e  nje dokumenti te magazines nga tabela perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.ktheTrupiMagazina"/> 
        /// </summary>
        /// <returns > nje object colTrupiNdryshimCmimSasi qe permban nje koleksion me trupin e dokumentit te magazines</returns>
        public bool mbushTrupNdryshimCmimSasi(clsDatabaseRegjistrim db)
        {
            ocolTrupiNdryshimCmimSasi = new colTrupiNdryshimCmimSasi();
            return ocolTrupiNdryshimCmimSasi.mbushTrupiNdryshimCmimSasi(IdKoka, db);
        }
        /// <summary>
        /// Merr trupin  e  nje dokumenti te magazines nga tabela perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.ktheTrupiMagazina"/> 
        /// </summary>
        /// <returns > nje object colTrupiNdryshimCmimSasi qe permban nje koleksion me trupin e dokumentit te magazines</returns>
        public bool mbushTrupNdryshimCmimSasi()
        {
            ocolTrupiNdryshimCmimSasi = new colTrupiNdryshimCmimSasi();
            return ocolTrupiNdryshimCmimSasi.mbushTrupiNdryshimCmimSasi(IdKoka);
        }


        /// <summary>
        /// mbush koken e magazines sipas id dokNga
        /// </summary>
        /// <param name="idDokNga">id dokumenti nga</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        public bool mbushKokaNdryshimCmimSasiSipasIDDokNga(int iddokNga)
        {
            clsDatabaseRegjistrim dbKokaMagazina = new clsDatabaseRegjistrim();
            bool sukses = mbushKokaNdryshimCmimSasi(dbKokaMagazina.ktheKokaNdryshimCmimSasiSipasIDDokNga(iddokNga));
            dbKokaMagazina.Dispose();
            return sukses;
        }

        /// <summary>
        /// mbush koken e magazines sipas id se kokes
        /// </summary>
        /// <param name="idKoka">id e kokes se magazines</param>
        /// <returns>kthen true nese mbushja kryhet me sukses ne te kundert kthen false</returns>
        /// <param name="dbKokaMagazina"></param>
        public bool mbushKokaNdryshimCmimSasiSipasID(int idKoka, clsDatabaseRegjistrim dbKokaMagazina)
        {
            return mbushKokaNdryshimCmimSasi(dbKokaMagazina.ktheKokaNdryshimCmimSasiSipasID(idKoka));
        }
        /// <summary>
        /// mbush koken e magazines sipas id se kokes
        /// </summary>
        /// <param name="idKoka">id e kokes se magazines</param>
        /// <returns>kthen true nese mbushja kryhet me sukses ne te kundert kthen false</returns>
        /// <param name="dbKokaMagazina"></param>
        public bool mbushKokaNdryshimCmimSasiSipasID(int idKoka)
        {
            clsDatabaseRegjistrim dbKokaMagazina = new clsDatabaseRegjistrim();
            bool sukses = mbushKokaNdryshimCmimSasi(dbKokaMagazina.ktheKokaNdryshimCmimSasiSipasID(idKoka));
            dbKokaMagazina.Dispose();
            return sukses;
        }


        public static bool kaAutorizime(int idkoka, int idperdoruesi)
        {
            clsDatabaseRegjistrim dbKokaMagazina = new clsDatabaseRegjistrim();
            bool sukses = dbKokaMagazina.kaAutorizimKokaNdryshimCmimSasi(idkoka, idperdoruesi);
            dbKokaMagazina.Dispose();
            return sukses;
        }

        #endregion


        #region Metoda Internal

        internal bool mbushKokaNdryshimCmimSasi(DataRow rreshti)
        {
            if (rreshti != null)
            {
                try
                {
                    int.TryParse(rreshti["IDKOKA"].ToString(), out idKoka);
                    int.TryParse(rreshti["IDNIVEL"].ToString(), out idNivel);
                    int.TryParse(rreshti["IDKONFIGAMBJENTE"].ToString(), out idKonfigAmbjente);
                    int.TryParse(rreshti["IDMAG"].ToString(), out idMagazina);
                    nrDok = rreshti["NRDOK"].ToString();
                    DateTime.TryParse(rreshti["DTDOK"].ToString(), out dtDok);

                    int.TryParse(rreshti["IDDOKNGA"].ToString(), out idDokNga);
                    double.TryParse(rreshti["VLEFTA"].ToString(), out vlefta);
                    int.TryParse(rreshti["IDGRUP1"].ToString(), out idGrup1);
                    int.TryParse(rreshti["IDGRUP2"].ToString(), out idGrup2);
                    int.TryParse(rreshti["IDGRUP3"].ToString(), out idGrup3);
                    int.TryParse(rreshti["IDSTATUSDOK"].ToString(), out idStatusDok);
                    int.TryParse(rreshti["IDNDERMARJE"].ToString(), out idNdermarje);
                    int.TryParse(rreshti["IDNDERVITI"].ToString(), out idNderVit);
                    int.TryParse(rreshti["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    int.TryParse(rreshti["IDKRIJUES"].ToString(), out idKrijues);
                    DateTime.TryParse(rreshti["DTREGJ"].ToString(), out dtRegj);

                    int.TryParse(rreshti["IDNIVELGJENERUES"].ToString(), out idNivelGjenerues);
                    int.TryParse(rreshti["IDKONFIGGJENERUES"].ToString(), out idKonfigGjenerues);
                    int.TryParse(rreshti["IDGJENERUES"].ToString(), out idGjenerues);
                    int.TryParse(rreshti["IDDEGEADMINISTRATIVE"].ToString(), out idDegeAdministrative);
                    int.TryParse(rreshti["IDLLOGKUNDERPARTI"].ToString(), out idLlogKunderparti);

                    DateTime.TryParse(rreshti["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(rreshti["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    Pershkrimi = rreshti["PERSHKRIMI"].ToString();

                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se kategorive te nivelit te dokumentit nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}