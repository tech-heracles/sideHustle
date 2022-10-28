using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DbCore.IMBUtils.Fiskalizimi.API;

using DbCore.DbAdmin;
using System.Xml;
using Newtonsoft.Json;

using System.Web.Script.Serialization; // requires the reference 'System.Web.Extensions'
using System.IO;



namespace PlatinumWeb
{
    public partial class FaturaBlerjeEinvoice : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            clsNdermarrje ndermarrje = new clsNdermarrje(idNdermarrje);

            //kemi kaluar te dhenat nga ndermarrja
            var xml = clsFunksioneFiskalizimi.merrFaturatEinvoice(ndermarrje,"blerje", DateTime.UtcNow);

            // kerkesa per fiskalizim
            
            var faturat = clsFunksioneFiskalizimi.merrVleratEFaturaveEinvoice(xml, "Einvoices", true);
            
            
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(faturat);
            
            var json = JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.None, true);
            hfState.Set("idNdermarrje",idNdermarrje);

           
           

            hfStateNdermarrje.Value = idNdermarrje.ToString();

            //pershtatja e re per hfstate - idNdermarrje
            hfState.Set("json", json);
            hfState.Set("idNdermarrje", idNdermarrje);

           


        }
    }
}