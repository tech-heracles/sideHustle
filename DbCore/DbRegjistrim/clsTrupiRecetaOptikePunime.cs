using System;
using System.Data;
using AlphaWeb.Core.Interfaces.Data;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Logging;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne trupin e recetes
    ///  (Te dhenat  merren nga tabela : T_TRUPIRECETAOPTIKEPunime)
    /// </summary>
    public class clsTrupiRecetaOptikePunime : IDataBase
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
        /// id e numrit te fazes
        /// </summary>
        private int nrFaza;

        /// <summary>
        /// fusha syri i majte qe mban shenime per syrin e majte 
        /// </summary>
        private string shenimeSyriMajte;
        /// <summary>
        /// fusha syri i djathte qe mban shenime per syrin e djathte
        /// </summary>
        private string shenimeSyriDjathte;

        private int idArtikulliSyriMajte;
        private int idArtikulliSyriDjathte;
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
        public int NrFaza
        {
            get
            {
                return nrFaza;
            }
            set
            {
                nrFaza = value;
            }
        }
        /// <summary>
        /// syri i majte
        /// </summary>
        public string ShenimeSyriMajte
        {
            get
            {
                return shenimeSyriMajte;
            }
            set
            {
                shenimeSyriMajte = value;
            }
        }
        /// <summary>
        /// syri i djathte
        /// </summary>
        public string ShenimeSyriDjathte
        {
            get
            {
                return shenimeSyriDjathte;
            }
            set
            {
                shenimeSyriDjathte = value;
            }
        }

        public int IdArtikulliSyriMajte
        {
            get
            {
                return idArtikulliSyriMajte;
            }
            set
            {
                idArtikulliSyriMajte = value;
            }
        }
        public int IdArtikulliSyriDjathte
        {
            get
            {
                return idArtikulliSyriDjathte;
            }
            set
            {
                idArtikulliSyriDjathte = value;
            }
        }

        #endregion

        #region Konstruktoret
        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public clsTrupiRecetaOptikePunime()
        {
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idTrupi"></param>
        public clsTrupiRecetaOptikePunime(int idTrupi)
        {
            using (clsDatabaseRegjistrim db = new clsDatabaseRegjistrim())
                db.ktheTrupiRecetaPunimeSipasID(idTrupi, this);

        }

        public clsTrupiRecetaOptikePunime(IDataRecord rreshti)
        {
            Mbush(rreshti);
        }
        #endregion


        #region Metoda Publike


        #endregion

        #region Metoda Internal

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
                int.TryParse(dbDataRow["NRFAZA"].ToString(), out nrFaza);
                int.TryParse(dbDataRow["IDARTIKULLISYRIMAJTE"].ToString(), out idArtikulliSyriMajte);
                int.TryParse(dbDataRow["IDARTIKULLISYRIDJATHTE"].ToString(), out idArtikulliSyriDjathte);
                shenimeSyriMajte = dbDataRow["SHENIMESYRIMAJTE"].ToString();
                shenimeSyriDjathte = dbDataRow["SHENIMESYRIDJATHTE"].ToString();

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
                    return db.ruajTrupiRecetaOptikePunime(out idTrupi, IdKoka, NrFaza, IdArtikulliSyriMajte, IdArtikulliSyriDjathte, ShenimeSyriMajte, ShenimeSyriDjathte);

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
    }
}
