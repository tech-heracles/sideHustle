using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbAdmin
{
    public class colKolonatEmerLogjik : System.Collections.Generic.List<clsKoloneEmerLogjik>
    {
        #region Metoda Publike

        public new clsKoloneEmerLogjik this[int index]
        {
            get { return ((clsKoloneEmerLogjik)base[index]); }
        }

        public bool mbushKolonat(int id)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool sukses = mbushKolonat(data.merrKolonat(id));
            data.Dispose();
            return sukses;
        }

        #endregion

        #region Metoda Private

        private bool mbushKolonat(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsKoloneEmerLogjik kolone = new clsKoloneEmerLogjik();
                    //kolone.mbushKolone(rreshti);
                    Add(new clsKoloneEmerLogjik(rreshti));
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }

        #endregion
        [Obsolete("Perdor: bool mbushKolonat(DataTable dt)", true)]
        public colKolonatEmerLogjik mbushArrayListKolonat(DataSet ds)
        {
            colKolonatEmerLogjik col = new colKolonatEmerLogjik();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsKoloneEmerLogjik kolone = new clsKoloneEmerLogjik();

                kolone.IdKolone = int.Parse(rreshti[0].ToString());
                kolone.IdTabele = int.Parse(rreshti[1].ToString());
                kolone.NrKolone = int.Parse(rreshti[2].ToString());
                kolone.EmerRealKolone = rreshti[3].ToString();
                kolone.EmerLogjikKolone = rreshti[4].ToString();
                col.Add(kolone);
            }
            return col;
        }
    }
}