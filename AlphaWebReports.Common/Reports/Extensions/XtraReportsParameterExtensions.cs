using DbCore.DbShare;
using System.Collections.Generic;

namespace DbCore.IMBUtils.Extensions
{
    public static class XtraReportsParameterExtensions
    {
        public static List<clsParameter> ToColParameter(this DevExpress.XtraReports.Parameters.ParameterCollection parameters)
        {
            var listParametrat = new List<clsParameter>(parameters.Count);
            foreach (var param in parameters)
                listParametrat.Add(new DbCore.DbShare.clsParameter { Pershkrimi = param.Description, Vlera = param.Value.ToString() });
            return listParametrat;
        }
    }
}