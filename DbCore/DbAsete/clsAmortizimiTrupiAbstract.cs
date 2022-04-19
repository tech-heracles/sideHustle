using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DbCore.DbInventari;
using DbCore.IMBUtils.DataBase;

namespace DbCore.DbAsete
{
    /// <summary>
    /// MODULI ASETE:
    /// Klase e ndertuar per te mbajtur objektin e trupit te dokumentit te amortizimit.
    /// Te dhenat merret nga tabela T_ASETE_AMORTIZIMI_TRUPI.
    /// </summary>
    public abstract class clsAmortizimiTrupiAbstract : ICloneable
    {
        public virtual enumObjekteAmortizimi objektiKod => enumObjekteAmortizimi.ABSTRACT;

        #region Atribute

        private int idAmortizimiTrupi;
        private int idAmortizimKoka;
        private int nrRendor;
        private int idArtikulli;
        private int idArtikull_LlojAmortizimi;
        private DateTime dateAmortizimi;
        private DateTime dateMePareAmortizimi;
        private DateTime dateNdryshimStatusMagazine;
        private int idNjesiAdministrative;
        private double normaAmortizimi;
        private double amortizimiShtese;
        private double amortizimiGjithsej;
        private double amortizimiVjetor;
        private double vleftaPlusMinus;
        private double hdAmortizimGjithsej;
        private double hdAmortizimVjetor;
        private double vleftaGjendje;
        private double diteAmortizimi;
        private int idAQTSeriali;
        private double vleftaShteseRivleresim;
        private string emertimi;
        private string serial;
        private int idPrind;
        private int idPrindFillestar;
        private DbInventari.clsArtikulli artikull;
        #endregion

