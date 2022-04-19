using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using DevExpress.Web;
using System.Web.UI.WebControls;

namespace PlatinumWeb.Templates
{
    public class MyComboTemplateArtPerb : ITemplate
    {
        private ListEditItemRequestedByValueEventHandler requestByValueHandler;
        private ListEditItemsRequestedByFilterConditionEventHandler requestByFilterHandler;
        //private DevExpress.Web.CallbackEventHandlerBase callBackHandler;
        public MyComboTemplateArtPerb(ListEditItemRequestedByValueEventHandler requestByValueHandler, ListEditItemsRequestedByFilterConditionEventHandler requestByFilterHandler
            //, DevExpress.Web.CallbackEventHandlerBase callBackHandler
            )
        {
            this.requestByValueHandler = requestByValueHandler;
            this.requestByFilterHandler = requestByFilterHandler;
            //this.callBackHandler = callBackHandler;
        }
        public void InstantiateIn(Control Container)
        {
            ASPxComboBox cmb = new ASPxComboBox();
            cmb.DropDownButton.Visible = true;
            cmb.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            cmb.DropDownStyle = DropDownStyle.DropDown;
            cmb.IncrementalFilteringDelay = 500;
            cmb.ItemRequestedByValue += this.requestByValueHandler;
            cmb.ItemsRequestedByFilterCondition += this.requestByFilterHandler;
            //cmb.Callback += callBackHandler;
            cmb.TextFormatString = "{0}";
            GridViewDataItemTemplateContainer gridContainer = (GridViewDataItemTemplateContainer)Container;
            cmb.DropDownButton.Visible = false;
            cmb.Buttons.Add();
            //cmb.AutoPostBack = false;
            cmb.EnableCallbackMode = true;
            cmb.EnableClientSideAPI = true;
            //cmb.IncrementalFilteringDelay = 300;
            cmb.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            cmb.CallbackPageSize = 10;
            cmb.EnableSynchronization = DevExpress.Utils.DefaultBoolean.True;

            ListBoxColumn colprove = new ListBoxColumn();
            colprove.FieldName = "KodArtikulli";
            colprove.Caption = "Kodi";
            ListBoxColumn colemer = new ListBoxColumn();
            colemer.FieldName = "PershkrimArtikulli";
            colemer.Caption = "Emri";
            cmb.ValueField = "IdLidhese";
            cmb.Columns.Clear();
            cmb.ValueType = typeof(System.Int32);
            
            cmb.Columns.Add(colprove);
            cmb.Columns.Add(colemer);
            cmb.TextFormatString = "{0}";
            cmb.ID = "cmbBox";
            int value;
            if (!(gridContainer.Text == "&nbsp;" || gridContainer.Text == "0" || !int.TryParse(gridContainer.Text,out value)))
            {
                DbCore.DbInventari.clsArtikulli art = new DbCore.DbInventari.clsArtikulli(value);
                object[] colArtikuj;
                if (art.IdArtikulli == -1)
                    return;
                colArtikuj = new object[1];
                colArtikuj[0] = new { IdLidhese = art.IdArtikulli, KodArtikulli = art.KodArtikulli, PershkrimArtikulli = art.PershkrimArtikulli };

                //string text = "";
                //text = gridContainer.Text;

                //text = DbCore.clsFunksione.zevendesoKaraktere(text);
                
                cmb.DataSource = colArtikuj;
                cmb.DataBind();
                cmb.Value = value;
                if (cmb.Items.Count != 0)
                    cmb.Items[0].Selected = true;
                cmb.Width = Unit.Percentage(100);
            }
            else
            {
                cmb.SelectedIndex = -1;
                cmb.Width = Unit.Percentage(100);
            }
            Container.Controls.Add(cmb);
        }
    }
}