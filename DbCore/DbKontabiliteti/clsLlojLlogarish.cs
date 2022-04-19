using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbKontabiliteti
{
    /// <remarks>
    /// Kjo eshte klasa qe sherben per objektet qe perfaqesojne nje lloj llogarie <example>Llogari klienti, llogari banke, llogari artikulli, llogari plani etj</example>
    ///  (Te dhenat  merren nga tabela : T_LLOJLLOGARISH)
    /// </remarks>
    public class clsLlojLlogarish
    {
        #region Atributet

        private int idLlojLlogarie;
        private string kodLlojLlogarie;
        private string pershkrimLlojLlogarie;
        private colNenLlojLlogarish oColNenLlojLlogarish;
        private DataRow rreshti;

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktor i klases
        /// </summary>
        /// <param name="pershkrimllojLlogarie"></param>
        public clsLlojLlogarish(int idllojllogarie, String kodllojllogarie, string pershkrimllojLlogarie)
        {
            idLlojLlogarie = idllojllogarie;
            kodLlojLlogarie = kodllojllogarie;
            pershkrimLlojLlogarie = pershkrimllojLlogarie;
        }

        /// <summary>
        /// konstruktor me 1 parameter integer
        /// </summary>
        /// <param name="id">id e llojeve te llogarise</param>
        public clsLlojLlogarish(int id)
        {
            clsDatabaseKontabilitet dbLlojLlogarish = new clsDatabaseKontabilitet();
            mbushLlojLlogarish(dbLlojLlogarish.ktheLlojLlogarieSipasID(id));
            dbLlojLlogarish.Dispose();
        }
        public clsLlojLlogarish(int id, clsDatabaseKontabilitet dbLlojLlogarish)
        {
            mbushLlojLlogarish(dbLlojLlogarish.TransCache.getLlojLlogari(id, dbLlojLlogarish));
            //mbushLlojLlogarish(dbLlojLlogarish.ktheLlojLlogarieSipasID(id));
        }
        /// <summary>
        /// konstruktor me 1 parameter string
        /// </summary>
        /// <param name="kodi">kodi i llojit te llogarise</param>
        public clsLlojLlogarish(string kodi)
        {
            clsDatabaseKontabilitet dbLlojLlogarish = new clsDatabaseKontabilitet();
            mbushLlojLlogarish(dbLlojLlogarish.ktheLlojLlogarieSipasKodit(kodi));
            dbLlojLlogarish.Dispose();
        }

        /// <summary>
        /// Konstruktor i klases
        /// </summary>
        public clsLlojLlogarish()
        {
        }

        public clsLlojLlogarish(DataRow rreshti)
        {
            
            mbushLlojLlogarish(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht
        /// </summary>
        public int IdLlojLlogarie
        {
            get { return idLlojLlogarie; }
            set { idLlojLlogarie = value; }
        }

        /// <summary>
        /// Kthen/Vendos kodin e llojit te llogarise
        /// </summary>
        public String KodLlojLlogarie
        {
            get { return kodLlojLlogarie; }
            set { kodLlojLlogarie = value; }
        }

        /// <summary>
        /// Kthen/Vendos pershkrimin e llojit te llogarise
        /// </summary>
        public String PershkrimLlojLlogarie
        {
            get { return pershkrimLlojLlogarie; }
            set { pershkrimLlojLlogarie = value; }
        }

        /// <summary>
        /// Kthen/Vendos nje collection me objekte te tipit <see cref="DbCore.DbKontabiliteti.clsNenLlojLlogarish"/>
        /// </summary>
        public colNenLlojLlogarish OColNenLlojLlogarish
        {
            get { return oColNenLlojLlogarish; }
            set { oColNenLlojLlogarish = value; }
        }

        /// <summary>
        /// Kthen/Vendos nje collection me lloje llogarish duke filtruar sipas ID-se se llogarise.
        /// Therret funksionin <see cref="DbKontabilitet.clsDatabaseKontabilitet.merrLlojLlogarieSipasID"/>
        /// </summary>
        public clsLlojLlogarish merrLlojLlogarieSipasID()
        {
            //clsDatabaseKontabilitet data = new clsDatabaseKontabilitet();
            //return data.merrLlojLlogarieSipasID(this.IdLlojLlogarie);
            clsLlojLlogarish data = new clsLlojLlogarish(this.IdLlojLlogarie);
            return data;
        }

        /// <summary>
        /// Kthen/Vendos nje collection me lloje llogarish duke filtruar sipas kodit se llojit se llogarise.
        /// Therret funksionin <see cref="DbKontabilitet.clsDatabaseKontabilitet.merrLlojLlogarieSipasID"/>
        /// </summary>
        public clsLlojLlogarish merrLlojLlogarieSipasKodit()
        {
            //clsDatabaseKontabilitet data = new clsDatabaseKontabilitet();
            //return data.merrLlojLlogarieSipasKodit(this.kodLlojLlogarie);
            clsLlojLlogarish data = new clsLlojLlogarish(this.kodLlojLlogarie);
            return data;
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush llojet e llogarive nga databaza
        /// </summary>
        /// <param name="dbDataRowLlojLlog">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushLlojLlogarish(DataRow dbDataRowLlojLlog)
        {
            if (dbDataRowLlojLlog != null)
            {
                try
                {
                    int.TryParse(dbDataRowLlojLlog["IDLLOJLLOGARIE"].ToString(), out idLlojLlogarie);
                    kodLlojLlogarie = dbDataRowLlojLlog["KODILLOJLLOGARIE"].ToString();
                    pershkrimLlojLlogarie = dbDataRowLlojLlog["PERSHKRIMLLOJLLOGARIE"].ToString();
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se llojeve te Llogarive nga db-ja");
                }
            }
            else
                return false;
        }
        internal void mbushLlojLlogarish(clsLlojLlogarish cll)
        {
            idLlojLlogarie = cll.idLlojLlogarie;
            kodLlojLlogarie = cll.kodLlojLlogarie;
            pershkrimLlojLlogarie = cll.pershkrimLlojLlogarie;
            oColNenLlojLlogarish = cll.oColNenLlojLlogarish;
            rreshti = cll.rreshti;
        }
        #endregion
    }
}
