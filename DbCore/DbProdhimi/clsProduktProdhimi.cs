using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using DbCore.DbRegjistrim;

namespace DbCore.DbProdhimi
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne  nje produkti prodhimi
    ///  (Te dhenat  merren nga tabela : T_PRODUKTPRODHIMI)
    /// </summary>
    public class clsProduktProdhimi
    {
        /// <summary>
        /// konstante per mesazhin e gabimit te marrjes se te dhenave nga db
        /// </summary>
        private const string STR_ERRORGabimGjateMarrjesSeTrupitTePlanifikimitNgaD = "ERROR: Gabim gjate marrjes se produkteve  nga db-ja";

        #region Attributet
        /// <summary>
        /// id e produktit
        /// </summary>
        private int id;
        /// <summary>
        /// id e kokes
        /// </summary>
        private int idKoka;
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
        /// id e njesise se artikullit
        /// </summary>
        private int idNjesia;
        /// <summary>
        /// sasia e planifikuar
        /// </summary>
        private double sasiaPlanifikuar;
        /// <summary>
        /// id e magazines
        /// </summary>
        private int idMag;
        /// <summary>
        /// sasia Aktuale
        /// </summary>
        private double sasiaAktuale;
        /// <summary>
        /// kostoja e artikullit
        /// </summary>
        private double kosto;
        /// <summary>
        /// kosto totale e artikullit
        /// </summary>
        private double kostoTotale;
        /// <summary>
        /// gjeresia e planifikuar
        /// </summary>
        private double gjeresiPlanifikuar;
        /// <summary>
        /// gjatesia e planifikuar
        /// </summary>
        private double gjatesiPlanifikuar;
        /// <summary>
        /// sasia sipas permasave: gjatesi dhe gjeresi
        /// </summary>
        private double sasiPermase;
        /// <summary>
        /// recepturat e produktit
        /// </summary>
        private colRecepturaProdhimi colReceptura;
        /// <summary>
        /// id e trupit te planifikimit nga ka erdhur produkti
        /// </summary>
        private int idPlanifikim;
        /// <summary>
        /// id e trupit te urdherti te porosise
        /// </summary>
        private int idUrdherPorosi;
        /// <summary>
        /// fusha shenime nga urdher porosia
        /// </summary>
        private string shenime;
        /// <summary>
        /// id e detajimit 1  te artikullit
        /// </summary>
        private int idDetajim1;
        /// <summary>
        /// id e detajimit 2 te artikullit
        /// </summary>
        private int idDetajim2;
        private DataRow rreshti;
        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases me parametra
        /// </summary>
        /// <param name="idArt"> id e artikullit</param>
        /// <param name="idmag">id e magazines</param>
        /// <param name="idkoka">id e kokes</param>
        /// <param name="id">id ritese e produktit</param>
        /// <param name="idnjesia">id e njesise se artikullit</param>
        /// <param name="KodArt">kodi i artikullit</param>
        /// <param name="pershkArt">pershkrimi i artikullit</param>
        /// <param name="sasiakt">sasia aktuale</param>
        /// <param name="kosto">kostoja</param>
        /// <param name="sasplan"> sasia e planifikuar</param>
        public clsProduktProdhimi(int id, int idkoka, int idArt, string KodArt, string pershkArt, int idnjesia, double sasplan, double sasiakt, double kosto, int idmag, double gjeresiplan, double gjatesiplan, double sasiPermase, int idplanifikim, int idurdher, string shenime, int idDetajim1, int idDetajim2)
        {
            this.id = id;
            idKoka = idkoka;
            idArtikull = idArt;
            kodArtikull = KodArt;
            pershkrimArtikull = pershkArt;
            idNjesia = idnjesia;
            sasiaPlanifikuar = sasplan;
            idMag = idmag;
            sasiaAktuale = sasiakt;
            this.kosto = kosto;
            gjeresiPlanifikuar = gjeresiplan;
            gjatesiPlanifikuar = gjatesiplan;
            this.sasiPermase = sasiPermase;
            idPlanifikim = idplanifikim;
            idUrdherPorosi = idurdher;
            this.shenime = shenime;
            this.idDetajim1 = idDetajim1;
            this.idDetajim2 = idDetajim2;
            colReceptura = new colRecepturaProdhimi();
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="id">id e produktit</param>
        public clsProduktProdhimi(int id)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            mbushProdukt(db.ktheProduktProdhimiSipasID(id));
            db.Dispose();
        }

        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsProduktProdhimi()
        {
        }

        /// <summary>
        /// konstruktori me te dhenat nga grida
        /// </summary>
        /// <param name="idndermarje">idndermarje</param>
        /// <param name="rresht">rreshti me te dhena</param>
        public clsProduktProdhimi(int idndermarje, int idPerdorues, Dictionary<string, object> rreshtDokuKlient)
        {
            string kodart = rreshtDokuKlient["txtKodiArtikull"].ToString();

            if (kodart == "")
                return;
            int idkoka = 0;
            int.TryParse(rreshtDokuKlient["txtIdKoka"].ToString(), out idkoka);
            string idart = rreshtDokuKlient["txtIdArtikulli"].ToString();
            string pershkrimart = rreshtDokuKlient["txtPershkrimArtikull"].ToString();
            string njesia = rreshtDokuKlient["txtIdNjesia"].ToString();
            string gjeresi = rreshtDokuKlient["txtGjeresiPlanifikuar"].ToString();
            string gjatesi = rreshtDokuKlient["txtGjatesiPlanifikuar"].ToString();
            string sasiakt = rreshtDokuKlient["txtSasiaAktuale"].ToString();
            string sasipor = rreshtDokuKlient["txtSasiaPlanifikuar"].ToString();
            string kosto = rreshtDokuKlient["txtKosto"].ToString();
            string kostotot = rreshtDokuKlient["txtKostoTotale"].ToString();
            string mag = rreshtDokuKlient["txtIdMag"].ToString();
            string sasipermase = rreshtDokuKlient["txtSasiPermase"].ToString();
            string detajim1 = rreshtDokuKlient["txtDetajimi"].ToString();
            string detajim2 = rreshtDokuKlient["txtDetajimi2t"].ToString();
            int index = 0;
            int.TryParse(rreshtDokuKlient["txtIdUrdherPorosi"].ToString(), out index);
            string idplanifikim = rreshtDokuKlient["txtIdPlanifikim"].ToString();
            int id = 0;
            int.TryParse(rreshtDokuKlient["txtFshi"].ToString(), out id);
            if (kodart != "")
            {
                if (kodart != null && kodart != "null" && kodart != "")
                {
                    idKoka = idkoka;
                    int.TryParse(idart, out idArtikull);
                    kodArtikull = kodart;
                    pershkrimArtikull = pershkrimart;
                    DbInventari.clsNjesiArtikulli njesi = new DbInventari.clsNjesiArtikulli();
                    njesi.mbushNjesiArtikulliMeKod(njesia, idndermarje);
                    idNjesia = njesi.IdNjesia;
                    idDetajim1 = DbInventari.clsDetajimArtikulli.ktheIdDetajimi(detajim1, idndermarje);
                    idDetajim2 = DbInventari.clsDetajimArtikulli.ktheIdDetajimi(detajim2, idndermarje);

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
                    int.TryParse(idplanifikim, out idPlanifikim);
                    this.idUrdherPorosi = index;
                    if (gjeresi != null && gjeresi != "null" && gjeresi != "")
                        this.gjeresiPlanifikuar = double.Parse(gjeresi);
                    if (kosto != null && kosto != "null" && kosto != "")
                        this.kosto = double.Parse(kosto);
                    if (kostotot != null && kostotot != "null" && kostotot != "")
                        this.kostoTotale = double.Parse(kostotot);
                    if (sasiakt != null && sasiakt != "null" && sasiakt != "")
                        this.sasiaAktuale = double.Parse(sasiakt);
                    if (sasipor != null && sasipor != "null" && sasipor != "")
                        this.sasiaPlanifikuar = double.Parse(sasipor);
                    if (gjatesi != null && gjatesi != "null" && gjatesi != "")
                        this.gjatesiPlanifikuar = double.Parse(gjatesi);
                    if (sasipermase != null && sasipermase != "null" && sasipermase != "")
                        this.sasiPermase = double.Parse(sasipermase);

                    this.id = id;
                }
            }
        }

        public clsProduktProdhimi(DataRow rreshti)
        {
            
            mbushProdukt(rreshti);
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
        /// Kthen/Vendos ID-ne e kokes se dokumentit.
        /// </summary>
        public int IdKoka
        {
            get { return idKoka; }
            set { idKoka = value; }
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
        /// id e trupit te urdherti te porosise
        /// </summary>
        public int IdUrdherPorosi
        {
            get { return idUrdherPorosi; }
            set { idUrdherPorosi = value; }
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
        /// Kthen/Vendos sasia e planifikuar e artikullit.
        /// </summary>
        public double SasiaPlanifikuar
        {
            get { return sasiaPlanifikuar; }
            set { sasiaPlanifikuar = value; }
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
        /// sasia aktuale e artikullit
        /// </summary>
        public double SasiaAktuale
        {
            get { return sasiaAktuale; }
            set { sasiaAktuale = value; }
        }

        /// <summary>
        /// gjeresia e planifikuar
        /// </summary>
        public double GjeresiPlanifikuar
        {
            get { return gjeresiPlanifikuar; }
            set { gjeresiPlanifikuar = value; }
        }
        /// <summary>
        /// gjatesia e planifikuar
        /// </summary>
        public double GjatesiPlanifikuar
        {
            get { return gjatesiPlanifikuar; }
            set { gjatesiPlanifikuar = value; }
        }

        /// <summary>
        /// kostoja e artikullit
        /// </summary>
        public double Kosto
        {
            get { return kosto; }
            set { kosto = value; }
        }

        /// <summary>
        /// kosto totale e artikullit
        /// </summary>
        public double KostoTotale
        {
            get { return kostoTotale; }
            set { kostoTotale = value; }
        }

        /// <summary>
        /// id e trupit te planifikimit nga ka erdhur produkti
        /// </summary>
        public int IdPlanifikim
        {
            get { return idPlanifikim; }
            set { idPlanifikim = value; }
        }

        /// <summary>
        /// recepturat e produktit
        /// </summary>
        public colRecepturaProdhimi ColReceptura
        {
            get { return colReceptura; }
            set { colReceptura = value; }
        }

        /// <summary>
        /// Kthen/Vendos sasine sipas permasave
        /// </summary>
        public double SasiPermase
        {
            get { return sasiPermase; }
            set { sasiPermase = value; }
        }

        /// <summary>
        /// fusha shenime nga urdher porosia
        /// </summary>
        public string Shenime
        {
            get { return shenime; }
            set { shenime = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e detajimit 1 te artikullit
        /// </summary>
        public int IdDetajim1
        {
            get { return idDetajim1; }
            set { idDetajim1 = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e detajimit 2 te artikullit
        /// </summary>
        public int IdDetajim2
        {
            get { return idDetajim2; }
            set { idDetajim2 = value; }
        }
        #endregion

        #region Metoda Publike

        public clsMesazh krijoProduktProdhimiPerImport(string kodProd, string njesiProd, string magProd, double sasiProd, double sasiPlan, double gjeresiPlan, double gjatesiPlan, double sasiPermase, int idPerdorues, int idNdermarrje, colRecepturaProdhimi colRec)
        {
            if (String.IsNullOrEmpty(kodProd))
                return new clsMesazh(false, "Plotesoni kodin e produktit!");
            else
            { 
                DbInventari.clsArtikulli artikull = new DbInventari.clsArtikulli(kodProd, idNdermarrje);
                if (artikull.IdArtikulli <= 0)
                    return new clsMesazh(false, "Artikulli me kod " + kodProd + " nuk ekziston!");
                if (!artikull.Aktiv)
                    return new clsMesazh(false, "Artikulli me kod " + kodProd + " nuk eshte aktiv!");
                idArtikull = artikull.IdArtikulli;
                kodArtikull = artikull.KodArtikulli;
                pershkrimArtikull = artikull.PershkrimArtikulli;
            }
            if (String.IsNullOrEmpty(njesiProd))
                return new clsMesazh(false, "Plotesoni njesine e produktit!");
            else
            {
                idNjesia = DbInventari.clsNjesiArtikulli.ktheIdNjesiArtikulliSipasArtikulli(njesiProd, idNdermarrje, this.idArtikull);
                if (idNjesia <= 0)
                    return new clsMesazh(false, "Njesia e produktit me kod " + njesiProd + " nuk ekziston, ose nuk eshte e lidhur me produktin " + kodArtikull + "!");
            }
            if (String.IsNullOrEmpty(magProd))
                return new clsMesazh(false, "Plotesoni magazinen e produktit!");
            else
            {
                clsNjesiAdministrative njesiAdmin = new clsNjesiAdministrative(magProd, idNdermarrje);
                if (njesiAdmin.IdNjesiAdministrative <= 0)
                    return new clsMesazh(false, "Magazina e produktit me kod " + magProd + " nuk ekziston!");
                if (!njesiAdmin.Aktiv)
                    return new clsMesazh(false, "Magazina e produktit me kod " + magProd + " nuk eshte aktive!");
                idMag = njesiAdmin.IdNjesiAdministrative;
            }
            this.gjeresiPlanifikuar = gjeresiPlan;
            this.gjatesiPlanifikuar = gjatesiPlan;
            this.sasiPermase = sasiPermase;
            if (sasiProd == 0)
                return new clsMesazh(false, "Sasia e produktit nuk mund te jete zero!");
            this.sasiaAktuale = sasiProd;
            this.sasiaPlanifikuar = sasiPlan;
            if (colRec.Count == 0)
                return new clsMesazh(false, "Produkti " + kodProd + " nuk ka asnje recepture!");
            this.colReceptura = colRec;
            double kostoTotaleProd = 0;
            foreach (clsRecepturaProdhimi rec in colRec)
            {
                kostoTotaleProd += rec.KostoTotale;
            }
            this.kostoTotale = kostoTotaleProd;
            this.kosto = kostoTotaleProd / sasiProd;
            return new clsMesazh(true, "Produkti i prodhimit u krijua me sukses!");
        }

        /// <summary>
        /// Ruan objektin e  produkt ne tabelen perkatese ne databaze
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
        public clsMesazh ruaj()
        {
            clsDatabazeProdhimi data = new clsDatabazeProdhimi();
            int id;
            clsMesazh u_ruajt = data.ruajProduktProdhimi(out id, IdKoka, IdArtikulli, IdNjesia, SasiaPlanifikuar, SasiaAktuale, Kosto, IdMag, gjeresiPlanifikuar, gjatesiPlanifikuar, sasiPermase, idPlanifikim, idUrdherPorosi, idDetajim1, idDetajim2);
            Id = id;
            data.Dispose();
            return u_ruajt;
        }

        /// <summary>
        /// modifikon objektin e  produktit ne tabelen perkatese ne databaze
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo</returns>
        public clsMesazh modifiko()
        {
            clsDatabazeProdhimi data = new clsDatabazeProdhimi();
            clsMesazh u_modifikua = data.modifikoProduktProdhimi(Id, IdKoka, IdArtikulli, IdNjesia, SasiaPlanifikuar, SasiaAktuale, Kosto, IdMag, gjeresiPlanifikuar, gjatesiPlanifikuar, sasiPermase, idPlanifikim, idUrdherPorosi, idDetajim1, idDetajim2);
            data.Dispose();
            return u_modifikua;
        }

        /// <summary>
        /// fshin objektin e  produktit ne tabelen perkatese ne databaze
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh fshi()
        {
            clsDatabazeProdhimi data = new clsDatabazeProdhimi();
            clsMesazh u_fshi = data.fshiProduktProdhimiSipasID(Id);
            data.Dispose();
            return u_fshi;
        }

        /// <summary>
        /// fshin objektet e  produktin sipas id e koka ne tabelen perkatese ne databaze
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh fshiSipasKoka()
        {
            clsDatabazeProdhimi data = new clsDatabazeProdhimi();
            clsMesazh u_fshi = data.fshiProduktProdhimiSipasKoka(IdKoka);
            data.Dispose();
            return u_fshi;
        }

        /// <summary>
        /// Merr objektet e  produktet sipas idkoka nga tabela perkatese ne databaze
        /// </summary>
        /// <returns > nje objekt  colProduktProdhimi me te gjithe produktet</returns>
        public colProduktProdhimi merrSipasKokes()
        {
            colProduktProdhimi data = new colProduktProdhimi();
            //data.mbushProduktSipasIdkoka(IdKoka, null);
            data.mbushProduktSipasIdkoka(IdKoka);
            return data;
        }

        /// <summary>
        /// Merr objektin e produktit sipas id nga tabela perkatese ne databaze
        /// </summary>
        /// <returns > nje objekt clsProduktProdhimi me produktin te kerkuar</returns>
        public clsProduktProdhimi merriSipasID()
        {
            clsProduktProdhimi data = new clsProduktProdhimi(Id);
            return data;

        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush produktin nga databaza
        /// </summary>
        /// <param name="dbDataRow">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushProdukt(DataRow dbDataRow)
        {
            if (dbDataRow != null)
            {
                try
                {
                    int.TryParse(dbDataRow["ID"].ToString(), out id);
                    int.TryParse(dbDataRow["IDKOKA"].ToString(), out idKoka);
                    int.TryParse(dbDataRow["IDARTIKULLI"].ToString(), out idArtikull);
                    int.TryParse(dbDataRow["IDNJESIA"].ToString(), out idNjesia);
                    double.TryParse(dbDataRow["SASIAPLAN"].ToString(), out sasiaPlanifikuar);
                    double.TryParse(dbDataRow["SASIAAKT"].ToString(), out sasiaAktuale);
                    double.TryParse(dbDataRow["GJERESIPLAN"].ToString(), out gjeresiPlanifikuar);
                    double.TryParse(dbDataRow["GJATESIPLAN"].ToString(), out gjatesiPlanifikuar);
                    double.TryParse(dbDataRow["KOSTO"].ToString(), out kosto);
                    double.TryParse(dbDataRow["KOSTOTOTALE"].ToString(), out kostoTotale);
                    int.TryParse(dbDataRow["IDMAG"].ToString(), out idMag);
                    int.TryParse(dbDataRow["IDPLANIFIKIM"].ToString(), out idPlanifikim);
                    int.TryParse(dbDataRow["IDURDHERPOROSI"].ToString(), out idUrdherPorosi);
                    int.TryParse(dbDataRow["IDDETAJIM1"].ToString(), out idDetajim1);
                    int.TryParse(dbDataRow["IDDETAJIM2"].ToString(), out idDetajim2);
                    kodArtikull = dbDataRow["KODARTIKULLI"].ToString();
                    pershkrimArtikull = dbDataRow["PERSHKRIMARTIKULLI"].ToString();
                    double.TryParse(dbDataRow["SASIPERMASE"].ToString(), out sasiPermase);
                    ColReceptura = new colRecepturaProdhimi();
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