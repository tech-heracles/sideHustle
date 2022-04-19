using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbShare
{
   public class colKomponentPerFilter : System.Collections.Generic.List<clsKomponentPerFilter>
    {
        //public new clsKomponentPerFilter this[int index]
        //{
        //    get { return ((clsKomponentPerFilter)base[index]); }
        //}
       /// <summary>
       /// 
       /// </summary>
       /// <param name="idFilter"></param>
       /// <returns></returns>
        public bool merrKomponentPerFilter(int idFilter)
        {
            clsDatabaseShare data = new clsDatabaseShare();
            bool mbush = mbushColKomponentPerFilter(data.merrKomponentPerFilter(idFilter));
            data.Dispose();
            return mbush;
        }

        public colKomponentPerFilter mbushArrayListKomponentPerFilter(DataSet ds)
        {
            colKomponentPerFilter komponentet = new colKomponentPerFilter();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsKomponentPerFilter komponent = new clsKomponentPerFilter();
                komponent.IdKomponente = int.Parse(rreshti[0].ToString());
                komponent.KomponenteEmri = rreshti[1].ToString();
                komponent.IdFilter = int.Parse(rreshti[2].ToString());
                komponentet.Add(komponent);
            }
            return komponentet;
        }

        private bool mbushColKomponentPerFilter(DataTable dt)
        {
            foreach (DataRow rreshti in dt.Rows)
            {
                //clsKomponentPerFilter komponent = new clsKomponentPerFilter();
                //komponent.mbushKomponentPerFilter(rreshti);
                Add(new clsKomponentPerFilter(rreshti));
            }
            return true;
        }
    }
}
