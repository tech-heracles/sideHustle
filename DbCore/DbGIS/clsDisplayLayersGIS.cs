using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;

using DbCore.DbGIS;

namespace DbCore.DbGIS
{
    public class clsDisplayLayersGIS
    {
        #region Atribute

        private int varIDLAYER;
        private int varIDGRSTRUCTURE;
        private string varIDGROPENLAYERS;
        private int varIDLAYERSTYPE;
        private int varNRSTATUSI;
        private string varIDENTIFICATION;
        private string varDESCRIPTION;
        private Boolean varWITHAUTHORIZATION;
        private Boolean varDISPLAYONMAP;
        private Boolean varDISPLAYLEGEND;
        private Boolean varCHANGESTYLE;
        private string varCONNECTURL;
        private string varGEOMETRYLAYER;
        private Boolean varWITHFISCALYEAR;

        // PER PEMEN
        private int varIDGRMAPTREE;
        private string varTREEPATH;

        // PER STILET
        private string varSTYLEIDENTIFICATION;
        private Int32 varMINSCALE;
        private Int32 varMAXSCALE;
        private string varEXTENTLAYER;

        // PER TE DREJTAT
        private Boolean varD_AMB;
        private Boolean varD_SHTIM;
        private Boolean varD_MOD;
        private Boolean varD_FSH;
        private Boolean varD_KERKO;
        private Boolean varD_EKSPORTO;
        private Boolean varD_PRINTO;

        // PER PARAMETRAT
        private colLayersTrupiGIS varLayersTrupiGIS;
        private colLayersColsGIS varLayersKolonaGIS;

        #endregion

        #region Konstruktoret
        public clsDisplayLayersGIS()
        {
        }

        public clsDisplayLayersGIS(DataRow db)
        {
            mbushDisplayLayersGIS(db);
        }

        public clsDisplayLayersGIS (DataRow db, colLayersTrupiGIS tempTrupi, colLayersColsGIS tempKolona)
        {
            mbushDisplayLayersMeTrashigim(db, tempTrupi, tempKolona);
        }

