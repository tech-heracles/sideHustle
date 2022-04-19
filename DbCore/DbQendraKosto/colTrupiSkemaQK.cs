using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbQendraKosto
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsTrupiSkemaQK
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    /// <seealso cref="clsTrupiSkemaQK"/>
    public class colTrupiSkemaQK : List<clsTrupiSkemaQK>
    {
        #region Konstruktori

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public colTrupiSkemaQK()
        {
        }

        /// <summary>
        /// konstruktori me 1 parametra
        /// merr aktivitete trupi sipas idkoka
        /// </summary>
        /// <param name="idkoka">id e kokes</param>
        public colTrupiSkemaQK(int idkoka):this(idkoka, new clsDatabaseQendraKosto())
        {
           
        }
   public colTrupiSkemaQK(int idkoka,clsDatabaseQendraKosto db)
        {
            mbushTrupiSkema(db.TransCache.getTrupiSkemaQK(idkoka,db));
        }
    
        /// <summary>
        /// konstruktori qe implementon klasen baze
        /// </summary>
        /// <param name="collection">koleksion me clsAktiviteteTrupi</param>
        public colTrupiSkemaQK(IEnumerable<clsTrupiSkemaQK> collection)
            : base(collection)
        {

        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="clsTrupiSkemaQK"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsTrupiSkemaQK this[int index]
        {
            get
            {
                return ((clsTrupiSkemaQK)base[index]);
            }
        }



        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        /// </summary>
        /// <param name="dt">data table me te dhena te tipit skema trupi</param>
        /// <returns>true ose false nqs objekti u mbush ne rregull me te dhena</returns>
        internal bool mbushTrupiSkema(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsTrupiSkemaQK skema = new clsTrupiSkemaQK();
                    //skema.mbushTrupiSkema(rreshti);
                    Add(new clsTrupiSkemaQK(rreshti));
                }
            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }
        internal void mbushTrupiSkema(colTrupiSkemaQK coltrupi)
        {
            this.AddRange(coltrupi);
        }
        #endregion
    }
}
