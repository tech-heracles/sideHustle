using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Web.Script.Serialization;
using DbCore.DbAdmin;
using DbCore.DbKontabiliteti;
using DbCore.DbShare;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne  koken e nje dokumenti  lidhes
    ///  (Te dhenat  merren nga tabela : T_DOKUMENTLIDHESKOKA)
    /// </summary>
    public class clsDokumentLidhesKoka
    {
        #region Atribute

        private int idKoka;
        private String nrLidhje;
        private DateTime dateDokumenti;
        private DateTime dateRegjistrimi;
        private int idKlientFurnitor;
        private int idGjenerues;
        private int idLlojDok;
        private int idNdermarje;
        private int idNderViti;
        private int idNivel;
        private int idKonfigAmbjente;
        private int idDokNga;
        private int idNivelGjenerues;
        private int idKonfigGjenerues;
        private int idStatusDok;
        private int idPerdorues;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        public colDokumentLidhesTrupi OColTrupi;
        private clsKokaFleteKontabel oFleteKontabel;
        private colGjendjeKlientFurnitor oGjendjeKF;
        private DataRow rreshti;

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases me parametra
        /// </summary>
        /// <param name="datedokumenti">data e dokumentit</param>
        /// <param name="dateregjistrimi"> data e regjistrimit</param>
        /// <param name="idklientfurnitor"> id e klient furnitorit</param>
        /// <param name="idkoka"> id ritese</param>
        /// <param name="idgjenerues">id e dokumentit qe gjeneron lidhjen (psh id e dok te bankes) nga nje ambjent tjeter</param>
        /// <param name="nrlidhje">nr i lidhjes</param>
        /// <param name="iddoknga">id e dokumentit nga gjenerohet ne rastet e modifikimit</param>
        /// <param name="idkonfigambjente">id e konfigurimit te ambjentit</param>
        /// <param name="idkonfiggjenerues">id e konfigurimit te dokumentit qe e gjeneroi</param>
        /// <param name="idllojdok">id e llojit te dokumenti</param>
        /// <param name="idndermarje">id e ndermarjes</param>
        /// <param name="idnderviti">id e ndermarje vitit</param>
        /// <param name="idnivel">id e nivelit</param>
        /// <param name="idnivelgjenerues">id e nivelit te dokumentit qe e gjeneroi</param>
        /// <param name="idstatusdok">id e gjendjes se dokumenti ruajtur, fshire ejt.</param>
        /// <param name="idperdorues">id e perdoruesit</param>
        public clsDokumentLidhesKoka(int idkoka, String nrlidhje, DateTime datedokumenti, DateTime dateregjistrimi, int idklientfurnitor, int idgjenerues, int idllojdok, int idndermarje, int idnderviti, int idnivel, int idkonfigambjente, int iddoknga, int idnivelgjenerues, int idkonfiggjenerues, int idstatusdok, int idperdorues)
        {
            idKoka = idkoka;
            nrLidhje = nrlidhje;
            dateDokumenti = datedokumenti;
            dateRegjistrimi = dateregjistrimi;
            idKlientFurnitor = idklientfurnitor;
            idGjenerues = idgjenerues;
            idLlojDok = idllojdok;
            idNdermarje = idndermarje;
            idNderViti = idnderviti;
            idNivel = idnivel;
            idKonfigAmbjente = idkonfigambjente;
            idDokNga = iddoknga;
            idNivelGjenerues = idnivelgjenerues;
            idKonfigGjenerues = idkonfiggjenerues;
            idStatusDok = idstatusdok;
            idPerdorues = idperdorues;

            OColTrupi = new colDokumentLidhesTrupi();
        }



        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsDokumentLidhesKoka()
        {
        }

        public clsDokumentLidhesKoka(DataRow rreshti)
        {

            mbushDokumentLidhesKoka(rreshti, false);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht
        /// </summary>
        public int IdKoka
        {
            get { return idKoka; }
            set { idKoka = value; }
        }

        /// <summary>
        /// Kthen/Vendos numrin e dokumentit te lidhjes
        /// </summary>
        public String NrLidhje
        {
            get { return nrLidhje; }
            set { nrLidhje = value; }
        }

        /// <summary>
        /// Kthen/Vendos daten e dokumentit te lidhjes
        /// </summary>
        public DateTime DateDokumenti
        {
            get { return dateDokumenti; }
            set { dateDokumenti = value; }
        }

        /// <summary>
        /// Kthen/Vendos daten e regjistrimit te dokumentit te lidhjes
        /// </summary>
        public DateTime DateRegjistrimi
        {
            get { return dateRegjistrimi; }
            set { dateRegjistrimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e klientit/furnitorit te perbashket te dokumentit kryesor me ate lidhes
        /// </summary>
        public int IdKlientFurnitor
        {
            get { return idKlientFurnitor; }
            set { idKlientFurnitor = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne dokumentit qe gjeneron lidhjen
        /// </summary>
        public int IdGjenerues
        {
            get { return idGjenerues; }
            set { idGjenerues = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne llojit te dokumentit
        /// </summary>
        public int IdLlojDok
        {
            get { return idLlojDok; }
            set { idLlojDok = value; }
        }

        /// <summary>
        /// Kthen/vendos id e ndermarjes
        /// </summary>
        public int IdNdermarje
        {
            get { return idNdermarje; }
            set { idNdermarje = value; }
        }

        /// <summary>
        /// kthen/vendos id e ndermarje vitit
        /// </summary>
        public int IdNderViti
        {
            get { return idNderViti; }
            set { idNderViti = value; }
        }

        /// <summary>
        /// kthen/vendos id e nivelit
        /// </summary>
        public int IdNivel
        {
            get { return idNivel; }
            set { idNivel = value; }
        }

        /// <summary>
        /// kthen/vendos id e konfigurimit
        /// </summary>
        public int IdKonfigAmbjente
        {
            get { return idKonfigAmbjente; }
            set { idKonfigAmbjente = value; }
        }

        /// <summary>
        /// kthen/vendos id e dokumentit qe e gjeneron ne rastet e modifikimit
        /// </summary>
        public int IdDokNga
        {
            get { return idDokNga; }
            set { idDokNga = value; }
        }

        /// <summary>
        /// kthen/vendos id e nivelit te dokumentit qe e gjeneroi
        /// </summary>
        public int IdNivelGjenerues
        {
            get { return idNivelGjenerues; }
            set { idNivelGjenerues = value; }
        }

        /// <summary>
        /// kthen/vendos id e konfigurimi i dokumentit qe e gjeneroi
        /// </summary>
        public int IdKonfigGjenerues
        {
            get { return idKonfigGjenerues; }
            set { idKonfigGjenerues = value; }
        }

        /// <summary>
        /// kthen/vendos id e gjendes se dokumentit
        /// </summary>
        public int IdStatusDok
        {
            get { return idStatusDok; }
            set { idStatusDok = value; }
        }

        /// <summary>
        /// kthen/vendos id e perdoruesit
        /// </summary>
        public int IdPerdorues
        {
            get { return idPerdorues; }
            set { idPerdorues = value; }
        }

        /// <summary>
        /// Kthen/Vendos nje  flete kontabel te gjeneruar nga dokumenti kur kontabilizohet.
        /// </summary>
        public clsKokaFleteKontabel OFleteKontabel
        {
            get { return oFleteKontabel; }
            set { oFleteKontabel = value; }
        }

        /// <summary>
        /// Kthen/Vendos nje  clsGjendjeKlientFurnitor
        /// </summary>
        public colGjendjeKlientFurnitor OGjendjeKF
        {
            get { return oGjendjeKF; }
            set { oGjendjeKF = value; }
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

        #region Metoda Publike
        public clsMesazh krijoDokumentLidhes(String nrlidhje, DateTime datedokumenti, DateTime dateregjistrimi, int idklientfurnitor, int idgjenerues, int idllojdok, int idndermarje, int idnderviti, int idnivel, int idkonfigambjente, int iddoknga, int idnivelgjenerues, int idkonfiggjenerues, int idstatusdok, colDokumentLidhesTrupi trupi, int idperdoruesi, colDokumentat doklidhes, colDokumentat dokkryesor, int idperiudha, double totali, bool mekontabilizim, out string shfaqmesazhapolupe, DbQendraKosto.colTrupiQendraKosto trupivjeterqendra, int idGjuha, ResourceManager rm, CultureInfo ci)
        {
            shfaqmesazhapolupe = "jo";
            clsMesazh mesazh = krijoDokumentLidhesKoka(nrlidhje, datedokumenti, dateregjistrimi, idklientfurnitor, idgjenerues, idllojdok, idndermarje, idnderviti, idnivel, idkonfigambjente, iddoknga, idnivelgjenerues, idkonfiggjenerues, idstatusdok, trupi, idperdoruesi);
            string pershkrimi = "Nga lidhja e dokumentave";
            const int idllojdokfk = 68;
            const int kategoria = 10;
            List<string> rreshtakf;
            List<int> emratkf;
            colTrupatFletetKontabel trupatperGjendjekf;
            OFleteKontabel = new clsKokaFleteKontabel();
            oGjendjeKF = new colGjendjeKlientFurnitor();
            bool njihFitimNgaDifKursi = clsAlternativaKushti.getAlternativa(idkonfigambjente, "NFHNDK") == "Po";
            if (mekontabilizim && njihFitimNgaDifKursi)
            {
                DbQendraKosto.colObjektivaKosto objektivat;
                List<double> vleratobjektiva; List<double> vleratobjektivamonbaze;
                List<int> idllogobj;
                try
                {
                    oFleteKontabel = clsKokaFleteKontabel.gjeneroKontabilizimLidhjeDok(idKoka, idnivel, idkonfigambjente, datedokumenti, nrlidhje, idndermarje, idnderviti, idperdoruesi, dateregjistrimi, doklidhes, pershkrimi, 0, idllojdokfk, idperiudha, kategoria, out emratkf, out rreshtakf, out trupatperGjendjekf, idklientfurnitor, dokkryesor, totali, out objektivat, out vleratobjektiva, out vleratobjektivamonbaze, out idllogobj, 0, 0, 0, out shfaqmesazhapolupe, trupivjeterqendra, idGjuha);
                    oGjendjeKF = gjeneroGjendjeKf(idNivel, nrlidhje, datedokumenti, dateregjistrimi, emratkf, rreshtakf, trupatperGjendjekf);
                }
                catch (Exception ex)
                {
                    return new clsMesazh(false, ex.Message);
                }
            }
            return mesazh;
        }

        private static colGjendjeKlientFurnitor gjeneroGjendjeKf(int idniveli, string nrdok, DateTime data, DateTime dtregj, List<int> emrakf, List<string> rreshtakf, colTrupatFletetKontabel trupiPerGjendjeKF)
        {
            return colGjendjeKlientFurnitor.KrijoGjendjetKlientFurnitor(emrakf, idniveli, nrdok, data, dtregj, trupiPerGjendjeKF, rreshtakf);
        }
        public clsMesazh krijoDokumentLidhesKoka(String nrlidhje, DateTime datedokumenti, DateTime dateregjistrimi, int idklientfurnitor, int idgjenerues, int idllojdok, int idndermarje, int idnderviti, int idnivel, int idkonfigambjente, int iddoknga, int idnivelgjenerues, int idkonfiggjenerues, int idstatusdok, colDokumentLidhesTrupi trupi, int idperdorues)
        {
            nrLidhje = nrlidhje;
            dateDokumenti = datedokumenti;
            dateRegjistrimi = dateregjistrimi;
            idKlientFurnitor = idklientfurnitor;
            idGjenerues = idgjenerues;
            idLlojDok = idllojdok;
            idNdermarje = idndermarje;
            idNderViti = idnderviti;
            idNivel = idnivel;
            idKonfigAmbjente = idkonfigambjente;
            idDokNga = iddoknga;
            idNivelGjenerues = idnivelgjenerues;
            idKonfigGjenerues = idkonfiggjenerues;
            idStatusDok = idstatusdok;
            OColTrupi = trupi;
            idPerdorues = idperdorues;

            return new clsMesazh(true, "Dokumenti lidhes u krijua me sukses!");
        }


        public static clsMesazh LidhArketimeMeFatura(clsKokaShitje koka, JavaScriptSerializer serializusi, ResourceManager rm, CultureInfo ci, int idperdoruesi, int idperiudha, int idgjuha, out string shfaqmesazhapolupe, clsKonfigurimAmbjenti konfdl, bool meKontabilizim, int idnrautonrdok)
        {
            shfaqmesazhapolupe = "";
            try
            {
                colDokumentat dokLidhes = new colDokumentat();
                dokLidhes.mbushDokumentaNgaDokumentaBanka(koka.IdShitjeKoka);
                if (dokLidhes.Count == 0)
                    return new clsMesazh(false, "Fatura nuk ploteson kushtet per lidhje");
                double totali = dokLidhes.Sum(x => x.Vlefta * x.Kursi);
                colDokumentat dokKryesore = new colDokumentat();
                clsDokumenti dokshitje = new clsDokumenti(koka.IdNivel, koka.NrDok, koka.DtDok, koka.TotaliMeZbritjeMeTVSH, totali / koka.Kursi, koka.Kursi, koka.IdMonedha, koka.IdKlientFurnitor, koka.IdShitjeKoka);
                dokKryesore.Add(dokshitje);
                clsDokumentLidhesKoka dokumentiKoka = new clsDokumentLidhesKoka();

                Dictionary<string, object> hidden = new Dictionary<string, object>();
                List<NrAuto> list = new List<NrAuto>();

                string nrdoklidhes = clsNrAutom.merrVlerenNrAutomatik(idnrautonrdok, koka.DtDok);
                if (!String.IsNullOrEmpty(nrdoklidhes)) //nqs ka nr automatik
                {
                    NrAuto nrdokshi = new NrAuto();
                    nrdokshi.kodKontrolli = "NrLidhje";
                    nrdokshi.idNrAuto = idnrautonrdok;
                    nrdokshi.vlereNrAuto = nrdoklidhes;

                    list.Add(nrdokshi);
                    nrdoklidhes = nrdokshi.vlereNrAuto;
                    hidden.Add("NrLidhje", serializusi.Serialize(nrdokshi));
                }

                else nrdoklidhes = koka.NrDok;//nqs nuk ka nr automatik merr nr e urdherit
                clsMesazh mesazh = dokumentiKoka.krijoDokumentLidhes(nrdoklidhes, DateTime.Today, DateTime.Today, koka.IdKlientFurnitor, 0, 10, koka.IdNdermarrje, koka.IdNdermarrjeVit, konfdl.IdNivel, konfdl.IdKonfigAmbjente, 0, 0, 0, 1, colDokumentLidhesTrupi.krijoTrup(dokLidhes, dokKryesore), idperdoruesi, dokLidhes, dokKryesore, idperiudha, totali, meKontabilizim, out shfaqmesazhapolupe, new DbCore.DbQendraKosto.colTrupiQendraKosto(), idgjuha, rm, ci);
                if (!mesazh.Status)
                    return mesazh;
                return dokumentiKoka.ruaj(meKontabilizim, hidden, idperiudha);
            }
            catch (Exception ex)
            {
                return new clsMesazh(false, ex.Message);
            }
        }
        /// <summary>
        /// Ruan nje objekt koka lidhje dokumenti  sebashku me trupin  
        /// Nje objekt koka lidhje dokumenti ka nje koleksion me trupin e dokumentit  
        /// ruajtja e nje lidhje dokumenti imponon ruajtjen edhe te nje colection-i me trupin 
        /// Mqs cdo rresht i ri qe shtohet ne DB kerkon thirrjen e nje SP-je me parametra dhe koka e lidhjes se  dokumentit bashke me trupin  konsiderohet si nje regjistrim,
        /// perdoret nje transaksion qe imponon regjistrimin e rregullt te nje te nje lidhje dokumenti  sebashku me trupin
        /// </summary>
        /// <param name="datedokumenti">data e dokumentit</param>
        /// <param name="dateregjistrimi"> data e regjistrimit</param>
        /// <param name="idklientfurnitor"> id e klient furnitorit</param>
        /// <param name="idkoka"> id ritese</param>
        /// <param name="idgjenerues">id e dokumentit qe gjeneron lidhjen (psh id e dok te bankes) nga nje ambjent tjeter</param>
        /// <param name="nrlidhje">nr i lidhjes</param>
        /// <param name="iddoknga">id e dokumentit nga gjenerohet ne rastet e modifikimit</param>
        /// <param name="idkonfigambjente">id e konfigurimit te ambjentit</param>
        /// <param name="idkonfiggjenerues">id e konfigurimit te dokumentit qe e gjeneroi</param>
        /// <param name="idllojdok">id e llojit te dokumenti</param>
        /// <param name="idndermarje">id e ndermarjes</param>
        /// <param name="idnderviti">id e ndermarje vitit</param>
        /// <param name="idnivel">id e nivelit</param>
        /// <param name="idnivelgjenerues">id e nivelit te dokumentit qe e gjeneroi</param>
        /// <param name="idstatusdok">id e gjendjes se dokumenti ruajtur, fshire ejt.</param>
        /// <param name="OColTrupi"> koleksion i trupit</param>
        /// <param name="oFleteKontabel"> colection i fleteve kontabel</param>
        /// <param name="oGjendjeKF"> colection i gjendjes klient/furnitore</param>
        /// <param name="meKontabilizim"> nese do ruhet me kontabilizim apo jo</param>
        /// <returns> kthen nje obj clsMesazh per te identifikuar statusin e ruajtjes se te dhenave ne DB</returns>
        public clsMesazh ruajLidhjeDokumentash(out int idkoka, String nrlidhje, DateTime datedokumenti, DateTime dateregjistrimi, int idklientfurnitor, int idgjenerues, int idllojdok, int idndermarje, int idnderviti, int idnivel, int idkonfigambjente, int iddoknga, int idnivelgjenerues, int idkonfiggjenerues, int idstatusdok, int idperdorues, colDokumentLidhesTrupi OColTrupi, clsKokaFleteKontabel oFleteKontabel, colGjendjeKlientFurnitor oGjendjeKF, Boolean meKontabilizim, DbData dbData)
        {
            idkoka = 0;
            clsMesazh mesazh;
            clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim(dbData);

            mesazh = dbRegj.ruajDokumentLidhesKoka(out idkoka, nrlidhje, datedokumenti, dateregjistrimi, idklientfurnitor, idgjenerues, idllojdok, idndermarje, idnderviti, idnivel, idkonfigambjente, iddoknga, idnivelgjenerues, idkonfiggjenerues, idstatusdok, idperdorues);
            if (!mesazh.Status)
            {
                return mesazh;
            }
            foreach (clsDokumentLidhesTrupi o in OColTrupi)
            {
                o.IdKoka = idkoka;
                int id;
                mesazh = dbRegj.ruajDokumentLidhesTrupi(out id, o.IdKoka, o.IdDokumenti, o.LlojDokumenti, o.Statusi, o.VleraLidhjes);
                o.IdTrupi = id;
                if (!mesazh.Status)
                {
                    return mesazh;
                }
            }
            if (oGjendjeKF != null)
            {
                foreach (clsGjendjeKlientFurnitor gj in oGjendjeKF)
                {
                    gj.NrDok = nrlidhje;
                    int id;
                    mesazh = dbRegj.ruajGjendjeKF(out id, idkoka, gj.NrDok, gj.DateDok, gj.VlMinus, gj.VlPlus, gj.NivelDok, gj.IdMonedhaDok, gj.KursiDok, gj.DateRegj, gj.VlMinusMonedheBaze, gj.VlPlusMonedheBaze, gj.IdKlientGjendjeKf);
                    gj.IdGjendjeKf = id;
                    if (!mesazh.Status)
                    {
                        return mesazh;
                    }
                }
            }
            if (meKontabilizim && oFleteKontabel != null && oFleteKontabel.VleftaFleteKontabel != 0)
            {
                oFleteKontabel.NrDukumentiKokaFleteKontabel = nrlidhje;
                oFleteKontabel.IdGjenerues = idkoka;
                mesazh = oFleteKontabel.Ruaj(new clsDatabaseKontabilitet(dbRegj));
                if (!mesazh.Status)
                {
                    return mesazh;
                }
            }
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }

        /// <summary>
        /// Ruan lidhjen e dokumentave ne tabelen perkatese ne databaze.Therret funksionin
        /// </summary>
        /// <returns>Kthen true nese ruajtja perfundoi me sukses</returns>

        public clsMesazh ruaj(Boolean meKontabilizim, IDictionary<string, object> hfNrAutoKF, int idperdorues)
        {
            bool kaNdryshimNumri;
            clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();
            db.beginTransaksion();
            clsMesazh mesazhKontrolli = kontrolloLidhje(out kaNdryshimNumri, db, hfNrAutoKF, false, idperdorues);

            if (!mesazhKontrolli.Status)
            {
                return mesazhKontrolli;
            }
            int idkoka = 0;
            clsMesazh u_ruajt = ruajLidhjeDokumentash(out idkoka, this.NrLidhje, this.DateDokumenti, this.DateRegjistrimi, this.IdKlientFurnitor, this.IdGjenerues,
                this.IdLlojDok, this.IdNdermarje, this.IdNderViti, this.IdNivel, this.IdKonfigAmbjente, this.IdDokNga, this.IdNivelGjenerues, this.IdKonfigGjenerues,
                this.IdStatusDok, this.IdPerdorues, this.OColTrupi, this.OFleteKontabel, this.OGjendjeKF, meKontabilizim, db);
            this.IdKoka = idkoka;
            if (!u_ruajt.Status)
            {
                db.rollbackTransaksion();
                return u_ruajt;
            }
            db.commitTransaksion();

            if (kaNdryshimNumri)
                return mesazhKontrolli;
            return u_ruajt;

        }
        private clsMesazh kontrolloLidhje(out bool kaNdryshimNrAuto, clsDatabaseRegjistrim db, IDictionary<string, object> hfNrAutoKF, bool modifikim, int idperdorues)
        {
            kaNdryshimNrAuto = false;
            if (nrLidhje == "")
                return new clsMesazh(false, "Nr i dokumentit nuk mund te jete bosh");
            if (!modifikim)
            {
                clsMesazh mes = new clsMesazh();
                if (hfNrAutoKF != null)
                {
                    mes = kontrolloNrAutoKF(out kaNdryshimNrAuto, db, hfNrAutoKF, idperdorues);
                    if (!mes.Status)
                        return mes;
                }

                if (db.ekzistonRegjistrimDokuemtiLidhes(idKonfigAmbjente, nrLidhje, dateDokumenti, idNdermarje, idKlientFurnitor))
                    return new clsMesazh(false, "Ekziston nje dokument me kete nr!");
                return new clsMesazh(mes.Status, mes.PershkrimMesazhi);
            }
            return new clsMesazh(true, "Kontrollet u kaluan me sukses");

        }
        private clsMesazh kontrolloNrAutoKF(out bool kaNdryshimNumri, clsDatabaseRegjistrim db, IDictionary<string, object> hfNrAutoKf, int idperdorues)
        {
            clsDatabaseAdmin dbadm = new clsDatabaseAdmin(db);
            List<NrAuto> list = DbAdmin.clsNrAutom.kontrollogjithenumrat(dbadm, hfNrAutoKf, DateTime.Today);
            if (NrAuto.ktheVlerenEre(list, "NrLidhje") != "")
                this.NrLidhje = NrAuto.ktheVlerenEre(list, "NrLidhje");
            DbCore.clsMesazh mes = NrAuto.ruajvlera(out kaNdryshimNumri, list, DateTime.Today, idperdorues, this.idNdermarje, dbadm);
            return new clsMesazh(mes.Status, mes.PershkrimMesazhi);
        }
        public clsMesazh ruaj(Boolean meKontabilizim, DbData dbData)
        {
            int idkoka = 0;
            clsMesazh u_ruajt = ruajLidhjeDokumentash(out idkoka, this.NrLidhje, this.DateDokumenti, this.DateRegjistrimi, this.IdKlientFurnitor, this.IdGjenerues, this.IdLlojDok, this.IdNdermarje, this.IdNderViti, this.IdNivel, this.IdKonfigAmbjente, this.IdDokNga, this.IdNivelGjenerues, this.IdKonfigGjenerues, this.IdStatusDok, this.IdPerdorues, this.OColTrupi, this.OFleteKontabel, this.OGjendjeKF, meKontabilizim, dbData);
            this.IdKoka = idkoka;
            return u_ruajt;
        }
        /// <summary>
        /// Fshin lidhjen e dokumentave ne tabelen perkatese ne databaze.Therret funksionin
        /// <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.fshiLidhjeDokumentash"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh fshi(ResourceManager rm, CultureInfo ci)
        {
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            //data.krijoManager();
            data.beginTransaksion();

            clsMesazh u_fshi = fshiDokumentDheKontabilitet(data);
            if (u_fshi.Status)
                data.commitTransaksion();
            else
                data.rollbackTransaksion();
            return u_fshi;
        }
        public clsMesazh fshiDokumentDheKontabilitet(clsDatabaseRegjistrim data)
        {
            clsMesazh u_fshi = data.modifikoDokumentLidhesKoka(this.IdKoka, this.nrLidhje, this.dateDokumenti, this.dateRegjistrimi, this.IdKlientFurnitor, this.idGjenerues, this.idLlojDok, this.idNdermarje, this.idNderViti, this.idNivel, this.idKonfigAmbjente, this.idDokNga, this.idNivelGjenerues, this.idKonfigGjenerues, 2, idPerdorues);
            if (u_fshi.Status)
            {
                clsDatabaseKontabilitet dbkontab = new clsDatabaseKontabilitet(data);
                DbCore.clsMesazh mesazhKont = new DbCore.clsMesazh(true);
                clsKokaFleteKontabel newclsKokaFleteKontabel = new clsKokaFleteKontabel(this.IdKoka, 10, dbkontab);
                if (newclsKokaFleteKontabel.NrDukumentiKokaFleteKontabel != null)
                {
                    this.OFleteKontabel = newclsKokaFleteKontabel;
                    DbQendraKosto.clsKokaQendraKosto kokaqendra = new DbQendraKosto.clsKokaQendraKosto();
                    DbQendraKosto.clsDatabaseQendraKosto dbqendra = new DbQendraKosto.clsDatabaseQendraKosto(dbkontab);
                    kokaqendra.KtheKokaQKSipasIDGjeneruesDheKonfig(newclsKokaFleteKontabel.IdKokaFleteKontabel, newclsKokaFleteKontabel.IdKonfigAmbjente, dbqendra);
                    if (kokaqendra.IdKoka != 0 && kokaqendra.IdKoka != -1)
                    {
                        this.OFleteKontabel.KokaQendraKosto = kokaqendra;
                    }
                    else this.OFleteKontabel.KokaQendraKosto = new DbQendraKosto.clsKokaQendraKosto();

                    if (OFleteKontabel.IdKokaFleteKontabel != 0)
                        mesazhKont = OFleteKontabel.ModifikoFleteKontabel(true, dbkontab);
                }
                u_fshi.Status = mesazhKont.Status;
                if (u_fshi.Status)
                {
                    this.OGjendjeKF = new colGjendjeKlientFurnitor(this.IdKoka, this.IdNivel, data);
                    foreach (clsGjendjeKlientFurnitor gj in this.OGjendjeKF)
                    {
                        if (gj.IdGjendjeKf != 0)
                        {
                            gj.IdStatusGjendjeKf = 2; //rasti kur ndrysheohet statusi per treguar qe dokumenti eshte i modifikuar dhe nuk duhet marre parasysh
                            u_fshi = gj.Modifiko(data);
                        }
                    }
                }
            }

            return u_fshi;
        }
        public clsGjendjeKlientFurnitor krijoObjektGjendjeKF()
        {
            clsGjendjeKlientFurnitor gjendje = new clsGjendjeKlientFurnitor();
            gjendje.DateDok = this.DateDokumenti;
            gjendje.DateRegj = this.DateRegjistrimi;
            gjendje.IdDok = 0;
            gjendje.IdGjendjeKf = 0;
            gjendje.IdKlientGjendjeKf = this.IdKlientFurnitor;
            //do shtohet me vone ,pasi te shtohet id e ndermarrjes
            gjendje.IdMonedhaDok = -1;
            gjendje.KursiDok = 1;
            gjendje.NivelDok = -1;
            gjendje.NrDok = this.NrLidhje;
            clsDokumentLidhesTrupi dokumentKryesor = this.OColTrupi.ktheDokumentKryesor();
            clsKokaShitje dokument = new clsKokaShitje();
            ////dokument.IdShitjeKoka = dokumentKryesor.IdDokumenti;
            ////dokument = dokument.merrSipasId()[0];
            clsKlientFurnitor klient = new clsKlientFurnitor(IdKlientFurnitor);
            IEnumerable<clsTrupiFleteKontabel> tempColTrupiFleteKontabel;
            tempColTrupiFleteKontabel = from l in this.OFleteKontabel.OColTrupi
                                        where l.IdLlogari == klient.IdLlogari
                                        select l;
            clsTrupiFleteKontabel tempTrupiFleteKontabel = tempColTrupiFleteKontabel.First<clsTrupiFleteKontabel>();
            if (tempTrupiFleteKontabel.DK == "D")
            {
                if (klient.LlojiKF)
                {
                    gjendje.VlPlusMonedheBaze = tempTrupiFleteKontabel.VleftaDebiMonBazeTrupiFleteKontabel;
                    gjendje.VlPlus = 0;
                    gjendje.VlMinus = 0;
                    gjendje.VlMinusMonedheBaze = 0;
                }
                else
                {
                    gjendje.VlPlusMonedheBaze = 0;
                    gjendje.VlPlus = 0;
                    gjendje.VlMinus = 0;
                    gjendje.VlMinusMonedheBaze = tempTrupiFleteKontabel.VleftaDebiMonBazeTrupiFleteKontabel;
                }
            }
            return gjendje;
        }
        /// <summary>
        /// Merr objektin e  kokes se dokumentin lidhes sipas id nga tabela perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.ktheKokaDokumentiLidhesSipasId"/> 
        /// </summary>
        /// <returns > nje objekt clsKokaMagazina qe permban objektin e kerkuar</returns>
        public bool merrSipasId()
        {

            clsDatabaseRegjistrim dregj = new clsDatabaseRegjistrim();
            bool sukses = mbushDokumentLidhesKoka(dregj.ktheKokaDokumentiLidhesSipasId(this.IdKoka), false);
            dregj.Dispose();
            return sukses;
        }
        public bool merrSipasId(bool eshteFshirje)
        {

            clsDatabaseRegjistrim dregj = new clsDatabaseRegjistrim();
            bool sukses = mbushDokumentLidhesKoka(dregj.ktheKokaDokumentiLidhesSipasId(this.IdKoka), eshteFshirje);
            dregj.Dispose();
            return sukses;
        }
        public bool ekzistonDokLidhes(int idkonfigambjente, string nrlidhje, DateTime data, int idndermarja, int idklientfurnitor)
        {

            clsDatabaseRegjistrim dregj = new clsDatabaseRegjistrim();
            bool sukses = dregj.ekzistonRegjistrimDokuemtiLidhes(idkonfigambjente, nrlidhje, data, idndermarja, idklientfurnitor);
            dregj.Dispose();
            return sukses;
        }
        /// <summary>
        /// Merr objektin e  kokes se dokumentit lidhes sipas nr dhe dt dokumenti nga tabela perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.ktheKokaDokumentiLidhesSipasIdNivelNrDokDtDok"/> 
        /// </summary>
        /// <returns > nje objekt clsDokumentLidhesKoka qe permban objektin e kerkuar</returns>
        public clsDokumentLidhesKoka merrSipasIdNivelNrDokDtDok()
        {
            clsDokumentLidhesKoka data = new clsDokumentLidhesKoka();
            clsDatabaseRegjistrim dregj = new clsDatabaseRegjistrim();
            data.mbushDokumentLidhesKoka(dregj.ktheKokaDokumentiLidhesSipasIdNivelNrDokDtDok(this.IdNivel, this.NrLidhje, this.DateDokumenti, this.idKlientFurnitor), false);
            dregj.Dispose();
            return data;
        }
        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush koken e dokumentit lidhes nga databaza
        /// </summary>
        /// <param name="dbDataRowDokumentLidhesKoka">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushDokumentLidhesKoka(DataRow dbDataRowDokumentLidhesKoka, bool eshteFshirje)
        {
            if (dbDataRowDokumentLidhesKoka != null)
            {
                try
                {
                    int.TryParse(dbDataRowDokumentLidhesKoka["IDKOKA"].ToString(), out idKoka);
                    nrLidhje = dbDataRowDokumentLidhesKoka["NRLIDHJE"].ToString();
                    DateTime.TryParse(dbDataRowDokumentLidhesKoka["DATEDOKUMENTI"].ToString(), out dateDokumenti);
                    DateTime.TryParse(dbDataRowDokumentLidhesKoka["DATEREGJISTRIMI"].ToString(), out dateRegjistrimi);
                    int.TryParse(dbDataRowDokumentLidhesKoka["IDKLIENTFURNITOR"].ToString(), out idKlientFurnitor);
                    int.TryParse(dbDataRowDokumentLidhesKoka["IDGJENERUES"].ToString(), out idGjenerues);
                    int.TryParse(dbDataRowDokumentLidhesKoka["IDLLOJDOK"].ToString(), out idLlojDok);
                    int.TryParse(dbDataRowDokumentLidhesKoka["IDNDERMARJE"].ToString(), out idNdermarje);
                    int.TryParse(dbDataRowDokumentLidhesKoka["IDNDERVITI"].ToString(), out idNderViti);
                    int.TryParse(dbDataRowDokumentLidhesKoka["IDNIVEL"].ToString(), out idNivel);
                    int.TryParse(dbDataRowDokumentLidhesKoka["IDKONFIGAMBJENTE"].ToString(), out idKonfigAmbjente);
                    int.TryParse(dbDataRowDokumentLidhesKoka["IDDOKNGA"].ToString(), out idDokNga);
                    int.TryParse(dbDataRowDokumentLidhesKoka["IDNIVELGJENERUES"].ToString(), out idNivelGjenerues);
                    int.TryParse(dbDataRowDokumentLidhesKoka["IDKONFIGGJENERUES"].ToString(), out idKonfigGjenerues);
                    int.TryParse(dbDataRowDokumentLidhesKoka["IDSTATUSDOK"].ToString(), out idStatusDok);
                    DateTime.TryParse(dbDataRowDokumentLidhesKoka["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRowDokumentLidhesKoka["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    if (!eshteFshirje)
                        int.TryParse(dbDataRowDokumentLidhesKoka["IDPERDORUESI"].ToString(), out idPerdorues);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se kokes se dokumentit lidhes nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}

