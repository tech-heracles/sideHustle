using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbShare
{
   public class colFilterPerRaport : System.Collections.Generic.List<clsFilterPerRaport>
    {
        public new clsFilterPerRaport this[int index]=>base[index];
      
       /// <summary>
       /// Mbush koleksionin me filtrat e nje raporti
       /// </summary>
       /// <param name="idRaporti"></param>
       /// <returns></returns>
        public bool merrFilterPerRaport(int idRaporti)
        {
            clsDatabaseShare data = new clsDatabaseShare();
            bool mbush = mbushColFilterPerRaport(data.merrFilterPerRaport(idRaporti));
            data.Dispose();
            return mbush;
        }
        public colFilterPerRaport mbushArrayListFilterKoka(DataSet ds)
        {
            
            colFilterPerRaport filtrat = new colFilterPerRaport();

            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsFilterPerRaport filtri = new clsFilterPerRaport();
                filtri.IdFilter = int.Parse(rreshti[0].ToString());
                filtri.FilterEmri = rreshti[1].ToString();
                filtri.FilterPershkrimi = rreshti[2].ToString();
                filtri.IdRaporti= int.Parse(rreshti[3].ToString());
                filtrat.Add(filtri);
            }
            return filtrat;
        }

        private bool mbushColFilterPerRaport(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsFilterPerRaport filtri = new clsFilterPerRaport();
                    //filtri.mbushFilterPerRaport(rreshti);
                    Add(new clsFilterPerRaport(rreshti));
                }
                return true;
            //}
            //catch (Exception)
            //{
            //    return false;                
            //}
        }

    }
}
