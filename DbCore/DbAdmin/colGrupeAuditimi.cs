using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbAdmin
{
    public class colGrupeAuditimi : System.Collections.Generic.List<clsGrupAuditimi>
    {
        #region Metoda Publike

        public new clsGrupAuditimi this[int index]
        {
            get { return ((clsGrupAuditimi)base[index]); }
        }

        public colGrupAuditimiTrupi merrTrupinGrupitAuditimit(clsGrupAuditimi grup)
        {
            DbCore.DbAdmin.colGrupAuditimiTrupi db = new colGrupAuditimiTrupi();
            db.mbushTrupinGrupitAuditimit(grup.IdGrupi);
            return db;

        }

        public bool mbushGjitheGrupetAuditimit()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool sukses = mbushGrupetAuditimit(data.ktheGjitheGrupetAuditimit());
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
                    //clsGrupAuditimi grup = new clsGrupAuditimi();
                    //grup.mbushGrupAuditim(rreshti);
                    Add(new clsGrupAuditimi(rreshti));
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
        public colGrupeAuditimi mbushArrayListGrupetAuditimit(DataSet ds)
        {
            colGrupeAuditimi col = new colGrupeAuditimi();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsGrupAuditimi grup = new clsGrupAuditimi();

                grup.IdGrupi = int.Parse(rreshti[0].ToString());
                grup.NrGrupi = int.Parse(rreshti[1].ToString());
                grup.PershkrimiGrupi = rreshti[2].ToString();
                grup.IdPerdoruesi = int.Parse(rreshti[3].ToString());
                grup.OColTrupi = merrTrupinGrupitAuditimit(grup);
                col.Add(grup);
            }
            return col;
        }
    }
}