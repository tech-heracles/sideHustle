using System;
using System.Data;
using System.Linq;
using DbCore;
using DbCore.DbAdmin;
using DbCore.DbKontabiliteti;
using DbCore.DbShare;
using DbCore.IMBUtils.Messages;
using DevExpress.Data;
using DevExpress.Data.Filtering;
using DevExpress.Web;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class LupaKlientFurnitor : MyPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!mySessionObjects.isLogedIn(Session))
            {
                clsFunksione.logout(Session, true, "FaqePaautorizuar");
            }

            var vleraQueryString = "";
            if (Request.QueryString["idKonfigAmbjente"] != null && Request.QueryString["idKonfigAmbjente"] != "")
                vleraQueryString = Request.QueryString["idKonfigAmbjente"];

            int idKonfigambjenti = clsFunksione.getIdKonfigAmbLupa(vleraQueryString, IdNdermarrja, "KL/FUR");
            //Dhe nese ambjenti qe duhet te hapet eshte filtri i furnitorit, funksioni i mesiperm kthen idkonfigambjentin per filtrin e klientit
            //pasi te dyja kane vlere te njejte idniveli dhe nga ndarja e vleraQueryString idkonfig i pare qe do te vije eshte per klientin
            //Bejme kontrollin nese duhet hapur lupa e furnitorit dhe ne kete rast gjejme idkonfig perkates duke kerkuar ne pjesen e pakontrolluar te vleraQueryString-ut
            if (Request.QueryString["veprimi"] == "2")
            {
                if (vleraQueryString != "")
                {
                    var idte = vleraQueryString.Split('-');
                    for (int i = 0; i < idte.Length; i++)
                    {
                        if (Convert.ToInt32(idte[i]) != idKonfigambjenti) continue;

                        idte = idte.Skip(i + 1).Take(idte.Length - i - 1).ToArray();
                        break;
                    }

                    vleraQueryString = string.Join("-", idte);
                    idKonfigambjenti = clsFunksione.getIdKonfigAmbLupa(vleraQueryString, IdNdermarrja, "KL/FUR");
                }
            }

            if (!IsPostBack)
            {
                cmbKonfigurimi.Value = idKonfigambjenti.ToString();
                VendosHfMePerkthime();
                PercaktoTemplateMenu();
                clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, IdNdermarrja, "gvLupaKlientFurnitor", idKonfigambjenti, "LupaKlientFurnitor.aspx");
                hfState.Set("idKonfigambjenti", idKonfigambjenti);
                MbushGrideNgaDb();

                var tedrejtaInfo = new clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();

                if (Request.QueryString["idKfV"] != null && Request.QueryString["idKfV"] != "")
                {
                    var idKlienteFurnitoreVartes = Request.QueryString["idKfV"].Split(',').ToList();
                    foreach (var id in idKlienteFurnitoreVartes)
                    {
                        gvLupaKlientFurnitor.Selection.SelectRowByKey(id);
                    }
                }

                if (clsAlternativaKushti.getAlternativa(idKonfigambjenti, "KSSH") == "Po")
                    hfState["KSSH"] = true;
                else
                    hfState["KSSH"] = false;
                if (clsAlternativaKushti.getAlternativa(idKonfigambjenti, "ES") == "Po")
                    hfState["ES"] = true;
                else
                    hfState["ES"] = false;
                KonfiguroPopupGride(true, idKonfigambjenti);
            }
            else
            {
                if (!IsCallback || IsCallback && Request["__CALLBACKID"].Contains("ASPxMenu1"))
                    PercaktoTemplateMenu();

                if (!IsCallback || IsCallback && Request["__CALLBACKID"].Contains("gvLupaKlientFurnitor"))
                {
                    MbushPopUpListeNgaSession(idKonfigambjenti);
                    KonfiguroPopupGride(false, idKonfigambjenti);
                }
            }
        }

        private void VendosHfMePerkthime()
        {
            hfState.Set("JQgridShtoFurnitor", MessagesResource.Messages["JQgridShtoFurnitor"]);
            hfState.Set("JQgridModifikoKlientFurnitor", MessagesResource.Messages["JQgridModifikoKlientFurnitor"]);
            hfState.Set("JQgridShtoKlient", MessagesResource.Messages["JQgridShtoKlient"]);
            hfState.Set("msgSelektoniNjeRresht", MessagesResource.Messages["msgSelektoniNjeRresht"]);
            hfState.Set("msgLupaKFNukMundTeZgjidhniMeShumeSeNjeKF", MessagesResource.Messages["msgLupaKFNukMundTeZgjidhniMeShumeSeNjeKF"]);
            hfState.Set("msgLupaKFEkzistonKyKFNeGride", MessagesResource.Messages["msgLupaKFEkzistonKyKFNeGride"]);
            hfState.Set("headerPopUpZgjidhFiltrin", MessagesResource.Messages["headerPopUpZgjidhFiltrin"]);
            hfState.Set("headerPopUpKlonoKlientFurnitor", MessagesResource.Messages["headerPopUpKlonoKlientFurnitor"]);
        }

        private void MbushPopUpListeNgaSession(int idKonfigAmbjenti)
        {
            object tmpObject;
            mySessionObjects.merrGrideNgaSessioniLupa(Session, out tmpObject);
            if (tmpObject == null)
            {
                MbushGrideNgaDb();
                return;
            }
            gvLupaKlientFurnitor.DataSource = tmpObject;
            gvLupaKlientFurnitor.DataBind();
        }

        /// <summary>
        /// mbush griden e popupit me te dhena  
        /// </summary>
        /// <param name="idPerdoruesi"></param>
        public override void MbushGrideNgaDb(bool ndryshoFiltrinGrides = false)
        {
            int idPerdoruesi = IdPerdoruesi;
            int idKonfigAmbjenti = Convert.ToInt32(cmbKonfigurimi.Value);
            string filterDefault = Request.QueryString["KlientApoFurnitor"] == "Klient" || Request.QueryString["veprimi"] == "1" ? "[LlojiKF] = true" : Request.QueryString["KlientApoFurnitor"] == "Furnitor" || Request.QueryString["veprimi"] == "2" ? "[LlojiKF] = false" : "";

            var op = CriteriaOperator.Parse(string.IsNullOrWhiteSpace(gvLupaKlientFurnitor.FilterExpression) ? filterDefault : gvLupaKlientFurnitor.FilterExpression, 0);
            string filterString = CriteriaToWhereClauseHelper.GetMsSqlWhere(op);
            if (string.IsNullOrWhiteSpace(gvLupaKlientFurnitor.FilterExpression))
                gvLupaKlientFurnitor.FilterExpression = filterDefault;

            string topRows = "";
            var topRowControl = this.MerrTopRowsControl(hfState);
            if (topRowControl != null)
            {
                if (((DataTable)gvLupaKlientFurnitor.DataSource)?.Rows.Count > 50 && string.IsNullOrEmpty(topRowControl.GetTopRowsPerSql()) && gvLupaKlientFurnitor.FilterExpression.ToLower() != filterDefault.ToLower() && Request.Params["__CALLBACKPARAM"].Contains("COLUMNFILTER"))
                    return;
                topRows = topRowControl.GetTopRowsPerSql();
            }
            else
            {
                clsGridaKoka gridaKoka = new clsGridaKoka("gvLupaKlientFurnitor", "LupaKlientFurnitor.aspx", IdNdermarrja, idKonfigAmbjenti);
                topRows = (gridaKoka.TopRows != null && gridaKoka.TopRows > 0) ? gridaKoka.TopRows.ToString() : string.Empty;
            }

            if (!string.IsNullOrEmpty(topRows))
                topRows = $"TOP {topRows}";

            var dt = new DataTable();
            var meProspekt = false;
            var klientVarLidhurKlientKryesor = clsAlternativaKushti.getAlternativa((int)hfState.Get("idKonfigambjenti"), "SHKFVNVTKFK") == "Jo";
            int kfkryesor = 0;

            //me qellim qe te marrim nga db edhe klientet prospekt te cilet jane shtuar posacerisht per CRM
            if (!string.IsNullOrWhiteSpace(Request.QueryString["crm"]))
                meProspekt = Request.QueryString["crm"] == "po";

            if (!string.IsNullOrEmpty(Request.QueryString["kfkryesor"]))
                kfkryesor = Convert.ToInt32(Request.QueryString["kfkryesor"]);

            if (klientVarLidhurKlientKryesor)
                kfkryesor = 0;

            if (!string.IsNullOrEmpty(Request.QueryString["veprimi"]))
            {
                var veprimi = Request.QueryString["veprimi"];
                filterString = $"{filterString} {(kfkryesor != 0 ? $"AND \"IdKlientFurnitorKryesor\" = {kfkryesor}" : "")}";

                if (veprimi == "1" && !string.IsNullOrEmpty(Request.QueryString["meAgjent"]))
                {
                    var agjenti = new clsAgjentShitje(int.Parse(mySessionObjects.merrObjectNgaSesioni(Session).ToString()));
                    idPerdoruesi = agjenti.IdPerdoruesMobile;
                }
            }

            dt = colKlienteFurnitore.merrSipasKFNdermarrjesAndAutorizimeDTLupe(IdNdermarrja, idPerdoruesi, meProspekt, idKonfigAmbjenti, filterString, topRows);
            mySessionObjects.ruajGrideNeSessionLupa(Session, dt);
            gvLupaKlientFurnitor.DataSource = dt;
            gvLupaKlientFurnitor.DataBind();
            dt.Dispose();
        }

        private void KonfiguroPopupGride(bool visibleIndex, int idKonfigambjenti)
        {
            ShtoLloj(visibleIndex);
            ShtoProspekt();
            if (!IsPostBack)
            {
                string kodKonfigAmbjente = clsKonfigurimAmbjenti.ktheKodKonfigurimi(idKonfigambjenti);
                GridUtil.percaktoVisibleColumnsSipasKonfigurimit(gvLupaKlientFurnitor, "gvLupaKlientFurnitor", "LupaKlientFurnitor.aspx", idKonfigambjenti, true, IdGjuha);
            }

            GridUtil.KonfiguroGrideListeMadhePopupiPaTheme(gvLupaKlientFurnitor, "IdKlientFurnitor", (bool)hfState["KSSH"], (bool)hfState["ES"]);
            gvLupaKlientFurnitor.PercaktoTitlePanelMeRefresh(this, MenuInfo, pnlMesazhi, hfState, IdPerdoruesi, IdNdermarrja, IdViti, IdGjuha, idKonfigambjenti, "LupaKlientFurnitor.aspx", rm, ci, true);
        }

        private void ShtoProspekt()
        {
            if (gvLupaKlientFurnitor.Columns["Prospekt"] == null)
                return;
            var colnew = new GridViewDataComboBoxColumn();

            gvLupaKlientFurnitor.Columns.Remove(gvLupaKlientFurnitor.Columns["Prospekt"]);

            colnew.PropertiesComboBox.Items.Add("", null);
            colnew.PropertiesComboBox.Items.Add("Prospekt", true);
            colnew.PropertiesComboBox.Items.Add("Financiar", false);
            colnew.FieldName = "Prospekt";
            colnew.Caption = "Status";
            gvLupaKlientFurnitor.Columns.Add(colnew);
        }

        private void ShtoLloj(bool index)
        {
            if (gvLupaKlientFurnitor.Columns["LlojiKF"] == null)
                return;
            var colnew = new GridViewDataComboBoxColumn();

            if (typeof(GridViewDataComboBoxColumn) != gvLupaKlientFurnitor.Columns["LlojiKF"].GetType())
            {
                int visibleindex = gvLupaKlientFurnitor.Columns["LlojiKF"].VisibleIndex;
                gvLupaKlientFurnitor.Columns.Remove(gvLupaKlientFurnitor.Columns["LlojiKF"]);
                gvLupaKlientFurnitor.Columns.Add(colnew);
                colnew.PropertiesComboBox.EnableCallbackMode = true;
                colnew.PropertiesComboBox.Items.Add(rm.GetString("comboItemBlerjeShitjeKlient", ci), true);
                colnew.PropertiesComboBox.Items.Add(rm.GetString("comboItemBlerjeShitjeFurnitor", ci), false);
                colnew.PropertiesComboBox.ValueField = "LlojiKF";
                colnew.FieldName = "LlojiKF";
                colnew.Caption = rm.GetString("labelRaportiLloji", ci);
                colnew.VisibleIndex = 1;

                if (!index)
                    colnew.VisibleIndex = visibleindex;

                mySessionObjects.ruajDsComboGrideNeSession(Session, colnew.PropertiesComboBox.Items, "colLlojiKF");
            }
            else
            {
                colnew = (GridViewDataComboBoxColumn)gvLupaKlientFurnitor.Columns["LlojiKF"];
                if (colnew.PropertiesComboBox.Items.Count == 0)
                    colnew.PropertiesComboBox.Items.AddRange((ListEditItemCollection)mySessionObjects.merrDsComboGrideNeSession(Session, "colLlojiKF"));
            }
        }

        protected void gvLupaKlientFurnitor_DataBound(object sender, EventArgs e)
        {
            if (gvLupaKlientFurnitor.Columns.Count == 0 || gvLupaKlientFurnitor.Columns["#"] != null)
                return;

            var check = new GridViewCommandColumn("#")
            {
                ShowSelectCheckbox = true,
                Width = System.Web.UI.WebControls.Unit.Percentage(4)
            };
            gvLupaKlientFurnitor.Settings.ShowFilterRow = true;
            gvLupaKlientFurnitor.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
            gvLupaKlientFurnitor.Settings.ShowFilterRowMenu = true;
            gvLupaKlientFurnitor.Columns.Add(check);
            gvLupaKlientFurnitor.KeyFieldName = "IdKlientFurnitor";
            gvLupaKlientFurnitor.SettingsBehavior.AllowSelectByRowClick = true;
            gvLupaKlientFurnitor.SettingsBehavior.AllowFocusedRow = true;
            gvLupaKlientFurnitor.Columns["#"].VisibleIndex = 0;
        }

        protected void gvLupaKlientFurnitor_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            if (this.NdryshimFiltriGrida(gvLupaKlientFurnitor.ID))
            {
                MbushGrideNgaDb();
            }
        }

        protected void ASPxMenu1_DataBound(object sender, EventArgs e) =>
            PercaktoTemplateMenu();

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        private void PercaktoTemplateMenu()
        {
            clsToolbarConfig.percaktoTemplateMenu(IdGjuha, IdViti, IdPerdoruesi, IdNdermarrja, ASPxMenu1, "LupaKlientFurnitor.aspx", this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, true, false, false, Meme, Request.QueryString["theme"] != null && Request.QueryString["theme"] == "Moderno");
            ASPxMenu1.Items.FindByName("Anullo").Text = MessagesResource.Messages["MenuItemMbyll"];
            ASPxMenu1.Items.FindByName("Shto").Text = MessagesResource.Messages["menuItemShtoKlient"];
            ASPxMenu1.Items.FindByName("ShtoFurnitor").Index = 2;
        }

        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
            clsMenuInfo.ShtoMenuItemInfo(this, MenuInfo);
            var itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            var cmbFiltra = ((MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            var filtri = new clsFiltraGrida
            {
                FiltraKodi = cmbFiltra.Text,
                FiltraShenime = cmbFiltra.Text,
                FiltraUniversal = false,
                IdPerdoruesi = IdPerdoruesi,
                IdNdermarje = IdNdermarrja,
                IdStatusDok = 1
            };
            var koka = new clsGridaKoka(IdGjuha, "gvLupaKlientFurnitor", "LupaKlientFurnitor.aspx", IdNdermarrja, Convert.ToInt32(cmbKonfigurimi.Value));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvLupaKlientFurnitor.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("IdKlientFurnitor", gvLupaKlientFurnitor);
            //var kolona = gvLupaKlientFurnitor.GetSortedColumns();
            //if (kolona.Count > 0)
            //{
            //    filtri.KoloneRenditje = kolona[0].FieldName;
            //    filtri.DrejtimRenditje = kolona[0].SortOrder == ColumnSortOrder.Ascending;
            //}
            //else
            //{
            //    filtri.KoloneRenditje = "IdKlientFurnitor";
            //    filtri.DrejtimRenditje = true;
            //}

            var mesazh = filtri.ruaj();
            PercaktoTemplateMenu();
            clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, IdNdermarrja, "gvLupaKlientFurnitor", Convert.ToInt32(cmbKonfigurimi.Value), "LupaKlientFurnitor.aspx");
            if (mesazh.Status)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

            cmbFiltra.Text = "";
        }

        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
            clsMenuInfo.ShtoMenuItemInfo(this, MenuInfo);
            var itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            var cmbFiltra = ((MenuFilter)itemButton.Template).FindControl("btnFiltra") as ASPxComboBox;
            var filtra = new clsFiltraGrida();
            var koka = new clsGridaKoka(IdGjuha, "gvLupaKlientFurnitor", "LupaKlientFurnitor.aspx", IdNdermarrja, Convert.ToInt32(cmbKonfigurimi.Value));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra?.Text, IdNdermarrja, koka.IdGridaKoka);

            if (filtra.FiltraKodi == null) return;

            filtra.IdPerdoruesi = IdPerdoruesi;
            var mesazh = filtra.fshi();
            PercaktoTemplateMenu();
            clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, IdNdermarrja, "gvLupaKlientFurnitor", Convert.ToInt32(cmbKonfigurimi.Value), "LupaKlientFurnitor.aspx");
            if (mesazh.Status)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

            cmbFiltra.Text = "";
            gvLupaKlientFurnitor.FilterExpression = string.Empty;
        }

        protected void gvLupaKlientFurnitor_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            bool getFromDb = false;
            var arr = e.Parameters.Split(';');
            switch (arr.Length)
            {
                case 1:
                    if (arr[0] == "ndryshimMenyreFiltrimi" && !string.IsNullOrEmpty(gvLupaKlientFurnitor.FilterExpression))
                        getFromDb = true;
                    break;
                case 3:
                    if (arr[2] == "")
                        gvLupaKlientFurnitor.FilterExpression = "";
                    else
                    {
                        var filtra = new clsFiltraGrida();
                        var koka = new clsGridaKoka(IdGjuha, "gvLupaKlientFurnitor", "LupaKlientFurnitor.aspx", IdNdermarrja, Convert.ToInt32(cmbKonfigurimi.Value));
                        filtra.mbushFilterPerGrideSipasKodit(arr[2], IdNdermarrja, koka.IdGridaKoka);

                        if (filtra.FiltraKodi != null)
                        {
                            gvLupaKlientFurnitor.FilterExpression = filtra.FiltraVlera;
                            GridUtil.renditGriden(filtra.KoloneRenditje, gvLupaKlientFurnitor);
                            getFromDb = true;
                        }
                    }
                    break;
                default:
                    getFromDb = false;
                    break;
            }
            
            if (getFromDb)
            {
                int idKonfigAmbjente = Convert.ToInt32(cmbKonfigurimi.Value);
                MbushGrideNgaDb();
                KonfiguroPopupGride(true, idKonfigAmbjente);
            }

            if (string.IsNullOrEmpty(Request.QueryString["vjenNgaRaporti"]))
                gvLupaKlientFurnitor.Selection.UnselectAll();
        }

        protected void gvLupaKlientFurnitor_PreRender(object sender, EventArgs e) =>
            ((ASPxGridView)sender).FocusedRowIndex = -1;
    }
}