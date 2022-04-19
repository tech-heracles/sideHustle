using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using DevExpress.Web;
using System.Web.UI.WebControls;
namespace PlatinumWeb.Templates
{
    public class MyTemplatePrindiGrupKF : ITemplate //template i krijuar per Autorizime
    {
        private int llojKodifikimi;
        private int llojKF;
        private int idNdermarrje;

        public MyTemplatePrindiGrupKF(int idNdermarrje, int llojKodifikimi, int llojKF)
        {
            this.llojKodifikimi = llojKodifikimi;
            this.llojKF = llojKF;
            this.idNdermarrje = idNdermarrje;
        }
        public void InstantiateIn(Control Container)
        {
            DbCore.DbKontabiliteti.colGrupeKF col = new DbCore.DbKontabiliteti.colGrupeKF();
            DbCore.DbKontabiliteti.clsGrupeKF grup = new DbCore.DbKontabiliteti.clsGrupeKF();
             grup.IdGrupi = 0;
            col.Add(grup);
            col.MerrGrupeKfSipasLlojKodifikimiDheLlojKf(llojKodifikimi, this.idNdermarrje, llojKF);
            
            ASPxComboBox cmb = new ASPxComboBox();
            cmb.ClientInstanceName = "IdPrindi";
            cmb.ClientSideEvents.KeyPress = "function(s,e){var code =_getKeyCode(e.htmlEvent);KeyPresPrind(code,IdPrindi,0); }";
            cmb.ClientSideEvents.TextChanged = "function(s,e){TextChangedPrind(0); }";
            cmb.ClientSideEvents.LostFocus = "function(s,e){LostFocusPrind(0);}";
            cmb.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedPrind(IdPrindi, 0); }";
            cmb.ClientSideEvents.Init = "function(s,e){InitPrind();}";
            cmb.DropDownButton.Visible = false;
            EditButton b1 = new EditButton();

            cmb.Buttons.Add(b1);
            cmb.IncrementalFilteringMode = IncrementalFilteringMode.StartsWith;
            cmb.DropDownStyle = DropDownStyle.DropDownList;
            cmb.TextFormatString = "{0},{1},{2}";
            GridViewDataItemTemplateContainer gridContainer = (GridViewDataItemTemplateContainer)Container;
            cmb.ID = "cmbBox";

            ListBoxColumn colprove = new ListBoxColumn();
            colprove.FieldName = "KodGrupi";
            ListBoxColumn colemer = new ListBoxColumn();
            colemer.FieldName = "PershkrimGrupi";
            ListBoxColumn colNiveli = new ListBoxColumn();
            colNiveli.FieldName = "NivelGrupi";

            cmb.Columns.Add(colprove);
            cmb.Columns.Add(colemer);
            cmb.Columns.Add(colNiveli);

            cmb.ValueField = "IdGrupi";
            cmb.DataSource = col;
            cmb.DataBind();
            if (gridContainer.Column.FieldName == "IdPrindi")
            {
                if (gridContainer.Text == "0")
                {
                    //cmb.Text = "";
                    cmb.SelectedIndex = -1;
                    cmb.Width = Unit.Percentage(100);

                }
                else
                {
                    if (gridContainer.Text == "&nbsp;")
                    {
                        //cmb.Text = "";
                        cmb.SelectedIndex = -1;
                        cmb.Width = Unit.Percentage(100);
                    }
                    else
                    {
                        string text = "";
                        text = gridContainer.Text;
                       // DbCore.clsFunksione funksion = new DbCore.clsFunksione(DbCore.mySessionObjects.ktheCultureInfo(Session));
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
                   // DbCore.clsFunksione funksion = new DbCore.clsFunksione(DbCore.mySessionObjects.ktheCultureInfo(Session));
                    text = DbCore.clsFunksione.zevendesoKaraktere(text);
                    cmb.Text = text;
                    cmb.Width = Unit.Percentage(100);
                }
            }
            Container.Controls.Add(cmb);
        }
    }
}