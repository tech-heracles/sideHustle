using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using DbCore.IMBUtils.Extensions;
using LiquidEngine.Tools;
using DbCore.IMBUtils.Messages;
using DbCore.IMBUtils.Fiskalizimi.Controls;

namespace DbCore.DbAdmin
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne nje ndermarrje.
    ///  (Te dhenat  merren nga tabela : T_NDERMARJE)
    /// </summary>
    public class clsNdermarrje : IDisposable
    {
        #region Atributet
        private int idNdermarrje;
        private String ndermarrjeKodi;
        private String ndermarrjePershkrimi;
        private String ndermarrjeVendi;
        private String ndermarrjeNipt;
        private int ndermarrjeMonedha;
        private String ndermarrjeMonedhaPershkrimi;
        private int ndermarrjeQyteti;
        private String ndermarrjeQytetiPershkrimi;
        private String ndermarrjeTel;
        private String ndermarrjeFax;
        private String ndermarrjeEMail;
        private String ndermarrjeLicenca;
        private String ndermarrjeKodiFiskal;
        //private byte[] ndermarrjeLogo;
        private System.Drawing.Image ndermarrjeLogoImage;
        private System.Drawing.Imaging.ImageFormat ndermarrjeLogoImageFormat;
        private int idPerdoruesi;
        private int idViti;
        private int llojNdermarje;
        private int idLicenca;
        private int idStatusDok;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private int idTakse;
        private string nrTvsh;
        private int lloji;
        private bool disposed = false;
        private bool logoLoaded = false;
        private bool prind;
        private int idPrindi;
        private int idGrupi;
        private string kodFurnitori;
        private string emerFurnitori;
        private string nrLlogariFurnitori;
        private bool arkiva;
        private double limitiShitjes;
        private bool ownShop;
        private bool Logu;
        private bool Fiskalizim;
        private int sizeMaxArkiva;
        private int raportuesi;
        private int nivelstrukture; // 1 kur ndermarrja nuk ka raportues (pa prind), 2 kur ndermarrja ka nje raportues qe nuk ka raportues me lart (prind fundor), ne te kundert 3 
        private DataRow rreshti;
        private string kodilicenca;
        private string kodbiznesi;
        private string pathname;
        private bool meTvsh;
        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsNdermarrje(int idndermarrje, String ndermarrjekodi, String ndermarrjepershkrimi, String ndermarrjevendi, String ndermarrjenipt, int ndermarrjemonedha, int ndermarrjeqyteti, String ndermarrjetel, String ndermarrjefax, String ndermarrjeemail, String ndermarrjelicenca, String ndermarrjekodifiskal, int idperdoruesi, int idviti, int llojndermarje, int idlicenca, int idstatusdok, int idtakse, string nrtvsh, int lloji, bool prind, int idprindi, int idgrupi, string kodFurnitori, string emerFurnitori, string nrLlogariFurnitori, double limitishitjes, bool ownshop, bool logu, int raportues, int nivelstrukture,bool fiskalizim,string kodbiznesi,string pathname, bool meTvsh)
        {
            idNdermarrje = idndermarrje;
            ndermarrjeKodi = ndermarrjekodi;
            ndermarrjePershkrimi = ndermarrjepershkrimi;
            ndermarrjeVendi = ndermarrjevendi;
            ndermarrjeNipt = ndermarrjenipt;
            ndermarrjeMonedha = ndermarrjemonedha;
            ndermarrjeQyteti = ndermarrjeqyteti;
            ndermarrjeTel = ndermarrjetel;
            ndermarrjeFax = ndermarrjefax;
            ndermarrjeEMail = ndermarrjeemail;
            ndermarrjeLicenca = ndermarrjelicenca;
            ndermarrjeKodiFiskal = ndermarrjekodifiskal;
            idPerdoruesi = idperdoruesi;
            idViti = idviti;
            llojNdermarje = llojndermarje;
            idLicenca = idlicenca;
            idStatusDok = idstatusdok;
            idTakse = idtakse;
            nrTvsh = nrtvsh;
            this.lloji = lloji;
            this.prind = prind;
            this.idPrindi = idprindi;
            this.idGrupi = idgrupi;
            this.limitiShitjes = limitishitjes;
            this.kodFurnitori = kodFurnitori;
            this.emerFurnitori = emerFurnitori;
            this.nrLlogariFurnitori = nrLlogariFurnitori;
            this.ownShop = ownshop;
            this.Logu = logu;
            this.Fiskalizim = fiskalizim;
            this.raportuesi = raportues;
            this.nivelstrukture = nivelstrukture;
            this.kodbiznesi = kodbiznesi;
            this.pathname = pathname;
            this.meTvsh = meTvsh;
        }

        /// <summary>
        ///  Konstruktori i klases
        /// </summary>
        public clsNdermarrje(String ndermarrjekodi, String ndermarrjepershkrimi, String ndermarrjevendi, String ndermarrjenipt, int ndermarrjemonedha, int ndermarrjeqyteti, String ndermarrjetel, String ndermarrjefax, String ndermarrjeemail, String ndermarrjelicenca, String ndermarrjekodifiskal, int idperdoruesi, int idviti, int llojndermarje, int idlicenca, int idstatusdok, int idtakse, string nrtvsh, int lloji, bool prind, int idprindi, int idgrupi, string kodFurnitori, string emerFurnitori, string nrLlogariFurnitori, double limitishitjes, bool ownshop, bool logu, int raportues, int nivelstrukture,bool fiskalizim,string kodbiznesi,string pathname)
        {
            ndermarrjeKodi = ndermarrjekodi;
            ndermarrjePershkrimi = ndermarrjepershkrimi;
            ndermarrjeVendi = ndermarrjevendi;
            ndermarrjeNipt = ndermarrjenipt;
            ndermarrjeMonedha = ndermarrjemonedha;
            ndermarrjeQyteti = ndermarrjeqyteti;
            ndermarrjeTel = ndermarrjetel;
            ndermarrjeFax = ndermarrjefax;
            ndermarrjeEMail = ndermarrjeemail;
            ndermarrjeLicenca = ndermarrjelicenca;
            ndermarrjeKodiFiskal = ndermarrjekodifiskal;
            idPerdoruesi = idperdoruesi;
            idViti = idviti;
            llojNdermarje = llojndermarje;
            idLicenca = idlicenca;
            idStatusDok = idstatusdok;
            idTakse = idtakse;
            nrTvsh = nrtvsh;
            this.lloji = lloji;
            this.prind = prind;
            this.idPrindi = idprindi;
            this.limitiShitjes = limitishitjes;
            this.idGrupi = idgrupi;
            this.kodFurnitori = kodFurnitori;
            this.emerFurnitori = emerFurnitori;
            this.nrLlogariFurnitori = nrLlogariFurnitori;
            this.ownShop = ownshop;
            this.Logu = logu;
            this.Fiskalizim = fiskalizim;
            this.raportuesi = raportues;
            this.nivelstrukture = nivelstrukture;
            this.kodbiznesi = kodbiznesi;
            this.pathname = pathname;

        }

        /// <summary>
        /// konstruktor me 1 parameter id
        /// </summary>
        /// <param name="id">id e ndermarrjes</param>
        public clsNdermarrje(int id)
        {
            //System.Diagnostics.Trace.WriteLine("clsNdermarrje: Costructor");
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            mbushNdermarrja(data.merrNdermarrje(id));
            data.Dispose();
        }

        public clsNdermarrje(int id, clsDatabaseAdmin data)
        {
            mbushNdermarrja(data.TransCache.getNdermarrjeById(id, data));
        }

        /// <summary>
        /// konstruktor me 1 parameter string 
        /// </summary>
        /// <param name="kodi">kdoi i ndermarrjes</param>
        public clsNdermarrje(string kodi)
        {
            //System.Diagnostics.Trace.WriteLine("clsNdermarrje: Costructor");
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            mbushNdermarrja(data.merrNdermarrje(kodi));
            data.Dispose();
        }

        /// <summary>
        /// konstruktor me 2 parameter
        /// </summary>
        /// <param name="kodi">kodi i ndermarrjes</param>
        /// <param name="dbAdmin">clsDatabaseAdmin</param>
        public clsNdermarrje(string kodi, clsDatabaseAdmin dbAdmin)
        {
            mbushNdermarrja(dbAdmin.TransCache.getNdermarrje(kodi, dbAdmin));
        }
        /// <summary>
        ///  Konstruktori default i klases
        /// </summary>
        public clsNdermarrje()
        {
            //System.Diagnostics.Trace.WriteLine("clsNdermarrje: Costructor");
        }

        public clsNdermarrje(DataRow rreshti)
        {
            
            mbushNdermarrja(rreshti);
        }

        ~clsNdermarrje()
        {
            //System.Diagnostics.Trace.WriteLine("clsNdermarrje: Destructor");
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposeManagedResources)
        {
            // process only if mananged and unmanaged resources have
            // not been disposed of.
            if (!disposed)
            {
                //System.Diagnostics.Trace.WriteLine("clsNdermarrje: Resources not disposed");
                if (disposeManagedResources)
                {
                    //System.Diagnostics.Trace.WriteLine("clsNdermarrje: Closing connection");

                    //System.Diagnostics.Trace.WriteLine("clsNdermarrje: Disposing managed resources");
                    // dispose managed resources                                                                
                    if (ndermarrjeLogoImage != null)
                    {
                        ndermarrjeLogoImage.Dispose();
                        ndermarrjeLogoImage = null;
                    }
                }
                // dispose unmanaged resources
                //System.Diagnostics.Trace.WriteLine("clsNdermarrje: Disposing unmanaged resouces");
                disposed = true;
            }
            else
            {
                //System.Diagnostics.Trace.WriteLine("clsNdermarrje: Resources already disposed");
            }
        }

        #endregion

        #region Properties

        /// <summary>
        ///  Kthen id e rreshtit perkates qe eshte ne databaze
        /// </summary>
        public int IdNdermarrje
        {
            get { return idNdermarrje; }
            set { idNdermarrje = value; }
        }
        /// <summary>
        /// limiti i shitjes per kupon dhe fature tatimore
        /// </summary>
        public double LimitiShitjes
        {
            get
            {
                return limitiShitjes;
            }
            set
            {
                limitiShitjes = value;
            }
        }
        /// <summary>
        ///  Kthen kodin e ndermarrjes
        /// </summary>
        public String NdermarrjeKodi
        {
            get { return ndermarrjeKodi; }
            set { ndermarrjeKodi = value; }
        }

        /// <summary>
        ///  Kthen kodin e ndermarrjes
        /// </summary>
        public String NdermarrjePershkrimi
        {
            get { return ndermarrjePershkrimi; }
            set { ndermarrjePershkrimi = value; }
        }

        /// <summary>
        ///  Kthen vendin e ndermarrjes
        /// </summary>
        public String NdermarrjeVendi
        {
            get { return ndermarrjeVendi; }
            set { ndermarrjeVendi = value; }
        }

        /// <summary>
        ///  Kthen NIPTI-n e ndermarrjes
        /// </summary>
        public String NdermarrjeNipt
        {
            get { return ndermarrjeNipt; }
            set { ndermarrjeNipt = value; }
        }

        /// <summary>
        ///  Kthen kodin e monedhes e ndermarrjes
        /// </summary>
        public int NdermarrjeMonedha
        {
            get { return ndermarrjeMonedha; }
            set { ndermarrjeMonedha = value; }
        }

        /// <summary>
        ///  Kthen pershkrimin e monedhes e ndermarrjes
        /// </summary>
        public String NdermarrjeMonedhaPershkrimi
        {
            get { return ndermarrjeMonedhaPershkrimi; }
            set { ndermarrjeMonedhaPershkrimi = value; }
        }

        /// <summary>
        ///  Kthen kodin e qytetit te ndermarrjes
        /// </summary>
        public int NdermarrjeQyteti
        {
            get { return ndermarrjeQyteti; }
            set { ndermarrjeQyteti = value; }
        }

        /// <summary>
        ///  Kthen pershkrimin e qytetit te ndermarrjes
        /// </summary>
        public String NdermarrjeQytetiPershkrimi
        {
            get { return ndermarrjeQytetiPershkrimi; }
            set { ndermarrjeQytetiPershkrimi = value; }
        }

        /// <summary>
        ///  Kthen telefonin e ndermarrjes
        /// </summary>
        public String NdermarrjeTel
        {
            get { return ndermarrjeTel; }
            set { ndermarrjeTel = value; }
        }

        /// <summary>
        ///  Kthen faxin e ndermarrjes
        /// </summary>
        public String NdermarrjeFax
        {
            get { return ndermarrjeFax; }
            set { ndermarrjeFax = value; }
        }

        /// <summary>
        ///  Kthen emailin e ndermarrjes
        /// </summary>
        public String NdermarrjeEMail
        {
            get { return ndermarrjeEMail; }
            set { ndermarrjeEMail = value; }
        }

        /// <summary>
        ///  Kthen licencen e ndermarrjes
        /// </summary>
        public String NdermarrjeLicenca
        {
            get { return ndermarrjeLicenca; }
            set { ndermarrjeLicenca = value; }
        }

        /// <summary>
        ///  Kthen kodin fiskal te ndermarrjes
        /// </summary>
        public String NdermarrjeKodiFiskal
        {
            get { return ndermarrjeKodiFiskal; }
            set { ndermarrjeKodiFiskal = value; }
        }

        /// <summary>
        ///  Kthen ID-ne e perdoruesit qe ka krijuar  ndermarrjen
        /// </summary>
        public int IdPerdoruesi
        {
            get { return idPerdoruesi; }
            set { idPerdoruesi = value; }
        }

        /// <summary>
        ///  Kthen ID-ne e vitit qe i caktohet kesaj ndermarrje, mbase nuk duhet
        /// </summary>
        public int IdViti
        {
            get { return idViti; }
            set { idViti = value; }
        }
        /// <summary>
        ///  Kthen llojin e ndermarjes
        /// </summary>
        public int LlojNdermarje
        {
            get { return llojNdermarje; }
            set { llojNdermarje = value; }
        }
        /// <summary>
        ///  Kthen id e licences
        /// </summary>
        public int IdLicenca
        {
            get { return idLicenca; }
            set { idLicenca = value; }
        }
        public int IdStatusDok
        {
            get { return idStatusDok; }
            set { idStatusDok = value; }
        }
        public int IdTakse
        {
            get { return idTakse; }
            set { idTakse = value; }
        }
        public byte[] NdermarrjeLogo
        {
            get
            {
                if (ndermarrjeLogoImage == null && logoLoaded)
                    return null;
                if (idNdermarrje == 0)
                    return null;

                if (ndermarrjeLogoImage == null && !logoLoaded)
                {
                    byte[] logoja = merrLogo();
                    if (logoja != null && ((byte[])logoja).Length != 0)
                    {
                        using(MemoryTributary tempMs = new MemoryTributary(logoja))
                        {
                            if (tempMs != null)
                                ndermarrjeLogoImage = GetReducedImage(tempMs);
                        }
                    }
                    else
                        return null;
                }
                using(MemoryTributary ms = new MemoryTributary())
                {
                    ndermarrjeLogoImage.Save(ms, ndermarrjeLogoImageFormat);
                    return ms.ToArray();
                }
            }
            set
            {
                if (value != null)
                    using (MemoryTributary msNew = new MemoryTributary(value))
                        ndermarrjeLogoImage = GetReducedImage(msNew);
                else
                    ndermarrjeLogoImage = null;
                logoLoaded = true;
            }
        }

        private Image GetReducedImage(MemoryTributary memoryTributary)
        {
            using (Image logo = Image.FromStream(memoryTributary))
            {
                ndermarrjeLogoImageFormat = logo.RawFormat;
                int newWidth = logo.Width <= 256 ? logo.Width : 256;
                int newHeight = logo.Width <= 256 ? logo.Height : Convert.ToInt32((logo.Height * 256) / logo.Width);
                return logo.GetThumbnailImage(newWidth, newHeight, null, IntPtr.Zero);
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
        public string NrTvsh
        {
            get { return nrTvsh; }
            set { nrTvsh = value; }
        }
        /// <summary>
        /// sherben per te dalluar llojin e ndermarjes 1-default,2-buxhetor,3-kosove
        /// </summary>
        public int Lloji
        {
            get
            {
                return lloji;
            }
            set
            {
                lloji = value;
            }
        }
        /// <summary>
        /// tregon nese kjo ndermarje eshte own shop i vodafonit
        /// </summary>
        public bool OwnShop
        {
            get
            {
                return ownShop;
            }
            set
            {
                ownShop = value;
            }
        }
        /// <summary>
        /// tregon nese kjo ndermarje eshte ndermarje prind apo jo
        /// </summary>
        public bool Prind
        {
            get
            {
                return prind;
            }
            set
            {
                prind = value;
            }
        }
        /// <summary>
        /// tregon id e prindit te kesaj ndermarje
        /// </summary>
        public int IdPrindi
        {
            get
            {
                return idPrindi;
            }
            set
            {
                idPrindi = value;
            }
        }
        /// <summary>
        /// id e grupit te ndermarjes
        /// </summary>
        public int IdGrupi
        {
            get
            {
                return idGrupi;
            }
            set
            {
                idGrupi = value;
            }
        }
        /// <summary>
        /// kodi i furnitori qe do kalohet tek ndermarjet bij
        /// </summary>
        public string KodFurnitori
        {
            get
            {
                return kodFurnitori;
            }
            set
            {
                kodFurnitori = value;
            }
        }
        /// <summary>
        /// emertimi i furnitorit qe do i kalohet tek ndermarja bij
        /// </summary>
        public string EmerFurnitori
        {
            get
            {
                return emerFurnitori;
            }
            set
            {
                emerFurnitori = value;
            }
        }
        /// <summary>
        /// nr i llogarise se furnitorit qe do i kalohet tek ndermarja bij
        /// </summary>
        public string NrLlogariFurnitori
        {
            get
            {
                return nrLlogariFurnitori;
            }
            set
            {
                nrLlogariFurnitori = value;
            }
        }


        public bool isArkiva
        {
            get
            {
                return arkiva;
            }
            set
            {
                arkiva = value;
            }
        }

        /// <summary>
        /// tregon nese kjo do ruhet logu i veprimeve te klientit per kete ndermarrje
        /// </summary>
        public bool LogNdermarrje
        {
            get
            {
                return Logu;
            }
            set
            {
                Logu = value;
            }
        }
        public bool Fiskalizimi
        {
            get
            {
                return Fiskalizim;
            }
            set
            {
                Fiskalizim = value;
            }
        }
        /// <summary>
        /// Madhesia maksimale e skedareve qe mund te ngarkohen te arkiva
        /// </summary>
        public int SizeMaxArkiva
        {
            get
            {
                return sizeMaxArkiva;
            }
            set
            {
                sizeMaxArkiva = value;
            }
        }

        /// <summary>
        /// tregon raportuesin e kesaj ndermarje
        /// </summary>
        public int Raportuesi
        {
            get
            {
                return raportuesi;
            }
            set
            {
                raportuesi = value;
            }
        }

        public int Nivelstrukture {
            get { return nivelstrukture; }
            set { nivelstrukture = value; }
        }
        public string Kodbiznesi
        {
            get
            {
                return kodbiznesi;
            }
            set
            {
                kodbiznesi = value;
            }
        }
        public string Pathname
        {
            get
            {
                return pathname;
            }
            set
            {
                pathname = value;
            }
        }
        public bool MeTvsh
        {
            get
            {
                return meTvsh;
            }
            set
            {
                meTvsh = value;
            }
        }
        //public string KodiLicenca
        //{
        //    get
        //    {
        //        return kodilicenca;
        //    }
        //    set
        //    {
        //        kodilicenca = value;
        //    }
        //}

        #endregion

        #region Metoda Publike

        /// <summary>
        /// Kthen true apo false per arkiven ne varesi te ndermarrjes
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <returns></returns>
        public static bool meArkive(int idNdermarrje)
        {
            using (clsDatabaseAdmin db = new clsDatabaseAdmin())
            {
                return db.merrMeArkive(idNdermarrje);
            }
        }

        public static bool eshtePrind(int idnderamrje)
        {
            using (clsDatabaseAdmin dbadmin = new clsDatabaseAdmin())
            {
                return dbadmin.eshtePrindNdermarje(idnderamrje);
            }
        }

        public static bool EshteRaportuese(int idNdermarrje)
        {
            using (clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin())
            {
                return dbAdmin.EshteNdermarrjeRaportuese(idNdermarrje);
            }
        }

        public static int ktheLicenceNdermarrje(int idnderamrje)
        {
            using (clsDatabaseAdmin dbadmin = new clsDatabaseAdmin())
            {
                return dbadmin.ktheLicenceNdermarrje(idnderamrje);
            }
        }

        /// <summary>
        /// kthen llojin e ndermarrjes ne baze te id-se se saj
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <returns></returns>
        public static int ktheLlojNdermSipasID(int idNdermarrje)
        {
            using (clsDatabaseAdmin dbadmin = new clsDatabaseAdmin())
            {
                return dbadmin.ktheLlojNdermarrjeSipasID(idNdermarrje);
            }
        }

        public static int merrIdNdermarrjeOwn()
        {
            using (var dbAdmin = new clsDatabaseAdmin())
            {
                return dbAdmin.merrIdNdermarrjeOwn();
            }
        }
        public static int merrIdNdermarrjeOwn(clsDatabaseAdmin dbAdmin)
        {
            return dbAdmin.merrIdNdermarrjeOwn();
        }

        /// <summary>
        /// kthen monedhen e ndermarrjes ne baze te id-se se saj
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <returns></returns>
        public static int ktheIdMonedheNdermSipasID(int idNdermarrje)
        {
            using (clsDatabaseAdmin dbadmin = new clsDatabaseAdmin())
            {
                return ktheIdMonedheNdermSipasID(idNdermarrje, dbadmin);
            }
        }

        public static int ktheIdMonedheNdermSipasID(int idNdermarrje, clsDatabaseAdmin dbadmin)
        {
            return dbadmin.ktheidMonedheNdermSipasID(idNdermarrje);
        }
        /// <summary>
        /// Kthen pathin e arkives ne varesi te ndermarrjes
        /// </summary>
        /// <param name="idNdermarrje">id e ndermarrjes</param>
        /// <returns></returns>
        public static int merrMaxSizeArkive(int idNdermarrje)
        {
            using (clsDatabaseAdmin db = new clsDatabaseAdmin())
            {
                return db.merrMaxSizeArkive(idNdermarrje);
            }
        }

        public static int ktheIdNdermarrje(string kodNdermarrje)
        {
            clsDatabaseAdmin db = new clsDatabaseAdmin();
            int idNdermarrje = db.merrIdNdermarrje(kodNdermarrje);
            db.Dispose();
            return idNdermarrje;
        }
        
        /// <summary>
        /// VODAFONE
        /// Metode qe perdoret vetem per integrimin me VODAFONE per marrjen e ndermarrjes meme sipas emrin te ndermarrjes meme (e vendosur direkt ne SP)
        /// </summary>
        /// <returns></returns>
        public static int ktheIdNdermarrjeMeme()
        {
            clsDatabaseAdmin db = new clsDatabaseAdmin();
            int idNdermarrje = db.merrIdNdermarrjeMeme();
            db.Dispose();
            return idNdermarrje;
        }

        /// <summary>
        /// merr IdNdermarrjeMeme
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <returns></returns>
        public static int ktheIdNdermarrjeMeme(int idNdermarje)
        {
            clsDatabaseAdmin db = new clsDatabaseAdmin();
            int idNdermarrje = db.merrIdNdermarrjeMeme(idNdermarje);
            db.Dispose();
            return idNdermarrje;
        }

        /// <summary>
        /// merr id ndermarrje raportimi
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <returns></returns>
        public static int ktheIdNdermarrjeRaportimi(int idNdermarje)
        {
            clsDatabaseAdmin db = new clsDatabaseAdmin();
            int idNdermarrje = db.merrIdNdermarrjeRaportimi(idNdermarje);
            db.Dispose();
            return idNdermarrje;
        }

        public static int ktheIdNdermarrjeRaportimiRoot(int idNdermarrje)
        {
            using (clsDatabaseAdmin db = new clsDatabaseAdmin())
                return db.merrIdNdermarrjeRaportimiRoot(idNdermarrje);

        }

        public static int ktheNivelStruktureSipasIdNderm(int idNdermarrje)
        {
            using (clsDatabaseAdmin db = new clsDatabaseAdmin())
                return db.merrNivelStrukturePerIdNderm(idNdermarrje);
        }

        /// <summary>
        /// merr Pershkrimin e ndermarrjes
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <returns></returns>

        public static string merrPershkrimNdermarrje(int idNdermarrje)
        {
            clsDatabaseAdmin db = new clsDatabaseAdmin();
            String pershkrimi = db.merrPershkrimNdermarrje(idNdermarrje);
            db.Dispose();
            return pershkrimi;
        }

        public static string merrEmailTeBijave(int idNdermarrje)
        {
            using (clsDatabaseAdmin dba = new clsDatabaseAdmin())
                return dba.merrEmailTeBijavePerNdermarrjen(idNdermarrje);
        }

        public clsMesazh backup(DataTable dt, int idlicenca)
        {
            clsMesazh mes = new clsMesazh();

            string inidnderm = "";
            foreach (DataRow dr in dt.Rows)
            {
                int idndermarje = Convert.ToInt32(dr["IdNdermarrje"]);
                inidnderm += idndermarje + ",";
            }

            if (inidnderm != "")
                inidnderm = inidnderm.Substring(0, inidnderm.Length - 1);

            clsLicenca licenca = new clsLicenca(idlicenca);
            DataSet ds = krijods(inidnderm);
            if (ds == null)
            {
                mes.PershkrimMesazhi = "Back up-i perfundoi me gabime!";
                return mes;
            }
            string pathDir = System.Web.HttpContext.Current.Server.MapPath(null) + @"\Licencat\";
            DirectoryExtension.CreateDirIfNotExists(pathDir);
            pathDir += licenca.KodLicenca + "\\";
            DirectoryExtension.CreateDirIfNotExists(pathDir);
            string filenamePath = pathDir + "backup" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".xml";
            string zipname = pathDir + "zip" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".rar";
            try
            {
                ds.WriteXml(filenamePath, XmlWriteMode.WriteSchema);
                zip(filenamePath, zipname);
                mes.PershkrimMesazhi = "Back up-i perfundoi me sukses!";
                mes.Status = true;
                string[] filePaths = Directory.GetFiles(pathDir, "*.rar");
            }
            catch (Exception)
            {
                mes.PershkrimMesazhi = "Back up-i perfundoi me gabime!";
                return mes;
            }
            return mes;
        }
        
        /// <summary>
        ///  Sherben per ruajtjen e objektit ndermarrje ne databaze. Thirret funksioni
        ///  <see cref="DbCore.DbAdmin.clsNdermarrje.ruajNdermarrje"/> 
        /// </summary>
        public clsMesazh ruaj(String konfigurimiDefault, int idndermnga)
        {
            clsMesazh u_ruajt = ruajNdermarrje(this, konfigurimiDefault, idndermnga);
            return u_ruajt;
        }

        /// <summary>
        ///  Sherben per modifikimin e objektit ndermarrje ne databaze. Thirret funksioni
        ///  <see cref="DbCore.DbAdmin.clsNdermarrje.modifikoNdermarrje"/> 
        /// </summary>
        public clsMesazh modifiko()
        {
            clsMesazh u_modifikua = modifikoNdermarrje(this);
            return u_modifikua;
        }
        //public clsMesazh Ruajpath(int idnderm,string path)
        //{
        //    clsMesazh mesazh = RuajNdermPath(idnderm,path);
        //    return mesazh;
        //}
        /// <summary>
        ///  Sherben per fshirjen e objektit ndermarrje ne databaze. Thirret funksioni
        ///  <see cref="DbCore.DbAdmin.clsDatabaseAdmin.fshiNdermarrje"/> 
        /// </summary>
        public clsMesazh fshiNgaBD()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            clsMesazh u_fshi = data.fshiNdermarrje(this.IdNdermarrje);
            data.Dispose();
            return u_fshi;
        }

        public clsMesazh fshi(int idPeroduresILoguar)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            clsMesazh u_fshi = data.fshiNdermarrjeStatus(this.IdNdermarrje, idPeroduresILoguar);
            data.Dispose();
            return u_fshi;
        }

        public clsMesazh ruajNdermarrje(clsNdermarrje ndermarrje, String konfigurimiDefault, int idndermnga)
        {
            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();
            clsMesazh mesazh;
            dbAdmin.beginTransaksion(0);
            try
            {
                clsMonedha mon = new clsMonedha(ndermarrje.NdermarrjeMonedha, dbAdmin);
               
              
                clsViti vit = new clsViti();
                vit.mbushVitetMet(ndermarrje.IdViti, dbAdmin);

                clsQyteti qytet = new clsQyteti();
                if (ndermarrje.NdermarrjeQyteti != -1)
                    qytet = new clsQyteti(ndermarrje.NdermarrjeQyteti, dbAdmin);

                if ((idndermnga > 0 && !dbAdmin.ekzistonVitPerKeteNdermarrje(idndermnga, vit.KodiViti)))
                {
                    dbAdmin.rollbackTransaksion();
                    return new clsMesazh(false, "Në ndërmarrjen nga po klononi të dhënat nuk ekziston viti: " + vit.KodiViti+"!");
                }

                if (dbAdmin.ekzistonNdermarrjeMeKeteKod(ndermarrje.NdermarrjeKodi))
                {
                    dbAdmin.rollbackTransaksion();
                    return new clsMesazh(false, "Ekziston nje ndermarrje me te njejtin kod!");
                }
                int idN;
                bool klientFiskalizimi = false;
                if (clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizim())
                    klientFiskalizimi = true;
                mesazh = dbAdmin.ruajNdermarrje(out idN, ndermarrje.NdermarrjeKodi, ndermarrje.NdermarrjePershkrimi, ndermarrje.NdermarrjeVendi, ndermarrje.NdermarrjeNipt, ndermarrje.NdermarrjeMonedha, ndermarrje.NdermarrjeQyteti, ndermarrje.NdermarrjeTel, ndermarrje.NdermarrjeFax, ndermarrje.NdermarrjeEMail, ndermarrje.NdermarrjeLicenca, ndermarrje.NdermarrjeKodiFiskal, ndermarrje.IdPerdoruesi, ndermarrje.IdViti, ndermarrje.LlojNdermarje, ndermarrje.IdLicenca, ndermarrje.idStatusDok, ndermarrje.IdTakse, ndermarrje.NdermarrjeLogo, ndermarrje.nrTvsh, ndermarrje.lloji, ndermarrje.prind, ndermarrje.idPrindi, ndermarrje.idGrupi, ndermarrje.kodFurnitori, ndermarrje.emerFurnitori, ndermarrje.nrLlogariFurnitori, ndermarrje.limitiShitjes, ndermarrje.ownShop, ndermarrje.Logu, ndermarrje.raportuesi, ndermarrje.nivelstrukture,ndermarrje.Fiskalizim,ndermarrje.kodbiznesi,ndermarrje.pathname, ndermarrje.meTvsh, klientFiskalizimi);
                if (!mesazh.Status)
                {
                    dbAdmin.rollbackTransaksion();
                    return mesazh;// new clsMesazh(false, mesazh.PershkrimMesazhi);
                }
                ndermarrje.IdNdermarrje = idN;
                mon.IdNdermarje = ndermarrje.IdNdermarrje; //TOCHECK NESTILA - sduhet te kete nevoj se eshte monedha e ndermarrjes
                vit.IdNdermarje = ndermarrje.IdNdermarrje;
                qytet.IdNdermarja = ndermarrje.IdNdermarrje;
                int idM;
                clsMonedha monndernga = new clsMonedha();
                //bool mbushMon = 
                monndernga.mbushMonedhen(mon.KodiMonedha, idndermnga, dbAdmin);
                //if (!mbushMon)
                //{
                //    dbAdmin.rollbackTransaksion();
                //    return new clsMesazh(false, "Ndodhi nje gabim gjate mbushjes se monedhes se ndermarrjes default!");
                //}
                mesazh = dbAdmin.ruajMonedhe(out idM, mon.KodiMonedha, mon.PershkrimiMonedha, mon.AktivMonedha, mon.IdPerdoruesi, monndernga.IdLlogFitimi, monndernga.IdLlogHumbje, mon.IdNdermarje, 1, mon.IdFormatNrKursi);
                if (!mesazh.Status)
                {
                    dbAdmin.rollbackTransaksion();
                    return mesazh;// new clsMesazh(false, mesazh.PershkrimMesazhi);
                }
                ndermarrje.ndermarrjeMonedha = idM;
                int idV;
                mesazh = dbAdmin.ruajVit(out idV, vit.KodiViti, vit.FillimiViti, vit.MbarimiViti, vit.PeriudhaLloji, vit.PeriudhaHapjes, vit.PeriudhaMbylljes, vit.IdPerdoruesi, vit.IdKonfig, vit.IdNdermarje, vit.IdStatusDok, vit.MbyllurMe, vit.IdLlogMbylljeViti);
                if (!mesazh.Status)
                {
                    dbAdmin.rollbackTransaksion();
                    return mesazh;
                }
                vit.IdViti = idV;
                ndermarrje.idViti = idV;
                DbCore.DbAdmin.clsKurset kursi = new DbCore.DbAdmin.clsKurset(0, 1, vit.FillimiViti, 1, idM, 1, "Kursi1");
                mesazh = kursi.krijoKurs(dbAdmin);
                if (!mesazh.Status)
                {
                    dbAdmin.rollbackTransaksion();
                    return mesazh;
                }
                int idQ = -1;
                if (qytet.KodiQyteti != null)
                {
                    mesazh = dbAdmin.ruajQytet(out idQ, qytet.KodiQyteti, qytet.EmriQyteti, qytet.IdNdermarja, qytet.IdPerdoruesi, qytet.IdStatusDok);
                    if (!mesazh.Status)
                    {
                        dbAdmin.rollbackTransaksion();
                        return mesazh;
                    }
                }
                ndermarrje.ndermarrjeQyteti = idQ;
                klientFiskalizimi = false;
                if (clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizim())
                    klientFiskalizimi = true;
                mesazh = dbAdmin.modifikoNder(ndermarrje.idNdermarrje, ndermarrje.NdermarrjeKodi, ndermarrje.NdermarrjePershkrimi, ndermarrje.NdermarrjeVendi, ndermarrje.NdermarrjeNipt, ndermarrje.NdermarrjeMonedha, ndermarrje.NdermarrjeQyteti, ndermarrje.NdermarrjeTel, ndermarrje.NdermarrjeFax, ndermarrje.NdermarrjeEMail, ndermarrje.NdermarrjeLicenca, ndermarrje.NdermarrjeKodiFiskal, ndermarrje.IdPerdoruesi, ndermarrje.IdViti, ndermarrje.LlojNdermarje, ndermarrje.IdLicenca, ndermarrje.idStatusDok, ndermarrje.IdTakse, ndermarrje.NdermarrjeLogo, ndermarrje.nrTvsh, ndermarrje.prind, ndermarrje.idPrindi, ndermarrje.idGrupi, ndermarrje.limitiShitjes, ndermarrje.ownShop, ndermarrje.Logu, ndermarrje.raportuesi, ndermarrje.nivelstrukture,ndermarrje.Fiskalizim,ndermarrje.Kodbiznesi,ndermarrje.pathname,ndermarrje.meTvsh,klientFiskalizimi);
                if (!mesazh.Status)
                {
                    dbAdmin.rollbackTransaksion();
                    return mesazh;
                }
                mesazh = dbAdmin.ruajDefaultKlonoNrAuto(ndermarrje.IdPerdoruesi, ndermarrje.IdNdermarrje, idndermnga);
                if (!mesazh.Status)
                {
                    dbAdmin.rollbackTransaksion();
                    return mesazh;
                }
                mesazh = dbAdmin.ruajDefaultKlonoFormatNr(ndermarrje.IdPerdoruesi, ndermarrje.IdNdermarrje, idndermnga);
              
                if (!mesazh.Status)
                {
                    dbAdmin.rollbackTransaksion();
                    return mesazh;
                }
                int idstandartamortizimi;//shtimi i standartit shqiptar
                DbAsete.clsDatabazeAsete dbasete = new DbAsete.clsDatabazeAsete(dbAdmin);
                mesazh = dbasete.ruajStandartAmortizimi(out idstandartamortizimi, "Shqiptar", "Standarti Shqiptar", 1, ndermarrje.idNdermarrje, new DateTime(), ndermarrje.idPerdoruesi, ndermarrje.idPerdoruesi);
                if (!mesazh.Status)
                {
                    dbAdmin.rollbackTransaksion();
                    return mesazh;
                }
                mesazh = dbAdmin.ruajDefaultKlonoInfo(ndermarrje.IdPerdoruesi, ndermarrje.IdNdermarrje, idndermnga);
                if (!mesazh.Status)
                {
                    dbAdmin.rollbackTransaksion();
                    return new clsMesazh(false, mesazh.PershkrimMesazhi);
                }
                //mon.IdMonedha = idM;
                // ndermarrje.NdermarrjeMonedha = mon.IdMonedha;
                // modifikoNder(ndermarrje); 

                //   colVitet vitet = new colVitet();
                //   vitet.merrVitet(ndermarrje.IdViti);
                //colVitet vitet = ktheVitPerNdermarrjenRe(ndermarrje.IdViti);
                clsNdermarrjeViti nderviti = new clsNdermarrjeViti();
                mesazh = dbAdmin.shtoDokumentaDefaultNdermarje(ndermarrje.IdNdermarrje, idndermnga);
                if (!mesazh.Status)
                {
                    dbAdmin.rollbackTransaksion();
                    return mesazh;
                }
                mesazh = dbAdmin.shtoRapDesignDefault(ndermarrje.IdNdermarrje, idndermnga);
                if (!mesazh.Status)
                {
                    dbAdmin.rollbackTransaksion();
                    return mesazh;
                }
                nderviti = new clsNdermarrjeViti();
                nderviti.IdViti = vit.IdViti;
                nderviti.NdermarrjeViti = int.Parse(vit.FillimiViti.Year.ToString());
                nderviti.IdNdermarrje = ndermarrje.IdNdermarrje;
                nderviti.NdermarrjeVitiMbyllur = true;
                nderviti.NdermarrjeVitiFillim = DateTime.Parse(vit.FillimiViti.ToString());
                nderviti.NdermarrjeVitiFund = DateTime.Parse(vit.MbarimiViti.ToString());
                nderviti.IdPerdoruesi = ndermarrje.IdPerdoruesi;
                int idNV;
                mesazh = dbAdmin.ruajNdermarrjeReVit(out idNV, nderviti.IdViti, nderviti.Viti, nderviti.IdNdermarrje, nderviti.NdermarrjeVitiMbyllur, nderviti.NdermarrjeVitiFillim, nderviti.NdermarrjeVitiFund, nderviti.IdPerdoruesi);
                if (!mesazh.Status)
                {
                    dbAdmin.rollbackTransaksion();
                    return mesazh;
                }
                nderviti.IdNderViti = idNV;
                if (konfigurimiDefault != "")
                {
                    konfigurimiDefault += ";T_SKEMAKONTABILITETIARTIKULLI" + ":" + true + "";
                    konfigurimiDefault += ";T_QENDRAKOSTO" + ":" + true + "";
                }
                else
                {
                    konfigurimiDefault += "T_SKEMAKONTABILITETIARTIKULLI" + ":" + true + "";
                    konfigurimiDefault += ";T_QENDRAKOSTO" + ":" + true + "";
                }
                string[] rreshtat = konfigurimiDefault.Split(';');
                for (int i = 0; i < rreshtat.Length; i++)
                {
                    string[] vlerat = rreshtat[i].Split(':');
                    string emriTabeles = vlerat[0].ToString();
                    bool selektuar = bool.Parse(vlerat[1].ToString());
                    if (selektuar == true)
                    {
                        mesazh = dbAdmin.shtoVleratDefaultNdermarrjes(emriTabeles, ndermarrje.IdNdermarrje, nderviti.IdNderViti, idndermnga);
                        if (!mesazh.Status)
                        {
                            dbAdmin.rollbackTransaksion();
                            return mesazh;
                        }
                    }
                }
                if (idndermnga < 1)
                {
                    if (lloji == 2)// komponente jane te ndryshme  per buxhetoret dhe ndermarjet normale
                        mesazh = dbAdmin.ruajDefaultKomponentePage(false, vit.FillimiViti, ndermarrje.idPerdoruesi, ndermarrje.idNdermarrje, -2);//shtimi i komponenteve te pages
                    else mesazh = dbAdmin.ruajDefaultKomponentePage(false, vit.FillimiViti, ndermarrje.idPerdoruesi, ndermarrje.idNdermarrje, -1);//shtimi i komponenteve te pages
                }
                else mesazh = dbAdmin.ruajDefaultKomponentePageKlono(false, ndermarrje.IdPerdoruesi, ndermarrje.IdNdermarrje, idndermnga);

                if (!mesazh.Status)
                {
                    dbAdmin.rollbackTransaksion();
                    return new clsMesazh(false, mesazh.PershkrimMesazhi);
                }
                if (idndermnga < 1)
                {
                    if (lloji == 2)
                        mesazh = dbAdmin.ruajDefaultKomponentePage(true, vit.FillimiViti, ndermarrje.idPerdoruesi, ndermarrje.idNdermarrje, -2);
                    else mesazh = dbAdmin.ruajDefaultKomponentePage(true, vit.FillimiViti, ndermarrje.idPerdoruesi, ndermarrje.idNdermarrje, -1);
                }
                else mesazh = dbAdmin.ruajDefaultKomponentePageKlono(true, ndermarrje.IdPerdoruesi, ndermarrje.IdNdermarrje, idndermnga);
                //shtimi i listpagesave 
                if (!mesazh.Status)
                {
                    dbAdmin.rollbackTransaksion();
                    return new clsMesazh(false, mesazh.PershkrimMesazhi);
                }

                mesazh = dbAdmin.ruajDefaultKlonoFushaShtese(ndermarrje.idPerdoruesi, ndermarrje.idNdermarrje, idndermnga);
                if (!mesazh.Status)
                {
                    dbAdmin.rollbackTransaksion();
                    return new clsMesazh(false, mesazh.PershkrimMesazhi);
                }

                mesazh = dbAdmin.ruajDefaultKonfigurimeKasaKlono(ndermarrje.IdPerdoruesi, ndermarrje.IdNdermarrje, idndermnga);
                //shtimi i listpagesave 
                if (!mesazh.Status)
                {
                    dbAdmin.rollbackTransaksion();
                    return new clsMesazh(false, mesazh.PershkrimMesazhi);
                }
                mesazh = dbAdmin.ruajDefaultKlonoShperndarjeQk(ndermarrje.IdPerdoruesi, ndermarrje.IdNdermarrje, idndermnga);
                //shtimi i listpagesave 
                if (!mesazh.Status)
                {
                    dbAdmin.rollbackTransaksion();
                    return new clsMesazh(false, mesazh.PershkrimMesazhi);
                }
                mesazh = dbAdmin.ruajFormatImportiDefault(ndermarrje.idNdermarrje, idndermnga, ndermarrje.idPerdoruesi);
                if (!mesazh.Status)
                {
                    dbAdmin.rollbackTransaksion();
                    return new clsMesazh(false, mesazh.PershkrimMesazhi);
                }
                mesazh = dbAdmin.ruajDefaultSigurime(ndermarrje.idPerdoruesi, ndermarrje.idNdermarrje);
                //shtimi i sigurimeve 
                if (!mesazh.Status)
                {
                    dbAdmin.rollbackTransaksion();
                    return new clsMesazh(false, mesazh.PershkrimMesazhi);
                }
                mesazh = dbAdmin.ruajDefaultTatime(ndermarrje.idPerdoruesi, ndermarrje.idNdermarrje);
                //shtimi i tatimeve
                if (!mesazh.Status)
                {
                    dbAdmin.rollbackTransaksion();
                    return new clsMesazh(false, mesazh.PershkrimMesazhi);
                }
                DbInventari.clsDatabaseInventari dbinv = new DbInventari.clsDatabaseInventari(dbAdmin);
                mesazh = dbinv.ruajStatusRiparimiNeNdermarjeTeRe(ndermarrje.idNdermarrje, idndermnga);
                if (!mesazh.Status)
                {
                    dbAdmin.rollbackTransaksion();
                    return new clsMesazh(false, mesazh.PershkrimMesazhi);
                }
                mesazh = dbinv.ruajLlojDifektiNeNdermarjeTeRe(ndermarrje.idNdermarrje, idndermnga);
                if (!mesazh.Status)
                {
                    dbAdmin.rollbackTransaksion();
                    return new clsMesazh(false, mesazh.PershkrimMesazhi);
                }
                DbRegjistrim.clsDatabaseRegjistrim dbregj = new DbRegjistrim.clsDatabaseRegjistrim(dbAdmin);
                mesazh = dbregj.shtoGrupimNeNdermarjeRe(ndermarrje.idNdermarrje, idndermnga);
                if (!mesazh.Status)
                {
                    dbAdmin.rollbackTransaksion();
                    return new clsMesazh(false, mesazh.PershkrimMesazhi);
                }
                DbShare.clsDatabaseShare dbshare = new DbShare.clsDatabaseShare(dbAdmin);

                mesazh = dbasete.ruajStatusMagazinaNderamrjeRe(idndermnga, ndermarrje.idNdermarrje);// shtimi i statuseve te magazines
                if (!mesazh.Status)
                {
                    dbAdmin.rollbackTransaksion();
                    return mesazh;
                }

                if (ndermarrje.idPrindi > 0)
                {
                    clsNdermarrje prindi = new clsNdermarrje(ndermarrje.idPrindi, dbAdmin);
                    if (prindi.kodFurnitori != "")
                    {
                        DbShare.clsKonfigurimAmbjenti konffur = new DbShare.clsKonfigurimAmbjenti();

                        konffur.mbushKonfigAmbjSipasKod("F", ndermarrje.idNdermarrje, dbshare);
                        DbKontabiliteti.clsKlientFurnitor furnitorimeme = new DbKontabiliteti.clsKlientFurnitor();

                        DbKontabiliteti.clsDatabaseKontabilitet dbkont = new DbKontabiliteti.clsDatabaseKontabilitet(dbAdmin);
                        int idkf = 0;
                        DbKontabiliteti.clsLlogari llog = new DbKontabiliteti.clsLlogari(prindi.nrLlogariFurnitori, ndermarrje.idNdermarrje, dbkont);
                        if (llog.IdLlogari > 0)
                        {
                            mesazh = furnitorimeme.ruajKlientFurnitor(out idkf, prindi.kodFurnitori, llog.IdLlogari, false, 1, "", prindi.emerFurnitori, "", "", 0, "", "", "", "", "", "", "", "", true, 0, 0, 0, 0, 0, 0, 0, 0, 0, "", "", false, 0, 0, 0, 0, 0, 0, 0, 0, 0, ndermarrje.idNdermarrje, DateTime.Today.Year, ndermarrje.idPerdoruesi, konffur.IdKonfigAmbjente, new DbKontabiliteti.colAdresatKlientFurnitor(), new DbKontabiliteti.colKontaktiKlientFurnitor(), new DbKontabiliteti.colBuxhetet(), new colVleraFushaShtese(), new colLidhjetAutorizim(), 1, "", "", 0, "", dbkont, 0, 0, 0, "", 0, ndermarrje.idPerdoruesi, 0, 0, false, null, "", 0, false, false, false, false, 0, 0, false, "", new DbKontabiliteti.colMarreveshjetPerKlient(), "", 0, new DateTime(), 0,0,0 ,"" , false ,"", "", false, "");
                            if (!mesazh.Status)
                            {
                                dbAdmin.rollbackTransaksion();
                                return new clsMesazh(false, mesazh.PershkrimMesazhi);
                            }

                            DbShare.clsKonfigurimAmbjenti konfKB = new DbShare.clsKonfigurimAmbjenti();
                            konfKB.mbushKonfigAmbjSipasKod("KB", ndermarrje.idNdermarrje, dbshare);
                            int idkontrolli = DbShare.clsKontroll.merrKontrollSipasKoditKomponentes("btnKlienti", 506).IdKontrolli;
                            DbShare.clsAtributeTrupi atr = new DbShare.clsAtributeTrupi();
                            mesazh = dbshare.modifikoAtributVlereDefault(idkontrolli, konfKB.IdKonfigAmbjente, idkf.ToString());
                            if (!mesazh.Status)
                            {
                                dbAdmin.rollbackTransaksion();
                                return new clsMesazh(false, mesazh.PershkrimMesazhi);
                            }
                        }
                        else
                        {
                            dbAdmin.rollbackTransaksion();
                            return new clsMesazh(false, "Llogaria e furnitorit nuk ekziston tek ndermarrja bije!");
                        }
                    }

                    int idnjes = 0;
                    DbShare.clsKonfigurimAmbjenti konfMag = new DbShare.clsKonfigurimAmbjenti();
                    konfMag.mbushKonfigAmbjSipasKod("MAG", ndermarrje.idNdermarrje, dbshare);
                    mesazh = dbregj.ruajNjesiAdministrative(out idnjes, "MQ", "Magazina Qendrore", "", 1, true, true, ndermarrje.idNdermarrje, ndermarrje.idPerdoruesi, DateTime.Today, konfMag.IdKonfigAmbjente, 0, 1, 1, 0, DateTime.Now, 0, 0, "", false, "", "", 1, 0, "", 0, 0, 0, 1, false, false, false,"",0,false);
                    if (!mesazh.Status)
                    {
                        dbAdmin.rollbackTransaksion();
                        return new clsMesazh(false, mesazh.PershkrimMesazhi);
                    }
                }
                dbAdmin.commitTransaksion();
                mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
                return mesazh;
            }
            catch (Exception ce)
            {
                dbAdmin.rollbackTransaksion();
                return new clsMesazh(false, ce.Message);
            }
        }

        public clsMesazh modifikoNderm(clsDatabaseAdmin data)
        {
            return data.modifikoNder(this.idNdermarrje, this.ndermarrjeKodi, this.ndermarrjePershkrimi, this.ndermarrjeVendi, this.ndermarrjeNipt, this.ndermarrjeMonedha
                   , this.ndermarrjeQyteti, this.ndermarrjeTel, this.ndermarrjeFax, this.ndermarrjeEMail, this.ndermarrjeLicenca, this.ndermarrjeKodiFiskal
                   , this.idPerdoruesi, this.idViti, this.llojNdermarje, this.idLicenca, this.idStatusDok, this.idTakse, this.NdermarrjeLogo, this.nrTvsh, this.prind, this.idPrindi, this.idGrupi, this.limitiShitjes, this.ownShop, this.Logu, this.raportuesi, this.nivelstrukture,this.Fiskalizim,this.Kodbiznesi,this.pathname, meTvsh, clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizim());

        }
        public clsMesazh RuajNdermPath(int IdNdermarrje,string pathname)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            clsMesazh mesazh;
            mesazh= data.RuajPath(IdNdermarrje, pathname);
            return mesazh;
        }

        public clsMesazh modifikoNdermarrje(clsNdermarrje ndermarrje)
        {
            colMonedhat monedhat = new colMonedhat();
            monedhat.mbushGjitheMonedhatPozitive(ndermarrje.IdNdermarrje, ndermarrje.IdPerdoruesi);
            colVitet vitet = new colVitet();
            vitet.merrGjitheVitetENdermarjes(ndermarrje.idNdermarrje);
            clsMonedha mon = new clsMonedha(ndermarrje.NdermarrjeMonedha);
            clsViti vit = new clsViti();
            vit.mbushVitetMet(ndermarrje.IdViti);
            clsNdermarrje ndermarjapara = new clsNdermarrje(ndermarrje.idNdermarrje);
            clsViti vitipara = new clsViti();
            vitipara.mbushVitetMet(ndermarjapara.idViti);
            clsMesazh mesazh;
            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();
            dbAdmin.beginTransaksion();
            try
            {
                mesazh = dbAdmin.modifikoNder(ndermarrje.IdNdermarrje, ndermarrje.NdermarrjeKodi, ndermarrje.NdermarrjePershkrimi, ndermarrje.NdermarrjeVendi, ndermarrje.NdermarrjeNipt,
                    ndermarrje.NdermarrjeMonedha, ndermarrje.NdermarrjeQyteti, ndermarrje.NdermarrjeTel, ndermarrje.NdermarrjeFax, ndermarrje.NdermarrjeEMail, ndermarrje.NdermarrjeLicenca,
                    ndermarrje.NdermarrjeKodiFiskal, ndermarrje.IdPerdoruesi, ndermarrje.IdViti, ndermarrje.LlojNdermarje, ndermarrje.IdLicenca, ndermarrje.idStatusDok, ndermarrje.IdTakse, ndermarrje.NdermarrjeLogo, ndermarrje.nrTvsh, ndermarrje.prind, ndermarrje.idPrindi, ndermarrje.idGrupi, ndermarrje.limitiShitjes, ndermarrje.ownShop, ndermarrje.Logu, ndermarrje.raportuesi, ndermarrje.nivelstrukture, ndermarrje.Fiskalizimi,ndermarrje.Kodbiznesi,ndermarrje.pathname,ndermarrje.meTvsh,clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizim());
                if (!mesazh.Status)
                {
                    dbAdmin.rollbackTransaksion();
                    return mesazh;
                }
                bool ekziston = false;
                foreach (clsMonedha m in monedhat)
                    if (m.KodiMonedha == mon.KodiMonedha)
                        ekziston = true;
                if (!ekziston)
                {
                    mon.IdNdermarje = ndermarrje.IdNdermarrje; //TOCHECK NESTILA - sduhet te kete nevoj se eshte monedha e ndermarrjes
                    int idM;
                    mesazh = dbAdmin.ruajMonedhe(out idM, mon.KodiMonedha, mon.PershkrimiMonedha, mon.AktivMonedha, mon.IdPerdoruesi, mon.IdLlogFitimi, mon.IdLlogHumbje, mon.IdNdermarje, 1, mon.IdFormatNrKursi);
                    if (!mesazh.Status)
                    {
                        dbAdmin.rollbackTransaksion();
                        return mesazh;
                    }
                }
                ekziston = false;
                foreach (clsViti v in vitet)
                    if (v.KodiViti == vit.KodiViti)
                    {
                        ekziston = true;
                        vit = v;
                        break;
                    }
                if (!ekziston)
                {
                    int idV;
                    vit.IdNdermarje = ndermarrje.IdNdermarrje;
                    mesazh = dbAdmin.ruajVit(out idV, vit.KodiViti, vit.FillimiViti, vit.MbarimiViti, vit.PeriudhaLloji, vit.PeriudhaHapjes, vit.PeriudhaMbylljes, vit.IdPerdoruesi, vit.IdKonfig, vit.IdNdermarje, vit.IdStatusDok, vit.MbyllurMe, vit.IdLlogMbylljeViti);
                    if (!mesazh.Status)
                    {
                        dbAdmin.rollbackTransaksion();
                        return mesazh;
                    }
                }
                clsNdermarrjeViti nderviti = new clsNdermarrjeViti();
                //nderviti.IdNderViti = dbAdmin.merrIdNdermarrjeVit(ndermarrje.idNdermarrje, vit.IdViti);
                nderviti.IdNderViti = dbAdmin.merrIdNdermarrjeVitSipasNdermarjesDheVitit(ndermarrje.idNdermarrje, vit.IdViti);
                nderviti.IdViti = vit.IdViti;
                nderviti.NdermarrjeViti = int.Parse(vit.FillimiViti.Year.ToString());
                nderviti.IdNdermarrje = ndermarrje.IdNdermarrje;
                nderviti.NdermarrjeVitiMbyllur = true;
                nderviti.NdermarrjeVitiFillim = DateTime.Parse(vit.FillimiViti.ToString());
                nderviti.NdermarrjeVitiFund = DateTime.Parse(vit.MbarimiViti.ToString());
                nderviti.IdPerdoruesi = ndermarrje.IdPerdoruesi;
                dbAdmin.modifikoNdermarrjeVit(nderviti.IdNderViti, nderviti.IdViti, nderviti.Viti, nderviti.IdNdermarrje, nderviti.NdermarrjeVitiMbyllur, nderviti.NdermarrjeVitiFillim, nderviti.NdermarrjeVitiFund, nderviti.IdPerdoruesi);// nderviti.modifiko();
                //  mesazh = dbAdmin.ruajNdermarrjeReVit(out idNV, nderviti.IdViti, nderviti.Viti, nderviti.IdNdermarrje, nderviti.NdermarrjeVitiMbyllur, nderviti.NdermarrjeVitiFillim, nderviti.NdermarrjeVitiFund, nderviti.IdPerdoruesi);                
                dbAdmin.commitTransaksion();
                return mesazh;
            }
            catch (Exception ce)
            {
                dbAdmin.rollbackTransaksion();
                return new clsMesazh(false, ce.Message);
            }
        }
        
        /// <summary>
        /// Kthen ndermarrjen sipas ID-se
        /// </summary>
        public clsNdermarrje merrSipasID()
        {
            clsNdermarrje cls = new clsNdermarrje(this.IdNdermarrje);
            if (cls != null)
            {
                return cls;
            }
            else
            {
                return new clsNdermarrje();
            }
        }

        /// <summary>
        ///  Sherben per marrjen e te gjitha ndermarrjeve nga databaza. Thirret funksioni
        ///  <see cref="DbCore.DbAdmin.clsDatabaseAdmin.ktheGjitheNdermarrjet"/> 
        /// </summary>
        public colNdermarrjet merriTeGjithe(int idperdoruesi, int idlicenca)
        {
            colNdermarrjet data = new colNdermarrjet();
            data.mbushGjitheNdermarrjet(idperdoruesi, idlicenca);
            return data;
        }

        public static clsMonedha ktheMonedheNdermSipasID(int idNdermarrje, clsDatabaseAdmin dbadmin)
        {
            return new clsMonedha(dbadmin.ktheMonedheNdermSipasID(idNdermarrje));
        }

        public static double KtheLimitshitjeNdermarrjes(int idNdermarrje)
        {
            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();
            return dbAdmin.KtheLimitshitjeNdermarrjes(idNdermarrje);
        }
        #endregion

        #region Metoda Internal

        internal bool mbushNdermarrja(DataRow dbDataRowNdermarrja)
        {
            if (dbDataRowNdermarrja != null)
            {
                try
                {
                    idNdermarrje = int.Parse(dbDataRowNdermarrja["IDNDERMARJE"].ToString());
                    ndermarrjeKodi = dbDataRowNdermarrja["NDERMARJEKODI"].ToString();
                    ndermarrjePershkrimi = dbDataRowNdermarrja["NDERMARJEPERSHK"].ToString();
                    ndermarrjeVendi = dbDataRowNdermarrja["NDERMARJEVEND"].ToString();
                    ndermarrjeNipt = dbDataRowNdermarrja["NDERMARJENIPT"].ToString();
                    ndermarrjeMonedha = int.Parse(dbDataRowNdermarrja["NDERMARJEMON"].ToString());
                    int.TryParse(dbDataRowNdermarrja["NDERMARJAQYTETI"].ToString(), out ndermarrjeQyteti);
                    ndermarrjeTel = dbDataRowNdermarrja["NDERMARJETEL"].ToString();
                    ndermarrjeFax = dbDataRowNdermarrja["NDERMARJEFAX"].ToString();
                    ndermarrjeEMail = dbDataRowNdermarrja["NDERMARJEMAIL"].ToString();
                    ndermarrjeLicenca = dbDataRowNdermarrja["NDERMARJELICEN"].ToString();
                    ndermarrjeKodiFiskal = dbDataRowNdermarrja["NDERMARJEKODIFISK"].ToString();
                    idPerdoruesi = int.Parse(dbDataRowNdermarrja["IDPERDORUESI"].ToString());
                    idViti = int.Parse(dbDataRowNdermarrja["IDVITI"].ToString());
                    llojNdermarje = int.Parse(dbDataRowNdermarrja["LLOJNDERMARJE"].ToString());
                    idLicenca = int.Parse(dbDataRowNdermarrja["IDLICENCA"].ToString());
                    int.TryParse(dbDataRowNdermarrja["IDSTATUSDOK"].ToString(), out idStatusDok);
                    int.TryParse(dbDataRowNdermarrja["LLOJI"].ToString(), out lloji);
                    DateTime.TryParse(dbDataRowNdermarrja["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRowNdermarrja["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    int.TryParse(dbDataRowNdermarrja["NIVELTVSH"].ToString(), out idTakse);
                    nrTvsh = dbDataRowNdermarrja["NRTVSH"].ToString();
                    bool.TryParse(dbDataRowNdermarrja["PRIND"].ToString(), out prind);
                    bool.TryParse(dbDataRowNdermarrja["OWNSHOP"].ToString(), out ownShop);
                    int.TryParse(dbDataRowNdermarrja["IDPRINDI"].ToString(), out idPrindi);
                    int.TryParse(dbDataRowNdermarrja["IDGRUPI"].ToString(), out idGrupi);
                    double.TryParse(dbDataRowNdermarrja["LIMITISHITJES"].ToString(), out limitiShitjes);
                    kodFurnitori = dbDataRowNdermarrja["KODFURNITORI"].ToString();
                    emerFurnitori = dbDataRowNdermarrja["EMERFURNITORI"].ToString();
                    nrLlogariFurnitori = dbDataRowNdermarrja["NRLLOGARIFURNITORI"].ToString();
                    bool.TryParse(dbDataRowNdermarrja["ARKIVA"].ToString(), out arkiva);
                    ndermarrjeQytetiPershkrimi = dbDataRowNdermarrja["QYTETIEMRI"].ToString();
                    bool.TryParse(dbDataRowNdermarrja["LOGU"].ToString(), out Logu);
                    bool.TryParse(dbDataRowNdermarrja["FISKALIZIM"].ToString(), out Fiskalizim);
                    int.TryParse(dbDataRowNdermarrja["RAPORTUESI"].ToString(), out raportuesi);
                    int.TryParse(dbDataRowNdermarrja["NIVELSTRUKTURE"].ToString(), out nivelstrukture);
                    kodbiznesi= dbDataRowNdermarrja["KODBIZNESI"].ToString();
                    pathname = dbDataRowNdermarrja["CERTIFIKATA"].ToString();
                    if(clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizim())
                        bool.TryParse(dbDataRowNdermarrja["METVSH"].ToString(), out meTvsh);
                    //  kodilicenca= dbDataRowNdermarrja["NDERMARJEKODIFISK"].ToString();
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se ndermarrjes nga db-ja");
                }
            }
            else
                return false;
        }
        internal clsMesazh mbushNdermarrja(clsNdermarrje ndermarrja)
        {
            idNdermarrje = ndermarrja.idNdermarrje;
            ndermarrjeKodi = ndermarrja.ndermarrjeKodi;
            ndermarrjePershkrimi = ndermarrja.ndermarrjePershkrimi;
            ndermarrjeVendi = ndermarrja.ndermarrjeVendi;
            ndermarrjeNipt = ndermarrja.ndermarrjeNipt;
            ndermarrjeMonedha = ndermarrja.ndermarrjeMonedha;
            ndermarrjeQyteti = ndermarrja.ndermarrjeQyteti;
            ndermarrjeTel = ndermarrja.ndermarrjeTel;
            ndermarrjeFax = ndermarrja.ndermarrjeFax;
            ndermarrjeEMail = ndermarrja.ndermarrjeEMail;
            ndermarrjeLicenca = ndermarrja.ndermarrjeLicenca;
            ndermarrjeKodiFiskal = ndermarrja.ndermarrjeKodiFiskal;
            idPerdoruesi = ndermarrja.idPerdoruesi;
            idViti = ndermarrja.idViti;
            llojNdermarje = ndermarrja.llojNdermarje;
            idLicenca = ndermarrja.idLicenca;
            idStatusDok = ndermarrja.idStatusDok;
            lloji = ndermarrja.lloji;
            dtKrijimi = ndermarrja.dtKrijimi;
            dtModifikimi = ndermarrja.dtModifikimi;
            idTakse = ndermarrja.idTakse;
            nrTvsh = ndermarrja.nrTvsh;
            prind = ndermarrja.prind;
            ownShop = ndermarrja.ownShop;
            idPrindi = ndermarrja.idPrindi;
            idGrupi = ndermarrja.idGrupi;
            limitiShitjes = ndermarrja.limitiShitjes;
            kodFurnitori = ndermarrja.kodFurnitori;
            emerFurnitori = ndermarrja.emerFurnitori;
            nrLlogariFurnitori = ndermarrja.nrLlogariFurnitori;
            arkiva = ndermarrja.arkiva;
            ndermarrjeQytetiPershkrimi = ndermarrja.ndermarrjeQytetiPershkrimi;
            Logu = ndermarrja.Logu;
            raportuesi = ndermarrja.raportuesi;
            nivelstrukture = ndermarrja.nivelstrukture;
            Fiskalizim = ndermarrja.Fiskalizim;
            kodbiznesi = ndermarrja.kodbiznesi;
            pathname = ndermarrja.pathname;

            return new clsMesazh(true, $"Mbushja e ndermarrjes me kod {ndermarrja.NdermarrjeKodi} u krye me sukses!");
        }

        internal bool mbushNdermarrjaList(DataRow dbDataRowNdermarrja)
        {
            if (dbDataRowNdermarrja != null)
            {
                try
                {
                    idNdermarrje = int.Parse(dbDataRowNdermarrja["IDNDERMARJE"].ToString());
                    ndermarrjeKodi = dbDataRowNdermarrja["NDERMARJEKODI"].ToString();
                    ndermarrjePershkrimi = dbDataRowNdermarrja["NDERMARJEPERSHK"].ToString();
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se ndermarrjes nga db-ja");
                }
            }
            else
                return false;
        }

        internal static int ktheIdNdermPareNeListeSipasIdPerdoruesit(int idPerdoruesi, bool eshtePunonjes)
        {
            using (clsDatabaseAdmin dbadmin = new clsDatabaseAdmin())
            {
                return dbadmin.ktheIdNdermPareNeListe(idPerdoruesi, eshtePunonjes);
            }
        }

        internal static int ktheIdNdermPareNeListeSipasIdPerdoruesit(int idPerdoruesi, bool eshtePunonjes, DbCore.DbAdmin.clsDatabaseAdmin dbAdmin)
        {
            return dbAdmin.ktheIdNdermPareNeListe(idPerdoruesi, eshtePunonjes);
        }

        internal static int ktheIdNdermPareNeListeSipasLicences()
        {
            using (clsDatabaseAdmin dbadmin = new clsDatabaseAdmin())
            {
                return dbadmin.ktheIdNdermPareNeListeSipasLicences();
            }
        }
        
        #endregion

        #region Metoda Private

        private byte[] merrLogo()
        {
            clsDatabaseAdmin db = new clsDatabaseAdmin();
            byte[] logo = db.merrLogoNderm(idNdermarrje);
            logoLoaded = true;
            db.Dispose();
            return logo;
        }

        private void zip(string filename, string zipname)
        {
            try
            {
                ZipOutputStream strmZipOutputStream = new ZipOutputStream(File.Create(zipname));
                strmZipOutputStream.SetLevel(6);
                Crc32 objCrc32 = new Crc32();


                FileStream sourceFile = File.OpenRead(filename);
                ZipEntry objZipEntry = new ZipEntry(filename);
                objZipEntry.DateTime = DateTime.Now;
                objZipEntry.Size = sourceFile.Length;
                strmZipOutputStream.PutNextEntry(objZipEntry);
                int size = 100000000;
                objCrc32.Reset();
                byte[] abyBuffer = new byte[size];
                bool vazhdo = true;
                while (vazhdo)
                {
                    size = sourceFile.Read(abyBuffer, 0, abyBuffer.Length);
                    if (size > 0)
                    {
                        objCrc32.Update(abyBuffer, 0, size);
                        strmZipOutputStream.Write(abyBuffer, 0, size);
                    }
                    else vazhdo = false;

                }
                objZipEntry.Crc = objCrc32.Value;
                sourceFile.Close();
                File.Delete(filename);
                strmZipOutputStream.Finish();
                strmZipOutputStream.Close();
            }
            catch (Exception)
            {
            }
        }
                
        private static bool krijodt(string tablename, string query, DataSet ds, bool ukrijua)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            DataTable dtt = new DataTable();
            dtt = data.ktheDataTable(tablename, query);
            if (dtt == null || ukrijua == false)
            {
                data.Dispose();
                return false;
            }
            ds.Tables.Add(dtt);
            data.Dispose();
            return true;
        }
        
        private string unzip(Stream filecontent, int idlicenca)
        {
            string finame = "";
            clsLicenca licenca = new clsLicenca(idlicenca);
            string pathDir = System.Web.HttpContext.Current.Server.MapPath(null) + @"\Licencat\";
            DirectoryExtension.CreateDirIfNotExists(pathDir);
            pathDir += licenca.KodLicenca + "\\";
            DirectoryExtension.CreateDirIfNotExists(pathDir);
            try
            {                                    // File.OpenRead(zipname)
                ZipInputStream s = new ZipInputStream(filecontent);
                ZipEntry theEntry = s.GetNextEntry();
                string dirname = Path.GetDirectoryName(theEntry.Name);
                finame = Path.GetFileName(theEntry.Name);
                FileStream sWriter = File.Create(pathDir + finame);
                int size = 2048;
                byte[] data = new byte[2048];
                bool vazhdo = true;
                while (vazhdo)
                {
                    size = s.Read(data, 0, 2048);
                    if (size > 0)
                        sWriter.Write(data, 0, size);
                    else vazhdo = false;
                }
                sWriter.Close();
                s.Close();
            }
            catch (Exception)
            {

            }
            return pathDir + finame;

        }

        private DataSet krijods(string inidnderm)
        {
            DataSet ds = new DataSet("WebInf");
            bool ukrijua = true;
            #region A
            //dtt = data.ktheDataTable("T_ACRNUMRAAUTOMATIKE","SELECT  * from T_ACRNUMRAAUTOMATIKE where IDNDERMARJE in ("+inidnderm+")");//nuk perdoret me
            //if (dtt == null)
            //    return null;
            //ds.Tables.Add(dtt);
            ukrijua = krijodt("T_ADRESAKLIENTFURNITOR", "SELECT T_ADRESAKLIENTFURNITOR. * FROM T_ADRESAKLIENTFURNITOR INNER JOIN dbo.T_KLIENTFURNITOR ON  dbo.T_ADRESAKLIENTFURNITOR.IDKLIENTFURNITOR = dbo.T_KLIENTFURNITOR.IDKLIENTFURNITOR WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_AGJENTSHITJE", "SELECT * FROM  T_AGJENTSHITJE WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_AKTIVITETEKOKA", "SELECT * FROM  T_AKTIVITETEKOKA WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_AKTIVITETETRUPI", "SELECT T_AKTIVITETETRUPI.* FROM T_AKTIVITETETRUPI INNER JOIN T_AKTIVITETEKOKA ON dbo.T_AKTIVITETETRUPI.IDKOKA = dbo.T_AKTIVITETEKOKA.IDKOKA WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_ARTIKULLI", "SELECT * FROM  T_ARTIKULLI WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_ARTIKULLIPERBERES", "SELECT T_ARTIKULLIPERBERES.* FROM T_ARTIKULLIPERBERES INNER JOIN dbo.T_ARTIKULLI ON dbo.T_ARTIKULLIPERBERES.IDARTIKULLKYESOR = dbo.T_ARTIKULLI.IDARTIKULLI WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_ARTIKULLIPERBERESTEMPLATEKOKA", "SELECT * FROM T_ARTIKULLIPERBERESTEMPLATEKOKA WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_ARTIKULLIPERBERESTEMPLATETRUPI", "SELECT T_ARTIKULLIPERBERESTEMPLATETRUPI.* FROM T_ARTIKULLIPERBERESTEMPLATETRUPI  INNER JOIN  T_ARTIKULLIPERBERESTEMPLATEKOKA  ON  dbo.T_ARTIKULLIPERBERESTEMPLATETRUPI.IDKOKA = dbo.T_ARTIKULLIPERBERESTEMPLATEKOKA.IDKOKA WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_ARTIKULLIZEVENDESUES", "SELECT T_ARTIKULLIZEVENDESUES.* FROM T_ARTIKULLIZEVENDESUES INNER JOIN t_artikulli ON dbo.T_ARTIKULLIZEVENDESUES.IDARTIKULLIKRYESOR = dbo.T_ARTIKULLI.IDARTIKULLI WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_ATRIBUTETRUPI", "SELECT  T_ATRIBUTETRUPI.* FROM T_ATRIBUTETRUPI INNER JOIN dbo.T_KONFIGAMBJENTE ON dbo.T_ATRIBUTETRUPI.IDKONFIGAMBJENTE = dbo.T_KONFIGAMBJENTE.IDKONFIGAMBJENTE WHERE IDKATDOK=5 AND  IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_ATRIBUTETRUPI2", "SELECT  T_ATRIBUTETRUPI. IDATRIBUTETRUPI, T_ATRIBUTETRUPI.IDKONTROLL, T_ATRIBUTETRUPI.IDKONFIGAMBJENTE, Convert (numeric(18,0),T_ATRIBUTETRUPI.VLEREDEFAULT)VLEREDEFAULT, T_ATRIBUTETRUPI.VISIBLE, T_ATRIBUTETRUPI.ENABLED1,  T_ATRIBUTETRUPI.IDKONFIGLUPA, T_ATRIBUTETRUPI. IDENTIFIKUES, T_ATRIBUTETRUPI.RRESHTI, T_ATRIBUTETRUPI.KOLONA, T_ATRIBUTETRUPI.DETYRUESHME, T_ATRIBUTETRUPI.IDNRAUTOMATIK FROM T_ATRIBUTETRUPI INNER JOIN dbo.T_KONFIGAMBJENTE ON dbo.T_ATRIBUTETRUPI.IDKONFIGAMBJENTE = dbo.T_KONFIGAMBJENTE.IDKONFIGAMBJENTE WHERE IDKATDOK<>5 and idkontroll in (select idkontroll from t_kontrolle where idkomponente=402 and kodkontroll in ('btneLlogInv','btneLlogBle','btneLlogShit','btneLlogTretet','btnLlogShpe','cmbLlogAmortizimi')) and vleredefault!='' and vleredefault!='0' and IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);

            ukrijua = krijodt("T_ATRIBUTETRUPI3", "SELECT  T_ATRIBUTETRUPI. IDATRIBUTETRUPI, T_ATRIBUTETRUPI.IDKONTROLL, T_ATRIBUTETRUPI.IDKONFIGAMBJENTE, Convert (numeric(18,0),T_ATRIBUTETRUPI.VLEREDEFAULT)VLEREDEFAULT, T_ATRIBUTETRUPI.VISIBLE,  T_ATRIBUTETRUPI.ENABLED1,  T_ATRIBUTETRUPI.IDKONFIGLUPA, T_ATRIBUTETRUPI. IDENTIFIKUES, T_ATRIBUTETRUPI.RRESHTI, T_ATRIBUTETRUPI.KOLONA, T_ATRIBUTETRUPI.DETYRUESHME, T_ATRIBUTETRUPI.IDNRAUTOMATIK FROM T_ATRIBUTETRUPI INNER JOIN dbo.T_KONFIGAMBJENTE ON dbo.T_ATRIBUTETRUPI.IDKONFIGAMBJENTE = dbo.T_KONFIGAMBJENTE.IDKONFIGAMBJENTE WHERE IDKATDOK<>5 and idkontroll in (select idkontroll from t_kontrolle where idkomponente=402 and kodkontroll in ('btneSkema'))and vleredefault!='' and vleredefault!='0' and IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);

            ukrijua = krijodt("T_ATRIBUTETRUPI4", "SELECT T_ATRIBUTETRUPI. IDATRIBUTETRUPI, T_ATRIBUTETRUPI.IDKONTROLL, T_ATRIBUTETRUPI.IDKONFIGAMBJENTE, Convert (numeric(18,0),T_ATRIBUTETRUPI.VLEREDEFAULT)VLEREDEFAULT, T_ATRIBUTETRUPI.VISIBLE, T_ATRIBUTETRUPI.ENABLED1,  T_ATRIBUTETRUPI.IDKONFIGLUPA, T_ATRIBUTETRUPI. IDENTIFIKUES, T_ATRIBUTETRUPI.RRESHTI, T_ATRIBUTETRUPI.KOLONA, T_ATRIBUTETRUPI.DETYRUESHME, T_ATRIBUTETRUPI.IDNRAUTOMATIK FROM T_ATRIBUTETRUPI INNER JOIN dbo.T_KONFIGAMBJENTE ON dbo.T_ATRIBUTETRUPI.IDKONFIGAMBJENTE = dbo.T_KONFIGAMBJENTE.IDKONFIGAMBJENTE WHERE IDKATDOK<>5 and idkontroll in (select idkontroll from t_kontrolle where (idkomponente=510 and kodkontroll in ('btneMagazina','btneMagazina2')) or(idkomponente=506 and kodkontroll in ('btnMagazina')))and vleredefault!='' and vleredefault!='0' and IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);

            ukrijua = krijodt("T_ATRIBUTETRUPI5", "SELECT  T_ATRIBUTETRUPI. IDATRIBUTETRUPI, T_ATRIBUTETRUPI.IDKONTROLL, T_ATRIBUTETRUPI.IDKONFIGAMBJENTE, Convert (numeric(18,0),T_ATRIBUTETRUPI.VLEREDEFAULT)VLEREDEFAULT, T_ATRIBUTETRUPI.VISIBLE, T_ATRIBUTETRUPI.ENABLED1,  T_ATRIBUTETRUPI.IDKONFIGLUPA, T_ATRIBUTETRUPI. IDENTIFIKUES, T_ATRIBUTETRUPI.RRESHTI, T_ATRIBUTETRUPI.KOLONA, T_ATRIBUTETRUPI.DETYRUESHME, T_ATRIBUTETRUPI.IDNRAUTOMATIK FROM T_ATRIBUTETRUPI INNER JOIN dbo.T_KONFIGAMBJENTE ON dbo.T_ATRIBUTETRUPI.IDKONFIGAMBJENTE = dbo.T_KONFIGAMBJENTE.IDKONFIGAMBJENTE WHERE IDKATDOK<>5 and idkontroll in (select idkontroll from t_kontrolle where (idkomponente=510 and kodkontroll in ('btneKlientFurnitori')) or(idkomponente=506 and kodkontroll in ('btnKlienti'))) and vleredefault!='' and vleredefault!='0' and IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);

            ukrijua = krijodt("T_ATRIBUTETRUPI6", "SELECT  T_ATRIBUTETRUPI. IDATRIBUTETRUPI, T_ATRIBUTETRUPI.IDKONTROLL, T_ATRIBUTETRUPI.IDKONFIGAMBJENTE, Convert (numeric(18,0),T_ATRIBUTETRUPI.VLEREDEFAULT)VLEREDEFAULT, T_ATRIBUTETRUPI.VISIBLE, T_ATRIBUTETRUPI.ENABLED1, T_ATRIBUTETRUPI.IDKONFIGLUPA, T_ATRIBUTETRUPI. IDENTIFIKUES, T_ATRIBUTETRUPI.RRESHTI, T_ATRIBUTETRUPI.KOLONA, T_ATRIBUTETRUPI.DETYRUESHME, T_ATRIBUTETRUPI.IDNRAUTOMATIK FROM T_ATRIBUTETRUPI INNER JOIN dbo.T_KONFIGAMBJENTE ON dbo.T_ATRIBUTETRUPI.IDKONFIGAMBJENTE = dbo.T_KONFIGAMBJENTE.IDKONFIGAMBJENTE WHERE IDKATDOK<>5 and idkontroll in (select idkontroll from t_kontrolle where (idkomponente=301 and kodkontroll in ('banka_ComboBox')) )and vleredefault!='' and vleredefault!='0' and IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);

            ukrijua = krijodt("T_ATRIBUTETRUPI7", "SELECT  T_ATRIBUTETRUPI. IDATRIBUTETRUPI, T_ATRIBUTETRUPI.IDKONTROLL, T_ATRIBUTETRUPI.IDKONFIGAMBJENTE, Convert (numeric(18,0),T_ATRIBUTETRUPI.VLEREDEFAULT)VLEREDEFAULT, T_ATRIBUTETRUPI.VISIBLE, T_ATRIBUTETRUPI.ENABLED1,  T_ATRIBUTETRUPI.IDKONFIGLUPA, T_ATRIBUTETRUPI. IDENTIFIKUES, T_ATRIBUTETRUPI.RRESHTI, T_ATRIBUTETRUPI.KOLONA, T_ATRIBUTETRUPI.DETYRUESHME, T_ATRIBUTETRUPI.IDNRAUTOMATIK FROM T_ATRIBUTETRUPI INNER JOIN dbo.T_KONFIGAMBJENTE ON dbo.T_ATRIBUTETRUPI.IDKONFIGAMBJENTE = dbo.T_KONFIGAMBJENTE.IDKONFIGAMBJENTE WHERE IDKATDOK<>5 and idkontroll in (select idkontroll from t_kontrolle where (idkomponente=506 and kodkontroll in ('btnKushtDergimi')) ) and vleredefault!=''  and vleredefault!='0' and IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);

            ukrijua = krijodt("T_ATRIBUTETRUPI8", "SELECT T_ATRIBUTETRUPI. IDATRIBUTETRUPI, T_ATRIBUTETRUPI.IDKONTROLL, T_ATRIBUTETRUPI.IDKONFIGAMBJENTE, Convert (numeric(18,0),T_ATRIBUTETRUPI.VLEREDEFAULT)VLEREDEFAULT, T_ATRIBUTETRUPI.VISIBLE,  T_ATRIBUTETRUPI.ENABLED1,  T_ATRIBUTETRUPI.IDKONFIGLUPA, T_ATRIBUTETRUPI. IDENTIFIKUES, T_ATRIBUTETRUPI.RRESHTI, T_ATRIBUTETRUPI.KOLONA, T_ATRIBUTETRUPI.DETYRUESHME, T_ATRIBUTETRUPI.IDNRAUTOMATIK FROM T_ATRIBUTETRUPI INNER JOIN dbo.T_KONFIGAMBJENTE ON dbo.T_ATRIBUTETRUPI.IDKONFIGAMBJENTE = dbo.T_KONFIGAMBJENTE.IDKONFIGAMBJENTE WHERE IDKATDOK<>5 and idkontroll in (select idkontroll from t_kontrolle where (idkomponente=506 and kodkontroll in ('btnMenyreTransporti')) ) and vleredefault!='' and vleredefault!='0' and IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);

            ukrijua = krijodt("T_ATRIBUTETRUPI9", "SELECT  T_ATRIBUTETRUPI. IDATRIBUTETRUPI, T_ATRIBUTETRUPI.IDKONTROLL, T_ATRIBUTETRUPI.IDKONFIGAMBJENTE, Convert (numeric(18,0),T_ATRIBUTETRUPI.VLEREDEFAULT)VLEREDEFAULT, T_ATRIBUTETRUPI.VISIBLE, T_ATRIBUTETRUPI.ENABLED1,  T_ATRIBUTETRUPI.IDKONFIGLUPA, T_ATRIBUTETRUPI. IDENTIFIKUES, T_ATRIBUTETRUPI.RRESHTI, T_ATRIBUTETRUPI.KOLONA, T_ATRIBUTETRUPI.DETYRUESHME, T_ATRIBUTETRUPI.IDNRAUTOMATIK FROM T_ATRIBUTETRUPI INNER JOIN dbo.T_KONFIGAMBJENTE ON dbo.T_ATRIBUTETRUPI.IDKONFIGAMBJENTE = dbo.T_KONFIGAMBJENTE.IDKONFIGAMBJENTE WHERE IDKATDOK<>5 and idkontroll in (select idkontroll from t_kontrolle where (idkomponente=506 and kodkontroll in ('btnAgjenti')) ) and vleredefault!='' and vleredefault!='0' and IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);

            ukrijua = krijodt("T_ATRIBUTETRUPI10", "SELECT T_ATRIBUTETRUPI. IDATRIBUTETRUPI, T_ATRIBUTETRUPI.IDKONTROLL, T_ATRIBUTETRUPI.IDKONFIGAMBJENTE, Convert (numeric(18,0),T_ATRIBUTETRUPI.VLEREDEFAULT)VLEREDEFAULT, T_ATRIBUTETRUPI.VISIBLE,  T_ATRIBUTETRUPI.ENABLED1,  T_ATRIBUTETRUPI.IDKONFIGLUPA, T_ATRIBUTETRUPI. IDENTIFIKUES, T_ATRIBUTETRUPI.RRESHTI, T_ATRIBUTETRUPI.KOLONA, T_ATRIBUTETRUPI.DETYRUESHME, T_ATRIBUTETRUPI.IDNRAUTOMATIK FROM T_ATRIBUTETRUPI INNER JOIN dbo.T_KONFIGAMBJENTE ON dbo.T_ATRIBUTETRUPI.IDKONFIGAMBJENTE = dbo.T_KONFIGAMBJENTE.IDKONFIGAMBJENTE WHERE IDKATDOK<>5 and idkontroll in (select idkontroll from t_kontrolle where (idkomponente=506 and kodkontroll in ('cmbMonedha')) ) and vleredefault!='' and vleredefault!='0' and IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);

            ukrijua = krijodt("T_ATRIBUTETRUPI11", "SELECT T_ATRIBUTETRUPI. IDATRIBUTETRUPI, T_ATRIBUTETRUPI.IDKONTROLL, T_ATRIBUTETRUPI.IDKONFIGAMBJENTE, Convert (numeric(18,0),T_ATRIBUTETRUPI.VLEREDEFAULT)VLEREDEFAULT, T_ATRIBUTETRUPI.VISIBLE,  T_ATRIBUTETRUPI.ENABLED1,  T_ATRIBUTETRUPI.IDKONFIGLUPA, T_ATRIBUTETRUPI. IDENTIFIKUES, T_ATRIBUTETRUPI.RRESHTI, T_ATRIBUTETRUPI.KOLONA, T_ATRIBUTETRUPI.DETYRUESHME, T_ATRIBUTETRUPI.IDNRAUTOMATIK FROM T_ATRIBUTETRUPI INNER JOIN dbo.T_KONFIGAMBJENTE ON dbo.T_ATRIBUTETRUPI.IDKONFIGAMBJENTE = dbo.T_KONFIGAMBJENTE.IDKONFIGAMBJENTE WHERE IDKATDOK<>5 and idkontroll in (select idkontroll from t_kontrolle where (idkomponente=506 and kodkontroll in ('btnMaturimi')) ) and vleredefault!='' and vleredefault!='0' and IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);

            ukrijua = krijodt("T_ATRIBUTETRUPI12", "SELECT T_ATRIBUTETRUPI. IDATRIBUTETRUPI, T_ATRIBUTETRUPI.IDKONTROLL, T_ATRIBUTETRUPI.IDKONFIGAMBJENTE, Convert (numeric(18,0),T_ATRIBUTETRUPI.VLEREDEFAULT)VLEREDEFAULT, T_ATRIBUTETRUPI.VISIBLE,  T_ATRIBUTETRUPI.ENABLED1,  T_ATRIBUTETRUPI.IDKONFIGLUPA, T_ATRIBUTETRUPI. IDENTIFIKUES, T_ATRIBUTETRUPI.RRESHTI, T_ATRIBUTETRUPI.KOLONA, T_ATRIBUTETRUPI.DETYRUESHME, T_ATRIBUTETRUPI.IDNRAUTOMATIK FROM T_ATRIBUTETRUPI INNER JOIN dbo.T_KONFIGAMBJENTE ON dbo.T_ATRIBUTETRUPI.IDKONFIGAMBJENTE = dbo.T_KONFIGAMBJENTE.IDKONFIGAMBJENTE WHERE IDKATDOK<>5 and idkontroll in (select idkontroll from t_kontrolle where (idkomponente=506 and kodkontroll in ('btnKushtPagese')) ) and vleredefault!='' and vleredefault!='0' and IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);

            ukrijua = krijodt("T_ATRIBUTETRUPI13", "SELECT T_ATRIBUTETRUPI. IDATRIBUTETRUPI, T_ATRIBUTETRUPI.IDKONTROLL, T_ATRIBUTETRUPI.IDKONFIGAMBJENTE, Convert (numeric(18,0),T_ATRIBUTETRUPI.VLEREDEFAULT)VLEREDEFAULT, T_ATRIBUTETRUPI.VISIBLE, T_ATRIBUTETRUPI.ENABLED1, T_ATRIBUTETRUPI.IDKONFIGLUPA, T_ATRIBUTETRUPI. IDENTIFIKUES, T_ATRIBUTETRUPI.RRESHTI, T_ATRIBUTETRUPI.KOLONA, T_ATRIBUTETRUPI.DETYRUESHME, T_ATRIBUTETRUPI.IDNRAUTOMATIK FROM T_ATRIBUTETRUPI INNER JOIN dbo.T_KONFIGAMBJENTE ON dbo.T_ATRIBUTETRUPI.IDKONFIGAMBJENTE = dbo.T_KONFIGAMBJENTE.IDKONFIGAMBJENTE WHERE IDKATDOK<>5 and idkontroll in (select idkontroll from t_kontrolle where (idkomponente=506 and kodkontroll in ('cmbPikeShitjeFurnizimi')) ) and vleredefault!='' and vleredefault!='0' and IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);

            ukrijua = krijodt("T_ATRIBUTETRUPI14", "SELECT  T_ATRIBUTETRUPI.* FROM T_ATRIBUTETRUPI INNER JOIN dbo.T_KONFIGAMBJENTE ON dbo.T_ATRIBUTETRUPI.IDKONFIGAMBJENTE = dbo.T_KONFIGAMBJENTE.IDKONFIGAMBJENTE WHERE IDKATDOK<>5 and idkontroll not in (select idkontroll from t_kontrolle where (idkomponente=506 and kodkontroll in ('cmbPikeShitjeFurnizimi','btnKushtPagese','btnMaturimi','cmbMonedha','btnAgjenti','btnMenyreTransporti','btnKushtDergimi','btnKlienti','btnMagazina')) or (idkomponente=402 and kodkontroll in ('btneLlogInv','btneLlogBle','btneLlogShit','btneLlogTretet','btnLlogShpe','cmbLlogAmortizimi','btneSkema')) or (idkomponente=510 and kodkontroll in ('btneMagazina','btneMagazina2','btneKlientFurnitori')) or(idkomponente=301 and kodkontroll in ('banka_ComboBox')) ) and IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_ATRIBUTETRUPI15", "SELECT  T_ATRIBUTETRUPI.* FROM T_ATRIBUTETRUPI INNER JOIN dbo.T_KONFIGAMBJENTE ON dbo.T_ATRIBUTETRUPI.IDKONFIGAMBJENTE = dbo.T_KONFIGAMBJENTE.IDKONFIGAMBJENTE WHERE IDKATDOK<>5 and idkontroll  in (select idkontroll from t_kontrolle where (idkomponente=506 and kodkontroll in ('cmbPikeShitjeFurnizimi','btnKushtPagese','btnMaturimi','cmbMonedha','btnAgjenti','btnMenyreTransporti','btnKushtDergimi','btnKlienti','btnMagazina')) or (idkomponente=402 and kodkontroll in ('btneLlogInv','btneLlogBle','btneLlogShit','btneLlogTretet','btnLlogShpe','cmbLlogAmortizimi','btneSkema')) or (idkomponente=510 and kodkontroll in ('btneMagazina','btneMagazina2','btneKlientFurnitori')) or(idkomponente=301 and kodkontroll in ('banka_ComboBox')) ) and (vleredefault='' or  vleredefault='0') and IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);


            ukrijua = krijodt("T_AUTORIZIMKOKA", "SELECT * FROM  T_AUTORIZIMKOKA WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_AUTORIZIMTRUPI", "SELECT T_AUTORIZIMTRUPI.* FROM T_AUTORIZIMTRUPI INNER JOIN T_AUTORIZIMKOKA ON dbo.T_AUTORIZIMTRUPI.IDAUTORIZIMKOKA = dbo.T_AUTORIZIMKOKA.IDAUTORIZIMEKOKA  WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_AZHORNIMKFKOKA", "SELECT * FROM   T_AZHORNIMKFKOKA WHERE IDNDERMARRJE in (" + inidnderm + ")  ", ds, ukrijua);
            ukrijua = krijodt("T_AZHORNIMKFTRUPI", "SELECT  T_AZHORNIMKFTRUPI.* FROM  T_AZHORNIMKFTRUPI INNER JOIN   T_AZHORNIMKFKOKA ON dbo.T_AZHORNIMKFTRUPI.IDAZHORNIMKFKOKA = dbo.T_AZHORNIMKFKOKA.IDAZHORNIMKFKOKA WHERE IDNDERMARRJE in (" + inidnderm + ") ", ds, ukrijua);
            #endregion
            #region B C D
            ukrijua = krijodt("T_BANKA", "SELECT * FROM  T_BANKA WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_BURIME", "SELECT * FROM  T_BURIME WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_BUXHETI", "SELECT dbo.T_BUXHETI.* FROM t_buxheti INNER JOIN dbo.T_LLOGARI ON IDLIDHESE=IDLLOGARI AND IDLLOJBUXHETI=1 WHERE IDNDERMARJE in (" + inidnderm + ") ", ds, ukrijua);
            ukrijua = krijodt("T_BUXHETI2", "SELECT dbo.T_BUXHETI.* FROM t_buxheti INNER JOIN dbo.T_KPF ON IDLIDHESE=IDKPF AND IDLLOJBUXHETI=2 WHERE  IDNDERMARJE in (" + inidnderm + ")  ", ds, ukrijua);
            ukrijua = krijodt("T_BUXHETI3", "SELECT dbo.T_BUXHETI.* FROM t_buxheti INNER JOIN dbo.T_PASQYRAFINANCIAREKOKA ON IDLIDHESE=IDPASQFINKOKA AND IDLLOJBUXHETI=3 WHERE  IDNDERMARJE in (" + inidnderm + ") ", ds, ukrijua);
            ukrijua = krijodt("T_BUXHETI4", "SELECT dbo.T_BUXHETI.* FROM t_buxheti INNER JOIN dbo.T_KLIENTFURNITOR ON IDLIDHESE=IDKLIENTFURNITOR AND IDLLOJBUXHETI=4 WHERE  IDNDERMARJE in (" + inidnderm + ")  ", ds, ukrijua);
            ukrijua = krijodt("T_BUXHETI5", "SELECT dbo.T_BUXHETI.* FROM t_buxheti INNER JOIN dbo.T_ARTIKULLI ON IDLIDHESE=IDARTIKULLI AND IDLLOJBUXHETI=6 WHERE  IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_CMIMARTIKULLI", "SELECT * FROM dbo.T_CMIMARTIKULLI WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_DEGEADMINISTRATIVE", "SELECT * FROM   T_DEGEADMINISTRATIVE WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_DETAJIMART", "SELECT T_DETAJIMART.* FROM T_DETAJIMART INNER JOIN dbo.T_ARTIKULLI ON dbo.T_DETAJIMART.IDARTIKULLI = dbo.T_ARTIKULLI.IDARTIKULLI WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_DETAJIMARTIKULLI", "SELECT * FROM  T_DETAJIMARTIKULLI WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_DETAJIMREGJISTRIM", "SELECT T_DETAJIMREGJISTRIM.* FROM T_DETAJIMREGJISTRIM INNER JOIN dbo.T_DETAJIMARTIKULLI ON dbo.T_DETAJIMREGJISTRIM.IDDETAJIM = dbo.T_DETAJIMARTIKULLI.IDDETAJIMARTIKULLI WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_DETAJIMREGJISTRIMMAGAZINE", "SELECT T_DETAJIMREGJISTRIMMAGAZINE.* FROM T_DETAJIMREGJISTRIMMAGAZINE INNER JOIN dbo.T_DETAJIMARTIKULLI ON dbo.T_DETAJIMREGJISTRIMMAGAZINE.IDDETAJIM = dbo.T_DETAJIMARTIKULLI.IDDETAJIMARTIKULLI WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_DOKUMENTLIDHESKOKA", "SELECT * FROM dbo.T_DOKUMENTLIDHESKOKA WHERE IDLLOJDOK=10 and  IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);//gjeneruar nga lidhja e dokumentave
            ukrijua = krijodt("T_DOKUMENTLIDHESKOKA2", "SELECT * FROM dbo.T_DOKUMENTLIDHESKOKA WHERE IDLLOJDOK in (3,4) and IDNDERMARJE in (" + inidnderm + ") ", ds, ukrijua);//gjeneruar nga arka banka
            ukrijua = krijodt("T_DOKUMENTLIDHESKOKA3", "SELECT * FROM dbo.T_DOKUMENTLIDHESKOKA WHERE idllojdok =20 and  IDNDERMARJE in (" + inidnderm + ") ", ds, ukrijua);//gjeneruar nga veprimekf
            ukrijua = krijodt("T_DOKUMENTLIDHESTRUPI", "SELECT T_DOKUMENTLIDHESTRUPI.* FROM dbo.T_DOKUMENTLIDHESTRUPI INNER JOIN dbo.T_DOKUMENTLIDHESKOKA ON  dbo.T_DOKUMENTLIDHESTRUPI.IDKOKA = dbo.T_DOKUMENTLIDHESKOKA.IDKOKA WHERE IDLLOJDOK=10 and  IDNDERMARJE in (" + inidnderm + ")  AND LLOJDOKUMENTI IN (SELECT IDNIVEL FROM dbo.T_NIVELREGJISTRIMI WHERE IDKATDOK IN (1,2))", ds, ukrijua);//te gjeneruar nga lidhja e dokumentave te tipit shitje blerje
            ukrijua = krijodt("T_DOKUMENTLIDHESTRUPI2", "SELECT T_DOKUMENTLIDHESTRUPI.* FROM dbo.T_DOKUMENTLIDHESTRUPI INNER JOIN dbo.T_DOKUMENTLIDHESKOKA ON  dbo.T_DOKUMENTLIDHESTRUPI.IDKOKA = dbo.T_DOKUMENTLIDHESKOKA.IDKOKA WHERE IDLLOJDOK=10 and  IDNDERMARJE in (" + inidnderm + ") AND LLOJDOKUMENTI IN (SELECT IDNIVEL FROM dbo.T_NIVELREGJISTRIMI WHERE IDKATDOK IN (3,4))", ds, ukrijua);//te gjeneruar nga lidhja e dokumentave te tipit arka banka
            ukrijua = krijodt("T_DOKUMENTLIDHESTRUPI3", "SELECT T_DOKUMENTLIDHESTRUPI.* FROM dbo.T_DOKUMENTLIDHESTRUPI INNER JOIN dbo.T_DOKUMENTLIDHESKOKA ON  dbo.T_DOKUMENTLIDHESTRUPI.IDKOKA = dbo.T_DOKUMENTLIDHESKOKA.IDKOKA WHERE IDLLOJDOK=10 and  IDNDERMARJE in (" + inidnderm + ")  AND LLOJDOKUMENTI IN (SELECT IDNIVEL FROM dbo.T_NIVELREGJISTRIMI WHERE IDKATDOK IN (20))", ds, ukrijua);// te gjeneruar nga lidhja e dokumentave te tipit veprimekf
            ukrijua = krijodt("T_DOKUMENTLIDHESTRUPI4", "SELECT T_DOKUMENTLIDHESTRUPI.* FROM dbo.T_DOKUMENTLIDHESTRUPI INNER JOIN dbo.T_DOKUMENTLIDHESKOKA ON  dbo.T_DOKUMENTLIDHESTRUPI.IDKOKA = dbo.T_DOKUMENTLIDHESKOKA.IDKOKA WHERE IDLLOJDOK in (3,4)  and IDNDERMARJE in (" + inidnderm + ") AND LLOJDOKUMENTI IN (SELECT IDNIVEL FROM dbo.T_NIVELREGJISTRIMI WHERE IDKATDOK IN (1,2))", ds, ukrijua);//te gjeneruar nga arka banka te tipit shitje blerje
            ukrijua = krijodt("T_DOKUMENTLIDHESTRUPI5", "SELECT T_DOKUMENTLIDHESTRUPI.* FROM dbo.T_DOKUMENTLIDHESTRUPI INNER JOIN dbo.T_DOKUMENTLIDHESKOKA ON  dbo.T_DOKUMENTLIDHESTRUPI.IDKOKA = dbo.T_DOKUMENTLIDHESKOKA.IDKOKA WHERE IDLLOJDOK in (3,4) and IDNDERMARJE in (" + inidnderm + ")  AND LLOJDOKUMENTI IN (SELECT IDNIVEL FROM dbo.T_NIVELREGJISTRIMI WHERE IDKATDOK IN (3,4))", ds, ukrijua);//te gjeneruar nga arka banka te tipit arka banka
            ukrijua = krijodt("T_DOKUMENTLIDHESTRUPI6", "SELECT T_DOKUMENTLIDHESTRUPI.* FROM dbo.T_DOKUMENTLIDHESTRUPI INNER JOIN dbo.T_DOKUMENTLIDHESKOKA ON  dbo.T_DOKUMENTLIDHESTRUPI.IDKOKA = dbo.T_DOKUMENTLIDHESKOKA.IDKOKA WHERE IDLLOJDOK in (3,4) and IDNDERMARJE in (" + inidnderm + ")  AND LLOJDOKUMENTI IN (SELECT IDNIVEL FROM dbo.T_NIVELREGJISTRIMI WHERE IDKATDOK IN (20))", ds, ukrijua);//te gjeneruar nga arka banka te tipit veprimekf
            ukrijua = krijodt("T_DOKUMENTLIDHESTRUPI7", "SELECT T_DOKUMENTLIDHESTRUPI.* FROM dbo.T_DOKUMENTLIDHESTRUPI INNER JOIN dbo.T_DOKUMENTLIDHESKOKA ON  dbo.T_DOKUMENTLIDHESTRUPI.IDKOKA = dbo.T_DOKUMENTLIDHESKOKA.IDKOKA WHERE idllojdok =20 and IDNDERMARJE in (" + inidnderm + ")  AND LLOJDOKUMENTI IN (SELECT IDNIVEL FROM dbo.T_NIVELREGJISTRIMI WHERE IDKATDOK IN (1,2))", ds, ukrijua);// te gjeneruar nga veprimekf te tipit shitje blerje
            ukrijua = krijodt("T_DOKUMENTLIDHESTRUPI8", "SELECT T_DOKUMENTLIDHESTRUPI.* FROM dbo.T_DOKUMENTLIDHESTRUPI INNER JOIN dbo.T_DOKUMENTLIDHESKOKA ON  dbo.T_DOKUMENTLIDHESTRUPI.IDKOKA = dbo.T_DOKUMENTLIDHESKOKA.IDKOKA WHERE idllojdok =20 and IDNDERMARJE in (" + inidnderm + ")  AND LLOJDOKUMENTI IN (SELECT IDNIVEL FROM dbo.T_NIVELREGJISTRIMI WHERE IDKATDOK IN (3,4))", ds, ukrijua);//te gjeneruar nga veprimekf te tipit arka banka
            ukrijua = krijodt("T_DOKUMENTLIDHESTRUPI9", "SELECT T_DOKUMENTLIDHESTRUPI.* FROM dbo.T_DOKUMENTLIDHESTRUPI INNER JOIN dbo.T_DOKUMENTLIDHESKOKA ON  dbo.T_DOKUMENTLIDHESTRUPI.IDKOKA = dbo.T_DOKUMENTLIDHESKOKA.IDKOKA WHERE idllojdok =20 and IDNDERMARJE in (" + inidnderm + ") AND LLOJDOKUMENTI IN (SELECT IDNIVEL FROM dbo.T_NIVELREGJISTRIMI WHERE IDKATDOK IN (20))", ds, ukrijua);// te gjeneruar nga veprimekf te tipit veprimekf           
            #endregion
            #region E F
            ukrijua = krijodt("T_ETAPAAPROVIMI", "SELECT * FROM  T_ETAPAAPROVIMI WHERE llojaprovuesi=1 and IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);//perdoruesit
            ukrijua = krijodt("T_ETAPAAPROVIMI2", "SELECT * FROM  T_ETAPAAPROVIMI WHERE llojaprovuesi=2 and IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);//rolet
            ukrijua = krijodt("T_FILTERKOKA", "SELECT * FROM  T_FILTERKOKA WHERE IDNDERMARRJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_FILTERTRUPI", "SELECT  T_FILTERTRUPI.* FROM  T_FILTERTRUPI INNER JOIN  T_FILTERKOKA ON dbo.T_FILTERTRUPI.IDKOKAFILTER = dbo.T_FILTERKOKA.IDKOKAFILTER WHERE IDNDERMARRJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_FILTRAGRIDA", "SELECT * FROM  T_FILTRAGRIDA WHERE IDNDERMARJE in (" + inidnderm + ") and GRIDKOKAID IN( SELECT GRIDKOKAID FROM T_GRIDAKOKA INNER JOIN dbo.T_KONFIGAMBJENTE  ON dbo.T_GRIDAKOKA.IDKONFIGURIM = dbo.T_KONFIGAMBJENTE.IDKONFIGAMBJENTE WHERE IDKATDOK<>5  AND IDNDERMARJE in (" + inidnderm + "))", ds, ukrijua);
            ukrijua = krijodt("T_FILTRAGRIDA2", "SELECT * FROM  T_FILTRAGRIDA WHERE IDNDERMARJE in (" + inidnderm + ") and GRIDKOKAID NOT IN( SELECT GRIDKOKAID FROM T_GRIDAKOKA INNER JOIN dbo.T_KONFIGAMBJENTE  ON dbo.T_GRIDAKOKA.IDKONFIGURIM = dbo.T_KONFIGAMBJENTE.IDKONFIGAMBJENTE WHERE IDKATDOK<>5  AND IDNDERMARJE in (" + inidnderm + "))", ds, ukrijua);
            ukrijua = krijodt("T_FLETEDOGANOREDETAJIM", "SELECT T_FLETEDOGANOREDETAJIM.* FROM T_FLETEDOGANOREDETAJIM INNER JOIN dbo.T_FLETEDOGANORETRUPI ON dbo.T_FLETEDOGANOREDETAJIM.IDFLETEDOGANORETRUPI = dbo.T_FLETEDOGANORETRUPI.IDFLETEDOGANORETRUPI INNER JOIN dbo.T_FLETEDOGANOREKOKA ON dbo.T_FLETEDOGANORETRUPI.IDFLETEDOGANORE = dbo.T_FLETEDOGANOREKOKA.IDFLETEDOGANORE WHERE IDNDERM in (" + inidnderm + ")  ", ds, ukrijua);
            ukrijua = krijodt("T_FLETEDOGANOREKOKA", "SELECT * FROM   T_FLETEDOGANOREKOKA WHERE IDNDERM in (" + inidnderm + ") ", ds, ukrijua);
            ukrijua = krijodt("T_FLETEDOGANORETAKSA", "SELECT T_FLETEDOGANORETAKSA.* FROM T_FLETEDOGANORETAKSA  INNER JOIN dbo.T_FLETEDOGANOREKOKA ON dbo.T_FLETEDOGANORETAKSA.IDFLETEDOGANORE = dbo.T_FLETEDOGANOREKOKA.IDFLETEDOGANORE WHERE IDNDERM in (" + inidnderm + ") ", ds, ukrijua);
            ukrijua = krijodt("T_FLETEDOGANORETRUPI", "SELECT T_FLETEDOGANORETRUPI.* FROM T_FLETEDOGANORETRUPI  INNER JOIN dbo.T_FLETEDOGANOREKOKA ON dbo.T_FLETEDOGANORETRUPI.IDFLETEDOGANORE = dbo.T_FLETEDOGANOREKOKA.IDFLETEDOGANORE WHERE IDNDERM in (" + inidnderm + ") ", ds, ukrijua);
            ukrijua = krijodt("T_FLETEDOGANORETVSH", "SELECT T_FLETEDOGANORETVSH.* FROM T_FLETEDOGANORETVSH  INNER JOIN dbo.T_FLETEDOGANOREKOKA ON dbo.T_FLETEDOGANORETVSH.IDFLETEDOGANORE = dbo.T_FLETEDOGANOREKOKA.IDFLETEDOGANORE  WHERE IDNDERM in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_FURNITOREARTIKULLI", " SELECT T_FURNITOREARTIKULLI.* FROM T_FURNITOREARTIKULLI INNER JOIN dbo.T_ARTIKULLI ON dbo.T_FURNITOREARTIKULLI.IDARTIKULLI = dbo.T_ARTIKULLI.IDARTIKULLI WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_FUSHASHTESE", " SELECT T_FUSHASHTESE.* FROM T_FUSHASHTESE INNER JOIN dbo.T_MODELIFUSHASHTESE ON  dbo.T_FUSHASHTESE.IDMODELIFUSHASHTESE = dbo.T_MODELIFUSHASHTESE.IDMODELIFUSHASHTESE WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_FORMULA", " SELECT T_FORMULA.* FROM T_FORMULA WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            #endregion
            #region G H I J K
            ukrijua = krijodt("T_GJENDJEKF", "SELECT dbo.T_GJENDJEKF.* FROM dbo.T_GJENDJEKF INNER JOIN dbo.T_KOKASHITJE ON dbo.T_GJENDJEKF.IDODOKGJENDJEKF=dbo.T_KOKASHITJE.IDSHITJEKOKA AND dbo.T_GJENDJEKF.IDNIVELIGJENDJEKF=dbo.T_KOKASHITJE.IDNIVEL WHERE IDNDERM in (" + inidnderm + ")  ", ds, ukrijua);//gjendjekf te gjeneruar nga shitja blerja
            ukrijua = krijodt("T_GJENDJEKF2", "SELECT dbo.T_GJENDJEKF.* FROM dbo.T_GJENDJEKF INNER JOIN dbo.T_VEPRIMBANKAKOKA ON dbo.T_GJENDJEKF.IDODOKGJENDJEKF=dbo.T_VEPRIMBANKAKOKA.IDKOKA AND dbo.T_GJENDJEKF.IDNIVELIGJENDJEKF=dbo.T_VEPRIMBANKAKOKA.IDNIVEL WHERE   IDNDERmarje in (" + inidnderm + ") ", ds, ukrijua);//gjendje kf te gjeneruar nga arka banka
            ukrijua = krijodt("T_GJENDJEKF3", "SELECT dbo.T_GJENDJEKF.* FROM dbo.T_GJENDJEKF INNER JOIN dbo.T_DOKUMENTLIDHESKOKA ON dbo.T_GJENDJEKF.IDODOKGJENDJEKF=dbo.T_DOKUMENTLIDHESKOKA.IDKOKA AND dbo.T_GJENDJEKF.IDNIVELIGJENDJEKF=dbo.T_DOKUMENTLIDHESKOKA.IDNIVEL WHERE IDLLOJDOK=10 and  IDNDERMARJE in (" + inidnderm + ")  ", ds, ukrijua);// gjendje kf te gjeneruar nga dokumenti lidhes i gjeneruar nga dokumenti lidhes
            ukrijua = krijodt("T_GJENDJEKF6", "SELECT dbo.T_GJENDJEKF.* FROM dbo.T_GJENDJEKF INNER JOIN dbo.T_DOKUMENTLIDHESKOKA ON dbo.T_GJENDJEKF.IDODOKGJENDJEKF=dbo.T_DOKUMENTLIDHESKOKA.IDKOKA AND dbo.T_GJENDJEKF.IDNIVELIGJENDJEKF=dbo.T_DOKUMENTLIDHESKOKA.IDNIVEL WHERE IDLLOJDOK in (3,4) and  IDNDERMARJE in (" + inidnderm + ")  ", ds, ukrijua);//gjendje kf i gjeneruar nga dokumenti lidhes i gjeneruar nga arka banka
            ukrijua = krijodt("T_GJENDJEKF7", "SELECT dbo.T_GJENDJEKF.* FROM dbo.T_GJENDJEKF INNER JOIN dbo.T_DOKUMENTLIDHESKOKA ON dbo.T_GJENDJEKF.IDODOKGJENDJEKF=dbo.T_DOKUMENTLIDHESKOKA.IDKOKA AND dbo.T_GJENDJEKF.IDNIVELIGJENDJEKF=dbo.T_DOKUMENTLIDHESKOKA.IDNIVEL WHERE IDLLOJDOK=20 and  IDNDERMARJE in (" + inidnderm + ")  ", ds, ukrijua);//gjendje kf i gjeneruar nga dokumenti lidhes i gjeneruar nga veprimekf
            ukrijua = krijodt("T_GJENDJEKF4", "SELECT dbo.T_GJENDJEKF.* FROM dbo.T_GJENDJEKF INNER JOIN dbo.T_VEPRIMEKFKOKA ON dbo.T_GJENDJEKF.IDODOKGJENDJEKF=dbo.T_VEPRIMEKFKOKA.IDVEPRIMKFKOKA AND dbo.T_GJENDJEKF.IDNIVELIGJENDJEKF=dbo.T_VEPRIMEKFKOKA.IDNIVEL WHERE IDNDERM in (" + inidnderm + ")", ds, ukrijua);//gjendje kf te gjeneruar nga veprimekf
            ukrijua = krijodt("T_GJENDJEKF5", "SELECT dbo.T_GJENDJEKF.* FROM dbo.T_GJENDJEKF INNER JOIN dbo.T_AZHORNIMKFKOKA ON dbo.T_GJENDJEKF.IDODOKGJENDJEKF=dbo.T_AZHORNIMKFKOKA.IDAZHORNIMKFKOKA AND dbo.T_GJENDJEKF.IDNIVELIGJENDJEKF=dbo.T_AZHORNIMKFKOKA.IDNIVEL WHERE IDNDERMARRJE  in (" + inidnderm + ") ", ds, ukrijua);//gjendje kf te gjeneruar nga azhornimekf
            ukrijua = krijodt("T_GRIDAKOKA", " SELECT T_GRIDAKOKA.* FROM T_GRIDAKOKA INNER JOIN dbo.T_KONFIGAMBJENTE  ON dbo.T_GRIDAKOKA.IDKONFIGURIM = dbo.T_KONFIGAMBJENTE.IDKONFIGAMBJENTE WHERE IDKATDOK=5 AND IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_GRIDAKOKA2", " SELECT T_GRIDAKOKA.* FROM T_GRIDAKOKA INNER JOIN dbo.T_KONFIGAMBJENTE  ON dbo.T_GRIDAKOKA.IDKONFIGURIM = dbo.T_KONFIGAMBJENTE.IDKONFIGAMBJENTE WHERE IDKATDOK<>5  AND IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_GRIDATRUPI", "SELECT T_GRIDATRUPI.* FROM T_GRIDATRUPI INNER JOIN  T_GRIDAKOKA ON dbo.T_GRIDATRUPI.GRIDAKOKAID = dbo.T_GRIDAKOKA.GRIDKOKAID INNER JOIN dbo.T_KONFIGAMBJENTE  ON dbo.T_GRIDAKOKA.IDKONFIGURIM = dbo.T_KONFIGAMBJENTE.IDKONFIGAMBJENTE WHERE IDKATDOK=5 AND (IDKONFIGLUPA  IN (0, - 1, 1)or idkonfiglupa is null) AND IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);//gridatrupi te konfigurimit te fletes kontabel qe nuk kane lupa
            ukrijua = krijodt("T_GRIDATRUPI2", "SELECT T_GRIDATRUPI.* FROM T_GRIDATRUPI INNER JOIN  T_GRIDAKOKA ON dbo.T_GRIDATRUPI.GRIDAKOKAID = dbo.T_GRIDAKOKA.GRIDKOKAID INNER JOIN dbo.T_KONFIGAMBJENTE  ON dbo.T_GRIDAKOKA.IDKONFIGURIM = dbo.T_KONFIGAMBJENTE.IDKONFIGAMBJENTE WHERE IDKATDOK=5 AND (IDKONFIGLUPA NOT IN (0, - 1, 1)) AND  IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);//gridatrupi te konfigurimit te fletes kontabel qe kane lupa
            ukrijua = krijodt("T_GRIDATRUPI3", "SELECT T_GRIDATRUPI.* FROM T_GRIDATRUPI INNER JOIN  T_GRIDAKOKA ON dbo.T_GRIDATRUPI.GRIDAKOKAID = dbo.T_GRIDAKOKA.GRIDKOKAID INNER JOIN dbo.T_KONFIGAMBJENTE  ON dbo.T_GRIDAKOKA.IDKONFIGURIM = dbo.T_KONFIGAMBJENTE.IDKONFIGAMBJENTE WHERE IDKATDOK<>5 AND (IDKONFIGLUPA  IN (0, - 1, 1) or idkonfiglupa is null) AND IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);//gridatrupi te konfigurimit te ndryshme nga fleta kontabel qe nuk kane lupa
            ukrijua = krijodt("T_GRIDATRUPI4", "SELECT T_GRIDATRUPI.* FROM T_GRIDATRUPI INNER JOIN  T_GRIDAKOKA ON dbo.T_GRIDATRUPI.GRIDAKOKAID = dbo.T_GRIDAKOKA.GRIDKOKAID INNER JOIN dbo.T_KONFIGAMBJENTE  ON dbo.T_GRIDAKOKA.IDKONFIGURIM = dbo.T_KONFIGAMBJENTE.IDKONFIGAMBJENTE WHERE IDKATDOK<>5 AND (IDKONFIGLUPA not  IN (0, - 1, 1)) AND IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);//gridatrupi te konfigurimit te ndryshme nga fleta kontabel qe kane lupa
            ukrijua = krijodt("T_GRUPBANKE", "SELECT * FROM  T_GRUPBANKE WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_GRUPPUNONJESISH", "SELECT * FROM  T_GRUPPUNONJESISH WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_GRUPEKF", "SELECT * FROM  T_GRUPEKF WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_GRUPIMDOKUMENTIKOKA", "SELECT * FROM  T_GRUPIMDOKUMENTIKOKA WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_GRUPIMDOKUMENTITRUPI", "SELECT T_GRUPIMDOKUMENTITRUPI.* FROM  T_GRUPIMDOKUMENTITRUPI INNER JOIN T_GRUPIMDOKUMENTIKOKA ON T_GRUPIMDOKUMENTITRUPI.IDGRUPIMKOKA=T_GRUPIMDOKUMENTIKOKA.IDGRUPIMKOKA WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_GRUPILLOGARIA", "SELECT * FROM  T_GRUPILLOGARIA WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_GRUPKONTABILIZIMI", "SELECT * FROM  T_GRUPKONTABILIZIMI WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_INFOKOKA", "SELECT * FROM  T_INFOKOKA WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_INFOTRUPI", "SELECT T_INFOTRUPI.* FROM T_INFOTRUPI INNER join T_INFOKOKA ON T_INFOTRUPI.IDINFOKOKA=T_INFOKOKA.IDINFOKOKA AND T_INFOKOKA.IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_KLIENTFURNITOR", "SELECT * FROM   T_KLIENTFURNITOR WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_KATEGORIPAGE", "SELECT * FROM   T_KATEGORIPAGE WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_KATEGORISHPENZIMI", "SELECT * FROM   T_KATEGORISHPENZIMI WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_KODBARI", " SELECT  T_KODBARI.* FROM  T_KODBARI INNER JOIN dbo.T_ARTIKULLI ON dbo.T_KODBARI.IDARTIKULLI = dbo.T_ARTIKULLI.IDARTIKULLI WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_KODIFIKIMARTIKULLI", "SELECT * FROM  T_KODIFIKIMARTIKULLI WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_KOKAFLETEKONTABEL", "SELECT * FROM dbo.T_KOKAFLETEKONTABEL WHERE IDNDERMARJE in (" + inidnderm + ") and IDKATEGORIA =5", ds, ukrijua);//te gjeneruar nga fleta kontabel
            ukrijua = krijodt("T_KOKAFLETEKONTABEL2", "SELECT * FROM dbo.T_KOKAFLETEKONTABEL WHERE IDNDERMARJE in (" + inidnderm + ") and IDKATEGORIA in (1,2)", ds, ukrijua);// te gjeneruar nga shitje blerja
            ukrijua = krijodt("T_KOKAFLETEKONTABEL3", "SELECT * FROM dbo.T_KOKAFLETEKONTABEL WHERE IDNDERMARJE in (" + inidnderm + ") and IDKATEGORIA in (3,4)", ds, ukrijua);//te gjeneruar nga arka banka
            ukrijua = krijodt("T_KOKAFLETEKONTABEL4", "SELECT * FROM dbo.T_KOKAFLETEKONTABEL WHERE IDNDERMARJE in (" + inidnderm + ") and IDKATEGORIA in (6) and idgjenerues in (SELECT idkokamagazina FROM dbo.T_KOKAMAGAZINA WHERE IDNDERM in (" + inidnderm + ") AND (IDNIVELGJENERUES IS NULL OR IDNIVELGJENERUES IN (SELECT IDNIVEL FROM dbo.T_NIVELREGJISTRIMI WHERE IDKATDOK=6)))", ds, ukrijua);// te gjeneruar nga magazina e gjeneruar nga ambjenti i magazines
            ukrijua = krijodt("T_KOKAFLETEKONTABEL10", "SELECT * FROM dbo.T_KOKAFLETEKONTABEL WHERE IDNDERMARJE in (" + inidnderm + ") and IDKATEGORIA in (6) and idgjenerues in (SELECT idkokamagazina FROM dbo.T_KOKAMAGAZINA WHERE IDNDERM in (" + inidnderm + ")  AND (IDNIVELGJENERUES IN (SELECT IDNIVEL FROM dbo.T_NIVELREGJISTRIMI WHERE IDKATDOK in (1,2))))", ds, ukrijua);// te gjeneruar nga magazina e gjeneruar nga shitja/ blerja
            ukrijua = krijodt("T_KOKAFLETEKONTABEL11", "SELECT * FROM dbo.T_KOKAFLETEKONTABEL WHERE IDNDERMARJE in (" + inidnderm + ") and IDKATEGORIA in (6)  and idgjenerues in (SELECT idkokamagazina FROM dbo.T_KOKAMAGAZINA WHERE IDNDERM in (" + inidnderm + ") AND ( IDNIVELGJENERUES IN (SELECT IDNIVEL FROM dbo.T_NIVELREGJISTRIMI WHERE IDKATDOK=7)))", ds, ukrijua);// te gjeneruar nga magazina e gjeneruar nga shperndarje shpenzime
            ukrijua = krijodt("T_KOKAFLETEKONTABEL5", "SELECT * FROM dbo.T_KOKAFLETEKONTABEL WHERE IDNDERMARJE in (" + inidnderm + ") and IDKATEGORIA in (7)", ds, ukrijua);// te gjeneruar nga shperdarje shpenzimesh
            ukrijua = krijodt("T_KOKAFLETEKONTABEL6", "SELECT * FROM dbo.T_KOKAFLETEKONTABEL WHERE IDNDERMARJE in (" + inidnderm + ") and IDKATEGORIA in (8)", ds, ukrijua);//te gjeneruar nga fleta doganore
            ukrijua = krijodt("T_KOKAFLETEKONTABEL7", "SELECT * FROM dbo.T_KOKAFLETEKONTABEL WHERE IDNDERMARJE in (" + inidnderm + ") and IDKATEGORIA in (10) and idgjenerues in (SELECT idkoka FROM dbo.T_DOKUMENTLIDHESKOKA WHERE IDLLOJDOK=10 and  IDNDERMARJE in (" + inidnderm + ") )", ds, ukrijua);// te gjeneruar nga lidhja e dokumentave te gjeneruar nga lidhja e dokumentave
            ukrijua = krijodt("T_KOKAFLETEKONTABEL12", "SELECT * FROM dbo.T_KOKAFLETEKONTABEL WHERE IDNDERMARJE in (" + inidnderm + ") and IDKATEGORIA in (10) and idgjenerues in (SELECT idkoka FROM dbo.T_DOKUMENTLIDHESKOKA WHERE IDLLOJDOK in (3,4) and  IDNDERMARJE in (" + inidnderm + ") )", ds, ukrijua);// te gjeneruar nga lidhja e dokumentave te gjeneruar nga arka banka
            ukrijua = krijodt("T_KOKAFLETEKONTABEL13", "SELECT * FROM dbo.T_KOKAFLETEKONTABEL WHERE IDNDERMARJE in (" + inidnderm + ") and IDKATEGORIA in (10) and idgjenerues in (SELECT idkoka FROM dbo.T_DOKUMENTLIDHESKOKA WHERE IDLLOJDOK=20 and  IDNDERMARJE in (" + inidnderm + ") )", ds, ukrijua);// te gjeneruar nga lidhja e dokumentave te gjeneruar nga veprimekf
            ukrijua = krijodt("T_KOKAFLETEKONTABEL8", "SELECT * FROM dbo.T_KOKAFLETEKONTABEL WHERE IDNDERMARJE in (" + inidnderm + ") and IDKATEGORIA in (11)", ds, ukrijua);// te gjeneruar nga azhornimi kf
            ukrijua = krijodt("T_KOKAFLETEKONTABEL9", "SELECT * FROM dbo.T_KOKAFLETEKONTABEL WHERE IDNDERMARJE in (" + inidnderm + ") and IDKATEGORIA in (20)", ds, ukrijua);// te gjeneruar nga veprimet kf
            ukrijua = krijodt("T_KOKAFLETEKONTABEL14", "SELECT * FROM dbo.T_KOKAFLETEKONTABEL WHERE IDNDERMARJE in (" + inidnderm + ") and IDKATEGORIA in (38)", ds, ukrijua);// te gjeneruar nga LISTPAGESA  
            ukrijua = krijodt("T_KOKAFLETEKONTABEL15", "SELECT * FROM dbo.T_KOKAFLETEKONTABEL WHERE IDNDERMARJE in (" + inidnderm + ") and IDKATEGORIA in (45)", ds, ukrijua);// te gjeneruar nga ekzekutim prodhimi  
            ukrijua = krijodt("T_KOKAFLETEKONTABEL16", "SELECT * FROM dbo.T_KOKAFLETEKONTABEL WHERE IDNDERMARJE in (" + inidnderm + ") and IDKATEGORIA in (64)", ds, ukrijua);// te gjeneruar nga mbylljekf 
            ukrijua = krijodt("T_KOKAKATEGORIZBRITJE", "SELECT * FROM   T_KOKAKATEGORIZBRITJE WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_KOKAFORMATIMPORTI", "SELECT * FROM   T_KOKAFORMATIMPORTI WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_KOKAQENDRAKOSTO", "SELECT * FROM   T_KOKAQENDRAKOSTO WHERE idgjenerues is null IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);// te gjeneruara nga ambjenti i shperndarjes
            ukrijua = krijodt("T_KOKAQENDRAKOSTO17", "SELECT T_KOKAQENDRAKOSTO.* FROM dbo.T_KOKAQENDRAKOSTO WHERE IDNDERMARJE in (" + inidnderm + ") and idgjenerues in (select idkokafletekontabel from t_kokafletekontabel where idndermarje in (" + inidnderm + ") and idkategoria =5)", ds, ukrijua);//te gjeneruar nga fleta kontabel
            ukrijua = krijodt("T_KOKAQENDRAKOSTO2", "SELECT T_KOKAQENDRAKOSTO.* FROM dbo.T_KOKAQENDRAKOSTO WHERE IDNDERMARJE in (" + inidnderm + ")and idgjenerues in (select idkokafletekontabel from t_kokafletekontabel where idndermarje in (" + inidnderm + ")  and IDKATEGORIA in (1,2))", ds, ukrijua);// te gjeneruar nga shitje blerja
            ukrijua = krijodt("T_KOKAQENDRAKOSTO3", "SELECT T_KOKAQENDRAKOSTO.* FROM dbo.T_KOKAQENDRAKOSTO WHERE IDNDERMARJE in (" + inidnderm + ")and idgjenerues in (select idkokafletekontabel from t_kokafletekontabel where idndermarje in (" + inidnderm + ")  and IDKATEGORIA in (3,4))", ds, ukrijua);//te gjeneruar nga arka banka
            ukrijua = krijodt("T_KOKAQENDRAKOSTO4", "SELECT T_KOKAQENDRAKOSTO.* FROM dbo.T_KOKAQENDRAKOSTO WHERE IDNDERMARJE in (" + inidnderm + ")and idgjenerues in (select idkokafletekontabel from t_kokafletekontabel where idndermarje in (" + inidnderm + ")  and IDKATEGORIA in (6) and idgjenerues in (SELECT idkokamagazina FROM dbo.T_KOKAMAGAZINA WHERE IDNDERM in (" + inidnderm + ") AND (IDNIVELGJENERUES IS NULL OR IDNIVELGJENERUES IN (SELECT IDNIVEL FROM dbo.T_NIVELREGJISTRIMI WHERE IDKATDOK=6))))", ds, ukrijua);// te gjeneruar nga magazina e gjeneruar nga ambjenti i magazines
            ukrijua = krijodt("T_KOKAQENDRAKOSTO10", "SELECT T_KOKAQENDRAKOSTO.* FROM dbo.T_KOKAQENDRAKOSTO WHERE IDNDERMARJE in (" + inidnderm + ") and idgjenerues in (select idkokafletekontabel from t_kokafletekontabel where idndermarje in (" + inidnderm + ") and IDKATEGORIA in (6) and idgjenerues in (SELECT idkokamagazina FROM dbo.T_KOKAMAGAZINA WHERE IDNDERM in (" + inidnderm + ")  AND (IDNIVELGJENERUES IN (SELECT IDNIVEL FROM dbo.T_NIVELREGJISTRIMI WHERE IDKATDOK in (1,2)))))", ds, ukrijua);// te gjeneruar nga magazina e gjeneruar nga shitja/ blerja
            ukrijua = krijodt("T_KOKAQENDRAKOSTO11", "SELECT T_KOKAQENDRAKOSTO.* FROM dbo.T_KOKAQENDRAKOSTO WHERE IDNDERMARJE in (" + inidnderm + ") and idgjenerues in (select idkokafletekontabel from t_kokafletekontabel where idndermarje in (" + inidnderm + ") and IDKATEGORIA in (6)  and idgjenerues in (SELECT idkokamagazina FROM dbo.T_KOKAMAGAZINA WHERE IDNDERM in (" + inidnderm + ") AND ( IDNIVELGJENERUES IN (SELECT IDNIVEL FROM dbo.T_NIVELREGJISTRIMI WHERE IDKATDOK=7))))", ds, ukrijua);// te gjeneruar nga magazina e gjeneruar nga shperndarje shpenzime
            ukrijua = krijodt("T_KOKAQENDRAKOSTO5", "SELECT T_KOKAQENDRAKOSTO.* FROM dbo.T_KOKAQENDRAKOSTO WHERE IDNDERMARJE in (" + inidnderm + ") and idgjenerues in (select idkokafletekontabel from t_kokafletekontabel where idndermarje in (" + inidnderm + ") and IDKATEGORIA in (7))", ds, ukrijua);// te gjeneruar nga shperdarje shpenzimesh
            ukrijua = krijodt("T_KOKAQENDRAKOSTO6", "SELECT T_KOKAQENDRAKOSTO.* FROM dbo.T_KOKAQENDRAKOSTO WHERE IDNDERMARJE in (" + inidnderm + ")and idgjenerues in (select idkokafletekontabel from t_kokafletekontabel where idndermarje in (" + inidnderm + ")  and IDKATEGORIA in (8))", ds, ukrijua);//te gjeneruar nga fleta doganore
            ukrijua = krijodt("T_KOKAQENDRAKOSTO7", "SELECT T_KOKAQENDRAKOSTO.* FROM dbo.T_KOKAQENDRAKOSTO WHERE IDNDERMARJE in (" + inidnderm + ") and idgjenerues in (select idkokafletekontabel from t_kokafletekontabel where idndermarje in (" + inidnderm + ") and IDKATEGORIA in (10) and idgjenerues in (SELECT idkoka FROM dbo.T_DOKUMENTLIDHESKOKA WHERE IDLLOJDOK=10 and  IDNDERMARJE in (" + inidnderm + ") ))", ds, ukrijua);// te gjeneruar nga lidhja e dokumentave te gjeneruar nga lidhja e dokumentave
            ukrijua = krijodt("T_KOKAQENDRAKOSTO12", "SELECT T_KOKAQENDRAKOSTO.* FROM dbo.T_KOKAQENDRAKOSTO WHERE IDNDERMARJE in (" + inidnderm + ")and idgjenerues in (select idkokafletekontabel from t_kokafletekontabel where idndermarje in (" + inidnderm + ")  and IDKATEGORIA in (10) and idgjenerues in (SELECT idkoka FROM dbo.T_DOKUMENTLIDHESKOKA WHERE IDLLOJDOK in (3,4) and  IDNDERMARJE in (" + inidnderm + ") ))", ds, ukrijua);// te gjeneruar nga lidhja e dokumentave te gjeneruar nga arka banka
            ukrijua = krijodt("T_KOKAQENDRAKOSTO13", "SELECT T_KOKAQENDRAKOSTO.* FROM dbo.T_KOKAQENDRAKOSTO WHERE IDNDERMARJE in (" + inidnderm + ")and idgjenerues in (select idkokafletekontabel from t_kokafletekontabel where idndermarje in (" + inidnderm + ")  and IDKATEGORIA in (10) and idgjenerues in (SELECT idkoka FROM dbo.T_DOKUMENTLIDHESKOKA WHERE IDLLOJDOK=20 and  IDNDERMARJE in (" + inidnderm + ") )", ds, ukrijua);// te gjeneruar nga lidhja e dokumentave te gjeneruar nga veprimekf
            ukrijua = krijodt("T_KOKAQENDRAKOSTO8", "SELECT T_KOKAQENDRAKOSTO.* FROM dbo.T_KOKAQENDRAKOSTO WHERE IDNDERMARJE in (" + inidnderm + ")and idgjenerues in (select idkokafletekontabel from t_kokafletekontabel where idndermarje in (" + inidnderm + ")  and IDKATEGORIA in (11))", ds, ukrijua);// te gjeneruar nga azhornimi kf
            ukrijua = krijodt("T_KOKAQENDRAKOSTO9", "SELECT T_KOKAQENDRAKOSTO.* FROM dbo.T_KOKAQENDRAKOSTO WHERE IDNDERMARJE in (" + inidnderm + ")and idgjenerues in (select idkokafletekontabel from t_kokafletekontabel where idndermarje in (" + inidnderm + ")  and IDKATEGORIA in (20))", ds, ukrijua);// te gjeneruar nga veprimet kf
            ukrijua = krijodt("T_KOKAQENDRAKOSTO14", "SELECT T_KOKAQENDRAKOSTO.* FROM dbo.T_KOKAQENDRAKOSTO WHERE IDNDERMARJE in (" + inidnderm + ")and idgjenerues in (select idkokafletekontabel from t_kokafletekontabel where idndermarje in (" + inidnderm + ")  and IDKATEGORIA in (38))", ds, ukrijua);// te gjeneruar nga LISTPAGESA  
            ukrijua = krijodt("T_KOKAQENDRAKOSTO15", "SELECT T_KOKAQENDRAKOSTO.* FROM dbo.T_KOKAQENDRAKOSTO WHERE IDNDERMARJE in (" + inidnderm + ")and idgjenerues in (select idkokafletekontabel from t_kokafletekontabel where idndermarje in (" + inidnderm + ")  and IDKATEGORIA in (45))", ds, ukrijua);// te gjeneruar nga ekzekutim prodhimi  
            ukrijua = krijodt("T_KOKAQENDRAKOSTO16", "SELECT T_KOKAQENDRAKOSTO.* FROM dbo.T_KOKAQENDRAKOSTO WHERE IDNDERMARJE in (" + inidnderm + ")and idgjenerues in (select idkokafletekontabel from t_kokafletekontabel where idndermarje in (" + inidnderm + ")  and IDKATEGORIA in (64))", ds, ukrijua);// te gjeneruar nga mbylljekf 

            ukrijua = krijodt("T_KOKAFILTRAEXPORTI", "SELECT * FROM   T_KOKAFILTRAEXPORTI WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_KONFIGIMPORTI", "SELECT * FROM   T_KONFIGIMPORTI WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_KONFIGEXPORTI", "SELECT * FROM   T_KONFIGEXPORTI WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_KOKALISTPAGESE", "SELECT * FROM dbo.T_KOKALISTPAGESE WHERE IDNDERMarje in (" + inidnderm + ") ", ds, ukrijua);
            ukrijua = krijodt("T_KOKASKEMAWORKFLOW", "SELECT * FROM dbo.T_KOKASKEMAWORKFLOW WHERE IDNDERMarje in (" + inidnderm + ") ", ds, ukrijua);
            ukrijua = krijodt("T_KOKASKEMAQK", "SELECT * FROM dbo.T_KOKASKEMAQK WHERE IDNDERMarje in (" + inidnderm + ") ", ds, ukrijua);
            ukrijua = krijodt("T_KOKAMBYLLJEKF", "SELECT * FROM dbo.T_KOKAMBYLLJEKF WHERE IDNDERMarje in (" + inidnderm + ") ", ds, ukrijua);
            ukrijua = krijodt("T_KOKAURDHERPAGESA", "SELECT * FROM dbo.T_KOKAURDHERPAGESA WHERE IDNDERMarje in (" + inidnderm + ") ", ds, ukrijua);
            ukrijua = krijodt("T_KOKAMAGAZINA", "SELECT * FROM dbo.T_KOKAMAGAZINA WHERE IDNDERM in (" + inidnderm + ")  AND (IDNIVELGJENERUES IS NULL OR IDNIVELGJENERUES IN (SELECT IDNIVEL FROM dbo.T_NIVELREGJISTRIMI WHERE IDKATDOK=6))", ds, ukrijua);//te pagjeneruarat dhe te gjeneruar nga transferimi
            ukrijua = krijodt("T_KOKAMAGAZINA2", "SELECT * FROM dbo.T_KOKAMAGAZINA WHERE IDNDERM in (" + inidnderm + ")   and IDNIVELGJENERUES IN (SELECT IDNIVEL FROM dbo.T_NIVELREGJISTRIMI WHERE IDKATDOK in (1,2))", ds, ukrijua);//te gjeneruar nga shitja
            ukrijua = krijodt("T_KOKAMAGAZINA3", "SELECT * FROM dbo.T_KOKAMAGAZINA WHERE IDNDERM in (" + inidnderm + ")  and IDNIVELGJENERUES IN (SELECT IDNIVEL FROM dbo.T_NIVELREGJISTRIMI WHERE IDKATDOK=7)", ds, ukrijua);// te gjeneruar nga shperndarje shpez
            ukrijua = krijodt("T_KOKAMAKRO", "SELECT * FROM  T_KOKAMAKRO WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_KOKAPLANIFIKIM", "SELECT * FROM dbo.T_KOKAPLANIFIKIM WHERE IDNDERMarje in (" + inidnderm + ") ", ds, ukrijua);
            ukrijua = krijodt("T_KOKAEKZEKUTIMPRODHIMI", "SELECT * FROM dbo.T_KOKAEKZEKUTIMPRODHIMI WHERE IDNDERMarje in (" + inidnderm + ") ", ds, ukrijua);
            ukrijua = krijodt("T_KOKASHITJE", "SELECT * FROM dbo.T_KOKASHITJE WHERE IDNDERM in (" + inidnderm + ") ", ds, ukrijua);
            ukrijua = krijodt("T_KOKASKEMAFLETEKONTABEL", "SELECT * FROM dbo.T_KOKASKEMAFLETEKONTABEL WHERE IDNDERMARJE in (" + inidnderm + ") ", ds, ukrijua);
            ukrijua = krijodt("T_KOMPLISTPAGESE", "SELECT  T_KOMPLISTPAGESE.* FROM T_KOMPLISTPAGESE INNER JOIN  dbo. T_TRUPILISTPAGESE ON T_KOMPLISTPAGESE.IDTRUPI=T_TRUPILISTPAGESE.IDTRUPI INNER JOIN dbo.T_KOKALISTPAGESE ON    dbo.T_TRUPILISTPAGESE.IDKOKA = dbo.T_KOKALISTPAGESE.IDKOKA WHERE IDNDERMARJE in (" + inidnderm + ") ", ds, ukrijua);
            ukrijua = krijodt("T_KOMPONENTEPAGE", "SELECT * FROM dbo.T_KOMPONENTEPAGE WHERE IDNDERMARJE in (" + inidnderm + ") ", ds, ukrijua);
            ukrijua = krijodt("T_KOMPONENTELISTPAGESEPUNONJES", "SELECT T_KOMPONENTELISTPAGESEPUNONJES.* FROM T_KOMPONENTELISTPAGESEPUNONJES INNER JOIN dbo.T_PUNONJES ON  dbo.T_KOMPONENTELISTPAGESEPUNONJES.IDPUNONJES = dbo.T_PUNONJES.IDPUNONJES    WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_KOMENTEAPROVIMI", "SELECT T_KOMENTEAPROVIMI.* FROM T_KOMENTEAPROVIMI INNER JOIN dbo.T_ETAPAAPROVIMI ON  dbo.T_KOMENTEAPROVIMI.idetape = dbo.T_ETAPAAPROVIMI.idetapa    WHERE llojaprovuesi=1 and IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);//perdoruesit
            ukrijua = krijodt("T_KOMENTEAPROVIMI2", "SELECT T_KOMENTEAPROVIMI.* FROM T_KOMENTEAPROVIMI INNER JOIN dbo.T_ETAPAAPROVIMI ON  dbo.T_KOMENTEAPROVIMI.idetape = dbo.T_ETAPAAPROVIMI.idetapa    WHERE llojaprovuesi=2 and IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);//rolet
            ukrijua = krijodt("T_KONFIG_PIVOTGRIDA_KOKA", "SELECT * FROM dbo.T_KONFIG_PIVOTGRIDA_KOKA WHERE IDNDERMARJE in (" + inidnderm + ") ", ds, ukrijua);
            ukrijua = krijodt("T_KONFIG_PIVOTGRIDA_TRUPI", "SELECT T_KONFIG_PIVOTGRIDA_TRUPI.* FROM T_KONFIG_PIVOTGRIDA_TRUPI INNER join T_KONFIG_PIVOTGRIDA_KOKA ON T_KONFIG_PIVOTGRIDA_TRUPI.IDPIVOTGRIDAKOKA=T_KONFIG_PIVOTGRIDA_KOKA.IDKONFPIVOTGRIDAKOKA AND T_KONFIG_PIVOTGRIDA_KOKA.IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_KONFIGURIMQK", "SELECT * FROM  T_KONFIGURIMQK WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_KONFIGAMBJENTE", "SELECT * FROM  T_KONFIGAMBJENTE WHERE IDKATDOK=5 AND IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_KONFIGAMBJENTE2", "SELECT * FROM  T_KONFIGAMBJENTE WHERE IDKATDOK<>5 AND IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_KONFIGURIMKASA", "SELECT * FROM  T_KONFIGURIMKASA WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_KONFIGURDHERPAGESE", "SELECT * FROM  T_KONFIGURDHERPAGESE WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_KONFLLOJRRESHTI", "  SELECT T_KONFLLOJRRESHTI.* FROM T_KONFLLOJRRESHTI INNER JOIN dbo.T_KUSHTEMPLATE ON dbo.T_KONFLLOJRRESHTI.IDKUSHTEMPLATE = dbo.T_KUSHTEMPLATE.IDKUSHTEMPLATE INNER JOIN dbo.T_KONFIGAMBJENTE ON dbo.T_KUSHTEMPLATE.IDKONFIGAMBJENTE = dbo.T_KONFIGAMBJENTE.IDKONFIGAMBJENTE WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_KONTAKTIKLIENTFURNITOR", "SELECT  T_KONTAKTIKLIENTFURNITOR.* FROM  T_KONTAKTIKLIENTFURNITOR INNER JOIN dbo.T_KLIENTFURNITOR ON   dbo.T_KONTAKTIKLIENTFURNITOR.IDKLIENTFURNITOR = dbo.T_KLIENTFURNITOR.IDKLIENTFURNITOR WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_KONVERTIMI", "SELECT T_KONVERTIMI.* FROM dbo.T_KONVERTIMI INNER JOIN dbo.T_KONFIGAMBJENTE ON dbo.T_KONVERTIMI.IDKONFIGAMBJENTEKONVERTUAR = dbo.T_KONFIGAMBJENTE.IDKONFIGAMBJENTE INNER JOIN dbo.T_KONFIGAMBJENTE k2 ON k2.IDKONFIGAMBJENTE=dbo.T_KONVERTIMI.IDKONFIGAMBJENTEPASKONVERTIMI WHERE dbo.T_KONFIGAMBJENTE.IDKATDOK IN (1,2) AND dbo.T_KONFIGAMBJENTE.IDNDERMARJE  in (" + inidnderm + ")  AND k2.IDKATDOK IN (1,2) AND k2.IDNDERMARJE  in (" + inidnderm + ")", ds, ukrijua);// dokumentat e kategorise shitje/blerje te konvertuara ne dokumenta te kategorise shitje /blerje
            ukrijua = krijodt("T_KONVERTIMI2", "SELECT T_KONVERTIMI.* FROM dbo.T_KONVERTIMI INNER JOIN dbo.T_KONFIGAMBJENTE ON dbo.T_KONVERTIMI.IDKONFIGAMBJENTEKONVERTUAR = dbo.T_KONFIGAMBJENTE.IDKONFIGAMBJENTE INNER JOIN dbo.T_KONFIGAMBJENTE k2 ON k2.IDKONFIGAMBJENTE=dbo.T_KONVERTIMI.IDKONFIGAMBJENTEPASKONVERTIMI WHERE dbo.T_KONFIGAMBJENTE.IDKATDOK IN (1,2) AND dbo.T_KONFIGAMBJENTE.IDNDERMARJE  in (" + inidnderm + ")  AND k2.IDKATDOK IN (6) AND k2.IDNDERMARJE  in (" + inidnderm + ")", ds, ukrijua);// dokumentat e kategorise shitje/blerje te konvertuara ne dokumenta te kategorise magazine
            ukrijua = krijodt("T_KONVERTIMI3", "SELECT T_KONVERTIMI.* FROM dbo.T_KONVERTIMI INNER JOIN dbo.T_KONFIGAMBJENTE ON dbo.T_KONVERTIMI.IDKONFIGAMBJENTEKONVERTUAR = dbo.T_KONFIGAMBJENTE.IDKONFIGAMBJENTE INNER JOIN dbo.T_KONFIGAMBJENTE k2 ON k2.IDKONFIGAMBJENTE=dbo.T_KONVERTIMI.IDKONFIGAMBJENTEPASKONVERTIMI WHERE dbo.T_KONFIGAMBJENTE.IDKATDOK IN (6) AND dbo.T_KONFIGAMBJENTE.IDNDERMARJE  in (" + inidnderm + ")  AND k2.IDKATDOK IN (1,2) AND k2.IDNDERMARJE  in (" + inidnderm + ")", ds, ukrijua);// dokumentat e kategorise magazine te konvertuara ne dokumenta te kategorise shitje /blerje
            ukrijua = krijodt("T_KONVERTIMI4", "SELECT T_KONVERTIMI.* FROM dbo.T_KONVERTIMI INNER JOIN dbo.T_KONFIGAMBJENTE ON dbo.T_KONVERTIMI.IDKONFIGAMBJENTEKONVERTUAR = dbo.T_KONFIGAMBJENTE.IDKONFIGAMBJENTE INNER JOIN dbo.T_KONFIGAMBJENTE k2 ON k2.IDKONFIGAMBJENTE=dbo.T_KONVERTIMI.IDKONFIGAMBJENTEPASKONVERTIMI WHERE dbo.T_KONFIGAMBJENTE.IDKATDOK IN (6) AND dbo.T_KONFIGAMBJENTE.IDNDERMARJE  in (" + inidnderm + ")  AND k2.IDKATDOK IN (6) AND k2.IDNDERMARJE  in (" + inidnderm + ")", ds, ukrijua);// dokumentat e kategorise magazine te konvertuara ne dokumenta te kategorise magazine /// shenim ne te ardhmen duhen shtuar dhe rastet e tjera te konvertimeve nqs do kete por per momentin jane vetem shitje/blerje dhe magazine
            ukrijua = krijodt("T_KPF", "SELECT * FROM  T_KPF WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_KURSET", " SELECT  T_KURSET.* FROM  T_KURSET INNER JOIN dbo.T_MONEDHA ON    dbo.T_KURSET.IDMONEDHA = dbo.T_MONEDHA.IDMONEDHA WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_KUSHTEDERGIMI", "SELECT * FROM  T_KUSHTEDERGIMI WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_KUSHTEMPLATE", "SELECT  T_KUSHTEMPLATE.* FROM  dbo.T_KUSHTEMPLATE INNER JOIN dbo.T_KONFIGAMBJENTE ON dbo.T_KUSHTEMPLATE.IDKONFIGAMBJENTE = dbo.T_KONFIGAMBJENTE.IDKONFIGAMBJENTE WHERE IDKATDOK=5 AND IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_KUSHTEMPLATE2", "SELECT  T_KUSHTEMPLATE.* FROM  dbo.T_KUSHTEMPLATE INNER JOIN dbo.T_KONFIGAMBJENTE ON dbo.T_KUSHTEMPLATE.IDKONFIGAMBJENTE = dbo.T_KONFIGAMBJENTE.IDKONFIGAMBJENTE WHERE IDKATDOK<>5 AND IDNDERMARJE in (" + inidnderm + ") AND (idkusht not  IN (25,26,34,35) or vlera=0  or (SELECT IDNDERMARJE FROM dbo.T_KONFIGAMBJENTE WHERE IDKONFIGAMBJENTE=vlera)=-1)", ds, ukrijua);//kushtet template te marra nga alternativa kushte dhe me konfigurime nga ndermarja default
            ukrijua = krijodt("T_KUSHTEMPLATE3", "SELECT  T_KUSHTEMPLATE.* FROM  dbo.T_KUSHTEMPLATE INNER JOIN dbo.T_KONFIGAMBJENTE ON dbo.T_KUSHTEMPLATE.IDKONFIGAMBJENTE = dbo.T_KONFIGAMBJENTE.IDKONFIGAMBJENTE WHERE IDKATDOK<>5 AND IDNDERMARJE in (" + inidnderm + ") AND (idkusht   IN (25,26,34) and vlera!=0 ) AND (SELECT IDNDERMARJE FROM dbo.T_KONFIGAMBJENTE WHERE IDKONFIGAMBJENTE=vlera and idkatdok=5)in(" + inidnderm + ")", ds, ukrijua);//kushte template me konfigurime nga ndermarja te fletes kontabel
            ukrijua = krijodt("T_KUSHTEMPLATE4", "SELECT  T_KUSHTEMPLATE.* FROM  dbo.T_KUSHTEMPLATE INNER JOIN dbo.T_KONFIGAMBJENTE ON dbo.T_KUSHTEMPLATE.IDKONFIGAMBJENTE = dbo.T_KONFIGAMBJENTE.IDKONFIGAMBJENTE WHERE IDKATDOK<>5 AND IDNDERMARJE in (" + inidnderm + ") AND (idkusht   IN (25,26,34) and vlera!=0 ) AND (SELECT IDNDERMARJE FROM dbo.T_KONFIGAMBJENTE WHERE IDKONFIGAMBJENTE=vlera and idkatdok<>5)in(" + inidnderm + ")", ds, ukrijua);//kushte template me konfigurime nga ndermarja te ndryshme nga fleta kontabel
            ukrijua = krijodt("T_KUSHTEMPLATE5", "SELECT  T_KUSHTEMPLATE.* FROM  dbo.T_KUSHTEMPLATE INNER JOIN dbo.T_KONFIGAMBJENTE ON dbo.T_KUSHTEMPLATE.IDKONFIGAMBJENTE = dbo.T_KONFIGAMBJENTE.IDKONFIGAMBJENTE WHERE IDKATDOK<>5 AND IDNDERMARJE in (" + inidnderm + ") AND (idkusht   IN (35,38) and vlera!=0 ) ", ds, ukrijua);//kushte template me vlera nga info artikulli
            ukrijua = krijodt("T_KUSHTPAGESEKOKA", "SELECT * FROM  T_KUSHTPAGESEKOKA WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_KUSHTPAGESETRUPI", " SELECT  T_KUSHTPAGESETRUPI.* FROM  T_KUSHTPAGESETRUPI INNER JOIN dbo.T_KUSHTPAGESEKOKA ON    dbo.T_KUSHTPAGESETRUPI.IDKOKA = dbo.T_KUSHTPAGESEKOKA.IDKOKA WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_KONFIGURIMEMAIL", "SELECT * FROM  T_KONFIGURIMEMAIL WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            #endregion
            #region L M N
            ukrijua = krijodt("T_LIDHESPERKONTROLL", "SELECT T_LIDHESPERKONTROLL.* FROM T_LIDHESPERKONTROLL INNER JOIN dbo.T_NIVELREGJISTRIMI ON dbo.T_LIDHESPERKONTROLL.IDNIVEL = dbo.T_NIVELREGJISTRIMI.IDNIVEL WHERE IDNDERMARRJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_LIDHJEAUTORIZIM1", "SELECT T_LIDHJEAUTORIZIM.* FROM T_LIDHJEAUTORIZIM INNER JOIN dbo.T_AUTORIZIMKOKA ON dbo.T_LIDHJEAUTORIZIM.IDAUTORIZIMEKOKA = dbo.T_AUTORIZIMKOKA.IDAUTORIZIMEKOKA WHERE idlloji=17 and IDNDERMARJE in (" + inidnderm + ") and idlidhese in (select idkonfigambjente from t_konfigambjente where idkatdok=5)", ds, ukrijua);//konfigurimi i dokumentave te fletes kontabel
            ukrijua = krijodt("T_LIDHJEAUTORIZIM2", "SELECT T_LIDHJEAUTORIZIM.* FROM T_LIDHJEAUTORIZIM INNER JOIN dbo.T_AUTORIZIMKOKA ON dbo.T_LIDHJEAUTORIZIM.IDAUTORIZIMEKOKA = dbo.T_AUTORIZIMKOKA.IDAUTORIZIMEKOKA WHERE idlloji=17 and IDNDERMARJE in (" + inidnderm + ") and idlidhese in (select idkonfigambjente from t_konfigambjente where idkatdok<>5)", ds, ukrijua);//konfigurimi i dokumentave pervec fletes kontabel
            ukrijua = krijodt("T_LLOGARI", "SELECT * FROM  T_LLOGARI WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_LLOGARISHPERNDARJEQK", "SELECT * FROM  T_LLOGARISHPERNDARJEQK WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_LLOGARIATRUPIPASQ", "SELECT T_LLOGARIATRUPIPASQ.* FROM T_LLOGARIATRUPIPASQ INNER JOIN dbo.T_PASQYRAFINANCIARETRUPI ON T_LLOGARIATRUPIPASQ.IDTRUPIPASQFIN=dbo.T_PASQYRAFINANCIARETRUPI.IDPASQFINTRUPI INNER JOIN dbo.T_PASQYRAFINANCIAREKOKA ON  dbo.T_PASQYRAFINANCIARETRUPI.IDPASQFINKOKA = dbo.T_PASQYRAFINANCIAREKOKA.IDPASQFINKOKA WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_LLOGARIKONTABILITETI", "SELECT T_LLOGARIKONTABILITETI.* FROM T_LLOGARIKONTABILITETI INNER JOIN dbo.T_KOKAFLETEKONTABEL ON  dbo.T_LLOGARIKONTABILITETI.IDKOKAFLETEKONTABEL = dbo.T_KOKAFLETEKONTABEL.IDKOKAFLETEKONTABEL WHERE IDNDERMARJE in (" + inidnderm + ") AND IDKATEGORIA =5", ds, ukrijua);
            ukrijua = krijodt("T_LLOGARIKONTABILITETI2", "SELECT T_LLOGARIKONTABILITETI.* FROM dbo.T_LLOGARIKONTABILITETI INNER JOIN dbo.T_KOKAFLETEKONTABEL ON dbo.T_LLOGARIKONTABILITETI.IDKOKAFLETEKONTABEL = dbo.T_KOKAFLETEKONTABEL.IDKOKAFLETEKONTABEL WHERE IDNDERMARJE in (" + inidnderm + ") and IDKATEGORIA  in (1,2)", ds, ukrijua);
            ukrijua = krijodt("T_LLOGARIKONTABILITETI3", "SELECT T_LLOGARIKONTABILITETI.* FROM dbo.T_LLOGARIKONTABILITETI INNER JOIN dbo.T_KOKAFLETEKONTABEL ON dbo.T_LLOGARIKONTABILITETI.IDKOKAFLETEKONTABEL = dbo.T_KOKAFLETEKONTABEL.IDKOKAFLETEKONTABEL WHERE IDNDERMARJE in (" + inidnderm + ") and IDKATEGORIA in (3,4)", ds, ukrijua);
            ukrijua = krijodt("T_LLOGARIKONTABILITETI4", "SELECT T_LLOGARIKONTABILITETI.* FROM dbo.T_LLOGARIKONTABILITETI INNER JOIN dbo.T_KOKAFLETEKONTABEL ON dbo.T_LLOGARIKONTABILITETI.IDKOKAFLETEKONTABEL = dbo.T_KOKAFLETEKONTABEL.IDKOKAFLETEKONTABEL WHERE IDNDERMARJE in (" + inidnderm + ") and IDKATEGORIA =6 and idgjenerues in (SELECT idkokamagazina FROM dbo.T_KOKAMAGAZINA WHERE IDNDERM in (" + inidnderm + ")  AND (IDNIVELGJENERUES IS NULL OR IDNIVELGJENERUES IN (SELECT IDNIVEL FROM dbo.T_NIVELREGJISTRIMI WHERE IDKATDOK=6)))", ds, ukrijua);
            ukrijua = krijodt("T_LLOGARIKONTABILITETI5", "SELECT T_LLOGARIKONTABILITETI.* FROM dbo.T_LLOGARIKONTABILITETI INNER JOIN dbo.T_KOKAFLETEKONTABEL ON dbo.T_LLOGARIKONTABILITETI.IDKOKAFLETEKONTABEL = dbo.T_KOKAFLETEKONTABEL.IDKOKAFLETEKONTABEL WHERE IDNDERMARJE in (" + inidnderm + ") and IDKATEGORIA =7", ds, ukrijua);
            ukrijua = krijodt("T_LLOGARIKONTABILITETI6", "SELECT T_LLOGARIKONTABILITETI.* FROM dbo.T_LLOGARIKONTABILITETI INNER JOIN dbo.T_KOKAFLETEKONTABEL ON dbo.T_LLOGARIKONTABILITETI.IDKOKAFLETEKONTABEL = dbo.T_KOKAFLETEKONTABEL.IDKOKAFLETEKONTABEL WHERE IDNDERMARJE in (" + inidnderm + ") and IDKATEGORIA =8", ds, ukrijua);
            ukrijua = krijodt("T_LLOGARIKONTABILITETI7", "SELECT T_LLOGARIKONTABILITETI.* FROM dbo.T_LLOGARIKONTABILITETI INNER JOIN dbo.T_KOKAFLETEKONTABEL ON dbo.T_LLOGARIKONTABILITETI.IDKOKAFLETEKONTABEL = dbo.T_KOKAFLETEKONTABEL.IDKOKAFLETEKONTABEL WHERE IDNDERMARJE in (" + inidnderm + ") and IDKATEGORIA =10 and idgjenerues in (SELECT idkoka FROM dbo.T_DOKUMENTLIDHESKOKA WHERE IDLLOJDOK=10 and  IDNDERMARJE in (" + inidnderm + ") )", ds, ukrijua);
            ukrijua = krijodt("T_LLOGARIKONTABILITETI8", "SELECT T_LLOGARIKONTABILITETI.* FROM dbo.T_LLOGARIKONTABILITETI INNER JOIN dbo.T_KOKAFLETEKONTABEL ON dbo.T_LLOGARIKONTABILITETI.IDKOKAFLETEKONTABEL = dbo.T_KOKAFLETEKONTABEL.IDKOKAFLETEKONTABEL WHERE IDNDERMARJE in (" + inidnderm + ") and IDKATEGORIA =11", ds, ukrijua);
            ukrijua = krijodt("T_LLOGARIKONTABILITETI9", "SELECT T_LLOGARIKONTABILITETI.* FROM dbo.T_LLOGARIKONTABILITETI INNER JOIN dbo.T_KOKAFLETEKONTABEL ON dbo.T_LLOGARIKONTABILITETI.IDKOKAFLETEKONTABEL = dbo.T_KOKAFLETEKONTABEL.IDKOKAFLETEKONTABEL WHERE IDNDERMARJE in (" + inidnderm + ") and IDKATEGORIA =20", ds, ukrijua);
            ukrijua = krijodt("T_LLOGARIKONTABILITETI10", "SELECT T_LLOGARIKONTABILITETI.* FROM dbo.T_LLOGARIKONTABILITETI INNER JOIN dbo.T_KOKAFLETEKONTABEL ON dbo.T_LLOGARIKONTABILITETI.IDKOKAFLETEKONTABEL = dbo.T_KOKAFLETEKONTABEL.IDKOKAFLETEKONTABEL WHERE IDNDERMARJE in (" + inidnderm + ") and IDKATEGORIA =6 and idgjenerues in (SELECT idkokamagazina FROM dbo.T_KOKAMAGAZINA WHERE IDNDERM in (" + inidnderm + ")  AND ( IDNIVELGJENERUES IN (SELECT IDNIVEL FROM dbo.T_NIVELREGJISTRIMI WHERE IDKATDOK in (1,2))))", ds, ukrijua);
            ukrijua = krijodt("T_LLOGARIKONTABILITETI11", "SELECT T_LLOGARIKONTABILITETI.* FROM dbo.T_LLOGARIKONTABILITETI INNER JOIN dbo.T_KOKAFLETEKONTABEL ON dbo.T_LLOGARIKONTABILITETI.IDKOKAFLETEKONTABEL = dbo.T_KOKAFLETEKONTABEL.IDKOKAFLETEKONTABEL WHERE IDNDERMARJE in (" + inidnderm + ") and IDKATEGORIA =6 and idgjenerues in (SELECT idkokamagazina FROM dbo.T_KOKAMAGAZINA WHERE IDNDERM in (" + inidnderm + ")  AND (IDNIVELGJENERUES IN (SELECT IDNIVEL FROM dbo.T_NIVELREGJISTRIMI WHERE IDKATDOK=7)))", ds, ukrijua);
            ukrijua = krijodt("T_LLOGARIKONTABILITETI12", "SELECT T_LLOGARIKONTABILITETI.* FROM dbo.T_LLOGARIKONTABILITETI INNER JOIN dbo.T_KOKAFLETEKONTABEL ON dbo.T_LLOGARIKONTABILITETI.IDKOKAFLETEKONTABEL = dbo.T_KOKAFLETEKONTABEL.IDKOKAFLETEKONTABEL WHERE IDNDERMARJE in (" + inidnderm + ") and IDKATEGORIA =10 and idgjenerues in (SELECT idkoka FROM dbo.T_DOKUMENTLIDHESKOKA WHERE IDLLOJDOK in (3,4) and  IDNDERMARJE in (" + inidnderm + "))", ds, ukrijua);
            ukrijua = krijodt("T_LLOGARIKONTABILITETI13", "SELECT T_LLOGARIKONTABILITETI.* FROM dbo.T_LLOGARIKONTABILITETI INNER JOIN dbo.T_KOKAFLETEKONTABEL ON dbo.T_LLOGARIKONTABILITETI.IDKOKAFLETEKONTABEL = dbo.T_KOKAFLETEKONTABEL.IDKOKAFLETEKONTABEL WHERE IDNDERMARJE in (" + inidnderm + ") and IDKATEGORIA =10 and idgjenerues in (SELECT idkoka FROM dbo.T_DOKUMENTLIDHESKOKA WHERE IDLLOJDOK=20 and  IDNDERMARJE in (" + inidnderm + ") )", ds, ukrijua);
            ukrijua = krijodt("T_LLOGARIKONTABILITETI14", "SELECT T_LLOGARIKONTABILITETI.* FROM dbo.T_LLOGARIKONTABILITETI INNER JOIN dbo.T_KOKAFLETEKONTABEL ON dbo.T_LLOGARIKONTABILITETI.IDKOKAFLETEKONTABEL = dbo.T_KOKAFLETEKONTABEL.IDKOKAFLETEKONTABEL WHERE IDNDERMARJE in (" + inidnderm + ") and IDKATEGORIA =38 ", ds, ukrijua);
            ukrijua = krijodt("T_LLOGARIKONTABILITETI15", "SELECT T_LLOGARIKONTABILITETI.* FROM dbo.T_LLOGARIKONTABILITETI INNER JOIN dbo.T_KOKAFLETEKONTABEL ON dbo.T_LLOGARIKONTABILITETI.IDKOKAFLETEKONTABEL = dbo.T_KOKAFLETEKONTABEL.IDKOKAFLETEKONTABEL WHERE IDNDERMARJE in (" + inidnderm + ") and IDKATEGORIA =45 ", ds, ukrijua);
            ukrijua = krijodt("T_LLOGARIKONTABILITETI16", "SELECT T_LLOGARIKONTABILITETI.* FROM dbo.T_LLOGARIKONTABILITETI INNER JOIN dbo.T_KOKAFLETEKONTABEL ON dbo.T_LLOGARIKONTABILITETI.IDKOKAFLETEKONTABEL = dbo.T_KOKAFLETEKONTABEL.IDKOKAFLETEKONTABEL WHERE IDNDERMARJE in (" + inidnderm + ") and IDKATEGORIA =64 ", ds, ukrijua);
            ukrijua = krijodt("T_LUPAMULTIPLE", " SELECT T_LUPAMULTIPLE.* FROM T_LUPAMULTIPLE INNER JOIN  T_GRIDATRUPI ON dbo.T_LUPAMULTIPLE.GRIDATRUPIID = dbo.T_GRIDATRUPI.GRIDATRUPIID INNER JOIN  T_GRIDAKOKA ON dbo.T_GRIDATRUPI.GRIDAKOKAID = dbo.T_GRIDAKOKA.GRIDKOKAID INNER JOIN dbo.T_KONFIGAMBJENTE  ON dbo.T_GRIDAKOKA.IDKONFIGURIM = dbo.T_KONFIGAMBJENTE.IDKONFIGAMBJENTE WHERE   IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_MATURIMI", "SELECT * FROM  T_MATURIMI WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_MENYRATRANSPORTI", "SELECT * FROM  T_MENYRATRANSPORTI WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_MODELIFUSHASHTESE", "SELECT * FROM  T_MODELIFUSHASHTESE WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_MONEDHA", "SELECT * FROM  T_MONEDHA WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_NDERMARJE", "SELECT * FROM  T_NDERMARJE WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_NDERMARJEVITI", "SELECT * FROM dbo.T_NDERMARJEVITI WHERE IDNDERMARJE in (" + inidnderm + ")  ", ds, ukrijua);
            ukrijua = krijodt("T_NENGRUPILLOGARIA", "SELECT * FROM   T_NENGRUPILLOGARIA WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_NIVELCMIMI", "SELECT * FROM  T_NIVELCMIMI WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_NIVELREGJISTRIMI", "SELECT * FROM   T_NIVELREGJISTRIMI WHERE IDNDERMARRJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_NIVELREGJKONVERTO", "SELECT T_NIVELREGJKONVERTO.* FROM T_NIVELREGJKONVERTO INNER JOIN dbo.T_NIVELREGJISTRIMI ON dbo.T_NIVELREGJKONVERTO.IDNIVEL = dbo.T_NIVELREGJISTRIMI.IDNIVEL WHERE IDNDERMARRJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_NIVELZBRITJE", "SELECT * FROM  T_NIVELZBRITJE WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_NJESIADMINISTRATIVE", "SELECT * FROM  T_NJESIADMINISTRATIVE WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_NJESIVARTESE", "SELECT * FROM  T_NJESIVARTESE WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_NJESIARTIKULLI", "SELECT * FROM  T_NJESIARTIKULLI WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_NRAUTOM", "SELECT * FROM  T_NRAUTOM WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_NRAUTOMATIKFUNDIT", "SELECT * FROM  T_NRAUTOMATIKFUNDIT WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            #endregion
            #region O P Q R S
            ukrijua = krijodt("T_OBJEKTIVAKOSTO", "SELECT * FROM  T_OBJEKTIVAKOSTO WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);

            ukrijua = krijodt("T_PAGASHTESA", "SELECT T_PAGASHTESA.* FROM T_PAGASHTESA INNER JOIN dbo.T_PUNONJES ON  dbo.T_PAGASHTESA.IDPUNONJES = dbo.T_PUNONJES.IDPUNONJES    WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_PASQYRAFINANCIAREKOKA", "SELECT * FROM  T_PASQYRAFINANCIAREKOKA WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_PASQYRAFINANCIARETRUPI", "SELECT T_PASQYRAFINANCIARETRUPI.* FROM T_PASQYRAFINANCIARETRUPI INNER JOIN dbo.T_PASQYRAFINANCIAREKOKA ON  dbo.T_PASQYRAFINANCIARETRUPI.IDPASQFINKOKA = dbo.T_PASQYRAFINANCIAREKOKA.IDPASQFINKOKA    WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_PERDORUESI", "SELECT T_PERDORUESI.* FROM T_PERDORUESI WHERE IDPERDORUES IN (SELECT IDPERDORUES FROM dbo.T_ROLPERDORUES WHERE IDROLI IN (SELECT IDROLI FROM dbo.T_ROLI WHERE IDROLI IN (SELECT idroli FROM dbo.T_ROLDREJTA WHERE IDNDERMARRJE in (" + inidnderm + ")) AND IDLICENCA IN (SELECT IDLICENCA FROM dbo.T_NDERMARJE WHERE IDNDERMARJE in (" + inidnderm + "))))", ds, ukrijua);
            ukrijua = krijodt("T_PERIUDHAT", "SELECT T_PERIUDHAT.* FROM T_PERIUDHAT INNER JOIN dbo.T_VITET ON    dbo.T_PERIUDHAT.IDVITI = dbo.T_VITET.IDVITI   WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_PIKESHITJEFURNIZIMI", "SELECT * FROM  T_PIKESHITJEFURNIZIMI WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_PLANIFIKIM_EKZEKUTIM", "SELECT T_PLANIFIKIM_EKZEKUTIM.* FROM dbo.T_PLANIFIKIM_EKZEKUTIM INNER JOIN dbo.T_KONFIGAMBJENTE ON dbo.T_PLANIFIKIM_EKZEKUTIM.IDKONFIGEKZEKUTIMI = dbo.T_KONFIGAMBJENTE.IDKONFIGAMBJENTE WHERE  dbo.T_KONFIGAMBJENTE.IDNDERMARJE  in (" + inidnderm + ")  ", ds, ukrijua);
            ukrijua = krijodt("T_PUNESIM", "SELECT T_PUNESIM.* FROM T_PUNESIM INNER JOIN dbo.T_PUNONJES ON  dbo.T_PUNESIM.IDPUNONJES = dbo.T_PUNONJES.IDPUNONJES    WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_PRODUKTPRODHIMI", "SELECT T_PRODUKTPRODHIMI.* FROM T_PRODUKTPRODHIMI INNER JOIN dbo.T_KOKAEKZEKUTIMPRODHIMI ON  dbo.T_PRODUKTPRODHIMI.idkoka = dbo.T_KOKAEKZEKUTIMPRODHIMI.idkoka    WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_PUNONJES", "SELECT * FROM  T_PUNONJES WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_QYTETI", "SELECT * FROM  T_QYTETI WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_QENDRAKOSTO", "SELECT * FROM  T_QENDRAKOSTO WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_RAPFILTER", "SELECT T_RAPFILTER.* FROM T_RAPFILTER INNER JOIN dbo.T_FILTERKOKA ON     dbo.T_RAPFILTER.IDFILTERKOKA = dbo.T_FILTERKOKA.IDKOKAFILTER   WHERE IDNDERMARRJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_RAPXDESIGN", "SELECT * FROM  T_RAPXDESIGN WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_RECEPTURAPRODHIMI", "SELECT T_RECEPTURAPRODHIMI.* FROM T_RECEPTURAPRODHIMI inner join  T_PRODUKTPRODHIMI on T_RECEPTURAPRODHIMI.idprodukti=T_PRODUKTPRODHIMI.id INNER JOIN dbo.T_KOKAEKZEKUTIMPRODHIMI ON  dbo.T_PRODUKTPRODHIMI.idkoka = dbo.T_KOKAEKZEKUTIMPRODHIMI.idkoka    WHERE idartikulli is not null IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_RECEPTURAPRODHIMI2", "SELECT T_RECEPTURAPRODHIMI.* FROM T_RECEPTURAPRODHIMI inner join  T_PRODUKTPRODHIMI on T_RECEPTURAPRODHIMI.idprodukti=T_PRODUKTPRODHIMI.id INNER JOIN dbo.T_KOKAEKZEKUTIMPRODHIMI ON  dbo.T_PRODUKTPRODHIMI.idkoka = dbo.T_KOKAEKZEKUTIMPRODHIMI.idkoka    WHERE idburimi is not null IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_ROLDREJTA", "SELECT * FROM  T_ROLDREJTA WHERE IDNDERMARRJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_ROLI", "SELECT * FROM  T_ROLI  WHERE IDROLI IN (SELECT idroli FROM dbo.T_ROLDREJTA WHERE IDNDERMARRJE in (" + inidnderm + ")) ", ds, ukrijua);
            ukrijua = krijodt("T_ROLPERDORUES", "SELECT * FROM  T_ROLPERDORUES WHERE IDROLI IN (SELECT IDROLI FROM dbo.T_ROLI  WHERE IDROLI IN (SELECT idroli FROM dbo.T_ROLDREJTA WHERE IDNDERMARRJE in (" + inidnderm + ")) AND IDLICENCA in (SELECT IDLICENCA FROM dbo.T_NDERMARJE WHERE IDNDERMARJE in (" + inidnderm + "))) ", ds, ukrijua);
            ukrijua = krijodt("T_SHPERNDARJESHPENZIMEFATURAT", "SELECT T_SHPERNDARJESHPENZIMEFATURAT.* FROM T_SHPERNDARJESHPENZIMEFATURAT INNER JOIN dbo.T_SHPERNDARJESHPENZIMEKOKA ON  dbo.T_SHPERNDARJESHPENZIMEFATURAT.IDSHPERNDARJESHPENZKOKA = dbo.T_SHPERNDARJESHPENZIMEKOKA.IDSHPERNDARJESHPENZ WHERE IDNDERM in (" + inidnderm + ") ", ds, ukrijua);// perdoren vetem dokumentat e magazines te gjeneruar nga shitja/blerja;
            ukrijua = krijodt("T_SHPERNDARJESHPENZIMEKOKA", "SELECT * FROM dbo.T_SHPERNDARJESHPENZIMEKOKA WHERE IDNDERM in (" + inidnderm + ") ", ds, ukrijua);
            ukrijua = krijodt("T_SHPERNDARJESHPENZIMETRUPI", "SELECT T_SHPERNDARJESHPENZIMETRUPI.* FROM T_SHPERNDARJESHPENZIMETRUPI INNER JOIN dbo.T_SHPERNDARJESHPENZIMEKOKA ON  dbo.T_SHPERNDARJESHPENZIMETRUPI.IDSHPERNDARJESHPENZKOKA = dbo.T_SHPERNDARJESHPENZIMEKOKA.IDSHPERNDARJESHPENZ WHERE IDNDERM in (" + inidnderm + ") ", ds, ukrijua);
            ukrijua = krijodt("T_SHPERNDARJESHPENZIMETRUPIFATURAT", "SELECT T_SHPERNDARJESHPENZIMETRUPIFATURAT.* FROM T_SHPERNDARJESHPENZIMETRUPIFATURAT INNER JOIN  T_SHPERNDARJESHPENZIMETRUPI  ON dbo.T_SHPERNDARJESHPENZIMETRUPIFATURAT.IDSHPERNDARJESHPENZ = dbo.T_SHPERNDARJESHPENZIMETRUPI.IDSHPERNDARJESHPENZTRUPI INNER JOIN dbo.T_SHPERNDARJESHPENZIMEKOKA ON  dbo.T_SHPERNDARJESHPENZIMETRUPI.IDSHPERNDARJESHPENZKOKA = dbo.T_SHPERNDARJESHPENZIMEKOKA.IDSHPERNDARJESHPENZ WHERE IDNDERM in (" + inidnderm + ")  AND IDTRUPISHITJE IN (SELECT idtrupimagazina FROM dbo.T_TRUPIMAGAZINA WHERE IDLLOJVEPRIMI=1)", ds, ukrijua);
            ukrijua = krijodt("T_SHPERNDARJESHPENZIMETRUPIFATURAT2", "SELECT T_SHPERNDARJESHPENZIMETRUPIFATURAT.* FROM T_SHPERNDARJESHPENZIMETRUPIFATURAT INNER JOIN  T_SHPERNDARJESHPENZIMETRUPI  ON dbo.T_SHPERNDARJESHPENZIMETRUPIFATURAT.IDSHPERNDARJESHPENZ = dbo.T_SHPERNDARJESHPENZIMETRUPI.IDSHPERNDARJESHPENZTRUPI INNER JOIN dbo.T_SHPERNDARJESHPENZIMEKOKA ON  dbo.T_SHPERNDARJESHPENZIMETRUPI.IDSHPERNDARJESHPENZKOKA = dbo.T_SHPERNDARJESHPENZIMEKOKA.IDSHPERNDARJESHPENZ WHERE IDNDERM in (" + inidnderm + ")  AND IDTRUPISHITJE IN (SELECT idtrupimagazina FROM dbo.T_TRUPIMAGAZINA WHERE IDLLOJVEPRIMI=2)", ds, ukrijua);
            ukrijua = krijodt("T_SHPERNDARJESHPENZIMELLOGARITE", "SELECT  T_SHPERNDARJESHPENZIMELLOGARITE.* FROM dbo. T_SHPERNDARJESHPENZIMELLOGARITE INNER JOIN dbo.T_SHPERNDARJESHPENZIMEKOKA ON    dbo.T_SHPERNDARJESHPENZIMELLOGARITE.IDSHPERNDARJESHPEZKOKA = dbo.T_SHPERNDARJESHPENZIMEKOKA.IDSHPERNDARJESHPENZ WHERE IDNDERM in (" + inidnderm + ") ", ds, ukrijua);
            ukrijua = krijodt("T_SHTESAPAGE", "SELECT * FROM  T_SHTESAPAGE WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_SIGURIME", "SELECT * FROM dbo.T_SIGURIME WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_SIGURIMESUPLEMENTARE", "SELECT * FROM dbo.T_SIGURIMESUPLEMENTARE WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_SKEMAKONTABILITETIARTIKULLI", "SELECT * FROM  T_SKEMAKONTABILITETIARTIKULLI WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_SKEMASIGURIMI", "SELECT T_SKEMASIGURIMI.* FROM T_SKEMASIGURIMI INNER JOIN dbo.T_PUNONJES ON  dbo.T_SKEMASIGURIMI.IDPUNONJES = dbo.T_PUNONJES.IDPUNONJES    WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_STRUKTURAADMINISTRATIVE", "SELECT * FROM  T_STRUKTURAADMINISTRATIVE WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            #endregion
            #region T U V X Y Z
            ukrijua = krijodt("T_TAKSAT", "SELECT * FROM  T_TAKSAT WHERE IDNDERM in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_TATIME", "SELECT * FROM  T_TATIME WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_THEMEUSER", "SELECT * FROM  T_THEMEUSER WHERE IDNDERMARRJE in (" + inidnderm + ")", ds, ukrijua);
            //    ukrijua = krijodt("T_THEMESAMBJENTE", "SELECT * FROM  T_THEMESAMBJENTE WHERE IDNDERMARRJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_TIPKONTRATE", "SELECT * FROM  T_TIPKONTRATE WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_TRUPIFLETEKONTABEL", "SELECT T_TRUPIFLETEKONTABEL.* FROM dbo.T_TRUPIFLETEKONTABEL INNER JOIN dbo.T_KOKAFLETEKONTABEL ON dbo.T_TRUPIFLETEKONTABEL.IDKOKAFLETEKONTABEL = dbo.T_KOKAFLETEKONTABEL.IDKOKAFLETEKONTABEL WHERE IDNDERMARJE in (" + inidnderm + ") and IDKATEGORIA =5", ds, ukrijua);
            ukrijua = krijodt("T_TRUPIFLETEKONTABEL2", "SELECT T_TRUPIFLETEKONTABEL.* FROM dbo.T_TRUPIFLETEKONTABEL INNER JOIN dbo.T_KOKAFLETEKONTABEL ON dbo.T_TRUPIFLETEKONTABEL.IDKOKAFLETEKONTABEL = dbo.T_KOKAFLETEKONTABEL.IDKOKAFLETEKONTABEL WHERE IDNDERMARJE in (" + inidnderm + ") and IDKATEGORIA  in (1,2)", ds, ukrijua);
            ukrijua = krijodt("T_TRUPIFLETEKONTABEL3", "SELECT T_TRUPIFLETEKONTABEL.* FROM dbo.T_TRUPIFLETEKONTABEL INNER JOIN dbo.T_KOKAFLETEKONTABEL ON dbo.T_TRUPIFLETEKONTABEL.IDKOKAFLETEKONTABEL = dbo.T_KOKAFLETEKONTABEL.IDKOKAFLETEKONTABEL WHERE IDNDERMARJE in (" + inidnderm + ") and IDKATEGORIA in (3,4)", ds, ukrijua);
            ukrijua = krijodt("T_TRUPIFLETEKONTABEL4", "SELECT T_TRUPIFLETEKONTABEL.* FROM dbo.T_TRUPIFLETEKONTABEL INNER JOIN dbo.T_KOKAFLETEKONTABEL ON dbo.T_TRUPIFLETEKONTABEL.IDKOKAFLETEKONTABEL = dbo.T_KOKAFLETEKONTABEL.IDKOKAFLETEKONTABEL WHERE IDNDERMARJE in (" + inidnderm + ") and IDKATEGORIA =6 and idgjenerues in (SELECT idkokamagazina FROM dbo.T_KOKAMAGAZINA WHERE IDNDERM in (" + inidnderm + ")  AND (IDNIVELGJENERUES IS NULL OR IDNIVELGJENERUES IN (SELECT IDNIVEL FROM dbo.T_NIVELREGJISTRIMI WHERE IDKATDOK=6)))", ds, ukrijua);
            ukrijua = krijodt("T_TRUPIFLETEKONTABEL5", "SELECT T_TRUPIFLETEKONTABEL.* FROM dbo.T_TRUPIFLETEKONTABEL INNER JOIN dbo.T_KOKAFLETEKONTABEL ON dbo.T_TRUPIFLETEKONTABEL.IDKOKAFLETEKONTABEL = dbo.T_KOKAFLETEKONTABEL.IDKOKAFLETEKONTABEL WHERE IDNDERMARJE in (" + inidnderm + ") and IDKATEGORIA =7", ds, ukrijua);
            ukrijua = krijodt("T_TRUPIFLETEKONTABEL6", "SELECT T_TRUPIFLETEKONTABEL.* FROM dbo.T_TRUPIFLETEKONTABEL INNER JOIN dbo.T_KOKAFLETEKONTABEL ON dbo.T_TRUPIFLETEKONTABEL.IDKOKAFLETEKONTABEL = dbo.T_KOKAFLETEKONTABEL.IDKOKAFLETEKONTABEL WHERE IDNDERMARJE in (" + inidnderm + ") and IDKATEGORIA =8", ds, ukrijua);
            ukrijua = krijodt("T_TRUPIFLETEKONTABEL7", "SELECT T_TRUPIFLETEKONTABEL.* FROM dbo.T_TRUPIFLETEKONTABEL INNER JOIN dbo.T_KOKAFLETEKONTABEL ON dbo.T_TRUPIFLETEKONTABEL.IDKOKAFLETEKONTABEL = dbo.T_KOKAFLETEKONTABEL.IDKOKAFLETEKONTABEL WHERE IDNDERMARJE in (" + inidnderm + ") and IDKATEGORIA =10 and idgjenerues in (SELECT idkoka FROM dbo.T_DOKUMENTLIDHESKOKA WHERE IDLLOJDOK=10 and  IDNDERMARJE in (" + inidnderm + ") )", ds, ukrijua);
            ukrijua = krijodt("T_TRUPIFLETEKONTABEL8", "SELECT T_TRUPIFLETEKONTABEL.* FROM dbo.T_TRUPIFLETEKONTABEL INNER JOIN dbo.T_KOKAFLETEKONTABEL ON dbo.T_TRUPIFLETEKONTABEL.IDKOKAFLETEKONTABEL = dbo.T_KOKAFLETEKONTABEL.IDKOKAFLETEKONTABEL WHERE IDNDERMARJE in (" + inidnderm + ") and IDKATEGORIA =11", ds, ukrijua);
            ukrijua = krijodt("T_TRUPIFLETEKONTABEL9", "SELECT T_TRUPIFLETEKONTABEL.* FROM dbo.T_TRUPIFLETEKONTABEL INNER JOIN dbo.T_KOKAFLETEKONTABEL ON dbo.T_TRUPIFLETEKONTABEL.IDKOKAFLETEKONTABEL = dbo.T_KOKAFLETEKONTABEL.IDKOKAFLETEKONTABEL WHERE IDNDERMARJE in (" + inidnderm + ") and IDKATEGORIA =20", ds, ukrijua);
            ukrijua = krijodt("T_TRUPIFLETEKONTABEL10", "SELECT T_TRUPIFLETEKONTABEL.* FROM dbo.T_TRUPIFLETEKONTABEL INNER JOIN dbo.T_KOKAFLETEKONTABEL ON dbo.T_TRUPIFLETEKONTABEL.IDKOKAFLETEKONTABEL = dbo.T_KOKAFLETEKONTABEL.IDKOKAFLETEKONTABEL WHERE IDNDERMARJE in (" + inidnderm + ") and IDKATEGORIA =6 and idgjenerues in (SELECT idkokamagazina FROM dbo.T_KOKAMAGAZINA WHERE IDNDERM in (" + inidnderm + ") AND ( IDNIVELGJENERUES IN (SELECT IDNIVEL FROM dbo.T_NIVELREGJISTRIMI WHERE IDKATDOK in (1,2))))", ds, ukrijua);
            ukrijua = krijodt("T_TRUPIFLETEKONTABEL11", "SELECT T_TRUPIFLETEKONTABEL.* FROM dbo.T_TRUPIFLETEKONTABEL INNER JOIN dbo.T_KOKAFLETEKONTABEL ON dbo.T_TRUPIFLETEKONTABEL.IDKOKAFLETEKONTABEL = dbo.T_KOKAFLETEKONTABEL.IDKOKAFLETEKONTABEL WHERE IDNDERMARJE in (" + inidnderm + ") and IDKATEGORIA =6 and idgjenerues in (SELECT idkokamagazina FROM dbo.T_KOKAMAGAZINA WHERE IDNDERM in (" + inidnderm + ")  AND (IDNIVELGJENERUES IN (SELECT IDNIVEL FROM dbo.T_NIVELREGJISTRIMI WHERE IDKATDOK=7)))", ds, ukrijua);
            ukrijua = krijodt("T_TRUPIFLETEKONTABEL12", "SELECT T_TRUPIFLETEKONTABEL.* FROM dbo.T_TRUPIFLETEKONTABEL INNER JOIN dbo.T_KOKAFLETEKONTABEL ON dbo.T_TRUPIFLETEKONTABEL.IDKOKAFLETEKONTABEL = dbo.T_KOKAFLETEKONTABEL.IDKOKAFLETEKONTABEL WHERE IDNDERMARJE in (" + inidnderm + ") and IDKATEGORIA =10 and idgjenerues in (SELECT idkoka FROM dbo.T_DOKUMENTLIDHESKOKA WHERE IDLLOJDOK in (3,4) and  IDNDERMARJE in (" + inidnderm + ") )", ds, ukrijua);
            ukrijua = krijodt("T_TRUPIFLETEKONTABEL13", "SELECT T_TRUPIFLETEKONTABEL.* FROM dbo.T_TRUPIFLETEKONTABEL INNER JOIN dbo.T_KOKAFLETEKONTABEL ON dbo.T_TRUPIFLETEKONTABEL.IDKOKAFLETEKONTABEL = dbo.T_KOKAFLETEKONTABEL.IDKOKAFLETEKONTABEL WHERE IDNDERMARJE in (" + inidnderm + ") and IDKATEGORIA =10 and idgjenerues in (SELECT idkoka FROM dbo.T_DOKUMENTLIDHESKOKA WHERE IDLLOJDOK=20 and  IDNDERMARJE in (" + inidnderm + "))", ds, ukrijua);
            ukrijua = krijodt("T_TRUPIFLETEKONTABEL14", "SELECT T_TRUPIFLETEKONTABEL.* FROM dbo.T_TRUPIFLETEKONTABEL INNER JOIN dbo.T_KOKAFLETEKONTABEL ON dbo.T_TRUPIFLETEKONTABEL.IDKOKAFLETEKONTABEL = dbo.T_KOKAFLETEKONTABEL.IDKOKAFLETEKONTABEL WHERE IDNDERMARJE in (" + inidnderm + ") and IDKATEGORIA =38", ds, ukrijua);
            ukrijua = krijodt("T_TRUPIFLETEKONTABEL15", "SELECT T_TRUPIFLETEKONTABEL.* FROM dbo.T_TRUPIFLETEKONTABEL INNER JOIN dbo.T_KOKAFLETEKONTABEL ON dbo.T_TRUPIFLETEKONTABEL.IDKOKAFLETEKONTABEL = dbo.T_KOKAFLETEKONTABEL.IDKOKAFLETEKONTABEL WHERE IDNDERMARJE in (" + inidnderm + ") and IDKATEGORIA =45", ds, ukrijua);
            ukrijua = krijodt("T_TRUPIFLETEKONTABEL16", "SELECT T_TRUPIFLETEKONTABEL.* FROM dbo.T_TRUPIFLETEKONTABEL INNER JOIN dbo.T_KOKAFLETEKONTABEL ON dbo.T_TRUPIFLETEKONTABEL.IDKOKAFLETEKONTABEL = dbo.T_KOKAFLETEKONTABEL.IDKOKAFLETEKONTABEL WHERE IDNDERMARJE in (" + inidnderm + ") and IDKATEGORIA =64", ds, ukrijua);
            ukrijua = krijodt("T_TRUPIKATEGORIZBRITJE", "SELECT T_TRUPIKATEGORIZBRITJE.* FROM T_TRUPIKATEGORIZBRITJE INNER JOIN dbo.T_KOKAKATEGORIZBRITJE ON dbo.T_TRUPIKATEGORIZBRITJE.IDKOKAKATEGORIZBRITJE = dbo.T_KOKAKATEGORIZBRITJE.IDKOKAKATEGORIZBRITJE WHERE  IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_TRUPIFORMATIMPORTI", "SELECT T_TRUPIFORMATIMPORTI.* FROM T_TRUPIFORMATIMPORTI INNER JOIN dbo.T_KOKAFORMATIMPORTI ON dbo.T_TRUPIFORMATIMPORTI.IDKOKA = dbo.T_KOKAFORMATIMPORTI.IDKOKA WHERE  IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_TRUPILISTPAGESE", "SELECT  T_TRUPILISTPAGESE.* FROM dbo. T_TRUPILISTPAGESE INNER JOIN dbo.T_KOKALISTPAGESE ON    dbo.T_TRUPILISTPAGESE.IDKOKA = dbo.T_KOKALISTPAGESE.IDKOKA WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_TRUPIMBYLLJEKF", "SELECT  T_TRUPIMBYLLJEKF.* FROM dbo. T_TRUPIMBYLLJEKF INNER JOIN dbo.T_KOKAMBYLLJEKF ON    dbo.T_TRUPIMBYLLJEKF.IDKOKA = dbo.T_KOKAMBYLLJEKF.IDKOKA WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_TRUPIUDHERPAGESA", "SELECT  T_TRUPIUDHERPAGESA.* FROM dbo. T_TRUPIUDHERPAGESA INNER JOIN dbo.T_KOKAURDHERPAGESA ON    dbo.T_TRUPIUDHERPAGESA.IDKOKA = dbo.T_KOKAURDHERPAGESA.IDKOKA WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_TRUPIMAGAZINA", "SELECT  T_TRUPIMAGAZINA.* FROM dbo. T_TRUPIMAGAZINA INNER JOIN dbo. T_KOKAMAGAZINA ON   dbo.T_TRUPIMAGAZINA.IDKOKAMAGAZINA = dbo.T_KOKAMAGAZINA.IDKOKAMAGAZINA WHERE IDLLOJVEPRIMI=1 and IDNDERM in (" + inidnderm + ")  AND (IDNIVELGJENERUES IS NULL OR IDNIVELGJENERUES IN (SELECT IDNIVEL FROM dbo.T_NIVELREGJISTRIMI WHERE IDKATDOK=6))", ds, ukrijua);//magazinat e pagjeneruar dhe te gjeneruar nga transferimi me artikuj
            ukrijua = krijodt("T_TRUPIMAGAZINA2", "SELECT  T_TRUPIMAGAZINA.* FROM dbo. T_TRUPIMAGAZINA INNER JOIN dbo. T_KOKAMAGAZINA ON   dbo.T_TRUPIMAGAZINA.IDKOKAMAGAZINA = dbo.T_KOKAMAGAZINA.IDKOKAMAGAZINA WHERE IDLLOJVEPRIMI=2 and IDNDERM in (" + inidnderm + ") AND (IDNIVELGJENERUES IS NULL OR IDNIVELGJENERUES IN (SELECT IDNIVEL FROM dbo.T_NIVELREGJISTRIMI WHERE IDKATDOK=6))", ds, ukrijua);//magazinat e pagjeneruar dhe te gjeneruar nga transferimi me makro
            ukrijua = krijodt("T_TRUPIMAGAZINA3", "SELECT  T_TRUPIMAGAZINA.* FROM dbo. T_TRUPIMAGAZINA INNER JOIN dbo. T_KOKAMAGAZINA ON   dbo.T_TRUPIMAGAZINA.IDKOKAMAGAZINA = dbo.T_KOKAMAGAZINA.IDKOKAMAGAZINA WHERE IDLLOJVEPRIMI=1 and IDNDERM in (" + inidnderm + ") and IDNIVELGJENERUES IN (SELECT IDNIVEL FROM dbo.T_NIVELREGJISTRIMI WHERE IDKATDOK in (1,2))", ds, ukrijua);//magazinat e gjeneruar nga shitja blerja me artikuj
            ukrijua = krijodt("T_TRUPIMAGAZINA4", "SELECT  T_TRUPIMAGAZINA.* FROM dbo. T_TRUPIMAGAZINA INNER JOIN dbo. T_KOKAMAGAZINA ON   dbo.T_TRUPIMAGAZINA.IDKOKAMAGAZINA = dbo.T_KOKAMAGAZINA.IDKOKAMAGAZINA WHERE IDLLOJVEPRIMI=2 and IDNDERM in (" + inidnderm + ")  and IDNIVELGJENERUES IN (SELECT IDNIVEL FROM dbo.T_NIVELREGJISTRIMI WHERE IDKATDOK in (1,2))", ds, ukrijua);//magazinat e gjeneruar nga shitja blerja me makro
            ukrijua = krijodt("T_TRUPIMAGAZINA5", "SELECT  T_TRUPIMAGAZINA.* FROM dbo. T_TRUPIMAGAZINA INNER JOIN dbo. T_KOKAMAGAZINA ON   dbo.T_TRUPIMAGAZINA.IDKOKAMAGAZINA = dbo.T_KOKAMAGAZINA.IDKOKAMAGAZINA WHERE IDLLOJVEPRIMI=1 and IDNDERM in (" + inidnderm + ")  and IDNIVELGJENERUES IN (SELECT IDNIVEL FROM dbo.T_NIVELREGJISTRIMI WHERE IDKATDOK in (7))", ds, ukrijua);//magazinat e gjeneruar nga shperndarje shpenzime me artikuj
            ukrijua = krijodt("T_TRUPIMAGAZINA6", "SELECT  T_TRUPIMAGAZINA.* FROM dbo. T_TRUPIMAGAZINA INNER JOIN dbo. T_KOKAMAGAZINA ON   dbo.T_TRUPIMAGAZINA.IDKOKAMAGAZINA = dbo.T_KOKAMAGAZINA.IDKOKAMAGAZINA WHERE IDLLOJVEPRIMI=2 and IDNDERM in (" + inidnderm + ")  and IDNIVELGJENERUES IN (SELECT IDNIVEL FROM dbo.T_NIVELREGJISTRIMI WHERE IDKATDOK in (7))", ds, ukrijua);//magazinat e gjeneruar nga shperndarje shpenzime me makro
            ukrijua = krijodt("T_TRUPIMAKRO", "SELECT T_TRUPIMAKRO.* FROM T_TRUPIMAKRO INNER JOIN dbo.T_KOKAMAKRO ON   dbo.T_TRUPIMAKRO.IDKOKAMAKRO = dbo.T_KOKAMAKRO.IDKOKAMAKRO WHERE IDLLOJIMAKRO=1 and  IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_TRUPIMAKRO2", "SELECT T_TRUPIMAKRO.* FROM T_TRUPIMAKRO INNER JOIN dbo.T_KOKAMAKRO ON   dbo.T_TRUPIMAKRO.IDKOKAMAKRO = dbo.T_KOKAMAKRO.IDKOKAMAKRO WHERE IDLLOJIMAKRO=2 and  IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_TRUPIMAKRO3", "SELECT T_TRUPIMAKRO.* FROM T_TRUPIMAKRO INNER JOIN dbo.T_KOKAMAKRO ON   dbo.T_TRUPIMAKRO.IDKOKAMAKRO = dbo.T_KOKAMAKRO.IDKOKAMAKRO WHERE IDLLOJIMAKRO=3 and  IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_TRUPIPLANIFIKIM", "SELECT  T_TRUPIPLANIFIKIM.* FROM dbo. T_TRUPIPLANIFIKIM INNER JOIN dbo.T_KOKAPLANIFIKIM ON    dbo.T_TRUPIPLANIFIKIM.IDKOKAPLANIFIKIM = dbo.T_KOKAPLANIFIKIM.IDKOKAPLANIFIKIM WHERE IDNDERMarje in (" + inidnderm + ") ", ds, ukrijua);
            ukrijua = krijodt("T_TRUPIQENDRAKOSTO", "SELECT  T_TRUPIQENDRAKOSTO.* FROM dbo. T_TRUPIQENDRAKOSTO INNER JOIN dbo.T_KOKAQENDRAKOSTO ON    dbo.T_TRUPIQENDRAKOSTO.IDKOKA = dbo.T_KOKAQENDRAKOSTO.IDKOKA WHERE idgjenerues is null and  IDNDERMarje in (" + inidnderm + ") ", ds, ukrijua);

            ukrijua = krijodt("T_TRUPIQENDRAKOSTO17", "SELECT T_TRUPIQENDRAKOSTO.* FROM dbo. T_TRUPIQENDRAKOSTO INNER JOIN dbo.T_KOKAQENDRAKOSTO ON    dbo.T_TRUPIQENDRAKOSTO.IDKOKA = dbo.T_KOKAQENDRAKOSTO.IDKOKA WHERE   IDNDERMarje in (" + inidnderm + ") and idgjenerues in(select idkokafletekontabel from  dbo.T_KOKAFLETEKONTABEL WHERE IDNDERMARJE in (" + inidnderm + ") and IDKATEGORIA =5)", ds, ukrijua);
            ukrijua = krijodt("T_TRUPIQENDRAKOSTO2", "SELECT T_TRUPIQENDRAKOSTO.* FROM dbo. T_TRUPIQENDRAKOSTO INNER JOIN dbo.T_KOKAQENDRAKOSTO ON    dbo.T_TRUPIQENDRAKOSTO.IDKOKA = dbo.T_KOKAQENDRAKOSTO.IDKOKA WHERE   IDNDERMarje in (" + inidnderm + ") and idgjenerues in(select idkokafletekontabel from  dbo.T_KOKAFLETEKONTABEL WHERE IDNDERMARJE in (" + inidnderm + ") and IDKATEGORIA  in (1,2))", ds, ukrijua);
            ukrijua = krijodt("T_TRUPIQENDRAKOSTO3", "SELECT T_TRUPIQENDRAKOSTO.* FROM dbo. T_TRUPIQENDRAKOSTO INNER JOIN dbo.T_KOKAQENDRAKOSTO ON    dbo.T_TRUPIQENDRAKOSTO.IDKOKA = dbo.T_KOKAQENDRAKOSTO.IDKOKA WHERE   IDNDERMarje in (" + inidnderm + ") and idgjenerues in(select idkokafletekontabel from  dbo.T_KOKAFLETEKONTABEL WHERE IDNDERMARJE in (" + inidnderm + ") and IDKATEGORIA in (3,4))", ds, ukrijua);
            ukrijua = krijodt("T_TRUPIQENDRAKOSTO4", "SELECT T_TRUPIQENDRAKOSTO.* FROM dbo. T_TRUPIQENDRAKOSTO INNER JOIN dbo.T_KOKAQENDRAKOSTO ON    dbo.T_TRUPIQENDRAKOSTO.IDKOKA = dbo.T_KOKAQENDRAKOSTO.IDKOKA WHERE   IDNDERMarje in (" + inidnderm + ") and idgjenerues in(select idkokafletekontabel from  dbo.T_KOKAFLETEKONTABEL WHERE IDNDERMARJE in (" + inidnderm + ") and IDKATEGORIA =6 and idgjenerues in (SELECT idkokamagazina FROM dbo.T_KOKAMAGAZINA WHERE IDNDERM in (" + inidnderm + ")  AND (IDNIVELGJENERUES IS NULL OR IDNIVELGJENERUES IN (SELECT IDNIVEL FROM dbo.T_NIVELREGJISTRIMI WHERE IDKATDOK=6))))", ds, ukrijua);
            ukrijua = krijodt("T_TRUPIQENDRAKOSTO5", "SELECT T_TRUPIQENDRAKOSTO.* FROM dbo. T_TRUPIQENDRAKOSTO INNER JOIN dbo.T_KOKAQENDRAKOSTO ON    dbo.T_TRUPIQENDRAKOSTO.IDKOKA = dbo.T_KOKAQENDRAKOSTO.IDKOKA WHERE   IDNDERMarje in (" + inidnderm + ") and idgjenerues in(select idkokafletekontabel from  dbo.T_KOKAFLETEKONTABEL WHERE IDNDERMARJE in (" + inidnderm + ") and IDKATEGORIA =7)", ds, ukrijua);
            ukrijua = krijodt("T_TRUPIQENDRAKOSTO6", "SELECT T_TRUPIQENDRAKOSTO.* FROM dbo. T_TRUPIQENDRAKOSTO INNER JOIN dbo.T_KOKAQENDRAKOSTO ON    dbo.T_TRUPIQENDRAKOSTO.IDKOKA = dbo.T_KOKAQENDRAKOSTO.IDKOKA WHERE   IDNDERMarje in (" + inidnderm + ") and idgjenerues in(select idkokafletekontabel from  dbo.T_KOKAFLETEKONTABEL WHERE IDNDERMARJE in (" + inidnderm + ")and IDKATEGORIA =8)", ds, ukrijua);
            ukrijua = krijodt("T_TRUPIQENDRAKOSTO7", "SELECT T_TRUPIQENDRAKOSTO.* FROM dbo. T_TRUPIQENDRAKOSTO INNER JOIN dbo.T_KOKAQENDRAKOSTO ON    dbo.T_TRUPIQENDRAKOSTO.IDKOKA = dbo.T_KOKAQENDRAKOSTO.IDKOKA WHERE   IDNDERMarje in (" + inidnderm + ") and idgjenerues in(select idkokafletekontabel from  dbo.T_KOKAFLETEKONTABEL WHERE IDNDERMARJE in (" + inidnderm + ") and IDKATEGORIA =10 and idgjenerues in (SELECT idkoka FROM dbo.T_DOKUMENTLIDHESKOKA WHERE IDLLOJDOK=10 and  IDNDERMARJE in (" + inidnderm + ") ))", ds, ukrijua);
            ukrijua = krijodt("T_TRUPIQENDRAKOSTO8", "SELECT T_TRUPIQENDRAKOSTO.* FROM dbo. T_TRUPIQENDRAKOSTO INNER JOIN dbo.T_KOKAQENDRAKOSTO ON    dbo.T_TRUPIQENDRAKOSTO.IDKOKA = dbo.T_KOKAQENDRAKOSTO.IDKOKA WHERE   IDNDERMarje in (" + inidnderm + ") and idgjenerues in(select idkokafletekontabel from  dbo.T_KOKAFLETEKONTABEL WHERE IDNDERMARJE in (" + inidnderm + ") and IDKATEGORIA =11)", ds, ukrijua);
            ukrijua = krijodt("T_TRUPIQENDRAKOSTO9", "SELECT T_TRUPIQENDRAKOSTO.* FROM dbo. T_TRUPIQENDRAKOSTO INNER JOIN dbo.T_KOKAQENDRAKOSTO ON    dbo.T_TRUPIQENDRAKOSTO.IDKOKA = dbo.T_KOKAQENDRAKOSTO.IDKOKA WHERE   IDNDERMarje in (" + inidnderm + ") and idgjenerues in(select idkokafletekontabel from  dbo.T_KOKAFLETEKONTABEL WHERE IDNDERMARJE in (" + inidnderm + ") and IDKATEGORIA =20)", ds, ukrijua);
            ukrijua = krijodt("T_TRUPIQENDRAKOSTO10", "SELECT T_TRUPIQENDRAKOSTO.* FROM dbo. T_TRUPIQENDRAKOSTO INNER JOIN dbo.T_KOKAQENDRAKOSTO ON    dbo.T_TRUPIQENDRAKOSTO.IDKOKA = dbo.T_KOKAQENDRAKOSTO.IDKOKA WHERE   IDNDERMarje in (" + inidnderm + ") and idgjenerues in(select idkokafletekontabel from  dbo.T_KOKAFLETEKONTABEL WHERE IDNDERMARJE in (" + inidnderm + ") and IDKATEGORIA =6 and idgjenerues in (SELECT idkokamagazina FROM dbo.T_KOKAMAGAZINA WHERE IDNDERM in (" + inidnderm + ") AND ( IDNIVELGJENERUES IN (SELECT IDNIVEL FROM dbo.T_NIVELREGJISTRIMI WHERE IDKATDOK in (1,2)))))", ds, ukrijua);
            ukrijua = krijodt("T_TRUPIQENDRAKOSTO11", "SELECT T_TRUPIQENDRAKOSTO.* FROM dbo. T_TRUPIQENDRAKOSTO INNER JOIN dbo.T_KOKAQENDRAKOSTO ON    dbo.T_TRUPIQENDRAKOSTO.IDKOKA = dbo.T_KOKAQENDRAKOSTO.IDKOKA WHERE   IDNDERMarje in (" + inidnderm + ") and idgjenerues in(select idkokafletekontabel from  dbo.T_KOKAFLETEKONTABEL WHERE IDNDERMARJE in (" + inidnderm + ") and IDKATEGORIA =6 and idgjenerues in (SELECT idkokamagazina FROM dbo.T_KOKAMAGAZINA WHERE IDNDERM in (" + inidnderm + ")  AND (IDNIVELGJENERUES IN (SELECT IDNIVEL FROM dbo.T_NIVELREGJISTRIMI WHERE IDKATDOK=7))))", ds, ukrijua);
            ukrijua = krijodt("T_TRUPIQENDRAKOSTO12", "SELECT T_TRUPIQENDRAKOSTO.*FROM dbo. T_TRUPIQENDRAKOSTO INNER JOIN dbo.T_KOKAQENDRAKOSTO ON    dbo.T_TRUPIQENDRAKOSTO.IDKOKA = dbo.T_KOKAQENDRAKOSTO.IDKOKA WHERE   IDNDERMarje in (" + inidnderm + ") and idgjenerues in(select idkokafletekontabel from  dbo.T_KOKAFLETEKONTABEL WHERE IDNDERMARJE in (" + inidnderm + ") and IDKATEGORIA =10 and idgjenerues in (SELECT idkoka FROM dbo.T_DOKUMENTLIDHESKOKA WHERE IDLLOJDOK in (3,4) and  IDNDERMARJE in (" + inidnderm + ") ))", ds, ukrijua);
            ukrijua = krijodt("T_TRUPIQENDRAKOSTO13", "SELECT T_TRUPIQENDRAKOSTO.* FROM dbo. T_TRUPIQENDRAKOSTO INNER JOIN dbo.T_KOKAQENDRAKOSTO ON    dbo.T_TRUPIQENDRAKOSTO.IDKOKA = dbo.T_KOKAQENDRAKOSTO.IDKOKA WHERE   IDNDERMarje in (" + inidnderm + ") and idgjenerues in(select idkokafletekontabel from  dbo.T_KOKAFLETEKONTABEL WHERE IDNDERMARJE in (" + inidnderm + ") and IDKATEGORIA =10 and idgjenerues in (SELECT idkoka FROM dbo.T_DOKUMENTLIDHESKOKA WHERE IDLLOJDOK=20 and  IDNDERMARJE in (" + inidnderm + ")))", ds, ukrijua);
            ukrijua = krijodt("T_TRUPIQENDRAKOSTO14", "SELECT T_TRUPIQENDRAKOSTO.* FROM dbo. T_TRUPIQENDRAKOSTO INNER JOIN dbo.T_KOKAQENDRAKOSTO ON    dbo.T_TRUPIQENDRAKOSTO.IDKOKA = dbo.T_KOKAQENDRAKOSTO.IDKOKA WHERE   IDNDERMarje in (" + inidnderm + ") and idgjenerues in(select idkokafletekontabel from  dbo.T_KOKAFLETEKONTABEL WHERE IDNDERMARJE in (" + inidnderm + ") and IDKATEGORIA =38)", ds, ukrijua);
            ukrijua = krijodt("T_TRUPIQENDRAKOSTO15", "SELECT T_TRUPIQENDRAKOSTO.* FROM dbo. T_TRUPIQENDRAKOSTO INNER JOIN dbo.T_KOKAQENDRAKOSTO ON    dbo.T_TRUPIQENDRAKOSTO.IDKOKA = dbo.T_KOKAQENDRAKOSTO.IDKOKA WHERE   IDNDERMarje in (" + inidnderm + ") and idgjenerues in(select idkokafletekontabel from  dbo.T_KOKAFLETEKONTABEL WHERE IDNDERMARJE in (" + inidnderm + ") and IDKATEGORIA =45)", ds, ukrijua);
            ukrijua = krijodt("T_TRUPIQENDRAKOSTO16", "SELECT T_TRUPIQENDRAKOSTO.* FROM dbo. T_TRUPIQENDRAKOSTO INNER JOIN dbo.T_KOKAQENDRAKOSTO ON    dbo.T_TRUPIQENDRAKOSTO.IDKOKA = dbo.T_KOKAQENDRAKOSTO.IDKOKA WHERE   IDNDERMarje in (" + inidnderm + ") and idgjenerues in(select idkokafletekontabel from  dbo.T_KOKAFLETEKONTABEL WHERE IDNDERMARJE in (" + inidnderm + ") and IDKATEGORIA =64)", ds, ukrijua);


            ukrijua = krijodt("T_TRUPISKEMAQK", "SELECT  T_TRUPISKEMAQK.* FROM dbo. T_TRUPISKEMAQK INNER JOIN dbo.T_KOKASKEMAQK ON    dbo.T_TRUPISKEMAQK.IDKOKA= dbo.T_KOKASKEMAQK.IDKOKA WHERE   IDNDERMarje in (" + inidnderm + ") ", ds, ukrijua);
            ukrijua = krijodt("T_TRUPISKEMAWORKFLOW", "SELECT  T_TRUPISKEMAWORKFLOW.* FROM dbo. T_TRUPISKEMAWORKFLOW INNER JOIN dbo.T_KOKASKEMAWORKFLOW ON    dbo.T_TRUPISKEMAWORKFLOW.IDKOKA= dbo.T_KOKASKEMAWORKFLOW.IDKOKA WHERE LLOJI=1 AND IDNDERMarje in (" + inidnderm + ") ", ds, ukrijua);

            ukrijua = krijodt("T_TRUPISKEMAWORKFLOW2", "SELECT  T_TRUPISKEMAWORKFLOW.* FROM dbo. T_TRUPISKEMAWORKFLOW INNER JOIN dbo.T_KOKASKEMAWORKFLOW ON    dbo.T_TRUPISKEMAWORKFLOW.IDKOKA= dbo.T_KOKASKEMAWORKFLOW.IDKOKA WHERE LLOJI=2 AND IDNDERMarje in (" + inidnderm + ") ", ds, ukrijua);
            ukrijua = krijodt("T_TRUPISHITJE", "SELECT  T_TRUPISHITJE.* FROM dbo. T_TRUPISHITJE INNER JOIN dbo.T_KOKASHITJE ON    dbo.T_TRUPISHITJE.IDSHITJEKOKA = dbo.T_KOKASHITJE.IDSHITJEKOKA WHERE IDLLOJVEPRIMI=1 and IDNDERM in (" + inidnderm + ") ", ds, ukrijua);// artikujt
            ukrijua = krijodt("T_TRUPISHITJE2", "SELECT  T_TRUPISHITJE.* FROM dbo. T_TRUPISHITJE INNER JOIN dbo.T_KOKASHITJE ON    dbo.T_TRUPISHITJE.IDSHITJEKOKA = dbo.T_KOKASHITJE.IDSHITJEKOKA WHERE  IDLLOJVEPRIMI=3 and IDNDERM in (" + inidnderm + ") ", ds, ukrijua);//llogarite
            ukrijua = krijodt("T_TRUPISKEMAFLETEKONTABEL", "SELECT  T_TRUPISKEMAFLETEKONTABEL.* FROM dbo. T_TRUPISKEMAFLETEKONTABEL INNER JOIN dbo.T_KOKASKEMAFLETEKONTABEL ON    dbo.T_TRUPISKEMAFLETEKONTABEL.IDKOKASKEMAFK = dbo.T_KOKASKEMAFLETEKONTABEL.IDKOKASKEMAFK WHERE IDNDERMARJE in (" + inidnderm + ") ", ds, ukrijua);
            ukrijua = krijodt("T_URDHERPOROSI_PLANIFIKIM", "SELECT T_URDHERPOROSI_PLANIFIKIM.* FROM dbo.T_URDHERPOROSI_PLANIFIKIM INNER JOIN dbo.T_KONFIGAMBJENTE ON dbo.T_URDHERPOROSI_PLANIFIKIM.IDKONFIGUrdher = dbo.T_KONFIGAMBJENTE.IDKONFIGAMBJENTE WHERE  dbo.T_KONFIGAMBJENTE.IDNDERMARJE  in (" + inidnderm + ")  ", ds, ukrijua);
            ukrijua = krijodt("T_VEPRIMBANKAKOKA", "SELECT * FROM  T_VEPRIMBANKAKOKA WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_VEPRIMBANKATRUPI", "SELECT  T_VEPRIMBANKATRUPI.* FROM dbo. T_VEPRIMBANKATRUPI INNER JOIN dbo.T_VEPRIMBANKAKOKA ON     dbo.T_VEPRIMBANKATRUPI.IDKOKA = dbo.T_VEPRIMBANKAKOKA.IDKOKA WHERE LLOJI='Llogari' and  IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);//trupat qe kane llogari
            ukrijua = krijodt("T_VEPRIMBANKATRUPI2", "SELECT  T_VEPRIMBANKATRUPI.* FROM dbo. T_VEPRIMBANKATRUPI INNER JOIN dbo.T_VEPRIMBANKAKOKA ON     dbo.T_VEPRIMBANKATRUPI.IDKOKA = dbo.T_VEPRIMBANKAKOKA.IDKOKA WHERE LLOJI!='Llogari' AND IDFATURA IS NULL and  IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);// trupat qe kane klient furnitor por jo te lidhur me fatura
            ukrijua = krijodt("T_VEPRIMBANKATRUPI3", "SELECT  DISTINCT dbo.T_VEPRIMBANKATRUPI.* FROM dbo. T_VEPRIMBANKATRUPI INNER JOIN dbo.T_VEPRIMBANKAKOKA ON     dbo.T_VEPRIMBANKATRUPI.IDKOKA = dbo.T_VEPRIMBANKAKOKA.IDKOKA WHERE LLOJI!='Llogari' and  T_VEPRIMBANKAKOKA.IDNDERMARJE in (" + inidnderm + ") AND IDFATURA IS NOT NULL  AND dbo.T_VEPRIMBANKATRUPI.IDNIVEL IN (SELECT idnivel FROM dbo.T_NIVELREGJISTRIMI WHERE IDKATDOK IN (1,2))", ds, ukrijua);// trupat klient furnitor por te lidhur me fatura shitje blerje
            ukrijua = krijodt("T_VEPRIMBANKATRUPI4", "SELECT  DISTINCT dbo.T_VEPRIMBANKATRUPI.* FROM dbo. T_VEPRIMBANKATRUPI INNER JOIN dbo.T_VEPRIMBANKAKOKA ON     dbo.T_VEPRIMBANKATRUPI.IDKOKA = dbo.T_VEPRIMBANKAKOKA.IDKOKA WHERE LLOJI!='Llogari' and  T_VEPRIMBANKAKOKA.IDNDERMARJE in (" + inidnderm + ") AND IDFATURA IS NOT NULL  AND dbo.T_VEPRIMBANKATRUPI.IDNIVEL IN (SELECT idnivel FROM dbo.T_NIVELREGJISTRIMI WHERE IDKATDOK IN (20))", ds, ukrijua);//trupat klient furnitor por te lidhur me veprime kf
            ukrijua = krijodt("T_VEPRIMEKFKOKA", "  SELECT * FROM dbo.T_VEPRIMEKFKOKA WHERE  IDNDERM in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_VEPRIMEKFTRUPI", "SELECT  T_VEPRIMEKFTRUPI.* FROM dbo. T_VEPRIMEKFTRUPI INNER JOIN dbo.T_VEPRIMEKFKOKA ON    dbo.T_VEPRIMEKFTRUPI.IDVEPRIMEKFKOKA = dbo.T_VEPRIMEKFKOKA.IDVEPRIMKFKOKA WHERE  IDNDERM in (" + inidnderm + ") ", ds, ukrijua);
            ukrijua = krijodt("T_VITET", "SELECT * FROM  T_VITET WHERE IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_VLERAFUSHASHTESE", "SELECT T_VLERAFUSHASHTESE.* FROM T_VLERAFUSHASHTESE INNER JOIN dbo.T_MODELIFUSHASHTESE ON  dbo.T_VLERAFUSHASHTESE.IDMODELIFUSHASHTESE = dbo.T_MODELIFUSHASHTESE.IDMODELIFUSHASHTESE WHERE IDLLOJMODELIFUSHASHTESE=1 and   IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_VLERAFUSHASHTESE2", "SELECT T_VLERAFUSHASHTESE.* FROM T_VLERAFUSHASHTESE INNER JOIN dbo.T_MODELIFUSHASHTESE ON  dbo.T_VLERAFUSHASHTESE.IDMODELIFUSHASHTESE = dbo.T_MODELIFUSHASHTESE.IDMODELIFUSHASHTESE WHERE IDLLOJMODELIFUSHASHTESE=2 and   IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_VLERAFUSHASHTESE3", "SELECT T_VLERAFUSHASHTESE.* FROM T_VLERAFUSHASHTESE INNER JOIN dbo.T_MODELIFUSHASHTESE ON  dbo.T_VLERAFUSHASHTESE.IDMODELIFUSHASHTESE = dbo.T_MODELIFUSHASHTESE.IDMODELIFUSHASHTESE WHERE IDLLOJMODELIFUSHASHTESE=3 and   IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_VLERAKONFIGURIMIKASA", "SELECT  T_VLERAKONFIGURIMIKASA.* FROM  T_VLERAKONFIGURIMIKASA INNER JOIN dbo.T_KONFIGURIMKASA ON    dbo.T_VLERAKONFIGURIMIKASA.IDKONFIGURIMI = dbo.T_KONFIGURIMKASA.IDKONFIGURIMI WHERE  IDNDERMARJE in (" + inidnderm + ")", ds, ukrijua);
            ukrijua = krijodt("T_ZBRITJEANALITIKE", "  SELECT * FROM dbo.T_ZBRITJEANALITIKE WHERE  IDNDERMARJE in (" + inidnderm + ") ", ds, ukrijua);
            #endregion
            if (ukrijua == false)
                return null;
            return ds;
        }
        
        #endregion
        
    }
}