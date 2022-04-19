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
    public class colNjoftimePerdorues : List<clsNjoftimePerdorues>, IDataBase
    {
        #region Konstruktoret

        /// <summary>
        /// konstruktori pa parametra i klases
        /// </summary>
        public colNjoftimePerdorues()
        {

        }

        #endregion

        #region Metoda Publike
        /// <summary>
        /// kthen objektin <see cref="DbCore.DbShare.clsNjoftimePerdorues"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>

        public new clsNjoftimePerdorues this[int index]
        {
            get { return ((clsNjoftimePerdorues)base[index]); }
        }

        public clsMesazh Ruaj()
        {
            clsMesazh mesazh = null;
            using (var scope = new MyTransactionScope())
            {
                foreach (var njoftimperdorues in this)
                {
                    mesazh = njoftimperdorues.Ruaj();
                    if (!mesazh) return mesazh;
                }
                scope.Complete();
            }
            return mesazh;
        }

        public clsMesazh Fshi()
        {
            throw new NotImplementedException();
        }

        public void Mbush(IDataRecord record)
        {
            throw new NotImplementedException();
        }

        public clsMesazh Modifiko()
        {
            throw new NotImplementedException();
        }  
        #endregion  
    }
}
