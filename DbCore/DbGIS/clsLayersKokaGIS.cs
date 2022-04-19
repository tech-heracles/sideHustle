using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;

using DbCore.DbGIS;

namespace DbCore.DbGIS
{
    public class clsLayersKokaGIS
    {
        #region Atribute

        private int varIDLAYER;
        private int varIDGRSTRUCTURE;
        private string varIDGROPENLAYERS;
        private int varIDGRMAPTREE;
        private int varIDLAYERSTYPE;
        private int varNRSTATUSI;
        private string varIDENTIFICATION;
        private string varDESCRIPTIONAL;
        private string varDESCRIPTIONEN;
        private Boolean varWITHRIGHTS;
        private Boolean varWITHRIGHTSASLAYER;
        private Boolean varWITHAUTHORIZATION;
        private Boolean varDISPLAYONMAP;
        private Boolean varDISPLAYLEGEND;
        private Boolean varCHANGESTYLE;
        private string varCONNECTURL;
        private Boolean varWITHFISCALYEAR;
        #endregion

        #region Konstruktoret
        public clsLayersKokaGIS()
        {
        }

        public clsLayersKokaGIS(DataRow db)
        {
            mbushLayersBaseGIS(db);
        }

        public clsLayersKokaGIS(int iDLAYER, int iDGRSTRUCTURE, string iDGROPENLAYERS, int iDGRMAPTREE, int iDLAYERSTYPE, int nRSTATUSI, string iDENTIFICATION, string dESCRIPTIONAL, string dESCRIPTIONEN, Boolean wITHRIGHTS, Boolean wITHRIGHTSASLAYER, Boolean wITHAUTHORIZATION, Boolean dISPLAYONMAP, Boolean dISPLAYLEGEND, Boolean cHANGESTYLE, string cONNECTURL, Boolean wITHFISCALYEAR)
        {
            this.IDLAYER = iDLAYER;
            this.IDGRSTRUCTURE = iDGRSTRUCTURE;
            this.IDGROPENLAYERS = iDGROPENLAYERS;
            this.IDGRMAPTREE = iDGRMAPTREE;
            this.IDLAYERSTYPE = iDLAYERSTYPE;
            this.NRSTATUSI = nRSTATUSI;
            this.IDENTIFICATION = iDENTIFICATION;
            this.DESCRIPTIONAL = dESCRIPTIONAL;
            this.DESCRIPTIONEN = dESCRIPTIONEN;
            this.WITHRIGHTS = wITHRIGHTS;
            this.WITHRIGHTSASLAYER = wITHRIGHTSASLAYER;
            this.WITHAUTHORIZATION = wITHAUTHORIZATION;
            this.DISPLAYONMAP = dISPLAYONMAP;
            this.DISPLAYLEGEND = dISPLAYLEGEND;
            this.CHANGESTYLE = cHANGESTYLE;
            this.CONNECTURL = cONNECTURL;
            this.WITHFISCALYEAR = wITHFISCALYEAR;
        }

        #endregion

        #region Properties