        public clsDisplayLayersGIS(int IDLAYER, int IDGRSTRUCTURE, string IDGROPENLAYERS, int IDLAYERSTYPE, int NRSTATUSI, string IDENTIFICATION, string DESCRIPTION, Boolean WITHAUTHORIZATION, Boolean DISPLAYONMAP, Boolean DISPLAYLEGEND, Boolean CHANGESTYLE, string CONNECTURL, string GEOMETRYLAYER,
            int IDGRMAPTREE, string TREEPATH, string STYLEIDENTIFICATION, Int32 MINSCALE, Int32 MAXSCALE, string EXTENTLAYER,
            Boolean D_AMB, Boolean D_SHTIM, Boolean D_MOD, Boolean D_FSH, Boolean D_KERKO, Boolean D_EKSPORTO, Boolean D_PRINTO,  Boolean WITHFISCALYEAR)
        {
            this.IDLAYER = IDLAYER;
            this.IDGRSTRUCTURE = IDGRSTRUCTURE;
            this.IDGROPENLAYERS = IDGROPENLAYERS;
            this.IDLAYERSTYPE = IDLAYERSTYPE;
            this.NRSTATUSI = NRSTATUSI;
            this.IDENTIFICATION = IDENTIFICATION;
            this.DESCRIPTION = DESCRIPTION;
            this.WITHAUTHORIZATION = DISPLAYONMAP;
            this.DISPLAYONMAP = DISPLAYONMAP;
            this.DISPLAYLEGEND = DISPLAYLEGEND;
            this.CHANGESTYLE = CHANGESTYLE;
            this.CONNECTURL = CONNECTURL;
            this.GEOMETRYLAYER = GEOMETRYLAYER;
            this.WITHFISCALYEAR = WITHFISCALYEAR;

            this.IDGRMAPTREE = IDGRMAPTREE;
            this.TREEPATH = TREEPATH;

            this.STYLEIDENTIFICATION = STYLEIDENTIFICATION;
            this.MINSCALE = MINSCALE;
            this.MAXSCALE = MAXSCALE;

            this.D_AMB = D_AMB;
            this.D_SHTIM = D_SHTIM;
            this.D_MOD = D_MOD;
            this.D_FSH = D_FSH;
            this.D_KERKO = D_KERKO;
            this.D_EKSPORTO = D_EKSPORTO;
            this.D_PRINTO = D_PRINTO;
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
        public string DESCRIPTION
        {
            get { return varDESCRIPTION; }
            set { varDESCRIPTION = value; }
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

        public string GEOMETRYLAYER
        {
            get { return varGEOMETRYLAYER; }
            set { varGEOMETRYLAYER = value; }
        }
        public Boolean WITHFISCALYEAR
        {
            get { return varWITHFISCALYEAR; }
            set { varWITHFISCALYEAR = value; }
        }
        public int IDGRMAPTREE
        {
            get { return varIDGRMAPTREE; }
            set { varIDGRMAPTREE = value; }
        }
        public string TREEPATH
        {
            get { return varTREEPATH; }
            set { varTREEPATH = value; }
        }

        public string STYLEIDENTIFICATION
        {
            get { return varSTYLEIDENTIFICATION; }
            set { varSTYLEIDENTIFICATION = value; }
        }
        public Int32 MINSCALE
        {
            get { return varMINSCALE; }
            set { varMINSCALE = value; }
        }
        public Int32 MAXSCALE
        {
            get { return varMAXSCALE; }
            set { varMAXSCALE = value; }
        }

        public string EXTENTLAYER
        {
            get { return varEXTENTLAYER; }
            set { varEXTENTLAYER = value; }
        }

        public Boolean D_AMB
        {
            get { return varD_AMB; }
            set { varD_AMB = value; }
        }
        public Boolean D_SHTIM
        {
            get { return varD_SHTIM; }
            set { varD_SHTIM = value; }
        }
        public Boolean D_MOD
        {
            get { return varD_MOD; }
            set { varD_MOD = value; }
        }
        public Boolean D_FSH
        {
            get { return varD_FSH; }
            set { varD_FSH = value; }
        }
        public Boolean D_KERKO
        {
            get { return varD_KERKO; }
            set { varD_KERKO = value; }
        }        
        public Boolean D_EKSPORTO
        {
            get { return varD_EKSPORTO; }
            set { varD_EKSPORTO = value; }
        }
        public Boolean D_PRINTO
        {
            get { return varD_PRINTO; }
            set { varD_PRINTO = value; }
        }

        public colLayersTrupiGIS LAYERSTRUPIGIS
        {
            get { return varLayersTrupiGIS; }
            set { varLayersTrupiGIS = value; }
        }        
        public colLayersColsGIS LAYERSKOLONAGIS
        {
            get { return varLayersKolonaGIS; }
            set { varLayersKolonaGIS = value; }
        }
        #endregion

        #region Metoda Publike
        #endregion

        #region Metoda Internal

        internal bool mbushDisplayLayersGIS(DataRow db)
        {
            mbushDisplayLayersPaTrup(db);
            return true;
        }

        internal bool mbushDisplayLayersMeTrashigim(DataRow db, colLayersTrupiGIS tempTrupi, colLayersColsGIS tempKolona)
        {
            mbushDisplayLayersPaTrup(db);
            varLayersTrupiGIS = tempTrupi;
            varLayersKolonaGIS = tempKolona;
            return true;
        }

        internal bool mbushDisplayLayersPaTrup(DataRow db)
        {
            if (db != null)
            {
                try
                {
                    int.TryParse(db["IDLAYER"].ToString(), out varIDLAYER);
                    int.TryParse(db["IDGRSTRUCTURE"].ToString(), out varIDGRSTRUCTURE);
                    varIDGROPENLAYERS = db["IDGROPENLAYERS"].ToString();
                    int.TryParse(db["IDLAYERSTYPE"].ToString(), out varIDLAYERSTYPE);
                    int.TryParse(db["NRSTATUSI"].ToString(), out varNRSTATUSI);
                    varIDENTIFICATION = db["IDENTIFICATION"].ToString();
                    varDESCRIPTION = db["DESCRIPTION"].ToString();
                    varWITHAUTHORIZATION = Convert.ToBoolean(db["WITHAUTHORIZATION"].ToString());
                    varDISPLAYONMAP = Convert.ToBoolean(db["DISPLAYONMAP"].ToString());
                    varDISPLAYLEGEND = Convert.ToBoolean(db["DISPLAYLEGEND"].ToString());
                    varCHANGESTYLE = Convert.ToBoolean(db["CHANGESTYLE"].ToString());
                    varCONNECTURL = db["CONNECTURL"].ToString();
                    varGEOMETRYLAYER = db["GEOMETRYLAYER"].ToString();
                    varWITHFISCALYEAR = Convert.ToBoolean(db["WITHFISCALYEAR"].ToString());

                    int.TryParse(db["IDGRMAPTREE"].ToString(), out varIDGRMAPTREE);
                    varTREEPATH = db["TREEPATH"].ToString();

                    varSTYLEIDENTIFICATION = db["STYLEIDENTIFICATION"].ToString();
                    Int32.TryParse(db["MINSCALE"].ToString(), out varMINSCALE);
                    Int32.TryParse(db["MAXSCALE"].ToString(), out varMAXSCALE);
                    varEXTENTLAYER = db["EXTENTLAYER"].ToString();

                    varD_AMB = Convert.ToBoolean(db["D_AMB"].ToString());
                    varD_SHTIM = Convert.ToBoolean(db["D_SHTIM"].ToString());
                    varD_MOD = Convert.ToBoolean(db["D_MOD"].ToString());
                    varD_FSH = Convert.ToBoolean(db["D_FSH"].ToString());
                    varD_KERKO = Convert.ToBoolean(db["D_KERKO"].ToString());
                    varD_EKSPORTO = Convert.ToBoolean(db["D_EKSPORTO"].ToString());
                    varD_PRINTO = Convert.ToBoolean(db["D_PRINTO"].ToString());
                    
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
