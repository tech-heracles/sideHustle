using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using DevExpress.Web;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGauges;
using DevExpress.Web.ASPxGauges.Gauges.State;
using System.Drawing;
using DevExpress.Web.ASPxGauges.Gauges;
using DevExpress.XtraExport.Helpers;

namespace PlatinumWeb.Templates
{
    public class MyGaugeTemplate : ITemplate
    {
        public void InstantiateIn(Control Container)
        {
            ASPxGaugeControl text = new ASPxGaugeControl() { ID="gague"};
            text.BackColor = Color.Transparent;
            text.SaveStateOnCallbacks = true;
            StateIndicatorGauge g = new StateIndicatorGauge();
            StateIndicatorComponent com = new StateIndicatorComponent("ind");
            IndicatorStateWeb s1 = new IndicatorStateWeb(DevExpress.XtraGauges.Core.Model.StateIndicatorShapeType.ElectricLight4);
            IndicatorStateWeb s2 = new IndicatorStateWeb(DevExpress.XtraGauges.Core.Model.StateIndicatorShapeType.ElectricLight2);
            IndicatorStateWeb s3 = new IndicatorStateWeb(DevExpress.XtraGauges.Core.Model.StateIndicatorShapeType.ElectricLight3);
            IndicatorStateWeb s0 = new IndicatorStateWeb(DevExpress.XtraGauges.Core.Model.StateIndicatorShapeType.ElectricLight1);

            Rectangle rec = new Rectangle(0, 0, 20, 20);
            g.Bounds = rec;
            com.States.Add(s0);
            com.States.Add(s1);
            com.States.Add(s2);
            com.States.Add(s3);
            Point p = new Point(10, 10);
            com.Center = p;
            SizeF f = new SizeF(20, 20);
            com.Size = f;
            g.Indicators.Add(com);
            text.Gauges.Add(g);
            
            text.Width = 20;
            text.Height = 20;

            GridViewDataItemTemplateContainer gridContainer = (GridViewDataItemTemplateContainer)Container;
             if (gridContainer.Text == "&nbsp;")
            {
                com.StateIndex = 0;
            }
            else if (gridContainer.Text == "gri")
            {
                com.StateIndex = 0;
            }
            else if (gridContainer.Text == "verdhe")
            {
                com.StateIndex = 3;
            }
            else if (gridContainer.Text == "kuqe")
            {
                com.StateIndex = 2;
            }
            else if (gridContainer.Text == "gjelber")
            {
                com.StateIndex = 1;
            }
            Container.Controls.Add(text);

        }
    }
}