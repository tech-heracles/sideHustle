using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Resources;
using DbCore.DbAdmin;
using DbCore.DbKontabiliteti;
using DbCore.DbQendraKosto;
using DbCore.DbShare;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Messages;
using static System.Convert;

namespace DbCore.DbRegjistrim
{
    public class clsVeprimeKFKoka
    {
        #region Attribute

        private colVeprimeKFTrupi _colVeprimeKfTrupi;
        private string _nrLlogKundraParti;
        private string _kodMonedha;
        private string _kodKlientFurnitor;
        private bool _meKontabilizim;
        private DateTime dtKrijimi;

        #endregion

        #region Properties

        /// <summary>
        /// kthen trupin e veprime kf 
        /// </summary>
        public colVeprimeKFTrupi ColVeprimeKFTrupi
        {
            get
            {
                if(IdVeprimeKFKoka > 0)
                    _colVeprimeKfTrupi.MbushVeprimeKfTrupi(IdVeprimeKFKoka);
                return _colVeprimeKfTrupi;
            }
            set
            {

                _colVeprimeKfTrupi = value;
            }

        }
        /// <summary>
        /// kthen fleten kontabel
        /// </summary>
        public clsKokaFleteKontabel ClsKokaFleteKontabel { get; set; }

        /// <summary>
        /// kthen gjendjen e kf
        /// </summary>
        public colGjendjeKlientFurnitor ColGjendjeKlientFurnitor { get; set; }

        public colDokumentLidhesKoka ColDokumentalidhes { get; set; }

        public int IdKfKunderParti { get; set; }

        /// <summary>
        /// Get Set: id e veprime kf koka
        /// </summary>
        public int IdVeprimeKFKoka { get; set; }

        /// <summary>
        /// Get Set: llojin e veprimit
        /// </summary>
        /// <example> 1-hedhja e gjendjes fillestare</example>
        public int LlojVeprimi { get; set; }

        /// <summary>
        /// Get Set: nr e dokumentit
        /// </summary>
        public string NrDok { get; set; }

        /// <summary>
        /// Get Set: daten e dokumentit
        /// </summary>
        public DateTime DtDok { get; set; }

        /// <summary>
        /// Get Set: daten e regjistrimit
        /// </summary>
        public DateTime DtRegj { get; set; }

        /// <summary>
        /// Get Set: id e klient furnitorit
        /// </summary>
        public int IdKf { get; set; }

        /// <summary>
        /// Get Set: id  e llogarise kunderparti
        /// </summary>
        public int IdLlogKunderParti { get; set; }

        /// <summary>
        /// Get Set: pershkrimin
        /// </summary>
        public string Pershkrimi { get; set; }

        /// <summary>
        /// Get Set: id e monedhes
        /// </summary>
        public int IdMonedha { get; set; }

        /// <summary>
        /// Get Set: vleften e dokumentit
        /// </summary>
        public double Vlefta { get; set; }

        /// <summary>
        /// Get Set: id e ndermarjes
        /// </summary>
        public int IdNderm { get; set; }

        /// <summary>
        /// Get Set: id e ndermarje vitit
        /// </summary>
        public int IdNderViti { get; set; }

        /// <summary>
        /// Get Set: id e statusit te dokumentit
        /// </summary>
        /// <example>0-draft, 1-ruajtur 20-modifkuar</example>
        public int IdStatusDok { get; set; }

        /// <summary>
        /// Get Set: id e nivelit te dokumentit
        /// </summary>
        public int IdNivel { get; set; }

        /// <summary>
        /// Get Set: id e konfigurimit te ambjentit
        /// </summary>
        public int IdKonfigAmbjente { get; set; }

        /// <summary>
        /// Get Set: id e dokumentit nga eshte gjeneruar ne rastet e modifikimit
        /// </summary>
        public int IdDokNga { get; set; }

        /// <summary>
        /// Get Set: id e nivelit nga eshte gjeneruar kur gjenerohet nga ambjente te tjera
        /// </summary>
        public int IdNivelGjenerues { get; set; }

        /// <summary>
        /// Get Set: id e konfigurimit te dokumentit gjenerues
        /// </summary>
        public int IdKonfigGjenerues { get; set; }

        /// <summary>
        /// Get Set: id e dokumentit qe e ka gjeneruar kur gjenerohet nga ambjente te tjera
        /// </summary>
        public int IdGjenerues { get; set; }

        /// <summary>
        /// Get Set: id e perdoruesit qe ka kryer veprimin
        /// </summary>
        public int IdPerdoruesi { get; set; }

        public DateTime DtKrijimi { get; private set; }

        public DateTime DtModifikimi {
            get { return dtKrijimi; }
            set { dtKrijimi = value; } }

        public int IdDegeAdministrative { get; set; }

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktor bosh
        /// </summary>
        public clsVeprimeKFKoka()
        {
        }

        /// <summary>
        /// konstruktori me id
        /// </summary>
        /// <param name="idVeprimeKfKoka">id-ja e dhene per te lexuar te dhenat nga db-ja</param>
        public clsVeprimeKFKoka(int idVeprimeKfKoka)
        {
            var data = new clsDatabaseRegjistrim();
            if (!MbushVeprimeKfKoka(data.merrVeprimeKFKokaSipasID(idVeprimeKfKoka)))
                return;
            data.Dispose();
        }

