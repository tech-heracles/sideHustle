using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;

using DbCore.DbGIS;

namespace DbCore.DbGIS
{
    public class clsLayersTrupiGIS
    {
        #region Atribute

        private int varIDLAYERSTRUPI;
        private int varIDLAYER;
        private string varPARAMETERS;
        private string varBASEKEY;
        private string varBASEVALUE;
        private int varIDLLOJFUSHE;

        #endregion

        #region Konstruktoret
        public clsLayersTrupiGIS()
        {
        }

        public clsLayersTrupiGIS(DataRow db)
        {
            mbushLayersTrupiGIS(db);
        }

        public clsLayersTrupiGIS(int IDLAYERSTRUPI, int IDLAYER, string PARAMETERS, string BASEKEY, string BASEVALUE, int IDLLOJFUSHE)
        {
            this.IDLAYERSTRUPI = IDLAYERSTRUPI;
            this.IDLAYER = IDLAYER;
            this.PARAMETERS = PARAMETERS;
            this.BASEKEY = BASEKEY;
            this.BASEVALUE = BASEVALUE;
            this.IDLLOJFUSHE = IDLLOJFUSHE;
        }

        #endregion

        #region Properties

        public int IDLAYERSTRUPI
        {
            get { return varIDLAYERSTRUPI; }
            set { varIDLAYERSTRUPI = value; }
        }
        public int IDLAYER
        {
            get { return varIDLAYER; }
            set { varIDLAYER = value; }
        }
        public string PARAMETERS
        {
            get { return varPARAMETERS; }
            set { varPARAMETERS = value; }
        }
        public string BASEKEY
        {
            get { return varBASEKEY; }
            set { varBASEKEY = value; }
        }
        public string BASEVALUE
        {
            get { return varBASEVALUE; }
            set { varBASEVALUE = value; }
        }
        public int IDLLOJFUSHE
        {
            get { return varIDLLOJFUSHE; }
            set { varIDLLOJFUSHE = value; }
        }

        #endregion

        #region Metoda Publike
        #endregion

        #region Metoda Internal

        internal bool mbushLayersTrupiGIS(DataRow db)
        {
            if (db != null)
            {
                try
                {
                    int.TryParse(db["IDLAYERSTRUPI"].ToString(), out varIDLAYERSTRUPI);
                    int.TryParse(db["IDLAYER"].ToString(), out varIDLAYER);
                    varPARAMETERS = db["PARAMETERS"].ToString();
                    varBASEKEY = db["BASEKEY"].ToString();
                    varBASEVALUE = db["BASEVALUE"].ToString();
                    int.TryParse(db["IDLLOJFUSHE"].ToString(), out varIDLLOJFUSHE);

                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se layerave trupi nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}
