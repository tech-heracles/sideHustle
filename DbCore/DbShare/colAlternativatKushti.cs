using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;

namespace DbCore.DbShare
{
    public class colAlternativatKushti : System.Collections.Generic.List<clsAlternativaKushti >
    {
        public new clsAlternativaKushti this[int index]
        {
            get { return ((clsAlternativaKushti)base[index]); }
        }

        public colAlternativatKushti()
        {

        }

        public colAlternativatKushti(int idkushti) { 
            clsDatabaseShare data = new clsDatabaseShare();
            mbushAlternativaKushtesh(data.merrAlternativKushtiSipasIdKushti(idkushti));
            data.Dispose();
        }

      
        public clsAlternativaKushti MerrAlternativen(string kodi)
        {
            return Find(x => x.Kodi == kodi);
        }
        /// <summary>
        /// marrim nje dictionary me gjithe alternativat e zgjedhura te kushteve,aryseja pse zgjidhet dictionary eshte per 
        /// arsye shpejtese ne kerkim,pasi dictionary eshte me i shpejt
        /// </summary>
        /// <param name="idKonfigurimi"></param>
        /// <returns></returns>
        public static Dictionary<string, clsAlternativaKushti> MerrAlternativaKushtiSipasIdKonfigurimi(int idKonfigurimi)
        {
            Dictionary<string, clsAlternativaKushti> dic = new Dictionary<string, clsAlternativaKushti>();
            
            using (clsDatabaseShare dbShare = new clsDatabaseShare())
            {
                foreach (clsAlternativaKushti alt in dbShare.merrAlternativKushtiSipasIdKonfigurimitAll(idKonfigurimi))
                    dic.Add(alt.Kodi, alt);
            }
            return dic;
        }

        private bool mbushAlternativaKushtesh(DataTable dt)
        {

            //try
            //{

                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsAlternativaKushti trupi = new clsAlternativaKushti();
                    //trupi.mbushAlternativKusht(rreshti);
                    this.Add(new clsAlternativaKushti(rreshti));
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //    //throw;
            //}
            return true;

        }
        [Obsolete("Nuk perdoret me: perdor mbushAlternativaKushtesh(DataTable) dhe mbushAlternativKusht(DataRow)", false)]
        public colAlternativatKushti mbushArrayListAlternativaKushtesh(DataSet ds)
        {
            colAlternativatKushti trupat = new colAlternativatKushti();

            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsAlternativaKushti trupi = new clsAlternativaKushti();
                trupi.IdAlternativaKushti  = int.Parse(rreshti[0].ToString());
                trupi.Alternativa  = rreshti[1].ToString();
                trupi.IdKushti  = int.Parse (rreshti[2].ToString());
                trupat.Add(trupi);
            }
            return trupat;
        }
    }
}
