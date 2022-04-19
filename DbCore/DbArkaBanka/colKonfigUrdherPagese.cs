using System;
using System.Collections.Generic;
using System.Linq;

using System.Data;

namespace DbCore.DbArkaBanka
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsGrupPunonjesish
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colKonfigUrdherPagese : List<clsKonfigUrdherPagese>
    {
        #region Konstruktoret
        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public colKonfigUrdherPagese()
        {

        }
        /// <summary>
        /// konstruktori me 1 parametra
        /// </summary>
        /// <param name="idnderm">id e ndermarrjes</param>
        public colKonfigUrdherPagese(int idnderm, int lloji)
        {
            using (clsDatabaseArkaBanka data = new clsDatabaseArkaBanka())
            {
                mbushKonfigUrdherPagese(data.ktheGjitheKonfigUrdherPageseSipasNdermarjesDheLlojit(idnderm, lloji));
            }
        }
        /// <summary>
        /// konstruktori me 3 parametra
        /// </summary>
        /// <param name="idnderm">id e ndermarrjes</param>
        public colKonfigUrdherPagese(int idnderm, int lloji, string kodi)
        {
            using (clsDatabaseArkaBanka data = new clsDatabaseArkaBanka())
            {
                mbushKonfigUrdherPagese(data.ktheGjitheKonfigUrdherPageseSipasNdermarjesDheLlojitDheKodiLike(idnderm, lloji,kodi));
            }
        }
        public colKonfigUrdherPagese(IEnumerable<clsKonfigUrdherPagese> collection)
            : base(collection)
        {

        }

        #endregion

        #region Metoda Publike
        /// <summary>
        /// kthen objektin <see cref="clsKonfigUrdherPagese"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsKonfigUrdherPagese this[int index]
        {
            get { return ((clsKonfigUrdherPagese)base[index]); }
        }


        #endregion

        #region Metoda Private
        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        /// </summary>
        private bool mbushKonfigUrdherPagese(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsKonfigUrdherPagese konfig = new clsKonfigUrdherPagese();
                    //konfig.mbushKonfigUrdherPagese(rreshti);
                    Add(new clsKonfigUrdherPagese(rreshti));
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }
        public static DataTable colKonfigUrdherPageseNew(int idnderm, int lloji, string kodi)
        {
            clsDatabaseArkaBanka data = new clsDatabaseArkaBanka();
            
            return data.ktheGjitheKonfigUrdherPageseSipasNdermarjesDheLlojitDheKodiLike(idnderm, lloji, kodi);
            
        }
        #endregion
    }
}
