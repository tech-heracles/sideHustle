using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.Text;
using System.Linq;
using System.Resources;
using System.Globalization;
using System.Configuration;
using System.Collections.Generic;

using DbCore.DbGIS;
using DbCore.DbAdmin;

namespace DbCore.DbGIS
{
    public class clsGeoserverGIS
    {
        #region GEOSERVER_SKEMAT        
        /// <summary>
        /// Krijon nje dokument XML nga nje string i dhene (response i nje webservice ose i krijuar vete)
        /// </summary>
        /// <param name="stringXmlSchema">Stringu qe do te konvertohet</param>
        /// <returns>Kthen dokumentin xml</returns>
        public static XmlDocument CreateXmlDokFromString(string stringXmlSchema)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(stringXmlSchema);

            return doc;
        }

        public static clsMesazh existsStringOnLastNodeXML(XmlDocument dok, string lastNodePath, string nodeToSearch, string stringToCompare)
        {
            clsMesazh existsString = new clsMesazh(true, "Ekziston!");

            XmlNodeList xnList = dok.SelectNodes(lastNodePath);
            foreach (XmlNode xn in xnList)
            {
                XmlNode node = xn.SelectSingleNode(nodeToSearch);
                if (node != null)
                {
                    if (stringToCompare == node.InnerText)
                    {
                        existsString.Status = false;
                        existsString.PershkrimMesazhi = "Nuk Ekziston!";
                    }
                }
            }

            return existsString;
        }
        /// <summary>
        /// Krijon skemen e nje workspace te ri ne Geoserver
        /// </summary>
        /// <param name="wsName">Emri i workspace se ri qe do te krijohet</param>
        /// <returns>Stringu i gjeneruar sipas skemes se workspace dhe kerkeses se re</returns>
        protected static string CreateWorkspaceXml(string wsName)
        {
            string fXml =
            "<workspace>" +
                "<name>" + wsName + "</name>" +
            "</workspace>";
            return fXml;
        }

        /// <summary>
        /// Krijon skemen e nje DataStore te ri ne Geoserver
        /// </summary>
        /// <param name="dsName">Emri i DataStore te ri qe do te krijohet</param>
        /// <returns>Stringu i gjeneruar sipas skemes se datastore dhe kerkeses se re</returns>
        protected static string CreateDataStoreXml(clsWorkspaceGIS workspace, string geoPassDB)
        {
            SqlConnectionStringBuilder sqlConnection = new SqlConnectionStringBuilder(ConfigurationManager.ConnectionStrings["connStringAlpha"].ConnectionString);
            string fXml =
            "<dataStore>" +
                "<name>" + workspace.Workspace_name + "</name>" +
                "<type>Microsoft SQL Server</type>" +
                "<connectionParameters>" +
                    "<entry key=\"port\">" + workspace.DSPORT + "</entry>" +
                    "<entry key=\"passwd\">" + sqlConnection.Password + "</entry>" +
                    "<entry key=\"dbtype\">sqlserver</entry>" +
                    "<entry key=\"Geometry metadata table\">T_GIS_App_GEOMETRYCOLS</entry>" +
                    "<entry key=\"Evictor tests per run\">3</entry>" +
                    "<entry key=\"host\">127.0.0.1</entry>" +
                    "<entry key=\"Use native geometry serialization\">false</entry>" +
                    "<entry key=\"Integrated Security\">false</entry>" +
                    "<entry key=\"Force spatial index usage via hints\">false</entry>" +
                    "<entry key=\"validate connections\">true</entry>" +
                    "<entry key=\"max connections\">10</entry>" +
                    "<entry key=\"database\">" + workspace.DSDATABASE + "</entry>" + //sqlConnection.InitialCatalog
                    "<entry key=\"namespace\">http://" + workspace.Workspace_name + "</entry>" +
                    "<entry key=\"Evictor run periodicity\">300</entry>" +
                    "<entry key=\"Max connection idle time\">300</entry>" +
                    "<entry key=\"schema\">dbo</entry>" +
                    "<entry key=\"Test while idle\">true</entry>" +
                    "<entry key=\"Expose primary keys\">true</entry>" +
                    "<entry key=\"Use Native Paging\">true</entry>" +
                    "<entry key=\"fetch size\">1000</entry>" +
                    "<entry key=\"user\">" + workspace.DSUSER + "</entry>" + //sqlConnection.UserID
                    "<entry key=\"min connections\">1</entry>" +
                "</connectionParameters>" +
            "</dataStore>";
            return fXml;
        }

