using System;
using DevExpress.Web;
using System.Data;
using DbCore;
using PlatinumWeb.ApplicationUtils.Pages;
using System.Web.UI.WebControls;
using DevExpress.Data.Filtering;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using DbCore.DbInventari;
using DbCore.IMBUtils.Extensions;

namespace PlatinumWeb
{

    /// <summary>
    /// nderfaqja e planifikimit te prodhimit
    /// </summary>
    public partial class RaporteGrida : MyPageBase
    {
        /// <summary>
        /// kur lodohet faqja  mbushet me te dhena dhe kontrollohen ne eshte i autorizuar perdoruesi
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!mySessionObjects.isLogedIn(Session))
            {
                clsFunksione.logout(Session, true, "FaqePaautorizuar");
            }

            if (mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + IdPerdoruesi);
            }

            PercaktoTemplateMenu();

            var oPeriudha = mySessionObjects.merrPeriudheKontabel(Session);
            if (!IsPostBack)
            {
                MbushGridNgaDb(oPeriudha.FillimiPeriudha, oPeriudha.MbarimiPeriudha, oPeriudha.FillimiPeriudha, oPeriudha.MbarimiPeriudha);
                gvRaporti.FilterExpression = "";

                var tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();

                KonfiguroGride();

                GridUtil.percaktoVisibleColumnsMeWidth(IdGjuha, IdNdermarrja, gvRaporti, "gvRaporti", "RaporteGrida.aspx?lloji=" + Request.QueryString["lloji"]);
                if (gvRaporti.Columns["#"] != null)
                    gvRaporti.Columns["#"].VisibleIndex = 0;
            }
            else
            {
                var dtfillimi = (DateTime)mySessionObjects.merrdtfillNgaSesioni(Session);
                var dtmbarimi = (DateTime)mySessionObjects.merrdtmbarNgaSesioni(Session);
                var dtfillimiexe = (DateTime)mySessionObjects.merrdtfillexeNgaSesioni(Session);
                var dtmbarimiexe = (DateTime)mySessionObjects.merrdtmbarexeNgaSesioni(Session);

                MbushGridNgaSession(dtfillimi, dtmbarimi, dtfillimiexe, dtmbarimiexe);

                var txtNgaDok = gvRaporti.FindTitleTemplateControl("txtNgaDok") as ASPxDateEdit;
                txtNgaDok.Date = dtfillimi;

                var txtNgaDok1 = gvRaporti.FindTitleTemplateControl("txtNgaDok1") as ASPxDateEdit;
                txtNgaDok1.Date = dtfillimiexe;

                var txtDeriDok1 = gvRaporti.FindTitleTemplateControl("txtDeriDok1") as ASPxDateEdit;
                txtDeriDok1.Date = dtmbarimiexe;

                var txtDeriDok = gvRaporti.FindTitleTemplateControl("txtDeriDok") as ASPxDateEdit;
                txtDeriDok.Date = dtmbarimi;

                var radDtDok = gvRaporti.FindTitleTemplateControl("radDtDok5") as ASPxRadioButtonList;
                var radDtDok1 = gvRaporti.FindTitleTemplateControl("radDtDok11") as ASPxRadioButtonList;
                radDtDok.SelectedIndex = (int)mySessionObjects.merrrad2NgaSesioni(Session);
                radDtDok1.SelectedIndex = (int)mySessionObjects.merrradNgaSesioni(Session);

                KonfiguroGride();

            }
            GridUtil.konfigGrideListeEMadhePaTheme(gvRaporti, Request.QueryString["lloji"] == "GjendjaEArtikujveMeSeriale" ? "SerialiKryesor" : Request.QueryString["lloji"] == "GjendjaEMagazines" ? "IdArtikulli" : "Id");
            clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, IdNdermarrja, "gvRaporti", 1, "RaporteGrida.aspx?lloji=" + Request.QueryString["lloji"]);
            if (IsCallback) gvRaporti.Columns["#"].VisibleIndex = 0;
            GridUtil.ToolTipButonaveMbiGride(gvRaporti, ci, rm);


            if (gvRaporti.FindTitleTemplateControl("txtNgaDok1") is ASPxDateEdit)
            {
                if (!IsPostBack)
                {
                    var txtNgaDok1 = gvRaporti.FindTitleTemplateControl("txtNgaDok1") as ASPxDateEdit;
                    txtNgaDok1.Date = oPeriudha.FillimiPeriudha;

                    var txtNgaDok = gvRaporti.FindTitleTemplateControl("txtNgaDok") as ASPxDateEdit;
                    txtNgaDok.Date = oPeriudha.FillimiPeriudha;

                    var txtDeriDok1 = gvRaporti.FindTitleTemplateControl("txtDeriDok1") as ASPxDateEdit;
                    txtDeriDok1.Date = oPeriudha.MbarimiPeriudha;

                    var txtDeriDok = gvRaporti.FindTitleTemplateControl("txtDeriDok") as ASPxDateEdit;
                    txtDeriDok.Date = oPeriudha.MbarimiPeriudha;

                    var radDtDok = gvRaporti.FindTitleTemplateControl("radDtDok5") as ASPxRadioButtonList;
                    var radDtDok1 = gvRaporti.FindTitleTemplateControl("radDtDok11") as ASPxRadioButtonList;

                    mySessionObjects.ruajradNeSesion(Session, radDtDok1.SelectedIndex);
                    mySessionObjects.ruajrad2NeSesion(Session, radDtDok.SelectedIndex);

                }
            }
            if (Request.QueryString["indexrow"] != null)
            {
                gvRaporti.FocusedRowIndex = int.Parse(Request.QueryString["indexrow"]);
            }
        }

        #region Menu

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        private void PercaktoTemplateMenu()
        {
            clsToolbarConfig.percaktoTemplateMenu(IdGjuha, IdViti, IdPerdoruesi, IdNdermarrja, ASPxMenu1,
                "RaporteGrida.aspx?lloji=" + Request.QueryString["lloji"], this, MenuInfo, Ruaj_ASPxButton_Click,
                FshiFilter_ASPxButton_Click, true, false, false, Meme);
        }

        /// <summary>
        /// eventet e menuse
        /// </summary>
        /// <param name="source">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {
            if (e.Item.Name == "Shiko")
            {
                DateTime dtfillimi, dtmbarimi, dtfillimiexe, dtmbarimiexe;
                dtfillimi = (DateTime)mySessionObjects.merrdtfillNgaSesioni(Session);
                dtmbarimi = (DateTime)mySessionObjects.merrdtmbarNgaSesioni(Session);
                dtfillimiexe = (DateTime)mySessionObjects.merrdtfillexeNgaSesioni(Session);
                dtmbarimiexe = (DateTime)mySessionObjects.merrdtmbarexeNgaSesioni(Session);
                MbushGridNgaDb(dtfillimi, dtmbarimi, dtfillimiexe, dtmbarimiexe);
                gvRaporti.FilterExpression = "";
                gvRaporti.Selection.UnselectAll();
            }
        }

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            PercaktoTemplateMenu();
        }

        #endregion

        #region Grid

        /// <summary>
        /// ruan konfigurimin e grides dhe filtrin e zgjedhur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RuajKolona_Click(object sender, EventArgs e)
        {
            var mesazh = GridUtil.ruajkonfigurimgridePaKonfigurim(gvRaporti, IdNdermarrja, IdPerdoruesi, "RaporteGrida.aspx?lloji=" + Request.QueryString["lloji"], IdViti, ci, IdGjuha);

            clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, IdNdermarrja, "gvRaporti", 1, "RaporteGrida.aspx?lloji=" + Request.QueryString["lloji"]);

            PercaktoTemplateMenu();

            if (mesazh.Status)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
        }

        /// <summary>
        /// mbush griden me te dhena te ruajtura ne sesion
        /// </summary>
        private void MbushGridNgaSession(DateTime dtfillimi, DateTime dtmbarimi, DateTime dtfillimiexe, DateTime dtmbarimiexe)
        {
            var sukses = mySessionObjects.merrGrideNgaSessioni(Session, out DataTable tmpObject);
            if (!sukses)
                MbushGridNgaDb(dtfillimi, dtmbarimi, dtfillimiexe, dtmbarimiexe);
            else
            {
                gvRaporti.DataSource = tmpObject;
                gvRaporti.DataBind();
                tmpObject.Dispose();
            }
        }

        /// <summary>
        /// mbush griden me te dhena nga db
        /// </summary>
        private void MbushGridNgaDb(DateTime dtfillimi, DateTime dtmbarimi, DateTime dtfillimiexe, DateTime dtmbarimiexe)
        {
            DataTable dt;
            switch (Request.QueryString["lloji"])
            {
                case "tollon":
                    dt = DbCore.DbTollona.colShitjeMeSerial.merrShitjeMeSerialPerRaport(IdNdermarrja);
                    break;
                case "kastrat":
                    dt = DbCore.DbTollona.colTollonaLeter.merrTollonaLeterPerRaport(IdNdermarrja, dtfillimi, dtmbarimi, dtfillimiexe, dtmbarimiexe);
                    break;
                case "GjendjaEMagazines":
                    dt = DbCore.DbInventari.colSerialeUnikeMagazina.MerrRaportinGjendjaEMagazines("", dtfillimi.ToString(), dtmbarimi.ToString(), "", "", "", "", "", "", "", "", IdNdermarrja.ToString(), "", "", "", "", "", "", "", "", "", "", IdPerdoruesi.ToString());
                    break;
                case "gjendjaArtikujveIMEI":
                case "gjendjaArtikujveIMEIEkspozitor":
                    var filterExpression = CriteriaOperator.Parse(gvRaporti.FilterExpression, 0);
                    string filterString = CriteriaToWhereClauseHelper.GetMsSqlWhere(filterExpression);
                    string dtDokFillimi = dtfillimi.ToShortDateString();
                    string dtDokMbarimi = dtmbarimi.ToShortDateString();
                    string dtDok = $"(  (T_KOKAMAGAZINA.DTDOK>=convert(datetime, '{dtDokFillimi}',103) and T_KOKAMAGAZINA.DTDOK <=convert(datetime,'{dtDokMbarimi}',103) ) ) AND ";
                    string dtRegj = $"(  (T_KOKAMAGAZINA.DTREGJISTRIMI>=convert(datetime,'01/01/1900',103) and T_KOKAMAGAZINA.DTREGJISTRIMI <=convert(datetime,'31/12/9999',103) ) ) AND ";
                    dt = colArtikujt.MerrRaportinGjendjaEArtikujveMeImei(Request.QueryString["lloji"], IdNdermarrja, IdPerdoruesi, dtDokFillimi, dtDokMbarimi, dtDok, dtRegj, filterString);
                    if (dt.Rows.Count > 0)
                    {
                        bool shfaqCmime = (dt.Rows[0]["SHFAQCM_BLERJE"].ToString().ToLower()) == "po";
                        if (!shfaqCmime)
                        {
                            dt.Columns.Remove("CmimBlerjePaTvsh");
                            dt.Columns.Remove("CmimBlerjeMeTvsh");
                        }
                    }
                    break;
                default:
                    var op1 = CriteriaColumnAffinityResolver.SplitByColumnNames(CriteriaOperator.Parse(gvRaporti.FilterExpression));
                    var op = op1.Item2;

                    var filterSe = op.Count != 0 && op.ContainsKey("SerialiKryesor") ? ((DevExpress.Data.Filtering.FunctionOperator)op["SerialiKryesor"])?.Operands[1].ToString() : string.Empty;
                    var filterNrSerial = Regex.Replace(filterSe, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled);

                    op.Remove("SerialiKryesor");
                    op.Remove("SerialiDytesor");
                    var filter = CriteriaToWhereClauseHelper.GetMsSqlWhere(CriteriaOperator.And(op.Select(d => d.Value).ToList()));

                    dt = DbCore.DbInventari.colSerialeUnikeMagazina.MerrRaportinGjendjaEArtikujveMeSeriale(IdNdermarrja, IsPostBack ? dtmbarimi.ToShortDateString() : "01/01/1990", filter, filterNrSerial);
                    break;
            }

            mySessionObjects.ruajGrideNeSession(Session, dt);
            gvRaporti.DataSource = dt;
            gvRaporti.DataBind();
            dt.Dispose();

            mySessionObjects.ruajdtfillNeSesion(Session, dtfillimi);
            mySessionObjects.ruajdtfillexeNeSesion(Session, dtfillimiexe);
            mySessionObjects.ruajdtmbarNeSesion(Session, dtmbarimi);
            mySessionObjects.ruajdtmbarexeNeSesion(Session, dtmbarimiexe);
        }

        /// <summary>
        /// konfiguron griden
        /// </summary>
        private void KonfiguroGride()
        {
            switch (Request.QueryString["lloji"])
            {
                case "gjendjaArtikujveIMEI":
                case "gjendjaArtikujveIMEIEkspozitor":
                    gvRaporti.SettingsBehavior.FilterRowMode = GridViewFilterRowMode.OnClick;
                    GridViewDataTextColumn cmimeBlerjePaTvsh = gvRaporti.Columns["CmimBlerjePaTvsh"] as GridViewDataTextColumn;
                    GridViewDataTextColumn cmimeBlerjeMeTvsh = gvRaporti.Columns["CmimBlerjeMeTvsh"] as GridViewDataTextColumn;
                    if (cmimeBlerjePaTvsh != null)
                    {
                        cmimeBlerjePaTvsh.Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False;
                        GridUtil.VendosFormatNumri(cmimeBlerjePaTvsh, 2);
                    }
                    if (cmimeBlerjeMeTvsh != null)
                    {
                        cmimeBlerjeMeTvsh.Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False;
                        GridUtil.VendosFormatNumri(cmimeBlerjeMeTvsh, 2);
                    }
                    GridUtil.VendosFormatNumriPerFushatNumerike(gvRaporti, 0, "Gjendja", "Mbartur", "SasiaFD", "SasiaFH");
                    break;
                case "GjendjaEMagazines":
                    gvRaporti.SettingsBehavior.FilterRowMode = GridViewFilterRowMode.OnClick;
                    (gvRaporti.Columns["Kosto"] as GridViewDataTextColumn).Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False;
                    (gvRaporti.Columns["VD"] as GridViewDataTextColumn).Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False;
                    GridUtil.VendosFormatNumriPerFushatNumerike(gvRaporti, 2, "Gjendja", "Mbartur", "SasiaFD", "SasiaFH", "VD", "Kosto");
                    gvRaporti.ShtoTotalSummary("#,#.00", DevExpress.Data.SummaryItemType.Sum, "Gjendja", "Mbartur", "SasiaFH", "SasiaFD", "VD");
                    GridUtil.PercaktoTemplateTotalSummaryFooter(gvRaporti, "#,#.00", "Gjendja", "Mbartur", "SasiaFH", "SasiaFD", "VD");
                    break;
                case "GjendjaEArtikujveMeSeriale":
                    gvRaporti.SettingsBehavior.FilterRowMode = GridViewFilterRowMode.OnClick;
                    var col = gvRaporti.Columns["Gjendja"] as GridViewDataTextColumn;
                    col.Settings.AllowAutoFilter = DevExpress.Utils.DefaultBoolean.False;
                    gvRaporti.GroupBy(gvRaporti.Columns["KodArtikulli"]);
                    gvRaporti.ExpandAll();
                    gvRaporti.PercaktoTemplateGroupSummaryFooter("n0", "Gjendja");
                    gvRaporti.ShtoGroupSummary("n0", DevExpress.Data.SummaryItemType.Sum, "Gjendja");
                    gvRaporti.ShtoTotalSummary("n0", DevExpress.Data.SummaryItemType.Sum, "Gjendja");
                    GridUtil.PercaktoTemplateTotalSummaryFooter(gvRaporti, "n0", "Gjendja");
                    KonfigurimComboGride.ShtoKategoriSerileshUnike(gvRaporti, IdNdermarrja, Session, Request.QueryString["lloji"], GuidString, "IdKategori");
                    KonfigurimComboGride.ShtoMagazinaSipasPerdoruesitDheNdermarrjes(gvRaporti, IdNdermarrja, IdPerdoruesi, Session, Request.QueryString["lloji"], GuidString, "IDNJESIADM");
                    break;
                default:
                    ShtoDtEkzekutimiTime();
                    gvRaporti.Settings.ShowFooter = true;

                    var col3 = gvRaporti.Columns["Cmimi"] as GridViewDataTextColumn;
                    col3.PropertiesEdit.DisplayFormatString = "0.00000";

                    var col2 = gvRaporti.Columns["CmimFaturimi"] as GridViewDataTextColumn;
                    col2.PropertiesEdit.DisplayFormatString = "0.00000";

                    var col4 = gvRaporti.Columns["Diferenca"] as GridViewDataTextColumn;
                    col4.PropertiesEdit.DisplayFormatString = "0.00000";

                    gvRaporti.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "Diferenca");
                    gvRaporti.TotalSummary.GetVisibleItem(0).DisplayFormat = "Shuma {0}";

                    if (Request.QueryString["lloji"] == "kastrat")
                    {
                        col4.PropertiesEdit.DisplayFormatString = "0.00000";
                        gvRaporti.TotalSummary.Add(DevExpress.Data.SummaryItemType.Sum, "TotaliLitra");
                        gvRaporti.TotalSummary.GetVisibleItem(1).DisplayFormat = "Shuma {0}";
                    }
                    break;
            }
        }

        private void ShtoDtEkzekutimiTime()
        {
            gvRaporti.Columns.Remove(gvRaporti.Columns["DtEkzekutimi"]);

            var colnew = new GridViewDataDateColumn();
            colnew.PropertiesDateEdit.DateOnError = DateOnError.Undo;
            colnew.PropertiesDateEdit.DisplayFormatString = Request.QueryString["lloji"] == "tollon" ? "dd/MM/yyyy hh:mm:ss tt" : "dd/MM/yyyy";
            colnew.FieldName = "DtEkzekutimi";
            colnew.Settings.AutoFilterCondition = AutoFilterCondition.Greater;
            gvRaporti.Columns.Add(colnew);
        }

        /// <summary>
        /// kur grida ben databound per ti shtuar kolonen e selektimit
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvRaporti_DataBound(object sender, EventArgs e)
        {
            if (Request.QueryString["lloji"] == "GjendjaEMagazina")
            {
                GridUtil.GridDataBound(sender, e, gvRaporti, "IdArtikulli");
            }
            else if (Request.QueryString["lloji"] == "GjendjaEArtikujveMeSeriale")
            {
                GridUtil.GridDataBound(sender, e, gvRaporti, "SerialiKryesor");
                gvRaporti.Settings.ShowGroupFooter = GridViewGroupFooterMode.VisibleAlways;
                gvRaporti.Settings.ShowFooter = true;
                gvRaporti.ExpandAll();
            }
            else
            {
                GridUtil.GridDataBound(sender, e, gvRaporti, "Id");
            }
        }

        /// <summary>
        /// fshin filtrin e zgjedhur dhe aplikuar mbi gride, fshirje nga DB ku ai eshte ruajtur, dhe jo pastrimi i grides nga aplikimi i filtrit
        /// per kete do sherbeje Clear Filter ne fund te grides
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
            var itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            var cmbFiltra = ((MenuFilter)itemButton.Template).FindControl("btnFiltra") as ASPxComboBox;

            var filtra = new DbCore.DbAdmin.clsFiltraGrida();
            var koka = new DbCore.DbAdmin.clsGridaKoka(IdGjuha, "gvRaporti", "RaporteGrida.aspx?lloji=" + Request.QueryString["lloji"], IdNdermarrja, 1);
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, IdNdermarrja, koka.IdGridaKoka);

            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = IdPerdoruesi;
                var mesazh = filtra.fshi();
                clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, IdNdermarrja, "gvRaporti", 1, "RaporteGrida.aspx?lloji=" + Request.QueryString["lloji"]);
                PercaktoTemplateMenu();

                if (mesazh.Status)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                gvRaporti.FilterExpression = "";
            }
        }

        /// <summary>
        /// sherben per te ruajtur filtrin e zgjedhur
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
            var itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            var cmbFiltra = ((MenuFilter)itemButton.Template).FindControl("btnFiltra") as ASPxComboBox;

            var koka = new DbCore.DbAdmin.clsGridaKoka(IdGjuha, "gvRaporti", "RaporteGrida.aspx ? lloji = " + Request.QueryString["lloji"], IdNdermarrja, 1);
            var filtri = new DbCore.DbAdmin.clsFiltraGrida
            {
                FiltraKodi = cmbFiltra.Text,
                FiltraShenime = cmbFiltra.Text,
                FiltraUniversal = false,
                GridaKokaId = koka.IdGridaKoka,
                FiltraVlera = gvRaporti.FilterExpression,
                IdPerdoruesi = IdPerdoruesi,
                IdNdermarje = IdNdermarrja,
                IdStatusDok = 1
            };
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("Id", gvRaporti);
            //var kolona = gvRaporti.GetSortedColumns();
            //if (kolona.Count > 0)
            //{
            //    filtri.KoloneRenditje = kolona[0].FieldName;
            //    filtri.DrejtimRenditje = kolona[0].SortOrder == DevExpress.Data.ColumnSortOrder.Ascending;
            //}
            //else
            //{
            //    filtri.KoloneRenditje = "Id";
            //    filtri.DrejtimRenditje = true;
            //}

            var mesazh = filtri.ruaj();

            clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, IdNdermarrja, "gvRaporti", 1, "RaporteGrida.aspx?lloji=" + Request.QueryString["lloji"]);
            PercaktoTemplateMenu();

            if (mesazh.Status)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

            cmbFiltra.Text = "";
        }

        /// <summary>
        /// kur grida ben callback
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvRaporti_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            GridUtil.EmrateButonaveMbiGride(gvRaporti);
            if (e.CallbackName == "COLUMNMOVE" && gvRaporti.AllColumns[int.Parse(e.Args[0])].Width.Value == 0)
                gvRaporti.AllColumns[int.Parse(e.Args[0])].Width = System.Web.UI.WebControls.Unit.Percentage(3);

            if (e.CallbackName == "APPLYFILTER" && e.Args[0] == "" && Request.QueryString["lloji"] != "GjendjaEArtikujveMeSeriale")
            {
                if (Request.QueryString["lloji"].EqualsAnyIgnoreCase("gjendjaArtikujveIMEIEkspozitor", "gjendjaArtikujveIMEI"))
                    Filtro();
                else
                {
                    var itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
                    var cmbFiltra = ((MenuFilter)itemButton.Template).FindControl("btnFiltra") as ASPxComboBox;
                    cmbFiltra.Text = "";
                }
            }

            if (e.CallbackName == "APPLYMULTICOLUMNFILTER" && Request.QueryString["lloji"].EqualsAnyIgnoreCase("GjendjaEArtikujveMeSeriale", "gjendjaArtikujveIMEI", "gjendjaArtikujveIMEIEkspozitor", "GjendjaEMagazines"))
            {
                Filtro();
            }

            gvRaporti.Columns["#"].VisibleIndex = 0;
            gvRaporti.ExpandAll();
        }

        /// <summary>
        /// kur perdoruesi i ben grides callbak per te mare konfigurimin e ri
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvRaporti_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            GridUtil.percaktoVisibleColumnsMeWidth(IdGjuha, IdNdermarrja, gvRaporti, "gvRaporti", "RaporteGrida.aspx?lloji=" + Request.QueryString["lloji"]);

            if (e.Parameters == "filtro" || e.Parameters == "APPLYCOLUMNFILTER")
            {
                Filtro();
            }

            var hf = new HiddenField();
            GridUtil.GridCustomCallbackDefault(sender, e, gvRaporti, "RaporteGrida.aspx?lloji=" + Request.QueryString["lloji"], IdNdermarrja, IdGjuha, null, ref hf);
        }

        /// <summary>
        /// perdoret per te vendosur karakteristika te griden ne javascript
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvRaporti_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvRaporti.PageIndex;
            e.Properties["cpPageRow"] = gvRaporti.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvRaporti.VisibleRowCount;
        }

        /// <summary>
        /// perdoret per te hequr filtrimin e grides kur zgjedhim elementin bosh
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvRaporti_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {


        }

        /// <summary>
        /// perdoret per te shtuar menyra filtrimi
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvRaporti_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            if (Request.QueryString["lloji"] != "GjendjaEArtikujveMeSeriale" && Request.QueryString["lloji"] != "GjendjaEMagazines")
            {
                GridUtil.GridHeaderFilterFillItem(sender, e, rm, ci, "Klienti", "KodKlienti", "LlojiTolloni", "PikeShitje", "Perdoruesi");
            }
        }

        protected void btnXlsxExport_Click(object sender, EventArgs e)
        {
            try
            {
                gridExport.WriteXlsxToResponse(Request.QueryString["lloji"], true);

            }
            catch (Exception)
            {
            }
        }

        protected void btnPdfExport_Click(object sender, EventArgs e)
        {
            try
            {

                gridExport.WritePdfToResponse(Request.QueryString["lloji"], true);

            }
            catch (Exception)
            {
            }
        }

        private void Filtro()
        {
            ASPxRadioButtonList radDtDok = gvRaporti.FindTitleTemplateControl("radDtDok5") as ASPxRadioButtonList;
            DateTime dtfillimi = new DateTime(), dtmbarimi = new DateTime(), dtfillimiexe = new DateTime(), dtmbarimiexe = new DateTime();
            DbCore.DbAdmin.clsPeriudhaKontabel oPeriudha = DbCore.mySessionObjects.merrPeriudheKontabel(Session);

            if (radDtDok.SelectedIndex == 0)
            {
                dtfillimiexe = oPeriudha.FillimiPeriudha;
                dtmbarimiexe = oPeriudha.MbarimiPeriudha;
            }
            else if (radDtDok.SelectedIndex == 1)
            {
                if ((gvRaporti.FindTitleTemplateControl("txtNgaDok1") as ASPxDateEdit) != null)
                {

                    ASPxDateEdit txtNgaDok = gvRaporti.FindTitleTemplateControl("txtNgaDok1") as ASPxDateEdit;
                    ASPxDateEdit txtDeriDok = gvRaporti.FindTitleTemplateControl("txtDeriDok1") as ASPxDateEdit;
                    dtfillimiexe = txtNgaDok.Date;
                    dtmbarimiexe = txtDeriDok.Date;
                }
            }
            else if (radDtDok.SelectedIndex == 2)
            {
                dtfillimiexe = new DateTime(oPeriudha.FillimiPeriudha.Year, 1, 1);
                dtmbarimiexe = new DateTime(oPeriudha.FillimiPeriudha.Year, 12, 31);

            }
            else
            {
                dtfillimiexe = new DateTime(1970, 1, 1);
                dtmbarimiexe = new DateTime(2100, 12, 31);
            }

            ASPxRadioButtonList radDtDok5 = gvRaporti.FindTitleTemplateControl("radDtDok11") as ASPxRadioButtonList;
            if (radDtDok5.SelectedIndex == 0)
            {
                dtfillimi = oPeriudha.FillimiPeriudha;
                dtmbarimi = oPeriudha.MbarimiPeriudha;
            }
            else if (radDtDok5.SelectedIndex == 1)
            {
                if ((gvRaporti.FindTitleTemplateControl("txtNgaDok") as ASPxDateEdit) != null)
                {

                    ASPxDateEdit txtNgaDok1 = gvRaporti.FindTitleTemplateControl("txtNgaDok") as ASPxDateEdit;
                    ASPxDateEdit txtDeriDok1 = gvRaporti.FindTitleTemplateControl("txtDeriDok") as ASPxDateEdit;
                    dtfillimi = txtNgaDok1.Date;
                    dtmbarimi = txtDeriDok1.Date;
                }
            }
            else if (radDtDok5.SelectedIndex == 2)
            {
                dtfillimi = new DateTime(oPeriudha.FillimiPeriudha.Year, 1, 1);
                dtmbarimi = new DateTime(oPeriudha.FillimiPeriudha.Year, 12, 31);
            }
            else
            {
                dtfillimi = new DateTime(1970, 1, 1);
                dtmbarimi = new DateTime(2100, 12, 31);
            }

            mySessionObjects.ruajradNeSesion(Session, radDtDok5.SelectedIndex);
            mySessionObjects.ruajrad2NeSesion(Session, radDtDok.SelectedIndex);

            MbushGridNgaDb(dtfillimi, dtmbarimi, dtfillimiexe, dtmbarimiexe);
        }

        #endregion
    }
}