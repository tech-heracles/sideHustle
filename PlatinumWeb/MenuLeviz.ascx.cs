using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PlatinumWeb
{
    public partial class MenuLeviz : System.Web.UI.UserControl, ITemplate
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        void ITemplate.InstantiateIn(Control Container)
        {
            Container.Controls.Add(this);
        }

    }
}