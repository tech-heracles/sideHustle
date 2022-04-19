using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace DbCore.DbArkaBanka
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje Urdhere me objekte te tipit clsKokaUrdherPagese
    ///  dhe lejon te manipulohet kjo Urdhere nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colKokaUrdherPagese : List<clsKokaUrdherPagese>
    {

        #region Konstruktor

        /// <summary>
        /// 
        /// </summary>
        public colKokaUrdherPagese()
        {
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idNdermVit">id qe lidh ndermarrjen me vitin</param>
        public colKokaUrdherPagese(int idNdermVit)
        {
            using (clsDatabaseArkaBanka db = new clsDatabaseArkaBanka())
            {
                mbushKokat(db.ktheGjitheKokaUrdherPagese(idNdermVit), db);
            }
        }
        public colKokaUrdherPagese(IEnumerable<clsKokaUrdherPagese> collection)
            : base(collection)
        {

        }




        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbRegjistrim.clsKokaUrdherPagese"/>  qe ndodhet ne nje index te caktuar te arrayUrdher-es
        /// </summary> 
        public new clsKokaUrdherPagese this[int index]
        {
            get { return ((clsKokaUrdherPagese)base[index]); }
        }

        /// <summary>
        /// merr Urdher pagesat dt sipas idndermviti
        /// </summary>
        /// <param name="idndermvit"></param>
        /// <returns></returns>
        public static DataTable merrKokaUrdherPagesaDT(int idndermvit, int idperdoruesi)
        {
            using (clsDatabaseArkaBanka db = new clsDatabaseArkaBanka())
            {
                return db.merrKokaUrdherPageseDT(idndermvit,idperdoruesi);
            }
        }
        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbRegjistrim.clsKokaUrdherPagese"/> 
        /// </summary>
        private bool mbushKokat(DataTable dt, clsDatabaseArkaBanka db)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsKokaUrdherPagese koka = new clsKokaUrdherPagese();
                    //koka.mbushKokaUrdherPagese(rreshti, db);
                    Add(new clsKokaUrdherPagese(rreshti, db));
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
