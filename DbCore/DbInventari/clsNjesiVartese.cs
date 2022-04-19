using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
namespace DbCore.DbInventari
{



    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne  njesine vartese
    ///  (Te dhenat  merren nga tabela : T_NJESIVARTESE)
    /// </summary>
    public class clsNjesiVartese
    {
        #region Atribute

        private int idNjesiVartese;
        private string kodi;
        private string pershkrimi;
        private string adresa;
        private int idLlogari;
        private int idNdermarrje;
        private int idPerdorues;
        private int idKonfig;
        private int idStatusDok;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private string llogari = null;
        private DataRow rreshti;
        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases me parametra
        /// </summary>
        /// <param name="adr"> adresa e njesise vartese</param>
        /// <param name="idLlog">id llogari</param>
        /// <param name="idNjes"> id ritese e njesise </param>
        /// <param name="idPerd">id e perdoruesit qe e ka ruajtur</param>
        /// <param name="kod">kodi i njesise </param>
        /// <param name="nderm">id e ndermarjes</param>
        /// <param name="pershk"> pershkrimi</param>
        public clsNjesiVartese(int idNjes, string kod, string pershk, string adr, int idLlog, int nderm, int idPerd, int idKonfig, int idstatusdok)
        {
            idNjesiVartese = idNjes;
            kodi = kod;
            pershkrimi = pershk;
            adresa = adr;
            idLlogari = idLlog;

            idNdermarrje = nderm;
            idPerdorues = idPerd;
            this.idKonfig = idKonfig;

            this.idStatusDok = idstatusdok;
        }

        /// <summary>
        /// konstruktor me 2 parametra
        /// </summary>
        /// <param name="kodi">kodi i njesise </param>
        /// <param name="idNderm">id e ndermarrjes</param>
        public clsNjesiVartese(string kodi, int idNderm)
        {
            clsDatabaseInventari dbNjesi = new clsDatabaseInventari();
            if (!mbushNjesiVartese(dbNjesi.ktheNjesiVarteseSipasKodit(kodi, idNderm)))
                idNjesiVartese = -1;
            dbNjesi.Dispose();
        }

