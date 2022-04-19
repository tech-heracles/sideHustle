using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne nje skeme azhornimi
    ///  (Te dhenat  merren nga tabela : T_SKEMAAZHORNIMI)
    /// </summary>
    public class colSkemaAzhornimi : System.Collections.Generic.List<clsSkemaAzhornimi>
    {
        #region Konstruktor

        /// <summary>
        /// konstruktor pa parametra
        /// </summary>
        public colSkemaAzhornimi()
        {
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idndermarje">id e ndermarrjes</param>
        public colSkemaAzhornimi(int idndermarje)
        {
            clsDatabaseRegjistrim dbSkemaAzhornimi = new clsDatabaseRegjistrim();
            mbushSkematAzhornimit(dbSkemaAzhornimi.ktheGjitheSkematAzhornimit(idndermarje));
            dbSkemaAzhornimi.Dispose();
        }

        #endregion 


        #region Metoda Publike

        public new clsSkemaAzhornimi this[int index]
        {
            get { return ((clsSkemaAzhornimi)base[index]); }
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// Metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje dataseti
        /// nepermjet kesaj metode informacioni kalohet nga dataset-i ne nje liste me objekte te tipit 
        ///  <see cref="DbCore.DbRegjistrim.clsSkemaAzhornimi"/> 
        /// </summary>
        private bool mbushSkematAzhornimit(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsSkemaAzhornimi skema = new clsSkemaAzhornimi();
                    //skema.mbushSkemaAzhornimi(rreshti);
                    Add(new clsSkemaAzhornimi(rreshti));
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
        [Obsolete("Perdor: bool mbushSkematAzhornimit(DataTable dt)", true)]
        public colSkemaAzhornimi mbushArrayListSkematAzhornimit(DataSet ds)
        {
            colSkemaAzhornimi skemat = new colSkemaAzhornimi();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsSkemaAzhornimi skema = new clsSkemaAzhornimi();

                skema.IdSkemaAzhornimi = int.Parse(rreshti[0].ToString());
                skema.IdGrupiLlogaria = int.Parse(rreshti[1].ToString());
                skema.IdNengrupiLlogaria = int.Parse(rreshti[2].ToString());
                skema.MenyraAzhornimit = int.Parse(rreshti[3].ToString());
                skema.IdLlogariFitim = int.Parse(rreshti[4].ToString());
                skema.IdLlogariHumbje = int.Parse(rreshti[5].ToString());
                skema.IdNdermarje = int.Parse(rreshti[6].ToString());
                skema.IdNderViti = int.Parse(rreshti[7].ToString());
                skemat.Add(skema);
            }
            return skemat;
        }
    }
}