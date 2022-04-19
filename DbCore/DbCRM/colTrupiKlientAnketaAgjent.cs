using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace DbCore.DbCRM
{
  public  class colTrupiKlientAnketaAgjent: System.Collections.Generic.List<clsTrupiKlientAnketaAgjent>
    {
        #region Konstruktore

        /// <summary>
        /// konstruktor bosh
        /// </summary>
        public colTrupiKlientAnketaAgjent()
        {
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbCRM.clsTrupiKlientAnketaAgjent"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsTrupiKlientAnketaAgjent this[int index]
        {
            get { return ((clsTrupiKlientAnketaAgjent)base[index]); }
        }

        /// <summary>
        /// metoda per shtimin e nje obj clsTrupiKlientAnketaAgjent ne nje arraylist
        /// </summary>
        public bool shtotrupiKlientAnketaAgjent(clsTrupiKlientAnketaAgjent trupiKlientAnketaAgjent)
        {
            base.Add(trupiKlientAnketaAgjent);
            if (base.Contains(trupiKlientAnketaAgjent))
                return true;
            else return false;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsTrupiKlientAnketaAgjent ne nje arraylist
        /// </summary>
        public bool fshitrupiKlientAnketaAgjent(clsTrupiKlientAnketaAgjent trupiKlientAnketaAgjent)
        {
            base.Remove(trupiKlientAnketaAgjent);
            if (base.Contains(trupiKlientAnketaAgjent))
                return false;
            else return true;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsTrupiKlientAnketaAgjent ne nje arraylist
        /// </summary>
        public bool fshiGjithetrupiKlientAnketaAgjent()
        {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsTrupiKlientAnketaAgjent ne nje arraylist
        /// </summary>
        public void fshiKetetrupiKlientAnketaAgjent(int index)
        {
            base.RemoveAt(index);
        }

        /// <summary>
        /// metoda shton nje obj te ri ne array list ne pozicionin e percaktuar nga index-i
        /// </summary>
        public void shtotrupiKlientAnketaAgjentNeIndeksin(int index, clsTrupiKlientAnketaAgjent trupiKlientAnketaAgjent)
        {
            base.Insert(index, trupiKlientAnketaAgjent);
        }

        /// <summary>
        /// metoda gjen index-in ne liste ne te cilin ndodhet nje obj i caktuar
        /// </summary>
        public int indeksitrupiKlientAnketaAgjent(clsTrupiKlientAnketaAgjent trupiKlientAnketaAgjent)
        {
            return base.IndexOf(trupiKlientAnketaAgjent);
        }

        /// <summary>
        /// metoda kontrollon nqs nje obj ndodhet ne arraylist
        /// </summary>
        public bool ekzistontrupiKlientAnketaAgjent(clsTrupiKlientAnketaAgjent trupiKlientAnketaAgjent)
        {
            if (base.Contains(trupiKlientAnketaAgjent))
                return true;
            else return false;
        }

        /// <summary>
        /// metoda qe kthen nr e obj qe ka arraylist
        /// </summary>
        public int numritrupiKlientAnketaAgjent()
        {
            return base.Count;
        }

        public DataTable merrtrupiKlientAnketaAgjentSipasIdKoka(int idKoka)
        {
            clsDatabaseCRM dbCRM = new clsDatabaseCRM();
            DataTable dt = dbCRM.merrTrupiKlientAnketeAgjentSipasIdKoka(idKoka);
            dbCRM.Dispose();
            return dt;
        }
        public static  DataTable merrTrupiKlientAnketeAgjentSipasIdKokaDt(int idKoka)
        {
            clsDatabaseCRM dbCRM = new clsDatabaseCRM();
            DataTable dt = dbCRM.merrTrupiKlientAnketeAgjentSipasIdKokaDt(idKoka);
            dbCRM.Dispose();
            return dt;
        }
        public bool mbushtrupiKlientAnketaAgjentSipasIdKoka(int idKoka)
        {
            clsDatabaseCRM dbCRM = new clsDatabaseCRM();
            bool sukses = mbushtrupiKlientAnketaAgjent(dbCRM.merrTrupiKlientAnketeAgjentSipasIdKoka(idKoka));
            dbCRM.Dispose();
            return sukses;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbCRM.clsTrupiKlientAnketaAgjent"/> 
        /// </summary>
        private bool mbushtrupiKlientAnketaAgjent(DataTable dt)
        {
            try
            {
                foreach (DataRow rreshti in dt.Rows)
                {
                    clsTrupiKlientAnketaAgjent trupiKlientAnketaAgjent = new clsTrupiKlientAnketaAgjent();
                    trupiKlientAnketaAgjent.mbushTrupiKlientAnketaAgjent(rreshti);
                    this.Add(trupiKlientAnketaAgjent);
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        #endregion



        public static DataTable merrTrupKlientAnketaAgjent(int idNdermarrje, int idAgjenti, int idKlienti, string dtTakimi,int idPerdoruesi)
        {
            clsDatabaseCRM dbCRM = new clsDatabaseCRM();
            DataTable dt = dbCRM.merrTrupKlientAnketaAgjent(idNdermarrje, idAgjenti, idKlienti, dtTakimi,idPerdoruesi);
            dbCRM.Dispose();
            return dt;
        }
    }
}
