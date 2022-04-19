using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DbCore.DbShare
{
    public class colRaporteDesign : System.Collections.Generic.List<clsRaportDesign>
    {
        public colRaporteDesign()
        {
        }

        public colRaporteDesign(int idndermarje, int idraport)
        {
            using (clsDatabaseShare dbshare = new clsDatabaseShare())
            {
                MbushColNgaDt(dbshare.merrSipasNdermarjedheRaport(idndermarje, idraport));
            }
        }


        public colRaporteDesign(int idndermarje, string emerReal)
        {
            using (clsDatabaseShare dbshare = new clsDatabaseShare())
            {
                MbushColNgaDt(dbshare.merrSipasNdermarjedheEmerRaport(idndermarje, emerReal));
            }
        }

        public void merrSipasKategorise(int idkategori, int idNderm)
        {
            using (DbCore.DbShare.clsDatabaseShare db = new clsDatabaseShare())
            {
                Mbush(db.merrRaportDesignSipasKategorise(idkategori, idNderm));
            }
        }

        public void merrSipasRaportit(int idRaporti)
        {
            using(DbCore.DbShare.clsDatabaseShare db = new clsDatabaseShare()){
                Mbush(db.merrDesignSipasRaportit(idRaporti));
            }
        }

        public void MbushColNgaDt(DataTable dt)
        {
            foreach (DataRow row in dt.Rows)
            {
                Add(new clsRaportDesign(row));
            }
        }

        /// <summary>
        /// kthen dizajnin e zgjedhur ose ta parin ne radhe nese nuk eshte zgjedhur asnje
        /// </summary>
        /// <returns></returns>
        public clsRaportDesign MerrDizajnTeZgjedhur()
        {
            var iZgjedhur = this.FirstOrDefault(x => x.Zgjedhur);
            if (iZgjedhur == null) return this.OrderBy(x => x.IdRaportDesign).First();
            return iZgjedhur;
        }


        private void Mbush(DataTable dt)
        {
            foreach (DataRow rreshti in dt.Rows)
            {
                Add(new clsRaportDesign(rreshti));
            }
        }
    }
}
