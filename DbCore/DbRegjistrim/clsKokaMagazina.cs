using DbCore.DbAdmin;
using DbCore.DbAsete;
using DbCore.DbInventari;
using DbCore.DbKontabiliteti;
using DbCore.DbShare;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Resources;
using DbCore.IMBUtils.Extensions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.Messages;
using DbCore.IMBUtils.Fiskalizimi.Controls;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne  koken e nje dokumenti te magazine
    ///  (Te dhenat  merren nga tabela : T_KOKAMAGAZINA)
    /// </summary>
    public class clsKokaMagazina
    {
        #region Attributet

        private int idKokaMagazina;
        private int idNivel;
        private int idKonfigAmbjente;
        private int idKlientFurnitor;
        private int idMagazina;
        private DateTime dtDok;
        private String nrDok;
        private int idProjekt;
        private String nrProjekt;
        private int idKategoria;
        private int idDokNga;
        private double vlefta;
        private int idStatusDok;
        private int idNdermarrje;
        private int idNdermarrjeVit;
        private int idPerdoruesi;
        private DateTime dtRegjistrimi;
        private int idLlojDokumentiMagazine;
        private string shenime;
        private int idRenditjes;
        private int idNivelGjenerues;
        private int idKonfigGjenerues;
        private int idGjenerues;
        private int idDegeAdministrative;
        private int idLlogari;
        private int idNjesiVartese;
        private bool meKonfirmim;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private colTrupiMagazina ocolTrupiMagazina;
        private clsKokaFleteKontabel oFleteKontabel;
        private clsKokaMagazina oMagazinaTransferim;
        private string kodKlientFurnitori;
        private string kodMagazina;
        private string kodDegeAdministrative;
        private string nrLlogari;
        private string kodNjesiVartese;
        private string magazinieri;
        private clsKokaRezervime oKokaRezervime;
        private string pershkrimi;
        private string adresa;
        private int idKrijuesi;
        private string krijuesi;
        private int idKategoriSeriali;
        private string nrSerial;
        private string nivfsh;
        private string wtnic;
        private int idOperator;
        private bool mallraTeDjegshme;
        private bool shoqerimIKerkuar;
        private int transportuesi;
        private string tipi;
        private string transaksioni;

        /// <summary>
        /// id e grupimit te pare
        /// </summary>
        private int idGrup1;

        /// <summary>
        /// id e grupimit te dyte
        /// </summary>
        private int idGrup2;

        /// <summary>
        /// id e grupimit te trete
        /// </summary>
        private int idGrup3;

        /// <summary>
        /// Perdoret si pershrim fletekontabel per dokumentat e magazines te gjeneruar nga shitja
        /// </summary>
        public static string pershkrimDokKontabilitetDaljeMag = "Nga regjistrimet e shitjes me magazine";

        /// <summary>
        /// Perdoret si pershrim fletekontabel per dokumentat e magazines te gjeneruar nga blerja
        /// </summary>
        public static string pershkrimDokKontabilitetHyrjeMag = "Nga regjistrimet e blerjes me magazine";

        private int idAutomjet;
        private string targa;
        private int idRaportDesing;
        private DateTime dtTransporti;
        private DataRow rreshti;
        private colArkiva oArkiva;
        private string targaShoferi;
        private string shoferi;
        private clsDatabaseRegjistrim db;
        #endregion Attributet

        #region Properties

        public string KodMagazina
        {
            get { return kodMagazina; }
            set { kodMagazina = value; }
        }

        public colArkiva OArkiva
        {
            get { return oArkiva; }
            set { oArkiva = value; }
        }

        /// <summary>
        /// data e tranportit te mallit
        /// </summary>
        public DateTime DtTransporti
        {
            get { return dtTransporti; }
            set { dtTransporti = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdKokaMagazina
        {
            get { return idKokaMagazina; }
            set { idKokaMagazina = value; }
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
        /// Kthen/Vendos ID-ne e projektit ne te cilin ben pjese.
        /// </summary>
        public int IdProjekt
        {
            get { return idProjekt; }
            set { idProjekt = value; }
        }

        /// <summary>
        /// id e grupimit te pare
        /// </summary>
        public int IdGrup1
        {
            get { return idGrup1; }
            set { idGrup1 = value; }
        }

        /// <summary>
        /// id e grupimit te dyte
        /// </summary>
        public int IdGrup2
        {
            get { return idGrup2; }
            set { idGrup2 = value; }
        }

        /// <summary>
        /// id e grupimit te trete
        /// </summary>
        public int IdGrup3
        {
            get { return idGrup3; }
            set { idGrup3 = value; }
        }

        /// <summary>
        /// Kthen/Vendos Nr i projektit.
        /// </summary>
        public string NrProjekt
        {
            get { return nrProjekt; }
            set { nrProjekt = value; }
        }

        /// <summary>
        /// Kthen/Vendos nr i dokumentit.
        /// </summary>
        public string NrDok
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
        /// koka e rezervimit te daljes
        /// </summary>
        public clsKokaRezervime OKokaRezervime
        {
            get { return oKokaRezervime; }
            set { oKokaRezervime = value; }
        }

        /// <summary>
        /// Kthen/Vendos shenime.
        /// </summary>
        public string Shenime
        {
            get { return shenime; }
            set { shenime = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne  e kategorise se dokumentit.
        /// </summary>
        public int IdKategoria
        {
            get { return idKategoria; }
            set { idKategoria = value; }
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
        /// Kthen/Vendos vleften totale te dokumentit.
        /// </summary>
        public double Vlefta
        {
            get { return vlefta; }
            set { vlefta = value; }
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
        public DateTime DtRegjistrimi
        {
            get { return dtRegjistrimi; }
            set { dtRegjistrimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos id e llojit te dokumentit
        /// <example>LLojet mund te jene Hyrje, Dalje, Transferim, Dalje nga shitja, Hyrje nga blerja etj</example>.
        /// </summary>
        public int IdLlojDokumentiMagazine
        {
            get { return idLlojDokumentiMagazine; }
            set { idLlojDokumentiMagazine = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e renditjes qe rendit dokumentat e te njejtes date sipas kohes se regjistrimit
        /// </summary>

        public int IdRenditjes
        {
            get { return idRenditjes; }
            set { idRenditjes = value; }
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
        /// kthen vendos njesine vartese
        /// </summary>
        public int IdNjesiVartese
        {
            get { return idNjesiVartese; }
            set { idNjesiVartese = value; }
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
        /// Kthen/Vendos ID-ne e llogarise
        /// </summary>

        public int IdLlogari
        {
            get { return idLlogari; }
            set { idLlogari = value; }
        }

        /// <summary>
        /// Kthen/Vendos  nje koleksion me trupin e dokumentit te magazines
        /// </summary>
        public colTrupiMagazina OcolTrupiMagazina
        {
            get { return ocolTrupiMagazina; }
            set { ocolTrupiMagazina = value; }
        }

        /// <summary>
        /// kthen/vendos me konfirmim
        /// </summary>
        public bool MeKonfirmim
        {
            get { return meKonfirmim; }
            set { meKonfirmim = value; }
        }

        /// <summary>
        /// Kthen/Vendos nje  flete kontabel te gjeneruar nga dokumenti i magazines kur kontabilizohet.
        /// </summary>
        public clsKokaFleteKontabel OFleteKontabel
        {
            get { return oFleteKontabel; }
            set { oFleteKontabel = value; }
        }

        /// <summary>
        /// Kthen/Vendos  nje koke dokumenti magazine ne rastet kur behet transferim mban dokumentin e magazines ku transferohen .
        /// </summary>
        public clsKokaMagazina OMagazinaTransferim
        {
            get { return oMagazinaTransferim; }
            set { oMagazinaTransferim = value; }
        }

        public DateTime DtKrijimi
        {
            get { return dtKrijimi; }
        }

        public DateTime DtModifikimi
        {
            get { return dtModifikimi; }
        }

        public string Magazinieri
        {
            get { return magazinieri; }
            set { magazinieri = value; }
        }

        public string Pershkrimi
        {
            get { return pershkrimi; }
            set { pershkrimi = value; }
        }

        public string Adresa
        {
            get { return adresa; }
            set { adresa = value; }
        }
        public string NIVFSH
        {
            get { return nivfsh; }
            set { nivfsh = value; }
        }
        public string WTNIC
        {
            get { return wtnic; }
            set { wtnic = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e automjetit.
        /// </summary>
        public int IdAutomjet
        {
            get { return idAutomjet; }
            set { idAutomjet = value; }
        }

        public string Targa
        {
            get { return targa; }
        }

        public int IdRaportDesing
        {
            get { return idRaportDesing; }
            set { idRaportDesing = value; }
        }

        public string Krijuesi
        {
            get { return krijuesi; }
            set { krijuesi = value; }
        }

        public int IdKrijuesi
        {
            get { return idKrijuesi; }
            set { idKrijuesi = value; }
        }
        public string Shoferi
        {
            get { return shoferi; }
            set { shoferi = value; }
        }

        public string TargaShoferi
        {
            get { return targaShoferi; }
            set { targaShoferi = value; }
        }

        public IDictionary<string, object> HfArkiva { get; set; }

        public int IdKategoriSeriali
        {
            get { return idKategoriSeriali; }
            set { idKategoriSeriali = value; }
        }

        public string NrSerial
        {
            get { return nrSerial; }
            set { nrSerial = value; }
        }

        public int IdOperator
        {
            get { return idOperator; }
            set { idOperator = value; }
        }
        public bool MallraTeDjeghsme
        {
            get { return mallraTeDjegshme; }
            set { mallraTeDjegshme = value; }
        }
        public bool ShoqerimIKerkuar
        {
            get { return shoqerimIKerkuar; }
            set { shoqerimIKerkuar = value; }
        }
        public int Transportuesi
        {
            get { return transportuesi; }
            set { transportuesi = value; }
        }
        public string Tipi
        {
            get { return tipi; }
            set { tipi = value; }
        }
        public string Transaksioni
        {
            get { return transaksioni; }
            set { transaksioni = value; }
        }

        #endregion Properties

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases me parametra
        /// </summary>
        /// <param name="dtDk"> data e dokumentit te magazines</param>
        /// <param name="dtRegj"> data e regjistrimit te dokumentit te magazines</param>
        /// <param name="idKDok"> id kategorise se dokumentit me te cilin lidhet.</param>
        /// <param name="idKlFurn"> id e klient furnitorit me te cilin lidhet</param>
        /// <param name="idKonf"> id e konfigurimit te dokumentit</param>
        /// <param name="idLidhes">id e dokumentit nga e cila gjenerohet ne rastet e modifikimit </param>
        /// <param name="idllojdokmag"> id e llojit te dokumentit</param>
        /// <param name="idMag">id e magazines</param>
        /// <param name="idMagKoka">id ritese e kokes se magazines</param>
        /// <param name="idNder"> id e ndermarjes</param>
        /// <param name="idNdVt"> id e ndermarje vitit</param>
        /// <param name="idNiv"> id e nivelit te regjistrimit</param>
        /// <param name="idPer"> id e perdoruesit se e ben regjistrimin</param>
        /// <param name="idProj"> id e projektit ne te cilin ben pjese</param>
        /// <param name="idrenditjes"> id e renditjes per te renditur dokumentat e te njejtes date sipas kohes se regjistrimit</param>
        /// <param name="idSt"> id e statusit te dokumentit</param>
        /// <param name="nrDk">nr i dokumentit</param>
        /// <param name="nrProj"> nr i projektit</param>
        /// <param name="shenim"> shenime </param>
        /// <param name="vl"> vlera totale e dokumentit</param>
        /// <param name="idGjenerues">id e dokumentit nga gjenerohet magazina nga nje ambjent tjeter</param>
        /// <param name="idKonfigGjenerues">id e konfigurimit nga gjenerohet dokumenti</param>
        /// <param name="idNivelGjenerues">id e nivelit nga gjenerohet dokumenti</param>
        /// <param name="idgrup1">id e grupimit te pare</param>
        /// <param name="idgrup2">id e grupimit te dyte</param>
        /// <param name="idgrup3">id e grupimit te trete</param>
        /// <param name="nrSerial">nr Serial i dokumentit</param>
        /// <param name="nivfsh">kodi i gjeneruar nga sistemi i fiskalizimit</param>
        /// <param name="wtnic">kodi i gjeneruar nga kombinimi i elementeve te dokumentit te transferimit</param>
        public clsKokaMagazina(int idMagKoka, int idNiv, int idKonf, int idKlFurn, int idMag, DateTime dtDk, string nrDk, int idProj, string nrProj, int idKDok, int idLidhes, double vl, int idSt, int idNder,
            int idNdVt, int idPer, DateTime dtRegj, int idllojdokmag, string shenim, int idrenditjes, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues, int idDegeAdministrative, int idllogari, 
            int idnjesivartese, bool mekonfirmim, int idgrup1, int idgrup2, int idgrup3, string pershkrimi, string magazinieri, string adresa, int idAutomjet, int idrap, int idkrijuesi, DateTime dttrans, 
            int idkategoriseriali, string nrSerial, string nivfsh, string wtnic, int operatori)
        {
            idKokaMagazina = idMagKoka;
            idNivel = idNiv;
            idKonfigAmbjente = idKonf;
            idKlientFurnitor = idKlFurn;
            idMagazina = idMag;
            nrDok = nrDk;
            dtDok = dtDk;
            idProjekt = idProj;
            nrProjekt = nrProj;
            idKategoria = idKDok;
            idDokNga = idLidhes;
            vlefta = vl;
            idStatusDok = idSt;
            idNdermarrje = idNder;
            idNdermarrjeVit = idNdVt;
            idPerdoruesi = idPer;
            dtRegjistrimi = dtRegj;
            idLlogari = idllogari;
            idLlojDokumentiMagazine = idllojdokmag;
            shenime = shenim;
            idNjesiVartese = idnjesivartese;
            meKonfirmim = mekonfirmim;
            idGrup1 = idgrup1;
            idGrup2 = idgrup2;
            idGrup3 = idgrup3;
            this.pershkrimi = pershkrimi;
            this.magazinieri = magazinieri;
            this.adresa = adresa;
            this.idNivelGjenerues = idNivelGjenerues;
            this.idKonfigGjenerues = idKonfigGjenerues;
            this.idGjenerues = idGjenerues;
            this.idDegeAdministrative = idDegeAdministrative;
            this.idAutomjet = idAutomjet;
            idRaportDesing = idrap;
            this.dtTransporti = dttrans;
            idKrijuesi = idkrijuesi;
            oMagazinaTransferim = new clsKokaMagazina();
            this.idKategoriSeriali = idkategoriseriali;
            this.nrSerial = nrSerial;
            this.nivfsh = nivfsh;
            this.wtnic = wtnic;
            idOperator = operatori;
        }

        /// <summary>
        /// konstruktor me 3 parametra
        /// </summary>
        /// <param name="idNivel">id e nivelit</param>
        /// <param name="nrdok">nr i dokumentit</param>
        /// <param name="dtdok">data e dokumentit</param>
        public clsKokaMagazina(int idNivel, string nrdok, DateTime dtdok)
        {
            clsDatabaseRegjistrim dbKokaMagazina = new clsDatabaseRegjistrim();
            mbushKokaMagazina(dbKokaMagazina.ktheKokaMagazinaSipasIdNivelNrDokDtDok(idNivel, nrdok, dtdok), dbKokaMagazina);
            dbKokaMagazina.Dispose();
        }

        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsKokaMagazina()
        {
            ocolTrupiMagazina = new colTrupiMagazina();
        }

        public clsKokaMagazina(DataRow rreshti, clsDatabaseRegjistrim db)
        {

            mbushKokaMagazina(rreshti, db);
        }
        public clsKokaMagazina(int id)
        {
            ocolTrupiMagazina = new colTrupiMagazina();
            mbushKokaMagazinaSipasID(id);
        }
        #endregion Konstruktoret

        #region Metoda Publike

        public clsMesazh krijoMagazine(int idekzistuese, int idNiv, int idKonf, int idKlFurn, string kodklientfurnitor, int idMag, string kodmag, DateTime dtDk, string nrDk, int idProj, string nrProj, int idKDok, 
            double vl, int idSt, int idNder, int idNdVt, int idPer, DateTime dtRegj, int idllojdokmag, string shenim, int idDegeAdministrative, string koddege, int idllogari, string nrllogari, int idnjesivartese,
            string kodnjesivartese, bool mekonfirmim, int idgrup1, int idgrup2, int idgrup3, string pershkrimi, string magazinieri, string adresa, colTrupiMagazina coltrupi, clsKokaMagazina magtrans, 
            clsKokaFleteKontabel fletekont, int idkokarezervimekzistues, out string mesazhinformues, bool meautorizim, int idAutomjet, string targa, int idrap, int idkrijuesi, DateTime dttransp, string shoferi, 
            string targaShf, string nivfsh, string wtnic, int operatori, IDictionary<string, object> hfArkiva, bool kontrolloEkzistence, int idkategoriseriali, colSerialeUnikeMagazina serialetUnike, 
            bool kontrollogjendje, string nrSerial, clsKokaRezervime rezervimShitje, bool mallraTeDjegshme, bool shoqerimIKerkuar, string tipi, string transaksioni,int transportuesi)
        {
            clsKokaRezervime kokadalje = new clsKokaRezervime();
            mesazhinformues = "";
            colTrupiRezervime coltrupiRez = new colTrupiRezervime();
            clsKokaRezervime rezervimehyrje = new clsKokaRezervime();
            rezervimehyrje.mbushKokaRezervimiSipasIDGjenerues(idGjenerues, 1, idKonfigGjenerues);
            clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim();
            foreach (clsTrupiMagazina trupMag in coltrupi)
            {

                clsArtikulli art = (clsArtikulli)trupMag.Element;
                if (!art.IRezervueshem)
                    continue;
                clsTrupiRezervime truprezer = null;
                if (trupMag.IdTrupiRezervimi != 0)
                {
                    truprezer = new clsTrupiRezervime(trupMag.IdTrupiRezervimi);
                    
                }
                else if (rezervimShitje.IdKokaRezervimi > 0)
                {
                    truprezer = rezervimShitje.OcolTrupiRezervime.Find(x => x.IdArtikulli == trupMag.IdArtikulli && x.IdMag == trupMag.IdMag);
                    idkokarezervimekzistues = rezervimShitje.IdKokaRezervimi;
                }
                if (truprezer != null)
                {
                    double sasiaeharxhuar = dbRegj.ktheSasineKonvertuarSipasArtikullitPaDokEkzistues(truprezer.IdArtikulli, truprezer.IdTrupiRezervime, idkokarezervimekzistues);
                    if (truprezer.IdMag == trupMag.IdMag || truprezer.IdMag == 0)
                    {
                        clsTrupiRezervime truprez = new clsTrupiRezervime(0, 0, trupMag.IdArtikulli, art.KodArtikulli, art.PershkrimArtikulli, trupMag.IdNjesia, ((trupMag.IdNjesia == art.Njesi1Artikulli ? trupMag.Sasia : (trupMag.Sasia * Convert.ToDouble(art.KoeficientArtikulli))) < (truprezer.IdNjesia == art.Njesi1Artikulli ? truprezer.Sasia - sasiaeharxhuar : truprezer.Sasia * Convert.ToDouble(art.KoeficientArtikulli) - sasiaeharxhuar) ? trupMag.Sasia : (truprezer.IdNjesia == art.Njesi1Artikulli ? truprezer.Sasia - sasiaeharxhuar : truprezer.Sasia * Convert.ToDouble(art.KoeficientArtikulli) - sasiaeharxhuar)) / (art.Njesi1Artikulli == trupMag.IdNjesia ? 1 : Convert.ToDouble(art.KoeficientArtikulli)), 
                                                                            art.Njesi1Artikulli == trupMag.IdNjesia ? 1 : Convert.ToDouble(art.KoeficientArtikulli), trupMag.IdMag, dtDk, idSt, -1, trupMag.IdTrupiMagazina, truprezer.IdTrupiRezervime, art);

                        coltrupiRez.Add(truprez);
                        if (trupMag.Sasia > truprez.Sasia)
                            mesazhinformues += "Per artikullin " + art.KodArtikulli + " sasia e mbetur e rezervuar eshte " + truprez.Sasia + "!";
                    }
                    else
                        mesazhinformues += "Per artikullin " + art.KodArtikulli + " nuk ka sasi te rezervuar per kete magazine!";
                    
                }
                else//kur nuk eshte krijuar dok rezervimi me pare
                {
                    clsTrupiRezervime rez = new clsTrupiRezervime(0, 0, trupMag.IdArtikulli, art.KodArtikulli, art.PershkrimArtikulli, trupMag.IdNjesia, trupMag.Sasia, art.Njesi1Artikulli == trupMag.IdNjesia ? 1 : Convert.ToDouble(art.KoeficientArtikulli), trupMag.IdMag, dtDk, idSt, 1, trupMag.IdTrupiMagazina, 0, art);
                    coltrupiRez.Add(rez);
                }
            }
            if (coltrupiRez.Count > 0)
                kokadalje.krijoRezervim(0, 0, idKlFurn, kodklientfurnitor, idMag, kodmag, dtDk, nrDk, idSt, idNder, idNdVt, idPer, dtRegj, pershkrimi, idDegeAdministrative, koddege, 0, 2, 1, 0, 0, idNiv, idKonf, 0, coltrupiRez, new clsKokaRezervime());
            clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();
            return krijoMagazine(kontrollogjendje, idekzistuese, idNiv, idKonf, idKlFurn, kodklientfurnitor, idMag, kodmag, dtDk, nrDk, idProj, nrProj, idKDok, 0, vl, idSt, idNder, idNdVt, idPer, dtRegj, idllojdokmag,
                shenim, 0, 0, 0, idDegeAdministrative, koddege, idllogari, nrllogari, idnjesivartese, kodnjesivartese, mekonfirmim, idgrup1, idgrup2, idgrup3, pershkrimi, magazinieri, adresa, coltrupi, magtrans, 
                fletekont, kokadalje, db, meautorizim, idAutomjet, targa, idrap, kontrolloEkzistence, idkrijuesi, dttransp, idkategoriseriali, nrSerial, nivfsh, wtnic, operatori, hfArkiva, mallraTeDjegshme, shoqerimIKerkuar, tipi, transaksioni, transportuesi);
        }
        public clsMesazh krijoMagazinee(int idekzistuese, int idNiv, int idKonf, int idKlFurn, string kodklientfurnitor, int idMag, string kodmag, DateTime dtDk, string nrDk, int idProj, string nrProj, int idKDok,
            double vl, int idSt, int idNder, int idNdVt, int idPer, DateTime dtRegj, int idllojdokmag, string shenim, int idDegeAdministrative, string koddege, int idllogari, string nrllogari, int idnjesivartese,
            string kodnjesivartese, bool mekonfirmim, int idgrup1, int idgrup2, int idgrup3, string pershkrimi, string magazinieri, string adresa, colTrupiMagazina coltrupi, clsKokaMagazina magtrans,
            clsKokaFleteKontabel fletekont, int idkokarezervimekzistues, out string mesazhinformues, bool meautorizim, int idAutomjet, string targa, int idrap, int idkrijuesi, DateTime dttransp, string shoferi,
            string targaShf, string nivfsh, string wtnic, int operatori, IDictionary<string, object> hfArkiva, bool kontrolloEkzistence, int idkategoriseriali, colSerialeUnikeMagazina serialetUnike,
            bool kontrollogjendje, string nrSerial, clsKokaRezervime rezervimShitje)
        {
            clsKokaRezervime kokadalje = new clsKokaRezervime();
            mesazhinformues = "";
            colTrupiRezervime coltrupiRez = new colTrupiRezervime();
            clsKokaRezervime rezervimehyrje = new clsKokaRezervime();
            rezervimehyrje.mbushKokaRezervimiSipasIDGjenerues(idGjenerues, 1, idKonfigGjenerues);
            clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim();
            foreach (clsTrupiMagazina trupMag in coltrupi)
            {

                clsArtikulli art = (clsArtikulli)trupMag.Element;
                if (!art.IRezervueshem)
                    continue;
                clsTrupiRezervime truprezer = null;
                if (trupMag.IdTrupiRezervimi != 0)
                {
                    truprezer = new clsTrupiRezervime(trupMag.IdTrupiRezervimi);

                }
                else if (rezervimShitje.IdKokaRezervimi > 0)
                {
                    truprezer = rezervimShitje.OcolTrupiRezervime.Find(x => x.IdArtikulli == trupMag.IdArtikulli && x.IdMag == trupMag.IdMag);
                    idkokarezervimekzistues = rezervimShitje.IdKokaRezervimi;
                }
                if (truprezer != null)
                {
                    double sasiaeharxhuar = dbRegj.ktheSasineKonvertuarSipasArtikullitPaDokEkzistues(truprezer.IdArtikulli, truprezer.IdTrupiRezervime, idkokarezervimekzistues);
                    if (truprezer.IdMag == trupMag.IdMag || truprezer.IdMag == 0)
                    {
                        clsTrupiRezervime truprez = new clsTrupiRezervime(0, 0, trupMag.IdArtikulli, art.KodArtikulli, art.PershkrimArtikulli, trupMag.IdNjesia, ((trupMag.IdNjesia == art.Njesi1Artikulli ? trupMag.Sasia : (trupMag.Sasia * Convert.ToDouble(art.KoeficientArtikulli))) < (truprezer.IdNjesia == art.Njesi1Artikulli ? truprezer.Sasia - sasiaeharxhuar : truprezer.Sasia * Convert.ToDouble(art.KoeficientArtikulli) - sasiaeharxhuar) ? trupMag.Sasia : (truprezer.IdNjesia == art.Njesi1Artikulli ? truprezer.Sasia - sasiaeharxhuar : truprezer.Sasia * Convert.ToDouble(art.KoeficientArtikulli) - sasiaeharxhuar)) / (art.Njesi1Artikulli == trupMag.IdNjesia ? 1 : Convert.ToDouble(art.KoeficientArtikulli)),
                                                                            art.Njesi1Artikulli == trupMag.IdNjesia ? 1 : Convert.ToDouble(art.KoeficientArtikulli), trupMag.IdMag, dtDk, idSt, -1, trupMag.IdTrupiMagazina, truprezer.IdTrupiRezervime, art);

                        coltrupiRez.Add(truprez);
                        if (trupMag.Sasia > truprez.Sasia)
                            mesazhinformues += "Per artikullin " + art.KodArtikulli + " sasia e mbetur e rezervuar eshte " + truprez.Sasia + "!";
                    }
                    else
                        mesazhinformues += "Per artikullin " + art.KodArtikulli + " nuk ka sasi te rezervuar per kete magazine!";

                }
                else//kur nuk eshte krijuar dok rezervimi me pare
                {
                    clsTrupiRezervime rez = new clsTrupiRezervime(0, 0, trupMag.IdArtikulli, art.KodArtikulli, art.PershkrimArtikulli, trupMag.IdNjesia, trupMag.Sasia, art.Njesi1Artikulli == trupMag.IdNjesia ? 1 : Convert.ToDouble(art.KoeficientArtikulli), trupMag.IdMag, dtDk, idSt, 1, trupMag.IdTrupiMagazina, 0, art);
                    coltrupiRez.Add(rez);
                }
            }
            if (coltrupiRez.Count > 0)
                kokadalje.krijoRezervim(0, 0, idKlFurn, kodklientfurnitor, idMag, kodmag, dtDk, nrDk, idSt, idNder, idNdVt, idPer, dtRegj, pershkrimi, idDegeAdministrative, koddege, 0, 2, 1, 0, 0, idNiv, idKonf, 0, coltrupiRez, new clsKokaRezervime());
            clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();
            return krijoMagazine(kontrollogjendje, idekzistuese, idNiv, idKonf, idKlFurn, kodklientfurnitor, idMag, kodmag, dtDk, nrDk, idProj, nrProj, idKDok, 0, vl, idSt, idNder, idNdVt, idPer, dtRegj, idllojdokmag,
                shenim, 0, 0, 0, idDegeAdministrative, koddege, idllogari, nrllogari, idnjesivartese, kodnjesivartese, mekonfirmim, idgrup1, idgrup2, idgrup3, pershkrimi, magazinieri, adresa, coltrupi, magtrans,
                fletekont, kokadalje, db, meautorizim, idAutomjet, targa, idrap, kontrolloEkzistence, idkrijuesi, dttransp, idkategoriseriali, nrSerial, nivfsh, wtnic, operatori, hfArkiva, mallraTeDjegshme, shoqerimIKerkuar,tipi,transaksioni, transportuesi, shoferi, targaShf);
        }

        public clsMesazh krijoMagazine(bool kontrollogjendje, int idekzistuese, int idNiv, int idKonf, int idKlFurn, string kodklientfurnitor, int idMag, string kodmag, DateTime dtDk, string nrDk, int idProj, 
            string nrProj, int idKDok, int idLidhes, double vl, int idSt, int idNder, int idNdVt, int idPer, DateTime dtRegj, int idllojdokmag, string shenim, int idNivelGjenerues, int idKonfigGjenerues, 
            int idGjenerues, int idDegeAdministrative, string koddege, int idllogari, string nrllogari, int idnjesivartese, string kodnjesivartese, bool mekonfirmim, int idgrup1, int idgrup2, int idgrup3, 
            string pershkrimi, string magazinieri, string adresa, colTrupiMagazina coltrupi, clsKokaMagazina magtrans, clsKokaFleteKontabel fletekont, clsKokaRezervime kokarez, clsDatabaseRegjistrim db, 
            bool meautorizm, int idAutomjet, string targa, int idrap, bool kontrolloEkzistence,int idkrijuesi, DateTime dttransp, int idkategoriseriali, string nrSerial, string nivfsh, string wtnic, 
            int operatori, IDictionary<string, object> hfArkiva, bool mallratedjegshme, bool shoqerimikerkuar, string tipi,string transaksioni, int transportues, string shoferi = "", string targaShf = "")
        {
            ImbLogger.LogWarningShitje($"Filloi krijimi i magazines me parametra => kontrollogjendje:{kontrollogjendje}, idekzistuese:{idekzistuese}, idNiv:{idNiv}, idKonf:{idKonf}, idKlFurn:{idKlFurn}, kodklientfurnitor:{kodklientfurnitor}, idMag:{idMag}, kodmag:{kodmag}, dtDk;{dtDk}, nrDk:{nrDk}, idProj:{idProj}, idKDok:{idKDok},idLidhes:{idLidhes}, vl:{vl}, idSt:{idSt}, idNder:{idNder},idNdVt:{idNdVt}, idPer:{idPer}, dtRegj:{dtRegj}, idllojdokmag:{idllojdokmag}, shenim: {shenim}, idNivelGjenerues:{idNivelGjenerues}, idKonfigGjenerues:{idKonfigGjenerues}, idGjenerues:{idGjenerues}, idDegeAdministrative:{idDegeAdministrative}, koddege:{koddege}, nivfsh: {nivfsh}, wtnic: {wtnic}.");
            idKokaMagazina = idekzistuese;
            idNivel = idNiv;
            idKonfigAmbjente = idKonf;
            idKlientFurnitor = idKlFurn;
            kodKlientFurnitori = kodklientfurnitor;
            idMagazina = idMag;
            kodMagazina = kodmag;
            nrDok = nrDk;
            dtDok = dtDk;
            idProjekt = idProj;
            nrProjekt = nrProj;
            idKategoria = idKDok;
            idDokNga = idLidhes;
            vlefta = vl;
            idStatusDok = idSt;
            idNdermarrje = idNder;
            idNdermarrjeVit = idNdVt;
            idPerdoruesi = idPer;
            dtRegjistrimi = dtRegj;
            idLlogari = idllogari;
            idGrup1 = idgrup1;
            idGrup2 = idgrup2;
            idGrup3 = idgrup3;
            this.pershkrimi = pershkrimi;
            this.magazinieri = magazinieri;
            this.adresa = adresa;
            idLlojDokumentiMagazine = idllojdokmag;
            shenime = shenim;
            idNjesiVartese = idnjesivartese;
            meKonfirmim = mekonfirmim;
            this.idNivelGjenerues = idNivelGjenerues;
            this.idKonfigGjenerues = idKonfigGjenerues;
            this.idGjenerues = idGjenerues;
            this.idDegeAdministrative = idDegeAdministrative;
            kodDegeAdministrative = koddege;
            nrLlogari = nrllogari;
            kodNjesiVartese = kodnjesivartese;
            ocolTrupiMagazina = coltrupi;
            oMagazinaTransferim = magtrans;
            oFleteKontabel = fletekont;
            bool kontrollodetajim = false;
            this.idAutomjet = idAutomjet;
            this.targa = targa;
            idRaportDesing = idrap;
            this.dtTransporti = dttransp;
            this.idKrijuesi = idkrijuesi;
            this.shoferi = shoferi;
            this.targaShoferi = targaShf;
            this.idKategoriSeriali = idkategoriseriali;
            this.nrSerial = nrSerial;
            oArkiva = new colArkiva();
            this.HfArkiva = hfArkiva;
            this.nivfsh = nivfsh;
            this.wtnic = wtnic;
            idOperator = operatori;
            mallraTeDjegshme = mallratedjegshme;
            shoqerimIKerkuar = shoqerimikerkuar;
            transportuesi = transportues;
            this.tipi = tipi;
            this.transaksioni = transaksioni;


            if (clsAlternativaKushti.getAlternativa(IdKonfigAmbjente, "PDS", new clsDatabaseShare(db)) == "Po")
            {
                kontrollodetajim = true;
            }
            clsMesazh mesazh = this.kontrollo(kontrollodetajim, db, meautorizm);
            if (!mesazh.Status)
                return mesazh;
            if (kontrolloEkzistence) //kontroll ekzistence qe ketu per rastin e importit
            {
                bool kaNdryshimNumri;
                mesazh = kontrolloMagazine(out kaNdryshimNumri, db, null);
                if (!mesazh.Status)
                    return mesazh;
            }
            bool isDalje = this.IdLlojDokumentiMagazine == 2 ? true : false;
            clsDatabaseInventari dbinv = new clsDatabaseInventari(db);
            if (isDalje && kontrollogjendje && IdStatusDok==1)
            {
                mesazh = this.kontrolloGjendjeArtikujshPerDokImporti(isDalje, this.OcolTrupiMagazina, dbinv, this.IdNdermarrje, idKonf);
                if (!mesazh.Status)
                    return mesazh;
            }

            oKokaRezervime = new clsKokaRezervime();
            clsDatabaseShare dbshare = new clsDatabaseShare(db);
            if (clsAlternativaKushti.getAlternativa(IdKonfigAmbjente, "GJDR", dbshare) == "Po")
            {
                clsKusht kushtkonfrezervim = new clsKusht(IdKonfigAmbjente, "ZKR", dbshare);
                oKokaRezervime = kokarez;
                oKokaRezervime.IdKonfigAmbjente = kushtkonfrezervim.Vlera; // konfrez.IdKonfigAmbjente;
                oKokaRezervime.IdNivel = clsKonfigurimAmbjenti.ktheIdNivel(kushtkonfrezervim.Vlera, dbshare); // konfrez.IdNivel;
            }
            ImbLogger.LogWarningShitje("Magazina u krijua me sukses!");
            return new clsMesazh(true, "Magazina u krijua me sukses!");
        }

        private clsMesazh kontrollo(bool detajimDetyrueshem, clsDatabaseRegjistrim db, bool meautorizim)
        {
            ImbLogger.LogWarningShitje($"Filloi metoda kontrollo  me parametra detajimDetyrueshem:{detajimDetyrueshem}, meautorizim:{meautorizim}");
            clsDatabaseKontabilitet dbkont = new clsDatabaseKontabilitet(db);
            clsDatabaseInventari dbinv = new clsDatabaseInventari(db);
            if (nrDok == "")
            {
                ImbLogger.LogTraceShitje("Numri i dokumentit nuk mund të jetë bosh");
                return new clsMesazh(false, "Numri i dokumentit nuk mund të jetë bosh");
            }

            if (dtDok == null || dtDok.ToShortDateString() == "01/01/0100")
            {
                ImbLogger.LogTraceShitje("Zgjidhni datën e dokumentit!");
                return new clsMesazh(false, "Zgjidhni datën e dokumentit!");
            }

            if (dtRegjistrimi == null || dtRegjistrimi.ToShortDateString() == "01/01/0100")
            {
                ImbLogger.LogTraceShitje("Zgjidhni datën e regjistrimit!");
                return new clsMesazh(false, "Zgjidhni datën e regjistrimit!");
            }
                
            if (!string.IsNullOrEmpty(kodKlientFurnitori))
            {
                clsKlientFurnitor kf = new clsKlientFurnitor(kodKlientFurnitori, idNdermarrje, dbkont);
                if (kf.IdKlientFurnitor < 1)
                {
                    ImbLogger.LogTraceShitje("Klient/Furnitori nuk ekziston!");
                    return new clsMesazh(false, "Klient/Furnitori nuk ekziston!");
                }
                    
                kf.mbushKlientFurnitorSipasKodit(kodKlientFurnitori, idNdermarrje, idPerdoruesi, dbkont);
                if (kf.IdKlientFurnitor < 1)
                {
                    ImbLogger.LogTraceShitje("Ju nuk keni autorizime per kete klient/furnitor!");
                    return new clsMesazh(false, "Ju nuk keni autorizime per kete klient/furnitor!");
                }

                if (kodKlientFurnitori != "" && !kf.AktivKF)
                {
                    ImbLogger.LogTraceShitje("Klient/Furnitori nuk është aktive!");
                    return new clsMesazh(false, "Klient/Furnitori nuk është aktive!");
                }
            }
            if (!string.IsNullOrEmpty(kodMagazina))
            {
                clsNjesiAdministrative mag;
                if (meautorizim)
                    mag = new clsNjesiAdministrative(kodMagazina, idNdermarrje, idPerdoruesi, db);
                else
                    mag = new clsNjesiAdministrative(kodMagazina, idNdermarrje, db);
                if (mag.IdNjesiAdministrative < 1)
                    return new clsMesazh(false, "Magazina nuk ekziston ose nuk keni autorizime ne kete magazine!");
                if (kodMagazina != "" && !mag.Aktiv)
                    return new clsMesazh(false, "Magazina nuk eshte aktive!");
            }
            if (!string.IsNullOrEmpty(kodDegeAdministrative) )
            {
                clsDegeAdministrative deg = new clsDegeAdministrative(kodDegeAdministrative, idNdermarrje, db);
                if (deg.IdDegeAdministrative < 1)
                    return new clsMesazh(false, "Dega administrative nuk ekziston!");
                if (!deg.Aktiv)
                    return new clsMesazh(false, "Dega administrative nuk është aktive!");
            }
            if (nrLlogari != "")
                new clsLlogari(nrLlogari, idNdermarrje, dbkont);//throw error nqs nuk ekziston
            if (kodNjesiVartese != "" && !clsNjesiVartese.ekziston(kodNjesiVartese, idNdermarrje, dbinv))
                return new clsMesazh(false, "Njësia Vartëse nuk ekziston!");

            bool kushtVendosjeSeriali = (clsAlternativaKushti.getAlternativa(IdKonfigAmbjente, "VSPA") == "JO") ? false : true;

            if (idStatusDok != 8)
            {
                foreach (clsTrupiMagazina trup in ocolTrupiMagazina)
                {
                    if (((clsArtikulli)trup.Element).IdFormatSeriali > 0 && kushtVendosjeSeriali && trup.Sasia != 0 && idStatusDok != 0)
                    {
                        if (trup.OColSerialeUnikeMagazina == null || trup.OColSerialeUnikeMagazina.Count == 0)
                            return new clsMesazh(false, "Artikulli qe keni zgjedhur nuk ka te caktuar seriale, Vendosni seriale per artikujt te cilet kane te lidhur kategori seriali ne kartele!");
                        var sasiSerialesh = trup.OColSerialeUnikeMagazina.Sum(x => x.Sasia);
                        if (sasiSerialesh != trup.Sasia)
                        {
                            string kodArtikulli;
                            if (trup.IdArtikullSet == 0)
                                kodArtikulli = ((clsArtikulli)trup.Element).KodArtikulli;
                            else
                                kodArtikulli = clsArtikulli.ktheKodArtikulliSipasId(trup.IdArtikullSet);
                            return new MesazhGabimi($"Sasia {trup.Sasia}  e Artikullit {kodArtikulli} nuk perkon me sasine {sasiSerialesh} te serialeve!");
                        }
                    }
                    if (trup.IdLlojVeprimi == 1 && detajimDetyrueshem && idStatusDok == 1)
                    {
                        colDetajimePerArt col = new colDetajimePerArt();
                        col.mbushDetajimArtSipasIdArtikulliDheLlojit(trup.IdArtikulli, 1, dbinv);
                        if (col.Count == 0)
                            continue;
                        if (col.Count > 0 && trup.IdDetajimi < 1)
                            return new clsMesazh(false, "Ju lutem plotesoni serialet e artikujve aparate!");

                        clsDetajimArtikulli detArtikulli = new clsDetajimArtikulli();
                        detArtikulli.ktheDetajimeSipasArtikullitDheIDEkzistonTekRegjistrimi(trup.IdDetajimi, idNdermarrje, idPerdoruesi, trup.IdArtikulli, 1, idKokaMagazina, dbinv);

                        if (detArtikulli.IdDetajimArtikulli < 1 && IdLlojDokumentiMagazine == 1)
                            return new clsMesazh(false, "Seriali i vendosur nuk i takon kesaj blerje ose eshte perdorur ne dokumentat e meparshem!");
                        if (ocolTrupiMagazina.FindAll(x => x.IdDetajimi == trup.IdDetajimi).Count > 1)
                            return new clsMesazh(false, "Ka seriale te perdorura me shume se njehere ne kete dokument!");
                    }
                } 
            }

            return new clsMesazh(true, "Kontrollet  u kaluan me sukses!");
        }

        public clsMesazh krijoMagazinePerImport(string kodNiv, string kodKonf, string kodKlFurn, int idMag, string kodMag, DateTime dtDk, string nrDk, int idProj, string nrProj, int idKatDok, int idDokNga, double vl, 
            int idSt, int idNder, int idNdVt, int idPer, DateTime dtRegj, int idllojdokmag, string shenim, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues, string kodDegeAdministrative, string nrllogari,
            string kodnjesivartese, bool mekonfirmim, string grup1, string grup2, string grup3, string pershkrimi,colTrupiMagazina coltrupi, clsKokaMagazina magtrans, clsKokaFleteKontabel fletekont, int idrap,
            clsKonfigurimAmbjenti konfigAmbjenti, int idkrijuesi, string automjeti, ResourceManager rm, CultureInfo ci, bool transferim,bool kontrollogjendje, int idNdermarrjeMeme, string nrSerial, string Nivfsh,
            string Wtnic, int operatori, DbData dbData, bool shoqerimIKerkuar, bool mallraTeDjegshme,int transportuesi)
        {
            if (MbylljePeriudhe.PeriodClosing.IsPeriodClosed(dtDk, dbData.MyScopeDbManager.ConnectionName, idNder, KategoriDokumenti.Magazina, idKonfigAmbjente))
                return new MesazhGabimi(MessagesResource.Messages["msgPeriodIsClosed"]);

            int idNivel;
            if (kodNiv == "")
                throw new Exception("Nenkategoria nuk mund te jete bosh!");
            clsNivelRegjistrimi nivelRegj = new clsNivelRegjistrimi();
            nivelRegj.Kodi = kodNiv; nivelRegj.IdNdermarje = idNder;
            nivelRegj.merrNivelRegjSipasKodi();
            idNivel = nivelRegj.IdNivel;
            if (kodKonf == "")
                throw new Exception("Lloji i dokumentit nuk mund te jete bosh!");
            colKonfigurimAmbjenti colKonfig = new colKonfigurimAmbjenti();
            colKonfig.mbushKonfigAmbjSipasIdKategoriIdNivelMeLloj(nivelRegj.IdKategori, idNivel, idPer);
            if (colKonfig.Find(kodKonfig => kodKonfig.KodKonfigAmbjente.Contains(kodKonf)) == null)
            {
                throw new Exception("Nuk ekziston ky lloj dokumenti per kete kategori!");
            }
            clsKlientFurnitor kf = new clsKlientFurnitor();
            kf.mbushKlientFurnitorSipasKodit(kodKlFurn, idNder);
            idKlientFurnitor = kf.IdKlientFurnitor;

            clsNjesiAdministrative mag = new clsNjesiAdministrative(kodMag, idNder, idPer);
            idMagazina = mag.IdNjesiAdministrative;

            clsDegeAdministrative deg = new clsDegeAdministrative(kodDegeAdministrative, idNder);
            idDegeAdministrative = deg.IdDegeAdministrative;

            clsLlogari llog = new clsLlogari(nrllogari, idNder);
            idLlogari = llog.IdLlogari;
            int idNjVartese = 0;
            if (kodnjesivartese != "")
            {
                if (clsNjesiVartese.ekziston(kodnjesivartese, idNder))
                {
                    clsNjesiVartese njesi = new clsNjesiVartese(kodnjesivartese, idNder); //TODO KRISTI bej metode statike qe kthen id e njesise vartese sipas kodit dhe ndermarrjes.
                    idNjVartese = njesi.IdNjesiVartese;
                }
                else
                    throw new Exception("Njesia vartese me kod " + kodnjesivartese + " nuk ekziston!");
            }

            int idgrup1 = 0, idgrup2 = 0, idgrup3 = 0;
            if (grup1 != "")
            {
                if (!clsGrupimDokumentiKoka.ekzistonGrup(grup1, idNder, 1))
                    return new clsMesazh(false, "Grupimi i pare i dokumentit nuk ekziston!");
                else
                {
                    clsGrupimDokumentiKoka grupimi1 = new clsGrupimDokumentiKoka(grup1, idNder, 1, idPer);
                    idGrup1 = grupimi1.IdGrupimKoka;
                }
            }

            if (grup2 != "")
            {
                if (!clsGrupimDokumentiKoka.ekzistonGrup(grup2, idNder, 2))
                    return new clsMesazh(false, "Grupimi i dyte i dokumentit nuk ekziston!");
                else
                {
                    clsGrupimDokumentiKoka grupimi2 = new clsGrupimDokumentiKoka(grup2, idNder, 2, idPer);
                    idGrup2 = grupimi2.IdGrupimKoka;
                }
            }

            if (grup3 != "")
            {
                if (!clsGrupimDokumentiKoka.ekzistonGrup(grup3, idNder, 3))
                    return new clsMesazh(false, "Grupimi i trete i dokumentit nuk ekziston!");
                else
                {
                    clsGrupimDokumentiKoka grupimi3 = new clsGrupimDokumentiKoka(grup3, idNder, 3, idPer);
                    idGrup3 = grupimi3.IdGrupimKoka;
                }
            }

            int idAuto = 0;
            if (automjeti != "" && clsAutomjete.ekzistonAutomjet(idNder, automjeti))
            {
                idAuto = clsAutomjete.ktheidAutomjetSipasShasise(automjeti, idNder);
            }
            else if (automjeti != "" && !clsAutomjete.ekzistonAutomjet(idNder, automjeti))
                return new clsMesazh(false, "Automjeti nuk ekziston!");
            
            if (clsAlternativaKushti.getAlternativa(konfigAmbjenti.IdKonfigAmbjente, "V") == "Po" && !transferim)
                return new clsMesazh(false, "Nuk mund te importohen dokumente vartes!");

            string nrProjekti = "";
            if (nrProj != "")
                nrProjekti = nrProj;

            if (idkrijuesi == 0)
                return new clsMesazh(false, "Perdoruesi nuk ekziston!");

            clsPeriudhaKontabel periudha = new clsPeriudhaKontabel(dtDk, idNder);

            string mesazhGabimi;
            if (!clsFunksione.checkPeriudheKontabel(out mesazhGabimi, dtDk, periudha, idSt))
            {
                return new clsMesazh(false, mesazhGabimi);
            }

            //TODO KRISTI futi te funksioni krijoMagazinePerImport

            if (idNivelGjenerues > 0)
            {
                if (!clsNivelRegjistrimi.ekzistonIdNivelGjeneruesi(idNivelGjenerues, idNdermarrjeMeme > 0 ? idNdermarrjeMeme : idNdermarrje).Status)
                    return new clsMesazh(false, "Gjeneruesi me id " + idNivelGjenerues + " nuk ekziston!");
                else
                {
                    if (!clsKonfigurimAmbjenti.ekzistonKonfigurimSipasIDKONFIG(idKonfigGjenerues, idNdermarrjeMeme > 0 ? idNdermarrjeMeme : idNdermarrje).Status)
                        idKonfigGjenerues = 0;
                    if (!clsNivelRegjistrimi.kontrolloGjeneruesPerNivelRegjistrimi(idNivelGjenerues, idNdermarrjeMeme > 0 ? idNdermarrjeMeme : idNdermarrje, idGjenerues, idKonfigGjenerues))
                        idNivelGjenerues = 0;
                }
            }
            
            if (idDokNga != 0)
            {
                using (clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim())
                {
                    if (!dbRegj.ktheKokaMagazinaEkzistonDoksipasID(idDokNga))
                    {
                        idDokNga = 0;
                    }
                }
            }

            return krijoMagazine(kontrollogjendje, 0, idNivel, konfigAmbjenti.IdKonfigAmbjente, idKlientFurnitor, kodKlFurn, idMag, kodMag, dtDk, nrDk, idProj, nrProj, idKatDok, idDokNga, vl, idSt, idNder, idNdVt, 
                idPer, dtRegj, idllojdokmag, shenim, idNivelGjenerues, idKonfigGjenerues, idGjenerues, idDegeAdministrative, kodDegeAdministrative, idLlogari, nrllogari, idNjesiVartese, kodnjesivartese, mekonfirmim,
                idgrup1, idgrup2, idgrup3,pershkrimi, "", "", coltrupi, magtrans, fletekont, new clsKokaRezervime(), new clsDatabaseRegjistrim(), false, idAuto, automjeti, idrap, true, idkrijuesi, dtDk, 0, nrSerial,
                Nivfsh, Wtnic, operatori, HfArkiva, mallraTeDjegshme, shoqerimIKerkuar,tipi,transaksioni, transportuesi , "", "");
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public bool eshteILidhur()
        {
            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();
            bool lidhur = dbAdmin.eshteDokumentiILidhur(idKokaMagazina, idNivel, "T_KOKAMAGAZINA", "IDKOKAMAGAZINA");
            dbAdmin.Dispose();
            return lidhur;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public DataTable merrIdsDokLidhur()
        {
            using (var dbAdmin = new clsDatabaseAdmin())
                return dbAdmin.MerrDokLidhur(idKokaMagazina, idNivel, "T_KOKAMAGAZINA", "IDKOKAMAGAZINA");
        }

        /// <summary>
        /// Ruan nje objekt dokumenti magazine sebashku me trupin perkates per dokumentin e transferimit
        /// Nje objekt koka dokumenti magazine ka nje koleksion me trupin e dokumentit   ,
        /// ruajtja e nje dokumenti imponon ruajtjen edhe te nje colection-i me trupin
        /// Mqs cdo rresht i ri qe shtohet ne DB kerkon thirrjen e nje SP-je me parametra dhe koka e dokumentit bashke me trupin  konsiderohet si nje regjistrim,
        /// perdoret nje transaksion qe imponon regjistrimin e rregullt te nje te nje dokumenti magazine sebashku me trupin
        /// ne rastin e flete daljeve behet kontrolli i gjendjes nqs eshte zgjedhur kontrolli i gjendes tek regjistrimi i artikullit dhe nuk lejohet transaksioni nqs gjendja ne magazine eshte me e vogel sesa gjendja qe duhet te dale
        /// dokumentat e te njejtes date renditen sipas kohes kur jane ruajtur
        /// ne rastin e transferimit nuk ka kontabilizim
        /// </summary>
        /// <param name="newIdRenditje"></param>
        /// <param name="dtDk"> data e dokumentit te magazines</param>
        /// <param name="dtRegj"> data e regjistrimit te dokumentit te magazines</param>
        /// <param name="idKDok"> id kategorise se dokumentit me te cilin lidhet.</param>
        /// <param name="idKlFurn"> id e klient furnitorit me te cilin lidhet</param>
        /// <param name="idKonf"> id e konfigurimit te dokumentit</param>
        /// <param name="idLidhes">id e dokumentit nga e cila gjenerohet ne rastet e modifikimit </param>
        /// <param name="idllojdokmag"> id e llojit te dokumentit</param>
        /// <param name="idMag">id e magazines</param>
        /// <param name="idMagKoka">id ritese e kokes se magazines</param>
        /// <param name="idNder"> id e ndermarjes</param>
        /// <param name="idNdVt"> id e ndermarje vitit</param>
        /// <param name="idNiv"> id e nivelit te regjistrimit</param>
        /// <param name="idPer"> id e perdoruesit se e ben regjistrimin</param>
        /// <param name="idProj"> id e projektit ne te cilin ben pjese</param>
        /// <param name="idrenditjes"> id e renditjes per te renditur dokumentat e te njejtes date sipas kohes se regjistrimit</param>
        /// <param name="idSt"> id e statusit te dokumentit</param>
        /// <param name="nrDk">nr i dokumentit</param>
        /// <param name="nrProj"> nr i projektit</param>
        /// <param name="shenim"> shenime </param>
        /// <param name="vl"> vlera totale e dokumentit</param>
        /// <param name="idGjenerues">id e dokumentit nga gjenerohet magazina nga nje ambjent tjeter</param>
        /// <param name="idKonfigGjenerues">id e konfigurimit nga gjenerohet dokumenti</param>
        /// <param name="idNivelGjenerues">id e nivelit nga gjenerohet dokumenti</param>
        /// <param name="ocolTrupiMagazina">kolektion i trupit te magazines</param>
        /// <param name="modifikim">nese duhet bere modifikim apo jo</param>
        /// <param name="idgrup1">id e grupimit te pare </param>
        /// <param name="idgrup2">id e grupimit te dyte</param>
        /// <param name="idgrup3">id e grupimit te trete</param>
        /// <returns> kthen nje obj clsMesazh per te identifikuar statusin e ruajtjes se te dhenave ne DB</returns>
        public clsMesazh ruajHyrjePerTransferim(bool newIdRenditje, int idMagKoka, int idNiv, int idKonf, int idKlFurn, int idMag, DateTime dtDk, string nrDk, int idProj, string nrProj, int idKDok, int idLidhes, double vl, int idSt, int idNder, int idNdVt, int idPer, DateTime dtRegj, int idllojdokmag, string shenim, int idrenditjes, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues, int iddegeadministrative, int idllogari, int idnjesivartese, bool mekonfirmim, int idgrup1, int idgrup2, int idgrup3, colTrupiMagazina ocolTrupiMagazina, bool modifikim, DbCore.DbAsete.colSerialetMagazine serialemag, clsDatabaseRegjistrim dbRegj, int idkrijuesi, DateTime dttranp, string shofer, string targaSHF, int idkategoriseriali, String nrSerial, ref DataTable idTrupis, bool kaveprimepas)
        {
            clsMesazh mesazh = new clsMesazh();
            clsDatabazeAsete dbasete = new clsDatabazeAsete(dbRegj);
            //   clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim();

            mesazh.Status = true;
            if (!modifikim && dbRegj.ekzistonRegjistrimMagazineSipasIdentifikuese(0, idKonf, nrDk, idMag, dtDk, idNder, idNiv, idKlFurn, iddegeadministrative, idRaportDesing, idgrup1, idgrup2, idgrup3))

            {
                return new clsMesazh(false, "Ekziston nje dokument magazine me te njejtat te dhena!");
            }
            else
            {
                idrenditjes = dbRegj.vendosIdRenditjes(idrenditjes, idMag, dtDk);

                vl = 0;
                foreach (clsTrupiMagazina o in ocolTrupiMagazina)
                {
                    vl += o.Vlefta;
                }
                if (!mesazh.Status)
                    return mesazh;
                bool klientFiskalizuar = false;
                if (clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizim())
                    klientFiskalizuar = true;
                idMagKoka = dbRegj.ruajKokaMagazina(idMagKoka, idNiv, idKonf, idKlFurn, idMag, dtDk, nrDk, idProj, nrProj, idKDok, idLidhes, vl, idSt, idNder, idNdVt, idPer, dtRegj, idllojdokmag, shenim, idrenditjes, idNivelGjenerues, idKonfigGjenerues, idGjenerues, iddegeadministrative, idllogari, idnjesivartese, mekonfirmim, idgrup1, idgrup2, idgrup3, pershkrimi, magazinieri, adresa, idAutomjet, idRaportDesing, idkrijuesi, dttranp, shofer, targaSHF, idkategoriseriali, nrSerial, nivfsh, wtnic, idOperator,mallraTeDjegshme,shoqerimIKerkuar,transportuesi,klientFiskalizuar,tipi,transaksioni,clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizimV3());

                if (idMagKoka == 0)
                    return new clsMesazh("Nuk u be ruajta e kokes se hyrjes per transferim");
                colTrupiMagazina trupiivjetertransferim = new colTrupiMagazina();
                if (idLidhes != 0)
                    trupiivjetertransferim.mbushGjitheTrupiMagazinaNgaKoka(idLidhes, dbRegj);
                int i = 0;
                foreach (clsTrupiMagazina o in ocolTrupiMagazina)
                {//behet ruajtja e trupit te magazines
                    if (o.IdTrupiMagazina == 0)
                        o.IdTrupiMagazina = int.MaxValue - ocolTrupiMagazina.Count + i;
                    o.IdKokaMagazina = idMagKoka;
                    o.IdStatusDok = idSt;
                    if (!newIdRenditje)
                        o.IdRenditjes = trupiivjetertransferim[i].IdRenditjes;
                    i++;
                }
                //o.IdRenditjes = dbRegj.vendosIdRenditjesTrupi(o.IdMag, dtDk);
                //int idM;
                //mesazh = dbRegj.ruajTrupiMagazina(out idM, o.IdKokaMagazina, o.IdLlojVeprimi, o.IdArtikulli, o.IdNjesia, o.Sasia, o.Vlefta, o.Koeficenti, o.Shenja, o.SasiProgresive, o.VleftaProgresive, o.IdMag, o.Data, o.IdStatusDok, o.IdRenditjes, o.IdDetajimi, o.SasiProgresive, o.VlefteProgresiveDetajimi, o.IdDetajimi2, o.IdTrupiRezervimi, 0, 0, 0, 0, 0, o.Shenime);
                DataTable dt = ocolTrupiMagazina.ToDataTable<clsTrupiMagazina>("IdTrupiMagazina", "IdKokaMagazina", "IdLlojVeprimi", "IdArtikulli", "IdNjesia",
                    "Sasia", "Vlefta", "Koeficenti", "Shenja", "SasiProgresive", "VleftaProgresive", "IdMag", "Data", "IdStatusDok", "IdRenditjes", "IdDetajimi",
                    "SasiProgresiveDetajimi", "VlefteProgresiveDetajimi", "IdDetajimi2", "IdTrupiRezervimi", "IdTrupiKonvertimFSH", "IdTrupiKonvertimUSH",
                    "IdTrupiKonvertimUD", "IdKthimi", "IdTrupiShitjeGjenerimi", "Shenime", "IdArtikullSet", "IdBarkodi");

                idTrupis = dbRegj.ruajTrupiMagazina(false, dt, newIdRenditje); //toWatch Ridi
                if (!mesazh.Status)
                    return mesazh;

                foreach (DbCore.DbAsete.clsSerialetMagazine s in serialemag)
                {
                    s.IdDok = idMagKoka;
                    s.IdStatusDokumenti = idSt;
                    s.IdNjesiAdministrative = ocolTrupiMagazina[s.NrRendor].IdMag;
                    s.IdKonfigAmbjenti = idKonf;
                    mesazh = s.ruaj();
                    if (!mesazh.Status)
                    {
                        return mesazh;
                    }
                    clsAQTSeriale serial = new clsAQTSeriale();
                    serial.merrAQTSerialSipasID(s.IdAQTSeriali, dbasete);
                    if (serial.IdHistorikAktualPaSerial > 0)//nqs kemi seriale te ndashem kalojme id e dokumentit te magazines dhe sasine dhe cmimin e ketij seriali
                    {
                        mesazh = dbasete.modifikoHistorikAQTSerial(serial.IdHistorikAktualPaSerial, idPer, idMagKoka, s.Sasia, s.Cmimi, s.Vlefta, 1);
                        if (!mesazh.Status)
                        {
                            return mesazh;
                        }
                    }
                    if (idSt == 0)
                    {
                        mesazh = dbasete.modifikoAQTSerialNgaVeprimi(s.IdAQTSeriali, serial.AqtSerialDataHyrje, serial.AqtSerialDataMagAktive, serial.AqtSerialDataAmortizimfillestar, s.IdNjesiAdministrative, serial.IdHistorikAktualPaSerial, idPer, idSt, false);
                        if (!mesazh.Status)
                        {
                            return mesazh;
                        }
                    }
                    else
                    {
                        if (serial.AqtSerialDataHyrje == DateTime.MinValue)
                        {
                            if (serial.IdHistorikAktualPaSerial > 0)
                            {
                                clsHistorikAQTSeriale hist = new clsHistorikAQTSeriale();
                                hist.merrHistorikAQTSerialSipasID(serial.IdHistorikAktualPaSerial, idNder, dbasete);
                                if (hist.IdPrindi > 0)
                                {
                                    clsAQTSeriale histprind = new clsAQTSeriale();
                                    histprind.merrAQTSerialSipasID(hist.IdPrindi, dbasete);
                                    serial.AqtSerialDataHyrje = histprind.AqtSerialDataHyrje;
                                    serial.AqtSerialDataAmortizimfillestar = histprind.AqtSerialDataAmortizimfillestar;
                                }
                                else
                                {
                                    serial.AqtSerialDataHyrje = dtDk;
                                    serial.AqtSerialDataAmortizimfillestar = dtDk;
                                }
                            }
                            else
                            {
                                serial.AqtSerialDataHyrje = dtDk;
                                serial.AqtSerialDataAmortizimfillestar = dtDk;
                            }
                        }
                        if (serial.AqtSerialDataMagAktive == DateTime.MinValue)
                        {
                            clsNjesiAdministrative njesiadm = new clsNjesiAdministrative(s.IdNjesiAdministrative, dbRegj);
                            clsStatusMagazine_Asete status = new clsStatusMagazine_Asete(njesiadm.IdStatusAktualMagazine, dbasete);

                            if (status.Emertimi == "Aktive")
                                serial.AqtSerialDataMagAktive = dtDk;
                        }
                        clsSerialetMagazine vepfundit = new clsSerialetMagazine();

                        int magfundit = vepfundit.ktheSerialetMagazineSipasIdmagFundit(serial.IdAQTSerial, idNder, dtDk, new clsDatabazeAsete(dbRegj));
                        mesazh = dbasete.modifikoAQTSerialNgaVeprimi(s.IdAQTSeriali, serial.AqtSerialDataHyrje, serial.AqtSerialDataMagAktive, serial.AqtSerialDataAmortizimfillestar, magfundit, serial.IdHistorikAktualPaSerial, idPer, serial.IdStatusDokumenti, kaveprimepas);
                        if (!mesazh.Status)
                        {
                            return mesazh;
                        }
                    }
                }
                return mesazh;
            }
        }

        public static clsMesazh GjeneroKontabilitet(int idMagKoka, int idNiv, int idKonf, DateTime dtDk, string nrDk, double vl, int idSt, int idNder, int idNdVt, int idPer, DateTime dtRegj, int iddegeadministrative, colTrupiMagazina ocolTrupiMagazina, clsKokaFleteKontabel oFleteKontabel, clsKokaMagazina oMagazinaTransferim, int meKontabilizim, int idPeriudha, clsDatabaseRegjistrim dbRegj, string pershkrimDokKontabiliteti, int idLlojDok, int idDokNgaFK, int idllogari, int idnjesivartese, bool mekonfirmim, ref string shfaqmesazhapolupe, int iddokngaQKFK, DbQendraKosto.colTrupiQendraKosto trupivjeterqendra, bool gjithmone, colAmortizimiKoka col, DbCore.clsMesazh kontMesazh, int idMag)
        {

            if (idSt == 1 && meKontabilizim != 0) // idllojdokmag == 2 &&
            {//nese eshte dalje fleta kontabel krijohet ketu pasi vlefta percaktohet sipas cmimit mesatar me progresiv
             //DbShare.clsKonfigurimAmbjenti konf = new DbShare.clsKonfigurimAmbjenti();
             //konf.mbushKonfiguriminMeID(idKonf);
             //if (konf != null)
             //    idLlojDok = konf.IdKategori;
             //else
             //    idLlojDok = -1;
                clsDatabaseKontabilitet dbkont = new clsDatabaseKontabilitet(dbRegj);
                DbQendraKosto.colObjektivaKosto objektivat;
                List<double> vleratobjektiva; List<double> vleratobjektivamonbaze;
                List<int> idllogobj;
                System.Diagnostics.Stopwatch myWatchgjeneroKontabilizimMagazine = System.Diagnostics.Stopwatch.StartNew();
                oFleteKontabel = DbKontabiliteti.clsKokaFleteKontabel.GjeneroKontabilizimMagazine(idMagKoka, idNiv, idKonf, dtDk, nrDk, vl, idNder, idNdVt, idPer, dtRegj, ocolTrupiMagazina, pershkrimDokKontabiliteti, idDokNgaFK, idLlojDok, idPeriudha, idllogari, idnjesivartese, mekonfirmim, null, 0, 6, out objektivat, out vleratobjektiva, out vleratobjektivamonbaze, out idllogobj, iddegeadministrative, 0, 0, out shfaqmesazhapolupe, iddokngaQKFK, trupivjeterqendra, oMagazinaTransferim.OcolTrupiMagazina, col, gjithmone, dbkont, idMag);
                if (oFleteKontabel.OColTrupi.Count > 0)
                {
                    oFleteKontabel.IdGjenerues = idMagKoka;
                    oFleteKontabel.NrDukumentiKokaFleteKontabel = nrDk;

                    if (meKontabilizim == 1)
                        oFleteKontabel.Kontabilizuar = true;
                    else
                        oFleteKontabel.Kontabilizuar = false;
                    kontMesazh = oFleteKontabel.Ruaj(dbkont);
                    if (!kontMesazh.Status)
                    {
                        //dbRegj.rollbackTransaksion();
                        //if (connectionIRi)
                        //dbManager.RollBackTransaction();

                        return new clsMesazh(kontMesazh.Status, kontMesazh.PershkrimMesazhi);
                    }
                }
                myWatchgjeneroKontabilizimMagazine.Stop();
                if (myWatchgjeneroKontabilizimMagazine.Elapsed > new TimeSpan(0, 0, 1))
                    System.Diagnostics.Trace.WriteLine("myWatchgjeneroKontabilizimMagazine: " + myWatchgjeneroKontabilizimMagazine.Elapsed);
            }
            return new clsMesazh(true, "Kontabiliteti u krijua me sukses!");
        }
        /// <summary>
        /// Ruan nje objekt dokumenti magazine sebashku me trupin  dhe kontabilitetin perkates
        /// Nje objekt koka dokumenti magazine ka nje koleksion me trupin e dokumentit  dhe kontabilitetin perkates ,
        /// ruajtja e nje dokumenti imponon ruajtjen edhe te nje colection-i me trupin dhe kontabilitetin kur kontabilizohet
        /// Mqs cdo rresht i ri qe shtohet ne DB kerkon thirrjen e nje SP-je me parametra dhe koka e dokumentit bashke me trupin dhe kontabilitetin konsiderohet si nje regjistrim,
        /// perdoret nje transaksion qe imponon regjistrimin e rregullt te nje te nje dokumenti magazine sebashku me trupin dhe kontabilitetin perkates
        /// ne rastin e flete daljeve behet kontrolli i gjendjes nqs eshte zgjedhur kontrolli i gjendes tek regjistrimi i artikullit dhe nuk lejohet transaksioni nqs gjendja ne magazine eshte me e vogel sesa gjendja qe duhet te dale
        /// ne rastet e transferimit ruhet edhe nje dokument tjeter hyrje per transferimin ne magazinen e dyte
        /// dokumentat e te njejtes date renditen sipas kohes kur jane ruajtur
        /// </summary>
        /// <param name="eshteTrasferim"> tregon nqs dokumenti eshte transferim ne menyre qe te kryhet ruajtja e dokumentit te dyte te transferimit</param>
        /// <param name="dtDk"> data e dokumentit te magazines</param>
        /// <param name="dtRegj"> data e regjistrimit te dokumentit te magazines</param>
        /// <param name="idKDok"> id kategorise se dokumentit me te cilin lidhet.</param>
        /// <param name="idKlFurn"> id e klient furnitorit me te cilin lidhet</param>
        /// <param name="idKonf"> id e konfigurimit te dokumentit</param>
        /// <param name="idLidhes">id e dokumentit nga e cila gjenerohet ne rastet e modifikimit </param>
        /// <param name="idllojdokmag"> id e llojit te dokumentit</param>
        /// <param name="idMag">id e magazines</param>
        /// <param name="idMagKoka">id ritese e kokes se magazines</param>
        /// <param name="idNder"> id e ndermarjes</param>
        /// <param name="idNdVt"> id e ndermarje vitit</param>
        /// <param name="idNiv"> id e nivelit te regjistrimit</param>
        /// <param name="idPer"> id e perdoruesit se e ben regjistrimin</param>
        /// <param name="idProj"> id e projektit ne te cilin ben pjese</param>
        /// <param name="idrenditjes"> id e renditjes per te renditur dokumentat e te njejtes date sipas kohes se regjistrimit</param>
        /// <param name="idSt"> id e statusit te dokumentit</param>
        /// <param name="nrDk">nr i dokumentit</param>
        /// <param name="nrProj"> nr i projektit</param>
        /// <param name="shenim"> shenime </param>
        /// <param name="vl"> vlera totale e dokumentit</param>
        /// <param name="idGjenerues">id e dokumentit nga gjenerohet magazina nga nje ambjent tjeter</param>
        /// <param name="idKonfigGjenerues">id e konfigurimit nga gjenerohet dokumenti</param>
        /// <param name="idNivelGjenerues">id e nivelit nga gjenerohet dokumenti</param>
        /// <param name="ocolTrupiMagazina">kolektion i trupit te magazines</param>
        /// <param name="oFleteKontabel">kolektion i fleteve kontabel</param>
        /// <param name="oMagazinaTransferim">kolektion i transferimit ne magazine</param>
        /// <param name="modifikim"> tregon nqs dokumenti po modifikohet ne menyre qe te mos behet kontrolli nqs ekziston ky dokument per kete ndermarje</param>
        /// <param name="meKontabilizim"> tregon nese dokumenti i magazines do te kontabilizohet apo jo</param>
        /// <param name="iddoktransferimi"> id e dokumentit te transferimit</param>
        /// <param name="idgrup1">id e grupimit te pare </param>
        /// <param name="idgrup2">id e grupimit te dyte</param>
        /// <param name="idgrup3">id e grupimit te trete</param>
        /// <returns> kthen nje obj clsMesazh per te identifikuar statusin e ruajtjes se te dhenave ne DB</returns>
        /// <param name="pershkrimDokKontabiliteti"></param>
        /// <param name="idLlojDok"></param>
        /// <param name="idDokNga"></param>
        /// <param name="kontabilizioamortizim"> perdoret per rastet kur kemi kontabilizim magazine brenda shitjes dhe nuk kontabilizohet magazina por duhet te kontabilizohet amortizimi</param>
        /// <param name="mosLlogaritAmortizimShtese"></param>

        public clsMesazh ruajMagazina(out int idMagKoka, int idNiv, int idKonf, int idKlFurn, int idMag, DateTime dtDk, string nrDk, int idProj, string nrProj, int idKDok, int idLidhes, double vl, int idSt, int idNder,
            int idNdVt, int idPer, DateTime dtRegj, int idllojdokmag, string shenim, int idrenditjes, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues, int iddegeadministrative, colTrupiMagazina ocolTrupiMagazina,
            clsKokaFleteKontabel oFleteKontabel, clsKokaMagazina oMagazinaTransferim, bool eshteTrasferim, bool modifikim, int meKontabilizim, int iddoktransferimi, int idPeriudha, clsDatabaseRegjistrim dbRegj,
            string pershkrimDokKontabiliteti, int idLlojDok, int idDokNgaFK, int idllogari, int idnjesivartese, bool mekonfirmim, int idgrup1, int idgrup2, int idgrup3, string pershkrimi, string magazinieri, string adresa,
            out string shfaqmesazhapolupe, int iddokngaQKFK, DbQendraKosto.colTrupiQendraKosto trupivjeterqendra, clsKokaRezervime kokarezervime, bool kontrollodisponibel, bool eshteOwn, int idAutomjeti, int idRap, 
            colSerialetMagazine serialemag, colSerialetMagazine serialetransf, clsKonfigurimAmbjenti konfamortizimi, clsKonfigurimAmbjenti konfamortizimihyrje, clsKokaMagazina koka, colAmortizimiKoka colAmortizimetEVjetra,
            colAmortizimiKoka colAmortizimetEVjetraHyrje, int idDokNgaFKAM, int iddokngaQKFKAM, DbQendraKosto.colTrupiQendraKosto trupivjeterqendraAM, bool isshitje, clsKokaShitje kokashitje, bool ngarivleresimi, bool gjithmone,
            colAmortizimiKoka col, int kontabilizoamortizim, bool mosLlogaritAmortizimShtese, int idkrijuesi, DateTime dttranp, string shoferi, string targaShoferi, colTrupiMagazina coltrupivjeterKryesor, bool ruajrenditje,
            int[] idinv, bool kaveprimepas, bool kontrolloIMEIFifo, bool blerjengadealer, bool promocione, int idKategoriSeriali, string nrSeriali,  out string mesazhmevonshem, colSerialeUnikeKategori kategorite, bool serialeNeDetajim,
            bool bashkoArtikujt, string nivfsh, string wtnic, int operatori, bool mallraTeDjegshme, bool shoqerimIKerkuar, int transportuesi, string tipi,string transaksioni)
        {
            mesazhmevonshem = "";
            clsMesazh mesazh = new clsMesazh();
            clsMesazh kontMesazh = new clsMesazh(true);

            clsDatabazeAsete dbasete = new clsDatabazeAsete(dbRegj);
            clsDatabaseShare dbShare = new clsDatabaseShare(dbRegj);
            clsDatabaseInventari dbinventari = new clsDatabaseInventari(dbRegj);
            shfaqmesazhapolupe = "jo";
            colHistorikAQTSeriale colHistorik = new colHistorikAQTSeriale();

            idMagKoka = 0;

            idrenditjes = dbRegj.vendosIdRenditjes(idrenditjes, idMag, dtDk);
            if (idllojdokmag == 1 && !ngarivleresimi)
            {
                mesazh = serialemag.kontrolloNevojatArtikullitPerSeriale(koka, dbasete);
                if (!mesazh.Status)
                    return mesazh;
            }
            
            System.Diagnostics.Stopwatch myWatchLlogaritkosto = System.Diagnostics.Stopwatch.StartNew();

            #region llogaritkosto

            colTrupiMagazina trupatEShtuar = new colTrupiMagazina();
            colTrupiMagazina trupatEShtuarTransferim = new colTrupiMagazina();
            bool isDalje = idllojdokmag == 2 ? true : false;
            bool newIdRenditje = !(modifikim && ruajrenditje && coltrupivjeterKryesor.Count != 0);
            bool kontrollGjendjeDokumenti = (clsAlternativaKushti.getAlternativa(this.idKonfigAmbjente, "KGJA") == "Jo");
            bool detajimDetyrueshem = (clsAlternativaKushti.getAlternativa(this.idKonfigAmbjente, "PDS") == "Po");
            colTrupiMagazina colTrupiMAgazinaDistinct = new colTrupiMagazina();


            for (int i = 0, nrRreshta = ocolTrupiMagazina.Count, nrRreshtaDokVjeter = coltrupivjeterKryesor.Count; i < nrRreshta; i++)
            {
                clsTrupiMagazina rreshtDokMag = ocolTrupiMagazina[i];
                var magEkzistencial = clsNjesiAdministrative.ktheMagazineSipasIdNeseEkziston(rreshtDokMag.IdMag, dbRegj);
                bool magMeNdjekjeGjendje = magEkzistencial == null ? false : magEkzistencial.NdjekjeGjendje;

                if (idllojdokmag == 2 || (idllojdokmag == 1 && rreshtDokMag.Sasia < 0))
                {
                    if (newIdRenditje) //nuk eshte modifikim ose kemi rreshta plus
                        rreshtDokMag.IdRenditjes = Int16.MaxValue; //dbRegj.vendosIdRenditjesTrupi(rreshtDokMag.IdMag, dtDk);// idrenditjes;
                    else
                        rreshtDokMag.IdRenditjes = coltrupivjeterKryesor[i].IdRenditjes;
                    DbInventari.clsArtikulli artikulli = (DbInventari.clsArtikulli)(rreshtDokMag.Element);

                    if (konfamortizimi.IdKonfigAmbjente > 0 && artikulli.LlojiArt)///vetem kur kemi amortizim
                    {
                        double totaliArtikullitperserial = ktheTotalinArtikullit(ocolTrupiMagazina, rreshtDokMag.IdArtikulli, rreshtDokMag.IdMag);
                        int nrtrupateshtuarPerketeRresht = trupatEShtuar.Count;
                        mesazh = serialemag.KontrolloSerialdheMerrSerialinSipasFifo(rreshtDokMag, artikulli, colHistorik, i, idNder, idNiv, idKonf, idSt, idPer, dtDk, totaliArtikullitperserial, ocolTrupiMagazina.Count, trupatEShtuar, serialetransf, oMagazinaTransferim.ocolTrupiMagazina.Count > 0 ? oMagazinaTransferim.ocolTrupiMagazina[i].IdMag : 0, trupatEShtuarTransferim, dbasete, iddoktransferimi);
                        if (!mesazh.Status)
                            return mesazh;


                    }
                    else
                    {
                        double cm = rreshtDokMag.Cmimi;
                        int idkthimi = 0;
                        if (kokashitje.OColTrupiShitje != null && kokashitje.OColTrupiShitje.Count > rreshtDokMag.RreshtiShitjes)
                        {
                            idkthimi = kokashitje.OColTrupiShitje[rreshtDokMag.RreshtiShitjes].IdTrupiKthim;
                        }
                        if (idkthimi != 0)
                        {
                            rreshtDokMag.IdKthimi = dbRegj.ktheIdTrupiMagKthimi(idkthimi, rreshtDokMag.IdArtikulli);
                        }
                        if (idkthimi != 0 && (rreshtDokMag.Sasia < 0))
                        {
                            cm = dbRegj.llogaritCmimPerKthimBlerje(rreshtDokMag.IdKthimi);
                        }
                        else
                        {
                            double gjendjeArtikulliTeNjejte = 0;
                            double gjendjeArtikulliDetajimTeNjejte = 0;
                            if (artikulli.MetodeKostojeArtikulli == 4 || artikulli.MetodeKostojeArtikulli == 5) //fifo or fifo2
                            {
                                for (int j = 0; j < i; j++)
                                {
                                    if (ocolTrupiMagazina[j].IdArtikulli == rreshtDokMag.IdArtikulli && ocolTrupiMagazina[j].IdMag == rreshtDokMag.IdMag)
                                    {
                                        gjendjeArtikulliTeNjejte += ocolTrupiMagazina[j].Sasia * ocolTrupiMagazina[j].Koeficenti;
                                        if (ocolTrupiMagazina[j].IdDetajimi == rreshtDokMag.IdDetajimi)
                                            gjendjeArtikulliDetajimTeNjejte += ocolTrupiMagazina[j].Sasia * ocolTrupiMagazina[j].Koeficenti;
                                    }
                                }
                            }
                            double cmimiartnjejte = 0, sasiartnjejte = 0;
                            if ((idllojdokmag == 1 && rreshtDokMag.Sasia < 0))
                            {
                                clsTrupiMagazina trupinjejte = ocolTrupiMagazina.Find(x => x.IdArtikulli == rreshtDokMag.IdArtikulli && x.IdMag == rreshtDokMag.IdMag && x.IdDetajimi == rreshtDokMag.IdDetajimi && x.IdDetajimi2 == rreshtDokMag.IdDetajimi2 && x.Sasia > 0);
                                cmimiartnjejte = trupinjejte == null ? 0 : trupinjejte.Cmimi;
                                sasiartnjejte = ocolTrupiMagazina.Where(x => x.IdArtikulli == rreshtDokMag.IdArtikulli && x.IdMag == rreshtDokMag.IdMag && x.IdDetajimi == rreshtDokMag.IdDetajimi && x.IdDetajimi2 == rreshtDokMag.IdDetajimi2 && x.Sasia > 0).Sum(x => x.Sasia);
                            }
                            bool merrSipasDetajimit = rreshtDokMag.IdDetajimi != -1 && rreshtDokMag.IdDetajimi != 0 && artikulli.KontrollCmimi;
                            switch (artikulli.MetodeKostojeArtikulli)
                            {
                                case 1://cmim mesatar hyrje para dalje
                                    if (merrSipasDetajimit)//nqs rreshti eshte me detajim marrim cmimin mesatar per ate detajim prn cmimin mesatar te artikullit
                                        cm = dbRegj.llogaritCmimMesatarMeTotalDetajimitHPD(rreshtDokMag.IdArtikulli, rreshtDokMag.IdDetajimi, rreshtDokMag.IdMag, rreshtDokMag.Data, rreshtDokMag.IdRenditjes, cmimiartnjejte, sasiartnjejte);
                                    else
                                        cm = dbRegj.llogaritCmimMesatarMeTotalHPD(rreshtDokMag.IdArtikulli, rreshtDokMag.IdMag, rreshtDokMag.Data, rreshtDokMag.IdRenditjes, cmimiartnjejte, sasiartnjejte, -1);
                                    break;
                                //nqs nje artikull ndodhet me shume se nje here ne gride merr cmimin paraardhes per ate artikull dhe nuk akseson dhe njeher databazen
                                case 2://cmim mesatar sipas id se renditjes
                                    clsTrupiMagazina trupinjejte = colTrupiMAgazinaDistinct?.Find(x => x.IdArtikulli == rreshtDokMag.IdArtikulli && x.IdMag == rreshtDokMag.IdMag && x.IdDetajimi == rreshtDokMag.IdDetajimi);
                                    if (trupinjejte == null) {
                                        if (merrSipasDetajimit)//nqs rreshti eshte me detajim marrim cmimin mesatar per ate detajim ndryshe cmimin mesatar te artikullit
                                            cm = dbRegj.llogaritCmimMesatarMeTotalDetajimitSipasRadhes(rreshtDokMag.IdArtikulli, rreshtDokMag.IdDetajimi, rreshtDokMag.IdMag, rreshtDokMag.Data, rreshtDokMag.IdRenditjes, cmimiartnjejte, sasiartnjejte, 0);
                                        else
                                            cm = dbRegj.llogaritCmimMesatarMeTotalSipasRadhes(rreshtDokMag.Cmimi, rreshtDokMag.IdArtikulli, rreshtDokMag.IdMag, rreshtDokMag.Data, rreshtDokMag.IdRenditjes, cmimiartnjejte, sasiartnjejte, 0);

                                        rreshtDokMag.Cmimi = cm;
                                        colTrupiMAgazinaDistinct.Add(rreshtDokMag);
                                    }
                                    else
                                        cm = colTrupiMAgazinaDistinct.FirstOrDefault(x => x.IdArtikulli == rreshtDokMag.IdArtikulli && x.IdMag == rreshtDokMag.IdMag && x.IdDetajimi == rreshtDokMag.IdDetajimi).Cmimi;
                                    break;
                                case 6:
                                    double cmimireferues = 0;
                                    if (artikulli.MetodeKostojeArtikulli == 6)
                                        cmimireferues = merrCmimReferues(dtDk, idNder, idPer, rreshtDokMag.IdArtikulli, rreshtDokMag.IdNjesia, dbinventari);
                                    if (merrSipasDetajimit)//nqs rreshti eshte me detajim marrim cmimin mesatar per ate detajim ndryshe cmimin mesatar te artikullit
                                        cm = dbRegj.llogaritCmimMesatarMeTotalDetajimitSipasRadhes(rreshtDokMag.IdArtikulli, rreshtDokMag.IdDetajimi, rreshtDokMag.IdMag, rreshtDokMag.Data, rreshtDokMag.IdRenditjes, cmimiartnjejte, sasiartnjejte, cmimireferues);
                                    else cm = dbRegj.llogaritCmimMesatarMeTotalSipasRadhes(rreshtDokMag.Cmimi, rreshtDokMag.IdArtikulli, rreshtDokMag.IdMag, rreshtDokMag.Data, rreshtDokMag.IdRenditjes, cmimiartnjejte, sasiartnjejte, cmimireferues);
                                    break;
                                case 3:// cmim mesatar sipas progresivit
                                    if (merrSipasDetajimit)//nqs rreshti eshte me detajim marrim cmimin mesatar per ate detajim prn cmimin mesatar te artikullit
                                        cm = dbRegj.llogaritCmimMesatarMeProgresivDetajimit(rreshtDokMag.Cmimi, rreshtDokMag.IdArtikulli, rreshtDokMag.IdDetajimi, rreshtDokMag.IdMag, rreshtDokMag.Data, rreshtDokMag.IdRenditjes, cmimiartnjejte, sasiartnjejte);
                                    else
                                        cm = dbRegj.llogaritCmimMesatarMeProgresiv(rreshtDokMag.Cmimi, rreshtDokMag.IdArtikulli, rreshtDokMag.IdMag, rreshtDokMag.Data, rreshtDokMag.IdRenditjes, cmimiartnjejte, sasiartnjejte);
                                    break;

                                case 4:  //fifo
                                    int idDetajimi = merrSipasDetajimit ? rreshtDokMag.IdDetajimi : 0;
                                    double gjendje = dbRegj.ktheSasineProgresiveArtikullitEdheSipasDetajimit(rreshtDokMag.IdArtikulli, idDetajimi, rreshtDokMag.IdMag, rreshtDokMag.Data, rreshtDokMag.IdRenditjes);
                                    double gjendjaPerKosto = merrSipasDetajimit ? gjendje - gjendjeArtikulliDetajimTeNjejte : gjendje - gjendjeArtikulliTeNjejte;
                                    if (gjendje == 0 && cmimiartnjejte != 0)
                                        cm = cmimiartnjejte;
                                    else
                                        cm = clsTrupiMagazina.llogaritCmimMesatarFifo(rreshtDokMag.IdArtikulli, idDetajimi, rreshtDokMag.IdMag, rreshtDokMag.Data, rreshtDokMag.IdRenditjes, Math.Abs(rreshtDokMag.Sasia), rreshtDokMag.Koeficenti, gjendjaPerKosto, merrSipasDetajimit, dbRegj, false, cmimiartnjejte);
                                    break;

                                case 5: //fifo2
                                    if (merrSipasDetajimit)
                                    {
                                        double gjendjeFifo2 = dbRegj.ktheSasineTotaleSipasDetajimitHPD(rreshtDokMag.IdArtikulli, rreshtDokMag.IdDetajimi, rreshtDokMag.IdMag, rreshtDokMag.Data, rreshtDokMag.IdRenditjes);
                                        if (gjendjeFifo2 == 0 && cmimiartnjejte != 0)
                                            cm = cmimiartnjejte;
                                        else
                                            cm = clsTrupiMagazina.llogaritCmimMesatarFifo(rreshtDokMag.IdArtikulli, rreshtDokMag.IdDetajimi, rreshtDokMag.IdMag, rreshtDokMag.Data, rreshtDokMag.IdRenditjes, Math.Abs(rreshtDokMag.Sasia), rreshtDokMag.Koeficenti, gjendjeFifo2 - gjendjeArtikulliDetajimTeNjejte, true, dbRegj, true, cmimiartnjejte);
                                    }
                                    else
                                    {
                                        double gjendjeFifo2 = dbRegj.ktheSasineTotaleSipasArtikullitHPD(rreshtDokMag.IdArtikulli, rreshtDokMag.IdMag, rreshtDokMag.Data, rreshtDokMag.IdRenditjes);
                                        if (gjendjeFifo2 == 0 && cmimiartnjejte != 0)
                                            cm = cmimiartnjejte;
                                        else
                                            cm = clsTrupiMagazina.llogaritCmimMesatarFifo(rreshtDokMag.IdArtikulli, rreshtDokMag.IdDetajimi, rreshtDokMag.IdMag, rreshtDokMag.Data, rreshtDokMag.IdRenditjes, Math.Abs(rreshtDokMag.Sasia), rreshtDokMag.Koeficenti, gjendjeFifo2 - gjendjeArtikulliTeNjejte, false, dbRegj, true, cmimiartnjejte);
                                    }
                                    break;

                                default: //UFO
                                    throw new MyException("Metode Kosto artikulli e panjohur");
                            }
                        }
                        
                        rreshtDokMag.Vlefta = cm * rreshtDokMag.Sasia * rreshtDokMag.Koeficenti;    // u shtua koeficienti per te llogaritur cmimn ne njesine e dyte
                                                                                                    //}
                        rreshtDokMag.Cmimi = cm;
                    }
                    double totaliArtikullit = 0;//totali i artikullit qe duam te bejme dalje(vlera e ketij rreshti + gjithe rreshtave te tjere qe kane kete artikull)
                    
                    if (rreshtDokMag.IdLlojVeprimi != 0 && idSt == 1)
                    {
                        if (!modifikim)    //Kontrolli i gjendjes behet ketu vetem kur dokumenti eshte i ri, per te vjetrit kontrolli behet me pare
                        {
                            totaliArtikullit = totaliArtikullit == 0 ? ktheTotalinArtikullit(ocolTrupiMagazina, artikulli.IdArtikulli, rreshtDokMag.IdMag) : totaliArtikullit;
                            mesazh = KontrolloGjendjeArtikulli(isDalje, totaliArtikullit, idNder, ocolTrupiMagazina, artikulli, rreshtDokMag, dbinventari, dbRegj, kontrollGjendjeDokumenti, magMeNdjekjeGjendje);
                            if (!mesazh.Status)
                                return mesazh;
                        }
                        if (detajimDetyrueshem && isDalje && rreshtDokMag.IdDetajimi > 0)
                        {
                            if (clsTrupiMagazina.ekzistonIMEIneDokKthimiDraft(rreshtDokMag.IdDetajimi, rreshtDokMag.IdMag, dtDk))
                                return new MesazhGabimi(string.Format(MessagesResource.Messages["msgImeiNeKthimDraft"], rreshtDokMag.KodDetajimi1));
                        }
                    }
                    if (kontrollodisponibel && artikulli.KontrollGjendjeArtikulli && artikulli.Klasa != 3 && artikulli.Klasa != 2 && idSt == 1)
                    {
                        double sasimag = clsTrupiMagazina.merrSasi(artikulli, rreshtDokMag.IdMag, rreshtDokMag.Data, -1, dbRegj);
                       double sasiDaljeRezervimi = (koka.OKokaRezervime != null && koka.OKokaRezervime.OcolTrupiRezervime != null) ? koka.OKokaRezervime.OcolTrupiRezervime.ktheSasiDaljeRezervimi(rreshtDokMag.IdArtikulli, rreshtDokMag.IdMag) : 0;
                        //double sasiadisp = sasimag - sasiarez + sasiaporUB - sasiarezUB;

                        totaliArtikullit = totaliArtikullit == 0 ? ktheTotalinArtikullit(ocolTrupiMagazina, artikulli.IdArtikulli, rreshtDokMag.IdMag) : totaliArtikullit;
                        if (sasiDaljeRezervimi == 0 || sasimag < sasiDaljeRezervimi)
                        {
                            double sasiarez = dbRegj.ktheSasineRezervuarSipasArtikullitDheMagazines(rreshtDokMag.IdArtikulli, 0, rreshtDokMag.IdMag, rreshtDokMag.Data);
                            double sasiadisp = sasimag - (sasiarez - sasiDaljeRezervimi);

                            if (idllojdokmag == 2)
                            {
                                bool kontrollGjendje = (clsAlternativaKushti.getAlternativa(idKonf, "KGJA") == "Po");
                                if (kontrollGjendje && totaliArtikullit > sasiadisp)
                                    return new clsMesazh(false, "Sasia e daljes është më e madhe se gjendja disponibel " + sasiadisp + " e artikullit: " + artikulli.KodArtikulli + "!");
                            }
                            else
                            {
                                if (Math.Abs(totaliArtikullit) > sasiadisp)
                                {
                                    DbInventari.clsDetajimArtikulli detArt = new DbInventari.clsDetajimArtikulli(rreshtDokMag.IdDetajimi2);
                                    return new clsMesazh(false, "Sasia e hyrjes është më e madhe se gjendja disponibel " + sasiadisp + " e artikullit: " + artikulli.KodArtikulli + "!");
                                }
                            }
                        }
                    }
                }
            }

            ocolTrupiMagazina.AddRange(trupatEShtuar);//shtohen rreshtat e rinj
            System.Diagnostics.Stopwatch myWatchvleftaTotale = System.Diagnostics.Stopwatch.StartNew();
            vl = ocolTrupiMagazina.Select(o => o.Vlefta).Sum();
            myWatchvleftaTotale.Stop();
            if (myWatchvleftaTotale.Elapsed > new TimeSpan(0, 0, 1))
                System.Diagnostics.Trace.WriteLine("vleftaTotale: " + myWatchvleftaTotale.Elapsed);

            #endregion llogaritkosto

            myWatchLlogaritkosto.Stop();
            if (myWatchLlogaritkosto.Elapsed > new TimeSpan(0, 0, 1))
                System.Diagnostics.Trace.WriteLine("myWatchLlogaritkosto: " + myWatchLlogaritkosto.Elapsed);
            bool klientFiskalizuar = false;
            if (clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizim())
                klientFiskalizuar = true;
            idMagKoka = dbRegj.ruajKokaMagazina(idMagKoka, idNiv, idKonf, idKlFurn, idMag, dtDk, nrDk, idProj, nrProj, idKDok, idLidhes, vl, idSt, idNder, idNdVt, idPer, dtRegj, idllojdokmag, shenim, idrenditjes, 
                idNivelGjenerues, idKonfigGjenerues, idGjenerues, iddegeadministrative, idllogari, idnjesivartese, mekonfirmim, idgrup1, idgrup2, idgrup3, pershkrimi, magazinieri, adresa, idAutomjet, idRap, idkrijuesi,
                dttranp, shoferi, targaShoferi, idKategoriSeriali, nrSerial, nivfsh, wtnic, operatori, mallraTeDjegshme,shoqerimIKerkuar,transportuesi,klientFiskalizuar,tipi,transaksioni,clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizimV3());

            if (idMagKoka == 0)
                return new clsMesazh(false, "Ndodhi një Gabim gjatë ruajtjes së Kokës së Magazinës");
            if (modifikim)
            {
                mesazh = dbRegj.modifikoKonvertimSipasIdDokKonvertuar(idLidhes, idMagKoka);
                if (!mesazh.Status)
                {
                    return mesazh;
                }
            }
            DataTable idTrupaTransferim = null;
            if (eshteTrasferim)
            {
                System.Diagnostics.Stopwatch myWatchEshteTrasferim = System.Diagnostics.Stopwatch.StartNew();
                oMagazinaTransferim.IdGjenerues = idMagKoka;
                oMagazinaTransferim.IdKonfigGjenerues = idKonf;
                oMagazinaTransferim.IdNivelGjenerues = idNiv;
                oMagazinaTransferim.IdDokNga = iddoktransferimi;
                oMagazinaTransferim.nrDok = nrDk;
                oMagazinaTransferim.nrProjekt = nrProj;
                oMagazinaTransferim.ocolTrupiMagazina.AddRange(trupatEShtuarTransferim);
                RregulloTrupinEDokTransferimit(kategorite, serialeNeDetajim, bashkoArtikujt, oMagazinaTransferim.IdMagazina);
                if (myWatchEshteTrasferim.Elapsed > new TimeSpan(0, 0, 1))
                    System.Diagnostics.Trace.WriteLine("myWatchEshteTrasferimSub1: " + myWatchEshteTrasferim.Elapsed);

                mesazh = ruajHyrjePerTransferim(newIdRenditje, oMagazinaTransferim.IdKokaMagazina, oMagazinaTransferim.IdNivel, oMagazinaTransferim.IdKonfigAmbjente, oMagazinaTransferim.IdKlientFurnitor, oMagazinaTransferim.IdMagazina, oMagazinaTransferim.DtDok, oMagazinaTransferim.NrDok, oMagazinaTransferim.IdProjekt, oMagazinaTransferim.NrProjekt, oMagazinaTransferim.IdKategoria, oMagazinaTransferim.IdDokNga, oMagazinaTransferim.Vlefta, oMagazinaTransferim.IdStatusDok, oMagazinaTransferim.IdNdermarrje, oMagazinaTransferim.IdNdermarrjeVit, oMagazinaTransferim.IdPerdoruesi, oMagazinaTransferim.DtRegjistrimi, oMagazinaTransferim.IdLlojDokumentiMagazine, oMagazinaTransferim.Shenime, oMagazinaTransferim.IdRenditjes, oMagazinaTransferim.IdNivelGjenerues, oMagazinaTransferim.IdKonfigGjenerues, oMagazinaTransferim.IdGjenerues, oMagazinaTransferim.IdDegeAdministrative, oMagazinaTransferim.idLlogari, oMagazinaTransferim.IdNjesiVartese, oMagazinaTransferim.MeKonfirmim, oMagazinaTransferim.IdGrup1, oMagazinaTransferim.idGrup2, oMagazinaTransferim.IdGrup3, oMagazinaTransferim.OcolTrupiMagazina, modifikim, serialetransf, dbRegj, idkrijuesi, dttranp, shoferi, targaShoferi, oMagazinaTransferim.idKategoriSeriali,nrSerial, ref idTrupaTransferim, kaveprimepas);
                if (!mesazh.Status)
                {
                    return mesazh;
                }
                myWatchEshteTrasferim.Stop();
                if (myWatchEshteTrasferim.Elapsed > new TimeSpan(0, 0, 1))
                    System.Diagnostics.Trace.WriteLine("myWatchEshteTrasferim: " + myWatchEshteTrasferim.Elapsed);
            }
            System.Diagnostics.Stopwatch myWatchRuajTrupin = System.Diagnostics.Stopwatch.StartNew();

            #region Ruajtrupin

            //int count = 0;
            int iii = 0;
            System.Diagnostics.Stopwatch myWatchRuajTrupinOcolTrupiMagazina = System.Diagnostics.Stopwatch.StartNew();
            DbInventari.clsArtikulli tmpArt;

            string idtrupashkonvertimeshitje = string.Join(",", ocolTrupiMagazina.Where(x => x.IdTrupiKonvertimFSH > 0).Select(x => x.IdTrupiKonvertimFSH).Distinct());
            if (idtrupashkonvertimeshitje != "")
                idtrupashkonvertimeshitje += ",";// nese ka dhe IdTrupiKonvertimFSH dhe IdTrupiKonvertimUSH i duhet "," perpara 
            idtrupashkonvertimeshitje += string.Join(",", ocolTrupiMagazina.Where(x => x.IdTrupiKonvertimUSH > 0 && x.IdTrupiKonvertimFSH == 0).Select(x => x.IdTrupiKonvertimUSH).Distinct());

            string idtrupashkonvertimemag = string.Join(",", ocolTrupiMagazina.Where(x => x.IdTrupiKonvertimUD > 0).Select(x => x.IdTrupiKonvertimUD).Distinct());
            string idtrupashkonvertimerezervimi = string.Join(",", ocolTrupiMagazina.Where(x => x.IdTrupiRezervimi > 0).Select(x => x.IdTrupiRezervimi).Distinct());
            if (!modifikim && !(string.IsNullOrEmpty(idtrupashkonvertimeshitje) && string.IsNullOrEmpty(idtrupashkonvertimemag) && string.IsNullOrEmpty(idtrupashkonvertimerezervimi)))
            {
                mesazh = clsKokaShitje.kontrolloEkzistojneDokQePoKonvertohen(idtrupashkonvertimeshitje, idtrupashkonvertimemag, idtrupashkonvertimerezervimi);
                if (!mesazh.Status)
                    return mesazh;
            }

            foreach (clsTrupiMagazina trup in ocolTrupiMagazina)
            {//behet ruajtja e trupit te magazines
                trup.IdKokaMagazina = idMagKoka;
                trup.IdStatusDok = idSt;

                if (trup.IdTrupiMagazina == 0)
                    trup.IdTrupiMagazina = int.MaxValue - ocolTrupiMagazina.Count + iii;
                //if (!modifikim || !ruajrenditje)
                //trup.IdRenditjes = dbRegj.vendosIdRenditjesTrupi(trup.IdMag, dtDk);
                //else
                if (!newIdRenditje)
                    trup.IdRenditjes = coltrupivjeterKryesor[iii].IdRenditjes;
                //int idM;

                //kontrollojme nqs detajimi i vendosur tek fusha eshte i ri, apo ekziston aktualisht.
                //Nqs eshte i ri (pra nuk ekziston), atehere ai do krijohet dhe me pas do te behet edhe lidhja me artikullin.
                //Nqs ky detajim ekziston, por nuk eshte i lidhur me artikullin, atehere do te behet lidhja e artikullit me detajimin.

                if (trup.IdTrupiKonvertimFSH > 0 && trup.IdTrupiKonvertimUD == 0)
                {
                    if (clsAlternativaKushti.getAlternativaSipasIdTrupiDok(trup.IdTrupiKonvertimFSH, "NK", dbShare) == "Po")
                    {
                        double sasia;
                        if (((clsArtikulli)trup.Element).Njesi1Artikulli == trup.IdNjesia)
                            sasia = trup.Sasia;
                        else
                            sasia = trup.Sasia * trup.Koeficenti;
                        if (Math.Abs(sasia) > dbRegj.ktheSasineKonvertuarDheKthyer(trup.IdTrupiKonvertimFSH, idNdermarrje))
                            return new clsMesazh(false, "Sasia e artikullit " + trup.KodiArtikull + " qe doni te konvertoni eshte me e madhe se sasia e mbetur!");
                    }
                }
               
                if (trup.IdArtikulli > 0) //krijodetajime sherben per rastin kur krijohen detajimet nga grida te trupi i shitjes.ne ate rast ato rkijohen direkte me trupin e shitjes dhe ska nevoje te krijohen ketu
                {
                    tmpArt = (DbInventari.clsArtikulli)trup.Element;
                    if (trup.IdDetajimi <= 0 && !String.IsNullOrEmpty(trup.KodDetajimi1))
                    {
                        if (!DbInventari.clsDetajimArtikulli.ekziston(trup.KodDetajimi1, idNder, dbinventari))
                        {

                            int llojDetajim = 0;
                            if (tmpArt.IdKategoriDetajimi == 3)
                                llojDetajim = 3;
                            else if (tmpArt.IdKategoriDetajimi == 4)
                                llojDetajim = 1;

                            DbInventari.clsDetajimArtikulli det = new DbInventari.clsDetajimArtikulli(trup.KodDetajimi1, llojDetajim, "", idPerdoruesi, tmpArt.IdKategoriDetajimi, idNder, "", 1, 0);
                            mesazh = det.ruajShpejte(tmpArt, 1, dbinventari);
                            trup.IdDetajimi = det.IdDetajimArtikulli;
                            if (!mesazh.Status)
                                return mesazh;
                        }
                        else
                            trup.IdDetajimi = clsDetajimArtikulli.ktheIdDetajimi(trup.KodDetajimi1, idNder, dbinventari);
                    }
                    else if (trup.IdDetajimi > 0 && !blerjengadealer) //nqs o.IdDetajimi=-1 ath nuk do krijohet as nuk do lidhet ndonje detajim
                    {
                        bool lidhurMeArt = DbInventari.clsDetajimArtikulli.ekzistonDetajimLidhurMeArtikullinSipasId(trup.IdDetajimi, idNder, tmpArt.KodArtikulli, 1, dbinventari);
                        if (!lidhurMeArt)
                        {
                            mesazh = clsDetajimPerArt.ruajLidhje(tmpArt, trup.IdDetajimi, 1, idNder, idPer, dbinventari);
                            if (!mesazh.Status)
                                return mesazh;
                        }
                    }

                    if (trup.IdDetajimi2 <= 0 && !String.IsNullOrEmpty(trup.KodDetajimi2))
                    {
                        if (!DbInventari.clsDetajimArtikulli.ekziston(trup.KodDetajimi2, idNder, dbinventari))
                        {
                            int llojDetajim = 0;
                            if (tmpArt.IdKategoriDetajimi2 == 3)
                                llojDetajim = 3;
                            else if (tmpArt.IdKategoriDetajimi2 == 4)
                                llojDetajim = 1;

                            DbInventari.clsDetajimArtikulli det2 = new DbInventari.clsDetajimArtikulli(trup.KodDetajimi2, llojDetajim, "", idPerdoruesi, tmpArt.IdKategoriDetajimi2, idNder, "", 1, 0);
                            mesazh = det2.ruajShpejte(tmpArt, 2, dbinventari);
                            trup.IdDetajimi2 = det2.IdDetajimArtikulli;
                            if (!mesazh.Status)
                                return mesazh;
                        }
                        else
                            trup.IdDetajimi2 = clsDetajimArtikulli.ktheIdDetajimi(trup.KodDetajimi2, idNder, dbinventari);
                    }
                    else if (trup.IdDetajimi2 > 0) //nqs o.IdDetajimi2=-1 ath nuk do krijohet as nuk do lidhet ndonje detajim
                    {
                        bool lidhurMeArt = DbInventari.clsDetajimArtikulli.ekzistonDetajimLidhurMeArtikullinSipasId(trup.IdDetajimi2, idNder, tmpArt.KodArtikulli, 2, dbinventari);
                        if (!lidhurMeArt)
                        {
                            mesazh = clsDetajimPerArt.ruajLidhje(tmpArt, trup.IdDetajimi2, 2, idNder, idPer, dbinventari);
                            if (!mesazh.Status)
                                return mesazh;
                        }
                    }
                }
                if (kontrolloIMEIFifo && trup.IdDetajimi > 0)
                {
                    string mag = clsNjesiAdministrative.ktheKodiNjesiAdministrativeSipasiD(trup.IdMag, idPer, dbRegj);


                    mesazh = clsDetajimArtikulli.KontrolloDetajimFundit(trup.KodDetajimi1, dtDk, mag, trup.KodiArtikull, idNder, new String[0], promocione, new clsDatabaseInventari(dbRegj));
                    if (mesazh.KodMesazhi == 0)
                    {
                        return new clsMesazh(false, "Kujdes: ky IMEI " + trup.KodDetajimi1 + " nuk mund te shitet.Ju keni IMEI te tjere me te vjeter per kete aparat.Vendosni nje IMEI tjeter");
                    }
                    if (mesazh.KodMesazhi == -1 || !mesazh.Status)
                        return mesazh;
                }
                iii++;
            }
            myWatchRuajTrupinOcolTrupiMagazina.Stop();
            if (myWatchRuajTrupinOcolTrupiMagazina.Elapsed > new TimeSpan(0, 0, 1))
                System.Diagnostics.Trace.WriteLine("myWatchRuajTrupinOcolTrupiMagazina: " + myWatchRuajTrupinOcolTrupiMagazina.Elapsed);

            System.Diagnostics.Stopwatch myWatchRuajTrupinSpeedy = System.Diagnostics.Stopwatch.StartNew();
            DataTable dt = ocolTrupiMagazina.ToDataTable("IdTrupiMagazina", "IdKokaMagazina", "IdLlojVeprimi", "IdArtikulli", "IdNjesia", "Sasia", "Vlefta", "Koeficenti", "Shenja", "SasiProgresive", "VleftaProgresive", "IdMag", "Data", "IdStatusDok", "IdRenditjes", "IdDetajimi", "SasiProgresiveDetajimi", "VlefteProgresiveDetajimi", "IdDetajimi2", "IdTrupiRezervimi", "IdTrupiKonvertimFSH", "IdTrupiKonvertimUSH", "IdTrupiKonvertimUD", "IdKthimi", "IdTrupiShitjeGjenerimi", "Shenime", "IdArtikullSet", "IdBarkodi");

            DataTable idTrupis = dbRegj.ruajTrupiMagazina(isDalje, dt, newIdRenditje);

            myWatchRuajTrupinSpeedy.Stop();
            if (myWatchRuajTrupinSpeedy.Elapsed > new TimeSpan(0, 0, 1))
                System.Diagnostics.Trace.WriteLine("myWatchRuajTrupinSpeedy: " + myWatchRuajTrupinSpeedy.Elapsed);
            
            if (IdStatusDok != 8)
            {
                mesazh = RuajSerialeUnike(idTrupis, idllojdokmag, dbRegj, dtDk, ref mesazhmevonshem, false);  
            }

            if (!mesazh)
                return mesazh;
            if (eshteTrasferim)
            {
                //ne rastin e transferimit, hyrja e serialit duhet te behet pasi te kryhet dalja, perndryshe do te gjeje serialet gjendje dhe nuk do te kryhet ruajtja
                mesazh = this.OMagazinaTransferim.RuajSerialeUnike(idTrupaTransferim, 1, dbRegj, dtDk, ref mesazhmevonshem, false);
                if (!mesazh)
                    return mesazh;
            }
            #endregion Ruajtrupin

            myWatchRuajTrupin.Stop();
            if (myWatchRuajTrupin.Elapsed > new TimeSpan(0, 0, 1))
                System.Diagnostics.Trace.WriteLine("myWatchRuajTrupin: " + myWatchRuajTrupin.Elapsed);
            if (kokarezervime.NrDok != null && kokarezervime.NrDok != "")
            {
                kokarezervime.IdGjenerues = idMagKoka;
                mesazh = kokarezervime.ruaj(null, dbRegj);
                if (!mesazh.Status)
                    return mesazh;
            }

            int idkonfiginv = 0;
            if (idinv.Length > 0)
            {
                clsKokaInventarizim kokainv = new clsKokaInventarizim();
                kokainv.mbushKokaInventarizimSipasID(idinv[0], dbRegj);
                idkonfiginv = kokainv.IdKonfigAmbjente;
            }
            foreach (int idd in idinv)
            {
                mesazh = dbRegj.ruajLidhjeMagInv(0, idMagKoka, idd, idKonf, idkonfiginv);
                if (!mesazh.Status)
                    return mesazh;
            }
            System.Diagnostics.Stopwatch myWatchAmortizimi = System.Diagnostics.Stopwatch.StartNew();

            #region amortizimi

            if (serialemag.Count > 0 && (kokashitje.IdShitjeKoka <= 0 || (kokashitje.IdShitjeKoka > 0 && isshitje)))//kur ka seriale dhe vjen nga magazina ose nga shitja jo nga bleraja
            {
                foreach (DbCore.DbAsete.clsSerialetMagazine s in serialemag)
                {
                    s.IdDok = idMagKoka;
                    s.IdStatusDokumenti = idSt;
                    s.IdKonfigAmbjenti = idKonf;
                    s.IdNjesiAdministrative = ocolTrupiMagazina[s.NrRendor].IdMag;
                    
                    mesazh = s.ruaj();
                    clsAQTSeriale serial = new clsAQTSeriale();
                    serial.merrAQTSerialSipasID(s.IdAQTSeriali, dbasete);
                    if (!mesazh.Status)
                    {
                        if (!eshteTrasferim && idLlojDokumentiMagazine == 1 && serial.IdHistorikAktualPaSerial <= 0 && serial.IdNjesiAdministrativeAktuale != 0)
                        {
                            return new clsMesazh(false, string.Format(MessagesResource.Messages["msgSerialPerdorurNeVeprime"], serial.AqtSerialKod));
                        }
                        return mesazh;
                    }
                    if (konfamortizimihyrje.IdKonfigAmbjente == 0 && idLlojDokumentiMagazine == 2 && serial.IdHistorikAktualPaSerial <= 0)// ne rastet e daljes shpiem magazinen 0
                    {
                        mesazh = dbasete.modifikoAQTSerialNgaVeprimi(s.IdAQTSeriali, serial.AqtSerialDataHyrje, serial.AqtSerialDataMagAktive, serial.AqtSerialDataAmortizimfillestar, 0, serial.IdHistorikAktualPaSerial, idPer, serial.IdStatusDokumenti, false);
                        if (!mesazh.Status)
                            return mesazh;
                    }
                    //if (!eshteTrasferim && idLlojDokumentiMagazine == 1 && serial.IdHistorikAktualPaSerial <= 0 && serial.IdNjesiAdministrativeAktuale != 0)
                    //{
                    //    return new clsMesazh(false, string.Format(MessagesResource.Messages["msgSerialPerdorurNeVeprime"], serial.AqtSerialKod));
                    //}
                    if (serial.IdHistorikAktualPaSerial > 0)//nqs kemi seriale te ndashem kalojme id e dokumentit te magazines dhe sasine dhe cmimin e ketij seriali
                    {
                        if (idLlojDokumentiMagazine == 2)
                        {
                            clsHistorikAQTSeriale histroikpara = new clsHistorikAQTSeriale();//nesnes
                            histroikpara.merrHistorikAQTSerialSipasID(serial.IdHistorikAktualPaSerial, idNder, dbasete);
                            mesazh = dbasete.modifikoHistorikAQTSerial(serial.IdHistorikAktualPaSerial, koka.idPerdoruesi, histroikpara.IdDok, histroikpara.SasiaProgresive - s.Sasia, s.Cmimi, histroikpara.VleftaProgresive - s.Vlefta, 1);
                        }
                        else
                            mesazh = dbasete.modifikoHistorikAQTSerial(serial.IdHistorikAktualPaSerial, koka.idPerdoruesi, idMagKoka, s.Sasia, s.Cmimi, s.Vlefta, 1);
                        if (!mesazh.Status)
                        {
                            return mesazh;
                        }
                    }
                    if (idLlojDokumentiMagazine == 1 && !ngarivleresimi)
                    {
                        clsNjesiAdministrative njesiadm = new clsNjesiAdministrative(s.IdNjesiAdministrative, dbRegj);
                        clsStatusMagazine_Asete status = new clsStatusMagazine_Asete(njesiadm.IdStatusAktualMagazine, dbasete);

                        DateTime aqtSerialDataMagAktive = DateTime.MinValue;
                        if (status.Emertimi == "Aktive")
                            aqtSerialDataMagAktive = koka.dtDok;
                        mesazh = dbasete.modifikoAQTSerialNgaVeprimi(s.IdAQTSeriali, koka.dtDok, aqtSerialDataMagAktive, koka.dtDok, s.IdNjesiAdministrative, serial.IdHistorikAktualPaSerial, koka.idPerdoruesi, koka.idStatusDok, kaveprimepas);
                        if (!mesazh.Status)
                        {
                            return mesazh;
                        }
                    }
                }
                string shfaqmesazhapolupeamortizim = "jo";
                if (kokashitje.IdShitjeKoka > 0 && isshitje)
                {
                    mesazh = col.krijoAmortizimeKokaShitjeMagazine(serialemag, colHistorik, kokashitje, konfamortizimi, colAmortizimetEVjetra, mosLlogaritAmortizimShtese, idMagKoka, out mesazhmevonshem);
                    if (!mesazh.Status)
                    {
                        return mesazh;
                    }
                    string tmpmesazh = "";
                    mesazh = col.ruajListAmortizime(kokashitje.IdShitjeKoka, kontabilizoamortizim, out shfaqmesazhapolupeamortizim, colAmortizimetEVjetra, idPeriudha, 86, new colSerialetMagazine(), false, null, modifikim, out tmpmesazh);
                    if (tmpmesazh != "")
                        mesazhmevonshem = tmpmesazh;
                    if (!mesazh.Status)
                    {
                        return mesazh;
                    }
                }
                else
                {
                    if (konfamortizimi.IdKonfigAmbjente > 0)
                    {
                        if (konfamortizimihyrje.IdKonfigAmbjente > 0)   //rasti transferim
                            mesazh = col.krijoAmortizimeKokaTransferimMagazine(serialemag, colHistorik, koka, konfamortizimi, konfamortizimihyrje, colAmortizimetEVjetra, colAmortizimetEVjetraHyrje, mosLlogaritAmortizimShtese, idMagKoka, out mesazhmevonshem);
                        else
                            if (idLlojDokumentiMagazine == 2)
                            mesazh = col.krijoAmortizimeKokaDaljaMagazine(serialemag, colHistorik, koka, konfamortizimi, colAmortizimetEVjetra, mosLlogaritAmortizimShtese, idMagKoka, out mesazhmevonshem);
                        else
                        {
                            clsKokaShitje dokblereje = new clsKokaShitje(0, koka.IdNivel, 0, koka.idKonfigAmbjente, koka.idKlientFurnitor, koka.IdProjekt, koka.nrProjekt, koka.dtDok, koka.NrDok, "", koka.dtDok, 0, 0, 0, koka.dtDok, 0, 0, 0, 0, 0, koka.vlefta, 0, koka.dtRegjistrimi, koka.IdStatusDok, koka.IdNdermarrje, koka.idNdermarrjeVit, koka.idNivelGjenerues, koka.IdKonfigGjenerues, koka.idGjenerues, koka.IdDokNga, "", "", koka.pershkrimi, false, koka.IdDegeAdministrative, 0, koka.idPerdoruesi, koka.IdRaportDesing, koka.IdGrup1, koka.idGrup2, koka.idGrup3, koka.dtDok, 0, StatusAprovimi.Undefined, koka.idKrijuesi, 0, 0, 0, false, false, koka.dtDok, koka.dtDok, 0, 0, 0, 0, 0, 0, "", 0, false, false, 0, koka.dtDok, false, 0, 0, new colKlienteFurnitore(), true, 0, DateTime.Now, String.Empty, String.Empty, "", 0, "", false, 0, 0, "", false , "" , DateTime.Now, koka.NrDok);
                            dokblereje.OKokaMagazina = koka;
                            mesazh = new clsMesazh();
                            mesazh.Status = col.krijoAmortizimeKokaNgaBlerja(serialemag, dokblereje, konfamortizimi, colAmortizimetEVjetra, dbasete);
                        }
                        if (!mesazh.Status)
                        {
                            return mesazh;
                        }
                        string tmpmesazh = "";
                        mesazh = col.ruajListAmortizime(idMagKoka, kontabilizoamortizim, out shfaqmesazhapolupeamortizim, colAmortizimetEVjetra, idPeriudha, 86, new colSerialetMagazine(), false, null, modifikim, out tmpmesazh);
                        if (tmpmesazh != "")
                            mesazhmevonshem = tmpmesazh;
                        if (!mesazh.Status)
                        {
                            return mesazh;
                        }
                    }
                }

                //foreach (DbAsete.clsHistorikAQTSeriale h in colHistorik)//nesnes
                //{
                //    clsAQTSeriale serialprindi = new clsAQTSeriale();
                //    serialprindi.merrAQTSerialSipasID(h.IdPrindi, dbasete);
                //    clsHistorikAQTSeriale historikprindi = new clsHistorikAQTSeriale();
                //    historikprindi.merrHistorikAQTSerialSipasID(serialprindi.IdHistorikAktualPaSerial, idNder, dbasete);
                //    mesazh = dbasete.modifikoHistorikAQTSerial(serialprindi.IdHistorikAktualPaSerial, idPer, historikprindi.IdDok, historikprindi.SasiaProgresive - h.SasiaProgresive, h.CmimiProgresive, h.CmimiProgresive * (historikprindi.SasiaProgresive - h.SasiaProgresive), historikprindi.IdStatusDokumenti);//zbresim sasine e serialit te ri nga prindi
                //    if (!mesazh.Status)
                //        return mesazh;
                //}
            }

            #endregion amortizimi

            myWatchAmortizimi.Stop();
            if (myWatchAmortizimi.Elapsed > new TimeSpan(0, 0, 1))
                System.Diagnostics.Trace.WriteLine("myWatchAmortizimi: " + myWatchAmortizimi.Elapsed);
  
            mesazh = GjeneroKontabilitet(idMagKoka, idNiv, idKonf, dtDk, nrDk, vl, idSt, idNder, idNdVt, idPer, dtRegj, iddegeadministrative, ocolTrupiMagazina, oFleteKontabel, oMagazinaTransferim, meKontabilizim, idPeriudha, dbRegj, pershkrimDokKontabiliteti, idLlojDok, idDokNgaFK, idllogari, idnjesivartese, mekonfirmim, ref shfaqmesazhapolupe, iddokngaQKFK, trupivjeterqendra, gjithmone, col, kontMesazh, idMag);
            if (!mesazh.Status)
                return mesazh;
            mesazh = new clsMesazh(true, "Ruajtja përfundoi me sukses!");
            return mesazh;
        }

        private void RregulloTrupinEDokTransferimit(colSerialeUnikeKategori kategorite, bool serialeNeDetajim, bool bashkoArtikujt, int idMagDestinacion)
        {
            if (bashkoArtikujt)
            {
                this.OcolTrupiMagazina.BashkoTrupin(kategorite, this.OMagazinaTransferim.OcolTrupiMagazina);
                this.OMagazinaTransferim.OcolTrupiMagazina.ForEach(cls =>
                {
                    cls.Shenja = -(cls.Shenja);
                    cls.IdTrupiKonvertimUSH = 0;
                    cls.IdTrupiKonvertimFSH = 0;
                    cls.IdTrupiMagazina = 0;
                    cls.IdMag = idMagDestinacion;
                });
                return;
            }

            int s = 0;

            foreach (clsTrupiMagazina o in ocolTrupiMagazina)
            {

                var kat = kategorite == null ? null : kategorite.MerrKategoriSipasIdFormatit(((clsArtikulli)o.Element).IdFormatSeriali);
                if (serialeNeDetajim && kat != null && kat.Kategori.EqualsIgnoreCase(enumKategoriSerialesh.APARATE.ToString()) && ((clsArtikulli)o.Element).DetajimArtikulli)
                {
                    for (int i = 0; i < o.Sasia; i++)
                    {
                        oMagazinaTransferim.OcolTrupiMagazina[s].Cmimi = o.Cmimi;
                        oMagazinaTransferim.OcolTrupiMagazina[s].Sasia = 1;
                        oMagazinaTransferim.OcolTrupiMagazina[s].Vlefta = o.Cmimi * o.Koeficenti;
                        oMagazinaTransferim.ocolTrupiMagazina[s].Shenja = -(o.Shenja);
                        oMagazinaTransferim.ocolTrupiMagazina[s].IdTrupiKonvertimUD = 0;//zerohen keto vlera sepse del e konvertuar dy here dhe sikur vijne nga i njejti rresht dhe hyrja dhe dalja dhe jep probleme ne konvertim
                        oMagazinaTransferim.ocolTrupiMagazina[s].IdTrupiKonvertimUSH = 0;
                        oMagazinaTransferim.ocolTrupiMagazina[s].IdTrupiKonvertimFSH = 0;
                        oMagazinaTransferim.ocolTrupiMagazina[s].IdTrupiMagazina = 0;
                        s++;
                    }
                }
                else
                {
                    oMagazinaTransferim.OcolTrupiMagazina[s].Cmimi = o.Cmimi;
                    oMagazinaTransferim.OcolTrupiMagazina[s].Vlefta = o.Vlefta;
                    oMagazinaTransferim.OcolTrupiMagazina[s].Sasia = o.Sasia;
                    oMagazinaTransferim.ocolTrupiMagazina[s].Shenja = -(o.Shenja);
                    oMagazinaTransferim.ocolTrupiMagazina[s].IdTrupiKonvertimUD = 0;//zerohen keto vlera sepse del e konvertuar dy here dhe sikur vijne nga i njejti rresht dhe hyrja dhe dalja dhe jep probleme ne konvertim
                    oMagazinaTransferim.ocolTrupiMagazina[s].IdTrupiKonvertimUSH = 0;
                    oMagazinaTransferim.ocolTrupiMagazina[s].IdTrupiKonvertimFSH = 0;
                    oMagazinaTransferim.ocolTrupiMagazina[s].IdTrupiMagazina = 0;
                    s++;
                }

            }
        }

        private clsMesazh RuajSerialeUnike(DataTable idTrupis, int idllojdokmag, clsDatabaseRegjistrim dbRegj, DateTime dtDk, ref string mesazhmevonshem, bool vetemTeRinj)
        {
            colSerialeUnikeKategori serialeKategori = new colSerialeUnikeKategori(idNdermarrje);
            foreach (DataRow row in idTrupis.Rows)
            {
                var idKoka = Convert.ToInt32(row["IDKOKAMAGAZINA"]);
                var idTrupiMag = Convert.ToInt32(row["IDTRUPIMAGAZINA"]);
                var idArtikulli = Convert.ToInt32(row["idartikull"]);
                var idMag = Convert.ToInt32(row["idmag"]);
                int.TryParse(row["IDARTIKULLSET"].ToString(), out int idSeti);

                var clsTrupiMag = ocolTrupiMagazina.Find(colt => colt.IdArtikulli == idArtikulli && colt.IdArtikullSet == idSeti && colt.IdMag == idMag);
                if (clsTrupiMag.OColSerialeUnikeMagazina == null || clsTrupiMag.OColSerialeUnikeMagazina.Count == 0)
                    continue;

                if (vetemTeRinj)
                {
                    clsTrupiMag.OColSerialeUnikeMagazina.FindAllAndRemove(x => x.Id != 0);
                    if (clsTrupiMag.OColSerialeUnikeMagazina.Count == 0)
                        continue;
                }

                foreach (clsSerialeUnikeMagazina clsSerialUnik in clsTrupiMag.OColSerialeUnikeMagazina)
                {
                    clsSerialUnik.IdKokaMagazine = idKoka;
                    clsSerialUnik.IdTrupiMagazine = idTrupiMag;
                    clsSerialUnik.IdLlojDokumentMagazine = idllojdokmag;
                    clsSerialeUnikeKategori kategoriSeriali = serialeKategori.merrKategoriSipasSerialitUnik(clsSerialUnik.SerialiKryesore, out int idFormati);
                    if (!String.IsNullOrEmpty(clsTrupiMag.KodDetajimi1) && kategoriSeriali.Kategori.ToString() == "KARTA")
                    {

                        ((clsSerialeUnikeKarta)clsSerialUnik).Airtime = clsTrupiMag.KodDetajimi1;
                    }
                }
            }
            System.Diagnostics.Stopwatch myWatchSerDt = System.Diagnostics.Stopwatch.StartNew();
            var SerialetPerInsert = new colSerialeUnikeMagazina(OcolTrupiMagazina.Where(l => l.OColSerialeUnikeMagazina != null).SelectMany(x => x.OColSerialeUnikeMagazina)).KtheNeDataTable();
            myWatchSerDt.Stop();
            if (myWatchSerDt.Elapsed > new TimeSpan(0, 0, 1))
                System.Diagnostics.Trace.WriteLine("myWatchSerDt: " + myWatchSerDt.Elapsed);
            if (SerialetPerInsert.Rows.Count == 0)
                return new MesazhSuksesi();

            System.Diagnostics.Stopwatch myWatchInsBulkSeriale = System.Diagnostics.Stopwatch.StartNew();
            dbRegj.RuajTrupiSerialeUnikeMagazina(SerialetPerInsert);
            myWatchInsBulkSeriale.Stop();
            if (myWatchInsBulkSeriale.Elapsed > new TimeSpan(0, 0, 1))
                System.Diagnostics.Trace.WriteLine("myWatchInsBulkSeriale: " + myWatchInsBulkSeriale.Elapsed);

            return new MesazhSuksesi();
        }

        public static double merrCmimReferues(DateTime dtDk, int idNder, int idPer, int idartikulli, int idnjesia, clsDatabaseInventari dbInv)
        {
            double cmimiartnjejte;
            clsNivelCmimi niv = new clsNivelCmimi();
            niv.mbushNivelCmimiBaze(idNder, 1, dbInv);
            if (niv.IdNivelCmimi <= 0)
                cmimiartnjejte = 0;
            clsArtikulli art = new clsArtikulli(idartikulli, dbInv);
            clsNjesiArtikulli njes = new DbInventari.clsNjesiArtikulli(idnjesia, dbInv);
            cmimiartnjejte = double.Parse(clsFunksione.merrCmimSipasNivelit(niv.IdNivelCmimi, art.KodArtikulli, idPer, njes.KodNjesia, "LEK", dtDk.ToShortDateString(), 1, 1, idNder, 1, dbInv, 0)[0].ToString());
            return cmimiartnjejte;
        }

        public static double merrCmimReferues(DateTime dtDk, int idNder, int idPer, int idartikulli, int idnjesia)
        {
            using (clsDatabaseInventari dbInv = new clsDatabaseInventari())
            {
                return merrCmimReferues(dtDk, idNder, idPer, idartikulli, idnjesia, dbInv);
            }
        }

        private clsKokaRezervime venndosIdNgaVjenRezervim(clsKokaRezervime kokarezervime, DataTable idTrupis)
        {
            for (int i = 0, nrTrupi = kokarezervime.OcolTrupiRezervime.Count; i < nrTrupi; i++)
            {
                kokarezervime.OcolTrupiRezervime[i].IdTrupiNgaVjen = (int)idTrupis.Select("IDTRUPIREZERVIMI = " + kokarezervime.OcolTrupiRezervime[i].IdTrupiRezervime)[0]["IDTRUPIMAGAZINA"];
            }
            return kokarezervime;
        }

        /// <summary>
        /// Ruan objektin e  kokes se dokumentit te magazines ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsKokaMagazine.ruajMagazina"/>
        /// </summary>
        /// <param name="eshteTransferim">tregon nese dokumenti i regjistruar eshte transferim apo jo</param>
        /// <param name="meKontabilizim">tregon nese te konfigurimet magazina eshte me kontabilizim apo jo</param>
        /// <param name="hfNrAutoregjistrime">hiddenField i Devit qe mban numrat automatike,null nese nuk perodren</param>
        /// <param name="idPeriudha"></param>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
        public clsMesazh ruaj(bool eshteTransferim, int meKontabilizim, IDictionary<string, object> hfNrAutoregjistrime, int idPeriudha, string pershkrimFK, out string shfaqmesazhapolupe, bool eshteOwn, colSerialetMagazine serialemag, colSerialetMagazine serialetranf, clsKonfigurimAmbjenti konfamortizimi, clsKonfigurimAmbjenti konfamortizimihyrje, bool gjithmone, bool vjenNgaImportSQL, string idDokImporti, string ndermarrjeKey, string emerTabKoka, string primaryKey, bool ruajrenditje, bool modifikim, int[] idinv, bool kontrolloIMEIFifo, bool blerjengadealer, bool promocione, out string mesazhmevonshem, int idShitjeTransferimi, ref DbData dbData, bool ngaImporti, colSerialeUnikeKategori kategorite, bool serialeNeDetajim,bool bashkoArtikujt)
        {
            mesazhmevonshem = "";
            clsKonfigurimAmbjenti konf = new clsKonfigurimAmbjenti();
            konf.mbushKonfiguriminMeID(this.IdKonfigAmbjente);

            int idLlojDokFK;
            if (konf != null)
                idLlojDokFK = konf.IdKategori;
            else
                idLlojDokFK = -1;
            int idDokNgaFK = -1;
            int iddokngafkam = -1;
            clsMesazh u_ruajt = new clsMesazh();
            shfaqmesazhapolupe = "";
            try
            {

                using (var scope = new MyTransactionScope(dbData))
                {
                    clsDatabaseRegjistrim db = new clsDatabaseRegjistrim(dbData);

                    u_ruajt = ruaj(eshteTransferim, meKontabilizim, hfNrAutoregjistrime, idPeriudha, pershkrimFK, idLlojDokFK, idDokNgaFK, db, out shfaqmesazhapolupe, 0, new DbQendraKosto.colTrupiQendraKosto(), eshteOwn, serialemag, serialetranf, konfamortizimi, konfamortizimihyrje, iddokngafkam, 0, new DbQendraKosto.colTrupiQendraKosto(), false, new clsKokaShitje(), false, gjithmone, new colAmortizimiKoka(), meKontabilizim, ruajrenditje, modifikim, new colTrupiMagazina(), new colAmortizimiKoka(), idinv, kontrolloIMEIFifo, blerjengadealer, promocione, out mesazhmevonshem, kategorite, serialeNeDetajim, bashkoArtikujt); //perdor ruajtjen me transaksion
                    if (!u_ruajt.Status)
                    {
                        return u_ruajt;
                    }
                    if (HfArkiva != null)
                        u_ruajt = colArkiva.RuajArkiven(idKokaMagazina, idKategoria, IdPerdoruesi, idNdermarrje, HfArkiva);
                    if (!u_ruajt.Status)
                    {
                        return u_ruajt;
                    }
                    if (vjenNgaImportSQL && idDokImporti != string.Empty)
                    {
                        string[] idte = idDokImporti.Split(';');
                        for (var i = 0; i < idte.Length; i++)
                        {
                            if (idte[i] == string.Empty)
                                continue;
                            int statusi = 1;

                            clsMesazh mesazh = db.updateDokTabeleTemportal(idte[i], idNdermarrje, statusi, emerTabKoka, primaryKey, ndermarrjeKey);

                            if (!mesazh.Status)
                            {
                                return mesazh;
                            }
                        }
                    }
                    if (idShitjeTransferimi != 0)
                    {
                        var mesazh = clsKokaShitje.ndryshoStatusTransferimi(idShitjeTransferimi, StatusTrasferimi.Transferuar);
                        if (!mesazh)
                            return mesazh;
                    }
                    scope.Complete();
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                ImbLogger.Error(ex);
                if (ngaImporti && (ex.Number == 1205 || ex.Number == 121 || ex.Number == 1236))//nga importi
                    throw ex;
                u_ruajt.PershkrimMesazhi = ex.Message;
                u_ruajt.Status = false;
                return u_ruajt;
            }
            catch (Exception err)
            {
                return new clsMesazh(err.Message);
            }
            return u_ruajt;
        }


        /// <summary>
        /// Ruan objektin e  kokes se dokumentit te magazines ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsKokaMagazine.ruajMagazina"/>
        /// </summary>
        /// <param name="eshteTransferim">tregon nese dokumenti i regjistruar eshte transferim apo jo</param>
        /// <param name="meKontabilizim">tregon nese te konfigurimet magazina eshte me kontabilizim apo jo</param>
        /// <param name="hfNrAutoregjistrime">hiddenField i Devit qe mban numrat automatike,null nese nuk perodren</param>
        /// <param name="idPeriudha"></param>
        /// <param name="kontabilizioamortizim"> perdoret per rastet kur kemi kontabilizim magazine brenda shitjes dhe nuk kontabilizohet magazina por duhet te kontabilizohet amortizimi</param>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
        public clsMesazh ruaj(bool eshteTransferim, int meKontabilizim, IDictionary<string, object> hfNrAutoregjistrime, int idPeriudha, string pershkrimFK, int idLlojDokFK, int idDokNgaFK, clsDatabaseRegjistrim db, out string shfaqmesazhapolupe, int iddokngaqk, DbQendraKosto.colTrupiQendraKosto trupivjeterqendra, bool eshteOwn, colSerialetMagazine serialemag, colSerialetMagazine serialetransf, clsKonfigurimAmbjenti konfamortizimi, clsKonfigurimAmbjenti konfamortizimihyrje, int idDokNgaFKAM, int iddokngaqkAM, DbQendraKosto.colTrupiQendraKosto trupivjeterqendraAM, bool isshitje, clsKokaShitje kokashitje, bool ngarivleresimi, bool gjithmone, colAmortizimiKoka col, int kontabilizioamortizim, bool ruajrenditje, bool modifikim, colTrupiMagazina trupivjetermagKryesor, colAmortizimiKoka colAmortizimeVjetra, int[] idinv, bool kontrolloIMEIFifo, bool blerjengadealer, bool promocione, out string mesazhmevonshem, colSerialeUnikeKategori kategorite, bool serialeNeDetajim, bool bashkoArtikujt)
        {
            mesazhmevonshem = "";
            clsDatabaseShare dbshare = new clsDatabaseShare(db);
            bool kontrollodisponibel = (clsAlternativaKushti.getAlternativa(this.IdKonfigAmbjente, "KD", dbshare) == "Po");
            bool mosLlogaritAmortizimShtese = (clsAlternativaKushti.getAlternativa(this.IdKonfigAmbjente, "MLLASH", dbshare) == "Po");

            clsMesazh mesazhKontrolli = kontrolloMagazine(out bool kaNdryshimNumri, db, hfNrAutoregjistrime); //TODO KEVI duhet hequr kontrolli qe ekziston doku apo jo per dokumentat e gjeneruar nga shitja
            if (!mesazhKontrolli.Status)
            {
                shfaqmesazhapolupe = "jo";
                return mesazhKontrolli;
            }

            clsMesazh u_ruajt = this.ruajMagazina(out int idkoka, IdNivel, IdKonfigAmbjente, IdKlientFurnitor, IdMagazina, DtDok, NrDok, IdProjekt, NrProjekt, IdKategoria, IdDokNga, Vlefta, IdStatusDok, IdNdermarrje,
                IdNdermarrjeVit, IdPerdoruesi, DtRegjistrimi, IdLlojDokumentiMagazine, Shenime, IdRenditjes, IdNivelGjenerues, IdKonfigGjenerues, IdGjenerues, IdDegeAdministrative, OcolTrupiMagazina, OFleteKontabel,
                OMagazinaTransferim, eshteTransferim, modifikim, meKontabilizim, 0, idPeriudha, db, pershkrimFK, idLlojDokFK, idDokNgaFK, idLlogari, IdNjesiVartese, MeKonfirmim, IdGrup1, IdGrup2, IdGrup3, Pershkrimi,
                Magazinieri, Adresa, out shfaqmesazhapolupe, iddokngaqk, trupivjeterqendra, oKokaRezervime, kontrollodisponibel, eshteOwn, idAutomjet, idRaportDesing, serialemag, serialetransf, konfamortizimi,
                konfamortizimihyrje, this, colAmortizimeVjetra, new colAmortizimiKoka(), idDokNgaFKAM, iddokngaqkAM, trupivjeterqendraAM, isshitje, kokashitje, ngarivleresimi, gjithmone, col, kontabilizioamortizim,
                mosLlogaritAmortizimShtese, IdKrijuesi, dtTransporti, Shoferi, TargaShoferi, trupivjetermagKryesor, ruajrenditje, idinv, false, kontrolloIMEIFifo, blerjengadealer, promocione, idKategoriSeriali, 
                nrSerial, out mesazhmevonshem, kategorite, serialeNeDetajim, bashkoArtikujt, NIVFSH, WTNIC, idOperator,mallraTeDjegshme,shoqerimIKerkuar,transportuesi,tipi,transaksioni);

            idKokaMagazina = idkoka;

            if (!u_ruajt.Status)
            {
                return u_ruajt;
            }

            if (kaNdryshimNumri)
                return mesazhKontrolli;
            return u_ruajt;
        }

        private clsMesazh kontrolloMagazine(out bool kaNdryshimNumri, clsDatabaseRegjistrim dbRegj, IDictionary<string, object> hfNrAutoregjistrime)
        {
            kaNdryshimNumri = false;
            if (this.ocolTrupiMagazina.Count == 0)
                return new clsMesazh("Trupi magazines nuk mund te jete bosh!");
            foreach (clsTrupiMagazina t in this.ocolTrupiMagazina)
            {
                if (t.KodiArtikull != "" && t.PershkrimArtikull == "")
                {
                    return new clsMesazh("Te dhënat nuk janë të sakta");
                }
                if (t.KodiArtikull != "" && t.IdLlojVeprimi == 1 && t.IdNjesia == 0)
                {
                    return new clsMesazh("Te dhënat nuk janë të sakta");
                }
                if (t.KodiArtikull != "" && t.IdMag == 0)
                {
                    return new clsMesazh("Të dhënat nuk janë të sakta");
                }
            }

            clsMesazh mes = new clsMesazh();

            if (hfNrAutoregjistrime != null)
            {
                mes = kontrolloNrAutoMagazine(out kaNdryshimNumri, dbRegj, hfNrAutoregjistrime);
                if (!mes.Status)
                    return mes;
            }
            //  if (dbRegj.ekzistonRegjistrimMagazine(idKonfigAmbjente, nrDok, idMagazina, dtDok, idNdermarrje))
            if (dbRegj.ekzistonRegjistrimMagazineSipasIdentifikuese(IdKokaMagazina, idKonfigAmbjente, nrDok, idMagazina, dtDok, idNdermarrje, idNivel, idKlientFurnitor, idDegeAdministrative, idRaportDesing, idGrup1, idGrup2, idGrup3))
                return new clsMesazh(false, "Ekziston një regjistrim magazine me këto të dhëna identifikuese!");
            if (kaNdryshimNumri)
                return mes;
            return new clsMesazh(true, "Kontrolli i magazinës u krye me sukses!");
        }

        private clsMesazh kontrolloNrAutoMagazine(out bool kaNdryshimNumri, clsDatabaseRegjistrim dbRegj, IDictionary<string, object> hfregjistrime)
        {
            clsDatabaseAdmin db = new clsDatabaseAdmin(dbRegj);
            List<NrAuto> list = clsNrAutom.kontrollogjithenumrat(db, hfregjistrime, this.dtDok);
            if (NrAuto.ktheVlerenEre(list, "NrDok") != "")
                this.nrDok = NrAuto.ktheVlerenEre(list, "NrDok");
            if (NrAuto.ktheVlerenEre(list, "NrProjekt") != "")
                this.NrProjekt = NrAuto.ktheVlerenEre(list, "NrProjekt");
            if (NrAuto.ktheVlerenEre(list, "NrSerial") != "")
                this.nrSerial = NrAuto.ktheVlerenEre(list, "NrSerial");
            clsMesazh mes = NrAuto.ruajvlera(out kaNdryshimNumri, list, this.dtDok, this.idPerdoruesi, this.idNdermarrje, db);
            return new clsMesazh(mes.Status, mes.PershkrimMesazhi);
        }

        /// <summary>
        /// Modifikon nje objekt dokument magazine sebashku me te trupin dhe kontabilitetin
        /// Nje objekt dokument magazine ka nje koleksion me trupin dhe kontabilitetin perkates ,
        /// modifikimi e nje dokumenti imponon modifikimin edhe te nje colection-i me trupin dhe kontabilitetin
        /// Mqs cdo rresht i ri qe modifikon ne DB kerkon thirrjen e nje SP-je me parametra dhe dokumenti i magazines bashke me trupin dhe kontabilitetin konsiderohet si nje regjistrim,
        /// perdoret nje transaksion qe imponon modifikimin e rregullt te nje te nje dokumenti magazine sebashku me trupin dhe kontabilitetin
        /// 1. merret dokumenti eksistues  i magazines dhe kalohet ne gjendjen 20 te modifikimit
        /// 2. ruhet dokumenti i ri i magzines se bashku me trupin dhe kontabilitetin
        /// 3. stornohen kontabiliteti i dokumentave eksistues  te magazines
        /// </summary>
        /// <param name="transferim"> tregon nqs dokumenti eshte transferim ne menyre qe te kryhet ruajtja e dokumentit te dyte te transferimit</param>
        /// <param name="dtDk"> data e dokumentit te magazines</param>
        /// <param name="dtRegj"> data e regjistrimit te dokumentit te magazines</param>
        /// <param name="idKDok"> id kategorise se dokumentit me te cilin lidhet.</param>
        /// <param name="idKlFurn"> id e klient furnitorit me te cilin lidhet</param>
        /// <param name="idKonf"> id e konfigurimit te dokumentit</param>
        /// <param name="idLidhes">id e dokumentit nga e cila gjenerohet ne rastet e modifikimit </param>
        /// <param name="idllojdokmag"> id e llojit te dokumentit</param>
        /// <param name="idMag">id e magazines</param>
        /// <param name="idMagKoka">id ritese e kokes se magazines</param>
        /// <param name="idNder"> id e ndermarjes</param>
        /// <param name="idNdVt"> id e ndermarje vitit</param>
        /// <param name="idNiv"> id e nivelit te regjistrimit</param>
        /// <param name="idPer"> id e perdoruesit se e ben regjistrimin</param>
        /// <param name="idProj"> id e projektit ne te cilin ben pjese</param>
        /// <param name="idrenditjes"> id e renditjes per te renditur dokumentat e te njejtes date sipas kohes se regjistrimit</param>
        /// <param name="idSt"> id e statusit te dokumentit</param>
        /// <param name="nrDk">nr i dokumentit</param>
        /// <param name="nrProj"> nr i projektit</param>
        /// <param name="shenim"> shenime </param>
        /// <param name="vl"> vlera totale e dokumentit</param>
        /// <param name="idGjenerues">id e dokumentit nga gjenerohet magazina nga nje ambjent tjeter</param>
        /// <param name="idKonfigGjenerues">id e konfigurimit nga gjenerohet dokumenti</param>
        /// <param name="idNivelGjenerues">id e nivelit nga gjenerohet dokumenti</param>
        /// <param name="ocolTrupiMagazina">kolektion i trupit te magazines</param>
        /// <param name="oFleteKontabel">kolektion i fleteve kontabel</param>
        /// <param name="oMagazinaTransferim">kolektion i transferimit ne magazine</param>
        /// <param name="idgrup1">id e grupimit te pare </param>
        /// <param name="idgrup2">id e grupimit te dyte</param>
        /// <param name="idgrup3">id e grupimit te trete</param>
        /// <param name="meKontabilizim"> tregon nese dokumenti i magazines do te kontabilizohet apo jo</param>
        /// <returns> kthen nje obj clsMesazh per te identifikuar statusin e modifikimit se te dhenave ne DB</returns>
        /// <param name="mosLlogaritAmortizimShtese"></param>
        public clsMesazh modifikoMagazina(int idMagKoka, int idNiv, int idKonf, int idKlFurn, int idMag, DateTime dtDk, string nrDk, int idProj, string nrProj, int idKDok, int idLidhes, double vl, int idSt, int idNder,
            int idNdVt, int idPer, DateTime dtRegj, int idllojdokmag, string shenim, int idrenditjes, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues, int iddegeadministrative, int idllogari, int idnjesivartese,
            bool mekonfirmim, colTrupiMagazina ocolTrupiMagazina, clsKokaFleteKontabel oFleteKontabel, clsKokaMagazina oMagazinaTransferim, int meKontabilizim, bool transferim, string pershkrimFK, int idgrup1, int idgrup2,
            int idgrup3, string pershkrimi, string magazinieri, string adresa, clsDatabaseRegjistrim dbRegj, out string shfaqmesazhapolupe, out int idkokare, clsKokaRezervime kokarezeervime, bool kontrollodisponibel, 
            bool eshteOwn, int idAutomjet, int idRap, colSerialetMagazine serialemag, colSerialetMagazine serialetransf, clsKonfigurimAmbjenti konfamortizimi, clsKonfigurimAmbjenti konfamortizimihyrje, clsKokaMagazina koka,
            bool gjithmone, int kontabilizoamortizim, bool mosLlogaritAmortizimShtese, ResourceManager rm, CultureInfo ci, int idkrijuesi, DateTime dttranp, bool ruajrenditje, string shoferi, string targashoferi,
            bool kontrolloIMEIFifo, bool blerjengadealer, bool promocione, int idKategoriSeriali, out string mesazhmevonshem, bool mbajIdgjeneruesTevjeter, ref List<object[]> mesazheInformueseAsete, 
            colSerialeUnikeKategori kategorite, bool serialeNeDetajim, bool bashkoArtikujt, string nivfsh, string wtnic, int operatori, bool mallraTeDjegshme, bool shoqerimIKerkuar, int transportuesi,string tipi,string transaksioni)
        {
            mesazhmevonshem = "";
            bool kaveprimepas = false;
            idkokare = 0;
            shfaqmesazhapolupe = "jo";
            clsMesazh mesazh;
            //obj koka qe i kalohet si parameter eshte si duhet te modifikohet
            clsKokaMagazina kokaEkzistuese = new clsKokaMagazina();
            colAmortizimiKoka colAmortizimetEVjetra = new colAmortizimiKoka();
            colAmortizimiKoka colAmortizimetEVjetraHyrje = new colAmortizimiKoka();
            clsDatabazeAsete dbasete = new clsDatabazeAsete(dbRegj);
            clsDatabaseInventari dbinv = new clsDatabaseInventari(dbRegj);
            kokaEkzistuese.mbushKokaMagazinaSipasID(idMagKoka, dbRegj);

            if (string.IsNullOrEmpty(kokaEkzistuese.NrDok) || kokaEkzistuese.IdStatusDok == 2)
            {
                return new clsMesazh(false, rm.GetString("msgDokumentiKaNdryshuarHapeniPerseri", ci));
            }
            this.idKrijuesi = kokaEkzistuese.idKrijuesi;//dokumentit te ri i vendosim id e krijuesit te dokumentit te vjeter

            kokaEkzistuese.mbushTrupMagazine(dbRegj);
            var coleksistues = new colArtikujt(kokaEkzistuese.IdKokaMagazina, dbinv);
            
            System.Diagnostics.Stopwatch myWatch = System.Diagnostics.Stopwatch.StartNew();
            for (int i = 0, count = kokaEkzistuese.OcolTrupiMagazina.Count; i < count; i++)
            {
                clsTrupiMagazina trup = kokaEkzistuese.OcolTrupiMagazina[i];
                if (trup.IdLlojVeprimi == 1)
                    trup.Element = coleksistues[i];
            }
            myWatch.Stop();
            if (myWatch.Elapsed > new TimeSpan(0, 0, 1))
                System.Diagnostics.Trace.WriteLine("trup.Element = coleksistues[i];: " + myWatch.Elapsed);
            ocolTrupiMagazina.ForEach(x => x.IdKokaMagazina = idMagKoka);
            myWatch = System.Diagnostics.Stopwatch.StartNew();
            if (mbajIdgjeneruesTevjeter)
            {
                this.idGjenerues = kokaEkzistuese.IdGjenerues;
                this.IdKonfigGjenerues = kokaEkzistuese.IdKonfigGjenerues;
                this.IdNivelGjenerues = kokaEkzistuese.IdNivelGjenerues;
            }
            mesazh = kokaEkzistuese.kontrolloGjendjeNeFshirje(dbRegj, ocolTrupiMagazina, idSt);
            myWatch.Stop();
            if (myWatch.Elapsed > new TimeSpan(0, 0, 1))
                System.Diagnostics.Trace.WriteLine("kokaEkzistuese.kontrolloGjendjeNeFshirje: " + myWatch.Elapsed);
            if (!mesazh.Status)
                return mesazh;
            kokaEkzistuese.IdStatusDok = 2;  //duhet vendosur nje status i pershtatshem per kete modifikim
            if (!mesazh.Status)
                return mesazh;

            kokaEkzistuese.OFleteKontabel = new clsKokaFleteKontabel();
            idLidhes = kokaEkzistuese.IdKokaMagazina;
            clsDatabaseKontabilitet dbkontab = new clsDatabaseKontabilitet(dbRegj);
            clsKokaFleteKontabel newclsKokaFleteKontabel = new clsKokaFleteKontabel(kokaEkzistuese.IdKokaMagazina, 6, dbkontab);
            DbQendraKosto.clsKokaQendraKosto kokaqendra = new DbQendraKosto.clsKokaQendraKosto();
            if (newclsKokaFleteKontabel.NrDukumentiKokaFleteKontabel != null)
            {
                kokaEkzistuese.OFleteKontabel = newclsKokaFleteKontabel;

                DbQendraKosto.clsDatabaseQendraKosto dbqendra = new DbQendraKosto.clsDatabaseQendraKosto(dbkontab);
                kokaqendra.KtheKokaQKSipasIDGjeneruesDheKonfigMeTrup(newclsKokaFleteKontabel.IdKokaFleteKontabel, newclsKokaFleteKontabel.IdKonfigAmbjente, dbqendra);
                if (kokaqendra.IdKoka != 0 && kokaqendra.IdKoka != -1)
                {
                    kokaEkzistuese.OFleteKontabel.KokaQendraKosto = kokaqendra;
                }
                else kokaEkzistuese.OFleteKontabel.KokaQendraKosto = new DbQendraKosto.clsKokaQendraKosto();
            }

            oFleteKontabel.IdDokNga = kokaEkzistuese.OFleteKontabel.IdKokaFleteKontabel;
            oFleteKontabel.KokaQendraKosto.IdDokNga = kokaEkzistuese.oFleteKontabel.KokaQendraKosto.IdKoka;
            int idDokNgaFK = kokaEkzistuese.OFleteKontabel.IdKokaFleteKontabel;
            bool klientFiskalizuar = false;
            if (clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizim())
                klientFiskalizuar = true;
            mesazh = dbRegj.modifikoKokaMagazina(kokaEkzistuese.IdKokaMagazina, kokaEkzistuese.IdNivel, kokaEkzistuese.IdKonfigAmbjente, kokaEkzistuese.IdKlientFurnitor, kokaEkzistuese.IdMagazina, kokaEkzistuese.DtDok,
                kokaEkzistuese.NrDok, kokaEkzistuese.IdProjekt, kokaEkzistuese.NrProjekt, kokaEkzistuese.IdKategoria, kokaEkzistuese.Vlefta, kokaEkzistuese.IdStatusDok, kokaEkzistuese.DtRegjistrimi, kokaEkzistuese.IdLlojDokumentiMagazine,
                kokaEkzistuese.Shenime, kokaEkzistuese.IdDegeAdministrative, kokaEkzistuese.idLlogari, kokaEkzistuese.IdNjesiVartese, kokaEkzistuese.MeKonfirmim, kokaEkzistuese.IdGrup1, kokaEkzistuese.IdGrup2, kokaEkzistuese.IdGrup3,
                kokaEkzistuese.Pershkrimi, kokaEkzistuese.Magazinieri, kokaEkzistuese.Adresa, koka.idPerdoruesi, kokaEkzistuese.idAutomjet, kokaEkzistuese.idRaportDesing, kokaEkzistuese.idKrijuesi, kokaEkzistuese.dtTransporti,
                kokaEkzistuese.Shoferi, kokaEkzistuese.TargaShoferi, kokaEkzistuese.idKategoriSeriali,kokaEkzistuese.nrSerial, NIVFSH, WTNIC, operatori, shoqerimIKerkuar,mallraTeDjegshme,transportuesi, klientFiskalizuar,tipi,transaksioni,clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizimV3());
            if (!mesazh.Status)
                return mesazh;
            mesazh = kaloNeHistorikKokaMagazina(kokaEkzistuese.IdKokaMagazina, dbRegj);
            if (!mesazh.Status)
            {
                return mesazh;
            }
            kokaEkzistuese.OMagazinaTransferim = new clsKokaMagazina();
            kokaEkzistuese.OMagazinaTransferim.IdGjenerues = kokaEkzistuese.IdKokaMagazina;
            kokaEkzistuese.OMagazinaTransferim.mbushKokaMagazinaSipasIDGjenerues(kokaEkzistuese.OMagazinaTransferim.IdGjenerues, 1, kokaEkzistuese.IdKonfigAmbjente, dbRegj);
            if (kokaEkzistuese.OMagazinaTransferim.IdKokaMagazina != 0)
            {
                mesazh = kokaEkzistuese.kontrolloGjendjeMagazineTrasferim(dbRegj, kokaEkzistuese.OMagazinaTransferim, oMagazinaTransferim, idSt);
                if (!mesazh.Status)
                    return mesazh;
                kokaEkzistuese.OMagazinaTransferim.IdStatusDok = 2;
                klientFiskalizuar = false;
                if (clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizim())
                    klientFiskalizuar = true;
                mesazh = dbRegj.modifikoKokaMagazina(kokaEkzistuese.OMagazinaTransferim.IdKokaMagazina, kokaEkzistuese.OMagazinaTransferim.IdNivel, kokaEkzistuese.OMagazinaTransferim.IdKonfigAmbjente, kokaEkzistuese.OMagazinaTransferim.IdKlientFurnitor,
                    kokaEkzistuese.OMagazinaTransferim.IdMagazina, kokaEkzistuese.OMagazinaTransferim.DtDok, kokaEkzistuese.OMagazinaTransferim.NrDok, kokaEkzistuese.OMagazinaTransferim.IdProjekt, kokaEkzistuese.OMagazinaTransferim.NrProjekt,
                    kokaEkzistuese.OMagazinaTransferim.IdKategoria, kokaEkzistuese.OMagazinaTransferim.Vlefta, kokaEkzistuese.OMagazinaTransferim.IdStatusDok, kokaEkzistuese.OMagazinaTransferim.DtRegjistrimi, kokaEkzistuese.OMagazinaTransferim.IdLlojDokumentiMagazine,
                    kokaEkzistuese.OMagazinaTransferim.Shenime, kokaEkzistuese.OMagazinaTransferim.IdDegeAdministrative, kokaEkzistuese.OMagazinaTransferim.idLlogari, kokaEkzistuese.OMagazinaTransferim.IdNjesiVartese, kokaEkzistuese.OMagazinaTransferim.MeKonfirmim,
                    kokaEkzistuese.oMagazinaTransferim.IdGrup1, kokaEkzistuese.oMagazinaTransferim.IdGrup2, kokaEkzistuese.oMagazinaTransferim.IdGrup3, kokaEkzistuese.Pershkrimi, kokaEkzistuese.Magazinieri, kokaEkzistuese.Adresa, koka.idPerdoruesi, kokaEkzistuese.idAutomjet,
                    kokaEkzistuese.idRaportDesing, kokaEkzistuese.idKrijuesi, kokaEkzistuese.dtTransporti, kokaEkzistuese.Shoferi, kokaEkzistuese.TargaShoferi, kokaEkzistuese.idKategoriSeriali, kokaEkzistuese.nrSerial, kokaEkzistuese.NIVFSH,
                    kokaEkzistuese.WTNIC, kokaEkzistuese.IdOperator,kokaEkzistuese.ShoqerimIKerkuar,kokaEkzistuese.MallraTeDjeghsme,kokaEkzistuese.Transportuesi, klientFiskalizuar,kokaEkzistuese.Tipi,koka.Transaksioni, clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizimV3());
                if (!mesazh.Status)
                    return mesazh;
                mesazh = kaloNeHistorikKokaMagazina(kokaEkzistuese.OMagazinaTransferim.IdKokaMagazina, dbRegj);
                if (!mesazh.Status)
                {
                    return mesazh;
                }
            }
            clsKokaRezervime kokaeksistuezerez = new clsKokaRezervime();
            kokaeksistuezerez.mbushKokaRezervimiSipasIDGjenerues(kokaEkzistuese.idKokaMagazina, 2, kokaEkzistuese.IdKonfigAmbjente, dbRegj);
            if (kokaeksistuezerez.IdKokaRezervimi != 0)
            {
                kokarezeervime.IdDokNga = kokaeksistuezerez.IdKokaRezervimi;
                mesazh = kokaeksistuezerez.fshiRezervim(kokaeksistuezerez.IdKokaRezervimi, idPerdoruesi, dbRegj, false);
                if (!mesazh.Status)
                    return mesazh;
            }

            DataTable dtinv = dbRegj.ktheSipasMagazines(kokaEkzistuese.idKokaMagazina);
            int[] idinv = new int[dtinv.Rows.Count];
            int ii = 0;
            foreach (DataRow dr in dtinv.Rows)
            {
                idinv[ii] = int.Parse(dr["IDKOKAINVENTARIZIM"].ToString());
                ii++;
            }
            mesazh = dbRegj.fshiLidhjeMagInvSipasMag(kokaEkzistuese.idKokaMagazina);
            if (!mesazh.Status)
                return mesazh;

            #region amortizimi

            colAmortizimetEVjetra.ktheAmortizimKokaSipasIdGjeneruesi(kokaEkzistuese.idKokaMagazina, kokaEkzistuese.IdKonfigAmbjente);
            DbAsete.colAmortizimiTrupiAbstract trupiIRi = new colAmortizimiTrupi();///krijohet nje trup hipotetik sa per te bere kontrollin tek fshirja e dokumentit sepse trupi i dokumentit krijohet me vone
            foreach (clsSerialetMagazine serial in serialemag)
            {
                trupiIRi.Add(new clsAmortizimiTrupi(0, serial.IdArtikulli, "", 0, new DateTime(), new DateTime(), new DateTime(), serial.IdNjesiAdministrative, serial.IdAQTSeriali, "", new DbInventari.clsArtikulli(), 0));
            }
            foreach (clsAmortizimiKoka kokaam in colAmortizimetEVjetra)
            {
                colAmortizimiKoka kokahyrje = new colAmortizimiKoka();
                kokahyrje.ktheAmortizimKokaSipasIdGjeneruesi(kokaam.IdAmortizimi, kokaam.IdKonfigurimAmbjenti);
                colAmortizimetEVjetraHyrje.AddRange(kokahyrje);
                foreach (clsAmortizimiKoka kokah in kokahyrje)
                {
                    mesazh = kokah.fshi(idPer, 86, false, trupiIRi);
                    if (!mesazh.Status)
                    {
                        return mesazh;
                    }
                }
                mesazh = kokaam.fshi(idPer, 86, false, trupiIRi);
                if (!mesazh.Status)
                {
                    return mesazh;
                }
            }

            var colserialevjeter = new colSerialetMagazine();
            colserialevjeter.merrSerialetMagazineSipasIDDokumenti(kokaEkzistuese.IdKokaMagazina, kokaEkzistuese.idNdermarrje, kokaEkzistuese.IdKonfigAmbjente);

            if (idllojdokmag == 1 && coleksistues.Select(x => x.LlojiArt).Any() && colserialevjeter.Any())
            {
                var idSerialiList = colserialevjeter.Where(y => !serialemag.Any(z => z.IdAQTSeriali == y.IdAQTSeriali)).Select(x => x.IdAQTSeriali).ToList();
                if (idSerialiList.Any())
                {
                    var veprimet = KtheVeprimeTeMevonshmeAsete(idSerialiList, idNder, dtDk);
                    if (veprimet.Rows.Count > 0)
                    {
                        var mesazhetList = veprimet
                            .Rows
                            .Cast<DataRow>()
                            .Select(row => veprimet.Columns.Cast<DataColumn>()
                            .Select(col => row[col])
                            .ToArray())
                            .ToList();

                        var mesazhi = MessagesResource.Messages["ShtoRegjistrimMagazine.NukMundTaFshini"];

                        var mesazhet = mesazhetList.Select(obj => string.Format(MessagesResource.Messages["ShtoRegjistrimMagazine.FshiAsetiMsg"], obj)).ToList();
                        mesazhi += string.Join(", ", mesazhet.ToArray()) + "!";

                        return new clsMesazh(false, mesazhi);
                    }
                }

                idSerialiList = serialemag.Where(y => colserialevjeter.Any(z => z.IdAQTSeriali == y.IdAQTSeriali && z.Vlefta != y.Vlefta)).Select(x => x.IdAQTSeriali).ToList();
                if (idSerialiList.Any())
                {
                    var vaprimet = KtheVeprimeTeMevonshmeAsete(idSerialiList, idNder, dtDk);
                    mesazheInformueseAsete = vaprimet
                        .Rows
                        .Cast<DataRow>()
                        .Select(row => vaprimet.Columns.Cast<DataColumn>()
                        .Select(col => row[col])
                        .ToArray())
                        .ToList();
                }
            }

            foreach (DbCore.DbAsete.clsSerialetMagazine s in colserialevjeter)
            {
                if (kokaEkzistuese.dtDok == dtDok)
                    serialemag.ForEach(x =>
                    {
                        if (x.IdAQTSeriali == s.IdAQTSeriali)
                            x.NrRendDitor = s.NrRendDitor;

                    });
                bool shfaqMesazhGabimi = false;
                clsSerialetMagazine serialri = serialemag.Find(x => x.IdAQTSeriali == s.IdAQTSeriali);
                if (colSerialetMagazine.ktheSerialetMagazineKaVeprimePas(s.IdAQTSeriali, kokaEkzistuese.idNdermarrje, s.NrRendDitor, kokaEkzistuese.dtDok, dbasete))
                {
                    shfaqMesazhGabimi = true;
                    kaveprimepas = true;
                }
                clsAQTSeriale serial = new clsAQTSeriale();
                serial.merrAQTSerialSipasID(s.IdAQTSeriali, dbasete);
                if (serialri == null || serialri.IdNjesiAdministrative != s.IdNjesiAdministrative)// nqs seriali nuk ekziston tek dokumenti i ri ose i ka ndryshuar njesia administrative kontrollojme nqs ka veprime pas
                {
                    if (shfaqMesazhGabimi)
                        return new clsMesazh(false, String.Format("Ka veprime me pas me serialin {0} dhe nuk mund te ndryshohet magazina!", serial.AqtSerialKod));
                }
                mesazh = s.fshi();
                if (!mesazh.Status)
                {
                    return mesazh;
                }

                if (serial.IdHistorikAktualPaSerial > 0)// historikun e serialit te ri te daljes e fshijme
                {
                    clsHistorikAQTSeriale historik = new clsHistorikAQTSeriale();
                    historik.merrHistorikAQTSerialSipasID(serial.IdHistorikAktualPaSerial, kokaEkzistuese.idNdermarrje, dbasete);
                    // clsAQTSeriale serialprind = new clsAQTSeriale();//nesnes
                    // serialprind.merrAQTSerialSipasID(historik.IdPrindi, dbasete);
                    // if (serialprind.IdHistorikAktualPaSerial > 0)
                    //  {
                    // clsHistorikAQTSeriale historikprind = new clsHistorikAQTSeriale();
                    //  historikprind.merrHistorikAQTSerialSipasID(serialprind.IdHistorikAktualPaSerial, kokaEkzistuese.idNdermarrje, dbasete);
                    //  mesazh = dbasete.modifikoHistorikAQTSerial(historikprind.IdHistoriku, kokaEkzistuese.idPerdoruesi, historikprind.IdDok, historikprind.SasiaProgresive + historik.SasiaProgresive, historikprind.CmimiProgresive, historikprind.CmimiProgresive * (historikprind.SasiaProgresive + historik.SasiaProgresive), historikprind.IdStatusDokumenti);//shtojme  sasine e serialit te ri nga prindi meqe seriali i ri do fshihet
                    if (idllojdokmag == 2)
                    {
                        mesazh = dbasete.modifikoHistorikAQTSerial(historik.IdHistoriku, kokaEkzistuese.idPerdoruesi, historik.IdDok, historik.SasiaProgresive + s.Sasia, historik.CmimiProgresive, historik.CmimiProgresive * (historik.SasiaProgresive + s.Sasia), historik.IdStatusDokumenti);//shtojme  sasine e serialit te ri nga prindi meqe seriali i ri do fshihet
                        if (!mesazh.Status)
                            return mesazh;
                    }

                    //   }
                    if (idllojdokmag == 1)
                    {
                        mesazh = dbasete.modifikoHistorikAQTSerialStatusSipasHistorikID(serial.IdHistorikAktualPaSerial, idPer);//nesnes
                        if (!mesazh.Status)
                        {
                            return mesazh;
                        }
                        mesazh = dbasete.modifikoAQTSerialNgaVeprimi(s.IdAQTSeriali, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, 0, serial.IdHistorikAktualPaSerial, kokaEkzistuese.idPerdoruesi, 1, true);//serialin e ri e kalojme pa magazine//nesnes
                        if (!mesazh.Status)
                        {
                            return mesazh;
                        }
                    }
                    //if (idLlojDokumentiMagazine == 2) if (serialemag.Find(x => x.IdAQTSeriali == s.IdAQTSeriali) != null) serialemag.Find(x => x.IdAQTSeriali == s.IdAQTSeriali).IdAQTSeriali = serialprind.IdAQTSerial;//kalohet seriali i prindit//nesnes
                    continue;
                }
                if (idLlojDokumentiMagazine == 1)//hyrje
                    mesazh = dbasete.modifikoAQTSerialNgaVeprimi(s.IdAQTSeriali, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, 0, serial.IdHistorikAktualPaSerial, koka.idPerdoruesi, 1, true);
                else
                {
                    if (serial.AqtSerialDataMagAktive == kokaEkzistuese.dtDok)
                    {
                        clsNjesiAdministrative njesiadm = serial.IdNjesiAdministrativeAktuale == 0 ? new clsNjesiAdministrative() : new clsNjesiAdministrative(serial.IdNjesiAdministrativeAktuale, dbRegj);
                        clsStatusMagazine_Asete status = njesiadm.IdStatusAktualMagazine == 0 ? new clsStatusMagazine_Asete() : new clsStatusMagazine_Asete(njesiadm.IdStatusAktualMagazine, dbasete);

                        clsNjesiAdministrative njesiadmdalje = new clsNjesiAdministrative(s.IdNjesiAdministrative, dbRegj);
                        clsStatusMagazine_Asete statusdalje = new clsStatusMagazine_Asete(njesiadmdalje.IdStatusAktualMagazine, dbasete);

                        if (status.Emertimi == "Aktive" && statusdalje.Emertimi != "Aktive")
                            serial.AqtSerialDataMagAktive = DateTime.MinValue;
                    }
                    mesazh = dbasete.modifikoAQTSerialNgaVeprimi(s.IdAQTSeriali, serial.AqtSerialDataHyrje, serial.AqtSerialDataMagAktive, serial.AqtSerialDataAmortizimfillestar, s.IdNjesiAdministrative, serial.IdHistorikAktualPaSerial, idPer, 1, kaveprimepas);//kthejme serialin ne magazinen qe ishte ate te daljes
                }
                if (!mesazh.Status)
                {
                    return mesazh;
                }
            }
            colSerialetMagazine colserialevjeterhyrje = new colSerialetMagazine();
            colserialevjeterhyrje.merrSerialetMagazineSipasIDDokumenti(kokaEkzistuese.OMagazinaTransferim.IdKokaMagazina, kokaEkzistuese.idNdermarrje, kokaEkzistuese.OMagazinaTransferim.IdKonfigAmbjente);
            foreach (DbCore.DbAsete.clsSerialetMagazine s in colserialevjeterhyrje)
            {
                if (kokaEkzistuese.dtDok == dtDok)
                    serialetransf.ForEach(x =>
                {
                    if (x.IdAQTSeriali == s.IdAQTSeriali)
                        x.NrRendDitor = s.NrRendDitor;

                });
                mesazh = s.fshi();
                if (!mesazh.Status)
                {
                    return mesazh;
                }
                clsAQTSeriale serial = new clsAQTSeriale();
                serial.merrAQTSerialSipasID(s.IdAQTSeriali, dbasete);
                if (serial.IdHistorikAktualPaSerial > 0)
                {
                    mesazh = dbasete.modifikoHistorikAQTSerialStatusSipasHistorikID(serial.IdHistorikAktualPaSerial, idPer);
                    if (!mesazh.Status)
                    {
                        return mesazh;
                    }
                    mesazh = dbasete.modifikoAQTSerialNgaVeprimi(s.IdAQTSeriali, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, 0, serial.IdHistorikAktualPaSerial, kokaEkzistuese.idPerdoruesi, 1, true);//serialin e ri e kalojme pa magazine//nesnes
                    if (!mesazh.Status)
                    {
                        return mesazh;
                    }
                }
            }

            #endregion amortizimi

            clsDatabaseAdmin data = new clsDatabaseAdmin(dbRegj);
            int idPeriudheDoku = new clsPeriudhaKontabel(dtDk, idNder, data).IdPeriudha;
            clsKonfigurimAmbjenti konf = new clsKonfigurimAmbjenti();
            konf.mbushKonfiguriminMeID(idKonf);
            int idLlojDok;
            if (konf != null)
                idLlojDok = konf.IdKategori;
            else
                idLlojDok = -1;

            mesazh = ruajMagazina(out idMagKoka, idNiv, idKonf, idKlFurn, idMag, dtDk, nrDk, idProj, nrProj, idKDok, idLidhes, vl, idSt, idNder, idNdVt, idPer, dtRegj, idllojdokmag, shenim, idrenditjes, IdNivelGjenerues, 
                IdKonfigGjenerues, IdGjenerues, iddegeadministrative, ocolTrupiMagazina, oFleteKontabel, oMagazinaTransferim, transferim, true, meKontabilizim, kokaEkzistuese.OMagazinaTransferim.IdKokaMagazina, idPeriudheDoku,
                dbRegj, pershkrimFK, idLlojDok, idDokNgaFK, idllogari, idnjesivartese, mekonfirmim, idgrup1, idgrup2, idgrup3, pershkrimi, magazinieri, adresa, out shfaqmesazhapolupe, OFleteKontabel.KokaQendraKosto.IdDokNga,
                kokaqendra.ColTrupi, kokarezeervime, kontrollodisponibel, eshteOwn, idAutomjet, idRap, serialemag, serialetransf, konfamortizimi, konfamortizimihyrje, koka, colAmortizimetEVjetra, colAmortizimetEVjetraHyrje, 0, 0,
                new DbQendraKosto.colTrupiQendraKosto(), false, new clsKokaShitje(), false, gjithmone, new colAmortizimiKoka(), kontabilizoamortizim, mosLlogaritAmortizimShtese, kokaEkzistuese.idKrijuesi, dttranp, shoferi, targaShoferi,
                kokaEkzistuese.ocolTrupiMagazina, ruajrenditje, idinv, kaveprimepas, kontrolloIMEIFifo, blerjengadealer, promocione, idKategoriSeriali,nrSerial, out mesazhmevonshem, kategorite, serialeNeDetajim, bashkoArtikujt, nivfsh,
                wtnic, operatori, mallraTeDjegshme,shoqerimIKerkuar,transportuesi,tipi,transaksioni);

            idkokare = idMagKoka;
            if (!mesazh.Status)
                return mesazh;
            clsDatabaseKontabilitet dbkont = new clsDatabaseKontabilitet(dbRegj);
            if (kokaEkzistuese.OFleteKontabel.IdKokaFleteKontabel != 0)
            {
                mesazh = kokaEkzistuese.OFleteKontabel.ModifikoFleteKontabel(true, dbkont);
                if (!mesazh.Status)
                    return mesazh;
            }
            return new clsMesazh(true, rm.GetString("regjMagModifikimiPerfundoiMeSukses", ci));
        }

        /// <summary>
        /// Modifikon objektin e  kokes se dokumentit te magazines ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsKokaMagazina.modifikoMagazina"/>
        /// </summary>
        /// <param name="meKontabilizim">tregon nese te konfigurimet magazina eshte me kontabilizim apo jo</param>
        /// <param name="transferim">nese eshte i transferuar ose jo</param>
        /// <param name="lidhur">tregon nese eshte i lidhur ose jo</param>
        /// <returns > nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo</returns>
        public clsMesazh modifiko(bool transferim, int meKontabilizim, bool lidhur, string pershkrimFK, out string shfaqmesazhapolupe, bool eshteOwn, colSerialetMagazine serialemag, colSerialetMagazine serialetransf,
            clsKonfigurimAmbjenti konfamortizimi, clsKonfigurimAmbjenti konfamortizimihyrje, bool gjithmone, int kontabilizoamortizim, ResourceManager rm, CultureInfo ci, bool ruajrenditje, bool kontrolloIMEIFifo, bool blerjengadealer,
            bool promocione, out string mesazhmevonshem, bool mbajIdGjeneruestevjeter, ref List<object[]> mesazheInformueseAsete, colSerialeUnikeKategori kategorite, bool serialeNeDetajim, bool bashkoArtikujt, bool DokumentTransferimiOwn)
        {
            mesazhmevonshem = "";
            clsMesazh u_modifikua;
            bool kontrollodisponibel = false;
            bool mosLlogaritAmortizimShtese = false;
            if (clsAlternativaKushti.getAlternativa(this.IdKonfigAmbjente, "MLLASH") == "Po")
                mosLlogaritAmortizimShtese = true;
            if (clsAlternativaKushti.getAlternativa(this.IdKonfigAmbjente, "KD") == "Po")
                kontrollodisponibel = true;

            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            shfaqmesazhapolupe = "jo";

            //ne rastet kur ka nderveprim me filesystemin perdorim transactionScope qe te fusim ne nje transaksion ndryshimet ne db dhe ndryshimet ne file system
            using (var scope = new MyTransactionScope())
            {
                try
                {
                    if (!lidhur)
                    {
                        int idkokare = 0;
                        u_modifikua = modifikoMagazina(IdKokaMagazina, IdNivel, IdKonfigAmbjente, IdKlientFurnitor, IdMagazina, DtDok, NrDok, IdProjekt, NrProjekt, IdKategoria, IdDokNga, Vlefta, IdStatusDok, IdNdermarrje, 
                            IdNdermarrjeVit, IdPerdoruesi, DtRegjistrimi, IdLlojDokumentiMagazine, Shenime, IdRenditjes, IdNivelGjenerues, IdKonfigGjenerues, IdGjenerues, IdDegeAdministrative, idLlogari, IdNjesiVartese,
                            MeKonfirmim, OcolTrupiMagazina, OFleteKontabel, OMagazinaTransferim, meKontabilizim, transferim, pershkrimFK, IdGrup1, IdGrup2, IdGrup3, Pershkrimi, Magazinieri, Adresa, data, out shfaqmesazhapolupe,
                            out idkokare, oKokaRezervime, kontrollodisponibel, eshteOwn, idAutomjet, idRaportDesing, serialemag, serialetransf, konfamortizimi, konfamortizimihyrje, this, gjithmone, kontabilizoamortizim,
                            mosLlogaritAmortizimShtese, rm, ci, idKrijuesi, dtTransporti, ruajrenditje, Shoferi, targaShoferi, kontrolloIMEIFifo, blerjengadealer, promocione, idKategoriSeriali, out mesazhmevonshem, mbajIdGjeneruestevjeter,
                            ref mesazheInformueseAsete, kategorite, serialeNeDetajim, bashkoArtikujt, NIVFSH, WTNIC, idOperator,mallraTeDjegshme,shoqerimIKerkuar,transportuesi,tipi,transaksioni);
                        if (!u_modifikua.Status)
                            return u_modifikua;

                        u_modifikua = colArkiva.ModifikoArkiven(idKokaMagazina, idkokare, idKategoria, IdNdermarrje, IdPerdoruesi);

                        this.idKokaMagazina = idkokare;
                        if (!u_modifikua.Status)
                        {
                            return u_modifikua;
                        }
                        scope.Complete();
                        return u_modifikua;
                    }

                    colAmortizimiKoka kokadalje = new colAmortizimiKoka();
                    clsKokaMagazina kokavjeter = new clsKokaMagazina();
                    kokavjeter.mbushKokaMagazinaSipasID(this.idKokaMagazina, data);
                    int idStatusdokGjeneruar = merrIdStatusDok(kokavjeter.IdGjenerues, data); // merret statusi i dokumentit nga i cili eshte gjeneruar
                    //kontrollohet nese dok nga eshte gjeneruar(persh FDTK), nuk eshte i ruajtur, ath nuk duhet te lejohet ruajtja e dok (persh FHTK)
                    if (kokavjeter.idStatusDok == 0 && (clsAlternativaKushti.getAlternativa(this.IdKonfigAmbjente, "DOKMEKONF") == "Jo" || idStatusdokGjeneruar == 0))
                    {
                        return new clsMesazh(false, "Nuk mund te ruani nje dokument te lidhur draft!");
                    }
                    this.idKrijuesi = kokavjeter.idKrijuesi;//dokumentit te ri i vendosim id e krijuesit te dokumentit te vjeter
                    clsKokaMagazina kokatranf = new clsKokaMagazina();
                    kokatranf.mbushKokaMagazinaSipasID(kokavjeter.IdGjenerues, data);
                    kokadalje.ktheAmortizimKokaSipasIdGjeneruesi(kokatranf.idKokaMagazina, kokatranf.idKonfigAmbjente);
                    if (this.IdStatusDok == 4)
                    {
                        kokatranf.IdStatusDok = 4;
                        bool klientiFiskalizuar = false;
                        if (clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizim())
                            klientiFiskalizuar = true;
                        u_modifikua = data.modifikoKokaMagazina(kokatranf.IdKokaMagazina, kokatranf.IdNivel, kokatranf.IdKonfigAmbjente, kokatranf.IdKlientFurnitor, kokatranf.IdMagazina, kokatranf.DtDok, kokatranf.NrDok, 
                            kokatranf.IdProjekt, kokatranf.NrProjekt, kokatranf.IdKategoria, kokatranf.Vlefta, kokatranf.IdStatusDok, kokatranf.DtRegjistrimi, kokatranf.IdLlojDokumentiMagazine, kokatranf.Shenime, 
                            kokatranf.IdDegeAdministrative, kokatranf.idLlogari, kokatranf.IdNjesiVartese, kokatranf.meKonfirmim, kokatranf.IdGrup1, kokatranf.IdGrup2, kokatranf.IdGrup3, kokatranf.Pershkrimi, 
                            kokatranf.Magazinieri, kokatranf.Adresa, kokatranf.idPerdoruesi, kokatranf.idAutomjet, kokatranf.idRaportDesing, kokatranf.idKrijuesi, kokatranf.dtTransporti, kokatranf.Shoferi, 
                            kokatranf.TargaShoferi, kokatranf.idKategoriSeriali, kokatranf.nrSerial, kokatranf.NIVFSH, kokatranf.WTNIC, kokatranf.IdOperator, kokatranf.ShoqerimIKerkuar, kokatranf.MallraTeDjeghsme, kokatranf.Transportuesi, klientiFiskalizuar, kokatranf.Tipi, kokatranf.Transaksioni, clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizimV3());
                        if (!u_modifikua.Status)
                        {
                            return u_modifikua;
                        }
                        foreach (clsAmortizimiKoka kokaam in kokadalje)
                        {
                            u_modifikua = kokaam.modifikoStatusAmortizimKokaTransaksion(idPerdoruesi, 4, 86);
                            if (!u_modifikua.Status)
                            {
                                return u_modifikua;
                            }
                        }
                        colSerialetMagazine colserialevjeter = new colSerialetMagazine();
                        colserialevjeter.merrSerialetMagazineSipasIDDokumenti(kokatranf.idKokaMagazina, idNdermarrje, kokatranf.IdKonfigAmbjente);//serialet e daljes
                        foreach (clsSerialetMagazine s in colserialevjeter)
                        {
                            s.IdStatusDokumenti = idStatusDok;
                            u_modifikua = s.modifiko();
                            if (!u_modifikua.Status)
                            {
                                return u_modifikua;
                            }
                        }
                    }
                    bool klientFiskalizuar = false;
                    if (clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizim())
                        klientFiskalizuar = true;
                    u_modifikua = data.modifikoKokaMagazina(IdKokaMagazina, IdNivel, IdKonfigAmbjente, IdKlientFurnitor, IdMagazina, DtDok, NrDok, IdProjekt, NrProjekt, IdKategoria, Vlefta, IdStatusDok, DtRegjistrimi,
                        IdLlojDokumentiMagazine, Shenime, IdDegeAdministrative, idLlogari, IdNjesiVartese, meKonfirmim, idGrup1, IdGrup2, IdGrup3, Pershkrimi, Magazinieri, Adresa, idPerdoruesi, idAutomjet, idRaportDesing,
                        idKrijuesi, dtTransporti, Shoferi, TargaShoferi, idKategoriSeriali, nrSerial, NIVFSH, WTNIC, idOperator, shoqerimIKerkuar,mallraTeDjegshme,transportuesi,klientFiskalizuar,tipi,transaksioni, clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizimV3());
                    if (!u_modifikua.Status)
                    {
                        return u_modifikua;
                    }

                    if (DokumentTransferimiOwn && this.IdStatusDok == 1)
                    {
                        u_modifikua = RuajDetajimetNeTrup(data, kategorite);
                        if (!u_modifikua)
                            return u_modifikua;
                    }
                    foreach (clsAmortizimiKoka kokaam in kokadalje)
                    {
                        colAmortizimiKoka kokahyrje = new colAmortizimiKoka();
                        kokahyrje.ktheAmortizimKokaSipasIdGjeneruesi(kokaam.IdAmortizimi, kokaam.IdKonfigurimAmbjenti);
                        foreach (clsAmortizimiKoka kokah in kokahyrje)
                        {
                            u_modifikua = kokah.modifikoStatusAmortizimKokaTransaksion(idPerdoruesi, this.IdStatusDok, 86);
                            if (!u_modifikua.Status)
                            {
                                return u_modifikua;
                            }
                        }
                    }
                    colSerialetMagazine colserialevjeterkonf = new colSerialetMagazine();
                    colserialevjeterkonf.merrSerialetMagazineSipasIDDokumenti(IdKokaMagazina, idNdermarrje, IdKonfigAmbjente);//serialet e hyrjes
                    clsDatabazeAsete dbasete = new clsDatabazeAsete();
                    foreach (clsSerialetMagazine s in colserialevjeterkonf)
                    {
                        s.IdStatusDokumenti = idStatusDok;
                        u_modifikua = s.modifiko();
                        if (!u_modifikua.Status)
                        {
                            return u_modifikua;
                        }
                        clsAQTSeriale serial = new clsAQTSeriale();
                        serial.merrAQTSerialSipasID(s.IdAQTSeriali);
                        if (IdStatusDok == 4)
                        {
                            u_modifikua = dbasete.modifikoAQTSerialNgaVeprimi(s.IdAQTSeriali, serial.AqtSerialDataHyrje, serial.AqtSerialDataMagAktive, serial.AqtSerialDataAmortizimfillestar, serial.IdNjesiAdministrativeAktuale, serial.IdHistorikAktualPaSerial, idPerdoruesi, 1, false);
                            if (!u_modifikua.Status)
                            {
                                return u_modifikua;
                            }
                            if (serial.IdHistorikAktualPaSerial > 0)// historikun e serialit te ri te daljes e fshijme
                            {
                                clsHistorikAQTSeriale historik = new clsHistorikAQTSeriale();
                                historik.merrHistorikAQTSerialSipasID(serial.IdHistorikAktualPaSerial, idNdermarrje);
                                clsAQTSeriale serialprind = new clsAQTSeriale();
                                serialprind.merrAQTSerialSipasID(historik.IdPrindi);
                                if (serialprind.IdHistorikAktualPaSerial > 0)
                                {
                                    clsHistorikAQTSeriale historikprind = new clsHistorikAQTSeriale();
                                    historikprind.merrHistorikAQTSerialSipasID(serialprind.IdHistorikAktualPaSerial, idNdermarrje);
                                    u_modifikua = dbasete.modifikoHistorikAQTSerial(historikprind.IdHistoriku, idPerdoruesi, historikprind.IdDok, historikprind.SasiaProgresive + historik.SasiaProgresive, historikprind.CmimiProgresive, historikprind.CmimiProgresive * (historikprind.SasiaProgresive + historik.SasiaProgresive), historikprind.IdStatusDokumenti);//shtojme  sasine e serialit te ri nga prindi meqe seriali i ri do fshihet
                                    if (!u_modifikua.Status)
                                    {
                                        //data.rollbackTransaksion();
                                        return u_modifikua;
                                    }
                                }
                                u_modifikua = dbasete.modifikoHistorikAQTSerialStatusSipasHistorikID(serial.IdHistorikAktualPaSerial, idPerdoruesi);
                                if (!u_modifikua.Status)
                                {
                                    //data.rollbackTransaksion();
                                    return u_modifikua;
                                }

                                u_modifikua = dbasete.modifikoAQTSerialNgaVeprimi(s.IdAQTSeriali, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, 0, serial.IdHistorikAktualPaSerial, idPerdoruesi, 1, false);//serialin e ri e kalojme pa magazine
                                if (!u_modifikua.Status)
                                {
                                    //data.rollbackTransaksion();
                                    return u_modifikua;
                                }
                            }
                        }
                        else
                        {
                            if (serial.AqtSerialDataMagAktive == DateTime.MinValue)
                            {
                                clsNjesiAdministrative njesiadm = new clsNjesiAdministrative(s.IdNjesiAdministrative, data);
                                clsStatusMagazine_Asete status = new clsStatusMagazine_Asete(njesiadm.IdStatusAktualMagazine);

                                if (status.Emertimi == "Aktive")
                                    serial.AqtSerialDataMagAktive = dtDok;
                            }
                            u_modifikua = dbasete.modifikoAQTSerialNgaVeprimi(s.IdAQTSeriali, serial.AqtSerialDataHyrje, serial.AqtSerialDataMagAktive, serial.AqtSerialDataAmortizimfillestar, ocolTrupiMagazina[s.NrRendor].IdMag, serial.IdHistorikAktualPaSerial, idPerdoruesi, 1, false);
                            if (!u_modifikua.Status)
                            {
                                //data.rollbackTransaksion();
                                return u_modifikua;
                            }
                        }
                    }
                    

                  var dt = this.OcolTrupiMagazina.Where(x => x.OColSerialeUnikeMagazina != null).Select(x => new { IDKOKAMAGAZINA = this.IdKokaMagazina, IDTRUPIMAGAZINA = x.IdTrupiMagazina, idartikull = x.IdArtikulli, idmag = x.IdMag, IDARTIKULLSET = x.IdArtikullSet }).ToDataTable("IDKOKAMAGAZINA", "IDTRUPIMAGAZINA", "idartikull", "idmag", "IDARTIKULLSET");
                   if(RuajSerialeUnike(dt, IdLlojDokumentiMagazine, new clsDatabaseRegjistrim(), DtDok, ref mesazhmevonshem, true))
                        scope.Complete();
                    return u_modifikua;
                }
                catch (Exception err)
                {
                    return new clsMesazh(err.Message);
                }
            }
        }

        private clsMesazh RuajDetajimetNeTrup(clsDatabaseRegjistrim data, colSerialeUnikeKategori kategorite)
        {
                colSerialeUnikeMagazina serialet = new colSerialeUnikeMagazina(clsKokaMagazina.merrIdGjenerues(IdKokaMagazina), IdNdermarrje);

            var t = this.ocolTrupiMagazina.Where(trupMag => {
                var kat = kategorite.MerrKategoriSipasIdFormatit(((clsArtikulli)trupMag.Element).IdFormatSeriali);
                return ((clsArtikulli)trupMag.Element).DetajimArtikulli && kat != null && kat.Kategori.EqualsIgnoreCase(enumKategoriSerialesh.APARATE.ToString());
            }).ToList().Find(trup =>
                !serialet.Exists(serial => serial.SerialiKryesore == trup.KodDetajimi1 && trup.IdArtikulli == serial.IdArtikulli));

            if (t != null)
            {
                if (string.IsNullOrWhiteSpace(t.KodDetajimi1))
                    return new MesazhGabimi($"Ju lutem plotesoni IMEI per artikullin {((clsArtikulli)t.Element).KodArtikulli}!");
                return new MesazhGabimi($"IMEI {t.KodDetajimi1} per artikullin {((clsArtikulli)t.Element).KodArtikulli}! nuk i takon ketij dokumenti");
            }
            data.RuajDetajimetNeTrup(this.OcolTrupiMagazina.ToDataTable("IdTrupiMagazina", "IdDetajimi"));

            return new MesazhSuksesi();
        }

        private static clsMesazh fshiAmortizimSipasAmortizimitOseShperndarjes(clsAQTSeriale serial, clsKokaMagazina kokaEkzistuese, clsDatabazeAsete dbasete)
        {
            clsMesazh pergjigja = new clsMesazh(true);
            if (serial.IdHistorikAktualPaSerial > 0)// historikun e serialit te ri te daljes e fshijme
            {
                clsHistorikAQTSeriale historik = new clsHistorikAQTSeriale();
                historik.merrHistorikAQTSerialSipasID(serial.IdHistorikAktualPaSerial, kokaEkzistuese.IdNdermarrje, dbasete);

                clsSerialetMagazine serialmag = new clsSerialetMagazine();
                serialmag.ktheSerialetMagazineSipasIDSerialDokFunditmerrVepriminFundit(serial.IdAQTSerial, kokaEkzistuese.IdNdermarrje, kokaEkzistuese.dtDok, 0, dbasete);
                historik.CmimiProgresive = clsSerialetMagazine.ktheSerialetMagazineCmimiSipasIDSerialDokFundit(serial.IdAQTSerial, kokaEkzistuese.IdNdermarrje, kokaEkzistuese.dtDok, serialmag.NrRendDitor, dbasete);
                historik.VleftaProgresive = historik.CmimiProgresive * historik.SasiaProgresive;
                pergjigja = dbasete.modifikoHistorikAQTSerial(historik.IdHistoriku, kokaEkzistuese.IdPerdoruesi, serialmag.IdDok, historik.SasiaProgresive, historik.CmimiProgresive, historik.VleftaProgresive, historik.IdStatusDokumenti);//shtojme  sasine e serialit te ri nga prindi meqe seriali i ri do fshihet
                if (!pergjigja.Status)
                    return pergjigja;

            }
            return pergjigja;
        }
        private static clsMesazh fshiAmortizimSipasMagazinesOseShitjes(clsAQTSeriale serial, clsKokaMagazina kokaEkzistuese, clsDatabazeAsete dbasete, clsSerialetMagazine s, int idperdoruesi, clsDatabaseRegjistrim dbRegj, bool modifikimblerje, bool vjenngaModifikimShitje)
        {
            clsMesazh mesazh = new clsMesazh(true);


            if (serial.IdHistorikAktualPaSerial > 0)// historikun e serialit te ri te daljes e fshijme
            {
                clsHistorikAQTSeriale historik = new clsHistorikAQTSeriale();
                historik.merrHistorikAQTSerialSipasID(serial.IdHistorikAktualPaSerial, kokaEkzistuese.idNdermarrje, dbasete);

                if (kokaEkzistuese.idLlojDokumentiMagazine == 2)
                {
                    mesazh = dbasete.modifikoHistorikAQTSerial(historik.IdHistoriku, kokaEkzistuese.idPerdoruesi, historik.IdDok, historik.SasiaProgresive + s.Sasia, historik.CmimiProgresive, historik.CmimiProgresive * (historik.SasiaProgresive + s.Sasia), historik.IdStatusDokumenti);//shtojme  sasine e serialit te ri nga prindi meqe seriali i ri do fshihet

                    if (!mesazh.Status)
                    {

                        return mesazh;
                    }
                }

                // }
                if (kokaEkzistuese.idLlojDokumentiMagazine == 1)
                {
                    if (vjenngaModifikimShitje)
                        mesazh = dbasete.modifikoHistorikAQTSerial(historik.IdHistoriku, kokaEkzistuese.idPerdoruesi, historik.IdDok, historik.SasiaProgresive - s.Sasia, historik.CmimiProgresive, historik.CmimiProgresive * (historik.SasiaProgresive - s.Sasia), historik.IdStatusDokumenti);//shtojme  sasine e serialit te ri nga prindi meqe seriali i ri do 
                    else mesazh = dbasete.modifikoHistorikAQTSerialStatusSipasHistorikID(serial.IdHistorikAktualPaSerial, idperdoruesi);//nesnes
                    if (!mesazh.Status)
                    {

                        return mesazh;
                    }
                    mesazh = dbasete.modifikoAQTSerialNgaVeprimi(s.IdAQTSeriali, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, 0, serial.IdHistorikAktualPaSerial, kokaEkzistuese.idPerdoruesi, 1, modifikimblerje);//serialin e ri e kalojme pa magazine//nesnes

                    if (!mesazh.Status)
                    {

                        return mesazh;
                    }
                }
                return mesazh;
            }
            if (kokaEkzistuese.idLlojDokumentiMagazine == 1)
                mesazh = dbasete.modifikoAQTSerialNgaVeprimi(s.IdAQTSeriali, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, 0, serial.IdHistorikAktualPaSerial, idperdoruesi, 1, modifikimblerje);
            else
            {
                //rasti kur eshte dalje, por ka magazine aktuale, pra nuk eshte null
                if (serial.AqtSerialDataMagAktive == kokaEkzistuese.dtDok && serial.IdNjesiAdministrativeAktuale != 0)
                {
                    clsNjesiAdministrative njesiadm = new clsNjesiAdministrative(serial.IdNjesiAdministrativeAktuale, dbRegj);
                    clsStatusMagazine_Asete status = new clsStatusMagazine_Asete(njesiadm.IdStatusAktualMagazine, dbasete);

                    clsNjesiAdministrative njesiadmdalje = new clsNjesiAdministrative(s.IdNjesiAdministrative, dbRegj);
                    clsStatusMagazine_Asete statusdalje = new clsStatusMagazine_Asete(njesiadmdalje.IdStatusAktualMagazine, dbasete);

                    if (status.Emertimi == "Aktive" && statusdalje.Emertimi != "Aktive")
                        serial.AqtSerialDataMagAktive = DateTime.MinValue;
                }

                mesazh = dbasete.modifikoAQTSerialNgaVeprimi(s.IdAQTSeriali, serial.AqtSerialDataHyrje, serial.AqtSerialDataMagAktive, serial.AqtSerialDataAmortizimfillestar, s.IdNjesiAdministrative, serial.IdHistorikAktualPaSerial, idperdoruesi, 1, false);//kthejme serialin ne magazinen qe ishte ate te daljes

            }
            if (!mesazh.Status)
            {

                return mesazh;
            }
            mesazh = dbasete.modifikoHistorikAQTSerialStatusSipasHistorikID(serial.IdHistorikAktualPaSerial, idperdoruesi);
            if (!mesazh.Status)
            {
                return mesazh;
            }
            return mesazh;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idperdoruesi"></param>
        /// <param name="dbRegj"></param>
        /// <param name="kokaEkzistuese"></param>
        /// <param name="sipasShitjes">tregon nqs eshte true atehere beje fshirjen sipas shitjes dhe magazines, nqs eshte false beje fshirjen sipas amortizimit dhe shperndarjes se shpenzimeve</param>
        /// <returns></returns>
        private static clsMesazh fshiAmortizimMagazine(int idperdoruesi, clsDatabaseRegjistrim dbRegj, clsKokaMagazina kokaEkzistuese, bool sipasShitjes, bool modifikimblerje, bool vjenngaModifikimShitje, colSerialetMagazine colserialet, out bool kaveprimepas, bool kontrolloveprimepas)
        {
            kaveprimepas = false;
            clsMesazh mesazh = new clsMesazh(true);
            clsDatabazeAsete dbasete = new clsDatabazeAsete(dbRegj);
            colAmortizimiKoka colAmortizimetEVjetra = new colAmortizimiKoka();

            colAmortizimetEVjetra.ktheAmortizimKokaSipasIdGjeneruesi(kokaEkzistuese.idKokaMagazina, kokaEkzistuese.IdKonfigAmbjente);
            foreach (clsAmortizimiKoka kokaam in colAmortizimetEVjetra)
            {
                colAmortizimiKoka kokahyrje = new colAmortizimiKoka();
                kokahyrje.ktheAmortizimKokaSipasIdGjeneruesi(kokaam.IdAmortizimi, kokaam.IdKonfigurimAmbjenti);
                foreach (clsAmortizimiKoka kokah in kokahyrje)
                {
                    mesazh = kokah.fshi(idperdoruesi, 86, false, new colAmortizimiTrupi());
                    if (!mesazh.Status)
                        return mesazh;

                }
                mesazh = kokaam.fshi(idperdoruesi, 86, false, new colAmortizimiTrupi());
                if (!mesazh.Status)
                    return mesazh;
            }
            colSerialetMagazine colserialevjeter = new colSerialetMagazine();
            colserialevjeter.merrSerialetMagazineSipasIDDokumenti(kokaEkzistuese.IdKokaMagazina, kokaEkzistuese.idNdermarrje, kokaEkzistuese.IdKonfigAmbjente);

            foreach (clsSerialetMagazine s in colserialevjeter)
            {
                clsAQTSeriale serial = new clsAQTSeriale();
                serial.merrAQTSerialSipasID(s.IdAQTSeriali, dbasete);

                clsSerialetMagazine serialri = colserialet.Find(x => x.IdAQTSeriali == s.IdAQTSeriali);
                kaveprimepas = (kontrolloveprimepas && colSerialetMagazine.ktheSerialetMagazineKaVeprimePas(s.IdAQTSeriali, kokaEkzistuese.idNdermarrje, s.NrRendDitor, kokaEkzistuese.dtDok, dbasete));

                if (serialri == null || serialri.IdNjesiAdministrative != s.IdNjesiAdministrative)// nqs seriali nuk ekziston tek dokumenti i ri ose i ka ndryshuar njesia administrative kontrollojme nqs ka veprime pas
                {
                    if (kaveprimepas)
                        return new clsMesazh(false, "Ka veprime me pas me kete serial dhe nuk mund te ndryshohet magazina!");
                }
                mesazh = s.fshi();
                if (!mesazh.Status)
                    return mesazh;

                if (sipasShitjes)
                    mesazh = fshiAmortizimSipasMagazinesOseShitjes(serial, kokaEkzistuese, dbasete, s, idperdoruesi, dbRegj, modifikimblerje, vjenngaModifikimShitje);
                else
                    mesazh = fshiAmortizimSipasAmortizimitOseShperndarjes(serial, kokaEkzistuese, dbasete);
            }

            colSerialetMagazine colserialevjeterhyrje = new colSerialetMagazine();
            colserialevjeterhyrje.merrSerialetMagazineSipasIDDokumenti(kokaEkzistuese.OMagazinaTransferim.IdKokaMagazina, kokaEkzistuese.idNdermarrje, kokaEkzistuese.OMagazinaTransferim.IdKonfigAmbjente);
            foreach (clsSerialetMagazine s in colserialevjeterhyrje)
            {
                mesazh = s.fshi();
                if (!mesazh.Status)
                    return mesazh;

                clsAQTSeriale serial = new clsAQTSeriale();
                serial.merrAQTSerialSipasID(s.IdAQTSeriali, dbasete);
                if (serial.IdHistorikAktualPaSerial > 0)
                {
                    mesazh = dbasete.modifikoHistorikAQTSerialStatusSipasHistorikID(serial.IdHistorikAktualPaSerial, idperdoruesi);
                    if (!mesazh.Status)
                        return mesazh;
                    mesazh = dbasete.modifikoAQTSerialNgaVeprimi(s.IdAQTSeriali, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, 0, serial.IdHistorikAktualPaSerial, kokaEkzistuese.idPerdoruesi, 1, false);//serialin e ri e kalojme pa magazine//nesnes
                    if (!mesazh.Status)
                        return mesazh;
                }
            }
            return mesazh;
        }
        /// <summary>
        /// fshin nje objekt dokument magazine sebashku me te trupin dhe kontabilitetin perkates
        /// Nje objekt dokument magazine ka nje koleksion me trupin dhe kontabilitetin ,
        /// fshirja e nje dokumenti magazine imponon fshirjen edhe te nje colection-i me trupin dhe kontabilitetin
        /// Mqs cdo rresht i ri qe fshihet ne DB kerkon thirrjen e nje SP-je me parametra dhe dokumenti i magazines bashke me trupin dhe kontabilitetin konsiderohet si nje regjistrim,
        /// perdoret nje transaksion qe imponon fshirjen e rregullt te nje te nje dokumenti sebashku me trupin dhe kontabilitetin e tij
        ///1. merr kontabilitetet eksistuese te dokumentit te shitjes dhe magazines dhe i stornon
        ///2. ben fshirjen e trupit dhe me pas te kokes se dokumentit te magazines
        /// </summary>
        ///<param name="idMagKoka"> koka e dokumentit te magazines i cili do te fshihet</param>
        ///<param name="dbRegj"> clsDatabase regjistrimi kur eshte pjese e nje trasaksioni</param>
        ///<param name="idperdoruesi"> id e perdoruesit qe po kryen veprimin</param>
        /// <returns> kthen nje obj clsMesazh per te identifikuar statusin e fshirjes se te dhenave ne DB</returns>
        public clsMesazh fshiMagazina(int idMagKoka, int idperdoruesi, clsDatabaseRegjistrim dbRegj, bool sipasShitjes, bool modifikimblerje, bool vjenngaModifikimShitje, colSerialetMagazine colserialet, out bool kaveprimepas, bool kontrolloveprimepas)
        {
            kaveprimepas = false;
            clsMesazh mesazh;
            clsDatabaseKontabilitet dbkontab = new clsDatabaseKontabilitet(dbRegj);
            clsKokaMagazina kokaEkzistuese = new clsKokaMagazina();
            kokaEkzistuese.mbushKokaMagazinaSipasID(idMagKoka, dbRegj);
            clsKokaFleteKontabel newclsKokaFleteKontabel = new clsKokaFleteKontabel(kokaEkzistuese.IdKokaMagazina, 6, dbkontab);///fleta kontabel e dokumentit
            if (newclsKokaFleteKontabel.NrDukumentiKokaFleteKontabel != null)
                kokaEkzistuese.OFleteKontabel = newclsKokaFleteKontabel;
            else
                kokaEkzistuese.OFleteKontabel = new clsKokaFleteKontabel();

            kokaEkzistuese.OMagazinaTransferim = new clsKokaMagazina();
            kokaEkzistuese.OMagazinaTransferim.IdGjenerues = kokaEkzistuese.IdKokaMagazina;
            kokaEkzistuese.OMagazinaTransferim.mbushKokaMagazinaSipasIDGjenerues(kokaEkzistuese.OMagazinaTransferim.IdGjenerues, 1, kokaEkzistuese.IdKonfigAmbjente, dbRegj);///magazina per transferim
            mesazh = dbRegj.fshiLidhjeMagInvSipasMag(kokaEkzistuese.idKokaMagazina);
            if (!mesazh.Status)
                return mesazh;
            if (kokaEkzistuese.OFleteKontabel.IdKokaFleteKontabel != 0)/// nqs dokumenti eshte i kontabilizuar kalojme dokumentin me status fshire dhe nqs eshte me stornim krijojme dokumentin e kundert
            {
                mesazh = kokaEkzistuese.OFleteKontabel.fshiupd(dbkontab);
                if (!mesazh.Status)
                    return mesazh;
            }
            mesazh = colArkiva.UpdateStatusDokFshi(new clsDatabaseShare(dbRegj), kokaEkzistuese.idMagazina, kokaEkzistuese.idKategoria, idperdoruesi);
            if (!mesazh.Status)
            {
                return mesazh;
            }

            if (kokaEkzistuese.OMagazinaTransferim.IdKokaMagazina != 0)/// nqs dokumenti ka dokument transferimi e kalojme kete dokument me status fshire
            {
                clsKokaMagazina KokaRe = new clsKokaMagazina();
                kokaEkzistuese.OMagazinaTransferim.mbushTrupMagazine(dbRegj);
                mesazh = kokaEkzistuese.kontrolloGjendjeMagazineTrasferim(dbRegj, kokaEkzistuese.OMagazinaTransferim, new clsKokaMagazina(), kokaEkzistuese.OMagazinaTransferim.IdStatusDok);
                if (!mesazh.Status)
                    return mesazh;

                kokaEkzistuese.OMagazinaTransferim.IdStatusDok = 2;
                bool klientiFiskalizuar = false;
                if (clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizim())
                    klientiFiskalizuar = true;
                mesazh = dbRegj.modifikoKokaMagazina(kokaEkzistuese.OMagazinaTransferim.IdKokaMagazina, kokaEkzistuese.OMagazinaTransferim.IdNivel, kokaEkzistuese.OMagazinaTransferim.IdKonfigAmbjente, kokaEkzistuese.OMagazinaTransferim.IdKlientFurnitor, kokaEkzistuese.OMagazinaTransferim.IdMagazina, kokaEkzistuese.OMagazinaTransferim.DtDok, kokaEkzistuese.OMagazinaTransferim.NrDok, kokaEkzistuese.OMagazinaTransferim.IdProjekt, kokaEkzistuese.OMagazinaTransferim.NrProjekt, kokaEkzistuese.OMagazinaTransferim.IdKategoria, kokaEkzistuese.OMagazinaTransferim.Vlefta, kokaEkzistuese.OMagazinaTransferim.IdStatusDok, kokaEkzistuese.OMagazinaTransferim.DtRegjistrimi, kokaEkzistuese.OMagazinaTransferim.IdLlojDokumentiMagazine, kokaEkzistuese.OMagazinaTransferim.Shenime, kokaEkzistuese.OMagazinaTransferim.IdDegeAdministrative, kokaEkzistuese.oMagazinaTransferim.idLlogari, kokaEkzistuese.OMagazinaTransferim.idNjesiVartese, kokaEkzistuese.OMagazinaTransferim.meKonfirmim, kokaEkzistuese.OMagazinaTransferim.IdGrup1, kokaEkzistuese.OMagazinaTransferim.IdGrup2, kokaEkzistuese.OMagazinaTransferim.IdGrup3, kokaEkzistuese.OMagazinaTransferim.Pershkrimi,
                    kokaEkzistuese.OMagazinaTransferim.Magazinieri, kokaEkzistuese.OMagazinaTransferim.Adresa, idperdoruesi, kokaEkzistuese.OMagazinaTransferim.idAutomjet, kokaEkzistuese.OMagazinaTransferim.idRaportDesing, kokaEkzistuese.oMagazinaTransferim.IdKrijuesi, kokaEkzistuese.oMagazinaTransferim.dtTransporti, kokaEkzistuese.OMagazinaTransferim.Shoferi, kokaEkzistuese.OMagazinaTransferim.TargaShoferi, kokaEkzistuese.oMagazinaTransferim.idKategoriSeriali, kokaEkzistuese.oMagazinaTransferim.nrSerial, kokaEkzistuese.NIVFSH, kokaEkzistuese.WTNIC, kokaEkzistuese.IdOperator, kokaEkzistuese.MallraTeDjeghsme, kokaEkzistuese.ShoqerimIKerkuar, kokaEkzistuese.Transportuesi, klientiFiskalizuar, kokaEkzistuese.Tipi, kokaEkzistuese.Transaksioni, clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizimV3());
                if (!mesazh.Status)
                    return mesazh;
                mesazh = clsKokaMagazina.kaloNeHistorikKokaMagazina(kokaEkzistuese.OMagazinaTransferim.IdKokaMagazina, dbRegj);
                if (!mesazh.Status)
                {
                    return mesazh;
                }
            }
            kokaEkzistuese.mbushKokaMagazinaSipasID(idMagKoka, dbRegj);
            mesazh = dbRegj.fshiKonvertimSipasIdDokKonvertuar(kokaEkzistuese.IdKokaMagazina);///fshijme lidhjet tek konvertimet
            if (!mesazh.Status)
                return mesazh;
            clsKokaRezervime kokaeksistuezerez = new clsKokaRezervime();///fshijme dokumentat e rezervimit te krijuara nga ky dokument
            kokaeksistuezerez.mbushKokaRezervimiSipasIDGjenerues(kokaEkzistuese.idKokaMagazina, 2, kokaEkzistuese.IdKonfigAmbjente, dbRegj);
            if (kokaeksistuezerez.IdKokaRezervimi != 0)
            {
                mesazh = kokaeksistuezerez.fshiRezervim(kokaeksistuezerez.IdKokaRezervimi, idperdoruesi, dbRegj, false);
                if (!mesazh.Status)
                    return mesazh;
            }

            #region amortizimi


            mesazh = fshiAmortizimMagazine(idperdoruesi, dbRegj, kokaEkzistuese, sipasShitjes, modifikimblerje, vjenngaModifikimShitje, colserialet, out kaveprimepas, kontrolloveprimepas); //todo kevi besoj behet qe te anashkalohet per ato dok qe skane artikuj aqt
            if (!mesazh.Status)
                return mesazh;
            #endregion amortizimi

            kokaEkzistuese.IdStatusDok = 2; ///fshijme dokumentin e magazines duke e kaluar me status fshire
            bool klientFiskalizuar = false;
            if (clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizim())
                klientFiskalizuar = true;
            mesazh = dbRegj.modifikoKokaMagazina(kokaEkzistuese.IdKokaMagazina, kokaEkzistuese.IdNivel, kokaEkzistuese.IdKonfigAmbjente, kokaEkzistuese.IdKlientFurnitor, kokaEkzistuese.IdMagazina, kokaEkzistuese.DtDok, kokaEkzistuese.NrDok, kokaEkzistuese.IdProjekt, kokaEkzistuese.NrProjekt, kokaEkzistuese.IdKategoria, kokaEkzistuese.Vlefta, kokaEkzistuese.IdStatusDok, kokaEkzistuese.DtRegjistrimi, kokaEkzistuese.IdLlojDokumentiMagazine, kokaEkzistuese.Shenime, kokaEkzistuese.IdDegeAdministrative, kokaEkzistuese.idLlogari, kokaEkzistuese.IdNjesiVartese, kokaEkzistuese.MeKonfirmim, kokaEkzistuese.IdGrup1, kokaEkzistuese.IdGrup2, kokaEkzistuese.IdGrup3, kokaEkzistuese.Pershkrimi, kokaEkzistuese.Magazinieri, kokaEkzistuese.Adresa, idperdoruesi, kokaEkzistuese.idAutomjet, kokaEkzistuese.idRaportDesing, kokaEkzistuese.IdKrijuesi, kokaEkzistuese.dtTransporti, kokaEkzistuese.Shoferi, kokaEkzistuese.TargaShoferi, kokaEkzistuese.idKategoriSeriali,kokaEkzistuese.nrSerial, kokaEkzistuese.NIVFSH, kokaEkzistuese.WTNIC, kokaEkzistuese.IdOperator, kokaEkzistuese.ShoqerimIKerkuar, kokaEkzistuese.MallraTeDjeghsme, kokaEkzistuese.Transportuesi,klientFiskalizuar, kokaEkzistuese.Tipi, kokaEkzistuese.Transaksioni, clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizimV3());
            if (!mesazh.Status)
                return mesazh;
            mesazh = clsKokaMagazina.kaloNeHistorikKokaMagazina(kokaEkzistuese.IdKokaMagazina, dbRegj);
            if (!mesazh.Status)
            {
                return mesazh;
            }
            return new clsMesazh(true, "Fshirja përfundoi me sukses!");
        }
        public bool rivleresim()
        {
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            clsKokaMagazina kokaEkzistuese = new clsKokaMagazina();
            try
            {
                data.beginTransaksion();
                if (!kokaEkzistuese.mbushKokaMagazinaSipasID(idKokaMagazina, data))
                {
                    data.rollbackTransaksion();
                    return false;
                }
                kokaEkzistuese.OMagazinaTransferim = new clsKokaMagazina();
                kokaEkzistuese.OMagazinaTransferim.IdGjenerues = kokaEkzistuese.IdKokaMagazina;
                kokaEkzistuese.OMagazinaTransferim.mbushKokaMagazinaSipasIDGjenerues(kokaEkzistuese.OMagazinaTransferim.IdGjenerues, 1, kokaEkzistuese.IdKonfigAmbjente, data);
                if (kokaEkzistuese.OMagazinaTransferim.IdKokaMagazina != 0)
                {
                    colTrupiMagazina trupattrans = new colTrupiMagazina();
                    trupattrans.mbushTrupiMagazina(kokaEkzistuese.OMagazinaTransferim.IdKokaMagazina, data);
                    foreach (clsTrupiMagazina trup in trupattrans)
                        if (trup.kaVeprimePas(data))
                        {
                            data.commitTransaksion();
                            return true;
                        }
                }
                colTrupiMagazina trupat = new colTrupiMagazina();
                trupat.mbushTrupiMagazina(kokaEkzistuese.IdKokaMagazina, data);
                foreach (clsTrupiMagazina trup in trupat)
                    if (trup.kaVeprimePas(data))
                    {
                        data.commitTransaksion();
                        return true;
                    }
                data.rollbackTransaksion();
                return false;
            }
            catch (Exception)
            {
                data.rollbackTransaksion();
                return false;
            }
        }

        public clsMesazh kontrolloGjendjeNeFshirje(colTrupiMagazina trupiri, int idstatusdok)
        {
            clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim();
            clsMesazh mesazhi;
            try
            {
                dbRegj.beginTransaksion();
                mesazhi = kontrolloGjendjeNeFshirje(dbRegj, trupiri, idstatusdok);
                if (mesazhi.Status)
                    dbRegj.commitTransaksion();
                else
                    dbRegj.rollbackTransaksion();
                return mesazhi;
            }
            catch (Exception)
            {
                dbRegj.rollbackTransaksion();
                throw;
            }
        }

        public clsMesazh kontrolloGjendjeNeFshirje(clsDatabaseRegjistrim dbRegj, colTrupiMagazina trupiri, int idstatusdok)
        {
            clsMesazh mesazh;
            if ((idstatusdok == 0 && this.idStatusDok == 0))
                return new clsMesazh(true, "Kontrollet u kaluan me sukses!");

            clsDatabaseInventari dbInv = new clsDatabaseInventari(dbRegj);

            bool eshteDalje;

            int idNderm = this.idNdermarrje;
            eshteDalje = this.idLlojDokumentiMagazine == 2 ? true : false;

            mesazh = kontrolloGjendjeNeFshirjeSipasMagazines(dbRegj, dbInv, trupiri, ocolTrupiMagazina, idstatusdok, this.idStatusDok, idNderm, eshteDalje);
            if (!mesazh.Status)
                return mesazh;

            return new clsMesazh(true, "Kontrollet u kaluan me sukses!");
        }

        public clsMesazh kontrolloGjendjeMagazineTrasferim(clsDatabaseRegjistrim dbRegj, clsKokaMagazina kokaMagTransferuar, clsKokaMagazina oMagazinaTransferim, int idstatusdok)
        {
            clsMesazh mesazh;
            if ((idstatusdok == 0 && this.idStatusDok == 0))
                return new clsMesazh(true, "Kontrollet u kaluan me sukses!");

            clsDatabaseInventari dbInv = new clsDatabaseInventari(dbRegj);

            int idNderm = this.idNdermarrje;
            bool eshteDalje = kokaMagTransferuar.idLlojDokumentiMagazine == 2 ? true : false;

            kokaMagTransferuar.mbushTrupMagazine(dbRegj);

            mesazh = kontrolloGjendjeNeFshirjeSipasMagazines(dbRegj, dbInv, oMagazinaTransferim.ocolTrupiMagazina, kokaMagTransferuar.ocolTrupiMagazina, idstatusdok, this.idStatusDok, idNderm, eshteDalje);
            if (!mesazh.Status)
                return mesazh;

            return new clsMesazh(true, "Kontrollet u kaluan me sukses!");
        }

        public clsMesazh kontrolloGjendjeNeFshirjeSipasMagazines(clsDatabaseRegjistrim dbRegj, clsDatabaseInventari dbInv, colTrupiMagazina trupiNew, colTrupiMagazina trupiOld, int idstatusdokNew, int idstatusdokOld, int idNderm, bool eshteDalje)
        {
            clsMesazh mesazh;
            colTrupiMagazina grupimTrupiNew;
            colTrupiMagazina grupimTrupiOld;
            double oldSasi;
            bool kontrollGjendjeDokumenti = (clsAlternativaKushti.getAlternativa(this.idKonfigAmbjente, "KGJA") == "Jo");
           
            //krijojme dy koleksione te reja qe permban totalin per cdo artikull dhe detajim ne menyre qe nese ndonjeri prej tyre eshte me shume se nje here ne gride te mos perseritet kontrolli, si dhe bredhja rresht me rresht per te kapur totalin e sasise se artikullit te behet vetem nje here
            //Kujdes nuk duhet modifikuar kopja origjinale ndaj perdorim ShallowCopy()
            grupimTrupiOld = grupoTrupinSipasArtDheDet(trupiOld, idstatusdokOld);
            grupimTrupiNew = grupoTrupinSipasArtDheDet(trupiNew, idstatusdokNew);

            #region fshiDokument

            //konsiderojme qe dokumenti fshihet edhe nese ai kalon ne status draft
            if (trupiNew.Count == 0 || (idstatusdokNew == 0))
            {
                for (int i = 0; i < grupimTrupiOld.Count; i++)
                {
                    //1. Kontrolli i gjendjes ne kete rast behet vetem kur eshte hyrje me gjendje pozitive ose dalje me gjendje negative
                    if ((eshteDalje && grupimTrupiOld[i].Sasia < 0) || (!eshteDalje && grupimTrupiOld[i].Sasia > 0))
                    {
                        //Nese artikulli ka detajim, kerkohet qe te kontrollohet edhe vete gjendja per artikullin
                        double sasiTotaleArtikulliPaDet = grupimTrupiOld[i].Sasia;
                        if (grupimTrupiOld[i].IdDetajimi != 0 || grupimTrupiOld[i].IdDetajimi2 != 0)
                            sasiTotaleArtikulliPaDet = ktheTotalinArtikullit(trupiNew, grupimTrupiOld[i].IdArtikulli, grupimTrupiOld[i].IdMag);

                        clsTrupiMagazina tempTrupi = (clsTrupiMagazina)grupimTrupiOld[i].ShallowCopy();         //krijojme nje kopje pa ndryshuar origjinalin
                        var mag = clsNjesiAdministrative.ktheMagazineSipasIdNeseEkziston(tempTrupi.IdMag, dbRegj);
                        bool meNdjekjeMagazina = mag != null ? mag.NdjekjeGjendje : false;

                        mesazh = kontrolloGjendjePerRreshtArtikulli(dbRegj, dbInv, tempTrupi, idNderm, eshteDalje, false, false, new DateTime(1900, 1, 1), sasiTotaleArtikulliPaDet, 2, 0, tempTrupi.Sasia, tempTrupi.Sasia, kontrollGjendjeDokumenti, meNdjekjeMagazina);
                        if (!mesazh.Status)
                            return mesazh;
                        continue;
                    }
                }
            }

            #endregion fshiDokument

            #region modifikoDokument

            else
            {
                //A. Bredhim trupin e vjeter per te pare nese ndonje nga rreshtat nuk eshte me ne trupin e ri, ose i eshte ndryshuar magazina, qe nenkupton qe eshte hequr dok. i pare, ndaj behet kontroll per fshirje

                #region trupiVjeter

                // Kujdes nese dokumenti i vjeter ka qene draft nuk jemi te interesuar se cfare ka ndodhur me artikujt e trupit te tij, duke qene se konsiderohet si i ri dhe kontrollohet gjendja vetem per statusin final te artikujve
                if (idstatusdokOld != 0)
                {
                    for (int i = 0; i < grupimTrupiOld.Count; i++)
                    {

                        if (!(grupimTrupiNew.Exists(x => (x.IdArtikulli == grupimTrupiOld[i].IdArtikulli) && (x.IdDetajimi == grupimTrupiOld[i].IdDetajimi) && (x.IdDetajimi2 == grupimTrupiOld[i].IdDetajimi2) && (x.IdMag == grupimTrupiOld[i].IdMag))))
                        {
                            //1. Kontrolli i gjendjes ne kete rast behet vetem kur eshte hyrje me gjendje pozitive ose dalje me gjendje negative
                            if ((eshteDalje && grupimTrupiOld[i].Sasia < 0) || (!eshteDalje && grupimTrupiOld[i].Sasia > 0))
                            {
                                //Nese artikulli ka detajim, kerkohet qe te kontrollohet edhe vete gjendja per artikullin
                                double sasiTotaleArtikulliPaDet = grupimTrupiOld[i].Sasia;
                                if (grupimTrupiOld[i].IdDetajimi != 0 || grupimTrupiOld[i].IdDetajimi2 != 0)
                                    sasiTotaleArtikulliPaDet = ktheTotalinArtikullit(trupiNew, grupimTrupiOld[i].IdArtikulli, grupimTrupiOld[i].IdMag);

                                clsTrupiMagazina tempTrupi = (clsTrupiMagazina)grupimTrupiOld[i].ShallowCopy();         //krijojme nje kopje pa ndryshuar origjinalin
                                var mag = clsNjesiAdministrative.ktheMagazineSipasIdNeseEkziston(grupimTrupiOld[i].IdMag, dbRegj);
                                bool meNdjekjeMagazina = mag != null ? mag.NdjekjeGjendje : false;
                                mesazh = kontrolloGjendjePerRreshtArtikulli(dbRegj, dbInv, tempTrupi, idNderm, eshteDalje, false, false, new DateTime(1900, 1, 1), sasiTotaleArtikulliPaDet, 2, 0, tempTrupi.Sasia, tempTrupi.Sasia, kontrollGjendjeDokumenti, meNdjekjeMagazina);
                                if (!mesazh.Status)
                                    return mesazh;
                            }
                        }
                    }
                }

                #endregion trupiVjeter

                //B. Bredhim ne dokumentin e ri dhe shohim rastet

                #region trupiRi

                for (int i = 0; i < grupimTrupiNew.Count; i++)
                {
                    //Nese artikulli ka detajim, kerkohet qe te kontrollohet edhe vete gjendja per artikullin
                    double sasiTotaleArtikulliPaDet = grupimTrupiNew[i].Sasia;
                    if (grupimTrupiNew[i].IdDetajimi > 0 || grupimTrupiNew[i].IdDetajimi2 > 0)
                    {
                        //double sasiTotaleArtikulliPaDetOld = ktheTotalinArtikullit(trupiOld, grupimTrupiNew[i].IdArtikulli, grupimTrupiNew[i].IdMag);
                        sasiTotaleArtikulliPaDet = ktheTotalinArtikullit(trupiNew, grupimTrupiNew[i].IdArtikulli, grupimTrupiNew[i].IdMag);// - sasiTotaleArtikulliPaDetOld;
                    }
                    var mag = clsNjesiAdministrative.ktheMagazineSipasIdNeseEkziston(grupimTrupiNew[i].IdMag, dbRegj);
                    bool meNdjekjeMagazina = mag != null ? mag.NdjekjeGjendje : false;

                    //1. a. Nese nuk eshte fare ne dokumentin e vjeter, b. ose eshte ndryshuar magazina, c. ose dokumenti i vjeter ishte draft 0 dhe kalon ne dokument te rregullt 1 (pra konsiderohet si rresht i ri)

                    #region Rasti1

                    if ((idstatusdokNew == 1 && idstatusdokOld == 0) || !(grupimTrupiOld.Exists(x => (x.IdArtikulli == grupimTrupiNew[i].IdArtikulli) && (x.IdDetajimi == grupimTrupiNew[i].IdDetajimi) && (x.IdDetajimi2 == grupimTrupiNew[i].IdDetajimi2) && (x.IdMag == grupimTrupiNew[i].IdMag))))
                    {
                        //Kontrolli i gjendjes ne kete rast behet vetem nese dokumenti eshte hyrje me sasi negative, ose dokument dalje me sasi pozitive
                        if ((!eshteDalje && grupimTrupiNew[i].Sasia < 0) || (eshteDalje && grupimTrupiNew[i].Sasia > 0))
                        {
                            // Nqs ka detajim artikulli, duhet te kalohet edhe idkokamagazina, meqe merret sasia totale e artikullit ne te gjithe rreshtat e dokumentit dhe duhet perjashtuar sasia e vjeter e dokumentit aktual.
                            if (grupimTrupiNew[i].IdDetajimi > 0 || grupimTrupiNew[i].IdDetajimi2 > 0)
                            {
                                clsTrupiMagazina tempTrupi = (clsTrupiMagazina)grupimTrupiNew[i].ShallowCopy();         //krijojme nje kopje pa ndryshuar origjinalin
                                tempTrupi.IdKokaMagazina = grupimTrupiOld[0].IdKokaMagazina;
                                mesazh = kontrolloGjendjePerRreshtArtikulli(dbRegj, dbInv, tempTrupi, idNderm, eshteDalje, false, false, new DateTime(1900, 1, 1), sasiTotaleArtikulliPaDet, 0, 0, tempTrupi.Sasia, tempTrupi.Sasia, kontrollGjendjeDokumenti, meNdjekjeMagazina);
                            }
                            else
                                mesazh = kontrolloGjendjePerRreshtArtikulli(dbRegj, dbInv, grupimTrupiNew[i], idNderm, eshteDalje, false, false, new DateTime(1900, 1, 1), sasiTotaleArtikulliPaDet, 0, 0, grupimTrupiNew[i].Sasia, grupimTrupiNew[i].Sasia, kontrollGjendjeDokumenti, meNdjekjeMagazina);
                            if (!mesazh.Status)
                                return mesazh;
                        }
                    }

                    #endregion Rasti1

                    //2. Nese i njejti artikull rezulton te jete perseri ne trupin e dokumentit

                    #region Rasti2

                    else
                    {
                        oldSasi = grupimTrupiOld.Find(x => (x.IdArtikulli == grupimTrupiNew[i].IdArtikulli) && (x.IdDetajimi == grupimTrupiNew[i].IdDetajimi) && (x.IdDetajimi2 == grupimTrupiNew[i].IdDetajimi2) && (x.IdMag == grupimTrupiNew[i].IdMag)).Sasia;
                        clsTrupiMagazina tempTrupi = (clsTrupiMagazina)grupimTrupiNew[i].ShallowCopy();        //krijojme nje kopje pa ndryshuar origjinalin
                        tempTrupi.IdKokaMagazina = grupimTrupiOld[0].IdKokaMagazina;

                        // 2.1 Kur data e dokumentit nuk ndryshon

                        #region Rasti2.1

                        if (trupiNew[0].Data == trupiOld[0].Data)
                        {
                            //Kur sasia ndryshon kontrolli i gjendjes behet nese dokumenti eshte hyrje dhe sasia zvogelohet, pra 1. Hyrje me sasi pozitive ose 2. Dalje me sasi negative ose dokumenti
                            if ((!eshteDalje && grupimTrupiNew[i].Sasia >= 0 && grupimTrupiNew[i].Sasia < oldSasi) || (eshteDalje && grupimTrupiNew[i].Sasia <= 0 && Math.Abs(grupimTrupiNew[i].Sasia) < Math.Abs(oldSasi)))
                            {
                                mesazh = kontrolloGjendjePerRreshtArtikulli(dbRegj, dbInv, tempTrupi, idNderm, eshteDalje, false, false, new DateTime(1900, 1, 1), sasiTotaleArtikulliPaDet, 1, oldSasi, tempTrupi.Sasia, tempTrupi.Sasia, kontrollGjendjeDokumenti, meNdjekjeMagazina);
                                if (!mesazh.Status)
                                    return mesazh;
                                continue;
                            }
                            //Kur sasia ndryshon kontrolli i gjendjes per daljen behet kur dhe sasia rritet, pra 1. Dalje me sasi pozitive ose 2. Hyrje me sasi negative
                            if ((eshteDalje && grupimTrupiNew[i].Sasia >= 0 && grupimTrupiNew[i].Sasia > oldSasi) || (!eshteDalje && grupimTrupiNew[i].Sasia <= 0 && Math.Abs(grupimTrupiNew[i].Sasia) > Math.Abs(oldSasi)))
                            {
                                mesazh = kontrolloGjendjePerRreshtArtikulli(dbRegj, dbInv, tempTrupi, idNderm, eshteDalje, false, false, new DateTime(1900, 1, 1), sasiTotaleArtikulliPaDet, 1, oldSasi, tempTrupi.Sasia, tempTrupi.Sasia, kontrollGjendjeDokumenti, meNdjekjeMagazina);
                                if (!mesazh.Status)
                                    return mesazh;
                                continue;
                            }
                        }

                        #endregion Rasti2.1

                        // 2.2 Kur data e dokumentit ndryshon

                        #region Rasti 2.2

                        else
                        {
                            //HYRJET: Futet ne kontroll vetem nese eshte hyrje me sasi pozitive, ose dalje me sasi negative (qe konsiderohet si hyrje)
                            if ((!eshteDalje && grupimTrupiNew[i].Sasia > 0) || (eshteDalje && grupimTrupiNew[i].Sasia < 0))
                            {
                                // 2.2.1 Kur eshte hyrje dhe data shtyhet dhe sasia rritet interesohemi vetem per periudhen mes dy datave qe te mos shkoje me gjendje negative
                                if (trupiNew[0].Data > trupiOld[0].Data && Math.Abs(grupimTrupiNew[i].Sasia) >= Math.Abs(oldSasi))
                                {
                                    mesazh = kontrolloGjendjePerRreshtArtikulli(dbRegj, dbInv, tempTrupi, idNderm, eshteDalje, true, true, trupiOld[0].Data, sasiTotaleArtikulliPaDet, 1, oldSasi, tempTrupi.Sasia, tempTrupi.Sasia, kontrollGjendjeDokumenti, meNdjekjeMagazina);
                                    if (!mesazh.Status)
                                        return mesazh;
                                    continue;
                                }
                                // 2.2.2 Kur eshte hyrje dhe data shtyhet dhe sasia zvogelohet, interesohemi qe asnjhere te mos shkoje gjendja negative, nga data e veprimit te vjeter
                                if (trupiNew[0].Data > trupiOld[0].Data && Math.Abs(grupimTrupiNew[i].Sasia) < Math.Abs(oldSasi))
                                {
                                    mesazh = kontrolloGjendjePerRreshtArtikulli(dbRegj, dbInv, tempTrupi, idNderm, eshteDalje, true, false, trupiOld[0].Data, sasiTotaleArtikulliPaDet, 1, oldSasi, tempTrupi.Sasia, tempTrupi.Sasia, kontrollGjendjeDokumenti, meNdjekjeMagazina);
                                    if (!mesazh.Status)
                                        return mesazh;
                                    continue;
                                }
                                // 2.2.3 Kur eshte hyrje dhe data avancohet dhe sasia zvogelohet, pra kur kemi 1. Hyrje me sasi pozitive ose 2. Dalje me sasi negative
                                if (trupiNew[0].Data < trupiOld[0].Data && Math.Abs(grupimTrupiNew[i].Sasia) < Math.Abs(oldSasi))
                                {
                                    mesazh = kontrolloGjendjePerRreshtArtikulli(dbRegj, dbInv, tempTrupi, idNderm, eshteDalje, true, false, trupiOld[0].Data, sasiTotaleArtikulliPaDet, 1, oldSasi, tempTrupi.Sasia, tempTrupi.Sasia, kontrollGjendjeDokumenti, meNdjekjeMagazina);
                                    if (!mesazh.Status)
                                        return mesazh;
                                    continue;
                                }
                                continue;
                            }

                            //DALJET: Futet ne kontroll vetem nese eshte dalje me sasi pozitive, ose hyrje me sasi negative (qe konsiderohet si dalje)
                            if ((eshteDalje && grupimTrupiNew[i].Sasia > 0) || (!eshteDalje && grupimTrupiNew[i].Sasia < 0))
                            {
                                // 2.2.4 Kur eshte dalje dhe data avancohet dhe sasia e daljes nuk ndryshon ose zgjogelohet, interesohemi vetem per periudhen mes dy datave qe te mos shkoje me vlere negative
                                if (trupiNew[0].Data < trupiOld[0].Data && Math.Abs(grupimTrupiNew[i].Sasia) <= Math.Abs(oldSasi))
                                {
                                    mesazh = kontrolloGjendjePerRreshtArtikulli(dbRegj, dbInv, tempTrupi, idNderm, eshteDalje, true, true, trupiOld[0].Data, sasiTotaleArtikulliPaDet, 1, oldSasi, tempTrupi.Sasia, tempTrupi.Sasia, kontrollGjendjeDokumenti, meNdjekjeMagazina);
                                    if (!mesazh.Status)
                                        return mesazh;
                                    continue;
                                }
                                // 2.2.5 Kur eshte dalje dhe data avancohet dhe sasia e daljes rritet, interesohemi qe te mos gjeneroje asnjehere gjendje negative duket filluar nga data e dokumentit te ri
                                if (trupiNew[0].Data < trupiOld[0].Data && Math.Abs(grupimTrupiNew[i].Sasia) > Math.Abs(oldSasi))
                                {
                                    mesazh = kontrolloGjendjePerRreshtArtikulli(dbRegj, dbInv, tempTrupi, idNderm, eshteDalje, true, false, trupiOld[0].Data, sasiTotaleArtikulliPaDet, 1, oldSasi, tempTrupi.Sasia, tempTrupi.Sasia, kontrollGjendjeDokumenti, meNdjekjeMagazina);
                                    if (!mesazh.Status)
                                        return mesazh;
                                    continue;
                                }
                                // 2.2.6 Kur eshte dalje dhe data e dokumentit te ri shtyhet, interesohemi vetem ne rastin kur sasia e daljes rritet per te pare nese gjeneron gjendje negative ne dok e mevonshme
                                if (trupiNew[0].Data > trupiOld[0].Data && Math.Abs(grupimTrupiNew[i].Sasia) > Math.Abs(oldSasi))
                                {
                                    mesazh = kontrolloGjendjePerRreshtArtikulli(dbRegj, dbInv, tempTrupi, idNderm, eshteDalje, true, false, trupiOld[0].Data, sasiTotaleArtikulliPaDet, 1, oldSasi, tempTrupi.Sasia, tempTrupi.Sasia, kontrollGjendjeDokumenti, meNdjekjeMagazina);
                                    if (!mesazh.Status)
                                        return mesazh;
                                    continue;
                                }
                                continue;
                            }
                        }

                        #endregion Rasti 2.2
                    }

                    #endregion Rasti2
                }

                #endregion trupiRi
            }//fund lloji modifikim

            #endregion modifikoDokument

            return new clsMesazh(true, "Kontrollet per gjendjen ne magazine u kaluan me sukses!");
        }

        public colTrupiMagazina grupoTrupinSipasArtDheDet(colTrupiMagazina tempTrupi, int idstatusdok)
        {
            colTrupiMagazina grupimTrupi = new colTrupiMagazina();
            double sasiaRresht;

            for (int i = 0; i < tempTrupi.Count; i++)
            {
                //krijon nje kopje pa modifikuar origjinalin
                clsTrupiMagazina tempRresht = (clsTrupiMagazina)tempTrupi[i].ShallowCopy();

                if (tempTrupi[i].IdDetajimi == -1)
                    tempRresht.IdDetajimi = 0;
                if (tempTrupi[i].IdDetajimi2 == -1)
                    tempRresht.IdDetajimi2 = 0;

                // nese statusi i dokumentit eshte 0 ath nuk interesohemi per sasine ne kontrollin e gjendjes.
                if (idstatusdok == 0)
                {
                    sasiaRresht = 0;
                }
                else
                {
                    sasiaRresht = tempRresht.Sasia * tempRresht.Koeficenti;
                }

                if (grupimTrupi.Exists(x => (x.IdArtikulli == tempRresht.IdArtikulli) && (x.IdDetajimi == tempRresht.IdDetajimi) && (x.IdDetajimi2 == tempRresht.IdDetajimi2) && (x.IdMag == tempRresht.IdMag)))
                {
                    grupimTrupi.Find(x => (x.IdArtikulli == tempRresht.IdArtikulli) && (x.IdDetajimi == tempRresht.IdDetajimi) && (x.IdDetajimi2 == tempRresht.IdDetajimi2) && (x.IdMag == tempRresht.IdMag)).Sasia += sasiaRresht;
                }
                else
                {
                    tempRresht.Sasia = sasiaRresht;
                    grupimTrupi.Add(tempRresht);
                }
            }

            for (int i = 0; i < grupimTrupi.Count; i++)
            {
                grupimTrupi[i].Sasia = Math.Round(grupimTrupi[i].Sasia, 10);
            }
            return grupimTrupi;
        }

        public clsMesazh kontrolloGjendjePerRreshtArtikulli(clsDatabaseRegjistrim dbRegj, clsDatabaseInventari dbInv, clsTrupiMagazina rreshtTrupi, int idNderm, bool eshteDalje, bool meDyData, bool kapDokMesDatave, DateTime data2, double sasiTotArtikull, int llojVeprimi, double sasiArtikulliOld, double sasidet1, double sasidet2, bool kontrollGjendjeDokumenti, bool meNdjekjeMagazina)
        {
            clsMesazh mesazh;

            if (kontrollGjendjeDokumenti) {
                return new clsMesazh(true, "Kontrollet u kaluan me sukses!");
            }

            if(!meNdjekjeMagazina)
                return new clsMesazh(true, "Kontrollet u kaluan me sukses!");

            double totaliArtikullit = rreshtTrupi.Sasia;


            //Shto Artikull llojVeprimi=0, Modifiko Artikull llojVeprimi=1, Fshi Artikull llojVeprimi=2

            DbInventari.clsArtikulli artikulli = rreshtTrupi.Element != null ? (DbInventari.clsArtikulli)rreshtTrupi.Element : new DbInventari.clsArtikulli(rreshtTrupi.IdArtikulli, dbInv);
            if (artikulli.KontrollGjendjeArtikulli)
                if (!dbRegj.lejonGjendjeNegative(this.idKonfigAmbjente, rreshtTrupi.IdMag, rreshtTrupi.IdArtikulli)) //kontrollon nese lejon gjendje negative per detajimin e pare
                {
                    mesazh = kontrollgjendjeModifikim(dbRegj, artikulli, idNderm, rreshtTrupi.IdMag, meDyData, rreshtTrupi.Data, data2, kapDokMesDatave, rreshtTrupi.IdKokaMagazina, eshteDalje, sasiTotArtikull, 0, 0, "", llojVeprimi, sasiArtikulliOld);
                    if (!mesazh.Status)
                        return mesazh;
                }
            if (rreshtTrupi.IdDetajimi > 0)
            {
                if (artikulli.KontrollGjendje && !dbRegj.lejonGjendjeNegativeDetPare(this.idKonfigAmbjente, rreshtTrupi.IdMag, rreshtTrupi.IdArtikulli))
                {
                    DbInventari.clsDetajimArtikulli detArt = new DbInventari.clsDetajimArtikulli(rreshtTrupi.IdDetajimi, dbInv);
                    mesazh = kontrollgjendjeModifikim(dbRegj, artikulli, idNderm, rreshtTrupi.IdMag, meDyData, rreshtTrupi.Data, data2, kapDokMesDatave, rreshtTrupi.IdKokaMagazina, eshteDalje, sasidet1, 1, rreshtTrupi.IdDetajimi, detArt.KodDetajimArtikulli, llojVeprimi, sasiArtikulliOld);
                    if (!mesazh.Status)
                        return mesazh;
                }
            }
            //end lejonGjendjeNegative

            if (artikulli.KontrollGjendjeDetajim2 && !dbRegj.lejonGjendjeNegativeDetDyte(this.idKonfigAmbjente, rreshtTrupi.IdMag, rreshtTrupi.IdArtikulli))
            {
                if (rreshtTrupi.IdDetajimi2 > 0)    //Nqs ka zgjedhur detajim ne kete rresht te trupit
                {
                    DbInventari.clsDetajimArtikulli detArt = new DbInventari.clsDetajimArtikulli(rreshtTrupi.IdDetajimi2, dbInv);
                    mesazh = kontrollgjendjeModifikim(dbRegj, artikulli, idNderm, rreshtTrupi.IdMag, meDyData, rreshtTrupi.Data, data2, kapDokMesDatave, rreshtTrupi.IdKokaMagazina, eshteDalje, sasidet2, 2, rreshtTrupi.IdDetajimi2, detArt.KodDetajimArtikulli, llojVeprimi, sasiArtikulliOld);
                    if (!mesazh.Status)
                        return mesazh;
                }
            }  //end lejonGjendjeNegativeDetDyte

            return new clsMesazh(true, "Kontrollet u kaluan me sukses!");
        }
        
        public clsMesazh kontrollgjendjeModifikim(clsDatabaseRegjistrim dbRegj, DbInventari.clsArtikulli artikulli, int idNderm, int idmag, bool meDyData, DateTime data1, DateTime data2, bool kapDokMesDatave, int idKokaDokument, bool eshteDalje, double sasiArtikulli, int meMetajim, int idDet, string kodDet, int llojVeprimi, double sasiArtikulliOld)
        {
            return dbRegj.kontrolloTeTeraModifikim(artikulli.IdArtikulli, artikulli.KodArtikulli, artikulli.MetodeKostojeArtikulli, idNderm, idmag, meDyData, data1, data2, kapDokMesDatave, idKokaDokument, eshteDalje, sasiArtikulli, meMetajim, idDet, kodDet, llojVeprimi, sasiArtikulliOld);
        }

        public clsMesazh kontrollgjendje(DbInventari.clsArtikulli artikulli, int idmag, DateTime data, int idrenditje, int iddetajim, double gjendjeartikulli, double gjendjedetajimi, int llojDetajimi, bool dalje, double shtimgjendjemod, double shtimgjendjedetmod)
        {
            using (clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim())
            {
                return kontrollgjendje(artikulli, idmag, data, idrenditje, iddetajim, gjendjeartikulli, gjendjedetajimi, dbRegj, llojDetajimi, dalje, shtimgjendjemod, shtimgjendjedetmod);
            }
        }

        public clsMesazh kontrollgjendje(DbInventari.clsArtikulli artikulli, int idmag, DateTime data, int idrenditje, int iddetajim, double gjendjeartikulli, double gjendjedetajimi, clsDatabaseRegjistrim dbRegj, int llojDetajimi, bool dalje, double shtimgjendjemod, double shtimgjendjedetmod)
        {
            DbInventari.clsDatabaseInventari dbinv = new DbInventari.clsDatabaseInventari(dbRegj);
            double gjendjaArtikullitNeMagazine = 0;
            if (artikulli.MetodeKostojeArtikulli == 1 || artikulli.MetodeKostojeArtikulli == 5)   //cmim mesatar hyrje para dalje
                gjendjaArtikullitNeMagazine = dbRegj.ktheSasineTotaleSipasArtikullitHPD(artikulli.IdArtikulli, idmag, data, idrenditje);
            else if (artikulli.MetodeKostojeArtikulli == 2) //cmim mesatar sipas id se renditjes
                gjendjaArtikullitNeMagazine = dbRegj.ktheSasineTotaleSipasArtikullitSipasRadhes(artikulli.IdArtikulli, idmag, data, idrenditje);
            else //progresiv apo fifo
            {
                gjendjaArtikullitNeMagazine = dbRegj.ktheSasineProgresiveArtikullitEdheSipasDetajimit(artikulli.IdArtikulli, 0, idmag, data, idrenditje);
            }
            gjendjaArtikullitNeMagazine += shtimgjendjemod;
            if (dalje)
            {
                if (gjendjeartikulli > gjendjaArtikullitNeMagazine)
                    return new clsMesazh(false, "Per artikullin: " + artikulli.KodArtikulli + " gjenerohet gjendje negative ne daten " + data.ToShortDateString() + ". Gjendja ne kete date eshte " + gjendjaArtikullitNeMagazine + "!");
            }
            else
                if (Math.Abs(gjendjeartikulli) > gjendjaArtikullitNeMagazine)
                return new clsMesazh(false, "Per artikullin: " + artikulli.KodArtikulli + " gjenerohet gjendje negative ne daten " + data.ToShortDateString() + ". Gjendja ne kete date eshte " + gjendjaArtikullitNeMagazine + "!");
            //break;
            if (llojDetajimi == 1)
            {
                if (artikulli.KontrollGjendje == true)
                {
                    if (iddetajim != -1 && iddetajim != 0)//Nqs ka zgjedhur detajim ne kete rresht te trupit
                    {
                        double sasiaProgresiveDetajimit = 0;
                        if (artikulli.MetodeKostojeArtikulli == 1 || artikulli.MetodeKostojeArtikulli == 5)   //cmim mesatar hyrje para dalje
                            sasiaProgresiveDetajimit = dbRegj.ktheSasineTotaleSipasDetajimitHPD(artikulli.IdArtikulli, iddetajim, idmag, data, idrenditje);
                        else if (artikulli.MetodeKostojeArtikulli == 2) //cmim mesatar sipas id se renditjes
                            sasiaProgresiveDetajimit = dbRegj.ktheSasineTotaleSipasDetajimitSipasRadhes(artikulli.IdArtikulli, iddetajim, idmag, data, idrenditje);
                        else    //fifo    apo progresiv
                        {
                            sasiaProgresiveDetajimit = dbRegj.ktheSasineProgresiveArtikullitEdheSipasDetajimit(artikulli.IdArtikulli, iddetajim, idmag, data, idrenditje);
                        }
                        sasiaProgresiveDetajimit += shtimgjendjedetmod;
                        if (dalje)
                        {
                            if (gjendjedetajimi > sasiaProgresiveDetajimit)
                            {
                                DbInventari.clsDetajimArtikulli detArt = new DbInventari.clsDetajimArtikulli(iddetajim, dbinv);
                                return new clsMesazh(false, "Per artikullin: " + artikulli.KodArtikulli + " me detajim: " + detArt.KodDetajimArtikulli + "gjenerohet gjendje negative ne daten " + data.ToShortDateString() + ". Gjendja ne kete date eshte " + sasiaProgresiveDetajimit + "!");
                            }
                        }
                        else
                            if (Math.Abs(gjendjedetajimi) > sasiaProgresiveDetajimit)
                        {
                            DbInventari.clsDetajimArtikulli detArt = new DbInventari.clsDetajimArtikulli(iddetajim, dbinv);
                            return new clsMesazh(false, "Per artikullin: " + artikulli.KodArtikulli + " me detajim: " + detArt.KodDetajimArtikulli + "gjenerohet gjendje negative ne daten " + data.ToShortDateString() + ". Gjendja ne kete date eshte " + sasiaProgresiveDetajimit + "!");
                        }
                    }
                }
            }
            else if (llojDetajimi == 2)
            {
                if (artikulli.KontrollGjendjeDetajim2 == true)
                {
                    if (iddetajim != -1 && iddetajim != 0)//Nqs ka zgjedhur detajim ne kete rresht te trupit
                    {
                        double sasiaDetajimitDyte = 0;
                        if (artikulli.MetodeKostojeArtikulli == 1 || artikulli.MetodeKostojeArtikulli == 5)   //cmim mesatar hyrje para dalje
                            sasiaDetajimitDyte = dbRegj.ktheSasineTotaleSipasDetajimitTeDyteHPD(artikulli.IdArtikulli, iddetajim, idmag, data, idrenditje);
                        else if (artikulli.MetodeKostojeArtikulli == 2) //cmim mesatar sipas id se renditjes
                            sasiaDetajimitDyte = dbRegj.ktheSasineTotaleSipasDetajimitTeDyteSipasRadhes(artikulli.IdArtikulli, iddetajim, idmag, data, idrenditje);
                        //else    //fifo    apo progresiv
                        //{
                        //    sasiaProgresiveDetajimit = dbRegj.ktheSasineProgresiveSipasDetajimit(artikulli.IdArtikulli, iddetajim, idmag, data, idrenditje);
                        //}
                        sasiaDetajimitDyte += shtimgjendjedetmod;
                        if (dalje)
                        {
                            if (gjendjedetajimi > sasiaDetajimitDyte)
                            {
                                DbInventari.clsDetajimArtikulli detArt = new DbInventari.clsDetajimArtikulli(iddetajim, dbinv);
                                return new clsMesazh(false, "Per artikullin: " + artikulli.KodArtikulli + " me detajim: " + detArt.KodDetajimArtikulli + "gjenerohet gjendje negative ne daten " + data.ToShortDateString() + ". Gjendja ne kete date eshte " + sasiaDetajimitDyte + "!");
                            }
                        }
                        else if (Math.Abs(gjendjedetajimi) > sasiaDetajimitDyte)
                        {
                            DbInventari.clsDetajimArtikulli detArt = new DbInventari.clsDetajimArtikulli(iddetajim, dbinv);
                            return new clsMesazh(false, "Per artikullin: " + artikulli.KodArtikulli + " me detajim: " + detArt.KodDetajimArtikulli + "gjenerohet gjendje negative ne daten " + data.ToShortDateString() + ". Gjendja ne kete date eshte " + sasiaDetajimitDyte + "!");
                        }
                    }
                }
            }
            return new clsMesazh(true);
        }

        public bool rivleresimPas()
        {
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            clsKokaMagazina kokaEkzistuese = new clsKokaMagazina();
            kokaEkzistuese.mbushKokaMagazinaSipasID(idKokaMagazina);
            kokaEkzistuese.OMagazinaTransferim = new clsKokaMagazina();
            kokaEkzistuese.OMagazinaTransferim.IdGjenerues = kokaEkzistuese.IdKokaMagazina;
            kokaEkzistuese.OMagazinaTransferim.mbushKokaMagazinaSipasIDGjenerues(kokaEkzistuese.OMagazinaTransferim.IdGjenerues, 1, kokaEkzistuese.IdKonfigAmbjente, data);
            if (kokaEkzistuese.OMagazinaTransferim.IdKokaMagazina != 0)
            {
                colTrupiMagazina trupattrans = new colTrupiMagazina();
                trupattrans.mbushTrupiMagazina(kokaEkzistuese.OMagazinaTransferim.IdKokaMagazina, data);
                foreach (clsTrupiMagazina trup in trupattrans)
                    if (trup.kaVeprimePas())
                        return true;
            }
            colTrupiMagazina trupat = new colTrupiMagazina();
            trupat.mbushTrupiMagazina(kokaEkzistuese.IdKokaMagazina, data);
            foreach (clsTrupiMagazina trup in trupat)
                if (trup.kaVeprimePas())
                    return true;
            return false;
        }

        /// <summary>
        /// Fshin objektin e  kokes se dokumentit te magazines ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.fshiKokaMagazina"/>
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh fshi()
        {
            clsKokaMagazina data = new clsKokaMagazina();
            using (var scope = new MyTransactionScope())
            {
                clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim();
                //dbRegj.krijoManager();
                // dbRegj.beginTransaksion();
                bool kaveprimepas = false;
                clsMesazh u_fshi = data.fshiMagazina(this.IdKokaMagazina, this.idPerdoruesi, dbRegj, true, true, false, new colSerialetMagazine(), out kaveprimepas, true);
                if (u_fshi.Status)
                {
                    // dbRegj.commitTransaksion();
                    scope.Complete();
                }

                //  else
                // dbRegj.rollbackTransaksion();
                return u_fshi;
            }
        }

        /////// <summary>
        ///// sherben per te gjeneruar nje objekt te trupit te fletes kontabel sipas te dhenave
        ///// </summary>
        ///// <param name="debikredi">tregon nese trupi eshte prekur ne debi apo ne kredi </param>
        ///// <param name="vlera"> vlefta me te cilen eshte prekur llogaria</param>
        ///// <param name="oLlogari"> llogaria e trupit</param>
        ///// <returns> kthen nje obj clsTrupiFleteKontabel me nje rresht te trupit te fletes kontabel</returns>
        //[Obsolete("perdor: public clsTrupiFleteKontabel(int debikredi, double vlera, clsLlogari oLlogari, bool azhornim, int idndermarje, Nullable<Double> kursi, Nullable<Int32> idMonedha)")]
        //    public clsTrupiFleteKontabel krijoObjektTrupiFleteKontabelMagDalje(int debikredi, double vlera, clsLlogari oLlogari, bool azhornim, int idndermarje)
        //    {
        //        clsTrupiFleteKontabel trupiFK = new clsTrupiFleteKontabel();
        //        trupiFK.NrLlogari = oLlogari.NrLlogari;
        //        trupiFK.IdLlogari = oLlogari.IdLlogari;
        //        trupiFK.EmerLlogari = oLlogari.EmerLlogari1;
        //        DbAdmin.clsMonedha monKF = new DbAdmin.clsMonedha();
        //        if (azhornim)
        //        {
        //            DbAdmin.clsNdermarrje nd = new DbAdmin.clsNdermarrje(idndermarje);
        //            monKF.IdMonedha = nd.NdermarrjeMonedha;
        //            monKF = monKF.merr();
        //            monKF.mbushMonedhen(monKF.KodiMonedha, idndermarje);
        //        }
        //        else
        //        {
        //            monKF.IdMonedha = oLlogari.IdMonedha;
        //            monKF = monKF.merr();
        //        }
        //        trupiFK.IdMonedha = monKF.IdMonedha;
        //        trupiFK.KodMonedha = monKF.KodiMonedha;
        //        if (azhornim)
        //            trupiFK.Kursi = 1;
        //        else
        //        {
        //            DbAdmin.colKurset kurset = new DbAdmin.colKurset();
        //            kurset.mbushKursetFunditMonedhesSipasLlojit(trupiFK.IdMonedha, 1);
        //            if (kurset.Count > 0)
        //                trupiFK.Kursi = double.Parse(kurset[0].VleraKursi.ToString());
        //            else
        //                trupiFK.Kursi = 1;
        //        }

        //        trupiFK.KodiSkemaKontabel = "";
        //        trupiFK.PershkrimTrupiFleteKontabel = "";
        //        if (debikredi == 1) //rasti kur preket llogaria ne debi
        //        {
        //            if (azhornim)
        //                trupiFK.VleftaDebiTrupiFleteKontabel = 0;
        //            else
        //                trupiFK.VleftaDebiTrupiFleteKontabel = vlera / trupiFK.Kursi;
        //            trupiFK.VleftaKrediTrupiFleteKontabel = 0;
        //            trupiFK.VleftaKrediMonBazeTrupiFleteKontabel = 0;
        //            trupiFK.VleftaDebiMonBazeTrupiFleteKontabel = vlera;
        //            trupiFK.DK = "D";
        //        }
        //        else if (debikredi == 2) //rasti kur preket llogaria ne kredi
        //        {
        //            trupiFK.VleftaDebiTrupiFleteKontabel = 0;
        //            if (azhornim)
        //                trupiFK.VleftaKrediTrupiFleteKontabel = 0;
        //            else trupiFK.VleftaKrediTrupiFleteKontabel = vlera / trupiFK.Kursi;
        //            trupiFK.VleftaKrediMonBazeTrupiFleteKontabel = vlera;
        //            trupiFK.VleftaDebiMonBazeTrupiFleteKontabel = 0;
        //            trupiFK.DK = "K";
        //        }
        //        return trupiFK;
        //    }

        /// <summary>
        /// perdoret per te gjetur totalin e sasise te nje artikulli ne trupin e nje dokumenti magazine per te kontrolluar me pas gjendjen e tij ne krahasim me sasine qe do dale nga magazina
        /// </summary>
        /// <param name="trupi"> koleksion me rreshta te trupit te nje dokumenti magazine</param>
        /// <param name="idArtikulli"> id e artikulli per te cilin do te gjejme totalin</param>
        /// <param name="idMagazina">id-ja e magazines</param>
        /// <returns> kthen totalin e sasise te artikullit</returns>
        public double ktheTotalinArtikullit(colTrupiMagazina trupi, int idArtikulli, int idMagazina)
        {
            //double newTotali = 0;
            double totali = 0;
            //System.Diagnostics.Stopwatch myWatch1 = new System.Diagnostics.Stopwatch();
            //myWatch1.Start();
            foreach (clsTrupiMagazina o in trupi)
            {
                if (o.IdArtikulli == idArtikulli && o.IdMag == idMagazina)
                    totali += o.Sasia * o.Koeficenti;
            }

            totali = Math.Round(totali, 10);
            //myWatch1.Stop();
            //System.Diagnostics.Trace.WriteLine("myWatch1: " + myWatch1.Elapsed + "; Totali:" + totali);
            return Math.Round(totali, 10);
        }
        public bool shtoNivfshTeMagazina(int idNdermarrje, int idDokumenti, string nivfsh)
        {
            using (clsDatabaseRegjistrim db = new clsDatabaseRegjistrim())
                return db.shtoNivfshTeMagazina(idNdermarrje, idDokumenti, nivfsh);
        }
        public bool shtoWTNICTeMagazina(int idNdermarrje, int idDokumenti, string wtnic)
        {
            using (clsDatabaseRegjistrim db = new clsDatabaseRegjistrim())
                return db.shtoWTNICTeMagazina(idNdermarrje, idDokumenti, wtnic);
        }

        //public double ktheTotalinArtikullitMeShenje(colTrupiMagazina trupi, int idArtikulli, int idMagazina)
        //{
        //    double totali = 0;
        //    foreach (clsTrupiMagazina o in trupi)
        //    {
        //        if (o.IdArtikulli == idArtikulli && o.IdMag == idMagazina)
        //            totali += o.Sasia * o.Koeficenti * o.Shenja;
        //    }
        //    return Math.Round(totali, 10);
        //}

        public double ktheTotalinArtikullitDetajim(colTrupiMagazina trupi, int idArtikulli, int iddetajim, int idMagazina)
        {
            double totali = 0;
            if (iddetajim < 1)
                return totali;
            foreach (clsTrupiMagazina o in trupi)
            {
                if (o.IdArtikulli == idArtikulli && o.IdDetajimi == iddetajim && o.IdMag == idMagazina)
                    totali += o.Sasia * o.Koeficenti;
            }
            return Math.Round(totali, 10);
        }

        public double ktheTotalinArtikullitDetajimDyte(colTrupiMagazina trupi, int idArtikulli, int iddetajim, int idMagazina)
        {
            double totali = 0;
            if (iddetajim < 1)
                return totali;
            foreach (clsTrupiMagazina o in trupi)
            {
                if (o.IdArtikulli == idArtikulli && o.IdDetajimi2 == iddetajim && o.IdMag == idMagazina)
                    totali += o.Sasia * o.Koeficenti;
            }
            return Math.Round(totali, 10);
        }
        /// <summary>
        /// Merr trupin  e  nje dokumenti te magazines nga tabela perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.ktheTrupiMagazina"/>
        /// </summary>
        /// <returns > nje object colTrupiMagazina qe permban nje koleksion me trupin e dokumentit te magazines</returns>
        public bool mbushTrupMagazine(clsDatabaseRegjistrim db)
        {
            ocolTrupiMagazina = new colTrupiMagazina();
            return ocolTrupiMagazina.mbushTrupiMagazina(IdKokaMagazina, db);
        }

        public static string merrNgjyreKonvertime(int idndermarje, int idkoka)
        {
            clsDatabaseRegjistrim dbKokeShitje = new clsDatabaseRegjistrim();
            string mbush = dbKokeShitje.merrNgjyreKonvertimePerMagazine(idndermarje, idkoka);
            dbKokeShitje.Dispose();
            return mbush;
        }

        public static DataTable merrArtikujTeKonvertuarPlotesisht(int idShitjeKoka, int idNdermarje, bool ngaShitja)
        {
            using (clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim())
            {
                return dbRegj.merrArtikujTeKonvertuarPlotesishtPerMagazinen(idShitjeKoka, idNdermarje, ngaShitja);
            }
        }

        /// <summary>
        /// Merr trupin  e  nje dokumenti te magazines nga tabela perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.ktheTrupiMagazina"/>
        /// </summary>
        /// <returns > nje object colTrupiMagazina qe permban nje koleksion me trupin e dokumentit te magazines</returns>
        ///<param name="PerjashtoArtAfgj">Ne rastet qe do te jete true nuk do te marri artikujt afategjate</param>

        public bool mbushTrupMagazine(bool PerjashtoArtAfgj)
        {
            ocolTrupiMagazina = new colTrupiMagazina();
            return ocolTrupiMagazina.mbushTrupiMagazina(IdKokaMagazina, PerjashtoArtAfgj);
        }

        /// <summary>
        /// mbush koken e magazines sipas id gjenerues
        /// </summary>
        /// <param name="idGjenerues">id e gjeneruesit</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        /// <param name="lloji">lloji 1-hyrje 2-dalje</param>
        /// <param name="idkonfiggjenerues"></param>
        public bool mbushKokaMagazinaSipasIDGjenerues(int idGjenerues, int lloji, int idkonfiggjenerues, clsDatabaseRegjistrim db)
        {
            return mbushKokaMagazina(db.ktheKokaMagazinaSipasIDGjenerues(idGjenerues, lloji, idkonfiggjenerues), db);
        }

        /// <summary>
        /// mbush koken e magazines sipas id gjenerues
        /// </summary>
        /// <param name="idGjenerues">id e gjeneruesit</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        /// <param name="lloji">lloji 1-hyrje 2-dalje</param>
        /// <param name="idkonfiggjenerues"></param>
        public bool mbushKokaMagazinaSipasIDGjenerues(int idGjenerues, int lloji, int idkonfiggjenerues)
        {
            clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();
            bool sukses = mbushKokaMagazina(db.ktheKokaMagazinaSipasIDGjenerues(idGjenerues, lloji, idkonfiggjenerues), db);
            db.Dispose();
            return sukses;
        }

        /// <summary>
        /// mbush koken e magazines sipas id dokNga
        /// </summary>
        /// <param name="idDokNga">id dokumenti nga</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        public bool mbushKokaMagazinaSipasIDDokNga(int iddokNga)
        {
            clsDatabaseRegjistrim dbKokaMagazina = new clsDatabaseRegjistrim();
            bool sukses = mbushKokaMagazina(dbKokaMagazina.ktheKokaMagazinaSipasIDDokNga(iddokNga), dbKokaMagazina);
            dbKokaMagazina.Dispose();
            return sukses;
        }
        
        /// <summary>
        /// mbush koken e magazines sipas id se kokes
        /// </summary>
        /// <param name="idKokaMagazina">id e kokes se magazines</param>
        /// <returns>kthen true nese mbushja kryhet me sukses ne te kundert kthen false</returns>
        /// <param name="dbKokaMagazina"></param>
        public bool mbushKokaMagazinaSipasID(int idKokaMagazina, clsDatabaseRegjistrim dbKokaMagazina)
        {
            return mbushKokaMagazina(dbKokaMagazina.ktheKokaMagazinaSipasID(idKokaMagazina), dbKokaMagazina);
        }

        public bool merrKokaMagazinaSipasIdKonfigNrdokDtdok(int idKonfig, String nrdok, DateTime dtdok)
        {
            using (clsDatabaseRegjistrim dbKokaMagazina = new clsDatabaseRegjistrim())
               return mbushKokaMagazina(dbKokaMagazina.ktheKokaMagazinaSipasIdKonfigNrDokDtDok(idKonfig, nrdok, dtdok), dbKokaMagazina);
        }
        /// <summary>
        /// mbush koken e magazines sipas id se kokes
        /// </summary>
        /// <param name="idKokaMagazina">id e kokes se magazines</param>
        /// <returns>kthen true nese mbushja kryhet me sukses ne te kundert kthen false</returns>
        /// <param name="dbKokaMagazina"></param>
        public bool mbushKokaMagazinaSipasID(int idKokaMagazina)
        {
            using (clsDatabaseRegjistrim dbKokaMagazina = new clsDatabaseRegjistrim())
                return mbushKokaMagazina(dbKokaMagazina.ktheKokaMagazinaSipasID(idKokaMagazina), dbKokaMagazina);
        }

        public static bool lejonGjendjeNegative(int idKonfigAmbjente, int idMag, int idArt)
        {
            using (clsDatabaseRegjistrim db = new clsDatabaseRegjistrim())
                return db.lejonGjendjeNegative(idKonfigAmbjente, idMag, idArt);

        }

        public static bool lejonGjendjeNegativeDetDyte(int idKonfigAmbjente, int idMag, int idArt)
        {
            using (clsDatabaseRegjistrim db = new clsDatabaseRegjistrim())
                return db.lejonGjendjeNegativeDetDyte(idKonfigAmbjente, idMag, idArt);

        }
        public static bool lejonGjendjeNegativeDetPare(int idKonfigAmbjente, int idMag, int idArt)
        {
            using (clsDatabaseRegjistrim db = new clsDatabaseRegjistrim())
                return db.lejonGjendjeNegativeDetPare(idKonfigAmbjente, idMag, idArt);


        }

        public static bool kaAutorizime(int idkokamagazina, int idperdoruesi)
        {
            clsDatabaseRegjistrim dbKokaMagazina = new clsDatabaseRegjistrim();
            bool sukses = dbKokaMagazina.kaAutorizimKokaMagazina(idkokamagazina, idperdoruesi);
            dbKokaMagazina.Dispose();
            return sukses;
        }
        public static bool KaFAFSeriali(int idkokamagazina)
        {
            clsDatabaseRegjistrim dbKokaMagazina = new clsDatabaseRegjistrim();
            bool sukses = dbKokaMagazina.KaFAFSeriali(idkokamagazina);
            dbKokaMagazina.Dispose();
            return sukses;
        }

        public static int merrKrijuesin(String nrdok, int idnderm, DateTime data, String lloji, clsDatabaseRegjistrim dbKokaMagazina)
        {
            int idkrijuesi = dbKokaMagazina.merrKrijuesin(nrdok, idnderm, data, lloji, 0);

            return idkrijuesi;
        }

        public static int merrIdStatusDok(int idKokaMag)
        {
            using (clsDatabaseRegjistrim dbKokaMagazina = new clsDatabaseRegjistrim())
            {
                return dbKokaMagazina.merrIdStatusDok(idKokaMag);
            }
        }

        public static int merrIdStatusDok(int idKokaMag, clsDatabaseRegjistrim data)
        {
            return data.merrIdStatusDok(idKokaMag);
        }

        /// <summary>
        /// Merr id gjeneruesin e dokumentit te magazines
        /// </summary>
        /// <param name="idKokaMagazines">(int) Id e kokes se magazines</param>
        /// <returns>Kthen id e dokumentit gjenerues te dokumentit te magazines</returns>
        public static int merrIdGjenerues(int idKokaMagazines)
        {
            clsDatabaseRegjistrim dbKokaMagazina = new clsDatabaseRegjistrim();
            return dbKokaMagazina.ktheIdGjenerues(idKokaMagazines);
        }

        public static clsMesazh kaloNeHistorikKokaMagazina(int idkoka, clsDatabaseRegjistrim db)
        {
            clsMesazh mesazh = db.kaloNeHistorikKokaMagazina(idkoka);
            return mesazh;
        }

        public clsMesazh kontrolloGjendjeArtikujshPerDokImporti(bool isDalje, colTrupiMagazina ocolTrupiMagazina, clsDatabaseInventari dbInv, int idNdermarrje, int idKonfAmbjente)
        {
            ImbLogger.LogTraceShitje("Filloi metoda kontrolloGjendjeArtikujshPerDokImporti");
            
            clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim(dbInv);
            clsMesazh mesazh = new clsMesazh();
            bool kontrollGjendjeDokumenti = (clsAlternativaKushti.getAlternativa(idKonfAmbjente, "KGJA") == "Jo");
           
            for (int i = 0, nrRreshta = ocolTrupiMagazina.Count; i < nrRreshta; i++)
            {
                double totaliArtikullit = 0;
                clsTrupiMagazina rreshtDokMag = ocolTrupiMagazina[i];
                var mag = clsNjesiAdministrative.ktheMagazineSipasIdNeseEkziston(rreshtDokMag.IdMag, dbRegj);
                bool magMeNdjekjeGjendje = mag != null ? mag.NdjekjeGjendje : false;
                DbInventari.clsArtikulli artikulli = (DbInventari.clsArtikulli)(rreshtDokMag.Element);
                totaliArtikullit = ktheTotalinArtikullit(ocolTrupiMagazina, artikulli.IdArtikulli, rreshtDokMag.IdMag);
                mesazh = KontrolloGjendjeArtikulli(isDalje, totaliArtikullit, idNdermarrje, ocolTrupiMagazina, artikulli, rreshtDokMag, dbInv, dbRegj, kontrollGjendjeDokumenti, magMeNdjekjeGjendje);
                if (!mesazh.Status)
                {
                    ImbLogger.LogTraceShitje("Mbaroi metoda kontrolloGjendjeArtikujshPerDokImporti");
                    return mesazh;
                }
                    
            }

            ImbLogger.LogTraceShitje("Mbaroi metoda kontrolloGjendjeArtikujshPerDokImporti");
            return new clsMesazh(true, "Kontrollet u kaluan me sukses!");
        }

        public clsMesazh KontrolloGjendjeArtikulli(bool isDalje, double totaliArtikullit, int idNdermarrje, colTrupiMagazina ocolTrupiMagazina, clsArtikulli artikulli, clsTrupiMagazina rreshtDokMag, clsDatabaseInventari dbInv, clsDatabaseRegjistrim dbRegj, bool kontrollGjendjeDokumenti, bool meNdjekjeMagazina)
        {
            clsTrupiMagazina tempTrupi = (clsTrupiMagazina)rreshtDokMag.ShallowCopy();
            tempTrupi.Sasia = totaliArtikullit;
            double sasidet1 = 0;
            double sasidet2 = 0;
            if (rreshtDokMag.IdDetajimi > 0)
            {
                double totaliArtikullitDet = ktheTotalinArtikullitDetajim(ocolTrupiMagazina, artikulli.IdArtikulli, rreshtDokMag.IdDetajimi, rreshtDokMag.IdMag);//totali i artikullit qe duam te bejme dalje(vlera e ketij rreshti + gjithe rreshtave te tjere qe kane kete artikull per kete detajim)
                sasidet1 = totaliArtikullitDet;
            }
            if (rreshtDokMag.IdDetajimi2 > 0)
            {
                double totaliArtikullitDet = ktheTotalinArtikullitDetajimDyte(ocolTrupiMagazina, artikulli.IdArtikulli, rreshtDokMag.IdDetajimi2, rreshtDokMag.IdMag);//totali i artikullit qe duam te bejme dalje(vlera e ketij rreshti + gjithe rreshtave te tjere qe kane kete artikull per kete detajim)
                sasidet2 = totaliArtikullitDet;
            }

            return kontrolloGjendjePerRreshtArtikulli(dbRegj, dbInv, tempTrupi, idNdermarrje, isDalje, false, false, new DateTime(1900, 1, 1), totaliArtikullit, 0, 0, sasidet1, sasidet2, kontrollGjendjeDokumenti, meNdjekjeMagazina);
        }


        public static int merrIdDokHyrjeNgaTransferimi(int idKokaMagazina)
        {
            using (var db = new clsDatabaseRegjistrim())
                return db.merrIdDokHyrjeNgaTransferimi(idKokaMagazina);
        }

        public DataTable KtheVeprimeTeMevonshmeAsete(List<int> idSerialiList, int idNdermarje, DateTime dateDokumenti)
        {
            using (var dbRegj = new clsDatabaseRegjistrim())
                return dbRegj.KtheVeprimeTeMevonshmeAsete(idSerialiList, idNdermarje, dateDokumenti);
        }
        
        public static clsMesazh RefuzoDokument(int id)
        {
            clsMesazh mesazh = new clsMesazh(true);
            using (var scope = new MyTransactionScope())
            using (clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim())
            {
                dbRegj.refuzoStatusDokumentiMagazine(id);
                var idshitjekoka = merrIdGjenerues(id);
                if(idshitjekoka > 0)
                    mesazh = clsKokaShitje.refuzo(idshitjekoka, "", dbRegj);
                if (mesazh)
                    scope.Complete();

                return mesazh;
            }
            
        }
        public static List<Tuple<int, string>> merrDetajimeNeFDTK(int idKokaMagazina)
        {
            List<Tuple<int, string>> seriale = new List<Tuple<int, string>>();

            colTrupiMagazina mag = new colTrupiMagazina();
            mag.mbushGjitheTrupiMagazinaDaljeNgahyrja(idKokaMagazina);
            colDetajimeArtikulli detajime = mag.ktheColDetArt();
            if (detajime.Count > 0)
            {
                for (int i = 0; i < mag.Count; i++)
                {
                    seriale.Add(new Tuple<int, string>(mag[i].IdArtikulli, detajime[i].KodDetajimArtikulli));
                }
            }
            return seriale;
        }

        public static bool eshteDokumentHyrjeTransferimiVod(int idKokaMagazina)
        {
            using(var dbRegj = new clsDatabaseRegjistrim())
            {
                return dbRegj.eshteDokumentHyrjeTransferimiVod(idKokaMagazina);
            }
        }

        public static DataTable MerrDokumentMagazinePerShperndarjeShpenzimesh(int idKokaMagazina)
        {
            using (var dbRegj = new clsDatabaseRegjistrim())
            {
                return dbRegj.MerrDokumentMagazinePerShperndarjeShpenzimesh(idKokaMagazina);
            }
        }

        #endregion Metoda Publike

        #region Metoda Internal

        internal bool mbushKokaMagazina(DataRow rreshti, clsDatabaseRegjistrim db)
        {
            ImbLogger.LogTraceShitje("Filloi metoda mbushKokaMagazina");
            if (rreshti != null)
            {
                try
                {
                    int.TryParse(rreshti["IDKOKAMAGAZINA"].ToString(), out idKokaMagazina);
                    int.TryParse(rreshti["IDNIVEL"].ToString(), out idNivel);
                    int.TryParse(rreshti["IDKONFIGAMBJENTE"].ToString(), out idKonfigAmbjente);
                    int.TryParse(rreshti["IDKLIENTFURNITOR"].ToString(), out idKlientFurnitor);
                    int.TryParse(rreshti["IDMAG"].ToString(), out idMagazina);
                    nrDok = rreshti["NRDOK"].ToString();
                    DateTime.TryParse(rreshti["DTDOK"].ToString(), out dtDok);
                    int.TryParse(rreshti["IDPROJEKTI"].ToString(), out idProjekt);
                    nrProjekt = rreshti["NRPROJEKTI"].ToString();
                    int.TryParse(rreshti["IDKATDOK"].ToString(), out idKategoria);
                    int.TryParse(rreshti["IDDOKNGA"].ToString(), out idDokNga);
                    double.TryParse(rreshti["VLEFTA"].ToString(), out vlefta);
                    int.TryParse(rreshti["IDGRUP1"].ToString(), out idGrup1);
                    int.TryParse(rreshti["IDGRUP2"].ToString(), out idGrup2);
                    int.TryParse(rreshti["IDGRUP3"].ToString(), out idGrup3);
                    int.TryParse(rreshti["IDSTATUSDOK"].ToString(), out idStatusDok);
                    int.TryParse(rreshti["IDNDERM"].ToString(), out idNdermarrje);
                    int.TryParse(rreshti["IDNDERMVIT"].ToString(), out idNdermarrjeVit);
                    int.TryParse(rreshti["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    DateTime.TryParse(rreshti["DTREGJISTRIMI"].ToString(), out dtRegjistrimi);
                    int.TryParse(rreshti["IDLLOJDOKUMENTIMAGAZINE"].ToString(), out idLlojDokumentiMagazine);
                    shenime = rreshti["SHENIME"].ToString();
                    int.TryParse(rreshti["IDRENDITJES"].ToString(), out idRenditjes);
                    int.TryParse(rreshti["IDNIVELGJENERUES"].ToString(), out idNivelGjenerues);
                    int.TryParse(rreshti["IDKONFIGGJENERUES"].ToString(), out idKonfigGjenerues);
                    int.TryParse(rreshti["IDGJENERUES"].ToString(), out idGjenerues);
                    int.TryParse(rreshti["IDDEGEADMINISTRATIVE"].ToString(), out idDegeAdministrative);
                    int.TryParse(rreshti["IDLLOG"].ToString(), out idLlogari);
                    int.TryParse(rreshti["IDNJESIVARTESE"].ToString(), out idNjesiVartese);
                    bool.TryParse(rreshti["MEKONFIRMIM"].ToString(), out meKonfirmim);
                    int.TryParse(rreshti["IDKRIJUESI"].ToString(), out idKrijuesi);
                    DateTime.TryParse(rreshti["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(rreshti["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    Pershkrimi = rreshti["PERSHKRIMI"].ToString();
                    Magazinieri = rreshti["MAGAZINIERI"].ToString();

                    Adresa = rreshti["ADRESA"].ToString();
                    int.TryParse(rreshti["IDAUTOMJETI"].ToString(), out idAutomjet);
                    int.TryParse(rreshti["IDRAPORTDESING"].ToString(), out idRaportDesing);
                    DateTime.TryParse(rreshti["DTTRANSPORTI"].ToString(), out dtTransporti);
                    shoferi = rreshti["SHOFERI"].ToString();
                    targaShoferi = rreshti["TARGA"].ToString();
                    int.TryParse(rreshti["IDKATEGORISERIALI"].ToString(), out idKategoriSeriali);
                    nrSerial = rreshti["NrSerial"].ToString();
                    NIVFSH = rreshti["NIVFSH"].ToString();
                    WTNIC = rreshti["WTNIC"].ToString();
                    int.TryParse(rreshti["IDOPERATOR"].ToString(), out idOperator);
                    if (rreshti.Table.Columns.Contains("Transportuesi"))
                    {
                        int.TryParse(rreshti["Transportuesi"].ToString(), out transportuesi);
                        bool.TryParse(rreshti["MALLRATEDJEGSHME"].ToString(), out mallraTeDjegshme);
                        bool.TryParse(rreshti["SHOQERIMIKERKUAR"].ToString(), out shoqerimIKerkuar);
                        if (rreshti.Table.Columns.Contains("TIPI"))
                        {
                            Tipi = rreshti["TIPI"].ToString();
                            Transaksioni = rreshti["TRANSAKSIONI"].ToString();
                        }
                           
                    }

                    //ocolTrupiMagazina = new colTrupiMagazina();
                    //ocolTrupiMagazina = merrTrupiMagazina(db);
                    return true;
                }
                catch (InvalidCastException)
                {
                    ImbLogger.LogErrorShitje("Gabim gjate marrjes se dokumentit te magazines nga db-ja");
                    throw new Exception("ERROR: Gabim gjate marrjes se dokumentit te magazines nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion Metoda Internal
    }
}