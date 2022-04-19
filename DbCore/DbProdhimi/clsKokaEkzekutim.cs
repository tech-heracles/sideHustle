using System;
using System.Collections.Generic;
using System.Linq;
using DbCore.DbAdmin;
using DbCore.DbKontabiliteti;
using System.Data;
using DbCore.DbRegjistrim;
using DbCore.DbShare;
using System.Resources;
using System.Globalization;
using DbCore.DbInventari;

namespace DbCore.DbProdhimi
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne  koken e nje dokumenti te ekzekutimit
    ///  (Te dhenat  merren nga tabela : T_KOKAEKZEKUTIMPRODHIMI)
    /// </summary>
    public class clsKokaEkzekutim
    {

        /// <summary>
        /// konstante per mesazhin e gabimit kur merren te dhenat
        /// </summary>
        private const string STR_ERRORGabimGjateMarrjesSeDokumentitTePlanifikimit = "ERROR: Gabim gjatë marrjes së dokumentit të ekzekutimit nga db-ja";
        /// <summary>
        /// pershkrimi i dokumentit te magazines
        /// </summary>
        private const string STR_NgaProcesiIProdhimit = "Nga Procesi i prodhimit ";
        /// <summary>
        /// constante qe sasia nuk duhet te jete zero
        /// </summary>
        private const string STR_SasiaNukDuhetTeJeteZero = "Sasia nuk duhet të jetë zero";
        /// <summary>
        /// konstante kur ekzekutimi i prodhimit krijohet me sukses
        /// </summary>
        private const string STR_PlanifikimiUKrijuaMeSukses = "Ekzekutimi i prodhimit u krijua me sukses!";
        /// <summary>
        /// konstante per mesazhin e gabimit kur nr i dokumentit eshte bosh
        /// </summary>
        private const string STR_NumriIDokumentitNukMundTeJeteBosh = "Numri i dokumentit nuk mund të jetë bosh";
        /// <summary>
        /// konstante per mesazhin e gabimit kur nuk eshte zgjedhur data e dokumentit
        /// </summary>
        private const string STR_ZgjidhniDatenEDokumentit = "Zgjidhni datën e dokumentit!";
        /// <summary>
        /// konstante per mesazhin e gabimit kur nuk eshte zgjedhur data e regjistrimit
        /// </summary>
        private const string STR_ZgjidhniDatenERegjistrimit = "Zgjidhni datcase 98:n e regjistrimit!";
        /// <summary>
        /// mesazh gabimi kur magazina nuk ekziston
        /// </summary>
        private const string STR_MagazinaNukEkziston = "Magazina nuk ekziston!";
        /// <summary>
        /// mesazh gabimi kur magazina nuk eshte aktive
        /// </summary>
        private const string STR_MagazinaNukEshteAktive = "Magazina nuk është aktive!";
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
        private const string STR_NdodhiNjeGabimGjateRuajtjesSeKokesSePlanifikimit = "Ndodhi një Gabim gjatë ruajtjes së Kokës së ekzekutimit";
        /// <summary>
        /// konstante per mesazhin e gabimit kur ekziston nje dokument me keto te dhena
        /// </summary>
        private const string STR_EkzistonNjeRegjistrimMeTeNjejtinNumerDokumenti = "Ekziston nje regjistrim me të njëjtin numër dokumenti!";
        /// <summary>
        /// mesazh kur kontrollet kalojne me sukses
        /// </summary>
        private const string STR_KontrolliIMagazinesUKryeMeSukses = "Kontrolli i ekzekutimit u krye me sukses!";
        /// <summary>
        /// konstante per mesazhin e gabimit kur dokumenti ka ndryshuar gjate modifikimit
        /// </summary>
        private const string STR_DokumentiKaNdryshuarJuLutemRihapeniPerseri = "Dokumenti ka ndryshuar! Ju lutem rihapeni përsëri!";

        #region Attributet
        /// <summary>
        /// id e kokes se dokumentit te ekzekutimit
        /// </summary>
        private int idKokaEkzekutim;
        /// <summary>
        /// id e nivelit te regjistrimit
        /// </summary>
        private int idNivel;
        /// <summary>
        /// id e konfigurimit te ambjenteve
        /// </summary>
        private int idKonfigAmbjente;
        /// <summary>
        /// id e magazines ku do shkoje produkti
        /// </summary>
        private int idMagProdukti;
        /// <summary>
        /// id e magazines nga merren artikujt e receptures
        /// </summary>
        private int idMagReceptura;
        /// <summary>
        /// data e dokumentit
        /// </summary>
        private DateTime dtDok;
        /// <summary>
        /// nr i dokumentit
        /// </summary>
        private String nrDok;
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
        private int idNdermarrje;
        /// <summary>
        /// id nderamrje vit
        /// </summary>
        private int idNdermarrjeVit;
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
        private string shenime;
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
        /// kodi i magazines se produktit
        /// </summary>
        private string kodMagProdukti;
        /// <summary>
        /// kodi i magazines se receptures
        /// </summary>
        private string kodMagReceptura;
        /// <summary>
        /// totali i dokumentit
        /// </summary>
        private double totali;
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
        /// <summary>
        /// koleksioni me produktet e ekzekutimit
        /// </summary>
        private colProduktProdhimi colProdukte;
        /// <summary>
        /// dokumenti i fletes kontabel qe gjenerohet nga ky dokument
        /// </summary>
        private clsKokaFleteKontabel oFleteKontabel;
        /// <summary>
        /// dokumenti i magazines per hyrjen e produkteve ne magazine
        /// </summary>
        private clsKokaMagazina oMagazineHyrje;
        /// <summary>
        /// dokumenti i magazines per daljen e recepturave nga magazina
        /// </summary>
        private clsKokaMagazina oMagazineDalje;

        private int idNjesiProdhimi;
        private string kodNjesiProdhimi;
        private DataRow rreshti;
        private clsDatabazeProdhimi db;

        #endregion

        #region Properties
        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdKokaEkzekutim
        {
            get { return idKokaEkzekutim; }
            set { idKokaEkzekutim = value; }
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
        /// Kthen/Vendos ID-ne e magazines ku do shkoje produkti
        /// </summary>
        public int IdMagProdukti
        {
            get { return idMagProdukti; }
            set { idMagProdukti = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e magazines nga merren artikujt e recepturave
        /// </summary>
        public int IdMagReceptura
        {
            get { return idMagReceptura; }
            set { idMagReceptura = value; }
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
        /// Kthen/Vendos shenime.
        /// </summary>
        public String Shenime
        {
            get { return shenime; }
            set { shenime = value; }
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
        public int IdNdermarrje
        {
            get { return idNdermarrje; }
            set { idNdermarrje = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e ndermarje vitit.
        /// </summary>
        public int IdNdermarrjeVit
        {
            get { return idNdermarrjeVit; }
            set { idNdermarrjeVit = value; }
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
        /// Kthen/Vendos  nje koleksion me produktet e ekzekutimit
        /// </summary>
        public colProduktProdhimi ColProdukte
        {
            get { return colProdukte; }
            set { colProdukte = value; }
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
        /// <summary>
        /// totali i dokumentit
        /// </summary>
        public double Totali
        {
            get
            {
                return totali;
            }
            set
            {
                totali = value;
            }
        }
        /// <summary>
        /// dokumenti i fletes kontabel qe gjenerohet nga ky dokument
        /// </summary>
        public clsKokaFleteKontabel OFleteKontabel
        {
            get
            {
                return oFleteKontabel;
            }
            set
            {
                oFleteKontabel = value;
            }
        }
        /// <summary>
        /// dokumenti i magazines per hyrjen e produkteve ne magazine
        /// </summary>
        public clsKokaMagazina OMagazineHyrje
        {
            get
            {
                return oMagazineHyrje;
            }
            set
            {
                oMagazineHyrje = value;
            }
        }
        /// <summary>
        /// dokumenti i magazines per daljen e recepturave nga magazina
        /// </summary>
        public clsKokaMagazina OMagazineDalje
        {
            get
            {
                return oMagazineDalje;
            }
            set
            {
                oMagazineDalje = value;
            }
        }
        public int IdNjesiProdhimi
        {
            get { return idNjesiProdhimi; }
            set { idNjesiProdhimi = value; }
        }

        public string KodNjesiProdhimi
        {
            get { return kodNjesiProdhimi; }
            set { kodNjesiProdhimi = value; }
        }

        #endregion

        #region Konstruktoret
        /// <summary>
        /// Konstruktori i klases me parametra
        /// </summary>
        /// <param name="dtDk"> data e dokumentit te ekzekutimit</param>
        /// <param name="dtRegj"> data e regjistrimit te dokumentit te ekzekutimit</param>
        /// <param name="idmagprodukti"> id e magazines ku do shkoje produkti</param>
        /// <param name="idKonf"> id e konfigurimit te dokumentit</param>
        /// <param name="idLidhes">id e dokumentit nga e cila gjenerohet ne rastet e modifikimit </param>
        /// <param name="idmagreceptura">id e magazines nga merren recepturat</param>
        /// <param name="idkoka">id ritese e kokes se ekzekutimit</param>
        /// <param name="idNder"> id e ndermarjes</param>
        /// <param name="idNdVt"> id e ndermarje vitit</param>
        /// <param name="idNiv"> id e nivelit te regjistrimit</param>
        /// <param name="idPer"> id e perdoruesit se e ben regjistrimin</param>
        /// <param name="idSt"> id e statusit te dokumentit</param>
        /// <param name="nrDk">nr i dokumentit</param>
        /// <param name="shenim"> shenime </param>
        /// <param name="idGjenerues">id e dokumentit nga gjenerohet planifikimi nga nje ambjent tjeter</param>
        /// <param name="idKonfigGjenerues">id e konfigurimit nga gjenerohet dokumenti</param>
        /// <param name="idNivelGjenerues">id e nivelit nga gjenerohet dokumenti</param>
        /// <param name="totali">totali i dokumentit</param>
        /// <param name="idgrup1">id e grupimit te pare</param>
        /// <param name="idgrup2">id e grupimit te dyte</param>
        /// <param name="idgrup3">id e grupimit te trete</param>
        public clsKokaEkzekutim(int idkoka, int idNiv, int idKonf, int idmagprodukti, int idmagreceptura, DateTime dtDk, string nrDk, int idLidhes, int idSt, int idNder, int idNdVt, int idPer, DateTime dtRe, string shenim, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues, double totali, int idgrup1, int idgrup2, int idgrup3, int idNjesiProdhimi)
        {
            idKokaEkzekutim = idkoka;
            idNivel = idNiv;
            idKonfigAmbjente = idKonf;
            idMagProdukti = idmagprodukti;
            idMagReceptura = idmagreceptura;
            nrDok = nrDk;
            dtDok = dtDk;
            idDokNga = idLidhes;
            idStatusDok = idSt;
            idNdermarrje = idNder;
            idNdermarrjeVit = idNdVt;
            idPerdoruesi = idPer;
            dtRegj = dtRe;
            shenime = shenim;
            this.idNivelGjenerues = idNivelGjenerues;
            this.idKonfigGjenerues = idKonfigGjenerues;
            this.idGjenerues = idGjenerues;
            this.totali = totali;
            idGrup1 = idgrup1;
            idGrup2 = idgrup2;
            idGrup3 = idgrup3;
            colProdukte = new colProduktProdhimi();
            oFleteKontabel = new clsKokaFleteKontabel();
            oMagazineDalje = new clsKokaMagazina();
            oMagazineHyrje = new clsKokaMagazina();
            this.idNjesiProdhimi = idNjesiProdhimi;
        }

        /// <summary>
        /// konstruktor me 3 parametra
        /// </summary>
        /// <param name="idNivel">id e nivelit</param>
        /// <param name="nrdok">nr i dokumentit</param>
        /// <param name="dtdok">data e dokumentit</param>
        public clsKokaEkzekutim(int idNivel, string nrdok, DateTime dtdok)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            mbushKokaEkzekutim(db.ktheKokaEkzekutimSipasIdNivelNrDokDtDok(idNivel, nrdok, dtdok), db);
            db.Dispose();
        }

        /// <summary>
        /// konstruktori me nje parameter
        /// </summary>
        /// <param name="idkoka">id koka</param>
        public clsKokaEkzekutim(int idkoka)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            mbushKokaEkzekutim(db.ktheKokaEkzekutimSipasID(idkoka), db);
            db.Dispose();
        }

        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsKokaEkzekutim()
        {
        }

        public clsKokaEkzekutim(DataRow rreshti, clsDatabazeProdhimi db)
        {
            
            mbushKokaEkzekutim(rreshti, db);
        }
        #endregion

        #region Metoda Publike

        /// <summary>
        /// krijon objektin e dokumentit te ekzekutimit kur krijohet nga ambjenti i ekzekutimit
        /// </summary>
        /// <param name="idNiv">id e nivelit</param>
        /// <param name="idKonf">id e konfigurimit te dokumentit</param>
        /// <param name="idmagprodukt">id mag e produktit</param>
        /// <param name="kodmagprodukt"> kodi mag i produktit</param>
        /// <param name="idmagrec">id e magazines se receptura</param>
        /// <param name="kodmagrec">kodmagazina receptura</param>
        /// <param name="dtDk">data e dokumentit</param>
        /// <param name="nrDk">nr i dokumentit</param>
        /// <param name="idSt">id e statusit te dokumentit</param>
        /// <param name="idNder">id e ndermarjes</param>
        /// <param name="idNdVt">id e ndermarje vitit</param>
        /// <param name="idPer">id e perdoruesit</param>
        /// <param name="dtRegj">dt e regjistrimit</param>
        /// <param name="shenim">shenime</param>
        /// <param name="colprodukt">produktet</param>
        /// <param name="totali">totali i dokumentit</param>
        /// <param name="idgrup1">id e grupimit te pare </param>
        /// <param name="idgrup2">id e grupimit te dyte</param>
        /// <param name="idgrup3">id e grupimit te trete</param>
        /// <returns> kthen clsMesazh </returns>
        public clsMesazh krijoEkzekutim(int idNiv, int idKonf, int idmagprodukt, string kodmagprodukt, int idmagrec, string kodmagrec, DateTime dtDk, string nrDk, int idSt, int idNder, int idNdVt, int idPer, DateTime dtRegj, string shenim, double totali, int idgrup1, int idgrup2, int idgrup3, colProduktProdhimi colprodukt, int idNjesiProdhimi, string kodNjesiProdhimi)
        {
            return krijoEkzekutim(idNiv, idKonf, idmagprodukt, kodmagprodukt, idmagrec, kodmagrec, dtDk, nrDk, 0, idSt, idNder, idNdVt, idPer, dtRegj, shenim, 0, 0, 0, totali, idgrup1, idgrup2, idgrup3, colprodukt, idNjesiProdhimi, kodNjesiProdhimi, false);
        }

        /// <summary>
        /// krijon objektin e dokumentit te ekzekutimit kur krijohet nga dokumentat gjenerues
        /// </summary>
        /// <param name="idNiv">id e nivelit</param>
        /// <param name="idKonf">id e konfigurimit te dokumentit</param>
        /// <param name="idmagprodukt">id e magazines produkt</param>
        /// <param name="kodmagprodukt"> kodi i magazines produkt</param>
        /// <param name="idmagrec">id e magazines se recepturave</param>
        /// <param name="kodmagrec">kodmagazina se recepturave</param>
        /// <param name="dtDk">data e dokumentit</param>
        /// <param name="nrDk">nr i dokumentit</param>
        /// <param name="idSt">id e statusit te dokumentit</param>
        /// <param name="idNder">id e ndermarjes</param>
        /// <param name="idNdVt">id e ndermarje vitit</param>
        /// <param name="idPer">id e perdoruesit</param>
        /// <param name="dtRe">dt e regjistrimit</param>
        /// <param name="shenim">shenime</param>
        /// <param name="iddoknga">id e dokumentit nga eshte krijuar ne modifikim</param>
        /// <param name="idGjenerues">id e dokumentit qe e ka gjeneruar</param>
        /// <param name="idKonfigGjenerues"> id e konfigurimit te dokumentit qe e ka gjeneruar</param>
        /// <param name="idNivelGjenerues">id e nivelit te dokumentit qe e ka gjeneruar</param>
        /// <param name="colprodukt"> produktet</param>
        /// <param name="totali"> totali i dokumentit</param>
        /// <param name="idgrup1">id e grupimit te pare </param>
        /// <param name="idgrup2">id e grupimit te dyte</param>
        /// <param name="idgrup3">id e grupimit te trete</param>
        /// <returns> kthen clsMesazh </returns>
        public clsMesazh krijoEkzekutim(int idNiv, int idKonf, int idmagprodukt, string kodmagprodukt, int idmagrec, string kodmagrec, DateTime dtDk, string nrDk, int iddoknga, int idSt, int idNder, int idNdVt, int idPer, DateTime dtRe, string shenim, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues, double totali, int idgrup1, int idgrup2, int idgrup3, colProduktProdhimi colprodukt, int idNjesiProdhimi, string kodNjesiProdhimi, bool kontrolloEkzistence)
        {
            idNivel = idNiv;
            idKonfigAmbjente = idKonf;
            idMagProdukti = idmagprodukt;
            kodMagProdukti = kodmagprodukt;
            idMagReceptura = idmagrec;
            kodMagReceptura = kodmagrec;
            nrDok = nrDk;
            dtDok = dtDk;
            idDokNga = iddoknga;
            idStatusDok = idSt;
            idNdermarrje = idNder;
            idNdermarrjeVit = idNdVt;
            idPerdoruesi = idPer;
            dtRegj = dtRe;
            shenime = shenim;
            idGrup1 = idgrup1;
            idGrup2 = idgrup2;
            idGrup3 = idgrup3;
            this.idNivelGjenerues = idNivelGjenerues;
            this.idKonfigGjenerues = idKonfigGjenerues;
            this.idGjenerues = idGjenerues;
            this.totali = totali;
            colProdukte = colprodukt;
            this.idNjesiProdhimi = idNjesiProdhimi;
            this.kodNjesiProdhimi = kodNjesiProdhimi;
            clsMesazh mesazh = kontrollo();
            if (!mesazh.Status)
                return mesazh;
            if (kontrolloEkzistence)
            {
                bool kaNdryshimNumri;
                clsDatabazeProdhimi dbProdhim = new clsDatabazeProdhimi();
                mesazh = kontrolloEkzekutim(out kaNdryshimNumri, dbProdhim, null);
                if (!mesazh.Status)
                {
                    dbProdhim.Dispose();
                    return mesazh;
                }
                dbProdhim.Dispose();
            }
            oMagazineDalje = new clsKokaMagazina();//magazinat dhe fleta kontabel krijohen gjate ruajtjes sepse varen nga kostoja e artikullit
            oMagazineHyrje = new clsKokaMagazina();
            oFleteKontabel = new clsKokaFleteKontabel();
            return new clsMesazh(true, STR_PlanifikimiUKrijuaMeSukses);
        }

        /// <summary>
        /// krijon dokumentin e magazines nga dokumenti i ekzekutimit
        /// </summary>
        /// <param name="mag">koka e magazines</param>
        /// <param name="nrdok"> nr i dokumentit</param>
        /// <param name="dtdok"> dt e dokumentit</param>
        /// <param name="dtregj"> dt e regjistrimit</param>
        /// <param name="idperdoruesi">id e perdoruesit</param>
        /// <param name="idndermarje">id e ndermarjes</param>
        /// <param name="idnderviti">id e ndermarje vitit</param>
        /// <param name="idkokaekzekutim">id e kokes se ekzektutimit</param>
        /// <param name="idkonfigekzekutim">id e konfigurimit te ekzektutimit</param>
        /// <param name="idnivelekzekutim">id e nivelit te ekzekutimit</param>
        /// <param name="colprodukte"> koleksioni me produktet </param>
        /// <param name="pershkrimi">pershkrimi</param>
        /// <param name="iddoknga">id e dokumentit nga</param>
        /// <param name="idstatusdok">id e statusit te dokumentit</param>
        /// <param name="hyrje_dalje"> eshte magazine hyrje apo dalje</param>
        /// <param name="konfmag"> konfigurimi i magazines</param>
        /// <param name="idmag">id e magazines</param>
        /// <param name="kodmag">kodi i magazines</param>
        /// <param name="idgrup1">id e grupimit te pare </param>
        /// <param name="idgrup2">id e grupimit te dyte</param>
        /// <param name="idgrup3">id e grupimit te trete</param>
        /// <returns>kthen clsMesazh nese dokumenti u krijua me sukses apo jo</returns>
        private static clsMesazh krijoMagazineNgaEkzekutimi(clsKokaMagazina mag, string nrdok, DateTime dtdok, DateTime dtregj, int idperdoruesi, int idndermarje, int idnderviti, int idkokaekzekutim, int idkonfigekzekutim, int idnivelekzekutim, colProduktProdhimi colprodukte, string pershkrimi, int iddoknga, int idstatusdok, bool hyrje_dalje, DbShare.clsKonfigurimAmbjenti konfmag, int idmag, string kodmag, int idgrup1, int idgrup2, int idgrup3, int idkrijuesi, clsDatabazeProdhimi dbprodh)
        {
            string shenime = "";
            if (pershkrimi != String.Empty) shenime = pershkrimi;
            else
            {

                shenime = STR_NgaProcesiIProdhimit;

            }
            DbInventari.clsDatabaseInventari db = new DbInventari.clsDatabaseInventari(dbprodh );
            colTrupiMagazina coltrupi;
            try
            {
                coltrupi = ruajTrupinEMagazines(colprodukte, hyrje_dalje ? 1 : -1, hyrje_dalje, idndermarje, dtdok, db);
            }
            catch (Exception ex)
            {
                return new clsMesazh(false, ex.Message);
            }
          
            return mag.krijoMagazine(false,0, konfmag.IdNivel, konfmag.IdKonfigAmbjente, 0, "", idmag, kodmag, dtdok, nrdok, 0, "", konfmag.IdKategori, iddoknga, 0, idstatusdok, idndermarje, idnderviti, idperdoruesi, dtregj, hyrje_dalje ? 1 : 2, shenime, idnivelekzekutim, idkonfigekzekutim, idkokaekzekutim, 0, "", 0, "", 0, "", false, idgrup1, idgrup2, idgrup3, "", "", "", coltrupi, new clsKokaMagazina(), new clsKokaFleteKontabel(), new clsKokaRezervime(), new clsDatabaseRegjistrim(dbprodh ), true, 0, "", 0, false, idkrijuesi, dtdok, 0, "", "", "", 0, null,false,false,"","",0);
        }

        /// <summary>
        /// krijon trupin e dokumentit te magazines nga dokumenti i ekzekutimit
        /// </summary>
        /// <param name="colProdukte">koleksion me produktet</param>
        /// <param name="shenja"> shenja e veprimit </param>
        /// <param name="hyrje_dalje"> dokument hyrje apo dokument dalje</param>
        /// <param name="idndermarje">id e nderamrjes</param>
        /// <param name="dtdok"> dt e dokumentit</param>
        /// <returns>kthen trupin e dokumentit te magazines</returns>
        private static colTrupiMagazina ruajTrupinEMagazines(colProduktProdhimi colProdukte, int shenja, bool hyrje_dalje, int idndermarje, DateTime dtdok, DbCore.DbInventari.clsDatabaseInventari dbinv)
        {

            colTrupiMagazina trupat = new colTrupiMagazina();

            foreach (clsProduktProdhimi tsh in colProdukte)
            {
                if (hyrje_dalje)
                {
                    clsTrupiMagazina trupMag = new clsTrupiMagazina();
                    bool krijuar = false;
                    try
                    {
                        krijuar = trupMag.krijoTrupMagazinaNgaGrida(idndermarje, dtdok, 1, 1, tsh.IdArtikulli, tsh.PershkrimArtikull, tsh.IdDetajim1, tsh.IdNjesia, tsh.SasiaAktuale, tsh.Kosto, tsh.KostoTotale, tsh.IdMag, shenja, tsh.IdDetajim2, tsh.KodiArtikull, "", "", 0, 0, 0, "", 0, dbinv, false, 0, 0);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    if (trupMag.IdArtikulli != -1 && krijuar) //ky kusht duhet pare kur te shtohen makrot
                    {
                        if (trupMag.IdMag == -1)
                        {
                            throw new Exception(STR_MagazinaNukEkziston);
                        }

                        trupMag.Shenja = shenja;
                        trupat.Add(trupMag);
                    }
                    foreach (clsRecepturaProdhimi rec in tsh.ColReceptura)
                    {

                        clsTrupiMagazina trupMag1 = new clsTrupiMagazina();


                        if (rec.Lloji == 2 || rec.SasiaAktuale >= 0)
                            continue;
                        krijuar = false;
                        try
                        {
                            krijuar = trupMag1.krijoTrupMagazinaNgaGrida(idndermarje, dtdok, 1, 1, rec.IdArtikulli, rec.PershkrimArtikull, rec.IdDetajimi, rec.NjesiArtikull, -rec.SasiaAktuale, rec.Kosto, -rec.KostoTotale, rec.IdMag, shenja, rec.IdDetajimi2, rec.KodiArtikull, "", "", 0, 0, 0, "",0, dbinv,false, 0, 0);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }

                        if (trupMag1.IdArtikulli != -1 && krijuar) //ky kusht duhet pare kur te shtohen makrot
                        {
                            if (trupMag1.IdMag == -1)
                            {
                                throw new Exception(STR_MagazinaNukEkziston);
                            }

                            trupMag1.Shenja = shenja;
                            trupat.Add(trupMag1);
                        }
                    }
                }
                else
                {
                    foreach (clsRecepturaProdhimi rec in tsh.ColReceptura)
                    {
                        clsTrupiMagazina trupMag = new clsTrupiMagazina();
                        if (rec.Lloji == 2 || rec.SasiaAktuale < 0)
                            continue;
                        bool krijuar = false;
                        try
                        {
                            krijuar = trupMag.krijoTrupMagazinaNgaGrida(idndermarje, dtdok, 1, 1, rec.IdArtikulli, rec.PershkrimArtikull, rec.IdDetajimi, rec.NjesiArtikull, rec.SasiaAktuale, rec.Kosto, rec.KostoTotale, rec.IdMag, shenja, rec.IdDetajimi2, rec.KodiArtikull, "", "", 0, 0, 0, "",0, dbinv,false, 0, 0);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }

                        if (trupMag.IdArtikulli != -1 && krijuar) //ky kusht duhet pare kur te shtohen makrot
                        {
                            if (trupMag.IdMag == -1)
                            {
                                throw new Exception(STR_MagazinaNukEkziston);
                            }

                            trupMag.Shenja = shenja;
                            trupat.Add(trupMag);
                        }
                    }
                }
            }
            return trupat;
        }

        /// <summary>
        /// kontrollon te dhenat e kokes ne jane te sakta
        /// </summary>
        /// <returns>clsMesazh </returns>
        private clsMesazh kontrollo()
        {
            if (nrDok == "")
                return new clsMesazh(false, STR_NumriIDokumentitNukMundTeJeteBosh);
            if (dtDok == null || dtDok.ToShortDateString() == databosh)
                return new clsMesazh(false, STR_ZgjidhniDatenEDokumentit);
            if (dtRegj == null || dtRegj.ToShortDateString() == databosh)
                return new clsMesazh(false, STR_ZgjidhniDatenERegjistrimit);
            if (kodMagProdukti != "" && !clsNjesiAdministrative.ekziston(kodMagProdukti, idNdermarrje))
                return new clsMesazh(false, STR_MagazinaNukEkziston);
            clsNjesiAdministrative mag = new clsNjesiAdministrative(kodMagProdukti, idNdermarrje, idPerdoruesi);
            if (kodMagProdukti != "" && !mag.Aktiv)
                return new clsMesazh(false, STR_MagazinaNukEshteAktive);
            if (kodMagReceptura != "" && !clsNjesiAdministrative.ekziston(kodMagReceptura, idNdermarrje))
                return new clsMesazh(false, STR_MagazinaNukEkziston);
            mag = new clsNjesiAdministrative(kodMagReceptura, idNdermarrje, idPerdoruesi);
            if (kodMagReceptura != "" && !mag.Aktiv)
                return new clsMesazh(false, STR_MagazinaNukEshteAktive);
            if (!String.IsNullOrEmpty(kodNjesiProdhimi))
            {
                if (idNjesiProdhimi <= 0 || !clsNjesiProdhimi.ekzistonSipasKodit(kodNjesiProdhimi, idNdermarrje))
                    return new clsMesazh(false, "Njesia e prodhimit nuk ekziston!");
                if (!clsNjesiProdhimi.eshteAktiveNjesiProdhimiSipasKodit(kodNjesiProdhimi, idNdermarrje))
                    return new clsMesazh(false, "Njesia e prodhimit nuk eshte aktive!");
            }

            foreach (clsProduktProdhimi trupi in colProdukte)
                if (trupi.SasiaAktuale == 0)
                    return new clsMesazh(false, STR_SasiaNukDuhetTeJeteZero);            
            return new clsMesazh(true, STR_KontrolletUKaluanMeSukses);
        }

        /// <summary>
        /// krijon objektin e dokumentit te ekzekutim kur kemi import
        /// </summary>
        /// <param name="idNiv">id e nivelit</param>
        /// <param name="idKonf">id e konfigurimit</param>
        /// <param name="kodmagpro">kodi i magazinese se produktit</param>
        /// <param name="kodmagrec">kodi i magazines ser recepturave</param>
        /// <param name="dtDk">data e dokumentit</param>
        /// <param name="nrDk">nr i dokumentit</param>
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
        /// <param name="coltrupi">produktet</param>
        /// <param name="totali"> totali</param>
        /// <param name="mekontabilizim">me kontabilizim</param>
        /// <returns> clsMesazh</returns>
        public clsMesazh krijoKokaEkzekutimPerImport(string niveli, clsKonfigurimAmbjenti konfigAmb, string kodmagpro, string kodmagrec, DateTime dtDk, string nrDk, int idSt, int idNder, int idNdVt, int idPer, DateTime dtRegj, string shenim, string kodGrup1, string kodGrup2, string kodGrup3, colProduktProdhimi coltrupi, string kodNjesiProdhimi)
        {
            int idNivel = clsNivelRegjistrimi.ktheIdNivelRegjistrimiSipasKodi("EP", idNder);
            if (idNivel <= 0)
                return new clsMesazh(false, "Nenkategoria Ekzekutim Prodhimi nuk ekziston!");

            if (dtDk.Year != clsNdermarrjeViti.ktheKodVitiSipasIdNdermViti(idNdVt))
                return new clsMesazh(false, "Data nuk i përket vitit ushtrimor të zgjedhur!");


            this.idKonfigAmbjente = konfigAmb.IdKonfigAmbjente;
            int idGrup1 = 0, idGrup2 = 0, idGrup3 = 0;
            if (!String.IsNullOrEmpty(kodGrup1))
            {
                idGrup1 = clsGrupimDokumentiKoka.ktheIdGrupDokumentash(kodGrup1, idNder, 1);
                if (idGrup1 <= 0)
                    return new clsMesazh(false, "Grupimi i pare i dokumentit me kod " + kodGrup1 + " nuk ekziston!");
            }
            if (!String.IsNullOrEmpty(kodGrup2))
            {
                idGrup2 = clsGrupimDokumentiKoka.ktheIdGrupDokumentash(kodGrup2, idNder, 2);
                if (idGrup2 <= 0)
                    return new clsMesazh(false, "Grupimi i dyte i dokumentit me kod " + kodGrup2 + " nuk ekziston!");
            }
            if (!String.IsNullOrEmpty(kodGrup3))
            {
                idGrup3 = clsGrupimDokumentiKoka.ktheIdGrupDokumentash(kodGrup3, idNder, 3);
                if (idGrup3 <= 0)
                    return new clsMesazh(false, "Grupimi i dyte i dokumentit me kod " + kodGrup3 + " nuk ekziston!");
            }

            double totali = 0;
            int idMagProd = 0, idMagRec = 0;
            if (!String.IsNullOrEmpty(kodmagpro))
            {
                if (!clsNjesiAdministrative.ekziston(kodmagpro, idNder))
                {
                    return new clsMesazh(false, "Magazina me kod " + kodmagpro + " nuk ekziston!");
                }

                clsNjesiAdministrative magProd = new clsNjesiAdministrative(kodmagpro, idNder, idPer);
                if (magProd.IdNjesiAdministrative <= 0)
                    return new clsMesazh(false, "Ju nuk keni autorizim ne magazinen me kod " + kodmagpro + "!");
                if (!magProd.Aktiv)
                    return new clsMesazh(false, "Magazina me kod " + kodmagpro + " nuk eshte aktive!");
                idMagProd = magProd.IdNjesiAdministrative;
            }

            if (!String.IsNullOrEmpty(kodmagrec))
            {
                if (!clsNjesiAdministrative.ekziston(kodmagpro, idNder))
                {
                    return new clsMesazh(false, "Magazina me kod " + kodmagpro + " nuk ekziston!");
                }
                clsNjesiAdministrative magRec = new clsNjesiAdministrative(kodmagrec, idNder, idPer);
                if (magRec.IdNjesiAdministrative <= 0)
                    return new clsMesazh(false, "Ju nuk keni autorizim ne magazinen me kod " + kodmagpro + "!");
                if (!magRec.Aktiv)
                    return new clsMesazh(false, "Magazina me kod " + kodmagpro + " nuk eshte aktive!");
                idMagRec = magRec.IdNjesiAdministrative;
            }

            int idNjesiProdh = 0;
            if (kodNjesiProdhimi != "")
            {
                if (!clsNjesiProdhimi.ekzistonSipasKodit(kodNjesiProdhimi, idNder))
                    return new clsMesazh(false, "Njesia e prodhimit " + kodNjesiProdhimi + " nuk ekziston!");
                if (!clsNjesiProdhimi.eshteAktiveNjesiProdhimiSipasKodit(kodNjesiProdhimi, idNder))
                    return new clsMesazh(false, "Njesia e prodhimit " + kodNjesiProdhimi + " nuk eshte aktive!");
                idNjesiProdh = clsNjesiProdhimi.ktheIdNjesiProdhimiSipasKodit(kodNjesiProdhimi, idNder);
            }            
            return krijoEkzekutim(idNivel, konfigAmb.IdKonfigAmbjente, idMagProd, kodmagpro, idMagRec, kodmagrec, dtDk, nrDk, 0, idSt, idNder, idNdVt, idPer, dtRegj, shenim, 0, 0, 0, totali, idGrup1, idGrup2, idGrup3, coltrupi, idNjesiProdh, kodNjesiProdhimi, true);
        }

        /// <summary>
        /// kontrollon nese dokumenti eshte i lidhur
        /// </summary>
        /// <returns> true ose false</returns>
        public bool eshteILidhur()
        {
            DbAdmin.clsDatabaseAdmin dbAdmin = new DbAdmin.clsDatabaseAdmin();
            bool lidhur = dbAdmin.eshteDokumentiILidhur(idKokaEkzekutim, idNivel, "T_KOKAEKZEKUTIMPRODHIMI", "IDKOKA");
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
            DataTable dt = dbAdmin.MerrDokLidhur(idKokaEkzekutim, idNivel, "T_KOKAEKZEKUTIMPRODHIMI", "IDKOKA");
            dbAdmin.Dispose();
            return dt;
        }

        /// <summary>
        /// Ruan nje objekt dokumenti planifikimit sebashku me trupin  
        /// perdoret ne rastin e shtimit kur duhet te update-ojme vetem totalin
        /// </summary>
        /// <param name="dtDk"> data e dokumentit te planifikimit</param>
        /// <param name="dtRegj"> data e regjistrimit te dokumentit te planifikimit</param>
        /// <param name="idmagprodukt"> id e klient furnitorit me te cilin lidhet</param>
        /// <param name="idKonf"> id e konfigurimit te dokumentit</param>
        /// <param name="idLidhes">id e dokumentit nga e cila gjenerohet ne rastet e modifikimit </param>
        /// <param name="idmagreceptura">id e magazines</param>
        /// <param name="idkoka">id ritese e kokes se planifikimit</param>
        /// <param name="idNder"> id e ndermarjes</param>
        /// <param name="idNdVt"> id e ndermarje vitit</param>
        /// <param name="idNiv"> id e nivelit te regjistrimit</param>
        /// <param name="idPer"> id e perdoruesit se e ben regjistrimin</param>
        /// <param name="idSt"> id e statusit te dokumentit</param>
        /// <param name="nrDk">nr i dokumentit</param>
        /// <param name="shenim"> shenime </param>
        /// <param name="idGjenerues">id e dokumentit nga gjenerohet planifikimi nga nje ambjent tjeter</param>
        /// <param name="idKonfigGjenerues">id e konfigurimit nga gjenerohet dokumenti</param>
        /// <param name="idNivelGjenerues">id e nivelit nga gjenerohet dokumenti</param>
        /// <param name="ocolTrupiPlanifikim">kolektion i trupit te planifikimit</param>
        /// <param name="dbRegj"> clsdatabazeprodhimi per transaksionin</param>
        /// 
        /// <returns> kthen nje obj clsMesazh per te identifikuar statusin e ruajtjes se te dhenave ne DB</returns>
        public static clsMesazh ruajTotalEkzekutim(out int idkoka, int idNiv, int idKonf, int idmagprodukt, int idmagreceptura, DateTime dtDk, string nrDk, int idLidhes, int idSt, int idNder, int idNdVt, int idPer, DateTime dtRegj, string shenim, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues, double totali, colProduktProdhimi colprodukt, clsDatabazeProdhimi dbRegj, colPlanifikimEkzekutim colpanekze, int idperiudha, bool mekontabilizim, DbShare.clsKonfigurimAmbjenti konfmagdalje, DbShare.clsKonfigurimAmbjenti konfmaghyrje, int iddokngamag, int iddokngamag2, string kodmagprodukt, string kodmagrec, int idDokNgaFK, int idgrup1, int idgrup2, int idgrup3, out string shfaqmesazhapolupe, int iddokngaqendra, DbQendraKosto.colTrupiQendraKosto trupivjeterqendra, bool eshteOwn, int idGjuha, ResourceManager rm, CultureInfo ci, int idkrijuesi, int idNjesiProdhimi, bool modifikim)
        {
            clsMesazh mesazh;
            string shfaqmesazhapolupemag = "jo";
            shfaqmesazhapolupe = "jo";
         string   mesazhmevonshem = "";
            idkoka = 0;
            try
            {
                clsDatabaseRegjistrim db = new clsDatabaseRegjistrim(dbRegj );
                mesazh = dbRegj.ruajKokaEkzekutim(out idkoka, idNiv, idKonf, idmagprodukt, idmagreceptura, dtDk, nrDk, idLidhes, idSt, idNder, idNdVt, idPer, dtRegj, shenim, idNivelGjenerues, idKonfigGjenerues, idGjenerues, totali, idgrup1, idgrup2, idgrup3, idNjesiProdhimi);
                if (!mesazh.Status)
                    return new clsMesazh(false, STR_NdodhiNjeGabimGjateRuajtjesSeKokesSePlanifikimit);
                foreach (clsPlanifikimEkzekutim konv in colpanekze)
                {
                    konv.IdEkzekutimi = idkoka;
                    konv.IdKonfigAmbjenteEkzekutimi = idKonf;
                    mesazh = dbRegj.ruajPlanifikimEkzekutim(0, konv.IdPlanifikimi, konv.IdEkzekutimi, konv.IdKonfigAmbjentePlanifikimi, konv.IdKonfigAmbjenteEkzekutimi);
                    if (!mesazh.Status)
                        return mesazh;
                }
                clsKokaMagazina magdalje = new clsKokaMagazina();
                clsKokaMagazina maghyrje = new clsKokaMagazina();
                if (konfmagdalje.IdKonfigAmbjente != 0)
                {
                    mesazh = krijoMagazineNgaEkzekutimi(magdalje, nrDk, dtDk, dtRegj, idPer, idNder, idNdVt, idkoka, idKonf, idNiv, colprodukt, shenim, iddokngamag, idSt, false, konfmagdalje, idmagreceptura, kodmagrec, idgrup1, idgrup2, idgrup3, idkrijuesi, dbRegj);
                    if (!mesazh.Status)
                        return mesazh;
                }
                if (magdalje.NrDok != null && magdalje.OcolTrupiMagazina.Count > 0)
                {
                    clsDatabaseShare dbshare = new clsDatabaseShare(db );
                    bool gjithmone = false;
                    //clsKusht kushtgj = new clsKusht(magdalje.IdKonfigAmbjente, "GJKGJ", dbshare);
                    //clsAlternativaKushti alt = new clsAlternativaKushti(kushtgj.Vlera, dbshare);
                    if (clsAlternativaKushti.getAlternativa(magdalje.IdKonfigAmbjente, "GJKGJ", dbshare) == "Po") gjithmone = true;
                    magdalje.NrDok = nrDk;
                    magdalje.IdGjenerues = idkoka;

                    mesazh = magdalje.ruaj(false, 0, null, idperiudha, "", 0, 0, db, out shfaqmesazhapolupemag, 0, new DbQendraKosto.colTrupiQendraKosto(), eshteOwn, new DbAsete.colSerialetMagazine(), new DbAsete.colSerialetMagazine(), new DbShare.clsKonfigurimAmbjenti(), new DbShare.clsKonfigurimAmbjenti(), 0, 0, new DbQendraKosto.colTrupiQendraKosto(), false, new clsKokaShitje(), false, gjithmone, new DbAsete.colAmortizimiKoka(), 0, false, modifikim, new colTrupiMagazina(), new DbAsete.colAmortizimiKoka(), new int[0],false,false,false, out mesazhmevonshem, null ,false, false);
                    if (!mesazh.Status)
                        return mesazh;
                }
                double shumatotale = 0; int i = 0;
                foreach (clsProduktProdhimi o in colprodukt)
                {
                   
                    double shuma = 0;
                    o.IdKoka = idkoka;
                    foreach (clsRecepturaProdhimi r in o.ColReceptura)
                    {
                        if (r.Lloji == 2)
                        {
                            shuma += r.KostoTotale; continue;
                        }

                       
                        if (r.SasiaAktuale > 0) 
                        {
                            r.Kosto = magdalje.OcolTrupiMagazina[i].Cmimi;
                            r.KostoTotale = magdalje.OcolTrupiMagazina[i].Vlefta;
                            shuma += r.KostoTotale;
                            
                        }
                        else
                        {
                            shuma += r.KostoTotale;
                        }
                        i++;
                    }
                    o.Kosto = shuma / o.SasiaAktuale;
                    o.KostoTotale = shuma;
                    shumatotale += shuma;
                    int idM;
                    mesazh = dbRegj.ruajProduktProdhimi(out idM, o.IdKoka, o.IdArtikulli, o.IdNjesia, o.SasiaPlanifikuar, o.SasiaAktuale, o.Kosto, o.IdMag, o.GjeresiPlanifikuar, o.GjatesiPlanifikuar, o.SasiPermase, o.IdPlanifikim, o.IdUrdherPorosi, o.IdDetajim1, o.IdDetajim2);
                    if (!mesazh.Status)
                    {
                        return mesazh;
                    }
                    o.Id = idM;
                    foreach (clsRecepturaProdhimi r in o.ColReceptura)
                    {
                        r.IdProdukti = idM;
                        int idr;
                        mesazh = dbRegj.ruajRecepturaProdhimi(out idr, r.IdProdukti, r.IdArtikulli, r.IdBurimi, r.Sasia, r.Scrap, r.Kosto, r.IdMag, r.Lloji, r.SasiaAktuale, r.NjesiArtikull, r.FiroPerqindje, r.IdDetajimi, r.IdDetajimi2);
                        if (!mesazh.Status)
                        {
                            return mesazh;
                        }
                        if (!String.IsNullOrEmpty(r.Skedulim))
                        {
                            string[] skedulim = r.Skedulim.Split(',');
                            for (int s = 0; s < skedulim.Length; s++)
                            {
                                if (skedulim[s] != "" && skedulim[s] != " ")
                                {
                                    int idlidhes;
                                    mesazh = dbRegj.ruajLidhesRecepturaSkedulim(out idlidhes, idr, int.Parse(skedulim[s]));
                                    if (!mesazh.Status)
                                    {
                                        return mesazh;
                                    }
                                }
                            }
                        }
                    }
                }
                mesazh = dbRegj.modifikoTotalKokaEkzekutim(idkoka, shumatotale);

              
                if (konfmaghyrje.IdKonfigAmbjente != 0)
                {
                    mesazh = krijoMagazineNgaEkzekutimi(maghyrje, nrDk, dtDk, dtRegj, idPer, idNder, idNdVt, idkoka, idKonf, idNiv, colprodukt, shenim, iddokngamag2, idSt, true, konfmaghyrje, idmagprodukt, kodmagprodukt, idgrup1, idgrup2, idgrup3, idkrijuesi, dbRegj);
                    if (!mesazh.Status)
                        return mesazh;
                }

                

                if (maghyrje.NrDok != null)
                {

                    maghyrje.NrDok = nrDk;
                    maghyrje.IdGjenerues = idkoka;

                    mesazh = maghyrje.ruaj(false, 0, null, idperiudha, "", 0, 0, db, out shfaqmesazhapolupemag, 0, new DbQendraKosto.colTrupiQendraKosto(), eshteOwn, new DbAsete.colSerialetMagazine(), new DbAsete.colSerialetMagazine(), new DbShare.clsKonfigurimAmbjenti(), new DbShare.clsKonfigurimAmbjenti(), 0, 0, new DbQendraKosto.colTrupiQendraKosto(), false, new clsKokaShitje(), false, false, new DbAsete.colAmortizimiKoka(), 0, false, modifikim, new colTrupiMagazina(), new DbAsete.colAmortizimiKoka(), new int[0],false,false,false, out mesazhmevonshem, null, false, false);
                    if (!mesazh.Status)
                        return mesazh;
                }

                if (mekontabilizim)
                {
                    string perfk = "";
                    if (shenim != String.Empty)
                        perfk = shenim;
                    else
                    {
                        perfk = STR_NgaProcesiIProdhimit;
                    }
                    int idLlojDok = 40;
                    int idkategoria = 45;
                    DbQendraKosto.colObjektivaKosto objektivat;
                    List<double> vleratobjektiva; List<double> vleratobjektivamonbaze;
                    List<int> idllogobj;
  clsDatabaseKontabilitet dbKont = new clsDatabaseKontabilitet(dbRegj );
                    clsKokaFleteKontabel fletekont = DbKontabiliteti.clsKokaFleteKontabel.gjeneroKontabilizimProdhim(idkoka, idNiv, idKonf, dtDk, nrDk, idNder, idNdVt, idPer, dtRegj, colprodukt, perfk, idDokNgaFK, idLlojDok, idperiudha, idkategoria, out objektivat, out vleratobjektiva, out vleratobjektivamonbaze, out idllogobj, 0, 0, 0, out  shfaqmesazhapolupe, iddokngaqendra, trupivjeterqendra, idGjuha, rm, ci, dbKont);
                    if (fletekont.NrDukumentiKokaFleteKontabel != null)
                    {
                        fletekont.NrDukumentiKokaFleteKontabel = nrDk;
                        fletekont.IdGjenerues = idkoka;
                      
                        mesazh = fletekont.Ruaj(dbKont);
                        if (!mesazh.Status)
                        {
                            return mesazh;
                        }

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
        /// Ruan objektin e  kokes se dokumentit te ekzekutimit ne tabelen perkatese ne databaze
        /// </summary>
        /// <param name="hfNrAutoregjistrime">hiddenField i Devit qe mban numrat automatike,null nese nuk perodren</param>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
        public clsMesazh ruaj(IDictionary<string, object> hfNrAutoregjistrime, colPlanifikimEkzekutim colpan, int idperiudha, bool mekontabilizim, DbShare.clsKonfigurimAmbjenti konfmagdalje, DbShare.clsKonfigurimAmbjenti konfmaghyrje, out  string shfaqmesazhapolupe, bool eshteOwn, int idGjuha, ResourceManager rm, CultureInfo ci, bool vjenNgaImportSQL, string idDokImport, string emerTabKoka, string primaryKeyEmerFushe, string emerFusheNdermarrje, bool modifikim)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            //db.krijoManager();
            db.beginTransaksion(0);
            try
            {
                clsMesazh u_ruajt = ruaj(hfNrAutoregjistrime, db, colpan, idperiudha, mekontabilizim, konfmagdalje, konfmaghyrje, 0, 0, 0, out shfaqmesazhapolupe, eshteOwn, idGjuha, rm, ci, modifikim); //perdor ruajtjen me transaksion

                if (!u_ruajt.Status)
                {
                    db.rollbackTransaksion();
                    return u_ruajt;
                }
                if (vjenNgaImportSQL && idDokImport != string.Empty)
                {
                    DbRegjistrim.clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim(db );
                    int statusi = 1;
                    u_ruajt = dbRegj.updateDokTabeleTemportal(idDokImport, idNdermarrje, statusi, emerTabKoka, primaryKeyEmerFushe, emerFusheNdermarrje);
                    if (!u_ruajt.Status)
                    {
                        db.rollbackTransaksion();
                        return u_ruajt;
                    }
                }
                db.commitTransaksion();
                return u_ruajt;
            }
            catch (Exception ex)
            {
                shfaqmesazhapolupe = "";
                db.rollbackTransaksion();
                return new clsMesazh(false, ex.Message);
            }
        }

        /// <summary>
        /// Ruan objektin e  kokes se dokumentit te ekzekutimit ne tabelen perkatese ne databaze
        /// </summary>
        /// <param name="hfNrAutoregjistrime">hiddenField i Devit qe mban numrat automatike,null nese nuk perodren</param>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
        public clsMesazh ruaj(IDictionary<string, object> hfNrAutoregjistrime, clsDatabazeProdhimi db, colPlanifikimEkzekutim colpan, int idperiudha, bool mekontabilizim, DbShare.clsKonfigurimAmbjenti konfmagdalje, DbShare.clsKonfigurimAmbjenti konfmaghyrje, int iddokngamag, int iddokngamag2, int idDokNgaFK, out string shfaqmesazhapolupe, bool eshteOwn, int idGjuha, ResourceManager rm, CultureInfo ci, bool modifikim)
        {
            shfaqmesazhapolupe = "jo";
            bool kaNdryshimNumri;
            clsMesazh mesazhKontrolli = kontrolloEkzekutim(out kaNdryshimNumri, db, hfNrAutoregjistrime);
            if (!mesazhKontrolli.Status)
            {
                return mesazhKontrolli;
            }
            int id = 0;
            // thirret ruajTotalEkzekutimi sepse ne rastin e modifikimit kur ruhet nje rekord i ri me ndryshimet qe bejme DtModifikimi duhet te jete null, ruhet DtModifikimi te rekordi mbi te cilin behen modifikimet
            clsMesazh u_ruajt = clsKokaEkzekutim.ruajTotalEkzekutim(out id, IdNivel, IdKonfigAmbjente, IdMagProdukti, IdMagReceptura, DtDok, NrDok, IdDokNga, IdStatusDok, IdNdermarrje, IdNdermarrjeVit, IdPerdoruesi, DtRegj, Shenime, IdNivelGjenerues, IdKonfigGjenerues, IdGjenerues, totali, colProdukte, db, colpan, idperiudha, mekontabilizim, konfmagdalje, konfmaghyrje, iddokngamag, iddokngamag2, kodMagProdukti, kodMagReceptura, idDokNgaFK, IdGrup1, IdGrup2, IdGrup3, out  shfaqmesazhapolupe, 0, new DbQendraKosto.colTrupiQendraKosto(), eshteOwn, idGjuha, rm, ci, idPerdoruesi, idNjesiProdhimi, modifikim);
            idKokaEkzekutim = id;
            if (!u_ruajt.Status)
            {
                return u_ruajt;
            }
            if (kaNdryshimNumri)
                return mesazhKontrolli;
            return u_ruajt;
        }

        /// <summary>
        /// kontrollon dokumentin e ekzekutimit gjate ruajtjes dhe ben ndryshimet per nr automatik
        /// </summary>
        /// <param name="kaNdryshimNumri">tregon nese ka ndryshuar numri</param>
        /// <param name="dbProdhim">clsdatabazeprodhimi per transaksion</param>
        /// <param name="hfNrAutoregjistrime">hiddenfield me fushat e nr automatik</param>
        /// <returns> clsMesazh </returns>
        private clsMesazh kontrolloEkzekutim(out bool kaNdryshimNumri, clsDatabazeProdhimi dbProdhim, IDictionary<string, object> hfNrAutoregjistrime)
        {
            kaNdryshimNumri = false;
            clsMesazh mes = new clsMesazh();

            if (hfNrAutoregjistrime != null)
            {
                mes = kontrolloNrAutoEkzekutim(out kaNdryshimNumri, dbProdhim, hfNrAutoregjistrime);
                if (!mes.Status)
                    return mes;
            }
            if (dbProdhim.ekzistonRegjistrimEkzekutimi(idKonfigAmbjente, nrDok, dtDok, idNdermarrje))
                return new clsMesazh(false, STR_EkzistonNjeRegjistrimMeTeNjejtinNumerDokumenti);
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
        private clsMesazh kontrolloNrAutoEkzekutim(out bool kaNdryshimNumri, clsDatabazeProdhimi dbRegj, IDictionary<string, object> hfregjistrime)
        {
            clsDatabaseAdmin db = new clsDatabaseAdmin(dbRegj );
            List<NrAuto> list = DbAdmin.clsNrAutom.kontrollogjithenumrat(db, hfregjistrime, dtDok);
            if (NrAuto.ktheVlerenEre(list, "NrDok") != "")
                nrDok = NrAuto.ktheVlerenEre(list, "NrDok");
            DbCore.clsMesazh mes = NrAuto.ruajvlera(out kaNdryshimNumri, list, dtDok, idPerdoruesi, idNdermarrje, db);
            return new clsMesazh(mes.Status, mes.PershkrimMesazhi);
        }

        /// <summary>
        /// Modifikon nje objekt dokument ekzekutimi sebashku me te produktet, recepturat, kontabilitetin dhe dokumentat e magazines 
        /// </summary>
        /// <param name="dtDk"> data e dokumentit te ekzekutimit</param>
        /// <param name="dtRegj"> data e regjistrimit te dokumentit te ekzekutimit</param>
        /// <param name="idmagprod"> id e magazines se produktit</param>
        /// <param name="idKonf"> id e konfigurimit te dokumentit</param>
        /// <param name="idLidhes">id e dokumentit nga e cila gjenerohet ne rastet e modifikimit </param>
        /// <param name="idmagrec">id e magazines se recepturave</param>
        /// <param name="idkoka">id ritese e kokes se ekzekutimit</param>
        /// <param name="idNder"> id e ndermarjes</param>
        /// <param name="idNdVt"> id e ndermarje vitit</param>
        /// <param name="idNiv"> id e nivelit te regjistrimit</param>
        /// <param name="idPer"> id e perdoruesit se e ben regjistrimin</param>
        /// <param name="idSt"> id e statusit te dokumentit</param>
        /// <param name="nrDk">nr i dokumentit</param>
        /// <param name="shenim"> shenime </param>
        /// <param name="idGjenerues">id e dokumentit nga gjenerohet planifikimi nga nje ambjent tjeter</param>
        /// <param name="idKonfigGjenerues">id e konfigurimit nga gjenerohet dokumenti</param>
        /// <param name="idNivelGjenerues">id e nivelit nga gjenerohet dokumenti</param>
        /// <param name="colProdukt">kolektion i me produktet</param>
        /// <param name="dbProdh">clsDataBazeProdhimi per transaksionet</param>
        /// <param name="totali"> totali i dokumentit</param>
        /// <returns> kthen nje obj clsMesazh per te identifikuar statusin e modifikimit se te dhenave ne DB</returns>
        public static clsMesazh modifikoEkzekutim(int idkoka, int idNiv, int idKonf, int idmagprod, int idmagrec, DateTime dtDk, string nrDk, int idLidhes, int idSt, int idNder, int idNdVt, int idPer, DateTime dtRegj, string shenim, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues, double totali, colProduktProdhimi colProdukt, clsDatabazeProdhimi dbProdh, colPlanifikimEkzekutim colplan, int idperiudha, bool mekontabilizim, DbShare.clsKonfigurimAmbjenti konfmagdalje, DbShare.clsKonfigurimAmbjenti konfmaghyrje, string kodmagprodukt, string kodmagrec, int idgrup1, int idgrup2, int idgrup3, out  string shfaqmesazhapolupe, out int idkokare, bool eshteOwn, int idGjuha, ResourceManager rm, CultureInfo ci, clsKokaEkzekutim koka, int idNjesiProdhimi, bool modifikim)
        {
            idkokare = 0;
            shfaqmesazhapolupe = "jo";
            clsMesazh mesazh = new clsMesazh();
            clsDatabaseRegjistrim db = new clsDatabaseRegjistrim(dbProdh );
            //db.vendosManager(dbProdh );
            clsDatabaseKontabilitet dbkont = new clsDatabaseKontabilitet(dbProdh );
            DbInventari.clsDatabaseInventari dbinv = new DbInventari.clsDatabaseInventari(dbProdh );
            try
            {   //obj koka qe i kalohet si parameter eshte si duhet te modifikohet
                clsKokaEkzekutim kokaEkzistuese = new clsKokaEkzekutim();
                DbQendraKosto.clsDatabaseQendraKosto dbqendra = new DbQendraKosto.clsDatabaseQendraKosto(dbProdh );
                kokaEkzistuese.mbushKokaEkzekutimSipasID(idkoka, dbProdh);

                if (string.IsNullOrEmpty(kokaEkzistuese.NrDok) || kokaEkzistuese.IdStatusDok == 2)
                {
                    return new clsMesazh(false, STR_DokumentiKaNdryshuarJuLutemRihapeniPerseri);
                }
                mesazh = dbProdh.fshiLidhesSkedulimReceptura(kokaEkzistuese.idKokaEkzekutim);
                if (!mesazh.Status)
                {
                    return mesazh;
                }
                kokaEkzistuese.IdStatusDok = 2;  //duhet vendosur nje status i pershtatshem per kete modifikim
                idLidhes = kokaEkzistuese.IdKokaEkzekutim;
                mesazh = dbProdh.modifikoKokaEkzekutim(kokaEkzistuese.IdKokaEkzekutim, kokaEkzistuese.IdNivel, kokaEkzistuese.IdKonfigAmbjente, kokaEkzistuese.IdMagProdukti, kokaEkzistuese.IdMagReceptura, kokaEkzistuese.DtDok, kokaEkzistuese.NrDok, kokaEkzistuese.idDokNga, kokaEkzistuese.IdStatusDok, kokaEkzistuese.idNdermarrje, kokaEkzistuese.idNdermarrjeVit, idPer, kokaEkzistuese.DtRegj, kokaEkzistuese.Shenime, kokaEkzistuese.idNivelGjenerues, kokaEkzistuese.idKonfigGjenerues, kokaEkzistuese.idGjenerues, kokaEkzistuese.totali, kokaEkzistuese.idGrup1, kokaEkzistuese.idGrup2, kokaEkzistuese.IdGrup3, kokaEkzistuese.idNjesiProdhimi);

                if (!mesazh.Status)
                {
                    return mesazh;
                }
                mesazh = clsKokaEkzekutim.kaloNeHistorikKokaEkzekutim(kokaEkzistuese.idKokaEkzekutim, dbProdh);
                if (!mesazh.Status)
                {
                    return mesazh;
                }
                mesazh = dbProdh.fshiSipasIdEkzekutimi(kokaEkzistuese.IdKokaEkzekutim);
                if (!mesazh.Status)
                {
                    return mesazh;
                }

                kokaEkzistuese.OMagazineDalje.mbushKokaMagazinaSipasIDGjenerues(kokaEkzistuese.IdKokaEkzekutim, 2, kokaEkzistuese.IdKonfigAmbjente, db);
                kokaEkzistuese.oMagazineHyrje.mbushKokaMagazinaSipasIDGjenerues(kokaEkzistuese.IdKokaEkzekutim, 1, kokaEkzistuese.IdKonfigAmbjente, db);
                kokaEkzistuese.OMagazineDalje.mbushTrupMagazine(db);
                colArtikujt coleksistues = new colArtikujt(kokaEkzistuese.OMagazineDalje.IdKokaMagazina, dbinv);
                int i = 0;
                foreach (clsTrupiMagazina trup in kokaEkzistuese.OMagazineDalje.OcolTrupiMagazina)
                {
                    if (trup.IdLlojVeprimi == 1)
                        trup.Element = coleksistues[i];
                    i++;
                }
                mesazh = kokaEkzistuese.OMagazineDalje.kontrolloGjendjeNeFshirje(db, ruajTrupinEMagazines(colProdukt, -1, false, idNder, dtDk, dbinv), idSt);
                if (!mesazh.Status)
                    return mesazh;
                kokaEkzistuese.oMagazineHyrje.mbushTrupMagazine(db);
                colArtikujt coleksistues1 = new colArtikujt(kokaEkzistuese.oMagazineHyrje.IdKokaMagazina, dbinv);
                int i2 = 0;
                foreach (clsTrupiMagazina trup in kokaEkzistuese.oMagazineHyrje.OcolTrupiMagazina)
                {
                    if (trup.IdLlojVeprimi == 1)
                        trup.Element = coleksistues1[i2];
                    i2++;
                }
                mesazh = kokaEkzistuese.oMagazineHyrje.kontrolloGjendjeNeFshirje(db, ruajTrupinEMagazines(colProdukt, 1, true, idNder, dtDk, dbinv), idSt);
                if (!mesazh.Status)
                    return mesazh;
                clsKokaFleteKontabel newclsKokaFleteKontabel = new clsKokaFleteKontabel(kokaEkzistuese.IdKokaEkzekutim, 45, dbkont);
                if (newclsKokaFleteKontabel.NrDukumentiKokaFleteKontabel != null)
                {
                    kokaEkzistuese.OFleteKontabel = newclsKokaFleteKontabel;
                   
                }

                if (kokaEkzistuese.OFleteKontabel.IdKokaFleteKontabel != 0)
                {
                    mesazh = kokaEkzistuese.oFleteKontabel.fshiupd(dbkont);
                    if (!mesazh.Status)
                        return mesazh;
                }
                bool kaveprimepas = false;
                if (kokaEkzistuese.OMagazineDalje.IdKokaMagazina != 0)
                {
                   
                    mesazh = kokaEkzistuese.OMagazineDalje.fshiMagazina(kokaEkzistuese.OMagazineDalje.IdKokaMagazina, idPer, db,false,false,false, new DbAsete.colSerialetMagazine (), out kaveprimepas,true);
                    if (!mesazh.Status)
                    {
                        return mesazh;
                    }
                    
                }
                if (kokaEkzistuese.OMagazineHyrje.IdKokaMagazina != 0)
                {
               
                    mesazh = kokaEkzistuese.OMagazineHyrje.fshiMagazina(kokaEkzistuese.OMagazineHyrje.IdKokaMagazina, idPer, db,false,false,false, new DbAsete.colSerialetMagazine (), out kaveprimepas,true);
                    if (!mesazh.Status)
                    {
                        return mesazh;
                    }
                    
                }

                mesazh = ruajTotalEkzekutim(out idkoka, idNiv, idKonf, idmagprod, idmagrec, dtDk, nrDk, idLidhes, idSt, idNder, idNdVt, idPer, dtRegj, shenim, idNivelGjenerues, idKonfigGjenerues, idGjenerues, totali, colProdukt, dbProdh, colplan, idperiudha, mekontabilizim, konfmagdalje, konfmaghyrje, kokaEkzistuese.OMagazineDalje.IdKokaMagazina, kokaEkzistuese.oMagazineHyrje.IdKokaMagazina, kodmagprodukt, kodmagrec, kokaEkzistuese.oFleteKontabel.IdKokaFleteKontabel, idgrup1, idgrup2, idgrup3, out   shfaqmesazhapolupe, kokaEkzistuese.OFleteKontabel.KokaQendraKosto.IdKoka, kokaEkzistuese.oFleteKontabel.KokaQendraKosto.ColTrupi, eshteOwn, idGjuha, rm, ci, kokaEkzistuese.oMagazineHyrje.IdKrijuesi, idNjesiProdhimi, modifikim);
                idkokare = idkoka;
                return mesazh;
            }
            catch (Exception ce)
            {
                return new clsMesazh(false, ce.Message);
            }
        }


        /// <summary>
        /// Modifikon nje objekt dokument ekzekutimi sebashku me te produktet, recepturat, kontabilitetin dhe dokumentat e magazines 
        /// </summary>
        /// <param name="dtDk"> data e dokumentit te ekzekutimit</param>
        /// <param name="dtRegj"> data e regjistrimit te dokumentit te ekzekutimit</param>
        /// <param name="idmagprod"> id e magazines se produktit</param>
        /// <param name="idKonf"> id e konfigurimit te dokumentit</param>
        /// <param name="idLidhes">id e dokumentit nga e cila gjenerohet ne rastet e modifikimit </param>
        /// <param name="idmagrec">id e magazines se recepturave</param>
        /// <param name="idkoka">id ritese e kokes se ekzekutimit</param>
        /// <param name="idNder"> id e ndermarjes</param>
        /// <param name="idNdVt"> id e ndermarje vitit</param>
        /// <param name="idNiv"> id e nivelit te regjistrimit</param>
        /// <param name="idPer"> id e perdoruesit se e ben regjistrimin</param>
        /// <param name="idSt"> id e statusit te dokumentit</param>
        /// <param name="nrDk">nr i dokumentit</param>
        /// <param name="shenim"> shenime </param>
        /// <param name="idGjenerues">id e dokumentit nga gjenerohet planifikimi nga nje ambjent tjeter</param>
        /// <param name="idKonfigGjenerues">id e konfigurimit nga gjenerohet dokumenti</param>
        /// <param name="idNivelGjenerues">id e nivelit nga gjenerohet dokumenti</param>
        /// <param name="colProdukt">kolektion i me produktet</param>
        /// <param name="dbProdh">clsDataBazeProdhimi per transaksionet</param>
        /// <param name="totali"> totali i dokumentit</param>
        /// <returns> kthen nje obj clsMesazh per te identifikuar statusin e modifikimit se te dhenave ne DB</returns>
        public static clsMesazh modifikoTotalEkzekutim(int idkoka, int idNiv, int idKonf, int idmagprod, int idmagrec, DateTime dtDk, string nrDk, int idLidhes, int idSt, int idNder, int idNdVt, int idPer, DateTime dtRegj, string shenim, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues, double totali, colProduktProdhimi colProdukt, clsDatabazeProdhimi dbProdh, colPlanifikimEkzekutim colplan, int idperiudha, bool mekontabilizim, DbShare.clsKonfigurimAmbjenti konfmagdalje, DbShare.clsKonfigurimAmbjenti konfmaghyrje, string kodmagprodukt, string kodmagrec, int idgrup1, int idgrup2, int idgrup3, out  string shfaqmesazhapolupe, out int idkokare, bool eshteOwn, int idGjuha, ResourceManager rm, CultureInfo ci, int idNjesiProdhimi, bool modifikim)
        {
            idkokare = 0;
            shfaqmesazhapolupe = "jo";
            clsMesazh mesazh = new clsMesazh();
            clsDatabaseRegjistrim db = new clsDatabaseRegjistrim(dbProdh );
            //db.vendosManager(dbProdh );
            clsDatabaseKontabilitet dbkont = new clsDatabaseKontabilitet(dbProdh );
            DbInventari.clsDatabaseInventari dbinv = new DbInventari.clsDatabaseInventari(dbProdh );
            try
            {   //obj koka qe i kalohet si parameter eshte si duhet te modifikohet
                clsKokaEkzekutim kokaEkzistuese = new clsKokaEkzekutim();
                DbQendraKosto.clsDatabaseQendraKosto dbqendra = new DbQendraKosto.clsDatabaseQendraKosto(dbProdh );
                kokaEkzistuese.mbushKokaEkzekutimSipasID(idkoka, dbProdh);
                if (string.IsNullOrEmpty(kokaEkzistuese.NrDok) || kokaEkzistuese.IdStatusDok == 2)
                {
                    return new clsMesazh(false, STR_DokumentiKaNdryshuarJuLutemRihapeniPerseri);
                }
                mesazh = dbProdh.fshiLidhesSkedulimReceptura(kokaEkzistuese.idKokaEkzekutim);
                if (!mesazh.Status)
                {
                    return mesazh;
                }
                kokaEkzistuese.IdStatusDok = 2;  //duhet vendosur nje status i pershtatshem per kete modifikim
                idLidhes = kokaEkzistuese.IdKokaEkzekutim;
                mesazh = dbProdh.modifikoTotalKokaEkzekutim(kokaEkzistuese.IdKokaEkzekutim, kokaEkzistuese.totali);

                if (!mesazh.Status)
                {
                    return mesazh;
                }
                mesazh = dbProdh.fshiSipasIdEkzekutimi(kokaEkzistuese.IdKokaEkzekutim);
                if (!mesazh.Status)
                {
                    return mesazh;
                }

                kokaEkzistuese.OMagazineDalje.mbushKokaMagazinaSipasIDGjenerues(kokaEkzistuese.IdKokaEkzekutim, 2, kokaEkzistuese.IdKonfigAmbjente, db);
                kokaEkzistuese.oMagazineHyrje.mbushKokaMagazinaSipasIDGjenerues(kokaEkzistuese.IdKokaEkzekutim, 1, kokaEkzistuese.IdKonfigAmbjente, db);
                kokaEkzistuese.OMagazineDalje.mbushTrupMagazine(db);
                colArtikujt coleksistues = new colArtikujt(kokaEkzistuese.OMagazineDalje.IdKokaMagazina, dbinv);
                int i = 0;
                foreach (clsTrupiMagazina trup in kokaEkzistuese.OMagazineDalje.OcolTrupiMagazina)
                {
                    if (trup.IdLlojVeprimi == 1)
                        trup.Element = coleksistues[i];
                    i++;
                }
                mesazh = kokaEkzistuese.OMagazineDalje.kontrolloGjendjeNeFshirje(db, ruajTrupinEMagazines(colProdukt, -1, false, idNder, dtDk, dbinv), idSt);
                if (!mesazh.Status)
                    return mesazh;
                kokaEkzistuese.oMagazineHyrje.mbushTrupMagazine(db);
                colArtikujt coleksistues1 = new colArtikujt(kokaEkzistuese.oMagazineHyrje.IdKokaMagazina, dbinv);
                int i2 = 0;
                foreach (clsTrupiMagazina trup in kokaEkzistuese.oMagazineHyrje.OcolTrupiMagazina)
                {
                    if (trup.IdLlojVeprimi == 1)
                        trup.Element = coleksistues1[i2];
                    i2++;
                }
                mesazh = kokaEkzistuese.oMagazineHyrje.kontrolloGjendjeNeFshirje(db, ruajTrupinEMagazines(colProdukt, 1, true, idNder, dtDk, dbinv), idSt);
                if (!mesazh.Status)
                    return mesazh;
                clsKokaFleteKontabel newclsKokaFleteKontabel = new clsKokaFleteKontabel(kokaEkzistuese.IdKokaEkzekutim, 45, dbkont);
                if (newclsKokaFleteKontabel.NrDukumentiKokaFleteKontabel != null)
                {
                    kokaEkzistuese.OFleteKontabel = newclsKokaFleteKontabel;
                    

                }

                if (kokaEkzistuese.OFleteKontabel.IdKokaFleteKontabel != 0)
                {
                    mesazh = kokaEkzistuese.oFleteKontabel.fshiupd(dbkont);
                    if (!mesazh.Status)
                        return mesazh;
                }
                bool kaveprimepas = false;
                if (kokaEkzistuese.OMagazineDalje.IdKokaMagazina != 0)
                {
             
                    mesazh = kokaEkzistuese.OMagazineDalje.fshiMagazina(kokaEkzistuese.OMagazineDalje.IdKokaMagazina, idPer, db,false,false,false, new DbAsete.colSerialetMagazine (), out kaveprimepas,true);
                    if (!mesazh.Status)
                    {
                        return mesazh;
                    }
                    
                }
                if (kokaEkzistuese.OMagazineHyrje.IdKokaMagazina != 0)
                {

                    mesazh = kokaEkzistuese.OMagazineHyrje.fshiMagazina(kokaEkzistuese.OMagazineHyrje.IdKokaMagazina, idPer, db,false,false,false, new DbAsete.colSerialetMagazine (), out kaveprimepas,true);
                    if (!mesazh.Status)
                    {
                        return mesazh;
                    }
                  
                }
                mesazh = ruajTotalEkzekutim(out idkoka, idNiv, idKonf, idmagprod, idmagrec, dtDk, nrDk, idLidhes, idSt, idNder, idNdVt, idPer, dtRegj, shenim, idNivelGjenerues, idKonfigGjenerues, idGjenerues, totali, colProdukt, dbProdh, colplan, idperiudha, mekontabilizim, konfmagdalje, konfmaghyrje, kokaEkzistuese.OMagazineDalje.IdKokaMagazina, kokaEkzistuese.oMagazineHyrje.IdKokaMagazina, kodmagprodukt, kodmagrec, kokaEkzistuese.oFleteKontabel.IdKokaFleteKontabel, idgrup1, idgrup2, idgrup3, out   shfaqmesazhapolupe, kokaEkzistuese.OFleteKontabel.KokaQendraKosto.IdKoka, kokaEkzistuese.oFleteKontabel.KokaQendraKosto.ColTrupi, eshteOwn, idGjuha, rm, ci, idPer, idNjesiProdhimi, modifikim);
                idkokare = idkoka;
                return mesazh;
            }
            catch (Exception ce)
            {
                return new clsMesazh(false, ce.Message);
            }
        }

        /// <summary>
        /// Modifikon objektin e  kokes se dokumentit te ekzekutimit ne tabelen perkatese ne databaze
        /// </summary>
        /// <param name="lidhur">tregon nese eshte i lidhur ose jo</param>
        /// <returns > nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo</returns>
        public clsMesazh modifiko(bool lidhur, colPlanifikimEkzekutim colplan, int idperiudha, bool mekontabilizim, DbShare.clsKonfigurimAmbjenti konfmagdalje, DbShare.clsKonfigurimAmbjenti konfmaghyrje, out  string shfaqmesazhapolupe, bool eshteOwn, int idGjuha, ResourceManager rm, CultureInfo ci, bool modifikim)
        {
            shfaqmesazhapolupe = "jo";
            clsMesazh u_modifikua;
            clsDatabazeProdhimi data = new clsDatabazeProdhimi();
            if (lidhur == false)
            {
                //data.krijoManager();
                data.beginTransaksion(0);
                int idkokare = 0;
                clsKokaEkzekutim kokaEkzistuese = new clsKokaEkzekutim();
                kokaEkzistuese.mbushKokaEkzekutimSipasID(this.IdKokaEkzekutim);
                u_modifikua = modifikoEkzekutim(IdKokaEkzekutim, IdNivel, IdKonfigAmbjente, IdMagProdukti, IdMagReceptura, DtDok, NrDok, IdDokNga, IdStatusDok, IdNdermarrje, IdNdermarrjeVit, IdPerdoruesi, DtRegj, Shenime, IdNivelGjenerues, IdKonfigGjenerues, IdGjenerues, totali, colProdukte, data, colplan, idperiudha, mekontabilizim, konfmagdalje, konfmaghyrje, kodMagProdukti, kodMagReceptura, IdGrup1, IdGrup2, IdGrup3, out shfaqmesazhapolupe, out idkokare, eshteOwn, idGjuha, rm, ci, kokaEkzistuese, idNjesiProdhimi, modifikim);
                idKokaEkzekutim = idkokare;
                if (u_modifikua.Status)
                    data.commitTransaksion();
                else data.rollbackTransaksion();
            }
            else
            {
                u_modifikua = data.modifikoKokaEkzekutim(IdKokaEkzekutim, IdNivel, IdKonfigAmbjente, IdMagProdukti, IdMagReceptura, DtDok, NrDok, idDokNga, IdStatusDok, idNdermarrje, idNdermarrjeVit, idPerdoruesi, DtRegj, Shenime, idNivelGjenerues, idKonfigGjenerues, idGjenerues, totali, idGrup1, idGrup2, idGrup3, idNjesiProdhimi);
                data.Dispose();
            }
            return u_modifikua;
        }
        public static clsMesazh kaloNeHistorikKokaEkzekutim(int idkoka, clsDatabazeProdhimi db)
        {
            clsMesazh mesazh = db.kaloNeHistorikKokaEkzekutim(idkoka);
            return mesazh;
        }

        /// <summary>
        /// Modifikon objektin e  kokes se dokumentit te ekzekutimit ne tabelen perkatese ne databaze kur jemi duke bere shtim. Modifikimi ne shtim perdoret per te marre totalin
        /// </summary>
        /// <param name="lidhur">tregon nese eshte i lidhur ose jo</param>
        /// <returns > nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo</returns>
        public clsMesazh modifikoTotal(bool lidhur, colPlanifikimEkzekutim colplan, int idperiudha, bool mekontabilizim, DbShare.clsKonfigurimAmbjenti konfmagdalje, DbShare.clsKonfigurimAmbjenti konfmaghyrje, out  string shfaqmesazhapolupe, bool eshteOwn, int idGjuha, ResourceManager rm, CultureInfo ci, bool modifikim)
        {
            shfaqmesazhapolupe = "jo";
            clsMesazh u_modifikua;
            clsDatabazeProdhimi data = new clsDatabazeProdhimi();
            if (lidhur == false)
            {
                data.beginTransaksion();
                int idkokare = 0;
                u_modifikua = modifikoTotalEkzekutim(IdKokaEkzekutim, IdNivel, IdKonfigAmbjente, IdMagProdukti, IdMagReceptura, DtDok, NrDok, IdDokNga, IdStatusDok, IdNdermarrje, IdNdermarrjeVit, IdPerdoruesi, DtRegj, Shenime, IdNivelGjenerues, IdKonfigGjenerues, IdGjenerues, totali, colProdukte, data, colplan, idperiudha, mekontabilizim, konfmagdalje, konfmaghyrje, kodMagProdukti, kodMagReceptura, IdGrup1, IdGrup2, IdGrup3, out shfaqmesazhapolupe, out idkokare, eshteOwn, idGjuha, rm, ci, idNjesiProdhimi, modifikim);
                idKokaEkzekutim = idkokare;
                if (u_modifikua.Status)
                    data.commitTransaksion();
                else data.rollbackTransaksion();
            }
            else
            {
                u_modifikua = data.modifikoTotalKokaEkzekutim(IdKokaEkzekutim, totali);
                data.Dispose();
            }
            return u_modifikua;
        }

        /// <summary>
        /// fshin nje objekt dokument ekzekutimit  duke i ndryshuar statusin
        /// thirret prc_T_KOKAEKZEKUTIMPRODHIMI_upddel qe ben ndryshimin e statusDok dhe te idperdorues
        /// </summary>
        ///<param name="idkokaekzekutim"> koka e dokumentit te ekzekutimit i cili do te fshihet</param>
        ///<param name="idperdoruesi">id e perdoruesit qe ka bere veprimin</param>
        ///<param name="dbProdh"> clsdatabaseProdhimi si pjese e transaksionit</param>
        /// <returns> kthen nje obj clsMesazh per te identifikuar statusin e fshirjes se te dhenave ne DB</returns>
        public static clsMesazh fshiEkzekutim(int idkokaekzekutim, int idperdoruesi, clsDatabazeProdhimi dbProdh)
        {

            clsMesazh mesazh = new clsMesazh(true);
            try
            {
                clsDatabaseKontabilitet dbkontab = new clsDatabaseKontabilitet(dbProdh );
                clsDatabaseRegjistrim dbRegjistrim = new clsDatabaseRegjistrim(dbProdh );
                DbQendraKosto.clsDatabaseQendraKosto dbqendra = new DbQendraKosto.clsDatabaseQendraKosto(dbProdh );
                //dbRegjistrim.vendosManager(dbProdh );
                clsKokaEkzekutim kokaEkzistuese = new clsKokaEkzekutim();
                kokaEkzistuese.mbushKokaEkzekutimSipasID(idkokaekzekutim, dbProdh);
                kokaEkzistuese.IdStatusDok = 2;  //duhet vendosur nje status i pershtatshem per kete modifikim
                mesazh = dbProdh.fshiKokaEkzekutim(kokaEkzistuese.IdKokaEkzekutim, idperdoruesi);

                if (!mesazh.Status)
                {
                    return mesazh;
                }
                mesazh = clsKokaEkzekutim.kaloNeHistorikKokaEkzekutim(kokaEkzistuese.idKokaEkzekutim, dbProdh);
                if (!mesazh.Status)
                {
                    return mesazh;
                }
                mesazh = dbProdh.fshiLidhesSkedulimReceptura(kokaEkzistuese.idKokaEkzekutim);
                if (!mesazh.Status)
                {
                    return mesazh;
                }
                kokaEkzistuese.OMagazineDalje.mbushKokaMagazinaSipasIDGjenerues(kokaEkzistuese.IdKokaEkzekutim, 2, kokaEkzistuese.idKonfigAmbjente, dbRegjistrim);
                kokaEkzistuese.oMagazineHyrje.mbushKokaMagazinaSipasIDGjenerues(kokaEkzistuese.IdKokaEkzekutim, 1, kokaEkzistuese.idKonfigAmbjente, dbRegjistrim);
                clsKokaFleteKontabel newclsKokaFleteKontabel = new clsKokaFleteKontabel(kokaEkzistuese.IdKokaEkzekutim, 45, dbkontab);
                if (newclsKokaFleteKontabel.NrDukumentiKokaFleteKontabel != null)
                {
                    kokaEkzistuese.OFleteKontabel = newclsKokaFleteKontabel;
                    
                }
                if (kokaEkzistuese.OFleteKontabel.IdKokaFleteKontabel != 0)
                {
                    mesazh = kokaEkzistuese.oFleteKontabel.fshiupd(dbkontab);
                    if (!mesazh.Status)
                        return mesazh;

                }
                bool kaveprimepas = false;
                if (kokaEkzistuese.OMagazineDalje.IdKokaMagazina != 0)
                {

                    mesazh = kokaEkzistuese.OMagazineDalje.fshiMagazina(kokaEkzistuese.OMagazineDalje.IdKokaMagazina, idperdoruesi, dbRegjistrim,false,false,false, new DbAsete.colSerialetMagazine (), out kaveprimepas,true);
                    if (!mesazh.Status)
                    {
                        return mesazh;
                    }

                }
                if (kokaEkzistuese.OMagazineHyrje.IdKokaMagazina != 0)
                {
            
                    mesazh = kokaEkzistuese.OMagazineHyrje.fshiMagazina(kokaEkzistuese.OMagazineHyrje.IdKokaMagazina, idperdoruesi, dbRegjistrim,false,false,false, new DbAsete.colSerialetMagazine (), out kaveprimepas,true);
                    if (!mesazh.Status)
                    {
                        return mesazh;
                    }

                }
                mesazh = dbProdh.fshiSipasIdEkzekutimi(kokaEkzistuese.IdKokaEkzekutim);
                if (!mesazh.Status)
                {
                    return mesazh;
                }

                return mesazh;

            }
            catch (Exception ce)
            {

                return new clsMesazh(false, ce.Message);
            }
        }

        /// <summary>
        /// Fshin objektin e  kokes se dokumentit te ekzekutimit ne tabelen perkatese ne databaze
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh fshi()
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            //db.krijoManager();
            db.beginTransaksion();
            clsMesazh u_fshi = fshiEkzekutim(IdKokaEkzekutim, idPerdoruesi, db);
            if (u_fshi.Status)
                db.commitTransaksion();
            else db.rollbackTransaksion();
            return u_fshi;
        }

        /// <summary>
        /// Merr objektin e  kokes se dokumentit te ekzekutimit sipas id nga tabela perkatese ne databaze
        /// </summary>
        /// <param name="id"> id e kokes se ekzekutimit</param>
        /// <returns > nje objekt clsKokaEkzekutim qe permban objektin e kerkuar</returns>
        public static clsKokaEkzekutim merrSipasId(int id)
        {
            clsKokaEkzekutim data = new clsKokaEkzekutim();
            //data.mbushKokaEkzekutimSipasID(id, null);
            data.mbushKokaEkzekutimSipasID(id);
            return data;
        }

        /// <summary>
        /// Merr objektin e  kokes se dokumentit te ekzekutimit sipas nr dhe dt dokumenti nga tabela perkatese ne databaze.
        /// </summary>
        /// <returns > nje objekt clsKokaEkzekutim qe permban objektin e kerkuar</returns>
        public clsKokaEkzekutim merrSipasIdNivelNrDokDtDok()
        {
            clsKokaEkzekutim data = new clsKokaEkzekutim(IdNivel, NrDok, DtDok);
            return data;
        }

        /// <summary>
        /// Merr gjithe e  kokat e dokumentave te ekzekutimit sipas ndermarjevitit nga tabela perkatese ne databaze
        /// </summary>
        /// <param name="idNdermVit">id e ndermarje vitit</param>
        /// <returns > nje object colKokaEkzekutim qe permban nje koleksion me gjithe kokat e dokumentave te ekzekutimit te nje ndermarje ne nje vit te caktuar</returns>
        public static colKokaEkzekutim merriTeGjithe(int idNdermVit)
        {
            colKokaEkzekutim data = new colKokaEkzekutim(idNdermVit);
            return data;
        }

        public colProduktProdhimi merrProdukteProdhimi()
        {
            using (clsDatabazeProdhimi db = new clsDatabazeProdhimi())
            {
                colProduktProdhimi trupi = new colProduktProdhimi();
                trupi.mbushProduktSipasIdkoka(IdKokaEkzekutim, db);
                return trupi;
            }
        }

        /// <summary>
        /// Merr produketet e prodhimit te dokumentit nga tabela perkatese ne databaze
        /// </summary>
        /// <param name="db">clsdatabaze prodhimi per raste transaksioni</param>
        /// <returns > nje object colProduktProdhimi qe permban nje koleksion me produktet</returns>
        public colProduktProdhimi merrProdukteProdhimi(clsDatabazeProdhimi db)
        {
            colProduktProdhimi trupi = new colProduktProdhimi();
            trupi.mbushProduktSipasIdkoka(IdKokaEkzekutim, db);
            return trupi;
        }

        /// <summary>
        /// mbush koken e ekzekutimit sipas id dokNga
        /// </summary>
        /// <param name="idDokNga">id dokumenti nga</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        public bool mbushKokaEkzekutimSipasIDDokNga(int iddokNga)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            try
            {
                db.beginTransaksion();
                bool mbush = mbushKokaEkzekutim(db.ktheKokaEkzekutimSipasIDDokNga(iddokNga), db);
                if (!mbush)
                {
                    db.rollbackTransaksion();
                }
                else
                {
                    db.commitTransaksion();
                }
                return mbush;
            }
            catch (Exception)
            {
                db.rollbackTransaksion();
                return false;
            }
        }

        /// <summary>
        /// mbush koken e ekzekutimit sipas id se kokes
        /// </summary>
        /// <param name="idKokaEkzekutim">id e kokes se ekzekutimit</param>
        /// <returns>kthen true nese mbushja kryhet me sukses ne te kundert kthen false</returns>        
        public bool mbushKokaEkzekutimSipasID(int idKokaEkzekutim)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            bool mbush = mbushKokaEkzekutimSipasID(idKokaEkzekutim, db);
            db.Dispose();
            return mbush;
        }

        /// <summary>
        /// mbush koken e ekzekutimit sipas id se kokes
        /// </summary>
        /// <param name="idKokaEkzekutim">id e kokes se ekzekutimit</param>
        /// <returns>kthen true nese mbushja kryhet me sukses ne te kundert kthen false</returns>        
        public bool mbushKokaEkzekutimSipasIDPaTrup(int idKokaEkzekutim)
        {
            using (clsDatabazeProdhimi db = new clsDatabazeProdhimi())
            {
                return mbushKokaEkzekutimPaTrup(db.ktheKokaEkzekutimSipasID(idKokaEkzekutim));
            }
        }

        /// <summary>
        /// mbush koken e ekzekutimit sipas id se kokes
        /// </summary>
        /// <param name="idKokaEkzekutim">id e kokes se ekzekutimit</param>
        /// <returns>kthen true nese mbushja kryhet me sukses ne te kundert kthen false</returns>
        /// <param name="db">clsDatabazeProdhimi ne rast transaksioni</param>
        public bool mbushKokaEkzekutimSipasID(int idKokaEkzekutim, clsDatabazeProdhimi db)
        {
            //if (db == null) 
            //    db = new clsDatabazeProdhimi();
            bool mbush = mbushKokaEkzekutim(db.ktheKokaEkzekutimSipasID(idKokaEkzekutim), db);
            return mbush;
        }

        public static bool kaAutorizime(int idkoka, int idperdoruesi)
        {
            clsDatabazeProdhimi dbKokaMagazina = new clsDatabazeProdhimi();
            bool sukses = dbKokaMagazina.kaAutorizimKokaEkzekutim(idkoka, idperdoruesi);
            dbKokaMagazina.Dispose();
            return sukses;
        }

        #endregion

        #region Metoda Internal


        /// <summary>
        /// mbush burimet me te dhenat nga databaza
        /// </summary>
        /// <param name="rreshti">rreshti me te dhena</param>
        /// <param name="db">clsdatabazeprodhimi nqs bej pjese ne nje transaksion</param>
        /// <returns> kthen ne se mbushja u be ne rregull apo jo</returns>
        internal bool mbushKokaEkzekutim(DataRow rreshti, clsDatabazeProdhimi db)
        {
            if (rreshti != null)
            {
                try
                {
                    int.TryParse(rreshti["IDKOKA"].ToString(), out idKokaEkzekutim);
                    int.TryParse(rreshti["IDNIVEL"].ToString(), out idNivel);
                    int.TryParse(rreshti["IDKONFIGAMBJENTE"].ToString(), out idKonfigAmbjente);
                    int.TryParse(rreshti["IDMAGPRODUKTI"].ToString(), out idMagProdukti);
                    int.TryParse(rreshti["IDMAGRECEPTURA"].ToString(), out idMagReceptura);
                    nrDok = rreshti["NRDOK"].ToString();
                    DateTime.TryParse(rreshti["DTDOK"].ToString(), out dtDok);
                    double.TryParse(rreshti["TOTALI"].ToString(), out totali);
                    int.TryParse(rreshti["IDDOKNGA"].ToString(), out idDokNga);
                    int.TryParse(rreshti["IDSTATUSDOK"].ToString(), out idStatusDok);
                    int.TryParse(rreshti["IDNDERMARJE"].ToString(), out idNdermarrje);
                    int.TryParse(rreshti["IDNDERVITI"].ToString(), out idNdermarrjeVit);
                    int.TryParse(rreshti["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    DateTime.TryParse(rreshti["DTREGJ"].ToString(), out dtRegj);
                    shenime = rreshti["SHENIME"].ToString();
                    int.TryParse(rreshti["IDGRUP1"].ToString(), out idGrup1);
                    int.TryParse(rreshti["IDGRUP2"].ToString(), out idGrup2);
                    int.TryParse(rreshti["IDGRUP3"].ToString(), out idGrup3);
                    int.TryParse(rreshti["IDNIVELGJENERUES"].ToString(), out idNivelGjenerues);
                    int.TryParse(rreshti["IDKONFIGGJENERUES"].ToString(), out idKonfigGjenerues);
                    int.TryParse(rreshti["IDGJENERUES"].ToString(), out idGjenerues);
                    int.TryParse(rreshti["IDNJESIPRODHIMI"].ToString(), out idNjesiProdhimi);
                    DateTime.TryParse(rreshti["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(rreshti["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    colProdukte = new colProduktProdhimi();
                    colProdukte = merrProdukteProdhimi(db);
                    oMagazineHyrje = new clsKokaMagazina();
                    oMagazineDalje = new clsKokaMagazina();
                    oFleteKontabel = new clsKokaFleteKontabel();
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

        internal bool mbushKokaEkzekutimPaTrup(DataRow rreshti)
        {
            if (rreshti != null)
            {
                try
                {
                    int.TryParse(rreshti["IDKOKA"].ToString(), out idKokaEkzekutim);
                    int.TryParse(rreshti["IDNIVEL"].ToString(), out idNivel);
                    int.TryParse(rreshti["IDKONFIGAMBJENTE"].ToString(), out idKonfigAmbjente);
                    int.TryParse(rreshti["IDMAGPRODUKTI"].ToString(), out idMagProdukti);
                    int.TryParse(rreshti["IDMAGRECEPTURA"].ToString(), out idMagReceptura);
                    nrDok = rreshti["NRDOK"].ToString();
                    DateTime.TryParse(rreshti["DTDOK"].ToString(), out dtDok);
                    double.TryParse(rreshti["TOTALI"].ToString(), out totali);
                    int.TryParse(rreshti["IDDOKNGA"].ToString(), out idDokNga);
                    int.TryParse(rreshti["IDSTATUSDOK"].ToString(), out idStatusDok);
                    int.TryParse(rreshti["IDNDERMARJE"].ToString(), out idNdermarrje);
                    int.TryParse(rreshti["IDNDERVITI"].ToString(), out idNdermarrjeVit);
                    int.TryParse(rreshti["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    DateTime.TryParse(rreshti["DTREGJ"].ToString(), out dtRegj);
                    shenime = rreshti["SHENIME"].ToString();
                    int.TryParse(rreshti["IDGRUP1"].ToString(), out idGrup1);
                    int.TryParse(rreshti["IDGRUP2"].ToString(), out idGrup2);
                    int.TryParse(rreshti["IDGRUP3"].ToString(), out idGrup3);
                    int.TryParse(rreshti["IDNIVELGJENERUES"].ToString(), out idNivelGjenerues);
                    int.TryParse(rreshti["IDKONFIGGJENERUES"].ToString(), out idKonfigGjenerues);
                    int.TryParse(rreshti["IDGJENERUES"].ToString(), out idGjenerues);
                    int.TryParse(rreshti["IDNJESIPRODHIMI"].ToString(), out idNjesiProdhimi);
                    DateTime.TryParse(rreshti["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(rreshti["DTMODIFIKIMI"].ToString(), out dtModifikimi);                    
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