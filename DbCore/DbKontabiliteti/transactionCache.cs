using DbCore.DbInventari;
using DbCore.DbShare;
using System.Linq;
using System;
using System.Data;
using DbCore.DbRegjistrim;
using DbCore.DbAdmin;
using DbCore.DbListPagesat;
using DbCore.DbKontabiliteti;
using DbCore.DbQendraKosto;
using DbCore.DbAsete;
using DbCore.DbArkaBanka;
using System.Collections.Generic;
using DbCore.IMBUtils.Logging;
using DbCore.DbAccessIntegration;
using DbCore.DbAccessIntegration.DbISKSH;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Messages;

namespace DbCore
{
    public class transactionCache
    {
        private colLlogarite myColLlog;
        private int llogFromCache;
        private colKlienteFurnitore myColKf;
        private int kfFromCache;
        private colNenLlojLlogarish myColNenLlojLlog;
        private int nenLlojLlogFromCache;
        private colLlojLlogarish myColLlojLlog;
        private int llojLlogFromCache;
        private colKonfigurimAmbjenti myColKa;
        private int konfigAmbientiFromCache;
        private colArtikujt myColArt;
        private int artFromCache;
        private colTaksa myColTaksa;
        private int taksaFromCache;
        private colNjesiAdministrative myColNjesiAdministrative;
        private int njesiAdministrativeFromCache;
        private colStatusMagazine_Asete myStatusMagazine;
        private int statusMagazineCache;
        private colNjesiteArtikulli myColNjesiArt;
        private int njesiArtFromCache;
        private colMonedhat myColMonedha;
        private int monedhaFromCache;
        private colQendraKosto colQendraKosto;
        private int colQendraNgaCache;
        private colKarakteristikaStandarti colKarakteristikaStandarti;
        private int karakteristikaNgaCache;
        private colObjektivaKosto myColObjektiva;
        private int objektivaNgaCache;
        private Dictionary<int, colLlogarite> myColLlogariTeMundshmePerQK;
        private int llogariTeMundshmeQkCache;
        private clsKonfigurimQK myKonfigQk;
        private int konfigQkCache;
        private colDegeAdministrative myColDega;
        private int degaCache;
        private colStrukturatAdministrative myColStruktura;
        private int strukturaCache;
        private clsMonedha myMonedhaNdermarje;
        private int monedhaNdermCache;
        private colTrupiSkemaQK myColTrupiSkemaQK;
        private int trupiskemaqkCache;
        private colNivelRegjistrimi myColNivelRegj;
        private int nivelRegjFromCache;
        private colPikaShitjeFurnizimi myColPikeShitje;
        private int pikeShitjeFromCache;
        private colKusht myColKushte;
        private int kushteFromCache;
        private colKarta myColKartat;
        private int kartatFromCache;
        private colPerdoruesit myColPerdoruesi;
        private int perdoruesiFromCache;
        private colNdermarrjeVitet myColNdermarrjeViti;
        private int ndermarrjeVitiFromCache;
        private List<Tuple<int, clsPeriudhaKontabel>> myColPeriudhaKontabel;
        private int periudhaKonabelFromCache;
        private colVitet myColVitet;
        private int vitiFromCache;
        private List<Tuple<int,clsFormatiKonfig>> myColFormatKonfig;
        private int formatKonfigFromCache;
        private colAgjenteShitje myColAgjenteShitje;
        private int agjentShitjeFromCache;
        private colAutomjete myColAutomjete;
        private int automjeteFromCache;
        private colBankat myColBankat;
        private int bankaFromCache;
        private colGrupimDokumentiKoka myColGrupimDokumentiKoka;
        private int grupimDokumentiKokaFromCache;
        private colGrupeKF myColGrupimeKF;
        private int grupimeKFFromCache;
        private DataTable myAlternativa;
        private int alternativaCache;
        private DataTable myKonfigAutorizime;
        private int konfigAutorizimeCache;
        private List<colArtikulliPerberes> myColArtikulliPerberes;
        private int colArtikulliPerberesFromCache;
        private colNdermarrjet myColNdermarrjet;
        private int ndermarrjaNgaCache;
        private List<FarmaciISKSH> myColFarmaciISKSH;
        private int farmaciISKSHCache;
        private List<DepoFarmaceutikeISKSH> myColDepoFarmaceutikeISKSH;
        private int depoFarmaceutikeISKSHCache;
        private List<FirmeFarmaceutikeISKSH> myColFirmeFarmaceutikeISKSH;
        private int firmeFarmaceutikeISKSHCache;
        private List<MedikamentISKSH> myColMedikamentISKSH;
        private int medikamentISKSHCache;
        private colKodifikimeArtikulli myColKodifikimArtikulli;
        private int kodifikimArtikulliCache;
        private colKarakteristikaStandartiTrupi myColKarakteristikaStandartiTrupi;
        private int karakteristikaStandartiTrupiCache;
        private List<Dictionary<string, int>> myListIdLlojAmortizimi;
        private int idLlojAmortizimiCache;
        private colKurset myColKurset;
        private int kursFromCache;
        private colSkemaKontabelNew myColSkemaKontabel;
        private int skemaKontabelFromCache;
        private colMonedhat myColMonedhatNdermarrjes;
        private int monedhaNdermarrjesFromCache;
        private List<Tuple<int, bool>> myListKaLlogariQkNdermarrja;
        private int kaLlogariQkNdermarrja;
        private List<Tuple<int, bool>> myListEshtePrindQk;
        private int eshtePrindQk;
        private colNiveleCmimesh myColNivelCmimesh;
        private int nivelCmimiFromCache;

        public colQendraKosto ColQendraKosto
        {
            set { colQendraKosto = value; }
            get { return colQendraKosto; }
        }

        public colKarakteristikaStandarti ColKarakteristikaStandarti
        {
            get
            {
                return colKarakteristikaStandarti;
            }

            set
            {
                colKarakteristikaStandarti = value;
            }
        }

        public transactionCache()
        {
            myColLlog = new colLlogarite();
            llogFromCache = 0;
            myColKf = new colKlienteFurnitore();
            kfFromCache = 0;
            myColNenLlojLlog = new colNenLlojLlogarish();
            nenLlojLlogFromCache = 0;
            myColLlojLlog = new colLlojLlogarish();
            llojLlogFromCache = 0;
            myColKa = new colKonfigurimAmbjenti();
            konfigAmbientiFromCache = 0;
            myColArt = new colArtikujt();
            artFromCache = 0;
            myColTaksa = new colTaksa();
            taksaFromCache = 0;
            myColNjesiAdministrative = new colNjesiAdministrative();
            njesiAdministrativeFromCache = 0;
            myStatusMagazine = new colStatusMagazine_Asete();
            statusMagazineCache = 0;
            myColNjesiArt = new colNjesiteArtikulli();
            njesiArtFromCache = 0;
            myColMonedha = new colMonedhat();
            monedhaFromCache = 0;
            colQendraKosto = new DbQendraKosto.colQendraKosto();
            colQendraNgaCache = 0;
            ColKarakteristikaStandarti = new colKarakteristikaStandarti();
            karakteristikaNgaCache = 0;
            myColObjektiva = new colObjektivaKosto();
            objektivaNgaCache = 0;
            myColLlogariTeMundshmePerQK = new Dictionary<int, colLlogarite>();
            llogariTeMundshmeQkCache = 0;
            myKonfigQk = new clsKonfigurimQK();
            konfigQkCache = 0;
            myColDega = new colDegeAdministrative();
            degaCache = 0;
            myColStruktura = new colStrukturatAdministrative();
            strukturaCache = 0;
            myMonedhaNdermarje = new clsMonedha();
            monedhaFromCache = 0;
            myColTrupiSkemaQK = new colTrupiSkemaQK();
            trupiskemaqkCache = 0;
            myColNivelRegj = new colNivelRegjistrimi();
            nivelRegjFromCache = 0;
            myColPikeShitje = new colPikaShitjeFurnizimi();
            pikeShitjeFromCache = 0;
            myColKushte = new colKusht();
            kushteFromCache = 0;
            myColKartat = new colKarta();
            kartatFromCache = 0;
            myColPerdoruesi = new colPerdoruesit();
            perdoruesiFromCache = 0;
            myColNdermarrjeViti = new colNdermarrjeVitet();
            ndermarrjeVitiFromCache = 0;
            myColPeriudhaKontabel = new List<Tuple<int, clsPeriudhaKontabel>>();
            periudhaKonabelFromCache = 0;
            myColVitet = new colVitet();
            vitiFromCache = 0;
            myColFormatKonfig = new List<Tuple<int, clsFormatiKonfig>>();
            formatKonfigFromCache = 0;
            myColAgjenteShitje = new colAgjenteShitje();
            agjentShitjeFromCache = 0;
            myColAutomjete = new colAutomjete();
            automjeteFromCache = 0;
            myColBankat = new colBankat();
            bankaFromCache = 0;
            myColGrupimDokumentiKoka = new colGrupimDokumentiKoka();
            grupimDokumentiKokaFromCache = 0;
            myColGrupimeKF = new colGrupeKF();
            grupimeKFFromCache = 0;
            myAlternativa = new DataTable("alternativa");
            myAlternativa.Columns.Add("idKonfig", Type.GetType("System.Int32"));
            myAlternativa.Columns.Add("kushtKod", Type.GetType("System.String"));
            myAlternativa.Columns.Add("vlera", Type.GetType("System.String"));
            alternativaCache = 0;
            myKonfigAutorizime = new DataTable("konfigAutorizime");
            myKonfigAutorizime.Columns.Add("idKonfig", Type.GetType("System.Int32"));
            myKonfigAutorizime.Columns.Add("idPerdoruesi", Type.GetType("System.Int32"));
            myKonfigAutorizime.Columns.Add("kaAutorizim", Type.GetType("System.Boolean"));
            konfigAutorizimeCache = 0;
            myColArtikulliPerberes = new List<colArtikulliPerberes>();
            colArtikulliPerberesFromCache = 0;
            myColNdermarrjet = new colNdermarrjet();
            ndermarrjaNgaCache = 0;
            myColFarmaciISKSH = new List<FarmaciISKSH>();
            farmaciISKSHCache = 0;
            myColMedikamentISKSH = new List<MedikamentISKSH>();
            medikamentISKSHCache = 0;
            myColDepoFarmaceutikeISKSH = new List<DepoFarmaceutikeISKSH>();
            depoFarmaceutikeISKSHCache = 0;
            myColFirmeFarmaceutikeISKSH = new List<FirmeFarmaceutikeISKSH>();
            firmeFarmaceutikeISKSHCache = 0;
            myColKodifikimArtikulli = new colKodifikimeArtikulli();
            kodifikimArtikulliCache = 0;
            myColKarakteristikaStandartiTrupi = new colKarakteristikaStandartiTrupi();
            karakteristikaStandartiTrupiCache = 0;
            myListIdLlojAmortizimi = new List<Dictionary<string, int>>();
            idLlojAmortizimiCache = 0;
            myColKurset = new colKurset();
            kursFromCache = 0;
            myColSkemaKontabel = new colSkemaKontabelNew();
            skemaKontabelFromCache = 0;
            myColMonedhatNdermarrjes = new colMonedhat();
            monedhaNdermarrjesFromCache = 0;
            myListKaLlogariQkNdermarrja = new List<Tuple<int, bool>>();
            kaLlogariQkNdermarrja = 0;
            myListEshtePrindQk = new List<Tuple<int, bool>>();
            eshtePrindQk = 0;
            myColNivelCmimesh = new colNiveleCmimesh();
            nivelCmimiFromCache = 0;
        }



