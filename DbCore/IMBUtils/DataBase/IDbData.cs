using AlphaWeb.Core.Interfaces.Data;
using AlphaWeb.Infrastructure.Data.AdoNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.IMBUtils.DataBase
{
    public interface IDbData
    {
        IDbManager GetDbManager();
        transactionCache GetTransactionCache();
    }
}
