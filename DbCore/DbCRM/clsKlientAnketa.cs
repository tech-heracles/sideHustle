using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbCRM
{
    /// <summary>
    /// Kjo eshte klasa qe perfaqeson  lidhjen klient ankete
    ///  (Te dhenat  merren nga tabela : T_CRM_KLIENT_ANKETA)
    /// </summary>
    public class clsKlientAnketa
    {
        #region Atribute

        private int idKlientAnkete;
        private int idKlient;
        private int idKokaAnketa;
        private int idStatusDok;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private int idKrijuesi;
        private int idModifikuesi;
        private int idNdermarrje;
       

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdKlientAnkete
        {
            get { return idKlientAnkete; }
            set { idKlientAnkete = value; }
        }
        /// <summary>
        /// Kthen/Vendos id e klientit.
        /// </summary>
        public int IdKlient
        {
            get { return idKlient; }
            set { idKlient = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e kokes se anketes.
        /// </summary>
        public int IdKokaAnketa
        {
            get { return idKokaAnketa; }
            set { idKokaAnketa = value; }
        }
        /// <summary>
        /// Kthen/Vendos id e krijuesit
        /// </summary>
        public int IdKrijuesi
        {
            get { return idKrijuesi; }
            set { idKrijuesi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne  e modifikuesit.
        /// </summary>
        public int IdModifikuesi
        {
            get { return idModifikuesi; }
            set { idModifikuesi = value; }
        }

        /// <summary>
        /// Kthen/Vendos idstatusdok
        /// </summary>
        public int IdStatusDok
        {
            get { return idStatusDok; }
            set { idStatusDok = value; }
        }
        /// <summary>
        /// Kthen/Vendos daten e krijimit
        /// </summary>
        public DateTime DtKrijimi
        {
            get { return dtKrijimi; }

        }
        /// <summary>
        /// Kthen/Vendos daten e modifikimit
        /// </summary>
        public DateTime DtModifikimi
        {
            get { return dtModifikimi; }

        }

        /// <summary>
        /// Kthen/Vendos id e ndermarrjes
        /// </summary>
        public int IdNdermarrje
        {
            get { return idNdermarrje; }
            set { idNdermarrje = value; }
        }

    
        #endregion

        #region Konstruktoret

        /// <summary>
        /// konstruktor me parametra clsKlientAnketa
        /// </summary>
        /// <param name="idKlientAnkete">id e tabeles KlientAnketa</param>
        /// <param name="idKlient">id e klientit qe do lidhet me anketen</param>
        /// <param name="idKokaAnketa">idKoka e anketes se krijuar</param>
        /// <param name="idStatusDok">status i KlientAnketes</param>
        /// <param name="dtKrijimi">dt e krijimit te klient-anketes</param>
        /// <param name="dtModifikimi">dt e modifikimit te lidhjes klient ankete</param>
        /// <param name="idKrijuesi">id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idModifikuesi">id e perdoruesit qe ka modifikuar lidhjen klient anketen</param>
        public clsKlientAnketa(int idKlientAnkete, int idKlient, int idKokaAnketa, int idStatusDok, DateTime dtKrijimi, DateTime dtModifikimi, int idKrijuesi, int idModifikuesi, int idNdermarrje)
        {
            this.idKlientAnkete = idKlientAnkete;
            this.idKlient = idKlient;
            this.idKokaAnketa = idKokaAnketa;
            this.dtModifikimi = dtModifikimi;
            this.idKrijuesi = idKrijuesi;
            this.idModifikuesi = idModifikuesi;
            this.dtKrijimi = dtKrijimi;
            this.idStatusDok = idStatusDok;
            this.idNdermarrje = idNdermarrje;
        
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idKlientAnkete">idKlientAnkete</param>
        public clsKlientAnketa(int idKlientAnkete)
        {
            clsDatabaseCRM dbKlientAnketa = new clsDatabaseCRM();
            mbushKlientAnketa(dbKlientAnketa.merrKlientAnketa(idKlientAnkete));
            dbKlientAnketa.Dispose();
        }

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public clsKlientAnketa()
        {
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// Ruan objektin  njesi artikulli ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbCRM.clsDatabaseCRM.ruajKlientAnketa"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
        public clsMesazh ruaj()
        {
            clsDatabaseCRM data = new clsDatabaseCRM();
            clsMesazh u_ruajt = ruaj(data);
            data.Dispose();
            return u_ruajt;
        }

        public clsMesazh ruaj(clsDatabaseCRM data)
        {
            int id;
            clsMesazh u_ruajt = data.ruajKlientAnketa(out id, this.IdKlient, this.IdKokaAnketa, this.idKrijuesi, this.idStatusDok, this.idNdermarrje);
            this.idKlientAnkete = id;
            return u_ruajt;
        }

        /// <summary>
        /// Modifikon objektin  KlientAnkete ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbCRM.clsDatabaseCRM.modifikoKlientAnkete"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo</returns>
        public clsMesazh modifiko()
        {
            clsDatabaseCRM data = new clsDatabaseCRM();
            clsMesazh u_modifikua = modifiko(data);
            data.Dispose();
            return u_modifikua;
        }

        public clsMesazh modifiko(clsDatabaseCRM data)
        {
            clsMesazh u_modifikua = data.modifikoKlientAnkete(this.idKlientAnkete, this.idKlient, this.idKokaAnketa, this.idModifikuesi, this.idStatusDok);
            return u_modifikua;
        }

        /// <summary>
        /// Fshin objektin  njesi artikulli ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbInventari.clsDatabaseInventari.fshiNjesiArtikulli"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public static clsMesazh fshi(int idKlientAnkete, int idModifikuesi)
        {
            clsDatabaseCRM data = new clsDatabaseCRM();
            clsMesazh u_fshi = data.fshiKlientAnketaStatus(idKlientAnkete, idModifikuesi);
            data.Dispose();
            return u_fshi;
        }

        public static clsMesazh fshiSipasKlientAktive(int idKlient, int idModifikuesi)
        {
            clsDatabaseCRM data = new clsDatabaseCRM();
            clsMesazh u_fshi = data.fshiKlientAnketaStatusSipasKlientit(idKlient, idModifikuesi);
            data.Dispose();
            return u_fshi;
        }

        public static bool ekzistonKlientAnketa(int idKlient, int idKokaAnketa)
        {
            clsDatabaseCRM db = new clsDatabaseCRM();
            bool ekziston = ekzistonKlientAnketa(idKlient, idKokaAnketa, db);
            db.Dispose();
            return ekziston;
        }

        public static bool ekzistonKlientAnketa(int idKlient, int idKokaAnketa, clsDatabaseCRM db)
        {
            //clsDatabaseCRM db = new clsDatabaseCRM();
            bool ekziston = db.ekzistonKlientAnketa(idKlient, idKokaAnketa);
            //db.Dispose();
            return ekziston;
        }
        public static bool eshteLidhur(int idKokaAnkete)
        {
            using (clsDatabaseCRM db = new clsDatabaseCRM())
            {
                return db.EshteAnketaELidhur(idKokaAnkete);
            }
        }


        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbushja e njesise se artikullit nga databaza
        /// </summary>
        /// <param name="dbDataRowKlientAnketa">datarow qe duhet mbushur nga databaza</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        internal bool mbushKlientAnketa(DataRow dbDataRowKlientAnketa)
        {
            if (dbDataRowKlientAnketa != null)
            {
                try
                {
                    int.TryParse(dbDataRowKlientAnketa["IDKLIENTANKETE"].ToString(), out idKlientAnkete);
                    int.TryParse(dbDataRowKlientAnketa["IDKLIENT"].ToString(), out idKlient);
                    int.TryParse(dbDataRowKlientAnketa["IDKOKAANKETA"].ToString(), out idKokaAnketa);
                    int.TryParse(dbDataRowKlientAnketa["IDKRIJUESI"].ToString(), out idKrijuesi);
                    int.TryParse(dbDataRowKlientAnketa["IDMODIFIKUESI"].ToString(), out idModifikuesi);
                    int.TryParse(dbDataRowKlientAnketa["IDSTATUSDOK"].ToString(), out idStatusDok);
                    DateTime.TryParse(dbDataRowKlientAnketa["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRowKlientAnketa["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    int.TryParse(dbDataRowKlientAnketa["IDNDERMARRJE"].ToString(), out idNdermarrje);
                    //int.TryParse(dbDataRowKlientAnketa["LLOJI"].ToString(), out lloji);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se lidhjes klient ankete nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}