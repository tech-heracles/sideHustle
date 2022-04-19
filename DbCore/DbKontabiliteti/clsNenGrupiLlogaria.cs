using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbKontabiliteti
{
    /// <remarks>
    /// Kjo eshte klasa qe sherben per objektet qe perfaqesojne nje nengrup llogarie <example>Grupi "Inventare" ka nengrupet: Prodhim ne proces, produkte, mallra etj</example>
    ///  (Te dhenat  merren nga tabela : T_NENGRUPILLOGARIA)
    /// </remarks>
    public  class clsNenGrupiLlogaria
    {
        #region Atributet

        private int idNenGrupiLlogaria;
        private int nrNenGrupiLlogaria;
        private String pershkrimiNenGrupiLlogaria;
        private int idGrupiLlogaria;
        private int idNdermarje;
        private DataRow rreshti;
  
        #endregion 

        #region Konstruktoret
        
        /// <summary>
        /// Kontruktor i klases
        /// </summary>
        public clsNenGrupiLlogaria(int idnengrupiLlogaria, int nrnengrupiLlogaria, String pershkriminengrupiLlogaria,int idgrupiLlogaria, int idnderm)
        {
            idNenGrupiLlogaria = idnengrupiLlogaria;
            nrNenGrupiLlogaria = nrnengrupiLlogaria;
            pershkrimiNenGrupiLlogaria = pershkriminengrupiLlogaria;
            idGrupiLlogaria=idgrupiLlogaria;
            idNdermarje = idnderm;
        }

        /// <summary>
        /// Kontruktor i klases
        /// </summary>
        public clsNenGrupiLlogaria(int nrnengrupiLlogaria, String pershkriminengrupiLlogaria,int idgrupiLlogaria, int idnderm)
        {
            nrNenGrupiLlogaria = nrnengrupiLlogaria;
            pershkrimiNenGrupiLlogaria = pershkriminengrupiLlogaria;
            idGrupiLlogaria=idgrupiLlogaria;
            idNdermarje = idnderm;
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idndengrupi">id e nen grupit</param>
        public clsNenGrupiLlogaria(int idndengrupi, int idGjuha)
        {
            clsDatabaseKontabilitet dbNenGrupLlogari = new clsDatabaseKontabilitet();
            mbushNenGrupLlogaria(dbNenGrupLlogari.ktheNenGrupLlogaria(idndengrupi, idGjuha));
            dbNenGrupLlogari.Dispose();
        }
        
        /// <summary>
        /// Kontruktor i klases
        /// </summary>
        public clsNenGrupiLlogaria()
        { 
        }

        public clsNenGrupiLlogaria(DataRow rreshti)
        {
            
            mbushNenGrupLlogaria(rreshti);
        }

        #endregion

        #region Properties
        
        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht
        /// </summary>
        public int IdNenGrupiLlogaria
        {
            get { return idNenGrupiLlogaria; }
            set { idNenGrupiLlogaria = value; }
        }

        /// <summary>
            /// Kthen/Vendos numrin e nengrupit te llogarise
        /// </summary>
        public int NrNenGrupiLlogaria
        {
            get { return nrNenGrupiLlogaria; }
            set { nrNenGrupiLlogaria = value; }
        }

        /// <summary>
            /// Kthen/Vendos pershkrimin e nengrupit te llogarise
        /// </summary>
        public String PershkrimiNenGrupiLlogaria
        {
            get { return pershkrimiNenGrupiLlogaria; }
            set { pershkrimiNenGrupiLlogaria = value; }
        }

        /// <summary>
            /// Kthen/Vendos ID-ne e grupit te llogarise te cilit i perket nengrupi
        /// </summary>
        public int IdGrupiLlogaria
        {
            get {return idGrupiLlogaria;}
            set{idGrupiLlogaria = value;}
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e ndermarrjes
        /// </summary>
        public int IdNdermarje
        {
            get { return idNdermarje; }
            set { idNdermarje = value; }
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// Ruan objektin e nengrupit te llogarise ne tabelen perkatese ne databaze.Therret funksionin
        /// <see cref="DbCore.DbKontabiliteti.clsDatabaseKontabilitet.ruajNenGrupLlogaria"/>
        /// </summary>
        /// <returns>Kthen true nese ruajtja perfundoi me sukses</returns>
        public clsMesazh ruaj()
        {
            clsDatabaseKontabilitet data = new clsDatabaseKontabilitet();
            int idN;
            clsMesazh u_ruajt = data.ruajNenGrupLlogaria(out idN, this.NrNenGrupiLlogaria, this.PershkrimiNenGrupiLlogaria, this.IdGrupiLlogaria, this.IdNdermarje);
            data.Dispose();
            return u_ruajt;
        }

        /// <summary>
            /// Modifikon objektin e nengrupit te llogarise ne tabelen perkatese ne databaze.Therret funksionin
            /// <see cref="DbCore.DbKontabiliteti.clsDatabaseKontabilitet.modifikoNenGrupLlogaria"/>
            /// </summary>
            /// <returns>Kthen true nese modifikimi perfundoi me sukses</returns>
        public clsMesazh modifiko()
        {
            clsDatabaseKontabilitet data = new clsDatabaseKontabilitet();
            clsMesazh u_modifikua = data.modifikoNenGrupLlogaria(this.IdNenGrupiLlogaria, this.NrNenGrupiLlogaria, this.PershkrimiNenGrupiLlogaria, this.IdGrupiLlogaria, this.IdNdermarje);
            data.Dispose();
            return u_modifikua;
        }

        /// <summary>
            /// Fshin objektin e nengrupit te llogarise ne tabelen perkatese ne databaze.Therret funksionin
            /// <see cref="DbCore.DbKontabiliteti.clsDatabaseKontabilitet.fshiNenGrupLlogaria"/>
            /// </summary>
            /// <returns>Kthen true nese fshirja perfundoi me sukses</returns>
        public clsMesazh fshi()
        {
            clsDatabaseKontabilitet data = new clsDatabaseKontabilitet();
            clsMesazh u_fshi = data.fshiNenGrupLlogaria(this.IdNenGrupiLlogaria);
            data.Dispose();
            return u_fshi;
        }

        /// <summary>
        /// Kthen/Vendos nje collection me objekte nengrup llogarie duke filtruar sipas ndermarrjes.Therret funksionin
        /// <see cref="DbCore.DbKontabiliteti.clsNenGrupiLlogaria.ktheGjitheNenGrupetLlogaria"/>
        /// </summary>
        public colNenGrupetLlogaria merriTeGjithe(int idGjuha)
        {
            colNenGrupetLlogaria data = new colNenGrupetLlogaria();
            data.mbushGjitheNenGrupetLlogaria(this.idNdermarje, idGjuha);
            return data;
            //clsDatabaseKontabilitet data = new clsDatabaseKontabilitet();
            //return data.merrGjitheNenGrupetLlogaria(this.idNdermarje);
        }

        /// <summary>
        /// Metode e klases, jo e objektit. Kthen nje id e nen grupit te llogarise sipas kodit dhe idndermarrjes
        /// </summary>
        /// <param name="kodi">kodi i nengrupit te llogarise</param>
        /// <param name="idnder">id e ndermarrjes</param>
        /// <returns>id e nen grupit te llogarise</returns>
        public static int ktheIDNenGrupLlogaria(string kodi, int idnder, int idGjuha)
        {
            clsDatabaseKontabilitet dbNenGrupLlogari= new clsDatabaseKontabilitet();
            int id = (dbNenGrupLlogari.ktheNenGrupLlogaria(kodi, idnder, idGjuha));
            dbNenGrupLlogari.Dispose();
            return id;
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush nen grupet e llogarise nga databaza
        /// </summary>
        /// <param name="dbDataRowLlogari">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushNenGrupLlogaria(DataRow dbDataRowNenGrupLlogaria)
        {
            if (dbDataRowNenGrupLlogaria != null)
            {
                try
                {
                    int.TryParse(dbDataRowNenGrupLlogaria["IDNENGRUPILLOGARIA"].ToString(), out idNenGrupiLlogaria);
                    int.TryParse(dbDataRowNenGrupLlogaria["NRNENGRUPILLOGARIA"].ToString(), out nrNenGrupiLlogaria);
                    pershkrimiNenGrupiLlogaria = dbDataRowNenGrupLlogaria["PERSHKNENGRUPILLOGARIA"].ToString();
                    int.TryParse(dbDataRowNenGrupLlogaria["IDGRUPILLOGARIA"].ToString(), out idGrupiLlogaria);
                    int.TryParse(dbDataRowNenGrupLlogaria["IDNDERMARJE"].ToString(), out idNdermarje);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se nen grupeve te llogarise nga db-ja");
                }
            }
            else
                return false;
        }
        
        #endregion
    }
}