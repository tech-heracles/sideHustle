using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Resources;
using System.Reflection;

namespace PlatinumWeb
{
    public partial class MenuExport : System.Web.UI.UserControl, ITemplate
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            EmratLabelave(DbCore.mySessionObjects.ktheCultureInfo(Session));
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