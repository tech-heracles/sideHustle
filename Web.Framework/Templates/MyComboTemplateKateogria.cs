using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using DevExpress.Web;
using System.Web.UI.WebControls;

namespace PlatinumWeb.Templates
{
    public class MyComboTemplateKateogria : ITemplate //template i krijuar per monedhat te formati i numrave
    {        
        private int idNderviti;
        private bool formatNumri;

        public MyComboTemplateKateogria(int idNderviti, bool formatNr)
        {
            this.idNderviti = idNderviti;
            this.formatNumri = formatNr;
        }

        public void InstantiateIn(Control Container)
        {
            DbCore.DbRegjistrim.colKategoriNiveleDok col = new DbCore.DbRegjistrim.colKategoriNiveleDok();
            if (this.formatNumri)
              {
                col.mbushKategoriNivelDokPerFormatNumrash();
              }
            else
                col.mbushGjitheKategoriNivelDok();

            ASPxComboBox cmb = new ASPxComboBox();
            cmb.ClientSideEvents.TextChanged = "function(s,e){TextChangedKategoria(s, e); }";
            cmb.ClientSideEvents.Init = "function(s,e){InitKategoria(s, e);}";
            cmb.DropDownButton.Visible = true;
            
            cmb.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            cmb.DropDownStyle = DropDownStyle.DropDownList;
            GridViewDataItemTemplateContainer gridContainer = (GridViewDataItemTemplateContainer)Container;
            cmb.ID = "cmbBox";

            cmb.ValueField = "IdKategori";
            cmb.TextField = "Pershkrimi";
            cmb.DataSource = col;
            cmb.DataBind();            

            if (!(gridContainer.Text == "&nbsp;" || gridContainer.Text == "0"))
            {
                string text = "";
                text = gridContainer.Text;
                text = DbCore.clsFunksione.zevendesoKaraktere(text);
                cmb.Text = text;
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