using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;

using DbCore.DbGIS;

namespace DbCore.DbGIS
{
    public class clsWorkspaceGIS
    {
        #region Atribute

        private int varId_workspace;
        private string varWorkspace_name;
        private int varIDNDERMARJE;
        private string varGEOURL;
        private string varGEOUSER;
        private string varGEOPASSWORD;
        private string varDSDATABASE;
        private string varDSPORT;
        private string varDSUSER;
        private string varWSFUNCTIONS;
        private string varWSDEFAULT;
        private string varWSGUIDING;
        private string varWSPUBLIC;

        #endregion

        #region Konstruktoret

        public clsWorkspaceGIS()
        {
        }

        public clsWorkspaceGIS(DataRow db)
        {
            mbushWorkspaceGIS(db);
        }

        public clsWorkspaceGIS(int Id_workspace, string Workspace_name, int IDNDERMARJE, string GEOURL, string GEOUSER, string GEOPASSWORD, string DSDATABASE, string DSPORT, string DSUSER, string WSFUNCTIONS, string WSDEFAULT, string WSGUIDING, string WSPUBLIC)
        {
            this.Id_workspace = Id_workspace;
            this.Workspace_name = Workspace_name;
            this.IDNDERMARJE = IDNDERMARJE;
            this.GEOURL = GEOURL;
            this.GEOUSER = GEOUSER;
            this.GEOPASSWORD = GEOPASSWORD;
            this.DSDATABASE = DSDATABASE;
            this.DSPORT = DSPORT;
            this.DSUSER = DSUSER;
            this.WSFUNCTIONS = WSFUNCTIONS;
            this.WSDEFAULT = WSDEFAULT;
            this.WSGUIDING = WSGUIDING;
            this.WSPUBLIC = WSPUBLIC;
        }

        #endregion

        #region Properties
        public int Id_workspace
        {
            get { return varId_workspace; }
            set { varId_workspace = value; }
        }
        public string Workspace_name
        {
            get { return varWorkspace_name; }
            set { varWorkspace_name = value; }
        }
        public int IDNDERMARJE
        {
            get { return varIDNDERMARJE; }
            set { varIDNDERMARJE = value; }
        }
        public string GEOURL
        {
            get { return varGEOURL; }
            set { varGEOURL = value; }
        }
        public string GEOUSER
        {
            get { return varGEOUSER; }
            set { varGEOUSER = value; }
        }
        public string GEOPASSWORD
        {
            get { return varGEOPASSWORD; }
            set { varGEOPASSWORD = value; }
        }
        public string DSDATABASE
        {
            get { return varDSDATABASE; }
            set { varDSDATABASE = value; }
        }
        public string DSPORT
        {
            get { return varDSPORT; }
            set { varDSPORT = value; }
        }
        public string DSUSER
        {
            get { return varDSUSER; }
            set { varDSUSER = value; }
        }
        public string WSFUNCTIONS
        {
            get { return varWSFUNCTIONS; }
            set { varWSFUNCTIONS = value; }
        }
        public string WSDEFAULT
        {
            get { return varWSDEFAULT; }
            set { varWSDEFAULT = value; }
        }
        public string WSGUIDING
        {
            get { return varWSGUIDING; }
            set { varWSGUIDING = value; }
        }
        public string WSPUBLIC
        {
            get { return varWSPUBLIC; }
            set { varWSPUBLIC = value; }
        }
        #endregion

        #region Metoda Public

        /// <summary>
        /// kthe nje datarow me workspace e marre nga db sipas id se ndermarrjes
        /// </summary>
        /// <param name="idnderm">Id e ndermarrjes</param>
        /// <returns>datarow</returns>
        public static DataRow ktheWorkspaceSipasId(int idnderm)
        {
            DataRow dr;
            using (clsDatabaseGIS dbGIS = new clsDatabaseGIS())
            {
                dr = dbGIS.merrWorkspaceSipasNdermarrjesId(idnderm);
            }
            return dr;
        }

