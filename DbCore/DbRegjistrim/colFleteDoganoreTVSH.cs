using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbRegjistrim
    {
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsFleteDoganoreTVSH
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colFleteDoganoreTVSH : System.Collections.Generic.List<clsFleteDoganoreTVSH>
        {
        #region Konstruktoret

        /// <summary>
        /// konstruktor pa parametra
        /// </summary>
        public colFleteDoganoreTVSH()
            {
            }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="id">id e kokes se fletes doganore</param>
        public colFleteDoganoreTVSH(int id)
        {
            clsDatabaseRegjistrim dbFleteDoganoreTVSH = new clsDatabaseRegjistrim();
            mbushFletaDoganoreTVSHt(dbFleteDoganoreTVSH.ktheFleteDoganoreTVSHSipasKoka(id));
            dbFleteDoganoreTVSH.Dispose();
        }
        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbRegjistrim.clsFleteDoganoreTVSH"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary> 
        public new clsFleteDoganoreTVSH this[int index]
            {
            get { return ((clsFleteDoganoreTVSH)base[index]); }
            }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje dataseti
        /// nepermjet kesaj metode informacioni kalohet nga dataset-i ne nje liste me objekte te tipit 

        ///  <see cref="DbCore.DbRegjistrim.clsFleteDoganoreTVSH"/> 
        /// </summary>
        private bool mbushFletaDoganoreTVSHt(DataTable dt)
            {
            //try
            //    {

                foreach (DataRow rreshti in dt.Rows)
                    {
                    //clsFleteDoganoreTVSH TVSH = new clsFleteDoganoreTVSH();
                    //TVSH.mbushFleteDoganoreTVSH(rreshti);
                    Add(new clsFleteDoganoreTVSH(rreshti));
                    }

            //    }
            //catch (Exception)
            //    {
            //    return false;
            //    //throw;
            //    }
            return true;
            }

        #endregion
       
        }
    }
