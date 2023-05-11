using DbCore.DbAdmin;
using DbCore.DbAsete;
using DbCore.DbInventari;
using DbCore.DbKontabiliteti;
using DbCore.DbShare;
using DbCore.DbTollona;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Web.Script.Serialization;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Logging;
using DbCore.Integrime;
using DbCore.DbArkaBanka;
using DbCore.IMBUtils.Messages;
using Newtonsoft.Json;
using DbCore.IMBUtils.Fiskalizimi.Controls;
using System.Web.Configuration;
using DbCore.IMBUtils.Kontrolle.Controls;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne  koken e nje dokumenti  shitje ose blerje
    ///  (Te dhenat  merren nga tabela : T_KOKASHITJE)
    /// </summary>
    public sealed class clsKokaShitje
    {
        #region Atribute

        private int idShitjeKoka;
        private int idNivel;
        private int idTemplate;
        private int idKonfigAmbjente;
        private int idKlientFurnitor;
        private int idProjekt;
        private String nrProjekt;
        private DateTime dtDok;
        private String nrDok;
        private String nrSerial;
        private DateTime dtMaturimi;

        private int idMonedha;
        private double kursi;
        private int idMenyreTransporti;
        private DateTime dtTransportimi;
        private int idKushtDergimi;
        private int idAgjent;
        private int idMenyrePagese;
        private int idKushtPagese;
        private double zbritje;
        private double totali;
        private double tvsh;
        private DateTime dtRegjistrimi;
        private int idStatusDok;
        private int idNdermarje;
        private int idNdermarjeVit;
        private int idNivelGjenerues;
        private int idKonfigGjenerues;
        private int idGjenerues;
        private int idDokNga;
        private string adresaFaturimit;
        private string adresaDergimit;
        private string pershkrimi;
        private double totaliMeZbritjeMeTvsh;
        private bool dogana;
        private int idDegeAdministrative;
        private int idPikeShitjeFurnizimi;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private int idPerdoruesi;
        private int idRaportDesing;
        private double vleraMbetur;
        private string colorVleraMbetur;
        private colTrupiShitje oColTrupiShitje;
        private clsKokaFleteKontabel oFleteKontabel;
        private string kodKlientFurnitor;
        private string kodDegeAdministrative;
        private string kodMonedha;
        private string kodMenyreTransporti;
        private string kodKushtDergimi;
        private string kodAgjenti;
        private string kodMenyrePagese;
        private string kodKushtPagese;
        private string kodPikeShitje;
        private double perqindjeAgjenti;
        private DateTime dtFillimi;
        private DateTime dtMbarimi;
        private DateTime dtFature;
        private bool gjeneruar;
        private int muajRaportimi;
        private int idVitRaportimi;
        private int idLlojMarreveshje;
        private string idMarreveshje;
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
        /// pagesa kesh e bere
        /// </summary>
        private double cash;
        private StatusAprovimi statusAprovimi;
        private int idKrijuesi;
        private string krijuesi;
        private int idTransferimi;
        private int idKonfigTransferimi;
        private clsKokaMagazina oKokaMagazina;
        private colGjendjeKlientFurnitor oGjendjeKF;
        private clsKokaRezervime oKokaRezervime;
        private colArkiva oArkiva;
        private colGaranciArtikulli colGaranci;
        private DateTime afatKohor;
        private StatusTrasferimi statusTransferimi;
        private string emerKlienti;
        private string kontakti;
        private bool kase;
        private bool kupon;
        private int idAutomjet;
        private string targa;
        private double kilometraAuto;
        private int idAgjenti2;
        private double perqindjeAgjenti2;
        private string kodAgjenti2;
        private int idAgjenti3;
        private double perqindjeAgjenti3;
        private string kodAgjenti3;
        private string marresi;
        private int idTransportues;
        private string emertimTr;
        private bool faturePermbledhese;
        private bool shpenzimeJoTeZbritshme;
        private int idarka;
        private colDokumentLidhesKoka oDokumentLidhes;
        private int idKarta;
        private int pike;
        private colFazaKontrate ocolFazat;
        private int idFaza;
        private string shoferi;
        private string targaSHF;
        private int idKategoriSeriali;
        public static string mesazhSuksesKase = "Fatura u dergua ne kasen fiskale me sukses";
        private static string krijimStreamKaseMeSukses = "Krijimi i skontrinos u krye me sukses";
        private static string krijimStreamKaseMeSuksesDheArritjaLimititPLU = "Krijimi i skontrinos u krye me sukses! Ju lutem ne fund te dites pasi te beni mbylljen ditore duhet te beni reset edhe PLU.";
        private DataRow rreshti;
        private bool zbritjeNeVlere;
        private double perqindjeZbritje;
        private DateTime dtKrijimiPajisje;
        private string llojiKf;
        private string koordinata;
        private string niptKlienti;
        private string qytetiK;
        private string shenime2;
        private bool kartaPaPagese;
        private int idDokTransferimNga;
        private string nrDokMagazine;
        private string iic;
        private string nivf;
        private int idOperator;
        private string nivfKthim;
        private string eic;
        private string einStatus;
        private int procesi;
        private int eInvoiceType;
        private string tipiIVetefaturimit;

        public static void merrDokParaPas(int idDok, ref int idDokPara, ref int idDokPas)
        {
            if (idDok == 0)
            {
                if (idDokPara == 0)
                    idDokPara = clsKokaShitje.merrDokunEFundit();
                return;
            }
            if (idDokPara == 0 && idDokPas == 0)
            {
                //merri te dy nga db-ja
                return;
            }
            if (idDokPara == 0)
            {
                idDokPara = merrDokPara(idDok);
            }
            if (idDokPas == 0)
            {
                idDokPas = merrDokPas(idDok);
            }
            return;
            throw new NotImplementedException();
        }
        private static int merrDokPara(int idDok)
        {
            int idDokPara = 0;
            return idDokPara;
            throw new NotImplementedException();
        }
        private static int merrDokPas(int idDok)
        {
            int idDokPas = 0;
            return idDokPas;
            throw new NotImplementedException();
        }
        private static int merrDokunEFundit()
        {
            return 0;
            throw new NotImplementedException();
        }
        private string kerkuarNga;
        private DateTime dateKerkese;


        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases me parametra
        /// </summary>
        /// <param name="dtDk"> data e dokumentit</param>
        /// <param name="dtMat"> data e maturimit te dokumentit</param>
        /// <param name="dtregj"> data e regjistrimit te dokumentit</param>
        /// <param name="dttrans"> data e transportimit </param>
        /// <param name="idAgj"> id e agjentit te shitjes</param>
        /// <param name="idKlFurn">id e klient furnitorit</param>
        /// <param name="idKokaMagazina"> id e kokes se magazines qe gjenerohet nga ky dokument</param>
        /// <param name="idKonf"> id e konfigurimit te dokumentit</param>
        /// <param name="idKushtDerg"> id e kushtit te dergimit</param>
        /// <param name="idKushtPag"> id e kushtit te pageses</param>
        /// <param name="idMenPag"> id e menyres se pageses</param>
        /// <param name="idMenTrans"> id e menyres se transportimit</param>
        /// <param name="idMon"> id e monedhes se dokumentit</param>
        /// <param name="idNder"> id e ndermarjes </param>
        /// <param name="idNdVt"> id e ndermarje vitit </param>
        /// <param name="idNiv">id e nivelit te dokumentit</param>
        /// <param name="idProj">id e projektit ne te cilin ben pjese</param>
        /// <param name="idShiKoka">id ritese e dokumentit</param>
        /// <param name="idSt"> id e gjendjes se dokumentit</param>
        /// <param name="idtempl">id e templatit nga eshte gjeneruar dokumenti</param>
        /// <param name="krs"> kursi i dokumentit</param>
        /// <param name="nrDk"> nr i dokumentit</param>
        /// <param name="nrProj"> nr i projektit</param>
        /// <param name="nrserial"> nr serial </param>
        /// <param name="tot"> totali i dokumentit</param>
        /// <param name="tv"> tvsh e dokumentit </param>
        /// <param name="zbr"> zbritja totale e dokumentit</param>
        /// <param name="idDokNga">id e dokumentit te shitje/blerjes nga i cili gjenerohet ne rastet e modifikimit</param>
        /// <param name="idGjenerues"> id e dokumentit nga i cili gjenerohet kur gjenerohet nga nje dokument i nje ambjenti tjeter</param>
        /// <param name="idKonfigGjenerues">id e konfigurimit te dokumentit nga gjenerohet</param>
        /// <param name="idNivelGjenerues">id e nivelit te dokumentit nga gjenerohet</param>
        /// <param name="pershkrimi">pershkrimi</param>
        /// <param name="adrFaturimi">adrFaturimi</param>
        /// <param name="adrDergimi">adrDergimi</param>  
        /// <param name="idgrup1">id e grupimit te pare</param>
        /// <param name="idgrup2">id e grupimit te dyte</param>
        /// <param name="idgrup3">id e grupimit te trete</param>
        /// <param name="dtfill"> data e fillimit te kontrates </param>
        /// <param name="dtmb"> data e mbarimit te kontrates </param>
        /// <param name="koordinata">koordinata a shitjes</param>
        /// <param name="niptKlienti">nipti i klientit</param>
        /// <param name="dtmb"> data e fatures per te cilen po behet pagesa e fatures aktuale </param>
        public clsKokaShitje(int idShiKoka, int idNiv, int idtempl, int idKonf, int idKlFurn, int idProj, string nrProj, DateTime dtDk, string nrDk, string nrserial, DateTime dtMat, int idMon, double krs, int idMenTrans, DateTime dttrans, int idKushtDerg, int idAgj, int idMenPag, int idKushtPag, double zbr, double tot, double tv, DateTime dtregj, int idSt, int idNder, int idNdVt, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues, int idDokNga, string adrFaturim, string adrDergim, string pershkrimi, bool dogana, int iddegeadministrative, int idPikeshitjefurnizimi, int idperdoruesi, int idrap, int idgrup1, int idgrup2, int idgrup3, DateTime afatikohor, double cash, StatusAprovimi statusAprovimi, int idkrijuesi, double PerqAgj, int idtransferimi, int idkonfigtransferimi, bool kase, bool kupon, DateTime dtfill, DateTime dtmb, int idAutomjet, double kilometraAuto, int idAgjenti2, double perqindjeAgjent2, int idAgjenti3, double perqindjeAgjent3, string marresi, int idTransportues, bool faturepermbledhese, bool shpenzimeJoTeZbritshme, int idarka, DateTime dtfat, bool gjeneruar, int idMuajRaportimi, int idVitRaportimi, colKlienteFurnitore colKlienteFurnitoreVartes, bool zbritjeNeVlere, double perqindjaZritje, DateTime dtKijimiPajisje, string koordinata, string niptKLienti, string qytetik, int idKategoriSeriali, string shenime2, bool kartaPaPagese, int idDokTransferimNga, int idLlojMarreveshje, string marrveshje, bool marrAktive, string kerkuarNga, DateTime dateKerkese, string nrDokMagazine)
        {
            idShitjeKoka = idShiKoka;
            idNivel = idNiv;
            idTemplate = idtempl;
            idKonfigAmbjente = idKonf;
            idKlientFurnitor = idKlFurn;
            idProjekt = idProj;
            nrProjekt = nrProj;
            nrDok = nrDk;
            nrSerial = nrserial;
            dtDok = dtDk;
            dtMaturimi = dtMat;
            idMonedha = idMon;
            kursi = krs;
            idMenyreTransporti = idMenTrans;
            dtTransportimi = dttrans;
            idKushtDergimi = idKushtDerg;
            idAgjent = idAgj;
            idMenyrePagese = idMenPag;
            idKushtPagese = idKushtPag;
            zbritje = zbr;
            totali = tot;
            tvsh = tv;
            this.kase = kase;
            this.kupon = kupon;
            this.faturePermbledhese = faturepermbledhese;
            dtRegjistrimi = dtregj;
            idStatusDok = idSt;
            idNdermarje = idNder;
            idNdermarjeVit = idNdVt;
            this.afatKohor = afatikohor;
            this.idNivelGjenerues = idNivelGjenerues;
            this.idKonfigGjenerues = idKonfigGjenerues;
            this.idGjenerues = idGjenerues;
            this.idDokNga = idDokNga;
            this.adresaFaturimit = adrFaturim;
            this.adresaDergimit = adrDergim;
            this.pershkrimi = pershkrimi;
            this.dogana = dogana;
            this.idDegeAdministrative = iddegeadministrative;
            this.idPikeShitjeFurnizimi = idPikeshitjefurnizimi;
            idPerdoruesi = idperdoruesi;
            idRaportDesing = idrap;
            idGrup1 = idgrup1;
            idGrup2 = idgrup2;
            idGrup3 = idgrup3;
            this.cash = cash;
            this.statusAprovimi = statusAprovimi;
            this.idKrijuesi = idkrijuesi;
            perqindjeAgjenti = PerqAgj;
            this.idTransferimi = idtransferimi;
            this.idKonfigTransferimi = idkonfigtransferimi;
            oKokaRezervime = new clsKokaRezervime();
            oColTrupiShitje = new colTrupiShitje();
            colGaranci = new colGaranciArtikulli();
            dtFillimi = dtfill;
            dtMbarimi = dtmb;
            this.idAutomjet = idAutomjet;
            this.kilometraAuto = kilometraAuto;
            this.idAgjenti2 = idAgjenti2;
            this.perqindjeAgjenti2 = perqindjeAgjent2;
            this.idAgjenti3 = idAgjenti3;
            this.perqindjeAgjenti3 = perqindjeAgjent3;
            this.marresi = marresi;
            this.idTransportues = idTransportues;
            this.shpenzimeJoTeZbritshme = shpenzimeJoTeZbritshme;
            this.idarka = idarka;
            dtFature = dtfat;
            this.gjeneruar = gjeneruar;
            this.muajRaportimi = idMuajRaportimi;
            this.idVitRaportimi = idVitRaportimi;
            ColKlienteFurnitoreVartes = colKlienteFurnitoreVartes;
            this.zbritjeNeVlere = zbritjeNeVlere;
            this.perqindjeZbritje = perqindjaZritje;
            this.dtKrijimiPajisje = dtKijimiPajisje;
            this.koordinata = koordinata;
            this.niptKlienti = niptKLienti;
            this.qytetiK = qytetik;
            this.idKategoriSeriali = idKategoriSeriali;
            this.shenime2 = shenime2;
            this.kartaPaPagese = kartaPaPagese;
            this.idDokTransferimNga = idDokTransferimNga;
            this.idLlojMarreveshje = idLlojMarreveshje;
            this.kerkuarNga = kerkuarNga;
            this.dateKerkese = dateKerkese;
            this.nrDokMagazine = nrDokMagazine;
            
        }

        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsKokaShitje()
        {
        }

        public clsKokaShitje(int idShitjeKoka)
        {
            mbushKokaShitjeSipasIDPaTrup(idShitjeKoka, new clsDatabaseRegjistrim());
        }

        public clsKokaShitje(int idShitjeKoka, clsDatabaseRegjistrim dbRegj)
        {
            mbushKokaShitjeSipasIDPaTrup(idShitjeKoka, dbRegj);
        }

        /// <summary>
        /// kujdes! mbush koken bashke me trup
        /// </summary>
        /// <param name="rreshti"></param>
        public clsKokaShitje(DataRow rreshti)
        {
            mbushKokeShitjeSipasDT(rreshti);
        }

        #endregion

        #region Properties

        public int IdVitRaportimi
        {
            get { return idVitRaportimi; }
            set { idVitRaportimi = value; }
        }
        public int MuajRaportimi
        {
            get { return muajRaportimi; }
            set { muajRaportimi = value; }
        }
        /// <summary>
        /// tregon nese kjo fature eshte fature permbledhese e kuponave tatimore
        /// </summary>
        public bool FaturePermbledhese
        {
            get
            {
                return faturePermbledhese;
            }
            set
            {
                faturePermbledhese = value;
            }
        }
        /// <summary>
        /// tregon nese ky dokument ka gjeneruar dokument tjeter apo jo
        /// </summary>
        public bool Gjeneruar
        {
            get
            {
                return gjeneruar;
            }
            set
            {
                gjeneruar = value;
            }
        }
        /// <summary>
        /// tregon nese kjo fature ka shpenzime jo te zbritshme 
        /// </summary>
        public bool ShpenzimeJoTeZbritshme
        {
            get
            {
                return shpenzimeJoTeZbritshme;
            }
            set
            {
                shpenzimeJoTeZbritshme = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdShitjeKoka
        {
            get { return idShitjeKoka; }
            set { idShitjeKoka = value; }
        }

        /// <summary>
        /// emri i klientit, fushe tekst qe shtohet per informacion
        /// </summary>
        public string EmerKlienti
        {
            get
            {
                return emerKlienti;
            }
            set
            {
                emerKlienti = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne  e nivelit te dokumentit
        /// <example> id e KB,OB,UB,FB, KSH,FSH,USH,OSH etj</example>
        /// </summary>
        public int IdNivel
        {
            get { return idNivel; }
            set { idNivel = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne  e arkes
        /// </summary>
        public int IdArka
        {
            get { return idarka; }
            set { idarka = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne  e templatit nga gjenerohet dokumenti.
        /// </summary>
        public int IdTemplate
        {
            get { return idTemplate; }
            set { idTemplate = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne  e konfigurimit te dokumentit.
        /// </summary>
        public int IdKonfigAmbjente
        {
            get { return idKonfigAmbjente; }
            set { idKonfigAmbjente = value; }
        }

        /// <summary>
        /// id e grupimit te pare
        /// </summary>
        public int IdGrup1
        {
            get
            {
                return idGrup1;
            }
            set
            {
                idGrup1 = value;
            }
        }

        /// <summary>
        /// id e grupimit te dyte
        /// </summary>
        public int IdGrup2
        {
            get
            {
                return idGrup2;
            }
            set
            {
                idGrup2 = value;
            }
        }

        /// <summary>
        /// id e grupimit te trete
        /// </summary>
        public int IdGrup3
        {
            get
            {
                return idGrup3;
            }
            set
            {
                idGrup3 = value;
            }
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
        /// Kthen/Vendos ID-ne  e projektit ne te cilin ben pjese.
        /// </summary>
        public int IdProjekt
        {
            get { return idProjekt; }
            set { idProjekt = value; }
        }

        /// <summary>
        /// koleksioni me garancite e artikujve
        /// </summary>
        public colGaranciArtikulli ColGaranci
        {
            get
            {
                return colGaranci;
            }
            set
            {
                colGaranci = value;
            }
        }

        /// <summary>
        /// tregon nese eshte shtypur butoni printo ne kase po apo jo
        /// </summary>
        public bool Kase
        {
            get
            {
                return kase;
            }
            set
            {
                kase = value;
            }
        }

        public string KodAgjenti
        {
            get
            {
                return kodAgjenti;
            }
        }

        public string KodDegeAdministrative
        {
            get
            {
                return kodDegeAdministrative;
            }
        }

        public string KodKushtDergimi
        {
            get
            {
                return kodKushtDergimi;
            }
        }

        public string KodKushtPagese
        {
            get
            {
                return kodKushtPagese;
            }
        }

        public string KodMenyrePagese
        {
            get
            {
                return kodMenyrePagese;
            }
        }

        public string KodMenyreTransporti
        {
            get
            {
                return kodMenyreTransporti;
            }
        }

        public string KodPikeShitje
        {
            get
            {
                return kodPikeShitje;
            }
        }

        /// <summary>
        /// nr i telefonit te klientit
        /// </summary>
        public string Kontakti
        {
            get
            {
                return kontakti;
            }
            set
            {
                kontakti = value;
            }
        }

        /// <summary>
        /// tregon nese eshte printuar kupon apo fature tatimore
        /// </summary>
        public bool Kupon
        {
            get
            {
                return kupon;
            }
            set
            {
                kupon = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos Nr i projektit
        /// </summary>
        public string NrProjekt
        {
            get { return nrProjekt; }
            set { nrProjekt = value; }
        }

        /// <summary>
        /// Kthen/Vendos  nr i dokumentit.
        /// </summary>
        public string NrDok
        {
            get { return nrDok; }
            set { nrDok = value; }
        }

        /// <summary>
        /// Kthen/Vendos nr i serialit.
        /// </summary>
        public string NrSerial
        {
            get { return nrSerial; }
            set { nrSerial = value; }
        }

        /// <summary>
        /// Kthen/Vendos  dt e dokumentit.
        /// </summary>
        public DateTime DtDok
        {
            get { return dtDok; }
            set { dtDok = value; }
        }

        /// <summary>
        /// Kthen/Vendos dt e maturimit te dokumentit.
        /// </summary>
        public DateTime DtMaturimi
        {
            get { return dtMaturimi; }
            set { dtMaturimi = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne  e monedhes se dokumentit.
        /// </summary>
        public int IdMonedha
        {
            get { return idMonedha; }
            set { idMonedha = value; }
        }

        /// <summary>
        /// Kthen/Vendos  kursi i monedhes se dokumentit.
        /// </summary>
        public double Kursi
        {
            get { return kursi; }
            set { kursi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne  e menyres se transportit.
        /// </summary>
        public int IdMenyreTransporti
        {
            get { return idMenyreTransporti; }
            set { idMenyreTransporti = value; }
        }

        /// <summary>
        /// Kthen/Vendos dt e transporitimit.
        /// </summary>
        public DateTime DtTransportimi
        {
            get { return dtTransportimi; }
            set { dtTransportimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e kushtit te dergimit.
        /// </summary>
        public int IdKushtDergimi
        {
            get { return idKushtDergimi; }
            set { idKushtDergimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e agjentit te shitjes.
        /// </summary>
        public int IdAgjent
        {
            get { return idAgjent; }
            set { idAgjent = value; }
        }

        /// <summary>
        /// Kthen/Vendos perqindjen e agjentit te shitjes.
        /// </summary>
        public double PerqindjeAgjenti
        {
            get { return perqindjeAgjenti; }
            set { perqindjeAgjenti = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e menyres se pageses.
        /// </summary>
        public int IdMenyrePagese
        {
            get { return idMenyrePagese; }
            set { idMenyrePagese = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e kushtit te pageses
        /// </summary>
        public int IdKushtPagese
        {
            get { return idKushtPagese; }
            set { idKushtPagese = value; }
        }

        /// <summary>
        /// statusi i aprovimit
        /// </summary>
        public StatusAprovimi StatusAprovimi
        {
            get { return statusAprovimi; }
            set { statusAprovimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos zbritja totale e dokumentit.
        /// </summary>
        public double Zbritje
        {
            get { return zbritje; }
            set { zbritje = value; }
        }

        /// <summary>
        /// Kthen/Vendos totali i dokumentit.
        /// </summary>
        public double Totali
        {
            get { return totali; }
            set { totali = value; }
        }

        /// <summary>
        /// Kthen/Vendos totali me zbritje me tvsh.
        /// </summary>
        public double TotaliMeZbritjeMeTVSH
        {
            get { return totaliMeZbritjeMeTvsh; }
            set { totaliMeZbritjeMeTvsh = value; }
        }

        public double VleraMbetur
        {
            get { return vleraMbetur; }
            set { vleraMbetur = value; }
        }

        public string ColorVleraMbetur
        {
            get { return colorVleraMbetur; }
            set { colorVleraMbetur = value; }
        }

        /// <summary>
        /// pagesa kesh e bere
        /// </summary>
        public double Cash
        {
            get { return cash; }
            set { cash = value; }
        }

        /// <summary>
        /// Kthen/Vendos tvsh e dokumentit.
        /// </summary>
        public double Tvsh
        {
            get { return tvsh; }
            set { tvsh = value; }
        }

        /// <summary>
        /// Kthen/Vendos data e regjistrimit te dokumentit.
        /// </summary>
        public DateTime DtRegjistrimi
        {
            get { return dtRegjistrimi; }
            set { dtRegjistrimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e gjendjes se dokumentit.
        /// <example> ruajtur , draft etj</example>
        /// </summary>
        public int IdStatusDok
        {
            get { return idStatusDok; }
            set { idStatusDok = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e ndermarjes.
        /// </summary>
        public int IdNdermarrje
        {
            get { return idNdermarje; }
            set { idNdermarje = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne  e ndermarje vitit.
        /// </summary>
        public int IdNdermarrjeVit
        {
            get { return idNdermarjeVit; }
            set { idNdermarjeVit = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e nivelit nga gjenerohet ky dokument.
        /// </summary>
        public int IdNivelGjenerues
        {
            get { return idNivelGjenerues; }
            set { idNivelGjenerues = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e konfigurimit nga gjenerohet ky dokument.
        /// </summary>
        public int IdKonfigGjenerues
        {
            get { return idKonfigGjenerues; }
            set { idKonfigGjenerues = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e dokumentit qe e gjeneron kete dokument kur eshte nga nje ambjent tjeter.
        /// </summary>
        public int IdGjenerues
        {
            get { return idGjenerues; }
            set { idGjenerues = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e dokumentit qe e gjeneron kete dokument kur kemi modifikim te dokumentit.
        /// </summary>
        public int IdDokNga
        {
            get { return idDokNga; }
            set
            {
                idDokNga = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e deges administrative
        /// </summary>
        public int IdDegeAdministrative
        {
            get { return idDegeAdministrative; }
            set
            {
                idDegeAdministrative = value;
            }
        }

        public int IdPerdoruesi
        {
            get { return idPerdoruesi; }
            set
            {
                idPerdoruesi = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e pikes se shitjes furnizimit
        /// </summary>
        public int IdPikeShitjeFurnizimi
        {
            get { return idPikeShitjeFurnizimi; }
            set
            {
                idPikeShitjeFurnizimi = value;
            }
        }

        public int IdRaportDesing
        {
            get { return idRaportDesing; }
            set
            {
                idRaportDesing = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos adresen e faturimit te dokumentit
        /// </summary>
        public string AdresaFaturimit
        {
            get { return adresaFaturimit; }
            set
            {
                adresaFaturimit = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos adresen e dergimit te dokumentit
        /// </summary>
        public string AdresaDergimit
        {
            get { return adresaDergimit; }
            set
            {
                adresaDergimit = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos pershkrimin e dokumentit
        /// </summary>
        public string Pershkrimi
        {
            get { return pershkrimi; }
            set
            {
                pershkrimi = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos doganen
        /// </summary>
        public bool Dogana
        {
            get { return dogana; }
            set
            {
                dogana = value;
            }
        }

        public DateTime DtKrijimi
        {
            get { return dtKrijimi; }

        }

        public DateTime DtModifikimi
        {
            get { return dtModifikimi; }

        }

        /// <summary>
        /// Kthen/Vendos nje kolekson me trupin e dokumentit.
        /// </summary>
        public colTrupiShitje OColTrupiShitje
        {
            get { return oColTrupiShitje; }
            set { oColTrupiShitje = value; }
        }

        /// <summary>
        /// Kthen/Vendos nje dokument flete kontabel qe gjenerohet kur dokumenti kontabilizohet.
        /// </summary>
        public clsKokaFleteKontabel OFleteKontabel
        {
            get { return oFleteKontabel; }
            set { oFleteKontabel = value; }
        }

        /// <summary>
        /// Kthen/Vendos nje dokument koke magazine qe mban dokumentin e magazines te gjeneruar nga ky dokument.
        /// </summary>
        public clsKokaMagazina OKokaMagazina
        {
            get { return oKokaMagazina; }
            set { oKokaMagazina = value; }
        }

        /// <summary>
        /// dokumenti i rezervimit
        /// </summary>
        public clsKokaRezervime OKokaRezervime
        {
            get
            {
                return oKokaRezervime;
            }
            set
            {
                oKokaRezervime = value;
            }
        }

        /// <summary>
        /// afati kohor i urdher porosise
        /// </summary>
        public DateTime AfatKohor
        {
            get
            {
                return afatKohor;
            }
            set
            {
                afatKohor = value;
            }
        }

        public colGjendjeKlientFurnitor OGjendjeKF
        {
            get { return oGjendjeKF; }
            set { oGjendjeKF = value; }
        }

        /// <summary>
        /// id e perdoruesit qe ka krijuar dokumentin
        /// </summary>
        public int IdKrijuesi
        {
            get
            {
                return idKrijuesi;
            }
            set
            {
                idKrijuesi = value;
            }
        }

        /// <summary>
        /// username i perdoruesit qe ka krijuar dokumentin
        /// </summary>
        public string Krijuesi
        {
            get
            {
                return krijuesi;
            }
        }

        /// <summary>
        /// id e konfigurimit te dokumentit nga eshte transferuar
        /// </summary>
        public int IdKonfigTransferimi
        {
            get
            {
                return idKonfigTransferimi;
            }
            set
            {
                idKonfigTransferimi = value;
            }
        }

        /// <summary>
        /// id e dokumentit nga eshte transferuar
        /// </summary>
        public int IdTransferimi
        {
            get
            {
                return idTransferimi;
            }
            set
            {
                idTransferimi = value;
            }
        }

        /// <summary>
        /// kthen/vendos statusin e transferimit ne winlinekarta
        /// </summary>
        public StatusTrasferimi StatusTransferimi
        {
            get
            {
                return statusTransferimi;
            }
            set
            {
                statusTransferimi = value;
            }
        }

        public DateTime DtFillimi
        {
            get { return dtFillimi; }
        }

        public DateTime DtMbarimi
        {
            get { return dtMbarimi; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e automjetit.
        /// </summary>
        public int IdAutomjet
        {
            get { return idAutomjet; }
            set { idAutomjet = value; }
        }

        /// <summary>
        /// Kthen/Vendos kilometrazhin e automjetit kur behet sherbimi.
        /// </summary>
        public double KilometraAuto
        {
            get { return kilometraAuto; }
            set { kilometraAuto = value; }
        }

        public string Targa
        {
            get
            {
                return targa;
            }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e agjentit te shitjes.
        /// </summary>
        public int IdAgjenti2
        {
            get { return idAgjenti2; }
            set { idAgjenti2 = value; }
        }

        /// <summary>
        /// Kthen/Vendos perqindjen e agjentit te shitjes.
        /// </summary>
        public double PerqindjeAgjenti2
        {
            get { return perqindjeAgjenti2; }
            set { perqindjeAgjenti2 = value; }
        }

        public string KodAgjenti2
        {
            get
            {
                return kodAgjenti2;
            }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e agjentit te shitjes.
        /// </summary>
        public int IdAgjenti3
        {
            get { return idAgjenti3; }
            set { idAgjenti3 = value; }
        }

        /// <summary>
        /// Kthen/Vendos perqindjen e agjentit te shitjes.
        /// </summary>
        public double PerqindjeAgjenti3
        {
            get { return perqindjeAgjenti3; }
            set { perqindjeAgjenti3 = value; }
        }

        public string KodAgjenti3
        {
            get
            {
                return kodAgjenti3;
            }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e transportuesit
        /// </summary>
        public int IdTransportues
        {
            get { return idTransportues; }
            set { idTransportues = value; }
        }

        /// <summary>
        /// Kthen/Vendos marresin.
        /// </summary>
        public string Marresi
        {
            get { return marresi; }
            set { marresi = value; }
        }

        public string EmertimTr
        {
            get
            {
                return emertimTr;
            }
        }

        public int IdKarta
        {

            get { return idKarta; }
            set { idKarta = value; }
        }

        public int Pike
        {

            get { return pike; }
            set { pike = value; }
        }
        /// <summary>
        /// Kthen/Vendos nje objekt te tipit <see cref="DbCore.DbRegjistrim.clsDokumentLidhesKoka"/>
        /// </summary>
        public colDokumentLidhesKoka ODokumentLidhes
        {
            get { return oDokumentLidhes; }
            set { oDokumentLidhes = value; }
        }
        /// <summary>
        /// Kthen daten e fatures per te cilen po behet pagesa ne faturen aktuale
        /// </summary>
        public DateTime DtFature
        {
            get { return dtFature; }
        }

        public colFazaKontrate OColFazat
        {
            get { return ocolFazat; }
            set { ocolFazat = value; }
        }

        public int IdFaza
        {
            get { return idFaza; }
            set { idFaza = value; }
        }
        public string Shoferi
        {
            get { return shoferi; }
            set { shoferi = value; }
        }
        public string TargaShoferit
        {
            get { return targaSHF; }
            set { targaSHF = value; }
        }

        public IDictionary<string, object> HfArkiva { get; set; }

        public colKlienteFurnitore ColKlienteFurnitoreVartes { get; set; }

        public bool ZbritjeNeVlere
        {
            get { return zbritjeNeVlere; }
            set { zbritjeNeVlere = value; }
        }

        public double PerqindjeZbritje
        {
            get { return perqindjeZbritje; }
            set { perqindjeZbritje = value; }
        }

        public DateTime DtKrijimiPajisje
        {
            get { return dtKrijimiPajisje; }
            set { dtKrijimiPajisje = value; }
        }

        public string LlojiKf
        {
            get
            {
                return llojiKf;
            }

            set
            {
                llojiKf = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos koordinaten e klientit/furnitorit
        /// </summary>
        public string Koordinata
        {
            get
            {
                return koordinata;
            }
            set
            {
                this.koordinata = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos Niptin e klientit
        /// </summary>
        public string NiptK
        {
            get
            {
                return niptKlienti;
            }
            set
            {
                this.niptKlienti = value;
            }
        }

        public string QytetiK
        {
            get
            {
                return qytetiK;
            }

            set
            {
                qytetiK = value;
            }
        }

        public int IdKategoriSeriali
        {
            get
            {
                return idKategoriSeriali;
            }

            set
            {
                idKategoriSeriali = value;
            }
        }

        public string Shenime2
        {
            get { return shenime2; }
            set { shenime2 = value; }
        }

        public bool KartaPaPagese
        {
            get { return kartaPaPagese; }
            set { kartaPaPagese = value; }
        }

        public int IdDokTransferimNga
        {
            get { return idDokTransferimNga; }
            set { idDokTransferimNga = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e llojit te marreveshjes.
        /// </summary>
        public int IdLlojMarreveshje
        {
            get { return idLlojMarreveshje; }
            set { idLlojMarreveshje = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e marreveshjes.
        /// </summary>
        public string IdMarreveshje
        {
            get { return idMarreveshje; }
            set { idMarreveshje = value; }
        }

        /// <summary>
        /// Kthen/Vendos statusin e marreveshjes.
        /// </summary>
        public StatusMarreveshje StatusMarreveshje { get; set; }

        public string KerkuarNga
        {
            get { return kerkuarNga; }
            set { kerkuarNga = value; }
        }

        public DateTime DateKerkese
        {
            get { return dateKerkese; }
            set { dateKerkese = value; }
        }

        /// <summary>
        /// emri i klientit, fushe tekst qe shtohet per informacion
        /// </summary>
        public string NrDokMagazine
        {
            get { return nrDokMagazine; }
            set { nrDokMagazine = value; }
        }

        public string IIC
        {
            get { return iic; }
            set { iic = value; }
        }

        public string NIVF
        {
            get { return nivf; }
            set { nivf = value; }
        }

        public int IdOperator
        {
            get { return idOperator; }
            set { idOperator = value; }
        }
        public string NivfKthim
        {
            get { return nivfKthim; }
            set { nivfKthim = value; }
        }
        public string EIC
        {
            get { return eic; }
            set { eic = value; }
        }
        public string EinStatus
        {
            get { return einStatus; }
            set { einStatus = value; }
        }
        public int Procesi
        {
            get { return procesi; }
            set { procesi = value; }
        }
        public int EInvoiceType
        {
            get { return eInvoiceType; }
            set { eInvoiceType = value; }
        }
        public string TipiIVetefaturimit
        {
            get { return tipiIVetefaturimit; }
            set { tipiIVetefaturimit = value; }
        }

        #endregion

        #region Metoda Publike

        public clsMesazh krijoShitje(ref bool gjeneroDokMag, int idNiv, int idtempl, int idKonf, int idKlFurn, string kodKlFurn, int idProj, string nrProj, DateTime dtDk, string nrDk, string nrserial, DateTime dtMat, int idMon, string kodMon, double krs, int idMenTrans, string kodMenTrans, DateTime dttrans, int idKushtDerg, string kodKushtDerg, int idAgj, string kodAgj, int idMenPag, string kodMenPag, int idKushtPag, string kodKushtPag, double zbr, double tot, double tv, DateTime dtregj, int idSt, int idNder, int idNdVt, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues, int idDokNga, string adrFaturim, string adrDergim, string pershkrimi, bool dogana, int iddegeadministrative, string koddegeadministrative, int idPikeshitjefurnizimi, string kodPikeshitjefurnizimi, int idperdoruesi, int idrap, colTrupiShitje trupi, bool eshteShitje, string kodkonfigurimi, int idperiudha, clsKonfigurimAmbjenti konfmag, int idmag, string kodmag, bool mekontabilizim, int idgrup1, int idgrup2, int idgrup3, DateTime afatikohor, double cash, StatusAprovimi statusAprovimi, int idkrijuesi, double PerqindjeAgj, out string shfaqmesazhapolupe, IDictionary<string, object> hfArkiva, DbQendraKosto.colTrupiQendraKosto trupivjeterqendra, int idkokaekzistueserezervim, out string mesazhinformues, bool gjeneromeme, clsKokaShitje kokameme, int idtransferimi, int idkonfigtransferimi, bool eshtememe, bool gjenerobij, StatusTrasferimi statustrasferimi, string emerklienti, string kontakti, bool kase, bool kupon, string kodgrup1, DateTime dtfill, DateTime dtmb, int idAutomjet, double kilometraAutomjet, string targa, int idAgjenti2, double perqindjeAgjenti2, string kodAgjenti2, int idAgjenti3, double perqindjeAgjenti3, string kodAgjenti3, string marresi, int idTransportues, string emertimTr, bool faturepermbledhese, clsKokaShitje faturashitjengaurdhershitjamekupontatimor, bool shpenzimeJoTeZbritshme, int idArka, bool kontrollEkzistence, bool tollona, bool autoklient, bool ngaImporti, DateTime dtfat, bool gjeneruar, bool tollonkastrati, bool tollonakastratielektronik, int idMuajRaportimi, int idVitRaportimi, string shoferi, string targashoferit, bool zbritjeNeVlere, double perqindjeZbritje, int idKarta, int pike, colFazaKontrate ocolFazat, int idFaza, colKlienteFurnitore colKlienteFurnitoreVartes, DateTime dtKrijimiPajisje, DbData dbData, string llojzevendesimi, string koordinata, bool blerengadealeri, bool krijoartri, int idGjuha, clsKonfigurimAmbjenti konfvfone, int idstatusvjeter, string niptKlienti, string qytetiK, bool kontrolloGjendje, int idKategoriSeriali, colSerialeUnikeMagazina serialeUnike, string shenime2, bool kartaPaPagese, int idDokTransferimNga, bool eshteDokKthimi, int idLlojMarreveshje, string marrveshje, StatusMarreveshje statusMarreveshje, string kerkuarNga, string shtimModifikim, DateTime dateKerkese, bool merrMagazinePerberesi, string nrDokPerMag, string Iic, string Nivf, int operatori, string nivfKthim, string eic, string einStatus,int procesi,int einvoicetype, string tipiivetefaturimit)
        {
            ImbLogger.LogTraceShitje($"Filloi krijimi dokumentit te shitje/blerje me parametra idNiv:{idNiv}, idtempl:{idtempl}, idKonf:{idKonf}, idKlFurn:{idKlFurn}, kodKlFurn:" + kodKlFurn + $", idProj:{idProj}, nrProj:" + nrProj + $", dtDk:{dtDk}, nrDk: " + nrDk + $", nrserial:" + nrserial + $", dtMat:{dtMat}, idMon:{idMon}, kodMon:" + kodMon + $", krs:{krs}, idAgj:{idAgj}, kodAgj:" + kodAgj + $", idNder:{idNder}, idNdVt:{idNdVt},idNivelGjenerues:{idNivelGjenerues},idKonfigGjenerues:{idKonfigGjenerues},idGjenerues:{idGjenerues},idDokNga:{idDokNga},adrFaturim:{adrFaturim},adrDergim:{adrDergim},pershkrimi:{pershkrimi},dogana:{dogana},iddegeadministrative:{iddegeadministrative},koddegeadministrative:{koddegeadministrative},idPikeshitjefurnizimi:{idPikeshitjefurnizimi},kodPikeshitjefurnizimi:{kodPikeshitjefurnizimi}, idperdoruesi:{idperdoruesi},idrap:{idrap},trupi:{JsonConvert.SerializeObject(trupi)},eshteShitje:{eshteShitje},kodkonfigurimi:{kodkonfigurimi}, idperiudha:{idperiudha}, konfmag:{JsonConvert.SerializeObject(konfmag)}, idmag:{idmag}, kodmag:{kodmag}, mekontabilizim:{mekontabilizim}, idgrup1:{idgrup1}, idgrup2:{idgrup2}, idgrup3:{idgrup3}, afatikohor;{JsonConvert.SerializeObject(afatikohor)}, cash:{cash}, statusAprovimi:{JsonConvert.SerializeObject(statusAprovimi)}, idkrijuesi:{idkrijuesi}, PerqindjeAgj:{PerqindjeAgj},idkokaekzistueserezervim:{idkokaekzistueserezervim}, gjeneromeme:{gjeneromeme}, kokameme:{kokameme},idtransferimi:{idtransferimi},idkonfigtransferimi:{idkonfigtransferimi}, eshtememe:{eshtememe},gjenerobij:{gjenerobij}, statustrasferimi:{JsonConvert.SerializeObject(statusTransferimi)}, emerklienti:{emerklienti}, kontakti:{kontakti}, kase:{kase}, kupon:{kupon}, kodgrup1:{kodgrup1}, dtfill:{dtfill}, dtmb:{dtmb}, idAutomjet:{idAutomjet}, kilometraAutomjet:{kilometraAutomjet}, targa:{targa}, idAgjenti2:{idAgjenti2}, perqindjeAgjenti2:{PerqindjeAgjenti2}, kodAgjenti2:{kodAgjenti2}, idAgjenti3:{idAgjenti3}, perqindjeAgjenti3:{perqindjeAgjenti3}, kodAgjenti3:{KodAgjenti3},marresi:{marresi},idTransportues:{idTransportues}, emertimTr:{emertimTr}, faturepermbledhese:{faturepermbledhese}, faturashitjengaurdhershitjamekupontatimor:{JsonConvert.SerializeObject(faturashitjengaurdhershitjamekupontatimor)}, shpenzimeJoTeZbritshme:{shpenzimeJoTeZbritshme},idArka:{idArka},kontrollEkzistence:{kontrollEkzistence}, tollona:{tollona},autoklient:{autoklient}, ngaImporti:{ngaImporti}, dtfat:{dtfat}, gjeneruar:{gjeneruar}, tollonkastrati;{tollonkastrati}, tollonakastratielektronik:{tollonakastratielektronik}, idMuajRaportimi:{idMuajRaportimi}, idVitRaportimi:{idVitRaportimi}, shoferi:{shoferi}, targashoferit:{targashoferit}, zbritjeNeVlere:{ZbritjeNeVlere}, perqindjeZbritje;{perqindjeZbritje}, idKarta:{idKarta}, pike:{pike}, ocolFazat:{JsonConvert.SerializeObject(ocolFazat)}, idFaza:{idFaza}, dtKrijimiPajisje:{dtKrijimiPajisje}, dbData:{dbData},llojzevendesimi:{llojzevendesimi}, koordinata:{koordinata}, blerengadealeri:{blerengadealeri},krijoartri:{krijoartri}, idGjuha:{idGjuha}, konfvfone:{JsonConvert.SerializeObject(konfvfone)}, idstatusvjeter:{idstatusvjeter}, niptKlienti:{niptKlienti}, qytetiK:{qytetiK},kontrolloGjendje:{kontrolloGjendje}, idKategoriSeriali:{idKategoriSeriali},serialeUnike:{serialeUnike}, shenime2:{shenime2}, kartaPaPagese:{kartaPaPagese},idDokTransferimNga:{idDokTransferimNga}, eshteDokKthimi:{eshteDokKthimi}, idLlojMarreveshje:{ idLlojMarreveshje}, marrveshje: {marrveshje}, marrAktive {statusMarreveshje}, kerkuarNga {kerkuarNga}, datekerkese {dateKerkese}, IIC: {Iic}, NIVF: {Nivf}.");
            mesazhinformues = string.Empty;
            shfaqmesazhapolupe = "jo";
            perqindjeAgjenti = PerqindjeAgj;
            idNivel = idNiv;
            idTemplate = idtempl;
            idKonfigAmbjente = idKonf;
            idKlientFurnitor = idKlFurn;
            kodKlientFurnitor = kodKlFurn;
            idProjekt = idProj;
            nrProjekt = nrProj;
            nrDok = nrDk;
            nrSerial = nrserial;
            dtDok = dtDk;
            dtMaturimi = dtMat;
            idMonedha = idMon;
            kodMonedha = kodMon;
            kursi = krs;
            faturePermbledhese = faturepermbledhese;
            idMenyreTransporti = idMenTrans;
            this.kodMenyreTransporti = kodMenTrans;
            dtTransportimi = dttrans;
            idKushtDergimi = idKushtDerg;
            kodKushtDergimi = kodKushtDerg;
            idAgjent = idAgj;
            kodAgjenti = kodAgj;
            idMenyrePagese = idMenPag;
            kodMenyrePagese = kodMenPag;
            idKushtPagese = idKushtPag;
            kodKushtPagese = kodKushtPag;
            zbritje = zbr;
            totali = tot;
            this.kase = kase;
            this.kupon = kupon;
            idGrup1 = idgrup1;
            idGrup2 = idgrup2;
            idGrup3 = idgrup3;
            this.idTransferimi = idtransferimi;
            this.idKonfigTransferimi = idkonfigtransferimi;
            tvsh = tv;
            dtRegjistrimi = dtregj;
            idStatusDok = idSt;
            idNdermarje = idNder;
            idNdermarjeVit = idNdVt;
            afatKohor = afatikohor;
            this.idNivelGjenerues = idNivelGjenerues;
            this.idKonfigGjenerues = idKonfigGjenerues;
            this.idGjenerues = idGjenerues;
            this.idDokNga = idDokNga;
            this.adresaFaturimit = adrFaturim;
            this.adresaDergimit = adrDergim;
            this.pershkrimi = pershkrimi;
            this.dogana = dogana;
            ColKlienteFurnitoreVartes = colKlienteFurnitoreVartes;
            this.idDegeAdministrative = iddegeadministrative;
            kodDegeAdministrative = koddegeadministrative;
            this.idPikeShitjeFurnizimi = idPikeshitjefurnizimi;
            kodPikeShitje = kodPikeshitjefurnizimi;
            idPerdoruesi = idperdoruesi;
            idRaportDesing = idrap;
            oColTrupiShitje = trupi;
            this.cash = cash;
            this.emerKlienti = emerklienti;
            this.kontakti = kontakti;
            this.statusTransferimi = statustrasferimi;
            this.statusAprovimi = statusAprovimi;
            this.idKrijuesi = idkrijuesi;
            OFleteKontabel = new clsKokaFleteKontabel();
            oGjendjeKF = new colGjendjeKlientFurnitor();
            oArkiva = new colArkiva();
            colGaranci = new colGaranciArtikulli();
            dtFillimi = dtfill;
            dtMbarimi = dtmb;
            this.idAutomjet = idAutomjet;
            this.kilometraAuto = kilometraAutomjet;
            this.targa = targa;
            this.idAgjenti2 = idAgjenti2;
            this.kodAgjenti2 = kodAgjenti2;
            this.perqindjeAgjenti2 = perqindjeAgjenti2;
            this.idAgjenti3 = idAgjenti3;
            this.kodAgjenti3 = kodAgjenti3;
            this.perqindjeAgjenti3 = perqindjeAgjenti3;
            this.marresi = marresi;
            this.idTransportues = idTransportues;
            this.emertimTr = emertimTr;
            this.shpenzimeJoTeZbritshme = shpenzimeJoTeZbritshme;
            this.idarka = idArka;
            this.gjeneruar = gjeneruar;
            this.idKarta = idKarta;
            this.pike = pike;
            ODokumentLidhes = new colDokumentLidhesKoka();
            dtFature = dtfat;
            this.muajRaportimi = idMuajRaportimi;
            this.idVitRaportimi = idVitRaportimi;
            this.ocolFazat = ocolFazat;
            this.idFaza = idFaza;
            this.shoferi = shoferi;
            this.targaSHF = targashoferit;
            this.zbritjeNeVlere = zbritjeNeVlere;
            this.perqindjeZbritje = perqindjeZbritje;
            this.dtKrijimiPajisje = dtKrijimiPajisje;
            this.koordinata = koordinata;
            this.niptKlienti = niptKlienti;
            this.qytetiK = qytetiK;
            this.idKategoriSeriali = idKategoriSeriali;
            this.shenime2 = shenime2;
            this.kartaPaPagese = kartaPaPagese;
            this.IdDokTransferimNga = idDokTransferimNga;
            this.idLlojMarreveshje = idLlojMarreveshje;
            this.idMarreveshje = marrveshje;
            StatusMarreveshje = statusMarreveshje;
            this.kerkuarNga = kerkuarNga;
            this.dateKerkese = dateKerkese;
            this.nrDokMagazine = nrDokPerMag;
            this.iic = Iic;
            this.nivf = Nivf;
            this.idOperator = operatori;
            this.nivfKthim = nivfKthim;
            this.eic = eic;
            this.einStatus = einStatus;
            this.procesi = procesi;
            this.eInvoiceType = einvoicetype;
            this.tipiIVetefaturimit = tipiivetefaturimit;
            var dbShare = new clsDatabaseShare(dbData);

            var vlera = clsAlternativaKushti.getVleraSipasId(idKonf, "PVMPDMVP");
            if (vlera != 0 && vlera != null)
            {
                if ((Convert.ToDouble(totali) < vlera && Convert.ToDouble(totali) > 0) || Convert.ToDouble(totali) == 0)
                {
                    ImbLogger.LogErrorShitje("Ky dokument ka vlere me te vogel se vlera e lejuar!");
                    throw new MyException("Ky dokument ka vlere me te vogel se vlera e lejuar!");

                }
            }
            #region Marveshja

            if (clsAlternativaKushti.getAlternativa(idKonf, "KMKV", dbShare) == "Po")
                ValidoMarreveshje();

            #endregion


            #region krijimi i garancise
            ImbLogger.LogTraceShitje("Po fillon krijimi garancise");
            if (clsAlternativaKushti.getAlternativa(idKonf, "RGA", dbShare) == "Po")
            {
                var mesazhGarancie = GjeneroGaranci(serialeUnike, new clsPerdorues(this.idKrijuesi).PerdoruesUsername);
                if (!mesazhGarancie)
                    return mesazhGarancie;
            }

            ImbLogger.LogTraceShitje("Mbaroi krijimi i garancise.");
            #endregion

            #region Tollonat
            clsDatabaseRegjistrim dbR = new clsDatabaseRegjistrim(dbData);
            bool kontrollokupon = true;
            bool aplikonrserialneruajte = clsAlternativaKushti.getAlternativa(IdKonfigAmbjente, "ANRSVSR", dbShare) == "Po";
            bool lejoSasiPozitiveKthim = clsAlternativaKushti.getAlternativa(IdKonfigAmbjente, "LSPK", dbShare) == "Po";
            if (kupon && idSt == 1 && clsAlternativaKushti.getAlternativa(idKonf, "GJDSH", dbShare) == "Po")
                kontrollokupon = false;///sepse do kontrollohet tek fatura dhe jo tek urdheri
            if (clsNivelRegjistrimi.ktheKodNivelRegjistrimi(idNiv, dbR) != "FSH")
                kontrollokupon = false;
            clsMesazh mesazh = kontrollo(tollona, tollonkastrati, tollonakastratielektronik, autoklient, dbData, llojzevendesimi, kontrollokupon, eshteDokKthimi, aplikonrserialneruajte, lejoSasiPozitiveKthim);
            if (!mesazh.Status)
                return mesazh;
            if (ngaImporti)
            {
                bool kaNdryshimNrAuto;
                mesazh = kontrolloShitje(out kaNdryshimNrAuto, dbR, null, false, kontrollEkzistence);
                if (!mesazh.Status)
                    return mesazh;
            }

            #endregion

            #region magazina
            oKokaMagazina = new clsKokaMagazina();
            // oKokaRezervime = new clsKokaRezervime();
            oKokaMagazina.OFleteKontabel = new clsKokaFleteKontabel();

            clsKokaShitje shitjePerRezervim = this;
            bool rezervimOwn = false;
            int idNderOwn = clsNdermarrje.merrIdNdermarrjeOwn();
            string dega = koddegeadministrative;
            if (idNderOwn == idNdermarje && eshteShitje && IdDokTransferimNga != 0)
            {
                shitjePerRezervim = new clsKokaShitje(IdDokTransferimNga);
                shitjePerRezervim.mbushTrupShitje();
                rezervimOwn = true;
                dega = clsDegeAdministrative.mbushKodiDegeAdministrativeSipasiD(shitjePerRezervim.idDegeAdministrative);
            }
            if (gjeneroDokMag)
            {

                //gjenerohet dokumenti i magazines po pa fleten kontabel te magazines sepse varet nga cmimi i daljes dhe gjenerohet kur ruhet dokumenti i magazines
                mesazh = krijoMagazineNgaShitja(ref gjeneroDokMag, eshteShitje, konfmag, idmag, kodmag, idkokaekzistueserezervim, out mesazhinformues, dbData, blerengadealeri, krijoartri, idGjuha, idstatusvjeter, kontrolloGjendje, serialeUnike, shitjePerRezervim, rezervimOwn, merrMagazinePerberesi);
                if (!mesazh.Status)
                    return mesazh;
            }
            #endregion

            this.HfArkiva = hfArkiva;
            oFleteKontabel = new clsKokaFleteKontabel();
            oGjendjeKF = new colGjendjeKlientFurnitor();

            #region rezervimi

            if (clsAlternativaKushti.getAlternativa(idKonfigAmbjente, "GJDR", dbShare) == "Po")
            {
                string alternativa = clsAlternativaKushti.getAlternativa(idKonfigAmbjente, "GJDRVD", dbShare);///kushti per te gjeneruar dokument rezervimi vetem kur eshte draft per vfone porosi
                if ((idStatusDok == 0 && alternativa == "Po") || (idstatusvjeter == 0 && alternativa == "Po") || alternativa == "Jo" || string.IsNullOrWhiteSpace(alternativa))
                {
                    clsKusht kushtkonfrezervim = new clsKusht(IdKonfigAmbjente, "ZKR", dbShare);
                    colTrupiRezervime coltrupi = new colTrupiRezervime(trupi, dtDk, idSt, idKonfigAmbjente, false, new clsDatabaseInventari(dbData), this.Zbritje, merrMagazinePerberesi);
                    oKokaRezervime = new clsKokaRezervime();
                    if (coltrupi.Count > 0)
                    {                        
                        mesazh = oKokaRezervime.krijoRezervim(clsKonfigurimAmbjenti.ktheIdNivel(kushtkonfrezervim.Vlera, dbShare), kushtkonfrezervim.Vlera, eshteShitje ? idKlFurn : 0, eshteShitje ? kodKlFurn : string.Empty, idmag == -1 ? coltrupi[0].IdMag : idmag, kodmag, dtDk, nrDk, (idStatusDok == 0 && alternativa == "Po") ? 1 : idStatusDok, idNder, idNdVt, idperdoruesi, dtregj, pershkrimi, iddegeadministrative, koddegeadministrative, 0, 1, 0, 0, 0, idNiv, idKonf, 0, coltrupi, new clsKokaRezervime());
                        if (!mesazh.Status)
                            return mesazh;
                    }
                }
            }

            #endregion

            #region rezervim ne own shop
            if (clsAlternativaKushti.getAlternativa(idKonfigAmbjente, "GJDRONSH", dbShare) == "Po" && idStatusDok == 1)
            {
                oKokaRezervime = new clsKokaRezervime();
                mesazh = clsKokaShitje.mungojneEntiteteNeNdermarrje(trupi, idKlFurn, idNderOwn);
                if (!mesazh.Status)
                    return mesazh;
                mesazh = clsArtikulli.kontrolloNeseArtikulliNeOwnKaTeLIdhurMagazine(trupi);
                if (!mesazh.Status)
                    return mesazh;
                colTrupiRezervime coltrupi = new colTrupiRezervime(trupi, dtDk, idSt, idKonfigAmbjente, true, new clsDatabaseInventari(dbData), this.Zbritje, merrMagazinePerberesi);
                clsKusht kushtkonfrezervim = new clsKusht(IdKonfigAmbjente, "ZKR", dbShare);
                if (coltrupi.Count > 0)
                {
                    string artikulli = clsArtikulli.ktheTeDhenaPerArtikullin(coltrupi[0].KodiArtikull);
                    string[] VleratMagOwn = artikulli.Split(',');
                    int idmagOwn = Int32.Parse(VleratMagOwn[0]);
                    kodmag = VleratMagOwn[1];
                    idNdVt = clsNdermarrjeViti.ktheIdNdermarrjeVitiSipasNdermarjesDheKodVitit(idNderOwn, this.DtDok.Year);
                    kushtkonfrezervim.Vlera = clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(clsKonfigurimAmbjenti.ktheKodKonfigurimi(kushtkonfrezervim.Vlera), idNderOwn);
                    mesazh = oKokaRezervime.krijoRezervim(clsKonfigurimAmbjenti.ktheIdNivel(kushtkonfrezervim.Vlera, dbShare), kushtkonfrezervim.Vlera, eshteShitje ? idKlFurn : 0, eshteShitje ? kodKlFurn : string.Empty, idmagOwn, kodmag, dtDk, nrDk, idStatusDok, idNderOwn, idNdVt, idperdoruesi, dtregj, pershkrimi, iddegeadministrative, koddegeadministrative, 0, 1, 0, 0, 0, idNiv, idKonf, 0, coltrupi, new clsKokaRezervime());
                }
                if (!mesazh.Status)
                    return mesazh;
            }

            #endregion

            #region gjenerimi i memes
            bool gjeneroUSHMeme = clsAlternativaKushti.getAlternativa(idKonf, "GJUSHNMD", dbShare) == "Po";
            if ((gjeneromeme && idStatusDok == 1) || (gjeneroUSHMeme && idStatusDok == 0 && shtimModifikim == "shtim"))
            {
                ImbLogger.LogTraceShitje("Fillon gjenerimi meme");
                clsNdermarrje nderm = new clsNdermarrje(idNder);
                clsNdermarrje ndermpr = new clsNdermarrje(nderm.OwnShop ? clsNdermarrje.ktheIdNdermarrjeMeme() : nderm.IdPrindi);
                clsKonfigurimAmbjenti konf = new clsKonfigurimAmbjenti();
                konf.mbushKonfigAmbjSipasKod(gjeneroUSHMeme ? "USH" : "OSH", ndermpr.IdNdermarrje);
                clsKlientFurnitor klientmeme = new clsKlientFurnitor();
                int llojporosie = 0;
                if (!(gjeneromeme && idStatusDok == 1) && (gjeneroUSHMeme && idStatusDok == 0))
                {
                    kodgrup1 = "Aparate";
                }

                if (string.IsNullOrEmpty(kodgrup1))
                {
                    ImbLogger.LogWarningShitje("Grupimi i blerjes eshte :" + Convert.ToString(kodgrup1));
                    return new MesazhGabimi("Ju lutem plotesoni grupimin e blerjes!");
                }

                ImbLogger.LogTraceShitje("Grupimi i blerjes eshte :" + Convert.ToString(kodgrup1));
                switch (kodgrup1)
                {
                    case "Aparate":
                        llojporosie = 1;
                        break;
                    case "Karta":
                        llojporosie = 2;
                        break;
                    case "Loan":
                        llojporosie = 3;
                        break;
                    case "Dhurate":
                        llojporosie = 4;
                        break;
                    case "Te gjitha":
                        llojporosie = 5;
                        break;
                    case "Aparate ekspozitore":
                        llojporosie = 6;
                        break;
                    default:
                        llojporosie = 0;
                        break;
                }

                klientmeme.KtheKlientFurnitorSipasNdermarjeBij(idNder, ndermpr.IdNdermarrje, llojporosie);
                if (klientmeme.IdKlientFurnitor < 1)
                {
                    ImbLogger.LogErrorShitje("Klineti me ID: " + klientmeme.IdKlientFurnitor + " nuk ekziston tek ndermarrja meme");
                    throw new MyException("Nuk ekzistoni ju si klient tek ndermarja meme!");
                }

                clsLlogari llog = new clsLlogari(klientmeme.IdLlogari);
                int idMonedh = llog.IdMonedha;
                string kodiMon = clsMonedha.ktheKodMonedheSipasId(llog.IdMonedha);
                //clsMonedha mon = new clsMonedha(llog.IdMonedha);

                clsNdermarrjeViti nderviti = new clsNdermarrjeViti(idNdVt);
                clsViti viti = new clsViti(nderviti.IdViti);
                clsViti vitiprind = new clsViti();
                vitiprind.mbushVitetMet(viti.KodiViti, ndermpr.IdNdermarrje);
                clsNdermarrjeViti ndervitiprind = new clsNdermarrjeViti();
                ndervitiprind.mbushNdermarrjeVitiSipasNdermarjesDheVitit(ndermpr.IdNdermarrje, vitiprind.IdViti);
                clsKurset kursimeme = new clsKurset(idMonedh, dtDk);
                if (kursimeme.VleraKursi == 0)
                    kursimeme.VleraKursi = 1.00;
                int idPeriudhaKont = clsPeriudhaKontabel.ktheIdPeriudheSipasDatesDheNdermarrjes(dtDk, ndermpr.IdNdermarrje);
                if (kodiMon != kodMon)
                {
                    ImbLogger.LogErrorShitje("Nuk mund te transferoni veprime ne monedha te ndryshme!");
                    throw new MyException("Nuk mund te transferoni veprime ne monedha te ndryshme!");
                }

                // ndryshimi per shtimin e logeve per kete do behet ne clsMesazh               
                clsDegeAdministrative dege = new clsDegeAdministrative();
                dege.Kodi = string.Empty;
                if (iddegeadministrative > 0 && !gjeneroUSHMeme)
                {
                    dege = new clsDegeAdministrative(iddegeadministrative);
                    mesazh = dege.kontrollotransferim(dege, ndermpr.IdNdermarrje, new DbRegjistrim.clsDatabaseRegjistrim(), idperdoruesi);
                    if (!mesazh.Status)
                    {
                        return mesazh;
                    }
                }
                mesazh = clsArtikulli.ekzistojneArtikujtNeNdermarrje(trupi.Select(x => x.Kodi).Distinct().ToList(), ndermpr.IdNdermarrje);
                if (!mesazh.Status)
                {
                    return mesazh;
                }

                int idTransferimiMeme = gjeneroUSHMeme && idStatusDok == 0 && shtimModifikim == "shtim" ? 0 : idtransferimi;
                int idKonfigTransferimiMeme = gjeneroUSHMeme && idStatusDok == 0 && shtimModifikim == "shtim" ? 0 : idKonf;

                clsGrupimDokumentiKoka grup = new clsGrupimDokumentiKoka(kodgrup1, ndermpr.IdNdermarrje, 1, idperdoruesi, dbR);
                mesazh = kokameme.krijoShitje(ref gjeneroDokMag, konf.IdNivel, idtempl, konf.IdKonfigAmbjente, klientmeme.IdKlientFurnitor, klientmeme.KodKlientFurnitor, idProj, nrProj, dtDk, nrDk + "-" + nderm.NdermarrjeKodi, (gjeneroUSHMeme && idStatusDok == 0 && shtimModifikim == "shtim") ? "" : nrserial, dtMat, idMonedh, kodiMon, kursimeme.VleraKursi, 0, "", dttrans, 0, "", 0, "", idMenPag, kodMenPag, 0, "", zbr, tot, tv, dtregj, 1, ndermpr.IdNdermarrje, ndervitiprind.IdNderViti, idNivelGjenerues, idKonfigGjenerues, idGjenerues, idDokNga, "", "", pershkrimi, dogana, dege.IdDegeAdministrative, dege.Kodi, 0, "", idperdoruesi, idrap, trupi, !eshteShitje, konf.KodKonfigAmbjente, idPeriudhaKont, konfmag, 0, "", mekontabilizim, grup.IdGrupimKoka, 0, 0, afatikohor, cash, statusAprovimi, idperdoruesi, 0, out shfaqmesazhapolupe, hfArkiva, trupivjeterqendra, idkokaekzistueserezervim, out mesazhinformues, false, new clsKokaShitje(), idTransferimiMeme, idKonfigTransferimiMeme, false, false, StatusTrasferimi.PaTransferuar, emerklienti, kontakti, kase, kupon, kodgrup1, dtfill, dtmb, 0, 0, "", 0, 0, "", 0, 0, "", "", 0, "", faturepermbledhese, new clsKokaShitje(), shpenzimeJoTeZbritshme, idarka, false, tollona, autoklient, false, dtfat, false, tollonkastrati, tollonakastratielektronik, idMuajRaportimi, vitiprind.IdViti, shoferi, targaSHF, zbritjeNeVlere, perqindjeZbritje, 0, 0, new colFazaKontrate(), 0, new colKlienteFurnitore(), DateTime.Now, dbData, llojzevendesimi, koordinata, blerengadealeri, krijoartri, idGjuha, new clsKonfigurimAmbjenti(), -1, niptKlienti, qytetiK, false, idKategoriSeriali, serialeUnike, shenime2, kartaPaPagese, idDokTransferimNga, eshteDokKthimi, idLlojMarreveshje, idMarreveshje, StatusMarreveshje, kerkuarNga, shtimModifikim, dateKerkese, false, nrDk, iic, nivf, idOperator,nivfKthim,eic,einStatus,procesi,einvoicetype,tipiivetefaturimit);
                if (!mesazh.Status)
                    return mesazh;
            }

            #endregion

            #region gjenerimi i bijes
            if (eshtememe && idStatusDok == 1 && gjenerobij)
            {
                ImbLogger.LogTraceShitje("Fillon gjenerimi i bijes");
                clsKlientFurnitor kf = new clsKlientFurnitor(idKlFurn);
                int idndermarjebij = kf.IdNdermarjeBij;
                if (idndermarjebij > 0)
                {
                    //clsNdermarrje nderm = new clsNdermarrje(idndermarjebij);
                    clsNdermarrje ndermpr = new clsNdermarrje(idNder);
                    clsKonfigurimAmbjenti konf = new clsKonfigurimAmbjenti();
                    string kodNivelRegjistrimi = clsNivelRegjistrimi.ktheKodNivelRegjistrimi(idNiv);

                    if (kodNivelRegjistrimi == "USH")
                        konf.mbushKonfigAmbjSipasKod("UB", idndermarjebij);
                    else
                        konf.mbushKonfigAmbjSipasKod("FB", idndermarjebij);//// konf.mbushKonfigAmbjSipasKod("FBmagazine", idndermarjebij);
                    clsKlientFurnitor furnitoribij = new clsKlientFurnitor();
                    furnitoribij.MbushKlientFurnitor(ndermpr.KodFurnitori, idndermarjebij);

                    if (furnitoribij.IdKlientFurnitor < 1)
                    {
                        ImbLogger.LogErrorShitje("Nuk ekzistoni furnitori prind tek bija, ku id = " + Convert.ToString(furnitoribij.IdKlientFurnitor));
                        throw new MyException("Nuk ekzistoni furnitori prind tek bija!");
                    }
                    clsLlogari llog = new clsLlogari(furnitoribij.IdLlogari);
                    int idMonedh = llog.IdMonedha;
                    string kodiMon = clsMonedha.ktheKodMonedheSipasId(llog.IdMonedha);
                    //clsMonedha mon = new clsMonedha(llog.IdMonedha);
                    clsNdermarrjeViti nderviti = new clsNdermarrjeViti(idNdVt);
                    clsViti viti = new clsViti(nderviti.IdViti);
                    clsViti vitiprind = new clsViti();
                    //deri ketu
                    vitiprind.mbushVitetMet(viti.KodiViti, idndermarjebij);
                    clsNdermarrjeViti ndervitiprind = new clsNdermarrjeViti();
                    ndervitiprind.mbushNdermarrjeVitiSipasNdermarjesDheVitit(idndermarjebij, vitiprind.IdViti);
                    clsKurset kursimeme = new clsKurset(idMonedh, dtDk);
                    if (kursimeme.VleraKursi == 0)
                    {
                        ImbLogger.LogTraceShitje("Vlera e kursit meme = " + Convert.ToString(kursimeme.VleraKursi));
                        kursimeme.VleraKursi = 1.00;
                    }

                    //clsPeriudhaKontabel per = new clsPeriudhaKontabel(dtDk, idndermarjebij);
                    int idPeriudhaKont = clsPeriudhaKontabel.ktheIdPeriudheSipasDatesDheNdermarrjes(dtDk, idndermarjebij);
                    if (kodiMon != kodMon)
                    {
                        ImbLogger.LogErrorShitje("Nuk mund te transferoni veprime ne monedha te ndryshme! \n kodiMon = " + Convert.ToString(kodiMon) + " dhe kodMon = " + Convert.ToString(kodMon));
                        throw new MyException("Nuk mund te transferoni veprime ne monedha te ndryshme!");
                    }

                    clsKonfigurimAmbjenti konfmagbij = new clsKonfigurimAmbjenti(konf.IdKonfigurimi);

                    clsDegeAdministrative dege = new clsDegeAdministrative();
                    dege.Kodi = string.Empty;
                    if (iddegeadministrative > 0)
                    {
                        dege = new clsDegeAdministrative(iddegeadministrative);
                        mesazh = dege.kontrollotransferim(dege, idndermarjebij, new DbRegjistrim.clsDatabaseRegjistrim(), idperdoruesi);
                        if (!mesazh.Status)
                        {
                            return mesazh;
                        }
                    }
                    if (string.IsNullOrEmpty(kodgrup1))
                    {
                        ImbLogger.LogWarningShitje("Grupimi i blerjes => " + Convert.ToString(kodgrup1));
                        return new MesazhGabimi("Ju lutem plotesoni grupimin e blerjes!");
                    }


                    mesazh = clsArtikulli.ekzistojneArtikujtNeNdermarrje(trupi.Select(x => x.Kodi).Distinct().ToList(), ndermpr.IdNdermarrje);
                    if (!mesazh.Status)
                    {
                        return mesazh;
                    }
                    clsGrupimDokumentiKoka grup = new clsGrupimDokumentiKoka(kodgrup1, idndermarjebij, 1, idperdoruesi, dbR);
                    mesazh = kokameme.krijoShitje(ref gjeneroDokMag, konf.IdNivel, idtempl, konf.IdKonfigAmbjente, furnitoribij.IdKlientFurnitor, furnitoribij.KodKlientFurnitor, idProj, nrProj, dtDk, nrDk, nrserial, dtMat, idMonedh, kodiMon, kursimeme.VleraKursi, 0, "", dttrans, 0, "", 0, "", idMenPag, kodMenPag, 0, "", zbr, tot, tv, dtregj, idSt, idndermarjebij, ndervitiprind.IdNderViti, idNivelGjenerues, idKonfigGjenerues, idGjenerues, idDokNga, "", "", pershkrimi, dogana, dege.IdDegeAdministrative, dege.Kodi, 0, "", idperdoruesi, idrap, trupi, !eshteShitje, konf.KodKonfigAmbjente, idPeriudhaKont, konfmagbij, idmag, kodmag, mekontabilizim, grup.IdGrupimKoka, 0, 0, afatikohor, cash, statusAprovimi, idperdoruesi, 0, out shfaqmesazhapolupe, hfArkiva, trupivjeterqendra, idkokaekzistueserezervim, out mesazhinformues, false, new clsKokaShitje(), idtransferimi, idKonf, false, false, DbRegjistrim.StatusTrasferimi.PaTransferuar, emerklienti, kontakti, kase, kupon, kodgrup1, dtfill, dtmb, 0, 0, "", 0, 0, "", 0, 0, "", "", 0, "", faturepermbledhese, new clsKokaShitje(), shpenzimeJoTeZbritshme, idarka, false, tollona, autoklient, false, dtfat, false, tollonkastrati, tollonakastratielektronik, idMuajRaportimi, vitiprind.IdViti, shoferi, targaSHF, zbritjeNeVlere, perqindjeZbritje, 0, 0, new colFazaKontrate(), 0, ColKlienteFurnitoreVartes, DateTime.Now, dbData, llojzevendesimi, koordinata, blerengadealeri, krijoartri, idGjuha, new clsKonfigurimAmbjenti(), -1, niptKlienti, qytetiK, false, idKategoriSeriali, serialeUnike, shenime2, kartaPaPagese, idDokTransferimNga, eshteDokKthimi, idLlojMarreveshje, idMarreveshje, StatusMarreveshje, kerkuarNga, shtimModifikim, dateKerkese, false, nrDk, iic, nivf, idOperator, nivfKthim, eic, einStatus, procesi, einvoicetype,tipiivetefaturimit);
                    if (!mesazh.Status)
                        return mesazh;
                }
                ImbLogger.LogTraceShitje("Perfundon gjenerimi i bijes");
            }
            #endregion

            #region fatura per kupon tatimor
            if (kupon && idSt == 1 && clsAlternativaKushti.getAlternativa(idKonf, "GJDSH") == "Po" && faturashitjengaurdhershitjamekupontatimor != null)
            {

                bool gjeneroDokMagshitje = false;
                clsKusht kushtkonfshitje = new clsKusht(idKonf, "ZKDSH");
                int idnrautonrdok = clsAtributeTrupi.merrNrAutomatikSipasKontrollitDheKonfigurimit(kushtkonfshitje.Vlera, "txtNumerSerial", 506);
                string nrdokserial = DbCore.DbAdmin.clsNrAutom.merrVlerenNrAutomatik(idnrautonrdok, dtDk);

                if (!String.IsNullOrEmpty(nrdokserial))//nqs ka nr automatik
                {
                    nrSerial = nrdokserial;

                }

                mesazh = faturashitjengaurdhershitjamekupontatimor.krijoShitje(ref gjeneroDokMagshitje, clsKonfigurimAmbjenti.ktheIdNivel(kushtkonfshitje.Vlera), idtempl, kushtkonfshitje.Vlera, idKlFurn, kodKlFurn, idProj, nrProj, dtDk, nrDk, nrSerial, dtMat, idMon, kodMon, krs, idMenTrans, kodMenTrans, dttrans, idKushtDerg, kodKushtDerg, idAgj, kodAgj, idMenPag, kodMenPag, idKushtPag, kodKushtPag, zbr, tot, tv, dtregj, idSt, idNder, idNdVt, idNivelGjenerues, idKonfigGjenerues, idGjenerues, idDokNga, adrFaturim, adrDergim, pershkrimi, dogana, iddegeadministrative, koddegeadministrative, idPikeshitjefurnizimi, kodPikeshitjefurnizimi, idperdoruesi, idrap, trupi, eshteShitje, "FSH", idperiudha, new clsKonfigurimAmbjenti(), 0, "", true, idgrup1, idgrup2, idgrup3, afatikohor, cash, statusAprovimi, idkrijuesi, PerqindjeAgj, out shfaqmesazhapolupe, hfArkiva, trupivjeterqendra, idkokaekzistueserezervim, out mesazhinformues, gjeneromeme, kokameme, idtransferimi, idkonfigtransferimi, eshtememe, gjenerobij, statustrasferimi, emerklienti, kontakti, kase, kupon, kodgrup1, dtfill, dtmb, idAutomjet, kilometraAutomjet, targa, idAgjenti2, perqindjeAgjenti2, kodAgjenti2, idAgjenti3, perqindjeAgjenti3, kodAgjenti3, marresi, idTransportues, emertimTr, faturepermbledhese, new clsKokaShitje(), shpenzimeJoTeZbritshme, idarka, false, tollona, autoklient, false, dtfat, gjeneruar, tollonkastrati, tollonakastratielektronik, idMuajRaportimi, idVitRaportimi, shoferi, targaSHF, zbritjeNeVlere, perqindjeZbritje, 0, 0, new colFazaKontrate(), 0, ColKlienteFurnitoreVartes, DateTime.Now, dbData, llojzevendesimi, koordinata, blerengadealeri, krijoartri, idGjuha, new clsKonfigurimAmbjenti(), -1, niptKlienti, qytetiK, false, idKategoriSeriali, serialeUnike, shenime2, kartaPaPagese, idDokTransferimNga, eshteDokKthimi, idLlojMarreveshje, idMarreveshje, StatusMarreveshje, kerkuarNga, shtimModifikim, dateKerkese, false, nrDk, iic, nivf, idOperator, nivfKthim, eic, einStatus, procesi, einvoicetype,tipiivetefaturimit);

                if (!mesazh.Status)
                    return mesazh;
            }
            #endregion

            ImbLogger.LogTraceShitje($"Mbaroi krijimi dokumnetit te shitje/blerje me parametra idNiv:{idNiv}, idtempl:{idtempl}, idKonf:{idKonf}, idKlFurn:{idKlFurn}, kodKlFurn:" + kodKlFurn + $", idProj:{idProj}, nrProj:" + nrProj + $", dtDk:{dtDk}, nrDk" + nrDk + $", nrserial:" + nrserial + $", dtMat:{dtMat}, idMon:{idMon}, kodMon:" + kodMon + $", krs:{krs}, idAgj:{idAgj}, kodAgj:" + kodAgj + $", idNder:{idNder}, idNdVt:{idNdVt},idNivelGjenerues:{idNivelGjenerues},idKonfigGjenerues:{idKonfigGjenerues},idGjenerues:{idGjenerues},idDokNga:{idDokNga},adrFaturim:{adrFaturim},adrDergim:{adrDergim},pershkrimi:{pershkrimi},dogana:{dogana},iddegeadministrative:{iddegeadministrative},koddegeadministrative:{koddegeadministrative},idPikeshitjefurnizimi:{idPikeshitjefurnizimi},kodPikeshitjefurnizimi:{kodPikeshitjefurnizimi}, idperdoruesi:{idperdoruesi},idrap:{idrap},eshteShitje:{eshteShitje},kodkonfigurimi:{kodkonfigurimi}, idperiudha:{idperiudha}, konfmag:{JsonConvert.SerializeObject(konfmag)}, idmag:{idmag}, kodmag:{kodmag}, mekontabilizim:{mekontabilizim}, idgrup1:{idgrup1}, idgrup2:{idgrup2}, idgrup3:{idgrup3}, afatikohor;{JsonConvert.SerializeObject(afatikohor)}, cash:{cash}, statusAprovimi:{JsonConvert.SerializeObject(statusAprovimi)}, idkrijuesi:{idkrijuesi}, PerqindjeAgj:{PerqindjeAgj},idkokaekzistueserezervim:{idkokaekzistueserezervim}, gjeneromeme:{gjeneromeme}, kokameme:{kokameme},idtransferimi:{idtransferimi},idkonfigtransferimi:{idkonfigtransferimi}, eshtememe:{eshtememe},gjenerobij:{gjenerobij}, statustrasferimi:{JsonConvert.SerializeObject(statusTransferimi)}, emerklienti:{emerklienti}, kontakti:{kontakti}, kase:{kase}, kupon:{kupon}, kodgrup1:{kodgrup1}, dtfill:{dtfill}, dtmb:{dtmb}, idAutomjet:{idAutomjet}, kilometraAutomjet:{kilometraAutomjet}, targa:{targa}, idAgjenti2:{idAgjenti2}, perqindjeAgjenti2:{PerqindjeAgjenti2}, kodAgjenti2:{kodAgjenti2}, idAgjenti3:{idAgjenti3}, perqindjeAgjenti3:{perqindjeAgjenti3}, kodAgjenti3:{KodAgjenti3},marresi:{marresi},idTransportues:{idTransportues}, emertimTr:{emertimTr}, faturepermbledhese:{faturepermbledhese}, faturashitjengaurdhershitjamekupontatimor:{JsonConvert.SerializeObject(faturashitjengaurdhershitjamekupontatimor)}, shpenzimeJoTeZbritshme:{shpenzimeJoTeZbritshme},idArka:{idArka},kontrollEkzistence:{kontrollEkzistence}, tollona:{tollona},autoklient:{autoklient}, ngaImporti:{ngaImporti}, dtfat:{dtfat}, gjeneruar:{gjeneruar}, tollonkastrati;{tollonkastrati}, tollonakastratielektronik:{tollonakastratielektronik}, idMuajRaportimi:{idMuajRaportimi}, idVitRaportimi:{idVitRaportimi}, shoferi:{shoferi}, targashoferit:{targashoferit}, zbritjeNeVlere:{ZbritjeNeVlere}, perqindjeZbritje;{perqindjeZbritje}, idKarta:{idKarta}, pike:{pike}, ocolFazat:{JsonConvert.SerializeObject(ocolFazat)}, idFaza:{idFaza}, dtKrijimiPajisje:{dtKrijimiPajisje}, dbData:{dbData},llojzevendesimi:{llojzevendesimi}, koordinata:{koordinata}, blerengadealeri:{blerengadealeri},krijoartri:{krijoartri}, idGjuha:{idGjuha}, konfvfone:{JsonConvert.SerializeObject(konfvfone)}, idstatusvjeter:{idstatusvjeter}, niptKlienti:{niptKlienti}, qytetiK:{qytetiK},kontrolloGjendje:{kontrolloGjendje}, idKategoriSeriali:{idKategoriSeriali},serialeUnike:{serialeUnike}, shenime2:{shenime2}, kartaPaPagese:{kartaPaPagese},idDokTransferimNga:{idDokTransferimNga}, eshteDokKthimi:{eshteDokKthimi}, idLlojMarreveshje:{ idLlojMarreveshje}, marrveshje: { marrveshje}, marrAktive { StatusMarreveshje} , kerkruarNga {kerkuarNga} , dateKerkese {dateKerkese}");
            return new clsMesazh(true, "Dokumenti i shitje/blerjes u krijua me sukses!");
        }

        public clsMesazh GjeneroGaranci(colSerialeUnikeMagazina serialeUnike, string username)
        {
            if (this.idStatusDok != 1)
                return new MesazhSuksesi();

            for (int i = 0, trupShitjeCount = oColTrupiShitje.Count; i < trupShitjeCount; i++)
            {
                clsTrupiShitje t = oColTrupiShitje[i];
                if (t.IdLlojVeprimi != 1)
                    continue;
                clsArtikulli art = (clsArtikulli)t.Element;
                if (art.IdllojGarancie == 0)
                    continue;

                if (string.IsNullOrEmpty(kontakti))
                    return new clsMesazh(false, "Vendosni Numrin e kontaktit!");
                if (string.IsNullOrEmpty(emerKlienti))
                    return new clsMesazh(false, "Vendosni Emrin e klientit!");

                double koeficient = 1;
                if (t.IdNjesia != art.Njesi1Artikulli)
                    koeficient = double.Parse(art.KoeficientArtikulli.ToString());

                string IMEI = "";
                if ((t.KodDetajim1 == null || t.KodDetajim1 == string.Empty) && serialeUnike != null && serialeUnike.Count > 0)
                {

                    foreach (clsSerialeUnikeMagazina serial in serialeUnike)
                    {
                        if ((serial.IdArtikulli == art.IdArtikulli) && (serial.GetType().Equals(typeof(clsSerialeUnikeAparate))))
                        {
                            IMEI = serial.SerialiKryesore;
                            clsGaranciArtikulli garanci = new clsGaranciArtikulli(0, string.Empty, idNivel, dtDok, username, emerKlienti, kontakti, art.IdArtikulli, IMEI, t.IdMagazina, idNdermarje, 0);
                            colGaranci.Add(garanci);
                        }
                    }
                }

                else if ((t.KodDetajim1 != null && t.KodDetajim1 != string.Empty))
                {
                    IMEI = t.KodDetajim1;
                    double sasiKoef = t.Sasia * koeficient;
                    for (int j = 0; j < sasiKoef; j++)
                    {
                        clsGaranciArtikulli garanci = new clsGaranciArtikulli(0, string.Empty, idNivel, dtDok, username, emerKlienti, kontakti, art.IdArtikulli, IMEI, t.IdMagazina, idNdermarje, 0);
                        colGaranci.Add(garanci);
                    }
                }

                else if (IMEI == "")
                    return new clsMesazh("Vendosni IMEI-in e produktit!");
            }

            return new MesazhSuksesi();
        }

        //u ben loget
        public bool eshteILidhur()
        {
            using (clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin())
            {

                return dbAdmin.eshteDokumentiILidhur(this.idShitjeKoka, this.idNivel, "T_KOKASHITJE", "IDSHITJEKOKA");
            }
        }

        public DataTable merrIdsDokLidhur()
        {
            using (clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin())
            {
                return dbAdmin.MerrDokLidhur(this.idShitjeKoka, this.idNivel, "T_KOKASHITJE", "IDSHITJEKOKA");
            }
        }
        
        public static Int64 merrNrMaxDokumenti(int idndermarje, string formatdate)
        {
            ImbLogger.LogTraceShitje("Merr nr max dokumenti per id ndermarrje: " + Convert.ToString(idndermarje) + " dhe format date: " + formatdate);
            clsDatabaseRegjistrim dbregj = new clsDatabaseRegjistrim();
            Int64 nrmax = 0;
            nrmax = dbregj.merrNrMaxDokumenti(nrmax, idndermarje, formatdate);
            dbregj.Dispose();
            ImbLogger.LogTraceShitje("Nr max i dokumentit = " + Convert.ToString(nrmax));
            return nrmax;
        }
        
        public clsMesazh krijoShitjePerImport(string kodNiveli, string kodKonf, clsKlientFurnitor klFurn, string nrProj, DateTime dtDk, string nrDk, string nrserial, DateTime dtMat, string kodMon, int idMonedha, double krs, string kodAgj, string kodMenPag, double zbr, DateTime dtregj, int idSt, int idNder, string adrFaturim, string adrDergim, string pershkrimi, bool dogana, string koddegeadministrative, string kodPikeshitjefurnizimi, int idperdoruesi, colTrupiShitje trupi, bool shitje_blerje, string grup1, string grup2, string grup3, DateTime afatikohor, int idkrijuesi, double perqAgj1, string kodAgjenti2, double perqAgj2, string kodAgjenti3, double perqAgj3, string emerklienti, string kontakti, bool kase, bool kupon, DateTime dtfill, DateTime dtmb, string automjeti, double kilometra, bool shpenzimeJoZbr, string marresi, int idNdermVit, int idRaportDesign, clsPeriudhaKontabel periudha, out bool gjeneroDokMag, clsKonfigurimAmbjenti konfigAmbjenti, int idMag, string kodMag, bool gjeneroFaturePermbledhese, ResourceManager rm, CultureInfo ci, DateTime dtfat, int idNivelGjenerues, int idKonfigGjenerues, int idDokNga, clsKonfigurimAmbjenti konfmag, string targashoferi, string shoferi, string klientFurnitorVartes, string kodKarta, string transportuesi, DateTime dtKrijimiPajisje, string arka, DbData dbData, string koordinata, string niptKlienti, string qyteti, int idGjuha, bool kontrolloGjendje, int idViti, StatusTrasferimi statustransferimi, bool eshteMeme, bool gjeneroBij, clsKokaShitje kokaMeme, string llojzbritje, string shenime2, string llojmarr, string idmarr, string kerkuarNga, DateTime dateKerkese, colSerialeUnikeMagazina serialeUnike , DateTime dtTransp, string Iic, string Nivf, int IdOperator=0,string Eic="",int Procesi=0,int eInvoiceType=0,string TipiVetefaturimit="",string NivfKthim="")
        {
            ImbLogger.LogTraceShitje($"Filloi krijimi i dokumentit per shitje import me parametra kodNiveli:" + kodNiveli + $", kodKonf:" + kodKonf + $", klFurn:{JsonConvert.SerializeObject(klFurn)}, nrProj:" + nrProj + $", dtDk:{dtDk}, nrDk:" + nrDk + $", nrserial:" + nrserial + $", dtMat:{dtMat}, kodMon:" + kodMon + $", idMonedha:{idMonedha}, krs:{krs}, kodAgj:{kodAgj}, kodMenPag:" + kodMenPag + $", zbr:{zbr}, dtregj:{dtregj}, idSt:{idSt}, idNder:{idNder}, adrFaturim:" + adrFaturim + $", adrDergim:" + adrDergim + $", pershkrimi:" + pershkrimi + $", dogana:{dogana}, koddegeadministrative:" + koddegeadministrative + $", kodPikeshitjefurnizimi:" + kodPikeshitjefurnizimi + $", idperdoruesi:{idperdoruesi}, trupi:{JsonConvert.SerializeObject(trupi)}, shitje_blerje:{shitje_blerje}, grup1:" + grup1 + $", grup2:" + grup2 + $", grup3:" + grup3 + $", afatikohor:{afatikohor}, idkrijuesi:{idkrijuesi}, perqAgj1:{perqAgj1}, kodAgjenti2:" + kodAgjenti2 + $", perqAgj2:{perqAgj2}, kodAgjenti3:" + kodAgjenti3 + $", perqAgj3:{perqAgj3}, emerklienti:" + emerklienti + $", kontakti:" + kontakti + $", kase:{kase}, kupon:{kupon}, dtfill:{dtfill}, dtmb:{dtmb}, automjeti:" + automjeti + $", kilometra:{kilometra}, shpenzimeJoZbr:{shpenzimeJoZbr}, marresi:" + marresi + $", idNdermVit:{idNdermVit}, idRaportDesign:{idRaportDesing}, periudha:{JsonConvert.SerializeObject(periudha)}, idMag:{idMag}, kodMag:" + kodMag + $", gjeneroFaturePermbledhese:{gjeneroFaturePermbledhese}, dtfat:{dtfat}, idNivelGjenerues:{idNivelGjenerues}, idKonfigGjenerues:{idKonfigGjenerues}, idDokNga:{idDokNga}, konfmag:{JsonConvert.SerializeObject(konfmag)}, targashoferi:" + targashoferi + $", shoferi:" + shoferi + $", klientFurnitorVartes:" + klientFurnitorVartes + $", kodKarta:" + kodKarta + $", transportuesi:" + transportuesi + $", dtKrijimiPajisje:{dtKrijimiPajisje}, arka:" + arka + $", dbData:{dbData}, koordinata:" + koordinata + $", niptKlienti:" + niptKlienti + $", idGjuha:{idGjuha}, kontrolloGjendje:{kontrolloGjendje}, idViti:{idViti}, statustransferimi:{JsonConvert.SerializeObject(statustransferimi)}, eshteMeme:{eshteMeme}, gjeneroBij:{gjeneroBij}, kokaMeme:{JsonConvert.SerializeObject(kokaMeme)}, llojzbritje: {llojzbritje}, shenime2: {shenime2}, kerkuarNga: {kerkuarNga}, dateKerkese: {dateKerkese}, IIC: {Iic}, NIVF: {Nivf}.");
            gjeneroDokMag = false;
            
            if (MbylljePeriudhe.PeriodClosing.IsPeriodClosed(dtDk, dbData.MyScopeDbManager.ConnectionName, idNder, shitje_blerje ? KategoriDokumenti.Shitje : KategoriDokumenti.Shitje, konfigAmbjenti.IdKonfigAmbjente))
            {
                ImbLogger.LogErrorShitje(MessagesResource.Messages["msgPeriodIsClosed"]);
                return new MesazhGabimi(MessagesResource.Messages["msgPeriodIsClosed"]);
            }

            clsDatabaseRegjistrim dbR = new clsDatabaseRegjistrim(dbData);
            clsDatabaseKontabilitet dbK = new clsDatabaseKontabilitet(dbData);
            clsDatabaseShare dbS = new clsDatabaseShare(dbData);
            clsDatabaseAdmin dbA = new clsDatabaseAdmin(dbData);
            clsDatabaseInventari dbI = new clsDatabaseInventari(dbData);
            clsDatabaseArkaBanka dbB = new clsDatabaseArkaBanka(dbData);
            clsNivelRegjistrimi nivelRegj = new clsNivelRegjistrimi(kodNiveli, idNder, dbR); //todo Nestila - pls check se me duket se mbushne kot nivelet me konvertim
            idNivel = nivelRegj.IdNivel;

            if (nivelRegj.IdKategori != konfigAmbjenti.IdKategori || nivelRegj.IdNivel != konfigAmbjenti.IdNivel)
            {
                ImbLogger.LogErrorShitje($"Ky dokumnet nuk i perket nenkategorise se zgjedhur.");
                return new clsMesazh(false, "Ky lloj dokumenti nuk i perket nenkategorise se zgjedhur!");
            }

            if (!String.IsNullOrEmpty(kodAgj))
            {
                clsAgjentShitje agj = new clsAgjentShitje(kodAgj, idNder, dbA);
                idAgjent = agj.IdAgjentShitje;
            }

            if (!clsLidhjeAutorizim.KaAutorizimPerdoruesi(idperdoruesi, klFurn.IdKlientFurnitor, "KlientFurnitor"))
                return new MesazhGabimi(String.Format(MessagesResource.Messages["msgAutorizimKlientFurnitor"], klFurn.KodKlientFurnitor));

            if (!String.IsNullOrEmpty(kodAgjenti2))
            {
                clsAgjentShitje agj2 = new clsAgjentShitje(kodAgjenti2, idNder, dbA);
                idAgjenti2 = agj2.IdAgjentShitje;
            }

            if (!String.IsNullOrEmpty(kodAgjenti3))
            {
                clsAgjentShitje agj3 = new clsAgjentShitje(kodAgjenti3, idNder, dbA);
                idAgjenti3 = agj3.IdAgjentShitje;
            }

            int idAutomjet = 0;
            if (!String.IsNullOrEmpty(automjeti))
            {
                clsAutomjete Automjeti = new clsAutomjete(automjeti, idNder, dbI);
                idAutomjet = Automjeti.IdAutomjeti;
            }
            idMenyrePagese = clsFunksione.ktheMenyrePageseSipasLlojit(kodMenPag);
            if (!String.IsNullOrEmpty(kodMenPag) && idMenyrePagese == -1)
            {
                ImbLogger.LogErrorShitje($"Menyra e pageses nuk ekziston! Id menyres se pagese = {idMenyrePagese}.");
                throw new Exception("Menyra e pageses nuk ekziston!");
            }

            if (idSt == 0 && kodMenPag.ContainsAnyIgnoreCase("Pagese Automatike"))
            {
                return new clsMesazh(false, rm.GetString("msgNukMundTeBehetPageseAutomatikeDraft", ci));
            }
            //do te kontrollen vlerat e arkes, nese ekziston arka ose banka dhe nese per menyren e pageses Karte Krediti eshte vendosur banke dhe jo arke

            if (!String.IsNullOrEmpty(arka))
            {
                clsBanka arkaBanka = new clsBanka(arka, idNder, dbB);
                IdArka = arkaBanka.IdBanka;
                if ((idMenyrePagese == 9) && !arkaBanka.LlojArkaBanka)
                {
                    return new clsMesazh(false, rm.GetString("msgArkaJoSaktePerMenyrePagese", ci));
                }
            }

            if (!String.IsNullOrEmpty(koddegeadministrative))
            {
                clsDegeAdministrative deg = new clsDegeAdministrative(koddegeadministrative, idNder, dbR); //todo kevi
                idDegeAdministrative = deg.IdDegeAdministrative;
            }
            if (!String.IsNullOrEmpty(kodPikeshitjefurnizimi))
            {
                clsPikeShitjeFurnizimi pike = new clsPikeShitjeFurnizimi(kodPikeshitjefurnizimi, idNder, dbR); //todo kevi
                idPikeShitjeFurnizimi = pike.IdPikeShitjeFurnizimi;
            }

            string shfaqmesazhapolupe = "jo";
            string mesazhinformues = string.Empty;

            gjeneroDokMag = clsAlternativaKushti.getAlternativa(konfigAmbjenti.IdKonfigAmbjente, "GJDM", dbS) == "Po";

            if (!String.IsNullOrEmpty(grup1))
            {
                clsGrupimDokumentiKoka grupimi1 = new clsGrupimDokumentiKoka(grup1, idNder, 1, idperdoruesi, dbR);
                idGrup1 = grupimi1.IdGrupimKoka;

            }

            if (!String.IsNullOrEmpty(grup2))
            {
                clsGrupimDokumentiKoka grupimi2 = new clsGrupimDokumentiKoka(grup2, idNder, 2, idperdoruesi, dbR);
                idGrup2 = grupimi2.IdGrupimKoka;
            }

            if (!String.IsNullOrEmpty(grup3))
            {
                clsGrupimDokumentiKoka grupimi3 = new clsGrupimDokumentiKoka(grup3, idNder, 3, idperdoruesi, dbR);
                idGrup3 = grupimi3.IdGrupimKoka;
            }

            if (trupi.Count == 0)
                return new clsMesazh(false, "Trupi i dokumentit nuk mund te jete bosh!");

            clsFormatiKonfig formatNrPerKonfig = new clsFormatiKonfig(idKonfigAmbjente, dbS);
            ImbLogger.LogTraceShitje("Kontrolli i formatit te monedhes!");
            clsFormatKonfigTrup formatMonedhe = new clsFormatKonfigTrup();
            if (formatNrPerKonfig.IdFormatKonfig > 0)
                formatMonedhe = formatNrPerKonfig.KonfigTrupi.merrFormatSipasMonedhes(idMonedha);
            else
                formatMonedhe = new clsFormatKonfigTrup(clsFormatKonfigTrup.defaultFormatSasia, clsFormatKonfigTrup.defaultFormatCmimi, clsFormatKonfigTrup.defaultFormatVlefta, clsFormatKonfigTrup.defaultFormatZbritja, clsFormatKonfigTrup.defaultFormatStringSasia, clsFormatKonfigTrup.defaultFormatStringCmimi, clsFormatKonfigTrup.defaultFormatStringVlefta, clsFormatKonfigTrup.defaultFormatStringZbritja);

            foreach (clsTrupiShitje t in trupi)
            {
                if (t.Kodi != string.Empty && t.Kodi != null)
                {
                    totali += t.VleftaMeTvsh;
                }
            }
            totali = Math.Round(totali, formatMonedhe.ShifraPasPresjesVlefta + 1);
            bool isZbritjeNeVlere = llojzbritje.Equals("Perqindje") ? false : true;
            double zbritjePerqindje = 0;
            double zbritjeVlere = 0;
            if (!isZbritjeNeVlere)
            {
                zbritjeVlere = totali * (zbr / 100);
                zbritjePerqindje = zbr;
            }
            else
            {
                zbritjeVlere = zbr;
                zbritjePerqindje = (totali != 0 ? (zbr / totali) * 100 : 0);
            }

            //llogaritja e zbritjes ne vlere ose ne perqindje ne varesi te llojit

            double shumaKomisionit = 0;
            double tvsh = 0;
            foreach (clsTrupiShitje t in trupi)
            {
                if (t.IdLlojVeprimi == 1)
                    if (((clsArtikulli)t.Element).LlogaritKomision && klFurn.LlogaritKomision)
                        shumaKomisionit += t.VleraKomisionit;
                if (t.VleftaPaTvsh == 0 || totali == 0)
                    tvsh += 0;
                else if (((t.VleftaMeTvsh - t.VleftaPaTvsh) / t.VleftaPaTvsh) > 0 && dogana == false)
                    tvsh += ((t.VleftaMeTvsh - t.VleftaPaTvsh) - (Math.Round((zbritjeVlere / totali), 4) * ((t.VleftaMeTvsh - t.VleftaPaTvsh))));
                else tvsh += 0;
            }

            if ((totali >= 0 && zbritjeVlere > totali) || (totali < 0 && zbritjeVlere < totali))
            {
                ImbLogger.LogWarningShitje("Zbritja ne vlere nuk mund te jete me e madhe se totali!");
                return new clsMesazh(false, "Zbritja ne vlere nuk mund te jete me e madhe se totali!");
            }

            if (idSt == 1 && klFurn.LimitBllokues > 0 && clsAlternativaKushti.getAlternativa(konfigAmbjenti.IdKonfigAmbjente, "KKLKF") == "Po" && totali + Convert.ToDouble(clsKlientFurnitor.MerrDetyrimKf(klFurn.IdKlientFurnitor, dtDk)) > klFurn.LimitBllokues)
            {
                ImbLogger.LogWarningShitje("Kujdes totali i fatures ka kaluar limitin bllokues te ketij klienti/furnitori!");
                return new clsMesazh(false, "Kujdes totali i fatures ka kaluar limitin bllokues te ketij klienti/furnitori!");
            }

            if (idkrijuesi == 0)
            {
                return new clsMesazh(false, "Perdoruesi nuk ekziston!");
            }

            bool kontrolloEkzistenceDok = true;
            //ne qofte se dokumenti qe po krijohet do te perfshihet ne fature permbledhese, atehere nuk ka nevoje te kontrollojme nese ekziston dokument me ate nr dhe date dokumenti 
            //ose nqs dokumenti po vjen i Refuzuar dhe duhet vetem te updatohet te mema, prape nuk ka nevoje te kontrollojme nese ekziston sepse e dime qe ekziston, nuk do e rikrijojme, vetem do behet update statusi ne 4
            if (gjeneroFaturePermbledhese || clsAlternativaKushti.getAlternativa(konfigAmbjenti.IdKonfigAmbjente, "MMDT") == "Refuzo")
                kontrolloEkzistenceDok = false;

            string mesazhGabimi;
            if (!clsFunksione.checkPeriudheKontabel(out mesazhGabimi, dtDk, periudha, idSt))
            {
                return new clsMesazh(false, mesazhGabimi);
            }

            if (idNivelGjenerues > 0)
            {
                if (!clsNivelRegjistrimi.ekzistonIdNivelGjeneruesi(idNivelGjenerues, idNder).Status)
                {
                    return new clsMesazh(false, "Gjeneruesi me id " + idNivelGjenerues + " nuk ekziston!");
                }
                else
                {
                    if (!clsKonfigurimAmbjenti.ekzistonKonfigurimSipasIDKONFIG(idKonfigGjenerues, idNder).Status)
                        idKonfigGjenerues = 0;
                    if (!clsNivelRegjistrimi.kontrolloGjeneruesPerNivelRegjistrimi(idNivelGjenerues, idNder, idGjenerues, idKonfigGjenerues))
                        idNivelGjenerues = 0;
                }
            }

            if (idDokNga != 0)
            {
                using (clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim())
                {
                    if (!dbRegj.ktheKokaShitjeEkzistonDoksipasID(idDokNga))
                    {
                        return new clsMesazh(false, "Id Dok Nga " + idDokNga + " nuk ekziston!");
                    }
                }
            }

            var colKlientefurnitoreVartes = new colKlienteFurnitore();
            if (!string.IsNullOrEmpty(klientFurnitorVartes))
            {
                colKlientefurnitoreVartes = colKlienteFurnitore.KontrolloEkzistenceKlientFurnitoreVartes(klientFurnitorVartes, idNder, dbK);
            }

            int idKarta = 0;
            if (!string.IsNullOrEmpty(kodKarta))
            {
                clsKarta kartaKlientit = new clsKarta(kodKarta, idNder, dbR);
                idKarta = kartaKlientit.IdKarta;
                if (idKarta <= 0)
                {
                    ImbLogger.LogErrorShitje("Karta me kod " + kodKarta + " nuk ekziston ");
                    throw new Exception("Karta me kod " + kodKarta + " nuk ekziston!");
                }
            }

            int idTransportues = 0;
            if (!string.IsNullOrEmpty(transportuesi))
            {
                idTransportues = clsTransportues.merrIdTransportuesSipasEmertimit(transportuesi, idNder);
                if (idTransportues <= 0)
                {
                    ImbLogger.LogErrorShitje("Transportuesi me emertim " + transportuesi + " nuk ekziston!");
                    throw new Exception("Transportuesi me emertim " + transportuesi + " nuk ekziston!");
                }
            }
            if (dtregj == DateTime.MinValue)
                dtregj = DateTime.Today;
            if (dtMat == DateTime.MinValue)
                dtMat = dtDk;
            if (afatikohor == DateTime.MinValue)
                afatikohor = dtDk;
            if (dtfill == DateTime.MinValue)
                dtfill = dtDk;
            if (dtmb == DateTime.MinValue)
                dtmb = dtDk;
            if (dtfat == DateTime.MinValue)
                dtfat = dtDk;

            idLlojMarreveshje = clsFunksione.ktheIdLlojMarreveshjeSipasLlojit(llojmarr);
            if (idLlojMarreveshje == -1)
                throw new Exception("Lloji i marreveshjes " + llojmarr + " nuk ekziston!");

            if (string.IsNullOrEmpty(emerklienti))
                emerklienti = klFurn.EmertimFature;

            var merrMagazinePerberesi = clsAlternativaKushti.getAlternativa(konfigAmbjenti.IdKonfigAmbjente, "KGJAPMR", dbS) == "Po";
            
            return krijoShitje(ref gjeneroDokMag, idNivel, 0, konfigAmbjenti.IdKonfigAmbjente, klFurn.IdKlientFurnitor, klFurn.KodKlientFurnitor, 0, nrProj, dtDk, nrDk, nrserial, dtMat, idMonedha, kodMon,
                krs, 0, "", dtTransp, 0, "", idAgjent, kodAgj, idMenyrePagese, kodMenPag, 0, "", zbritjeVlere, totali, tvsh, dtregj, idSt, idNder, idNdermVit, idNivelGjenerues, idKonfigGjenerues, idGjenerues,
                idDokNga, adrFaturim, adrDergim, pershkrimi, dogana, idDegeAdministrative, koddegeadministrative, idPikeShitjeFurnizimi, kodPikeshitjefurnizimi, idperdoruesi, idRaportDesign, trupi, shitje_blerje,
                kodKonf, periudha.IdPeriudha, konfmag, idMag, kodMag, false, idGrup1, idGrup2, idGrup3, afatikohor, cash, statusAprovimi, idkrijuesi, perqAgj1, out shfaqmesazhapolupe, null, new DbQendraKosto.colTrupiQendraKosto(),
                0, out mesazhinformues, false, kokaMeme, 0, 0, eshteMeme, gjeneroBij, statustransferimi, emerklienti, kontakti, kase, kupon, grup1, dtfill, dtmb, idAutomjet, kilometra, automjeti, idAgjenti2, perqAgj2,
                kodAgjenti2, idAgjenti3, perqAgj3, kodAgjenti3, marresi, idTransportues, transportuesi, false, new clsKokaShitje(), shpenzimeJoZbr, idarka, kontrolloEkzistenceDok, false, false, true, dtfat, false, false,
                false, dtDk.Month, idViti, shoferi, targashoferi, isZbritjeNeVlere, zbritjePerqindje, idKarta, 0, new colFazaKontrate(), 0, colKlientefurnitoreVartes, dtKrijimiPajisje, dbData, "Jo", koordinata, false, false,
                idGjuha, new clsKonfigurimAmbjenti(), -1, niptKlienti, qyteti, kontrolloGjendje, 0, serialeUnike, shenime2, false, 0, false, idLlojMarreveshje, idmarr, string.IsNullOrEmpty(idmarr) ? StatusMarreveshje.Inaktive : StatusMarreveshje.Aktive,
                kerkuarNga, "shtim", dateKerkese, merrMagazinePerberesi, nrDk, Iic, Nivf, IdOperator, NivfKthim, Eic, einStatus, Procesi, eInvoiceType, TipiVetefaturimit);
        }

        public clsMesazh krijoShitjePerImportWK(int idNiv, int idtempl, int idKonf, string kodKlFurn, int idProj, string nrProj, DateTime dtDk, string nrDk, string nrserial, DateTime dtMat, string kodMon, double krs, int MenTrans, DateTime dttrans, int idKushtDerg, int idAgj, int idMenPag, int idKushtPag, double zbr, double tot, double tv, DateTime dtregj, int idSt, int idNder, int idNdVt, int idNivelGjenerues, int idKonfigGjenerues, int idGjenerues, int idDokNga, string adrFaturim, string adrDergim, string pershkrimi, bool dogana, int iddegeadministrative, int idPikeshitjefurnizimi, int idperdoruesi, int idrap, colTrupiShitje trupi, bool shitje_blerje, string kodkonfigurimi, int idperiudha, clsKonfigurimAmbjenti konfmag, int idmag, string kodmag, bool mekontabilizim, int idgrup1, int idgrup2, int idgrup3, DateTime afatikohor, double cash, StatusAprovimi statusAprovimi, int idkrijuesi, double PerqAgj, StatusTrasferimi statustransferimi, bool gjeneromeme, clsKokaShitje kokameme, int idtransferimi, int idkonfigtransferimi, bool eshtememe, bool gjenerobij, string emerklienti, string kontakti, DateTime dtfill, DateTime dtmb, int idAutomjet, double kilometra, int idAgjent2, double perqAgj2, int idAgjent3, double perqAgj3, string marresi, bool shpenzimeJoZbr, int idarka, DateTime dtfat, DateTime dtKrijimiPajisje, DbData dbData, int idGjuha, string Iic, string Nivf, int IdOperator, string Eic,int Procesi,int eInvoiceType,string TipiVetefaturimit,string NivfKthimi)
        {
            ImbLogger.LogTraceShitje($"Filloi krijo shitje per import WK me parametra idNiv:{idNiv}, idtempl:{idtempl}, idKonf:{idKonf}, kodKlFurn:" + kodKlFurn + $", idProj:{idProj}, nrProj:{nrProj}, dtDk:{dtDk}, nrDk:{nrDk}, nrserial:{nrserial}, dtMat:{dtMat}, kodMon:{kodMon}, krs:{krs}, MenTrans:{MenTrans}, dttrans:{dttrans}, idKushtDerg:{idKushtDerg}, idAgj:{idAgj}, idMenPag:{idMenPag}, idKushtPag:{idKushtPag}, zbr:{zbr}, tot:{tot}, tv:{tv}, dtregj:{dtregj}, idSt:{idSt}, idNder:{idNder}, idNdVt:{idNdVt}, idNivelGjenerues:{idNivelGjenerues}, idKonfigGjenerues:{idKonfigGjenerues}, idGjenerues:{idGjenerues}, idDokNga:{IdDokNga}, adrFaturim:" + adrFaturim + $", adrDergim:" + adrDergim + $", pershkrimi:" + pershkrimi + $", dogana:{dogana}, iddegeadministrative:{iddegeadministrative}, idPikeshitjefurnizimi:{idPikeShitjeFurnizimi}, idperdoruesi:{idperdoruesi}, idrap:{idrap}, trupi:{JsonConvert.SerializeObject(trupi)}, shitje_blerje:{shitje_blerje}, kodkonfigurimi:" + kodkonfigurimi + $", idperiudha:{idperiudha}, konfmag:{JsonConvert.SerializeObject(konfmag)}, idmag:{idmag}, kodmag:" + kodmag + $", mekontabilizim:{mekontabilizim}, idgrup1:{idgrup1}, idgrup2:{idGrup2}, idgrup3:{idGrup3}, afatikohor:{afatikohor}, cash:{cash}, statusAprovimi:{JsonConvert.SerializeObject(statusAprovimi)}, idkrijuesi:{idkrijuesi}, PerqAgj:{PerqAgj}, statustransferimi:{JsonConvert.SerializeObject(statustransferimi)}, gjeneromeme:{gjeneromeme}, kokameme:{JsonConvert.SerializeObject(kokameme)}, idtransferimi:{idtransferimi}, idkonfigtransferimi:{idkonfigtransferimi}, eshtememe:{eshtememe}, gjenerobij:{gjenerobij}, emerklienti:" + emerKlienti + $", kontakti:" + kontakti + $", dtfill:{dtfill}, dtmb:{dtmb}, idAutomjet:{idAutomjet}, kilometra:{kilometra}, idAgjent2:{idAgjent2}, perqAgj2:{perqAgj2}, idAgjent3:{IdAgjenti3}, perqAgj3:{perqAgj3}, marresi:" + marresi + $", shpenzimeJoZbr:{shpenzimeJoZbr}, idarka:{idarka}, dtfat:{dtfat}, dtKrijimiPajisje:{dtKrijimiPajisje}, dbData:{dbData}, idGjuha:{idGjuha}, IIC: {Iic}, NIVF: {Nivf}.");
            clsKlientFurnitor kf = new clsKlientFurnitor();
            kf.mbushKlientFurnitorSipasKodit(kodKlFurn, idNder);
            idKlientFurnitor = kf.IdKlientFurnitor;
            clsMonedha mon = new clsMonedha();
            mon.mbushMonedhen(kodMon, idNder);
            idMonedha = mon.IdMonedha;
            string menyretranp = string.Empty, kushtpag = string.Empty, koddeg = string.Empty, kodpike = string.Empty, kodkushtder = string.Empty, kodagj = string.Empty, kodgrup1 = string.Empty, kodagj2 = string.Empty, kodagj3 = string.Empty;
            if (MenTrans != 0)
            {
                clsMenyreTransporti menyre = new clsMenyreTransporti(MenTrans);
                menyretranp = menyre.KodiMenyreTransporti;
            }
            if (idKushtPag != 0)
            {
                clsKushtPageseKoka kushtpagese = new clsKushtPageseKoka(idKushtPag);
                kushtpag = kushtpagese.KodiKushtPagese;
            }
            if (iddegeadministrative != 0)
            {
                clsDegeAdministrative deg = new clsDegeAdministrative(iddegeadministrative);
                koddeg = deg.Kodi;
            }
            if (idPikeshitjefurnizimi != 0)
            {
                clsPikeShitjeFurnizimi pike = new clsPikeShitjeFurnizimi(idPikeshitjefurnizimi);
                kodpike = pike.Kodi;
            }
            if (idKushtDerg != 0)
            {
                clsKushtDergimi kushte = new clsKushtDergimi(idKushtDerg);
                kodkushtder = kushte.KodiKushtDergimi;
            }
            if (idAgj != 0)
            {
                clsAgjentShitje agj = new clsAgjentShitje(idAgj);
                kodagj = agj.KodiAgjentShitje;
            }
            if (idAgjent2 != 0)
            {
                clsAgjentShitje agj = new clsAgjentShitje(idAgjent2);
                kodagj2 = agj.KodiAgjentShitje;
            }
            if (idAgjent3 != 0)
            {
                clsAgjentShitje agj = new clsAgjentShitje(idAgjent3);
                kodagj3 = agj.KodiAgjentShitje;
            }
            if (idTransportues != 0)
            {
                clsTransportues tr = new clsTransportues(idTransportues);
                emertimTr = tr.Emertimi;
            }
            if (idgrup1 != 0)
            {
                clsGrupimDokumentiKoka agj = new clsGrupimDokumentiKoka(idgrup1);
                kodgrup1 = agj.Kodi;
            }
            string nrShasie = string.Empty;
            if (idAutomjet != 0 && idAutomjet != -1)
            {
                clsAutomjete auto = new clsAutomjete();
                auto.mbushAutomjet(idAutomjet);
                nrShasie = auto.NrShasie;
            }
            string menyrepag = string.Empty;
            menyrepag = clsFunksione.ktheMenyrePageseSipasID(idMenPag);

            string shfaqmesazhapolupe = "jo";
            string mesazhinformues = string.Empty;

            clsKonfigurimAmbjenti konfig = new clsKonfigurimAmbjenti();
            konfig.mbushKonfigAmbjSipasKod(kodkonfigurimi, idNder);
            bool gjeneroDokMag = clsAlternativaKushti.getAlternativa(konfig.IdKonfigAmbjente, "GJDM") == "Po";
            double uljePerqindje = (totali != 0 ? Math.Round((zbr / totali) * 100, 2) : 0);
            var merrMagazinePerberesi = clsAlternativaKushti.getAlternativa(konfig.IdKonfigAmbjente, "KGJAPMR", new clsDatabaseShare(dbData)) == "Po";

            ImbLogger.LogTraceShitje($"Mbaroi krijo shitje per import WK me parametra idNiv:{idNiv}, idtempl:{idtempl}, idKonf:{idKonf}, kodKlFurn:" + kodKlFurn + $", idProj:{idProj}, nrProj:{nrProj}, dtDk:{dtDk}, nrDk:{nrDk}, nrserial:{nrserial}, dtMat:{dtMat}, kodMon:{kodMon}, krs:{krs}, MenTrans:{MenTrans}, dttrans:{dttrans}, idKushtDerg:{idKushtDerg}, idAgj:{idAgj}, idMenPag:{idMenPag}, idKushtPag:{idKushtPag}, zbr:{zbr}, tot:{tot}, tv:{tv}, dtregj:{dtregj}, idSt:{idSt}, idNder:{idNder}, idNdVt:{idNdVt}, idNivelGjenerues:{idNivelGjenerues}, idKonfigGjenerues:{idKonfigGjenerues}, idGjenerues:{idGjenerues}, idDokNga:{IdDokNga}, adrFaturim:" + adrFaturim + $", adrDergim:" + adrDergim + $", pershkrimi:" + pershkrimi + $", dogana:{dogana}, iddegeadministrative:{iddegeadministrative}, idPikeshitjefurnizimi:{idPikeShitjeFurnizimi}, idperdoruesi:{idperdoruesi}, idrap:{idrap}, trupi:{JsonConvert.SerializeObject(trupi)}, shitje_blerje:{shitje_blerje}, kodkonfigurimi:" + kodkonfigurimi + $", idperiudha:{idperiudha}, konfmag:{JsonConvert.SerializeObject(konfmag)}, idmag:{idmag}, kodmag:" + kodmag + $", mekontabilizim:{mekontabilizim}, idgrup1:{idgrup1}, idgrup2:{idGrup2}, idgrup3:{idGrup3}, afatikohor:{afatikohor}, cash:{cash}, statusAprovimi:{JsonConvert.SerializeObject(statusAprovimi)}, idkrijuesi:{idkrijuesi}, PerqAgj:{PerqAgj}, statustransferimi:{JsonConvert.SerializeObject(statustransferimi)}, gjeneromeme:{gjeneromeme}, kokameme:{JsonConvert.SerializeObject(kokameme)}, idtransferimi:{idtransferimi}, idkonfigtransferimi:{idkonfigtransferimi}, eshtememe:{eshtememe}, gjenerobij:{gjenerobij}, emerklienti:" + emerKlienti + $", kontakti:" + kontakti + $", dtfill:{dtfill}, dtmb:{dtmb}, idAutomjet:{idAutomjet}, kilometra:{kilometra}, idAgjent2:{idAgjent2}, perqAgj2:{perqAgj2}, idAgjent3:{IdAgjenti3}, perqAgj3:{perqAgj3}, marresi:" + marresi + $", shpenzimeJoZbr:{shpenzimeJoZbr}, idarka:{idarka}, dtfat:{dtfat}, dtKrijimiPajisje:{dtKrijimiPajisje}, dbData:{dbData}, idGjuha:{idGjuha}, IIC: {Iic}, NIVF: {Nivf}.");
            return krijoShitje(ref gjeneroDokMag, idNiv, idtempl, idKonf, idKlientFurnitor, kodKlFurn, idProj, nrProj, dtDk, nrDk, nrserial, dtMat, idMonedha, kodMon, krs, MenTrans, menyretranp, dttrans, idKushtDerg, kodkushtder,
                idAgj, kodagj, idMenPag, menyrepag, idKushtPag, kushtpag, zbr, tot, tv, dtregj, idSt, idNder, idNdVt, idNivelGjenerues, idKonfigGjenerues, idGjenerues, idDokNga, adrFaturim, adrDergim, pershkrimi, dogana, iddegeadministrative,
                koddeg, idPikeshitjefurnizimi, kodpike, idperdoruesi, idrap, trupi, shitje_blerje, kodkonfigurimi, idperiudha, konfmag, idmag, kodmag, mekontabilizim, idgrup1, idgrup2, idgrup3, afatikohor, cash, statusAprovimi, idkrijuesi, PerqAgj, out shfaqmesazhapolupe, null,
                new DbQendraKosto.colTrupiQendraKosto(), 0, out mesazhinformues, gjeneromeme, kokameme, idtransferimi, idkonfigtransferimi, eshtememe, gjenerobij, statustransferimi, emerklienti, kontakti, false, false, kodgrup1, dtfill, dtmb, idAutomjet, kilometra, nrShasie, idAgjent2,
                perqAgj2, kodagj2, idAgjent3, perqAgj3, kodagj3, marresi, idTransportues, emertimTr, false, new clsKokaShitje(), shpenzimeJoZbr, idarka, false, false, false, false, dtfat, false, false, false, dtDk.Month, clsViti.ktheIdVitPerNdermarrjenSipasKodit(idNder, Convert.ToString(dtDk.Year)), "", "", true,
                uljePerqindje, 0, 0, new colFazaKontrate(), 0, new colKlienteFurnitore(), dtKrijimiPajisje, dbData, "Jo", string.Empty, false, false, idGjuha, new clsKonfigurimAmbjenti(), -1, niptKlienti, "", false, 0, null, "", false, 0, false,
                idLlojMarreveshje, idMarreveshje, StatusMarreveshje, "", "shtim", DateTime.Now, merrMagazinePerberesi, nrDk, Iic, Nivf, idOperator, NivfKthimi, Eic, einStatus, Procesi, eInvoiceType,TipiVetefaturimit);
        }
        
        private clsMesazh kontrollo(bool tollona, bool tollonkastrati, bool tollonakastratielektronik, bool autoshitje, DbData dbData, string llojzevendesimi, bool kontrollokupon, bool eshteDokKthimi, bool aplikonrserialneruajte, bool lejoSasiPozitiveKthim)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda kontrollo me parametra tollona:{tollona}, tollonkastrati:{tollonkastrati}, tollonakastratielektronik:{tollonakastratielektronik}, autoshitje:{autoshitje}, llojzevendesimi:" + llojzevendesimi + $", kontrollokupon:{kontrollokupon}, eshteDokKthimi:{eshteDokKthimi}, aplikonrserialneruajte:{aplikonrserialneruajte}");
            clsDatabaseKontabilitet dbK = new clsDatabaseKontabilitet(dbData);
            clsDatabaseAdmin dbA = new clsDatabaseAdmin(dbData);
            clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim(dbData);
            clsDatabaseInventari dbI = new clsDatabaseInventari(dbData);

            if (string.IsNullOrEmpty(nrDok))
            {
                ImbLogger.LogWarningShitje("Numri i dokumentit nuk mund të jetë bosh");
                return new clsMesazh(false, "Numri i dokumentit nuk mund të jetë bosh");
            }

            if (dtDok == null || dtDok.ToShortDateString() == "01/01/0100")
            {
                ImbLogger.LogWarningShitje("Zgjidhni datën e dokumentit!");
                return new clsMesazh(false, "Zgjidhni datën e dokumentit!");
            }

            if (dtRegjistrimi == null || dtRegjistrimi.ToShortDateString() == "01/01/0100")
            {
                ImbLogger.LogWarningShitje("Zgjidhni datën e regjistrimit!");
                return new clsMesazh(false, "Zgjidhni datën e regjistrimit!");
            }

            DbKontabiliteti.clsKlientFurnitor kf = new clsKlientFurnitor();
            if (this.kupon && kontrollokupon && String.IsNullOrEmpty(this.nrSerial) && (!aplikonrserialneruajte || (aplikonrserialneruajte && this.IdStatusDok == 1)))
            {
                ImbLogger.LogTraceShitje("Duhet te vendosni nje Serial fature per kuponat me fature tatimore!");
                return new clsMesazh(false, "Duhet te vendosni nje Serial fature per kuponat me fature tatimore!");
            }

            if (!String.IsNullOrEmpty(kodKlientFurnitor))
            {
                kf = new clsKlientFurnitor(kodKlientFurnitor, idNdermarje, idPerdoruesi, dbK);
                if (!kf.AktivKF)
                {
                    ImbLogger.LogWarningShitje("Klient/Furnitori nuk është aktiv!");
                    return new clsMesazh(false, $"Klient/Furnitori me kod {kf.KodKlientFurnitor} nuk është aktiv!");
                }
            }
            clsMonedha mon = new clsMonedha(kodMonedha, idNdermarje, dbA);

            if (!String.IsNullOrEmpty(kodMenyreTransporti) && !clsMenyreTransporti.ekziston(kodMenyreTransporti, idNdermarje))
            {
                ImbLogger.LogWarningShitje("Menyra e transportit nuk ekzisston!");
                return new clsMesazh(false, "Mënyra e transportit nuk ekziston!");
            }

            if (!String.IsNullOrEmpty(kodKushtDergimi) && !clsKushtDergimi.ekziston(kodKushtDergimi, idNdermarje))
            {
                ImbLogger.LogWarningShitje("Kushti i dërgimit nuk ekziston!");
                return new clsMesazh(false, "Kushti i dërgimit nuk ekziston!");
            }

            if (!String.IsNullOrEmpty(kodKushtPagese) && !clsKushtPageseKoka.ekziston(kodKushtPagese, idNdermarje))
            {
                ImbLogger.LogWarningShitje("Kushti i pagesës nuk ekziston!");
                return new clsMesazh(false, "Kushti i pagesës nuk ekziston!");
            }

            if (!String.IsNullOrEmpty(kodAgjenti))
            {
                clsAgjentShitje agjentShitje = new clsAgjentShitje(kodAgjenti, idNdermarje, dbA);
            }

            if (!String.IsNullOrEmpty(kodDegeAdministrative))
            {
                clsDegeAdministrative deg = new clsDegeAdministrative(kodDegeAdministrative, idNdermarje, dbRegj);
                if (!deg.Aktiv)
                {
                    ImbLogger.LogWarningShitje("Dega administrative nuk është aktive!");
                    return new clsMesazh(false, $"Dega administrative me kod {deg.Kodi} nuk është aktive!");
                }
            }

            if (!String.IsNullOrEmpty(kodPikeShitje))
            {
                clsPikeShitjeFurnizimi pike = new clsPikeShitjeFurnizimi(kodPikeShitje, idNdermarje, dbRegj);
                if (!pike.Aktiv)
                {
                    ImbLogger.LogWarningShitje("Pika e shitjes nuk është aktive!");
                    return new clsMesazh(false, $"Pika e shitjes me kod {pike.Kodi} nuk është aktive!");
                }
            }

            if (!String.IsNullOrEmpty(targa))
            {
                clsAutomjete auto = new clsAutomjete(idNdermarje, targa, dbI);
                if (auto.IdKlienti != 0 && auto.IdKlienti != kf.IdKlientFurnitor && autoshitje == true)
                {
                    ImbLogger.LogWarningShitje("Makina nuk i përket klientit të zgjedhur!");
                    return new clsMesazh(false, "Makina nuk i përket klientit të zgjedhur!");
                }
            }
            clsViti vitiRaportimit = new clsViti(idVitRaportimi, dbA);
            int kodVitRaportimi = Convert.ToInt32(vitiRaportimit.KodiViti);
            if ((DtDok.Month > MuajRaportimi && ((DtDok.Year > kodVitRaportimi) || DtDok.Year == kodVitRaportimi)) || (DtDok.Year > kodVitRaportimi))
            {
                ImbLogger.LogWarningShitje("Periudha e raportimit duhet te jete me e madhe ose e barabarte me periudhen e dokumentit!");
                return new clsMesazh(false, "Periudha e raportimit duhet te jete me e madhe ose e barabarte me periudhen e dokumentit!");
            }
            double totali = 0;
            foreach (clsTrupiShitje trup in oColTrupiShitje)
            {
                totali += trup.VleftaMeTvsh;
                if (tollona || tollonkastrati)
                {
                    if (trup.IdLlojVeprimi == 1)
                    {
                        double sasiatot = oColTrupiShitje.FindAll(x => x.IdKodi == trup.IdKodi && x.IdLlojVeprimi == trup.IdLlojVeprimi).Sum(x => x.Sasia);
                        if (llojzevendesimi == "Zëvendësim brenda llojit" && sasiatot != 0)
                        {
                            ImbLogger.LogWarningShitje("Totali i sasise per zevendesimet duhet te jete zero!");
                            return new clsMesazh(false, "Totali i sasise per zevendesimet duhet te jete zero!");
                        }
                        if (llojzevendesimi == "Zëvendësim lloje të ndryshme" && trup.Sasia < 0 && Totali != 0)
                        {
                            ImbLogger.LogWarningShitje("Totali i fatures per zevendesimet duhet te jete zero!");
                            return new clsMesazh(false, "Totali i fatures per zevendesimet duhet te jete zero!");
                        }
                    }
                }
            }

            if (eshteDokKthimi && totali > 0 && !lejoSasiPozitiveKthim)
            {
                ImbLogger.LogWarningShitje("Vlera totale e fatures duhet te jete negative!");
                return new clsMesazh(false, "Vlera totale e fatures duhet te jete negative!");
            }

            if ((IdMenyrePagese == 5) || (IdMenyrePagese == 4))
            {
                string kodNivelRegjistrimi = clsNivelRegjistrimi.ktheKodNivelRegjistrimi(IdNivel);
                if (kodNivelRegjistrimi != "FSH" && kodNivelRegjistrimi != "FB")
                {
                    ImbLogger.LogWarningShitje("Nuk mund të bëni pagesë automatike të një dokumenti urdhër, ofertë apo kërkesë. Ju lutem zgjidhni një mënyrë tjetër pagese!");
                    return new clsMesazh(false, "Nuk mund të bëni pagesë automatike apo pagesë të një dokumenti urdhër, ofertë apo kërkesë. Ju lutem zgjidhni një mënyrë tjetër pagese!");
                }
            }

            if (IdArka == 0)
                this.IdArka = kf.EmriBanka;

            if (IdMenyrePagese == 5 && this.IdArka == 0 && clsKlientFurnitor.MerrIdBanke(idKlientFurnitor) == 0)
            {
                ImbLogger.LogWarningShitje("Klienti i zgjedhur duhet të ketë një bankë të paracaktuar!");
                return new clsMesazh(false, "Klienti i zgjedhur duhet të ketë një bankë të paracaktuar!");
            }

            if (clsAtributeTrupi.IsRequiredField(IdKonfigAmbjente, "btneKlientfurnitorVartes", 506) && ColKlienteFurnitoreVartes.Count == 0)
            {
                ImbLogger.LogWarningShitje("Ju lutem zgjidhni klientin/furnitorin vartes.");
                return new clsMesazh(false, MessagesResource.Messages["msgZgjidhKlientVartes"]);
            }

            ImbLogger.LogWarningShitje("Kontrollet u kaluan me sukses!");
            ImbLogger.LogWarningShitje($"Mbaroi metoda kontrollo me parametra tollona:{tollona}, tollonkastrati:{tollonkastrati}, tollonakastratielektronik:{tollonakastratielektronik}, autoshitje:{autoshitje}, llojzevendesimi:" + llojzevendesimi + $", kontrollokupon:{kontrollokupon}, eshteDokKthimi:{eshteDokKthimi}, aplikonrserialneruajte:{aplikonrserialneruajte}");
            return new clsMesazh(true, "Kontrollet u kaluan me sukses!");
        }

        private static colGjendjeKlientFurnitor gjeneroGjendjeKf(int idniveli, string nrdok, DateTime data, DateTime dtregj, List<int> emrakf, List<string> rreshtakf, colTrupatFletetKontabel trupiPerGjendjeKF)
        {
            return colGjendjeKlientFurnitor.KrijoGjendjetKlientFurnitor(emrakf, idniveli, nrdok, data, dtregj, trupiPerGjendjeKF, rreshtakf);
        }

        private static void krijoRezervimiNgaMagazina(clsKokaShitje koka, int idkokaekzistueserezervim, ref string mesazhinformues, clsDatabaseRegjistrim dbRegj, colTrupiRezervime coltrupiRez, clsKokaRezervime rezervimehyrje, clsTrupiShitje trupsh, int idshitjenga, bool rezervimOwn, int idKodi, string kodi, string pershkrimi, int idMagazina, int idNjesia, double sasi, clsDatabaseInventari dbInv)
        {
            ImbLogger.LogWarningShitje($"Filloi metoda krijoRezervimiNgaMagazina me parametra idkokaekzistueserezervim:{idkokaekzistueserezervim}, idshitjenga:{idshitjenga}");
            if (trupsh.IdLlojVeprimi != 1)
                return;

            clsArtikulli art = rezervimOwn ? new clsArtikulli(kodi, koka.IdNdermarrje, dbInv) : new clsArtikulli(idKodi, dbInv);
            if (!art.IRezervueshem)
                return;

            foreach (clsTrupiRezervime trrez in rezervimehyrje.OcolTrupiRezervime)
            {
                if ((trrez.IdTrupiNgaVjen == idshitjenga && trrez.IdArtikulli == art.IdArtikulli) && (trrez.IdMag == idMagazina || trrez.IdMag == 0 || rezervimOwn))
                {
                    double sasiaeharxhuar = dbRegj.ktheSasineKonvertuarSipasArtikullitPaDokEkzistues(trrez.IdArtikulli, trrez.IdTrupiRezervime, idkokaekzistueserezervim);
                    double sasiaRez = trrez.IdNjesia == art.Njesi1Artikulli ? trrez.Sasia - sasiaeharxhuar : trrez.Sasia * Convert.ToDouble(art.KoeficientArtikulli) - sasiaeharxhuar;
                    double sasia = (idNjesia == art.Njesi1Artikulli ? sasi : (sasi * Convert.ToDouble(art.KoeficientArtikulli)));
                    double koeficent = art.Njesi1Artikulli == idNjesia ? 1 : Convert.ToDouble(art.KoeficientArtikulli);
                    clsTrupiRezervime truprez;
                    if (!rezervimOwn)
                    {
                        truprez = new clsTrupiRezervime(0, 0, idKodi, kodi, pershkrimi, idNjesia,
                           (sasia < sasiaRez ? sasia : sasiaRez) / koeficent, koeficent, idMagazina, koka.dtDok, koka.idStatusDok, -1, trupsh.IdShitjeTrupi, trrez.IdTrupiRezervime, art);
                    }
                    else
                    {
                        truprez = new clsTrupiRezervime(0, 0, art.IdArtikulli, kodi, pershkrimi, trrez.IdNjesia,
                           (sasia < sasiaRez ? sasia : sasiaRez) / koeficent, koeficent, trrez.IdMag, koka.dtDok, koka.idStatusDok, -1, 0, trrez.IdTrupiRezervime, art);
                    }
                    coltrupiRez.Add(truprez);
                    if (sasi > truprez.Sasia)
                    {
                        ImbLogger.LogWarningShitje("Per artikullin " + art.KodArtikulli + " sasia e mbetur e rezervuar eshte " + truprez.Sasia + "!");
                        mesazhinformues += "Per artikullin " + art.KodArtikulli + " sasia e mbetur e rezervuar eshte " + truprez.Sasia + "!";
                    }
                }
                if (!(trrez.IdMag == idMagazina || trrez.IdMag == 0) && !rezervimOwn)
                {
                    ImbLogger.LogWarningShitje("Per artikullin " + art.KodArtikulli + " nuk ka sasi te rezervuar per kete magazine!");
                    mesazhinformues += "Per artikullin " + art.KodArtikulli + " nuk ka sasi te rezervuar per kete magazine!";
                }
            }

            ImbLogger.LogWarningShitje($"Mbaroi metoda krijoRezervimiNgaMagazina me parametra idkokaekzistueserezervim:{idkokaekzistueserezervim}, idshitjenga:{idshitjenga}");
        }

        private clsMesazh krijoMagazineNgaShitja(ref bool gjeneroDokMag, bool eshteShitje, clsKonfigurimAmbjenti konfmag, int idmag, string kodmag, int idkokaekzistueserezervim, out string mesazhinformues, DbData dbData, bool blerengadealeri, bool krijoartri, int idGjuha, int idstatusvjeter, bool kontrolloGjendje, colSerialeUnikeMagazina serialeUnike, clsKokaShitje shitjePerRezervim, bool rezervimOwn, bool merrMagazinePerberesi)
        {
            ImbLogger.LogWarningShitje($"Filloi metoda krijoMagazineNgaShitja me parametra gjeneroDokMag:{gjeneroDokMag}, eshteShitje:{eshteShitje}, konfmag:{JsonConvert.SerializeObject(konfmag)}, idmag:{idmag}, idkokaekzistueserezervim:{idkokaekzistueserezervim}, blerengadealeri:{blerengadealeri}, krijoartri:{krijoartri}, idGjuha:{idGjuha}, idstatusvjeter:{idstatusvjeter}, kontrolloGjendje:{kontrolloGjendje}");
            mesazhinformues = string.Empty;
            string shenime = string.Empty;
            if (this.pershkrimi != String.Empty)
                shenime = this.pershkrimi;
            else
            {
                if (!eshteShitje)
                    shenime = "Nga blerja";
                else shenime = "Nga shitja";
            }
            colTrupiMagazina coltrupi;
            clsDatabaseInventari dbinv = new clsDatabaseInventari(dbData);
            coltrupi = ruajTrupinEMagazines(this, eshteShitje ? -1 : 1, 0, dbinv, false, blerengadealeri, krijoartri, idGjuha, serialeUnike, merrMagazinePerberesi);
            ImbLogger.LogWarningShitje($"U ruajt trupi i magazines me parametra eshteShitje = {eshteShitje}, dbinv = {dbinv}, blerengadealeri = {blerengadealeri}, krijo artikull te ri = {krijoartri}, idgjuha = {idGjuha}");
            if (coltrupi.Count == 0)
            {
                gjeneroDokMag = false;
                ImbLogger.LogWarningShitje("Dokumenti i shitjes nuk ka rreshta per te krijuar dokument magazine");
                return new clsMesazh(true, "Dokumenti i shitjes nuk ka rreshta per te krijuar dokument magazine");
            }

            if (eshteShitje && kontrolloGjendje && idStatusDok == 1)
            {
                clsMesazh mesazh = this.OKokaMagazina.kontrolloGjendjeArtikujshPerDokImporti(eshteShitje, coltrupi, dbinv, this.idNdermarje, konfmag.IdKonfigAmbjente);
                if (!mesazh.Status)
                    return mesazh;
            }

            clsKokaRezervime kokadalje = KrijoDokumentRezervimiPerDokumentMagazine(dbData, idkokaekzistueserezervim, ref mesazhinformues, eshteShitje, idstatusvjeter, idmag, kodmag, konfmag, shitjePerRezervim, rezervimOwn, merrMagazinePerberesi);


            int idRaportDesign = Convert.ToInt32(clsAtributeTrupi.merrVleredefaultSipasKontrollitDheKonfigurimit(konfmag.IdKonfigAmbjente, "cmbFormatiPrintimit", 510, new clsDatabaseShare(dbData)));
            ImbLogger.LogWarningShitje("Magazina u krijua me sukses!");
            ImbLogger.LogWarningShitje($"Mbaroi metoda krijoMagazineNgaShitja me parametra gjeneroDokMag:{gjeneroDokMag}, eshteShitje:{eshteShitje}, idmag:{idmag}, idkokaekzistueserezervim:{idkokaekzistueserezervim}, blerengadealeri:{blerengadealeri}, idGjuha:{idGjuha}, idstatusvjeter:{idstatusvjeter}, kontrolloGjendje:{kontrolloGjendje}");
            return this.OKokaMagazina.krijoMagazine(kontrolloGjendje, this.oKokaMagazina.IdDokNga, konfmag.IdNivel, konfmag.IdKonfigAmbjente, this.idKlientFurnitor, this.kodKlientFurnitor, idmag, kodmag, this.DtDok, this.NrDokMagazine, this.IdProjekt, this.NrProjekt, konfmag.IdKategori, this.oKokaMagazina.IdDokNga, 0, this.IdStatusDok, this.IdNdermarrje, this.IdNdermarrjeVit, this.IdPerdoruesi, this.DtRegjistrimi, (eshteShitje) ? 2 : 1, shenime, this.idNivel, this.idKonfigAmbjente, this.idShitjeKoka, this.idDegeAdministrative, this.kodDegeAdministrative, 0, string.Empty, 0, string.Empty, false, this.IdGrup1, this.IdGrup2, this.IdGrup3, string.Empty, string.Empty, string.Empty, coltrupi, new clsKokaMagazina(), new clsKokaFleteKontabel(), kokadalje, new clsDatabaseRegjistrim(dbData), true, this.idAutomjet,
              this.targa, idRaportDesign, false, this.IdKrijuesi, this.dtTransportimi, this.idKategoriSeriali, this.nrSerial, string.Empty, string.Empty, 0, null,false,false,"","",0);
        }

        private clsKokaRezervime KrijoDokumentRezervimiPerDokumentMagazine(DbData dbData, int idkokaekzistueserezervim, ref string mesazhinformues, bool eshteShitje, int idstatusvjeter, int idmag, string kodmag, clsKonfigurimAmbjenti konfmag, clsKokaShitje shitjePerRezervim, bool rezervimOwn, bool merrMagazinePerberesish)
        {
            clsKokaRezervime kokadalje = new clsKokaRezervime();
            clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim(dbData);
            var dbInv = new clsDatabaseInventari(dbData);
            colTrupiRezervime coltrupiRez = new colTrupiRezervime();
            clsKokaRezervime rezervimehyrje = new clsKokaRezervime();
            colTrupiRezervime coltrupiRezKonv = new colTrupiRezervime();
            ArrayList idkonvertime = new ArrayList();
            ArrayList idkonvertimekonf = new ArrayList();
            string alternativa = clsAlternativaKushti.getAlternativa(idKonfigAmbjente, "GJDRVD", new clsDatabaseShare(dbData));///kushti per te gjeneruar dokument rezervimi vetem kur eshte draft per vfone porosi
            var trupi = rezervimOwn ? shitjePerRezervim.OColTrupiShitje : this.oColTrupiShitje;
            for (int i = 0, trupShitjeCount = trupi.Count; i < trupShitjeCount; i++)
            {
                clsTrupiShitje trupsh = trupi[i];
                if (trupsh.IdLlojVeprimi != 1)
                    continue;
                clsArtikulli art = rezervimOwn ? new clsArtikulli(trupsh.Kodi, this.idNdermarje, new clsDatabaseInventari(dbData)) : (clsArtikulli)trupsh.Element;
                if (art.Klasa != 4)
                {
                    KrijoTrupRezervimiPerDokumentMagazine(trupsh, rezervimOwn, dbData, eshteShitje, dbRegj, idkokaekzistueserezervim, coltrupiRez, ref mesazhinformues, rezervimehyrje, shitjePerRezervim, coltrupiRezKonv, idkonvertime, alternativa, idstatusvjeter, art, trupsh.IdNjesia, trupsh.IdMagazina, trupsh.IdKodi, trupsh.Kodi, trupsh.Pershkrimi, trupsh.Sasia, trupsh.IdNjesia, art, dbInv);
                }
                else
                {
                    var artper = new colArtikulliPerberes(art.IdArtikulli, this.dtDok, dbInv);
                    for (int j = 0; j < artper.Count; j++)
                    {
                        var perberes = new clsArtikulli(artper[j].IdLidheseArt, dbInv);
                        KrijoTrupRezervimiPerDokumentMagazine(trupsh, rezervimOwn, dbData, eshteShitje, dbRegj, idkokaekzistueserezervim, coltrupiRez, ref mesazhinformues, rezervimehyrje, shitjePerRezervim, coltrupiRezKonv, idkonvertime, alternativa, idstatusvjeter, perberes, perberes.Njesi1Artikulli, merrMagazinePerberesish ? perberes.IdMagazina : trupsh.IdMagazina, perberes.IdArtikulli, perberes.KodArtikulli, perberes.PershkrimArtikulli, trupsh.Sasia * Convert.ToDouble(artper[j].Koeficienti), trupsh.IdNjesia, art, dbInv);
                    }
                }
            }
            if (coltrupiRez.Count > 0)
                kokadalje.krijoRezervim(0, 0, this.idKlientFurnitor, this.kodKlientFurnitor, idmag, kodmag, this.dtDok, this.nrDok, idStatusDok, this.idNdermarje, this.idNdermarjeVit, this.idPerdoruesi, this.dtRegjistrimi, pershkrimi, this.idDegeAdministrative, this.kodDegeAdministrative, 0, 2, 1, 0, 0, konfmag.IdNivel, konfmag.IdKonfigAmbjente, 0, coltrupiRez, new clsKokaRezervime());
            if (eshteShitje && coltrupiRezKonv.Count > 0)
                kokadalje.krijoRezervim(0, 0, this.idKlientFurnitor, this.kodKlientFurnitor, idmag, kodmag, this.dtDok, this.nrDok, idStatusDok, this.idNdermarje, this.idNdermarjeVit, this.idPerdoruesi, this.dtRegjistrimi, pershkrimi, this.idDegeAdministrative, this.kodDegeAdministrative, 0, 2, 1, 0, 0, konfmag.IdNivel, konfmag.IdKonfigAmbjente, 0, coltrupiRezKonv, new clsKokaRezervime());

            return kokadalje;
        }

        private void KrijoTrupRezervimiPerDokumentMagazine(clsTrupiShitje trupsh, bool rezervimOwn, DbData dbData, bool eshteShitje, clsDatabaseRegjistrim dbRegj, int idkokaekzistueserezervim, colTrupiRezervime coltrupiRez, ref string mesazhinformues, clsKokaRezervime rezervimehyrje, clsKokaShitje shitjePerRezervim, colTrupiRezervime coltrupiRezKonv, ArrayList idkonvertime, string alternativa, int idstatusvjeter, clsArtikulli art, int idNjesia, int idMagazina, int idKodi, string kodi, string pershkrimi, double sasiTrup, int idNjesiTrup, clsArtikulli set, clsDatabaseInventari dbInv)
        {
            if (!art.IRezervueshem)
                return;
            if (trupsh.IdTrupiRezervimi != 0)
            {
                clsTrupiRezervime truprezer = new clsTrupiRezervime(trupsh.IdTrupiRezervimi, dbRegj);
                if (truprezer.IdMag == idMagazina || truprezer.IdMag == 0)
                {
                    double sasiaeharxhuar = dbRegj.ktheSasineKonvertuarSipasArtikullitPaDokEkzistues(truprezer.IdArtikulli, truprezer.IdTrupiRezervime, idkokaekzistueserezervim);
                    double sasiaTrupMeKoeficent = idNjesiTrup == set.Njesi1Artikulli ? sasiTrup : (sasiTrup * Convert.ToDouble(set.KoeficientArtikulli));
                    double sasia = (sasiaTrupMeKoeficent < (truprezer.IdNjesia == art.Njesi1Artikulli ? truprezer.Sasia - sasiaeharxhuar : truprezer.Sasia * Convert.ToDouble(set.KoeficientArtikulli) - sasiaeharxhuar) ? sasiaTrupMeKoeficent : (idNjesiTrup == set.Njesi1Artikulli ? truprezer.Sasia - sasiaeharxhuar : truprezer.Sasia * Convert.ToDouble(set.KoeficientArtikulli) - sasiaeharxhuar)) / (set.Njesi1Artikulli == idNjesiTrup ? 1 : Convert.ToDouble(art.KoeficientArtikulli));

                    clsTrupiRezervime truprez = new clsTrupiRezervime(0, 0, idKodi, kodi, pershkrimi, idNjesia, sasia, idNjesia == art.Njesi1Artikulli ? 1 : Convert.ToDouble(art.KoeficientArtikulli), idMagazina, this.dtDok, this.idStatusDok, -1, trupsh.IdShitjeTrupi, truprezer.IdTrupiRezervime, art);
                    coltrupiRez.Add(truprez);
                    if (sasia > truprez.Sasia)
                    {
                        ImbLogger.LogWarningShitje("MesazhiInformues: Per artikullin " + art.KodArtikulli + " sasia e mbetur e rezervuar eshte " + truprez.Sasia);
                        mesazhinformues += "Per artikullin " + art.KodArtikulli + " sasia e mbetur e rezervuar eshte " + truprez.Sasia + "!";
                    }
                }
                else
                {
                    ImbLogger.LogWarningShitje("MesazhiInformues:Per artikullin " + art.KodArtikulli + "nuk ka sasi te rezervuar per kete magazine!");
                    mesazhinformues += "Per artikullin " + art.KodArtikulli + " nuk ka sasi te rezervuar per kete magazine!";
                }
            }
            if (eshteShitje)
            {
                if (rezervimOwn && IdStatusDok == 1)
                {
                    rezervimehyrje.mbushKokaRezervimiSipasIDGjenerues(shitjePerRezervim.IdShitjeKoka, 1, shitjePerRezervim.IdKonfigAmbjente, dbRegj);
                    rezervimehyrje.OcolTrupiRezervime.Clear();
                    rezervimehyrje.OcolTrupiRezervime.mbushGjitheTrupiRezervimiNgaKoka(rezervimehyrje.IdKokaRezervimi, dbRegj);
                    krijoRezervimiNgaMagazina(this, 0, ref mesazhinformues, dbRegj, coltrupiRezKonv, rezervimehyrje, trupsh, trupsh.IdShitjeTrupi, rezervimOwn, idKodi, kodi, pershkrimi, idMagazina, idNjesia, sasiTrup, dbInv);
                }
                else if (trupsh.IdTrupiKonvertimi != 0 && !rezervimOwn)
                {
                    int idKoka = clsTrupiShitje.ktheIdKoka(trupsh.IdTrupiKonvertimi, dbRegj);
                    if (!idkonvertime.Contains(idKoka))
                    {
                        rezervimehyrje.OcolTrupiRezervime.mbushGjitheTrupiRezervimiNgaKokaShitjes(idKoka, 1, dbRegj);
                        idkonvertime.Add(idKoka);
                    }
                    krijoRezervimiNgaMagazina(this, idkokaekzistueserezervim, ref mesazhinformues, dbRegj, coltrupiRezKonv, rezervimehyrje, trupsh, trupsh.IdTrupiKonvertimi, rezervimOwn, idKodi, kodi, pershkrimi, idMagazina, idNjesia, sasiTrup, dbInv);
                }
                else if ((idStatusDok == 1 && alternativa == "Po" && idstatusvjeter == 0 && trupsh.IdShitjeTrupi != 0))//nqs kemi dokument rezervimi tek drafti
                {
                    clsTrupiShitje tr = new clsTrupiShitje(trupsh.IdShitjeTrupi, dbRegj);

                    rezervimehyrje.mbushKokaRezervimiSipasIDGjenerues(tr.IdShitjeKoka, 1, this.idKonfigAmbjente, dbRegj);
                    rezervimehyrje.OcolTrupiRezervime.mbushGjitheTrupiRezervimiNgaKoka(rezervimehyrje.IdKokaRezervimi, dbRegj);

                    krijoRezervimiNgaMagazina(this, idkokaekzistueserezervim, ref mesazhinformues, dbRegj, coltrupiRezKonv, rezervimehyrje, trupsh, trupsh.IdShitjeTrupi, rezervimOwn, idKodi, kodi, pershkrimi, idMagazina, idNjesia, sasiTrup, dbInv);
                }
            }
        }

        private colTrupiMagazina ruajTrupinEMagazines(clsKokaShitje koka, int shenja, int idmag, clsDatabaseInventari dbInv, bool konvertim, bool blerjedealer, bool krijoartri, int idGjuha, colSerialeUnikeMagazina serialetUnike, bool merrMagazinePerberesi)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda ruajTrupinEMagazines me parametra shenja:{shenja}, idmag:{idmag}, konvertim:{konvertim}, blerjedealer:{blerjedealer}, krijoartri:{krijoartri}, idGjuha:{idGjuha}");
            colTrupiMagazina trupat = new colTrupiMagazina();
            double perqindjeZbritje = (koka.Totali != 0 ? (koka.Zbritje / koka.Totali) : 0);//(koka.Totali != 0 ? Math.Round((koka.Zbritje / koka.Totali), 5) : 0);
            clsDatabaseShare dbshare = new clsDatabaseShare(dbInv);
            bool lejoArtPerbNeBlerjeKusht = clsAlternativaKushti.getAlternativa(koka.IdKonfigAmbjente, "LEJOARTPERB", dbshare) == "Po";
            bool vendosDetajim = clsAlternativaKushti.getAlternativa(koka.IdKonfigAmbjente, "NDPAP", dbshare) == "Po";
            bool ruajBarkodet = clsAlternativaKushti.getAlternativa(koka.IdKonfigAmbjente, "RBART", dbshare) == "Po";
            for (int i = 0, trupShitjeCount = koka.oColTrupiShitje.Count; i < trupShitjeCount; i++)
            {
                clsTrupiShitje tsh = koka.oColTrupiShitje[i];
                if (idmag != 0 && tsh.IdMagazina != idmag)
                    continue;
                clsTrupiMagazina trupMag = new clsTrupiMagazina();

                if (tsh.IdLlojVeprimi != 1)
                    continue; //nese nuk eshte artikull smerret parasysh
                bool krijuar = false;
                clsArtikulli art = (clsArtikulli)tsh.Element;

                if (art.Klasa != 4)
                {
                    krijuar = trupMag.krijoTrupMagazinaNgaGrida(koka.idNdermarje, koka.DtDok, koka.kursi, tsh.IdLlojVeprimi, tsh.IdKodi, tsh.Pershkrimi, konvertim && !blerjedealer ? 0 : tsh.IdDetajimArt, tsh.IdNjesia, tsh.Sasia, tsh.VleftaPaTvsh * (1 - perqindjeZbritje) / tsh.Sasia, (tsh.VleftaPaTvsh * (1 - perqindjeZbritje)), tsh.IdMagazina, shenja, konvertim && !blerjedealer ? 0 : tsh.IdDetajimArt2, tsh.Kodi, konvertim && !blerjedealer ? "" : tsh.KodDetajim1, konvertim && !blerjedealer ? "" : tsh.KodDetajim2, konvertim ? tsh.IdShitjeTrupi : 0, konvertim ? tsh.IdShitjeTrupi : 0, 0, i, art, tsh.Shenime, 0, dbInv, blerjedealer, tsh.IdBarkodi);
                    if (blerjedealer)
                    {
                        shtoartikullbleredealer(koka, shenja, trupat, trupMag, krijuar, art, krijoartri, dbInv, idGjuha);
                    }
                    trupat.ShtoTrupinNeCollectionSeBashkuMeSerialet(trupMag, serialetUnike, shenja, tsh.Tvsh, krijuar);
                }
                else
                {
                    DbInventari.colArtikulliPerberes artper = new DbInventari.colArtikulliPerberes();
                    artper.ktheArtikujPerberesSipasIdArtikullitKryesorDheDatesMeTeAfert(art.IdArtikulli, koka.dtDok, dbInv);
                    double sasiaPerGjitheArtPerberes = 0;
                    if (lejoArtPerbNeBlerjeKusht)
                    {
                        for (int j = 0, artPerCount = artper.Count; j < artPerCount; j++)
                        {
                            sasiaPerGjitheArtPerberes += tsh.Sasia * (double)artper[j].Koeficienti;
                        }
                    }
                    for (int j = 0, artPerCount = artper.Count; j < artPerCount; j++)
                    {
                        DbInventari.clsArtikulliPerberes aper = artper[j];
                        if (aper.Lloji == 1)    //artikull
                        {
                            trupMag = new clsTrupiMagazina();
                            int idNjesia = tsh.IdNjesia;
                            DbInventari.clsArtikulli a = new DbInventari.clsArtikulli(aper.IdLidheseArt, dbInv);
                            int idMag = merrMagazinePerberesi && a.IdMagazina > 0 ? a.IdMagazina : tsh.IdMagazina;
                            int idDetajimi1 = 0;
                            string kodDetajim1 = "";
                            int idBarkodi = tsh.IdBarkodi;
                            if (ruajBarkodet)
                            {
                                idBarkodi = clsKodbari.ktheIdKodbarSipasIdArtikulliNjesiaKodbariIPare(a.KodArtikulli, idNdermarje);
                            }

                            if (vendosDetajim && clsDetajimArtikulli.ekzistonDetajimLidhurMeArtikullinSipasId(tsh.IdDetajimArt, koka.IdNdermarrje, a.KodArtikulli, 1, dbInv))
                            {
                                idDetajimi1 = tsh.IdDetajimArt;
                                kodDetajim1 = tsh.KodDetajim1;
                            }

                            if (lejoArtPerbNeBlerjeKusht)
                            {
                                double sasia = tsh.Sasia * (double)aper.Koeficienti * ((art.Njesi1Artikulli == idNjesia) ? 1 : Convert.ToDouble(art.KoeficientArtikulli));
                                double cmimi = tsh.VleftaPaTvsh * (1 - perqindjeZbritje) / sasiaPerGjitheArtPerberes;

                                krijuar = trupMag.krijoTrupMagazinaNgaGrida(koka.idNdermarje, koka.dtDok, koka.kursi, tsh.IdLlojVeprimi, a.IdArtikulli, a.PershkrimArtikulli, idDetajimi1, a.Njesi1Artikulli, sasia, cmimi, sasia * cmimi, idMag, shenja, 0, a.KodArtikulli, kodDetajim1, "", konvertim ? tsh.IdShitjeTrupi : 0, konvertim ? tsh.IdShitjeTrupi : 0, 0, i, a, tsh.Shenime, art.IdArtikulli, dbInv, blerjedealer, idBarkodi);
                            }
                            else
                            {
                                krijuar = trupMag.krijoTrupMagazinaNgaGrida(koka.idNdermarje, koka.dtDok, koka.kursi, tsh.IdLlojVeprimi, a.IdArtikulli, a.PershkrimArtikulli, idDetajimi1, a.Njesi1Artikulli, tsh.Sasia * (double)aper.Koeficienti * ((art.Njesi1Artikulli == idNjesia) ? 1 : Convert.ToDouble(art.KoeficientArtikulli)), tsh.VleftaPaTvsh * (1 - perqindjeZbritje) / tsh.Sasia, tsh.VleftaPaTvsh * (1 - perqindjeZbritje), idMag, shenja, 0, a.KodArtikulli, kodDetajim1, "", konvertim ? tsh.IdShitjeTrupi : 0, konvertim ? tsh.IdShitjeTrupi : 0, 0, i, a, tsh.Shenime, art.IdArtikulli, dbInv, blerjedealer, idBarkodi);
                            }
                            if (blerjedealer)
                            {
                                shtoartikullbleredealer(koka, shenja, trupat, trupMag, krijuar, a, krijoartri, dbInv, idGjuha);

                            }
                            trupat.ShtoTrupinNeCollectionSeBashkuMeSerialet(trupMag, serialetUnike, shenja, tsh.Tvsh, krijuar);
                        }
                    }
                    continue;
                }
            }
            ImbLogger.LogTraceShitje($"Mbaroi metoda ruajTrupinEMagazines me parametra shenja:{shenja}, idmag:{idmag}, konvertim:{konvertim}, blerjedealer:{blerjedealer}, krijoartri:{krijoartri}, idGjuha:{idGjuha}");
            return trupat;
        }

        private static void shtoartikullbleredealer(clsKokaShitje koka, int shenja, colTrupiMagazina trupat, clsTrupiMagazina trupMag, bool krijuar, clsArtikulli art, bool krijoartri, clsDatabaseInventari dbinv, int idGjuha)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda shtoartikullbleredealer me parametra koka:{JsonConvert.SerializeObject(koka)}, colTruoiMagazina:{JsonConvert.SerializeObject(trupat)}, clsTrupiMagazina:{JsonConvert.SerializeObject(trupMag)}, shenja:{shenja}, art:{JsonConvert.SerializeObject(art)} krijuar:{krijuar}, krijoartri:{krijoartri}, idGjuha:{idGjuha}");
            string kodartikullidealer = art.KodArtikulli + "_blere_nga_dealer";
            clsArtikulli artdealer = new clsArtikulli();
            if (!krijoartri)
                artdealer = art;
            else
            {
                if (!clsArtikulli.ekziston(kodartikullidealer, koka.idNdermarje, dbinv))
                {
                    artdealer = new clsArtikulli(0, kodartikullidealer, art.PershkrimArtikulli, art.PershkrimiAngArtikulli, art.KodiDoganorArtikulli, art.VendodhjeArtikulli, art.Kodifikimi1Artikulli, art.Kodifikimi2Artikulli, art.OrigjineArtikulli, art.Njesi1Artikulli, art.Njesi2Artikulli, art.KoeficientArtikulli, art.IdFurnitoriKryesor, art.PeshaBrutoArtikulli, art.PeshaNetoArtikulli, art.DetajimArtikulli, art.Klasa, art.IdSkemaKontabilitetiArtikulli, art.IdLlogariInventari, art.IdLlogariBlerje, art.IdLlogariShitje, art.IdLlogariTeTrete, art.IdLlogariShpenzime, art.IdLlogRez, art.IdLlogPakRez, art.MinimumArtikulli, art.MaximumArtikulli, art.MetodeKostojeArtikulli, art.LlogaritjaKMSHArtikulli, art.ZevendesimAutomatikArtikulli, art.IdPerdoruesi, art.IdNdermarje, art.KontrollGjendje, art.KontrollCmimi, art.KontrollGjendjeArtikulli, art.IdTvsh, art.IdKonfig, art.Aktiv, art.SasiNjesi, art.Scrap, art.ProdhimMePorosi, art.IdKategoriDetajimi, art.IdKategoriDetajimi2, art.KontrollGjendjeDetajim2, art.IdObjektivaKosto, art.IdllojGarancie, art.Garancia, art.IdMagazina, art.IRezervueshem, art.PerTransferim, art.Loan, art.Dhurate, art.AplikimDhurate, art.Pike, art.Vlere, art.KodVFOne, art.MeSerial, art.IShitshem, art.MbetjeShitshme, art.IdArtRaportuesi, art.PerPeshore, art.PershkrimFurnitori, art.SiperfaqjaM2, art.NrKontrate, art.NrPasurie, art.ZonaKadastrale, art.Shasia, art.Marka, art.Modeli, art.VitProdhimi, art.TeDhenaTeknika, art.MeBarkodLogjik, art.SkemaBarkodit, art.Kodifikimi3Artikulli, art.AparatBazaar, art.KodOferte, art.ArtikullIVjeter, art.IdFormatSeriali, art.MeRezerveRivleresimi, 0, false, 0, "", 0, art.KodiIBarit, art.IRimbursueshem);

                    colCmimeArtikujsh cmime = new colCmimeArtikujsh();
                    cmime.mbushCmimArtikulliSipasArtikullit(art.IdArtikulli, art.IdNdermarje, dbinv);
                    artdealer.ColArtikujPerberes = new colArtikulliPerberes();
                    artdealer.ColArtikujPerberes.merrSipasIdArtikullKryesore(art.IdArtikulli, dbinv);
                    artdealer.ColArtikujVfone = new colArtikullVfone();
                    artdealer.ColArtikujVfone.merrSipasIdArtikull(art.IdArtikulli, dbinv);
                    artdealer.IdStatusDok = 1;
                    //colKodbare kodbare = new colKodbare(art.IdArtikulli, dbinv);

                    //artdealer.Kodbari = "";
                    //foreach (clsKodbari k in kodbare)
                    //{
                    //    artdealer.Kodbari += k.Pershkrimi + ",";

                    //}
                    //if (artdealer.Kodbari.Length > 0)
                    //    artdealer.Kodbari = artdealer.Kodbari.Substring(0, artdealer.Kodbari.Length - 1);
                    artdealer.OColDetajime = new colDetajimePerArt();
                    artdealer.OColDetajime2 = new colDetajimePerArt();
                    artdealer.OColKodbare = new colKodbare();
                    artdealer.Autorizimet = "";
                    clsMesazh mesazh = artdealer.ruaj(cmime, new clsArtikullPerberesTemplateKoka(), dbinv, artdealer.IdNdermarje);
                    if (!mesazh.Status)
                    {
                        ImbLogger.LogErrorShitje($"Exception:{mesazh.PershkrimMesazhi}");
                        throw new Exception(mesazh.PershkrimMesazhi);
                    }

                    clsNdermarrje ndermarje = new clsNdermarrje(art.IdNdermarje, new clsDatabaseAdmin(dbinv));
                    int ndermarjePrind = ndermarje.IdPrindi;
                    if (!clsArtikulli.ekziston(kodartikullidealer, ndermarjePrind, dbinv))
                    {
                        object[] idartikulli = { artdealer.IdArtikulli };
                        List<object> idndermarjew = new List<object>();
                        idndermarjew.Add(ndermarjePrind);
                        mesazh = artdealer.transfero(idartikulli, idndermarjew, koka.IdPerdoruesi, idGjuha, dbinv, art.IdNdermarje);
                        if (!mesazh.Status)
                        {
                            ImbLogger.LogErrorShitje($"Exception:{mesazh.PershkrimMesazhi}");
                            throw new Exception(mesazh.PershkrimMesazhi);
                        }
                    }

                }
                else
                    artdealer.mbushArtikull(kodartikullidealer, koka.idNdermarje, dbinv);
                clsDetajimPerArt det = new clsDetajimPerArt();
                det.mbushDetajimArtSipasIdArtikulliDheDetajimi(art.IdArtikulli, trupMag.IdDetajimi, dbinv);
                dbinv.fshiDetajimArt(det.IdDetajimArt);
                dbinv.ruajDetajimArt(artdealer.IdArtikulli, trupMag.IdDetajimi, 1);
            }
            clsTrupiMagazina trupblerjedealer = new clsTrupiMagazina(0, trupMag.IdKokaMagazina, trupMag.IdLlojVeprimi, artdealer.IdArtikulli, artdealer.KodArtikulli, artdealer.PershkrimArtikulli, trupMag.IdNjesia, -trupMag.Sasia, trupMag.Cmimi, -trupMag.Vlefta, trupMag.Koeficenti, trupMag.Shenja, trupMag.SasiProgresive, trupMag.VleftaProgresive, trupMag.IdMag, trupMag.Data, trupMag.IdStatusDok, trupMag.IdRenditjes, trupMag.IdDetajimi, trupMag.SasiProgresiveDetajimi, trupMag.VlefteProgresiveDetajimi, trupMag.IdDetajimi2, trupMag.IdTrupiRezervimi, trupMag.IdTrupiKonvertimFSH, trupMag.IdTrupiKonvertimUSH, trupMag.IdTrupiKonvertimUD, trupMag.IdKthimi, trupMag.IdTrupiShitjeGjenerimi, artdealer, trupMag.Shenime, trupMag.IdArtikullSet, trupMag.IdBarkodi);

            if (trupblerjedealer.IdArtikulli != -1 && krijuar) //ky kusht duhet pare kur te shtohen makrot kevi
            {
                if (trupblerjedealer.IdMag == -1)
                {
                    ImbLogger.LogErrorShitje("Magazina nuk ekziston!");
                    throw new Exception("Magazina nuk ekziston!");
                }

                trupblerjedealer.Shenja = shenja;
                trupat.Add(trupblerjedealer);
            }

            ImbLogger.LogTraceShitje($"Mbaroi metoda shtoartikullbleredealer me parametra koka:{JsonConvert.SerializeObject(koka)}, colTruoiMagazina:{JsonConvert.SerializeObject(trupat)}, clsTrupiMagazina:{JsonConvert.SerializeObject(trupMag)}, shenja:{shenja}, art:{JsonConvert.SerializeObject(art)} krijuar:{krijuar}, krijoartri:{krijoartri}, idGjuha:{idGjuha}");
        }

        private clsMesazh krijoMagazinatPerFature(clsKokaShitje koka, clsDatabaseRegjistrim dbRegj, int idPeriudha, out string shfaqmesazhapolupemag, bool eshteOwn, string kodkonfigurimi, bool blerjedealer, bool krijoartri, int idGjuha, bool ruajrenditje, bool promocione)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda krijoMagazinatPerFature me parametra koka:{JsonConvert.SerializeObject(koka)}, idPeriudha:{idPeriudha}, eshteOwn:{eshteOwn}, kodkonfigurimi:" + kodkonfigurimi + $", blerjedealer:{blerjedealer}, krijoartri:{krijoartri}, idGjuha:{idGjuha}, ruajrenditje:{ruajrenditje}, promocione:{promocione}");
            string mesazhmevonshem = "";
            clsMesazh u_ruajt = new clsMesazh();
            shfaqmesazhapolupemag = "";
            ArrayList magazinat = new ArrayList();
            foreach (clsTrupiShitje sh in koka.oColTrupiShitje)
                if (!magazinat.Contains(sh.IdMagazina))
                    magazinat.Add(sh.IdMagazina);

            DbShare.clsDatabaseShare dbshare = new DbShare.clsDatabaseShare(dbRegj);
            DbShare.clsKonfigurimAmbjenti konfmag = new DbShare.clsKonfigurimAmbjenti();
            konfmag.mbushKonfigAmbjSipasKod(kodkonfigurimi, koka.idNdermarje, dbshare);
            bool gjithmone = false;
            if (clsAlternativaKushti.getAlternativa(konfmag.IdKonfigAmbjente, "GJKGJ", dbshare) == "Po")
                gjithmone = true;

            clsDatabaseInventari dbinv = new clsDatabaseInventari(dbRegj);
            foreach (int i in magazinat)
                try
                {
                    colTrupiMagazina coltrupi = new colTrupiMagazina();
                    coltrupi = ruajTrupinEMagazines(koka, 1, i, dbinv, true, blerjedealer, krijoartri, idGjuha, null, false);
                    clsNjesiAdministrative njesiadm = new clsNjesiAdministrative(i, dbRegj);
                    clsDegeAdministrative dege = new clsDegeAdministrative(njesiadm.IdDegeAdministrative, dbRegj);

                    clsKokaMagazina kokamag = new clsKokaMagazina();

                    u_ruajt = kokamag.krijoMagazine(false, kokamag.IdDokNga, konfmag.IdNivel, konfmag.IdKonfigAmbjente, koka.idKlientFurnitor, koka.kodKlientFurnitor, i, njesiadm.Kodi, koka.DtDok, koka.NrDok, koka.IdProjekt, koka.NrProjekt, konfmag.IdKategori, kokamag.IdDokNga, 0, blerjedealer ? 1 : 0, koka.IdNdermarrje, koka.IdNdermarrjeVit, koka.IdPerdoruesi, koka.DtRegjistrimi, 1, koka.pershkrimi, 0, 0, 0, dege.IdDegeAdministrative, dege.Kodi, 0, string.Empty, 0, string.Empty, false, koka.IdGrup1, koka.IdGrup2, koka.IdGrup3, string.Empty, string.Empty, string.Empty, coltrupi, new clsKokaMagazina(), new clsKokaFleteKontabel(), new clsKokaRezervime(), dbRegj, false, koka.IdAutomjet, koka.Targa, koka.IdRaportDesing, false, koka.IdKrijuesi, koka.dtTransportimi, koka.idKategoriSeriali, koka.nrSerial, string.Empty, string.Empty, 0, null,false,false,"","",0);
                    ImbLogger.LogTraceShitje($"Mbaroi metoda krijoMagazinatPerFature me parametra koka:{JsonConvert.SerializeObject(koka)}, idPeriudha:{idPeriudha}, eshteOwn:{eshteOwn}, kodkonfigurimi:" + kodkonfigurimi + $", blerjedealer:{blerjedealer}, krijoartri:{krijoartri}, idGjuha:{idGjuha}, ruajrenditje:{ruajrenditje}, promocione:{promocione}");
                    if (!u_ruajt.Status)
                    {
                        return u_ruajt;
                    }
                    u_ruajt = kokamag.ruaj(false, 0, null, idPeriudha, string.Empty, 51, 0, dbRegj, out shfaqmesazhapolupemag, 0, new DbQendraKosto.colTrupiQendraKosto(), eshteOwn, new colSerialetMagazine(), new colSerialetMagazine(), new clsKonfigurimAmbjenti(), new clsKonfigurimAmbjenti(), 0, 0, new DbQendraKosto.colTrupiQendraKosto(), false, new clsKokaShitje(), false, gjithmone, new colAmortizimiKoka(), 0, ruajrenditje, false, new colTrupiMagazina(), new colAmortizimiKoka(), new int[0], false, blerjedealer, promocione, out mesazhmevonshem, null, false, false);

                    if (!u_ruajt.Status)
                    {
                        return u_ruajt;
                    }
                }
                catch (Exception ex)
                {
                    ImbLogger.LogErrorShitje($"Exception:{ex}");
                    u_ruajt = new clsMesazh(false, ex.Message);
                    return u_ruajt;
                }
            return u_ruajt;
        }
        //u ben logot
        private static clsMesazh pergatitTrupPerMeme(clsKokaShitje shitjemema, clsDatabaseRegjistrim dbRegj, out colKonvertimi colkonvertimet, int idmagdefault, bool merrcmime, int idperiudha, bool isShitje, bool vendosIdTrupiTransferimi)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda pergatitTrupPerMeme me parametra shitjemema:{JsonConvert.SerializeObject(shitjemema)}, idmagdefault:{idmagdefault}, merrcmime:{merrcmime}, idperiudha:{idperiudha}, isShitje:{isShitje}");
            clsMesazh mesazh = new clsMesazh();
            colkonvertimet = new colKonvertimi();
            clsDatabaseKontabilitet dbkont = new clsDatabaseKontabilitet(dbRegj);
            clsDatabaseInventari dbinv = new clsDatabaseInventari(dbRegj);
            clsDatabaseAdmin dbadm = new clsDatabaseAdmin(dbRegj);
            object[] resultcmimi = new object[2];
            clsMonedha mon = new clsMonedha(shitjemema.idMonedha, dbadm);
            clsKlientFurnitor kf = new clsKlientFurnitor(shitjemema.idKlientFurnitor, dbkont);
            double totali = 0;
            double tvsh = 0;
            foreach (clsTrupiShitje trup in shitjemema.oColTrupiShitje)
            {

                clsNjesiArtikulli njesi = new clsNjesiArtikulli(trup.IdNjesia, dbinv);
                njesi = new clsNjesiArtikulli(njesi.KodNjesia, shitjemema.idNdermarje, dbinv);
                trup.IdNjesia = njesi.IdNjesia;
                if (trup.IdMagazina > 0)
                {
                    clsNjesiAdministrative mag = new clsNjesiAdministrative(trup.IdMagazina, dbRegj);
                    mesazh = mag.kontrollotransferim(mag, shitjemema.idNdermarje, dbRegj, shitjemema.idPerdoruesi, idperiudha);
                    if (!mesazh.Status)
                    {
                        ImbLogger.LogTraceShitje($"Mbaroi metoda pergatitTrupPerMeme me parametra idmagdefault:{idmagdefault}, merrcmime:{merrcmime}, idperiudha:{idperiudha}, isShitje:{isShitje}");
                        return mesazh;
                    }
                    mag = new clsNjesiAdministrative(mag.Kodi, shitjemema.idNdermarje, dbRegj);
                    trup.IdMagazina = mag.IdNjesiAdministrative;
                }
                DbRegjistrim.clsTaksa taksa = new clsTaksa();
                if (trup.Tvsh != 0)
                {
                    taksa = new DbRegjistrim.clsTaksa(trup.Tvsh, dbRegj);
                    mesazh = taksa.kontrollotransferim(taksa, shitjemema.idNdermarje, dbRegj, shitjemema.idPerdoruesi);
                    if (!mesazh.Status)
                    {
                        ImbLogger.LogTraceShitje($"Mbaroi metoda pergatitTrupPerMeme me parametra idmagdefault:{idmagdefault}, merrcmime:{merrcmime}, idperiudha:{idperiudha}, isShitje:{isShitje}");
                        return mesazh;
                    }
                    taksa = new DbRegjistrim.clsTaksa(taksa.KodTaksa, shitjemema.idNdermarje, dbRegj);
                    trup.Tvsh = taksa.IdTaksa;
                }

                if (trup.IdLlojVeprimi == 1)
                {
                    clsArtikulli art = (clsArtikulli)trup.Element;
                    art.merrSipasKodArtikullit(art.KodArtikulli, shitjemema.idNdermarje, dbinv);
                    if (art.IdArtikulli <= 0)
                    {
                        ImbLogger.LogTraceShitje("Mesazh: Artikulli nuk ekziston ne ndermarrjen meme");
                        return new clsMesazh(false, MessagesResource.Messages["msgArtikulliNukEkzistonNeNdermarrjenMeme"]);
                    }
                    trup.IdKodi = art.IdArtikulli;
                    trup.Element = art;
                    if (trup.IdDetajimArt > 0)
                    {
                        clsDetajimArtikulli det = new clsDetajimArtikulli(trup.IdDetajimArt, dbinv);
                        mesazh = det.kontrollotransferim(det, shitjemema.idNdermarje, dbinv, shitjemema.idPerdoruesi);

                        if (!mesazh.Status)
                        {
                            ImbLogger.LogTraceShitje($"Mbaroi metoda pergatitTrupPerMeme me parametra idmagdefault:{idmagdefault}, merrcmime:{merrcmime}, idperiudha:{idperiudha}, isShitje:{isShitje}");
                            return mesazh;
                        }
                        det.mbushDetajimArtikulli(det.KodDetajimArtikulli, shitjemema.idNdermarje, dbinv);
                        bool lidhurMeArt = clsDetajimArtikulli.ekzistonDetajimLidhurMeArtikullin(det.KodDetajimArtikulli, shitjemema.idNdermarje, trup.Kodi, 1, dbinv);
                        if (!lidhurMeArt)
                        {
                            mesazh = clsDetajimPerArt.ruajLidhje(art, det.IdDetajimArtikulli, 1, shitjemema.idNdermarje, shitjemema.idPerdoruesi, dbinv);
                            if (!mesazh.Status)
                            {
                                ImbLogger.LogTraceShitje($"Mbaroi metoda pergatitTrupPerMeme me parametra idmagdefault:{idmagdefault}, merrcmime:{merrcmime}, idperiudha:{idperiudha}, isShitje:{isShitje}");
                                return mesazh;
                            }
                        }

                        trup.IdDetajimArt = det.IdDetajimArtikulli;
                    }
                    if (trup.IdDetajimArt2 > 0)
                    {
                        clsDetajimArtikulli det2 = new clsDetajimArtikulli(trup.IdDetajimArt2, dbinv);
                        mesazh = det2.kontrollotransferim(det2, shitjemema.idNdermarje, dbinv, shitjemema.idPerdoruesi);
                        if (!mesazh.Status)
                        {
                            ImbLogger.LogTraceShitje($"Mbaroi metoda pergatitTrupPerMeme me parametra idmagdefault:{idmagdefault}, merrcmime:{merrcmime}, idperiudha:{idperiudha}, isShitje:{isShitje}");
                            return mesazh;
                        }
                        det2.mbushDetajimArtikulli(det2.KodDetajimArtikulli, shitjemema.idNdermarje, dbinv);
                        bool lidhurMeArt = clsDetajimArtikulli.ekzistonDetajimLidhurMeArtikullin(det2.KodDetajimArtikulli, shitjemema.idNdermarje, trup.Kodi, 2, dbinv);
                        if (!lidhurMeArt)
                        {
                            mesazh = clsDetajimPerArt.ruajLidhje(art, det2.IdDetajimArtikulli, 2, shitjemema.idNdermarje, shitjemema.idPerdoruesi, dbinv);
                            if (!mesazh.Status)
                            {
                                ImbLogger.LogTraceShitje($"Mbaroi metoda pergatitTrupPerMeme me parametra idmagdefault:{idmagdefault}, merrcmime:{merrcmime}, idperiudha:{idperiudha}, isShitje:{isShitje}");
                                return mesazh;
                            }
                        }
                        trup.IdDetajimArt2 = det2.IdDetajimArtikulli;
                    }

                    if (merrcmime)
                    {
                        resultcmimi = clsFunksione.merrCmimSipasNivelitMeDetajim(kf.IdNivelCmimi, art.KodArtikulli, shitjemema.idPerdoruesi, njesi, mon, shitjemema.dtDok.ToShortDateString(), decimal.Parse(shitjemema.kursi.ToString()), decimal.Parse(trup.Sasia.ToString()), shitjemema.idNdermarje, isShitje ? 0 : 1, dbinv, trup.IdDetajimArt > 0 ? trup.IdDetajimArt : 0, true);
                        trup.Cmimi = double.Parse(resultcmimi[0].ToString());
                        if (resultcmimi[1].ToString() == "0")
                        {
                            trup.VleftaPaTvsh = trup.Sasia * trup.Cmimi * (1 - trup.Zbritje / 100);
                            trup.VleftaMeTvsh = trup.VleftaPaTvsh * (1 + double.Parse(taksa.NormaPerqindje.ToString()) / 100);
                        }
                        else
                        {
                            trup.VleftaPaTvsh = trup.Sasia * (trup.Cmimi / (1 + double.Parse(taksa.NormaPerqindje.ToString()) / 100)) * (1 - trup.Zbritje / 100);
                            trup.VleftaMeTvsh = trup.VleftaPaTvsh * (1 + double.Parse(taksa.NormaPerqindje.ToString()) / 100);
                        }
                        tvsh += trup.VleftaPaTvsh * double.Parse(taksa.NormaPerqindje.ToString()) / 100;
                        totali += trup.VleftaMeTvsh;
                    }
                }
                else if (trup.IdLlojVeprimi == 3)
                {

                    clsLlogari llog = (clsLlogari)trup.Element;
                    llog = new clsLlogari(llog.NrLlogari, shitjemema.idNdermarje, dbkont);
                    if (llog.IdLlogari <= 0)
                    {
                        ImbLogger.LogWarningShitje("Mesazh: Nuk ekziston ndermarrje meme");
                        return new clsMesazh(false, MessagesResource.Messages["msgLlogariaNukEkzistonNeNdermarrjenMeme"]);
                    }
                    trup.IdKodi = llog.IdLlogari;
                    trup.Element = llog;
                }

                trup.IdTrupiKonvertimi = trup.IdTrupiTransferimi;
                if (trup.IdTrupiKonvertimi != 0)
                {
                    clsTrupiShitje tr = new clsTrupiShitje(trup.IdTrupiKonvertimi, dbRegj);
                    clsKokaShitje kok = new clsKokaShitje();
                    kok.mbushKokaShitjeSipasIDPaTrup(tr.IdShitjeKoka, dbRegj);
                    clsKonvertimi kon = new clsKonvertimi(0, shitjemema.idShitjeKoka, tr.IdShitjeKoka, kok.idKonfigAmbjente, shitjemema.idKonfigAmbjente);
                    if (colkonvertimet.Find(x => x.IdDokKonvertuar == tr.IdShitjeKoka && x.IdDokPasKonvertimi == shitjemema.idShitjeKoka && x.IdKonfigAmbjenteKonvertuar == kok.idKonfigAmbjente && x.IdKonfigAmbjentePasKonvertimi == shitjemema.idKonfigAmbjente) == null)
                        colkonvertimet.Add(kon);

                }
                if (vendosIdTrupiTransferimi)
                    trup.IdTrupiTransferimi = trup.IdShitjeTrupi;
                trup.IdShitjeTrupi = 0;
            }
            if (shitjemema.oKokaMagazina.OcolTrupiMagazina != null)
                foreach (clsTrupiMagazina trup in shitjemema.oKokaMagazina.OcolTrupiMagazina)
                {

                    clsNjesiArtikulli njesi = new clsNjesiArtikulli(trup.IdNjesia, dbinv);
                    njesi = new clsNjesiArtikulli(njesi.KodNjesia, shitjemema.idNdermarje, dbinv);
                    trup.IdNjesia = njesi.IdNjesia;

                    if (trup.IdMag > 0)
                    {
                        clsNjesiAdministrative mag = new clsNjesiAdministrative(trup.IdMag, dbRegj);
                        mag = new clsNjesiAdministrative(mag.Kodi, shitjemema.idNdermarje, dbRegj);
                        trup.IdMag = mag.IdNjesiAdministrative;
                    }


                    if (trup.IdLlojVeprimi == 1)
                    {
                        clsArtikulli art = (clsArtikulli)trup.Element;
                        art.merrSipasKodArtikullit(art.KodArtikulli, shitjemema.idNdermarje, dbinv);
                        if (art.IdArtikulli <= 0)
                        {
                            ImbLogger.LogWarningShitje("Mesazh: Nuk ekziston ndermarrje meme");
                            return new clsMesazh(false, MessagesResource.Messages["msgArtikulliNukEkzistonNeNdermarrjenMeme"]);
                        }
                        trup.IdArtikulli = art.IdArtikulli;
                        trup.Element = art;
                        if (trup.IdDetajimi > 0)
                        {
                            clsDetajimArtikulli det = new clsDetajimArtikulli(trup.IdDetajimi, dbinv);
                            det.mbushDetajimArtikulli(det.KodDetajimArtikulli, shitjemema.idNdermarje, dbinv);
                            trup.IdDetajimi = det.IdDetajimArtikulli;
                        }
                        if (trup.IdDetajimi2 > 0)
                        {
                            clsDetajimArtikulli det2 = new clsDetajimArtikulli(trup.IdDetajimi2, dbinv);
                            det2.mbushDetajimArtikulli(det2.KodDetajimArtikulli, shitjemema.idNdermarje, dbinv);
                            trup.IdDetajimi2 = det2.IdDetajimArtikulli;
                        }
                    }
                }
            if (merrcmime)
            {
                double zbritjeperqindje = shitjemema.totali == 0 ? 0 : shitjemema.zbritje / shitjemema.totali;

                shitjemema.totali = totali;
                shitjemema.tvsh = tvsh * (1 - zbritjeperqindje);
                shitjemema.zbritje = totali * zbritjeperqindje;

            }
            ImbLogger.LogTraceShitje($"Mbaroi metoda pergatitTrupPerMeme me parametra shitjemema:{JsonConvert.SerializeObject(shitjemema)}, idmagdefault:{idmagdefault}, merrcmime:{merrcmime}, idperiudha:{idperiudha}, isShitje:{isShitje}");
            return new clsMesazh(true, "Trupi u krijua me sukses!");
        }

        /// <summary>
        /// metode per te vendosur nr automatik tek nr i dokumentit dhe tek seriali i fatures se shitjes se gjeneruar nga urdher shitja per fature tatimore dhe kontrollon nese ekziston nje dokument me keto te dhena apo jo
        /// </summary>
        /// <param name="faturashitjengaurdhershitjamekupontatimor">fatura qe do ruhet</param>
        /// <param name="dbRegj">clsdatabaze regjistrim</param>
        /// <param name="dbAdmin">clsdatabaze admin</param>
        /// <returns>mesazh nese jane vendosur ne rregull nr apo jo</returns>
        private clsMesazh vendosNrAutomatikPerShitje(clsKokaShitje faturashitjengaurdhershitjamekupontatimor, clsDatabaseRegjistrim dbRegj, clsDatabaseAdmin dbAdmin)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda vendosNrAutomatikPerShitje me parametra faturashitjengaurdhershitjamekupontatimor:{JsonConvert.SerializeObject(faturashitjengaurdhershitjamekupontatimor)}");
            clsDatabaseShare dbshare = new clsDatabaseShare(dbRegj);
            clsMesazh mesazh = new clsMesazh();
            bool kaNdryshimNumri;
            List<NrAuto> list = new List<NrAuto>();
            int idnrautonrdok = clsAtributeTrupi.merrNrAutomatikSipasKontrollitDheKonfigurimit(faturashitjengaurdhershitjamekupontatimor.IdKonfigAmbjente, "txtNumer", 506, dbshare);
            string nrdokshitje = DbCore.DbAdmin.clsNrAutom.merrVlerenNrAutomatik(idnrautonrdok, faturashitjengaurdhershitjamekupontatimor.dtDok, dbAdmin);
            if (!String.IsNullOrEmpty(nrdokshitje))    //nqs ka nr automatik
            {
                ImbLogger.LogTraceShitje("Filloi kontrolli nqs ka numer automatik!");
                DbCore.DbAdmin.NrAuto nrdokshi = new NrAuto();
                nrdokshi.kodKontrolli = "txtNumer";
                nrdokshi.idNrAuto = idnrautonrdok;
                nrdokshi.vlereNrAuto = nrdokshitje;
                DbCore.DbAdmin.clsNrAutom nrauto = new clsNrAutom(dbAdmin, idnrautonrdok);
                nrdokshi = nrauto.kontrolloNrAutomatik(dbAdmin, nrdokshi, faturashitjengaurdhershitjamekupontatimor.dtDok);

                list.Add(nrdokshi);
                nrdokshitje = nrdokshi.vlereNrAuto;
            }
            else
            {
                ImbLogger.LogTraceShitje("Neqoftese nuk ka numer automatik merr nr e urdherit!");
                nrdokshitje = faturashitjengaurdhershitjamekupontatimor.nrDok;//nqs nuk ka nr automatik merr nr e urdherit
            }
            int idnrautonrserial = clsAtributeTrupi.merrNrAutomatikSipasKontrollitDheKonfigurimit(faturashitjengaurdhershitjamekupontatimor.IdKonfigAmbjente, "txtNumerSerial", 506, dbshare);
            string nrserialshitje = DbCore.DbAdmin.clsNrAutom.merrVlerenNrAutomatik(idnrautonrserial, faturashitjengaurdhershitjamekupontatimor.dtDok, dbAdmin);
            if (!String.IsNullOrEmpty(nrserialshitje))
            {
                ImbLogger.LogTraceShitje("Filloi kontrolli per numrin serial te shitjes!");
                DbCore.DbAdmin.NrAuto nrser = new NrAuto();
                nrser.kodKontrolli = "txtNumerSerial";
                nrser.idNrAuto = idnrautonrserial;
                nrser.vlereNrAuto = nrserialshitje;
                DbCore.DbAdmin.clsNrAutom nrauto = new clsNrAutom(dbAdmin, idnrautonrserial);
                nrser = nrauto.kontrolloNrAutomatik(dbAdmin, nrser, faturashitjengaurdhershitjamekupontatimor.dtDok);

                list.Add(nrser);
                nrserialshitje = nrser.vlereNrAuto;
            }
            else nrserialshitje = faturashitjengaurdhershitjamekupontatimor.nrSerial;
            ImbLogger.LogWarningShitje("Filloi ruajtja e vlerave te nr automatik!");
            mesazh = NrAuto.ruajvlera(out kaNdryshimNumri, list, faturashitjengaurdhershitjamekupontatimor.dtDok, faturashitjengaurdhershitjamekupontatimor.idPerdoruesi, faturashitjengaurdhershitjamekupontatimor.idNdermarje, dbAdmin);
            if (!mesazh.Status)
            {
                ImbLogger.LogTraceShitje("Mbaroi metoda vendosNrAutomatikPerShitje!");
                return mesazh;
            }
            faturashitjengaurdhershitjamekupontatimor.nrDok = nrdokshitje;
            faturashitjengaurdhershitjamekupontatimor.nrSerial = nrserialshitje;
            if (dbRegj.ekzistonRegjistrimShitjeSipasIdentifikuese(faturashitjengaurdhershitjamekupontatimor.IdShitjeKoka, faturashitjengaurdhershitjamekupontatimor.idNivel, faturashitjengaurdhershitjamekupontatimor.idKonfigAmbjente, faturashitjengaurdhershitjamekupontatimor.dtDok, faturashitjengaurdhershitjamekupontatimor.nrDok, faturashitjengaurdhershitjamekupontatimor.idNdermarje, faturashitjengaurdhershitjamekupontatimor.IdKlientFurnitor, faturashitjengaurdhershitjamekupontatimor.nrSerial, faturashitjengaurdhershitjamekupontatimor.idMenyrePagese, faturashitjengaurdhershitjamekupontatimor.idPikeShitjeFurnizimi, faturashitjengaurdhershitjamekupontatimor.idDegeAdministrative, faturashitjengaurdhershitjamekupontatimor.idRaportDesing, faturashitjengaurdhershitjamekupontatimor.idGrup1, faturashitjengaurdhershitjamekupontatimor.idGrup2, faturashitjengaurdhershitjamekupontatimor.idGrup3, faturashitjengaurdhershitjamekupontatimor.idAgjent))
            //if (dbRegj.ekzistonRegjistrimShitje(faturashitjengaurdhershitjamekupontatimor.idNivel, faturashitjengaurdhershitjamekupontatimor.idKonfigAmbjente, faturashitjengaurdhershitjamekupontatimor.dtDok, faturashitjengaurdhershitjamekupontatimor.nrDok, faturashitjengaurdhershitjamekupontatimor.idNdermarje))
            {
                ImbLogger.LogWarningShitje($"Ekziston një regjistrim me këto të dhëna identifikuese: faturashitjengaurdhershitjamekupontatimor.idShitjeKoka:{faturashitjengaurdhershitjamekupontatimor.IdShitjeKoka}, faturashitjengaurdhershitjamekupontatimor.idNivel:{faturashitjengaurdhershitjamekupontatimor.idNivel},faturashitjengaurdhershitjamekupontatimor.idKonfigAmbjente:{faturashitjengaurdhershitjamekupontatimor.idKonfigAmbjente}");
                return new clsMesazh(false, "Ekziston një regjistrim me këto të dhëna identifikuese!");
            }
            ImbLogger.LogTraceShitje($"Mbaroi metoda vendosNrAutomatikPerShitje me parametra faturashitjengaurdhershitjamekupontatimor:{JsonConvert.SerializeObject(faturashitjengaurdhershitjamekupontatimor)}");
            return mesazh;
        }

        /// <summary>
        /// Thirret nga importi. Kalon serverUrl si string bosh.
        /// </summary>        
        /// <returns></returns>
        public clsMesazh ruaj(int idGjuha, System.Globalization.CultureInfo ci, bool isShitje, IDictionary<string, object> hfregjistrime, int idPeriudha, colKonvertimi colkonvertimi, bool gjenerodokmagazine, out DbArkaBanka.clsVeprimBankaKoka banka, int skemaWorkFlow, DbRegjistrim.StatusAprovimi statusapp, int idetapa, out string shfaqmesazhapolupemag, out string shfaqmesazhapolupebanka, out string shfaqmesazhapolupeVDK, clsKokaShitje shitjemema, int idush, int idtransferimiTemp, bool dergoEmail, bool eshteOwn, bool dergoemailVFOne, string kodvodone, colSerialetMagazine serialet, clsKonfigurimAmbjenti konfigurimAmortizimi, clsKokaShitje faturashitjengaurdhershitjamekupontatimor, out bool printofature, out bool printogarancifature, out bool pagesefature, bool mekontabilizim, out string shfaqmesazhapolupe, string kodkonfigurimi, ResourceManager rm, bool vjenNgaImportSQL, string idDokImport, int iddokshitje, bool tollona, bool zevendesim, bool fatPermbNgaImportSQL, bool ndryshostatusdokgjenerues, string emerTabKoka, string primaryKeyEmerFushe, string emerFusheNdermarrje, bool tollonakastrati, bool tollonakastratielektronik, bool ruajrenditje, bool kontrolloSasiKonvertimiDheKthimi, colKokaShitje colshitjekonvertuar, bool kontrolloIMEIFifo, bool blerjedealer, bool promocione, ref DbData dbData, bool krijoartri, string kodKuponiDD, string msisdn, bool kthimVod, out string mesazhmevonshem, bool ngaImporti, bool nrDokMagBosh,string iic,string nivf)
        {
            return ruaj(idGjuha, "", isShitje, hfregjistrime, idPeriudha, colkonvertimi, gjenerodokmagazine, out banka, skemaWorkFlow, statusapp, idetapa, out shfaqmesazhapolupemag, out shfaqmesazhapolupebanka, out shfaqmesazhapolupeVDK, shitjemema, idush, idtransferimiTemp, dergoEmail, eshteOwn, dergoemailVFOne, kodvodone, serialet, konfigurimAmortizimi, faturashitjengaurdhershitjamekupontatimor, out printofature, out printogarancifature, out pagesefature, mekontabilizim, out shfaqmesazhapolupe, kodkonfigurimi, vjenNgaImportSQL, idDokImport, iddokshitje, tollona, zevendesim, fatPermbNgaImportSQL, ndryshostatusdokgjenerues, emerTabKoka, primaryKeyEmerFushe, emerFusheNdermarrje, tollonakastrati, tollonakastratielektronik, false, "", false, false, false, ruajrenditje, kontrolloSasiKonvertimiDheKthimi, colshitjekonvertuar, kontrolloIMEIFifo, blerjedealer, promocione, ref dbData, krijoartri, kodKuponiDD, msisdn, kthimVod, out mesazhmevonshem, ngaImporti, nrDokMagBosh,iic,nivf);//todo kevi

        }

        /// <summary>
        /// Ruan objektin e  kokes se dokumentit ne tabelen perkatese ne databaze.Therret funksionin        
        /// </summary>
        /// <param name="idGjuha"></param>
        /// <param name="ci"></param>
        /// <param name="serverUrl"></param>
        /// <param name="isShitje"> tregon nese dokumenti qe po regjistrojme eshte nje dokument shitje apo nje dokument blerje</param>
        /// <example> true -shitje, false-blerje</example>
        /// <returns> nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
        /// <param name="gjenerodokmagazine"></param>
        /// <param name="skemaWorkFlow"></param>
        /// <param name="nrDokMagBosh"> tregon nese fusha nr Dokumenti Magazine tek shitja eshte bosh, ne kete rast nr i dokumentit te magazines duhet te jete i njejte me nr e dokumentit te shitjes</param>
        public clsMesazh ruaj(int idGjuha, string serverUrl, bool isShitje, IDictionary<string, object> hfregjistrime, int idPeriudha, colKonvertimi colkonvertimi, bool gjenerodokmagazine, out DbArkaBanka.clsVeprimBankaKoka banka, int skemaWorkFlow, DbRegjistrim.StatusAprovimi statusapp, int idetapa, out string shfaqmesazhapolupemag, out string shfaqmesazhapolupebanka, out string shfaqmesazhapolupeVDK, clsKokaShitje shitjemema, int idush, int idtransferimiTemp, bool dergoEmail, bool eshteOwn, bool dergoemailVFOne, string kodvodone, colSerialetMagazine serialet, clsKonfigurimAmbjenti konfigurimAmortizimi, clsKokaShitje faturashitjengaurdhershitjamekupontatimor, out bool printofature, out bool printogarancifature, out bool pagesefature, bool mekontabilizim, out string shfaqmesazhapolupe, string kodkonfigurimi, bool vjenNgaImportSQL, string idDokImport, int iddokshitje, bool tollona, bool zevendesim, bool fatPermbNgaImportSQL, bool ndryshostatusdokgjenerues, string emerTabKoka, string primaryKeyEmerFushe, string emerFusheNdermarrje, bool tollonakastrati, bool tollonakastratielektronik, bool ndryshostatusTolloniLeter, string pikeshitje, bool ndryshostatusTolloniElektronik, bool ndryshostatusTolloniElektronikSpecifik, bool zevendesimtollonakastrati, bool ruajrenditje, bool kontrolloSasiKonvertimiDheKthimi, colKokaShitje colshitjekonvertuar, bool kontrolloIMEIFifo, bool blerjedealer, bool promocione, ref DbData dbData, bool krijoartri, string kodKuponiDD, string msisdn, bool kthimVod, out string mesazhmevonshem, bool ngaImporti, bool nrDokMagBosh,string iic,string nivf)
        {
            ImbLogger.LogTraceShitje($"Filloi clsMesazh ruaj per objektin me nr dokumenti {this.NrDok}. Metoda: clsMesazh ruaj(int idGjuha, string serverUrl, bool isShitje, IDictionary<string, object> hfregjistrime, int idPeriudha, colKonvertimi colkonvertimi, bool gjenerodokmagazine, out DbArkaBanka.clsVeprimBankaKoka banka, int skemaWorkFlow, DbRegjistrim.StatusAprovimi statusapp, int idetapa, out string shfaqmesazhapolupemag, out string shfaqmesazhapolupebanka, out string shfaqmesazhapolupeVDK, clsKokaShitje shitjemema, int idush, int idtransferimiTemp, bool dergoEmail, bool eshteOwn, bool dergoemailVFOne, string kodvodone, colSerialetMagazine serialet, clsKonfigurimAmbjenti konfigurimAmortizimi, clsKokaShitje faturashitjengaurdhershitjamekupontatimor, out bool printofature, out bool printogarancifature, out bool pagesefature, bool mekontabilizim, out string shfaqmesazhapolupe, string kodkonfigurimi, bool vjenNgaImportSQL, string idDokImport, int iddokshitje, bool tollona, bool zevendesim, bool fatPermbNgaImportSQL, bool ndryshostatusdokgjenerues, string emerTabKoka, string primaryKeyEmerFushe, string emerFusheNdermarrje, bool tollonakastrati, bool tollonakastratielektronik, bool ndryshostatusTolloniLeter, string pikeshitje, bool ndryshostatusTolloniElektronik, bool ndryshostatusTolloniElektronikSpecifik, bool zevendesimtollonakastrati, bool ruajrenditje, bool kontrolloSasiKonvertimiDheKthimi, colKokaShitje colshitjekonvertuar, bool kontrolloIMEIFifo, bool blerjedealer, bool promocione, ref DbData dbData, bool krijoartri, string kodKuponiDD, string msisdn, bool kthimVod, out string mesazhmevonshem, bool ngaImporti)");
            mesazhmevonshem = "";
            shfaqmesazhapolupebanka = "jo";
            shfaqmesazhapolupeVDK = "jo";
            shfaqmesazhapolupe = "jo";
            shfaqmesazhapolupemag = "jo";
            bool kaNdryshimNumri;
            printogarancifature = false;
            pagesefature = false;
            printofature = false;
            this.IIC = iic;
            clsDatabaseShare dbshare = new clsDatabaseShare(dbData);
            bool kontrollodisponibel = clsAlternativaKushti.getAlternativa(this.IdKonfigAmbjente, "KD", dbshare) == "Po";
            bool kontrollodisponibelmema = clsAlternativaKushti.getAlternativa(shitjemema.IdKonfigAmbjente, "KD", dbshare) == "Po";
            bool eshteShitjeBazaar = false;//nese po thirr metoden per dergimin e komandes
            string kodOferteBundle = string.Empty;
            String kodNiveli = clsNivelRegjistrimi.ktheKodNivelRegjistrimi(this.idNivel);
            bool fshidok = false; bool eshteKlientSpecifik = false;
            if (colshitjekonvertuar.Count > 0)
            {
                if (clsAlternativaKushti.getAlternativa(colshitjekonvertuar[0].IdKonfigAmbjente, "FDOKPGJDPKLJOSP", dbshare) == "Po")
                    fshidok = true;
                eshteKlientSpecifik = clsKlientFurnitor.EshteKlientSpecifik(colshitjekonvertuar[0].IdKlientFurnitor, new clsDatabaseKontabilitet(dbData));
            }
            clsMesazh u_ruajt = new clsMesazh();
            clsMesazh mesazhKontrolli = new clsMesazh();
            banka = new clsVeprimBankaKoka();

            try
            {
                int nrRreshtaTrupi = this.oColTrupiShitje.Count;
                System.Diagnostics.Stopwatch myWatchTransaksion = new System.Diagnostics.Stopwatch();
                myWatchTransaksion.Start();

                using (var scope = new MyTransactionScope(dbData))
                {
                    clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin(dbData);
                    clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim(dbData);
                    mesazhKontrolli = kontrolloShitje(out kaNdryshimNumri, dbRegj, hfregjistrime, false, true);
                    if (kaNdryshimNumri && mesazhKontrolli.PershkrimMesazhi.Contains("NrDok"))
                    {
                        if (nrDokMagBosh)
                            NrDokMagazine = NrDok;// marrim nr e ri automatik edhe per magazinen, meqe eshte bosh NrDokMagazine dhe duhet te jete njelloj me nrdok te shitjes 
                        if (!String.IsNullOrEmpty(shitjemema.nrDok) && kodNiveli == "OB")
                            shitjemema.nrDok = NrDok + "-" + new clsNdermarrje(IdNdermarrje, dbAdmin).NdermarrjeKodi;
                    }
                       

                    clsDatabazeTollona dbtollona = new clsDatabazeTollona(dbData);

                    if (!mesazhKontrolli.Status)
                    {
                        shfaqmesazhapolupemag = "jo";
                        return mesazhKontrolli;
                    }
                    if (fshidok && !eshteKlientSpecifik)
                    {
                        System.Diagnostics.Stopwatch myWatchPer = new System.Diagnostics.Stopwatch();
                        myWatchPer.Start();
                        try
                        {
                            //info 
                            clsMesazh msgFshi = colshitjekonvertuar.fshiUshPermbledhur(this.idPerdoruesi, dbRegj);
                            if (!msgFshi.Status)
                            {
                                return msgFshi;
                            }
                        }
                        catch (Exception e)
                        {
                            ImbLogger.LogErrorShitje($"Exception :{e}");
                            ImbLogger.Error(e);
                            System.Diagnostics.Debug.WriteLine("exception" + e + myWatchPer.Elapsed);
                            throw e;
                        }
                        myWatchPer.Stop();
                        ImbLogger.LogWarningShitje("fshiUSHPERMB: " + myWatchPer.Elapsed);
                        System.Diagnostics.Debug.WriteLine("fshiUSHPERMB: " + myWatchPer.Elapsed);
                    }
                    else if (colshitjekonvertuar.Count > 0)
                    {
                        clsMesazh msgKontrollo = colshitjekonvertuar.KontrolloPermbledhur();
                        if (!msgKontrollo)
                        {
                            return msgKontrollo;
                        }
                    }
                    u_ruajt = this.ruajShitje(serverUrl, this, isShitje, false, idPeriudha, dbRegj, colkonvertimi, gjenerodokmagazine, skemaWorkFlow, statusapp, idetapa, out shfaqmesazhapolupemag, new DbQendraKosto.colTrupiQendraKosto(), kontrollodisponibel, eshteOwn, serialet, konfigurimAmortizimi, new colAmortizimiKoka(), mekontabilizim, new DbQendraKosto.colTrupiQendraKosto(), out shfaqmesazhapolupe, kodkonfigurimi, 0, 0, iddokshitje, idGjuha, 0, tollona, zevendesim, tollonakastrati, tollonakastratielektronik, zevendesimtollonakastrati, ruajrenditje, kontrolloSasiKonvertimiDheKthimi, false, kontrolloIMEIFifo, blerjedealer, promocione, out mesazhmevonshem, false,new clsKokaShitje());

                    if (!u_ruajt.Status)
                    {
                        // dbRegj.rollbackTransaksion();
                        return u_ruajt;
                    }
                    if (kthimVod)
                    {
                        u_ruajt = krijoMagazinatPerFature(this, dbRegj, idPeriudha, out shfaqmesazhapolupemag, eshteOwn, "FHKTHIM", blerjedealer, krijoartri, idGjuha, ruajrenditje, promocione);
                        if (!u_ruajt.Status)
                        {
                            // dbRegj.rollbackTransaksion();
                            return u_ruajt;
                        }
                    }
                    if (blerjedealer)
                    {
                        u_ruajt = krijoMagazinatPerFature(this, dbRegj, idPeriudha, out shfaqmesazhapolupemag, eshteOwn, "FHDealer", blerjedealer, krijoartri, idGjuha, ruajrenditje, promocione);
                        if (!u_ruajt.Status)
                        {
                            return u_ruajt;
                        }
                    }
                    if (ndryshostatusdokgjenerues)
                    {
                        u_ruajt = dbRegj.modifikoKokaShitjeStatusGjenerimi(this.idGjenerues, true);
                        if (!u_ruajt.Status)
                        {
                            // dbRegj.rollbackTransaksion();
                            return u_ruajt;
                        }
                    }
                    if (ndryshostatusTolloniLeter)
                    {
                        u_ruajt = dbtollona.ndryshoStatusTolloniLeter(pikeshitje, this.dtDok, true);
                        if (!u_ruajt.Status)
                        {
                            // dbRegj.rollbackTransaksion();
                            return u_ruajt;
                        }
                    }
                    if (ndryshostatusTolloniElektronik)
                    {
                        u_ruajt = dbtollona.ndryshoStatusTollonElektronik(this.dtDok, pikeshitje, true);
                        if (!u_ruajt.Status)
                        {
                            // dbRegj.rollbackTransaksion();
                            return u_ruajt;
                        }
                    }
                    if (ndryshostatusTolloniElektronikSpecifik)
                    {
                        u_ruajt = dbtollona.ndryshoStatusTollonElektronikSpecifik(this.dtDok, pikeshitje, true, this.kodKlientFurnitor);
                        if (!u_ruajt.Status)
                        {
                            // dbRegj.rollbackTransaksion();
                            return u_ruajt;
                        }
                    }

                    if (vjenNgaImportSQL && idDokImport != string.Empty)
                    {
                        clsMesazh mesazh = updateStatusImporti(idDokImport, fatPermbNgaImportSQL, dbRegj, emerTabKoka, primaryKeyEmerFushe, emerFusheNdermarrje, idNdermarje);

                        if (!mesazh.Status)
                        {
                            return mesazh;
                        }
                    }

                    if (!string.IsNullOrEmpty(faturashitjengaurdhershitjamekupontatimor?.nrDok))
                    {
                        colKonvertimi colkonvshitje = new colKonvertimi();
                        colkonvshitje.Add(new clsKonvertimi(0, 0, this.idShitjeKoka, this.idKonfigAmbjente, faturashitjengaurdhershitjamekupontatimor.idKonfigAmbjente));///krijojme konvertimin
                        foreach (clsTrupiShitje trup in faturashitjengaurdhershitjamekupontatimor.oColTrupiShitje) //kalojme id e konvertimit
                        {
                            trup.IdTrupiKonvertimi = trup.IdShitjeTrupi;
                        }

                        u_ruajt = vendosNrAutomatikPerShitje(faturashitjengaurdhershitjamekupontatimor, dbRegj, dbAdmin);

                        string printimfature = clsAtributeTrupi.merrVleredefaultSipasKontrollitDheKonfigurimit(faturashitjengaurdhershitjamekupontatimor.idKonfigAmbjente, "cbPrinto", 506, dbshare);
                        if (printimfature == "true")
                            printofature = true;
                        string printimgaranci = clsAtributeTrupi.merrVleredefaultSipasKontrollitDheKonfigurimit(faturashitjengaurdhershitjamekupontatimor.idKonfigAmbjente, "cbGaranci", 506, dbshare);
                        if (printimgaranci == "true")
                            printogarancifature = true;
                        if (!u_ruajt.Status)
                        {
                            // dbRegj.rollbackTransaksion();
                            return u_ruajt;
                        }
                        faturashitjengaurdhershitjamekupontatimor.faturePermbledhese = true;
                        string mesazhLupeMag = "", mesazhLupe = "";
                        u_ruajt = faturashitjengaurdhershitjamekupontatimor.ruajShitje(serverUrl, faturashitjengaurdhershitjamekupontatimor, isShitje, false, idPeriudha, dbRegj, colkonvshitje, false, skemaWorkFlow, statusapp, idetapa, out mesazhLupeMag, new DbQendraKosto.colTrupiQendraKosto(), false, eshteOwn, serialet, konfigurimAmortizimi, new colAmortizimiKoka(), true, new DbQendraKosto.colTrupiQendraKosto(), out mesazhLupe, kodkonfigurimi, 0, 0, 0, idGjuha, 0, tollona, zevendesim, tollonakastrati, tollonakastratielektronik, zevendesimtollonakastrati, ruajrenditje, kontrolloSasiKonvertimiDheKthimi, false, kontrolloIMEIFifo, blerjedealer, promocione, out mesazhmevonshem, false,new clsKokaShitje());
                        if (!u_ruajt.Status)
                        {
                            // dbRegj.rollbackTransaksion();
                            return u_ruajt;
                        }
                    }
                    if (shitjemema.nrDok != null && shitjemema.nrDok != string.Empty)
                    {
                        if (kodkonfigurimi.Length < 5 || kodkonfigurimi.Substring(0, 5) != "VFONE")
                            shitjemema.idTransferimi = this.idShitjeKoka;
                        shitjemema.oColTrupiShitje = this.oColTrupiShitje;
                        //clsNjesiAdministrative mag = new clsNjesiAdministrative("MQ", shitjemema.idNdermarje, dbRegj);kodNiveli == "FSH" ? mag.IdNjesiAdministrative : 0

                        u_ruajt = pergatitTrupPerMeme(shitjemema, dbRegj, out colkonvertimi, 0, kodNiveli == "OB" ? true : false, idPeriudha, isShitje, kodkonfigurimi.Length < 5 || kodkonfigurimi.Substring(0, 5) != "VFONE");

                        if (!u_ruajt.Status)
                        {
                            // dbRegj.rollbackTransaksion();
                            return u_ruajt;
                        }

                        u_ruajt = shitjemema.ruajShitje(serverUrl, shitjemema, !isShitje, false, idPeriudha, dbRegj, colkonvertimi, gjenerodokmagazine, skemaWorkFlow, statusapp, idetapa, out shfaqmesazhapolupemag, new DbQendraKosto.colTrupiQendraKosto(), kontrollodisponibelmema, eshteOwn, serialet, konfigurimAmortizimi, new colAmortizimiKoka(), mekontabilizim, new DbQendraKosto.colTrupiQendraKosto(), out shfaqmesazhapolupe, kodkonfigurimi, 0, 0, 0, idGjuha, 0, tollona, zevendesim, tollonakastrati, tollonakastratielektronik, zevendesimtollonakastrati, ruajrenditje, kontrolloSasiKonvertimiDheKthimi, false, kontrolloIMEIFifo, blerjedealer, promocione, out mesazhmevonshem, false,new clsKokaShitje());
                        if (!u_ruajt.Status)
                        {
                            // dbRegj.rollbackTransaksion();
                            return u_ruajt;
                        }

                        u_ruajt = dbRegj.modifikoKokaShitjeStatusTransferimi(idush, StatusTrasferimi.Konvertuar);
                        if (!u_ruajt.Status)
                        {
                            // dbRegj.rollbackTransaksion();
                            return u_ruajt;
                        }

                        if (kodNiveli == "FSH")
                        {
                            u_ruajt = krijoMagazinatPerFature(shitjemema, dbRegj, idPeriudha, out shfaqmesazhapolupemag, eshteOwn, "FHK", blerjedealer, krijoartri, idGjuha, ruajrenditje, promocione);
                            if (!u_ruajt.Status)
                            {
                                // dbRegj.rollbackTransaksion();
                                return u_ruajt;
                            }
                        }

                        if (clsAlternativaKushti.getAlternativa(this.IdKonfigAmbjente, "GJUSHNMD", dbshare) == "Po" && this.idStatusDok == 0)
                        {
                            clsKonfigurimAmbjenti konfAmb = new clsKonfigurimAmbjenti(shitjemema.IdKonfigAmbjente, dbshare);
                            if (konfAmb.KodKonfigAmbjente == "USH")
                            {
                                bool own = clsKlientFurnitor.eshteNdermarrjeKlientiOwn(shitjemema.IdKlientFurnitor);
                                List<int> shitjepertrasferim = new List<int>();
                                shitjepertrasferim.Add(shitjemema.idShitjeKoka);
                                if (!own)
                                {
                                    u_ruajt = clsFunksione.eksportAutomatikDokumentesh(clsServerConfiguration.LexoKonfigurimSipasKey<string>(ServerKonfigKey.FORMAT_EKSPORTI_TRANSFER_USH), shitjemema.idNdermarje, idPerdoruesi, shitjepertrasferim, true);
                                    if (!u_ruajt.Status)
                                        return u_ruajt;
                                }
                                else
                                {
                                    DataTable err = new DataTable();
                                    err.Columns.Add("Kodi");
                                    err.Columns.Add("Gabimi");
                                    err.Columns.Add("Rreshti");
                                    int nrreshta = 0, nrOk = 0;
                                    clsKokaShitje.KonvertoDokumentaShitjeNeDokMagazine(shitjepertrasferim, IdPerdoruesi, err, ref nrreshta, this.IdNdermarrje, ref nrOk, dbData, false);
                                    if (err.Rows.Count > 0)
                                    {
                                        u_ruajt = new clsMesazh(false, err.Rows[0].ItemArray[1].ToString());
                                        return u_ruajt;
                                    }
                                    else
                                    {
                                        u_ruajt = clsKokaShitje.ndryshoStatusTransferimi(shitjemema.idShitjeKoka, StatusTrasferimi.Transferuar);
                                        if (!u_ruajt)
                                            return u_ruajt;
                                    }
                                }
                            }
                        }

                    }
                    if (HfArkiva != null)
                    {
                        //ruajme arkiven per kete dokuement nese eshte ruajtur me sukses koka e dokuemntit
                        clsMesazh arkmesazh = colArkiva.RuajArkiven(idShitjeKoka, isShitje ? 1 : 2, idKrijuesi, idNdermarje,
                                 HfArkiva);
                        if (!arkmesazh.Status)
                        {
                            return arkmesazh;
                        }
                    }
                    clsMesazh mesazhArkeBanke = krijoDokArkeBanke(faturashitjengaurdhershitjamekupontatimor, ref pagesefature, ref banka, isShitje, ref shfaqmesazhapolupebanka, ref shfaqmesazhapolupeVDK, idPeriudha, dbRegj, dbshare);
                    if (!mesazhArkeBanke.Status)
                    {
                        return mesazhArkeBanke;
                    }
                    if (idtransferimiTemp != 0)
                    {
                        u_ruajt = dbRegj.modifikoStatusTrigeri(idtransferimiTemp);
                        if (!u_ruajt.Status)
                        {
                            return u_ruajt;
                        }
                    }

                    #region bazaari dhe device with 
                    eshteShitjeBazaar = (new clsKonfigurimAmbjenti(IdKonfigAmbjente, dbshare)).KodKonfigAmbjente.Contains("BAZAAR");
                    if (eshteShitjeBazaar)
                    {
                        clsArtikulli art = new clsArtikulli(oColTrupiShitje[0].IdKodi, new clsDatabaseInventari(dbRegj));
                        kodOferteBundle = art.KodOferte;

                        if (art.AparatBazaar)
                        {//bazaar
                            ImbLogger.LogInfoPromocione(string.Format("Po tentohet te update-ohet statusi i klientit per bazaar  me msisdn {0} nga perdoruesi {1}", msisdn, idPerdoruesi));
                            u_ruajt = clsKlientPerBazaar.UpdateStatusPerdorur(dbRegj, msisdn, LlojMsisdn.Bazaar, idShitjeKoka, idPerdoruesi);

                            if (!u_ruajt.Status)
                            {
                                ImbLogger.LogErrorPromocione(string.Format("Ndodhi nje gabim gjate updatetimi te klientit per bazaar  me msisdn {0} nga perdoruesi {1}", msisdn, idPerdoruesi), u_ruajt.PershkrimMesazhi);
                                //dbRegj.rollbackTransaksion();
                                return u_ruajt;
                            }
                            ImbLogger.LogInfoPromocione(string.Format("klienti per bazaar me msisdn {0} u shenua si i perdorur nga perdoruesi {1}", msisdn, idPerdoruesi));
                        }


                        if (!string.IsNullOrEmpty(kodOferteBundle))
                        {
                            clsMesazh mesazh = AktivizoBundle(kontakti, kodOferteBundle, " bundle per bazaar", "0", idPerdoruesi);
                            if (!mesazh.Status)
                            {
                                fshiFunction(idPerdoruesi, true, false, false, dbRegj);

                            }
                            u_ruajt.PershkrimMesazhi += mesazh.PershkrimMesazhi;
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(kodKuponiDD))
                    {
                        ImbLogger.LogInfoPromocione(string.Format("Po tentohet te update-ohet statusi i kuponit me kodin {0} nga perdoruesi {1}", kodKuponiDD, idPerdoruesi));
                        u_ruajt = clsKlientMeKupon.UpdateStatusPerdorur(dbRegj, kodKuponiDD, kontakti, idShitjeKoka, idPerdoruesi);
                        ImbLogger.LogInfoPromocione(string.Format("Po tentohet te update-ohet statusi i kuponit me kodin {0} nga perdoruesi {1}", kodKuponiDD, idPerdoruesi));
                        ImbLogger.LogInfoPromocione(string.Format("kuponi me kodin  {0} u shenua si i perdorur nga perdoruesi {1}", kodKuponiDD, idPerdoruesi));
                        ImbLogger.LogInfoPromocione(string.Format("Po tentohet te update-ohet statusi i  msisdn {0} nga perdoruesi {1} per DD", kontakti, idPerdoruesi));
                        u_ruajt = clsKlientPerBazaar.UpdateStatusPerdorur(dbRegj, kontakti, LlojMsisdn.DeviceWithDiscount, idShitjeKoka, idPerdoruesi);
                        if (!u_ruajt.Status)
                        {
                            ImbLogger.LogErrorPromocione(string.Format("Ndodhi nje gabim gjate updatetimi te klientit per bazaar  me msisdn {0} nga perdoruesi {1}", kontakti, idPerdoruesi), u_ruajt.PershkrimMesazhi);
                            //dbRegj.rollbackTransaksion();
                            return u_ruajt;
                        }
                        ImbLogger.LogInfoPromocione(string.Format(" msisdn {0} u shenua si i perdorur nga perdoruesi {1}", msisdn, idPerdoruesi));
                    }

                    if (kodvodone != "")
                    {
                        ImbLogger.LogInfoPromocione(string.Format("dokumenti me nr {0} u ruajt me sukses nga perdoruesi {1}", nrDok, idPerdoruesi));
                        var vfOneAdapter = new PromocioneAdapter();
                        clsMesazh mesazh = vfOneAdapter.DergoDhuratenEZgjedhur(this.kontakti, kodvodone);
                        if (!mesazh.Status)
                        {
                            fshiFunction(idPerdoruesi, true, false, false, dbRegj);
                            if (shitjemema.nrDok != null && shitjemema.nrDok != string.Empty)
                                shitjemema.fshiFunction(idPerdoruesi, true, false, false, dbRegj);
                            return mesazh;
                        }
                        u_ruajt.PershkrimMesazhi += " Piket u zbriten me sukses!";
                    }
                    #endregion


                    scope.Complete(out dbData);
                }




                myWatchTransaksion.Stop();
                string debugMsg = $"TRANSAKSION: {myWatchTransaksion.Elapsed} ; nrRreshta: {nrRreshtaTrupi}";
                System.Diagnostics.Debug.WriteLine(debugMsg);
                ImbLogger.Info(debugMsg);
                //dbRegj.commitTransaksion();
                if (dergoEmail)
                {
                    EmailComposer.dergoEmailFaturen(idGjuha, idPerdoruesi, idNdermarjeVit, idNdermarje, idShitjeKoka, IdKlientFurnitor, NrDok, dtDok);
                    //to do kevi dergo email nga emaili i konfiguruar tek ambjenti i konfigurimit te emailit tek emaili i tabeles t_klientfurnitor per klientit e fatures  me tekst Porosia juaj nr xxx, dt 0x/0x/201x u modifikua
                }

                if (dergoemailVFOne)
                {
                    clsMesazh msgDergoEmailVfOne = EmailComposer.dergoEmailRezervimVodOne(idNdermarje, idPerdoruesi, this.oColTrupiShitje, NrDok, DtDok, this.IdKonfigAmbjente, true, false);
                    if (!msgDergoEmailVfOne.Status)
                        ImbLogger.LogErrorPromocione(string.Format("Ndodhi nje gabim gjate dergimit te email dergoEmailRezervimVodOne idNdermarje: {0}, perdoruesi: {1}, nrdok: {2}, dtdok: {3}, IdKonfigAmbjente: {4}, dergoEmailVfOne : {5}, dergoEmailPorosi:{6} ", idNdermarje, idPerdoruesi, NrDok, DtDok, IdKonfigAmbjente), msgDergoEmailVfOne.PershkrimMesazhi, true, false);
                }
                if (clsAlternativaKushti.getAlternativa(this.IdKonfigAmbjente, "DEPOROSI", dbshare) == "Po")
                {
                    clsMesazh msgDergoEmailVfOne = EmailComposer.dergoEmailRezervimVodOne(idNdermarje, idPerdoruesi, this.oColTrupiShitje, NrDok, DtDok, this.IdKonfigAmbjente, false, true);
                    if (!msgDergoEmailVfOne.Status)
                        ImbLogger.LogErrorPromocione(string.Format("Ndodhi nje gabim gjate dergimit te email dergoEmailRezervimVodOne idNdermarje: {0}, perdoruesi: {1}, nrdok: {2}, dtdok: {3}, IdKonfigAmbjente: {4}, dergoEmailVfOne : {5}, dergoEmailPorosi:{6} ", idNdermarje, idPerdoruesi, NrDok, DtDok, IdKonfigAmbjente), msgDergoEmailVfOne.PershkrimMesazhi, false, true);
                }
                if (kaNdryshimNumri)
                {
                    return mesazhKontrolli;
                }
                if (u_ruajt.PershkrimMesazhi == "Ruajtja përfundoi me sukses!")
                    u_ruajt.PershkrimMesazhi = MessagesResource.Messages["msgDokumentiURuajtMeSukese"];
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
            catch (Exception ex)
            {
                ImbLogger.Error(ex);
                u_ruajt.PershkrimMesazhi = ex.Message;
                u_ruajt.Status = false;
                if (ex.Data.Contains("SerialetKeq")) throw ex;
                ImbLogger.Error(ex, "Ndodhi nje gabim gjate ruajtjes se fatures se shitjes");
                return u_ruajt;
            }
            return u_ruajt;
        }

        public static clsMesazh updateStatusImporti(string idDokImport, bool fatPermbNgaImportSQL, clsDatabaseRegjistrim dbRegj, string emerTabKoka, string primaryKeyEmerFushe, string emerFusheNdermarrje, int idNdermarje)
        {
            ImbLogger.LogTraceShitje("Filloi clsMesazh updateStatusImporti me parametra idDokImport:" + idDokImport + $", fatPermbNgaImportSQL:{fatPermbNgaImportSQL}, emerTabKoka:" + emerTabKoka + $", primaryKeyEmerFushe:" + primaryKeyEmerFushe + $", emerFusheNdermarrje:" + emerFusheNdermarrje + $", idNdermarje:{idNdermarje}");
            string[] idte = idDokImport.Split(';');
            for (var i = 0; i < idte.Length; i++)
            {
                if (idte[i] == string.Empty)
                    continue;

                int statusi = 1;
                if (fatPermbNgaImportSQL)
                    statusi = 2;

                clsMesazh mesazh = dbRegj.updateDokTabeleTemportal(idte[i], idNdermarje, statusi, emerTabKoka, primaryKeyEmerFushe, emerFusheNdermarrje);
                if (!mesazh.Status)
                {
                    ImbLogger.LogTraceShitje("Mbaroi clsMesazh updateStatusImporti me parametra idDokImport:" + idDokImport + $", fatPermbNgaImportSQL:{fatPermbNgaImportSQL}, emerTabKoka:" + emerTabKoka + $", primaryKeyEmerFushe:" + primaryKeyEmerFushe + $", emerFusheNdermarrje:" + emerFusheNdermarrje + $", idNdermarje:{idNdermarje}");
                    return mesazh;
                }
            }
            ImbLogger.LogTraceShitje("Mbaroi clsMesazh updateStatusImporti me parametra idDokImport:" + idDokImport + $", fatPermbNgaImportSQL:{fatPermbNgaImportSQL}, emerTabKoka:" + emerTabKoka + $", primaryKeyEmerFushe:" + primaryKeyEmerFushe + $", emerFusheNdermarrje:" + emerFusheNdermarrje + $", idNdermarje:{idNdermarje}");
            return new clsMesazh(true, "U be update statusi i importit ne tabelen temporale per te gjithe dokumentet per import.");
        }

        public clsMesazh krijoDokArkeBanke(clsKokaShitje faturashitjengaurdhershitjamekupontatimor, ref bool pagesefature, ref DbArkaBanka.clsVeprimBankaKoka banka, bool isShitje, ref string shfaqmesazhapolupebanka, ref string shfaqmesazhapolupeVDK, int idPeriudha, clsDatabaseRegjistrim dbRegj, clsDatabaseShare dbshare)
        {
            ImbLogger.LogTraceShitje($"Filloi clsMesazh krijoDokArkeBanke me parametra faturashitjengaurdhershitjamekupontatimor:{JsonConvert.SerializeObject(faturashitjengaurdhershitjamekupontatimor)}, isShitje:{isShitje}, int idPeriudha:{idPeriudha}");
            clsMesazh u_ruajt;
            string menyrepagese = "";
            if (faturashitjengaurdhershitjamekupontatimor != null && faturashitjengaurdhershitjamekupontatimor.idKonfigAmbjente != 0)
            {
                menyrepagese = clsAtributeTrupi.merrVleredefaultSipasKontrollitDheKonfigurimit(faturashitjengaurdhershitjamekupontatimor.idKonfigAmbjente, "cmbMenyrePagese", 506, dbshare);
            }
            if (menyrepagese == "4")
                pagesefature = true;
            else if ((menyrepagese == "4" || kodMenyrePagese == "Pagese") && (Totali - Zbritje) == 0)
            {
                ImbLogger.LogErrorShitje("Ju lutem zgjidhni nje menyre tjeter pagese pasi totali i fatures eshte 0!");
                return new clsMesazh(false, MessagesResource.Messages["msgZgjidhniTjeterMenyrePagese"]);
            }
            else if (menyrepagese == "5" && faturashitjengaurdhershitjamekupontatimor != null && faturashitjengaurdhershitjamekupontatimor?.totali != 0)
            {
                clsDatabaseKontabilitet dbkont = new clsDatabaseKontabilitet(dbRegj);
                DbArkaBanka.clsDatabaseArkaBanka db = new DbArkaBanka.clsDatabaseArkaBanka(dbRegj);
                try
                {
                    banka = krijoVeprimBanka(faturashitjengaurdhershitjamekupontatimor, faturashitjengaurdhershitjamekupontatimor.idStatusDok, idPeriudha, faturashitjengaurdhershitjamekupontatimor.OFleteKontabel.NrDukumentiKokaFleteKontabel != null ? true : false, isShitje, db, out shfaqmesazhapolupebanka, out shfaqmesazhapolupeVDK, DbCore.DbKontabiliteti.clsKlientFurnitor.MerrIdBanke(faturashitjengaurdhershitjamekupontatimor.idKlientFurnitor, dbkont), DbCore.DbKontabiliteti.clsKlientFurnitor.MerrLlojin(faturashitjengaurdhershitjamekupontatimor.idKlientFurnitor, dbkont));
                }
                catch (MyException mine)
                {
                    ImbLogger.LogErrorShitje($"Exception:{mine}");
                    ImbLogger.Error(mine);
                    ImbLogger.LogTraceShitje($"Mbaroi clsMesazh krijoDokArkeBanke me parametra faturashitjengaurdhershitjamekupontatimor:{JsonConvert.SerializeObject(faturashitjengaurdhershitjamekupontatimor)}, isShitje:{isShitje}, int idPeriudha:{idPeriudha}");
                    return new clsMesazh(false, mine.Message);
                }
                catch (Exception ex)
                {
                    ImbLogger.LogErrorShitje($"Exception:{ex}");
                    ImbLogger.Error(ex);
                    ImbLogger.LogTraceShitje($"Mbaroi clsMesazh krijoDokArkeBanke me parametra faturashitjengaurdhershitjamekupontatimor:{JsonConvert.SerializeObject(faturashitjengaurdhershitjamekupontatimor)}, isShitje:{isShitje}, int idPeriudha:{idPeriudha}");
                    return new clsMesazh(false, MessagesResource.Messages["msgNdodhi1GabimGjateKrijimitTeVeprimitTeBankes"]);
                }
                u_ruajt = banka.ruaj(null, db, 0, StatusAprovimi.Undefined, 0, string.Empty, false);
                if (!u_ruajt.Status)
                {
                    ImbLogger.LogTraceShitje($"Mbaroi clsMesazh krijoDokArkeBanke me parametra faturashitjengaurdhershitjamekupontatimor:{JsonConvert.SerializeObject(faturashitjengaurdhershitjamekupontatimor)}, isShitje:{isShitje}, int idPeriudha:{idPeriudha}");
                    return new clsMesazh(false, "Dokumenti i bankës :" + u_ruajt.PershkrimMesazhi);
                }
            }
            else if (kodMenyrePagese == "Pagese Automatike" && Totali != 0)
            {
                clsDatabaseKontabilitet dbkont = new clsDatabaseKontabilitet(dbRegj);
                DbArkaBanka.clsDatabaseArkaBanka db = new DbArkaBanka.clsDatabaseArkaBanka(dbRegj);
                try
                {
                    banka = krijoVeprimBanka(this, this.idStatusDok, idPeriudha, this.OFleteKontabel.NrDukumentiKokaFleteKontabel != null ? true : false, isShitje, db, out shfaqmesazhapolupebanka, out shfaqmesazhapolupeVDK, DbCore.DbKontabiliteti.clsKlientFurnitor.MerrIdBanke(idKlientFurnitor, dbkont), clsKlientFurnitor.MerrLlojin(idKlientFurnitor, dbkont));
                }
                catch (MyException mine)
                {
                    ImbLogger.LogErrorShitje($"Exception:{mine}");
                    ImbLogger.Error(mine);
                    ImbLogger.LogTraceShitje($"Mbaroi clsMesazh krijoDokArkeBanke me parametra faturashitjengaurdhershitjamekupontatimor:{JsonConvert.SerializeObject(faturashitjengaurdhershitjamekupontatimor)}, isShitje:{isShitje}, int idPeriudha:{idPeriudha}");
                    return new clsMesazh(false, mine.Message);
                }
                catch (Exception e)
                {
                    ImbLogger.LogErrorShitje($"Exception:{e}");
                    ImbLogger.Error(e);
                    ImbLogger.LogTraceShitje($"Mbaroi clsMesazh krijoDokArkeBanke me parametra faturashitjengaurdhershitjamekupontatimor:{JsonConvert.SerializeObject(faturashitjengaurdhershitjamekupontatimor)}, isShitje:{isShitje}, int idPeriudha:{idPeriudha}");
                    return new clsMesazh(false, MessagesResource.Messages["msgNdodhi1GabimGjateKrijimitTeVeprimitTeBankes"]);


                }
                u_ruajt = banka.ruaj(null, db, 0, StatusAprovimi.Undefined, 0, string.Empty, false);
                if (!u_ruajt.Status)
                {
                    ImbLogger.LogTraceShitje($"Mbaroi clsMesazh krijoDokArkeBanke me parametra faturashitjengaurdhershitjamekupontatimor:{JsonConvert.SerializeObject(faturashitjengaurdhershitjamekupontatimor)}, isShitje:{isShitje}, int idPeriudha:{idPeriudha}");
                    return new clsMesazh(false, "Dokumenti i bankës :" + u_ruajt.PershkrimMesazhi);
                }
            }
            ImbLogger.LogTraceShitje($"Mbaroi clsMesazh krijoDokArkeBanke me parametra faturashitjengaurdhershitjamekupontatimor:{JsonConvert.SerializeObject(faturashitjengaurdhershitjamekupontatimor)}, isShitje:{isShitje}, int idPeriudha:{idPeriudha}");
            return new clsMesazh(true);
        }

        public colDokumentLidhesTrupi krijoTrupDokumentLidhes(double kursi, int idklientfurnitor, int iddokkthimi, int idniveldokkthimi, int iddokshitje, int idniveldokshitje, double vlera, double kursfature, double vlerashitje)
        {
            ImbLogger.LogTraceShitje($"Filloi colDokumentLidhesTrupi krijoTrupDokumentLidhes me parametra kursi:{kursi}, idklientfurnitor:{idklientfurnitor}, iddokkthimi:{iddokkthimi}, idniveldokkthimi:{idniveldokkthimi}, iddokshitje:{iddokshitje}, idniveldokshitje:{idniveldokshitje}, vlera:{vlera}, kursfature:{kursfature}, vlerashitje:{vlerashitje}");
            colDokumentLidhesTrupi coltrup = new colDokumentLidhesTrupi();
            string llojivepkthimit = "1";
            clsDokumentLidhesTrupi dokLidhes = new clsDokumentLidhesTrupi();
            dokLidhes.IdDokumenti = iddokkthimi;
            dokLidhes.LlojDokumenti = idniveldokkthimi.ToString();

            dokLidhes.VleraLidhjes = Math.Min(Math.Abs(vlera), Math.Abs(vlerashitje));//meqe dokumenti i shitjes dhe i kthimit jane ne te njejten monedhe
            // Math.Min(Math.Abs(vlera * kursi), Math.Abs(vlerashitje * kursfature)) / kursi;//vlera ne monedhen e pageses
            dokLidhes.Statusi = llojivepkthimit;

            //dokumenti kryesor eshte fatura
            clsDokumentLidhesTrupi dokKryesor = new clsDokumentLidhesTrupi();
            dokKryesor.IdDokumenti = iddokshitje;

            dokKryesor.LlojDokumenti = idniveldokshitje.ToString();
            dokKryesor.Statusi = "0";
            dokKryesor.VleraLidhjes = dokLidhes.VleraLidhjes; ;// (vlera * kursi) / kursfature;//vlera ne monedhen e pageses ne fillim kthehet ne vlere ne monedhe baze pastaj ne vlere ne monedhen e fatures
            coltrup.Add(dokKryesor);

            //dokumenti lidhes eshte veprimi i bankes


            coltrup.Add(dokLidhes);
            ImbLogger.LogTraceShitje($"Mbaroi colDokumentLidhesTrupi krijoTrupDokumentLidhes me parametra kursi:{kursi}, idklientfurnitor:{idklientfurnitor}, iddokkthimi:{iddokkthimi}, idniveldokkthimi:{idniveldokkthimi}, iddokshitje:{iddokshitje}, idniveldokshitje:{idniveldokshitje}, vlera:{vlera}, kursfature:{kursfature}, vlerashitje:{vlerashitje}");
            return coltrup;
        }

        private clsMesazh kontrolloShitje(out bool kaNdryshimNrAuto, clsDatabaseRegjistrim dbRegj, IDictionary<string, object> hfregjistrime, bool modifikim, bool kontrollEkzistence)
        {
            ImbLogger.LogTraceShitje($"Filloi clsMesazh kontrolloShitje me parametra modifikim:{modifikim}, kontrollEkzistence:{kontrollEkzistence}");
            kaNdryshimNrAuto = false;
            double shumaPaKurs = Totali - Zbritje;
            
            if (nrDok == string.Empty)
            {
                ImbLogger.LogWarningShitje("Numri i dokumentit tek metoda kontrolloShitje eshte bosh!");
                return new clsMesazh(false, "Numri i dokumentit nuk mund të jetë bosh");
            }
            double totali = 0;

            clsDatabaseShare dbShare = new clsDatabaseShare(dbRegj);

            if (dtFillimi > dtMbarimi)
            {
                ImbLogger.LogWarningShitje($"Data e mbarimit duhet te jete me e madhe se data e fillimit, data mbarimit:{dtMbarimi}, data fillimit:{dtFillimi}");
                return new clsMesazh("Data e mbarimit duhet te jete me e madhe se data e fillimit!");
            }

            clsFormatiKonfig formatNrPerKonfig = new clsFormatiKonfig(idKonfigAmbjente, dbShare);

            ImbLogger.LogTraceShitje("Kontrolli i formatit te monedhes!");
            clsFormatKonfigTrup formatMonedhe = new clsFormatKonfigTrup();
            if (formatNrPerKonfig.IdFormatKonfig > 0)
                formatMonedhe = formatNrPerKonfig.KonfigTrupi.merrFormatSipasMonedhes(idMonedha);
            else
                formatMonedhe = new clsFormatKonfigTrup(clsFormatKonfigTrup.defaultFormatSasia, clsFormatKonfigTrup.defaultFormatCmimi, clsFormatKonfigTrup.defaultFormatVlefta, clsFormatKonfigTrup.defaultFormatZbritja, clsFormatKonfigTrup.defaultFormatStringSasia, clsFormatKonfigTrup.defaultFormatStringCmimi, clsFormatKonfigTrup.defaultFormatStringVlefta, clsFormatKonfigTrup.defaultFormatStringZbritja);

            bool aplikonrserialneruajte = clsAlternativaKushti.getAlternativa(IdKonfigAmbjente, "ANRSVSR", dbShare) == "Po";
            foreach (clsTrupiShitje t in OColTrupiShitje)
            {
                // if (t.IdLlojVeprimi == 1)
                //    shumaKomision += t.VleraKomisionit;
                if (t.Kodi != string.Empty && t.Pershkrimi == string.Empty)
                {
                    ImbLogger.LogTraceShitje("Ka rreshta pa pershkrim. --Msg i nxjerr ne metoden kontrolloShitje!");
                    return new clsMesazh("Ka rreshta pa pershkrim");
                }
                if (t.Kodi != string.Empty && t.IdLlojVeprimi == 1 && t.IdNjesia == 0)
                {
                    ImbLogger.LogTraceShitje("Ka artikuj pa njesi. --Msg i nxjerr ne metoden kontrolloShitje!");
                    return new clsMesazh("Ka artikuj pa njesi");
                }
                if (this.dogana && t.Tvsh != 0)
                {
                    ImbLogger.LogTraceShitje($"TVSH duhet te jete 0 pasi Dogana eshte Po.--Msg i nxjerr ne metoden kontrolloShitje!");
                    ImbLogger.LogTraceShitje($"Mbaroi clsMesazh kontrolloShitje me parametra modifikim:{modifikim}, kontrollEkzistence:{kontrollEkzistence}");
                    return new clsMesazh(false, "TVSH duhet te jete 0 pasi Dogana eshte Po");
                }

                if (t.IdMagazina > 0)
                {
                    clsNjesiAdministrative njesiadm = new clsNjesiAdministrative(t.IdMagazina);
                    if (njesiadm.Kodi != "" && !njesiadm.Aktiv)
                        return new clsMesazh(false, "Magazina me kod " + njesiadm.Kodi + " nuk eshte aktive!");
                }

                if (!string.IsNullOrEmpty(t.Kodi))
                {
                    decimal normePerqindje = 0;
                    if (t.Tvsh != 0)
                        normePerqindje = new clsTaksa(t.Tvsh, dbRegj).NormaPerqindje;

                    double maxSingleErr = double.Parse((clsFunksione.krijoNumer(formatMonedhe.ShifraPasPresjesVlefta, "0") + "5"), System.Globalization.CultureInfo.InvariantCulture);
                    double errorNivCmimPaTvsh = (t.Sasia + t.Cmimi + (1 - t.Zbritje / 100)) * (maxSingleErr);

                    double vleraLejueshmeGabimit = Math.Abs(maxSingleErr * (t.VleftaPaTvsh + 1 + double.Parse(normePerqindje.ToString()) / 100));

                    double vlefta = t.Sasia * t.Cmimi * (1 - t.Zbritje / 100);
                    bool pranohetPaTvsh = Math.Abs(maxSingleErr *(vlefta - t.VleftaPaTvsh)) <= Math.Abs(errorNivCmimPaTvsh);
                    if (!pranohetPaTvsh)    //nivel cmimi pa tvsh
                    {
                        //begin nivel cmimi me tvsh
                        double errorNivCmimMeTvsh = (t.Sasia + t.Cmimi + (1 - t.Zbritje / 100)) * (maxSingleErr);
                        bool pranohetMeTvsh = Math.Abs(vlefta - t.VleftaMeTvsh) <= Math.Abs(errorNivCmimMeTvsh);
                        if (pranohetMeTvsh)//nivel cmimi me tvsh
                        {
                            vleraLejueshmeGabimit = Math.Abs(maxSingleErr * (t.VleftaPaTvsh + 1 + double.Parse(normePerqindje.ToString()) / 100));
                            if ((Math.Abs(t.VleftaMeTvsh / (1 + double.Parse(normePerqindje.ToString()) / 100) - t.VleftaPaTvsh) > vleraLejueshmeGabimit))
                            {
                                ImbLogger.LogTraceShitje($"Mbaroi clsMesazh kontrolloShitje sepse Vlerat nuk janë të sakta");
                                return new clsMesazh("Vlerat nuk janë të sakta!");
                            }
                        }
                        //end nivel cmimi me tvsh
                        else
                            return new clsMesazh("Vlerat nuk janë të sakta!");
                    }
                    if (Math.Abs (maxSingleErr * (t.VleftaPaTvsh * (1 + double.Parse(normePerqindje.ToString()) / 100) - t.VleftaMeTvsh)) > vleraLejueshmeGabimit)
                    {
                        ImbLogger.LogTraceShitje($"Mbaroi clsMesazh kontrolloShitje sepse Vlerat nuk janë të sakta");
                        return new clsMesazh("Vlerat nuk janë të sakta!");
                    }
                    totali += t.VleftaMeTvsh;
                    #region komentuar
                    //if (Math.Round(t.Sasia * t.Cmimi * (1 - t.Zbritje / 100), 2) == t.VleftaPaTvsh)//nivel cmimi pa tvsh
                    //{
                    //    if (Math.Round(t.Sasia * t.Cmimi * (1 - t.Zbritje / 100) * (1 + double.Parse(taksa.NormaPerqindje.ToString()) / 100), 2) != t.VleftaMeTvsh)
                    //    {
                    //        return new clsMesazh("Te dhenat nuk jane te sakta");
                    //    }
                    //}
                    //else
                    //    if (Math.Round(t.Sasia * t.Cmimi * (1 - t.Zbritje / 100) / (1 + double.Parse(taksa.NormaPerqindje.ToString()) / 100), 2) == t.VleftaPaTvsh)//nivel cmimi me tvsh
                    //    {
                    //        if (Math.Round(t.Sasia * t.Cmimi * (1 - t.Zbritje / 100), 2) != t.VleftaMeTvsh)
                    //        {
                    //            return new clsMesazh("Te dhenat nuk jane te sakta");
                    //        }
                    //    }
                    //    else
                    //        return new clsMesazh("Te dhenat nuk jane te sakta");

                    //totali += t.VleftaMeTvsh;
                    #endregion
                }
            }
            int shifraPasPresjesVlefta = formatMonedhe.ShifraPasPresjesVlefta;
            if (Math.Round(totali, shifraPasPresjesVlefta) != Math.Round(this.Totali, shifraPasPresjesVlefta))
            {
                ImbLogger.LogTraceShitje($"Mbaroi clsMesazh kontrolloShitje sepse Totali i dokumentit nuk eshte i barabarte me shumen e rreshtave");
                return new clsMesazh("Totali i dokumentit nuk eshte i barabarte me shumen e rreshtave");
            }
            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin(dbRegj);
            if (kodAgjenti != string.Empty && kodAgjenti != null)
            {
                clsAgjentShitje agjentShitje = new clsAgjentShitje(kodAgjenti, idNdermarje, dbAdmin);
            }
            if (kodAgjenti2 != string.Empty && kodAgjenti2 != null)
            {
                clsAgjentShitje agjentShitje = new clsAgjentShitje(kodAgjenti2, idNdermarje, dbAdmin);
            }
            if (kodAgjenti3 != string.Empty && kodAgjenti3 != null)
            {
                clsAgjentShitje agjentShitje = new clsAgjentShitje(kodAgjenti3, idNdermarje, dbAdmin);
            }
            if (!string.IsNullOrEmpty(emertimTr) && !clsTransportues.ekzistonTransportues(new clsDatabaseInventari(dbRegj), emertimTr, idNdermarje))
            {
                ImbLogger.LogTraceShitje($"Mbaroi clsMesazh kontrolloShitje sepse Transportuesi nuk ekziston!");
                return new clsMesazh(false, "Transportuesi nuk ekziston!");
            }
            if (clsAlternativaKushti.getAlternativa(IdKonfigAmbjente, "VF_VM", dbShare) == "Po")// kontrolle per dokument marreveshje
            {
                var mesazh = KontrolloMarreveshje(dbRegj);
                if (!mesazh.Status)
                    return mesazh;
            }
            if (kontrollEkzistence)
            {

                clsMesazh mes = new clsMesazh();
                if (hfregjistrime != null)
                {
                    mes = modifikim ? kontrolloNrAutoShitjaMod(out kaNdryshimNrAuto, dbRegj, hfregjistrime, aplikonrserialneruajte, idStatusDok) : kontrolloNrAutoShitja(out kaNdryshimNrAuto, dbRegj, hfregjistrime, aplikonrserialneruajte, idStatusDok);
                    if (!mes.Status)
                    {
                        return mes;
                    }
                }
                if (!string.IsNullOrEmpty(this.NrSerial))
                {
                    var nivelRegjistrimi = new clsNivelRegjistrimi();
                    nivelRegjistrimi.mbushNivelRegjistrimiSipasIdPaKonvertime(this.idNivel, dbRegj);
                    if (nivelRegjistrimi.NrSerialUnik && dbRegj.ekzistonNumerSerialUnikPerKeteNivelDheNdermarrjeKokaShitje(modifikim ? this.IdShitjeKoka : 0, this.IdNivel, this.nrSerial, this.idNdermarje))
                    {
                        ImbLogger.LogTraceShitje($"Mbaroi clsMesazh kontrolloShitje sepse Ky numer serial dokumenti ekziston! Ju lutem vendosni nje numer tjeter!");
                        return new clsMesazh(false, "Ky numer serial dokumenti ekziston! Ju lutem vendosni nje numer tjeter!");
                    }
                }
                if (dbRegj.ekzistonRegjistrimShitjeSipasIdentifikuese(modifikim ? this.IdShitjeKoka : 0, idNivel, idKonfigAmbjente, dtDok, nrDok, idNdermarje, IdKlientFurnitor, nrSerial, idMenyrePagese, idPikeShitjeFurnizimi, idDegeAdministrative, idRaportDesing, idGrup1, idGrup2, idGrup3, idAgjent))
                {
                    ImbLogger.LogTraceShitje($"Mbaroi clsMesazh kontrolloShitje sepse Ekziston një regjistrim me këto të dhëna identifikuese");
                    return new clsMesazh(false, "Ekziston një regjistrim me këto të dhëna identifikuese!");
                }
                if (kaNdryshimNrAuto)
                {
                    return mes;
                }
            }
            ImbLogger.LogTraceShitje($"Mbaroi clsMesazh kontrolloShitje me parametra modifikim:{modifikim}, kontrollEkzistence:{kontrollEkzistence}");
            return new clsMesazh(true, "Kontrollet u kaluan me sukses");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kaNdryshimNumri">true Nese</param>
        /// <param name="dbRegj"></param>
        /// <param name="hfregjistrime"></param>
        /// <param name="modifikim"></param>
        /// <returns></returns>
        private clsMesazh kontrolloShitje(out bool kaNdryshimNrAuto, clsDatabaseRegjistrim dbRegj, IDictionary<string, object> hfregjistrime, bool modifikim, clsKokaShitje kokaEkzistuese)
        {
            ImbLogger.LogTraceShitje($"Filloi clsMesazh kontrolloShitje me parametra modifikim:{modifikim}, kokaEkzistuese:{kokaEkzistuese}");
            kaNdryshimNrAuto = false;
            if (kokaEkzistuese != null)
                if (string.IsNullOrEmpty(kokaEkzistuese.NrDok) || kokaEkzistuese.IdStatusDok == 2)
                {
                    ImbLogger.LogTraceShitje($"Mbaroi clsMesazh kontrolloShitje me parametra modifikim:{modifikim}, kokaEkzistuese:{kokaEkzistuese}");
                    return new clsMesazh(false, "Dokumenti ka ndryshuar! Ju lutem rihapeni përsëri!");
                }
            if (!string.IsNullOrEmpty(kokaEkzistuese.nrSerial) && kokaEkzistuese.nrSerial == this.nrSerial)
                hfregjistrime = null;
            ImbLogger.LogTraceShitje($"Mbaroi clsMesazh kontrolloShitje me parametra modifikim:{modifikim}, kokaEkzistuese:{kokaEkzistuese}");
            return kontrolloShitje(out kaNdryshimNrAuto, dbRegj, hfregjistrime, modifikim, true);
        }

        /// <summary>
        /// Kontrollon nese dokumenti qe po konvertohet ne dok shitje ka ndryshuar.
        /// </summary>
        /// <param name="idtrupikonvertimi"></param>
        /// <param name="idtrupirezervimi"></param>
        /// <returns></returns>
        public static clsMesazh kontrolloEkzistojneDokQePoKonvertohen(string idtrupikonvertimi, string idtrupimag, string idtrupirezervimi)
        {
            bool exists;
            using (var dbRegj = new clsDatabaseRegjistrim())
            {
                exists = dbRegj.kontrolloEkzistojneDokQePoKonvertohen(idtrupikonvertimi, idtrupimag, idtrupirezervimi);
            }

            return !exists
                ? new clsMesazh(false, "Dokumenti i konvertuar mund te jete modifikuar nga nje perdorues tjeter! Ju lutemi konvertojeni perseri !")
                : new clsMesazh(true, "Kontrolli u kalua me sukses");
        }

        private clsMesazh KontrolloMarreveshje(clsDatabaseRegjistrim dbRegj)
        {
            if (dtMbarimi < dtDok)
            {
                ImbLogger.LogWarningShitje($"Data e mbarimit duhet te jete me e madhe se data e dokumentit, data mbarimit:{dtMbarimi}, data dokumentit:{DtDok}");
                return new MesazhGabimi(MessagesResource.Messages["msgDtMbarimiMeEVogelDtDok"]);
            }
            double vlereBuxhetiPerdorur = dbRegj.MerrVlereBuxhetiTePerdorur(idMarreveshje);
            var gjendjabuxheti = Math.Round((totali - vlereBuxhetiPerdorur), 2);
            if (gjendjabuxheti < 0)
            {
                ImbLogger.LogWarningShitje($"Kujdes! Gjendja e buxhetit shkon {gjendjabuxheti}!");
                return new MesazhGabimi(MessagesResource.Messages["msgGjendjeBuxhetiNegative"].Replace("#xx", gjendjabuxheti.ToString()));
            }
            if (ColKlienteFurnitoreVartes.Count > 0)
            {
                string colkfvartes = string.Join(",", ColKlienteFurnitoreVartes.Select(x => x.KodKlientFurnitor));
                string klientMeMarreveshje = clsKlientFurnitor.KontrolloKlientMeMarreveshjeAktive(colkfvartes, dtDok, idMarreveshje);
                if (!String.IsNullOrEmpty(klientMeMarreveshje))
                    return new MesazhGabimi(MessagesResource.Messages["msgKlientiKaMarreveshjeAktive"].Replace("#XX", klientMeMarreveshje));
            }
            return new MesazhSuksesi();
        }

        public clsMesazh KtheMesazhStatusMarreveshje(string idMarreveshje, DateTime dtdok, bool ngarkoVlere)
        {
            if (this.StatusAprovimi == StatusAprovimi.Per_Aprovim || this.StatusAprovimi == StatusAprovimi.Deleguar || (this.StatusAprovimi == StatusAprovimi.Aprovuar && IdStatusDok == 0))
                return new MesazhGabimi(MessagesResource.Messages["msgIdMarreveshjeNeCikelAprovimi"]);
            if (this.StatusMarreveshje == StatusMarreveshje.Aktive)
            {
                if (this.DtMbarimi > dtdok)
                {
                    if (ngarkoVlere)
                        return new MesazhInformimi(MessagesResource.Messages["msgExistIDmarreveshjeAktive"].Replace("#xx", idMarreveshje));
                    else
                        return new MesazhInformimi();//shume e qarte qe ekziston, s'ka nevoje per mesazh sepse as nuk ngarkohet vlere e re buxheti
                }
                else
                    return new MesazhInformimi(MessagesResource.Messages["msgExistIDmarreveshjeSkaduar"].Replace("#xx", idMarreveshje));
            }
            else
                return new MesazhInformimi(MessagesResource.Messages["msgExistIDmarreveshjeInaktive"].Replace("#xx", idMarreveshje));
        }

        private clsMesazh kontrolloNrAutoShitja(out bool kaNdryshimNumri, clsDatabaseRegjistrim dbRegj, IDictionary<string, object> hfregjistrime, bool aplikonrserialneruajte, int idstatusdok)
        {
            ImbLogger.LogTraceShitje($"Filloi clsMesazh kontrolloNrAutoShitja me parametra aplikonrserialneruajte:{aplikonrserialneruajte}, idstatusdok:{idstatusdok}");
            clsDatabaseAdmin db = new clsDatabaseAdmin(dbRegj);
            List<NrAuto> list = clsNrAutom.kontrollogjithenumrat(db, hfregjistrime, this.dtDok);
            if (NrAuto.ktheVlerenEre(list, "NrDok") != string.Empty)
                this.nrDok = NrAuto.ktheVlerenEre(list, "NrDok");
            if (aplikonrserialneruajte && idstatusdok == 0)
            {
                this.NrSerial = "";
                list.FindAndRemove(x => x.kodKontrolli == "NrSerial");
            }

            else if (NrAuto.ktheVlerenEre(list, "NrSerial") != string.Empty)
                this.nrSerial = NrAuto.ktheVlerenEre(list, "NrSerial");
            if (NrAuto.ktheVlerenEre(list, "NrProjekt") != string.Empty)
                this.NrProjekt = NrAuto.ktheVlerenEre(list, "NrProjekt");
            if (NrAuto.ktheVlerenEre(list, "NrDokMagazine") != string.Empty)
                this.NrDokMagazine = NrAuto.ktheVlerenEre(list, "NrDokMagazine");
            DbCore.clsMesazh mes = NrAuto.ruajvlera(out kaNdryshimNumri, list, this.dtDok, this.idPerdoruesi, this.idNdermarje, db);
            ImbLogger.LogTraceShitje($"Mbaroi clsMesazh kontrolloNrAutoShitja me parametra aplikonrserialneruajte:{aplikonrserialneruajte}, idstatusdok:{idstatusdok}");
            return new clsMesazh(mes.Status, mes.PershkrimMesazhi);
        }

        private clsMesazh kontrolloNrAutoShitjaMod(out bool kaNdryshimNumri, clsDatabaseRegjistrim dbRegj, IDictionary<string, object> hfregjistrime, bool aplikonrserialneruajte, int idstatusdok)
        {
            ImbLogger.LogTraceShitje($"Filloi clsMesazh kontrolloNrAutoShitjaMod me parametra aplikonrserialneruajte:{aplikonrserialneruajte}, idstatusdok:{idstatusdok}");
            clsDatabaseAdmin db = new clsDatabaseAdmin(dbRegj);
            List<NrAuto> list = clsNrAutom.kontrollogjithenumrat(db, hfregjistrime, this.dtDok);
            if (aplikonrserialneruajte && idstatusdok == 0)
            {
                this.NrSerial = "";
                list.FindAndRemove(x => x.kodKontrolli == "NrSerial");
            }
            else if (NrAuto.ktheVlerenEre(list, "NrSerial") != string.Empty)
                this.nrSerial = NrAuto.ktheVlerenEre(list, "NrSerial");
            DbCore.clsMesazh mes = NrAuto.ruajvlera(out kaNdryshimNumri, list, this.dtDok, this.idPerdoruesi, this.idNdermarje, db);
            ImbLogger.LogTraceShitje($"Mbaroi clsMesazh kontrolloNrAutoShitjaMod me parametra aplikonrserialneruajte:{aplikonrserialneruajte}, idstatusdok:{idstatusdok}");
            return new clsMesazh(mes.Status, mes.PershkrimMesazhi);
        }

        /// <summary>
        /// Ruan nje objekt dokumenti shitje/blerje sebashku me trupin , dokumentin e magazines dhe kontabilitetin perkates
        /// Nje objekt koka dokumenti shitje ka nje koleksion me trupin e dokumentit , nje dokument magazine dhe kontabilitetin perkates , 
        /// ruajtja e nje dokumenti imponon ruajtjen edhe te nje colection-i me trupin, nje dokument magazine ne rastet kur dokumenti kryen veprime me magazinen dhe kontabilitetin kur kontabilizohet
        /// Mqs cdo rresht i ri qe shtohet ne DB kerkon thirrjen e nje SP-je me parametra dhe koka e dokumentit bashke me trupin, dokumentin e magazines dhe kontabilitetin konsiderohet si nje regjistrim,
        /// perdoret nje transaksion qe imponon regjistrimin e rregullt te nje te nje dokumenti shitje sebashku me trupin, dokumentin e magazines dhe kontabilitetin perkates
        /// </summary>
        /// <param name="serverUrl"></param>
        /// <param name="koka"> koka e dokumentit te shitjes qe do ruhet</param>
        /// <param name="isShitje"> tregon nese dokumenti eshte dokument shitje apo blerje</param>
        /// <param name="modifikim"> tregon nese dokumenti qe po ruhet eshte nga modifikimi</param>
        /// <example> true- shitje, false-blerje</example>
        /// <returns> kthen nje obj clsMesazh per te identifikuar statusin e ruajtjes se te dhenave ne DB</returns>
        /// <param name="skemaWorkFlow"></param>
        public clsMesazh ruajShitje(string serverUrl, clsKokaShitje koka, bool isShitje, bool modifikim, int idPeriudha, clsDatabaseRegjistrim dbRegj, colKonvertimi colkonvertimi, bool gjenerodokmagazine, int skemaWorkFlow, StatusAprovimi statusapp, int idetapa, out string shfaqmesazhapolupemag, DbQendraKosto.colTrupiQendraKosto colTrupiQendramagvejter, bool kontrollodisponibel, bool eshteOwn, colSerialetMagazine serialet, clsKonfigurimAmbjenti konfigurimAmortizimi, colAmortizimiKoka colAmortizimetEVjetra, bool mekontabilizim, DbQendraKosto.colTrupiQendraKosto trupivjeterqendra, out string shfaqmesazhapolupe, string kodkonfigurimi, int iddokngafk, int iddokngaqk, int iddokshitje, int idGjuha, int iddokngalidhes, bool tollona, bool zevendesim, bool tollonakastrati, bool tollonakastratielektronik, bool zevendesimtollonakastrati, bool ruajrenditje, bool kontrolloSasiKonvertimiDheKthimi, bool kaveprimepas, bool kontrolloIMEIFifo, bool blerjedealer, bool promocione, out string mesazhmevonshem, bool modifikimAprovimi,clsKokaShitje kokaEkz)
        {//transaksioni per te ruajtur 
            ImbLogger.LogTraceShitje($"Filloi metoda ruajShitje me parametra serverUrl:" + serverUrl + $", koka:{JsonConvert.SerializeObject(koka)} isShitje:{isShitje}, modifikim:{modifikim} idPeriudha:{idPeriudha}, colkonvertimi:{JsonConvert.SerializeObject(colkonvertimi)}, gjenerodokmagazine:{gjenerodokmagazine}, skemaWorkFlow:{skemaWorkFlow}, idetapa:{idetapa}, kontrollodisponibel:{kontrollodisponibel}, eshteOwn:{eshteOwn}, serialet:{JsonConvert.SerializeObject(serialet)}, konfiguriimAmortizimi:{JsonConvert.SerializeObject(konfigurimAmortizimi)}, colAmortizimetEVjetra:{JsonConvert.SerializeObject(colAmortizimetEVjetra)}, mekontabilizim:{mekontabilizim}, kodkonfigurimi" + kodkonfigurimi + $", iddokngafk:{iddokngafk}, iddokngaqk:{iddokngaqk}, iddokshitje:{iddokshitje}, idGjuha:{idGjuha}, iddokngalidhes:{iddokngalidhes}, tollona:{tollona}, zevendesim:{zevendesim}, tollonakastrati:{tollonakastrati}, tollonakastartielektronik:{tollonakastratielektronik}, zevendesimtollonakastrati:{zevendesimtollonakastrati}, ruajrenditje:{ruajrenditje}, kontrolloSasiKonvertimiDheKthimi:{kontrolloSasiKonvertimiDheKthimi}, kaveprimepas:{kaveprimepas}, kontrolloIMEIFifo:{kontrolloIMEIFifo}, blerjedealer:{blerjedealer}, promocione:{promocione}");
            shfaqmesazhapolupemag = "jo";
            shfaqmesazhapolupe = "jo";
            mesazhmevonshem = "";

            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgDokumentiURuajtMeSukese"]);
            var kategoria = isShitje ? 1 : 2;
            clsMesazh kontMesazh = new clsMesazh();
            clsDatabaseKontabilitet dbKont = new clsDatabaseKontabilitet(dbRegj);
            clsDatabaseShare dbshare = new clsDatabaseShare(dbRegj);
            clsDatabazeTollona dbtollon = new clsDatabazeTollona(dbRegj);
            clsDatabaseInventari dbinv = new clsDatabaseInventari(dbRegj);

            #region koka
            bool klientFiskalizimi = false;
            if (clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizim())
                klientFiskalizimi = true;
            koka.IdShitjeKoka = dbRegj.ruajKokaShitje(koka.IdShitjeKoka, koka.IdNivel, koka.IdTemplate, koka.IdKonfigAmbjente, koka.IdKlientFurnitor, koka.IdProjekt, koka.NrProjekt, koka.DtDok, koka.NrDok, koka.NrSerial, koka.DtMaturimi, koka.IdMonedha, koka.Kursi, koka.IdMenyreTransporti, koka.DtTransportimi, koka.IdKushtDergimi, koka.IdAgjent, koka.IdMenyrePagese, koka.IdKushtPagese, koka.Zbritje, koka.Totali, koka.Tvsh, koka.DtRegjistrimi, koka.IdStatusDok, koka.IdNdermarrje, koka.IdNdermarrjeVit, koka.IdNivelGjenerues, koka.IdKonfigGjenerues, koka.IdGjenerues, koka.IdDokNga, koka.adresaFaturimit, koka.AdresaDergimit, koka.Pershkrimi, koka.Dogana, koka.idDegeAdministrative, koka.idPikeShitjeFurnizimi, koka.idPerdoruesi, koka.idRaportDesing, koka.IdGrup1, koka.IdGrup2, koka.IdGrup3, koka.AfatKohor, koka.Cash, koka.StatusAprovimi, koka.idKrijuesi, koka.PerqindjeAgjenti, koka.idTransferimi, koka.idKonfigTransferimi, koka.statusTransferimi, koka.emerKlienti, koka.kontakti, koka.kase, koka.kupon, koka.DtFillimi, koka.DtMbarimi, koka.idAutomjet, koka.kilometraAuto, koka.idAgjenti2, koka.perqindjeAgjenti2, koka.idAgjenti3, koka.perqindjeAgjenti3, koka.marresi, koka.idTransportues, koka.faturePermbledhese, koka.ShpenzimeJoTeZbritshme, koka.idarka, koka.DtFature, koka.Gjeneruar, koka.idKarta, koka.pike, koka.MuajRaportimi, koka.IdVitRaportimi, koka.idFaza, koka.shoferi, koka.targaSHF, koka.zbritjeNeVlere, koka.perqindjeZbritje, koka.dtKrijimiPajisje, koka.koordinata, koka.niptKlienti, koka.qytetiK, koka.idKategoriSeriali, koka.shenime2, koka.kartaPaPagese, koka.idDokTransferimNga, koka.idLlojMarreveshje, koka.idMarreveshje, koka.StatusMarreveshje, koka.kerkuarNga, koka.dateKerkese, koka.NrDokMagazine, koka.iic, koka.nivf, koka.IdOperator,koka.NivfKthim,koka.EIC,koka.EInvoiceType, koka.Procesi,koka.EinStatus, klientFiskalizimi, koka.TipiIVetefaturimit, clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizimV3());
           
            if (koka.IdShitjeKoka == 0)
                return new clsMesazh(false, "Koka e shitjes nuk u ruajt");

            if (koka.ColKlienteFurnitoreVartes.Count != 0)
                dbRegj.RuajKlienteFurnitoreVartes(KrijoDataTableKlientFurnitorVartes(koka.IdShitjeKoka, koka.ColKlienteFurnitoreVartes));

            if (modifikim)
            {
                mesazh = dbRegj.modifikoIdDokNgaSipasIdKokaShitje(koka.IdDokNga, koka.IdShitjeKoka, koka.idNivel);
                if (!mesazh.Status)
                    return mesazh;

                dbRegj.FshiKlienteFurnitoreVartes(koka.idDokNga);
            }

            #endregion

            #region konvertimet

            foreach (clsKonvertimi konv in colkonvertimi)
            {
                if (ktheIdStatusDok(konv.IdDokKonvertuar) == 8)
                {
                    clsMesazh m = modifikoStatusDokumenti(konv.IdDokKonvertuar, 1);
                    if (!m.Status)
                    {
                        return m;
                    }
                }
            }

            #endregion

            int count = 0;
            int nrreshti = 0;
            ArrayList cmimetolloni = new ArrayList();
            ArrayList idarttolloni = new ArrayList();

            #region trupi

            var merrMagazinePerberesi = clsAlternativaKushti.getAlternativa(koka.IdKonfigAmbjente, "KGJAPMR", dbshare) == "Po";
            var rezervimNeOwn = clsAlternativaKushti.getAlternativa(idKonfigAmbjente, "GJDRONSH", dbshare) == "Po" && idStatusDok == 1;

            string idtrupashkonvertime = string.Join(",", koka.OColTrupiShitje.Where(x => x.IdTrupiKonvertimi > 0).Select(x => x.IdTrupiKonvertimi).Distinct());
            if (idtrupashkonvertime != "")
                idtrupashkonvertime += ",";// nese ka dhe IdTrupiKonvertimi dhe IdTrupiKonvertimBlerje i duhet "," perpara, psh kur konvertohet dy here
            idtrupashkonvertime += string.Join(",", koka.OColTrupiShitje.Where(x => x.IdTrupiKonvertimBlerje > 0).Select(x => x.IdTrupiKonvertimBlerje).Distinct());
            string idtrupashkonvertimerezervimi = string.Join(",", koka.OColTrupiShitje.Where(x => x.IdTrupiRezervimi > 0).Select(x => x.IdTrupiRezervimi).Distinct());
            if (!modifikim && !(string.IsNullOrEmpty(idtrupashkonvertime) && string.IsNullOrEmpty(idtrupashkonvertimerezervimi)))
            {
                mesazh = kontrolloEkzistojneDokQePoKonvertohen(idtrupashkonvertime, "", idtrupashkonvertimerezervimi);
                if (!mesazh.Status)
                    return mesazh;
            }
            foreach (clsTrupiShitje o in koka.OColTrupiShitje)
            {//behet ruajtja e trupit te shitjes                    
                int idT;
                o.IdShitjeKoka = koka.IdShitjeKoka;
                if (iddokshitje == 0 && o.IdTrupiKthim > 0)
                    iddokshitje = clsTrupiShitje.ktheIdKoka(o.IdTrupiKthim);

                if (o.IdLlojVeprimi != 0)
                {
                    if (isShitje && kontrollodisponibel && idStatusDok == 1 && o.IdLlojVeprimi == 1)
                    {
                        clsArtikulli artikulli = (clsArtikulli)o.Element;
                        if (artikulli.Klasa != 3 && artikulli.Klasa != 2 && artikulli.KontrollGjendjeArtikulli)
                        {
                            if (artikulli.Klasa != 4)
                            {
                                mesazh = KontrolloDisponibilitet(koka, o, artikulli, dbRegj, o.IdKodi, o.IdMagazina, o.IdDetajimArt, artikulli, 1, o.IdMagazina, o.IdKodi);
                                if (!mesazh)
                                    return mesazh;
                            }
                            else
                            {
                                colArtikulliPerberes artper = new colArtikulliPerberes(artikulli.IdArtikulli, koka.DtDok, dbinv);
                                var njesi = new clsNjesiArtikulli(artikulli.Njesi1Artikulli, dbinv);
                                for (int j = 0, artPerCount = artper.Count; j < artPerCount; j++)
                                {
                                    DbInventari.clsArtikulliPerberes aper = artper[j];
                                    clsArtikulli perberes = new clsArtikulli(aper.IdLidheseArt, dbinv);
                                    if (aper.Lloji == 1)    //artikull
                                    {//merr magazinen sipas kushtit
                                        mesazh = KontrolloDisponibilitet(koka, o, artikulli, dbRegj, perberes.IdArtikulli, merrMagazinePerberesi ? perberes.IdArtikulli : o.IdMagazina, 0, perberes, Convert.ToDouble(artper[j].Koeficienti), o.IdMagazina, o.IdKodi);
                                        if (!mesazh)
                                            return mesazh;
                                    }
                                }
                            }
                        }
                    }
                    if (kontrolloSasiKonvertimiDheKthimi && !gjenerodokmagazine)
                    {
                        if (iddokshitje > 0) //eshte kthim
                        {
                            double sasiaMbeturArt = dbRegj.ktheSasineKonvertuarDheKthyer(o.IdTrupiKthim, idNdermarje);
                            if (Math.Abs(o.Sasia) > sasiaMbeturArt)
                                return new clsMesazh(false, String.Format(MessagesResource.Messages["msgSasiMbeturKthimShitje"], o.Sasimbetur, o.Kodi));
                        }
                    }
                    double sasiaMbeturKokaEkz = 0;
                    if(kokaEkz.OColTrupiShitje != null)
                    {
                        foreach (var value in kokaEkz.OColTrupiShitje)
                        {
                            if (value.IdShitjeTrupi == o.IdShitjeTrupi)
                                sasiaMbeturKokaEkz = value.Sasia;
                        }
                    }
                   
                    if (o.IdTrupiKthim > 0 && clsAlternativaKushti.getAlternativa(idKonfigAmbjente, "LSPK", dbshare) == "Jo")
                    {
                        if (o.Sasia > 0)
                            return new clsMesazh(false, MessagesResource.Messages["msgSasiPozitiveKthimShitje"]);
                        if (o.Sasia < o.Sasimbetur + sasiaMbeturKokaEkz)
                            return new clsMesazh(false, String.Format(MessagesResource.Messages["msgSasiMbeturKthimShitje"], o.Sasimbetur, o.Kodi));
                    }

                    mesazh = dbRegj.ruajTrupiShitje(out idT, o.IdShitjeKoka, o.IdLlojVeprimi, o.Kodi, o.Pershkrimi, o.IdDetajimArt, o.IdNjesia, o.Sasia, o.Cmimi, o.Zbritje, o.VleftaMeTvsh, o.Tvsh, o.VleftaPaTvsh, o.IdMagazina, o.IdKodi, o.IdDetajimArt2, o.Gjeresi, o.Gjatesi, o.SasiPermasa, o.Shenime, o.DtFillimi, o.DtMbarimi, o.IdTrupiKonvertimi, o.SasiRez, o.IdTrupiRezervimi, o.IdTrupiTransferimi, o.IdLlogShpenzimi, o.IdTrupiKthim, o.IdTrupiKonvertimBlerje, o.Shenime2, o.LlojZbritje, o.ZbritjeVlere, o.IdKategoriShpenzimi, o.IdBarkodi, o.IdTrupiTransferimNga, o.VleraKomisionit, o.MeKomision); //todo Getson - ruaj gjithe trupat nje heresh

                    if (!mesazh.Status)
                        return mesazh;

                    if (modifikim)
                    {
                        mesazh = dbRegj.modifikoIdTrupiKonvertimiTransferimi(o.IdShitjeTrupi, idT);
                        if (!mesazh.Status)
                            return mesazh;
                    }

                    if (gjenerodokmagazine && koka.oKokaMagazina.OcolTrupiMagazina.Count > 0)
                    {
                        List<clsTrupiMagazina> coltrupimag = koka.oKokaMagazina.OcolTrupiMagazina.FindAll(x => x.RreshtiShitjes == nrreshti);
                        foreach (clsTrupiMagazina tm in coltrupimag)
                            tm.IdTrupiShitjeGjenerimi = idT;
                    }
                    #region tollona
                    #region gulf
                    if (tollona && isShitje && o.IdLlojVeprimi == 1 && idStatusDok == 1)
                    {
                        int idarttollon = 0;
                        clsArtikulli artikulli = (clsArtikulli)(o.Element);
                        if (!dbtollon.ekzistonArtikulli(o.IdKodi))//nqs artikulli nuk ekziston tek tabela e tollonave e shtojme
                        {
                            string kodartikulliperberes = "";
                            decimal koeficientartikulliperberes = 1;

                            colArtikulliPerberes colartber = new colArtikulliPerberes();
                            colartber.ktheArtikujPerberesSipasIdArtikullitKryesorDheDatesMeTeAfert(artikulli.IdArtikulli, koka.dtDok, dbinv);
                            if (colartber.Count > 0)
                            {
                                clsArtikulli artiper = new clsArtikulli(colartber[0].IdLidheseArt, dbinv);
                                kodartikulliperberes = artiper.KodArtikulli;
                                koeficientartikulliperberes = colartber[0].Koeficienti;
                            }
                            mesazh = dbtollon.ruajArtTollon(out idarttollon, artikulli.KodArtikulli, artikulli.PershkrimArtikulli, artikulli.IdArtikulli, artikulli.Aktiv, artikulli.IdStatusDok, kodartikulliperberes, koeficientartikulliperberes);

                            if (!mesazh.Status)
                            {
                                return mesazh;
                            }
                        }
                        else idarttollon = dbtollon.ktheIdArtikulliTollonaSipasIdArtWeb(o.IdKodi);
                        string serialitollon = o.Shenime;
                        if (o.Sasia < 0)
                        {
                            for (int i = 0; i > o.Sasia * Convert.ToDouble((o.IdNjesia == artikulli.Njesi1Artikulli) ? 1 : artikulli.KoeficientArtikulli); i--)
                            {
                                if (!dbtollon.ekzistonSeriali(serialitollon))
                                {
                                    return new clsMesazh(false, "Nuk ekziston seriali " + serialitollon);
                                }

                                DbTollona.clsShitjeMeSerial shitjeserial = new DbTollona.clsShitjeMeSerial(idarttollon, serialitollon, dbtollon);
                                if (i == 0)
                                {///ruajme cmimet dhe id e artikujve per ti marre artikullit te ri qe e zevendeson
                                    idarttolloni.Add(idarttollon);
                                    cmimetolloni.Add(shitjeserial.Cmimi);

                                }
                                if (dtDok < shitjeserial.DtFillimi)
                                    return new clsMesazh(false, "Seriali " + serialitollon + " nuk eshte aktiv!");
                                if (dtDok > shitjeserial.DtMbarrimi)
                                    return new clsMesazh(false, "Seriali " + serialitollon + " ka skaduar!");
                                if (shitjeserial.IdStatusDok == 5)//konsumuar
                                    return new clsMesazh(false, "Seriali " + serialitollon + "  eshte konsumuar!");
                                mesazh = dbtollon.ndryshoStatusSeriali(shitjeserial.Id, 2);//e fshijme serialin me -

                                if (!mesazh.Status)
                                {
                                    return mesazh;
                                }

                                serialitollon = Convert.ToString(long.Parse("1" + serialitollon) + 1);
                                serialitollon = serialitollon.Substring(1, serialitollon.Length - 1);

                            }
                        }
                        else
                        {
                            for (int i = 0; i < o.Sasia * Convert.ToDouble((o.IdNjesia == artikulli.Njesi1Artikulli) ? 1 : artikulli.KoeficientArtikulli); i++)
                            {
                                double cmimi = o.Cmimi / Convert.ToDouble((o.IdNjesia == artikulli.Njesi1Artikulli) ? 1 : artikulli.KoeficientArtikulli);
                                if (!dbtollon.ekzistonSeriali(serialitollon))
                                {
                                    if (zevendesim)
                                    {
                                        if (idarttolloni.Contains(idarttollon))
                                        {
                                            int index = idarttolloni.IndexOf(idarttollon);

                                            cmimi = Convert.ToDouble(cmimetolloni[index]);
                                        }
                                    }
                                    int idshitjetollon = 0;

                                    mesazh = dbtollon.ruajShitjeMeSerial(out idshitjetollon, serialitollon, 1, cmimi, idT, o.DtFillimi, o.DtMbarimi, idarttollon, 1);
                                    if (!mesazh.Status)
                                    {
                                        return mesazh;
                                    }
                                    serialitollon = Convert.ToString(long.Parse("1" + serialitollon) + 1);
                                    serialitollon = serialitollon.Substring(1, serialitollon.Length - 1);
                                }
                                else return new clsMesazh(false, "Ekziston seriali " + serialitollon + " per artikullin " + o.Kodi);
                            }
                        }

                    }
                    #endregion
                    #region kastrati leter
                    if (tollonakastrati && isShitje && o.IdLlojVeprimi == 1 && idStatusDok == 1)
                    {
                        int idarttollon = 0;
                        string kodartikulliperberes = "";
                        decimal koeficientartikulliperberes = 1;
                        clsArtikulli artikulli = (clsArtikulli)(o.Element);
                        if (!dbtollon.ekzistonArtikulli(o.IdKodi))//nqs artikulli nuk ekziston tek tabela e tollonave e shtojme
                        {


                            colArtikulliPerberes colartber = new colArtikulliPerberes();
                            colartber.ktheArtikujPerberesSipasIdArtikullitKryesorDheDatesMeTeAfert(artikulli.IdArtikulli, koka.dtDok, dbinv);
                            if (colartber.Count > 0)
                            {
                                clsArtikulli artiper = new clsArtikulli(colartber[0].IdLidheseArt, dbinv);
                                kodartikulliperberes = artiper.KodArtikulli;
                                koeficientartikulliperberes = colartber[0].Koeficienti;
                            }

                            mesazh = dbtollon.ruajArtTollon(out idarttollon, artikulli.KodArtikulli, artikulli.PershkrimArtikulli, artikulli.IdArtikulli, artikulli.Aktiv, artikulli.IdStatusDok, kodartikulliperberes, koeficientartikulliperberes);

                            if (!mesazh.Status)
                            {
                                return mesazh;
                            }
                        }
                        else idarttollon = dbtollon.ktheIdArtikulliTollonaSipasIdArtWeb(o.IdKodi);
                        string serialitollon = o.Shenime;
                        clsKlientFurnitor kl = new clsKlientFurnitor(koka.idKlientFurnitor, dbKont);
                        if (o.Sasia < 0)
                        {
                            //colArtikulliPerberes colartber = new colArtikulliPerberes();
                            //colartber.merrSipasIdArtikullKryesore(artikulli.IdArtikulli, dbinv);

                            clsNdermarrje nderm = new clsNdermarrje(koka.idNdermarje, new clsDatabaseAdmin(dbRegj));
                            //string mbarimtollon = Convert.ToString(int.Parse("1" + serialitollon) + (o.Sasia * Convert.ToDouble(artikulli.KoeficientArtikulli) - 1));
                            //mbarimtollon = mbarimtollon.Substring(1, mbarimtollon.Length - 1);
                            for (int i = 0; i > o.Sasia * Convert.ToDouble((o.IdNjesia == artikulli.Njesi1Artikulli) ? 1 : artikulli.KoeficientArtikulli); i--)
                            {
                                if (!dbtollon.ekzistonSerialiKastrati(serialitollon, serialitollon))
                                {
                                    return new clsMesazh(false, "Nuk ekziston seriali " + serialitollon);
                                }


                                if (dbtollon.eshteKonsumuarSerialiLeter(serialitollon))//konsumuar
                                    return new clsMesazh(false, "Seriali " + serialitollon + "  eshte konsumuar!");
                                int id = 0;

                                mesazh = dbtollon.ruajTollonaLeterKonsumimi(out id, serialitollon, 2, koka.dtDok, nderm.NdermarrjeKodi, nderm.NdermarrjePershkrimi, 1 * Convert.ToDouble(koeficientartikulliperberes), o.Cmimi / Convert.ToDouble(koeficientartikulliperberes), o.Cmimi, kl.KodKlientFurnitor, kl.EmertimiKF, true);//ruajme serialin e zevendsuar tek tabela e konsumimeve

                                if (!mesazh.Status)
                                {
                                    return mesazh;
                                }

                                serialitollon = serialitollon.Substring(0, 10) + Convert.ToString(long.Parse("1" + serialitollon.Substring(10, 9)) + 1);
                                serialitollon = serialitollon.Substring(0, 10) + serialitollon.Substring(11, 9);

                            }
                        }
                        else
                        {

                            string mbarimtollon = serialitollon.Substring(0, 10) + Convert.ToString(long.Parse("1" + serialitollon.Substring(10, 9)) + (o.Sasia * Convert.ToDouble(artikulli.KoeficientArtikulli) - 1));
                            mbarimtollon = mbarimtollon.Substring(0, 10) + mbarimtollon.Substring(11, 9);
                            colArtikulliPerberes colartber = new colArtikulliPerberes();
                            colartber.ktheArtikujPerberesSipasIdArtikullitKryesorDheDatesMeTeAfert(artikulli.IdArtikulli, koka.dtDok, dbinv);
                            if (colartber.Count > 0)
                            {
                                // clsArtikulli artiper = new clsArtikulli(colartber[0].IdArtikulliPerberes);
                                if (!dbtollon.ekzistonSerialiKastrati(serialitollon, mbarimtollon))
                                {
                                    int idshitjetollon = 0;
                                    mesazh = dbtollon.ruajTollonaLeter(out idshitjetollon, serialitollon, o.Sasia * Convert.ToDouble(artikulli.KoeficientArtikulli) * Convert.ToDouble(colartber[0].Koeficienti), mbarimtollon, idT, kl.KodKlientFurnitor, kl.EmertimiKF, idarttollon, false);
                                    if (!mesazh.Status)
                                    {
                                        return mesazh;
                                    }
                                }
                                else return new clsMesazh(false, "Ekzistonje seriale  midis " + serialitollon + " dhe " + mbarimtollon + " per artikullin " + o.Kodi);

                            }
                        }

                    }
                    #endregion
                    #region kastrati elektronik
                    if (tollonakastratielektronik && isShitje && o.IdLlojVeprimi == 1 && idStatusDok == 1)
                    {
                        int idarttollon = 0;
                        clsArtikulli artikulli = (clsArtikulli)(o.Element);
                        if (!dbtollon.ekzistonArtikulli(o.IdKodi))//nqs artikulli nuk ekziston tek tabela e tollonave e shtojme
                        {


                            mesazh = dbtollon.ruajArtTollon(out idarttollon, artikulli.KodArtikulli, artikulli.PershkrimArtikulli, artikulli.IdArtikulli, artikulli.Aktiv, artikulli.IdStatusDok, "", 1);

                            if (!mesazh.Status)
                            {
                                return mesazh;
                            }
                        }
                        else idarttollon = dbtollon.ktheIdArtikulliTollonaSipasIdArtWeb(o.IdKodi);
                        clsKlientFurnitor kl = new clsKlientFurnitor(koka.idKlientFurnitor, dbKont);
                        int idshitjetollon = 0;
                        mesazh = dbtollon.ruajTollonaElektronik(out idshitjetollon, artikulli.KodArtikulli, o.Sasia * Convert.ToDouble(artikulli.KoeficientArtikulli), idT, kl.KodKlientFurnitor, kl.EmertimiKF, idarttollon, false);
                        if (!mesazh.Status)
                        {
                            return mesazh;
                        }
                    }
                    #endregion
                    #endregion
                    if (koka.oKokaRezervime != null)
                    {
                        var artikull = ((clsArtikulli)o.Element);
                        if (rezervimNeOwn)
                            artikull = new clsArtikulli(artikull.KodArtikulli, clsNdermarrje.merrIdNdermarrjeOwn(), dbinv);

                        if (artikull.Klasa != 4)
                        {
                            VendosIdTrupiNgaVjenNeTrupRezervimi(artikull, koka, ref count, o, idT);
                        }
                        else
                        {
                            var artper = new colArtikulliPerberes(artikull.IdArtikulli, koka.DtDok, dbinv);
                            for (int j = 0, artPerCount = artper.Count; j < artPerCount; j++)
                            {
                                var perberes = new clsArtikulli(artper[j].IdLidheseArt, dbinv);
                                VendosIdTrupiNgaVjenNeTrupRezervimi(perberes, koka, ref count, o, idT);
                            }

                        }

                    }


                    o.IdShitjeTrupi = idT;
                    nrreshti++;
                }
            }
            #endregion

            bool llogaritfitimhumbjeneto = false;
            clsKusht kusht = new clsKusht(koka.idKonfigAmbjente, "LLFHN", dbshare);
            clsAlternativaKushti altllog = new clsAlternativaKushti(kusht.Vlera, dbshare);
            if (altllog.Alternativa == "Po")
                llogaritfitimhumbjeneto = true;
            colAmortizimiKoka col = new colAmortizimiKoka();

            #region magazina
            //warning

            if (gjenerodokmagazine)
            {
                int idDokNgaFK;
                if (koka.OKokaMagazina.OFleteKontabel.IdDokNga == 0)
                    idDokNgaFK = -1;
                else
                    idDokNgaFK = koka.OKokaMagazina.OFleteKontabel.IdDokNga;

                koka.OKokaMagazina.NrDok = koka.NrDokMagazine;
                koka.OKokaMagazina.IdGjenerues = koka.IdShitjeKoka;
                koka.OKokaMagazina.NrProjekt = koka.NrProjekt;
                if (koka.OKokaMagazina.IdKokaMagazina == 0)
                {
                    koka.OKokaMagazina.IdKrijuesi = koka.IdKrijuesi;
                    koka.OKokaMagazina.IdPerdoruesi = koka.IdPerdoruesi;
                }

                //KEVI testim
                string pershkrimMagFK;
                if (koka.OKokaMagazina.Shenime != String.Empty)
                {
                    pershkrimMagFK = koka.OKokaMagazina.Shenime;
                }
                else
                    if (isShitje) //shitje
                    pershkrimMagFK = clsKokaMagazina.pershkrimDokKontabilitetDaljeMag;
                else
                    pershkrimMagFK = clsKokaMagazina.pershkrimDokKontabilitetHyrjeMag;
                int idLlojDok;
                if (isShitje)
                    idLlojDok = 52; //shitje
                else
                    idLlojDok = 51;//blerje            
                bool gjithmone = false;
                if (clsAlternativaKushti.getAlternativa(koka.oKokaMagazina.IdKonfigAmbjente, "GJKGJ", dbshare) == "Po")
                    gjithmone = true;
                int kontabalizimmag = 1;
                if (llogaritfitimhumbjeneto)
                    kontabalizimmag = 0;

                bool ekziston = dbRegj.ktheKokaShitjeEkzistonDoksipasID(koka.idShitjeKoka);
                if (!ekziston)
                {
                    return new clsMesazh(false, "Nuk ekziston dokumenti i shitje/blerjes!");
                }
                colTrupiMagazina coltrupivjetermagKryesor = new colTrupiMagazina();
                coltrupivjetermagKryesor.mbushGjitheTrupiMagazinaNgaKoka(koka.oKokaMagazina.IdDokNga, dbRegj);
                mesazh = koka.OKokaMagazina.ruaj(false, kontabalizimmag, null, idPeriudha, pershkrimMagFK, idLlojDok, idDokNgaFK, dbRegj, out shfaqmesazhapolupemag, koka.oKokaMagazina.OFleteKontabel.KokaQendraKosto.IdDokNga, colTrupiQendramagvejter, eshteOwn, serialet, new colSerialetMagazine(), konfigurimAmortizimi, new clsKonfigurimAmbjenti(), 0, 0, new DbQendraKosto.colTrupiQendraKosto(), isShitje, koka, false, gjithmone, col, mekontabilizim ? 1 : 0, ruajrenditje, modifikim, coltrupivjetermagKryesor, colAmortizimetEVjetra, new int[0], kontrolloIMEIFifo, blerjedealer, promocione, out mesazhmevonshem, null, false, false);
                //mesazh = ruajMagazinePerShitje(koka.OKokaMagazina,false, shitje_blerje, koka.IdKonfigAmbjente, koka,idPeriudha, dbRegj);
                if (!mesazh.Status)
                    return mesazh;
                #region amortizimi

                if (!isShitje && serialet.Count > 0)
                {
                    clsDatabazeAsete dbasete = new clsDatabazeAsete(dbRegj);
                    clsSerialetMagazine clsSerialMag = new clsSerialetMagazine();
                    DataTable table = clsSerialMag.ktheNeDataTableCol(serialet, koka.OKokaMagazina.IdKokaMagazina);
                    mesazh = dbasete.RuajSerialeLidhjeMag(table, modifikim);
                    string shfaqmesazhapolupeamortizim = "jo";

                    if (!col.krijoAmortizimeKokaNgaBlerja(serialet, koka, konfigurimAmortizimi, colAmortizimetEVjetra, dbasete))
                        return new clsMesazh(false, "Nuk ekziston rregull amortizimi per ndonje nga standardet!");//Nje gabim ndodhi gjate krijimit te amortizimit te blerjes! 
                    mesazh = col.ruajListAmortizime(koka.idShitjeKoka, 0, out shfaqmesazhapolupeamortizim, colAmortizimetEVjetra, idPeriudha, 86, new colSerialetMagazine(), false, null, modifikim, out mesazhmevonshem);

                    if (!mesazh.Status)
                    {
                        return mesazh;
                    }

                }
                #endregion
            }
            #endregion

            #region kontabilizimi
            if (mekontabilizim)
            {
                List<int> emratkf;
                List<string> rreshtakf;
                colTrupatFletetKontabel trupatperGjendjekf;
                DbQendraKosto.colObjektivaKosto objektivat;
                List<double> vleratobjektiva; List<double> vleratobjektivamonbaze;
                List<int> idllogobj;
                string pershk;
                if (this.pershkrimi != String.Empty)
                {
                    pershk = pershkrimi;
                }
                else
                {
                    if (isShitje == true) //shitje
                    {
                        if (kodkonfigurimi == "FST")
                            pershk = "Nga shitjet pa artikuj";
                        else
                            pershk = "Nga shitjet";
                    }
                    else
                    {
                        if (kodkonfigurimi == "FBT")
                            pershk = "Nga blerjet pa artikuj";
                        else
                            pershk = "Nga blerjet";
                    }
                }

                int idLlojDok;
                int idkategoria;
                if (isShitje == true)
                {
                    idLlojDok = 14; //shitje
                    idkategoria = 1;
                }
                else
                {
                    idLlojDok = 13;//blerje      
                    idkategoria = 2;
                }
                clsFormatiKonfig formatNrPerKonfig = new clsFormatiKonfig();
                formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(dbshare, koka.idKonfigAmbjente);
                //gjenerimi i fletes kontabel per dokumentin e shitje /blerjes
                var konf = new clsKonfigurimAmbjenti(koka.idKonfigAmbjente, dbshare);
                if (konf.IdSkemeKontabel != 0)
                {
                    oFleteKontabel = clsKokaFleteKontabel.GjeneroKontabilizimShitje(koka.idShitjeKoka, koka.idNivel, koka.idKonfigAmbjente, koka.dtDok, koka.nrDok, koka.idNdermarje, koka.idNdermarjeVit, koka.idPerdoruesi, koka.dtRegjistrimi, koka.oColTrupiShitje, pershk, iddokngafk, idLlojDok, idPeriudha, idkategoria, isShitje, koka.idKlientFurnitor, koka.zbritje, koka.tvsh, koka.kursi, koka.idMonedha, out emratkf, out rreshtakf, out trupatperGjendjekf, out objektivat, out vleratobjektiva, out vleratobjektivamonbaze, out idllogobj, koka.idDegeAdministrative, 0, 0, out shfaqmesazhapolupe, trupivjeterqendra, llogaritfitimhumbjeneto, kusht.IdKusht, col, koka.OKokaMagazina.OcolTrupiMagazina, iddokngaqk, formatNrPerKonfig, dbKont);

                    // gjenerimi i gjendjes kf
                    oGjendjeKF = gjeneroGjendjeKf(koka.idNivel, koka.nrDok, koka.dtDok, koka.dtRegjistrimi, emratkf, rreshtakf, trupatperGjendjekf);

                }
            }

            if (koka.OFleteKontabel.NrDukumentiKokaFleteKontabel != null)
            {
                koka.OFleteKontabel.NrDukumentiKokaFleteKontabel = koka.nrDok;
                koka.OFleteKontabel.IdGjenerues = koka.IdShitjeKoka;
                kontMesazh = koka.OFleteKontabel.Ruaj(new clsDatabaseKontabilitet(dbRegj));
                if (!kontMesazh.Status)
                {
                    return new clsMesazh(false, kontMesazh.PershkrimMesazhi);
                }

                foreach (clsGjendjeKlientFurnitor gj in koka.OGjendjeKF)
                {
                    gj.IdDok = koka.IdShitjeKoka;
                    gj.NrDok = koka.nrDok;
                    if (gj.IdKlientGjendjeKf != 0)
                    {
                        int idG;
                        mesazh = dbRegj.ruajGjendjeKF(out idG, gj.IdDok, gj.NrDok, gj.DateDok, gj.VlMinus, gj.VlPlus, gj.NivelDok, gj.IdMonedhaDok, gj.KursiDok, gj.DateRegj, gj.VlMinusMonedheBaze, gj.VlPlusMonedheBaze, gj.IdKlientGjendjeKf);
                        if (!mesazh.Status)
                        {
                            //if (!modifikim) dbRegj.rollbackTransaksion();
                            //dbManager.RollBackTransaction();
                            return mesazh;
                        }

                    }
                }
            }
            #endregion

            #region lidhja e kthimit
            if (iddokshitje > 0 && idStatusDok == 1)
            {
                clsKokaShitje dokshitjepertukthyer = new clsKokaShitje();
                dokshitjepertukthyer.mbushKokaShitjeSipasIDPaTrup(iddokshitje, dbRegj);
                var azhornimi = new clsAzhornimKFKoka(dokshitjepertukthyer.idKlientFurnitor, this.dtDok, dbRegj);
                clsDokumentLidhesKoka dokumentiKoka = new clsDokumentLidhesKoka();
                colDokumentLidhesTrupi coltrupidok = new colDokumentLidhesTrupi();
                coltrupidok = krijoTrupDokumentLidhes(kursi, this.idKlientFurnitor, idShitjeKoka, this.idNivel, iddokshitje, dokshitjepertukthyer.idNivel, -(this.totali - this.zbritje), dokshitjepertukthyer.kursi, dokshitjepertukthyer.vleraMbetur);//krijojme trupin 
                clsKonfigurimAmbjenti konfigdokLidhes = new clsKonfigurimAmbjenti();
                konfigdokLidhes.mbushKonfigAmbjSipasKod("DL", idNdermarje, dbshare);
                mesazh = dokumentiKoka.krijoDokumentLidhesKoka(this.nrDok, this.dtDok, this.dtRegjistrimi, this.idKlientFurnitor, idShitjeKoka, isShitje ? 1 : 2, idNdermarje, this.idNdermarjeVit, konfigdokLidhes.IdNivel, konfigdokLidhes.IdKonfigAmbjente, iddokngalidhes, IdNivel, IdKonfigAmbjente, this.idStatusDok, coltrupidok, this.idPerdoruesi);//krijojme koken
                if (!mesazh.Status)
                {

                    return mesazh;
                }
                
                if (dokumentiKoka.OColTrupi.Count > 1 && this.idStatusDok == 1)//nese eshte bere lidhje dokumentash te gjenerohen diferencat nga kursi 
                {
                    string pershk = string.Empty;
                    if (this.pershkrimi != String.Empty)
                    {
                        pershk = this.pershkrimi;
                    }
                    else
                    {
                        if (isShitje)
                            pershk = "Diferenca nga kursi (Nga kthime shitje)";
                        else
                            pershk = "Diferenca nga kursi (Nga kthime blerje)";
                    }

                    int IdLlojDok = 68;//NKLD  
                    DbQendraKosto.clsKokaQendraKosto kokaqendra = new DbQendraKosto.clsKokaQendraKosto();
                    List<int> emratkf;
                    List<string> rreshtakf;
                    colTrupatFletetKontabel trupatperGjendjekf;
                    DbQendraKosto.colObjektivaKosto objektivat;
                    List<double> vleratobjektiva; List<double> vleratobjektivamonbaze;
                    List<int> idllogobj;
                    colDokumentat collidhes = new colDokumentat();
                    colDokumentat colkryesor = new colDokumentat();
                    clsDokumenti doklidhes = new clsDokumenti(this.idNivel, this.nrDok, this.DtDok, -(this.Totali - this.zbritje), -(this.totali - this.zbritje), this.kursi, this.kodMonedha, idNdermarje, this.idKlientFurnitor, this.idShitjeKoka, dbRegj);
                    doklidhes.IdNiveli = this.idNivel;
                    doklidhes.IdMonedha = this.idMonedha;
                    clsDokumenti dokkryesor = new clsDokumenti(dokshitjepertukthyer.idNivel, dokshitjepertukthyer.nrDok, dokshitjepertukthyer.DtDok, dokshitjepertukthyer.TotaliMeZbritjeMeTVSH, dokshitjepertukthyer.TotaliMeZbritjeMeTVSH, (azhornimi.Kursi != null && azhornimi.DateDok >= dokshitjepertukthyer.dtDok ? azhornimi.Kursi : dokshitjepertukthyer.kursi), dokshitjepertukthyer.kodMonedha, idNdermarje, dokshitjepertukthyer.idKlientFurnitor, dokshitjepertukthyer.idShitjeKoka, dbRegj);
                    dokkryesor.IdNiveli = dokshitjepertukthyer.idNivel;
                    dokkryesor.IdMonedha = dokshitjepertukthyer.idMonedha;
                    collidhes.Add(doklidhes);
                    colkryesor.Add(dokkryesor);
                    bool kontablidhes = clsAlternativaKushti.getAlternativa(konfigdokLidhes.IdKonfigAmbjente, "GJK", dbshare) == "Jo" ? false : true;
                    bool njihFitimNgaDifKursi = clsAlternativaKushti.getAlternativa(konfigdokLidhes.IdKonfigAmbjente, "NFHNDK") == "Po";
                    if (kontablidhes && njihFitimNgaDifKursi)
                    {
                        clsDatabaseKontabilitet dbkont = new clsDatabaseKontabilitet(dbRegj);
                        dokumentiKoka.OFleteKontabel = clsKokaFleteKontabel.gjeneroKontabilizimLidhjeDok(dokumentiKoka.IdKoka, dokumentiKoka.IdNivel, dokumentiKoka.IdKonfigAmbjente, this.DtDok, this.nrDok, this.idNdermarje, this.idNdermarjeVit, this.idPerdoruesi, this.dtRegjistrimi, collidhes, pershkrimi, 0, IdLlojDok, idPeriudha, 10, out emratkf, out rreshtakf, out trupatperGjendjekf, this.idKlientFurnitor, colkryesor, totali, out objektivat, out vleratobjektiva, out vleratobjektivamonbaze, out idllogobj, idDegeAdministrative, 0, 0, kokaqendra.ColTrupi, idGjuha, dbkont, konfigdokLidhes.IdKonfigAmbjente);

                        dokumentiKoka.OGjendjeKF = gjeneroGjendjeKf(dokumentiKoka.IdNivel, this.nrDok, this.DtDok, this.dtRegjistrimi, emratkf, rreshtakf, trupatperGjendjekf);
                    }
                    mesazh = dokumentiKoka.ruaj(kontablidhes, dbRegj);
                    if (!mesazh.Status)
                    {

                        return mesazh;
                    }

                }


            }
            #endregion

            foreach (clsGaranciArtikulli g in koka.colGaranci)
            {
                g.IdKokaShitje = koka.idShitjeKoka;
                g.Ruaj(dbRegj);
            }

            //ruajtja e fazave te kontrates
            if (koka.ocolFazat != null)
            {
                foreach (clsFazaKontrate f in koka.ocolFazat)
                {
                    f.IdKontrata = koka.IdShitjeKoka;
                    mesazh = f.ruaj(dbRegj);
                    if (!mesazh.Status)
                    {
                        return mesazh;
                    }
                }
            }

            if (koka.oKokaRezervime != null && koka.oKokaRezervime.NrDok != null && koka.oKokaRezervime.NrDok != string.Empty)
            {
                koka.oKokaRezervime.IdGjenerues = koka.idShitjeKoka;
                koka.oKokaRezervime.NrDok = koka.NrDok;
                colTrupiRezervime coltrupi = new colTrupiRezervime();
                if (modifikim)
                {
                    if (koka.idStatusDok == 1 && kodkonfigurimi.Length >= 5 && kodkonfigurimi.Substring(0, 5) == "VFONE")
                    {

                        koka.OKokaRezervime.Statusi = 1;//per dok e rezervimit te vfone e dime qe ne momentin e ruajtjes dok do jete ekzekutuar sepse krijohen njekohesisht dhe hyrja dhe dalja me te njejten dokumenta
                    }
                    kokaeksistuezerez.OcolTrupiRezervime.mbushGjitheTrupiRezervimiNgaKoka(kokaeksistuezerez.IdKokaRezervimi, dbRegj);
                    mesazh = koka.oKokaRezervime.ruajNgaKokaShitje(null, dbRegj, koka.idKrijuesi, null, !(koka.idStatusDok == 1 && kodkonfigurimi.Length >= 5 && kodkonfigurimi.Substring(0, 5) == "VFONE"));
                }
                else
                {
                    mesazh = koka.oKokaRezervime.ruajNgaKokaShitje(null, dbRegj, koka.idKrijuesi, null, true);
                }

                if (!mesazh.Status)
                {
                    return mesazh;
                }
            }

            if (!modifikimAprovimi)
            {
                clsEtapeAprovimi etape = new clsEtapeAprovimi();
                mesazh = etape.ruaj(idPerdoruesi, serverUrl, skemaWorkFlow, dbRegj, koka.idDokNga, koka.IdPerdoruesi, statusapp, idetapa, kategoria, koka.idStatusDok, koka.idNdermarje, koka.statusAprovimi, koka.idKlientFurnitor, koka.idShitjeKoka, koka.idPerdoruesi, koka.Totali - koka.tvsh - koka.zbritje, koka.dtKrijimi, koka.idKonfigAmbjente, koka.nrDok, koka.dtDok);
                if (!mesazh.Status)
                {
                    return mesazh;
                }

                if (modifikim && koka.IdStatusDok == 1) //vetem nqs aprovohet
                {
                    mesazh = dbRegj.modifikoEtapeIdkoka(koka.IdDokNga, koka.idShitjeKoka);  //modifikojme id e kokes se shitjes tek etapat pasi aprovohet dhe ruhet
                    if (!mesazh.Status)
                    {
                        return mesazh;
                    }
                    if (clsAlternativaKushti.getAlternativa(koka.IdKonfigAmbjente, "VF_VM", dbshare) == "Po")
                    {
                        mesazh = dbRegj.modifikoStatusMarreveshje(koka.nrDok, koka.IdMarreveshje, koka.DtDok);
                        if (!mesazh.Status)
                            return mesazh;
                    }
                }
            }

            ImbLogger.LogTraceShitje($"Mbaroi metoda ruajShitje me parametra serverUrl:" + serverUrl + $", koka:{JsonConvert.SerializeObject(koka)} isShitje:{isShitje}, modifikim:{modifikim} idPeriudha:{idPeriudha}, colkonvertimi:{JsonConvert.SerializeObject(colkonvertimi)}, gjenerodokmagazine:{gjenerodokmagazine}, skemaWorkFlow:{skemaWorkFlow}, idetapa:{idetapa}, kontrollodisponibel:{kontrollodisponibel}, eshteOwn:{eshteOwn}, serialet:{JsonConvert.SerializeObject(serialet)}, konfiguriimAmortizimi:{JsonConvert.SerializeObject(konfigurimAmortizimi)}, colAmortizimetEVjetra:{JsonConvert.SerializeObject(colAmortizimetEVjetra)}, mekontabilizim:{mekontabilizim}, kodkonfigurimi" + kodkonfigurimi + $", iddokngafk:{iddokngafk}, iddokngaqk:{iddokngaqk}, iddokshitje:{iddokshitje}, idGjuha:{idGjuha}, iddokngalidhes:{iddokngalidhes}, tollona:{tollona}, zevendesim:{zevendesim}, tollonakastrati:{tollonakastrati}, tollonakastartielektronik:{tollonakastratielektronik}, zevendesimtollonakastrati:{zevendesimtollonakastrati}, ruajrenditje:{ruajrenditje}, kontrolloSasiKonvertimiDheKthimi:{kontrolloSasiKonvertimiDheKthimi}, kaveprimepas:{kaveprimepas}, kontrolloIMEIFifo:{kontrolloIMEIFifo}, blerjedealer:{blerjedealer}, promocione:{promocione}");

            return new clsMesazh(true, mesazh.PershkrimMesazhi == "Ruajtja përfundoi me sukses!" ? MessagesResource.Messages["msgDokumentiURuajtMeSukese"] : mesazh.PershkrimMesazhi);
        }

        private clsMesazh KontrolloDisponibilitet(clsKokaShitje koka, clsTrupiShitje o, clsArtikulli artikulli, clsDatabaseRegjistrim dbRegj, int idKodi, int idMagazina, int idDetajimi, clsArtikulli perberes, double koeficent, int idMagazineShitje, int idKodiShitje)
        {   //ne rast se jemi ne modifikim dhe kemi kontroll duhet te mos marrim vete dokumentin 
            double sasimag = clsTrupiMagazina.merrSasi(perberes, idMagazina, koka.dtDok, 0, dbRegj, true);
            double sasiDaljeRezervimi = (koka.oKokaMagazina.OKokaRezervime != null && koka.oKokaMagazina.OKokaRezervime.OcolTrupiRezervime != null) ? koka.oKokaMagazina.OKokaRezervime.OcolTrupiRezervime.ktheSasiDaljeRezervimi(idKodi, idMagazina) : 0;

            if (sasiDaljeRezervimi == 0 || sasimag < sasiDaljeRezervimi)
            {
                double totaliArtikullit = ktheTotalinArtikullit(koka.OColTrupiShitje, idKodiShitje, idMagazineShitje, o.IdNjesia == artikulli.Njesi1Artikulli ? 1 : artikulli.KoeficientArtikulli) * koeficent;

                double sasiarez = dbRegj.ktheSasineRezervuarSipasArtikullitDheMagazines(idKodi, 0, idMagazina, koka.dtDok);

                double sasiadisp = sasimag - sasiarez + sasiDaljeRezervimi;
                if (totaliArtikullit > sasiadisp)
                    return new clsMesazh(false, "Sasia e daljes është më e madhe se gjendja disponibel " + sasiadisp + " e artikullit: " + artikulli.KodArtikulli + "!");
            }

            return new MesazhSuksesi();
        }

        private void VendosIdTrupiNgaVjenNeTrupRezervimi(clsArtikulli artikull, clsKokaShitje koka, ref int count, clsTrupiShitje o, int idT)
        {
            for (int i = count; i < koka.oKokaRezervime.OcolTrupiRezervime.Count; i++)
            {
                if (o.IdLlojVeprimi == 1)
                {
                    if (artikull.KodArtikulli == ((clsArtikulli)koka.oKokaRezervime.OcolTrupiRezervime[i].Element).KodArtikulli)
                    {
                        koka.oKokaRezervime.OcolTrupiRezervime[i].IdTrupiNgaVjen = idT;
                        count = i + 1;
                        break;
                    }
                }
            }
        }

        public double ktheTotalinArtikullit(colTrupiShitje trupi, int idArtikulli, int idMagazina, decimal koeficient)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda ktheTotalinArtikullit me parametra trupi:{JsonConvert.SerializeObject(trupi)}, idArtikulli:{idArtikulli}, idMagazina:{idMagazina}, koeficient:{koeficient}");
            double totali = 0;
            foreach (clsTrupiShitje o in trupi)
            {
                if (o.IdLlojVeprimi == 1 && o.IdKodi == idArtikulli && o.IdMagazina == idMagazina)
                    totali += o.Sasia * double.Parse(koeficient.ToString());
            }
            ImbLogger.LogTraceShitje($"Mbaroi metoda ktheTotalinArtikullit me parametra trupi:{JsonConvert.SerializeObject(trupi)}, idArtikulli:{idArtikulli}, idMagazina:{idMagazina}, koeficient:{koeficient}");
            return Math.Round(totali, 10);
        }

        private clsVeprimBankaKoka krijoVeprimBanka(clsKokaShitje kokeShitje, int statusDokumenti, int idperiudha, bool mekontabilizim, bool isShitje, clsDatabaseArkaBanka db, out string shfaqmesazhapolupe, out string shfaqmesazhapolupeVDK, int idBankeKf, bool llojiKF)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda ktheTotalinArtikullit me parametra kokeShitje:{JsonConvert.SerializeObject(kokeShitje)}, statusDokumenti:{statusDokumenti}, idperiudha:{idperiudha}, mekontabilizim:{mekontabilizim}, isShitje:{isShitje}, idBankeKf:{idBankeKf}, llojiKF:{llojiKF}");
            clsDatabaseAdmin dbadmin = new clsDatabaseAdmin(db);
            clsDatabaseShare dbshare = new clsDatabaseShare(db);
            clsDatabaseKontabilitet dbKontab = new clsDatabaseKontabilitet(db);
            clsVeprimBankaKoka veprimebanka = new clsVeprimBankaKoka();
            colVeprimBankaTrupi colTrupi = new colVeprimBankaTrupi();

            clsKonfigurimAmbjenti konf = new clsKonfigurimAmbjenti();
            colKonfigurimAmbjenti colKonfig = new colKonfigurimAmbjenti();

            int idmonedhabanka = 0;
            double shuma = 0, kursi = 1;
            string llojveprimi = "Arketim";
            string arkabanka = "arka";
            int idllojdokumenti = 3;
            clsMesazh mesazh;
            object[] nivele = { kokeShitje.IdNivel };
            clsBanka arka = new clsBanka();
            if (kokeShitje.IdKlientFurnitor != 0)
            {
                colBankat bankat = new colBankat();
                bankat.mbushGjitheBankatSipasAutorizimeveSipasLlojit(kokeShitje.idNdermarje, kokeShitje.idPerdoruesi, false, db);

                if (kokeShitje.IdArka != 0)
                {
                    arka.mbushBankeID(kokeShitje.IdArka, db);
                    if (arka.LlojArkaBanka == true)
                        arkabanka = "banka";
                    else
                        arkabanka = "arka";
                    clsKurset kurs = new clsKurset(arka.IdMonedhaBanka, kokeShitje.dtDok, dbadmin);
                    if (kurs.VleraKursi == 0)
                        kurs.VleraKursi = 1;
                    kursi = kurs.VleraKursi;
                    idmonedhabanka = arka.IdMonedhaBanka;
                }
                else if (bankat.Count == 1)
                {
                    arka = bankat[0];
                    if (arka.LlojArkaBanka == true)
                        arkabanka = "banka";
                    else
                        arkabanka = "arka";
                    clsKurset kurs = new clsKurset(arka.IdMonedhaBanka, kokeShitje.dtDok, dbadmin);
                    if (kurs.VleraKursi == 0)
                        kurs.VleraKursi = 1;
                    kursi = kurs.VleraKursi;
                    idmonedhabanka = arka.IdMonedhaBanka;
                }
                else
                {
                    if (idBankeKf != 0)
                    {
                        mesazh = arka.mbushBankeID(idBankeKf, db);
                        if (!mesazh.Status)
                            throw new MyException(mesazh.PershkrimMesazhi);
                        if (arka.LlojArkaBanka == true)
                            arkabanka = "banka";
                        else
                            arkabanka = "arka";
                        clsKurset kurs = new clsKurset(arka.IdMonedhaBanka, kokeShitje.dtDok, dbadmin);
                        if (kurs.VleraKursi == 0)
                            kurs.VleraKursi = 1;
                        kursi = kurs.VleraKursi;
                        idmonedhabanka = arka.IdMonedhaBanka;
                    }
                    else
                    {
                        ImbLogger.LogErrorShitje("Klienti i zgjedhur duhet të ketë një bankë të paracaktuar!");
                        throw new MyException("Klienti i zgjedhur duhet të ketë një bankë të paracaktuar!");
                    }
                }
            }
            else
            {
                ImbLogger.LogErrorShitje("Nuk mund të bëhet me pagese automatike pa zgjedhur një klient!");
                throw new MyException("Nuk mund të bëhet me pagese automatike pa zgjedhur një klient!");
            }

            if ((kokeShitje.Totali - kokeShitje.Zbritje) == 0)
            {
                ImbLogger.LogErrorShitje("Ju lutem zgjidhni nje menyre tjeter pagese pasi totali i fatures eshte 0!");
                throw new MyException(MessagesResource.Messages["msgZgjidhniTjeterMenyrePagese"]);
            }

            double shumaPaKurs = kokeShitje.Totali - kokeShitje.Zbritje;
            bool isVleraTotalPozitive = shumaPaKurs >= 0;
            llojveprimi = clsVeprimBankaKoka.llojVeprimi(isShitje, arkabanka, shumaPaKurs);
            shumaPaKurs = isVleraTotalPozitive ? shumaPaKurs : -shumaPaKurs;

            if (idmonedhabanka == kokeShitje.IdMonedha)
                shuma = shumaPaKurs;
            else
                shuma = shumaPaKurs * kokeShitje.Kursi / kursi;

            if (llojveprimi == "Terheqje" || llojveprimi == "Pagese")
                idllojdokumenti = 3;
            else
                if (llojveprimi == "Derdhje" || llojveprimi == "Arketim")
                idllojdokumenti = 4;

            if (llojveprimi == "Arketim" || llojveprimi == "Pagese")
                konf.IdKategori = 3;   //arka
            else
                konf.IdKategori = 4;  //banka

            clsDatabaseRegjistrim dbregj = new clsDatabaseRegjistrim(db);
            int idNiveli = clsNivelRegjistrimi.ktheIdNivelRegjistrimiSipasKodi(llojveprimi, kokeShitje.idNdermarje, dbregj);
            colKonfig.mbushKonfigAmbjSipasIdKategoriIdNivel(konf.IdKategori, idNiveli, kokeShitje.idPerdoruesi, dbshare);
            if (colKonfig.Count > 0)
                konf = colKonfig[0];
            clsKonfigurimAmbjenti konflidhes = new clsKonfigurimAmbjenti(konf.IdKonfigurimi, dbshare);

            clsVeprimBankaTrupi trup = new clsVeprimBankaTrupi
            {
                IdFatura = kokeShitje.idShitjeKoka,
                IdNivel = kokeShitje.IdNivel,
                IdOpsionePagese = 1
            };
            //pagese fature
            trup.Lloji = llojiKF ? "Klient" : "Furnitor";
            trup.IdSubjekti = kokeShitje.IdKlientFurnitor;
            switch (llojveprimi)
            {
                case "Terheqje":
                case "Pagese":
                    trup.DebiKredi = "Debi";
                    break;
                case "Derdhje":
                case "Arketim":
                    trup.DebiKredi = "Kredi";
                    break;
                default:
                    trup.DebiKredi = string.Empty;
                    break;
            }
            trup.NrTel = string.Empty;
            if (idmonedhabanka == kokeShitje.IdMonedha)
            {
                trup.VleraPaguar = (shumaPaKurs);
                trup.VleraPaArketueshme = (shumaPaKurs);
                trup.VleraPaguarMonedhaBaze = (shumaPaKurs) * kursi;
                trup.KMK = kursi;// kokeShitje.Kursi;
            }
            else
            {
                trup.VleraPaguar = (shumaPaKurs) * kokeShitje.Kursi / kursi;
                trup.VleraPaArketueshme = (shumaPaKurs) * kokeShitje.Kursi / kursi;
                trup.VleraPaguarMonedhaBaze = (shumaPaKurs) * kokeShitje.Kursi;
                trup.KMK = kokeShitje.Kursi;
            }
            trup.PershkrimiTrupi = kokeShitje.Pershkrimi;

            colTrupi.Add(trup);
            int idDegeAdm = 0; string kodiDegeAdm = string.Empty;
            if (arka.IdDegeAdministrative > 0)
            {
                idDegeAdm = arka.IdDegeAdministrative;
                kodiDegeAdm = clsDegeAdministrative.mbushKodiDegeAdministrativeSipasiD(arka.IdDegeAdministrative, dbregj);
            }
            else
            {
                idDegeAdm = kokeShitje.idDegeAdministrative;
                kodiDegeAdm = kokeShitje.kodDegeAdministrative;
            }
            string idRaportDesArkaBanka = clsAtributeTrupi.merrVleredefaultSipasKontrollitDheKonfigurimit(konf.IdKonfigAmbjente, "cmbFormatiPrintimit", 301);
            bool meKontabilizimVeprimBanke = clsAlternativaKushti.getAlternativa(konf.IdKonfigAmbjente, "GJK") != "Jo";

            mesazh = veprimebanka.krijoVeprimeBanke(arka.IdBanka, arka.KodiBanka, kursi, kokeShitje.DtDok, kokeShitje.DtRegjistrimi, kokeShitje.NrDok, int.Parse(clsKokaFleteKontabel.GjeneroNrReference(kokeShitje.idNdermarjeVit, dbKontab).ToString()), string.Empty, kokeShitje.Pershkrimi, kokeShitje.IdMenyrePagese, kokeShitje.kodMenyrePagese, shuma, shuma * kursi, 0, 0, llojveprimi, kokeShitje.idPerdoruesi, idllojdokumenti, statusDokumenti, kokeShitje.idNdermarjeVit, konf.IdKonfigAmbjente, 0, 0, 0, konf.IdNivel, 0, idDegeAdm, kodiDegeAdm, kokeShitje.idNdermarje, 0, colTrupi, meKontabilizimVeprimBanke, idperiudha, arka.IdMonedhaBanka, string.Empty, 0, 0, 0, konflidhes, nivele, db, kokeShitje, out shfaqmesazhapolupe, out shfaqmesazhapolupeVDK, new DbQendraKosto.colTrupiQendraKosto(), 0, kokeShitje.EmerKlienti, string.Empty, kokeShitje.idAutomjet, kokeShitje.targa, !string.IsNullOrEmpty(idRaportDesArkaBanka) ? Convert.ToInt32(idRaportDesArkaBanka) : 0, string.Empty, string.Empty, string.Empty, false, StatusAprovimi.Undefined, "", 0, "", null, konf.IdKategori, kokeShitje.idPerdoruesi, kokeShitje.idKrijuesi);
            if (!mesazh.Status)
            {
                ImbLogger.LogErrorShitje("Mbaroi metoda ktheTotalinArtikullit");
                throw new MyException(mesazh.PershkrimMesazhi);
            }
            ImbLogger.LogTraceShitje($"Mbaroi metoda ktheTotalinArtikullit me parametra kokeShitje:{JsonConvert.SerializeObject(kokeShitje)}, statusDokumenti:{statusDokumenti}, idperiudha:{idperiudha}, mekontabilizim:{mekontabilizim}, isShitje:{isShitje}, idBankeKf:{idBankeKf}, llojiKF:{llojiKF}");
            return veprimebanka;
        }

        public clsMesazh modifiko(int idGjuha, string serverUrl, bool lidhur, IDictionary<string, object> hfregjistrime, int idPeriudha, colKonvertimi colkonvertimi, bool gjenerodokmag, int skemaWorkFlow, DbRegjistrim.StatusAprovimi statusapp, int idetapa, out string shfaqmesazhapolupemag, clsKokaShitje shitjemema, bool dergoEmail, bool eshteOwn, bool dergoemailVFOne, colSerialetMagazine serialet, clsKonfigurimAmbjenti konfigurimAmortizimi, bool isshitje, clsKokaShitje faturaShitjeNgaUrdherShitjaMeKuponTatimor, out bool printofature, out bool printogarancifature, out string shfaqmesazhapolupe, bool mekontabilizim, DbQendraKosto.colTrupiQendraKosto trupivjeterqendra, string kodkonfigurimi, ResourceManager rm, CultureInfo cultinf, bool tollona, bool zevendesim, bool tollonakastrati, bool tollonakastratielektronik, bool zevendesimtollonakastrati, bool ruajrenditje, bool kontrolloSasiKonvertimiDheKthimi, bool vjenNgaRiruajtja, bool kontrolloIMEIFifo, bool blerengadealer, bool promocione, ref DbData dbData, out string mesazhmevonshem, bool vjenNgaImportSQL, string idDokImport, string emerTabKoka, string primaryKeyEmerFushe, string emerFusheNdermarrje)
        {
            ImbLogger.LogTraceShitje($"Filloi clsMesazh modifiko me parametra idGjuha:{idGjuha}, serverUrl:" + serverUrl + $", lidhur:{lidhur}, idPeriudha:{idPeriudha}, colkonvertimi:{JsonConvert.SerializeObject(colkonvertimi)}, gjenerodokmag:{gjenerodokmag}, skemaWorkFlow:{skemaWorkFlow}, idetapa:{idetapa}, shitjemema:{JsonConvert.SerializeObject(shitjemema)}, dergoEmail:{dergoEmail}, eshteOwn:{eshteOwn}, dergoemailVFOne:{dergoemailVFOne}, serialet:{JsonConvert.SerializeObject(serialet)}, konfigurimAmortizimi:{JsonConvert.SerializeObject(konfigurimAmortizimi)}, isshitje:{isshitje}, faturaShitjeNgaUrdherShitjaMeKuponTatimor:{JsonConvert.SerializeObject(faturaShitjeNgaUrdherShitjaMeKuponTatimor)}, mekontabilizim:{mekontabilizim}, kodkonfigurimi:" + kodkonfigurimi + $", ruajrenditje:{ruajrenditje}, kontrolloSasiKonvertimiDheKthimi:{kontrolloSasiKonvertimiDheKthimi}, vjenNgaRiruajtja:{vjenNgaRiruajtja}, kontrolloIMEIFifo:{kontrolloIMEIFifo}, blerengadealer:{blerengadealer}, promocione:{promocione}, idDokImport:" + idDokImport + $",emerTabKoka:" + emerTabKoka + $", primaryKeyEmerFushe:" + primaryKeyEmerFushe + $", emerFusheNdermarrje:" + emerFusheNdermarrje);
            clsVeprimBankaKoka banka = new clsVeprimBankaKoka();
            string shfaqmesazhapolupebanka, shfaqmesazhapolupeVDK = "";
            bool pageseFature = false;
            ImbLogger.LogTraceShitje($"Mbaroi clsMesazh modifiko me parametra idGjuha:{idGjuha}, serverUrl:" + serverUrl + $", lidhur:{lidhur}, idPeriudha:{idPeriudha}, colkonvertimi:{JsonConvert.SerializeObject(colkonvertimi)}, gjenerodokmag:{gjenerodokmag}, skemaWorkFlow:{skemaWorkFlow}, idetapa:{idetapa}, shitjemema:{JsonConvert.SerializeObject(shitjemema)}, dergoEmail:{dergoEmail}, eshteOwn:{eshteOwn}, dergoemailVFOne:{dergoemailVFOne}, serialet:{JsonConvert.SerializeObject(serialet)}, konfigurimAmortizimi:{JsonConvert.SerializeObject(konfigurimAmortizimi)}, isshitje:{isshitje}, faturaShitjeNgaUrdherShitjaMeKuponTatimor:{JsonConvert.SerializeObject(faturaShitjeNgaUrdherShitjaMeKuponTatimor)}, mekontabilizim:{mekontabilizim}, kodkonfigurimi:" + kodkonfigurimi + $", ruajrenditje:{ruajrenditje}, kontrolloSasiKonvertimiDheKthimi:{kontrolloSasiKonvertimiDheKthimi}, vjenNgaRiruajtja:{vjenNgaRiruajtja}, kontrolloIMEIFifo:{kontrolloIMEIFifo}, blerengadealer:{blerengadealer}, promocione:{promocione}, idDokImport:" + idDokImport + $",emerTabKoka:" + emerTabKoka + $", primaryKeyEmerFushe:" + primaryKeyEmerFushe + $", emerFusheNdermarrje:" + emerFusheNdermarrje);
            return modifikoSh(idGjuha, serverUrl, lidhur, hfregjistrime, idPeriudha, colkonvertimi, gjenerodokmag, skemaWorkFlow, statusAprovimi, idetapa, out shfaqmesazhapolupemag, shitjemema, dergoEmail, eshteOwn, dergoemailVFOne, serialet, konfigurimAmortizimi, isshitje, faturaShitjeNgaUrdherShitjaMeKuponTatimor, out printofature, out printogarancifature, out shfaqmesazhapolupe, mekontabilizim, trupivjeterqendra, kodkonfigurimi, rm, cultinf, tollona, zevendesim, tollonakastrati, tollonakastratielektronik, zevendesimtollonakastrati, ruajrenditje, kontrolloSasiKonvertimiDheKthimi, out banka, out shfaqmesazhapolupebanka, out shfaqmesazhapolupeVDK, out pageseFature, vjenNgaRiruajtja, kontrolloIMEIFifo, blerengadealer, promocione, ref dbData, out mesazhmevonshem, vjenNgaImportSQL, idDokImport, emerTabKoka, primaryKeyEmerFushe, emerFusheNdermarrje);
        }

        /// <summary>
        /// Modifikon objektin e  kokes se dokumentit ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsKokaShitje.modifikoShitje"/> 
        /// </summary>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo</returns>
        /// <param name="serverUrl"></param>
        public clsMesazh modifikoSh(int idGjuha, string serverUrl, bool lidhur, IDictionary<string, object> hfregjistrime, int idPeriudha, colKonvertimi colkonvertimi, bool gjenerodokmag, int skemaWorkFlow, 
            StatusAprovimi statusapp, int idetapa, out string shfaqmesazhapolupemag, clsKokaShitje shitjemema, bool dergoEmail, bool eshteOwn, bool dergoemailVFOne, colSerialetMagazine serialet, 
            clsKonfigurimAmbjenti konfigurimAmortizimi, bool isshitje, clsKokaShitje faturaShitjeNgaUrdherShitjaMeKuponTatimor, out bool printofature, out bool printogarancifature, out string shfaqmesazhapolupe,
            bool mekontabilizim, DbQendraKosto.colTrupiQendraKosto trupivjeterqendra, string kodkonfigurimi, ResourceManager rm, CultureInfo cultinf, bool tollona, bool zevendesim, bool tollonakastrati, 
            bool tollonakastratielektronik, bool zevendesimtollonakastrati, bool ruajrenditje, bool kontrolloSasiKonvertimiDheKthimi, out clsVeprimBankaKoka banka, out string shfaqmesazhapolupebanka,
            out string shfaqmesazhapolupeVDK, out bool pagesefature, bool vjenNgaRiruajtja, bool kontrolloIMEIFifo, bool blerjengadealer, bool promocione, ref DbData dbData, out string mesazhmevonshem, bool vjenNgaImportSQL, 
            string idDokImport, string emerTabKoka, string primaryKeyEmerFushe, string emerFusheNdermarrje)
        {
            ImbLogger.LogTraceShitje($"Filloi clsMesazh modifiko me parametra idGjuha:{idGjuha}, serverUrl:" + serverUrl + $", lidhur:{lidhur}, idPeriudha:{idPeriudha}, colkonvertimi:{JsonConvert.SerializeObject(colkonvertimi)}, gjenerodokmag:{gjenerodokmag}, skemaWorkFlow:{skemaWorkFlow}, idetapa:{idetapa}, shitjemema:{JsonConvert.SerializeObject(shitjemema)}, dergoEmail:{dergoEmail}, eshteOwn:{eshteOwn}, dergoemailVFOne:{dergoemailVFOne}, serialet:{JsonConvert.SerializeObject(serialet)}, konfigurimAmortizimi:{JsonConvert.SerializeObject(konfigurimAmortizimi)}, isshitje:{isshitje}, faturaShitjeNgaUrdherShitjaMeKuponTatimor:{JsonConvert.SerializeObject(faturaShitjeNgaUrdherShitjaMeKuponTatimor)}, mekontabilizim:{mekontabilizim}, kodkonfigurimi:" + kodkonfigurimi + $", ruajrenditje:{ruajrenditje}, kontrolloSasiKonvertimiDheKthimi:{kontrolloSasiKonvertimiDheKthimi}, vjenNgaRiruajtja:{vjenNgaRiruajtja}, kontrolloIMEIFifo:{kontrolloIMEIFifo}, blerjengadealer:{blerjengadealer}, promocione:{promocione}, idDokImport:" + idDokImport + $",emerTabKoka:" + emerTabKoka + $", primaryKeyEmerFushe:" + primaryKeyEmerFushe + $", emerFusheNdermarrje:" + emerFusheNdermarrje);
            mesazhmevonshem = "";
            shfaqmesazhapolupebanka = "jo";
            shfaqmesazhapolupeVDK = "jo";
            shfaqmesazhapolupemag = "jo";
            shfaqmesazhapolupe = "jo";
            bool kontrollodisponibel = false;
            printogarancifature = false;
            printofature = false;
            pagesefature = false;
            banka = new clsVeprimBankaKoka();
            if (clsAlternativaKushti.getAlternativa(this.IdKonfigAmbjente, "KD") == "Po")
                kontrollodisponibel = true;
            bool kontrollodisponibelmema = clsAlternativaKushti.getAlternativa(shitjemema.IdKonfigAmbjente, "KD") == "Po";
            clsKonfigurimAmbjenti konfigFature = new clsKonfigurimAmbjenti();
            konfigFature.mbushKonfigAmbjSipasKod(kodkonfigurimi, idNdermarje);
            double vleraMbetur = ktheVlereMbeturPerDok(this.IdShitjeKoka, idNdermarje, konfigFature.IdKategori);
            clsMesazh u_modifikua = new clsMesazh();
            try
            {
                using (var scope = new MyTransactionScope(dbData))
                {
                    clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim(dbData);
                    clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin(dbData);

                    clsKokaShitje kokaEkzistuese = new clsKokaShitje();
                    kokaEkzistuese.mbushKokaShitjeSipasIDPaTrup(this.IdShitjeKoka, dbRegj);
                    this.idKrijuesi = kokaEkzistuese.idKrijuesi;
                    //dokumentit te ri i vendosim id e krijuesit te dokumentit te vjeter
                    if (!vjenNgaImportSQL)
                        this.idDokTransferimNga = kokaEkzistuese.idDokTransferimNga;

                    clsNivelRegjistrimi nivel = new clsNivelRegjistrimi();
                    nivel.mbushNivelRegjistrimiSipasIdPaKonvertime(kokaEkzistuese.IdNivel, dbRegj);
                    clsKokaShitje kokaEkzistuesemema = new clsKokaShitje();
                    ImbLogger.LogTraceShitje($"Mbaroi clsMesazh modifiko me parametra idGjuha:{idGjuha}, serverUrl:" + serverUrl + $", lidhur:{lidhur}, idPeriudha:{idPeriudha}, colkonvertimi:{JsonConvert.SerializeObject(colkonvertimi)}, gjenerodokmag:{gjenerodokmag}, skemaWorkFlow:{skemaWorkFlow}, idetapa:{idetapa}, shitjemema:{JsonConvert.SerializeObject(shitjemema)}, dergoEmail:{dergoEmail}, eshteOwn:{eshteOwn}, dergoemailVFOne:{dergoemailVFOne}, serialet:{JsonConvert.SerializeObject(serialet)}, konfigurimAmortizimi:{JsonConvert.SerializeObject(konfigurimAmortizimi)}, isshitje:{isshitje}, faturaShitjeNgaUrdherShitjaMeKuponTatimor:{JsonConvert.SerializeObject(faturaShitjeNgaUrdherShitjaMeKuponTatimor)}, mekontabilizim:{mekontabilizim}, kodkonfigurimi:" + kodkonfigurimi + $", ruajrenditje:{ruajrenditje}, kontrolloSasiKonvertimiDheKthimi:{kontrolloSasiKonvertimiDheKthimi}, vjenNgaRiruajtja:{vjenNgaRiruajtja}, kontrolloIMEIFifo:{kontrolloIMEIFifo}, blerjengadealer:{blerjengadealer}, promocione:{promocione}, idDokImport:" + idDokImport + $",emerTabKoka:" + emerTabKoka + $", primaryKeyEmerFushe:" + primaryKeyEmerFushe + $", emerFusheNdermarrje:" + emerFusheNdermarrje);

                    if (shitjemema.nrDok != null && shitjemema.nrDok != string.Empty)
                    {
                        kokaEkzistuesemema.ktheKokaShitjeSipasIdTransferimi(kokaEkzistuese.IdShitjeKoka,
                            kokaEkzistuese.idKonfigAmbjente, dbRegj);
                    }

                    bool modifikimAprovimi = statusapp != DbRegjistrim.StatusAprovimi.Undefined;
                    bool draftSDAF = clsAlternativaKushti.getAlternativa(this.IdKonfigAmbjente, "SDAF") == "Draft";
                    if (!lidhur)
                    {
                        u_modifikua = kontrolloShitje(dbRegj, hfregjistrime, kokaEkzistuese);
                        if (!u_modifikua.Status)
                        {
                            return u_modifikua;
                        }

                        this.idTransferimi = kokaEkzistuese.idTransferimi;
                        this.idKonfigTransferimi = kokaEkzistuese.idKonfigTransferimi;
                        kokaEkzistuese.mbushTrupShitje();
                        
                        u_modifikua = this.modifikoShitje(serverUrl, kokaEkzistuese, this, dbRegj, idPeriudha,
                            colkonvertimi, gjenerodokmag, skemaWorkFlow, statusapp, idetapa, out shfaqmesazhapolupemag,
                            kontrollodisponibel, eshteOwn, serialet, konfigurimAmortizimi, isshitje, mekontabilizim,
                            trupivjeterqendra, out shfaqmesazhapolupe, kodkonfigurimi, rm, cultinf, idGjuha, tollona,
                            zevendesim, tollonakastrati, tollonakastratielektronik, zevendesimtollonakastrati,
                            ruajrenditje, kontrolloSasiKonvertimiDheKthimi, kontrolloIMEIFifo, blerjengadealer, promocione, out mesazhmevonshem, modifikimAprovimi, kokaEkzistuese);
                        if (!u_modifikua.Status)
                        {
                            return u_modifikua;
                        }

                        clsDatabaseShare dbshare = new clsDatabaseShare(dbAdmin);
                        if (!string.IsNullOrEmpty(faturaShitjeNgaUrdherShitjaMeKuponTatimor?.nrDok))
                        {
                            colKonvertimi colkonvshitje = new colKonvertimi
                            {
                                new clsKonvertimi(0, 0, this.idShitjeKoka, this.idKonfigAmbjente, faturaShitjeNgaUrdherShitjaMeKuponTatimor.idKonfigAmbjente)
                            };
                            //krijojme konvertimin
                            foreach (clsTrupiShitje trup in faturaShitjeNgaUrdherShitjaMeKuponTatimor.oColTrupiShitje)
                            {
                                //kalojme id e konvertimit
                                trup.IdTrupiKonvertimi = trup.IdShitjeTrupi;
                            }
                            u_modifikua = vendosNrAutomatikPerShitje(faturaShitjeNgaUrdherShitjaMeKuponTatimor, dbRegj,
                                dbAdmin);
                            if (
                                clsAtributeTrupi.merrVleredefaultSipasKontrollitDheKonfigurimit(
                                    faturaShitjeNgaUrdherShitjaMeKuponTatimor.idKonfigAmbjente, "cbPrinto", 506, dbshare) ==
                                "true")
                                printofature = true;
                            if (
                                clsAtributeTrupi.merrVleredefaultSipasKontrollitDheKonfigurimit(
                                    faturaShitjeNgaUrdherShitjaMeKuponTatimor.idKonfigAmbjente, "cbGaranci", 506,
                                    dbshare) == "true")
                                printogarancifature = true;
                            if (!u_modifikua.Status)
                            {
                                return u_modifikua;
                            }
                            faturaShitjeNgaUrdherShitjaMeKuponTatimor.faturePermbledhese = true;
                            string mesazhLupe = "", mesazhLupeMag = "";
                            bool kontrollodisponibelfsh = false;
                            if (clsAlternativaKushti.getAlternativa(faturaShitjeNgaUrdherShitjaMeKuponTatimor.IdKonfigAmbjente, "KD") == "Po")
                                kontrollodisponibelfsh = true;
                            u_modifikua = faturaShitjeNgaUrdherShitjaMeKuponTatimor.ruajShitje(serverUrl, faturaShitjeNgaUrdherShitjaMeKuponTatimor, isshitje,
                                false, idPeriudha, dbRegj, colkonvshitje, false, skemaWorkFlow, statusapp, idetapa,
                                out mesazhLupeMag, new DbQendraKosto.colTrupiQendraKosto(), kontrollodisponibelfsh,
                                eshteOwn, serialet, konfigurimAmortizimi, new colAmortizimiKoka(), true,
                                new DbQendraKosto.colTrupiQendraKosto(), out mesazhLupe, kodkonfigurimi, 0, 0,
                                0, idGjuha, 0, tollona, zevendesim, tollonakastrati,
                                tollonakastratielektronik, zevendesimtollonakastrati, ruajrenditje,
                                kontrolloSasiKonvertimiDheKthimi, false, kontrolloIMEIFifo, blerjengadealer, promocione, out mesazhmevonshem, modifikimAprovimi,kokaEkzistuese);
                            if (!u_modifikua.Status)
                            {
                                return u_modifikua;
                            }
                        }

                        if (!vjenNgaRiruajtja)
                        {
                            clsMesazh mesazhArkeBanke = krijoDokArkeBanke(faturaShitjeNgaUrdherShitjaMeKuponTatimor,
                                ref pagesefature, ref banka, isshitje, ref shfaqmesazhapolupebanka,
                                ref shfaqmesazhapolupeVDK, idPeriudha, dbRegj, dbshare);
                            if (!mesazhArkeBanke.Status)
                            {
                                return mesazhArkeBanke;
                            }
                        }

                        if (!string.IsNullOrEmpty(shitjemema.nrDok))
                        {
                            shitjemema.idTransferimi = this.idShitjeKoka;
                            shitjemema.oColTrupiShitje = this.oColTrupiShitje;
                            int idStatusEkzistues = kokaEkzistuesemema.IdStatusDok;

                            u_modifikua = pergatitTrupPerMeme(shitjemema, dbRegj, out colkonvertimi, 0, nivel.Kodi == "OB", idPeriudha, isshitje, false);
                            if (!u_modifikua.Status)
                            {
                                //dbRegj.rollbackTransaksion();
                                return u_modifikua;
                            }

                            if (!string.IsNullOrEmpty(kokaEkzistuesemema.nrDok))
                                u_modifikua = this.modifikoShitje(serverUrl, kokaEkzistuesemema, shitjemema, dbRegj,
                                    idPeriudha, colkonvertimi, nivel.Kodi == "FSH" || gjenerodokmag, skemaWorkFlow,
                                    statusapp, idetapa, out shfaqmesazhapolupemag, kontrollodisponibelmema, eshteOwn,
                                    serialet, konfigurimAmortizimi, isshitje, mekontabilizim, trupivjeterqendra,
                                    out shfaqmesazhapolupe, kodkonfigurimi, rm, cultinf, idGjuha, tollona, zevendesim,
                                    tollonakastrati, tollonakastratielektronik, zevendesimtollonakastrati, ruajrenditje,
                                    kontrolloSasiKonvertimiDheKthimi, kontrolloIMEIFifo, blerjengadealer, promocione, out mesazhmevonshem, modifikimAprovimi, kokaEkzistuese);
                            else
                                u_modifikua = this.ruajShitje(serverUrl, shitjemema,
                                    nivel.IdKategori != 1, false, idPeriudha, dbRegj, colkonvertimi,
                                    nivel.Kodi == "FSH" || gjenerodokmag, skemaWorkFlow, statusapp, idetapa,
                                    out shfaqmesazhapolupemag, new DbQendraKosto.colTrupiQendraKosto(),
                                    kontrollodisponibelmema, eshteOwn, serialet, konfigurimAmortizimi,
                                    new colAmortizimiKoka(), mekontabilizim, new DbQendraKosto.colTrupiQendraKosto(),
                                    out shfaqmesazhapolupe, kodkonfigurimi, 0, 0, 0, idGjuha, 0, tollona,
                                    zevendesim, tollonakastrati, tollonakastratielektronik, zevendesimtollonakastrati,
                                    ruajrenditje, kontrolloSasiKonvertimiDheKthimi, false, kontrolloIMEIFifo, blerjengadealer, promocione, out mesazhmevonshem, modifikimAprovimi,kokaEkzistuese);
                            if (!u_modifikua.Status)
                            {
                                return u_modifikua;
                            }

                            if (idStatusEkzistues == 4 && shitjemema.IdStatusDok == 1 && nivel.Kodi == "USH")
                            {
                                updateStatusDokumentashVartes(this.IdShitjeKoka, IdStatusDok);
                            }
                        }

                        if (HfArkiva != null)
                        {
                            u_modifikua = colArkiva.ModifikoArkiven(kokaEkzistuese.IdShitjeKoka, IdShitjeKoka,
                                isshitje ? 1 : 2, IdNdermarrje, idPerdoruesi);
                            if (!u_modifikua.Status)
                            {
                                return u_modifikua;
                            }
                        }
                    }
                    else if ((statusapp != StatusAprovimi.Per_Aprovim) && !(modifikimAprovimi && draftSDAF))
                    {
                        //eshte i lidhur
                        u_modifikua = kontrolloShitje(dbRegj, hfregjistrime);
                        if (!u_modifikua.Status)
                            return u_modifikua;

                        u_modifikua = clsKokaShitje.ShtoNeHistorik(kokaEkzistuese.IdShitjeKoka, idPerdoruesi, dbRegj);
                        if (!u_modifikua.Status)
                            return u_modifikua;
                        bool klientFiskalizimi = false;
                        if (clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizim())
                            klientFiskalizimi = true;
                        u_modifikua = dbRegj.modifikoKokaShitje(IdShitjeKoka, IdNivel, IdTemplate, IdKonfigAmbjente, IdKlientFurnitor, IdProjekt, NrProjekt, DtDok, NrDok, NrSerial, DtMaturimi, IdMonedha, Kursi, IdMenyreTransporti,
                            DtTransportimi, IdKushtDergimi, IdAgjent, IdMenyrePagese, IdKushtPagese, Zbritje, Totali, Tvsh, DtRegjistrimi, IdStatusDok, AdresaFaturimit, AdresaDergimit, Pershkrimi, Dogana, IdDegeAdministrative,
                            IdPikeShitjeFurnizimi, idPerdoruesi, idRaportDesing, IdGrup1, IdGrup2, IdGrup3, AfatKohor, Cash, statusAprovimi, idKrijuesi, PerqindjeAgjenti, idTransferimi, idKonfigTransferimi, emerKlienti, kontakti,
                            kase, kupon, DtFillimi, DtMbarimi, idAutomjet, kilometraAuto, idAgjenti2, perqindjeAgjenti2, idAgjenti3, perqindjeAgjenti3, marresi, idTransportues, ShpenzimeJoTeZbritshme, idarka, DtFature, idKarta, pike,
                            muajRaportimi, idVitRaportimi, idFaza, shoferi, targaSHF, zbritjeNeVlere, perqindjeZbritje, koordinata, niptKlienti, qytetiK, idKategoriSeriali, shenime2, kartaPaPagese, idDokTransferimNga, idLlojMarreveshje,
                            idMarreveshje, StatusMarreveshje, kerkuarNga, dateKerkese, nrDokMagazine, iic, nivf, idOperator, nivfKthim,eic,einStatus,procesi,eInvoiceType, klientFiskalizimi, tipiIVetefaturimit, clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizimV3());
                        if (!u_modifikua.Status)
                        {
                            return u_modifikua;
                        }

                        dbRegj.FshiKlienteFurnitoreVartes(IdShitjeKoka);
                        if (ColKlienteFurnitoreVartes.Count != 0)
                            dbRegj.RuajKlienteFurnitoreVartes(KrijoDataTableKlientFurnitorVartes(IdShitjeKoka, ColKlienteFurnitoreVartes));


                        if (this.nrDok != kokaEkzistuese.nrDok || this.DtDok != kokaEkzistuese.dtDok || this.nrDokMagazine != kokaEkzistuese.nrDokMagazine)//nqs dokumenti eshte i lidhur po kemi ndryshuar nr apo daten e dokumentit e levizim dhe tek dokumentat e tjere qe gjeneron shitja
                        {
                            if (this.nrDokMagazine != kokaEkzistuese.nrDokMagazine)
                            {
                                clsKokaMagazina kokaEkzistueseMag = new clsKokaMagazina
                                {
                                    OFleteKontabel = new clsKokaFleteKontabel()
                                };
                                kokaEkzistueseMag.mbushKokaMagazinaSipasIDGjenerues(kokaEkzistuese.IdShitjeKoka, isshitje ? 2 : 1, kokaEkzistuese.IdKonfigAmbjente, dbRegj);

                                if (kokaEkzistueseMag.IdKokaMagazina != 0 && dbRegj.ekzistonRegjistrimMagazineSipasIdentifikuese(kokaEkzistueseMag.IdKokaMagazina, kokaEkzistueseMag.IdKonfigAmbjente, this.nrDokMagazine, kokaEkzistueseMag.IdMagazina, this.DtDok, idNdermarje, kokaEkzistueseMag.IdNivel, idKlientFurnitor, idDegeAdministrative, idRaportDesing, idGrup1, idGrup2, idGrup3))
                                    return new clsMesazh(false, "Ekziston një regjistrim magazine me këto të dhëna identifikuese!");
                            }
                            u_modifikua = dbRegj.modifikoDokumentaVartesSeShitjesNRDokDheDtDok(this.IdShitjeKoka, this.IdNivel, this.IdKonfigAmbjente, this.dtDok, this.nrDok, rm, cultinf, this.nrDokMagazine);
                            if (!u_modifikua.Status)
                            {
                                //dbRegj.rollbackTransaksion();
                                return u_modifikua;
                            }
                        }

                        if (!string.IsNullOrEmpty(shitjemema.nrDok))
                        {
                            shitjemema.idTransferimi = this.idShitjeKoka;
                            klientFiskalizimi = false;
                            if (clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizim())
                                klientFiskalizimi = true;
                            u_modifikua = dbRegj.modifikoKokaShitje(kokaEkzistuesemema.IdShitjeKoka, shitjemema.IdNivel, shitjemema.IdTemplate, shitjemema.IdKonfigAmbjente, shitjemema.IdKlientFurnitor, shitjemema.IdProjekt, 
                                shitjemema.NrProjekt, shitjemema.DtDok, shitjemema.NrDok, shitjemema.NrSerial, shitjemema.DtMaturimi, shitjemema.IdMonedha, shitjemema.Kursi, shitjemema.IdMenyreTransporti, shitjemema.DtTransportimi,
                                shitjemema.IdKushtDergimi, shitjemema.IdAgjent, shitjemema.IdMenyrePagese, shitjemema.IdKushtPagese, shitjemema.Zbritje, shitjemema.Totali, shitjemema.Tvsh, shitjemema.DtRegjistrimi, 
                                shitjemema.IdStatusDok, shitjemema.AdresaFaturimit, shitjemema.AdresaDergimit, shitjemema.Pershkrimi, shitjemema.Dogana, shitjemema.IdDegeAdministrative, shitjemema.IdPikeShitjeFurnizimi, 
                                shitjemema.idPerdoruesi, shitjemema.idRaportDesing, shitjemema.IdGrup1, shitjemema.IdGrup2, shitjemema.IdGrup3, shitjemema.AfatKohor, shitjemema.Cash, shitjemema.statusAprovimi, shitjemema.idKrijuesi, 
                                shitjemema.PerqindjeAgjenti, shitjemema.idTransferimi, shitjemema.idKonfigTransferimi, shitjemema.emerKlienti, shitjemema.kontakti, shitjemema.kase, shitjemema.kupon, shitjemema.DtFillimi,
                                shitjemema.DtMbarimi, shitjemema.idAutomjet, shitjemema.kilometraAuto, shitjemema.idAgjenti2, shitjemema.perqindjeAgjenti2, shitjemema.idAgjenti3, shitjemema.perqindjeAgjenti3, shitjemema.marresi,
                                shitjemema.idTransportues, this.ShpenzimeJoTeZbritshme, shitjemema.idarka, shitjemema.DtFature, shitjemema.idKarta, shitjemema.pike, shitjemema.muajRaportimi, shitjemema.idVitRaportimi, shitjemema.idFaza,
                                shitjemema.shoferi, shitjemema.targaSHF, shitjemema.zbritjeNeVlere, shitjemema.perqindjeZbritje, shitjemema.koordinata, shitjemema.niptKlienti, shitjemema.qytetiK, shitjemema.idKategoriSeriali, 
                                shitjemema.shenime2, shitjemema.kartaPaPagese, shitjemema.idDokTransferimNga, shitjemema.idLlojMarreveshje, shitjemema.idMarreveshje, shitjemema.StatusMarreveshje, shitjemema.kerkuarNga, shitjemema.dateKerkese,
                                shitjemema.nrDokMagazine, shitjemema.iic, shitjemema.nivf, shitjemema.idOperator, shitjemema.nivfKthim,shitjemema.eic,shitjemema.einStatus,shitjemema.procesi,shitjemema.eInvoiceType, klientFiskalizimi,shitjemema.tipiIVetefaturimit, clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizimV3());
                            if (!u_modifikua.Status)
                            {
                                return u_modifikua;
                            }

                            dbRegj.FshiKlienteFurnitoreVartes(kokaEkzistuese.IdShitjeKoka);
                            if (shitjemema.ColKlienteFurnitoreVartes.Count != 0)
                                dbRegj.RuajKlienteFurnitoreVartes(KrijoDataTableKlientFurnitorVartes(kokaEkzistuesemema.IdShitjeKoka, shitjemema.ColKlienteFurnitoreVartes));
                        }

                        if (!vjenNgaRiruajtja && vleraMbetur != 0) //ka vlere te mbetur
                        {
                            double vlera = this.totali;
                            this.totali = vleraMbetur + zbritje;
                            clsMesazh mesazhArkeBanke = krijoDokArkeBanke(faturaShitjeNgaUrdherShitjaMeKuponTatimor,
                                ref pagesefature, ref banka, isshitje, ref shfaqmesazhapolupebanka,
                                ref shfaqmesazhapolupeVDK, idPeriudha, dbRegj, new clsDatabaseShare(dbAdmin));

                            if (!mesazhArkeBanke.Status)
                            {
                                return mesazhArkeBanke;
                            }

                            this.totali = vlera;
                        }
                    }

                    if (modifikimAprovimi)
                    {
                        u_modifikua = new clsEtapeAprovimi().ruaj(idPerdoruesi, serverUrl, skemaWorkFlow, dbRegj,
                            kokaEkzistuese.IdShitjeKoka, this.IdPerdoruesi, statusapp, idetapa, isshitje ? 1 : 2,
                            this.idStatusDok, this.idNdermarje, this.statusAprovimi,
                            this.idKlientFurnitor, this.idShitjeKoka, this.idPerdoruesi,
                            this.totali - this.tvsh - this.zbritje,
                            this.dtKrijimi, this.idKonfigAmbjente, this.nrDok,
                            this.dtDok);

                        if (!u_modifikua.Status)
                        {
                            return u_modifikua;
                        }
                    }

                    if (vjenNgaImportSQL && idDokImport != string.Empty)
                    {
                        clsMesazh mesazh = updateStatusImporti(idDokImport, false, dbRegj, emerTabKoka, primaryKeyEmerFushe, emerFusheNdermarrje, idNdermarje);

                        if (!mesazh.Status)
                        {
                            return mesazh;
                        }
                    }

                    if (eshteOwn && isshitje && idDokTransferimNga != 0 && idStatusDok == 1) // momenti kur ruhet nga magazinieri dok fshmag draft i ardhur nga mema
                    {
                        u_modifikua = dbRegj.modifikoKokaShitjeStatusTransferimi(this.idShitjeKoka, StatusTrasferimi.Transferuar);
                    }

                    if (idStatusDok == 8) // kur pezullohet ky dok, duhet te pezullohet dhe dok nga i cili eshte konvertuar
                        u_modifikua = updateStatusTeDokKonvertuarNga(idShitjeKoka, 8);

                    if (!u_modifikua.Status)
                    {
                        return u_modifikua;
                    }

                    scope.Complete();
                }

                if (dergoEmail)
                {
                    EmailComposer.dergoEmailFaturen(idGjuha, idPerdoruesi, idNdermarjeVit, idNdermarje, idShitjeKoka, IdKlientFurnitor, NrDok, dtDok);
                    //to do kevi dergo email nga emaili i konfiguruar tek ambjenti i konfigurimit te emailit tek emaili i tabeles t_klientfurnitor per klientit e fatures  me tekst Porosia juaj nr xxx, dt 0x/0x/201x u modifikua
                }

                if (dergoemailVFOne)
                {
                    clsMesazh msgDergoEmailVfOne = EmailComposer.dergoEmailRezervimVodOne(idNdermarje, idPerdoruesi, this.oColTrupiShitje, NrDok, DtDok, this.IdKonfigAmbjente, true, false);
                    if (!msgDergoEmailVfOne.Status)
                        ImbLogger.LogErrorPromocione(string.Format("Ndodhi nje gabim gjate dergimit te email dergoEmailRezervimVodOne idNdermarje: {0}, perdoruesi: {1}, nrdok: {2}, dtdok: {3}, IdKonfigAmbjente: {4}, dergoEmailVfOne : {5}, dergoEmailPorosi:{6} ", idNdermarje, idPerdoruesi, NrDok, DtDok, IdKonfigAmbjente), msgDergoEmailVfOne.PershkrimMesazhi, true, false);
                }

                if (clsAlternativaKushti.getAlternativa(this.IdKonfigAmbjente, "DEPOROSI") == "Po")
                {
                    clsMesazh msgDergoEmailVfOne = EmailComposer.dergoEmailRezervimVodOne(idNdermarje, idPerdoruesi, this.oColTrupiShitje, NrDok, DtDok, this.IdKonfigAmbjente, false, true);
                    if (!msgDergoEmailVfOne.Status)
                        ImbLogger.LogErrorPromocione(string.Format("Ndodhi nje gabim gjate dergimit te email dergoEmailRezervimVodOne idNdermarje: {0}, perdoruesi: {1}, nrdok: {2}, dtdok: {3}, IdKonfigAmbjente: {4}, dergoEmailVfOne : {5}, dergoEmailPorosi:{6} ", idNdermarje, idPerdoruesi, NrDok, DtDok, IdKonfigAmbjente), msgDergoEmailVfOne.PershkrimMesazhi, false, true);
                }

                if (u_modifikua.PershkrimMesazhi == "Modifikimi përfundoi me sukses!")
                    u_modifikua.PershkrimMesazhi = rm.GetString("msgModifikimiMeSukses", cultinf);

                return u_modifikua;
            }

            catch (System.Data.SqlClient.SqlException ex)
            {
                ImbLogger.LogErrorShitje($"Exception {ex}");
                ImbLogger.Error(ex);
                u_modifikua = new clsMesazh(false, ex.Number == 15600 ? "Ekziston nje dokument me keto te dhena identifikuese!" : ex.Message);
                ImbLogger.LogTraceShitje($"Mbaroi clsMesazh modifiko me parametra idGjuha:{idGjuha}, serverUrl:" + serverUrl + $", lidhur:{lidhur}, idPeriudha:{idPeriudha}, colkonvertimi:{JsonConvert.SerializeObject(colkonvertimi)}, gjenerodokmag:{gjenerodokmag}, skemaWorkFlow:{skemaWorkFlow}, idetapa:{idetapa}, shitjemema:{JsonConvert.SerializeObject(shitjemema)}, dergoEmail:{dergoEmail}, eshteOwn:{eshteOwn}, dergoemailVFOne:{dergoemailVFOne}, serialet:{JsonConvert.SerializeObject(serialet)}, konfigurimAmortizimi:{JsonConvert.SerializeObject(konfigurimAmortizimi)}, isshitje:{isshitje}, faturaShitjeNgaUrdherShitjaMeKuponTatimor:{JsonConvert.SerializeObject(faturaShitjeNgaUrdherShitjaMeKuponTatimor)}, mekontabilizim:{mekontabilizim}, kodkonfigurimi:" + kodkonfigurimi + $", ruajrenditje:{ruajrenditje}, kontrolloSasiKonvertimiDheKthimi:{kontrolloSasiKonvertimiDheKthimi}, vjenNgaRiruajtja:{vjenNgaRiruajtja}, kontrolloIMEIFifo:{kontrolloIMEIFifo}, blerjengadealer:{blerjengadealer}, promocione:{promocione}, idDokImport:" + idDokImport + $",emerTabKoka:" + emerTabKoka + $", primaryKeyEmerFushe:" + primaryKeyEmerFushe + $", emerFusheNdermarrje:" + emerFusheNdermarrje);
                return u_modifikua;
            }

            catch (Exception ex)
            {
                ImbLogger.LogErrorShitje($"Exception {ex}");
                ImbLogger.Error(ex);
                u_modifikua = new clsMesazh(false, ex.Message);
                ImbLogger.LogTraceShitje($"Mbaroi clsMesazh modifiko me parametra idGjuha:{idGjuha}, serverUrl:" + serverUrl + $", lidhur:{lidhur}, idPeriudha:{idPeriudha}, colkonvertimi:{JsonConvert.SerializeObject(colkonvertimi)}, gjenerodokmag:{gjenerodokmag}, skemaWorkFlow:{skemaWorkFlow}, idetapa:{idetapa}, shitjemema:{JsonConvert.SerializeObject(shitjemema)}, dergoEmail:{dergoEmail}, eshteOwn:{eshteOwn}, dergoemailVFOne:{dergoemailVFOne}, serialet:{JsonConvert.SerializeObject(serialet)}, konfigurimAmortizimi:{JsonConvert.SerializeObject(konfigurimAmortizimi)}, isshitje:{isshitje}, faturaShitjeNgaUrdherShitjaMeKuponTatimor:{JsonConvert.SerializeObject(faturaShitjeNgaUrdherShitjaMeKuponTatimor)}, mekontabilizim:{mekontabilizim}, kodkonfigurimi:" + kodkonfigurimi + $", ruajrenditje:{ruajrenditje}, kontrolloSasiKonvertimiDheKthimi:{kontrolloSasiKonvertimiDheKthimi}, vjenNgaRiruajtja:{vjenNgaRiruajtja}, kontrolloIMEIFifo:{kontrolloIMEIFifo}, blerjengadealer:{blerjengadealer}, promocione:{promocione}, idDokImport:" + idDokImport + $",emerTabKoka:" + emerTabKoka + $", primaryKeyEmerFushe:" + primaryKeyEmerFushe + $", emerFusheNdermarrje:" + emerFusheNdermarrje);
                return u_modifikua;
            }
        }

        /// <summary>
        /// Riruan dokumentin e shitjes
        /// </summary>
        /// <param name="idNdermarrje">id e ndermarrjes ku po kryhet veprimi</param>
        /// <param name="idPerdoruesi">id e perdoruesit qe po kryen veprimin</param>
        /// <param name="konfig">konfigurimi i dokumentit te shitjes</param>
        /// <param name="idGjuha">id e gjuhes</param>
        /// <param name="eshteMeme">eshte ndermarrje meme</param>
        /// <param name="eshteOwn">eshte ndermarrje own</param>
        /// <param name="rm">Resource Manager</param>
        /// <param name="ci">Culture Info</param>
        /// <param name="hfArkiva">hiddenfield i arkives</param>
        /// <param name="serverUrl">url serveri</param>
        /// <returns>objekt mesazhi</returns>
        public clsMesazh Riruaj(bool ngaAprovimi, int idNdermarrje, int idPerdoruesi, clsKonfigurimAmbjenti konfig, int idGjuha, bool eshteMeme, bool eshteOwn, ResourceManager rm, CultureInfo ci, IDictionary<string, object> hfArkiva, string serverUrl)
        {

            clsKusht kushtskema = new clsKusht(this.IdKonfigAmbjente, "ZSP");
            if (!ngaAprovimi && ((this.StatusAprovimi == StatusAprovimi.Undefined && kushtskema.Vlera != 0) || (this.StatusAprovimi != StatusAprovimi.Undefined && this.StatusAprovimi != StatusAprovimi.Aprovuar)))
                return new MesazhGabimi("Nuk u riruajt pasi eshte ne proces aprovimi!");

            colTrupiShitje col = new colTrupiShitje();
            clsKokaMagazina kokam = new clsKokaMagazina();
            int idKategoria = clsKonfigurimAmbjenti.ktheIdKategori(this.IdKonfigAmbjente);
            kokam.mbushKokaMagazinaSipasIDGjenerues(this.IdShitjeKoka, idKategoria == 1 ? 2 : 1, this.IdKonfigAmbjente);
            colArtikujt colart = new colArtikujt(this.IdShitjeKoka);
            colLlogarite colllog = new colLlogarite(this.IdShitjeKoka);
            bool ruajBarkod = clsAlternativaKushti.getAlternativa(this.IdKonfigAmbjente, "RBART") == "Po";
            col.mbushGjitheTrupiShitjeNgaKoka(this.IdShitjeKoka);
            int count = 0; bool eshteDokKthimi = false;
            foreach (clsTrupiShitje trup in col)
            {
                if (!ruajBarkod)
                {
                    trup.IdBarkodi = 0;
                }
                if (trup.IdTrupiKthim != 0)
                    eshteDokKthimi = true; //mjafton te jete nje rresht me idtrupikthimi te ndryshme nga zero.
                if (trup.IdLlojVeprimi == 1)
                    trup.Element = colart[count];
                else trup.Element = colllog[count];
                count++;
            }

            colSerialetMagazine colseriale = new DbCore.DbAsete.colSerialetMagazine();
            colseriale.merrSerialetMagazineSipasIDDokumenti(kokam.IdKokaMagazina, idNdermarrje, kokam.IdKonfigAmbjente);
            foreach (clsSerialetMagazine s in colseriale)
            {
                s.IdStatusDokumenti = 1;
            }
            for (var i = 0; i < col.Count; i++)
            {
                if ((col[i].Element).GetType() == typeof(clsArtikulli))
                {
                    clsArtikulli art = (clsArtikulli)col[i].Element;
                    if (!art.Aktiv)
                        return new MesazhGabimi("Artikulli me kod:" + art.KodArtikulli + " nuk eshte aktiv!");
                }
            }
            colSerialeUnikeMagazina serialeUnike = new colSerialeUnikeMagazina(kokam.IdKokaMagazina, idNdermarrje);

            colAdresatKlientFurnitor adresat = new colAdresatKlientFurnitor();
            if (this.IdKlientFurnitor != 0)
            {
                adresat = new colAdresatKlientFurnitor(this.IdKlientFurnitor);
                if (adresat.Count > 0)
                {
                    if (adresat[0].IdTipAdrese == 1 && this.AdresaFaturimit == "")    //adresa biznesi
                        this.AdresaFaturimit = adresat[0].Adresa.ToString();
                    if (adresat[0].IdTipAdrese == 2 && this.AdresaDergimit == "")    //adrese magazine                                                 
                        this.AdresaDergimit = adresat[0].Adresa.ToString();
                }
            }
            colKonvertimi colKonvertime = new colKonvertimi(this.IdShitjeKoka);

            int idPeriudheKontabel = clsPeriudhaKontabel.ktheIdPeriudheSipasDatesDheNdermarrjes(this.DtDok, idNdermarrje);
            bool lidhur = this.eshteILidhur();

            colGaranciArtikulli garanci = new colGaranciArtikulli(this.IdShitjeKoka);
            // eshte shtuar qe te mos modifikohet trupi i nje dokumenti per te cilin jane printuar garancite
            if (!lidhur && garanci.Count > 0)
                lidhur = true;
            if (this.FaturePermbledhese && this.IdStatusDok != 0)//nqs eshte fature permbledhese e ruajtur si draft, atehere vjen nga importi si fature permbledhese
                lidhur = true;
            if (!ngaAprovimi && this.StatusAprovimi != StatusAprovimi.Undefined && this.StatusAprovimi != StatusAprovimi.Aprovuar)
                lidhur = true;
            if (lidhur)
            {
                DataTable dtlidhur = this.merrIdsDokLidhur();
                if (dtlidhur.Columns.Contains("tipi") && dtlidhur.Rows.Count == 1)
                    if (Convert.ToString(dtlidhur.Rows[0]["tipi"]) == "transferuarngamema")
                        lidhur = false;
            }
            clsKokaRezervime rez = new clsKokaRezervime();
            rez.mbushKokaRezervimiSipasIDGjenerues(kokam.IdKokaMagazina, 2, kokam.IdKonfigAmbjente);
            clsKokaFleteKontabel kokfk = new clsKokaFleteKontabel(this.IdShitjeKoka, idKategoria);
            DbCore.DbQendraKosto.clsKokaQendraKosto qend = new DbCore.DbQendraKosto.clsKokaQendraKosto();
            qend.KtheKokaQKSipasIDGjeneruesDheKonfigMeTrup(kokfk.IdKokaFleteKontabel, kokfk.IdKonfigAmbjente);
            #region validime

            if (this.IdMonedha == 0)
                return new MesazhGabimi(MessagesResource.Messages["msgZgjidhniNjeMonedhe"]);

            if (this.Kursi == 0)
                return new MesazhGabimi(MessagesResource.Messages["msgShenoniKursin"]);

            clsNivelRegjistrimi niv = new clsNivelRegjistrimi();
            niv.IdNivel = this.IdNivel;
            niv.merrNivelRegjSipasId();
            if ((this.IdMenyrePagese == 5) && (niv.Pershkrimi != "Fature shitje" && niv.Pershkrimi != "Fature blerje"))
                return new MesazhGabimi(MessagesResource.Messages["msgNukMundTeBeniPageseAutomatikePerDokUrdher"]);

            if (this.IdGrup1 != 0)
            {
                clsGrupimDokumentiKoka grup = new clsGrupimDokumentiKoka(this.IdGrup1);
                if (grup.IdGrupimKoka < 1)
                    return new MesazhGabimi(MessagesResource.Messages["msgGrupimiIPareNukEkziston"]);
            }
            if (this.IdGrup2 != 0)
            {
                clsGrupimDokumentiKoka grup = new clsGrupimDokumentiKoka(this.IdGrup2);
                if (grup.IdGrupimKoka < 1)
                    return new MesazhGabimi(MessagesResource.Messages["msgGrupimiDyteNukEkziston"]);
            }
            if (this.IdGrup3 != 0)
            {
                clsGrupimDokumentiKoka grup = new clsGrupimDokumentiKoka(this.IdGrup3);
                if (grup.IdGrupimKoka < 1)
                    return new MesazhGabimi(MessagesResource.Messages["msgGrupimiITreteNukEkziston"]);
            }

            Dictionary<string, object> hf = null;

            if (this.Kupon && string.IsNullOrEmpty(this.NrSerial))
            {
                var idNrAutoNrSerial = clsAtributeTrupi.merrNrAutomatikSipasKontrollitDheKonfigurimit(this.IdKonfigAmbjente, "txtNumerSerial", 506);
                var nrSerial = clsNrAutom.merrVlerenNrAutomatik(idNrAutoNrSerial, this.DtDok);
                if (!string.IsNullOrEmpty(nrSerial))//nqs ka nr automatik
                {
                    var nrAuto = new NrAuto
                    {
                        kodKontrolli = "txtNumerSerial",
                        idNrAuto = idNrAutoNrSerial,
                        isModified = false,
                        vlereNrAuto = nrSerial
                    };

                    var serializusi = new JavaScriptSerializer { MaxJsonLength = 50000000 };
                    hf = new Dictionary<string, object> { { "txtNumerSerial", serializusi.Serialize(nrAuto) } };
                    this.NrSerial = nrSerial;
                }
            }

            if (this.Kupon && this.Kase && this.NrSerial == "")
                return new MesazhGabimi(MessagesResource.Messages["msgVendosniNjeSerialFaturePerKuponatMeFatureTatimore"]);

            #endregion
            #region krijimi i kokes se re
            clsKokaShitje kokare = new clsKokaShitje();
            bool gjeneroDokMag = clsAlternativaKushti.getAlternativa(this.IdKonfigAmbjente, "GJDM") == "Po";
            bool meKontabilizim = clsAlternativaKushti.getAlternativa(this.IdKonfigAmbjente, "GJK") != "Jo";
            bool gjeneromeme = clsAlternativaKushti.getAlternativa(this.IdKonfigAmbjente, "GJOSHM") == "Po";
            bool gjenerobij = false;
            if (clsAlternativaKushti.getAlternativa(this.IdKonfigAmbjente, "GJUBB") == "Po")
                gjenerobij = true;
            else if (clsAlternativaKushti.getAlternativa(this.IdKonfigAmbjente, "GJFBB") == "Po")
                gjenerobij = true;

            string kodklienti = "", kodmonedhe = "", kodmenyretras = "", kodkushtderg = "", kodagjenti1 = "", kodkushtepages = "", koddege = "", kodpike = "", kodmagazina = "", kodgrup1 = "", kodagjenti2 = "", kodagjenti3 = "", kodtrasportuesi = "";
            if (this.IdKlientFurnitor != 0)
            {
                clsKlientFurnitor kf = new clsKlientFurnitor(this.IdKlientFurnitor);
                kodklienti = kf.KodKlientFurnitor;
                if (new clsKlientFurnitor(kodklienti, idNdermarrje, IdPerdoruesi).IdKlientFurnitor < 1)
                    return new MesazhGabimi("Ju nuk keni autorizime per kete klient/furnitor!");

                bool limitikf = false;

                if (clsAlternativaKushti.getAlternativa(this.IdKonfigAmbjente, "KKLKF") == "Po")
                    limitikf = true;

                if (limitikf && kf.LimitBllokues > 0 && (this.IdStatusDok == 0 && this.TotaliMeZbritjeMeTVSH + Convert.ToDouble(clsKlientFurnitor.MerrDetyrimKf(kf.IdKlientFurnitor, this.DtDok)) > kf.LimitBllokues))
                    return new MesazhGabimi(MessagesResource.Messages["msgTotalFatureKaluarLimitBllokues"]);
            }

            if (this.IdMonedha != 0)
            {
                kodmonedhe = clsMonedha.ktheKodMonedheSipasId(this.IdMonedha);
            }
            if (this.IdMenyreTransporti != 0)
            {
                clsMenyreTransporti menyretrans = new DbCore.DbAdmin.clsMenyreTransporti(this.IdMenyreTransporti);
                kodmenyretras = menyretrans.KodiMenyreTransporti;
            }
            if (this.IdKushtDergimi != 0)
            {
                clsKushtDergimi kushtderg = new DbCore.DbAdmin.clsKushtDergimi(this.IdKushtDergimi);
                kodkushtderg = kushtderg.KodiKushtDergimi;
            }
            if (this.IdAgjent != 0)
            {
                clsAgjentShitje agjenti = new DbCore.DbAdmin.clsAgjentShitje(this.IdAgjent);
                kodagjenti1 = agjenti.KodiAgjentShitje;
            }
            string menyrepag = "";
            menyrepag = clsFunksione.ktheMenyrePageseSipasID(this.IdMenyrePagese);
            if (this.IdKushtPagese != 0)
            {
                clsKushtPageseKoka kushtpagese = new clsKushtPageseKoka(this.IdKushtPagese);
                kodkushtepages = kushtpagese.KodiKushtPagese;
            }
            if (this.IdDegeAdministrative != 0)
            {
                clsDegeAdministrative dege = new clsDegeAdministrative(this.IdDegeAdministrative);
                koddege = dege.Kodi;
            }
            if (this.IdPikeShitjeFurnizimi != 0)
            {
                clsPikeShitjeFurnizimi pike = new clsPikeShitjeFurnizimi(this.IdPikeShitjeFurnizimi);
                kodpike = pike.Kodi;
            }

            clsKonfigurimAmbjenti konfmag = new clsKonfigurimAmbjenti(konfig.IdKonfigurimi, idGjuha);
            if (kokam.IdMagazina != 0)
            {
                clsNjesiAdministrative magazina = new clsNjesiAdministrative(kokam.IdMagazina);
                kodmagazina = magazina.Kodi;
            }
            string shfaqmesazhapolupe = "Jo", mesazhinformues = "", shfaqmesazhapolupemagazina = "Jo";
            string llojZevendesimi = clsAlternativaKushti.getAlternativa(konfig.IdKonfigAmbjente, "ZT");
            clsKokaShitje kokaMema = new clsKokaShitje();
            if (this.IdGrup1 != 0)
            {
                clsGrupimDokumentiKoka grup1 = new clsGrupimDokumentiKoka(this.IdGrup1);
                kodgrup1 = grup1.Kodi;
            }
            if (this.IdAgjenti2 != 0)
            {
                clsAgjentShitje agjent2 = new clsAgjentShitje(this.IdAgjenti2);
                kodagjenti2 = agjent2.KodiAgjentShitje;
            }
            if (this.IdAgjenti3 != 0)
            {
                clsAgjentShitje agjent3 = new clsAgjentShitje(this.IdAgjenti3);
                kodagjenti3 = agjent3.KodiAgjentShitje;
            }
            if (this.IdTransportues != 0)
            {
                clsTransportues tranportues = new clsTransportues(this.IdTransportues);
                kodtrasportuesi = tranportues.Emertimi;
            }
            clsKokaShitje faturashitjengaurdhershitjamekupontatimor = lidhur ? null : new clsKokaShitje();
            bool tollona = false;

            if (clsAlternativaKushti.getAlternativa(this.IdKonfigAmbjente, "RSHTT") == "Po")
                tollona = true;
            bool tollonakastrati = false;

            if (clsAlternativaKushti.getAlternativa(this.IdKonfigAmbjente, "RSHTTK") == "Po")
                tollonakastrati = true;
            bool tollonakastratielektronik = false;

            if (clsAlternativaKushti.getAlternativa(this.IdKonfigAmbjente, "RSHTTKE") == "Po")
                tollonakastratielektronik = true;
            bool autoklient = false;

            if (clsAlternativaKushti.getAlternativa(this.IdKonfigAmbjente, "LAVK") == "Po")
                autoklient = true;

            colFazaKontrate ocolFaza = new colFazaKontrate(idNdermarrje, this.IdShitjeKoka);
            var merrMagazinePerberesi = clsAlternativaKushti.getAlternativa(this.IdKonfigAmbjente, "KGJAPMR") == "Po";
            clsMesazh mesazh = kokare.krijoShitje(ref gjeneroDokMag, IdNivel, IdTemplate, IdKonfigAmbjente, IdKlientFurnitor, kodklienti, IdProjekt, NrProjekt, DtDok, NrDok, NrSerial, DtMaturimi, IdMonedha, kodmonedhe,
                Kursi, IdMenyreTransporti, kodmenyretras, DtTransportimi, IdKushtDergimi, kodkushtderg, IdAgjent, kodagjenti1, IdMenyrePagese, menyrepag, IdKushtPagese, kodkushtepages, Zbritje, Totali, Tvsh, DtRegjistrimi,
                1, IdNdermarrje, IdNdermarrjeVit, IdNivelGjenerues, IdKonfigGjenerues, IdGjenerues, IdDokNga, AdresaFaturimit, AdresaDergimit, Pershkrimi, Dogana, IdDegeAdministrative, koddege, IdPikeShitjeFurnizimi, kodpike, 
                idPerdoruesi, IdRaportDesing, col, idKategoria == 1 ? true : false, konfig.KodKonfigAmbjente, kokfk.IdPeriudha > 0 ? kokfk.IdPeriudha : idPeriudheKontabel, konfmag, kokam.IdMagazina, kodmagazina, meKontabilizim, 
                IdGrup1, IdGrup2, IdGrup3, AfatKohor, Cash, StatusAprovimi, IdKrijuesi, PerqindjeAgjenti, out shfaqmesazhapolupe, hfArkiva, qend.ColTrupi, rez.IdKokaRezervimi, out mesazhinformues, gjeneromeme, kokaMema, 
                IdTransferimi, IdKonfigTransferimi, eshteMeme, gjenerobij, StatusTransferimi, EmerKlienti, Kontakti, Kase, Kupon, kodgrup1, DtFillimi, DtMbarimi, IdAutomjet, KilometraAuto, Targa, IdAgjenti2, PerqindjeAgjenti2, 
                kodagjenti2, IdAgjenti3, PerqindjeAgjenti3, kodagjenti3, Marresi, IdTransportues, kodtrasportuesi, FaturePermbledhese, faturashitjengaurdhershitjamekupontatimor, ShpenzimeJoTeZbritshme, IdArka, false, tollona, 
                autoklient, false, DtFature, Gjeneruar, tollonakastrati, tollonakastratielektronik, MuajRaportimi, IdVitRaportimi, Shoferi, TargaShoferit, ZbritjeNeVlere, PerqindjeZbritje, IdKarta, Pike, ocolFaza, 0,
                ColKlienteFurnitoreVartes, DateTime.Now, new DbData(), llojZevendesimi, Koordinata, false, false, idGjuha, new clsKonfigurimAmbjenti(), IdStatusDok, NiptK, QytetiK, false, IdKategoriSeriali, serialeUnike, Shenime2,
                KartaPaPagese, IdDokTransferimNga, eshteDokKthimi, IdLlojMarreveshje, IdMarreveshje, StatusMarreveshje, KerkuarNga, "shtim", DateKerkese, merrMagazinePerberesi, NrDokMagazine, iic, nivf, idOperator, nivfKthim, eic, einStatus, procesi, eInvoiceType, tipiIVetefaturimit);
            if (!mesazh.Status)
                return mesazh;

            kokare.IdShitjeKoka = this.IdShitjeKoka;
            #endregion

            int idskema = kushtskema.Vlera;
            if (this.StatusAprovimi == StatusAprovimi.Undefined)
                idskema = 0;

            int idetapa = 0;

            //etapa e fundit kur hapet nga shitja
            clsEtapeAprovimi etapafund = new clsEtapeAprovimi();
            etapafund.ktheEtapeFunditSipasKokaShitjeDhePerdorues(this.IdShitjeKoka, idPerdoruesi);
            idetapa = etapafund.IdEtapa;

            bool dergoemail = (clsAlternativaKushti.getAlternativa(this.IdKonfigAmbjente, "LE") == "Po");
            bool dergoemailVFOne = (clsAlternativaKushti.getAlternativa(this.IdKonfigAmbjente, "DEVFOne") == "Po");

            bool zevendesimtollona = false;
            if (!(llojZevendesimi == "Jo"))
                zevendesimtollona = true;
            bool zevendesimtollonakastrati = false;

            //if (altzevtollona.Alternativa == "Po")
            if (clsAlternativaKushti.getAlternativa(konfig.IdKonfigAmbjente, "ZTK") == "Po")
                zevendesimtollonakastrati = true;
            clsKusht kushtamor = new clsKusht(this.IdKonfigAmbjente, "ZDAM");
            bool printofature = false, printogarancifature = false;
            bool kontrolloSasiKonvertimiDheKthimi = false;
            if (clsAlternativaKushti.getAlternativa(this.IdKonfigAmbjente, "NK") == "Po")
                kontrolloSasiKonvertimiDheKthimi = true;
            bool kontrolloIMEIFifo = false;

            if (clsAlternativaKushti.getAlternativa(this.IdKonfigAmbjente, "AFI") == "Po")
                kontrolloIMEIFifo = true;
            clsKonfigurimAmbjenti konfamortizimi = new clsKonfigurimAmbjenti(kushtamor.Vlera, idGjuha);
            kokare.DtKrijimiPajisje = this.DtKrijimiPajisje;

            string mesazhmevonshem;
            DbData dbData = new DbData();
            mesazh = kokare.modifiko(idGjuha, serverUrl, lidhur, hf, idPeriudheKontabel, colKonvertime, gjeneroDokMag, idskema, kokare.StatusAprovimi, idetapa, out shfaqmesazhapolupemagazina, kokaMema, dergoemail, eshteOwn, dergoemailVFOne, colseriale, konfamortizimi, (idKategoria == 1) ? true : false, faturashitjengaurdhershitjamekupontatimor, out printofature, out printogarancifature, out shfaqmesazhapolupe, meKontabilizim, qend.ColTrupi, konfig.KodKonfigAmbjente, rm, ci, tollona, zevendesimtollona, tollonakastrati, tollonakastratielektronik, zevendesimtollonakastrati, true, kontrolloSasiKonvertimiDheKthimi, true, kontrolloIMEIFifo, false, false, ref dbData, out mesazhmevonshem, false, "", "", "", "");
            return mesazh;
        }

        /// <summary>
        /// Kontrollo shitje per modifikim per dokumenta jo te lidhur
        /// </summary>
        /// <param name="dbRegj"></param>
        /// <param name="hfregjistrime"></param>
        /// <param name="modifikim"></param>
        /// <param name="kokaEkzistuese">Mbushet i njejti dok nga db-ja per te pare nese ka ndryshuar nderkohe apo jo. Ky kontroll behet vetem kur eshte modifikim dhe jo kur eshte dok i lidhur</param>
        /// <returns></returns>
        private clsMesazh kontrolloShitje(clsDatabaseRegjistrim dbRegj, IDictionary<string, object> hfregjistrime, clsKokaShitje kokaEkzistuese)
        {
            bool kaNdryshimNrAuto;
            return kontrolloShitje(out kaNdryshimNrAuto, dbRegj, hfregjistrime, true, kokaEkzistuese);
        }

        /// <summary>
        /// Kontrollo shitje per modifikim, per dokumentat  e lidhur
        /// </summary>
        /// <param name="dbRegj"></param>
        /// <param name="hfregjistrime"></param>
        /// <param name="modifikim"></param>        
        /// <returns></returns>
        private clsMesazh kontrolloShitje(clsDatabaseRegjistrim dbRegj, IDictionary<string, object> hfregjistrime)
        {
            bool kaNdryshimNrAuto;
            return kontrolloShitje(out kaNdryshimNrAuto, dbRegj, hfregjistrime, true, true);
        }

        /// <summary>
        /// Modifikon nje objekt dokument shitje sebashku me te trupin, dokumentin e magazines dhe kontabilitetin
        /// Nje objekt dokument shitje ka nje koleksion me trupin, dokumentin e magazines dhe kontabilitetin perkates , 
        /// modifikimi e nje dokumenti imponon modifikimin edhe te nje colection-i me trupin, dokumentit te magazines dhe kontabilitetin
        /// Mqs cdo rresht i ri qe modifikon ne DB kerkon thirrjen e nje SP-je me parametra dhe dokumenti i shitjes bashke me trupin, dokumentin e magazines dhe kontabilitetin konsiderohet si nje regjistrim,
        /// perdoret nje transaksion qe imponon modifikimin e rregullt te nje te nje dokumenti shitje sebashku me trupin, dokumentin e magazines dhe kontabilitetin
        /// 1. merret dokumenti eksistues i shitjes dhe i magazines dhe kalohen ne gjendjen 20 te modifikimit
        /// 2. ruhet dokumenti i ri i shitjes se bashku me trupin, dokumentin e magazines dhe kontabilitetin
        /// 3. stornohen kontabiliteti i dokumentave eksistues te shitjes dhe te magazines
        /// </summary>
        /// <param name="serverUrl"></param>
        /// <param name="koka"> koka e dokumentit te shitjes qe do modifikohet</param>
        /// <returns> kthen nje obj clsMesazh per te identifikuar statusin e modifikimit se te dhenave ne DB</returns>
        /// 
        clsKokaRezervime kokaeksistuezerez = new clsKokaRezervime();

        public clsMesazh modifikoShitje(string serverUrl, clsKokaShitje kokaEkzistuese, clsKokaShitje koka, clsDatabaseRegjistrim dbRegj, int idPeriudha, colKonvertimi colkonvertimi, bool gjenerodokmag, int skemaWorkFlow, DbRegjistrim.StatusAprovimi statusapp, int idetapa, out string shfaqmesazhapolupemag, bool kontrollodisponibel, bool eshteOwn, colSerialetMagazine serialet, clsKonfigurimAmbjenti konfigurimAmortizimi, bool isshitje, bool mekontabilizim, DbQendraKosto.colTrupiQendraKosto trupivjeterqendra, out string shfaqmesazhapolupe, string kodkonfigurimi, ResourceManager rm, CultureInfo cultinf, int idGjuha, bool tollona, bool zevendesim, bool tollonakastrati, bool tollonakastratielektronik, bool zevendesimtollonakastrati, bool ruajrenditje, bool kontrolloSasiKonvertimiDheKthimi, bool kontrolloIMEIFifo, bool blerjengadealer, bool promocione, out string mesazhmevonshem, bool modifikimAprovimi,clsKokaShitje kokaEkz)
        {//transaksioni per te modifikuar fleten kontabel
            ImbLogger.LogTraceShitje("Filloi clsMesazh modifikoShitje me parametra serverUrl:" + serverUrl + $",kokaEkzistuese:{JsonConvert.SerializeObject(kokaEkzistuese)}, koka:{JsonConvert.SerializeObject(koka)}, idPeriudha:{idPeriudha}, colkonvertimi:{JsonConvert.SerializeObject(colkonvertimi)}, gjenerodokmag:{gjenerodokmag}, skemaWorkFlow:{skemaWorkFlow}, idetapa:{idetapa}, kontrollodisponabel:{kontrollodisponibel}, eshteOwn:{eshteOwn}, serialet:{JsonConvert.SerializeObject(serialet)}, konfigurimAmortizimi:{JsonConvert.SerializeObject(konfigurimAmortizimi)}, isshitje:{isshitje}, mekontabilizim:{mekontabilizim}, kodkonfigurimi:" + kodkonfigurimi + $", idGjuha:{idGjuha}, tollona:{tollona}, zevendesim:{zevendesim}, tollonakastrati:{tollonakastrati}, tollonakastratielektronik:{tollonakastratielektronik}, zevendesimtollonakastrati:{zevendesimtollonakastrati}, ruajrenditje:{ruajrenditje}, kontrolloSasiKonvertimiDheKthimi:{kontrolloSasiKonvertimiDheKthimi}, kontrolloIMEIFifo:{kontrolloIMEIFifo}, blerjengadealer:{blerjengadealer}, promocione:{promocione}");
            shfaqmesazhapolupemag = "jo";
            shfaqmesazhapolupe = "jo";
            mesazhmevonshem = "";
            var nivel = new clsNivelRegjistrimi();
            nivel.mbushNivelRegjistrimiSipasIdMeKonvertime(IdNivel);
            //obj koka qe i kalohet si parameter eshte si duhet te modifikohet
            bool kaveprimepas = false;
            int idKategori = clsNivelRegjistrimi.ktheIdKategoriaNivelRegjistrimi(kokaEkzistuese.IdNivel, dbRegj);
            int idStatusIVjeter = kokaEkzistuese.IdStatusDok;
            kokaEkzistuese.IdStatusDok = 2;  //duhet vendosur nje status i pershtatshem per kete modifikim
            koka.IdDokNga = kokaEkzistuese.IdShitjeKoka;//dokumentit te ri do i ruajme id e dokumentit nga u krijua  
            koka.Gjeneruar = kokaEkzistuese.Gjeneruar;
            kokaEkzistuese.OFleteKontabel = new clsKokaFleteKontabel();
            kokaEkzistuese.oGjendjeKF = new colGjendjeKlientFurnitor();
            clsNdermarrje nderm = new clsNdermarrje(IdNdermarrje);
            string kodSoftueri = WebConfigurationManager.AppSettings["kodSoftueri"];
            if (idStatusIVjeter == 0 && isshitje == true && nivel.Kodi == "FSH" && nderm.Fiskalizimi && this.iic == "")
            {
                koka.iic = clsFunksione.GjeneroIIC(nderm, nrDok, koka.TotaliMeZbritjeMeTVSH.ToString(), "ur271so291", kodSoftueri);
                if (koka.iic == "Ju lutem ngarkoni filen e passwordit!")
                    return new clsMesazh(false, "Ju lutem ngarkoni filen e passwordit!");
                else if (koka.iic == "Ju lutem ngarkoni certifikaten e sigurise!")
                    return new clsMesazh(false, "Ju lutem ngarkoni certifikaten e sigurise!");
            }
            if (mekontabilizim)
                kokaEkzistuese.OGjendjeKF = new colGjendjeKlientFurnitor(kokaEkzistuese.IdShitjeKoka, kokaEkzistuese.IdNivel, dbRegj);
            clsDatabaseInventari dbinv = new clsDatabaseInventari(dbRegj);
            colAmortizimiKoka colAmortizimetEVjetra = new colAmortizimiKoka();
            bool eshteDokumentQeNukKaGjeneruarDokMagazine = dbRegj.eshteDokumentShitjeQeNukKaGjeneruarMagazine(kokaEkzistuese.IdShitjeKoka);

            bool klientFiskalizimi = false;
            if (clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizim())
                klientFiskalizimi = true;
            clsMesazh mesazh = dbRegj.modifikoKokaShitje(kokaEkzistuese.IdShitjeKoka, kokaEkzistuese.IdNivel, kokaEkzistuese.IdTemplate, kokaEkzistuese.IdKonfigAmbjente, kokaEkzistuese.IdKlientFurnitor, kokaEkzistuese.IdProjekt, kokaEkzistuese.NrProjekt, kokaEkzistuese.DtDok, kokaEkzistuese.NrDok, kokaEkzistuese.NrSerial, kokaEkzistuese.DtMaturimi, kokaEkzistuese.IdMonedha, kokaEkzistuese.Kursi, kokaEkzistuese.IdMenyreTransporti, kokaEkzistuese.DtTransportimi, kokaEkzistuese.IdKushtDergimi, kokaEkzistuese.IdAgjent, kokaEkzistuese.IdMenyrePagese, kokaEkzistuese.IdKushtPagese, kokaEkzistuese.Zbritje, kokaEkzistuese.Totali, kokaEkzistuese.Tvsh, kokaEkzistuese.DtRegjistrimi, kokaEkzistuese.IdStatusDok, kokaEkzistuese.AdresaFaturimit, kokaEkzistuese.AdresaDergimit, kokaEkzistuese.Pershkrimi, kokaEkzistuese.Dogana, kokaEkzistuese.IdDegeAdministrative, kokaEkzistuese.IdPikeShitjeFurnizimi, koka.idPerdoruesi, koka.idRaportDesing, kokaEkzistuese.IdGrup1, kokaEkzistuese.IdGrup2, kokaEkzistuese.IdGrup3, kokaEkzistuese.AfatKohor, kokaEkzistuese.Cash, kokaEkzistuese.statusAprovimi, kokaEkzistuese.idKrijuesi, kokaEkzistuese.PerqindjeAgjenti, kokaEkzistuese.idTransferimi, kokaEkzistuese.idKonfigTransferimi, kokaEkzistuese.emerKlienti, kokaEkzistuese.kontakti, kokaEkzistuese.kase, kokaEkzistuese.kupon, kokaEkzistuese.DtFillimi, kokaEkzistuese.DtMbarimi, kokaEkzistuese.idAutomjet, kokaEkzistuese.kilometraAuto, kokaEkzistuese.idAgjenti2, kokaEkzistuese.perqindjeAgjenti2, kokaEkzistuese.idAgjenti3, kokaEkzistuese.perqindjeAgjenti3, kokaEkzistuese.marresi, kokaEkzistuese.idTransportues, this.ShpenzimeJoTeZbritshme, kokaEkzistuese.idarka, kokaEkzistuese.DtFature, kokaEkzistuese.idKarta, kokaEkzistuese.pike, kokaEkzistuese.MuajRaportimi, kokaEkzistuese.IdVitRaportimi, kokaEkzistuese.idFaza, kokaEkzistuese.shoferi, kokaEkzistuese.targaSHF, kokaEkzistuese.zbritjeNeVlere, kokaEkzistuese.perqindjeZbritje, kokaEkzistuese.koordinata, kokaEkzistuese.niptKlienti, kokaEkzistuese.qytetiK, kokaEkzistuese.idKategoriSeriali, kokaEkzistuese.shenime2, kokaEkzistuese.kartaPaPagese, idDokTransferimNga, idLlojMarreveshje, idMarreveshje, StatusMarreveshje, kokaEkzistuese.kerkuarNga, kokaEkzistuese.dateKerkese, kokaEkzistuese.nrDokMagazine, kokaEkzistuese.iic, kokaEkzistuese.nivf, kokaEkzistuese.idOperator, kokaEkzistuese.nivfKthim, kokaEkzistuese.eic, kokaEkzistuese.einStatus, kokaEkzistuese.procesi, kokaEkzistuese.eInvoiceType, klientFiskalizimi,kokaEkzistuese.tipiIVetefaturimit, clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizimV3());
            
            if (!mesazh.Status)
            {
                return mesazh;
            }

            dbRegj.FshiKlienteFurnitoreVartes(kokaEkzistuese.IdShitjeKoka);
            if (kokaEkzistuese.ColKlienteFurnitoreVartes.Count != 0)
            {
                dbRegj.RuajKlienteFurnitoreVartes(KrijoDataTableKlientFurnitorVartes(kokaEkzistuese.IdShitjeKoka, kokaEkzistuese.ColKlienteFurnitoreVartes));
            }

            mesazh = clsKokaShitje.hidhNeHistorik(kokaEkzistuese.IdShitjeKoka, dbRegj);
            if (!mesazh.Status)
            {
                return mesazh;
            }

            DbQendraKosto.clsDatabaseQendraKosto dbqendra = new DbQendraKosto.clsDatabaseQendraKosto(dbRegj);

            mesazh = dbRegj.fshiKonvertimSipasIdDokPasKonvertimi(kokaEkzistuese.IdShitjeKoka);
            if (!mesazh.Status)
            {
                return mesazh;
            }
            clsKokaMagazina kokaEkzistueseMag = new clsKokaMagazina
            {
                OFleteKontabel = new clsKokaFleteKontabel()
            };
            kokaEkzistueseMag.mbushKokaMagazinaSipasIDGjenerues(kokaEkzistuese.IdShitjeKoka, idKategori == 2 ? 1 : 2, kokaEkzistuese.IdKonfigAmbjente, dbRegj);
            clsDatabaseKontabilitet dbkontab = new clsDatabaseKontabilitet(dbRegj);
            // if (mekontabilizim) //gerta ///nuk lejon fshirjen e kont te magazines per ushmag
            {
                clsKokaFleteKontabel tmpKokeFK;
                if (idKategori == 1)
                {
                    tmpKokeFK = new clsKokaFleteKontabel(kokaEkzistuese.IdShitjeKoka, 1, dbkontab);
                    if (tmpKokeFK.IdKokaFleteKontabel != 0)
                    {
                        kokaEkzistuese.OFleteKontabel = tmpKokeFK;
                        DbQendraKosto.clsKokaQendraKosto kokaqendra = new DbQendraKosto.clsKokaQendraKosto();
                        kokaqendra.KtheKokaQKSipasIDGjeneruesDheKonfigMeTrup(tmpKokeFK.IdKokaFleteKontabel, tmpKokeFK.IdKonfigAmbjente, dbqendra);
                        if (kokaqendra.IdKoka != 0 && kokaqendra.IdKoka != -1)
                        {
                            kokaEkzistuese.OFleteKontabel.KokaQendraKosto = kokaqendra;
                        }
                        else
                            kokaEkzistuese.OFleteKontabel.KokaQendraKosto = new DbQendraKosto.clsKokaQendraKosto();
                    }
                    tmpKokeFK = new clsKokaFleteKontabel(kokaEkzistueseMag.IdKokaMagazina, 6, dbkontab);
                    if (tmpKokeFK.IdKokaFleteKontabel != 0)
                    {
                        kokaEkzistueseMag.OFleteKontabel = tmpKokeFK;
                        DbQendraKosto.clsKokaQendraKosto kokaqendra = new DbQendraKosto.clsKokaQendraKosto();
                        kokaqendra.KtheKokaQKSipasIDGjeneruesDheKonfigMeTrup(tmpKokeFK.IdKokaFleteKontabel, tmpKokeFK.IdKonfigAmbjente, dbqendra);
                        if (kokaqendra.IdKoka != 0 && kokaqendra.IdKoka != -1)
                            kokaEkzistueseMag.OFleteKontabel.KokaQendraKosto = kokaqendra;
                        else
                            kokaEkzistueseMag.OFleteKontabel.KokaQendraKosto = new DbQendraKosto.clsKokaQendraKosto();
                    }
                }
                else
                    if (idKategori == 2)
                {
                    tmpKokeFK = new clsKokaFleteKontabel(kokaEkzistuese.IdShitjeKoka, 2, dbkontab);
                    if (tmpKokeFK.IdKokaFleteKontabel != 0)
                    {
                        kokaEkzistuese.OFleteKontabel = tmpKokeFK;
                        DbQendraKosto.clsKokaQendraKosto kokaqendra = new DbQendraKosto.clsKokaQendraKosto();
                        kokaqendra.KtheKokaQKSipasIDGjeneruesDheKonfigMeTrup(tmpKokeFK.IdKokaFleteKontabel, tmpKokeFK.IdKonfigAmbjente, dbqendra);
                        if (kokaqendra.IdKoka != 0 && kokaqendra.IdKoka != -1)
                        {
                            kokaEkzistuese.OFleteKontabel.KokaQendraKosto = kokaqendra;
                        }
                        else
                            kokaEkzistuese.OFleteKontabel.KokaQendraKosto = new DbQendraKosto.clsKokaQendraKosto();
                    }
                    tmpKokeFK = new clsKokaFleteKontabel(kokaEkzistueseMag.IdKokaMagazina, 6, dbkontab);
                    if (tmpKokeFK.IdKokaFleteKontabel != 0)
                    {
                        kokaEkzistueseMag.OFleteKontabel = tmpKokeFK;
                        DbQendraKosto.clsKokaQendraKosto kokaqendra = new DbQendraKosto.clsKokaQendraKosto();
                        kokaqendra.KtheKokaQKSipasIDGjeneruesDheKonfigMeTrup(tmpKokeFK.IdKokaFleteKontabel, tmpKokeFK.IdKonfigAmbjente, dbqendra);
                        if (kokaqendra.IdKoka != 0 && kokaqendra.IdKoka != -1)
                        {
                            kokaEkzistueseMag.OFleteKontabel.KokaQendraKosto = kokaqendra;
                        }
                        else
                            kokaEkzistueseMag.OFleteKontabel.KokaQendraKosto = new DbQendraKosto.clsKokaQendraKosto();
                    }
                }
                koka.OFleteKontabel.IdDokNga = kokaEkzistuese.OFleteKontabel.IdKokaFleteKontabel;
                koka.OKokaMagazina.OFleteKontabel.IdDokNga = kokaEkzistueseMag.OFleteKontabel.IdKokaFleteKontabel;
                koka.OFleteKontabel.KokaQendraKosto.IdDokNga = kokaEkzistuese.OFleteKontabel.KokaQendraKosto.IdKoka;

                koka.oKokaMagazina.OFleteKontabel.KokaQendraKosto.IdDokNga = kokaEkzistueseMag.OFleteKontabel.KokaQendraKosto.IdKoka;
                if (kokaEkzistuese.OFleteKontabel.IdKokaFleteKontabel != 0)
                {
                    mesazh = kokaEkzistuese.OFleteKontabel.ModifikoFleteKontabel(true, dbkontab);
                    if (!mesazh.Status)
                    {
                        return mesazh;
                    }
                }
            }

            if (kokaEkzistueseMag.IdKokaMagazina != 0 || eshteDokumentQeNukKaGjeneruarDokMagazine)
            {
                kokaEkzistueseMag.mbushTrupMagazine(dbRegj);
                colArtikujt coleksistues = new colArtikujt(kokaEkzistueseMag.IdKokaMagazina, dbinv);
                int i = 0;
                foreach (clsTrupiMagazina trup in kokaEkzistueseMag.OcolTrupiMagazina)
                {
                    if (trup.IdLlojVeprimi == 1)
                        trup.Element = coleksistues[i];
                    i++;
                }
                if (eshteDokumentQeNukKaGjeneruarDokMagazine)
                {
                    kokaEkzistueseMag.IdLlojDokumentiMagazine = isshitje ? 2 : 1;
                    kokaEkzistueseMag.IdNdermarrje = koka.IdNdermarrje;
                    kokaEkzistueseMag.IdNdermarrjeVit = koka.IdNdermarrjeVit;
                    kokaEkzistueseMag.IdStatusDok = koka.IdStatusDok;
                }
                mesazh = kokaEkzistueseMag.kontrolloGjendjeNeFshirje(dbRegj, koka.OKokaMagazina.OcolTrupiMagazina, koka.OKokaMagazina.IdStatusDok); //TOCHECK

                if (!mesazh.Status)
                {
                    return mesazh;
                }

                koka.OKokaMagazina.IdDokNga = kokaEkzistueseMag.IdKokaMagazina;
                koka.OKokaMagazina.IdKrijuesi = kokaEkzistueseMag.IdKrijuesi;

                #region amortizimi
                clsDatabazeAsete dbasete = new clsDatabazeAsete(dbRegj);
                colAmortizimetEVjetra.ktheAmortizimKokaSipasIdGjeneruesi(kokaEkzistuese.idShitjeKoka, kokaEkzistuese.IdKonfigAmbjente);
                colAmortizimiTrupiAbstract trupiIRi = new colAmortizimiTrupi();///krijohet nje trup hipotetik sa per te bere kontrollin tek fshirja e dokumentit sepse trupi i dokumentit krijohet me vone
                foreach (clsSerialetMagazine serial in serialet)
                {
                    clsAmortizimiTrupiAbstract tr = new clsAmortizimiTrupi(0, serial.IdArtikulli, string.Empty, 0, new DateTime(), new DateTime(), new DateTime(), serial.IdNjesiAdministrative, serial.IdAQTSeriali, string.Empty, new clsArtikulli(), 0);
                    trupiIRi.Add(tr);
                }
                foreach (clsAmortizimiKoka kokaam in colAmortizimetEVjetra)
                {
                    mesazh = kokaam.fshi(koka.idPerdoruesi, 86, false, trupiIRi);
                    if (!mesazh.Status)
                        return mesazh;
                }


                mesazh = kokaEkzistueseMag.fshiMagazina(kokaEkzistueseMag.IdKokaMagazina, idPerdoruesi, dbRegj, true, true, true, serialet, out kaveprimepas, true);
                if (!mesazh.Status)
                    return mesazh;
                #endregion

            }

            kokaeksistuezerez = new clsKokaRezervime();
            kokaeksistuezerez.mbushKokaRezervimiSipasIDGjenerues(kokaEkzistuese.IdShitjeKoka, 1, kokaEkzistuese.IdKonfigAmbjente, dbRegj);
            if (kokaeksistuezerez.IdKokaRezervimi != 0)
            {
                koka.oKokaRezervime.IdDokNga = kokaeksistuezerez.IdKokaRezervimi;
                mesazh = kokaeksistuezerez.fshiRezervim(kokaeksistuezerez.IdKokaRezervimi, idPerdoruesi, dbRegj, idStatusIVjeter == 4);
                if (!mesazh.Status)
                {
                    return mesazh;
                }
            }

            //clsGjendjeKlientFurnitor gjendjeEkzistuese = koka.ktheObjektGjendjeKF();   
            foreach (clsGjendjeKlientFurnitor gj in kokaEkzistuese.OGjendjeKF)
            {
                if (gj.IdGjendjeKf != 0)
                {
                    gj.IdStatusGjendjeKf = 2; //rasti kur ndrysheohet statusi per treguar qe dokumenti eshte i modifikuar dhe nuk duhet marre parasysh
                    mesazh = gj.Modifiko(dbRegj);
                    if (!mesazh.Status)
                    {
                        return mesazh;
                    }
                }
            }

            //fshirja e fazave te kontrates
            if (koka.ocolFazat != null)
            {

                mesazh = clsFazaKontrate.fshi(dbRegj, kokaEkzistuese.idShitjeKoka, koka.idPerdoruesi);
                if (!mesazh.Status)
                {
                    return mesazh;
                }
            }

            colDokumentLidhesKoka cls = new colDokumentLidhesKoka(kokaEkzistuese.idShitjeKoka, 1, dbRegj);
            if (cls.Count == 0)
                cls = new colDokumentLidhesKoka(kokaEkzistuese.idShitjeKoka, 2, dbRegj);
            int iddokngadoklidhes = 0;
            int iddokshitje = 0;
            if (cls.Count != 0)
            {
                iddokngadoklidhes = cls[0].IdKoka;

                colDokumentLidhesTrupi trup = new colDokumentLidhesTrupi(cls[0].IdKoka, dbRegj);
                iddokshitje = trup.Find(x => x.Statusi == "0").IdDokumenti;
            }
            foreach (clsDokumentLidhesKoka k in cls)
            {
                mesazh = k.fshiDokumentDheKontabilitet(dbRegj);
                //Tani per tani kur modifikohet nje veprim banke fshihet lidhja e meparsheme e dok dhe ruhet lidhja e re. Kjo do ndryshohet me vone dhe lidhjes se vjeter do i vihet nje status dallues.
                if (!mesazh.Status)
                {
                    return mesazh;
                }
            }

            clsDatabaseAdmin data = new clsDatabaseAdmin(dbRegj);

            koka.StatusTransferimi = kokaEkzistuese.StatusTransferimi;
            if (idKategori == 1)
                mesazh = ruajShitje(serverUrl, koka, true, true, idPeriudha, dbRegj, colkonvertimi, gjenerodokmag, skemaWorkFlow, statusapp, idetapa, out shfaqmesazhapolupemag, kokaEkzistueseMag.OFleteKontabel.KokaQendraKosto.ColTrupi, kontrollodisponibel, eshteOwn, serialet, konfigurimAmortizimi, colAmortizimetEVjetra, mekontabilizim, trupivjeterqendra, out shfaqmesazhapolupe, kodkonfigurimi, koka.oFleteKontabel.IdDokNga, koka.oFleteKontabel.KokaQendraKosto.IdDokNga, iddokshitje, idGjuha, iddokngadoklidhes, tollona, zevendesim, tollonakastrati, tollonakastratielektronik, zevendesimtollonakastrati, ruajrenditje, kontrolloSasiKonvertimiDheKthimi, kaveprimepas, kontrolloIMEIFifo, blerjengadealer, promocione, out mesazhmevonshem, modifikimAprovimi, kokaEkz);
            else
                mesazh = ruajShitje(serverUrl, koka, false, true, idPeriudha, dbRegj, colkonvertimi, gjenerodokmag, skemaWorkFlow, statusapp, idetapa, out shfaqmesazhapolupemag, kokaEkzistueseMag.OFleteKontabel.KokaQendraKosto.ColTrupi, kontrollodisponibel, eshteOwn, serialet, konfigurimAmortizimi, colAmortizimetEVjetra, mekontabilizim, trupivjeterqendra, out shfaqmesazhapolupe, kodkonfigurimi, koka.oFleteKontabel.IdDokNga, koka.oFleteKontabel.KokaQendraKosto.IdDokNga, iddokshitje, idGjuha, iddokngadoklidhes, tollona, zevendesim, tollonakastrati, tollonakastratielektronik, zevendesimtollonakastrati, ruajrenditje, kontrolloSasiKonvertimiDheKthimi, kaveprimepas, kontrolloIMEIFifo, blerjengadealer, promocione, out mesazhmevonshem, modifikimAprovimi, kokaEkz);
            if (!mesazh.Status)
            {
                return mesazh;
            }
            ImbLogger.LogTraceShitje("Mbaroi clsMesazh modifikoShitje me parametra serverUrl:" + serverUrl + $",kokaEkzistuese:{JsonConvert.SerializeObject(kokaEkzistuese)}, koka:{JsonConvert.SerializeObject(koka)}, idPeriudha:{idPeriudha}, colkonvertimi:{JsonConvert.SerializeObject(colkonvertimi)}, gjenerodokmag:{gjenerodokmag}, skemaWorkFlow:{skemaWorkFlow}, idetapa:{idetapa}, kontrollodisponabel:{kontrollodisponibel}, eshteOwn:{eshteOwn}, serialet:{JsonConvert.SerializeObject(serialet)}, konfigurimAmortizimi:{JsonConvert.SerializeObject(konfigurimAmortizimi)}, isshitje:{isshitje}, mekontabilizim:{mekontabilizim}, kodkonfigurimi:" + kodkonfigurimi + $", idGjuha:{idGjuha}, tollona:{tollona}, zevendesim:{zevendesim}, tollonakastrati:{tollonakastrati}, tollonakastratielektronik:{tollonakastratielektronik}, zevendesimtollonakastrati:{zevendesimtollonakastrati}, ruajrenditje:{ruajrenditje}, kontrolloSasiKonvertimiDheKthimi:{kontrolloSasiKonvertimiDheKthimi}, kontrolloIMEIFifo:{kontrolloIMEIFifo}, blerjengadealer:{blerjengadealer}, promocione:{promocione}");
            return new clsMesazh(true, "Modifikimi përfundoi me sukses!");
        }

        public clsMesazh fshi(int idPerdoruesi, bool isshitje, bool tollonakastrati, bool tollonakastratielektronik)
        {
            using (var scope = new MyTransactionScope())
            {
                clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim();
                clsMesazh u_fshi = fshiFunction(idPerdoruesi, isshitje, tollonakastrati, tollonakastratielektronik, dbRegj);
                if (!u_fshi.Status)
                    return u_fshi;
                scope.Complete();
                return u_fshi;
            }
        }
        public clsMesazh fshiFunction(int idPerdoruesi, bool isshitje, bool tollonakastrati, bool tollonakastratielektronik, clsDatabaseRegjistrim dbRegj)
        {
            ImbLogger.LogTraceShitje($"Filloi clsMesazh fshiFunction me parametra idPerdoruesi:{idPerdoruesi}, isshitje:{isshitje}, tollonakastrati:{tollonakastrati}, tollonakastratielektronik:{tollonakastratielektronik}");
            clsKokaShitje kokaEkzistuesemema = new clsKokaShitje();
            kokaEkzistuesemema.ktheKokaShitjeSipasIdTransferimi(this.IdShitjeKoka, this.idKonfigAmbjente);
            try
            {
                clsKokaShitje data = new clsKokaShitje();
                clsMesazh u_fshi = data.fshiShitje(idPerdoruesi, this, isshitje, dbRegj, tollonakastrati, tollonakastratielektronik, 2);

                if (!u_fshi.Status)
                {
                    ImbLogger.LogTraceShitje($"Mbaroi clsMesazh fshi me parametra idPerdoruesi:{idPerdoruesi}, isshitje:{isshitje}, tollonakastrati:{tollonakastrati}, tollonakastratielektronik:{tollonakastratielektronik}");
                    return u_fshi;
                }

                if (this.idKonfigGjenerues == this.idKonfigAmbjente)
                {
                    ///ndryshojme statusin e dokumentit qe e ka gjeneruar
                    u_fshi = dbRegj.modifikoKokaShitjeStatusGjenerimi(this.idGjenerues, false);
                    if (!u_fshi.Status)
                    {
                        ImbLogger.LogTraceShitje($"Mbaroi clsMesazh fshi me parametra idPerdoruesi:{idPerdoruesi}, isshitje:{isshitje}, tollonakastrati:{tollonakastrati}, tollonakastratielektronik:{tollonakastratielektronik}");
                        return u_fshi;
                    }
                }

                if (kokaEkzistuesemema.idShitjeKoka > 0)
                {
                    data.fshiShitje(idPerdoruesi, kokaEkzistuesemema, clsKonfigurimAmbjenti.ktheIdKategori(kokaEkzistuesemema.idKonfigAmbjente) == 1, dbRegj, tollonakastrati, tollonakastratielektronik, 2);
                    if (!u_fshi.Status)
                    {
                        ImbLogger.LogTraceShitje($"Mbaroi clsMesazh fshi me parametra idPerdoruesi:{idPerdoruesi}, isshitje:{isshitje}, tollonakastrati:{tollonakastrati}, tollonakastratielektronik:{tollonakastratielektronik}");
                        return u_fshi;
                    }
                }
                else
                {
                    kokaEkzistuesemema.ktheKokaShitjeSipasIdTransferimiDokKryesor(this.IdTransferimi, this.idKonfigTransferimi, dbRegj);
                    if (kokaEkzistuesemema.idShitjeKoka > 0)
                    {
                        data.fshiShitje(idPerdoruesi, kokaEkzistuesemema, clsKonfigurimAmbjenti.ktheIdKategori(kokaEkzistuesemema.idKonfigAmbjente) == 1, dbRegj, tollonakastrati, tollonakastratielektronik, 2);
                        if (!u_fshi.Status)
                        {
                            ImbLogger.LogTraceShitje($"Mbaroi clsMesazh fshi me parametra idPerdoruesi:{idPerdoruesi}, isshitje:{isshitje}, tollonakastrati:{tollonakastrati}, tollonakastratielektronik:{tollonakastratielektronik}");
                            return u_fshi;
                        }
                    }
                }
                ImbLogger.LogTraceShitje($"Mbaroi clsMesazh fshi me parametra idPerdoruesi:{idPerdoruesi}, isshitje:{isshitje}, tollonakastrati:{tollonakastrati}, tollonakastratielektronik:{tollonakastratielektronik}");
                return u_fshi;
            }
            catch (Exception ex)
            {
                ImbLogger.Error($"Exception:{ex}");
                ImbLogger.LogTraceShitje($"Mbaroi clsMesazh fshi me parametra idPerdoruesi:{idPerdoruesi}, isshitje:{isshitje}, tollonakastrati:{tollonakastrati}, tollonakastratielektronik:{tollonakastratielektronik}");
                return new clsMesazh(false, ex.Message);
            }
        }

        public clsMesazh fshiShitje(int idPerdoruesi, clsKokaShitje koka, bool isshitje, clsDatabaseRegjistrim dbRegj, bool tollonakastrati, bool tollonakastratielektronik, int idstatusfshirje)
        {
            ImbLogger.LogTraceShitje($"Filloi clsMesazh fshiShitje me parametra idPerdoruesi:{idPerdoruesi}, koka:{JsonConvert.SerializeObject(koka)}, isshitje:{isshitje}, tollonakastrati:{tollonakastrati}, tollonakastratielektronik:{tollonakastratielektronik}, idstatusfshirje:{idstatusfshirje}");
            clsMesazh mesazh = new clsMesazh(true);
            clsDatabaseKontabilitet dbkontab = new clsDatabaseKontabilitet(dbRegj);
            clsKokaShitje kokaEkzistuese = new clsKokaShitje();
            clsDatabazeAsete dbasete = new clsDatabazeAsete(dbRegj);
            kokaEkzistuese.mbushKokaShitjeSipasIDPaTrup(koka.IdShitjeKoka, dbRegj);
            kokaEkzistuese.OFleteKontabel = new clsKokaFleteKontabel();
            kokaEkzistuese.OGjendjeKF = new colGjendjeKlientFurnitor(kokaEkzistuese.IdShitjeKoka, kokaEkzistuese.IdNivel, dbRegj);
            clsKokaMagazina kokaEkzistueseMag = new clsKokaMagazina();
            kokaEkzistueseMag.OFleteKontabel = new clsKokaFleteKontabel();
            koka.OKokaMagazina = new clsKokaMagazina();
            koka.OKokaMagazina.IdGjenerues = kokaEkzistuese.IdShitjeKoka;
            int idKategori = clsNivelRegjistrimi.ktheIdKategoriaNivelRegjistrimi(kokaEkzistuese.IdNivel, dbRegj);
            //koka.oArkiva = new colArkiva(koka.idShitjeKoka, idKategori, dbshare);
            ImbLogger.LogTraceShitje($"Mbaroi clsMesazh fshiShitje me parametra idPerdoruesi:{idPerdoruesi}, koka:{JsonConvert.SerializeObject(koka)}, isshitje:{isshitje}, tollonakastrati:{tollonakastrati}, tollonakastratielektronik:{tollonakastratielektronik}, idstatusfshirje:{idstatusfshirje}");
            foreach (clsGjendjeKlientFurnitor gj in kokaEkzistuese.OGjendjeKF)
            {
                if (gj.IdGjendjeKf != 0)
                {
                    gj.IdStatusGjendjeKf = 2; //rasti kur ndrysheohet statusi per treguar qe dokumenti eshte i modifikuar dhe nuk duhet marre parasysh
                    mesazh = gj.Modifiko(dbRegj);
                    if (!mesazh.Status)
                    {
                        return mesazh;
                    }
                }
            }
            mesazh = clsFazaKontrate.fshi(dbRegj, kokaEkzistuese.idShitjeKoka, koka.idPerdoruesi); //todo Nestila duhet pare te limitohet vetem per rastet kur ka kontrate jo per gjithe dokumentat
            if (!mesazh.Status)
            {
                return mesazh;
            }
            koka.OKokaMagazina.mbushKokaMagazinaSipasIDGjenerues(koka.OKokaMagazina.IdGjenerues, idKategori == 2 ? 1 : 2, koka.IdKonfigAmbjente, dbRegj);
            if (koka.OKokaMagazina != null && koka.OKokaMagazina.IdKokaMagazina > 0)
            {
                kokaEkzistueseMag = koka.OKokaMagazina;
                kokaEkzistueseMag.OFleteKontabel = new clsKokaFleteKontabel();

                #region amortizimi

                colAmortizimiKoka colAmortizimetEVjetra = new colAmortizimiKoka();

                colAmortizimetEVjetra.ktheAmortizimKokaSipasIdGjeneruesi(kokaEkzistuese.idShitjeKoka, kokaEkzistuese.IdKonfigAmbjente);
                foreach (clsAmortizimiKoka kokaam in colAmortizimetEVjetra)
                {
                    mesazh = kokaam.fshi(koka.idPerdoruesi, 86, false, new DbCore.DbAsete.colAmortizimiTrupi());
                    if (!mesazh.Status)
                    {
                        return mesazh;
                    }
                }
                bool kaveprimepas = false;
                mesazh = kokaEkzistueseMag.fshiMagazina(kokaEkzistueseMag.IdKokaMagazina, idPerdoruesi, dbRegj, true, false, false, new colSerialetMagazine(), out kaveprimepas, true);
                if (!mesazh.Status)
                {
                    return mesazh;
                }

                #endregion

            }
            switch (idKategori)
            {
                case 1:
                case 2:
                    clsKokaFleteKontabel newclsKokaFleteKontabel = new clsKokaFleteKontabel(kokaEkzistuese.IdShitjeKoka, idKategori, dbkontab);
                    if (newclsKokaFleteKontabel.NrDukumentiKokaFleteKontabel != null)
                        kokaEkzistuese.OFleteKontabel = newclsKokaFleteKontabel;
                    colDokumentLidhesKoka cls = new colDokumentLidhesKoka(kokaEkzistuese.idShitjeKoka, idKategori, dbRegj);
                    mesazh = cls.Fshi(dbRegj);
                    if (!mesazh.Status)
                    {
                        return mesazh;
                    }
                    break;
                default:
                    return new clsMesazh("Lloj i panjohur idKategorie: " + idKategori);
            }
            clsKokaRezervime kokaEkzistueseRez = new clsKokaRezervime();
            kokaEkzistueseRez.mbushKokaRezervimiSipasIDGjenerues(koka.IdShitjeKoka, 1, koka.idKonfigAmbjente, dbRegj);
            if (kokaEkzistueseRez.IdKokaRezervimi != 0)
            {
                mesazh = kokaEkzistueseRez.fshiRezervim(kokaEkzistueseRez.IdKokaRezervimi, idPerdoruesi, dbRegj, false);
                if (!mesazh.Status)
                {
                    return mesazh;
                }
            }
            if (kokaEkzistuese.OFleteKontabel.IdKokaFleteKontabel != 0)
            {
                mesazh = kokaEkzistuese.oFleteKontabel.fshiupd(dbkontab);
                if (!mesazh.Status)
                {
                    return mesazh;
                }
            }
            if (mesazh.Status)
            {
                mesazh = dbRegj.fshiKonvertimSipasIdDokKonvertuar(kokaEkzistuese.IdShitjeKoka);
                if (!mesazh.Status)
                {
                    return mesazh;
                }
                clsDatabazeTollona dbtollona = new clsDatabazeTollona(dbRegj);
                if (kokaEkzistuese.idDegeAdministrative != 0 && (tollonakastrati || tollonakastratielektronik))
                {
                    clsDegeAdministrative dege = new clsDegeAdministrative(kokaEkzistuese.idDegeAdministrative, dbRegj);
                    mesazh = dbtollona.ndryshoStatusTolloniLeter(dege.Kodi, kokaEkzistuese.dtDok, false);
                    if (!mesazh.Status)
                    {
                        return mesazh;
                    }
                    mesazh = dbtollona.ndryshoStatusTollonElektronik(kokaEkzistuese.dtDok, dege.Kodi, false);
                    if (!mesazh.Status)
                    {
                        return mesazh;
                    }
                    clsKlientFurnitor kf = new clsKlientFurnitor(kokaEkzistuese.idKlientFurnitor, dbkontab);
                    mesazh = dbtollona.ndryshoStatusTollonElektronikSpecifik(kokaEkzistuese.dtDok, dege.Kodi, false, kf.KodKlientFurnitor);
                    if (!mesazh.Status)
                    {
                        return mesazh;
                    }
                }
                mesazh = dbRegj.fshiKonvertimSipasIdDokPasKonvertimi(kokaEkzistuese.IdShitjeKoka);
                if (!mesazh.Status)
                {
                    return mesazh;
                }

                DbProdhimi.clsDatabazeProdhimi db = new DbProdhimi.clsDatabazeProdhimi(dbRegj);
                //db.vendosManager(dbRegj );
                mesazh = db.fshiUrdherPorosiPlanifikimiSipasIdUrdheri(kokaEkzistuese.IdShitjeKoka);
                if (!mesazh.Status)
                {
                    return mesazh;
                }

                kokaEkzistuese.IdStatusDok = idstatusfshirje; //duhet vendosur nje status i pershtatshem per kete modifikim
                bool klientFiskalizimi = false;
                if (clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizim())
                    klientFiskalizimi = true;
                mesazh = dbRegj.modifikoKokaShitje(kokaEkzistuese.IdShitjeKoka, kokaEkzistuese.IdNivel, kokaEkzistuese.IdTemplate, kokaEkzistuese.IdKonfigAmbjente, kokaEkzistuese.IdKlientFurnitor, kokaEkzistuese.IdProjekt, kokaEkzistuese.NrProjekt, kokaEkzistuese.DtDok, kokaEkzistuese.NrDok, kokaEkzistuese.NrSerial, kokaEkzistuese.DtMaturimi, kokaEkzistuese.IdMonedha, kokaEkzistuese.Kursi, kokaEkzistuese.IdMenyreTransporti, kokaEkzistuese.DtTransportimi, kokaEkzistuese.IdKushtDergimi, kokaEkzistuese.IdAgjent, kokaEkzistuese.IdMenyrePagese, kokaEkzistuese.IdKushtPagese, kokaEkzistuese.Zbritje, kokaEkzistuese.Totali, kokaEkzistuese.Tvsh, kokaEkzistuese.DtRegjistrimi, kokaEkzistuese.IdStatusDok, kokaEkzistuese.AdresaFaturimit, kokaEkzistuese.AdresaDergimit, kokaEkzistuese.Pershkrimi, kokaEkzistuese.Dogana, kokaEkzistuese.IdDegeAdministrative, kokaEkzistuese.IdPikeShitjeFurnizimi, idPerdoruesi, kokaEkzistuese.idRaportDesing, kokaEkzistuese.IdGrup1, kokaEkzistuese.IdGrup2, kokaEkzistuese.IdGrup3, kokaEkzistuese.AfatKohor, kokaEkzistuese.Cash, kokaEkzistuese.statusAprovimi, kokaEkzistuese.idKrijuesi, kokaEkzistuese.PerqindjeAgjenti, kokaEkzistuese.idTransferimi, kokaEkzistuese.idKonfigTransferimi, kokaEkzistuese.emerKlienti, kokaEkzistuese.kontakti, kokaEkzistuese.kase, kokaEkzistuese.kupon, kokaEkzistuese.DtFillimi, kokaEkzistuese.DtMbarimi, kokaEkzistuese.idAutomjet, kokaEkzistuese.kilometraAuto, kokaEkzistuese.idAgjenti2, kokaEkzistuese.perqindjeAgjenti2, kokaEkzistuese.idAgjenti3, kokaEkzistuese.perqindjeAgjenti3, kokaEkzistuese.marresi, kokaEkzistuese.idTransportues, kokaEkzistuese.ShpenzimeJoTeZbritshme, kokaEkzistuese.IdArka, kokaEkzistuese.DtFature, kokaEkzistuese.idKarta, kokaEkzistuese.pike, kokaEkzistuese.MuajRaportimi, kokaEkzistuese.IdVitRaportimi, kokaEkzistuese.IdFaza, kokaEkzistuese.Shoferi, kokaEkzistuese.TargaShoferit, kokaEkzistuese.zbritjeNeVlere, kokaEkzistuese.perqindjeZbritje, kokaEkzistuese.koordinata, kokaEkzistuese.niptKlienti, kokaEkzistuese.qytetiK, kokaEkzistuese.idKategoriSeriali, kokaEkzistuese.shenime2, kokaEkzistuese.kartaPaPagese, kokaEkzistuese.idDokTransferimNga, kokaEkzistuese.idLlojMarreveshje, kokaEkzistuese.idMarreveshje, kokaEkzistuese.StatusMarreveshje, kokaEkzistuese.kerkuarNga, kokaEkzistuese.dateKerkese, kokaEkzistuese.nrDokMagazine, kokaEkzistuese.iic, kokaEkzistuese.nivf, kokaEkzistuese.idOperator, kokaEkzistuese.nivfKthim, kokaEkzistuese.eic, kokaEkzistuese.einStatus, kokaEkzistuese.procesi, kokaEkzistuese.eInvoiceType, klientFiskalizimi, kokaEkzistuese.tipiIVetefaturimit, clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizimV3());
                if (!mesazh.Status)
                {
                    return mesazh;
                }

                mesazh = hidhNeHistorik(kokaEkzistuese.IdShitjeKoka, dbRegj);
                if (mesazh.Status)
                    mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);

                dbRegj.FshiKlienteFurnitoreVartes(kokaEkzistuese.IdShitjeKoka);
                if (kokaEkzistuese.ColKlienteFurnitoreVartes.Count != 0)
                {
                    dbRegj.RuajKlienteFurnitoreVartes(KrijoDataTableKlientFurnitorVartes(kokaEkzistuese.IdShitjeKoka, kokaEkzistuese.ColKlienteFurnitoreVartes));
                }
            }
            return mesazh;
        }

        public static DataRow ktheShitjenFunditteArtikullit(int idartikulli, DateTime date, int idkategori)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda ktheShitjenFunditteArtikullit me parametra idartikulli:{idartikulli}, date:{date}, idkategori:{idkategori}");
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            DataRow dr = data.ktheShitjenFunditteArtikullit(idartikulli, date, idkategori);
            data.Dispose();
            ImbLogger.LogTraceShitje($"Mbaroi metoda ktheShitjenFunditteArtikullit me parametra idartikulli:{idartikulli}, date:{date}, idkategori:{idkategori}");
            return dr;
        }

        public static DataRow ktheShitjenFunditteArtikullitKlient(int idartikulli, DateTime date, int idkategori, int idklient)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda ktheShitjenFunditteArtikullitKlient me parametra idartikulli:{idartikulli}, date:{date}, idkategori:{idkategori}, idklient:{idklient}");
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            DataRow dr = data.ktheShitjenFunditteArtikullitKlient(idartikulli, date, idkategori, idklient);
            data.Dispose();
            ImbLogger.LogTraceShitje($"Filloi metoda ktheShitjenFunditteArtikullitKlient me parametra idartikulli:{idartikulli}, date:{date}, idkategori:{idkategori}, idklient:{idklient}");
            return dr;
        }

        public static DataRow ktheCmiminFunditMeTVSHArtikullKlient(int idartikulli, DateTime date, int idklient, int idNdermarrje)
        {
            using (clsDatabaseRegjistrim data = new clsDatabaseRegjistrim())
            {
                return data.ktheCmiminFunditMeTVSHArtikullKlient(idartikulli, date, idklient, idNdermarrje);
            }
        }

        /// <summary>
        /// Merr trupin e nje dokumenti nga tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.ktheTrupiShitje"/> 
        /// </summary>
        /// <returns > nje object colTrupiShitje me trupin e dokumentit</returns>
        public static colTrupiShitje merrTrupiShitje(int idKokeShitje)
        {
            colTrupiShitje data = new colTrupiShitje();
            data.mbushTrupiShitje(idKokeShitje);
            return data;
        }
        public static colTrupiShitje merrTrupiShitjeWebhook(int idKokeShitje)
        {
            colTrupiShitje data = new colTrupiShitje();
            data.mbushTrupiShitjeWebhook(idKokeShitje);
            return data;
        }
        public static colTrupiShitje merrTrupiShitjePerWebhook(int idKokeShitje)
        {
            colTrupiShitje data = new colTrupiShitje();
            data.mbushTrupiShitjePerWebhook(idKokeShitje);
            return data;
        }

        public colTrupiShitje merrTrupShitje()
        {
            return merrTrupiShitje(this.idShitjeKoka);
        }

        public colTrupiShitje mbushTrupShitje()
        {
            this.oColTrupiShitje = merrTrupShitje();
            return this.oColTrupiShitje;
        }

        public colTrupiShitje merrTrupiShitjeDheAutorizime(int idperdorues, int idndermarje)
        {
            colTrupiShitje data = new colTrupiShitje();
            data.ktheTrupiShitjeDheAutorizime(this.IdShitjeKoka, idperdorues, idndermarje);
            return data;
        }

        /// <summary>
        /// mbush koken e shitjes sipas id pa trupin
        /// </summary>
        /// <param name="idShitjeKoka">id e kokes se shitjes</param>
        /// <returns>mbush true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushKokaShitjeSipasIDPaTrup(int idShitjeKoka)
        {
            using (clsDatabaseRegjistrim dbKokeShitje = new clsDatabaseRegjistrim())
            {
                return mbushKokeShitjePaTrup(dbKokeShitje.ktheKokaShitjeSipasID(idShitjeKoka));
            }
        }
        public DataTable ktheKodOperatori(int idndermarje)
        {
            using (clsDatabaseRegjistrim dbKokeShitje = new clsDatabaseRegjistrim())
            {
                return dbKokeShitje.ktheKodOperatori(idndermarje);
            }
        }
        public DataTable ktheKodProcesi()
        {
            using (clsDatabaseRegjistrim dbKokeShitje = new clsDatabaseRegjistrim())
            {
                return dbKokeShitje.ktheKodProcesi();
            }
        }
        public DataTable ktheKodTipiEinvoice()
        {
            using (clsDatabaseRegjistrim dbKokeShitje = new clsDatabaseRegjistrim())
            {
                return dbKokeShitje.ktheKodTipiEinvoice();
            }
        }
        public DataTable ktheVleratProcesi()
        {
            using (clsDatabaseRegjistrim dbKokeShitje = new clsDatabaseRegjistrim())
            {
                return dbKokeShitje.ktheVleratProcesi();
            }
        }
        public DataTable ktheVleratTipiEinvoice()
        {
            using (clsDatabaseRegjistrim dbKokeShitje = new clsDatabaseRegjistrim())
            {
                return dbKokeShitje.ktheVleratTipiEinvoice();
            }
        }
        public DataTable ktheKodDhePershkrimProcesi(int id)
        {
            using (clsDatabaseRegjistrim dbKokeShitje = new clsDatabaseRegjistrim())
            {
                return dbKokeShitje.ktheKodDhePershkrimProcesi(id);
            }
        }
        public DataTable ktheKodDhePershkrimTipiEinvoice(int id)
        {
            using (clsDatabaseRegjistrim dbKokeShitje = new clsDatabaseRegjistrim())
            {
                return dbKokeShitje.ktheKodDhePershkrimTipiEinvoice(id);
            }
        }
        public static DataTable kthePershkrimTipiEinvoice(string Kodi)
        {
            using (clsDatabaseRegjistrim dbKokeShitje = new clsDatabaseRegjistrim())
            {
                return dbKokeShitje.kthePershkrimTipiEinvoice(Kodi);
            }
        }
        public DataTable ktheIdProcesi(string kodProcesi)
        {
            using (clsDatabaseRegjistrim dbKokeShitje = new clsDatabaseRegjistrim())
            {
                return dbKokeShitje.ktheIdProcesi(kodProcesi);
            }
        }
        public static DataTable kthePershkrimProcesi(string kodProcesi)
        {
            using (clsDatabaseRegjistrim dbKokeShitje = new clsDatabaseRegjistrim())
            {
                return dbKokeShitje.kthePershkrimProcesi(kodProcesi);
            }
        }
        public DataTable ktheIdTipiEinvoice(string kodTipiEinvoice)
        {
            using (clsDatabaseRegjistrim dbKokeShitje = new clsDatabaseRegjistrim())
            {
                return dbKokeShitje.ktheIdTipiEinvoice(kodTipiEinvoice);
            }
        }
        public bool mbushKokaShitjeSipasIDPaTrup(int idShitjeKoka, clsDatabaseRegjistrim dbKokeShitje)
        {

            bool mbush = mbushKokeShitjePaTrup(dbKokeShitje.ktheKokaShitjeSipasID(idShitjeKoka));

            return mbush;
        }

        /// <summary>
        /// mbush koken e shitjes sipas id nivelit dhe nr te dokumentit dhe dates se dokumentit
        /// </summary>
        /// <param name="idNivel">id e nivelit</param>
        /// <param name="nrDok">nr i dokumentit</param>
        /// <param name="dtDok">data e dokumentit</param>
        /// <returns>mbush true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushKokaShitjeSipasIdNivelNrDokDtDok(int idNivel, string nrDok, DateTime dtDok)
        {
            using (clsDatabaseRegjistrim dbKokeShitje = new clsDatabaseRegjistrim())
            {
                return mbushKokeShitjePaTrup(dbKokeShitje.ktheKokaShitjeSipasIdNivelNrDokDtDok(idNivel, nrDok, dtDok));
            }
        }

        public bool mbushKokaShitjeSipasIdKonfigAmbNrDokDtDok(int idKonfigAmb, string nrDok, DateTime dtDok)
        {
            using (clsDatabaseRegjistrim dbKokeShitje = new clsDatabaseRegjistrim())
            {
                return mbushKokeShitjePaTrup(dbKokeShitje.ktheKokaShitjeSipasIdKonfigAmbjenteNrDokDtDok(idKonfigAmb, nrDok, dtDok));
            }
        }

        public bool ktheKokaShitjeSipasIdTransferimi(int idtransferimi, int idkonfigtransferimi)
        {
            using (clsDatabaseRegjistrim dbKokeShitje = new clsDatabaseRegjistrim())
            {
                return mbushKokeShitjePaTrup(dbKokeShitje.ktheKokaShitjeSipasIdTransferimi(idtransferimi, idkonfigtransferimi));
            }
        }

        public bool ktheKokaShitjeSipasIdTransferimi(int idtransferimi, int idkonfigtransferimi, clsDatabaseRegjistrim dbKokeShitje)
        {

            return mbushKokeShitjePaTrup(dbKokeShitje.ktheKokaShitjeSipasIdTransferimi(idtransferimi, idkonfigtransferimi));

        }

        public bool ktheKokaShitjeSipasIdTransferimiDokKryesor(int idtransferimi, int idkonfigtransferimi, clsDatabaseRegjistrim dbKokeShitje)
        {
            return mbushKokeShitjePaTrup(dbKokeShitje.ktheKokaShitjeSipasIdTransferimiDokKryesor(idtransferimi, idkonfigtransferimi));
        }

        public static string merrNgjyreKonvertime(bool isBlerje, int idndermarje, int idkoka)
        {
            using (clsDatabaseRegjistrim dbKokeShitje = new clsDatabaseRegjistrim())
            {
                return dbKokeShitje.merrNgjyreKonvertime(isBlerje, idndermarje, idkoka);
            }
        }

        public static string merrNgjyreGjenerimi(int idndermarje, int idkoka)
        {
            clsDatabaseRegjistrim dbKokeShitje = new clsDatabaseRegjistrim();
            string mbush = dbKokeShitje.merrNgjyreGjenerimi(idndermarje, idkoka);
            dbKokeShitje.Dispose();
            return mbush;
        }

        public static double merrSasiTePaGjeneruarArtikulli(int idartikulli, int idkoka)
        {
            clsDatabaseRegjistrim dbKokeShitje = new clsDatabaseRegjistrim();
            double mbush = dbKokeShitje.merrSasiTePaGjeneruarArtikulli(idartikulli, idkoka);
            dbKokeShitje.Dispose();
            return mbush;
        }

        public static string merrNgjyreKonvertimeMag(int idndermarje, int idkoka)
        {
            clsDatabaseRegjistrim dbKokeShitje = new clsDatabaseRegjistrim();
            string mbush = dbKokeShitje.merrNgjyreKonvertimeMag(idndermarje, idkoka);
            dbKokeShitje.Dispose();
            return mbush;
        }
        public static string merrKodNiveliSipasIdShitjes(int idshitje)
        {
            clsDatabaseRegjistrim dbKokeShitje = new clsDatabaseRegjistrim();
            string mbush = dbKokeShitje.merrKodNiveliSipasIdShitjes(idshitje);
            dbKokeShitje.Dispose();
            return mbush;
        }

        public static bool gjeneruarUrdherShitje(int idUrdherShitje)
        {
            clsDatabaseRegjistrim dbKokeShitje = new clsDatabaseRegjistrim();
            bool gjeneruar = dbKokeShitje.gjeneruarUrdherShitje(idUrdherShitje);
            dbKokeShitje.Dispose();
            return gjeneruar;
        }

        public static clsMesazh modifikoStatusDokumenti(int idShitjeKoka, int idStatusDok)
        {
            using (var dbKokeShitje = new clsDatabaseRegjistrim())
                return dbKokeShitje.modifikoKokaShitjeStatusDokumenti(idShitjeKoka, idStatusDok);
        }

        public static clsMesazh modifikoStatusTrigeri(int idtransferimi)
        {
            clsDatabaseRegjistrim dbKokeShitje = new clsDatabaseRegjistrim();
            clsMesazh gjeneruar = dbKokeShitje.modifikoStatusTrigeri(idtransferimi);
            dbKokeShitje.Dispose();
            return gjeneruar;
        }

        #region kasa

        public clsMesazh gjeneroFatureFiskaleIva(out string stream, clsKonfigurimKase kasa, double perqindja, string totalimonbaze, int idperdoruesi, string totatimezbritjemetvsh, bool metvsh)
        {
            stream = string.Empty;
            if (double.Parse(totatimezbritjemetvsh) < 0)
                return new clsMesazh(false, "Kasa nuk pranon fature me total negativ. Fatura nuk u regjistrua ne kase!");
            else
            {
                string text = string.Empty;
                string QPZ; //sasia
                double PPZI; //cmimi
                string DESIC; //pershkrimi
                string descria; //rreshti qe do te shkruhet ne kase
                int kaseLogicalNumber;
                string filename = kasa.OColVlerat.ktheVlereOpsioni("KASEFISKALEPATH");
                //clsVleraKonfigurimiKasa vlera = new clsVleraKonfigurimiKasa();
                kaseLogicalNumber = int.Parse(kasa.OColVlerat.ktheVlereOpsioni("KASEFISKALELOGICALNR")); //meret nr llogjik i kases
                bool printoPershkrim2 = bool.Parse(kasa.OColVlerat.ktheVlereOpsioni("PRINTOPERSHKRIM2"));
                bool eshtePrinterFiskal = (kasa.OColVlerat.ktheVlereOpsioni("KASEAPOPRINTER") == "False");
                bool nrkopjesh = (kasa.OColVlerat.ktheVlereOpsioni("NRKOPJESH") == "True");

                text = filename + "&&";


                if (kasa.OColVlerat.ktheVlereOpsioni("PRINTOMANUALISHTNGAKASA") == "True")
                    text += "H,1,______,_,__;" + "||";

                if (kasa.OColVlerat.ktheVlereOpsioni("KUPONTATIMOR") == "True")     //seriali per kupon tatimor
                    if (eshtePrinterFiskal)
                    {
                        text += "48,1,______,_,__;" + this.NrSerial + ";||";
                    }
                //else
                //{
                //    text += "M,1,______,_,__;" + this.NrSerial + ";||";
                //}

                if (this.kupon)
                    text += "M,1,______,_,__;" + this.NrSerial + ";||";

                //if (kasa.OColVlerat.ktheVlereOpsioni("PRINTONRFATURE") == "True")
                //    if (eshtePrinterFiskal)
                //    {
                //        if (!string.IsNullOrEmpty(this.NrSerial))
                //        {
                //           text += "54," + kaseLogicalNumber + ",______,_,__;Serial:" + this.NrSerial + ";||";
                //        }
                //    }
                //    else if (!string.IsNullOrEmpty(this.NrSerial))
                //    {                       
                //        text += "P," + kaseLogicalNumber.ToString() + ",______,_,__;Serial:" + this.NrSerial + ";;;;;||";
                //    }
                if (kaRreshtaKthimi(this))
                {
                    this.OColTrupiShitje = rendit(this.OColTrupiShitje);  //renditen ne fund rreshtat e kthimit
                                                                          // nrkopjesh = int.Parse(kasa.OColVlerat.ktheVlereOpsioni("NRKOPJESHKTHIMI"));
                }
                foreach (clsTrupiShitje t in this.OColTrupiShitje)
                {
                    if (t.Kodi == null)
                        continue;
                    QPZ = (Math.Abs(t.Sasia)).ToString();  //sasia
                    //llogaritet cmimi qe shkon ne kasen fiskale
                    if (true)    //cmimet me tvsh 
                    {
                        //nese ndermarrja eshte konfiguruar me cmimet me tvsh, merret nga grida cmimi i dhene dhe shumezohet me kursin, qe te printohet vlera ne mon baze
                        //PPZI = t.Cmimi * this.Kursi;
                        if (metvsh)
                        {
                            PPZI = t.Cmimi * this.Kursi;
                        }
                        else
                        {
                            //PPZI = t.VleftaMeTvsh / t.Sasia * this.Kursi;
                            // PPZI = (t.VleftaMeTvsh / (t.Sasia * (1 - t.Zbritje / 100))) * this.Kursi;
                            if (t.Zbritje == 100)
                            {
                                PPZI = 0;
                            }
                            else
                            {
                                PPZI = (t.VleftaMeTvsh / (t.Sasia * (1 - t.Zbritje / 100))) * this.Kursi;
                            }
                        }



                    }
                    //else TODO: kur ndermarrje eshte e konfiguruar pa tvsh
                    //{
                    //    //llogaritet cmimi si (cmim pa tvsh * perqindje tvsh per artikullin) dhe shumezohet me kursin, qe te printohet vlera ne mon baze
                    //    PPZI = t.Cmimi * (1 + t.Zbritje / 100) * this.Kursi;
                    //}


                    double cmimSipasKonfigKase;
                    //int MeShifraDhjetore;
                    if (kasa.OColVlerat.ktheVlereOpsioni("MESHIFRADHJETORE") == "False")
                    {
                        cmimSipasKonfigKase = PPZI;
                        //MeShifraDhjetore = 0; 
                    } //kasa eshte konfiguruar pa presje dhjetore, i dergohen cmimet te konvertuar ne numra te plote
                    else
                    {
                        cmimSipasKonfigKase = PPZI * 100;
                        //MeShifraDhjetore = 1; 
                    } //kasa eshte konfiguruar me presje dhjetore, i dergohen cmimet te shumezuar me 100, te konvertuar ne numra te plote, ne menyre qe dy shifrat e fundit te perfaqesojne pjesen dhjetore
                    int paramKthim = 0;
                    if (t.Sasia < 0)     //nqs kthime cmimi vete me minus
                        if (eshtePrinterFiskal)
                            paramKthim = 1;
                        else
                            cmimSipasKonfigKase = -cmimSipasKonfigKase;
                    DESIC = clsFunksione.kthePershkrimArtikullPerKasen(1, false, "", printoPershkrim2, t.Pershkrimi, t.Pershkrim2, 25);
                    string niveltvsh = "1";
                    //niveltvsh = merrNivelTvsh(t, this.idNdermarje, idperdoruesi);
                    niveltvsh = kasa.OColVlerat.ktheVlereTakse(t.Tvsh);
                    if (eshtePrinterFiskal)
                        descria = "49," + kaseLogicalNumber + ",______,_,__;" + DESIC + ";" + Math.Round(cmimSipasKonfigKase, 2) + ";" + QPZ + ";" + niveltvsh + ";" + paramKthim + ";1;" + Math.Round(t.Zbritje, 2) + ";";
                    else
                        descria = "S," + kaseLogicalNumber + ",______,_,__;" + DESIC + ";" + Math.Round(cmimSipasKonfigKase, 2) + ";" + QPZ + ";1;1;" + niveltvsh + ";0;0;";
                    //sw.WriteLine(descria);   //nr i kases+pershkrim artikulli+cmim+sasi+kodi i tvsh
                    text += descria + "||";
                    //printojme zbritjen analitike, nese ka per artikullin e rradhes
                    if (!eshtePrinterFiskal)
                        if (t.Zbritje > 0)
                        {
                            descria = "C," + kaseLogicalNumber + ",______,_,__;1;" + Math.Round(t.Zbritje * 100, 2) + ";;;;";
                            //sw.WriteLine(descria);
                            text += descria + "||";
                        }
                }
                //printojme zbritjen ne totalin e fatures, nese ka, por fillimisht duhet printuar subtotal
                if (perqindja > 0)
                {
                    if (eshtePrinterFiskal)
                        descria = "51," + kaseLogicalNumber + ",______,_,__;1;1;1;" + Math.Round(perqindja, 2) + ";";
                    else
                    {
                        text += "T," + kaseLogicalNumber + ",______,_,__;4;;;;;||";
                        //sw.WriteLine("T," + kaseLogicalNumber + ",______,_,__;4;;;;;");
                        descria = "C," + kaseLogicalNumber + ",______,_,__;1;" + Math.Round(perqindja * 100, 2) + ";;;;";
                    }
                    text += descria + "||";
                    //sw.WriteLine(descria);
                }
                //per opsionin e afishimit te cmimit ne fature sipas nje monedhe te dyte
                string cmimMonDyte;
                cmimMonDyte = kasa.OColVlerat.ktheVlereOpsioni("CMIMMONEDHEDYTE");
                if (cmimMonDyte != string.Empty)
                {
                    double kurs;
                    clsKurset kursi = new clsKurset(int.Parse(cmimMonDyte), this.DtDok);
                    //clsMonedha mon = new clsMonedha(int.Parse(cmimMonDyte));
                    kurs = kursi.VleraKursi;
                    if (kurs != -1 && kurs != 0)
                    {
                        if (eshtePrinterFiskal)
                        {
                            text += "54," + kaseLogicalNumber + ",______,_,__;" + clsMonedha.ktheKodMonedheSipasId(int.Parse(cmimMonDyte)) + ":" + Math.Round(double.Parse(totalimonbaze) / kurs, 2) + ";||";
                            //sw.WriteLine("54," + kaseLogicalNumber + ",______,_,__;" + mon.KodiMonedha + ":" + Math.Round(double.Parse(totalimonbaze) / kurs, 2) + ";");
                        }
                        else
                        {
                            text += "P," + kaseLogicalNumber + ",______,_,__;" + clsMonedha.ktheKodMonedheSipasId(int.Parse(cmimMonDyte)) + ":" + Math.Round(double.Parse(totalimonbaze) / kurs, 2).ToString() + ";;;;;||";
                            //sw.WriteLine("P," + kaseLogicalNumber + ",______,_,__;" + mon.KodiMonedha + ":" + Math.Round(double.Parse(totalimonbaze) / kurs, 2).ToString() + ";;;;;");
                        }
                    }
                }
                //mbyllja e fatures
                if (eshtePrinterFiskal)
                {
                    text += "53," + kaseLogicalNumber + ",______,_,__;" + this.IdMenyrePagese + ";;||";
                    text += "56," + kaseLogicalNumber + ",______,_,__;||";
                    //sw.WriteLine("53," + kaseLogicalNumber + ",______,_,__;" + this.IdMenyrePagese + ";;");
                    //sw.WriteLine("56," + kaseLogicalNumber + ",______,_,__;");
                }
                else
                {
                    text += String.Format("T,{0},______,_,__;", kaseLogicalNumber) + "||";
                    if (nrkopjesh)
                        text += "D,1,______,_,__;" + "||"; //ALPHAWEB-3368
                    //sw.WriteLine(String.Format("T,{0},______,_,__;", kaseLogicalNumber));
                }
                if (eshtePrinterFiskal && nrkopjesh)
                {
                    text += "109," + kaseLogicalNumber + ",______,_,__;" + 1 + ";";
                    //sw.WriteLine("109," + kaseLogicalNumber + ",______,_,__;" + nrkopjesh + ";");
                }

                if (kasa.OColVlerat.ktheVlereOpsioni("PRINTOMANUALISHTNGAKASA") == "True")
                    text += "F,1,______,_,__;" + "||";
                //  sw.Close();
                stream = text;
                return new clsMesazh(true, clsKokaShitje.krijimStreamKaseMeSukses);
                //return new clsMesazh(true, clsKokaShitje.mesazhSuksesKase);
            }
        }

        public clsMesazh gjeneroFatureFiskaleAed(out string stream, clsKonfigurimKase kasa, double perqindja, int idperdoruesi, string totatimezbritjemetvsh, bool metvsh)
        {
            stream = string.Empty;
            if (double.Parse(totatimezbritjemetvsh) < 0)
                return new clsMesazh(false, "Kasa nuk pranon fature me total negativ. Fatura nuk u regjistrua ne kase!");
            else
            {
                string QPZ; //sasia
                string PPZI;  //cmimi
                double cmimi;
                string DESIC;   //pershkrimi
                double ZPZ;
                string TVSHKOD;
                string descria; //rreshti qe do te shkruhet ne kase
                MenyrePagese LLPAG;
                Boolean Kthim;  //fatura permban kthime
                Kthim = false;
                string filename = kasa.OColVlerat.ktheVlereOpsioni("KASEFISKALEPATH");
                clsVleraKonfigurimiKasa vlera = new clsVleraKonfigurimiKasa();
                bool printoBarkod = bool.Parse(kasa.OColVlerat.ktheVlereOpsioni("PRINTBARKOD"));
                bool printoKodArtikull = bool.Parse(kasa.OColVlerat.ktheVlereOpsioni("printoKodArtikulli"));
                bool printoPershkrim2 = bool.Parse(kasa.OColVlerat.ktheVlereOpsioni("PRINTOPERSHKRIM2"));
                string extens = Path.GetExtension(filename);
                string kf_filenameCopy = filename.Substring(0, filename.Length - extens.Length + 1) + this.NrDok.Replace('\\', '_').Replace('/', '_') + " " + System.DateTime.Now.ToString().Replace('/', ' ').Replace(':', ' ') + extens;
                string text = string.Empty;
                text = kf_filenameCopy + "&&";
                if (this.kupon)
                {
                    if (this.nrSerial == string.Empty || this.nrSerial == null)
                        return new clsMesazh(false, "Duhet te vendosni nje Serial fature per kuponat me fature tatimore");
                    text += "INP NUM=" + this.nrSerial + ",TERM=TSFATS" + "||";
                }

                if (kaRreshtaKthimi(this))
                {
                    this.OColTrupiShitje = rendit(this.OColTrupiShitje);
                }
                bool meShifraDhjetore = kasa.OColVlerat.ktheVlereOpsioni("MESHIFRADHJETORE") == "True";
                foreach (clsTrupiShitje t in this.OColTrupiShitje)
                {
                    if (t.Kodi == null)
                        continue;
                    QPZ = t.Sasia.ToString();
                    if (t.Zbritje > 0)    //kur kemi zbritje
                    {
                        if (true) //per ndermarrjet e konfiguruara me tvsh
                        {
                            if (t.Sasia < 0)
                            {
                                //kur kemi kthim ja perfshijme zbritjen ne cmim se perndryshe nuk e pranon kasa
                                //PPZI = Convert.ToString(((t.Cmimi) * (100 - (t.Zbritje)) / 100) * this.Kursi);
                                if (metvsh)
                                    cmimi = Convert.ToDouble(((t.Cmimi) * (100 - (t.Zbritje)) / 100) * this.Kursi);
                                else
                                    cmimi = Convert.ToDouble(t.VleftaMeTvsh / Math.Abs(t.Sasia) * this.Kursi);
                                ZPZ = 0;
                            }
                            else
                            {
                                if (metvsh)
                                    cmimi = (t.Cmimi * this.Kursi);
                                else
                                    cmimi = Convert.ToDouble(t.VleftaMeTvsh / (t.Sasia * (1 - t.Zbritje / 100)) * this.Kursi);
                                ZPZ = (t.Zbritje);
                            }
                        }
                        //else TODO: per ndermarrjet pa tvsh
                        //{
                        //    if (t.Sasia < 0)  //cmimi pa tvsh
                        //    {
                        //        PPZI = ((t.Cmimi * (100 - (t.Zbritje)) / 100) * this.Kursi).ToString();
                        //        ZPZ = 0;
                        //    }
                        //    else
                        //    {    //cmimi pa tvsh
                        //        PPZI = (t.Cmimi * this.Kursi).ToString();
                        //        ZPZ = (t.Zbritje);
                        //    }

                        //}
                    }
                    else   //kur nuk ka zbritje
                    {
                        if (metvsh)
                            cmimi = (t.Cmimi * this.Kursi);   //ose cmim pa tvsh kur ndermarja te konfigurohet me cmim me tvsh apo pa tvsh
                        else
                            cmimi = Convert.ToDouble(t.VleftaMeTvsh / (t.Sasia * (1 - t.Zbritje / 100)) * this.Kursi);
                        ZPZ = 0;
                    }
                    if (meShifraDhjetore) //kasa eshte konfiguruar pa presje dhjetore, i dergohen cmimet te konvertuar ne numra te plote
                        PPZI = Math.Abs(cmimi).ToString("F2", System.Globalization.CultureInfo.InvariantCulture);
                    else
                        PPZI = Math.Abs(cmimi).ToString("F0", System.Globalization.CultureInfo.InvariantCulture);

                    DESIC = clsFunksione.kthePershkrimArtikullPerKasen(2, printoKodArtikull, t.Kodi, printoPershkrim2, t.Pershkrimi, t.Pershkrim2, 30);

                    //TVSHKOD = merrNivelTvsh(t, this.idNdermarje, idperdoruesi);
                    TVSHKOD = kasa.OColVlerat.ktheVlereTakse(t.Tvsh);
                    if (TVSHKOD == "0")
                        TVSHKOD = "1";
                    //Shkruhet ne file
                    if (decimal.Parse(QPZ) < 0)
                    {
                        descria = "vend rep=" + TVSHKOD + ",des='" + DESIC + "',qty=" + Math.Abs(decimal.Parse(QPZ)) + ",prezzo=" + PPZI + ",reso";
                        text += descria + "||";
                        //sw.WriteLine(descria);
                        Kthim = true;
                    }
                    else
                    {
                        if (ZPZ != 0)
                        {
                            descria = "vend rep=" + TVSHKOD + ",des='" + DESIC + "',qty=" + Math.Abs(decimal.Parse(QPZ)) + ",prezzo=" + PPZI;
                            text += descria + "||"; //sw.WriteLine(descria);
                            descria = "PERCA ALI=" + ZPZ;
                            text += descria + "||"; //sw.WriteLine(descria);
                        }
                        else
                        {
                            descria = "vend rep=" + TVSHKOD + ",des='" + DESIC + "',qty=" + Math.Abs(decimal.Parse(QPZ)) + ",prezzo=" + PPZI;
                            text += descria + "||"; //sw.WriteLine(descria);
                        }
                    }

                    if (printoBarkod != false)
                    {
                        string kodBari = string.Empty;
                        colKodbare kodBare = new colKodbare(t.IdKodi);
                        foreach (clsKodbari kodBar in kodBare)
                        {
                            kodBari += kodBar.Pershkrimi + ",";
                        }
                        descria = "PRMSG RIGA='" + kodBari + "'";
                        text += descria + "||"; //sw.WriteLine(descria);
                    }
                }
                //Per Zbritjen ne totatin e fatures
                if (perqindja > 0)
                {
                    descria = "PERCA ALI=" + perqindja + ",SUBTOT";
                    text += descria + "||"; //sw.WriteLine(descria);
                }
                //Lloji i pageses
                LLPAG = (MenyrePagese)this.IdMenyrePagese;
                if (int.Parse(kasa.OColVlerat.ktheVlereOpsioni("CHIUS")) == 1)
                {

                    switch (LLPAG)
                    {
                        case MenyrePagese.Me_mirebesim:
                        case MenyrePagese.Banke:
                        case MenyrePagese.Me_parapagim:
                            {
                                descria = "chius T=2";     //mirebesim
                                text += descria + "||"; //sw.WriteLine(descria);
                                break;
                            }

                        case MenyrePagese.Pagese:
                        case MenyrePagese.Pagese_Automatike:
                        case MenyrePagese.Arke:

                            {
                                descria = "chius T=1";       //kesh
                                text += descria + "||"; //sw.WriteLine(descria);
                                break;
                            }

                        case MenyrePagese.Karte_krediti:
                            {
                                descria = "chius T=5";   //kartekrediti
                                text += descria + "||"; //sw.WriteLine(descria);
                                break;
                            }
                        default:

                            descria = "chius T=1";
                            text += descria + "||";

                            break;
                    }
                    //Printon Kthimin ne dy kopje 
                    if (Kthim == true)
                    {
                        descria = "inp num=5, term=35";
                        text += descria + "||"; //sw.WriteLine(descria);
                    }
                }

                //ne varesi te konfigurimit ne KonfigPOS.xml, fshijme ose ruajme kopjen
                string ruajKopje = kasa.OColVlerat.ktheVlereOpsioni("RUAJKOPJE");

                text += "&&" + filename + "&&" + ruajKopje;
                stream = text;
                return new clsMesazh(true, clsKokaShitje.krijimStreamKaseMeSukses);

            }
        }

        public clsMesazh gjeneroFatureFiskaleBtn(out string stream, clsKonfigurimKase kasa, double perqindja, string totalimonbaze, int idperdoruesi, string totatimezbritjemetvsh, bool metvsh, int idKonfigurimiKases)
        {
            stream = string.Empty;
            if (double.Parse(totatimezbritjemetvsh) < 0)
                return new clsMesazh(false, "Kasa nuk pranon fature me total negativ. Fatura nuk u regjistrua ne kase!");
            else
            {
                string QPZ; //sasia
                double PPZI; //cmimi
                string DESIC; //pershkrimi
                string descria; //rreshti qe do te shkruhet ne kase
                int kaseLogicalNumber;
                string filename = kasa.OColVlerat.ktheVlereOpsioni("KASEFISKALEPATH");
                clsVleraKonfigurimiKasa vlera = new clsVleraKonfigurimiKasa();
                kaseLogicalNumber = int.Parse(kasa.OColVlerat.ktheVlereOpsioni("KASEFISKALELOGICALNR"));
                bool eshtePrinteFiskal = (kasa.OColVlerat.ktheVlereOpsioni("KASEAPOPRINTER") == "False"); // kaseapoprinter eshte false per printer, eshte true per kase

                if (!eshtePrinteFiskal)
                {
                    if (filename.LastIndexOf("\\") != 0)
                    {
                        filename = filename.Substring(0, filename.LastIndexOf("\\") + 1) + "rcpt.txt";
                    }
                    else if (filename.LastIndexOf("/") != 0)
                    {
                        filename = filename.Substring(0, filename.LastIndexOf("/") + 1) + "rcpt.txt";
                    }
                }

                string text = string.Empty;
                text = filename + "&&";
                //text += "H,1,______,_,__;||"; p //hap kase
                //    StreamWriter sw = File.CreateText(filename);
                //if (kasa.OColVlerat.ktheVlereOpsioni("KUPONTATIMOR") == "True" && !eshtePrinteFiskal)  //p nr i fatures
                if (kasa.OColVlerat.ktheVlereOpsioni("KUPONTATIMOR") == "False" && eshtePrinteFiskal)  //nr i fatures 
                    text += "Q,0,______,_,__;" + this.NrDok.Substring(Math.Max(0, this.NrDok.Length - 4)) + ";||";

                // sw.WriteLine("M,1,______,_,__;" + this.NrDok + ";");
                if (kasa.OColVlerat.ktheVlereOpsioni("PRINTONRFATURE") == "True")
                    if (eshtePrinteFiskal)
                    {
                        if (!string.IsNullOrEmpty(this.NrSerial)) text += "P," + kaseLogicalNumber + ",______,_,__;Serial:" + this.NrSerial + "||"; // sw.WriteLine("P," + kaseLogicalNumber + ",______,_,__;Serial:" + this.NrSerial);
                    }
                //else
                //    if (!string.IsNullOrEmpty(this.NrSerial)) text += "P," + kaseLogicalNumber.ToString() + ",______,_,__;Serial:" + this.NrSerial + ";;;;;||"; // sw.WriteLine("P," + kaseLogicalNumber.ToString() + ",______,_,__;Serial:" + this.NrSerial + ";;;;;");
                if (kaRreshtaKthimi(this))
                {
                    this.OColTrupiShitje = rendit(this.OColTrupiShitje);
                }
                int i = 0;

                clsVleraKonfigurimiKasa vlKonf = new clsVleraKonfigurimiKasa();
                int plu;
                plu = int.Parse(vlKonf.merrPLUActualNumberPerKase(this.idNdermarje, idKonfigurimiKases));
                if (plu == -1)
                    plu = 1;

                foreach (clsTrupiShitje t in this.OColTrupiShitje)
                {
                    if (t.Kodi == null)
                        continue;
                    i++;
                    QPZ = (Math.Abs(t.Sasia)).ToString();
                    //llogaritet cmimi qe shkon ne kasen fiskale

                    //if (true)//nese ndermarrja eshte konfiguruar me cmimet me tvsh, merret nga grida cmimi i dhene dhe shumezohet me kursin, qe te printohet vlera ne mon baze

                    //{
                    //    PPZI = t.Cmimi * this.Kursi;
                    //}
                    //else TODO:llogarit cmimin ne rastin pa tvsh
                    //{
                    //    //llogaritet cmimi si (cmim pa tvsh * perqindje tvsh per artikullin) dhe shumezohet me kursin, qe te printohet vlera ne mon baze
                    //    PPZI = t.Cmimi * (1 + t.Zbritje / 100) * this.Kursi;
                    //}

                    //nese ndermarrja eshte konfiguruar me cmimet me tvsh, merret nga grida cmimi i dhene dhe shumezohet me kursin, qe te printohet vlera ne mon baze
                    if (metvsh)
                        PPZI = t.Cmimi * this.Kursi;
                    else
                        PPZI = (t.VleftaMeTvsh / (t.Sasia * (1 - t.Zbritje / 100))) * this.Kursi;

                    double cmimSipasKonfigKase;
                    if (kasa.OColVlerat.ktheVlereOpsioni("MESHIFRADHJETORE") == "False")
                        cmimSipasKonfigKase = PPZI;  //kasa eshte konfiguruar pa presje dhjetore, i dergohen cmimet te konvertuar ne numra te plote
                    else
                        cmimSipasKonfigKase = PPZI * 100; //kasa eshte konfiguruar me presje dhjetore, i dergohen cmimet te shumezuar me 100, te konvertuar ne numra te plote, ne menyre qe dy shifrat e fundit te perfaqesojne pjesen dhjetore
                    if (t.Sasia < 0)
                        cmimSipasKonfigKase = -cmimSipasKonfigKase;
                    DESIC = clsFunksione.kthePershkrimArtikullPerKasen(1, false, "", bool.Parse(kasa.OColVlerat.ktheVlereOpsioni("PRINTOPERSHKRIM2")), t.Pershkrimi, t.Pershkrim2, 25);

                    //string niveletvsh = merrNivelTvsh(t, this.idNdermarje, idperdoruesi);
                    string niveletvsh = kasa.OColVlerat.ktheVlereTakse(t.Tvsh);

                    if (!eshtePrinteFiskal)
                    {
                        descria = plu + ";" + DESIC + ";" + niveletvsh + ";" + Math.Round(cmimSipasKonfigKase, 2) * (1 - Math.Round(perqindja, 2) / 100) * (1 - Math.Round(t.Zbritje, 2) / 100) + ";" + QPZ;// +KasePLUActualNumber;
                        //descria = plu + ";" + DESIC + ";" + niveletvsh + ";" + Math.Round((t.VleftaMeTvsh / t.Sasia) * this.Kursi, 2) * (1 - Math.Round(double.Parse(perqindja), 2) / 100) * (1 - Math.Round(t.Zbritje, 2) / 100) + ";" + QPZ;// +KasePLUActualNumber;
                    }
                    else
                        descria = "S," + kaseLogicalNumber + ",______,_,__;" + DESIC + ";" + Math.Round(cmimSipasKonfigKase, 2) + ";" + QPZ + ";1;1;" + niveletvsh + ";0;" + plu + ";";

                    plu++;
                    text += descria + "||";
                    //  sw.WriteLine(descria);
                    //printojme zbritjen analitike, nese ka per artikullin e rradhes
                    if (t.Zbritje > 0 && eshtePrinteFiskal)
                    {
                        descria = "C," + kaseLogicalNumber + ",______,_,__;1;" + Math.Round(t.Zbritje, 2) + ";;;;";
                        text += descria + "||";//   sw.WriteLine(descria);
                    }

                }

                vlKonf.updatePLU(this.idNdermarje, plu, idKonfigurimiKases);

                //printojme zbritjen ne totalin e fatures, nese ka, por fillimisht duhet printuar subtotal
                if (perqindja > 0 && eshtePrinteFiskal)
                {
                    text += "T," + kaseLogicalNumber + ",______,_,__;4;;;;;||";// sw.WriteLine("T," + kaseLogicalNumber + ",______,_,__;4;;;;;");
                    descria = "C," + kaseLogicalNumber + ",______,_,__;1;" + Math.Round(perqindja, 2) + ";;;;";
                    text += descria + "||"; //sw.WriteLine(descria);
                }
                string cmimMonDyte;
                cmimMonDyte = kasa.OColVlerat.ktheVlereOpsioni("CMIMMONEDHEDYTE");
                if (cmimMonDyte != "0" && eshtePrinteFiskal)
                {
                    double kurs;
                    clsKurset kursi = new clsKurset(int.Parse(cmimMonDyte), this.DtDok);
                    //clsMonedha mon = new clsMonedha(int.Parse(cmimMonDyte));
                    kurs = kursi.VleraKursi;
                    if (kurs != -1 && kurs != 0)
                    {
                        text += "P," + kaseLogicalNumber + ",______,_,__;" + clsMonedha.ktheKodMonedheSipasId(int.Parse(cmimMonDyte)) + ":" + Math.Round(double.Parse(totalimonbaze) / kurs, 2).ToString() + ";;;;;||";
                        //  sw.WriteLine("P," + kaseLogicalNumber + ",______,_,__;" + mon.KodiMonedha + ":" + Math.Round(double.Parse(totalimonbaze) / kurs, 2).ToString() + ";;;;;");
                    }
                }
                //mbyllja e fatures
                if (eshtePrinteFiskal)
                    text += "T," + kaseLogicalNumber + ",______,_,__;";//  sw.WriteLine("T," + kaseLogicalNumber + ",______,_,__;");
                else
                {
                    text += string.Empty;
                    text += "Ga;";
                    // sw.WriteLine("T," + kaseLogicalNumber + ",______,_,__;0;");
                }
                // sw.Close();
                //p text += "||F,1,______,_,__;"; //hap kase
                stream = text;

                string mesazhi;
                if (plu < 9000)
                    mesazhi = clsKokaShitje.krijimStreamKaseMeSukses;
                else
                    mesazhi = clsKokaShitje.krijimStreamKaseMeSuksesDheArritjaLimititPLU;

                return new clsMesazh(true, mesazhi);
                //return new clsMesazh(true, clsKokaShitje.mesazhSuksesKase);
            }
        }

        public clsMesazh gjeneroFatureFiskaleBntAclas(out string stream, clsKonfigurimKase kasa, double perqindje, string vleftaPaguar, string totatimezbritjemetvsh, bool meTvsh)
        {
            stream = string.Empty;
            if (double.Parse(totatimezbritjemetvsh) < 0)
                return new clsMesazh(false, "Kasa nuk pranon fature me total negativ. Fatura nuk u regjistrua ne kase!");
            else
            {

                // string filename = kasa.OColVlerat.ktheVlereOpsioni("KASEFISKALEPATH");
                string port = kasa.OColVlerat.ktheVlereOpsioni("COMPORT");
                string boudrate = kasa.OColVlerat.ktheVlereOpsioni("BOUDRATE");
                string text = string.Empty;

                if (vleftaPaguar == "") vleftaPaguar = "0";


                if (kaRreshtaKthimi(this))
                {
                    this.OColTrupiShitje = rendit(this.OColTrupiShitje);
                }
                foreach (clsTrupiShitje t in this.OColTrupiShitje)
                {
                    t.TvshKod = kasa.OColVlerat.ktheVlereTakse(t.Tvsh);

                }

                string fileStream;
                JavaScriptSerializer serializusi = new JavaScriptSerializer();
                serializusi.MaxJsonLength = 50000000;
                fileStream = serializusi.Serialize(this);

                text = fileStream + "&&" + port + "&&" + boudrate + "&&" + meTvsh.ToString() + "&&" + perqindje + "&&" + vleftaPaguar;
                stream = text;
                return new clsMesazh(true, clsKokaShitje.krijimStreamKaseMeSukses);
            }


        }

        public clsMesazh gjeneroFatureFiskaleBntAclasSkedar(out string stream, clsKonfigurimKase kasa, double perqindja, string totalimonbaze, int idperdoruesi, string totatimezbritjemetvsh, bool metvsh)
        {
            stream = string.Empty;
            if (double.Parse(totatimezbritjemetvsh) < 0)
                return new clsMesazh(false, "Kasa nuk pranon fature me total negativ. Fatura nuk u regjistrua ne kase!");
            else
            {
                string text = string.Empty;
                string QPZ; //sasia
                double PPZI; //cmimi
                string DESIC; //pershkrimi
                string descria; //rreshti qe do te shkruhet ne kase
                int kaseLogicalNumber = 1;
                string filename = kasa.OColVlerat.ktheVlereOpsioni("KASEFISKALEPATH");

                text = filename + "&&";


                text += "H,1,______,_,__;";
                text += "||";

                if (kasa.OColVlerat.ktheVlereOpsioni("PRINTONRFATURE") == "True") //fature tatimore
                {
                    text += "54,1,______,_,__;Serial: " + this.nrSerial + ";";
                    text += "||";
                }
                if (kaRreshtaKthimi(this))
                {
                    this.OColTrupiShitje = rendit(this.OColTrupiShitje);  //renditen ne fund rreshtat e kthimit

                }
                foreach (clsTrupiShitje t in this.OColTrupiShitje)
                {
                    if (t.Kodi == null)
                        continue;
                    QPZ = (Math.Abs(t.Sasia)).ToString();  //sasia
                    //llogaritet cmimi qe shkon ne kasen fiskale
                    if (true)    //cmimet me tvsh 
                    {
                        //nese ndermarrja eshte konfiguruar me cmimet me tvsh, merret nga grida cmimi i dhene dhe shumezohet me kursin, qe te printohet vlera ne mon baze                       
                        if (metvsh)
                            PPZI = t.Cmimi * this.Kursi;
                        else
                            PPZI = (t.VleftaMeTvsh / (t.Sasia * (1 - t.Zbritje / 100))) * this.Kursi;
                    }

                    double cmimSipasKonfigKase;
                    //int MeShifraDhjetore;
                    if (kasa.OColVlerat.ktheVlereOpsioni("MESHIFRADHJETORE") == "False")
                    {
                        cmimSipasKonfigKase = PPZI;

                    } //kasa eshte konfiguruar pa presje dhjetore, i dergohen cmimet te konvertuar ne numra te plote
                    else
                    {
                        cmimSipasKonfigKase = PPZI * 100;

                    } //kasa eshte konfiguruar me presje dhjetore, i dergohen cmimet te shumezuar me 100, te konvertuar ne numra te plote, ne menyre qe dy shifrat e fundit te perfaqesojne pjesen dhjetore

                    if (t.Sasia < 0)     //nqs kthime cmimi vete me minus                       
                        cmimSipasKonfigKase = -cmimSipasKonfigKase;

                    DESIC = clsFunksione.kthePershkrimArtikullPerKasen(1, false, "", bool.Parse(kasa.OColVlerat.ktheVlereOpsioni("PRINTOPERSHKRIM2")), t.Pershkrimi, t.Pershkrim2, 25);

                    string niveltvsh = "1";

                    niveltvsh = kasa.OColVlerat.ktheVlereTakse(t.Tvsh);

                    //printojme zbritjen analitike, nese ka per artikullin e rradhes

                    if (t.Zbritje > 0)
                    {
                        descria = "S," + kaseLogicalNumber + ",______,_,__;" + DESIC + ";" + Math.Round(cmimSipasKonfigKase, 2) + ";" + QPZ + ";" + niveltvsh + ";1;" + "0;" + -Math.Round(t.Zbritje, 2) + ";";

                    }
                    else
                        descria = "S," + kaseLogicalNumber + ",______,_,__;" + DESIC + ";" + Math.Round(cmimSipasKonfigKase, 2) + ";" + QPZ + ";" + niveltvsh + ";1;" + "0;0;";
                    text += descria + "||";
                }

                //printojme zbritjen ne totalin e fatures, nese ka, por fillimisht duhet printuar subtotal
                if (perqindja > 0)
                {

                    descria = "51," + kaseLogicalNumber + ",______,_,__;1;1;0;" + -Math.Round(perqindja, 2) + ";";
                    //text += "T," + kaseLogicalNumber + ",______,_,__;4;;;;;||";
                    //descria = "C," + kaseLogicalNumber + ",______,_,__;1;" + Math.Round(perqindja, 2) + ";;;;";
                    text += descria + "||";

                }

                //mbyllja e fatures


                text += String.Format("T,{0},______,_,__;", kaseLogicalNumber) + "||";
                text += "F,1,______,_,__;" + "||";

                stream = text;
                return new clsMesazh(true, clsKokaShitje.krijimStreamKaseMeSukses);
            }

        }

        public clsMesazh gjeneroFatureFiskaleCKVNOKISkedar(out string stream, clsKonfigurimKase kasa, double perqindja, string totalimonbaze, int idperdoruesi, string totatimezbritjemetvsh, bool metvsh)
        {
            stream = string.Empty;
            if (double.Parse(totatimezbritjemetvsh) < 0)
                return new clsMesazh(false, "Kasa nuk pranon fature me total negativ. Fatura nuk u regjistrua ne kase!");
            else
            {
                string text = string.Empty;
                string QPZ; //sasia
                double PPZI; //cmimi
                string DESIC; //pershkrimi
                string descria; //rreshti qe do te shkruhet ne kase

                string filename = kasa.OColVlerat.ktheVlereOpsioni("KASEFISKALEPATH");
                bool meShifraDhjetore = Boolean.Parse(kasa.OColVlerat.ktheVlereOpsioni("MESHIFRADHJETORE"));
                text = filename + "&&";


                text += "F2;1;0;1";


                if (kasa.OColVlerat.ktheVlereOpsioni("PRINTONRFATURE") == "True") //fature tatimore
                    text += "\"" + nrSerial + "\"";
                text += "||";
                if (kaRreshtaKthimi(this))
                {
                    this.OColTrupiShitje = rendit(this.OColTrupiShitje);  //renditen ne fund rreshtat e kthimit

                }
                foreach (clsTrupiShitje t in this.OColTrupiShitje)
                {
                    if (t.Kodi == null)
                        continue;
                    QPZ = (Math.Abs(t.Sasia)).ToString();  //sasia
                    //llogaritet cmimi qe shkon ne kasen fiskale
                    if (true)    //cmimet me tvsh 
                    {
                        //nese ndermarrja eshte konfiguruar me cmimet me tvsh, merret nga grida cmimi i dhene dhe shumezohet me kursin, qe te printohet vlera ne mon baze                       
                        if (metvsh)
                            PPZI = t.Cmimi * this.Kursi;
                        else
                            PPZI = (t.VleftaMeTvsh / (t.Sasia * (1 - t.Zbritje / 100))) * this.Kursi;
                    }

                    double cmimSipasKonfigKase;
                    //int MeShifraDhjetore;
                    if (!meShifraDhjetore)
                    {
                        cmimSipasKonfigKase = PPZI;

                    } //kasa eshte konfiguruar pa presje dhjetore, i dergohen cmimet te konvertuar ne numra te plote
                    else
                    {
                        cmimSipasKonfigKase = PPZI * 100;

                    } //kasa eshte konfiguruar me presje dhjetore, i dergohen cmimet te shumezuar me 100, te konvertuar ne numra te plote, ne menyre qe dy shifrat e fundit te perfaqesojne pjesen dhjetore

                    if (t.Sasia < 0)     //nqs kthime cmimi vete me minus                       
                        cmimSipasKonfigKase = -cmimSipasKonfigKase;
                    DESIC = clsFunksione.kthePershkrimArtikullPerKasen(1, false, "", bool.Parse(kasa.OColVlerat.ktheVlereOpsioni("PRINTOPERSHKRIM2")), t.Pershkrimi, t.Pershkrim2, 25);

                    string niveltvsh = "1";

                    niveltvsh = kasa.OColVlerat.ktheVlereTakse(t.Tvsh);
                    descria = "F2;3;\"" + DESIC + "\";" + Math.Round(cmimSipasKonfigKase, 2) + ";" + niveltvsh + ";" + QPZ;

                    text += descria + "||";
                    //printojme zbritjen analitike, nese ka per artikullin e rradhes

                    if (t.Zbritje > 0)
                    {
                        if (meShifraDhjetore)
                            text += "F2;6;;1;\"Ulja %-\";" + (t.Zbritje * 100).ToString() + ";;356";
                        else
                            text += "F2;6;;1;\"Ulja %-\";" + t.Zbritje + ";;356";
                        text += "||";
                    }

                }
                //printojme zbritjen ne totalin e fatures, nese ka, por fillimisht duhet printuar subtotal
                if (perqindja > 0)
                {
                    text += "F2;4;||";
                    if (meShifraDhjetore)
                    {
                        text += "F2;6;;1;\"Ulja %-\";" + perqindja * 100 + ";;76;345";
                    }
                    else
                    {
                        text += "F2;6;;1;\"Ulja %-\";" + perqindja + ";;76;345";
                    }

                    text += "||";

                }

                //mbyllja e fatures
                text += "F2;2;" + "||";

                stream = text;
                return new clsMesazh(true, clsKokaShitje.krijimStreamKaseMeSukses);


            }

        }
        public clsMesazh gjeneroFatureFiskaleCkvNoki(out string stream, clsKonfigurimKase kasa, double perqindje, string totatimezbritjemetvsh, bool meTvsh)
        {
            stream = string.Empty;
            if (double.Parse(totatimezbritjemetvsh) < 0)
                return new clsMesazh(false, "Kasa nuk pranon fature me total negativ. Fatura nuk u regjistrua ne kase!");
            else
            {

                // string filename = kasa.OColVlerat.ktheVlereOpsioni("KASEFISKALEPATH");
                string port = kasa.OColVlerat.ktheVlereOpsioni("COMPORT");
                string boudrate = kasa.OColVlerat.ktheVlereOpsioni("BOUDRATE");
                string meShifraDhjetore = kasa.OColVlerat.ktheVlereOpsioni("MESHIFRADHJETORE");
                string printoSerial = kasa.OColVlerat.ktheVlereOpsioni("PRINTONRFATURE");
                string text = string.Empty;

                if (kaRreshtaKthimi(this))
                {
                    this.OColTrupiShitje = rendit(this.OColTrupiShitje);
                }
                foreach (clsTrupiShitje t in this.OColTrupiShitje)
                {
                    t.TvshKod = kasa.OColVlerat.ktheVlereTakse(t.Tvsh);

                }

                string fileStream;
                JavaScriptSerializer serializusi = new JavaScriptSerializer();
                serializusi.MaxJsonLength = 50000000;
                fileStream = serializusi.Serialize(this);

                text = fileStream + "&&" + port + "&&" + boudrate + "&&" + meTvsh.ToString() + "&&" + perqindje + "&&" + meShifraDhjetore + "&&" + printoSerial;
                stream = text;
                return new clsMesazh(true, clsKokaShitje.krijimStreamKaseMeSukses);
            }


        }

        public clsMesazh gjeneroFatureFiskaleCkvNoki(out string stream, clsKonfigurimKase kasa, double perqindja, int idperdoruesi, string totatimezbritjemetvsh, bool metvsh)
        {
            stream = string.Empty;
            if (double.Parse(totatimezbritjemetvsh) < 0)
                return new clsMesazh(false, "Kasa nuk pranon fature me total negativ. Fatura nuk u regjistrua ne kase!");
            else
            {
                string QPZ; //sasia
                double PPZI; //cmimi
                string DESIC; //pershkrimi
                string descria; //rreshti qe do te shkruhet ne kase
                string filename = kasa.OColVlerat.ktheVlereOpsioni("KASEFISKALEPATH");
                clsVleraKonfigurimiKasa vlera = new clsVleraKonfigurimiKasa();
                Boolean meShifraDhjetore;
                meShifraDhjetore = (kasa.OColVlerat.ktheVlereOpsioni("MESHIFRADHJETORE") == "True");
                int kaseLogicalNumber;
                kaseLogicalNumber = int.Parse(kasa.OColVlerat.ktheVlereOpsioni("KASEFISKALELOGICALNR"));
                string port = kasa.OColVlerat.ktheVlereOpsioni("COMPORT");
                long boudrate = long.Parse(kasa.OColVlerat.ktheVlereOpsioni("BOUDRATE"));
                //if (!Directory.Exists(filename.Remove(filename.LastIndexOf("\\"))))
                //    {
                //    return new clsMesazh(false, "Direktoria nuk ekziston!");
                //    }
                //krijohet file text
                string text = filename + "&&";
                //  StreamWriter sw = File.CreateText(filename);
                // TransmitPrinterCommand (0, port, boudrate, "F2;1") ;
                if (kasa.OColVlerat.ktheVlereOpsioni("PRINTONRFATURE") == "True")
                    text += "F2;1;;1;\"\"" + this.NrSerial + "\"\"||";  //    sw.WriteLine("F2;1;;1;\"\string.Empty + this.NrSerial + "\"\string.Empty);
                else
                    text += "F2;1||";  //      sw.WriteLine("F2;1");
                if (kaRreshtaKthimi(this))
                {
                    this.OColTrupiShitje = renditrites(this.OColTrupiShitje);
                    //kjo komande tregon qe rreshti ne vazhdim do te regjistrohet si kthim
                    //       TransmitPrinterCommand 0, port, baudRate, "F2;1;3"
                    text += "F2;1;3||"; //  sw.WriteLine("F2;1;3");
                }
                foreach (clsTrupiShitje t in this.OColTrupiShitje)
                {
                    if (t.Sasia > 999999)
                    {
                        return new clsMesazh(false, "Sasi e papranueshme per kasen! Printimi nuk u krye!");
                    }
                    QPZ = (Math.Abs(t.Sasia)).ToString();

                    //llogaritet cmimi qe shkon ne kasen fiskale
                    //if (true)    //cmimet me tvsh 
                    //{
                    //    //nese ndermarrja eshte konfiguruar me cmimet me tvsh, merret nga grida cmimi i dhene dhe shumezohet me kursin, qe te printohet vlera ne mon baze
                    //    PPZI = t.Cmimi * this.Kursi;
                    //}
                    //else
                    //{
                    //    //llogaritet cmimi si (cmim pa tvsh * perqindje tvsh per artikullin) dhe shumezohet me kursin, qe te printohet vlera ne mon baze
                    //    PPZI = t.Cmimi * (1 + t.Zbritje / 100) * this.Kursi;
                    //}
                    if (metvsh) //nqs cmimi eshte metvsh e perfshire duhet vetem ta shumezojme me kursin, perndryshe duhet llogaritur se sa eshte cmimi me tvsh pa marre parasysh zbritjen, sepse ate e llogarit vet kasa.
                        PPZI = t.Cmimi * this.Kursi;
                    else
                        PPZI = (t.VleftaMeTvsh / (t.Sasia * (1 - t.Zbritje / 100))) * this.Kursi;
                    double cmimSipasKonfigKase;
                    if (kasa.OColVlerat.ktheVlereOpsioni("MESHIFRADHJETORE") == "False")
                        cmimSipasKonfigKase = PPZI;  //kasa eshte konfiguruar pa presje dhjetore, i dergohen cmimet te konvertuar ne numra te plote
                    else
                        cmimSipasKonfigKase = PPZI * 100; //kasa eshte konfiguruar me presje dhjetore, i dergohen cmimet te shumezuar me 100, te konvertuar ne numra te plote, ne menyre qe dy shifrat e fundit te perfaqesojne pjesen dhjetor

                    DESIC = clsFunksione.kthePershkrimArtikullPerKasen(1, false, "", bool.Parse(kasa.OColVlerat.ktheVlereOpsioni("PRINTOPERSHKRIM2")), t.Pershkrimi, t.Pershkrim2, 25);

                    //string niveltvsh = merrNivelTvsh(t, this.idNdermarje, idperdoruesi);
                    string niveltvsh = kasa.OColVlerat.ktheVlereTakse(t.Tvsh);
                    descria = "F2;3;\"\"" + DESIC + "\"\";" + Math.Round(cmimSipasKonfigKase, 2) + ";" + niveltvsh + ";" + QPZ;
                    text += descria + "||";    //    sw.WriteLine(descria);
                    //printojme zbritjen analitike, nese ka per artikullin e rradhes
                    if (t.Zbritje > 0)
                    {
                        if (meShifraDhjetore)
                            descria = "F2;6;;1;\"\"Ulja %-\"\";" + (t.Zbritje * 100).ToString() + ";;46;345";
                        else
                            descria = "F2;6;;1;\"\"Ulja %-\"\";" + t.Zbritje + ";;46;345";
                        text += descria + "||";      //       sw.WriteLine(descria);
                    }
                }
                //printojme zbritjen ne totalin e fatures, nese ka, por fillimisht duhet printuar subtotal
                if (perqindja > 0)
                {
                    text += "F2;4||";  //    sw.WriteLine("F2;4");
                    if (meShifraDhjetore)
                    {
                        descria = "F2;6;;1;\"\"Ulja %-\"\";" + perqindja * 100 + ";;76;345";
                    }
                    else
                    {
                        descria = "F2;6;;1;\"\"Ulja %-\"\";" + perqindja + ";;76;345";
                    }
                    text += descria + "||";   //    sw.WriteLine(descria);
                }
                //mbyllja e fatures
                text += "F2;2";   //  sw.WriteLine("F2;2");
                //  if (string.IsNullOrEmpty(GetKonfigPos(KonfigPosNodes.kasefiskaleexecutable)))
                //       clsMenuInfo.ShtoMesazhGabimi(MenuInfo,"Nuk gjendet file-i i ekzekutimit te printimin ne kase!",pnlMesazhi);
                //     else
                {//???? ShellExecute( NULL, "open", "C:\Program Files\Internet Explorer\iexplore.exe -new", NULL, NULL, SW_SHOWNORMAL);
                }
                //         sw.Close();
                stream = text;
                return new clsMesazh(true, clsKokaShitje.krijimStreamKaseMeSukses);
                //return new clsMesazh(true, clsKokaShitje.mesazhSuksesKase);
            }
        }

        public clsMesazh gjeneroFatureFiskalePkp(out string stream, clsKonfigurimKase kasa, double perqindja, string totalimonbaze, int idperdoruesi, string totatimezbritjemetvsh, bool metvsh)
        {
            stream = string.Empty;
            if (double.Parse(totatimezbritjemetvsh) < 0)
                return new clsMesazh(false, "Kasa nuk pranon fature me total negativ. Fatura nuk u regjistrua ne kase!");
            else
            {
                string QPZ; //sasia
                double PPZI; //cmimi
                string DESIC; //pershkrimi
                string descria; //rreshti qe do te shkruhet ne kase
                int kaseLogicalNumber;
                string filename = kasa.OColVlerat.ktheVlereOpsioni("KASEFISKALEPATH");
                clsVleraKonfigurimiKasa vlera = new clsVleraKonfigurimiKasa();
                kaseLogicalNumber = int.Parse(kasa.OColVlerat.ktheVlereOpsioni("KASEFISKALELOGICALNR"));    //meret nr llogjik i kases
                //if (!Directory.Exists(filename.Remove(filename.LastIndexOf("\\"))))
                //    {
                //    return new clsMesazh(false, "Direktoria nuk ekziston!");
                //    }
                //krijohet file text
                string text = filename + "&&";
                // StreamWriter sw = File.CreateText(filename);
                if (kasa.OColVlerat.ktheVlereOpsioni("PRINTONRFATURE") == "True")
                {
                    text += "P,1,______,_,__;Fatura:" + this.NrDok + ";;;;;||";
                    // sw.WriteLine("P,1,______,_,__;Fatura:" + this.NrDok + ";;;;;");

                }
                if (this.kupon && !string.IsNullOrEmpty(this.NrSerial))
                    text += $"M,{kaseLogicalNumber},____,,__;;;;;;;{this.NrSerial};||";//text += "P," + kaseLogicalNumber.ToString() + ",______,_,__;Serial:" + this.NrSerial + ";;;;;||";
                if (kaRreshtaKthimi(this))
                {
                    this.OColTrupiShitje = rendit(this.OColTrupiShitje);
                }
                foreach (clsTrupiShitje t in this.OColTrupiShitje)
                {
                    QPZ = (Math.Abs(t.Sasia)).ToString();
                    //llogaritet cmimi qe shkon ne kasen fiskale
                    //if (true)    //cmimet me tvsh 
                    //{
                    //    //nese ndermarrja eshte konfiguruar me cmimet me tvsh, merret nga grida cmimi i dhene dhe shumezohet me kursin, qe te printohet vlera ne mon baze
                    //    PPZI = t.Cmimi * this.Kursi;
                    //}
                    //else
                    //{
                    //    //llogaritet cmimi si (cmim pa tvsh * perqindje tvsh per artikullin) dhe shumezohet me kursin, qe te printohet vlera ne mon baze
                    //    PPZI = t.Cmimi * (1 + t.Zbritje / 100) * this.Kursi;
                    //}
                    if (metvsh) //nqs cmimi eshte metvsh e perfshire duhet vetem ta shumezojme me kursin, perndryshe duhet llogaritur se sa eshte cmimi me tvsh pa marre parasysh zbritjen, sepse ate e llogarit vet kasa.
                        PPZI = t.Cmimi * this.Kursi;
                    else
                        PPZI = (t.VleftaMeTvsh / (t.Sasia * (1 - t.Zbritje / 100))) * this.Kursi;

                    double cmimSipasKonfigKase;
                    if (kasa.OColVlerat.ktheVlereOpsioni("MESHIFRADHJETORE") == "False")
                        cmimSipasKonfigKase = PPZI;  //kasa eshte konfiguruar pa presje dhjetore, i dergohen cmimet te konvertuar ne numra te plote
                    else
                        cmimSipasKonfigKase = PPZI * 100; //kasa eshte konfiguruar me presje dhjetore, i dergohen cmimet te shumezuar me 100, te konvertuar ne numra te plote, ne menyre qe dy shifrat e fundit te perfaqesojne pjesen dhjetore
                    if (t.Sasia < 0)
                        cmimSipasKonfigKase = -cmimSipasKonfigKase;
                    DESIC = clsFunksione.kthePershkrimArtikullPerKasen(1, false, "", bool.Parse(kasa.OColVlerat.ktheVlereOpsioni("PRINTOPERSHKRIM2")), t.Pershkrimi, t.Pershkrim2, 25);

                    //string niveletvsh = merrNivelTvsh(t, this.idNdermarje, idperdoruesi);
                    string niveletvsh = kasa.OColVlerat.ktheVlereTakse(t.Tvsh);
                    descria = "S," + kaseLogicalNumber + ",______,_,__;" + DESIC + ";" + Math.Round(Math.Abs(cmimSipasKonfigKase), 2) + ";" + QPZ + ";1;1;" + niveletvsh + ";0;0;";
                    text += descria + "||";// sw.WriteLine(descria);
                    //printojme zbritjen analitike, nese ka per artikullin e rradhes
                    if (t.Zbritje > 0)
                    {
                        descria = "C," + kaseLogicalNumber + ",______,_,__;1;" + Math.Round(t.Zbritje, 2) + ";;;;";
                        text += descria + "||";//   sw.WriteLine(descria);
                    }
                }
                //printojme zbritjen ne totalin e fatures, nese ka, por fillimisht duhet printuar subtotal
                if (perqindja > 0)
                {
                    text += "T," + kaseLogicalNumber + ",______,_,__;4;;;;;||"; // sw.WriteLine("T," + kaseLogicalNumber + ",______,_,__;4;;;;;");
                    descria = "C," + kaseLogicalNumber + ",______,_,__;1;" + Math.Round(perqindja, 2) + ";;;;";
                    text += descria + "||";// sw.WriteLine(descria);
                }
                //per opsionin e afishimit te cmimit ne fature sipas nje monedhe te dyte  
                string cmimMonDyte;
                cmimMonDyte = kasa.OColVlerat.ktheVlereOpsioni("CMIMMONEDHEDYTE");
                if (cmimMonDyte != "0")
                {
                    double kurs;
                    clsKurset kursi = new clsKurset(int.Parse(cmimMonDyte), this.DtDok);
                    //clsMonedha mon = new clsMonedha(int.Parse(cmimMonDyte));
                    kurs = kursi.VleraKursi;
                    if (kurs != -1 && kurs != 0)
                    {
                        text += "P," + kaseLogicalNumber + ",______,_,__;" + clsMonedha.ktheKodMonedheSipasId(int.Parse(cmimMonDyte)) + ":" + Math.Round(double.Parse(totalimonbaze) / kurs, 2).ToString() + ";;;;;||";
                        //  sw.WriteLine("P," + kaseLogicalNumber + ",______,_,__;" + mon.KodiMonedha + ":" + Math.Round(double.Parse(totalimonbaze) / kurs, 2).ToString() + ";;;;;");
                    }
                }
                //mbyllja e fatures
                text += "T," + kaseLogicalNumber + ",______,_,__;";
                //    sw.WriteLine("T," + kaseLogicalNumber + ",______,_,__;");
                //    sw.Close();
                stream = text;
                return new clsMesazh(true, clsKokaShitje.krijimStreamKaseMeSukses);
                //return new clsMesazh(true, clsKokaShitje.mesazhSuksesKase);
            }
        }

        public clsMesazh gjeneroFatureFiskaleGekos(out string stream, clsKonfigurimKase kasa, string username, double perqindja, string vlefta, int idperdoruesi, string totatimezbritjemetvsh, bool metvsh)
        {
            //vlefta eshte zbritje pa tvsh, perdoret ne rastin kur ka perqindje
            //totalimezbritjemetvsh eshte totali ne fund fare
            stream = string.Empty;
            if (double.Parse(totatimezbritjemetvsh) < 0)
                return new clsMesazh(false, "Kasa nuk pranon fature me total negativ. Fatura nuk u regjistrua ne kase!");

            //DbCore.clsMesazh msg = validateNivelTvshTotal(this, idperdoruesi);
            //if (msg.Status == false)
            //{
            //    return msg;
            //}

            else
            {
                double QPZ; //sasia
                double PPZI; //cmimi
                double ZbritjeAnalitike; //zbritja analitike e artikujve 
                string DESIC; //pershkrimi
                string descria; //rreshti qe do te shkruhet ne kase
                string kodTvsh;
                bool uPrintuaZbritjaNeTotal = false;
                double ShumaTotale;
                ShumaTotale = 0;
                int kaseLogicalNumber;
                string filename = kasa.OColVlerat.ktheVlereOpsioni("KASEFISKALEPATH");
                clsVleraKonfigurimiKasa vlera = new clsVleraKonfigurimiKasa();
                kaseLogicalNumber = int.Parse(kasa.OColVlerat.ktheVlereOpsioni("KASEFISKALELOGICALNR"));    //meret nr llogjik i kases
                string kodDefaultTvsh;
                kodDefaultTvsh = kasa.OColVlerat.ktheVlereOpsioni("NIVELDEFAULTTVSH");
                int shifraPasPresjes;
                shifraPasPresjes = int.Parse(vlera.merrVlereOpsioni("SHIFRAPASPRESJE"));
                //if (!Directory.Exists(filename.Remove(filename.LastIndexOf("\\"))))
                //    {
                //    return new clsMesazh(false, "Direktoria nuk ekziston!");
                //    }
                //krijohet file text bosh
                //krijojme nje file me emer kf_filename + kohen kur eshte krijuar
                //string extens = Path.GetExtension(filename);
                //filename = String.Format("{0}{1} {2}{3}", filename.Substring(0, filename.Length - extens.Length + 1), this.NrDok, System.DateTime.Now.ToString().Replace('/', ' ').Replace(':', ' '), extens);
                //   StreamWriter sw = File.CreateText(filename);
                filename = filename.Replace(".", this.NrDok.Replace('\\', '_').Replace('/', '_') + ".");
                string text = filename + "&&";
                string operatorPass = kasa.OColVlerat.ktheVlereOpsioni("OPERATORPASS");
                string gjuha = kasa.OColVlerat.ktheVlereOpsioni("GJUHA");
                //hapja e fatures
                //nese operatorPass = string.Empty, atehere nuk dergohet fare komanda e identifikimit te operatorit
                //e leme bosh se ka perplasje me komanden e restos (kur dergon komanden e operatorit, nuk printohet restoja ????)
                if (!string.IsNullOrEmpty(operatorPass))
                    text += "K," + kaseLogicalNumber + ",______,_,__;;" + username + ";" + operatorPass + ";;;;;;" + gjuha + ";||";   //   sw.WriteLine("K," + kaseLogicalNumber + ",______,_,__;;" + username+ ";" + operatorPass + ";;;;;;" + gjuha + ";");
                //printimi i serialit te fatures si pershkrim
                if (kasa.OColVlerat.ktheVlereOpsioni("PRINTONRFATURE") == "True")
                {
                    if (!string.IsNullOrEmpty(this.NrSerial))
                        text += "E," + kaseLogicalNumber + ",______,_,__;Serial:" + this.NrSerial + ";||";// sw.WriteLine("E," + kaseLogicalNumber + ",______,_,__;Serial:" + this.NrSerial + ";");
                }
                //behet nje renditje sipas vleftes , qe rreshtat negative te dalin ne fund
                if (kaRreshtaKthimi(this))
                    this.OColTrupiShitje = rendit(this.OColTrupiShitje);
                int i = 0;
                foreach (clsTrupiShitje t in this.OColTrupiShitje)
                {
                    if (t.Kodi == null)
                        continue;

                    QPZ = Math.Round(t.Sasia, shifraPasPresjes);
                    if (QPZ < 0 && !uPrintuaZbritjaNeTotal)
                    {
                        if (perqindja > 0)
                        {
                            if (kasa.OColVlerat.ktheVlereOpsioni("MESHIFRADHJETORE") == "False") //'kasa eshte konfiguruar pa presje dhjetore, i dergohen cmimet te konvertuar ne numra te plote
                                descria = "C," + kaseLogicalNumber + ",______,_,__;;;" + Math.Round(double.Parse(vlefta), 2);
                            else
                                descria = "C," + kaseLogicalNumber + ",______,_,__;;;" + Math.Round(double.Parse(vlefta) * 100, 2);
                            text += descria + "||";
                            //sw.WriteLine(descria);
                            uPrintuaZbritjaNeTotal = true;
                        }
                    }
                    //llogaritet cmimi qe shkon ne kasen fiskale

                    if (true)    //cmimet me tvsh 
                    {
                        //nese ndermarrja eshte konfiguruar me cmimet me tvsh, merret nga grida cmimi i dhene dhe shumezohet me kursin, qe te printohet vlera ne mon baze
                        //PPZI = (t.VleftaMeTvsh / t.Sasia * this.Kursi);
                        if (metvsh)
                            PPZI = t.Cmimi * this.Kursi;
                        else
                            PPZI = Math.Round(((t.VleftaMeTvsh / (t.Sasia * (1 - t.Zbritje / 100))) * this.Kursi), shifraPasPresjes);
                    }
                    //else TODO: te llogaritet per ndermarrjet patvsh
                    //{
                    //    //llogaritet cmimi si (cmim pa tvsh * perqindje tvsh per artikullin) dhe shumezohet me kursin, qe te printohet vlera ne mon baze
                    //  PPZI = Math.Round((t.Cmimi * (1 + t.Zbritje / 100) * this.Kursi), shifraPasPresjes);
                    //        if (kasa.OColVlerat.ktheVlereOpsioni("MESHIFRADHJETORE") == "False") //'kasa eshte konfiguruar pa presje dhjetore, i dergohen cmimet te konvertuar ne numra te plote
                    //    cmimSipasKonfigKase = PPZI;
                    //else
                    //    cmimSipasKonfigKase = PPZI * 100;  //kasa eshte konfiguruar me presje dhjetore, i dergohen cmimet te shumezuar me 100, te konvertuar ne numra te plote, ne menyre qe dy shifrat e fundit te perfaqesojne pjesen dhjetore

                    //        ShumaTotale = ShumaTotale + Math.Round(((QPZ * cmimSipasKonfigKase * (1 - ZbritjeAnalitike / 100)) * Math.Pow(10, shifraPasPresjes)), shifraPasPresjes) / Math.Pow(10, shifraPasPresjes);      //rounddown
                    // }                   

                    ZbritjeAnalitike = Math.Round(t.Zbritje, shifraPasPresjes);
                    double cmimSipasKonfigKase;

                    if (kasa.OColVlerat.ktheVlereOpsioni("MESHIFRADHJETORE") == "False") //'kasa eshte konfiguruar pa presje dhjetore, i dergohen cmimet te konvertuar ne numra te plote
                        cmimSipasKonfigKase = PPZI;
                    else
                        cmimSipasKonfigKase = PPZI * 100;

                    //ShumaTotale = ShumaTotale + Math.Round((cmimSipasKonfigKase * Math.Pow(10, shifraPasPresjes)), shifraPasPresjes) / Math.Pow(10, shifraPasPresjes);      //rounddown
                    ShumaTotale = ShumaTotale + Math.Round(((QPZ * cmimSipasKonfigKase * (1 - ZbritjeAnalitike / 100)) * Math.Pow(10, shifraPasPresjes)), shifraPasPresjes) / Math.Pow(10, shifraPasPresjes); //rounddown

                    DESIC = clsFunksione.kthePershkrimArtikullPerKasen(1, false, "", bool.Parse(kasa.OColVlerat.ktheVlereOpsioni("PRINTOPERSHKRIM2")), t.Pershkrimi, t.Pershkrim2, 32);

                    kodTvsh = kasa.OColVlerat.ktheVlereTakse(t.Tvsh);
                    //kodTvsh = merrNivelTvsh(t, this.idNdermarje, idperdoruesi);
                    if (kodTvsh == string.Empty)
                    {
                        kodTvsh = kodDefaultTvsh;
                    }
                    if (QPZ < 0)
                    {
                        cmimSipasKonfigKase = cmimSipasKonfigKase * (1 - ZbritjeAnalitike / 100);
                        ZbritjeAnalitike = 0;
                    }
                    //descria = "S," + kaseLogicalNumber + ",______,_,__;" + DESIC + ";" + cmimSipasKonfigKase + ";" + QPZ + ";1;1;" + kodTvsh + ";0;" + rowTrupiShitje["ARTNR"].ToString() + ";" + rowTrupiShitje["Zb"].ToString() + ";";
                    descria = String.Format("S,{0},______,_,__;{1};{2};{3};1;1;{4};0;{5};{6};", kaseLogicalNumber, DESIC, cmimSipasKonfigKase, QPZ, kodTvsh, KthePLU(i, this), ZbritjeAnalitike);
                    text += descria + "||";//   sw.WriteLine(descria);

                    i++;
                }
                ShumaTotale = Math.Round((ShumaTotale * Math.Pow(10, shifraPasPresjes)), shifraPasPresjes) / Math.Pow(10, shifraPasPresjes); //rounddown
                //printojme zbritjen ne totalin e fatures
                //bejme njehere kontroll se mos u printua me siper, para rreshtave te kthimit
                if (perqindja > 0)
                {
                    if (kasa.OColVlerat.ktheVlereOpsioni("MESHIFRADHJETORE") != "False") //'kasa eshte konfiguruar pa presje dhjetore, i dergohen cmimet te konvertuar ne numra te plote
                        //    vlefta = vlefta;
                        //else
                        vlefta = (double.Parse(vlefta) * 100).ToString();
                    ShumaTotale = ShumaTotale - Math.Round(double.Parse(vlefta) * this.Kursi, shifraPasPresjes);
                    if ((!uPrintuaZbritjaNeTotal))
                        text += "C," + kaseLogicalNumber + ",______,_,__;;;" + Math.Round((double.Parse(vlefta) * this.Kursi), shifraPasPresjes) + "||";
                    //sw.WriteLine("C," + kaseLogicalNumber + ",______,_,__;;;" + Math.Round((double.Parse(vlefta) * this.Kursi), shifraPasPresjes));
                }
                //mbyllja e fatures duke kapur dhe menyren e pageses
                double shuma;
                if (kasa.OColVlerat.ktheVlereOpsioni("VLERAPAGUAR") == "0")
                    if (this.IdMenyrePagese == 1)
                        shuma = ShumaTotale;   //shuma e paguar ne tani per tani nuk e kemi
                    else
                        shuma = ShumaTotale;
                else
                    shuma = ShumaTotale;
                switch (this.IdMenyrePagese)
                {
                    case 0: //mirebesim
                    case 5: //automatike
                    case 8://arka
                    case 9://banka
                    case 10://pezull
                        text += "T," + kaseLogicalNumber + ",______,_,__;1;" + shuma + "||";//   sw.WriteLine("T," + kaseLogicalNumber + ",______,_,__;1;" + shuma); 
                        break;
                    case 1:
                    case 4://kesh
                        if (shuma > 0)
                            text += "T," + kaseLogicalNumber + ",______,_,__;;" + shuma + "||";//     sw.WriteLine("T," + kaseLogicalNumber + ",______,_,__;;" + shuma);
                        if (shuma < ShumaTotale)
                            text += "T," + kaseLogicalNumber + ",______,_,__;1;" + (ShumaTotale - shuma) + "||";//     sw.WriteLine("T," + kaseLogicalNumber + ",______,_,__;1;" + (ShumaTotale - shuma));
                        break;
                    case 2: //cek
                        text += "T," + kaseLogicalNumber + ",______,_,__;2;" + shuma + "||";//  sw.WriteLine("T," + kaseLogicalNumber + ",______,_,__;2;" + shuma);
                        break;
                    case 3: //karte krediti
                        text += "T," + kaseLogicalNumber + ",______,_,__;3;" + shuma + "||";// sw.WriteLine("T," + kaseLogicalNumber + ",______,_,__;3;" + shuma);
                        break;
                    default:
                        text += "T," + kaseLogicalNumber + ",______,_,__;1;" + shuma + "||";//   sw.WriteLine("T," + kaseLogicalNumber + ",______,_,__;1;" + shuma); 
                        break;
                }
                bool nrKopjesh = (kasa.OColVlerat.ktheVlereOpsioni("NRKOPJESH") == "True");
                if (nrKopjesh)
                    text += "V," + kaseLogicalNumber + ",______,_,__;m||";// sw.WriteLine("V," + kaseLogicalNumber + ",______,_,__;m");
                text += "V," + kaseLogicalNumber + ",______,_,__;/" + "Mireservini||";
                text += "V," + kaseLogicalNumber + ",______,_,__;#" + string.Empty;
                // sw.WriteLine("V," + kaseLogicalNumber + ",______,_,__;/" + "Mireservini");
                //  sw.WriteLine("V," + kaseLogicalNumber + ",______,_,__;#" + string.Empty);
                //  sw.Close();
                stream = text;
                return new clsMesazh(true, clsKokaShitje.krijimStreamKaseMeSukses);
                //return new clsMesazh(true, clsKokaShitje.mesazhSuksesKase);
            }
        }

        private string KthePLU(int i, clsKokaShitje shitje)
        {
            Random random = new Random();
            int ran = random.Next(1, 999);
            return string.Format("{0:00#}", ran + string.Format("{0:00#}", i) + shitje.IdShitjeKoka.ToString());
        }

        private Boolean kaRreshtaKthimi(clsKokaShitje koka)
        {
            foreach (clsTrupiShitje t in koka.OColTrupiShitje)
            {
                if (t.Sasia < 0)
                    return true;
            }
            return false;

        }

        private colTrupiShitje rendit(colTrupiShitje trupat)
        {
            colTrupiShitje trupiRiPoz = new colTrupiShitje();
            colTrupiShitje trupiRiNeg = new colTrupiShitje();

            foreach (clsTrupiShitje t in trupat)
            {
                if (t.Sasia < 0)
                    trupiRiNeg.Add(t);
                else trupiRiPoz.Add(t);

            }
            trupat = new colTrupiShitje();
            trupat.AddRange(trupiRiPoz);
            trupat.AddRange(trupiRiNeg);
            return trupat;
        }

        private colTrupiShitje renditrites(colTrupiShitje trupat)
        {
            colTrupiShitje trupiRiPoz = new colTrupiShitje();
            colTrupiShitje trupiRiNeg = new colTrupiShitje();

            foreach (clsTrupiShitje t in trupat)
            {
                if (t.Sasia < 0)
                    trupiRiNeg.Add(t);
                else trupiRiPoz.Add(t);

            }
            trupat = new colTrupiShitje();

            trupat.AddRange(trupiRiNeg);
            trupat.AddRange(trupiRiPoz);
            return trupat;
        }

        #endregion

        public static bool mungojneEntiteteNeNdermarrje(int idNdermarrjeOwn, DataTable err, ref int rreshti, int idShitjeKoka)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda mungojneEntiteteNeNdermarrje me parametra idNdermarrjeOwn:{idNdermarrjeOwn}, idShitjeKoka:{idShitjeKoka}");
            if (idNdermarrjeOwn == 0)
            {
                ImbLogger.LogTraceShitje($"Mbaroi metoda mungojneEntiteteNeNdermarrje me parametra idNdermarrjeOwn:{idNdermarrjeOwn}, idShitjeKoka:{idShitjeKoka}");
                return false;
            }

            using (var db = new clsDatabaseRegjistrim())
            {
                DataTable dt = db.mungojneEntiteteNeNdermarrje(idShitjeKoka, idNdermarrjeOwn);
                if (dt == null)
                {
                    ImbLogger.LogTraceShitje($"Mbaroi metoda mungojneEntiteteNeNdermarrje me parametra idNdermarrjeOwn:{idNdermarrjeOwn}, idShitjeKoka:{idShitjeKoka}");
                    return false;
                }

                foreach (DataRow row in dt.Rows)
                {
                    clsFunksione.ShtoNeTabeleGabimesh(err, row["DOKUMENTI"].ToString(), $"{row["ENTITETI"]} {row["KODI"]} nuk eshte krijuar ne magazine", rreshti);
                }
                ImbLogger.LogTraceShitje($"Mbaroi metoda mungojneEntiteteNeNdermarrje me parametra idNdermarrjeOwn:{idNdermarrjeOwn}, idShitjeKoka:{idShitjeKoka}");
                return true;
            }
        }

        public static clsMesazh mungojneEntiteteNeNdermarrje(colTrupiShitje coltrupishitje, int idkl, int idNdermarrjeOwn)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda mungojneEntiteteNeNdermarrje me parametra idNdermarrjeOwn:{idNdermarrjeOwn}");
            if (idNdermarrjeOwn == 0)
            {
                ImbLogger.LogTraceShitje($"Mbaroi metoda mungojneEntiteteNeNdermarrje me parametra idNdermarrjeOwn:{idNdermarrjeOwn}");
                return new MesazhGabimi("Nuk ekziston nje ndermarrje Own!");
            }
            using (var db = new clsDatabaseRegjistrim())
            {
                DataTable dt = new DataTable();
                var dta = "";
                foreach (clsTrupiShitje trup in coltrupishitje)
                {
                    dt = db.mungojneEntiteteNeNdermarrje(trup.Kodi, trup.Tvsh, idkl);

                    if (dt == null || dt.Rows.Count == 0)
                        continue;
                    ImbLogger.LogTraceShitje($"Mbaroi metoda mungojneEntiteteNeNdermarrje me parametra idNdermarrjeOwn:{idNdermarrjeOwn}");

                    return new MesazhGabimi($"{dt.Rows[0]["ENTITETI"]} {dt.Rows[0]["KODI"]} nuk eshte krijuar ne magazine");

                }
                ImbLogger.LogTraceShitje($"Mbaroi metoda mungojneEntiteteNeNdermarrje me parametra idNdermarrjeOwn:{idNdermarrjeOwn}");
                return new MesazhSuksesi();
            }
        }

        public static int merrIdTrupiShitjeNeOwnShop(int idshitjekoka, string kodArtikulli)
        {
            using (var db = new clsDatabaseRegjistrim())
                return db.merrIdTrupiShitjeNeOwnShop(idshitjekoka, kodArtikulli);

        }

        public static bool eshteGjeneruarDokumentiNgaNdermarrjaMeme(int idkokashitje, int idkonfig)
        {
            using (var db = new clsDatabaseRegjistrim())
                return db.eshteGjeneruarDokumentiNgaNdermarrjaMeme(idkokashitje, idkonfig);
        }

        public static void KonvertoDokumentaShitjeNeDokMagazine(List<int> ids, int idPerdorues, DataTable err, ref int nrrreshti, int idNdermarrjeOwn, ref int nrOk, DbData dbData, bool meTransaksion)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda KonvertoDokumentaShitjeNeDokMagazine me parametra int idPerdorues:{idPerdorues}, idNdermarrjeOwn:{idNdermarrjeOwn}");
            try
            {
                if (idNdermarrjeOwn == 0)
                {
                    clsFunksione.ShtoNeTabeleGabimesh(err, DateTime.Now.ToShortDateString(), "Nuk ka asnje ndermarrje own te konfiguruar", nrrreshti);
                    ImbLogger.LogTraceShitje($"Mbaroi metoda KonvertoDokumentaShitjeNeDokMagazine me parametra int idPerdorues:{idPerdorues}, idNdermarrjeOwn:{idNdermarrjeOwn}");
                    return;
                }
                var mesazh = new clsMesazh();

                var dbshare = new clsDatabaseShare(dbData);
                var dbInv = new clsDatabaseInventari(dbData);
                var dbRegj = new clsDatabaseRegjistrim(dbData);
                var konfigFDTK = new clsKonfigurimAmbjenti(clsServerConfiguration.LexoKonfigurimSipasKey<string>(ServerKonfigKey.KONFIGURIM_TRANSFERIM_USH_FDTK), idNdermarrjeOwn, dbshare);
                var konfigFHTK = new clsKonfigurimAmbjenti(clsKusht.kthevlereSipasKushtitDheIdKonfig(konfigFDTK.IdKonfigAmbjente, "ZFH"), dbshare);
                colSerialeUnikeKategori kategorite = new colSerialeUnikeKategori(idNdermarrjeOwn);
                foreach (var id in ids)
                {
                    var shitje = new clsKokaShitje(id);

                    shitje.mbushTrupShitje();
                    var magazina = new clsKokaMagazina();
                    var magazinaDest = new clsKokaMagazina();
                    var konfiShitje = new clsKonfigurimAmbjenti(shitje.IdKonfigAmbjente, dbshare);
                    if (shitje.IdStatusDok == 4)
                    {
                        clsFunksione.ShtoNeTabeleGabimesh(err, konfiShitje.KodKonfigAmbjente + " " + shitje.NrDok + " " + shitje.DtDok.ToShortDateString(), $"Dokumenti numer {shitje.nrDok} eshte me statusin Refuzuar dhe nuk mund te transferohet!", nrrreshti);
                        continue;
                    }
                    var trupiFDTK = new colTrupiMagazina();
                    var trupiFHTK = new colTrupiMagazina();
                    int index = 0;

                    int idMagDest = -1, idMagOrigjine = -1;
                    string kodMagDest = "", kodMagOrigjine = "";

                    int idRapDesignFDTK = clsAtributeTrupi.merrVleredefaultSipasKontrollitKodKonfigDheNderm<int>(konfigFDTK.KodKonfigAmbjente, idNdermarrjeOwn, "cmbFormatiPrintimit");
                    int idRapDesignFHTK = clsAtributeTrupi.merrVleredefaultSipasKontrollitKodKonfigDheNderm<int>(konfigFHTK.KodKonfigAmbjente, idNdermarrjeOwn, "cmbFormatiPrintimit");
                    bool serialeNeDetajim = clsAlternativaKushti.getAlternativa(konfigFDTK.IdKonfigAmbjente, "TSD1", dbshare) == "Po";
                    int idNderViti = clsNdermarrjeViti.ktheIdNdermarrjeVitiSipasNdermarjesDheKodVitit(idNdermarrjeOwn, shitje.DtDok.Year);
                    bool lejoModifikimDetajimi = clsAlternativaKushti.getAlternativa(konfigFHTK.IdKonfigAmbjente, "LMDET", dbshare) == "Po";
                    int loan = 0;
                    clsKlientFurnitor klient = new clsKlientFurnitor(shitje.IdKlientFurnitor, new clsDatabaseKontabilitet(dbData));
                    if (klient.LlojPorosie == 3)
                        loan = 1;
                    foreach (var trupShitje in shitje.OColTrupiShitje)
                    {

                        var art = new clsArtikulli(trupShitje.Kodi, idNdermarrjeOwn, dbInv);
                        var njesi = new clsNjesiArtikulli(art.Njesi1Artikulli, dbInv);
                        var colSerialet = new DbAsete.colSerialetMagazine();

                        double sasia = trupShitje.Sasia;
                        double cmimi = trupShitje.Cmimi;
                        double vlefta = trupShitje.Cmimi * trupShitje.Sasia;
                        if (art.Klasa == 4)
                        {
                            colArtikulliPerberes artper = new colArtikulliPerberes();
                            artper.ktheArtikujPerberesSipasIdArtikullitKryesorDheDatesMeTeAfert(art.IdArtikulli, shitje.dtDok);
                            double sasiaPerGjitheArtPerberes = 0;
                            for (int j = 0, artPerCount = artper.Count; j < artPerCount; j++)
                            {
                                sasiaPerGjitheArtPerberes += trupShitje.Sasia * (double)artper[j].Koeficienti;
                            }

                            for (int j = 0, artPerCount = artper.Count; j < artPerCount; j++)
                            {
                                DbInventari.clsArtikulliPerberes aper = artper[j];
                                if (aper.Lloji == 1)    //artikull
                                {
                                    double sasiaP = sasia * (double)aper.Koeficienti * ((art.Njesi1Artikulli == njesi.IdNjesia) ? 1 : Convert.ToDouble(art.KoeficientArtikulli));
                                    double cmimiP = trupShitje.VleftaPaTvsh * (1 - shitje.Zbritje) / sasiaPerGjitheArtPerberes;
                                    double vleftaP = sasiaP * cmimiP;
                                    DbInventari.clsArtikulli a = new DbInventari.clsArtikulli(aper.IdLidheseArt, dbInv);
                                    clsNjesiArtikulli njesiP = new clsNjesiArtikulli(a.Njesi1Artikulli, dbInv);
                                    if (a.IdMagazina == 0)
                                    {
                                        clsFunksione.ShtoNeTabeleGabimesh(err, konfiShitje.KodKonfigAmbjente + " " + shitje.NrDok + " " + shitje.DtDok.ToShortDateString(), $"Artikulli {trupShitje.Kodi} nuk ka asnje magazine te lidhur!", nrrreshti);
                                        continue;
                                    }

                                    var magOrigjine = new clsNjesiAdministrative(a.IdMagazina, dbRegj);

                                    var magDestinacion = new clsNjesiAdministrative(trupShitje.IdMagazina, dbRegj);

                                    var magDestinacionOwn = new clsNjesiAdministrative(magDestinacion.Kodi, idNdermarrjeOwn, dbRegj);

                                    if (magOrigjine.IdNjesiAdministrative == 0)
                                    {
                                        clsFunksione.ShtoNeTabeleGabimesh(err, konfiShitje.KodKonfigAmbjente + " " + shitje.NrDok + " " + shitje.DtDok.ToShortDateString(), $"Artikulli {trupShitje.Kodi} nuk ka asnje magazine te lidhur!", nrrreshti);
                                        continue;
                                    }
                                    if (magDestinacionOwn.IdNjesiAdministrative == 0)
                                    {
                                        clsFunksione.ShtoNeTabeleGabimesh(err, konfiShitje.KodKonfigAmbjente + " " + shitje.NrDok + " " + shitje.DtDok.ToShortDateString(), $"Magazina {magDestinacion.Kodi} nuk gjendet ne ndermarrjen tjeter!", nrrreshti);
                                        continue;
                                    }
                                    if (idMagDest == -1)
                                    {
                                        idMagDest = magDestinacionOwn.IdNjesiAdministrative;
                                        kodMagDest = magDestinacionOwn.Kodi;
                                    }
                                    else if (idMagDest != magDestinacionOwn.IdNjesiAdministrative)
                                    {
                                        idMagDest = 0;
                                        kodMagDest = "";
                                    }
                                    if (idMagOrigjine == -1)
                                    {
                                        idMagOrigjine = magOrigjine.IdNjesiAdministrative;
                                        kodMagOrigjine = magOrigjine.Kodi;
                                    }
                                    else if (idMagOrigjine != magOrigjine.IdNjesiAdministrative)
                                    {
                                        idMagOrigjine = 0;
                                        kodMagOrigjine = "";
                                    }



                                    mesazh = ShtoTrupatEMagazines(trupShitje, njesiP, sasiaP, cmimiP, vleftaP, magOrigjine, idNdermarrjeOwn, idPerdorues, magDestinacion, shitje.DtDok, index, konfigFDTK, shitje.Kursi, ref colSerialet, magDestinacionOwn, trupiFDTK, trupiFHTK, kategorite, a, serialeNeDetajim, lejoModifikimDetajimi, art.KodArtikulli, loan);
                                    if (!mesazh)
                                        break;
                                }
                            }
                        }
                        else
                        {
                            var magOrigjine = new clsNjesiAdministrative(art.IdMagazina, dbRegj);
                            var magDestinacion = new clsNjesiAdministrative(trupShitje.IdMagazina, dbRegj);

                            var magDestinacionOwn = new clsNjesiAdministrative(magDestinacion.Kodi, idNdermarrjeOwn, dbRegj);

                            if (magOrigjine.IdNjesiAdministrative == 0)
                            {
                                clsFunksione.ShtoNeTabeleGabimesh(err, konfiShitje.KodKonfigAmbjente + " " + shitje.NrDok + " " + shitje.DtDok.ToShortDateString(), $"Artikulli {trupShitje.Kodi} nuk ka asnje magazine te lidhur!", nrrreshti);
                                continue;
                            }
                            if (magDestinacionOwn.IdNjesiAdministrative == 0)
                            {
                                clsFunksione.ShtoNeTabeleGabimesh(err, konfiShitje.KodKonfigAmbjente + " " + shitje.NrDok + " " + shitje.DtDok.ToShortDateString(), $"Magazina {magDestinacion.Kodi} nuk gjendet ne ndermarrjen tjeter!", nrrreshti);
                                continue;
                            }
                            if (idMagDest == -1)
                            {
                                idMagDest = magDestinacionOwn.IdNjesiAdministrative;
                                kodMagDest = magDestinacionOwn.Kodi;
                            }
                            else if (idMagDest != magDestinacionOwn.IdNjesiAdministrative)
                            {
                                idMagDest = 0;
                                kodMagDest = "";
                            }
                            if (idMagOrigjine == -1)
                            {
                                idMagOrigjine = magOrigjine.IdNjesiAdministrative;
                                kodMagOrigjine = magOrigjine.Kodi;
                            }
                            else if (idMagOrigjine != magOrigjine.IdNjesiAdministrative)
                            {
                                idMagOrigjine = 0;
                                kodMagOrigjine = "";
                            }


                            mesazh = ShtoTrupatEMagazines(trupShitje, njesi, sasia, cmimi, vlefta, magOrigjine, idNdermarrjeOwn, idPerdorues, magDestinacion, shitje.DtDok, index, konfigFDTK, shitje.Kursi, ref colSerialet, magDestinacionOwn, trupiFDTK, trupiFHTK, kategorite, art, serialeNeDetajim, lejoModifikimDetajimi, "", loan);
                        }

                        if (!mesazh)
                        {

                            clsFunksione.ShtoNeTabeleGabimesh(err, konfiShitje.KodKonfigAmbjente + " " + shitje.NrDok + " " + shitje.DtDok.ToShortDateString(), mesazh.PershkrimMesazhi, nrrreshti);
                            continue;
                        }

                        index++;
                    }
                    (string nrAuto, Dictionary<string, object> hiddenFieldPerNrAuto) result = clsFunksione.ktheNrAutoPerKonfigurim(konfigFDTK.IdKonfigAmbjente, shitje.NrDok, "txtNrDok", "NrDok", 510, shitje.DtDok, dbshare);
                    string nrAutom = result.nrAuto;
                    Dictionary<string, object> hiddenFieldPerNrAuto = result.hiddenFieldPerNrAuto;

                    mesazh = magazinaDest.krijoMagazinePerImport("FH", konfigFHTK.KodKonfigAmbjente, klient.KodKlientFurnitor, idMagDest, kodMagDest, shitje.DtDok, nrAutom, 0, "", 6, 0, 0, 0, idNdermarrjeOwn, idNderViti, idPerdorues, DateTime.Now, 1, shitje.Pershkrimi, 0, 0, 0, "", "", "", false, "", "", "", "", trupiFHTK, new clsKokaMagazina(), new clsKokaFleteKontabel(), idRapDesignFHTK, konfigFHTK, idPerdorues, "", MessagesResource.CurrentResourceManager, MessagesResource.Messages.CurrentCultureInfo, true, true, 0, "", "", "", 0, dbData,false,false,0);
                    if (!mesazh)
                    {
                        clsFunksione.ShtoNeTabeleGabimesh(err, konfiShitje.KodKonfigAmbjente + " " + shitje.NrDok + " " + shitje.DtDok.ToShortDateString(), mesazh.PershkrimMesazhi, nrrreshti);
                        continue;
                    }

                    mesazh = magazina.krijoMagazinePerImport("FD", konfigFDTK.KodKonfigAmbjente, klient.KodKlientFurnitor, idMagOrigjine, kodMagOrigjine, shitje.DtDok, nrAutom, 0, "", 6, 0, 0, 0, idNdermarrjeOwn, idNderViti, idPerdorues, DateTime.Now, 2, shitje.Pershkrimi, shitje.IdNivel, shitje.IdKonfigAmbjente, shitje.IdShitjeKoka, "", "", "", false, "", "", "", "", trupiFDTK, magazinaDest, new clsKokaFleteKontabel(), idRapDesignFDTK, konfigFDTK, idPerdorues, "", MessagesResource.CurrentResourceManager, MessagesResource.Messages.CurrentCultureInfo, true, true, shitje.idNdermarje, "", "", "", 0, dbData,false,false,0);
                    if (!mesazh)
                    {
                        clsFunksione.ShtoNeTabeleGabimesh(err, konfiShitje.KodKonfigAmbjente + " " + shitje.NrDok + " " + shitje.DtDok.ToShortDateString(), mesazh.PershkrimMesazhi, nrrreshti);
                        continue;
                    }
                    string shfaqmesazhapolupe = "jo";
                    bool gjithmone = clsAlternativaKushti.getAlternativa(konfigFDTK.IdKonfigAmbjente, "GJKGJ") == "Po";
                    clsKusht kushtamor = new clsKusht(konfigFDTK.IdKonfigAmbjente, "ZDAM");
                    clsKonfigurimAmbjenti konfamortizimi = new clsKonfigurimAmbjenti();
                    konfamortizimi.mbushKonfigAmbjSipasId(kushtamor.Vlera, MessagesResource.Messages.IdGjuha);
                    clsKonfigurimAmbjenti konfamortizimihyrje = new clsKonfigurimAmbjenti();

                    clsKusht kushtamortizim = new clsKusht(konfigFHTK.IdKonfigAmbjente, "ZDAM");
                    konfamortizimihyrje.mbushKonfigAmbjSipasId(kushtamortizim.Vlera, MessagesResource.Messages.IdGjuha);

                    clsPeriudhaKontabel per = new clsPeriudhaKontabel(magazina.DtDok, idNdermarrjeOwn);
                    string mesazhmevonshem = "";
                    if (meTransaksion)
                        mesazh = magazina.ruaj(true, 0, hiddenFieldPerNrAuto, per.IdPeriudha, "", out shfaqmesazhapolupe, false, new DbAsete.colSerialetMagazine(), new DbAsete.colSerialetMagazine(), konfamortizimi, konfamortizimihyrje, gjithmone, false, "", "", "", "", false, false, new int[0], false, false, false, out mesazhmevonshem, shitje.IdShitjeKoka, ref dbData, false, kategorite, serialeNeDetajim, false);
                    else
                    {
                        mesazh = magazina.ruaj(true, 0, hiddenFieldPerNrAuto, per.IdPeriudha, "", konfigFDTK.IdKategori, -1, dbRegj, out shfaqmesazhapolupe, 0, new DbQendraKosto.colTrupiQendraKosto(), false, new colSerialetMagazine(), new colSerialetMagazine(), konfamortizimi, konfamortizimihyrje, -1, 0, new DbQendraKosto.colTrupiQendraKosto(), false, new clsKokaShitje(), false, gjithmone, new colAmortizimiKoka(), 0, false, false, new colTrupiMagazina(), new colAmortizimiKoka(), new int[0], false, false, false, out mesazhmevonshem, kategorite, serialeNeDetajim, false);
                    }
                    // ruaj(eshteTransferim, meKontabilizim, hfNrAutoregjistrime, idPeriudha, pershkrimFK, idLlojDokFK, idDokNgaFK, db, out shfaqmesazhapolupe, 0, new DbQendraKosto.colTrupiQendraKosto(), eshteOwn, serialemag, serialetranf, konfamortizimi, konfamortizimihyrje, iddokngafkam, 0, new DbQendraKosto.colTrupiQendraKosto(), false, new clsKokaShitje(), false, gjithmone, new colAmortizimiKoka(), meKontabilizim, ruajrenditje, modifikim, new colTrupiMagazina(), new colAmortizimiKoka(), idinv, kontrolloIMEIFifo, blerjengadealer, promocione, out mesazhmevonshem, kategorite, serialeNeDetajim, bashkoArtikujt); //perdor ruajtjen me transaksion
                    if (!mesazh)
                    {
                        clsFunksione.ShtoNeTabeleGabimesh(err, konfiShitje.KodKonfigAmbjente + " " + shitje.NrDok + " " + shitje.DtDok.ToShortDateString(), mesazh.PershkrimMesazhi, nrrreshti);
                        continue;
                    }
                    else
                    {
                        nrOk++;
                    }

                    nrrreshti++;
                }

            }
            catch (Exception ex)
            {
                ImbLogger.LogErrorShitje($"Exception:{ex}");
                IMBUtils.Logging.ImbLogger.Error(ex);
                clsFunksione.ShtoNeTabeleGabimesh(err, DateTime.Now.ToShortDateString(), ex.Message, nrrreshti);
            }

            ImbLogger.LogTraceShitje($"Mbaroi metoda KonvertoDokumentaShitjeNeDokMagazine me parametra int idPerdorues:{idPerdorues}, idNdermarrjeOwn:{idNdermarrjeOwn}");

        }

        private static clsMesazh ShtoTrupatEMagazines(clsTrupiShitje trupShitje, clsNjesiArtikulli njesi, double sasia, double cmimi, double vlefta, clsNjesiAdministrative magOrigjine, int idNdermarrjeOwn, int idPerdorues, clsNjesiAdministrative magDestinacion, DateTime DtDok, int index, clsKonfigurimAmbjenti konfigFDTK, double kursi, ref colSerialetMagazine colSerialet, clsNjesiAdministrative magDestinacionOwn, colTrupiMagazina trupiFDTK, colTrupiMagazina trupiFHTK, colSerialeUnikeKategori kategorite, clsArtikulli art, bool serialeNeDetajim, bool lejoModifikimDetajimi, string artSet, int loan)
        {

            var clsTrupiFDTK = new clsTrupiMagazina();
            var clsTrupiFHTK = new clsTrupiMagazina();

            clsMesazh mesazh = clsTrupiFDTK.krijoTrupMagazineNgaImporti(art.KodArtikulli, njesi.KodNjesia, sasia, cmimi, vlefta, magOrigjine.Kodi, false, "", "", idNdermarrjeOwn, idPerdorues, magDestinacion.Kodi, false, DtDok, 1, "", index, konfigFDTK, kursi, 0, ref colSerialet, 0, trupShitje.Shenime, artSet, "", true);

            if (!mesazh)
            {
                return mesazh;
                //clsFunksione.ShtoNeTabeleGabimesh(err, konfiShitje.KodKonfigAmbjente + " " + shitje.NrDok + " " + shitje.DtDok.ToShortDateString(), mesazh.PershkrimMesazhi, nrrreshti);
                //continue;
            }
            clsTrupiFHTK = clsTrupiFDTK.ShallowCopy();


            clsTrupiFHTK.IdMag = magDestinacionOwn.IdNjesiAdministrative;
            trupiFDTK.Add(clsTrupiFDTK);
            var kat = kategorite.MerrKategoriSipasIdFormatit(art.IdFormatSeriali);
            if (!serialeNeDetajim || kat == null || !kat.Kategori.EqualsIgnoreCase(enumKategoriSerialesh.APARATE.ToString()) || !art.DetajimArtikulli)
                trupiFHTK.Add(clsTrupiFHTK);
            else
            {
                if (!kat.Kategori.EqualsIgnoreCase(enumKategoriSerialesh.APARATE.ToString()))
                    trupiFHTK.Add(clsTrupiFHTK);
                else
                {
                    var trupaFHTK = clsTrupiFHTK.ShperndaTrupinSipasSerialeve(idPerdorues, idNdermarrjeOwn, lejoModifikimDetajimi, loan);
                    trupiFHTK.AddRange(trupaFHTK);
                }

            }

            return new MesazhSuksesi();
        }

        public static bool KaMagazinaTePaRuajtura(int idkoka)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda KonvertoDokumentaShitjeNeDokMagazine me parameter idkoka:{idkoka}");
            clsDatabaseRegjistrim dbKokaMagazina = new clsDatabaseRegjistrim();
            bool sukses = dbKokaMagazina.KaMagazinaTePaRuajtura(idkoka);
            dbKokaMagazina.Dispose();
            ImbLogger.LogTraceShitje($"Mbaroi metoda KonvertoDokumentaShitjeNeDokMagazine me parameter idkoka:{idkoka}");
            return sukses;
        }

        public static bool KaTrupiShitjeNgaKokaPerKthim(int idkoka)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda KaTrupiShitjeNgaKokaPerKthim me parameter idkoka:{idkoka}");
            clsDatabaseRegjistrim dbKokaMagazina = new clsDatabaseRegjistrim();
            bool sukses = dbKokaMagazina.KaTrupiShitjeNgaKokaPerKthim(idkoka);
            dbKokaMagazina.Dispose();
            ImbLogger.LogTraceShitje($"Mbaroi metoda KaTrupiShitjeNgaKokaPerKthim me parameter idkoka:{idkoka}");
            return sukses;
        }

        public static bool KaDokumetKthimi(int idkoka)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda KaDokumetKthimi me parameter idkoka:{idkoka}");
            clsDatabaseRegjistrim dbKokaMagazina = new clsDatabaseRegjistrim();
            bool sukses = dbKokaMagazina.KaDokumetKthimi(idkoka);
            dbKokaMagazina.Dispose();
            ImbLogger.LogTraceShitje($"Mbaroi metoda KaDokumetKthimi me parameter idkoka:{idkoka}");
            return sukses;
        }

        public static DbCore.clsMesazh ndryshoStatusTransferimi(int id, StatusTrasferimi status)
        {
            using (var dbregj = new clsDatabaseRegjistrim())
                return dbregj.modifikoKokaShitjeStatusTransferimi(id, status);

        }

        public static DbCore.clsMesazh hidhNeHistorik(int id, clsDatabaseRegjistrim dbregj)
        {
            clsMesazh mesazh = new clsMesazh();

            mesazh = dbregj.hidhNeHistorik(id);

            return mesazh;

        }

        public static DbCore.clsMesazh ShtoNeHistorik(int id, int idPerdoruesi, clsDatabaseRegjistrim dbregj)
        {
            clsMesazh mesazh = new clsMesazh();

            mesazh = dbregj.ShtoNeHistorik(id, idPerdoruesi);

            return mesazh;

        }

        public static DbCore.clsMesazh modifikoKokaShitjeStatusGjenerimi(int id, bool status)
        {
            clsMesazh mesazh = new clsMesazh();
            clsDatabaseRegjistrim dbregj = new clsDatabaseRegjistrim();
            mesazh = dbregj.modifikoKokaShitjeStatusGjenerimi(id, status);
            dbregj.Dispose();
            return mesazh;

        }

        public static bool kaAutorizime(int idkoka, int idperdoruesi)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda kaAutorizime me parametra idkoka:{idkoka}, idperdoruesi:{idperdoruesi}");
            clsDatabaseRegjistrim dbKokaMagazina = new clsDatabaseRegjistrim();
            bool sukses = dbKokaMagazina.kaAutorizimKokaShitje(idkoka, idperdoruesi);
            dbKokaMagazina.Dispose();
            ImbLogger.LogTraceShitje($"Mbaroi metoda kaAutorizime me parametra idkoka:{idkoka}, idperdoruesi:{idperdoruesi}");
            return sukses;
        }

        public static string ktheStatusPorosie(int idkokashitje)
        {
            using (clsDatabaseRegjistrim dbKokaMagazina = new clsDatabaseRegjistrim())
            {
                return dbKokaMagazina.merrStatusPorosieSipasId(idkokashitje);
            }
        }

        public static int ktheIdStatusDok(int idkokashitje)
        {
            using (clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim())
            {
                return dbRegj.ktheIdStatusDokKokaShitje(idkokashitje);
            }
        }

        public static int ktheIdKonfigAmbjente(int idkokashitje)
        {
            using (clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim())
            {
                return dbRegj.ktheIdKonfigAmbjenteKokaShitje(idkokashitje);
            }
        }

        public static int ktheIdMonedhe(int idkokashitje)
        {
            using (clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim())
            {
                return dbRegj.ktheIdMonedheKokaShitje(idkokashitje);
            }
        }

        public static int ktheIdRaportDesign(int idkokashitje)
        {
            using (clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim())
            {
                return dbRegj.ktheIdRaportDesignKokaShitje(idkokashitje);
            }
        }

        public static string ktheNrDok(int idkokashitje)
        {
            using (clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim())
            {
                return dbRegj.ktheNrDokKokaShitje(idkokashitje);
            }
        }

        /// <summary>
        /// Funksion qe kontrollon nese nje dokument eshte dokument i krijuar si konvertim nga niveli i regjistrimit qe i kalohet si parameter.
        /// </summary>
        /// <returns></returns>
        public static bool eshteDokKonvertuarNgaNiveli(int idDok, int idNderm, string kodNiveli)
        {
            clsDatabaseRegjistrim dbKokeShitje = new clsDatabaseRegjistrim();
            bool konvertuar = dbKokeShitje.eshteDokKonvertuarNgaNiveli(idDok, idNderm, kodNiveli);
            dbKokeShitje.Dispose();
            return konvertuar;
        }
        public bool eshteDokILidhur()
        {
            using (clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin())
            {
                return dbAdmin.eshteILidhur(this.idShitjeKoka);
            }
        }

        public static double ktheVlereMbeturPerDok(int idDok, int idNderm, int idKatDok)
        {
            clsDatabaseRegjistrim dbKokeShitje = new clsDatabaseRegjistrim();
            double vlMbetur = dbKokeShitje.ktheVlereMbeturPerDok(idDok, idNderm, idKatDok);
            dbKokeShitje.Dispose();
            return vlMbetur;
        }

        /// <summary>
        /// Nga importi
        /// </summary>
        /// <param name="colurdhra"></param>
        /// <param name="colkonvetimi"></param>
        /// <param name="meme"></param>
        /// <param name="ngaImporti"></param>
        /// <param name="dokGjenerues"></param>
        /// <param name="dbData"></param>
        /// <returns></returns>
        public static colKokaShitje gjeneroFaturePermbledhese(colKokaShitje colurdhra, List<colKonvertimi> colkonvetimi, bool meme, bool ngaImporti, ArrayList dokGjenerues, DbData dbData, int idGjuha, ref DataTable gabime)
        {
            return gjeneroFaturePermbledheseNew(colurdhra, colkonvetimi, meme, ngaImporti, dokGjenerues, DateTime.MinValue, DateTime.MaxValue, dbData, idGjuha, ref gabime, false);
        }
        private static void merrMenyrePageseNgaDokumentiPermbledhes(bool ngaImporti, colKokaShitje colShitjeurdherSipasGrupimeve, out bool merrmenyre, out int menyrefature, out bool eshteKlientSpecifik, out int vleraKushtKlientSpecifik, clsDatabaseShare dbShare)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda merrMenyrePageseNgaDokumentiPermbledhes me parametra ngaImporti:{ngaImporti}, colShitjeurdherSipasGrupimeve.Count:{colShitjeurdherSipasGrupimeve.Count}");
            eshteKlientSpecifik = clsKlientFurnitor.EshteKlientSpecifik(colShitjeurdherSipasGrupimeve[0].IdKlientFurnitor, new DbKontabiliteti.clsDatabaseKontabilitet(dbShare));
            vleraKushtKlientSpecifik = clsKusht.ktheVlereKushti(colShitjeurdherSipasGrupimeve[0].IdKonfigAmbjente, "ZKDSHKLSP", dbShare);
            int idkonfiggjenerues = GetIdKonfigAmbjentiDokumentiPermbledhes(colShitjeurdherSipasGrupimeve[0].IdKonfigAmbjente, ngaImporti, vleraKushtKlientSpecifik, eshteKlientSpecifik, dbShare);
            merrmenyre = true;
            menyrefature = clsKusht.ktheVlereKushti(idkonfiggjenerues, "MPPFPGJ", dbShare);
            if (menyrefature >= 0)
                merrmenyre = false;
            ImbLogger.LogTraceShitje($"Mbaroi metoda merrMenyrePageseNgaDokumentiPermbledhes me parametra ngaImporti:{ngaImporti}, colShitjeurdherSipasGrupimeve.Count:{colShitjeurdherSipasGrupimeve.Count}");
        }
        /// <summary>
        /// kontrollon nqs gjate grupimit sipas idkonfig kemi konfigurime qe gjenerojne te njejtin lloj dokumenti dhe i bashkon ne nje
        /// </summary>
        /// <param name="ngaImporti"></param>
        /// <param name="ushSipasLlojDokumenti"></param>
        /// <param name="indexeperSkip"></param>
        /// <param name="i"></param>
        /// <param name="kk"></param>
        /// <param name="vleraKushtIdKonfigAmbjenti"></param>
        private static void kontrolloFaturePermbledheseTeNjejte(bool ngaImporti, IEnumerable<IGrouping<object, clsKokaShitje>> ushSipasLlojDokumenti, ArrayList indexeperSkip, int i, colKokaShitje kk, int vleraKushtIdKonfigAmbjenti, clsDatabaseShare dbShare)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda kontrolloFaturePermbledheseTeNjejte me parametra ngaImporti:{ngaImporti}, kk.Count = :{kk.Count}, vleraKushtIdKonfigAmbjenti:{vleraKushtIdKonfigAmbjenti}");
            for (int nes = i + 1; nes < ushSipasLlojDokumenti.Count(); nes++)
            {

                colKokaShitje kokatGrupNext = new colKokaShitje();
                kokatGrupNext.AddRange(ushSipasLlojDokumenti.ElementAt(nes));
                int vleraKushtKlientSpecifikNext = clsKusht.ktheVlereKushti(kokatGrupNext[0].IdKonfigAmbjente, "ZKDSHKLSP", dbShare);
                int vleraKushtIdKonfigAmbjentiNext = GetIdKonfigAmbjentiDokumentiPermbledhes(kokatGrupNext[0].IdKonfigAmbjente, ngaImporti, vleraKushtKlientSpecifikNext, false, dbShare);
                if (vleraKushtIdKonfigAmbjenti == vleraKushtIdKonfigAmbjentiNext)
                {
                    kk.AddRange(kokatGrupNext);
                    indexeperSkip.Add(nes);
                }
            }
            ImbLogger.LogTraceShitje($"Mbaroi metoda kontrolloFaturePermbledheseTeNjejte me parametra ngaImporti:{ngaImporti}, kk:{kk.Count}, vleraKushtIdKonfigAmbjenti:{vleraKushtIdKonfigAmbjenti}");
        }
        public static colKokaShitje gjeneroFaturePermbledheseNew(colKokaShitje colurdhra, List<colKonvertimi> colkonvetimi, bool meme, bool ngaImporti, ArrayList dokGjenerues, DateTime dtFillimi, DateTime dtMbarimi, DbData dbData, int idGjuha, ref DataTable gabime, bool gjenerimAutomatik)
        {
            ImbLogger.LogTraceShitje($"Filloi colKokaShitje gjeneroFaturePermbledheseNew per {colurdhra.Count} dokumenta me parametra: meme:{meme}, ngaImporti:{ngaImporti}, dtFillimi:{dtFillimi}, idGjuha:{idGjuha}");
            IEnumerable<IGrouping<object, clsKokaShitje>> urdhershitjesipasgrupimeve = null;
            colKokaShitje colshitje = new colKokaShitje();
            var dbShare = new clsDatabaseShare(dbData);
            if (ngaImporti)
            {
                if (!colurdhra.Any()) return new colKokaShitje();
                urdhershitjesipasgrupimeve = from urdher in colurdhra group urdher by new { urdher.IdKonfigAmbjente, urdher.DtDok, urdher.IdKlientFurnitor };

                colKokaShitje colShitjeurdherSipasGrupimeve = new colKokaShitje(urdhershitjesipasgrupimeve.ElementAt(0));

                bool merrmenyre, eshteKlientSpecifik; int menyrefature, vleraKushtKlientSpecifik;
                merrMenyrePageseNgaDokumentiPermbledhes(ngaImporti, colShitjeurdherSipasGrupimeve, out merrmenyre, out menyrefature, out eshteKlientSpecifik, out vleraKushtKlientSpecifik, dbShare);
                ImbLogger.LogTraceShitje($"Mbaroi colKokaShitje gjeneroFaturePermbledheseNew per {colurdhra.Count} dokumenta me parametra: meme:{meme}, ngaImporti:{ngaImporti}, dtFillimi:{dtFillimi}, idGjuha:{idGjuha}");
                return grupoSipasKushtitNGF(urdhershitjesipasgrupimeve, colkonvetimi, meme, ngaImporti, colshitje, dokGjenerues, merrmenyre, menyrefature, dbData, new DateTime(), idGjuha, ref gabime);
            }
            //Grupimi i pare sipas llojit te dokumentit
            IEnumerable<IGrouping<object, clsKokaShitje>> ushSipasLlojDokumenti = from urdher in colurdhra group urdher by new { urdher.IdKonfigAmbjente };
            ArrayList indexeperSkip = new ArrayList();
            var i = 0;
            foreach (var myUshSipasLloji in ushSipasLlojDokumenti)
            {
                colKokaShitje kk = new colKokaShitje();
                kk.AddRange(myUshSipasLloji);
                if (indexeperSkip.Contains(i))
                {
                    i++;
                    continue;
                }


                int vleraKushtKlientSpecifik = clsKusht.ktheVlereKushti(kk[0].IdKonfigAmbjente, "ZKDSHKLSP", dbShare);
                int vleraKushtIdKonfigAmbjenti = GetIdKonfigAmbjentiDokumentiPermbledhes(kk[0].IdKonfigAmbjente, ngaImporti, vleraKushtKlientSpecifik, false, dbShare);
                kontrolloFaturePermbledheseTeNjejte(ngaImporti, ushSipasLlojDokumenti, indexeperSkip, i++, kk, vleraKushtIdKonfigAmbjenti, dbShare);//todo pse duhet kjo ???
                var ushSipasKlienteve = from urdher in kk group urdher by urdher.IdKlientFurnitor;

                foreach (var myUsh in ushSipasKlienteve)
                {
                    colKokaShitje colShitjeKlientSpecOseJo = new colKokaShitje();
                    colShitjeKlientSpecOseJo.AddRange(myUsh);
                    bool merrmenyre, eshteKlientSpecifik; int menyrefature;

                    merrMenyrePageseNgaDokumentiPermbledhes(ngaImporti, colShitjeKlientSpecOseJo, out merrmenyre, out menyrefature, out eshteKlientSpecifik, out vleraKushtKlientSpecifik, dbShare);
                    //kontrollohet nese eshte grupi me klientet specifik ose jo
                    DateTime dtfund = new DateTime();
                    if (eshteKlientSpecifik)
                    {
                        //nese eshte grupi me klient specifik do te merret parasysh kushti i periudhes permbledhese, perndryshe do grupohet sipas dates se dokumentit
                        string kushtPeriudhePermbledhese = clsAlternativaKushti.getAlternativa(colShitjeKlientSpecOseJo[0].IdKonfigAmbjente, "PPFKLSP", dbShare);
                        //Grupimi sipas kushtit PPFKLSP(Periudha permbledhese per faturat per klientet specifik)
                        switch (kushtPeriudhePermbledhese)
                        {
                            case "1 ditore":
                                if (merrmenyre)
                                    urdhershitjesipasgrupimeve = (from urdher in colShitjeKlientSpecOseJo group urdher by new { urdher.DtDok, urdher.IdKlientFurnitor, urdher.IdMenyrePagese });
                                else
                                    urdhershitjesipasgrupimeve = (from urdher in colShitjeKlientSpecOseJo group urdher by new { urdher.DtDok, urdher.IdKlientFurnitor });
                                break;
                            case "Mujore":
                                dtfund = DateTime.MaxValue;
                                colShitjeKlientSpecOseJo.RemoveAll(x => (!x.dtDok.IsBeforeStartOfCurrentMonth()));

                                if (merrmenyre)
                                    urdhershitjesipasgrupimeve = from urdher in colShitjeKlientSpecOseJo group urdher by new { urdher.DtDok.Month, urdher.IdKlientFurnitor, urdher.IdMenyrePagese };
                                else
                                    urdhershitjesipasgrupimeve = from urdher in colShitjeKlientSpecOseJo group urdher by new { urdher.DtDok.Month, urdher.IdKlientFurnitor };
                                break;
                            case "Periudhe":
                                if (gjenerimAutomatik)
                                    continue;
                                dtfund = dtMbarimi;
                                colShitjeKlientSpecOseJo.RemoveAll(x => (!x.dtDok.eshteBrendaperiudhes(dtFillimi, dtMbarimi)));

                                if (merrmenyre)
                                    urdhershitjesipasgrupimeve = from urdher in colShitjeKlientSpecOseJo group urdher by new { urdher.IdKlientFurnitor, urdher.IdMenyrePagese };
                                else
                                    urdhershitjesipasgrupimeve = from urdher in colShitjeKlientSpecOseJo group urdher by new { urdher.IdKlientFurnitor };
                                break;
                        }
                    }
                    else if (merrmenyre)
                        urdhershitjesipasgrupimeve = from urdher in colShitjeKlientSpecOseJo group urdher by new { urdher.DtDok, urdher.IdKlientFurnitor, urdher.IdMenyrePagese };
                    else
                        urdhershitjesipasgrupimeve = from urdher in colShitjeKlientSpecOseJo group urdher by new { urdher.DtDok, urdher.IdKlientFurnitor };

                    colshitje = grupoSipasKushtitNGF(urdhershitjesipasgrupimeve, colkonvetimi, meme, ngaImporti, colshitje, dokGjenerues, merrmenyre, menyrefature, dbData, dtfund, idGjuha, ref gabime);
                }
            }
            ImbLogger.LogTraceShitje($"Mbaroi colKokaShitje gjeneroFaturePermbledheseNew per {colurdhra.Count} dokumenta me parametra: meme:{meme}, ngaImporti:{ngaImporti}, dtFillimi:{dtFillimi}, idGjuha:{idGjuha}");
            return colshitje;
        }

        private static colKokaShitje grupoSipasKushtitNGF(IEnumerable<IGrouping<object, clsKokaShitje>> urdhershitjesipasgrupimeve, List<colKonvertimi> colkonvetimi, bool meme, bool ngaImporti, colKokaShitje colshitje, ArrayList dokGjenerues, bool merrmenyre, int menyrefature, DbData dbData, DateTime datafundit, int idGjuha, ref DataTable gabime)
        {
            ImbLogger.LogTraceShitje($"Filloi colKokaShitje grupoSipasKushtitNGF me parametra meme:{meme}, ngaImporti:{ngaImporti}, colshitje.Count:{colshitje.Count}, merrmenyre:{merrmenyre}, menyrefature:{menyrefature}, dbData:{dbData}, datafundit:{datafundit}, idGjuha:{idGjuha}");
            var dbShare = new clsDatabaseShare(dbData);
            var dbKont = new clsDatabaseKontabilitet(dbData);
            foreach (var ush in urdhershitjesipasgrupimeve)
            {
                colKokaShitje kokatGrup = new colKokaShitje();
                kokatGrup.AddRange(ush);
                int vleraKushtKlientSpecifik = clsKusht.ktheVlereKushti(kokatGrup[0].IdKonfigAmbjente, "ZKDSHKLSP", dbShare);
                bool eshteKlientSpecifik = clsKlientFurnitor.EshteKlientSpecifik(kokatGrup[0].IdKlientFurnitor, dbKont);
                string kushti = (eshteKlientSpecifik ? "NGFKS" : "NGF");
                var alternativa = clsAlternativaKushti.getAlternativa(kokatGrup[0].IdKonfigAmbjente, kushti, dbShare);
                int vleraKushtIdKonfigAmbjenti = GetIdKonfigAmbjentiDokumentiPermbledhes(kokatGrup[0].IdKonfigAmbjente, ngaImporti, vleraKushtKlientSpecifik, eshteKlientSpecifik, dbShare);
                clsKokaShitje kokashitje;
                switch (alternativa)
                {
                    case "Perdorues":
                        IEnumerable<IGrouping<int, clsKokaShitje>> urdhershitjesipasperdorues = from urdher in kokatGrup group urdher by urdher.IdKrijuesi;
                        foreach (var myUsh in urdhershitjesipasperdorues)
                        {
                            colKokaShitje kp = new colKokaShitje();
                            kp.AddRange(myUsh);
                            try
                            {
                                kokashitje = krijoRegjistrimPermbledheseNew(kp, colkonvetimi, meme, ngaImporti, merrmenyre, menyrefature, eshteKlientSpecifik, vleraKushtIdKonfigAmbjenti, dbData, datafundit, idGjuha);
                            }
                            catch (MyException ex)
                            {
                                ImbLogger.LogErrorShitje($"Exception:{ex}");
                                ImbLogger.Error(ex);
                                object[] arr =
                                    {
                                        " ",
                                        ex.Message, " "
                                    };
                                gabime.Rows.Add(arr);
                                continue;
                            }
                            catch (Exception ex)
                            {
                                ImbLogger.LogErrorShitje($"Exception:{ex}");
                                ImbLogger.Error(ex);
                                object[] arr =
                                    {
                                        " ",
                                        ex.Message, " "
                                    };
                                gabime.Rows.Add(arr);
                                continue;
                            }

                            colshitje.Add(kokashitje);
                            dokGjenerues.Add(kp);
                        }
                        break;
                    case "Magazine":
                        IEnumerable<IGrouping<int, clsKokaShitje>> urdhershitjesipasmagazina = from urdher in kokatGrup group urdher by urdher.IdDegeAdministrative;
                        foreach (var myUsh in urdhershitjesipasmagazina)
                        {
                            colKokaShitje kp = new colKokaShitje();
                            kp.AddRange(myUsh);
                            try
                            {
                                kokashitje = krijoRegjistrimPermbledheseNew(kp, colkonvetimi, meme, ngaImporti, merrmenyre, menyrefature, eshteKlientSpecifik, vleraKushtIdKonfigAmbjenti, dbData, datafundit, idGjuha);
                            }
                            catch (DbCore.MyException ex)
                            {
                                ImbLogger.LogErrorShitje($"Exception:{ex}");
                                ImbLogger.Error(ex);
                                object[] arr =
                                    {
                                        " ",
                                        ex.Message, " "
                                    };
                                gabime.Rows.Add(arr);
                                continue;
                            }
                            catch (Exception ex)
                            {
                                ImbLogger.LogErrorShitje($"Exception:{ex}");
                                ImbLogger.Error(ex);
                                object[] arr =
                                    {
                                        " ",
                                        ex.Message, " "
                                    };
                                gabime.Rows.Add(arr);
                                continue;
                            }

                            colshitje.Add(kokashitje);
                            dokGjenerues.Add(kp);
                        }
                        break;
                    case "kase":
                    case "":
                        try
                        {
                            kokashitje = krijoRegjistrimPermbledheseNew(kokatGrup, colkonvetimi, meme, ngaImporti, merrmenyre, menyrefature, eshteKlientSpecifik, vleraKushtIdKonfigAmbjenti, dbData, datafundit, idGjuha);
                        }
                        catch (DbCore.MyException ex)
                        {
                            ImbLogger.LogErrorShitje($"Exception:{ex}");
                            ImbLogger.Error(ex);
                            object[] arr =
                                {
                                        " ",
                                        ex.Message, " "
                                    };
                            gabime.Rows.Add(arr);
                            continue;
                        }
                        catch (Exception ex)
                        {
                            ImbLogger.LogErrorShitje($"Exception:{ex}");
                            ImbLogger.Error(ex);
                            object[] arr =
                                {
                                        " ",
                                        ex.Message, " "
                                    };
                            gabime.Rows.Add(arr);
                            continue;
                        }

                        colshitje.Add(kokashitje);
                        dokGjenerues.Add(kokatGrup);
                        break;
                    default:
                        ImbLogger.LogErrorShitje("Lloj i panjohur alternative per kushtin " + kushti + " alternativa: " + alternativa);
                        throw new MyException("Lloj i panjohur alternative per kushtin " + kushti + " alternativa: " + alternativa);
                }
            }
            ImbLogger.LogTraceShitje($"Mbaroi colKokaShitje grupoSipasKushtitNGF me parametra meme:{meme}, ngaImporti:{ngaImporti}, colshitje.Count:{colshitje.Count}, merrmenyre:{merrmenyre}, menyrefature:{menyrefature}, dbData:{dbData}, datafundit:{datafundit}, idGjuha:{idGjuha}");
            return colshitje;
        }
        private static int GetIdKonfigAmbjentiDokumentiPermbledhes(int idkonfigambjente, bool ngaImporti, int vleraKushtiZgjedhDokPerKlientSpecifik, bool eshteKlientSpecifik)
        {
            using (clsDatabaseShare dbShare = new clsDatabaseShare())
            {
                return GetIdKonfigAmbjentiDokumentiPermbledhes(idkonfigambjente, ngaImporti, vleraKushtiZgjedhDokPerKlientSpecifik, eshteKlientSpecifik, dbShare);
            }
        }
        private static int GetIdKonfigAmbjentiDokumentiPermbledhes(int idkonfigambjente, bool ngaImporti, int vleraKushtiZgjedhDokPerKlientSpecifik, bool eshteKlientSpecifik, clsDatabaseShare dbShare)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda GetIdKonfigAmbjentiDokumentiPermbledhes me parametra idkonfigambjente:{idkonfigambjente}, ngaImporti:{ngaImporti}, vleraKushtiZgjedhDokPerKlientSpecifik:{vleraKushtiZgjedhDokPerKlientSpecifik}, eshteKlientSpecifik:{eshteKlientSpecifik}");
            int vleraKushtIdKonfigAmbjenti;
            if (ngaImporti)
                vleraKushtIdKonfigAmbjenti = clsKusht.ktheVlereKushti(idkonfigambjente, "ZKDSHI", dbShare);//kane te njejtin konfigurimi sepse keshtu grupohen
            else
            {
                if (vleraKushtiZgjedhDokPerKlientSpecifik == 0 || !eshteKlientSpecifik)
                    vleraKushtIdKonfigAmbjenti = clsKusht.ktheVlereKushti(idkonfigambjente, "ZKDSH", dbShare);//kane te njejtin konfigurimi sepse keshtu grupohen)
                else
                {

                    if (eshteKlientSpecifik)
                        vleraKushtIdKonfigAmbjenti = vleraKushtiZgjedhDokPerKlientSpecifik;
                    else
                        vleraKushtIdKonfigAmbjenti = clsKusht.ktheVlereKushti(idkonfigambjente, "ZKDSH", dbShare);
                }
            }
            ImbLogger.LogTraceShitje($"Mbaroi metoda GetIdKonfigAmbjentiDokumentiPermbledhes me parametra idkonfigambjente:{idkonfigambjente}, ngaImporti:{ngaImporti}, vleraKushtiZgjedhDokPerKlientSpecifik:{vleraKushtiZgjedhDokPerKlientSpecifik}, eshteKlientSpecifik:{eshteKlientSpecifik}");
            return vleraKushtIdKonfigAmbjenti;
        }
        public static clsKokaShitje krijoRegjistrimPermbledheseNew(colKokaShitje colurdhera, List<colKonvertimi> konvertimet, bool meme, bool ngaImporti, bool merrmenyre, int menyrefature, bool eshteKlientSpecifik, int vleraKushtIdKonfigAmbjenti, DbData dbData, DateTime datafundit, int idGjuha)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda krijoRegjistrimPermbledheseNew ne parametra meme:{meme}, ngaImporti:{ngaImporti}, merrmenyre:{merrmenyre}, menyrefature:{menyrefature}, eshteKlientSpecifik:{eshteKlientSpecifik}, vleraKushtIdKonfigAmbjenti:{vleraKushtIdKonfigAmbjenti}, idGjuha:{idGjuha}");
            colKonvertimi colkonvertime = new colKonvertimi();
            var dbShare = new clsDatabaseShare(dbData);
            clsKonfigurimAmbjenti konfigshitje = new clsKonfigurimAmbjenti(vleraKushtIdKonfigAmbjenti, dbShare);
            colTrupiShitje trupatPermbledhes = new colTrupiShitje();
            
            //kane te njejtin klient sepse keshtu grupohen
            DbCore.DbKontabiliteti.clsKlientFurnitor klient = new DbCore.DbKontabiliteti.clsKlientFurnitor(colurdhera[0].IdKlientFurnitor, new clsDatabaseKontabilitet(dbData));
            string emerklienti = klient.EmertimFature; 
            string kontakti = klient.TelKF; 
            string niptKlienti = klient.NiptiKF; 
            string qytetiK = klient.EmriQytetitKF;

            //DbCore.DbAdmin.clsMonedha monedha = new DbCore.DbAdmin.clsMonedha(colurdhera[0].IdMonedha);            
            clsDegeAdministrative dege = new clsDegeAdministrative(colurdhera[0].IdDegeAdministrative);
            clsPikeShitjeFurnizimi pike = new clsPikeShitjeFurnizimi(colurdhera[0].IdPikeShitjeFurnizimi);
            clsGrupimDokumentiKoka grup = new clsGrupimDokumentiKoka(colurdhera[0].IdGrup1);


            double shumakursi = 0;
            double shumatotali = 0;
            double shumazbritje = 0;
            double shumatvsh = 0;
            double shumatotalmezbritje = 0;
            colTrupiShitje trupat = new colTrupiShitje();
            colurdhera.MbushElementTrupi();

            foreach (clsKokaShitje koka in colurdhera)
            {
                shumakursi += koka.Kursi * (koka.Totali - koka.Zbritje); // koka.TotaliMeZbritjeMeTVSH;
                shumatotalmezbritje += (koka.Totali - koka.Zbritje); // koka.TotaliMeZbritjeMeTVSH;
                shumatotali += koka.Totali;
                shumazbritje += koka.Zbritje;
                shumatvsh += koka.Tvsh;
                trupat.AddRange(koka.oColTrupiShitje);
                if (!ngaImporti)
                {
                    clsKonvertimi konv = new clsKonvertimi();
                    konv.IdDokKonvertuar = koka.IdShitjeKoka;
                    konv.IdKonfigAmbjenteKonvertuar = koka.IdKonfigAmbjente;
                    konv.IdKonfigAmbjentePasKonvertimi = konfigshitje.IdKonfigAmbjente;
                    colkonvertime.Add(konv);
                }
            }
            konvertimet.Add(colkonvertime);
            int idMagTemp = -1;
            bool isMagENjejte = true;
            bool fshidok = clsAlternativaKushti.getAlternativa(colurdhera[0].IdKonfigAmbjente, "FDOKPGJDPKLJOSP", dbShare) == "Po";
            if (!ngaImporti && (!fshidok || eshteKlientSpecifik))
            {
                foreach (clsTrupiShitje tr in trupat) //kalojme id per konvertimet
                {
                    tr.IdTrupiKonvertimi = tr.IdShitjeTrupi;
                    if (idMagTemp == -1)
                        idMagTemp = tr.IdMagazina;
                    else
                        if (isMagENjejte && tr.IdMagazina != idMagTemp)
                        isMagENjejte = false;
                }
            }
            else
            {
                IEnumerable<IGrouping<object, clsTrupiShitje>> trupatSipasGrupimeve = from tr in trupat group tr by new { tr.IdLlojVeprimi, tr.Kodi, tr.IdNjesia, tr.IdMagazina, tr.Cmimi, tr.Tvsh };
                foreach (var myTrup in trupatSipasGrupimeve)
                {
                    colTrupiShitje kk = new colTrupiShitje();
                    kk.AddRange(myTrup);
                    double sasia = 0, zbritje = 0, vleftametvsh = 0, vleftapatvsh = 0, zbritjevlere = 0;
                    double vleftaPasZbritjes = 0, vletaParaZbritjes = 0;
                    foreach (clsTrupiShitje tt in kk)
                    {
                        sasia += tt.Sasia;
                        vleftametvsh += tt.VleftaMeTvsh;
                        vleftapatvsh += tt.VleftaPaTvsh;
                        zbritjevlere += tt.ZbritjeVlere;
                        if (idMagTemp == -1)
                            idMagTemp = tt.IdMagazina;
                        else
                            if (isMagENjejte && tt.IdMagazina != idMagTemp)
                            isMagENjejte = false;
                        vleftaPasZbritjes += tt.Cmimi * tt.Sasia * (tt.Zbritje / 100);
                        vletaParaZbritjes += tt.Cmimi * tt.Sasia;
                    }
                    zbritje = vletaParaZbritjes == 0 ? 0 : (vleftaPasZbritjes / vletaParaZbritjes) * 100;
                    clsTrupiShitje trup = new clsTrupiShitje(0, 0, kk[0].IdLlojVeprimi, kk[0].Kodi, kk[0].Pershkrimi, 0, kk[0].IdNjesia, sasia, kk[0].Cmimi, zbritje, vleftametvsh, kk[0].Tvsh, vleftapatvsh, kk[0].IdKodi, kk[0].IdMagazina, kk[0].Gjeresi, kk[0].Gjatesi, kk[0].SasiPermasa, string.Empty, colurdhera[0].dtDok, colurdhera[0].dtDok, 0, 0, 0, 0, 0, 0, 0, kk[0].Element, "", 1, zbritjevlere, 0, 0, kk[0].KodDetajim1, kk[0].KodDetajim2);
                    trupatPermbledhes.Add(trup);
                }
            }
            if (merrmenyre == true)
            {
                if (colurdhera[0].IdMenyrePagese == (int)MenyrePagese.Arke)
                {
                    int menyra = clsKusht.kthevlereSipasKushtitDheIdKonfig(colurdhera[0].IdKonfigAmbjente, "PMPFPDMPA");
                    if (menyra != -1)
                        colurdhera[0].IdMenyrePagese = menyra;
                }
            }
            int idMag = 0;
            string kodMag = string.Empty;
            if (isMagENjejte)
            {
                idMag = colurdhera[0].oColTrupiShitje[0].IdMagazina;
                kodMag = clsNjesiAdministrative.ktheKodiNjesiAdministrativeSipasiDPaAutorizime(idMag);
            }
            double kurs = 1;
            if (shumakursi == 0 && shumatotalmezbritje == 0)
                kurs = colurdhera[0].kursi;
            else kurs = shumakursi / shumatotalmezbritje;
            string shfaqmesazhapolupe, mesazhinformues;
            var data = colurdhera[0].dtDok;
            if (datafundit == DateTime.MaxValue)
                data = new DateTime(colurdhera[0].dtDok.Year, colurdhera[0].dtDok.Month, DateTime.DaysInMonth(colurdhera[0].dtDok.Year, colurdhera[0].dtDok.Month));
            else if (datafundit != new DateTime())
                data = datafundit;
            ImbLogger.LogTraceShitje($"Mbaroi metoda krijoRegjistrimPermbledheseNew ne parametra meme:{meme}, ngaImporti:{ngaImporti}, merrmenyre:{merrmenyre}, menyrefature:{menyrefature}, eshteKlientSpecifik:{eshteKlientSpecifik}, vleraKushtIdKonfigAmbjenti:{vleraKushtIdKonfigAmbjenti}, idGjuha:{idGjuha}");
            return krijoRegjistrimPermbledhese(konfigshitje, klient.IdKlientFurnitor, klient.KodKlientFurnitor, data, colurdhera[0].IdMonedha, clsMonedha.ktheKodMonedheSipasId(colurdhera[0].IdMonedha, new clsDatabaseAdmin(dbData)), kurs, shumazbritje, shumatotali, shumatvsh, colurdhera[0].IdDegeAdministrative, dege.Kodi ?? string.Empty, colurdhera[0].IdPikeShitjeFurnizimi, pike.Kodi ?? string.Empty, (ngaImporti || !(!fshidok || eshteKlientSpecifik)) ? trupatPermbledhes : trupat, colurdhera[0].IdGrup1, grup.Kodi ?? string.Empty, idMag, kodMag, colurdhera[0].IdNdermarrjeVit, colurdhera[0].IdPerdoruesi, colurdhera[0].IdNdermarrje, StatusAprovimi.Undefined, out shfaqmesazhapolupe, out mesazhinformues, new clsKokaShitje(), new DbCore.DbAsete.colSerialetMagazine(), false, new clsKokaShitje(), meme, ngaImporti, false, merrmenyre ? colurdhera[0].IdMenyrePagese : menyrefature, dbData, idGjuha, emerklienti, kontakti, niptKlienti, qytetiK);
        }

        public static clsKokaShitje krijoRegjistrimPermbledhese(clsKonfigurimAmbjenti KonfigAmbjente, int klienti, string kodklienti, DateTime dtDk, int idMon, string kodMon, double kursi, double zbritje, double totali, double tvsh, int degeadm, string koddege, int idpikeshitje, string kodpikeshitje, colTrupiShitje trupi, int idgrup, string kodgrup, int idmag, string kodmag, int idNderViti, int idPerdoruesi, int idNdermarrje, StatusAprovimi statusAprovimi, out string shfaqmesazhapolupe, out string mesazhinformues, clsKokaShitje kokamema, DbCore.DbAsete.colSerialetMagazine colserialemag, bool kontrolloSasi, clsKokaShitje faturashitjengaurdhershitjamekupontatimor, bool meme, bool ngaImporti, bool gjeneruar, int idMenyrePagese, DbData dbData, int idGjuha, string emerklienti, string kontakti, string niptKlienti, string qytetiK)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda krijoRegjistrimPermbledhese me parametra KonfigAmbjente:{JsonConvert.SerializeObject(KonfigAmbjente)}, klienti:{klienti}, kodklienti:" + kodklienti + $", dtDk:{dtDk}, idMon:{idMon}, kodMon:{kodMon}, kursi:{kursi}, zbritje:{zbritje}, totali:{totali}, tvsh:{tvsh}, degeadm:{degeadm}, koddege:" + koddege + $", idpikeshitje:{idpikeshitje}, kodpikeshitje:" + kodpikeshitje + $", trupi:{JsonConvert.SerializeObject(trupi)}, idgrup:{idgrup}, kodgrup:{kodgrup}, idmag:{idmag}, kodmag:" + kodmag + $", idNderVIti:{idNderViti}, idPerdoruesi:{idPerdoruesi}, idNdermarrje:{idNdermarrje}, kokamema:{JsonConvert.SerializeObject(kokamema)}, kontrolloSasi:{kontrolloSasi}, faturashitjengaurdhershitjamekupontatimor:{JsonConvert.SerializeObject(faturashitjengaurdhershitjamekupontatimor)}, meme:{meme}, ngaImporti:{ngaImporti}, gjeneruar:{gjeneruar}, idMenyrePagese:{idMenyrePagese}, dbData:{dbData}, idGjuha:{idGjuha}");
            clsKokaShitje koka = new clsKokaShitje();

            if (trupi.Count == 0)
            {
                ImbLogger.LogErrorShitje("Trupi i dokumentit nuk duhet të jetë bosh!");
                throw new Exception("Trupi i dokumentit nuk duhet të jetë bosh!");
            }

            int idPeriudhaKont = clsPeriudhaKontabel.ktheIdPeriudheSipasDatesDheNdermarrjes(dtDk, idNdermarrje);
            bool mekontabilizim = false;
            int idStatusDok = 1;
            var dbShare = new clsDatabaseShare(dbData);
            if (ngaImporti)
            {
                if (clsAlternativaKushti.getAlternativa(KonfigAmbjente.IdKonfigAmbjente, "SDI", dbShare) == "Draft")
                {
                    mekontabilizim = false;
                    idStatusDok = 0;
                }
                else
                {
                    mekontabilizim = true;
                    idStatusDok = 1;
                }
            }

            mekontabilizim = clsAlternativaKushti.getAlternativa(KonfigAmbjente.IdKonfigAmbjente, "GJK", dbShare) == "Jo" ? false : true;
            DbCore.DbQendraKosto.clsKokaQendraKosto qend = new DbCore.DbQendraKosto.clsKokaQendraKosto();
            clsKokaRezervime rez = new clsKokaRezervime();
            bool gjeneromeme = clsAlternativaKushti.getAlternativa(KonfigAmbjente.IdKonfigAmbjente, "GJOSHM", dbShare) == "Po";
            bool gjenerobij = false;
            var alternativa = clsAlternativaKushti.getAlternativa(KonfigAmbjente.IdKonfigAmbjente, "GJUBB", dbShare);
            if (alternativa == "Po")
                gjenerobij = true;
            else
            {
                alternativa = clsAlternativaKushti.getAlternativa(KonfigAmbjente.IdKonfigAmbjente, "GJFBB", dbShare);
                if (alternativa == "Po")
                    gjenerobij = true;
            }
            bool tollona = false;
            if (clsAlternativaKushti.getAlternativa(KonfigAmbjente.IdKonfigAmbjente, "RSHTT", dbShare) == "Po")
                tollona = true;
            bool tollonkastrati = false;
            if (clsAlternativaKushti.getAlternativa(KonfigAmbjente.IdKonfigAmbjente, "RSHTTK", dbShare) == "Po")
                tollonkastrati = true;
            bool tollonakastratielektronik = false;
            if (clsAlternativaKushti.getAlternativa(KonfigAmbjente.IdKonfigAmbjente, "RSHTTKE", dbShare) == "Po")
                tollonakastratielektronik = true;
            string llojZevendesimi = clsAlternativaKushti.getAlternativa(KonfigAmbjente.IdKonfigAmbjente, "ZT", dbShare);
            bool autoshitje = false;
            if (clsAlternativaKushti.getAlternativa(KonfigAmbjente.IdKonfigAmbjente, "LAVK", dbShare) == "Po")
                autoshitje = true;

            string formatprintimi = clsAtributeTrupi.merrVleredefaultSipasKontrollitDheKonfigurimit(KonfigAmbjente.IdKonfigAmbjente, "cmbFormatiPrintimit", 506, dbShare);

            bool gjeneroDokMag = false;
            clsKonfigurimAmbjenti konfmag = new clsKonfigurimAmbjenti();
            if (clsAlternativaKushti.getAlternativa(KonfigAmbjente.IdKonfigAmbjente, "GJDM") == "Po")
            {
                gjeneroDokMag = true;
                konfmag = new clsKonfigurimAmbjenti(KonfigAmbjente.IdKonfigurimi, dbShare);
            }

            double uljePerqindje = (totali != 0 ? Math.Round((zbritje / totali) * 100, 2) : 0);
            int IdFormatPrintimi = 0;
            Int32.TryParse(formatprintimi, out IdFormatPrintimi);

            var merrMagazinePerberesi = clsAlternativaKushti.getAlternativa(KonfigAmbjente.IdKonfigAmbjente, "KGJAPMR", dbShare) == "Po";
            string nrDok = "nr";
            string iic = "";
            string nivf = "";
            clsMesazh mesazh = koka.krijoShitje(ref gjeneroDokMag, KonfigAmbjente.IdNivel, 0, KonfigAmbjente.IdKonfigAmbjente, klienti, kodklienti, 1, "", dtDk, nrDok, "", dtDk, idMon, kodMon, kursi, 0, "", dtDk, 0, "", 0, "",
                idMenyrePagese, Enum.GetName(typeof(MenyrePagese), Convert.ToInt32(idMenyrePagese)).Replace('_', ' ').Replace('1', '&'), 0, "", zbritje, totali, tvsh, DateTime.Today, idStatusDok, idNdermarrje, idNderViti, 0, 0, 0, 0, "", "", "", false,
                degeadm, koddege, idpikeshitje, kodpikeshitje, idPerdoruesi, IdFormatPrintimi, trupi, true, KonfigAmbjente.KodKonfigAmbjente, idPeriudhaKont, konfmag, idmag, kodmag, mekontabilizim, idgrup, 0, 0, dtDk, 0, statusAprovimi, idPerdoruesi, 0, out shfaqmesazhapolupe, null,
                qend.ColTrupi, rez.IdKokaRezervimi, out mesazhinformues, gjeneromeme, kokamema, 0, 0, meme, gjenerobij, StatusTrasferimi.PaTransferuar, emerklienti, kontakti, false, false, kodgrup, dtDk, dtDk, 0, 0, "", 0, 0, "", 0, 0, "", "", 0, "", true,
                faturashitjengaurdhershitjamekupontatimor, false, 0, false, tollona, autoshitje, false, dtDk, gjeneruar, tollonkastrati, tollonakastratielektronik, dtDk.Month, clsViti.ktheIdVitPerNdermarrjenSipasKodit(idNdermarrje, Convert.ToString(dtDk.Year)), "", "", true, uljePerqindje, 0, 0, new colFazaKontrate(), 0, new colKlienteFurnitore(), DateTime.Now,
                dbData, llojZevendesimi, string.Empty, false, false, idGjuha, new clsKonfigurimAmbjenti(), -1, niptKlienti, qytetiK, false, 0, null, "", false, 0, false, 0, "", StatusMarreveshje.Inaktive, "", "shtim", DateTime.Now, merrMagazinePerberesi, nrDok, iic, nivf, 0, "", "", "", 0, 0, "");

            if (!mesazh.Status)
            {
                ImbLogger.LogErrorShitje($"Exception:{mesazh.PershkrimMesazhi}");
                throw new Exception(mesazh.PershkrimMesazhi);
            }
            ImbLogger.LogTraceShitje($"Mbaroi metoda krijoRegjistrimPermbledhese me parametra KonfigAmbjente:{JsonConvert.SerializeObject(KonfigAmbjente)}, klienti:{klienti}, kodklienti:" + kodklienti + $", dtDk:{dtDk}, idMon:{idMon}, kodMon:{kodMon}, kursi:{kursi}, zbritje:{zbritje}, totali:{totali}, tvsh:{tvsh}, degeadm:{degeadm}, koddege:" + koddege + $", idpikeshitje:{idpikeshitje}, kodpikeshitje:" + kodpikeshitje + $", idgrup:{idgrup}, kodgrup:{kodgrup}, idmag:{idmag}, kodmag:" + kodmag + $", idNderVIti:{idNderViti}, idPerdoruesi:{idPerdoruesi}, idNdermarrje:{idNdermarrje}, kokamema:{JsonConvert.SerializeObject(kokamema)}, kontrolloSasi:{kontrolloSasi}, faturashitjengaurdhershitjamekupontatimor:{JsonConvert.SerializeObject(faturashitjengaurdhershitjamekupontatimor)}, meme:{meme}, ngaImporti:{ngaImporti}, gjeneruar:{gjeneruar}, idMenyrePagese:{idMenyrePagese}, dbData:{dbData}, idGjuha:{idGjuha}");
            return koka;
        }

        public static clsMesazh AktivizoBundle(string msisdn, string bundleCode, string reason, string bundleCOST, int idPerdoruesi)
        {
            ImbLogger.LogTraceShitje($"Filloi clsMesazh AktivizoBundle me parametra msisdn:" + msisdn + $", bundleCode:" + bundleCode + $", reason:" + reason + $", bundleCOST:" + bundleCOST + $", idPerdoruesi:{idPerdoruesi}");

            if (KonfigurimeStatikeIntegrimi.FakeResponse)
                return new clsMesazh(true);
            //msisdn = "355692223906";
            //bundleCode = "500MBMonthly"; // </ inp:PTP_ID >
            //bundleCOST = "0";
            //reason = "8888";

            clsMesazh mesazh = new clsMesazh(false);
            string url = System.Web.Configuration.WebConfigurationManager.AppSettings["wsAktivzoBundle"];

            clsWebRequest req = new clsWebRequest(url, "SubscribeBundle_WithSoap");
            mesazh = req.SubscribeBundle(msisdn, bundleCode, bundleCOST, reason, idPerdoruesi);
            ImbLogger.LogTraceShitje($"Mbaroi clsMesazh AktivizoBundle me parametra msisdn:" + msisdn + $", bundleCode:" + bundleCode + $", reason:" + reason + $", bundleCOST:" + bundleCOST + $", idPerdoruesi:{idPerdoruesi}");
            return mesazh;
        }
        public static clsMesazh CaktivizoBundle(string msisdn, string bundleCode, string reason, string bundleCOST, int idPerdoruesi)
        {
            ImbLogger.LogTraceShitje($"Filloi clsMesazh CaktivizoBundle me parametra msisdn:" + msisdn + $", bundleCode:" + bundleCode + $", reason:" + reason + $", bundleCOST:" + bundleCOST + $", idPerdoruesi:{idPerdoruesi}");
            //msisdn = "355692223906";
            //bundleCode = "500MBMonthly"; // </ inp:PTP_ID >
            //bundleCOST = "0";
            //reason = "8888";
            if (KonfigurimeStatikeIntegrimi.FakeResponse)
                return new clsMesazh(true);
            clsMesazh mesazh = new clsMesazh(false);
            string url = System.Web.Configuration.WebConfigurationManager.AppSettings["wsAktivzoBundle"];

            clsWebRequest req = new clsWebRequest(url, "SubscribeBundle_WithSoap");
            mesazh = req.RemoveBundle(msisdn, bundleCode, bundleCOST, reason, idPerdoruesi);
            ImbLogger.LogTraceShitje($"Mbaroi clsMesazh CaktivizoBundle me parametra msisdn:" + msisdn + $", bundleCode:" + bundleCode + $", reason:" + reason + $", bundleCOST:" + bundleCOST + $", idPerdoruesi:{idPerdoruesi}");
            return mesazh;
        }

        public static DataTable merrArtikujTeKonvertuarPlotesisht(int idShitjeKoka, int idNdermarje, bool isBlerje)
        {
            using (clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim())
            {
                return dbRegj.merrArtikujTeKonvertuarPlotesisht(idShitjeKoka, idNdermarje, isBlerje);
            }
        }

        public static clsMesazh modifikoStatusdokNeRefuzuar(int idShitjeKoka, string komentRefuzimi)
        {
            using (var scope = new MyTransactionScope())
            using (var dbRegj = new clsDatabaseRegjistrim())
            {
                var mesazh = modifikoStatusdokNeRefuzuar(idShitjeKoka, komentRefuzimi, dbRegj);
                if (mesazh)
                    scope.Complete();

                return mesazh;
            }
        }

        public static clsMesazh modifikoStatusdokNeRefuzuar(int idShitjeKoka, string komentRefuzimi, clsDatabaseRegjistrim dbRegj)
        {
            ImbLogger.LogTraceShitje($"Filloi clsMesazh modifikoStatusdokNeRefuzuar me parametra idShitjeKoka:{idShitjeKoka}, komentRefuzimi:" + komentRefuzimi);

            clsMesazh mesazh = dbRegj.modifikoKokaShitjEIdstatusdokNeRefuzuar(idShitjeKoka, komentRefuzimi);
            if (!mesazh)
            {
                ImbLogger.LogTraceShitje("Mbaroi clsMesazh modifikoStatusdokNeRefuzuar");
                return mesazh;
            }
            mesazh = updateStatusTeDokKonvertuarNga(idShitjeKoka, 4);
            ImbLogger.LogTraceShitje($"Mbaroi clsMesazh modifikoStatusdokNeRefuzuar me parametra idShitjeKoka:{idShitjeKoka}, komentRefuzimi:" + komentRefuzimi);

            return mesazh;
        }

        public static int merrIdShitjeKokaSipasIdTransferimi(int idDokTransferimNga)
        {
            using (clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim())
                return dbRegj.ktheIdShitjeKokaSipasIdTransferimi(idDokTransferimNga);
        }

        public static clsMesazh ruajRefuzim(string idDokImporti, int idDokTransferimNga, string shenime2, bool vjenNgaImportSQL, bool gjeneronFaturePermbledhese, clsDatabaseRegjistrim dbR, string emerTabKoka, string primaryKey, string ndermarrjeKey, int idNdermarrje, DbData dbData)
        {
            ImbLogger.LogTraceShitje($"Filloi clsMesazh ruajRefuzim me parametra idDokImporti:" + idDokImporti + $", idDokTransferimNga:{idDokTransferimNga}, shenime2:" + shenime2 + $", vjenNgaImportSQL:{vjenNgaImportSQL}, gjeneronFaturePermbledhese:{gjeneronFaturePermbledhese}, emerTabKoka:" + emerTabKoka + $", primaryKey:" + primaryKey + $", ndermarrjeKey:" + ndermarrjeKey + $", idNdermarrje:{ndermarrjeKey}");
            clsMesazh mesazh = new clsMesazh(true);
            try
            {
                System.Diagnostics.Stopwatch myWatchTransaksion = new System.Diagnostics.Stopwatch();
                myWatchTransaksion.Start();
                using (var scope = new MyTransactionScope(dbData))
                {

                    mesazh = refuzo(idDokTransferimNga, shenime2, dbR);
                    if (!mesazh.Status)
                    {
                        ImbLogger.LogTraceShitje($"Mbaroi clsMesazh ruajRefuzim me parametra idDokImporti:" + idDokImporti + $", idDokTransferimNga:{idDokTransferimNga}, shenime2:" + shenime2 + $", vjenNgaImportSQL:{vjenNgaImportSQL}, gjeneronFaturePermbledhese:{gjeneronFaturePermbledhese}, emerTabKoka:" + emerTabKoka + $", primaryKey:" + primaryKey + $", ndermarrjeKey:" + ndermarrjeKey + $", idNdermarrje:{ndermarrjeKey}");
                        return mesazh;
                    }

                    if (vjenNgaImportSQL && idDokImporti != string.Empty)
                    {
                        mesazh = updateStatusImporti(idDokImporti, gjeneronFaturePermbledhese, dbR, emerTabKoka, primaryKey, ndermarrjeKey, idNdermarrje);

                        if (!mesazh.Status)
                        {
                            ImbLogger.LogTraceShitje($"Mbaroi clsMesazh ruajRefuzim me parametra idDokImporti:" + idDokImporti + $", idDokTransferimNga:{idDokTransferimNga}, shenime2:" + shenime2 + $", vjenNgaImportSQL:{vjenNgaImportSQL}, gjeneronFaturePermbledhese:{gjeneronFaturePermbledhese}, emerTabKoka:" + emerTabKoka + $", primaryKey:" + primaryKey + $", ndermarrjeKey:" + ndermarrjeKey + $", idNdermarrje:{ndermarrjeKey}");
                            return mesazh;
                        }
                    }
                    scope.Complete(out dbData);
                }
                myWatchTransaksion.Stop();
                string debugMsg = $"TRANSAKSION: {myWatchTransaksion.Elapsed} ";
                System.Diagnostics.Debug.WriteLine(debugMsg);
                ImbLogger.Info(debugMsg);
            }
            catch (Exception ex)
            {
                ImbLogger.LogErrorShitje($"Exception:{ex}");
                ImbLogger.Error(ex);
                mesazh.PershkrimMesazhi = ex.Message;
                mesazh.Status = false;
                ImbLogger.Error(ex, "Ndodhi nje gabim gjate ruajtjes se refuzimit te dokumentit!");
                ImbLogger.LogTraceShitje($"Mbaroi clsMesazh ruajRefuzim me parametra idDokImporti:" + idDokImporti + $", idDokTransferimNga:{idDokTransferimNga}, shenime2:" + shenime2 + $", vjenNgaImportSQL:{vjenNgaImportSQL}, gjeneronFaturePermbledhese:{gjeneronFaturePermbledhese}, emerTabKoka:" + emerTabKoka + $", primaryKey:" + primaryKey + $", ndermarrjeKey:" + ndermarrjeKey + $", idNdermarrje:{ndermarrjeKey}");
                return mesazh;
            }
            ImbLogger.LogTraceShitje($"Mbaroi clsMesazh ruajRefuzim me parametra idDokImporti:" + idDokImporti + $", idDokTransferimNga:{idDokTransferimNga}, shenime2:" + shenime2 + $", vjenNgaImportSQL:{vjenNgaImportSQL}, gjeneronFaturePermbledhese:{gjeneronFaturePermbledhese}, emerTabKoka:" + emerTabKoka + $", primaryKey:" + primaryKey + $", ndermarrjeKey:" + ndermarrjeKey + $", idNdermarrje:{ndermarrjeKey}");
            return mesazh;
        }

        public static clsMesazh refuzo(int idShitjeKoka, string shenime2, clsDatabaseRegjistrim dbR)
        {
            clsMesazh mesazh = modifikoStatusdokNeRefuzuar(idShitjeKoka, shenime2, dbR);

            if (!mesazh.Status)
                return mesazh;


            clsKokaShitje shitje = new clsKokaShitje(idShitjeKoka);
            clsKokaRezervime rezervim = new clsKokaRezervime();
            rezervim.mbushKokaRezervimiSipasIDGjenerues(shitje.idShitjeKoka, 1, shitje.IdKonfigAmbjente, dbR);
            rezervim.KrijoAnullim();
            string _ = "";
            mesazh = rezervim.modifiko(rezervim.eshteILidhur(), true, out _, dbR);

            return mesazh;
        }

        public static clsMesazh updateStatusTeDokKonvertuarNga(int idDokKonvertNga, int idStatusDokNew)
        {
            using (clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim())
                return dbRegj.updateIdStatusDokKonvertuarNga(idDokKonvertNga, idStatusDokNew);
        }

        private static DataRow KtheMarveshjeSipasKlientit(int idKlientFurnitorVartes, int idNdermarrje, DateTime dtDok, int idShitjeTrupi)
        {
            using (clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim())
                return dbRegj.KtheMarveshjeSipasKlientit(idKlientFurnitorVartes, idNdermarrje, dtDok, idShitjeTrupi);
        }

        private void ValidoMarreveshje()
        {
            var nivel = new clsNivelRegjistrimi();
            nivel.mbushNivelRegjistrimiSipasIdMeKonvertime(IdNivel);
            if (ColKlienteFurnitoreVartes.Count > 0 && nivel.Kodi == "FSH")
            {
                var marreveshje = KtheMarveshjeSipasKlientit(ColKlienteFurnitoreVartes[0].IdKlientFurnitor, IdNdermarrje, DtDok, OColTrupiShitje[0].IdShitjeTrupi);
                if (marreveshje == null)
                    throw new MyException("Klienti nuk ka marreveshje aktive me buxhet! Ju lutem regjistroni fillimisht marreveshjen per te vazhduar me shitjen!");

                var gjendja = Math.Round(((double)marreveshje["VLERAMBETUR"] - totali + cash),2);
                if (gjendja < 0)
                    throw new MyException($"Kujdes! Gjendja e buxhetit shkon {gjendja}");

                IdMarreveshje = (string)marreveshje["IDMARREVESHJE"];
            }
        }

        public static List<Tuple<int, string>> merrSerialeNeFBKthim(int idShitjeKoka)
        {
            List<Tuple<int, string>> serialeNeFBKthim = new List<Tuple<int, string>>();

            clsKokaShitje currentDok = new clsKokaShitje(idShitjeKoka);
            clsKokaShitje fbKthim = new clsKokaShitje();
            if (currentDok.IdDokTransferimNga != 0)
            {
                fbKthim.mbushKokaShitjeSipasIDPaTrup(currentDok.IdDokTransferimNga);
                fbKthim.mbushTrupShitje();
                foreach (clsTrupiShitje trup in fbKthim.OColTrupiShitje)
                {
                    string detajim = new clsDetajimArtikulli(trup.IdDetajimArt).KodDetajimArtikulli;
                    serialeNeFBKthim.Add(new Tuple<int, string>(trup.IdKodi, detajim));
                }
            }
            return serialeNeFBKthim;
        }

        private void updateStatusDokumentashVartes(int idshitjekoka, int idstatusdok)
        {
            using (var db = new clsDatabaseRegjistrim())
                db.updateStatusDokumentashVarteS(idshitjekoka, idstatusdok);
        }

        public static Tuple<DataTable, List<int>, clsMesazh, int, int, List<object>> GjeneroFaturePermbledhese(int idGjuha, int idNdermarrja, string serverUrl, DataRow[] dr, bool meme, DateTime dtFillimi, DateTime dtMbarimi, int? idPeriudha, bool gjenerimAutomatik, bool printoFaturePermbledhese)
        {
            var colshitje = new colKokaShitje();
            var dokGjenerues = new ArrayList();
            var colkonvetimi = new List<colKonvertimi>();
            var colurdhra = new colKokaShitje();
            var myWatchPer = new System.Diagnostics.Stopwatch();
            myWatchPer.Start();

            var dbData = new DbData();
            colurdhra.mbushKokatShitjeRowArray(dr);
            colurdhra.MbushTrupat();

            myWatchPer.Stop();
            System.Diagnostics.Debug.WriteLine("myWatchPer Mbush trupa: " + myWatchPer.Elapsed);

            var err = new DataTable();
            err.Columns.Add("Kodi");
            err.Columns.Add("Gabimi");
            err.Columns.Add("Rreshti");

            var mesazh = new clsMesazh();


            var myWatchPer1 = new System.Diagnostics.Stopwatch();
            myWatchPer1.Start();

            colshitje = gjeneroFaturePermbledheseNew(colurdhra, colkonvetimi, meme, false, dokGjenerues, dtFillimi, dtMbarimi, dbData, idGjuha, ref err, gjenerimAutomatik);

            myWatchPer1.Stop();
            System.Diagnostics.Debug.WriteLine("myWatchPer permbledhese: " + myWatchPer1.Elapsed);


            var nr = 0;
            var dokumentat = err.Rows.Count + colshitje.Count;
            var serializusi = new JavaScriptSerializer { MaxJsonLength = 50000000 };

            var dbShare = new clsDatabaseShare(dbData);
            var dbAdmin = new clsDatabaseAdmin(dbData);
            var nrreshta = 0;
            var idSukses = new List<int>();
            List<object> rreshtatKokaPerPrintim = new List<object>();

            foreach (var kokeShitje in colshitje)
            {
                nrreshta++;
                var konfig = new clsKonfigurimAmbjenti(kokeShitje.IdKonfigAmbjente, dbShare);
                var kushtamor = new clsKusht(kokeShitje.IdKonfigAmbjente, "ZDAM", dbShare);
                var konfamortizimi = kushtamor.Vlera == 0 ? new clsKonfigurimAmbjenti() : new clsKonfigurimAmbjenti(kushtamor.Vlera, dbShare);
                var dergoemail = clsAlternativaKushti.getAlternativa(kokeShitje.IdKonfigAmbjente, "LE", dbShare) == "Po";
                var gjenerodokmagazine = clsAlternativaKushti.getAlternativa(kokeShitje.IdKonfigAmbjente, "GJDM", dbShare) == "Po" && kokeShitje.OKokaMagazina.NrDok != null;
                var dergoemailVFOne = clsAlternativaKushti.getAlternativa(kokeShitje.IdKonfigAmbjente, "DEVFOne", dbShare) == "Po";
                var kontrolloIMEIFifo = clsAlternativaKushti.getAlternativa(kokeShitje.IdKonfigAmbjente, "AFI", dbShare) == "Po";

                var hidden = new Dictionary<string, object>();
                var list = new List<NrAuto>();
                var idnrautonrdok = clsAtributeTrupi.merrNrAutomatikSipasKontrollitDheKonfigurimit(kokeShitje.IdKonfigAmbjente, "txtNumer", 506, dbShare);
                var nrdokshitje = clsNrAutom.merrVlerenNrAutomatik(idnrautonrdok, kokeShitje.DtDok, dbAdmin);

                if (!string.IsNullOrEmpty(nrdokshitje)) //nqs ka nr automatik
                {
                    var nrdokshi = new NrAuto
                    {
                        kodKontrolli = "NrDok",
                        idNrAuto = idnrautonrdok,
                        vlereNrAuto = nrdokshitje
                    };
                    list.Add(nrdokshi);
                    //nrdokshitje = nrdokshi.vlereNrAuto; kjo eshte e tepert, i japim dhe nje here vleren qe morem me lart nga vetja????!!!!
                    hidden.Add("NrDok", serializusi.Serialize(nrdokshi));
                }
                else
                {
                    var nrd = merrNrMaxDokumenti(idNdermarrja, kokeShitje.DtDok.Day + "/" + kokeShitje.DtDok.Month + "/" + kokeShitje.DtDok.Year + "_%") + 1;
                    nrdokshitje = kokeShitje.DtDok.Day + "/" + kokeShitje.DtDok.Month + "/" + kokeShitje.DtDok.Year + "_" + nrd;//nqs nuk ka nr automatik merr daten e dokumentit +nr incrementues
                    kokeShitje.NrDok = nrdokshitje;
                }

                var idnrautonrdokMag = clsAtributeTrupi.merrNrAutomatikSipasKontrollitDheKonfigurimit(kokeShitje.IdKonfigAmbjente, "txtNrDokMagazine", 506, dbShare);
                var nrdokshitjeMag = clsNrAutom.merrVlerenNrAutomatik(idnrautonrdokMag, kokeShitje.DtDok, dbAdmin);
                if (!string.IsNullOrEmpty(nrdokshitjeMag)) //nqs ka nr automatik do merret nr automatik i konfiguruar, ne te kundert do plotesohet me vleren e nr te dok.
                {
                    var nrdokshiMag = new NrAuto
                    {
                        kodKontrolli = "NrDokMagazine",
                        idNrAuto = idnrautonrdokMag,
                        vlereNrAuto = nrdokshitjeMag
                    };
                    list.Add(nrdokshiMag);
                    hidden.Add("NrDokMagazine", serializusi.Serialize(nrdokshiMag));
                }
                else
                {
                    kokeShitje.NrDokMagazine = nrdokshitje;
                }

                var idnrautonrserial = clsAtributeTrupi.merrNrAutomatikSipasKontrollitDheKonfigurimit(kokeShitje.IdKonfigAmbjente, "txtNumerSerial", 506, dbShare);
                var nrserialshitje = clsNrAutom.merrVlerenNrAutomatik(idnrautonrserial, kokeShitje.DtDok, dbAdmin);

                if (!string.IsNullOrEmpty(nrserialshitje))
                {
                    var nrser = new NrAuto
                    {
                        kodKontrolli = "NrSerial",
                        idNrAuto = idnrautonrserial,
                        vlereNrAuto = nrserialshitje
                    };

                    list.Add(nrser);
                    nrserialshitje = nrser.vlereNrAuto;
                    hidden.Add("NrSerial", serializusi.Serialize(nrser));
                }
                else nrserialshitje = "";

                var mekontabilizim = clsAlternativaKushti.getAlternativa(kokeShitje.IdKonfigAmbjente, "GJK", dbShare) != "Jo";
                var kokaTePermbledhura = dokGjenerues[nr] as colKokaShitje;
                clsVeprimBankaKoka veprimebanka = new clsVeprimBankaKoka();
                var iic = "";
                var nivf = "";
                try
                {
                    string shfaqmesazhapolupemagazina, shfaqmesazhapolupebanka, shfaqmesazhapolupeVdk, shfaqmesazhapolupe, mesazhmevonshem;
                    bool printofature, printogarancifature, pageseFature;
                    mesazh = kokeShitje.ruaj(idGjuha, serverUrl, true, hidden, idPeriudha ?? clsPeriudhaKontabel.ktheIdPeriudheSipasDatesDheNdermarrjes(kokeShitje.DtDok, idNdermarrja), colkonvetimi[nr], gjenerodokmagazine, out veprimebanka, 0, StatusAprovimi.Undefined, 0, out shfaqmesazhapolupemagazina, out shfaqmesazhapolupebanka, out shfaqmesazhapolupeVdk, new clsKokaShitje(), 0, 0, dergoemail, false, dergoemailVFOne, "", new DbCore.DbAsete.colSerialetMagazine(), konfamortizimi, new clsKokaShitje(), out printofature, out printogarancifature, out pageseFature, mekontabilizim, out shfaqmesazhapolupe, konfig.KodKonfigAmbjente, false, "", 0, false, false, false, false, "", "", "", false, false, false, "", false, false, false, false, false, kokaTePermbledhura, kontrolloIMEIFifo, false, false, ref dbData, false, "", "", false, out mesazhmevonshem, false, true,iic,nivf);
                }
                catch (Exception ex)
                {
                    ImbLogger.Error(ex.Message);
                    mesazh = new clsMesazh(false, ex.Message);
                }

                nr++;

                if (!mesazh.Status)
                {
                    object[] arr =
                    {
                        konfig.KodKonfigAmbjente + " " + kokeShitje.NrDok + " " + kokeShitje.DtDok.ToShortDateString(),
                        mesazh.PershkrimMesazhi, nrreshta
                    };
                    err.Rows.Add(arr);
                    continue;
                }
                if (printoFaturePermbledhese)
                {
                    object idDesign = kokeShitje.IdRaportDesing > 0 ? (object)kokeShitje.IdRaportDesing : "";
                    rreshtatKokaPerPrintim.Add(new object[3] { kokeShitje.IdShitjeKoka, kokeShitje.NrDok, idDesign });
                    if (kokeShitje.IdRaportDesing > 0 && veprimebanka.IdKoka > 0 && veprimebanka.Printo && veprimebanka.IdRaportDesing > 0)
                        rreshtatKokaPerPrintim.Add(new object[3] { veprimebanka.IdKoka, veprimebanka.NrDokumenti, veprimebanka.IdRaportDesing });
                }
                idSukses.AddRange(kokaTePermbledhura.Select(x => x.IdShitjeKoka).ToList());
            }

            System.Diagnostics.Debug.WriteLine($"Shpetova nga: {dbData.TransCache.getFromCacheTotal()} lexime db-je");
            dbData.Dispose();
            dbData = new DbData();

            return new Tuple<DataTable, List<int>, clsMesazh, int, int, List<object>>(err, idSukses, mesazh, dokumentat, colshitje.Count, rreshtatKokaPerPrintim);
        }

        public static clsMesazh GjeneroFaturePermbledheseAutomatike(string perdoruesi, string kodNdermarrja, int dtMuaji)
        {
            if (DateTime.Now.Day < dtMuaji)
                return new clsMesazh(false, "Data aktuale eshte para dates se skeduluar per gjenerim automatik!");

            var idNdermarrja = clsNdermarrje.ktheIdNdermarrje(kodNdermarrja);
            var idPerdoruesi = clsPerdorues.ktheIdPerdoruesSipasUsername(perdoruesi, idNdermarrja);
            var dtFillimi = DateTime.ParseExact("01/01/1900", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var dtMbarimi = DateTime.Now;
            var ndervit = new clsNdermarrjeViti();
            var idndermvit = ndervit.merrNdermarrjeVitSipasNdermarjesDheDatesAktuale(idNdermarrja);
            var dt = colKokaShitje.merrKokaShitjePaFatureTatimoreDT(idPerdoruesi, idNdermarrja, idndermvit, "01/01/1900", dtMbarimi.ToString("dd/MM/yyyy"));
            if (dt.Rows.Count == 0)
                return new clsMesazh(false, "Nuk ka dokumenta per te gjeneruar!");
            var gjenerimi = GjeneroFaturePermbledhese(0, idNdermarrja, "", dt.Select(), false, dtFillimi, dtMbarimi, null, true, false);
            return gjenerimi.Item3;
        }
        public static string MerrPikeNeModifikimTeVFONE(int idshitjekoka, int idNdermarrje)
        {
            using (var db = new clsDatabaseRegjistrim())
                return db.MerrPikeNeModifikimTeVFONE(idshitjekoka, idNdermarrje);
        }
        private static DataRow FillDataRowKlientFurnitorVartes(DataRow dtRow, int idShitjeKoka, int idKlientFurnitor)
        {
            dtRow["IDSHITJEKOKAKLIENTVARTES"] = 0;
            dtRow["IDSHITJEKOKA"] = idShitjeKoka;
            dtRow["IDKLIENTFURNITOR"] = idKlientFurnitor;
            return dtRow;
        }

        private DataTable KrijoDataTableKlientFurnitorVartes(int idShitjeKoka, colKlienteFurnitore colKlienteFurnitoreVartes)
        {
            var dt = new DataTable();
            dt.Columns.AddRange(new[]
            {
                new DataColumn("IDSHITJEKOKAKLIENTVARTES", typeof(decimal)),
                new DataColumn("IDSHITJEKOKA", typeof(decimal)),
                new DataColumn("IDKLIENTFURNITOR", typeof(decimal))
            });

            foreach (var klientFurnitorVartes in colKlienteFurnitoreVartes)
            {
                dt.Rows.Add(FillDataRowKlientFurnitorVartes(dt.NewRow(), idShitjeKoka, klientFurnitorVartes.IdKlientFurnitor));
            }

            return dt;
        }

        public bool KtheDokMarreveshjeNgaIdMarreveshje(string idMarreveshje)
        {
            using (var db = new clsDatabaseRegjistrim())
            {
                return mbushKokeShitjeSipasDT(db.KtheDokMarreveshjeNgaIdMarreveshje(idMarreveshje));
            }
        }
        public clsKokaShitje ktheFatureMeKuponNgaUSH()
        {
            using (var db = new clsDatabaseRegjistrim())
            {
                int id = db.ktheIdFatureMeKuponNgaUSH(idShitjeKoka);

                if (id > 0)
                    return new clsKokaShitje();
                else
                    return new clsKokaShitje(id);
            }
        }
        public static bool shtoKodinNivfTeShitja(int nrDok, string nivf, int idNdermarje, string eic, string iic)
        {
            using (clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim())
            {
                return dbRegj.shtoKodinNivfTeShitja(nrDok, nivf, idNdermarje, eic,iic);
            }
        }
        public void shtoFaturatUbl(string xml, bool veprimi)
        {
            using (clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim())
                dbRegj.ShtoFaturatUbl(xml, veprimi);
        }
        public object[] krijoObjektPerWebhook(clsKokaShitje koka ,string eventi,string objekti)
        {
            clsPerdorues perdorues =  new clsPerdorues(koka.IdPerdoruesi);
            clsNdermarrje ndermarrje =  new clsNdermarrje(koka.idNdermarje);
            clsKlientFurnitor klient =  new clsKlientFurnitor(koka.IdKlientFurnitor);
            object[] kokaWebhook = new object[2];
            kokaWebhook[0] = new
            {
                eventi = eventi,
                objekti = objekti,
                idShitjeKoka = koka.idShitjeKoka,
                perdoruesi = perdorues.PerdoruesUsername,
                idPerdoruesi = perdorues.IdPerdorues
            };
            kokaWebhook[1] = new
            {   
                nrDok = koka.nrDok,
                kodKlienti = clsKlientFurnitor.merrKodKlientFurnitorSipasId(koka.idKlientFurnitor),
                kodMonedha = clsMonedha.ktheKodMonedheSipasId(koka.idMonedha),
                kodNdermarje = ndermarrje.NdermarrjeKodi,
                kursi = koka.kursi,
                emerKlienti = klient.EmertimiKF,
                Llojdokumenti = new clsKonfigurimAmbjenti(koka.IdKonfigAmbjente).KodKonfigAmbjente,
                Nenkategoria = new clsNivelRegjistrimi(clsNivelRegjistrimi.ktheKodNivelRegjistrimi(koka.idNivel),koka.idNdermarje).Pershkrimi,
                kase = koka.kase,
                zbritje = koka.zbritje,
                totali = koka.totali,
                tvsh = koka.tvsh,
                cash = koka.cash,
                pershkrimi = koka.pershkrimi,
                dogana = koka.dogana,
                dtKrijimi = koka.DtKrijimi.ToString(),
                dtTransportimi = koka.dtTransportimi.ToString(),
                adresaFaturimit = koka.adresaFaturimit,
                niptKlienti = klient.NiptiKF,
                idNdermarrje = koka.idNdermarje,
                dtRegjistrimi = koka.DtRegjistrimi.ToString(),
                idStatusDok = koka.idStatusDok,
                adresaDergimit = koka.adresaDergimit,
                gjeneruar = koka.gjeneruar,
                idDegeAdministrative = koka.idDegeAdministrative,
                idPerdoruesi = koka.IdPerdoruesi,
                idKrijuesi = koka.idKrijuesi,
                Krijuesi = new clsPerdorues(koka.idKrijuesi).PerdoruesUsername,
                idRaportDesing = koka.IdRaportDesing,
                perqindjeAgjenti = koka.perqindjeAgjenti,
                kupon = koka.kupon,
                faturePermbledhese = koka.faturePermbledhese,
                kontakti =koka.kontakti,
                marresi = koka.marresi,
                koordinata = koka.koordinata,
                shenime2 = koka.shenime2,
                kerkuarNga = koka.kerkuarNga,
                idMarreveshje = koka.idMarreveshje,
                ColKlienteFurnitoreVartes = colKlienteFurnitore.MerrKlientFurnitoreVartesSipasIdShitjeKoka(koka.idShitjeKoka),
                nrDokMagazine = koka.NrDokMagazine
            };
            return kokaWebhook;
        }
     
        #region kasa
        public (clsMesazh, string, clsMesazh) PrintoNeKase(clsKokaShitje faturashitjengaurdhershitjamekupontatimor, clsPerdorues perdoruesi, int idNdermarrje, clsKlientFurnitor kf, string merrIpKasaNgaWebServisi)
        {
            string stream = "";
            var idkonfigurimKase = perdoruesi.IdKonfigKasa != 0 ? perdoruesi.IdKonfigKasa.ToString() : clsAtributeTrupi.merrVleredefaultSipasKontrollitDheKonfigurimit(IdKonfigAmbjente, "cmbKonfigurimKase", 506);
            if (idkonfigurimKase == "")
                return (new clsMesazh(false, "Nuk keni konfiguruar kase per kete lloj dokumenti."), null, new clsMesazh(false));

            return PrintoNeKase(faturashitjengaurdhershitjamekupontatimor, perdoruesi.IdPerdoruesi, idNdermarrje, kf, ref stream, merrIpKasaNgaWebServisi, Convert.ToInt32(idkonfigurimKase), "0");
        }
        public (clsMesazh, string, clsMesazh) PrintoNeKase(clsKokaShitje faturashitjengaurdhershitjamekupontatimor, int idPerdoruesi, int idNdermarrje, clsKlientFurnitor kf, ref string stream, string merrIpKasaNgaWebServisi, int idKonfigurimiKases, string paguartxt)
        {
            System.Diagnostics.Stopwatch myWatch = System.Diagnostics.Stopwatch.StartNew();
            ImbLogger.Info($"PrintoNeKase: IdKokashitje: ${IdShitjeKoka} Numer Dokumenti: {NrDok}, Date dokumenti: {DtDok}, merrIpKasaNgaWebServisi:{merrIpKasaNgaWebServisi}, Lloj Dokumenti: {new clsKonfigurimAmbjenti(IdKonfigAmbjente).KodKonfigAmbjente}");
            var mesazhkasa = new clsMesazh();
            string alternativa = clsAlternativaKushti.getAlternativa(IdKonfigAmbjente, "DK");
            if (alternativa == "File")
            {

                if (!string.IsNullOrEmpty(faturashitjengaurdhershitjamekupontatimor?.NrDok))
                    mesazhkasa = gjeneroFatureFiskale(out stream, idPerdoruesi, idNdermarrje, faturashitjengaurdhershitjamekupontatimor, kf, idKonfigurimiKases, Kase, PerqindjeZbritje.ToString(), paguartxt);
                else
                    mesazhkasa = gjeneroFatureFiskale(out stream, idPerdoruesi, idNdermarrje, this, kf, idKonfigurimiKases, Kase, PerqindjeZbritje.ToString(), paguartxt);
                myWatch.Stop();
                ImbLogger.Info($"Mbaroi Procesi i printimit ne kase per dokumentin me id {IdShitjeKoka}. {mesazhkasa.PershkrimMesazhi} Kohezgjatja: {myWatch.ElapsedMilliseconds / 1000.0} sekonda.");
                return (mesazhkasa, null, null);
            }
            else
            {
                clsNivelCmimi nivel = new clsNivelCmimi(kf.IdNivelCmimi);
                (clsMesazh, string, clsMesazh) mesazhKasa;
                bool meTvsh = nivel.BrutoNetoNivelCmimi == 1 ? true : false;

                if (!string.IsNullOrEmpty(faturashitjengaurdhershitjamekupontatimor?.NrDok))
                    mesazhKasa = KrijuesKasash.printoNeKase(idKonfigurimiKases, Kase, PerqindjeZbritje.ToString(), faturashitjengaurdhershitjamekupontatimor, idPerdoruesi, idNdermarrje, merrIpKasaNgaWebServisi, meTvsh, false);
                else
                    mesazhKasa = KrijuesKasash.printoNeKase(idKonfigurimiKases, Kase, PerqindjeZbritje.ToString(), this, idPerdoruesi, idNdermarrje, merrIpKasaNgaWebServisi, meTvsh, false);
                myWatch.Stop();
                ImbLogger.Info($"Mbaroi Procesi i printimit ne kase per dokumentin me id {IdShitjeKoka}. {mesazhKasa.Item1.PershkrimMesazhi} Kohezgjatja: {myWatch.ElapsedMilliseconds / 1000.0} sekonda.");
                return mesazhKasa;
            }

        }



        //private clsMesazh gjeneroFatureFiskale(int idPerdoruesi, int idNdermarrje, clsKokaShitje koka)
        //{
        //    clsKlientFurnitor kf = new clsKlientFurnitor(Convert.ToInt32(btnKlienti.Value));
        //    string stream;
        //    return gjeneroFatureFiskale(out stream, idPerdoruesi, idNdermarrje, koka, kf, cmbKonfigurimKase.Value == null ? 0 : Convert.ToInt32(cmbKonfigurimKase.Value));
        //}

        private clsMesazh gjeneroFatureFiskale(out string stream, int idPerdoruesi, int idNdermarrje, clsKokaShitje koka, clsKlientFurnitor kf, int idKonfigurimiKases, bool kasacheck, string perqindjetxt, string paguartxt)
        {
            clsKonfigurimKase kasa = new clsKonfigurimKase(idKonfigurimiKases);
            if (kasacheck && kasa.IdKonfigurimi == 0)
            {
                stream = string.Empty;
                return new clsMesazh(false, "Fatura nuk u printua ne kase. Ju lutemi, zgjidhni kasen!");
            }
            //kasa.merrKonfiguriminSipasNdermarjes(idNdermarrje);
            if (kasa.IdKonfigurimi == 0)
            {
                stream = string.Empty;
                return new clsMesazh(false, "Nuk keni konfiguruar kase per kete ndermarje");
            }

            //kf.mbushKlientFurnitorSipasKodit(btnKlienti.Text, idNdermarrje);           
            clsNivelCmimi nivel = new clsNivelCmimi(kf.IdNivelCmimi);
            bool meTvsh = nivel.BrutoNetoNivelCmimi == 1 ? true : false;
            clsVleraKonfigurimiKasa vlera = new clsVleraKonfigurimiKasa();
            string llojkase = kasa.OColVlerat.ktheVlereOpsioni("KASEFISKALELLOJ");// string filename = kasa.OColVlerat.ktheVlereOpsioni("KASEFISKALEPATH");
            clsMesazh mesazh;
            double perqindje;
            if (!double.TryParse(perqindjetxt, out perqindje))
                perqindje = 0;
            string totalimonedhabaze = (TotaliMeZbritjeMeTVSH * Kursi).ToString();
            clsPerdorues perdoruesi = new clsPerdorues(idPerdoruesi);
            switch (llojkase)
            {
                case "0":
                    //mesazh = koka.gjeneroFatureFiskaleIva(kasa, txtPerqindje.Text, txtTotal2.Text, idperdoruesi, txtTotal1.Text);
                    mesazh = koka.gjeneroFatureFiskaleIva(out stream, kasa, perqindje, totalimonedhabaze, idPerdoruesi, TotaliMeZbritjeMeTVSH.ToString(), meTvsh);
                    break;
                case "1":
                    //mesazh = koka.gjeneroFatureFiskaleAed(kasa, perqindje, idperdoruesi, txtTotal1.Text);
                    mesazh = koka.gjeneroFatureFiskaleAed(out stream, kasa, perqindje, idPerdoruesi, TotaliMeZbritjeMeTVSH.ToString(), meTvsh);
                    break;
                case "2":
                    //mesazh = koka.gjeneroFatureFiskaleBtn(kasa, perqindje, txtTotal2.Text, idperdoruesi, txtTotal1.Text);
                    mesazh = koka.gjeneroFatureFiskaleBtn(out stream, kasa, perqindje, totalimonedhabaze, idPerdoruesi, TotaliMeZbritjeMeTVSH.ToString(), meTvsh, idKonfigurimiKases);
                    break;
                case "3":
                    bool meSkedar = Boolean.Parse(kasa.OColVlerat.ktheVlereOpsioni("MESKEDAR"));
                    if (meSkedar)
                        mesazh = koka.gjeneroFatureFiskaleCKVNOKISkedar(out stream, kasa, perqindje, totalimonedhabaze, idPerdoruesi, TotaliMeZbritjeMeTVSH.ToString(), meTvsh);
                    else
                        mesazh = koka.gjeneroFatureFiskaleCkvNoki(out stream, kasa, perqindje, TotaliMeZbritjeMeTVSH.ToString(), meTvsh);
                    break;
                case "4":
                    //mesazh = koka.gjeneroFatureFiskalePkp(kasa, perqindje, txtTotal2.Text, idperdoruesi, txtTotal1.Text);
                    mesazh = koka.gjeneroFatureFiskalePkp(out stream, kasa, perqindje, totalimonedhabaze, idPerdoruesi, TotaliMeZbritjeMeTVSH.ToString(), meTvsh);
                    break;
                case "5":
                    //mesazh = koka.gjeneroFatureFiskaleGekos(kasa, DbCore.clsFunksione.kthePerdorues(Session).PerdoruesUsername, perqindje, txtVlefte.Text, idperdoruesi, txtTotal1.Text);
                    mesazh = koka.gjeneroFatureFiskaleGekos(out stream, kasa, perdoruesi.PerdoruesUsername, perqindje, Zbritje.ToString(), idPerdoruesi, TotaliMeZbritjeMeTVSH.ToString(), meTvsh);
                    break;
                case "6":
                    mesazh = koka.gjeneroFatureFiskaleBntAclas(out stream, kasa, perqindje, paguartxt, TotaliMeZbritjeMeTVSH.ToString(), meTvsh);
                    break;
                case "7":
                    mesazh = koka.gjeneroFatureFiskaleBntAclasSkedar(out stream, kasa, perqindje, totalimonedhabaze, idPerdoruesi, TotaliMeZbritjeMeTVSH.ToString(), meTvsh);
                    break;

                default:
                    stream = string.Empty;
                    return new clsMesazh(false, "Kase e pakonfiguruar");
            }
            return mesazh;
        }

        public static bool TransferuarNgaFBKthim(int idkokashitje)
        {
            using (var db = new clsDatabaseRegjistrim())
                return db.TransferuarNgaFBKthim(idkokashitje);
        }

        #endregion
        #endregion

        #region Metoda Internal

        internal bool mbushKokeShitjeSipasDT(DataRow dbDataRowKokeShitje)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda mbushKokeShitjeSipasDT");
            if (dbDataRowKokeShitje != null)
            {
                try
                {
                    mbushKokeShitjePaTrupSipasDT(dbDataRowKokeShitje);
                    oGjendjeKF = new colGjendjeKlientFurnitor();
                    ImbLogger.LogTraceShitje("Mbaroi metoda mbushKokeShitjeSipasDT");
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    ImbLogger.LogErrorShitje($"Exception:{ex}");
                    ImbLogger.Error(ex);
                    throw new Exception("ERROR: Gabim gjate marrjes se kokes se shitjes nga db-ja");
                }
            }
            else
            {
                ImbLogger.LogTraceShitje("Mbaroi metoda mbushKokeShitjeSipasDT");
                return false;
            }
        }

        internal bool mbushKokeShitjePaTrup(DataRow dbDataRowKokeShitje)
        {
            ImbLogger.LogTraceShitje("Filloi metoda mbushKokeShitjePaTrup");
            if (dbDataRowKokeShitje != null)
            {
                try
                {
                    idShitjeKoka = int.Parse(dbDataRowKokeShitje["IDSHITJEKOKA"].ToString());
                    idNivel = int.Parse(dbDataRowKokeShitje["IDNIVEL"].ToString());
                    idTemplate = int.Parse(dbDataRowKokeShitje["IDTEMPLATE"].ToString());
                    idKonfigAmbjente = int.Parse(dbDataRowKokeShitje["IDKONFIGAMBJENTE"].ToString());
                    int.TryParse(dbDataRowKokeShitje["IDKLIENTFURNITOR"].ToString(), out idKlientFurnitor);
                    idProjekt = int.Parse(dbDataRowKokeShitje["IDPROJEKT"].ToString());
                    nrProjekt = dbDataRowKokeShitje["NRPROJEKT"].ToString();
                    nrDok = dbDataRowKokeShitje["NRDOK"].ToString();
                    nrSerial = dbDataRowKokeShitje["NRSERIAL"].ToString();
                    dtDok = DateTime.Parse(dbDataRowKokeShitje["DTDOK"].ToString());
                    DateTime.TryParse(dbDataRowKokeShitje["DTMATURIMI"].ToString(), out dtMaturimi);
                    idMonedha = int.Parse(dbDataRowKokeShitje["IDMONEDHA"].ToString());
                    kursi = double.Parse(dbDataRowKokeShitje["KURSI"].ToString());
                    int.TryParse(dbDataRowKokeShitje["IDMENYRETRANSPORT"].ToString(), out idMenyreTransporti);
                    dtTransportimi = DateTime.Parse(dbDataRowKokeShitje["DTTRANSPORTIMI"].ToString());
                    int.TryParse(dbDataRowKokeShitje["IDKUSHTDERGIM"].ToString(), out idKushtDergimi);
                    int.TryParse(dbDataRowKokeShitje["IDAGJENT"].ToString(), out idAgjent);
                    int.TryParse(dbDataRowKokeShitje["IDMENYREPAGESE"].ToString(), out idMenyrePagese);
                    int.TryParse(dbDataRowKokeShitje["IDKUSHTPAGESE"].ToString(), out idKushtPagese);
                    zbritje = double.Parse(dbDataRowKokeShitje["ZBRITJE"].ToString());
                    totali = double.Parse(dbDataRowKokeShitje["TOTALI"].ToString());
                    tvsh = double.Parse(dbDataRowKokeShitje["TVSH"].ToString());
                    cash = double.Parse(dbDataRowKokeShitje["CASH"].ToString());
                    int stap = 0;
                    int.TryParse(dbDataRowKokeShitje["STATUSAPROVIMI"].ToString(), out stap);
                    statusAprovimi = (StatusAprovimi)stap;
                    dtRegjistrimi = DateTime.Parse(dbDataRowKokeShitje["DTREGJISTRIMI"].ToString());
                    idStatusDok = int.Parse(dbDataRowKokeShitje["IDSTATUSDOK"].ToString());
                    idNdermarje = int.Parse(dbDataRowKokeShitje["IDNDERM"].ToString());
                    idNdermarjeVit = int.Parse(dbDataRowKokeShitje["IDNDERMVIT"].ToString());
                    int.TryParse(dbDataRowKokeShitje["IDNIVELGJENERUES"].ToString(), out idNivelGjenerues);
                    int.TryParse(dbDataRowKokeShitje["IDKONFIGGJENERUES"].ToString(), out idKonfigGjenerues);
                    int.TryParse(dbDataRowKokeShitje["IDGJENERUES"].ToString(), out idGjenerues);
                    int.TryParse(dbDataRowKokeShitje["IDDOKNGA"].ToString(), out idDokNga);
                    adresaFaturimit = dbDataRowKokeShitje["ADRESAFATURIMIT"].ToString();
                    adresaDergimit = dbDataRowKokeShitje["ADRESADERGIMIT"].ToString();
                    pershkrimi = dbDataRowKokeShitje["PERSHKRIMI"].ToString();
                    totaliMeZbritjeMeTvsh = double.Parse(dbDataRowKokeShitje["TOTALIMETVSHMEZBRITJE"].ToString());
                    dogana = bool.Parse(dbDataRowKokeShitje["DOGANA"].ToString());
                    gjeneruar = bool.Parse(dbDataRowKokeShitje["GJENERUAR"].ToString());
                    int.TryParse(dbDataRowKokeShitje["IDDEGEADMINISTRATIVE"].ToString(), out idDegeAdministrative);
                    int.TryParse(dbDataRowKokeShitje["IDPIKESHITJEFURNIZIMI"].ToString(), out idPikeShitjeFurnizimi);
                    int.TryParse(dbDataRowKokeShitje["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    int.TryParse(dbDataRowKokeShitje["IDKRIJUESI"].ToString(), out idKrijuesi);
                    krijuesi = dbDataRowKokeShitje["PERDORUESUSERNAME"].ToString();
                    int.TryParse(dbDataRowKokeShitje["IDRAPORTDESING"].ToString(), out idRaportDesing);
                    DateTime.TryParse(dbDataRowKokeShitje["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRowKokeShitje["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    DateTime.TryParse(dbDataRowKokeShitje["AFATIKOHOR"].ToString(), out afatKohor);
                    vleraMbetur = double.Parse(dbDataRowKokeShitje["VleraMbetur"].ToString());
                    int.TryParse(dbDataRowKokeShitje["IDGRUP1"].ToString(), out idGrup1);
                    int.TryParse(dbDataRowKokeShitje["IDGRUP2"].ToString(), out idGrup2);
                    int.TryParse(dbDataRowKokeShitje["IDGRUP3"].ToString(), out idGrup3);
                    colorVleraMbetur = dbDataRowKokeShitje["ColorVleraMbetur"].ToString();
                    double.TryParse(dbDataRowKokeShitje["PERQINDJEAGJENTI"].ToString(), out perqindjeAgjenti);
                    int.TryParse(dbDataRowKokeShitje["IDTRANSFERIMI"].ToString(), out idTransferimi);
                    int.TryParse(dbDataRowKokeShitje["IDKONFIGTRANSFERIMI"].ToString(), out idKonfigTransferimi);
                    bool.TryParse(dbDataRowKokeShitje["KASE"].ToString(), out kase);
                    bool.TryParse(dbDataRowKokeShitje["KUPON"].ToString(), out kupon);
                    bool.TryParse(dbDataRowKokeShitje["FATUREPERMBLEDHESE"].ToString(), out faturePermbledhese);
                    int st = 0;
                    int.TryParse(dbDataRowKokeShitje["STATUSTRANSFERIMI"].ToString(), out st);
                    emerKlienti = dbDataRowKokeShitje["EMERKLIENTI"].ToString();
                    kontakti = dbDataRowKokeShitje["KONTAKTI"].ToString();
                    statusTransferimi = (StatusTrasferimi)st;
                    colGaranci = new colGaranciArtikulli();
                    DateTime.TryParse(dbDataRowKokeShitje["DTFILLIMI"].ToString(), out dtFillimi);
                    DateTime.TryParse(dbDataRowKokeShitje["DTMBARIMI"].ToString(), out dtMbarimi);
                    int.TryParse(dbDataRowKokeShitje["IDAUTOMJETI"].ToString(), out idAutomjet);
                    double.TryParse(dbDataRowKokeShitje["KILOMETRAAUTO"].ToString(), out kilometraAuto);
                    int.TryParse(dbDataRowKokeShitje["IDAGJENTI2"].ToString(), out idAgjenti2);
                    double.TryParse(dbDataRowKokeShitje["PERQINDJEAGJENTI2"].ToString(), out perqindjeAgjenti2);
                    int.TryParse(dbDataRowKokeShitje["IDAGJENTI3"].ToString(), out idAgjenti3);
                    double.TryParse(dbDataRowKokeShitje["PERQINDJEAGJENTI3"].ToString(), out perqindjeAgjenti3);
                    marresi = dbDataRowKokeShitje["MARRESI"].ToString();
                    int.TryParse(dbDataRowKokeShitje["IDTRANSPORTUES"].ToString(), out idTransportues);
                    bool.TryParse(dbDataRowKokeShitje["SHPENZJOTEZBRITSHME"].ToString(), out shpenzimeJoTeZbritshme);
                    int.TryParse(dbDataRowKokeShitje["IDARKA"].ToString(), out idarka);
                    DateTime.TryParse(dbDataRowKokeShitje["DTFATURE"].ToString(), out dtFature);
                    int.TryParse((dbDataRowKokeShitje["IDKARTA"]).ToString(), out idKarta);
                    int.TryParse((dbDataRowKokeShitje["PIKE"]).ToString(), out pike);
                    int.TryParse(dbDataRowKokeShitje["MUAJRAPORTIMI"].ToString(), out muajRaportimi);
                    int.TryParse(dbDataRowKokeShitje["IDVITRAPORTIMI"].ToString(), out idVitRaportimi);
                    int.TryParse(dbDataRowKokeShitje["IDFAZA"].ToString(), out idFaza);
                    targaSHF = dbDataRowKokeShitje["TARGA"].ToString();
                    shoferi = dbDataRowKokeShitje["SHOFERI"].ToString();
                    bool.TryParse(dbDataRowKokeShitje["ZBRITJENEVLERE"].ToString(), out zbritjeNeVlere);
                    double.TryParse(dbDataRowKokeShitje["PERQINDJEZBRITJE"].ToString(), out perqindjeZbritje);
                    DateTime.TryParse(dbDataRowKokeShitje["DTKRIJIMIPAJISJE"].ToString(), out dtKrijimiPajisje);
                    koordinata = dbDataRowKokeShitje["KOORDINATA"].ToString();
                    niptKlienti = dbDataRowKokeShitje["NIPTK"].ToString();
                    qytetiK = dbDataRowKokeShitje["QytetiK"].ToString();
                    int.TryParse(dbDataRowKokeShitje["IdKategoriSeriali"].ToString(), out idKategoriSeriali);
                    shenime2 = dbDataRowKokeShitje["Shenime2"].ToString();
                    bool.TryParse(dbDataRowKokeShitje["KartaPaPagese"].ToString(), out kartaPaPagese);
                    int.TryParse(dbDataRowKokeShitje["IDDOKTRANSFERIMNGA"].ToString(), out idDokTransferimNga);
                    int.TryParse(dbDataRowKokeShitje["IDLLOJMARREVESHJE"].ToString(), out idLlojMarreveshje);
                    idMarreveshje = dbDataRowKokeShitje["IDMARREVESHJE"].ToString();
                    StatusMarreveshje = !Convert.IsDBNull(dbDataRowKokeShitje["StatusMarreveshje"])
                        ? (StatusMarreveshje)Convert.ToInt32(dbDataRowKokeShitje["StatusMarreveshje"])
                        : 0;
                    kerkuarNga = dbDataRowKokeShitje["KERKUARNGA"].ToString();
                    DateTime.TryParse(dbDataRowKokeShitje["DATEKERKESE"].ToString(), out dateKerkese);
                    ColKlienteFurnitoreVartes = colKlienteFurnitore.MerrKlientFurnitoreVartesSipasIdShitjeKoka(IdShitjeKoka);
                    nrDokMagazine = dbDataRowKokeShitje["NRDOKMAGAZINE"].ToString();
                    iic = dbDataRowKokeShitje["IIC"].ToString();
                    nivf = dbDataRowKokeShitje["NIVF"].ToString();
                    int.TryParse(dbDataRowKokeShitje["IDOPERATOR"].ToString(), out idOperator);
                    if (dbDataRowKokeShitje.Table.Columns.Contains("NIVFKTHIM"))
                    {
                        nivfKthim = dbDataRowKokeShitje["NIVFKTHIM"].ToString();
                        eic = dbDataRowKokeShitje["EIC"].ToString();
                        einStatus = dbDataRowKokeShitje["EinStatus"].ToString();
                        int.TryParse(dbDataRowKokeShitje["Procesi"].ToString(), out procesi);
                        int.TryParse(dbDataRowKokeShitje["eInvoiceType"].ToString(), out eInvoiceType);
                        if(dbDataRowKokeShitje.Table.Columns.Contains("TipiIVetefaturimit"))
                          tipiIVetefaturimit = dbDataRowKokeShitje["TipiIVetefaturimit"].ToString();
                    }

                    ImbLogger.LogTraceShitje("Mbaroi metoda mbushKokeShitjePaTrup");
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    ImbLogger.Error("Gabim gjate marrjes se kokes se shitjes nga db-ja");
                    throw new Exception("ERROR: Gabim gjate marrjes se kokes se shitjes nga db-ja");
                }
            }
            else
                ImbLogger.LogTraceShitje("Mbaroi metoda mbushKokeShitjePaTrup");
            return false;
        }

        internal bool mbushKokeShitjePaTrupSipasDT(DataRow dbDataRowKokeShitje)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda mbushKokeShitjePaTrupSipasDT");
            if (dbDataRowKokeShitje != null)
            {
                try
                {
                    idShitjeKoka = int.Parse(dbDataRowKokeShitje["IdShitjeKoka"].ToString());
                    idNivel = int.Parse(dbDataRowKokeShitje["IDNIVEL"].ToString());
                    idTemplate = int.Parse(dbDataRowKokeShitje["IDTEMPLATE"].ToString());
                    idKonfigAmbjente = int.Parse(dbDataRowKokeShitje["IDKONFIGAMBJENTE"].ToString());
                    int.TryParse(dbDataRowKokeShitje["IDKLIENTFURNITOR"].ToString(), out idKlientFurnitor);
                    idProjekt = int.Parse(dbDataRowKokeShitje["IDPROJEKT"].ToString());
                    nrProjekt = dbDataRowKokeShitje["NRPROJEKT"].ToString();
                    nrDok = dbDataRowKokeShitje["NRDOK"].ToString();
                    nrSerial = dbDataRowKokeShitje["NRSERIAL"].ToString();
                    dtDok = DateTime.Parse(dbDataRowKokeShitje["DTDOK"].ToString());
                    DateTime.TryParse(dbDataRowKokeShitje["DTMATURIMI"].ToString(), out dtMaturimi);
                    idMonedha = int.Parse(dbDataRowKokeShitje["IDMONEDHA"].ToString());
                    kursi = double.Parse(dbDataRowKokeShitje["KURSI"].ToString());
                    int.TryParse(dbDataRowKokeShitje["IdMenyreTransporti"].ToString(), out idMenyreTransporti);
                    dtTransportimi = DateTime.Parse(dbDataRowKokeShitje["DTTRANSPORTIMI"].ToString());
                    int.TryParse(dbDataRowKokeShitje["IdKushtDergimi"].ToString(), out idKushtDergimi);
                    int.TryParse(dbDataRowKokeShitje["IDAGJENT"].ToString(), out idAgjent);
                    int.TryParse(dbDataRowKokeShitje["IDMENYREPAGESE"].ToString(), out idMenyrePagese);
                    int.TryParse(dbDataRowKokeShitje["IDKUSHTPAGESE"].ToString(), out idKushtPagese);
                    zbritje = double.Parse(dbDataRowKokeShitje["ZBRITJE"].ToString());
                    totali = double.Parse(dbDataRowKokeShitje["TOTALI"].ToString());
                    tvsh = double.Parse(dbDataRowKokeShitje["TVSH"].ToString());
                    cash = double.Parse(dbDataRowKokeShitje["CASH"].ToString());
                    int stap = 0;
                    int.TryParse(dbDataRowKokeShitje["STATUSAPROVIMI"].ToString(), out stap);
                    statusAprovimi = (StatusAprovimi)stap;
                    dtRegjistrimi = DateTime.Parse(dbDataRowKokeShitje["DTREGJISTRIMI"].ToString());
                    idStatusDok = int.Parse(dbDataRowKokeShitje["IDSTATUSDOK"].ToString());
                    idNdermarje = int.Parse(dbDataRowKokeShitje["IdNdermarrje"].ToString());
                    idNdermarjeVit = int.Parse(dbDataRowKokeShitje["IdNdermarrjeVit"].ToString());
                    int.TryParse(dbDataRowKokeShitje["IDNIVELGJENERUES"].ToString(), out idNivelGjenerues);
                    int.TryParse(dbDataRowKokeShitje["IDKONFIGGJENERUES"].ToString(), out idKonfigGjenerues);
                    int.TryParse(dbDataRowKokeShitje["IDGJENERUES"].ToString(), out idGjenerues);
                    int.TryParse(dbDataRowKokeShitje["IDDOKNGA"].ToString(), out idDokNga);
                    adresaFaturimit = dbDataRowKokeShitje["ADRESAFATURIMIT"].ToString();
                    adresaDergimit = dbDataRowKokeShitje["ADRESADERGIMIT"].ToString();
                    pershkrimi = dbDataRowKokeShitje["PERSHKRIMI"].ToString();
                    totaliMeZbritjeMeTvsh = double.Parse(dbDataRowKokeShitje["TotaliMeZbritjeMeTVSH"].ToString());
                    dogana = bool.Parse(dbDataRowKokeShitje["DOGANA"].ToString());
                    gjeneruar = bool.Parse(dbDataRowKokeShitje["GJENERUAR"].ToString());
                    int.TryParse(dbDataRowKokeShitje["IDDEGEADMINISTRATIVE"].ToString(), out idDegeAdministrative);
                    int.TryParse(dbDataRowKokeShitje["IDPIKESHITJEFURNIZIMI"].ToString(), out idPikeShitjeFurnizimi);
                    int.TryParse(dbDataRowKokeShitje["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    int.TryParse(dbDataRowKokeShitje["IDKRIJUESI"].ToString(), out idKrijuesi);
                    krijuesi = dbDataRowKokeShitje["Krijuesi"].ToString();
                    int.TryParse(dbDataRowKokeShitje["IDRAPORTDESING"].ToString(), out idRaportDesing);
                    DateTime.TryParse(dbDataRowKokeShitje["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRowKokeShitje["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    DateTime.TryParse(dbDataRowKokeShitje["AfatKohor"].ToString(), out afatKohor);
                    int.TryParse(dbDataRowKokeShitje["IDGRUP1"].ToString(), out idGrup1);
                    int.TryParse(dbDataRowKokeShitje["IDGRUP2"].ToString(), out idGrup2);
                    int.TryParse(dbDataRowKokeShitje["IDGRUP3"].ToString(), out idGrup3);
                    double.TryParse(dbDataRowKokeShitje["PERQINDJEAGJENTI"].ToString(), out perqindjeAgjenti);
                    int.TryParse(dbDataRowKokeShitje["IDTRANSFERIMI"].ToString(), out idTransferimi);
                    int.TryParse(dbDataRowKokeShitje["IDKONFIGTRANSFERIMI"].ToString(), out idKonfigTransferimi);
                    bool.TryParse(dbDataRowKokeShitje["KASE"].ToString(), out kase);
                    bool.TryParse(dbDataRowKokeShitje["KUPON"].ToString(), out kupon);
                    bool.TryParse(dbDataRowKokeShitje["FATUREPERMBLEDHESE"].ToString(), out faturePermbledhese);
                    int st = 0;
                    int.TryParse(dbDataRowKokeShitje["STATUSTRANSFERIMI"].ToString(), out st);
                    emerKlienti = dbDataRowKokeShitje["EMERKLIENTI"].ToString();
                    kontakti = dbDataRowKokeShitje["KONTAKTI"].ToString();
                    statusTransferimi = (StatusTrasferimi)st;
                    colGaranci = new colGaranciArtikulli();
                    DateTime.TryParse(dbDataRowKokeShitje["DTFILLIMI"].ToString(), out dtFillimi);
                    DateTime.TryParse(dbDataRowKokeShitje["DTMBARIMI"].ToString(), out dtMbarimi);
                    int.TryParse(dbDataRowKokeShitje["IDAUTOMJETI"].ToString(), out idAutomjet);
                    double.TryParse(dbDataRowKokeShitje["KILOMETRAAUTO"].ToString(), out kilometraAuto);
                    int.TryParse(dbDataRowKokeShitje["IDAGJENTI2"].ToString(), out idAgjenti2);
                    double.TryParse(dbDataRowKokeShitje["PERQINDJEAGJENTI2"].ToString(), out perqindjeAgjenti2);
                    int.TryParse(dbDataRowKokeShitje["IDAGJENTI3"].ToString(), out idAgjenti3);
                    double.TryParse(dbDataRowKokeShitje["PERQINDJEAGJENTI3"].ToString(), out perqindjeAgjenti3);
                    marresi = dbDataRowKokeShitje["MARRESI"].ToString();
                    int.TryParse(dbDataRowKokeShitje["IDTRANSPORTUES"].ToString(), out idTransportues);
                    bool.TryParse(dbDataRowKokeShitje["SHPENZJOTEZBRITSHME"].ToString(), out shpenzimeJoTeZbritshme);
                    int.TryParse(dbDataRowKokeShitje["IDARKA"].ToString(), out idarka);
                    DateTime.TryParse(dbDataRowKokeShitje["DTFATURE"].ToString(), out dtFature);
                    int.TryParse(dbDataRowKokeShitje["MUAJRAPORTIMI"].ToString(), out muajRaportimi);
                    int.TryParse(dbDataRowKokeShitje["IDVITRAPORTIMI"].ToString(), out idVitRaportimi);
                    bool.TryParse(dbDataRowKokeShitje["ZBRITJENEVLERE"].ToString(), out zbritjeNeVlere);
                    double.TryParse(dbDataRowKokeShitje["PERQINDJEZBRITJE"].ToString(), out perqindjeZbritje);
                    DateTime.TryParse(dbDataRowKokeShitje["DTKRIJIMIPAJISJE"].ToString(), out dtKrijimiPajisje);
                    koordinata = dbDataRowKokeShitje["KOORDINATA"].ToString();
                    niptKlienti = dbDataRowKokeShitje["NIPTK"].ToString();
                    qytetiK = dbDataRowKokeShitje["QytetiK"].ToString();
                    int.TryParse(dbDataRowKokeShitje["IdKategoriSeriali"].ToString(), out idKategoriSeriali);
                    shenime2 = dbDataRowKokeShitje["Shenime2"].ToString();
                    bool.TryParse(dbDataRowKokeShitje["KartaPaPagese"].ToString(), out zbritjeNeVlere);
                    int.TryParse(dbDataRowKokeShitje["IDDOKTRANSFERIMNGA"].ToString(), out idDokTransferimNga);
                    kerkuarNga = dbDataRowKokeShitje["KERKUARNGA"].ToString();
                    idMarreveshje = dbDataRowKokeShitje["IDMARREVESHJE"].ToString();
                    DateTime.TryParse(dbDataRowKokeShitje["DATEKERKESE"].ToString(), out dateKerkese);
                    ColKlienteFurnitoreVartes = colKlienteFurnitore.MerrKlientFurnitoreVartesSipasIdShitjeKoka(IdShitjeKoka);
                    int.TryParse(dbDataRowKokeShitje["StatusMarreveshje"].ToString(), out int stmarreveshje);
                    StatusMarreveshje = (StatusMarreveshje)stmarreveshje;
                    nrDokMagazine = dbDataRowKokeShitje["NRDOKMAGAZINE"].ToString();
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    ImbLogger.LogErrorShitje($"Exception:{ex}");
                    ImbLogger.Error(ex);
                    throw new Exception("ERROR: Gabim gjate marrjes se kokes se shitjes nga db-ja");
                }
            }
            else
            {
                ImbLogger.LogTraceShitje("Mbaroi metoda mbushKokeShitjePaTrupSipasDT");
                return false;
            }
        }

        #endregion
    }
}