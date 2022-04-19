using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DbCore.DbInventari;
using System.Data;
using System.Web;
using System.Globalization;
using DbCore.IMBUtils.Logging;

namespace DbCore.DbRegjistrim
{
    public class clsTrupiRezervime
    {


        #region Attributet
        private int idTrupiRezervime;
        private int idKokaRezervime;
        private int idArtikull;
        private string kodArtikull;
        private string pershkrimArtikull;
        private int idNjesia;
        private double sasia;
        private double koeficenti;
        private int idMag;
        private DateTime data;
        private int idStatusDok;
        private int shenja;
        private int idTrupiNgaVjen;
        private int idTrupiHyrje;
        private object element;
        private DataRow rreshti;
        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases me parametra
        /// </summary>

        /// <param name="dt"> data e dokumentit</param>
        /// <param name="idArt"> id e artikullit</param>
        /// <param name="idmag">id e magazines</param>
        /// <param name="idRezKoka">id e kokes se dokumentit te rezervimit</param>
        /// <param name="idRezTrup">id ritese e trupit te dokumentit te rezervimit</param>
        /// <param name="idNjes">id e njesise se artikullit</param>
        /// <param name="idstatusdok">id e gjendjes se dokumentit</param>
        /// <param name="KodArt">kodi i artikullit</param>
        /// <param name="koefic">koeficienti midis dy njesive te artikullit</param>
        /// <param name="pershkArt">pershkrimi i artikullit</param>
        /// <param name="sas"> sasia e artikullit</param>

