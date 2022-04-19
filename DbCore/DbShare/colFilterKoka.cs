using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;

namespace DbCore.DbShare
{
    public class colFilterKoka : System.Collections.Generic.List<clsFilterKoka>
    {
        #region Metoda Publike

        public new clsFilterKoka this[int index]
        {
            get { return ((clsFilterKoka)base[index]); }
        }

        public bool mbushGjitheFilterKokaModuli(int idModuli)
        {
            clsDatabaseShare data = new clsDatabaseShare();
            bool mbush = mbushColFilterKoka(data.ktheGjitheFilterKokaModuli(idModuli));
            data.Dispose();
            return mbush;
        }

        #endregion

        #region Konstruktoret

        public colFilterKoka() 
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idRaporti"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNdermarrje"></param>
        public colFilterKoka(int idRaporti, int idPerdorues, int idNdermarrje)
        {
            clsDatabaseShare shareDb = new clsDatabaseShare();
            mbushColFilterKoka(shareDb.merrFilterPerModul(idRaporti, idPerdorues, idNdermarrje));
            shareDb.Dispose();
        }


        /// <summary>
        /// Kthen filtrat e te gjithe raporteve te nje moduli te caktuar
        /// </summary>
        /// <param name="idModul"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNdermarrje"></param>
        /// <returns></returns>
        public static colFilterKoka merrFiltratSipasModulit(int idModul, int idPerdorues, int idNdermarrje)
        {
            clsDatabaseShare shareDb = new clsDatabaseShare();
            colFilterKoka coliltra = new colFilterKoka();
            coliltra.mbushColFilterKoka(shareDb.merrFiltratRaporteveSipasModulit(idModul, idPerdorues, idNdermarrje));
            shareDb.Dispose();
            return coliltra;
        }

        #endregion

        #region Metoda Private

        private bool mbushColFilterKoka(DataTable dt) 
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsFilterKoka filterKoka = new clsFilterKoka();
                    //filterKoka.mbushFilterKoka(rreshti);
                    Add(new clsFilterKoka(rreshti));
                }
                return true;
            //}
            //catch (Exception)
            //{
            //    return false;                
            //}
        }

        #endregion
        //[Obsolete("Perdor: bool mbushColFilterKoka(DataTable dt)", true)]
        //public colFilterKoka mbushArrayListFilterKoka(DataSet ds)
        //{
        //    clsFilterTrupi oTrupi = new clsFilterTrupi();
        //    colFilterKoka kokat = new colFilterKoka();

        //    foreach (DataRow rreshti in ds.Tables[0].Rows)
        //    {
        //        clsFilterKoka koka = new clsFilterKoka();

        //        koka.IdKokaFilter = int.Parse(rreshti[0].ToString());
        //        koka.KokaFilterKodi = rreshti[1].ToString();
        //        koka.KokaFilterPershkrimi = rreshti[2].ToString();
        //        koka.IdPerdoruesi = int.Parse(rreshti[3].ToString());
        //        //koka.Default = Boolean.Parse(rreshti[4].ToString());
        //        //koka.OColFilterTrupi = oTrupi.merrFilterTrupin(koka);
        //        kokat.Add(koka);
        //    }
        //    return kokat;
        //}

    }
}
