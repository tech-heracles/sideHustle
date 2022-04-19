using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Web.UI;
using System.Web.UI.WebControls;
using DbCore;
using DbCore.DbShare;
using DevExpress.Web;
using Newtonsoft.Json;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Filters;
using PlatinumWeb.ApplicationUtils.Pages;
using Web.Framework.Templates;

namespace PlatinumWeb.Templates
{
    public class MyTitlePanelTemplateMePeriudhe : MyBaseTitlePanelTemplate
    {
        private bool _raiseValueChanged;
        private ASPxButton _btnfilterDefault;
        private ASPxRadioButtonList _radDtDok;
        private ASPxLabel _lblPeriudha;
        protected ASPxRadioButtonList RadDtDok => _radDtDok;
        protected TitlePeriudha Periudhat { get; set; }
        protected ASPxLabel LblPeriudha => _lblPeriudha;
		protected ASPxButton BtnFilterDefault => _btnfilterDefault;
        public bool PeriudheGjitheVitet { get; set; }

        public MyTitlePanelTemplateMePeriudhe(string emriGrides,Page page, ASPxMenu menuMesazhesh, UpdatePanel pnlMesazhi, ASPxHiddenField hfState, int idkonfigAmbjenti, int idPerdoruesi, int idNdermarrje, int idViti, int idGjuha, string komponente, int idKomponente, string renditjeDefault, ResourceManager rm, CultureInfo cultureInfo, bool meExpandCollapse, GridViewExportedRowType exportType, bool periudheGjitheVitet, bool vetemPeriudha) : base(emriGrides,page, menuMesazhesh, pnlMesazhi, hfState, idkonfigAmbjenti, idPerdoruesi, idNdermarrje, idViti, idGjuha, komponente, idKomponente, renditjeDefault, rm, cultureInfo, meExpandCollapse, exportType, false, true, vetemPeriudha)
        {
            PeriudheGjitheVitet = periudheGjitheVitet;
            VendosPeriudhat();
        }


        /// <summary>
        /// inicializimi i template-it
        /// </summary>
        /// <param name="container"></param>
        public override void InstantiateIn(Control container)
        {
            Grid = ((DevExpress.Web.GridViewTitleTemplateContainer)container).Grid;
            var tbl = KrijoTable(1, 12);
            MbushTableMeControle(tbl, BaseControls(), KontrolletEPeriudhes());
            container.Controls.Add(tbl);
        }

        #region INICIALIZIM KONTROLLESH
        protected void InicializoLblPeriudha()
        {
            _lblPeriudha = new ASPxLabel
            {
                Text = rm.GetString("RadioButtonListEditItemPeriudha", cultinf)
            };
        }

        protected void InicializoRadioDtDok(bool raiseValueChanged, bool periudheTreDitore)
        {
            _radDtDok = new ASPxRadioButtonList
            { 
                ID = "radDtDok",
                SelectedIndex = -1,
                ClientInstanceName = "radDtDok",
                RepeatColumns = 6,
                ForeColor = Color.FromName("#0072c6"),
                Height = 16,
                CssPostfix = "Glass"

            };
            _radDtDok.Border.BorderStyle = BorderStyle.None;
            _radDtDok.Font.Bold = true;
            var items = new List<ListEditItem>();
            items.Add( new ListEditItem
                {
                    Text = rm.GetString("RadioButtonListEditItemDitore", cultinf),
                    Value = LlojPeriudhe.Ditore
                });
            if(periudheTreDitore)
                items.Add(new ListEditItem
                {
                    Text = rm.GetString("RadioButtonListEditItem3Ditore", cultinf),
                    Value = LlojPeriudhe.TreDitore
                });
            items.Add(new ListEditItem
            {
                Text = rm.GetString("RadioButtonListEditItemJavore", cultinf),
                Value = LlojPeriudhe.Javore
            });
            items.Add(new ListEditItem
            {
                Text = rm.GetString("RadioButtonListEditItemAktuale", cultinf),
                Value = LlojPeriudhe.Aktuale
            });
            items.Add(new ListEditItem
            {
                Text = rm.GetString("RadioButtonListEditItem3Mujore", cultinf),
                Value = LlojPeriudhe.TreMujore
            });
            items.Add(new ListEditItem
            {
                Text = rm.GetString("RadioButtonListEditItemVitUshtrimor", cultinf),
                Value = LlojPeriudhe.VitUshtrimor
            });
            if (PeriudheGjitheVitet)
                items.Add(new ListEditItem
                {
                    Text = rm.GetString("RadioButtonListEditItemGjitheVitet", cultinf),
                    Value = LlojPeriudhe.GjitheVitet
                });
            _radDtDok.Items.AddRange(items.ToArray());
            _radDtDok.PreRender += radDtDok_PreRender;
            if (raiseValueChanged)
                _radDtDok.ClientSideEvents.ValueChanged = ValueChangedClientSide();
        }

