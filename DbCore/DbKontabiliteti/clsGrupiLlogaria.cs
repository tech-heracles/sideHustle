using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbKontabiliteti
{
    /// <remarks>
    /// Kjo eshte klasa qe sherben per objektet qe perfaqesojne nje grup llogarie
    ///  (Te dhenat  merren nga tabela : T_GRUPILLOGARIA)
    ///</remarks>
    public  class clsGrupiLlogaria
    {
        #region Atribute

        private int idGrupiLlogaria;
        private int nrGrupiLlogaria;
        private String pershkrimiGrupiLlogaria;
        private int idNdermarje;
        private DataRow rreshti;

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktor i klases
        /// </summary>
        /// <param name="idgrupiLlogaria">Id e grupit te llogarise</param>
        /// <param name="nrgrupiLlogaria">Numri i grupit te llogarise</param>
        /// <param name="pershkrimigrupiLlogaria">Pershkrimi i grupit te llogarise</param>
        /// <param name="idndermarje">Id e ndermarrjes</param>
        public clsGrupiLlogaria(int idgrupiLlogaria, int nrgrupiLlogaria, String pershkrimigrupiLlogaria, int idndermarje)
        {
            idGrupiLlogaria = idgrupiLlogaria;
            nrGrupiLlogaria = nrgrupiLlogaria;
            pershkrimiGrupiLlogaria = pershkrimigrupiLlogaria;
            idNdermarje = idndermarje;
        }

        /// <summary>
        /// kostruktor me 2 parametra
        /// </summary>
        /// <param name="kodi">kodi i llogarise</param>
        /// <param name="idnderm">id e ndermarrjes</param>
        public clsGrupiLlogaria(string kodi, int idnderm, int idGjuha)
        {
            clsDatabaseKontabilitet dbGrupLlogari = new clsDatabaseKontabilitet();
            mbushGrupLlogaria(dbGrupLlogari.ktheGrupLlogaria(kodi, idnderm, idGjuha));
            dbGrupLlogari.Dispose();
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idgrupi">id e grupit</param>
        public clsGrupiLlogaria(int idgrupi, int idGjuha)
        {
            clsDatabaseKontabilitet dbGrupLlogari = new clsDatabaseKontabilitet();
            mbushGrupLlogaria(dbGrupLlogari.ktheGrupLlogaria(idgrupi, idGjuha));
            dbGrupLlogari.Dispose();
        }

        /// <summary>
        /// Konstruktor i klases
        /// </summary>
        public clsGrupiLlogaria()
        { 
        }

        public clsGrupiLlogaria(DataRow rreshti)
        {
            
            mbushGrupLlogaria(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos Id-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdGrupiLlogaria
        {
            get { return idGrupiLlogaria; }
            set { idGrupiLlogaria = value; }
        }

        /// <summary>
        /// Kthen/Vendos numrin e grupit te llogarise
        /// </summary>
        public int NrGrupiLlogaria
        {
            get { return nrGrupiLlogaria; }
            set { nrGrupiLlogaria = value; }
        }

        /// <summary>
        /// Kthen/Vendos pershkrimin e grupit te llogarise
        /// </summary>
        public String PershkrimiGrupiLlogaria
        {
            get { return pershkrimiGrupiLlogaria; }
            set { pershkrimiGrupiLlogaria = value; }
        }

        /// <summary>
        /// Kthen/Vendos Id-ne e ndermarrjes
        /// </summary>
        public int IdNdermarje
        {
            get { return idNdermarje; }
            set { idNdermarje = value; }
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// Ruan objektin e grupit te llogarise ne tabelen perkatese ne databaze.Therret funksionin
        /// <see cref="DbCore.DbKontabiliteti.clsDatabaseKontabilitet.ruajGrupLlogaria"/>
        /// </summary>
        /// <returns>Kthen true nese ruajtja perfundoi me sukses</returns>
        public clsMesazh ruaj()
        {
            clsDatabaseKontabilitet data = new clsDatabaseKontabilitet();
            int idG;
            clsMesazh u_ruajt = data.ruajGrupLlogaria(out idG, this.NrGrupiLlogaria, this.PershkrimiGrupiLlogaria, this.IdNdermarje);
            data.Dispose();
            return u_ruajt;
        }

        /// <summary>
        /// Modifikon objektin e grupit te llogarise ne tabelen perkatese ne databaze.Therret funksionin
        /// <see cref="DbCore.DbKontabiliteti.clsDatabaseKontabilitet.modifikoGrupLlogaria"/>
        /// </summary>
        /// <returns>Kthen true nese modifikimi perfundoi me sukses</returns>
        public clsMesazh modifiko()
        {
            clsDatabaseKontabilitet data = new clsDatabaseKontabilitet();
            clsMesazh u_modifikua = data.modifikoGrupLlogaria(this.IdGrupiLlogaria, this.NrGrupiLlogaria, this.PershkrimiGrupiLlogaria, this.IdNdermarje);
            data.Dispose();
            return u_modifikua;
        }

        /// <summary>
        /// Fshin objektin e grupit te llogarise ne tabelen perkatese ne databaze.Therret funksionin
        /// <see cref="DbCore.DbKontabiliteti.clsDatabaseKontabilitet.fshiGrupLlogaria"/>
        /// </summary>
        /// <returns>Kthen true nese fshirja perfundoi me sukses</returns>
        public clsMesazh fshi()
        {
            clsDatabaseKontabilitet data = new clsDatabaseKontabilitet();
            clsMesazh u_fshi = data.fshiGrupLlogaria(this.IdGrupiLlogaria);
            data.Dispose();
            return u_fshi;
        }        
        
        /// <summary>
        ///Kthen/Vendos nje collection me objekte grup llogarie
        /// </summary>
        public colGrupetLlogaria merriTeGjithe()
        {
            colGrupetLlogaria data = new colGrupetLlogaria();
            data.mbushGjitheGrupetLlogaria();
            return data;
            //clsDatabaseKontabilitet data = new clsDatabaseKontabilitet();
            //return data.merrGjitheGrupetLlogaria();

        }

        /// <summary>
        /// Metode e klases, jo e objektit. Kthen nje id e grupit te llogarise sipas kodit dhe idndermarrjes
        /// </summary>
        /// <param name="kodi">kodi i llogarise</param>
        /// <param name="idnderm">id e ndermarrjes</param>
        /// <returns>id e grup llogarise</returns>
        public static int mbushIDGrupLlogari(string kodi, int idnderm, int idGjuha)
        {
            clsDatabaseKontabilitet dbLlogari = new clsDatabaseKontabilitet();
            int id = (dbLlogari.ktheIDGrupLlogaria(kodi, idnderm, idGjuha));
            dbLlogari.Dispose();
            return id;
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush grup llogari nga databaza
        /// </summary>
        /// <param name="dbDataRowGrupLlogaria">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushGrupLlogaria(DataRow dbDataRowGrupLlogaria)
        {
            if (dbDataRowGrupLlogaria != null)
            {
                try
                {
                    int.TryParse(dbDataRowGrupLlogaria["IDGRUPILLOGARIA"].ToString(), out idGrupiLlogaria);
                    int.TryParse(dbDataRowGrupLlogaria["NRGRUPILLOGARIA"].ToString(), out nrGrupiLlogaria);
                    pershkrimiGrupiLlogaria = dbDataRowGrupLlogaria["PERSHKGRUPILLOGARIA"].ToString();
                    int.TryParse(dbDataRowGrupLlogaria["IDNDERMARJE"].ToString(), out idNdermarje);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se grupit te llogarive nga db-ja");
                }
            }
            else
                return false;
        }
        
        #endregion
    }
}

