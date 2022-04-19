using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DbCore.DbAdmin;
using System.Globalization;
using System.Resources;
using System.Reflection;
using DbCore;

namespace PlatinumWeb.E_PaySlip
{
    public partial class MenuFrame : System.Web.UI.UserControl, ITemplate
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            int idGjuha;
            if (!IsPostBack)
            {
                idGjuha = mySessionObjects.ktheGjuhe(Session);
                hfIdGjuha.Set("idGjuha", idGjuha);
            }
            else
                if (hfIdGjuha.Contains("idGjuha"))
                    idGjuha = Convert.ToInt16(hfIdGjuha.Get("idGjuha"));
                else
                {
                    idGjuha = mySessionObjects.ktheGjuhe(Session);
                    hfIdGjuha.Set("idGjuha", idGjuha);
                }
            EmratLabelave(DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo(idGjuha));
        }

        void ITemplate.InstantiateIn(Control Container)
        {
            Container.Controls.Add(this);
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmratLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                         System.Reflection.Assembly.Load("App_GlobalResources"));
            btnHelp.ToolTip = rm.GetString("tooltipBtnHelp", ci);
            btnCollapseAll.ToolTip = rm.GetString("tooltipBtnCollapseAll", ci);
            btnExpandAll.ToolTip = rm.GetString("tooltipBtnExpandAll", ci);
        }
    }
}