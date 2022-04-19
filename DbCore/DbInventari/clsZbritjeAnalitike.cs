using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DbCore.DbKontabiliteti;
using System.Data;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbInventari
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne zbritjet analitike
    ///  (Te dhenat  merren nga tabela : T_ZBRITJEANALITIKE)
    /// </summary>
    public  class clsZbritjeAnalitike
    { 
        #region Atribute

        private int idZbritjeAnalitike;
        private int idArtikulli;
        private int idNivelZbritje;
        private int idNjesia;
        private DateTime  dateFillimit;
        private DateTime  dateMbarimit;
        private decimal  sasiMin;
        private decimal  sasiMax;
        private decimal vleftaMin;
        private decimal vleftaMax;
        private int llojZbritje;
        private decimal zbritja;
        private int idPerdoruesi;
        //private int idNderViti;
        private string emerArtikulli1;
        private string emerArtikulli2;
        private string kodbarArtikulli;
        private string kodifikimArtikulli1;
        private string kodifikimArtikulli2;
        private string nrLlogariFurnitori;
        private bool check;
        private int idNdermarje;
        private string kodArtikulli;
        private int idKonfig;
        private int idStatusDok;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private int idNjesia1;
        private int idNjesia2;
        private string pershkrimNjesia1;
        private string pershkrimNjesia2;
        private decimal zbritja2;
        private DataRow rreshti;
        private decimal koeficent;
        private string pershkrimiNjesia;
        private string emertimFurnitori;
        private string pershkrimGrupi1;
        private string pershkrimGrupi2;
        private string pershkrimGrupi3;
        private decimal cmimBazeZbritjeAnalitike;
        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdZbritjeAnalitike
        {
            get { return idZbritjeAnalitike  ; }
            set { idZbritjeAnalitike  = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e artikullit.
        /// </summary>
        public int IdArtikulli
        {
            get { return idArtikulli; }
            set { idArtikulli = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e nivelit te zbritjes.
        /// </summary>
        public int IdNivelZbritje
        {
            get { return idNivelZbritje; }
            set { idNivelZbritje = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e njesise.
        /// </summary>
        public int IdNjesia
        {
            get { return idNjesia ; }
            set { idNjesia  = value; }
        }
        public int IdNjesia1
        {
            get { return idNjesia1; }
            set { idNjesia1 = value; }
        }
        public int IdNjesia2
        {
            get { return idNjesia2; }
            set { idNjesia2 = value; }
        }
        /// <summary>
        /// Kthen/Vendos daten e fillimit.
        /// </summary>
        public DateTime  DateFillimi
        {
            get { return dateFillimit; }
            set { dateFillimit = value; }
        }
        /// <summary>
        /// Kthen/Vendos daten e mbarimit.
        /// </summary>
        public DateTime  DateMbarimi
        {
            get { return dateMbarimit; }
            set { dateMbarimit = value; }
        }
        /// <summary>
        /// Kthen/Vendos sasine min.
        /// </summary>
        public decimal SasiMin
        {
            get { return sasiMin; }
            set { sasiMin = value; }
        }
        /// <summary>
        /// Kthen/Vendos sasine max.
        /// </summary>
        public decimal SasiMax
        {
            get { return sasiMax; }
            set { sasiMax = value; }
        }
        /// <summary>
        /// Kthen/Vendos vlefta min.
        /// </summary>
        public decimal VleftaMin
        {
            get { return vleftaMin ; }
            set { vleftaMin  = value; }
        }
        /// <summary>
        /// Kthen/Vendos vlefta max.
        /// </summary>
        public decimal VleftaMax
        {
            get { return vleftaMax; }
            set { vleftaMax = value; }
        }
        /// <summary>
        /// Kthen/Vendos llojin e zbritjes.
        /// </summary>
        public int LlojZbritje
        {
            get { return llojZbritje ; }
            set { llojZbritje  = value; }
        }
        /// <summary>
        /// Kthen/Vendos zbritjen.
        /// </summary>
        public decimal  Zbritja
        {
            get { return zbritja; }
            set { zbritja = value; }
        }  
        /// <summary>
        /// Kthen/Vendos zbritjen.
        /// </summary>
        public decimal  Zbritja2
        {
            get { return zbritja2; }
            set { zbritja2 = value; }
        }

        public decimal CmimBazeZbritjeAnalitike
        {
            get { return cmimBazeZbritjeAnalitike; }
            set { cmimBazeZbritjeAnalitike = value; }
        }
        /// <summary>
        /// Kthen/Vendos Cmimin
        /// </summary>
        public decimal Koeficent
        {
            get { return koeficent; }
            set { koeficent = value; }
        }
        public string PershkrimNjesia1
        {
            get
            {
                return pershkrimNjesia1;
            }
            set
            {
                pershkrimNjesia1 = value;
            }
        }
        public string PershkrimNjesia2
        {
            get
            {
                return pershkrimNjesia2;
            }
            set
            {
                pershkrimNjesia2 = value;
            }
        }

        public string PershkrimiNjesia
        {
            get
            {
                return pershkrimiNjesia;
            }
            set
            {
                pershkrimiNjesia = value;
            }
        }

        /// <summary>
        /// Kthen dhe vendos emertimin e furnitorit
        /// </summary>
        public string EmertimiFurnitorit
        {
            get
            {
                return emertimFurnitori;
            }

            set
            {
                emertimFurnitori = value;
            }

        }

        /// <summary>
        /// Kthen dhe modifikon pershkrimin e grupit te pare
        /// </summary>
        public string Kodifikim1
        {
            get
            {
                return pershkrimGrupi1;
            }
            set
            {
                pershkrimGrupi1 = value;
            }
        }

        public string Kodifikim2
        {
            get
            {
                return pershkrimGrupi2;
            }
            set
            {
                pershkrimGrupi2 = value;
            }

        }

        public string Kodifikim3
        {
            get
            {
                return pershkrimGrupi3;
            }
            set
            {
                pershkrimGrupi3 = value;
            }
        }


        /// <summary>
        /// Kthen/Vendos ID-ne e perdoruesit.
        /// </summary>
        public int IdPerdoruesi
        {
            get
            {
                return idPerdoruesi;
            }
            set
            {
                idPerdoruesi = value;
            }
        }
       
        /// Kthen/Vendos ID-ne e konfigurimit.
        /// </summary>
        public int IdKonfig
        {
            get { return idKonfig; }
            set { idKonfig = value; }
        }
        /// <summary>
        /// Kthen/Vendos emrin e artikullit.
        /// </summary>
        public string EmerArtikulli1
        {
            get
            {
                return emerArtikulli1;
            }
            set
            {
                emerArtikulli1 = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos emrin e dyte te artikullit.
        /// </summary>
        public string EmerArtikulli2
        {
            get
            {
                return emerArtikulli2;
            }
            set
            {
                emerArtikulli2 = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos kodbarin e artikullit.
        /// </summary>
        public string KodbarArtikulli
        {
            get
            {
                return kodbarArtikulli;
            }
            set
            {
                kodbarArtikulli = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos kodifikimin e pare te artikullit.
        /// </summary>
        public string KodifikimArtikulli1
        {
            get
            {
                return kodifikimArtikulli1;
            }
            set
            {
                kodifikimArtikulli1 = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos kodifikimin e dyte te artikullit.
        /// </summary>
        public string KodifikimArtikulli2
        {
            get
            {
                return kodifikimArtikulli2;
            }
            set
            {
                kodifikimArtikulli2 = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos nr e llogarise se furnitorit.
        /// </summary>
        public string NrLlogariFurnitori
        {
            get
            {
                return nrLlogariFurnitori ;
            }
            set
            {
                nrLlogariFurnitori = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos nese reshti zgjidhet apo jo.
        /// </summary>
        public Boolean Check
        {
            get { return check; }
            set { check = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e ndermarjes.
        /// </summary>
        public int IdNdermarje
        {
            get { return idNdermarje ; }
            set { idNdermarje  = value; }

        }
        /// <summary>
        /// Kthen/Vendos kodin e artikullit.
        /// </summary>
        public string KodArtikulli
        {
            get
            {
                return kodArtikulli;
            }
            set
            {
                kodArtikulli = value;
            }
        }
        public int IdStatusDok
            {
            get { return idStatusDok; }
            set { idStatusDok = value; }
            }
        public DateTime DtKrijimi
            {
            get { return dtKrijimi; }

            }
        public DateTime DtModifikimi
            {
            get { return dtModifikimi; }

            }
 
        #endregion

        #region Konstruktoret

        /// <summary>
        /// konstruktori me parametra
        /// </summary>
        /// <param name="idZbritjaAnalitike"> id ritese e zbritjes analitike</param>
        /// <param name="idArtikulli"> id e artikullit</param>
        /// <param name="idNivelZbritje"> id e nivelit te zbritjes</param>
        /// <param name="idNjesia"> id e njesise</param>
        /// <param name="dateFillimit"> data e fillimit</param>
        /// <param name="dateMbarimit"> data e mbarimit</param>
        /// <param name="sasiMin"> sasi min</param>
        /// <param name="sasiMax"> sasi max</param>
        /// <param name="vleftaMin"> vlefta min</param>
        /// <param name="vleftaMax"> vlefta max</param>
        /// <param name="llojZbritje"> lloj zbritje</param>
        /// <param name="zbritja"> zbritja</param>
        /// <param name="idPerdoruesi">id perdoruesi</param>
        /// <param name="idNderViti"> id nderviti</param>
        /// <param name="idNdermarje"> id ndermarje</param>
        /// <param name="idkonfig">id e konfigurimit</param>
        public clsZbritjeAnalitike(int idZbritjaAnalitike,int idArtikulli, int idNivelZbritje, int idNjesia, DateTime dateFillimit, DateTime dateMbarimit, decimal sasiMin, decimal sasiMax,
                                   decimal vleftaMin, decimal vleftaMax, int llojZbritje, decimal zbritja, int idPerdoruesi, int idNdermarje, int idkonfig, int idstatusdok, int idnjesia2, decimal zbritja2)  
        {
            this.idNivelZbritje = idNivelZbritje;
            this.idArtikulli = idArtikulli;
            this.idNivelZbritje = idNivelZbritje;
            this.idNjesia = idNjesia;
            this.dateFillimit = dateFillimit;
            this.dateMbarimit = dateMbarimit;
            this.sasiMin = sasiMin;
            this.sasiMax = sasiMax;
            this.vleftaMin = vleftaMin;
            this.vleftaMax = vleftaMax;
            this.llojZbritje = llojZbritje;
            this.zbritja = zbritja;
            this.idPerdoruesi = idPerdoruesi;
            this.idNdermarje = idNdermarje;
            this.idKonfig = idkonfig;
            this.idStatusDok = idstatusdok;
            this.idNjesia2 = idnjesia2;
            this.zbritja2 = zbritja2;
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idZbritjeAnalitike">id e zbritjes analitike</param>
        public clsZbritjeAnalitike(int idZbritjeAnalitike)
        {
            clsDatabaseInventari dbZbritjeAnalitike = new clsDatabaseInventari();
            mbushZbritjeAnalitike(dbZbritjeAnalitike.ktheZbritjeAnalitike(idZbritjeAnalitike));
            dbZbritjeAnalitike.Dispose();
        }

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public clsZbritjeAnalitike()
        {
        }

        public clsZbritjeAnalitike(DataRow rreshti)
        {
            mbushZbritjeAnalitike(rreshti);
        }

        #endregion

        #region Metoda Publike

        public clsZbritjeAnalitike krijoZbritjeAnalitikePerImport(string niveli, string kodArtikulli, string llojZbritje, string njesia1, string njesia2, decimal zbritja1, decimal zbritja2, DateTime dtFillimi, DateTime dtMbarimi, int idNdermarrje, int idPerdorues, int idKonfigAmbjente)
        {
            if (string.IsNullOrEmpty(niveli))
                throw new MyException("Plotesoni nivelin e zbritjes!");

            if (string.IsNullOrEmpty(kodArtikulli))
                throw new MyException("Plotesoni kodin e artikullit!");

            if (string.IsNullOrEmpty(njesia1))
                throw new MyException("Plotesoni njesine e pare te artikullit!");

            if (string.IsNullOrEmpty(njesia2))
                throw new MyException("Plotesoni njesine e dyte te artikullit!");

            int idNivelZbritje = clsNivelZbritje.ktheIdNivelZbritje(niveli, idNdermarrje);
            if (idNivelZbritje <= 0)
                throw new MyException("Niveli i zbritjes " + niveli + " nuk ekziston!");
            clsArtikulli art = new clsArtikulli(kodArtikulli, idNdermarrje);
            if (art.IdArtikulli <= 0)
                throw new MyException("Artikulli me kod " + kodArtikulli + " nuk ekziston!");

            int idNjesia = clsNjesiArtikulli.ktheIdNjesiArtikulli(njesia1, idNdermarrje);
            if (idNjesia <= 0)
                throw new MyException("Njesia me kod " + njesia1 + " nuk ekziston!");

            if (art.Njesi1Artikulli != idNjesia)
                throw new MyException("Njesia e pare e artikullit " + art.KodArtikulli + " nuk eshte " + njesia1 + "!");

            int idNjesia2 = clsNjesiArtikulli.ktheIdNjesiArtikulli(njesia2, idNdermarrje);
            if (idNjesia2 <= 0)
                throw new MyException("Njesia me kod " + njesia1 + " nuk ekziston!");

            if (art.Njesi2Artikulli != idNjesia2)
                throw new MyException("Njesia e dyte e artikullit " + art.KodArtikulli + " nuk eshte " + njesia2 + "!");

            if (llojZbritje != "Perqindje" && llojZbritje != "Vlere")
                throw new MyException("Lloji i zbritjes duhet te jete ose Perqindje, ose Vlere!");
            int idLlojZbritje = 0;
            if (llojZbritje == "Perqindje") {
                idLlojZbritje = 0;
                if(zbritja1 <0 || zbritja1 > 100)
                    throw new MyException("Zbritja 1 duhet te jete nje numer me i madh ose i barabarte me zero dhe me i vogel ose i barabarte me 100!");
                if (zbritja2 < 0 || zbritja2 > 100)
                    throw new MyException("Zbritja 2 duhet te jete nje numer me i madh ose i barabarte me zero dhe me i vogel ose i barabarte me 100!");
            }
            else
                idLlojZbritje = 1;

            if (dtFillimi > dtMbarimi)
                throw new MyException("Data e fillimit nuk mund te jete me e madhe se data e mbarimit!");

            return new clsZbritjeAnalitike(0, art.IdArtikulli, idNivelZbritje, idNjesia, dtFillimi, dtMbarimi, 0, 0, 0, 0, idLlojZbritje, zbritja1, idPerdorues, idNdermarrje, idKonfigAmbjente, 1, idNjesia2, zbritja2);
        }

        public clsMesazh ruaj()
        {
            clsDatabaseInventari dbInv = new clsDatabaseInventari();
            try
            {
                dbInv.beginTransaksion();
                clsMesazh mesazh = new clsMesazh(true);
                int idZ;
                idZ = ktheIdZbritjeAnalitikeSipasArtikullitDheNivelit(this.idNivelZbritje, this.idArtikulli, this.idNdermarje);
                if(idZ == 0)
                mesazh = dbInv.ruajZbritjeAnalitike(out idZ, this.IdArtikulli, this.IdNivelZbritje, this.IdNjesia, this.DateFillimi, this.DateMbarimi, this.SasiMin, this.SasiMax,
                    this.VleftaMin, this.VleftaMax, this.LlojZbritje, this.Zbritja, this.IdPerdoruesi, this.IdNdermarje, this.IdKonfig, this.idStatusDok, this.idNjesia2, this.zbritja2);
                else
                    mesazh = dbInv.modifikoZbritjeAnalitike(idZ, this.IdArtikulli, this.IdNivelZbritje, this.IdNjesia, this.DateFillimi, this.DateMbarimi, this.SasiMin, this.SasiMax, this.VleftaMin, this.VleftaMax, this.LlojZbritje, this.Zbritja, this.IdPerdoruesi, this.IdNdermarje, this.IdKonfig, this.idStatusDok, this.idNjesia2, this.zbritja2);
                if (!mesazh.Status)
                {
                    dbInv.rollbackTransaksion();
                    return mesazh;
                }

                dbInv.commitTransaksion();
                mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
                return mesazh;
            }
            catch (Exception ce)
            {
                dbInv.rollbackTransaksion();
                return new clsMesazh(false, ce.Message);
            }
        }

        public clsMesazh ruajZbritje(colZbritjetAnalitike zbritjetAnalitike)
        {
            clsDatabaseInventari dbInv = new clsDatabaseInventari();
            try
            {
                dbInv.beginTransaksion();
                clsMesazh mesazh = new clsMesazh(true);

                foreach (clsZbritjeAnalitike c in zbritjetAnalitike)
                {
                    if (mesazh.Status)
                    {
                        if (c.IdZbritjeAnalitike == 0)
                        {
                            int idZ;
                            mesazh = dbInv.ruajZbritjeAnalitike(out idZ, c.IdArtikulli, c.IdNivelZbritje, c.IdNjesia, c.DateFillimi, c.DateMbarimi, c.SasiMin, c.SasiMax,
                                c.VleftaMin, c.VleftaMax, c.LlojZbritje, c.Zbritja, c.IdPerdoruesi, c.IdNdermarje, c.IdKonfig, c.idStatusDok, c.idNjesia2, c.zbritja2);
                            //mesazh= ruajZbritjeAnalitike(c);
                        }
                        else
                            mesazh = dbInv.modifikoZbritjeAnalitike(c.IdZbritjeAnalitike, c.IdArtikulli, c.IdNivelZbritje, c.IdNjesia, c.DateFillimi, c.DateMbarimi, c.SasiMin, c.SasiMax,
    c.VleftaMin, c.VleftaMax, c.LlojZbritje, c.Zbritja, c.IdPerdoruesi, c.IdNdermarje, c.IdKonfig, c.idStatusDok, c.idNjesia2, c.zbritja2);
                        //mesazh= modifikoZbritjeAnalitike(c);
                    }
                    else
                    {
                        dbInv.rollbackTransaksion();
                        return mesazh;
                    }
                }
                if (mesazh.Status)
                {
                    dbInv.commitTransaksion();
                    mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
                    return mesazh;
                }
                else
                {
                    dbInv.rollbackTransaksion();
                    return mesazh;
                }
            }
            catch (Exception ce)
            {
                dbInv.rollbackTransaksion();
                return new clsMesazh(false, ce.Message);
            }
        }

       
        /// <summary>
        /// Modifikon objektin  zbritje analitike ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbInventari.clsDatabaseInventari.modifikoZbritjeAnalitike"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo</returns>
     
        public clsMesazh modifiko()
        {
            clsDatabaseInventari data = new clsDatabaseInventari();
            clsMesazh u_modifikua = data.modifikoZbritjeAnalitike(this.IdZbritjeAnalitike, this.IdArtikulli, this.IdNivelZbritje, this.IdNjesia, this.DateFillimi, this.DateMbarimi, this.SasiMin, this.SasiMax,
                                this.VleftaMin, this.VleftaMax, this.LlojZbritje, this.Zbritja, this.IdPerdoruesi, this.IdNdermarje, this.IdKonfig, this.idStatusDok, this.idNjesia2, this.zbritja2);
            data.Dispose();
            //clsMesazh u_modifikua = data.modifikoZbritjeAnalitike(this);
            return u_modifikua;
        }
        /// <summary>
        /// Fshin objektin  zbritje analitike ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbInventari.clsDatabaseInventari.fshiZbritjeAnalitike"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
     
        public clsMesazh fshi()
        {
            clsDatabaseInventari data = new clsDatabaseInventari();
            clsMesazh u_fshi = data.fshiZbritjeAnalitikeStatus(this.IdZbritjeAnalitike, this.idPerdoruesi);
            data.Dispose();
            //clsMesazh u_fshi = data.fshiZbritjeAnalitike(this);
            return u_fshi;
        }
        /// <summary>
        /// Merr objektin  zbritje analitike nga tabela perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbInventari.clsDatabaseInventari.ktheZbritjeAnalitike"/> 
        /// </summary>
      
       

        public static int ktheIdZbritjeAnalitikeSipasArtikullitDheNivelit(int idNivelZbritje, int idArtikulli, int idNdermarrje)
        {
            using (clsDatabaseInventari dbInv = new clsDatabaseInventari())
            {
                return dbInv.ktheIdZbritjeAnalitikeSipasArtikullitDheNivelit(idNivelZbritje, idArtikulli, idNdermarrje);
            }
        }


        public static clsZbritjeAnalitike ktheZbritjeAnalitikeArtikulliRow(string kodArtikulli, int ZbritjaKlientit, string date, int idPerdoruesi, int idNdermarrje)
        {
            clsZbritjeAnalitike zbritja;
            clsNivelZbritje niv = new clsNivelZbritje();
            niv.mbushNivelZbritje(ZbritjaKlientit);
            if (niv.IdNivelZbritje == 0)
                return null;

            colNiveleZbritjesh colNivele = new colNiveleZbritjesh();
            colNivele.mbushNiveleZbritjeshSipasPrindit(ZbritjaKlientit);
            bool prind = true;
            if (colNivele.Count == 0)
            {
                ZbritjaKlientit = niv.IdNivelZbritje;
                return ktheZbritje(ZbritjaKlientit, kodArtikulli, idPerdoruesi, date, idNdermarrje);
            }
            foreach (clsNivelZbritje nivelZbritje in colNivele)
            {
                if (prind && niv.PrioritetiNivelZbritje > nivelZbritje.PrioritetiNivelZbritje)
                {
                    prind = false;
                    ZbritjaKlientit = niv.IdNivelZbritje;
                    zbritja = ktheZbritje(ZbritjaKlientit, kodArtikulli, idPerdoruesi, date, idNdermarrje);
                    if (zbritja != null)
                    {
                        return zbritja;
                    }
                    ZbritjaKlientit = nivelZbritje.IdNivelZbritje;
                    zbritja = ktheZbritje(ZbritjaKlientit, kodArtikulli, idPerdoruesi, date, idNdermarrje);
                    if (zbritja != null)
                    {
                        return zbritja;
                    }
                }
                else
                {
                    ZbritjaKlientit = nivelZbritje.IdNivelZbritje;
                    zbritja = ktheZbritje(ZbritjaKlientit, kodArtikulli, idPerdoruesi, date, idNdermarrje);
                    if (zbritja != null)
                    {
                        return zbritja;
                    }
                }
            }
            if (prind)
            {
                ZbritjaKlientit = niv.IdNivelZbritje;
                zbritja = ktheZbritje(ZbritjaKlientit, kodArtikulli, idPerdoruesi, date, idNdermarrje);
                if (zbritja != null)
                {
                    return zbritja;
                }
            }
            return null;
        }
        public static clsZbritjeAnalitike ktheZbritje(int nivelZbritje, string kodArtikulli, int idperdorues, string date, int idNdermarrje)
        {
            colZbritjetAnalitike colZbritje = new colZbritjetAnalitike(kodArtikulli, nivelZbritje, idperdorues, idNdermarrje);
            if (colZbritje.Count > 0 && DateTime.Parse(date) >= colZbritje[0].DateFillimi) // && DateTime.Parse(date) <= colZbritje[0].DateMbarimi)
            {
                //if (idNjesia == colZbritje[0].IdNjesia)
                //    zbritje += colZbritje[0].Zbritja;
                //else zbritje += colZbritje[0].Zbritja2;
                return colZbritje[0];
            }
            return null;
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush zbritje analitike nga databaza
        /// </summary>
        /// <param name="dbDataRowZbritjeAnalitike">datarow qe duhet mbushur nga databaza</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        internal bool mbushZbritjeAnalitike(DataRow dbDataRowZbritjeAnalitike)
        {
            if (dbDataRowZbritjeAnalitike != null)
            {

                try
                {

                    int.TryParse(dbDataRowZbritjeAnalitike["IDZBRITJEANALITIKE"].ToString(), out idZbritjeAnalitike);
                    int.TryParse(dbDataRowZbritjeAnalitike["IDARTIKULLI"].ToString(), out idArtikulli);
                    int.TryParse(dbDataRowZbritjeAnalitike["IDNIVELZBRITJE"].ToString(), out idNivelZbritje);
                    int.TryParse(dbDataRowZbritjeAnalitike["IDNJESIA"].ToString(), out idNjesia);
                    int.TryParse(dbDataRowZbritjeAnalitike["IDNJESIA2"].ToString(), out idNjesia2);
                    DateTime.TryParse(dbDataRowZbritjeAnalitike["DTFILLIMIT"].ToString(), out dateFillimit);
                    DateTime.TryParse(dbDataRowZbritjeAnalitike["DTMBARIMIT"].ToString(), out dateMbarimit);
                    decimal.TryParse(dbDataRowZbritjeAnalitike["SASIMIN"].ToString(), out sasiMin);
                    decimal.TryParse(dbDataRowZbritjeAnalitike["SASIMAX"].ToString(), out sasiMax);
                    decimal.TryParse(dbDataRowZbritjeAnalitike["VLEFTAMIN"].ToString(), out vleftaMin);
                    decimal.TryParse(dbDataRowZbritjeAnalitike["VLEFTAMAX"].ToString(), out vleftaMax);
                    int.TryParse(dbDataRowZbritjeAnalitike["LLOJZBRITJE"].ToString(), out llojZbritje);
                    decimal.TryParse(dbDataRowZbritjeAnalitike["ZBRITJA"].ToString(), out zbritja);
                    decimal.TryParse(dbDataRowZbritjeAnalitike["ZBRITJA2"].ToString(), out zbritja2);

                    int.TryParse(dbDataRowZbritjeAnalitike["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    //int.TryParse(dbDataRowZbritjeAnalitike["IDNDERVITI"].ToString(), out idNderViti);
                    int.TryParse(dbDataRowZbritjeAnalitike["IDNDERMARJE"].ToString(), out idNdermarje);
                    int.TryParse(dbDataRowZbritjeAnalitike["IDKONFIG"].ToString(), out idKonfig);
                    int.TryParse(dbDataRowZbritjeAnalitike["IDSTATUSDOK"].ToString(), out idStatusDok);
                    DateTime.TryParse(dbDataRowZbritjeAnalitike["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRowZbritjeAnalitike["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    kodbarArtikulli = dbDataRowZbritjeAnalitike["kodbari"].ToString();
                    emerArtikulli1 = dbDataRowZbritjeAnalitike["PERSHKRIMARTIKULLI"].ToString();
                    emerArtikulli2 = dbDataRowZbritjeAnalitike["PERSHKRIMIANGARTIKULLI"].ToString();
                    kodifikimArtikulli1 = dbDataRowZbritjeAnalitike["KODIFIKIMI1ARTIKULLI"].ToString();
                    kodifikimArtikulli2 = dbDataRowZbritjeAnalitike["KODIFIKIMI2ARTIKULLI"].ToString();
                    nrLlogariFurnitori = dbDataRowZbritjeAnalitike["furnitori"].ToString();
                    kodArtikulli = dbDataRowZbritjeAnalitike["KODARTIKULLI"].ToString();

                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se zbritjes analitike nga db-ja");
                }
            }
            else
                return false;
        }

        /// <summary>
        /// mbush zbritje analitike nga databaza
        /// </summary>
        /// <param name="dbDataRowZbritjeAnalitike">datarow qe duhet mbushur nga databaza per te shfaqur ne gride</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        internal bool mbushZbritjeAnalitikePerGriden(DataRow dbDataRowZbritjeAnalitike)
        {
            if (dbDataRowZbritjeAnalitike != null)
            {

                try
                {

                    int.TryParse(dbDataRowZbritjeAnalitike["IdZbritjeAnalitike"].ToString(), out idZbritjeAnalitike);
                    int.TryParse(dbDataRowZbritjeAnalitike["IdArtikulli"].ToString(), out idArtikulli);
                    int.TryParse(dbDataRowZbritjeAnalitike["IdNivelZbritje"].ToString(), out idNivelZbritje);
                    int.TryParse(dbDataRowZbritjeAnalitike["IdNjesia"].ToString(), out idNjesia);
                    int.TryParse(dbDataRowZbritjeAnalitike["IdNjesia1"].ToString(), out idNjesia1);
                    int.TryParse(dbDataRowZbritjeAnalitike["IdNjesia2"].ToString(), out idNjesia2);
                    DateTime.TryParse(dbDataRowZbritjeAnalitike["DateFillimi"].ToString(), out dateFillimit);
                    DateTime.TryParse(dbDataRowZbritjeAnalitike["DateMbarimi"].ToString(), out dateMbarimit);
                    decimal.TryParse(dbDataRowZbritjeAnalitike["SasiMin"].ToString(), out sasiMin);
                    decimal.TryParse(dbDataRowZbritjeAnalitike["SasiMax"].ToString(), out sasiMax);
                    decimal.TryParse(dbDataRowZbritjeAnalitike["VleftaMin"].ToString(), out vleftaMin);
                    decimal.TryParse(dbDataRowZbritjeAnalitike["VleftaMax"].ToString(), out vleftaMax);
                    int.TryParse(dbDataRowZbritjeAnalitike["LlojZbritje"].ToString(), out llojZbritje);
                    decimal.TryParse(dbDataRowZbritjeAnalitike["Zbritja"].ToString(), out zbritja);
                    decimal.TryParse(dbDataRowZbritjeAnalitike["Zbritja2"].ToString(), out zbritja2);
                    bool.TryParse(dbDataRowZbritjeAnalitike["Check"].ToString(), out check);
                    int.TryParse(dbDataRowZbritjeAnalitike["IdPerdoruesi"].ToString(), out idPerdoruesi);
                    int.TryParse(dbDataRowZbritjeAnalitike["IdNdermarje"].ToString(), out idNdermarje);
                    int.TryParse(dbDataRowZbritjeAnalitike["IdKonfig"].ToString(), out idKonfig);
                    int.TryParse(dbDataRowZbritjeAnalitike["IdStatusDok"].ToString(), out idStatusDok);
                    DateTime.TryParse(dbDataRowZbritjeAnalitike["DtKrijimi"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRowZbritjeAnalitike["DtModifikimi"].ToString(), out dtModifikimi);
                    kodbarArtikulli = dbDataRowZbritjeAnalitike["KodbarArtikulli"].ToString();
                    emerArtikulli1 = dbDataRowZbritjeAnalitike["EmerArtikulli1"].ToString();
                    emerArtikulli2 = dbDataRowZbritjeAnalitike["EmerArtikulli2"].ToString();
                    kodifikimArtikulli1 = dbDataRowZbritjeAnalitike["KodifikimArtikulli1"].ToString();
                    kodifikimArtikulli2 = dbDataRowZbritjeAnalitike["KodifikimArtikulli2"].ToString();
                    nrLlogariFurnitori = dbDataRowZbritjeAnalitike["NrLlogariFurnitori"].ToString();
                    kodArtikulli = dbDataRowZbritjeAnalitike["KodArtikulli"].ToString();
                    pershkrimNjesia1 = dbDataRowZbritjeAnalitike["PershkrimNjesia1"].ToString();
                    pershkrimNjesia2 = dbDataRowZbritjeAnalitike["PershkrimNjesia2"].ToString();
                    pershkrimiNjesia = dbDataRowZbritjeAnalitike["PershkrimiNjesia"].ToString();
                    emertimFurnitori = dbDataRowZbritjeAnalitike["EmertimiFurnitorit"].ToString();
                    pershkrimGrupi1 = dbDataRowZbritjeAnalitike["Kodifikim1"].ToString();
                    pershkrimGrupi2 = dbDataRowZbritjeAnalitike["Kodifikim2"].ToString();
                    pershkrimGrupi3 = dbDataRowZbritjeAnalitike["Kodifikim3"].ToString();
                    decimal.TryParse(dbDataRowZbritjeAnalitike["Koeficent"].ToString(), out koeficent); 
                    decimal.TryParse(dbDataRowZbritjeAnalitike["CmimBazeZbritjeAnalitike"].ToString(), out cmimBazeZbritjeAnalitike);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se zbritjes analitike nga db-ja");
                }
            }
            else
                return false;
        }
        #endregion

    }
}
