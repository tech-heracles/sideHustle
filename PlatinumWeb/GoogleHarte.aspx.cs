using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class GoogleHarte : MyPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                int idviti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
                int idgjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
                int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                hfState.Set("idViti", idviti);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idGjuha", idgjuha);
                hfState.Set("idPerdoruesi", idPerdoruesi);
                string lloji = Request.QueryString["lloji"];
                if (!String.IsNullOrEmpty(lloji))
                {
                    string tip = "map";
                    DataTable dt;
                    switch (lloji)
                    {
                        case "mag":
                             dt = DbCore.DbRegjistrim.colNjesiAdministrative.ktheKoordinatatGjitheNjesiNdermarrjesDT(idNdermarrje, idPerdoruesi);
                            if (dt.Rows.Count > 0)
                            {
                                System.Web.Script.Serialization.JavaScriptSerializer serializusi = new System.Web.Script.Serialization.JavaScriptSerializer();
                                string[] geoms = dt.AsEnumerable().Select(r => r.Field<string>("Koordinata")).ToArray();
                                object[] names = new object[dt.Rows.Count];// dt.AsEnumerable().Select(r => r.Field<string>("PERSHKRIMI")).ToArray();
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    DataRow dr = dt.Rows[i];
                                    names[i] = new { pershkrimi = dr["PERSHKRIMI"]};
                                }
                                hfState.Set("geom", serializusi.Serialize(geoms));
                                hfState.Set("names", serializusi.Serialize(names));
                            }
                            break;
                        case "magshitje":
                            tip = "geochart";
                            dt = DbCore.DbRegjistrim.colNjesiAdministrative.ktheKoordinatatGjitheNjesiAdministrativeDheShitje(idNdermarrje, idPerdoruesi);
                            if (dt.Rows.Count > 0)
                            {
                                System.Web.Script.Serialization.JavaScriptSerializer serializusi = new System.Web.Script.Serialization.JavaScriptSerializer();
                                string[] geoms = dt.AsEnumerable().Select(r => r.Field<string>("Koordinata")).ToArray();
                                object[] names = new object[dt.Rows.Count];
                                for (int i=0;i<dt.Rows.Count;i++) { 
                                    DataRow dr = dt.Rows[i];
                                    names[i] = new { pershkrimi = dr["PERSHKRIMI"],  vlefta = dr["totalishitje"]};
                                }
                                hfState.Set("geom", serializusi.Serialize(geoms));
                                hfState.Set("names", serializusi.Serialize(names));
                            }
                            break;
                        case "klientshitje":
                            tip = "geochart";
                            dt = DbCore.DbKontabiliteti.colKlienteFurnitore.ktheKoordinatatGjitheKlientFurnitoreDheShitjeBlerje(idNdermarrje, idPerdoruesi,true);
                            if (dt.Rows.Count > 0)
                            {
                                System.Web.Script.Serialization.JavaScriptSerializer serializusi = new System.Web.Script.Serialization.JavaScriptSerializer();
                                string[] geoms = dt.AsEnumerable().Select(r => r.Field<string>("Koordinata")).ToArray();
                                object[] names = new object[dt.Rows.Count];
                                for (int i=0;i<dt.Rows.Count;i++) { 
                                    DataRow dr = dt.Rows[i];
                                    names[i] = new { pershkrimi = dr["PERSHKRIMI"],  vlefta = dr["totalishitje"]};
                                }
                                hfState.Set("geom", serializusi.Serialize(geoms));
                                hfState.Set("names", serializusi.Serialize(names));
                            }
                            break;
                        case "pikashitje":
                            tip = "geochart";
                            dt = DbCore.DbRegjistrim.colPikaShitjeFurnizimi.ktheKoordinatatGjithePikaveDheShitje(idNdermarrje, idPerdoruesi);
                            if (dt.Rows.Count > 0)
                            {
                                System.Web.Script.Serialization.JavaScriptSerializer serializusi = new System.Web.Script.Serialization.JavaScriptSerializer();
                                string[] geoms = dt.AsEnumerable().Select(r => r.Field<string>("Koordinata")).ToArray();
                                object[] names = new object[dt.Rows.Count];
                                for (int i=0;i<dt.Rows.Count;i++) { 
                                    DataRow dr = dt.Rows[i];
                                    names[i] = new { pershkrimi = dr["PERSHKRIMI"],  vlefta = dr["totalishitje"]};
                                }
                                hfState.Set("geom", serializusi.Serialize(geoms));
                                hfState.Set("names", serializusi.Serialize(names));
                            }
                            break;
                        case "marzhishitje":
                            tip = "geochart";
                            dt = DbCore.DbKontabiliteti.colKlienteFurnitore.ktheKoordinatatGjitheMarzhiShitje(idNdermarrje, idPerdoruesi);
                            if (dt.Rows.Count > 0)
                            {
                                System.Web.Script.Serialization.JavaScriptSerializer serializusi = new System.Web.Script.Serialization.JavaScriptSerializer();
                                string[] geoms = dt.AsEnumerable().Select(r => r.Field<string>("Koordinata")).ToArray();
                                object[] names = new object[dt.Rows.Count];
                                for (int i=0;i<dt.Rows.Count;i++) { 
                                    DataRow dr = dt.Rows[i];
                                    names[i] = new { pershkrimi = dr["PERSHKRIMI"], marzhi_vlere = dr["marzhvlere"], marzhi_perqindje = dr["marzhperqindje"] };
                                }
                                hfState.Set("geom", serializusi.Serialize(geoms));
                                hfState.Set("names", serializusi.Serialize(names));
                            }
                            break;
                        case "funritorblerje":
                            tip = "geochart";
                            dt = DbCore.DbKontabiliteti.colKlienteFurnitore.ktheKoordinatatGjitheKlientFurnitoreDheShitjeBlerje(idNdermarrje, idPerdoruesi, false);
                            if (dt.Rows.Count > 0)
                            {
                                System.Web.Script.Serialization.JavaScriptSerializer serializusi = new System.Web.Script.Serialization.JavaScriptSerializer();
                                string[] geoms = dt.AsEnumerable().Select(r => r.Field<string>("Koordinata")).ToArray();
                                object[] names = new object[dt.Rows.Count];
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    DataRow dr = dt.Rows[i];
                                    names[i] = new { pershkrimi = dr["PERSHKRIMI"], vlefta = dr["totalishitje"] };
                                }
                                hfState.Set("geom", serializusi.Serialize(geoms));
                                hfState.Set("names", serializusi.Serialize(names));
                            }
                            break;
                        case "maggjendje":
                            tip = "geochart";
                            dt = DbCore.DbRegjistrim.colNjesiAdministrative.ktheKoordinatatGjitheNjesiAdministrativeDheMagazinaGjendje(idNdermarrje, idPerdoruesi);
                            if (dt.Rows.Count > 0)
                            {
                                System.Web.Script.Serialization.JavaScriptSerializer serializusi = new System.Web.Script.Serialization.JavaScriptSerializer();
                                string[] geoms = dt.AsEnumerable().Select(r => r.Field<string>("Koordinata")).ToArray();
                                object[] names = new object[dt.Rows.Count];
                                for (int i=0;i<dt.Rows.Count;i++) { 
                                    DataRow dr = dt.Rows[i];
                                    names[i] = new { pershkrimi = dr["PERSHKRIMI"], vlefta_gjendje = dr["gjendja"]};
                                }
                                hfState.Set("geom", serializusi.Serialize(geoms));
                                hfState.Set("names", serializusi.Serialize(names));
                            }
                            break;
                        case "amortizimShqiptar":
                            tip = "geochart";
                            dt = DbCore.DbRegjistrim.colNjesiAdministrative.ktheKoordinatatGjitheNjesiAdministrativeDheAmortizimiShqiptar(idNdermarrje, idPerdoruesi);
                            if (dt.Rows.Count > 0)
                            {
                                System.Web.Script.Serialization.JavaScriptSerializer serializusi = new System.Web.Script.Serialization.JavaScriptSerializer();
                                string[] geoms = dt.AsEnumerable().Select(r => r.Field<string>("Koordinata")).ToArray();
                                object[] names = new object[dt.Rows.Count];
                                for (int i=0;i<dt.Rows.Count;i++) { 
                                    DataRow dr = dt.Rows[i];
                                    names[i] = new { pershkrimi = dr["PERSHKRIMI"], perqindje_amortizimi = dr["vleraGjendje"] };
                                }
                                hfState.Set("geom", serializusi.Serialize(geoms));
                                hfState.Set("names", serializusi.Serialize(names));
                            }
                            break;
                        case "klientKoordinata":
                            dt = DbCore.DbKontabiliteti.colKlienteFurnitore.ktheKoordinatatEKlienteve(idNdermarrje, idPerdoruesi);
                            if (dt.Rows.Count > 0)
                            {
                                System.Web.Script.Serialization.JavaScriptSerializer serializusi = new System.Web.Script.Serialization.JavaScriptSerializer();
                                string[] geoms = dt.AsEnumerable().Select(r => r.Field<string>("Koordinata")).ToArray();
                                object[] names = new object[dt.Rows.Count];
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    DataRow dr = dt.Rows[i];
                                    names[i] = new { pershkrimi = dr["PERSHKRIMI"] };
                                }
                                hfState.Set("geom", serializusi.Serialize(geoms));
                                hfState.Set("names", serializusi.Serialize(names));
                                hfState.Set("raporti", lloji);
                            }
                            break;
                    }
                    hfState.Set("type", tip); //geochart
                    
                }
                //string geomsToDeserialize = "";

                //object[] koordinata = new object[5];
                //for (int i = 0; i < 5; i++)
                //{
                //    koordinata[i] = "" 
                //}

            }
        }
    }
}