        /// <summary>
        /// Krijon skemen e nje Feature te ri ne Geoserver
        /// </summary>
        /// <param name="featureName">Emri i View qe do te krijohet ne menyre automatike</param>
        /// <param name="projection">Projeksioni qe do te kene te dhenat</param>
        /// <param name="geomType">Tipi i te dhenave</param>
        /// <returns>Stringu i gjeneruar sipas skemes se features dhe kerkeses se re</returns>
        protected static string CreateFeatureXml(string featuresName, string projection, string geomType, string weblloji)
        {
            string fXml;
            fXml =
            "<featureType>" +
                "<name>" + featuresName + "</name>" +
                "<nativeName>" + featuresName + "</nativeName>" +
                "<title>" + featuresName + "</title>" +
                "<srs>" + projection + "</srs>" +
                createXMLBoundingBoxByProjection(projection) +
                "<attributes>" +
                    createXMLFeatureChild("gid", "1", "1", "false", "Integer") +
                    createXMLFeatureChild("the_geom", "1", "1", "true", geomType);

                    if (weblloji == "SERIALE")
                        fXml = fXml + createXMLFeatureChild("SERIALI", "0", "1", "true", "String");

                    fXml = fXml + 
                    createXMLFeatureChild("KODI", "0", "1", "true", "String") +
                    createXMLFeatureChild("PERSHKRIMI", "0", "1", "true", "String") +
                    createXMLFeatureChild("IDAUTORIZUESI", "1", "1", "true", "Integer") +
                    createXMLFeatureChild("NRSTATUSI", "1", "1", "true", "Integer") +
                "</attributes>" +
            "</featureType>";
            return fXml;
        }

        /// <summary>
        /// Krijon skemen per modifikimin e stilit default te layerit
        /// </summary>
        /// <param name="styleName">Emri i stilit qe ekziston ne geoserver dhe qe do ti lidhet Layerit</param>
        /// <param name="workspaceName">Workspace ku ndodhet Layer</param>
        /// <returns>Stringu i gjeneruar sipas skemes se modifikimit te skemes deafult</returns>
        protected static string ChangeLayerDefaultStyle(string styleName, string workspaceName)
        {
            string fXml =
            "<layer>" +
                "<defaultStyle>" +
                    "<name>" + styleName + "</name>" +
                    "<workspace>" + workspaceName + "</workspace>" +
                "</defaultStyle>" +
            "</layer>";
            return fXml;
        }

        /// <summary>
        /// Lexon tipin e te dhenes te dhene nga perdoruesi edhe e konverton ne tipet qe njihen nga geoserver
        /// </summary>
        /// <param name="type">Tipi i te dhenave qe mbart layeri LINESTRING/MULTILINESTRING/POLYGON/MULTIPOLYGON/POINT/Integer/Double etj</param>
        /// <returns>Kthen tipin qe do te perdoret ne kerkesen e geoserver</returns>
        protected static string getGeoserverJavaType(string userType)
        {
            string geomType;
            switch (userType)
            {
                case "LINESTRING":
                case "MULTILINESTRING":
                    geomType = "com.vividsolutions.jts.geom.MultiLineString";
                    break;
                case "POLYGON":
                case "MULTIPOLYGON":
                    geomType = "com.vividsolutions.jts.geom.MultiPolygon";
                    break;
                case "POINT":
                    geomType = "com.vividsolutions.jts.geom.Point";
                    break;
                case "Integer":
                    geomType = "java.lang.Integer";
                    break;
                case "String":
                    geomType = "java.lang.String";
                    break;
                default:
                    geomType = "com.vividsolutions.jts.geom.Point";
                    break;
            };
            return geomType;
        }

        /// <summary>
        /// Krijon Feature Type Details per Layerat qe do te krijohen
        /// </summary>
        /// <param name="name">Fusha "Property" ne geoserver</param>
        /// <param name="minOccurs">Vlera Min Occurences</param>
        /// <param name="maxOccurs">Vlera Max Occurences</param>
        /// <param name="nillable">Vlera e nillable ne geoserver</param>
        /// <param name="binding">Fusha "Type" ne geoserver</param>
        /// <returns>Kthen tipin qe do te perdoret ne kerkesen e geoserver</returns>
        protected static string createXMLFeatureChild(string name, string minOccurs, string maxOccurs, string nillable, string binding)
        {
            return "<attribute>" +
                        "<name>" + name + "</name>" +
                        "<minOccurs>" + minOccurs + "</minOccurs>" +
                        "<maxOccurs>" + maxOccurs + "</maxOccurs>" +
                        "<nillable>" + nillable + "</nillable>" +
                        "<binding>" + getGeoserverJavaType(binding) + "</binding>" +
                    "</attribute>";
        }

        protected static string createXMLBoundingBoxByProjection(string projeksioni)
        {
            return "<nativeBoundingBox>" +
                    "<minx>352896.20880000014</minx>" +
                    "<maxx>504847.45469999965</maxx>" +
                    "<miny>4388655.469900001</miny>" +
                    "<maxy>4723980.840299999</maxy>" +
                  "</nativeBoundingBox>" +
                  "<latLonBoundingBox>" +
                    "<minx>19.205285603731916</minx>" +
                    "<maxx>21.05915465484968</maxx>" +
                    "<miny>39.635040063926006</miny>" +
                    "<maxy>42.66828197066209</maxy>" +
                  "</latLonBoundingBox>";
        }

