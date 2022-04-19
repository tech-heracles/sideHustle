using System.Collections.Generic;
using System.Data;
using System.Linq;
using DbCore.DbAdmin;
using DbCore.DbKontabiliteti;

namespace DbCore.DbRegjistrim
{
    public class colVeprimeKFTrupi : List<clsVeprimeKFTrupi>
    {
        public void MbushVeprimeKfTrupi(int idkoka)
        {
            using (var data = new clsDatabaseRegjistrim())
                MbushVeprimeKfTrupi(idkoka, data);
        }

        public void MbushVeprimeKfTrupi(int idkoka, clsDatabaseRegjistrim data)
        {
            MbushVeprimeKfTrupi(data.merrGjitheVeprimeKFTrupiNgaKoka(idkoka));
        }

        public new clsVeprimeKFTrupi this[int index] => base[index];

        public colKlienteFurnitore KtheColKf()
        {
            var colKlienteFurnitore = new colKlienteFurnitore();
            colKlienteFurnitore.AddRange(this.Select(trupMag => new clsKlientFurnitor(trupMag.IdKF)));

            return colKlienteFurnitore;
        }

        public colLlogarite KtheColLLogari()
        {
            var colLLogari = new colLlogarite();
            colLLogari.AddRange(this.Select(trupMag => new clsLlogari(trupMag.IdLlogKunderParti)));

            return colLLogari;
        }

        public colKlienteFurnitore KtheColKfKundra()
        {
            var colLLogari = new colKlienteFurnitore();
            colLLogari.AddRange(this.Select(trupMag => new clsKlientFurnitor(trupMag.IdKfKunderParti)));

            return colLLogari;
        }

        public colMonedhat KtheColMonedha()
        {
            var colMon = new colMonedhat();
            colMon.AddRange(this.Select(trupMag => new clsMonedha(trupMag.IdMonedha)));

            return colMon;
        }

        public colKokaShitje KtheColShitje(int idndermarje)
        {
            var col = new colKokaShitje();
            foreach (var trupMag in this)
            {
                if (trupMag.IdFatura != 0)
                {
                    var fature = new clsKokaShitje();
                    fature.mbushKokaShitjeSipasIDPaTrup(trupMag.IdFatura);
                    col.Add(fature);
                }
                else col.Add(new clsKokaShitje());
            }

            return col;
        }

        public List<int> KtheIdFature()
        {
            //shtohet sepse grida e jquerit fillon nga 1
            var idte = new List<int> {0};
            idte.AddRange(this.Select(trupMag => trupMag.IdFatura));

            return idte;
        }

        public List<int> KtheNivele()
        {
            //shtohet sepse grida e jquerit fillon nga 1
            var idte = new List<int> {0};
            idte.AddRange(this.Select(trupMag => trupMag.IdNivelFatura));

            return idte;
        }

        #region metoda private

        private void MbushVeprimeKfTrupi(DataTable dt)
        {
            foreach (DataRow rreshti in dt.Rows)
            {
                Add(new clsVeprimeKFTrupi(rreshti));
            }
        }

        #endregion
    }
}

