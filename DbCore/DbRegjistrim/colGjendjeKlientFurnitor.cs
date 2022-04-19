using System;
using System.Collections.Generic;
using System.Data;
using DbCore.DbKontabiliteti;


namespace DbCore.DbRegjistrim
{
    public class colGjendjeKlientFurnitor : List<clsGjendjeKlientFurnitor>
    {
        public colGjendjeKlientFurnitor()
        {

        }

        public colGjendjeKlientFurnitor(int iddokumenti, int idniveli, clsDatabaseRegjistrim data)
        {
            MbushGjendjeKf(data.ktheGjendjeKFSipasIdDok(iddokumenti, idniveli));
        }

        public static colGjendjeKlientFurnitor KrijoGjendjetKlientFurnitor(List<int> idklientfurnitor, int idnivel, string nrdok, DateTime datedok, DateTime dateregj, colTrupatFletetKontabel trupi, List<string> rreshtakf)
        {
            var gjendjet = new colGjendjeKlientFurnitor();

            for (var i = 0; i < rreshtakf.Count; i++)
            {
                if (rreshtakf[i] != null)
                    //gjendjet.Add(clsGjendjeKlientFurnitor.KrijoGjendjeKlientFurnitor(int.Parse(idklientfurnitor[i].Item1.ToString()), idnivel, nrdok, datedok,dateregj, trupi[i], rreshtakf[i]));
                    gjendjet.Add(clsGjendjeKlientFurnitor.KrijoGjendjeKlientFurnitor(idklientfurnitor[i], idnivel, nrdok, datedok, dateregj, trupi[i], rreshtakf[i]));

                else break;
            }

            return gjendjet;
        }

        public new clsGjendjeKlientFurnitor this[int index] => base[index];

        #region Metoda Private

        private void MbushGjendjeKf(DataTable dt)
        {
            if (dt == null) return;
            foreach (DataRow rreshti in dt.Rows)
            {
                Add(new clsGjendjeKlientFurnitor(rreshti));
            }
        }

        #endregion
    }
}
