using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbAdmin
{
    public class colGrupAuditimiTrupi : System.Collections.Generic.List<clsGrupAuditimiTrupi>
    {
        #region Metoda Publike

        public new clsGrupAuditimiTrupi this[int index]
        {
            get { return ((clsGrupAuditimiTrupi)base[index]); }
        }

        public bool mbushTrupinGrupitAuditimit(int idgrupi)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool sukses = mbushGrupetAuditimit(data.ktheTrupinGrupitAuditimit(idgrupi));
            data.Dispose();
            return sukses;
        }

        #endregion

        #region Metoda Private

        private bool mbushGrupetAuditimit(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsGrupAuditimiTrupi grup = new clsGrupAuditimiTrupi();
                    //grup.mbushGrupAuditimTrup(rreshti);
                    Add(new clsGrupAuditimiTrupi(rreshti));
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }

        #endregion
        [Obsolete("Perdor: bool mbushGrupetAuditimit(DataTable dt)", true)]
        public colGrupAuditimiTrupi mbushArrayListGrupetAuditimit(DataSet ds)
        {
            colGrupAuditimiTrupi col = new colGrupAuditimiTrupi();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsGrupAuditimiTrupi grup = new clsGrupAuditimiTrupi();

                grup.IdGrupi = int.Parse(rreshti[0].ToString());
                grup.IdTabele = int.Parse(rreshti[1].ToString());
                grup.IdKolone = int.Parse(rreshti[2].ToString());
                col.Add(grup);
            }
            return col;
        }
    }
}
