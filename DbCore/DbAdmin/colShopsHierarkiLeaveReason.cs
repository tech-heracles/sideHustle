using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using AlphaWeb.Core.Interfaces.Data;

namespace DbCore.DbAdmin
    
{

    public class colShopsHierarkiLeaveReason : List<clsShopsHierarkiLeaveReason>, IDataBaseReader
    {
            public colShopsHierarkiLeaveReason()
            {
                using (clsDatabaseAdmin dbShopsHierarkiLeaveReason = new clsDatabaseAdmin())
                {
                   dbShopsHierarkiLeaveReason.ktheLeaveReasonShopsHierarki(this);
                }
            }

     
            #region Metoda Publike

            #endregion

            #region Metoda Private
            public void Mbush(IDataRecord record)
            {
            clsShopsHierarkiLeaveReason ShopsHierarkiLeaveReason = new clsShopsHierarkiLeaveReason(record);
                this.Add(ShopsHierarkiLeaveReason);
            }
            #endregion
        }
    }

