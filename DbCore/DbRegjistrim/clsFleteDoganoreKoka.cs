using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DbCore.DbKontabiliteti;
using System.Data;
using DbCore.DbAdmin;
using System.Resources;
using System.Globalization;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbRegjistrim
{ /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne koken e fletes doganore
    ///  (Te dhenat  merren nga tabela : T_FLETEDOGANOREKOKA)
    /// </summary>
    public class clsFleteDoganoreKoka
    {
        #region Atribute

        private int idFleteDoganoreKoka;
        private String nrDok;
        private DateTime dtDok;
        private DateTime dtRegjistrimi;
        private int idMonedha;
        private string kodMonedha;
        private decimal kursi;
        private decimal vlFaturuar;
        private decimal vlMb;
        private decimal vlTransport;
        private decimal vlSiguracion;
        private decimal vlTjera;
        private decimal vlDoganim;
        private int idStatusDok;
        private int idNderm;
        private int idNdermVit;
        private int idKonfigAmbjente;
        private int idNivel;
        private int idDokNga;
        private int idNivelGjenerues;
        private int idKonfigGjenerues;
        private int idGjenerues;
        private int importExport;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private int idPerdoruesi;
        private colFleteDoganoreTrupi oColTrupi;
        private colFleteDoganoreTaksa oColTaksat;
        private clsKokaFleteKontabel oFleteKontabel;
        private colFleteDoganoreTVSH oColTVSH;
        private DataRow rreshti;
        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases me parametra
        /// </summary>
        /// <param name="dtDk">date dokumenti</param>
        /// <param name="dtRegj"> date regjistrimi</param>
        /// <param name="id"> id e ritese e fletes doganore</param>
        /// <param name="idMon"> id e monedhes me te cilen lidhet</param>
        /// <param name="idnderm"> id e ndermarjes qe i perket</param>
        /// <param name="idndermvit">id e ndermarje vit qe i perket</param>
        /// <param name="idStatus"> id e gjendjes</param>
        /// <param name="kodMon"> kodi i monedhes</param>
        /// <param name="kurs"> kursi i monedhes</param>
        /// <param name="nrDk">nr dokumenti</param>
        /// <param name="vlDog"> vlefta e doganes</param>
        /// <param name="vlFat"> vlefta e faturuar</param>
        /// <param name="vlM"> vlefta ne monedhen baze</param>
        /// <param name="vlSig"> vlefta e siguracionit</param>
        /// <param name="vlTj"> vlefta te tjera</param>
        /// <param name="vlTransp"> vlefta e transportit</param>
        /// <param name="idkonfig">id e konfigurimit</param>
        /// <param name="iddoknga">id e dokumentit nga gjenerohet ne rastet e modifikimit</param>
        /// <param name="idgjenerues">id e dokumentit nga gjenerohet nga nje ambjent tjeter</param>
        /// <param name="idkonfiggjenerues">id e konfigurimit te dokumentit nga gjenerohet</param>
        /// <param name="idnivel">id e nivelit </param>
        /// <param name="idnivelgjenerues">id e nivelit te dokumentit nga gjenerohet</param>
        public clsFleteDoganoreKoka(int id, string nrDk, DateTime dtDk, DateTime dtRegj,
            int idMon, string kodMon, decimal kurs, decimal vlFat, decimal vlM, decimal vlTransp, decimal vlSig, decimal vlTj, decimal vlDog,
            int idStatus, int idnderm, int idndermvit, int idkonfig, int idnivel, int iddoknga, int idnivelgjenerues, int idkonfiggjenerues, int idgjenerues, int importexport, int idperdoruesi)
        {
            idFleteDoganoreKoka = id;
            nrDok = nrDk;
            dtDok = dtDk;
            dtRegjistrimi = dtRegj;
            idMonedha = idMon;
            kodMonedha = kodMon;
            kursi = kurs;
            idNderm = idnderm;
            idNdermVit = idndermvit;
            idKonfigAmbjente = idkonfig;
            idNivel = idnivel;
            idDokNga = iddoknga;
            idNivelGjenerues = idnivelgjenerues;
            idKonfigGjenerues = idkonfiggjenerues;
            idGjenerues = idgjenerues;
            idPerdoruesi = idperdoruesi;
            this.importExport = importexport;
            oColTrupi = new colFleteDoganoreTrupi();
            // oColDetajim = new colFleteDoganoreDetajim();
            oColTaksat = new colFleteDoganoreTaksa();
            oColTVSH = new colFleteDoganoreTVSH();
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="id">id e kokes se fletes doganore</param>
        public clsFleteDoganoreKoka(int id)
        {
            clsDatabaseRegjistrim dbFleteDogKoka = new clsDatabaseRegjistrim();
            mbushFleteDoganoreKok(dbFleteDogKoka.ktheFleteDoganoreKokaSipasId(id));
            dbFleteDogKoka.Dispose();
        }

        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsFleteDoganoreKoka()
        {
        }

        public clsFleteDoganoreKoka(DataRow rreshti)
        {
            
            mbushFleteDoganoreKok(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdFleteDoganoreKoka
        {
            get { return idFleteDoganoreKoka; }
            set { idFleteDoganoreKoka = value; }
        }

        /// <summary>
        /// Kthen/Vendos nr e dokumentit.
        /// </summary>
        public String NrDok
        {
            get { return nrDok; }
            set { nrDok = value; }
        }

        /// <summary>
        /// Kthen/Vendos daten e dokumentit.
        /// </summary>
        public DateTime DtDok
        {
            get { return dtDok; }
            set { dtDok = value; }
        }

        /// <summary>
        /// Kthen/Vendos daten e regjistrimit.
        /// </summary>
        public DateTime DtRegjistrimi
        {
            get { return dtRegjistrimi; }
            set { dtRegjistrimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e monedhes
        /// </summary>
        public int IdMonedha
        {
            get { return idMonedha; }
            set { idMonedha = value; }
        }

        /// <summary>
        /// Kthen/Vendos kodin e monedhes.
        /// </summary>
        public String KodMonedha
        {
            get { return kodMonedha; }
            set { kodMonedha = value; }
        }

        /// <summary>
        /// Kthen/Vendos kursin e monedhes.
        /// </summary>
        public Decimal Kursi
        {
            get { return kursi; }
            set { kursi = value; }
        }

        /// <summary>
        /// Kthen/Vendos vleften e faturuar.
        /// </summary>
        public Decimal VlFaturuar
        {
            get { return vlFaturuar; }
            set { vlFaturuar = value; }
        }

        /// <summary>
        /// Kthen/Vendos vleften ne monedhen baze.
        /// </summary>
        public Decimal VlMb
        {
            get { return vlMb; }
            set { vlMb = value; }
        }

        /// <summary>
        /// Kthen/Vendos vleften e transportit
        /// </summary>
        public Decimal VlTransport
        {
            get { return vlTransport; }
            set { vlTransport = value; }
        }

        /// <summary>
        /// Kthen/Vendos vleften e siguracionit.
        /// </summary>
        public Decimal VlSiguracion
        {
            get { return vlSiguracion; }
            set { vlSiguracion = value; }
        }

        /// <summary>
        /// Kthen/Vendos vlefta te tjera.
        /// </summary>
        public Decimal VlTjera
        {
            get { return vlTjera; }
            set { vlTjera = value; }
        }

        /// <summary>
        /// Kthen/Vendos vleften e doganimit.
        /// </summary>
        public Decimal VlDoganim
        {
            get { return vlDoganim; }
            set { vlDoganim = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e ndermarjes qe i perket.
        /// </summary>
        public int IdNdermarrje
        {
            get { return idNderm; }
            set { idNderm = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e ndermarje viti qe i perket.
        /// </summary>
        public int IdNdermarrjeVit
        {
            get { return idNdermVit; }
            set { idNdermVit = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e gjendjes.
        /// <example > ruajtur, draft, stornuar, fshire etj</example>
        /// </summary>
        public int IdStatusDok
        {
            get { return idStatusDok; }
            set { idStatusDok = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e konfigurimit.
        /// </summary>
        public int IdKonfigAmbjente
        {
            get { return idKonfigAmbjente; }
            set { idKonfigAmbjente = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e nivelit.
        /// </summary>
        public int IdNivel
        {
            get { return idNivel; }
            set { idNivel = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e dokumentit nga gjenerohet ne rastet e modifikimit.
        /// </summary>
        public int IdDokNga
        {
            get { return idDokNga; }
            set { idDokNga = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e nivelit te dokumentit nga gjenerohet
        /// </summary>
        public int IdNivelGjenerues
        {
            get { return idNivelGjenerues; }
            set { idNivelGjenerues = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e konfigurimit te dokumentit nga gjenerohet
        /// </summary>
        public int IdKonfigGjenerues
        {
            get { return idKonfigGjenerues; }
            set { idKonfigGjenerues = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e dokumentit nga gjenerohet nga ambjente te tjera.
        /// </summary>
        public int IdGjenerues
        {
            get { return idGjenerues; }
            set { idGjenerues = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e perdoruesit
        /// </summary>
        public int IdPerdoruesi
        {
            get { return idPerdoruesi; }
            set { idPerdoruesi = value; }
        }
        /// <summary>
        /// Kthen/Vendos import export 1-import 2-export
        /// </summary>
        public int ImportExport
        {
            get { return importExport; }
            set { importExport = value; }
        }
        /// <summary>
        /// Kthen/Vendos nje koleksion me trupa te fletes doganore.
        /// </summary>
        public colFleteDoganoreTrupi OColTrupi
        {
            get { return oColTrupi; }
            set { oColTrupi = value; }
        }

        public clsKokaFleteKontabel OFleteKontabel
        {
            get { return oFleteKontabel; }
            set { oFleteKontabel = value; }
        }

        /// <summary>
        /// Kthen/Vendos nje koleksion me taksa.
        /// </summary>
        public colFleteDoganoreTaksa OColTaksat
        {
            get { return oColTaksat; }
            set { oColTaksat = value; }
        }
        /// <summary>
        /// Kthen/Vendos nje koleksion me tvsh.
        /// </summary>
        public colFleteDoganoreTVSH OColTVSH
        {
            get { return oColTVSH; }
            set { oColTVSH = value; }
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

        public clsMesazh krijoFleteDoganore(string nrDk, DateTime dtDk, DateTime dtRegj, int idMon, string kodMon, decimal kurs, decimal vlFat, decimal vlM, decimal vlTransp, decimal vlSig, decimal vlTj, decimal vlDog, int idStatus, int idnderm, int idndermvit, int idkonfig, int idnivel, int iddoknga, int idnivelgjenerues, int idkonfiggjenerues, int idgjenerues, int importexport, int idperdoruesi, colFleteDoganoreTrupi coltrupi, colFleteDoganoreTaksa coltaksa, colFleteDoganoreTVSH coltvsh, int idperiudha, bool mekontabilizim, out string shfaqmesazhapolupe, DbQendraKosto.colTrupiQendraKosto trupivjeterqendra, int idGjuha, ResourceManager rm, CultureInfo ci)
        {
            shfaqmesazhapolupe = "jo";
            nrDok = nrDk;
            dtDok = dtDk;
            dtRegjistrimi = dtRegj;
            idMonedha = idMon;
            kodMonedha = kodMon;
            kursi = kurs;
            idNderm = idnderm;
            idNdermVit = idndermvit;
            idKonfigAmbjente = idkonfig;
            idNivel = idnivel;
            idDokNga = iddoknga;
            idNivelGjenerues = idnivelgjenerues;
            idKonfigGjenerues = idkonfiggjenerues;
            idGjenerues = idgjenerues;
            idPerdoruesi = idperdoruesi;
            vlFaturuar = vlFat;
            vlMb = vlM;
            vlTransport = vlTransp;
            vlSiguracion = vlSig;
            vlTjera = vlTj;
            vlDoganim = vlDog;
            idStatusDok = idStatus;
            this.importExport = importexport;
            oColTrupi = coltrupi;
            oColTaksat = coltaksa;
            oColTVSH = coltvsh;
            clsMesazh mesazh = kontrollo();
            if (!mesazh.Status)
                return mesazh;
            oFleteKontabel = new clsKokaFleteKontabel();
            if (mekontabilizim)
            {
                DbQendraKosto.colObjektivaKosto objektivat;
                List<double> vleratobjektiva; List<double> vleratobjektivamonbaze;
                List<int> idllogobj;
                string pershkrimi = "";

                if (importexport == 1)
                    pershkrimi = "Nga Flete Doganore-Import";
                else pershkrimi = "Nga Flete Doganore-Export";
                const int idllojdok = 70;
                const int idkategoria = 8;
                oFleteKontabel = DbKontabiliteti.clsKokaFleteKontabel.gjeneroKontabilizimFleteDoganore(idFleteDoganoreKoka, idNivel, idKonfigAmbjente, dtDok, nrDok, idNderm, idNdermVit, idPerdoruesi, dtRegjistrimi, coltaksa, pershkrimi, 0, idllojdok, idperiudha, idkategoria, coltvsh, out objektivat, out vleratobjektiva, out vleratobjektivamonbaze, out idllogobj, 0, 0, 0, out  shfaqmesazhapolupe, trupivjeterqendra, idGjuha, rm, ci);
            }
            return new clsMesazh(true, "Dokumenti u krijua me sukses!");
        }

        public clsMesazh krijoFleteDoganoreImport(string nrDk, DateTime dtDk, DateTime dtRegj, string kodMon, decimal kurs, decimal vlFat, decimal vlM, decimal vlTransp, decimal vlSig, decimal vlTj, decimal vlDog, int idStatus, int idnderm, int idndermvit, int idkonfig, int idnivel, int iddoknga, int idnivelgjenerues, int idkonfiggjenerues, int idgjenerues, int importexport, int idperdoruesi, colFleteDoganoreTrupi coltrupi, colFleteDoganoreTaksa coltaksa, colFleteDoganoreTVSH coltvsh, int idperiudha, bool mekontabilizim, int idGjuha, ResourceManager rm, CultureInfo ci)
        {
            DbAdmin.clsMonedha mon = new DbAdmin.clsMonedha();
            mon.mbushMonedhen(kodMon, idnderm);
            idMonedha = mon.IdMonedha;
            string shfaqmesazhapolupe = "jo";
            return krijoFleteDoganore(nrDk, dtDk, dtRegj, idMonedha, kodMon, kurs, vlFat, vlM, vlTransp, vlSig, vlTj, vlDog, idStatus, idnderm, idndermvit, idkonfig, idnivel, iddoknga, idnivelgjenerues, idkonfiggjenerues, idgjenerues, importexport, idperdoruesi, coltrupi, coltaksa, coltvsh, idperiudha, mekontabilizim, out shfaqmesazhapolupe, new DbQendraKosto.colTrupiQendraKosto(), idGjuha, rm, ci);
        }
      
        private clsMesazh kontrollo()
        {
            if (nrDok == "")
                return new clsMesazh(false, "Numri i dokumentit nuk mund te jete bosh");
         
            if (importExport == 1 && this.OColTVSH.Count==0)
               return new clsMesazh(false, "Duhet te kete te pakten nje nivel TVSH te zgjedhur!");

            if (dtDok == null || dtDok.ToShortDateString() == "01/01/0100")
                return new clsMesazh(false, "Zgjidhni daten e dokumentit!");
            if (dtRegjistrimi == null || dtRegjistrimi.ToShortDateString() == "01/01/0100")
                return new clsMesazh(false, "Zgjidhni daten e regjistrimit!");
                if (!DbCore.DbAdmin.clsMonedha.ekziston(kodMonedha, idNderm))
            return new clsMesazh(false, "Monedha nuk ekziston!");
           



            return new clsMesazh(true, "Kontrollet u kaluan me sukses!");
        }


        /// <summary>
        /// Ruan nje objekt flete doganore koka se bashku me trupin dhe taksat perkatese
        /// Nje objekt koka fleta doganore ka nje koleksion me trupin dhe taksat , 
        /// ruajtja e nje koka flete doganore imponon ruajtjen edhe te nje colection-i me trupin dhe taksat
        /// Mqs cdo rresht i ri qe shtohet ne DB kerkon thirrjen e nje SP-je me parametra dhe koka e fletes doganore sebashku me trupin dhe taksat konsiderohet si nje regjistrim,
        /// perdoret nje transaksion qe imponon regjistrimin e rregullt te nje  flete doganore sebashku me trupin dhe taksat
        /// </summary>
        /// <param name="dtDk">date dokumenti</param>
        /// <param name="dtRegj"> date regjistrimi</param>
        /// <param name="id"> id e ritese e fletes doganore</param>
        /// <param name="idMon"> id e monedhes me te cilen lidhet</param>
        /// <param name="idnderm"> id e ndermarjes qe i perket</param>
        /// <param name="idndermvit">id e ndermarje vit qe i perket</param>
        /// <param name="idStatus"> id e gjendjes</param>
        /// <param name="kurs"> kursi i monedhes</param>
        /// <param name="nrDk">nr dokumenti</param>
        /// <param name="vlDog"> vlefta e doganes</param>
        /// <param name="vlFat"> vlefta e faturuar</param>
        /// <param name="vlM"> vlefta ne monedhen baze</param>
        /// <param name="vlSig"> vlefta e siguracionit</param>
        /// <param name="vlTj"> vlefta te tjera</param>
        /// <param name="vlTransp"> vlefta e transportit</param>
        /// <param name="idkonfig">id e konfigurimit</param>
        /// <param name="iddoknga">id e dokumentit nga gjenerohet ne rastet e modifikimit</param>
        /// <param name="idgjenerues">id e dokumentit nga gjenerohet nga nje ambjent tjeter</param>
        /// <param name="idkonfiggjenerues">id e konfigurimit te dokumentit nga gjenerohet</param>
        /// <param name="idnivel">id e nivelit </param>
        /// <param name="idnivelgjenerues">id e nivelit te dokumentit nga gjenerohet</param>
        /// <param name="oColTrupi">koleksioni i trupit</param>
        /// <param name="oColTaksat">koleksioni i taksave</param>
        /// <param name="oFleteKontabel">fleta kontabel</param>
        /// <param name="meKontabilizim">tregon nese fleta doganore kontabilizohet</param>
        ///  <returns> kthen nje obj clsMesazh per te identifikuar statusin e ruajtjes se te dhenave ne DB</returns>
        public clsMesazh ruajFleteDoganoreKokaMeTransaksion(out int id, string nrDk, DateTime dtDk, DateTime dtRegj, int idMon, decimal kurs, decimal vlFat, decimal vlM, decimal vlTransp, decimal vlSig, decimal vlTj, decimal vlDog, int idStatus, int idnderm, int idndermvit, int idkonfig, int idnivel, int iddoknga, int idnivelgjenerues, int idkonfiggjenerues, int idgjenerues, colFleteDoganoreTrupi oColTrupi, colFleteDoganoreTaksa oColTaksat, clsKokaFleteKontabel oFleteKontabel, bool meKontabilizim, colFleteDoganoreTVSH oColTVSH, clsDatabaseRegjistrim dbRegj, int idperdoruesi, ResourceManager rm, CultureInfo ci)
        {
            id = 0;
            //transaksioni per te ruajtur             
            clsMesazh mesazh;
           
                id = dbRegj.ruajFleteDoganoreKoka(id, nrDk, dtDk, dtRegj, idMon, kurs, vlFat, vlM, vlTransp, vlSig, vlTj, vlDog, idStatus, idnderm, idndermvit, idkonfig, idnivel, iddoknga, idnivelgjenerues, idkonfiggjenerues, idgjenerues, importExport, idperdoruesi);
                if (id == 0)
                    return new clsMesazh(false, rm.GetString("msgGabimGjateRuajtesSeKokesSeFletesDoganore", ci));
                foreach (clsFleteDoganoreTrupi o in oColTrupi)
                {
                    o.IdFleteDoganoreKoka = id;
                    mesazh = o.ruajFleteDoganoreTrupiDheDetajim(o.IdFleteDoganoreTrupi, o.IdFleteDoganoreKoka, o.IdFatura, o.NrDok, o.DtDok, o.OColDetajim, dbRegj);
                    if (!mesazh.Status)
                        return mesazh;
                }
                foreach (clsFleteDoganoreTaksa o in oColTaksat)
                {
                    o.IdFleteDoganoreKoka = id;
                    int idT;
                    mesazh = dbRegj.ruajFleteDoganoreTaksa(out idT, o.IdFleteDoganoreKoka, o.IdTaksa, o.Pershkrimi, o.Vlefta, o.Tvsh);
                    o.IdFleteDoganoreTaksa = idT;
                    if (!mesazh.Status)
                        return mesazh;
                }
                foreach (clsFleteDoganoreTVSH o in oColTVSH)
                {
                    o.IdFleteDoganoreKoka = id;
                    int idT;
                    mesazh = dbRegj.ruajFleteDoganoreTVSH(out idT, o.IdFleteDoganoreKoka, o.IdTaksa, o.Pershkrimi, o.VleftaFaturuar, o.VleftaTvsh,o.Aqt);
                    o.IdFleteDoganoreTVSH = idT;
                    if (!mesazh.Status)
                        return mesazh;
                }
                if (meKontabilizim == true)
                {
                    oFleteKontabel.IdGjenerues = id;
                    mesazh = oFleteKontabel.Ruaj(new clsDatabaseKontabilitet(dbRegj));
                    if (!mesazh.Status)
                        return mesazh;
                }
                return new clsMesazh(true, rm.GetString("labelRaportMesazhRuajtjaPerfundoiSukses", ci));
            
        }

        private clsMesazh kontrolloFleteDoganore(out bool kaNdryshimNumri, clsDatabaseRegjistrim dbRegj, IDictionary<string, object> hfNrAutoregjistrime)
        {
            kaNdryshimNumri = false;
            clsMesazh mes = new clsMesazh();
            if (dbRegj.ekzistonFleteDoganore(NrDok, DtDok, IdNdermarrje, IdNdermarrjeVit, IdKonfigAmbjente))
            {
                mes = new clsMesazh(false, "Ekziston nje regjistrim i nje Flete Doganore me te njejtat te dhena!");
                return mes;
            }
            if (hfNrAutoregjistrime != null)
            {
                mes = kontrolloNrAutoFleteDoganore(out kaNdryshimNumri, dbRegj, hfNrAutoregjistrime);
                if (!mes.Status)
                    return mes;
            }
            if (kaNdryshimNumri)
                return mes;
            return new clsMesazh(true, "Kontrolli i fletes doganore u krye me sukses!");
        }

        private clsMesazh kontrolloNrAutoFleteDoganore(out bool kaNdryshimNumri, clsDatabaseRegjistrim dbRegj, IDictionary<string, object> hfregjistrime)
        {
            clsDatabaseAdmin db = new clsDatabaseAdmin(dbRegj );
            List<NrAuto> list = DbAdmin.clsNrAutom.kontrollogjithenumrat(db, hfregjistrime, this.dtDok);
            if (NrAuto.ktheVlerenEre(list, "NrDok") != "")
                this.nrDok = NrAuto.ktheVlerenEre(list, "NrDok");            
            DbCore.clsMesazh mes = NrAuto.ruajvlera(out kaNdryshimNumri, list, this.dtDok, this.idPerdoruesi, this.idNderm, db);
            return new clsMesazh(mes.Status, mes.PershkrimMesazhi);
        }

        /// <summary>
        /// Ruan objektin e  kokes se fletes doganore ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsFleteDoganoreKoka.ruajFleteDoganoreKokaMeTransaksion"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
        public clsMesazh ruaj(IDictionary<string, object> hfNrAutoregjistrime, bool meKontabilizim, ResourceManager rm, CultureInfo ci)
        {
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            clsMesazh u_ruajt;
            bool kaNdryshimNumri;
            try
            {
                data.beginTransaksion();
                u_ruajt = kontrolloFleteDoganore(out kaNdryshimNumri, data, hfNrAutoregjistrime);
                if (!u_ruajt.Status)
                {
                    data.rollbackTransaksion();
                    return u_ruajt;
                }
                int id = 0;
                u_ruajt = ruajFleteDoganoreKokaMeTransaksion(out id, this.NrDok, this.DtDok, this.DtRegjistrimi, this.IdMonedha, this.Kursi, this.VlFaturuar, this.VlMb, this.VlTransport, this.VlSiguracion, this.VlTjera, this.VlDoganim, this.IdStatusDok, this.IdNdermarrje, this.IdNdermarrjeVit, this.IdKonfigAmbjente, this.IdNivel, this.IdDokNga, this.IdNivelGjenerues, this.IdKonfigGjenerues, this.IdGjenerues, this.OColTrupi, this.OColTaksat, this.OFleteKontabel, meKontabilizim, this.OColTVSH, data, this.idPerdoruesi, rm, ci);
                this.idFleteDoganoreKoka = id;
                if (!u_ruajt.Status)
                {
                    data.rollbackTransaksion();
                    return u_ruajt;
                }

                data.commitTransaksion();
                return u_ruajt;
            }
            catch (Exception)
            {
                data.rollbackTransaksion();
                return new clsMesazh(false, "Nodhi nje gabim gjate ruajtjes!");
            }
        }

        /// <summary>
        /// Modifikon nje objekt koka fleta doganore sebashku me te trupin dhe taksat
        /// Nje objekt koka fleta doganore ka nje koleksion me trupin dhe taksat perkates , 
        /// modifikimi e nje dokumenti imponon modifikimin edhe te nje colection-i me trupin dhe taksat
        /// Mqs cdo rresht i ri qe modifikon ne DB kerkon thirrjen e nje SP-je me parametra dhe koka flete doganore bashke me trupin dhe taksat konsiderohet si nje regjistrim,
        /// perdoret nje transaksion qe imponon modifikimin e rregullt te nje kokes fleta doganore sebashku me trupin dhe taksat
        /// 1. merret dokumenti eksistues  fletes doganore dhe kalohet ne gjendjen 20 te modifikimit
        /// 2. ruhet dokumenti i ri i fletes doganore  se bashku me trupin dhe taksat
        /// </summary>
        /// <param name="dtDk">date dokumenti</param>
        /// <param name="dtRegj"> date regjistrimi</param>
        /// <param name="id"> id e ritese e fletes doganore</param>
        /// <param name="idMon"> id e monedhes me te cilen lidhet</param>
        /// <param name="idnderm"> id e ndermarjes qe i perket</param>
        /// <param name="idndermvit">id e ndermarje vit qe i perket</param>
        /// <param name="idStatus"> id e gjendjes</param>
        /// <param name="kurs"> kursi i monedhes</param>
        /// <param name="nrDk">nr dokumenti</param>
        /// <param name="vlDog"> vlefta e doganes</param>
        /// <param name="vlFat"> vlefta e faturuar</param>
        /// <param name="vlM"> vlefta ne monedhen baze</param>
        /// <param name="vlSig"> vlefta e siguracionit</param>
        /// <param name="vlTj"> vlefta te tjera</param>
        /// <param name="vlTransp"> vlefta e transportit</param>
        /// <param name="idkonfig">id e konfigurimit</param>
        /// <param name="iddoknga">id e dokumentit nga gjenerohet ne rastet e modifikimit</param>
        /// <param name="idgjenerues">id e dokumentit nga gjenerohet nga nje ambjent tjeter</param>
        /// <param name="idkonfiggjenerues">id e konfigurimit te dokumentit nga gjenerohet</param>
        /// <param name="idnivel">id e nivelit </param>
        /// <param name="idnivelgjenerues">id e nivelit te dokumentit nga gjenerohet</param>
        /// <param name="oColTrupi">koleksioni i trupit</param>
        /// <param name="oColTaksat">koleksioni i taksave</param>
        /// <param name="oFleteKontabel">fleta kontabel</param>
        /// <param name="meKontabilizim">tregon nese fleta doganore kontabilizohet</param>
        /// <returns> kthen nje obj clsMesazh per te identifikuar statusin e modifikimit se te dhenave ne DB</returns>
        public clsMesazh modifikoFleteDoganoreMeTransaksion(int id, string nrDk, DateTime dtDk, DateTime dtRegj, int idMon, decimal kurs, decimal vlFat, decimal vlM, decimal vlTransp, decimal vlSig, decimal vlTj, decimal vlDog, int idStatus, int idnderm, int idndermvit, int idkonfig, int idnivel, int iddoknga, int idnivelgjenerues, int idkonfiggjenerues, int idgjenerues, int importexport, colFleteDoganoreTrupi oColTrupi, colFleteDoganoreTaksa oColTaksat, clsKokaFleteKontabel oFleteKontabel, bool meKontabilizim, colFleteDoganoreTVSH oColTVSH, int idperdoruesi, out int idkokare, ResourceManager rm, CultureInfo ci)
        {
            idkokare = 0;
            clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim();
            //dbRegj.krijoManager();
            dbRegj.beginTransaksion();
            clsMesazh mesazh = new clsMesazh();
            DbCore.clsMesazh mesazhKont = new DbCore.clsMesazh(true);
            try
            {   //obj koka qe i kalohet si parameter eshte si duhet te modifikohet
                clsFleteDoganoreKoka kokaEkzistuese = new clsFleteDoganoreKoka(id);
                //clsFleteDoganoreKoka kokaEkzistuese = this.merrFleteDoganoreKokaSipasId(koka.IdFleteDoganoreKoka);
                if (string.IsNullOrEmpty(kokaEkzistuese.NrDok) || kokaEkzistuese.IdStatusDok == 2)
                {
                    dbRegj.rollbackTransaksion();
                    return new clsMesazh(false, rm.GetString("msgDokumentiKaNdryshuarHapeniPerseri", ci));
                }
                kokaEkzistuese.IdStatusDok = 2;  //duhet vendosur nje status i pershtatshem per kete modifikim
                mesazh = dbRegj.modifikoFleteDoganoreKoka(kokaEkzistuese.IdFleteDoganoreKoka, kokaEkzistuese.NrDok, kokaEkzistuese.DtDok, kokaEkzistuese.DtRegjistrimi, kokaEkzistuese.IdMonedha, kokaEkzistuese.Kursi, kokaEkzistuese.VlFaturuar, kokaEkzistuese.VlMb, kokaEkzistuese.VlTransport, kokaEkzistuese.VlSiguracion, kokaEkzistuese.VlTjera, kokaEkzistuese.VlDoganim, kokaEkzistuese.IdStatusDok, kokaEkzistuese.importExport, idperdoruesi);
                iddoknga = kokaEkzistuese.IdFleteDoganoreKoka;
                DbQendraKosto.clsDatabaseQendraKosto dbqendra = new DbQendraKosto.clsDatabaseQendraKosto(dbRegj );
                kokaEkzistuese.OFleteKontabel = new clsKokaFleteKontabel();
                clsDatabaseKontabilitet dbkontab = new clsDatabaseKontabilitet(dbRegj );
                clsKokaFleteKontabel newclsKokaFleteKontabel = new clsKokaFleteKontabel(kokaEkzistuese.IdFleteDoganoreKoka, 8, dbkontab);
                //if (newclsKokaFleteKontabel.IdStatusDokumenti == 0)
                //{
                    kokaEkzistuese.OFleteKontabel = newclsKokaFleteKontabel;
                    DbQendraKosto.clsKokaQendraKosto kokaqendra = new DbQendraKosto.clsKokaQendraKosto();
                    kokaqendra.KtheKokaQKSipasIDGjeneruesDheKonfigMeTrup(newclsKokaFleteKontabel.IdKokaFleteKontabel, newclsKokaFleteKontabel.IdKonfigAmbjente, dbqendra);
                    if (kokaqendra.IdKoka != 0 && kokaqendra.IdKoka != -1)
                    {
                        kokaEkzistuese.OFleteKontabel.KokaQendraKosto = kokaqendra;
                    }
                    else kokaEkzistuese.OFleteKontabel.KokaQendraKosto = new DbQendraKosto.clsKokaQendraKosto();
                    //if (dbkontab.merrKokaFleteKontabelSipasIDDokAndIDLlojDok(kokaEkzistuese.IdFleteDoganoreKoka, 70).Count > 0)
                    //{
                    //    DbKontabiliteti.clsKokaFleteKontabel kokaFleteKontabel = dbkontab.merrKokaFleteKontabelSipasIDDokAndIDLlojDok(kokaEkzistuese.IdFleteDoganoreKoka, 70)[0];
                    oFleteKontabel.IdDokNga = kokaEkzistuese.OFleteKontabel.IdKokaFleteKontabel;
                    oFleteKontabel.KokaQendraKosto.IdDokNga = kokaEkzistuese.OFleteKontabel.KokaQendraKosto.IdKoka;
                    if (kokaEkzistuese.OFleteKontabel.IdKokaFleteKontabel != 0)
                        mesazhKont = kokaEkzistuese.OFleteKontabel.ModifikoFleteKontabel(true, dbkontab);
                //}
                if (mesazh.Status && mesazhKont.Status)
                {
                    mesazh = ruajFleteDoganoreKokaMeTransaksion(out id, nrDk, dtDk, dtRegj, idMon, kurs, vlFat, vlM, vlTransp, vlSig, vlTj, vlDog, idStatus, idnderm, idndermvit, idkonfig, idnivel, iddoknga, idnivelgjenerues, idkonfiggjenerues, idgjenerues, oColTrupi, oColTaksat, oFleteKontabel, meKontabilizim, oColTVSH, dbRegj, idperdoruesi, rm, ci);
                    idkokare = id;
                    if (mesazh.Status)
                    {
                        dbRegj.commitTransaksion();
                        mesazh = new clsMesazh(true, rm.GetString("msgModifikimiMeSukses", ci));
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

                    return mesazh;
                }
            }
            catch (Exception ce)
            {
                dbRegj.rollbackTransaksion();

                return new clsMesazh(false, ce.Message);
            }
        }

        /// <summary>
        /// Modifikon objektin e  kokes se fletes doganore ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsFleteDoganoreKoka.modifikoFleteDoganoreMeTransaksion"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo</returns>
        public clsMesazh modifiko(bool meKontabilizim, bool lidhur, ResourceManager rm, CultureInfo ci)
        {
            clsMesazh u_modifikua;
            int idkokare = 0;
            if (lidhur == false)
            {
                u_modifikua = modifikoFleteDoganoreMeTransaksion(this.IdFleteDoganoreKoka, this.NrDok, this.DtDok, this.DtRegjistrimi, this.IdMonedha, this.Kursi, this.VlFaturuar, this.VlMb, this.VlTransport, this.VlSiguracion, this.VlTjera, this.VlDoganim, this.IdStatusDok, this.IdNdermarrje, this.IdNdermarrjeVit, this.IdKonfigAmbjente, this.IdNivel, this.IdDokNga, this.IdNivelGjenerues, this.IdKonfigGjenerues, this.IdGjenerues, this.ImportExport, this.OColTrupi, this.OColTaksat, this.OFleteKontabel, meKontabilizim, this.oColTVSH, this.idPerdoruesi, out idkokare, rm, ci);
                this.idFleteDoganoreKoka = idkokare;
            }
            else
            {
                clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
                u_modifikua = data.modifikoFleteDoganoreKoka(this.IdFleteDoganoreKoka, this.NrDok, this.DtDok, this.DtRegjistrimi, this.IdMonedha, this.Kursi, this.VlFaturuar, this.VlMb, this.VlTransport, this.VlSiguracion, this.VlTjera, this.VlDoganim, this.IdStatusDok, this.importExport, this.idPerdoruesi);
                data.Dispose();
            }
            return u_modifikua;

        }

        /// <summary>
        /// fshin nje objekt dokument flete doganore sebashku me te trupin, faturat  dhe kontabilitetin perkates
        /// Nje objekt dokument flete doganore ka nje koleksion me trupin,faturat dhe kontabilitetin , 
        /// fshirja e nje dokumenti flete doganore imponon fshirjen edhe te nje colection-i me trupin, faturat dhe kontabilitetin
        /// Mqs cdo rresht i ri qe fshihet ne DB kerkon thirrjen e nje SP-je me parametra dhe dokumenti i flete doganore bashke me trupin,faturat dhe kontabilitetin konsiderohet si nje regjistrim,
        /// perdoret nje transaksion qe imponon fshirjen e rregullt te nje  dokumenti flete doganore sebashku me trupin, faturat dhe kontabilitetin e tij
        ///1. merr kontabilitetet eksistuese te dokumentit flete doganore dhe e stornon
        ///2. ben fshirjen e trupit, fatures dhe koken e dokumentit flete doganore

        /// </summary>
        /// <param name="id"> id e kokes se fletes doganore qe do fshihet</param>
        /// <param name="oColTrupi">koleksioni i trupit</param>
        /// <param name="oColTaksat">koleksioni i taksave</param>
        /// <returns> kthen nje obj clsMesazh per te identifikuar statusin e fshirjes se te dhenave ne DB</returns>
        public clsMesazh fshiFleteDoganore(int id)
        {

            clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim();
            //dbRegj.krijoManager();
            dbRegj.beginTransaksion();
            clsMesazh mesazh = new clsMesazh(true);
            DbCore.clsMesazh mesazhKont = new DbCore.clsMesazh(true);
            try
            {
                clsDatabaseKontabilitet dbkontab = new clsDatabaseKontabilitet(dbRegj );
                clsFleteDoganoreKoka kokaEkzistuese = new clsFleteDoganoreKoka(id);
                kokaEkzistuese.OFleteKontabel = new clsKokaFleteKontabel();
                DbQendraKosto.clsDatabaseQendraKosto dbqendra = new DbQendraKosto.clsDatabaseQendraKosto(dbRegj );
                if (mesazhKont.Status)
                {
                    clsKokaFleteKontabel newclsKokaFleteKontabel = new clsKokaFleteKontabel(kokaEkzistuese.IdFleteDoganoreKoka, 8, dbkontab);
                    if (newclsKokaFleteKontabel.IdKokaFleteKontabel != 0)
                    {
                        clsKokaFleteKontabel kokaFk = newclsKokaFleteKontabel;
                      
                        if (kokaFk.IdKokaFleteKontabel != 0)
                        {
                            mesazh = kokaFk.fshiupd(dbkontab);
                            if (!mesazh.Status)
                                return mesazh;

                        }
                    }
                }
                else
                {
                    dbRegj.rollbackTransaksion();

                    mesazh.Status = false;
                    mesazh.PershkrimMesazhi = mesazhKont.PershkrimMesazhi;
                    return mesazh;
                }

                if (mesazhKont.Status)
                {
                    kokaEkzistuese.IdStatusDok = 2;  //duhet vendosur nje status i pershtatshem per kete modifikim

                    mesazh = dbRegj.modifikoFleteDoganoreKoka(kokaEkzistuese.IdFleteDoganoreKoka, kokaEkzistuese.NrDok, kokaEkzistuese.DtDok, kokaEkzistuese.DtRegjistrimi, kokaEkzistuese.IdMonedha, kokaEkzistuese.Kursi, kokaEkzistuese.VlFaturuar, kokaEkzistuese.VlMb, kokaEkzistuese.VlTransport, kokaEkzistuese.VlSiguracion, kokaEkzistuese.VlTjera, kokaEkzistuese.VlDoganim, kokaEkzistuese.IdStatusDok, kokaEkzistuese.ImportExport, kokaEkzistuese.idPerdoruesi);

                    if (mesazh.Status)
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
                    mesazh.PershkrimMesazhi = mesazhKont.PershkrimMesazhi;
                    return mesazh;
                }
            }
            catch (Exception ce)
            {
                dbRegj.rollbackTransaksion();

                return new clsMesazh(false, ce.Message);
            }
        }

        /// <summary>
        /// Fshin objektin e  kokes se fletes doganore ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.fshiFleteDoganoreKokaSipasId"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh fshi()
        {

            clsMesazh u_fshi = fshiFleteDoganore(this.IdFleteDoganoreKoka);
            return u_fshi;
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush koken e fleteve doganore nga databaza
        /// </summary>
        /// <param name="dbDataRowFleteDoganore">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushFleteDoganoreKok(DataRow dbDataRowFleteDoganore)
        {
            if (dbDataRowFleteDoganore != null)
            {
                try
                {
                    int.TryParse(dbDataRowFleteDoganore["IDFLETEDOGANORE"].ToString(), out idFleteDoganoreKoka);
                    nrDok = dbDataRowFleteDoganore["NRDOK"].ToString();
                    DateTime.TryParse(dbDataRowFleteDoganore["DTDOK"].ToString(), out dtDok);
                    DateTime.TryParse(dbDataRowFleteDoganore["DTREGJ"].ToString(), out dtRegjistrimi);
                    int.TryParse(dbDataRowFleteDoganore["IDMONEDHA"].ToString(), out idMonedha);
                    decimal.TryParse(dbDataRowFleteDoganore["KURSI"].ToString(), out kursi);
                    decimal.TryParse(dbDataRowFleteDoganore["VLFATURUAR"].ToString(), out vlFaturuar);
                    decimal.TryParse(dbDataRowFleteDoganore["VLMB"].ToString(), out vlMb);
                    decimal.TryParse(dbDataRowFleteDoganore["VLTRANSPORT"].ToString(), out vlTransport);
                    decimal.TryParse(dbDataRowFleteDoganore["VLSIGURACION"].ToString(), out vlSiguracion);
                    decimal.TryParse(dbDataRowFleteDoganore["VLTJERA"].ToString(), out vlTjera);
                    decimal.TryParse(dbDataRowFleteDoganore["VLDOGANIM"].ToString(), out vlDoganim);
                    int.TryParse(dbDataRowFleteDoganore["IDSTATUSDOK"].ToString(), out idStatusDok);
                    int.TryParse(dbDataRowFleteDoganore["IDNDERM"].ToString(), out idNderm);
                    int.TryParse(dbDataRowFleteDoganore["IDNDERMVIT"].ToString(), out idNdermVit);
                    int.TryParse(dbDataRowFleteDoganore["IDKONFIGAMBJENTE"].ToString(), out idKonfigAmbjente);
                    int.TryParse(dbDataRowFleteDoganore["IDNIVEL"].ToString(), out idNivel);
                    int.TryParse(dbDataRowFleteDoganore["IDDOKNGA"].ToString(), out idDokNga);
                    int.TryParse(dbDataRowFleteDoganore["IDNIVELGJENERUES"].ToString(), out idNivelGjenerues);
                    int.TryParse(dbDataRowFleteDoganore["IDKONFIGGJENERUES"].ToString(), out idKonfigGjenerues);
                    int.TryParse(dbDataRowFleteDoganore["IDGJENERUES"].ToString(), out idGjenerues);
                    int.TryParse(dbDataRowFleteDoganore["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    int.TryParse(dbDataRowFleteDoganore["IMPORTEXPORT"].ToString(), out importExport);
                    DateTime.TryParse(dbDataRowFleteDoganore["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRowFleteDoganore["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    if (dbDataRowFleteDoganore["MONEDHAKOD"] != null)
                        kodMonedha = dbDataRowFleteDoganore["MONEDHAKOD"].ToString();
                    oColTrupi = new colFleteDoganoreTrupi();
                    oColTaksat = new colFleteDoganoreTaksa();
                    oColTVSH = new colFleteDoganoreTVSH();
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se kokes se fleteve doganore nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}
