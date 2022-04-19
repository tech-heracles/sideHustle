using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;

using DbCore.DbGIS;

namespace DbCore.DbGIS
{
    public class clsGeoProjectionsGIS
    {
        #region Atribute

        private int varIDGEOPROJECTION;
        private string varSPATIALREFID;
        private string varAUTHORITYNAME;
        private string varAUTHORITYSPATIALREFID;
        private string varDESCRIPTION;
        private string varUNIT;
        private string varPROJ4;
        private string varWELLKNOWTEXT;
        private string varTYPE;
        private string varCRS;
        private string varDATUM;
        private string varELLIPSOID;
        private string varPRIMEMERIDIAN;
        private string varREVISIONDATE;
        private string varCENTER;
        private string varBOUNDS;
        private string varAREAOFUSE;

        #endregion

        #region Konstruktoret
        public clsGeoProjectionsGIS()
        {
        }

        public clsGeoProjectionsGIS(DataRow db)
        {
            mbushGeoProjectionsGIS(db);
        }

        public clsGeoProjectionsGIS(int IDGEOPROJECTION, string SPATIALREFID, string AUTHORITYNAME, string AUTHORITYSPATIALREFID, string DESCRIPTION, string UNIT, string PROJ4, string WELLKNOWTEXT, string TYPE, string CRS, string DATUM, string ELLIPSOID, string PRIMEMERIDIAN, string REVISIONDATE, string CENTER, string BOUNDS, string AREAOFUSE)
        {
            this.IDGEOPROJECTION = IDGEOPROJECTION;
            this.SPATIALREFID = SPATIALREFID;
            this.AUTHORITYNAME = AUTHORITYNAME;
            this.AUTHORITYSPATIALREFID = AUTHORITYSPATIALREFID;
            this.DESCRIPTION = DESCRIPTION;
            this.UNIT = UNIT;
            this.PROJ4 = PROJ4;
            this.WELLKNOWTEXT = WELLKNOWTEXT;
            this.TYPE = TYPE;
            this.CRS = CRS;
            this.DATUM = DATUM;
            this.ELLIPSOID = ELLIPSOID;
            this.PRIMEMERIDIAN = PRIMEMERIDIAN;
            this.REVISIONDATE = REVISIONDATE;
            this.CENTER = CENTER; 
            this.BOUNDS = BOUNDS;
            this.AREAOFUSE = AREAOFUSE;
        }

        #endregion

        #region Properties

        public int IDGEOPROJECTION
        {
            get { return varIDGEOPROJECTION; }
            set { varIDGEOPROJECTION = value; }
        }
        public string SPATIALREFID
        {
            get { return varSPATIALREFID; }
            set { varSPATIALREFID = value; }
        }
        public string AUTHORITYNAME
        {
            get { return varAUTHORITYNAME; }
            set { varAUTHORITYNAME = value; }
        }
        public string AUTHORITYSPATIALREFID
        {
            get { return varAUTHORITYSPATIALREFID; }
            set { varAUTHORITYSPATIALREFID = value; }
        }
        public string DESCRIPTION
        {
            get { return varDESCRIPTION; }
            set { varDESCRIPTION = value; }
        }
        public string UNIT
        {
            get { return varUNIT; }
            set { varUNIT = value; }
        }
        public string PROJ4
        {
            get { return varPROJ4; }
            set { varPROJ4 = value; }
        }
        public string WELLKNOWTEXT
        {
            get { return varWELLKNOWTEXT; }
            set { varWELLKNOWTEXT = value; }
        }
        public string TYPE
        {
            get { return varTYPE; }
            set { varTYPE = value; }
        }
        public string CRS
        {
            get { return varCRS; }
            set { varCRS = value; }
        }
        public string DATUM
        {
            get { return varDATUM; }
            set { varDATUM = value; }
        }
        public string ELLIPSOID
        {
            get { return varELLIPSOID; }
            set { varELLIPSOID = value; }
        }
        public string PRIMEMERIDIAN
        {
            get { return varPRIMEMERIDIAN; }
            set { varPRIMEMERIDIAN = value; }
        }
        public string REVISIONDATE
        {
            get { return varREVISIONDATE; }
            set { varREVISIONDATE = value; }
        }
        public string CENTER
        {
            get { return varCENTER; }
            set { varCENTER = value; }
        }
        public string BOUNDS
        {
            get { return varBOUNDS; }
            set { varBOUNDS = value; }
        }
        public string AREAOFUSE
        {
            get { return varAREAOFUSE; }
            set { varAREAOFUSE = value; }
        }
        #endregion

        #region Metoda Publike
        #endregion

        #region Metoda Internal

        internal bool mbushGeoProjectionsGIS(DataRow db)
        {
            if (db != null)
            {
                try
                {
                    int.TryParse(db["IDGEOPROJECTION"].ToString(), out varIDGEOPROJECTION);
                    varSPATIALREFID = db["SPATIALREFID"].ToString();
                    varAUTHORITYNAME = db["AUTHORITYNAME"].ToString();
                    varAUTHORITYSPATIALREFID = db["AUTHORITYSPATIALREFID"].ToString();
                    varDESCRIPTION = db["DESCRIPTION"].ToString();
                    varUNIT = db["UNIT"].ToString(); 
                    varPROJ4 = db["PROJ4"].ToString();
                    varWELLKNOWTEXT = db["WELLKNOWTEXT"].ToString();
                    varTYPE = db["TYPE"].ToString(); 
                    varCRS = db["CRS"].ToString(); 
                    varDATUM = db["DATUM"].ToString();
                    varELLIPSOID = db["ELLIPSOID"].ToString(); 
                    varPRIMEMERIDIAN = db["PRIMEMERIDIAN"].ToString(); 
                    varREVISIONDATE = db["REVISIONDATE"].ToString();
                    varCENTER = db["CENTER"].ToString(); 
                    varBOUNDS = db["BOUNDS"].ToString(); 
                    varAREAOFUSE = db["AREAOFUSE"].ToString();

                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se projeksioneve nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}
