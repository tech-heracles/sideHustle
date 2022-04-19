using System;
using DevExpress.Web;

namespace PlatinumWeb.ApplicationUtils.ASPxControlExtensions
{
    public static class AspxCheckBoxListExtensions
    {
        public static void ConfigureAndFill<T>(this ASPxCheckBoxList checkBoxList, Func<T> dsFunc, string textField, string valueField)
        {
            checkBoxList.DataSource = dsFunc.Invoke();
            checkBoxList.TextField = textField;
            checkBoxList.ValueField = valueField;
            checkBoxList.DataBind();
        }
    }
}