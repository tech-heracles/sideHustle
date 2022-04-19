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
    public partial class RaportHarta : MyPageBase
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
                if (!String.IsNullOrEmpty(lloji)) {
                    DataTable dt = DbCore.DbRegjistrim.colNjesiAdministrative.ktheKoordinatatGjitheNjesiNdermarrjesDT(idNdermarrje, idPerdoruesi);
                    if (dt.Rows.Count > 0)
                    {
                        System.Web.Script.Serialization.JavaScriptSerializer serializusi = new System.Web.Script.Serialization.JavaScriptSerializer();
                        string[] geoms = dt.AsEnumerable().Select(r => r.Field<string>("Koordinata")).ToArray();                        
                        hfState.Set("geom", serializusi.Serialize(geoms));
                    }
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