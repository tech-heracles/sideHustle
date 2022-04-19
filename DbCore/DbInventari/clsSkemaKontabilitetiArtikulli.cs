using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbInventari
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne skemat e kontabilitetit te artikullit
    ///  (Te dhenat  merren nga tabela : T_SKEMAKONTABILITETIARTIKULLI)
    /// </summary>
    public class clsSkemaKontabilitetiArtikulli
    {

        #region Atributet

        private int idSkemaKontabilitetiArtikulli;
        private string  kodiSkemaKontabilitetiArtikulli;
        private string pershkrimiSkemaKontabilitetiArtikulli;
        private int klasa;
        private int idLlogariInventari;
        private int idLlogariBlerje;
        private int idLlogariShitje;
        private int idLlogariTekTeTretet;   
        private int idLlogariShpenzimi;
        private int idLlogariAmortizimi;
        private int idLlogariPakesimi;
        private int idNdermarje;
        private string nrLlogariInventari;
        private string nrLlogariBlerje;
        private string nrLlogariShitje;
        private string nrLlogariTekTeTretet;
        private string nrLlogariShpenzimi; 
        private string nrLlogariAmortizimi;
        private string nrLlogariPakesimi;
        private bool llojiArt;
        private DataRow rreshti;

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdSkemaKontabilitetiArtikulli
        {
            get { return idSkemaKontabilitetiArtikulli; }
            set { idSkemaKontabilitetiArtikulli = value; }
        }
        /// <summary>
        /// Kthen/Vendos kodin e skemes.
        /// </summary>
        public string KodiSkemaKontabilitetiArtikulli
        {
            get { return kodiSkemaKontabilitetiArtikulli; }
            set { kodiSkemaKontabilitetiArtikulli = value; }
        }
        /// <summary>
        /// Kthen/Vendos pershkrimin e skemes.
        /// </summary>
        public string PershkrimiSkemaKontabilitetiArtikulli
        {
            get { return pershkrimiSkemaKontabilitetiArtikulli; }
            set { pershkrimiSkemaKontabilitetiArtikulli = value; }
        }
        /// <summary>
        /// Kthen/Vendos  klasen e skemes.
        /// </summary>
        public int Klasa
        {
            get { return klasa; }
            set { klasa = value; }
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
        /// Kthen/Vendos ID-ne e llogarise se blerjes.
        /// </summary>
        public int IdLlogariBlerje
        {
            get { return idLlogariBlerje; }
            set { idLlogariBlerje = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e llogarise se shitjes.
        /// </summary>
        public int IdLlogariShitje
        {
            get { return idLlogariShitje; }
            set { idLlogariShitje = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e llogarise tek te tretet.
        /// </summary>
        public int IdLlogariTekTeTretet
        {
            get { return idLlogariTekTeTretet; }
            set { idLlogariTekTeTretet = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e llogarise te shpenzimeve
        /// </summary>
        public int IdLlogariShpenzimi
        {
            get { return idLlogariShpenzimi; }
            set { idLlogariShpenzimi = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e llogarise te amortizimi
        /// </summary>
        public int IdLlogariAmortizimi
        {
            get { return idLlogariAmortizimi; }
            set { idLlogariAmortizimi = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e llogarise te pakesimit vlere dalje
        /// </summary>
        public int IdLlogariPakesimi
        {
            get { return idLlogariPakesimi; }
            set { idLlogariPakesimi = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e ndermarjes.
        /// </summary>
        public int IdNdermarje
        {
            get { return idNdermarje; }
            set { idNdermarje = value; }
        }
        /// <summary>
        /// Kthen/Vendos nr e llogarise se inventarit.
        /// </summary>
        public string NrLlogariInventari
        {
            get { return nrLlogariInventari; }
            set { nrLlogariInventari = value; }
        }
        /// <summary>
        /// Kthen/Vendos nr e llogarise se blerjes.
        /// </summary>
        public string NrLlogariBlerje
        {
            get { return nrLlogariBlerje; }
            set { nrLlogariBlerje = value; }
        }
        /// <summary>
        /// Kthen/Vendos nr e llogarise se shitjes.
        /// </summary>
        public string NrLlogariShitje
        {
            get { return nrLlogariShitje; }
            set { nrLlogariShitje = value; }
        }
        /// <summary>
        /// Kthen/Vendos nr e llogarise tek te tretet.
        /// </summary>
        public string NrLlogariTekTeTretet
        {
            get { return nrLlogariTekTeTretet; }
            set { nrLlogariTekTeTretet = value; }
        }
        /// <summary>
        /// Kthen/Vendos nr e llogarise se pakesimit.
        /// </summary>
        public string NrLlogariPakesimi
        {
            get { return nrLlogariPakesimi; }
            set { nrLlogariPakesimi = value; }
        }
        /// <summary>
        /// Kthen/Vendos nr e llogarise se shpenzimeve.
        /// </summary>
        public string NrLlogariShpenzimi
        {
            get { return nrLlogariShpenzimi; }
            set { nrLlogariShpenzimi = value; }
        }/// <summary>
        /// Kthen/Vendos nr e llogarise se amortizimi.
        /// </summary>
        public string NrLlogariAmortizimi
        {
            get { return nrLlogariAmortizimi; }
            set { nrLlogariAmortizimi = value; }
        }
        /// <summary>
        /// Kthen/Vendos nese artikulli eshte afatgjate apo afatshkurter.
        /// </summary>
        public bool LlojiArt
        {
            get { return llojiArt; }
            set { llojiArt = value; }
        }

        #endregion

        #region Konstruktoret

        /// <summary>
        /// konstruktori me parametra
        /// </summary>
        /// <param name="idSkemaKontabilitetiArtikulli"> id ritese e skemes se kontabilitetit te artikullit</param>
        /// <param name="kodiSkemaKontabilitetiArtikulli"> kodi i skemes</param>
        /// <param name="pershkrimiSkemaKontabilitetiArtikulli"> pershkrimi i skemes</param>
        /// <param name="klasa"> klasa e artikullit te ciles i perket kjo skeme</param>
        /// <param name="idLlogariInventari"> id e llogarise se inventarit</param>
        /// <param name="idLlogariBlerje"> id e llogarise se blerjes</param>
        /// <param name="idLlogariShitje"> id e llogarise se shitjes</param>
        /// <param name="idLlogariTekTeTretet"> id e llogarise tek te tretet</param>
        /// <param name="idLlogariShpenzimi"> id e llogarise shpenzimeve</param>
        /// <param name="idLlogariPakesimi"> id e llogarise se pakesimit</param>
        /// <param name="idnderm"> id e ndermarjes</param>
        public clsSkemaKontabilitetiArtikulli( int idSkemaKontabilitetiArtikulli,string  kodiSkemaKontabilitetiArtikulli, string pershkrimiSkemaKontabilitetiArtikulli, int klasa, int idLlogariInventari, int idLlogariBlerje, int idLlogariShitje, int idLlogariTekTeTretet, int idLlogariPakesimi, int idLlogariShpenzimi, int idLlogariAmortizimi, int idnderm, bool llojiart)
        {
            this.idSkemaKontabilitetiArtikulli = idSkemaKontabilitetiArtikulli;
            this.kodiSkemaKontabilitetiArtikulli = kodiSkemaKontabilitetiArtikulli;
            this.pershkrimiSkemaKontabilitetiArtikulli = pershkrimiSkemaKontabilitetiArtikulli;
            this.klasa = klasa;
            this.idLlogariInventari = idLlogariInventari;
            this.idLlogariBlerje = idLlogariBlerje;
            this.idLlogariShitje = idLlogariShitje;
            this.idLlogariTekTeTretet = idLlogariTekTeTretet;
            this.idLlogariShpenzimi = idLlogariShpenzimi;
            this.idLlogariAmortizimi = idLlogariAmortizimi;
            this.idNdermarje = idnderm;
            this.llojiArt = llojiart;
            this.idLlogariPakesimi = idLlogariPakesimi;
        }
        /// <summary>
        /// konstruktor me 2 parametra
        /// </summary>
        /// <param name="kodi">kodi</param>
        /// <param name="idnderm">id e ndermarrjes</param>
        public clsSkemaKontabilitetiArtikulli(String kodi, int idnderm,bool llojart)
        {
            clsDatabaseInventari dbSkemaKontabilitetArtikull = new clsDatabaseInventari();
            mbushSkemaKontabilitetiArtikull(dbSkemaKontabilitetArtikull.ktheSkemaKontabilitetiArtikulliSipasKodit(kodi, idnderm, llojart));
            dbSkemaKontabilitetArtikull.Dispose();
        } 
        public clsSkemaKontabilitetiArtikulli(String kodi, int idnderm,bool llojart,clsDatabaseInventari dbSkemaKontabilitetArtikull)
        {
             mbushSkemaKontabilitetiArtikull(dbSkemaKontabilitetArtikull.ktheSkemaKontabilitetiArtikulliSipasKodit(kodi, idnderm, llojart));
       
        }
        /// <summary>
        /// konstruktore me 1 parameter
        /// </summary>
        /// <param name="id">id e skemes</param>
        public clsSkemaKontabilitetiArtikulli(int id)
        {
            clsDatabaseInventari dbSkemaKontabilitetArtikull = new clsDatabaseInventari();
            mbushSkemaKontabilitetiArtikull(dbSkemaKontabilitetArtikull.ktheSkemaKontabilitetiArtikulli(id));
            dbSkemaKontabilitetArtikull.Dispose();
        } 
        public clsSkemaKontabilitetiArtikulli(int id,  clsDatabaseInventari dbSkemaKontabilitetArtikull)
        {
             mbushSkemaKontabilitetiArtikull(dbSkemaKontabilitetArtikull.ktheSkemaKontabilitetiArtikulli(id));
           
        }
        /// <summary>
        /// kostruktori pa parametra
        /// </summary>
        public clsSkemaKontabilitetiArtikulli()
        {
        }

        public clsSkemaKontabilitetiArtikulli(DataRow rreshti)
        {
            
            mbushSkemaKontabilitetiArtikull(rreshti);
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// Ruan objektin  skema kontabel artikulli ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbInventari.clsDatabaseInventari.ruajSkemaKontabilitetiArtikulli"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
     
        public clsMesazh ruaj()
        {
            clsDatabaseInventari data = new clsDatabaseInventari();
            int id;
            clsMesazh u_ruajt = data.ruajSkemaKontabilitetiArtikulli(out id, this.KodiSkemaKontabilitetiArtikulli, this.PershkrimiSkemaKontabilitetiArtikulli, this.Klasa, this.IdLlogariInventari, this.IdLlogariBlerje, this.IdLlogariShitje, this.IdLlogariTekTeTretet, this.IdLlogariShpenzimi,idLlogariAmortizimi, this.idLlogariPakesimi, this.IdNdermarje, this.llojiArt);
            data.Dispose();
            //clsMesazh u_ruajt = data.ruajSkemaKontabilitetiArtikulli(this);
            return u_ruajt;
        }
        /// <summary>
        /// Modifikon objektin  skema kontabel artikulli ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbInventari.clsDatabaseInventari.modifikoSkemaKontabilitetiArtikulli"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo</returns>
     
        public clsMesazh modifiko()
        {
            clsDatabaseInventari data = new clsDatabaseInventari();
            clsMesazh u_modifikua = data.modifikoSkemaKontabilitetiArtikulli(this.IdSkemaKontabilitetiArtikulli, this.KodiSkemaKontabilitetiArtikulli, this.PershkrimiSkemaKontabilitetiArtikulli, this.Klasa, this.IdLlogariInventari, this.IdLlogariBlerje, this.IdLlogariShitje, this.IdLlogariTekTeTretet, this.IdLlogariShpenzimi,idLlogariAmortizimi,idLlogariPakesimi, this.IdNdermarje, this.llojiArt);
            data.Dispose();
            //clsMesazh u_modifikua = data.modifikoSkemaKontabilitetiArtikulli(this);
            return u_modifikua;
        }
        /// <summary>
        /// Fshin objektin  skema kontabel artikulli ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbInventari.clsDatabaseInventari.fshiSkemaKontabilitetiArtikulli"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
     
        public clsMesazh fshi()
        {
            clsDatabaseInventari data = new clsDatabaseInventari();
            clsMesazh u_fshi = data.fshiSkemaKontabilitetiArtikulli(this.IdSkemaKontabilitetiArtikulli);
            data.Dispose();
            //clsMesazh u_fshi = data.fshiSkemaKontabilitetiArtikulli(this);
            return u_fshi;
        }
        /// <summary>
        /// Merr objektin  skema kontabel artikulli nga tabela perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbInventari.clsDatabaseInventari.ktheSkemaKontabilitetiArtikulli"/> 
        /// </summary>
        
        public void merr()
        {
            clsDatabaseInventari data = new clsDatabaseInventari();
            data.ktheSkemaKontabilitetiArtikulli(this.IdSkemaKontabilitetiArtikulli);
            data.Dispose();
            //data.merrSkemaKontabilitetiArtikulli(this.IdSkemaKontabilitetiArtikulli );
        }

        /// <summary>
        /// Metode e klases, jo e objektit. Kthen id e skemes se kontabilitetit te artikullit sipas kodit dhe idndermarrjes
        /// </summary>
        /// <param name="kod">kodi i artikullit</param>
        /// <param name="idndermarje">id e ndermarrjes</param>
        /// <returns>id e skemes se kontabilitetit te artikullit</returns>
        public static int ktheIdSkemaKontabilitetArtikulli(string kod, int idndermarje, bool llojart)
        {
            clsDatabaseInventari dbSkemaKontabilitetArtikulli = new clsDatabaseInventari();
            int idSkema = (dbSkemaKontabilitetArtikulli.ktheIdSkemaKontabilitetiArtikulliSipasKodit(kod, idndermarje,llojart));
            dbSkemaKontabilitetArtikulli.Dispose();
            return idSkema;
        }

       

        #endregion

        #region Metoda Internal

        /// <summary>
        ///  mbushja e skemes se kontabilitetit te artikullit nga databaza
        /// </summary>
        /// <param name="dbDataRowSkemaKontabilitetiArtikulli">datarow qe duhet mbushur nga databaza</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        internal bool mbushSkemaKontabilitetiArtikull(DataRow dbDataRowSkemaKontabilitetiArtikulli)
        {
            if (dbDataRowSkemaKontabilitetiArtikulli != null)
            {

                try
                {
                    int.TryParse(dbDataRowSkemaKontabilitetiArtikulli["IDSKEMAKONTABILITETIARTIKULLI"].ToString(), out idSkemaKontabilitetiArtikulli);
                    kodiSkemaKontabilitetiArtikulli = dbDataRowSkemaKontabilitetiArtikulli["KODISKEMAKONTABILITETIARTIKULLI"].ToString();
                    pershkrimiSkemaKontabilitetiArtikulli = dbDataRowSkemaKontabilitetiArtikulli["PERSHKRIMISKEMAKONTABILITETIARTIKULLI"].ToString();
                    int.TryParse(dbDataRowSkemaKontabilitetiArtikulli["KLASA"].ToString(), out klasa);
                    int.TryParse(dbDataRowSkemaKontabilitetiArtikulli["IDLLOGARIINVENTARI"].ToString(), out idLlogariInventari);
                    int.TryParse(dbDataRowSkemaKontabilitetiArtikulli["IDLLOGARIBLERJE"].ToString(), out idLlogariBlerje);
                    int.TryParse(dbDataRowSkemaKontabilitetiArtikulli["IDLLOGARISHITJE"].ToString(), out idLlogariShitje);
                    int.TryParse(dbDataRowSkemaKontabilitetiArtikulli["IDLLOGARITEKTETRETET"].ToString(), out idLlogariTekTeTretet);
                    int.TryParse(dbDataRowSkemaKontabilitetiArtikulli["IDLLOGARISHPENZIME"].ToString(), out idLlogariShpenzimi);
                    int.TryParse(dbDataRowSkemaKontabilitetiArtikulli["IDLLOGARIAMORTIZIMI"].ToString(), out idLlogariAmortizimi);
                    int.TryParse(dbDataRowSkemaKontabilitetiArtikulli["IDLLOGARIPAKESIM"].ToString(), out idLlogariPakesimi);
                    int.TryParse(dbDataRowSkemaKontabilitetiArtikulli["IDNDERMARJE"].ToString(), out idNdermarje);
                    nrLlogariInventari = dbDataRowSkemaKontabilitetiArtikulli["NRLLOGARIINVENTARI"].ToString();
                    nrLlogariBlerje = dbDataRowSkemaKontabilitetiArtikulli["NRLLOGARIBLERJE"].ToString();
                    nrLlogariShitje = dbDataRowSkemaKontabilitetiArtikulli["NRLLOGARISHITJE"].ToString();
                    nrLlogariTekTeTretet = dbDataRowSkemaKontabilitetiArtikulli["NRLLOGARITEKTETRETET"].ToString();
                    nrLlogariShpenzimi = dbDataRowSkemaKontabilitetiArtikulli["NRLLOGARISHPENZIME"].ToString();
                    nrLlogariAmortizimi = dbDataRowSkemaKontabilitetiArtikulli["NRLLOGARIAMORTIZIMI"].ToString();
                    nrLlogariPakesimi = dbDataRowSkemaKontabilitetiArtikulli["NRLLOGARIPAKESIMI"].ToString();
                   bool.TryParse(dbDataRowSkemaKontabilitetiArtikulli["LLOJIART"].ToString(), out llojiArt);

                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se skemes se kontabilitetit te artikullit nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion

    }
}
