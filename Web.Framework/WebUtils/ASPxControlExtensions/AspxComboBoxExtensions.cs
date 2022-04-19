using DevExpress.Web;
using System;
using System.Linq;
using DbCore.IMBUtils.Messages;


namespace PlatinumWeb.ApplicationUtils.ASPxControlExtensions
{
    public static class AspxComboBoxExtensions
    {
        public static void ConfigureAndFill<T>(this ASPxComboBox comboBox, Func<T> dsFunc, string textField, string valueField)
        {
            comboBox.DataSource = dsFunc.Invoke();
            comboBox.TextField = textField;
            comboBox.ValueField = valueField;
            comboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            comboBox.DataBind();
        }

        public static void ConfigureAndFill<T>(this ASPxComboBox comboBox, int selectedIndex) where T : struct, IComparable
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("T must be an enumerated type");

            foreach (T e in Enum.GetValues(typeof(T)))
            {
                if (Convert.ToInt32(e) < 0 || e.ToString() == "Undefined")
                    continue;

                comboBox.Items.Add(MessagesResource.Messages[e.ToString()], Convert.ToInt32(e));
            }

            comboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            comboBox.SelectedIndex = selectedIndex;
        }
    }
}