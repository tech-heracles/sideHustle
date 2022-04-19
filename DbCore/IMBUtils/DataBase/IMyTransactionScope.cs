using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.IMBUtils.DataBase
{
    public interface IMyTransactionScope : IDisposable
    {
        void Complete();
    }
}
