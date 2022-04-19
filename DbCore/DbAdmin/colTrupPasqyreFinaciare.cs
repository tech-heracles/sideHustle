using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbAdmin
{
    public class colTrupPasqyreFinaciare : System.Collections.Generic.List<clsTrupPasqyreFinanciare>
    {
        public new clsTrupPasqyreFinanciare this[int index]
        {
            get { return ((clsTrupPasqyreFinanciare)base[index]); }
        }

        public colTrupPasqyreFinaciare mbushArrayListVitet(DataSet ds)
        {
            colTrupPasqyreFinaciare colTrup = new colTrupPasqyreFinaciare();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsTrupPasqyreFinanciare trupi = new clsTrupPasqyreFinanciare();

                trupi.IdTrupi = int.Parse(rreshti[0].ToString());
                trupi.IdKoka = int.Parse(rreshti[1].ToString());
                trupi.KodiZerit = rreshti[2].ToString();
                trupi.PershkrimiZerit = rreshti[3].ToString();
                trupi.PrindiZerit = int.Parse(rreshti[4].ToString());
                trupi.NiveliZerit = int.Parse(rreshti[5].ToString());
                trupi.LlojiZerit = rreshti[6].ToString();

                colTrup.Add(trupi);
            }
            return colTrup;
        }
    }
}
