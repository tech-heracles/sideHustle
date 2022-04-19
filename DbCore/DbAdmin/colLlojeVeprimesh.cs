using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;

namespace DbCore.DbAdmin
{
   public class colLlojeVeprimesh : System.Collections.Generic.List<clsLlojVeprimi >
    {
        public new clsLlojVeprimi this[int index]
        {
            get { return ((clsLlojVeprimi)base[index]); }
        }


        public colLlojeVeprimesh mbushArrayListArtikujshZevendesues(DataSet ds)
        {
            colLlojeVeprimesh llojeveprimesh = new colLlojeVeprimesh();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsLlojVeprimi lloj = new clsLlojVeprimi();

                lloj.IdLloji  = int.Parse(rreshti[0].ToString());
                lloj.PershkrimLloji  =rreshti[1].ToString();
                lloj.Prioriteti = int.Parse( rreshti[2].ToString());

                llojeveprimesh.Add(lloj);
            }
            return llojeveprimesh;
        }
    }
}