        public clsTrupiRezervime(int idRezTrup, int idRezKoka, int idArt, string KodArt, string pershkArt, int idNjes, double sas, double koefic, int idmag, DateTime dt, int idstatusdok, int sgn, int idtrupingavjen, int idtrupihyrje, clsArtikulli art)
        {
            ImbLogger.LogTraceShitje("Filloi mbushja clsTrupiRezervime!");
            idTrupiRezervime = idRezTrup;
            idKokaRezervime = idRezKoka;
            idArtikull = idArt;
            kodArtikull = KodArt;
            pershkrimArtikull = pershkArt;
            idNjesia = idNjes;
            sasia = sas;
            koeficenti = koefic;
            idMag = idmag;
            data = dt;
            idStatusDok = idstatusdok;
            shenja = sgn;
            idTrupiNgaVjen = idtrupingavjen;
            idTrupiHyrje = idtrupihyrje;
            this.element = art;
            ImbLogger.LogTraceShitje("Mbaroi mbushja clsTrupiRezervime!");

        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idTrupi"></param>
        public clsTrupiRezervime(int idTrupi)
        {
            clsDatabaseRegjistrim dbtrup = new clsDatabaseRegjistrim();
            mbushTrupRezervimi(dbtrup.ktheTrupiRezervimSipasID(idTrupi));
            dbtrup.Dispose();
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idTrupi"></param>
        /// <param name="dbtrup"></param>
        public clsTrupiRezervime(int idTrupi, clsDatabaseRegjistrim dbtrup)
        {
            mbushTrupRezervimi(dbtrup.ktheTrupiRezervimSipasID(idTrupi));
        }


        public bool krijoTrupRezervimiNgaGrida(int idNdermarrje, DateTime date, double kursi, int idkodi, string emertimi, int njesia, double sasia, int magazina, string kodi)
        {
            if (idkodi == 0 || idkodi == -1) return false;

            clsArtikulli art = new clsArtikulli(idkodi);
            if (art.IdArtikulli == 0)
            {
                throw new Exception("Nje nga artikujt nuk ekziston!");
            }
            if (art.Klasa == 2 || art.Klasa == 3) return false;
            if (art.Klasa == 4)
            {
                throw new Exception("Artikull i perbere!");
            }
            else
            {
                idArtikull = idkodi;
                kodArtikull = kodi;
                pershkrimArtikull = emertimi;
                idNjesia = njesia;
                if (art.Njesi1Artikulli == idNjesia)//nese njesia e zgjedhuer eshte njesia 1 e artikullit ath koeficienti vendoset 1
                    this.koeficenti = 1;
                else
                    this.koeficenti = double.Parse(art.KoeficientArtikulli.ToString());//nese njesia e zgjedhur nuk eshte njesia e pare e artikullit ath koeficienti vendoset sa koeficienti i percaktuar tek artikulli                        
                this.sasia = sasia;
                idMag = magazina;
                data = date;

            }
            return true;
        }

        /// <summary>
        /// Sherben per te krijuar trupRezervimin nga grida client
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="eshteTransferim"></param>
        /// <param name="rreshtDokuKlient"></param>
        public clsTrupiRezervime(int idNdermarrje, int idPerdorues, DateTime date, Dictionary<string, object> rreshtDokuKlient, int lloji, int iddokezkistuesdalje)
        {
            string kategoria = rreshtDokuKlient["txtKategoria"].ToString();
            string kodi = rreshtDokuKlient["txtKodi"].ToString();
            if (kodi == "")
                return;
            int idKodi = Convert.ToInt32(rreshtDokuKlient["txtIdKodi"]);
            string emertimi = rreshtDokuKlient["txtEmertimi"].ToString();
            string njesia = rreshtDokuKlient["txtNjesia"].ToString();
            string sasia = rreshtDokuKlient["txtSasia"].ToString();
            string magazina = rreshtDokuKlient["txtMagazina"].ToString();
            int idtrupi = 0;
            int.TryParse(rreshtDokuKlient["txtIdTrupi"].ToString(), out idtrupi);
            if (kodi != null && kodi != "null" && kodi != "")
            {
                if (!(kategoria == "Artikull" || kategoria == "Makro"))
                    throw new Exception("Kategoria nuk mund te jete e ndryshme nga Makro dhe Artikull te rregjistrim dokument magazine");

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
                idArtikull = art.IdArtikulli;
                kodArtikull = kodi;
                if (emertimi != null && emertimi != "null")
                    pershkrimArtikull = emertimi;
                element = art;
                if (njesia != null && njesia != "null" && njesia != "")
                {
                    clsNjesiArtikulli njesi = new clsNjesiArtikulli();
                    njesi.mbushNjesiArtikulliMeKod(njesia, idNdermarrje); //kevi ndryshim nga me pershk me kod
                    idNjesia = njesi.IdNjesia;
                    if (art.Njesi1Artikulli == idNjesia)//nese njesia e zgjedhuer eshte njesia 1 e artikullit ath koeficienti vendoset 1
                        koeficenti = 1;
                    else
                        koeficenti = double.Parse(art.KoeficientArtikulli.ToString());//nese njesia e zgjedhur nuk eshte njesia e pare e artikullit ath koeficienti vendoset sa koeficienti i percaktuar tek artikulli
                }
                if (lloji == 1 || lloji == 2)
                    this.idTrupiHyrje = idtrupi;
                else this.IdTrupiRezervime = idtrupi;
                if (sasia != null && sasia != "null" && sasia != "")
                    this.sasia = double.Parse(sasia);
                if (lloji == 1 || lloji == 2)
                {
                    clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim();
                    double sasiaeharxhuar = dbRegj.ktheSasineKonvertuarSipasArtikullitPaDokEkzistues(this.IdArtikulli, this.IdTrupiHyrje, iddokezkistuesdalje);

                    this.sasia = ((this.idNjesia == art.Njesi1Artikulli ? this.sasia : (this.sasia * Convert.ToDouble(art.KoeficientArtikulli))) < (this.IdNjesia == art.Njesi1Artikulli ? this.Sasia - sasiaeharxhuar : this.Sasia * Convert.ToDouble(art.KoeficientArtikulli) - sasiaeharxhuar) ? this.Sasia : (this.IdNjesia == art.Njesi1Artikulli ? this.Sasia - sasiaeharxhuar : this.Sasia * Convert.ToDouble(art.KoeficientArtikulli) - sasiaeharxhuar)) / (art.Njesi1Artikulli == this.IdNjesia ? 1 : Convert.ToDouble(art.KoeficientArtikulli));

                }
                int idMagHyrje = -1;


                if (magazina != null && magazina != "null" && magazina != "" && magazina != "Pa Magazine")
                {
                    DbCore.DbRegjistrim.clsNjesiAdministrative njesiadm = new DbCore.DbRegjistrim.clsNjesiAdministrative(magazina, idNdermarrje);
                    if (njesiadm.IdNjesiAdministrative <= 0)
                        throw new Exception("Nje nga magazinat nuk ekziston!");
                    njesiadm = new clsNjesiAdministrative(magazina, idNdermarrje, idPerdorues);

                    if (njesiadm.IdNjesiAdministrative < 1)
                        throw new Exception("Nuk keni autorizime ne kete magazine!");
                    if (magazina != "" && !njesiadm.Aktiv)
                        throw new Exception("Magazina nuk eshte aktive!");
                    idMagHyrje = njesiadm.IdNjesiAdministrative;
                }

                if (magazina == "Pa Magazine") idMagHyrje = 0;

                idMag = idMagHyrje;
                data = date;
            }
            else
                idArtikull = -1;
        }

        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsTrupiRezervime()
        {
        }

        public clsTrupiRezervime(DataRow rreshti)
        {
            
            mbushTrupRezervimi(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdTrupiRezervime
        {
            get { return idTrupiRezervime; }
            set { idTrupiRezervime = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e kokes se dokumentit te magazines.
        /// </summary>
        public int IdKokaRezervime
        {
            get { return idKokaRezervime; }
            set { idKokaRezervime = value; }
        }


        /// <summary>
        /// id e trupit te dokumentit nga vjen psh ush
        /// </summary>
        public int IdTrupiNgaVjen
        {
            get
            {
                return idTrupiNgaVjen;
            }
            set
            {
                idTrupiNgaVjen = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e artikullit .
        /// </summary>
        public int IdArtikulli
        {
            get { return idArtikull; }
            set { idArtikull = value; }
        }

        /// <summary>
        /// Kthen/Vendos Kodi i artikullit.
        /// </summary>
        public String KodiArtikull
        {
            get { return kodArtikull; }
            set { kodArtikull = value; }
        }

        /// <summary>
        /// Kthen/Vendos pershkrimi i artikullit.
        /// </summary>
        public String PershkrimArtikull
        {
            get { return pershkrimArtikull; }
            set { pershkrimArtikull = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e njesise se artikullit.
        /// </summary>
        public int IdNjesia
        {
            get { return idNjesia; }
            set { idNjesia = value; }
        }
        /// <summary>
        /// ruhet artikulli per te mos e aksesuar shume here nga db
        /// </summary>
        public object Element
        {
            get
            {
                return element;
            }
            set
            {
                element = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos sasia e artikullit.
        /// </summary>
        public double Sasia
        {
            get { return sasia; }
            set { sasia = value; }
        }

        /// <summary>
        /// Kthen/Vendos koeficienti midis dy njesive te artikullit.
        /// </summary>
        public double Koeficenti
        {
            get { return koeficenti; }
            set { koeficenti = value; }
        }


        /// <summary>
        /// Kthen/Vendos ID-ne e magazines.
        /// </summary>
        public int IdMag
        {
            get { return idMag; }
            set { idMag = value; }
        }

        /// <summary>
        /// Kthen/Vendos data e dokumentit.
        /// </summary>
        public DateTime Data
        {
            get { return data; }
            set { data = value; }
        }
        /// <summary>
        /// id e trupit te dokumentit te hyrjes per te cilen po behet dalje
        /// </summary>
        public int IdTrupiHyrje
        {
            get
            {
                return idTrupiHyrje;
            }
            set
            {
                idTrupiHyrje = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e gjendjes se dokumentit.
        /// <example>ruajtur, draft etj</example>
        /// </summary>
        public int IdStatusDok
        {
            get { return idStatusDok; }
            set { idStatusDok = value; }
        }

        public int Shenja
        {
            get { return shenja; }
            set { shenja = value; }
        }
        #endregion

        #region Metoda Publike


        /// <summary>
        /// Ruan objektin e  trupit te dokumentit te rezervimit ne tabelen perkatese ne databaze.Therret funksionin
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
        public clsMesazh ruaj()
        {
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            int id;
            clsMesazh u_ruajt = data.ruajTrupiRezervime(out id, this.IdKokaRezervime, this.IdArtikulli, this.IdNjesia, this.Sasia, this.Koeficenti, this.IdMag, this.Data, this.IdStatusDok, this.Shenja, this.IdTrupiNgaVjen, this.idTrupiHyrje);
            this.IdTrupiRezervime = id;
            data.Dispose();
            return u_ruajt;
        }

        /// <summary>
        /// modifikon objektin e  trupit te dokumentit te rezervimit ne tabelen perkatese ne databaze.Therret funksionin
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo</returns>
        public clsMesazh modifiko()
        {
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            clsMesazh u_modifikua = data.modifikoTrupiRezervime(this.IdTrupiRezervime, this.IdKokaRezervime, this.IdArtikulli, this.IdNjesia, this.Sasia, this.Koeficenti, this.IdMag, this.Data, this.IdStatusDok, this.Shenja, this.idTrupiNgaVjen, this.IdTrupiHyrje);
            data.Dispose();
            return u_modifikua;
        }

        /// <summary>
        /// fshin objektin e  trupit te dokumentit te rezervimit ne tabelen perkatese ne databaze.Therret funksionin
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh fshi()
        {
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            clsMesazh u_fshi = data.fshiTrupiRezervimeSipasID(this.IdTrupiRezervime);
            data.Dispose();
            return u_fshi;
        }

        /// <summary>
        /// fshin objektet e  trupit te dokumentit te rezervimit sipas id se kokes ne tabelen perkatese ne databaze.Therret funksionin
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh fshiSipasKoka()
        {
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            clsMesazh u_fshi = data.fshiTrupiRezervimeSipasKoka(this.IdKokaRezervime);
            data.Dispose();
            return u_fshi;
        }

        /// <summary>
        /// Merr objektet e  trupit te dokumentit te rezervimit sipas kokes nga tabela perkatese ne databaze.Therret funksionin
        /// </summary>
        /// <returns > nje objekt  colTrupiRezervime me te gjithe trupat e nje dokumenti</returns>
        //public colTrupiRezervime merriSipasKoka()
        //{
        //    colTrupiRezervime data = new colTrupiRezervime();
        //    data.mbushGjitheTrupiRezervimiNgaKoka(this.IdKokaRezervime);
        //    return data;
        //}


        public static bool kaVeprimeRezPerArtikull(int idArt, int idNderm)
        {
            clsDatabaseRegjistrim dat = new clsDatabaseRegjistrim();
            bool kaVeprime = dat.kaVeprimeRezArtikulli(idArt, idNderm);
            dat.Dispose();
            return kaVeprime;
        }

        /// <summary>
        /// Merr objektin e  trupit te dokumentit te rezervimit sipas id nga tabela perkatese ne databaze.Therret funksionin
        /// </summary>
        ///// <returns > nje objekt clsTrupiRezervime me trupin e dokumentit te rezervimit te kerkuar</returns>
        //public clsTrupiRezervime merriSipasID()
        //{
        //    clsTrupiRezervime data = new clsTrupiRezervime(this.IdTrupiRezervime);
        //    return data;

        //}

        public static double merrSasiNgaUB(clsArtikulli artikulli)
        {
            double sasi = 0;
            clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim();

            sasi = dbRegj.merrSasiPorositur(artikulli.IdArtikulli);

            dbRegj.Dispose();
            return sasi;
        }

        public static double merrSasi(int artikulli, int idtrupi, int idmag, DateTime data)
        {
            double gjendjaArtikullitNeMagazine = 0;
            clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim();

            gjendjaArtikullitNeMagazine = dbRegj.ktheSasineRezervuarSipasArtikullit(artikulli, idtrupi, idmag, data);

            dbRegj.Dispose();
            return gjendjaArtikullitNeMagazine;
        }

        public static double merrSasiSipasArtikullitDheMagazines(int artikulli, int idtrupi, int idmag, DateTime data)
        {
            using (var db = new clsDatabaseRegjistrim())
                return db.ktheSasineRezervuarSipasArtikullitDheMagazines(artikulli, idtrupi, idmag, data);
        }
        /// <summary>
        /// merr sasine e rezervuar ne UB sipas magazines
        /// eshte sasia qe shfaqet te Sasi e rezervuar UB te info e artikullit sipas magazines
        /// </summary>
        /// <param name="artikulli"></param>
        /// <param name="idtrupi"></param>
        /// <param name="idmag"></param>
        /// <param name="data"></param>
        /// <returns></returns>

        public static double merrSasiUB(int artikulli, int idtrupi, int idmag, DateTime data)
        {
            double gjendjaArtikullitNeMagazine = 0;
            clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim();

            gjendjaArtikullitNeMagazine = dbRegj.ktheSasineRezervuarSipasArtikullitUB(artikulli, idtrupi, idmag, data);

            dbRegj.Dispose();
            return gjendjaArtikullitNeMagazine;
        }


        public static double merrSasi(clsArtikulli artikulli, int idtrupi, int idmag, DateTime data, clsDatabaseRegjistrim dbRegj)
        {
            double gjendjaArtikullitNeMagazine = 0;


            gjendjaArtikullitNeMagazine = dbRegj.ktheSasineRezervuarSipasArtikullit(artikulli.IdArtikulli, idtrupi, idmag, data);


            return gjendjaArtikullitNeMagazine;
        }

        public static double merrSasiGjitheMag(clsArtikulli artikulli, int idtrupi, DateTime data, clsDatabaseRegjistrim dbRegj)
        {
            double gjendjaArtikullitNeMagazine = 0;


            gjendjaArtikullitNeMagazine = dbRegj.ktheSasineRezervuarSipasArtikullitTotal(artikulli.IdArtikulli, idtrupi, data);

            return gjendjaArtikullitNeMagazine;
        }

        public static double merrSasiGjitheMag(clsArtikulli artikulli, int idtrupi, DateTime data)
        {
            double gjendjaArtikullitNeMagazine = 0;

            clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim();
            gjendjaArtikullitNeMagazine = dbRegj.ktheSasineRezervuarSipasArtikullitTotal(artikulli.IdArtikulli, idtrupi, data);
            dbRegj.Dispose();
            return gjendjaArtikullitNeMagazine;
        }

        public static double merrSasiGjitheMagInfo(clsArtikulli artikulli, int idtrupi, DateTime data)
        {
            double gjendjaArtikullitNeMagazine = 0;

            clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim();
            gjendjaArtikullitNeMagazine = dbRegj.ktheSasineRezervuarSipasArtikullitTotalInfo(artikulli.IdArtikulli, idtrupi, data);
            dbRegj.Dispose();
            return gjendjaArtikullitNeMagazine;
        }

        public clsTrupiRezervime ShallowCopy()
        {
            return (clsTrupiRezervime)this.MemberwiseClone();
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush trupin e rezervimit nga databaza
        /// </summary>
        /// <param name="dbDataRowTrup">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushTrupRezervimi(DataRow dbDataRowTrup)
        {
            ImbLogger.LogTraceShitje("Filloi metoda mbushtrupRezervimi!");
            if (dbDataRowTrup != null)
            {
                try
                {
                    int.TryParse(dbDataRowTrup["IDTRUPIREZERVIME"].ToString(), out idTrupiRezervime);
                    int.TryParse(dbDataRowTrup["IDKOKAREZERVIME"].ToString(), out idKokaRezervime);
                    int.TryParse(dbDataRowTrup["IDARTIKULL"].ToString(), out idArtikull);
                    int.TryParse(dbDataRowTrup["IDNJESIA"].ToString(), out idNjesia);
                    double.TryParse(dbDataRowTrup["SASIA"].ToString(), out sasia);
                    double.TryParse(dbDataRowTrup["KOEFICENTI"].ToString(), out koeficenti);
                    int.TryParse(dbDataRowTrup["IDMAG"].ToString(), out idMag);
                    DateTime.TryParse(dbDataRowTrup["DATA"].ToString(), out data);
                    int.TryParse(dbDataRowTrup["IDSTATUSDOK"].ToString(), out idStatusDok);
                    int.TryParse(dbDataRowTrup["SHENJA"].ToString(), out shenja);
                    int.TryParse(dbDataRowTrup["IDTRUPINGAVJEN"].ToString(), out idTrupiNgaVjen);
                    int.TryParse(dbDataRowTrup["IDTRUPIHYRJE"].ToString(), out idTrupiHyrje);
                    ImbLogger.LogTraceShitje("Mbaroi metoda mbushtrupRezervimi!");
                    return true;
                }
                catch (InvalidCastException)
                {
                    ImbLogger.LogErrorShitje("ERROR: Gabim gjate marrjes se trupit te rezervimit nga db-ja");
                    throw new Exception("ERROR: Gabim gjate marrjes se trupit te rezervimit nga db-ja");
                }
            }
            else
            {
                ImbLogger.LogTraceShitje("Mbaroi metoda mbushtrupRezervimi!");
                return false;
            }
        }

        #endregion

    }
}
