using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using AlphaWeb.Core.Interfaces.Data;

namespace DbCore.DbAdmin
{

    public class colShopsHierarkiStatus : List<clsShopsHierarkiStatus>, IDataBaseReader
    {
        #region Konstruktoret

      

        public colShopsHierarkiStatus()
        {
            using (clsDatabaseAdmin dbShopsHierarkiStatus = new clsDatabaseAdmin())
            {
                dbShopsHierarkiStatus.ktheStatuseShopsHierarki(this);
            }
        }

        #endregion

        #region Metoda Publike
   
        #endregion

        #region Metoda Private
        public void Mbush(IDataRecord record)
        {
            clsShopsHierarkiStatus ShopsHierarkiStatus = new clsShopsHierarkiStatus(record);
            this.Add(ShopsHierarkiStatus);
        }
    
       
        #endregion
    }
}