        public clsVeprimeKFKoka(DataRow rreshti)
        {
            MbushVeprimeKfKoka(rreshti);
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush objektin nga nje datarow i marr nga db-ja
        /// </summary>
        /// <param name="rreshti">datarow me te dhenat e te drejtes</param>
        /// <returns>True nese te dhenat merren me sukses, False perndryshe</returns>
        internal bool MbushVeprimeKfKoka(DataRow rreshti)
        {
            try
            {
                IdVeprimeKFKoka = ToInt32(rreshti["IDVEPRIMKFKOKA"]);
                LlojVeprimi = ToInt32(rreshti["LLOJVEPRIMI"]);
                NrDok = Convert.ToString(rreshti["NRDOK"]);
                DtDok = ToDateTime(rreshti["DTDOK"]);
                DtRegj = ToDateTime(rreshti["DTREGJ"]);
                IdKf = !IsDBNull(rreshti["IDKF"])
                    ? ToInt32(rreshti["IDKF"])
                    : 0;
                IdLlogKunderParti = !IsDBNull(rreshti["IDLLOGKUNDERPARTI"])
                    ? ToInt32(rreshti["IDLLOGKUNDERPARTI"])
                    : 0;
                IdKfKunderParti = !IsDBNull(rreshti["IDKFKUNDERPARTI"])
                    ? ToInt32(rreshti["IDKFKUNDERPARTI"])
                    : 0;
                Pershkrimi = Convert.ToString(rreshti["PERSHKRIMI"]);
                IdMonedha = ToInt32(rreshti["MONEDHA"]);
                Vlefta = double.Parse(rreshti["VLEFTA"].ToString());
                IdNderm = ToInt32(rreshti["IDNDERM"]);
                IdNderViti = ToInt32(rreshti["IDNDERVITI"]);
                IdStatusDok = ToInt32(rreshti["IDSTATUSDOK"]);
                IdNivel = ToInt32(rreshti["IDNIVEL"]);
                IdKonfigAmbjente = ToInt32(rreshti["IDKONFIGAMBJENTE"]);
                IdDokNga = !IsDBNull(rreshti["IDDOKNGA"])
                    ? ToInt32(rreshti["IDDOKNGA"])
                    : 0;
                IdNivelGjenerues = !IsDBNull(rreshti["IDNIVELGJENERUES"])
                    ? ToInt32(rreshti["IDNIVELGJENERUES"])
                    : 0;
                IdKonfigGjenerues = !IsDBNull(rreshti["IDKONFIGGJENERUES"])
                    ? ToInt32(rreshti["IDKONFIGGJENERUES"])
                    : 0;
                IdGjenerues = !IsDBNull(rreshti["IDGJENERUES"])
                    ? ToInt32(rreshti["IDGJENERUES"])
                    : 0;
                IdPerdoruesi = ToInt32(rreshti["IDPERDORUESI"]);
                DtKrijimi = !IsDBNull(rreshti["DTKRIJIMI"])
                    ? ToDateTime(rreshti["DTKRIJIMI"])
                    : DateTime.MinValue;
                DtModifikimi = !IsDBNull(rreshti["DTMODIFIKIMI"])
                    ? ToDateTime(rreshti["DTMODIFIKIMI"])
                    : DateTime.MinValue;
                IdDegeAdministrative = !IsDBNull(rreshti["IDDEGEADMINISTRATIVE"])
                    ? ToInt32(rreshti["IDDEGEADMINISTRATIVE"])
                    : 0;
                ColVeprimeKFTrupi = new colVeprimeKFTrupi();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region Metoda Publike

        public static bool KaAutorizime(int idkoka, int idperdoruesi)
        {
            using (var dbKokaMagazina = new clsDatabaseRegjistrim())
                return dbKokaMagazina.kaAutorizimVeprimeKF(idkoka, idperdoruesi);
        }

        public clsMesazh KrijoVeprimeKf(DbData dbData, int llojVeprimi, string nrDok, DateTime dtDok, DateTime dtRegj, int idKf, string kodKlientfurnitor, int idLlogKundraParti, string nrllog, string pershkrimi, int idmonedha, string monedha, double vlefta, int idNderm, int idNderviti, int idStatusDok, int idNivel, int idKonfigAmbjente, int idDokNga, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues, int idPerdoruesi, int idkfkunderparti, colVeprimeKFTrupi coltrupi, int idperiudha, bool mekontabilizim, string kodkodifikimi, bool mefatura, clsKonfigurimAmbjenti konfigdokLidhes, out string shfaqmesazhapolupe, out string shfaqmesazhapolupeVdk, colTrupiQendraKosto trupivjeterqendra, int idkokaeksistuese, int idGjuha, ResourceManager rm, CultureInfo ci, int idDegeAdministrative)
        {
            shfaqmesazhapolupe = "jo";
            shfaqmesazhapolupeVdk = "jo";
            LlojVeprimi = llojVeprimi;
            NrDok = nrDok;
            DtDok = dtDok;
            DtRegj = dtRegj;
            IdKf = idKf;
            IdLlogKunderParti = idLlogKundraParti;
            IdKfKunderParti = idkfkunderparti;
            Pershkrimi = pershkrimi;
            IdMonedha = idmonedha;
            Vlefta = vlefta;
            IdNderm = idNderm;
            IdNderViti = idNderviti;
            IdStatusDok = idStatusDok;
            IdNivel = idNivel;
            IdKonfigAmbjente = idKonfigAmbjente;
            IdDokNga = idDokNga;
            IdNivelGjenerues = idNivelGjenerues;
            IdKonfigGjenerues = idKonfigGjenerues;
            IdGjenerues = idGjenerues;
            IdPerdoruesi = idPerdoruesi;
            _nrLlogKundraParti = nrllog;
            _kodKlientFurnitor = kodKlientfurnitor;
            _kodMonedha = monedha;
            IdDegeAdministrative = idDegeAdministrative;
            _colVeprimeKfTrupi = coltrupi;
            _meKontabilizim = mekontabilizim;
            var mesazh = Kontrollo(dbData);
            if (!mesazh.Status)
                return mesazh;

            ColDokumentalidhes = new colDokumentLidhesKoka();

            List<string> rreshtakfvdk;
            colTrupatFletetKontabel trupatperGjendjekf, trupatperGjendjekfvdk;
            if (mekontabilizim)
            {
                string pershkrim;
                if (pershkrimi != string.Empty)
                {
                    pershkrim = pershkrimi;
                }
                else
                {
                    if (kodkodifikimi == "VKF")
                        pershkrim = "Nga hedhja e gjendjes fillestare klient/furnitor";
                    else if (mefatura)
                        pershkrim = "Nga ndryshim detyrimi klient/furnitor(me fatura)";
                    else
                        pershkrim = "Nga ndryshim detyrimi klient/furnitor (pa fatura)";
                }

                const int idllojdok = 38;
                const int idkategoria = 20;

                try
                {
                    colObjektivaKosto objektivat;
                    List<double> vleratobjektiva; List<double> vleratobjektivamonbaze;
                    List<int> idllogobj;
                    List<int> emratkf;
                    List<string> rreshtakf;

                    ClsKokaFleteKontabel = clsKokaFleteKontabel.gjeneroKontabilizimVeprimKF(dbData, IdVeprimeKFKoka, idNivel, idKonfigAmbjente, dtDok, nrDok, idNderm, idNderviti, idPerdoruesi, dtRegj, coltrupi, pershkrim, idDokNga, idllojdok, idperiudha, idkategoria, out emratkf, out rreshtakf, out trupatperGjendjekf, out objektivat, out vleratobjektiva, out vleratobjektivamonbaze, out idllogobj, 0, 0, 0, out shfaqmesazhapolupe, trupivjeterqendra, idGjuha, rm, ci, idDegeAdministrative);
                    // gjenerimi i gjendjes kf
                    string shfaqmesazh;
                    ColGjendjeKlientFurnitor = colGjendjeKlientFurnitor.KrijoGjendjetKlientFurnitor(emratkf, idNivel, nrDok, dtDok, dtRegj, trupatperGjendjekf, rreshtakf);

                    if (konfigdokLidhes.IdKonfigAmbjente != 0)
                    {
                        var colDokumentLidhesKoka = new colDokumentLidhesKoka(idkokaeksistuese, idkategoria);
                        foreach (var idKlientFurnitor in emratkf)
                        {
                            var dokumentiKoka = new clsDokumentLidhesKoka();
                            var coltrupidok = new colDokumentLidhesTrupi();

                            coltrupidok = KrijoTrupDokumentLidhes(dbData, idKlientFurnitor, coltrupi, idNivel, dtDok, clsKlientFurnitor.MerrLlojin(idKlientFurnitor));
                            dokumentiKoka.krijoDokumentLidhesKoka(nrDok, dtDok, dtRegj, idKlientFurnitor, IdVeprimeKFKoka, idkategoria, idNderm, idNderviti, konfigdokLidhes.IdNivel, konfigdokLidhes.IdKonfigAmbjente, 0, IdNivel, IdKonfigAmbjente, idStatusDok, coltrupidok, IdPerdoruesi);

                            var kontablidhes = clsAlternativaKushti.getAlternativa(konfigdokLidhes.IdKonfigAmbjente, "GJK", new clsDatabaseShare(dbData)) != "Jo";
                            var njihDifNgaKursi = clsAlternativaKushti.getAlternativa(konfigdokLidhes.IdKonfigAmbjente, "NFHNDK", new clsDatabaseShare(dbData)) == "Po";
                            if (dokumentiKoka.OColTrupi.Count > 1 && idStatusDok == 1 && kontablidhes && njihDifNgaKursi)//nese eshte bere lidhje dokumentash te gjenerohen diferencat nga kursi
                            {
                                var pershk = pershkrimi != string.Empty ? pershkrimi : "Diferenca nga kursi (Nga ndryshim detyrimi i klient/furnitor)";
                                int idkategoriavdk = 10;
                                int IdLlojDok = 68;//NKLD

                                var kokaqendra = new clsKokaQendraKosto();

                                if (colDokumentLidhesKoka.Count != 0)
                                {
                                    foreach (clsDokumentLidhesKoka k in colDokumentLidhesKoka)
                                    {
                                        if (k.IdKlientFurnitor != idKlientFurnitor) continue;
                                        var newclsKokaFleteKontabel = new clsKokaFleteKontabel(k.IdKoka, 10);
                                        if (newclsKokaFleteKontabel.NrDukumentiKokaFleteKontabel != null)
                                        {
                                            kokaqendra.KtheKokaQKSipasIDGjeneruesDheKonfigMeTrup(newclsKokaFleteKontabel.IdKokaFleteKontabel, newclsKokaFleteKontabel.IdKonfigAmbjente);
                                        }
                                    }
                                }

                                List<int> emratkfvdk;
                                dokumentiKoka.OFleteKontabel = clsKokaFleteKontabel.gjeneroKontabilizimVDKVKF(dbData, dokumentiKoka.IdKoka, dokumentiKoka.IdNivel, dokumentiKoka.IdKonfigAmbjente, dokumentiKoka.DateDokumenti, dokumentiKoka.NrLidhje, dokumentiKoka.IdNdermarje, dokumentiKoka.IdNderViti, IdPerdoruesi, dokumentiKoka.DateRegjistrimi, coltrupi, pershk, 0, IdLlojDok, idperiudha, idkategoriavdk, out emratkfvdk, out rreshtakfvdk, out trupatperGjendjekfvdk, dokumentiKoka.IdKlientFurnitor, dokumentiKoka.OColTrupi, out objektivat, out vleratobjektiva, out vleratobjektivamonbaze, out idllogobj, 0, 0, 0, out shfaqmesazh, kokaqendra.ColTrupi, idGjuha, rm, ci, idDegeAdministrative);
                                if (shfaqmesazh != "jo")
                                    shfaqmesazhapolupeVdk = shfaqmesazh;
                                if (dokumentiKoka.OFleteKontabel.OColTrupi.Count > 0)
                                    dokumentiKoka.OGjendjeKF = colGjendjeKlientFurnitor.KrijoGjendjetKlientFurnitor(emratkfvdk, dokumentiKoka.IdNivel, dokumentiKoka.NrLidhje, dokumentiKoka.DateDokumenti, dokumentiKoka.DateRegjistrimi, trupatperGjendjekfvdk, rreshtakfvdk);
                            }
                            else
                                dokumentiKoka.OFleteKontabel = new clsKokaFleteKontabel();

                            ColDokumentalidhes.Add(dokumentiKoka);
                        }
                    }
                }
                catch (Exception ex)
                {
                    return new clsMesazh(false, ex.Message);
                }
            }
            else
            {
                ClsKokaFleteKontabel = new clsKokaFleteKontabel();
                ColGjendjeKlientFurnitor = new colGjendjeKlientFurnitor();
            }

            return new clsMesazh(true, "Dokumenti  u krijua me sukses!");
        }

        public clsMesazh KrijoVeprimeKfImport(DbData dbData, int llojVeprimi, string nrDok, DateTime dtDok, DateTime dtRegj, string kodikf, string nrllogKunderparti, string pershkrimi, string monedha, double vlefta, int idNderm, int idNderviti, int viti, int idStatusDok, string kodNivel, clsKonfigurimAmbjenti konfigAmbjente, int idPerdoruesi, colVeprimeKFTrupi coltrupi, int idperiudha, bool mekontabilizim, bool mefatura, int idGjuha, ResourceManager rm, CultureInfo ci, int idKf, int idLlogKunderparti, clsKonfigurimAmbjenti konfigAmbjentiLidhes, int idMonedha, bool importo)
        {
            if (string.IsNullOrEmpty(kodNivel))
                return new clsMesazh(false, "Nenkategoria nuk duhet te jete bosh!");

            clsDatabaseRegjistrim dbRegjistrim = new clsDatabaseRegjistrim(dbData);
            clsNivelRegjistrimi nivelRegjistrimi = new clsNivelRegjistrimi(kodNivel, idNderm, dbRegjistrim);

            if (konfigAmbjente.IdNivel != nivelRegjistrimi.IdNivel)
                return new clsMesazh(false, string.Format("Lloji i dokumentit {0} nuk i perket nenkategorise {1}!", konfigAmbjente.KodKonfigAmbjente, kodNivel));

            if (dtDok.Year != viti)
                return new clsMesazh(false, rm.GetString("msgDataNukPerketVititUshtrimor", ci));

            using (var dbRegj = new clsDatabaseRegjistrim())
            {
                if (!importo && dbRegj.ekzistonVeprimKFKoka(nrDok, nivelRegjistrimi.IdNivel, idNderm, dtDok, idKf, coltrupi.FirstOrDefault().Kursi))
                {
                    return new clsMesazh(false, $"Ekziston nje regjistrim me numer dokumenti {nrDok} dhe date {dtDok.Date}!");
                }
            }

            var shfaqmesazhapolupe = "jo";
            var shfaqmesazhapolupeVdk = "jo";

            return KrijoVeprimeKf(dbData, llojVeprimi, nrDok, dtDok, dtRegj, idKf, kodikf, idLlogKunderparti, nrllogKunderparti, pershkrimi, idMonedha, monedha, vlefta, idNderm, idNderviti, idStatusDok, nivelRegjistrimi.IdNivel, konfigAmbjente.IdKonfigAmbjente, 0, 0, 0, 0, idPerdoruesi, 0, coltrupi, idperiudha, mekontabilizim, string.Empty, mefatura, konfigAmbjentiLidhes, out shfaqmesazhapolupe, out shfaqmesazhapolupeVdk, new colTrupiQendraKosto(), 0, idGjuha, rm, ci, 0);
        }

        /// <summary>
        /// shkruan  ne databaze
        /// </summary>
        /// <returns>kthehet True nese shkruhet me sukses, False perndryshe</returns>
        public clsMesazh Modifiko(bool lidhur)
        {
            clsMesazh mesazh;
            var data = new clsDatabaseRegjistrim();

            if (lidhur == false)
            {
                data.beginTransaksion();
                mesazh = ModifikoVeprimKf(data);
            }
            else
            {
                mesazh = data.modifikoVeprimeKFKoka(IdVeprimeKFKoka, LlojVeprimi, NrDok, DtDok, DtRegj, IdKf, IdLlogKunderParti, Pershkrimi, IdMonedha,
                    Vlefta, IdNderm, IdNderViti, IdStatusDok, IdNivel, IdKonfigAmbjente, IdDokNga, IdNivelGjenerues, IdKonfigGjenerues, IdGjenerues, IdPerdoruesi, IdKfKunderParti, IdDegeAdministrative);
                clsMesazh msg = ruajVeprimKFNeHistorik(data, IdVeprimeKFKoka, IdStatusDok, IdPerdoruesi);
                data.Dispose();
            }

            return mesazh;
        }

        public clsMesazh Ruaj(IDictionary<string, object> hfNrAutoregjistrime, DbData dbData)
        {            
            bool kaNdryshimNumri;
            using (clsDatabaseRegjistrim data = new clsDatabaseRegjistrim(dbData))
            {
                clsMesazh ruaj = KontrolloVeprimKf(out kaNdryshimNumri, data, hfNrAutoregjistrime);
                if (!ruaj.Status)
                    return ruaj;

                return RuajVeprimKf(false, data);
            }
        }

        /// <summary>
        /// fshin nje objekt dokument veprimekf sebashku me te trupin, gjendjenkf dhe kontabilitetin perkates
        /// Nje objekt dokument veprimekf ka nje koleksion me trupin, gjendjenkf dhe kontabilitetin , 
        /// fshirja e nje dokumenti veprimekf imponon fshirjen edhe te nje colection-i me trupin, gjendjenkf dhe kontabilitetin
        /// Mqs cdo rresht i ri qe fshihet ne DB kerkon thirrjen e nje SP-je me parametra dhe dokumenti i veprimitkf bashke me trupin, gjendjenkf dhe kontabilitetin konsiderohet si nje regjistrim,
        /// perdoret nje transaksion qe imponon fshirjen e rregullt te nje te nje dokumenti sebashku me trupin,gjendjen kf dhe kontabilitetin e tij
        ///1. merr kontabilitetet eksistuese te dokumentit te shitjes dhe magazines dhe i stornon
        ///3. ben fshirjen e trupit dhe me pas te kokes se dokumentit te shitjes
        /// </summary>
        /// <returns> kthen nje obj clsMesazh per te identifikuar statusin e fshirjes se te dhenave ne DB</returns>
        public clsMesazh Fshi()
        {
            var data = new clsDatabaseRegjistrim();

            var mesazh = new clsMesazh(true);
            var mesazhKont = new clsMesazh(true);

            try
            {
                data.beginTransaksion();
                clsDatabaseKontabilitet dbkontab = new clsDatabaseKontabilitet(data);

                var kokaEkzistuese = new clsVeprimeKFKoka(IdVeprimeKFKoka);
                kokaEkzistuese.ClsKokaFleteKontabel = new clsKokaFleteKontabel();
                kokaEkzistuese.ColGjendjeKlientFurnitor = new colGjendjeKlientFurnitor(kokaEkzistuese.IdVeprimeKFKoka, kokaEkzistuese.IdNivel, data);

                var newclsKokaFleteKontabel = new clsKokaFleteKontabel(kokaEkzistuese.IdVeprimeKFKoka, 20, dbkontab);
                if (newclsKokaFleteKontabel.NrDukumentiKokaFleteKontabel != null)
                {
                    kokaEkzistuese.ClsKokaFleteKontabel = newclsKokaFleteKontabel;

                }

                var colDokumentLidhesKoka = new colDokumentLidhesKoka(kokaEkzistuese.IdVeprimeKFKoka, 20, data);
                foreach (var dokumentLidhesKoka in colDokumentLidhesKoka)
                {
                    if (dokumentLidhesKoka.IdKoka != 0)
                    {
                        //Tani per tani kur modifikohet nje veprim banke fshihet lidhja e meparsheme e dok dhe ruhet lidhja e re. Kjo do ndryshohet me vone dhe lidhjes se vjeter do i vihet nje status dallues.
                        mesazh = dokumentLidhesKoka.fshiDokumentDheKontabilitet(data);
                    }
                }

                foreach (var gj in kokaEkzistuese.ColGjendjeKlientFurnitor)
                {
                    if (gj.IdGjendjeKf != 0)
                    {
                        gj.IdStatusGjendjeKf = 2; //rasti kur ndrysheohet statusi per treguar qe dokumenti eshte i modifikuar dhe nuk duhet marre parasysh
                        mesazh = gj.Modifiko(data);
                    }

                    if (!mesazh.Status)
                    {
                        data.rollbackTransaksion();
                        return mesazh;
                    }
                }

                if (mesazh.Status)
                {
                    if (kokaEkzistuese.ClsKokaFleteKontabel.IdKokaFleteKontabel != 0)
                    {
                        mesazh = kokaEkzistuese.ClsKokaFleteKontabel.fshiupd(dbkontab);
                        if (!mesazh.Status)
                        {
                            data.rollbackTransaksion();
                            return mesazh;
                        }
                    }

                    if (mesazhKont.Status)
                    {
                        kokaEkzistuese.IdStatusDok = 2;  //duhet vendosur nje status i pershtatshem per kete modifikim
                        clsMesazh msg = ruajVeprimKFNeHistorik(data, IdVeprimeKFKoka, 2, IdPerdoruesi);
                        mesazh = data.fshiVeprimeKFKoka(kokaEkzistuese.IdVeprimeKFKoka);
                        if (mesazh.Status)
                        {
                            data.commitTransaksion();
                            mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
                            return mesazh;
                        }
                        data.rollbackTransaksion();

                        return mesazh;
                    }

                    data.rollbackTransaksion();
                    mesazh.Status = false;
                    mesazh.PershkrimMesazhi = mesazhKont.PershkrimMesazhi;
                    return mesazh;
                }

                data.rollbackTransaksion();
                return mesazh;
            }
            catch (Exception ce)
            {
                data.rollbackTransaksion();
                return new clsMesazh(false, ce.Message);
            }
        }

        public bool MerrVeprimeKfKokaSipasIdKonfigAmbjenteNrDokDtDok(int idKonfigAmb, string nrdok, DateTime dtdok)
        {
            using (var db = new clsDatabaseRegjistrim())
                return MbushVeprimeKfKoka(db.merrVeprimeKFKokaSipasIdKonfigAmbjenteNrDokDtDok(idKonfigAmb, nrdok, dtdok));
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// 
        /// </summary>
        ///<param name="dtDok"> date e dokumenti</param>
        ///<param name="dtRegj">date e regjistrimit</param>
        ///<param name="idDokNga">id e dokumentit nga eshte gjeneruar ne rastet e modifikimit</param>
        ///<param name="idGjenerues"> id e dokumentit nga eshte gjeneruar kur gjenerohet nga ambjente te tjera</param>
        ///<param name="idKF"> id e klient furnitorit</param>
        ///<param name="idKonfigAmbjente"> id e konfigurimit te ambjentit</param>
        ///<param name="idKonfigGjenerues">id e konfigurimit e dokumentit gjenerues</param>
        ///<param name="idLlogKundraParti">id e llogarise kunderparti</param>
        ///<param name="idmonedha"> id e monedhes</param>
        ///<param name="idNivel">id e nivelit</param>
        ///<param name="idNivelGjenerues">id e nivelit te dokumentit gjenerues</param>
        ///<param name="idPerdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        ///<param name="idStatusDok">id e statusit te dokumentit</param>
        ///<param name="idVeprimeKFKoka">id ritese e dokumentit</param>
        ///<param name="llojVeprimi">nr i llojit te veprimit 1-hedhje gjendje fillestare</param>
        ///<param name="nrDok">nr dokumenti</param>
        ///<param name="pershkrimi"> pershkrimi</param>
        ///<param name="vlefta"> vlefta</param>
        ///<param name="idNderm">id e ndermarjes</param>
        ///<param name="idNderviti">id e ndermarje vitit</param>
        /// <returns>kthen id-ne e rolit te krijuar ose zero nese krijimi deshtoi</returns>
        private static int KrijoKoka(int idVeprimeKFKoka, int llojVeprimi, string nrDok, DateTime dtDok, DateTime dtRegj, int idKF, int idLlogKundraParti,
            string pershkrimi, int idmonedha, double vlefta, int idNderm, int idNderviti, int idStatusDok, int idNivel, int idKonfigAmbjente, int idDokNga, int idNivelGjenerues,
            int idKonfigGjenerues, int idGjenerues, int idPerdoruesi, int idkfkunderparti, int idDegeAdministrative, clsDatabaseRegjistrim data, DateTime dtKrijimi)
        {
            idVeprimeKFKoka = data.ruajVeprimeKFKoka(idVeprimeKFKoka, llojVeprimi, nrDok, dtDok, dtRegj, idKF, idLlogKundraParti, pershkrimi, idmonedha, vlefta, idNderm, idNderviti, idStatusDok, idNivel, idKonfigAmbjente, idDokNga, idNivelGjenerues, idKonfigGjenerues, idGjenerues, idPerdoruesi, idkfkunderparti, idDegeAdministrative,dtKrijimi);
            //if (idVeprimeKFKoka <= 0)
            //    return idVeprimeKFKoka;
            //else
            return idVeprimeKFKoka;
        }

        public clsMesazh RuajVeprimKF(bool modifikim, clsDatabaseRegjistrim data)
        {
            return RuajVeprimKf(modifikim, data);
        }

        /// <summary>
        /// Ruan nje objekt dokumenti verpime kf sebashku me trupin , gjendjen kf dhe kontabilitetin perkates
        /// Nje objekt koka dokumenti veprimekf ka nje koleksion me trupin e dokumentit , nje gjendje kf dhe kontabilitetin perkates , 
        /// ruajtja e nje dokumenti imponon ruajtjen edhe te nje colection-i me trupin, gjendjes kf  dhe kontabilitetin kur kontabilizohet
        /// Mqs cdo rresht i ri qe shtohet ne DB kerkon thirrjen e nje SP-je me parametra dhe koka e dokumentit bashke me trupin, gjendjenkf dhe kontabilitetin konsiderohet si nje regjistrim,
        /// perdoret nje transaksion qe imponon regjistrimin e rregullt te nje te nje dokumenti veprime kf sebashku me trupin, gjendjenkf dhe kontabilitetin perkates
        /// </summary>
        /// <param name="modifikim"></param>
        /// <param name="data"></param>
        /// <param name="rm"></param>
        /// <param name="ci"></param>
        /// <returns></returns>
        private clsMesazh RuajVeprimKf(bool modifikim, clsDatabaseRegjistrim data)
        {//transaksioni per te ruajtur             
            try
            {
                IdVeprimeKFKoka = KrijoKoka(IdVeprimeKFKoka, LlojVeprimi, NrDok, DtDok, DtRegj, IdKf, IdLlogKunderParti, Pershkrimi, IdMonedha, Vlefta, IdNderm, IdNderViti, IdStatusDok, IdNivel, IdKonfigAmbjente, IdDokNga, IdNivelGjenerues, IdKonfigGjenerues, IdGjenerues, IdPerdoruesi, IdKfKunderParti, IdDegeAdministrative, data,dtKrijimi);

                var mesazh = IdVeprimeKFKoka == -1
                    ? new clsMesazh(false, MessagesResource.Messages["msgNukURuajtKokaEDokumentit"])
                    : new clsMesazh(true, MessagesResource.Messages["labelRaportMesazhRuajtjaPerfundoiSukses"]);

                if (!mesazh.Status)
                {
                    if (!modifikim)
                        data.rollbackTransaksion();
                    return mesazh;
                }

                foreach (var veprimeKfTrupi in _colVeprimeKfTrupi)
                {
                    veprimeKfTrupi.IdVeprimeKFKoka = IdVeprimeKFKoka;
                    mesazh = veprimeKfTrupi.Ruaj(data);

                    if (!mesazh.Status)
                    {
                        if (!modifikim)
                            data.rollbackTransaksion();
                        return mesazh;
                    }
                }
                mesazh = mesazh = ruajVeprimKFNeHistorik(data, IdVeprimeKFKoka,IdStatusDok, IdPerdoruesi);
                if (ColDokumentalidhes.Count > 0)  //mesazh.Status && 
                {
                    foreach (var dokumentLidhesKoka in ColDokumentalidhes)
                    {
                        if (dokumentLidhesKoka.OColTrupi.Count > 0)
                        {
                            dokumentLidhesKoka.IdGjenerues = IdVeprimeKFKoka;
                            foreach (var dokumentLidhesTrupi in dokumentLidhesKoka.OColTrupi)
                            {
                                if (dokumentLidhesTrupi.IdDokumenti == -5)
                                    dokumentLidhesTrupi.IdDokumenti = IdVeprimeKFKoka;
                            }

                            mesazh = dokumentLidhesKoka.ruaj(IdStatusDok != 0, data);
                        }

                        if (!mesazh.Status)
                        {
                            if (!modifikim)
                                data.rollbackTransaksion();
                            return mesazh;
                        }
                    }
                }
                else
                {
                    mesazh = new clsMesazh(true, MessagesResource.Messages["labelRaportMesazhRuajtjaPerfundoiSukses"]);
                }

                if (!mesazh.Status)
                {
                    if (!modifikim)
                        data.rollbackTransaksion();
                    return mesazh;
                }
                if (!_meKontabilizim)
                {
                    if (!modifikim)
                        data.commitTransaksion();
                    mesazh = new clsMesazh(true, MessagesResource.Messages["labelRaportMesazhRuajtjaPerfundoiSukses"]);
                    return mesazh;
                }
                ClsKokaFleteKontabel.IdGjenerues = IdVeprimeKFKoka;
                var kontMesazh = ClsKokaFleteKontabel.Ruaj(new clsDatabaseKontabilitet(data));
                if (!kontMesazh.Status)
                {
                    if (!modifikim)
                        data.rollbackTransaksion();
                    return mesazh;
                }

                foreach (var gjendjeKlientFurnitor in ColGjendjeKlientFurnitor)
                {
                    gjendjeKlientFurnitor.IdDok = IdVeprimeKFKoka;
                    if (gjendjeKlientFurnitor.IdKlientGjendjeKf != 0)
                    {
                        int idG;
                        mesazh = data.ruajGjendjeKF(out idG, IdVeprimeKFKoka, gjendjeKlientFurnitor.NrDok, gjendjeKlientFurnitor.DateDok, gjendjeKlientFurnitor.VlMinus, gjendjeKlientFurnitor.VlPlus, gjendjeKlientFurnitor.NivelDok, gjendjeKlientFurnitor.IdMonedhaDok, gjendjeKlientFurnitor.KursiDok, gjendjeKlientFurnitor.DateRegj, gjendjeKlientFurnitor.VlMinusMonedheBaze, gjendjeKlientFurnitor.VlPlusMonedheBaze, gjendjeKlientFurnitor.IdKlientGjendjeKf);
                    }

                    if (!mesazh.Status)
                    {
                        if (!modifikim)
                            data.rollbackTransaksion();
                        return mesazh;
                    }
                }

                if (!mesazh.Status)
                {
                    if (!modifikim)
                        data.rollbackTransaksion();
                    return mesazh;
                }

                if (!modifikim)
                    data.commitTransaksion();

                mesazh = new clsMesazh(true, MessagesResource.Messages["labelRaportMesazhRuajtjaPerfundoiSukses"]);
                return mesazh;
            }
            catch (Exception ce)
            {
                if (!modifikim)
                    data.rollbackTransaksion();
                return new clsMesazh(false, ce.Message);
            }
        }
        
        internal clsMesazh KontrolloVeprimKf(out bool kaNdryshimNumri, clsDatabaseRegjistrim dbRegj, IDictionary<string, object> hfNrAutoregjistrime)
        {
            kaNdryshimNumri = false;
            if (dbRegj.ekzistonVeprimKFKoka(NrDok, IdNivel, IdNderm, DtDok, IdKf, ColVeprimeKFTrupi.First().Kursi))
            {
                return new clsMesazh(false, $"Ekziston nje regjistrim me numer dokumenti {NrDok} dhe klient {_kodKlientFurnitor}!");
            }

            var mes = new clsMesazh();

            if (hfNrAutoregjistrime != null)
            {
                mes = KontrolloNrAutoVeprimeKf(out kaNdryshimNumri, dbRegj, hfNrAutoregjistrime);
                if (!mes.Status)
                    return mes;
            }

            if (kaNdryshimNumri)
                return mes;
            
            return new clsMesazh(true, "Kontrolli i veprimeve klient/furnitor u krye me sukses!");
        }

        private clsMesazh KontrolloNrAutoVeprimeKf(out bool kaNdryshimNumri, clsDatabaseRegjistrim dbRegj, IDictionary<string, object> hfregjistrime)
        {
            var db = new clsDatabaseAdmin(dbRegj);
            var list = clsNrAutom.kontrollogjithenumrat(db, hfregjistrime, DtDok);

            if (NrAuto.ktheVlerenEre(list, "NrDok") != "")
                NrDok = NrAuto.ktheVlerenEre(list, "NrDok");

            return NrAuto.ruajvlera(out kaNdryshimNumri, list, DtDok, IdPerdoruesi, IdNderm, db);
        }

        /// <summary>
        /// Modifikon nje objekt dokument veprimekf sebashku me te trupin, gjendjakf dhe kontabilitetin
        /// Nje objekt dokument veprimekf ka nje koleksion me trupin, gjendjakf dhe kontabilitetin perkates , 
        /// modifikimi e nje dokumenti imponon modifikimin edhe te nje colection-i me trupin,gjendjakf dhe kontabilitetin
        /// Mqs cdo rresht i ri qe modifikon ne DB kerkon thirrjen e nje SP-je me parametra dhe dokumenti i veprimekf bashke me trupin, gjendjenkf dhe kontabilitetin konsiderohet si nje regjistrim,
        /// perdoret nje transaksion qe imponon modifikimin e rregullt te nje te nje dokumenti veprimekf sebashku me trupin, gjendjenkf dhe kontabilitetin
        /// 1. merret dokumenti eksistues i veprimekf  kalohen ne gjendjen 20 te modifikimit
        /// 2. ruhet dokumenti i ri i veprimekf se bashku me trupin,gjendjen kf dhe kontabilitetin
        /// 3. stornohen kontabiliteti i dokumentave eksistues 
        /// </summary>
        /// <returns> kthen nje obj clsMesazh per te identifikuar statusin e modifikimit se te dhenave ne DB</returns>
        private clsMesazh ModifikoVeprimKf(clsDatabaseRegjistrim data)
        {
            clsMesazh mesazh;
            var mesazhKont = new clsMesazh(true);
            try
            {   //obj koka qe i kalohet si parameter eshte si duhet te modifikohet
                var kokaEkzistuese = new clsVeprimeKFKoka(IdVeprimeKFKoka);
                var dbqendra = new clsDatabaseQendraKosto(data);

                if (string.IsNullOrEmpty(kokaEkzistuese.NrDok) || kokaEkzistuese.IdStatusDok == 2)
                {
                    data.rollbackTransaksion();
                    return new clsMesazh(false, MessagesResource.Messages["msgDokumentiKaNdryshuarHapeniPerseri"]);
                }
               mesazh= data.fshiVeprimeKFKoka(kokaEkzistuese.IdVeprimeKFKoka);
                if (!mesazh.Status)
                    return mesazh;
                
                IdDokNga = kokaEkzistuese.IdVeprimeKFKoka;//dokumentit te ri do i ruajme id e dokumentit nga u krijua
                kokaEkzistuese.ClsKokaFleteKontabel = new clsKokaFleteKontabel();
                kokaEkzistuese.ColGjendjeKlientFurnitor = new colGjendjeKlientFurnitor(kokaEkzistuese.IdVeprimeKFKoka, kokaEkzistuese.IdNivel, data);

                
                if (mesazh.Status)
                {
                    var colDokumentLidhesKoka = new colDokumentLidhesKoka(kokaEkzistuese.IdVeprimeKFKoka, 20, data);
                    foreach (var dokumentLidhesKoka in colDokumentLidhesKoka)
                    {
                        if (dokumentLidhesKoka.IdKoka != 0)
                        {
                            foreach (var k in ColDokumentalidhes)
                                k.IdDokNga = dokumentLidhesKoka.IdKoka;

                            mesazh = dokumentLidhesKoka.fshiDokumentDheKontabilitet(data);//Tani per tani kur modifikohet nje veprim banke fshihet lidhja e meparsheme e dok dhe ruhet lidhja e re. Kjo do ndryshohet me vone dhe lidhjes se vjeter do i vihet nje status dallues.
                        }
                    }

                    foreach (var gjendjeKlientFurnitor in kokaEkzistuese.ColGjendjeKlientFurnitor)
                    {
                        if (gjendjeKlientFurnitor.IdGjendjeKf != 0)
                        {
                            gjendjeKlientFurnitor.IdStatusGjendjeKf = 2; //rasti kur ndrysheohet statusi per treguar qe dokumenti eshte i modifikuar dhe nuk duhet marre parasysh
                            mesazh = gjendjeKlientFurnitor.Modifiko(data);
                        }

                        if (!mesazh.Status)
                        {
                            data.rollbackTransaksion();
                            return mesazh;
                        }
                    }

                    if (mesazh.Status)
                    {
                        var dbkontab = new clsDatabaseKontabilitet(data);
                        var kokFleteKont = new clsKokaFleteKontabel(kokaEkzistuese.IdVeprimeKFKoka, 20, dbkontab);
                        if (kokFleteKont.NrDukumentiKokaFleteKontabel != null)
                        {
                            kokaEkzistuese.ClsKokaFleteKontabel = kokFleteKont;
                            var kokaqendra = new clsKokaQendraKosto();
                            kokaqendra.KtheKokaQKSipasIDGjeneruesDheKonfigMeTrup(kokFleteKont.IdKokaFleteKontabel, kokFleteKont.IdKonfigAmbjente, dbqendra); //TOCHECK NESTILA

                            if (kokaqendra.IdKoka != 0 && kokaqendra.IdKoka != -1)
                                kokaEkzistuese.ClsKokaFleteKontabel.KokaQendraKosto = kokaqendra;
                            else
                                kokaEkzistuese.ClsKokaFleteKontabel.KokaQendraKosto = new clsKokaQendraKosto();
                        }

                        ClsKokaFleteKontabel.IdDokNga = kokaEkzistuese.ClsKokaFleteKontabel.IdKokaFleteKontabel;
                        ClsKokaFleteKontabel.KokaQendraKosto.IdDokNga = kokaEkzistuese.ClsKokaFleteKontabel.KokaQendraKosto.IdKoka;

                        mesazh = RuajVeprimKf(true, data);

                        if (mesazh.Status)
                        {
                            if (kokaEkzistuese.ClsKokaFleteKontabel.IdKokaFleteKontabel != 0)
                                mesazhKont = kokaEkzistuese.ClsKokaFleteKontabel.ModifikoFleteKontabel(true, dbkontab);

                            if (mesazhKont.Status && mesazh.Status)
                            {
                                data.commitTransaksion();
                                mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
                                return mesazh;
                            }

                            data.rollbackTransaksion();
                            return mesazh;
                        }

                        data.rollbackTransaksion();
                        return mesazh;
                    }

                    data.rollbackTransaksion();
                    return mesazh;
                }

                data.rollbackTransaksion();
                return mesazh;
            }
            catch (Exception ce)
            {
                data.rollbackTransaksion();
                return new clsMesazh(false, ce.Message);
            }
        }

        private clsMesazh Kontrollo(DbData dbData)
        {
            clsDatabaseKontabilitet dbKontabilitet = new clsDatabaseKontabilitet(dbData);
            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin(dbData);

            if (NrDok == "")
                return new clsMesazh(false, "Numri i dokumentit nuk mund te jete bosh");

            if (DtDok == null || DtDok.ToShortDateString() == "01/01/0100")
                return new clsMesazh(false, "Zgjidhni daten e dokumentit!");

            if (DtRegj == null || DtRegj.ToShortDateString() == "01/01/0100")
                return new clsMesazh(false, "Zgjidhni daten e regjistrimit!");

            var kf = new clsKlientFurnitor(_kodKlientFurnitor, IdNderm, IdPerdoruesi, dbKontabilitet);

            if (_kodKlientFurnitor != "" && kf.IdKlientFurnitor < 1)
                return new clsMesazh(false, $"Klient/Furnitori {_kodKlientFurnitor} nuk ekziston ose ju nuk keni autorizim!");

            if (_kodKlientFurnitor != "" && !kf.AktivKF)
                return new clsMesazh(false, $"Klient/Furnitori {_kodKlientFurnitor} nuk eshte aktiv!");

            clsMonedha monedha = new clsMonedha(_kodMonedha, IdNderm, dbAdmin);
            if (monedha.IdMonedha < 1)
                return new clsMesazh(false, $"Monedha {_kodMonedha} nuk ekziston!");

            clsLlogari llogari = new clsLlogari(_nrLlogKundraParti, IdNderm, dbKontabilitet);
            if (_nrLlogKundraParti != "" && llogari.IdLlogari < 1)
                return new clsMesazh(false, $"Llogaria kunderparti {_nrLlogKundraParti} nuk ekziston!");

            if (IdKfKunderParti > 0)
            {
                var kfkundra = new clsKlientFurnitor(IdKfKunderParti, dbKontabilitet);

                if (kfkundra.IdKlientFurnitor < 1)
                    return new clsMesazh(false, $"Ju nuk keni autorizim per klient/furnitorin {kfkundra.KodKlientFurnitor}!");

                if (!kfkundra.AktivKF)
                    return new clsMesazh(false, $"Klient/Furnitori {kfkundra.KodKlientFurnitor} nuk eshte aktive!");

                if (kf.KodKlientFurnitor == kfkundra.KodKlientFurnitor)
                    return new clsMesazh(false, $"Klient/Furnitori kunderparti {kfkundra.KodKlientFurnitor} nuk duhet te jete i njejte me klientin/furnitorin kryesor!");
            }

            return new clsMesazh(true, "Kontrollet u kaluan me sukses!");
        }

        private static colDokumentLidhesTrupi KrijoTrupDokumentLidhes(DbData dbData, int idklientfurnitor, colVeprimeKFTrupi colVeprimeKfTrupi, int idnivel, DateTime data, bool llojikf)
        {
            var colDokumentLidhesTrupi = new colDokumentLidhesTrupi();
            int j = 0;
            var llojivepBankes = "1";
            var dokLidhes = new clsDokumentLidhesTrupi();
            dokLidhes.IdDokumenti = -5;
            dokLidhes.LlojDokumenti = idnivel.ToString();

            foreach (var veprimeKfTrupi in colVeprimeKfTrupi)
            {
                if (veprimeKfTrupi.IdKF == idklientfurnitor)
                {
                    var idFatura = veprimeKfTrupi.IdFatura;

                    if (idFatura != 0)//nese eshte zgjedhur nje fature per tu likujduar
                    {
                        if ((llojikf && veprimeKfTrupi.DebiKredi == 2) || (!llojikf && veprimeKfTrupi.DebiKredi == 1))// dokumenti kryesor apo i lidhur varet nese e rrit apo e zvogelon detyrimin e kf
                            llojivepBankes = "1";
                        else
                            llojivepBankes = "0";

                        //dokumenti kryesor eshte fatura
                        var dokKryesor = new clsDokumentLidhesTrupi();

                        dokKryesor.IdDokumenti = idFatura;
                        dokKryesor.LlojDokumenti = veprimeKfTrupi.IdNivelFatura.ToString(); ;
                        dokKryesor.Statusi = llojivepBankes == "1" ? "0" : "1"; //dokumentat kryesore i ruajme me status 0

                        int idMonedheFature = clsKokaShitje.ktheIdMonedhe(idFatura);
                        if (idMonedheFature == 0)
                            throw new MyException("Ky dokument ka ndryshuar,hapeni perseri per ta modifikuar!");

                        var kurs = clsKurset.getKursSipasIdMonedhaFromCache(idMonedheFature, data, new clsDatabaseAdmin(dbData));

                        var kmk = idMonedheFature == veprimeKfTrupi.IdMonedha ? veprimeKfTrupi.Kursi : kurs.VleraKursi;
                        dokKryesor.VleraLidhjes = veprimeKfTrupi.VleftaMonBaze / kmk;//vlera ne monedhen e pageses ne fillim kthehet ne vlere ne monedhe baze pastaj ne vlere ne monedhen e fatures
                        colDokumentLidhesTrupi.Add(dokKryesor);

                        //dokumenti lidhes eshte veprimi i bankes
                        dokLidhes.VleraLidhjes += veprimeKfTrupi.Vlefta;//vlera ne monedhen e pageses
                    }
                }

                j++;
            }

            dokLidhes.Statusi = llojivepBankes;
            colDokumentLidhesTrupi.Add(dokLidhes);

            return colDokumentLidhesTrupi;
        }

        private clsMesazh Modifiko(clsDatabaseRegjistrim data)
        {
            return data.modifikoVeprimeKFKoka(IdVeprimeKFKoka, LlojVeprimi, NrDok, DtDok, DtRegj, IdKf, IdLlogKunderParti, Pershkrimi, IdMonedha,
                Vlefta, IdNderm, IdNderViti, IdStatusDok, IdNivel, IdKonfigAmbjente, IdDokNga, IdNivelGjenerues, IdKonfigGjenerues, IdGjenerues, IdPerdoruesi, IdKfKunderParti, IdDegeAdministrative);
        }
        private clsMesazh ruajVeprimKFNeHistorik(clsDatabaseRegjistrim db, int idVeprimeKFKoka, int idStatusDokumenti, int idPerdoruesi)
        {
            return db.ruajVeprimKFNeHistorik(idVeprimeKFKoka, idStatusDokumenti, idPerdoruesi);
        }
        #endregion
    }
}
