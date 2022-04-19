using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DbCore.DbRegjistrim
{
    public class colFleteDoganoreDetajimSub : System.Collections.Generic.List<clsFleteDoganoreDetajimSub>
    {
        #region Konstruktoret

        /// <summary>
        /// konstruktor pa parametra
        /// </summary>
        public colFleteDoganoreDetajimSub()
        {
        }
        public colFleteDoganoreDetajimSub(List<Dictionary<string, object>> vlerasub, out decimal shumataksa)
        {
            shumataksa = 0;
            for (int i = 0; i < vlerasub.Count; i++)
            {
                clsFleteDoganoreDetajimSub sub = new clsFleteDoganoreDetajimSub((Dictionary<string, object>)vlerasub[i],i);
                this.Add(sub);
                shumataksa += sub.VlTaksa;
            }
        }
        public colFleteDoganoreDetajimSub(object transport, object siguracion, object tjera, object vldog, object taksa, object rreshti)
        {

            DbCore.DbRegjistrim.clsFleteDoganoreDetajimSub oDetajim = new clsFleteDoganoreDetajimSub();


            oDetajim.VlTransport = decimal.Parse(transport.ToString());
            oDetajim.VlSiguracion = decimal.Parse(siguracion.ToString());
            oDetajim.VlTjera = decimal.Parse(tjera.ToString());
            oDetajim.VlDoganim = decimal.Parse(vldog.ToString());
            oDetajim.VlTaksa = decimal.Parse(taksa.ToString());
            oDetajim.Rreshti = int.Parse(rreshti.ToString());
            this.Add(oDetajim);
        }
        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="id">id e trupit</param>
        public colFleteDoganoreDetajimSub(int id)
        {
            clsDatabaseRegjistrim dbFleteDoganoreDetajim = new clsDatabaseRegjistrim();
            mbushFletetDoganoreDetajim(dbFleteDoganoreDetajim.ktheFleteDoganoreDetajimSipasTrupiSub(id));
            dbFleteDoganoreDetajim.Dispose();
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbRegjistrim.clsFleteDoganoreDetajim"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary> 
        public new clsFleteDoganoreDetajimSub this[int index]
        {
            get { return ((clsFleteDoganoreDetajimSub)base[index]); }
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
                    //clsFleteDoganoreDetajimSub trupi = new clsFleteDoganoreDetajimSub();
                    //trupi.mbushFleteDoganoreDetajim(rreshti);
                    Add(new clsFleteDoganoreDetajimSub(rreshti));
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
