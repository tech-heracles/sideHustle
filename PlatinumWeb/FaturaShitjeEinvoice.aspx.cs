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
    public partial class FaturaShitjeEinvoice : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            clsNdermarrje ndermarrje = new clsNdermarrje(idNdermarrje);

            //kemi kaluar te dhenat nga ndermarrja
            string xml;
            try
            {
                xml = clsFunksioneFiskalizimi.merrFaturatEinvoice(ndermarrje, "shitje", DateTime.UtcNow);
            }
            catch (Exception ex)
            {
                ShfaqGabimin(ex.Message);
                return;
            }

            // kerkesa per fiskalizim
            
            var faturat = clsFunksioneFiskalizimi.merrVleratEFaturaveEinvoice(xml, "Einvoices", true) ;
            
            
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(faturat);
            
            var json = JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.None, true);
            hfState.Set("idNdermarrje",idNdermarrje);

           
           

            hfStateNdermarrje.Value = idNdermarrje.ToString();

            //pershtatja e re per hfstate - idNdermarrje

            hfState.Set("json", json);
            hfState.Set("idNdermarrje", idNdermarrje);

           


        }

        /// <summary>
        /// Shfaq arsyen pse faturat e-invoice nuk u ngarkuan (p.sh. mungon certifikata e fiskalizimit), ne vend te faqes se gabimit.
        /// </summary>
        private void ShfaqGabimin(string mesazhi)
        {
            Response.Clear();
            Response.ContentType = "text/html";
            Response.Write("<!DOCTYPE html><html><head><meta charset=\"utf-8\"><link href=\"App_Themes/Avec/avec.css\" rel=\"stylesheet\" /></head>"
                + "<body style=\"padding:24px\"><h3 style=\"margin:0 0 8px;font-size:16px;color:#f2f2f0\">Faturat e-invoice nuk mund të ngarkohen</h3>"
                + "<p style=\"margin:0;color:#a3a3a0\">" + HttpUtility.HtmlEncode(mesazhi) + "</p></body></html>");
            Response.End();
        }
    }
}