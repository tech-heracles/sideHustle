using System.Collections.Generic;

namespace AlphaWebCommon.TreeStructure
{
    public class TreeStructure<T>
    {
        public T Item { get; set; }
        public IEnumerable<TreeStructure<T>> Children { get; set; }
       
    }
}
