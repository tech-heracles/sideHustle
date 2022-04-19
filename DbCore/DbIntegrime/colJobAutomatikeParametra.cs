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

    public class colJobAutomatikeParametra : List<clsJobAutomatikeParametra>, IDataBase
    {
        #region Konstruktoret

        /// <summary>
        /// konstruktori pa parametra i klases
        /// </summary>
  
        public colJobAutomatikeParametra()
        {
        }

        public colJobAutomatikeParametra(int idskeduleri)
        {
            using (var db = new clsDatabaseIntegrime())
            {
                db.merrJobAutomatikeParametra(idskeduleri, this);
            }
        }

        #endregion

        #region Metoda Publike
        /// <summary>
        /// kthen objektin <see cref="DbCore.DbIntegrime.clsJobAutomatikeParametra"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsJobAutomatikeParametra this[int index]
        {
            get { return ((clsJobAutomatikeParametra)base[index]); }
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
            Add(new clsJobAutomatikeParametra(record));
        }

        #endregion

    }
}
