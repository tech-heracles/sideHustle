using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbAdmin
{
    public class colVleratKonfigurimiKasa : List<clsVleraKonfigurimiKasa>
    {
        #region Konstruktoret

        public colVleratKonfigurimiKasa()
        {
        }
        public colVleratKonfigurimiKasa(int idkonfigurimi)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            this.mbushVleratKonfigurimKasash(data.merrVleraSipasIdKonfigurimi(idkonfigurimi));
            data.Dispose();
        }

        #endregion

        public new clsVleraKonfigurimiKasa this[int index]
        {
            get { return ((clsVleraKonfigurimiKasa)base[index]); }
        }

        public void mbushVleraNiveleTaksa(int idkonfigurimi)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            this.mbushVleratKonfigurimKasash(data.merrVleraSipasNiveleTaksa(idkonfigurimi));
            data.Dispose();
        }

        public static DataTable ktheLlojeKasashDhePeshoreshSipasLlojit(int lloji)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                return data.ktheLlojeKasashDhePeshoreshSipasLlojit(lloji);
            }
        }

        public static DataTable ktheLlojeKasashDhePeshoreshAll()
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                return data.ktheLlojeKasashDhePeshoreshAll();
            }
        }

        #region metoda private

        private bool mbushVleratKonfigurimKasash(DataTable dt)
        {
            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {

                    //clsVleraKonfigurimiKasa kasa = new clsVleraKonfigurimiKasa();
                    //kasa.mbushVleraKonfigurimKase(rreshti);
                    this.Add(new clsVleraKonfigurimiKasa(rreshti));
                }
            //}
            //catch (Exception)
            //{
            //    return false;
            //    //throw;
            //}
            return true;
        }
        #endregion

      public string ktheVlereOpsioni(string pershkrimi)
        {
            var opsioni = this.Find(delegate (clsVleraKonfigurimiKasa vlkf)
            {
                return vlkf.PershkrimOpsioni == pershkrimi;
            });
            return opsioni == null ? "" : opsioni.Vlera;
        }
        //shtoi denisi  per te konvertuar string ne boolean
        public bool ktheVlereOpsioniKasaBOOL(string pershkrimi)
        {
            var opsioni = this.Find(delegate (clsVleraKonfigurimiKasa vlkf)
            {
                return vlkf.PershkrimOpsioni == pershkrimi;
            });
            bool OpsioniBool = false;
            if (opsioni == null)
            {
                return false;
            }
                bool.TryParse(opsioni.Vlera, out OpsioniBool);
                return OpsioniBool;
        }

        public string ktheVlereTakse(int idTakse)
        {
            if (idTakse != 0)
            {
                clsVleraKonfigurimiKasa vlera = this.Find(delegate(clsVleraKonfigurimiKasa vlkf)
                {
                    return vlkf.IdTaksa == idTakse;
                });
                return (vlera == null) ? "1" : vlera.Vlera;
            }
            else
                return "1";
        }
    }
}
