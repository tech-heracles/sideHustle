using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace DbCore.IMBUtils.Extensions
{
    public static class DictionaryExtensions
    {
        public static List<KeyValuePair<string, string>> AsKVP(this NameValueCollection source)
        {
            return source.AllKeys.SelectMany(source.GetValues, (k, v) => new KeyValuePair<string, string>(k, v)).ToList();
        }

        public static Dictionary<string, object> ToDictionary(this NameValueCollection col)
        {
            var dict = new Dictionary<string, object>(col.Count);
            foreach (var name in col.AllKeys)
            {
                if (name == null) continue;
                dict.Add(name, col[name]);
            }
            return dict;
        }

        public static KeyValuePair<TKey, TValue> Pop<TKey, TValue>(this Dictionary<TKey, TValue> dic)
        {
            var firstDic = dic.First();
            dic.Remove(firstDic.Key);
            return firstDic;
        }

        public static void AddRange<TKey, TValue>(this Dictionary<TKey, TValue> dic, Dictionary<TKey, TValue> dicToAdd) =>
            dicToAdd.ForEach(x =>
            {
                if (!dic.ContainsKey(x.Key)) dic.Add(x.Key, x.Value);
            });

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
                action(item);
        }
    }
}