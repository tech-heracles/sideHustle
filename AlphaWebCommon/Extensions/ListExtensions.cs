using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using DbCore.IMBUtils.Types;

namespace DbCore.IMBUtils.Extensions
{
    public static class ListExtensions
    {
        /// <summary>
        /// shton nje objekt ne list vetem nese ai nuk gjendet
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="item"></param>
        public static void AddIfNotExists<T>(this IList<T> list, T item)
        {
            if (list.IndexOf(item) == -1)
                list.Add(item);
        }

        public static void AddIfNotExists<T>(this IList<T> list, T item, IEqualityComparer<T> comparer)
        {
            if (!list.Contains(item, comparer))
                list.Add(item);
        }

        /// <summary>
        /// fshin nga lista nje liste
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <param name="lista"></param>
        public static void RemoveFromList<T>(this IList<T> parentList, IList<T> childList)
        {

            foreach (var item in childList)
                parentList.Remove(item);
            //  parent.RemoveAll(x => lista.Contains(x));
        }

        /// <summary>
        ///konverton ne datatable nje colection qe implementon IList.Merr nga objektet e listes vetem propertit qe i jane kaluar si parameter
        /// </summary>
        /// <typeparam name="T">tipi i objektit qe permban lista</typeparam>
        /// <param name="data">lista e cila do konvertohet</param>
        /// <param name="properties">nje seri atributesh</param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this IEnumerable<T> data, params string[] properties)
        {
            var props = GetPublicProperties(typeof(T));
            var atoQeNaDuhen = new List<PropertyDescriptor>(properties.Length);
            var table = new DataTable();
            properties.ForEach(atribut => {
                var prop = props[atribut];
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                atoQeNaDuhen.Add(prop);
            });
            data.ForEach(item =>
            {
                table.Rows.Add(atoQeNaDuhen.Select(x => x.GetValue(item) ?? DBNull.Value).ToArray());
            });
            return table;
        }

        public static PropertyDescriptorCollection GetPublicProperties(this Type type)
        {
            if (!type.IsInterface)
                return TypeDescriptor.GetProperties(type);

            var props = (new Type[] { type })
                   .Concat(type.GetInterfaces())
                   .Select(i => TypeDescriptor.GetProperties(i));

            ICollection<PropertyDescriptor> list = new List<PropertyDescriptor>();

            props.ForEach(x =>
            {
                var p = x.GetEnumerator();
                while (p.MoveNext())
                {
                    PropertyDescriptor property = (PropertyDescriptor)p.Current;
                    if (!list.Contains(property))
                        list.Add(property);
                }
            });

            return new PropertyDescriptorCollection(list.ToArray());
        }

        public static IEnumerable<T> Merge<T>(this IEnumerable<List<T>> data)
        {
            var list = new List<T>();
            foreach (var t in data)
                list.AddRange(t);

            return list.AsEnumerable();
        }

        /// <summary>
        /// konverton ne datatable listen me te gjithe atributet qe ka secili objekt
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            var props = TypeDescriptor.GetProperties(typeof(T));

            var table = new DataTable();
            foreach (PropertyDescriptor prop in props)
            {
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }
            foreach (var item in data)
            {
                var dr = table.NewRow();

                for (int i = 0, length = props.Count; i < length; i++)
                {
                    dr[i] = props[i].GetValue(item) ?? DBNull.Value;
                }

                table.Rows.Add(dr);
            }
            return table;
        }

        public static bool ShtoObjektBosh<T>(this IList<T> data)
        {
            data.Add(NewInstance<T>.Instance());
            return true;
        }

        public static string Join<T>(this IList<T> data, char joiner, Func<T, string> funksionQeKonvertonNeString)
        {
            string result = string.Empty;

            foreach (var d in data)
            {
                result = $"{result}{joiner}{funksionQeKonvertonNeString(d)}";
            }

            return result.Trim(joiner);
        }

        //TOTEST GETSON
        public static void FindAndRemove<T>(this IList<T> list, Func<T, bool> finder)
        {
            var item = list.FirstOrDefault(finder);
            var index = list.IndexOf(item);
            if (index > -1)
                list.RemoveAt(index);
        }
        public static void FindAllAndRemove<T>(this IList<T> list, Func<T, bool> finder)
        {
            list.RemoveFromList(list.Where(finder).ToList());
        }

        public static bool ContainsAny<T>(this IList<T> list, params T[] objects)
        {
            foreach (var obj in objects)
                if (list.Contains(obj))
                    return true;
            return false;
        }
    }
}