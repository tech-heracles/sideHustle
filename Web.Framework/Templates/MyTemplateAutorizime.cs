using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using DevExpress.Web;
using System.Web.UI.WebControls;
namespace PlatinumWeb.Templates
{
    public class MyTemplateAutorizime : ITemplate //template i krijuar per Autorizime
    {
        private int idNdermarrje;
        private int idPerdorues;
        public MyTemplateAutorizime(int idNdermarrje, int idPerdorues)
        {
            this.idNdermarrje = idNdermarrje;
            this.idPerdorues = idPerdorues;
        }
        public void InstantiateIn(Control Container)
        {
           
            DbCore.DbAdmin.colAutorizimetKoka colAutorizime = new DbCore.DbAdmin.colAutorizimetKoka();
            colAutorizime.Add(new DbCore.DbAdmin.clsAutorizimKoka());
            colAutorizime.mbushGjitheAutorizimet(this.idNdermarrje, this.idPerdorues);
            ASPxComboBox cmb = new ASPxComboBox();
            cmb.ClientInstanceName = "IdNivelAutorizimi";
            cmb.ClientSideEvents.KeyPress = "function(s,e){var code =_getKeyCode(e.htmlEvent);KeyPresAutorizime(code,IdNivelAutorizimi,0); }";
            cmb.ClientSideEvents.TextChanged = "function(s,e){TextChangedAutorizime(0); }";
            cmb.ClientSideEvents.LostFocus = "function(s,e){LostFocusAutorizime(0);}";
            cmb.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedAutorizime(IdNivelAutorizimi, 0) }";
            cmb.ClientSideEvents.Init = "function(s,e){InitAutorizim()}";
            cmb.DropDownButton.Visible = false;
           EditButton b1 = new EditButton();

            cmb.Buttons.Add(b1);
            cmb.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            cmb.DropDownStyle = DropDownStyle.DropDownList;
            cmb.TextFormatString = "{0}";
            GridViewDataItemTemplateContainer gridContainer = (GridViewDataItemTemplateContainer)Container;
            cmb.ID = "cmbBox";
            ListBoxColumn colprove = new ListBoxColumn();
            colprove.FieldName = "KodiAutorizim";
            ListBoxColumn colemer = new ListBoxColumn();
            colemer.FieldName = "PershkrimAutorizim";
            cmb.Columns.Add(colprove);
            cmb.Columns.Add(colemer);
            cmb.ValueField = "KodiAutorizim";
            cmb.DataSource = colAutorizime;
            cmb.DataBind();
            if (gridContainer.Column.FieldName == "IdNivelAutorizimi")
            {
                if (gridContainer.Text == "0")
                {
                    //cmb.Text = "";
                    cmb.SelectedIndex = -1;
                  //  cmb.Width = Unit.Percentage(100);
                }
                else
                {
                    if (gridContainer.Text == "&nbsp;")
                    {
                        cmb.Text = "";
                        cmb.SelectedIndex = -1;
                      //  cmb.Width = Unit.Percentage(100);
                    }
                    else
                    {
                        string text = "";
                        text = gridContainer.Text;

                        text = DbCore.clsFunksione.zevendesoKaraktere(text);
                        cmb.Text = text;
                       // cmb.Width = Unit.Percentage(100);
                    }
                }
            }
            else
            {
                if (gridContainer.Text == "&nbsp;")
                {
                    cmb.Text = "";
                }
                else
                {
                    string text = "";
                    text = gridContainer.Text;

                    text = DbCore.clsFunksione.zevendesoKaraktere(text);
                    cmb.Text = text;
                  //  cmb.Width = Unit.Percentage(100);
                }
            }
            Container.Controls.Add(cmb);
        }
    }
}