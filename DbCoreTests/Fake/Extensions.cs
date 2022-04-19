using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;

namespace DbCoreTests.Fake
{
    public static class Extensions
    {
        // N.B. DataReaderStub cannot be create directly
        public static IDataReader AsDataReader<TObject, TDataRow>(this IEnumerable<TObject> items, Expression<Func<TObject, TDataRow>> mapper)
        {
            return new DataReaderStub<TObject, TDataRow>(items, mapper);
        }
    }
}
