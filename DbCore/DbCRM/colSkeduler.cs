using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using DbCore.IMBUtils;
using DbCore.IMBUtils.Extensions;

namespace DbCore.DbCRM
{
    public class colSkeduler : System.Collections.Generic.List<clsSkeduler>
    {
        #region Konstruktore

        /// <summary>
        /// konstruktor bosh
        /// </summary>
        public colSkeduler()
        {
        }
        public colSkeduler(String kodNdermarrje,DateTime data)
        {
        
            clsDatabaseCRM dbCRM = new clsDatabaseCRM();
            bool sukses = mbushSkeduler(dbCRM.merrTakimeEPanisurSipasNdermarrjesDheDates(kodNdermarrje,data));
            dbCRM.Dispose();
           
        }

        
     
        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbCRM.clsSkeduler"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsSkeduler this[int index]
        {
            get { return ((clsSkeduler)base[index]); }
        }

        /// <summary>
        /// metoda per shtimin e nje obj clsSkeduler ne nje arraylist
        /// </summary>
        public bool shtoSkeduler(clsSkeduler Skeduler)
        {
            base.Add(Skeduler);
            if (base.Contains(Skeduler))
                return true;
            else return false;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsSkeduler ne nje arraylist
        /// </summary>
        public bool fshiSkeduler(clsSkeduler Skeduler)
        {
            base.Remove(Skeduler);
            if (base.Contains(Skeduler))
                return false;
            else return true;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsSkeduler ne nje arraylist
        /// </summary>
        public bool fshiGjitheSkeduler()
        {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// metoda per heqjen e nje obj clsSkeduler ne nje arraylist
        /// </summary>
        public void fshiKeteSkeduler(int index)
        {
            base.RemoveAt(index);
        }

        /// <summary>
        /// metoda shton nje obj te ri ne array list ne pozicionin e percaktuar nga index-i
        /// </summary>
        public void shtoSkedulerNeIndeksin(int index, clsSkeduler Skeduler)
        {
            base.Insert(index, Skeduler);
        }

        /// <summary>
        /// metoda gjen index-in ne liste ne te cilin ndodhet nje obj i caktuar
        /// </summary>
        public int indeksiSkeduler(clsSkeduler Skeduler)
        {
            return base.IndexOf(Skeduler);
        }

        /// <summary>
        /// metoda kontrollon nqs nje obj ndodhet ne arraylist
        /// </summary>
        public bool ekzistonSkeduler(clsSkeduler Skeduler)
        {
            if (base.Contains(Skeduler))
                return true;
            else return false;
        }

        /// <summary>
        /// metoda qe kthen nr e obj qe ka arraylist
        /// </summary>
        public int numriSkeduler()
        {
            return base.Count;
        }

        /// <summary>
        /// kontrollon nese ekzistojne takime per cdo date,nese ka data pa takime perdoruesit do i shfaqet mesazhi 
        /// </summary>
        /// <param name="fromDateStart"></param>
        /// <param name="fromDateEnd"></param>
        /// <param name="toDateStart"></param>
        /// <param name="toDateEnd"></param>
        /// <returns></returns>
        public static clsMesazh KontrolloTakimet(DateTime fromDateStart, DateTime fromDateEnd, DateTime toDateStart, DateTime toDateEnd, int idNdermarje, int idPerdoruesi,bool klonoDetyra,bool klonoKlient)
        {
            clsDatabaseCRM data = new clsDatabaseCRM();
            clsMesazh mesazh = data.KontrolloDatat(fromDateStart, fromDateEnd, toDateStart, toDateEnd, idNdermarje, idPerdoruesi, klonoDetyra, klonoKlient);
            return mesazh;
        }
        public static clsMesazh KlonoTakimet(DateTime fromDateStart, DateTime fromDateEnd, DateTime toDateStart, DateTime toDateEnd, int idAgjenti, int idPerdoruesi, int idNdermarrje,bool klonoDetyra,bool klonoKlient)
        {

            clsDatabaseCRM data = new clsDatabaseCRM();
            clsMesazh mesazh = data.KlonoTakimet(fromDateStart, fromDateEnd, toDateStart, toDateEnd, idAgjenti, idPerdoruesi, idNdermarrje,klonoDetyra,klonoKlient);

            return mesazh;
        }
        /// <summary>
        /// Merr takimet sipas ndermarrjes.
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <returns></returns>
        public static DataTable merrSkedulerPerNdermarrje(int idNdermarrje)
        {
            clsDatabaseCRM dbCRM = new clsDatabaseCRM();
            DataTable dt = dbCRM.merrTakimeSipasNdermarrjes(idNdermarrje);
            dbCRM.Dispose();
            return dt;
        }
        public static DataTable merrTakimeSipasNdermarrjesAndAgjent(int idNdermarrje, int idagjent)
        {
            clsDatabaseCRM dbCRM = new clsDatabaseCRM();
            DataTable dt = dbCRM.merrTakimeSipasNdermarrjesAndAgjent(idNdermarrje, idagjent);
            dbCRM.Dispose();
            return dt;
        }

        public static DataTable merrHistorikTakimesh(int idNdermarrje, int idPerdoruesi, string dataNga, string dataDeri)
        {
            clsDatabaseCRM dbCRM = new clsDatabaseCRM();
            DataTable dt = dbCRM.merrHistorikTakimesh(idNdermarrje, idPerdoruesi,dataNga,dataDeri);
            dbCRM.Dispose();
            return dt;

        }

        public bool mbushSkedulerPerNdermarrje(int idNdermarrje)
        {
            clsDatabaseCRM dbCRM = new clsDatabaseCRM();
            bool sukses = mbushSkeduler(dbCRM.merrTakimeSipasNdermarrjes(idNdermarrje));
            dbCRM.Dispose();
            return sukses;
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbCRM.clsSkeduler"/> 
        /// </summary>
        private bool mbushSkeduler(DataTable dt)
        {
            try
            {
                foreach (DataRow rreshti in dt.Rows)
                {
                    clsSkeduler Skeduler = new clsSkeduler();
                    Skeduler.mbushTakim(rreshti);
                    this.Add(Skeduler);
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        #endregion


        /// <summary>
        /// e paperfunduar
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static List<LevizjeAgjentiModel> merrLevizetEAgjenteve(int idNdermarrje, string filter)
        {

            //temp

            clsDatabaseCRM dbCRM = new clsDatabaseCRM();
            DataTable dt = dbCRM.merrLevizjetEAgjenteve(idNdermarrje,filter);
            dbCRM.Dispose();
            return dt.ToList<LevizjeAgjentiModel>();
        }
      public  class LevizjeAgjentiModel
        {
            public int IdPerdoruesi { get; set; }
            public DateTime DateFillimRealizimi { get; set; }
            public string Agjenti { get; set; }
            public string Klienti { get; set; }
            public string Kohezgjatja { get; set; }
            public string NrTakimi { get; set; }
            public string TeKlienti { get; set; }
            public string Description { get; set; }
            public string KoordinateFillimi { get; set; }
            public string Ora { get { return DateFillimRealizimi.ToString("HH:mm"); } }
        }
    }
}
