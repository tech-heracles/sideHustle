using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using AlphaWeb.Core.Interfaces.Data;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Logging;

namespace DbCore.DbShare
{

    public class clsNjoftime : IDataBase
    {
        #region Atribute

        private int njoftimeid;
        private string titulli;
        private string permbajtja;
        private DateTime dt_fillimi;
        private DateTime dt_mbarimi;
        private DateTime dt_krijimi;
        private DateTime dt_modifikimi;
        private int statusi;
        #endregion

        #region Properties
        public int NJOFTIMEID
        {
            get { return njoftimeid; }
            set { njoftimeid = value; }
        }

        public string MESAZH_TITULLI
        {
            get { return titulli; }
            set { titulli = value; }
        }

        public string MESAZH_PERMBAJTJA
        {
            get { return permbajtja; }
            set { permbajtja = value; }
        }
        public DateTime DATE_FILLIMI
        {
            get { return dt_fillimi; }
            set { dt_fillimi = value; }
        }

        public DateTime DATE_MBARIMI
        {
            get { return dt_mbarimi; }
            set { dt_mbarimi = value; }
        }
        public DateTime DATE_KRIJIMI
        {
            get { return dt_krijimi; }
            set { dt_krijimi = value; }
        }
        public DateTime DATE_MODIFIKIMI
        {
            get { return dt_modifikimi; }
            set { dt_modifikimi = value; }
        }

        public int STATUSI
        {
            get { return statusi; }
            set { statusi = value; }
        }

        #endregion

        #region Konstruktoret
        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsNjoftime()
        {
        }

        public clsNjoftime(IDataRecord record)
        {
            Mbush(record);
        }

        #endregion

        #region MetodatPublike
        public static DateTime MerrDateFunditNJoftime()
        {
            using (clsDatabaseShare dbshare = new clsDatabaseShare())
            {
                return dbshare.ktheDateFunditNjoftimi();
            }
        }

        public clsMesazh Ruaj()
        {
            try
            {
                using (clsDatabaseShare data = new clsDatabaseShare())
                {
                    njoftimeid = 0;
                    clsMesazh u_ruajt = data.ruajNjoftim(out njoftimeid, titulli, permbajtja, dt_fillimi, dt_mbarimi, dt_krijimi, dt_modifikimi, statusi);
                    return u_ruajt;
                }
            }
            catch (Exception ex)
            {
                ImbLogger.Error(ex);
                return new MesazhGabimi($"Njoftimi me titull {titulli} nuk u ruajt!");
            }
        }

        public clsMesazh Modifiko()
        {
            throw new NotImplementedException();
        }

        public clsMesazh Fshi()
        {
            throw new NotImplementedException();
        }

        public void Mbush(IDataRecord record)
        {
            njoftimeid = Convert.ToInt32(record["NJOFTIMEID"].ToString());
            titulli = record["TITULLI"].ToString();
            permbajtja = record["PERMBAJTJA"].ToString();
            dt_fillimi = Convert.ToDateTime(record["DATE_FILLIMI"]);
            dt_mbarimi = Convert.ToDateTime(record["DATE_FILLIMI"]);
            dt_krijimi = Convert.ToDateTime(record["DATE_FILLIMI"]);
            dt_modifikimi = Convert.ToDateTime(record["DATE_FILLIMI"]);
            statusi = Convert.ToInt32(record["STATUSI"].ToString());
        }

        #endregion
    }

}