        public int getFromCacheTotal()
        {
            //return llogFromCache + kfFromCache + nenLlojLlogFromCache + llojLlogFromCache + konfigAmbientiFromCache + artFromCache + taksaFromCache + njesiAdministrativeFromCache + njesiArtFromCache + monedhaFromCache + colQendraNgaCache + statusMagazineCache + karakteristikaNgaCache + objektivaNgaCache + konfigQkCache + degaCache + strukturaCache + monedhaNdermCache + trupiskemaqkCache;
            return llogFromCache + kfFromCache + nenLlojLlogFromCache + llojLlogFromCache + konfigAmbientiFromCache + artFromCache + taksaFromCache + njesiAdministrativeFromCache + statusMagazineCache + monedhaFromCache + colQendraNgaCache + karakteristikaNgaCache + objektivaNgaCache + llogariTeMundshmeQkCache + konfigQkCache + degaCache + strukturaCache + monedhaNdermCache + trupiskemaqkCache + nivelRegjFromCache + pikeShitjeFromCache + kushteFromCache + kartatFromCache + perdoruesiFromCache + ndermarrjeVitiFromCache + periudhaKonabelFromCache + vitiFromCache + formatKonfigFromCache + agjentShitjeFromCache + automjeteFromCache + bankaFromCache + grupimDokumentiKokaFromCache + grupimeKFFromCache + alternativaCache + konfigAutorizimeCache + colArtikulliPerberesFromCache + ndermarrjaNgaCache + medikamentISKSHCache + farmaciISKSHCache + depoFarmaceutikeISKSHCache + firmeFarmaceutikeISKSHCache + kodifikimArtikulliCache + karakteristikaStandartiTrupiCache + kursFromCache + skemaKontabelFromCache + monedhaNdermarrjesFromCache + kaLlogariQkNdermarrja + eshtePrindQk + nivelCmimiFromCache;
        }

        public transactionCache ShallowCopy()
        {
            return (transactionCache)this.MemberwiseClone();
        }
        public clsLlogari getLlogari(int idLlogari, clsDatabaseKontabilitet db)
        {
            clsLlogari oLlogari = myColLlog.FirstOrDefault(x => x.IdLlogari == idLlogari);
            if (oLlogari == null)
            {
                System.Diagnostics.Debug.WriteLine("DB - Mbush llogarine me id: " + idLlogari);
                oLlogari = new clsLlogari();
                oLlogari.mbushLlogari(db.merrLlogari(idLlogari));
                if (oLlogari.IdLlogari == 0)
                    throw new MyException($"Llogaria me id: {idLlogari} nuk ekziston");
                myColLlog.Add(oLlogari);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Cache - Mbush llogarine: " + idLlogari);
                llogFromCache++;
            }
            return oLlogari;
        }

        public clsLlogari getLlogari(string nrLLogari, int idNdermarrje, clsDatabaseKontabilitet db)
        {

            clsLlogari oLlogari = myColLlog.FirstOrDefault(x => x.NrLlogari == nrLLogari && x.IdNdermarja == idNdermarrje);
            if (oLlogari == null)
            {
                System.Diagnostics.Debug.WriteLine("DB - Mbush llogarine nga : " + nrLLogari);
                oLlogari = new clsLlogari();
                oLlogari.mbushLlogari(db.ktheLlogariSipasKodit(nrLLogari, idNdermarrje));
                if (oLlogari.IdLlogari == 0)
                    throw new MyException($"Llogaria me nr: {nrLLogari} nuk ekziston");
                myColLlog.Add(oLlogari);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Cache - Mbush llogarine: " + nrLLogari);
                llogFromCache++;
            }
            return oLlogari;
        }

        public clsKlientFurnitor getKlientFurnitor(string kodKf, int idNdermarrje, bool meAutorizim, int idPerdorues, clsDatabaseKontabilitet db)
        {
            clsKlientFurnitor oKf = myColKf.FirstOrDefault(x => x.KodKlientFurnitor == kodKf && x.IdNdermarja == idNdermarrje);
            if (oKf == null)
            {
                ImbLogger.LogWarningShitje("DB - Mbush kf me kod: " + kodKf);
                System.Diagnostics.Debug.WriteLine("DB - Mbush kf me kod: " + kodKf);
                oKf = new clsKlientFurnitor();

                if (meAutorizim)
                {
                    oKf.mbushKlientFurnitorSipasKodit(kodKf, idNdermarrje, idPerdorues);
                    if (oKf.IdKlientFurnitor == 0)
                    {
                        ImbLogger.LogErrorShitje($"Nuk ka autorizime per klientin me kod: {kodKf} ose nuk ekziston!");
                        throw new MyException($"Nuk ka autorizime per klientin me kod: {kodKf} ose nuk ekziston!");
                    }

                }
                else
                {
                    oKf.MbushKlientFurnitor(db.ktheKlientFurnitorSipasKodit(kodKf, idNdermarrje));
                    if (oKf.IdKlientFurnitor == 0)
                    {
                        ImbLogger.LogErrorShitje($"Klienti me kod: {kodKf} nuk ekziston!");
                        throw new MyException($"Klienti me kod: {kodKf} nuk ekziston!");
                    }
                }
                    
                myColKf.Add(oKf);
            }
            else
            {
                ImbLogger.LogWarningShitje("Cache - kf: " + kodKf);
                System.Diagnostics.Debug.WriteLine("Cache - kf: " + kodKf);
                kfFromCache++;
            }
            return oKf;
        }

        public clsKlientFurnitor getKlientFurnitor(int idKf, clsDatabaseKontabilitet db)
        {

            clsKlientFurnitor oKf = myColKf.FirstOrDefault(x => x.IdKlientFurnitor == idKf);
            if (oKf == null)
            {
                System.Diagnostics.Debug.WriteLine("DB - Mbush kf me ID: " + idKf);
                oKf = new clsKlientFurnitor();
                oKf.MbushKlientFurnitor(db.merrKlientFurnitorSipasID(idKf));
                if (oKf.IdKlientFurnitor == 0)
                    throw new MyException($"Klienti me id: {idKf} nuk ekziston");
                myColKf.Add(oKf);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Cache - kf: " + idKf);
                kfFromCache++;
            }
            return oKf;
        }
        internal bool eshteKlientSpecifik(int idKf, clsDatabaseKontabilitet dbKont)
        {
            clsKlientFurnitor oKf = myColKf.FirstOrDefault(x => x.IdKlientFurnitor == idKf);
            if (oKf == null)
            {
                return dbKont.eshteKlientSpecifik(idKf);
            }
            System.Diagnostics.Debug.WriteLine("Cache - kf: " + idKf);
            kfFromCache++;
            return oKf.KlientSpecifik;
        }
        public clsNenLlojLlogarish getNenLlojLlogarish(int idNenLlojLlogarie, clsDatabaseKontabilitet db)
        {
            clsNenLlojLlogarish oNenLlojLlogari = myColNenLlojLlog.FirstOrDefault(x => x.IdNenLlojLlogarie == idNenLlojLlogarie);
            if (oNenLlojLlogari == null)
            {
                System.Diagnostics.Debug.WriteLine("DB - Mbush NenLlojLlogarine: " + idNenLlojLlogarie);
                oNenLlojLlogari = new clsNenLlojLlogarish();
                oNenLlojLlogari.mbushNenLlojLlogarish(db.ktheNenLlojLlogarieSipasID(idNenLlojLlogarie));
                if (oNenLlojLlogari.IdNenLlojLlogarie == 0)
                    throw new MyException($"NenLlojLlogaria me id: {idNenLlojLlogarie} nuk ekziston");
                myColNenLlojLlog.Add(oNenLlojLlogari);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Cache - Mbush NenLlojLlogarine: " + idNenLlojLlogarie);
                nenLlojLlogFromCache++;
            }
            return oNenLlojLlogari;
        }
        public clsLlojLlogarish getLlojLlogari(int idLlojLlogari, clsDatabaseKontabilitet db)
        {
            clsLlojLlogarish oLlojLlogari = myColLlojLlog.FirstOrDefault(x => x.IdLlojLlogarie == idLlojLlogari);
            if (oLlojLlogari == null)
            {
                System.Diagnostics.Debug.WriteLine("DB - Mbush LlojLlogarine me id: " + idLlojLlogari);
                oLlojLlogari = new clsLlojLlogarish();
                oLlojLlogari.mbushLlojLlogarish(db.ktheLlojLlogarieSipasID(idLlojLlogari));
                if (oLlojLlogari.IdLlojLlogarie == 0)
                    throw new MyException($"LlojLlogaria me id: {idLlojLlogari} nuk ekziston");
                myColLlojLlog.Add(oLlojLlogari);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Cache - Mbush LlojLlogarine: " + idLlojLlogari);
                llojLlogFromCache++;
            }
            return oLlojLlogari;
        }

        internal clsKodifikimArtikulli getKodifikimArtikulli(int idkodifikimi, clsDatabaseInventari dbKodifikimArtikujsh)
        {
            clsKodifikimArtikulli kodifikimArtikulli = myColKodifikimArtikulli.FirstOrDefault(x => x.IdKodifikimi == idkodifikimi);
            if(kodifikimArtikulli == null)
            {
                System.Diagnostics.Debug.WriteLine($"DB - Mbush kodifikim artikulli me id kodifikimi: {idkodifikimi}");
                kodifikimArtikulli = new clsKodifikimArtikulli();
                kodifikimArtikulli.mbushKodifikimArtikulli(dbKodifikimArtikujsh.merrKodifikimArtikulli(idkodifikimi));

                if (kodifikimArtikulli.IdKodifikimi == 0)
                    throw new MyException($"Kodifikimi i artikullit me id kodifikimi: {idkodifikimi} nuk ekziston");
                myColKodifikimArtikulli.Add(kodifikimArtikulli);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Cache - Mbush kodifikim artikulli me id kodifikimi: {idkodifikimi}");
                kodifikimArtikulliCache++;
            }
            return kodifikimArtikulli;
        }


