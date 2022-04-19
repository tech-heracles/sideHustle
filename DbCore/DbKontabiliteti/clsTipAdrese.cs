using System;
using System.Data;
using static System.Convert;

namespace DbCore.DbKontabiliteti
{
    /// <remarks>
    /// Kjo eshte klasa qe sherben per objektet qe perfaqesojne nje tip adrese psh adrese Biznesi, Megazine, Dege
    ///  (Te dhenat  merren nga tabela : T_TIPADRESE)
    /// </remarks>
    public class clsTipAdrese
    {
        #region Konstruktoret

        /// <summary>
        /// Konstruktor i klases
        /// </summary>
        public clsTipAdrese()
        {
        }

        public clsTipAdrese(DataRow rreshti)
        {
            MbushTipAdrese(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne e tipit te adreses
        /// </summary>
        public int IdTipAdrese { get; set; }

        /// <summary>
        /// Kthen/Vendos pershkrimin e titpit te adreses
        /// </summary>
        public string PershkrimTipAdrese { get; set; }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush tipin e adreses nga databaza
        /// </summary>
        /// <param name="dbDataRowTipAdrese">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal void MbushTipAdrese(DataRow dbDataRowTipAdrese)
        {
            if (dbDataRowTipAdrese != null)
            {
                try
                {
                    IdTipAdrese = !IsDBNull(dbDataRowTipAdrese["IDTIPADRESE"])
                        ? ToInt32(dbDataRowTipAdrese["IDTIPADRESE"])
                        : 0;
                    PershkrimTipAdrese = dbDataRowTipAdrese["PERSHKRIMTIPADRESE"].ToString();
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se tipit te adreses nga db-ja");
                }
            }
        }

        #endregion
    }
}