        internal bool mbushWorkspaceGIS(DataRow db)
        {
            if (db != null)
            {
                try
                {
                    int.TryParse(db["Id_workspace"].ToString(), out varId_workspace);
                    varWorkspace_name = db["Workspace_name"].ToString();
                    int.TryParse(db["IDNDERMARJE"].ToString(), out varIDNDERMARJE);
                    varGEOURL = db["GEOURL"].ToString();
                    varGEOUSER = db["GEOUSER"].ToString();
                    varGEOPASSWORD = db["GEOPASSWORD"].ToString();
                    varDSDATABASE = db["DSDATABASE"].ToString();
                    varDSPORT = db["DSPORT"].ToString();
                    varDSUSER = db["DSUSER"].ToString();
                    varWSFUNCTIONS = db["WSFUNCTIONS"].ToString();
                    varWSDEFAULT = db["WSDEFAULT"].ToString();
                    varWSGUIDING = db["WSGUIDING"].ToString();
                    varWSPUBLIC = db["WSPUBLIC"].ToString();
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se workspace nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// Mbush objektin e workspace qe eshte e lidhur me ate ndermarrje
        /// </summary>
        /// <param name="idnderm">Id e ndermarrjes</param>
        /// <returns>Kthen objektin e workspace</returns>
        public clsMesazh mbushWorkspaceSipasNdermarrjesId(int idnderm)
        {
            clsMesazh mesazhi = new clsMesazh();
            using (clsDatabaseGIS data = new clsDatabaseGIS())
            {
                mbushWorkspaceSipasNdermarrjesId(data, idnderm);
            }
            return mesazhi;
        }

        /// <summary>
        /// Mbush objektin e workspace qe eshte e lidhur me ate ndermarrje
        /// </summary>
        /// <param name="data">Connection me db</param>
        /// <param name="idnderm">Id e ndermarrjes</param>
        /// <returns>Kthen objektin e workspace</returns>
        public clsMesazh mbushWorkspaceSipasNdermarrjesId(clsDatabaseGIS data, int idnderm)
        {
            clsMesazh mesazhi = new clsMesazh();
            mesazhi.Status = mbushWorkspaceGIS(data.merrWorkspaceSipasNdermarrjesId(idnderm));
            if (mesazhi.Status)
                mesazhi.PershkrimMesazhi = "Mbushja e Workspace sipas idndermarrjes:" + idnderm + " u krye me sukses";
            else
                mesazhi.PershkrimMesazhi = "Nuk ekziston Workspace per idndermarrje:" + idnderm;
            return mesazhi;
        }

        /// <summary>
        /// Ruan nje Workspace te ri ne SQL server ne tablen  T_GIS_App_WORKSPACE
        /// </summary>
        /// <param name="dbGIS">Connection me db</param>
        /// <returns>Kthen mesazh suksesi ose jo</returns>
        public clsMesazh ruajWorkspaceGIS(clsDatabaseGIS dbGIS)
        {
            clsMesazh u_ruajt = new clsMesazh();
            u_ruajt = dbGIS.ruajWorkspaceGIS(this);
            return u_ruajt;
        }

        /// <summary>
        /// Ruan ndryshimet e nje Workspace ne SQL server ne tablen  T_GIS_App_WORKSPACE
        /// </summary>
        /// <param name="dbGIS">Connection me db</param>
        /// <returns>Kthen mesazh suksesi ose jo</returns>
        public clsMesazh modifikoWorkspaceGIS(clsDatabaseGIS dbGIS)
        {
            clsMesazh u_ruajt = new clsMesazh();
            u_ruajt = dbGIS.modifikoWorkspaceGIS(this);
            return u_ruajt;
        }

        /// <summary>
        /// Fshin nje Workspace ne SQL server ne tablen  T_GIS_App_WORKSPACE
        /// </summary>
        /// <param name="dbGIS">Connection me db</param>
        /// <returns>Kthen mesazh suksesi ose jo</returns>
        public clsMesazh fshiWorkspaceGIS(clsDatabaseGIS dbGIS)
        {
            clsMesazh u_fshi = new clsMesazh();
            u_fshi = dbGIS.fshiWorkspaceGIS(this);
            return u_fshi;
        }

        /// <summary>
        /// Ruan nje te dhenat default te lidhura me nje Workspace te ri ne SQL server ne tablen  T_GIS_App_LAYERSBASEXWORKSPACE
        /// </summary>
        /// <param name="dbGIS">Connection me db</param>
        /// <returns>Kthen mesazh suksesi ose jo</returns>
        public clsMesazh ruajBaseLayerXWorkspaceGIS(clsDatabaseGIS dbGIS)
        {
            clsMesazh u_ruajt = new clsMesazh();
            u_ruajt = dbGIS.ruajBaseLayerXWorkspaceGIS(this.Id_workspace);
            return u_ruajt;
        }

        /// <summary>
        /// Fshin te dhenat default te lidhura me nje Workspace qe do te fshihet: Nee SQL server ne tablen  T_GIS_App_LAYERSBASEXWORKSPACE
        /// </summary>
        /// <param name="dbGIS">Connection me db</param>
        /// <returns>Kthen mesazh suksesi ose jo</returns>
        public clsMesazh fshiBaseLayerXWorkspaceGIS(clsDatabaseGIS dbGIS)
        {
            clsMesazh u_fshi = new clsMesazh();
            u_fshi = dbGIS.fshiBaseLayerXWorkspaceGIS(this.Id_workspace);
            return u_fshi;
        }
        #endregion
    }
}
