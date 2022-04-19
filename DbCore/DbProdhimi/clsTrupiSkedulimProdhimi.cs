using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;


namespace DbCore.DbProdhimi
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne  trupin e nje dokumenti skedulim prodhimi
    ///  (Te dhenat  merren nga tabela : T_TRUPISKEDULIMPRODHIMI)
    /// </summary>
    public class clsTrupiSkedulimProdhimi
    {
        /// <summary>
        /// konstante per mesazhin e gabimit te marrjes se te dhenave nga db
        /// </summary>
        private const string STR_ERRORGabimGjateMarrjesSeTrupitTePlanifikimitNgaD = "ERROR: Gabim gjate marrjes se trupit te skedulimit nga db-ja";

        #region Attributet
        /// <summary>
        /// id e trupit te skedulimi
        /// </summary>
        private int idTrupi;
        /// <summary>
        /// id e kokes se skedulimit
        /// </summary>
        private int idKoka;
        /// <summary>
        /// id e artikullit
        /// </summary>
        private int idProdukti;
        /// <summary>
        /// kodi i artikullit
        /// </summary>
        private string kodArtikull;
        /// <summary>
        /// pershkrimi i artikullit
        /// </summary>
        private string pershkrimArtikull;
        /// <summary>
        /// id e njesise se burimit
        /// </summary>
        private int njesia;
        /// <summary>
        /// kosto
        /// </summary>
        private decimal kosto;
        /// <summary>
        /// id e burimit
        /// </summary>
        private int idBurimi;
        /// <summary>
        /// kosto totale
        /// </summary>
        private decimal kostoTotale;
        /// <summary>
        /// koha 
        /// </summary>
        private decimal koha;

        private int idPlanifikimi;
        private int idAktiviteti;
        private DateTime data;
        private DateTime nga;
        private DateTime ne;
        private string shenime;
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
        public clsTrupiSkedulimProdhimi(int idtrupi, int idkoka,int idburimi, int idplanifikimi, int idaktiviteti,int idprodukti,DateTime data, DateTime nga, DateTime ne, decimal koha, int njesia, decimal kosto, decimal kostototale, string shenime)
        {
            idTrupi = idtrupi;
            idKoka = idkoka;
            idProdukti = idprodukti;
            this.njesia = njesia;
           this. kosto = kosto;
            idBurimi = idburimi;
            this.kostoTotale= kostototale;
            this.koha = koha;
            this.idPlanifikimi = idplanifikimi;
            this.idAktiviteti = idaktiviteti;
            this.data = data;
            this.nga = nga;
            this.ne = ne;
            this.shenime = shenime;

        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idTrupi">id e trupit</param>
        /// <param name="db">clsDatabaseprodhimi per raste transaksioni</param>
        public clsTrupiSkedulimProdhimi(int idTrupi, int idndermarje)
        {
            clsDatabazeProdhimi db = new clsDatabazeProdhimi();
            mbushTrupSkedulim(db.ktheTrupiSkedulimProdhimiSipasID(idTrupi, idndermarje));
            db.Dispose();
        }

        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsTrupiSkedulimProdhimi()
        {
        }


        public clsTrupiSkedulimProdhimi(int idndermarje, Dictionary<string, object> rresht)
        {
            string burimi = rresht["txtBurimi"].ToString();
            string pershkrimburimi= rresht["txtEmertimiB"].ToString();
            string projekti = rresht["txtProjekti"].ToString();
            string aktiviteti = rresht["txtAktiviteti"].ToString();
            string produkti = rresht["txtProdukti"].ToString();
            string pershkrimprodukti = rresht["txtEmertimiA"].ToString();
            string data = rresht["txtData"].ToString();
            string nga = rresht["txtNga"].ToString();
            string ne = rresht["txtNe"].ToString();
            string koha = rresht["txtKoha"].ToString();
            string njesia = rresht["txtNjesia"].ToString();
            string shenime = rresht["txtShenime"].ToString();
            string kosto = rresht["txtKosto"].ToString();
            string kostototale = rresht["txtKostoTotale"].ToString();
            string idburimi = rresht["txtIdKodi"].ToString();
            string idprojekti = rresht["txtIdProjekti"].ToString();
           
            DbCore.DbProdhimi.clsBurime bur = new  clsBurime ();
            if (burimi != "" && projekti != ""  && produkti != "" && koha!="0" && koha!="")
            {
                if (!DbCore.DbProdhimi.clsBurime.ekzistonBurim(burimi, idndermarje))
                {
                    throw new Exception("Burimi " + burimi + " nuk ekziston!");
                }
                bur = new clsBurime(burimi, idndermarje);

                if (!bur.Aktiv)
                {
                    throw new Exception("Burimi me kod:" + bur.Kodi + " nuk eshte aktiv!");
                }
                idBurimi = bur.IdBurimi;


                if (projekti != "")
                {
                    if (idprojekti == "0")
                    {
                        throw new Exception("Projekti " + projekti + " nuk ekziston!");
                    }
                    idPlanifikimi = int.Parse(idprojekti);
                }
                if (aktiviteti != "")
                {
                    if (!clsAktiviteteKoka.ekzistonAktivitet(aktiviteti, idndermarje))
                        throw new Exception("Aktiviteti " + aktiviteti + " nuk ekziston!");
                    clsAktiviteteKoka aktivitet = new clsAktiviteteKoka(aktiviteti, idndermarje);
                    idAktiviteti = aktivitet.IdKoka;
                }
                if (produkti != "")
                {
                    
                    DbInventari.clsArtikulli prod = new DbInventari.clsArtikulli(produkti, idndermarje);
                    if (prod.IdArtikulli < 1)
                        throw new Exception("Artikulli " + produkti + " nuk ekziston!");
                    if (!prod.Aktiv)
                        throw new Exception("Artikulli " + produkti + " nuk eshte aktiv!");
                    idProdukti = prod.IdArtikulli;
                    kodArtikull = prod.KodArtikulli;
                    pershkrimArtikull = prod.PershkrimArtikulli;
                }
                this.data = DateTime.Parse(data);
                this.nga = DateTime.Parse(nga);
                this.ne = DateTime.Parse(ne);
                this.koha = decimal.Parse(koha);
                switch (njesia)
                {
                    case "Sec":
                        this.njesia = 1;
                        break;
                    case "Min":
                        this.njesia = 2;
                        break;
                    case "Ore":
                        this.njesia = 3;
                        break;
                    case "Dite":
                        this.njesia = 4;
                        break;
                    default:
                        this.njesia = 0;
                        break;
                }
                this.shenime = shenime;
                this.kosto = decimal.Parse(kosto);
                this.kostoTotale = decimal.Parse(kostototale);
            }
            else idBurimi = -1;
        }

        public clsTrupiSkedulimProdhimi(DataRow rreshti)
        {
            
            mbushTrupSkedulim(rreshti);
        }
        #endregion

        #region Properties
        /// <summary>
        /// kthen daten
        /// </summary>
        public DateTime Data
        {
            get
            {
                return data;
            }
            set
            {
                data = value;
            }
        }
        /// <summary>
        /// kthen vendos id e aktivitetit
        /// </summary>
        public int IdAktiviteti
        {
            get
            {
                return idAktiviteti;
            }
            set
            {
                idAktiviteti = value;
            }
        }
        /// <summary>
        /// kthen vendos id e planifikimit
        /// </summary>
        public int IdPlanifikimi
        {
            get
            {
                return idPlanifikimi;
            }
            set
            {
                idPlanifikimi = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdTrupi
        {
            get { return idTrupi; }
            set { idTrupi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e kokes se dokumentit te skedulimit.
        /// </summary>
        public int IdKoka
        {
            get { return idKoka; }
            set { idKoka = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e artikullit .
        /// </summary>
        public int IdProdukti
        {
            get { return idProdukti; }
            set { idProdukti = value; }
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
        /// koha 
        /// </summary>
        public decimal Koha
        {
            get
            {
                return koha;
            }
            set
            {
                koha = value;
            }
        }
        /// <summary>
        /// kthen vendos oren kur mbaron 
        /// </summary>
        public DateTime Ne
        {
            get
            {
                return ne;
            }
            set
            {
                ne = value;
            }
        }
        /// <summary>
        /// kthen vendos oren kur fillon
        /// </summary>
        public DateTime Nga
        {
            get
            {
                return nga;
            }
            set
            {
                nga = value;
            }
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
        /// Kthen/Vendos ID-ne e njesise se burimit.
        /// </summary>
        public int Njesia
        {
            get { return njesia; }
            set { njesia = value; }
        }

        /// <summary>
        /// Kthen/Vendos koston e burimit.
        /// </summary>
        public decimal Kosto
        {
            get { return kosto; }
            set { kosto = value; }
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
        /// koston totale te burimit
        /// </summary>
        public decimal KostoTotale
        {
            get
            {
                return kostoTotale;
            }
            set
            {
                kostoTotale = value;
            }
        }
       /// <summary>
       /// kthen vendos shenimet
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
        #endregion

        #region Metoda Publike


        /// <summary>
        /// Ruan objektin e  trupit te dokumentit te skedulimit ne tabelen perkatese ne databaze
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
        public clsMesazh ruaj()
        {
            clsDatabazeProdhimi data = new clsDatabazeProdhimi();
            int id;
            clsMesazh u_ruajt = data.ruajTrupiSkedulimProdhimi(out id,idKoka,idBurimi,idPlanifikimi,idAktiviteti,idProdukti,this.data,nga,ne,koha,njesia,kosto,kostoTotale,shenime);
            IdTrupi = id;
            data.Dispose();
            return u_ruajt;
        }

        /// <summary>
        /// modifikon objektin e  trupit te dokumentit te skedulimit ne tabelen perkatese ne databaze
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo</returns>
        public clsMesazh modifiko()
        {
            clsDatabazeProdhimi data = new clsDatabazeProdhimi();
            clsMesazh u_modifikua = data.modifikoTrupiSkedulimProdhimi(IdTrupi, idKoka, idBurimi, idPlanifikimi, idAktiviteti, idProdukti, this.data, nga,ne, koha,njesia,kosto,kostoTotale,shenime);
            data.Dispose();
            return u_modifikua;
        }

        /// <summary>
        /// fshin objektin e  trupit te dokumentit te skedulimit ne tabelen perkatese ne databaze
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh fshi()
        {
            clsDatabazeProdhimi data = new clsDatabazeProdhimi();
            clsMesazh u_fshi = data.fshiTrupiSkedulimProdhimiSipasID(IdTrupi);
            data.Dispose();
            return u_fshi;
        }

        /// <summary>
        /// fshin objektet e  trupit te dokumentit te skedulimit sipas id se kokes ne tabelen perkatese ne databaze
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh fshiSipasKoka()
        {
            clsDatabazeProdhimi data = new clsDatabazeProdhimi();
            clsMesazh u_fshi = data.fshiTrupiSkedulimProdhimiSipasKoka(IdKoka);
            data.Dispose();
            return u_fshi;
        }

        /// <summary>
        /// Merr objektet e  trupit te dokumentit te planifikimit sipas kokes nga tabela perkatese ne databaze
        /// </summary>
        /// <returns > nje objekt  colTrupiSkedulimProdhimi me te gjithe trupat e nje dokumenti</returns>
        public colTrupiSkedulimProdhimi merriSipasKoka(int idndermarje)
        {
            colTrupiSkedulimProdhimi data = new colTrupiSkedulimProdhimi();
            //data.mbushTrupiPlanifikimi(IdKokaPlanifikim,idndermarje, null);
            data.ktheTrupiSkedulimProdhimi(IdKoka, idndermarje);
            return data;
        }

        /// <summary>
        /// Merr objektin e  trupit te dokumentit te planifikimit sipas id nga tabela perkatese ne databaze
        /// </summary>
        /// <returns > nje objekt clsTrupiSkedulimProdhimi me trupin e dokumentit te planifikimit te kerkuar</returns>
        public clsTrupiSkedulimProdhimi merriSipasID(int idndermarje)
        {
            clsTrupiSkedulimProdhimi data = new clsTrupiSkedulimProdhimi(IdTrupi, idndermarje);
            return data;

        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush trupin e planifikimit nga databaza
        /// </summary>
        /// <param name="dbDataRow">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushTrupSkedulim(DataRow dbDataRow)
        {
            if (dbDataRow != null)
            {
                try
                {
                    int.TryParse(dbDataRow["IDTRUPI"].ToString(), out idTrupi);
                    int.TryParse(dbDataRow["IDKOKA"].ToString(), out idKoka);
                    int.TryParse(dbDataRow["IDPRODUKTI"].ToString(), out idProdukti);
                    int.TryParse(dbDataRow["NJESIA"].ToString(), out njesia);
                    decimal.TryParse(dbDataRow["KOSTO"].ToString(), out kosto);
                    decimal.TryParse(dbDataRow["KOHA"].ToString(), out koha);
                    int.TryParse(dbDataRow["IDBURIMI"].ToString(), out idBurimi);
                    int.TryParse(dbDataRow["IDAKTIVITETI"].ToString(), out idAktiviteti);
                    int.TryParse(dbDataRow["IDPLANIFIKIMI"].ToString(), out idPlanifikimi);
                    kodArtikull = dbDataRow["KODARTIKULLI"].ToString();
                    pershkrimArtikull = dbDataRow["PERSHKRIMARTIKULLI"].ToString();
                    decimal.TryParse(dbDataRow["KOSTOTOTALE"].ToString(), out kostoTotale);
                    DateTime.TryParse(dbDataRow["DATA"].ToString(), out data);
                    DateTime.TryParse(dbDataRow["NGA"].ToString(), out nga);
                    DateTime.TryParse(dbDataRow["NE"].ToString(), out ne);
                    shenime = dbDataRow["SHENIME"].ToString();
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
