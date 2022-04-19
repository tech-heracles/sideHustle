using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsFleteDoganoreTrupi
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colFleteDoganoreTrupi : System.Collections.Generic.List<clsFleteDoganoreTrupi>
    {
        #region Konstruktor

        /// <summary>
        /// konstruktor pa parametra
        /// </summary>
        public colFleteDoganoreTrupi()
        {
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="id">id e kokes</param>
        public colFleteDoganoreTrupi(int id)
        {
            clsDatabaseRegjistrim dbFleteDoganoreTrupi = new clsDatabaseRegjistrim();
            mbushFletatDoganoreTrupi(dbFleteDoganoreTrupi.ktheFleteDoganoreTrupiSipasKoka(id));
            dbFleteDoganoreTrupi.Dispose();
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbRegjistrim.clsFleteDoganoreTrupi"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary> 
        public new clsFleteDoganoreTrupi this[int index]
        {
            get { return ((clsFleteDoganoreTrupi)base[index]); }
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbRegjistrim.clsFleteDoganoreTrupi"/> 
        /// </summary>
        private bool mbushFletatDoganoreTrupi(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsFleteDoganoreTrupi trupi = new clsFleteDoganoreTrupi();
                    //trupi.mbushFleteDoganoreTrupi(rreshti);
                    Add(new clsFleteDoganoreTrupi(rreshti));
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //    //throw;
            //}
            return true;
        }

        #endregion
        
    }
}
