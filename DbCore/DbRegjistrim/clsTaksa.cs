using System;
using System.Data;
using AlphaWeb.Core.Interfaces.Data;
using AlphaWeb.Core.Interfaces.Localization;
using DbCore.DbAdmin;
using DbCore.DbKontabiliteti;
using DbCore.IMBUtils.Fiskalizimi.Controls;
using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne  taksat
    ///  (Te dhenat  merren nga tabela : T_TAKSAT)
    /// </summary>
    public class clsTaksa : IDataBase
    {
        #region Atributet
        public const int idTaksaPaTVSH = -1;
        public const int idTaksaPaTVSH2 = 0;
        public const string kodTaksaPaTVSH = "Pa TVSH";
        private int idTaksa;
        private string kodi;
        private string pershkrimi;
        private decimal norma;
        private int idNdermarrje;
        private string llogariD;
        private string llogariK;
        private int idLlojTakse;
        private string njesia;
        private string tipiIPerjashtimit;

        private int idPerdoruesi;
        private string idNivelAutorizimi;
        private int idKonfig;
        private int idStatusDok;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private bool aktiv;
        private bool takseNdermarje;
        private string llogariDogane;
        private bool ePerjashtuar;
        private bool aplikoTvshNeFleteDoganore;
        private bool furnizimezero;
        private bool ShitjePaTvshTaksa;
        private DataRow rreshti;
        private IMessagesResource messages;
        #endregion

        #region Konstruktoret

        /// <summary>
        /// konstruktor me 2 parametra
        /// </summary>
        /// <param name="kodi">kodi i takses</param>
        /// <param name="idNderm">id e ndermarrjes</param>
        public clsTaksa(string kodi, int idNderm)
        {
            clsDatabaseRegjistrim dbTaksa = new clsDatabaseRegjistrim();
            dbTaksa.ktheTaksaSipasKodi(kodi, idNderm, this);
            dbTaksa.Dispose();
        }
        public clsTaksa(string kodi, int idNderm, clsDatabaseRegjistrim dbTaksa)
        {
            mbushTaksa(dbTaksa.TransCache.getTakse(kodi, idNderm, dbTaksa));
            //mbushTaksa(dbTaksa.ktheTaksaSipasKodi(kodi, idNderm));
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="id">id e takses</param>
        public clsTaksa(int id)
        {
            clsDatabaseRegjistrim dbTaksa = new clsDatabaseRegjistrim();
            this.MbushDataRow(dbTaksa.ktheTaksaSipasIdDataTable(id));
            dbTaksa.Dispose();
        }
        public clsTaksa(int id, clsDatabaseRegjistrim dbTaksa)
        {
            mbushTaksa(dbTaksa.TransCache.getTakse(id, dbTaksa));
        }

        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsTaksa()
        {
        }

        public clsTaksa(IMessagesResource messages)
        {
            this.messages = messages;
        }

        public clsTaksa(IDataRecord rreshti)
        {

            Mbush(rreshti);
        }
        public clsTaksa(DataRow rreshti)
        {

            MbushDataRow(rreshti);
        }

        public clsTaksa(IMessagesResource messages, int id, string kod, string pershk, decimal nrm, int nderm, string llogdeb, string llogkred, int idlloj, string njes, int idperdoruesi, int idkonfig, int idstatusdok, bool aktiv, string llogaridogane, bool ePerjashtuar, bool aplikoTvshNeFleteDoganore, bool takseNderm, string nivelAutorizimi, bool furnizimeZero, bool ShitjePaTvshTaksa, bool shtim, string lidhurNderfaqe, string tipiIPerjashtimit)
        {
            idTaksa = id;
            kodi = kod;
            pershkrimi = pershk;
            norma = nrm;
            idNdermarrje = nderm;
            idLlojTakse = idlloj;
            njesia = njes;
            idPerdoruesi = idperdoruesi;
            llogariD = llogdeb;
            llogariK = llogkred;
            idKonfig = idkonfig;
            idStatusDok = idstatusdok;
            this.ePerjashtuar = ePerjashtuar;
            this.aktiv = aktiv;
            this.llogariDogane = llogaridogane;
            this.takseNdermarje = takseNderm;
            this.aplikoTvshNeFleteDoganore = aplikoTvshNeFleteDoganore;
            this.furnizimezero = furnizimeZero;
            this.ShitjePaTvshTaksa = ShitjePaTvshTaksa;
            this.messages = messages;
            this.tipiIPerjashtimit = tipiIPerjashtimit;
            clsMesazh validoTakse = valido(shtim, lidhurNderfaqe);
            if (!validoTakse.Status)
            {
                throw new DbCore.MyException(validoTakse.PershkrimMesazhi);
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdTaksa
        {
            get { return idTaksa; }
            set { idTaksa = value; }
        }

        /// <summary>
        /// Kthen/Vendos kodit e takses.
        /// </summary>
        public string KodTaksa
        {
            get { return kodi; }
            set { kodi = value; }
        }

        /// <summary>
        /// Kthen/Vendos pershkrimin e takses.
        /// </summary>
        public string Pershkrimi
        {
            get { return pershkrimi; }
            set { pershkrimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos norma ne perqindje e takses.
        /// </summary>
        public decimal NormaPerqindje
        {
            get { return norma; }
            set { norma = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e ndermarjes.
        /// </summary>
        public int IdNdermarje
        {
            get { return idNdermarrje; }
            set { idNdermarrje = value; }
        }

        /// <summary>
        /// Kthen/Vendos llogarine debi.
        /// </summary>
        public string LlogariDebi
        {
            get { return llogariD; }
            set { llogariD = value; }
        }

        /// <summary>
        /// Kthen/Vendos llogarine kredi.
        /// </summary>
        public string LlogariKredi
        {
            get { return llogariK; }
            set { llogariK = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e llojit te takses.
        /// </summary>
        /// <example>1-Nivel tvsh, 2-tvsh,3-takse doganore</example>
        public int IdLlojTakse
        {
            get { return idLlojTakse; }
            set { idLlojTakse = value; }
        }

        /// <summary>
        /// Kthen/Vendos njesine.
        /// </summary>
        /// <example> vlere/perqindje</example>
        public string Njesia
        {
            get { return njesia; }
            set { njesia = value; }
        }


        public bool TakseNdermarje
        {
            get
            {
                return takseNdermarje;
            }
            set
            {
                takseNdermarje = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e perdoruesit.
        /// </summary>
        public int IdPerdoruesi
        {
            get { return idPerdoruesi; }
            set { idPerdoruesi = value; }
        }

        /// <summary>
        /// Kthen/Vendos nivelet e autorizimeve
        /// </summary>
        public string IdNivelAutorizimi
        {
            get
            {
                return idNivelAutorizimi;
            }
            set
            {
                this.idNivelAutorizimi = value;
            }

        }
        /// <summary>
        /// Kthen/Vendos ID-ne e konfigurimi.
        /// </summary>
        public int IdKonfig
        {
            get { return idKonfig; }
            set { idKonfig = value; }
        }
        public int IdStatusDok
        {
            get { return idStatusDok; }
            set { idStatusDok = value; }
        }
        public DateTime DtKrijimi
        {
            get { return dtKrijimi; }

        }
        public DateTime DtModifikimi
        {
            get { return dtModifikimi; }

        }
        public bool Aktiv { get { return aktiv; } set { aktiv = value; } }

        public bool ShitjePaTVSHTaksa { get { return ShitjePaTvshTaksa; } set { ShitjePaTvshTaksa = value; } }

        public string LlogariDogane
        {
            get { return llogariDogane; }
            set { llogariDogane = value; }

        }

        /// <summary>
        /// Kthen/Vendos nese taksa duhet te perjashtohet nga deklarimi te librat e blerjes/shitjes apo jo
        /// </summary>
        public bool EPerjashtuar
        {
            get { return ePerjashtuar; }
            set { ePerjashtuar = value; }
        }

        /// <summary>
        /// Kthen/Vendos nese takses i sugjerohet aplikimi i tvsh ne flete doganore apo jo
        /// </summary>
        public bool AplikoTvshNeFleteDoganore
        {
            get { return aplikoTvshNeFleteDoganore; }
            set { aplikoTvshNeFleteDoganore = value; }
        }

        public bool FurnizimeZero
        {
            get { return furnizimezero; }
            set { furnizimezero = value; }
        }
        public string TipiIPerjashtimit
        {
            get { return tipiIPerjashtimit; }
            set { tipiIPerjashtimit = value; }
        }



        #endregion

        #region Metoda Publike

        public clsMesazh valido(bool shtim, string lidhurNderfaqe)
        {
            clsMesazh valido = new clsMesazh(true);

            valido = validoEkzistonTakseMeKeteKod(shtim);
            if (!valido.Status)
                return valido;

            valido = validoTakseNdermAktive();
            if (!valido.Status)
                return valido;

            valido = validoTakseNdermMeLlojTre();
            if (!valido.Status)
                return valido;

            valido = validoLlogari(this.llogariD, "msgLlogDebiNukEkziston", "msgLlogDebiJoAktive");
            if (!valido.Status)
                return valido;

            valido = validoLlogari(this.llogariK, "msgLlogKrediNukEkziston", "msgLlogKrediJoAktive");
            if (!valido.Status)
                return valido;

            valido = validoLlogari(this.llogariDogane, "msgLlogariaDoganeNukEkziston", "msgLlogariaDoganeNukEshteAktive");
            if (!valido.Status)
                return valido;

            valido = validoTaksePaTvshMeLlojNje(messages["txtPaTVSH"]);
            if (!valido.Status)
                return valido;

            valido = validoNeseKaTakseTjeterNdermarrjeje();
            if (!valido.Status)
                return valido;

            valido = validoFurnizimeZero();
            if (!valido.Status)
                return valido;
            valido = validoEshteTaksaLidhur(shtim, lidhurNderfaqe);
            if (!valido.Status)
                return valido;

            return valido;
        }

        public clsMesazh validoLlogari(string nrLlogarie, string mesazhLlogNukEkziston, string mesazhLlogJoAktive)
        {
            if (!String.IsNullOrEmpty(nrLlogarie))
            {
                clsLlogari llogaria = new clsLlogari(nrLlogarie, this.idNdermarrje);
                if (llogaria.IdLlogari == 0)
                    return new clsMesazh(false, messages[mesazhLlogNukEkziston]);

                if (!llogaria.Aktiv)
                    return new clsMesazh(false, messages[mesazhLlogJoAktive]);
            }
            return new clsMesazh(true, "Kontrollet e llogarive u kaluan me sukses!");
        }

        public clsMesazh validoFurnizimeZero()
        {
            if (this.furnizimezero && (this.njesia == "Vlere" || this.norma != 0 || this.ePerjashtuar))
                return new clsMesazh(false, messages["msgTaksaMeFurnizimZeroEPasakte"]);
            return new clsMesazh(true, "Kontrollet e furnizime zero u kaluan me sukses!");
        }

        public clsMesazh validoTakseNdermAktive()
        {
            if (this.takseNdermarje && !this.Aktiv)
                return new clsMesazh(false, messages["msgTaksaNuMundTeCaktoni1NivelTVSHJoAktive"]);
            return new clsMesazh(true, "Kontrolli per takse ndermarrje aktive u kalua me sukses!");
        }

        public clsMesazh validoTakseNdermMeLlojTre()
        {
            if (this.takseNdermarje && this.idLlojTakse == 3)
                return new clsMesazh(false, messages["msgTaksaNukMundTeZgjidhniTaxDogSiNivelTVSHNderm"]);
            return new clsMesazh(true, "Kontrolli per takse ndermarrje me lloj tre u kalua me sukses!");
        }

        public clsMesazh validoTaksePaTvshMeLlojNje(string textMePaTvsh)
        {
            if (this.kodi.Equals(textMePaTvsh, StringComparison.InvariantCultureIgnoreCase) && this.idLlojTakse == 1)
                return new clsMesazh(false, messages["msgKodiPaTvsh"]);
            return new clsMesazh(true, "Kontrolli per takse pa tvsh me lloj nje u kalua me sukses!");
        }

        public clsMesazh validoNeseKaTakseTjeterNdermarrjeje()
        {
            if (this.takseNdermarje)
            {
                DbCore.DbRegjistrim.clsTaksa taksenderm = new DbCore.DbRegjistrim.clsTaksa();
                taksenderm.mbushTakseDefaultNdermarrje(this.idNdermarrje);
                if (taksenderm.idTaksa != 0 && taksenderm.IdTaksa != this.idTaksa)
                    return new clsMesazh(false, messages["msgTaksaEkziston1TaxPErKeteNdermarrje"]);
            }
            return new clsMesazh(true, "Kontrolli nese ka tjeter takse ndermarrjeje u kalua me sukses!");
        }

        public clsMesazh validoEkzistonTakseMeKeteKod(bool shtim)
        {
            using (clsDatabaseRegjistrim dbRegjistrime = new clsDatabaseRegjistrim())
            {
                if (dbRegjistrime.ekzistonTaksa(this.kodi, this.idNdermarrje) && shtim)
                {
                    return new clsMesazh(false, messages["msgTaksaEkzistonTaksaMeKeteKod"]);
                }
            }
            return new clsMesazh(true, "Kontrolli nese ka tjeter takse me kete kod u kalua me sukses!");
        }

        public clsMesazh validoEshteTaksaLidhur(bool shtim, string lidhurNderfaqe)
        {
            if (shtim)
                return new clsMesazh(true, "Kontrolli nese taksa eshte e lidhur ne modifikim u kalua me sukses!");

            using (clsDatabaseRegjistrim dbRegjistrim = new clsDatabaseRegjistrim())
            {
                int idNivel = DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdNivel(this.idKonfig);
                bool lidhur = dbRegjistrim.eshteDokumentiILidhurCelje(this.idTaksa.ToString(), idNivel.ToString());
                if (lidhur.ToString() != lidhurNderfaqe)
                    return new clsMesazh(false, messages["msgTaksaEshteElidhur"]);
                else
                    return new clsMesazh(true, "Kontrolli nese taksa eshte e lidhur ne modifikim u kalua me sukses!");
            }
        }

        /// <summary>
        /// kthen idTaksen ne baze te kodit dhe ndermarrjes
        /// </summary>
        /// <param name="kodi"></param>
        /// <param name="idNdermarrje"></param>
        /// <returns></returns>
        public static int ktheIdTakse(string kodi, int idNdermarrje)
        {
            using (clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim())
            {
                return dbRegj.ktheIdTaksaSipasKodit(kodi, idNdermarrje);
            }
        }

        /// <summary>
        /// Ekzekuton nje transaksion per te fshire nje objekt clsTaksa dhe autorizimet.
        /// <param name="id"> id ritese e taksave</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te transaksionit (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        public clsMesazh fshiTaksa(int id)
        {
            DbAdmin.colLidhjetAutorizim colLidhjeAutorizim = new DbAdmin.colLidhjetAutorizim(id, "Taksa");
            clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim();
            dbRegj.beginTransaksion();
            clsMesazh mesazh = new clsMesazh();
            DbCore.clsMesazh mesazhAdmin = new DbCore.clsMesazh(true);
            try
            {
                clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin(dbRegj);

                foreach (DbAdmin.clsLidhjeAutorizim o in colLidhjeAutorizim)
                {
                    if (mesazhAdmin.Status)
                        mesazhAdmin = dbAdmin.fshiLidhjeAutorizim(o.IdLidhjeAutorizim);
                    else
                    {
                        dbRegj.rollbackTransaksion();

                        mesazh.Status = false;
                        mesazh.PershkrimMesazhi = mesazhAdmin.PershkrimMesazhi;
                        return mesazh;
                    }
                }
                if (mesazhAdmin.Status)
                {
                    mesazh = dbRegj.fshiTaks(id);
                    if (mesazh.Status && mesazhAdmin.Status)
                    {
                        dbRegj.commitTransaksion();

                        mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
                        return mesazh;
                    }
                    else
                    {
                        dbRegj.rollbackTransaksion();

                        return mesazh;
                    }
                }
                else
                {
                    dbRegj.rollbackTransaksion();

                    mesazh.Status = false;
                    mesazh.PershkrimMesazhi = mesazhAdmin.PershkrimMesazhi;
                    return mesazh;
                }
            }
            catch (Exception ce)
            {
                dbRegj.rollbackTransaksion();

                return new clsMesazh(false, ce.Message);
            }
        }

        public clsMesazh kontrollotransferim(clsTaksa kod, int idndermarje, clsDatabaseRegjistrim db, int idperdoruesi)
        {
            ImbLogger.LogTraceShitje("Transferimi mbaroi me sukses!");
            clsMesazh mesazh = new clsMesazh(true, "Transferimi mbaroi me sukses!");
            if (!db.ekzistonTaksa(kod.kodi, idndermarje))
            {

                DbShare.clsKonfigurimAmbjenti konf = new DbShare.clsKonfigurimAmbjenti();
                DbShare.clsDatabaseShare dbshare = new DbShare.clsDatabaseShare(db);

                konf.mbushKonfigAmbjSipasKod("TA", idndermarje, dbshare);
                kod.idKonfig = konf.IdKonfigAmbjente;
                kod.idPerdoruesi = idperdoruesi;
                kod.IdNdermarje = idndermarje;
                mesazh = kod.ruaj(db);
                if (!mesazh.Status)
                    return mesazh;

            }
            else
            {
                clsTaksa kodnderm = new clsTaksa(kod.kodi, idndermarje, db);
                kod.IdTaksa = kodnderm.IdTaksa;
                if (kodnderm.dtModifikimi < kod.dtModifikimi)
                {
                    kod.idPerdoruesi = idperdoruesi;
                    kod.IdNdermarje = idndermarje;

                    kod.idKonfig = kodnderm.idKonfig;
                    mesazh = kod.modifiko(db);
                    if (!mesazh.Status)
                        return mesazh;
                }

            }
            return mesazh;
        }


        /// <summary>
        /// Ekzekuton nje transaksion per te ruajtur nje objekt clsTaksa dhe autorizimet.
        /// <param name="id"> id ritese e taksave</param>
        /// <param name="kod"> kodi i takses</param>
        /// <param name="idNdermarrje"> id e ndermarjes</param>
        /// <param name="nrm">norma e takses</param>
        /// <param name="pershk">pershkrimi i takses</param>
        /// <param name="dtfillimi">data e fillimit</param>
        /// <param name="dtmbarimi"> data e mbarrimit</param>
        /// <param name="idlloj"> id llojit te takses</param>
        /// <param name="idperdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="njes">njesia (vlere/perqindje)</param>
        /// <param name="llogdeb">llogaria ne debi</param>
        /// <param name="llogkred">llogaria ne kredi</param>
        /// <param name="nivelAutorizim">niveli i autorizimit</param>
        ///Therret funksionin <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.ruajTaks"/>
        ///Therret funksionin <see cref="DbAdmin.clsDatabaseAdmin.ruajLidhjeAutorizim"/>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te transaksionit (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        public clsMesazh ruajTaksa(int id, string kod, string pershk, decimal nrm, int idNdermarrje, string llogdeb, string llogkred, int idlloj, string njes, int idperdoruesi, string nivelAutorizim, int idkonfig, int idstatusdok, bool aktiv, bool taksenderamrje, string llogaridogane, bool ePerjashtuar, bool aplikoTvshNeFleteDoganore, clsDatabaseRegjistrim dbRegj, bool furnizimezero, bool ShitjePaTvshTaksa, string tipiIPerjashtimit)
        {
            bool statusVeprimi;
            clsMesazh mesazh;

            mesazh = dbRegj.ruajTaks(out id, kod, pershk, nrm, idNdermarrje, llogdeb, llogkred, idlloj, njes, idperdoruesi, idkonfig, idstatusdok, aktiv, llogariDogane, ePerjashtuar, aplikoTvshNeFleteDoganore, furnizimezero, ShitjePaTvshTaksa, tipiIPerjashtimit, clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizimV4());
            statusVeprimi = mesazh.Status;
            this.IdTaksa = id;
            if (!mesazh.Status)
                return mesazh;
            DbAdmin.clsDatabaseAdmin dataAdmin = new DbAdmin.clsDatabaseAdmin(dbRegj);
            clsDatabaseKontabilitet dbkont = new clsDatabaseKontabilitet(dbRegj);
            if (taksenderamrje == true)
            {
                clsNdermarrje ndermarje = new clsNdermarrje(idNdermarrje, dataAdmin);
                ndermarje.IdTakse = id;
                mesazh = ndermarje.modifikoNderm(dataAdmin);

                if (!mesazh.Status)
                {

                    return mesazh;
                }
            }
            if (!string.IsNullOrEmpty(nivelAutorizim))
            {
                DbAdmin.colLidhjetAutorizim colLidhjet = new DbAdmin.colLidhjetAutorizim();
                string[] pars1 = nivelAutorizim.Split(',');
                for (int i = 0; i < pars1.Length; i++)
                {
                    DbAdmin.clsLidhjeAutorizim lidhje = new DbAdmin.clsLidhjeAutorizim();
                    lidhje.IdAutorizimeKoka = DbAdmin.clsAutorizimKoka.ktheIDAutorizim(pars1[i], dataAdmin);
                    //lidhje.IdAutorizimeKoka = new DbAdmin.clsDatabaseAdmin().ktheAutorizim(pars1[i])[0].IdAutorizimKoka;
                    colLidhjet.Add(lidhje);
                }
                foreach (DbAdmin.clsLidhjeAutorizim o in colLidhjet)
                {

                    o.IdLloji = DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("Taksa", dbkont);
                    o.IdLidhese = id;
                    mesazh = dataAdmin.ruajLidhjeAutorizim(o.IdLidhjeAutorizim, o.IdLidhese, o.IdLloji, o.IdAutorizimeKoka, 1);
                    if (!mesazh.Status)
                    {

                        return mesazh;
                    }
                }
            }

            return mesazh;

        }

        /// <summary>
        /// Ekzekuton nje transaksion per te modifikuar nje objekt clsTaksa dhe autorizimet.
        /// <param name="id"> id ritese e taksave</param>
        /// <param name="kod"> kodi i takses</param>
        /// <param name="nderm"> id e ndermarjes</param>
        /// <param name="nrm">norma e takses</param>
        /// <param name="pershk">pershkrimi i takses</param>
        /// <param name="dtfillimi">data e fillimit</param>
        /// <param name="dtmbarimi"> data e mbarrimit</param>
        /// <param name="idlloj"> id llojit te takses</param>
        /// <param name="idperdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="njes">njesia (vlere/perqindje)</param>
        /// <param name="llogdeb">llogaria ne debi</param>
        /// <param name="llogkred">llogaria ne kredi</param>
        /// <param name="nivelAutorizim">niveli i autorizimit</param>
        ///Therret funksionin <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.modifikoTaks"/>
        ///Therret funksionin <see cref="DbAdmin.clsDatabaseAdmin.ruajLidhjeAutorizim"/>, <see cref="DbAdmin.clsDatabaseAdmin.modifikoLidhjeAutorizim"/>, <see cref="DbAdmin.clsDatabaseAdmin.fshiLidhjeAutorizim"/> sipas rastit nese jane shtuar apo hequr rreshta
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te transaksionit (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        public clsMesazh modifikoTaksa(int id, string kod, string pershk, decimal nrm, int nderm, string llogdeb, string llogkred, int idlloj, string njes, int idperdoruesi, string nivelAutorizim, int idkonfig, int idstatusdok, bool aktiv, bool taksendermarje, string llogaridogane, bool ePerjashtuar, bool aplikoTvshNeFleteDoganore, clsDatabaseRegjistrim dbRegj, bool furnizimezero, bool ShitjePaTvshTaksa, string tipiIPerjashtimit)
        {

            //clsDatabaseKontabilitet db = new clsDatabaseKontabilitet();
            //colLlojeBuxhetesh colLloj = db.merrLlojBuxhetiSipasKodit("Taksa");
            clsDatabaseAdmin data = new clsDatabaseAdmin(dbRegj);
            clsDatabaseKontabilitet dbkont = new clsDatabaseKontabilitet(dbRegj);
            DbAdmin.colLidhjetAutorizim colLidhjetAutorizim = new DbAdmin.colLidhjetAutorizim(id, "Taksa", data);


            clsMesazh mesazh = new clsMesazh();
            DbCore.clsMesazh mesazhAdmin = new DbCore.clsMesazh(true);
            mesazh = dbRegj.modifikoTaks(id, kod, pershk, nrm, nderm, llogdeb, llogkred, idlloj, njes, idperdoruesi, idkonfig, idstatusdok, aktiv, llogaridogane, ePerjashtuar, aplikoTvshNeFleteDoganore, furnizimezero, ShitjePaTvshTaksa, tipiIPerjashtimit, clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizimV4());
            if (mesazh.Status)
            {
                if (taksendermarje == true)
                {
                    clsNdermarrje ndermarje = new clsNdermarrje(nderm, data);
                    ndermarje.IdTakse = id;
                    mesazhAdmin = ndermarje.modifikoNderm(data);
                }
                else
                {
                    clsNdermarrje ndermarje = new clsNdermarrje(nderm, data);
                    if (ndermarje.IdTakse == id)
                    {
                        ndermarje.IdTakse = 0;
                        mesazhAdmin = ndermarje.modifikoNderm(data);
                    }
                }
                if (mesazhAdmin.Status)
                {

                    DbAdmin.colLidhjetAutorizim colLidhjet = new DbAdmin.colLidhjetAutorizim();
                    if (!string.IsNullOrEmpty(nivelAutorizim))
                    {
                        string[] pars1 = nivelAutorizim.Split(',');
                        for (int i = 0; i < pars1.Length; i++)
                        {
                            DbAdmin.clsLidhjeAutorizim lidhje = new DbAdmin.clsLidhjeAutorizim();
                            lidhje.IdAutorizimeKoka = DbAdmin.clsAutorizimKoka.ktheIDAutorizim(pars1[i], data);
                            //lidhje.IdAutorizimeKoka = new DbAdmin.clsDatabaseAdmin().ktheAutorizim(pars1[i])[0].IdAutorizimKoka;
                            colLidhjet.Add(lidhje);
                        }
                    }
                    mesazhAdmin = clsFunksione.modifikoLidhjeAutorizimSipasLlojitTeBuxhetit(colLidhjet, "Taksa", id, colLidhjetAutorizim, dbkont, data, false, IdPerdoruesi);
                }
                else
                {

                    mesazh.Status = false;
                    mesazh.PershkrimMesazhi = mesazhAdmin.PershkrimMesazhi;
                    return mesazh;
                }

                if (mesazhAdmin.Status && mesazh.Status)
                {

                    mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
                    return mesazh;
                }
                else
                {

                    mesazh.Status = false;
                    mesazh.PershkrimMesazhi = mesazhAdmin.PershkrimMesazhi;
                    return mesazh;
                }
            }
            else
            {

                return mesazh;
            }

        }

        /// <summary>
        /// Ruan objektin e  takses ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsTaksa.ruajTaksa"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
        public clsMesazh ruaj(clsDatabaseRegjistrim db)
        {
            clsMesazh u_ruajt = this.ruajTaksa(this.IdTaksa, this.KodTaksa, this.Pershkrimi, this.NormaPerqindje, this.IdNdermarje, this.LlogariDebi, this.LlogariKredi, this.IdLlojTakse, this.Njesia, this.IdPerdoruesi, this.IdNivelAutorizimi, this.IdKonfig, this.idStatusDok, this.Aktiv, this.takseNdermarje, this.llogariDogane, this.EPerjashtuar, this.AplikoTvshNeFleteDoganore, db, this.FurnizimeZero, this.ShitjePaTVSHTaksa, this.TipiIPerjashtimit);
            return u_ruajt;
        }

        public clsMesazh Ruaj()
        {
            clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();
            db.beginTransaksion();
            clsMesazh u_ruajt = ruaj(db);
            if (!u_ruajt.Status)
                db.rollbackTransaksion();
            else db.commitTransaksion();
            return u_ruajt;
        }

        /// <summary>
        /// Modifiko objektin e  takses ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsTaksa.modifikoTaksa"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo</returns>
        public clsMesazh modifiko(clsDatabaseRegjistrim db)
        {
            clsTaksa data = new clsTaksa();
            clsMesazh u_modifikua = data.modifikoTaksa(this.IdTaksa, this.KodTaksa, this.Pershkrimi, this.NormaPerqindje, this.IdNdermarje, this.LlogariDebi, this.LlogariKredi, this.IdLlojTakse, this.Njesia, this.IdPerdoruesi, this.IdNivelAutorizimi, this.IdKonfig, this.idStatusDok, this.aktiv, this.takseNdermarje, this.llogariDogane, this.EPerjashtuar, this.AplikoTvshNeFleteDoganore, db, this.FurnizimeZero, this.ShitjePaTvshTaksa, this.tipiIPerjashtimit);
            return u_modifikua;
        }
        public clsMesazh Modifiko()
        {
            clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();
            db.beginTransaksion();
            clsMesazh u_modifikua = modifiko(db);
            if (!u_modifikua.Status)
                db.rollbackTransaksion();
            else db.commitTransaksion();

            return u_modifikua;
        }

        /// <summary>
        /// fshin objektin e  takses ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsTaksa.fshiTaksa"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh Fshi()
        {
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            clsMesazh u_fshi = data.fshiTaksStatus(this.IdTaksa, this.idPerdoruesi);
            data.Dispose();
            return u_fshi;
        }

        /// <summary>
        /// merr gjithe taksat e nje ndermarje nga tabela perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.ktheGjitheTaksa"/> 
        /// </summary>
        /// <returns > nje objekt colTaksa me te gjithe taksat e nje ndermarje</returns>
        public colTaksa merrGjitheTaksa()
        {
            colTaksa data = new colTaksa(this.IdNdermarje, this.idPerdoruesi);
            return data;
        }

        public void mbushTakseDefaultNdermarrje(int idNdermarje)
        {
            using (clsDatabaseRegjistrim dbTaksa = new clsDatabaseRegjistrim())
                dbTaksa.ktheTakseDefaultNdermarrje(idNdermarje, this);
        }

        public static clsTaksa krijoTaksePaTVSH()
        {
            clsTaksa takseDef = new clsTaksa();
            takseDef.IdTaksa = clsTaksa.idTaksaPaTVSH;
            takseDef.KodTaksa = clsTaksa.kodTaksaPaTVSH;
            takseDef.norma = 0;
            return takseDef;
        }
        /// <summary>
        /// merr nje combo item per nivelin e tvsh sipas takses
        /// </summary>
        /// <param name="taksa"></param>
        /// <param name="caktuarParam"></param>
        /// <returns></returns>
        public static ComboListTvsh MerrComboItemTaksa(clsTaksa taksa, string caktuarParam)
        {

            var tvshPaTakse = new ComboListTvsh
            {

                value = taksa.IdTaksa,
                norma = taksa.NormaPerqindje,
                text = taksa.KodTaksa,
                caktuar = caktuarParam
            };
            return tvshPaTakse;
        }
        public static string ktheKodTakseMeId(int idTakse)
        {
            if (idTakse == clsTaksa.idTaksaPaTVSH || idTakse == clsTaksa.idTaksaPaTVSH2)
                return clsTaksa.kodTaksaPaTVSH;
            clsDatabaseRegjistrim dbTaksa = new clsDatabaseRegjistrim();
            string kodi = dbTaksa.kthekodTakseMeId(idTakse);
            dbTaksa.Dispose();
            return kodi;
        }
        public static bool ekziston(string kodi, int idndermarje)
        {
            clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();
            bool ekziston = db.ekzistonTaksa(kodi, idndermarje);
            db.Dispose();
            return ekziston;
        }

        public static decimal ktheNormePerqindjeMeId(int idTaksa)
        {
            using (clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim())
            {
                return ktheNormePerqindjeMeId(idTaksa, dbRegj);
            }
        }

        public static bool ktheAplikohetTVSHNeTakseApoJo(string kodTaksa, int idNderm)
        {
            using (clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim())
            {
                return ktheAplikohetTVSHNeTakseApoJo(kodTaksa, idNderm, dbRegj);
            }
        }
        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush taksen nga databaza
        /// </summary>
        /// <param name="dbDataRowTaksa">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public void Mbush(IDataRecord dbDataRowTaksa)
        {
            int.TryParse(dbDataRowTaksa["IDTAKSA"].ToString(), out idTaksa);
            kodi = dbDataRowTaksa["KODI"].ToString();
            pershkrimi = dbDataRowTaksa["PERSHKRIMI"].ToString();
            decimal.TryParse(dbDataRowTaksa["NORMAPERQINDJE"].ToString(), out norma);
            int.TryParse(dbDataRowTaksa["IDNDERM"].ToString(), out idNdermarrje);
            llogariD = dbDataRowTaksa["LLOGARID"].ToString();
            llogariK = dbDataRowTaksa["LLOGARIK"].ToString();
            int.TryParse(dbDataRowTaksa["IDLLOJTAKSE"].ToString(), out idLlojTakse);
            njesia = dbDataRowTaksa["NJESI"].ToString();
            int.TryParse(dbDataRowTaksa["IDPERDORUESI"].ToString(), out idPerdoruesi);
            int.TryParse(dbDataRowTaksa["IDKONFIG"].ToString(), out idKonfig);
            int.TryParse(dbDataRowTaksa["IDSTATUSDOK"].ToString(), out idStatusDok);
            DateTime.TryParse(dbDataRowTaksa["DTKRIJIMI"].ToString(), out dtKrijimi);
            DateTime.TryParse(dbDataRowTaksa["DTMODIFIKIMI"].ToString(), out dtModifikimi);
            bool.TryParse(dbDataRowTaksa["AKTIV"].ToString(), out aktiv);
            llogariDogane = dbDataRowTaksa["LLOGARIDOG"].ToString();
            bool.TryParse(dbDataRowTaksa["EPERJASHTUAR"].ToString(), out ePerjashtuar);
            bool.TryParse(dbDataRowTaksa["APLIKOTVSHNEFLETEDOGANORE"].ToString(), out aplikoTvshNeFleteDoganore);
            bool.TryParse(dbDataRowTaksa["FURNIZIMEZERO"].ToString(), out furnizimezero);
            bool.TryParse(dbDataRowTaksa["SHITJEPATVSHTAKSA"].ToString(), out ShitjePaTvshTaksa);
            if (clsKontrollePerFiskalizimin.checkIfColumnExists(dbDataRowTaksa, "TIPIIPERJASHTIMIT"))
                tipiIPerjashtimit = dbDataRowTaksa["TIPIIPERJASHTIMIT"].ToString();

        }

        public void MbushDataRow(DataRow dbDataRowTaksa)
        {
            if (dbDataRowTaksa != null)
            {
                int.TryParse(dbDataRowTaksa["IDTAKSA"].ToString(), out idTaksa);
                kodi = dbDataRowTaksa["KODI"].ToString();
                pershkrimi = dbDataRowTaksa["PERSHKRIMI"].ToString();
                decimal.TryParse(dbDataRowTaksa["NORMAPERQINDJE"].ToString(), out norma);
                int.TryParse(dbDataRowTaksa["IDNDERM"].ToString(), out idNdermarrje);
                llogariD = dbDataRowTaksa["LLOGARID"].ToString();
                llogariK = dbDataRowTaksa["LLOGARIK"].ToString();
                int.TryParse(dbDataRowTaksa["IDLLOJTAKSE"].ToString(), out idLlojTakse);
                njesia = dbDataRowTaksa["NJESI"].ToString();
                int.TryParse(dbDataRowTaksa["IDPERDORUESI"].ToString(), out idPerdoruesi);
                int.TryParse(dbDataRowTaksa["IDKONFIG"].ToString(), out idKonfig);
                int.TryParse(dbDataRowTaksa["IDSTATUSDOK"].ToString(), out idStatusDok);
                DateTime.TryParse(dbDataRowTaksa["DTKRIJIMI"].ToString(), out dtKrijimi);
                DateTime.TryParse(dbDataRowTaksa["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                bool.TryParse(dbDataRowTaksa["AKTIV"].ToString(), out aktiv);
                llogariDogane = dbDataRowTaksa["LLOGARIDOG"].ToString();
                bool.TryParse(dbDataRowTaksa["EPERJASHTUAR"].ToString(), out ePerjashtuar);
                bool.TryParse(dbDataRowTaksa["APLIKOTVSHNEFLETEDOGANORE"].ToString(), out aplikoTvshNeFleteDoganore);
                bool.TryParse(dbDataRowTaksa["FURNIZIMEZERO"].ToString(), out furnizimezero);
                bool.TryParse(dbDataRowTaksa["SHITJEPATVSHTAKSA"].ToString(), out ShitjePaTvshTaksa);
                if (dbDataRowTaksa.Table.Columns.Contains("TIPIIPERJASHTIMIT"))
                    tipiIPerjashtimit = dbDataRowTaksa["TIPIIPERJASHTIMIT"].ToString();
            }


        }

        internal void mbushTaksa(clsTaksa taksa)
        {
            ImbLogger.LogTraceShitje("Filloi metoda mbushTaksa!");
            idTaksa = taksa.idTaksa;
            kodi = taksa.kodi;
            pershkrimi = taksa.pershkrimi;
            norma = taksa.norma;
            idNdermarrje = taksa.idNdermarrje;
            llogariD = taksa.llogariD;
            llogariK = taksa.llogariK;
            idLlojTakse = taksa.idLlojTakse;
            njesia = taksa.njesia;

            idPerdoruesi = taksa.idPerdoruesi;
            idNivelAutorizimi = taksa.idNivelAutorizimi;
            idKonfig = taksa.idKonfig;
            idStatusDok = taksa.idStatusDok;
            dtKrijimi = taksa.dtKrijimi;
            dtModifikimi = taksa.dtModifikimi;
            aktiv = taksa.aktiv;
            takseNdermarje = taksa.takseNdermarje;
            llogariDogane = taksa.llogariDogane;
            ePerjashtuar = taksa.ePerjashtuar;
            aplikoTvshNeFleteDoganore = taksa.aplikoTvshNeFleteDoganore;
            furnizimezero = taksa.furnizimezero;
            ShitjePaTvshTaksa = taksa.ShitjePaTvshTaksa;
            rreshti = taksa.rreshti;
            tipiIPerjashtimit = taksa.tipiIPerjashtimit;
            ImbLogger.LogTraceShitje("Mbaroi metoda mbushTaksa!");
        }

        internal static decimal ktheNormePerqindjeMeId(int idTaksa, clsDatabaseRegjistrim dbRegj)
        {
            return dbRegj.ktheNormePerqindjeMeId(idTaksa);
        }

        internal static bool ktheAplikohetTVSHNeTakseApoJo(string kodTaksa, int idNderm, clsDatabaseRegjistrim dbRegj)
        {
            return dbRegj.ktheAplikohetTVSHNeTakseApoJo(kodTaksa, idNderm);
        }
        #endregion
    }
}