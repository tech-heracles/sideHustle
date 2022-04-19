using CacheLayer;
using static CacheLayer.GlobalCacheManager;
using DbCore.DbAdmin;
using DbCore.DbAsete;
using DbCore.DbCRM;
using DbCore.DbGIS;
using DbCore.DbKontabiliteti;
using DbCore.DbListPagesat;
using DbCore.DbProdhimi;
using DbCore.DbQendraKosto;
using DbCore.DbShare;
using DbCore.Integrime;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using System.Globalization;
using System.Web;
using System.Web.Configuration;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using DbCore.IMBUtils.Cache;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.Messages;
using System.Reflection;
using System.Resources;
using DbCore;
using DbCore.DbRegjistrim;

namespace DbCore
{
    /// <summary>
    /// kjo klase perdoret per funksionet qe perdoren nga shume faqe dhe qe kane te bejne me Sesionet
    /// </summary>
    public class mySessionObjects
    {
        protected static string ComposedGuidName(string key, string guid) => $"{key}_{guid}";
        /// <summary>
        /// Kjo Metode ruan griden ne session
        /// </summary>
        /// <param name="session">Sessioni ku do ruhet grida</param>
        /// <param name="gridDataSource">dataSource-i i grides qe do ruhet</param>
        /// <returns>True nese ruajta perfundoi me sukses, False perndryshe</returns>
        public static bool ruajGrideNeSessionLupa(HttpSessionState session, Object gridDataSource, bool expire = true)
        {

            MyPageCache["gridDataSourceLupa", expire] = gridDataSource;
            return true;

        }

       
        public static void SaveFilter(int idNdermarrje, string filterExpression, string gridId, string ambient)
        {
            MySessionCache["Filter" + gridId + ambient + idNdermarrje] = filterExpression;
        }


        /// <summary>
        /// Vendos filtrin nga sessioni ne grid
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="sesioni"></param>
        /// <param name="grid"></param>
        /// <param name="ambient"></param>
        public static string RestoreFilter(int idNdermarrje, string gridId, string ambient)
        {
            var filtri = MySessionCache["Filter" + gridId + ambient + idNdermarrje];
            if (filtri != null)
                return filtri.ToString();
            return null;
        }


        /// <summary>
        /// ruan griden ne sesion
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <param name="gridDataSource">datasource i grides</param>
        /// <returns></returns>
        public static bool ruajGrideNeSession(HttpSessionState session, DataTable gridDataSource)
        {
            return ruajGrideNeSession("", session, gridDataSource);
        }

        public static void RemoveGridRowsInSessionById(string key, string guidString, string keyFieldName, List<int> keys)
        {
            RemoveGridRowsInSessionById<DataTable>(ComposedGuidName(key, guidString), keyFieldName, keys, null);
        }

        public static void RemoveGridRowsInSessionById<T>(string komponente, string keyFieldName, List<int> keys, Predicate<T> finder)
        {
            if (komponente == "")
                komponente = "gridDataSource";
            var obj = MySessionCache[komponente];
            if (obj == null)
                return;
            if (obj.GetType() == typeof(DataTable))
            {
                var dt = (DataTable)obj;
                if (keys.Count != 0)
                    dt.RemoveRows(keyFieldName, keys);

                return;
            }

            if (obj.GetType().BaseType.Name == typeof(List<T>).Name)
            {
                var col = (List<T>)obj;
                col.RemoveAll(finder);
           
            }
        }
     
        public static void RemoveGridRowsInSessionByIdPC(string komponente, string keyFieldName, List<int> keys)
        {
            if (komponente == "")
                komponente = "gridDataSource";
            var dt = (DataTable)MyPageCache[komponente];
            if (keys.Count != 0 && dt != null)
                dt.RemoveRows(keyFieldName, keys);
        }

        public static bool ruajGrideNeSession(string komponente, HttpSessionState session, DataTable gridDataSource)
        {
            ruajGrideNeSession(komponente, session, (object)gridDataSource, true);
            return true;
        }
        public static bool ruajGrideNeSession(string komponente, int idViti, string periudhaDok, HttpSessionState session, DataTable gridDataSource)
        {
            return ruajGrideNeSession(SessionKeyUtils.MerrSessionKeyPerDsGride(komponente, idViti, periudhaDok), session, gridDataSource);
        }

        public static void ruajGrideNeSession(string key, HttpSessionState session, object gridDataSource, bool expire = false)
        {
            if (key == "")
                key = "gridDataSource";
            MyPageCache[key, expire] = gridDataSource;
        }

        public static object merrDsComboGrideNeSession(HttpSessionState session, string emerCombo)
        {
            if (session.IsNewSession)
            {
                //throw new mySessionNewException("Sessioni ka skaduar! Ju lutem logohuni perseri");
                clsFunksione.logout(session, true, "MbarimSessioni");
            }


            if (MyPageCache[emerCombo] != null)
            {
                return (object)MyPageCache[emerCombo];
            }
            else return null;
        }

        public static bool ruajDsComboGrideNeSession(HttpSessionState session, object gridComboDataSource, string emerCombo)
        {
            MyPageCache[emerCombo] = gridComboDataSource;
            return true;
        }

        /// <summary>
        /// Ruan koleksion ndermarrjesh te ruajtur ne sesion: "colnder"
        /// </summary>
        /// <param name="session"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static bool ruajColNderNeSession(HttpSessionState session, DataTable dt)
        {
            if (MyPageCache["colnder"] == null)
                MyPageCache.Add("colnder", dt);
            else MyPageCache["colnder"] = dt;
            return true;
        }

        /// <summary>
        /// merr koleksionin e ndermarrjeve te ruajtura ne sesion: "colnder"
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <returns></returns>
        public static DataTable merrColNderNgaSesioni(HttpSessionState session)
        {
            if (session.IsNewSession)
            {
                clsFunksione.logout(session, true, "MbarimSessioni");
            }

            if (MyPageCache["colnder"] != null)
            {
                return (DataTable)MyPageCache["colnder"];
            }
            else return null;
        }

        /// <summary>
        /// merr trupat nga sesioni
        /// </summary>
        /// <param name="session"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DbCore.DbRegjistrim.colTrupiMagazina merrTrupatNgaSesioni(HttpSessionState session)
        {
            return merrTrupatNgaSesioni("trupat", session);
        }

        public static DbCore.DbRegjistrim.colTrupiMagazina merrTrupatNgaSesioni(string guidString, HttpSessionState session)
        {
            if (session.IsNewSession)
            {
                //throw new mySessionNewException("Sessioni ka skaduar! Ju lutem logohuni perseri");
                clsFunksione.logout(session, true, "MbarimSessioni");
            }
            try
            {
                return (DbCore.DbRegjistrim.colTrupiMagazina)MyPageCache[guidString];

            }
            catch (Exception)
            {
                return null;
            }


        }

        /// <summary>
        /// Ruan trupat e magazines ne sesion
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <param name="trupat">trupat e magazines</param>
        /// <returns>true nese ruajtja behet ne rregull, false nqs ndodh ndonje gabim gjate ruajtjes</returns>
        public static bool ruajTrupatNeSession(HttpSessionState session, DbCore.DbRegjistrim.colTrupiMagazina trupat)
        {
            return ruajTrupatNeSession("trupat", session, trupat);
        }

        public static bool ruajTrupatNeSession(string guidString, HttpSessionState session, DbCore.DbRegjistrim.colTrupiMagazina trupat)
        {
            MyPageCache[guidString] = trupat;
            return true;
        }

        /// <summary>
        /// Ruan trupat e magazines ne sesion
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <param name="trupat">trupat e rezervimit</param>
        /// <returns>true nese ruajtja behet ne rregull, false nqs ndodh ndonje gabim gjate ruajtjes</returns>
        public static bool ruajTrupatRezNeSession(HttpSessionState session, DbCore.DbRegjistrim.colTrupiRezervime trupat)
        {

            if (MyPageCache["trupat"] == null)
                MyPageCache.Add("trupat", trupat);
            else
                MyPageCache["trupat"] = trupat;
            return true;
        }

        /// <summary>
        /// merr trupat e rezervimit nga sesioni
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static DbCore.DbRegjistrim.colTrupiRezervime merrTrupatRezNgaSesioni(HttpSessionState session)
        {

            return (DbCore.DbRegjistrim.colTrupiRezervime)MyPageCache["trupat"];
        }

        /// <summary>
        /// merr produktet e prodhimit nga sesioni
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <returns></returns>
        public static colProduktProdhimi merrProdukteProdhimiNgaSesioni(HttpSessionState session)
        {

            if (MyPageCache["Produktet"] != null)
            {
                return (colProduktProdhimi)MyPageCache["Produktet"];
            }
            else return null;
        }

        /// <summary>
        /// merr projektet e prodhimit nga sesioni
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <returns></returns>
        public static colProjektProdhimi merrProjektProdhimiNgaSesioni(HttpSessionState session)
        {

            if (MyPageCache["Produktet"] != null)
            {
                return (colProjektProdhimi)MyPageCache["Produktet"];
            }
            else return null;

        }

        /// <summary>
        /// Ruan produktet e prodhimit ne sesion
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <param name="prod">produkti i prodhimit qe do ruhet ne sesion</param>
        /// <returns>true nese ruajtja behet ne rregull, false nqs ndodh ndonje gabim gjate ruajtjes</returns>
        public static bool ruajProdukteProdhimiNeSession(HttpSessionState session, Object prod)
        {

            if (MyPageCache["Produktet"] == null)
                MyPageCache.Add("Produktet", prod);
            else
                MyPageCache["Produktet"] = prod;
            return true;
        }

        /// <summary>
        /// ruan projekt prodhimin ne sesion
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <param name="projekt">projekti i prodhimit qe do ruhet</param>
        /// <returns>true nese ruajtja behet ne rregull, false nqs ndodh ndonje gabim gjate ruajtjes</returns>
        public static bool ruajProjektProdhimiNeSesion(HttpSessionState session, colProjektProdhimi projekt)
        {

            if (MyPageCache["Produktet"] == null)
                MyPageCache.Add("Produktet", projekt);
            else
                MyPageCache["Produktet"] = projekt;
            return true;
        }

        /// <summary>
        /// merr recepturat nga sesioni
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <returns></returns>
        public static colRecepturaProdhimi merrRecepturatNgaSesioni(HttpSessionState session)
        {


            if (MyPageCache["Recepturat"] != null)
            {
                return (colRecepturaProdhimi)MyPageCache["Recepturat"];
            }
            else return null;
        }

        /// <summary>
        /// Ruan recepturat ne sesion
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <param name="rec">recepturat</param>
        /// <returns></returns>
        public static bool ruajRecepturatNeSession(HttpSessionState session, colRecepturaProdhimi rec)
        {
            if (MyPageCache["Recepturat"] == null)
                MyPageCache.Add("Recepturat", rec);
            else
                MyPageCache["Recepturat"] = rec;
            return true;
        }

        /// <summary>
        /// Ruan koleksion ndermarrjesh ne sesion: "colnder2"
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <param name="dt">data table, koleksioni i ndermarrjeve</param>
        /// <returns></returns>
        public static bool ruajColNder2NeSession(HttpSessionState session, DataTable dt)
        {

            if (MyPageCache["colnder2"] == null)
                MyPageCache.Add("colnder2", dt);
            else
                MyPageCache["colnder2"] = dt;
            return true;
        }

        /// <summary>
        /// merr koleksion ndermarrjes nga sesioni: "colnder2"
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static DataTable merrColNder2NgaSesioni(HttpSessionState session)
        {


            if (MyPageCache["colnder2"] != null)
            {
                return (DataTable)MyPageCache["colnder2"];
            }
            else return null;

        }

        /// <summary>
        /// Kjo metode merr griden nga sessioni
        /// </summary>
        /// <param name="session">Sessioni ku eshte ruajtur grida</param>
        /// <param name="gridDataSource">dataSource-i i grides</param>
        /// <returns>True nese marrja perfundoi me sukses, False perndryshe</returns>
        public static bool merrGrideNgaSessioniLupa(HttpSessionState session, out Object gridDataSource)
        {
            try
            {
                gridDataSource = (Object)MyPageCache["gridDataSourceLupa"];
                return true;
            }
            catch (Exception)
            {
                gridDataSource = null;
                return false;
            }
        }

        /// <summary>
        /// Kjo metode merr griden nga sessioni
        /// </summary>
        /// <param name="session">Sessioni ku eshte ruajtur grida</param>
        /// <param name="gridDataSource">dataSource-i i grides</param>
        /// <returns>True nese marrja perfundoi me sukses, False perndryshe</returns>
        public static bool merrGrideNgaSessioniLupaArtikull(HttpSessionState session, out Object gridDataSource)
        {
            try
            {
                gridDataSource = (Object)MyPageCache["gridDataSourceLupaArtikull"];
                return true;
            }
            catch (Exception)
            {
                gridDataSource = null;
                return false;
            }
        }

