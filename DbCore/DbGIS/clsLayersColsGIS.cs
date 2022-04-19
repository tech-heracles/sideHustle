using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;

using DbCore.DbGIS;

namespace DbCore.DbGIS
{
    public class clsLayersColsGIS
    {
        #region Atribute

        private int     varIDLAYERSCOLS;
        private string  varIDENTIFICATION;
        private int     varIDLAYER;
        private int     varORDERBY;
        private string  varDESCRIPTIONAL;
        private string  varDESCRIPTIONEN;
        private string  varDESCRIPTIONIT;

        private string  varDESCRIPTION;
        private string  varObjColSchemaIsNull;
        private string  varObjColSchemaDataType;
        private string  varObjType;
        private bool    varObjVisible;
        private bool    varObjVisibleInsert;

        private String  vlereDefault;        
        private bool    enabled;
        private bool    detyrueshme;
        private int     idTipiKontrollit;
        private String  vlereDefaultEng;
        private bool    enabledInsert;

        #endregion

        #region Konstruktoret

        public clsLayersColsGIS()
        {
        }

        public clsLayersColsGIS(DataRow db)
        {
            mbushLayersCols(db);
        }

        //Kontruktori per tabelen e plote te LayersCols
        public clsLayersColsGIS(int idKolona, string emerKolona, int iDLAYER, int ORDERBY, string emertimSq, string emertimEn, string emertimIt, string emertim, String vlereDefault, bool enabled, int rreshti, int kolona, bool detyrueshme, int idTipiKontrollit, String vlereDefaultEng, bool visibleInsert)
        {
            this.IdKolona = idKolona;
            this.EmerKolona = emerKolona;
            this.IDLAYER = iDLAYER;
            this.ORDERBY = ORDERBY;
            this.DESCRIPTIONAL = emertimSq;
            this.DESCRIPTIONEN = emertimEn;
            this.DESCRIPTIONIT = emertimIt;
            this.EmertimPerkthyer = emertim;
            this.vlereDefault = vlereDefault;
            this.enabled = enabled;
            this.detyrueshme = detyrueshme;
            this.idTipiKontrollit = idTipiKontrollit;
            this.vlereDefaultEng = vlereDefaultEng;
            this.varObjVisibleInsert = visibleInsert;
        }

        //Kontruktori sipas perzgjedhjes ishte KoloneLayerElement
        public clsLayersColsGIS(int idKolona, string emerKolona, string emertim, string colSchemaIsNull, string colSchemaDataType, string type, bool visible, String vlereDefault, bool enabled, int rreshti, int kolona, bool detyrueshme, int idTipiKontrollit, String vlereDefaultEng, bool visibleInsert)
        {
            this.IdKolona = idKolona;
            this.EmerKolona = emerKolona;
            this.EmertimPerkthyer = emertim;
            this.ObjColSchemaIsNull = colSchemaIsNull;
            this.ObjColSchemaDataType = colSchemaDataType;
            this.ObjType = type;
            this.ObjVisible = visible;
            this.vlereDefault = vlereDefault;
            this.enabled = enabled;
            this.detyrueshme = detyrueshme;
            this.idTipiKontrollit = idTipiKontrollit;
            this.vlereDefaultEng = vlereDefaultEng;
            this.varObjVisibleInsert = visibleInsert;
        }

        #endregion

        #region Properties

        public int IdKolona
        {
            get { return varIDLAYERSCOLS; }
            set { varIDLAYERSCOLS = value; }
        }

        public string EmerKolona
        {
            get { return varIDENTIFICATION; }
            set { varIDENTIFICATION = value; }
        }

        public int IDLAYER
        {
            get { return varIDLAYER; }
            set { varIDLAYER = value; }
        }

        public int ORDERBY
        {
            get { return varORDERBY; }
            set { varORDERBY = value; }
        }

        public string DESCRIPTIONAL
        {
            get { return varDESCRIPTIONAL; }
            set { varDESCRIPTIONAL = value; }
        }

        public string DESCRIPTIONEN
        {
            get { return varDESCRIPTIONEN; }
            set { varDESCRIPTIONEN = value; }
        }

        public string DESCRIPTIONIT
        {
            get { return varDESCRIPTIONIT; }
            set { varDESCRIPTIONIT = value; }
        }

        public string EmertimPerkthyer
        {
            get { return varDESCRIPTION; }
            set { varDESCRIPTION = value; }
        }

        public string ObjColSchemaIsNull
        {
            get { return varObjColSchemaIsNull; }
            set { varObjColSchemaIsNull = value; }
        }

        public string ObjColSchemaDataType
        {
            get { return varObjColSchemaDataType; }
            set { varObjColSchemaDataType = value; }
        }

        public string ObjType
        {
            get { return varObjType; }
            set { varObjType = value; }
        }

        public bool ObjVisible
        {
            get { return varObjVisible; }
            set { varObjVisible = value; }
        }

        public bool ObjVisibleInsert
        {
            get { return varObjVisibleInsert; }
            set { varObjVisibleInsert = value; }
        }

        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }

        public bool EnabledInsert
        {
            get { return enabledInsert; }
            set { enabledInsert = value; }
        }

        public bool Detyrueshme
        {
            get { return detyrueshme; }
            set { detyrueshme = value; }
        }

        public String VlereDefault
        {
            get { return vlereDefault; }
            set { vlereDefault = value; }
        }

        public String VlereDefaultEng
        {
            get { return vlereDefaultEng; }
            set { vlereDefaultEng = value; }
        }

        public int IdTipiKontrollit
        {
            get { return idTipiKontrollit; }
            set { idTipiKontrollit = value; }
        }

        #endregion

        #region Metoda Publike
        #endregion

        #region Metoda Internal
        internal bool mbushLayersCols(DataRow db)
        {
            if (db != null)
            {
                try
                {
                    int.TryParse(db["IDLAYERSCOLS"].ToString(), out varIDLAYERSCOLS);
                    varIDENTIFICATION = db["IDENTIFICATION"].ToString();
                    int.TryParse(db["IDLAYER"].ToString(), out varIDLAYER);
                    int.TryParse(db["ORDERBY"].ToString(), out varORDERBY);
                    varDESCRIPTIONAL = db["DESCRIPTIONAL"].ToString();
                    varDESCRIPTIONEN = db["DESCRIPTIONEN"].ToString();
                    varDESCRIPTIONIT = db["DESCRIPTIONIT"].ToString();

                    varDESCRIPTION = db["DESCRIPTION"].ToString();

                    varObjColSchemaIsNull = db["IS_NULLABLE"].ToString();
                    varObjColSchemaDataType = db["DATA_TYPE"].ToString();
                    varObjType = db["TYPE"].ToString();
                    bool.TryParse(db["VISIBLE"].ToString(), out varObjVisible);
                    bool.TryParse(db["VISIBLEINSERT"].ToString(), out varObjVisibleInsert);

                    vlereDefault = db["VLEREDEFAULT"].ToString();
                    vlereDefaultEng = db["VLEREDEFAULTENG"].ToString();
                    bool.TryParse(db["ENABLED"].ToString(), out enabled);
                    bool.TryParse(db["ENABLEDINSERT"].ToString(), out enabledInsert);
                    bool.TryParse(db["DETYRUESHME"].ToString(), out detyrueshme);
                    int.TryParse(db["TIPI"].ToString(), out idTipiKontrollit); 

                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se skedareve nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}