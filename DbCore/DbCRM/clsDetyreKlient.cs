using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DbCore.DbCRM
{
  public class clsDetyreKlient
    {

        #region Atribute

        private int idDetyreKlient;
        private int idKlient;
        private int idDetyre;
        private int idStatusDok;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private int idKrijuesi;
        private int idModifikuesi;
      private int idNdermarrje;
      private DateTime dtFillimi;
      private DateTime dtMbarimi;

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdDetyreKlient
        {
            get { return idDetyreKlient; }
            set { idDetyreKlient = value; }
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
        public int IdDetyre
        {
            get { return idDetyre; }
            set { idDetyre = value; }
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
        
        public int IdNdermarrje{
            get{return idNdermarrje;}
            set{idNdermarrje=value;}
        }
      public DateTime DtFillimi{
          get{return dtFillimi;}
          set{dtFillimi=value;}
      }
      public DateTime DtMbarimi{get{return dtMbarimi;}set{dtMbarimi=value;}}
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
        public clsDetyreKlient(int idDetyreKlient, int idKlient, int idDetyre, int idStatusDok, DateTime dtKrijimi, DateTime dtModifikimi, int idKrijuesi, int idModifikuesi,int idNdermarrje,DateTime dtFillimi,DateTime dtMbarimi)
        {
            this.idDetyreKlient = idDetyreKlient;
            this.idKlient = idKlient;
            this.IdDetyre = idDetyre;
            this.dtModifikimi = dtModifikimi;
            this.idKrijuesi = idKrijuesi;
            this.idModifikuesi = idModifikuesi;
            this.dtKrijimi = dtKrijimi;
            this.idStatusDok = idStatusDok;
            this.idNdermarrje=idNdermarrje;
            this.dtFillimi=dtFillimi;
            this.dtMbarimi=dtMbarimi;
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idKlientAnkete">idKlientAnkete</param>
        public clsDetyreKlient(int idDetyreKlient)
        {
            clsDatabaseCRM dbCRM = new clsDatabaseCRM();
            mbushDetyreKlient(dbCRM.merrDetyreKlient(idDetyreKlient));
            dbCRM.Dispose();
        }

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public clsDetyreKlient()
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
            clsMesazh u_ruajt = data.ruajDetyreKlient(out id, this.IdKlient, this.IdDetyre,this.idNdermarrje,this.dtFillimi,this.dtMbarimi, this.idKrijuesi, this.idStatusDok);
            this.idDetyreKlient = id;
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
            clsMesazh u_modifikua = data.modifikoDetyreKlient(this.idDetyreKlient, this.idKlient, this.IdDetyre,this.idNdermarrje,this.dtFillimi,this.dtMbarimi, this.idModifikuesi, 1);

            return u_modifikua;
        }
        /// <summary>
        /// Fshin objektin  njesi artikulli ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbInventari.clsDatabaseInventari.fshiNjesiArtikulli"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>

        public static clsMesazh fshi(int idDetyreKlient, int idModifikuesi)
        {
            clsDatabaseCRM data = new clsDatabaseCRM();
            clsMesazh u_fshi = data.fshiDetyreKlient(idDetyreKlient, idModifikuesi);
            data.Dispose();
            return u_fshi;
        } 
     
        public static bool ekzistonDetyraPerKeteKlient(int idKlient, int idDetyra,int idNdermarrje,DateTime dtFillimi,DateTime dtMbarimi)
        {
            clsDatabaseCRM db = new clsDatabaseCRM();
            bool ekziston = ekzistonDetyraPerKeteKlient(idKlient, idDetyra,idNdermarrje,dtFillimi,dtMbarimi, db);
            db.Dispose();
            return ekziston;
        }

        public static bool ekzistonDetyraPerKeteKlient(int idKlient, int idDetyra,int idNdermarrje,DateTime dtFillimi ,DateTime dtMbarimi,clsDatabaseCRM db)
        {
            //clsDatabaseCRM db = new clsDatabaseCRM();
            bool ekziston = db.ekzistonDetyreKlient(idKlient, idDetyra,idNdermarrje,dtFillimi,dtMbarimi);
            //db.Dispose();
            return ekziston;
        }

        #endregion
        #region Metoda Internal

        /// <summary>
        /// mbushja e njesise se artikullit nga databaza
        /// </summary>
        /// <param name="dbDataRowDetyreKlient">datarow qe duhet mbushur nga databaza</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        internal bool mbushDetyreKlient(DataRow dbDataRowDetyreKlient)
        {

            if (dbDataRowDetyreKlient != null)
            {

                try
                {

                    int.TryParse(dbDataRowDetyreKlient["IDDETYREKLIENT"].ToString(), out idDetyreKlient);
                    int.TryParse(dbDataRowDetyreKlient["IDKLIENT"].ToString(), out idKlient);
                    int.TryParse(dbDataRowDetyreKlient["IDDETYRE"].ToString(), out idDetyre);
                    int.TryParse(dbDataRowDetyreKlient["IDKRIJUESI"].ToString(), out idKrijuesi);
                    int.TryParse(dbDataRowDetyreKlient["IDMODIFIKUESI"].ToString(), out idModifikuesi);
                    int.TryParse(dbDataRowDetyreKlient["IDSTATUSDOK"].ToString(), out idStatusDok);
                    DateTime.TryParse(dbDataRowDetyreKlient["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRowDetyreKlient["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    int.TryParse(dbDataRowDetyreKlient["IDNDERMARRJE"].ToString(), out idNdermarrje);
                    DateTime.TryParse(dbDataRowDetyreKlient["DTFILLIMI"].ToString(), out dtFillimi);
                    DateTime.TryParse(dbDataRowDetyreKlient["DTMBARIMI"].ToString(), out dtMbarimi);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se lidhjes klient detyre nga db-ja");
                }
            }
            else
                return false;

        }

        #endregion


        public static bool kaPrerjeDetyrash(int idNdermarrje, int idKlienti, DateTime dtFillimi, DateTime dtMbarimi, int idDetyre)
        {

          
            clsDatabaseCRM data = new clsDatabaseCRM();
            bool kaPrerje = data.kaPrerjeVlefshmerieDetyra(idNdermarrje, idKlienti, idDetyre, dtFillimi, dtMbarimi);

            data.Dispose();
            return kaPrerje;
        }

      /// <summary>
      /// heq detyrat e panisura
      /// </summary>
      /// <param name="idKlient"></param>
      /// <param name="idModifikuesi"></param>
      /// <returns></returns>
        public static clsMesazh hiqDetyraTePaNisura(int idKlient,int idModifikuesi)
        {
            clsDatabaseCRM data = new clsDatabaseCRM();
            clsMesazh u_fshi = data.fshiDetyraTePaNisura(idKlient, idModifikuesi);
            data.Dispose();
            return u_fshi;
        }

        public static bool EshtePeriudhaReMeEVogelSeEvjetra(int idNdermarrje, int idKlienti, DateTime dtFillimi, DateTime dtMbarimi, int idDetyre)
        {
            clsDatabaseCRM dbCrm = new clsDatabaseCRM();
            return dbCrm.EshtePeriudhaReMeEVogelSeEvjetra(idNdermarrje, idKlienti, dtFillimi, dtMbarimi, idDetyre);


        }


    }
}

/*
 * [IDDETYREKLIENT]
      ,[IDKLIENT]
      ,[IDDETYRE]
      ,[IDSTATUSDOK]
      ,[DTKRIJIMI]
      ,[DTMODIFIKIMI]
      ,[IDKRIJUESI]
      ,[IDMODIFIKUESI]
      ,[IDNDERMARRJE]
      ,[DTFILLIMI]
      ,[DTMBARIMI]
 
 
 
 */