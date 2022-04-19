using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DbCore.DbAdmin;
using DbCore.DbKontabiliteti;
using System.Data;
using System.Threading;
using System.Collections;
using DbCore.DbShare;
using DbCore.IMBUtils.Logging;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne  koken e nje dokumenti te rezervimit
    ///  (Te dhenat  merren nga tabela : T_KOKAREZERVIME)
    /// </summary>

    public class clsKokaRezervime
    {
        #region Attributet
        private int idKokaRezervime;
        private int idNivel;
        private int idKonfigAmbjente;
        private int idKlientFurnitor;
        private int idMagazina;
        private int idDokNga;
        private String nrDok;
        private DateTime dtDok;
        private DateTime dtRegjistrimi;
        private int idStatusDok;
        private int idNdermarrje;
        private int idNdermarrjeVit;
        private int idKrijuesi;
        private int idPerdoruesi;
        private string shenime;
        private int idNivelGjenerues;
        private int idKonfigGjenerues;
        private int idGjenerues;
        private int idDegeAdministrative;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private colTrupiRezervime ocolTrupiRezervime;
        private clsKokaRezervime oRezervimDalje;
        private string kodKlientFurnitori;
        private string kodMagazina;
        private string kodDegeAdministrative;
        private int prioriteti;
        private int idLloji;
        private int statusi;
        private int idlidhje;
        private DataRow rreshti;


        #endregion

        #region Properties
        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdKokaRezervimi
        {
            get { return idKokaRezervime; }
            set { idKokaRezervime = value; }
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
        /// Kthen/Vendos ID-ne e magazines
        /// </summary>
        public int IdMagazina
        {
            get { return idMagazina; }
            set { idMagazina = value; }
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
        public int IdKrijuesi
        {
            get { return idKrijuesi; }
            set { idKrijuesi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne  e perdoruesit qe e ka modifikuar.
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
            get { return dtRegjistrimi; }
            set { dtRegjistrimi = value; }
        }


        /// <summary>
        /// Kthen/Vendos ID-ne e nivelit te dokumentit nga eshte gjeneruar magazina
        /// </summary>

        public int IdNivelGjenerues
        {
            get { return idNivelGjenerues; }
            set { idNivelGjenerues = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e konfigurimit te dokumentit nga eshte gjeneruar magazina
        /// </summary>

        public int IdKonfigGjenerues
        {
            get { return idKonfigGjenerues; }
            set { idKonfigGjenerues = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e dokumentit nga eshte gjeneruar magazina nga nje ambjent tjeter
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
        /// Kthen/Vendos  nje koleksion me trupin e dokumentit te rezervimeve
        /// </summary>
        public colTrupiRezervime OcolTrupiRezervime
        {
            get { return ocolTrupiRezervime; }
            set { ocolTrupiRezervime = value; }
        }


        public DateTime DtKrijimi
        {
            get { return dtKrijimi; }

        }
        public DateTime DtModifikimi
        {
            get { return dtModifikimi; }

        }

        public int Prioriteti
        {
            get { return prioriteti; }
            set { prioriteti = value; }
        }

        //hyrje apo dalje
        public int Lloji
        {
            get { return idLloji; }
            set { idLloji = value; }
        }


        public int Statusi
        {
            get { return statusi; }
            set { statusi = value; }
        }

        public int IdLidhje
        {
            get { return idlidhje; }
            set { idlidhje = value; }
        }

        public clsKokaRezervime ORezervimDalje
        {
            get { return oRezervimDalje; }
            set { oRezervimDalje = value; }
        }
        #endregion

        #region Konstruktoret
        /// <summary>
        /// Konstruktori i klases me parametra
        /// </summary>
        /// <param name="dtDk"> data e dokumentit te rezervimit</param>
        /// <param name="dtRegj"> data e regjistrimit te dokumentit te rezervimit</param>
        /// <param name="idKDok"> id kategorise se dokumentit me te cilin lidhet.</param>
        /// <param name="idKlFurn"> id e klient furnitorit me te cilin lidhet</param>
        /// <param name="idKonf"> id e konfigurimit te dokumentit</param>
        /// <param name="idLidhes">id e dokumentit nga e cila gjenerohet ne rastet e modifikimit </param>
        /// <param name="idMag">id e magazines</param>
        /// <param name="idKoka">id ritese e kokes se rezervimit</param>
        /// <param name="idNder"> id e ndermarjes</param>
        /// <param name="idNdVt"> id e ndermarje vitit</param>
        /// <param name="idNiv"> id e nivelit te regjistrimit</param>
        ///  <param name="idKrij"> id e perdoruesit se e ben regjistrimin</param>
        /// <param name="idPer"> id e perdoruesit se e ben modifikimin</param>
        /// <param name="idrenditjes"> id e renditjes per te renditur dokumentat e te njejtes date sipas kohes se regjistrimit</param>
        /// <param name="idSt"> id e statusit te dokumentit</param>
        /// <param name="shenim"> shenime </param>
        /// <param name="idGjenerues">id e dokumentit nga gjenerohet magazina nga nje ambjent tjeter</param>
        /// <param name="idKonfigGjenerues">id e konfigurimit nga gjenerohet dokumenti</param>
        /// <param name="idNivelGjenerues">id e nivelit nga gjenerohet dokumenti</param>  
        ///<param name="idDegeAdministrative"> id e deges admin</param>
        ///<param name="prior">prioriteti i rezervimit</param>


        public clsKokaRezervime(int idKoka, int idNiv, int idKonf, int idKlFurn, int idMag, DateTime dtDk, string nrDk, int idLidhes, int idSt, int idNder, int idNdVt, int idKrij, int idPer, DateTime dtRegj, string shenim, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues, int idDegeAdministrative, int prior, int llojRez, int stat, int idlidh)
        {
            idKokaRezervime = idKoka;
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
            idKrijuesi = idKrij;
            idPerdoruesi = idPer;
            dtRegjistrimi = dtRegj;
            shenime = shenim;
            this.idNivelGjenerues = idNivelGjenerues;
            this.idKonfigGjenerues = idKonfigGjenerues;
            this.idGjenerues = idGjenerues;
            this.idDegeAdministrative = idDegeAdministrative;
            prioriteti = prior;
            statusi = stat;
            idLloji = llojRez;
            idlidhje = idlidh;

        }

        /// <summary>
        /// konstruktor me 3 parametra
        /// </summary>
        /// <param name="idNivel">id e nivelit</param>
        /// <param name="nrdok">nr i dokumentit</param>
        /// <param name="dtdok">data e dokumentit</param>
        public clsKokaRezervime(int idNivel, string nrdok, DateTime dtdok)
        {
            clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();
            mbushKokaRezervime(db.ktheKokaRezervimiSipasIdNivelNrDokDtDok(idNivel, nrdok, dtdok));
            db.Dispose();
        }

        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsKokaRezervime()
        {
            ocolTrupiRezervime = new colTrupiRezervime();
        }
        public clsKokaRezervime(int idKoka)
        {
            mbushKokaRezervimiSipasID(idKoka);
            ocolTrupiRezervime = new colTrupiRezervime();
        }

        public clsKokaRezervime(DataRow rreshti)
        {
            
            mbushKokaRezervime(rreshti);
        }
        #endregion

        #region Metoda Publike

        public clsKokaRezervime ShallowCopy()
        {
            var koka = (clsKokaRezervime) this.MemberwiseClone();
            if (this.OcolTrupiRezervime != null)
                koka.OcolTrupiRezervime = this.OcolTrupiRezervime.ShallowCopy();
            if (this.ORezervimDalje != null)
                koka.ORezervimDalje = this.ORezervimDalje.ShallowCopy();

            return koka;
        }


        public clsMesazh krijoRezervim(int idNiv, int idKonf, int idKlFurn, string kodklientfurnitor, int idMag, string kodmag, DateTime dtDk, string nrDk, int idllojrez, int idSt, int idNder, int idNdVt, int idPer, DateTime dtRegj, string shenim, int idDegeAdministrative, string koddege, int prior, int stat, int idlidh, colTrupiRezervime coltrupi, clsKokaRezervime rd, out string mesazhinformues)
        {
            mesazhinformues = "";
            clsMesazh msg = krijoRezervim(idNiv, idKonf, idKlFurn, kodklientfurnitor, idMag, kodmag, dtDk, nrDk, idSt, idNder, idNdVt, idPer, dtRegj, shenim, idDegeAdministrative, koddege, prior, idllojrez, stat, idlidh, 0, 0, 0, 0, coltrupi, rd);
            if (msg.Status == false) mesazhinformues = msg.PershkrimMesazhi;
            return msg;
        }

        public clsMesazh krijoRezervim(int idNiv, int idKonf, int idKlFurn, string kodklientfurnitor, int idMag, string kodmag, DateTime dtDk, string nrDk, int idSt, int idNder, int idNdVt, int idPer, DateTime dtRegj, string shenim, int idDegeAdministrative, string koddege, int prior, int idllojrez, int stat, int idlidh, int idLidhes, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues, colTrupiRezervime coltrupi, clsKokaRezervime rd)
        {
            idNivel = idNiv;
            idKonfigAmbjente = idKonf;
            idKlientFurnitor = idKlFurn;
            kodKlientFurnitori = kodklientfurnitor;
            idMagazina = idMag;
            kodMagazina = kodmag;
            nrDok = nrDk;
            dtDok = dtDk;
            idDokNga = idLidhes;
            idStatusDok = idSt;
            idNdermarrje = idNder;
            idNdermarrjeVit = idNdVt;
            idPerdoruesi = idPer;
            dtRegjistrimi = dtRegj;
            shenime = shenim;
            this.idNivelGjenerues = idNivelGjenerues;
            this.idKonfigGjenerues = idKonfigGjenerues;
            this.idGjenerues = idGjenerues;
            this.idDegeAdministrative = idDegeAdministrative;
            kodDegeAdministrative = koddege;
            prioriteti = prior;
            statusi = stat;
            idlidhje = idlidh; // id e RH qe lidhet me RD
            idLloji = idllojrez;
            ocolTrupiRezervime = coltrupi;
            oRezervimDalje = rd;
            clsMesazh mesazh = this.kontrollo();
            if (!mesazh.Status)
                return mesazh;
            return new clsMesazh(true, "Rezervimi u krijua me sukses!");
        }
        public void KrijoAnullim()
        {
            this.mbushTrupRezervime();
            int idNivelGjen = clsNivelRegjistrimi.ktheIdNivelRegjistrimiSipasKodi("RA", idNdermarrje);
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasKod("RA", idNdermarrje);

            var anullim = this.ShallowCopy();
            anullim.IdKonfigAmbjente = konf.IdKonfigAmbjente;
            anullim.IdNivel = idNivelGjen;
            anullim.IdStatusDok = 1;
            anullim.Lloji = 2;
            anullim.OcolTrupiRezervime.ForEach(x => x.Shenja = -1);
            this.ORezervimDalje = anullim;
            this.Statusi = 2;
        }

        private clsMesazh kontrollo()
        {
            bool kontrolloGjendje = clsAlternativaKushti.getAlternativa(IdKonfigAmbjente, "KRPGJ") == "Jo";
            double sasiRez = 0;
            double sasitot = 0;
            double sasiDisp = 0;

            clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();

            if (nrDok == "")
                return new clsMesazh(false, "Numri i dokumentit nuk mund të jetë bosh");
            if (dtDok == null || dtDok.ToShortDateString() == "01/01/0100")
                return new clsMesazh(false, "Zgjidhni datën e dokumentit!");
            if (dtRegjistrimi == null || dtRegjistrimi.ToShortDateString() == "01/01/0100")
                return new clsMesazh(false, "Zgjidhni datën e regjistrimit!");

            if (kodMagazina != "" && !clsNjesiAdministrative.ekziston(kodMagazina, idNdermarrje))
                return new clsMesazh(false, "Magazina nuk ekziston!");
            clsNjesiAdministrative mag = new clsNjesiAdministrative(kodMagazina, idNdermarrje, idPerdoruesi);

            if (kodMagazina != "" && !mag.Aktiv)
                return new clsMesazh(false, "Magazina nuk eshte aktive!");
            if (kodDegeAdministrative != "" && !clsDegeAdministrative.ekziston(kodDegeAdministrative, idNdermarrje))
                return new clsMesazh(false, "Dega administrative nuk ekziston!");
            clsDegeAdministrative deg = new clsDegeAdministrative(kodDegeAdministrative, idNdermarrje);
            if (kodDegeAdministrative != "" && !deg.Aktiv)
                return new clsMesazh(false, "Dega administrative nuk është aktive!");

            if (kontrolloGjendje)
            {
                DateTime currentDate = DateTime.Now;
                DateTime data = currentDate.AddYears(2);
                foreach (clsTrupiRezervime t in this.ocolTrupiRezervime)
                {
                    DbCore.DbInventari.clsArtikulli artikulli = (DbCore.DbInventari.clsArtikulli)(t.Element);
                    artikulli.IdArtikulli = t.IdArtikulli;
                    double sasiTrup = 0;
                    if (t.IdMag != 0)
                    {
                        sasiTrup = this.ocolTrupiRezervime.ktheSasiDaljeRezervimi(artikulli.IdArtikulli, t.IdMag);
                        sasitot = clsTrupiMagazina.merrSasi(artikulli, t.IdMag, data, -1, db);
                        sasiRez = clsTrupiRezervime.merrSasiSipasArtikullitDheMagazines(artikulli.IdArtikulli, t.IdTrupiRezervime, t.IdMag, data);
                    }
                    else
                    {
                        sasiTrup = this.ocolTrupiRezervime.ktheSasiDaljeRezervimi(artikulli.IdArtikulli);
                        sasitot = clsTrupiMagazina.merrSasi(artikulli, -1, data, -1, db);
                        sasiRez = clsTrupiRezervime.merrSasiSipasArtikullitDheMagazines(artikulli.IdArtikulli, t.IdTrupiRezervime, 0, data);
                    }

                    sasiDisp = sasitot - sasiRez;
                    if (sasiTrup > sasiDisp) { return new clsMesazh("Per kete artikull: " + artikulli.KodArtikulli + "  nuk keni gjendje! Gjendje disponueshme: " + sasiDisp); }
                }
            }
            return new clsMesazh(true, "Kontrollet  u kaluan me sukses!");
        }

        public bool eshteILidhur()
        {
            using (var dbAdmin = new DbAdmin.clsDatabaseAdmin())
                return dbAdmin.eshteDokumentiILidhur(idKokaRezervime, idNivel, "T_KOKAREZERVIME", "IDKOKAREZERVIME");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable merrIdsDokLidhur()
        {
            DbAdmin.clsDatabaseAdmin dbAdmin = new DbAdmin.clsDatabaseAdmin();
            DataTable dt = dbAdmin.MerrDokLidhur(idKokaRezervime, idNivel, "T_KOKAREZERVIME", "IDKOKAREZERVIME");
            dbAdmin.Dispose();
            return dt;
        }



        public clsMesazh ruajRezervime(out int idKoka, int idNiv, int idKonf, int idKlFurn, int idMag, DateTime dtDk, string nrDk, int idlloj, int idLidhes, int idSt, int idNder, int idNdVt, int idPer, DateTime dtRegj, string shenim, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues, int iddegeadministrative, colTrupiRezervime ocolTrupiRezervime, bool modifikim, int idPeriudha, clsDatabaseRegjistrim dbRegj, int prioriteti, int stat, int lidhje, colTrupiRezervime ocoltrupigjeneruar, int idkrijuesi)
        {
            clsMesazh mesazh;


            idKoka = 0;
            try
            {
                idKoka = dbRegj.ruajKokaRezervimi(idKoka, idNiv, idKonf, idKlFurn, idMag, dtDk, nrDk, idLidhes, idSt, idNder, idNdVt, idPer, dtRegj, shenim, idNivelGjenerues, idKonfigGjenerues, idGjenerues, iddegeadministrative, prioriteti, idlloj, stat, lidhje, idkrijuesi);

                if (idKoka == 0)
                    return new clsMesazh(false, "Ndodhi një Gabim gjatë ruajtjes së Kokës së Rezervimit");
                if (modifikim)
                {
                    mesazh = dbRegj.modifikoKonvertimSipasIdDokKonvertuar(idLidhes, idKoka);
                    if (!mesazh.Status)
                    {
                        return mesazh;
                    }
                }

                foreach (clsTrupiRezervime o in ocolTrupiRezervime)
                {
                    o.IdKokaRezervime = idKoka;
                    o.IdStatusDok = idSt;
                    int idM;
                    mesazh = dbRegj.ruajTrupiRezervime(out idM, o.IdKokaRezervime, o.IdArtikulli, o.IdNjesia, o.Sasia, o.Koeficenti, o.IdMag, o.Data, o.IdStatusDok, o.Shenja, o.IdTrupiNgaVjen, o.IdTrupiHyrje);
                    if (!mesazh.Status)
                    {
                        return mesazh;
                    }
                    if (o.IdTrupiHyrje > 0)
                    {
                        clsTrupiRezervime truphyrje = new clsTrupiRezervime(o.IdTrupiHyrje, dbRegj);
                        mesazh = dbRegj.modifikoTrupiRezervime(truphyrje.IdTrupiRezervime, truphyrje.IdKokaRezervime, o.IdArtikulli, o.IdNjesia, o.Sasia, o.Koeficenti, truphyrje.IdMag, truphyrje.Data, truphyrje.IdStatusDok, truphyrje.Shenja, truphyrje.IdTrupiNgaVjen, truphyrje.IdTrupiHyrje);
                        if (!mesazh.Status)
                        {
                            return mesazh;
                        }
                    }
                    mesazh = dbRegj.modifikoTrupiRezervimeIdHyrje(o.IdTrupiRezervime, idM);
                    if (!mesazh.Status)
                    {
                        return mesazh;
                    }

                    mesazh = dbRegj.modifikoTrupiShitjeIdRezervimi(o.IdTrupiRezervime, idM);
                    if (!mesazh.Status)
                    {
                        return mesazh;
                    }
                    mesazh = dbRegj.modifikoTrupiMagazinaIdRezervimi(o.IdTrupiRezervime, idM);
                    if (!mesazh.Status)
                    {
                        return mesazh;
                    }

                    foreach (clsTrupiRezervime gj in ocoltrupigjeneruar)
                    {
                        if (gj.IdTrupiHyrje == o.IdTrupiRezervime)
                            gj.IdTrupiHyrje = idM;
                    }
                    o.IdTrupiRezervime = idM;
                }
                if (stat != 2)//jo per dokumentat e anulluar
                {// ndryshojme statusin e dokumentit nqs eshte ekzekutuar komplete
                    if (idlloj == 2)// ne dalje kontrollojme hyrjet e tij
                    {
                        ArrayList idte = new ArrayList();
                        foreach (clsTrupiRezervime o in ocolTrupiRezervime)
                        {
                            if (o.IdTrupiHyrje != 0)
                            {
                                clsTrupiRezervime tr = new clsTrupiRezervime(o.IdTrupiHyrje, dbRegj);
                                if (!idte.Contains(tr.IdKokaRezervime))
                                {
                                    idte.Add(tr.IdKokaRezervime);
                                    colTrupiRezervime col = new colTrupiRezervime();
                                    col.mbushGjitheTrupiRezervimiNgaKoka(tr.IdKokaRezervime, dbRegj);
                                    bool ekzekutuar = true;
                                    foreach (clsTrupiRezervime trhyrje in col)
                                    {

                                        double sasiMbetur = (trhyrje.Sasia * trhyrje.Koeficenti - dbRegj.ktheSasineKonvertuarSipasArtikullit(trhyrje.IdArtikulli, trhyrje.IdTrupiRezervime)) / trhyrje.Koeficenti;
                                        if (sasiMbetur != 0)
                                        {
                                            ekzekutuar = false;
                                            break;
                                        }
                                    }
                                    clsKokaRezervime kokarez = new clsKokaRezervime();
                                    kokarez.mbushKokaRezervimiSipasID(tr.IdKokaRezervime, dbRegj);
                                    if (kokarez.statusi != 2)
                                    {
                                        mesazh = dbRegj.modifikoKokaRezervime(kokarez.idKokaRezervime, kokarez.idNivel, kokarez.idKonfigAmbjente, kokarez.idKlientFurnitor, kokarez.idMagazina, kokarez.dtDok, kokarez.nrDok, kokarez.idStatusDok, kokarez.dtRegjistrimi, kokarez.shenime, kokarez.idDegeAdministrative, kokarez.idPerdoruesi, kokarez.prioriteti, ekzekutuar ? 1 : 0, this.IdPerdoruesi);
                                        if (!mesazh.Status)
                                            return mesazh;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {// kontrollojme hyrjen
                        bool ekzekutuar = true;
                        foreach (clsTrupiRezervime o in ocolTrupiRezervime)
                        {
                            double sasiMbetur = (o.Sasia * o.Koeficenti - dbRegj.ktheSasineKonvertuarSipasArtikullit(o.IdArtikulli, o.IdTrupiRezervime)) / o.Koeficenti;
                            if (sasiMbetur != 0)
                            {
                                ekzekutuar = false;
                                break;
                            }
                        }
                        mesazh = dbRegj.modifikoKokaRezervime(idKoka, idNiv, idKonf, idKlFurn, idMag, dtDk, nrDk, idSt, dtRegj, shenim, iddegeadministrative, idPer, prioriteti, ekzekutuar ? 1 : 0, this.IdPerdoruesi);
                        if (!mesazh.Status)
                            return mesazh;
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

        public clsMesazh ruajStatusRezervime(out int idKoka, int idNiv, int idKonf, int idKlFurn, int idMag, DateTime dtDk, string nrDk, int idlloj, int idLidhes, int idSt, int idNder, int idNdVt, int idPer, DateTime dtRegj, string shenim, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues, int iddegeadministrative, colTrupiRezervime ocolTrupiRezervime, bool modifikim, clsDatabaseRegjistrim dbRegj, int prioriteti, int stat, int lidhje, colTrupiRezervime ocoltrupigjeneruar, int idkrijuesi, colTrupiRezervime trupiEkzistues, bool kontrolloKonvertuar)
        {
            clsMesazh mesazh;


            idKoka = 0;

            idKoka = dbRegj.ruajKokaRezervimi(idKoka, idNiv, idKonf, idKlFurn, idMag, dtDk, nrDk, idLidhes, idSt, idNder, idNdVt, idPer, dtRegj, shenim, idNivelGjenerues, idKonfigGjenerues, idGjenerues, iddegeadministrative, prioriteti, idlloj, stat, lidhje, idkrijuesi);

            if (idKoka == 0)
                return new clsMesazh(false, "Ndodhi një Gabim gjatë ruajtjes së Kokës së Rezervimit");
            if (modifikim)
            {
                mesazh = dbRegj.modifikoKonvertimSipasIdDokKonvertuar(idLidhes, idKoka);
                if (!mesazh.Status)
                {
                    return mesazh;
                }
            }
              int idM=0;
            foreach (clsTrupiRezervime o in ocolTrupiRezervime)
            {
                o.IdKokaRezervime = idKoka;
                o.IdStatusDok = idSt;
                mesazh = dbRegj.ruajTrupiRezervime(out idM, o.IdKokaRezervime, o.IdArtikulli, o.IdNjesia, o.Sasia, o.Koeficenti, o.IdMag, o.Data, o.IdStatusDok, o.Shenja, o.IdTrupiNgaVjen, o.IdTrupiHyrje);
                if (!mesazh.Status)
                {
                    return mesazh;
                }
                clsTrupiRezervime trup = trupiEkzistues != null ? trupiEkzistues.Find(x => x.IdArtikulli == o.IdArtikulli && x.IdMag == o.IdMag) : null;//gjejme rreshtin e vjeter me kete artikull per te bere me pas zevendesimet per id e vjeter me id e re
                int idvjeter = trup == null?0: trup.IdTrupiRezervime;
             
                // ketu duhet koka e re jo e vjetra qe te behet update ne rregull
                //mesazh = dbRegj.ruajTrupiRezervime(out idM, o.IdKokaRezervime, o.IdArtikulli, o.IdNjesia, o.Sasia, o.Koeficenti, o.IdMag, o.Data, o.IdStatusDok, o.Shenja, o.IdTrupiNgaVjen, o.IdTrupiHyrje);
                //if (!mesazh.Status)
                //{
                //    return mesazh;
                //}
                mesazh = dbRegj.modifikoTrupiRezervimeIdHyrje(idvjeter, idM);
                if (!mesazh.Status)
                {
                    return mesazh;
                }

                mesazh = dbRegj.modifikoTrupiShitjeIdRezervimi(idvjeter, idM);
                if (!mesazh.Status)
                {
                    return mesazh;
                }
                mesazh = dbRegj.modifikoTrupiMagazinaIdRezervimi(idvjeter, idM);
                if (!mesazh.Status)
                {
                    return mesazh;
                }

                foreach (clsTrupiRezervime gj in ocoltrupigjeneruar)
                {
                    if (gj.IdTrupiHyrje == idvjeter)
                        gj.IdTrupiHyrje = idM;
                }
            }
            if (stat != 2)//jo per dokumentat e anulluar
            {// ndryshojme statusin e dokumentit nqs eshte ekzekutuar komplete
                if (idlloj == 2)// ne dalje kontrollojme hyrjet e tij
                {
                    ArrayList idte = new ArrayList();
                    foreach (clsTrupiRezervime o in ocolTrupiRezervime)
                    {
                        if (o.IdTrupiHyrje != 0)
                        {
                            clsTrupiRezervime tr = new clsTrupiRezervime(o.IdTrupiHyrje, dbRegj);
                            if (!idte.Contains(tr.IdKokaRezervime))
                            {
                                idte.Add(tr.IdKokaRezervime);
                                colTrupiRezervime col = new colTrupiRezervime();
                                col.mbushGjitheTrupiRezervimiNgaKoka(tr.IdKokaRezervime, dbRegj);
                                bool ekzekutuar = true;
                                foreach (clsTrupiRezervime trhyrje in col)
                                {

                                    double sasiMbetur = (trhyrje.Sasia * trhyrje.Koeficenti - dbRegj.ktheSasineKonvertuarSipasArtikullit(trhyrje.IdArtikulli, trhyrje.IdTrupiRezervime)) / trhyrje.Koeficenti;
                                    if (sasiMbetur != 0)
                                    {
                                        ekzekutuar = false;
                                        break;
                                    }
                                }
                                clsKokaRezervime kokarez = new clsKokaRezervime();
                                kokarez.mbushKokaRezervimiSipasID(tr.IdKokaRezervime, dbRegj);
                                if (kokarez.statusi != 2)
                                {
                                    mesazh = dbRegj.modifikoKokaRezervime(kokarez.idKokaRezervime, kokarez.idNivel, kokarez.idKonfigAmbjente, kokarez.idKlientFurnitor, kokarez.idMagazina, kokarez.dtDok, kokarez.nrDok, kokarez.idStatusDok, kokarez.dtRegjistrimi, kokarez.shenime, kokarez.idDegeAdministrative, kokarez.idPerdoruesi, kokarez.prioriteti, ekzekutuar ? 1 : 0, this.IdPerdoruesi);
                                    if (!mesazh.Status)
                                        return mesazh;
                                }
                            }
                        }
                    }
                }
                else if(kontrolloKonvertuar)
                {// kontrollojme hyrjen
                    bool ekzekutuar = true;
                    foreach (clsTrupiRezervime o in ocolTrupiRezervime)
                    {
                        double sasiMbetur = (o.Sasia * o.Koeficenti - dbRegj.ktheSasineKonvertuarSipasArtikullit(o.IdArtikulli, o.IdTrupiRezervime)) / o.Koeficenti;
                        if (sasiMbetur != 0)
                        {
                            ekzekutuar = false;
                            break;
                        }
                    }
                    mesazh = dbRegj.modifikoStatusKokaRezervime(idKoka, ekzekutuar ? 1 : 0);
                    if (!mesazh.Status)
                        return mesazh;
                }
            }
            mesazh = new clsMesazh(true, "Ruajtja përfundoi me sukses!");
            return mesazh;

        }

        /// <summary>
        /// Ruan objektin e  kokes se dokumentit te rezervimit ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsKokaRezervime.ruajRezervime"/> 
        /// </summary>
        /// <param name="hfNrAutoregjistrime">hiddenField i Devit qe mban numrat automatike,null nese nuk perodren</param>
        /// <param name="idPeriudha"></param>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
        public clsMesazh ruaj(IDictionary<string, object> hfNrAutoregjistrime, int idPeriudha)
        {
            DbShare.clsKonfigurimAmbjenti konf = new DbShare.clsKonfigurimAmbjenti();
            konf.mbushKonfiguriminMeID(this.IdKonfigAmbjente);

            clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();
            db.beginTransaksion();
            clsMesazh u_ruajt = ruaj(hfNrAutoregjistrime,  db); //perdor ruajtjen me transaksion

            if (!u_ruajt.Status)
            {
                db.rollbackTransaksion();
                return u_ruajt;
            }
            db.commitTransaksion();
            return u_ruajt;
        }

        public clsMesazh ruaj(IDictionary<string, object> hfNrAutoregjistrime, clsDatabaseRegjistrim db)
        {
            bool kaNdryshimNumri;
            clsMesazh mesazhKontrolli = kontrolloRezervim(out kaNdryshimNumri, db, hfNrAutoregjistrime);
            if (!mesazhKontrolli.Status)
            {
                return mesazhKontrolli;
            }
            int idkoka = 0;
            
            clsMesazh u_ruajt = this.ruajStatusRezervime(out idkoka, this.IdNivel, this.IdKonfigAmbjente, this.IdKlientFurnitor, this.IdMagazina, this.DtDok, this.NrDok, this.idLloji, this.IdDokNga, this.IdStatusDok, this.IdNdermarrje, this.IdNdermarrjeVit, this.IdPerdoruesi, this.DtRegjistrimi, this.Shenime, this.IdNivelGjenerues, this.IdKonfigGjenerues, this.IdGjenerues, this.IdDegeAdministrative, this.OcolTrupiRezervime, false,  db, this.prioriteti, this.statusi, this.idlidhje, new colTrupiRezervime(), this.IdPerdoruesi,null,true);

            this.idKokaRezervime = idkoka;
            if (!u_ruajt.Status)
            {
                return u_ruajt;
            }

            if (kaNdryshimNumri)
                return mesazhKontrolli;
            return u_ruajt;
        }

        public clsMesazh ruajNgaKokaShitje(IDictionary<string, object> hfNrAutoregjistrime, clsDatabaseRegjistrim db, int idkrijuesi, clsKokaRezervime kokaekzistueserezervimi, bool kontrolloKonvertuar)
        {
            bool kaNdryshimNumri;
            clsMesazh mesazhKontrolli = kontrolloRezervim(out kaNdryshimNumri, db, hfNrAutoregjistrime);
            if (!mesazhKontrolli.Status)
            {
                return mesazhKontrolli;
            }
            int idkoka = 0;
            colTrupiRezervime trupi;
            if (kokaekzistueserezervimi == null) trupi = this.OcolTrupiRezervime; else trupi = kokaekzistueserezervimi.OcolTrupiRezervime;
            clsMesazh u_ruajt = this.ruajStatusRezervime(out idkoka, this.IdNivel, this.IdKonfigAmbjente, this.IdKlientFurnitor, this.IdMagazina, this.DtDok, this.NrDok, this.idLloji, this.IdDokNga, this.IdStatusDok, this.IdNdermarrje, this.IdNdermarrjeVit, this.IdPerdoruesi, this.DtRegjistrimi, this.Shenime, this.IdNivelGjenerues, this.IdKonfigGjenerues, this.IdGjenerues, this.IdDegeAdministrative, this.OcolTrupiRezervime, false, db, this.prioriteti, this.statusi, this.idlidhje, new colTrupiRezervime(), idkrijuesi,trupi,kontrolloKonvertuar);
            this.idKokaRezervime = idkoka;
            if (!u_ruajt.Status)
            {
                return u_ruajt;
            }

            if (kaNdryshimNumri)
                return mesazhKontrolli;
            return u_ruajt;
        }
        private clsMesazh kontrolloRezervim(out bool kaNdryshimNumri, clsDatabaseRegjistrim dbRegj, IDictionary<string, object> hfNrAutoregjistrime)
        {
            kaNdryshimNumri = false;

            if (this.ocolTrupiRezervime.Count == 0)
                return new clsMesazh("Trupi rezervimit nuk mund te jete bosh!");

            foreach (clsTrupiRezervime t in this.ocolTrupiRezervime)
            {
                if (t.KodiArtikull != "" && t.PershkrimArtikull == "")
                {
                    return new clsMesazh("Te dhënat nuk janë të sakta");
                }
                if (t.KodiArtikull != "" && t.IdNjesia == 0)
                {
                    return new clsMesazh("Te dhënat nuk janë të sakta");
                }
                if (t.KodiArtikull != "" && t.IdMag == -1)
                {
                    return new clsMesazh("Të dhënat nuk janë të sakta");
                }
            }

            clsDatabaseAdmin db = new clsDatabaseAdmin(dbRegj );
            clsMesazh mes = new clsMesazh();

            if (hfNrAutoregjistrime != null)
            {
                mes = kontrolloNrAutoRezervime(out kaNdryshimNumri, dbRegj, hfNrAutoregjistrime);
                if (!mes.Status)
                    return mes;
            }
            if (dbRegj.ekzistonRegjistrimRezervimi(idKonfigAmbjente, nrDok, idMagazina, dtDok, idNdermarrje))
                return new clsMesazh(false, "Ekziston një rezervim me të njëjtin numër dokumenti: "+ nrDok+" ne kete date: "+dtDok+"!");
            if (kaNdryshimNumri)
                return mes;
            return new clsMesazh(true, "Kontrolli i rezervime u krye me sukses!");
        }

        private clsMesazh kontrolloNrAutoRezervime(out bool kaNdryshimNumri, clsDatabaseRegjistrim dbRegj, IDictionary<string, object> hfregjistrime)
        {
            clsDatabaseAdmin db = new clsDatabaseAdmin(dbRegj );
            List<NrAuto> list = DbAdmin.clsNrAutom.kontrollogjithenumrat(db, hfregjistrime, this.dtDok);
            if (NrAuto.ktheVlerenEre(list, "NrDok") != "")
                this.nrDok = NrAuto.ktheVlerenEre(list, "NrDok");
            DbCore.clsMesazh mes = NrAuto.ruajvlera(out kaNdryshimNumri, list, this.dtDok, this.idPerdoruesi, this.idNdermarrje, db);
            return new clsMesazh(mes.Status, mes.PershkrimMesazhi);
        }

        public clsMesazh modifikoRezervim(bool eshtetransferim, int idRezKoka, int idNiv, int idKonf, int idKlFurn, int idMag, DateTime dtDk, string nrDk, int idLidhes, int idSt, int idNder, int idNdVt, int idPer, DateTime dtRegj, string shenim, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues, int iddegeadministrative, int prioritet, int stat, colTrupiRezervime ocolTrupiRezervime, clsKokaRezervime oRezervimGjeneruar, clsDatabaseRegjistrim dbRegj, out int idkokare)
        {
            idkokare = 0;
            //colTrupiRezervime trupat = new colTrupiRezervime();
            //trupat.mbushTrupiRezervimi(idRezKoka, dbRegj);
            int idLidhesgjenerues = 0;
            clsMesazh mesazh;
            try
            {   //obj koka qe i kalohet si parameter eshte si duhet te modifikohet
                clsKokaRezervime kokaEkzistuese = new clsKokaRezervime();
                kokaEkzistuese.mbushKokaRezervimiSipasID(idRezKoka, dbRegj);

                if (string.IsNullOrEmpty(kokaEkzistuese.NrDok) || kokaEkzistuese.IdStatusDok == 2)
                {
                    return new clsMesazh(false, "Dokumenti ka ndryshuar! Ju lutem rihapeni përsëri!");
                }
                kokaEkzistuese.mbushTrupRezervime(dbRegj);

                kokaEkzistuese.IdStatusDok = 2;  //fshire

                idLidhes = kokaEkzistuese.IdKokaRezervimi;

                mesazh = dbRegj.modifikoKokaRezervime(kokaEkzistuese.IdKokaRezervimi, kokaEkzistuese.IdNivel, kokaEkzistuese.IdKonfigAmbjente, kokaEkzistuese.IdKlientFurnitor, kokaEkzistuese.IdMagazina, kokaEkzistuese.DtDok, kokaEkzistuese.NrDok, kokaEkzistuese.IdStatusDok, kokaEkzistuese.DtRegjistrimi, kokaEkzistuese.Shenime, kokaEkzistuese.IdDegeAdministrative, kokaEkzistuese.IdPerdoruesi, kokaEkzistuese.Prioriteti, kokaEkzistuese.Statusi, this.IdPerdoruesi);
                if (!mesazh.Status)
                    return mesazh;
                clsKokaRezervime kokaEkzistuesegjeneruar = new clsKokaRezervime();
                kokaEkzistuesegjeneruar.mbushKokaRezervimiSipasIDGjenerues(idRezKoka, 2, kokaEkzistuese.idKonfigAmbjente, dbRegj);
                if (kokaEkzistuesegjeneruar.idKokaRezervime > 0)
                {
                    if (string.IsNullOrEmpty(kokaEkzistuesegjeneruar.NrDok) || kokaEkzistuesegjeneruar.IdStatusDok == 2)
                    {
                        return new clsMesazh(false, "Dokumenti ka ndryshuar! Ju lutem rihapeni përsëri!");
                    }


                    kokaEkzistuesegjeneruar.IdStatusDok = 2;  //fshire

                    idLidhesgjenerues = kokaEkzistuesegjeneruar.IdKokaRezervimi;

                    mesazh = dbRegj.modifikoKokaRezervime(kokaEkzistuesegjeneruar.IdKokaRezervimi, kokaEkzistuesegjeneruar.IdNivel, kokaEkzistuesegjeneruar.IdKonfigAmbjente, kokaEkzistuesegjeneruar.IdKlientFurnitor, kokaEkzistuesegjeneruar.IdMagazina, kokaEkzistuesegjeneruar.DtDok, kokaEkzistuesegjeneruar.NrDok, kokaEkzistuesegjeneruar.IdStatusDok, kokaEkzistuesegjeneruar.DtRegjistrimi, kokaEkzistuesegjeneruar.Shenime, kokaEkzistuesegjeneruar.IdDegeAdministrative, kokaEkzistuesegjeneruar.IdPerdoruesi, kokaEkzistuesegjeneruar.Prioriteti, kokaEkzistuesegjeneruar.Statusi, this.IdPerdoruesi);
                    if (!mesazh.Status)
                        return mesazh;
                }

                DbAdmin.clsDatabaseAdmin data = new DbAdmin.clsDatabaseAdmin(dbRegj );
                DbShare.clsKonfigurimAmbjenti konf = new DbShare.clsKonfigurimAmbjenti();
                konf.mbushKonfiguriminMeID(idKonf);
                int idLlojDok;
                if (konf != null)
                    idLlojDok = konf.IdKategori;
                else
                    idLlojDok = -1;

                mesazh = ruajStatusRezervime(out idRezKoka, idNiv, idKonf, idKlFurn, idMag, dtDk, nrDk, kokaEkzistuese.idLloji, idLidhes, idSt, idNder, idNdVt, idPer, dtRegj, shenim, idNivelGjenerues, idKonfigGjenerues, idGjenerues, iddegeadministrative, ocolTrupiRezervime, true, dbRegj, prioritet, stat, kokaEkzistuese.idlidhje, oRezervimGjeneruar.ocolTrupiRezervime, this.idKrijuesi, ocolTrupiRezervime,true);

                idkokare = idRezKoka;

                //gjenerimi i RD/RA
                if (eshtetransferim)
                {

                    int idRezGjen = 0;
                    
                    mesazh = ruajStatusRezervime(out idRezGjen, oRezervimGjeneruar.idNivel, oRezervimGjeneruar.IdKonfigAmbjente, idKlFurn, idMag, dtDk, nrDk, oRezervimGjeneruar.idLloji, idLidhesgjenerues, oRezervimGjeneruar.IdStatusDok, idNder, idNdVt, idPer, dtRegj, shenim, idNiv, idKonf, idkokare, iddegeadministrative, oRezervimGjeneruar.OcolTrupiRezervime, false, dbRegj, prioritet, stat, idkokare, new colTrupiRezervime(),
                        this.idKrijuesi, oRezervimGjeneruar.OcolTrupiRezervime,true);


                }

                if (!mesazh.Status)
                    return mesazh;

                return new clsMesazh(true, "Modifikimi përfundoi me sukses!");
            }
            catch (Exception ce)
            {
                return new clsMesazh(false, ce.Message);
            }
        }

        public clsMesazh modifikoStatusRezervim(bool eshtetransferim, int idRezKoka, int idNiv, int idKonf, int idKlFurn, int idMag, DateTime dtDk, string nrDk, int idLidhes, int idSt, int idNder, int idNdVt, int idPer, DateTime dtRegj, string shenim, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues, int iddegeadministrative, int prioritet, int stat, colTrupiRezervime ocolTrupiRezervime, clsKokaRezervime oRezervimGjeneruar, clsDatabaseRegjistrim dbRegj, out int idkokare)
        {
            idkokare = 0;
            //colTrupiRezervime trupat = new colTrupiRezervime();
            //trupat.mbushTrupiRezervimi(idRezKoka, dbRegj);
            int idLidhesgjenerues = 0;
            clsMesazh mesazh;
            try
            {   //obj koka qe i kalohet si parameter eshte si duhet te modifikohet
                clsKokaRezervime kokaEkzistuese = new clsKokaRezervime();
                kokaEkzistuese.mbushKokaRezervimiSipasID(idRezKoka, dbRegj);

                if (string.IsNullOrEmpty(kokaEkzistuese.NrDok) || kokaEkzistuese.IdStatusDok == 2)
                {
                    return new clsMesazh(false, "Dokumenti ka ndryshuar! Ju lutem rihapeni përsëri!");
                }
                kokaEkzistuese.mbushTrupRezervime(dbRegj);

                kokaEkzistuese.IdStatusDok = 2;  //fshire

                idLidhes = kokaEkzistuese.IdKokaRezervimi;

                mesazh = dbRegj.modifikoKokaRezervime(kokaEkzistuese.IdKokaRezervimi, kokaEkzistuese.IdNivel, kokaEkzistuese.IdKonfigAmbjente, kokaEkzistuese.IdKlientFurnitor, kokaEkzistuese.IdMagazina, kokaEkzistuese.DtDok, kokaEkzistuese.NrDok, kokaEkzistuese.IdStatusDok, kokaEkzistuese.DtRegjistrimi, kokaEkzistuese.Shenime, kokaEkzistuese.IdDegeAdministrative, kokaEkzistuese.IdPerdoruesi, kokaEkzistuese.Prioriteti, kokaEkzistuese.Statusi, this.IdPerdoruesi);
                if (!mesazh.Status)
                    return mesazh;
                clsKokaRezervime kokaEkzistuesegjeneruar = new clsKokaRezervime();
                kokaEkzistuesegjeneruar.mbushKokaRezervimiSipasIDGjenerues(idRezKoka, 2, kokaEkzistuese.idKonfigAmbjente, dbRegj);
                if (kokaEkzistuesegjeneruar.idKokaRezervime > 0)
                {
                    if (string.IsNullOrEmpty(kokaEkzistuesegjeneruar.NrDok) || kokaEkzistuesegjeneruar.IdStatusDok == 2)
                    {
                        return new clsMesazh(false, "Dokumenti ka ndryshuar! Ju lutem rihapeni përsëri!");
                    }


                    kokaEkzistuesegjeneruar.IdStatusDok = 2;  //fshire

                    idLidhesgjenerues = kokaEkzistuesegjeneruar.IdKokaRezervimi;

                    mesazh = dbRegj.modifikoStatusKokaRezervime(kokaEkzistuesegjeneruar.IdKokaRezervimi, kokaEkzistuesegjeneruar.Statusi);
                    if (!mesazh.Status)
                        return mesazh;
                }

                DbAdmin.clsDatabaseAdmin data = new DbAdmin.clsDatabaseAdmin(dbRegj );
                int idPeriudheDoku = new DbAdmin.clsPeriudhaKontabel(dtDk, idNder, data).IdPeriudha;
                DbShare.clsKonfigurimAmbjenti konf = new DbShare.clsKonfigurimAmbjenti();
                konf.mbushKonfiguriminMeID(idKonf);
                int idLlojDok;
                if (konf != null)
                    idLlojDok = konf.IdKategori;
                else
                    idLlojDok = -1;

                mesazh = ruajRezervime(out idRezKoka, idNiv, idKonf, idKlFurn, idMag, dtDk, nrDk, kokaEkzistuese.idLloji, idLidhes, idSt, idNder, idNdVt, idPer, dtRegj, shenim, idNivelGjenerues, idKonfigGjenerues, idGjenerues, iddegeadministrative, ocolTrupiRezervime, true, idPeriudheDoku, dbRegj, prioritet, stat, kokaEkzistuese.idlidhje, oRezervimGjeneruar.ocolTrupiRezervime, this.idKrijuesi);

                idkokare = idRezKoka;

                //gjenerimi i RD/RA
                if (eshtetransferim)
                {

                    int idRezGjen = 0;

                    mesazh = ruajRezervime(out idRezGjen, oRezervimGjeneruar.idNivel, oRezervimGjeneruar.IdKonfigAmbjente, idKlFurn, idMag, dtDk, nrDk, oRezervimGjeneruar.idLloji, idLidhesgjenerues, oRezervimGjeneruar.IdStatusDok, idNder, idNdVt, idPer, dtRegj, shenim, idNiv, idKonf, idkokare, iddegeadministrative, oRezervimGjeneruar.OcolTrupiRezervime, false, idPeriudheDoku, dbRegj, prioritet, stat, idkokare, new colTrupiRezervime(),
                        this.idKrijuesi);


                }

                if (!mesazh.Status)
                    return mesazh;

                return new clsMesazh(true, "Modifikimi përfundoi me sukses!");
            }
            catch (Exception ce)
            {
                return new clsMesazh(false, ce.Message);
            }
        }

        /// <summary>
        /// Modifikon objektin e  kokes se dokumentit te rezervimit ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsKokaRezervime.modifikoRezervim"/> 
        /// </summary>
        /// <param name="lidhur">tregon nese eshte i lidhur ose jo</param>
        /// <returns > nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo</returns>
        public clsMesazh modifiko(bool lidhur, bool eshtetransferim, out  string shfaqmesazhapolupe)
        {
            using (var scope = new MyTransactionScope())
            using (clsDatabaseRegjistrim data = new clsDatabaseRegjistrim())
            {
                var mesazh = modifiko(lidhur, eshtetransferim, out shfaqmesazhapolupe, data);
                if (mesazh)
                    scope.Complete();

                return mesazh;
            }
        }


        public clsMesazh modifiko(bool lidhur, bool eshtetransferim, out string shfaqmesazhapolupe, clsDatabaseRegjistrim dbRegj)
        {
            clsKokaRezervime kokaEkzistuese = new clsKokaRezervime(this.idKokaRezervime);
            this.idKrijuesi = kokaEkzistuese.idKrijuesi;
            shfaqmesazhapolupe = "jo";
            clsMesazh u_modifikua;
            if (!lidhur || (lidhur && this.statusi != 0))
            {
                int idkokare = 0;
                u_modifikua = modifikoRezervim(eshtetransferim, this.IdKokaRezervimi, IdNivel, this.IdKonfigAmbjente, this.IdKlientFurnitor, this.IdMagazina, this.DtDok, this.NrDok, this.IdDokNga, this.IdStatusDok, this.IdNdermarrje, this.IdNdermarrjeVit, this.IdPerdoruesi, this.DtRegjistrimi, this.Shenime, this.IdNivelGjenerues, this.IdKonfigGjenerues, this.IdGjenerues, this.IdDegeAdministrative, this.Prioriteti, this.Statusi, this.OcolTrupiRezervime, this.ORezervimDalje, dbRegj, out idkokare);

                this.idKokaRezervime = idkokare;

            }
            else
            {

                u_modifikua = dbRegj.modifikoKokaRezervime(this.IdKokaRezervimi, this.IdNivel, this.IdKonfigAmbjente, this.IdKlientFurnitor, this.IdMagazina, this.DtDok, this.NrDok, this.IdStatusDok, this.DtRegjistrimi, this.Shenime, this.IdDegeAdministrative, this.IdPerdoruesi, this.Prioriteti, this.Statusi, this.IdPerdoruesi);
            }
            
            return u_modifikua;
        }
        /// Modifikon objektin e  kokes se dokumentit te rezervimit ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsKokaRezervime.modifikoRezervim"/> 
        /// </summary>
        /// <param name="lidhur">tregon nese eshte i lidhur ose jo</param>
        /// <returns > nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo</returns>
        public clsMesazh modifikoStatus(bool lidhur, bool eshtetransferim, out  string shfaqmesazhapolupe)
        {
            clsMesazh u_modifikua;
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            clsKokaRezervime kokaEkzistuese = new clsKokaRezervime();
            kokaEkzistuese.mbushKokaRezervimiSipasID(this.idKokaRezervime);
            this.idKrijuesi = kokaEkzistuese.idKrijuesi;

            shfaqmesazhapolupe = "jo";
            if (lidhur == false || (lidhur && this.statusi != 0))
            {
                try
                {
                    data.beginTransaksion();
                    int idkokare = 0;
                    u_modifikua = modifikoStatusRezervim(eshtetransferim, this.IdKokaRezervimi, IdNivel, this.IdKonfigAmbjente, this.IdKlientFurnitor, this.IdMagazina, this.DtDok, this.NrDok, this.IdDokNga, this.IdStatusDok, this.IdNdermarrje, this.IdNdermarrjeVit, this.IdPerdoruesi, this.DtRegjistrimi, this.Shenime, this.IdNivelGjenerues, this.IdKonfigGjenerues, this.IdGjenerues, this.IdDegeAdministrative, this.Prioriteti, this.Statusi, this.OcolTrupiRezervime, this.ORezervimDalje, data, out idkokare);
                    this.idKokaRezervime = idkokare;
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

            u_modifikua = data.modifikoStatusKokaRezervime(this.IdKokaRezervimi, this.Statusi);
            if (!u_modifikua.Status)
            {
                data.rollbackTransaksion();
                return u_modifikua;
            }
            data.commitTransaksion();
            return u_modifikua;
        }



        public clsMesazh fshiRezervim(int idRezKoka, int idperdoruesi, clsDatabaseRegjistrim dbRegj, bool fshiAnullim)
        {
            clsMesazh mesazh;

            clsKokaRezervime kokaEkzistuese = new clsKokaRezervime();
            kokaEkzistuese.mbushKokaRezervimiSipasID(idRezKoka, dbRegj);

            mesazh = dbRegj.fshiKonvertimSipasIdDokKonvertuar(kokaEkzistuese.IdKokaRezervimi);
            if (!mesazh.Status)
                return mesazh;

            kokaEkzistuese.IdStatusDok = 2;
            mesazh = dbRegj.modifikoKokaRezervime(kokaEkzistuese.IdKokaRezervimi, kokaEkzistuese.IdNivel, kokaEkzistuese.IdKonfigAmbjente, kokaEkzistuese.IdKlientFurnitor, kokaEkzistuese.IdMagazina, kokaEkzistuese.DtDok, kokaEkzistuese.NrDok, kokaEkzistuese.IdStatusDok, kokaEkzistuese.DtRegjistrimi, kokaEkzistuese.Shenime, kokaEkzistuese.IdDegeAdministrative, kokaEkzistuese.IdPerdoruesi, kokaEkzistuese.Prioriteti, kokaEkzistuese.Statusi, idperdoruesi);

            if (!mesazh.Status)
                return mesazh;

            mesazh = fshiDokumentinEGjeneruar(idRezKoka, kokaEkzistuese, dbRegj, idPerdoruesi, 2);
            if (!mesazh.Status)
                return mesazh;
            
            if(fshiAnullim)
            {
                mesazh = fshiDokumentinEGjeneruar(idRezKoka, kokaEkzistuese, dbRegj, idPerdoruesi, 1);
                if (!mesazh.Status)
                    return mesazh;
            }

            if (idLloji == 2)// ne dalje kontrollojme hyrjet e tij
            {
                ArrayList idte = new ArrayList();
                ocolTrupiRezervime.mbushGjitheTrupiRezervimiNgaKoka(kokaEkzistuese.idKokaRezervime, dbRegj);
                foreach (clsTrupiRezervime o in ocolTrupiRezervime)
                {
                    if (o.IdTrupiHyrje != 0)
                    {
                        clsTrupiRezervime tr = new clsTrupiRezervime(o.IdTrupiHyrje, dbRegj);
                        if (!idte.Contains(tr.IdKokaRezervime))
                        {
                            idte.Add(tr.IdKokaRezervime);
                            colTrupiRezervime col = new colTrupiRezervime();
                            col.mbushGjitheTrupiRezervimiNgaKoka(tr.IdKokaRezervime, dbRegj);
                            bool ekzekutuar = true;
                            foreach (clsTrupiRezervime trhyrje in col)
                            {
                                double sasiMbetur = (trhyrje.Sasia * trhyrje.Koeficenti - dbRegj.ktheSasineKonvertuarSipasArtikullit(trhyrje.IdArtikulli, trhyrje.IdTrupiRezervime)) / trhyrje.Koeficenti;
                                if (sasiMbetur != 0)
                                {
                                    ekzekutuar = false;
                                    break;
                                }
                            }
                            clsKokaRezervime kokarez = new clsKokaRezervime();
                            kokarez.mbushKokaRezervimiSipasID(tr.IdKokaRezervime, dbRegj);
                            if (kokarez.statusi != 2)
                            {
                                mesazh = dbRegj.modifikoKokaRezervime(kokarez.idKokaRezervime, kokarez.idNivel, kokarez.idKonfigAmbjente, kokarez.idKlientFurnitor, kokarez.idMagazina, kokarez.dtDok, kokarez.nrDok, kokarez.idStatusDok, kokarez.dtRegjistrimi, kokarez.shenime, kokarez.idDegeAdministrative, kokarez.idPerdoruesi, kokarez.prioriteti, ekzekutuar ? 1 : 0, idperdoruesi);
                                if (!mesazh.Status)
                                    return mesazh;
                            }
                        }
                    }
                }
            }
            return new clsMesazh(true, "Fshirja përfundoi me sukses!");


        }

        public clsMesazh fshiDokumentinEGjeneruar(int idRezKoka, clsKokaRezervime kokaEkzistuese, clsDatabaseRegjistrim dbRegj, int idperdoruesi, int lloji)
        {
            clsKokaRezervime kokaEkzistuesegjeneruar = new clsKokaRezervime();
            kokaEkzistuesegjeneruar.mbushKokaRezervimiSipasIDGjenerues(idRezKoka, lloji, kokaEkzistuese.idKonfigAmbjente, dbRegj);
            if (kokaEkzistuesegjeneruar.idKokaRezervime > 0)
            {
                if (string.IsNullOrEmpty(kokaEkzistuesegjeneruar.NrDok) || kokaEkzistuesegjeneruar.IdStatusDok == 2)
                {
                    return new clsMesazh(false, "Dokumenti ka ndryshuar! Ju lutem rihapeni përsëri!");
                }


                kokaEkzistuesegjeneruar.IdStatusDok = 2;  //fshire


                return dbRegj.modifikoKokaRezervime(kokaEkzistuesegjeneruar.IdKokaRezervimi, kokaEkzistuesegjeneruar.IdNivel, kokaEkzistuesegjeneruar.IdKonfigAmbjente, kokaEkzistuesegjeneruar.IdKlientFurnitor, kokaEkzistuesegjeneruar.IdMagazina, kokaEkzistuesegjeneruar.DtDok, kokaEkzistuesegjeneruar.NrDok, kokaEkzistuesegjeneruar.IdStatusDok, kokaEkzistuesegjeneruar.DtRegjistrimi, kokaEkzistuesegjeneruar.Shenime, kokaEkzistuesegjeneruar.IdDegeAdministrative, kokaEkzistuesegjeneruar.IdPerdoruesi, kokaEkzistuesegjeneruar.Prioriteti, kokaEkzistuesegjeneruar.Statusi, idperdoruesi);
               
            }

            return new MesazhSuksesi();
        }


        /// <summary>
        /// Fshin objektin e  kokes se dokumentit te rezervimit ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.fshiKokaRezervime"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh fshi()
        {
            clsKokaRezervime data = new clsKokaRezervime();
            clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim();
            dbRegj.beginTransaksion();
            clsMesazh u_fshi = data.fshiRezervim(this.idKokaRezervime, this.idPerdoruesi, dbRegj,false);
            if (u_fshi.Status)
                dbRegj.commitTransaksion();
            else
                dbRegj.rollbackTransaksion();
            return u_fshi;
        }


        /// <summary>
        /// perdoret per te gjetur totalin e sasise te nje artikulli ne trupin e nje dokumenti rezervim per te kontrolluar me pas gjendjen e tij ne krahasim me sasine qe do dale nga magazina
        /// </summary>
        /// <param name="trupi"> koleksion me rreshta te trupit te nje dokumenti rezervim</param>
        /// <param name="idArtikulli"> id e artikulli per te cilin do te gjejme totalin</param>
        /// <param name="idMagazina">id-ja e magazines</param>
        /// <returns> kthen totalin e sasise te artikullit</returns>
        public double ktheTotalinArtikullit(colTrupiRezervime trupi, int idArtikulli, int idMagazina)
        {
            double totali = 0;
            foreach (clsTrupiRezervime o in trupi)
            {
                if (o.IdArtikulli == idArtikulli && o.IdMag == idMagazina)
                    totali += o.Sasia * o.Koeficenti;
            }
            return totali;
        }


        /// <summary>
        /// Merr objektin e  kokes se dokumentit te rezervimit sipas nr dhe dt dokumenti nga tabela perkatese ne databaze.
        /// </summary>
        /// <returns > nje objekt clsKokaRezervime qe permban objektin e kerkuar</returns>
        public clsKokaRezervime merrSipasIdNivelNrDokDtDok()
        {
            clsKokaRezervime data = new clsKokaRezervime(this.IdNivel, this.NrDok, this.DtDok);
            return data;

        }

        /// <summary>
        /// Merr gjithe e  kokat e dokumentave te rezervimit sipas ndermarjevitit nga tabela perkatese ne databaze.
        /// </summary>
        /// <param name="idNdermVit">id e ndermarje vitit</param>
        /// <returns > nje object colKokaRezervime qe permban nje koleksion me gjithe kokat e dokumentave te rezervimeve te nje ndermarje ne nje vit te caktuar</returns>
        //public colKokaRezervime merriTeGjithe(int idNdermVit)
        //{
        //    colKokaRezervime data = new colKokaRezervime(idNdermVit);
        //    return data;            
        //}

        ///// <summary>
        ///// Merr trupin  e  nje dokumenti te rezervimit nga tabela perkatese ne databaze.
        ///// </summary>
        ///// <returns > nje object colTrupiRezervime qe permban nje koleksion me trupin e dokumentit te rezervimit</returns>
        //public colTrupiRezervime merrTrupiRezervime(clsDatabaseRegjistrim db)
        //{
        //    colTrupiRezervime trupi = new colTrupiRezervime();
        //    trupi.mbushTrupiRezervimi(idKokaRezervime, db);
        //    return trupi;
        //}
        /// <summary>
        /// Merr trupin  e  nje dokumenti te rezervimt nga tabela perkatese ne databaze.Therret funksionin
        /// </summary>
        /// <returns > nje object colTrupiRezervime qe permban nje koleksion me trupin e dokumentit te rezervimit</returns>
        public bool mbushTrupRezervime(clsDatabaseRegjistrim db)
        {
            ocolTrupiRezervime = new colTrupiRezervime();
            return ocolTrupiRezervime.mbushTrupiRezervimi(idKokaRezervime, db);
        }
        /// <summary>
        /// Merr trupin  e  nje dokumenti te rezervimit nga tabela perkatese ne databaze.Therret funksionin
        /// </summary>
        /// <returns > nje object colTrupiRezervime qe permban nje koleksion me trupin e dokumentit te rezervimit</returns>
        public bool mbushTrupRezervime()
        {
            ocolTrupiRezervime = new colTrupiRezervime();
            return ocolTrupiRezervime.mbushTrupiRezervimi(idKokaRezervime);
        }
        /// <summary>
        /// mbush koken e rezervimit sipas id gjenerues
        /// </summary>
        /// <param name="idGjenerues">id e gjeneruesit</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        /// <param name="lloji">lloji 1-hyrje 2-dalje</param>
        /// <param name="idkonfiggjenerues"></param>
        public bool mbushKokaRezervimiSipasIDGjenerues(int idGjenerues, int lloji, int idkonfiggjenerues, clsDatabaseRegjistrim db)
        {
            return mbushKokaRezervime(db.ktheKokaRezervimiSipasIDGjenerues(idGjenerues, lloji, idkonfiggjenerues));
        }
        /// <summary>
        /// mbush koken e rezervimit sipas id gjenerues
        /// </summary>
        /// <param name="idGjenerues">id e gjeneruesit</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        /// <param name="lloji">lloji 1-hyrje 2-dalje</param>
        /// <param name="idkonfiggjenerues"></param>
        public bool mbushKokaRezervimiSipasIDGjenerues(int idGjenerues, int lloji, int idkonfiggjenerues)
        {
            clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();
            bool sukses = mbushKokaRezervime(db.ktheKokaRezervimiSipasIDGjenerues(idGjenerues, lloji, idkonfiggjenerues));
            db.Dispose();
            return sukses;
        }
        /// <summary>
        /// mbush koken e rezervimit sipas id dokNga
        /// </summary>
        /// <param name="iddokNga">id dokumenti nga</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        public bool mbushKokaRezervimiSipasIDDokNga(int iddokNga)
        {
            clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();
            bool sukses = mbushKokaRezervime(db.ktheKokaRezervimiSipasIDDokNga(iddokNga));
            db.Dispose();
            return sukses;
        }

        /// <summary>
        /// mbush koken e rezervimit sipas id se kokes
        /// </summary>
        /// <param name="idKoka">id e kokes se rezervimit</param>
        /// <returns>kthen true nese mbushja kryhet me sukses ne te kundert kthen false</returns>
        /// <param name="db"></param>
        public bool mbushKokaRezervimiSipasID(int idKoka, clsDatabaseRegjistrim db)
        {
            return mbushKokaRezervime(db.ktheKokaRezervimiSipasID(idKoka));
        }
        /// <summary>
        /// mbush koken e rezervimit sipas id se kokes
        /// </summary>
        /// <param name="idKokaRezervime">id e kokes se rezervimit</param>
        /// <returns>kthen true nese mbushja kryhet me sukses ne te kundert kthen false</returns>
        public bool mbushKokaRezervimiSipasID(int idKokaRezervime)
        {
            using (clsDatabaseRegjistrim db = new clsDatabaseRegjistrim())
            {
                return mbushKokaRezervime(db.ktheKokaRezervimiSipasID(idKokaRezervime));
            }
        }

        public static string merrNgjyreKonvertimeRezervime(int idndermarje, int idkoka)
        {
            using (clsDatabaseRegjistrim dbKokeShitje = new clsDatabaseRegjistrim())
            {
                return dbKokeShitje.merrNgjyreKonvertimeRezervime(idndermarje, idkoka);
            }
        }

        public static bool kaAutorizime(int idkoka, int idperdoruesi)
        {
            clsDatabaseRegjistrim dbKokeShitje = new clsDatabaseRegjistrim();
            bool mbush = dbKokeShitje.kaAutorizimKokaRezervime(idkoka, idperdoruesi);
            dbKokeShitje.Dispose();
            return mbush;
        }
        #endregion

        #region Metoda Internal

        internal bool mbushKokaRezervime(DataRow rreshti)
        {
            if (rreshti != null)
            {
                try
                {
                    int.TryParse(rreshti["IDKOKAREZERVIME"].ToString(), out idKokaRezervime);
                    int.TryParse(rreshti["IDNIVEL"].ToString(), out idNivel);
                    int.TryParse(rreshti["IDKONFIGAMBJENTE"].ToString(), out idKonfigAmbjente);
                    int.TryParse(rreshti["IDKLIENTFURNITOR"].ToString(), out idKlientFurnitor);
                    int.TryParse(rreshti["IDMAG"].ToString(), out idMagazina);
                    nrDok = rreshti["NRDOK"].ToString();
                    DateTime.TryParse(rreshti["DTDOK"].ToString(), out dtDok);
                    int.TryParse(rreshti["IDDOKNGA"].ToString(), out idDokNga);
                    int.TryParse(rreshti["IDSTATUSDOK"].ToString(), out idStatusDok);
                    int.TryParse(rreshti["IDNDERM"].ToString(), out idNdermarrje);
                    int.TryParse(rreshti["IDNDERMVIT"].ToString(), out idNdermarrjeVit);
                    int.TryParse(rreshti["IDKRIJUESI"].ToString(), out idKrijuesi);
                    int.TryParse(rreshti["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    DateTime.TryParse(rreshti["DTREGJISTRIMI"].ToString(), out dtRegjistrimi);
                    shenime = rreshti["SHENIME"].ToString();
                    int.TryParse(rreshti["IDNIVELGJENERUES"].ToString(), out idNivelGjenerues);
                    int.TryParse(rreshti["IDKONFIGGJENERUES"].ToString(), out idKonfigGjenerues);
                    int.TryParse(rreshti["IDGJENERUES"].ToString(), out idGjenerues);
                    int.TryParse(rreshti["IDDEGEADMINISTRATIVE"].ToString(), out idDegeAdministrative);
                    DateTime.TryParse(rreshti["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(rreshti["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    int.TryParse(rreshti["PRIORITETI"].ToString(), out prioriteti);
                    int.TryParse(rreshti["IDLLOJI"].ToString(), out idLloji);
                    int.TryParse(rreshti["IDSTATUS"].ToString(), out statusi);
                    ImbLogger.LogTraceShitje("Mbaroi metoda mbushKokaRezervime");
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se dokumentit nga db-ja");
                }
            }
            else
            {
                return false;
            }
                
        }

        #endregion
    }
}