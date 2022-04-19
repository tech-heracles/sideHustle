using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Globalization;
using System.Data;

namespace DbCore.DbAdmin
{
    public class colPeriudhaKontabel : System.Collections.Generic.List<clsPeriudhaKontabel>
    {

        #region Metoda Publike

        public new clsPeriudhaKontabel this[int index]
        {
            get { return ((clsPeriudhaKontabel)base[index]); }
        }

        public bool shtoPeriudhe(clsPeriudhaKontabel periudha)
        {
            base.Add(periudha);
            if (base.Contains(periudha))
                return true;
            else return false;
        }

        public bool fshiPeriudhe(clsPeriudhaKontabel periudha)
        {
            base.Remove(periudha);
            if (base.Contains(periudha))
                return false;
            else return true;
        }

        public bool fshiGjithePeriudhat()
        {
            base.Clear();
            if (base.Count == 0)
                return true;
            else
                return false;
        }

        public void fshiKetePeriudhe(int index)
        {
            base.RemoveAt(index);
        }

        public void shtoPeriudheNeIndeksin(int index, clsPeriudhaKontabel periudha)
        {
            base.Insert(index, periudha);
        }

        public int indeksiPeriudhes(clsPeriudhaKontabel periudha)
        {
            return base.IndexOf(periudha);
        }

        public bool ekzistonMonedha(clsPeriudhaKontabel periudha)
        {
            if (base.Contains(periudha))
                return true;
            else return false;
        }

        public int numriPeriudhave()
        {
            return base.Count;
        }

        /// <summary>
        /// Kthen true nese mbushet periudha kontabel me periudhat sipas vitit, pendryshe false <see cref="clsPeriudhaKontabel"/>.
        /// Theret funksionin
        /// :  <see cref="DbCore.DbAdmin.clsDatabaseAdmin.kthePeriudhaSipasViti"/> 
        /// </summary>
        public bool merrSipasViti(int idViti)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                return mbushPeriudhKontabel(data.kthePeriudhaSipasViti(idViti));
            }
        }

         public bool merrSipasVitiDheGjuhes(int idViti, int idgjuha)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                return mbushPeriudhKontabel(data.kthePeriudhatSipasVitiDheGjuhes(idViti, idgjuha));
            }
        }
        public bool merrSipasViti(int idViti, CultureInfo ci)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                return mbushPeriudhKontabel(data.kthePeriudhaSipasViti(idViti, ((ci.Name == "sq-AL") ? 0 : 1)));
            }
        }
        /// <summary>
        /// Kthen true nese mbushet periudha kontabel me periudhat sipas vitit, pendryshe false <see cref="clsPeriudhaKontabel"/>.
        /// Theret funksionin
        /// :  <see cref="DbCore.DbAdmin.clsDatabaseAdmin.kthePeriudhaSipasViti"/> 
        /// </summary>
        public bool merrSipasVitiEpaySlip(int idViti)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                return mbushPeriudhKontabel(data.kthePeriudhaSipasVitiEPaySlip(idViti));
            }
        }

        public bool merrSipasVitiEpaySlip(int idViti, CultureInfo ci)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                return mbushPeriudhKontabel(data.kthePeriudhaSipasVitiEPaySlip(idViti, ((ci.Name == "sq-AL") ? 0 : 1)));
            }
        }
        /// <summary>
        /// Kthen true nese mbushet periudha kontabel me te gjitha periudhat, pendryshe false <see cref="clsPeriudhaKontabel"/>.
        /// Theret funksionin
        /// :  <see cref="DbCore.DbAdmin.clsDatabaseAdmin.kthePeriudhaAll"/> 
        /// </summary>
        public bool merrTeGjithe()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool sukses = mbushPeriudhKontabel(data.kthePeriudhaAll());
            data.Dispose();
            return sukses;
        }
        #endregion

        #region Metoda Private

        /// <summary>
        /// Metode per te mbushur klasen clsPeriudhaKontabel. Therret clsPeriudhaKontabel.mbushPeriudhaKontabel
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <returns>kthe true nese ndodh mbushja pa gabime</returns>
        private bool mbushPeriudhKontabel(DataTable dt)
        {

            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsPeriudhaKontabel periudha = new clsPeriudhaKontabel();
                    //periudha.mbushPeriudhaKontabel(rreshti);
                    this.Add(new clsPeriudhaKontabel(rreshti));
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
        [Obsolete("mbushPeriudhKontabel(DataTable dt)", true)]
        public colPeriudhaKontabel mbushArrayListPeriudhaKontabel(DataSet ds)
        {
            colPeriudhaKontabel periudhat = new colPeriudhaKontabel();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsPeriudhaKontabel periudha = new clsPeriudhaKontabel();
                periudha.IdPeriudha = int.Parse(rreshti[0].ToString());
                periudha.IdViti = int.Parse(rreshti[1].ToString());
                periudha.NrPeriudha = int.Parse(rreshti[2].ToString());
                periudha.FillimiPeriudha = (DateTime)(rreshti[3]);
                periudha.MbarimiPeriudha = (DateTime)(rreshti[4]);
                periudha.Ekycur = bool.Parse(rreshti[5].ToString());
                periudha.EmerPeriudha = rreshti[6].ToString();
                periudhat.Add(periudha);
            }
            return periudhat;
        }

        /// <summary>
        /// Gjen periudhen kontabel dhe e ruan ne session
        /// </summary>
        /// <param name="Session"></param>
        public void GjejPeriudhen(System.Web.SessionState.HttpSessionState Session)
        {
            DateTime dtAktuale = Convert.ToDateTime(DateTime.Now.ToString());
            DbCore.mySessionObjects.ruajPeriudheKontabelNeSesion(new DbCore.DbAdmin.clsPeriudhaKontabel(), Session);
            bool ekziston = false;
            for (int i = 0; i < this.Count; i++)
            {
                if (dtAktuale >= this[i].FillimiPeriudha && dtAktuale <= this[i].MbarimiPeriudha.AddDays(1))
                {
                    DbCore.mySessionObjects.ruajPeriudheKontabelNeSesion(this[i], Session);
                    ekziston = true;
                    break;
                }
            }
            if (!ekziston)
            {
                if (dtAktuale <= this[0].FillimiPeriudha)
                    if (this[0].EmerPeriudha != DbCore.DbAdmin.clsPeriudhaKontabel.PeriudheFillestare)
                    {
                        DbCore.mySessionObjects.ruajPeriudheKontabelNeSesion(this[0], Session);
                    }
                    else
                    {
                        DbCore.mySessionObjects.ruajPeriudheKontabelNeSesion(this[1], Session);
                    }
                else
                    if (dtAktuale >= this[this.Count - 1].MbarimiPeriudha)
                        if (this[this.Count - 1].EmerPeriudha != DbCore.DbAdmin.clsPeriudhaKontabel.PeriudheMbyllje)
                        {
                            DbCore.mySessionObjects.ruajPeriudheKontabelNeSesion(this[this.Count - 1], Session);
                        }
                        else
                        {
                            DbCore.mySessionObjects.ruajPeriudheKontabelNeSesion(this[this.Count - 2], Session);
                        }
            }
        }
    }
}