        #region Properties

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id automatike te trupit te dokumentit te amortizimit.
        /// </summary>
        public int IdAmortizimiTrupi
        {
            get { return idAmortizimiTrupi; }
            set { idAmortizimiTrupi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se kokes se dokumentit te amortizmit me te cilen eshte i lidhur trupi i dokumentit.
        /// </summary>
        public int IdAmortizimKoka
        {
            get { return idAmortizimKoka; }
            set { idAmortizimKoka = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id e rreshtit ku eshte ne trupin aktual te dokumentit.
        /// </summary>
        public int NrRendor
        {
            get { return nrRendor; }
            set { nrRendor = value; }
        }
        /// <summary>
        /// ruhet artikulli qe te mos merret disa here nga db
        /// </summary>
        public DbInventari.clsArtikulli Artikull
        {
            get
            {
                return artikull;
            }
            set
            {
                artikull = value;
            }
        }
        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se artikullit per te cilen eshte trupi i dokumentit te amortizimit.
        /// </summary>
        public int IdArtikulli
        {
            get { return idArtikulli; }
            set { idArtikulli = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (string) Merr emertimin e artikullit
        /// </summary>
        public string Emertimi
        {
            get
            {
                return emertimi;
            }
            set { emertimi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (double) Merr amortizimin e akumuluar si rezultat i amortizimit gjithsej dhe amortizimit shtese.
        /// </summary>
        public double AmortizimAkumuluar
        {
            get
            {
                return amortizimiGjithsej + amortizimiShtese;
            }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere se per cfare lloj amortizimi eshte trupi i dokumentit te amortizimit.
        /// </summary>
        public int IdArtikull_LlojAmortizimi
        {
            get { return idArtikull_LlojAmortizimi; }
            set { idArtikull_LlojAmortizimi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (DateTime) Merr ose jep vlere dates se kur eshte data e llogaritjes se amortizimit per dokumentin e amortizimit.
        /// </summary>
        public DateTime DateAmortizimi
        {
            get { return dateAmortizimi; }
            set { dateAmortizimi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (DateTime) Merr ose jep vlere dates kur ka ndodhur llogaritja paraardhese e dokumentit te amortizimit per serialin dhe artikullin e rreshtit.
        /// </summary>
        public DateTime DateMePareAmortizimi
        {
            get { return dateMePareAmortizimi; }
            set { dateMePareAmortizimi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (DateTime) Merr ose jep vlere daten kur eshte vendosur seriali ne magazine inaktive. Mbivendoset sa here seriali rikthehet ne magazine inaktive.
        /// </summary>
        public DateTime DateNdryshimStatusMagazine
        {
            get { return dateNdryshimStatusMagazine; }
            set { dateNdryshimStatusMagazine = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se njesi administrative ku po behet dokumenti i amortizimit.
        /// </summary>
        public int IdNjesiAdministrative
        {
            get { return idNjesiAdministrative; }
            set { idNjesiAdministrative = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (double) Merr ose jep normen e amortizimit qe po llogaritet per serialin e rreshtit te trupit te dokumentit te amortizimit.
        /// </summary>
        public double NormaAmortizimi
        {
            get { return normaAmortizimi; }
            set { normaAmortizimi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (double) Merr ose jep amortizimit shtese te llogaritur per diten e pallogaritura te amortizimit per serialin e rreshtit te trupit te dokumentit te amortizimit.
        /// </summary>
        public double AmortizimiShtese
        {
            get { return amortizimiShtese; }
            set { amortizimiShtese = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (double) Merr ose jep vlere e amortizimit gjithsej qe kur eshte amortizuar per here te pare deri ne muajin aktual qe po llogaritim per serialin e rreshtit te trupit te dokumentit te amortizimit.
        /// </summary>
        public double AmortizimiGjithsej
        {
            get { return amortizimiGjithsej; }
            set { amortizimiGjithsej = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (double) Merr ose jep vlere amortizmit vetem per vite te plote qe kur eshte amortizuar per here te pare deri momentalisht qe po llogaritim per serialin e rreshtit te trupit te dokumentit te amortizimit.
        /// </summary>
        public double AmortizimiVjetor
        {
            get { return amortizimiVjetor; }
            set { amortizimiVjetor = value; }
        }

        /// <summary>
        /// merr kodin e serialit
        /// </summary>
        public string Serial
        {
            get
            {
                return serial;
            }
            set { serial = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (double) Merr ose jep vlere amortizimeve shtese qe i shohen ose i hiqen serialit te rreshtit te trupit te dokumentit te amortizimit qe po transferohet nga nje magazine ne tjetren.
        /// Nese behet dalje nga nje magazine vlera do te jete negative, dhe nese behet hyrje ne magazinen tjeter aty do te jete e njejta vlere me shenje pozitive.
        /// </summary>
        public double VleftaPlusMinus
        {
            get { return vleftaPlusMinus; }
            set { vleftaPlusMinus = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (double) Merr ose jep vlere amotizimit gjithsej te llogaritur qe nga momenti i pare i llogaritjes se amortizimit per seriallin e trupit te dokumentit te amortizimit deri ne amortizimin e aktual nese seriali do te transferohet nga nje magazine ne tjetren.
        /// Nese behet dalje nga nje magazine vlera do te jete negative, dhe nese behet hyrje ne magazinen tjeter aty do te jete e njejta vlere me shenje pozitive.
        /// </summary>
        public double HdAmortizimGjithsej
        {
            get { return hdAmortizimGjithsej; }
            set { hdAmortizimGjithsej = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (double) Merr ose jep vlere amotizimit gjithsej vetem per vitet e plote te llogaritur qe nga momenti i pare i llogaritjes se amortizimit per seriallin e trupit te dokumentit te amortizimit deri ne amortizimin e aktual nese seriali do te transferohet nga nje magazine ne tjetren.
        /// Nese behet dalje nga nje magazine vlera do te jete negative, dhe nese behet hyrje ne magazinen tjeter aty do te jete e njejta vlere me shenje pozitive.
        /// </summary>
        public double HdAmortizimVjetor
        {
            get { return hdAmortizimVjetor; }
            set { hdAmortizimVjetor = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (double) Merr ose jep vlere vleftes se blerjes se serialit.
        /// </summary>
        public double VleftaGjendje
        {
            get { return vleftaGjendje + vleftaShteseRivleresim; }
            set { vleftaGjendje = value - vleftaShteseRivleresim; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (double) Merr ose jep vlere diteve aktuale te amortizimit qe llogaritin amortizimin shtese.
        /// </summary>
        public double DiteAmortizimi
        {
            get { return diteAmortizimi; }
            set { diteAmortizimi = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se serialit per te cilen eshte llogaritur amortizimi ne trupin e dokumentit te amortizimit.
        /// </summary>
        public int IdAQTSeriali
        {
            get { return idAQTSeriali; }
            set { idAQTSeriali = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (double) Merr ose jep vlere vleftes se marre nga rivleresimi
        /// </summary>
        public double VleftaShteseRivleresim
        {
            get { return vleftaShteseRivleresim; }
            set { vleftaShteseRivleresim = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se serialit per te cilen eshte llogaritur amortizimi ne trupin e dokumentit te amortizimit.
        /// </summary>
        public int IdPrind
        {
            get { return idPrind; }
            set { idPrind = value; }
        }

        /// <summary>
        /// MODULI ASETE:
        /// (int) Merr ose jep vlere id se serialit per te cilen eshte llogaritur amortizimi ne trupin e dokumentit te amortizimit.
        /// </summary>
        public int IdPrindFillestar
        {
            get { return idPrindFillestar; }
            set { idPrindFillestar = value; }
        }

        #endregion

        #region Konstruktori

        /// <summary>
        /// MODULI ASETE:
        /// Krijon objektin bosh te klases clsAmortizimiTrupi per trupin e dokumentit te amortizimit .
        /// </summary>
        public clsAmortizimiTrupiAbstract()
        {
        }

        public clsAmortizimiTrupiAbstract(DataRow dbDataRowAmortizimTrupi)
        {
            mbushAmortizimTrupiObjekt(dbDataRowAmortizimTrupi);
        }

        /// <summary>
        /// MODULI ASETE:
        /// Sherben per te krijuar clsAmortizimiTrupi nga grida client
        /// </summary>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes.</param>
        /// <param name="idPerdorues">(int) Id e perdoruesit.</param>
        /// <param name="kontrolloserial">(bool) True nes duhet te kontrolloj serialin dhe ne te kundert False.</param>
        /// <param name="rreshtDokuKlient">(Dictionary) Rreshti i dokumentit te klientit.</param>
        public clsAmortizimiTrupiAbstract(int idNdermarrje, int idPerdorues, bool kontrolloserial, Dictionary<string, object> rreshtDokuKlient, string lloj, List<double> amortizimfillestar)
        {
            string kodi = rreshtDokuKlient["txtKodi"].ToString();
            if (kodi == "")
                return;
            int idKodi = Convert.ToInt32(rreshtDokuKlient["txtIdKodi"]);
            string emertimi = rreshtDokuKlient["txtEmertimi"].ToString();
            string gjendja = rreshtDokuKlient["txtGjendja"].ToString();
            string gjithsej = rreshtDokuKlient["txtGjithsej"].ToString();
            string vjetor = rreshtDokuKlient["txtVjetor"].ToString();
            string amfillestar = rreshtDokuKlient["txtAmFillestar"].ToString();
            string magazina = rreshtDokuKlient["txtMagazina"].ToString();
            string serial = rreshtDokuKlient["txtSerial"].ToString();
            if (kodi != null && kodi != "null" && kodi != "")
            {
                clsArtikulli art = new clsArtikulli();
                if (!clsArtikulli.ekziston(kodi, idNdermarrje))
                {
                    throw new Exception("Nje nga artikujt nuk ekziston!");
                }
                art.ktheArtikullSipasKoditDheAutorizime(kodi, idNdermarrje, idPerdorues);
                if (art.IdArtikulli == 0)
                {
                    throw new Exception("Nuk keni autorizime per artikullin " + kodi + "!");
                }
                if (!art.Aktiv)
                {
                    throw new Exception("Artikulli me kod:" + art.KodArtikulli + " nuk eshte aktiv!");
                }
                if (idKodi != art.IdArtikulli)
                    throw new Exception("Artikulli me kod: " + art.KodArtikulli + " eshte marre gabimisht!");
                if (!art.LlojiArt)
                    throw new Exception("Artikulli me kod: " + art.KodArtikulli + " nuk eshte artikull afatgjate!");
                idArtikulli = art.IdArtikulli;
                artikull = art;
                if (emertimi != null && emertimi != "null")
                    this.emertimi = emertimi;

                if (gjendja != null && gjendja != "null" && gjendja != "")
                    this.vleftaGjendje = double.Parse(gjendja);
                if (lloj == "amortizim")
                {
                    if (gjithsej != null && gjithsej != "null" && gjithsej != "")
                        this.amortizimiGjithsej = double.Parse(gjithsej);
                    if (vjetor != null && vjetor != "null" && vjetor != "")
                        this.amortizimiVjetor = double.Parse(vjetor);
                    if (amfillestar != null && amfillestar != "null" && amfillestar != "")
                        amortizimfillestar.Add(double.Parse(amfillestar));
                }
                else
                {
                    if (gjithsej != null && gjithsej != "null" && gjithsej != "")
                        this.vleftaPlusMinus = double.Parse(gjithsej);
                }

                int idMagHyrje = -1;
                if (magazina != null && magazina != "null" && magazina != "")
                {
                    DbCore.DbRegjistrim.clsNjesiAdministrative njesiadm = new DbCore.DbRegjistrim.clsNjesiAdministrative(magazina, idNdermarrje);
                    if (njesiadm.IdNjesiAdministrative <= 0)
                        throw new Exception("Nje nga magazinat nuk ekziston!");
                    njesiadm = new DbCore.DbRegjistrim.clsNjesiAdministrative(magazina, idNdermarrje, idPerdorues);

                    if (njesiadm.IdNjesiAdministrative < 1)
                        throw new Exception("Nuk keni autorizime ne kete magazine!");
                    if (magazina != "" && !njesiadm.Aktiv)
                        throw new Exception("Magazina nuk eshte aktive!");
                    idMagHyrje = njesiadm.IdNjesiAdministrative;
                }


                IdNjesiAdministrative = idMagHyrje;
                if (serial != null && serial != "null" && serial != "")
                {
                    clsAQTSeriale seriali = new clsAQTSeriale();
                    seriali.merrAQTSerialSipasKodAQT(serial, idNdermarrje);
                    if (seriali.IdAQTSerial <= 0)
                        throw new Exception("Nje nga serialet nuk ekziston!");
                    IdAQTSeriali = seriali.IdAQTSerial;
                }
                else IdAQTSeriali = 0;
                if (kontrolloserial && IdAQTSeriali == 0)
                    throw new Exception("Ka artikuj pa seriale ne gride");
            }
            else
                idArtikulli = -1;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Krijon objektin me parametra te klases clsAmortizimiTrupi per trupin e dokumentit te amortizimit .
        /// </summary>
        /// <param name="nrRreshti">(int) Numri i rreshtit te trupit.</param>
        /// <param name="idArtikulli">(int) Id e artikullit per te cilen po rregjistrohet rreshti.</param>
        /// <param name="emertim">(string) Merr emertimin e artikullit.</param>
        /// <param name="llojAmortizimi">(int) Id e llojit te amortizimit.</param>
        /// <param name="dataMePareAmortizimi">(DateTime) Data e amortizimit me pare.</param>
        /// <param name="dataAmortizimi">(DateTime) Data e amortizimit.</param>
        /// <param name="dateNdryshimiMagazine">(DateTime) Data e ndryshimit te magazines.</param>
        /// <param name="idNjesiAdministrative">(int) Id e njesi administrative ku eshte seriali.</param>
        /// <param name="idAQTSerial">(int) Id e serialit te aqt-se.</param>
        public clsAmortizimiTrupiAbstract(int nrRreshti, int idArtikulli, string emertim, int llojAmortizimi, DateTime dataMePareAmortizimi, DateTime dataAmortizimi, DateTime dateNdryshimiMagazine, int idNjesiAdministrative, int idAQTSerial, string serial, DbInventari.clsArtikulli elem, double vleftaShteseRivleresim)
        {
            idAmortizimiTrupi = 0;
            idAmortizimKoka = 0;
            nrRendor = nrRreshti;
            this.idArtikulli = idArtikulli;
            emertimi = emertim;
            idArtikull_LlojAmortizimi = llojAmortizimi;
            dateMePareAmortizimi = dataMePareAmortizimi;
            dateAmortizimi = dataAmortizimi;
            /*Nese statusi i magazines ndryshohet, ath ne dokumentin e ndryshimit te statusit ne amortizim kolona DateMagazineInaktive do te marr vleren e dates se amortizimit.
            Duhet per te llogaritur se sa kohe mund te qendroje ne nje status magazine pa i llogaritur amortizim.
            Ne veprimet e tjera qe skane lidhje me ndrrimin e statusit do te qendroje sic vjen nga veprimi paraardhes.*/
            dateNdryshimStatusMagazine = dateNdryshimiMagazine;
            this.idNjesiAdministrative = idNjesiAdministrative;
            this.vleftaShteseRivleresim = vleftaShteseRivleresim;
            idAQTSeriali = idAQTSerial;
            this.serial = serial;
            artikull = elem;
        }

        #endregion

        #region Krijues instance

        public static clsAmortizimiTrupiAbstract krijoInstance(enumObjekteAmortizimi objektiKod)
        {
            if (objektiKod == enumObjekteAmortizimi.REZERVA)
                return new clsAmortizimiTrupiRezerva();
            else
                return new clsAmortizimiTrupi();
        }

        public static clsAmortizimiTrupiAbstract krijoInstance(int nrRreshti, int idArtikulli, string emertim, int llojAmortizimi, DateTime dataMePareAmortizimi, DateTime dataAmortizimi, DateTime dateNdryshimiMagazine, int idNjesiAdministrative, int idAQTSerial, string serial, DbInventari.clsArtikulli elem, double vleftaShteseRivleresim, enumObjekteAmortizimi objektiKod)
        {
            if (objektiKod == enumObjekteAmortizimi.REZERVA)
                return new clsAmortizimiTrupiRezerva(nrRreshti, idArtikulli, emertim, llojAmortizimi, dataMePareAmortizimi, dataAmortizimi, dateNdryshimiMagazine, idNjesiAdministrative, idAQTSerial, serial, elem, vleftaShteseRivleresim);
            else
                return new clsAmortizimiTrupi(nrRreshti, idArtikulli, emertim, llojAmortizimi, dataMePareAmortizimi, dataAmortizimi, dateNdryshimiMagazine, idNjesiAdministrative, idAQTSerial, serial, elem, vleftaShteseRivleresim);
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// MODULI ASETE:
        /// Metoda perdoret per te mbushur te gjitha atributet e objektit clsAmortizimiTrupi sipas te dhenave qe vijne nga cdo rresht i databazes nga tabela T_ASETE_AMORTIZIMI_TRUPI.
        /// </summary>
        /// <param name="dbDataRowAmortizimTrupi">(DataRow) Merr si parameter vetem nje rresht te kthyer nga tabelat ne databaze per te mbushur nje objekt.</param>
        /// <returns>(bool) Kthen true nese nuk ndodh asnje gabim gjate leximit te te dhenave, ne te kundert false.</returns>
        internal abstract bool mbushAmortizimTrupiObjekt(DataRow dbDataRowAmortizimTrupi);

        /// <summary>
        /// MODULI ASETE:
        /// Metoda perdoret per te mbushur te gjitha atributet e objektit clsAmortizimiTrupi sipas te dhenave qe vijne nga cdo rresht i databazes nga tabela T_ASETE_AMORTIZIMI_TRUPI per rillogaritjet e amortizimeve ne artikujt pa serial.
        /// </summary>
        /// <param name="dbDataRowAmortizimTrupi">(DataRow) Merr si parameter vetem nje rresht te kthyer nga tabelat ne databaze per te mbushur nje objekt.</param>
        /// <returns>(bool) Kthen true nese nuk ndodh asnje gabim gjate leximit te te dhenave, ne te kundert false.</returns>
        internal abstract bool mbushAmortizimTrupiObjektPerRillogaritjeArtikujPaSerial(DataRow dbDataRowAmortizimTrupi);

        #endregion

        #region Metoda Publike

        /// <summary>
        /// MODULI ASETE:
        /// Ruan objektin e rreshtit te trupit te amortizimit.
        /// </summary>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen True nese ruajtja perfundon me sukses, ose False nese ruajtja jep gabim.</returns>
        public clsMesazh ruaj()
        {
            clsMesazh pergjigja = ruajAmortizimiTrupiTransaksion();
            return pergjigja;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Modifikon objektin e rreshtit te trupit te amortizimit.
        /// </summary>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen True nese modifikimi perfundon me sukses, ose False nese modifikimi jep gabim.</returns>
        public clsMesazh modifikoTrupPerRillogaritje(DbData dbData)
        {
            clsMesazh pergjigja = modifikoAmortizimiTrupiTransaksionPerRillogaritje(dbData);
            return pergjigja;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr numrin e trupave te gjetur sipas parametrave.
        /// </summary>
        /// <param name="idSeriali">(int) Id e serialit te aqt-se qe po amortizohet.</param>
        /// <param name="dataAmortizimi">(DateTime) Data e amortizimit te rreshtit te trupit.</param>
        /// <param name="idStandartAmortizimi">(int) Id e standartit te amortizimit.</param>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen numrin e trupave te gjetur nese gjendet, ne te kundert kthen 0.</returns>
        public static int merrNrAmortizimTrupiSipasIdSerialiDateAmortizimi(int idSeriali, DateTime dataAmortizimi, int idStandartAmortizimi, clsDatabazeAsete moduliAsete)
        {
            int pergjigje = moduliAsete.ktheNrAmortizimTrupiSipasIdSerialiDateAmortizimi(idSeriali, dataAmortizimi, idStandartAmortizimi);
            return pergjigje;
        }

        public static bool kaVeprimePasPerKeteSerial(int idSeriali, DateTime dataAmortizimi, int idStandartAmortizimi, enumObjekteAmortizimi objektiKod)
        {
            clsDatabazeAseteAbstract moduliAsete = clsDatabazeAseteAbstract.krijoInstance(objektiKod);
            bool pergjigje = moduliAsete.kaVeprimePasPerKeteSerial(idSeriali, dataAmortizimi, idStandartAmortizimi);
            return pergjigje;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr objektin e fundit te trupit te dokumentit te amortizimit per serialin.
        /// </summary>
        /// <param name="idNdermarrje">(int) Id e ndermarrjes per te cilen po behet regjistrimi i amortizimit.</param>
        /// <param name="dataMaksimaleZgjedhje">(DateTime) Data maksimale e zgjedhjeve te dokumetave te meparshem te amortizimit.</param>
        /// <param name="idLlojStandarti">(int) Id e llojit te standartit qe behet kontrolli.</param>
        /// <param name="idSeriali">(int) Id e serialit qe kerkojme veprimin paraardhes.</param>
        /// <param name="idRenditje">(int) Id e renditjes qe kerkojme veprimin paraardhes.</param>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate marrjes se trupit te fundit te aqt-se ose False ne te kundert.</returns>
        public bool merrAmortizimTrupiAmortFunditSipasSerialit(int idNdermarrje, DateTime dataMaksimaleZgjedhje, int idLlojStandarti, int idSeriali, int idRenditje)
        {
            using (clsDatabazeAseteAbstract moduliAsete =  clsDatabazeAseteAbstract.krijoInstance(this.objektiKod)) 
            { bool pergjigje = mbushAmortizimTrupiObjekt(moduliAsete.ktheAmortizimTrupiAmortFunditSipasSerialit(idNdermarrje, dataMaksimaleZgjedhje, idLlojStandarti, idSeriali, idRenditje));

                return pergjigje; }
        }

        public clsMesazh krijoTrupAmortizimiPerImport(string kodArt, string magazina, string serial, double amortVjetor, double amortGjithsej, double amortFillestar, int idNdermarrje, int idPerdorues, bool kontrolloserial, List<double> amortizimiFillestar, DateTime dtDok, double gjendja)
        {
            if (kodArt == "")
                return new clsMesazh(false, "Plotesoni kodin e artikullit!");
            clsArtikulli art = new clsArtikulli();
            art.ktheArtikullSipasKoditDheAutorizime(kodArt, idNdermarrje, idPerdorues);
            if (art.IdArtikulli == 0)
                return new clsMesazh(false, String.Format("Artikulli me kod {0} nuk ekziston, ose nuk keni autorizime per te!", kodArt));

            if (!art.Aktiv)
                return new clsMesazh(false, String.Format("Artikulli me kod {0} nuk eshte aktiv!", art.KodArtikulli));

            if (!art.LlojiArt)
                return new clsMesazh(false, String.Format("Artikulli me kod {0} nuk eshte artikull afatgjate!", art.KodArtikulli));

            if ( !art.MeRezerveRivleresimi && this.objektiKod == enumObjekteAmortizimi.REZERVA)
               return new clsMesazh(false, String.Format("Artikulli me kod {0} nuk eshte me rezerve!", art.KodArtikulli));


            this.idArtikulli = art.IdArtikulli;
            this.emertimi = art.PershkrimArtikulli;

            if (magazina == "")
                return new clsMesazh(false, "Plotesoni magazinen!");

            DbCore.DbRegjistrim.clsNjesiAdministrative njesiadm = new DbCore.DbRegjistrim.clsNjesiAdministrative(magazina, idNdermarrje, idPerdorues);

            if (njesiadm.IdNjesiAdministrative <= 0)
                return new clsMesazh(false, String.Format("Magazina me kod {0} nuk ekziston, ose nuk keni autorizime per te!", magazina));

            if (!njesiadm.Aktiv)
                return new clsMesazh(false, "Magazina nuk eshte aktive!");

            this.IdNjesiAdministrative = njesiadm.IdNjesiAdministrative;

            if (serial != null && serial != "")
            {
                clsAQTSeriale seriali = new clsAQTSeriale();
                seriali.merrAQTSerialSipasKodAQT(serial, idNdermarrje);
                if (seriali.IdAQTSerial <= 0)
                    return new clsMesazh(false, "Seriali " + serial + " nuk ekziston!");
                if (seriali.IdAQTArt != this.idArtikulli)
                    return new clsMesazh(false, "Seriali " + serial + " nuk i perket artikullit te caktuar!");
                IdAQTSeriali = seriali.IdAQTSerial;
            }
            else IdAQTSeriali = 0;

            if (kontrolloserial && IdAQTSeriali == 0)
                return new clsMesazh(false, "Ka artikuj pa seriale ne gride");

            this.amortizimiGjithsej = amortGjithsej;
            this.amortizimiVjetor = amortVjetor;
            amortizimiFillestar.Add(amortFillestar);

            if (this.IdAQTSeriali > 0 && !colSerialetMagazine.ekzistonSerialetMagazineSipasIDSerialIDMag(this.idAQTSeriali, this.idNjesiAdministrative, idNdermarrje))
                return new clsMesazh(false, "Seriali " + serial + " nuk ka gjendje ne magazinen " + magazina + "!");

            if (this.objektiKod == enumObjekteAmortizimi.ASETE)
            {
                if (this.IdAQTSeriali > 0)
                    this.vleftaGjendje = DbCore.DbAsete.clsSerialetMagazine.ktheSerialetMagazineSipasIDSerialDheDates(this.IdAQTSeriali, idNdermarrje, dtDok, 0);
                else this.vleftaGjendje = DbCore.DbAsete.clsSerialetMagazine.ktheSerialetMagazineGjendjeTotaleArtikulliNeMagazine(art.IdArtikulli, idNdermarrje, this.idNjesiAdministrative, dtDok);
            }
            else this.vleftaGjendje = gjendja;

            if (this.amortizimiVjetor > this.vleftaGjendje)
                return new clsMesazh(false, "Amortizimi vjetor nuk duhet te jete me i madh se gjendja!");
            if (this.amortizimiGjithsej > this.vleftaGjendje)
                return new clsMesazh(false, "Amortizimi gjithsej nuk duhet te jete me i madh se gjendja!");
            if (this.amortizimiVjetor > this.amortizimiGjithsej)
                return new clsMesazh(false, "Amortizimi vjetor nuk duhet te jete me i madh se amortizimi gjithsej!");
            if (dtDok.Day == 1 && dtDok.Month == 1 && this.amortizimiVjetor != this.amortizimiGjithsej)
                return new clsMesazh(false, "Amortizimi vjetor  duhet te jete i barabarte me amortizimi gjithsej!");

            return new clsMesazh(true, "Trupi i amortizimit u krijua me sukses!");
        }

        /// <summary>
        /// Metode publike per krijimin e nje objekt klone me hapesira pointimi te ndryshe nga objekti qe po e krijon
        /// </summary>
        /// <returns>Kthen nje objekt identik me ate qe po e krijon</returns>
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        /// <summary>
        /// Metode publike per krijimin e nje objekt klone me hapesira pointimi te ndryshe nga objekti qe po e krijon
        /// </summary>
        /// <returns>Kthen nje objekt identik me ate qe po e krijon</returns>
        public clsAmortizimiTrupiAbstract DeepClone()
        {
            clsAmortizimiTrupiAbstract objektTrupi = (clsAmortizimiTrupiAbstract)this.Clone();
            objektTrupi.Artikull = (clsArtikulli)this.Artikull?.Clone();
            return objektTrupi;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// MODULI ASETE:
        /// Ruan objektin e rreshtit te trupit te amortizimit.
        /// </summary>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen True nese ruajtja perfundon me sukses, ose False nese ruajtja jep gabim.</returns>
        private clsMesazh ruajAmortizimiTrupiTransaksion()
        {
            clsDatabazeAseteAbstract moduliAsete = clsDatabazeAseteAbstract.krijoInstance(this.objektiKod);
            clsMesazh pergjigja = moduliAsete.ruajAmortizimiTrupi(out idAmortizimiTrupi, idAmortizimKoka, nrRendor, idArtikulli, idArtikull_LlojAmortizimi, dateAmortizimi, dateMePareAmortizimi, dateNdryshimStatusMagazine, idNjesiAdministrative,
                normaAmortizimi, amortizimiShtese, amortizimiGjithsej, amortizimiVjetor, vleftaPlusMinus, hdAmortizimGjithsej, hdAmortizimVjetor, vleftaGjendje, diteAmortizimi, idAQTSeriali, vleftaShteseRivleresim);
            return pergjigja;
        }

        /// <summary>
        /// MODULI ASETE:
        /// Modifikon objektin e rreshtit te trupit te amortizimit ne transaksion.
        /// </summary>
        /// <param name="moduliAsete">(clsDatabazeAsete) Merr objektin e transaksionit qe te lidhi veprimet me njera tjetren.</param>
        /// <returns>Kthen True nese modifikimi perfundon me sukses, ose False nese ruajtja jep gabim.</returns>
        private clsMesazh modifikoAmortizimiTrupiTransaksionPerRillogaritje(DbData dbData)
        {
            clsDatabazeAseteAbstract moduliAsete = clsDatabazeAseteAbstract.krijoInstance(this.objektiKod, dbData);
            clsMesazh pergjigja = moduliAsete.modifikoAmortizimiTrupiPerRillogaritje(idAmortizimiTrupi, idAmortizimKoka, dateMePareAmortizimi, dateNdryshimStatusMagazine, idNjesiAdministrative,
                normaAmortizimi, amortizimiShtese, amortizimiGjithsej, amortizimiVjetor, vleftaPlusMinus, hdAmortizimGjithsej, hdAmortizimVjetor, vleftaGjendje, diteAmortizimi, vleftaShteseRivleresim);
            return pergjigja;
        }

        #endregion
    }
}