        /// <summary>
        /// Kthen stilin default qe do te kete layeri ne momentin qe krijohet, ne varesi te tipit te gjeometrise
        /// </summary>
        /// <param name="type">Tipi i te dhenave qe mbart layeri LINESTRING/MULTILINESTRING/POLYGON/MULTIPOLYGON/POINT</param>
        /// <returns>Kthen emrin e stilit qe do te perdoret</returns>
        protected static string getDefaultStyleByType(string type)
        {
            string styleName;
            switch (type)
            {
                case "LINESTRING":
                case "MULTILINESTRING":
                    styleName = "line";
                    break;
                case "POLYGON":
                case "MULTIPOLYGON":
                    styleName = "polygon";
                    break;
                default:
                    styleName = "point";
                    break;
            };
            return styleName;
        }

        #endregion

        #region SQLSERVER_STRUCTURE_LAYERS
        /// <summary>
        /// Krijon strukturen e view qe i bashkengjitet layerit
        /// </summary>
        /// <param name="layerIdentifikues">Emri i View: korespondues edhe me ate qe do te krijohet ne geoserver</param>
        /// <param name="layersType">Tipi i layer</param>
        /// <param name="nrStatusi">Statusi i Layer</param>
        /// <param name="meAutorizim">Do te aplikohet ose jo autorizimi</param>
        /// <returns></returns>
        protected static string createLayerViewStruc(string layerIdentifikues, clsLayersTypeGIS layersType, int nrStatusi, int meAutorizim)
        {
            string viewStruct = "";

            switch (layersType.WEBLLOJI)
            {
                case "MAGAZINA":
                    if (nrStatusi == 3)
                    {
                        viewStruct =
                        "CREATE VIEW [dbo].[" + layerIdentifikues + "] " + "\r\n" +
                        "AS " + "\r\n" + "\r\n" +
                        "       SELECT  objWeb.gid, objWeb.the_geom, objWeb.KODI, objWeb.PERSHKRIMI, IDAUTORIZUESI = -1, NRSTATUSI= " + nrStatusi + "\r\n" +
                        "       FROM    T_GIS_App_LIDHJE_OBJEKTE_WEB    objWeb " + "\r\n" +
                        "       WHERE   objWeb.IDLAYERSTYPE=" + layersType.IDLAYERSTYPE + " AND objWeb.NRSTATUSI=" + nrStatusi ;
                    }
                    else
                    { 
                        viewStruct =
                        "CREATE VIEW [dbo].[" + layerIdentifikues + "] " + " \r\n " +
                        "AS " + " \r\n " + " \r\n " +
                        "       SELECT  objWeb.gid, objWeb.the_geom, objWeb.KODI, objWeb.PERSHKRIMI, IDAUTORIZUESI, NRSTATUSI= " + nrStatusi + " \r\n " +
                        "       FROM    T_GIS_App_LIDHJE_OBJEKTE_WEB        objWeb " + "\r\n" +
                        "               INNER JOIN T_GIS_App_LAYERSTYPE     objType ON objWeb.IDLAYERSTYPE = objType.IDLAYERSTYPE " + " \r\n " +
                        "               INNER JOIN T_GIS_App_LAYERS         lays    ON lays.id_layer = objWeb.IDLAYER " + " \r\n " +
                        "               INNER JOIN " + " \r\n " +
                        "               (   SELECT  mag.LLOJLAYERI, mag.IDNJESIADM, IDAUTORIZUESI = trup.IDPERDORUESI " + " \r\n " +
                        "                   FROM    T_NJESIADMINISTRATIVE           mag " + " \r\n " +
                        "                           INNER JOIN T_LIDHJEAUTORIZIM    lidh    ON mag.IDNJESIADM = lidh.IDLIDHESE AND IDLLOJI = 20 " + " \r\n " +
                        "                           INNER JOIN T_AUTORIZIMTRUPI     trup    ON lidh.IDAUTORIZIMEKOKA = trup.IDAUTORIZIMKOKA " + " \r\n " +
                        "                           INNER JOIN T_AUTORIZIMKOKA      koka    ON trup.IDAUTORIZIMKOKA = koka.IDAUTORIZIMEKOKA " + " \r\n " +
                        "                   WHERE   mag.IDSTATUSDOK = 1 AND lidh.IDSTATUSDOK = 1 AND koka.IDSTATUSDOK = 1 " + " \r\n " +
                        "                   GROUP BY mag.LLOJLAYERI, mag.IDNJESIADM, trup.IDPERDORUESI " + " \r\n " +
                        "               ) MAGAUTO ON MAGAUTO.IDNJESIADM = objWeb.IDMAGAZINA AND MAGAUTO.LLOJLAYERI = objType.WEBLLOJID " + " \r\n " +
                        "       WHERE   objWeb.IDLAYERSTYPE=" + layersType.IDLAYERSTYPE + " AND objWeb.NRSTATUSI=" + nrStatusi + " AND objType.WEBLLOJI= '" + layersType.WEBLLOJI + "' ";
                    }
                    break;
                case "SERIALE":
                    if (nrStatusi == 3)
                        viewStruct =
                        "CREATE VIEW [dbo].[" + layerIdentifikues + "] " + "\r\n" +
                        "AS " + "\r\n" + "\r\n" +
                        "       SELECT  objWeb.gid, objWeb.the_geom, SERIALI=objWeb.SERIALKOD, objWeb.KODI, objWeb.PERSHKRIMI, IDAUTORIZUESI = -1, NRSTATUSI= " + nrStatusi + "\r\n" +
                        "       FROM    T_GIS_App_LIDHJE_OBJEKTE_WEB    objWeb " + "\r\n" +
                        "       WHERE   objWeb.IDLAYERSTYPE=" + layersType.IDLAYERSTYPE + " AND objWeb.NRSTATUSI=" + nrStatusi;
                    else
                        viewStruct =
                        "CREATE VIEW [dbo].[" + layerIdentifikues + "] " + " \r\n " +
                        "AS " + " \r\n " + " \r\n " +
                        "       SELECT  objWeb.gid, objWeb.the_geom, SERIALI=objWeb.SERIALKOD, objWeb.KODI, objWeb.PERSHKRIMI, IDAUTORIZUESI, NRSTATUSI= " + nrStatusi + " \r\n " +
                        "       FROM    T_GIS_App_LIDHJE_OBJEKTE_WEB        objWeb " + "\r\n" +
                        "               INNER JOIN T_GIS_App_LAYERSTYPE     objType ON objWeb.IDLAYERSTYPE = objType.IDLAYERSTYPE " + " \r\n " +
                        "               INNER JOIN T_GIS_App_LAYERS         lays    ON lays.id_layer = objWeb.IDLAYER " + " \r\n " +
                        "               INNER JOIN " + " \r\n " +
                        "               (   SELECT	IDAUTORIZUESI = trup.IDPERDORUESI, mag.IDNJESIADM " + "\r\n" +
                        "                   FROM    T_NJESIADMINISTRATIVE           mag " + " \r\n " +
                        "                           INNER JOIN T_LIDHJEAUTORIZIM    lidh    ON mag.IDNJESIADM = lidh.IDLIDHESE AND IDLLOJI = 20 " + " \r\n " +
                        "                           INNER JOIN T_AUTORIZIMTRUPI     trup    ON lidh.IDAUTORIZIMEKOKA = trup.IDAUTORIZIMKOKA " + " \r\n " +
                        "                           INNER JOIN T_AUTORIZIMKOKA      koka    ON trup.IDAUTORIZIMKOKA = koka.IDAUTORIZIMEKOKA " + " \r\n " +
                        "                   WHERE   mag.IDSTATUSDOK = 1 AND lidh.IDSTATUSDOK = 1 AND koka.IDSTATUSDOK = 1 " + " \r\n " +
                        "                   GROUP BY mag.LLOJLAYERI, mag.IDNJESIADM, trup.IDPERDORUESI " + " \r\n " +
                        "               ) MAGAUTO ON objWeb.IDMAGAZINA = MAGAUTO.IDNJESIADM	" + "\r\n" +
                        "       WHERE   objWeb.IDLAYERSTYPE=" + layersType.IDLAYERSTYPE + " AND objWeb.NRSTATUSI=" + nrStatusi + " AND objType.WEBLLOJI= '" + layersType.WEBLLOJI + "' ";
                    break;
                default:
                    viewStruct = "";
                    break;
            };

            return viewStruct;
        }
        #endregion

