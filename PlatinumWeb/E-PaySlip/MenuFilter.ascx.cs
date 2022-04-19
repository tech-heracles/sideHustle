using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DbCore.DbAdmin;
using System.Globalization;
using System.Resources;
using System.Reflection;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;

namespace PlatinumWeb.E_PaySlip
{
    public partial class MenuFilter : System.Web.UI.UserControl, ITemplate
    {
        
        //private string filtervlera;
        public static string emergrida = "";
        public static string emerfaqe = "";

        private string _imageFolderPath = "~/Images"; 
        public static object colekstioni = null;
        public static String TextField = String.Empty;
        public static String ValueField = String.Empty;
      
       
        public string ImageFolderPath
        {
            get { return _imageFolderPath; }
            set { _imageFolderPath = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ConfigureAspxComboBox.percaktoTemplateComboMeEnableCallbackPaButon(btnFiltra);
            ConfigureAspxComboBox.mbushComboFiltra(btnFiltra, colekstioni, ValueField, TextField);
            EmratLabelave(DbCore.mySessionObjects.ktheCultureInfo(Session));
        }

        void ITemplate.InstantiateIn(Control Container)
        {
            Container.Controls.Add(this);
        }

        protected void btnFiltra_TextBox_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (Page.IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("btnFiltra"))
                 {
                 //new DbCore.clsFunksione().mbushComboFiltra(btnFiltra, emergrida, emerfaqe);   
                 ConfigureAspxComboBox.mbushComboFiltra(btnFiltra,colekstioni,ValueField,TextField);   
                 }
            }
        }
        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmratLabelave(CultureInfo ci)
        {
          ResourceManager  rm = new ResourceManager("Resources.Strings",
                          System.Reflection.Assembly.Load("App_GlobalResources"));
           btnFshi.ToolTip = rm.GetString("tooltipBtnFshi", ci);
            Button1.Text = rm.GetString("buttonRuaj", ci);
        }

       
    }
}