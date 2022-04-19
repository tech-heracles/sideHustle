using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DbCore.DbRegjistrim
{
    public class colTrupiMbylljeKF : List<clsTrupiMbylljeKF>
    {
        #region Konstruktoret

        /// <inheritdoc />
        /// <summary>
        /// konstruktoret
        /// </summary>
        public colTrupiMbylljeKF()
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// konstruktore me 1 parameter
        /// </summary>
        /// <param name="id">id e kokes</param>
        public colTrupiMbylljeKF(int id)
        {
            using (var db = new clsDatabaseRegjistrim())
                MbushMbylljeKfTrupi(db.ktheMbylljeTrupiKFSipasId(id));
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="clsAzhornimKFKoka"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary> 
        public new clsTrupiMbylljeKF this[int index] => base[index];

        public DbKontabiliteti.colLlogarite KtheColLlogarite(int idndermarje)
        {
            var colllog = new DbKontabiliteti.colLlogarite();
            colllog.AddRange(this.Select(trupMag => new DbKontabiliteti.clsLlogari(new DbKontabiliteti.clsKlientFurnitor(trupMag.IdKf).IdLlogari)));

            return colllog;
        }

        public DbKontabiliteti.colLlogarite KtheColLlogariteKunder(int idndermarje)
        {
            var colllog = new DbKontabiliteti.colLlogarite();
            colllog.AddRange(this.Select(trupMag => new DbKontabiliteti.clsLlogari(trupMag.IdLlogariKp)));

            return colllog;
        }

        public DbKontabiliteti.colKlienteFurnitore KtheColKlientFurnitor(int idndermarje)
        {
            var coll = new DbKontabiliteti.colKlienteFurnitore();
            coll.AddRange(this.Select(trupMag => new DbKontabiliteti.clsKlientFurnitor(trupMag.IdKf)));

            return coll;
        }

        #endregion
        
        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje dataseti
        /// nepermjet kesaj metode informacioni kalohet nga dataset-i ne nje liste me objekte te tipit 
        ///  <see cref="DbCore.DbRegjistrim.clsAzhornimKFKoka"/> 
        /// </summary>
        private void MbushMbylljeKfTrupi(DataTable dt)
        {
            foreach (DataRow rreshti in dt.Rows)
            {
                Add(new clsTrupiMbylljeKF(rreshti));
            }
        }

        #endregion
    }
}
