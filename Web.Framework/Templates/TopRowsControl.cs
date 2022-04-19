using DbCore;
using DevExpress.Web;
using Newtonsoft.Json;
using PlatinumWeb;
using PlatinumWeb.ApplicationUtils.Filters;
using PlatinumWeb.Templates;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Framework.Templates
{
    public class TopRowsControl
    {
        public const string KeyFieldTopRows = "topRowsHfStateKey";
        public const string KeyParamNdryshimTopRows = "ndryshimTopRows";
        public const string KeyParamNdryshimMenyreFiltrimi = "ndryshimMenyreFiltrimi";

        private ASPxLabel _lblTopRows;
        private ASPxComboBox _cmbTopRows;
        private ASPxButton _btnKerko;
        private ASPxComboBox _cmbMenyraFiltrimi;
        private ASPxRadioButtonList  _radDtDok;
        private ASPxHiddenField _hfState;
        private ASPxGridView _grid;

        public int TopRows { get; set; }
        public int MenyreFiltrimi { get; set; }
        public string GetTopRowsPerSql()
        {
            return (TopRows == -1 || TopRows == 0) ? "" : TopRows.ToString();
        }
        public bool MePeriudhe { get; set; }

        public List<Control> KontrolletETopRows(ASPxRadioButtonList radDtDok, ASPxHiddenField hfState, ASPxGridView grid)
        {
            this._radDtDok = radDtDok;
            this._hfState = hfState;
            this._grid = grid;
            var topRowsControls = new List<Control>();
            InicializoLblTopRows();
            InicializoCmbTopRows();
            InicializoBtnKerko();
            InicializoBtnMenyreFiltrimi();
            var tblCell = new TableCell();
            topRowsControls.AddRange(new Control[] { _lblTopRows, _cmbTopRows, _cmbMenyraFiltrimi, _btnKerko });
            return topRowsControls;
        }

        private void InicializoBtnMenyreFiltrimi()
        {
            _cmbMenyraFiltrimi = new ASPxComboBox
            {
                ID = "cmbMenyreFiltrimi",
                ClientInstanceName = "cmbMenyreFiltrimi",
                ForeColor = Color.FromName("#0072c6"),
                Height = 16,
                Width = 85,
                CssPostfix = "Glass",
                DropDownStyle = DropDownStyle.DropDownList,
                ValueType = typeof(int),
                AutoPostBack = false,
            };
            _cmbMenyraFiltrimi.Border.BorderStyle = BorderStyle.Solid;
            _cmbMenyraFiltrimi.ItemStyle.Paddings.PaddingRight = 0;
            _cmbMenyraFiltrimi.Font.Bold = true;
            _cmbMenyraFiltrimi.PreRender += _cmbMenyraFiltrimi_PreRender;
            _cmbMenyraFiltrimi.ClientSideEvents.ValueChanged = CmbMenyreFiltrimiChangedClientSide();
            _cmbMenyraFiltrimi.Items.AddRange(new[]
            {
                new ListEditItem("Automatik",(int) GridViewFilterRowMode.Auto),
                new ListEditItem("Manual", (int)GridViewFilterRowMode.OnClick)

            });
        }
        
        private void InicializoLblTopRows()
        {
            _lblTopRows = new ASPxLabel { Text = "Shfaq: " };
            _lblTopRows.Font.Bold = false;
            _lblTopRows.Style.Add("margin-left", "10px");

        }

        private void InicializoBtnKerko()
        {
            _btnKerko = new ASPxButton
            {
                Text = "Kerko",
                ID = "btnKerko",
                ClientInstanceName = "btnKerko",
                ForeColor = Color.FromName("#0072c6"),
                Height = 16,
                Width = 60,
                CssPostfix = "Glass",
                AutoPostBack = false
            };
            _btnKerko.Font.Bold = true;
            _btnKerko.Style.Add("margin-left", "10px");
            _btnKerko.HoverStyle.ForeColor = Color.FromName("#ffffff");
            _btnKerko.ClientSideEvents.Click = BtnKerkoCliendSideClick();//BtnClickClientSide();
            _btnKerko.ClientVisible = MenyreFiltrimi == (int)GridViewFilterRowMode.OnClick;
        }

        private void InicializoCmbTopRows()
        {
            _cmbTopRows = new ASPxComboBox
            {
                ID = "cmbTopRows",
                SelectedIndex = -1,
                ClientInstanceName = "cmbTopRows",
                ForeColor = Color.FromName("#0072c6"),
                Height = 16,
                Width = 75,
                CssPostfix = "Glass",
                DropDownStyle = DropDownStyle.DropDown,
                ValueType = typeof(int),
            };
            _cmbTopRows.Border.BorderStyle = BorderStyle.Solid;
            _cmbTopRows.ItemStyle.Paddings.PaddingRight = 0;
            _cmbTopRows.Font.Bold = true;
            _cmbTopRows.Style.Add("margin-left", "7px");
            _cmbTopRows.PreRender += cmbTopRows_PreRender;
            if (MenyreFiltrimi == 0)
                _cmbTopRows.ClientSideEvents.ValueChanged = ValueChangedClientSide();
            MbushTopRows();
        }

        private void MbushTopRows()
        {
            var enumType = (MePeriudhe) ? typeof(TopRowsDocuments) : typeof(TopRowsEntities);
            foreach (var item in Enum.GetValues(enumType))
            {
                if ((int)item == -1)
                {
                    _cmbTopRows.Items.Add("Te Gjithe", -1);
                    continue;
                }
                _cmbTopRows.Items.Add(((int)item).ToString(), (int)item);
            }
        }


        #region EVENTET CLIENT SIDE
        private string ValueChangedClientSide()
        {
            if(MePeriudhe)
                return $@"function(s,e)
              {{
                 e.processOnServer=false;
                 var periudhat =JSON.parse({_hfState.ClientInstanceName}.Get('{TitlePeriudha.KeyFieldPeriudha}'));
                 periudhat.TopRowsControl.TopRows = {_cmbTopRows.ClientInstanceName}.GetValue();
                 {_hfState.ClientInstanceName}.Set('{TitlePeriudha.KeyFieldPeriudha}',JSON.stringify(periudhat));
                 {_grid.ClientInstanceName}.PerformCallback('{KeyParamNdryshimTopRows}');
              }}";

            return $@"function(s,e)
              {{
                 e.processOnServer=false;
                 var topRowsControl =JSON.parse({_hfState.ClientInstanceName}.Get('{KeyFieldTopRows}'));
                 topRowsControl.TopRows = {_cmbTopRows.ClientInstanceName}.GetValue();
                 {_hfState.ClientInstanceName}.Set('{KeyFieldTopRows}',JSON.stringify(topRowsControl));
                 {_grid.ClientInstanceName}.PerformCallback('{KeyParamNdryshimTopRows}');
              }}";
        }

        private string CmbMenyreFiltrimiChangedClientSide()
        {
            if(MePeriudhe)
                return $@"function(s,e)
              {{
                 var periudhat =JSON.parse({_hfState.ClientInstanceName}.Get('{TitlePeriudha.KeyFieldPeriudha}'));
                 periudhat.TopRowsControl.TopRows = {_cmbTopRows.ClientInstanceName}.GetValue();
                 {((_radDtDok != null) ? $@"periudhat.PeriudhaDok = {_radDtDok.ClientInstanceName}.GetValue();" : "")}  
                 periudhat.TopRowsControl.MenyreFiltrimi={_cmbMenyraFiltrimi.ClientInstanceName}.GetValue();
                 {_hfState.ClientInstanceName}.Set('{TitlePeriudha.KeyFieldPeriudha}',JSON.stringify(periudhat));
                 {_grid.ClientInstanceName}.PerformCallback('{KeyParamNdryshimMenyreFiltrimi}');
              }}";

            return $@"function(s,e)
              {{
                 var topRowsControl =JSON.parse({_hfState.ClientInstanceName}.Get('{KeyFieldTopRows}'));
                 topRowsControl.TopRows = {_cmbTopRows.ClientInstanceName}.GetValue();
                 topRowsControl.MenyreFiltrimi={_cmbMenyraFiltrimi.ClientInstanceName}.GetValue();
                 {_hfState.ClientInstanceName}.Set('{KeyFieldTopRows}',JSON.stringify(topRowsControl));
                 {_grid.ClientInstanceName}.PerformCallback('{KeyParamNdryshimMenyreFiltrimi}');
              }}";
        }
        private string BtnKerkoCliendSideClick()
        {
            if(MePeriudhe)
                return $@"function(s,e)
              {{
                 var periudhat =JSON.parse({_hfState.ClientInstanceName}.Get('{TitlePeriudha.KeyFieldPeriudha}'));
                 periudhat.TopRowsControl.TopRows = {_cmbTopRows.ClientInstanceName}.GetValue();
                 {((_radDtDok != null) ? $@"periudhat.PeriudhaDok = {_radDtDok.ClientInstanceName}.GetValue();" : "")}            
                 {_hfState.ClientInstanceName}.Set('{TitlePeriudha.KeyFieldPeriudha}',JSON.stringify(periudhat));
                 var modifiedAutoFilter ={_grid.ClientInstanceName}.filterHelper.GetChangedAutoFilterValues();
                 if (Object.keys(modifiedAutoFilter).length > 0) {_grid.ClientInstanceName}.filterHelper.ApplyMultiColumnAutoFilter();
                 else {_grid.ClientInstanceName}.PerformCallback('{GridUtil.CallbackParamApplyColumnFilter}');
              }}";

            return $@"function(s,e)
              {{
                 var topRowsControl =JSON.parse({_hfState.ClientInstanceName}.Get('{KeyFieldTopRows}'));
                 topRowsControl.TopRows = {_cmbTopRows.ClientInstanceName}.GetValue();        
                 {_hfState.ClientInstanceName}.Set('{KeyFieldTopRows}',JSON.stringify(topRowsControl));
                 var modifiedAutoFilter ={_grid.ClientInstanceName}.filterHelper.GetChangedAutoFilterValues();
                 if (Object.keys(modifiedAutoFilter).length > 0) {_grid.ClientInstanceName}.filterHelper.ApplyMultiColumnAutoFilter();
                 else {_grid.ClientInstanceName}.PerformCallback('{GridUtil.CallbackParamApplyColumnFilter}');
              }}";
        }
        #endregion

        #region Eventet
        private void cmbTopRows_PreRender(object sender, EventArgs e)
        {
            var cmbTopRows = sender as ASPxComboBox;
            if (cmbTopRows == null)
                throw new MyException("cmbTopRows eshte null!");
            cmbTopRows.Value = TopRows;
        }

        private void _cmbMenyraFiltrimi_PreRender(object sender, EventArgs e)
        {
            var cmbMenyraFiltrimi = sender as ASPxComboBox;
            if (cmbMenyraFiltrimi == null)
                throw new MyException("_cmbMenyraFiltrimi eshte null!");
            cmbMenyraFiltrimi.Value = MenyreFiltrimi;
        }
    }
    #endregion
}
