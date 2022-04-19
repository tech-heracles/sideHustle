using System.Collections.Generic;
using System.Web.UI;
using DevExpress.Web;
using DbCore.IMBUtils.Extensions;

namespace PlatinumWeb.ApplicationUtils.ASPxControlExtensions
{
    public static class ControlExtensions
    {
        public static Dictionary<string, string> GetAsPxTextEditIdValue(this Control parent)
        {
            var result = new Dictionary<string, string>();
            foreach (Control control in parent.Controls)
            {
                if (control is ASPxTextBox || control is ASPxMemo)
                    if (control.ID != null)
                        result.Add(control.ID, ((ASPxTextEdit)control).Text);
                if (control.HasControls())
                    result.AddRange(control.GetAsPxTextEditIdValue());
            }
            return result;
        }
    }
}