        public DbShare.clsKonfigurimAmbjenti getKonfigAmbiente(int idKonfigAmbjente, DbShare.clsDatabaseShare data)
        {
            clsKonfigurimAmbjenti ka = myColKa.FirstOrDefault(x => x.IdKonfigAmbjente == idKonfigAmbjente);
            if (ka == null)
            {
                System.Diagnostics.Debug.WriteLine($"DB - Mbush konfigAmbjente me id: {idKonfigAmbjente}");
                ka = new clsKonfigurimAmbjenti();
                ka.mbushKonfigurimAmbjenti(data.ktheKonfigAmbjSipasId(idKonfigAmbjente));
                if (ka.IdKonfigAmbjente == 0)
                    throw new MyException($"konfigAmbjente me id: {idKonfigAmbjente} nuk ekziston");
                myColKa.Add(ka);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Cache - Mbush konfigAmbjente me id: {idKonfigAmbjente}");
                konfigAmbientiFromCache++;
            }
            return ka;
        }
        public DbShare.clsKonfigurimAmbjenti getKonfigAmbiente(string kodKonfigurim, int idNdermarrje, DbShare.clsDatabaseShare data)
        {
            clsKonfigurimAmbjenti ka = myColKa.FirstOrDefault(x => x.KodKonfigAmbjente == kodKonfigurim && x.IdNdermarje == idNdermarrje);
            if (ka == null)
            {
                System.Diagnostics.Debug.WriteLine($"DB - Mbush konfigAmbjente me kod: {kodKonfigurim}");
                ka = new clsKonfigurimAmbjenti();
                ka.mbushKonfigurimAmbjenti(data.ktheKonfigAmbjSipasKod(kodKonfigurim, idNdermarrje, true));
                if (ka.IdKonfigAmbjente == 0)
                    throw new MyException($"Lloji i dokumentit me kod: {kodKonfigurim} nuk ekziston!");
                myColKa.Add(ka);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Cache - Mbush konfigAmbjente: {kodKonfigurim}");
                konfigAmbientiFromCache++;
            }
            return ka;
        }

        public clsArtikulli getArtikull(string kodArtikulli, int idNdermarrje, clsDatabaseInventari db)
        {
            clsArtikulli artikull = myColArt.FirstOrDefault(x => (x.KodArtikulli == kodArtikulli && x.IdNdermarje == idNdermarrje));
            if (artikull == null)
            {
                System.Diagnostics.Debug.WriteLine($"DB - Mbush artikullin nga : {kodArtikulli}");
                artikull = new clsArtikulli();
                artikull.mbushArtikull(db.merrArtikull(kodArtikulli, idNdermarrje));
                if (artikull.IdArtikulli == 0)
                    throw new MyException($"Artikulli me kod: {kodArtikulli} nuk ekziston");
                myColArt.Add(artikull);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Cache - Mbush artikullin: {kodArtikulli}");
                artFromCache++;
            }
            return artikull;
        }

        public clsArtikulli getArtikull(int idArtikulli, clsDatabaseInventari db)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda getArtikull sipas idArtikulli:{idArtikulli}");
            clsArtikulli artikull = myColArt.FirstOrDefault(x => (x.IdArtikulli == idArtikulli));
            if (artikull == null)
            {
                ImbLogger.LogWarningShitje($"DB - Mbush artikullin nga : {idArtikulli}");
                System.Diagnostics.Debug.WriteLine($"DB - Mbush artikullin nga : {idArtikulli}");
                artikull = new clsArtikulli();
                artikull.mbushArtikull(db.merrArtikull(idArtikulli));
                if (artikull.IdArtikulli == 0)
                {
                    ImbLogger.LogErrorShitje($"Artikulli me kod: {idArtikulli} nuk ekziston");
                    throw new MyException($"Artikulli me kod: {idArtikulli} nuk ekziston");
                }
                   
                myColArt.Add(artikull);
            }
            else
            {
                ImbLogger.LogWarningShitje($"Cache - Mbush artikullin: {idArtikulli}");
                System.Diagnostics.Debug.WriteLine($"Cache - Mbush artikullin: {idArtikulli}");
                artFromCache++;
            }
            ImbLogger.LogTraceShitje($"Filloi metoda getArtikull sipas idArtikulli:{idArtikulli}");
            return artikull;
        }

        public clsTaksa getTakse(string kodi, int idNdermarrje, clsDatabaseRegjistrim db)
        {

            clsTaksa takse = myColTaksa.FirstOrDefault(x => (x.KodTaksa == kodi && x.IdNdermarje == idNdermarrje));
            if (takse == null)
            {   
                ImbLogger.LogTraceShitje($"DB - Mbush taksen nga kodi: {kodi}");
                System.Diagnostics.Debug.WriteLine($"DB - Mbush taksen nga kodi: {kodi}");
                takse = new clsTaksa();
                db.ktheTaksaSipasKodi(kodi, idNdermarrje,takse);
                if (takse.IdTaksa == 0)
                {
                    ImbLogger.LogErrorShitje($"Taksa me kod: {kodi} nuk ekziston");
                    throw new MyException($"Taksa me kod: {kodi} nuk ekziston");
                }
                    
                myColTaksa.Add(takse);
            }
            else
            {
                ImbLogger.LogTraceShitje($"Cache - Mbush taksen: {kodi}");
                System.Diagnostics.Debug.WriteLine($"Cache - Mbush taksen: {kodi}");
                taksaFromCache++;
            }
            return takse;
        }

        internal clsNjesiAdministrative getNjesiAdministrative(string kodi, int idNdermarrje, clsDatabaseRegjistrim db)
        {
            clsNjesiAdministrative njesiAdministrative = myColNjesiAdministrative.FirstOrDefault(x => (x.Kodi == kodi && x.IdNdermarje == idNdermarrje));
            if (njesiAdministrative == null)
            {
                ImbLogger.LogTraceShitje($"DB - Mbush njesine administrative nga: { kodi}");
                System.Diagnostics.Debug.WriteLine($"DB - Mbush njesine administrative nga : {kodi}");
                njesiAdministrative = new clsNjesiAdministrative();
                njesiAdministrative.mbushNjesiAdministrative(db.ktheNjesiAdministrativeSipasKoditPaAutorizim(kodi, idNdermarrje));
                if (njesiAdministrative.IdNjesiAdministrative == 0)
                {
                    ImbLogger.LogErrorShitje($"Njesia administrative me kod: {kodi} nuk ekziston");
                    throw new MyException($"Njesia administrative me kod: {kodi} nuk ekziston");
                }
                    
                myColNjesiAdministrative.Add(njesiAdministrative);
            }
            else
            {
                ImbLogger.LogTraceShitje($"Cache - Mbush njesine administrative: {kodi}");
                System.Diagnostics.Debug.WriteLine($"Cache - Mbush njesine administrative: {kodi}");
                njesiAdministrativeFromCache++;
            }
            return njesiAdministrative;
        }
        internal clsNjesiAdministrative getNjesiAdministrative(int idNjesiAdm, clsDatabaseRegjistrim dbNjesiAdministrative)
        {
            clsNjesiAdministrative njesiAdministrative = myColNjesiAdministrative.FirstOrDefault(x => (x.IdNjesiAdministrative == idNjesiAdm));
            if (njesiAdministrative == null)
            {
                System.Diagnostics.Debug.WriteLine($"DB - Mbush njesine administrative nga id: {idNjesiAdm}");
                njesiAdministrative = new clsNjesiAdministrative();
                njesiAdministrative.mbushNjesiAdministrative(dbNjesiAdministrative.ktheNjesiAdministrativeSipasiDPaAutorizime(idNjesiAdm));
                if (njesiAdministrative.IdNjesiAdministrative == 0)
                    throw new MyException($"Njesia administrative me id: {idNjesiAdm} nuk ekziston");
                myColNjesiAdministrative.Add(njesiAdministrative);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Cache - Mbush njesine administrative me id: {idNjesiAdm}");
                njesiAdministrativeFromCache++;
            }
            return njesiAdministrative;
        }
        internal clsDegeAdministrative getDegaAdministrative(int idDega, clsDatabaseRegjistrim db)
        {
            clsDegeAdministrative dega = myColDega.FirstOrDefault(x => (x.IdDegeAdministrative == idDega));
            if (dega == null)
            {
                ImbLogger.LogWarningShitje($"DB - Mbush degen administrative nga id: {idDega}");
                System.Diagnostics.Debug.WriteLine($"DB - Mbush degen administrative nga id: {idDega}");
                dega = new clsDegeAdministrative();
                dega.mbushDegeAdministrative(db.ktheDegeAdministrativeSipasiD(idDega));
                if (dega.IdDegeAdministrative == 0)
                {
                    ImbLogger.LogErrorShitje($"Dega administrative me id: {idDega} nuk ekziston");
                    throw new MyException($"Dega administrative me id: {idDega} nuk ekziston");
                }
                    
                myColDega.Add(dega);
            }
            else
            {
                ImbLogger.LogWarningShitje($"Cache - Mbush degen administrative me id: {idDega}");
                System.Diagnostics.Debug.WriteLine($"Cache - Mbush degen administrative me id: {idDega}");
                degaCache++;
            }
            return dega;
        }
        internal colTrupiSkemaQK getTrupiSkemaQK(int idkoka, clsDatabaseQendraKosto db)
        {
            colTrupiSkemaQK dega = new colTrupiSkemaQK(myColTrupiSkemaQK.Where(x => x.IdKoka == idkoka));
            if (dega.Count == 0)
            {
                System.Diagnostics.Debug.WriteLine($"DB - Mbush trupi skema qendra nga id: {idkoka}");
                dega = new colTrupiSkemaQK();
                dega.mbushTrupiSkema(db.ktheTrupiSkemaQKSipasIdKoka(idkoka));
                if (dega.Count == 0)
                    throw new MyException($"Trupi skema qendra me id: {idkoka} nuk ekziston");
                myColTrupiSkemaQK.AddRange(dega);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Cache - Mbush trupin skema qendra me id: {idkoka}");
                degaCache++;
            }
            return dega;
        }
        internal clsStrukturaAdministrative getStrukturaAdministrative(int id, clsDatabazeListPagesa db)
        {
            clsStrukturaAdministrative struktura = myColStruktura.FirstOrDefault(x => (x.IdStrukturaAdm == id));
            if (struktura == null)
            {
                System.Diagnostics.Debug.WriteLine($"DB - Mbush struktura administrative nga id: {id}");
                struktura = new clsStrukturaAdministrative();
                db.ktheStrukture(id, struktura);
                if (struktura.IdStrukturaAdm == 0)
                    throw new MyException($"Struktura administrative me id: {id} nuk ekziston");
                myColStruktura.Add(struktura);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Cache - Mbush Strukturen administrative me id: {id}");
                strukturaCache++;
            }
            return struktura;
        }
        internal clsStatusMagazine_Asete getStatusMagazine(int idstatus, clsDatabazeAsete db)
        {
            clsStatusMagazine_Asete status = myStatusMagazine.FirstOrDefault(x => (x.IdStatusMagazine == idstatus));
            if (status == null)
            {
                System.Diagnostics.Debug.WriteLine($"DB - Mbush statusi per njesine administrative nga id: {idstatus}");
                status = new clsStatusMagazine_Asete();
                status.mbushStatusMagazineObjekt(db.ktheStatusMagazineSipasID(idstatus));
                if (status.IdStatusMagazine == 0)
                    throw new MyException($"Statusi i magazines me id: {idstatus} nuk ekziston");
                myStatusMagazine.Add(status);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Cache - Mbush status magazine me id: {idstatus}");
                statusMagazineCache++;
            }
            return status;
        }
        internal clsObjektivaKosto getObjektiva(int idobjektiva, clsDatabaseQendraKosto db)
        {
            clsObjektivaKosto objektiva = myColObjektiva.FirstOrDefault(x => (x.Id == idobjektiva));
            if (objektiva == null)
            {
                System.Diagnostics.Debug.WriteLine($"DB - Mbush objektiven nga id: {idobjektiva}");
                objektiva = new clsObjektivaKosto();
                objektiva.mbushObjektivKosto(db.ktheObjektivKosto(idobjektiva));
                if (objektiva.Id == 0)
                    throw new MyException($"Objektiva me id: {idobjektiva} nuk ekziston");
                myColObjektiva.Add(objektiva);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Cache - Mbush objektiven  me id: {idobjektiva}");
                objektivaNgaCache++;
            }
            return objektiva;
        }
        internal colLlogarite GetLlogariTeMundshmePerQk(int idndermarje, clsDatabaseKontabilitet db)
        {
            
            if (myColLlogariTeMundshmePerQK.ContainsKey(idndermarje))
            {
                llogariTeMundshmeQkCache++;
                System.Diagnostics.Debug.WriteLine($"Cache - Mbush llogarite per qk  me id ndermarje: {idndermarje}");
                return myColLlogariTeMundshmePerQK[idndermarje];
            }

            var colLlogariTeMundshmePerQK = new colLlogarite();
            colLlogariTeMundshmePerQK.ktheLLogariteNdermarrjesTeMundshmePerQK(idndermarje, db);
            System.Diagnostics.Debug.WriteLine($"DB - Mbush Llogarite per qk nga id ndermarje: {idndermarje}");
            myColLlogariTeMundshmePerQK.Add(idndermarje, colLlogariTeMundshmePerQK);
            return colLlogariTeMundshmePerQK;
        }
        internal clsKonfigurimQK GetKonfigurimQKNdermarje(int idndermarje, int idperdoruesi, clsDatabaseQendraKosto db)
        {
            if (myKonfigQk.IdNdermarje == idndermarje && myKonfigQk.IdPerdoruesi == idperdoruesi)
            {
                konfigQkCache++;
                System.Diagnostics.Debug.WriteLine($"Cache - Mbush konfigurimi i QK per ndermarjen: {idndermarje}");
                return myKonfigQk;
            }
            System.Diagnostics.Debug.WriteLine($"DB - Mbush Konfigurimin e QK nga idndermarje: {idndermarje}");
            myKonfigQk = new clsKonfigurimQK();
            myKonfigQk.merrSipasIdNdermarje(idndermarje, idperdoruesi, db);
            if(myKonfigQk.IdKonfigurimQK == 0)
            {
                myKonfigQk.IdNdermarje = idndermarje;
                myKonfigQk.IdPerdoruesi = idperdoruesi;
            }
            return myKonfigQk;

        }
        internal clsNjesiArtikulli getNjesiArt(string kodnjesia, int idNdermarrje, clsDatabaseInventari db)
        {
            clsNjesiArtikulli njesiArt = myColNjesiArt.FirstOrDefault(x => (x.KodNjesia == kodnjesia && x.IdNdermarje == idNdermarrje));
            if (njesiArt == null)
            {
                System.Diagnostics.Debug.WriteLine($"DB - Mbush njesine e artikullit nga : {kodnjesia}");
                njesiArt = new clsNjesiArtikulli();
                njesiArt.mbushNjesiArtikulli(db.merrNjesiArtikulliMeKod(kodnjesia, idNdermarrje));
                if (njesiArt.IdNjesia == 0)
                    throw new MyException($"Njesia artikullit me kod: {kodnjesia} nuk ekziston");
                myColNjesiArt.Add(njesiArt);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Cache - Mbush njesine e artikullit: {kodnjesia}");
                njesiArtFromCache++;
            }
            return njesiArt;
        }