        protected void InicializoFilterDefault()
        {
             _btnfilterDefault = new ASPxButton
            {
                ID = "filterDefault",
                ClientInstanceName = "filterDefault",
                ImageUrl = $"images/theme/{CurrentPage.Theme}/grida/filter_default.png",
                ToolTip = rm.GetString("regjisDokBtnToolTipAplikoFilterDefault", cultinf)
            };
           // _btnfilterDefault.Click += btnfilterDefault_Click;
			_btnfilterDefault.Image.UrlHottracked = $"images/theme/{CurrentPage.Theme}/grida/filter_default_W.png";
            _btnfilterDefault.Image.Height = 16;
            _btnfilterDefault.ClientSideEvents.Click = $@"function(s,e)
          {{
             {Grid.ClientInstanceName}.PerformCallback('{GridUtil.APLIKOFILTERDEFAULT}');
          }}";
            _btnfilterDefault.AutoPostBack = false;
            //ScriptManager.RegisterPostBackControl(_btnfilterDefault);
        }
        #endregion

        protected virtual List<Control> KontrolletEPeriudhes(bool raiseValueChanged = true)
        {
            var periudhaControls = new List<Control>();
            if (!VetemPeriudha)
            {
                InicializoFilterDefault();
                InicializoLblPeriudha();
                InicializoRadioDtDok(raiseValueChanged, true);

                periudhaControls.AddRange(new Control[] { _btnfilterDefault, new TableCell(), LblPeriudha, RadDtDok });
            }
            else
            {
                InicializoLblPeriudha();
                InicializoRadioDtDok(raiseValueChanged, false);

                periudhaControls.AddRange(new Control[] { new TableCell(), LblPeriudha, RadDtDok });
            }
            return periudhaControls;
        }
        private void VendosPeriudhat()
        {
            //todo riemerimi i periudhave ne db
            if (!CurrentPage.IsPostBack)
            {
                var periudhaKontabel = mySessionObjects.merrPeriudheKontabel(Session);
                Periudhat = new TitlePeriudha
                {
                    DataNgaAktuale = periudhaKontabel.FillimiPeriudha.ToString("dd/MM/yyyy"),
                    DataDeriAktuale = periudhaKontabel.MbarimiPeriudha.ToString("dd/MM/yyyy"),
                    DataNgaViti = $"01/01/{periudhaKontabel.MbarimiPeriudha.Year}",
                    DataDeriViti = $"31/12/{periudhaKontabel.MbarimiPeriudha.Year}",
                    PeriudhaDefault = clsAlternativaKushti.getAlternativa(idKonfig, "SHDPER"),
                    IdViti = periudhaKontabel.IdViti

                };
                Periudhat.PeriudhaDefault = string.IsNullOrWhiteSpace(Periudhat.PeriudhaDefault) ? LlojPeriudhe.Aktuale.ToString() : TitlePeriudha.MappingPeriudha[Periudhat.PeriudhaDefault];
                Periudhat.PeriudhaDok = Periudhat.PeriudhaDefault;
                Grid.PastroDataSourceNgaSessioni(emerKomponente, idViti, Session);
                Periudhat.TopRowsControl = new TopRowsControl
                {
                    TopRows = (gridaKoka.TopRows == 0 ? -1 : gridaKoka.TopRows),//nese ka vleren 0 ath ska konfig te zgjedhur
                    MenyreFiltrimi = gridaKoka.MenyreFiltrimi,
                    MePeriudhe = true
                };
            }
            else
            {
                Periudhat = CurrentPage.MerrPeriudhe(hfState);
              
            }
            Periudhat.LlogaritKufijtEDates();
            hfState.Set(TitlePeriudha.KeyFieldPeriudha, JsonConvert.SerializeObject(Periudhat));
        }

        #region EVENTET CLIENTSIDE

        private string ValueChangedClientSide()
        {
            return $@"function(s,e)
          {{
             var periudhat =JSON.parse({hfState.ClientInstanceName}.Get('{TitlePeriudha.KeyFieldPeriudha}'));
             periudhat.PeriudhaDok = {_radDtDok.ClientInstanceName}.GetValue();
             {hfState.ClientInstanceName}.Set('{TitlePeriudha.KeyFieldPeriudha}',JSON.stringify(periudhat));
             {Grid.ClientInstanceName}.PerformCallback('{TitlePeriudha.KeyParamNdryshimPeriudhe}');
          }}";
        }

        #endregion
        #region Eventet SERVER SIDE
        private void radDtDok_PreRender(object sender, EventArgs e)
        {
            var radDtDok = sender as ASPxRadioButtonList;
            if (radDtDok == null)
                throw new MyException("radDtDok eshte null!");
            radDtDok.SelectedItem = radDtDok.Items.FindByValue(Periudhat.PeriudhaDok);
        }

        private void btnfilterDefault_Click(object sender, EventArgs e)
        {
            GridUtil.AplikoFilterDefault(Grid, idKonfig);
        }

        #endregion 

    }
}