        public clsNjesiVartese(string kodi, int idNderm, clsDatabaseInventari dbNjesi)
        {

            if (!mbushNjesiVartese(dbNjesi.ktheNjesiVarteseSipasKodit(kodi, idNderm)))
                idNjesiVartese = -1;

        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idNjesi">id e njesise </param>
        public clsNjesiVartese(int idNjesi)
        {
            clsDatabaseInventari dbNjesi = new clsDatabaseInventari();
            mbushNjesiVartese(dbNjesi.ktheNjesiVarteseSipasiD(idNjesi));
            dbNjesi.Dispose();
        }

        public clsNjesiVartese(int idNjesi, clsDatabaseInventari dbNjesi)
        {

            mbushNjesiVartese(dbNjesi.ktheNjesiVarteseSipasiD(idNjesi));

        }

        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsNjesiVartese()
        {
        }

        public clsNjesiVartese(DataRow rreshti)
        {
            
            mbushNjesiVartese(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdNjesiVartese
        {
            get { return idNjesiVartese; }
            set { idNjesiVartese = value; }
        }

        /// <summary>
        /// Kthen/Vendos adresa e njesise .
        /// </summary>
        public string Adresa
        {
            get { return adresa; }
            set { adresa = value; }
        }

        /// <summary>
        /// Kthen/Vendos kodi i njesise .
        /// </summary>
        public string Kodi
        {
            get { return kodi; }
            set { kodi = value; }
        }

        /// <summary>
        /// Kthen/Vendos pershkrimi i njesise .
        /// </summary>
        public string Pershkrimi
        {
            get { return pershkrimi; }
            set { pershkrimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e llogarise.
        /// </summary>
        public int IdLlogari
        {
            get { return idLlogari; }
            set { idLlogari = value; }
        }


        /// <summary>
        /// Kthen/Vendos ID-ne e ndermarjes
        /// </summary>
        public int IdNdermarje
        {
            get { return idNdermarrje; }
            set { idNdermarrje = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e perdoruesit qe e ka kryer veprimin.
        /// </summary>
        public int IdPerdorues
        {
            get { return idPerdorues; }
            set { idPerdorues = value; }
        }


        /// <summary>
        /// Kthen/Vendos ID-ne e konfigurimit
        /// </summary>
        public int IdKonfig
        {
            get { return idKonfig; }
            set { idKonfig = value; }
        }
        public int IdStatusDok
        {
            get { return idStatusDok; }
            set { idStatusDok = value; }
        }
        public string Llogari
        {
            get
            {
                return llogari;
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
        #endregion

        #region Metoda Publike

        /// <summary>
        /// Ruan objektin e  njesise  ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseInventari.ruajNjesiAdministrative"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
        public clsMesazh ruaj()
        {
            clsDatabaseInventari data = new clsDatabaseInventari();
            int id;
            clsMesazh u_ruajt = data.ruajNjesiVartese(out id, this.Kodi, this.Pershkrimi, this.Adresa, this.IdLlogari, this.IdNdermarje, this.IdPerdorues, this.IdKonfig, this.idStatusDok);
            data.Dispose();
            this.IdNjesiVartese = id;
            return u_ruajt;
        }

        /// <summary>
        /// Modifikon objektin e  njesise  ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseInventari.modifikoNjesiAdministrative"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo</returns>
        public clsMesazh modifiko()
        {
            clsDatabaseInventari data = new clsDatabaseInventari();
            clsMesazh u_modifikua = data.modifikoNjesiVartese(this.IdNjesiVartese, this.Kodi, this.Pershkrimi, this.Adresa, this.IdLlogari, this.IdNdermarje, this.IdPerdorues, this.IdKonfig, this.idStatusDok);
            data.Dispose();
            return u_modifikua;
        }

        /// <summary>
        /// Fshin objektin e  njesise  ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseInventari.fshiNjesiAdministrative"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh fshi()
        {
            clsDatabaseInventari data = new clsDatabaseInventari();
            clsMesazh u_fshi = data.fshiNjesiVarteseStatus(this.IdNjesiVartese, this.idPerdorues);
            data.Dispose();
            return u_fshi;
        }

        /// <summary>
        /// merr te gjithe njesite  te nje ndermarje nga tabela perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseInventari.ktheGjitheNjesiAdministrative"/> 
        /// </summary>
        /// <returns > nje objekt colNjesiVartese me te gjitha njesite  te ndermarjeso</returns>
        public colNjesiVartese merriTeGjithe()
        {
            colNjesiVartese data = new colNjesiVartese();
            data.mbushGjitheNjesiVartese(this.IdNdermarje);
            return data;
        }

        /// <summary>
        /// merr objektin e  njesise  sipas kodit nga tabela perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseInventari.ktheNjesiAdministrativeSipasKodit"/> 
        /// </summary>
        /// <returns > nje objekt clsNjesiVartese me njesine  te kerkuar</returns>
        public clsNjesiVartese merrNjesiSipasKodit()
        {
            clsNjesiVartese data = new clsNjesiVartese(this.Kodi, this.IdNdermarje);
            return data;
        }

        /// <summary>
        /// Metode e klases, jo e objektit. kthen kodin e njesise  sipas id
        /// </summary>
        /// <param name="idNjesi">id e njesise </param>
        /// <returns>kodi i njesise </returns>
        public static string mbushKodiNjesiVarteseSipasiD(int idNjesi)
        {
            clsDatabaseInventari dbNjesi = new clsDatabaseInventari();
            string kodi = (dbNjesi.ktheKodiNjesiVarteseSipasiD(idNjesi));
            dbNjesi.Dispose();
            return kodi;
        }
        public static bool ekziston(string kodi, int idndermarje)
        {
            clsDatabaseInventari dbNjesi = new clsDatabaseInventari();
            bool ekz = (dbNjesi.ekzistonKodNjesiVartese(kodi, idndermarje));
            dbNjesi.Dispose();
            return ekz;
        }
        public static bool ekziston(string kodi, int idndermarje, clsDatabaseInventari dbNjesi)
        {
            bool ekz = (dbNjesi.ekzistonKodNjesiVartese(kodi, idndermarje));

            return ekz;
        }
        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush njesine  nga databaza
        /// </summary>
        /// <param name="dbDataRowNjesi">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushNjesiVartese(DataRow dbDataRowNjesi)
        {
            if (dbDataRowNjesi != null)
            {
                try
                {
                    int.TryParse(dbDataRowNjesi["IDNJESIVARTESE"].ToString(), out idNjesiVartese);
                    kodi = dbDataRowNjesi["KODI"].ToString();
                    pershkrimi = dbDataRowNjesi["PERSHKRIMI"].ToString();
                    adresa = dbDataRowNjesi["ADRESA"].ToString();
                    int.TryParse(dbDataRowNjesi["IDLLOGARI"].ToString(), out idLlogari);
                    int.TryParse(dbDataRowNjesi["IDNDERMARJE"].ToString(), out idNdermarrje);
                    int.TryParse(dbDataRowNjesi["IDPERDORUESI"].ToString(), out idPerdorues);
                    int.TryParse(dbDataRowNjesi["IDKONFIG"].ToString(), out idKonfig);
                    int.TryParse(dbDataRowNjesi["IDSTATUSDOK"].ToString(), out idStatusDok);
                    DateTime.TryParse(dbDataRowNjesi["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRowNjesi["DTMODIFIKIMI"].ToString(), out dtModifikimi);

                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se njesise  nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion

    }
}