        internal clsNjesiArtikulli getNjesiArt(int idNjesia, clsDatabaseInventari db)
        {
            clsNjesiArtikulli njesiArt = myColNjesiArt.FirstOrDefault(x => (x.IdNjesia == idNjesia));
            if (njesiArt == null)
            {
                System.Diagnostics.Debug.WriteLine($"DB - Mbush njesine e artikullit nga : {idNjesia}");
                njesiArt = new clsNjesiArtikulli();
                njesiArt.mbushNjesiArtikulli(db.merrNjesiArtikulli(idNjesia));
                if (njesiArt.IdNjesia == 0)
                    throw new MyException($"Njesia artikullit me id: {idNjesia} nuk ekziston");
                myColNjesiArt.Add(njesiArt);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Cache - Mbush njesine e artikullit: {idNjesia}");
                njesiArtFromCache++;
            }
            return njesiArt;
        }
        /// <summary>
        /// Nuk e merr nga cache por e ngarkon ne cache se mund te duhet ne nje moment te dyte kjo monedhe
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public clsMonedha getMonedheNdermarrje(int idNdermarrje, clsDatabaseAdmin data)
        {
            System.Diagnostics.Debug.WriteLine($"DB - Mbush monedhen e ndermarrjes me id: {idNdermarrje}");
            clsMonedha monedha = myColMonedhatNdermarrjes.FirstOrDefault(x => x.IdNdermarje == idNdermarrje);

            if(monedha == null)
            {
                monedha = new clsMonedha();
                monedha.mbushMonedha(data.ktheMonedhenNdermarrjes(idNdermarrje));
                if (monedha.IdMonedha == 0)
                    throw new MyException($"Monedha me e ndermarrjes me id: {idNdermarrje} nuk ekziston");
                if (!myColMonedha.Exists(x => x.IdMonedha == monedha.IdMonedha))
                {
                    System.Diagnostics.Debug.WriteLine($"Cache - Karikoj monedhen me id: {monedha.IdMonedha} ne cache");
                    myColMonedha.Add(monedha);
                }
                myColMonedhatNdermarrjes.Add(monedha);
            }            
            return monedha;
        }
        public clsMonedha merrMonedheNdermarrjeNgaCacheja(int idndermarje, clsDatabaseAdmin db)
        {
            if (myMonedhaNdermarje.IdNdermarje == idndermarje)
            {
                monedhaNdermCache++;
                System.Diagnostics.Debug.WriteLine($"Cache - Mbush monedha e ndermarjes: {idndermarje}");
                return myMonedhaNdermarje;
            }
            System.Diagnostics.Debug.WriteLine($"DB - Mbush monedha e ndermarjes: {idndermarje}");
            myMonedhaNdermarje = new clsMonedha();
            myMonedhaNdermarje.mbushMonedha(db.ktheMonedhenNdermarrjes(idndermarje));
            if (myMonedhaNdermarje.IdMonedha == 0)
                throw new MyException($"Monedha e ndermarjes me id: {idndermarje} nuk ekziston");
            return myMonedhaNdermarje;
        }
        public clsMonedha getMonedha(int idMonedha, clsDatabaseAdmin data)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda getMonedha sipas idMonedha:{idMonedha}");
            clsMonedha monedha = myColMonedha.FirstOrDefault(x => (x.IdMonedha == idMonedha));
            if (monedha == null)
            {
                ImbLogger.LogTraceShitje($"DB - Mbush monedhen nga id: {idMonedha}");
                System.Diagnostics.Debug.WriteLine($"DB - Mbush monedhen nga id: {idMonedha}");
                monedha = new clsMonedha();
                monedha.mbushMonedha(data.merrMonedhe(idMonedha));
                if (monedha.IdMonedha == 0)
                {
                    ImbLogger.LogErrorShitje($"Monedha me id: {idMonedha} nuk ekziston");
                    throw new MyException($"Monedha me id: {idMonedha} nuk ekziston");
                }
                    
                myColMonedha.Add(monedha);
            }
            else
            {
                ImbLogger.LogTraceShitje($"Cache - Mbush monedhen me id: {idMonedha}");
                System.Diagnostics.Debug.WriteLine($"Cache - Mbush monedhen me id: {idMonedha}");
                monedhaFromCache++;
            }

