using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.IMBUtils
{
    public static class ClassExtensions
    {
        public static void EmptyObject<T>(this T myObject) where T : class
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (var info in properties)
            {
                // if a string and null, set to String.Empty
                if (info.PropertyType == typeof(string) && info.GetValue(myObject, null) == null)
                    info.SetValue(myObject, String.Empty, null);
                else
                   if (info.PropertyType.IsClass && !info.PropertyType.IsAbstract && info.GetValue(myObject, null) == null)
                    info.SetValue(myObject, Activator.CreateInstance(info.PropertyType), null);
            }
        }
    }
}
