using DbCore.DbAdmin;
using DbCore.DbShare;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Resources;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Extensions;
using DbCore.DbKontabiliteti;
using System.Linq;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbQendraKosto
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne  koken e nje dokumenti te qendra kosto
    ///  (Te dhenat  merren nga tabela : T_KOKAQENRAKOSTO)
    /// </summary>
    public class clsKokaQendraKosto
    {
        #region Struktura ndihmese
        /// <summary>
        /// Struktura qe perdoret per te gjitha objektet ne shperndarjen e QK
        /// </summary>
        public struct TrupaFkPerShperndarjeQK
        {
            public colTrupatFletetKontabel TrupatFK;
            public int IdDegeAdministrative;
            public int IdDepartamenti;
            public int IdNendepartamenti;
            public int IdMagazina;
            public List<int> IdLlogariObjektiv;
            public List<double> Kursi;
            public List<double> VleraObjektiva;
            public List<double> VleraMonBazeObjektiva;
            public colObjektivaKosto Objektivat;
        }

        /// <summary>
        /// Struktura qe perdoret per shperndarjen e vlerave te llogarive ne qendra kosto
        /// </summary>
        public struct QenderKostoTrupFK
        {
            public int IdQendra;
            public int LlojiQk;
            public int IdLlogaria;
            public string NrLlogari;
            public string EmerLlogari;
            public int IdMonedha;
            public string KodiMonedha;
            public double Kursi;
            public double VleftaDebiMonBazeTrupiFleteKontabel;
            public double VleftaDebiTrupiFleteKontabel;
            public double VleftaKrediMonBazeTrupiFleteKontabel;
            public double VleftaKrediTrupiFleteKontabel;
            public List<int> IdLlogariObjektiv;
            public List<double> VleraObjektiva;
            public List<double> VleraMonBazeObjektiva;
            public colObjektivaKosto Objektivat;
        }
        #endregion

        #region Attributet
        /// <summary>
        /// id e kokes se dokumentit 
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
        /// nr i references te fletes kontabel
        /// </summary>
        private int nrRef;
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
        /// pershkrimi
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
        private int idKokaMagazina;//duhet per rivleresim
        /// <summary>
        /// koleksioni me trupin e qendres se kostos
        /// </summary>
        private colTrupiQendraKosto colTrupi;
        private DataRow rreshti;
        private clsDatabaseQendraKosto db;

        #endregion

        #region Properties
        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdKoka { get { return idKoka; } set { idKoka = value; } }
        public int IdKokaMagazina { get { return idKokaMagazina; } set { idKokaMagazina = value; } }
        /// <summary>
        /// Kthen/Vendos ID-ne  e nivelit te regjistrimit.
        /// </summary>
        public int IdNivel { get { return idNivel; } set { idNivel = value; } }
        /// <summary>
        /// Kthen/Vendos ID-ne e konfigurimit te ambjentit
        /// </summary>
        public int IdKonfigAmbjente { get { return idKonfigAmbjente; } set { idKonfigAmbjente = value; } }
        /// <summary>
        /// Kthen/Vendos nr e ref
        /// </summary>
        public int NrRef { get { return nrRef; } set { nrRef = value; } }
        /// <summary>
        /// Kthen/Vendos nr i dokumentit.
        /// </summary>
        public String NrDok { get { return nrDok; } set { nrDok = value; } }
        /// <summary>
        /// Kthen/Vendos dt e dokumentit.
        /// </summary>
        public DateTime DtDok { get { return dtDok; } set { dtDok = value; } }
        /// <summary>
        /// Kthen/Vendos pershkrimi.
        /// </summary>
        public String Pershkrimi { get { return pershkrimi; } set { pershkrimi = value; } }
        /// <summary>
        /// Kthen/Vendos ID-ne  e dokumentit nga i cili gjenerohet ne rastet e modifikimit
        /// </summary>
        public int IdDokNga { get { return idDokNga; } set { idDokNga = value; } }
        /// <summary>
        /// Kthen/Vendos ID-ne e gjendjes se dokumentit
        /// <example> ruajtur, draft etj.</example>
        /// </summary>
        public int IdStatusDok { get { return idStatusDok; } set { idStatusDok = value; } }
        /// <summary>
        /// Kthen/Vendos ID-ne  e ndermarjes.
        /// </summary>
        public int IdNdermarrje { get { return idNdermarrje; } set { idNdermarrje = value; } }
        /// <summary>
        /// Kthen/Vendos ID-ne e ndermarje vitit.
        /// </summary>
        public int IdNdermarrjeVit { get { return idNdermarrjeVit; } set { idNdermarrjeVit = value; } }
        /// <summary>
        /// Kthen/Vendos ID-ne  e perdoruesit qe e ka ruajtur.
        /// </summary>
        public int IdPerdoruesi { get { return idPerdoruesi; } set { idPerdoruesi = value; } }
        /// <summary>
        /// Kthen/Vendos dt e regjistrimit te dokumentit.
        /// </summary>
        public DateTime DtRegj { get { return dtRegj; } set { dtRegj = value; } }
        /// <summary>
        /// Kthen/Vendos ID-ne e nivelit te dokumentit nga eshte gjeneruar 
        /// </summary>
        public int IdNivelGjenerues { get { return idNivelGjenerues; } set { idNivelGjenerues = value; } }
        /// <summary>
        /// Kthen/Vendos ID-ne e konfigurimit te dokumentit nga eshte gjeneruar 
        /// </summary>
        public int IdKonfigGjenerues { get { return idKonfigGjenerues; } set { idKonfigGjenerues = value; } }
        /// <summary>
        /// Kthen/Vendos ID-ne e dokumentit nga eshte gjeneruar  nga nje ambjent tjeter
        /// </summary>
        public int IdGjenerues { get { return idGjenerues; } set { idGjenerues = value; } }
        /// <summary>
        /// Kthen/Vendos  nje koleksion me trupin e dokumentit 
        /// </summary>
        public colTrupiQendraKosto ColTrupi { get { return colTrupi; } set { colTrupi = value; } }
        /// <summary>
        /// data e krijimit te dokumentit
        /// </summary>
        public DateTime DtKrijimi { get { return dtKrijimi; } }
        /// <summary>
        /// data e modifikimit te fundit te dokumentit
        /// </summary>
        public DateTime DtModifikimi { get { return dtModifikimi; } }

        #endregion

        #region Konstruktoret
        /// <summary>
        /// Konstruktori i klases me parametra
        /// </summary>
        /// <param name="dtDk"> data e dokumentit te qendres se kostos</param>
        /// <param name="dtRegj"> data e regjistrimit te dokumentit te qendres se kostos</param>
        /// <param name="nrref"> nr i ref</param>
        /// <param name="idKonf"> id e konfigurimit te dokumentit</param>
        /// <param name="idLidhes">id e dokumentit nga e cila gjenerohet ne rastet e modifikimit </param>
        /// <param name="idkoka">id ritese e kokes se dokumentit</param>
        /// <param name="idNder"> id e ndermarjes</param>
        /// <param name="idNdVt"> id e ndermarje vitit</param>
        /// <param name="idNiv"> id e nivelit te regjistrimit</param>
        /// <param name="idPer"> id e perdoruesit se e ben regjistrimin</param>
        /// <param name="idSt"> id e statusit te dokumentit</param>
        /// <param name="nrDk">nr i dokumentit</param>
        /// <param name="shenim"> shenime </param>
        /// <param name="idGjenerues">id e dokumentit nga gjenerohet dokumenti nga nje ambjent tjeter</param>
        /// <param name="idKonfigGjenerues">id e konfigurimit nga gjenerohet dokumenti</param>
        /// <param name="idNivelGjenerues">id e nivelit nga gjenerohet dokumenti</param>   
        public clsKokaQendraKosto(int idkoka, int idNiv, int idKonf, int nrref, DateTime dtDk, string nrDk, int idLidhes, int idSt, int idNder, int idNdVt, int idPer, DateTime dtRe, string shenim, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues)
        {
            idKoka = idkoka;
            idNivel = idNiv;
            idKonfigAmbjente = idKonf;
            nrRef = nrref;
            nrDok = nrDk;
            dtDok = dtDk;
            idDokNga = idLidhes;
            idStatusDok = idSt;
            idNdermarrje = idNder;
            idNdermarrjeVit = idNdVt;
            idPerdoruesi = idPer;
            dtRegj = dtRe;
            pershkrimi = shenim;
            this.idNivelGjenerues = idNivelGjenerues;
            this.idKonfigGjenerues = idKonfigGjenerues;
            this.idGjenerues = idGjenerues;
            colTrupi = new colTrupiQendraKosto();
        }
        /// <summary>
        /// konstruktori me nje parameter
        /// </summary>
        /// <param name="idkoka">id koka</param>
        public clsKokaQendraKosto(int idkoka)
        {
            using (clsDatabaseQendraKosto db = new clsDatabaseQendraKosto())
                mbushKokaQendraKosto(db.ktheKokaQenderKostoSipasID(idkoka));
        }
        public clsKokaQendraKosto(int idkoka, clsDatabaseQendraKosto db)
        {
            mbushKokaQendraKosto(db.ktheKokaQenderKostoSipasID(idkoka));
        }
        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsKokaQendraKosto()
        {
            colTrupi = new colTrupiQendraKosto();
        }
        public clsKokaQendraKosto(DataRow rreshti)
        {
            mbushKokaQendraKosto(rreshti);
        }
        public clsKokaQendraKosto(DataRow rreshti, bool rivleresim)
        {
            mbushKokaQendraKosto(rreshti);
        }
        #endregion

        #region Metoda Private

        private static clsMesazh ruajQendraKosto(DbRegjistrim.clsKokaMagazina mag)
        {
            clsMesazh mesazh = new clsMesazh();
            if (!(mag.IdStatusDok == 1 && DbShare.clsAlternativaKushti.getAlternativa(mag.IdKonfigAmbjente, "GJK") != "Jo"))
                return new clsMesazh(true, "Sduhet gjeneruar qender kostoje!");
            DbQendraKosto.colObjektivaKosto objektivat;
            List<double> vleratobjektiva; List<double> vleratobjektivamonbaze;
            List<int> idllogobj;
            string pershkrimFK, shfaqmesazhapolupe = "";
            if (mag.Shenime != String.Empty)
                pershkrimFK = mag.Shenime;
            else
                if (mag.IdLlojDokumentiMagazine == 1)
                pershkrimFK = "Nga hyrjet e magazinës";
            else
                pershkrimFK = "Nga daljet e magazinës";
            bool gjithmone = false;

            if (DbShare.clsAlternativaKushti.getAlternativa(mag.IdKonfigAmbjente, "GJKGJ") == "Po")
                gjithmone = true;
            DbQendraKosto.clsDatabaseQendraKosto dbqendra = new DbQendraKosto.clsDatabaseQendraKosto();
            dbqendra.beginTransaksion();
            try
            {
                DbRegjistrim.clsDatabaseRegjistrim dbRegj = new DbRegjistrim.clsDatabaseRegjistrim(dbqendra);
                DbKontabiliteti.clsDatabaseKontabilitet dbkont = new DbKontabiliteti.clsDatabaseKontabilitet(dbqendra);
                DbRegjistrim.clsKokaMagazina magtransf = new DbRegjistrim.clsKokaMagazina();
                bool kamDokTransferimi = magtransf.mbushKokaMagazinaSipasIDGjenerues(mag.IdKokaMagazina, 1, mag.IdKonfigAmbjente, dbRegj);
                if (kamDokTransferimi && !magtransf.OcolTrupiMagazina.mbushGjitheTrupiMagazinaNgaKoka(magtransf.IdKokaMagazina, dbRegj))
                {
                    dbqendra.rollbackTransaksion();
                    return new clsMesazh(true, "nuk lexohet trupi i kokes se magazines");
                }
                int idPeriudheDoku = new DbAdmin.clsPeriudhaKontabel(mag.DtDok, mag.IdNdermarrje, new clsDatabaseAdmin(dbqendra)).IdPeriudha;
                DbAsete.colAmortizimiKoka col = new DbAsete.colAmortizimiKoka();
                col.ktheAmortizimKokaSipasIdGjeneruesi(mag.IdKokaMagazina, mag.IdKonfigAmbjente);

                mag.OFleteKontabel = DbKontabiliteti.clsKokaFleteKontabel.GjeneroKontabilizimMagazine(mag.IdKokaMagazina, mag.IdNivel, mag.IdKonfigAmbjente, mag.DtDok, mag.NrDok, mag.Vlefta, mag.IdNdermarrje, mag.IdNdermarrjeVit, mag.IdPerdoruesi, mag.DtRegjistrimi, mag.OcolTrupiMagazina, pershkrimFK, 0, 6, idPeriudheDoku, mag.IdLlogari, mag.IdNjesiVartese, mag.MeKonfirmim, null, 0, 6, out objektivat, out vleratobjektiva, out vleratobjektivamonbaze, out idllogobj, mag.IdDegeAdministrative, 0, 0, out shfaqmesazhapolupe, 0, new DbQendraKosto.colTrupiQendraKosto(), magtransf.OcolTrupiMagazina, col, gjithmone, dbkont, mag.IdMagazina);

                if (mag.OFleteKontabel.KokaQendraKosto.NrDok != null && mag.OFleteKontabel.KokaQendraKosto.NrDok != "")
                {
                    DbKontabiliteti.clsKokaFleteKontabel kokafk = new DbKontabiliteti.clsKokaFleteKontabel(mag.IdKokaMagazina, 6, dbkont);
                    mesazh = mag.OFleteKontabel.KokaQendraKosto.Ruaj(dbqendra, kokafk.IdKokaFleteKontabel);
                    if (!mesazh.Status)
                    {
                        dbqendra.rollbackTransaksion();
                        return mesazh;
                    }
                }
                dbqendra.commitTransaksion();
            }
            catch (Exception)
            {
                dbqendra.rollbackTransaksion();
            }
            return new clsMesazh(true, "Qendra u gjenerua me sukses!");
        }

        /// <summary>
        /// kontrollon te dhenat e kokes ne jane te sakta
        /// </summary>
        /// <returns>clsMesazh </returns>
        private clsMesazh kontrollo(bool kontrollpashperndare, clsDatabaseQendraKosto db, bool kontrollodebikredi)
        {
            if (nrDok == "")
                return new clsMesazh(false, MessagesResource.Messages["STR_NumriIDokumentitNukMundTeJeteBosh"]);
            if (dtDok == null || dtDok.ToShortDateString() == "01/01/0100")
                return new clsMesazh(false, MessagesResource.Messages["STR_ZgjidhniDatenEDokumentit"]);
            if (dtRegj == null || dtRegj.ToShortDateString() == "01/01/0100")
                return new clsMesazh(false, MessagesResource.Messages["STR_ZgjidhniDatenERegjistrimit"]);

            clsDatabaseKontabilitet dbkont = new clsDatabaseKontabilitet(db);
            clsDatabaseAdmin dbadmin = new clsDatabaseAdmin(db);
            List<int> llogarite = new List<int>();
            List<double> vleratllogarite = new List<double>();
            foreach (clsTrupiQendraKosto trupi in colTrupi)
            {
                if (trupi.VleftaMonBaze == 0)
                    return new clsMesazh(false, MessagesResource.Messages["STR_VleraNukDuhetTeJeteZero"]);

                clsQendraKosto q = clsQendraKosto.getQendraKostoByCodeFromCache(trupi.Qendra, idNdermarrje, db);
                if (q.Id < 1)
                    return new clsMesazh(false, "Qendra e kostos nuk ekziston!");

                if (!q.Aktiv)
                    return new clsMesazh(false, $"Qendra e kostos me kod {q.Kodi} nuk eshte aktive!" );

                if (clsQendraKosto.eshtePrindQendraKosto(q.Id,db))
                    return new clsMesazh(false, "Kjo qender eshte qender prind dhe nuk mund te perdoret!");

                if (trupi.Objektiva != "")
                {
                    if (!clsObjektivaKosto.ekzistonOK(trupi.Objektiva, idNdermarrje, db))
                        return new clsMesazh(false, "Objektiva e kostos nuk ekziston!");
                    clsObjektivaKosto obj = new clsObjektivaKosto(trupi.Objektiva, idNdermarrje, db);
                    if (!obj.Aktiv)
                        return new clsMesazh(false, "Objektiva e kostos nuk eshte aktive!");
                    if (obj.Nga.Date > dtDok.Date || (obj.Deri != new DateTime() && obj.Deri.Date < dtDok.Date))
                        return new clsMesazh(false, "Objektiva e kostos" + obj.Kodi + " nuk eshte aktive ne kete date!");

                }

                if (!clsLlogari.ekzistonLlogari(trupi.Llogaria, idNdermarrje, dbkont))
                    return new clsMesazh(false, "Llogaria nuk ekziston!");
                if (kontrollodebikredi)
                    if (llogarite.Contains(trupi.IdLlog))
                    {
                        int index = llogarite.IndexOf(trupi.IdLlog);
                        if (trupi.DebiKredi == 1)
                            vleratllogarite[index] += trupi.VleftaMonBaze;
                        else vleratllogarite[index] -= trupi.VleftaMonBaze;
                    }
                    else
                    {
                        llogarite.Add(trupi.IdLlog);
                        if (trupi.DebiKredi == 1)
                            vleratllogarite.Add(trupi.VleftaMonBaze);
                        else vleratllogarite.Add(-trupi.VleftaMonBaze);
                    }

            }
            if (kontrollodebikredi)
            {
                for (int i = 0; i < llogarite.Count; i++)
                {
                    if (Math.Round(vleratllogarite[i], 2) != 0)
                        return new clsMesazh(false, "Veprimi eshte i pakuadruar!");
                }
            }
            colTrupatFletetKontabel coltfk = new colTrupatFletetKontabel();
            if(idGjenerues > 0)
                coltfk.mbushTrupiSipasKokesPerQK(idGjenerues, dbkont);
            clsQendraKosto qkp = new clsQendraKosto();
            qkp.merrQKP(idNdermarrje, db);
            clsNdermarrje nder = new clsNdermarrje(idNdermarrje, dbadmin);
            colTrupiQendraKosto trupaqkp = new colTrupiQendraKosto();
            foreach (clsTrupiFleteKontabel trup in coltfk)
            {
                double shuma = trup.VleftaDebiTrupiFleteKontabel;
                foreach (clsTrupiQendraKosto trupi in colTrupi)
                {
                    if (trupi.IdLlog == trup.IdLlogari && trupi.KursiLlog == trup.Kursi)
                        if ((trupi.DebiKredi == 1 && trup.DK == "D") || (trupi.DebiKredi == 2 && trup.DK == "K"))
                            shuma -= trupi.VleftaLlog;
                        else shuma += trupi.VleftaLlog;
                }

                if ((Math.Round(shuma, 2) < 0))
                    return new clsMesazh(false, "Vlera e shperndare eshte me e madhe se vlera e prekur per llogarine:" + trup.NrLlogari);
                else if (kontrollpashperndare && ((Math.Round(shuma, 2) > 0)))
                    return new clsMesazh(false, "Llogaria " + trup.NrLlogari + " nuk eshte shperndare ne qendra kostoje per vleren " + Math.Abs(shuma) + ". Doni ta shperndani?");
                else if (!kontrollpashperndare && ((Math.Round(shuma, 2) > 0)))
                {
                    double kursi = 1;
                    if (nder.NdermarrjeMonedha == trup.IdMonedha)
                        kursi = 1;
                    else
                    {
                        clsKurset kur = clsKurset.getKursSipasIdMonedhaFromCache(trup.IdMonedha, dtDok, dbadmin);
                        kursi = kur.VleraKursi;
                        if (kursi == 0)
                            kursi = 1;
                    }
                    clsTrupiQendraKosto tr = new clsTrupiQendraKosto(0, 0, qkp.Id, qkp.Kodi, qkp.Pershkrimi, 0, "", "", trup.IdLlogari, trup.NrLlogari, trup.EmerLlogari, trup.DK.ToLower() == "d" ? 1 : 2, Math.Abs(shuma), Math.Abs(shuma) * kursi, Math.Abs(shuma) * kursi, trup.KodMonedha, trup.Kursi, trup.PershkrimTrupiFleteKontabel);
                    trupaqkp.Add(tr);
                }
            }
            colTrupi.AddRange(trupaqkp);
            return new clsMesazh(true, MessagesResource.Messages["msgKontrolletUKaluanMeSukses"]);
        }

        /// <summary>
        /// kontrollon dokumentin e qendra kosto gjate ruajtjes dhe ben ndryshimet per nr automatik
        /// </summary>
        /// <param name="kaNdryshimNumri">tregon nese ka ndryshuar numri</param>
        /// <param name="dbRegj">clsDatabaseQendraKosto per transaksion</param>
        /// <param name="hfNrAutoregjistrime">hiddenfield me fushat e nr automatik</param>
        /// <returns> clsMesazh </returns>
        private clsMesazh kontrolloQendreKosto(clsDatabaseQendraKosto dbRegj)
        {

            if (idGjenerues == 0 && dbRegj.ekzistonRegjistrimQenderKosto(idKonfigAmbjente, nrDok, dtDok, idNdermarrje))
                return new clsMesazh(false, MessagesResource.Messages["msgEkziston1RegjistrimMeTeNjejtinNrDokumenti"]);

            return new clsMesazh(true, MessagesResource.Messages["STR_KontrolliIQKUKryeMeSukses"]);
        }

        /// <summary>
        /// Modifikon nje objekt dokument qender kosto sebashku me te trupin 
        /// </summary>
        /// <param name="dtDk"> data e dokumentit </param>
        /// <param name="dtRegj"> data e regjistrimit te dokumentit </param>
        /// <param name="nrref"> nr ref</param>
        /// <param name="idKonf"> id e konfigurimit te dokumentit</param>
        /// <param name="idLidhes">id e dokumentit nga e cila gjenerohet ne rastet e modifikimit </param>
        /// <param name="idkoka">id ritese e kokes </param>
        /// <param name="idNder"> id e ndermarjes</param>
        /// <param name="idNdVt"> id e ndermarje vitit</param>
        /// <param name="idNiv"> id e nivelit te regjistrimit</param>
        /// <param name="idPer"> id e perdoruesit se e ben regjistrimin</param>
        /// <param name="idSt"> id e statusit te dokumentit</param>
        /// <param name="nrDk">nr i dokumentit</param>
        /// <param name="shenim"> shenime </param>
        /// <param name="idGjenerues">id e dokumentit nga gjenerohet qendra kosto nga nje ambjent tjeter</param>
        /// <param name="idKonfigGjenerues">id e konfigurimit nga gjenerohet dokumenti</param>
        /// <param name="idNivelGjenerues">id e nivelit nga gjenerohet dokumenti</param>
        /// <param name="ocolTrupiPlanifikim">kolektion i trupit te planifikimit</param>
        /// <returns> kthen nje obj clsMesazh per te identifikuar statusin e modifikimit se te dhenave ne DB</returns>
        private static clsMesazh modifikoQendraKosto(int idkoka, int idNiv, int idKonf, int nrref, DateTime dtDk, string nrDk, int idSt, int idNder, int idNdVt, int idPer, DateTime dtRegj, string shenim, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues, colTrupiQendraKosto ocolTrupiPlanifikim, int idstatusfshirje, clsDatabaseQendraKosto dbProdh)
        {
            clsMesazh mesazh = new clsMesazh();
            try
            {   //obj koka qe i kalohet si parameter eshte si duhet te modifikohet
                clsKokaQendraKosto kokaEkzistuese = new clsKokaQendraKosto();
                kokaEkzistuese.mbushKokaQendraKostoSipasID(idkoka, dbProdh);
                if (string.IsNullOrEmpty(kokaEkzistuese.NrDok) || kokaEkzistuese.IdStatusDok == 2)
                    return new clsMesazh(false, MessagesResource.Messages["msgDokumentiKaNdryshuarHapeniPerseri"]);

                mesazh = kokaEkzistuese.Fshi(idPer, idstatusfshirje, dbProdh);
                if (!mesazh.Status)
                    return mesazh;

                mesazh = ruajQendraKosto(idkoka, idNiv, idKonf, nrref, dtDk, nrDk, kokaEkzistuese.IdKoka, idSt, idNder, idNdVt, idPer, dtRegj, shenim, idNivelGjenerues, idKonfigGjenerues, idGjenerues, ocolTrupiPlanifikim, dbProdh);
                return mesazh;
            }
            catch (Exception ce)
            {
                return new clsMesazh(false, ce.Message);
            }
        }

        /// <summary>
        /// Modifikon objektin e  kokes se dokumentit te qendra kosto ne tabelen perkatese ne databaze
        /// </summary>
        /// <param name="lidhur">tregon nese eshte i lidhur ose jo</param>
        /// <returns > nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo</returns>
        private clsMesazh modifiko(bool lidhur, int idstatusfshirje, clsDatabaseQendraKosto data)
        {
            data.beginTransaksion();

            clsMesazh u_modifikua = modifikoQK(lidhur, idstatusfshirje, data);
            if (u_modifikua.Status)
                data.commitTransaksion();
            else
                data.rollbackTransaksion();

            return u_modifikua;
        }

        private clsMesazh modifikoQK(bool lidhur, int idstatusfshirje, clsDatabaseQendraKosto data)
        {
            clsMesazh u_modifikua;
            if (!lidhur)
            {
                u_modifikua = modifikoQendraKosto(IdKoka, IdNivel, IdKonfigAmbjente, NrRef, DtDok, NrDok, IdStatusDok, IdNdermarrje, IdNdermarrjeVit, IdPerdoruesi, DtRegj, Pershkrimi, IdNivelGjenerues, IdKonfigGjenerues, IdGjenerues, colTrupi, idstatusfshirje, data);
            }
            else
                u_modifikua = data.modifikoKokaQenderKosto(IdKoka, IdNivel, IdKonfigAmbjente, NrRef, DtDok, NrDok, IdStatusDok, idNdermarrje, idNdermarrjeVit, idPerdoruesi, DtRegj, Pershkrimi);
            return u_modifikua;
        }

        private static clsMesazh kaloNeHistorik(int idkoka, int idPerdoruesi, int idStatusDok, clsDatabaseQendraKosto db)
        {
            clsMesazh mesazh = db.kaloNeHistorikKokaQenderKosto(idkoka, idPerdoruesi, idStatusDok);
            return mesazh;
        }

        /// <summary>
        /// mbush koken e qendra kosto sipas id se kokes
        /// </summary>
        /// <param name="idKoka">id e kokes se qendra kosto</param>
        /// <returns>kthen true nese mbushja kryhet me sukses ne te kundert kthen false</returns>
        /// <param name="db"></param>
        private bool mbushKokaQendraKostoSipasID(int idKoka, clsDatabaseQendraKosto db)
        {
            return mbushKokaQendraKosto(db.ktheKokaQenderKostoSipasID(idKoka));
        }
        
        /// <summary>
        /// krijon objektin e dokumentit te qendra e kostos kur krijohet nga dokumentat gjenerues
        /// </summary>
        /// <param name="idNiv">id e nivelit</param>
        /// <param name="idKonf">id e konfigurimit te dokumentit</param>
        /// <param name="nrref">nr reference</param>
        /// <param name="dtDk">data e dokumentit</param>
        /// <param name="nrDk">nr i dokumentit</param>
        /// <param name="iddoknga">id e dokumentit nga eshte krijuar ne modifikim</param>
        /// <param name="idSt">id e statusit te dokumentit</param>
        /// <param name="idNder">id e ndermarjes</param>
        /// <param name="idNdVt">id e ndermarje vitit</param>
        /// <param name="idPer">id e perdoruesit</param>
        /// <param name="dtRe">dt e regjistrimit</param>
        /// <param name="shenim">shenime</param>
        /// <param name="idNivelGjenerues">id e nivelit te dokumentit qe e ka gjeneruar</param>
        /// <param name="idKonfigGjenerues"> id e konfigurimit te dokumentit qe e ka gjeneruar</param>
        /// <param name="idGjenerues">id e dokumentit qe e ka gjeneruar</param>
        /// <param name="coltrup">trupi i dokumentit</param>
        /// <returns> kthen clsMesazh </returns>
        private clsMesazh krijoQendraKosto(int idNiv, int idKonf, int nrref, DateTime dtDk, string nrDk, int iddoknga, int idSt, int idNder, int idNdVt, int idPer, DateTime dtRe, string shenim, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues, colTrupiQendraKosto coltrup, bool kontrollpashperndare, bool kontrollodebikredi, clsDatabaseQendraKosto db)
        {
            idNivel = idNiv;
            idKonfigAmbjente = idKonf;
            nrRef = nrref;
            nrDok = nrDk;
            dtDok = dtDk;
            idDokNga = iddoknga;
            idStatusDok = idSt;
            idNdermarrje = idNder;
            idNdermarrjeVit = idNdVt;
            idPerdoruesi = idPer;
            dtRegj = dtRe;
            pershkrimi = shenim;
            this.idNivelGjenerues = idNivelGjenerues;
            this.idKonfigGjenerues = idKonfigGjenerues;
            this.idGjenerues = idGjenerues;
            colTrupi = coltrup;
            colTrupi.ForEach(el => el.Pershkrimi = (el.Pershkrimi != "") ? el.Pershkrimi : shenim);
            clsMesazh mesazh = kontrollo(kontrollpashperndare, db, kontrollodebikredi);
            if (!mesazh.Status)
                return mesazh;
            return new clsMesazh(true, MessagesResource.Messages["STR_DokumentiQKUKrijuaMeSukses"]);
        }

        /// <summary>
        /// Ruan nje objekt dokumenti qendra kosto sebashku me trupin  
        /// </summary>
        /// <param name="dtDk"> data e dokumentit te qendres se kostos</param>
        /// <param name="dtRegj"> data e regjistrimit te dokumentit te qendres se kostos</param>
        /// <param name="nrref"> nr reference</param>
        /// <param name="idKonf"> id e konfigurimit te dokumentit</param>
        /// <param name="idLidhes">id e dokumentit nga e cila gjenerohet ne rastet e modifikimit </param>
        /// <param name="idkoka">id ritese e kokes se qendres se kostos</param>
        /// <param name="idNder"> id e ndermarjes</param>
        /// <param name="idNdVt"> id e ndermarje vitit</param>
        /// <param name="idNiv"> id e nivelit te regjistrimit</param>
        /// <param name="idPer"> id e perdoruesit se e ben regjistrimin</param>
        /// <param name="idSt"> id e statusit te dokumentit</param>
        /// <param name="nrDk">nr i dokumentit</param>
        /// <param name="shenim"> shenime </param>
        /// <param name="idGjenerues">id e dokumentit nga gjenerohet qendra e kostos nga nje ambjent tjeter</param>
        /// <param name="idKonfigGjenerues">id e konfigurimit nga gjenerohet dokumenti</param>
        /// <param name="idNivelGjenerues">id e nivelit nga gjenerohet dokumenti</param>
        /// <param name="ocolTrupi">kolektion i trupit te planifikimit</param>
        /// <param name="dbRegj"> clsDatabaseQendraKosto per transaksionin</param>
        /// <returns> kthen nje obj clsMesazh per te identifikuar statusin e ruajtjes se te dhenave ne DB</returns>
        private static clsMesazh ruajQendraKosto(int idkoka, int idNiv, int idKonf, int nrref, DateTime dtDk, string nrDk, int idLidhes, int idSt, int idNder, int idNdVt, int idPer, DateTime dtRegj, string shenim, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues, colTrupiQendraKosto ocolTrupi, clsDatabaseQendraKosto dbRegj)
        {
            clsMesazh mesazh;
            mesazh = dbRegj.ruajKokaQendraKosto(out idkoka, idNiv, idKonf, nrref, dtDk, nrDk, idLidhes, idSt, idNder, idNdVt, idPer, dtRegj, shenim, idNivelGjenerues, idKonfigGjenerues, idGjenerues);

            if (!mesazh.Status)
                return new clsMesazh(false, MessagesResource.Messages["STR_NdodhiNjeGabimGjateRuajtjesSeKokesSeQK"]);

            ocolTrupi.VendosIdKokeNeTrup(idkoka);

            DataTable dtTrupi = ocolTrupi.ToDataTable("IdTrupi", "IdKoka", "IdQK", "IdOk", "IdLlog", "DebiKredi", "VleftaLlog", "VleftaQK", "VleftaMonBaze", "KursiLlog", "Pershkrimi");

            mesazh = dbRegj.RuajTrupQendraKostoDT(dtTrupi);
            return mesazh;
        }

        /// <summary>
        /// Kthen te grupuara trupat e fletes kontabel qe jane per tu shperndare, sipas konfigurimeve. 
        /// </summary>
        /// <param name="trupateRiFKPerShperndarje">Lista e elemneteve per tu shperndare</param>
        /// <param name="konf">Konfigurimi i QK</param>
        /// <param name="dbQk"></param>
        /// <param name="ekzistonLlogPerQK">Variabel bool nese asnjera nga llogarite e prekura nuk i perket ndermarrjes</param>
        /// <param name="kaQKP">Variabel bool nese ne listen e QK te reja do kete QKP</param>
        /// <param name="QKP">Qendra e kostos e pacaktuar e ndermarrjes</param>
        /// <param name="_colQK">Kolesksion i qendrave qe mund te preken</param>
        /// <param name="IdNdermarrje">IdNdermarrje</param>
        /// <returns>Funksioni kthen nje liste objektesh te ngjashem me trupin e fletes kontabel, te grupuara sipas llogarive, qendrave dhe objektivave te kostos</returns>
        private static List<QenderKostoTrupFK> MerrQKTeGrupuarperTRFKSipasKonfigurimeve(List<TrupaFkPerShperndarjeQK> trupateRiFKPerShperndarje, ref clsKonfigurimQK konf, clsDatabaseQendraKosto dbQk, ref bool ekzistonLlogPerQK, ref bool kaQKP, clsQendraKosto QKP, ref colQendraKosto _colQK, int IdNdermarrje)
        {
            List<QenderKostoTrupFK> QKperTRFK = new List<QenderKostoTrupFK>();
            clsDatabaseKontabilitet dbKont = new clsDatabaseKontabilitet(dbQk);
            colLlogarite colLlog = new colLlogarite(IdNdermarrje, true, dbKont);
            if (konf.Prioriteti1 == 0)
            {
                konf.Prioriteti1 = (int)PrioritetShperndarje.Dege_Administrative;
                konf.Prioriteti2 = (int)PrioritetShperndarje.Nendepartamenti;
                konf.Prioriteti3 = (int)PrioritetShperndarje.Departamenti;
                konf.Prioriteti4 = (int)PrioritetShperndarje.Llogari_Kontabel;
                konf.Prioriteti5 = (int)PrioritetShperndarje.Magazine;
                konf.IdMenyreMesazhi = 3;
            }
            DbRegjistrim.clsDatabaseRegjistrim dbRegj = new DbRegjistrim.clsDatabaseRegjistrim(dbQk);
            foreach (TrupaFkPerShperndarjeQK obj in trupateRiFKPerShperndarje)
            {
                DbListPagesat.clsDatabazeListPagesa _dbListPagesa = new DbListPagesat.clsDatabazeListPagesa(dbQk);
                DbListPagesat.clsStrukturaAdministrative _departamenti = new DbListPagesat.clsStrukturaAdministrative(obj.IdDepartamenti, _dbListPagesa);
                DbListPagesat.clsStrukturaAdministrative _nendepartamenti = new DbListPagesat.clsStrukturaAdministrative(obj.IdNendepartamenti, _dbListPagesa);
                DbRegjistrim.clsDegeAdministrative _degaAdministrative = new DbRegjistrim.clsDegeAdministrative(obj.IdDegeAdministrative, dbRegj);
                DbRegjistrim.clsNjesiAdministrative _magazina = obj.IdMagazina == 0 ? new DbRegjistrim.clsNjesiAdministrative() : new DbRegjistrim.clsNjesiAdministrative(obj.IdMagazina, dbRegj);

                foreach (clsTrupiFleteKontabel trup in obj.TrupatFK)
                {
                    int _idQK = 0;
                    int _llojiQK = 1;
                    clsLlogari _llogaria = new clsLlogari(trup.IdLlogari, dbKont);
                    if (colLlog.Exists(p => p.IdLlogari == trup.IdLlogari))
                    {
                        MerrIdDheLlojQendre(konf, ref _llojiQK, ref _idQK, _llogaria, _magazina, _degaAdministrative, _departamenti, _nendepartamenti);
                        ekzistonLlogPerQK = true;
                        if (_idQK == 0 || _idQK == -1)
                        {
                            _llojiQK = 1;
                            _idQK = QKP.Id;
                            kaQKP = true;
                        }
                        if (!_colQK.Exists(q => q.Id == _idQK))
                            _colQK.Add(new clsQendraKosto(_idQK, dbQk));

                        QenderKostoTrupFK rreshti = new QenderKostoTrupFK { IdQendra = _idQK, LlojiQk = _llojiQK, IdLlogaria = trup.IdLlogari, VleftaDebiMonBazeTrupiFleteKontabel = trup.VleftaDebiMonBazeTrupiFleteKontabel, VleftaDebiTrupiFleteKontabel = trup.VleftaDebiTrupiFleteKontabel, VleftaKrediMonBazeTrupiFleteKontabel = trup.VleftaKrediMonBazeTrupiFleteKontabel, VleftaKrediTrupiFleteKontabel = trup.VleftaKrediTrupiFleteKontabel, Kursi = trup.Kursi, NrLlogari = _llogaria.NrLlogari, EmerLlogari = _llogaria.EmerLlogari1, IdMonedha = _llogaria.IdMonedha, KodiMonedha = _llogaria.KodiMonedha, Objektivat = obj.Objektivat, VleraObjektiva = obj.VleraObjektiva, VleraMonBazeObjektiva = obj.VleraMonBazeObjektiva, IdLlogariObjektiv = obj.IdLlogariObjektiv };

                        QKperTRFK.Add(rreshti);
                    }
                }
            }

            List<QenderKostoTrupFK> QKperTRFKgrupuar = (
                from f in QKperTRFK
                group f by new { f.IdQendra, f.LlojiQk, f.IdLlogaria, f.NrLlogari, f.EmerLlogari, f.Kursi, f.IdMonedha, f.KodiMonedha, f.Objektivat, f.IdLlogariObjektiv, f.VleraObjektiva, f.VleraMonBazeObjektiva }
                into g
                select new QenderKostoTrupFK { IdQendra = g.Key.IdQendra, LlojiQk = g.Key.LlojiQk, IdLlogaria = g.Key.IdLlogaria, NrLlogari = g.Key.NrLlogari, EmerLlogari = g.Key.EmerLlogari, Kursi = g.Key.Kursi, IdMonedha = g.Key.IdMonedha, KodiMonedha = g.Key.KodiMonedha, Objektivat = g.Key.Objektivat, VleraObjektiva = g.Key.VleraObjektiva, VleraMonBazeObjektiva = g.Key.VleraMonBazeObjektiva, IdLlogariObjektiv = g.Key.IdLlogariObjektiv, VleftaDebiMonBazeTrupiFleteKontabel = Convert.ToDouble(g.Sum(f => f.VleftaDebiMonBazeTrupiFleteKontabel)), VleftaDebiTrupiFleteKontabel = Convert.ToDouble(g.Sum(f => f.VleftaDebiTrupiFleteKontabel)), VleftaKrediMonBazeTrupiFleteKontabel = Convert.ToDouble(g.Sum(f => f.VleftaKrediMonBazeTrupiFleteKontabel)), VleftaKrediTrupiFleteKontabel = Convert.ToDouble(g.Sum(f => f.VleftaKrediTrupiFleteKontabel)) }
                ).ToList();

            return QKperTRFKgrupuar;
        }

        #endregion

        #region Metoda Publike

        public static int KtheIdStatusDokSipasID(int idKoka)
        {
            using (clsDatabaseQendraKosto db = new clsDatabaseQendraKosto())
                return db.ktheIdStatusDokSipasIdKoka(idKoka);
        }

        /// <summary>
        /// kontrollon nese dokumenti eshte i lidhur
        /// </summary>
        /// <returns> true ose false</returns>
        public bool EshteILidhur()
        {
            DbAdmin.clsDatabaseAdmin dbAdmin = new DbAdmin.clsDatabaseAdmin();
            bool lidhur = dbAdmin.eshteDokumentiILidhur(idKoka, idNivel, "T_KOKAQENDRAKOSTO", "IDKOKA");
            dbAdmin.Dispose();
            return lidhur;
        }

        /// <summary>
        /// merr id e dokumentave qe e kane lidhur
        /// </summary>
        /// <returns>Data table me keto id</returns>
        public DataTable MerrIdsDokLidhur()
        {
            DbAdmin.clsDatabaseAdmin dbAdmin = new DbAdmin.clsDatabaseAdmin();
            DataTable dt = dbAdmin.MerrDokLidhur(idKoka, idNivel, "T_KOKAQENDRAKOSTO", "IDKOKA");
            dbAdmin.Dispose();
            return dt;
        }

        public bool eshteDokumentILidhur(int idKoka, int idNivel)
        {
            using (clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin())
            {
                DataTable dt = dbAdmin.MerrDokLidhur(idKoka, idNivel, "T_KOKAQENDRAKOSTO", "IDKOKA");
                return (dt.Rows.Count > 0) ? true : false;
            }
        }

        public static clsKokaQendraKosto KrijoQenderRe(clsKonfigurimAmbjenti konfig, colTrupatFletetKontabel colTrupFk, colObjektivaKosto objektivat, List<double> vleratobjektiva, List<double> vleratobjektivamonbaze, List<int> idllogobj, int statusdok, out string shfaqmesazhapolupe, int idNdermarrje, int idGjuha, string shtimModifikim, int idKokaFleteKontabel, string pershkrimi, DateTime data, DateTime dateRegjistrimi, int idNderVit, int idPerdoruesi, string nrDokumenti)
        {
            shfaqmesazhapolupe = "jo";
            //if (statusdok == 0)
            //    return new clsKokaQendraKosto();
            var clsKonf = new clsKonfigurimAmbjenti();
            clsKonf.mbushKonfigDefaultKomponentes(906, idNdermarrje);

            var kusht = new clsKusht(konfig.IdKonfigAmbjente, "ZRQK");
            if (kusht.Vlera != 0)
                clsKonf.mbushKonfigAmbjSipasId(kusht.Vlera, idGjuha);
            else
                clsKonf.mbushKonfigAmbjSipasKod("RQK", idNdermarrje);
            var rishpernda = clsAlternativaKushti.getAlternativa(clsKonf.IdKonfigAmbjente, "RSKDMD") == "Po";
            var shperndaDifQKPModDok = false;
            var qendravjeter = new clsKokaQendraKosto();
            if (shtimModifikim == "modifikim")
            {
                var kok = new clsKokaFleteKontabel();
                kok = new clsKokaFleteKontabel(idKokaFleteKontabel);
                qendravjeter.KtheKokaQKSipasIDGjeneruesDheKonfigMeTrup(kok.IdKokaFleteKontabel, kok.IdKonfigAmbjente);
                shperndaDifQKPModDok = clsAlternativaKushti.getAlternativa(clsKonf.IdKonfigAmbjente, "SHDQKPMD") == "Po";
            }
            var trupipadublikime = new colTrupatFletetKontabel();
            foreach (var trup in colTrupFk)
            {
                var eksiton = false;
                foreach (var trupiri in trupipadublikime)
                {
                    if (trup.IdLlogari != trupiri.IdLlogari) continue;
                    eksiton = true;
                    if (trup.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                    {
                        trupiri.VleftaDebiMonBazeTrupiFleteKontabel += trup.VleftaDebiMonBazeTrupiFleteKontabel;
                        trupiri.VleftaDebiTrupiFleteKontabel += trup.VleftaDebiTrupiFleteKontabel;
                    }
                    else if (trup.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                    {
                        trupiri.VleftaKrediMonBazeTrupiFleteKontabel += trup.VleftaKrediMonBazeTrupiFleteKontabel;
                        trupiri.VleftaKrediTrupiFleteKontabel += trup.VleftaKrediTrupiFleteKontabel;
                    }
                    break;
                }
                if (eksiton) continue;
                var trupr = new clsTrupiFleteKontabel(trup.IdTrupiFleteKontabel, trup.IdKokaFleteKontabel, trup.IdLlogari, trup.PershkrimTrupiFleteKontabel, trup.IdMonedha, trup.Kursi, trup.VleftaDebiTrupiFleteKontabel, trup.VleftaKrediTrupiFleteKontabel, trup.KodMonedha, trup.KodiSkemaKontabel, trup.VleftaDebiMonBazeTrupiFleteKontabel, trup.VleftaKrediMonBazeTrupiFleteKontabel);
                trupipadublikime.Add(trupr);
            }
            var koka = KrijoQK(new DbData(), clsKonf.IdNivel, clsKonf.IdKonfigAmbjente, 1, data, nrDokumenti, 0, statusdok, idNdermarrje, idNderVit, idPerdoruesi, dateRegjistrimi, pershkrimi, konfig.IdNivel, konfig.IdKonfigAmbjente, 0, trupipadublikime, 0, 0, 0, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, out shfaqmesazhapolupe, qendravjeter.ColTrupi, 0, rishpernda, shperndaDifQKPModDok);
            if (idKokaFleteKontabel == 0) return koka;
            var kokaflete = new clsKokaFleteKontabel(idKokaFleteKontabel);
            var qend = new clsKokaQendraKosto();
            qend.KtheKokaQKSipasIDGjeneruesDheKonfig(idKokaFleteKontabel, kokaflete.IdKonfigAmbjente);
            koka.IdKoka = qend.IdKoka;
            return koka;
        }

        /// <summary>
        /// Krijon dokument qenderKostoje dhene idKokaMagazina, kujdes mos ekzistoj me pare se nuk e kontrollon
        /// </summary>
        /// <param name="ci"></param>
        /// <param name="rm"></param>
        /// <param name="idKokaMagazina"></param>
        /// <returns></returns>
        public static clsMesazh KrijoDheRuajQKTeReja(int idKokaMagazina)
        {
            clsMesazh mesazh;
            DbRegjistrim.clsKokaMagazina mag = new DbRegjistrim.clsKokaMagazina();
            if (!mag.mbushKokaMagazinaSipasID(idKokaMagazina))
                return new clsMesazh(false, "nuk u lexua mire dokumenti i magazines me id: " + idKokaMagazina);
            if (!mag.OcolTrupiMagazina.mbushGjitheTrupiMagazinaNgaKoka(mag.IdKokaMagazina))
                return new clsMesazh(false, "nuk u mbush dot trupi dokut te magazines me id: " + idKokaMagazina);
            mesazh = mag.OcolTrupiMagazina.mbushArtikujTrupi(mag.IdKokaMagazina);
            if (!mesazh.Status)
                return mesazh;
            return ruajQendraKosto(mag);
        }

        /// <summary>
        /// krijon objektin e dokumentit te qender kosto kur krijohet nga ambjenti i qendrave te kostos
        /// </summary>
        /// <param name="idNiv">id e nivelit</param>
        /// <param name="idKonf">id e konfigurimit te dokumentit</param>
        /// <param name="nrref">nrref</param>
        /// <param name="dtDk">data e dokumentit</param>
        /// <param name="nrDk">nr i dokumentit</param>
        /// <param name="idSt">id e statusit te dokumentit</param>
        /// <param name="idNder">id e ndermarjes</param>
        /// <param name="idNdVt">id e ndermarje vitit</param>
        /// <param name="idPer">id e perdoruesit</param>
        /// <param name="dtRegj">dt e regjistrimit</param>
        /// <param name="shenim">shenime</param>
        /// <param name="coltrupi">trupi i dokumentit</param>
        /// <returns> kthen clsMesazh </returns>
        public clsMesazh KrijoQK(int idNiv, int idKonf, int nrref, DateTime dtDk, string nrDk, int iddoknga, int idSt, int idNder, int idNdVt, int idPer, DateTime dtRe, string shenim, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues, colTrupiQendraKosto coltrup, bool kontrollpashperndare, bool kontrollodebikredi)
        {
            using (clsDatabaseQendraKosto db = new clsDatabaseQendraKosto())
                return krijoQendraKosto(idNiv, idKonf, nrref, dtDk, nrDk, iddoknga, idSt, idNder, idNdVt, idPer, dtRe, shenim, idNivelGjenerues, idKonfigGjenerues, idGjenerues, coltrup, kontrollpashperndare, kontrollodebikredi, db);
        }
        
        public static clsKokaQendraKosto KrijoQK(DbData dbData, int idnivel, int ikonfig, int nrref, DateTime dtdok, string nrdok, int iddoknga, int idst, int idnderma, int idnderviti, int iperd, DateTime dtregj, string shenim, int idnivgjenerues, int idkonfiggjenerues, int idgjenerues, DbKontabiliteti.colTrupatFletetKontabel trupiFleteKont, int iddegeadm, int idstrukadm, int idnendep, DbQendraKosto.colObjektivaKosto objektivat, List<double> vleratobjektiva, List<double> vleratobjektivamonbaze, List<int> idllogobj, out string shfaqmesazhapolupe, colTrupiQendraKosto trupivjeter, int idmag, bool rishpernda, bool shperndaDifQKPModDok)
        {
            using (clsDatabaseQendraKosto dbQk = new clsDatabaseQendraKosto(dbData))
            {
                shfaqmesazhapolupe = "jo";
                if (!colLlogariShperndarjeQK.kaLlogariShpernadrjeQKNdermarrja(idnderma, dbQk))
                    return new clsKokaQendraKosto();
                clsKokaQendraKosto kokaFK = new clsKokaQendraKosto();
                colTrupiQendraKosto colTrupFK = new colTrupiQendraKosto();

                TrupaFkPerShperndarjeQK objektiTK = new TrupaFkPerShperndarjeQK { TrupatFK = trupiFleteKont, IdDegeAdministrative = iddegeadm, IdDepartamenti = idstrukadm, IdNendepartamenti = idnendep, IdMagazina = idmag, IdLlogariObjektiv = idllogobj, VleraObjektiva = vleratobjektiva, VleraMonBazeObjektiva = vleratobjektivamonbaze, Objektivat = objektivat };
                List<TrupaFkPerShperndarjeQK> objektetTK = new List<TrupaFkPerShperndarjeQK>();
                objektetTK.Add(objektiTK);

                colTrupFK = KrijoTrupQK(dbQk, objektetTK, trupivjeter, iperd, idnderma, dtdok, rishpernda, shperndaDifQKPModDok, idst, out shfaqmesazhapolupe);

                if (colTrupFK.Count == 0)
                    return kokaFK;
                clsMesazh mesazh = kokaFK.krijoQendraKosto(idnivel, ikonfig, nrref, dtdok, nrdok, iddoknga, idst, idnderma, idnderviti, iperd, dtregj, shenim, idnivgjenerues, idkonfiggjenerues, idgjenerues, colTrupFK, false, false, dbQk);
                if (!mesazh.Status)
                    throw new Exception(mesazh.PershkrimMesazhi);
                return kokaFK;
            }
        }

        public static clsKokaQendraKosto KrijoQK(int idnivel, int ikonfig, int nrref, DateTime dtdok, string nrdok, int iddoknga, int idst, int idnderma, int idnderviti, int iperd, DateTime dtregj, string shenim, int idnivgjenerues, int idkonfiggjenerues, int idgjenerues, DbKontabiliteti.colTrupatFletetKontabel trupiFleteKont, int iddegeadm, int idstrukadm, int idnendep, DbQendraKosto.colObjektivaKosto objektivat, List<double> vleratobjektiva, List<double> vleratobjektivamonbaze, List<int> idllogobj, out string shfaqmesazhapolupe, DbData dbData, DbQendraKosto.colTrupiQendraKosto qendrakostovjeter, int idmag)
        {
            shfaqmesazhapolupe = "jo";
            clsKokaQendraKosto kokaQK = new clsKokaQendraKosto();
            clsDatabaseQendraKosto db = new clsDatabaseQendraKosto(dbData);
            clsDatabaseShare dbShare = new clsDatabaseShare(dbData);

            if (!colLlogariShperndarjeQK.kaLlogariShpernadrjeQKNdermarrja(idnderma, db))
                return kokaQK;

            colTrupiQendraKosto colTrupiQK = new colTrupiQendraKosto();
            bool rishpernda = clsAlternativaKushti.getAlternativa(ikonfig, "RSKDMD", dbShare) == "Po";
            bool shperndaDifQKPModDok = clsAlternativaKushti.getAlternativa(ikonfig, "SHDQKPMD", dbShare) == "Po";

            TrupaFkPerShperndarjeQK objektiTK = new clsKokaQendraKosto.TrupaFkPerShperndarjeQK { TrupatFK = trupiFleteKont, IdDegeAdministrative = iddegeadm, IdDepartamenti = idstrukadm, IdNendepartamenti = idnendep, IdMagazina = idmag, IdLlogariObjektiv = idllogobj, VleraObjektiva = vleratobjektiva, VleraMonBazeObjektiva = vleratobjektivamonbaze, Objektivat = objektivat };
            List<TrupaFkPerShperndarjeQK> objektetTK = new List<TrupaFkPerShperndarjeQK>();
            objektetTK.Add(objektiTK);

            colTrupiQK = KrijoTrupQK(db, objektetTK, qendrakostovjeter, iperd, idnderma, dtdok, rishpernda, shperndaDifQKPModDok, idst, out shfaqmesazhapolupe);

            if (colTrupiQK.Count == 0)
            {
                shfaqmesazhapolupe = "jo";
                return kokaQK;
            }
            clsMesazh mesazh = kokaQK.krijoQendraKosto(idnivel, ikonfig, nrref, dtdok, nrdok, iddoknga, idst, idnderma, idnderviti, iperd, dtregj, shenim, idnivgjenerues, idkonfiggjenerues, idgjenerues, colTrupiQK, false, false, db);
            if (!mesazh.Status)
                throw new MyException(mesazh.PershkrimMesazhi);
            return kokaQK;

        }

        /// <summary>
        /// Ky funksion krijon trupat e qendrave te kostos qe shperndahen pas ruajtjeve/modifikimeve te cdo dokumenti
        /// </summary>
        /// <param name="trupateRiFKPerShperndarje">Lista e elementeve baze qe duhen per shpendarjen e elementeve te dokumentave ne QK</param>
        /// <param name="QendraKostoVjeter">Trupi i vjeter i dokumentit eksizutes nese jemi ne modifikim</param>
        /// <param name="IdPerdoruesi">IdPerdoruesi</param>
        /// <param name="IdNdermarrje">IdNdermarrje</param>
        /// <param name="DtDok">Data e dokumentit</param>
        /// <param name="Rishpernda">Kushti i vendosur ne ambjentin e konfigurimeve RSKDMD: true/Po</param>
        /// <param name="ShperndaDifQKPModDok">Kushti i vendosur ne ambjentin e konfigurimeve SHDQKPMD: true/Po</param>
        /// <param name="IdStatusDok">Statis dok</param>
        /// <param name="shfaqmesazhapolupe">Kthen mesazhin qe do te shfaqet tek lupa</param>
        /// <returns>Funksioni kthen trupin e QK te ri qe do te krijohet</returns>
        public static colTrupiQendraKosto KrijoTrupQK(clsDatabaseQendraKosto _dbQk, List<TrupaFkPerShperndarjeQK> trupateRiFKPerShperndarje, colTrupiQendraKosto QendraKostoVjeter, int IdPerdoruesi, int IdNdermarrje, DateTime DtDok, bool Rishpernda, bool ShperndaDifQKPModDok, int IdStatusDok, out string shfaqmesazhapolupe)
        {
            colTrupiQendraKosto col = new colTrupiQendraKosto();
            clsDatabaseAdmin _dbAdmin = new clsDatabaseAdmin(_dbQk);

            clsMonedha _monNdermarje = new clsMonedha();
            _monNdermarje.merrMonedheNdermarjeNgaCacheja(IdNdermarrje, _dbAdmin);
            int _idMonedheNderm = _monNdermarje.IdMonedha;

            colQendraKosto _colQK = new colQendraKosto();
            clsQendraKosto _QKP = new clsQendraKosto();
            _QKP.merrQKP(IdNdermarrje, _dbQk);

            bool _kaQKP = false;
            bool _ekzistonLlogPerQK = false;
            clsKonfigurimQK _konf = new clsKonfigurimQK(IdNdermarrje, IdPerdoruesi, _dbQk);

            //merren konfigurimet default se ku duhet te shperndahet vlerat e llogarive 
            List<QenderKostoTrupFK> QKperTRFKgrupuar = MerrQKTeGrupuarperTRFKSipasKonfigurimeve(trupateRiFKPerShperndarje, ref _konf, _dbQk, ref _ekzistonLlogPerQK, ref _kaQKP, _QKP, ref _colQK, IdNdermarrje);

            if (Rishpernda)
                col.AddRange(BejShperndarjeNeQKSipasKonfigurimeve(QKperTRFKgrupuar, _dbQk, DtDok, _idMonedheNderm, _dbAdmin, _QKP, _colQK));
            else
            {
                Tuple<List<QenderKostoTrupFK>, List<QenderKostoTrupFK>> _TrupatSipasLlogarive = GrupoTrupatSipasEkzistencesSeLlogarive(QKperTRFKgrupuar, QendraKostoVjeter);

                // Behet shperndarja per llogarite qe ekzistonin me pare te trupi i qendres se kostos
                col.AddRange(BejShperndarjeNeQKSipasKushtitSHDQKPMD(_dbQk, _dbAdmin, _TrupatSipasLlogarive.Item1, QendraKostoVjeter, DtDok, ShperndaDifQKPModDok, _idMonedheNderm, _QKP));

                // Behet shperndarja e llogarive te reja
                col.AddRange(BejShperndarjeNeQKSipasKonfigurimeve(_TrupatSipasLlogarive.Item2, _dbQk, DtDok, _idMonedheNderm, _dbAdmin, _QKP, _colQK));
            }

            shfaqmesazhapolupe = KthePermbajtjePerShfaqjenEMesazhit(_ekzistonLlogPerQK, _kaQKP, IdStatusDok, _konf.IdMenyreMesazhi);
            return col;
        }

        public static void MerrIdDheLlojQendre(clsKonfigurimQK konf, ref int llojiqk, ref int idqk, DbKontabiliteti.clsLlogari llog, DbRegjistrim.clsNjesiAdministrative mag, DbRegjistrim.clsDegeAdministrative dege, DbListPagesat.clsStrukturaAdministrative dep, DbListPagesat.clsStrukturaAdministrative nendep)
        {
            Tuple<int, int> vlerat = PercaktoIdDheLlojQendre(konf.Prioriteti1, llojiqk, idqk, llog, mag, dege, dep, nendep);
            if (vlerat.Item2 == 0 || vlerat.Item2 == -1)
                vlerat = PercaktoIdDheLlojQendre(konf.Prioriteti2, vlerat.Item1, vlerat.Item2, llog, mag, dege, dep, nendep);
            if (vlerat.Item2 == 0 || vlerat.Item2 == -1)
                vlerat = PercaktoIdDheLlojQendre(konf.Prioriteti3, vlerat.Item1, vlerat.Item2, llog, mag, dege, dep, nendep);
            if (vlerat.Item2 == 0 || vlerat.Item2 == -1)
                vlerat = PercaktoIdDheLlojQendre(konf.Prioriteti4, vlerat.Item1, vlerat.Item2, llog, mag, dege, dep, nendep);
            if (vlerat.Item2 == 0 || vlerat.Item2 == -1)
                vlerat = PercaktoIdDheLlojQendre(konf.Prioriteti5, vlerat.Item1, vlerat.Item2, llog, mag, dege, dep, nendep);

            llojiqk = vlerat.Item1;
            idqk = vlerat.Item2;
        }

        public static Tuple<int, int> PercaktoIdDheLlojQendre(int prioriteti, int llojiqk, int idqk, DbKontabiliteti.clsLlogari llog, DbRegjistrim.clsNjesiAdministrative mag, DbRegjistrim.clsDegeAdministrative dege, DbListPagesat.clsStrukturaAdministrative dep, DbListPagesat.clsStrukturaAdministrative nendep)
        {
            switch (prioriteti)
            {
                case (int)PrioritetShperndarje.Llogari_Kontabel:
                    llojiqk = llog.LlojQendre;
                    if (llojiqk == 1)
                        idqk = llog.QendraKostos;
                    else idqk = llog.IdSkemaQendraKosto;
                    break;
                case (int)PrioritetShperndarje.Magazine:

                    llojiqk = mag.LlojQendre;
                    if (llojiqk == 1)
                        idqk = mag.QendraKostos;
                    else idqk = mag.IdSkemaQendraKosto;
                    break;
                case (int)PrioritetShperndarje.Dege_Administrative:
                    llojiqk = dege.LlojQendre;
                    if (llojiqk == 1)
                        idqk = dege.QendraKostos;
                    else idqk = dege.IdSkemaQendraKosto;
                    break;
                case (int)PrioritetShperndarje.Departamenti:
                    llojiqk = dep.LlojQendre;
                    if (llojiqk == 1)
                        idqk = dep.QendraKostos;
                    else idqk = dep.IdSkemaQendraKosto;
                    break;
                case (int)PrioritetShperndarje.Nendepartamenti:
                    llojiqk = nendep.LlojQendre;
                    if (llojiqk == 1)
                        idqk = nendep.QendraKostos;
                    else idqk = nendep.IdSkemaQendraKosto;
                    break;
            }
            return new Tuple<int, int>(llojiqk, idqk);
        }

        public static void LlogaritVleraTrupi(int idLlogaria, int idMonedhaLlog, string kodiMonedhaLlog, string nrLlogaria, string emerLlogaria1, int idQenderKosto, int idMonQK, string kodiQK, string pershkrimiQK, ref colTrupiQendraKosto col, double kursiTRFK, double vlera, double vleramonbaze, int idMonedheNderm, ref double perqindjambetur, colObjektivaKosto objektivat, List<double> vleratobjektiva, List<double> vleratobjektivamonbaze, List<int> idllogobj, int debikredi, double perqindja, bool skeme, bool qkp, double vleraKursit)
        {
            Tuple<double, double> vleraDheKursi = KtheVlereQKDheKurs(idMonedhaLlog, idMonQK, idMonedheNderm, vlera, vleramonbaze, kursiTRFK, vleraKursit);

            double vleraqk = vleraDheKursi.Item1;
            double kursiqk = vleraDheKursi.Item2;

            double vleraperqender = 0, vleraqkperqender = 0, vleramonbazeperqender = 0;

            vleraperqender = (skeme || (!skeme && qkp)) ? vlera * perqindja / 100 : vlera;
            vleraqkperqender = (skeme || (!skeme && qkp)) ? vleraqk * perqindja / 100 : vleraqk;
            vleramonbazeperqender = (skeme || (!skeme && qkp)) ? vleramonbaze * perqindja / 100 : vleramonbaze;
            perqindjambetur = (skeme || (!skeme && qkp)) ? perqindjambetur - perqindja : perqindjambetur;
            
            if (objektivat != null)
            for (int o = 0; o < objektivat.Count; o++)
            {
                if (idllogobj[o] == idLlogaria)
                {
                    double vleftallog = skeme || (!skeme && qkp) ? vleratobjektiva[o] * perqindja / 100 : vleratobjektiva[o];
                    double vleftaqk = (skeme || (!skeme && qkp) ? vleratobjektivamonbaze[o] * perqindja / 100 : vleratobjektivamonbaze[o]) / kursiqk;
                        clsTrupiQendraKosto trqk = new clsTrupiQendraKosto(0, 0, idQenderKosto, kodiQK, pershkrimiQK, objektivat[o].Id, objektivat[o].Kodi, objektivat[o].Pershkrimi, idLlogaria, nrLlogaria, emerLlogaria1, vleftallog > 0 ? 1 : 2, Math.Abs(vleftallog), Math.Abs(vleftaqk), Math.Abs(vleftaqk), kodiMonedhaLlog, kursiqk, "");
                    if (trqk.VleftaMonBaze != 0)
                        col.Add(trqk);

                    vleraperqender = vleraperqender - ((skeme || (!skeme && qkp)) ? vleratobjektiva[o] * perqindja / 100 : vleratobjektiva[o]);
                    vleraqkperqender = vleraqkperqender - ((skeme || (!skeme && qkp)) ? vleratobjektivamonbaze[o] * perqindja / 100 : vleratobjektivamonbaze[o]);
                    vleramonbazeperqender = vleramonbazeperqender - ((skeme || (!skeme && qkp)) ? (vleratobjektivamonbaze[o] / kursiqk) * perqindja / 100 : vleratobjektivamonbaze[o] / kursiqk);
                }
            }
            if (Math.Round(vleramonbazeperqender, 2) != 0)
            {
                DbCore.DbQendraKosto.clsTrupiQendraKosto trqk = new clsTrupiQendraKosto(0, 0, idQenderKosto, kodiQK, pershkrimiQK, 0, "", "", idLlogaria, nrLlogaria, emerLlogaria1, debikredi, Math.Abs(vleraperqender), Math.Abs(vleraqkperqender), Math.Abs(vleramonbazeperqender), kodiMonedhaLlog, kursiTRFK, "");
                if (trqk.VleftaMonBaze != 0)
                    col.Add(trqk);
            }
        }

        public static Tuple<double, double> KtheVlereQKDheKurs(int monedheLlog, int monedheQendre, int idMonedheNderm, double vlera, double vleramonbaze, double kursTrupi, double kursiIFundit)
        {
            double vleraqk = 0;
            double kursi = 1;
            if (monedheQendre == monedheLlog)
            {
                vleraqk = vlera;
                kursi = kursTrupi;
            }
            else if (monedheQendre == idMonedheNderm)
            {
                vleraqk = vleramonbaze;
                kursi = 1;
            }
            else
            {
                if (kursiIFundit == 0)
                    kursiIFundit = 1;
                vleraqk = vleramonbaze / kursiIFundit;
                kursi = kursiIFundit;
            }
            return new Tuple<double, double>(vleraqk, kursi);
        }
 
        /// <summary>
        /// Krijon trupat e qendrave te kostos per grupin e elementeve qe kalohen si parameter
        /// </summary>
        /// <param name="QKperTRFKgrupuar">Lista e elementeve per shperndarje</param>
        /// <param name="dbQk"></param>
        /// <param name="DtDok">Data e dokumentit</param>
        /// <param name="_idMonedheNderm">Monedha e ndermarrjes</param>
        /// <param name="dbAdmin"></param>
        /// <param name="QKP">Qendra e kostos e pacaktuar e ndermarrjes</param>
        /// <param name="_colQK">Koleksion i qendrave te kostos</param>
        /// <param name="col">Lista e trupave te ri te qendrave te kostos, kalohet si reference</param>
        public static colTrupiQendraKosto BejShperndarjeNeQKSipasKonfigurimeve(List<QenderKostoTrupFK> QKperTRFKgrupuar, clsDatabaseQendraKosto dbQk, DateTime DtDok, int _idMonedheNderm, clsDatabaseAdmin dbAdmin, clsQendraKosto QKP, colQendraKosto _colQK)
        {
            colTrupiQendraKosto col = new colTrupiQendraKosto();
            foreach (QenderKostoTrupFK tr in QKperTRFKgrupuar)
            {
                double vlera = tr.VleftaDebiTrupiFleteKontabel - tr.VleftaKrediTrupiFleteKontabel;
                double vleraMonBaze = tr.VleftaDebiMonBazeTrupiFleteKontabel - tr.VleftaKrediMonBazeTrupiFleteKontabel;
                int debikredi = vleraMonBaze > 0 ? 1 : 2;

                if (tr.LlojiQk == 1)
                {
                    double perqindjambetur = 0;
                    double vleraKursit = clsKurset.merrKursinFunditPerMonedheDateDheLloj(_colQK.Find(q => q.Id == tr.IdQendra).IdMonedha, DtDok, 1, dbAdmin);
                    LlogaritVleraTrupi(tr.IdLlogaria, tr.IdMonedha, tr.KodiMonedha, tr.NrLlogari, tr.EmerLlogari, tr.IdQendra, _colQK.Find(q => q.Id == tr.IdQendra).IdMonedha, _colQK.Find(q => q.Id == tr.IdQendra).Kodi, _colQK.Find(q => q.Id == tr.IdQendra).Pershkrimi, ref col, tr.Kursi, vlera, vleraMonBaze, _idMonedheNderm, ref perqindjambetur, tr.Objektivat, tr.VleraObjektiva, tr.VleraMonBazeObjektiva, tr.IdLlogariObjektiv, debikredi, 0, false, false, vleraKursit);
                }
                else
                {
                    double perqindjambetur = 100;
                    colTrupiSkemaQK trskema = new colTrupiSkemaQK(tr.IdQendra, dbQk);
                    foreach (clsTrupiSkemaQK tsqk in trskema)
                    {
                        clsQendraKosto qendra = new clsQendraKosto(tsqk.IdQK, dbQk);
                        double perqindja = Convert.ToDouble(tsqk.Perqindja);
                        double vleraKursit = clsKurset.merrKursinFunditPerMonedheDateDheLloj(qendra.IdMonedha, DtDok, 1, dbAdmin);
                        LlogaritVleraTrupi(tr.IdLlogaria, tr.IdMonedha, tr.KodiMonedha, tr.NrLlogari, tr.EmerLlogari, qendra.Id, qendra.IdMonedha, qendra.Kodi, qendra.Pershkrimi, ref col, tr.Kursi, vlera, vleraMonBaze, _idMonedheNderm, ref perqindjambetur, tr.Objektivat, tr.VleraObjektiva, tr.VleraMonBazeObjektiva, tr.IdLlogariObjektiv, debikredi, perqindja, true, false, vleraKursit);
                    }
                    if (perqindjambetur > 0)
                    {
                        double vleraKursit = clsKurset.merrKursinFunditPerMonedheDateDheLloj(QKP.IdMonedha, DtDok, 1, dbAdmin);
                        LlogaritVleraTrupi(tr.IdLlogaria, tr.IdMonedha, tr.KodiMonedha, tr.NrLlogari, tr.EmerLlogari, QKP.Id, QKP.IdMonedha, QKP.Kodi, QKP.Pershkrimi, ref col, tr.Kursi, vlera, vleraMonBaze, _idMonedheNderm, ref perqindjambetur, tr.Objektivat, tr.VleraObjektiva, tr.VleraMonBazeObjektiva, tr.IdLlogariObjektiv, debikredi, perqindjambetur, false, true, vleraKursit);
                    }
                }
            }
            return col;
        }

        /// <summary>
        /// Sipas SHDQKPMD (Shpernda diferencat ne QKP ne modifikim dokumenti) : 
        ///     Nese eshte true  -> kur dokumenti modifikohet, ndryshimi qe do kene vlerat e llogarive te prekura do te shperndahen ne QKP 
        ///     Nese eshte false -> vlerat e llogarive te prekura do te shperndahen sipas konfigurimeve default
        /// </summary>
        /// <param name="dbQk"></param>
        /// <param name="dbAdmin"></param>
        /// <param name="trupatPerShperndarje"></param>
        /// <param name="QendraKostoVjeter"></param>
        /// <param name="DtDok"></param>
        /// <param name="ShperndaDifQKPModDok"></param>
        /// <param name="_idMonedheNderm"></param>
        /// <param name="QKP"></param>
        /// <returns></returns>
        public static colTrupiQendraKosto BejShperndarjeNeQKSipasKushtitSHDQKPMD(clsDatabaseQendraKosto dbQk, clsDatabaseAdmin dbAdmin, List<QenderKostoTrupFK> trupatPerShperndarje, List<clsTrupiQendraKosto> QendraKostoVjeter, DateTime DtDok, bool ShperndaDifQKPModDok, int _idMonedheNderm, clsQendraKosto QKP)
        {
            colTrupiQendraKosto col = new colTrupiQendraKosto();
            var _totaletEVjetraTeLlogarive = (
                       from f in QendraKostoVjeter
                       group f by new { f.IdLlog, f.KursiLlog } into g
                       select new { IdLlog = g.Key.IdLlog, Kursi = g.Key.KursiLlog, TotaliVjeterMonBaze = g.Sum(f => ((f.DebiKredi == 1) ? (-f.VleftaMonBaze) : f.VleftaMonBaze)), TotaliVjeterLlog = g.Sum(f => ((f.DebiKredi == 1) ? (-f.VleftaLlog) : f.VleftaLlog)) }
                   ).ToList();

            foreach (QenderKostoTrupFK tr in trupatPerShperndarje)
            {
                bool _uShtuaQKP = false;
                double _totaliVjeterMonBaze = _totaletEVjetraTeLlogarive.Where(x => x.IdLlog == tr.IdLlogaria && x.Kursi == tr.Kursi).Sum(x => x.TotaliVjeterMonBaze);
                double _totaliVjeterLlog = _totaletEVjetraTeLlogarive.Where(x => x.IdLlog == tr.IdLlogaria && x.Kursi == tr.Kursi).Sum(x => x.TotaliVjeterLlog);
                

                foreach (clsTrupiQendraKosto _tqkVjeter in QendraKostoVjeter)
                {
                    if (!(tr.IdLlogaria == _tqkVjeter.IdLlog && tr.Kursi == _tqkVjeter.KursiLlog)) continue;
                    clsQendraKosto qendra = new clsQendraKosto(_tqkVjeter.IdQK, dbQk);

                    if (ShperndaDifQKPModDok)
                    {
                        double vleraKursit = clsKurset.merrKursinFunditPerMonedheDateDheLloj(qendra.IdMonedha, DtDok, 1, dbAdmin);

                        clsTrupiQendraKosto trqk = KrijoTrupQKPerQKVjeterSipasSHDQKPMD(tr, _tqkVjeter, vleraKursit, _idMonedheNderm, qendra.IdMonedha);
                        if (trqk.VleftaMonBaze != 0)
                            col.Add(trqk);

                        if (_totaliVjeterLlog != tr.VleftaKrediTrupiFleteKontabel - tr.VleftaDebiTrupiFleteKontabel && !_uShtuaQKP)
                        {
                            double vleraKursitQKP = clsKurset.merrKursinFunditPerMonedheDateDheLloj(QKP.IdMonedha, DtDok, 1, dbAdmin);

                            clsTrupiQendraKosto trQKP = KrijoTrupQKPPerDiferencenSipasSHDQKPMD(tr, _totaliVjeterLlog, _totaliVjeterMonBaze, QKP, vleraKursitQKP, _idMonedheNderm);
                            if (trQKP.VleftaMonBaze != 0)
                                col.Add(trQKP);

                            _uShtuaQKP = true;
                        }
                    }
                    else
                    {
                        double vleraKursit = clsKurset.merrKursinFunditPerMonedheDateDheLloj(qendra.IdMonedha, DtDok, 1, dbAdmin);

                        clsTrupiQendraKosto trqk = KrijoTrupQKDukeShperndareVleratNeQKEkzistuese(tr, _tqkVjeter, vleraKursit, _totaliVjeterMonBaze, qendra.IdMonedha, _idMonedheNderm);
                        if (trqk.VleftaMonBaze != 0)
                            col.Add(trqk);
                    }
                }
            }
            return col;
        }

        public static clsTrupiQendraKosto KrijoTrupQKDukeShperndareVleratNeQKEkzistuese(QenderKostoTrupFK tr, clsTrupiQendraKosto _tqkVjeter, double vleraKursit, double _totaliVjeterMonBaze, int _qendraIdMonedha, int _idMonedheNderm)
        {
            double _vleraRe = tr.VleftaDebiTrupiFleteKontabel - tr.VleftaKrediTrupiFleteKontabel;

            double _vleraMonBazeRe = tr.VleftaDebiMonBazeTrupiFleteKontabel - tr.VleftaKrediMonBazeTrupiFleteKontabel;
            
            Tuple<double, double> vleraDheKursi = KtheVlereQKDheKurs(tr.IdMonedha, _qendraIdMonedha, _idMonedheNderm, _vleraRe, _vleraMonBazeRe, tr.Kursi, vleraKursit);
            double vleraqkre = vleraDheKursi.Item1;

            int debikredi = vleraqkre < 0 ? 2 : 1; //1: Debi, 2: Kredi

            _vleraMonBazeRe = Math.Abs(_totaliVjeterMonBaze != 0 ? _vleraMonBazeRe * (_tqkVjeter.VleftaMonBaze / _totaliVjeterMonBaze) : _vleraMonBazeRe);
            _vleraRe = Math.Abs(_totaliVjeterMonBaze != 0 ? _vleraRe * (_tqkVjeter.VleftaMonBaze / _totaliVjeterMonBaze) : _vleraRe);
            vleraqkre = Math.Abs(_totaliVjeterMonBaze != 0 ? vleraqkre * (_tqkVjeter.VleftaMonBaze / _totaliVjeterMonBaze) : vleraqkre);

            clsTrupiQendraKosto trqk = new clsTrupiQendraKosto(0, 0, _tqkVjeter.IdQK, _tqkVjeter.Qendra, _tqkVjeter.PershkrimiQendra, _tqkVjeter.IdOk, _tqkVjeter.Objektiva, _tqkVjeter.PershkrimiObj, tr.IdLlogaria, tr.NrLlogari, tr.EmerLlogari, debikredi, _vleraRe, vleraqkre, _vleraMonBazeRe, tr.KodiMonedha, tr.Kursi, _tqkVjeter.Pershkrimi);

            return trqk;
        }

        public static clsTrupiQendraKosto KrijoTrupQKPerQKVjeterSipasSHDQKPMD(QenderKostoTrupFK tr, clsTrupiQendraKosto _tqkVjeter, double vleraKursit, int _idMonedheNderm, int _qendraIdMonedha)
        {
            int shenja = _tqkVjeter.DebiKredi == 1 ? -1 : 1;
            double _vlereRe = _tqkVjeter.VleftaLlog * shenja;
            double _vlereMonBazeRe = _tqkVjeter.VleftaMonBaze * shenja;

            Tuple<double, double> vleraDheKursi = KtheVlereQKDheKurs(tr.IdMonedha, _qendraIdMonedha, _idMonedheNderm, _vlereRe, _vlereMonBazeRe, tr.Kursi, vleraKursit);

            clsTrupiQendraKosto trqk = new clsTrupiQendraKosto(0, 0, _tqkVjeter.IdQK, _tqkVjeter.Qendra, _tqkVjeter.PershkrimiQendra, _tqkVjeter.IdOk, _tqkVjeter.Objektiva, _tqkVjeter.PershkrimiObj, tr.IdLlogaria, tr.NrLlogari, tr.EmerLlogari, _tqkVjeter.DebiKredi, Math.Abs(_vlereRe), Math.Abs(vleraDheKursi.Item1), Math.Abs(_vlereMonBazeRe), tr.KodiMonedha, tr.Kursi, _tqkVjeter.Pershkrimi);

            return trqk;
        }

        public static clsTrupiQendraKosto KrijoTrupQKPPerDiferencenSipasSHDQKPMD(QenderKostoTrupFK tr, double _totaliVjeterLlog, double _totaliVjeterMonBaze, clsQendraKosto QKP, double vleraKursitQKP, int _idMonedheNderm)
        {
            double vleraQKP = (tr.VleftaDebiTrupiFleteKontabel - Math.Abs(_totaliVjeterLlog < 0 ? _totaliVjeterLlog : 0)) - (tr.VleftaKrediTrupiFleteKontabel - Math.Abs(_totaliVjeterLlog > 0 ? _totaliVjeterLlog : 0));
            double vleraMonBazeQKP = (tr.VleftaDebiMonBazeTrupiFleteKontabel - Math.Abs(_totaliVjeterMonBaze < 0 ? _totaliVjeterMonBaze : 0)) - (tr.VleftaKrediMonBazeTrupiFleteKontabel - Math.Abs(_totaliVjeterMonBaze > 0 ? _totaliVjeterMonBaze : 0));

            Tuple<double, double> vleraDheKursiQKP = KtheVlereQKDheKurs(tr.IdMonedha, QKP.IdMonedha, _idMonedheNderm, vleraQKP, vleraMonBazeQKP, tr.Kursi, vleraKursitQKP);

            clsTrupiQendraKosto trQKP = new clsTrupiQendraKosto(0, 0, QKP.Id, QKP.Kodi, QKP.Pershkrimi, 0, "", "", tr.IdLlogaria, tr.NrLlogari, tr.EmerLlogari, vleraMonBazeQKP > 0 ? 1 : 2, Math.Abs(vleraQKP), Math.Abs(vleraDheKursiQKP.Item1), Math.Abs(vleraMonBazeQKP), tr.KodiMonedha, tr.Kursi, "");

            return trQKP;
        }

        /// <summary>
        /// Ben grupim te rreshtave sipas llogarive ne llogari ekzistuese ne trupin e vjeter te fletes kontabel dhe joekzistuese 
        /// </summary>
        /// <param name="QKperTRFKgrupuar"></param>
        /// <param name="QendraKostoVjeter"></param>
        /// <returns></returns>
        public static Tuple<List<QenderKostoTrupFK>, List<QenderKostoTrupFK>> GrupoTrupatSipasEkzistencesSeLlogarive(List<QenderKostoTrupFK> QKperTRFKgrupuar, colTrupiQendraKosto QendraKostoVjeter)
        {            
            // Ka interes qe te dihet vetem totali per llogari ne dokumentin e vjeter
            List<QenderKostoTrupFK> llogariEkzistueseTRFK = (
                    from f in QKperTRFKgrupuar
                    where QendraKostoVjeter.Exists(x => x.IdLlog == f.IdLlogaria)
                    group f by new {IdLlogaria = f.IdLlogaria, NrLlogari = f.NrLlogari, EmerLlogari = f.EmerLlogari, IdMonedha = f.IdMonedha, KodiMonedha = f.KodiMonedha, Kursi = f.Kursi }
                    into g
                    select new QenderKostoTrupFK { IdLlogaria = g.Key.IdLlogaria, NrLlogari = g.Key.NrLlogari, EmerLlogari = g.Key.EmerLlogari, IdMonedha = g.Key.IdMonedha, KodiMonedha = g.Key.KodiMonedha, Kursi = g.Key.Kursi, VleftaDebiMonBazeTrupiFleteKontabel = g.Sum(f => f.VleftaDebiMonBazeTrupiFleteKontabel), VleftaDebiTrupiFleteKontabel = g.Sum(f => f.VleftaDebiTrupiFleteKontabel), VleftaKrediMonBazeTrupiFleteKontabel = g.Sum(f => f.VleftaKrediMonBazeTrupiFleteKontabel), VleftaKrediTrupiFleteKontabel = g.Sum(f => f.VleftaKrediTrupiFleteKontabel) }
                ).ToList<QenderKostoTrupFK>();
            
            List<QenderKostoTrupFK> llogariJoEkzistueseTRFK = (
                    from f in QKperTRFKgrupuar
                    where !QendraKostoVjeter.Exists(x => x.IdLlog == f.IdLlogaria)
                    group f by new { IdQendra = f.IdQendra, LlojiQk = f.LlojiQk, IdLlogaria = f.IdLlogaria, NrLlogari = f.NrLlogari, EmerLlogari = f.EmerLlogari, IdMonedha = f.IdMonedha, f.KodiMonedha, Kursi = f.Kursi }
                    into g
                    select new QenderKostoTrupFK { IdQendra = g.Key.IdQendra, LlojiQk = g.Key.LlojiQk, IdLlogaria = g.Key.IdLlogaria, NrLlogari = g.Key.NrLlogari, EmerLlogari = g.Key.EmerLlogari, IdMonedha = g.Key.IdMonedha, KodiMonedha = g.Key.KodiMonedha, Kursi = g.Key.Kursi, VleftaDebiMonBazeTrupiFleteKontabel = g.Sum(f => f.VleftaDebiMonBazeTrupiFleteKontabel), VleftaDebiTrupiFleteKontabel = g.Sum(f => f.VleftaDebiTrupiFleteKontabel), VleftaKrediMonBazeTrupiFleteKontabel = g.Sum(f => f.VleftaKrediMonBazeTrupiFleteKontabel), VleftaKrediTrupiFleteKontabel = g.Sum(f => f.VleftaKrediTrupiFleteKontabel) }
                ).ToList<QenderKostoTrupFK>();

            List<QenderKostoTrupFK> fundit = new List<QenderKostoTrupFK>();
            foreach(QenderKostoTrupFK ll in llogariJoEkzistueseTRFK)
            {
                colObjektivaKosto objektivat = new colObjektivaKosto();
                List<double> vleraObjektivave = new List<double>(); 
                List<double> vleraMonBazeObjektivave = new List<double>();
                List<int> idLLogObjektivave = new List<int>();
                foreach (QenderKostoTrupFK f in QKperTRFKgrupuar)
                {
                    if (QendraKostoVjeter.Exists(x => x.IdLlog == f.IdLlogaria)) continue;
                    if (ll.IdQendra == f.IdQendra && ll.LlojiQk == f.LlojiQk && ll.IdLlogaria == f.IdLlogaria && ll.IdMonedha == f.IdMonedha && ll.Kursi == f.Kursi)
                    {
                        var _arrObjektivat = f.Objektivat.ToArray();
                        var _arrVleratObjektivat = f.VleraObjektiva.ToArray();
                        var _arrVleratMonBazeObjektivat = f.VleraMonBazeObjektiva.ToArray();
                        var _arrIdLlogObjektivat = f.IdLlogariObjektiv.ToArray();

                        var _objektivat = objektivat.ToArray();
                        var _idLlogObjektivave = idLLogObjektivave.ToArray();
                        var _vleraObjektivave = vleraObjektivave.ToArray();
                        var _vleraMonBazeObjektivave = vleraMonBazeObjektivave.ToArray();

                        bool gjendet = false;
                        for (int i = 0; i < _arrObjektivat.Length; i++)
                        {
                            for (int index = 0; index < _idLlogObjektivave.Length; index++)
                            {
                                if (!(_objektivat[index].Id == _arrObjektivat[i].Id && _idLlogObjektivave[index] == _arrIdLlogObjektivat[i])) continue;
                                _vleraObjektivave[index] = _vleraObjektivave[index] + _arrVleratObjektivat[i];
                                _vleraMonBazeObjektivave[index] = _vleraMonBazeObjektivave[index] + _arrVleratMonBazeObjektivat[i];
                                vleraObjektivave = _vleraObjektivave.ToList();
                                vleraMonBazeObjektivave = _vleraMonBazeObjektivave.ToList();
                                gjendet = true;
                            }
                            if (!gjendet)
                            {
                                objektivat.Add(_arrObjektivat[i]);
                                vleraObjektivave.Add(_arrVleratObjektivat[i]);
                                vleraMonBazeObjektivave.Add(_arrVleratMonBazeObjektivat[i]);
                                idLLogObjektivave.Add(_arrIdLlogObjektivat[i]);
                            }
                        }
                    }
                }
                QenderKostoTrupFK el = ll;
                el.Objektivat = objektivat;
                el.VleraObjektiva = vleraObjektivave;
                el.VleraMonBazeObjektiva = vleraMonBazeObjektivave;
                el.IdLlogariObjektiv = idLLogObjektivave;
                fundit.Add(el);
            }

            return new Tuple<List<QenderKostoTrupFK>, List<QenderKostoTrupFK>>(llogariEkzistueseTRFK, fundit);
        }

        /// <summary>
        /// Kthen permbajtjen e mesazhin qe me pas vendos per shfaqjen e lupes se QK
        /// </summary>
        /// <param name="ekzistonLlogPerQK">Variabel bool - eshte true nese ka llogari ekzistente ne listen e elementeve per tu shperndare</param>
        /// <param name="kaQKP">Variabel bool - true nese ne trupin e ri te QK ka QKP</param>
        /// <param name="idStatusDok">Statusi i dokumentit</param>
        /// <param name="konfMenyreMesazhi">Konfigurimi i menyres se mesazhit</param>
        /// <returns></returns>
        public static string KthePermbajtjePerShfaqjenEMesazhit(bool ekzistonLlogPerQK, bool kaQKP, int idStatusDok, int konfMenyreMesazhi)
        {
            string shfaqmesazhapolupe;
            if (!ekzistonLlogPerQK || idStatusDok == 0) // (IdStatusDok == 0) => kur dokumenti qe po kontabilizohet ka kushtin Gjenerim kontabilizimi=Indirekt, fleta kontabel krijohet me status dok 0, ne kete rast nuk duhet te shfaqet mesazhi pavaresisht konfigurimit te QK
                shfaqmesazhapolupe = "jo";
            else
                switch (konfMenyreMesazhi)
                {
                    case 1:
                        shfaqmesazhapolupe = "jo";
                        break;
                    case 2:
                        if (kaQKP)
                            shfaqmesazhapolupe = "shfaqmesazh";
                        else
                            shfaqmesazhapolupe = "jo";
                        break;
                    case 3:
                        shfaqmesazhapolupe = "shfaqmesazh";
                        break;
                    case 4:
                        shfaqmesazhapolupe = "shfaqlupe";
                        break;
                    default:
                        shfaqmesazhapolupe = "jo";
                        break;
                }
            return shfaqmesazhapolupe;
        }

        public clsMesazh rillogarit(DbCore.DbQendraKosto.clsDatabaseQendraKosto dbQK, int idstatusfshirje)
        {
            DbCore.DbShare.clsKonfigurimAmbjenti konfig = new DbCore.DbShare.clsKonfigurimAmbjenti(this.IdKonfigAmbjente, new DbCore.DbShare.clsDatabaseShare(dbQK));
            return rillogarit(idPerdoruesi, konfig, idstatusfshirje, dbQK);
        }

        public clsMesazh rillogarit(int idPerdoruesi, DbCore.DbShare.clsKonfigurimAmbjenti konfig, int idstatusfshirje, DbCore.DbQendraKosto.clsDatabaseQendraKosto dbQK)
        {
            if (this.IdStatusDok == 2)
                return new clsMesazh(true, "Doku eshte me status 2");
            if (this.IdStatusDok == 7)
                return new clsMesazh(true, "Doku eshte me status 7");
            string shfaqmesazhapolupe = "";
            clsDatabaseKontabilitet dbKont = new clsDatabaseKontabilitet(dbQK);
            clsKokaFleteKontabel kokafl = new clsKokaFleteKontabel(this.IdGjenerues, dbKont);
            colTrupatFletetKontabel coltrupfk = new colTrupatFletetKontabel(kokafl.IdKokaFleteKontabel, dbKont);
            DbRegjistrim.clsDatabaseRegjistrim dbRegj = new DbRegjistrim.clsDatabaseRegjistrim(dbQK);

            int iddegeadm = DbRegjistrim.clsDegeAdministrative.ktheIdDegeAdminSipasKategorise(kokafl.IdKategoria, kokafl.IdGjenerues, dbRegj);
            colObjektivaKosto objektivat = new colObjektivaKosto();
            List<double> vleratobjektiva = new List<double>();
            List<double> vleratobjektivamonbaze = new List<double>();
            List<int> idllogobj = new List<int>();

            foreach (clsTrupiFleteKontabel trup in coltrupfk)
            {
                clsObjektivaKosto objekt = new clsObjektivaKosto();
                clsLlogari llog = new clsLlogari(trup.IdLlogari, dbKont);
                if (llog.IdObjektivaKosto != 0 && llog.IdObjektivaKosto != -1)
                {
                    objekt = new clsObjektivaKosto(llog.IdObjektivaKosto, dbQK);
                    if (objekt.Id != 0 && objekt.Id != -1)
                        clsKokaFleteKontabel.ShtoTeDhenaPerObjektivat(this.DtDok, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, llog, trup, objekt);
                }
            }
            clsDatabaseShare dbShare = new clsDatabaseShare(dbQK);
            bool rishpernda = clsAlternativaKushti.getAlternativa(konfig.IdKonfigAmbjente, "RSKDMD", dbShare) == "Po";
            bool shperndaDifQKPModDok = clsAlternativaKushti.getAlternativa(konfig.IdKonfigAmbjente, "SHDQKPMD", dbShare) == "Po";
            colTrupiQendraKosto coltrup = new colTrupiQendraKosto(this.IdKoka, dbQK);
            //DbCore.DbQendraKosto.clsKokaQendraKosto kokaqender = DbCore.DbQendraKosto.clsKokaQendraKosto.krijoQK(konfig.IdNivel, konfig.IdKonfigAmbjente, this.NrRef, this.DtDok, this.NrDok, this.IdKoka, 1, this.IdNdermarrje, this.IdNdermarrjeVit, idPerdoruesi, this.DtRegj, this.Pershkrimi, kokafl.IdNivel, kokafl.IdKonfigAmbjente, kokafl.IdKokaFleteKontabel, coltrupfk, iddegeadm, 0, 0, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, out shfaqmesazhapolupe, coltrup, dbQK, 0, rishpernda, shperndaDifQKPModDok);
            DbCore.DbQendraKosto.clsKokaQendraKosto kokaqender = DbCore.DbQendraKosto.clsKokaQendraKosto.KrijoQK(dbQK, konfig.IdNivel, konfig.IdKonfigAmbjente, this.NrRef, this.DtDok, this.NrDok, this.IdKoka, 1, this.IdNdermarrje, this.IdNdermarrjeVit, idPerdoruesi, this.DtRegj, this.Pershkrimi, kokafl.IdNivel, kokafl.IdKonfigAmbjente, kokafl.IdKokaFleteKontabel, coltrupfk, iddegeadm, 0, 0, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, out shfaqmesazhapolupe, coltrup, 0, rishpernda, shperndaDifQKPModDok);
            kokaqender.IdKoka = this.IdKoka;
            if (kokaqender.colTrupi.Count > 0)
                return kokaqender.modifikoQK(false, idstatusfshirje, dbQK);
            return this.Fshi(this.idPerdoruesi, idstatusfshirje, dbQK); //e fshime nese trupi i ri eshte bosh.
        }

        /// <summary>
        /// Ruan objektin e  kokes se dokumentit te qendra kosto ne tabelen perkatese ne databaze
        /// </summary>
        /// <param name="hfNrAutoregjistrime">hiddenField i Devit qe mban numrat automatike,null nese nuk perodren</param>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
        public clsMesazh Ruaj()
        {
            clsDatabaseQendraKosto db = new clsDatabaseQendraKosto();
            db.beginTransaksion();

            clsMesazh u_ruajt = Ruaj(db, this.idGjenerues); //perdor ruajtjen me transaksion

            if (!u_ruajt.Status)
            {
                db.rollbackTransaksion();
                return u_ruajt;
            }
            db.commitTransaksion();
            return u_ruajt;
        }

        /// <summary>
        /// Ruan objektin e  kokes se dokumentit te qendra kosto ne tabelen perkatese ne databaze
        /// </summary>
        /// <param name="hfNrAutoregjistrime">hiddenField i Devit qe mban numrat automatike,null nese nuk perodren</param>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
        public clsMesazh Ruaj(clsDatabaseQendraKosto db, int idGjenerues)
        {
            this.idGjenerues = idGjenerues;
            clsMesazh mesazhKontrolli = kontrolloQendreKosto(db);

            if (!mesazhKontrolli.Status)
                return mesazhKontrolli;

            return ruajQendraKosto(IdKoka, IdNivel, IdKonfigAmbjente, NrRef, DtDok, NrDok, IdDokNga, IdStatusDok, IdNdermarrje, IdNdermarrjeVit, IdPerdoruesi, DtRegj, Pershkrimi, IdNivelGjenerues, IdKonfigGjenerues, IdGjenerues, colTrupi, db);
        }

        public clsMesazh Modifiko(bool lidhur)
        {
            using (clsDatabaseQendraKosto data = new clsDatabaseQendraKosto())
                return modifiko(lidhur, 2, data);
        }

        /// <summary>
        /// Fshin objektin e  kokes se dokumentit te qendra kosto ne tabelen perkatese ne databaze
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh Fshi(int idperdoruesi, int idstatusfshirje, clsDatabaseQendraKosto dbProdh)
        {
            if (this.idKoka > 0)
            {
                //this.IdStatusDok = idstatusfshirje;  //duhet vendosur nje status i pershtatshem per kete modifikim
                //clsMesazh mesazh = dbProdh.modifikoKokaQenderKosto(this.IdKoka, this.IdNivel, this.IdKonfigAmbjente, this.NrRef, this.DtDok, this.NrDok, this.IdStatusDok, this.idNdermarrje, this.idNdermarrjeVit, idperdoruesi, this.DtRegj, this.pershkrimi);
                clsMesazh mesazh = clsKokaQendraKosto.kaloNeHistorik(this.idKoka, idperdoruesi, idstatusfshirje, dbProdh);
                if (!mesazh.Status)
                    return mesazh;
                mesazh = dbProdh.fshiKokaQenderKosto(idKoka);
                return mesazh;
            }
            else
                return new clsMesazh(true, "Nuk ekziston ske ca fshin");
        }

        /// <summary>
        /// Fshin objektin e  kokes se dokumentit te qendra kosto ne tabelen perkatese ne databaze
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh Fshi(int idPerdoruesi, int idstatusfshirje)
        {
            using (clsDatabaseQendraKosto db = new clsDatabaseQendraKosto())
            {
                db.beginTransaksion();
                clsMesazh u_fshi = Fshi(idPerdoruesi, idstatusfshirje, db);
                if (u_fshi.Status)
                    db.commitTransaksion();
                else db.rollbackTransaksion();
                return u_fshi;
            }
        }

        public bool KtheKokaQKSipasIDGjeneruesDheKonfig(int idgjenerues, int idkonfig)
        {
            using (clsDatabaseQendraKosto db = new clsDatabaseQendraKosto())
                return KtheKokaQKSipasIDGjeneruesDheKonfig(idgjenerues, idkonfig, db);
        }

        public bool KtheKokaQKSipasIDGjeneruesDheKonfigMeTrup(int idgjenerues, int idkonfig)
        {
            using (clsDatabaseQendraKosto db = new clsDatabaseQendraKosto())
                return KtheKokaQKSipasIDGjeneruesDheKonfigMeTrup(idgjenerues, idkonfig, db);
        }

        public bool KtheKokaQKSipasIDGjeneruesDheKonfigMeTrup(int idgjenerues, int idkonfig, clsDatabaseQendraKosto db)
        {
            bool sukses = KtheKokaQKSipasIDGjeneruesDheKonfig(idgjenerues, idkonfig, db);
            this.colTrupi.mbushTrupiQendraKosto(IdKoka, db);
            return sukses;
        }

        public bool KtheKokaQKSipasIDGjeneruesDheKonfig(int idgjenerues, int idkonfig, clsDatabaseQendraKosto db)
        {
            return mbushKokaQendraKosto(db.ktheKokaQenderKostoSipasIDGjeneruesDheKonfig(idgjenerues, idkonfig));
        }

        public static clsKokaQendraKosto KrijoQKLP(int idnivel, int ikonfig, int nrref, DateTime dtdok, string nrdok, int iddoknga, int idst, int idnderma, int idnderviti, int iperd, DateTime dtregj, string shenim, int idnivgjenerues, int idkonfiggjenerues, int idgjenerues, colTrupiQendraKosto colTrupFK, bool shperndaDifNeQKP)
        {
            clsKokaQendraKosto kokaFK = new clsKokaQendraKosto();
            colLlogariShperndarjeQK col = new colLlogariShperndarjeQK(idnderma);
            if (col.Count == 0)
                return kokaFK;
            #region komentuar permbledhja
            colTrupiQendraKosto trupiRi = new colTrupiQendraKosto();
            if (!shperndaDifNeQKP)
                foreach (clsTrupiQendraKosto q in colTrupFK)
                {
                    bool eksitonObjperLlog = false;
                    for (int ob = 0; ob < trupiRi.Count; ob++)
                    {
                        if (trupiRi[ob].IdLlog == q.IdLlog && trupiRi[ob].IdQK == q.IdQK && trupiRi[ob].IdOk == q.IdOk)
                        {
                            eksitonObjperLlog = true;
                            if ((q.DebiKredi == 1 && trupiRi[ob].DebiKredi == 1) || (q.DebiKredi == 2 && trupiRi[ob].DebiKredi == 2))
                            {
                                trupiRi[ob].VleftaLlog += q.VleftaLlog;
                                trupiRi[ob].VleftaMonBaze += q.VleftaMonBaze;
                                trupiRi[ob].VleftaQK += q.VleftaQK;
                            }
                            else
                            {
                                trupiRi[ob].VleftaLlog -= q.VleftaLlog;
                                trupiRi[ob].VleftaMonBaze -= q.VleftaMonBaze;
                                trupiRi[ob].VleftaQK -= q.VleftaQK;
                                if (trupiRi[ob].VleftaMonBaze < 0)
                                {
                                    trupiRi[ob].DebiKredi = trupiRi[ob].DebiKredi == 1 ? 2 : 1;
                                    trupiRi[ob].VleftaLlog = Math.Abs(trupiRi[ob].VleftaLlog);
                                    trupiRi[ob].VleftaMonBaze = Math.Abs(trupiRi[ob].VleftaMonBaze);
                                    trupiRi[ob].VleftaQK = Math.Abs(trupiRi[ob].VleftaQK);
                                }
                                if (trupiRi[ob].VleftaMonBaze == 0)
                                    trupiRi.RemoveAt(ob);
                            }
                        }
                    }

                    if (!eksitonObjperLlog)
                        trupiRi.Add(q);
                }

            #endregion

            if ((trupiRi.Count == 0 && !shperndaDifNeQKP) || (colTrupFK.Count == 0 && shperndaDifNeQKP))
                return kokaFK;
            DbCore.clsMesazh mesazh = kokaFK.KrijoQK(idnivel, ikonfig, nrref, dtdok, nrdok, iddoknga, idst, idnderma, idnderviti, iperd, dtregj, shenim, idnivgjenerues, idkonfiggjenerues, idgjenerues, shperndaDifNeQKP ? colTrupFK : trupiRi, false, false);

            if (!mesazh.Status)
                throw new Exception(mesazh.PershkrimMesazhi); //hedhim exception sepse nuk u krijua fleta
            return kokaFK;
        }

        public static clsMesazh KrijoQKLP(int idnivel, int ikonfig, int nrref, DateTime dtdok, string nrdok, int iddoknga, int idst, int idnderma, int idnderviti, int iperd, DateTime dtregj, string shenim, int idnivgjenerues, int idkonfiggjenerues, int idgjenerues, colTrupiQendraKosto colTrupFK, clsDatabaseQendraKosto db, ref clsKokaQendraKosto kokaFK, bool shperndaDifNeQKP)
        {
            if (!colLlogariShperndarjeQK.kaLlogariShpernadrjeQKNdermarrja(idnderma, db))
                return new clsMesazh(true);
            #region komentuar permbledhja

            colTrupiQendraKosto trupiRi = new colTrupiQendraKosto();
            if (!shperndaDifNeQKP)
                foreach (clsTrupiQendraKosto q in colTrupFK)
                {
                    bool eksitonObjperLlog = false;
                    for (int ob = 0; ob < trupiRi.Count; ob++)
                    {
                        if (trupiRi[ob].IdLlog == q.IdLlog && trupiRi[ob].IdQK == q.IdQK && trupiRi[ob].IdOk == q.IdOk)
                        {
                            eksitonObjperLlog = true;
                            if ((q.DebiKredi == 1 && trupiRi[ob].DebiKredi == 1) || (q.DebiKredi == 2 && trupiRi[ob].DebiKredi == 2))
                            {
                                trupiRi[ob].VleftaLlog += q.VleftaLlog;
                                trupiRi[ob].VleftaMonBaze += q.VleftaMonBaze;
                                trupiRi[ob].VleftaQK += q.VleftaQK;
                            }
                            else
                            {
                                trupiRi[ob].VleftaLlog -= q.VleftaLlog;
                                trupiRi[ob].VleftaMonBaze -= q.VleftaMonBaze;
                                trupiRi[ob].VleftaQK -= q.VleftaQK;
                                if (trupiRi[ob].VleftaMonBaze < 0)
                                {
                                    trupiRi[ob].VleftaLlog = Math.Abs(trupiRi[ob].VleftaLlog);
                                    trupiRi[ob].VleftaMonBaze = Math.Abs(trupiRi[ob].VleftaMonBaze);
                                    trupiRi[ob].VleftaQK = Math.Abs(trupiRi[ob].VleftaQK);
                                    trupiRi[ob].DebiKredi = trupiRi[ob].DebiKredi == 1 ? 2 : 1;
                                }
                                if (trupiRi[ob].VleftaMonBaze == 0)
                                    trupiRi.RemoveAt(ob);
                            }
                        }
                    }

                    if (!eksitonObjperLlog)
                        trupiRi.Add(q);
                }
            #endregion
            if ((trupiRi.Count == 0 && !shperndaDifNeQKP) || (colTrupFK.Count == 0 && shperndaDifNeQKP))
                return new clsMesazh(true);
            DbCore.clsMesazh mesazh = kokaFK.krijoQendraKosto(idnivel, ikonfig, nrref, dtdok, nrdok, iddoknga, idst, idnderma, idnderviti, iperd, dtregj, shenim, idnivgjenerues, idkonfiggjenerues, idgjenerues, shperndaDifNeQKP ? colTrupFK : trupiRi, false, false, db);
            if (!mesazh.Status)
                return new clsMesazh(false, mesazh.PershkrimMesazhi); //hedhim exception sepse nuk u krijua fleta
            return new clsMesazh(true);
        }
             
        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush burimet me te dhenat nga databaza
        /// </summary>
        /// <param name="rreshti">rreshti me te dhena</param>
        /// <returns> kthen ne se mbushja u be ne rregull apo jo</returns>
        internal bool mbushKokaQendraKosto(DataRow rreshti)
        {
            if (rreshti != null)
            {
                try
                {
                    int.TryParse(rreshti["IDKOKA"].ToString(), out this.idKoka);
                    int.TryParse(rreshti["IDNIVEL"].ToString(), out this.idNivel);
                    int.TryParse(rreshti["IDKONFIGAMBJENTE"].ToString(), out this.idKonfigAmbjente);
                    int.TryParse(rreshti["NRREF"].ToString(), out this.nrRef);
                    this.nrDok = rreshti["NRDOK"].ToString();
                    DateTime.TryParse(rreshti["DTDOK"].ToString(), out this.dtDok);
                    int.TryParse(rreshti["IDDOKNGA"].ToString(), out this.idDokNga);
                    int.TryParse(rreshti["IDSTATUSDOK"].ToString(), out this.idStatusDok);
                    int.TryParse(rreshti["IDNDERMARJE"].ToString(), out this.idNdermarrje);
                    int.TryParse(rreshti["IDNDERVITI"].ToString(), out this.idNdermarrjeVit);
                    int.TryParse(rreshti["IDPERDORUESI"].ToString(), out this.idPerdoruesi);
                    DateTime.TryParse(rreshti["DTREGJ"].ToString(), out this.dtRegj);
                    this.pershkrimi = rreshti["PERSHKRIMI"].ToString();
                    int.TryParse(rreshti["IDNIVELGJENERUES"].ToString(), out this.idNivelGjenerues);
                    int.TryParse(rreshti["IDKONFIGGJENERUES"].ToString(), out this.idKonfigGjenerues);
                    int.TryParse(rreshti["IDGJENERUES"].ToString(), out this.idGjenerues);
                    DateTime.TryParse(rreshti["DTKRIJIMI"].ToString(), out this.dtKrijimi);
                    DateTime.TryParse(rreshti["DTMODIFIKIMI"].ToString(), out this.dtModifikimi);
                    if(rreshti.Table.GetAllColumnNames().Contains("IDKOKAMAGAZINA"))
                        int.TryParse(rreshti["IDKOKAMAGAZINA"].ToString(), out this.idKokaMagazina);
                    this.colTrupi = new colTrupiQendraKosto();
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception(MessagesResource.Messages["STR_ERRORGabimGjateMarrjesSeDokumentitTeQK"]);
                }
            }
            else
                return false;
        }
        
        #endregion
    }
}