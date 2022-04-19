using System;
using System.Collections.Generic;
using System.Linq;
using DbCore.IMBUtils;

namespace AlphaWebCommon.TreeStructure
{
    public static class GenericHelpers
    {

        /// <summary>
        /// Generates tree of items from item list
        /// </summary>
        /// 
        /// <typeparam name="T">Type of item in collection</typeparam>
        /// <typeparam name="K">Type of parent_id</typeparam>
        /// 
        /// <param name="collection">Collection of items</param>
        /// <param name="id_selector">Function extracting item's id</param>
        /// <param name="parent_id_selector">Function extracting item's parent_id</param>
        /// <param name="root_id">Root element id</param>
        /// 
        /// <returns>Tree of items</returns>
        public static IEnumerable<TreeStructure<T>> GenerateTree<T, K>(
            this IEnumerable<T> collection,
            Func<T, K> id_selector,
            Func<T, K> parent_id_selector,
            K root_id = default(K))
        {
            List<TreeStructure<T>> recursiveObjects = new List<TreeStructure<T>>();
            foreach (var item in collection.Where(x => parent_id_selector(x).Equals(root_id)))
            {
                recursiveObjects.Add(new TreeStructure<T>
                {
                    Item = item,
                    Children = collection.GenerateTree(id_selector, parent_id_selector, id_selector(item))
                }); 
            }
            return recursiveObjects;
        }
    }
}
