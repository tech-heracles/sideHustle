using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using static System.Convert;

namespace DbCore.DbAdmin
{
    public class ReportStyle
    {
        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdStili { get; set; }

        /// <summary>
        /// Kthen/Vendos emrin e stilit te raporteve.
        /// </summary>
        public string EmerStili { get; set; }
        
        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        public string FileName { get; set; }

        #endregion

        #region Public Methods

        public static List<ReportStyle> GetReportStyles(int idGjuha)
        {
            using (var data = new clsDatabaseAdmin())
                return data.GetReportSytles(idGjuha).ToList();
        } 

        #endregion

        #region Internal Methods

        internal static ReportStyle Create(IDataRecord dataRecord)
        {
            var stilRaporti = new ReportStyle();
            stilRaporti.Mbush(dataRecord);
            return stilRaporti;
        }

        internal void Mbush(IDataRecord dataRecord)
        {
            if (dataRecord == null) return;
            try
            {
                EmerStili = dataRecord["EmerStili"].ToString();
                IdStili = !IsDBNull(dataRecord["IdStili"])
                    ? ToInt32(dataRecord["IdStili"])
                    : 0;
                FileName = dataRecord["FileName"].ToString();
            }
            catch (InvalidCastException)
            {
                throw new Exception("ERROR: Gabim gjate marrjes se stileve nga db-ja");
            }
        }

        #endregion
    }
}