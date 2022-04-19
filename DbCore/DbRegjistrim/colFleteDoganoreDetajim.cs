using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbRegjistrim
{  
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsFleteDoganoreDetajim
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colFleteDoganoreDetajim : System.Collections.Generic.List<clsFleteDoganoreDetajim>
    {
        #region Konstruktoret

        /// <summary>
        /// konstruktor pa parametra
        /// </summary>
        public colFleteDoganoreDetajim()
        {
        }

        public colFleteDoganoreDetajim(object transport, object siguracion, object tjera, object vldog, object taksa, List<Dictionary<string, object>> vlerasub, decimal marzhiGabimit)
        {
            DbCore.DbRegjistrim.clsFleteDoganoreDetajim oDetajim = new clsFleteDoganoreDetajim();
            decimal shumataksa = 0;
            oDetajim.VlTransport = decimal.Parse(transport.ToString());
            oDetajim.VlSiguracion = decimal.Parse(siguracion.ToString());
            oDetajim.VlTjera = decimal.Parse(tjera.ToString());
            oDetajim.VlDoganim = decimal.Parse(vldog.ToString());
            oDetajim.VlTaksa = decimal.Parse(taksa.ToString());
            oDetajim.OColSub = new colFleteDoganoreDetajimSub(vlerasub,out shumataksa);
            if ((oDetajim.VlTaksa - shumataksa) > marzhiGabimit)
                throw new Exception("Totali i taksave te artikujve nuk eshte i barabarte me totalin e takses se fatures!");
           this.Add(oDetajim);
        }
        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="id">id e trupit</param>
        public colFleteDoganoreDetajim(int id)
        {
            clsDatabaseRegjistrim dbFleteDoganoreDetajim = new clsDatabaseRegjistrim();
            mbushFletetDoganoreDetajim(dbFleteDoganoreDetajim.ktheFleteDoganoreDetajimSipasTrupi(id));
            dbFleteDoganoreDetajim.Dispose();
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbRegjistrim.clsFleteDoganoreDetajim"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary> 
        public new clsFleteDoganoreDetajim this[int index]
        {
            get { return ((clsFleteDoganoreDetajim)base[index]); }
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbRegjistrim.clsFleteDoganoreDetajim"/> 
        /// </summary>
        private bool mbushFletetDoganoreDetajim(DataTable dt)
        {
            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsFleteDoganoreDetajim trupi = new clsFleteDoganoreDetajim();
                    //trupi.mbushFleteDoganoreDetajim(rreshti);
                    Add(new clsFleteDoganoreDetajim(rreshti));
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
        [Obsolete("Perdor: bool mbushFletetDoganoreDetajim(DataTable dt)", true)]
        public colFleteDoganoreDetajim mbushArrayListFleteDoganoreDetajim(DataSet ds)
        {
            colFleteDoganoreDetajim trupat = new colFleteDoganoreDetajim();

            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsFleteDoganoreDetajim trupi = new clsFleteDoganoreDetajim();

                trupi.IdFleteDoganoreDetajim = int.Parse(rreshti[0].ToString());
                trupi.IdFleteDoganoreTrupi = int.Parse(rreshti[1].ToString());
                trupi.VlTransport = decimal.Parse(rreshti[2].ToString());
                trupi.VlSiguracion =decimal.Parse(rreshti[3].ToString());
                trupi.VlTjera = decimal.Parse(rreshti[4].ToString());
                trupi.VlDoganim = decimal.Parse(rreshti[5].ToString());
                trupi.VlTjera = decimal.Parse(rreshti[6].ToString());
                //trupi.OColTrupi = new colFleteDoganoreTrupi();
                trupat.Add(trupi);
            }
            return trupat;
        }


    }
}