        public int IDLAYER
        {
            get { return varIDLAYER; }
            set { varIDLAYER = value; }
        }
        public int IDGRSTRUCTURE
        {
            get { return varIDGRSTRUCTURE; }
            set { varIDGRSTRUCTURE = value; }
        }
        public string IDGROPENLAYERS
        {
            get { return varIDGROPENLAYERS; }
            set { varIDGROPENLAYERS = value; }
        }
        public int IDGRMAPTREE
        {
            get { return varIDGRMAPTREE; }
            set { varIDGRMAPTREE = value; }
        }
        public int IDLAYERSTYPE
        {
            get { return varIDLAYERSTYPE; }
            set { varIDLAYERSTYPE = value; }
        }
        public int NRSTATUSI
        {
            get { return varNRSTATUSI; }
            set { varNRSTATUSI = value; }
        }
        public string IDENTIFICATION
        {
            get { return varIDENTIFICATION; }
            set { varIDENTIFICATION = value; }
        }
        public string DESCRIPTIONAL
        {
            get { return varDESCRIPTIONAL; }
            set { DESCRIPTIONAL = value; }
        }
        public string DESCRIPTIONEN
        {
            get { return varDESCRIPTIONEN; }
            set { DESCRIPTIONEN = value; }
        }
        public Boolean WITHRIGHTS
        {
            get { return varWITHRIGHTS; }
            set { varWITHRIGHTS = value; }
        }
        public Boolean WITHRIGHTSASLAYER
        {
            get { return varWITHRIGHTSASLAYER; }
            set { varWITHRIGHTSASLAYER = value; }
        }
        public Boolean WITHAUTHORIZATION
        {
            get { return varWITHAUTHORIZATION; }
            set { varWITHAUTHORIZATION = value; }
        }
        public Boolean DISPLAYONMAP
        {
            get { return varDISPLAYONMAP; }
            set { varDISPLAYONMAP = value; }
        }
        public Boolean DISPLAYLEGEND
        {
            get { return varDISPLAYLEGEND; }
            set { varDISPLAYLEGEND = value; }
        }
        public Boolean CHANGESTYLE
        {
            get { return varCHANGESTYLE; }
            set { varCHANGESTYLE = value; }
        }
        public string CONNECTURL
        {
            get { return varCONNECTURL; }
            set { varCONNECTURL = value; }
        }
        public Boolean WITHFISCALYEAR
        {
            get { return varWITHFISCALYEAR; }
            set { varWITHFISCALYEAR = value; }
        }

        #endregion

        #region Metoda Publike

        public bool mbushObjektLayerKerkimiSipasGid(int idnderviti, Int32 gid, int gjuhaId)
        {
            using (clsDatabaseGIS db = new clsDatabaseGIS())
            {
                return mbushLayersBaseGIS(db.mbushObjektLayerKerkimiSipasGid(idnderviti, gid, gjuhaId));
            }
        }
        #endregion

        #region Metoda Internal

        internal bool mbushLayersBaseGIS(DataRow db)
        {
            if (db != null)
            {
                try
                {
                    int.TryParse(db["IDLAYER"].ToString(), out varIDLAYER);
                    int.TryParse(db["IDGRSTRUCTURE"].ToString(), out varIDGRSTRUCTURE);
                    varIDGROPENLAYERS = db["IDGROPENLAYERS"].ToString();
                    int.TryParse(db["IDGRMAPTREE"].ToString(), out varIDGRMAPTREE);
                    int.TryParse(db["IDLAYERSTYPE"].ToString(), out varIDLAYERSTYPE);
                    int.TryParse(db["NRSTATUSI"].ToString(), out varNRSTATUSI);
                    varIDENTIFICATION = db["IDENTIFICATION"].ToString();
                    varDESCRIPTIONAL = db["DESCRIPTIONAL"].ToString();
                    varDESCRIPTIONEN = db["DESCRIPTIONEN"].ToString();
                    varWITHRIGHTS = Convert.ToBoolean(db["WITHRIGHTS"].ToString());
                    varWITHRIGHTSASLAYER = Convert.ToBoolean(db["WITHRIGHTSASLAYER"].ToString());
                    varWITHAUTHORIZATION = Convert.ToBoolean(db["WITHAUTHORIZATION"].ToString());
                    varDISPLAYONMAP = Convert.ToBoolean(db["DISPLAYONMAP"].ToString());
                    varDISPLAYLEGEND = Convert.ToBoolean(db["DISPLAYLEGEND"].ToString());
                    varCHANGESTYLE = Convert.ToBoolean(db["CHANGESTYLE"].ToString());
                    varCONNECTURL = db["CONNECTURL"].ToString();
                    varWITHFISCALYEAR = Convert.ToBoolean(db["WITHFISCALYEAR"].ToString());

                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se layerave bazee nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}
