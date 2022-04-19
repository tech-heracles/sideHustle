using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace DbCore.IMBUtils.Types
{
    /// <summary>
    /// klase e cila sherben per instacim dinamik te  objekteve ne clr
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class NewInstance<T>
    {
        public static readonly Func<T> Instance = Creator();
        static Func<T> Creator()
        {
            var t = typeof(T);
            if (t == typeof(string))
                return Expression.Lambda<Func<T>>(Expression.Constant(string.Empty)).Compile();

            if (t.IsValueType || t.GetConstructor(Type.EmptyTypes) != null)
                return Expression.Lambda<Func<T>>(Expression.New(t)).Compile();
            return () => (T)FormatterServices.GetUninitializedObject(t);
        }
    }
}

