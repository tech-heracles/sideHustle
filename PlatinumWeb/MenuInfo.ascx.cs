using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using DevExpress.Web;

namespace PlatinumWeb
{
    public partial class MenuInfo : System.Web.UI.UserControl, ITemplate
    {
        protected void Page_Load(object sender, EventArgs e)
        {              
        }

        void ITemplate.InstantiateIn(Control Container)
        {
            Container.Controls.Add(this);
        }

        protected void mesazhList_ItemInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
        {
            mesazhList.Width = Unit.Percentage(100);
        }
    }
}