        /// <summary>
        /// Kjo Metode ruan griden ne session
        /// </summary>
        /// <param name="session">Sessioni ku do ruhet grida</param>
        /// <param name="gridDataSource">dataSource-i i grides qe do ruhet</param>
        /// <returns>True nese ruajta perfundoi me sukses, False perndryshe</returns>
        public static bool ruajGrideNeSessionLupaArtikull(HttpSessionState session, Object gridDataSource)
        {
            try
            {
                MyPageCache["gridDataSourceLupaArtikull", true] = gridDataSource;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// merr griden e ruajtur ne sesion
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <param name="gridDataSource">datasource i grides</param>
        /// <returns></returns>
        public static bool merrGrideNgaSessioni(HttpSessionState session, out DataTable gridDataSource)
        {
            return merrGrideNgaSessioni("", session, out gridDataSource);
        }


        public static bool merrGrideNgaSessioni(string komponente, HttpSessionState session, out DataTable gridDataSource, bool shtoPageId)
        {
            if (komponente == "")
                komponente = "gridDataSource";
            if (MyPageCache.Get<object>(komponente, shtoPageId) == null)
            {
                gridDataSource = null;
                return false;
            }
            gridDataSource = (DataTable)MyPageCache.Get<object>(komponente, shtoPageId);
            return true;
        }
        public static bool merrGrideNgaSessioni(string komponente, HttpSessionState session, out DataTable gridDataSource)
        {
            return merrGrideNgaSessioni(komponente, session, out gridDataSource, true);
        }

        public static void merrGrideNgaSessioni(string komponente, HttpSessionState session, Dictionary<string, DataTable> myDt)
        {
            merrGrideNgaSessioni(komponente, session, false, myDt);
        }

        public static void merrGrideNgaSessioni(string komponente, HttpSessionState session, bool shtoPageId, Dictionary<string, DataTable> myDt)
        {
            komponente = komponente == "" ? "gridDataSource" : komponente;
            string CacheKey = MyPageCache.MyCache.AllKeys?.FirstOrDefault(s => s.Contains(komponente));
            DataTable gridDataSource = (DataTable)MyPageCache.Get<object>(CacheKey, shtoPageId);

            if (gridDataSource != null && gridDataSource.Rows.Count >= 0)
                myDt.Add(CacheKey, gridDataSource) ;
        }
        public static bool merrGrideNgaSessioni(string komponente, int idViti, string periudheDok, HttpSessionState session, out DataTable gridDataSource)
        {
            return merrGrideNgaSessioni(SessionKeyUtils.MerrSessionKeyPerDsGride(komponente, idViti, periudheDok), session, out gridDataSource);
        }

        public static bool merrGrideNgaSessioni(string komponente, HttpSessionState session, out object gridDataSource)
        {


            if (komponente == "")
                komponente = "gridDataSource";
            if (MyPageCache[komponente] == null)
            {
                gridDataSource = null;
                return false;
            }
            else
            {
                gridDataSource = (object)MyPageCache[komponente];
                return true;
            }

        }

        public static bool merrGrideNgaSessioni(HttpSessionState session, out object gridDataSource)
        {

            if (MyPageCache["gridDataSource"] == null)
            {
                gridDataSource = null;
                return false;
            }
            gridDataSource = MyPageCache["gridDataSource"];
            return true;
        }

        /// <summary>
        /// merr data table e ruajtur ne sesion
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <returns></returns>
        public static DataTable merrDtNgaSessioni(HttpSessionState session)
        {
            if (MyPageCache["dt"] != null)
                return (DataTable)MyPageCache["dt"];
            else return null;
        }
        /// <summary>
        /// merr data table e ruajtur ne sesion 
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <returns></returns>
        public static DataTable merrDtNgaSessioni(HttpSessionState session, string pageId)
        {
            var cache = GlobalCacheManager.GetPageCacheByPageID(pageId);
            if (cache["dt"] != null)
                return (DataTable)cache["dt"];
            else return null;
        }
        /// <summary>
        /// ruan nje data table ne sesion
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <param name="dt">data table</param>
        /// <returns>True nese ruajtja perfundoi me sukses, False perndryshe</returns>
        public static bool ruajdtNeSession(HttpSessionState session, DataTable dt)
        {

            if (MyPageCache["dt"] == null)
                MyPageCache.Add("dt", dt);
            else
                MyPageCache["dt"] = dt;
            return true;
        }

        /// <summary>
        /// Kjo metode merr clsRaportin nga sessioni
        /// </summary>
        /// <param name="session">Sessioni</param>
        /// <returns></returns>
        public static clsRaporti merrOClsRaportiNgaSesioni(HttpSessionState session)
        {
            if (session.IsNewSession)
            {
                //throw new mySessionNewException("Sessioni ka skaduar! Ju lutem logohuni perseri");
                clsFunksione.logout(session, true, "MbarimSessioni");
            }

            if (MySessionCache["oClsRaporti"] != null)
                return (clsRaporti)MySessionCache["oClsRaporti"];
            else return null;


        }

        /// <summary>
        /// ruan clsRaporti ne sesion
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <param name="rap">raporti qe do ruhet ne sesion</param>
        /// <returns>True nese ruajtja perfundoi me sukses, False perndryshe</returns>
        public static bool ruajOClsRaportiNeSession(HttpSessionState session, clsRaporti rap)
        {
            MySessionCache["oClsRaporti"] = rap;
            return true;

        }

        /// <summary>
        /// merr atributet e trupit nga sesioni
        /// </summary>
        /// <param name="session"></param>
        /// <returns>True nese ruajtja perfundoi me sukses, False perndryshe</returns>
        public static colAtributeTrupi merrAtributetNgaSessioni(HttpSessionState session)
        {
            if (session.IsNewSession)
            {
                //throw new mySessionNewException("Sessioni ka skaduar! Ju lutem logohuni perseri");
                clsFunksione.logout(session, true, "MbarimSessioni");
            }

            if (MySessionCache["Atributet"] != null)
                return (colAtributeTrupi)MySessionCache["Atributet"];
            else return null;

        }

        /// <summary>
        /// ruan atributet e trupit ne sesion
        /// </summary>
        /// <param name="session">sesion</param>
        /// <param name="obj">atributet</param>
        /// <returns>True nese ruajtja perfundoi me sukses, False perndryshe</returns>
        public static bool ruajAtributetNeSession(HttpSessionState session, colAtributeTrupi obj)
        {

            if (MySessionCache["Atributet"] == null)
                MySessionCache.Add("Atributet", obj);
            else
                MySessionCache["Atributet"] = obj;
            return true;

        }

        /// <summary>
        /// merr kushtet nga sesioni
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static colKusht merrKushteNgaSessioni(HttpSessionState session)
        {
            if (session.IsNewSession)
            {
                //throw new mySessionNewException("Sessioni ka skaduar! Ju lutem logohuni perseri");
                clsFunksione.logout(session, true, "MbarimSessioni");
            }

            if (MySessionCache["kushte"] != null)
                return (colKusht)MySessionCache["kushte"];
            else return null;

        }

        /// <summary>
        /// ruan kushtet ne sesion
        /// </summary>
        /// <param name="session"></param>
        /// <param name="kusht">kushtet qe do ruhen ne sesion</param>
        /// <returns>True nese ruajtja perfundoi me sukses, False perndryshe</returns>
        public static bool ruajKushteNeSesion(HttpSessionState session, colKusht kusht)
        {

            if (MySessionCache["kushte"] == null)
                MySessionCache.Add("kushte", kusht);
            else
                MySessionCache["kushte"] = kusht;
            return true;
        }

        /// <summary>
        /// merr trupin e grides nga sesioni
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <returns></returns>
        public static colGridaTrupi merrGridaTrupiNgaSessioni(HttpSessionState session)
        {
            if (session.IsNewSession)
            {
                //throw new mySessionNewException("Sessioni ka skaduar! Ju lutem logohuni perseri");
                clsFunksione.logout(session, true, "MbarimSessioni");
            }

            if (MySessionCache["GridaTrupi"] != null)
                return (colGridaTrupi)MySessionCache["GridaTrupi"];
            else return null;

        }

        /// <summary>
        /// ruan trupin e grides ne sesion
        /// </summary>
        /// <param name="session">sesion</param>
        /// <param name="obj">trupi i grides</param>
        /// <returns>True nese ruajtja perfundoi me sukses, False perndryshe</returns>
        public static bool ruajGridaTrupiNeSesion(HttpSessionState session, colGridaTrupi obj)
        {


            if (MySessionCache["GridaTrupi"] == null)
                MySessionCache.Add("GridaTrupi", obj);
            else
                MySessionCache["GridaTrupi"] = obj;
            return true;

        }

        /// <summary>
        /// merr raportin nga sesioni
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <returns></returns>
        public static T merrMyReportNgaSessioni<T>(HttpSessionState session)
        {
            if (session.IsNewSession)
            {
                //throw new mySessionNewException("Sessioni ka skaduar! Ju lutem logohuni perseri");
                clsFunksione.logout(session, true, "MbarimSessioni");
            }

            if (MySessionCache["MyReport"] != null)
                return (T)MySessionCache["MyReport"];
            else return default(T);

        }
        public static T merrMyReportNgaSessioni<T>(HttpSessionState session, String key)
        {
            if (session.IsNewSession)
            {
                //throw new mySessionNewException("Sessioni ka skaduar! Ju lutem logohuni perseri");
                clsFunksione.logout(session, true, "MbarimSessioni");
            }

            if (MySessionCache["MyReport" + key] != null)
                return (T)MySessionCache["MyReport" + key];
            else return default(T);

        }
        /// <summary>
        /// ruan raportin ne sesion
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <param name="obj">raporti</param>
        /// <returns>True nese ruajtja perfundoi me sukses, False perndryshe</returns>
        public static bool ruajMyReportNeSession<T>(HttpSessionState session, T obj)
        {

            if (MySessionCache["MyReport"] == null)
                MySessionCache.Add("MyReport", obj);
            else
                MySessionCache["MyReport"] = obj;
            return true;

        }
        /// <summary>
        /// ruan raportin ne sesion
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <param name="obj">raporti</param>
        /// <returns>True nese ruajtja perfundoi me sukses, False perndryshe</returns>
        public static bool ruajMyReportNeSession<T>(HttpSessionState session, String key, T obj)
        {

            if (MySessionCache["MyReport" + key] == null)
                MySessionCache.Add("MyReport" + key, obj);
            else
                MySessionCache["MyReport" + key] = obj;
            return true;

        }
        /// <summary>
        /// merr filter nga sesioni
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <returns></returns>
        public static string merrFilterNgaSessioni(HttpSessionState session)
        {
            if (session.IsNewSession)
            {
                //throw new mySessionNewException("Sessioni ka skaduar! Ju lutem logohuni perseri");
                clsFunksione.logout(session, true, "MbarimSessioni");
            }

            if (MySessionCache["Filtri"] == null)
                return null;
            else
                return MySessionCache["Filtri"].ToString();
        }

        /// <summary>
        /// ruan filtrin e grides ne sesion
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <param name="obj">filtri</param>
        /// <returns>True nese ruajtja perfundoi me sukses, False perndryshe</returns>
        public static bool ruajFilterNeSessioni(HttpSessionState session, Object obj)
        {
            if (MySessionCache["Filtri"] == null)
                MySessionCache.Add("Filtri", obj);
            else
                MySessionCache["Filtri"] = obj;
            return true;
        }

        /// <summary>
        /// ruan te pivot grida te dhenat
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <param name="pivotGridDataSource">datasource i pivotgrides</param>
        /// <returns>True nese ruajtja perfundoi me sukses, False perndryshe</returns>
        public static bool ruajTeDhenaPivotGrideNeSession(string keyforPivot, HttpSessionState session, DataView pivotGridDataSource)
        {
            MySessionCache["pivotGridDataSourceKond"] = keyforPivot;
            MySessionCache["pivotGridDataSource"] = pivotGridDataSource;
            return true;
        }

        /// <summary>
        /// merr nga sesioni te dhenat nga pivot grida
        /// </summary>
        /// <param name="keyforPivot">keyforPivot</param>
        /// <param name="session">sesioni</param>
        /// <param name="pivotGridDataSource">data source i pivot grides</param>
        /// <returns></returns>
        public static bool merrTeDhenaPivotGrideNgaSession(string keyforPivot, HttpSessionState session, out DataView pivotGridDataSource)
        {

            try
            {
                if (MySessionCache["pivotGridDataSourceKond"] != null && keyforPivot.CompareTo(MySessionCache["pivotGridDataSourceKond"]) == 0)
                {
                    pivotGridDataSource = (DataView)MySessionCache["pivotGridDataSource"];
                    return true;
                }
                else
                {
                    MySessionCache["pivotGridDataSource"] = pivotGridDataSource = null;
                    return false;
                }
            }
            catch (Exception)
            {
                pivotGridDataSource = null;
                return false;
            }

        }

        public static void resetTeDhenaPivotGrideNgaSession(HttpSessionState session)
        {
            MySessionCache["pivotGridDataSourceKond"] = "";
        }

        /// <summary>
        /// merr koleksionin e artikujve nga sesioni
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <returns></returns>
        public static DataTable merrColArtikullNgaSesioni(HttpSessionState session)
        {
            if (session.IsNewSession)
            {
                //throw new mySessionNewException("Sessioni ka skaduar! Ju lutem logohuni perseri");
                clsFunksione.logout(session, true, "MbarimSessioni");
            }

            if (MySessionCache["colArtikull"] != null)
                return (DataTable)MySessionCache["colArtikull"];
            else return null;
        }

        /// <summary>
        /// ruan njekoleksion me artikuj ne sesion "colArtikull"
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <param name="obj">koleksioni me artikuj</param>
        /// <returns>True nese ruajtja perfundoi me sukses, False perndryshe</returns>
        public static bool ruajColArtikullNeSesion(HttpSessionState session, DataTable obj)
        {

            if (MySessionCache["colArtikull"] == null)
                MySessionCache.Add("colArtikull", obj);
            else
                MySessionCache["colArtikull"] = obj;
            return true;
        }

        /// <summary>
        /// merr koleksionin me artikujt nga sesioni
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <returns></returns>
        public static DataTable merrColArtikull2NgaSesioni(HttpSessionState session)
        {
            if (session.IsNewSession)
            {
                //throw new mySessionNewException("Sessioni ka skaduar! Ju lutem logohuni perseri");
                clsFunksione.logout(session, true, "MbarimSessioni");
            }

            if (MySessionCache["colArtikull2"] != null)
                return (DataTable)MySessionCache["colArtikull2"];
            else return null;

        }

        /// <summary>
        /// ruan nje koleksion me artikuj ne sesion "colArtikull2"
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <param name="obj">koleksioni</param>
        /// <returns>True nese ruajtja perfundoi me sukses, False perndryshe</returns>
        public static bool ruajColArtikull2NeSesion(HttpSessionState session, DataTable obj)
        {

            if (MySessionCache["colArtikull2"] == null)
                MySessionCache.Add("colArtikull2", obj);
            else
                MySessionCache["colArtikull2"] = obj;
            return true;
        }

        public static bool ruajObjectNeSesion(HttpSessionState session, object obj)
        {
            return ruajObjectNeSesion(session, obj, "Object");
        }
        public static bool ruajObjectNeSesion(HttpSessionState session, object obj, string name)
        {

            if (name == "")
                name = "Object";
            if (MySessionCache[name] == null)
                MySessionCache.Add(name, obj);
            else
                MySessionCache[name] = obj;
            return true;

        }
        
        public static void RuajNeSession<T>(HttpSessionState session, T obj, string name)
        {
            MySessionCache[name] = obj;
        }
        public static void RuajNeSession<T>(HttpSessionState session, T obj, string name, string guidString)
        {
            RuajNeSession(session, obj, $"{name}_{guidString}");
        }
        public static void RuajNeSession<T>(string key, string guid, T obj)
        {
            MySessionCache[ComposedGuidName(key, guid), true] = obj;
        }

        public static void hiqObjectNeSesion(HttpSessionState session, string name)
        {
            if (name == "")
                name = "Object";
            if (MySessionCache[name] != null)
                MySessionCache.Remove(name);
            
        }
        public static void hiqObjectNeSesion(HttpSessionState session, string name, string guidString)
        {
            hiqObjectNeSesion(session, $"{name}_{guidString}");
        }
        public static bool ruajdtfillNeSesion(HttpSessionState session, object obj)
        {
            if (MySessionCache["dtfillimi"] == null)
                MySessionCache.Add("dtfillimi", obj);
            else
                MySessionCache["dtfillimi"] = obj;
            return true;
        }
        public static bool ruajdtmbarNeSesion(HttpSessionState session, object obj)
        {

            if (MySessionCache["dtmbarimi"] == null)
                MySessionCache.Add("dtmbarimi", obj);
            else
                MySessionCache["dtmbarimi"] = obj;
            return true;
        }
        public static bool ruajdtfillexeNeSesion(HttpSessionState session, object obj)
        {

            if (MySessionCache["dtfillimiexe"] == null)
                MySessionCache.Add("dtfillimiexe", obj);
            else
                MySessionCache["dtfillimiexe"] = obj;
            return true;

        }
        public static bool ruajdtmbarexeNeSesion(HttpSessionState session, object obj)
        {


            if (MySessionCache["dtmbarimiexe"] == null)
                MySessionCache.Add("dtmbarimiexe", obj);
            else
                MySessionCache["dtmbarimiexe"] = obj;
            return true;

        }
        public static bool ruajradNeSesion(HttpSessionState session, object obj)
        {


            if (MySessionCache["radDtDok"] == null)
                MySessionCache.Add("radDtDok", obj);
            else
                MySessionCache["radDtDok"] = obj;
            return true;

        }
        public static bool ruajrad2NeSesion(HttpSessionState session, object obj)
        {


            if (MySessionCache["radDtDok1"] == null)
                MySessionCache.Add("radDtDok1", obj);
            else
                MySessionCache["radDtDok1"] = obj;
            return true;

        }

        public static bool ruajObjectModNeSesion(HttpSessionState session, object obj)
        {

            if (MySessionCache["ObjectMod"] == null)
                MySessionCache.Add("ObjectMod", obj);
            else
                MySessionCache["ObjectMod"] = obj;
            return true;

        }

        public static object merrObjectNgaSesioni(HttpSessionState session)
        {
            if (session.IsNewSession)
            {
                //throw new mySessionNewException("Sessioni ka skaduar! Ju lutem logohuni perseri");
                clsFunksione.logout(session, true, "MbarimSessioni");
            }

            if (MySessionCache["Object"] != null)
                return MySessionCache["Object"];
            else return null;

        }
        public static object merrObjectNgaSesioni(HttpSessionState session, string name)
        {

            if (session.IsNewSession)
            {
                //throw new mySessionNewException("Sessioni ka skaduar! Ju lutem logohuni perseri");
                clsFunksione.logout(session, true, "MbarimSessioni");
            }

            if (name == "")
                name = "Object";
            if (MySessionCache[name] != null)
                return MySessionCache[name];
            else return null;

        }
        public static T MerrNgaSession<T>(HttpSessionState session, string name)
        {

            if (session == null) throw new NullReferenceException("Sessioni eshte null");
            return (T)MySessionCache[name];
        }
        public static T MerrNgaSession<T>(HttpSessionState session, string name, string guidString)
        {
            return MerrNgaSession<T>(session, ComposedGuidName(name, guidString));
        }

        public static object[] merrParametratERaportitNgaSesioni(HttpSessionState session, String guidString)
        {
            if (session.IsNewSession)
            {
                clsFunksione.logout(session, true, "MbarimSessioni");
            }

            if (MySessionCache["FiltraRap" + guidString] != null)
                return (object[])MySessionCache["FiltraRap" + guidString];
            else return null;

        }

        public static bool ruajParametratERaportit(HttpSessionState session, Object[] obj, String guidString)
        {

            if (MySessionCache["FiltraRap" + guidString] == null)
                MySessionCache.Add("FiltraRap" + guidString, obj);
            else
                MySessionCache["FiltraRap" + guidString] = obj;
            return true;

        }
        public static bool ruajParametraRaporti(HttpSessionState session, SqlParameter[] parametrat, String guidString)
        {

            if (MySessionCache["FiltraDefaultRap" + guidString] == null)
                MySessionCache.Add("FiltraDefaultRap" + guidString, parametrat);
            else
                MySessionCache["FiltraDefaultRap" + guidString] = parametrat;
            return true;

        }
        public static SqlParameter[] merrParametraRaporti(HttpSessionState session, String guidString)
        {
            if (session.IsNewSession)
            {
                clsFunksione.logout(session, true, "MbarimSessioni");
            }

            if (MySessionCache["FiltraDefaultRap" + guidString] != null)
                return (SqlParameter[])MySessionCache["FiltraDefaultRap" + guidString];
            else return null;

        }
        public static object merrdtfillNgaSesioni(HttpSessionState session)
        {
            if (session.IsNewSession)
            {
                //throw new mySessionNewException("Sessioni ka skaduar! Ju lutem logohuni perseri");
                clsFunksione.logout(session, true, "MbarimSessioni");
            }

            if (MySessionCache["dtfillimi"] != null)
                return MySessionCache["dtfillimi"];
            else return null;

        }
        public static object merrdtmbarNgaSesioni(HttpSessionState session)
        {
            if (session.IsNewSession)
            {
                //throw new mySessionNewException("Sessioni ka skaduar! Ju lutem logohuni perseri");
                clsFunksione.logout(session, true, "MbarimSessioni");
            }

            if (MySessionCache["dtmbarimi"] != null)
                return MySessionCache["dtmbarimi"];
            else return null;

        }
        public static object merrdtfillexeNgaSesioni(HttpSessionState session)
        {
            if (session.IsNewSession)
            {
                //throw new mySessionNewException("Sessioni ka skaduar! Ju lutem logohuni perseri");
                clsFunksione.logout(session, true, "MbarimSessioni");
            }

            if (MySessionCache["dtfillimiexe"] != null)
                return MySessionCache["dtfillimiexe"];
            else return null;

        }
        public static object merrdtmbarexeNgaSesioni(HttpSessionState session)
        {
            if (session.IsNewSession)
            {
                //throw new mySessionNewException("Sessioni ka skaduar! Ju lutem logohuni perseri");
                clsFunksione.logout(session, true, "MbarimSessioni");
            }

            if (MySessionCache["dtmbarimiexe"] != null)
                return MySessionCache["dtmbarimiexe"];
            else return null;

        }
        public static object merrradNgaSesioni(HttpSessionState session)
        {
            if (session.IsNewSession)
            {
                //throw new mySessionNewException("Sessioni ka skaduar! Ju lutem logohuni perseri");
                clsFunksione.logout(session, true, "MbarimSessioni");
            }

            if (MySessionCache["radDtDok"] != null)
                return MySessionCache["radDtDok"];
            else return null;

        }
        public static object merrrad2NgaSesioni(HttpSessionState session)
        {
            if (session.IsNewSession)
            {
                //throw new mySessionNewException("Sessioni ka skaduar! Ju lutem logohuni perseri");
                clsFunksione.logout(session, true, "MbarimSessioni");
            }

            if (MySessionCache["radDtDok1"] != null)
                return MySessionCache["radDtDok1"];
            else return null;

        }
        public static object merrObjectModNgaSesioni(HttpSessionState session)
        {
            if (session.IsNewSession)
            {
                //throw new mySessionNewException("Sessioni ka skaduar! Ju lutem logohuni perseri");
                clsFunksione.logout(session, true, "MbarimSessioni");
            }

            if (MySessionCache["ObjectMod"] != null)
                return MySessionCache["ObjectMod"];
            else return null;

        }
        /// <summary>
        /// merr ndermarrjen e re nga sesioni
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <returns></returns>
        public static clsNdermarrje merrNdermarjeReNgaSesioni(HttpSessionState session)
        {
            if (session.IsNewSession)
            {
                //throw new mySessionNewException("Sessioni ka skaduar! Ju lutem logohuni perseri");
                clsFunksione.logout(session, true, "MbarimSessioni");
            }

            if (MySessionCache["ndermarjere"] != null)
                return (clsNdermarrje)MySessionCache["ndermarjere"];
            else return null;
        }

        /// <summary>
        /// ruan ndermarrjen e re ne sesion
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <param name="obj">ndermarrja e re qe po krijohet</param>
        /// <returns>True nese ruajtja perfundoi me sukses, False perndryshe</returns>
        public static bool ruajNdermarjeReNeSesion(HttpSessionState session, clsNdermarrje obj)
        {

            if (MySessionCache["ndermarjere"] == null)
                MySessionCache.Add("ndermarjere", obj);
            else
                MySessionCache["ndermarjere"] = obj;
            return true;
        }

        /// <summary>
        /// merr kpf2 nga sesioni
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <returns></returns>
        public static DataTable merrKPF2NgaSesioni(HttpSessionState session)
        {
            if (session.IsNewSession)
            {
                //throw new mySessionNewException("Sessioni ka skaduar! Ju lutem logohuni perseri");
                clsFunksione.logout(session, true, "MbarimSessioni");
            }

            if (MySessionCache["KPF2"] != null)
                return (DataTable)MySessionCache["KPF2"];
            else return null;
        }

        /// <summary>
        /// ruan kpf2 ne sesion, grida e dyte tek kpf
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <param name="dt">data table</param>
        /// <returns>True nese ruajtja perfundoi me sukses, False perndryshe</returns>
        public static bool ruajKPF2NeSesion(HttpSessionState session, DataTable dt)
        {

            if (MySessionCache["KPF2"] == null)
                MySessionCache.Add("KPF2", dt);
            else
                MySessionCache["KPF2"] = dt;
            return true;
        }

        /// <summary>
        /// merr nga sesioni kpf3
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <returns></returns>
        public static DataTable merrKPF3NgaSesioni(HttpSessionState session)
        {
            if (session.IsNewSession)
            {
                //throw new mySessionNewException("Sessioni ka skaduar! Ju lutem logohuni perseri");
                clsFunksione.logout(session, true, "MbarimSessioni");
            }

            if (MySessionCache["KPF3"] != null)
                return (DataTable)MySessionCache["KPF3"];
            else return null;

        }

        /// <summary>
        /// ruan kpf3 ne sesion, grida e trete tek kpf
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <param name="obj">data table</param>
        /// <returns>True nese ruajtja perfundoi me sukses, False perndryshe</returns>
        public static bool ruajKPF3NeSesion(HttpSessionState session, DataTable obj)
        {
            if (MySessionCache["KPF3"] == null)
                MySessionCache.Add("KPF3", obj);
            else
                MySessionCache["KPF3"] = obj;
            return true;
        }

        /// <summary>
        /// merr nje data table me artikuj ne sesioni
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <returns></returns>
        public static DataTable merrArtProdhNgaSesioni(HttpSessionState session)
        {
            if (session.IsNewSession)
            {
                //throw new mySessionNewException("Sessioni ka skaduar! Ju lutem logohuni perseri");
                clsFunksione.logout(session, true, "MbarimSessioni");
            }

            if (MySessionCache["artprodh"] != null)
                return (DataTable)MySessionCache["artprodh"];
            else return null;

        }

        /// <summary>
        /// ruan nje data table me artikuj ne sesion
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <param name="dt">data table me artikuj qe do ruhet ne sesion</param>
        /// <returns>True nese ruajtja perfundoi me sukses, False perndryshe</returns>
        public static bool ruajArtProdhNeSesion(HttpSessionState session, DataTable dt)
        {

            if (MySessionCache["artprodh"] == null)
                MySessionCache.Add("artprodh", dt);
            else
                MySessionCache["artprodh"] = dt;
            return true;
        }

        /// <summary>
        /// merr koleksionin e klient/furnitoreve te ruajtur ne sesion
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <returns></returns>
        public static colKlienteFurnitore merrColKFNgaSesioni(HttpSessionState session)
        {

            if (session.IsNewSession)
            {
                //throw new mySessionNewException("Sessioni ka skaduar! Ju lutem logohuni perseri");
                clsFunksione.logout(session, true, "MbarimSessioni");
            }

            if (MySessionCache["colkf"] != null)
                return (colKlienteFurnitore)MySessionCache["colkf"];
            else return null;


        }

        /// <summary>
        /// ruan nje koleksion te klient/furnitoreve ne sesion
        /// </summary>
        /// <param name="session">sesion</param>
        /// <param name="col">koleksioni i kl/furn</param>
        /// <returns>True nese ruajtja perfundoi me sukses, False perndryshe</returns>
        public static bool ruajColKFNeSesion(HttpSessionState session, colKlienteFurnitore col)
        {

            if (MySessionCache["colkf"] == null)
                MySessionCache.Add("colkf", col);
            else
                MySessionCache["colkf"] = col;
            return true;

        }

        /// <summary>
        /// merr koleksionin e klient/furnitoreve te ruajtur ne sesion
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <returns></returns>
        public static colKlienteFurnitore merrColKFZgjedhurNgaSesioni(HttpSessionState session)
        {
            if (session.IsNewSession)
            {
                //throw new mySessionNewException("Sessioni ka skaduar! Ju lutem logohuni perseri");
                clsFunksione.logout(session, true, "MbarimSessioni");
            }

            if (MySessionCache["colkfZgjedhur"] != null)
                return (colKlienteFurnitore)MySessionCache["colkfZgjedhur"];
            else return null;

        }

        /// <summary>
        /// ruan nje koleksion te klient/furnitoreve ne sesion
        /// </summary>
        /// <param name="session">sesion</param>
        /// <param name="col">koleksioni i kl/furn</param>
        /// <returns>True nese ruajtja perfundoi me sukses, False perndryshe</returns>
        public static bool ruajColKFZgjedhurNeSesion(HttpSessionState session, colKlienteFurnitore col)
        {
            if (MySessionCache["colkfZgjedhur"] == null)
                MySessionCache.Add("colkfZgjedhur", col);
            else
                MySessionCache["colkfZgjedhur"] = col;
            return true;

        }

        /// <summary>
        /// fshin pivotGridDataSource nga sesioni
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <returns></returns>
        public static bool fshiTeDhenaPivotGridNgaSessioni(HttpSessionState session)
        {
            try
            {
                MySessionCache.Remove("pivotGridDataSource");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// fshin raportin nga sesioni
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <returns></returns>
        public static bool fshiRaportNgaSessioni(HttpSessionState session)
        {
            try
            {
                MySessionCache.Remove("report");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Kjo metode zbraz sessionin nga grida
        /// </summary>
        /// <param name="session">Sessioni qe duhet te zbrazet nga grida</param>
        /// <returns>True nese zbrazja perfundoi me sukses, False perndryshe</returns>
        public static bool fshiGridNgaSessioniLupa(HttpSessionState session)
        {
            try
            {
                MySessionCache.Remove("gridDataSourceLupa");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// fshin griden nga sesioni
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <returns></returns>
        public static bool fshiGridNgaSessioni(HttpSessionState session)
        {
            try
            {
                MySessionCache.Remove("gridDataSource");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// kthen nese perdoruesi eshte i loguar ose jo ne sistem
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <returns></returns>
        public static bool isLogedIn(HttpSessionState session)
        {
            if (session.IsNewSession)
            {
                clsFunksione.logout(session, true, "MbarimSessioni");
            }
            var isLoggedIn = IMBUtils.Types.Converter.MerrVlereOseDefault<string>(MySessionCache.Get<string>("LoggedIn", false));
            return "Yes".Equals(isLoggedIn);
        }

        /// <summary>
        /// ruan neser perdoruesi eshte i loguar apo jo ne sesion
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <param name="loguar">yes, ose no ne varesi te gjendjes se logimit te perdoruesit</param>
        /// <returns>True nese ruajtja perfundoi me sukses, False perndryshe</returns>
        public static bool ruajIsLoggedIn(HttpSessionState session, string loguar)
        {

            GlobalCacheManager.GetSessionCacheByKey(session.SessionID).Set("LoggedIn", loguar, false, false);
            return true;

        }


        /// <summary>
        /// kthen vleren myCallback nga sesioni
        /// </summary>
        /// <param name="session">sesion</param>
        /// <returns></returns>
        public static bool merrMyCallbackNgaSesioni(HttpSessionState session)
        {
            if (session.IsNewSession)
            {
                clsFunksione.logout(session, true, "MbarimSessioni");
            }

            if (MySessionCache["MyCallback"] == null)
                return false;
            else return Convert.ToBoolean(MySessionCache["MyCallback"]);

        }
        public static bool merrMyCallbackNgaSesioni(HttpSessionState session, String key)
        {
            if (session.IsNewSession)
            {
                clsFunksione.logout(session, true, "MbarimSessioni");
            }

            if (MySessionCache["MyCallback" + key] == null)
                return false;
            else return Convert.ToBoolean(MySessionCache["MyCallback" + key]);

        }
        /// <summary>
        /// ruan mycallback ne sesion
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <param name="mycallback">vlera true ose false qe do ruhet ne sesion</param>
        /// <returns>True nese ruajtja perfundoi me sukses, False perndryshe</returns>
        public static bool ruajMyCallbackNeSesion(HttpSessionState session, bool mycallback)
        {

            if (MySessionCache["MyCallback"] == null)
                MySessionCache.Add("MyCallback", mycallback);
            else
                MySessionCache["MyCallback"] = mycallback;
            return true;

        }
        public static bool ruajMyCallbackNeSesion(HttpSessionState session, String key, bool mycallback)
        {

            if (MySessionCache["MyCallback" + key] == null)
                MySessionCache.Add("MyCallback" + key, mycallback);
            else
                MySessionCache["MyCallback" + key] = mycallback;
            return true;

        }
        /// <summary>
        /// kthen vleren SubRaportCallback nga sesioni
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <returns></returns>
        public static bool merrSubRaportCallbackNgaSesioni(HttpSessionState session, String key)
        {
            if (session.IsNewSession)
            {
                clsFunksione.logout(session, true, "MbarimSessioni");
            }

            if (MySessionCache["SubRaportCallback" + key] == null) return false;
            else return Convert.ToBoolean(MySessionCache["SubRaportCallback" + key]);


        }

        /// <summary>
        /// ruan ne sesion nese SubRaportCallback eshte true ose false
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <param name="subraport">vlera true ose false qe ruhet ne sesion</param>
        /// <returns>True nese ruajtja perfundoi me sukses, False perndryshe</returns>
        public static bool ruajSubRaportCallbackNeSesion(HttpSessionState session, String key, bool subraport)
        {

            if (MySessionCache["SubRaportCallback" + key] == null)
                MySessionCache.Add("SubRaportCallback" + key, subraport);
            else
                MySessionCache["SubRaportCallback" + key] = subraport;
            return true;

        }

        /// <summary>
        /// kthen kodin e ndermarrjes nga sesioni
        /// </summary>
        /// <param name="Session">sesioni</param>
        /// <returns></returns>
        public static string ktheKodNdermarrje(HttpSessionState session)
        {
            if (session.IsNewSession)
            {
                clsFunksione.logout(session, true, "MbarimSessioni");
            }

            if (MySessionCache["KodiNdermarrjes"] == null)
                return null;
            else
                return MySessionCache["KodiNdermarrjes"].ToString();

        }

        /// <summary>
        ///Ruan kodin e ndermarrjes ne sesion
        /// </summary>
        /// <param name="kodNderm">kodi i ndermarrjes</param>
        /// <param name="session">sesioni</param>
        /// <returns>True nese ruajtja perfundoi me sukses, False perndryshe</returns>
        public static bool ruajKodNdermarrje(string kodNderm, HttpSessionState session)
        {
            if (MySessionCache["KodiNdermarrjes"] == null)
                MySessionCache.Add("KodiNdermarrjes", kodNderm);
            else MySessionCache["KodiNdermarrjes"] = kodNderm;
            return true;
        }

        /// <summary>
        /// kthen vleren e fshi nga sesioni
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <returns></returns>
        public static string merrFshiNgaSesioni(HttpSessionState session)
        {
            if (session.IsNewSession)
            {
                clsFunksione.logout(session, true, "MbarimSessioni");
            }

            if (MySessionCache["Fshi"] == null) return null;
            else return MySessionCache["Fshi"].ToString();
        }

        /// <summary>
        ///Ruan vleren e fshi ne sesion
        /// </summary>
        /// <param name="session"></param>
        /// <param name="fshi">stringu qe do ruhet ne sesion</param>
        /// <returns>True nese ruajtja perfundoi me sukses, False perndryshe</returns>
        public static bool ruajFshiNeSesion(HttpSessionState session, string fshi)
        {
            if (MySessionCache["Fshi"] == null)
                MySessionCache.Add("Fshi", fshi);
            else
                MySessionCache["Fshi"] = fshi;
            return true;
        }

        /// <summary>
        /// kthen mesazhin e ruajtur ne sesion
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <returns></returns>
        public static string merrMesazhNgaSesioni(HttpSessionState session)
        {
            if (session.IsNewSession)
            {
                clsFunksione.logout(session, true, "MbarimSessioni");
            }
            if (MySessionCache["mesazh"] == null)
                return null;
            else
                return MySessionCache["mesazh"].ToString();

        }
        [Obsolete("Duhet te ktheje clsMesazh", false)]
        public static object hiqMesazhNgaSesioni(HttpSessionState session)
        {
            if (session.IsNewSession)
            {
                clsFunksione.logout(session, true, "MbarimSessioni");
            }

            if (MySessionCache["mesazh"] == null)
                return null;
            else
            {
                string mesazhi = MySessionCache["mesazh"].ToString();
                MySessionCache.Remove("mesazh");
                return mesazhi;
            }

        }
        public static bool shtoMesazhNeSession(HttpSessionState session, clsMesazh mesazhi)
        {
            if (MySessionCache["mesazh"] == null)
                MySessionCache.Add("mesazh", mesazhi);
            else
                MySessionCache["mesazh"] = mesazhi;
            return true;
        }

        public static bool shtoMesazhNeSession(HttpSessionState session, clsMesazh mesazhi, string guidString)
        {
            if (MySessionCache[$"mesazh{guidString}"] == null)
                MySessionCache.Add($"mesazh{guidString}", mesazhi);
            else
                MySessionCache[$"mesazh{guidString}"] = mesazhi;
            return true;
        }
        public static object hiqMesazhNgaSesioni(HttpSessionState session, string guidString)
        {
            if (session.IsNewSession)
            {
                clsFunksione.logout(session, true, "MbarimSessioni");
            }

            if (MySessionCache[$"mesazh{guidString}"] == null)
                return null;
            else
            {
                clsMesazh mesazhi = MySessionCache[$"mesazh{guidString}"] as clsMesazh;
                MySessionCache.Remove($"mesazh{guidString}");
                return mesazhi;
            }
        }

        //TODO NEDJAN te zevendesohet ne te gjithe vendet funksioni
        [Obsolete("Duhet te perdoret shtoMesazhNeSession ", false)]
        public static bool ruajMesazhNeSesion(HttpSessionState session, string mesazhi)
        {
            if (MySessionCache["mesazh"] == null)
                MySessionCache.Add("mesazh", mesazhi);
            else
                MySessionCache["mesazh"] = mesazhi;
            return true;
        }

        /// <summary>
        /// kthen emrin dhe mbiemrin e perdoruesit nga sesioni
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <returns></returns>
        public static string merrEmerPerdoruesiNgaSesioni(HttpSessionState session)
        {
            var myCache = GlobalCacheManager.GetSessionCacheByKey(session.SessionID);
            if (myCache["perdoruesi"] == null)
                return null;
            else
                return myCache["perdoruesi"].ToString();
        }

        /// <summary>
        ///Ruan emrin dhe mbiemrin e perdoruesit ne sesion
        /// </summary>
        ///<param name="session">sesioni</param>
        /// <param name="emer">emri&mbiemri i perdoruesit</param>
        /// <returns>True nese ruajtja perfundoi me sukses, False perndryshe</returns>
        public static bool ruajEmerPerdoruesiNeSesion(HttpSessionState session, string emer)
        {
            if (GlobalCacheManager.GetSessionCacheByKey(session.SessionID)["perdoruesi"] == null)
                GlobalCacheManager.GetSessionCacheByKey(session.SessionID).Add("perdoruesi", emer);
            else
                GlobalCacheManager.GetSessionCacheByKey(session.SessionID)["perdoruesi"] = emer;
            return true;
        }

     

        /// <summary>
        /// Merr periudhen kontabel nga sesioni
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <returns></returns>
        public static clsPeriudhaKontabel merrPeriudheKontabel(HttpSessionState session)
        {
            if (session.IsNewSession)
                clsFunksione.logout(session, true, "MbarimSessioni");
            if (MySessionCache["oPeriudhaAktuale"] == null)
                return null;
            else
                return (clsPeriudhaKontabel)MySessionCache["oPeriudhaAktuale"];
        }
        public static clsPeriudhaKontabel merrPeriudheKontabel(string guidString, HttpSessionState session)
        {
            string key = guidString + "oPeriudhaAktuale";
            if (session.IsNewSession)
                clsFunksione.logout(session, true, "MbarimSessioni");
            if (MySessionCache[key] == null)
                return null;
            else
                return (clsPeriudhaKontabel)MySessionCache[key];
        }
        /// <summary>
        /// Ruan periudhen kontabel ne sesion
        /// </summary>
        /// <param name="periudha">periudha</param>
        /// <param name="session">sesioni</param>
        /// <returns>True nese ruajtja perfundoi me sukses, False perndryshe</returns>

        public static bool ruajPeriudheKontabelNeSesion(clsPeriudhaKontabel periudha, HttpSessionState session)
        {
            MySessionCache["oPeriudhaAktuale"] = periudha;
            return true;
        }
        public static bool ruajPeriudheKontabelNeSesion(clsPeriudhaKontabel periudha, HttpSessionState session, string scopeId)
        {
            if (string.IsNullOrWhiteSpace(scopeId))
                return false;

            SetTemporaryScopeId(scopeId);
            ruajPeriudheKontabelNeSesion(periudha, session);
            RemoveTemporaryScopeId();
            return true;

        }
        public static bool ruajPeriudheKontabelNeSesion(string guidString, clsPeriudhaKontabel periudha, HttpSessionState session)
        {
            string key = guidString + "oPeriudhaAktuale";
            if (MySessionCache[key] == null)
                MySessionCache.Add(key, periudha);
            MySessionCache[key] = periudha;
            return true;
        }
        /// <summary>
        /// Merr url e imazhit te backgroundit.
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <returns></returns>
        [Obsolete("nuk perdoret me", true)]
        public static string merrBackgroundPath(HttpSessionState session)
        {
            if (session.IsNewSession)
            {
                clsFunksione.logout(session, true, "MbarimSessioni");
            }

            if (MySessionCache["BackgroundPath"] == null)
                return null;
            else
                return MySessionCache["BackgroundPath"].ToString();

        }

        /// <summary>
        /// Ruan url e imazhit te backgroundit ne sesion.
        /// </summary>
        /// <param name="backpath">url e imazhit te backgroundit</param>
        /// <param name="session">sesioni</param>
        /// <returns>True nese ruajtja perfundoi me sukses, False perndryshe</returns>
        [Obsolete("nuk perdoret me", true)]
        public static bool ruajBackgroundPathNeSesion(string backpath, HttpSessionState session)
        {

            if (MySessionCache["BackgroundPath"] == null)
                MySessionCache.Add("BackgroundPath", backpath);
            else
                MySessionCache["BackgroundPath"] = backpath;
            return true;
        }

        /// <summary>
        /// kthen id-ne e ndermarje vitit per ndermarjen dhe vitin qe jemi loguar (idndermviti)
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <returns>nje integer qe permban id e ndermarje vitit</returns>
        public static int ktheNdermarrjeVit(HttpSessionState session)
        {
            if (session.IsNewSession)
            {
                //throw new mySessionNewException("Sessioni ka skaduar, Ju lutem logohuni perseri");
                clsFunksione.logout(session, true, "MbarimSessioni");
            }
            int idNdermViti = -1;
            if (MySessionCache["IdNdermViti"] == null ? true : !int.TryParse(GetSessionCacheByKey(session.SessionID)["IdNdermViti"].ToString(), out idNdermViti))
                ImbLogger.Error("mySessionObjects metoda ktheNdermarrjeVit" + Environment.NewLine + "ERROR: Gabim gjate konvertimit te id - se ndermarrjeviti nga session");
            return idNdermViti;
        }

        /// <summary>
        /// ruan idnderviti ne sesion
        /// </summary>
        /// <param name="idNdervit">idnderviti</param>
        /// <param name="session">Sesioni</param>
        /// <returns>True nese ruajtja perfundoi me sukses, False perndryshe</returns>
        public static bool ruajIdNdermarrjeVit(string idNdervit, HttpSessionState session)
        {
            if (MySessionCache["IdNdermViti"] == null)
                MySessionCache.Add("IdNdermViti", idNdervit);
            else
                MySessionCache["IdNdermViti"] = idNdervit;
            return true;
        }

        /// <summary>
        /// ruan ne sesion id Ndermarrjen Raportuese te ndermarrjes se loguar
        /// </summary>
        /// <param name="session"></param>
        /// <param name="idNdermRaportuese"></param>
        /// <returns></returns>
        public static bool ruajNdermRaportuese(HttpSessionState session, int idNdermRaportuese)
        {
            if (MySessionCache["idNdermRaportuese"] == null)
                MySessionCache.Add("idNdermRaportuese", idNdermRaportuese);
            else
                MySessionCache["idNdermRaportuese"] = idNdermRaportuese;
            return true;
        }

        /// <summary>
        /// merr nga sesioni id Ndermarrjen Raportuese te ndermarrjes se loguar
        /// </summary>
        /// <param name="sesion"></param>
        /// <returns></returns>
        public static int ktheNdermRaportuese(HttpSessionState sesion)
        {
            if (sesion.IsNewSession)
                clsFunksione.logout(sesion, true, "MbarimSessioni");
            int idNdermRaportuese = 0;
            if (MySessionCache["idNdermRaportuese"] == null || !int.TryParse(MySessionCache["idNdermRaportuese"].ToString(), out idNdermRaportuese))
                ImbLogger.Error("mySessionObjects metoda ktheNdermRaportuese" + Environment.NewLine + "ERROR: Gabim gjate leximit te idNdermRaportuese nga session");
            return idNdermRaportuese;
        }

        /// <summary>
        /// ruan formatin e sasise ne session
        /// </summary>
        /// <param name="formatsasia">formati i sasise</param>
        /// <param name="session">sesioni</param>
        /// <returns>true ose false nqs ruajta ka ndodhur me sukses</returns>
        public static bool ruajFormatSasia(int formatsasia, HttpSessionState session)
        {

            if (MySessionCache["formatsasia"] == null)
                MySessionCache.Add("formatsasia", formatsasia);
            else
                MySessionCache["formatsasia"] = formatsasia;
            return true;

        }

        public static bool ruajFormatNr(object[] formatnr, HttpSessionState session)
        {

            if (MySessionCache["formatnr"] == null)
                MySessionCache.Add("formatnr", formatnr);
            else
                MySessionCache["formatnr"] = formatnr;
            return true;

        }

        /// <summary>
        /// ruan formatin e vleftes ne session
        /// </summary>
        /// <param name="formatvlefta">formati i vleftes</param>
        /// <param name="session">sesioni</param>
        /// <returns>true ose false nqs ruajta ka ndodhur me sukses</returns>
        public static bool ruajFormatVlefta(int formatvlefta, HttpSessionState session)
        {

            if (MySessionCache["formatVlefta"] == null)
                MySessionCache.Add("formatVlefta", formatvlefta);
            else
                MySessionCache["formatVlefta"] = formatvlefta;
            return true;

        }

        /// <summary>
        /// merr formatin e sasise nga session
        /// </summary>
        /// <param name="sesion">sesioni</param>
        /// <returns>kthen formatin e sasise</returns>
        public static int merrFormatSasiaSesioni(HttpSessionState sesion)
        {
            if (sesion.IsNewSession)
                //throw new mySessionNewException("Sessioni ka skaduar, Ju lutem logohuni perseri");
                clsFunksione.logout(sesion, true, "MbarimSessioni");
            int formatsasia = 0;
            if (MySessionCache["formatsasia"] == null || !int.TryParse(MySessionCache["formatsasia"].ToString(), out formatsasia))
                ImbLogger.Error("mySessionObjects metoda merrFormatSasiaSesioni" + Environment.NewLine + "ERROR: Gabim gjate leximit te id-se se format sasia nga session");
            return formatsasia;
        }

        public static object[] merrFormatNRSesioni(HttpSessionState sesion)
        {
            if (sesion.IsNewSession)
                //throw new mySessionNewException("Sessioni ka skaduar, Ju lutem logohuni perseri");
                clsFunksione.logout(sesion, true, "MbarimSessioni");
            object[] format = (object[])MySessionCache["formatnr"];
            return format;
        }

        /// <summary>
        /// merr formatin e vleftes nga session
        /// </summary>
        /// <param name="sesion">sesioni</param>
        /// <returns>kthen formatin e vleftes</returns>
        public static int merrFormatVleftaSesioni(HttpSessionState sesion)
        {
            if (sesion.IsNewSession)
                //throw new mySessionNewException("Sessioni ka skaduar, Ju lutem logohuni perseri");
                clsFunksione.logout(sesion, true, "MbarimSessioni");
            int formatvlefta = 0;
            if (MySessionCache["formatVlefta"] == null || !int.TryParse(MySessionCache["formatVlefta"].ToString(), out formatvlefta))
                ImbLogger.Error("mySessionObjects metoda merrFormatVleftaSesioni" + Environment.NewLine + "ERROR: Gabim gjate leximit te id-se se format sasia nga session");
            return formatvlefta;
        }

        /// <summary>
        /// kthen idNdermarrjen per kete sesion
        /// </summary>
        /// <param name="sesion">Sesioni</param>
        /// <returns></returns>
        public static int merrIdNdermarrjeSesioni(HttpSessionState sesion)
        {
            if (sesion.IsNewSession)
                //throw new mySessionNewException("Sessioni ka skaduar, Ju lutem logohuni perseri");
                clsFunksione.logout(sesion, true, "MbarimSessioni");
            int idNdermarrje = 0;
                if (MySessionCache["IdNdermarrjes"] == null || !int.TryParse(MySessionCache["IdNdermarrjes"].ToString(), out idNdermarrje))
                    ImbLogger.LogTrace("mySessionObjects metoda merrIdNdermarrjeSesioni" + Environment.NewLine + "ERROR: Gabim gjate leximit te id-se se ndermarrjes korente nga session");
                return idNdermarrje;
        }

        public static bool merrEshteMemeSesioni(HttpSessionState sesion)
        {
            if (sesion.IsNewSession)
                //throw new mySessionNewException("Sessioni ka skaduar, Ju lutem logohuni perseri");
                clsFunksione.logout(sesion, true, "MbarimSessioni");
            bool eshteMeme = false;
                if (MySessionCache["NdermarjeMeme"] == null || !bool.TryParse(MySessionCache["NdermarjeMeme"].ToString(), out eshteMeme))
                    ImbLogger.Error("mySessionObjects metoda merrEshteMemeSesioni" + Environment.NewLine + "ERROR: Gabim gjate leximit te ndermarjes meme nga session");
                return eshteMeme;
        }

        public static bool merrEshteOwnSesioni(HttpSessionState sesion)
        {
            if (sesion.IsNewSession)
                //throw new mySessionNewException("Sessioni ka skaduar, Ju lutem logohuni perseri");
                clsFunksione.logout(sesion, true, "MbarimSessioni");
            bool eshteOwn = false;
            if (MySessionCache["NdermarjeOwn"] == null || !bool.TryParse(MySessionCache["NdermarjeOwn"].ToString(), out eshteOwn))
                ImbLogger.Error("mySessionObjects metoda merrEshteOwnSesioni" + Environment.NewLine + "ERROR: Gabim gjate leximit te ndermarjes Own nga session");
            return eshteOwn;
        }

        public static bool merrRuajLog(HttpSessionState sesion)
        {
            if (sesion.IsNewSession)
                //throw new mySessionNewException("Sessioni ka skaduar, Ju lutem logohuni perseri");
                clsFunksione.logout(sesion, true, "MbarimSessioni");
            //bool ruajLog;
            //if (!bool.TryParse(MySessionCache["RuajLog"].ToString(), out ruajLog))
            //    throw new Exception("ERROR: Gabim gjate leximit te ndermarjes Own nga session");
            //return ruajLog;
            return Convert.ToBoolean(MySessionCache["RuajLog"]);
        }

        public static bool merrMosMerrNgaDb(HttpSessionState sesion)
        {
            if (sesion.IsNewSession)
                //throw new mySessionNewException("Sessioni ka skaduar, Ju lutem logohuni perseri");
                clsFunksione.logout(sesion, true, "MbarimSessioni");
            bool mosRuajngaDb = false;
            if (MySessionCache["MosMerrNgaDb"] == null || !bool.TryParse(MySessionCache["MosMerrNgaDb"].ToString(), out mosRuajngaDb))
                throw new MyException("ERROR: Gabim gjate leximit te mos ruaj nga db nga session");
            return mosRuajngaDb;
        }

        /// <summary>
        /// Ruan id e ndermarrjes ne sesion
        /// </summary>
        /// <param name="session">Sesioni</param>
        /// <param name="idNderm">id e ndemarrjes</param>
        /// <returns>True nese ruajtja perfundoi me sukses, False perndryshe</returns>
        public static bool ruajIdNdermarrjeNeSesion(HttpSessionState session, string idNderm)
        {
            if (MySessionCache["IdNdermarrjes"] == null)
                MySessionCache.Add("IdNdermarrjes", idNderm);
            else
                MySessionCache["IdNdermarrjes"] = idNderm;
            return true;
        }

        public static bool ruajEshteNdermarjeMemeNeSesion(HttpSessionState session, bool meme)
        {
            if (MySessionCache["NdermarjeMeme"] == null)
                MySessionCache.Add("NdermarjeMeme", meme);
            else
                MySessionCache["NdermarjeMeme"] = meme;
            return true;
        }

        public static bool ruajEshteNdermarjeOwnNeSesion(HttpSessionState session, bool own)
        {

            if (MySessionCache["NdermarjeOwn"] == null)
                MySessionCache.Add("NdermarjeOwn", own);
            else
                MySessionCache["NdermarjeOwn"] = own;
            return true;
        }

        public static bool ruajRuajLogNeSesion(HttpSessionState session, bool logu)
        {
            if (MySessionCache["RuajLog"] == null)
                MySessionCache.Add("RuajLog", logu);
            else
                MySessionCache["RuajLog"] = logu;
            return true;
        }

        public static bool ruajMosMerrNgaDbNeSesion(HttpSessionState session, bool mosmerrngadb)
        {
            if (MySessionCache["MosMerrNgaDb"] == null)
                MySessionCache.Add("MosMerrNgaDb", mosmerrngadb);
            else
                MySessionCache["MosMerrNgaDb"] = mosmerrngadb;
            return true;
        }

        /// <summary>
        /// Kthen Id e gjuhes se perdoruesit ne sesion
        /// </summary>
        /// <param name="Session">sesioni</param>
        /// <returns></returns>
        public static int ktheGjuhe(HttpSessionState session)
        {
         
           return IMBUtils.Types.Converter.MerrVlereOseDefault<int>( GetSessionCacheByKey(session.SessionID).Get<int>("gjuha",false));
         
        }


        /// <summary>
        /// Kthen Id e gjuhes se perdoruesit ne sesion
        /// </summary>
        /// <param name="Session">sesioni</param>
        /// <returns></returns>
        public static bool ruajGjuhe(HttpSessionState session, int idGjuha)
        {
            MySessionCache.Set("gjuha", idGjuha, false, false);
            return true;
        }
        public static bool ruajTerms(HttpSessionState session, bool terms)
        {
            MySessionCache.Set("Terms", terms, false, false);
            return true;
        }

        public static bool ktheTerms(string sessionId)
        {
            var myCache = GetSessionCacheByKey(sessionId);
            return myCache.Get<bool>("Terms", false);
        }

        /// <summary>
        /// Kthen CultureInfo ne baze te gjuhes
        /// </summary>
        /// <param name="Session">sesioni</param>
        /// <returns></returns>
        public static CultureInfo ktheCultureInfo(HttpSessionState session)
        {
            if (session.IsNewSession)
                clsFunksione.logout(session, true, "MbarimSessioni");
            return MessagesResource.KtheCultureInfo(ktheGjuhe(session));
        }

        /// <summary>
        /// Kthen Id e perdoruesit ne sesion
        /// </summary>
        /// <param name="Session">sesioni</param>
        /// <returns></returns>
        public static int ktheIdPerdoruesi(HttpSessionState session)
        {
            var idPerdoruesi = ktheIdPerdoruesi(session.SessionID);
            if (session.IsNewSession && idPerdoruesiRequired() && idPerdoruesi == 0)
            {
                clsFunksione.logout(session, true, true, false, "MbarimSessioni");
                return 0;
            }
            //HttpContext.Current.Application[MySessionCache.SessionID] nese nuk eshte null do te thote qe ky perdorues eshte loguar dhe diku tjeter me kete username 
            //dhe ka te konfiguruar te politikat e fjalekalimit BllokoLogin true ose te konfiguruar te licencat BllokoMultipleLogin true, qe do te thote qe te lejoje bllokimin heren e dyte, dhe te beje logout te vendi i pare qe eshte loguar. Ne kete menyre lejon qe perdoruesi te mos jete i loguar me shume se nje here
            if (session != null && HttpContext.Current.Application[session.SessionID] != null)
            {
                HttpContext.Current.Application.Remove(session.SessionID);
                clsFunksione.logout(session, true, true, true, "double");
                return 0;
            }
            return idPerdoruesi;
        }
        public static bool? ktheRuajFilter(string sessionId)
        {
            var myCache = GetSessionCacheByKey(sessionId);
            return myCache.Get<bool?>("RuajFilter");
        }


        public static int ktheIdPerdoruesi(string sessionId)
        {
            var myCache = GetSessionCacheByKey(sessionId);
            return myCache.Get<int>("Idperdoruesi",false);
        }
        private static bool idPerdoruesiRequired()
        {
            if (HttpContext.Current.Request.Url.LocalPath.ContainsAnyIgnoreCase("NdryshimFjalekalimi"))
                return false;
            return true;
        }
    


        /// <summary>
        /// Ruan id e perdoruesit ne sesion
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <param name="idPerd">id e perdoruesit</param>
        /// <returns>True nese ruajtja perfundoi me sukses, False perndryshe</returns>
        public static bool ruajIdPerdoruesiNeSesion(HttpSessionState session, string idPerd)
        {

            MySessionCache.Set("Idperdoruesi", idPerd, false, false);
            return true;
        }

        /// <summary>
        /// Kthen rreshtin nga sesioni
        /// </summary>
        /// <param name="Session"></param>
        /// <returns></returns>
        public static int merrRreshtiNgaSesioni(HttpSessionState session)
        {
            if (session.IsNewSession)
            {
                //throw new mySessionNewException("Sessioni ka skaduar, Ju lutem logohuni perseri");
                clsFunksione.logout(session, true, "MbarimSessioni");
            }
            return (MySessionCache["rreshti"] != null) ? Convert.ToInt32(MySessionCache["rreshti"]) : 0;
        }

        /// <summary>
        /// Ruan id e perdoruesit ne sesion
        /// </summary>
        /// <param name="rreshti"></param>
        /// <param name="session"></param>
        /// <returns>True nese ruajtja perfundoi me sukses, False perndryshe</returns>
        public static bool ruajRreshtiNeSesion(HttpSessionState session, int rreshti)
        {

            if (MySessionCache["rreshti"] == null)
                MySessionCache.Add("rreshti", rreshti);
            else MySessionCache["rreshti"] = rreshti;
            return true;
        }

        /// <summary>
        /// fshin rreshtin nga sesioni
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static bool fshiRreshtiNgaSessioni(HttpSessionState session)
        {
            try
            {
                MySessionCache.Remove("rreshti");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// kthen nga sesioni vitin e ndermarrjes
        /// </summary>
        /// <param name="Session">sesioni</param>
        /// <returns></returns>
        public static int ktheVitiNdermarrjes(HttpSessionState session)
        {
            if (session.IsNewSession)
            {
                clsFunksione.logout(session, true, "MbarimSessioni");
            }
            return (MySessionCache["VitiNdermarrjes"] != null) ? Convert.ToInt32(MySessionCache["VitiNdermarrjes"]) : 0;
        }

        /// <summary>
        /// ruan vitin e ndermarrjes
        /// </summary>
        /// <param name="session">Sesioni</param>
        /// <param name="viti">viti</param>
        /// <returns>True nese ruajtja perfundoi me sukses, False perndryshe</returns>
        public static bool ruajVitiNdermarrjes(HttpSessionState session, String viti)
        {
            if (MySessionCache["VitiNdermarrjes"] == null)
                MySessionCache.Add("VitiNdermarrjes", viti);
            else MySessionCache["VitiNdermarrjes"] = viti;
            return true;
        }

        /// <summary>
        /// kthen numrin e provave per login te perdoruesit
        /// </summary>
        /// <param name="Session"></param>
        /// <returns></returns>
        public static int merrLoginCount(HttpSessionState session)
        {

            return IMBUtils.Types.Converter.MerrVlereOseDefault<int>(GlobalCacheManager.GetSessionCacheByKey(session.SessionID).Get<int>("LoginCount", false));
        }

        /// <summary>
        /// ruan numrin e provave per login te perdoruesit
        /// </summary>
        /// <param name="count">numri i provave</param>
        /// <param name="session">sesioni</param>
        /// <returns>True nese ruajtja perfundoi me sukses, False perndryshe</returns>
        public static bool ruajLoginCount(HttpSessionState session, int count)
        {
            GlobalCacheManager.GetSessionCacheByKey(session.SessionID).Set("LoginCount", count, false, false);
            return true;
        }

        /// <summary>
        /// Kthen periudhen
        /// </summary>
        /// <param name="Session"></param>
        /// <returns></returns>
        public static int merrPeriudheNgaSesioni(HttpSessionState session)
        {
            if (session.IsNewSession)
            {
                //throw new mySessionNewException("Sessioni ka skaduar, Ju lutem logohuni perseri");
                clsFunksione.logout(session, true, "MbarimSessioni");
            }
            return (MySessionCache["Periudha"] != null) ? Convert.ToInt32(MySessionCache["Periudha"]) : 0;
        }

        /// <summary>
        /// ruan ne sesion periudhen.
        /// </summary>
        /// <param name="Session">Sesioni</param>
        /// <param name="periudha">numri i periudhes</param>
        /// <returns>True nese ruajtja perfundoi me sukses, False perndryshe</returns>
        public static bool ruajPeriudheNeSesion(HttpSessionState session, int periudha)
        {
            if (MySessionCache["Periudha"] == null)
                MySessionCache.Add("Periudha", periudha);
            else
                MySessionCache["Periudha"] = periudha;
            return true;

        }

        /// <summary>
        /// kthen numrin maksimal te provave te mundshme per t'u loguar te perdoruesit
        /// </summary>
        /// <param name="Session">sesioni</param>
        /// <returns></returns>
        public static int merrMaxLoginAttempts(HttpSessionState session)
        {
            String stringMaxLoginAttempts = WebConfigurationManager.AppSettings["MaxLoginAttempts"];
            return String.IsNullOrEmpty(stringMaxLoginAttempts) ? 3 : Convert.ToInt32(stringMaxLoginAttempts);
        }



        /// <summary>
        /// kthen nese ka dok aprovimi apo jo
        /// </summary>
        /// <param name="Session">Sesioni</param>
        /// <returns></returns>
        public static bool merrKaDokAprovimi(HttpSessionState session)
        {
            if (session.IsNewSession)
            {
                //throw new mySessionNewException("Sessioni ka skaduar, Ju lutem logohuni perseri");
                clsFunksione.logout(session, true, "MbarimSessioni");
            }
            return (MySessionCache["DokAprovim"] != null) ? Convert.ToBoolean(MySessionCache["DokAprovim"]) : false;
        }

        /// <summary>
        /// ruan ka dok aprovimi per kete skeme ne sesion
        /// </summary>
        /// <param name="Session">Sesioni</param>
        /// <param name="kadokaprovimi"> ka dok aprovimi</param>
        /// <returns>True nese ruajtja perfundoi me sukses, False perndryshe</returns>
        public static bool ruajKaDokAprovimi(HttpSessionState session, bool kadokaprovimi)
        {

            if (MySessionCache["DokAprovim"] == null)
                MySessionCache.Add("DokAprovim", kadokaprovimi);
            else
                MySessionCache["DokAprovim"] = kadokaprovimi;
            return true;
        }

        /// <summary>
        /// perdoret per te kthyer perdoruesin e loguar ne kete moment
        /// </summary>
        /// <returns>objektin perdorues, ngre exception perndryshe</returns>
        public static clsPerdorues kthePerdorues(HttpSessionState session)
        {
            clsPerdorues oPerdorues = new clsPerdorues();
            if (session.IsNewSession)
            {
                clsFunksione.logout(session, true, "MbarimSessioni");
            }
            try
            {
                oPerdorues = MySessionCache.Get<clsPerdorues>("oClsPerdoruesi", false);
                if (oPerdorues == null)
                    clsFunksione.logout(session, true, "MbarimSessioni");
            }
            catch (Exception)
            {
                clsFunksione.logout(session, true, "MbarimSessioni");
            }
            return oPerdorues;
        }

        /// <summary>
        /// Ruan perdoruesin ne sesion
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <param name="perdoruesi">Perdoruesi, i tipit clsPerodrues</param>
        /// <returns>True nese ruajtja perfundoi me sukses, False perndryshe</returns>
        public static bool ruajPerdoruesNeSesion(HttpSessionState session, clsPerdorues perdoruesi)
        {
            MySessionCache.Set("oClsPerdoruesi", perdoruesi, false, false);
            return true;
        }

        /// <summary>
        /// kthen username te perdoruesit te loguar aktualisht
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static string ktheEmerPerdorues(HttpSessionState session)
        {
            return MySessionCache.Get<string>("emerUser", false);

        }

        /// <summary>
        /// ruan username te perdoruesit te loguar
        /// </summary>
        /// <param name="session"></param>
        /// <param name="perdoruesi"></param>
        /// <returns></returns>
        public static bool ruajEmerPerdoruesNeSesion(HttpSessionState session, string perdoruesi)
        {
            MySessionCache.Set("emerUser", perdoruesi, false, false);
            return true;
        }



        /// <summary>
        /// Kthen idVitin nga periudha aktuale ne session
        /// </summary>
        /// <param name="Session"></param>
        /// <returns></returns>
        public static int ktheIdVitNdermarrje(HttpSessionState session)
        {
            if (session.IsNewSession)
                //throw new mySessionNewException("Sessioni ka skaduar, Ju lutem logohuni perseri");
                clsFunksione.logout(session, true, "MbarimSessioni");
            var myCache = GetSessionCacheByKey(session.SessionID);
            int idViti;
            if (myCache["oPeriudhaAktuale"] != null)
            {
                idViti = myCache.Get<clsPeriudhaKontabel>("oPeriudhaAktuale").IdViti;

                return idViti;
            }
            return 0;

        }
        /// <summary>
        /// merr nga sesioni imazhin e ruajtur ne sesion, imazi eshte logoja e ndermarrjes
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <returns></returns>
        public static Byte[] merrImazhNgaSesioni(HttpSessionState session)
        {
            if (session.IsNewSession)
            {
                clsFunksione.logout(session, true, "MbarimSessioni");
            }

            if (MySessionCache["Imazhi"] != null)
                return (Byte[])MySessionCache["Imazhi"];
            else return null;

        }
        public static string merrPathNgaSesioni(HttpSessionState session)
        {
            if (session.IsNewSession)
            {
                clsFunksione.logout(session, true, "MbarimSessioni");
            }

            if (MySessionCache["Pathi"] != null)
                return (string)MySessionCache["Pathi"];
            else return null;

        }

        /// <summary>
        /// Ruan imazhin e logos se ndermarrjes ne sesion
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <param name="img">imazhi qe do ruhet ne sesion</param>
        /// <returns>True nese ruajtja perfundoi me sukses, False perndryshe</returns>
        public static bool ruajImazhNeSesion(HttpSessionState session, Byte[] img)
        {
            MySessionCache["Imazhi"] = img;
            return true;
        }
        public static bool ruajpathneSession(HttpSessionState session,string path)
        {
            MySessionCache["Pathi"] = path;
            return true;
        }
        public static bool ruajNdermarjeselectSession(HttpSessionState session, string  id)
        {
            MySessionCache["Hfid"] = id;
            return true;
        }
        public static string merrNdermarjeselectSesioni(HttpSessionState session)
        {
            if (session.IsNewSession)
            {
                clsFunksione.logout(session, true, "MbarimSessioni");
            }

            if (MySessionCache["Hfid"] != null)
                return (string)MySessionCache["Hfid"];
            else return null;

        }
        /// <summary>
        /// merr nga sesioni thumbnailfilename
        /// </summary>
        /// <param name="session">Sesioni</param>
        /// <returns></returns>
        public static String merrThumbnailFileNameNgaSesioni(HttpSessionState session)
        {
            if (session.IsNewSession)
            {
                clsFunksione.logout(session, true, "MbarimSessioni");
            }

            if (MySessionCache["ThumbnailFileName"] != null)
                return MySessionCache["ThumbnailFileName"].ToString();
            else return null;

        }

        /// <summary>
        /// ruan thumbnailfilename ne sesion
        /// </summary>
        /// <param name="session">Sesioni</param>
        /// <param name="str">stringu qe do ruhet tek thumbnailfilename</param>
        /// <returns>True nese ruajtja perfundoi me sukses, False perndryshe</returns>
        public static bool ruajThumbnailFileNameNeSesion(HttpSessionState session, String str)
        {


            if (MySessionCache["ThumbnailFileName"] == null)
                MySessionCache.Add("ThumbnailFileName", str);
            else
                MySessionCache["ThumbnailFileName"] = str;
            return true;
        }

        /// <summary>
        /// merr nga sesioni punesimin
        /// </summary>
        /// <param name="session">Sesioni</param>
        /// <returns></returns>
        public static colPunesim merrPunesimNgaSesioni(HttpSessionState session)
        {
            if (session.IsNewSession)
            {
                clsFunksione.logout(session, true, "MbarimSessioni");
            }

            if (MySessionCache["Punesim"] != null)
                return (colPunesim)MySessionCache["Punesim"];
            else return new colPunesim();

        }

        /// <summary>
        /// ruan punesimin ne sesion
        /// </summary>
        /// <param name="session">Sesioni</param>
        /// <param name="punesim">punesimi qe do ruhet ne sesion</param>
        /// <returns>True nese ruajtja perfundoi me sukses, False perndryshe</returns>
        public static bool ruajPunesimNeSesion(HttpSessionState session, colPunesim punesim)
        {

            if (MySessionCache["Punesim"] == null)
                MySessionCache.Add("Punesim", punesim);
            else
                MySessionCache["Punesim"] = punesim;
            return true;
        }

        /// <summary>
        /// merr shtesat e pagave nga sesioni
        /// </summary>
        /// <param name="session">Sesioni</param>
        /// <returns></returns>
        public static colPagaShtesa merrPageShteseNgaSesioni(HttpSessionState session)
        {
            if (session.IsNewSession)
            {
                clsFunksione.logout(session, true, "MbarimSessioni");
            }

            if (MySessionCache["PagaShtesa"] != null)
                return (colPagaShtesa)MySessionCache["PagaShtesa"];
            else return new colPagaShtesa();

        }

        /// <summary>
        /// ruan shtesat e pagave ne sesion
        /// </summary>
        /// <param name="session">Sesioni</param>
        /// <param name="pagashtesa">paga, objekti qe do ruhet ne sesion</param>
        /// <returns>True nese ruajtja perfundoi me sukses, False perndryshe</returns>
        public static bool ruajPageShteseNeSesion(HttpSessionState session, colPagaShtesa pagashtesa)
        {

            if (MySessionCache["PagaShtesa"] == null)
                MySessionCache.Add("PagaShtesa", pagashtesa);
            else
                MySessionCache["PagaShtesa"] = pagashtesa;
            return true;

        }

        /// <summary>
        /// merr id e komponentes qe do shtohet ne gride
        /// </summary>
        /// <param name="Session">Sesioni</param>
        /// <returns></returns>
        public static int merrIdShtimiNgaSesioni(HttpSessionState session)
        {
            if (session.IsNewSession)
            {
                clsFunksione.logout(session, true, "MbarimSessioni");
            }
            return (MySessionCache["idShtimi"] != null) ? Convert.ToInt32(MySessionCache["idShtimi"]) : 0;
        }

        /// <summary>
        /// ruan id e komponentes qe do shtohet ne gride
        /// </summary>
        /// <param name="session">Sesioni</param>
        /// <param name="id">id e shtimit</param>
        /// <returns>True nese ruajtja perfundoi me sukses, False perndryshe</returns>
        public static bool ruajIdShtimiNeSesion(HttpSessionState session, int id)
        {

            if (MySessionCache["idShtimi"] == null)
                MySessionCache.Add("idShtimi", id);
            else MySessionCache["idShtimi"] = id;
            return true;
        }

        public static DataTable merrGridFaturatNgaSessioni(string komponente, HttpSessionState session, bool shtoPageId)
        {
            DataTable dt;
            merrGrideNgaSessioni(komponente, session, out dt,shtoPageId);
            return dt;
        }

        public static DataTable merrGridFaturatNgaSessioni(string komponente, HttpSessionState session)
        {
            return merrGridFaturatNgaSessioni(komponente, session, true);
        }



        public static bool ruajGridFaturatNeSession(string komponente, HttpSessionState session, DataTable dt)
        {
            return ruajGrideNeSession(komponente, session, dt);
        }

        /// <summary>
        /// merr nga sesioni ndermarrjet e selektuara
        /// </summary>
        /// <param name="Session">Sesioni</param>
        /// <returns></returns>
        public static colNdermarrjet merrNdermarrjetSelNgaSesioni(HttpSessionState session)
        {
            if (session.IsNewSession)
            {
                clsFunksione.logout(session, true, "MbarimSessioni");
            }

            if (MySessionCache["NdermarjetSel"] != null)
                return (colNdermarrjet)MySessionCache["NdermarjetSel"];
            else return null;

        }

        /// <summary>
        /// ruan ne sesion ndermarrjet e selektuara
        /// </summary>
        /// <param name="Session">Sesioni</param>
        /// <param name="nderm">Ndermarrjet e selektuara</param>
        /// <returns>True nese ruajtja perfundoi me sukses, False perndryshe</returns>
        public static bool ruajNdermarjetSelNeSesion(HttpSessionState session, colNdermarrjet nderm)
        {

            if (MySessionCache["NdermarjetSel"] == null)
                MySessionCache.Add("NdermarjetSel", nderm);
            else MySessionCache["NdermarjetSel"] = nderm;
            return true;
        }

        /// <summary>
        /// merr nga sesioni komponenten e list pageses se punonjesit
        /// </summary>
        /// <param name="Session"></param>
        /// <returns></returns>
        public static colKomponenteListPagesePunonjesi merrKompListPagesPunonjesiNgaSesioni(HttpSessionState session)
        {
            if (session.IsNewSession)
            {
                clsFunksione.logout(session, true, "MbarimSessioni");
            }

            if (MySessionCache["KomponenteListPagesePunonjes"] != null)
                return (colKomponenteListPagesePunonjesi)MySessionCache["KomponenteListPagesePunonjes"];
            else return null;
        }

        /// <summary>
        /// ruan ne sesion komponenten e listpageses se punonjesit
        /// </summary>
        /// <param name="Session">Sesion</param>
        /// <param name="komp">komponente listpagese punonjes</param>
        /// <returns>True nese ruajtja perfundoi me sukses, False perndryshe</returns>
        public static bool ruajKompListPagesPunonjesiNeSesion(HttpSessionState session, colKomponenteListPagesePunonjesi komp)
        {

            if (MySessionCache["KomponenteListPagesePunonjes"] == null)
                MySessionCache.Add("KomponenteListPagesePunonjes", komp);
            else MySessionCache["KomponenteListPagesePunonjes"] = komp;
            return true;
        }

        /// <summary>
        /// kthen nga sesioni id e modelit
        /// </summary>
        /// <param name="Session">Sesioni</param>
        /// <returns></returns>
        public static int merrIdModNgaSesioni(HttpSessionState session)
        {
            if (session.IsNewSession)
            {
                clsFunksione.logout(session, true, "MbarimSessioni");
            }
            return (MySessionCache["idmod"] != null) ? Convert.ToInt32(MySessionCache["idmod"]) : 0;
        }

        /// <summary>
        /// ruan ne sesion id e modelit
        /// </summary>
        /// <param name="Session">Session</param>
        /// <param name="idMod">id modeli</param>
        /// <returns>True nese ruajtja perfundoi me sukses, False perndryshe</returns>
        public static bool ruajIdModNeSesion(HttpSessionState session, Object idMod)
        {

            if (MySessionCache["idmod"] == null)
                MySessionCache.Add("idmod", idMod);
            else
                MySessionCache["idmod"] = idMod;
            return true;
        }

        public static colVleraFushaShtese merrVleraNgaSesioniGrida(HttpSessionState session, string sessionKey)
        {

            return (MyPageCache[$"VleraGrida_{sessionKey}"] != null) ? (colVleraFushaShtese)MyPageCache[$"VleraGrida_{sessionKey}"] : new colVleraFushaShtese();
            //return (MySessionCache[$"VleraGrida_{sessionKey}"] != null) ? (colVleraFushaShtese)MySessionCache[$"VleraGrida_{sessionKey}"] : new colVleraFushaShtese();
        }
        public static void RuajFushaShteseNeSessionGrida(HttpSessionState session, string sessionKey, colFushatShtese colfusha)
        {
            MyPageCache[$"FushaGrida_{sessionKey}"] = colfusha;
            //MySessionCache[$"FushaGrida_{sessionKey}"] = colfusha;

        }
        public static colFushatShtese merrFushaShteseNgaSesioniGrida(HttpSessionState session, string sessionKey)
        {
            if (session.IsNewSession)
            {
                clsFunksione.logout(session, true, "MbarimSessioni");
            }
            return (MyPageCache[$"FushaGrida_{sessionKey}"] != null) ? (colFushatShtese)MyPageCache[$"FushaGrida_{sessionKey}"] : new colFushatShtese();
            //return (MySessionCache[$"FushaGrida_{sessionKey}"] != null) ? (colFushatShtese)MySessionCache[$"FushaGrida_{sessionKey}"] : new colFushatShtese();
        }
        /// <summary>
        /// ruan ne sesion id e modelit
        /// </summary>
        /// <param name="Session">Session</param>
        /// <param name="idMod">id modeli</param>
        /// <returns>True nese ruajtja perfundoi me sukses, False perndryshe</returns>
        public static bool ruajVleraNeSesionGrida(HttpSessionState session, string sessionKey, colVleraFushaShtese colVlera)
        {
            MyPageCache[$"VleraGrida_{sessionKey}"] = colVlera;
            //MySessionCache[$"VleraGrida_{sessionKey}"] = colVlera;
            return true;
        }

        public static Dictionary<int, Dictionary<string, colVleraFushaShtese>> merrVleraNgaSesioni(HttpSessionState Session, string sessionKey)
        {
            return (Dictionary<int, Dictionary<string, colVleraFushaShtese>>)MyPageCache[$"Vlera_{sessionKey}"] ?? new Dictionary<int, Dictionary<string, colVleraFushaShtese>>();

            //return MerrNgaSession<Dictionary<int, Dictionary<string, colVleraFushaShtese>>>(Session, $"Vlera_{sessionKey}") ?? new Dictionary<int, Dictionary<string, colVleraFushaShtese>>();
        }
        public static Dictionary<int, colFushatShtese> merrFushaShteseNGaSessioni(HttpSessionState session, string sessionKey)
        {
            return (Dictionary<int, colFushatShtese>)MyPageCache[$"FushaShteseCol_{sessionKey}"] ?? new Dictionary<int, colFushatShtese>();

            //return MerrNgaSession<Dictionary<int, colFushatShtese>>(session, $"FushaShteseCol_{sessionKey}") ?? new Dictionary<int, colFushatShtese>();
        }
        public static void ruajFushaShteseNeSession(HttpSessionState session, string sessionKey, Dictionary<int, colFushatShtese> fushatShtese)
        {
            MyPageCache[$"FushaShteseCol_{sessionKey}"] = fushatShtese;
           // MySessionCache[$"FushaShteseCol_{sessionKey}"] = fushatShtese;
        }
        /// <summary>
        /// ruan ne sesion id e modelit
        /// </summary>
        /// <param name="Session">Session</param>
        /// <param name="idMod">id modeli</param>
        /// <returns>True nese ruajtja perfundoi me sukses, False perndryshe</returns>
        public static void ruajVleraNeSesion(HttpSessionState Session, string sessionKey, Dictionary<int, Dictionary<string, colVleraFushaShtese>> colVlera)
        {
            MyPageCache[$"Vlera_{sessionKey}"] = colVlera;
            //RuajNeSession<Dictionary<int, Dictionary<string, colVleraFushaShtese>>>(Session, colVlera, $"Vlera_{sessionKey}");
        }

        /// <summary>
        /// kthen nga sesioni url e lupes
        /// </summary>
        /// <param name="Session">Sesioni</param>
        /// <returns></returns>
        public static string merrURLARTNgaSesioni(HttpSessionState session)
        {
            if (session.IsNewSession)
            {
                clsFunksione.logout(session, true, "MbarimSessioni");
            }
            return (MySessionCache["URLLupaART"] != null) ? MySessionCache["URLLupaART"].ToString() : "";
        }

        /// <summary>
        /// ruan ne sesion urlart
        /// </summary>
        /// <param name="Session">Session</param>
        /// <param name="url">url</param>
        /// <returns>True nese ruajtja perfundoi me sukses, False perndryshe</returns>
        public static bool ruajURLARTNeSesion(HttpSessionState session, Object url)
        {

            if (MySessionCache["URLLupaART"] == null)
                MySessionCache.Add("URLLupaART", url);
            else
                MySessionCache["URLLupaART"] = url;
            return true;

        }

        /// <summary>
        /// kthen nga sesioni url e lupes
        /// </summary>
        /// <param name="Session">Sesioni</param>
        /// <returns></returns>
        public static string merrURLLupaShpejteNgaSesioni(HttpSessionState session, string lupa)
        {
            if (session.IsNewSession)
            {
                clsFunksione.logout(session, true, "MbarimSessioni");
            }
            return (MySessionCache["URLLupa" + lupa] != null) ? MySessionCache["URLLupa" + lupa].ToString() : "";
        }

        /// <summary>
        /// ruan ne sesion urlart
        /// </summary>
        /// <param name="Session">Session</param>
        /// <param name="url">url</param>
        /// <returns>True nese ruajtja perfundoi me sukses, False perndryshe</returns>
        public static bool ruajURLLupaShpejteNeSesion(HttpSessionState session, Object url, string lupa)
        {


            if (MySessionCache["URLLupa" + lupa] == null)
                MySessionCache.Add("URLLupa" + lupa, url);
            else
                MySessionCache["URLLupa" + lupa] = url;
            return true;

        }

        /// <summary>
        /// kthen nga sesioni url e lupes se llogarise
        /// </summary>
        /// <param name="Session">Sesioni</param>
        /// <returns></returns>
        public static string merrURLllogNgaSesioni(HttpSessionState session)
        {
            if (session.IsNewSession)
            {
                clsFunksione.logout(session, true, "MbarimSessioni");
            }
            return (MySessionCache["URLLupaLLOG"] != null) ? MySessionCache["URLLupaLLOG"].ToString() : "";
        }

        /// <summary>
        /// ruan ne sesion url e lupes se llogarise
        /// </summary>
        /// <param name="Session">Session</param>
        /// <param name="url">url</param>
        /// <returns>True nese ruajtja perfundoi me sukses, False perndryshe</returns>
        public static bool ruajURLllogNeSesion(HttpSessionState session, Object url)
        {



            if (MySessionCache["URLLupaLLOG"] == null)
                MySessionCache.Add("URLLupaLLOG", url);
            else
                MySessionCache["URLLupaLLOG"] = url;
            return true;

        }

        /// <summary>
        /// kthen nga sesioni url e lupes
        /// </summary>
        /// <param name="Session">Sesioni</param>
        /// <returns></returns>
        public static string merrURLNgaSesioni(HttpSessionState session)
        {
            if (session.IsNewSession)
            {
                clsFunksione.logout(session, true, "MbarimSessioni");
            }
            return (MySessionCache["URLLupa"] != null) ? MySessionCache["URLLupa"].ToString() : "";
        }

        /// <summary>
        /// ruan ne sesion urlart
        /// </summary>
        /// <param name="Session">Session</param>
        /// <param name="url">url</param>
        /// <returns>True nese ruajtja perfundoi me sukses, False perndryshe</returns>
        public static bool ruajURLNeSesion(HttpSessionState session, Object url)
        {

            if (MySessionCache["URLLupa"] == null)
                MySessionCache.Add("URLLupa", url);
            else
                MySessionCache["URLLupa"] = url;
            return true;

        }

        /// <summary>
        /// merr rreshtat visible te infos se artikullit nga sesioni
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <returns>koleksion me info e artikullit te dukshme</returns>
        public static colInfoTrupi merrInfoVisibleNgaSesioni(HttpSessionState session)
        {
            if (session.IsNewSession)
            {
                clsFunksione.logout(session, true, "MbarimSessioni");
            }

            if (MySessionCache["colInfoVis"] != null)
            {
                return (colInfoTrupi)MySessionCache["colInfoVis"];
            }
            else return null;

        }

        /// <summary>
        /// ruan ne session rreshtat e dukshme te infos se artikulit
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <param name="prod">coleksioni me info artikulli</param>
        /// <returns>true ose false</returns>
        public static bool ruajInfoVisibleNeSession(HttpSessionState session, Object prod)
        {

            if (MySessionCache["colInfoVis"] == null)
                MySessionCache.Add("colInfoVis", prod);
            else
                MySessionCache["colInfoVis"] = prod;
            return true;

        }

        /// <summary>
        /// merr rreshtat te padukshme te infos se artikullit nga sesioni
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <returns>koleksion me info e artikullit te pa dukshme</returns>
        public static colInfoTrupi merrInfoInVisibleNgaSesioni(HttpSessionState session)
        {
            if (session.IsNewSession)
            {
                clsFunksione.logout(session, true, "MbarimSessioni");
            }

            if (MySessionCache["colInfoInVis"] != null)
            {
                return (colInfoTrupi)MySessionCache["colInfoInVis"];
            }
            else return null;

        }

        /// <summary>
        /// ruan ne session rreshtat e padukshme te infos se artikulit
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <param name="prod">coleksioni me info artikulli</param>
        /// <returns>true ose false</returns>
        public static bool ruajInfoInVisibleNeSession(HttpSessionState session, Object prod)
        {

            if (MySessionCache["colInfoInVis"] == null)
                MySessionCache.Add("colInfoInVis", prod);
            else
                MySessionCache["colInfoInVis"] = prod;
            return true;

        }

        /// <summary>
        /// merr rreshtat visible te format importi nga sesioni
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <returns>koleksion me kontrolle format importi te dukshme</returns>
        public static colTrupiFormatImporti merrFormatImportiVisibleNgaSesioni(HttpSessionState session)
        {
            if (session.IsNewSession)
            {
                clsFunksione.logout(session, true, "MbarimSessioni");
            }

            if (MySessionCache["colFormatImportiVis"] != null)
            {
                return (colTrupiFormatImporti)MySessionCache["colFormatImportiVis"];
            }
            else return null;

        }
        public static colTrupiAnketa merrAnketaVisibleNgaSesioni(HttpSessionState session)
        {
            if (session.IsNewSession)
            {
                clsFunksione.logout(session, true, "MbarimSessioni");
            }

            if (MySessionCache["colFormatImportiVis"] != null)
            {
                return (colTrupiAnketa)MySessionCache["colFormatImportiVis"];
            }
            else return null;


        }
        public static colAQTSeriale merrSerialeArtikulliNgaSesioni(HttpSessionState session, int idartikulli)
        {
            if (session.IsNewSession)
            {
                clsFunksione.logout(session, true, "MbarimSessioni");
            }
            if (MySessionCache["colSeriale" + idartikulli] != null)
            {
                return (colAQTSeriale)MySessionCache["colSeriale" + idartikulli];
            }
            else return null;

        }

        public static colAQTSeriale merrSerialeArtikulliZgjedhurNgaSesioni(HttpSessionState session, int idartikulli)
        {
            if (session.IsNewSession)
            {
                clsFunksione.logout(session, true, "MbarimSessioni");
            }

            if (MySessionCache["colSerialeZgjedhur" + idartikulli] != null)
            {
                return (colAQTSeriale)MySessionCache["colSerialeZgjedhur" + idartikulli];
            }
            else return null;

        }
        public static bool ruajSerialeArtikulliZgjedhurNeSession(HttpSessionState session, Object prod, int idartikulli)
        {

            if (MySessionCache["colSerialeZgjedhur" + idartikulli] == null)
                MySessionCache.Add("colSerialeZgjedhur" + idartikulli, prod);
            else
                MySessionCache["colSerialeZgjedhur" + idartikulli] = prod;
            return true;

        }

        /// <summary>
        /// ruan ne session rreshtat e dukshme te FormatImporti
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <param name="prod">coleksioni me FormatImporti</param>
        /// <returns>true ose false</returns>
        public static bool ruajFormatImportiVisibleNeSession(HttpSessionState session, Object prod)
        {

            if (MySessionCache["colFormatImportiVis"] == null)
                MySessionCache.Add("colFormatImportiVis", prod);
            else
                MySessionCache["colFormatImportiVis"] = prod;
            return true;

        }

        /// <summary>
        /// merr rreshtat te padukshme te FormatImporti nga sesioni
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <returns>koleksion me FormatImporti te pa dukshme</returns>
        public static colTrupiFormatImporti merrFormatImportiInVisibleNgaSesioni(HttpSessionState session)
        {
            if (session.IsNewSession)
            {
                clsFunksione.logout(session, true, "MbarimSessioni");
            }

            if (MySessionCache["colFormatImportiInVis"] != null)
            {
                return (colTrupiFormatImporti)MySessionCache["colFormatImportiInVis"];
            }
            else return null;


        }
        public static colTrupiAnketa merrAnketaInVisibleNgaSesioni(HttpSessionState session)
        {
            if (session.IsNewSession)
            {
                clsFunksione.logout(session, true, "MbarimSessioni");
            }

            if (MySessionCache["colFormatImportiInVis"] != null)
            {
                return (colTrupiAnketa)MySessionCache["colFormatImportiInVis"];
            }
            else return null;


        }

        /// <summary>
        /// ruan ne session rreshtat e padukshme te FormatImporti
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <param name="prod">coleksioni me FormatImporti</param>
        /// <returns>true ose false</returns>
        public static bool ruajFormatImportiInVisibleNeSession(HttpSessionState session, Object prod)
        {

            if (MySessionCache["colFormatImportiInVis"] == null)
                MySessionCache.Add("colFormatImportiInVis", prod);
            else
                MySessionCache["colFormatImportiInVis"] = prod;
            return true;

        }

        public static bool ruajSerialeArtikulliNeSession(HttpSessionState session, Object prod, int idartikulli)
        {

            if (MySessionCache["colSeriale" + idartikulli] == null)
                MySessionCache.Add("colSeriale" + idartikulli, prod);
            else
                MySessionCache["colSeriale" + idartikulli] = prod;
            return true;

        }


        /// <summary>
        /// merr rreshtat llogarite qk nga sesioni
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <returns>koleksion me llogariteqk te pa dukshme</returns>
        public static colLlogariShperndarjeQK merrLlogariteQKNgaSesioni(HttpSessionState session)
        {
            if (session.IsNewSession)
            {
                clsFunksione.logout(session, true, "MbarimSessioni");
            }

            if (MyPageCache["LlogariteQK"] != null)
            {
                return (colLlogariShperndarjeQK)MyPageCache["LlogariteQK"];
            }
            else return null;

        }

        /// <summary>
        /// merr rreshtat te kpf nga sesioni
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <returns>koleksion me kpf te pa dukshme</returns>
        public static colKPFte merrKPFQKNgaSesioni(HttpSessionState session)
        {
            if (session.IsNewSession)
            {
                clsFunksione.logout(session, true, "MbarimSessioni");
            }

            if (MyPageCache["KPFQK"] != null)
            {
                return (colKPFte)MyPageCache["KPFQK"];
            }
            else return null;

        }

        /// <summary>
        /// ruan ne session rreshtat llogarite qk
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <param name="prod">coleksioni me llogarite</param>
        /// <returns>true ose false</returns>
        public static bool ruajLlogariteQKNeSession(HttpSessionState session, Object prod)
        {


            if (MyPageCache["LlogariteQK"] == null)
                MyPageCache.Add("LlogariteQK", prod);
            else
                MyPageCache["LlogariteQK"] = prod;
            return true;

        }

        /// <summary>
        /// ruan ne session rreshtat kpf
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <param name="prod">coleksioni me kpf</param>
        /// <returns>true ose false</returns>
        public static bool ruajKPFQKNeSession(HttpSessionState session, Object prod)
        {

            if (MyPageCache["KPFQK"] == null)
                MyPageCache.Add("KPFQK", prod);
            else
                MyPageCache["KPFQK"] = prod;
            return true;

        }

        /// <summary>
        /// merr rreshtat  e grides se importit te ruajtura ne session
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <returns>datatable me rreshtat e grides</returns>
        public static DataTable merrRreshtaImportiNgaGrida(HttpSessionState session)
        {
            if (session.IsNewSession)
            {
                clsFunksione.logout(session, true, "MbarimSessioni");
            }

            if (MySessionCache["gvImport"] != null)
            {
                return (DataTable)MySessionCache["gvImport"];
            }
            else return null;

        }

        /// <summary>
        /// ruan ne session rreshtat e grides se importit
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <param name="prod">Data table me rreshtat</param>
        /// <returns>true ose false</returns>
        public static bool ruajRreshtaImportiNgaGrida(HttpSessionState session, Object prod)
        {

            if (MySessionCache["gvImport"] == null)
                MySessionCache.Add("gvImport", prod);
            else
                MySessionCache["gvImport"] = prod;
            return true;

        }

        /// <summary>
        /// merr rreshtat  e tables se gabimeve te importit qe duhen per raportin e gabimeve
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <returns>datatable me rreshtat e grides</returns>
        public static DataTable merrTabeleGabimeshImporti(HttpSessionState session)
        {
            if (session.IsNewSession)
            {
                clsFunksione.logout(session, true, "MbarimSessioni");
            }

            if (MySessionCache["TabeleGabimesh"] != null)
            {
                return (DataTable)MySessionCache["TabeleGabimesh"];
            }
            else return null;

        }

        /// <summary>
        /// ruan ne session rreshtat e tables se gabimeve te importit
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <param name="prod">Data table me rreshtat</param>
        /// <returns>true ose false</returns>
        public static bool ruajTabeleGabimeshImporti(HttpSessionState session, Object prod)
        {

            if (MySessionCache["TabeleGabimesh"] == null)
                MySessionCache.Add("TabeleGabimesh", prod);
            else
                MySessionCache["TabeleGabimesh"] = prod;
            return true;

        }

        

        public static (string FileName, System.IO.Stream FileContent) merrTedhenaNgafileUplodi(HttpSessionState session) => 
            ((string FileName, System.IO.Stream FileContent))MySessionCache["FileImport"];

        /// <summary>
        /// ruan ne session filin e importit
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <param name="prod">Data table me rreshtat</param>
        /// <returns>true ose false</returns>
        public static bool ruajFileUpload(HttpSessionState session,string FileName, System.IO.Stream FileContent)
        {
            MySessionCache["FileImport"] = (FileName, FileContent);
            return true;
        }

        /// <summary>
        /// merr nga sesioni vleren e colFaturat.
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <returns></returns>
        public static DataTable merrColFaturatNgaSessioni(HttpSessionState session)
        {
            if (session.IsNewSession)
            {
                clsFunksione.logout(session, true, "MbarimSessioni");
            }

            if (MyPageCache["colfaturat"] != null)
                return (DataTable)MyPageCache["colfaturat"];
            else return null;

        }

        /// <summary>
        /// ruan tek colfaturat nje data table dt.
        /// </summary>
        /// <param name="session">Sesioni</param>
        /// <param name="dt">data table qe do ruhet ne sesion</param>
        /// <returns>True nese ruajtja perfundoi me sukses, False perndryshe</returns>
        public static bool ruajColFaturatNeSession(HttpSessionState session, DataTable dt)
        {
            MyPageCache["colfaturat", true] = dt;
            return true;

        }

        /// <summary>
        /// merr SubReport nga sesioni
        /// </summary>
        /// <param name="session">Sesioni</param>
        /// <returns></returns>
        public static T merrReportNgaSessioni<T>(HttpSessionState session, String key)
        {
            if (session.IsNewSession)
            {
                clsFunksione.logout(session, true, "MbarimSessioni");
            }

            if (MySessionCache["SubReport" + key] != null)
                return (T)MySessionCache["SubReport" + key];
            else return default(T);

        }

        /// <summary>
        /// duhet ne rastin kur hapim dhe faturen dhe garancine qe te mos mbishkruajne njera tjetern
        ///
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static T merrReport2NgaSessioni<T>(HttpSessionState session, String key)
        {
            
            if (session.IsNewSession)
            {
                clsFunksione.logout(session, true, "MbarimSessioni");
            }

            if (MySessionCache["SubReport2" + key] != null)
                return (T)MySessionCache["SubReport2" + key] ;
            else return default(T);

        }

        /// <summary>
        /// ruan  raportin ne session
        /// eshte e detyrueshme per raportin sepse perndryshe del raporti bosh
        /// </summary>
        /// <param name="session"></param>
        /// <param name="obj">Objekti i tipit XtraReport qe do ruhet ne sesion</param>
        /// <returns></returns>
        public static bool ruajReportNeSession<T>(HttpSessionState session, String key, T obj)
        {

            if (MySessionCache["SubReport"] == null)
                MySessionCache.Add("SubReport" + key, obj);
            else
                MySessionCache["SubReport" + key] = obj;
            return true;

        }

        public static bool ruajReport2NeSession<T>(HttpSessionState session, String key, T obj)
        {

            if (MySessionCache["SubReport2" + key] == null)
                MySessionCache.Add("SubReport2" + key, obj);
            else
                MySessionCache["SubReport2" + key] = obj;
            return true;

        }
        

        /// <summary>
        /// merr nga sesioni vleren e ParametratSubRaportit + index
        /// </summary>
        /// <param name="session"></param>
        /// <param name="key">indexi</param>
        /// <returns></returns>
        public static Dictionary<int, String> merrParametratSubRaportitNgaSesioni(HttpSessionState session, int key, String guidString)
        {
            if (session.IsNewSession)
            {
                clsFunksione.logout(session, true, "MbarimSessioni");
            }

            if (MySessionCache["ParametratSubRaportit" + key + guidString] != null)
                return (Dictionary<int, String>)MySessionCache["ParametratSubRaportit" + key + guidString];
            else return null;

        }

        /// <summary>
        /// ruan ne sesion vleren e parametrit ParametratSubRaportit me index key
        /// </summary>
        /// <param name="session"></param>
        /// <param name="vlera"></param>
        /// <param name="key"></param>
        /// <returns>True nese ruajtja perfundoi me sukses, False perndryshe</returns>
        public static bool ruajParametratSubRaportitNeSesion(HttpSessionState session, Dictionary<int, string> vlera, int key, String guidString)
        {

            if (MySessionCache["ParametratSubRaportit" + key + guidString] == null)
                MySessionCache.Add("ParametratSubRaportit" + key + guidString, vlera);
            else
                MySessionCache["ParametratSubRaportit" + key + guidString] = vlera;
            return true;
        }

        /// <summary>
        /// merr nga sesioni vleren e ParametratShfaqSubraport + index
        /// </summary>
        /// <param name="session"></param>
        /// <param name="key">indexi</param>
        /// <returns></returns>
        public static colParameter merrParametratShfaqSubRaportitNgaSesioni(HttpSessionState session, int key, String guidString)
        {
            if (session.IsNewSession)
            {
                clsFunksione.logout(session, true, "MbarimSessioni");
            }

            if (MySessionCache["ParametratShfaqSubraport" + key + guidString] != null)
                return (colParameter)MySessionCache["ParametratShfaqSubraport" + key + guidString];
            else return null;

        }

        /// <summary>
        /// ruan ne sesion vleren e parametrit ParametratShfaqSubraport me index key
        /// </summary>
        /// <param name="session">Sesioni</param>
        /// <param name="vlera">vlera qe do ruhet ne sesion</param>
        /// <param name="key">indexi</param>
        /// <returns>True nese ruajtja perfundoi me sukses, False perndryshe</returns>
        public static bool ruajParametratShfaqSubRaportitNeSesion(HttpSessionState session, colParameter vlera, int key, String guidString)
        {
            if (MySessionCache["ParametratShfaqSubraport" + key + guidString] == null)
                MySessionCache.Add("ParametratShfaqSubraport" + key + guidString, vlera);
            else
                MySessionCache["ParametratShfaqSubraport" + key + guidString] = vlera;
            return true;
        }
        

        /// <summary>
        /// merr nga sesioni ndermarrjet e licences dhe perdoruesit
        /// </summary>
        /// <param name="Session">Sesioni</param>
        /// <returns></returns>
        public static colNdermarrjet merrNdermarrjetPerPerdoruesDheLicenceNgaSesioni(HttpSessionState session)
        {
            if (session.IsNewSession)
            {
                clsFunksione.logout(session, true, "MbarimSessioni");
            }

            if (MyPageCache["NdermarjetPerLicenceDhePerdorues"] != null)
                return (colNdermarrjet)MyPageCache["NdermarjetPerLicenceDhePerdorues"];
            else return null;

        }

        /// <summary>
        /// ruan ne sesion ndermarrjet e licences dhe perdoruesit
        /// </summary>
        /// <param name="ndermarje"></param>
        /// <param name="Session"></param>
        /// <returns></returns>
        public static bool ruajNdermarjetPerPerdoruesDheLicenceNeSesion(colNdermarrjet ndermarje, HttpSessionState session)
        {
            MyPageCache["NdermarjetPerLicenceDhePerdorues"] = ndermarje;
            return true;

        }

        /// <summary>
        /// Ruan ndermarrjen ku duhet te logohet useri nga webservisi
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <param name="ndermarrja">nderrmarja per te cilen eshte thirrur webservisi</param>
        /// <returns>True nese ruajtja perfundoi me sukses, False perndryshe</returns>
        public static void ruajNdermarrjenNgaWebServisi(HttpSessionState session, string ndermarrja)
        {
            MySessionCache.Set("ndermarrjaWS", ndermarrja, false, false);
        }

        /// <summary>
        /// Ruan ip e dyqanit te userit qe po logohet
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <param name="ipkasa">ip e kases se dyqanit ku do ndodhi printimi ne kase</param>
        /// <returns>True nese ruajtja perfundoi me sukses, False perndryshe</returns>
        public static void ruajIpKasaNgaWebServisi(HttpSessionState session, string ipkasa)
        {
            MySessionCache.Set("ipKasaWS", ipkasa, false, false);
        }

        /// <summary>
        /// Ruan adresa ne rrjet e printerit ku do ndodhi printimi
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <param name="printer">adresa e printerit se dyqanit ku do ndodhi printimi</param>
        /// <returns>True nese ruajtja perfundoi me sukses, False perndryshe</returns>
        public static void ruajPrinterNgaWebServisi(HttpSessionState session, string printer)
        {
            MySessionCache.Set("printeriWS", printer, false, false);
        }

        /// <summary>
        /// Ruan dyqanin ku do logohet useri
        /// </summary>
        /// <param name="session">sesioni</param>
        /// <param name="dyqani">Dyqani ku do logohet useri</param>
        /// <returns>True nese ruajtja perfundoi me sukses, False perndryshe</returns>
        public static void ruajDyqaninNgaWebServisi(HttpSessionState session, string dyqani)
        {
            MySessionCache.Set("dyqaniWS", dyqani, false, false);
        }

        /// <summary>
        /// kthen numrin e provave per login te perdoruesit
        /// </summary>
        /// <param name="Session"></param>
        /// <returns></returns>
        public static string merrIPKasaNgaWebServisi(HttpSessionState session)
        {
            var obj = MySessionCache.Get<object>("ipKasaWS", false);
            return (obj != null) ? obj.ToString() : "";
        }

        public static bool ekzistonIdNdermarrje(HttpSessionState sesion)
        {
            return MySessionCache["IdNdermarrjes"] != null;
        }

        public static bool ruajNumrinAutomatikPerFlexCube(HttpSessionState session, string nrSerialBatchNo)
        {


            if (MySessionCache["nrSerialBatchNo"] == null)
                MySessionCache.Add("nrSerialBatchNo", nrSerialBatchNo);
            else
                MySessionCache["nrSerialBatchNo"] = nrSerialBatchNo;
            return true;

        }

        public static String merrNrAutomatikPerFlexCube(HttpSessionState session)
        {

            if (MySessionCache["nrSerialBatchNo"] != null)
            {
                return Convert.ToString(MySessionCache["nrSerialBatchNo"]);
            }
            else return null;

        }

        public static bool ruajcbRuajFilteriNeSesion(HttpSessionState session, bool ruajFilter)
        {
            if (MySessionCache["RuajFliter"] == null)
                MySessionCache.Add("RuajFilter", ruajFilter);
            else
                MySessionCache["RuajFilter"] = ruajFilter;
            return true;
        }


        /// <summary>
        /// ruan ne session emer raporti
        /// </summary>
        /// <param name="Session"></param>
        /// <param name="emerRaporti"></param>
        public static bool RuajEmerRaporti(HttpSessionState session, string emerRaporti, string guidString)
        {
            var sessionKey = $"EmerRaporti{guidString}";

            if (MySessionCache[sessionKey] == null)
                MySessionCache.Add(sessionKey, emerRaporti);
            else
                MySessionCache[sessionKey] = emerRaporti;
            return true;

        }
        public static void RuajVleraMarreveshjeNeSession(HttpSessionState session, Object obj)
        {
            MySessionCache["VleraMarreveshje"] = obj;
        }
        public static MarreveshjeExcelReader MerrVlereMarreveshjeNgaSessioni(HttpSessionState session)
        {
            return MySessionCache.Get<MarreveshjeExcelReader>("VleraMarreveshje");
        }

        /// <summary>
        /// merr emrin e raportit qe do modifikohet
        /// </summary>
        /// <param name="Session">sessioni</param>
        /// <returns></returns>
        public static string MerrEmerRaporti(HttpSessionState session, string guidString)
        {
            var sessionKey = $"EmerRaporti{guidString}";
            if (MySessionCache[sessionKey] != null)
            {
                return Convert.ToString(MySessionCache[sessionKey]);
            }
            else return null;

        }
        public static bool RuajFaturatNgaBrmNeSession(HttpSessionState session, Tuple<string, string, string, string, string, DataTable> faturat)
        {
            if (MySessionCache["brmFaturat"] == null)
                MySessionCache.Add("brmFaturat", faturat);
            else MySessionCache["brmFaturat"] = faturat;
            return true;
        }

        public static Tuple<string, string, string, string, string, DataTable> MerrFaturatBRMngaSession(HttpSessionState session)
        {
            return (MySessionCache["brmFaturat"] as Tuple<string, string, string, string, string, DataTable>) ?? new Tuple<string, string, string, string, string, DataTable>(null, null, null, null, null, BrmAdapter.KrijoDataTableBoshPerArkaBanka());
        }

        public static void RuajNdermarrjeRolNeSession(HttpSessionState session, string guidString, int idRoli, DataTable ndermarrjet)
        {
            if (MySessionCache[guidString + idRoli] == null)
                MySessionCache.Add(guidString + idRoli, ndermarrjet);
            else
                MySessionCache[guidString + idRoli] = ndermarrjet;
        }

        public static DataTable MerrNdermarrjeRolNgaSession(HttpSessionState session, string guidString, int idRoli) =>
            (DataTable)MySessionCache[guidString + idRoli];

        #region GIS

        /// <summary>
        /// Ruahet ne session ndermarrja e punes ne menyre qe te riperdoret sa here qe te jete e nevojshme
        /// </summary>
        /// <param name="session">Sessioni</param>
        /// <param name="ndermarrjePune">klasa e ndermarrjes</param>
        /// <returns>true</returns>
        public static bool ruajNdermarrjePuneNeSession(HttpSessionState session, clsNdermarrje ndermarrjePune)
        {
            if (MySessionCache["NdermarrjePune"] == null)
                MySessionCache.Add("NdermarrjePune", ndermarrjePune);
            else
                MySessionCache["NdermarrjePune"] = ndermarrjePune;
            return true;
        }

        /// <summary>
        /// Merren te dhenat e ndermarrjes aktuale qe ruhen ne session
        /// </summary>
        /// <param name="session">sessioni</param>
        /// <returns>ndermarrjen e punes</returns>
        public static clsNdermarrje merrNdermarrjePuneNgaSession(HttpSessionState session)
        {
            if (session.IsNewSession)
                clsFunksione.logout(session, true, "MbarimSessioni");
            if (MySessionCache["NdermarrjePune"] == null)
                return null;
            else
                return (clsNdermarrje)MySessionCache["NdermarrjePune"];
        }

        /// <summary>
        /// Ruhet ne session mime type logo e ndermarrjes ne menyre qe te riperdoret sa here qe te jete e nevojshme
        /// </summary>
        /// <param name="session">Sessioni</param>
        /// <param name="logoNdermarrje">logo e ndermarrjes</param>
        /// <returns>true</returns>
        public static bool ruajMimeTypeLogoNdermarrjeNeSession(HttpSessionState session, string logoNdermarrje)
        {
            if (MySessionCache["MimeTypeLogoNdermarrje"] == null)
                MySessionCache.Add("MimeTypeLogoNdermarrje", logoNdermarrje);
            else
                MySessionCache["MimeTypeLogoNdermarrje"] = logoNdermarrje;
            return true;
        }

        /// <summary>
        /// Merren mime type logo e ndermarrjes aktuale qe ruhen ne session
        /// </summary>
        /// <param name="session">sessioni</param>
        /// <returns>logon e ndermarrjs e punes</returns>
        public static string merrMimeTypeLogoNdermarrjeNgaSession(HttpSessionState session)
        {
            if (session.IsNewSession)
                clsFunksione.logout(session, true, "MbarimSessioni");
            if (MySessionCache["MimeTypeLogoNdermarrje"] == null)
                return null;
            else
                return (string)MySessionCache["MimeTypeLogoNdermarrje"];
        }

        /// <summary>
        /// Ruhen ne session te drejtat e perdoruesit per te gjithe layerat ne modulin GIS
        /// </summary>
        /// <param name="session">sessioni</param>
        /// <param name="gisTeDrejtaSipasPerdorues">Te drejtat e perdoruesit te loguar</param>
        /// <returns>true</returns>
        public static bool ruajGISTeDrejtaSipasPerdoruesNeSession(HttpSessionState session, colTeDrejtaRoli gisTeDrejtaSipasPerdorues)
        {
            if (MySessionCache["GISTeDrejtaSipasPerdorues"] == null)
                MySessionCache.Add("GISTeDrejtaSipasPerdorues", gisTeDrejtaSipasPerdorues);
            else
                MySessionCache["GISTeDrejtaSipasPerdorues"] = gisTeDrejtaSipasPerdorues;
            return true;
        }

        /// <summary>
        /// Merren nga session te drejtat e perdoruesit te loguar
        /// </summary>
        /// <param name="session">sessioni</param>
        /// <returns>Kthen gjithe koleksionin e te drejtave te perdoruesit te loguar per modulin GIS</returns>
        public static colTeDrejtaRoli merrGISTeDrejtaSipasPerdoruesNgaSession(HttpSessionState session)
        {
            if (session.IsNewSession)
                clsFunksione.logout(session, true, "GISTeDrejtaSipasPerdorues");
            if (MySessionCache["GISTeDrejtaSipasPerdorues"] == null)
                return null;
            else
                return (colTeDrejtaRoli)MySessionCache["GISTeDrejtaSipasPerdorues"];
        }

        /// <summary>
        /// Ruhet ne session workspace per GIS
        /// </summary>
        /// <param name="session">sessioni</param>
        /// <param name="workspace">klasa e workspace aktual</param>
        /// <returns>true</returns>
        public static bool ruajWorkspaceNeSession(HttpSessionState session, clsWorkspaceGIS workspace)
        {
            if (MySessionCache["GISWorkspace"] == null)
                MySessionCache.Add("GISWorkspace", workspace);
            else
                MySessionCache["GISWorkspace"] = workspace;
            return true;
        }

        /// <summary>
        /// Merren te dhenat e workspace aktual qe ruhen ne session
        /// </summary>
        /// <param name="session">sessioni</param>
        /// <returns>workspace</returns>
        public static clsWorkspaceGIS merrWorkspaceNgaSession(HttpSessionState session)
        {
            if (session.IsNewSession)
                clsFunksione.logout(session, true, "MbarimSessioni");
            if (MySessionCache["GISWorkspace"] == null)
                return null;
            else
                return (clsWorkspaceGIS)MySessionCache["GISWorkspace"];
        }

        /// <summary>
        /// Ruhen ne session te gjithe tipet e layerave jane ne nivel konfigurimesh edhe mund te perdoren ne cdo ndermarrje edhe ne cdo rast
        /// </summary>
        /// <param name="session">sessioni</param>
        /// <param name="layersType">colection i tipeve te layerave</param>
        /// <returns>true</returns>
        public static bool ruajLayersTypeNeSession(HttpSessionState session, colLayersTypeGIS layersType)
        {
            if (MySessionCache["GISLayersType"] == null)
                MySessionCache.Add("GISLayersType", layersType);
            else
                MySessionCache["GISLayersType"] = layersType;
            return true;
        }

        /// <summary>
        /// Merren te dhenat e te gjithe tipeve te layerave qe ruhen ne session
        /// </summary>
        /// <param name="session">sessioni</param>
        /// <returns>layersType</returns>
        public static colLayersTypeGIS merrLayersTypeNgaSession(HttpSessionState session)
        {
            if (session.IsNewSession)
                clsFunksione.logout(session, true, "MbarimSessioni");
            if (MySessionCache["GISLayersType"] == null)
                return null;
            else
                return (colLayersTypeGIS)MySessionCache["GISLayersType"];
        }
        /// <summary>
        /// Ruhen ne session te gjithe layerat jane ne nivel konfigurimesh qe perdoren nga 
        /// </summary>
        /// <param name="session">sessioni</param>
        /// <param name="displayLayer">colection i layerave qe shfaqen ne projekt</param>
        /// <returns></returns>
        public static bool ruajDisplayLayersNeSession(HttpSessionState session, colDisplayLayersGIS displayLayer)
        {
            if (MySessionCache["GISDisplayLayers"] == null)
                MySessionCache.Add("GISDisplayLayers", displayLayer);
            else
                MySessionCache["GISDisplayLayers"] = displayLayer;
            return true;
        }
        /// <summary>
        /// Merren te dhenat e te gjithe tipeve te layerave qe ruhen ne session
        /// </summary>
        /// <param name="session">sessioni</param>
        /// <returns>layersType</returns>
        public static colDisplayLayersGIS merrDisplayLayersNgaSession(HttpSessionState session)
        {
            if (session.IsNewSession)
                clsFunksione.logout(session, true, "MbarimSessioni");
            if (MySessionCache["GISDisplayLayers"] == null)
                return null;
            else
                return (colDisplayLayersGIS)MySessionCache["GISDisplayLayers"];
        }
        #endregion

        #region Metoda per menune

        public static void ruajTeDrejtatNeSesion(HttpSessionState session, DataTable dt)
        {
            if (session.IsNewSession)
                clsFunksione.logout(session, true, "MbarimSessioni");

            if (MySessionCache["teDrejtat"] == null)
                MySessionCache.Add("teDrejtat", dt);
            else MySessionCache["teDrejtat"] = dt;
        }

        public static DataTable merrTeDrejtatNeSesion(HttpSessionState session)
        {
            if (session.IsNewSession)
                clsFunksione.logout(session, true, "MbarimSessioni");

            if (MySessionCache["teDrejtat"] == null)
                return new DataTable();
            else return (DataTable)MySessionCache["teDrejtat"];
        }

        /// <summary>
        /// ruan formatin e vleftes ne session
        /// </summary>
        /// <param name="formatvlefta">formati i vleftes</param>
        /// <param name="session">sesioni</param>
        /// <returns>true ose false nqs ruajta ka ndodhur me sukses</returns>
        public static bool ruajMenuPersonalizuar(HttpSessionState session, string menuPersonalizuar)
        {

            if (MySessionCache["menuPersonalizuar"] == null)
                MySessionCache.Add("menuPersonalizuar", menuPersonalizuar);
            else
                MySessionCache["menuPersonalizuar"] = menuPersonalizuar;
            return true;

        }

        public static string merrMenuPersonalizuar(HttpSessionState session)
        {
            if (session.IsNewSession)
                clsFunksione.logout(session, true, "MbarimSessioni");

            if (MySessionCache["menuPersonalizuar"] == null)
                return "";
            else return MySessionCache["menuPersonalizuar"].ToString();
        }

        public static bool ruajMenuSipasTeDrejtave(HttpSessionState session, DataTable menuSipasTeDrejtave)
        {

            if (MySessionCache["menuSipasTeDrejtave"] == null)
                MySessionCache.Add("menuSipasTeDrejtave", menuSipasTeDrejtave);
            else
                MySessionCache["menuSipasTeDrejtave"] = menuSipasTeDrejtave;
            return true;

        }

        public static DataTable merrMenuSipasTeDrejtave(HttpSessionState session)
        {
            if (session.IsNewSession)
                clsFunksione.logout(session, true, "MbarimSessioni");

            if (MySessionCache["menuSipasTeDrejtave"] == null)
                return null;
            else return (DataTable)MySessionCache["menuSipasTeDrejtave"];
        }

        #endregion
    }
}