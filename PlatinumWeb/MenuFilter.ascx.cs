using System;
using System.Web;
using System.Web.UI;
using DevExpress.Web;
using System.Linq;
using DbCore.IMBUtils.Messages;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;

namespace PlatinumWeb
{
    public partial class MenuFilter : UserControl, ITemplate
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var parentPage = HttpContext.Current.Handler as Page;
            var cmbKonfigurimi = FindControlRecursive(parentPage, "cmbKonfigurimi") as ASPxComboBox;
            var idKonfigurimi = cmbKonfigurimi?.Value == null ? 1 : Convert.ToInt32(cmbKonfigurimi.Value);

            var myModel = DbCore.mySessionObjects.MerrNgaSession<DbCore.MyMenuFilterModel>(HttpContext.Current.Session, $"filtraGride_{idKonfigurimi}") ?? new DbCore.MyMenuFilterModel();

            ConfigureAspxComboBox.percaktoTemplateComboMeEnableCallbackPaButon(btnFiltra);

            if (myModel != null)
                ConfigureAspxComboBox.mbushComboFiltra(btnFiltra, myModel.DataSource, myModel.ValueField, myModel.TextField, myModel.ValueToSelect);

            EmratLabelave();
        }

        void ITemplate.InstantiateIn(Control container) => container.Controls.Add(this);

        protected Control FindControlRecursive(Control root, string id) =>
            root.ID == id
                ? root
                : (from Control control in root.Controls select FindControlRecursive(control, id)).FirstOrDefault(foundControl => foundControl != null);

        protected void btnFiltra_TextBox_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (!Page.IsCallback || !Request.Params["__CALLBACKID"].Contains("btnFiltra")) return;

            var parentPage = HttpContext.Current.Handler as Page;
            var cmbKonfigurimi = FindControlRecursive(parentPage, "cmbKonfigurimi") as ASPxComboBox;
            var idKonfigurimi = cmbKonfigurimi == null ? 1 : Convert.ToInt32(cmbKonfigurimi.Value);

            var myModel =
                DbCore.mySessionObjects.MerrNgaSession<DbCore.MyMenuFilterModel>(HttpContext.Current.Session, $"filtraGride_{idKonfigurimi}");

            ConfigureAspxComboBox.mbushComboFiltra(btnFiltra, myModel.DataSource, myModel.ValueField, myModel.TextField, myModel.ValueToSelect);
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmratLabelave()
        {
            btnFshi.ToolTip = MessagesResource.Messages["tooltipBtnFshi"];
            Button1.ToolTip = MessagesResource.Messages["buttonRuaj"];
        }
    }
}