        #region GIS_KONFIGURIME
        /// <summary>
        /// Sherben te detyruar Geoserver qe te behet reload, ne menyre qe ngarkoje te gjithe ndryshimet qe mund te kene ndodhur ne skema apo stile
        /// </summary>
        /// <param name="geoURL">Url e instalimit te Geoserver qe do te aksesohet </param>
        /// <param name="geoUSER">UserName admin ne geoserver nepermjet te cilit do te kaloje kerkesa</param>
        /// <param name="geoPASSWORD">UserPassword admin ne geoserver nepermjet te cilit do te kaloje kerkesa</param>
        /// <returns></returns>
        public static clsMesazh ReloadGeoserverStatus(string geoURL, string geoUSER, string geoPASSWORD)
        {
            clsMesazh uBeReloadGeoserver = new clsMesazh(false, "Ndodhi nje gabim! Kontakto administratorin!");            
            uBeReloadGeoserver = clsFunksioneGIS.geoserverRequestForRestApi(geoURL + "rest/reload", "text/xml", "POST", geoUSER, geoPASSWORD, "", "");
            return uBeReloadGeoserver;
        }

        /// <summary>
        /// Krijon Workspace dhe DataStore per ndermarrjen qe i kalohet si parameter. 
        /// Ka pergjegjesine per krijimin e skemave te workspace dhe datastore ne Geoserver dhe shtimin e vlerave default ne DB.
        /// </summary>
        /// <param name="workspaceName">Emri i Workspace i cili do te jete i njejte edhe per DataStore. Duhet te jete Unik</param>
        /// <param name="idNdermarrje">Ndermarrja e lidhur me workspace</param>
        /// <param name="geoURL">Url e instalimit te Geoserver qe do te aksesohet </param>
        /// <param name="geoUSER">UserName admin ne geoserver nepermjet te cilit do te kaloje kerkesa</param>
        /// <param name="geoPASSWORD">UserPassword admin ne geoserver nepermjet te cilit do te kaloje kerkesa</param>
        /// <param name="geoDatabase">Emri i DB qe do te lidhet me Geoserver</param>
        /// <param name="geoPortDB">Porta e DB qe do te lidhet me Geoserver</param>
        /// <param name="geoUserDB">Username i userit te databases</param>
        /// <param name="geoPassDB">Password i userit te databases</param>
        /// <param name="wsFunksionesh">Workspace ku ruhen layerat e funksioneve ne GIS</param>
        /// <param name="wsDefault">Workspace ku ruhen layerat e Default ne GIS</param>
        /// <param name="wsOrientues">Workspace ku ruhen layerat Orientues</param>
        /// <param name="wsPublike">Workspace Publike</param>
        public static clsMesazh AddNewWorkspace(string workspaceName, int idNdermarrje, string geoURL, string geoUSER, string geoPASSWORD, string geoDatabase, string geoPortDB, string geoUserDB, string geoPassDB, string wsFunksionesh, string wsDefault, string wsOrientues, string wsPublike)
        {
            clsMesazh uKrijuaWorkspaceMeDatastore = new clsMesazh(false, "Ndodhi nje gabim! Kontakto administratorin!");
            clsWorkspaceGIS workspace_iRi = new clsWorkspaceGIS(0, workspaceName, idNdermarrje, geoURL, geoUSER, geoPASSWORD, geoDatabase, geoPortDB, geoUserDB, wsFunksionesh, wsDefault, wsOrientues, wsPublike);

            //1. Validimi i te dhenave
            uKrijuaWorkspaceMeDatastore = ValidateBeforeAddNewWorkspace(workspaceName, idNdermarrje, geoURL, geoUSER, geoPASSWORD);
            if (!uKrijuaWorkspaceMeDatastore.Status)
                return uKrijuaWorkspaceMeDatastore;

            //2.1 Ne Geoserver: Krijimi i workspace 
            string urlRequest = geoURL + "rest/workspaces";
            uKrijuaWorkspaceMeDatastore = clsFunksioneGIS.geoserverRequestForRestApi(urlRequest, "text/xml", "POST", geoUSER, geoPASSWORD, clsGeoserverGIS.CreateWorkspaceXml(workspaceName),"");
            if (!uKrijuaWorkspaceMeDatastore.Status)
                return uKrijuaWorkspaceMeDatastore;

            //2.2 Ne Geoserver: Krijimi i datastore
            urlRequest = geoURL + "rest/workspaces/" + workspace_iRi.Workspace_name + "/datastores";
            uKrijuaWorkspaceMeDatastore = clsFunksioneGIS.geoserverRequestForRestApi(urlRequest, "text/xml", "POST", workspace_iRi.GEOUSER, workspace_iRi.GEOPASSWORD, clsGeoserverGIS.CreateDataStoreXml(workspace_iRi, geoPassDB), "");
            if (!uKrijuaWorkspaceMeDatastore.Status)
                return uKrijuaWorkspaceMeDatastore;

            //3. Ne SQL Server
           using (clsDatabaseGIS dbGIS = new clsDatabaseGIS())
            {
                uKrijuaWorkspaceMeDatastore = SaveNewWorkspace(dbGIS, workspace_iRi);
            }

            return uKrijuaWorkspaceMeDatastore;
        }

