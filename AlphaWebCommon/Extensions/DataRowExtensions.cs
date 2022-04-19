using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DbCore.IMBUtils.Types;

namespace DbCore.IMBUtils.Extensions
{
    public static class DataRowExtensions
    {
        public static T ToCustomType<T>(this DataRow dr)
        {
            var temp = typeof(T);
            var obj = NewInstance<T>.Instance();
            var properties = temp.GetProperties();
            foreach (DataColumn column in dr.Table.Columns)
            {
                var pro = properties.FirstOrDefault(x => x.Name.Equals(column.ColumnName, StringComparison.OrdinalIgnoreCase));

                if (dr[column.ColumnName] is DBNull)
                    continue;
                pro.SetValue(obj, Convert.ChangeType(dr[column.ColumnName], pro.PropertyType));
            }
            return obj;
        }

        public static DataTable GetDataTable(this IEnumerable<DataRow> drs, DataTable parentDt)
        {
            var dataRows = drs as IList<DataRow> ?? drs.ToList();
            return dataRows.Any() ? dataRows.CopyToDataTable() : parentDt.Clone();
        }

        public static List<int> ToList(this DataRow[] dataRows, string column)
        {
            var list = new List<int>();

            foreach (var dataRow in dataRows)
                list.Add(int.Parse(dataRow[column].ToString()));

            return list;
        }
    }
}