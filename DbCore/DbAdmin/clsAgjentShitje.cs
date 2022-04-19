using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Globalization;
using System.Resources;
using DbCore.IMBUtils.Validation;
using DbCore.IMBUtils.Logging;

namespace DbCore.DbAdmin
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne Agjentet e Shitjes
    ///  (Te dhenat merren nga tabela :T_AGJENTSHITJE)
    /// </summary> 
    public class clsAgjentShitje
    {
        #region Atribute

        private int idAgjentShitje;
        private String kodiAgjentShitje;
        private String emriAgjentShitje;
        private String mbiemriAgjentShitje;
        private String telAgjentShitje;
        private String faxAgjentShitje;
        private String emailAgjentShitje;
        private int idQyteti;
        private double perqindjeAgjentShitje;
        private int idLlogari;
        private int idNdermarje;
        private int idKonfig;
        private int idStatusDok;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private int idPerdoruesi;
        private colLidhjetAutorizim oColLidhjetAutorizim;
        private int idPerdoruesMobile;
        private int idDrejtori;

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsAgjentShitje(int id, string kodi, string emri, string mbiemri, string tel, string fax, string email,
                                int qyt, double perq, int llog, int idnderm, int idkonfig, int idstatusdok, int idperdoruesi, int idperdoruesmobile, int idDrejtori)
        {
            idAgjentShitje = id;
            kodiAgjentShitje = kodi;
            emriAgjentShitje = emri;
            mbiemriAgjentShitje = mbiemri;
            telAgjentShitje = tel;
            faxAgjentShitje = fax;
            emailAgjentShitje = email;
            idQyteti = qyt;
            perqindjeAgjentShitje = perq;
            idLlogari = llog;
            idNdermarje = idnderm;
            idKonfig = idkonfig;
            idStatusDok = idstatusdok;
            idPerdoruesi = idperdoruesi;
            oColLidhjetAutorizim = new colLidhjetAutorizim();
            idPerdoruesMobile = idperdoruesmobile;
            this.idDrejtori = idDrejtori;
        }

        /// <summary>
        /// Konstruktor i klases me parametra.
        /// Perdoret kur krijohet nje agjent i ri, ben dhe kontrollin nese vlerat jane te vlefshme, ose jo.
        /// </summary>
        /// <param name="kodi">kodi</param>
        /// <param name="emri">emri</param>
        /// <param name="mbiemri">mbiemri</param>
        /// <param name="tel">tel</param>
        /// <param name="fax">fax</param>
        /// <param name="email">email</param>
        /// <param name="qyt">qyt</param>
        /// <param name="perq">perq</param>
        /// <param name="llog">llog</param>
        /// <param name="idnderm">idnderm</param>
        /// <param name="idkonfig">idkonfig</param>
        /// <param name="idperdoruesi">idperdoruesi</param>
        /// <param name="shtim">shtim, variabel qe tregon nese veprimi qe do behet eshte shtim apo modifikim</param>
        public clsAgjentShitje(int idAgjentShitje, string kodi, string emri, string mbiemri, string tel, string fax, string email, int qyt, double perq, int llog, int idnderm, int idkonfig, int idperdoruesi, bool shtim, colLidhjetAutorizim collidhjesaut, int idperdoruesmobile, int idDrejtori, ResourceManager rm, CultureInfo ci)
        {
            try
            {
                this.IdAgjentShitje = idAgjentShitje;
                this.kodiAgjentShitje = kodi;
                this.emriAgjentShitje = emri;
                this.mbiemriAgjentShitje = mbiemri;
                this.telAgjentShitje = tel;
                this.faxAgjentShitje = fax;
                this.emailAgjentShitje = email;
                this.idQyteti = qyt;
                this.perqindjeAgjentShitje = perq;
                this.idLlogari = llog;
                this.idNdermarje = idnderm;
                this.idKonfig = idkonfig;
                this.idStatusDok = 1;
                this.idPerdoruesi = idperdoruesi;
                oColLidhjetAutorizim = collidhjesaut;
                idPerdoruesMobile = idperdoruesmobile;
                this.idDrejtori = idDrejtori;
                clsMesazh mesazh = this.kontrolloAgjent(shtim, rm, ci);
                if (!mesazh.Status)
                    throw new Exception(mesazh.PershkrimMesazhi);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        /// <summary>
        /// konstruktor me 2 parametra
        /// </summary>
        /// <param name="kodi">kodi i agjentit te shitjes</param>
        /// <param name="idnderm">id e ndermarrjes</param>
        public clsAgjentShitje(string kodi, int idnderm)
        {
            if (kodi == null || kodi == "") return;
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            mbushAgjentShitje(data.ktheAgjentShitjeSipasKodit(kodi, idnderm));
            data.Dispose();
        }

        public clsAgjentShitje(string kodi, int idnderm, clsDatabaseAdmin db)
        {
            mbushAgjentShitje(db.TransCache.getAgjentShitje(kodi, idnderm, db));
        }

        /// <summary>
        /// konstrukotr me 1 parameter
        /// </summary>
        /// <param name="id">id e agjentit te shitjes</param>
        public clsAgjentShitje(int id)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            mbushAgjentShitje(data.merrAgjentShitje(id));
            data.Dispose();
        }

        /// <summary>
        /// Konstruktori default i klases
        /// </summary>
        public clsAgjentShitje()
        {
        }
        public clsAgjentShitje(DataRow dbDataRowAgjentShitje)
        {
            mbushAgjentShitje(dbDataRowAgjentShitje);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdAgjentShitje
        {
            get { return idAgjentShitje; }
            set { idAgjentShitje = value; }
        }

        /// <summary>
        /// Kthen Kodin e Agjentit te Shtijes.
        /// </summary>
        public String KodiAgjentShitje
        {
            get { return kodiAgjentShitje; }
            set { kodiAgjentShitje = value; }
        }

        /// <summary>
        /// Kthen Emrin  e Agjentit te Shtijes.
        /// </summary>
        public String EmriAgjentShitje
        {
            get { return emriAgjentShitje; }
            set { emriAgjentShitje = value; }
        }

        /// <summary>
        /// Kthen Mbiemrin e Agjentit te Shtijes.
        /// </summary>
        public String MbiemriAgjentShitje
        {
            get { return mbiemriAgjentShitje; }
            set { mbiemriAgjentShitje = value; }
        }

        /// <summary>
        /// Kthen Nr e Telefonit te Agjentit te Shtijes.
        /// </summary>
        public String TelAgjentShitje
        {
            get { return telAgjentShitje; }
            set { telAgjentShitje = value; }
        }

        /// <summary>
        /// Kthen Faxin e Agjentit te Shtijes.
        /// </summary>
        public String FaxAgjentShitje
        {
            get { return faxAgjentShitje; }
            set { faxAgjentShitje = value; }
        }

        /// <summary>
        /// Kthen Email-in e Agjentit te Shtijes.
        /// </summary>
        public String EmailAgjentShitje
        {
            get { return emailAgjentShitje; }
            set { emailAgjentShitje = value; }
        }

        /// <summary>
        /// Kthen Id-ne qytetit qe i eshte caktuar Agjentit te Shtijes.
        /// </summary>
        public int IdQyteti
        {
            get { return idQyteti; }
            set { idQyteti = value; }
        }

        /// <summary>
        /// Kthen Perqindejn qe i eshte caktuar Agjentit te Shtijes.
        /// </summary>
        public double PerqindjeAgjentShitje
        {
            get { return perqindjeAgjentShitje; }
            set { perqindjeAgjentShitje = value; }
        }

        /// <summary>
        /// Kthen ID-ne e llogarise qe i eshte caktuar Agjentit te Shtijes.
        /// </summary>
        public int IdLlogari
        {
            get { return idLlogari; }
            set { idLlogari = value; }
        }

        /// <summary>
        /// Kthen ID-ne e ndermarrjes qe i eshte caktuar Agjentit te Shtijes.
        /// </summary>
        public int IdNdermarje
        {
            get { return idNdermarje; }
            set { idNdermarje = value; }
        }

        /// <summary>
        /// Kthen ID-ne e konfigurimit
        /// </summary>
        public int IdKonfig
        {
            get { return idKonfig; }
            set { idKonfig = value; }
        }
        /// <summary>
        /// Kthen ID-ne e statusit te agjentit 0-draft, 1-ruajtur,2 fshire, 3 stornim
        /// </summary>
        public int IdStatusDok
        {
            get { return idStatusDok; }
            set { idStatusDok = value; }
        }   /// <summary>
            /// Kthen ID-ne e idperdoruesi
            /// </summary>
        public int IdPerdoruesi
        {
            get { return idPerdoruesi; }
            set { idPerdoruesi = value; }
        }
        /// <summary>
        /// Kthen dt e krijimit
        /// </summary>
        public DateTime DtKrijimi
        {
            get { return dtKrijimi; }
            set { dtKrijimi = value; }

        }  /// <summary>
           /// Kthen date e modifikimit te fundit
           /// </summary>
        public DateTime DtModifikimi
        {
            get { return dtModifikimi; }
            set { dtModifikimi = value; }

        }
        public int IdPerdoruesMobile
        {
            get { return idPerdoruesMobile; }
            set { idPerdoruesMobile = value; }

        }

        public int IdDrejtori
        {
            get
            {
                return idDrejtori;
            }

            set
            {
                idDrejtori = value;
            }
        }
        #endregion

        #region Metoda Publike

        /// <summary>
        /// Ruan objektin e Agjentit te Shtijes ne databaze.
        /// </summary>
        public clsMesazh ruaj()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            clsMesazh mesazh;
            int id;
            try
            {
                data.beginTransaksion();
                mesazh = data.ruajAgjentShitje(out id, this.KodiAgjentShitje, this.EmriAgjentShitje, this.MbiemriAgjentShitje, this.TelAgjentShitje, this.FaxAgjentShitje, this.EmailAgjentShitje,
                this.IdQyteti, this.PerqindjeAgjentShitje, this.IdLlogari, this.IdNdermarje, this.IdKonfig, this.IdStatusDok, this.IdPerdoruesi, this.oColLidhjetAutorizim, this.IdPerdoruesMobile, this.idDrejtori);
                if (!mesazh.Status)
                {
                    data.rollbackTransaksion();
                    return mesazh;
                }
                this.IdAgjentShitje = id;
                DbKontabiliteti.clsDatabaseKontabilitet dbKont = new DbKontabiliteti.clsDatabaseKontabilitet(data);
                foreach (DbAdmin.clsLidhjeAutorizim o in oColLidhjetAutorizim)
                {
                    o.IdLloji = DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("AgjentShitje", dbKont);
                    o.IdLidhese = idAgjentShitje;
                    mesazh = data.ruajLidhjeAutorizim(o.IdLidhjeAutorizim, o.IdLidhese, o.IdLloji, o.IdAutorizimeKoka, 1);
                    if (!mesazh.Status)
                    {
                        data.rollbackTransaksion();
                        return mesazh;
                    }
                }

                data.commitTransaksion();
                return mesazh;
            }
            catch (Exception)
            {
                data.rollbackTransaksion();
                return new clsMesazh(false, "Ndodhi nje gabim gjate ruajtjes!");
            }
        }

        /// <summary>
        /// Modifikon objektin e Agjentit te Shtijes ne databaze.
        /// </summary>
        public clsMesazh modifiko()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            clsMesazh u_modifikua;
            try
            {
                data.beginTransaksion();
                u_modifikua = data.modifikoAgjentShitje(this.IdAgjentShitje, this.KodiAgjentShitje, this.EmriAgjentShitje, this.MbiemriAgjentShitje, this.TelAgjentShitje,
                   this.FaxAgjentShitje, this.EmailAgjentShitje, this.IdQyteti, this.PerqindjeAgjentShitje, this.IdLlogari, this.IdNdermarje, this.IdKonfig, this.IdStatusDok, this.IdPerdoruesi, this.oColLidhjetAutorizim, this.IdPerdoruesMobile, this.idDrejtori);
                if (!u_modifikua.Status)
                {
                    data.rollbackTransaksion();
                    return u_modifikua;
                }
                DbAdmin.colLidhjetAutorizim colLidhjetAutorizim = new DbAdmin.colLidhjetAutorizim(this.idAgjentShitje, "AgjentShitje", data);
                DbKontabiliteti.clsDatabaseKontabilitet dbKont = new DbKontabiliteti.clsDatabaseKontabilitet(data);
                for (int i = 0; i < oColLidhjetAutorizim.Count; i++)
                {
                    if (u_modifikua.Status)
                    {
                        int idAutorizimKoka = oColLidhjetAutorizim[i].IdAutorizimeKoka;
                        if (idAutorizimKoka == -1)
                            continue;
                        oColLidhjetAutorizim[i].IdLloji = DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("AgjentShitje", dbKont);
                        oColLidhjetAutorizim[i].IdLidhese = idAgjentShitje;
                        clsLidhjeAutorizim lidhjeNjejte = colLidhjetAutorizim.Find(x => x.IdAutorizimeKoka == idAutorizimKoka);
                        if (lidhjeNjejte != null)
                        {   //i heqim nga collectioni autorizimet qe sjane ndryshuar sepse ne te do ngelen vetem autorizimet qe do te fshihen(vendosen status 2) 
                            colLidhjetAutorizim.Remove(lidhjeNjejte);
                            continue;
                        }
                        u_modifikua = data.ruajLidhjeAutorizim(oColLidhjetAutorizim[i].IdLidhjeAutorizim, oColLidhjetAutorizim[i].IdLidhese, oColLidhjetAutorizim[i].IdLloji, oColLidhjetAutorizim[i].IdAutorizimeKoka, 1);
                    }
                    else
                    {
                        data.rollbackTransaksion();
                        return u_modifikua;
                    }
                }
              
                u_modifikua = colLidhjetAutorizim.FshiLidhjeAutorizim(colLidhjetAutorizim, IdPerdoruesi, data);
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
                return new clsMesazh(false, "Ndodhi nje gabim gjate ruajtjes se te dhenave!");
            }
        }

        /// <summary>
        /// fshin objektin e Agjentit te Shtijes ne databaze.
        /// </summary>
        public clsMesazh fshi()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            clsMesazh u_fshi = data.fshiAgjentShitje(this.IdAgjentShitje);
            data.Dispose();
            return u_fshi;
        }

        /// <summary>
        /// fshin objektin e Agjentit te Shtijes ne databaze.
        /// </summary>
        public clsMesazh fshistatus()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            clsMesazh u_fshi = data.fshiAgjentShitjestatus(this.IdAgjentShitje, this.IdPerdoruesi);
            data.Dispose();
            return u_fshi;
        }

        /// <summary>
        /// Kontrollon nese ekziston ndonje agjent tjeter me kete kod.
        /// </summary>
        /// <param name="kodi">kodi</param>
        /// <param name="idnderm">id e ndermarrjes</param>
        /// <returns>Kthen true nese ekziston dhe false ne te kundert</returns>
        public static bool ekziston(string kodi, int idnderm)
        {
            clsDatabaseAdmin db = new clsDatabaseAdmin();
            bool ekz = ekziston(db, kodi, idnderm);
            db.Dispose();
            return ekz;
        }

        /// <summary>
        /// kthen id agjent shitje sipas kodit dhe ndermarrjes
        /// </summary>
        /// <param name="kodAgjenti"></param>
        /// <param name="idNdermarrje"></param>
        /// <returns></returns>
        public static int ktheIdAgjentShitje(string kodAgjenti, int idNdermarrje)
        {
            using (clsDatabaseAdmin db = new clsDatabaseAdmin())
            {
                return db.ktheIdAgjentShitjeSipasKodDheNdermarrje(kodAgjenti, idNdermarrje);
            }
        }
        public static bool ekziston(clsDatabaseAdmin db, string kodi, int idnderm)
        {
            bool ekziston = db.ekzistonAgjentShitjeMeKeteKod(kodi, idnderm);
            return ekziston;
        }

        public static clsAgjentShitje MerrAgjentShitjeSipasIdPerdoruesModile(int idNdermarrje, int idPeroruesMobile)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            clsAgjentShitje agjent = new clsAgjentShitje();

            agjent.mbushAgjentShitje(data.merrAgjentShitjeSipasIdPeroruesMobile(idNdermarrje, idPeroruesMobile));
            data.Dispose();
            return agjent;
        }
        public DataRow ktheAgjentShitjeSipasID(int idAgjenti)
        {
            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();
            DataRow row = dbAdmin.merrAgjentShitje(idAgjenti);
            dbAdmin.Dispose();
            return row;
        }

        public static double merrPerqindjeAgjentiEdheSipasKlientit(int idAgjent, int idKlient, int llojAgjenti)
        {
            using (clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin())
            {
                return dbAdmin.merrPerqindjeAgjentiEdheSipasKlientit(idAgjent, idKlient, llojAgjenti);
            }
        }

        #endregion

        #region Metoda Private

        private clsMesazh kontrolloAgjent(bool shtim, ResourceManager rm, CultureInfo ci)
        {
            if (kodiAgjentShitje == "")
                return new clsMesazh(false, "Plotesoni kodin e agjentit!");
            clsMesazh kontrollkodiAgjentShitje = clsFunksione.kontrolloKaraktereMeMesazh(kodiAgjentShitje, FusheKontrolli.Kodi, false);
            if (!kontrollkodiAgjentShitje.Status)
                return kontrollkodiAgjentShitje;

            if (emriAgjentShitje == "")
                return new clsMesazh(false, "Plotesoni emrin e agjentit!");
            clsMesazh kontrollemriAgjentShitje = clsFunksione.kontrolloKaraktereMeMesazh(emriAgjentShitje, FusheKontrolli.Emri, true);
            if (!kontrollemriAgjentShitje.Status)
                return kontrollemriAgjentShitje;

            if (perqindjeAgjentShitje < 0 || perqindjeAgjentShitje > 100)
                return new clsMesazh(false, "Perqindja duhet te jete nga 0 ne 100!");
            if (idQyteti != 0 && !clsQyteti.ekzistonQyteti(idQyteti))
                return new clsMesazh(false, "Ky qytet nuk ekziston!");
            if ((idLlogari != 0 && !DbKontabiliteti.clsLlogari.ekzistonLlogariSipasID(idLlogari)) || idLlogari == -1)
                return new clsMesazh(false, "Kjo llogari nuk ekziston!");
            if (IdPerdoruesMobile > 0 && IdPerdoruesMobile == idDrejtori)
                return new clsMesazh(false, "Nuk mund te zgjedhesh drejtor veten tende");
            if (oColLidhjetAutorizim.Count != 0)
            {
                foreach (DbAdmin.clsLidhjeAutorizim o in oColLidhjetAutorizim)
                {
                    if (o.IdAutorizimeKoka == -1)
                        return new clsMesazh(false, "Niveli i autorizimit nuk ekziston!");
                }
            }
            clsDatabaseAdmin db = new clsDatabaseAdmin();
            if (shtim && db.ekzistonAgjentShitjeMeKeteKod(kodiAgjentShitje, idNdermarje))
            {
                db.Dispose();
                return new clsMesazh(false, "Ekziston nje agjent me kete kod. Ju lutem shkruani nje kod tjeter!");
            }
            db.Dispose();
            return new clsMesazh(true, "Kontrollet e agjentit u kaluan me sukses");
        }

        #endregion

        #region Metoda Internal

        internal bool mbushAgjentShitje(DataRow dbDataRowAgjentShitje)
        {
            if (dbDataRowAgjentShitje != null)
            {
                try
                {
                    idAgjentShitje = int.Parse(dbDataRowAgjentShitje["IDAGJENTSHITJE"].ToString());
                    kodiAgjentShitje = dbDataRowAgjentShitje["KODIAGJENTSHITJE"].ToString();
                    emriAgjentShitje = dbDataRowAgjentShitje["EMRIAGJENTSHITJE"].ToString();
                    mbiemriAgjentShitje = dbDataRowAgjentShitje["MBIEMRIAGJENTSHITJE"].ToString();
                    telAgjentShitje = dbDataRowAgjentShitje["TELAGJENTSHITJE"].ToString();
                    faxAgjentShitje = dbDataRowAgjentShitje["FAXAGJENTSHITJE"].ToString();
                    emailAgjentShitje = dbDataRowAgjentShitje["EMAILAGJENTSHITJE"].ToString();
                    int.TryParse(dbDataRowAgjentShitje["IDQYTETI"].ToString(), out idQyteti);
                    perqindjeAgjentShitje = double.Parse(dbDataRowAgjentShitje["PERQINDJEAGJENTSHITJE"].ToString());
                    int.TryParse(dbDataRowAgjentShitje["IDLLOGARI"].ToString(), out idLlogari);
                    idNdermarje = int.Parse(dbDataRowAgjentShitje["IDNDERMARJE"].ToString());
                    idKonfig = int.Parse(dbDataRowAgjentShitje["IDKONFIG"].ToString());
                    int.TryParse(dbDataRowAgjentShitje["IDSTATUSDOK"].ToString(), out idStatusDok);
                    int.TryParse(dbDataRowAgjentShitje["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    DateTime.TryParse(dbDataRowAgjentShitje["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRowAgjentShitje["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    int.TryParse(dbDataRowAgjentShitje["IDPERDORUESMOBILE"].ToString(), out idPerdoruesMobile);
                    int.TryParse(dbDataRowAgjentShitje["IDDREJTORI"].ToString(), out idDrejtori);
                    return true;
                }
                catch (InvalidCastException)
                {
                    ImbLogger.LogErrorShitje("ERROR: Gabim gjate marrjes se Agjentit te shitjes nga db-ja");
                    throw new Exception("ERROR: Gabim gjate marrjes se Agjentit te shitjes nga db-ja");
                }
            }
            else
            {
                return false;
            }
        }

        public clsMesazh mbushAgjentShitje(clsAgjentShitje agjentShitje)
        {
            ImbLogger.LogTraceShitje("Filloi mbushja e mbushjes se agjent shitje!");
            IdAgjentShitje = agjentShitje.IdAgjentShitje;
            KodiAgjentShitje = agjentShitje.KodiAgjentShitje;
            EmriAgjentShitje = agjentShitje.EmriAgjentShitje;
            MbiemriAgjentShitje = agjentShitje.MbiemriAgjentShitje;
            TelAgjentShitje = agjentShitje.TelAgjentShitje;
            FaxAgjentShitje = agjentShitje.FaxAgjentShitje;
            EmailAgjentShitje = agjentShitje.EmailAgjentShitje;
            IdQyteti = agjentShitje.IdQyteti;
            PerqindjeAgjentShitje = agjentShitje.PerqindjeAgjentShitje;
            IdLlogari = agjentShitje.IdLlogari;
            IdNdermarje = agjentShitje.IdNdermarje;
            IdKonfig = agjentShitje.IdKonfig;
            IdStatusDok = agjentShitje.IdStatusDok;
            IdPerdoruesi = agjentShitje.IdPerdoruesi;
            DtKrijimi = agjentShitje.DtKrijimi;
            DtModifikimi = agjentShitje.DtModifikimi;
            IdPerdoruesMobile = agjentShitje.IdPerdoruesMobile;
            IdDrejtori = agjentShitje.IdDrejtori;
            ImbLogger.LogWarningShitje($"Mbushja e agjentit te shitjes {agjentShitje.kodiAgjentShitje} u krye me sukses!");
            return new clsMesazh(true, $"Mbushja e agjentit te shitjes {agjentShitje.kodiAgjentShitje} u krye me sukses!");
        }

        internal List<string> merrEmailAgjentesh()
        {
            using (clsDatabaseAdmin db = new clsDatabaseAdmin())
            {
                return db.merrEmaileDrejtoreshDheAgjentiShitje(IdAgjentShitje);
            }
        }

        #endregion


    }
}
