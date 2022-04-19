using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Globalization;
using DbCore.IMBUtils.Validation;
using System.Resources;
using DbCore.IMBUtils.DataBase;


namespace DbCore.DbInventari
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne  kodbaret e artikujve
    ///  (Te dhenat  merren nga tabela : T_KODBARI)
    /// </summary>
    public class clsKodbari
    {  
        #region Atributet

        private int idKodbari;
        private int idArtikulli;
        private string  pershkrimi;
        private int njesia;
        private int detajim1;
        private int detajim2;       
        private int idndermarje;
        private string kodartikulli;
        private int llojdetajim;
        private string kodDetajimi1;
        private string kodDetajimi2;
       
        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdKodbari
        {
            get { return idKodbari ; }
            set { idKodbari  = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e artikullit
        /// </summary>
        public int IdArtikulli
        {
            get { return idArtikulli ; }
            set { idArtikulli  = value; }
        }
        /// <summary>
        /// Kthen/Vendos kodbarin e artikullit.
        /// </summary>
        public String Pershkrimi
        {
            get { return pershkrimi ; }
            set { pershkrimi  = value; }
        }
        public int Njesia {

            get { return njesia; }
            set { njesia = value; }
        
        }

        public int Detajim1
        {
            get
            {
                return detajim1;
            }

            set
            {
                detajim1 = value;
            }
        }

        public int Detajim2
        {
            get
            {
                return detajim2;
            }

            set
            {
                detajim2 = value;
            }
        }
     
        #endregion

        #region Konstruktoret

        /// <summary>
        /// konstruktori me parametra
        /// </summary>
        /// <param name="idKodbari">id ritese e kodbarit</param>
        /// <param name="idArtikulli"> id e artikullit</param>
        /// <param name="pershkrimi"> kodbari i artikullit</param>
        public clsKodbari(int idArtikulli,  String pershkrimi, int idnjesia, int detajim1, int detajim2)
        {          
            this.idArtikulli = idArtikulli;
            this.pershkrimi = pershkrimi;
            this.njesia = idnjesia;
            this.detajim1 = detajim1;
            this.detajim2 = detajim2;
        }

        public clsKodbari(string kodartikulli, int idArtikulli, String pershkrimi, int idnjesia, int idDetajim1, int idDetajim2, string detajimi1, string detajimi2, int idndermarje, int llojdetajim, System.Resources.ResourceManager rm, CultureInfo ci)
        {
            this.kodartikulli = kodartikulli;
            this.idArtikulli = idArtikulli;
            this.pershkrimi = pershkrimi;
            this.njesia = idnjesia;
            this.detajim1 = idDetajim1;
            this.detajim2 = idDetajim2;
            this.kodDetajimi1 = detajimi1;
            this.kodDetajimi2 = detajimi2;
            this.idndermarje = idndermarje;
            this.llojdetajim = llojdetajim;
            clsMesazh mesazh = this.kontrollkodbari(rm, ci);
            if (!mesazh.Status)
                throw new MyException(mesazh.PershkrimMesazhi);
        }

        /// <summary>
        /// konstruktori me parametra
        /// </summary>
        /// <param name="idArtikulli"> id artikulli</param>
        /// <param name="pershkrimi"> kodbari i artikullit</param>
        public clsKodbari(int idArtikulli, String pershkrimi)
        {
            this.idArtikulli = idArtikulli;
            this.pershkrimi = pershkrimi;
        }

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public clsKodbari()
        {
            this.Njesia = 1;
        }

        /// <summary>
        /// konstruktor me parametra
        /// </summary>
        /// <param name="pershkrimi">pershkrimi i kodbarit</param>
        /// <param name="rm"></param>
        /// <param name="ci"></param>
        public clsKodbari(String pershkrimi, int njesia, string detajimi1, int idDetajimi1, string detajimi2, int idDetajimi2)
        {
            this.Njesia = njesia;
            this.Pershkrimi = pershkrimi;
            this.detajim1 = idDetajimi1;
            this.kodDetajimi1 = detajimi1;
            this.detajim2 = idDetajimi2;
            this.kodDetajimi2 = detajimi2;
        }

        public clsKodbari(DataRow rreshti)
        {
            
            mbushKodbarin(rreshti);
        }

        public clsKodbari(System.Collections.Generic.Dictionary<string, object> kodbari)
        {
            this.pershkrimi = kodbari["pershkrimi"].ToString();
            if (this.pershkrimi != "")
            {
                this.njesia = Convert.ToInt32(kodbari["njesia"].ToString());
                this.detajim1 = Convert.ToInt32(kodbari["detajimi1"].ToString());
                if (this.detajim1 > 0)
                {
                    clsDetajimArtikulli det1 = new clsDetajimArtikulli(this.detajim1);
                    this.kodDetajimi1 = det1.KodDetajimArtikulli;
                }
                this.detajim2 = Convert.ToInt32(kodbari["detajimi2"].ToString());
                if (this.detajim2 > 0)
                {
                    clsDetajimArtikulli det2 = new clsDetajimArtikulli(this.detajim2);
                    this.kodDetajimi2 = det2.KodDetajimArtikulli;
                }
            }
        }

        #endregion

        #region Metoda Private
        private clsMesazh kontrollkodbari(System.Resources.ResourceManager rm, CultureInfo ci)
        {
            if (String.IsNullOrEmpty(Pershkrimi))
                return new clsMesazh(false, "Plotesoni barkodin e artikullit!");
            clsMesazh mesazh = clsFunksione.kontrolloKaraktereMeMesazh(Pershkrimi, FusheKontrolli.Kodbari, false);
            if (!mesazh)
                return mesazh;

            if (njesia != 1 && njesia != 2)
                return new clsMesazh(false, $"Njesia per barkodin {pershkrimi} duhet te jete 1 ose 2!");

            if (String.IsNullOrEmpty(kodartikulli))
                return new clsMesazh(false, $"Plotesoni kodin e artikullit per barkodin {pershkrimi}!");
            mesazh = clsFunksione.kontrolloKaraktereMeMesazh(kodartikulli, FusheKontrolli.Kodi, false);
            if (!mesazh)
                return mesazh;

            if (idArtikulli <= 0)
                return new clsMesazh(false, $"Artikulli me kod {kodartikulli} nuk ekziston!");
            if (ekzistonKodbar(pershkrimi, idndermarje))
                return new clsMesazh(false, $"Barkodi {pershkrimi} ekziston dhe nuk mund te importohet!");

            if (!String.IsNullOrEmpty(kodDetajimi1))
            {
                llojdetajim = 1;
                if (!clsArtikulli.EshteMeDetajim(idArtikulli))
                    return new clsMesazh(false, $"Artikulli me kod {kodartikulli} nuk eshte me detajim dhe nuk mund te importohet");                
                if (this.detajim1 == 0)
                    return new clsMesazh(false, $"Detajimi i pare {kodDetajimi1} nuk ekziston!");
                if (!clsArtikulli.ekzistonDetajimBarkodArtikulli(kodartikulli, idndermarje, llojdetajim, kodDetajimi1))
                    return new clsMesazh(false, $"Detajimi i pare {kodDetajimi1} nuk eshte i lidhur me artikullin {kodartikulli}!");              
            }
            if (!String.IsNullOrEmpty(kodDetajimi2))
            {
                llojdetajim = 2;
                if (!clsArtikulli.EshteMeDetajim(idArtikulli))
                    return new clsMesazh(false, $"Artikulli {kodartikulli} nuk eshte me detajim dhe nuk mund te importohet");                
                if (this.detajim1 == 0)
                    return new clsMesazh(false, $"Detajimi i dyte {kodDetajimi2} nuk ekziston!");
                if (!clsArtikulli.ekzistonDetajimBarkodArtikulli(kodartikulli, idndermarje, llojdetajim, kodDetajimi2))
                    return new clsMesazh(false, $"Detajimi i dyte {kodDetajimi2} nuk eshte i lidhur me artikullin {kodartikulli}!");
            }
            return new clsMesazh(true, "Kontrollet u kaluan me sukses!");
        }

        #endregion


        #region Metoda Publike

        /// <summary>
        /// kontrollon kodbarin per hapsira ose karaktere te palejuara
        /// </summary>
        /// <param name="rm"></param>
        /// <param name="ci"></param>
        /// <returns>kthen nje objekt te tipit clsMesazh</returns>
        public clsKodbari krijoKodbarePerImport(string kodartikulli, String pershkrimi, int njesia, string detajimi1, string detajimi2, int idndermarje, int llojdetajim, ResourceManager rm, CultureInfo ci)
        {
            int idArtikulli = clsArtikulli.ktheIdArtikulli(kodartikulli, idndermarje);
            int idDetajimi1 = 0, idDetajimi2 = 0;
            if (!String.IsNullOrEmpty(detajimi1))
            {
                clsDetajimArtikulli detajimiPare = new clsDetajimArtikulli(detajimi1, idndermarje);
                idDetajimi1 = detajimiPare.IdDetajimArtikulli;
            }
            if (!String.IsNullOrEmpty(detajimi2))
            {
                clsDetajimArtikulli detajimiDyte = new clsDetajimArtikulli(detajimi2, idndermarje);
                idDetajimi2 = detajimiDyte.IdDetajimArtikulli;
            }

            return new clsKodbari(kodartikulli, idArtikulli, pershkrimi, njesia, idDetajimi1, idDetajimi2, detajimi1, detajimi2, idndermarje, llojdetajim, rm, ci);
        }
        /// <summary>
        /// Ruan objektin kodbar artikulli ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbInventari.clsDatabaseInventari.ruajKodbar"/> 
        /// <param name="idndermarje"> id e ndermarjes</param>
        /// </summary>
        /// <returns> nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>     
        public clsMesazh ruaj(int idndermarje)
        {
            clsDatabaseInventari data = new clsDatabaseInventari();            
            if (clsKodbari.ekzistonKodbar(this.Pershkrimi, idndermarje, data))
            {
                data.Dispose();
                return new clsMesazh(false, String.Format("Barkodi {0} ekziston!", this.Pershkrimi));
            }
            clsMesazh u_ruajt = data.ruajKodbar(this.IdArtikulli, this.Pershkrimi, this.Njesia, idndermarje,this.detajim1,this.detajim2);
            data.Dispose();
            return u_ruajt;
        }
        
        /// <summary>
        /// Modifikon objektin kodbar artikulli ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbInventari.clsDatabaseInventari.modifikoKodbar"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo</returns>     
        public clsMesazh modifiko()
        {
            clsDatabaseInventari data = new clsDatabaseInventari();
            clsMesazh u_modifikua = data.modifikoKodbar(this.IdKodbari, this.IdArtikulli, this.Pershkrimi, this.njesia,this.detajim1,this.detajim2);
            data.Dispose();
            return u_modifikua;
        }

        /// <summary>
        /// Fshin objektin kodbar artikulli ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbInventari.clsDatabaseInventari.fshiKodbar"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>     
        public clsMesazh fshi()
        {
            clsDatabaseInventari data = new clsDatabaseInventari();
            clsMesazh u_fshi = data.fshiKodbar(this.IdKodbari);
            data.Dispose();
            return u_fshi;
        }

        /// <summary>
        /// Merr objektin kodbar artikulli nga tabela perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbInventari.clsDatabaseInventari.merrKodbar"/> 
        /// </summary>       
        public void merr()
        {
            clsDatabaseInventari data = new clsDatabaseInventari();
            data.merrKodbar(this.IdKodbari);
            data.Dispose();
        }
     public  bool   ktheKodbarSipasPershkrimit (string pershrkim,int idNdermarrje)
        {
            clsDatabaseInventari db = new clsDatabaseInventari();
          return  mbushKodbarin(db.ktheKodbarSipasPershkrimit(pershrkim,idNdermarrje));
        }
        public static string ktheKodbarSipasIdArtikulliNjesiaKodbariIPare(string kodartikulli, int idNdermarrje)
        {
           
            using (clsDatabaseInventari db = new clsDatabaseInventari())
            {
                return db.merrKodbarinSipasIdArtikulli(kodartikulli, idNdermarrje);
            }
        }

        public static int ktheIdKodbarSipasIdArtikulliNjesiaKodbariIPare(string kodartikulli, int idNdermarrje)
        {

            using (clsDatabaseInventari db = new clsDatabaseInventari())
            {
                return db.merrIdKodbarinSipasIdArtikulli(kodartikulli, idNdermarrje);
            }
        }
        public static int MerrNjesiKodbari(int idartikulli, string kodbari)
        {
            using (clsDatabaseInventari db = new clsDatabaseInventari())
            {
                return db.merrNjesiKodbari(idartikulli, kodbari);
            }
        }
        public static int MerrIdBarkodiSipasPershkrimDheArtikulli(string barkodi, int idArtikulli)
        {
            using (clsDatabaseInventari db = new clsDatabaseInventari())
            {
                return db.MerrIdBarkodiSipasPershkrimiDheIdArtikulli(barkodi, idArtikulli);
            }
        }
        public static int MerrIdBarkodiSipasPershkrimit(string barkodi)
        {
            using (clsDatabaseInventari db = new clsDatabaseInventari())
            {
                return db.MerrIdBarkodiSipasPershkrimit(barkodi);
            }
        }

        public static bool eshteBarkodILidhur(int idBarkod)
        {
            using (clsDatabaseInventari db = new clsDatabaseInventari())
            {
                return eshteBarkodILidhur(idBarkod, db);
            }
        }

        public static bool eshteBarkodILidhur(int idBarkod, clsDatabaseInventari db)
        {
            return db.eshteBarkodILidhur(idBarkod);
        }

        public static bool ekzistonKodbar(string pershkrimi, int idndermarje)
        {
            using (clsDatabaseInventari db = new clsDatabaseInventari())
            {
                return ekzistonKodbar(pershkrimi, idndermarje, db);
            }
        }

        public static bool ekzistonKodbar(string pershkrimi, int idndermarje, clsDatabaseInventari db)
        {
            return db.ekzistonKodbar(pershkrimi, idndermarje);
        }
        
        #endregion


        #region Metoda Internal

        /// <summary>
        /// mbush kodbarin nga databaza
        /// </summary>
        /// <param name="dbDataRowKodbar">datarow qe duhet mbushur nga databaza</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushKodbarin(DataRow dbDataRowKodbar)
        {
            if (dbDataRowKodbar != null)
            {

                try
                {
                    int.TryParse(dbDataRowKodbar["IDKODBARI"].ToString(), out idKodbari);
                    int.TryParse(dbDataRowKodbar["IDARTIKULLI"].ToString(), out idArtikulli);
                    pershkrimi = dbDataRowKodbar["PERSHKRIMI"].ToString();
                    int.TryParse(dbDataRowKodbar["NJESIA"].ToString(), out njesia);
                    int.TryParse(dbDataRowKodbar["DETAJIM1"].ToString(), out detajim1);
                    int.TryParse(dbDataRowKodbar["DETAJIM2"].ToString(), out detajim2);

                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se Kodbarit nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion

    }
}