using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DbCore.DbKontabiliteti;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections;
using System.Globalization;
using DbCore.DbAdmin;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne nje etape
    ///  (Te dhenat  merren nga tabela : T_ETAPAAPROVIMI)
    /// </summary>

    public class clsEtapeAprovimi
    {


        #region Atribute

        private int idEtapa;
        private int idSkema;
        private string skema;
        private int idKokaShitje;
        private string kategoria;
        private string nenkategoria;
        private String lloji;
        private string nrDokumenti;
        private String monedha;
        private double vlefta;
        private double vleftaMonBaze;
        private int nrProcesi;
        private int etapa;
        private int idKrijuesi;
        private string krijuesi;
        private DateTime dtDergimi;
        private int idAprovuesi;
        private string aprovuesi;
        private int idModifikuesi;
        private string modifikuesi;
        private DateTime dtModifikimi;
        private DateTime dtKujtese;
        private int idNdermarje;
        private string komente;
        private string statusi;
        private double progresi;
        private int niveli;
        private int statusAprovimi;
        private int llojAprovuesi;
        private colKomenteAprovimi oColKomente;
        private string klienti;
        private int idKategoria;
        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases me parametra
        /// </summary>
        /// <param name="idndermarje"> id e ndermarjes</param>
        /// <param name="idaprovuesi"> id e aprovuesit</param>
        /// <param name="idkokashitje"> id e kokes se shitjes</param>
        /// <param name="dtdergimi"> dt e dergimit</param>
        /// <param name="idmodifikuesi"> id e modifikuesit</param>
        /// <param name="idkrijues"> id e krijuesit</param>
        /// <param name="nrprocesi"> nr i procesit</param>
        /// <param name="status"> statusi </param>
        /// <param name="progres"> progresi</param>
        /// <param name="idskema">id e skemes</param>
        /// <param name="idetapa">id e etapes</param>
        /// <param name="etap"> etapa</param>
        /// <param name="dtmodifkimi"> dtmodifkimi</param>
        /// <param name="dtkujtese"> dt kujtese </param>
        /// <param name="nivel">niveli i aprovimit</param>

        public clsEtapeAprovimi(int idetapa, int idskema, int idkokashitje, int nrprocesi, int etap, int idkrijues, DateTime dtdergimi, int idaprovuesi, int idmodifikuesi, DateTime dtmodifkimi, DateTime dtkujtese, int idndermarje, int status, int nivel, int llojaprovuesi, int idkategoria)
        {
            idEtapa = idetapa;
            idSkema = idskema;
            idKokaShitje = idkokashitje;
            nrProcesi = nrprocesi;
            etapa = etap;
            idKrijuesi = idkrijues;
            dtDergimi = dtdergimi;
            idAprovuesi = idaprovuesi;
            idModifikuesi = idmodifikuesi;
            dtModifikimi = dtmodifkimi;
            dtKujtese = dtkujtese;
            idNdermarje = idndermarje;
            this.idKategoria = idkategoria;
            statusAprovimi = status;
            llojAprovuesi = llojaprovuesi;
            this.niveli = nivel;

            oColKomente = new colKomenteAprovimi();
        }

        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsEtapeAprovimi()
        {
            oColKomente = new colKomenteAprovimi();
        }

        public clsEtapeAprovimi(int id, int idkategoria)
        {
            using (clsDatabaseRegjistrim db = new clsDatabaseRegjistrim())
            {
                mbushEtape(db.ktheEtapeSipasId(id, idkategoria));
            }
        }
        public clsEtapeAprovimi(int id, int idkategoria, clsDatabaseRegjistrim db)
        {

            mbushEtape(db.ktheEtapeSipasId(id, idkategoria));

        }

        public clsEtapeAprovimi(DataRow rreshti)
        {

            mbushEtape(rreshti);
        }
        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdEtapa
        {
            get { return idEtapa; }
            set { idEtapa = value; }
        }

        public int IdKategoria
        {
            get
            {
                return idKategoria;
            }
            set
            {
                idKategoria = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne  e skemes
        /// </summary>
        public int IdSkema
        {
            get { return idSkema; }
            set { idSkema = value; }
        }

        /// <summary>
        /// Kthen/Vendos skemen
        /// </summary>
        public string Skema
        {
            get { return skema; }
            set { skema = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne  e kokes se shitjes
        /// </summary>
        public int IdKokaShitje
        {
            get { return idKokaShitje; }
            set { idKokaShitje = value; }
        }
        /// <summary>
        /// Kthen/Vendos kategorine.
        /// </summary>
        public string Kategoria
        {
            get { return kategoria; }
            set { kategoria = value; }
        }

        /// <summary>
        /// Kthen/Vendos nenkategorine.
        /// </summary>
        public string Nenkategoria
        {
            get { return nenkategoria; }
            set { nenkategoria = value; }
        }

        /// <summary>
        /// Kthen/Vendos konfigurimin
        /// </summary>
        public String Lloji
        {
            get { return lloji; }
            set { lloji = value; }
        }

        /// <summary>
        /// Kthen/Vendos  monedhen.
        /// </summary>
        public String Monedha
        {
            get { return monedha; }
            set { monedha = value; }
        }

        /// <summary>
        /// Kthen/Vendos vleften.
        /// </summary>
        public double Vlefta
        {
            get { return vlefta; }
            set { vlefta = value; }
        }

        /// <summary>
        /// Kthen/Vendos  nr e dokumentit.
        /// </summary>
        public string NrDokumenti
        {
            get { return nrDokumenti; }
            set { nrDokumenti = value; }
        }

        /// <summary>
        /// Kthen/Vendos vleften ne monedhen baze
        /// </summary>
        public double VleftaMonBaze
        {
            get { return vleftaMonBaze; }
            set { vleftaMonBaze = value; }
        }

        /// <summary>
        /// Kthen/Vendos nr e procesit
        /// </summary>
        public int NrProcesi
        {
            get { return nrProcesi; }
            set { nrProcesi = value; }
        }

        /// <summary>
        /// Kthen/Vendos  etapen.
        /// </summary>
        public int Etapa
        {
            get { return etapa; }
            set { etapa = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne  e krijuesit te dokumentit
        /// </summary>
        public int IdKrijuesi
        {
            get { return idKrijuesi; }
            set { idKrijuesi = value; }
        }

        /// <summary>
        /// Kthen/Vendos krijuesin.
        /// </summary>
        public string Krijuesi
        {
            get { return krijuesi; }
            set { krijuesi = value; }
        }

        /// <summary>
        /// Kthen/Vendos date dergimit.
        /// </summary>
        public DateTime DtDergimi
        {
            get { return dtDergimi; }
            set { dtDergimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e aprovuesit.
        /// </summary>
        public int IdAprovuesi
        {
            get { return idAprovuesi; }
            set { idAprovuesi = value; }
        }

        /// <summary>
        /// Kthen/Vendos aprovuesin.
        /// </summary>
        public string Aprovuesi
        {
            get { return aprovuesi; }
            set { aprovuesi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e modifikuesit
        /// </summary>
        public int IdModifikuesi
        {
            get { return idModifikuesi; }
            set { idModifikuesi = value; }
        }

        /// <summary>
        /// Kthen/Vendos modifikuesi.
        /// </summary>
        public string Modifikuesi
        {
            get { return modifikuesi; }
            set { modifikuesi = value; }
        }

        /// <summary>
        /// Kthen/Vendos dt e modifikimit.
        /// </summary>
        public DateTime DtModifikimi
        {
            get { return dtModifikimi; }
            set { dtModifikimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos daten e kujteses.
        /// </summary>
        public DateTime DtKujtese
        {
            get { return dtKujtese; }
            set { dtKujtese = value; }
        }

        /// <summary>
        /// Kthen/Vendos idndermarje.
        /// </summary>
        public int IdNdermarje
        {
            get { return idNdermarje; }
            set { idNdermarje = value; }
        }

        /// <summary>
        /// Kthen/Vendos nese kjo etape ka komente apo jo
        /// </summary>
        public string Komente
        {
            get { return komente; }
            set { komente = value; }
        }
        /// <summary>
        /// statusi i aprovimit
        /// </summary>
        public string Statusi
        {
            get
            {
                return statusi;
            }
            set
            {
                statusi = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos progresin
        /// </summary>
        public double Progresi
        {
            get { return progresi; }
            set { progresi = value; }
        }

        /// <summary>
        /// statusi i aprovimit
        /// </summary>
        public int StatusAprovimi
        {
            get
            {
                return statusAprovimi;
            }
            set
            {
                statusAprovimi = value;
            }
        }
        /// <summary>
        /// kthen/vendos llojin e aprovuesit perdorues apo rol
        /// </summary>
        public int LlojAprovuesi
        {
            get
            {
                return llojAprovuesi;
            }
            set
            {
                llojAprovuesi = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos nivelin e aprovimit.
        /// </summary>
        public int Niveli
        {
            get { return niveli; }
            set
            {
                niveli = value;
            }
        }



        /// <summary>
        /// Kthen/Vendos nje kolekson me komentet e etapes.
        /// </summary>
        public colKomenteAprovimi OColKomente
        {
            get { return oColKomente; }
            set { oColKomente = value; }
        }


        /// <summary>
        /// Kthen Klientin
        /// </summary>
        public string Klienti
        {
            get { return klienti; }
            set { klienti = value; }
        }

        #endregion

        #region Metoda Publike


        /// <summary>
        /// ruan nje etape
        /// </summary>
        /// <returns></returns>
        /// <param name="idPerdorues"></param>
        /// <param name="serverUrl"></param>
        /// <param name="page"></param>
        public clsMesazh ruaj(int idPerdorues, string serverUrl, System.Web.UI.Page page, int skemaWorkFlow, int idkokapara, int idaprovuesi, DbRegjistrim.StatusAprovimi statusapp, int idetapa, int idkategoria, int idstatusdok, int idndermarje, StatusAprovimi statusAprovimi, int idklientfurnitor, int idkoka, int idperdoruesdok, double vlefta, DateTime dtkrijimi, int idkonfig, string nrdok, DateTime dtdok, int dite)
        {
            clsMesazh u_ruajt;
            using (var scope = new MyTransactionScope())
            {
                clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim();
                u_ruajt = this.ruaj(idPerdorues, serverUrl, skemaWorkFlow, dbRegj, idkokapara, idaprovuesi, statusapp, idetapa, idkategoria, idstatusdok, idndermarje, statusAprovimi, idklientfurnitor, idkoka, idperdoruesdok, vlefta, dtkrijimi, idkonfig, nrdok, dtdok, dite);

                if (!u_ruajt.Status) return u_ruajt;
                scope.Complete();
            }
            return u_ruajt;
        }
        public clsMesazh ruaj(int idPerdorues, string serverUrl, System.Web.UI.Page page, int skemaWorkFlow, int idkokapara, int idaprovuesi, DbRegjistrim.StatusAprovimi statusapp, int idetapa, int idkategoria, int idstatusdok, int idndermarje, StatusAprovimi statusAprovimi, int idklientfurnitor, int idkoka, int idperdoruesdok, double vlefta, DateTime dtkrijimi, int idkonfig, string nrdok, DateTime dtdok)
        {
            return ruaj(idPerdorues, serverUrl, page, skemaWorkFlow, idkokapara, idaprovuesi, statusapp, idetapa, idkategoria, idstatusdok, idndermarje, statusAprovimi, idklientfurnitor, idkoka, idperdoruesdok, vlefta, dtkrijimi, idkonfig, nrdok, dtdok, 0);
        }
        /// <summary>
        /// ruan nje etape
        /// </summary>
        /// <returns></returns>
        public clsMesazh ruaj(clsDatabaseRegjistrim dbRegj)
        {

            return this.ruajEtape(this, dbRegj);

        }

        /// <summary>
        /// ruan nje etape
        /// </summary>
        /// <param name="koka">etapa</param>
        /// <param name="dbRegj">clsdatabazeregjistrime</param>
        /// <returns>kthen mesazh per ruajtjen</returns>
        private clsMesazh ruajEtape(clsEtapeAprovimi koka, clsDatabaseRegjistrim dbRegj)
        {//transaksioni per te ruajtur 


            clsMesazh mesazh;

            koka.idEtapa = dbRegj.ruajEtape(koka.idEtapa, koka.idSkema, koka.idKokaShitje, koka.nrProcesi, koka.etapa, koka.idKrijuesi, koka.dtDergimi, koka.dtModifikimi, koka.idModifikuesi, koka.idAprovuesi, koka.dtKujtese, koka.statusAprovimi, koka.idNdermarje, koka.niveli, koka.llojAprovuesi, koka.idKategoria);


            if (koka.idEtapa == 0)
                return new clsMesazh(false, "Etapa nuk u ruajt");

            foreach (clsKomenteAprovimi koment in koka.oColKomente)
            {
                koment.IdEtape = koka.idEtapa;
                mesazh = koment.ruaj(dbRegj);
                if (!mesazh.Status)
                    return mesazh;
            }

            return new clsMesazh(true, "Ruajtja përfundoi me sukses!");

        }

        /// <summary>
        /// modifikon nje etape
        /// </summary>
        /// <returns></returns>
        public clsMesazh modifiko()
        {
            clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim();

            dbRegj.beginTransaksion();
            clsMesazh u_modifikua;


            u_modifikua = modifiko(dbRegj);
            if (!u_modifikua.Status)
            {
                dbRegj.rollbackTransaksion();
                return u_modifikua;
            }


            dbRegj.commitTransaksion();
            return u_modifikua;
        }
        /// <summary>
        /// modifikon nje etape
        /// </summary>
        /// <returns></returns>
        public clsMesazh modifiko(clsDatabaseRegjistrim dbRegj)
        {
            return modifikoEtape(this, dbRegj);

        }

        /// <summary>
        /// modifikon nje etape
        /// </summary>
        /// <param name="koka">koka</param>
        /// <param name="dbRegj">clsdatabaseregjistrime</param>
        /// <returns>kthen mesazh me statusin e modifikimit</returns>
        public clsMesazh modifikoEtape(clsEtapeAprovimi koka, clsDatabaseRegjistrim dbRegj)
        {

            return dbRegj.modifikoEtape(koka.idEtapa, koka.idSkema, koka.idKokaShitje, koka.nrProcesi, koka.etapa, koka.idKrijuesi, koka.dtDergimi, koka.dtModifikimi, koka.idModifikuesi, koka.idAprovuesi, koka.dtKujtese, koka.statusAprovimi, koka.idNdermarje, koka.niveli, koka.llojAprovuesi, koka.idKategoria);


        }
        public clsMesazh fshi(clsDatabaseRegjistrim db)
        {
            return fshiEtape(this, db);
        }

        public clsMesazh fshiEtape(clsEtapeAprovimi koka, clsDatabaseRegjistrim dbRegj)
        {

            clsMesazh mesazh;
            koka.oColKomente.merrKomenteSipasEtapes(koka.idEtapa, dbRegj);
            foreach (clsKomenteAprovimi koment in koka.oColKomente)
            {
                mesazh = koment.fshi(koment.IdKomenti, dbRegj);
                if (!mesazh.Status) return mesazh;
            }
            return dbRegj.fshiEtape(koka.idEtapa);
        }

        /// <summary>
        /// Merr objektin sipas kokashitjes dhe nr etape
        /// </summary>
        /// <returns > nje object colKokashitje me dokumentin e kerkuar</returns>
        public bool merrSipasKokaShitjeDheNrEtape(int idkokashitje, int nretape)
        {
            using (clsDatabaseRegjistrim db = new clsDatabaseRegjistrim())
            {
                return mbushEtape(db.ktheEtapeSipasKokaShitjeDheNrEtape(idkokashitje, nretape));
            }
        }
        public bool ktheEtapeFunditSipasKokaShitjeDhePerdorues(int idkokashitje, int idperdorues)
        {
            using (clsDatabaseRegjistrim db = new clsDatabaseRegjistrim())
            {
                return mbushEtape(db.ktheEtapeFunditSipasKokaShitjeDhePerdorues(idkokashitje, idperdorues));
            }
        }
        public bool ktheEtapeFunditSipasKokaListpagesaDhePerdorues(int idkoka, int idperdorues)
        {
            using (clsDatabaseRegjistrim db = new clsDatabaseRegjistrim())
            {
                return mbushEtape(db.ktheEtapeFunditSipasKokaListpagesaDhePerdorues(idkoka, idperdorues));
            }
        }

        public bool ktheEtapeFunditSipasKokaVeprimeBankaDhePerdorues(int idkoka, int idperdorues)
        {
            using (clsDatabaseRegjistrim db = new clsDatabaseRegjistrim())
            {
                return mbushEtape(db.ktheEtapeFunditSipasKokaVeprimeBankaDhePerdorues(idkoka, idperdorues));
            }
        }
        public bool ktheEtapeFunditSipasIdKokaBuxhetiDhePerdorues(int idkoka, int idperdorues)
        {
            using (clsDatabaseRegjistrim db = new clsDatabaseRegjistrim())
            {
                return mbushEtape(db.ktheEtapeFunditSipasIdKokaBuxhetiDhePerdorues(idkoka, idperdorues));
            }
        }
        /// <summary>
        /// kthen nr max te procesit
        /// </summary>
        /// <returns></returns>
        /// <param name="idNdermarrje"></param>
        public int merrMaxNrProcesi(int idNdermarrje)
        {
            using (clsDatabaseRegjistrim db = new clsDatabaseRegjistrim())
            {
                return db.merrNrProcesiMax(idNdermarrje);
            }

        }
        /// <summary>
        /// kthen nr max te etapes ne gjendje jo aprovim
        /// </summary>
        /// <returns></returns>
        /// <param name="idNdermarrje"></param>
        public int merrMaxNrEtape(int procesi, int idNdermarrje)
        {
            using (clsDatabaseRegjistrim db = new clsDatabaseRegjistrim())
            {
                return db.ktheNrMaxEtape(procesi, idNdermarrje);
            }

        }
        /// <summary>
        /// kthen nr e procesit sipas kokashitje
        /// </summary>
        /// <returns></returns>
        public static int merrNrProcesiSipasKokaShitje(int idkokashitje, int idkategoria, int idetapaaktuale, clsDatabaseRegjistrim db)
        {
            return db.ktheNrProcesiSipasKokaShitje(idkokashitje, idkategoria, idetapaaktuale);
        }

        public static int merrNrProcesiSipasKokaShitje(int idkokashitje, int idkategoria, int idetapaaktuale)
        {
            using (clsDatabaseRegjistrim db = new clsDatabaseRegjistrim())
            {
                return db.ktheNrProcesiSipasKokaShitje(idkokashitje, idkategoria, idetapaaktuale);
            }
        }


        internal clsMesazh ruaj(int idPerdorues, string serverUrl, int skemaWorkFlow, clsDatabaseRegjistrim dbRegj, int idkokapara, int idaprovuesi, DbRegjistrim.StatusAprovimi statusapp, int idetapaaktuale, int idkategoria, int idstatusdok, int idndermarje, StatusAprovimi statusAprovimi, int idklientfurnitor, int idkoka, int idperdoruesdok, double vlefta, DateTime dtkrijimi, int idkonfig, string nrdok, DateTime dtdok)
        {
            return ruaj(idPerdorues, serverUrl, skemaWorkFlow, dbRegj, idkokapara, idaprovuesi, statusapp, idetapaaktuale, idkategoria, idstatusdok, idndermarje, statusAprovimi, idklientfurnitor, idkoka, idperdoruesdok, vlefta, dtkrijimi, idkonfig, nrdok, dtdok, 0);
        }
        internal clsMesazh ruaj(int idPerdorues, string serverUrl, int skemaWorkFlow, clsDatabaseRegjistrim dbRegj, int idkokapara, int idaprovuesi, DbRegjistrim.StatusAprovimi statusapp, int idetapaaktuale, int idkategoria, int idstatusdok, int idndermarje, StatusAprovimi statusAprovimi, int idklientfurnitor, int idkoka, int idperdoruesdok, double vlefta, DateTime dtkrijimi, int idkonfig, string nrdok, DateTime dtdok, int dite)
        {
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            const int llojiperdorues = 1;
            clsMesazh mesazh = new clsMesazh();
            clsDatabaseAdmin dbadmin = new clsDatabaseAdmin(dbRegj);
            if (skemaWorkFlow == 0)
                return new clsMesazh(true, MessagesResource.Messages["msgRuajtjeMeSukses"]);

            clsKokaSkemaWorkFlow kokaskema = new clsKokaSkemaWorkFlow(skemaWorkFlow, dbadmin);
            colEtapeAprovimi etapatEReja = new colEtapeAprovimi();
            DateTime dtdergimi = DateTime.Now;
            DateTime dtkujtese = new DateTime();
            if (kokaskema.Formula == Convert.ToInt32(DbAdmin.Formula.Dite))
                dtkujtese = dtdergimi.AddDays(kokaskema.Nr);
            if (kokaskema.Formula == Convert.ToInt32(DbAdmin.Formula.Jave))
                dtkujtese = dtdergimi.AddDays(kokaskema.Nr * 7);
            if (kokaskema.Formula == Convert.ToInt32(DbAdmin.Formula.Muaj))
                dtkujtese = dtdergimi.AddMonths(kokaskema.Nr);
            colTrupiSkemaWorkFlow trupiskema = new colTrupiSkemaWorkFlow();
            trupiskema.mbushTrupatSkemeWorkFlowSipasKokes(skemaWorkFlow, dbadmin);

            DbKontabiliteti.clsDatabaseKontabilitet dbkont = new clsDatabaseKontabilitet(dbRegj);
            int nrprocesi = merrNrProcesiSipasKokaShitje(idkokapara, idkategoria, idetapaaktuale, dbRegj);
            DbCore.DbAdmin.clsTrupiSkemaWorkFlow tt = new DbCore.DbAdmin.clsTrupiSkemaWorkFlow();
            if (idetapaaktuale == 0)
                tt.merrTrupSipasKokesDhePerdoruesit(skemaWorkFlow, idaprovuesi, dbadmin);

            else
            {
                clsEtapeAprovimi etapaakt = new clsEtapeAprovimi(idetapaaktuale, idkategoria, dbRegj);
                tt.merrTrupSipasKokesDhePerdoruesitDheNivelit(skemaWorkFlow, idaprovuesi, etapaakt.niveli, dbadmin);
                //     delegim = dbRegj.eshteDeleguar(idndermarje, nrprocesi,etapaakt.Niveli);
                //if (delegim)
                //    tt.merrTrupSipasKokesDheDeleguesi(skemaWorkFlow, idaprovuesi, dbadm);
            }



            int nretape = 0;
            bool procesiRi = false;
            if (nrprocesi == 0)
            {
                nrprocesi = dbRegj.merrNrProcesiMax(idndermarje) + 1;
                procesiRi = true;
            }
            nretape = dbRegj.ktheNrMaxEtape(nrprocesi, idndermarje) + 1;
            int nivelitjeter = tt.Niveli;
            List<int> listaParalelRefuzim = new List<int>();
            switch (statusapp)
            {
                case DbRegjistrim.StatusAprovimi.Per_Aprovim:
                    statusAprovimi = DbRegjistrim.StatusAprovimi.Per_Aprovim;
                    bool klientGrupi = EshteKlientGrupi(idndermarje, idklientfurnitor, dbkont, tt);
                    if (procesiRi)
                    {

                        #region procesi i ri
                        clsEtapeAprovimi etape = new clsEtapeAprovimi(0, skemaWorkFlow, idkoka, nrprocesi, nretape, idperdoruesdok, dtdergimi, idaprovuesi, idperdoruesdok, dtdergimi, dtkujtese, idndermarje, Convert.ToInt32(DbRegjistrim.StatusAprovimi.Aprovuar), tt.Niveli, llojiperdorues, idkategoria);//aprovon perdoruesi qe po kryen veprimin

                        etapatEReja.Add(etape);


                        //nuk ka limit por ka grup dhe plotesohet grupi ose ka limit dhe (nuk ka grup ose ka grup dhe plotesohet kushti) koka.Totali - koka.Zbritje - koka.Tvsh
                        if ((tt.VleraLimit == 0 && (tt.ColGrupeKF.Count > 0 && klientGrupi)) || ((tt.VleraLimit != 0 && tt.VleraLimit >= vlefta) && (tt.ColGrupeKF.Count == 0 || (tt.ColGrupeKF.Count > 0 && klientGrupi))) || (tt.Dite != 0 && dite < tt.Dite)) //nese vlera limit ne skeme eshte zero nuk behet aprovim limiti
                        {
                            if (tt.NiveliApr == 0)//nqs eshte aprovuar aprovon direkt prn shkon tek niveli i percaktuar
                                statusAprovimi = (DbRegjistrim.StatusAprovimi.Aprovuar);// me i vogel se limiti dokumenti aprovohet por duhet para nqs ka perdorues te tjere ne kete nivel
                            else nivelitjeter = tt.NiveliApr;

                        }
                        bool kaNeNivel = false;
                        foreach (DbAdmin.clsTrupiSkemaWorkFlow trup in trupiskema)
                        {
                            if (trup.Niveli < tt.Niveli)
                                continue;
                            if (trup.Niveli == tt.Niveli && !(tt.IdPerdRol == trup.IdPerdRol && tt.Lloji == trup.Lloji))
                            {
                                nretape++;
                                clsEtapeAprovimi etape1 = new clsEtapeAprovimi(0, skemaWorkFlow, idkoka, nrprocesi, nretape, idperdoruesdok, dtdergimi, trup.IdPerdRol, idperdoruesdok, dtdergimi, dtkujtese, idndermarje, Convert.ToInt32(DbRegjistrim.StatusAprovimi.Per_Aprovim), trup.Niveli, trup.Lloji, idkategoria);// shkon per aprovim per perdoruesit e tjere te te njejtit nivel                                    
                                nivelitjeter = 0;
                                etapatEReja.Add(etape1);
                                kaNeNivel = true;
                                statusAprovimi = (DbRegjistrim.StatusAprovimi.Per_Aprovim);// duhet te aprovohet edhe nga perdoruesit e tjere te ketij niveli

                            }

                            if (trup.Niveli > tt.Niveli)
                            {
                                if (kaNeNivel)//nqs ka ne kete nivel nderprite
                                    break;
                                if (statusAprovimi == (DbRegjistrim.StatusAprovimi.Aprovuar))//nderprit ciklin sepse dokumenti u aprovua
                                    break;


                                if (nivelitjeter == tt.Niveli)
                                    nivelitjeter = trup.Niveli;

                                if (trup.Niveli == nivelitjeter)
                                {
                                    nretape++;
                                    clsEtapeAprovimi etape1 = new clsEtapeAprovimi(0, skemaWorkFlow, idkoka, nrprocesi, nretape, idperdoruesdok, dtdergimi, trup.IdPerdRol, idperdoruesdok, dtdergimi, dtkujtese, idndermarje, Convert.ToInt32(DbRegjistrim.StatusAprovimi.Per_Aprovim), trup.Niveli, trup.Lloji, idkategoria);// shkon per aprovim per perdoruesit e nivelit tjeter
                                    etapatEReja.Add(etape1);
                                }
                                else if (trup.Niveli > nivelitjeter) break;
                            }
                        }

                        #endregion
                    }
                    else
                    {
                        #region proces ekzistues
                        colEtapeAprovimi etapat = new colEtapeAprovimi();
                        etapat.mbushEtapat(dbRegj.ktheEtapaSipasNrProcesiDheNiveliPerAprovim(nrprocesi, tt.Niveli, idndermarje, idkategoria));
                        //krijuesi                      aprovuesi     modifikuesi   dtmodifikimi
                        clsEtapeAprovimi etape2 = new clsEtapeAprovimi(0, skemaWorkFlow, idkoka, nrprocesi, nretape, etapat[0].IdKrijuesi, dtdergimi, idaprovuesi, idperdoruesdok, dtkrijimi, dtkujtese, idndermarje, Convert.ToInt32(DbRegjistrim.StatusAprovimi.Aprovuar), tt.Niveli, llojiperdorues, idkategoria);// perdoruesi aktual e aprovon

                        if ((tt.VleraLimit == 0 && (tt.ColGrupeKF.Count > 0 && klientGrupi)) || ((tt.VleraLimit != 0 && tt.VleraLimit >= vlefta) && (tt.ColGrupeKF.Count == 0 || (tt.ColGrupeKF.Count > 0 && klientGrupi))) || (tt.Dite != 0 && dite < tt.Dite)) //nese vlera limit ne skeme eshte zero nuk behet aprovim limiti
                        {
                            if (tt.NiveliApr == 0)//nqs eshte aprovuar aprovon direkt prn shkon tek niveli i percaktuar
                                statusAprovimi = (DbRegjistrim.StatusAprovimi.Aprovuar);// me i vogel se limiti dokumenti aprovohet por duhet para nqs ka perdorues te tjere ne kete nivel
                            else nivelitjeter = tt.NiveliApr;

                        }
                        clsEtapeAprovimi etapaVjeter = new clsEtapeAprovimi();
                        foreach (clsEtapeAprovimi et in etapat)// etapat e tjera te ketij niveli ju ndryshojme nr e etapes
                        {
                            // if ((et.IdAprovuesi == tt.IdPerdRol && et.LlojAprovuesi==tt.Lloji)||(delegim && et.idAprovuesi==tt.IdDelegimi))
                            if (et.idEtapa == idetapaaktuale)
                                etapaVjeter = et;
                            else
                            {
                                nretape++;
                                et.Etapa = nretape;
                                mesazh = et.modifiko(dbRegj);
                                if (!mesazh.Status)
                                    return mesazh;
                            }
                        }


                        etape2.oColKomente.merrKomenteSipasEtapes(etapaVjeter.IdEtapa, dbRegj);
                        etapatEReja.Add(etape2);
                        if (etapat.Count > 1)//me shume se nje duhet te aprovohet nga perdorues te tjere te ketij niveli
                        {  /// if(trup.VleraLimit>= koka.Totali - koka.Zbritje - koka.Tvsh)// duhet pare rasti si do behet kur njeri perdorues 5000 tjetri 6000 dhe vlefta 5500
                            statusAprovimi = (DbRegjistrim.StatusAprovimi.Per_Aprovim);// duhet te aprovohet edhe nga perdoruesit e tjere te ketij niveli
                            nivelitjeter = 0;
                        }

                        else if (etapat.Count == 1 && statusAprovimi == (DbRegjistrim.StatusAprovimi.Per_Aprovim))// kur eshte nje eshte vetem ky perdorues qe duhet ta aprovoje nqs eshte aprovuar nga ky perdorues nuk vazhdojme prn hyjme ne ciklin per te derguar tek perdoruesit e nivelit tjeter 
                        {
                            foreach (DbAdmin.clsTrupiSkemaWorkFlow trup in trupiskema)
                            {
                                if (trup.Niveli <= tt.Niveli)
                                    continue;

                                if (trup.Niveli > tt.Niveli)
                                {
                                    if (nivelitjeter == tt.Niveli)
                                        nivelitjeter = trup.Niveli;

                                    if (trup.Niveli == nivelitjeter)// shkon per aprovim tek perdoruesit e nivelit tjeter
                                    {

                                        nretape++;
                                        clsEtapeAprovimi etape1 = new clsEtapeAprovimi(0, skemaWorkFlow, idkoka, nrprocesi, nretape, etapat[0].IdKrijuesi, dtdergimi, trup.IdPerdRol, idperdoruesdok, dtkrijimi, dtkujtese, idndermarje, Convert.ToInt32(DbRegjistrim.StatusAprovimi.Per_Aprovim), trup.Niveli, trup.Lloji, idkategoria);
                                        etapatEReja.Add(etape1);
                                    }
                                    else if (trup.Niveli > nivelitjeter) break;
                                }
                            }

                        }

                        mesazh = etapaVjeter.fshi(dbRegj);//fshijme etapen e vjeter qe eshte me status per aprovim meqe ky perdorues e kalovi dokumentin me status aprovuar
                        if (!mesazh.Status)
                            return mesazh;
                        #endregion
                    }
                    if (nivelitjeter == tt.Niveli) //kemi aritur nivelin e fundit dokumenti shkon me status aprovuar
                        statusAprovimi = (DbRegjistrim.StatusAprovimi.Aprovuar);
                    break;
                case DbRegjistrim.StatusAprovimi.Refuzuar:
                    #region refuzuar
                    colEtapeAprovimi etapatref = new colEtapeAprovimi();
                    etapatref.mbushEtapat(dbRegj.ktheEtapaSipasNrProcesiDheNiveliPerAprovim(nrprocesi, tt.Niveli, idndermarje, idkategoria));

                    clsEtapeAprovimi etape4 = new clsEtapeAprovimi(0, skemaWorkFlow, idkoka, nrprocesi, nretape, etapatref[0].IdKrijuesi, dtdergimi, idaprovuesi, idperdoruesdok, dtkrijimi, dtkujtese, idndermarje, Convert.ToInt32(DbRegjistrim.StatusAprovimi.Refuzuar), tt.Niveli, llojiperdorues, idkategoria);// perdoruesi aktual e refuzon
                    colKomenteAprovimi col = new colKomenteAprovimi();
                    foreach (clsEtapeAprovimi et in etapatref)
                    {
                        col.merrKomenteSipasEtapes(et.IdEtapa, dbRegj);
                        mesazh = et.fshi(dbRegj);
                        if (!mesazh.Status)
                            return mesazh;
                        if (et.LlojAprovuesi == 0) throw new MyException(MessagesResource.Messages["msgLlojIPanjkohurPerdoruesiNeEtape"]);
                        if (et.LlojAprovuesi == 1)
                        { //shto perdoruesin
                            if (et.idAprovuesi == etape4.idAprovuesi) continue;
                            listaParalelRefuzim.Add(et.idAprovuesi);
                        }
                        else //shto perdoruesit e rolit
                        {
                            colRolPerdorues colRoli = new colRolPerdorues();
                            colRoli.mbushRolePerdoruesSipasRoli(et.idAprovuesi, dbadmin);
                            foreach (clsRolPerdorues roli in colRoli)
                            {
                                if (roli.IdPerdorues == etape4.idAprovuesi) continue;
                                listaParalelRefuzim.Add(roli.IdPerdorues);
                            }
                        }

                    }
                    statusAprovimi = (DbRegjistrim.StatusAprovimi.Refuzuar);
                    etape4.oColKomente = col;
                    etapatEReja.Add(etape4);
                    #endregion
                    break;

                case DbRegjistrim.StatusAprovimi.Deleguar:
                    #region deleguar
                    if (tt.IdDelegimi == 0 || idaprovuesi == tt.IdDelegimi)
                        return new clsMesazh(false, MessagesResource.Messages["msgNukKeniCaktuarDelegues"]);
                    colEtapeAprovimi etapatdel = new colEtapeAprovimi();
                    etapatdel.mbushEtapat(dbRegj.ktheEtapaSipasNrProcesiDheNiveliPerAprovim(nrprocesi, tt.Niveli, idndermarje, idkategoria));
                    clsEtapeAprovimi etape5 = new clsEtapeAprovimi(0, skemaWorkFlow, idkoka, nrprocesi, nretape, etapatdel[0].IdKrijuesi, dtdergimi, tt.IdPerdRol, idperdoruesdok, dtkrijimi, dtkujtese, idndermarje, Convert.ToInt32(DbRegjistrim.StatusAprovimi.Deleguar), tt.Niveli, tt.Lloji, idkategoria);// perdoruesi aktual e delegon

                    nretape++;
                    clsEtapeAprovimi etape3 = new clsEtapeAprovimi(0, skemaWorkFlow, idkoka, nrprocesi, nretape, etapatdel[0].IdKrijuesi, dtdergimi, tt.IdDelegimi, idperdoruesdok, dtkrijimi, dtkujtese, idndermarje, Convert.ToInt32(DbRegjistrim.StatusAprovimi.Per_Aprovim), tt.Niveli, llojiperdorues, idkategoria);// perdoruesi i deleguar per aprovim

                    clsEtapeAprovimi etapaVjeter3 = new clsEtapeAprovimi();
                    foreach (clsEtapeAprovimi et in etapatdel)// etapat e tjera te ketij niveli ju ndryshojme nr e etapes
                    {
                        // if ((et.IdAprovuesi == tt.IdPerdRol && et.LlojAprovuesi == tt.Lloji)||(delegim && et.idAprovuesi==tt.IdDelegimi))
                        if (et.idEtapa == idetapaaktuale)
                            etapaVjeter3 = et;
                        else
                        {
                            nretape++;
                            et.Etapa = nretape;
                            mesazh = et.modifiko(dbRegj);
                            if (!mesazh.Status)
                                return mesazh;
                        }
                    }
                    etape5.oColKomente = new colKomenteAprovimi();
                    etape5.oColKomente.merrKomenteSipasEtapes(etapaVjeter3.IdEtapa, dbRegj);
                    etapatEReja.Add(etape5);
                    etapatEReja.Add(etape3);
                    mesazh = etapaVjeter3.fshi(dbRegj);//fshijme etapen e vjeter qe eshte me status per aprovim meqe ky perdorues e kalovi dokumentin me status deleguar
                    if (!mesazh.Status)
                        return mesazh;
                    #endregion
                    break;
                case DbRegjistrim.StatusAprovimi.Undefined://rasti i modifikimit
                    #region modifikim
                    colEtapeAprovimi etapatmod = new colEtapeAprovimi();
                    etapatmod.mbushEtapat(dbRegj.ktheEtapaSipasNrProcesiDheNiveliPerAprovim(nrprocesi, tt.Niveli, idndermarje, idkategoria));
                    //krijuesi                      aprovuesi     modifikuesi   dtmodifikimi
                    clsEtapeAprovimi etape7 = new clsEtapeAprovimi(0, skemaWorkFlow, idkoka, nrprocesi, nretape, etapatmod[0].IdKrijuesi, dtdergimi, tt.IdPerdRol, idperdoruesdok, dtdergimi, dtkujtese, idndermarje, Convert.ToInt32(DbRegjistrim.StatusAprovimi.Per_Aprovim), tt.Niveli, tt.Lloji, idkategoria);// perdoruesi aktual e modifikon
                    etapatEReja.Add(etape7);
                    foreach (DbAdmin.clsTrupiSkemaWorkFlow trup in trupiskema)
                    {
                        if (trup.Niveli < tt.Niveli)
                            continue;

                        if (trup.Niveli == tt.Niveli && !(tt.IdPerdRol == trup.IdPerdRol && tt.Lloji == trup.Lloji))
                        {
                            nretape++;
                            clsEtapeAprovimi etape1 = new clsEtapeAprovimi(0, skemaWorkFlow, idkoka, nrprocesi, nretape, etapatmod[0].IdKrijuesi, dtdergimi, trup.IdPerdRol, idperdoruesdok, dtdergimi, dtkujtese, idndermarje, Convert.ToInt32(DbRegjistrim.StatusAprovimi.Per_Aprovim), trup.Niveli, trup.Lloji, idkategoria);// shkon per aprovim per perdoruesit e tjere te te njejtit nivel
                            nivelitjeter = 0;
                            foreach (clsEtapeAprovimi et in etapatmod)
                            {
                                if (et.idAprovuesi == trup.IdPerdRol && et.LlojAprovuesi == trup.Lloji)
                                {
                                    etape1.oColKomente.merrKomenteSipasEtapes(et.idEtapa, dbRegj);
                                }
                            }
                            etapatEReja.Add(etape1);


                        }
                        if (trup.Niveli > tt.Niveli)//nqs ka ne kete nivel nderprite
                            break;
                    }
                    foreach (clsEtapeAprovimi et in etapatmod)// fshihen etapat e vjetra
                    {
                        if (et.IdAprovuesi == etape7.IdAprovuesi && et.llojAprovuesi == etape7.llojAprovuesi)
                            etape7.oColKomente.merrKomenteSipasEtapes(et.idEtapa, dbRegj);
                        mesazh = et.fshi(dbRegj);
                        if (!mesazh.Status)
                            return mesazh;

                    }
                    #endregion
                    break;
                case DbRegjistrim.StatusAprovimi.Aprovuar:
                    if (statusAprovimi == DbRegjistrim.StatusAprovimi.Aprovuar)
                    {
                        var perdoruesiSkema = trupiskema.FirstOrDefault(x => x.IdPerdRol == idaprovuesi);
                        int niveli = perdoruesiSkema != null ? perdoruesiSkema.Niveli : 0;
                        if (niveli != 1)
                            return new MesazhGabimi(MessagesResource.Messages["msgDokNeProcesAprovimi"]);
                    }
                    break;
            }
            mesazh = ModifikoStatusAprovimi(dbRegj, idkategoria, statusAprovimi, idkoka);
            if (!mesazh) return mesazh;
            //DbShare.clsKonfigurimAmbjenti konfigAmb = new DbShare.clsKonfigurimAmbjenti(koka.IdKonfigAmbjente, dbRegj );
            string shitjeBlerje = DbShare.clsKonfigurimAmbjenti.ktheIdKategori(idkonfig, new DbShare.clsDatabaseShare(dbRegj)) == 1 ? "shitje" : "blerje";
            string kodKonfigAmbjente = DbShare.clsKonfigurimAmbjenti.ktheKodKonfigurimi(idkonfig, new DbShare.clsDatabaseShare(dbRegj));
            //konfigAmb.IdKategori;
            foreach (clsEtapeAprovimi etape in etapatEReja)// ruajme aprovimet e reja
            {
                mesazh = etape.ruaj(dbRegj);
                if (!mesazh.Status)
                    return mesazh;
            }
            foreach (clsEtapeAprovimi etape in etapatEReja)// dergojme email-et mbasi jane ruajtur te gjitha
            {
                if (!kokaskema.Njoftim) continue;
                if (statusAprovimi == DbRegjistrim.StatusAprovimi.Refuzuar) // Rasti3: refuzim dergohet email Formati3 + dergohet email rolit dhe paraleleve pervec aprovuesit Formati8
                {
                    #region dergim email krijuesit Formati3
                    clsPerdorues krijuesi = new clsPerdorues(etape.IdKrijuesi);
                    List<string> toEmail = new List<string>() { krijuesi.PerdoruesEmail };
                    if (kokaskema.DergoEmailPasAprovimitFinal)
                    {
                        toEmail.AddRange(kokaskema.EmailAprovimi.Split(';'));
                    }
                    
                    clsPerdorues refuzuesi = new clsPerdorues(etape.idAprovuesi, dbadmin);
                    var refuzuesiEmerPlote = $"{refuzuesi.EmriPerdorues} {refuzuesi.MbiemriPerdorues}";

                    EmailComposer.sendPlainTextEmailRefuzuar(etape.IdNdermarje, toEmail.ToArray(), serverUrl, shitjeBlerje, krijuesi.MerrCultureInfo(), kodKonfigAmbjente, nrdok, idkoka, etape.IdEtapa, etape.NrProcesi, dtdok.ToShortDateString(), refuzuesiEmerPlote, idkategoria, idPerdorues);
                    
                    #endregion
                    #region dergim email formati 8 paraleleve
                    colPerdoruesit toParalelEmail = new colPerdoruesit();
                    for (int i = 0; i < listaParalelRefuzim.Count; i++)
                    {
                        toParalelEmail.Add(new clsPerdorues(listaParalelRefuzim[i], dbadmin));
                    }
                                       
                    EmailComposer.sendPlainTextEmailRefuzuar(etape.IdNdermarje, toParalelEmail.merrEmailSipasGjuhesPerdoruesit(0), serverUrl, shitjeBlerje, MessagesResource.KtheCultureInfo(0), kodKonfigAmbjente, nrdok, idkoka, etape.IdEtapa, etape.NrProcesi, dtdok.ToShortDateString(), refuzuesiEmerPlote, idkategoria, idPerdorues);
                    
                    EmailComposer.sendPlainTextEmailRefuzuar(etape.IdNdermarje, toParalelEmail.merrEmailSipasGjuhesPerdoruesit(1), serverUrl, shitjeBlerje, MessagesResource.KtheCultureInfo(1), kodKonfigAmbjente, nrdok, idkoka, etape.IdEtapa, etape.NrProcesi, dtdok.ToShortDateString(), refuzuesiEmerPlote, idkategoria, idPerdorues);
                    
                    #endregion

                    #region menyra si behej me pare percaktimi i gjuhes se emailit (teknikisht nuk ndahej fare, ndaj u komentua)
                    //int tmpIdGjuha = -1;
                    //foreach (clsPerdorues tmpPerdorues in toParalelEmail.OrderBy(elem => elem.IdGjuha))
                    //{
                    //    if (tmpIdGjuha == -1)
                    //        tmpIdGjuha = tmpPerdorues.IdGjuha;

                    //    if (tmpIdGjuha == tmpPerdorues.IdGjuha)
                    //        toEmail1Gjuhe.Add(tmpPerdorues.PerdoruesEmail);
                    //    else
                    //    {
                    //        EmailComposer.sendPlainTextEmailRefuzuar(etape.IdNdermarje, toEmail1Gjuhe.ToArray(), serverUrl, shitjeBlerje, ci, kodKonfigAmbjente, nrdok, idkoka, etape.IdEtapa, etape.NrProcesi, dtdok.ToShortDateString(), refuzuesiEmerPlote, idkategoria, idPerdorues);
                    //        toEmail1Gjuhe.Clear();
                    //        toEmail1Gjuhe.Add(tmpPerdorues.PerdoruesEmail);
                    //        tmpIdGjuha = tmpPerdorues.IdGjuha;
                    //    }
                    //}
                    //if (tmpIdGjuha != -1)
                    //    EmailComposer.sendPlainTextEmailRefuzuar(etape.IdNdermarje, toEmail1Gjuhe.ToArray(), serverUrl, shitjeBlerje, ci, kodKonfigAmbjente, nrdok, idkoka, etape.IdEtapa, etape.NrProcesi, dtdok.ToShortDateString(), refuzuesiEmerPlote, idkategoria, idPerdorues);
                    #endregion
                    continue;
                }
                if (etape.StatusAprovimi == (int)DbRegjistrim.StatusAprovimi.Deleguar)//Rasti5 Delegimi dergohet email Formati6 krijuesit  dhe ishDestinatarit.
                {
                    #region dergim email formati 6 krijuesit dhe ish destinatarit/ve qe ndodhi delegimi
                    colPerdoruesit toParalelEmail = new colPerdoruesit();
                    toParalelEmail.Add(new clsPerdorues(etape.IdKrijuesi, dbadmin));
                    if (tt.Lloji == 0) throw new MyException(MessagesResource.Messages["msgLlojIPanjkohurPerdoruesiNeEtape"]);
                    if (tt.Lloji == 1)
                    { //shto perdoruesin

                        toParalelEmail.Add(new clsPerdorues(tt.IdPerdRol, dbadmin));
                    }
                    else //shto perdoruesit e rolit
                    {
                        colRolPerdorues colRoli = new colRolPerdorues();
                        colRoli.mbushRolePerdoruesSipasRoli(tt.IdPerdRol, dbadmin);
                        foreach (clsRolPerdorues roli in colRoli)
                        {
                            if (roli.IdPerdorues == idPerdorues) continue; //deleguesit nuk i cojme email
                            toParalelEmail.Add(new clsPerdorues(roli.IdPerdorues, dbadmin));
                        }
                    }
                    clsPerdorues deleguesi = new clsPerdorues(idPerdorues, dbadmin);
                    var deleguesiEmerPlote = $"{deleguesi.EmriPerdorues} {deleguesi.MbiemriPerdorues}";

                    clsPerdorues iDeleguari = new clsPerdorues(tt.IdDelegimi, dbadmin);
                    var iDeleguariEmerPlote = $"{iDeleguari.EmriPerdorues} {iDeleguari.MbiemriPerdorues}";

                    EmailComposer.sendPlainTextEmailKrijuesiDeleguesit(etape.IdNdermarje, toParalelEmail.merrEmailSipasGjuhesPerdoruesit(0), serverUrl, shitjeBlerje, MessagesResource.KtheCultureInfo(0), kodKonfigAmbjente, nrdok, idkoka, etape.IdEtapa, etape.NrProcesi, dtdok.ToShortDateString(), deleguesiEmerPlote, iDeleguariEmerPlote, idkategoria, idPerdorues);
                    EmailComposer.sendPlainTextEmailKrijuesiDeleguesit(etape.IdNdermarje, toParalelEmail.merrEmailSipasGjuhesPerdoruesit(1), serverUrl, shitjeBlerje, MessagesResource.KtheCultureInfo(1), kodKonfigAmbjente, nrdok, idkoka, etape.IdEtapa, etape.NrProcesi, dtdok.ToShortDateString(), deleguesiEmerPlote, iDeleguariEmerPlote, idkategoria, idPerdorues);

                    #endregion
                    continue;
                }
                if (etape.StatusAprovimi == (int)DbRegjistrim.StatusAprovimi.Per_Aprovim && statusapp == DbRegjistrim.StatusAprovimi.Deleguar)//Rasti5 Delegimi dergohet email Formati1 te deleguarit.
                {
                    #region dergim email formati1 te deleguarit
                    clsPerdorues iDeleguari = new clsPerdorues(etape.idAprovuesi);
                    string[] toEmail = { iDeleguari.PerdoruesEmail };
                    clsPerdorues krijuesi = new clsPerdorues(etapatEReja[0].IdKrijuesi, dbadmin);
                    var krijuesiEmerPlote = $"{krijuesi.EmriPerdorues} {krijuesi.MbiemriPerdorues}";
                    EmailComposer.sendPlainTextEmailPerAprovim(etape.IdNdermarje, toEmail, serverUrl, shitjeBlerje, iDeleguari.MerrCultureInfo(), kodKonfigAmbjente, nrdok, idkoka, etape.IdEtapa, etape.NrProcesi, dtdok.ToShortDateString(), krijuesiEmerPlote, idkategoria, idPerdorues);
                    #endregion
                    continue;
                }
                if (etape.StatusAprovimi == (int)DbRegjistrim.StatusAprovimi.Per_Aprovim && statusapp == DbRegjistrim.StatusAprovimi.Per_Aprovim)//Rasti1 formati1
                {
                    #region formati1 personave per aprovim
                    colPerdoruesit toParalelEmail = new colPerdoruesit();
                    clsPerdorues krijuesi = new clsPerdorues(etapatEReja[0].IdAprovuesi, dbadmin);
                    var krijuesiEmerPlote = $"{krijuesi.EmriPerdorues} {krijuesi.MbiemriPerdorues}";
                    if (etape.LlojAprovuesi == 0) throw new MyException("lloj perdoruesi te etapa i panjohur!");
                    if (etape.LlojAprovuesi == 1)
                    { //shto perdoruesin                            
                        toParalelEmail.Add(new clsPerdorues(etape.IdAprovuesi));
                    }
                    else //shto perdoruesit e rolit
                    {
                        colRolPerdorues colRoli = new colRolPerdorues();
                        colRoli.mbushRolePerdoruesSipasRoli(etape.IdAprovuesi);
                        foreach (clsRolPerdorues roli in colRoli)
                        {
                            if (roli.IdPerdorues == idPerdorues) continue; //deleguesit nuk i cojme email
                            toParalelEmail.Add(new clsPerdorues(roli.IdPerdorues));
                        }
                    }

                    EmailComposer.sendPlainTextEmailPerAprovim(etape.IdNdermarje, toParalelEmail.merrEmailSipasGjuhesPerdoruesit(0), serverUrl, shitjeBlerje, MessagesResource.KtheCultureInfo(0), kodKonfigAmbjente, nrdok, idkoka, etape.IdEtapa, etape.NrProcesi, dtdok.ToShortDateString(), krijuesiEmerPlote, idkategoria, idPerdorues);
                    EmailComposer.sendPlainTextEmailPerAprovim(etape.IdNdermarje, toParalelEmail.merrEmailSipasGjuhesPerdoruesit(1), serverUrl, shitjeBlerje, MessagesResource.KtheCultureInfo(1), kodKonfigAmbjente, nrdok, idkoka, etape.IdEtapa, etape.NrProcesi, dtdok.ToShortDateString(), krijuesiEmerPlote, idkategoria, idPerdorues);

                    #endregion
                    continue;
                }
                if (etape.statusAprovimi == (int)DbRegjistrim.StatusAprovimi.Aprovuar && statusAprovimi == DbRegjistrim.StatusAprovimi.Aprovuar) //rasti2 u aprovua totalisht
                {
                    if (etape.IdKrijuesi == etape.idAprovuesi) continue;
                    var aprovuesi = new clsPerdorues(etape.IdAprovuesi);
                    var aprovuesiEmerPlote = $"{aprovuesi.EmriPerdorues} {aprovuesi.MbiemriPerdorues}";
                    #region dergon email krijuesit formati 2
                    var krijuesi = new clsPerdorues(etape.IdKrijuesi);
                    var toEmail = new List<string> { krijuesi.PerdoruesEmail };
                    if (kokaskema.DergoEmailPasAprovimitFinal)
                    {
                        toEmail.AddRange(kokaskema.EmailAprovimi.Split(';'));
                    }
                    EmailComposer.sendPlainTextEmailAprovuar(etape.IdNdermarje, toEmail.ToArray(), serverUrl, shitjeBlerje, krijuesi.MerrCultureInfo(), kodKonfigAmbjente, nrdok, idkoka, etape.IdEtapa, etape.NrProcesi, dtdok.ToShortDateString(), aprovuesiEmerPlote, idkategoria, idPerdorues);
                    #endregion
                    #region dergohet email rolit pervec aprovuesit Formati7
                    if (tt.Lloji == 0) throw new MyException("lloj perdoruesi te etapa i panjohur!");
                    colPerdoruesit toParalelEmail = new colPerdoruesit();
                    if (tt.Lloji == 1)
                        continue;
                    else //shto perdoruesit e rolit
                    {
                        colRolPerdorues colRoli = new colRolPerdorues();
                        colRoli.mbushRolePerdoruesSipasRoli(tt.IdPerdRol, dbadmin);
                        foreach (clsRolPerdorues roli in colRoli)
                        {
                            if (roli.IdPerdorues == etape.idAprovuesi) continue; //deleguesit nuk i cojme email
                            toParalelEmail.Add(new clsPerdorues(roli.IdPerdorues, dbadmin));
                        }

                    }

                    EmailComposer.sendPlainTextEmailAprovuarRol(etape.IdNdermarje, toParalelEmail.merrEmailSipasGjuhesPerdoruesit(0), serverUrl, shitjeBlerje, MessagesResource.KtheCultureInfo(0), kodKonfigAmbjente, nrdok, idkoka, etape.IdEtapa, etape.NrProcesi, dtdok.ToShortDateString(), aprovuesiEmerPlote, idkategoria, idPerdorues);
                    EmailComposer.sendPlainTextEmailAprovuarRol(etape.IdNdermarje, toParalelEmail.merrEmailSipasGjuhesPerdoruesit(1), serverUrl, shitjeBlerje, MessagesResource.KtheCultureInfo(1), kodKonfigAmbjente, nrdok, idkoka, etape.IdEtapa, etape.NrProcesi, dtdok.ToShortDateString(), aprovuesiEmerPlote, idkategoria, idPerdorues);
                    #endregion
                    continue;
                }
                if (etape.statusAprovimi == (int)DbRegjistrim.StatusAprovimi.Aprovuar && statusAprovimi == DbRegjistrim.StatusAprovimi.Per_Aprovim) //Rasti1: dergohet email rolit pervec aprovuesit Formati7
                {
                    #region dergohet email rolit pervec aprovuesit Formati7
                    if (etape.idAprovuesi == etape.idKrijuesi) continue;

                    if (tt.Lloji == 0) throw new MyException("lloj perdoruesi te etapa i panjohur!");

                    var toParalelEmail = new colPerdoruesit();

                    if (tt.Lloji == 1)
                        continue;
                    else //shto perdoruesit e rolit
                    {
                        colRolPerdorues colRoli = new colRolPerdorues();
                        colRoli.mbushRolePerdoruesSipasRoli(tt.IdPerdRol, dbadmin);
                        foreach (clsRolPerdorues roli in colRoli)
                        {
                            //deleguesit nuk i cojme email
                            if (roli.IdPerdorues == etape.idAprovuesi) continue;
                            toParalelEmail.Add(new clsPerdorues(roli.IdPerdorues, dbadmin));
                        }
                    }

                    clsPerdorues aprovuesi = new clsPerdorues(etape.idAprovuesi, dbadmin);
                    var aprovuesiEmerPlote = $"{aprovuesi.EmriPerdorues} {aprovuesi.MbiemriPerdorues}";
                   
                    EmailComposer.sendPlainTextEmailAprovuarRol(etape.IdNdermarje, toParalelEmail.merrEmailSipasGjuhesPerdoruesit(0), serverUrl, shitjeBlerje, MessagesResource.KtheCultureInfo(0), kodKonfigAmbjente, nrdok, idkoka, etape.IdEtapa, etape.NrProcesi, dtdok.ToShortDateString(), aprovuesiEmerPlote, idkategoria, idPerdorues);
                    EmailComposer.sendPlainTextEmailAprovuarRol(etape.IdNdermarje, toParalelEmail.merrEmailSipasGjuhesPerdoruesit(1), serverUrl, shitjeBlerje, MessagesResource.KtheCultureInfo(1), kodKonfigAmbjente, nrdok, idkoka, etape.IdEtapa, etape.NrProcesi, dtdok.ToShortDateString(), aprovuesiEmerPlote, idkategoria, idPerdorues);
                    #endregion
                    continue;
                }
                if (DbRegjistrim.StatusAprovimi.Undefined == statusapp && etape.StatusAprovimi == (int)DbRegjistrim.StatusAprovimi.Per_Aprovim)
                { //modifikimit
                    #region dergim email formati 5 paraleleve

                    colPerdoruesit toParalelEmail = new colPerdoruesit();
                    clsPerdorues modifikuesi = new clsPerdorues(etape.IdModifikuesi, dbadmin);
                    var modifikuesiEmerPlote = $"{modifikuesi.EmriPerdorues} {modifikuesi.MbiemriPerdorues}";
                    if (etape.LlojAprovuesi == 0) throw new MyException("lloj perdoruesi te etapa i panjohur!");
                    if (etape.LlojAprovuesi == 1)
                    { //shto perdoruesin                            
                        toParalelEmail.Add(new clsPerdorues(etape.IdAprovuesi, dbadmin));
                    }
                    else //shto perdoruesit e rolit
                    {
                        colRolPerdorues colRoli = new colRolPerdorues();
                        colRoli.mbushRolePerdoruesSipasRoli(etape.IdAprovuesi, dbadmin);
                        foreach (clsRolPerdorues roli in colRoli)
                        {
                            if (roli.IdPerdorues == idPerdorues)
                                continue; //deleguesit nuk i cojme email
                            toParalelEmail.Add(new clsPerdorues(roli.IdPerdorues, dbadmin));
                        }
                    }
                    
                    EmailComposer.sendPlainTextEmailParalelPerModifikim(etape.IdNdermarje, toParalelEmail.merrEmailSipasGjuhesPerdoruesit(0), serverUrl, shitjeBlerje, MessagesResource.KtheCultureInfo(0), kodKonfigAmbjente, nrdok, idkoka, etape.IdEtapa, etape.NrProcesi, dtdok.ToShortDateString(), modifikuesiEmerPlote, idkategoria, idPerdorues);
                    EmailComposer.sendPlainTextEmailParalelPerModifikim(etape.IdNdermarje, toParalelEmail.merrEmailSipasGjuhesPerdoruesit(1), serverUrl, shitjeBlerje, MessagesResource.KtheCultureInfo(1), kodKonfigAmbjente, nrdok, idkoka, etape.IdEtapa, etape.NrProcesi, dtdok.ToShortDateString(), modifikuesiEmerPlote, idkategoria, idPerdorues);
                    #endregion
                    continue;
                }
            }
            if (DbRegjistrim.StatusAprovimi.Undefined == statusapp)
            { //modifikimit
                #region dergim email krijuesit Formati3
                bool NjoftimMeEmail = colEtapeAprovimi.NjoftimMeEmailSipasSkemesSePunes(skemaWorkFlow);
                if (NjoftimMeEmail)
                { 
                    clsPerdorues krijuesi = new clsPerdorues(etapatEReja[0].IdKrijuesi, dbadmin);
                string[] toEmail = { krijuesi.PerdoruesEmail };
                clsPerdorues modifikuesi = new clsPerdorues(etapatEReja[0].IdModifikuesi, dbadmin);
                var modifikuesiEmerPlote = $"{modifikuesi.EmriPerdorues} {modifikuesi.MbiemriPerdorues}";
                EmailComposer.sendPlainTextEmailKrijuesiPerModifikim(etapatEReja[0].IdNdermarje, toEmail, serverUrl, shitjeBlerje, krijuesi.MerrCultureInfo(), kodKonfigAmbjente, nrdok, idkoka, etapatEReja[0].IdEtapa, etapatEReja[0].NrProcesi, dtdok.ToShortDateString(), modifikuesiEmerPlote, idkategoria, idPerdorues);
                #endregion
            }
        }

            string mesazhi = "";
            if (statusAprovimi == (DbRegjistrim.StatusAprovimi.Per_Aprovim))
                mesazhi = MessagesResource.Messages["msgDokDerguaAprovim"];

            else if (statusAprovimi == (DbRegjistrim.StatusAprovimi.Aprovuar))
                mesazhi = MessagesResource.Messages["msgDokUAprovua"];

            else if (statusAprovimi == (DbRegjistrim.StatusAprovimi.Refuzuar))
                mesazhi = MessagesResource.Messages["msgDokURefuzua"];

            if (statusapp == DbRegjistrim.StatusAprovimi.Deleguar)
                mesazhi = MessagesResource.Messages["msgDokUDelegua"];

            if (statusapp == DbRegjistrim.StatusAprovimi.Undefined)//per modifikim
                mesazhi = MessagesResource.Messages["msgModifikimiMeSukses"];

            return new clsMesazh(true, mesazhi);
        }

        public static StatusAprovimi MerrStatusAprovimi(int skemaWorkFlow, clsDatabaseRegjistrim dbRegj, int idkokapara, int idaprovuesi, StatusAprovimi statusapp, int idetapaaktuale, int idkategoria, int idndermarje, int idklientfurnitor, double vlefta, int dite)
        {
            clsDatabaseAdmin dbadmin = new clsDatabaseAdmin(dbRegj);
            if (skemaWorkFlow == 0)
                return DbRegjistrim.StatusAprovimi.Aprovuar;
            colTrupiSkemaWorkFlow trupiskema = new colTrupiSkemaWorkFlow();
            trupiskema.mbushTrupatSkemeWorkFlowSipasKokes(skemaWorkFlow, dbadmin);
            clsDatabaseKontabilitet dbkont = new clsDatabaseKontabilitet(dbRegj);
            int nrprocesi = merrNrProcesiSipasKokaShitje(idkokapara, idkategoria, idetapaaktuale, dbRegj);
            clsTrupiSkemaWorkFlow tt = new clsTrupiSkemaWorkFlow();
            if (idetapaaktuale == 0)
                tt.merrTrupSipasKokesDhePerdoruesit(skemaWorkFlow, idaprovuesi, dbadmin);
            else
            {
                clsEtapeAprovimi etapaakt = new clsEtapeAprovimi(idetapaaktuale, idkategoria, dbRegj);
                tt.merrTrupSipasKokesDhePerdoruesitDheNivelit(skemaWorkFlow, idaprovuesi, etapaakt.niveli, dbadmin);
            }
            bool procesiRi = false;
            if (nrprocesi == 0)
            {
                nrprocesi = dbRegj.merrNrProcesiMax(idndermarje) + 1;
                procesiRi = true;
            }
            int nivelitjeter = tt.Niveli;
            StatusAprovimi statusAprovimi = DbRegjistrim.StatusAprovimi.Per_Aprovim;
            switch (statusapp)
            {
                case DbRegjistrim.StatusAprovimi.Per_Aprovim:
                    bool klientGrupi = EshteKlientGrupi(idndermarje, idklientfurnitor, dbkont, tt);
                    if (procesiRi)
                    {
                        //nuk ka limit por ka grup dhe plotesohet grupi ose ka limit dhe (nuk ka grup ose ka grup dhe plotesohet kushti) koka.Totali - koka.Zbritje - koka.Tvsh
                        if ((tt.VleraLimit == 0 && (tt.ColGrupeKF.Count > 0 && klientGrupi)) || ((tt.VleraLimit != 0 && tt.VleraLimit >= vlefta) && (tt.ColGrupeKF.Count == 0 || (tt.ColGrupeKF.Count > 0 && klientGrupi))) || (tt.Dite != 0 && dite < tt.Dite)) //nese vlera limit ne skeme eshte zero nuk behet aprovim limiti
                        {
                            if (tt.NiveliApr == 0)//nqs eshte aprovuar aprovon direkt prn shkon tek niveli i percaktuar
                                statusAprovimi = (DbRegjistrim.StatusAprovimi.Aprovuar);// me i vogel se limiti dokumenti aprovohet por duhet para nqs ka perdorues te tjere ne kete nivel
                            else
                                nivelitjeter = tt.NiveliApr;
                        }
                        bool kaNeNivel = false;
                        foreach (DbAdmin.clsTrupiSkemaWorkFlow trup in trupiskema)
                        {
                            if (trup.Niveli < tt.Niveli)
                                continue;
                            if (trup.Niveli == tt.Niveli && !(tt.IdPerdRol == trup.IdPerdRol && tt.Lloji == trup.Lloji))
                            {
                                nivelitjeter = 0;
                                kaNeNivel = true;
                                statusAprovimi = (DbRegjistrim.StatusAprovimi.Per_Aprovim);// duhet te aprovohet edhe nga perdoruesit e tjere te ketij niveli
                            }
                            if (trup.Niveli > tt.Niveli)
                            {
                                if (kaNeNivel)//nqs ka ne kete nivel nderprite
                                    break;
                                if (statusAprovimi == (DbRegjistrim.StatusAprovimi.Aprovuar))//nderprit ciklin sepse dokumenti u aprovua
                                    break;
                                if (nivelitjeter == tt.Niveli)
                                    nivelitjeter = trup.Niveli;
                                if (trup.Niveli > nivelitjeter) break;
                            }
                        }
                    }
                    else
                    {
                        colEtapeAprovimi etapat = new colEtapeAprovimi();
                        etapat.mbushEtapat(dbRegj.ktheEtapaSipasNrProcesiDheNiveliPerAprovim(nrprocesi, tt.Niveli, idndermarje, idkategoria));
                        //krijuesi                      aprovuesi     modifikuesi   dtmodifikimi
                        if ((tt.VleraLimit == 0 && (tt.ColGrupeKF.Count > 0 && klientGrupi)) || ((tt.VleraLimit != 0 && tt.VleraLimit >= vlefta) && (tt.ColGrupeKF.Count == 0 || (tt.ColGrupeKF.Count > 0 && klientGrupi))) || (tt.Dite != 0 && dite < tt.Dite)) //nese vlera limit ne skeme eshte zero nuk behet aprovim limiti
                        {
                            if (tt.NiveliApr == 0)//nqs eshte aprovuar aprovon direkt prn shkon tek niveli i percaktuar
                                statusAprovimi = (DbRegjistrim.StatusAprovimi.Aprovuar);// me i vogel se limiti dokumenti aprovohet por duhet para nqs ka perdorues te tjere ne kete nivel
                            else
                                nivelitjeter = tt.NiveliApr;
                        }

                        if (etapat.Count > 1)//me shume se nje duhet te aprovohet nga perdorues te tjere te ketij niveli
                        {  /// if(trup.VleraLimit>= koka.Totali - koka.Zbritje - koka.Tvsh)// duhet pare rasti si do behet kur njeri perdorues 5000 tjetri 6000 dhe vlefta 5500
                            statusAprovimi = (DbRegjistrim.StatusAprovimi.Per_Aprovim);// duhet te aprovohet edhe nga perdoruesit e tjere te ketij niveli
                            nivelitjeter = 0;
                        }
                        else if (etapat.Count == 1 && statusAprovimi == (DbRegjistrim.StatusAprovimi.Per_Aprovim))// kur eshte nje eshte vetem ky perdorues qe duhet ta aprovoje nqs eshte aprovuar nga ky perdorues nuk vazhdojme prn hyjme ne ciklin per te derguar tek perdoruesit e nivelit tjeter 
                        {
                            foreach (clsTrupiSkemaWorkFlow trup in trupiskema)
                            {
                                if (trup.Niveli <= tt.Niveli)
                                    continue;
                                if (trup.Niveli > tt.Niveli)
                                {
                                    if (nivelitjeter == tt.Niveli)
                                        nivelitjeter = trup.Niveli;
                                    if (trup.Niveli > nivelitjeter)
                                        break;
                                }
                            }
                        }
                    }
                    if (nivelitjeter == tt.Niveli) //kemi aritur nivelin e fundit dokumenti shkon me status aprovuar
                        statusAprovimi = (DbRegjistrim.StatusAprovimi.Aprovuar);
                    break;
            }
            return statusAprovimi;
        }

        private static bool EshteKlientGrupi(int idndermarje, int idklientfurnitor, clsDatabaseKontabilitet dbkont, clsTrupiSkemaWorkFlow tt)
        {
            bool klientGrupi = false;
            if (idklientfurnitor <= 0) return false;

            clsKlientFurnitor kf = new clsKlientFurnitor(idklientfurnitor, dbkont);

            if (string.IsNullOrWhiteSpace(tt.GrupeKF)) return false;
            string[] arrgrup = tt.GrupeKF.Split(',');
            for (int j = 0; j < arrgrup.Length; j++)
            {
                var g = new clsGrupeKF(arrgrup[j], idndermarje, tt.LlojGrupiKf, 0, dbkont);
                if (g.IdGrupi <= 0) continue;
                tt.ColGrupeKF.Add(g);
                switch (tt.LlojGrupiKf)
                {
                    case 1:
                        if (kf.Grupim1KF == g.KodGrupi)
                            klientGrupi = true;
                        break;
                    case 2:
                        if (kf.Grupim2KF == g.KodGrupi)
                            klientGrupi = true;
                        break;
                    case 3:
                        if (kf.Grupim3KF == g.KodGrupi)
                            klientGrupi = true;
                        break;

                }

            }
            return klientGrupi;
        }

        private static clsMesazh ModifikoStatusAprovimi(clsDatabaseRegjistrim dbRegj, int idkategoria, StatusAprovimi statusAprovimi, int idkoka)
        {
            clsMesazh mesazh;
            switch (idkategoria)
            {
                case 1:
                case 2:
                    mesazh = dbRegj.modifikoKokaShitjeStatusAprovimi(idkoka, statusAprovimi);// ndryshojme statusin e aprovimit te dokumentit
                    break;
                case 3:
                    mesazh = dbRegj.modifikoKokaVeprimeBankaStatusAprovimi(idkoka, statusAprovimi);// ndryshojme statusin e aprovimit te dokumentit
                    break;
                case 38:
                    mesazh = dbRegj.modifikoKokaListpageseStatusAprovimi(idkoka, statusAprovimi);// ndryshojme statusin e aprovimit te dokumentit
                    break;
                case 179:
                    mesazh = dbRegj.modifikoKokaBuxhetiStatusAprovimi(idkoka, statusAprovimi);// ndryshojme statusin e aprovimit te dokumentit
                    break;
                default:
                    throw new ArgumentException($"idKategoria {idkategoria} nuk eshte e sakte", nameof(idkategoria));
            }

            return mesazh;
        }


        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush etape nga databaza
        /// </summary>
        /// <param name="db">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushEtape(DataRow db)
        {
            if (db != null)
            {
                try
                {
                    idEtapa = int.Parse(db["IdEtapa"].ToString());
                    idSkema = int.Parse(db["IdSkema"].ToString());
                    skema = db["Skema"].ToString();
                    idKokaShitje = int.Parse(db["IdKokaShitje"].ToString());
                    idKategoria = int.Parse(db["IdKategoria"].ToString());
                    kategoria = db["Kategoria"].ToString();
                    nenkategoria = db["Nenkategoria"].ToString();
                    lloji = db["Lloji"].ToString();
                    monedha = db["Monedha"].ToString();
                    vlefta = double.Parse(db["Vlefta"].ToString());
                    nrDokumenti = db["NrDokumenti"].ToString();
                    vleftaMonBaze = double.Parse(db["VleftaMonBaze"].ToString());
                    nrProcesi = int.Parse(db["NrProcesi"].ToString());
                    etapa = int.Parse(db["Etapa"].ToString());
                    int.TryParse(db["IdKrijuesi"].ToString(), out idKrijuesi);
                    krijuesi = db["Krijuesi"].ToString();
                    DateTime.TryParse(db["DtDergimi"].ToString(), out dtDergimi);
                    int.TryParse(db["IdAprovuesi"].ToString(), out idAprovuesi);
                    aprovuesi = db["Aprovuesi"].ToString();
                    int.TryParse(db["IdModifikuesi"].ToString(), out idModifikuesi);
                    modifikuesi = db["Modifikuesi"].ToString();
                    DateTime.TryParse(db["DtModifikimi"].ToString(), out dtModifikimi);
                    DateTime.TryParse(db["DtKujtese"].ToString(), out dtKujtese);
                    int.TryParse(db["LlojAprovuesi"].ToString(), out llojAprovuesi);
                    int.TryParse(db["StatusAprovimi"].ToString(), out statusAprovimi);
                    idNdermarje = int.Parse(db["IdNdermarje"].ToString());
                    komente = db["Komente"].ToString();

                    progresi = double.Parse(db["Progresi"].ToString());
                    int.TryParse(db["Niveli"].ToString(), out niveli);
                    statusi = db["Statusi"].ToString();
                    klienti = db["Klienti"].ToString();
                    oColKomente = new colKomenteAprovimi();

                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se etapes nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion


    }
}
