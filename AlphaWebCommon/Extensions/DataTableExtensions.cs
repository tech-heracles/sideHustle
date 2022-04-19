using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DbCore.IMBUtils.Extensions
{
    public static class DataTableExtensions
    {
        /// <summary>
        /// extension method per datatables
        /// </summary>


        public static List<T> ToList<T>(this DataTable dt)
        {
            var data = new List<T>(dt.Rows.Count);
            data.AddRange(dt.Rows.Cast<DataRow>().Select<DataRow, T>(row => row.ToCustomType<T>()));
            return data;
        }

        public static DataTable ToDataTable(this DataTable dt, params string[] columns)
        {

            return (new DataView(dt)).ToTable(false, columns);

        }

        public static bool ColumnContainsValue(this DataTable dt, string fieldName, string value)
        {
            if (!dt.Columns.Contains(fieldName))
                throw new Exception($"Datatable doesn't contain column {fieldName}");

            return (dt.Select($"{fieldName} = '{value}'")).Length > 0;
        }
        public static string[] GetAllColumnNames(this DataTable dt)
        {
           return dt.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
        }

        public static void RemoveRows<T>(this DataTable dt, string keyFieldName, List<T> keys)
        {
            try
            {
                
                foreach (var key in keys)
                {
                    for (var i = dt.Rows.Count - 1; i >= 0; i--)
                    {
                        var dr = dt.Rows[i];
                        if (dr.RowState == DataRowState.Deleted)
                            continue;
                        if (string.Compare(key.ToString(), dr[keyFieldName].ToString(), StringComparison.Ordinal) == 0)
                        {
                            dr.Delete();
                        }
                    }
                }
                dt.AcceptChanges();
            }
            catch (Exception ex)
            {
                throw new MyException("Gabim ne fshirje te rreshtave nga grida!", ex);
            }
        }

        public static void AddColumns(this DataTable source, params DataColumn[] columns)
        {
            source.Columns.AddRange(columns);
        }

        public static void AddRow(this DataTable source, params object[] values)
        {
            source.Rows.Add(values);
        }


        public static List<string> GetColumnAsList(this DataTable source, string columnName){
            if (!source.Columns.Contains(columnName))
                throw new Exception($"DataTable does not contain column {columnName}");
            List<string> values = new List<string>(source.Rows.Count);
            foreach (DataRow row in source.Rows)
                values.Add(row[columnName].ToString());

            return values;
        }
    }   
}