using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;
using DbCore.DbAdmin;
using DbCore.DbAsete;
using DbCore.DbKontabiliteti;
using DbCore.DbRegjistrim;
using DbCore.DbShare;
using DbCore.IMBUtils;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Fiskalizimi.Controls;
using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.Messages;
using DbCore.IMBUtils.Validation;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Newtonsoft.Json;

namespace DbCore.DbInventari
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne artikujt
    ///  (Te dhenat  merren nga tabela : T_ARTIKULLI)
    /// </summary>
    public class clsArtikulli : ICloneable
    {

        #region Atribute

        public static string AfatGjateLabel = "Afatgjate";
        public static string AfatShkurterLabel = "Afatshkurter";
        public const string postStringTvsh = "postStringTvsh";
        public const string paymentBucket = "imb-payment.appspot.com";
        private int idArtikulli;
        private string kodArtikulli;
        private string pershkrimArtikulli;
        private string pershkrimiAngArtikulli;
        private string kodiDoganorArtikulli;
        private string vendodhjeArtikulli;
        private int kodifikimi1Artikulli;
        private int kodifikimi2Artikulli;
        private int kodifikimi3Artikulli;
        private string origjineArtikulli;
        private int njesi1Artikulli;
        private int njesi2Artikulli;
        private decimal koeficientArtikulli;
        private int idFurnitoriKryesor;
        private decimal peshaBrutoArtikulli;
        private decimal peshaNetoArtikulli;
        private bool detajimArtikulli;
        private int klasa;
        private int idSkemaKontabilitetiArtikulli;
        private int idLlogariInventari;
        private int idLlogariBlerje;
        private int idLlogariShitje;
        private int idLlogariTeTrete;
        private int idLlogariShpenzime;
        private int idLlogariAmortizimi;
        private int idLlogariPakesim;
        private int idLlogariKomision;
        private decimal minimumArtikulli;
        private decimal maximumArtikulli;
        private int metodeKostojeArtikulli;
        private string pershkrimMetodeKostoje;
        private int llogaritjaKMSHArtikulli;
        private int zevendesimAutomatikArtikulli;
        private int idPerdoruesi;
        //private int idNderViti;
        private int idNdermarje;
        private string kodKodifikimi1;
        private string kodKodifikimi2;
        private string kodKodifikimi3;
        private string kodNjesia1;
        private string kodNjesia2;
        private string pershkrimNjesia1;
        private string pershkrimNjesia2;
        private string kodKlientFurnitori;
        private string pershkrimKlasa;
        private string kodiSkema;
        private string nrLlogBlerje;
        private string nrLlogShitje;
        private string nrLlogInventari;
        private string nrLlogShpenzime;
        private string nrLlogTeTrete;
        private string nrLlogAmortizimi;
        private string nrLlogPakesimi;
        private string nrLlogRez;
        private string nrLlogPakRez;
        private string nrLlogKomision;
        private string kodTaksa;
        private decimal sasiNjesi;
        private decimal scrap;
        private colFurnitoreArtikujsh oColFurnitoreArtikujsh;
        private colArtikujtZevendesues oColArtikujtZevendesues;
        private colVleraFushaShtese oColVleratFushaShtese;
        private colBuxhetet oColBuxhetet;
        //private DbAdmin.colLidhjetAutorizim oColLidhjetAutorizim;
        private colDetajimePerArt oColDetajime;
        private colDetajimePerArt oColDetajime2;
        private colArtikulliPerberes colArtikujPerberes;
        private colGjendjeArtikulli colGjendjeArtikulliMag;
        private colArtikullVfone colArtikujVfone;
        private DbAsete.colAseteNormaAmortizimiAbstract colNorma;
        private colAseteNormaAmortizimiAbstract colNormaRezerva;
        private string autorizimet;
        private bool kontrollGjendje;
        private bool kontrollCmimPerDetajim;
        private bool kontrollGjendjeArtikulli;
        private int idTvsh;
        private int idKonfig;
        private bool aktiv;
        private int idStatusDok;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private bool llojiArt;
        private int nrKaraktereTAC;
        private bool llogaritKomision;

        /// <summary>
        /// tregon nese artikulli perdoret si prodhim me porosi
        /// </summary>
        private bool prodhimMePorosi;
        private int idKategoriDetajimi;
        private string kategoriDetajimi;
        private int idKategoriDetajimi2;
        private string kategoriDetajimi2;
        private bool kontrollGjendjeDetajim2;
        private int idObjektivaKosto;
        private string objektiva;
        private int idllojGarancie;
        private decimal garancia;
        private string magazina;
        private int idmagazina;
        private bool iRezervueshem;
        private bool perTransferim;
        //private colKodbare colKodbaret;
        private DbCore.DbShare.colArkiva oArkiva;
        private bool loan;
        private bool dhurate;
        private int aplikimDhurate;
        private decimal pike;
        private decimal vlere;
        private string kodVFOne;
        private bool meSerial;
        private bool iShitshem;
        private bool mbetjeShitshme;
        private colKodbare oColKodbare;
        private int idArtRaportuesi;
        private bool perPeshore;
        private string pershkrimFurnitori;
        private string siperfaqjaM2;
        private string nrKontrate;
        private string nrPasurie;
        private string zonaKadastrale;
        private string shasia;
        private string marka;
        private string modeli;
        private string vitProdhimi;
        private string teDhenaTeknika;
        private bool meBarkodLogjik;
        private string skemaBarkodit;
        private string kodbar;
        private bool eshteAfatShkurter;
        private bool aparatBazaar;
        private string kodOferte;
        private bool artikullIVjeter;
        private int idFormatSeriali;
        //private int v;
        private int idNdermarrje;
        private int idLlogRez;
        private int idLlogPakRez;
        private bool merezerverivleresimi;
        private static int llojdetajimi;
        private int stokuMaxVfOne;
        private string kodiiBarit;
        private bool iRimbursueshem;
        #endregion

        #region Properties
        public bool MeBarkodLogjik
        {
            get { return meBarkodLogjik; }
            set { meBarkodLogjik = value; }
        }

        public string SkemaBarkodit
        {
            get { return skemaBarkodit; }
            set { skemaBarkodit = value; }
        }
        public int AplikimDhurate
        {
            get
            {
                return aplikimDhurate;
            }
            set
            {
                aplikimDhurate = value;
            }
        }

        public colArtikulliPerberes ColArtikujPerberes
        {
            get
            {
                return colArtikujPerberes;
            }
            set
            {
                if (colArtikujPerberes == value)
                    return;
                colArtikujPerberes = value;
            }
        }

        public DbAsete.colAseteNormaAmortizimiAbstract ColNorma
        {
            get
            {
                return colNorma;
            }
            set
            {
                colNorma = value;
            }
        }


        public colGjendjeArtikulli oColGjendjeArtikulli
        {
            get { return colGjendjeArtikulliMag; }
            set { colGjendjeArtikulliMag = value; }
        }

        public bool Dhurate
        {
            get
            {
                return dhurate;
            }
            set
            {
                dhurate = value;
            }
        }
        /// <summary>
        /// tregon nese ky artikull do sherbeje si mbetje e shitshme per nje artikull prodhim psh tallash
        /// </summary>
        public bool MbetjeShitshme
        {
            get
            {
                return mbetjeShitshme;
            }
            set
            {
                mbetjeShitshme = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos nje collection me objekte te tipit <see cref="DbCore.DbInventari.clsKodbari"/>
        /// </summary>
        public colKodbare OColKodbare
        {
            get { return oColKodbare; }
            set { oColKodbare = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdArtikulli
        {
            get { return idArtikulli; }
            set { idArtikulli = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e nivelit te tvsh.
        /// </summary>
        public int IdTvsh
        {
            get { return idTvsh; }
            set { idTvsh = value; }
        }

        /// <summary>
        /// tregon nese artikulli eshte i rezervueshem apo jo
        /// 
        /// </summary>
        public bool IRezervueshem
        {
            get
            {
                return iRezervueshem;
            }
            set
            {
                iRezervueshem = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos kodin e artikullit.
        /// </summary>
        public string KodArtikulli
        {
            get { return kodArtikulli; }
            set { kodArtikulli = value; }
        }

        /// <summary>
        /// kodi qe e lidh me artikujt dhurate me inprom
        /// </summary>
        public string KodVFOne
        {
            get
            {
                return kodVFOne;
            }
            set
            {
                kodVFOne = value;
            }
        }

        /// <summary>
        /// tregon nese ky artikull ka loan 
        /// </summary>
        public bool Loan
        {
            get
            {
                return loan;
            }
            set
            {
                loan = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos peshkrimin e artikullit.
        /// </summary>
        public string PershkrimArtikulli
        {
            get { return pershkrimArtikulli; }
            set { pershkrimArtikulli = value; }
        }


        /// <summary>
        /// Kthen/Vendos peshkrimin e furnitorit per artikullin.
        /// </summary>
        public string PershkrimFurnitori
        {
            get { return pershkrimFurnitori; }
            set { pershkrimFurnitori = value; }
        }



        /// <summary>
        /// Kthen/Vendos pershkrimi ne gjuhen angleze te artikullit.
        /// </summary>
        public string PershkrimiAngArtikulli
        {
            get { return pershkrimiAngArtikulli; }
            set { pershkrimiAngArtikulli = value; }
        }

        /// <summary>
        /// Kthen/Vendos kodin doganor te artikullit.
        /// </summary>
        public string KodiDoganorArtikulli
        {
            get { return kodiDoganorArtikulli; }
            set { kodiDoganorArtikulli = value; }
        }

        /// <summary>
        /// tregon nese ky artikull do perdoret per transferim apo jo
        /// </summary>
        public bool PerTransferim
        {
            get
            {
                return perTransferim;
            }
            set
            {
                perTransferim = value;
            }
        }

        public decimal Pike
        {
            get
            {
                return pike;
            }
            set
            {
                pike = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos vendodhjen e artikullit.
        /// </summary>
        public string VendodhjeArtikulli
        {
            get { return vendodhjeArtikulli; }
            set { vendodhjeArtikulli = value; }
        }

        /// <summary>
        /// Kthen/Vendos kodifikimi i pare i artikullit.
        /// </summary>
        public int Kodifikimi1Artikulli
        {
            get { return kodifikimi1Artikulli; }
            set { kodifikimi1Artikulli = value; }
        }

        /// <summary>
        /// Kthen/Vendos kodifikimi i dyte i artikullit.
        /// </summary>
        public int Kodifikimi2Artikulli
        {
            get { return kodifikimi2Artikulli; }
            set { kodifikimi2Artikulli = value; }
        }
        /// <summary>
        /// Kthen/Vendos kodifikimi i trete i artikullit.
        /// </summary>
        public int Kodifikimi3Artikulli
        {
            get { return kodifikimi3Artikulli; }
            set { kodifikimi3Artikulli = value; }
        }
        /// <summary>
        /// Kthen/Vendos origjina e artikullit.
        /// </summary>
        public string OrigjineArtikulli
        {
            get { return origjineArtikulli; }
            set { origjineArtikulli = value; }
        }

        /// <summary>
        /// Kthen/Vendos njesia e pare e artikullit.
        /// </summary>
        public int Njesi1Artikulli
        {
            get { return njesi1Artikulli; }
            set { njesi1Artikulli = value; }
        }

        /// <summary>
        /// Kthen/Vendos njesia e dyte e artikullit.
        /// </summary>
        public int Njesi2Artikulli
        {
            get { return njesi2Artikulli; }
            set { njesi2Artikulli = value; }
        }

        /// <summary>
        /// Kthen/Vendos koeficienti midis dy njesive te artikullit.
        /// </summary>
        public decimal KoeficientArtikulli
        {
            get { return koeficientArtikulli; }
            set { koeficientArtikulli = value; }
        }

        /// <summary>
        /// Kthen/Vendos id e furnitorit kryesor.
        /// </summary>
        public int IdFurnitoriKryesor
        {
            get { return idFurnitoriKryesor; }
            set { idFurnitoriKryesor = value; }
        }

        /// <summary>
        /// Kthen/Vendos peshen bruto te artikullit.
        /// </summary>
        public decimal PeshaBrutoArtikulli
        {
            get { return peshaBrutoArtikulli; }
            set { peshaBrutoArtikulli = value; }
        }

        /// <summary>
        /// Kthen/Vendos peshen neto te artikullit.
        /// </summary>
        public decimal PeshaNetoArtikulli
        {
            get { return peshaNetoArtikulli; }
            set { peshaNetoArtikulli = value; }
        }

        /// <summary>
        /// Kthen/Vendos nese artikulli eshte me detajim apo jo.
        /// </summary>
        public bool DetajimArtikulli
        {
            get { return detajimArtikulli; }
            set { detajimArtikulli = value; }
        }

        /// <summary>
        /// Kthen/Vendos klasen e artikullit.
        /// </summary>
        public int Klasa
        {
            get { return klasa; }
            set { klasa = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e skemes se kontabilitetit te artikullit.
        /// </summary>
        public int IdSkemaKontabilitetiArtikulli
        {
            get { return idSkemaKontabilitetiArtikulli; }
            set { idSkemaKontabilitetiArtikulli = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e llogarise se inventarit.
        /// </summary>
        public int IdLlogariInventari
        {
            get { return idLlogariInventari; }
            set { idLlogariInventari = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e llogarise se pakesimit.
        /// </summary>
        public int IdLlogariPakesim
        {
            get { return idLlogariPakesim; }
            set { idLlogariPakesim = value; }
        }


        /// <summary>
        /// Kthen/Vendos ID-ne e llogarise se blerjes.
        /// </summary>
        public int IdLlogariBlerje
        {
            get { return idLlogariBlerje; }
            set { idLlogariBlerje = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne  e llogarise se shitjes.
        /// </summary>
        public int IdLlogariShitje
        {
            get { return idLlogariShitje; }
            set { idLlogariShitje = value; }
        }

        /// <summary>
        /// Kthen/Vendos id-ne e llogarise tek te tretet.
        /// </summary>
        public int IdLlogariTeTrete
        {
            get { return idLlogariTeTrete; }
            set { idLlogariTeTrete = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e llogarise se shpenzimeve.
        /// </summary>
        public int IdLlogariShpenzime
        {
            get { return idLlogariShpenzime; }
            set { idLlogariShpenzime = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e llogarise se amortizimeve.
        /// </summary>
        public int IdLlogariAmortizimi
        {
            get { return idLlogariAmortizimi; }
            set { idLlogariAmortizimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e llogarise se pakesimit te rezervimeve.
        /// </summary>
        public int IdLlogPakRez
        {
            get { return idLlogPakRez; }
            set { idLlogPakRez = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e llogarise se rezervimeve.
        /// </summary>
        public int IdLlogRez
        {
            get { return idLlogRez; }
            set { idLlogRez = value; }
        }



        /// <summary>
        /// Kthen/Vendos sasine min te artikullit.
        /// </summary>
        public decimal MinimumArtikulli
        {
            get { return minimumArtikulli; }
            set { minimumArtikulli = value; }
        }

        /// <summary>
        /// Kthen/Vendos sasine max te artikullit.
        /// </summary>
        public decimal MaximumArtikulli
        {
            get { return maximumArtikulli; }
            set { maximumArtikulli = value; }
        }

        /// <summary>
        /// Kthen/Vendos metoden e kostos te artikullit.
        /// </summary>
        public int MetodeKostojeArtikulli
        {
            get { return metodeKostojeArtikulli; }
            set { metodeKostojeArtikulli = value; }
        }

        /// <summary>
        /// Kthen/Vendos llogaritjen KMSH te artikullit.
        /// </summary>
        public int LlogaritjaKMSHArtikulli
        {
            get { return llogaritjaKMSHArtikulli; }
            set { llogaritjaKMSHArtikulli = value; }
        }

        public decimal Vlere
        {
            get
            {
                return vlere;
            }
            set
            {
                vlere = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos zevendesimin automatik te artikullit.
        /// <example>0- nuk behet zevendesim automatik, 1-behet zevendesim gjithmone ne menyre automatike, 2- pyetet perdoruesi per konfirmim</example>
        /// </summary>
        public int ZevendesimAutomatikArtikulli
        {
            get { return zevendesimAutomatikArtikulli; }
            set { zevendesimAutomatikArtikulli = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e perdoruesit qe ka kryer veprimin.
        /// </summary>
        public int IdPerdoruesi
        {
            get { return idPerdoruesi; }
            set { idPerdoruesi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne  e ndermarje vitit.
        /// </summary>
        //public int IdNderViti
        //{
        //    get { return idNderViti; }
        //    set { idNderViti = value; }
        //}

        /// <summary>
        /// Kthen/Vendos ID-ne  e ndermarjes.
        /// </summary>
        public int IdNdermarje
        {
            get { return idNdermarje; }
            set { idNdermarje = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne  e konfigurimit.
        /// </summary>
        public int IdKonfig
        {
            get { return idKonfig; }
            set { idKonfig = value; }
        }

        /// <summary>
        /// Kthen/Vendos kontrollin per gjendjen e artikullit.
        /// </summary>
        public bool KontrollGjendje
        {
            get { return kontrollGjendje; }
            set { kontrollGjendje = value; }
        }

        /// <summary>
        /// Kthen/Vendos  kontrollin per cmimin.
        /// </summary>
        public bool KontrollCmimi
        {
            get { return kontrollCmimPerDetajim; }
            set { kontrollCmimPerDetajim = value; }
        }

        /// <summary>
        /// Kthen/Vendos kontrollin e gjendjes per detajimin e artikullit
        /// </summary>
        public bool KontrollGjendjeArtikulli
        {
            get { return kontrollGjendjeArtikulli; }
            set { kontrollGjendjeArtikulli = value; }
        }

        /// <summary>
        /// Kthen/Vendos kodin e kodifikimit 1 te artikullit
        /// </summary>
        public string KodKodifikimi1
        {
            get { return kodKodifikimi1; }
            set { kodKodifikimi1 = value; }
        }

        /// <summary>
        /// Kthen/Vendos kodin e kodifikimit 2 te artikullit
        /// </summary>
        public string KodKodifikimi2
        {
            get { return kodKodifikimi2; }
            set { kodKodifikimi2 = value; }
        }
        /// <summary>
        /// Kthen/Vendos kodin e kodifikimit 3 te artikullit
        /// </summary>
        public string KodKodifikimi3
        {
            get { return kodKodifikimi3; }
            set { kodKodifikimi3 = value; }
        }

        /// <summary>
        /// Kthen kodin e njesise se pare
        /// </summary>
        public string KodNjesia1
        {
            get
            {
                return kodNjesia1;
            }
            set
            {
                kodNjesia1 = value;
            }
        }

        /// <summary>
        /// Kthen kodin e njesise se dyte
        /// </summary>
        public string KodNjesia2
        {
            get
            {
                return kodNjesia2;
            }
            set { kodNjesia2 = value; }
        }

        /// <summary>
        /// Kthen/Vendos pershkrim njesia 1
        /// </summary>
        public string PershkrimNjesia1
        {
            get { return pershkrimNjesia1; }
            set { pershkrimNjesia1 = value; }
        }

        /// <summary>
        /// Kthen/Vendos pershkrim njesia 2
        /// </summary>
        public string PershkrimNjesia2
        {
            get { return pershkrimNjesia2; }
            set { pershkrimNjesia2 = value; }
        }

        /// <summary>
        /// Kthen/Vendos kodfurnitori
        /// </summary>
        public string KodFurnitori
        {
            get { return kodKlientFurnitori; }
            set { kodKlientFurnitori = value; }
        }

        /// <summary>
        /// Kthen/Vendos pershkrimi i klases
        /// </summary>
        public string PershkrimKlasa
        {
            get { return pershkrimKlasa; }
            set { pershkrimKlasa = value; }
        }

        /// <summary>
        /// Kthen/Vendos kodin e skemes
        /// </summary>
        public string KodSkema
        {
            get { return kodiSkema; }
            set { kodiSkema = value; }
        }

        /// <summary>
        /// Kthen/Vendos nr e llogarise se blerjes
        /// </summary>
        public string NrLlogBlerje
        {
            get { return nrLlogBlerje; }
            set { nrLlogBlerje = value; }
        }

        /// <summary>
        /// Kthen/Vendos nr e llogarise se inventarit
        /// </summary>
        public string NrLlogInventari
        {
            get { return nrLlogInventari; }
            set { nrLlogInventari = value; }
        }

        /// <summary>
        /// Kthen/Vendos nr e llogarise se shitjes
        /// </summary>
        public string NrLlogShitje
        {
            get { return nrLlogShitje; }
            set { nrLlogShitje = value; }
        }

        /// <summary>
        /// Kthen/Vendos nr e llogarise se shpenzimit
        /// </summary>
        public string NrLlogShpenzime
        {
            get { return nrLlogShpenzime; }
            set { nrLlogShpenzime = value; }
        }

        /// <summary>
        /// Kthen/Vendos nr e llogarise tek te tretet
        /// </summary>
        public string NrLlogTeTrete
        {
            get { return nrLlogTeTrete; }
            set { nrLlogTeTrete = value; }
        }

        /// <summary>
        /// Kthen/Vendos nr e llogarise amortizimi
        /// </summary>
        public string NrLlogAmortizimi
        {
            get { return nrLlogAmortizimi; }
            set { nrLlogAmortizimi = value; }
        }
        /// <summary>
        /// Kthen/Vendos nr e llogarise se pakesimit
        /// </summary>
        public string NrLlogPakesimi
        {
            get { return nrLlogPakesimi; }
            set { nrLlogPakesimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos nr e llogarise amortizimi
        /// </summary>
        public string NrLlogRez
        {
            get { return nrLlogRez; }
            set { nrLlogRez = value; }
        }
        /// <summary>
        /// Kthen/Vendos nr e llogarise se pakesimit
        /// </summary>
        public string NrLlogPakRez
        {
            get { return nrLlogPakRez; }
            set { nrLlogPakRez = value; }
        }

        /// <summary>
        /// Kthen/Vendos kodin e takses
        /// </summary>
        public string KodTaksa
        {
            get { return kodTaksa; }
            set { kodTaksa = value; }
        }

        /// <summary>
        /// Kthen/Vendos Aktiv
        /// </summary>
        public bool Aktiv
        {
            get { return aktiv; }
            set { aktiv = value; }
        }

        /// <summary>
        /// Kthen/Vendos nje koleksion me furnitoret e artikullit.
        /// </summary>
        public colFurnitoreArtikujsh OColFurnitoreArtikujsh
        {
            get { return oColFurnitoreArtikujsh; }
            set { oColFurnitoreArtikujsh = value; }
        }

        /// <summary>
        /// Kthen/Vendos id e kategorise se detajimit te pare
        /// </summary>
        public int IdKategoriDetajimi
        {
            get
            {
                return idKategoriDetajimi;
            }
            set
            {
                idKategoriDetajimi = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos id e kategorise se detajimit te dyte
        /// </summary>
        public int IdKategoriDetajimi2
        {
            get
            {
                return idKategoriDetajimi2;
            }
            set
            {
                idKategoriDetajimi2 = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos nje koleksion me detajimet e artikullit.
        /// </summary>
        public colDetajimePerArt OColDetajime
        {
            get { return oColDetajime; }
            set { oColDetajime = value; }
        }

        /// <summary>
        /// Kthen/Vendos nje koleksion me detajimet e artikullit te kategorise se dyte.
        /// </summary>
        public colDetajimePerArt OColDetajime2
        {
            get { return oColDetajime2; }
            set { oColDetajime2 = value; }
        }

        /// <summary>
        /// Kthen/Vendos nje koleksion me artikujt zevendesues.
        /// </summary>
        public colArtikujtZevendesues OColArtikujtZevendesues
        {
            get { return oColArtikujtZevendesues; }
            set { oColArtikujtZevendesues = value; }
        }

        /// <summary>
        /// Kthen/Vendos  nje koleksion me buxhetet.
        /// </summary>
        public colBuxhetet OColBuxhetet
        {
            get { return oColBuxhetet; }
            set { oColBuxhetet = value; }
        }

        /// <summary>
        /// Kthen/Vendos nje koleksion me vlerat e fushave shtese.
        /// </summary>
        public colVleraFushaShtese OColVleraFushaShtese
        {
            get { return oColVleratFushaShtese; }
            set { oColVleratFushaShtese = value; }
        }

        /// <summary>
        /// Kthen/Vendos  autorizimet e artikullit.
        /// </summary>
        public string Autorizimet
        {
            get { return autorizimet; }
            set { autorizimet = value; }
        }

        public int IdStatusDok
        {
            get { return idStatusDok; }
            set { idStatusDok = value; }
        }

        public DateTime DtKrijimi
        {
            get
            {
                return dtKrijimi;
            }
        }

        public DateTime DtModifikimi
        {
            get
            {
                return dtModifikimi;
            }
        }

        /// <summary>
        /// Kthen/Vendos nese artikulli eshte afatgjate apo afatshkurter. afatshkurter = false, afatgjate = true
        /// </summary>
        public bool LlojiArt
        {
            get { return llojiArt; }
            set { llojiArt = value; }
        }

        public int NrKaraktereTAC
        {
            get { return nrKaraktereTAC; }
            set { nrKaraktereTAC = value; }
        }


        public bool LlogaritKomision
        {
            get { return llogaritKomision; }
            set { llogaritKomision = value; }
        }
        /// <summary>
        /// kthen/vendos scrapin ne perqindje
        /// </summary>
        public decimal Scrap
        {
            get
            {
                return scrap;
            }
            set
            {
                scrap = value;
            }
        }

        /// <summary>
        /// kthen vendos sasine qe krijohet gjate nje procesi
        /// </summary>
        public decimal SasiNjesi
        {
            get
            {
                return sasiNjesi;
            }
            set
            {
                sasiNjesi = value;
            }
        }

        public string PershkrimMetodeKostoje
        {
            get
            {
                return pershkrimMetodeKostoje;
            }
            set
            {
                pershkrimMetodeKostoje = value;
            }
        }

        /// <summary>
        /// tregon nese artikulli perdoret si prodhim me porosi
        /// </summary>
        public bool ProdhimMePorosi
        {
            get
            {
                return prodhimMePorosi;
            }
            set
            {
                prodhimMePorosi = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos kontrollin per gjendjen e artikullit.
        /// </summary>
        public bool KontrollGjendjeDetajim2
        {
            get { return kontrollGjendjeDetajim2; }
            set { kontrollGjendjeDetajim2 = value; }
        }

        /// <summary>
        /// id e objektives se kostos
        /// </summary>
        public int IdObjektivaKosto
        {
            get
            {
                return idObjektivaKosto;
            }
            set
            {
                idObjektivaKosto = value;
            }
        }

        public string Objektiva
        {
            get
            {
                return objektiva;
            }
            set
            {
                objektiva = value;
            }
        }

        public int IdllojGarancie
        {
            get
            {
                return idllojGarancie;
            }
            set
            {
                idllojGarancie = value;
            }
        }

        public decimal Garancia
        {
            get
            {
                return garancia;
            }
            set
            {
                garancia = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos kodin e magazines default per artikullin
        /// </summary>
        public string Magazina
        {
            get { return magazina; }
            set { magazina = value; }
        }

        /// <summary>
        /// Kthen/Vendos id e magazines default per artikullin
        /// </summary>
        public int IdMagazina
        {
            get { return idmagazina; }
            set { idmagazina = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere nese artikulli eshte artikull afatgjate qe duhet te gjenerohet serial per cdo njesi artikulli, apo nese artikulli afatgjate do te gjeneroje vetem nje serial per te gjithe sasine e artikullit ne faturen e blerjes ose hyrjes.
        /// </summary>
        public bool MeSerial
        {
            get { return meSerial; }
            set { meSerial = value; }
        }

        /// <summary>
        /// 
        /// Merr ose jep vlere nese artikulli mund te shitet, apo jo.
        /// </summary>
        public bool IShitshem
        {
            get { return iShitshem; }
            set { iShitshem = value; }
        }

        /// <summary>
        /// 
        /// Merr ose jep vlere nese artikulli eshtep artikull per peshore, apo jo.
        /// </summary>
        public bool PerPeshore
        {
            get { return perPeshore; }
            set { perPeshore = value; }
        }

        /// <summary>
        /// Kthen/Vendos id artikullin e ndermarrjes raportuese 
        /// </summary>
        public int IdArtRaportuesi
        {
            get { return idArtRaportuesi; }
            set { idArtRaportuesi = value; }
        }

        /// <summary>
        /// Kthen/Vendos siperfaqjen ne M2
        /// </summary>
        public string SiperfaqjaM2
        {
            get { return siperfaqjaM2; }
            set { siperfaqjaM2 = value; }
        }
        /// <summary>
        /// Kthen/Vendos numrin e kontrates
        /// </summary>
        public string NrKontrate
        {
            get { return nrKontrate; }
            set { nrKontrate = value; }
        }

        /// <summary>
        /// Kthen/Vendos numrin e pasurise
        /// </summary>
        public string NrPasurie
        {
            get { return nrPasurie; }
            set { nrPasurie = value; }
        }
        /// <summary>
        /// Kthen/Vendos zonen kadastrale
        /// </summary>
        public string ZonaKadastrale
        {
            get { return zonaKadastrale; }
            set { zonaKadastrale = value; }
        }
        /// <summary>
        /// Kthen/Vendos shasine e makines
        /// </summary>
        public string Shasia
        {
            get { return shasia; }
            set { shasia = value; }
        }
        /// <summary>
        /// Kthen/Vendos marken e makines
        /// </summary>
        public string Marka
        {
            get { return marka; }
            set { marka = value; }
        }
        /// <summary>
        /// Kthen/Vendos modein e makines
        /// </summary>
        public string Modeli
        {
            get { return modeli; }
            set { modeli = value; }
        }

        /// <summary>
        /// Kthen/Vendos vitin e prodhimit te makines
        /// </summary>
        public string VitProdhimi
        {
            get { return vitProdhimi; }
            set { vitProdhimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos te dhenat teknike te makines
        /// </summary>
        public string TeDhenaTeknika
        {
            get { return teDhenaTeknika; }
            set { teDhenaTeknika = value; }
        }

        public IDictionary<string, object> HfArkiva { get; set; }

        public colArtikullVfone ColArtikujVfone
        {
            get
            {
                return colArtikujVfone;
            }

            set
            {
                colArtikujVfone = value;
            }
        }

        public bool AparatBazaar
        {
            get
            {
                return aparatBazaar;
            }

            set
            {
                aparatBazaar = value;
            }
        }

        public string KodOferte
        {
            get
            {
                return kodOferte;
            }

            set
            {
                kodOferte = value;
            }
        }

        public bool ArtikullIVjeter
        {
            get
            {
                return artikullIVjeter;
            }

            set
            {
                artikullIVjeter = value;
            }
        }

        public int IdFormatSeriali
        {
            get
            {
                return idFormatSeriali;
            }

            set
            {
                idFormatSeriali = value;
            }
        }

        public bool MeRezerveRivleresimi
        {
            get
            {
                return merezerverivleresimi;
            }

            set
            {
                merezerverivleresimi = value;
            }
        }

        public colAseteNormaAmortizimiAbstract ColNormaRezerva
        {
            get
            {
                return colNormaRezerva;
            }

            set
            {
                colNormaRezerva = value;
            }
        }
        public int IdLlogariKomisioni
        {
            get { return idLlogariKomision; }
            set { idLlogariKomision = value; }
        }

        public string NrLlogKomision
        {
            get { return nrLlogKomision; }
            set { nrLlogKomision = value; }
        }

        /// <summary>
        /// Kthen/Vendos vleren e stokut maksimal per vodafone one
        /// </summary>
        public int StokuMaxVfOne
        {
            get { return stokuMaxVfOne; }
            set { stokuMaxVfOne = value; }
        }

        public string KodiIBarit { get { return kodiiBarit; } set { kodiiBarit = value; } }
        public bool IRimbursueshem { get { return iRimbursueshem; } set { iRimbursueshem = value; } }

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i plote
        /// </summary>
        /// <param name="idArtikulli"> id ritese e artikullit</param>
        /// <param name="kodArtikulli"> kodi i artkullit</param>
        /// <param name="pershkrimArtikulli"> pershkrimi i artikullit</param>
        /// <param name="pershkrimiAngArtikulli">pershkrimi i artikullit ne anglisht</param>
        /// <param name="kodiDoganorArtikulli">kodi doganor i artikullit</param>
        /// <param name="vendodhjeArtikulli">vendodhja e artikullit</param>
        /// <param name="kodifikimi1Artikulli">kodifikimi 1 i artikullit</param>
        /// <param name="kodifikimi2Artikulli"> kodifikimi 2 i artikullit</param>
        /// <param name="origjineArtikulli"> origjina e artikullit</param>
        /// <param name="njesi1Artikulli"> njesia e pare e artikullit</param>
        /// <param name="njesi2Artikulli">njesia e dyte e artikullit</param>
        /// <param name="koeficientArtikulli"> koeficienti midis njesive te artikullit</param>
        /// <param name="idFurnitoriKryesor">id e furnitorit kryesor</param>
        /// <param name="peshaBrutoArtikulli">pesha bruto e artikullit</param>
        /// <param name="peshaNetoArtikulli">pesha neto e artikullit</param>
        /// <param name="detajimArtikulli">detajimi i artikullit</param>
        /// <param name="klasa">klasa e artikullit</param>
        /// <param name="idSkemaKontabilitetiArtikulli">skema e kontabilitetit te artikullit</param>
        /// <param name="idLlogariInventari"> id llogari inventari</param>
        /// <param name="idLlogariBlerje">id llogari blerje</param>
        /// <param name="idLlogariShitje"> id llogari shitje</param>
        /// <param name="idLlogariTeTrete"> id llogari tek te tretet</param>
        /// <param name="idLlogariShpenzime"> id llogari shpenzimesh</param>
        /// <param name="idLlogariPakesim">id llogari pakesim vlere dalje</param>
        /// <param name="idLlogRez">id llogari rezervimi</param>
        /// <param name="idLlogPakRez">id llogari rezervimi pakesimje</param>
        /// <param name="minimumArtikulli">sasia min e artikullit</param>
        /// <param name="maximumArtikulli"> sasia max e artikullit</param>
        /// <param name="metodeKostojeArtikulli"> metode kostoje e artikullit</param>
        /// <param name="llogaritjaKMSHArtikulli"> llogaritja KMSH e artikullit</param>
        /// <param name="zevendesimAutomatikArtikulli"> zevendesimi automatik i artikullit</param>
        /// <param name="idPerdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idnderm"> id e ndermarjes</param>
        /// <param name="kontrollgjendje"> kontrolli mbi gjendjen e artikullit</param>
        /// <param name="kontrollcmimi"> kontrolli i cmimit te artikullit</param>
        /// <param name="kontrollgjendjeartikulli"> kontrolli i gjendjes se detajimit artikullit</param>
        /// <param name="Tvsh">tvsh</param>
        /// <param name="idkonfig">id e konfigurimit</param>
        /// <param name="aktiv">aktiv</param>
        /// <param name="sasinjesi">sasia qe krijohet gjate nje procesi</param>
        /// <param name="scrap">scrapi</param>
        /// <param name="prodhimmeprosi"> tregon nese artikulli perdoret per prodhim me porosi</param>
        /// <param name="idmag">id e magazines default qe i eshte caktuar artikullit</param>
        /// <param name="meSerial">merr nese artikulli do te jete me serial unik per cdo njesi apo per nje grup sasish te percaktuara ne nje dokument hyrjeje ose blerjeje.</param>
        /// <param name="meRezerveRivleresimi">nqs eshte e checkuar shfaqet grida gvNormaAmortizim</param>
        /// <param name="nrKaraktereTAC">merr numrin e karaktereve per sasine e karaktereve te imeit</param>
        /// <param name="llogaritKomision">tregon nese do llogaritet komisioni</param>
        /// <param name="idLlogariKomision">tregon llogarine e zgjedhur te komisionit</param>
        public clsArtikulli(int idArtikulli, string kodArtikulli, string pershkrimArtikulli, string pershkrimiAngArtikulli, string kodiDoganorArtikulli, string vendodhjeArtikulli, int kodifikimi1Artikulli, int kodifikimi2Artikulli,
            string origjineArtikulli, int njesi1Artikulli, int njesi2Artikulli, decimal koeficientArtikulli, int idFurnitoriKryesor, decimal peshaBrutoArtikulli, decimal peshaNetoArtikulli, bool detajimArtikulli, int klasa,
            int idSkemaKontabilitetiArtikulli, int idLlogariInventari, int idLlogariBlerje, int idLlogariShitje, int idLlogariTeTrete, int idLlogariShpenzime, int idLlogRez, int idLlogPakRez, decimal minimumArtikulli, decimal maximumArtikulli,
            int metodeKostojeArtikulli, int llogaritjaKMSHArtikulli, int zevendesimAutomatikArtikulli, int idPerdoruesi, int idnderm, bool kontrollgjendje, bool kontrollcmimi, bool kontrollgjendjeartikulli, int Tvsh, int idkonfig, bool aktiv,
            decimal sasinjesi, decimal scrap, bool prodhimmeprosi, int idkategoridetajimi, int idkategoridetajimi2, bool kontrollGjendjeDetajim2, int idobjektivakosto, int idllojgarancia, decimal garancia, int idmag, bool rezervueshem,
            bool pertransferim, bool loan, bool dhurate, int aplikimdhurate, decimal pike, decimal vlere, string kodvfone, bool meSerial, bool iShitshem, bool mbetjeShitshme, int idArtRaportuesi, bool perPeshore, string pershkrimFurnitori,
            string siperfaqjam2, string nrKontrate, string nrPasurie, string zonaKadastrale, string shasi, string marka, string modeli, string vitProdhimi, string tedhenateknike, bool meBarkodLogjik, string skemaBarkodit, int kodifikimi3Artikulli,
            bool aparatBazaar, string kodOferte, bool artikullIVjeter, int idformatserial, bool merezerveriv, int nrKaraktereTAC, bool llogaritKomision, int idLlogariKomision, string nrLlogariKomisioni, int stokuMaxVfOne, string kodiibarit, bool irimbursueshem)
        {
            this.idKategoriDetajimi = idkategoridetajimi;
            this.idKategoriDetajimi2 = idkategoridetajimi2;
            this.idArtikulli = idArtikulli;
            this.kodArtikulli = kodArtikulli;
            this.pershkrimArtikulli = pershkrimArtikulli;
            this.pershkrimiAngArtikulli = pershkrimiAngArtikulli;
            this.kodiDoganorArtikulli = kodiDoganorArtikulli;
            this.vendodhjeArtikulli = vendodhjeArtikulli;
            this.kodifikimi1Artikulli = kodifikimi1Artikulli;
            this.kodifikimi2Artikulli = kodifikimi2Artikulli;
            this.kodifikimi3Artikulli = kodifikimi3Artikulli;
            this.origjineArtikulli = origjineArtikulli;
            this.njesi1Artikulli = njesi1Artikulli;
            this.njesi2Artikulli = njesi2Artikulli;
            this.koeficientArtikulli = koeficientArtikulli;
            this.idFurnitoriKryesor = idFurnitoriKryesor;
            this.peshaBrutoArtikulli = peshaBrutoArtikulli;
            this.peshaNetoArtikulli = peshaNetoArtikulli;
            this.detajimArtikulli = detajimArtikulli;
            this.klasa = klasa;
            this.idSkemaKontabilitetiArtikulli = idSkemaKontabilitetiArtikulli;
            this.idLlogariInventari = idLlogariInventari;
            this.idLlogariBlerje = idLlogariBlerje;
            this.idLlogariShitje = idLlogariShitje;
            this.idLlogariTeTrete = idLlogariTeTrete;
            this.idLlogariShpenzime = idLlogariShpenzime;
            this.minimumArtikulli = minimumArtikulli;
            this.maximumArtikulli = maximumArtikulli;
            this.metodeKostojeArtikulli = metodeKostojeArtikulli;
            this.llogaritjaKMSHArtikulli = llogaritjaKMSHArtikulli;
            this.zevendesimAutomatikArtikulli = zevendesimAutomatikArtikulli;
            this.idPerdoruesi = idPerdoruesi;
            this.idNdermarje = idnderm;
            this.kontrollGjendje = kontrollgjendje;
            this.kontrollCmimPerDetajim = kontrollcmimi;
            this.kontrollGjendjeArtikulli = kontrollgjendjeartikulli;
            this.oColArtikujtZevendesues = new colArtikujtZevendesues();
            this.oColBuxhetet = new colBuxhetet();
            this.oColFurnitoreArtikujsh = new colFurnitoreArtikujsh();
            this.oColDetajime = new colDetajimePerArt();
            this.oColVleratFushaShtese = new colVleraFushaShtese();
            this.colNorma = new colAseteNormaAmortizimi();
            this.colNormaRezerva = new colNormaAmortizimiRezerva();
            this.idTvsh = Tvsh;
            this.idKonfig = idkonfig;
            this.sasiNjesi = sasinjesi;
            this.scrap = scrap;
            this.aktiv = aktiv;
            this.idObjektivaKosto = idobjektivakosto;
            prodhimMePorosi = prodhimmeprosi;
            this.kontrollGjendjeDetajim2 = kontrollGjendjeDetajim2;
            this.idllojGarancie = idllojgarancia;
            this.garancia = garancia;
            idmagazina = idmag;
            this.iRezervueshem = rezervueshem;
            this.perTransferim = pertransferim;
            this.loan = loan;
            this.dhurate = dhurate;
            this.pike = pike;
            this.vlere = vlere;
            this.aplikimDhurate = aplikimdhurate;
            this.kodVFOne = kodvfone;
            this.meSerial = meSerial;
            this.iShitshem = iShitshem;
            this.mbetjeShitshme = mbetjeShitshme;
            this.idArtRaportuesi = idArtRaportuesi;
            this.perPeshore = perPeshore;
            this.pershkrimFurnitori = pershkrimFurnitori;
            this.siperfaqjaM2 = siperfaqjam2;
            this.nrKontrate = nrKontrate;
            this.nrPasurie = nrPasurie;
            this.zonaKadastrale = zonaKadastrale;
            this.shasia = shasi;
            this.marka = marka;
            this.modeli = modeli;
            this.vitProdhimi = vitProdhimi;
            this.teDhenaTeknika = tedhenateknike;
            this.meBarkodLogjik = meBarkodLogjik;
            this.skemaBarkodit = skemaBarkodit;
            this.IdFormatSeriali = idformatserial;
            this.kodOferte = kodOferte;
            this.aparatBazaar = aparatBazaar;
            this.artikullIVjeter = artikullIVjeter;
            this.idLlogRez = idLlogRez;
            this.idLlogPakRez = idLlogPakRez;
            this.merezerverivleresimi = merezerveriv;
            this.nrKaraktereTAC = nrKaraktereTAC;
            this.llogaritKomision = llogaritKomision;
            this.idLlogariKomision = idLlogariKomision;
            this.nrLlogKomision = nrLlogariKomisioni;
            this.stokuMaxVfOne = stokuMaxVfOne;
            this.kodiiBarit = kodiibarit;
            this.iRimbursueshem = irimbursueshem;
        }

        /// <summary>
        /// lexon artikullin me id
        /// </summary>
        /// <param name="idja">id-ja artikullit qe do lexohet nga db-ja</param>
        /// <param name="dbInventari"></param>
        public clsArtikulli(int idja)
        {
            if (idja == 0 || idja == -1)
            {
                idArtikulli = 0;
                return;
            }
            using (clsDatabaseInventari dbInventari = new clsDatabaseInventari())
            {

                if (!mbushArtikull(dbInventari.merrArtikull(idja)))
                    idArtikulli = 0;
            }
        }

        public clsArtikulli(DataRow rreshti)
        {
            mbushArtikull(rreshti);
        }
        /// <summary>
        /// lexon artikullin me id
        /// </summary>
        /// <param name="idja">id-ja artikullit qe do lexohet nga db-ja</param>
        /// <param name="dbInventari"></param>
        public clsArtikulli(int idja, clsDatabaseInventari dbInventari)
        {
            if (idja == 0 || idja == -1)
            {
                idArtikulli = 0;
                return;
            }
            mbushArtikull(dbInventari.TransCache.getArtikull(idja, dbInventari));
        }

        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsArtikulli()
        {
        }
        public clsArtikulli(string kodArt, int idNdermarrje)
        {
            if (string.IsNullOrEmpty(kodArt))
                return;
            using (clsDatabaseInventari dbInventari = new clsDatabaseInventari())
            {
                mbushArtikull(dbInventari.merrArtikull(kodArt, idNdermarrje));
            }
        }
        public clsArtikulli(string kodArt, int idNdermarrje, clsDatabaseInventari dbInventari)
        {
            mbushArtikull(dbInventari.TransCache.getArtikull(kodArt, idNdermarrje, dbInventari));
            //mbushArtikull(dbInventari.merrArtikull(kodArt, idNdermarrje));
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="kodArtikulli"></param>
        /// <param name="pershkrimArtikulli"></param>
        /// <param name="pershkrimiAngArtikulli"></param>
        /// <param name="kodiDoganorArtikulli"></param>
        /// <param name="vendodhjeArtikulli"></param>
        /// <param name="kodifikimi1Artikulli"></param>
        /// <param name="kodifikimi2Artikulli"></param>
        /// <param name="origjineArtikulli"></param>
        /// <param name="njesi1Artikulli"></param>
        /// <param name="njesi2Artikulli"></param>
        /// <param name="koeficientArtikulli"></param>
        /// <param name="idFurnitoriKryesor"></param>
        /// <param name="peshaBrutoArtikulli"></param>
        /// <param name="peshaNetoArtikulli"></param>
        /// <param name="detajimArtikulli"></param>
        /// <param name="klasa"></param>
        /// <param name="idSkemaKontabilitetiArtikulli"></param>
        /// <param name="idLlogariInventari"></param>
        /// <param name="idLlogariBlerje"></param>
        /// <param name="idLlogariShitje"></param>
        /// <param name="idLlogariTeTrete"></param>
        /// <param name="idLlogariShpenzime"></param>
        /// <param name="idLlogariAmortizimi"></param>
        /// <param name="minimumArtikulli"></param>
        /// <param name="maximumArtikulli"></param>
        /// <param name="metodeKostojeArtikulli"></param>
        /// <param name="llogaritjaKMSHArtikulli"></param>
        /// <param name="zevendesimAutomatikArtikulli"></param>
        /// <param name="idPerdoruesi"></param>
        /// <param name="aktiv"></param>
        /// <param name="kontrollGjendjeArtikulli"></param>
        /// <param name="kontrollGjendje"></param>
        /// <param name="KontrollCmimi"></param>
        /// <param name="oColArtikujPerberes"></param>
        /// <param name="oColGjendjeArtSipasMag"></param>
        /// <param name="idTempKoka"></param>
        /// <param name="kodTempKoka"></param>
        /// <param name="pershkrimTempKoka"></param>
        /// <param name="oColFurnitoreArtikujsh"></param>
        /// <param name="oColArtikujtZevendesues"></param>
        /// <param name="oColVleratFushaShtese"></param>
        /// <param name="oColBuxhetet"></param>
        /// <param name="oColDetajime"></param>
        /// <param name="llojiArt"></param>
        /// <param name="sasinjesi">sasia qe krijohet gjate nje procesi</param>
        /// <param name="scrap">scrapi</param>
        /// <param name="prodhimmeporosi">tregon nese perdoret artikulli si prodhim me porosi</param>
        /// <param name="meSerial">merr nese artikulli do te jete me serial unik per cdo njesi apo per nje grup sasish te percaktuara ne nje dokument hyrjeje ose blerjeje.</param>
        /// <param name="nrKaraktereTAC">merr numrin e karaktereve per sasine e karaktereve te imeit</param>
        /// <param name="llogaritKomision">tregon nese do llogaritet komisioni</param>
        /// <param name="idLlogariKomision">tregon llogarine e zgjedhur te komisionit</param>
        public clsArtikulli(int idartikulli, string kodArtikulli, string pershkrimArtikulli, string pershkrimiAngArtikulli, string kodiDoganorArtikulli, string vendodhjeArtikulli, int kodifikimi1Artikulli, int kodifikimi2Artikulli, string kodkodifikimi1, string kodkodifikimi2, string origjineArtikulli, int njesi1Artikulli, int njesi2Artikulli, string kodnjesi1, string kodnjesi2, decimal koeficientArtikulli, int idFurnitoriKryesor, string kodfurntori, decimal peshaBrutoArtikulli, decimal peshaNetoArtikulli, bool detajimArtikulli, int klasa, int idSkemaKontabilitetiArtikulli, string pershkrimklasa, string kodiskema, int idLlogariInventari, int idLlogariBlerje, int idLlogariShitje, int idLlogariTeTrete, int idllogariPakesim, int idLlogariShpenzime, int idLlogariAmortizimi, int idLlogRez, int idLlogPakRez, string nrLlogInv, string nrLlogBlerje, string nrLlogShitje, string nrLlogPakesim, string nrLlogTeTrete, string nrLlogShpe, string nrLlogAmort, string nrLLogRez, string nrLlogPakRez, decimal minimumArtikulli, decimal maximumArtikulli, int metodeKostojeArtikulli, string pershkrimmetodekostoje, int llogaritjaKMSHArtikulli, int zevendesimAutomatikArtikulli, int idPerdoruesi, bool aktiv, bool kontrollGjendje, bool kontrollCmimi, bool kontrollGjendjeArtikulli, colArtikulliPerberes colArtikujPerberes, colGjendjeArtikulli oColGjendjeArtSipasMag, /*, int idTempKoka, string kodTempKoka, string pershkrimTempKoka*/colFurnitoreArtikujsh oColFurnitoreArtikujsh, colArtikujtZevendesues oColArtikujtZevendesues, colVleraFushaShtese oColVleratFushaShtese, colBuxhetet oColBuxhetet, colDetajimePerArt oColDetajime, bool llojiArt, int idNdermarje, int idTvsh, string kodtvsh, int idKonfgAmbienti, string autorizimet, bool shtim, decimal sasinjesi, decimal scrap, bool prodhimmeporosi, int idkategoridetajimi, string kategoridetajimi, bool kontrolloekzistence, int idkategoridetajimi2, string kategoridetajimi2, colDetajimePerArt oColDetajime2, bool kontrolloGjendjeDetajim2, int idobjektivakosto, string objektiva, int idllojgarancia, decimal garancia, int idmag, string mag, bool rezervueshem, bool pertransferim, bool loan, bool dhurate, int aplikimdhurate, decimal pike, decimal vlere, string kodvfone, bool meSerial, bool iShitshem, bool mbetjeShitshme, DbAsete.colAseteNormaAmortizimiAbstract colNorma, IDictionary<string, object> hfArkiva, System.Resources.ResourceManager rm, CultureInfo ci, colKodbare oColKodbare, int idArtRaportuesi, bool perPeshore, string pershkrimFurnitori, string siperfaqjam2, string nrKontrate, string nrPasurie, string zonaKadastrale, string shasi, string marka, string modeli, string vitProdhimi, string tedhenateknike, bool meBarkodLogjik, string skemaBarkodit, int kodifikimi3Artikulli, string kodkodifikimi3, bool aparatBazaar, string kodOferte, bool artikullIVjeter, colArtikullVfone colArtikujVfone, int idformatseriali, bool merezerveriv, DbAsete.colAseteNormaAmortizimiAbstract colNormaRezerva, int nrKaraktereTAC, bool llogaritKomision, int idLlogariKomision, string nrLlogariKomisioni, int stokuMaxVfOne, string kodiibarit, bool irimbursueshem)
        {
            try
            {
                this.idArtikulli = idartikulli;
                this.idStatusDok = 1;
                this.idNdermarje = idNdermarje;
                this.kodArtikulli = kodArtikulli;
                this.pershkrimArtikulli = pershkrimArtikulli;
                this.pershkrimiAngArtikulli = pershkrimiAngArtikulli;
                this.aktiv = aktiv;
                this.idKategoriDetajimi = idkategoridetajimi;
                this.kategoriDetajimi = kategoridetajimi;
                this.idKategoriDetajimi2 = idkategoridetajimi2;
                this.kategoriDetajimi2 = kategoridetajimi2;
                this.kodiDoganorArtikulli = kodiDoganorArtikulli;
                this.vendodhjeArtikulli = vendodhjeArtikulli;
                this.llojiArt = llojiArt;
                this.kodifikimi1Artikulli = kodifikimi1Artikulli;
                this.kodifikimi2Artikulli = kodifikimi2Artikulli;
                this.kodifikimi3Artikulli = kodifikimi3Artikulli;
                this.kodKodifikimi1 = kodkodifikimi1;
                this.kodKodifikimi2 = kodkodifikimi2;
                this.kodKodifikimi3 = kodkodifikimi3;
                this.idTvsh = idTvsh;
                this.idKonfig = idKonfgAmbienti;
                this.autorizimet = autorizimet;
                this.origjineArtikulli = origjineArtikulli;
                this.njesi1Artikulli = njesi1Artikulli;
                this.njesi2Artikulli = njesi2Artikulli;
                this.kodNjesia1 = kodnjesi1;
                this.kodNjesia2 = kodnjesi2;
                this.koeficientArtikulli = koeficientArtikulli;
                this.idFurnitoriKryesor = idFurnitoriKryesor;
                this.kodKlientFurnitori = kodfurntori;
                this.peshaBrutoArtikulli = peshaBrutoArtikulli;
                this.peshaNetoArtikulli = peshaNetoArtikulli;
                this.detajimArtikulli = detajimArtikulli;
                this.klasa = klasa;
                this.pershkrimKlasa = pershkrimklasa;
                this.kodiSkema = kodiskema;
                this.idSkemaKontabilitetiArtikulli = idSkemaKontabilitetiArtikulli;
                this.idLlogariAmortizimi = idLlogariAmortizimi;
                this.idLlogariBlerje = idLlogariBlerje;
                this.idLlogariInventari = idLlogariInventari;
                this.idLlogariShitje = idLlogariShitje;
                this.idLlogariShpenzime = idLlogariShpenzime;
                this.idLlogariTeTrete = idLlogariTeTrete;
                this.idLlogariPakesim = idllogariPakesim;
                this.nrLlogAmortizimi = nrLlogAmort;
                this.nrLlogBlerje = nrLlogBlerje;
                this.nrLlogInventari = nrLlogInv;
                this.nrLlogShitje = nrLlogShitje;
                this.nrLlogShpenzime = nrLlogShpe;
                this.nrLlogTeTrete = nrLlogTeTrete;
                this.nrLlogPakesimi = nrLlogPakesim;
                this.idLlogRez = idLlogRez;
                this.idLlogPakRez = idLlogPakRez;
                this.nrLlogRez = nrLLogRez;
                this.nrLlogPakRez = nrLlogPakRez;
                this.merezerverivleresimi = merezerveriv;
                this.minimumArtikulli = minimumArtikulli;
                this.maximumArtikulli = maximumArtikulli;
                this.llogaritjaKMSHArtikulli = llogaritjaKMSHArtikulli;
                this.zevendesimAutomatikArtikulli = zevendesimAutomatikArtikulli;
                this.IdPerdoruesi = idPerdoruesi;
                this.metodeKostojeArtikulli = metodeKostojeArtikulli;
                this.pershkrimMetodeKostoje = pershkrimmetodekostoje;
                this.kontrollGjendjeArtikulli = kontrollGjendjeArtikulli;
                this.kontrollGjendje = kontrollGjendje;
                this.kontrollCmimPerDetajim = kontrollCmimi;
                this.kodTaksa = kodtvsh;
                this.sasiNjesi = sasinjesi;
                this.scrap = scrap;
                this.colArtikujPerberes = colArtikujPerberes;
                this.oColGjendjeArtikulli = oColGjendjeArtSipasMag;
                this.oColFurnitoreArtikujsh = oColFurnitoreArtikujsh;
                this.oColArtikujtZevendesues = oColArtikujtZevendesues;
                this.oColVleratFushaShtese = oColVleratFushaShtese;
                this.oColBuxhetet = oColBuxhetet;
                this.oColDetajime = oColDetajime;
                this.OColDetajime2 = oColDetajime2;
                this.kontrollGjendjeDetajim2 = kontrolloGjendjeDetajim2;
                prodhimMePorosi = prodhimmeporosi;
                this.idObjektivaKosto = idobjektivakosto;
                this.objektiva = objektiva;
                this.idllojGarancie = idllojgarancia;
                this.garancia = garancia;
                idmagazina = idmag;
                this.iRezervueshem = rezervueshem;
                magazina = mag;
                this.loan = loan;
                this.perTransferim = pertransferim;
                this.dhurate = dhurate;
                this.pike = pike;
                this.vlere = vlere;
                this.kodVFOne = kodvfone;
                this.aplikimDhurate = aplikimdhurate;
                this.meSerial = meSerial;
                this.colNorma = colNorma;
                this.iShitshem = iShitshem;
                this.mbetjeShitshme = mbetjeShitshme;
                this.oColKodbare = oColKodbare;
                this.idArtRaportuesi = idArtRaportuesi;
                this.perPeshore = perPeshore;
                this.pershkrimFurnitori = pershkrimFurnitori;
                this.siperfaqjaM2 = siperfaqjam2;
                this.nrKontrate = nrKontrate;
                this.nrPasurie = nrPasurie;
                this.zonaKadastrale = zonaKadastrale;
                this.shasia = shasi;
                this.marka = marka;
                this.modeli = modeli;
                this.vitProdhimi = vitProdhimi;
                this.teDhenaTeknika = tedhenateknike;
                this.meBarkodLogjik = meBarkodLogjik;
                this.skemaBarkodit = skemaBarkodit;
                this.kodOferte = kodOferte;
                this.aparatBazaar = aparatBazaar;
                this.artikullIVjeter = artikullIVjeter;
                this.idFormatSeriali = idformatseriali;
                this.colArtikujVfone = colArtikujVfone;
                this.colNormaRezerva = colNormaRezerva;
                //if (hfArkiva != null)
                //    this.oArkiva = DbCore.DbShare.clsArkiva.krijoArkiva(idArtikulli, 1, 13, DateTime.Now, idPerdoruesi, hfArkiva);
                this.HfArkiva = hfArkiva;
                this.merezerverivleresimi = merezerveriv;
                this.nrKaraktereTAC = nrKaraktereTAC;
                this.llogaritKomision = llogaritKomision;
                this.idLlogariKomision = idLlogariKomision;
                this.nrLlogKomision = nrLlogariKomisioni;
                this.stokuMaxVfOne = stokuMaxVfOne;
                this.kodiiBarit = kodiibarit;
                this.iRimbursueshem = irimbursueshem;
                clsMesazh mesazh = this.kontrolloArtikull(shtim, kontrolloekzistence, rm, ci);
                if (!mesazh.Status)
                    throw new Exception(mesazh.PershkrimMesazhi);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public clsArtikulli(string kodbar, bool eshteAfatShkurter, int idNdermarrje)
        {
            using (clsDatabaseInventari db = new clsDatabaseInventari())
            {
                if (eshteAfatShkurter)
                    mbushArtikull(db.ktheArtikullSipasKodbarit(kodbar, idNdermarrje));
                else
                    throw new NotImplementedException();
            }
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// sygjeron detajimin e pare dhe te dyte te artikullit dhe kthen dhe gjendjen per detajimin e pare.
        /// </summary>
        /// <param name="idPerdorues"></param>
        /// <param name="data"></param>
        /// <param name="idMag"></param>
        /// <returns>mbush vetem: gjendjeTot;det1;det2 te struktures ListeArtikulli</returns>
        public ListeVleraArtikulli ktheVleraArtDetajime(int idPerdorues, DateTime data, int idMag, object sasiaNeGride)
        {
            return ktheVleraArtDetajime(this, idPerdorues, data, idMag, sasiaNeGride);
        }

        /// <summary>
        /// sygjeron detajimin e pare dhe te dyte te artikullit dhe kthen dhe gjendjen per detajimin e pare.
        /// </summary>
        /// <param name="idPerdorues"></param>
        /// <param name="data"></param>
        /// <param name="idMag"></param>
        /// <returns>mbush vetem: gjendjeTot;det1;det2 te struktures ListeArtikulli</returns>
        public ListeVleraArtikulliMeNjesiKodbaresh ktheVleraArtDetajimeMeNjesiKodbaresh(int idPerdorues, DateTime data, int idMag)
        {
            return ktheVleraArtDetajimeNjesiKodbare(this, idPerdorues, data, idMag);
        }

        /// <summary>
        /// sygjeron detajimin e pare dhe te dyte te artikullit dhe kthen dhe gjendjen per detajimin e pare.
        /// </summary>
        /// <param name="artikulli"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="data"></param>
        /// <param name="idMag"></param>
        /// <returns>mbush vetem: gjendjeTot;det1;det2 te struktures ListeArtikulli</returns>
        public static ListeVleraArtikulli ktheVleraArtDetajime(clsArtikulli artikulli, int idPerdorues, DateTime data, int idMag, object sasiaNeGride)
        {
            Dictionary<string, object>[] detajimDheSasi = new Dictionary<string, object>[2];
            double gjendjaTotale = clsTrupiMagazina.merrSasi(artikulli, -1, data, 0);
            detajimDheSasi = ktheArtDetajimet(artikulli, idPerdorues, data, idMag, gjendjaTotale, sasiaNeGride);
            return new ListeVleraArtikulli()
            {
                detajimiPare = detajimDheSasi[0] == null ? null : (clsDetajimArtikulli)detajimDheSasi[0]["detajim"],
                detajimiDyte = detajimDheSasi[1] == null ? null : (clsDetajimArtikulli)detajimDheSasi[1]["detajim"],
                gjendjeTot = detajimDheSasi[0] == null ? gjendjaTotale : (double)detajimDheSasi[0]["sasiTotDet"]
            };
        }

        /// <summary>
        /// sygjeron detajimin e pare dhe te dyte te artikullit dhe kthen dhe gjendjen per detajimin e pare.
        /// </summary>
        /// <param name="artikulli"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="data"></param>
        /// <param name="idMag"></param>
        /// <returns>mbush vetem: gjendjeTot;det1;det2 te struktures ListeArtikulli</returns>
        public static DbCore.ListeVleraArtikulliMeNjesiKodbaresh ktheVleraArtDetajimeNjesiKodbare(DbInventari.clsArtikulli artikulli, int idPerdorues, DateTime data, int idMag)
        {
            Dictionary<String, System.Object>[] detajimDheSasi = new Dictionary<String, System.Object>[2];
            double gjendjaTotale = DbCore.DbRegjistrim.clsTrupiMagazina.merrSasi(artikulli, -1, data, 0);
            detajimDheSasi = ktheArtDetajimet(artikulli, idPerdorues, data, idMag, gjendjaTotale);
            return new ListeVleraArtikulliMeNjesiKodbaresh()
            {
                detajimiPare = detajimDheSasi[0] == null ? null : (clsDetajimArtikulli)((Dictionary<String, System.Object>)detajimDheSasi[0])["detajim"],
                detajimiDyte = detajimDheSasi[1] == null ? null : (clsDetajimArtikulli)((Dictionary<String, System.Object>)detajimDheSasi[1])["detajim"],
                gjendjeTot = detajimDheSasi[0] == null ? gjendjaTotale : (double)((Dictionary<String, System.Object>)detajimDheSasi[0])["sasiTotDet"]
            };
        }

        public static decimal ktheKoeficent(string kodArtikulli, int idNdermarrje, clsDatabaseInventari dbInv)
        {
            return dbInv.ktheKoeficentArtikulli(kodArtikulli, idNdermarrje);
        }
        public static decimal ktheKoeficent(string kodArtikulli, int idNdermarrje)
        {
            using (clsDatabaseInventari dbInv = new clsDatabaseInventari())
            {
                return dbInv.ktheKoeficentArtikulli(kodArtikulli, idNdermarrje);
            }
        }

        public static bool ktheArtLoan(int idArtikulli)
        {
            using (clsDatabaseInventari db = new clsDatabaseInventari())
            {
                return db.ktheArtikullLoan(idArtikulli);
            }

        }
        public static DataTable ktheArtikujtMeKategoriSerialiGabim(int kategori, String artikujt)
        {

            using (clsDatabaseInventari dbInv = new clsDatabaseInventari())
            {
                return dbInv.merrArtikuj(kategori, artikujt);
            }

        }
        public static string ktheTeDhenaPerArtikullin(string kodArt)
        {
            using (clsDatabaseInventari dbInv = new clsDatabaseInventari())
            {
                return dbInv.ktheTeDhenaPerArtikullin(kodArt);
            }
        }
        public static clsMesazh kontrolloNeseArtikulliNeOwnKaTeLIdhurMagazine(colTrupiShitje coltrupishitje)
        {

            foreach (clsTrupiShitje trup in coltrupishitje)
            {
                var pergjigje = DbCore.DbInventari.clsArtikulli.ktheTeDhenaPerArtikullin(trup.Kodi);
                string[] vlerat = pergjigje.Split(',');
                if (vlerat[3] == "1" && vlerat[1] == "") //kontrolli nese artikulli eshte i rezervueshem dhe nese nuk ka te lidhur magazine ne ndermarrjen Own
                    return new clsMesazh(false, "Artikulli " + trup.Kodi + "  nuk ka te caktuar magazinen ne kartele!");
            }
            return new clsMesazh(true, "");
        }

        private static Dictionary<string, object> merrSasiPerArtPerDateSkadenceOseSeri(int lloji, int idDetajim1, int idMag, DateTime data, clsArtikulli artikulli, clsDetajimArtikulli det, string shtimModifikim, bool dokShitje, int idDok, List<Dictionary<string, string>> detSasiteList, int idKategoriDetajimi, clsDatabaseRegjistrim dbRegj)
        {
            Dictionary<string, object> sasiteArtikullit = new Dictionary<string, object>();
            double sasi = 0;
            //double sasitot = 0;
            DateTime dtSeria = new DateTime();
            double sasiaDet = 0;

            if (lloji == 2 && idDetajim1 != 0) //do behet marrja e sasise sipas detajimit te pare dhe te dyte
            {
                sasi = clsTrupiMagazina.merrSasiDetajimitDyteDheDetajimPare(artikulli, idMag, data, idDetajim1, det.IdDetajimArtikulli); //sipas magazines
                //sasitot = clsTrupiMagazina.merrSasiDetajimitDyteDheDetajimPare(artikulli, -1, data, idDetajim1, det.IdDetajimArtikulli);//per te gjitha magazinat
                if (idKategoriDetajimi == 4)
                    dtSeria = clsTrupiMagazina.merrDateSipasDetajimPareDheDyte(artikulli, idMag, idDetajim1, data, det.IdDetajimArtikulli);
            }
            else
            {
                if (idMag > 0)
                    sasi = clsTrupiMagazina.merrSasiSipasDetajimit(artikulli, idMag, data, det.IdDetajimArtikulli, lloji); //sipas magazines
                //sasitot = clsTrupiMagazina.merrSasiSipasDetajimit(artikulli, -1, data, det.IdDetajimArtikulli, lloji);//per te gjitha magazinat
                if (idKategoriDetajimi == 4)
                    dtSeria = clsTrupiMagazina.merrDatePerMag(artikulli, idMag > 0 ? idMag : -1, det.IdDetajimArtikulli, data, lloji, dbRegj);

            }

            //krijon nje objekt me vetem sasine qe ndodhet ne gride sipas detajimit te rradhes qe ka ne databaze
            Dictionary<string, string> sasiaDetObj = detSasiteList.FirstOrDefault(d => d.Values.Contains(det.KodDetajimArtikulli));

            if (sasiaDetObj != null)
            {
                sasiaDet = double.Parse(sasiaDetObj["Sasi"]); //nqs detajimi gjendet ne gride merret sasia per kete detajim
            }


            if (shtimModifikim == "modifikim")// kur eshte modifikim te marre sasine nga trupi i dokumentit
            {
                if (sasi < 0)
                    sasi = 0;
                sasi += clsTrupiMagazina.merrSasineNgaDokumenti(idDok, artikulli.IdArtikulli, det.IdDetajimArtikulli, dokShitje == false);
            }

            sasiteArtikullit.Add("sasi", sasi);
            //sasiteArtikullit.Add("sasitot", sasitot);
            sasiteArtikullit.Add("dtSeria", dtSeria);
            sasiteArtikullit.Add("sasiaDet", sasiaDet);

            return sasiteArtikullit;
        }
        private static object[] merrSasiPerArtPerDateSkadenceOseSeriDetajime(int lloji, int idDetajim1, int idMag, DateTime data, clsArtikulli artikulli, clsDetajimArtikulli det, string shtimModifikim, bool dokShitje, int idDok, List<Dictionary<string, string>> detSasiteList, int idKategoriDetajimi, clsDatabaseRegjistrim dbRegj, string detajime)
        {
            //double sasitot = 0;
            DateTime dtSeria = new DateTime();
            double sasiaDet = 0;


            var sasi = clsTrupiMagazina.merrSasiSipasDetajimeve(artikulli, idMag, data, det.IdDetajimArtikulli, lloji, detajime); //sipas magazines
                                                                                                                                  //sasitot = clsTrupiMagazina.merrSasiSipasDetajimit(artikulli, -1, data, det.IdDetajimArtikulli, lloji);//per te gjitha magazinat

            object[] sasiteArtikullit = new object[sasi.Rows.Count];
            for (var i = 0; i < sasi.Rows.Count; i++)
            {
                sasiteArtikullit[i] = new
                {
                    idDet = sasi.Rows[i].ItemArray[0].ToString(),
                    sasis = sasi.Rows[i].ItemArray[2].ToString(),
                    DtKrijimi = sasi.Rows[i].ItemArray[1].ToString()
                };
            }


            return sasiteArtikullit;
        }
        private static object[] merrSasiPerArtPerDateSkadenceOseSeriDetajime2(int lloji, int idDetajim1, int idMag, DateTime data, clsArtikulli artikulli, clsDetajimArtikulli det, string shtimModifikim, bool dokShitje, int idDok, List<Dictionary<string, string>> detSasiteList, int idKategoriDetajimi, clsDatabaseRegjistrim dbRegj, string detajime)
        {
            //double sasitot = 0;
            DateTime dtSeria = new DateTime();
            double sasiaDet = 0;


            var sasi = clsTrupiMagazina.merrSasiSipasDetajimeve2(artikulli, idMag, data, det.IdDetajimArtikulli, lloji, detajime); //sipas magazines
                                                                                                                                   //sasitot = clsTrupiMagazina.merrSasiSipasDetajimit(artikulli, -1, data, det.IdDetajimArtikulli, lloji);//per te gjitha magazinat

            object[] sasiteArtikullit = new object[sasi.Rows.Count];
            for (var i = 0; i < sasi.Rows.Count; i++)
            {
                sasiteArtikullit[i] = new
                {
                    idDet = sasi.Rows[i].ItemArray[0].ToString(),
                    sasis = sasi.Rows[i].ItemArray[2].ToString(),
                    DtKrijimi = sasi.Rows[i].ItemArray[1].ToString()
                };
            }


            return sasiteArtikullit;
        }
        public static Dictionary<string, object>[] ktheArtDetajimet(clsArtikulli artikulli, int idPerdorues, DateTime data, int idMag, double gjendjaTotale, object sasiteNeGrideObj = null, clsDatabaseInventari dbInv = null, clsDatabaseRegjistrim dbRegj = null)
        {
            if (clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerDetajime())
            {
                data = data.ToLocalTime();

                if (dbInv == null)
                    dbInv = new clsDatabaseInventari();

                if (dbRegj == null)
                    dbRegj = new clsDatabaseRegjistrim();

                Dictionary<string, object>[] detajimDheSasi = new Dictionary<string, object>[2];
                Dictionary<string, object> sasiteNeGride = new Dictionary<string, object>();
                List<Dictionary<string, string>> detSasiteList = new List<Dictionary<string, string>>();
                string shtimModifikim = string.Empty;
                bool dokShitje = false;
                int idDok = 0;
                int idDetajim1 = 0;

                if (sasiteNeGrideObj != null)//ketu behet deserializimi i objektit qe mban saste ne gride per artikujt me detajim date skadence
                {
                    sasiteNeGride = JsonConvert.DeserializeObject<Dictionary<string, object>>(sasiteNeGrideObj.ToString());
                    if (sasiteNeGride.Count > 0)
                    {
                        detSasiteList = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(sasiteNeGride["detSasite"].ToString());
                        object value;
                        if (sasiteNeGride.TryGetValue("shtimModifikim", out value) && value.ToString() == "modifikim")
                        {
                            shtimModifikim = value.ToString();
                            dokShitje = Convert.ToBoolean(sasiteNeGride["dokShitje"]);
                            idDok = Convert.ToInt32(sasiteNeGride["idDok"]);
                        }
                    }
                }

                for (int i = 0; i < 2; i++) // 2 -> detajim i pare dhe i dyte
                {
                    int idKategoriDetajimi = i == 0 ? artikulli.IdKategoriDetajimi : artikulli.IdKategoriDetajimi2;
                    int lloji = i + 1;
                    switch (idKategoriDetajimi)
                    {
                        case 3:
                        case 4:

                            double sasi = 0; double sasitot = 0;
                            DateTime dtMeHershme = new DateTime(9999, 12, 30); //dt maks
                            colDetajimeArtikulli colDet = new colDetajimeArtikulli();
                            colDet.mbushDetajimeSipasArtikullitNdermarrjesDheLlojit(artikulli.KodArtikulli, artikulli.IdNdermarje, idPerdorues, lloji, dbInv);
                            DateTime dtSeria = new DateTime(); //per serite
                            string detajime = "";
                            foreach (clsDetajimArtikulli det in colDet)
                            {
                                detajime = detajime + det.IdDetajimArtikulli + ",";
                            }
                            var sasiteArtikullitTest = new object[0];
                            if (i == 0)
                                sasiteArtikullitTest = merrSasiPerArtPerDateSkadenceOseSeriDetajime(lloji, idDetajim1, idMag, data, artikulli, new clsDetajimArtikulli(), shtimModifikim, dokShitje, idDok, detSasiteList, idKategoriDetajimi, dbRegj, detajime); //kthen nje objekt me sasite per kete artikull
                            else
                                sasiteArtikullitTest = merrSasiPerArtPerDateSkadenceOseSeriDetajime2(lloji, idDetajim1, idMag, data, artikulli, new clsDetajimArtikulli(idDetajim1), shtimModifikim, dokShitje, idDok, detSasiteList, idKategoriDetajimi, dbRegj, detajime); //kthen nje objekt me sasite per kete artikull



                            foreach (clsDetajimArtikulli det in colDet)
                            {
                                if (artikulli.Klasa == 4)
                                {
                                    detajimDheSasi[i] = new Dictionary<string, object>
                                {
                                    { "sasiTotDet", 0 },
                                    { "detajim", null }
                                };
                                    if (lloji == 1)
                                        idDetajim1 = 0;
                                    continue;
                                }
                                if (idKategoriDetajimi == 3)
                                {
                                    DateTime dtSkadence;
                                    bool dateVlefshme = DateTime.TryParseExact(det.KodDetajimArtikulli, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtSkadence);
                                    if (!dateVlefshme)
                                    {
                                        detajimDheSasi[i] = new Dictionary<string, object>
                                    {
                                        { "sasiTotDet", clsTrupiMagazina.merrSasiSipasDetajimit(artikulli, -1, data, det.IdDetajimArtikulli, lloji) },
                                        { "detajim", null }
                                    };
                                        if (lloji == 1)
                                            idDetajim1 = 0;
                                        continue;
                                    }
                                    double sasiaDet = 0;
                                    //Dictionary<string, object> sasiteArtikullit = merrSasiPerArtPerDateSkadenceOseSeri(lloji, idDetajim1, idMag, data, artikulli, det, shtimModifikim, dokShitje, idDok, detSasiteList, idKategoriDetajimi, dbRegj); //kthen nje objekt me sasite per kete artikull
                                    var sasiTest = sasiteArtikullitTest.Where(x => x.GetType().GetProperty("idDet").GetValue(x, null).ToString() == det.IdDetajimArtikulli.ToString());
                                    foreach (var value in sasiTest)
                                    {
                                        sasi = Double.Parse(value.GetType().GetProperty("sasis").GetValue(value, null).ToString());
                                        //dtSeria = DateTime.Parse(value.GetType().GetProperty("DtKrijimi").GetValue(value, null).ToString());
                                    }

                                    //sasitot = (double)sasiteArtikullit["sasitot"];
                                    Dictionary<string, string> sasiaDetObj = detSasiteList.FirstOrDefault(d => d.Values.Contains(det.KodDetajimArtikulli));
                                    if (sasiaDetObj != null)
                                        sasiaDet = double.Parse(sasiaDetObj["Sasi"]);
                                    else
                                        sasiaDet = 0; // sasia ne gride e art per kete detajim

                                    if (sasi > 0 && sasiaDet < sasi)
                                    {

                                        if (dateVlefshme && dtSkadence < dtMeHershme)
                                        {

                                            dtMeHershme = dtSkadence;
                                            detajimDheSasi[i] = new Dictionary<string, object>
                                            {
                                                { "sasiTotDet", sasi },
                                                { "detajim", det }
                                            };
                                            if (lloji == 1)
                                                idDetajim1 = det.IdDetajimArtikulli;


                                        }


                                    }
                                    sasi = 0;
                                    continue;
                                }
                                if (idKategoriDetajimi == 4)
                                {
                                    double sasiaDet = 0;
                                    //Dictionary<string, object> sasiteArtikullit = merrSasiPerArtPerDateSkadenceOseSeri(lloji, idDetajim1, idMag, data, artikulli, det, shtimModifikim, dokShitje, idDok, detSasiteList, idKategoriDetajimi, dbRegj); //kthen nje objekt me sasite per kete artikull
                                    var sasiTest = sasiteArtikullitTest.Where(x => x.GetType().GetProperty("idDet").GetValue(x, null).ToString() == det.IdDetajimArtikulli.ToString());
                                    foreach (var value in sasiTest)
                                    {
                                        sasi = Double.Parse(value.GetType().GetProperty("sasis").GetValue(value, null).ToString());
                                        dtSeria = DateTime.Parse(value.GetType().GetProperty("DtKrijimi").GetValue(value, null).ToString());
                                    }
                                    //sasitot = (double)sasiteArtikullit["sasitot"];
                                    Dictionary<string, string> sasiaDetObj = detSasiteList.FirstOrDefault(d => d.Values.Contains(det.KodDetajimArtikulli));
                                    if (sasiaDetObj != null)
                                        sasiaDet = double.Parse(sasiaDetObj["Sasi"]);

                                    if (sasi > 0 && sasiaDet < sasi)
                                    {
                                        if (dtSeria.ToString() != "01/01/1900 00:00:00")
                                        {
                                            if (dtSeria < dtMeHershme)
                                            {
                                                dtMeHershme = dtSeria;
                                                detajimDheSasi[i] = new Dictionary<string, object>
                                        {
                                            { "sasiTotDet", sasi },
                                            { "detajim", det }
                                        };
                                                if (lloji == 1)
                                                    idDetajim1 = det.IdDetajimArtikulli;
                                            }
                                        }

                                    }
                                    sasi = 0;
                                    continue;
                                }
                            }
                            break;
                        case 1:
                        case 2:
                            detajimDheSasi[i] = new Dictionary<string, object>
                        {
                            { "sasiTotDet", clsTrupiMagazina.merrSasiSipasDetajimit(artikulli, -1, data, 0, lloji) },
                            { "detajim", null }
                        };
                            if (lloji == 1)
                                idDetajim1 = 0;
                            break;
                        default:
                            detajimDheSasi[i] = new Dictionary<string, object>
                        {
                            { "sasiTotDet", gjendjaTotale },
                            { "detajim", null }
                        };
                            if (lloji == 1)
                                idDetajim1 = 0;
                            break;
                    }
                }
                return detajimDheSasi;
            }
            else
            {

                data = data.ToLocalTime();

                if (dbInv == null)
                    dbInv = new clsDatabaseInventari();

                if (dbRegj == null)
                    dbRegj = new clsDatabaseRegjistrim();

                Dictionary<string, object>[] detajimDheSasi = new Dictionary<string, object>[2];
                Dictionary<string, object> sasiteNeGride = new Dictionary<string, object>();
                List<Dictionary<string, string>> detSasiteList = new List<Dictionary<string, string>>();
                string shtimModifikim = string.Empty;
                bool dokShitje = false;
                int idDok = 0;
                int idDetajim1 = 0;

                if (sasiteNeGrideObj != null)//ketu behet deserializimi i objektit qe mban saste ne gride per artikujt me detajim date skadence
                {
                    sasiteNeGride = JsonConvert.DeserializeObject<Dictionary<string, object>>(sasiteNeGrideObj.ToString());
                    if (sasiteNeGride.Count > 0)
                    {
                        detSasiteList = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(sasiteNeGride["detSasite"].ToString());
                        object value;
                        if (sasiteNeGride.TryGetValue("shtimModifikim", out value) && value.ToString() == "modifikim")
                        {
                            shtimModifikim = value.ToString();
                            dokShitje = Convert.ToBoolean(sasiteNeGride["dokShitje"]);
                            idDok = Convert.ToInt32(sasiteNeGride["idDok"]);
                        }
                    }
                }

                for (int i = 0; i < 2; i++) // 2 -> detajim i pare dhe i dyte
                {
                    int idKategoriDetajimi = i == 0 ? artikulli.IdKategoriDetajimi : artikulli.IdKategoriDetajimi2;
                    int lloji = i + 1;
                    switch (idKategoriDetajimi)
                    {
                        case 3:
                        case 4:

                            double sasi = 0; double sasitot = 0;
                            DateTime dtMeHershme = new DateTime(9999, 12, 30); //dt maks
                            colDetajimeArtikulli colDet = new colDetajimeArtikulli();
                            colDet.mbushDetajimeSipasArtikullitNdermarrjesDheLlojit(artikulli.KodArtikulli, artikulli.IdNdermarje, idPerdorues, lloji, dbInv);
                            DateTime dtSeria = new DateTime(); //per serite
                            foreach (clsDetajimArtikulli det in colDet)
                            {
                                if (artikulli.Klasa == 4)
                                {
                                    detajimDheSasi[i] = new Dictionary<string, object>
                                {
                                    { "sasiTotDet", 0 },
                                    { "detajim", null }
                                };
                                    if (lloji == 1)
                                        idDetajim1 = 0;
                                    continue;
                                }
                                if (idKategoriDetajimi == 3)
                                {
                                    DateTime dtSkadence;
                                    bool dateVlefshme = DateTime.TryParseExact(det.KodDetajimArtikulli, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtSkadence);
                                    if (!dateVlefshme)
                                    {
                                        detajimDheSasi[i] = new Dictionary<string, object>
                                    {
                                        { "sasiTotDet", clsTrupiMagazina.merrSasiSipasDetajimit(artikulli, -1, data, det.IdDetajimArtikulli, lloji) },
                                        { "detajim", null }
                                    };
                                        if (lloji == 1)
                                            idDetajim1 = 0;
                                        continue;
                                    }
                                    double sasiaDet = 0;
                                    Dictionary<string, object> sasiteArtikullit = merrSasiPerArtPerDateSkadenceOseSeri(lloji, idDetajim1, idMag, data, artikulli, det, shtimModifikim, dokShitje, idDok, detSasiteList, idKategoriDetajimi, dbRegj); //kthen nje objekt me sasite per kete artikull

                                    sasi = (double)sasiteArtikullit["sasi"];
                                    //sasitot = (double)sasiteArtikullit["sasitot"];
                                    sasiaDet = (double)sasiteArtikullit["sasiaDet"]; // sasia ne gride e art per kete detajim

                                    if (sasi > 0 && sasiaDet < sasi)
                                    {
                                        if (dateVlefshme && dtSkadence < dtMeHershme)
                                        {
                                            dtMeHershme = dtSkadence;
                                            detajimDheSasi[i] = new Dictionary<string, object>
                                        {
                                            { "sasiTotDet", sasi },
                                            { "detajim", det }
                                        };
                                            if (lloji == 1)
                                                idDetajim1 = det.IdDetajimArtikulli;
                                        }
                                    }
                                    continue;
                                }
                                if (idKategoriDetajimi == 4)
                                {
                                    double sasiaDet = 0;
                                    Dictionary<string, object> sasiteArtikullit = merrSasiPerArtPerDateSkadenceOseSeri(lloji, idDetajim1, idMag, data, artikulli, det, shtimModifikim, dokShitje, idDok, detSasiteList, idKategoriDetajimi, dbRegj);
                                    dtSeria = Convert.ToDateTime(sasiteArtikullit["dtSeria"]);
                                    sasi = (double)sasiteArtikullit["sasi"];
                                    //sasitot = (double)sasiteArtikullit["sasitot"];
                                    sasiaDet = (double)sasiteArtikullit["sasiaDet"];

                                    if (sasi > 0 && sasiaDet < sasi)
                                    {
                                        if (dtSeria < dtMeHershme)
                                        {
                                            dtMeHershme = dtSeria;
                                            detajimDheSasi[i] = new Dictionary<string, object>
                                        {
                                            { "sasiTotDet", sasi },
                                            { "detajim", det }
                                        };
                                            if (lloji == 1)
                                                idDetajim1 = det.IdDetajimArtikulli;
                                        }
                                    }
                                    continue;
                                }
                            }
                            break;
                        case 1:
                        case 2:
                            detajimDheSasi[i] = new Dictionary<string, object>
                        {
                            { "sasiTotDet", clsTrupiMagazina.merrSasiSipasDetajimit(artikulli, -1, data, 0, lloji) },
                            { "detajim", null }
                        };
                            if (lloji == 1)
                                idDetajim1 = 0;
                            break;
                        default:
                            detajimDheSasi[i] = new Dictionary<string, object>
                        {
                            { "sasiTotDet", gjendjaTotale },
                            { "detajim", null }
                        };
                            if (lloji == 1)
                                idDetajim1 = 0;
                            break;
                    }
                }
                return detajimDheSasi;
            }

        }

        /// <summary>
        /// kthen tvsh-et e artikullit
        /// </summary>
        /// <param name="llojtvsh"></param>
        /// <param name="idPerdoruesi"></param>
        /// <returns></returns>
        public ComboListTvsh[] ktheTvsh(KonfigurimTVSHGjateRregj llojtvsh, int idPerdoruesi, clsTaksa takseDefaulNdermarrje, colTaksa taksaNdermarrje, clsTaksa taksaKF)
        {
            var listeTvsh = new List<ComboListTvsh>(taksaNdermarrje.Count + 1);

            var tvshSygjeruar = MerrTvshTeSugjeruar(llojtvsh, takseDefaulNdermarrje, taksaKF, taksaNdermarrje);
            //nese ka tvsh te sugjeruar e shtojem te paren ne liste
            if (tvshSygjeruar.value != 0)
                listeTvsh.Add(tvshSygjeruar);

            var tvshPaTakse = clsTaksa.MerrComboItemTaksa(clsTaksa.krijoTaksePaTVSH(), "asnje");
            //ne raste se nuk ka tvsh te sugjeruar i bie qe e para do dali ne liste 'PaTVSH'
            listeTvsh.Add(tvshPaTakse);
            //nese ndermarrja nuk ka TVSH
            if (taksaNdermarrje.Count == 0)
                return listeTvsh.ToArray();

            listeTvsh.AddRange(taksaNdermarrje.Where(tvsh => tvsh.IdTaksa != tvshSygjeruar.value).Select(cmbItem => clsTaksa.MerrComboItemTaksa(cmbItem, "asnje")));

            return listeTvsh.ToArray();
        }
        private ComboListTvsh MerrTvshSipasArtikullit(colTaksa colTaksa)
        {
            if (IdArtikulli > 0 && IdTvsh > 0)
            {
                clsTaksa taksa = colTaksa.Where(x => x.IdTaksa == idTvsh).FirstOrDefault();
                //var taksa = new clsTaksa(IdTvsh);
                if (taksa.Aktiv == true)
                    return clsTaksa.MerrComboItemTaksa(taksa, "art");
            }
            return new ComboListTvsh();
        }
        private ComboListTvsh MerrTvshTeSugjeruar(KonfigurimTVSHGjateRregj llojtvsh, clsTaksa takseDefaulNdermarrje, clsTaksa taksaKF, colTaksa colTaksa)
        {
            var tvshSygjeruar = new ComboListTvsh();
            if (llojtvsh == KonfigurimTVSHGjateRregj.Sipas_Klientit)
            {
                //nese lloji i tvsh se konfiguruar eshte sipas klientit ath si tvsh e sugjeruar do te merret tvsh e klientit
                if (taksaKF.IdTaksa > 0 && taksaKF.Aktiv == true)
                    return clsTaksa.MerrComboItemTaksa(taksaKF, "KF");
                tvshSygjeruar = MerrTvshSipasArtikullit(colTaksa);
                if (tvshSygjeruar.value != 0) return tvshSygjeruar;
            }
            if (llojtvsh == KonfigurimTVSHGjateRregj.Sipas_Artikullit)
            {
                //nese tvsh e klientit nuk ka qene aktive ose lloji eshte tvsh sipas artikullit
                tvshSygjeruar = MerrTvshSipasArtikullit(colTaksa);
                if (tvshSygjeruar.value != 0) return tvshSygjeruar;
            }
            if (llojtvsh != KonfigurimTVSHGjateRregj.Pa_TVSH)
                return clsTaksa.MerrComboItemTaksa(takseDefaulNdermarrje, "nderm");

            //else bosh
            return tvshSygjeruar;
        }

        /// <summary>
        /// Kryen rivleresimin mesatar per nje artikull te dhene ne metoden perkatese ne nje magazine per nje periudhe te caktuar
        /// </summary>
        /// <param name="idArt"></param>
        /// <param name="metodeKostoje"></param>
        /// <param name="idMagazina"></param>
        /// <param name="dataNga"></param>
        /// <param name="dataDeri"></param>
        /// <returns></returns>
        public static clsMesazh rivleresimCmimiMesatar(int idArt, int metodeKostoje, int idMagazina, DateTime dataNga, DateTime dataDeri, DbAdmin.clsLogRivleresimInventari log, CultureInfo ci, ResourceManager rm, int idndermarje, int idperdoruesi)
        {
            bool kontrollCmimPerDetajim = false;
            return rivleresimCmimiMesatar(idArt, metodeKostoje, idMagazina, dataNga, dataDeri, kontrollCmimPerDetajim, log, ci, rm, idndermarje, idperdoruesi);
        }
        /// <summary>
        /// Kryen rivleresimin mesatar per nje artikull te dhene ne metoden perkatese ne nje magazine per nje periudhe te caktuar
        /// </summary>
        /// <param name="idArt"></param>
        /// <param name="metodeKostoje"></param>
        /// <param name="idMagazina"></param>
        /// <param name="dataNga"></param>
        /// <param name="dataDeri"></param>
        /// <param name="kontrollCmimPerDetajim"></param>
        /// <returns></returns>
        public static clsMesazh rivleresimCmimiMesatar(int idArt, int metodeKostoje, int idMagazina, DateTime dataNga, DateTime dataDeri, bool kontrollCmimPerDetajim, clsLogRivleresimInventari log, CultureInfo ci, ResourceManager rm, int idndermarje, int idperdoruesi)
        {
            if (MbylljePeriudhe.PeriodClosing.IsPeriodClosed(dataNga.Date, IMBUtils.DataBase.MyConnectionsManager.GetSelectedConNameServer(), idndermarje, KategoriDokumenti.RivleresimInventari, 0))
            {
                return new MesazhGabimi(MessagesResource.Messages["msgPeriodIsClosed"]);
            }
            double nrDitesh = (dataDeri.Date - dataNga.Date).TotalDays + 1;
            DateTime data = dataNga;
            clsMesazh mesazh = new clsMesazh();
            for (int m = 0; m < nrDitesh; m++)
            {
                mesazh = clsTrupiMagazina.bejRivleresim(kontrollCmimPerDetajim, idArt, idMagazina, metodeKostoje, data, log, ci, rm, idndermarje, idperdoruesi);
                if (!mesazh.Status)
                    return mesazh;
                data = data.AddDays(1);
            }
            return mesazh;
        }


        public clsArtikulli krijoArtikullPerImport(clsArtikulli artEkzistues, string kodArtikulli, string pershkrimArtikulli, string pershkrimiAngArtikulli, string kodiDoganorArtikulli, string vendodhjeArtikulli,
            string kodkodifikimi1, string kodkodifikimi2, string origjineArtikulli, string kodnjesi1, string kodnjesi2, decimal? koeficientArtikulli, string kodfurntori, decimal? peshaBrutoArtikulli,
            decimal? peshaNetoArtikulli, bool? detajimArtikulli, string pershkrimklasa, string kodiskema, string nrLlogInv, string nrLlogBlerje, string nrLlogShitje, string nrLlogTeTrete, string nrLlogShpe,
            string nrLlogAmort, string nrLlogPak, string nrLlogRez, string nrLlogPakRez, decimal? minimumArtikulli, decimal? maximumArtikulli, string pershkrimmetodekostoje, int? llogaritjaKMSHArtikulli,
            int? zevendesimAutomatikArtikulli, int idPerdoruesi, bool? aktiv, bool? kontrollGjendje, bool? kontrollCmimi, bool? kontrollGjendjeArtikulli, colArtikulliPerberes colArtikujPerberes,
            colFurnitoreArtikujsh oColFurnitoreArtikujsh, colArtikujtZevendesues oColArtikujtZevendesues, colVleraFushaShtese oColVleratFushaShtese, colBuxhetet oColBuxhetet, string detajim1, bool? llojiArt,
            int idNdermarje, string Tvsh, int? idKonfgAmbienti, string autorizimet, bool shtim, decimal? sasinjesi, decimal? scrap, bool? prodhimmeporosi, string kategoridetajimi, bool kontrolloekzistence,
            string kategoridetajimi2, bool? kontrollGjendjeDetajim2, string objektiva, string detajim2, string magazina, bool? rezervueshem, string llojgarancie, decimal? garancia, bool? iShitshem, bool? meserial,
            bool? mbetjeshitshme, ResourceManager rm, CultureInfo ci, colKodbare oColKodbare, bool? perPeshore, string pershkrimFurnitori, string siperfaqjam2, string nrKontrate, string nrPasurie, string zonaKadastrale,
            string shasi, string marka, string modeli, string vitProdhimi, string tedhenateknike, bool? meBarkodLogjik, string skemaBarkodit, string kodkodifikimi3, bool? artikullIVjeter, colArtikullVfone colArtikujVfone,
            string formatSeriali, bool? merezerveriv, int? KaraktereTAC, bool? llogaritKomision, string nrLlogariKomisioni, int? stokuMaxVfOne, string kodiibarit, bool? irimbursueshem)
        {
            try
            {
                clsDatabaseRegjistrim dbRegjistrim = new clsDatabaseRegjistrim();
                artEkzistues.EmptyObject();
                this.LlojiArt = llojiArt == null ? artEkzistues.LlojiArt : (bool)llojiArt;
                this.IdKonfig = idKonfgAmbienti == null ? artEkzistues.IdKonfig : (int)idKonfgAmbienti;

                bool kaVeprimeArt = artEkzistues.IdArtikulli > 0 ? dbRegjistrim.eshteDokumentiILidhurCelje(artEkzistues.IdArtikulli.ToString(), clsKonfigurimAmbjenti.ktheIdNiveliSipasIdKonfigurimi(IdKonfig).ToString()) : false;

                this.IdArtikulli = artEkzistues.IdArtikulli > 0 ? artEkzistues.IdArtikulli : 0;
                clsKlasaArtikulli kl = string.IsNullOrEmpty(pershkrimklasa) ? new clsKlasaArtikulli() : new clsKlasaArtikulli(pershkrimklasa);
                this.Klasa = string.IsNullOrEmpty(pershkrimklasa) ? artEkzistues.Klasa : kl.IdKlasa;
                if (kaVeprimeArt && Klasa != artEkzistues.Klasa)
                    throw new Exception($"Nuk mund te ndryshoni klasen, pasi artikulli: {artEkzistues.KodArtikulli} eshte i lidhur");
                this.PershkrimKlasa = string.IsNullOrEmpty(pershkrimklasa) ? artEkzistues.PershkrimKlasa : pershkrimklasa;

                clsMetodeKostoje metode = new clsMetodeKostoje();
                if (!string.IsNullOrEmpty(pershkrimmetodekostoje))
                    metode.ktheMetodeKostojeSipasKodit(pershkrimmetodekostoje);
                else
                    metode.ktheMetodeKostojeSipasId(artEkzistues.MetodeKostojeArtikulli);
                this.MetodeKostojeArtikulli = string.IsNullOrEmpty(pershkrimmetodekostoje) ? artEkzistues.MetodeKostojeArtikulli : metode.IdMetodeKostoje;
                if (kaVeprimeArt && MetodeKostojeArtikulli != artEkzistues.MetodeKostojeArtikulli)
                    throw new Exception($"Nuk mund te ndryshoni metoden e kostos, pasi artikulli: {artEkzistues.KodArtikulli} eshte i lidhur");

                this.PershkrimMetodeKostoje = string.IsNullOrEmpty(pershkrimmetodekostoje) ? metode.Kodi ?? string.Empty : pershkrimmetodekostoje;

                clsKodifikimArtikulli kod1 = string.IsNullOrEmpty(kodkodifikimi1) ? new clsKodifikimArtikulli() : new clsKodifikimArtikulli(kodkodifikimi1, idNdermarje, 1, LlojiArt);
                this.Kodifikimi1Artikulli = string.IsNullOrEmpty(kodkodifikimi1) ? artEkzistues.Kodifikimi1Artikulli : kod1.IdKodifikimi;
                this.KodKodifikimi1 = string.IsNullOrEmpty(kodkodifikimi1) ? artEkzistues.KodKodifikimi1 : kodkodifikimi1;

                clsKodifikimArtikulli kod2 = string.IsNullOrEmpty(kodkodifikimi2) ? new clsKodifikimArtikulli() : new clsKodifikimArtikulli(kodkodifikimi2, idNdermarje, 2, LlojiArt);
                this.Kodifikimi2Artikulli = string.IsNullOrEmpty(kodkodifikimi2) ? artEkzistues.Kodifikimi2Artikulli : kod2.IdKodifikimi;
                this.KodKodifikimi2 = string.IsNullOrEmpty(kodkodifikimi2) ? artEkzistues.KodKodifikimi2 : kodkodifikimi2;

                clsKodifikimArtikulli kod3 = string.IsNullOrEmpty(kodkodifikimi3) ? new clsKodifikimArtikulli() : new clsKodifikimArtikulli(kodkodifikimi3, idNdermarje, 3, LlojiArt);
                this.Kodifikimi3Artikulli = string.IsNullOrEmpty(kodkodifikimi3) ? artEkzistues.Kodifikimi3Artikulli : kod3.IdKodifikimi;
                this.KodKodifikimi3 = string.IsNullOrEmpty(kodkodifikimi3) ? artEkzistues.KodKodifikimi3 : kodkodifikimi3;


                clsNjesiArtikulli njesi = new clsNjesiArtikulli();
                if (!string.IsNullOrEmpty(kodnjesi1))
                    njesi.mbushNjesiArtikulliMeKod(kodnjesi1, idNdermarje);
                this.Njesi1Artikulli = string.IsNullOrEmpty(kodnjesi1) ? artEkzistues.Njesi1Artikulli : njesi.IdNjesia;
                if (kaVeprimeArt && this.Njesi1Artikulli != artEkzistues.Njesi1Artikulli)
                    throw new Exception($"Nuk mund te ndryshoni njesite matese te artikullit, pasi artikulli: {artEkzistues.KodArtikulli} eshte i lidhur");

                clsNjesiArtikulli njesi2 = new clsNjesiArtikulli();
                if (!string.IsNullOrEmpty(kodnjesi2))
                    njesi2.mbushNjesiArtikulliMeKod(kodnjesi2, idNdermarje);
                this.Njesi2Artikulli = string.IsNullOrEmpty(kodnjesi2) ? artEkzistues.Njesi2Artikulli : njesi2.IdNjesia;
                if (kaVeprimeArt && this.Njesi2Artikulli != artEkzistues.Njesi2Artikulli)
                    throw new Exception($"Nuk mund te ndryshoni njesite matese te artikullit, pasi artikulli: {artEkzistues.KodArtikulli} eshte i lidhur");

                clsKlientFurnitor kf = new clsKlientFurnitor();
                if (!string.IsNullOrEmpty(kodfurntori))
                    kf.mbushKlientFurnitorSipasKodit(kodfurntori, idNdermarje);
                this.IdFurnitoriKryesor = string.IsNullOrEmpty(kodfurntori) ? artEkzistues.IdFurnitoriKryesor : kf.IdKlientFurnitor;
                this.KodFurnitori = string.IsNullOrEmpty(kodfurntori) ? artEkzistues.KodFurnitori : kodfurntori;

                this.IdSkemaKontabilitetiArtikulli = string.IsNullOrEmpty(kodiskema) ? artEkzistues.IdSkemaKontabilitetiArtikulli : clsSkemaKontabilitetiArtikulli.ktheIdSkemaKontabilitetArtikulli(kodiskema, idNdermarje, LlojiArt);
                this.KodSkema = string.IsNullOrEmpty(kodiskema) ? artEkzistues.KodSkema : kodiskema;
                if (kaVeprimeArt && this.IdSkemaKontabilitetiArtikulli != artEkzistues.IdSkemaKontabilitetiArtikulli)
                    throw new Exception($"Nuk mund te ndryshoni skemen, pasi artikulli: {artEkzistues.KodArtikulli} eshte i lidhur");

                this.IdLlogariInventari = string.IsNullOrEmpty(nrLlogInv) ? artEkzistues.IdLlogariInventari : clsLlogari.mbushIDLlogariSipasKodit(nrLlogInv, idNdermarje);
                this.NrLlogInventari = string.IsNullOrEmpty(nrLlogInv) ? artEkzistues.NrLlogInventari : nrLlogInv;
                if (kaVeprimeArt && IdLlogariInventari != artEkzistues.IdLlogariInventari)
                    throw new Exception($"Nuk mund te ndryshoni llogarine e inventarit, pasi artikulli: {artEkzistues.KodArtikulli} eshte i lidhur");
                this.IdLlogariBlerje = string.IsNullOrEmpty(nrLlogBlerje) ? artEkzistues.IdLlogariBlerje : clsLlogari.mbushIDLlogariSipasKodit(nrLlogBlerje, idNdermarje);
                this.NrLlogBlerje = string.IsNullOrEmpty(nrLlogBlerje) ? artEkzistues.NrLlogBlerje : nrLlogBlerje;
                if (kaVeprimeArt && IdLlogariBlerje != artEkzistues.IdLlogariBlerje)
                    throw new Exception($"Nuk mund te ndryshoni llogarine e blerjes, pasi artikulli: {artEkzistues.KodArtikulli} eshte i lidhur");
                this.IdLlogariShitje = string.IsNullOrEmpty(nrLlogShitje) ? artEkzistues.IdLlogariShitje : clsLlogari.mbushIDLlogariSipasKodit(nrLlogShitje, idNdermarje);
                this.NrLlogShitje = string.IsNullOrEmpty(nrLlogShitje) ? artEkzistues.NrLlogShitje : nrLlogShitje;
                if (kaVeprimeArt && IdLlogariShitje != artEkzistues.IdLlogariShitje)
                    throw new Exception($"Nuk mund te ndryshoni llogarine e shitjes, pasi artikulli: {artEkzistues.KodArtikulli} eshte i lidhur");
                this.IdLlogariTeTrete = string.IsNullOrEmpty(nrLlogTeTrete) ? artEkzistues.IdLlogariTeTrete : clsLlogari.mbushIDLlogariSipasKodit(nrLlogTeTrete, idNdermarje);
                this.NrLlogTeTrete = string.IsNullOrEmpty(nrLlogTeTrete) ? artEkzistues.NrLlogTeTrete : nrLlogTeTrete;
                if (kaVeprimeArt && IdLlogariTeTrete != artEkzistues.IdLlogariTeTrete)
                    throw new Exception($"Nuk mund te ndryshoni llogarine me te trete, pasi artikulli: {artEkzistues.KodArtikulli} eshte i lidhur");
                this.IdLlogariShpenzime = string.IsNullOrEmpty(nrLlogShpe) ? artEkzistues.IdLlogariShpenzime : clsLlogari.mbushIDLlogariSipasKodit(nrLlogShpe, idNdermarje);
                this.NrLlogShpenzime = string.IsNullOrEmpty(nrLlogShpe) ? artEkzistues.NrLlogShpenzime : nrLlogShpe;
                if (kaVeprimeArt && IdLlogariShpenzime != artEkzistues.IdLlogariShpenzime)
                    throw new Exception($"Nuk mund te ndryshoni llogarine e shpenzimit, pasi artikulli: {artEkzistues.KodArtikulli} eshte i lidhur");
                this.IdLlogariAmortizimi = string.IsNullOrEmpty(nrLlogAmort) ? artEkzistues.IdLlogariAmortizimi : clsLlogari.mbushIDLlogariSipasKodit(nrLlogAmort, idNdermarje);
                this.NrLlogAmortizimi = string.IsNullOrEmpty(nrLlogAmort) ? artEkzistues.NrLlogAmortizimi : nrLlogAmort;
                if (kaVeprimeArt && IdLlogariAmortizimi != artEkzistues.IdLlogariAmortizimi)
                    throw new Exception($"Nuk mund te ndryshoni llogarine e amortizimit, pasi artikulli: {artEkzistues.KodArtikulli} eshte i lidhur");
                this.IdLlogariPakesim = string.IsNullOrEmpty(nrLlogPak) ? artEkzistues.IdLlogariPakesim : clsLlogari.mbushIDLlogariSipasKodit(nrLlogPak, idNdermarje);
                this.NrLlogPakesimi = string.IsNullOrEmpty(nrLlogPak) ? artEkzistues.NrLlogPakesimi : nrLlogPak;
                if (kaVeprimeArt && IdLlogariPakesim != artEkzistues.IdLlogariPakesim)
                    throw new Exception($"Nuk mund te ndryshoni llogarine e pakesimit, pasi artikulli: {artEkzistues.KodArtikulli} eshte i lidhur");
                this.IdLlogRez = string.IsNullOrEmpty(nrLlogRez) ? artEkzistues.IdLlogRez : clsLlogari.mbushIDLlogariSipasKodit(nrLlogRez, idNdermarje);
                this.NrLlogRez = string.IsNullOrEmpty(nrLlogRez) ? artEkzistues.NrLlogRez : nrLlogRez;
                if (kaVeprimeArt && IdLlogRez != artEkzistues.IdLlogRez)
                    throw new Exception($"Nuk mund te ndryshoni llogarine rezerve, pasi artikulli: {artEkzistues.KodArtikulli} eshte i lidhur");
                this.IdLlogPakRez = string.IsNullOrEmpty(nrLlogPakRez) ? artEkzistues.IdLlogPakRez : clsLlogari.mbushIDLlogariSipasKodit(nrLlogPakRez, idNdermarje);
                this.NrLlogPakRez = string.IsNullOrEmpty(nrLlogPakRez) ? artEkzistues.NrLlogPakRez : nrLlogPakRez;
                if (kaVeprimeArt && IdLlogPakRez != artEkzistues.IdLlogPakRez)
                    throw new Exception($"Nuk mund te ndryshoni llogarine e pakesim rezerve, pasi artikulli: {artEkzistues.KodArtikulli} eshte i lidhur");
                this.IdLlogariKomisioni = string.IsNullOrEmpty(nrLlogariKomisioni) ? artEkzistues.IdLlogariKomisioni : clsLlogari.mbushIDLlogariSipasKodit(nrLlogariKomisioni, idNdermarje);
                this.NrLlogKomision = string.IsNullOrEmpty(nrLlogariKomisioni) ? artEkzistues.NrLlogKomision : nrLlogariKomisioni;
                if (kaVeprimeArt && IdLlogariKomisioni != artEkzistues.IdLlogariKomisioni)
                    throw new Exception($"Nuk mund te ndryshoni llogarine e komisionit, pasi artikulli: {artEkzistues.KodArtikulli} eshte i lidhur");

                this.IdTvsh = string.IsNullOrEmpty(Tvsh) ? artEkzistues.IdTvsh : new clsTaksa(Tvsh, idNdermarje).IdTaksa;
                this.KodTaksa = string.IsNullOrEmpty(Tvsh) ? artEkzistues.KodTaksa : Tvsh;

                clsKategoriDetajimArtikulli kat = string.IsNullOrEmpty(kategoridetajimi) ? new clsKategoriDetajimArtikulli() : new clsKategoriDetajimArtikulli(kategoridetajimi);
                this.IdKategoriDetajimi = string.IsNullOrEmpty(kategoridetajimi) ? artEkzistues.IdKategoriDetajimi : kat.IdKategoriDetajimi;
                this.kategoriDetajimi = string.IsNullOrEmpty(kategoridetajimi) ? artEkzistues.kategoriDetajimi ?? string.Empty : kategoridetajimi;
                if (!String.IsNullOrEmpty(kategoridetajimi) && idKategoriDetajimi == 0)
                    throw new DbCore.MyException("Kategoria e detajimit " + kategoridetajimi + " nuk ekziston!");

                if (!String.IsNullOrEmpty(formatSeriali))
                {
                    clsSerialeUnikeFormate formate = new clsSerialeUnikeFormate(formatSeriali, idNdermarje);
                    if (formate.ID == 0)
                        throw new DbCore.MyException("Formati i serialit  " + formatSeriali + " nuk ekziston ose nuk eshte aktiv!");
                    else
                        this.IdFormatSeriali = formate.ID;
                    if (artEkzistues.IdArtikulli > 0 && this.IdFormatSeriali != artEkzistues.IdFormatSeriali && colArtikujt.kaGjendjeArtikulli(artEkzistues.IdArtikulli))
                        throw new MyException(MessagesResource.Messages["msgArtGjendjeMosVendosFormatSerial"]);
                }
                else
                    this.IdFormatSeriali = String.IsNullOrEmpty(formatSeriali) ? artEkzistues.IdFormatSeriali : 0;

                clsKategoriDetajimArtikulli kat2 = String.IsNullOrEmpty(kategoridetajimi2) ? new clsKategoriDetajimArtikulli() : new clsKategoriDetajimArtikulli(kategoridetajimi2);
                this.IdKategoriDetajimi2 = String.IsNullOrEmpty(kategoridetajimi2) ? artEkzistues.IdKategoriDetajimi2 : kat2.IdKategoriDetajimi;
                this.kategoriDetajimi2 = String.IsNullOrEmpty(kategoridetajimi2) ? artEkzistues.kategoriDetajimi2 ?? string.Empty : kategoridetajimi2;
                if (!String.IsNullOrEmpty(kategoridetajimi2) && idKategoriDetajimi2 == 0)
                    throw new DbCore.MyException("Kategoria e detajimit " + kategoridetajimi2 + " nuk ekziston!");

                if (!String.IsNullOrEmpty(detajim1) && idKategoriDetajimi == 0)
                    throw new DbCore.MyException("Plotesoni kategorine e detajimit te pare!");

                if (!String.IsNullOrEmpty(detajim2) && idKategoriDetajimi2 == 0)
                    throw new DbCore.MyException("Plotesoni kategorine e detajimit te dyte!");


                colDetajimePerArt coldet = new colDetajimePerArt();
                coldet.mbushDetajimArtSipasIdArtikulliDheLlojit(artEkzistues.IdArtikulli, 1);
                if (!String.IsNullOrEmpty(detajim1))
                {
                    string[] detajimet = detajim1.Split(',');
                    for (int d = 0; d < detajimet.Length; d++)
                    {
                        string kodDetajimi = detajimet[d];
                        clsDetajimArtikulli det = new clsDetajimArtikulli(kodDetajimi, idNdermarje);
                        if (String.IsNullOrEmpty(kodDetajimi))
                            continue;
                        if (det.IdDetajimArtikulli == 0)
                            throw new DbCore.MyException(rm.GetString("msgDetajimiMeKodNukEkziston", ci).Replace("{0}", kodDetajimi));
                        if (det.KategoriDetajimi != idKategoriDetajimi)
                            throw new DbCore.MyException("Detajimi me kod " + kodDetajimi + " nuk i perket kategorise " + kategoridetajimi);
                        if (coldet.Find(x => x.IdDetajimArtikulli == det.IdDetajimArtikulli) != null)
                            throw new MyException($"Detajimi me kod {kodDetajimi} eshte i lidhur me artikullin {kodArtikulli}.");
                        coldet.Add(new clsDetajimPerArt(0, 0, det.IdDetajimArtikulli, 1));
                    }
                }
                this.oColDetajime = coldet;

                colDetajimePerArt coldetDyte = new colDetajimePerArt();
                coldetDyte.mbushDetajimArtSipasIdArtikulliDheLlojit(artEkzistues.IdArtikulli, 2);
                if (!String.IsNullOrEmpty(detajim2))
                {
                    string[] detajimet = detajim2.Split(',');
                    for (int d = 0; d < detajimet.Length; d++)
                    {
                        string kodDetajimi = detajimet[d];
                        clsDetajimArtikulli det = new clsDetajimArtikulli(kodDetajimi, idNdermarje);
                        if (String.IsNullOrEmpty(kodDetajimi))
                            continue;
                        if (det.IdDetajimArtikulli == 0)
                            throw new DbCore.MyException(rm.GetString("msgDetajimiMeKodNukEkziston", ci).Replace("{0}", kodDetajimi));
                        if (det.KategoriDetajimi != idKategoriDetajimi2)
                            throw new DbCore.MyException("Detajimi me kod " + kodDetajimi + " nuk i perket kategorise " + kategoridetajimi2);
                        if (coldetDyte.Find(x => x.IdDetajimArtikulli == det.IdDetajimArtikulli) != null)
                            throw new MyException($"Detajimi me kod {kodDetajimi} eshte i lidhur me artikullin {kodArtikulli}.");
                        coldetDyte.Add(new clsDetajimPerArt(0, 0, det.IdDetajimArtikulli, 2));
                    }
                }
                this.oColDetajime2 = coldetDyte;

                DbQendraKosto.clsObjektivaKosto obj = string.IsNullOrEmpty(objektiva) ? new DbQendraKosto.clsObjektivaKosto() : new DbQendraKosto.clsObjektivaKosto(objektiva, idNdermarje);
                this.IdObjektivaKosto = string.IsNullOrEmpty(objektiva) ? artEkzistues.IdObjektivaKosto : obj.Id;
                this.IdMagazina = string.IsNullOrEmpty(magazina) ? artEkzistues.IdMagazina : new clsNjesiAdministrative(magazina, idNdermarje, idPerdoruesi).IdNjesiAdministrative;
                this.IdllojGarancie = string.IsNullOrEmpty(llojgarancie) ? artEkzistues.IdllojGarancie : new clsGarancia(llojgarancie).IdLlojGarancia;


                this.Autorizimet = colAutorizimetKoka.merrAutorizimeArt(artEkzistues.IdArtikulli);
                if (!String.IsNullOrEmpty(autorizimet))
                {
                    string[] pars11 = autorizimet.Split(',');
                    for (int i = 0; i < pars11.Length; i++)
                    {
                        bool kaTeDrejtaPerAutorizimin = clsAutorizimKoka.kaAutorizimSipasPerdoruesit(pars11[i], idPerdoruesi);
                        if (!kaTeDrejtaPerAutorizimin)
                            throw new DbCore.MyException("Ju nuk keni te drejta te autorizimi " + pars11[i] + "!");
                        if (Autorizimet.Split(',').Contains(pars11[i]))
                            continue;
                        this.Autorizimet += ((",") + pars11[i]);
                    }
                }

                foreach (clsKodbari kodbar in oColKodbare)
                {
                    clsMesazh mesazh = clsFunksione.kontrolloKaraktereMeMesazh(kodbar.Pershkrimi, FusheKontrolli.Kodbari, false);
                    if (!mesazh)
                        throw new MyException(mesazh.PershkrimMesazhi);
                }
                this.KoeficientArtikulli = koeficientArtikulli == null ? artEkzistues.KoeficientArtikulli : (decimal)koeficientArtikulli;
                if (kaVeprimeArt && this.KoeficientArtikulli != artEkzistues.KoeficientArtikulli)
                    throw new Exception($"Nuk mund te ndryshoni koeficientin, pasi artikulli: {artEkzistues.KodArtikulli} eshte i lidhur");
                this.PeshaBrutoArtikulli = peshaBrutoArtikulli == null ? artEkzistues.PeshaBrutoArtikulli : (decimal)peshaBrutoArtikulli;
                this.PeshaNetoArtikulli = peshaNetoArtikulli == null ? artEkzistues.PeshaNetoArtikulli : (decimal)peshaNetoArtikulli;
                this.DetajimArtikulli = detajimArtikulli == null ? artEkzistues.DetajimArtikulli : (bool)detajimArtikulli;
                this.MinimumArtikulli = minimumArtikulli == null ? artEkzistues.MinimumArtikulli : (decimal)minimumArtikulli;
                this.MaximumArtikulli = maximumArtikulli == null ? artEkzistues.MaximumArtikulli : (decimal)maximumArtikulli;
                this.LlogaritjaKMSHArtikulli = llogaritjaKMSHArtikulli == null ? artEkzistues.LlogaritjaKMSHArtikulli : (int)llogaritjaKMSHArtikulli;
                this.ZevendesimAutomatikArtikulli = zevendesimAutomatikArtikulli == null ? artEkzistues.ZevendesimAutomatikArtikulli : (int)zevendesimAutomatikArtikulli;
                this.Aktiv = aktiv == null ? artEkzistues.Aktiv : (bool)aktiv;
                this.KontrollGjendje = kontrollGjendje == null ? artEkzistues.KontrollGjendje : (bool)kontrollGjendje;
                this.KontrollCmimi = kontrollCmimi == null ? artEkzistues.KontrollCmimi : (bool)kontrollCmimi;
                this.kontrollGjendjeArtikulli = kontrollGjendjeArtikulli == null ? artEkzistues.KontrollGjendjeArtikulli : (bool)kontrollGjendjeArtikulli;
                this.SasiNjesi = sasinjesi == null ? artEkzistues.SasiNjesi : (decimal)sasinjesi;
                this.Scrap = scrap == null ? artEkzistues.Scrap : (decimal)scrap;
                this.ProdhimMePorosi = prodhimmeporosi == null ? artEkzistues.ProdhimMePorosi : (bool)prodhimmeporosi;
                this.KontrollGjendjeDetajim2 = kontrollGjendjeDetajim2 == null ? artEkzistues.KontrollGjendjeDetajim2 : (bool)kontrollGjendjeDetajim2;
                this.Garancia = garancia == null ? artEkzistues.Garancia : (decimal)garancia;
                this.IRezervueshem = rezervueshem == null ? artEkzistues.IRezervueshem : (bool)rezervueshem;
                this.MeSerial = meserial == null ? artEkzistues.MeSerial : (bool)meserial;
                if (kaVeprimeArt && this.MeSerial != artEkzistues.MeSerial)
                    throw new Exception($"Nuk mund te ndryshoni fushen Me serial, pasi artikulli: {artEkzistues.KodArtikulli} eshte i lidhur");
                this.IShitshem = iShitshem == null ? artEkzistues.IShitshem : (bool)iShitshem;
                this.MbetjeShitshme = mbetjeshitshme == null ? artEkzistues.MbetjeShitshme : (bool)mbetjeshitshme;
                this.PerPeshore = perPeshore == null ? artEkzistues.PerPeshore : (bool)perPeshore;
                this.MeBarkodLogjik = meBarkodLogjik == null ? artEkzistues.MeBarkodLogjik : (bool)meBarkodLogjik;
                this.ArtikullIVjeter = artikullIVjeter == null ? artEkzistues.ArtikullIVjeter : (bool)artikullIVjeter;
                this.MeRezerveRivleresimi = merezerveriv == null ? artEkzistues.MeRezerveRivleresimi : (bool)merezerveriv;
                if (kaVeprimeArt && this.MeRezerveRivleresimi != artEkzistues.MeRezerveRivleresimi)
                    throw new Exception($"Nuk mund te ndryshoni fushen Me rezerve rivleresimi, pasi artikulli: {artEkzistues.KodArtikulli} eshte i lidhur");
                this.NrKaraktereTAC = KaraktereTAC == null ? artEkzistues.NrKaraktereTAC : (int)KaraktereTAC;
                this.LlogaritKomision = llogaritKomision == null ? artEkzistues.LlogaritKomision : (bool)llogaritKomision;
                this.StokuMaxVfOne = stokuMaxVfOne == null ? artEkzistues.StokuMaxVfOne : (int)stokuMaxVfOne;
                this.IRimbursueshem = irimbursueshem == null ? artEkzistues.IRimbursueshem : (bool)irimbursueshem;
                this.PershkrimArtikulli = string.IsNullOrEmpty(pershkrimArtikulli) ? artEkzistues.PershkrimArtikulli : pershkrimArtikulli;
                this.PershkrimiAngArtikulli = string.IsNullOrEmpty(pershkrimiAngArtikulli) ? artEkzistues.PershkrimiAngArtikulli : pershkrimiAngArtikulli;
                this.KodiDoganorArtikulli = string.IsNullOrEmpty(kodiDoganorArtikulli) ? artEkzistues.KodiDoganorArtikulli : kodiDoganorArtikulli;
                this.VendodhjeArtikulli = string.IsNullOrEmpty(vendodhjeArtikulli) ? artEkzistues.VendodhjeArtikulli : vendodhjeArtikulli;
                this.OrigjineArtikulli = string.IsNullOrEmpty(origjineArtikulli) ? artEkzistues.OrigjineArtikulli : origjineArtikulli;
                this.objektiva = string.IsNullOrEmpty(objektiva) ? artEkzistues.Objektiva : objektiva;
                this.Magazina = string.IsNullOrEmpty(magazina) ? artEkzistues.Magazina : magazina;
                this.PershkrimFurnitori = string.IsNullOrEmpty(pershkrimFurnitori) ? artEkzistues.PershkrimFurnitori : pershkrimFurnitori;
                this.SiperfaqjaM2 = string.IsNullOrEmpty(siperfaqjam2) ? artEkzistues.SiperfaqjaM2 : siperfaqjam2;
                this.NrKontrate = string.IsNullOrEmpty(nrKontrate) ? artEkzistues.NrKontrate : nrKontrate;
                this.NrPasurie = string.IsNullOrEmpty(nrPasurie) ? artEkzistues.NrPasurie : nrPasurie;
                this.ZonaKadastrale = string.IsNullOrEmpty(zonaKadastrale) ? artEkzistues.ZonaKadastrale : zonaKadastrale;
                this.Shasia = string.IsNullOrEmpty(shasi) ? artEkzistues.Shasia : shasi;
                this.Marka = string.IsNullOrEmpty(marka) ? artEkzistues.Marka : marka;
                this.Modeli = string.IsNullOrEmpty(modeli) ? artEkzistues.Modeli : modeli;
                this.VitProdhimi = string.IsNullOrEmpty(vitProdhimi) ? artEkzistues.VitProdhimi : vitProdhimi;
                this.TeDhenaTeknika = string.IsNullOrEmpty(tedhenateknike) ? artEkzistues.TeDhenaTeknika : tedhenateknike;
                this.SkemaBarkodit = string.IsNullOrEmpty(skemaBarkodit) ? artEkzistues.SkemaBarkodit : skemaBarkodit;
                this.KodiIBarit = string.IsNullOrEmpty(kodiibarit) ? artEkzistues.KodiIBarit : kodiibarit;
                this.kodNjesia1 = string.IsNullOrEmpty(kodnjesi1) ? artEkzistues.KodNjesia1 : kodnjesi1;
                this.kodNjesia2 = string.IsNullOrEmpty(kodnjesi2) ? artEkzistues.KodNjesia2 : kodnjesi2;

                colGjendjeArtikulli gjendjeArtikulli = new colGjendjeArtikulli();
                colAseteNormaAmortizimi aseteNormaAmortizimi = new colAseteNormaAmortizimi();
                colNormaAmortizimiRezerva normaAmortizimiRezerva = new colNormaAmortizimiRezerva();
                IDictionary<string, object> hfArkiva = new Dictionary<string, object>();
                bool aparatBazar = false;
                string kodOferte = string.Empty;
                bool pertransferim = false;
                bool loan = false;
                bool dhurate = false;
                int aplikimdhurate = 1;
                decimal pike = 0;
                decimal vlere = 0;
                string kodvfone = string.Empty;
                int idArtRaportuesi = 0;

                if (artEkzistues.IdArtikulli > 0)
                {
                    gjendjeArtikulli.ktheGjendjeMinMaxArtikulli(artEkzistues.IdArtikulli);
                    aparatBazaar = artEkzistues.AparatBazaar;
                    kodOferte = artEkzistues.KodOferte;
                    pertransferim = artEkzistues.PerTransferim;
                    loan = artEkzistues.Loan;
                    dhurate = artEkzistues.Dhurate;
                    dhurate = artEkzistues.Dhurate;
                    aplikimdhurate = artEkzistues.AplikimDhurate;
                    pike = artEkzistues.Pike;
                    vlere = artEkzistues.Vlere;
                    kodvfone = artEkzistues.KodVFOne;
                    idArtRaportuesi = artEkzistues.IdArtRaportuesi;
                }

                return new clsArtikulli(idArtikulli, kodArtikulli, PershkrimArtikulli, PershkrimiAngArtikulli, KodiDoganorArtikulli, VendodhjeArtikulli, Kodifikimi1Artikulli, Kodifikimi2Artikulli, KodKodifikimi1, KodKodifikimi2, OrigjineArtikulli,
                    Njesi1Artikulli, Njesi2Artikulli, KodNjesia1, KodNjesia2, KoeficientArtikulli, IdFurnitoriKryesor, KodFurnitori, PeshaBrutoArtikulli, PeshaNetoArtikulli, DetajimArtikulli, Klasa, IdSkemaKontabilitetiArtikulli, PershkrimKlasa,
                    KodSkema, IdLlogariInventari, IdLlogariBlerje, IdLlogariShitje, IdLlogariTeTrete, IdLlogariPakesim, IdLlogariShpenzime, idLlogariAmortizimi, IdLlogRez, IdLlogPakRez, NrLlogInventari, NrLlogBlerje, NrLlogShitje, NrLlogPakesimi,
                    NrLlogTeTrete, NrLlogShpenzime, NrLlogAmortizimi, NrLlogRez, NrLlogPakRez, MinimumArtikulli, MaximumArtikulli, MetodeKostojeArtikulli, PershkrimMetodeKostoje, LlogaritjaKMSHArtikulli, ZevendesimAutomatikArtikulli, idPerdoruesi,
                    Aktiv, KontrollGjendje, KontrollCmimi, KontrollGjendjeArtikulli, colArtikujPerberes, gjendjeArtikulli, oColFurnitoreArtikujsh, oColArtikujtZevendesues, oColVleratFushaShtese, oColBuxhetet, oColDetajime, LlojiArt, idNdermarje,
                    IdTvsh, KodTaksa, IdKonfig, Autorizimet, shtim, SasiNjesi, Scrap, ProdhimMePorosi, IdKategoriDetajimi, kategoriDetajimi, kontrolloekzistence, IdKategoriDetajimi2, kategoriDetajimi2, oColDetajime2, KontrollGjendjeDetajim2,
                    IdObjektivaKosto, Objektiva, IdllojGarancie, Garancia, IdMagazina, Magazina, IRezervueshem, pertransferim, loan, dhurate, aplikimdhurate, pike, vlere, kodvfone, MeSerial, IShitshem, MbetjeShitshme, aseteNormaAmortizimi,
                    hfArkiva, rm, ci, oColKodbare, idArtRaportuesi, PerPeshore, PershkrimFurnitori, SiperfaqjaM2, NrKontrate, NrPasurie, ZonaKadastrale, Shasia, Marka, Modeli, VitProdhimi, TeDhenaTeknika, MeBarkodLogjik, SkemaBarkodit,
                    Kodifikimi3Artikulli, KodKodifikimi3, aparatBazar, kodOferte, ArtikullIVjeter, colArtikujVfone, IdFormatSeriali, MeRezerveRivleresimi, normaAmortizimiRezerva, NrKaraktereTAC, LlogaritKomision, IdLlogariKomisioni,
                    NrLlogKomision, StokuMaxVfOne, KodiIBarit, IRimbursueshem);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private clsMesazh kontrolloArtikull(bool shtim, bool kontrolloekzistence, System.Resources.ResourceManager rm, CultureInfo ci)
        {
            if (kodArtikulli == "")
                return new clsMesazh(false, rm.GetString("msgShtoArtikullPlotesoKodin", ci));//"Plotesoni kodin e artikullit!");
            clsMesazh kontrollkodArtikulli = clsFunksione.kontrolloKaraktereMeMesazh(kodArtikulli, FusheKontrolli.Kodi, false);
            if (!kontrollkodArtikulli.Status)
                return kontrollkodArtikulli;

            if (pershkrimArtikulli == "")
                return new clsMesazh(false, rm.GetString("msgClsArtikulliPlotesPershkrimin", ci));
            clsMesazh kontrollpershkrimArtikulli = clsFunksione.kontrolloKaraktereMeMesazh(pershkrimArtikulli, FusheKontrolli.Pershkrimi, true);
            if (!kontrollpershkrimArtikulli.Status)
                return kontrollpershkrimArtikulli;

            clsMesazh kontrollpershkrimiAngArtikulli = clsFunksione.kontrolloKaraktereMeMesazh(pershkrimiAngArtikulli, FusheKontrolli.Pershkrimi, true);
            if (!kontrollpershkrimiAngArtikulli.Status)
                return kontrollpershkrimiAngArtikulli;

            if (pershkrimKlasa == "")
                return new clsMesazh(false, rm.GetString("msgClsArtikulliPlotesoKlaseArtikullit", ci));
            if (kodNjesia1 == "")
                return new clsMesazh(false, rm.GetString("msgClsArtikulliPlotesoNjesineEPare", ci));
            if (kodNjesia2 == "")
                return new clsMesazh(false, rm.GetString("msgClsArtikulliPlotesoNjesineEDyte", ci));
            if (koeficientArtikulli == 0)
                return new clsMesazh(false, rm.GetString("msgClsArtikulliPlotesoKoeficentin", ci));
            if (pershkrimMetodeKostoje == "")
                return new clsMesazh(false, rm.GetString("msgClsArtikulliPlotesoMetodeEKostos", ci));

            using (clsDatabaseInventari db = new clsDatabaseInventari())
            {
                if (shtim && kontrolloekzistence && db.ekzistonArtikull(kodArtikulli, idNdermarje))
                {
                    return new clsMesazh(false, rm.GetString("msgClsArtikulliEkzistonArtikulliMeKeteKod", ci));
                }
                else
                {
                    if (!shtim)
                    {
                        string kodArtikulliVjeter = clsArtikulli.ktheKodArtikulliSipasId(idArtikulli);
                        if (kodArtikulliVjeter != kodArtikulli)
                            if (db.ekzistonArtikull(kodArtikulli, idNdermarje))
                            {
                                return new clsMesazh(false, rm.GetString("msgClsArtikulliEkzistonArtikulliMeKeteKod", ci));
                            }
                    }
                }
                if (kodKodifikimi1 != "")
                {
                    clsKodifikimArtikulli grupi1 = new clsKodifikimArtikulli(kodKodifikimi1, idNdermarje, 1, llojiArt);
                    if (grupi1.IdKodifikimi == 0)
                    {
                        return (llojiArt) ? (new clsMesazh(false, String.Format(rm.GetString("msgClsArtikulliAgjGrupi1NukEkziston", ci), kodKodifikimi1))) : (new clsMesazh(false, String.Format(rm.GetString("msgClsArtikulliGrupi1NukEkziston", ci), kodKodifikimi1)));
                    }
                    if (clsKodifikimArtikulli.eshtePrind(kodifikimi1Artikulli))
                    {
                        //db.Dispose(); hequr sepse eshte me using aq me teper qe nuk perdoret fare db, sepse krijohet tjeter manager per db
                        return new clsMesazh(false, rm.GetString("msgClsArtikulliGrupi1EshtePrindDheNukZgjidhet", ci));
                    }
                }
                if (kodKodifikimi2 != "")
                {
                    clsKodifikimArtikulli grupi2 = new clsKodifikimArtikulli(kodKodifikimi2, idNdermarje, 2, llojiArt);
                    if (grupi2.IdKodifikimi == 0)
                    {
                        return (llojiArt) ? (new clsMesazh(false, String.Format(rm.GetString("msgClsArtikulliAgjGrupi2NukEkziston", ci), kodKodifikimi2))) : (new clsMesazh(false, String.Format(rm.GetString("msgClsArtikulliGrupi2NukEkziston", ci), kodKodifikimi2)));
                    }
                    if (clsKodifikimArtikulli.eshtePrind(kodifikimi2Artikulli))
                    {
                        //db.Dispose(); hequr sepse eshte me using aq me teper qe nuk perdoret fare db, sepse krijohet tjeter manager per db
                        return new clsMesazh(false, rm.GetString("msgClsArtikulliGrupi2EshtePrindDheNukZgjidhet", ci));
                    }
                }
                if (kodKodifikimi3 != "")
                {
                    clsKodifikimArtikulli grupi3 = new clsKodifikimArtikulli(kodKodifikimi3, idNdermarje, 3, llojiArt);
                    if (grupi3.IdKodifikimi == 0)
                    {
                        return (llojiArt) ? (new clsMesazh(false, String.Format(rm.GetString("msgClsArtikulliAgjGrupi3NukEkziston", ci), kodKodifikimi3))) : (new clsMesazh(false, String.Format(rm.GetString("msgClsArtikulliGrupi3NukEkziston", ci), kodKodifikimi3)));
                    }
                    if (clsKodifikimArtikulli.eshtePrind(kodifikimi3Artikulli))
                    {
                        //db.Dispose(); hequr sepse eshte me using aq me teper qe nuk perdoret fare db, sepse krijohet tjeter manager per db
                        return new clsMesazh(false, rm.GetString("msgClsArtikulliGrupi3EshtePrindDheNukZgjidhet", ci).Replace("{0}", kodKodifikimi3));
                    }
                }
                if (idFormatSeriali != 0)
                {
                    clsSerialeUnikeFormate formati = new clsSerialeUnikeFormate(idFormatSeriali);
                    if (!formati.Aktiv)
                        return new clsMesazh(false, "Formati i serialit " + formati.Kod + " nuk eshte aktiv!");

                }
                if (!clsNjesiArtikulli.ekzistonNjesiArtikulliMeKod(kodNjesia1, idNdermarje))
                {
                    return new clsMesazh(false, rm.GetString("msgClsArtikulliNjesia1NukEkziston", ci));
                }
                if (!clsNjesiArtikulli.ekzistonNjesiArtikulliMeKod(kodNjesia2, idNdermarje))
                {
                    return new clsMesazh(false, rm.GetString("msgClsArtikulliNjesia2NukEkziston", ci));
                }
                if (koeficientArtikulli < 0)
                {
                    return new clsMesazh(false, rm.GetString("msgClsArtikulliKoeficientiDuhetPozitiv", ci));
                }
                if (kodNjesia1 == kodNjesia2 && koeficientArtikulli != 1)
                {
                    return new clsMesazh(false, rm.GetString("msgClsArtikulliKoeficientiDuhet1KurArtikulliKaNjesiTeNjejta", ci));
                }
                if (kodKlientFurnitori != "" && kodKlientFurnitori != null)
                {
                    clsKlientFurnitor kf = new clsKlientFurnitor();
                    if (!clsKlientFurnitor.EkzistonKlientFurnitor(kodKlientFurnitori, idNdermarje))
                    {
                        return new clsMesazh(false, rm.GetString("msgClsArtikulliFurnitoriNukEkziston", ci));
                    }
                    kf.mbushKlientFurnitorSipasKodit(kodKlientFurnitori, idNdermarje, idPerdoruesi);
                    if (kf.IdKlientFurnitor < 1)
                    {
                        db.Dispose();
                        return new clsMesazh(false, rm.GetString("msgClsArtikulliNukKeniAutorizimPerKeteFurnitor", ci));
                    }
                    if (kf.AktivKF == false)
                    {
                        db.Dispose();
                        return new clsMesazh(false, rm.GetString("msgClsArtikulliFurnitoriNukEshteAktiv", ci));
                    }
                }
                if (peshaBrutoArtikulli < 0)
                {
                    return new clsMesazh(false, rm.GetString("msgClsArtikulliPeshaBrutoDuhetPozitive", ci));
                }
                if (peshaNetoArtikulli < 0)
                {
                    return new clsMesazh(false, rm.GetString("msgClsArtikulliPeshaNetoDuhetPozitive", ci));
                }
                if ((klasa == 5 || klasa == 6) && sasiNjesi <= 0)
                {
                    return new clsMesazh(false, rm.GetString("msgClsArtikulliSasiNjesiaDuhetPozitive", ci));
                }
                if (scrap < 0 || scrap > 100)
                {
                    return new clsMesazh(false, rm.GetString("msgClsArtikulliFiroLigjoreDuhetNga0Deri100", ci));
                }
                if (kodiSkema != "")
                {
                    idSkemaKontabilitetiArtikulli = clsSkemaKontabilitetiArtikulli.ktheIdSkemaKontabilitetArtikulli(kodiSkema, idNdermarje, llojiArt);
                    if (idSkemaKontabilitetiArtikulli == -1)
                    {
                        return new clsMesazh(false, rm.GetString("msgClsArtikulliSkemaEKontabilitetitNukEkziston", ci));
                    }
                    clsSkemaKontabilitetiArtikulli sk = new clsSkemaKontabilitetiArtikulli(idSkemaKontabilitetiArtikulli);
                    if (sk.Klasa != klasa)
                    {
                        db.Dispose();
                        return new clsMesazh(false, rm.GetString("msgClsArtikulliKjoSkemeNukIPerketKesajKlase", ci));
                    }
                }
                if (kodTaksa != "")
                {
                    if (!clsTaksa.ekziston(kodTaksa, idNdermarje))
                    {
                        return new clsMesazh(false, rm.GetString("msgClsArtikulliKjoTakseNukEkziston", ci));
                    }
                    clsTaksa taksa = new clsTaksa(kodTaksa, idNdermarje);
                    if (!taksa.Aktiv)
                    {
                        db.Dispose();
                        return new clsMesazh(false, rm.GetString("msgClsArtikulliKjoTakseNukEshteAktive", ci));
                    }

                }
                if (kategoriDetajimi != "" && kategoriDetajimi != null)
                {
                    if (idKategoriDetajimi == 0 || idKategoriDetajimi == -1)
                    {
                        return new clsMesazh(false, rm.GetString("msgClsArtikulliKjoKategoriDetajimiNukEkziston", ci));
                    }
                }
                if (kategoriDetajimi2 != "" && kategoriDetajimi2 != null)
                {
                    if (idKategoriDetajimi2 == 0 || idKategoriDetajimi2 == -1)
                    {
                        return new clsMesazh(false, rm.GetString("msgClsArtikulliKjoKategoriDetajimiNukEkziston", ci));
                    }
                }
                if (pershkrimKlasa == "Inventar")
                {
                    if (nrLlogInventari == "" || nrLlogBlerje == "" || nrLlogShitje == "" || nrLlogTeTrete == "" || nrLlogInventari == null || nrLlogBlerje == null || nrLlogShitje == null || nrLlogTeTrete == null)
                    {
                        return new clsMesazh(false, rm.GetString("msgShtoArtikullPlotesoniSkemenOseLlogarite", ci));
                    }
                }
                else
                    if (pershkrimKlasa == "Perbere")
                {
                    if (nrLlogShitje == "" || nrLlogShitje == null)
                    {
                        return new clsMesazh(false, rm.GetString("msgShtoArtikullPlotesoniSkemenOseLlogarite", ci));
                    }
                }
                else
                        if (pershkrimKlasa == "Pastokueshem" || pershkrimKlasa == "Sherbim")
                {
                    if (nrLlogBlerje == "" || nrLlogShitje == "" || nrLlogBlerje == null || nrLlogShitje == null)
                    {
                        db.Dispose();
                        return new clsMesazh(false, rm.GetString("msgShtoArtikullPlotesoniSkemenOseLlogarite", ci));
                    }
                }
                else
                            if (pershkrimKlasa == "Prodhim")
                {
                    if (nrLlogInventari == "" || nrLlogShitje == "" || nrLlogShpenzime == "" || nrLlogInventari == null || nrLlogShitje == null || nrLlogShpenzime == null)
                    {
                        db.Dispose();
                        return new clsMesazh(false, rm.GetString("msgShtoArtikullPlotesoniSkemenOseLlogarite", ci));
                    }
                }
                else if (pershkrimKlasa == "Prodhim ne proces")
                {
                    if (nrLlogInventari == "" || nrLlogShpenzime == "" || nrLlogInventari == null || nrLlogShpenzime == null)
                    {
                        db.Dispose();
                        return new clsMesazh(false, rm.GetString("msgShtoArtikullPlotesoniSkemenOseLlogarite", ci));
                    }
                }
                if (!String.IsNullOrEmpty(nrLlogInventari))
                {
                    clsLlogari llogariInv = new clsLlogari(nrLlogInventari, idNdermarje);
                    if (llogariInv.IdLlogari <= 0)
                        return new clsMesazh(false, rm.GetString("msgClsArtikulliLlogariaInventarNukEkziston", ci));

                    if (!llogariInv.Aktiv)
                        return new clsMesazh(false, rm.GetString("msgClsArtikulliLlogariaInventarNukEshteAktive", ci));
                }

                if (!String.IsNullOrEmpty(pershkrimMetodeKostoje))
                {
                    clsMetodeKostoje metode = new clsMetodeKostoje();
                    if (metode.ktheMetodeKostojeSipasKodit(pershkrimMetodeKostoje) == false)
                    {
                        return new clsMesazh(false, rm.GetString("msgClsArtikulliMetodaEKostosNukEkziston", ci));
                    }

                }
                if (!String.IsNullOrEmpty(nrLlogBlerje))
                {
                    clsLlogari llogariBlerje = new clsLlogari(nrLlogBlerje, idNdermarje);
                    if (llogariBlerje.IdLlogari <= 0)
                        return new clsMesazh(false, rm.GetString("msgShtoArtikullLlogariaEBlerjesNukEkziston", ci));
                    if (!llogariBlerje.Aktiv)
                        return new clsMesazh(false, rm.GetString("msgClsArtikulliLlogariaBlerjeNukEshteAktive", ci));
                }
                if (!String.IsNullOrEmpty(nrLlogShitje))
                {
                    clsLlogari llogariShitje = new clsLlogari(nrLlogShitje, idNdermarje);
                    if (llogariShitje.IdLlogari <= 0)
                        return new clsMesazh(false, rm.GetString("msgClsArtikulliLlogariaShitjeNukEkziston", ci));
                    if (!llogariShitje.Aktiv)
                        return new clsMesazh(false, rm.GetString("msgClsArtikulliLlogariaShitjeNukEshteAktive", ci));

                }
                if (!String.IsNullOrEmpty(nrLlogTeTrete))
                {
                    clsLlogari llogariTre = new clsLlogari(nrLlogTeTrete, idNdermarje);
                    if (llogariTre.IdLlogari <= 0)
                        return new clsMesazh(false, rm.GetString("msgClsArtikulliLlogariaTekTeTreteNukEkziston", ci));
                    if (!llogariTre.Aktiv)
                        return new clsMesazh(false, rm.GetString("msgClsArtikulliLlogariaTekTeTreteJoAktive", ci));
                }
                if (!String.IsNullOrEmpty(nrLlogShpenzime))
                {
                    clsLlogari llogariShpenzime = new clsLlogari(nrLlogShpenzime, idNdermarje);
                    if (llogariShpenzime.IdLlogari <= 0)
                        return new clsMesazh(false, rm.GetString("msgClsArtikulliLlogariaShpenzimeNukEkziston", ci));

                    if (!llogariShpenzime.Aktiv)
                        return new clsMesazh(false, rm.GetString("msgClsArtikulliLlogariaShpenzimeJoAktive", ci));
                }
                if (!String.IsNullOrEmpty(nrLlogAmortizimi))
                {
                    clsLlogari llogariAmortizimi = new clsLlogari(nrLlogAmortizimi, idNdermarje);
                    if (llogariAmortizimi.IdLlogari <= 0)
                        return new clsMesazh(false, rm.GetString("msgClsArtikulliLlogariaAmortizimiNukEkziston", ci));

                    if (!llogariAmortizimi.Aktiv)
                        return new clsMesazh(false, rm.GetString("msgClsArtikulliLlogariaAmortizimiJoAktive", ci));
                }
                if (!String.IsNullOrEmpty(nrLlogPakRez))
                {
                    clsLlogari llogariPakesimRezerve = new clsLlogari(nrLlogPakRez, idNdermarje);
                    if (llogariPakesimRezerve.IdLlogari <= 0)
                        return new clsMesazh(false, String.Format(rm.GetString("msgClsArtikulliLlogariaPakesimRezerveNukEkziston", ci), nrLlogPakRez));

                    if (!llogariPakesimRezerve.Aktiv)
                        return new clsMesazh(false, String.Format(rm.GetString("msgClsArtikulliLlogariaPakesimRezerveJoAktive", ci), nrLlogPakRez));
                }
                if (!String.IsNullOrEmpty(nrLlogRez))
                {
                    clsLlogari llogariRezerve = new clsLlogari(nrLlogRez, idNdermarje);
                    if (llogariRezerve.IdLlogari <= 0)
                        return new clsMesazh(false, String.Format(rm.GetString("msgClsArtikulliLlogariaPakesimRezerveNukEkziston", ci), nrLlogRez));

                    if (!llogariRezerve.Aktiv)
                        return new clsMesazh(false, String.Format(rm.GetString("msgClsArtikulliLlogariaPakesimRezerveJoAktive", ci), nrLlogRez));
                }
                if (!String.IsNullOrEmpty(nrLlogKomision))
                {
                    clsLlogari llogariKomisioni = new clsLlogari(nrLlogKomision, idNdermarje);
                    if (llogariKomisioni.IdLlogari <= 0)
                        return new clsMesazh(false, String.Format(rm.GetString("msgClsArtikulliLlogariaKomisionNukEkziston", ci), nrLlogRez));

                    if (!llogariKomisioni.Aktiv)
                        return new clsMesazh(false, String.Format(rm.GetString("msgClsArtikulliLlogariaKomisionJoAktive", ci), nrLlogRez));
                }
                if (minimumArtikulli < 0)
                {
                    return new clsMesazh(false, rm.GetString("msgClsArtikulliMinimumiDuhetNumerPozitiv", ci));
                }
                if (maximumArtikulli < 0)
                {
                    return new clsMesazh(false, rm.GetString("msgClsArtikulliMaksimumiDuhetNumerPozitiv", ci));
                }
                if (minimumArtikulli != 0 && maximumArtikulli != 0 && minimumArtikulli > maximumArtikulli)
                {
                    return new clsMesazh(false, rm.GetString("msgClsArtikulliMaxDuhetMeIMadhSeMin", ci));
                }
                if (prodhimMePorosi && klasa != 5)
                {
                    return new clsMesazh(false, rm.GetString("msgClsArtikulliProdhimMePorosiJaneVetemArtEProdhim", ci));
                }
                if (iRezervueshem && klasa != 5 && klasa != 1)
                {
                    return new clsMesazh(false, rm.GetString("msgClsArtikulliTeRezervueshemJaneVetemArtEProdhimOseInventar", ci));
                }
                if (dhurate && colArtikujVfone.Count == 0)
                {
                    db.Dispose();
                    return new clsMesazh(false, "Ju lutem vendosni te pakten nje kod per artikujt dhurate!");
                }

                if (oColArtikujtZevendesues.Count > 0)
                {
                    foreach (clsArtikullZevendesues artzv in oColArtikujtZevendesues)
                    {
                        if (!db.ekzistonArtikull(artzv.KodArtikulli, idNdermarje))
                        {
                            return new clsMesazh(false, rm.GetString("msgClsArtikulliNjeNgaArtZevendesuesNukEkziston", ci));
                        }
                        clsArtikulli art = new clsArtikulli();
                        art.merrSipasKodArtikullit(artzv.KodArtikulli, idNdermarje, db);
                        if (!art.Aktiv)
                        {
                            db.Dispose();
                            return new clsMesazh(false, rm.GetString("msgClsArtikulliNjeNgaArtZevendesuesNukEshteAktiv", ci));
                        }
                    }
                }

                if (colArtikujPerberes != null && colArtikujPerberes.Count > 0)
                {
                    foreach (clsArtikulliPerberes artper in colArtikujPerberes)
                    {
                        if (artper.Lloji == 1 && artper.IdLidheseArt == 0)
                        {
                            return new clsMesazh(false, rm.GetString("msgClsArtikulliNjeNgaArtPerberesNukEkziston", ci));
                        }
                        if (artper.Lloji == 2 && artper.IdLidheseAkt == 0)
                        {
                            db.Dispose();
                            return new clsMesazh(false, rm.GetString("msgClsArtikulliNjeNgaAktivitetePerbereseNukEkziston", ci));
                        }
                        if (artper.Lloji == 1)
                        {
                            clsArtikulli art = new clsArtikulli(artper.IdLidheseArt, db);
                            if (!art.Aktiv)
                            {
                                db.Dispose();
                                return new clsMesazh(false, rm.GetString("msgClsArtikulliNjeNgaArtPerberesNukEshteAktiv", ci));
                            }
                            if ((klasa == 5 || klasa == 6) && (art.Klasa == 2 || art.Klasa == 3 || art.Klasa == 4))
                            {
                                db.Dispose();
                                return new clsMesazh(false, rm.GetString("msgClsArtikulliDuhetTeZgjidhniVetemArtikujInvProdhDheProdhNePRoc", ci));
                            }
                        }
                        if (artper.Scrap < 0 || artper.Scrap > 100)
                        {
                            db.Dispose();
                            return new clsMesazh(false, rm.GetString("msgClsArtikulliFiroLigjoreDuhetNga0Deri100", ci));
                        }
                        if (artper.Lloji == 1)
                        {
                            clsArtikulli art = new clsArtikulli(artper.IdLidheseArt, db);
                            if (!art.MbetjeShitshme && artper.Koeficienti <= 0)
                            {
                                db.Dispose();
                                return new clsMesazh(false, rm.GetString("msgClsArtikulliKoefArtPerberesDuhetNumerPozitiv", ci));
                            }
                        }
                        else if (artper.Koeficienti <= 0)
                        {
                            db.Dispose();
                            return new clsMesazh(false, rm.GetString("msgClsArtikulliKoefArtPerberesDuhetNumerPozitiv", ci));
                        }
                    }
                }
                if (shtim && oColKodbare.Count != 0)
                {
                    foreach (clsKodbari kodbar in oColKodbare)
                    {
                        clsMesazh mesazh = clsFunksione.kontrolloKaraktereMeMesazh(kodbar.Pershkrimi, FusheKontrolli.Kodbari, false);
                        if (!mesazh)
                            return mesazh;
                        if (db.ekzistonKodbar(kodbar.Pershkrimi, idNdermarje))
                        {
                            return new clsMesazh(false, "Kodbari " + kodbar.Pershkrimi + " ekziston!");
                        }
                    }
                }
            }
            if (oColFurnitoreArtikujsh.Count > 0)
            {
                foreach (clsFurnitoreArtikulli fur in oColFurnitoreArtikujsh)
                {
                    if (!clsKlientFurnitor.EkzistonKlientFurnitor(fur.KodiKF, idNdermarje))
                        return new clsMesazh(false, rm.GetString("msgClsArtikulliNjeNgaFurnitoretEArtNukEkziston", ci));
                    clsKlientFurnitor kf = new clsKlientFurnitor();
                    kf.mbushKlientFurnitorSipasKodit(fur.KodiKF, idNdermarje, idPerdoruesi);
                    if (kf.IdKlientFurnitor < 1)
                        return new clsMesazh(false, rm.GetString("msgClsArtikulliNukKeniAutorizimePerNjeNgaFurnitoret", ci));
                    if (kf.AktivKF == false)
                        return new clsMesazh(false, rm.GetString("msgClsArtikulliNjeNgaFurnitoretEArtNukEshteAktiv", ci));
                }
            }
            if (oColDetajime.Count > 0)
            {
                clsDetajimArtikulli detajim = new clsDetajimArtikulli();
                foreach (clsDetajimPerArt det in oColDetajime)
                {
                    if (det.IdDetajimArtikulli == 0)
                        return new clsMesazh(false, rm.GetString("msgClsArtikulliNjeNgaDetajimetEKategorise1NukEkziston", ci));
                    detajim.mbushDetajimArtikulliSipasId(det.IdDetajimArtikulli);
                    if (detajim.KategoriDetajimi != idKategoriDetajimi)
                        return new clsMesazh(false, rm.GetString("msgClsArtikulliNjeNgaDetajEKat1NkIPerketKatSeZgjedhur", ci));
                }
            }
            if (oColGjendjeArtikulli.Count > 0)
            {
                foreach (clsGjendjeArtikulli gjendjeArt in oColGjendjeArtikulli)
                {
                    if (gjendjeArt.GjendjaMin < 0)
                    {
                        return new clsMesazh(false, rm.GetString("msgClsArtikulliMinimumiDuhetNumerPozitiv", ci));
                    }
                    if (gjendjeArt.GjendjaMax < 0)
                    {
                        return new clsMesazh(false, rm.GetString("msgClsArtikulliMaksimumiDuhetNumerPozitiv", ci));
                    }
                    if (gjendjeArt.GjendjaMin != 0 && gjendjeArt.GjendjaMax != 0 && gjendjeArt.GjendjaMin > gjendjeArt.GjendjaMax)
                    {
                        return new clsMesazh(false, rm.GetString("msgClsArtikulliMaxDuhetMeIMadhSeMin", ci));
                    }
                }
            }
            if (oColDetajime2.Count > 0)
            {
                clsDetajimArtikulli detajim2 = new clsDetajimArtikulli();
                foreach (clsDetajimPerArt det2 in oColDetajime2)
                {
                    if (det2.IdDetajimArtikulli == 0)
                        return new clsMesazh(false, rm.GetString("msgClsArtikulliNjeNgaDetajimetEKategorise2NukEkziston", ci));
                    detajim2.mbushDetajimArtikulliSipasId(det2.IdDetajimArtikulli);
                    if (detajim2.KategoriDetajimi != idKategoriDetajimi2)
                        return new clsMesazh(false, rm.GetString("msgClsArtikulliNjeNgaDetajEKat2NkIPerketKatSeZgjedhur", ci));
                }
            }
            if (objektiva != "")
            {
                if (!DbQendraKosto.clsObjektivaKosto.ekzistonOK(objektiva, idNdermarje))
                    return new clsMesazh(false, rm.GetString("msgShtoLlogariObjektivaEKostosNukEkziston", ci));
                DbQendraKosto.clsObjektivaKosto obj = new DbQendraKosto.clsObjektivaKosto(objektiva, idNdermarje);
                if (!obj.Aktiv)
                    return new clsMesazh(false, rm.GetString("msgShtoLlogariObjektivaEKostosNukEsteAktive", ci));
            }
            if ((idllojGarancie == 0 || idllojGarancie == -1) && garancia != 0)
            {
                return new clsMesazh(false, rm.GetString("msgClsArtikulliZgjidhniLlojinEGarancise", ci));
            }
            if (magazina != "" && (idmagazina == 0 || idmagazina == -1))
            {
                return new clsMesazh(false, rm.GetString("msgClsArtikulliMagazinaEZgjedhurNukEkziston", ci));
            }
            if (merezerverivleresimi && String.IsNullOrEmpty(nrLlogRez))
            {
                return new clsMesazh(false, rm.GetString("msgClsArtikulliPlotesoLlogRez", ci));
            }
            if (merezerverivleresimi && String.IsNullOrEmpty(nrLlogPakRez))
            {
                return new clsMesazh(false, rm.GetString("msgClsArtikulliPlotesoLlogPakRez", ci));
            }
            if (llogaritKomision == true && String.IsNullOrEmpty(nrLlogKomision))
            {
                return new clsMesazh(false, rm.GetString("msgClsArtikulliPlotesoLlogKomision", ci));
            }
            return new clsMesazh(true, rm.GetString("msgClsArtikulliKontrolletEArtUKaluanMeSukses", ci));
        }

        /// <summary>
        /// Gjen artikullin e pare sipas prefixit te kodit 
        /// </summary>
        /// <param name="prefixKodi"></param>
        /// <param name="idNderm"></param>
        public void gjejTeParinSipasKodArtikullit(string prefixKodi, int idNderm, int idperdoruesi)
        {
            using (clsDatabaseInventari dbInventari = new clsDatabaseInventari())
            {
                try
                {
                    dbInventari.beginTransaksion();
                    if (!mbushArtikull(dbInventari.merrArtikullinEPareSipasKodit(prefixKodi, idNderm, idperdoruesi)))
                        dbInventari.rollbackTransaksion();
                    dbInventari.commitTransaksion();
                }
                catch (Exception)
                {
                    dbInventari.rollbackTransaksion();
                }
            }
        }

        /// <summary>
        /// Ruan objektin klient/furnitor ne tabelen perkatese ne databaze.Therret funksionin
        /// <see cref="DbCore.DbKontabiliteti.clsKlientFurnitor.ruajKlientFurnitor"/>
        /// </summary>
        /// <returns>Kthen true nese ruajtja perfundoi me sukses</returns>
        public clsMesazh ruaj(IDictionary<string, object> hfNrAutoKF, colCmimeArtikujsh colCmime, clsArtikullPerberesTemplateKoka template, bool vjenNgaImportSQL, string idArtikulliImp, string emerTabele, string primaryKey, string ndermarrjeKey)
        {
            int idart = -1;
            bool kaNdryshimNumri;
            using (var scope = new MyTransactionScope())
            {
                clsDatabaseInventari db = new clsDatabaseInventari();

                //db.beginTransaksion();
                clsMesazh mesazhKontrolli = kontrolloArtikull(out kaNdryshimNumri, db, hfNrAutoKF, false);
                if (!mesazhKontrolli.Status)
                {
                    //db.rollbackTransaksion();
                    return mesazhKontrolli;
                }
                clsMesazh u_ruajt = ruajArtikull(out idart, this.KodArtikulli, this.PershkrimArtikulli, this.PershkrimiAngArtikulli, this.KodiDoganorArtikulli, this.VendodhjeArtikulli, this.Kodifikimi1Artikulli, this.Kodifikimi2Artikulli, this.OrigjineArtikulli, this.Njesi1Artikulli, this.Njesi2Artikulli, this.KoeficientArtikulli, this.IdFurnitoriKryesor, this.PeshaBrutoArtikulli, this.PeshaNetoArtikulli, this.DetajimArtikulli, this.Klasa, this.IdSkemaKontabilitetiArtikulli, this.IdLlogariInventari, this.IdLlogariBlerje, this.IdLlogariShitje, this.IdLlogariTeTrete, this.idLlogariPakesim, this.IdLlogariShpenzime, this.IdLlogariAmortizimi, this.IdLlogRez, this.IdLlogPakRez, this.MinimumArtikulli, this.MaximumArtikulli, this.MetodeKostojeArtikulli, this.LlogaritjaKMSHArtikulli, this.ZevendesimAutomatikArtikulli, this.IdPerdoruesi, this.Aktiv, this.ColArtikujPerberes, this.colGjendjeArtikulliMag, template.IdKoka, template.Kodi, template.Pershkrimi, this.OColFurnitoreArtikujsh, this.OColArtikujtZevendesues, this.OColVleraFushaShtese, this.OColBuxhetet, this.OColDetajime, this.IdStatusDok, this.LlojiArt, colCmime, this.SasiNjesi, this.Scrap, this.ProdhimMePorosi, this.IdKategoriDetajimi, this.idKonfig, this.idTvsh, this.KontrollGjendje, this.KontrollCmimi, this.kontrollGjendjeArtikulli, this.idNdermarje, this.idPerdoruesi, this.IdKategoriDetajimi2, this.OColDetajime2, this.KontrollGjendjeDetajim2, this.idObjektivaKosto, this.idllojGarancie, this.garancia, db, idmagazina, this.iRezervueshem, this.perTransferim, this.loan, this.Dhurate, this.AplikimDhurate, this.Pike, this.Vlere, this.kodVFOne, this.meSerial, this.colNorma, this.iShitshem, this.mbetjeShitshme, this.oColKodbare, this.idArtRaportuesi, this.perPeshore, this.PershkrimFurnitori, this.SiperfaqjaM2, this.NrKontrate, this.NrPasurie, this.ZonaKadastrale, this.Shasia, this.Marka, this.Modeli, this.VitProdhimi, this.TeDhenaTeknika, this.MeBarkodLogjik, this.SkemaBarkodit, this.kodifikimi3Artikulli, aparatBazaar, kodOferte, this.artikullIVjeter, this.idFormatSeriali, this.colArtikujVfone, this.MeRezerveRivleresimi, this.colNormaRezerva, this.nrKaraktereTAC, this.idNdermarje, this.llogaritKomision, this.idLlogariKomision, this.stokuMaxVfOne, this.kodiiBarit, this.iRimbursueshem);

                this.idArtikulli = idart;
                if (!u_ruajt.Status)
                {
                    //db.rollbackTransaksion();
                    return u_ruajt;
                }

                u_ruajt = DbShare.colArkiva.RuajArkiven(idart, 13, idPerdoruesi, idNdermarje, HfArkiva);
                if (!u_ruajt.Status)
                    return u_ruajt;
                if (vjenNgaImportSQL)
                {
                    u_ruajt = db.updateDokTabeleTemportal(idArtikulliImp, idNdermarje, 1, emerTabele, primaryKey, ndermarrjeKey);

                    if (!u_ruajt.Status)
                    {
                        //db.rollbackTransaksion();
                        return u_ruajt;
                    }
                }

                //db.commitTransaksion();
                scope.Complete();
                if (kaNdryshimNumri)
                    return mesazhKontrolli;
                return u_ruajt;

            }
        }

        public clsMesazh ruaj(colCmimeArtikujsh colCmime, clsArtikullPerberesTemplateKoka template, clsDatabaseInventari db, int idNdermOrigjine)
        {
            int idart = -1;
            clsMesazh u_ruajt = ruajArtikull(out idart, this.KodArtikulli, this.PershkrimArtikulli, this.PershkrimiAngArtikulli, this.KodiDoganorArtikulli, this.VendodhjeArtikulli, this.Kodifikimi1Artikulli, this.Kodifikimi2Artikulli, this.OrigjineArtikulli, this.Njesi1Artikulli, this.Njesi2Artikulli, this.KoeficientArtikulli, this.IdFurnitoriKryesor, this.PeshaBrutoArtikulli, this.PeshaNetoArtikulli, this.DetajimArtikulli, this.Klasa, this.IdSkemaKontabilitetiArtikulli, this.IdLlogariInventari, this.IdLlogariBlerje, this.IdLlogariShitje, this.IdLlogariTeTrete, this.idLlogariPakesim, this.IdLlogariShpenzime, this.IdLlogariAmortizimi, this.IdLlogRez, this.IdLlogPakRez, this.MinimumArtikulli, this.MaximumArtikulli, this.MetodeKostojeArtikulli, this.LlogaritjaKMSHArtikulli, this.ZevendesimAutomatikArtikulli, this.IdPerdoruesi, this.Aktiv, this.ColArtikujPerberes, this.colGjendjeArtikulliMag, template.IdKoka, template.Kodi, template.Pershkrimi, this.OColFurnitoreArtikujsh, this.OColArtikujtZevendesues, this.OColVleraFushaShtese, this.OColBuxhetet, this.OColDetajime, this.IdStatusDok, this.LlojiArt, colCmime, this.SasiNjesi, this.Scrap, this.ProdhimMePorosi, this.IdKategoriDetajimi, this.idKonfig, this.idTvsh, this.KontrollGjendje, this.KontrollCmimi, this.kontrollGjendjeArtikulli, this.idNdermarje, this.idPerdoruesi, this.IdKategoriDetajimi2, this.OColDetajime2, this.KontrollGjendjeDetajim2, this.idObjektivaKosto, this.idllojGarancie, this.garancia, db, idmagazina, this.iRezervueshem, this.perTransferim, this.loan, this.Dhurate, this.AplikimDhurate, this.Pike, this.Vlere, this.kodVFOne, this.meSerial, this.colNorma, this.iShitshem, this.mbetjeShitshme, this.oColKodbare, idArtRaportuesi, this.perPeshore, this.PershkrimFurnitori, this.SiperfaqjaM2, this.NrKontrate, this.NrPasurie, this.ZonaKadastrale, this.Shasia, this.Marka, this.Modeli, this.VitProdhimi, this.TeDhenaTeknika, this.MeBarkodLogjik, this.SkemaBarkodit, this.kodifikimi3Artikulli, aparatBazaar, kodOferte, this.artikullIVjeter, this.idFormatSeriali, this.colArtikujVfone, this.MeRezerveRivleresimi, this.colNormaRezerva, this.nrKaraktereTAC, idNdermOrigjine, this.llogaritKomision, this.idLlogariKomision, this.stokuMaxVfOne, this.kodiiBarit, this.iRimbursueshem);
            this.idArtikulli = idart;
            return u_ruajt;
        }

        private clsMesazh kontrolloArtikull(out bool kaNdryshimNrAuto, clsDatabaseInventari db, IDictionary<string, object> hfNrAutoKF, bool modifikim)
        {
            kaNdryshimNrAuto = false;
            if (kodArtikulli == "")
                return new clsMesazh(false, "Kodi i artikullit nuk mund te jete bosh");
            if (!modifikim)
            {
                clsMesazh mes = new clsMesazh(true);
                if (hfNrAutoKF != null)
                {
                    mes = kontrolloNrAutoArt(out kaNdryshimNrAuto, db, hfNrAutoKF);
                    if (!mes.Status)
                        return mes;
                }
                if (db.ekzistonArtikull(kodArtikulli, idNdermarje))
                    return new clsMesazh(false, "Ekziston nje artikull me kete kod!");
                return new clsMesazh(mes.Status, mes.PershkrimMesazhi);
            }
            return new clsMesazh(true, "Kontrollet u kaluan me sukses");
        }

        private clsMesazh kontrolloNrAutoArt(out bool kaNdryshimNumri, clsDatabaseInventari db, IDictionary<string, object> hfNrAutoKf)
        {
            clsDatabaseAdmin dbadm = new clsDatabaseAdmin(db);

            List<NrAuto> list = clsNrAutom.kontrollogjithenumrat(dbadm, hfNrAutoKf, DateTime.Today);
            if (NrAuto.ktheVlerenEre(list, "KodArtikulli") != "")
                this.KodArtikulli = NrAuto.ktheVlerenEre(list, "KodArtikulli");
            DbCore.clsMesazh mes = NrAuto.ruajvlera(out kaNdryshimNumri, list, DateTime.Today, this.idPerdoruesi, this.idNdermarje, dbadm);
            return new clsMesazh(mes.Status, mes.PershkrimMesazhi);

        }

        public static bool ekziston(string kod, int idndermarje)
        {
            using (clsDatabaseInventari db = new clsDatabaseInventari())
            {
                return db.ekzistonArtikull(kod, idndermarje);
            }
        }
        public static bool ekziston(string kod, int idndermarje, clsDatabaseInventari db)
        {

            return db.ekzistonArtikull(kod, idndermarje);

        }
        public static bool ekzistonKodbar(string kodbar, int idndermarje)
        {
            using (clsDatabaseInventari db = new clsDatabaseInventari())
            {
                return db.ekzistonKodbarArtikulli(kodbar, idndermarje);
            }
        }

        public static clsMesazh ekzistojneArtikujtNeNdermarrje(List<string> kode, int idNdermarrje)
        {
            using (var db = new clsDatabaseInventari())
            {
                var dt = db.gjejArtikujQeMungojne(kode.Join(',', kod => kod), idNdermarrje);
                if (dt == null) return new MesazhSuksesi();

                if (dt.Rows.Count == 1)
                    return new MesazhGabimi($"Artikulli {dt.Rows[0]["KODI"]} nuk eshte krijuar ne ndermarrjen meme!");
                string kodi = "";
                foreach (DataRow dr in dt.Rows)
                    kodi += dr["KODI"] + ", "; ;
                return new MesazhGabimi($"Artikujt {kodi.Trim(' ').Trim(',')} nuk jane krijuar ne ndermarrjen meme!");

            }
        }
        /// <summary>
        /// Ruan objektin artikull ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbInventari.clsDatabaseInventari.ruajArtikull"/> 
        /// </summary>
        /// <param name="oColArtikujPerberes"> koleksioni me artikujt perberes te artikullit</param>
        /// <param name="templateKoka">templati per artikullin kur eshte perberje e disa artikujve</param>
        /// <param name="prodhimmeporosi"></param>
        /// <param name="meSerial">merr nese artikulli do te jete me serial unik per cdo njesi apo per nje grup sasish te percaktuara ne nje dokument hyrjeje ose blerjeje.</param>
        /// <returns> nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
        public clsMesazh ruajArtikull(out int idArtikulli, string kodArtikulli, string pershkrimArtikulli, string pershkrimiAngArtikulli, string kodiDoganorArtikulli, string vendodhjeArtikulli, int kodifikimi1Artikulli, int kodifikimi2Artikulli, string origjineArtikulli, int njesi1Artikulli, int njesi2Artikulli, decimal koeficientArtikulli, int idFurnitoriKryesor, decimal peshaBrutoArtikulli, decimal peshaNetoArtikulli, bool detajimArtikulli, int klasa, int idSkemaKontabilitetiArtikulli, int idLlogariInventari, int idLlogariBlerje, int idLlogariShitje, int idLlogariTeTrete, int idLlogariPakesim, int idLlogariShpenzime, int idLlogariAmortizimi, int idLlogariRezerve, int idLlogariPakRezerve, decimal minimumArtikulli, decimal maximumArtikulli, int metodeKostojeArtikulli, int llogaritjaKMSHArtikulli, int zevendesimAutomatikArtikulli, int idPerdoruesi, bool aktiv, colArtikulliPerberes oColArtikujPerberes, colGjendjeArtikulli ocolgjendjeArtikulli, int idTempKoka, string kodTempKoka, string pershkrimTempKoka, colFurnitoreArtikujsh oColFurnitoreArtikujsh, colArtikujtZevendesues oColArtikujtZevendesues, colVleraFushaShtese oColVleratFushaShtese, colBuxhetet oColBuxhetet, colDetajimePerArt oColDetajime, int idstatusdok, bool llojiArt, colCmimeArtikujsh colcmime, decimal sasinjesi, decimal scrap, bool prodhimmeporosi, int idKategoriDetajimi, int idkonfig, int idtvsh, bool kontrollgjendje, bool kontrollcmimi, bool kontrollgjendjeart, int idndermarje, int idperdoruesi, int idKategoriDetajimi2, colDetajimePerArt oColDetajime2, bool kontrollGjendjeDetajim2, int idobjektivakosto, int idLlojGarancia, decimal garancia, clsDatabaseInventari dbInv, int idmag, bool irezervueshem, bool pertransferim, bool loan, bool dhurate, int aplikimdhurate, decimal pike, decimal vlere, string kodvfone, bool meSerial, DbAsete.colAseteNormaAmortizimiAbstract colNorma, bool iShitshem, bool mbetjeshitshme, colKodbare oColKodbare, int idArtRaportuesi, bool perPeshore, string pershkrimFurnitori, string siperfaqjam2, string nrKontrate, string nrPasurie, string zonaKadastrale, string shasi, string marka, string modeli, string vitProdhimi, string tedhenateknike, bool meBarkodLogjik, string skemaBarkodit, int kodifikimi3Artikulli, bool aparatBazaar, string kodOferte, bool artikullivjeter, int idformatseriali, colArtikullVfone ocolArtikujVfone, bool meRezerveRivleresimi, DbAsete.colAseteNormaAmortizimiAbstract colNormaRezerva, int nrKaraktereTAC, int idNdermOrigjine, bool llogaritKomision, int idLlogariKomisioni, int stokuMaxVfOne, string kodiibarit, bool irimbursueshem)
        {
            idArtikulli = 0;
            try
            {
                clsMesazh mesazh = new clsMesazh(true);
                idArtikulli = dbInv.ruajArt(idArtikulli, kodArtikulli, pershkrimArtikulli, pershkrimiAngArtikulli, kodiDoganorArtikulli, vendodhjeArtikulli, kodifikimi1Artikulli, kodifikimi2Artikulli, origjineArtikulli, njesi1Artikulli, njesi2Artikulli, koeficientArtikulli, idFurnitoriKryesor, peshaBrutoArtikulli, peshaNetoArtikulli, detajimArtikulli, klasa, idSkemaKontabilitetiArtikulli, idLlogariInventari, idLlogariBlerje, idLlogariShitje, idLlogariTeTrete, idLlogariPakesim, idLlogariShpenzime, idLlogariAmortizimi, idLlogariRezerve, idLlogariPakRezerve, minimumArtikulli, maximumArtikulli, metodeKostojeArtikulli, llogaritjaKMSHArtikulli, zevendesimAutomatikArtikulli, idperdoruesi, idndermarje, kontrollgjendje, kontrollcmimi, kontrollgjendjeart, idtvsh, idkonfig, aktiv, idstatusdok, llojiArt, sasinjesi, scrap, prodhimmeporosi, idKategoriDetajimi, idKategoriDetajimi2, kontrollGjendjeDetajim2, idobjektivakosto, idLlojGarancia, garancia, idmagazina, irezervueshem, pertransferim, loan, dhurate, aplikimdhurate, pike, vlere, kodvfone, meSerial, iShitshem, mbetjeshitshme, idArtRaportuesi, perPeshore, pershkrimFurnitori, siperfaqjam2, nrKontrate, nrPasurie, zonaKadastrale, shasia, marka, modeli, vitProdhimi, tedhenateknike, meBarkodLogjik, skemaBarkodit, kodifikimi3Artikulli, aparatBazaar, kodOferte, artikullIVjeter, idformatseriali, meRezerveRivleresimi, nrKaraktereTAC, llogaritKomision, idLlogariKomisioni, stokuMaxVfOne, kodiibarit, irimbursueshem);
                this.idArtikulli = idArtikulli;
                if (idArtikulli == 0)
                {
                    return new clsMesazh(false, "Gabim gjate ruajtjes se Artikullit!");
                }
                clsDatabaseKontabilitet dbKontabiliteti = new clsDatabaseKontabilitet(dbInv);
                if (oColBuxhetet != null)
                {

                    foreach (clsBuxheti o in oColBuxhetet)
                    {
                        o.IdLlojBuxheti = clsLlojBuxheti.mbushIDLlojBuxheti("Artikulli", dbKontabiliteti);
                        o.IdLidhese = idArtikulli;
                        int idB;
                        mesazh = dbKontabiliteti.ruajBuxhet(out idB, o.IdLlojBuxheti, o.IdLidhese, o.Muaj, o.Buxheti_1, o.Buxheti_2, o.DtAktivizimi, o.IdKonfigUrdherPagese);
                        if (!mesazh.Status)
                        {
                            return mesazh;
                        }
                    }
                }
                clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin(dbInv);
                if (autorizimet != "")
                {
                    colLidhjetAutorizim colLidhjet = new colLidhjetAutorizim();
                    string[] pars1 = autorizimet.Split(',');
                    for (int i = 0; i < pars1.Length; i++)
                    {
                        clsLidhjeAutorizim lidhje = new clsLidhjeAutorizim();
                        lidhje.IdAutorizimeKoka = clsAutorizimKoka.ktheIDAutorizim(pars1[i]);
                        //lidhje.IdAutorizimeKoka = new DbAdmin.clsDatabaseAdmin().ktheAutorizim(pars1[i])[0].IdAutorizimKoka;
                        colLidhjet.Add(lidhje);
                    }
                    foreach (clsLidhjeAutorizim o in colLidhjet)
                    {
                        if (o.IdAutorizimeKoka == -1)
                            continue;
                        o.IdLloji = clsLlojBuxheti.mbushIDLlojBuxheti("Artikulli", dbKontabiliteti);
                        o.IdLidhese = idArtikulli;

                        mesazh = dbAdmin.ruajLidhjeAutorizim(o.IdLidhjeAutorizim, o.IdLidhese, o.IdLloji, o.IdAutorizimeKoka, 1);
                        if (!mesazh.Status)
                        {
                            //dbInv.rollbackTransaksion();
                            return mesazh;
                        }
                    }
                }
                //if (kodbari != "")
                //{
                //    colKodbare colKodbare = new colKodbare();
                //    string[] pars1 = kodbari.Split(',');
                //    for (int i = 0; i < pars1.Length; i++)
                //    {
                //        clsKodbari kodbar = new clsKodbari();
                //        kodbar.Pershkrimi = pars1[i];
                //        colKodbare.Add(kodbar);
                //    }
                foreach (clsKodbari o in oColKodbare)
                {
                    o.IdArtikulli = idArtikulli;
                    if (clsKodbari.ekzistonKodbar(o.Pershkrimi, idNdermarje, dbInv))
                        return new clsMesazh(false, String.Format("Barkodi {0} ekziston!", o.Pershkrimi));
                    mesazh = dbInv.ruajKodbar(o.IdArtikulli, o.Pershkrimi, o.Njesia, idNdermarje, o.Detajim1, o.Detajim2);
                    if (!mesazh.Status)
                        return mesazh;
                }

                if (oColFurnitoreArtikujsh != null)
                    foreach (clsFurnitoreArtikulli o in oColFurnitoreArtikujsh)
                    {
                        o.IdArtikulli = idArtikulli;
                        int idF;
                        mesazh = dbInv.ruajFurnitoreArtikulli(out idF, o.IdArtikulli, o.IdFurnitori, o.Prioriteti);
                        if (!mesazh.Status)
                        {
                            //dbInv.rollbackTransaksion();
                            return mesazh;
                        }
                    }

                if (oColDetajime != null)
                    foreach (clsDetajimPerArt o in oColDetajime)
                    {
                        o.IdArtikulli = idArtikulli;

                        mesazh = dbInv.ruajDetajimArt(o.IdArtikulli, o.IdDetajimArtikulli, 1);
                        if (!mesazh.Status)
                        {
                            return mesazh;
                        }
                    }

                if (oColDetajime2 != null)
                    foreach (clsDetajimPerArt o in oColDetajime2)
                    {
                        o.IdArtikulli = idArtikulli;

                        mesazh = dbInv.ruajDetajimArt(o.IdArtikulli, o.IdDetajimArtikulli, 2);
                        if (!mesazh.Status)
                        {
                            return mesazh;
                        }
                    }
                if (oColArtikujtZevendesues != null)
                    foreach (clsArtikullZevendesues o in oColArtikujtZevendesues)
                    {
                        o.IdArtikulliKryesor = idArtikulli;
                        int idA;
                        mesazh = dbInv.ruajArtikulliZevendesues(out idA, o.IdArtikulliKryesor, o.IdArtikulliZevend, o.Prioriteti);
                        if (!mesazh)
                            return mesazh;

                    }
                if (colcmime != null)
                {
                    colcmime.ForEach(col => col.IdArtikulli = this.IdArtikulli);

                    clsNdermarrje ndermOrigjine = new clsNdermarrje(idNdermOrigjine);

                    if (!ndermOrigjine.Prind)
                        mesazh = colCmimeArtikujsh.RuajTeGjitheMeDT(colcmime);
                    else
                    {
                        foreach (clsCmimArtikulli c in colcmime)
                        {
                            int idCmimArt;
                            mesazh = dbInv.ruajCmimArtikulli(out idCmimArt, c.IdArtikulli, c.IdNivelCmimi, c.IdNjesia, c.IdMonedha, c.DateFillimi, c.DateMbarimi, c.SasiMin,
                                       c.SasiMax, c.Cmimi, c.IdPerdoruesi, c.IdNdermarje, c.IdKonfig, c.IdStatusDok, c.IdNjesia2, c.Cmimi2, c.KoheFillimi, c.KoheMbarimi, c.IdDetajim);

                            if (!mesazh.Status)
                                return mesazh;
                        }
                    }
                }
                if (!mesazh)
                    return mesazh;
                if (oColVleratFushaShtese != null && oColVleratFushaShtese.Count > 0)
                {
                    oColVleratFushaShtese.ForEach(x => x.IdLidhese = this.idArtikulli);
                    mesazh = oColVleratFushaShtese.Ruaj();
                }
                if (oColArtikujPerberes != null)
                    foreach (DbCore.DbInventari.clsArtikulliPerberes o in oColArtikujPerberes)
                    {
                        int idA;
                        o.IdArtikulliKryesor = idArtikulli;
                        if (o.Lloji != 0)
                        {
                            mesazh = dbInv.ruajArtikullPerbere(out idA, o.Lloji, o.IdArtikulliKryesor, o.IdLidheseArt, o.Koeficienti, o.Scrap, o.IdLidheseAkt, o.GjithmoneNgaStoku, o.DtNdryshimi);
                            if (!mesazh.Status)
                            {
                                //dbInv.rollbackTransaksion();
                                return mesazh;
                            }
                        }
                    }
                if (ocolArtikujVfone != null)
                    foreach (DbCore.DbInventari.clsArtikullVfone o in ocolArtikujVfone)
                    {
                        int idA;
                        o.IdArtikulli = idArtikulli;

                        mesazh = dbInv.ruajArtikullVfone(out idA, o.IdArtikulli, o.KodVfone, o.Pike, o.Vlere);
                        if (!mesazh.Status)
                        {
                            //dbInv.rollbackTransaksion();
                            return mesazh;
                        }

                    }
                if (ocolgjendjeArtikulli != null)
                    foreach (DbCore.DbInventari.clsGjendjeArtikulli o in ocolgjendjeArtikulli)
                    {
                        int idA;
                        o.IdArtikulli = idArtikulli;
                        //if (o.Lloji != 0)
                        //{
                        mesazh = dbInv.ruajGjendjeArtikulli(out idA, o.IdArtikulli, o.IdMagazina, o.GjendjaMin, o.GjendjaMax);
                        if (!mesazh.Status)
                        {
                            //dbInv.rollbackTransaksion();
                            return mesazh;
                        }
                        // }
                    }
                colNorma.ForEach(x => x.IdArtikulli = this.idArtikulli);
                ColNormaRezerva.ForEach(x => x.IdArtikulli = this.idArtikulli);
                mesazh = colNorma.ruaj();
                if (!mesazh) return mesazh;
                mesazh = colNormaRezerva.ruaj();
                if (!mesazh) return mesazh;
                clsArtikullPerberesTemplateKoka templateKoka = new clsArtikullPerberesTemplateKoka(idTempKoka, kodTempKoka, pershkrimTempKoka, idNdermarje);
                if (kodTempKoka != null && kodTempKoka != "")
                {
                    mesazh = dbInv.ruajTemplateArtikullPerberesKoka(out idTempKoka, kodTempKoka, pershkrimTempKoka, idNdermarje);
                    if (!mesazh.Status)
                    {
                        //dbInv.rollbackTransaksion();
                        return mesazh;
                    }
                    //ruajTemplateArtikullPerberesKoka(templateKoka);
                    foreach (DbCore.DbInventari.clsArtikulliPerberesTemplateTrupi o in templateKoka.oColTrupi)
                    {
                        o.IdKoka = templateKoka.IdKoka;
                        int idT;
                        mesazh = dbInv.ruajTemplateArtikullPerberesTrupi(out idT, o.IdKoka, o.Lloji, o.IdLidheseArt, o.Koeficienti, o.Vlera, o.IdLidheseLlog);
                        //mesazh=  ruajTemplateArtikullPerberesTrupi(o);
                        if (!mesazh.Status)
                        {
                            //dbInv.rollbackTransaksion();
                            return mesazh;
                        }
                    }
                }

                //ruajtja e Arkives
                //if (oArkiva != null)
                //{
                //    foreach (DbShare.clsArkiva ar in oArkiva)
                //    {
                //        ar.IDDok = idArtikulli;

                //        mesazh = ar.update();


                //        if (!mesazh.Status)
                //        {

                //            return mesazh;
                //        }
                //    }

                //}



                //dbInv.commitTransaksion();
                if (this.iShitshem)
                {
                    PubSub ps = new PubSub("alphaweb", "alpha_items", "AlphaToFatura_Items", "https://aso.alpha.al/rest/importItemsFromAlphaToFirebase");
                    ps.PublishPubSub(3, 1, 2, 1, krijoObjektPerPubSub());
                }
                return new clsMesazh(true, IMBUtils.Messages.MessagesResource.Messages["labelRaportMesazhRuajtjaPerfundoiSukses"]);
            }
            catch (Exception ce)
            {
                //dbInv.rollbackTransaksion();
                return new clsMesazh(false, ce.Message);
            }
        }
        public object krijoObjektPerPubSubBulk(colNjesiteArtikulli njesiArtikulli, DataTable kodbare, colKodifikimeArtikulli grupim1, colNiveleCmimesh nivele, colTaksa taksat, DataTable cmime, string organization, string enterprise)
        {
            try
            {
                List<int> idNjesish = new List<int>();
                idNjesish.AddIfNotExists(this.njesi1Artikulli);
                idNjesish.AddIfNotExists(this.njesi2Artikulli);


                List<object> cmimeArt = new List<object>();
                List<string> kodBaret = new List<string>();
                List<DataRow> kodBaretArtikulli = kodbare.Rows.Cast<DataRow>().Where(x => x.Field<string>("Kod artikulli") == this.kodArtikulli).ToList();
                List<DataRow> cmimeArtikujsh = cmime.Rows.Cast<DataRow>().Where(x => x.Field<string>("Kod artikulli") == this.kodArtikulli).ToList();

                decimal cmimiBaze = 0;
                decimal cmimiBazeMeTvsh = 0;
                decimal cmimiBlerjeBaze = 0;
                clsTaksa taksa = new clsTaksa();
                if (this.IdTvsh != 0) taksa = taksat.Where(x => x.IdTaksa == this.IdTvsh).First();
                clsKodifikimArtikulli kodifikimArtikulli = new clsKodifikimArtikulli();
                if (this.kodifikimi1Artikulli != 0) kodifikimArtikulli = grupim1.Where(x => x.IdKodifikimi == this.kodifikimi1Artikulli).First();
                for (int i = 0; i < cmimeArtikujsh.Count; i++)
                {
                    string kodNivelCmimi = (string)cmimeArtikujsh[i].ItemArray[3];
                    decimal cmimi = (decimal)cmimeArtikujsh[i].ItemArray[9];
                    decimal cmimiTvsh = (decimal)cmimeArtikujsh[i].ItemArray[16];
                    DateTime dtFillimi = (DateTime)cmimeArtikujsh[i].ItemArray[5];
                    DateTime dtMbarimi = (DateTime)cmimeArtikujsh[i].ItemArray[6];
                    if (nivele.Where(x => x.PershkrimNivelCmimi == kodNivelCmimi).Count() == 0) continue;
                    clsNivelCmimi nvCmimi = nivele.Where(x => x.PershkrimNivelCmimi == kodNivelCmimi).First();
                    bool nivelCmimi = nvCmimi.NivelCmimiBaze;
                    int llojiNivelCmimi = nvCmimi.LlojiNivelCmimi;

                    //if (nvCmimi.Lloji) continue;
                    //bool nivelCmimi = nvCmimi.NivelCmimiBaze;
                    if (llojiNivelCmimi == 1 && nivelCmimi)
                    {
                        cmimiBlerjeBaze = cmimi;
                        continue;
                    }
                    else if (nvCmimi.LlojiNivelCmimi == 1)
                    {
                        continue;
                    }
                    cmimiBaze = nivelCmimi == true ? cmimi : cmimiBaze;
                    cmimiBazeMeTvsh = nivelCmimi == true ? cmimiTvsh : cmimiBazeMeTvsh;
                    long startDate = new DateTimeOffset(dtFillimi).ToUnixTimeSeconds() * 1000;
                    long endDate = new DateTimeOffset(dtMbarimi).ToUnixTimeSeconds() * 1000;

                    cmimeArt.Add(new { priceLevel = nvCmimi.PershkrimNivelCmimi, price = cmimi, priceWithVat = cmimiTvsh, startDate = startDate, endDate = endDate, basePrice = nivelCmimi });

                }
                for (int i = 0; i < kodBaretArtikulli.Count; i++)
                {
                    this.kodiiBarit = this.kodiiBarit == "" || this.kodiiBarit == null ? kodBaretArtikulli[0].ItemArray[2].ToString() : this.kodiiBarit;
                    kodBaret.Add(kodBaretArtikulli[i].ItemArray[2].ToString());
                }
                clsNjesiArtikulli unit = njesiArtikulli.Where(x => x.IdNjesia == this.njesi1Artikulli).FirstOrDefault();
                //object[] njesite = new object[njesiArtikulli.Count];
                //for (int i = 0; i < njesiArtikulli.Count; i++) njesite[i] = new { unit = njesiArtikulli[i].KodNjesia, unitFisc = njesiArtikulli[i].KodEinvoice };
                object objectForPubSub = new
                {
                    code = this.kodArtikulli,
                    barcode = this.KodiIBarit,
                    name = this.pershkrimArtikulli,
                    unit = unit.KodNjesia,
                    active = this.aktiv,
                    unitFisc = unit.KodEinvoice,
                    price = cmimiBaze,
                    priceWithVat = cmimiBazeMeTvsh,
                    vatPercentage = taksa.NormaPerqindje,
                    noVat = this.IdTvsh == 0 ? true : false,
                    exemptReason = taksa.TipiIPerjashtimit == null ? "" : taksa.TipiIPerjashtimit,
                    priceLevels = cmimeArt,
                    barCodes = kodBaret,
                    category = kodifikimArtikulli.PershkrimKodifikimi
                };
                return objectForPubSub;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }


        }
        public clsMesazh modifiko(clsArtikullPerberesTemplateKoka template, colCmimeArtikujsh cmime, bool vjenNgaImportSQL, string idArtikulliImp, string emerTabele, string primaryKey, string ndermarrjeKey)
        {
            using (var scope = new MyTransactionScope())
            {
                clsDatabaseInventari dbInv = new clsDatabaseInventari();
                clsMesazh modifikim = modifiko(template, cmime, dbInv);
                if (!modifikim.Status)
                    return modifikim;

                if (vjenNgaImportSQL)
                {
                    modifikim = dbInv.updateDokTabeleTemportal(idArtikulliImp, idNdermarje, 1, emerTabele, primaryKey, ndermarrjeKey);
                    if (!modifikim.Status)
                        return modifikim;
                }
                if (this.iShitshem)
                {
                    PubSub ps = new PubSub("alphaweb", "alpha_items", "AlphaToFatura_Items", "https://aso.alpha.al/rest/importItemsFromAlphaToFirebase");
                    ps.PublishPubSub(3, 1, 2, 1, krijoObjektPerPubSub());

                }
                scope.Complete();
                return modifikim;
            }
        }

        public clsMesazh modifiko(clsArtikullPerberesTemplateKoka template, colCmimeArtikujsh cmime, clsDatabaseInventari dbInv)
        {
            clsMesazh modifikim = modifikoArtikull(this.idArtikulli, this.kodArtikulli, this.pershkrimArtikulli, this.pershkrimiAngArtikulli, this.kodiDoganorArtikulli, this.vendodhjeArtikulli, this.kodifikimi1Artikulli, this.kodifikimi2Artikulli, this.origjineArtikulli, this.njesi1Artikulli, this.njesi2Artikulli, this.koeficientArtikulli, this.idFurnitoriKryesor, this.peshaBrutoArtikulli, this.peshaNetoArtikulli, this.detajimArtikulli, this.klasa, this.idSkemaKontabilitetiArtikulli, this.idLlogariInventari, this.idLlogariBlerje, this.idLlogariShitje, this.idLlogariTeTrete, this.idLlogariShpenzime, this.idLlogariAmortizimi, this.IdLlogariPakesim, this.idLlogRez, this.idLlogPakRez, this.minimumArtikulli, this.maximumArtikulli, this.metodeKostojeArtikulli, this.llogaritjaKMSHArtikulli, this.zevendesimAutomatikArtikulli, this.idPerdoruesi, this.aktiv, this.colArtikujPerberes, template.IdKoka, template.Kodi, template.Pershkrimi, this.OColFurnitoreArtikujsh, this.OColArtikujtZevendesues, this.OColVleraFushaShtese, this.OColBuxhetet, this.OColDetajime, this.IdStatusDok, this.LlojiArt, cmime, this.SasiNjesi, this.Scrap, this.ProdhimMePorosi, this.IdKategoriDetajimi, this.IdKategoriDetajimi2, this.OColDetajime2, this.KontrollGjendjeDetajim2, this.IdObjektivaKosto, this.IdllojGarancie, this.Garancia, this.IdMagazina, this.IRezervueshem, this.PerTransferim, this.loan, this.Dhurate, this.AplikimDhurate, this.Pike, this.Vlere, this.kodVFOne, this.meSerial, this.iShitshem, this.mbetjeShitshme, this.colNorma, dbInv, this.oColKodbare, idArtRaportuesi, this.perPeshore, this.pershkrimFurnitori, this.SiperfaqjaM2, this.NrKontrate, this.NrPasurie, this.ZonaKadastrale, this.Shasia, this.Marka, this.Modeli, this.VitProdhimi, this.TeDhenaTeknika, this.MeBarkodLogjik, this.SkemaBarkodit, this.kodifikimi3Artikulli, aparatBazaar, kodOferte, this.artikullIVjeter, this.idFormatSeriali, this.ColArtikujVfone, this.merezerverivleresimi, this.colNormaRezerva, this.nrKaraktereTAC, this.llogaritKomision, this.idLlogariKomision, this.stokuMaxVfOne, this.kodiiBarit, this.iRimbursueshem);
            return modifikim;
        }
        public void sendItemsToPubSubInBulk(List<object> itemList, PubSub ps)
        {
            ps.PublishPubSub(3, 1, 2, 1, itemList);
        }
        public void sendItemToPubSub()
        {
            PubSub ps = new PubSub("alphaweb", "alpha_items", "AlphaToFatura_Items", "https://aso.alpha.al/rest/importItemsFromAlphaToFirebase");
            ps.PublishPubSub(3, 1, 2, 1, krijoObjektPerPubSub());
        }
        public object krijoObjektPerPubSub()
        {
            List<int> idNjesish = new List<int>();
            idNjesish.AddIfNotExists(this.njesi1Artikulli);
            idNjesish.AddIfNotExists(this.njesi2Artikulli);
            List<object> njesite = new List<object>();
            colNjesiteArtikulli njesiArtikulli = new colNjesiteArtikulli(idNjesish);


            List<object> cmimeArt = new List<object>();
            List<string> kodBaret = new List<string>();
            colKodbare kodBaretArtikulli = new colKodbare(this.IdArtikulli);
            colCmimeArtikujsh cmimeArtikujsh = new colCmimeArtikujsh();
            decimal cmimiBaze = 0;
            decimal cmimiBazeMeTvsh = 0;
            double cmimiBlerjeBaze = 0;
            decimal cmimiBlerjeBazeTvsh = 0;
            cmimeArtikujsh.mbushCmimArtikulliShitjeDheBlerjeSipasArtikullitMeKostoMeAutorizime(this.idArtikulli, IdNdermarje, IdPerdoruesi);
            clsTaksa taksa = new clsTaksa(this.IdTvsh);
            string photoURL = "";
            Guid uuid = Guid.NewGuid();
            string fileName = $"alpha/{uuid}";
            //try
            //{
            //    #region images
            //    colArkiva arkiva = new colArkiva(this.IdArtikulli, 13);
            //    foreach (clsArkiva keyValue in arkiva)
            //    {
            //        if (keyValue.FileType != ".jpg" && keyValue.FileType != ".png") continue;
            //        string filePath = System.Web.Hosting.HostingEnvironment.MapPath(keyValue.Path);
            //        //using (FileStream image = File.Open((string)keyValue.Value, FileMode.Open))
            //        using (FileStream image = File.Open(filePath, FileMode.Open))
            //        {
            //            Image img = Image.FromStream(image);
            //            int size = 1024;
            //            Image newImage = img;
            //            if (img.Width > size && img.Height > size)
            //            {
            //                Bitmap bitMap = new Bitmap(size, size);
            //                using (Graphics graphics = Graphics.FromImage((Image)bitMap))
            //                {
            //                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //                    graphics.DrawImage(img, 0, 0, size, size);
            //                    newImage = (Image)bitMap;
            //                }
            //            }
            //            UploadObjectOptions options = new UploadObjectOptions();
            //            options.UserProject = "imb-payment";

            //            GoogleCredential credential = Task.Run(() => GoogleCredential.GetApplicationDefault()).Result;
            //            StorageClient storage = StorageClient.Create(credential);
            //            string contentType = keyValue.FileType;
            //            storage.UploadObject(paymentBucket, fileName, contentType, image, options);
            //        }

            //    }
            //    #endregion
            //}
            //catch (Exception e)
            //{
            //    ImbLogger.LogErrorShitje(e.Message);
            //}


            clsKodifikimArtikulli kodifikimArtikulli = new clsKodifikimArtikulli(this.kodifikimi1Artikulli);
            for (int i = 0; i < cmimeArtikujsh.Count; i++)
            {
                clsCmimArtikulli currentPrice = cmimeArtikujsh[i];

                clsNivelCmimi nvCmimi = new clsNivelCmimi(currentPrice.IdNivelCmimi);
                //if (nvCmimi.Lloji) continue;
                bool nivelCmimi = nvCmimi.NivelCmimiBaze;
                if (nvCmimi.LlojiNivelCmimi == 1 && nivelCmimi)
                {
                    cmimiBlerjeBaze = currentPrice.Kosto;
                    //cmimiBlerjeBazeTvsh = currentPrice.CmimiTvsh;
                    continue;
                }
                else if (nvCmimi.LlojiNivelCmimi == 1)
                {
                    continue;
                }
                cmimiBaze = nivelCmimi == true ? currentPrice.Cmimi : cmimiBaze;
                cmimiBazeMeTvsh = nivelCmimi == true ? currentPrice.CmimiTvsh : cmimiBazeMeTvsh;
                long startDate = new DateTimeOffset(currentPrice.DateFillimi).ToUnixTimeSeconds() * 1000;
                long endDate = new DateTimeOffset(currentPrice.DateMbarimi).ToUnixTimeSeconds() * 1000;

                cmimeArt.Add(new { priceLevel = nvCmimi.PershkrimNivelCmimi, price = currentPrice.Cmimi, priceWithVat = currentPrice.CmimiTvsh, startDate = startDate, endDate = endDate, basePrice = nivelCmimi });
            }
            for (int i = 0; i < kodBaretArtikulli.Count; i++)
            {
                this.kodiiBarit = this.kodiiBarit == "" || this.kodiiBarit == null ? kodBaretArtikulli[0].Pershkrimi : this.kodiiBarit;
                kodBaret.Add(kodBaretArtikulli[i].Pershkrimi);
            }
            for (int i = 0; i < njesiArtikulli.Count; i++) njesite.Add(new { unit = njesiArtikulli[i].KodNjesia, unitFisc = njesiArtikulli[i].KodEinvoice });
            object objectForPubSub = new
            {
                code = this.kodArtikulli,
                barcode = this.KodiIBarit,
                name = this.pershkrimArtikulli,
                units = njesite,
                active = this.aktiv,
                price = cmimiBaze,
                priceWithVat = cmimiBazeMeTvsh,
                vatPercentage = taksa.NormaPerqindje,
                noVat = this.IdTvsh == 0 ? true : false,
                exemptReason = taksa.TipiIPerjashtimit,
                priceLevels = cmimeArt,
                organization = clsKontrollePerFiskalizimin.ktheInitialCatalogTeLoguar(),
                ndermarrja = new clsNdermarrje(idNdermarje).NdermarrjeKodi,
                barCodes = kodBaret,
                category = kodifikimArtikulli.PershkrimKodifikimi,
                cost = cmimiBlerjeBaze,
                description = this.pershkrimiAngArtikulli
            };
            return objectForPubSub;

        }
        /// <summary>
        /// Modifikon objektin artikull ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbInventari.clsDatabaseInventari.modifikoArtikull"/> 
        /// </summary>
        /// <param name="oColArtikujPerberes"> koleksioni me artikujt perberes te artikullit</param>
        /// <param name="templateKoka">templati per artikullin kur eshte perberje e disa artikujve</param>
        /// <param name="prodhimmeporosi"></param>
        /// <param name="meSerial">merr nese artikulli do te jete me serial unik per cdo njesi apo per nje grup sasish te percaktuara ne nje dokument hyrjeje ose blerjeje.</param>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo</returns>
        public clsMesazh modifikoArtikull(int idArtikulli, string kodArtikulli, string pershkrimArtikulli, string pershkrimiAngArtikulli, string kodiDoganorArtikulli, string vendodhjeArtikulli, int kodifikimi1Artikulli, int kodifikimi2Artikulli, string origjineArtikulli, int njesi1Artikulli, int njesi2Artikulli, decimal koeficientArtikulli, int idFurnitoriKryesor, decimal peshaBrutoArtikulli, decimal peshaNetoArtikulli, bool detajimArtikulli, int klasa, int idSkemaKontabilitetiArtikulli, int idLlogariInventari, int idLlogariBlerje, int idLlogariShitje, int idLlogariTeTrete, int idLlogariShpenzime, int idLlogariAmortizimi, int idLlogariPakesim, int idLlogariRezervim, int idLlogariPakRezerve, decimal minimumArtikulli, decimal maximumArtikulli, int metodeKostojeArtikulli, int llogaritjaKMSHArtikulli, int zevendesimAutomatikArtikulli, int idPerdoruesi, bool aktiv, colArtikulliPerberes oColArtikujPerberes, int idTempKoka, string kodTempKoka, string pershkrimTempKoka, colFurnitoreArtikujsh oColFurnitoreArtikujsh, colArtikujtZevendesues oColArtikujtZevendesues, colVleraFushaShtese oColVleratFushaShtese, colBuxhetet oColBuxhetet, colDetajimePerArt oColDetajime, int idstatusdok, bool llojiArt, colCmimeArtikujsh colCmime, decimal sasinjesi, decimal scrap, bool prodhimmeporosi, int idkategoridetajimi, int idKategoriDetajimi2, colDetajimePerArt oColDetajime2, bool kontrolloGjendjeDetajim2, int idobjektivakosto, int idLlojGarancie, decimal garancia, int idmag, bool irezervueshem, bool pertransferim, bool loan, bool dhurate, int aplikimdhurate, decimal pike, decimal vlere, string kodvfone, bool meSerial, bool iShitshem, bool mbetjeshitshme, DbAsete.colAseteNormaAmortizimiAbstract colNorma, clsDatabaseInventari dbInv, colKodbare oColKodbare, int idArtRaportuesi, bool perPeshore, string pershkrimFurnitori, string siperfaqjam2, string nrKontrate, string nrPasurie, string zonaKadastrale, string shasi, string marka, string modeli, string vitProdhimi, string tedhenateknike, bool meBarkodLogjik, string skemaBarkodit, int kodifikimi3Artikulli, bool aparatBazaar, string kodOferte, bool artikullIVjeter, int idformatseriali, colArtikullVfone ocolArtikujVfone, bool meRezerveRivleresimi, DbAsete.colAseteNormaAmortizimiAbstract colNormaRezerva, int nrKaraktereTAC, bool llogaritKomision, int llogariKomisioni, int stokuMaxVfOne, string kodiibarit, bool irimbursueshem)
        {
            colKodbare colKodbareEkzistues = new colKodbare(idArtikulli, dbInv);
            colFurnitoreArtikujsh colFurnitoret = new colFurnitoreArtikujsh();
            colFurnitoret.mbushFurnitoreArtikulliSipasIdArtikulli(idArtikulli, dbInv);
            colArtikujtZevendesues colArtikujtZevend = new colArtikujtZevendesues();
            colArtikujtZevend.merrArtZevendesuesSipasIdArtikulli(idArtikulli, dbInv);
            colDetajimePerArt detajimetEkzistuese = new colDetajimePerArt();
            detajimetEkzistuese.mbushDetajimArtSipasIdArtikulliDheLlojit(idArtikulli, 1, dbInv);
            colDetajimePerArt detajimetEkzistuese2 = new colDetajimePerArt();
            detajimetEkzistuese2.mbushDetajimArtSipasIdArtikulliDheLlojit(idArtikulli, 2, dbInv);
            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin(dbInv);
            List<string> data = colArtikulliPerberes.merrDataArtikujPerberes(idArtikulli, dbInv);
            colLidhjetAutorizim colLidhjetAutorizim = new colLidhjetAutorizim(idArtikulli, "Artikulli", dbAdmin);
            clsDatabaseKontabilitet dbKontabiliteti = new clsDatabaseKontabilitet(dbInv);
            try
            {
                clsMesazh mesazh;
                mesazh = dbInv.modifikoArt(idArtikulli, kodArtikulli, pershkrimArtikulli, pershkrimiAngArtikulli, kodiDoganorArtikulli, vendodhjeArtikulli, kodifikimi1Artikulli, kodifikimi2Artikulli, origjineArtikulli, njesi1Artikulli, njesi2Artikulli, koeficientArtikulli, idFurnitoriKryesor, peshaBrutoArtikulli, peshaNetoArtikulli, detajimArtikulli, klasa, idSkemaKontabilitetiArtikulli, idLlogariInventari, idLlogariBlerje, idLlogariShitje, idLlogariTeTrete, idLlogariShpenzime, idLlogariAmortizimi, idLlogariPakesim, idLlogariRezervim, idLlogariPakRezerve, minimumArtikulli, maximumArtikulli, metodeKostojeArtikulli, llogaritjaKMSHArtikulli, zevendesimAutomatikArtikulli, idPerdoruesi, idNdermarje, kontrollGjendje, kontrollCmimPerDetajim, kontrollGjendjeArtikulli, idTvsh, idKonfig, aktiv, idstatusdok, llojiArt, sasinjesi, scrap, prodhimmeporosi, idkategoridetajimi, idKategoriDetajimi2, kontrolloGjendjeDetajim2, idobjektivakosto, idLlojGarancie, garancia, idmagazina, irezervueshem, pertransferim, loan, dhurate, aplikimdhurate, pike, vlere, kodvfone, meSerial, iShitshem, mbetjeshitshme, idArtRaportuesi, perPeshore, pershkrimFurnitori, siperfaqjam2, nrKontrate, nrPasurie, zonaKadastrale, shasia, marka, modeli, vitProdhimi, tedhenateknike, meBarkodLogjik, skemaBarkodit, kodifikimi3Artikulli, aparatBazaar, kodOferte, artikullIVjeter, idformatseriali, meRezerveRivleresimi, nrKaraktereTAC, llogaritKomision, llogariKomisioni, stokuMaxVfOne, kodiibarit, irimbursueshem);

                if (!mesazh.Status)
                {
                    return mesazh;
                }
                if (oColBuxhetet != null)
                    foreach (clsBuxheti o in oColBuxhetet)
                    {
                        mesazh = dbKontabiliteti.modifikoBuxhet(o.IdBuxheti, o.IdLlojBuxheti, o.IdLidhese, o.Muaj, o.Buxheti_1, o.Buxheti_2, o.DtAktivizimi, o.IdKonfigUrdherPagese);
                        if (!mesazh.Status)
                        {
                            return mesazh;
                        }
                    }
                if (oColVleratFushaShtese.Count > 0)
                {
                    oColVleratFushaShtese.ForEach(x => x.IdLidhese = idArtikulli);
                    mesazh = oColVleratFushaShtese.Ruaj();
                }

                colLidhjetAutorizim colLidhjet = new colLidhjetAutorizim();
                if (autorizimet != "")
                {
                    string[] pars1 = autorizimet.Split(',');
                    for (int i = 0; i < pars1.Length; i++)
                    {
                        colLidhjet.Add(new clsLidhjeAutorizim() { IdAutorizimeKoka = clsAutorizimKoka.ktheIDAutorizim(pars1[i]) });
                    }
                }
                for (int i = 0; i < colLidhjet.Count; i++)
                {
                    int idAutorizimKoka = colLidhjet[i].IdAutorizimeKoka;
                    if (idAutorizimKoka == -1)
                        continue;
                    colLidhjet[i].IdLloji = clsLlojBuxheti.mbushIDLlojBuxheti("Artikulli", dbKontabiliteti);
                    colLidhjet[i].IdLidhese = idArtikulli;
                    clsLidhjeAutorizim lidhjeNjejte = colLidhjetAutorizim.Find(x => x.IdAutorizimeKoka == idAutorizimKoka);
                    if (lidhjeNjejte != null)
                    {   //i heqim nga collectioni autorizimet qe sjane ndryshuar sepse ne te do ngelen vetem autorizimet qe do te fshihen(vendosen status 2) 
                        colLidhjetAutorizim.Remove(lidhjeNjejte);
                        continue;
                    }
                    mesazh = dbAdmin.ruajLidhjeAutorizim(colLidhjet[i].IdLidhjeAutorizim, colLidhjet[i].IdLidhese, colLidhjet[i].IdLloji, colLidhjet[i].IdAutorizimeKoka, 1);
                    if (!mesazh.Status)
                        return mesazh;
                }
                //fshihen autorizimet e vjetra. Ps: Jo ato qe kane ngelur njesoj, ato do ngelen si jane
                mesazh = colLidhjetAutorizim.FshiLidhjeAutorizim(colLidhjetAutorizim, IdPerdoruesi, dbAdmin);
                if (!mesazh.Status)
                    return mesazh;


                mesazh = dbInv.fshiArtikullVfone(idArtikulli);
                if (!mesazh.Status)
                {

                    return mesazh;
                }

                //mesazh= fshiArtikujPerberes(artikulli);
                if (ocolArtikujVfone != null)
                    foreach (DbCore.DbInventari.clsArtikullVfone o in ocolArtikujVfone)
                    {
                        int idP;

                        o.IdArtikulli = idArtikulli;

                        mesazh = dbInv.ruajArtikullVfone(out idP, o.IdArtikulli, o.KodVfone, o.Pike, o.Vlere);
                        if (!mesazh.Status)
                        {

                            return mesazh;
                        }
                    }

                mesazh = RuajDetajime(dbInv, idArtikulli, detajimetEkzistuese, oColDetajime, 1);
                if (!mesazh)
                    return mesazh;

                mesazh = RuajDetajime(dbInv, idArtikulli, detajimetEkzistuese2, oColDetajime2, 2);
                if (!mesazh)
                    return mesazh;

                for (int i = 0; i < oColKodbare.Count; i++)
                {
                    clsKodbari kod = oColKodbare[i];
                    clsKodbari lidhjeNjejte = colKodbareEkzistues.Find(x => x.Pershkrimi == kod.Pershkrimi);
                    if (lidhjeNjejte != null)
                    {
                        if (kod.Njesia != lidhjeNjejte.Njesia || kod.Detajim1 != lidhjeNjejte.Detajim1 || kod.Detajim2 != lidhjeNjejte.Detajim2)
                        {
                            mesazh = dbInv.modifikoKodbar(lidhjeNjejte.IdKodbari, idArtikulli, kod.Pershkrimi, kod.Njesia, kod.Detajim1, kod.Detajim2);
                        }
                        //i heqim nga collectioni barkodet qe nuk jane ndryshuar, sepse ne te do ngelen vetem kodbaret qe do te fshihen                        
                        colKodbareEkzistues.Remove(lidhjeNjejte);
                        continue;
                    }
                    if (clsKodbari.ekzistonKodbar(kod.Pershkrimi, idNdermarje, dbInv))
                        return new clsMesazh(false, String.Format("Barkodi {0} ekziston!", kod.Pershkrimi));
                    mesazh = dbInv.ruajKodbar(idArtikulli, kod.Pershkrimi, kod.Njesia, idNdermarje, kod.Detajim1, kod.Detajim2);
                    if (!mesazh.Status)
                        return mesazh;
                }

                //fshihen barkodet qe jane hequr nga artikulli
                for (int j = 0; j < colKodbareEkzistues.Count; j++)
                {
                    int idBarkod = colKodbareEkzistues[j].IdKodbari;
                    if (clsKodbari.eshteBarkodILidhur(idBarkod, dbInv))
                        return new clsMesazh(false, String.Format("Barkodi {0} eshte perdorur ne veprime dhe nuk mund te fshihet!", colKodbareEkzistues[j].Pershkrimi));
                    mesazh = dbInv.fshiKodbar(idBarkod);
                    if (!mesazh.Status)
                        return mesazh;
                }


                if (colCmime != null)
                    mesazh = colCmimeArtikujsh.RuajTeGjitheMeDT(colCmime);
                if (!mesazh) return mesazh;
                #region komentuar

                #endregion
                if (colFurnitoret.Count < OColFurnitoreArtikujsh.Count)//rasti kur jane shtuar rreshta trupi
                {
                    for (int i = 0; i < OColFurnitoreArtikujsh.Count; i++)
                    {
                        OColFurnitoreArtikujsh[i].IdArtikulli = idArtikulli;
                        if (i < colFurnitoret.Count)
                        {

                            OColFurnitoreArtikujsh[i].IdFurnitoreArtikulli = colFurnitoret[i].IdFurnitoreArtikulli;
                            mesazh = dbInv.modifikoFurnitoreArtikulli(OColFurnitoreArtikujsh[i].IdFurnitoreArtikulli, OColFurnitoreArtikujsh[i].IdArtikulli, OColFurnitoreArtikujsh[i].IdFurnitori, OColFurnitoreArtikujsh[i].Prioriteti);
                            //mesazh =   modifikoFurnitoreArtikulli(artikulli.OColFurnitoreArtikujsh[i]);
                        }
                        else
                        {
                            int idoC;
                            mesazh = dbInv.ruajFurnitoreArtikulli(out idoC, OColFurnitoreArtikujsh[i].IdArtikulli, OColFurnitoreArtikujsh[i].IdFurnitori, OColFurnitoreArtikujsh[i].Prioriteti);
                            //mesazh=   ruajFurnitoreArtikulli(artikulli.OColFurnitoreArtikujsh[i]);
                        }
                        if (!mesazh.Status)
                        {

                            return mesazh;
                        }
                    }
                }
                else//rasti kur jane fshire rreshta
                {
                    int count = 0;
                    for (int i = 0; i < colFurnitoret.Count; i++)
                    {
                        if (count < OColFurnitoreArtikujsh.Count)
                        {

                            OColFurnitoreArtikujsh[i].IdArtikulli = idArtikulli;
                            OColFurnitoreArtikujsh[i].IdFurnitoreArtikulli = colFurnitoret[i].IdFurnitoreArtikulli;
                            mesazh = dbInv.modifikoFurnitoreArtikulli(OColFurnitoreArtikujsh[i].IdFurnitoreArtikulli, OColFurnitoreArtikujsh[i].IdArtikulli, OColFurnitoreArtikujsh[i].IdFurnitori, OColFurnitoreArtikujsh[i].Prioriteti);
                            //mesazh=  modifikoFurnitoreArtikulli(artikulli.OColFurnitoreArtikujsh[i]);
                        }
                        else
                        {
                            mesazh = dbInv.fshiFurnitoreArtikulli(colFurnitoret[i].IdFurnitoreArtikulli);
                            //mesazh= fshiFurnitoreArtikulli(colFurnitoret[i]);
                        }
                        count++;
                        if (!mesazh.Status)
                        {

                            return mesazh;
                        }
                    }
                }

                if (colArtikujtZevend.Count < OColArtikujtZevendesues.Count)//rasti kur jane shtuar rreshta trupi
                {
                    for (int i = 0; i < OColArtikujtZevendesues.Count; i++)
                    {
                        OColArtikujtZevendesues[i].IdArtikulliKryesor = idArtikulli;
                        if (i < colArtikujtZevend.Count)
                        {

                            OColArtikujtZevendesues[i].IdArtikulliZevendesues = colArtikujtZevend[i].IdArtikulliZevendesues;
                            mesazh = dbInv.modifikoArtikulliZevendesues(OColArtikujtZevendesues[i].IdArtikulliZevendesues, OColArtikujtZevendesues[i].IdArtikulliKryesor, OColArtikujtZevendesues[i].IdArtikulliZevend, OColArtikujtZevendesues[i].Prioriteti);
                            //mesazh=  modifikoArtikulliZevendesues(artikulli.OColArtikujtZevendesues[i]);
                        }
                        else
                        {
                            int idoC;
                            mesazh = dbInv.ruajArtikulliZevendesues(out idoC, OColArtikujtZevendesues[i].IdArtikulliKryesor, OColArtikujtZevendesues[i].IdArtikulliZevend, OColArtikujtZevendesues[i].Prioriteti);
                            //mesazh=    ruajArtikulliZevendesues(artikulli.OColArtikujtZevendesues[i]);
                        }
                        if (!mesazh.Status)
                        {

                            return mesazh;
                        }
                    }
                }
                else//rasti kur jane fshire rreshta
                {
                    int count = 0;
                    for (int i = 0; i < colArtikujtZevend.Count; i++)
                    {

                        if (count < OColArtikujtZevendesues.Count)
                        {

                            OColArtikujtZevendesues[i].IdArtikulliKryesor = idArtikulli;

                            OColArtikujtZevendesues[i].IdArtikulliZevendesues = colArtikujtZevend[i].IdArtikulliZevendesues;
                            mesazh = dbInv.modifikoArtikulliZevendesues(OColArtikujtZevendesues[i].IdArtikulliZevendesues, OColArtikujtZevendesues[i].IdArtikulliKryesor, OColArtikujtZevendesues[i].IdArtikulliZevend, OColArtikujtZevendesues[i].Prioriteti);
                            //mesazh=    modifikoArtikulliZevendesues(artikulli.OColArtikujtZevendesues[i]);
                        }
                        else
                        {
                            mesazh = dbInv.fshiArtikulliZevendesues(colArtikujtZevend[i].IdArtikulliZevendesues);
                            //mesazh=   fshiArtikulliZevendesues(colArtikujtZevend[i]);
                        }
                        count++;
                        if (!mesazh.Status)
                        {

                            return mesazh;
                        }
                    }
                }

                if (oColArtikujPerberes != null && oColArtikujPerberes.Count > 0)
                {
                    // if (this.Klasa == 4) //klasa Perbere
                    //    mesazh = dbInv.fshiArtikujPerberes(idArtikulli);//fshihen perberesit e artikullit kryesor sepse do shtohen perberesit e rinj
                    // else
                    mesazh = dbInv.fshiArtikujPerberesSipasDates(idArtikulli, oColArtikujPerberes[0].DtNdryshimi);//fshihen perberesit e asaj date sepse do shtohen perberesit e rinj
                    if (!mesazh.Status)
                        return new clsMesazh(false, "Nje nga artikujt perberes nuk u ruajt!");
                }
                if (oColArtikujPerberes == null || oColArtikujPerberes.Count == 0)
                {///per rastet kur kalojme nje artikull te perbere ose prodhim ne artikull inventar,te pastokueshem ect fshijme te gjithe perberesit
                    mesazh = dbInv.fshiArtikujPerberes(idArtikulli);
                    if (!mesazh.Status)
                    {

                        return mesazh;
                    }
                }
                //mesazh= fshiArtikujPerberes(artikulli);
                if (oColArtikujPerberes != null)
                    foreach (DbCore.DbInventari.clsArtikulliPerberes o in oColArtikujPerberes)
                    {
                        int idP;
                        //dr[2] = idArtikulli;
                        //if (int.Parse(dr[1].ToString()) != 0)
                        //    mesazh = dbInv.ruajArtikullPerbere(out idP, int.Parse(dr[1].ToString()), int.Parse(dr[2].ToString()), int.Parse(dr[3].ToString()), int.Parse(dr[4].ToString()), decimal.Parse(dr[5].ToString()));
                        o.IdArtikulliKryesor = idArtikulli;
                        if (o.Lloji != 0)
                        {
                            mesazh = dbInv.ruajArtikullPerbere(out idP, o.Lloji, o.IdArtikulliKryesor, o.IdLidheseArt, o.Koeficienti, o.Scrap, o.IdLidheseAkt, o.GjithmoneNgaStoku, o.DtNdryshimi);
                            if (!mesazh.Status)
                            {

                                return mesazh;
                            }
                        }
                    }

                if (oColGjendjeArtikulli != null)
                {
                    mesazh = dbInv.fshiGjendjeArtikulli(idArtikulli);
                    if (!mesazh.Status)
                        return new clsMesazh(false, "Nje nga artikujt  nuk u ruajt!");

                    foreach (DbCore.DbInventari.clsGjendjeArtikulli o in oColGjendjeArtikulli)
                    {
                        int idP;
                        o.IdArtikulli = idArtikulli;
                        mesazh = dbInv.ruajGjendjeArtikulli(out idP, o.IdArtikulli, o.IdMagazina, o.GjendjaMin, o.GjendjaMax);
                        if (!mesazh.Status)
                        {

                            return mesazh;
                        }

                    }
                }

                clsArtikullPerberesTemplateKoka templateKoka = new clsArtikullPerberesTemplateKoka(idTempKoka, kodTempKoka, pershkrimTempKoka, idNdermarje);
                if (kodTempKoka != null && kodTempKoka != "")
                {
                    mesazh = dbInv.ruajTemplateArtikullPerberesKoka(out idTempKoka, kodTempKoka, pershkrimTempKoka, idNdermarje);
                    if (!mesazh.Status)
                    {

                        return mesazh;
                    }
                    //mesazh=  ruajTemplateArtikullPerberesKoka(templateKoka);
                    foreach (DbCore.DbInventari.clsArtikulliPerberesTemplateTrupi o in templateKoka.oColTrupi)
                    {
                        o.IdKoka = templateKoka.IdKoka;
                        int idT;
                        mesazh = dbInv.ruajTemplateArtikullPerberesTrupi(out idT, o.IdKoka, o.Lloji, o.IdLidheseArt, o.Koeficienti, o.Vlera, o.IdLidheseLlog);
                        //mesazh=  ruajTemplateArtikullPerberesTrupi(o);
                        if (!mesazh.Status)
                        {

                            return mesazh;
                        }
                    }
                }
                DbAsete.clsDatabazeAsete dbasete = new DbAsete.clsDatabazeAsete(dbInv);
                foreach (DbAsete.clsAseteNormaAmortizimiAbstract grup in colNorma)
                {
                    DbAsete.clsAseteNormaAmortizimi grupekzistues = new DbAsete.clsAseteNormaAmortizimi();
                    grupekzistues.merrArtikullNormaAmortizimiSipasIDArtikullStandart(this.idArtikulli, grup.IdStandartAmortizimi, grup.DtAktivizimi);
                    if (grupekzistues.IdLidhjeArtikullLlojAmort > 0 && grupekzistues.Norme == grup.Norme && grupekzistues.IdLlojAmortizimi == grup.IdLlojAmortizimi && grup.NormeMagazine == grupekzistues.NormeMagazine)//nqs nuk kemi ndryshime nuk do ruhet
                        continue;
                    int idgrup = dbasete.ktheIDArtikullNormaAmortizimiSipasIDArtikullStandart(this.idArtikulli, grup.IdStandartAmortizimi, grup.DtAktivizimi);
                    if (idgrup > 0)
                    {
                        mesazh = dbasete.modifikimiArtikulliNormaAmortizimi(idgrup, grup.IdLlojAmortizimi, grup.NormeMagazine, grup.Norme);
                    }
                    else
                        mesazh = dbasete.ruajArtikulliNormaAmortizimi(out idgrup, this.idArtikulli, grup.IdLlojAmortizimi, grup.IdStandartAmortizimi, grup.NormeMagazine, grup.Norme, grup.DtAktivizimi);
                    if (!mesazh.Status)
                        return mesazh;
                }
                DbAsete.clsDatabazeAseteRezerva dbrez = new clsDatabazeAseteRezerva(dbInv);
                foreach (DbAsete.clsAseteNormaAmortizimiAbstract grup in colNormaRezerva)
                {
                    DbAsete.clsAseteNormaAmortizimiAbstract grupekzistues = new DbAsete.clsNormaAmortizimiRezerva();
                    grupekzistues.merrArtikullNormaAmortizimiSipasIDArtikullStandart(this.idArtikulli, grup.IdStandartAmortizimi, grup.DtAktivizimi);
                    if (grupekzistues.IdLidhjeArtikullLlojAmort > 0 && grupekzistues.Norme == grup.Norme && grupekzistues.IdLlojAmortizimi == grup.IdLlojAmortizimi && grup.NormeMagazine == grupekzistues.NormeMagazine)//nqs nuk kemi ndryshime nuk do ruhet
                        continue;
                    int idgrup = dbrez.ktheIDArtikullNormaAmortizimiSipasIDArtikullStandart(this.idArtikulli, grup.IdStandartAmortizimi, grup.DtAktivizimi);
                    if (idgrup > 0)
                    {
                        mesazh = dbrez.modifikimiArtikulliNormaAmortizimi(idgrup, grup.IdLlojAmortizimi, grup.NormeMagazine, grup.Norme);
                    }
                    else
                        mesazh = dbrez.ruajArtikulliNormaAmortizimi(out idgrup, this.idArtikulli, grup.IdLlojAmortizimi, grup.IdStandartAmortizimi, grup.NormeMagazine, grup.Norme, grup.DtAktivizimi);
                    if (!mesazh.Status)
                        return mesazh;
                }


                mesazh = new clsMesazh(true, IMBUtils.Messages.MessagesResource.Messages["msgModifikimiMeSukses"]);

                return mesazh;
            }
            catch (Exception ce)
            {

                return new clsMesazh(false, ce.Message);
            }
        }

        public clsMesazh RuajDetajime(clsDatabaseInventari dbInv, int idArtikulli, colDetajimePerArt colDetajimeEkzistuese, colDetajimePerArt oColDetajime, int llojDetajim)
        {
            clsMesazh mesazh = new clsMesazh(true);
            for (int i = 0; i < oColDetajime.Count; i++)
            {
                clsDetajimPerArt det = oColDetajime[i];
                clsDetajimPerArt lidhjeNjejte = colDetajimeEkzistuese.Find(x => x.IdDetajimArtikulli == det.IdDetajimArtikulli);
                if (lidhjeNjejte != null)
                {   //i heqim nga collectioni barkodet qe nuk jane ndryshuar, sepse ne te do ngelen vetem kodbaret qe do te fshihen
                    colDetajimeEkzistuese.Remove(lidhjeNjejte);
                    continue;
                }
                mesazh = dbInv.ruajDetajimArt(idArtikulli, det.IdDetajimArtikulli, llojDetajim);
                if (!mesazh.Status)
                    return mesazh;
            }

            //fshihen barkodet qe jane hequr nga artikulli
            for (int j = 0; j < colDetajimeEkzistuese.Count; j++)
            {
                int idDetajimArtikulli = colDetajimeEkzistuese[j].IdDetajimArtikulli;
                mesazh = dbInv.fshiDetajimArt(colDetajimeEkzistuese[j].IdDetajimArt);
                if (!mesazh.Status)
                    return mesazh;
            }
            return mesazh;
        }

        public clsMesazh transfero(object[] idartikulli, List<object> idndermarje, int idperdoruesi, int idGjuha, int idNdermOrigjine)
        {
            using (var scope = new MyTransactionScope())
            {
                clsDatabaseInventari dbinve = new clsDatabaseInventari();
                try
                {

                    clsMesazh mesazh = transfero(idartikulli, idndermarje, idperdoruesi, idGjuha, dbinve, idNdermOrigjine);
                    if (mesazh.Status)
                    {
                        scope.Complete();
                        return mesazh;
                    }
                    return mesazh;
                }
                catch (Exception ex)
                {
                    return new clsMesazh(false, ex.Message);
                }
            }

        }

        public clsMesazh transfero(object[] idartikulli, List<object> idndermarje, int idperdoruesi, int idGjuha, DbInventari.clsDatabaseInventari dbinv, int idNdermOrigjine)
        {
            var listMesazhesh = new List<clsMesazh>();
            clsMesazh mesazh = new clsMesazh(true, "Transferimi mbaroi me sukses!");
            for (int i = 0; i < idartikulli.Length; i++)
            {
                clsArtikulli art = new clsArtikulli(int.Parse(idartikulli[i].ToString()), dbinv);
                if (!art.perTransferim)
                    continue;
                colCmimeArtikujsh cmime = new colCmimeArtikujsh();
                cmime.mbushCmimArtikulliSipasArtikullit(art.idArtikulli, art.idNdermarje, dbinv);
                art.colArtikujPerberes = new colArtikulliPerberes();
                art.colArtikujPerberes.merrSipasIdArtikullKryesore(art.idArtikulli, dbinv);
                art.oColKodbare = art.mbushKodBare();
                art.colArtikujVfone = new colArtikullVfone();
                art.colArtikujVfone.merrSipasIdArtikull(art.IdArtikulli, dbinv);
                art.colNorma = new colAseteNormaAmortizimi();
                art.colNormaRezerva = new colNormaAmortizimiRezerva();
                art.oColDetajime = new colDetajimePerArt();
                art.oColDetajime2 = new colDetajimePerArt();
                foreach (object id in idndermarje)
                {
                    clsArtikulli artNderm = (clsArtikulli)art.Clone();
                    mesazh = kontrollotransferim(artNderm, int.Parse(id.ToString()), dbinv, idperdoruesi, cmime, idGjuha, idNdermOrigjine);
                    if (!mesazh.Status || mesazh.KodMesazhi == (int)KodMesazhi.ArtikulliJoNeOwn)
                        listMesazhesh.Add(mesazh);
                }
            }
            if (listMesazhesh.Any())
            {
                clsMesazh mesazhet = new clsMesazh(true, "");
                string kodeartikujsh = "";
                bool notfound = false;
                foreach (var mesazhi in listMesazhesh)
                {
                    if (mesazhi.KodMesazhi == (int)KodMesazhi.ArtikulliJoNeOwn)
                    {
                        notfound = true;
                        if (kodeartikujsh != "")
                            kodeartikujsh += ", ";
                        kodeartikujsh += mesazhi.PershkrimMesazhi;
                    }
                    else
                    {
                        mesazhet.PershkrimMesazhi += $"{mesazhi.PershkrimMesazhi} ";
                        mesazhet.Status = false;//statusi false vetem nese ka ndodhur ndonje error tjeter
                    }
                }
                if (notfound)
                    mesazhet.PershkrimMesazhi += $"Artikulli me kod {kodeartikujsh} nuk ekziston ne ndermarrjen e magazines. Nuk mund te trasferohen te dhenat e tij.";
                if (mesazhet.Status)
                    mesazhet.Tipi = TipMesazhi.Informim;//nese ska ndodhur asnje error tjeter atehere sdo beje rollback dhe do shfaqet si info artikujt qe nuk ekzistonin ne own.
                return mesazhet;
            }
            return mesazh;
        }

        private clsMesazh merrTeDhenaArtikulli(clsArtikulli art, int idndermarje, clsDatabaseInventari db, int idperdoruesi, colCmimeArtikujsh cmime, int idGjuha)
        {
            clsMesazh mesazh = new clsMesazh(true, "Transferimi mbaroi me sukses!");
            clsNjesiArtikulli njesi1 = new clsNjesiArtikulli(art.njesi1Artikulli, db);
            mesazh = njesi1.kontrollotransferim(njesi1, idndermarje, db, idperdoruesi);
            if (!mesazh.Status)
            {
                return mesazh;
            }
            if (art.njesi1Artikulli == art.njesi2Artikulli)
            {
                art.njesi1Artikulli = njesi1.IdNjesia;
                art.njesi2Artikulli = njesi1.IdNjesia;
            }
            else
            {
                art.njesi1Artikulli = njesi1.IdNjesia;
                clsNjesiArtikulli njesi2 = new clsNjesiArtikulli(art.njesi2Artikulli, db);
                mesazh = njesi2.kontrollotransferim(njesi2, idndermarje, db, idperdoruesi);
                if (!mesazh.Status)
                {

                    return mesazh;
                }
                art.njesi2Artikulli = njesi2.IdNjesia;

            }
            if (art.Kodifikimi1Artikulli > 0)
            {
                clsKodifikimArtikulli kodifikim1 = new clsKodifikimArtikulli(art.kodifikimi1Artikulli, db);
                mesazh = kodifikim1.kontrollotransferim(kodifikim1, idndermarje, db, idperdoruesi);
                if (!mesazh.Status)
                {

                    return mesazh;
                }
                art.kodifikimi1Artikulli = kodifikim1.IdKodifikimi;
            }
            if (art.Kodifikimi2Artikulli > 0)
            {
                clsKodifikimArtikulli kodifikim2 = new clsKodifikimArtikulli(art.kodifikimi2Artikulli, db);
                mesazh = kodifikim2.kontrollotransferim(kodifikim2, idndermarje, db, idperdoruesi);
                if (!mesazh.Status)
                {

                    return mesazh;
                }
                art.kodifikimi2Artikulli = kodifikim2.IdKodifikimi;
            }
            if (art.Kodifikimi3Artikulli > 0)
            {
                clsKodifikimArtikulli kodifikim3 = new clsKodifikimArtikulli(art.kodifikimi3Artikulli, db);
                mesazh = kodifikim3.kontrollotransferim(kodifikim3, idndermarje, db, idperdoruesi);
                if (!mesazh.Status)
                {

                    return mesazh;
                }
                art.kodifikimi3Artikulli = kodifikim3.IdKodifikimi;
            }
            if (art.IdTvsh > 0)
            {
                DbRegjistrim.clsDatabaseRegjistrim dbregj = new DbRegjistrim.clsDatabaseRegjistrim(db);
                DbRegjistrim.clsTaksa taksa = new DbRegjistrim.clsTaksa(art.IdTvsh, dbregj);
                mesazh = taksa.kontrollotransferim(taksa, idndermarje, dbregj, idperdoruesi);
                if (!mesazh.Status)
                {

                    return mesazh;
                }
                art.idTvsh = taksa.IdTaksa;

            }
            if (art.IdLlogariInventari > 0)
            {
                DbKontabiliteti.clsDatabaseKontabilitet dbkont = new clsDatabaseKontabilitet(db);
                clsLlogari llog = new clsLlogari(art.idLlogariInventari, dbkont);
                mesazh = llog.kontrollotransferim(llog, idndermarje, dbkont, idperdoruesi, idGjuha);
                if (!mesazh.Status)
                {

                    return mesazh;
                }
                art.idLlogariInventari = llog.IdLlogari;

            }
            if (art.idLlogariBlerje > 0)
            {
                DbKontabiliteti.clsDatabaseKontabilitet dbkont = new clsDatabaseKontabilitet(db);
                clsLlogari llog = new clsLlogari(art.idLlogariBlerje, dbkont);
                mesazh = llog.kontrollotransferim(llog, idndermarje, dbkont, idperdoruesi, idGjuha);
                if (!mesazh.Status)
                {

                    return mesazh;
                }
                art.idLlogariBlerje = llog.IdLlogari;

            }
            if (art.idLlogariShitje > 0)
            {
                DbKontabiliteti.clsDatabaseKontabilitet dbkont = new clsDatabaseKontabilitet(db);
                clsLlogari llog = new clsLlogari(art.idLlogariShitje, dbkont);
                mesazh = llog.kontrollotransferim(llog, idndermarje, dbkont, idperdoruesi, idGjuha);
                if (!mesazh.Status)
                {

                    return mesazh;
                }
                art.idLlogariShitje = llog.IdLlogari;

            }
            if (art.idLlogariTeTrete > 0)
            {
                DbKontabiliteti.clsDatabaseKontabilitet dbkont = new clsDatabaseKontabilitet(db);
                clsLlogari llog = new clsLlogari(art.idLlogariTeTrete, dbkont);
                mesazh = llog.kontrollotransferim(llog, idndermarje, dbkont, idperdoruesi, idGjuha);
                if (!mesazh.Status)
                {

                    return mesazh;
                }
                art.idLlogariTeTrete = llog.IdLlogari;

            }
            if (art.idLlogariShpenzime > 0)
            {
                DbKontabiliteti.clsDatabaseKontabilitet dbkont = new clsDatabaseKontabilitet(db);
                clsLlogari llog = new clsLlogari(art.idLlogariShpenzime, dbkont);
                mesazh = llog.kontrollotransferim(llog, idndermarje, dbkont, idperdoruesi, idGjuha);
                if (!mesazh.Status)
                {

                    return mesazh;
                }
                art.idLlogariShpenzime = llog.IdLlogari;

            }
            if (art.idLlogariAmortizimi > 0)
            {
                DbKontabiliteti.clsDatabaseKontabilitet dbkont = new clsDatabaseKontabilitet(db);
                clsLlogari llog = new clsLlogari(art.idLlogariAmortizimi, dbkont);
                mesazh = llog.kontrollotransferim(llog, idndermarje, dbkont, idperdoruesi, idGjuha);
                if (!mesazh.Status)
                {

                    return mesazh;
                }
                art.idLlogariAmortizimi = llog.IdLlogari;

            }

            if (art.IdLlogariPakesim > 0)
            {
                DbKontabiliteti.clsDatabaseKontabilitet dbkont = new clsDatabaseKontabilitet(db);
                clsLlogari llog = new clsLlogari(art.IdLlogariPakesim, dbkont);
                mesazh = llog.kontrollotransferim(llog, idndermarje, dbkont, idperdoruesi, idGjuha);
                if (!mesazh.Status)
                {

                    return mesazh;
                }
                art.idLlogariPakesim = llog.IdLlogari;

            }
            if (art.idSkemaKontabilitetiArtikulli > 0)
            {

                clsSkemaKontabilitetiArtikulli llog = new clsSkemaKontabilitetiArtikulli(art.idSkemaKontabilitetiArtikulli, db);
                llog = new clsSkemaKontabilitetiArtikulli(llog.KodiSkemaKontabilitetiArtikulli, idndermarje, false, db);
                art.idSkemaKontabilitetiArtikulli = llog.IdSkemaKontabilitetiArtikulli;

            }
            DbShare.clsKonfigurimAmbjenti konf = new DbShare.clsKonfigurimAmbjenti();
            DbShare.clsDatabaseShare dbshare = new DbShare.clsDatabaseShare(db);

            konf.mbushKonfigAmbjSipasKod("ART", idndermarje, dbshare);
            art.idKonfig = konf.IdKonfigAmbjente;


            foreach (clsArtikulliPerberes per in art.colArtikujPerberes)
            {
                if (per.Lloji == 1)
                {
                    clsArtikulli artper = new clsArtikulli(per.IdLidheseArt, db);
                    colCmimeArtikujsh cmimeartper = new colCmimeArtikujsh();
                    cmimeartper.mbushCmimArtikulliSipasArtikullit(artper.idArtikulli, idndermarje, db);
                    //colKodbare kodbareper = new colKodbare();
                    //kodbareper.mbushKodbarinSipasIdArtikulli(artper.idArtikulli, db);
                    //artper.kodbari = "";
                    //foreach (clsKodbari k in kodbareper)
                    //{
                    //    artper.kodbari += k.Pershkrimi + ",";

                    //}
                    //if (artper.kodbari.Length > 0)
                    //    artper.kodbari = artper.kodbari.Substring(0, artper.kodbari.Length - 1);

                    artper.oColKodbare = artper.mbushKodBare(db);
                    artper.colArtikujPerberes = new colArtikulliPerberes();
                    artper.colArtikujPerberes.merrSipasIdArtikullKryesore(artper.idArtikulli, db);

                    artper.oColDetajime = new colDetajimePerArt();
                    artper.oColDetajime.mbushDetajimArtSipasIdArtikulliDheLlojit(artper.idArtikulli, 1, db);
                    artper.oColDetajime2 = new colDetajimePerArt();
                    artper.oColDetajime2.mbushDetajimArtSipasIdArtikulliDheLlojit(artper.idArtikulli, 2, db);
                    mesazh = kontrollotransferim(artper, idndermarje, db, idperdoruesi, cmimeartper, idGjuha, artper.idNdermarje);
                    if (!mesazh.Status)
                    {

                        return mesazh;
                    }

                    per.IdLidheseArt = artper.idArtikulli;
                }
                else if (per.Lloji == 2)
                {
                    DbProdhimi.clsDatabazeProdhimi dbprodh = new DbProdhimi.clsDatabazeProdhimi(db);
                    DbProdhimi.clsAktiviteteKoka akt = new DbProdhimi.clsAktiviteteKoka(per.IdLidheseAkt, dbprodh);
                    akt.ColTrupi = new DbProdhimi.colAktiviteteTrupi(akt.IdKoka, dbprodh);
                    mesazh = akt.kontrollotransferim(akt, idndermarje, dbprodh, idperdoruesi);
                    if (!mesazh.Status)
                    {

                        return mesazh;
                    }

                    per.IdLidheseAkt = akt.IdKoka;
                }

            }
            foreach (clsCmimArtikulli cmim in cmime)
            {
                cmim.IdArtikulli = art.IdArtikulli;
                mesazh = clsCmimArtikulli.pergatiCmim(cmim, idndermarje, db, idperdoruesi);
                cmim.IdCmimArtikulli = db.merrCmimArtikull(cmim.IdArtikulli, cmim.IdNivelCmimi);
                if (!mesazh.Status)
                    return mesazh;
            }
            art.idmagazina = 0;
            art.idObjektivaKosto = 0;
            art.idFurnitoriKryesor = 0;
            art.idPerdoruesi = idperdoruesi;
            art.idNdermarje = idndermarje;
            art.autorizimet = "";
            return mesazh;
        }

        private clsMesazh modifikoArtNeOwn(clsArtikulli art, clsArtikulli artnderm, clsDatabaseInventari db, int idperdoruesi)
        {
            clsMesazh mesazh = new clsMesazh(true, "Transferimi mbaroi me sukses!");
            //duhet modifikuar vetem: Loan,Artikull dhurate,
            //Kod dhurate, Pike, Vlere (te ruajtura ne T_ARTIKULLVFONE),
            //Aparat bazaar, Kod oferte,Stoku max per VFONE, Gjendja max,Grupimi 1,Grupimi 2,Lloji i garancise,Garancia
            //art eshte artikulli me modifikimet dhe artnderm eshte artikulli tek ndermarrja i cili do te sinkronizohet
            if (art.Kodifikimi1Artikulli > 0)
            {
                clsKodifikimArtikulli kodifikim1 = new clsKodifikimArtikulli(art.kodifikimi1Artikulli, db);
                mesazh = kodifikim1.kontrollotransferim(kodifikim1, artnderm.idNdermarje, db, idperdoruesi);
                if (!mesazh.Status)
                {
                    return mesazh;
                }
                art.kodifikimi1Artikulli = kodifikim1.IdKodifikimi;
            }
            if (art.Kodifikimi2Artikulli > 0)
            {
                clsKodifikimArtikulli kodifikim2 = new clsKodifikimArtikulli(art.kodifikimi2Artikulli, db);
                mesazh = kodifikim2.kontrollotransferim(kodifikim2, artnderm.idNdermarje, db, idperdoruesi);
                if (!mesazh.Status)
                {
                    return mesazh;
                }
                art.kodifikimi2Artikulli = kodifikim2.IdKodifikimi;
            }

            mesazh = db.modifikoArt(art.idArtikulli, artnderm.kodArtikulli, artnderm.pershkrimArtikulli, artnderm.pershkrimiAngArtikulli, artnderm.kodiDoganorArtikulli, artnderm.vendodhjeArtikulli, art.kodifikimi1Artikulli, art.kodifikimi2Artikulli, artnderm.origjineArtikulli, artnderm.njesi1Artikulli, artnderm.njesi2Artikulli, artnderm.koeficientArtikulli, artnderm.idFurnitoriKryesor, artnderm.peshaBrutoArtikulli, artnderm.peshaNetoArtikulli, artnderm.detajimArtikulli, artnderm.klasa, artnderm.idSkemaKontabilitetiArtikulli, artnderm.idLlogariInventari, artnderm.idLlogariBlerje, artnderm.idLlogariShitje, artnderm.idLlogariTeTrete, artnderm.idLlogariShpenzime, artnderm.idLlogariAmortizimi, artnderm.idLlogariPakesim, artnderm.idLlogRez, artnderm.idLlogPakRez, artnderm.minimumArtikulli, art.maximumArtikulli, artnderm.metodeKostojeArtikulli, artnderm.llogaritjaKMSHArtikulli, artnderm.zevendesimAutomatikArtikulli, artnderm.idPerdoruesi, artnderm.idNdermarje, artnderm.kontrollGjendje, artnderm.kontrollCmimPerDetajim, artnderm.kontrollGjendjeArtikulli, artnderm.idTvsh, artnderm.idKonfig, artnderm.aktiv, artnderm.idStatusDok, artnderm.llojiArt, artnderm.sasiNjesi, artnderm.scrap, artnderm.prodhimMePorosi, artnderm.idKategoriDetajimi, artnderm.IdKategoriDetajimi2, artnderm.kontrollGjendjeDetajim2, artnderm.idObjektivaKosto, art.idllojGarancie, art.garancia, artnderm.idmagazina, artnderm.iRezervueshem, artnderm.perTransferim, art.loan, art.dhurate, artnderm.aplikimDhurate, artnderm.pike, artnderm.vlere, artnderm.kodVFOne, artnderm.meSerial, artnderm.iShitshem, artnderm.mbetjeShitshme, artnderm.idArtRaportuesi, artnderm.perPeshore, artnderm.pershkrimFurnitori, artnderm.siperfaqjaM2, artnderm.nrKontrate, artnderm.nrPasurie, artnderm.zonaKadastrale, artnderm.shasia, artnderm.marka, artnderm.modeli, artnderm.vitProdhimi, artnderm.teDhenaTeknika, artnderm.meBarkodLogjik, artnderm.skemaBarkodit, artnderm.kodifikimi3Artikulli, art.aparatBazaar, art.kodOferte, artnderm.artikullIVjeter, artnderm.idFormatSeriali, artnderm.merezerverivleresimi, artnderm.nrKaraktereTAC, artnderm.llogaritKomision, artnderm.idLlogariKomision, art.stokuMaxVfOne, artnderm.kodiiBarit, artnderm.iRimbursueshem);
            if (!mesazh.Status)
            {
                return mesazh;
            }

            mesazh = db.fshiArtikullVfone(art.idArtikulli);
            if (!mesazh.Status)
                return mesazh;

            if (art.colArtikujVfone != null)
                foreach (DbCore.DbInventari.clsArtikullVfone o in art.colArtikujVfone)
                {
                    int idP;
                    o.IdArtikulli = art.idArtikulli;
                    mesazh = db.ruajArtikullVfone(out idP, o.IdArtikulli, o.KodVfone, o.Pike, o.Vlere);
                    if (!mesazh.Status)
                    {
                        return mesazh;
                    }
                }
            return mesazh;
        }

        private clsMesazh kontrollotransferim(clsArtikulli art, int idndermarje, clsDatabaseInventari db, int idperdoruesi, colCmimeArtikujsh cmime, int idGjuha, int idNdermOrigjine)
        {
            clsMesazh mesazh = new clsMesazh(true, "Transferimi mbaroi me sukses!");
            clsNdermarrje nderm = new clsNdermarrje(idndermarje);
            if (!db.ekzistonArtikull(art.kodArtikulli, idndermarje))
            {
                if (nderm.OwnShop)
                    return new clsMesazh((int)KodMesazhi.ArtikulliJoNeOwn, true, art.kodArtikulli);//artikulli nuk ekziston ne ndermarrjen magazine.
                clsMesazh result = merrTeDhenaArtikulli(art, idndermarje, db, idperdoruesi, cmime, idGjuha);
                if (!result.Status)
                    return result;
                mesazh = art.ruaj(cmime, new clsArtikullPerberesTemplateKoka(), db, idNdermOrigjine);
                if (!mesazh.Status)
                    return mesazh;
            }
            else
            {
                clsArtikulli artnderm = new clsArtikulli();
                artnderm.merrSipasKodArtikullit(art.kodArtikulli, idndermarje, db);
                art.idArtikulli = artnderm.idArtikulli;
                DateTime dtmod = art.dtModifikimi == new DateTime() ? art.dtKrijimi : art.dtModifikimi;
                if ((artnderm.dtModifikimi - dtmod).Duration() > TimeSpan.FromSeconds(30) || artnderm.dtModifikimi == new DateTime())
                {
                    if (nderm.OwnShop)
                    {
                        mesazh = modifikoArtNeOwn(art, artnderm, db, idperdoruesi);
                    }
                    else
                    {
                        clsMesazh result = merrTeDhenaArtikulli(art, idndermarje, db, idperdoruesi, cmime, idGjuha);
                        if (!result.Status)
                            return result;
                        art.oColDetajime = new colDetajimePerArt();
                        art.oColDetajime.mbushDetajimArtSipasIdArtikulliDheLlojit(art.idArtikulli, 1, db);// merren detajimet ekzistuese
                        art.oColDetajime2 = new colDetajimePerArt();
                        art.oColDetajime2.mbushDetajimArtSipasIdArtikulliDheLlojit(art.idArtikulli, 2, db);
                        art.oColKodbare = new colKodbare(art.idArtikulli);//marrim kodbaret qe mund te kete ne ndermarrjet bija se perndryshe do tentoje ti fshije
                        mesazh = art.modifiko(new clsArtikullPerberesTemplateKoka(), cmime, db);
                    }
                    if (!mesazh.Status)
                        return mesazh;
                }
            }
            return new clsMesazh(true, "Transferimi mbaroi me sukses!");
        }

        public clsMesazh fshi()
        {
            using (var scope = new MyTransactionScope())
            {
                clsDatabaseInventari data = new clsDatabaseInventari();
                clsMesazh u_fshi = data.fshiArtStatus(IdArtikulli, IdPerdoruesi);
                if (!u_fshi) return u_fshi;
                u_fshi = DbShare.colArkiva.UpdateStatusDokFshi(idArtikulli, 13, IdPerdoruesi);
                if (!u_fshi) return u_fshi;

                DbAsete.clsAQTSeriale cls = new DbAsete.clsAQTSeriale();
                DbAsete.clsDatabazeAsete db = new DbAsete.clsDatabazeAsete(data);
                u_fshi = cls.fshiSipasArtikullit(db, this.idArtikulli, this.idPerdoruesi);
                if (!u_fshi.Status) return u_fshi;
                scope.Complete();
                return u_fshi;
            }
        }
        /// <summary>
        /// Merr objektin llogari nga tabela perkatese ne databaze.Therret funksionin:
        /// <see cref="DbCore.DbKontabiliteti.clsDatabaseKontabilitet.merrLlogariSipasIdNenLlojLlog"/> 
        /// </summary>
        /// <param name="idNenLlojLlogarie">id e nenllojit te llogarise qe tregon se ke llogari te artikullit do te marrim</param>
        /// <returns>kthen nje objekt clsLlogari me llogarine e artikullit</returns>
        public clsLlogari merrLlogariArtikulli(int idNenLlojLlogarie)
        {
            return new clsLlogari(idNenLlojLlogarie, this.idArtikulli);
        }
        /// <summary>
        /// Merr objektin llogari nga tabela perkatese ne databaze.Therret funksionin:
        /// <see cref="DbCore.DbKontabiliteti.clsDatabaseKontabilitet.merrLlogariSipasIdNenLlojLlog"/> 
        /// </summary>
        /// <param name="idNenLlojLlogarie">id e nenllojit te llogarise qe tregon se ke llogari te artikullit do te marrim</param>
        /// <param name="db"></param>
        /// <returns>kthen nje objekt clsLlogari me llogarine e artikullit</returns>
        public clsLlogari merrLlogariArtikulli(int idNenLlojLlogarie, clsDatabaseKontabilitet db)
        {
            return merrLlogariArtikulli(idNenLlojLlogarie, this.idArtikulli, db);
        }

        public static clsLlogari merrLlogariArtikulli(int idNenLlojLlogarie, int idArtikulli, clsDatabaseKontabilitet db)
        {
            return new clsLlogari(idNenLlojLlogarie, idArtikulli, db);
        }

        public clsLlogari merrLlogariArtikulli(string kodNenLlojLlogarie, clsDatabaseKontabilitet db)
        {
            int idLlogari;
            switch (kodNenLlojLlogarie)
            {
                case "LLSH":
                    idLlogari = this.idLlogariShitje;
                    if (idLlogari == 0)
                        throw new MyException("Mungon llogaria e shitjes (LLSH) per artikullin me kod: " + this.kodArtikulli);
                    break;
                case "LLBL":
                    idLlogari = this.idLlogariBlerje;
                    if (idLlogari == 0)
                        throw new MyException("Mungon llogaria e blerjes (LLBL) per artikullin me kod: " + this.kodArtikulli);
                    break;
                case "LLINV":
                    idLlogari = this.idLlogariInventari;
                    if (idLlogari == 0)
                        throw new MyException("Mungon llogaria e inventarit (LLINV) per artikullin me kod: " + this.kodArtikulli);
                    break;
                case "LLT":
                    idLlogari = this.idLlogariTeTrete;
                    if (idLlogari == 0)
                        throw new MyException("Mungon llogaria te Trete (LLT) per artikullin me kod: " + this.kodArtikulli);
                    break;
                case "LLSHP":
                    idLlogari = this.idLlogariShpenzime;
                    if (idLlogari == 0)
                        throw new MyException("Mungon llogaria e shpenzimeve (LLSHP) per artikullin me kod: " + this.kodArtikulli);
                    break;
                case "LLA":
                    idLlogari = this.idLlogariAmortizimi;
                    if (idLlogari == 0)
                        throw new MyException("Mungon llogaria e amortizimit (LLA) per artikullin me kod: " + this.kodArtikulli);
                    break;
                case "LLPD":
                    idLlogari = this.idLlogariPakesim;
                    if (idLlogari == 0)
                        throw new MyException("Mungon llogaria e pakesimit (LLPD) per artikullin me kod:  " + this.kodArtikulli);
                    break;
                case "LLR":
                    idLlogari = this.IdLlogRez;
                    if (idLlogari == 0)
                        throw new MyException("Mungon llogaria rezerve (LLR) per artikullin me kod: " + this.kodArtikulli);
                    break;
                case "LLPR":
                    idLlogari = this.IdLlogPakRez;
                    if (idLlogari == 0)
                        throw new MyException("Mungon llogaria e pakesimit rezerve (LLPR) per artikullin me kod:  " + this.kodArtikulli);
                    break;
                default:
                    throw new MyException("Lloj i panjohur kodNenLlojLlogarie: " + kodNenLlojLlogarie);
            }
            return new clsLlogari(idLlogari, db);
            //return transactionCache.getLlogariFromCache(myColLlogari, idLlogari, db);
        }

        /// <summary>
        /// merr artikullin sipas kodit te artikullit dhe idndermarrjes
        /// </summary>
        /// <param name="kodArtikulli">String i kod artikullit</param>
        /// <param name="idnderm">int i id-se se ndermarrjes</param>
        /// <param name="dbInventari"></param>
        public bool merrSipasKodArtikullit(string kodArtikulli, int idnderm, clsDatabaseInventari dbInventari)
        {
            if (kodArtikulli == null || kodArtikulli == "")
                return false;
            return mbushArtikull(dbInventari.ktheArtikullSipasKodit(kodArtikulli, idnderm));
        }

        public bool ktheArtikullSipasKoditDheAutorizime(string kodArtikulli, int idnderm, int idperdorues, clsDatabaseInventari dbInventari)
        {
            return mbushArtikull(dbInventari.ktheArtikullSipasKoditDheAutorizime(kodArtikulli, idnderm, idperdorues));
        }

        /// <summary>
        /// merr artikullin sipas kodit te artikullit dhe idndermarrjes
        /// </summary>
        /// <param name="kodArtikulli">String i kod artikullit</param>
        /// <param name="idnderm">int i id-se se ndermarrjes</param>        
        public bool merrSipasKodArtikullit(string kodArtikulli, int idnderm)
        {
            if (kodArtikulli == null || kodArtikulli == "")
                return false;
            clsDatabaseInventari dbInventari = new clsDatabaseInventari();
            try
            {
                dbInventari.beginTransaksion();
                bool sukses = merrSipasKodArtikullit(kodArtikulli, idnderm, dbInventari);
                if (!sukses)
                {
                    dbInventari.rollbackTransaksion();
                    return sukses;
                }
                dbInventari.commitTransaksion();
                return sukses;
            }
            catch (Exception)
            {
                dbInventari.rollbackTransaksion();
                return false;
            }
        }

        public bool ktheArtikullSipasKoditDheAutorizime(string kodArtikulli, int idnderm, int idperdorues)
        {
            clsDatabaseInventari dbInventari = new clsDatabaseInventari();
            try
            {

                dbInventari.beginTransaksion();
                bool sukses = ktheArtikullSipasKoditDheAutorizime(kodArtikulli, idnderm, idperdorues, dbInventari);
                if (!sukses)
                {
                    dbInventari.rollbackTransaksion();
                    return sukses;
                }
                dbInventari.commitTransaksion();
                return sukses;
            }
            catch (Exception)
            {
                dbInventari.rollbackTransaksion();
                return false;
            }

        }

        public bool merrSipasKodbarit(string kodbari, int idnderm)
        {
            using (clsDatabaseInventari dbInventari = new clsDatabaseInventari())
            {
                return mbushArtikull(dbInventari.ktheArtikullSipasKodbarit(kodbari, idnderm));
            }
        }

        public bool merrSipasKodbarit(string kodbari, int idnderm, clsDatabaseInventari dbInventari)
        {
            return mbushArtikull(dbInventari.ktheArtikullSipasKodbarit(kodbari, idnderm));
        }

        public bool ktheArtikullSipasDetajimit(string detajim, int idnderm)
        {
            using (clsDatabaseInventari dbInventari = new clsDatabaseInventari())
            {
                return mbushArtikull(dbInventari.ktheArtikullSipasDetajimit(detajim, idnderm));
            }
        }

        public static bool ekzistonDetajimBarkodArtikulli(string kodartikulli, int idndermarje, int llojdetajim, string koddetajimi)
        {
            using (clsDatabaseInventari db = new clsDatabaseInventari())
            {
                return ekzistonDetajimBarkodArtikulli(kodartikulli, idndermarje, llojdetajim, koddetajimi, db);
            }
        }

        public static bool ekzistonDetajimBarkodArtikulli(string kodartikulli, int idndermarje, int llojdetajim, string koddetajimi, clsDatabaseInventari db)
        {
            return db.ekzistonDetajimBarkodArtikulli(kodartikulli, idndermarje, llojdetajim, koddetajimi);
        }

        public static bool ekzistonArtikull(string kod, int idndermarje)
        {
            using (clsDatabaseInventari db = new clsDatabaseInventari())
            {
                return db.ekzistonArtikull(kod, idndermarje);
            }
        }

        public static bool ekzistonArtikull(string kod, int idndermarje, clsDatabaseInventari db)
        {

            return db.ekzistonArtikull(kod, idndermarje);

        }

        public static bool EshteMeDetajim(int idArtikulli)
        {
            using (clsDatabaseInventari db = new clsDatabaseInventari())
            {
                return db.EshteDetajim(idArtikulli);
            }
        }

        public static int ktheIdArtikulli(string kod, int idnderm)
        {
            using (clsDatabaseInventari dbartikullkodbar = new clsDatabaseInventari())
            {
                return (dbartikullkodbar.merrIdArtikull(kod, idnderm));
            }
        }

        /// <summary>
        /// mbush artikujt sipas kodit dhe id-se ndermarrjes
        /// </summary>
        /// <param name="kod">kodi i artikullit</param>
        /// <param name="idnderm">id e ndermarrjes</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        public bool mbushArtikull(string kod, int idnderm)
        {
            using (clsDatabaseInventari dbInventari = new clsDatabaseInventari())
            {
                return mbushArtikull(dbInventari.merrArtikull(kod, idnderm));
            }
        }

        public bool mbushArtikull(string kod, int idnderm, clsDatabaseInventari dbInventari)
        {
            return mbushArtikull(dbInventari.merrArtikull(kod, idnderm));
        }

        /// <summary>
        /// mbush artikulin sipas id-se 
        /// </summary>
        /// <param name="idArt">id e artikullit</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        public bool mbushArtikull(int idArt)
        {
            using (clsDatabaseInventari dbInventari = new clsDatabaseInventari())
            {
                return mbushArtikull(idArt, dbInventari);
            }
        }

        /// <summary>
        /// mbush artikulin sipas id-se 
        /// </summary>
        /// <param name="idArt">id e artikullit</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        public bool mbushArtikull(int idArt, clsDatabaseInventari dbInventari)
        {
            if (idArt == 0 || idArt == -1)
            {
                idArtikulli = 0;
                return false;
            }
            return mbushArtikull(dbInventari.merrArtikull(idArt));
        }

        public static bool eshteTransferuarTekBijArtikull(string kodartikulli, int idndermarje)
        {
            using (clsDatabaseInventari db = new clsDatabaseInventari())
            {
                return db.eshteTransferuarTekBijArtikull(kodartikulli, idndermarje);
            }
        }

        public static bool EshteMeSerial(int idArtikulli)
        {
            using (clsDatabaseInventari db = new clsDatabaseInventari())
            {
                return db.EshteMeSerial(idArtikulli);
            }
        }

        public ListeVleraInfo merrInfoArtSipasIdKokaDheVisibleVlera(int idPerdoruesi, DateTime data, string detajim, int idInfokoka, string detajim2, int idViti, int idklient, string mag, string njesiart, int idKarta = 0)
        {
            ListeVleraInfo lista = new ListeVleraInfo();
            if (idArtikulli <= 0)
                return lista;
            clsInfoKoka info = new clsInfoKoka(idInfokoka);
            if (info.IdPeriudha == 0)
            {
                data = data.ToLocalTime();
            }
            else
            {
                clsViti viti = new clsViti();
                viti.mbushVitetMet(idViti); //nderviti.IdViti
                data = viti.MbarimiViti;
            }
            int iddetajim = -1;
            int idDetajim2 = -1;
            if (detajim != "-1")
            {
                clsDetajimArtikulli det = new clsDetajimArtikulli();
                det.mbushDetajimArtikulli(detajim, idNdermarje);
                iddetajim = det.IdDetajimArtikulli;
            }
            if (detajim2 != "-1")
            {
                clsDetajimArtikulli det = new clsDetajimArtikulli();
                det.mbushDetajimArtikulli(detajim2, idNdermarje);
                idDetajim2 = det.IdDetajimArtikulli;
            }
            DbCore.DbRegjistrim.clsTrupiMagazina tr = new DbCore.DbRegjistrim.clsTrupiMagazina();
            System.Collections.ArrayList vlerat = new System.Collections.ArrayList();
            colInfoTrupi col = colInfoTrupi.merrInfoSipasIdKokaDheVisibleNew(idInfokoka, true, idNdermarje);
            lista.colInfoTrupi = col;
            lista.vlerat = vlerat;
            DataRow row = null;
            DataRow rowb = null;
            DataRow rowc = null;
            int Magzgjedhur = -1;
            double sasiPorositur = 0;
            double sasiGjitheMag = 0;
            bool readedSasiPorositur = false;
            bool readedSasiGjitheMag = false;
            double sasitot = 0;
            bool readedSasiTot = false;
            double sasimag, sasiamagzgjedhur;
            double cmmagzgjedhur = 0;
            int shifraPasPresjes = info.IdFormatNumri;
            double limitSasi, limitVlere, limitSasiMbetur, limitVlereMbetur;
            double sasiamagdet1zgjedhur; double sasiamagdet2zgjedhur;
            colArtikulliPerberes artPerberes = new colArtikulliPerberes();
            //kontrollohet nqs nje nga keto emra kolone jane ne collection dhe nqs po therritet vetem nje here sp qe ben marrjen e ketyre vlerave
            if (col.Any(x =>
                x.EmerKolone == "CmimiShitjesFunditTVSH" ||
                x.EmerKolone == "CmimiBlerjesFunditTVSH" ||
                x.EmerKolone == "CmimiShitjesFunditTVSH/Klient" ||
                x.EmerKolone == "CmimiBlerjesFunditTVSH/Klient"))
                rowc = clsKokaShitje.ktheCmiminFunditMeTVSHArtikullKlient(idArtikulli, data, idklient, idNdermarje);
            foreach (DbCore.DbAdmin.clsInfoTrupi trup in col)
            {
                try
                {
                    switch (trup.EmerKolone)
                    {
                        case "Periudha":
                            if (info.IdPeriudha == 0)
                                vlerat.Add("Data Fatures");
                            else
                                vlerat.Add("Viti Ushtrimor");
                            break;
                        case "Njesia":
                            vlerat.Add(KodNjesia1);
                            break;
                        case "Njesia2":
                            vlerat.Add(KodNjesia2);
                            break;
                        case "SasiaMax":
                            if (MaximumArtikulli != 0)
                                vlerat.Add(MaximumArtikulli.ToString("F" + shifraPasPresjes));
                            else vlerat.Add(MaximumArtikulli.ToString("F" + shifraPasPresjes));
                            break;
                        case "SasiaMin":
                            if (MinimumArtikulli != 0)
                                vlerat.Add(MinimumArtikulli.ToString("F" + shifraPasPresjes));
                            else vlerat.Add(MinimumArtikulli.ToString("F" + shifraPasPresjes));
                            break;
                        case "DataBlerjesFundit":
                            if (rowb == null)
                                rowb = DbCore.DbRegjistrim.clsKokaShitje.ktheShitjenFunditteArtikullit(idArtikulli, data, 2);//blerja e fundit
                            if (rowb != null)
                                vlerat.Add(DateTime.Parse(rowb[0].ToString()).ToShortDateString());
                            else
                                vlerat.Add("");
                            break;
                        case "CmimiBlerjesFundit":
                            if (rowb == null)
                                rowb = DbCore.DbRegjistrim.clsKokaShitje.ktheShitjenFunditteArtikullit(idArtikulli, data, 2);//blerja e fundit
                            if (rowb != null)
                                vlerat.Add(Convert.ToDouble(rowb[1]).ToString("F" + shifraPasPresjes));
                            else
                                vlerat.Add("");
                            break;
                        case "DataShitjesFundit":
                            if (row == null)
                                row = DbCore.DbRegjistrim.clsKokaShitje.ktheShitjenFunditteArtikullit(idArtikulli, data, 1);//shitja e fundit
                            if (row != null)
                            {
                                vlerat.Add(DateTime.Parse(row[0].ToString()).ToShortDateString());
                            }
                            else
                                vlerat.Add("");
                            break;
                        case "CmimiShtijesFundit":
                            if (row == null)
                                row = DbCore.DbRegjistrim.clsKokaShitje.ktheShitjenFunditteArtikullit(idArtikulli, data, 1);//shitja e fundit
                            if (row != null)
                                vlerat.Add(Convert.ToDouble(row[1]).ToString("F" + shifraPasPresjes));
                            else
                                vlerat.Add("");
                            break;
                        case "CmimiShitjesseFundit/Klient":
                            if (idklient != 0)
                            {
                                DataRow rowklient = DbCore.DbRegjistrim.clsKokaShitje.ktheShitjenFunditteArtikullitKlient(idArtikulli, data, 1, idklient);
                                if (rowklient != null)
                                    vlerat.Add(Convert.ToDouble(rowklient[1]).ToString("##########.##"));
                                else
                                    vlerat.Add("");
                                break;
                            }
                            else
                                vlerat.Add("");
                            break;
                        case "CmimiNivelitBaze":
                            if (njesiart == "")
                            {
                                vlerat.Add("");
                                break;
                            }
                            int idnjesia = clsNjesiArtikulli.ktheIdNjesiArtikulli(njesiart, IdNdermarje);
                            string cmimNivelBaze = clsArtikulli.ktheCmimArtikulliNivelBaze(idArtikulli, IdNdermarje, idnjesia);
                            vlerat.Add(cmimNivelBaze);
                            break;
                        case "CmimiBlerjesseFundit/Furnitor":
                            if (idklient != 0)
                            {
                                DataRow rowfurnitor = DbCore.DbRegjistrim.clsKokaShitje.ktheShitjenFunditteArtikullitKlient(idArtikulli, data, 2, idklient);
                                if (rowfurnitor != null)
                                    vlerat.Add(Convert.ToDouble(rowfurnitor[1]).ToString("F" + shifraPasPresjes));
                                else
                                    vlerat.Add("");
                            }
                            else
                                vlerat.Add("");
                            break;
                        case "Koeficienti":
                            vlerat.Add(koeficientArtikulli);
                            break;
                        case "SasiaTotale/Njesi2":
                            if (!readedSasiTot)
                            {
                                sasitot = (klasa == 4) ? 0 : DbCore.DbRegjistrim.clsTrupiMagazina.merrSasi(this, -1, data, -1);
                                readedSasiTot = true;
                            }
                            if (sasitot != 0)
                                vlerat.Add((sasitot / Convert.ToDouble(KoeficientArtikulli)).ToString("F" + shifraPasPresjes));
                            else vlerat.Add((sasitot / Convert.ToDouble(KoeficientArtikulli)).ToString("F" + shifraPasPresjes));
                            break;
                        case "Sasianemagazinenezgjedhur":
                            if (Magzgjedhur == -1)
                                Magzgjedhur = DbCore.DbRegjistrim.clsNjesiAdministrative.ktheIdMagazine(mag, idNdermarje, idPerdoruesi);
                            sasiamagzgjedhur = (this.Klasa == 4) ? 0 : DbCore.DbRegjistrim.clsTrupiMagazina.merrSasi(this, Magzgjedhur, data, -1);
                            if (sasiamagzgjedhur != 0)
                                vlerat.Add(sasiamagzgjedhur.ToString("F" + shifraPasPresjes));
                            else vlerat.Add(sasiamagzgjedhur.ToString("F" + shifraPasPresjes));
                            break;
                        case "Kostonemagazinenezgjedhur":
                            if (Magzgjedhur == -1)
                                Magzgjedhur = DbCore.DbRegjistrim.clsNjesiAdministrative.ktheIdMagazine(mag, idNdermarje, idPerdoruesi);
                            if (this.Klasa == 4)
                            {
                                if (artPerberes.Count == 0)
                                    artPerberes.ktheArtikujPerberesSipasIdArtikullitKryesorDheDatesMeTeAfert(this.IdArtikulli, data);
                                foreach (clsArtikulliPerberes art in artPerberes)
                                {
                                    //int tmpMetodeKostoje = clsArtikulli.ktheMetodeKostoje(art.IdLidheseArt);
                                    //decimal koef = clsArtikulli.ktheKoeficent(art.IdLidheseArt); 
                                    //cmmagzgjedhur += tr.llogaritCmimMesatarDetajimPare(art.IdLidheseArt, tmpMetodeKostoje, koef, Magzgjedhur, data, -1, 1) * (double)art.Koeficienti;
                                    clsArtikulli receptura = new clsArtikulli(art.IdLidheseArt);
                                    cmmagzgjedhur += tr.llogaritCmimMesatar(receptura, Magzgjedhur, data, -1, 1, idPerdoruesi) * (double)art.Koeficienti;
                                }
                            }
                            else
                                //cmmagzgjedhur = tr.llogaritCmimMesatar(this, Magzgjedhur, data, -1, 1);
                                cmmagzgjedhur = tr.llogaritCmimMesatar(this, Magzgjedhur, data, -1, 1, idPerdoruesi);
                            if (cmmagzgjedhur != 0)
                                vlerat.Add(cmmagzgjedhur.ToString("F" + shifraPasPresjes));
                            else vlerat.Add(cmmagzgjedhur.ToString("F" + shifraPasPresjes));
                            break;
                        case "SasiaTotale":
                            if (!readedSasiTot)
                            {
                                sasitot = (klasa == 4) ? 0 : DbCore.DbRegjistrim.clsTrupiMagazina.merrSasi(this, -1, data, -1);
                                readedSasiTot = true;
                            }
                            if (sasitot != 0)
                                vlerat.Add(sasitot.ToString("F" + shifraPasPresjes));
                            else vlerat.Add(sasitot.ToString("F" + shifraPasPresjes));
                            break;
                        case "KostoTotale":
                            double cmtot = 0;
                            if (klasa == 4)
                            {
                                colArtikulliPerberes per = new colArtikulliPerberes();
                                if (artPerberes.Count == 0)
                                    artPerberes.ktheArtikujPerberesSipasIdArtikullitKryesorDheDatesMeTeAfert(this.IdArtikulli, data);
                                foreach (clsArtikulliPerberes art in per)
                                {
                                    //cmtot += tr.llogaritCmimMesatarGjitheMag(new clsArtikulli(art.IdLidheseArt), data, -1, 1) * (double)art.Koeficienti;
                                    cmtot += tr.llogaritCmimMesatar(new clsArtikulli(art.IdLidheseArt), 0, data, -1, 1, idPerdoruesi) * (double)art.Koeficienti;
                                }
                            }
                            else
                            {
                                //cmtot = tr.llogaritCmimMesatarGjitheMag(this, data, -1, 1);
                                cmtot = tr.llogaritCmimMesatar(this, 0, data, -1, 1, idPerdoruesi);
                            }
                            if (cmtot != 0)
                                vlerat.Add(cmtot.ToString("F" + shifraPasPresjes));
                            else vlerat.Add(cmtot.ToString("F" + shifraPasPresjes));
                            break;
                        case "Kosto/Njesi2 totale":
                            double cmtotnjesi2 = 0;
                            if (klasa == 4)
                            {
                                colArtikulliPerberes per = new colArtikulliPerberes();
                                if (artPerberes.Count == 0)
                                    artPerberes.ktheArtikujPerberesSipasIdArtikullitKryesorDheDatesMeTeAfert(this.IdArtikulli, data);
                                foreach (clsArtikulliPerberes art in per)
                                {
                                    //cmtotnjesi2 += tr.llogaritCmimMesatarGjitheMag(new clsArtikulli(art.IdLidheseArt), data, -1, 1) * (double)art.Koeficienti;
                                    cmtotnjesi2 += tr.llogaritCmimMesatar(new clsArtikulli(art.IdLidheseArt), 0, data, -1, 1, idPerdoruesi, true) * (double)art.Koeficienti;
                                }
                            }
                            else
                            {
                                //cmtotnjesi2 = tr.llogaritCmimMesatarGjitheMag(this, data, -1, 1);
                                cmtotnjesi2 = tr.llogaritCmimMesatar(this, 0, data, -1, 1, idPerdoruesi, true);
                            }
                            if (cmtotnjesi2 != 0)
                                vlerat.Add((cmtotnjesi2 * Convert.ToDouble(KoeficientArtikulli)).ToString("F" + shifraPasPresjes));
                            else vlerat.Add((cmtotnjesi2 * Convert.ToDouble(KoeficientArtikulli)).ToString("F" + shifraPasPresjes));
                            break;
                        case "SasiPorositur":
                            if (!readedSasiPorositur)
                            {
                                sasiPorositur = DbCore.DbRegjistrim.clsTrupiShitje.merrSasiPorositur(idArtikulli);
                                readedSasiPorositur = true;
                            }
                            if (sasiPorositur != 0)
                                vlerat.Add(sasiPorositur.ToString("F" + shifraPasPresjes));
                            else vlerat.Add(sasiPorositur.ToString("F" + shifraPasPresjes));
                            break;
                        case "SasiRezervuar":
                            if (!readedSasiGjitheMag)
                            {
                                sasiGjitheMag = DbCore.DbRegjistrim.clsTrupiRezervime.merrSasiGjitheMagInfo(this, 0, data);
                                readedSasiGjitheMag = true;
                            }
                            if (sasiGjitheMag != 0)
                                vlerat.Add(sasiGjitheMag.ToString("F" + shifraPasPresjes));
                            else vlerat.Add(sasiGjitheMag.ToString("F" + shifraPasPresjes));
                            break;
                        case "Disponibel":
                            if (!readedSasiGjitheMag)
                            {
                                sasiGjitheMag = DbCore.DbRegjistrim.clsTrupiRezervime.merrSasiGjitheMagInfo(this, 0, data);
                                readedSasiGjitheMag = true;
                            }
                            if (!readedSasiTot)
                            {
                                sasitot = (klasa == 4) ? 0 : DbCore.DbRegjistrim.clsTrupiMagazina.merrSasi(this, -1, data, -1);
                                readedSasiTot = true;
                            }
                            if ((sasitot - sasiGjitheMag) != 0)
                                vlerat.Add((sasitot - sasiGjitheMag).ToString("F" + shifraPasPresjes));
                            else vlerat.Add((sasitot - sasiGjitheMag).ToString("F" + shifraPasPresjes));
                            break;
                        case "SasiLirePorositur":
                            if (!readedSasiPorositur)
                            {
                                sasiPorositur = DbCore.DbRegjistrim.clsTrupiShitje.merrSasiPorositur(idArtikulli);
                                readedSasiPorositur = true;
                            }
                            if (!readedSasiGjitheMag)
                            {
                                sasiGjitheMag = DbCore.DbRegjistrim.clsTrupiRezervime.merrSasiGjitheMag(this, 0, data);
                                readedSasiGjitheMag = true;
                            }
                            if ((sasiPorositur - sasiGjitheMag) != 0)
                                vlerat.Add((sasiPorositur - sasiGjitheMag).ToString("F" + shifraPasPresjes));
                            else vlerat.Add((sasiPorositur - sasiGjitheMag).ToString("F" + shifraPasPresjes));
                            break;
                        case "KodArtikulli":
                            vlerat.Add(KodArtikulli);
                            break;
                        case "Pershkrimi2":
                            vlerat.Add(PershkrimiAngArtikulli);
                            break;
                        case "PershkFurnitor":
                            vlerat.Add(PershkrimFurnitori);
                            break;
                        case "VendodhjeArtikulli":
                            vlerat.Add(VendodhjeArtikulli);
                            break;
                        case "FurnitoriKryesor":
                            var furnitorKryesor = new clsKlientFurnitor(IdFurnitoriKryesor);
                            vlerat.Add(furnitorKryesor == null ? "" : furnitorKryesor.EmertimiKF == null ? "" : furnitorKryesor.EmertimiKF);
                            break;
                        case "LimitSasi":
                            if (idKarta != 0)
                                limitSasi = DbCore.DbRegjistrim.clsLimitKarta.MerrLimitSasi(this.idArtikulli, idKarta, data);
                            else limitSasi = 0;
                            vlerat.Add(limitSasi.ToString("F" + shifraPasPresjes));
                            break;
                        case "LimitVlere":
                            if (idKarta != 0)
                                limitVlere = DbCore.DbRegjistrim.clsLimitKarta.MerrLimitVlere(this.idArtikulli, idKarta, data);
                            else limitVlere = 0;
                            vlerat.Add(limitVlere.ToString("F" + shifraPasPresjes));
                            break;
                        case "LimitSasiMbetur":
                            if (idKarta != 0)
                            {
                                limitSasiMbetur = DbCore.DbRegjistrim.clsLimitKarta.MerrLimitSasiMbetur(this.idArtikulli, idKarta, data);
                                if (limitSasiMbetur == -99999) limitSasiMbetur = 0;
                            }
                            else
                                limitSasiMbetur = 0;
                            vlerat.Add(limitSasiMbetur.ToString("F" + shifraPasPresjes));
                            break;
                        case "LimitVlereMbetur":
                            if (idKarta != 0)
                            {
                                limitVlereMbetur = DbCore.DbRegjistrim.clsLimitKarta.MerrLimitVlereMbetur(this.idArtikulli, idKarta, data);
                                if (limitVlereMbetur == -99999) limitVlereMbetur = 0;
                            }
                            else
                                limitVlereMbetur = 0;
                            vlerat.Add(limitVlereMbetur.ToString("F" + shifraPasPresjes));
                            break;
                        case "Sasiadetajim1nemagazinenezgjedhur":

                            if (Magzgjedhur == -1)
                                Magzgjedhur = DbCore.DbRegjistrim.clsNjesiAdministrative.ktheIdMagazine(mag, idNdermarje, idPerdoruesi);
                            if (iddetajim <= 0)
                                sasiamagdet1zgjedhur = 0;

                            else
                            {
                                sasiamagdet1zgjedhur = (this.Klasa == 4) ? 0 : DbCore.DbRegjistrim.clsTrupiMagazina.merrSasi(this, Magzgjedhur, data, iddetajim);
                            }
                            if (sasiamagdet1zgjedhur != 0)
                                vlerat.Add(sasiamagdet1zgjedhur.ToString("F" + shifraPasPresjes));
                            else vlerat.Add(sasiamagdet1zgjedhur.ToString("F" + shifraPasPresjes));
                            break;


                        case "Sasiadetajim2nemagazinenezgjedhur":

                            if (Magzgjedhur == -1)
                                Magzgjedhur = DbCore.DbRegjistrim.clsNjesiAdministrative.ktheIdMagazine(mag, idNdermarje, idPerdoruesi);
                            if (idDetajim2 <= 0)
                                sasiamagdet2zgjedhur = 0;

                            else
                            {
                                sasiamagdet2zgjedhur = (this.Klasa == 4) ? 0 : DbCore.DbRegjistrim.clsTrupiMagazina.merrSasiSipasDetajimit(this, Magzgjedhur, data, idDetajim2, 2);
                            }
                            if (sasiamagdet2zgjedhur != 0)
                                vlerat.Add(sasiamagdet2zgjedhur.ToString("F" + shifraPasPresjes));
                            else vlerat.Add(sasiamagdet2zgjedhur.ToString("F" + shifraPasPresjes));
                            break;

                        case "CmimiShitjesFunditTVSH":
                            if (rowc[0] != null && !String.IsNullOrEmpty(rowc[0].ToString()) && Convert.ToDouble(rowc[0]) != 0)
                                vlerat.Add(Convert.ToDouble(rowc[0]).ToString("F" + shifraPasPresjes));
                            else
                                vlerat.Add("");
                            break;

                        case "CmimiBlerjesFunditTVSH":
                            if (rowc[1] != null && !String.IsNullOrEmpty(rowc[1].ToString()) && Convert.ToDouble(rowc[1]) != 0)
                                vlerat.Add(Convert.ToDouble(rowc[1]).ToString("F" + shifraPasPresjes));
                            else
                                vlerat.Add("");
                            break;

                        case "CmimiShitjesFunditTVSH/Klient":
                            if (rowc[2] != null && !String.IsNullOrEmpty(rowc[2].ToString()) && Convert.ToDouble(rowc[2]) != 0)
                                vlerat.Add(Convert.ToDouble(rowc[2]).ToString("F" + shifraPasPresjes));
                            else
                                vlerat.Add("");
                            break;

                        case "CmimiBlerjesFunditTVSH/Furnitor":
                            if (rowc[3] != null && !String.IsNullOrEmpty(rowc[3].ToString()) && Convert.ToDouble(rowc[3]) != 0)
                                vlerat.Add(Convert.ToDouble(rowc[3]).ToString("F" + shifraPasPresjes));
                            else
                                vlerat.Add("");
                            break;
                        default:
                            string[] split = { "  -  " };
                            string kodmag = trup.PershkrimKolone.Split(split, StringSplitOptions.RemoveEmptyEntries)[1];
                            int idMag = DbCore.DbRegjistrim.clsNjesiAdministrative.ktheIdMagazine(kodmag, idNdermarje, idPerdoruesi);
                            if (idMag == -1)
                            {
                                vlerat.Add("jo autorizim");
                                break;
                            }
                            double cmmag = 0; double cmmagDet1 = 0; double cmmagDet2 = 0;
                            double sasimagDet1; double sasimagDet2;
                            double sasiarez; double sasiadisp; double sasiaporUB; double sasiarezUB; double sasialire;
                            if (trup.PershkrimKolone.Contains("Kosto"))
                            {
                                if (trup.PershkrimKolone.Contains("Kosto Detajim 1"))
                                {
                                    if (iddetajim <= 0)
                                    {
                                        cmmagDet1 = 1;
                                        if (cmmagDet1 != 0)
                                            vlerat.Add(cmmagDet1.ToString("F" + shifraPasPresjes));
                                        else vlerat.Add(cmmagDet1.ToString("F" + shifraPasPresjes));
                                        break;
                                    }
                                    if (this.Klasa == 4)
                                    {
                                        if (artPerberes.Count == 0)
                                            artPerberes.ktheArtikujPerberesSipasIdArtikullitKryesorDheDatesMeTeAfert(this.IdArtikulli, data);
                                        foreach (clsArtikulliPerberes art in artPerberes)
                                        {
                                            //int tmpMetodeKostoje = clsArtikulli.ktheMetodeKostoje(art.IdLidheseArt);
                                            //decimal koef = clsArtikulli.ktheKoeficent(art.IdLidheseArt);
                                            //cmmagDet1 += tr.llogaritCmimMesatarDetajimPare(art.IdLidheseArt, tmpMetodeKostoje, koef, idMag, data, iddetajim, 1) * (double)art.Koeficienti;
                                            clsArtikulli receptura = new clsArtikulli(art.IdLidheseArt);
                                            cmmagDet1 += tr.llogaritCmimMesatar(receptura, idMag, data, iddetajim, 1, idPerdoruesi) * (double)art.Koeficienti;
                                        }
                                    }
                                    else
                                        //cmmagDet1 = tr.llogaritCmimMesatar(this, idMag, data, iddetajim, 1);
                                        cmmagDet1 = tr.llogaritCmimMesatar(this, idMag, data, iddetajim, 1, idPerdoruesi);
                                    if (cmmagDet1 != 0)
                                        vlerat.Add(cmmagDet1.ToString("F" + shifraPasPresjes));
                                    else vlerat.Add(cmmagDet1.ToString("F" + shifraPasPresjes));
                                    break;
                                }
                                else if (trup.PershkrimKolone.Contains("Kosto Detajim 2"))
                                {
                                    if (idDetajim2 <= 0)
                                    {
                                        cmmagDet2 = 1;
                                        if (cmmagDet2 != 0)
                                            vlerat.Add(cmmagDet2.ToString("F" + shifraPasPresjes));
                                        else vlerat.Add(cmmagDet2.ToString("F" + shifraPasPresjes));
                                        break;
                                    }
                                    if (this.Klasa == 4)
                                    {
                                        if (artPerberes.Count == 0)
                                            artPerberes.ktheArtikujPerberesSipasIdArtikullitKryesorDheDatesMeTeAfert(this.IdArtikulli, data);
                                        foreach (clsArtikulliPerberes art in artPerberes)
                                        {
                                            clsArtikulli receptura = new clsArtikulli(art.IdLidheseArt);
                                            //int tmpMetodeKostoje = clsArtikulli.ktheMetodeKostoje(art.IdLidheseArt);
                                            decimal koef = clsArtikulli.ktheKoeficent(art.IdLidheseArt);
                                            //cmmagDet2 += tr.llogaritCmimMesatarDetajimDyte(art.IdLidheseArt, tmpMetodeKostoje, koef, idMag, data, idDetajim2, 1) * (double)art.Koeficienti;
                                            cmmagDet2 += tr.llogaritCmimMesatarDetajimDyte(receptura, koef, idMag, data, idDetajim2, 1, idPerdoruesi) * (double)art.Koeficienti;
                                        }
                                    }
                                    else
                                        //cmmagDet2 = tr.llogaritCmimMesatarDetajimDyte(this.IdArtikulli, this.MetodeKostojeArtikulli, this.KoeficientArtikulli, idMag, data, idDetajim2, 1);
                                        cmmagDet2 = tr.llogaritCmimMesatarDetajimDyte(this, this.KoeficientArtikulli, idMag, data, idDetajim2, 1, idPerdoruesi);
                                    if (cmmagDet2 != 0)
                                        vlerat.Add(cmmagDet2.ToString("F" + shifraPasPresjes));
                                    else vlerat.Add(cmmagDet2.ToString("F" + shifraPasPresjes));
                                    break;
                                }
                                else if (trup.PershkrimKolone.Contains("Kosto/Njesi2"))
                                {
                                    if (this.Klasa == 4)
                                    {
                                        if (artPerberes.Count == 0)
                                            artPerberes.ktheArtikujPerberesSipasIdArtikullitKryesorDheDatesMeTeAfert(this.IdArtikulli, data);
                                        foreach (clsArtikulliPerberes art in artPerberes)
                                        {
                                            //int tmpMetodeKostoje = clsArtikulli.ktheMetodeKostoje(art.IdLidheseArt);
                                            //decimal koef = clsArtikulli.ktheKoeficent(art.IdLidheseArt);
                                            //cmmag += tr.llogaritCmimMesatarDetajimPare(art.IdLidheseArt, tmpMetodeKostoje, koef, idMag, data, -1, 1) * (double)art.Koeficienti;
                                            clsArtikulli receptura = new clsArtikulli(art.IdLidheseArt);
                                            cmmag += tr.llogaritCmimMesatar(receptura, idMag, data, -1, 1, idPerdoruesi, true) * (double)art.Koeficienti;
                                        }
                                    }
                                    else
                                        //cmmag = tr.llogaritCmimMesatar(this, idMag, data, -1, 1);
                                        cmmag = tr.llogaritCmimMesatar(this, idMag, data, -1, 1, idPerdoruesi, true);
                                    if (cmmag != 0)
                                        vlerat.Add((cmmag * Convert.ToDouble(KoeficientArtikulli)).ToString("F" + shifraPasPresjes));
                                    else vlerat.Add((cmmag * Convert.ToDouble(KoeficientArtikulli)).ToString("F" + shifraPasPresjes));
                                    break;
                                }

                                else if (this.Klasa == 4)
                                {
                                    if (artPerberes.Count == 0)
                                        artPerberes.ktheArtikujPerberesSipasIdArtikullitKryesorDheDatesMeTeAfert(this.IdArtikulli, data);
                                    foreach (clsArtikulliPerberes art in artPerberes)
                                    {
                                        //int tmpMetodeKostoje = clsArtikulli.ktheMetodeKostoje(art.IdLidheseArt);
                                        //decimal koef = clsArtikulli.ktheKoeficent(art.IdLidheseArt);
                                        //cmmag += tr.llogaritCmimMesatarDetajimPare(art.IdLidheseArt, tmpMetodeKostoje, koef, idMag, data, -1, 1) * (double)art.Koeficienti;
                                        clsArtikulli receptura = new clsArtikulli(art.IdLidheseArt);
                                        cmmag += tr.llogaritCmimMesatar(receptura, idMag, data, -1, 1, idPerdoruesi) * (double)art.Koeficienti;
                                    }
                                }
                                else
                                    //cmmag = tr.llogaritCmimMesatar(this, idMag, data, -1, 1);
                                    cmmag = tr.llogaritCmimMesatar(this, idMag, data, -1, 1, idPerdoruesi);
                                if (cmmag != 0)
                                    vlerat.Add(cmmag.ToString("F" + shifraPasPresjes));
                                else vlerat.Add(cmmag.ToString("F" + shifraPasPresjes));
                                break;
                            }
                            if (trup.PershkrimKolone.Contains("Sasia"))
                            {
                                if (trup.PershkrimKolone.Contains("Sasia Detajim 1"))
                                {
                                    if (iddetajim <= 0)
                                        sasimagDet1 = 0;
                                    else
                                    {
                                        sasimagDet1 = (this.Klasa == 4) ? 0 : DbCore.DbRegjistrim.clsTrupiMagazina.merrSasi(this, idMag, data, iddetajim);
                                    }
                                    if (sasimagDet1 != 0)
                                        vlerat.Add(sasimagDet1.ToString("F" + shifraPasPresjes));
                                    else vlerat.Add(sasimagDet1.ToString("F" + shifraPasPresjes));
                                    break;
                                }
                                if (trup.PershkrimKolone.Contains("Sasia Detajim 2"))
                                {
                                    if (idDetajim2 <= 0)
                                        sasimagDet2 = 0;
                                    else
                                    {
                                        sasimagDet2 = (this.Klasa == 4) ? 0 : DbCore.DbRegjistrim.clsTrupiMagazina.merrSasiSipasDetajimit(this, idMag, data, idDetajim2, 2);
                                    }
                                    if (sasimagDet2 != 0)
                                        vlerat.Add(sasimagDet2.ToString("F" + shifraPasPresjes));
                                    else vlerat.Add(sasimagDet2.ToString("F" + shifraPasPresjes));
                                    break;
                                }
                                if (trup.PershkrimKolone.Contains("Sasia e rezervuar UB"))
                                {
                                    sasiarezUB = (this.Klasa == 4) ? 0 : DbCore.DbRegjistrim.clsTrupiRezervime.merrSasiUB(IdArtikulli, 0, idMag, data);
                                    if (sasiarezUB != 0)
                                        vlerat.Add(sasiarezUB.ToString("F" + shifraPasPresjes));
                                    else vlerat.Add(sasiarezUB.ToString("F" + shifraPasPresjes));
                                    break;
                                }
                                if (trup.PershkrimKolone.Contains("Sasia e rezervuar"))
                                {
                                    sasiarez = (this.Klasa == 4) ? 0 : DbCore.DbRegjistrim.clsTrupiRezervime.merrSasi(IdArtikulli, 0, idMag, data);
                                    if (sasiarez != 0)
                                        vlerat.Add(sasiarez.ToString("F" + shifraPasPresjes));
                                    else vlerat.Add(sasiarez.ToString("F" + shifraPasPresjes));
                                    break;
                                }

                                if (trup.PershkrimKolone.Contains("Sasia Disponibel"))
                                {
                                    if (this.Klasa == 4)
                                        sasiadisp = 0;
                                    else
                                    {
                                        sasimag = DbCore.DbRegjistrim.clsTrupiMagazina.merrSasi(this, idMag, data, -1);
                                        sasiarez = DbCore.DbRegjistrim.clsTrupiRezervime.merrSasi(IdArtikulli, 0, idMag, data);
                                        sasiadisp = sasimag - sasiarez;
                                    }
                                    if (sasiadisp != 0)
                                        vlerat.Add(sasiadisp.ToString("F" + shifraPasPresjes));
                                    else vlerat.Add(sasiadisp.ToString("F" + shifraPasPresjes));
                                    break;
                                }
                                if (trup.PershkrimKolone.Contains("Sasia e porositur"))
                                {
                                    sasiaporUB = (this.Klasa == 4) ? 0 : DbCore.DbRegjistrim.clsTrupiShitje.merrSasiPorositurSipasMagazines(IdArtikulli, idMag);
                                    if (sasiaporUB != 0)
                                        vlerat.Add(sasiaporUB.ToString("F" + shifraPasPresjes));
                                    else vlerat.Add(sasiaporUB.ToString("F" + shifraPasPresjes));
                                    break;
                                }
                                if (trup.PershkrimKolone.Contains("Sasia e lire e porositur"))
                                {
                                    if (this.Klasa == 4)
                                        sasialire = 0;
                                    else
                                    {
                                        sasiarezUB = DbCore.DbRegjistrim.clsTrupiRezervime.merrSasiUB(IdArtikulli, 0, idMag, data);
                                        sasiaporUB = DbCore.DbRegjistrim.clsTrupiShitje.merrSasiPorositurSipasMagazines(IdArtikulli, idMag);
                                        sasialire = sasiaporUB - sasiarezUB;
                                    }
                                    if (sasialire != 0)
                                        vlerat.Add(sasialire.ToString("F" + shifraPasPresjes));
                                    else vlerat.Add(sasialire.ToString("F" + shifraPasPresjes));

                                    break;
                                }

                                if (trup.PershkrimKolone.Contains("Sasia/Njesi2"))
                                {
                                    sasimag = (this.Klasa == 4) ? 0 : DbCore.DbRegjistrim.clsTrupiMagazina.merrSasi(this, idMag, data, -1);
                                    if (sasimag != 0)
                                        vlerat.Add((sasimag / Convert.ToDouble(koeficientArtikulli)).ToString("F" + shifraPasPresjes));
                                    else vlerat.Add((sasimag / Convert.ToDouble(koeficientArtikulli)).ToString("F" + shifraPasPresjes));

                                    break;
                                }
                                sasimag = (this.Klasa == 4) ? 0 : DbCore.DbRegjistrim.clsTrupiMagazina.merrSasi(this, idMag, data, -1);
                                if (sasimag != 0)
                                    vlerat.Add(sasimag.ToString("F" + shifraPasPresjes));
                                else vlerat.Add(sasimag.ToString("F" + shifraPasPresjes));
                            }
                            if (!trup.PershkrimKolone.Contains("Sasia") && !trup.PershkrimKolone.Contains("Kosto"))
                                vlerat.Add("Konfigurimi i fushes gabim!");
                            break;
                    }
                }
                catch (Exception e)
                {
                    vlerat.Add("Gabim");
                }
            }
            return lista;
        }

        public void mbushAutorizime()
        {
            using (clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin())
            {
                mbushAutorizime(dbAdmin);
            }
        }

        public void mbushAutorizime(clsDatabaseAdmin dbAdmin)
        {
            colLidhjetAutorizim lidhje = new colLidhjetAutorizim(idArtikulli, "Artikulli", dbAdmin);
            if (lidhje.Count != 0)
            {
                this.autorizimet = clsAutorizimKoka.ktheKodAutorizim(lidhje[0].IdAutorizimeKoka);
                for (int i = 1; i < lidhje.Count; i++)
                    autorizimet += "," + clsAutorizimKoka.ktheKodAutorizim(lidhje[i].IdAutorizimeKoka);
            }
            else
                this.autorizimet = "";
        }

        public colKodbare mbushKodBare(clsDatabaseInventari dbinventari)
        {
            this.oColKodbare = new colKodbare(this.idArtikulli, dbinventari);
            return this.oColKodbare;
        }

        public colKodbare mbushKodBare()
        {
            using (clsDatabaseInventari dbinventari = new clsDatabaseInventari())
            {
                return mbushKodBare(dbinventari);
            }
        }

        public static decimal ktheKoeficent(int idArtikulli)
        {
            using (clsDatabaseAdmin db = new clsDatabaseAdmin())
            {
                return db.ktheKoeficentArtikulli(idArtikulli);
            }
        }

        public static int ktheMetodeKostoje(int idArtikulli)
        {
            using (clsDatabaseAdmin db = new clsDatabaseAdmin())
            {
                return db.ktheMetodeKostoje(idArtikulli);
            }
        }

        public static int ktheIdTvsh(int idArtikulli)
        {
            using (clsDatabaseAdmin db = new clsDatabaseAdmin())
            {
                return db.ktheIdTvshSipasIdArtikullit(idArtikulli);
            }
        }

        public static string ktheCmimArtikulliNivelBaze(int idArtikulli, int idndermarrje, int idnjesia)
        {
            using (clsDatabaseAdmin db = new clsDatabaseAdmin())
            {
                return db.ktheCmimArtikulliNivelBaze(idArtikulli, idndermarrje, idnjesia);
            }
        }

        public static int ktheIdTvshSipasKodArt(string kodi, int idNdermarrje)
        {
            using (clsDatabaseAdmin db = new clsDatabaseAdmin())
            {
                return db.ktheIdTvshSipasKodArtikullit(kodi, idNdermarrje);
            }
        }

        /// <summary>
        /// update-on daten e modifikimit dhe perdoruesin per artikullin
        /// </summary>
        /// <param name="idArtikulli"></param>
        /// <param name="idPerdoruesi"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public static clsMesazh updateDtModifikimi(int idArtikulli, int idPerdoruesi, clsDatabaseAdmin db)
        {
            return db.updateDtModifikiArtikullit(idArtikulli, idPerdoruesi);
        }

        public static int ktheIdArtRaportuesSipasKodit(string kodArtRaportuesi, int idNdermRaportuese)
        {
            using (clsDatabaseAdmin db = new clsDatabaseAdmin())
            {
                return db.ktheIdArtRaportuesSipasKodit(kodArtRaportuesi, idNdermRaportuese);
            }
        }

        public static string ktheKodArtikulliSipasId(int idArtikulli)
        {
            using (clsDatabaseAdmin db = new clsDatabaseAdmin())
            {
                return db.ktheKodArtikulliSipasId(idArtikulli);
            }
        }

        public static bool ktheKontrollCmimiPerDetajim(int idArtikulli)
        {
            using (clsDatabaseAdmin db = new clsDatabaseAdmin())
            {
                return db.ktheKontrollCmimiPerDetajim(idArtikulli);
            }
        }

        public static string merrPershkrimArtikulliNgaKodbariNqsEkzistonKodbari(string barkod, int idNderm)
        {
            using (clsDatabaseInventari dbInventar = new clsDatabaseInventari())
            {
                return dbInventar.merrPershkrimArtikulliNgaKodbari(idNderm, barkod);
            }
        }

        public static bool eshteERezervueshmeRecepturaAparat(int idArtikull)
        {
            using (clsDatabaseInventari dbInventar = new clsDatabaseInventari())
            {
                return dbInventar.eshteERezervueshmeRecepturaAparat(idArtikull);
            }
        }


        public bool MundTePorositet(decimal gjendje, decimal gjendje2, decimal gjendjeOwn, decimal porositur, bool eshteOwn, decimal vlere, bool bazaar, bool vfone)
        {

            if ((this.klasa != 4 && !this.IRezervueshem) || (this.Klasa == 4 && !eshteERezervueshmeRecepturaAparat(this.IdArtikulli)) || this.KodArtikulli.EndsWith("blere_nga_dealer") || gjendje2 > 0)
                return false;

            if (bazaar)
            {
                if (vlere <= 0)
                    return false;

                return gjendje <= 0;

            }

            if (vfone)
            {
                if (gjendje > 0)
                    return false;

                if (gjendjeOwn <= 0)
                    return false;

                return (porositur < this.StokuMaxVfOne);

            }
            return gjendje <= 0;

        }

        public static System.Object MerrDetajimeArtikulli(string kodArtikulli, int idNdermarrje, int idPerdorues)
        {
            string detajime = "";
            string detajime2 = "";
            DataTable detajimet = colDetajimeArtikulli.ktheDetajimeMeLlojSipasArtikullitAndNdermarrjesAndAutorizime(kodArtikulli, idNdermarrje, idPerdorues);

            for (var i = 0; i < detajimet.Rows.Count; i++)
            {
                DataRow det = detajimet.Rows[i];
                if (det.ItemArray[0].ToString() == "1")
                    detajime += det.ItemArray[1].ToString() + ',';
                else
                    detajime2 += det.ItemArray[1].ToString() + ',';
            }

            if (detajime.Length > 0) detajime = detajime.Substring(0, detajime.Length - 1);

            if (detajime2.Length > 0) detajime2 = detajime2.Substring(0, detajime2.Length - 1);
            return new { detajime1 = detajime, detajime2 = detajime2 };
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// Metode per mbushjen e artikullit nga databaza. Thirret nga metoda mbushArtikujt e colArtikujt.cs
        /// </summary>
        /// <param name="dbDataRowArtikull">Si parameter merr nje DataRow.</param>
        /// <returns>Kthen true nese mbushja ndodh me sukses. Ne te kundert false.</returns>
        internal bool mbushArtikull(DataRow dbDataRowArtikull)
        {
            if (dbDataRowArtikull == null)
                return false;
            try
            {
                ImbLogger.LogTraceShitje("Filloi metoda mbushArtikull");
                int.TryParse(dbDataRowArtikull["IDARTIKULLI"].ToString(), out idArtikulli);
                kodArtikulli = dbDataRowArtikull["KODARTIKULLI"].ToString();
                pershkrimArtikulli = dbDataRowArtikull["PERSHKRIMARTIKULLI"].ToString();
                pershkrimiAngArtikulli = dbDataRowArtikull["PERSHKRIMIANGARTIKULLI"].ToString();
                kodiDoganorArtikulli = dbDataRowArtikull["KODIDOGANORARTIKULLI"].ToString();
                vendodhjeArtikulli = dbDataRowArtikull["VENDODHJEARTIKULLI"].ToString();
                int.TryParse(dbDataRowArtikull["KODIFIKIMI1ARTIKULLI"].ToString(), out kodifikimi1Artikulli);
                int.TryParse(dbDataRowArtikull["KODIFIKIMI2ARTIKULLI"].ToString(), out kodifikimi2Artikulli);
                int.TryParse(dbDataRowArtikull["KODIFIKIMI3ARTIKULLI"].ToString(), out kodifikimi3Artikulli);
                origjineArtikulli = dbDataRowArtikull["ORIGJINEARTIKULLI"].ToString();
                int.TryParse(dbDataRowArtikull["NJESI1ARTIKULLI"].ToString(), out njesi1Artikulli);
                int.TryParse(dbDataRowArtikull["NJESI2ARTIKULLI"].ToString(), out njesi2Artikulli);
                decimal.TryParse(dbDataRowArtikull["KOEFICENTARTIKULLI"].ToString(), out koeficientArtikulli);
                int.TryParse(dbDataRowArtikull["IDFURNITORIKRYESOR"].ToString(), out idFurnitoriKryesor);
                decimal.TryParse(dbDataRowArtikull["PESHABRUTOARTIKULLI"].ToString(), out peshaBrutoArtikulli);
                decimal.TryParse(dbDataRowArtikull["PESHANETOARTIKULLI"].ToString(), out peshaNetoArtikulli);
                bool.TryParse(dbDataRowArtikull["DETAJIMARTIKULLI"].ToString(), out detajimArtikulli);
                int.TryParse(dbDataRowArtikull["KLASA"].ToString(), out klasa);
                int.TryParse(dbDataRowArtikull["IDSKEMAKONTABILITETIARTIKULLI"].ToString(), out idSkemaKontabilitetiArtikulli);
                int.TryParse(dbDataRowArtikull["IDLLOGARIINVENTARI"].ToString(), out idLlogariInventari);
                int.TryParse(dbDataRowArtikull["IDLLOGARIBLERJE"].ToString(), out idLlogariBlerje);
                int.TryParse(dbDataRowArtikull["IDLLOGARISHITJE"].ToString(), out idLlogariShitje);
                int.TryParse(dbDataRowArtikull["IDLLOGARITETRETE"].ToString(), out idLlogariTeTrete);
                int.TryParse(dbDataRowArtikull["IDLLOGARISHPENZIME"].ToString(), out idLlogariShpenzime);
                int.TryParse(dbDataRowArtikull["IDLLOGARIAMORTIZIMI"].ToString(), out idLlogariAmortizimi);
                int.TryParse(dbDataRowArtikull["IDLLOGARIPAKESIM"].ToString(), out idLlogariPakesim);
                int.TryParse(dbDataRowArtikull["IdLlogariRezerve"].ToString(), out idLlogRez);
                int.TryParse(dbDataRowArtikull["IdLlogariPakesimRezerve"].ToString(), out idLlogPakRez);
                bool.TryParse(dbDataRowArtikull["MeRezerveRivleresimi"].ToString(), out merezerverivleresimi);
                decimal.TryParse(dbDataRowArtikull["MINIMUMARTIKULLI"].ToString(), out minimumArtikulli);
                decimal.TryParse(dbDataRowArtikull["MAXIMUMARTIKULLI"].ToString(), out maximumArtikulli);
                decimal.TryParse(dbDataRowArtikull["SASINJESI"].ToString(), out sasiNjesi);
                decimal.TryParse(dbDataRowArtikull["SCRAP"].ToString(), out scrap);
                int.TryParse(dbDataRowArtikull["METODEKOSTOJEARTIKULLI"].ToString(), out metodeKostojeArtikulli);
                int.TryParse(dbDataRowArtikull["LLOGARITJAKMSHARTIKULLI"].ToString(), out llogaritjaKMSHArtikulli);
                int.TryParse(dbDataRowArtikull["ZEVENDESIMAUTOMATIKARTIKULLI"].ToString(), out zevendesimAutomatikArtikulli);
                int.TryParse(dbDataRowArtikull["IDPERDORUESI"].ToString(), out idPerdoruesi);
                int.TryParse(dbDataRowArtikull["IDNDERMARJE"].ToString(), out idNdermarje);
                bool.TryParse(dbDataRowArtikull["KONTROLLGJENDJE"].ToString(), out kontrollGjendje);
                bool.TryParse(dbDataRowArtikull["KONTROLLCMIMI"].ToString(), out kontrollCmimPerDetajim);
                bool.TryParse(dbDataRowArtikull["KONTROLLGJENDJEARTIKULLI"].ToString(), out kontrollGjendjeArtikulli);
                int.TryParse(dbDataRowArtikull["IDTVSH"].ToString(), out idTvsh);
                int.TryParse(dbDataRowArtikull["IDKONFIG"].ToString(), out idKonfig);
                bool.TryParse(dbDataRowArtikull["AKTIV"].ToString(), out aktiv);
                int.TryParse(dbDataRowArtikull["IDSTATUSDOK"].ToString(), out idStatusDok);
                int.TryParse(dbDataRowArtikull["IDKATEGORIDETAJIMI"].ToString(), out idKategoriDetajimi);
                int.TryParse(dbDataRowArtikull["IDKATEGORIDETAJIMI2"].ToString(), out idKategoriDetajimi2);
                DateTime.TryParse(dbDataRowArtikull["DTKRIJIMI"].ToString(), out dtKrijimi);
                DateTime.TryParse(dbDataRowArtikull["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                kodKodifikimi1 = dbDataRowArtikull["KodKodifikimi1"].ToString();
                kodKodifikimi2 = dbDataRowArtikull["KodKodifikimi2"].ToString();
                kodKodifikimi3 = dbDataRowArtikull["KodKodifikimi3"].ToString();
                kodNjesia1 = dbDataRowArtikull["KodNjesia1"].ToString();
                kodNjesia2 = dbDataRowArtikull["KodNjesia2"].ToString();
                pershkrimNjesia1 = dbDataRowArtikull["PershkrimNjesia1"].ToString();
                pershkrimNjesia2 = dbDataRowArtikull["PershkrimNjesia2"].ToString();
                kodKlientFurnitori = dbDataRowArtikull["KodFurnitori"].ToString();
                pershkrimKlasa = dbDataRowArtikull["PershkrimKlasa"].ToString();
                kodiSkema = dbDataRowArtikull["KodiSkema"].ToString();
                nrLlogBlerje = dbDataRowArtikull["llogble"].ToString();
                nrLlogInventari = dbDataRowArtikull["lloginv"].ToString();
                nrLlogShitje = dbDataRowArtikull["llogshit"].ToString();
                nrLlogShpenzime = dbDataRowArtikull["llogshpe"].ToString();
                nrLlogTeTrete = dbDataRowArtikull["llogtret"].ToString();
                nrLlogAmortizimi = dbDataRowArtikull["llogamortizimi"].ToString();
                kodTaksa = dbDataRowArtikull["taksa"].ToString();
                bool.TryParse(dbDataRowArtikull["LLOJIART"].ToString(), out llojiArt);
                bool.TryParse(dbDataRowArtikull["PRODHIMMEPOROSI"].ToString(), out prodhimMePorosi);
                bool.TryParse(dbDataRowArtikull["IREZERVUESHEM"].ToString(), out iRezervueshem);
                bool.TryParse(dbDataRowArtikull["PERTRANSFERIM"].ToString(), out perTransferim);
                bool.TryParse(dbDataRowArtikull["LOAN"].ToString(), out loan);
                bool.TryParse(dbDataRowArtikull["DHURATE"].ToString(), out dhurate);
                int.TryParse(dbDataRowArtikull["APLIKIMDHURATE"].ToString(), out aplikimDhurate);
                decimal.TryParse(dbDataRowArtikull["PIKE"].ToString(), out pike);
                decimal.TryParse(dbDataRowArtikull["VLERE"].ToString(), out vlere);
                bool.TryParse(dbDataRowArtikull["KONTROLLGJENDJEDETAJIM2"].ToString(), out kontrollGjendjeDetajim2);
                int.TryParse(dbDataRowArtikull["IDOBJEKTIVAKOSTO"].ToString(), out idObjektivaKosto);
                objektiva = dbDataRowArtikull["OBJEKTIVA"].ToString();
                int.TryParse(dbDataRowArtikull["IDLLOJGARANCIA"].ToString(), out idllojGarancie);
                decimal.TryParse(dbDataRowArtikull["GARANCIA"].ToString(), out garancia);
                int.TryParse(dbDataRowArtikull["IDMAGAZINA"].ToString(), out idmagazina);
                magazina = dbDataRowArtikull["MAGAZINA"].ToString();
                kodVFOne = dbDataRowArtikull["KODVFONE"].ToString();
                bool.TryParse(dbDataRowArtikull["MESERIAL"].ToString(), out meSerial);
                bool.TryParse(dbDataRowArtikull["ISHITSHEM"].ToString(), out iShitshem);
                bool.TryParse(dbDataRowArtikull["MBETJESHITJSHME"].ToString(), out mbetjeShitshme);
                int.TryParse(dbDataRowArtikull["IDARTRAPORTUESI"].ToString(), out idArtRaportuesi);
                bool.TryParse(dbDataRowArtikull["PERPESHORE"].ToString(), out perPeshore);
                pershkrimFurnitori = dbDataRowArtikull["PERSHKRIMTEFURNITORI"].ToString();
                siperfaqjaM2 = dbDataRowArtikull["SIPERFAQJAM2"].ToString();
                nrKontrate = dbDataRowArtikull["NRKONTRATE"].ToString();
                nrPasurie = dbDataRowArtikull["NRPASURIE"].ToString();
                zonaKadastrale = dbDataRowArtikull["ZONAKADASTRALE"].ToString();
                shasia = dbDataRowArtikull["SHASIA"].ToString();
                marka = dbDataRowArtikull["MARKA"].ToString();
                modeli = dbDataRowArtikull["MODELI"].ToString();
                vitProdhimi = dbDataRowArtikull["VITPRODHIMI"].ToString();
                teDhenaTeknika = dbDataRowArtikull["TEDHENATEKNIKE"].ToString();
                bool.TryParse(dbDataRowArtikull["MEBARKODLOGJIK"].ToString(), out meBarkodLogjik);
                skemaBarkodit = Convert.ToString(dbDataRowArtikull["SKEMABARKODIT"]);
                kodOferte = dbDataRowArtikull["KODOFERTE"].ToString();
                bool.TryParse(dbDataRowArtikull["APARATBAZAAR"].ToString(), out aparatBazaar);
                bool.TryParse(dbDataRowArtikull["ARTIKULLIVJETER"].ToString(), out artikullIVjeter);
                int.TryParse(dbDataRowArtikull["IDKATEGORISERIALI"].ToString(), out idFormatSeriali);
                int.TryParse(dbDataRowArtikull["KaraktereTAC"].ToString(), out nrKaraktereTAC);
                bool.TryParse(dbDataRowArtikull["LlogaritKomision"].ToString(), out llogaritKomision);
                int.TryParse(dbDataRowArtikull["LlogariKomisioni"].ToString(), out idLlogariKomision);
                nrLlogKomision = dbDataRowArtikull["NrLlogariKomisioni"].ToString();
                int.TryParse(dbDataRowArtikull["STOKUMAXVFONE"].ToString(), out stokuMaxVfOne);
                kodiiBarit = dbDataRowArtikull["KodiIBarit"].ToString();
                bool.TryParse(dbDataRowArtikull["IRimbursueshem"].ToString(), out iRimbursueshem);
                oColKodbare = new colKodbare();
                oColArtikujtZevendesues = new colArtikujtZevendesues();
                oColBuxhetet = new colBuxhetet();
                oColDetajime = new colDetajimePerArt();
                oColDetajime2 = new colDetajimePerArt();
                oColFurnitoreArtikujsh = new colFurnitoreArtikujsh();
                OColVleraFushaShtese = new colVleraFushaShtese();
                colArtikujPerberes = new colArtikulliPerberes();
                colArtikujVfone = new colArtikullVfone();
                colGjendjeArtikulli colGjendjeArtikulliMag = new colGjendjeArtikulli();
                ImbLogger.LogTraceShitje("Mbaroi metoda mbushArtikull");
                return true;
            }
            catch (InvalidCastException)
            {
                ImbLogger.LogErrorShitje("ERROR: Gabim gjate marrjes se artikullit nga db-ja");
                throw new Exception("ERROR: Gabim gjate marrjes se artikullit nga db-ja");
            }
        }

        public void mbushArtikull(clsArtikulli art)
        {
            ImbLogger.LogTraceShitje("Filloi metoda mbushArtikull!");
            idArtikulli = art.idArtikulli;
            kodArtikulli = art.kodArtikulli;
            pershkrimArtikulli = art.pershkrimArtikulli;
            pershkrimiAngArtikulli = art.pershkrimiAngArtikulli;
            kodiDoganorArtikulli = art.kodiDoganorArtikulli;
            vendodhjeArtikulli = art.vendodhjeArtikulli;
            kodifikimi1Artikulli = art.kodifikimi1Artikulli;
            kodifikimi2Artikulli = art.kodifikimi2Artikulli;
            kodifikimi3Artikulli = art.kodifikimi3Artikulli;
            origjineArtikulli = art.origjineArtikulli;
            njesi1Artikulli = art.njesi1Artikulli;
            njesi2Artikulli = art.njesi2Artikulli;
            koeficientArtikulli = art.koeficientArtikulli;
            idFurnitoriKryesor = art.idFurnitoriKryesor;
            peshaBrutoArtikulli = art.peshaBrutoArtikulli;
            peshaNetoArtikulli = art.peshaNetoArtikulli;
            detajimArtikulli = art.detajimArtikulli;
            klasa = art.klasa;
            idSkemaKontabilitetiArtikulli = art.idSkemaKontabilitetiArtikulli;
            idLlogariInventari = art.idLlogariInventari;
            idLlogariBlerje = art.idLlogariBlerje;
            idLlogariShitje = art.idLlogariShitje;
            idLlogariTeTrete = art.idLlogariTeTrete;
            idLlogariShpenzime = art.idLlogariShpenzime;
            idLlogariAmortizimi = art.idLlogariAmortizimi;
            idLlogariPakesim = art.idLlogariPakesim;
            minimumArtikulli = art.minimumArtikulli;
            maximumArtikulli = art.maximumArtikulli;
            metodeKostojeArtikulli = art.metodeKostojeArtikulli;
            pershkrimMetodeKostoje = art.pershkrimMetodeKostoje;
            llogaritjaKMSHArtikulli = art.llogaritjaKMSHArtikulli;
            zevendesimAutomatikArtikulli = art.zevendesimAutomatikArtikulli;
            idPerdoruesi = art.idPerdoruesi;
            idNdermarje = art.idNdermarje;
            kodKodifikimi1 = art.kodKodifikimi1;
            kodKodifikimi2 = art.kodKodifikimi2;
            kodKodifikimi3 = art.kodKodifikimi3;
            kodNjesia1 = art.kodNjesia1;
            kodNjesia2 = art.kodNjesia2;
            pershkrimNjesia1 = art.pershkrimNjesia1;
            pershkrimNjesia2 = art.pershkrimNjesia2;
            kodKlientFurnitori = art.kodKlientFurnitori;
            pershkrimKlasa = art.pershkrimKlasa;
            kodiSkema = art.kodiSkema;
            nrLlogBlerje = art.nrLlogBlerje;
            nrLlogShitje = art.nrLlogShitje;
            nrLlogInventari = art.nrLlogInventari;
            nrLlogShpenzime = art.nrLlogShpenzime;
            nrLlogTeTrete = art.nrLlogTeTrete;
            nrLlogAmortizimi = art.nrLlogAmortizimi;
            nrLlogPakesimi = art.nrLlogPakesimi;
            kodTaksa = art.kodTaksa;
            sasiNjesi = art.sasiNjesi;
            scrap = art.scrap;
            oColFurnitoreArtikujsh = art.oColFurnitoreArtikujsh;
            oColArtikujtZevendesues = art.oColArtikujtZevendesues;
            oColVleratFushaShtese = art.oColVleratFushaShtese;
            oColBuxhetet = art.oColBuxhetet;
            oColDetajime = art.oColDetajime;
            oColDetajime2 = art.oColDetajime2;
            colArtikujPerberes = art.colArtikujPerberes;
            colGjendjeArtikulliMag = art.colGjendjeArtikulliMag;
            colNorma = art.colNorma;
            colNormaRezerva = art.colNormaRezerva;
            autorizimet = art.autorizimet;
            kontrollGjendje = art.kontrollGjendje;
            kontrollCmimPerDetajim = art.kontrollCmimPerDetajim;
            kontrollGjendjeArtikulli = art.kontrollGjendjeArtikulli;
            idTvsh = art.idTvsh;
            idKonfig = art.idKonfig;
            aktiv = art.aktiv;
            idStatusDok = art.idStatusDok;
            dtKrijimi = art.dtKrijimi;
            dtModifikimi = art.dtModifikimi;
            llojiArt = art.llojiArt;
            prodhimMePorosi = art.prodhimMePorosi;
            idKategoriDetajimi = art.idKategoriDetajimi;
            kategoriDetajimi = art.kategoriDetajimi;
            idKategoriDetajimi2 = art.idKategoriDetajimi2;
            kategoriDetajimi2 = art.kategoriDetajimi2;
            kontrollGjendjeDetajim2 = art.kontrollGjendjeDetajim2;
            idObjektivaKosto = art.idObjektivaKosto;
            objektiva = art.objektiva;
            idllojGarancie = art.idllojGarancie;
            garancia = art.garancia;
            magazina = art.magazina;
            idmagazina = art.idmagazina;
            iRezervueshem = art.iRezervueshem;
            perTransferim = art.perTransferim;
            oArkiva = art.oArkiva;
            loan = art.loan;
            dhurate = art.dhurate;
            aplikimDhurate = art.aplikimDhurate;
            pike = art.pike;
            vlere = art.vlere;
            kodVFOne = art.kodVFOne;
            meSerial = art.meSerial;
            iShitshem = art.iShitshem;
            mbetjeShitshme = art.mbetjeShitshme;
            oColKodbare = art.oColKodbare;
            idArtRaportuesi = art.idArtRaportuesi;
            perPeshore = art.perPeshore;
            pershkrimFurnitori = art.pershkrimFurnitori;
            siperfaqjaM2 = art.siperfaqjaM2;
            nrKontrate = art.nrKontrate;
            nrPasurie = art.nrPasurie;
            zonaKadastrale = art.zonaKadastrale;
            shasia = art.shasia;
            marka = art.marka;
            modeli = art.modeli;
            vitProdhimi = art.vitProdhimi;
            teDhenaTeknika = art.teDhenaTeknika;
            meBarkodLogjik = art.meBarkodLogjik;
            skemaBarkodit = art.skemaBarkodit;
            HfArkiva = art.HfArkiva;
            kodbar = art.kodbar;
            eshteAfatShkurter = art.eshteAfatShkurter;
            idNdermarrje = art.idNdermarrje;
            idFormatSeriali = art.idFormatSeriali;
            aparatBazaar = art.aparatBazaar;
            artikullIVjeter = art.artikullIVjeter;
            kodOferte = art.kodOferte;
            idLlogRez = art.idLlogRez;
            idLlogPakRez = art.idLlogPakRez;
            merezerverivleresimi = art.merezerverivleresimi;
            nrKaraktereTAC = art.nrKaraktereTAC;
            llogaritKomision = art.llogaritKomision;
            idLlogariKomision = art.idLlogariKomision;
            nrLlogKomision = art.nrLlogKomision;
            stokuMaxVfOne = art.stokuMaxVfOne;
            kodiiBarit = art.kodiiBarit;
            iRimbursueshem = art.iRimbursueshem;
            ImbLogger.LogTraceShitje("Mbaroi metoda mbushArtikull!");
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion

    }
}