using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaWeb.Core.Interfaces.Data;
using DbCore.IMBUtils.DataBase;

namespace DbCore.DbIntegrime
{

    public class colJobAutomatike : List<clsJobAutomatike>, IDataBase
    {
        #region Konstruktoret

        /// <summary>
        /// konstruktori pa parametra i klases
        /// </summary>
  
        public colJobAutomatike()
        {
            using (var db=new clsDatabaseIntegrime())
            {
                db.merrJobAutomatike(this);
            }
        }
        #endregion

        #region Metoda Publike
        /// <summary>
        /// kthen objektin <see cref="DbCore.DbIntegrime.clsJobAutomatike"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsJobAutomatike this[int index]
        {
            get { return ((clsJobAutomatike)base[index]); }
        }
        public clsMesazh Ruaj()
        {
            throw new NotImplementedException();
        }
        public clsMesazh Modifiko()
        {
            throw new NotImplementedException();
        }

        public clsMesazh Fshi()
        {
            throw new NotImplementedException();
        }

        public void Mbush(IDataRecord record)
        {
            Add(new clsJobAutomatike(record));
        }

        #endregion

    }
}