        /// <summary>
        /// Krijon Workspace dhe DataStore per ndermarrjen qe i kalohet si parameter. 
        /// Ka pergjegjesine per krijimin e skemave te workspace dhe datastore ne Geoserver dhe shtimin e vlerave default ne DB.
        /// </summary>
        /// <param name="idworkspace">Id e Workspace qe do te modfikohet </param>
        /// <param name="workspaceName">Emri i Workspace i cili do te jete i njejte edhe per DataStore. Duhet te jete Unik</param>
        /// <param name="idNdermarrje">Ndermarrja e lidhur me workspace</param>
        /// <param name="geoURL">Url e instalimit te Geoserver qe do te aksesohet </param>
        /// <param name="geoUSER">UserName admin ne geoserver nepermjet te cilit do te kaloje kerkesa</param>
        /// <param name="geoPASSWORD">UserPassword admin ne geoserver nepermjet te cilit do te kaloje kerkesa</param>
        /// <param name="geoDatabase">Emri i DB qe do te lidhet me Geoserver</param>
        /// <param name="geoPortDB">Porta e DB qe do te lidhet me Geoserver</param>
        /// <param name="geoUserDB">Username i userit te databases</param>
        /// <param name="geoPassDB">Password i userit te databases</param>
        /// <param name="wsFunksionesh">Workspace ku ruhen layerat e funksioneve ne GIS</param>
        /// <param name="wsDefault">Workspace ku ruhen layerat e Default ne GIS</param>
        /// <param name="wsOrientues">Workspace ku ruhen layerat Orientues</param>
        /// <param name="wsPublike">Workspace Publike</param>
        public static clsMesazh ModifyWorkspace(int idworkspace, string workspaceName, int idNdermarrje, string geoURL, string geoUSER, string geoPASSWORD, string geoDatabase, string geoPortDB, string geoUserDB, string geoPassDB, string wsFunksionesh, string wsDefault, string wsOrientues, string wsPublike)
        {
            clsMesazh uNdryshuaWorkspace = new clsMesazh(false, "Ndodhi nje gabim! Kontakto administratorin!");
            clsWorkspaceGIS workspace = new clsWorkspaceGIS(idworkspace, workspaceName, idNdermarrje, geoURL, geoUSER, geoPASSWORD, geoDatabase, geoPortDB, geoUserDB, wsFunksionesh, wsDefault, wsOrientues, wsPublike);
            
            //1. Ne SQL Server
            using (clsDatabaseGIS dbGIS = new clsDatabaseGIS())
            {
                uNdryshuaWorkspace = SaveExistWorkspace(dbGIS, workspace);
            }

            return uNdryshuaWorkspace;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="WSPerTuFshire">Lista e workspace per tu fshire</param>
        /// <param name="rm">ResourceManager</param>
        /// <param name="cultinf">CultureInfo</param>
        /// <returns></returns>
        public static clsMesazh FshiWorkspaceTeZgjedhur(clsWorkspaceGIS tempWorkspace)
        {
            clsMesazh uFshiWorkspace = new clsMesazh(false, tempWorkspace.Workspace_name);

            //1. Validimi i te dhenave
            uFshiWorkspace = ValidateBeforeDeleteWorkspace(tempWorkspace);

            //2. Ne SQL Server
            if (uFshiWorkspace.Status)
            {
                using (clsDatabaseGIS dbGIS = new clsDatabaseGIS())
                {
                    uFshiWorkspace = DeleteExistWorkspace(dbGIS, tempWorkspace);
                }
            }

            //3. Geoserver
            if (uFshiWorkspace.Status)
            {
                string urlRequest = tempWorkspace.GEOURL + "rest/workspaces/" + tempWorkspace.Workspace_name + "?recurse=true";
                uFshiWorkspace = clsFunksioneGIS.geoserverRequestForRestApi(urlRequest, "text/xml", "DELETE", tempWorkspace.GEOUSER, tempWorkspace.GEOPASSWORD, "", "");
                if (!uFshiWorkspace.Status)
                    return uFshiWorkspace;
                uFshiWorkspace.PershkrimMesazhi = tempWorkspace.Workspace_name;
            }
            return uFshiWorkspace;
        }

        /// <summary>
        /// Krijon Layer te ri ne Geoserver sebashku me skemen e saj te feature dhe e lidh me stilin default ne varesi te gjeometrise
        /// </summary>
        /// <param name="idNdermarrje">Ndermarrjes se ciles po i shtohet layeri</param>
        /// <param name="layerName">Emri i Layer i cili do te jete unik</param>
        /// <param name="projection">Projeksioni srs defaul ku do te projektoje te dhenat e layerit</param>
        /// <param name="type">Tipi i te dhenave qe mbart layeri LINESTRING/MULTILINESTRING/POLYGON/MULTIPOLYGON/POINT</param>
        /// <param name="idFolder">id e folderit ku grupohet layer ne shtresat e hartes</param>
        /// <param name="idLayerType">Tipi i layerit qe do te shtohet</param>
        /// <param name="nrStatusi">Status i layerit qe po shtohet</param>
        /// <param name="meAutorizim">Nese ne kete layer do te aplikohet autorizimi ose jo</param>
        /// <returns></returns>
        public static clsMesazh CreateLayer(int idNdermarrje, string layerName, string projection, string type, int idFolder, int idLayerType, int nrStatusi, int meAutorizim)
        {
            clsMesazh uKrijuaFeatures = new clsMesazh(false, "Ndodhi nje gabim! Kontakto administratorin!");
            clsWorkspaceGIS workspaceAktual = new clsWorkspaceGIS();
            clsLayersTypeGIS layersType = new clsLayersTypeGIS();
            string urlRequest, dataStoreName, layerIdentifikues;
            
            // Ne SQL Server
            using (clsDatabaseGIS dbGIS = new clsDatabaseGIS())
            {
                workspaceAktual.mbushWorkspaceSipasNdermarrjesId(dbGIS, idNdermarrje);
                layerIdentifikues = "V_GIS_Layer_" + workspaceAktual.Workspace_name + "_" + layerName + "_NR" + nrStatusi;
                if (!dbGIS.ekzistonLayerByTableName(layerIdentifikues))
                {
                    layersType.merrLayersTypeSipasId(dbGIS, idLayerType);
                    uKrijuaFeatures = ruajNewLayer(dbGIS, workspaceAktual, layerName, layerIdentifikues, idFolder, idLayerType, nrStatusi, projection, type, createLayerViewStruc(layerIdentifikues, layersType, nrStatusi, meAutorizim));
                }
            }

            // Ne Geoserver
            if (uKrijuaFeatures.Status)
            {
                dataStoreName = workspaceAktual.Workspace_name;
                urlRequest = workspaceAktual.GEOURL + "rest/workspaces/" + workspaceAktual.Workspace_name + "/datastores/" + dataStoreName + "/featuretypes";
                uKrijuaFeatures = clsFunksioneGIS.geoserverRequestForRestApi(urlRequest, "application/xml", "POST", workspaceAktual.GEOUSER, workspaceAktual.GEOPASSWORD, clsGeoserverGIS.CreateFeatureXml(layerIdentifikues, projection, type, layersType.WEBLLOJI), "");
            }

            if (uKrijuaFeatures.Status)
                uKrijuaFeatures = ChangeLayerDefaultStyle(workspaceAktual.Workspace_name, workspaceAktual.GEOURL, workspaceAktual.GEOUSER, workspaceAktual.GEOPASSWORD, layerIdentifikues, getDefaultStyleByType(type));

            return uKrijuaFeatures;
        }

        /// <summary>
        /// Ndryshon stilin default te Layerit
        /// </summary>
        /// <param name="workspaceName">Workspace ku ndodhet Layeri</param>
        /// <param name="geoURL">Url e instalimit te Geoserver qe do te aksesohet </param>
        /// <param name="geoUSER">UserName admin ne geoserver nepermjet te cilit do te kaloje kerkesa</param>
        /// <param name="geoPASSWORD">UserPassword admin ne geoserver nepermjet te cilit do te kaloje kerkesa</param>
        /// <param name="featuresName">Emri i feature i cili do te jete unik</param>
        /// <param name="styleName">Name i stilit ne geoserver qe do ti bashkengjitet layert te ri</param>
        /// <returns></returns>
        public static clsMesazh ChangeLayerDefaultStyle(string workspaceName, string geoURL, string geoUSER, string geoPASSWORD, string featuresName, string styleName)
        {
            clsMesazh uNdryshuaDefaultStyle = new clsMesazh(false, "Ndodhi nje gabim! Kontakto administratorin!");
            string urlRequest;

            urlRequest = geoURL + "rest/layers/" + workspaceName + ":" + featuresName;
            uNdryshuaDefaultStyle = clsFunksioneGIS.geoserverRequestForRestApi(urlRequest, "text/xml", "PUT", geoUSER, geoPASSWORD, clsGeoserverGIS.ChangeLayerDefaultStyle(styleName, workspaceName),"");

            return uNdryshuaDefaultStyle;
        }
        #endregion

        #region Validime te dhenash

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workspaceName"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="geoURL"></param>
        /// <param name="geoUSER"></param>
        /// <param name="geoPASSWORD"></param>
        /// <returns></returns>
        public static clsMesazh ValidateBeforeAddNewWorkspace(string workspaceName, int idNdermarrje, string geoURL, string geoUSER, string geoPASSWORD)
        {
            clsMesazh uValiduanTeDhenat = new clsMesazh(true, "Procedura e validimit u kalua me sukses!");

            // Ne SQL Server
            using (clsDatabaseGIS dbGIS = new clsDatabaseGIS())
            {
                if (dbGIS.ekzistonWorkspacePerNdermarrjen(idNdermarrje, workspaceName))
                {
                    uValiduanTeDhenat.Status = false;
                    uValiduanTeDhenat.PershkrimMesazhi = "Ekziston ne Sql Server!";
                    return uValiduanTeDhenat;
                }
            }

            // Ne Geoserver
            string urlRequest = geoURL + "rest/workspaces";
            uValiduanTeDhenat = clsFunksioneGIS.geoserverRequestForRestApi(urlRequest, "", "GET", geoUSER, geoPASSWORD, "", "text/xml");
            if (uValiduanTeDhenat.Status)
            {
                uValiduanTeDhenat = existsStringOnLastNodeXML(CreateXmlDokFromString(uValiduanTeDhenat.PershkrimMesazhi), "/workspaces/workspace", "name", workspaceName);
                if (!uValiduanTeDhenat.Status)
                    uValiduanTeDhenat.PershkrimMesazhi = "Ekziston ne Geoserver!";
            }

            return uValiduanTeDhenat;
        }

        public static clsMesazh ValidateBeforeDeleteWorkspace(clsWorkspaceGIS tempWorkspace)
        {
            clsMesazh uValiduanTeDhenat = new clsMesazh(true, tempWorkspace.Workspace_name);
            
            using (clsDatabaseGIS dbGIS = new clsDatabaseGIS())
            {
                if (dbGIS.ekzistonLayerNeNdermarrjen(tempWorkspace.IDNDERMARJE))
                    uValiduanTeDhenat.Status = false;
            }

            return uValiduanTeDhenat;
        }
        #endregion

        #region Transaksione
        /// <summary>
        /// Krijon Workspace te ri qe i bashkenjgjitet nje ndermarrje ne web. Perfshin klasen e Workspace si dhe Shtresat baze default
        /// </summary>
        /// <param name="dbGIS">Db Connection</param>
        /// <param name="workspace_iRi">Objekti i ri</param>
        /// <returns></returns>
        public static clsMesazh SaveNewWorkspace(clsDatabaseGIS dbGIS, clsWorkspaceGIS workspace_iRi)
        {
            clsMesazh mesazh = new clsMesazh(false, "");

            try
            {
                dbGIS.beginTransaksion();

                mesazh = workspace_iRi.ruajWorkspaceGIS(dbGIS);
                if (!mesazh.Status)
                {
                    dbGIS.rollbackTransaksion();
                    return mesazh;
                }

                mesazh = workspace_iRi.ruajBaseLayerXWorkspaceGIS(dbGIS);
                if (!mesazh.Status)
                {
                    dbGIS.rollbackTransaksion();
                    return mesazh;
                }

                dbGIS.commitTransaksion();
                return mesazh;
            }
            catch (Exception ce)
            {
                dbGIS.rollbackTransaksion();
                return new clsMesazh(false, ce.Message);
            }
        }


        /// <summary>
        /// Modifikon te dhenat e ruajtura per Workspace te celur me pare. Mund te ndryshohet path i instalimit te Geoserver, porta e saj, username ose password
        /// </summary>
        /// <param name="dbGIS">Db Connection</param>
        /// <param name="workspace">Objekti qe do te ndryshohet</param>
        /// <returns></returns>
        public static clsMesazh SaveExistWorkspace(clsDatabaseGIS dbGIS, clsWorkspaceGIS workspace)
        {
            clsMesazh mesazh = new clsMesazh(false, "");

            try
            {
                dbGIS.beginTransaksion();

                mesazh = workspace.modifikoWorkspaceGIS(dbGIS);
                if (!mesazh.Status)
                {
                    dbGIS.rollbackTransaksion();
                    return mesazh;
                }

                dbGIS.commitTransaksion();
                return mesazh;
            }
            catch (Exception ce)
            {
                dbGIS.rollbackTransaksion();
                return new clsMesazh(false, ce.Message);
            }
        }

        /// <summary>
        /// Fshihet nje workspace ekzistues
        /// </summary>
        /// <param name="dbGIS">Db Connection</param>
        /// <param name="workspace">Objekti i ri</param>
        /// <returns></returns>
        public static clsMesazh DeleteExistWorkspace(clsDatabaseGIS dbGIS, clsWorkspaceGIS workspace)
        {
            clsMesazh mesazh = new clsMesazh(false, "");

            try
            {
                dbGIS.beginTransaksion();

                mesazh = workspace.fshiBaseLayerXWorkspaceGIS(dbGIS);
                if (!mesazh.Status)
                {
                    dbGIS.rollbackTransaksion();
                    return mesazh;
                }

                mesazh = workspace.fshiWorkspaceGIS(dbGIS);
                if (!mesazh.Status)
                {
                    dbGIS.rollbackTransaksion();
                    return mesazh;
                }

                dbGIS.commitTransaksion();
                return mesazh;
            }
            catch (Exception ce)
            {
                dbGIS.rollbackTransaksion();
                return new clsMesazh(false, ce.Message);
            }
        }
        /// <summary>
        /// Procedura per ruajten e nje layeri te ri, i cili do te beje shtimin e te dhenave te nevojshme ne te gjithe tabelat duke perfshire te drejtat gjithashtu edhe strukturen e view 
        /// </summary>
        /// <param name="dbGIS">Db Connection</param>
        /// <param name="workspaceAktual">Workspace ku ndodhet Layeri</param>
        /// <param name="idndermarrje">Ndermarrja ku do te shtohet layer</param>
        /// <param name="layerName">Pershkrimi i layerit</param>
        /// <param name="layerIdentifikues">Emri i view unik ne sql dhe ne workspace</param>
        /// <param name="idFolder">Id e grupit te shtresave te hartes ku do te futet layeri</param>
        /// <param name="idLayerType">Tipi i layerit</param>
        /// <param name="nrStatusi">Statusi i layerit</param>
        /// <param name="projection">Projeksini i te dhenave gjeometrike</param>
        /// <param name="type">Tipi i gjeometrise</param>
        /// <param name="createLayerViewStruc">Struktura e view qe do te gjenerohet</param>
        /// <returns></returns>
        public static clsMesazh ruajNewLayer(clsDatabaseGIS dbGIS, clsWorkspaceGIS workspaceAktual, string layerName, string layerIdentifikues, int idFolder, int idLayerType, int nrStatusi, string projection, string type, string createLayerViewStruc)
        {
            clsMesazh mesazh = new clsMesazh(false, "");
            
            try
            {
                dbGIS.beginTransaksion();

                mesazh = dbGIS.ruajNewLayerMeTeDrejtaGIS(workspaceAktual.IDNDERMARJE, layerName, layerIdentifikues, idFolder, idLayerType, nrStatusi, type);
                if (!mesazh.Status)
                {
                    dbGIS.rollbackTransaksion();
                    return mesazh;
                }

                mesazh = dbGIS.createLayerViewStruc(createLayerViewStruc);
                if (!mesazh.Status)
                {
                    dbGIS.rollbackTransaksion();
                    return mesazh;
                }

                dbGIS.commitTransaksion();
                return mesazh;
            }
            catch (Exception ce)
            {
                dbGIS.rollbackTransaksion();
                return new clsMesazh(false, ce.Message);
            }
        }
        
        public static DataTable merrKordinataPerZgjedhjeQV2017(string filter)
        {
            clsDatabaseGIS dbGis = new clsDatabaseGIS();
            return dbGis.merrKordinataPerZgjedhjeQV2017NeDB(filter);
        }

        #endregion
    }
}