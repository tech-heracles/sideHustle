using System;
using System.Collections;
using System.Data;

namespace DbCore.DbAdmin
{
    public class colNdermarrjeVitet : System.Collections.Generic.List<clsNdermarrjeViti>
    {
        #region Metoda Publike

        public new clsNdermarrjeViti this[int index]
        {
            get { return ((clsNdermarrjeViti)base[index]); }
        }

        public bool mbushGjitheViteELidhuraMeNdermarrje(int idNdermarrje)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                return mbushcolNermarrjeVitet(data.ktheGjitheViteELidhuraMeNdermarrje(idNdermarrje));
            }
        }

        public bool mbushGjitheNdermarrjeVitet()
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                return mbushcolNermarrjeVitet(data.ktheGjitheNdermarrjeVitet());
            }
        }

        public bool mbushVitetENdermarrjeve(ArrayList ndermarrjet)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                return mbushcolNermarrjeVitet(data.ktheVitetENdermarrjeve(ndermarrjet));
            }
        }

        public static DataTable ktheVitetENdermarrjeve(int idNdermarrje)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                return data.ktheGjitheViteELidhuraMeNdermarrje(idNdermarrje);
            }
        }


        #endregion

        #region Metoda Private

        private bool mbushcolNermarrjeVitet(DataTable dt)
        {
            //try
            //{
            foreach (DataRow rreshti in dt.Rows)
            {
                //clsNdermarrjeViti ndermarrjeViti = new clsNdermarrjeViti();
                //ndermarrjeViti.mbushNdermarrjeViti(rreshti);
                Add(new clsNdermarrjeViti(rreshti));
            }

            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }

        #endregion
        [Obsolete("Perdor: bool mbushcolNermarrjeVitet(DataTable dt)", true)]
        public colNdermarrjeVitet mbushArrayListNdermarrjeVitet(DataSet ds)
        {
            colNdermarrjeVitet vitet = new colNdermarrjeVitet();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsNdermarrjeViti viti = new clsNdermarrjeViti();

                viti.IdNderViti = int.Parse(rreshti[0].ToString());
                viti.IdViti = int.Parse(rreshti[1].ToString());
                viti.NdermarrjeViti = int.Parse(rreshti[2].ToString());
                viti.IdNdermarrje = int.Parse(rreshti[3].ToString());
                viti.NdermarrjeVitiMbyllur = (bool)(rreshti[4]);
                viti.NdermarrjeVitiFillim = DateTime.Parse(rreshti[5].ToString());
                viti.NdermarrjeVitiFund = DateTime.Parse(rreshti[6].ToString());
                viti.IdPerdoruesi = int.Parse(rreshti[7].ToString());
                vitet.Add(viti);
            }
            return vitet;
        }
    }
}
