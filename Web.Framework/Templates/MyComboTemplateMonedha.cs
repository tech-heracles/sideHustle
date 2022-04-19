using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using DevExpress.Web;
using System.Web.UI.WebControls;
using System.Reflection;
using System.Configuration;
using System.Globalization;
namespace PlatinumWeb.Templates
{
    public class MyComboTemplateMonedha : ITemplate //template i krijuar per monedhat te formati i numrave
    {
        private int idNdermarrje;
        private int idPerdorues;
        private bool enabled;

        public MyComboTemplateMonedha(CultureInfo ci, int idNdermarrje, int idPerdorues, bool enabled)
        {
            this.idNdermarrje = idNdermarrje;
            this.idPerdorues = idPerdorues;
            this.enabled = enabled;
        }
        public void InstantiateIn(Control Container)
        {
            DbCore.DbAdmin.colMonedhat col = new DbCore.DbAdmin.colMonedhat();
            col.mbushGjitheMonedhatAktive(this.idNdermarrje, this.idPerdorues);

            ASPxComboBox cmb = new ASPxComboBox();
            cmb.ClientInstanceName = "IdMonedha";
            //cmb.ClientSideEvents.TextChanged = "function(s,e){TextChangedMonedha(s, e); }";
            //cmb.ClientSideEvents.ButtonClick = "function(s,e){ButtonClickedMonedha(s, e); }";
            //cmb.ClientSideEvents.Init = "function(s,e){InitMonedha(s, e);}";
            cmb.DropDownButton.Visible = false;
            EditButton b1 = new EditButton();

            cmb.Buttons.Add(b1);
            cmb.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            cmb.DropDownStyle = DropDownStyle.DropDownList;
            cmb.TextFormatString = "{0},{1}";
            GridViewDataItemTemplateContainer gridContainer = (GridViewDataItemTemplateContainer)Container;
            cmb.ID = "cmbBox";

            ListBoxColumn colprove = new ListBoxColumn();
            colprove.FieldName = "KodiMonedha";
            ListBoxColumn colemer = new ListBoxColumn();
            colemer.FieldName = "PershkrimiMonedha";

            cmb.Columns.Add(colprove);
            cmb.Columns.Add(colemer);

            cmb.ValueField = "IdMonedha";
            cmb.DataSource = col;
            cmb.DataBind();
            if (!enabled)
                cmb.ClientEnabled = false;
            else cmb.ClientEnabled = true;

            if (gridContainer.Column.FieldName == "IdMonedha")
            {
                if (gridContainer.Text == "0")
                {
                    cmb.SelectedIndex = -1;
                    cmb.Width = Unit.Percentage(100);

                }
                else
                {
                    if (gridContainer.Text == "&nbsp;")
                    {
                        cmb.SelectedIndex = -1;
                        cmb.Width = Unit.Percentage(100);
                    }
                    else
                    {
                        string text = "";
                        text = gridContainer.Text;
                        //DbCore.clsFunksione funksion = new DbCore.clsFunksione(ci);
                        text = DbCore.clsFunksione.zevendesoKaraktere(text);
                        cmb.Text = text;
                        cmb.Width = Unit.Percentage(100);
                    }
                }
            }
            else
            {
                if (gridContainer.Text == "&nbsp;")
                {
                    cmb.Text = gridContainer.Text;
                }
                else
                {
                    string text = "";
                    text = gridContainer.Text;
                    // DbCore.clsFunksione funksion = new DbCore.clsFunksione(ci);
                    text = DbCore.clsFunksione.zevendesoKaraktere(text);
                    cmb.Text = text;
                    cmb.Width = Unit.Percentage(100);
                }
            }
            Container.Controls.Add(cmb);
        }
    }
}