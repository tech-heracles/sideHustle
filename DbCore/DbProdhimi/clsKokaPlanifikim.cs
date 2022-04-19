 using System;
using System.Collections.Generic;
using System.Linq;
using DbCore.DbAdmin;
using DbCore.DbKontabiliteti;
using System.Data;
using DbCore.DbRegjistrim;
namespace DbCore.DbProdhimi
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne  koken e nje dokumenti te planifikimit
    ///  (Te dhenat  merren nga tabela : T_KOKAPLANIFIKIM)
    /// </summary>
    public class clsKokaPlanifikim
    {
        /// <summary>
        /// konstante per mesazhin e gabimit kur merren te dhenat
        /// </summary>
        private const string STR_ERRORGabimGjateMarrjesSeDokumentitTePlanifikimit = "ERROR: Gabim gjate marrjes se dokumentit te planifikimit nga db-ja";
        /// <summary>
        /// constante qe sasia nuk duhet te jete zero
        /// </summary>
        private const string STR_SasiaNukDuhetTeJeteZero = "Sasia nuk duhet te jete zero";
        /// <summary>
        /// konstante kur planifikimi krijohet me sukses
        /// </summary>
        private const string STR_PlanifikimiUKrijuaMeSukses = "Planifikimi u krijua me sukses!";
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
        private const string STR_MagazinaNukEkziston = "Magazina nuk ekziston!";
        /// <summary>
        /// mesazh gabimi kur magazina nuk eshte aktive
        /// </summary>
        private const string STR_MagazinaNukEshteAktive = "Magazina nuk eshte aktive!";
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
        /// id e kokes se dokumentit te planifikimit
        /// </summary>
        private int idKokaPlanifikim;
        /// <summary>
        /// id e nivelit te regjistrimit
        /// </summary>
        private int idNivel;
        /// <summary>
        /// id e konfigurimit te ambjenteve
        /// </summary>
        private int idKonfigAmbjente;
        /// <summary>
        /// id e klient furnitorit
        /// </summary>
        private int idKlientFurnitor;
        /// <summary>
        /// id e magazines
        /// </summary>
        private int idMagazina;
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
        /// kodi i klientit furnitorit
        /// </summary>
        private string kodKlientFurnitori;
        /// <summary>
        /// kodi i magazines
        /// </summary>
        private string kodMagazina;
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
        /// afati kohor nqs vjen nga shitja merr te shitjes
        /// </summary>
        private DateTime afatiKohor;
        /// <summary>
        /// koleksioni me trupin e planifikimit
        /// </summary>
        private colTrupiPlanifikim colTrupi;


        private int idRaportDesing;
        private int idNjesiProdhimi;
        private string kodNjesiProdhimi;
        private DataRow rreshti;
        private clsDatabazeProdhimi db;
        #endregion

        #region Properties
        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdKokaPlanifikim
        {
            get { return idKokaPlanifikim; }
            set { idKokaPlanifikim = value; }
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
        /// Kthen/Vendos ID-ne e klient furnitorit.
        /// </summary>
        public int IdKlientFurnitor
        {
            get { return idKlientFurnitor; }
            set { idKlientFurnitor = value; }
        }
        /// <summary>
        /// afati kohor nqs vjen nga shitja merr te shitjes
        /// </summary>
        public DateTime AfatiKohor
        {
            get
            {
                return afatiKohor;
            }
            set
            {
                afatiKohor = value;
            }
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
        /// Kthen/Vendos  nje koleksion me trupin e dokumentit 
        /// </summary>
        public colTrupiPlanifikim ColTrupi
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
        public int IdRaportDesing
        {
            get { return idRaportDesing; }
            set { idRaportDesing = value; }
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
        /// <param name="dtDk"> data e dokumentit te planifikimit</param>
        /// <param name="dtRegj"> data e regjistrimit te dokumentit te planifikimit</param>
        /// <param name="idKlFurn"> id e klient furnitorit me te cilin lidhet</param>
        /// <param name="idKonf"> id e konfigurimit te dokumentit</param>
        /// <param name="idLidhes">id e dokumentit nga e cila gjenerohet ne rastet e modifikimit </param>
        /// <param name="idMag">id e magazines</param>
        /// <param name="idkokaplanifikim">id ritese e kokes se planifikimit</param>
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
        /// <param name="idgrup1">id e grupimit te pare</param>
        /// <param name="idgrup2">id e grupimit te dyte</param>
        /// <param name="idgrup3">id e grupimit te trete</param>
        /// <param name="idRaportDesing"></param>
        public clsKokaPlanifikim(int idkokaplanifikim, int idNiv, int idKonf, int idKlFurn, int idMag, DateTime dtDk, string nrDk, int idLidhes, int idSt, int idNder, int idNdVt, int idPer, DateTime dtRe, string shenim, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues, int idgrup1, int idgrup2, int idgrup3, DateTime afatikohor, int idRaportDesing, int idNjesiProdhimi)
        {
            idKokaPlanifikim = idkokaplanifikim;
            idNivel = idNiv;
            idKonfigAmbjente = idKonf;
            idKlientFurnitor = idKlFurn;
            idMagazina = idMag;
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
            this.afatiKohor = afatikohor;
            idGrup1 = idgrup1;
            idGrup2 = idgrup2;
            idGrup3 = idgrup3;
            this.idRaportDesing = idRaportDesing;
            this.idNjesiProdhimi = idNjesiProdhimi;
            colTrupi = new colTrupiPlanifikim();
        }

        /// <summary>
        /// konstruktor me 3 parametra
        /// </summary>
        /// <param name="idNivel">id e nivelit</param>
        /// <param name="nrdok">nr i dokumentit</param>
        /// <param name="dtdok">data e dokumentit</param>
        public clsKokaPlanifikim(int idNivel, string nrdok, DateTime dtdok)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            mbushKokaPlanifikim(db.ktheKokaPlanifikimSipasIdNivelNrDokDtDok(idNivel, nrdok, dtdok), db);
            db.Dispose();
        }

        /// <summary>
        /// konstruktori me nje parameter
        /// </summary>
        /// <param name="idkoka">id koka</param>
        public clsKokaPlanifikim(int idkoka)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            mbushKokaPlanifikim(db.ktheKokaPlanifikimSipasID(idkoka), db);
            db.Dispose();
        } 

        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsKokaPlanifikim()
        {
        }

        public clsKokaPlanifikim(DataRow rreshti, clsDatabazeProdhimi db)
        {
            
            mbushKokaPlanifikim(rreshti, db);
        }
        #endregion

        #region Metoda Publike
        /// <summary>
        /// krijon objektin e dokumentit te planifikimit kur krijohet nga ambjenti i planifikimit
        /// </summary>
        /// <param name="idNiv">id e nivelit</param>
        /// <param name="idKonf">id e konfigurimit te dokumentit</param>
        /// <param name="idKlFurn">id e klient furnitorit</param>
        /// <param name="kodklientfurnitor"> kodi i klient furnitorit</param>
        /// <param name="idMag">id e magazines</param>
        /// <param name="kodmag">kodmagazina</param>
        /// <param name="dtDk">data e dokumentit</param>
        /// <param name="nrDk">nr i dokumentit</param>
        /// <param name="idSt">id e statusit te dokumentit</param>
        /// <param name="idNder">id e ndermarjes</param>
        /// <param name="idNdVt">id e ndermarje vitit</param>
        /// <param name="idPer">id e perdoruesit</param>
        /// <param name="dtRegj">dt e regjistrimit</param>
        /// <param name="shenim">shenime</param>
        /// <param name="idgrup1">id e grupimit te pare </param>
        /// <param name="idgrup2">id e grupimit te dyte</param>
        /// <param name="idgrup3">id e grupimit te trete</param>
        /// <param name="coltrupi">trupi i dokumentit</param>
        /// <returns> kthen clsMesazh </returns>
        public clsMesazh krijoPlanifikim(int idNiv, int idKonf, int idKlFurn, string kodklientfurnitor, int idMag, string kodmag, DateTime dtDk, string nrDk, int idSt, int idNder, int idNdVt, int idPer, DateTime dtRegj, string shenim, int idgrup1, int idgrup2, int idgrup3, DateTime afatikohor, colTrupiPlanifikim coltrupi, int idRaportDesign, int idNjesiProdhimi, string kodNjProdhimi)
        {
            return krijoPlanifikim(idNiv, idKonf, idKlFurn, kodklientfurnitor, idMag, kodmag, dtDk, nrDk, 0, idSt, idNder, idNdVt, idPer, dtRegj, shenim, 0, 0, 0, idgrup1, idgrup2, idgrup3, afatikohor, coltrupi, idRaportDesign, idNjesiProdhimi, kodNjProdhimi);
        }

        /// <summary>
        /// mbush koken e planifikimit sipas id pa trupin
        /// </summary>
        /// <param name="idShitjeKoka">id e kokes se planifikimit</param>
        /// <returns>mbush true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushKokaPlanifikimSipasIDPaTrup(int idkoka)
        {
            using (clsDatabazeProdhimi db = new clsDatabazeProdhimi())
            {
                return mbushKokaPlanifikimPaTrup(db.ktheKokaPlanifikimSipasID(idkoka), db);
            }
        }

        /// <summary>
        /// krijon objektin e dokumentit te planifikimit kur krijohet nga dokumentat gjenerues
        /// </summary>
        /// <param name="idNiv">id e nivelit</param>
        /// <param name="idKonf">id e konfigurimit te dokumentit</param>
        /// <param name="idKlFurn">id e klient furnitorit</param>
        /// <param name="kodklientfurnitor"> kodi i klient furnitorit</param>
        /// <param name="idMag">id e magazines</param>
        /// <param name="kodmag">kodmagazina</param>
        /// <param name="dtDk">data e dokumentit</param>
        /// <param name="nrDk">nr i dokumentit</param>
        /// <param name="idSt">id e statusit te dokumentit</param>
        /// <param name="idNder">id e ndermarjes</param>
        /// <param name="idNdVt">id e ndermarje vitit</param>
        /// <param name="idPer">id e perdoruesit</param>
        /// <param name="dtRe">dt e regjistrimit</param>
        /// <param name="shenim">shenime</param>
        /// <param name="coltrup">trupi i dokumentit</param>
        /// <param name="iddoknga">id e dokumentit nga eshte krijuar ne modifikim</param>
        /// <param name="idGjenerues">id e dokumentit qe e ka gjeneruar</param>
        /// <param name="idKonfigGjenerues"> id e konfigurimit te dokumentit qe e ka gjeneruar</param>
        /// <param name="idNivelGjenerues">id e nivelit te dokumentit qe e ka gjeneruar</param>
            /// <param name="idgrup1">id e grupimit te pare </param>
        /// <param name="idgrup2">id e grupimit te dyte</param>
        /// <param name="idgrup3">id e grupimit te trete</param>
        /// <returns> kthen clsMesazh </returns>
        public clsMesazh krijoPlanifikim(int idNiv, int idKonf, int idKlFurn, string kodklientfurnitor, int idMag, string kodmag, DateTime dtDk, string nrDk, int iddoknga, int idSt, int idNder, int idNdVt, int idPer, DateTime dtRe, string shenim, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues, int idgrup1, int idgrup2, int idgrup3, DateTime afatikohor, colTrupiPlanifikim coltrup, int idRaportDesign, int idNjesiProdhimi, string kodNjProdhimi)
        {
            idNivel = idNiv;
            idKonfigAmbjente = idKonf;
            idKlientFurnitor = idKlFurn;
            kodKlientFurnitori = kodklientfurnitor;
            idMagazina = idMag;
            kodMagazina = kodmag;
            nrDok = nrDk;
            dtDok = dtDk;
            idDokNga = iddoknga;
            idStatusDok = idSt;
            idNdermarrje = idNder;
            idNdermarrjeVit = idNdVt;
            idPerdoruesi = idPer;
            dtRegj = dtRe;
            shenime = shenim;
            this.afatiKohor = afatikohor;
            this.idNivelGjenerues = idNivelGjenerues;
            this.idKonfigGjenerues = idKonfigGjenerues;
            idGrup3 = idgrup3;
            idGrup2 = idgrup2;
            idGrup1 = idgrup1;
            this.idGjenerues = idGjenerues;
            this.idNjesiProdhimi = idNjesiProdhimi;
            colTrupi = coltrup;
            this.idRaportDesing = idRaportDesign;
            this.kodNjesiProdhimi = kodNjProdhimi;
            clsMesazh mesazh = kontrollo(coltrup);
            if (!mesazh.Status)
                return mesazh;
            return new clsMesazh(true, STR_PlanifikimiUKrijuaMeSukses);
        }

        /// <summary>
        /// kontrollon te dhenat e kokes ne jane te sakta
        /// </summary>
        /// <returns>clsMesazh </returns>
        /// <param name="colTrupi"></param>
        private clsMesazh kontrollo(colTrupiPlanifikim colTrupi)
        {
            if (nrDok == "")
                return new clsMesazh(false, STR_NumriIDokumentitNukMundTeJeteBosh);
            if (dtDok == null || dtDok.ToShortDateString() == databosh)
                return new clsMesazh(false, STR_ZgjidhniDatenEDokumentit);
            if (dtRegj == null || dtRegj.ToShortDateString() == databosh)
                return new clsMesazh(false, STR_ZgjidhniDatenERegjistrimit);
            if (afatiKohor == null || afatiKohor.ToShortDateString() == databosh)
                return new clsMesazh(false, "Ju lutem zgjidhni afatin kohor");
            if (kodKlientFurnitori != "")
            {
                if (kodKlientFurnitori != "" && !DbKontabiliteti.clsKlientFurnitor.EkzistonKlientFurnitor(kodKlientFurnitori, idNdermarrje))
                    return new clsMesazh(false, STR_KlientFurnitoriNukEkziston);

                DbKontabiliteti.clsKlientFurnitor kf = new clsKlientFurnitor();
                kf.mbushKlientFurnitorSipasKodit(kodKlientFurnitori, idNdermarrje, idPerdoruesi);
                if (kf.IdKlientFurnitor < 1)
                    return new clsMesazh(false, "Ju nuk keni autorizime per kete klient/furnitor!");
                if (kodKlientFurnitori != "" && !kf.AktivKF)
                    return new clsMesazh(false, STR_KlientFurnitoriNukEshteAktive);
            }
            if (kodMagazina != "")
            {
                if (!clsNjesiAdministrative.ekziston(kodMagazina, idNdermarrje))
                    return new clsMesazh(false, STR_MagazinaNukEkziston);
                clsNjesiAdministrative mag = new clsNjesiAdministrative(kodMagazina, idNdermarrje, idPerdoruesi);
                if (mag.IdNjesiAdministrative < 1)
                    return new clsMesazh(false, "Ju nuk keni autorizime per kete magazine!");
                if (kodMagazina != "" && !mag.Aktiv)
                    return new clsMesazh(false, STR_MagazinaNukEshteAktive);
            }
            if (!String.IsNullOrEmpty(kodNjesiProdhimi))
            {
                if (idNjesiProdhimi <= 0 || !clsNjesiProdhimi.ekzistonSipasKodit(kodNjesiProdhimi, idNdermarrje))
                    return new clsMesazh(false, "Njesia e prodhimit nuk ekziston!");
                if (!clsNjesiProdhimi.eshteAktiveNjesiProdhimiSipasKodit(kodNjesiProdhimi, idNdermarrje))
                    return new clsMesazh(false, "Njesia e prodhimit nuk eshte aktive!");
            }
            foreach (clsTrupiPlanifikim trupi in colTrupi)
            {
                if (trupi.Sasia == 0)
                    return new clsMesazh(false, STR_SasiaNukDuhetTeJeteZero);
                DbInventari.clsArtikulli art = new DbInventari.clsArtikulli(trupi.IdArtikulli);
                if (art.Klasa != 5 && art.Klasa != 6)
                {
                    return new clsMesazh(false, "Ka artikuj te cilet nuk i perkasin klases prodhim ose prodhim ne proces!");
                }
            }
            return new clsMesazh(true, STR_KontrolletUKaluanMeSukses);
        }

        /// <summary>
        /// krijon objektin e dokumentit te planifikimit kur kemi import
        /// </summary>
        /// <param name="idNiv">id e nivelit</param>
        /// <param name="idKonf">id e konfigurimit</param>
        /// <param name="kodKlFurn">kodi i klient furnitorit</param>
        /// <param name="kodMag">kodi i magazines</param>
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
        /// <param name="coltrupi">trupi i dokumentit</param>
        /// <param name="idgrup1">id e grupimit te pare </param>
        /// <param name="idgrup2">id e grupimit te dyte</param>
        /// <param name="idgrup3">id e grupimit te trete</param>
        /// <returns> clsMesazh</returns>
        public clsMesazh krijoPlanifikimPerImport(int idNiv, int idKonf, string kodKlFurn, string kodMag, DateTime dtDk, string nrDk, int idLidhes, int idSt, int idNder, int idNdVt, int idPer, DateTime dtRegj, string shenim, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues, int idgrup1, int idgrup2, int idgrup3, DateTime afatikohor, colTrupiPlanifikim coltrupi, int idRaportDesign)
        {

            DbKontabiliteti.clsKlientFurnitor kf = new clsKlientFurnitor();
            kf.mbushKlientFurnitorSipasKodit(kodKlFurn, idNder);
            idKlientFurnitor = kf.IdKlientFurnitor;
            clsNjesiAdministrative mag = new clsNjesiAdministrative(kodMag, idNder, idPer);
            idMagazina = mag.IdNjesiAdministrative;
            return krijoPlanifikim(idNiv, idKonf, idKlientFurnitor, kodKlFurn, idMagazina, kodMag, dtDk, nrDk, idLidhes, idSt, idNder, idNdVt, idPer, dtRegj, shenim, idNivelGjenerues, idKonfigGjenerues, idGjenerues, idgrup1, idgrup2, idgrup3, afatikohor, coltrupi, idRaportDesign, 0, "");
        }

        /// <summary>
        /// kontrollon nese dokumenti eshte i lidhur
        /// </summary>
        /// <returns> true ose false</returns>
        public bool eshteILidhur()
        {            
            DbAdmin.clsDatabaseAdmin dbAdmin = new DbAdmin.clsDatabaseAdmin();
            bool lidhur = dbAdmin.eshteDokumentiILidhur(idKokaPlanifikim, idNivel, "T_KOKAPLANIFIKIM", "IDKOKAPLANIFIKIM");
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
            DataTable dt = dbAdmin.MerrDokLidhur(idKokaPlanifikim, idNivel, "T_KOKAPLANIFIKIM", "IDKOKAPLANIFIKIM");
            dbAdmin.Dispose();
            return dt;
        }

        /// <summary>
        /// Ruan nje objekt dokumenti planifikimit sebashku me trupin  
        /// </summary>
        /// <param name="dtDk"> data e dokumentit te planifikimit</param>
        /// <param name="dtRegj"> data e regjistrimit te dokumentit te planifikimit</param>
        /// <param name="idKlFurn"> id e klient furnitorit me te cilin lidhet</param>
        /// <param name="idKonf"> id e konfigurimit te dokumentit</param>
        /// <param name="idLidhes">id e dokumentit nga e cila gjenerohet ne rastet e modifikimit </param>
        /// <param name="idMag">id e magazines</param>
        /// <param name="idkokaplanifikim">id ritese e kokes se planifikimit</param>
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
        /// <param name="idgrup1">id e grupimit te pare </param>
        /// <param name="idgrup2">id e grupimit te dyte</param>
        /// <param name="idgrup3">id e grupimit te trete</param>
        /// <returns> kthen nje obj clsMesazh per te identifikuar statusin e ruajtjes se te dhenave ne DB</returns>
        public static clsMesazh ruajPlanifikim(int idkokaplanifikim, int idNiv, int idKonf, int idKlFurn, int idMag, DateTime dtDk, string nrDk, int idLidhes, int idSt, int idNder, int idNdVt, int idPer, DateTime dtRegj, string shenim, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues, int idgrup1, int idgrup2, int idgrup3, DateTime afatikohor, colTrupiPlanifikim ocolTrupiPlanifikim, colUrdherPorosiPlanifikim colUrdherPlanifikim, clsDatabazeProdhimi dbRegj, bool modifikim, int idRaportDesign, int idNjesiProdhimi)
        {
            clsMesazh mesazh;

            try
            {
                mesazh = dbRegj.ruajKokaPlanifikim(out idkokaplanifikim, idNiv, idKonf, idKlFurn, idMag, dtDk, nrDk, idLidhes, idSt, idNder, idNdVt, idPer,
                       dtRegj, shenim, idNivelGjenerues, idKonfigGjenerues, idGjenerues, idgrup1, idgrup2, idgrup3, afatikohor, idRaportDesign, idNjesiProdhimi);


                if (!mesazh.Status)
                    return new clsMesazh(false, STR_NdodhiNjeGabimGjateRuajtjesSeKokesSePlanifikimit);
                foreach (clsUrdherPorosiPlanifikim konv in colUrdherPlanifikim)
                {
                    konv.IdPlanifikimi = idkokaplanifikim;
                    konv.IdKonfigPlanifikimi = idKonf;
                    mesazh = dbRegj.ruajUrdherPorosiPlanifikim(0, konv.IdUrdherPorosia, konv.IdPlanifikimi, konv.IdKonfigUrdher, konv.IdKonfigPlanifikimi);
                    if (!mesazh.Status)
                        return mesazh;
                }
                foreach (clsTrupiPlanifikim o in ocolTrupiPlanifikim)
                {
                    o.IdKokaPlanifikim = idkokaplanifikim;
                    int idM;
                    mesazh = dbRegj.ruajTrupiPlanifikim(out idM, o.IdKokaPlanifikim, o.IdArtikulli, o.IdNjesia, o.Sasia, o.IdMag, o.Gjeresi,o.Gjatesi, o.SasiPermase, o.IdUrdherPorosi, o.Shenime, o.Detajim1, o.Detajim2);
                    if (!mesazh.Status)
                    {
                        return mesazh;
                    }

                    if (modifikim)
                    {

                        mesazh = dbRegj.modifikoProduktProdhimiIdplanifikim(o.IdTrupiPlanifikim, idM);
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
        /// Ruan objektin e  kokes se dokumentit te planifikimit ne tabelen perkatese ne databaze
        /// </summary>
        /// <param name="hfNrAutoregjistrime">hiddenField i Devit qe mban numrat automatike,null nese nuk perodren</param>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
        public clsMesazh ruaj(IDictionary<string, object> hfNrAutoregjistrime,colUrdherPorosiPlanifikim colUrdherPlan)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();  
            db.beginTransaksion();
            clsMesazh u_ruajt = ruaj(hfNrAutoregjistrime, db,colUrdherPlan); //perdor ruajtjen me transaksion

            if (!u_ruajt.Status)
            {
                db.rollbackTransaksion();
                return u_ruajt;
            }
            db.commitTransaksion();
            return u_ruajt;
        }
        public static clsMesazh kaloNeHistorikKokaPlanifikim(int idkoka, clsDatabazeProdhimi db)
        {
            clsMesazh mesazh = db.kaloNeHistorikKokaPlanifikim(idkoka);
            return mesazh;
        }
        /// <summary>
        /// Ruan objektin e  kokes se dokumentit te planifikimit ne tabelen perkatese ne databaze
        /// </summary>
        /// <param name="hfNrAutoregjistrime">hiddenField i Devit qe mban numrat automatike,null nese nuk perodren</param>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
        public clsMesazh ruaj(IDictionary<string, object> hfNrAutoregjistrime, clsDatabazeProdhimi db,colUrdherPorosiPlanifikim colUrdherPlan)
        {
            bool kaNdryshimNumri;
            clsMesazh mesazhKontrolli = kontrolloPlanifikim(out kaNdryshimNumri, db, hfNrAutoregjistrime);
            if (!mesazhKontrolli.Status)
            {
                return mesazhKontrolli;
            }

            clsMesazh u_ruajt = clsKokaPlanifikim.ruajPlanifikim(IdKokaPlanifikim, IdNivel, IdKonfigAmbjente, IdKlientFurnitor, IdMagazina, DtDok, NrDok, IdDokNga, IdStatusDok, IdNdermarrje, IdNdermarrjeVit, IdPerdoruesi, DtRegj, Shenime, IdNivelGjenerues, IdKonfigGjenerues, IdGjenerues, IdGrup1, IdGrup2, IdGrup3, AfatiKohor, colTrupi, colUrdherPlan, db, false, IdRaportDesing, IdNjesiProdhimi);
            if (!u_ruajt.Status)
            {
                return u_ruajt;
            }
            if (kaNdryshimNumri)
                return mesazhKontrolli;
            return u_ruajt;
        }

        /// <summary>
        /// kontrollon dokumentin e planifikimit gjate ruajtjes dhe ben ndryshimet per nr automatik
        /// </summary>
        /// <param name="kaNdryshimNumri">tregon nese ka ndryshuar numri</param>
        /// <param name="dbRegj">clsdatabazeprodhimi per transaksion</param>
        /// <param name="hfNrAutoregjistrime">hiddenfield me fushat e nr automatik</param>
        /// <returns> clsMesazh </returns>
        private clsMesazh kontrolloPlanifikim(out bool kaNdryshimNumri, clsDatabazeProdhimi dbRegj, IDictionary<string, object> hfNrAutoregjistrime)
        {
            kaNdryshimNumri = false;
            clsMesazh mes = new clsMesazh();

            if (hfNrAutoregjistrime != null)
            {
                mes = kontrolloNrAutoPlanifikim(out kaNdryshimNumri, dbRegj, hfNrAutoregjistrime);
                if (!mes.Status)
                    return mes;
            }
            if (dbRegj.ekzistonRegjistrimPlanifikimi(idKonfigAmbjente, nrDok, dtDok, idNdermarrje))
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
        private clsMesazh kontrolloNrAutoPlanifikim(out bool kaNdryshimNumri, clsDatabazeProdhimi dbRegj, IDictionary<string, object> hfregjistrime)
        {
             clsDatabaseAdmin db = new clsDatabaseAdmin(dbRegj );
            List<NrAuto> list = DbAdmin.clsNrAutom.kontrollogjithenumrat(db, hfregjistrime, dtDok);
            if (NrAuto.ktheVlerenEre(list, "NrDok") != "")
                nrDok = NrAuto.ktheVlerenEre(list, "NrDok");
            DbCore.clsMesazh mes = NrAuto.ruajvlera(out kaNdryshimNumri, list, dtDok, idPerdoruesi, idNdermarrje, db);
            return new clsMesazh(mes.Status, mes.PershkrimMesazhi);
        }

        /// <summary>
        /// Modifikon nje objekt dokument planifikimit sebashku me te trupin 
        /// </summary>
        /// <param name="dtDk"> data e dokumentit te planifikimit</param>
        /// <param name="dtRegj"> data e regjistrimit te dokumentit te planifikimit</param>
        /// <param name="idKlFurn"> id e klient furnitorit me te cilin lidhet</param>
        /// <param name="idKonf"> id e konfigurimit te dokumentit</param>
        /// <param name="idLidhes">id e dokumentit nga e cila gjenerohet ne rastet e modifikimit </param>
        /// <param name="idMag">id e magazines</param>
        /// <param name="idkokaplanifikim">id ritese e kokes se planifikimit</param>
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
        /// <param name="idgrup1">id e grupimit te pare </param>
        /// <param name="idgrup2">id e grupimit te dyte</param>
        /// <param name="idgrup3">id e grupimit te trete</param>
        /// <returns> kthen nje obj clsMesazh per te identifikuar statusin e modifikimit se te dhenave ne DB</returns>
        public static clsMesazh modifikoPlanifikim(int idkokaplanifikim, int idNiv, int idKonf, int idKlFurn, int idMag, DateTime dtDk, string nrDk, int idLidhes, int idSt, int idNder, int idNdVt, int idPer, DateTime dtRegj, string shenim, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues, int idgrup1, int idgrup2, int idgrup3, DateTime afatikohor, colTrupiPlanifikim ocolTrupiPlanifikim, clsDatabazeProdhimi dbProdh, colUrdherPorosiPlanifikim colUrdherPlan, int idRaportDesign, int idNjesiProdhimi)
        {
            colTrupiPlanifikim trupat = new colTrupiPlanifikim();
            trupat.mbushTrupiPlanifikimi(idkokaplanifikim,idNder, dbProdh);
            clsMesazh mesazh = new clsMesazh();
            try
            {   //obj koka qe i kalohet si parameter eshte si duhet te modifikohet
                clsKokaPlanifikim kokaEkzistuese = new clsKokaPlanifikim();
                kokaEkzistuese.mbushKokaPlanifikimSipasID(idkokaplanifikim, dbProdh);
                if (string.IsNullOrEmpty(kokaEkzistuese.NrDok) || kokaEkzistuese.IdStatusDok == 2)
                {
                    return new clsMesazh(false, STR_DokumentiKaNdryshuarJuLutemRihapeniPerseri);
                }
                kokaEkzistuese.IdStatusDok = 2;  //duhet vendosur nje status i pershtatshem per kete modifikim
                idLidhes = kokaEkzistuese.IdKokaPlanifikim;
                mesazh = dbProdh.modifikoKokaPlanifikim(kokaEkzistuese.IdKokaPlanifikim, kokaEkzistuese.IdNivel, kokaEkzistuese.IdKonfigAmbjente, kokaEkzistuese.IdKlientFurnitor, kokaEkzistuese.IdMagazina, kokaEkzistuese.DtDok, kokaEkzistuese.NrDok, kokaEkzistuese.idDokNga, kokaEkzistuese.IdStatusDok, kokaEkzistuese.idNdermarrje, kokaEkzistuese.idNdermarrjeVit, idPer, kokaEkzistuese.DtRegj, kokaEkzistuese.Shenime, kokaEkzistuese.idNivelGjenerues, kokaEkzistuese.idKonfigGjenerues, kokaEkzistuese.idGjenerues, kokaEkzistuese.IdGrup1,kokaEkzistuese.IdGrup2,kokaEkzistuese.IdGrup3,kokaEkzistuese.AfatiKohor, kokaEkzistuese.IdRaportDesing, kokaEkzistuese.IdNjesiProdhimi);

                if (!mesazh.Status)
                {
                    return mesazh;
                }
                mesazh = clsKokaPlanifikim.kaloNeHistorikKokaPlanifikim(kokaEkzistuese.idKokaPlanifikim, dbProdh);
                if (!mesazh.Status)
                {
                    return mesazh;
                }
                mesazh = dbProdh.fshiUrdherPorosiPlanifikimSipasIdPlanfikimi(kokaEkzistuese.idKokaPlanifikim);
                if (!mesazh.Status)
                {
                    return mesazh;
                }
                mesazh = ruajPlanifikim(idkokaplanifikim, idNiv, idKonf, idKlFurn, idMag, dtDk, nrDk, idLidhes, idSt, idNder, idNdVt, idPer, dtRegj, shenim, idNivelGjenerues, idKonfigGjenerues, idGjenerues, idgrup1, idgrup2, idgrup3, afatikohor, ocolTrupiPlanifikim, colUrdherPlan, dbProdh, true, idRaportDesign, idNjesiProdhimi);

                return mesazh;
            }
            catch (Exception ce)
            {

                return new clsMesazh(false, ce.Message);
            }
        }

        /// <summary>
        /// Modifikon objektin e  kokes se dokumentit te planifikimit ne tabelen perkatese ne databaze
        /// </summary>
        /// <param name="lidhur">tregon nese eshte i lidhur ose jo</param>
        /// <returns > nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo</returns>
        public clsMesazh modifiko(bool lidhur, colUrdherPorosiPlanifikim colUrdherPlan)
        {
            clsMesazh u_modifikua;
            clsDatabazeProdhimi data = new clsDatabazeProdhimi();
            if (lidhur == false)
            {
                //data.krijoManager();
                data.beginTransaksion();
                u_modifikua = modifikoPlanifikim(IdKokaPlanifikim, IdNivel, IdKonfigAmbjente, IdKlientFurnitor, IdMagazina, DtDok, NrDok, IdDokNga, IdStatusDok, IdNdermarrje, IdNdermarrjeVit, IdPerdoruesi, DtRegj, Shenime, IdNivelGjenerues, IdKonfigGjenerues, IdGjenerues,IdGrup1,IdGrup2,IdGrup3, afatiKohor, colTrupi, data, colUrdherPlan, IdRaportDesing, IdNjesiProdhimi);
                if (u_modifikua.Status)
                    data.commitTransaksion();
                else data.rollbackTransaksion();
            }
            else
            {
                u_modifikua = data.modifikoKokaPlanifikim(IdKokaPlanifikim, IdNivel, IdKonfigAmbjente, IdKlientFurnitor, IdMagazina, DtDok, NrDok, idDokNga, IdStatusDok, idNdermarrje, idNdermarrjeVit, idPerdoruesi, DtRegj, Shenime, idNivelGjenerues, idKonfigGjenerues, idGjenerues ,IdGrup1,IdGrup2,IdGrup3, afatiKohor, IdRaportDesing, IdNjesiProdhimi);
                data.Dispose();
            }
            return u_modifikua;
        }
       

        /// <summary>
        /// fshin nje objekt dokument planifikimit  duke i ndryshuar statusin
        /// </summary>
        ///<param name="idkokaplanifikim"> koka e dokumentit te planifikimit i cili do te fshihet</param>
        ///<param name="idperdoruesi">id e perdoruesit qe ka bere veprimin</param>
        ///<param name="dbProdh"> clsdatabaseProdhimi si pjese e transaksionit</param>
        /// <returns> kthen nje obj clsMesazh per te identifikuar statusin e fshirjes se te dhenave ne DB</returns>
        public static  clsMesazh fshiPlanifikim(int idkokaplanifikim, int idperdoruesi, clsDatabazeProdhimi dbProdh)
        {

            clsMesazh mesazh = new clsMesazh(true);

            try
            {
                clsKokaPlanifikim kokaEkzistuese = new clsKokaPlanifikim();
                kokaEkzistuese.mbushKokaPlanifikimSipasID(idkokaplanifikim, dbProdh);
                kokaEkzistuese.IdStatusDok = 2;  //duhet vendosur nje status i pershtatshem per kete modifikim
              //  mesazh = dbProdh.modifikoKokaPlanifikim(kokaEkzistuese.IdKokaPlanifikim, kokaEkzistuese.IdNivel, kokaEkzistuese.IdKonfigAmbjente, kokaEkzistuese.IdKlientFurnitor, kokaEkzistuese.IdMagazina, kokaEkzistuese.DtDok, kokaEkzistuese.NrDok, kokaEkzistuese.idDokNga, kokaEkzistuese.IdStatusDok, kokaEkzistuese.idNdermarrje, kokaEkzistuese.idNdermarrjeVit, idperdoruesi, kokaEkzistuese.DtRegj, kokaEkzistuese.Shenime, kokaEkzistuese.idNivelGjenerues, kokaEkzistuese.idKonfigGjenerues, kokaEkzistuese.idGjenerues, kokaEkzistuese.IdGrup1,kokaEkzistuese.IdGrup2,kokaEkzistuese.idGrup3);
                mesazh = dbProdh.fshiKokaPlanifikim(kokaEkzistuese.IdKokaPlanifikim, idperdoruesi);
                if (!mesazh.Status)
                    return mesazh;
                mesazh = clsKokaPlanifikim.kaloNeHistorikKokaPlanifikim(kokaEkzistuese.idKokaPlanifikim, dbProdh);
                if (!mesazh.Status)
                {
                    return mesazh;
                }
                mesazh = dbProdh.fshiUrdherPorosiPlanifikimSipasIdPlanfikimi(kokaEkzistuese.idKokaPlanifikim);
                
                return mesazh;

            }
            catch (Exception ce)
            {

                return new clsMesazh(false, ce.Message);
            }
        }

        /// <summary>
        /// Fshin objektin e  kokes se dokumentit te planifikimit ne tabelen perkatese ne databaze
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh fshi()
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            //db.krijoManager();
            db.beginTransaksion();
            clsMesazh u_fshi = fshiPlanifikim(IdKokaPlanifikim, idPerdoruesi, db);
            if (u_fshi.Status)
                db.commitTransaksion();
            else db.rollbackTransaksion();
            return u_fshi;
        }

        /// <summary>
        /// Merr objektin e  kokes se dokumentit te planifikimi sipas id nga tabela perkatese ne databaze
        /// </summary>
        /// <param name="id"> id e kokes se planifikimit</param>
        /// <returns > nje objekt clsKokaPlanifikim qe permban objektin e kerkuar</returns>
        public static clsKokaPlanifikim merrSipasId(int id)
        {
            clsKokaPlanifikim data = new clsKokaPlanifikim();
            //data.mbushKokaPlanifikimSipasID(id, null);
            data.mbushKokaPlanifikimSipasID(id);
            return data;
        }

        /// <summary>
        /// Merr objektin e  kokes se dokumentit te planifikimit sipas nr dhe dt dokumenti nga tabela perkatese ne databaze.
        /// </summary>
        /// <returns > nje objekt clsKokaPlanifikim qe permban objektin e kerkuar</returns>
        public clsKokaPlanifikim merrSipasIdNivelNrDokDtDok()
        {
            clsKokaPlanifikim data = new clsKokaPlanifikim(IdNivel, NrDok, DtDok);
            return data;
        }

        /// <summary>
        /// Merr gjithe e  kokat e dokumentave te planifikimit sipas ndermarjevitit nga tabela perkatese ne databaze
        /// </summary>
        /// <param name="idNdermVit">id e ndermarje vitit</param>
        /// <returns > nje object colKokaPlanifikim qe permban nje koleksion me gjithe kokat e dokumentave te planifikimit te nje ndermarje ne nje vit te caktuar</returns>
        public static colKokaPlanifikim merriTeGjithe(int idNdermVit)
        {
            colKokaPlanifikim data = new colKokaPlanifikim(idNdermVit);
            return data;
        }

        /// <summary>
        /// Merr trupin  e  nje dokumenti te planifikimit nga tabela perkatese ne databaze
        /// </summary>
        /// <returns > nje object colTrupiPlanifikim qe permban nje koleksion me trupin e dokumentit te planifikimit</returns>
        public colTrupiPlanifikim merrTrupiPlanifikim(clsDatabazeProdhimi db)
        {
            colTrupiPlanifikim trupi = new colTrupiPlanifikim();
            trupi.mbushTrupiPlanifikimi(IdKokaPlanifikim,IdNdermarrje, db);
            return trupi;
        }

        /// <summary>
        /// mbush koken e planifikimit sipas id dokNga
        /// </summary>
        /// <param name="idDokNga">id dokumenti nga</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        public bool mbushKokaPlanifikimSipasIDDokNga(int iddokNga)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            bool mbush = mbushKokaPlanifikim(db.ktheKokaPlanifikimitSipasIDDokNga(iddokNga), db);
            db.Dispose();
            return mbush;
        }

        /// <summary>
        /// mbush koken e planifikimit sipas id se kokes
        /// </summary>
        /// <param name="idKokaPlanifikim">id e kokes se planifikimit</param>
        /// <returns>kthen true nese mbushja kryhet me sukses ne te kundert kthen false</returns>        
        public bool mbushKokaPlanifikimSipasID(int idKokaPlanifikim)
        {            
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            bool mbush = mbushKokaPlanifikimSipasID(idKokaPlanifikim, db);
            db.Dispose();
            return mbush;
        }

        /// <summary>
        /// mbush koken e planifikimit sipas id se kokes
        /// </summary>
        /// <param name="idKokaPlanifikim">id e kokes se planifikimit</param>
        /// <returns>kthen true nese mbushja kryhet me sukses ne te kundert kthen false</returns>
        /// <param name="db"></param>
        public bool mbushKokaPlanifikimSipasID(int idKokaPlanifikim, clsDatabazeProdhimi db)
        {
            //if (db == null) 
            //    db = new clsDatabazeProdhimi();
            bool mbush = mbushKokaPlanifikim(db.ktheKokaPlanifikimSipasID(idKokaPlanifikim), db);
            return mbush;
        }
        public static bool kaAutorizime(int idkoka, int idperdoruesi)
        {
            clsDatabazeProdhimi dbKokaMagazina = new  clsDatabazeProdhimi ();
            bool sukses = dbKokaMagazina.kaAutorizimKokaPlanifikim(idkoka, idperdoruesi);
            dbKokaMagazina.Dispose();
            return sukses;
        }
        public static string eshteEkzekutuarPlanifikimi(int idkoka, int idndermarje)
        {
            clsDatabazeProdhimi dbProdhimi = new  clsDatabazeProdhimi ();
            string sukses = dbProdhimi.eshteEkzekutuarPlanifikimi(idkoka, idndermarje);
            dbProdhimi.Dispose();
            return sukses;
        }

        public static int ktheNjesiProdhimiSipasIdPlanifikim(int idKoka)
        {
            clsDatabazeProdhimi dbProdhimi = new clsDatabazeProdhimi();
            int sukses = dbProdhimi.ktheIdNjesiProdhimiSipasKokePlanifikim(idKoka);
            dbProdhimi.Dispose();
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
        internal bool mbushKokaPlanifikim(DataRow rreshti, clsDatabazeProdhimi db)
        {
            if (rreshti != null)
            {
                try
                {
                    int.TryParse(rreshti["IDKOKAPLANIFIKIM"].ToString(), out idKokaPlanifikim);
                    int.TryParse(rreshti["IDNIVEL"].ToString(), out idNivel);
                    int.TryParse(rreshti["IDKONFIGAMBJENTE"].ToString(), out idKonfigAmbjente);
                    int.TryParse(rreshti["IDKLIENTI"].ToString(), out idKlientFurnitor);
                    int.TryParse(rreshti["IDMAG"].ToString(), out idMagazina);
                    int.TryParse(rreshti["IDGRUP1"].ToString(), out idGrup1);
                    int.TryParse(rreshti["IDGRUP2"].ToString(), out idGrup2);
                    int.TryParse(rreshti["IDGRUP3"].ToString(), out idGrup3);
                    nrDok = rreshti["NRDOK"].ToString();
                    DateTime.TryParse(rreshti["DTDOK"].ToString(), out dtDok);
                    int.TryParse(rreshti["IDDOKNGA"].ToString(), out idDokNga);
                    int.TryParse(rreshti["IDSTATUSDOK"].ToString(), out idStatusDok);
                    int.TryParse(rreshti["IDNDERMARJE"].ToString(), out idNdermarrje);
                    int.TryParse(rreshti["IDNDERVITI"].ToString(), out idNdermarrjeVit);
                    int.TryParse(rreshti["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    DateTime.TryParse(rreshti["DTREGJ"].ToString(), out dtRegj);
                    shenime = rreshti["SHENIME"].ToString();
                    int.TryParse(rreshti["IDNIVELGJENERUES"].ToString(), out idNivelGjenerues);
                    int.TryParse(rreshti["IDKONFIGGJENERUES"].ToString(), out idKonfigGjenerues);
                    int.TryParse(rreshti["IDGJENERUES"].ToString(), out idGjenerues);
                    DateTime.TryParse(rreshti["AFATIKOHOR"].ToString(), out afatiKohor);
                    DateTime.TryParse(rreshti["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(rreshti["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    int.TryParse(rreshti["IDRAPORTDESING"].ToString(), out idRaportDesing);
                    int.TryParse(rreshti["IDNJESIPRODHIMI"].ToString(), out idNjesiProdhimi);
                    colTrupi = new colTrupiPlanifikim();
                    colTrupi = merrTrupiPlanifikim(db);
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

        /// <summary>
        /// mbush burimet me te dhenat nga databaza pa trupin
        /// </summary>
        /// <param name="rreshti">rreshti me te dhena</param>
        /// <param name="db">clsdatabazeprodhimi nqs bej pjese ne nje transaksion</param>
        /// <returns> kthen ne se mbushja u be ne rregull apo jo</returns>
        internal bool mbushKokaPlanifikimPaTrup(DataRow rreshti, clsDatabazeProdhimi db)
        {
            if (rreshti != null)
            {
                try
                {
                    int.TryParse(rreshti["IDKOKAPLANIFIKIM"].ToString(), out idKokaPlanifikim);
                    int.TryParse(rreshti["IDNIVEL"].ToString(), out idNivel);
                    int.TryParse(rreshti["IDKONFIGAMBJENTE"].ToString(), out idKonfigAmbjente);
                    int.TryParse(rreshti["IDKLIENTI"].ToString(), out idKlientFurnitor);
                    int.TryParse(rreshti["IDMAG"].ToString(), out idMagazina);
                    int.TryParse(rreshti["IDGRUP1"].ToString(), out idGrup1);
                    int.TryParse(rreshti["IDGRUP2"].ToString(), out idGrup2);
                    int.TryParse(rreshti["IDGRUP3"].ToString(), out idGrup3);
                    nrDok = rreshti["NRDOK"].ToString();
                    DateTime.TryParse(rreshti["DTDOK"].ToString(), out dtDok);
                    int.TryParse(rreshti["IDDOKNGA"].ToString(), out idDokNga);
                    int.TryParse(rreshti["IDSTATUSDOK"].ToString(), out idStatusDok);
                    int.TryParse(rreshti["IDNDERMARJE"].ToString(), out idNdermarrje);
                    int.TryParse(rreshti["IDNDERVITI"].ToString(), out idNdermarrjeVit);
                    int.TryParse(rreshti["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    DateTime.TryParse(rreshti["DTREGJ"].ToString(), out dtRegj);
                    shenime = rreshti["SHENIME"].ToString();
                    int.TryParse(rreshti["IDNIVELGJENERUES"].ToString(), out idNivelGjenerues);
                    int.TryParse(rreshti["IDKONFIGGJENERUES"].ToString(), out idKonfigGjenerues);
                    int.TryParse(rreshti["IDGJENERUES"].ToString(), out idGjenerues);
                    DateTime.TryParse(rreshti["AFATIKOHOR"].ToString(), out afatiKohor);
                    DateTime.TryParse(rreshti["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(rreshti["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    int.TryParse(rreshti["IDRAPORTDESING"].ToString(), out idRaportDesing);
                    int.TryParse(rreshti["IDNJESIPRODHIMI"].ToString(), out idNjesiProdhimi);
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


