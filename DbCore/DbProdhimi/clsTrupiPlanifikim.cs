using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using DbCore.DbInventari;

namespace DbCore.DbProdhimi
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne  trupin e nje dokumenti planifikimi
    ///  (Te dhenat  merren nga tabela : T_TRUPIPLANIFIKIM)
    /// </summary>
    public class clsTrupiPlanifikim
    {
        /// <summary>
        /// konstante per mesazhin e gabimit te marrjes se te dhenave nga db
        /// </summary>
        private const string STR_ERRORGabimGjateMarrjesSeTrupitTePlanifikimitNgaD = "ERROR: Gabim gjate marrjes se trupit te planifikimit nga db-ja";

        #region Attributet
        /// <summary>
        /// id e trupit te planifikimit
        /// </summary>
        private int idTrupiPlanifikim;
        /// <summary>
        /// id e kokes se planifikimit
        /// </summary>
        private int idKokaPlanifikim;
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
        /// sasia
        /// </summary>
        private double sasia;
        /// <summary>
        /// id e magazines
        /// </summary>
        private int idMag;
        /// <summary>
        /// sasia e mbetur pas ekzekutimeve
        /// </summary>
        private double sasimbetur;
        /// <summary>
        /// gjeresia 
        /// </summary>
        private double gjeresi;
        /// <summary>
        /// gjatesia
        /// </summary>
        private double gjatesi;
        /// <summary>
        /// sasia sipas permasave
        /// </summary>
        private double sasiPermase;
        /// <summary>
        /// id e trupit te rreshtit te urdher porosise nga e cila eshte krijuar
        /// </summary>
        private int idUrdherPorosi;
        /// <summary>
        /// shenime per cdo rresht te trupi. nqs vjen nga shitja do marr te shitjes
        /// </summary>
        private string shenime;
        /// <summary>
        /// id detajimi 1 i artikullit
        /// </summary>
        private int detajim1;
        /// <summary>
        /// id detajimi 2 i artikullit
        /// </summary>
        private int detajim2;

        private DataRow rreshti;
        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases me parametra
        /// </summary>
        /// <param name="idArt"> id e artikullit</param>
        /// <param name="idmag">id e magazines</param>
        /// <param name="idkoka">id e kokes se dokumentit te planifikimit</param>
        /// <param name="idtrupi">id ritese e trupit te dokumentit te planifikimit</param>
        /// <param name="idNjes">id e njesise se artikullit</param>
        /// <param name="KodArt">kodi i artikullit</param>
        /// <param name="pershkArt">pershkrimi i artikullit</param>
        /// <param name="sas"> sasia e artikullit</param>
        public clsTrupiPlanifikim(int idtrupi, int idkoka, int idArt, string KodArt, string pershkArt, int idNjes, double sas, int idmag, double gjeresi, double gjatesi, double sasiPermase, int idurdherporosi, string shenime, int detajim1, int detajim2)
        {
            idTrupiPlanifikim = idtrupi;
            idKokaPlanifikim = idkoka;
            idArtikull = idArt;
            kodArtikull = KodArt;
            pershkrimArtikull = pershkArt;
            idNjesia = idNjes;
            sasia = sas;
            idMag = idmag;
            this.gjatesi = gjatesi;
            this.gjeresi = gjeresi;
            this.sasiPermase = sasiPermase;
            this.idUrdherPorosi = idurdherporosi;
            this.shenime = shenime;
            this.detajim1 = detajim1;
            this.detajim2 = detajim2;
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idTrupiPlanifikim">id e trupit</param>
        /// <param name="db">clsDatabaseprodhimi per raste transaksioni</param>
        public clsTrupiPlanifikim(int idTrupiPlanifikim, int idndermarje)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            mbushTrupPlanifikim(db.ktheTrupiPlanifikimSipasID(idTrupiPlanifikim, idndermarje));
            db.Dispose();
        }

        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsTrupiPlanifikim()
        {
        }


        public clsTrupiPlanifikim(int idndermarje, int idperdorues, Dictionary<string, object> rresht)
        {
            if (rresht["IdNjesia"]!=null) IdNjesia = int.Parse(rresht["IdNjesia"].ToString());
            if (rresht["Sasia"] != null) Sasia = double.Parse(rresht["Sasia"].ToString());
            if (rresht["Shenime"] != null) Shenime = rresht["Shenime"].ToString();
            if (rresht["Detajim1"] != null) Detajim1 = int.Parse(rresht["Detajim1"].ToString());
            if (rresht["Detajim2"] != null) Detajim2 = int.Parse(rresht["Detajim2"].ToString());
            if (rresht["Gjeresi"] != null) Gjeresi = double.Parse(rresht["Gjeresi"].ToString());
            if (rresht["Gjatesi"] != null) Gjatesi = double.Parse(rresht["Gjatesi"].ToString());
            if (rresht["IdMag"] != null ) IdMag = int.Parse(rresht["IdMag"].ToString());
            if (rresht["SasiPermase"] != null) SasiPermase = double.Parse(rresht["SasiPermase"].ToString());
            if (rresht["IdTrupiPlanifikim"] != null) IdTrupiPlanifikim = int.Parse(rresht["IdTrupiPlanifikim"].ToString());
            if (rresht["IdUrdherPorosi"] != null) IdUrdherPorosi = int.Parse(rresht["IdUrdherPorosi"].ToString());
            clsArtikulli art = new clsArtikulli();
            if (rresht["KodiArtikull"].ToString() != "")
            {
                if (!clsArtikulli.ekziston(rresht["KodiArtikull"].ToString(), idndermarje))
                {
                    throw new Exception("Nje nga artikujt nuk ekziston!");
                }
                art.ktheArtikullSipasKoditDheAutorizime(rresht["KodiArtikull"].ToString(), idndermarje, idperdorues);
                if (art.IdArtikulli == 0)
                {
                    throw new Exception("Nuk keni autorizime per artikullin " + rresht["KodiArtikull"].ToString() + "!");
                }
                if (!art.Aktiv)
                {
                    throw new Exception("Artikulli me kod:" + art.KodArtikulli + " nuk eshte aktiv!");
                }
                art.merrSipasKodArtikullit(rresht["KodiArtikull"].ToString(), idndermarje);
                IdArtikulli = art.IdArtikulli;
                PershkrimArtikull = art.PershkrimArtikulli;
                KodiArtikull = art.KodArtikulli;
                if (rresht["KodDetajim1"].ToString() != "" && Detajim1 == 0)
                {
                    clsDetajimArtikulli detajim1 = new clsDetajimArtikulli(rresht["KodDetajim1"].ToString(), idndermarje);
                    Detajim1 = detajim1.IdDetajimArtikulli;
                }
                if (rresht["KodDetajim2"].ToString() != "" && Detajim2 == 0)
                {
                    clsDetajimArtikulli detajim2 = new clsDetajimArtikulli(rresht["KodDetajim2"].ToString(), idndermarje);
                    Detajim2 = detajim2.IdDetajimArtikulli;
                }
            }
        }

        public clsTrupiPlanifikim(DataRow rreshti)
        {
            
            mbushTrupPlanifikim(rreshti);
        }
        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdTrupiPlanifikim
        {
            get { return idTrupiPlanifikim; }
            set { idTrupiPlanifikim = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e kokes se dokumentit te planifikimit.
        /// </summary>
        public int IdKokaPlanifikim
        {
            get { return idKokaPlanifikim; }
            set { idKokaPlanifikim = value; }
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
        /// id e trupit te rreshtit te urdher porosise nga e cila eshte krijuar
        /// </summary>
        public int IdUrdherPorosi
        {
            get
            {
                return idUrdherPorosi;
            }
            set
            {
                idUrdherPorosi = value;
            }
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
        /// sasia e mbetur pas ekzekutimeve
        /// </summary>
        public double Sasimbetur
        {
            get
            {
                return sasimbetur;
            }
            set
            {
                sasimbetur = value;
            }
        }
        /// <summary>
        /// gjeresia 
        /// </summary>
        public double Gjeresi
        {
            get
            {
                return gjeresi;
            }
            set
            {
                gjeresi = value;
            }
        }
        /// <summary>
        /// gjetesia
        /// </summary>
        public double Gjatesi
        {
            get
            {
                return gjatesi;
            }
            set
            {
                gjatesi = value;
            }
        }

        /// <summary>
        /// sasia sipas permasave
        /// </summary>
        public double SasiPermase
        {
            get
            {
                return sasiPermase;
            }
            set
            {
                sasiPermase = value;
            }
        }
        /// <summary>
        /// shenime per cdo rresht te trupi. nqs vjen nga shitja do marr te shitjes
        /// </summary>
        public string Shenime
        {
            get
            {
                return shenime;
            }
            set
            {
                shenime = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos id detajim1
        /// </summary>
        public int Detajim1
        {
            get { return detajim1; }
            set { detajim1 = value; }
        }
        /// <summary>
        /// Kthen/Vendos id detajim2
        /// </summary>
        public int Detajim2
        {
            get { return detajim2; }
            set { detajim2 = value; }
        }

        #endregion

        #region Metoda Publike


        /// <summary>
        /// Ruan objektin e  trupit te dokumentit te planifikimit ne tabelen perkatese ne databaze
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
        public clsMesazh ruaj()
        {
            clsDatabazeProdhimi data = new clsDatabazeProdhimi();
            int id;
            clsMesazh u_ruajt = data.ruajTrupiPlanifikim(out id, IdKokaPlanifikim, IdArtikulli, IdNjesia, Sasia, IdMag, Gjeresi, Gjatesi, sasiPermase, idUrdherPorosi, shenime, Detajim1, Detajim2);
            IdTrupiPlanifikim = id;
            data.Dispose();
            return u_ruajt;
        }

        /// <summary>
        /// modifikon objektin e  trupit te dokumentit te planifikimit ne tabelen perkatese ne databaze
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo</returns>
        public clsMesazh modifiko()
        {
            clsDatabazeProdhimi data = new clsDatabazeProdhimi();
            clsMesazh u_modifikua = data.modifikoTrupiPlanifikim(IdTrupiPlanifikim, IdKokaPlanifikim, IdArtikulli, IdNjesia, Sasia, IdMag, Gjeresi, Gjatesi, sasiPermase, idUrdherPorosi, shenime, Detajim1, Detajim2);
            data.Dispose();
            return u_modifikua;
        }

        /// <summary>
        /// fshin objektin e  trupit te dokumentit te planifikimit ne tabelen perkatese ne databaze
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh fshi()
        {
            clsDatabazeProdhimi data = new clsDatabazeProdhimi();
            clsMesazh u_fshi = data.fshiTrupiPlanifikimSipasID(IdTrupiPlanifikim);
            data.Dispose();
            return u_fshi;
        }

        /// <summary>
        /// fshin objektet e  trupit te dokumentit te planifikimit sipas id se kokes ne tabelen perkatese ne databaze
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh fshiSipasKoka()
        {
            clsDatabazeProdhimi data = new clsDatabazeProdhimi();
            clsMesazh u_fshi = data.fshiTrupiPlanifikimSipasKoka(IdKokaPlanifikim);
            data.Dispose();
            return u_fshi;
        }

        /// <summary>
        /// Merr objektet e  trupit te dokumentit te planifikimit sipas kokes nga tabela perkatese ne databaze
        /// </summary>
        /// <returns > nje objekt  colTrupiPlanifikim me te gjithe trupat e nje dokumenti</returns>
        public colTrupiPlanifikim merriSipasKoka(int idndermarje)
        {
            colTrupiPlanifikim data = new colTrupiPlanifikim();
            //data.mbushTrupiPlanifikimi(IdKokaPlanifikim,idndermarje, null);
            data.mbushTrupiPlanifikimi(IdKokaPlanifikim, idndermarje);
            return data;
        }

        /// <summary>
        /// Merr objektin e  trupit te dokumentit te planifikimit sipas id nga tabela perkatese ne databaze
        /// </summary>
        /// <returns > nje objekt clsTrupiPlanifikim me trupin e dokumentit te planifikimit te kerkuar</returns>
        public clsTrupiPlanifikim merriSipasID(int idndermarje)
        {
            clsTrupiPlanifikim data = new clsTrupiPlanifikim(IdTrupiPlanifikim, idndermarje);
            return data;

        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush trupin e planifikimit nga databaza
        /// </summary>
        /// <param name="dbDataRow">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushTrupPlanifikim(DataRow dbDataRow)
        {
            if (dbDataRow != null)
            {
                try
                {
                    int.TryParse(dbDataRow["IDTRUPIPLANIFIKIM"].ToString(), out idTrupiPlanifikim);
                    int.TryParse(dbDataRow["IDKOKAPLANIFIKIM"].ToString(), out idKokaPlanifikim);
                    int.TryParse(dbDataRow["IDARTIKULLI"].ToString(), out idArtikull);
                    int.TryParse(dbDataRow["IDNJESIA"].ToString(), out idNjesia);
                    double.TryParse(dbDataRow["SASIA"].ToString(), out sasia);
                    double.TryParse(dbDataRow["GJERESI"].ToString(), out gjeresi);
                    double.TryParse(dbDataRow["GJATESI"].ToString(), out gjatesi);
                    int.TryParse(dbDataRow["IDMAG"].ToString(), out idMag);
                    int.TryParse(dbDataRow["IDURDHERPOROSI"].ToString(), out idUrdherPorosi);
                    kodArtikull = dbDataRow["KODARTIKULLI"].ToString(); 
                    shenime = dbDataRow["SHENIME"].ToString();
                    pershkrimArtikull = dbDataRow["PERSHKRIMARTIKULLI"].ToString();
                    double.TryParse(dbDataRow["SASIMBETUR"].ToString(), out sasimbetur);
                    double.TryParse(dbDataRow["SASIPERMASE"].ToString(), out sasiPermase);
                    int.TryParse(dbDataRow["DETAJIM1"].ToString(), out detajim1);
                    int.TryParse(dbDataRow["DETAJIM2"].ToString(), out detajim2);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new MyException("Gabim gjate cast-it!");
                }
                catch (Exception)
                {
                    throw new MyException(STR_ERRORGabimGjateMarrjesSeTrupitTePlanifikimitNgaD);
                }
            }
            else
                return false;
        }

        #endregion

    }
}
