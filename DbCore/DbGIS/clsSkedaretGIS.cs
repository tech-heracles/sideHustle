using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;

using DbCore.DbGIS;

namespace DbCore.DbGIS
{
    public class clsSkedaretGIS
    {
        #region Atribute

        private int varIDSKEDARET;
        private string varLLOJI;
        private string varFILETYPE;
        private string varPATH;
        private string varFILENAME;
        private string varSHENIME;
        private int varIDSTATUSDOK;
        private DateTime varDTKRIJIMI;
        private int varIDKRIJUESI;
        private DateTime varDTMODIFIKIMI;
        private int varIDPERDORUESI;
        private string varPRAPASHTESESKEDAR;
        private string varIDDYTESORE;

        #endregion

        #region Konstruktoret

        public clsSkedaretGIS()
        {
        }

        public clsSkedaretGIS(DataRow db)
        {
            mbushSkedareGIS(db);
        }

        public clsSkedaretGIS(int IDSKEDARET, string LLOJI, string FILETYPE, string PATH, string FILENAME, string SHENIME, int IDSTATUSDOK, DateTime DTKRIJIMI, int IDKRIJUESI, DateTime DTMODIFIKIMI, int IDPERDORUESI, string PRAPASHTESESKEDAR, string IDDYTESORE)
        {
            this.IDSKEDARET = IDSKEDARET;
            this.LLOJI = LLOJI;
            this.FILETYPE = FILETYPE;
            this.PATH = PATH;
            this.FILENAME = FILENAME;
            this.SHENIME = SHENIME;
            this.IDSTATUSDOK = IDSTATUSDOK;
            this.DTKRIJIMI = DTKRIJIMI;
            this.IDKRIJUESI = IDKRIJUESI;
            this.DTMODIFIKIMI = DTMODIFIKIMI;
            this.IDPERDORUESI = IDPERDORUESI;
            this.PRAPASHTESESKEDAR = PRAPASHTESESKEDAR;
            this.IDDYTESORE = IDDYTESORE;
        }

        #endregion

        #region Properties

        public int IDSKEDARET
        {
            get { return varIDSKEDARET; }
            set { varIDSKEDARET = value; }
        }
        public string LLOJI
        {
            get { return varLLOJI; }
            set { varLLOJI = value; }
        }
        public string FILETYPE
        {
            get { return varFILETYPE; }
            set { varFILETYPE = value; }
        }
        public string PATH
        {
            get { return varPATH; }
            set { varPATH = value; }
        }
        public string FILENAME
        {
            get { return varFILENAME; }
            set { varFILENAME = value; }
        }
        public string SHENIME
        {
            get { return varSHENIME; }
            set { varSHENIME = value; }
        }
        public int IDSTATUSDOK
        {
            get { return varIDSTATUSDOK; }
            set { varIDSTATUSDOK = value; }
        }
        public DateTime DTKRIJIMI
        {
            get { return varDTKRIJIMI; }
            set { varDTKRIJIMI = value; }
        }
        public int IDKRIJUESI
        {
            get { return varIDKRIJUESI; }
            set { varIDKRIJUESI = value; }
        }
        public DateTime DTMODIFIKIMI
        {
            get { return varDTMODIFIKIMI; }
            set { varDTMODIFIKIMI = value; }
        }
        public int IDPERDORUESI
        {
            get { return varIDPERDORUESI; }
            set { varIDPERDORUESI = value; }
        }
        public string PRAPASHTESESKEDAR
        {
            get { return varPRAPASHTESESKEDAR; }
            set { varPRAPASHTESESKEDAR = value; }
        }
        public string IDDYTESORE
        {
            get { return varIDDYTESORE; }
            set { varIDDYTESORE = value; }
        }

        #endregion

        #region Metoda Publike

        public clsSkedaretGIS findSkedarByShenim(colSkedaretGIS skedaret, string fusha)
        {
            if (skedaret.Exists(x => (x.SHENIME == fusha)))
            {
                return skedaret.Find(x => (x.SHENIME == fusha));
            }
            else
            {
                return new clsSkedaretGIS();
            }
        }
        public colSkedaretGIS merrAllSkedare(int idR)
        {
            colSkedaretGIS data = new colSkedaretGIS();
            data.mbushSkedare(new clsDatabaseGIS());
            return data;
        }

        public clsMesazh ruaj()
        {
            clsDatabaseGIS data = new clsDatabaseGIS();
            data.beginTransaksion();
            clsMesazh u_ruajt = ruaj(data);
            if (u_ruajt.Status) data.commitTransaksion(); else data.rollbackTransaksion();
            return u_ruajt;
        }
        public clsMesazh ruaj(clsDatabaseGIS data)
        {
            int idAuto;
            clsMesazh u_ruajt = data.ruajSkedaret(out idAuto, this.LLOJI, this.FILETYPE, this.PATH, this.FILENAME, this.SHENIME, this.IDSTATUSDOK, this.DTKRIJIMI, this.IDKRIJUESI, this.DTMODIFIKIMI, this.IDPERDORUESI,this.PRAPASHTESESKEDAR,this.IDDYTESORE);
            this.IDSKEDARET = idAuto;
            return u_ruajt;
        }
        public clsMesazh fshi()
        {
            clsDatabaseGIS data = new clsDatabaseGIS();
            data.beginTransaksion();
            clsMesazh u_fshi = fshi(data);
            if (u_fshi.Status) data.commitTransaksion(); else data.rollbackTransaksion();
            return u_fshi;
        }
        public clsMesazh fshi(clsDatabaseGIS data)
        {
            clsMesazh u_fshi = data.updateStatusSkedarNgaId(this);
            return u_fshi;
        }

        #endregion

        #region Metoda Internal
        internal bool mbushSkedareGIS(DataRow db)
        {
            if (db != null)
            {
                try
                {
                    int.TryParse(db["IDSKEDARET"].ToString(), out varIDSKEDARET);
                    varLLOJI = db["LLOJI"].ToString();
                    varFILETYPE = db["FILETYPE"].ToString();
                    varPATH = db["PATH"].ToString();
                    varFILENAME = db["FILENAME"].ToString();
                    varSHENIME = db["SHENIME"].ToString();
                    int.TryParse(db["IDSTATUSDOK"].ToString(), out varIDSTATUSDOK);
                    DateTime.TryParse(db["DTKRIJIMI"].ToString(), out varDTKRIJIMI);
                    int.TryParse(db["IDKRIJUESI"].ToString(), out varIDKRIJUESI);
                    DateTime.TryParse(db["DTMODIFIKIMI"].ToString(), out varDTMODIFIKIMI);
                    int.TryParse(db["IDPERDORUESI"].ToString(), out varIDPERDORUESI);
                    varPRAPASHTESESKEDAR = db["PRAPASHTESESKEDAR"].ToString();
                    varIDDYTESORE = db["IDDYTESORE"].ToString();
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