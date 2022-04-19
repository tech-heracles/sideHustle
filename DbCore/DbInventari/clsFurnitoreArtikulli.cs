using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbInventari
{  

    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne furnitoret e artikujve
    ///  (Te dhenat  merren nga tabela : T_FURNITOREARTIKULLI)
    ///  lidh furnitoret me artikujt
    /// </summary>
    public class clsFurnitoreArtikulli
    { 

        #region Atribute

        private int idFurnitoreArtikulli;
        private int idArtikulli;
        private int idFurnitori;
        private string prioriteti;
        private string kodiKF;
        private string emertimiKF;
        private DataRow rreshti;

        #endregion 

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdFurnitoreArtikulli
        {
            get { return idFurnitoreArtikulli ; }
            set { idFurnitoreArtikulli  = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e artikullit
        /// </summary>
        public int IdArtikulli
        {
            get { return idArtikulli ; }
            set { idArtikulli = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e furnitorit.
        /// </summary>
        public int IdFurnitori
        {
            get { return idFurnitori; }
            set { idFurnitori = value; }
        }
        /// <summary>
        /// Kthen/Vendos prioritetin.
        /// </summary>
        public String Prioriteti
        {
            get { return prioriteti ; }
            set { prioriteti  = value; }
        }
        /// <summary>
        /// Kthen/Vendos kodi klient furnitori.
        /// </summary>
        public string KodiKF
        {
            get
            {
                return kodiKF;  

            }
            set
            {
                kodiKF = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos emri i klient furnitorit.
        /// </summary>
        public string EmertimiKF
        {
            get
            {
                return emertimiKF;
            }
            set
            {
                this.emertimiKF = value;
            }

        }

        #endregion

        #region Konstruktoret

        /// <summary>
        /// konstruktori me parametra
        /// </summary>
        /// <param name="idFurnitoreArtikulli"> id ritese e furnitorit te artikullit </param>
        /// <param name="idArtikulli"> id e artikullit</param>
        /// <param name="idFurnitori">id e furnitorit</param>
        /// <param name="prioriteti">prioriteti</param>
        public clsFurnitoreArtikulli(int idFurnitoreArtikulli,int idArtikulli,int idFurnitori,  String prioriteti)
        {
            this.idFurnitoreArtikulli = idFurnitoreArtikulli;
            this.idArtikulli = idArtikulli;
            this.idFurnitori = idFurnitori;
            this.prioriteti = prioriteti;
        }

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>

        public clsFurnitoreArtikulli()
        { 
        }

        public clsFurnitoreArtikulli(DataRow rreshti)
        {
            
            mbushFurnitoreArtikulli(rreshti);
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// Ruan objektin furnitor artikulli ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbInventari.clsDatabaseInventari.ruajFurnitoreArtikulli"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
        public clsMesazh ruaj()
        {
            clsDatabaseInventari data = new clsDatabaseInventari();
            int id;
            clsMesazh u_ruajt = data.ruajFurnitoreArtikulli(out id, this.IdArtikulli, this.IdFurnitori, this.Prioriteti);
            data.Dispose();
            //clsMesazh u_ruajt = data.ruajFurnitoreArtikulli(this);
            return u_ruajt;
        }

        /// <summary>
        /// Modifikon objektin furnitor artikulli ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbInventari.clsDatabaseInventari.modifikoFurnitoreArtikulli"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo</returns>
     
        public clsMesazh modifiko()
        {
            clsDatabaseInventari data = new clsDatabaseInventari();
            clsMesazh u_modifikua = data.modifikoFurnitoreArtikulli(this.IdFurnitoreArtikulli, this.IdArtikulli, this.IdFurnitori, this.Prioriteti);
            data.Dispose();
            //clsMesazh u_modifikua = data.modifikoFurnitoreArtikulli(this);
            return u_modifikua;
        }

        /// <summary>
        /// Fshin objektin furnitor artikulli ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbInventari.clsDatabaseInventari.fshiFurnitoreArtikulli"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
     
        public clsMesazh fshi()
        {
            clsDatabaseInventari data = new clsDatabaseInventari();
            clsMesazh u_fshi = data.fshiFurnitoreArtikulli(this.IdFurnitoreArtikulli);
            data.Dispose();
            //clsMesazh u_fshi = data.fshiFurnitoreArtikulli(this);
            return u_fshi;
        }

        /// <summary>
        /// merr objektin furnitor artikulli nga tabela perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbInventari.clsDatabaseInventari.merrFurnitoreArtikulli"/> 
        /// </summary>
      
        public void merr()
        {
            clsDatabaseInventari data = new clsDatabaseInventari();
            data.merrFurnitoreArtikulli(this.IdFurnitoreArtikulli);
            data.Dispose();
            //data.merrFurnitoreArtikulli(this);
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush furnitorin e artikullit nga databaza
        /// </summary>
        /// <param name="dbDataRowFurnitoreArtikulli">datarow qe duhet mbushur nga databaza</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushFurnitoreArtikulli(DataRow dbDataRowFurnitoreArtikulli)
        {

            if (dbDataRowFurnitoreArtikulli != null)
            {

                try
                {
                    int.TryParse(dbDataRowFurnitoreArtikulli["IDFURNITOREARTIKULLI"].ToString(), out idFurnitoreArtikulli);
                    int.TryParse(dbDataRowFurnitoreArtikulli["IDARTIKULLI"].ToString(), out idArtikulli);
                    int.TryParse(dbDataRowFurnitoreArtikulli["IDFURNITORI"].ToString(), out idFurnitori);
                    prioriteti = dbDataRowFurnitoreArtikulli["PRIORITETI"].ToString();
                    kodiKF = dbDataRowFurnitoreArtikulli["KODKLIENTFURNITOR"].ToString();
                    emertimiKF = dbDataRowFurnitoreArtikulli["EMERTIMIKF"].ToString();

                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se furnitorit te artikullit nga db-ja");
                }
            }
            else
                return false;

        }

        #endregion

    }
}
