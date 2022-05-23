using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DbCore.IMBUtils.Fiskalizimi.Controls;

namespace DbCore.DbInventari
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne njesite e artikullit
    ///  (Te dhenat  merren nga tabela : T_NJESIARTIKULLI)
    /// </summary>
    public class clsNjesiArtikulli
    { 

        #region Atribute

        private int idNjesia;
        private string kodNjesia;
        private string  pershkrimNjesia;
        private int idPerdoruesi;
        //private int idNderViti;
        private int idNdermarje;
        private int idStatusDok;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private DataRow rreshti;
        private string kodEinvoice;
        #endregion 

        #region Konstruktoret

        /// <summary>
        /// konstruktori me parametra
        /// </summary>
        /// <param name="kodNjesia"> kod njesia</param>
        /// <param name="pershkrimNjesia">pershkrim njesia</param>
        /// <param name="idPerdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNderViti"> id ndermarje viti</param>
        /// <param name="idnderm"> id ndermarje</param>
        public clsNjesiArtikulli( string kodNjesia, String pershkrimNjesia, int idPerdoruesi, int idnderm, int idstatusdok, string kodEinvoice)
        {
            this.kodNjesia = kodNjesia;
            this.pershkrimNjesia = pershkrimNjesia;
            this.idPerdoruesi = idPerdoruesi;
            //this.idNderViti = idNderViti;
            this.idNdermarje = idnderm;
            this.idStatusDok = idstatusdok;
            this.kodEinvoice = kodEinvoice;
        }
        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idnjesia">id e njesise</param>
        public clsNjesiArtikulli(int idnjesia)
        {
            clsDatabaseInventari dbNjesiArtikujsh = new clsDatabaseInventari();
            mbushNjesiArtikulli(dbNjesiArtikujsh.merrNjesiArtikulli(idnjesia));
            dbNjesiArtikujsh.Dispose();
        }

        public clsNjesiArtikulli(int idnjesia,clsDatabaseInventari dbNjesiArtikujsh)
        {         
            mbushNjesiArtikulli(dbNjesiArtikujsh.TransCache.getNjesiArt(idnjesia, dbNjesiArtikujsh));           
        } 
        
        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public clsNjesiArtikulli()
        { 
        }

        public clsNjesiArtikulli(string kodnjesia, int idnderm, clsDatabaseInventari dbNjesiArtikujsh)
        {
            mbushNjesiArtikulli(dbNjesiArtikujsh.TransCache.getNjesiArt(kodnjesia, idnderm, dbNjesiArtikujsh));
            //mbushNjesiArtikulli(dbNjesiArtikujsh.merrNjesiArtikulliMeKod(kodnjesia, idnderm));
        }

        public clsNjesiArtikulli(string kodnjesia, int idnderm)
        {
            using (clsDatabaseInventari dbNjesiArtikujsh = new clsDatabaseInventari())
                mbushNjesiArtikulli(dbNjesiArtikujsh.TransCache.getNjesiArt(kodnjesia, idnderm, dbNjesiArtikujsh));
        }


        public clsNjesiArtikulli(DataRow rreshti)
        {
            
            mbushNjesiArtikulli(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdNjesia
        {
            get { return idNjesia  ; }
            set { idNjesia  = value; }
        }
        /// <summary>
        /// Kthen/Vendos kod njesia.
        /// </summary>
        public String KodNjesia
        {
            get { return kodNjesia ; }
            set { kodNjesia  = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e perdoruesit.
        /// </summary>
        public int IdPerdoruesi
        {
            get { return idPerdoruesi ; }
            set { idPerdoruesi  = value; }
        }
        /// <summary>
        /// Kthen/Vendos pershkrimin e njesise.
        /// </summary>
        public String PershkrimNjesia
        {
            get { return pershkrimNjesia ; }
            set { pershkrimNjesia  = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne  e ndermarje vitit.
        /// </summary>
        //public int IdNderViti
        //{
        //    get { return idNderViti; }
        //    set { idNderViti = value; }
        //}
        /// <summary>
        /// Kthen/Vendos ID-ne  e ndermarjes.
        /// </summary>
        public int IdNdermarje
        {
            get { return idNdermarje; }
            set { idNdermarje = value; }
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
        public string KodEinvoice
        {
            get { return kodEinvoice; }
            set { kodEinvoice = value; }
        }

        #endregion

        #region Metoda Publike
        /// <summary>
        /// konstruktor me 2 parametra
        /// </summary>
        /// <param name="pershkrimnjesia">pershkrim i njesise</param>
        /// <param name="idnderm">id e ndermarrjes</param>
        public bool mbushNjesiArtikulliMePershk(string pershkrimnjesia, int idnderm)
        {
            clsDatabaseInventari dbNjesiArtikujsh = new clsDatabaseInventari();
            bool sukses = mbushNjesiArtikulli(dbNjesiArtikujsh.merrNjesiArtikulliMePershk(pershkrimnjesia, idnderm));
            dbNjesiArtikujsh.Dispose();
            return sukses;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="kodnjesia"></param>
        /// <param name="idnderm"></param>
        /// <returns></returns>
        public bool mbushNjesiArtikulliMeKod(string kodnjesia, int idnderm)
        {
            using (clsDatabaseInventari dbNjesiArtikujsh = new clsDatabaseInventari()) {
                return mbushNjesiArtikulliMeKod(kodnjesia, idnderm, dbNjesiArtikujsh);
            }
        } 
        public bool mbushNjesiArtikulliMeKod(string kodnjesia, int idnderm,clsDatabaseInventari dbNjesiArtikujsh)
        {           
            return mbushNjesiArtikulli(dbNjesiArtikujsh.merrNjesiArtikulliMeKod(kodnjesia, idnderm));
        }
        public static bool ekzistonNjesiArtikulliMeKod(string kodnjesia, int idnderm) 
        {
            clsDatabaseInventari dbNjesiArtikujsh = new clsDatabaseInventari();
            bool sukses = dbNjesiArtikujsh.ekzistonNjesiArtikulli(kodnjesia, idnderm);
            dbNjesiArtikujsh.Dispose();
            return sukses;
        }

        public clsMesazh kontrollotransferim(clsNjesiArtikulli kod, int idndermarje, clsDatabaseInventari db, int idperdoruesi)
        {
            clsMesazh mesazh = new clsMesazh(true, "Transferimi mbaroi me sukses!");
            if (!db.ekzistonNjesiArtikulli(kod.KodNjesia, idndermarje))
            {
                kod.idPerdoruesi = idperdoruesi;
                kod.idNdermarje = idndermarje;
                mesazh = kod.ruaj(db);
                if (!mesazh.Status)
                    return mesazh;
            }
            else
            {
                clsNjesiArtikulli kodnderm = new clsNjesiArtikulli(kod.kodNjesia, idndermarje, db);                     
                kod.idNjesia = kodnderm.idNjesia;
                if (kodnderm.dtModifikimi < kod.dtModifikimi)
                {
                    kod.idPerdoruesi = idperdoruesi;
                    kod.idNdermarje = idndermarje;

                    mesazh = kod.modifiko(db);
                    if (!mesazh.Status)
                        return mesazh;
                }

            }
            return mesazh;
        }
        /// <summary>
        /// Ruan objektin  njesi artikulli ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbInventari.clsDatabaseInventari.ruajNjesiArtikulli"/> 
        /// </summary>
            /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
     
        public clsMesazh ruaj()
        {
            clsDatabaseInventari data = new clsDatabaseInventari();
            clsMesazh u_ruajt = ruaj(data); 
            data.Dispose();
            //clsMesazh u_ruajt = data.ruajNjesiArtikulli(this);
            return u_ruajt;
        } 
        public clsMesazh ruaj( clsDatabaseInventari data )
        {
          
            int id;
            clsMesazh u_ruajt = data.ruajNjesiArtikulli(out id, this.KodNjesia, this.PershkrimNjesia, this.IdPerdoruesi, this.IdNdermarje, this.idStatusDok, this.kodEinvoice, clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizim());
            this.idNjesia = id;
            return u_ruajt;
        }
        /// <summary>
        /// Modifikon objektin  njesi artikulli ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbInventari.clsDatabaseInventari.modifikoNjesiArtikulli"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo</returns>
    
        public clsMesazh modifiko()
        {
            clsDatabaseInventari data = new clsDatabaseInventari();
            clsMesazh u_modifikua = modifiko(data); 
            data.Dispose();
            //clsMesazh u_modifikua = data.modifikoNjesiArtikulli(this);
            return u_modifikua;
        }
        public clsMesazh modifiko(clsDatabaseInventari data)
        {
            clsMesazh u_modifikua = data.modifikoNjesiArtikulli(this.IdNjesia, this.KodNjesia, this.PershkrimNjesia, this.IdPerdoruesi, this.IdNdermarje, this.idStatusDok, this.kodEinvoice, clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizim());
           
            return u_modifikua;
        }
        /// <summary>
        /// Fshin objektin  njesi artikulli ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbInventari.clsDatabaseInventari.fshiNjesiArtikulli"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
    
        public clsMesazh fshi()
        {
            clsDatabaseInventari data = new clsDatabaseInventari();
            clsMesazh u_fshi = data.fshiNjesiArtikulliStatus(this.IdNjesia, this.idPerdoruesi);
            data.Dispose();
            //clsMesazh u_fshi = data.fshiNjesiArtikulli(this);
            return u_fshi;
        }
        /// <summary>
        /// Merr objektin  njesi artikulli nga tabela perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbInventari.clsDatabaseInventari.merrNjesiArtikulli"/> 
        /// </summary>
      
        public void merr()
        {
            clsDatabaseInventari data = new clsDatabaseInventari();
            data.merrNjesiArtikulliPakthyer(this.IdNjesia);
            data.Dispose();
            //data.merrNjesiArtikulli(this);
        }

        /// <summary>
        /// Metode e klases, jo e objektit. Kthen nje id njesise se artikullit sipas kodit dhe idndermarrjes
        /// </summary>
        /// <param name="kod">kodi i artikullit</param>
        /// <param name="idndermarje">id e ndermarrjes</param>
        /// <returns>id e njesise se artikullit</returns>
        public static int ktheIdNjesiArtikulli(string kod, int idndermarje)
        {
            using (clsDatabaseInventari dbNjesiArtikulli = new clsDatabaseInventari()) {
                return (dbNjesiArtikulli.ktheIdNjesiArtikulliPerArtikull(kod, idndermarje));
            }
        }

        public static int ktheIdNjesiArtikulliSipasArtikulli(string kod, int idndermarje, int idArtikull)
        {
            using (clsDatabaseInventari dbNjesiArtikulli = new clsDatabaseInventari())
            {
                return (dbNjesiArtikulli.ktheIdNjesiArtikulliSipasArtikulli(kod, idndermarje, idArtikull));
            }
        }

        #endregion

        #region Metoda Internal
        private void mbushNjesiArtikulli(clsNjesiArtikulli njesia)
        {
            idNjesia = njesia.idNjesia;
            kodNjesia = njesia.kodNjesia;
            pershkrimNjesia = njesia.pershkrimNjesia;
            idPerdoruesi = njesia.idPerdoruesi;
            idNdermarje = njesia.idNdermarje;
            idStatusDok = njesia.idStatusDok;
            dtKrijimi = njesia.dtKrijimi;
            dtModifikimi = njesia.dtModifikimi;
            rreshti = njesia.rreshti;
        }
        /// <summary>
        /// mbushja e njesise se artikullit nga databaza
        /// </summary>
        /// <param name="dbDataRowNjesiArtikulli">datarow qe duhet mbushur nga databaza</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        internal bool mbushNjesiArtikulli(DataRow dbDataRowNjesiArtikulli)
        {
            if (dbDataRowNjesiArtikulli != null)
            {
                try
                {

                    int.TryParse(dbDataRowNjesiArtikulli["IDNJESIA"].ToString(), out idNjesia);
                    kodNjesia = dbDataRowNjesiArtikulli["KODNJESIA"].ToString();
                    pershkrimNjesia = dbDataRowNjesiArtikulli["PERSHKRIMNJESIA"].ToString();
                    int.TryParse(dbDataRowNjesiArtikulli["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    //int.TryParse(dbDataRowNjesiArtikulli["IDNDERVITI"].ToString(), out idNderViti);
                    int.TryParse(dbDataRowNjesiArtikulli["IDNDERMARJE"].ToString(), out idNdermarje);
                    int.TryParse(dbDataRowNjesiArtikulli["IDSTATUSDOK"].ToString(), out idStatusDok);
                    DateTime.TryParse(dbDataRowNjesiArtikulli["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRowNjesiArtikulli["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    if (dbDataRowNjesiArtikulli.Table.Columns.Contains("KODIEINVOICE"))
                        kodEinvoice = dbDataRowNjesiArtikulli["KODIEINVOICE"].ToString();

                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se njesise se artikullit nga db-ja");
                }
            }
            else
                return false;

        }

        #endregion
    }
}
