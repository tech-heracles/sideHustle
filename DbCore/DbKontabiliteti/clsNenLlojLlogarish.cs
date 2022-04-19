using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbKontabiliteti
{
    /// <remarks>
    /// Kjo eshte klasa qe sherben per objektet qe perfaqesojne nje nenlloj llogarie <example>Lloji "Llogari klienti" ka nenllojet: Llogari kruesore, llogari zbritje etj</example>
    ///  (Te dhenat  merren nga tabela : T_NENLLOJLLOGARISH)
    /// </remarks>
    public class clsNenLlojLlogarish
    {
        #region Atributet

        private int idNenLlojLlogarie;
        private string kodNenLlojLlogarie;
        private string pershkrimNenLlojLlogarie;
        private string idLlojLlogarie;
        private DataRow rreshti;

        #endregion

        #region Konstruktoret

        /// <summary>
        /// kontruktor i klases
        /// </summary>
        public clsNenLlojLlogarish(int idnenllojllogarie, String kodlnenlojllogarie, string pershkrimnenllojLlogarie, string idllijllogarie)
        {
            idNenLlojLlogarie = idnenllojllogarie;
            kodNenLlojLlogarie = kodlnenlojllogarie;
            pershkrimNenLlojLlogarie = pershkrimnenllojLlogarie;
            idLlojLlogarie = idllijllogarie;
        }

        /// <summary>
        /// konstruktor me 1 parameter integer
        /// </summary>
        /// <param name="id">id e nen llojit te llogarive</param>
        public clsNenLlojLlogarish(int id)
        {
            clsDatabaseKontabilitet dbNenLlojLlog = new clsDatabaseKontabilitet();
            mbushNenLlojLlogarish(dbNenLlojLlog.ktheNenLlojLlogarieSipasID(id));
            dbNenLlojLlog.Dispose();
        }
        public clsNenLlojLlogarish(int id, clsDatabaseKontabilitet dbNenLlojLlog)
        {
            mbushNenLlojLlogarish(dbNenLlojLlog.TransCache.getNenLlojLlogarish(id, dbNenLlojLlog));
            //mbushNenLlojLlogarish(dbNenLlojLlog.ktheNenLlojLlogarieSipasID(id));
        }

        /// <summary>
        /// konstruktor me 1 parameter string
        /// </summary>
        /// <param name="kodi">kodi i nen llogarise</param>
        public clsNenLlojLlogarish(string kodi)
        {
            clsDatabaseKontabilitet dbNenLlojLlog = new clsDatabaseKontabilitet();
            mbushNenLlojLlogarish(dbNenLlojLlog.ktheNenLlojLlogarieSipasKodit(kodi));
            dbNenLlojLlog.Dispose();
        }

        /// <summary>
        /// Kontruktor i klases
        /// </summary>
        public clsNenLlojLlogarish()
        {
        }

        public clsNenLlojLlogarish(DataRow rreshti)
        {
            
            mbushNenLlojLlogarish(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht
        /// </summary>
        public int IdNenLlojLlogarie
        {
            get { return idNenLlojLlogarie; }
            set { idNenLlojLlogarie = value; }
        }

        /// <summary>
        /// Kthen/Vendos kodin e nenllojit te llogarise
        /// </summary>
        public String KodNenLlojLlogarie
        {
            get { return kodNenLlojLlogarie; }
            set { kodNenLlojLlogarie = value; }
        }

        /// <summary>
        /// Kthen/Vendos pershkrimin e nenllojit te llogarise
        /// </summary>
        public String PershkrimNenLlojLlogarie
        {
            get { return pershkrimNenLlojLlogarie; }
            set { pershkrimNenLlojLlogarie = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e llojit te llogarise te cilit i perket nenlloji
        /// </summary>
        public string IdLlojLlogarie
        {
            get { return idLlojLlogarie; }
            set { idLlojLlogarie = value; }
        }

        /// <summary>
        /// Kthen/Vendos nje collection me nenlloje llogarish te filtruara sipas ID-se.
        /// Therret funksionin <see cref="DbCore.DbKontabiliteti.clsDatabaseKontabilitet.ktheNenLlojLlogarieSipasID"/>
        /// </summary>
        public clsNenLlojLlogarish merrNenLlojLlogarieSipasID()
        {
            clsNenLlojLlogarish data = new clsNenLlojLlogarish(this.IdNenLlojLlogarie);
            return data;
            //clsDatabaseKontabilitet data = new clsDatabaseKontabilitet();
            //colNenLlojLlogarish col = data.merrNenLlojLlogarieSipasID(this.IdNenLlojLlogarie);
            //if (col.Count > 0)
            //{
            //    return col[0];
            //}
            //else
            //{
            //    return new clsNenLlojLlogarish();
            //}
        }

        public clsNenLlojLlogarish merrNenLlojLlogarieSipasKodit()
        {
            clsNenLlojLlogarish data = new clsNenLlojLlogarish(this.KodNenLlojLlogarie);
            return data;
            //clsDatabaseKontabilitet data = new clsDatabaseKontabilitet();
            //colNenLlojLlogarish col = data.merrNenLlojLlogarieSipasKodit(this.KodNenLlojLlogarie);
            //if (col.Count > 0)
            //{
            //    return col[0];
            //}
            //else
            //{
            //    return new clsNenLlojLlogarish();
            //}
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush nen lloj llogarite nga databaza
        /// </summary>
        /// <param name="dbDataRowNenLlojLlog">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushNenLlojLlogarish(DataRow dbDataRowNenLlojLlog)
        {
            if (dbDataRowNenLlojLlog != null)
            {
                try
                {
                    idLlojLlogarie = dbDataRowNenLlojLlog["IDLLOJLLOGARIE"].ToString();
                    idNenLlojLlogarie = int.Parse(dbDataRowNenLlojLlog["IDNENLLOJLLOGARIE"].ToString());
                    kodNenLlojLlogarie = dbDataRowNenLlojLlog["KODNENLLOJLLOGARIE"].ToString();
                    pershkrimNenLlojLlogarie = dbDataRowNenLlojLlog["PERSHKRIMNENLLOJLLOGARIE"].ToString();
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se nen lloj llogarive nga db-ja");
                }
            }
            else
                return false;
        }
        internal void mbushNenLlojLlogarish(clsNenLlojLlogarish nll)
        {
            idNenLlojLlogarie = nll.idNenLlojLlogarie;
            kodNenLlojLlogarie = nll.kodNenLlojLlogarie;
            pershkrimNenLlojLlogarie = nll.pershkrimNenLlojLlogarie;
            idLlojLlogarie = nll.idLlojLlogarie;
            rreshti = nll.rreshti;            
        }
        #endregion
    }
}
