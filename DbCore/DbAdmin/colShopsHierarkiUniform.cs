using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using AlphaWeb.Core.Interfaces.Data;

namespace DbCore.DbAdmin
{

    public class colShopsHierarkiUniform : List<clsShopsHierarkiUniform>, IDataBaseReader
    {
            #region Konstruktoret
            public colShopsHierarkiUniform()
            {
                using (clsDatabaseAdmin dbShopsHierarkiUniform = new clsDatabaseAdmin())
                {
                dbShopsHierarkiUniform.ktheUniformShopsHierarki(this);
                }
            }

            #endregion

            #region Metoda Publike

            #endregion

            #region Metoda Private
            public void Mbush(IDataRecord record)
            {
            clsShopsHierarkiUniform ShopsHierarkiUniform = new clsShopsHierarkiUniform(record);
                this.Add(ShopsHierarkiUniform);
            }


            #endregion
        }
    }
