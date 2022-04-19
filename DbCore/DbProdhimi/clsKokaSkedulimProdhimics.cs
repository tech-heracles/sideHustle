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
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne  koken e nje dokumenti te skedulim prodhimi
    ///  (Te dhenat  merren nga tabela : T_KOKASKEDULIMPRODHIMI)
    /// </summary>
    public class clsKokaSkedulimProdhimi
    {
        /// <summary>
        /// konstante per mesazhin e gabimit kur merren te dhenat
        /// </summary>
        private const string STR_ERRORGabimGjateMarrjesSeDokumentitTeSkedulimProdhimit = "ERROR: Gabim gjate marrjes se dokumentit te skedulim prodhimi nga db-ja";
        /// <summary>
        /// constante qe sasia nuk duhet te jete zero
        /// </summary>
        private const string STR_SasiaNukDuhetTeJeteZero = "Koha nuk duhet te jete zero";
        /// <summary>
        /// konstante kur skedulimi krijohet me sukses
        /// </summary>
        private const string STR_SkedulimiUKrijuaMeSukses = "Skedulimi u krijua me sukses!";
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
        private const string STR_NdodhiNjeGabimGjateRuajtjesSeKokesSeSkedulimit = "Ndodhi nje Gabim gjate ruajtjes se Kokes se skedulimit";
        /// <summary>
        /// konstante per mesazhin e gabimit kur ekziston nje dokument me keto te dhena
        /// </summary>
        private const string STR_EkzistonNjeRegjistrimMeTeNjejtinNumerDokumenti = "Ekziston nje regjistrim me te njejtin numer dokumenti!";
        /// <summary>
        /// mesazh kur kontrollet kalojne me sukses
        /// </summary>
        private const string STR_KontrolliIMagazinesUKryeMeSukses = "Kontrolli i skedulimit u krye me sukses!";
        /// <summary>
        /// konstante per mesazhin e gabimit kur dokumenti ka ndryshuar gjate modifikimit
        /// </summary>
        private const string STR_DokumentiKaNdryshuarJuLutemRihapeniPerseri = "Dokumenti ka ndryshuar! Ju lutem rihapeni perseri!";

        #region Attributet
        /// <summary>
        /// id e kokes se dokumentit te skedulimit
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
        /// id e planifikimit
        /// </summary>
        private int idPlanifikimi;
        /// <summary>
        /// id e burimit
        /// </summary>
        private int idBurimi;
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
        /// kodi i burimit
        /// </summary>
        private string kodBurimi;
        
        /// <summary>
        /// id e krijuesit
        /// </summary>
        private int idKrijuesi;
        
        /// <summary>
        /// koleksioni me trupin e planifikimit
        /// </summary>
        private colTrupiSkedulimProdhimi colTrupi;
        private DataRow rreshti;
        private clsDatabazeProdhimi db;

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
        /// Kthen/Vendos ID-ne e planifikimit
        /// </summary>
        public int IdPlanifikimi
        {
            get { return idPlanifikimi; }
            set { idPlanifikimi = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e burimit
        /// </summary>
        public int IdBurimi
        {
            get { return idBurimi; }
            set { idBurimi = value; }
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
        public colTrupiSkedulimProdhimi ColTrupi
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
        /// Konstruktori i klases me parametra
        /// </summary>
        /// <param name="dtDk"> data e dokumentit </param>
        /// <param name="dtRegj"> data e regjistrimit te dokumentit </param>
        /// <param name="idplanifikim"> id e planifikimit me te cilin lidhet</param>
        /// <param name="idKonf"> id e konfigurimit te dokumentit</param>
        /// <param name="idLidhes">id e dokumentit nga e cila gjenerohet ne rastet e modifikimit </param>
        /// <param name="idburim">id e burimit</param>
        /// <param name="idkoka">id ritese e kokes </param>
        /// <param name="idNder"> id e ndermarjes</param>
        /// <param name="idNdVt"> id e ndermarje vitit</param>
        /// <param name="idNiv"> id e nivelit te regjistrimit</param>
        /// <param name="idPer"> id e perdoruesit se e ben regjistrimin</param>
        /// <param name="idSt"> id e statusit te dokumentit</param>
        /// <param name="nrDk">nr i dokumentit</param>
        /// <param name="shenim"> shenime </param>
        /// <param name="idGjenerues">id e dokumentit nga gjenerohet  nga nje ambjent tjeter</param>
        /// <param name="idKonfigGjenerues">id e konfigurimit nga gjenerohet dokumenti</param>
        /// <param name="idNivelGjenerues">id e nivelit nga gjenerohet dokumenti</param>   
        /// <param name="idkrijuesi">id e grupimit te pare</param>
 
        public clsKokaSkedulimProdhimi(int idkoka, int idNiv, int idKonf, int idplanifikim, int idburim, DateTime dtDk, string nrDk, int idLidhes, int idSt, int idNder, int idNdVt, int idPer, DateTime dtRe, string shenim, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues, int idkrijuesi)
        {
            idKoka = idkoka;
            idNivel = idNiv;
            idKonfigAmbjente = idKonf;
            idPlanifikimi = idplanifikim;
            idBurimi = idburim;
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

            idKrijuesi = idkrijuesi;
         
            colTrupi = new colTrupiSkedulimProdhimi();
        }

        /// <summary>
        /// konstruktor me 3 parametra
        /// </summary>
        /// <param name="idNivel">id e nivelit</param>
        /// <param name="nrdok">nr i dokumentit</param>
        /// <param name="dtdok">data e dokumentit</param>
        public clsKokaSkedulimProdhimi(int idNivel, string nrdok, DateTime dtdok)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            mbushKokaSkedulimProdhimi(db.ktheKokaSkedulimProdhimiSipasIdNivelNrDokDtDok(idNivel, nrdok, dtdok), db);
            db.Dispose();
        }

        /// <summary>
        /// konstruktori me nje parameter
        /// </summary>
        /// <param name="idkoka">id koka</param>
        public clsKokaSkedulimProdhimi(int idkoka)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            mbushKokaSkedulimProdhimi(db.ktheKokaSkedulimProdhimiSipasID(idkoka), db);
            db.Dispose();
        }

        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsKokaSkedulimProdhimi()
        {
        }

        public clsKokaSkedulimProdhimi(DataRow rreshti, clsDatabazeProdhimi db)
        {
            
            mbushKokaSkedulimProdhimi(rreshti, db);
        }
        #endregion

        #region Metoda Publike
        /// <summary>
        /// krijon objektin e dokumentit te skedulimit kur krijohet nga ambjenti i skedulimit
        /// </summary>
        /// <param name="idNiv">id e nivelit</param>
        /// <param name="idKonf">id e konfigurimit te dokumentit</param>
        /// <param name="idPlanifikim">id e planifikimit</param>
        /// <param name="nrdokplan"> nr i dokumentit te planifikimit</param>
        /// <param name="idburimi">id e burimit</param>
        /// <param name="kodburimi">kodi i burimit</param>
        /// <param name="dtDk">data e dokumentit</param>
        /// <param name="nrDk">nr i dokumentit</param>
        /// <param name="idSt">id e statusit te dokumentit</param>
        /// <param name="idNder">id e ndermarjes</param>
        /// <param name="idNdVt">id e ndermarje vitit</param>
        /// <param name="idPer">id e perdoruesit</param>
        /// <param name="dtRegj">dt e regjistrimit</param>
        /// <param name="shenim">shenime</param>
        /// <param name="idkrijuesi">id e krijuesit </param>
    
        /// <param name="coltrupi">trupi i dokumentit</param>
        /// <returns> kthen clsMesazh </returns>
        public clsMesazh krijoSkedulim(int idNiv, int idKonf, int idPlanifikim, string nrdokplan, int idburimi, string kodburimi, DateTime dtDk, string nrDk, int idSt, int idNder, int idNdVt, int idPer, DateTime dtRegj, string shenim, int idkrijuesi, colTrupiSkedulimProdhimi coltrupi)
        {
            return krijoSkedulim(idNiv, idKonf, idPlanifikim, nrdokplan, idburimi, kodburimi, dtDk, nrDk, 0, idSt, idNder, idNdVt, idPer, dtRegj, shenim, 0, 0, 0, idkrijuesi, coltrupi);
        }

        /// <summary>
        /// krijon objektin e dokumentit te skedulimit kur krijohet nga dokumentat gjenerues
        /// </summary>
        /// <param name="idNiv">id e nivelit</param>
        /// <param name="idKonf">id e konfigurimit te dokumentit</param>
        /// <param name="idPlanifikim">id e planifikimit</param>
        /// <param name="nrdokplan"> nr i dok te planifikimit</param>
        /// <param name="idburimi">id e burimit</param>
        /// <param name="kodburimi">kodi i burimit</param>
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
        /// <param name="idkrijuesi">id e grupimit te pare </param>
        /// <param name="idgrup2">id e grupimit te dyte</param>
        /// <param name="idgrup3">id e grupimit te trete</param>
        /// <returns> kthen clsMesazh </returns>
        public clsMesazh krijoSkedulim(int idNiv, int idKonf, int idPlanifikim, string nrdokplan, int idburimi, string kodburimi, DateTime dtDk, string nrDk, int iddoknga, int idSt, int idNder, int idNdVt, int idPer, DateTime dtRe, string shenim, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues, int idkrijuesi,  colTrupiSkedulimProdhimi coltrup)
        {
            idNivel = idNiv;
            idKonfigAmbjente = idKonf;
            idPlanifikimi = idPlanifikim;
            kodBurimi = kodburimi ;
            idBurimi = idburimi;
            nrDok = nrDk;
            dtDok = dtDk;
            idDokNga = iddoknga;
            idStatusDok = idSt;
            idNdermarrje = idNder;
            idNdermarrjeVit = idNdVt;
            idPerdoruesi = idPer;
            dtRegj = dtRe;
            shenime = shenim;
            this.idNivelGjenerues = idNivelGjenerues;
            this.idKonfigGjenerues = idKonfigGjenerues;
            idKrijuesi = idkrijuesi;
            this.idGjenerues = idGjenerues;
            colTrupi = coltrup;
            clsMesazh mesazh = kontrollo();
            if (!mesazh.Status)
                return mesazh;
            return new clsMesazh(true, STR_SkedulimiUKrijuaMeSukses);
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
            if (kodBurimi != "")
            {
                if (kodBurimi != "" && !DbProdhimi.clsBurime.ekzistonBurim(kodBurimi,idNdermarrje))
                    return new clsMesazh(false, "Burimi nuk ekziston");
                clsBurime burim = new clsBurime(kodBurimi, idNdermarrje);
                if (!burim.Aktiv)
                    return new clsMesazh(false, "Ky burim nuk eshte aktiv!");
                DbKontabiliteti.clsKlientFurnitor kf = new clsKlientFurnitor();
               
            }
           
            foreach (clsTrupiSkedulimProdhimi trupi in colTrupi)
            {
                if (trupi.Koha == 0)
                    return new clsMesazh(false, STR_SasiaNukDuhetTeJeteZero);
                DbInventari.clsArtikulli art = new DbInventari.clsArtikulli(trupi.IdProdukti);
                if (art.Klasa != 5 && art.Klasa != 6)
                {
                    return new clsMesazh(false, "Ka artikuj te cilet nuk i perkasin klases prodhim ose prodhim ne proces!");
                }
                colAktiviteteTrupi coltrupiakt = new colAktiviteteTrupi(trupi.IdAktiviteti);
                bool aktivitetiPermbanBurimin=false;
                foreach (clsAktiviteteTrupi akt in coltrupiakt)
                {
                    if (akt.IdBurimi == trupi.IdBurimi)
                    {
                        aktivitetiPermbanBurimin = true;
                        break;
                    }
                }
                if (!aktivitetiPermbanBurimin)
                {
                    return new clsMesazh(false, "Nje nga aktivitetet nuk e permban burimin e zgjedhur!");

                }
                bool artikullPlanifikimi=false;
                colTrupiPlanifikim coltrupiplan = new colTrupiPlanifikim(trupi.IdPlanifikimi,idNdermarrje);
                foreach (clsTrupiPlanifikim plan in coltrupiplan)
                {
                    if (plan.IdArtikulli == trupi.IdProdukti)
                    {
                        artikullPlanifikimi = true;
                        break;
                    }

                }
                if (!artikullPlanifikimi)
                {
                    return new clsMesazh(false, "Artikulli nuk e perket ketij planifikimi!");
                }
            }
            return new clsMesazh(true, STR_KontrolletUKaluanMeSukses);
        }

        /// <summary>
        /// krijon objektin e dokumentit te skedulimit te prodhimit kur kemi import
        /// </summary>
        /// <param name="idNiv">id e nivelit</param>
        /// <param name="idKonf">id e konfigurimit</param>
        /// <param name="idplanifikimi">id e planifikimit</param>
        /// <param name="kodburimi">kodi i burimit</param>
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
        /// <param name="idkrijuesi">id e grupimit te pare </param>
        /// <param name="idgrup2">id e grupimit te dyte</param>
        /// <param name="idgrup3">id e grupimit te trete</param>
        /// <returns> clsMesazh</returns>
        public clsMesazh krijoSkedulimPerImport(int idNiv, int idKonf, int  idplanifikimi, string kodburimi, DateTime dtDk, string nrDk, int idLidhes, int idSt, int idNder, int idNdVt, int idPer, DateTime dtRegj, string shenim, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues, int idkrijuesi,  colTrupiSkedulimProdhimi coltrupi)
        {

          
            clsBurime mag = new  clsBurime (kodburimi, idNder);
            idBurimi = mag.IdBurimi;
            return krijoSkedulim(idNiv, idKonf,idplanifikimi,"", idBurimi, kodburimi, dtDk, nrDk, idLidhes, idSt, idNder, idNdVt, idPer, dtRegj, shenim, idNivelGjenerues, idKonfigGjenerues, idGjenerues, idkrijuesi,  coltrupi);
        }

        /// <summary>
        /// kontrollon nese dokumenti eshte i lidhur
        /// </summary>
        /// <returns> true ose false</returns>
        public bool eshteILidhur()
        {
            DbAdmin.clsDatabaseAdmin dbAdmin = new DbAdmin.clsDatabaseAdmin();
            bool lidhur = dbAdmin.eshteDokumentiILidhur(idKoka, idNivel, "T_KOKASKEDULIMPRODHIMI", "IDKOKA");
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
            DataTable dt = dbAdmin.MerrDokLidhur(idKoka, idNivel, "T_KOKASKEDULIMPRODHIMI", "IDKOKA");
            dbAdmin.Dispose();
            return dt;
        }

        /// <summary>
        /// Ruan nje objekt dokumenti planifikimit sebashku me trupin  
        /// </summary>
        /// <param name="dtDk"> data e dokumentit te planifikimit</param>
        /// <param name="dtRegj"> data e regjistrimit te dokumentit te planifikimit</param>
        /// <param name="idPlanifikim"> id e klient furnitorit me te cilin lidhet</param>
        /// <param name="idKonf"> id e konfigurimit te dokumentit</param>
        /// <param name="idLidhes">id e dokumentit nga e cila gjenerohet ne rastet e modifikimit </param>
        /// <param name="idburimi">id e magazines</param>
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
        /// <param name="ocolTrupiSkedulimProdhimi">kolektion i trupit te planifikimit</param>
        /// <param name="dbRegj"> clsdatabazeprodhimi per transaksionin</param>
        /// <param name="idkrijuesi">id e grupimit te pare </param>
        /// <param name="idgrup2">id e grupimit te dyte</param>
        /// <param name="idgrup3">id e grupimit te trete</param>
        /// <returns> kthen nje obj clsMesazh per te identifikuar statusin e ruajtjes se te dhenave ne DB</returns>
        public static clsMesazh ruajSkedulim(int idkoka, int idNiv, int idKonf, int idPlanifikim, int idburimi, DateTime dtDk, string nrDk, int idLidhes, int idSt, int idNder, int idNdVt, int idPer, DateTime dtRegj, string shenim, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues, int idkrijuesi,  colTrupiSkedulimProdhimi ocolTrupiSkedulimProdhimi, clsDatabazeProdhimi dbRegj, bool modifikim)
        {
            clsMesazh mesazh;

            try
            {
                mesazh = dbRegj.ruajKokaSkedulimProdhimi(out idkoka, idNiv, idKonf, idburimi, idPlanifikim, dtDk, nrDk, idLidhes, idSt, idNder, idNdVt, idPer, dtRegj, shenim, idNivelGjenerues, idKonfigGjenerues, idGjenerues, idkrijuesi);


                if (!mesazh.Status)
                    return new clsMesazh(false, STR_NdodhiNjeGabimGjateRuajtjesSeKokesSeSkedulimit);
               
                foreach (clsTrupiSkedulimProdhimi o in ocolTrupiSkedulimProdhimi)
                {
                    o.IdKoka = idkoka;
                    int idM;
                    mesazh = dbRegj.ruajTrupiSkedulimProdhimi(out idM, o.IdKoka,o.IdBurimi,o.IdPlanifikimi,o.IdAktiviteti,o.IdProdukti,o.Data,o.Nga,o.Ne,o.Koha,o.Njesia,o.Kosto,o.KostoTotale,o.Shenime);
                    if (!mesazh.Status)
                    {
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
        public clsMesazh ruaj(IDictionary<string, object> hfNrAutoregjistrime)
        {

            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            //db.krijoManager();
            db.beginTransaksion();
            clsMesazh u_ruajt = ruaj(hfNrAutoregjistrime, db); //perdor ruajtjen me transaksion

            if (!u_ruajt.Status)
            {
                db.rollbackTransaksion();
                return u_ruajt;
            }
            db.commitTransaksion();
            return u_ruajt;
        }

        /// <summary>
        /// Ruan objektin e  kokes se dokumentit te planifikimit ne tabelen perkatese ne databaze
        /// </summary>
        /// <param name="hfNrAutoregjistrime">hiddenField i Devit qe mban numrat automatike,null nese nuk perodren</param>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
        public clsMesazh ruaj(IDictionary<string, object> hfNrAutoregjistrime, clsDatabazeProdhimi db)
        {
            bool kaNdryshimNumri;
            clsMesazh mesazhKontrolli = kontrolloSkedulim(out kaNdryshimNumri, db, hfNrAutoregjistrime);
            if (!mesazhKontrolli.Status)
            {
                return mesazhKontrolli;
            }

            clsMesazh u_ruajt = clsKokaSkedulimProdhimi.ruajSkedulim(IdKoka, IdNivel, IdKonfigAmbjente, IdPlanifikimi, IdBurimi, DtDok, NrDok, IdDokNga, IdStatusDok, IdNdermarrje, IdNdermarrjeVit, IdPerdoruesi, DtRegj, Shenime, IdNivelGjenerues, IdKonfigGjenerues, IdGjenerues, IdKrijuesi,  colTrupi,  db, false);
            if (!u_ruajt.Status)
            {
                return u_ruajt;
            }
            if (kaNdryshimNumri)
                return mesazhKontrolli;
            return u_ruajt;
        }

        /// <summary>
        /// kontrollon dokumentin e skedulimit gjate ruajtjes dhe ben ndryshimet per nr automatik
        /// </summary>
        /// <param name="kaNdryshimNumri">tregon nese ka ndryshuar numri</param>
        /// <param name="dbRegj">clsdatabazeprodhimi per transaksion</param>
        /// <param name="hfNrAutoregjistrime">hiddenfield me fushat e nr automatik</param>
        /// <returns> clsMesazh </returns>
        private clsMesazh kontrolloSkedulim(out bool kaNdryshimNumri, clsDatabazeProdhimi dbRegj, IDictionary<string, object> hfNrAutoregjistrime)
        {
            kaNdryshimNumri = false;
            clsMesazh mes = new clsMesazh();

            if (hfNrAutoregjistrime != null)
            {
                mes = kontrolloNrAutoSkedulim(out kaNdryshimNumri, dbRegj, hfNrAutoregjistrime);
                if (!mes.Status)
                    return mes;
            }
            if (dbRegj.ekzistonRegjistrimSkedulimProdhimi(idKonfigAmbjente, nrDok, dtDok, idNdermarrje))
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
        private clsMesazh kontrolloNrAutoSkedulim(out bool kaNdryshimNumri, clsDatabazeProdhimi dbRegj, IDictionary<string, object> hfregjistrime)
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
        /// <param name="idplanifikim"> id e klient furnitorit me te cilin lidhet</param>
        /// <param name="idKonf"> id e konfigurimit te dokumentit</param>
        /// <param name="idLidhes">id e dokumentit nga e cila gjenerohet ne rastet e modifikimit </param>
        /// <param name="idburim">id e magazines</param>
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
        /// <param name="ocolTrupiSkedulimProdhimi">kolektion i trupit te planifikimit</param>
        /// <param name="idgrup1">id e grupimit te pare </param>
        /// <param name="idgrup2">id e grupimit te dyte</param>
        /// <param name="idgrup3">id e grupimit te trete</param>
        /// <returns> kthen nje obj clsMesazh per te identifikuar statusin e modifikimit se te dhenave ne DB</returns>
        public static clsMesazh modifikoSkedulim(int idkoka, int idNiv, int idKonf, int idplanifikim, int idburim, DateTime dtDk, string nrDk, int idLidhes, int idSt, int idNder, int idNdVt, int idPer, DateTime dtRegj, string shenim, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues, colTrupiSkedulimProdhimi ocolTrupiSkedulimProdhimi, clsDatabazeProdhimi dbProdh)
        {
            colTrupiSkedulimProdhimi trupat = new colTrupiSkedulimProdhimi();
            trupat.ktheTrupiSkedulimProdhimi(idkoka, idNder, dbProdh);
            clsMesazh mesazh = new clsMesazh();
            try
            {   //obj koka qe i kalohet si parameter eshte si duhet te modifikohet
                clsKokaSkedulimProdhimi kokaEkzistuese = new clsKokaSkedulimProdhimi();
                kokaEkzistuese.mbushKokaSkedulimProdhimiSipasID(idkoka, dbProdh);
                if (string.IsNullOrEmpty(kokaEkzistuese.NrDok) || kokaEkzistuese.IdStatusDok == 2)
                {
                    return new clsMesazh(false, STR_DokumentiKaNdryshuarJuLutemRihapeniPerseri);
                }
                kokaEkzistuese.IdStatusDok = 2;  //duhet vendosur nje status i pershtatshem per kete modifikim
                idLidhes = kokaEkzistuese.IdKoka;
                mesazh = dbProdh.modifikoKokaSkedulimProdhimi(kokaEkzistuese.IdKoka, kokaEkzistuese.IdNivel, kokaEkzistuese.IdKonfigAmbjente, kokaEkzistuese.IdBurimi,kokaEkzistuese.IdPlanifikimi,  kokaEkzistuese.DtDok, kokaEkzistuese.NrDok, kokaEkzistuese.idDokNga, kokaEkzistuese.IdStatusDok, kokaEkzistuese.idNdermarrje, kokaEkzistuese.idNdermarrjeVit, idPer, kokaEkzistuese.DtRegj, kokaEkzistuese.Shenime, kokaEkzistuese.idNivelGjenerues, kokaEkzistuese.idKonfigGjenerues, kokaEkzistuese.idGjenerues);

                if (!mesazh.Status)
                {
                    return mesazh;
                }
               
                mesazh = ruajSkedulim(idkoka, idNiv, idKonf, idplanifikim, idburim, dtDk, nrDk, idLidhes, idSt, idNder, idNdVt, idPer, dtRegj, shenim, idNivelGjenerues, idKonfigGjenerues, idGjenerues, kokaEkzistuese.idKrijuesi, ocolTrupiSkedulimProdhimi,  dbProdh, true);

                return mesazh;
            }
            catch (Exception ce)
            {

                return new clsMesazh(false, ce.Message);
            }
        }

        /// <summary>
        /// Modifikon objektin e  kokes se dokumentit te skedulimit ne tabelen perkatese ne databaze
        /// </summary>
        /// <param name="lidhur">tregon nese eshte i lidhur ose jo</param>
        /// <returns > nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo</returns>
        public clsMesazh modifiko(bool lidhur)
        {
            clsMesazh u_modifikua;
            clsDatabazeProdhimi data = new clsDatabazeProdhimi();
            if (lidhur == false)
            {
                //data.krijoManager();
                data.beginTransaksion();
                u_modifikua = modifikoSkedulim(IdKoka, IdNivel, IdKonfigAmbjente, IdPlanifikimi, IdBurimi, DtDok, NrDok, IdDokNga, IdStatusDok, IdNdermarrje, IdNdermarrjeVit, IdPerdoruesi, DtRegj, Shenime, IdNivelGjenerues, IdKonfigGjenerues, IdGjenerues,  colTrupi, data);
                if (u_modifikua.Status)
                    data.commitTransaksion();
                else data.rollbackTransaksion();
            }
            else
            {
                u_modifikua = data.modifikoKokaSkedulimProdhimi(IdKoka, IdNivel, IdKonfigAmbjente,  IdBurimi,IdPlanifikimi, DtDok, NrDok, idDokNga, IdStatusDok, idNdermarrje, idNdermarrjeVit, idPerdoruesi, DtRegj, Shenime, idNivelGjenerues, idKonfigGjenerues, idGjenerues);
                data.Dispose();
            }
            return u_modifikua;
        }


        /// <summary>
        /// fshin nje objekt dokument skedulim prodhimi  duke i ndryshuar statusin
        /// </summary>
        ///<param name="idkoka"> koka e dokumentit te skedulimit i cili do te fshihet</param>
        ///<param name="idperdoruesi">id e perdoruesit qe ka bere veprimin</param>
        ///<param name="dbProdh"> clsdatabaseProdhimi si pjese e transaksionit</param>
        /// <returns> kthen nje obj clsMesazh per te identifikuar statusin e fshirjes se te dhenave ne DB</returns>
        public static clsMesazh fshiSkedulim(int idkoka, int idperdoruesi, clsDatabazeProdhimi dbProdh)
        {

            clsMesazh mesazh = new clsMesazh(true);

            try
            {
                clsKokaSkedulimProdhimi kokaEkzistuese = new clsKokaSkedulimProdhimi();
                kokaEkzistuese.mbushKokaSkedulimProdhimiSipasID(idkoka, dbProdh);
                kokaEkzistuese.IdStatusDok = 2;  //duhet vendosur nje status i pershtatshem per kete modifikim
               
                mesazh = dbProdh.fshiKokaSkedulimProdhimi(kokaEkzistuese.IdKoka, idperdoruesi);
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
        /// Fshin objektin e  kokes se dokumentit te skedulimit ne tabelen perkatese ne databaze
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh fshi()
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            //db.krijoManager();
            db.beginTransaksion();
            clsMesazh u_fshi = fshiSkedulim(IdKoka, idPerdoruesi, db);
            if (u_fshi.Status)
                db.commitTransaksion();
            else db.rollbackTransaksion();
            return u_fshi;
        }

        /// <summary>
        /// Merr objektin e  kokes se dokumentit te skedulimit te prodhimit sipas id nga tabela perkatese ne databaze
        /// </summary>
        /// <param name="id"> id e kokes se skedulimit</param>
        /// <returns > nje objekt clsKokaSkedulimProdhimi qe permban objektin e kerkuar</returns>
        public static clsKokaSkedulimProdhimi merrSipasId(int id)
        {
            clsKokaSkedulimProdhimi data = new clsKokaSkedulimProdhimi();
            //data.mbushKokaPlanifikimSipasID(id, null);
            data.mbushKokaSkedulimProdhimiSipasID(id);
            return data;
        }

        /// <summary>
        /// Merr objektin e  kokes se dokumentit te skedulimit sipas nr dhe dt dokumenti nga tabela perkatese ne databaze.
        /// </summary>
        /// <returns > nje objekt clsKokaSkedulimProdhimi qe permban objektin e kerkuar</returns>
        public clsKokaSkedulimProdhimi merrSipasIdNivelNrDokDtDok()
        {
            clsKokaSkedulimProdhimi data = new clsKokaSkedulimProdhimi(IdNivel, NrDok, DtDok);
            return data;
        }

        /// <summary>
        /// Merr gjithe e  kokat e dokumentave te skedulim prodhimi sipas ndermarjevitit nga tabela perkatese ne databaze
        /// </summary>
        /// <param name="idNdermVit">id e ndermarje vitit</param>
        /// <returns > nje object colKokaSkedulimProdhimi qe permban nje koleksion me gjithe kokat e dokumentave te skedulimit te nje ndermarje ne nje vit te caktuar</returns>
        public static colKokaSkedulimProdhimi merriTeGjithe(int idNdermVit)
        {
            colKokaSkedulimProdhimi data = new colKokaSkedulimProdhimi(idNdermVit);
            return data;
        }

        /// <summary>
        /// Merr trupin  e  nje dokumenti te skedulim prodhhimi nga tabela perkatese ne databaze
        /// </summary>
        /// <returns > nje object colTrupiSkedulimProdhimi qe permban nje koleksion me trupin e dokumentit te skedulimit</returns>
        public colTrupiSkedulimProdhimi merrTrupiSkedulimProdhimi(clsDatabazeProdhimi db)
        {
            colTrupiSkedulimProdhimi trupi = new colTrupiSkedulimProdhimi();
            trupi.ktheTrupiSkedulimProdhimi(IdKoka, IdNdermarrje, db);
            return trupi;
        }

        /// <summary>
        /// mbush koken e skedulim prodhimi sipas id dokNga
        /// </summary>
        /// <param name="idDokNga">id dokumenti nga</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        public bool mbushKokaSkedulimProdhimiSipasIDDokNga(int iddokNga)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            bool mbush = mbushKokaSkedulimProdhimi(db.ktheKokaSkedulimProdhimiSipasIDDokNga(iddokNga), db);
            db.Dispose();
            return mbush;
        }

        /// <summary>
        /// mbush koken e skedulimit te prodhimit sipas id se kokes
        /// </summary>
        /// <param name="idKoka">id e kokes </param>
        /// <returns>kthen true nese mbushja kryhet me sukses ne te kundert kthen false</returns>        
        public bool mbushKokaSkedulimProdhimiSipasID(int idKoka)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            bool mbush = mbushKokaSkedulimProdhimiSipasID(idKoka, db);
            db.Dispose();
            return mbush;
        }

        /// <summary>
        /// mbush koken e skedulim prodhimi sipas id se kokes
        /// </summary>
        /// <param name="idKoka">id e kokes </param>
        /// <returns>kthen true nese mbushja kryhet me sukses ne te kundert kthen false</returns>
        /// <param name="db"></param>
        public bool mbushKokaSkedulimProdhimiSipasID(int idKoka, clsDatabazeProdhimi db)
        {
            //if (db == null) 
            //    db = new clsDatabazeProdhimi();
            bool mbush = mbushKokaSkedulimProdhimi(db.ktheKokaSkedulimProdhimiSipasID(idKoka), db);
            return mbush;
        }
        public static bool kaAutorizime(int idkoka, int idperdoruesi)
        {
            clsDatabazeProdhimi dbKokaMagazina = new clsDatabazeProdhimi();
            bool sukses = dbKokaMagazina.kaAutorizimKokaSkedulimProdhimi(idkoka, idperdoruesi);
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
        internal bool mbushKokaSkedulimProdhimi(DataRow rreshti, clsDatabazeProdhimi db)
        {
            if (rreshti != null)
            {
                try
                {
                    int.TryParse(rreshti["IDKOKA"].ToString(), out idKoka);
                    int.TryParse(rreshti["IDNIVEL"].ToString(), out idNivel);
                    int.TryParse(rreshti["IDKONFIGAMBJENTE"].ToString(), out idKonfigAmbjente);
                    int.TryParse(rreshti["IDPLANIFIKIMI"].ToString(), out idPlanifikimi);
                    int.TryParse(rreshti["IDBURIMI"].ToString(), out idBurimi);
                    int.TryParse(rreshti["IDKRIJUESI"].ToString(), out idKrijuesi);
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
                    DateTime.TryParse(rreshti["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(rreshti["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    colTrupi = new colTrupiSkedulimProdhimi();
                    colTrupi = merrTrupiSkedulimProdhimi(db);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception(STR_ERRORGabimGjateMarrjesSeDokumentitTeSkedulimProdhimit);
                }
            }
            else
                return false;
        }

        #endregion
    }
}


