using DbCore;
using DevExpress.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PlatinumWeb.Templates;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using System.Web.Script.Serialization;
using System.Web.UI;
using DbCore.DbListPagesat;
using DbCore.DbAdmin;
using DbCore.DbShare;
using System.Reflection;
using DbCore.IMBUtils.Types;
using DevExpress.Web.Data;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class LupaKomponente : MyPageBase
    {
        private decimal totalPerGrup = 0;//perdoret tek llogaritja me kusht e totalit te rreshtave te grides
        private bool? eshteGrupuarSipasTipit = null;

        protected void Page_PreInit(object sender, EventArgs e)
        {
           base. Page_PreInit(sender, e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            int idPerdoruesi;
            int idGjuha;
            int idNdermarrje;
            int idViti;
            int idNdermarrjeVit;
            int shifraPasPresjesSasia;
            int idKonfig;
            if (!IsPostBack)
            {
                if (!mySessionObjects.isLogedIn(Session))
                {
                    clsFunksione.logout(Session, true, "FaqePaautorizuar");
                }

                idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);
                idGjuha = mySessionObjects.ktheGjuhe(Session);
                idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
                idViti = mySessionObjects.ktheIdVitNdermarrje(Session);
                idNdermarrjeVit = mySessionObjects.ktheNdermarrjeVit(Session);
                idKonfig = Converter.MerrVlereOseDefault<int>(Request.QueryString["konfig"]);
                var idMonedha = Converter.MerrVlereOseDefault<int>(Request.QueryString["monedha"]);
                var formatMonedhe = clsFormatKonfigTrup.MerrFormatMonedheSipasNdermarrjesDheKonfig(idNdermarrje, idKonfig, idMonedha);
                shifraPasPresjesSasia = formatMonedhe.ShifraPasPresjesSasia;
                var idpunonjesi = int.Parse(Request.QueryString["idpunonjesi"].Split(',')[0]);
                var pun = new clsPunonjes(idpunonjesi);

                hfState.Set("idPerdoruesi", idPerdoruesi);
                hfState.Set("idGjuha", idGjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idViti", idViti);
                hfState.Set("idNdermarrjeVit", idNdermarrjeVit);
                hfState.Set("IsFirstCallback", true);
                hfState.Set("formatMonedhe", JsonConvert.SerializeObject(formatMonedhe));
                hfState.Set("shifraPasPresjeSasia", shifraPasPresjesSasia);
                hfState.Set("punonjesi", JsonConvert.SerializeObject(pun));
                hfState.Set("idKonfig", idKonfig);
                txtPunonjesi.Text = String.Format("{0}-{1} {2}", pun.NrPersonal, pun.Emer, pun.Mbiemer);
                if (!pun.LlogaritNgaListorare)
                    btnImporti.ClientVisible = false;
                mbushPopUpListeNgaDB();
          
                gvLupaK.ExpandAll();

            }
            else
            {
                idPerdoruesi = (int)hfState["idPerdoruesi"];
                idGjuha = (int)hfState["idGjuha"];
                idNdermarrje = (int)hfState["idNdermarrje"];
                idViti = (int)hfState["idViti"];
                idNdermarrjeVit = (int)hfState["idNdermarrjeVit"];
                shifraPasPresjesSasia = (int)hfState.Get("shifraPasPresjeSasia");
                idKonfig = (int)hfState.Get("idKonfig");
                mbushPopUpListeNgaSession();
            }
            konfiguroGride(shifraPasPresjesSasia, idKonfig);
            if (!IsPostBack)
            {
                gvLupaK.ExpandAll();
            }
            perktheLabel();
            mbushHiddenFieldMePerkthime();
        }



        public void perktheLabel()
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            ASPxRoundPanel1.HeaderText = rm.GetString("headerPopUpText", cultinf);
            lblPunonjesi.Text = rm.GetString("msgLupaPunonjesPunonjesi", cultinf);
            btnImporti.Text = rm.GetString("btnRimerrVlerat", cultinf);
            lblPaguar.Text = rm.GetString("filterPaguar", cultinf);
            btnKtheu.Text = rm.GetString("btnListaEPunonjesve", cultinf);
            btnOk.Text = rm.GetString("labelOk", cultinf);
            popupUniversal.HeaderText = rm.GetString("headerPopUpText", cultinf);
        }

        public void mbushHiddenFieldMePerkthime()
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            hfState.Set("msgTeDhenaPerMuajin", rm.GetString("msgTeDhenaPerMuajin", cultinf));
            hfState.Set("msgLupaPunonjesPunonjesi", rm.GetString("msgLupaPunonjesPunonjesi", cultinf));
            hfState.Set("msgLupaPunonjesNukKaKomponentePerKeteDate", rm.GetString("msgLupaPunonjesNukKaKomponentePerKeteDate", cultinf));
            hfState.Set("msgPerDitetEPunes", rm.GetString("msgPerDitetEPunes", cultinf));
            hfState.Set("msgPerDitetEPunesJoNegative", rm.GetString("msgPerDitetEPunesJoNegative", cultinf));
            hfState.Set("msgPlotesoVlerenEKomponentes", rm.GetString("msgPlotesoVlerenEKomponentes", cultinf));
            hfState.Set("msgVleraDuhetJeteNumer", rm.GetString("msgVleraDuhetJeteNumer", cultinf));
            hfState.Set("msgVleraPozitive", rm.GetString("msgVleraPozitive", cultinf));
            hfState.Set("msgParametriNumer", rm.GetString("msgParametriNumer", cultinf));
            hfState.Set("msgParametriPozitive", rm.GetString("msgParametriPozitive", cultinf));
        }

        protected void gvLupaK_BatchUpdate(object sender, DevExpress.Web.Data.ASPxDataBatchUpdateEventArgs e)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));


            try
            {
                Object tmpObject;
                mySessionObjects.merrGrideNgaSessioniLupa(Session, out tmpObject);
                var col = (colKompListPagese)tmpObject;

                foreach (ASPxDataUpdateValues updated in e.UpdateValues)
                {
                    var komp = col.Find(x => x.IdKompListPagese == updated.MerrKeyValue<int>());
                    komp = updated.MerrCustomUpdatedObject(komp);
                }

                var rreshti = gvLupaK.GetRowValues(gvLupaK.FocusedRowIndex, "IdKompListPagese", "IdKomponentePage", "KodKomponente", "Njesia", "VleraParam", "Vlera");

                gvLupaK.JSProperties["cpRowValues"] = JsonConvert.SerializeObject(rreshti);
                gvLupaK.DataSource = col;
                gvLupaK.DataBind();

                e.Handled = true;
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex.Message);
                throw new Exception(rm.GetString("msgGabimGjateUpdatimit", cultinf) + ex.Message);
            }
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {


        }

        /// <summary>
        /// mbush lupen nga sessioni
        /// </summary>
        private void mbushPopUpListeNgaSession()
        {
            Object tmpObject;
            mySessionObjects.merrGrideNgaSessioniLupa(Session, out tmpObject);
            if (tmpObject == null)
            {
                mbushPopUpListeNgaDB();
                return;
            }

            gvLupaK.DataSource = tmpObject;
            gvLupaK.DataBind();
        }

        /// <summary>
        /// mbush lupen nga db
        /// </summary>
        private void mbushPopUpListeNgaDB()
        {//mbush griden e popupit me te dhena
            var idpunonjesi = int.Parse(Request.QueryString["idpunonjesi"].Split(',')[0]);
            var data = DateTime.Parse(Request.QueryString["data"]);
            int idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
            var col = new colKompListPagese(idpunonjesi, data, idNdermarrje);
            mySessionObjects.ruajGrideNeSessionLupa(Session, col);

            gvLupaK.DataSource = col;
            gvLupaK.DataBind();
            if (Request.QueryString["newrecord"] == "true")
                Utils.HiqKomponenteMuajiNgaSessioniPerPunonjes(idpunonjesi, Session);

            foreach (var d in col)
            {
                hflejomod.Add(d.KodKomponente, d.LejoModVlere);
                hfdetyrueshme.Add(d.KodKomponente, d.EDetyrueshme);
            }

            var colTegjitha = new colKompListPagese(true, data, idNdermarrje, idpunonjesi);
            hfKompFill.Value = JsonConvert.SerializeObject(colTegjitha);
        }

        /// <summary>
        /// perdoret per te konfiguruar griden. Per te dhenat mbi kolonat qe do te shfaqen nga
        /// databaza si dhe percakton disa karakteristika te grides
        /// </summary>
        /// :
        /// <see cref="GridUtil.percaktoVisibleColumns" />
        /// :
        /// <see cref="DbCore.clsFunksione.konfiguroGrideListeMadhe(grida,celesigrides)" />
        private void konfiguroGride(int shifraPasPresjes, int idKonfig)
        {
            var cultinf = mySessionObjects.ktheCultureInfo(Session);
            var rm = new ResourceManager("Resources.Strings", Assembly.Load("App_GlobalResources"));
            shtoNjesi(gvLupaK);
            if (!IsPostBack)
            {
                shtoTip();
            }
            shtoImport();
            GridUtil.percaktoVisibleColumnsMeWidth(mySessionObjects.ktheGjuhe(Session), mySessionObjects.merrIdNdermarrjeSesioni(Session), gvLupaK, "gvLupaKomponente", "LupaKomponente.aspx");
            GridUtil.konfiguroGrideRegjistrimEvogelPaTheme(gvLupaK, "IdKompListPagese", false);

            GridUtil.konfiguroGridaPerBatchEditing(gvLupaK, false, false, false, false, 1000, true, true);
            gvLupaK.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
            if (clsAlternativaKushti.getAlternativa(idKonfig, "ES") == "Po")
                gvLupaK.SettingsPager.Mode = GridViewPagerMode.EndlessPaging;
            GridUtil.VendosFormatNumriPerFushatNumerike(gvLupaK, shifraPasPresjes, "Vlera", "VleraParam");
            gvLupaK.SettingsBehavior.AllowFocusedRow = true;
            gvLupaK.SettingsBehavior.AllowSort = false;
            shtoGroupFooter();
            if (!IsPostBack)
            {
                GrupoKolonatSipasKonfigurimit(idKonfig);
                gvLupaK.Attributes.Add("onkeydown", String.Format("lupaKomponente.handlers.onKeyPressed(event);"));
            }
        }

        private void GrupoKolonatSipasKonfigurimit(int idKonfig)
        {
            if (clsAlternativaKushti.getAlternativa(idKonfig, "GNV") == "Po")
            {
                hfState["eshteGrupuarSipasTipit"] = false;
                gvLupaK.GroupBy(gvLupaK.Columns["Niveli1"], 1);
                gvLupaK.GroupBy(gvLupaK.Columns["Niveli2"], 2);
                gvLupaK.GroupBy(gvLupaK.Columns["Grupi"], 3);
                gvLupaK.SortBy(gvLupaK.Columns["IdRenditje2"], DevExpress.Data.ColumnSortOrder.Ascending);
            }
            else
            {
                hfState["eshteGrupuarSipasTipit"] = true;
                gvLupaK.GroupBy(gvLupaK.Columns["Tipi"], 1);
                gvLupaK.GroupBy(gvLupaK.Columns["Grupi"], 2);
            }
        }

        /// <summary>
        /// perdoret per te shfaqur llojin ne vend te true/false si dhe filtri i llojit te shfaqet
        /// ne forme komboje
        /// </summary>
        private void shtoNjesi(ASPxGridView grida)
        {
            var colnew = new GridViewDataComboBoxColumn();
            if (typeof(GridViewDataComboBoxColumn) != grida.Columns["Njesia"].GetType())
            {
                grida.Columns.Remove(grida.Columns["Njesia"]);
                grida.Columns.Add(colnew);
                colnew.PropertiesComboBox.Items.Add(" ", Convert.ToInt32(NjesiPagese.Undefined));
                colnew.PropertiesComboBox.Items.Add(NjesiPagese.Nr.ToString(), Convert.ToInt32(NjesiPagese.Nr));
                colnew.PropertiesComboBox.Items.Add(NjesiPagese.Tab.ToString(), Convert.ToInt32(NjesiPagese.Tab));
                colnew.PropertiesComboBox.Items.Add(NjesiPagese.For.ToString(), Convert.ToInt32(NjesiPagese.For));
                colnew.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                colnew.FieldName = "Njesia";
                colnew.VisibleIndex = 5;
                colnew.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                mySessionObjects.ruajDsComboGrideNeSession(Session, colnew.PropertiesComboBox.Items, "lupakomp" + "colLlojiKf");
            }
            else
            {
                colnew = (GridViewDataComboBoxColumn)grida.Columns["Njesia"];
                colnew.VisibleIndex = 5;
                if (colnew.PropertiesComboBox.Items.Count == 0)
                {
                    //grid_RegDok.Columns.Remove(grid_RegDok.Columns["LlojiKf"]);
                    colnew.PropertiesComboBox.Items.AddRange((ListEditItemCollection)mySessionObjects.merrDsComboGrideNeSession(Session, "lupakomp" + "colLlojiKf"));
                    //grid_RegDok.Columns.Add(colnew);
                }
            }
        }

        /// <summary>
        /// perdoret per te shfaqur llojin ne vend te true/false si dhe filtri i llojit te shfaqet
        /// ne forme komboje
        /// </summary>
        private void shtoTip()
        {
            gvLupaK.Columns.Remove(gvLupaK.Columns["Tipi"]);
            var colnew = new GridViewDataComboBoxColumn();
            colnew.PropertiesComboBox.Items.Add(" ", Convert.ToInt32(TipPagese.Undefined));
            colnew.PropertiesComboBox.Items.Add(TipPagese.Pagese.ToString(), Convert.ToInt32(TipPagese.Pagese));
            colnew.PropertiesComboBox.Items.Add(TipPagese.Ndalese.ToString(), Convert.ToInt32(TipPagese.Ndalese));
            colnew.PropertiesComboBox.Items.Add(TipPagese.Llogaritese.ToString(), Convert.ToInt32(TipPagese.Llogaritese));
            colnew.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            colnew.FieldName = "Tipi";
            colnew.PropertiesComboBox.ValueType = typeof(int);
            gvLupaK.Columns.Add(colnew);
        }

        private void shtoImport()
        {
            gvLupaK.Columns.Remove(gvLupaK.Columns["Import"]);
            var colnew = new GridViewDataButtonEditColumn();

            colnew.DataItemTemplate = new MyButtonTemplate("Importo");
            colnew.PropertiesEdit.EnableClientSideAPI = true;

            colnew.FieldName = "Import";

            gvLupaK.Columns.Add(colnew);
        }

        private void shtoGroupFooter()
        {
            gvLupaK.PercaktoTemplateGroupSummaryFooter(clsFunksione.krijoNumer(int.Parse(Request.QueryString["formatnr"]), "0"), "Vlera");
            gvLupaK.ShtoGroupSummary(clsFunksione.krijoNumer(int.Parse(Request.QueryString["formatnr"]), "0"), DevExpress.Data.SummaryItemType.Custom, "Vlera");
            //GridUtil.PercaktoTemplateGroupRowContent(gvLupaK, clsFunksione.krijoNumer(int.Parse(Request.QueryString["formatnr"]), "0"));
        }

        protected void gvLupaK_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType == GridViewRowType.Data)
            {
                var col = gvLupaK.Columns["Import"] as GridViewDataColumn;

                var btn = gvLupaK.FindRowCellTemplateControl(e.VisibleIndex, col, "btn") as ASPxButton;
                if (btn != null)
                {
                    btn.ClientSideEvents.Click = "function(s,e){buttonclickImport(" + e.VisibleIndex + ")}";
                    if (Request.QueryString["lidhur"] == "true")
                        btn.ClientEnabled = false;
                }
            }
        }

        protected void gvLupaK_CustomGroupDisplayText(object sender, ASPxGridViewColumnDisplayTextEventArgs e)
        {
            //if(e.Column.Name==gvLupaK.GetGroupedColumns()[0].Name)
        }

        protected void gvLupaK_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            //inicializimi
            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                totalPerGrup = 0;
            // llogaritja
            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
            {
                int tipi = Convert.ToInt16(e.GetValue("Tipi"));
                if (!eshteGrupuarSipasTipit.HasValue)
                    eshteGrupuarSipasTipit = Convert.ToBoolean(hfState["eshteGrupuarSipasTipit"]);

                if (eshteGrupuarSipasTipit.Value)
                {
                    var kodKomponente = ((clsKompListPagese)e.Row).KodKomponente;
                    gvLupaK.JSProperties["cpTipi_" + kodKomponente] = tipi;
                }

                if (tipi == 1)
                {
                    totalPerGrup += Decimal.Parse(e.FieldValue.ToString());
                }
                if (tipi == 2)
                {
                    totalPerGrup -= Decimal.Parse(e.FieldValue.ToString());
                }
            }
            // mbyllja
            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                e.TotalValue = totalPerGrup;
        }

        protected void gvLupaK_CustomColumnSort(object sender, CustomColumnSortEventArgs e)
        {
            if (e.Column != null & e.Column.FieldName == "Grupi")
            {
                var country1 = e.GetRow1Value("IdRenditje2");

                var country2 = e.GetRow2Value("IdRenditje2");

                var res = Comparer.Default.Compare(country1, country2);

                if (res == 0)
                {
                    var city1 = e.Value1;

                    var city2 = e.Value2;

                    res = Comparer.Default.Compare(city1, city2);
                }

                e.Result = res;

                e.Handled = true;
            }
        }
    }
}