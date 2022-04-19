using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Web;
using System.Data;
using System.Globalization;
using System.Resources;
using AlphaWebCommon.WebUtils.ASPxControlExtensions;
using DbCore.DbRegjistrim;
using DbCore;
using DbCore.DbShare;
using DbCore.DbShare;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Types;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Filters;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class ListPagesa : MyPageBase
    {
        private int idndermarje, idperdoruesi, idnderviti, idviti, idGjuha;
        private ResourceManager _rm;
        private CultureInfo _ci;
        private TitlePeriudha _periudha;
        private ResourceManager Rm => _rm ?? (_rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources")));
        private CultureInfo Ci => _ci ?? (_ci = mySessionObjects.ktheCultureInfo(Session));
        public TitlePeriudha Periudha => _periudha ?? (_periudha = this.MerrPeriudhe(hfState));
        private string komponente = "ListPagesa.aspx";
        private string guidString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                DbCore.clsFunksione.logout(Session, true, "FaqePaautorizuar");
            }
            idperdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idperdoruesi);
            }
            idndermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            idviti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            idnderviti = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
            idGjuha = mySessionObjects.ktheGjuhe(Session);
            int shifraPasPresjesSasia = 0;
            int idKonfig;
            if (!IsPostBack)
            {
                guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idperdoruesi, idndermarje, cmbKonfigurimi, 59, Rm, Ci, idGjuha);
                cmbKonfigurimi.SelectedIndex = 0;
                idKonfig = Converter.MerrVlereOseDefault<int>(cmbKonfigurimi.SelectedItem.Value);
                var konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                konf.mbushKonfigAmbjSipasId(idKonfig, idGjuha);
                hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
                mbushGridNgaDB();
                gvListPagesa.FilterExpression = "[IdStatusDok]=1";
                var formatMonedhe = clsFormatKonfigTrup.MerrFormatMonedheSipasNdermarrjesDheKonfig(idndermarje, idKonfig, 0);
                shifraPasPresjesSasia = formatMonedhe.ShifraPasPresjesSasia;
                konfiguroGride(Rm, Ci, idGjuha, shifraPasPresjesSasia);
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idndermarje, "gvListPagesa", gvListPagesa, cmbKonfigurimi.Text.Split(';')[0], "709", idGjuha);
                if (Request.QueryString["ruaj"] == "po")
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, Rm.GetString("regjMagModifikimiPerfundoiMeSukses", Ci), pnlMesazhi);
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idperdoruesi, idndermarje, idviti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                gvListPagesa.Columns["#"].VisibleIndex = 0;
                hfState.Set("guidString", guidString);
                hfState.Set("shifraPasPresjesSasia", shifraPasPresjesSasia);
                hfState.Set("regjMagMesazhZgjidhniNje", rm.GetString("regjMagMesazhZgjidhniNje", Ci));
                hfState.Set("msgJuKeniZgjedhur", rm.GetString("msgJuKeniZgjedhur", Ci));
                hfState.Set("msgRreshta", rm.GetString("msgRreshta", Ci));
                hfState.Set("labelAdministrimiMsgJeniSigurt", rm.GetString("labelAdministrimiMsgJeniSigurt", Ci));
                hfState.Set("regjisDokZgjidhDokPerTeBashkengjitur", rm.GetString("regjisDokZgjidhDokPerTeBashkengjitur", Ci));
            }
            else
            {
                guidString = hfState.Get<string>("guidString");
                shifraPasPresjesSasia = hfState.Get<int>("shifraPasPresjesSasia");

                mbushGridNgaSession();
                konfiguroGride(Rm, Ci, idGjuha, shifraPasPresjesSasia);
            }
            clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idndermarje, "gvListPagesa", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
            gvListPagesa.PercaktoTitlePanel(this, MenuInfo, pnlMesazhi, hfState, idperdoruesi, idndermarje, idviti, idGjuha, int.Parse(cmbKonfigurimi.Value.ToString()), "ListPagesa.aspx", Rm, Ci);
            if (Request.QueryString["indexrow"] != null)
                gvListPagesa.FocusedRowIndex = int.Parse(Request.QueryString["indexrow"]);
            perktheLabel();
            percaktoTemplateMenu(idGjuha, ASPxMenu1, idviti, idperdoruesi, idndermarje);
        }

        public void perktheLabel()
        {
            konfigurimi_Label.Text = Rm.GetString("lblModeli", Ci);
            hfState.Set("regjisDokNukKeniAsnjeDokTeZgjedhur", Rm.GetString("regjisDokNukKeniAsnjeDokTeZgjedhur", Ci));

        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        /// <param name="idViti"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNermarrje"></param>
        private void percaktoTemplateMenu(int idGjuha, ASPxMenu aSPxMenu1, int idViti, int idPerdorues, int idNdermarrje)
        {
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, komponente, this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));

        }

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(idGjuha, ASPxMenu1, idviti, idperdoruesi, idndermarje);
        }
        private void mbushGridNgaSession()
        {
            DataTable tmpObject = DbCore.mySessionObjects.MerrNgaSession<DataTable>(Session, "GrideListepagese", guidString);
            if (tmpObject != null)
                mbushGridNgaDB();
            else
            {
                gvListPagesa.DataSource = tmpObject;
                gvListPagesa.DataBind();
                tmpObject.Dispose();
            }
        }
        private void mbushGridNgaDB()
        {//mbush griden e popupit me te dhena            
            DataTable dt = DbCore.DbListPagesat.colKokaListPagese.merrKokaListPagesaDT(idnderviti, DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            DbCore.mySessionObjects.RuajNeSession<DataTable>(Session, dt,"GrideListepagese", guidString);
            gvListPagesa.DataSource = dt;
            gvListPagesa.DataBind();
            dt.Dispose();
        }

        private void konfiguroGride(ResourceManager Rm, CultureInfo Ci, int idGjuha, int shifraPasPresjes)
        {
            KonfigurimComboGride.ShtoStatusAprovimi(gvListPagesa, Rm, Ci);
            KonfigurimComboGride.ShtoStatus(gvListPagesa, Rm, Ci);
            KonfigurimComboGride.ShtoModel(gvListPagesa, 38, idndermarje, idperdoruesi, idGjuha, Session, komponente, guidString);
            KonfigurimComboGride.ShtoMonedhe(gvListPagesa, idndermarje, idperdoruesi, Session, komponente, guidString);
            gvListPagesa.ShtoDepartament(idndermarje, Session, komponente, guidString);
            gvListPagesa.ShtoNenDepartament(idndermarje, Session, komponente, guidString);
            KonfigurimComboGride.ShtoMuaj(gvListPagesa, Rm, Ci);
            GridUtil.konfigGrideListeEMadhePaTheme(gvListPagesa, "IdKoka");

            GridUtil.VendosFormatNumriPerFushatNumerike(gvListPagesa, shifraPasPresjes, "Totali", "TotaliNdermarje");
            GridUtil.VendosFormatNumri(gvListPagesa.Columns["Kursi"] as GridViewDataColumn, 2);
            GridViewDataDateColumn col6 = gvListPagesa.Columns["DtRegjistrimi"] as GridViewDataDateColumn;
            GridViewDataDateColumn col7 = gvListPagesa.Columns["DtDok"] as GridViewDataDateColumn;
            AspxWebControlUtils.vendosDateEditMask(col6.PropertiesDateEdit, col7.PropertiesDateEdit);

            gvListPagesa.Columns["#"].VisibleIndex = 0;
        }
        protected void gvListPagesa_DataBound(object sender, EventArgs e)
        {
            if (gvListPagesa.Columns["#"] == null)
            {
                GridViewCommandColumn check = new GridViewCommandColumn("#") { ShowSelectCheckbox = true, Width = System.Web.UI.WebControls.Unit.Percentage(2) };
                gvListPagesa.Settings.ShowFilterRow = true;
                gvListPagesa.Settings.ShowHeaderFilterButton = true;
                gvListPagesa.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                gvListPagesa.Settings.ShowFilterRowMenu = true;
                gvListPagesa.Columns.Add(check);
                gvListPagesa.Settings.ShowGroupPanel = true;
                gvListPagesa.KeyFieldName = "IdKoka";
                gvListPagesa.SettingsBehavior.AllowSelectByRowClick = true;
                gvListPagesa.SettingsBehavior.AllowFocusedRow = true;
            }
        }
        /// <summary>
        /// fshin filtrin e zgjedhur dhe aplikuar mbi gride, fshirje nga DB ku ai eshte ruajtur, dhe jo pastrimi i grides nga aplikimi i filtrit
        /// per kete do sherbeje Clear Filter ne fund te grides
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            //filtra.mbushFiltraGridaSipasFiltraEmri(cmbFiltra.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuha, "gvListPagesa", komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                filtra.IdPerdoruesi = idperdoruesi;
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra();
                clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, "gvListPagesa", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
                percaktoTemplateMenu(idGjuha, ASPxMenu1, idviti, idperdoruesi, idndermarje);
                if (mesazh)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, Rm.GetString("msgFshirjaPerfundoiMeSukses", Ci), pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, Rm.GetString("msgGagimIPanohur", Ci), pnlMesazhi);

                cmbFiltra.Text = "";
                gvListPagesa.FilterExpression = " [IdStatusDok]=1 ";
            }
        }
        /// <summary>
        /// sherben per te ruajtur filtrin e zgjedhur
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuha, "gvListPagesa", komponente, idndermarje, int.Parse(cmbKonfigurimi.Value.ToString()));
            DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida() { FiltraKodi = cmbFiltra.Text, FiltraShenime = cmbFiltra.Text, FiltraUniversal = false, GridaKokaId = koka.IdGridaKoka, FiltraVlera = gvListPagesa.FilterExpression, IdPerdoruesi = idperdoruesi, IdNdermarje = idndermarje, IdStatusDok = 1 };
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("NrDok", gvListPagesa);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvListPagesa.GetSortedColumns();
            //if (kolona.Count > 0)
            //{
            //    filtri.KoloneRenditje = kolona[0].FieldName;
            //    if (kolona[0].SortOrder == DevExpress.Data.ColumnSortOrder.Ascending)
            //        filtri.DrejtimRenditje = true;
            //    else
            //        filtri.DrejtimRenditje = false;
            //}
            //else
            //{
            //    filtri.KoloneRenditje = "NrDok";
            //    filtri.DrejtimRenditje = true;
            //}

            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            mesazh = filtri.ruaj();
            //mbushComboBoxFiltra();
            clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idndermarje, "gvListPagesa", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
            percaktoTemplateMenu(idGjuha, ASPxMenu1, idviti, idperdoruesi, idndermarje);
            if (mesazh.Status == true)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, Rm.GetString("msgFiltriURuajtMeSukses", Ci), pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, Rm.GetString("msgEkzistonFilterMeKeteKod", Ci), pnlMesazhi);

            cmbFiltra.Text = "";
        }

        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {

        }

        protected void gvListPagesa_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            if (e.CallbackName == "COLUMNMOVE" && gvListPagesa.AllColumns[int.Parse(e.Args[0])].Width.Value == 0)
                gvListPagesa.AllColumns[int.Parse(e.Args[0])].Width = System.Web.UI.WebControls.Unit.Percentage(3);
            if (e.CallbackName == "APPLYFILTER" && e.Args[0] == "")
            {
                MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
                ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
                cmbFiltra.Text = "";
            }
            //  konfiguroGride();
        }

        protected void gvListPagesa_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string[] arr = e.Parameters.Split(';');
            GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), "gvListPagesa", gvListPagesa, cmbKonfigurimi.Text.Split(';')[0], "709", idGjuha);
            if (arr.Length == 3)
            {
                if (arr[2] == "")
                    gvListPagesa.FilterExpression = " [IdStatusDok]=1 ";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    //filtra.mbushFiltraGridaSipasFiltraKodi(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuha, "gvListPagesa", komponente, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), int.Parse(cmbKonfigurimi.Value.ToString()));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvListPagesa.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvListPagesa);
                    }
                }
            }

            gvListPagesa.Selection.UnselectAll();
        }

        protected void gvListPagesa_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvListPagesa.PageIndex;
            e.Properties["cpPageRow"] = gvListPagesa.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvListPagesa.VisibleRowCount;
        }

        protected void gvListPagesa_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if (e.Column.FieldName.ContainsAnyIgnoreCase("IdDepartamenti", "IdMonedha", "IdKonfigAmbjente", "IdNenDepartamenti", "Muaji", "StatusAprovimi"))
            {
                if (Converter.ConvertToInt(e.Value) == 0)
                {
                    e.Criteria = null;
                }
            }
        }

        protected void gvListPagesa_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            CultureInfo Ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager Rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            string TeGjithe = Rm.GetString("GridHeaderFilterFillItemTeGjithe", Ci);
            string nga = Rm.GetString("GridHeaderFilterFillItemNga", Ci);
            if (e.Column.FieldName == "NrDok" || e.Column.FieldName == "Shenime")
            {
                e.Values.Clear();
                e.AddValue(TeGjithe, string.Empty, "true");
                e.AddValue(nga + " A-D ", string.Empty, String.Format("{0}>'A     ' and {0} <'DDDDDDD'", e.Column.FieldName));
                e.AddValue(nga + " D-G ", string.Empty, String.Format("{0}>'D     ' and {0}<'GGGGGGG'", e.Column.FieldName));
                e.AddValue(nga + " H-K ", string.Empty, String.Format("{0}>'H     ' and {0}<'KKKKKKK'", e.Column.FieldName));
                e.AddValue(nga + " L-O ", string.Empty, String.Format("{0}>'L     ' and {0}  <'OOOOOOO'", e.Column.FieldName));
                e.AddValue(nga + " P-S ", string.Empty, String.Format("{0}>'P     ' and {0}<'SSSSSSS'", e.Column.FieldName));
                e.AddValue(nga + " T-W ", string.Empty, String.Format("{0}>'T     ' and {0}<'WWWWWWW'", e.Column.FieldName));
                e.AddValue(nga + " X-Z ", string.Empty, String.Format("{0}>'X     ' and {0}<'ZZZZZZZ'", e.Column.FieldName));
            }
            else
            {
                e.Values.Clear();
                e.AddValue(TeGjithe, string.Empty, "true");
            }
        }
    }
}

