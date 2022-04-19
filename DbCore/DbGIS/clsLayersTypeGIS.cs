using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;

using DbCore.DbGIS;

namespace DbCore.DbGIS
{
    public class clsLayersTypeGIS
    {
        #region Atribute

        private int varIDLAYERSTYPE;
        private string varKODI;
        private string varPERSHKRIMI;
        private string varWEBCONN;
        private string varWEBLLOJI;
        private Boolean varGRUPOHET;
        private int varWEBLLOJID;
        private string varWEBLLOJPERSHKRIMI;

        #endregion

        #region Konstruktoret
        public clsLayersTypeGIS()
        {
        }

        public clsLayersTypeGIS(DataRow db)
        {
            mbushLayersTypeGIS(db);
        }

        public clsLayersTypeGIS(int idLayersType, string kodi, string pershkrimi, string webConn, string webLloji, Boolean grupohet, int webLlojiId, string webLlojiPershkrim)
        {
            this.IDLAYERSTYPE = idLayersType;
            this.KODI = kodi;
            this.PERSHKRIMI = pershkrimi;
            this.WEBCONN = webConn;
            this.WEBLLOJI = webLloji;
            this.GRUPOHET = grupohet;
            this.WEBLLOJID = webLlojiId;
            this.WEBLLOJPERSHKRIMI = webLlojiPershkrim;
        }

        #endregion

        #region Properties

        public int IDLAYERSTYPE
        {
            get { return varIDLAYERSTYPE; }
            set { varIDLAYERSTYPE = value; }
        }
        public string KODI
        {
            get { return varKODI; }
            set { varKODI = value; }
        }
        public string PERSHKRIMI
        {
            get { return varPERSHKRIMI; }
            set { varPERSHKRIMI = value; }
        }
        public string WEBCONN
        {
            get { return varWEBCONN; }
            set { varWEBCONN = value; }
        }
        public string WEBLLOJI
        {
            get { return varWEBLLOJI; }
            set { varWEBLLOJI = value; }
        }
        public Boolean GRUPOHET
        {
            get { return varGRUPOHET; }
            set { varGRUPOHET = value; }
        }
        public int WEBLLOJID
        {
            get { return varWEBLLOJID; }
            set { varWEBLLOJID = value; }
        }
        public string WEBLLOJPERSHKRIMI
        {
            get { return varWEBLLOJPERSHKRIMI; }
            set { varWEBLLOJPERSHKRIMI = value; }
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// Mbush objektin e Layer Type sipas tipit te layerit qe kerkohet
        /// </summary>
        /// <param name="data">Connection me db</param>
        /// <param name="idLayerType">Id e tipit qe kthen vlerat e tjera</param>
        /// <returns>Kthen objektin e workspace</returns>
        public clsMesazh merrLayersTypeSipasId(clsDatabaseGIS data, int idLayerType)
        {
            clsMesazh mesazhi = new clsMesazh();
            mesazhi.Status = mbushLayersTypeGIS(data.merrLayersTypeSipasId(idLayerType));
            if (mesazhi.Status)
                mesazhi.PershkrimMesazhi = "Mbushja e LayerType sipas id:" + idLayerType + " u krye me sukses";
            else
                mesazhi.PershkrimMesazhi = "Nuk ekziston LayerType per id:" + idLayerType;
            return mesazhi;
        }

        #endregion

        #region Metoda Internal

        internal bool mbushLayersTypeGIS(DataRow db)
        {
            if (db != null)
            {
                try
                {
                    int.TryParse(db["IDLAYERSTYPE"].ToString(), out varIDLAYERSTYPE);
                    varKODI = db["KODI"].ToString();
                    varPERSHKRIMI = db["PERSHKRIMI"].ToString();
                    varWEBCONN = db["WEBCONN"].ToString();
                    varWEBLLOJI = db["WEBLLOJI"].ToString();
                    varGRUPOHET= Convert.ToBoolean(db["GRUPOHET"].ToString());
                    int.TryParse(db["WEBLLOJID"].ToString(), out varWEBLLOJID);
                    varWEBLLOJPERSHKRIMI = db["WEBLLOJPERSHKRIMI"].ToString();

                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se tipeve te Layerave nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}
