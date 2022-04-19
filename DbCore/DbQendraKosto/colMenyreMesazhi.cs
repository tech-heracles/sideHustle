using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbQendraKosto
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsMenyreMesazhi
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colMenyreMesazhi: System.Collections.Generic.List<clsMenyreMesazhi>
        {
        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbInventari.clsMenyreMesazhi"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsMenyreMesazhi this[int index]
            {
            get { return ((clsMenyreMesazhi)base[index]); }
            }


        public bool mbushGjitheMenyreMesazhi(int idGjuha)
        {
            clsDatabaseQendraKosto dbMetodeKostoje = new clsDatabaseQendraKosto();
            bool sukses = mbushMenyreMesazhi(dbMetodeKostoje.ktheGjitheMenyreMesazhi(idGjuha));
            dbMetodeKostoje.Dispose();
            return sukses;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbInventari.clsMetodeKostoje"/> 
        /// </summary>
        private bool mbushMenyreMesazhi(DataTable dt)
            {
            //try
            //    {

                foreach (DataRow rreshti in dt.Rows)
                    {
                    //clsMenyreMesazhi menyre = new clsMenyreMesazhi();
                    //menyre.mbushMenyreMesazhi(rreshti);
                    this.Add(new clsMenyreMesazhi(rreshti));
                    }

            //    }
            //catch (Exception)
            //    {
            //    return false;
            //    }
            return true;
            }

        #endregion
    
        }
    }

