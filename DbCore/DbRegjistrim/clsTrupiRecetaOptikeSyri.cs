using System;
using System.Data;
using AlphaWeb.Core.Interfaces.Data;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Logging;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne trupin e recetes
    ///  (Te dhenat  merren nga tabela : T_TRUPIRECETAOPTIKESYRI)
    /// </summary>
    public class clsTrupiRecetaOptikeSyri : IDataBase
    {


        #region Atribute
        /// <summary>
        /// id e trupit te recetes
        /// </summary>
        private int idTrupi;
        /// <summary>
        /// id e kokes se recetes
        /// </summary>
        private int idKoka;

        /// <summary>
        /// id e fushes se trupit te recetes
        /// </summary>
        private int idFusha;

        /// <summary>
        /// fusha syri i majte qe mban te dhenat per syrin e majte per cdo idFusha 
        /// </summary>
        private string syriMajte;
        /// <summary>
        /// fusha syri i djathte qe mban te dhenat per syrin e djathte per cdo idFusha 
        /// </summary>
        private string syriDjathte;
        /// <summary>
        /// Pershkrimi nga clsFushaRecetaOptike 
        /// </summary>
        private string fusha;
        #endregion

        #region Konstruktoret
        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public clsTrupiRecetaOptikeSyri()
        {
        }

        /// <summary>
        /// konstruktori me parameter
        /// </summary>
        /// <param name="idTrupi"></param>
        /// <param name="idKoka"></param>
        /// <param name="idFusha"> </param>
        /// <param name="syriMajte"> </param>
        /// <param name="syriDjathte"></param>

        public clsTrupiRecetaOptikeSyri(int idTrupi, int idKoka, int idFusha, string syriMajte, string syriDjathte, string fusha)
        {
            this.idTrupi = idTrupi;
            this.idKoka = idKoka;
            this.idFusha = idFusha;
            this.syriMajte = syriMajte;
            this.SyriDjathte = syriDjathte;
            this.fusha = fusha;
        }




        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idTrupi"></param>
        public clsTrupiRecetaOptikeSyri(int idTrupi)
        {
            using (clsDatabaseRegjistrim db = new clsDatabaseRegjistrim())
                db.ktheTrupiRecetaSyriSipasID(idTrupi, this);

        }

        public clsTrupiRecetaOptikeSyri(IDataRecord rreshti)
        {
            Mbush(rreshti);
        }

     
       
        #endregion

        #region Properties
        /// <summary>
        /// id e trupit
        /// </summary>
        public int IdTrupi
        {
            get
            {
                return idTrupi;
            }
            set
            {
                idTrupi = value;
            }
        }
        /// <summary>
        /// id e kokes
        /// </summary>
        public int IdKoka
        {
            get
            {
                return idKoka;
            }
            set
            {
                idKoka = value;
            }
        }
        /// <summary>
        /// id e fushes
        /// </summary>
        public int IdFusha
        {
            get
            {
                return idFusha;
            }
            set
            {
                idFusha = value;
            }
        }
        /// <summary>
        /// syri i majte
        /// </summary>
        public string SyriMajte
        {
            get
            {
                return syriMajte;
            }
            set
            {
                syriMajte = value;
            }
        }
        /// <summary>
        /// syri i djathte
        /// </summary>
        public string SyriDjathte
        {
            get
            {
                return syriDjathte;
            }
            set
            {
                syriDjathte = value;
            }
        }

        public string Fusha
        {
            get
            {
                return fusha;
            }
        }

        #endregion

        #region Metoda Publike
        /// <summary>
        /// mbush trupin e recetes me te dhenat nga databaza
        /// </summary>
        /// <param name="dbDataRow">rreshti me te dhena</param>
        /// <returns> kthen ne se mbushja u be ne rregull apo jo</returns>
        public void Mbush(IDataRecord dbDataRow)
        {

            try
            {
                int.TryParse(dbDataRow["IDTRUPI"].ToString(), out idTrupi);
                int.TryParse(dbDataRow["IDKOKA"].ToString(), out idKoka);
                int.TryParse(dbDataRow["IDFUSHA"].ToString(), out idFusha);
                syriMajte = dbDataRow["SYRIMAJTE"].ToString();
                syriDjathte = dbDataRow["SYRIDJATHTE"].ToString();
                fusha = dbDataRow["PERSHKRIMI"].ToString();

            }
            catch (Exception ex)
            {
                ImbLogger.Error(ex);
                throw;
            }
        }

        public clsMesazh Ruaj()
        {
            try
            {
                using (var db = new clsDatabaseRegjistrim())
                    return db.ruajTrupiRecetaOptikeSyri(out idTrupi, IdKoka, IdFusha, SyriMajte, SyriDjathte);
            }
            catch (Exception ex)
            {
                ImbLogger.Error(ex);
                return new MesazhGabimi($"Dokumenti me id {IdTrupi} nuk u ruajt!");
            }

        }

        public clsMesazh Modifiko()
        {
            throw new NotImplementedException();
        }

        public clsMesazh Fshi()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Metoda Internal



        #endregion
    }
}
