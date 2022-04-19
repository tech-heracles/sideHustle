using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbAdmin
{
    public class colNrAutom: System.Collections.Generic.List<clsNrAutom>
    {
        #region Metoda Publike

        public new clsNrAutom this[int index]
        {
            get { return ((clsNrAutom)base[index]); }
        }

        public bool mbushGjitheNumratAutomatike(int idndermarrje)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool sukses = mbushNumratAutomatike(data.ktheGjitheNumratAutomatike(idndermarrje),data);
            data.Dispose();
            return sukses;
        }
        public bool mbushGjitheNumratAutomatikeSipasKategorise(int idndermarrje, int idkatdok)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool sukses = mbushNumratAutomatike(data.ktheGjitheNumratAutomatikeSipasKategorise(idndermarrje, idkatdok),data);
            data.Dispose();
            return sukses;
        }
        //public static DataTable mbushNrAutomTeNdermarrjes(int idNdermVit);
        public static DataTable mbushNrAutomTeNdermarrjes(int idndermarrje)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            //return data.merrNrAutomTeNdermarrjes(idNdermVit);
            DataTable tabela = data.merrNrAutomTeNdermarrjes(idndermarrje);
            data.Dispose();
            return tabela;
        }

        public bool mbushGjitheNumratAutomatikePerKonfigurim(int idndermarrje, string kodKonfigurimi)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool sukses = mbushNumratAutomatike(data.ktheGjitheNumratAutomatikePerKonfigurim(idndermarrje, kodKonfigurimi), data);
            data.Dispose();
            return sukses;
        }

        #endregion

        #region Metoda Private

        private bool mbushNumratAutomatike(DataTable dt, clsDatabaseAdmin data)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsNrAutom nr = new clsNrAutom();
                    //nr.mbushNumerAuto(rreshti,data);
                    Add(new clsNrAutom(rreshti, data));
                }

            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return true;
        }

        #endregion
        [Obsolete("Perdor: bool mbushNumratAutomatike(DataTable dt)", true)]
        public colNrAutom mbushArrayListNumratAutomatike(DataSet ds)
        {
            colNrAutom numrat = new colNrAutom();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsNrAutom nr = new clsNrAutom();

                nr.IdNrAutom = int.Parse(rreshti[0].ToString());
                nr.KodiNrAutom = rreshti[1].ToString();
                nr.EmertimiNrAutom = rreshti[2].ToString();
                nr.FillonNrAutom = int.Parse(rreshti[4].ToString());
                nr.MbaronNrAutom = int.Parse(rreshti[5].ToString());
                nr.HapiNrAutom= int.Parse(rreshti[6].ToString());
                nr.DrejtimiNrAutom =int.Parse(rreshti[7].ToString());
                nr.NgaDataNrAutom = (DateTime)rreshti[9];
                nr.DeriMeNrAutom = (DateTime)rreshti[10];
                nr.MajtasNrAutom= rreshti[11].ToString();
                nr.DjathtasNrAutom = rreshti[12].ToString();
                nr.KategoriaNrAutom = int.Parse(rreshti[14].ToString());
                nr.PeriudhaNrAutom= int.Parse(rreshti[15].ToString());
                nr.GjatesiaNrAutom = int.Parse(rreshti[16].ToString());

                nr.IdNdermarja = int.Parse(rreshti[17].ToString());
                nr.Viti = int.Parse(rreshti[18].ToString());
                //nr.IdNderViti= int.Parse(rreshti[19].ToString());
                nr.IdPerdoruesi = int.Parse(rreshti[19].ToString());
                nr.OColNrAutoFundit = new colNrAutomatikFundit(nr.IdNrAutom);
                numrat.Add(nr);
            }
            return numrat;
        }
    }
}
