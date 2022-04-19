using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlphaWeb.Services;

namespace PlatinumWeb.Test
{
    public partial class TestDI : System.Web.UI.Page
    {
        public ITestService testService { get; set; }


        protected void Page_Load(object sender, EventArgs e)
        {

            Label1.Text = testService.GetTestData();
           var srv= AlphaWeb.Core.Infrastructure.EngineContext.Current.Resolve<ITestService>();
        }
    }
}