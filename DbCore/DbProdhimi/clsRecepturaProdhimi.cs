using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using DbCore.DbRegjistrim;

namespace DbCore.DbProdhimi
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne  recepturat e nje produkti prodhimi
    ///  (Te dhenat  merren nga tabela : T_RECEPTURAPRODHIMI)
    /// </summary>
    public class clsRecepturaProdhimi
    {

        /// <summary>
        /// konstante per mesazhin e gabimit te marrjes se te dhenave nga db
        /// </summary>
        private const string STR_ERRORGabimGjateMarrjesSeTrupitTePlanifikimitNgaD = "ERROR: Gabim gjate marrjes se recepturave  nga db-ja";

        #region Attributet
        /// <summary>
        /// id e receptures
        /// </summary>
        private int id;
        /// <summary>
        /// id e produktit
        /// </summary>
        private int idProdukti;
        /// <summary>
        /// id e artikullit
        /// </summary>
        private int idArtikull;
        /// <summary>
        /// kodi i artikullit
        /// </summary>
        private string kodArtikull;
        /// <summary>
        /// pershkrimi i artikullit
        /// </summary>
        private string pershkrimArtikull;
        /// <summary>
        /// id e burimit
        /// </summary>
        private int idBurimi;
        /// <summary>
        /// sasia
        /// </summary>
        private double sasia;
        /// <summary>
        /// id e magazines
        /// </summary>
        private int idMag;
        /// <summary>
        /// scrapi
        /// </summary>
        private double scrap;
        /// <summary>
        /// kostoja e artikullit/burimit
        /// </summary>
        private double kosto;
        /// <summary>
        /// lloji 1-artikull 2-burim
        /// </summary>
        private int lloji;
        /// <summary>
        /// kosto totale e artikullit/burimit
        /// </summary>
        private double kostoTotale;
        /// <summary>
        /// njesia e pare e artikullit
        /// </summary>
        private int njesiArtikull;
        /// <summary>
        /// sasia aktuale e receptures
        /// </summary>
        private double sasiaAktuale;
        /// <summary>
        /// kodi i produktit
        /// </summary>
        private string kodProdukti;
        /// <summary>
        /// pershkrimi i produktit
        /// </summary>
        private string pershkrimProdukti;
        /// <summary>
        /// firo ligjore ne perqindje
        /// </summary>
        private double firoPerqindje;
        /// <summary>
        /// fushe qe mban id e trupit te skedulimit te prodhimit te ndara me presje ne raste kur receptura burimi merret nga skedulimi 
        /// </summary>
        private string skedulim;
        /// <summary>
        /// id e detajimit 1  te artikullit
        /// </summary>
        private int idDetajimi;
        /// <summary>
        /// id e detajimit 2 te artikullit
        /// </summary>
        private int idDetajimi2;
        private DataRow rreshti;
        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases me parametra
        /// </summary>
        /// <param name="idArt"> id e artikullit</param>
        /// <param name="idmag">id e magazines</param>
        /// <param name="idprodukti">id e produktit</param>
        /// <param name="id">id ritese e receptures</param>
        /// <param name="idburimi">id e burimit</param>
        /// <param name="KodArt">kodi i artikullit</param>
        /// <param name="pershkArt">pershkrimi i artikullit</param>
        /// <param name="scrap">scrapi</param>
        /// <param name="kosto">kostoja</param>
        /// <param name="lloji">lloji 1 artikull 2 burim</param>
        /// <param name="sas"> sasia e artikullit</param>
        /// <param name="sasiakt">sasia aktuale e receptures</param>
        /// <param name="firoperq">firo ne perqindje</param>
        public clsRecepturaProdhimi(int id, int idprodukti, int idArt, string KodArt, string pershkArt, int idburimi, double sas, double scrap, double kosto, int lloji, int idmag, double sasiakt, double firoperq, int iddetajimi, int iddetajimi2)
        {
            this.id = id;
            idProdukti = idprodukti;
            idArtikull = idArt;
            kodArtikull = KodArt;
            pershkrimArtikull = pershkArt;
            idBurimi = idburimi;
            sasia = sas;
            idMag = idmag;
            this.scrap = scrap;
            this.kosto = kosto;
            this.lloji = lloji;
            sasiaAktuale = sasiakt;
            firoPerqindje = firoperq;
            this.idDetajimi = iddetajimi;
            this.idDetajimi2 = iddetajimi2;

        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="id">id e receptures</param>
        public clsRecepturaProdhimi(int id)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            mbushRecepture(db.ktheReceptureProdhimiSipasID(id));
            db.Dispose();
        }

        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsRecepturaProdhimi()
        {
        }

        /// <summary>
        /// konstruktori me te dhenat nga grida
        /// </summary>
        /// <param name="idndermarje">idndermarje</param>
        /// <param name="rresht">rreshti me te dhena</param>
        public clsRecepturaProdhimi(int idndermarje, int idPerdorues, Dictionary<string, object> rreshtDokuKlient)
        {

            string kodprod = rreshtDokuKlient["txtKodProdukti"].ToString();
            if (kodprod == "")
                return;
            string kodart = rreshtDokuKlient["txtKodiArtikullR"].ToString();
            if (kodart == "")
                return;
            int idprod = Convert.ToInt32(rreshtDokuKlient["txtIdProdukti"]);
            int index = 0;
            int.TryParse(rreshtDokuKlient["txtIndexPrindi"].ToString(), out index);
            string mag = rreshtDokuKlient["txtIdMagR"].ToString();


            string pershkrimart = rreshtDokuKlient["txtPershkrimArtikullR"].ToString();
            string sasiakt = rreshtDokuKlient["txtSasiaAktualeR"].ToString();
            string firo = rreshtDokuKlient["txtFiroPerqindje"].ToString();
            string scrap = rreshtDokuKlient["txtScrap"].ToString();
            string sasipor = rreshtDokuKlient["txtSasia"].ToString();
            string kosto = rreshtDokuKlient["txtKostoR"].ToString();
            string kostotot = rreshtDokuKlient["txtKostoTotaleR"].ToString();
            string lloji = rreshtDokuKlient["txtLloji"].ToString();
            string pershkrimprod = rreshtDokuKlient["txtPershkrimProdukti"].ToString();
            string njesia = rreshtDokuKlient["txtNjesiArtikull"].ToString();
            string idart = rreshtDokuKlient["txtIdArtikulliR"].ToString();
            string idburim = rreshtDokuKlient["txtIdBurimi"].ToString();
            string skedulim = rreshtDokuKlient["txtSkedulim"].ToString();
            string detajim = rreshtDokuKlient["txtDetajimi"].ToString();
            string detajimi2 = rreshtDokuKlient["txtDetajimi2t"].ToString();
            if (kodprod != "")
            {
                if (kodprod != null && kodprod != "null" && kodprod != "")
                {
                    this.KodProdukti = kodprod;
                    int.TryParse(idart, out idArtikull);
                    int.TryParse(idburim, out idBurimi);
                    if (mag != null && mag != "null" && mag != "")
                    {
                        DbCore.DbRegjistrim.clsNjesiAdministrative njesiadm = new DbCore.DbRegjistrim.clsNjesiAdministrative(mag, idndermarje);
                        if (njesiadm.IdNjesiAdministrative <= 0)
                            throw new Exception("Nje nga magazinat nuk ekziston!");
                        njesiadm = new clsNjesiAdministrative(mag, idndermarje, idPerdorues);
                        if (njesiadm.IdNjesiAdministrative < 1)
                            throw new Exception("Nuk keni autorizime ne kete magazine!");
                        if (mag != "" && !njesiadm.Aktiv)
                            throw new Exception("Magazina nuk eshte aktive!");
                        this.idMag = njesiadm.IdNjesiAdministrative;
                    }
                    else this.idMag = 0;
                    this.idProdukti = index;
                    this.kodArtikull = kodart;
                    this.lloji = lloji == "Artikull" ? 1 : 2;
                    if (this.lloji == 1)
                    {
                        DbInventari.clsNjesiArtikulli njesi = new DbInventari.clsNjesiArtikulli();
                        njesi.mbushNjesiArtikulliMeKod(njesia, idndermarje);
                        this.njesiArtikull = njesi.IdNjesia;
                        this.idDetajimi =  DbInventari.clsDetajimArtikulli.ktheIdDetajimi(detajim, idndermarje);
                        this.idDetajimi2 = DbInventari.clsDetajimArtikulli.ktheIdDetajimi(detajimi2, idndermarje);
                    }
                    else
                    {
                        switch (njesia)
                        {
                            case "sec":
                                this.njesiArtikull = 1;
                                break;
                            case "min":
                                this.njesiArtikull =2;
                                break;
                            case "ore":
                                this.njesiArtikull = 3;
                                break;
                            case "dite":
                                this.njesiArtikull = 4;
                                break;
                        }
                        this.idDetajimi = 0;
                        this.idDetajimi2 = 0;
                    }
                    this.pershkrimArtikull = pershkrimart;
                    this.pershkrimProdukti = pershkrimprod;

                    if (firo != null && firo != "null" && firo != "")
                        this.firoPerqindje = double.Parse(firo);
                    if (kosto != null && kosto != "null" && kosto != "")
                        this.kosto = double.Parse(kosto);
                    if (kostotot != null && kostotot != "null" && kostotot != "")
                        this.kostoTotale = double.Parse(kostotot);
                    if (sasiakt != null && sasiakt != "null" && sasiakt != "")
                        this.sasiaAktuale = double.Parse(sasiakt);
                    if (sasipor != null && sasipor != "null" && sasipor != "")
                        this.sasia = double.Parse(sasipor);
                    if (scrap != null && scrap != "null" && scrap != "")
                        this.scrap = double.Parse(scrap);
                    this.skedulim = skedulim;
                }
            }
        }

        public clsRecepturaProdhimi(DataRow rreshti)
        {
            
            mbushRecepture(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e produktit.
        /// </summary>
        public int IdProdukti
        {
            get { return idProdukti; }
            set { idProdukti = value; }
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
        /// Kthen/Vendos ID-ne e burimit.
        /// </summary>
        public int IdBurimi
        {
            get { return idBurimi; }
            set { idBurimi = value; }
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
        /// Kthen/Vendos ID-ne e magazines.
        /// </summary>
        public int IdMag
        {
            get { return idMag; }
            set { idMag = value; }
        }

        /// <summary>
        /// scrapi
        /// </summary>
        public double Scrap
        {
            get { return scrap; }
            set { scrap = value; }
        }

        /// <summary>
        /// kostoja e artikullit/burimit
        /// </summary>
        public double Kosto
        {
            get { return kosto; }
            set { kosto = value; }
        }

        /// <summary>
        /// lloji 1-artikull 2-burim
        /// </summary>
        public int Lloji
        {
            get { return lloji; }
            set { lloji = value; }
        }

        /// <summary>
        /// kosto totale e artikullit/burimit
        /// </summary>
        public double KostoTotale
        {
            get { return kostoTotale; }
            set { kostoTotale = value; }
        }

        /// <summary>
        /// njesia e pare e artikullit
        /// </summary>
        public int NjesiArtikull
        {
            get { return njesiArtikull; }
            set { njesiArtikull = value; }
        }

        /// <summary>
        /// sasia aktuale e receptures
        /// </summary>
        public double SasiaAktuale
        {
            get { return sasiaAktuale; }
            set { sasiaAktuale = value; }
        }

        /// <summary>
        /// kodi i produktit
        /// </summary>
        public string KodProdukti
        {
            get { return kodProdukti; }
            set { kodProdukti = value; }
        }

        /// <summary>
        /// pershkrimi i produktit
        /// </summary>
        public string PershkrimProdukti
        {
            get { return pershkrimProdukti; }
            set { pershkrimProdukti = value; }
        }

        /// <summary>
        /// fushe qe mban id e trupit te skedulimit te prodhimit te ndara me presje ne raste kur receptura burimi merret nga skedulimi 
        /// </summary>
        public string Skedulim
        {
            get { return skedulim; }
            set { skedulim = value; }
        }

        /// <summary>
        /// firo ligjore ne perqindje
        /// </summary>
        public double FiroPerqindje
        {
            get { return firoPerqindje; }
            set { firoPerqindje = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e detajimit 1 te artikullit
        /// </summary>
        public int IdDetajimi
        {
            get { return idDetajimi; }
            set { idDetajimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e detajimit 2 te artikullit
        /// </summary>
        public int IdDetajimi2
        {
            get { return idDetajimi2; }
            set { idDetajimi2 = value; }
        }
        #endregion

        #region Metoda Publike

        public clsMesazh krijoRecepturaProdhimiPerImport(string kodRec, string llojRec, string njesiRec, string magRec, double sasiRec, double sasiPlan, double firoPerq, double firoLigjore, int idNdermarrje, int idPerdorues, DateTime dtDok, string kodProd, bool prodsirecepture)
        {
            if (String.IsNullOrEmpty(llojRec))
                return new clsMesazh(false, "Percaktoni llojin e receptures!");
            if (llojRec.ToLower() == "artikull")
                this.lloji = 1;
            else
                this.lloji = 2;

            if (String.IsNullOrEmpty(magRec))
                return new clsMesazh(false, "Plotesoni magazinen e receptures!");
            else
            {
                clsNjesiAdministrative njesiAdmin = new clsNjesiAdministrative(magRec, idNdermarrje);
                if (njesiAdmin.IdNjesiAdministrative <= 0)
                    return new clsMesazh(false, "Magazina e receptures me kod " + magRec + " nuk ekziston!");
                if (!njesiAdmin.Aktiv)
                    return new clsMesazh(false, "Magazina e receptures me kod " + magRec + " nuk eshte aktive!");
                this.idMag = njesiAdmin.IdNjesiAdministrative;
            }
            if (sasiRec == 0)
                return new clsMesazh(false, "Sasia e receptures nuk mund te jete zero!");

            this.sasiaAktuale = sasiRec;
            this.sasia = sasiPlan;

            if (this.lloji == 1)
            {                
                if (String.IsNullOrEmpty(kodRec))
                    return new clsMesazh(false, "Plotesoni kodin e receptures!");
                DbInventari.clsArtikulli artikull = new DbInventari.clsArtikulli(kodRec, idNdermarrje);
                if (artikull.IdArtikulli <= 0)
                    return new clsMesazh(false, "Artikulli me kod " + kodRec + " nuk ekziston!");
                if (!prodsirecepture && kodProd == kodRec)
                    return new clsMesazh(false, "Nuk lejohet te vendoset vete artikulli si recepture!");
                if (!artikull.Aktiv)
                    return new clsMesazh(false, "Artikulli me kod " + kodRec + " nuk eshte aktiv!");
                this.idArtikull = artikull.IdArtikulli;
                this.kodArtikull = artikull.KodArtikulli;
                this.pershkrimArtikull = artikull.PershkrimArtikulli;
                this.idBurimi = 0;
                clsTrupiMagazina tr = new clsTrupiMagazina();
                //this.Kosto = tr.llogaritCmimMesatar(artikull, this.idMag, dtDok, -1, this.sasiaAktuale);
                this.Kosto = tr.llogaritCmimMesatar(artikull, this.idMag, dtDok, -1, this.sasiaAktuale, idPerdorues);
                if (String.IsNullOrEmpty(njesiRec))
                    return new clsMesazh(false, "Plotesoni njesine e receptures!");
                njesiArtikull = DbInventari.clsNjesiArtikulli.ktheIdNjesiArtikulliSipasArtikulli(njesiRec, idNdermarrje, this.idArtikull);
                if (njesiArtikull <= 0)
                    return new clsMesazh(false, "Njesia e receptures me kod " + njesiRec + " nuk ekziston!");
                if (artikull.Njesi1Artikulli != njesiArtikull)
                    return new clsMesazh(false, "Njesia e receptures me kod " + njesiRec + " nuk eshte njesia e pare e artikullit " + artikull.KodArtikulli + "!");
            }
            if (this.lloji == 2)
            {
                if (String.IsNullOrEmpty(kodRec))
                    return new clsMesazh(false, "Plotesoni kodin e receptures!");
                else
                {
                    DbProdhimi.clsBurime burim = new clsBurime(kodRec, idNdermarrje);
                    if (burim.IdBurimi <= 0)
                        return new clsMesazh(false, "Burimi me kod " + kodRec + " nuk ekziston!");
                    this.idBurimi = burim.IdBurimi;
                    this.idArtikull = 0;
                    this.kodArtikull = burim.Kodi;
                    this.kosto = Convert.ToDouble(burim.KostoPlan);
                }
                switch (njesiRec.ToLower())
                {
                    case "sec":
                        this.njesiArtikull = 1;
                        break;
                    case "min":
                        this.njesiArtikull = 2;
                        break;
                    case "ore":
                        this.njesiArtikull = 3;
                        break;
                    case "dite":
                        this.njesiArtikull = 4;
                        break;
                }
            }
            this.kostoTotale = this.sasiaAktuale * this.kosto;
            return new clsMesazh(true, "Receptura u krijua me sukses!");
        }

        /// <summary>
        /// Ruan objektin e  recepturave ne tabelen perkatese ne databaze
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
        public clsMesazh ruaj()
        {
            clsDatabazeProdhimi data = new clsDatabazeProdhimi();
            int id;
            clsMesazh u_ruajt = data.ruajRecepturaProdhimi(out id, IdProdukti, IdArtikulli, IdBurimi, Sasia, Scrap, Kosto, IdMag, Lloji, SasiaAktuale, NjesiArtikull, FiroPerqindje, idDetajimi, idDetajimi2);
            Id = id;
            data.Dispose();
            return u_ruajt;
        }

        /// <summary>
        /// modifikon objektin e  recepturave ne tabelen perkatese ne databaze
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo</returns>
        public clsMesazh modifiko()
        {
            clsDatabazeProdhimi data = new clsDatabazeProdhimi();
            clsMesazh u_modifikua = data.modifikoRecepturaProdhimi(Id, IdProdukti, IdArtikulli, IdBurimi, Sasia, Scrap, Kosto, IdMag, Lloji, SasiaAktuale, NjesiArtikull, FiroPerqindje, idDetajimi, idDetajimi2);
            data.Dispose();
            return u_modifikua;
        }

        /// <summary>
        /// fshin objektin e  recepturave ne tabelen perkatese ne databaze
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh fshi()
        {
            clsDatabazeProdhimi data = new clsDatabazeProdhimi();
            clsMesazh u_fshi = data.fshiRecepturaProdhimiSipasID(Id);
            data.Dispose();
            return u_fshi;
        }

        /// <summary>
        /// fshin objektet e  recepturen sipas id e produktit ne tabelen perkatese ne databaze
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh fshiSipasProduktit()
        {
            clsDatabazeProdhimi data = new clsDatabazeProdhimi();
            clsMesazh u_fshi = data.fshiRecepturaProdhimiSipasIdProdukti(IdProdukti);
            data.Dispose();
            return u_fshi;
        }

        /// <summary>
        /// Merr objektet e  recepturave sipas idprodukti nga tabela perkatese ne databaze
        /// </summary>
        /// <returns > nje objekt  colRecepturaProdhimi me te gjithe recepturat</returns>
        public colRecepturaProdhimi merrSipasProduktit()
        {
            colRecepturaProdhimi data = new colRecepturaProdhimi();
            //data.mbushRecepturaSipasIdProdukti(IdProdukti, null);
            data.mbushRecepturaSipasIdProdukti(IdProdukti);
            return data;
        }

        /// <summary>
        /// Merr objektet e  recepturave sipas idkoka nga tabela perkatese ne databaze
        /// </summary>
        /// <returns > nje objekt  colRecepturaProdhimi me te gjithe recepturat</returns>
        public colRecepturaProdhimi merrSipasKokes(int idkoka)
        {
            colRecepturaProdhimi data = new colRecepturaProdhimi();
            //data.mbushRecepturaSipasIdKoka(idkoka, null);
            data.mbushRecepturaSipasIdKoka(idkoka);
            return data;
        }

        /// <summary>
        /// Merr objektin e recepturen sipas id nga tabela perkatese ne databaze
        /// </summary>
        /// <returns > nje objekt clsRecepturaProdhimi me recepturen te kerkuar</returns>
        public clsRecepturaProdhimi merriSipasID()
        {
            clsRecepturaProdhimi data = new clsRecepturaProdhimi(Id);
            return data;

        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush recepturen nga databaza
        /// </summary>
        /// <param name="dbDataRow">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushRecepture(DataRow dbDataRow)
        {
            if (dbDataRow != null)
            {
                try
                {
                    int.TryParse(dbDataRow["ID"].ToString(), out id);
                    int.TryParse(dbDataRow["IDPRODUKTI"].ToString(), out idProdukti);
                    int.TryParse(dbDataRow["IDARTIKULLI"].ToString(), out idArtikull);
                    int.TryParse(dbDataRow["IDBURIMI"].ToString(), out idBurimi);
                    int.TryParse(dbDataRow["LLOJI"].ToString(), out lloji);
                    double.TryParse(dbDataRow["SASIA"].ToString(), out sasia);
                    double.TryParse(dbDataRow["SASIAAKT"].ToString(), out sasiaAktuale);
                    double.TryParse(dbDataRow["SCRAP"].ToString(), out scrap);
                    double.TryParse(dbDataRow["FIROPERQ"].ToString(), out firoPerqindje);
                    double.TryParse(dbDataRow["KOSTO"].ToString(), out kosto);
                    double.TryParse(dbDataRow["KOSTOTOTALE"].ToString(), out kostoTotale);
                    int.TryParse(dbDataRow["IDMAG"].ToString(), out idMag);
                    kodArtikull = dbDataRow["KODARTIKULLI"].ToString();
                    pershkrimArtikull = dbDataRow["PERSHKRIMARTIKULLI"].ToString();
                    kodProdukti = dbDataRow["KODPRODUKTI"].ToString();
                    pershkrimProdukti = dbDataRow["PERSHKRIMPRODUKTI"].ToString();
                    int.TryParse(dbDataRow["IDNJESIA"].ToString(), out njesiArtikull);
                    skedulim = dbDataRow["skedulim"].ToString();
                    int.TryParse(dbDataRow["IDDETAJIMI"].ToString(), out idDetajimi);
                    int.TryParse(dbDataRow["IDDETAJIMI2"].ToString(), out idDetajimi2);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception(STR_ERRORGabimGjateMarrjesSeTrupitTePlanifikimitNgaD);
                }
            }
            else
                return false;
        }

        #endregion

    }
}
