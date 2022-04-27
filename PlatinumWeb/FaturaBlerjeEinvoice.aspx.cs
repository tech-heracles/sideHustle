using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DbCore.IMBUtils.Fiskalizimi.API;

using DbCore.DbAdmin;
using System.Xml;
using Newtonsoft.Json;

namespace PlatinumWeb
{
    public partial class FaturaBlerjeEinvoice : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            clsNdermarrje ndermarrje = new clsNdermarrje(idNdermarrje);

            //kemi kaluar te dhenat nga ndermarrja
            var xml = clsFunksioneFiskalizimi.merrFaturatEinvoice(ndermarrje, "blerje", DateTime.UtcNow);

            // kerkesa per fiskalizim

            var faturat = clsFunksioneFiskalizimi.merrVleratEFaturaveEinvoice(xml, "ns2:Einvoices", true);


            XmlDocument doc = new XmlDocument();
            doc.LoadXml(faturat);

            var json = JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.None, true);
            hfState.Value = json;



            hfStateNdermarrje.Value = idNdermarrje.ToString();



        }
    }
}