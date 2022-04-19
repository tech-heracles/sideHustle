using System;
using System.Collections.Generic;
using System.Linq;
using DbCore.DbAdmin;
using DbCore.DbKontabiliteti;
using System.Data;
using DbCore.DbInventari;
using DbCore.DbShare;
using System.Globalization;
using System.Resources;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne  koken e nje dokumenti te riparime
    ///  (Te dhenat  merren nga tabela : T_KOKARIPARIME)
    /// </summary>
    public class clsKokaRiparime
    {
        /// <summary>
        /// konstante per mesazhin e gabimit kur merren te dhenat
        /// </summary>
        private const string STR_ERRORGabimGjateMarrjesSeDokumentitTePlanifikimit = "ERROR: Gabim gjate marrjes se dokumentit te riparime nga db-ja";
        /// <summary>
        /// constante qe sasia nuk duhet te jete zero
        /// </summary>
        private const string STR_SasiaNukDuhetTeJeteZero = "Sasia nuk duhet te jete zero";
        /// <summary>
        /// konstante kur planifikimi krijohet me sukses
        /// </summary>
        private const string STR_PlanifikimiUKrijuaMeSukses = "Riparimi u krijua me sukses!";
        /// <summary>
        /// konstante per mesazhin e gabimit kur nr i dokumentit eshte bosh
        /// </summary>
        private const string STR_NumriIDokumentitNukMundTeJeteBosh = "Numri i dokumentit nuk mund te jete bosh";
        /// <summary>
        /// konstante per mesazhin e gabimit kur nuk eshte zgjedhur data e dokumentit
        /// </summary>
        private const string STR_ZgjidhniDatenEDokumentit = "Zgjidhni daten e dokumentit!";
        /// <summary>
        /// konstante per mesazhin e gabimit kur nuk eshte zgjedhur data e regjistrimit
        /// </summary>
        private const string STR_ZgjidhniDatenERegjistrimit = "Zgjidhni daten e regjistrimit!";
        /// <summary>
        /// konstante per mesazhin e gabimit kur klient nuk ekziston
        /// </summary>
        private const string STR_KlientFurnitoriNukEkziston = "Klient/Furnitori nuk ekziston!";
        /// <summary>
        /// konstante per mesazhin  e gabimit kur klienti nuk eshte aktiv
        /// </summary>
        private const string STR_KlientFurnitoriNukEshteAktive = "Klient/Furnitori nuk eshte aktive!";
        /// <summary>
        /// mesazh gabimi kur magazina nuk ekziston
        /// </summary>
        private const string STR_MagazinaNukEkziston = "Dyqani nuk ekziston!";
        /// <summary>
        /// mesazh gabimi kur magazina nuk eshte aktive
        /// </summary>
        private const string STR_MagazinaNukEshteAktive = "Dyqani nuk eshte aktiv!";
        /// <summary>
        /// mesazh kur kontrollet kalohen me sukses
        /// </summary>
        private const string STR_KontrolletUKaluanMeSukses = "Kontrollet  u kaluan me sukses!";
        /// <summary>
        /// konstante per daten bosh 01/01/0100
        /// </summary>
        private const string databosh = "01/01/0100";
        /// <summary>
        /// konstante per mesazhin e gabimit kur nuk ruhet koka e dokumentit
        /// </summary>
        private const string STR_NdodhiNjeGabimGjateRuajtjesSeKokesSePlanifikimit = "Ndodhi nje Gabim gjate ruajtjes se Kokes se planifikimit";
        /// <summary>
        /// konstante per mesazhin e gabimit kur ekziston nje dokument me keto te dhena
        /// </summary>
        private const string STR_EkzistonNjeRegjistrimMeTeNjejtinNumerDokumenti = "Ekziston nje regjistrim me te njejtin numer dokumenti!";
        /// <summary>
        /// mesazh kur kontrollet kalojne me sukses
        /// </summary>
        private const string STR_KontrolliIMagazinesUKryeMeSukses = "Kontrolli i planifikimit u krye me sukses!";
        /// <summary>
        /// konstante per mesazhin e gabimit kur dokumenti ka ndryshuar gjate modifikimit
        /// </summary>
        private const string STR_DokumentiKaNdryshuarJuLutemRihapeniPerseri = "Dokumenti ka ndryshuar! Ju lutem rihapeni perseri!";

        #region Attributet
        /// <summary>
        /// id e kokes se dokumentit te riparimeve
        /// </summary>
        private int idKoka;
        /// <summary>
        /// id e nivelit te regjistrimit
        /// </summary>
        private int idNivel;
        /// <summary>
        /// id e konfigurimit te ambjenteve
        /// </summary>
        private int idKonfigAmbjente;
        /// <summary>
        /// id e garancise
        /// </summary>
        private int idGaranci;
        /// <summary>
        /// id e magazines
        /// </summary>
        private int idMagazina;
        /// <summary>
        /// data e dokumentit
        /// </summary>
        private DateTime dtDok;
        /// <summary>
        /// nr i kontakti
        /// </summary>
        private String nrKontakti;
        /// <summary>
        /// id e dokumentit nga ka ardhur ne rast modifikimi
        /// </summary>
        private int idDokNga;
        /// <summary>
        /// id e statusit te dokumentit
        /// </summary>
        private int idStatusDok;
        /// <summary>
        /// id e nderamrjes
        /// </summary>
        private int idNdermarje;
        /// <summary>
        /// id nderamrje vit
        /// </summary>
        private int idNdermarjeVit;
        /// <summary>
        /// id e perdoruesit
        /// </summary>
        private int idPerdoruesi;
        /// <summary>
        /// data e regjistrimit te dokumentit
        /// </summary>
        private DateTime dtRegj;
        /// <summary>
        /// shenime
        /// </summary>
        private string pershkrimi;
        /// <summary>
        /// id e nivelet te dokumentit qe e ka gjeneruar
        /// </summary>
        private int idNivelGjenerues;
        /// <summary>
        /// id e konfigurimit te dokumentit qe e ka gjeneruar
        /// </summary>
        private int idKonfigGjenerues;
        /// <summary>
        /// id e dokumentit qe e ka gjeneruar
        /// </summary>
        private int idGjenerues;
        /// <summary>
        /// data e krijimit
        /// </summary>
        private DateTime dtKrijimi;
        /// <summary>
        /// data e modifikimit te fundit
        /// </summary>
        private DateTime dtModifikimi;
        /// <summary>
        /// aksesori
        /// </summary>
        private string aksesor;

        /// <summary>
        /// id e statusit te riparimit
        /// </summary>
        private int idStatusRiparimi;
        /// <summary>
        /// id e difektit
        /// </summary>
        private int idDifekti;
        /// <summary>
        /// id e krijuesit
        /// </summary>
        private int idKrijuesi;
        private int dorezuar;
        /// <summary>
        /// koleksioni me trupin e riparimit
        /// </summary>
        private colTrupiRiparime colTrupi;
        private string kodMagazina;
        private clsKokaMagazina oKokaMagazina;
        private clsKokaMagazina oKokaMagazina2;
        private DataRow rreshti;
        private clsDatabaseRegjistrim db;
        #endregion

        #region Properties
        /// <summary>
        /// aksesori
        /// </summary>
        public string Aksesor
        {
            get
            {
                return aksesor;
            }
            set
            {
                aksesor = value;
            }
        }
        /// <summary>
        /// tregon nese artikulli i dhene loan eshte kthyer apo jo ne dyqan
        /// </summary>
        public int Dorezuar
        {
            get
            {
                return dorezuar;
            }
            set
            {
                dorezuar = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdKoka
        {
            get { return idKoka; }
            set { idKoka = value; }
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
        /// Kthen/Vendos ID-ne e garancise.
        /// </summary>
        public int IdGaranci
        {
            get { return idGaranci; }
            set { idGaranci = value; }
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
        /// id e statusit te riparimit
        /// </summary>
        public int IdStatusRiparimi
        {
            get
            {
                return idStatusRiparimi;
            }
            set
            {
                idStatusRiparimi = value;
            }
        }
        /// <summary>
        /// id e difektit
        /// </summary>
        public int IdDifekti
        {
            get
            {
                return idDifekti;
            }
            set
            {
                idDifekti = value;
            }
        }
        /// <summary>
        /// id e krijuesit
        /// </summary>
        public int IdKrijuesi
        {
            get
            {
                return idKrijuesi;
            }
            set
            {
                idKrijuesi = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos nr i kontaktit.
        /// </summary>
        public String NrKontakti
        {
            get { return nrKontakti; }
            set { nrKontakti = value; }
        }
        /// <summary>
        /// Kthen/Vendos dt e dokumentit.
        /// </summary>
        public DateTime DtDok
        {
            get { return dtDok; }
            set { dtDok = value; }
        }
        public clsKokaMagazina OKokaMagazina
        {
            get
            {
                return oKokaMagazina;
            }
            set
            {
                oKokaMagazina = value;
            }
        }
        public clsKokaMagazina OKokaMagazina2
        {
            get
            {
                return oKokaMagazina2;
            }
            set
            {
                oKokaMagazina2 = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos pershkrimi.
        /// </summary>
        public String Pershkrimi
        {
            get { return pershkrimi; }
            set { pershkrimi = value; }
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
        public int IdNdermarjeVit
        {
            get { return idNdermarjeVit; }
            set { idNdermarjeVit = value; }
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
        public DateTime DtRegj
        {
            get { return dtRegj; }
            set { dtRegj = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e nivelit te dokumentit nga eshte gjeneruar 
        /// </summary>
        public int IdNivelGjenerues
        {
            get { return idNivelGjenerues; }
            set { idNivelGjenerues = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e konfigurimit te dokumentit nga eshte gjeneruar 
        /// </summary>
        public int IdKonfigGjenerues
        {
            get { return idKonfigGjenerues; }
            set { idKonfigGjenerues = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e dokumentit nga eshte gjeneruar  nga nje ambjent tjeter
        /// </summary>
        public int IdGjenerues
        {
            get { return idGjenerues; }
            set { idGjenerues = value; }
        }
        /// <summary>
        /// Kthen/Vendos  nje koleksion me trupin e dokumentit 
        /// </summary>
        public colTrupiRiparime ColTrupi
        {
            get { return colTrupi; }
            set { colTrupi = value; }
        }
        /// <summary>
        /// data e krijimit te dokumentit
        /// </summary>
        public DateTime DtKrijimi
        {
            get { return dtKrijimi; }

        }
        /// <summary>
        /// data e modifikimit te fundit te dokumentit
        /// </summary>
        public DateTime DtModifikimi
        {
            get { return dtModifikimi; }

        }

        #endregion

        #region Konstruktoret
        /// <summary>
        /// konstruktori me parametra
        /// </summary>
        /// <param name="idkoka"></param>
        /// <param name="idNiv"></param>
        /// <param name="idKonf"></param>
        /// <param name="idgaranci"></param>
        /// <param name="idMag"></param>
        /// <param name="dtDk"></param>
        /// <param name="nrkontakti"></param>
        /// <param name="idLidhes"></param>
        /// <param name="idSt"></param>
        /// <param name="idNder"></param>
        /// <param name="idNdVt"></param>
        /// <param name="idPer"></param>
        /// <param name="dtRe"></param>
        /// <param name="shenim"></param>
        /// <param name="idNivelGjenerues"></param>
        /// <param name="idKonfigGjenerues"></param>
        /// <param name="idGjenerues"></param>
        /// <param name="idstatusriparimi"></param>
        /// <param name="iddifekti"></param>
        /// <param name="idkrijuesi"></param>
        public clsKokaRiparime(int idkoka, int idNiv, int idKonf, int idgaranci, int idMag, DateTime dtDk, string nrkontakti, int idLidhes, int idSt, int idNder, int idNdVt, int idPer, DateTime dtRe, string shenim, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues, int idstatusriparimi, int iddifekti, int idkrijuesi, int dorezuar)
        {
            idKoka = idkoka;
            idNivel = idNiv;
            idKonfigAmbjente = idKonf;
            idGaranci = idgaranci;
            idMagazina = idMag;
            nrKontakti = nrkontakti;
            dtDok = dtDk;
            idDokNga = idLidhes;
            idStatusDok = idSt;
            idNdermarje = idNder;
            idNdermarjeVit = idNdVt;
            idPerdoruesi = idPer;
            dtRegj = dtRe;
            pershkrimi = shenim;
            this.idNivelGjenerues = idNivelGjenerues;
            this.idKonfigGjenerues = idKonfigGjenerues;
            this.idGjenerues = idGjenerues;
            this.dorezuar = dorezuar;
            idStatusRiparimi = idstatusriparimi;
            idDifekti = iddifekti;
            idKrijuesi = idkrijuesi;
            colTrupi = new colTrupiRiparime();
        }

        /// <summary>
        /// konstruktor me 3 parametra
        /// </summary>
        /// <param name="idNivel">id e nivelit</param>
        /// <param name="nrdok">nr i dokumentit</param>
        /// <param name="dtdok">data e dokumentit</param>
        public clsKokaRiparime(int idNivel, int idgarancia, DateTime dtdok)
        {
            clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();
            mbushKokaRiparime(db.ktheKokaRiparimSipasIdNivelGaranciseDtDok(idNivel, idgarancia, dtdok), db);
            db.Dispose();
        }

        /// <summary>
        /// konstruktori me nje parameter
        /// </summary>
        /// <param name="idkoka">id koka</param>
        public clsKokaRiparime(int idkoka)
        {
            clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();
            mbushKokaRiparime(db.ktheKokaRiparimeSipasID(idkoka), db);
            db.Dispose();
        }

        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsKokaRiparime()
        {
        }

        public clsKokaRiparime(DataRow rreshti, clsDatabaseRegjistrim db)
        {
            
            mbushKokaRiparime(rreshti, db);
        }
        #endregion

        #region Metoda Publike
        /// <summary>
        /// krijon objektin e riparimeve kur nuk eshte i gjeneruar
        /// </summary>
        /// <param name="idNiv"></param>
        /// <param name="idKonf"></param>
        /// <param name="idgarancia"></param>
        /// <param name="aksesor"></param>
        /// <param name="idMag"></param>
        /// <param name="dtDk"></param>
        /// <param name="nrkontakti"></param>
        /// <param name="idSt"></param>
        /// <param name="idNder"></param>
        /// <param name="idNdVt"></param>
        /// <param name="idPer"></param>
        /// <param name="dtRegj"></param>
        /// <param name="shenim"></param>
        /// <param name="idstatusriparimi"></param>
        /// <param name="iddifekti"></param>
        /// <param name="idkrijuesi"></param>
        /// <param name="coltrupi"></param>
        /// <returns></returns>
        public clsMesazh krijoRiparim(int idNiv, int idKonf, int idgarancia, string aksesor, int idMag, DateTime dtDk, string nrkontakti, int idSt, int idNder, int idNdVt, int idPer, DateTime dtRegj, string shenim, int idstatusriparimi, int iddifekti, int idkrijuesi, int dorezuar, string kodmag, DbShare.clsKonfigurimAmbjenti konfmag, bool gjeneroDokMag, string kodniveli, string pershkrimstatusi, DbShare.clsKonfigurimAmbjenti konfmag2, double cmimi, colTrupiRiparime coltrupi)
        {
            return krijoRiparim(idNiv, idKonf, idgarancia, aksesor, idMag, dtDk, nrkontakti, 0, idSt, idNder, idNdVt, idPer, dtRegj, shenim, 0, 0, 0, idstatusriparimi, iddifekti, idkrijuesi, dorezuar, kodmag, konfmag, gjeneroDokMag, kodniveli, pershkrimstatusi, konfmag2, cmimi, coltrupi);
        }

        /// <summary>
        /// krijon objektin e riparimit
        /// </summary>
        /// <param name="idNiv"></param>
        /// <param name="idKonf"></param>
        /// <param name="idGarancia"></param>
        /// <param name="aksesor"></param>
        /// <param name="idMag"></param>
        /// <param name="dtDk"></param>
        /// <param name="nrkontakti"></param>
        /// <param name="iddoknga"></param>
        /// <param name="idSt"></param>
        /// <param name="idNder"></param>
        /// <param name="idNdVt"></param>
        /// <param name="idPer"></param>
        /// <param name="dtRe"></param>
        /// <param name="shenim"></param>
        /// <param name="idNivelGjenerues"></param>
        /// <param name="idKonfigGjenerues"></param>
        /// <param name="idGjenerues"></param>
        /// <param name="idstatusriparimi"></param>
        /// <param name="iddifekti"></param>
        /// <param name="idkrijuesi"></param>
        /// <param name="coltrup"></param>
        /// <returns></returns>
        public clsMesazh krijoRiparim(int idNiv, int idKonf, int idGarancia, string aksesor, int idMag, DateTime dtDk, string nrkontakti, int iddoknga, int idSt, int idNder, int idNdVt, int idPer, DateTime dtRe, string shenim, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues, int idstatusriparimi, int iddifekti, int idkrijuesi, int dorezuar, string kodmag, DbShare.clsKonfigurimAmbjenti konfmag, bool gjeneroDokMag, string kodniveli, string pershkrimstatusi, DbShare.clsKonfigurimAmbjenti konfmag2, double cmimi, colTrupiRiparime coltrup)
        {
            idNivel = idNiv;
            idKonfigAmbjente = idKonf;
            idGaranci = idGarancia;
            this.aksesor = aksesor;
            idMagazina = idMag;
            kodMagazina = kodmag;
            nrKontakti = nrkontakti;
            dtDok = dtDk;
            idDokNga = iddoknga;
            idStatusDok = idSt;
            idNdermarje = idNder;
            idNdermarjeVit = idNdVt;
            idPerdoruesi = idPer;
            dtRegj = dtRe;
            pershkrimi = shenim;
            this.idNivelGjenerues = idNivelGjenerues;
            this.idKonfigGjenerues = idKonfigGjenerues;
            idKrijuesi = idkrijuesi;
            this.dorezuar = dorezuar;
            idDifekti = iddifekti;
            idStatusRiparimi = idstatusriparimi;
            this.idGjenerues = idGjenerues;
            colTrupi = coltrup;
            clsMesazh mesazh = kontrollo(kodniveli, pershkrimstatusi);
            if (!mesazh.Status)
                return mesazh;
            oKokaMagazina = new clsKokaMagazina();
            oKokaMagazina.OFleteKontabel = new clsKokaFleteKontabel();
            oKokaMagazina2 = new clsKokaMagazina();
            oKokaMagazina2.OFleteKontabel = new clsKokaFleteKontabel();
            if (gjeneroDokMag)
            {
                //gjenerohet dokumenti i magazines po pa fleten kontabel te magazines sepse varet nga cmimi i daljes dhe gjenerohet kur ruhet dokumenti i magazines
                if (kodniveli == "RF")
                    mesazh = krijoMagazineNgaRiparimi(ref gjeneroDokMag, this, false, konfmag, kodmag, true, cmimi);
                else
                {
                    if (dorezuar == 1)
                    {
                        mesazh = krijoMagazineNgaRiparimi(ref gjeneroDokMag, this, true, konfmag2, kodmag, true, cmimi);
                    }
                    if (pershkrimstatusi == "Zevendesuar me aparat te ri")
                    {
                        mesazh = krijoMagazineNgaRiparimi(ref gjeneroDokMag, this, false, konfmag, kodmag, false, cmimi);
                    }
                }
                if (!mesazh.Status)
                    return mesazh;
            }
            return new clsMesazh(true, STR_PlanifikimiUKrijuaMeSukses);
        }

        /// <summary>
        /// kontrollon te dhenat e kokes ne jane te sakta
        /// </summary>
        /// <returns>clsMesazh </returns>
        private clsMesazh kontrollo(string kodniveli, string pershkrimstatusi)
        {
            if (idGaranci == 0)
                return new clsMesazh(false, "Garanica nuk duhet te jete bosh!");
            if (dtDok == null || dtDok.ToShortDateString() == databosh)
                return new clsMesazh(false, STR_ZgjidhniDatenEDokumentit);
            if (dtRegj == null || dtRegj.ToShortDateString() == databosh)
                return new clsMesazh(false, STR_ZgjidhniDatenERegjistrimit);
            if (idStatusRiparimi == 0)
                return new clsMesazh(false, "Zgjidhni nje status riparimi");
            if (idDifekti == 0)
                return new clsMesazh(false, "Zgjidhni nje lloj difekti!");
            if (kodMagazina == "")
                return new clsMesazh(false, "Jepni dyqanin!");
            if (kodMagazina != "" && !clsNjesiAdministrative.ekziston(kodMagazina, idNdermarje))
                return new clsMesazh(false, STR_MagazinaNukEkziston);
            clsNjesiAdministrative mag = new clsNjesiAdministrative(kodMagazina, idNdermarje, idPerdoruesi);
            if (mag.IdNjesiAdministrative < 1)
                return new clsMesazh(false, "Nuk keni autorizime ne kete magazine!");
            if (kodMagazina != "" && !mag.Aktiv)
                return new clsMesazh(false, STR_MagazinaNukEshteAktive);
            //if(colTrupi.Count==0)
            //    return new clsMesazh(false, "Nuk mund te ruani dokumentin me trup bosh!");
            foreach (clsTrupiRiparime trupi in colTrupi)
            {
                if (kodniveli == "RF")
                {
                    clsDetajimArtikulli det = new clsDetajimArtikulli(trupi.IdDetLoan);

                    if (det.IdDetajimArtikulli < 1)
                        return new clsMesazh(false, "IMEI nuk ekziston");
                    clsArtikulli art = new clsArtikulli(trupi.IdArtLoan);
                    String kodi = clsArtikulli.ktheKodArtikulliSipasId(trupi.IdArtLoan);
                    if (!clsDetajimArtikulli.ekzistonDetajimLidhurMeArtikullin(det.KodDetajimArtikulli, idNdermarje, art.KodArtikulli, 1))
                        return new clsMesazh(false, "Ky serial nuk i perket ketij artikulli!");
                    if (det.Loan != 1)
                        return new clsMesazh(false, "Ky serial nuk eshte serial loan!");
                    double sasi = clsTrupiMagazina.merrSasi(art, idMagazina, dtDok, det.IdDetajimArtikulli);
                    if (sasi < 1)
                        return new clsMesazh(false, "Nuk ka gjendje per kete serial!");
                }

                if (pershkrimstatusi == "Zevendesuar me aparat te ri")
                {
                    clsDetajimArtikulli det = new clsDetajimArtikulli(trupi.IdDetSwap);
                    clsArtikulli art = new clsArtikulli(trupi.IdArtSwap);
                    if (!clsDetajimArtikulli.ekzistonDetajimLidhurMeArtikullin(det.KodDetajimArtikulli, idNdermarje, art.KodArtikulli, 1))
                        return new clsMesazh(false, "Ky serial nuk i perket ketij artikulli!");
                    if (det.Loan == 1)
                        return new clsMesazh(false, "Ky serial eshte serial loan!");
                    double sasi = clsTrupiMagazina.merrSasi(art, idMagazina, dtDok, det.IdDetajimArtikulli);
                    if (sasi < 1)
                        return new clsMesazh(false, "Nuk ka gjendje per kete serial!");
                }

            }
            return new clsMesazh(true, STR_KontrolletUKaluanMeSukses);
        }

        private clsMesazh krijoMagazineNgaRiparimi(ref bool gjeneroDokMag, clsKokaRiparime koka, bool hyrje_dalje, DbShare.clsKonfigurimAmbjenti konfmag, string kodmag, bool loan, double cmimi)
        {

            string shenime = "";
            if (koka.pershkrimi != String.Empty) shenime = koka.pershkrimi;
            else
            {
                //if (!hyrje_dalje)
                //    shenime = "Nga blerja";
                //else 
                shenime = "Nga riparimet";
            }
            clsDatabaseInventari dbinv = new clsDatabaseInventari();
            colTrupiMagazina coltrupi;

            coltrupi = ruajTrupinEMagazines(koka, hyrje_dalje ? 1 : -1, loan, cmimi, dbinv);
            dbinv.Dispose();
            if (coltrupi.Count == 0)
            {
                gjeneroDokMag = false;
                return new clsMesazh(true, "Dokumenti i riparimeve nuk ka rreshta per te krijuar dokument magazine");
            }

            clsGaranciArtikulli garanci = new clsGaranciArtikulli(koka.idGaranci);
            if (!hyrje_dalje)
                return koka.OKokaMagazina.krijoMagazine(false,koka.oKokaMagazina.IdDokNga, konfmag.IdNivel, konfmag.IdKonfigAmbjente, 0, "", koka.idMagazina, kodmag, koka.DtDok, garanci.Kodi, 0, "", konfmag.IdKategori, koka.oKokaMagazina.IdDokNga, 0, koka.IdStatusDok, koka.idNdermarje, koka.idNdermarjeVit, koka.IdPerdoruesi, koka.DtRegj, (hyrje_dalje) ? 1 : 2, shenime, koka.idNivel, koka.idKonfigAmbjente, koka.idKoka, 0, "", 0, "", 0, "", false, 0, 0, 0, "", "", "", coltrupi, new clsKokaMagazina(), new clsKokaFleteKontabel(), new clsKokaRezervime(), new clsDatabaseRegjistrim(), true, 0, "", 0, false, koka.IdKrijuesi, koka.dtDok, 0, "", "", "", 0, null,false,false,"","",0);
            else return koka.OKokaMagazina2.krijoMagazine(false,koka.oKokaMagazina.IdDokNga, konfmag.IdNivel, konfmag.IdKonfigAmbjente, 0, "", koka.idMagazina, kodmag, koka.DtDok, garanci.Kodi, 0, "", konfmag.IdKategori, koka.oKokaMagazina.IdDokNga, 0, koka.IdStatusDok, koka.idNdermarje, koka.idNdermarjeVit, koka.IdPerdoruesi, koka.DtRegj, (hyrje_dalje) ? 1 : 2, shenime, koka.idNivel, koka.idKonfigAmbjente, koka.idKoka, 0, "", 0, "", 0, "", false, 0, 0, 0, "", "", "", coltrupi, new clsKokaMagazina(), new clsKokaFleteKontabel(), new clsKokaRezervime(), new clsDatabaseRegjistrim(), true, 0, "", 0, false, koka.IdKrijuesi, koka.dtDok, 0, "", "", "", 0, null,false,false,"","",0);
        }
        private colTrupiMagazina ruajTrupinEMagazines(clsKokaRiparime koka, int shenja, bool loan, double cmimi, clsDatabaseInventari dbInv)
        {
            colTrupiMagazina trupat = new colTrupiMagazina();

            foreach (clsTrupiRiparime tsh in koka.colTrupi)
            {

                clsTrupiMagazina trupMag = new clsTrupiMagazina();


                bool krijuar = false;

                clsArtikulli art;
                if (loan) art = new clsArtikulli(tsh.IdArtLoan, dbInv);
                else art = new clsArtikulli(tsh.IdArtSwap, dbInv);
                clsDetajimArtikulli det;
                if (loan) det = new clsDetajimArtikulli(tsh.IdDetLoan, dbInv);
                else det = new clsDetajimArtikulli(tsh.IdDetSwap, dbInv);

                if (art.Klasa != 4)
                    krijuar = trupMag.krijoTrupMagazinaNgaGrida(koka.idNdermarje, koka.DtDok, 1, 1, art.IdArtikulli, art.PershkrimArtikulli, det.IdDetajimArtikulli, art.Njesi1Artikulli, 1, cmimi, cmimi, koka.idMagazina, shenja, 0, art.KodArtikulli, det.KodDetajimArtikulli, "", 0, 0, 0, "",0, dbInv,false, 0, 0);

                else
                {
                    // dbInv.vendosManager(dbregj );                
                    DbInventari.colArtikulliPerberes artper = new DbInventari.colArtikulliPerberes();
                    artper.ktheArtikujPerberesSipasIdArtikullitKryesorDheDatesMeTeAfert(art.IdArtikulli,koka.dtDok, dbInv);
                    foreach (DbInventari.clsArtikulliPerberes aper in artper)
                    {
                        if (aper.Lloji == 1)//artikull
                        {
                            trupMag = new clsTrupiMagazina();

                            int idNjesia = art.Njesi1Artikulli;
                            DbInventari.clsArtikulli a = new DbInventari.clsArtikulli(aper.IdLidheseArt, dbInv);
                            krijuar = trupMag.krijoTrupMagazinaNgaGrida(koka.idNdermarje, koka.dtDok, 1, 1, a.IdArtikulli, a.PershkrimArtikulli, tsh.IdDetLoan, a.Njesi1Artikulli, 1 * (double)aper.Koeficienti * ((art.Njesi1Artikulli == idNjesia) ? 1 : Convert.ToDouble(art.KoeficientArtikulli)), cmimi, (1 * (double)aper.Koeficienti * ((art.Njesi1Artikulli == idNjesia) ? 1 : Convert.ToDouble(art.KoeficientArtikulli))) * cmimi, koka.idMagazina, shenja, 0, a.KodArtikulli, det.KodDetajimArtikulli, "", 0, 0, 0, "",art.IdArtikulli, dbInv,false, 0, 0);

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

        /// <summary>
        /// krijon objektin e dokumentit te planifikimit kur kemi import
        /// </summary>
        /// <param name="idNiv">id e nivelit</param>
        /// <param name="idKonf">id e konfigurimit</param>
        /// <param name="aksesor">kodi i klient furnitorit</param>
        /// <param name="kodMag">kodi i magazines</param>
        /// <param name="dtDk">data e dokumentit</param>
        /// <param name="nrkontakti">nr i dokumentit</param>
        /// <param name="idLidhes">id e dokumentit nga ka ardhur ne modifikim</param>
        /// <param name="idSt">id e statusit te dokumentit</param>
        /// <param name="idNder">id e nderamrjes</param>
        /// <param name="idNdVt">id e nderamrje vitit</param>
        /// <param name="idPer">id perdoruesit</param>
        /// <param name="dtRegj">data e regjistrimit</param>
        /// <param name="shenim">shenime</param>
        /// <param name="idNivelGjenerues">id e nivelit te dokumentit gjenerues</param>
        /// <param name="idKonfigGjenerues">id e konfigurimit te dokumentit gjenerues</param>
        /// <param name="idGjenerues">id e dokumentit gjenerues</param>
        /// <param name="coltrupi">trupi i dokumentit</param>
        /// <param name="idstatusriparimi">id e grupimit te pare </param>
        /// <param name="iddifekti">id e grupimit te dyte</param>
        /// <param name="idkrijuesi">id e grupimit te trete</param>
        /// <returns> clsMesazh</returns>
        public clsMesazh krijoRiparimPerImport(int idNiv, int idKonf, int idgaranci, string aksesor, string kodMag, DateTime dtDk, string nrkontakti, int idLidhes, int idSt, int idNder, int idNdVt, int idPer, DateTime dtRegj, string shenim, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues, int idstatusriparimi, int iddifekti, int idkrijuesi, int dorezuar, string kodniveli, string pershkrimstatusi, colTrupiRiparime coltrupi)
        {


            clsNjesiAdministrative mag = new clsNjesiAdministrative(kodMag, idNder, idPer);
            idMagazina = mag.IdNjesiAdministrative;
            return krijoRiparim(idNiv, idKonf, idgaranci, aksesor, idMagazina, dtDk, nrkontakti, idLidhes, idSt, idNder, idNdVt, idPer, dtRegj, shenim, idNivelGjenerues, idKonfigGjenerues, idGjenerues, idstatusriparimi, iddifekti, idkrijuesi, dorezuar, kodMag, new DbShare.clsKonfigurimAmbjenti(), false, kodniveli, pershkrimstatusi, new DbShare.clsKonfigurimAmbjenti(), 0, coltrupi);
        }

        /// <summary>
        /// kontrollon nese dokumenti eshte i lidhur
        /// </summary>
        /// <returns> true ose false</returns>
        public bool eshteILidhur()
        {
            DbAdmin.clsDatabaseAdmin dbAdmin = new DbAdmin.clsDatabaseAdmin();
            bool lidhur = dbAdmin.eshteDokumentiILidhur(idKoka, idNivel, "T_KOKARIPARIME", "IDKOKA");
            dbAdmin.Dispose();
            return lidhur;
        }

        /// <summary>
        /// merr id e dokumentave qe e kane lidhur
        /// </summary>
        /// <returns>Data table me keto id</returns>
        public DataTable merrIdsDokLidhur()
        {
            DbAdmin.clsDatabaseAdmin dbAdmin = new DbAdmin.clsDatabaseAdmin();
            DataTable dt = dbAdmin.MerrDokLidhur(idKoka, idNivel, "T_KOKARIPARIME", "IDKOKA");
            dbAdmin.Dispose();
            return dt;
        }
        /// <summary>
        /// ruan objektin e riparimit
        /// </summary>
        /// <param name="idkoka"></param>
        /// <param name="idNiv"></param>
        /// <param name="idKonf"></param>
        /// <param name="idgaranci"></param>
        /// <param name="idMag"></param>
        /// <param name="dtDk"></param>
        /// <param name="nrkontakti"></param>
        /// <param name="idLidhes"></param>
        /// <param name="idSt"></param>
        /// <param name="idNder"></param>
        /// <param name="idNdVt"></param>
        /// <param name="idPer"></param>
        /// <param name="dtRegj"></param>
        /// <param name="shenim"></param>
        /// <param name="idNivelGjenerues"></param>
        /// <param name="idKonfigGjenerues"></param>
        /// <param name="idGjenerues"></param>
        /// <param name="idstatusriparimi"></param>
        /// <param name="iddifekti"></param>
        /// <param name="idkrijuesi"></param>
        /// <param name="aksesor"></param>
        /// <param name="ocolTrupi"></param>
        /// <param name="dbRegj"></param>
        /// <param name="modifikim"></param>
        /// <returns></returns>
        public static clsMesazh ruajRiparim(out int idkoka, int idNiv, int idKonf, int idgaranci, int idMag, DateTime dtDk, string nrkontakti, int idLidhes, int idSt, int idNder, int idNdVt, int idPer, DateTime dtRegj, string shenim, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues, int idstatusriparimi, int iddifekti, int idkrijuesi, string aksesor, int dorezuar, colTrupiRiparime ocolTrupi, clsDatabaseRegjistrim dbRegj, bool modifikim, bool gjenerodokmagazine, clsKokaMagazina kokamag, clsKokaMagazina kokamag2, int idPeriudha, out string shfaqmesazhapolupemag, DbQendraKosto.colTrupiQendraKosto colTrupiQendramagvejter, bool eshteOwn, ResourceManager rm, CultureInfo ci)
        {
            idkoka = 0;
            shfaqmesazhapolupemag = "jo";
            clsMesazh mesazh;
            string mesazhmevonshem = "";
            try
            {
                mesazh = dbRegj.ruajKokaRiparime(out idkoka, idNiv, idKonf, idgaranci, idMag, dtDk, nrkontakti, idLidhes, idSt, idNder, idNdVt, idPer,
                       dtRegj, shenim, idNivelGjenerues, idKonfigGjenerues, idGjenerues, idstatusriparimi, iddifekti, idkrijuesi, aksesor, dorezuar);


                if (!mesazh.Status)
                    return new clsMesazh(false, STR_NdodhiNjeGabimGjateRuajtjesSeKokesSePlanifikimit);

                foreach (clsTrupiRiparime o in ocolTrupi)
                {
                    o.IdKoka = idkoka;
                    int idM;
                    mesazh = dbRegj.ruajTrupiRiparime(out idM, o.IdKoka, o.IdArtLoan, o.IdDetLoan, o.Aksesor, o.IdArtSwap, o.IdDetSwap);
                    if (!mesazh.Status)
                    {
                        return mesazh;
                    }

                }
                clsDatabaseShare dbshare = new clsDatabaseShare(dbRegj );
                if (gjenerodokmagazine)
                {

                    if (kokamag.NrDok != null && kokamag.NrDok != "")
                    {
                        int idDokNgaFK;
                        if (kokamag.OFleteKontabel.IdDokNga == 0)
                            idDokNgaFK = -1;
                        else
                            idDokNgaFK = kokamag.OFleteKontabel.IdDokNga;

                        kokamag.IdGjenerues = idkoka;

                        //KEVI testim
                        string pershkrimMagFK;
                        if (kokamag.Shenime != String.Empty)
                        {
                            pershkrimMagFK = kokamag.Shenime;
                        }
                        else

                            pershkrimMagFK = clsKokaMagazina.pershkrimDokKontabilitetDaljeMag;

                        int idLlojDok;

                        idLlojDok = 52; //shitje
                        bool gjithmone = false;
                        //clsKusht kushtgj = new clsKusht(kokamag.IdKonfigAmbjente, "GJKGJ", dbshare);
                        //clsAlternativaKushti alt = new clsAlternativaKushti(kushtgj.Vlera, dbshare);
                        if (clsAlternativaKushti.getAlternativa(kokamag.IdKonfigAmbjente, "GJKGJ", dbshare) == "Po")
                            gjithmone = true;
                        mesazh = kokamag.ruaj(false, 1, null, idPeriudha, pershkrimMagFK, idLlojDok, idDokNgaFK, dbRegj, out shfaqmesazhapolupemag, kokamag.OFleteKontabel.KokaQendraKosto.IdDokNga, colTrupiQendramagvejter, eshteOwn, new DbAsete.colSerialetMagazine(), new DbAsete.colSerialetMagazine(), new DbShare.clsKonfigurimAmbjenti(), new DbShare.clsKonfigurimAmbjenti(), 0, 0, new DbQendraKosto.colTrupiQendraKosto(), false, new clsKokaShitje(), false, gjithmone, new DbAsete.colAmortizimiKoka(), 1, false, modifikim, new colTrupiMagazina(), new DbAsete.colAmortizimiKoka(), new int[0],false,false,false,out mesazhmevonshem, null, false, false);

                        if (!mesazh.Status)
                            return mesazh;
                    }
                    if (kokamag2.NrDok != null && kokamag2.NrDok != "")
                    {
                        int idDokNgaFK2;
                        if (kokamag2.OFleteKontabel.IdDokNga == 0)
                            idDokNgaFK2 = -1;
                        else
                            idDokNgaFK2 = kokamag2.OFleteKontabel.IdDokNga;

                        kokamag2.IdGjenerues = idkoka;

                        //KEVI testim
                        string pershkrimMagFK2;
                        if (kokamag2.Shenime != String.Empty)
                        {
                            pershkrimMagFK2 = kokamag2.Shenime;
                        }
                        else

                            pershkrimMagFK2 = clsKokaMagazina.pershkrimDokKontabilitetHyrjeMag;

                        int idLlojDok2;

                        idLlojDok2 = 51; //shitje
                        bool gjithmone = false;
                        //clsKusht kushtgj = new clsKusht(kokamag2.IdKonfigAmbjente, "GJKGJ", dbshare);
                        //clsAlternativaKushti alt = new clsAlternativaKushti(kushtgj.Vlera, dbshare);
                        if (clsAlternativaKushti.getAlternativa(kokamag2.IdKonfigAmbjente, "GJKGJ", dbshare) == "Po")
                            gjithmone = true;
                        mesazh = kokamag2.ruaj(false, 1, null, idPeriudha, pershkrimMagFK2, idLlojDok2, idDokNgaFK2, dbRegj, out shfaqmesazhapolupemag, kokamag2.OFleteKontabel.KokaQendraKosto.IdDokNga, colTrupiQendramagvejter, eshteOwn, new DbAsete.colSerialetMagazine(), new DbAsete.colSerialetMagazine(), new DbShare.clsKonfigurimAmbjenti(), new DbShare.clsKonfigurimAmbjenti(), 0, 0, new DbQendraKosto.colTrupiQendraKosto(), false, new clsKokaShitje(), false, gjithmone, new DbAsete.colAmortizimiKoka(), 1, false, modifikim, new colTrupiMagazina(), new DbAsete.colAmortizimiKoka(), new int[0],false,false,false, out mesazhmevonshem, null, false, false);

                        if (!mesazh.Status)
                            return mesazh;
                    }
                }

                return mesazh;
            }
            catch (Exception ce)
            {
                return new clsMesazh(false, ce.Message);
            }
        }

        /// <summary>
        /// Ruan objektin e  kokes se dokumentit te riparime ne tabelen perkatese ne databaze
        /// </summary>
        /// <param name="hfNrAutoregjistrime">hiddenField i Devit qe mban numrat automatike,null nese nuk perodren</param>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
        public clsMesazh ruaj(IDictionary<string, object> hfNrAutoregjistrime, int idPeriudha, bool gjenerodokmagazine, out string shfaqmesazhapolupemag, bool eshteOwn, ResourceManager rm, CultureInfo ci)
        {
            clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();
            //db.krijoManager();
            db.beginTransaksion();
            clsMesazh u_ruajt = ruaj(hfNrAutoregjistrime, idPeriudha, gjenerodokmagazine, out shfaqmesazhapolupemag, db, eshteOwn, rm, ci); //perdor ruajtjen me transaksion
            if (!u_ruajt.Status)
            {
                db.rollbackTransaksion();
                return u_ruajt;
            }
            db.commitTransaksion();
            return u_ruajt;
        }

        /// <summary>
        /// Ruan objektin e  kokes se dokumentit te riparimit ne tabelen perkatese ne databaze
        /// </summary>
        /// <param name="hfNrAutoregjistrime">hiddenField i Devit qe mban numrat automatike,null nese nuk perodren</param>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
        public clsMesazh ruaj(IDictionary<string, object> hfNrAutoregjistrime, int idPeriudha, bool gjenerodokmagazine, out string shfaqmesazhapolupemag, clsDatabaseRegjistrim db, bool eshteOwn, ResourceManager rm, CultureInfo ci)
        {
            bool kaNdryshimNumri;
            clsMesazh mesazhKontrolli = kontrolloRiparim(out kaNdryshimNumri, db, hfNrAutoregjistrime);
            if (!mesazhKontrolli.Status)
            {
                shfaqmesazhapolupemag = "jo";
                return mesazhKontrolli;
            }

            clsMesazh u_ruajt = clsKokaRiparime.ruajRiparim(out idKoka, IdNivel, IdKonfigAmbjente, IdGaranci, IdMagazina, DtDok, NrKontakti, IdDokNga, IdStatusDok, IdNdermarje, IdNdermarjeVit, IdPerdoruesi, DtRegj, Pershkrimi, IdNivelGjenerues, IdKonfigGjenerues, IdGjenerues, IdStatusRiparimi, IdDifekti, IdKrijuesi, aksesor, dorezuar, colTrupi, db, false, gjenerodokmagazine, oKokaMagazina, oKokaMagazina2, idPeriudha, out shfaqmesazhapolupemag, new DbQendraKosto.colTrupiQendraKosto(), eshteOwn, rm, ci);
            this.IdKoka = idKoka;
            if (!u_ruajt.Status)
            {
                return u_ruajt;
            }
            if (kaNdryshimNumri)
                return mesazhKontrolli;
            return u_ruajt;
        }

        /// <summary>
        /// kontrollon dokumentin e riparimit gjate ruajtjes dhe ben ndryshimet per nr automatik
        /// </summary>
        /// <param name="kaNdryshimNumri">tregon nese ka ndryshuar numri</param>
        /// <param name="dbRegj">clsdatabazeprodhimi per transaksion</param>
        /// <param name="hfNrAutoregjistrime">hiddenfield me fushat e nr automatik</param>
        /// <returns> clsMesazh </returns>
        private clsMesazh kontrolloRiparim(out bool kaNdryshimNumri, clsDatabaseRegjistrim dbRegj, IDictionary<string, object> hfNrAutoregjistrime)
        {
            kaNdryshimNumri = false;
            clsMesazh mes = new clsMesazh();

            if (hfNrAutoregjistrime != null)
            {
                mes = kontrolloNrAutoRiparim(out kaNdryshimNumri, dbRegj, hfNrAutoregjistrime);
                if (!mes.Status)
                    return mes;
            }
            ////if (dbRegj.ekzistonRegjistrimRiparimi(idKonfigAmbjente, IdGaranci,idStatusRiparimi, dtDok, idNdermarje))
            ////    return new clsMesazh(false, STR_EkzistonNjeRegjistrimMeTeNjejtinNumerDokumenti);
            if (kaNdryshimNumri)
                return mes;
            return new clsMesazh(true, STR_KontrolliIMagazinesUKryeMeSukses);
        }

        /// <summary>
        /// kontrollon nr automatik
        /// </summary>
        /// <param name="kaNdryshimNumri">tregon ne ka ndryshuar nr</param>
        /// <param name="dbRegj">clsDatabazeprodhimi per transaksion</param>
        /// <param name="hfregjistrime">hidden field me fushat e nr automatik</param>
        /// <returns> clsMesazh</returns>
        private clsMesazh kontrolloNrAutoRiparim(out bool kaNdryshimNumri, clsDatabaseRegjistrim dbRegj, IDictionary<string, object> hfregjistrime)
        {
            clsDatabaseAdmin db = new clsDatabaseAdmin(dbRegj );
            List<NrAuto> list = DbAdmin.clsNrAutom.kontrollogjithenumrat(db, hfregjistrime, dtDok);
            DbCore.clsMesazh mes = NrAuto.ruajvlera(out kaNdryshimNumri, list, dtDok, idPerdoruesi, idNdermarje, db);
            return new clsMesazh(mes.Status, mes.PershkrimMesazhi);
        }

        /// <summary>
        /// modifikon dokumentin e riparimit
        /// </summary>
        /// <param name="idkoka"></param>
        /// <param name="idNiv"></param>
        /// <param name="idKonf"></param>
        /// <param name="idgarancia"></param>
        /// <param name="idMag"></param>
        /// <param name="dtDk"></param>
        /// <param name="nrDk"></param>
        /// <param name="idLidhes"></param>
        /// <param name="idSt"></param>
        /// <param name="idNder"></param>
        /// <param name="idNdVt"></param>
        /// <param name="idPer"></param>
        /// <param name="dtRegj"></param>
        /// <param name="shenim"></param>
        /// <param name="idNivelGjenerues"></param>
        /// <param name="idKonfigGjenerues"></param>
        /// <param name="idGjenerues"></param>
        /// <param name="idstatusRiparime"></param>
        /// <param name="iddifekti"></param>
        /// <param name="aksesor"></param>
        /// <param name="ocolTrupi"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public static clsMesazh modifikoRiparim(int idkoka, int idNiv, int idKonf, int idgarancia, int idMag, DateTime dtDk, string nrDk, int idLidhes, int idSt, int idNder, int idNdVt, int idPer, DateTime dtRegj, string shenim, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues, int idstatusRiparime, int iddifekti, string aksesor, int dorezuar, colTrupiRiparime ocolTrupi, clsKokaMagazina kokamag, clsKokaMagazina kokamag2, int idPeriudha, bool gjenerodokmag, out string shfaqmesazhapolupemag, bool eshteOwn, clsDatabaseRegjistrim db, ResourceManager rm, CultureInfo ci)
        {
            shfaqmesazhapolupemag = "jo";
            colTrupiRiparime trupat = new colTrupiRiparime();
            trupat.mbushTrupiRiparime(idkoka, idNder, db);
            clsMesazh mesazh = new clsMesazh();
            try
            {   //obj koka qe i kalohet si parameter eshte si duhet te modifikohet
                clsKokaRiparime kokaEkzistuese = new clsKokaRiparime();
                kokaEkzistuese.mbushKokaRiparimeSipasID(idkoka, db);
                clsKokaMagazina kokaEkzistueseMag = new clsKokaMagazina();
                clsKokaMagazina kokaEkzistueseMag2 = new clsKokaMagazina();
                clsDatabaseInventari dbinv = new clsDatabaseInventari(db );
                if (kokaEkzistuese.IdStatusDok == 2)
                {
                    return new clsMesazh(false, STR_DokumentiKaNdryshuarJuLutemRihapeniPerseri);
                }
                kokaEkzistuese.IdStatusDok = 2;  //duhet vendosur nje status i pershtatshem per kete modifikim
                idLidhes = kokaEkzistuese.IdKoka;
                mesazh = db.modifikoKokaRiparime(kokaEkzistuese.IdKoka, kokaEkzistuese.IdNivel, kokaEkzistuese.IdKonfigAmbjente, kokaEkzistuese.IdGaranci, kokaEkzistuese.IdMagazina, kokaEkzistuese.DtDok, kokaEkzistuese.nrKontakti, kokaEkzistuese.idDokNga, kokaEkzistuese.IdStatusDok, kokaEkzistuese.idNdermarje, kokaEkzistuese.idNdermarjeVit, idPer, kokaEkzistuese.DtRegj, kokaEkzistuese.pershkrimi, kokaEkzistuese.idNivelGjenerues, kokaEkzistuese.idKonfigGjenerues, kokaEkzistuese.idGjenerues, kokaEkzistuese.idStatusRiparimi, kokaEkzistuese.idDifekti, kokaEkzistuese.aksesor, kokaEkzistuese.dorezuar);

                if (!mesazh.Status)
                {
                    return mesazh;
                }
                kokaEkzistueseMag.OFleteKontabel = new clsKokaFleteKontabel();
                kokaEkzistueseMag2.OFleteKontabel = new clsKokaFleteKontabel();
                clsDatabaseKontabilitet dbkontab = new clsDatabaseKontabilitet(db );
                DbQendraKosto.clsDatabaseQendraKosto dbqendra = new DbQendraKosto.clsDatabaseQendraKosto(db );

                kokaEkzistueseMag.mbushKokaMagazinaSipasIDGjenerues(kokaEkzistuese.idKoka, 2, kokaEkzistuese.IdKonfigAmbjente, db);
                if (kokaEkzistueseMag.IdKokaMagazina != 0)
                {
                    kokaEkzistueseMag.mbushTrupMagazine(db);
                    colArtikujt coleksistues1 = new colArtikujt(kokaEkzistueseMag.IdKokaMagazina, dbinv);
                    int i2 = 0;
                    foreach (clsTrupiMagazina trup in kokaEkzistueseMag.OcolTrupiMagazina)
                    {
                        if (trup.IdLlojVeprimi == 1)
                            trup.Element = coleksistues1[i2];
                        i2++;
                    }
                    mesazh = kokaEkzistueseMag.kontrolloGjendjeNeFshirje(db, kokamag.OcolTrupiMagazina, kokamag.IdStatusDok); //TOCHECK

                    if (!mesazh.Status)
                    {
                        return mesazh;
                    }
                    kokaEkzistueseMag.OFleteKontabel = new clsKokaFleteKontabel();
                    clsKokaFleteKontabel tmpKokeFK = new clsKokaFleteKontabel(kokaEkzistueseMag.IdKokaMagazina, 6, dbkontab);
                    if (tmpKokeFK.IdKokaFleteKontabel != 0)
                    {
                        kokaEkzistueseMag.OFleteKontabel = tmpKokeFK;
                        DbQendraKosto.clsKokaQendraKosto kokaqendra = new DbQendraKosto.clsKokaQendraKosto();
                        kokaqendra.KtheKokaQKSipasIDGjeneruesDheKonfigMeTrup(tmpKokeFK.IdKokaFleteKontabel, tmpKokeFK.IdKonfigAmbjente, dbqendra);
                        if (kokaqendra.IdKoka != 0 && kokaqendra.IdKoka != -1)
                        {
                            kokaEkzistueseMag.OFleteKontabel.KokaQendraKosto = kokaqendra;
                        }
                        else kokaEkzistueseMag.OFleteKontabel.KokaQendraKosto = new DbQendraKosto.clsKokaQendraKosto();
                    }
                    kokamag.OFleteKontabel.IdDokNga = kokaEkzistueseMag.OFleteKontabel.IdKokaFleteKontabel;

                    kokamag.OFleteKontabel.KokaQendraKosto.IdDokNga = kokaEkzistueseMag.OFleteKontabel.KokaQendraKosto.IdKoka;
                    bool kaveprimepas = false;
                    mesazh = kokaEkzistueseMag.fshiMagazina(kokaEkzistueseMag.IdKokaMagazina, idPer, db,false,false,false, new DbAsete.colSerialetMagazine  (), out kaveprimepas,true);
                    if (!mesazh.Status)
                    {
                        return mesazh;
                    }

                    kokamag.IdDokNga = kokaEkzistueseMag.IdKokaMagazina;



                }

                kokaEkzistueseMag2.mbushKokaMagazinaSipasIDGjenerues(kokaEkzistuese.idKoka, 1, kokaEkzistuese.IdKonfigAmbjente, db);
                if (kokaEkzistueseMag2.IdKokaMagazina != 0)
                {
                    kokaEkzistueseMag2.mbushTrupMagazine(db);
                    colArtikujt coleksistues1 = new colArtikujt(kokaEkzistueseMag2.IdKokaMagazina, dbinv);
                    int i2 = 0;
                    foreach (clsTrupiMagazina trup in kokaEkzistueseMag2.OcolTrupiMagazina)
                    {
                        if (trup.IdLlojVeprimi == 1)
                            trup.Element = coleksistues1[i2];
                        i2++;
                    }
                    mesazh = kokaEkzistueseMag2.kontrolloGjendjeNeFshirje(db, kokamag2.OcolTrupiMagazina, kokamag2.IdStatusDok); //TOCHECK

                    if (!mesazh.Status)
                    {
                        return mesazh;
                    }

                    kokaEkzistueseMag2.OFleteKontabel = new clsKokaFleteKontabel();
                    clsKokaFleteKontabel tmpKokeFK = new clsKokaFleteKontabel(kokaEkzistueseMag2.IdKokaMagazina, 6, dbkontab);
                    if (tmpKokeFK.IdKokaFleteKontabel != 0)
                    {
                        kokaEkzistueseMag2.OFleteKontabel = tmpKokeFK;
                        DbQendraKosto.clsKokaQendraKosto kokaqendra = new DbQendraKosto.clsKokaQendraKosto();
                        kokaqendra.KtheKokaQKSipasIDGjeneruesDheKonfigMeTrup(tmpKokeFK.IdKokaFleteKontabel, tmpKokeFK.IdKonfigAmbjente, dbqendra);
                        if (kokaqendra.IdKoka != 0 && kokaqendra.IdKoka != -1)
                        {
                            kokaEkzistueseMag2.OFleteKontabel.KokaQendraKosto = kokaqendra;
                        }
                        else kokaEkzistueseMag2.OFleteKontabel.KokaQendraKosto = new DbQendraKosto.clsKokaQendraKosto();
                    }
                    kokamag2.OFleteKontabel.IdDokNga = kokaEkzistueseMag2.OFleteKontabel.IdKokaFleteKontabel;

                    kokamag2.OFleteKontabel.KokaQendraKosto.IdDokNga = kokaEkzistueseMag2.OFleteKontabel.KokaQendraKosto.IdKoka;
                    bool kaveprimepas = false;
                    mesazh = kokaEkzistueseMag2.fshiMagazina(kokaEkzistueseMag2.IdKokaMagazina, idPer, db,false,false,false, new DbAsete.colSerialetMagazine (), out kaveprimepas,true);
                    if (!mesazh.Status)
                    {
                        return mesazh;
                    }
                    
                    kokamag.IdDokNga = kokaEkzistueseMag2.IdKokaMagazina;


                }

                mesazh = ruajRiparim(out idkoka, idNiv, idKonf, idgarancia, idMag, dtDk, nrDk, idLidhes, idSt, idNder, idNdVt, idPer, dtRegj, shenim, idNivelGjenerues, idKonfigGjenerues, idGjenerues, idstatusRiparime, iddifekti, kokaEkzistuese.idKrijuesi, aksesor, dorezuar, ocolTrupi, db, true, gjenerodokmag, kokamag, kokamag2, idPeriudha, out shfaqmesazhapolupemag, kokaEkzistueseMag.OFleteKontabel.KokaQendraKosto.ColTrupi, eshteOwn, rm, ci);

                return mesazh;
            }
            catch (Exception ce)
            {

                return new clsMesazh(false, ce.Message);
            }
        }

        /// <summary>
        /// Modifikon objektin e  kokes se dokumentit te riparimit ne tabelen perkatese ne databaze
        /// </summary>
        /// <param name="lidhur">tregon nese eshte i lidhur ose jo</param>
        /// <returns > nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo</returns>
        public clsMesazh modifiko(bool lidhur, int idPeriudha, bool gjenerodokmag, out string shfaqmesazhapolupemag, bool eshteOwn, ResourceManager rm, CultureInfo ci)
        {
            shfaqmesazhapolupemag = "jo";
            clsMesazh u_modifikua;
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            if (lidhur == false)
            {

                data.beginTransaksion();
                u_modifikua = modifikoRiparim(IdKoka, IdNivel, IdKonfigAmbjente, IdGaranci, IdMagazina, DtDok, NrKontakti, IdDokNga, IdStatusDok, IdNdermarje, IdNdermarjeVit, IdPerdoruesi, DtRegj, Pershkrimi, IdNivelGjenerues, IdKonfigGjenerues, IdGjenerues, IdStatusRiparimi, IdDifekti, aksesor, dorezuar, colTrupi, oKokaMagazina, oKokaMagazina2, idPeriudha, gjenerodokmag, out shfaqmesazhapolupemag, eshteOwn, data, rm, ci);
                if (u_modifikua.Status)
                    data.commitTransaksion();
                else data.rollbackTransaksion();
            }
            else
            {
                u_modifikua = data.modifikoKokaRiparime(IdKoka, IdNivel, IdKonfigAmbjente, IdGaranci, IdMagazina, DtDok, NrKontakti, idDokNga, IdStatusDok, idNdermarje, idNdermarjeVit, idPerdoruesi, DtRegj, Pershkrimi, idNivelGjenerues, idKonfigGjenerues, idGjenerues, IdStatusRiparimi, IdDifekti, aksesor, dorezuar);
                data.Dispose();
            }
            return u_modifikua;
        }

        /// <summary>
        /// fshin nje objekt dokument riparimit  duke i ndryshuar statusin
        /// </summary>
        ///<param name="idkokaplanifikim"> koka e dokumentit te riparimi i cili do te fshihet</param>
        ///<param name="idperdoruesi">id e perdoruesit qe ka bere veprimin</param>
        ///<param name="dbProdh"> clsdatabaseProdhimi si pjese e transaksionit</param>
        /// <returns> kthen nje obj clsMesazh per te identifikuar statusin e fshirjes se te dhenave ne DB</returns>
        public static clsMesazh fshiRiparim(int idkokaplanifikim, int idperdoruesi, clsDatabaseRegjistrim dbProdh)
        {

            clsMesazh mesazh = new clsMesazh(true);

            try
            {
                clsKokaRiparime kokaEkzistuese = new clsKokaRiparime();
                kokaEkzistuese.mbushKokaRiparimeSipasID(idkokaplanifikim, dbProdh);
                if (mesazh.Status)
                {
                    clsKokaMagazina k = new clsKokaMagazina();
                    k.mbushKokaMagazinaSipasIDGjenerues(kokaEkzistuese.IdKoka, 2, kokaEkzistuese.IdKonfigAmbjente, dbProdh);
                    if (k.IdKokaMagazina != 0)
                    {
                        bool kaveprimepas = false;
                        mesazh = k.fshiMagazina(k.IdKokaMagazina, idperdoruesi, dbProdh,false,false,false, new DbAsete.colSerialetMagazine (), out kaveprimepas,true);
                        if (!mesazh.Status)
                            return mesazh;
                    }
                }
                if (mesazh.Status)
                {
                    clsKokaMagazina k = new clsKokaMagazina();
                    k.mbushKokaMagazinaSipasIDGjenerues(kokaEkzistuese.IdKoka, 1, kokaEkzistuese.IdKonfigAmbjente, dbProdh);
                    if (k.IdKokaMagazina != 0)
                    {
                        bool kaveprimepas = false;
                        mesazh = k.fshiMagazina(k.IdKokaMagazina, idperdoruesi, dbProdh,false,false,false, new DbAsete.colSerialetMagazine (), out kaveprimepas,true);
                        if (!mesazh.Status)
                            return mesazh;
                    }
                }
                kokaEkzistuese.IdStatusDok = 2;  //duhet vendosur nje status i pershtatshem per kete modifikim
                mesazh = dbProdh.modifikoKokaRiparime(kokaEkzistuese.IdKoka, kokaEkzistuese.IdNivel, kokaEkzistuese.IdKonfigAmbjente, kokaEkzistuese.IdGaranci, kokaEkzistuese.IdMagazina, kokaEkzistuese.DtDok, kokaEkzistuese.NrKontakti, kokaEkzistuese.idDokNga, kokaEkzistuese.IdStatusDok, kokaEkzistuese.idNdermarje, kokaEkzistuese.idNdermarjeVit, idperdoruesi, kokaEkzistuese.DtRegj, kokaEkzistuese.pershkrimi, kokaEkzistuese.idNivelGjenerues, kokaEkzistuese.idKonfigGjenerues, kokaEkzistuese.idGjenerues, kokaEkzistuese.idStatusRiparimi, kokaEkzistuese.idDifekti, kokaEkzistuese.aksesor, kokaEkzistuese.dorezuar);
                if (!mesazh.Status)
                    return mesazh;


                return mesazh;

            }
            catch (Exception ce)
            {

                return new clsMesazh(false, ce.Message);
            }
        }

        /// <summary>
        /// Fshin objektin e  kokes se dokumentit te riparimit ne tabelen perkatese ne databaze
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh fshi()
        {
            clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();
            db.beginTransaksion();
            clsMesazh u_fshi = fshiRiparim(IdKoka, idPerdoruesi, db);
            if (u_fshi.Status)
                db.commitTransaksion();
            else db.rollbackTransaksion();
            return u_fshi;
        }

        /// <summary>
        /// Merr objektin e  kokes se dokumentit te riparimeve sipas id nga tabela perkatese ne databaze
        /// </summary>
        /// <param name="id"> id e kokes se riparimit</param>
        /// <returns > nje objekt clsKokaRiparime qe permban objektin e kerkuar</returns>
        public static clsKokaRiparime merrSipasId(int id)
        {
            clsKokaRiparime data = new clsKokaRiparime();
            data.mbushKokaRiparimSipasID(id);
            return data;
        }

        /// <summary>
        /// Merr objektin e  kokes se dokumentit te riparime sipas garancise dhe dt dokumenti nga tabela perkatese ne databaze.
        /// </summary>
        /// <returns > nje objekt clsKokaRiparime qe permban objektin e kerkuar</returns>
        public clsKokaRiparime merrSipasIdNivelGarancikDtDok()
        {
            clsKokaRiparime data = new clsKokaRiparime(IdNivel, IdGaranci, DtDok);
            return data;
        }

        /// <summary>
        /// Merr gjithe e  kokat e dokumentave te riparime sipas ndermarjevitit nga tabela perkatese ne databaze
        /// </summary>
        /// <param name="idNdermVit">id e ndermarje vitit</param>
        /// <returns > nje object colKokaPlanifikim qe permban nje koleksion me gjithe kokat e dokumentave te riparime te nje ndermarje ne nje vit te caktuar</returns>
        public static colKokaRiparime merriTeGjithe(int idNdermVit)
        {
            colKokaRiparime data = new colKokaRiparime(idNdermVit);
            return data;
        }

        /// <summary>
        /// Merr trupin  e  nje dokumenti te riparimeve nga tabela perkatese ne databaze
        /// </summary>
        /// <returns > nje object colTrupiRiparime qe permban nje koleksion me trupin e dokumentit te riparimeve</returns>
        public colTrupiRiparime merrTrupiRiparime(clsDatabaseRegjistrim db)
        {
            colTrupiRiparime trupi = new colTrupiRiparime();
            trupi.mbushTrupiRiparime(IdKoka, IdNdermarje, db);
            return trupi;
        }

        /// <summary>
        /// mbush koken e riparimit sipas id dokNga
        /// </summary>
        /// <param name="idDokNga">id dokumenti nga</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        public bool mbushKokaRiparimiSipasIDDokNga(int iddokNga)
        {
            clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();
            bool mbush = mbushKokaRiparime(db.ktheKokaRiparimeSipasIDDokNga(iddokNga), db);
            db.Dispose();
            return mbush;
        }

        /// <summary>
        /// mbush koken e riparimit sipas id se kokes
        /// </summary>
        /// <param name="idKoka">id e kokes se riparimit</param>
        /// <returns>kthen true nese mbushja kryhet me sukses ne te kundert kthen false</returns>        
        public bool mbushKokaRiparimSipasID(int idKoka)
        {
            clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();
            bool mbush = mbushKokaRiparimeSipasID(idKoka, db);
            db.Dispose();
            return mbush;
        }
        public static DataRow mbushKokaRiparimSipasIDDR(int idKoka)
        {
            clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();
            return db.ktheKokaRiparimeSipasIDDT(idKoka);

        }
        /// <summary>
        /// mbush koken e riparimit sipas id se kokes
        /// </summary>
        /// <param name="idKokaRiparim">id e kokes se riparimit</param>
        /// <returns>kthen true nese mbushja kryhet me sukses ne te kundert kthen false</returns>
        /// <param name="db"></param>
        public bool mbushKokaRiparimeSipasID(int idKokaRiparim, clsDatabaseRegjistrim db)
        {

            bool mbush = mbushKokaRiparime(db.ktheKokaRiparimeSipasID(idKokaRiparim), db);
            return mbush;
        }

        #endregion

        #region Metoda Internal


        /// <summary>
        /// mbush riparime me te dhenat nga databaza
        /// </summary>
        /// <param name="rreshti">rreshti me te dhena</param>
        /// <param name="db">clsDatabaseRegjistrim nqs bej pjese ne nje transaksion</param>
        /// <returns> kthen ne se mbushja u be ne rregull apo jo</returns>
        internal bool mbushKokaRiparime(DataRow rreshti, clsDatabaseRegjistrim db)
        {
            if (rreshti != null)
            {
                try
                {
                    int.TryParse(rreshti["IDKOKA"].ToString(), out idKoka);
                    int.TryParse(rreshti["IDNIVEL"].ToString(), out idNivel);
                    int.TryParse(rreshti["IDKONFIGAMBJENTE"].ToString(), out idKonfigAmbjente);
                    int.TryParse(rreshti["IDGARANCI"].ToString(), out idGaranci);
                    int.TryParse(rreshti["IDMAGAZINA"].ToString(), out idMagazina);
                    int.TryParse(rreshti["IDSTATUSRIPARIMI"].ToString(), out idStatusRiparimi);
                    int.TryParse(rreshti["IDDIFEKTI"].ToString(), out idDifekti);
                    int.TryParse(rreshti["IDKRIJUESI"].ToString(), out idKrijuesi);
                    nrKontakti = rreshti["NRKONTAKTI"].ToString();
                    aksesor = rreshti["AKSESOR"].ToString();
                    DateTime.TryParse(rreshti["DTDOK"].ToString(), out dtDok);

                    int.TryParse(rreshti["IDDOKNGA"].ToString(), out idDokNga);

                    int.TryParse(rreshti["IDSTATUSDOK"].ToString(), out idStatusDok);
                    int.TryParse(rreshti["IDNDERMARJE"].ToString(), out idNdermarje);
                    int.TryParse(rreshti["IDNDERVITI"].ToString(), out idNdermarjeVit);
                    int.TryParse(rreshti["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    int.TryParse(rreshti["DOREZUAR"].ToString(), out dorezuar);
                    DateTime.TryParse(rreshti["DTREGJ"].ToString(), out dtRegj);

                    pershkrimi = rreshti["PERSHKRIMI"].ToString();
                    int.TryParse(rreshti["IDNIVELGJENERUES"].ToString(), out idNivelGjenerues);
                    int.TryParse(rreshti["IDKONFIGGJENERUES"].ToString(), out idKonfigGjenerues);
                    int.TryParse(rreshti["IDGJENERUES"].ToString(), out idGjenerues);
                    DateTime.TryParse(rreshti["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(rreshti["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    colTrupi = new colTrupiRiparime();
                    colTrupi = merrTrupiRiparime(db);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception(STR_ERRORGabimGjateMarrjesSeDokumentitTePlanifikimit);
                }
            }
            else
                return false;
        }

        #endregion
    }
}
