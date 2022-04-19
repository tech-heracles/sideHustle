using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaWeb.Core.Interfaces.Data;
using DbCore.IMBUtils.DataBase;

namespace DbCore.DbShare
{

    public class colNjoftime : List<clsNjoftime>, IDataBase
    {
        #region Konstruktoret

        /// <summary>
        /// konstruktori pa parametra i klases
        /// </summary>

        public colNjoftime()
        {
        }
        public colNjoftime(int idperdorues,DateTime data)
        {
            using (var db=new clsDatabaseShare())
            {
                db.MerrNjoftimeSipasDates(idperdorues , data, this);
            }
        }
        #endregion

        #region Metoda Publike
        /// <summary>
        /// kthen objektin <see cref="DbCore.DbShare.clsNjoftime"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsNjoftime this[int index]
        {
            get { return ((clsNjoftime)base[index]); }
        }
        public clsMesazh Ruaj()
        {
            clsMesazh mesazh = null;
            using (var scope = new MyTransactionScope())
            {
                foreach (var njoftim in this)
                {
                    mesazh = njoftim.Ruaj();
                    if (!mesazh) return mesazh;
                }
                scope.Complete();
            }
            return mesazh;
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
            Add(new clsNjoftime(record));
        }

        #endregion

    }
}
