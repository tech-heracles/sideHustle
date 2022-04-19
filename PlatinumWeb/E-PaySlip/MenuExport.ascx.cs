using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
using DbCore;

namespace PlatinumWeb.E_PaySlip
{
    public partial class MenuExport : System.Web.UI.UserControl, ITemplate
    {
        int idGjuha;
        protected void Page_Load(object sender, EventArgs e)
        {
            idGjuha = mySessionObjects.ktheGjuhe(Session);
            EmratLabelave(DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo(idGjuha));
        }

        void ITemplate.InstantiateIn(Control Container)
        {
            Container.Controls.Add(this);
        }
        private void EmratLabelave(CultureInfo ci)
        {
          ResourceManager  rm = new ResourceManager("Resources.Strings",
                          System.Reflection.Assembly.Load("App_GlobalResources"));
           
            exportButton.Text = rm.GetString("buttonEksport", ci);
        }

    }
}