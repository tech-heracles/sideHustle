using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbKontabiliteti
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsSkemaKontabelTrupi
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colSkemaKontabelTrupi : System.Collections.Generic.List<clsSkemaKontabelTrupi>,
            System.Collections.Generic.IEnumerable<clsSkemaKontabelTrupi>
    {
        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbKontabiliteti.clsSkemaKontabelTrupi"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsSkemaKontabelTrupi this[int index]
        {
            get { return ((clsSkemaKontabelTrupi)base[index]); }
        }

        /// <summary>
        /// Metoda kthen nje collection me objekte te tipit <see cref="DbCore.DbKontabiliteti.clsSkemaKontabelTrupi"/> 
        /// duke filtruar sipas ID-se se skemes kontabel
        /// Thirret funksioni <see cref="DbCore.DbKontabiliteti.clsDatabaseKontabilitet.ktheSkemeTrupiSipasIdKoka"/>
        /// </summary>
        public colSkemaKontabelTrupi merrSkemaTrupiSipasIdKoka(string id)
        {
            //DbCore.DbKontabiliteti.clsDatabaseKontabilitet data= new clsDatabaseKontabilitet();
            //return data.merrSkemeTrupiSipasIdKoka(id);
            colSkemaKontabelTrupi data = new colSkemaKontabelTrupi();
            data.mbushSkemeTrupiSipasIdKoka(id);
            return data;

        }

        /// <summary>
        /// mbush trupin e skemes sipas id se kokes
        /// </summary>
        /// <param name="id">id e kokes</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushSkemeTrupiSipasIdKoka(string id)
        {
            clsDatabaseKontabilitet dbskemaTrupKont = new clsDatabaseKontabilitet();
            bool sukses = mbushSkematKontTrup(dbskemaTrupKont.ktheSkemeTrupiSipasIdKoka(id));
            dbskemaTrupKont.Dispose();
            return sukses;
        }

        /// <summary>
        /// mbush trupin e skemes
        /// </summary>
        /// <param name="id">id e skemes</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushTrupinESkemes(int id)
        {
            clsDatabaseKontabilitet dbskemaTrupKont = new clsDatabaseKontabilitet();
            bool sukses = mbushSkematKontTrup(dbskemaTrupKont.merrTrupinESkemes(id));
            dbskemaTrupKont.Dispose();
            return sukses;
        }

        #endregion

        #region Metoda private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbKontabiliteti.clsSkemaKontabelTrupi"/> 
        /// </summary>
        private bool mbushSkematKontTrup(DataTable dt)
        {
            try
            {

                foreach (DataRow rreshti in dt.Rows)
                {
                    clsSkemaKontabelTrupi trupi = new clsSkemaKontabelTrupi();
                    trupi.mbushSkemaKontabelTrupi(rreshti);
                    Add(trupi);
                }

            }
            catch (Exception)
            {
                return false;
                //throw;
            }
            return true;
        }

        #endregion
        [Obsolete("Perdor: bool mbushSkematKontTrup(DataTable dt)", true)]
        public colSkemaKontabelTrupi mbushArrayListSkemaKontTrupi(DataSet ds)
        {
            colSkemaKontabelTrupi trupat = new colSkemaKontabelTrupi();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsSkemaKontabelTrupi trupi = new clsSkemaKontabelTrupi();
                trupi.IdSkemaKontabelTrupi = int.Parse(rreshti[0].ToString());
                trupi.IdKoka = int.Parse(rreshti[1].ToString());
                trupi.IdSkemaModel= int.Parse(rreshti[2].ToString());
                trupi.DebiKrediSkemaKontabelTrupi = rreshti[3].ToString();
                trupi.IdLlogariSkemaKontabelTrupi =int.Parse(rreshti[4].ToString());
                trupi.KodiSkemaModel = rreshti[5].ToString();
                trupi.LlogariSkemaKontabelTrupi = rreshti[6].ToString();
                trupat.Add(trupi);
            }
            return trupat;
        }


    }
}
