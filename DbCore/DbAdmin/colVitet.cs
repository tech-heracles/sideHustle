using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbAdmin
{
    public class colVitet : System.Collections.Generic.List<clsViti>
    {
        #region Metoda Publike

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public new clsViti this[int index]
        {
            get { return ((clsViti)base[index]); }
        }

        public colVitet() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ndermarrja"></param>
        public colVitet(int ndermarrja) 
        {
            using (clsDatabaseAdmin dbadmin = new clsDatabaseAdmin())
            {
                DataTable dt = dbadmin.merrVitetENdermarrjes(ndermarrja);
                if (!mbushVitet(dt))
                {
                    throw new Exception("ERROR: Colcetion-i i viteve nuk arriti te mbushet");
                }
            }
        }

        public colVitet(string ndermarrjet)
        {
            using (clsDatabaseAdmin dbadmin = new clsDatabaseAdmin())
            {
                DataTable dt = dbadmin.merrVitetENdermarrjes(ndermarrjet);
                if (!mbushVitet(dt))
                {
                    throw new Exception("ERROR: Colcetion-i i viteve nuk arriti te mbushet");
                }
            }
        }

        //public void merrVitet(int ndermarrja)
        //{
        //    clsDatabaseAdmin dbadmin = new clsDatabaseAdmin();
        //    DataTable dt = dbadmin.merrVitetENdermarrjes(ndermarrja);
        //    if (!mbushVitet(dt))
        //        throw new Exception("ERROR: Colcetion-i i viteve nuk arriti te mbushet");
        //}

        /// <summary>
        /// mbush nje klase me te gjithe vitet    e celura ne kete ndermarje
        /// </summary>
        /// <returns>kthen true nese mbushja kryhet me sukse, ne te kundert false</returns>
        public bool merrGjitheVitetENdermarjes(int idndermarje, clsDatabaseAdmin dbadmin)
        {            
            return mbushVitet(dbadmin.ktheGjitheVitetENdermarjes(idndermarje));
        }
        public bool merrGjitheVitetENdermarjes(int idndermarje)
        {
            using (clsDatabaseAdmin dbadmin = new clsDatabaseAdmin())
            {
                return merrGjitheVitetENdermarjes(idndermarje, dbadmin);
            }
        }
        public static colVitet merrGjitheVitetEMundshme()
        {
            using (clsDatabaseAdmin dbadmin = new clsDatabaseAdmin())
            {
                return mbushArrayListVitetKodi(dbadmin.merrVitetEMundshme());
            }
        }

        public bool mbushVitetTeNdermarjesDheRolit(int idRoli, int idNdermarje)
        {
            using (clsDatabaseAdmin dbadmin = new clsDatabaseAdmin())
            {
                return mbushVitet(dbadmin.ktheVitetTeNdermarjesDheRolit(idRoli, idNdermarje));
            }
        }
        public bool mbushVitetTeNdermarjesDheRolit(int idRoli, int idNdermarje, clsDatabaseAdmin dbadmin)
        {
            if(dbadmin == null) 
                dbadmin = new clsDatabaseAdmin();
            bool sukses = mbushVitet(dbadmin.ktheVitetTeNdermarjesDheRolit(idRoli, idNdermarje));            
            return sukses;
        }
        public static DataRow merrViteSipasNdermarjesDR(int idnderm, int idviti)
        {
            using (clsDatabaseAdmin dbartikuj = new clsDatabaseAdmin())
            {
                return dbartikuj.merrViteSipasNdermarjesDR(idnderm, idviti);
            }
        }
        public static DataTable merrVitetNdermarjeDT(int idnderm)
        {
            using (clsDatabaseAdmin dbartikuj = new clsDatabaseAdmin())
            {
                return dbartikuj.merrVitetNdermarjeDT(idnderm);
            }
        }

        public static DataTable MerrIdVitetMeKodPerNdermarrje(string ids)
        {
            using (var data = new clsDatabaseAdmin())
            {
                return data.MerrIdVitetMeKodPerNdermarrje(ids);
            }
        }

        #endregion

        #region Metoda private

        /// <summary>
        /// mbush vitet nga nje store procedure
        /// </summary>
        /// <param name="dt">merr si parameter datatable</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        private bool mbushVitet(DataTable dt) {

            //try
            //{
                foreach (DataRow rreshti in dt.Rows)
                {
                    //clsViti viti = new clsViti();
                    //viti.mbushViti(rreshti);
                    this.Add(new clsViti(rreshti));
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
        [Obsolete("Perdor: bool mbushVitet(DataTable dt)", true)]
        public colVitet mbushArrayListVitet(DataSet ds)
        {
            colVitet vitet = new colVitet();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsViti viti = new clsViti();

                viti.IdViti = int.Parse(rreshti[0].ToString());
                viti.KodiViti = rreshti[1].ToString();
                viti.FillimiViti = (DateTime)rreshti[2];
                viti.MbarimiViti = (DateTime)rreshti[3];
                viti.PeriudhaLloji = rreshti[4].ToString();
                viti.PeriudhaHapjes = bool.Parse(rreshti[5].ToString());
                viti.PeriudhaMbylljes = bool.Parse(rreshti[6].ToString());
                viti.IdPerdoruesi = int.Parse(rreshti[7].ToString());
                viti.IdKonfig = int.Parse(rreshti[8].ToString());
                viti.IdNdermarje = int.Parse(rreshti[9].ToString());
                vitet.Add(viti);
            }
            return vitet;
        }

        public static colVitet mbushArrayListVitetKodi(DataSet ds)
        {
            colVitet vitet = new colVitet();
            foreach (DataRow rreshti in ds.Tables[0].Rows)
            {
                clsViti viti = new clsViti();

                viti.IdViti = 0;
                viti.KodiViti = rreshti[0].ToString();
                viti.FillimiViti = DateTime.Now;
                viti.MbarimiViti = DateTime.Now;
                viti.PeriudhaLloji = "1";
                viti.PeriudhaHapjes = false;
                viti.PeriudhaMbylljes = false;
                viti.IdPerdoruesi = 0;
                viti.IdKonfig = 0;
                viti.IdNdermarje = 0;
                viti.IdLlogMbylljeViti = 0;
                vitet.Add(viti);
            }
            return vitet;
        }
    }
}