            ImbLogger.LogTraceShitje($"Mbaroi metoda getMonedha sipas idMonedha:{idMonedha}");
            return monedha;
        }


        public clsMonedha getMonedha(string kodMonedhe, int idNdermarrje, clsDatabaseAdmin data)
        {
            clsMonedha monedha = myColMonedha.FirstOrDefault(x => (x.KodiMonedha == kodMonedhe && x.IdNdermarje == idNdermarrje));
            if (monedha == null)
            {
                ImbLogger.LogWarningShitje($"DB - Mbush monedhen me kod: {kodMonedhe}");
                System.Diagnostics.Debug.WriteLine($"DB - Mbush monedhen me kod: {kodMonedhe}");
                monedha = new clsMonedha();
                monedha.mbushMonedha(data.ktheMonedhen(kodMonedhe, idNdermarrje));
                if (monedha.IdMonedha == 0)
                {
                    ImbLogger.LogErrorShitje($"Monedha me kod: {kodMonedhe} nuk ekziston");
                    throw new MyException($"Monedha me kod: {kodMonedhe} nuk ekziston");
                }
                    
                myColMonedha.Add(monedha);
            }
            else
            {
                ImbLogger.LogWarningShitje($"Cache - Mbush monedhen me kod: {kodMonedhe}");
                System.Diagnostics.Debug.WriteLine($"Cache - Mbush monedhen me kod: {kodMonedhe}");
                monedhaFromCache++;
            }
            return monedha;
        }

        public clsQendraKosto getQendraKosto(int idQendraKosto, clsDatabaseQendraKosto data)
        {
            var qendra = colQendraKosto.FirstOrDefault(x => (x.Id == idQendraKosto));
            if (qendra == null)
            {
                System.Diagnostics.Debug.WriteLine($"DB - Mbush qk me id: {idQendraKosto}");
                qendra = new clsQendraKosto(data.ktheQenderKosto(idQendraKosto));
                if (qendra.Id == 0)
                    throw new MyException($"Qendra e kostos me id: {idQendraKosto} nuk ekziston");
                colQendraKosto.Add(qendra);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Cache - Mbush qendra kosto me id: {idQendraKosto}");
                colQendraNgaCache++;
            }
            return qendra;
        }

        internal clsQendraKosto getQendraKostoByCode(string kodi, int idNderm, clsDatabaseQendraKosto db)
        {
            var qendra = colQendraKosto.FirstOrDefault(x => (x.Kodi == kodi && x.IdNdermarje == idNderm));
            if (qendra == null)
            {
                System.Diagnostics.Debug.WriteLine($"DB - Mbush qk me kod: {kodi}");
                qendra = new clsQendraKosto(kodi, idNderm, db);
                if (qendra.Id == 0)
                    throw new MyException($"Qendra e kostos me kod: {kodi} nuk ekziston");
                colQendraKosto.Add(qendra);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Cache - Mbush qendra kosto me kod: {kodi}");
                colQendraNgaCache++;
            }
            return qendra;
        }

        public clsQendraKosto getQendraQKP(int idndermarje, clsDatabaseQendraKosto data)
        {
            var qendra = colQendraKosto.FirstOrDefault(x => (x.Kodi == "QKP" && x.IdNdermarje == idndermarje));
            if (qendra == null)
            {
                System.Diagnostics.Debug.WriteLine($"DB - Mbush qkp me idndermarje: {idndermarje}");
                qendra = new clsQendraKosto(data.ktheQenderKostoSipasKodit("QKP", idndermarje));
                if (qendra.Id == 0)
                    throw new MyException($"Qendra e kostos QKP me idndermarje: {idndermarje} nuk ekziston");
                colQendraKosto.Add(qendra);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Cache - Mbush qendra kosto QKP me idndermarje: {idndermarje}");
                colQendraNgaCache++;
            }
            return qendra;
        }

        public clsNivelRegjistrimi getNivelRegjistrimi(string kodi, int idNdermarrje, clsDatabaseRegjistrim db)
        {
            clsNivelRegjistrimi oNivelRegj = myColNivelRegj.FirstOrDefault(x => x.Kodi == kodi && x.IdNdermarje == idNdermarrje);
            if (oNivelRegj == null)
            {
                System.Diagnostics.Debug.WriteLine("DB - Mbush nivel regj me kod: " + kodi);
                oNivelRegj = new clsNivelRegjistrimi();
                oNivelRegj.mbushNivelRegjNew(db.ktheNivelRegjistrimiSipasKodit(kodi, Convert.ToString(idNdermarrje)));
                if (oNivelRegj.IdNivel == 0)
                    throw new MyException($"Niveli i regjistrimit me kod: {kodi} nuk ekziston");
                myColNivelRegj.Add(oNivelRegj);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Cache - nivel regjistrimi: " + kodi);
                nivelRegjFromCache++;
            }
            return oNivelRegj;
        }

        internal string ktheAlternativeSipasKushtitDheIdKonfig(int idKonfigAmbjente, string kushtKod, clsDatabaseShare shareDB)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda ktheAlternativeSipasKushtitDheIdKonfig me idKonfigAmbjente:{idKonfigAmbjente}, kushtKod:{kushtKod}");
            var alternativa = myAlternativa.AsEnumerable().Where(x => ((int)x["idKonfig"] == idKonfigAmbjente && (string)x["kushtKod"] == kushtKod)).Select(x=> x["vlera"]).FirstOrDefault();
            string postFixToDebug = $"Mbush alternativa me idKonfigAmbjente: {idKonfigAmbjente} dhe kushtKod:{kushtKod}";
            if (alternativa == null)
            {
                ImbLogger.LogWarningShitje($"DB - { postFixToDebug}");
                System.Diagnostics.Debug.WriteLine($"DB - { postFixToDebug}");
                alternativa = shareDB.ktheAlternativeSipasKushtitDheIdKonfig(idKonfigAmbjente, kushtKod);
                myAlternativa.AddRow(idKonfigAmbjente, kushtKod, alternativa);
            }
            else
            {
                ImbLogger.LogWarningShitje($"Cache - { postFixToDebug }");
                System.Diagnostics.Debug.WriteLine($"Cache - { postFixToDebug}");
                alternativaCache++;
            }
            ImbLogger.LogTraceShitje($"Mbaroi metoda ktheAlternativeSipasKushtitDheIdKonfig me idKonfigAmbjente:{idKonfigAmbjente}, kushtKod:{kushtKod}");
            return (string)alternativa;
        }

        public clsTaksa getTakse(int idTaksa, clsDatabaseRegjistrim db)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda getTakse me idTaksa = {idTaksa}");
            clsTaksa takse = myColTaksa.FirstOrDefault(x => x.IdTaksa == idTaksa);
            if (takse == null)
            {
                ImbLogger.LogWarningShitje($"DB - Mbush taksen nga id: {idTaksa}");
                System.Diagnostics.Debug.WriteLine($"DB - Mbush taksen nga id: {idTaksa}");
                takse = new clsTaksa();
                takse.MbushDataRow(db.ktheTaksaSipasIdDataTable(idTaksa));
                if (takse.IdTaksa == 0)
                {
                    ImbLogger.LogErrorShitje($"Taksa me id: {idTaksa} nuk ekziston");
                    throw new MyException($"Taksa me id: {idTaksa} nuk ekziston");
                }
                    
                myColTaksa.Add(takse);
            }
            else
            {
                ImbLogger.LogWarningShitje($"Cache - Mbush taksen me id: {idTaksa}");
                System.Diagnostics.Debug.WriteLine($"Cache - Mbush taksen me id: {idTaksa}");
                taksaFromCache++;
            }

            ImbLogger.LogTraceShitje($"Mbaroi metoda getTakse me idTaksa = {idTaksa}");
            return takse;
        }

        internal clsDegeAdministrative getDegaAdministrative(string kodDega, int idNdermarrje, clsDatabaseRegjistrim db)
        {
            clsDegeAdministrative dega = myColDega.FirstOrDefault(x => x.Kodi == kodDega && x.IdNdermarje == idNdermarrje);
            if (dega == null)
            {
                ImbLogger.LogWarningShitje($"DB - Mbush degen administrative nga kod: {kodDega}");
                System.Diagnostics.Debug.WriteLine($"DB - Mbush degen administrative nga kod: {kodDega}");
                dega = new clsDegeAdministrative();
                dega.mbushDegeAdministrative(db.ktheDegeAdministrativeSipasKodit(kodDega, idNdermarrje));
                if (dega.IdDegeAdministrative == 0)
                {
                    ImbLogger.LogErrorShitje($"Dega administrative me kod: {kodDega} nuk ekziston");
                    throw new MyException($"Dega administrative me kod: {kodDega} nuk ekziston");
                }
                    
                myColDega.Add(dega);
            }
            else
            {
                ImbLogger.LogWarningShitje($"Cache - Mbush degen administrative me kod: {kodDega}");
                System.Diagnostics.Debug.WriteLine($"Cache - Mbush degen administrative me kod: {kodDega}");
                degaCache++;
            }
            return dega;
        }

        internal clsPikeShitjeFurnizimi getPikeShitjeFurnizimi(string kodPikeShitje, int idNdermarrje, clsDatabaseRegjistrim db)
        {
            clsPikeShitjeFurnizimi pika = myColPikeShitje.FirstOrDefault(x => x.Kodi == kodPikeShitje && x.IdNdermarje == idNdermarrje);
            if (pika == null)
            {
                ImbLogger.LogWarningShitje($"DB - Mbush piken e shitjes/furnizimit nga kod: {kodPikeShitje}");
                System.Diagnostics.Debug.WriteLine($"DB - Mbush piken e shitjes/furnizimit nga kod: {kodPikeShitje}");
                pika = new clsPikeShitjeFurnizimi();
                pika.mbushPikeShitjeFurnizimi(db.kthePikeShitjeFurnizimiSipasKodit(kodPikeShitje, idNdermarrje));
                if (pika.IdPikeShitjeFurnizimi == 0)
                {
                    ImbLogger.LogErrorShitje($"Dega administrative me kod: {kodPikeShitje} nuk ekziston");
                    throw new MyException($"Dega administrative me kod: {kodPikeShitje} nuk ekziston");
                }
                    
                myColPikeShitje.Add(pika);
            }
            else
            {
                ImbLogger.LogWarningShitje($"Cache - Mbush degen administrative me kod: {kodPikeShitje}");
                System.Diagnostics.Debug.WriteLine($"Cache - Mbush degen administrative me kod: {kodPikeShitje}");
                pikeShitjeFromCache++;
            }
            return pika;
        }

        internal clsPerdorues getPerdorues(string username, clsDatabaseAdmin db)
        {
            clsPerdorues perdoruesi = myColPerdoruesi.FirstOrDefault(x => x.PerdoruesUsername == username);
            if (perdoruesi == null)
            {
                System.Diagnostics.Debug.WriteLine($"DB - Mbush perdoruesin nga username: {username}");
                perdoruesi = new clsPerdorues();
                perdoruesi.mbushPerdorues(db.kthePerdoruesSipasUsername(username));
                if (perdoruesi.IdPerdorues == 0)
                    throw new MyException($"Perdoruesi me username: {username} nuk ekziston");
                myColPerdoruesi.Add(perdoruesi);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Cache - Mbush perdoruesin me username: {username}");
                perdoruesiFromCache++;
            }
            return perdoruesi;
        }

        internal clsNdermarrjeViti getNdermarrjeViti(int idViti, int idNdermarrje, clsDatabaseAdmin db)
        {
            clsNdermarrjeViti ndermarrjeViti = myColNdermarrjeViti.FirstOrDefault(x => x.IdViti == idViti && x.IdNdermarrje == idNdermarrje);
            if (ndermarrjeViti == null)
            {
                System.Diagnostics.Debug.WriteLine($"DB - Mbush vitin e ndermarrjes nga idViti: {idViti}");
                ndermarrjeViti = new clsNdermarrjeViti();
                ndermarrjeViti.mbushNdermarrjeViti(db.merrNdermarrjeVitSipasNdermarjesDheVitit(idNdermarrje, idViti));
                if (ndermarrjeViti.IdNderViti == 0)
                    throw new MyException($"Viti i ndermarrjes me id viti: {idViti} nuk ekziston");
                myColNdermarrjeViti.Add(ndermarrjeViti);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Cache - Mbush vitin e ndermarrjes me idViti: {idViti}");
                ndermarrjeVitiFromCache++;
            }
            return ndermarrjeViti;
        }

        internal clsViti getViti(string kodi, int idNdermarrje, clsDatabaseAdmin db)
        {
            clsViti viti = myColVitet.FirstOrDefault(x => x.KodiViti == kodi && x.IdNdermarje == idNdermarrje);
            if (viti == null)
            {
                System.Diagnostics.Debug.WriteLine($"DB - Mbush vitin nga kodi: {kodi}");
                viti = new clsViti();
                viti.mbushViti(db.merrVit(kodi, idNdermarrje));
                if (viti.IdViti == 0)
                    throw new MyException($"Viti me kod: {kodi} nuk ekziston");
                myColVitet.Add(viti);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Cache - Mbush vitin me kod: {kodi}");
                vitiFromCache++;
            }
            return viti;
        }

        internal clsViti getViti(int idViti, clsDatabaseAdmin db)
        {
            clsViti viti = myColVitet.FirstOrDefault(x => x.IdViti == idViti);
            if (viti == null)
            {
                ImbLogger.LogWarningShitje($"DB - Mbush vitin nga id: {idViti}");
                System.Diagnostics.Debug.WriteLine($"DB - Mbush vitin nga id: {idViti}");
                viti = new clsViti();
                viti.mbushViti(db.merrVit(idViti));
                if (viti.IdViti == 0)
                {
                    ImbLogger.LogErrorShitje($"Viti me id: {idViti} nuk ekziston");
                    throw new MyException($"Viti me id: {idViti} nuk ekziston");
                }
                myColVitet.Add(viti);
            }
            else
            {
                ImbLogger.LogWarningShitje($"Cache - Mbush vitin me id: {idViti}");
                System.Diagnostics.Debug.WriteLine($"Cache - Mbush vitin me id: {idViti}");
                vitiFromCache++;
            }
            return viti;
        }

        internal clsPeriudhaKontabel getPeriudhaKontabel(DateTime data, int idNdermarrje, clsDatabaseAdmin db)
        {
            Tuple<int,clsPeriudhaKontabel> periudhaKontabelObj = myColPeriudhaKontabel.FirstOrDefault(x => x.Item1 == idNdermarrje && x.Item2.FillimiPeriudha <= data && x.Item2.MbarimiPeriudha > data);
            if (periudhaKontabelObj == null)
            {
                System.Diagnostics.Debug.WriteLine($"DB - Mbush periudhen kontabel nga data: {data}");
                clsPeriudhaKontabel periudhaKontabel = new clsPeriudhaKontabel();
                periudhaKontabel.MbushPeriudhaKontabel(db.kthePeriudhen(data, idNdermarrje));
                if (periudhaKontabel.IdPeriudha == 0)
                    throw new MyException($"Periudha kontabel per daten: {data} nuk ekziston");
                periudhaKontabelObj = new Tuple<int, clsPeriudhaKontabel>(idNdermarrje, periudhaKontabel);
                myColPeriudhaKontabel.Add(periudhaKontabelObj);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Cache - Mbush periudhen kontabel me date: {data}");
                periudhaKonabelFromCache++;
            }
            return periudhaKontabelObj.Item2;
        }

        
        internal clsPeriudhaKontabel getPeriudhaKontabelById(int idPeriudha, int idNdermarrje, clsDatabaseAdmin db)
        {
            Tuple<int, clsPeriudhaKontabel> periudhaKontabelObj = myColPeriudhaKontabel.FirstOrDefault(x => x.Item1 == idNdermarrje && x.Item2.IdPeriudha == idPeriudha);
            if (periudhaKontabelObj == null)
            {
                System.Diagnostics.Debug.WriteLine($"DB - Mbush periudhen kontabel me id: {idPeriudha}");
                clsPeriudhaKontabel periudhaKontabel = new clsPeriudhaKontabel();
                periudhaKontabel.MbushPeriudhaKontabel(db.kthePeriudhaSipasId(idPeriudha));
                if (periudhaKontabel.IdPeriudha == 0)
                    throw new MyException($"Periudha kontabel me id: {idPeriudha} nuk ekziston");
                periudhaKontabelObj = new Tuple<int, clsPeriudhaKontabel>(idNdermarrje, periudhaKontabel);
                myColPeriudhaKontabel.Add(periudhaKontabelObj);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Cache - Mbush periudhen kontabel me id: {idPeriudha}");
                periudhaKonabelFromCache++;
            }
            return periudhaKontabelObj.Item2;
        }

        public clsKusht getKusht(string kodi, int idKonfigAmbienti, clsDatabaseShare db)
        {
            clsKusht oKusht = myColKushte.FirstOrDefault(x => x.Kodi == kodi && x.IdKonfigurimAmbjente == idKonfigAmbienti);
            if (oKusht == null)
            {
                System.Diagnostics.Debug.WriteLine($"DB - Mbush kusht me kod: {kodi} dhe idkonfigurim {idKonfigAmbienti}");
                oKusht = new clsKusht();
                oKusht.mbushKusht(db.ktheKushtTemplateSipasIDkonfigurimdheKodKushti(idKonfigAmbienti, kodi));
                if (oKusht.IdKusht == 0)
                    System.Diagnostics.Debug.WriteLine($"Kushti me kod: {kodi} nuk ekziston per konfigurimin {idKonfigAmbienti}");
                myColKushte.Add(oKusht);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Cache - kushti: {kodi} per idkonfig {idKonfigAmbienti}");
                kushteFromCache++;
            }
            return oKusht;
        }

        public clsKarta getKarta(string kodi, int idNdermarrje, clsDatabaseRegjistrim db)
        {
            clsKarta oKarta = myColKartat.FirstOrDefault(x => x.Kodi == kodi && x.IdNdermarrje == idNdermarrje);
            if (oKarta == null)
            {
                System.Diagnostics.Debug.WriteLine($"DB - Mbush karte me kod: {kodi}");
                oKarta = new clsKarta();
                oKarta.Mbush(db.MerrKarteSipasKodit(kodi, idNdermarrje));
                if (oKarta.IdKarta == 0)
                    throw new MyException($"Karta e klientit me kod karte: {kodi} nuk ekziston!");
                myColKartat.Add(oKarta);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Cache - karta e klientit: {kodi}");
                kartatFromCache++;
            }
            return oKarta;
        }

        public clsFormatiKonfig getFormatKonfig(int idKonfig, clsDatabaseShare db)
        {
            Tuple<int,clsFormatiKonfig> formatKonfigTuple = myColFormatKonfig.FirstOrDefault(x => x.Item1 == idKonfig);
            if (formatKonfigTuple == null)
            {
                System.Diagnostics.Debug.WriteLine($"DB - Mbush format konfigurimi nga id konfig ambjenti: {idKonfig}");
                clsFormatiKonfig formatKonfig = new clsFormatiKonfig();
                formatKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(db, idKonfig);
                if (formatKonfig.IdFormatKonfig == 0)
                    System.Diagnostics.Debug.WriteLine($"Formati i konfigurimit me id konfig ambjenti: {idKonfig} nuk ekziston!");
                formatKonfigTuple = new Tuple<int, clsFormatiKonfig>(idKonfig, formatKonfig);
                myColFormatKonfig.Add(formatKonfigTuple);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Cache - Mbush format konfigurimi me id konfig ambjenti:: {idKonfig}");
                formatKonfigFromCache++;
            }
            return formatKonfigTuple.Item2;
        }

        public clsAgjentShitje getAgjentShitje(string kodi, int idNdermarrje, clsDatabaseAdmin db)
        {
            clsAgjentShitje agjentShitje = myColAgjenteShitje.FirstOrDefault(x => x.KodiAgjentShitje == kodi && x.IdNdermarje == idNdermarrje);
            if (agjentShitje == null)
            {
                ImbLogger.LogWarningShitje($"DB - Mbush agjent shitje me kod: {kodi}");
                System.Diagnostics.Debug.WriteLine($"DB - Mbush agjent shitje me kod: {kodi}");
                agjentShitje = new clsAgjentShitje();
                agjentShitje.mbushAgjentShitje(db.ktheAgjentShitjeSipasKodit(kodi, idNdermarrje));
                if (agjentShitje.IdAgjentShitje == 0)
                {
                    ImbLogger.LogErrorShitje($"Agjenti i shitjes me kod: {kodi} nuk ekziston!");
                    throw new MyException($"Agjenti i shitjes me kod: {kodi} nuk ekziston!");
                }
                    
                myColAgjenteShitje.Add(agjentShitje);
            }
            else
            {
                ImbLogger.LogWarningShitje($"Cache - Mbush agjent shitje me kod: {kodi}");
                System.Diagnostics.Debug.WriteLine($"Cache - Mbush agjent shitje me kod: {kodi}");
                agjentShitjeFromCache++;
            }
            return agjentShitje;
        }


        public clsAutomjete getAutomjet(string nrShasie, int idNdermarrje, clsDatabaseInventari db)
        {
            clsAutomjete automjet = myColAutomjete.FirstOrDefault(x => x.NrShasie == nrShasie && x.IdNdermarrje == idNdermarrje);
            if (automjet == null)
            {
                ImbLogger.LogWarningShitje($"DB - Mbush automjet me Nr.Shasie: {nrShasie}");
                System.Diagnostics.Debug.WriteLine($"DB - Mbush automjet me Nr.Shasie: {nrShasie}");
                automjet = new clsAutomjete();
                automjet.mbushAutomjet(db.merrAutomjetSipasNdermarrjeDheNrShasie(idNdermarrje, nrShasie));
                if (automjet.IdAutomjeti == 0)
                {
                    ImbLogger.LogErrorShitje($"Automjeti me Nr.Shasie: {nrShasie} nuk ekziston!");
                    throw new MyException($"Automjeti me Nr.Shasie: {nrShasie} nuk ekziston!");
                }
                    
                myColAutomjete.Add(automjet);
            }
            else
            {
                ImbLogger.LogWarningShitje($"Cache - Mbush automjet me Nr.Shasie: {nrShasie}");
                System.Diagnostics.Debug.WriteLine($"Cache - Mbush automjet me Nr.Shasie: {nrShasie}");
                automjeteFromCache++;
            }
            return automjet;
        }

        public clsAutomjete getAutomjet(int idNdermarrje, string targa, clsDatabaseInventari db)
        {
            clsAutomjete automjet = myColAutomjete.FirstOrDefault(x => x.Targa == targa && x.IdNdermarrje == idNdermarrje);
            if (automjet == null)
            {
                System.Diagnostics.Debug.WriteLine($"DB - Mbush automjet me Targe: {targa}");
                automjet = new clsAutomjete();
                automjet.mbushAutomjet(db.merrAutomjetSipasTarges(targa, idNdermarrje));
                if (automjet.IdAutomjeti == 0)
                    throw new MyException($"Automjeti me Targe: {targa} nuk ekziston!");
                myColAutomjete.Add(automjet);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Cache - Mbush automjet me Targe: {targa}");
                automjeteFromCache++;
            }
            return automjet;
        }

        public clsBanka getArkaBanka(int idNdermarrje, string kodi, clsDatabaseArkaBanka db)
        {
            clsBanka arkaBanka = myColBankat.FirstOrDefault(x => x.KodiBanka == kodi && x.IdNdermarje == idNdermarrje);
            if (arkaBanka == null)
            {
                System.Diagnostics.Debug.WriteLine($"DB - Mbush arke/banke me kod: {kodi}");
                arkaBanka = new clsBanka();
                arkaBanka.mbushBank(db.ktheBankeSipasKodit(kodi, idNdermarrje));
                if (arkaBanka.IdBanka == 0)
                    throw new Exception(MessagesResource.Messages["msgArkeBankeNukEkziston"]);
                myColBankat.Add(arkaBanka);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Cache - Mbush arke/banke me kod: {kodi}");
                bankaFromCache++;
            }
            return arkaBanka;
        }

        public colArtikulliPerberes ktheArtikujPerberesSipasIdArtikullitKryesorDheDatesMeTeAfert(int idArtikulli, DateTime dt, clsDatabaseInventari dbInv)
        {
            var artPerb = myColArtikulliPerberes.FirstOrDefault(col => col.Exists(x => x.IdArtikulliKryesor == idArtikulli));
            if(artPerb == null)
            {
                System.Diagnostics.Debug.WriteLine($"DB - Mbush colArtikulliPerberes me idArtikulli: {idArtikulli}");
                artPerb = new colArtikulliPerberes();
                artPerb.mbushArtikujPerberes(dbInv.ktheArtikujPerberesSipasIdArtikullitKryesorDheDatesMeTeAfert(idArtikulli, dt));
                myColArtikulliPerberes.Add(artPerb);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Cache - Mbush colArtikulliPerberes me idArtikulli: {idArtikulli}");
                colArtikulliPerberesFromCache++;
            }
            return artPerb;
        }


        public clsGrupimDokumentiKoka getGrupimDokumentiKoka(string kodi, int idNdermarrje, int grupi, int idPerdoruesi, clsDatabaseRegjistrim db)
        {
            clsGrupimDokumentiKoka grupDokKoka = myColGrupimDokumentiKoka.FirstOrDefault(x => x.Kodi == kodi && x.IdNdermarje == idNdermarrje && x.Grupi == grupi);
            if (grupDokKoka == null)
            {
                ImbLogger.LogWarningShitje($"DB - Mbush grupimin e dokumentave me kod: {kodi}");
                System.Diagnostics.Debug.WriteLine($"DB - Mbush grupimin e dokumentave me kod: {kodi}");
                grupDokKoka = new clsGrupimDokumentiKoka();
                grupDokKoka.mbushGrup(db.merrGrupimSipasKod(kodi, idNdermarrje, grupi, idPerdoruesi));
                if (grupDokKoka.IdGrupimKoka == 0)
                {
                    ImbLogger.LogErrorShitje($"Grupimi i dokumetave me kod: {kodi} nuk ekziston!");
                    throw new MyException($"Grupimi i dokumetave me kod: {kodi} nuk ekziston!");
                }
                    
                myColGrupimDokumentiKoka.Add(grupDokKoka);
            }
            else
            {
                ImbLogger.LogWarningShitje($"Cache - Mbush grupimin e dokumentave me kod: {kodi}");
                System.Diagnostics.Debug.WriteLine($"Cache - Mbush grupimin e dokumentave me kod: {kodi}");
                grupimDokumentiKokaFromCache++;
            }
            return grupDokKoka;
        }

        public clsGrupeKF getGrupimeKF(string kodi, int idNdermarrje, int llojKodifikimi, int llojKF, clsDatabaseKontabilitet db)
        {
            clsGrupeKF grupKF = myColGrupimeKF.FirstOrDefault(x => x.KodGrupi == kodi && x.IdNdermarje == idNdermarrje && x.LlojKodifikimi == llojKodifikimi && x.LlojKF == llojKF);
            if (grupKF == null)
            {
                System.Diagnostics.Debug.WriteLine($"DB - Mbush grupimin e klient/furnitor me kod: {kodi}");
                grupKF = new clsGrupeKF();
                grupKF.Mbush(db.merrKodifikimKFSipasKodLloj(kodi, idNdermarrje, llojKodifikimi, llojKF));
                if (grupKF.IdGrupi == 0)
                    throw new MyException($"Grupimi i klient/furnitor me kod: {kodi} nuk ekziston!");
                myColGrupimeKF.Add(grupKF);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Cache - Mbush grupimin e klient/furnitor me kod: {kodi}");
                grupimeKFFromCache++;
            }
            return grupKF;
        }

        internal bool getAutorizimeSipasIdKonfigDheIdPerdorues(int idKonfigAmbjente, int idPerdoruesi, clsDatabaseShare shareDB)
        {
            var kaAutorizim = myKonfigAutorizime.AsEnumerable().Where(x => ((int)x["idKonfig"] == idKonfigAmbjente && (int)x["idPerdoruesi"] == idPerdoruesi)).Select(x => x["kaAutorizim"]).FirstOrDefault();
            string postFixToDebug = $"Mbush autorizim konfigurimi me idKonfigAmbjente: {idKonfigAmbjente} dhe idPerdoruesi:{idPerdoruesi}";
            if (kaAutorizim == null)
            {
                System.Diagnostics.Debug.WriteLine($"DB - { postFixToDebug}");
                kaAutorizim = shareDB.kaAutorizimKonfigurim(idKonfigAmbjente, idPerdoruesi);
                myKonfigAutorizime.AddRow(idKonfigAmbjente, idPerdoruesi, kaAutorizim);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Cache - { postFixToDebug}");
                konfigAutorizimeCache++;
            }
            return (bool)kaAutorizim;
        }


        public clsNdermarrje getNdermarrje(string kodi, clsDatabaseAdmin db)
        {
            clsNdermarrje ndermarrja = myColNdermarrjet.FirstOrDefault(x => x.NdermarrjeKodi == kodi);
            if (ndermarrja == null)
            {
                System.Diagnostics.Debug.WriteLine("DB - Mbush ndermarrjen me kod: " + kodi);
                ndermarrja = new clsNdermarrje();
                ndermarrja.mbushNdermarrja(db.merrNdermarrje(kodi));
                if (ndermarrja.IdNdermarrje == 0)
                    throw new MyException($"Ndermarrja me kod: {kodi} nuk ekziston");
                myColNdermarrjet.Add(ndermarrja);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Cache - Mbush ndermarrjen: " + kodi);
                ndermarrjaNgaCache++;
            }
            return ndermarrja;
        }

        internal clsNdermarrje getNdermarrjeById(int idNdermarrje, clsDatabaseAdmin dbAdmin)
        {
            clsNdermarrje ndermarrja = myColNdermarrjet.FirstOrDefault(x => x.IdNdermarrje == idNdermarrje);
            if (ndermarrja == null)
            {
                System.Diagnostics.Debug.WriteLine("DB - Mbush ndermarrjen me id: " + idNdermarrje);
                ndermarrja = new clsNdermarrje();
                ndermarrja.mbushNdermarrja(dbAdmin.merrNdermarrje(idNdermarrje));

                if (ndermarrja.IdNdermarrje == 0)
                    throw new MyException($"Ndermarrja me id: {idNdermarrje} nuk ekziston");
                myColNdermarrjet.Add(ndermarrja);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Cache - Mbush ndermarrjen: " + idNdermarrje);
                ndermarrjaNgaCache++;
            }
            return ndermarrja;
        }

        public T getKlientFurnitorISKSH<T>(string kodi, EnumStructTypeISKSH lloji, DbAccess dbAccess)
        {
            object klientFurnitor = null;
            switch (lloji)
            {
                case EnumStructTypeISKSH.DepoFarmaceutikeISKSH:
                    klientFurnitor = getDepoFarmaceutikeISKSH(kodi, dbAccess);
                    break;
                case EnumStructTypeISKSH.FarmaciISKSH:
                    klientFurnitor = getFarmaciISKSH(kodi, dbAccess);
                    break;
                case EnumStructTypeISKSH.FirmeFarmaceutikeISKSH:
                    klientFurnitor = getFirmeFarmaceutikeISKSH(kodi, dbAccess);
                    break;
            }
            return (T)klientFurnitor;
        }

        public DepoFarmaceutikeISKSH getDepoFarmaceutikeISKSH(string kodi, DbAccess dbAccess)
        {
            DepoFarmaceutikeISKSH depo = myColDepoFarmaceutikeISKSH.FirstOrDefault(x => x.KodiDepos == kodi);

            if (depo == null)
            {
                System.Diagnostics.Debug.WriteLine("DB - DepoFarmaceutikeISKSH me kod: " + kodi);
                string condition = $" KodiDepos = '{kodi}'";
                depo = ClsIntegrimFaturaISKSH.MerrObjektNgaDbAccessSipasKushtit<DepoFarmaceutikeISKSH>(EnumStructTypeISKSH.DepoFarmaceutikeISKSH, TablesISKSH.DepoFarmaceutikeISKSH, condition, dbAccess);
                if (depo == null)
                    throw new MyException($"DepoFarmaceutikeISKSH me kod: {kodi} nuk ekziston");
                myColDepoFarmaceutikeISKSH.Add(depo);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Cache - Mbush DepoFarmaceutikeISKSH: " + kodi);
                depoFarmaceutikeISKSHCache++;
            }
            return depo;
        }

        public FarmaciISKSH getFarmaciISKSH(string kodi, DbAccess dbAccess)
        {
            FarmaciISKSH farmaci = myColFarmaciISKSH.FirstOrDefault(x => x.KodFarmacise == kodi);

            if (farmaci == null)
            {
                System.Diagnostics.Debug.WriteLine("DB - FarmaciISKSH me kod: " + kodi);
                string condition = $" KodFarmacise = '{kodi}'";
                farmaci = ClsIntegrimFaturaISKSH.MerrObjektNgaDbAccessSipasKushtit<FarmaciISKSH>(EnumStructTypeISKSH.FarmaciISKSH, TablesISKSH.FarmaciISKSH, condition, dbAccess);
                if (farmaci == null)
                    throw new MyException($"FarmaciISKSH me kod: {kodi} nuk ekziston");
                myColFarmaciISKSH.Add(farmaci);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Cache - Mbush FarmaciISKSH: " + kodi);
                farmaciISKSHCache++;
            }
            return farmaci;
        }

        public FirmeFarmaceutikeISKSH getFirmeFarmaceutikeISKSH(string kodi, DbAccess dbAccess)
        {
            FirmeFarmaceutikeISKSH firme = myColFirmeFarmaceutikeISKSH.FirstOrDefault(x => x.Kodi.ToString() == kodi);

            if (firme == null)
            {
                System.Diagnostics.Debug.WriteLine("DB - FirmeFarmaceutikeISKSH me kod: " + kodi);
                string condition = $" Kodi = {kodi}";
                firme = ClsIntegrimFaturaISKSH.MerrObjektNgaDbAccessSipasKushtit<FirmeFarmaceutikeISKSH>(EnumStructTypeISKSH.FirmeFarmaceutikeISKSH, TablesISKSH.FirmeFarmaceutikeISKSH, condition, dbAccess);
                if (firme == null)
                    throw new MyException($"FirmeFarmaceutikeISKSH me kod: {kodi} nuk ekziston");
                myColFirmeFarmaceutikeISKSH.Add(firme);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Cache - Mbush FirmeFarmaceutikeISKSH: " + kodi);
                firmeFarmaceutikeISKSHCache++;
            }
            return firme;
        }

        public MedikamentISKSH getMedikamentISKSH(string kodiBarit, DbAccess dbAccess)
        {
            MedikamentISKSH medikament = myColMedikamentISKSH.FirstOrDefault(x => x.KodiBarit == kodiBarit);
            if (medikament == null)
            {
                System.Diagnostics.Debug.WriteLine("DB - Mbush MedikamentISKSH me kod: " + kodiBarit);
                string condition = $"KodiBarit = '{kodiBarit}'";
                medikament = ClsIntegrimFaturaISKSH.MerrObjektNgaDbAccessSipasKushtit<MedikamentISKSH>(EnumStructTypeISKSH.MedikamentISKSH, TablesISKSH.MedikamentISKSH, condition, dbAccess);
                if (medikament == null || string.IsNullOrEmpty(medikament.KodiBarit))
                    throw new MyException($"MedikamentISKSH me kod: {kodiBarit} nuk ekziston");
                myColMedikamentISKSH.Add(medikament);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Cache - Mbush MedikamentISKSH: " + kodiBarit);
                medikamentISKSHCache++;
            }
            return (MedikamentISKSH)medikament;
        }

        internal clsKarakteristikaStandarti getKarakteristikaStandarti(int idStandarti, int idKodifikimArtikulli, int idNdermarrje, bool merrNgaDb, clsDatabazeAsete data)
        {
            clsKarakteristikaStandarti karaketeristika = colKarakteristikaStandarti.FirstOrDefault(x => (x.IdKodifikimArtikulli == idKodifikimArtikulli && idStandarti == x.IdStandart));
            if (karaketeristika == null)
            {
                if (merrNgaDb == false) return new clsKarakteristikaStandarti();

                System.Diagnostics.Debug.WriteLine($"DB - Mbush karakteristika me idKodifikimArtikulli: {idKodifikimArtikulli} dhe idStandarti: {idStandarti}");
                karaketeristika = new clsKarakteristikaStandarti(data.ktheKonfigurimStandartiTeNdermarrjesSipasStatGrupStandart(idKodifikimArtikulli, idStandarti, idNdermarrje));
                if (karaketeristika.IdKarakteristika == 0)
                {
                    throw new MyException($"Karakteristika standarti me idKodifikimArtikulli: {idKodifikimArtikulli} dhe idStandarti: {idStandarti} nuk ekziston ");
                }
                colKarakteristikaStandarti.Add(karaketeristika);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Cache - Mbush karakteristika me idKodifikimArtikulli: {idKodifikimArtikulli} dhe idStandarti: {idStandarti}");
                karakteristikaNgaCache++;
            }
            return karaketeristika;
        }

        internal clsKarakteristikaStandartiTrupi getKarakteristikaStandartiTrupi(int idKokaKarakteristikStandart, int idStatusMagazine, clsDatabazeAsete dbAsete)
        {
            clsKarakteristikaStandartiTrupi karaketeristika = myColKarakteristikaStandartiTrupi.FirstOrDefault(x => (x.IdKokaKarakteristikStandart == idKokaKarakteristikStandart && x.IdStatusMagazine == idStatusMagazine));
            if (karaketeristika == null)
            {
                System.Diagnostics.Debug.WriteLine($"DB - Mbush karakteristika standarti trupi me id koke: {idKokaKarakteristikStandart} dhe id status magazine: {idStatusMagazine}");
                karaketeristika = new clsKarakteristikaStandartiTrupi();
                karaketeristika.mbushKarakteristikaTrupiObjekt(dbAsete.ktheKonfigurimStandartiTrupiSipasIDKokaIDStatus(idKokaKarakteristikStandart, idStatusMagazine));

                if (karaketeristika.IdKokaKarakteristikStandart == 0)
                    throw new MyException($"Trupi karakteristika standarti me id koke: {idKokaKarakteristikStandart} dhe id status magazine: {idStatusMagazine} nuk ekziston");
                myColKarakteristikaStandartiTrupi.Add(karaketeristika);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Cache - Mbush karakteristika standarti trupi me id koke: {idKokaKarakteristikStandart} dhe id status magazine: {idStatusMagazine}");
                karakteristikaStandartiTrupiCache++;
            }
            return karaketeristika;
        }

        internal int getIdLlojAmortizimiNormaAmortizimiSipasId(int idArtikullLlojAmort, clsDatabazeAseteAbstract moduliAsete)
        {
            Dictionary<string,int> llojAmortizimi = myListIdLlojAmortizimi.Find(x => x["idArtikullLlojAmort"] == idArtikullLlojAmort);
            if (llojAmortizimi == null)
            {
                System.Diagnostics.Debug.WriteLine($"DB - Merr lloj amortizimi me idArtikullLlojAmort: {idArtikullLlojAmort}");
                int idLlojAmortizimi = moduliAsete.ktheIDLlojAmortizimiNormaAmortizimiSipasID(idArtikullLlojAmort);

                llojAmortizimi = new Dictionary<string, int>();
                llojAmortizimi.Add("idArtikullLlojAmort", idArtikullLlojAmort);
                llojAmortizimi.Add("idLlojAmortizimi", idLlojAmortizimi);

                myListIdLlojAmortizimi.Add(llojAmortizimi);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Cache - Merr lloj amortizimi me idArtikullLlojAmort: {idArtikullLlojAmort}");
                idLlojAmortizimiCache++;
            }
            return llojAmortizimi["idLlojAmortizimi"];
        }

        internal clsKurset ktheKursinSipasMonedhesAndDates(int idMonedha, DateTime data, clsDatabaseAdmin dbAdmin)
        {
            clsKurset kursi = myColKurset.Where(x => x.IdMonedha == idMonedha && x.DataKursit <= data).OrderByDescending(x=>x.DataKursit).ThenByDescending(x => x.IdKursi).FirstOrDefault();
            if (kursi == null)
            {
                System.Diagnostics.Debug.WriteLine($"DB - Merr kurs me idMonedha: {idMonedha} dhe data: {data.ToShortDateString()}");
                kursi = new clsKurset(idMonedha, data, dbAdmin);
                myColKurset.Add(kursi);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Cache - Merr kurs me idMonedha: {idMonedha} dhe data: {data.ToShortDateString()}");
                kursFromCache++;
            }
            return kursi.ShallowCopy();
        }

        internal clsKurset ktheKursinSipasMonedhesAndDatesAndLloji(int idMonedha, DateTime data, int lloji, clsDatabaseAdmin dbAdmin)
        {
            clsKurset kursi = myColKurset.Where(x => x.IdMonedha == idMonedha && x.DataKursit <= data && x.LlojKursi == lloji).OrderByDescending(x => x.DataKursit).ThenByDescending(x => x.IdKursi).FirstOrDefault();
            if (kursi == null || kursi.IdKursi < 1)
            {
                System.Diagnostics.Debug.WriteLine($"DB - Merr kurs me idMonedha: {idMonedha} dhe data: {data.ToShortDateString()}");
                kursi = new clsKurset(idMonedha, data, lloji, dbAdmin);
                myColKurset.Add(kursi);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Cache - Merr kurs me idMonedha: {idMonedha} dhe data: {data.ToShortDateString()}");
                kursFromCache++;
            }
            return kursi.ShallowCopy();
        }

        internal bool ekzistonLlogaria(string nrLlog, int idNdermarje, clsDatabaseKontabilitet dbKontab)
        {

            clsLlogari llogaria = myColLlog.Find(x => x.NrLlogari == nrLlog && x.IdNdermarja == idNdermarje);
            if (llogaria == null)
            {
                System.Diagnostics.Debug.WriteLine($"DB - Merr llogari me nrLlog: {nrLlog} dhe idNdermarje: {idNdermarje}");
                return dbKontab.ekzistonLlogari(nrLlog, idNdermarje);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Cache - Merr llogari me nrLlog: {nrLlog} dhe idNdermarje: {idNdermarje}");
                llogFromCache++;
            }
            return llogaria.IdLlogari > 0;
        }
        
        internal clsSkemaKontabelNew getSkemaKontabelNew(int idSkema, clsDatabaseKontabilitet dbSkemaKontNew)
        {

            clsSkemaKontabelNew skema = myColSkemaKontabel.Find(x => Convert.ToInt32(x.IdSkemeKont) == idSkema);
            if (skema == null)
            {
                System.Diagnostics.Debug.WriteLine($"DB - Merr skemen kontabel me idSkema: {idSkema}");
                skema = new clsSkemaKontabelNew(idSkema, dbSkemaKontNew);
                myColSkemaKontabel.Add(skema);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Cache - Merr skemen kontabel me idSkema: {idSkema}");
                skemaKontabelFromCache++;
            }
            return skema.ShallowCopy();
        }

        internal bool kaLlogariShpernadrjeQKNdermarrja(int idNdermarrje, clsDatabaseQendraKosto db)
        {
            Tuple<int, bool> ndermObj = myListKaLlogariQkNdermarrja.FirstOrDefault(x=>x.Item1 == idNdermarrje);//new Tuple<int, bool>(1, false);
            if (ndermObj == null)
            {
                colLlogariShperndarjeQK colLlog = new colLlogariShperndarjeQK(idNdermarrje, db);
                ndermObj = new Tuple<int, bool>(idNdermarrje, colLlog.Count > 0);
                myListKaLlogariQkNdermarrja.Add(ndermObj);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Cache kaLlogariShpernadrjeQKNdermarrja - { idNdermarrje}");
                kaLlogariQkNdermarrja++;
            }
            return ndermObj.Item2;
        }

        internal bool eshtePrindQendraKosto(int idQk, clsDatabaseQendraKosto db)
        {
            Tuple<int, bool> qkObj = myListEshtePrindQk.FirstOrDefault(x => x.Item1 == idQk);//new Tuple<int, bool>(1, false);
            if (qkObj == null)
            {
                bool eshtePrind = db.eshtePrindQendraKosto(idQk);
                qkObj = new Tuple<int, bool>(idQk, eshtePrind);
                myListEshtePrindQk.Add(qkObj);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Cache eshtePrindQendraKosto - { idQk}");
                eshtePrindQk++;
            }
            return qkObj.Item2;
        }

        internal clsNivelCmimi getNivelCmimi(string pershkrim, int idNdermarrje, clsDatabaseInventari db)
        {
            clsNivelCmimi nivelCmimi = myColNivelCmimesh.FirstOrDefault(x => (x.KodNivelCmimi == pershkrim && x.IdNdermarje == idNdermarrje));
            if (nivelCmimi == null)
            {
                System.Diagnostics.Debug.WriteLine($"DB - Mbush nivelin e cmimit nga : {pershkrim}");
                nivelCmimi = new clsNivelCmimi();
                nivelCmimi.mbushNivelCmimi(db.ktheNivelCmimiSipasKodit(pershkrim, idNdermarrje));
                if (nivelCmimi.IdNivelCmimi == 0)
                    throw new MyException($"Niveli i cmimit me pershkrim: {pershkrim} nuk ekziston");
                myColNivelCmimesh.Add(nivelCmimi);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Cache - Mbush nivelin e cmimit: {pershkrim}");
                nivelCmimiFromCache++;
            }
            return nivelCmimi;
        }

        internal clsNivelCmimi getNivelCmimiBaze(int lloji, int idNdermarrje, clsDatabaseInventari db)
        {
            clsNivelCmimi nivelCmimi = myColNivelCmimesh.FirstOrDefault(x => (x.NivelCmimiBaze == true && x.LlojiNivelCmimi == lloji && x.IdNdermarje == idNdermarrje));
            if (nivelCmimi == null)
            {
                System.Diagnostics.Debug.WriteLine($"DB - Mbush nivelin e cmimit baze me lloj : {lloji} per ndermarrjen {idNdermarrje}");
                nivelCmimi = new clsNivelCmimi();
                nivelCmimi.mbushNivelCmimi(db.merrNivelCmimiBaze(idNdermarrje, lloji));
                if (nivelCmimi.IdNivelCmimi == 0)
                    throw new MyException($"Niveli i cmimit baze me lloj: {lloji} nuk ekziston");
                myColNivelCmimesh.Add(nivelCmimi);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Cache - Mbush nivelin e cmimit baze me lloj : {lloji} per ndermarrjen {idNdermarrje}");
                nivelCmimiFromCache++;
            }
            return nivelCmimi;
        }
    }
}