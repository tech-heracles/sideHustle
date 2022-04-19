using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Collections;
using System.Drawing;
using DbCore.DbRegjistrim;
using System.Data;
using PlatinumWeb.Templates;
using System.Globalization;
using System.Resources;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.DbShare;

namespace PlatinumWeb
{
    public partial class LupaShtoArtikull : MyPageBase
    {
        //private string koloneFocus;
        ASPxTextBox temptxt = null;
        ASPxComboBox tempcombo = null;
        public static int idNdermVit = -1;
        private int idKonfigambjenti;
        DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();
        protected void Page_Load(object sender, EventArgs e)
        {
            String array = Request.QueryString["array"];
            string vleraQueryString = "";
            if (Request.QueryString["idKonfigAmbjente"] != null && Request.QueryString["idKonfigAmbjente"] != "")
                vleraQueryString = Request.QueryString["idKonfigAmbjente"].ToString();
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            if (!Page.IsCallback)
                mbushPopUpListeArtikujsh();
            //Container.Attributes["width"] = "400px";
            //Container.Attributes["height"] = "400px";
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
            int idKonfigambjenti = DbCore.clsFunksione.getIdKonfigAmbLupa(vleraQueryString, idNdermarrje, "ShtArt");
            bool kerkosaposhkruar = true;
            if (DbCore.DbShare.clsAlternativaKushti.getAlternativa(idKonfigambjenti, "KSSH") == "Po")
                kerkosaposhkruar = true;
            else kerkosaposhkruar = false;
            //mbushPopUpListeNiveleZbritje();
            //konfiguroPopupGride();
            if (!IsPostBack)
            {
                //DbCore.clsFunksione.konfiguroMenuRuajPerLupa(ASPxMenu1);
                GridUtil.AplikoFilterDefault(gvLupaShtoArt, idKonfigambjenti);
                konfiguroPopupGride(idKonfigambjenti, true, kerkosaposhkruar);
            }
            else
                konfiguroPopupGride(idKonfigambjenti, false, kerkosaposhkruar);
            Container.Attributes["width"] = "350px";
            Container.Attributes["height"] = "400px";
            Container.Attributes["src"] = "LupaFiltra.aspx?grida=gvLupaNivZbPrind&page=LupaNivelZbritjePrind.aspx";
        }
        private ArrayList mbushComboPrioriteti()
        {//mbush combon e prioriteteve 
            ArrayList prioriteti = new ArrayList();
            prioriteti.Add("i");
            prioriteti.Add("ii");
            prioriteti.Add("iii");
            prioriteti.Add("iv");
            prioriteti.Add("v");
            prioriteti.Add("vi");
            prioriteti.Add("vii");
            prioriteti.Add("viii");
            prioriteti.Add("ix");
            prioriteti.Add("x");
            prioriteti.Add("xi");
            prioriteti.Add("xii");
            prioriteti.Add("xiii");
            prioriteti.Add("xiv");
            prioriteti.Add("xv");
            prioriteti.Add("xvi");
            prioriteti.Add("xvii");
            prioriteti.Add("xviii");
            prioriteti.Add("xix");
            prioriteti.Add("xx");
            return prioriteti;
        }
        private void mbushPopUpListeArtikujsh()
        {//mbush griden e popupit me te dhena            
            DbCore.DbInventari.colArtikujtZevendesues colArtikuj = new DbCore.DbInventari.colArtikujtZevendesues();
            if (Request.QueryString["value"].ToString() == "")
            {
                for (int i = 0; i < 5; i++)
                {
                    DbCore.DbInventari.clsArtikullZevendesues artikulli = new DbCore.DbInventari.clsArtikullZevendesues();
                    colArtikuj.Add(artikulli);
                }
            }
            else
            {
                string initVal = Request.QueryString["value"].ToString();
                string[] pars1 = initVal.Split(',');
                string initVal2 = Request.QueryString["emri"].ToString();
                string[] pars3 = initVal2.Split(',');
                string initVal3 = Request.QueryString["prioriteti"].ToString();
                string[] pars5 = initVal3.Split(',');
                string[] kodi = new string[pars1.Length];
                string[] emri = new string[pars1.Length];
                string[] prioriteti = new string[pars1.Length];
                if (initVal != "")//merren te dhenat e hiden fieldeve te trupave te fleteve kontabel nga javascipti
                {
                    for (int i = 0; i < pars1.Length; i++)
                    {
                        string[] pars2 = pars1[i].Split(':');
                        kodi[Convert.ToInt32(pars2[0])] = pars2[1];
                    }
                }
                if (initVal2 != "")
                {
                    for (int i = 0; i < pars3.Length; i++)
                    {
                        string[] pars4 = pars3[i].Split(':');
                        emri[Convert.ToInt32(pars4[0])] = pars4[1];
                    }
                }
                if (initVal3 != "")
                {
                    for (int i = 0; i < pars5.Length; i++)
                    {
                        string[] pars6 = pars5[i].Split(':');
                        prioriteti[Convert.ToInt32(pars6[0])] = pars6[1];
                    }
                }
                for (int i = 0; i < pars1.Length; i++)//krijohet kolectioni me trupat e fleteve kontabel e futura nga perdoruesi
                {
                    DbCore.DbInventari.clsArtikullZevendesues artikulli = new DbCore.DbInventari.clsArtikullZevendesues();
                    if (kodi[i] != null && kodi[i] != "null" && kodi[i] != "")
                    {
                        artikulli.KodArtikulli = kodi[i];
                        artikulli.IdArtikulliKryesor = DbCore.DbInventari.clsArtikulli.ktheIdArtikulli(kodi[i], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                        //artikulli.IdArtikulliKryesor = dbInventari.ktheArtikull(kodi[i], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session))[0].IdArtikulli;
                    } if (emri[i] != null && emri[i] != "null" && emri[i] != "")
                        artikulli.PershkrimArtikulli = emri[i];
                    if (prioriteti[i] != null && prioriteti[i] != "null" && prioriteti[i] != "")
                        artikulli.Prioriteti = prioriteti[i];
                    colArtikuj.Add(artikulli);
                }
                DbCore.DbInventari.clsArtikullZevendesues art = new DbCore.DbInventari.clsArtikullZevendesues();
                colArtikuj.Add(art);
            }
            gvLupaShtoArt.DataSource = colArtikuj;
            gvLupaShtoArt.DataBind();
        }

        private void konfiguroPopupGride(int idKonfigambjenti, bool visibleIndex, bool kerkosaposhkruar)
        {//konfiguron popupgriden
            GridUtil.percaktoVisibleColumnsSipasKonfigurimit(gvLupaShtoArt, "gvLupaShtoArt", "LupaShtoArtikull.aspx", idKonfigambjenti, visibleIndex, DbCore.mySessionObjects.ktheGjuhe(Session));
            //funk.konfiguroGrideListeMadhe(gvLupaShtoArt, "IdArtikulliZevendesues");
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            var endlessScroll = clsAlternativaKushti.getAlternativa(idKonfigambjenti, "ES") == "Po";
            GridUtil.KonfiguroGrideListeMadhePopupiPaTheme(gvLupaShtoArt, "IdArtikulliZevendesues", kerkosaposhkruar, endlessScroll);
            gvLupaShtoArt.Settings.UseFixedTableLayout = false;
            percaktoTemplateArtikujsh();
        }

        private void percaktoTemplateArtikujsh()
        {//percaktohen templatet per fushat e grides
            GridViewDataTextColumn col0 = gvLupaShtoArt.Columns["Fshi"] as GridViewDataTextColumn;
            col0.DataItemTemplate = new MyButtonTemplate("");
            col0.VisibleIndex = 0;
            GridViewDataTextColumn col1 = gvLupaShtoArt.Columns["KodArtikulli"] as GridViewDataTextColumn;
            col1.DataItemTemplate = new MyComboTemplate();
            GridViewDataTextColumn col2 = gvLupaShtoArt.Columns["PershkrimArtikulli"] as GridViewDataTextColumn;
            col2.DataItemTemplate = new MyReadOnlyMemoTemplate();
            GridViewDataTextColumn col3 = gvLupaShtoArt.Columns["Prioriteti"] as GridViewDataTextColumn;
            col3.DataItemTemplate = new MyComboTemplate();
        }

        protected void gvLupaShtoArt_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            gvLupaShtoArt.DataBind();
        }

        protected void gvLupaShtoArt_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {//kur grida ben callback te ruajme te dhenat
            int key = -1;

            if (e.Parameters.ToString() != "")
            {
                key = int.Parse(e.Parameters.ToString());
            }
            DbCore.DbInventari.colArtikujtZevendesues artikujt = new DbCore.DbInventari.colArtikujtZevendesues();
            DbCore.DbInventari.clsArtikullZevendesues artikulli;
            int rreshta = gvLupaShtoArt.VisibleRowCount + 1;
            string initVal = this.hfArtikulli.Text;
            string[] pars1 = initVal.Split(',');
            string initVal2 = this.hfEmertimiA.Text;
            string[] pars3 = initVal2.Split(',');
            string initVal3 = this.hfPrioritetiA.Text;
            string[] pars5 = initVal3.Split(',');

            string[] kodi = new string[pars1.Length];
            string[] emri = new string[pars1.Length];
            string[] prioriteti = new string[pars1.Length];
            if (initVal != "")//merren te dhenat e hiden fieldeve te trupave te fleteve kontabel nga javascipti
            {
                for (int i = 0; i < pars1.Length; i++)
                {
                    string[] pars2 = pars1[i].Split(':');
                    kodi[Convert.ToInt32(pars2[0])] = pars2[1];
                }
            }
            if (initVal2 != "")
            {
                for (int i = 0; i < pars3.Length; i++)
                {
                    string[] pars4 = pars3[i].Split(':');
                    emri[Convert.ToInt32(pars4[0])] = pars4[1];
                }
            }
            if (initVal3 != "")
            {
                for (int i = 0; i < pars5.Length; i++)
                {
                    string[] pars6 = pars5[i].Split(':');
                    prioriteti[Convert.ToInt32(pars6[0])] = pars6[1];
                }
            }

            for (int i = 0; i < rreshta - 1; i++)//krijohet kolectioni me trupat e fleteve kontabel e futura nga perdoruesi
            {
                artikulli = new DbCore.DbInventari.clsArtikullZevendesues();
                if (kodi[i] != null && kodi[i] != "null" && kodi[i] != "")
                {
                    artikulli.KodArtikulli = kodi[i];
                    artikulli.IdArtikulliZevend = DbCore.DbInventari.clsArtikulli.ktheIdArtikulli(kodi[i], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    //artikulli.IdArtikulliZevend = dbInventari.ktheArtikull(kodi[i], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session))[0].IdArtikulli;
                } if (emri[i] != null && emri[i] != "null" && emri[i] != "")
                    artikulli.PershkrimArtikulli = emri[i];
                if (prioriteti[i] != null && prioriteti[i] != "null" && prioriteti[i] != "")
                    artikulli.Prioriteti = prioriteti[i];

                artikujt.Add(artikulli);
            }

            if (key != -1)
                artikujt.RemoveAt(key);
            else
            {
                DbCore.DbInventari.clsArtikullZevendesues art = new DbCore.DbInventari.clsArtikullZevendesues();

                artikujt.Add(art);
            }
            if (artikujt.Count == 0)
            {
                DbCore.DbInventari.clsArtikullZevendesues art = new DbCore.DbInventari.clsArtikullZevendesues();
                artikujt.Add(art);
            }
            this.gvLupaShtoArt.DataSource = artikujt;
            this.gvLupaShtoArt.DataBind();
            percaktoTemplateArtikujsh();
        }

        protected void gvLupaShtoArt_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpNoRows"] = gvLupaShtoArt.VisibleRowCount;
        }

        protected void gvLupaShtoArt_DataBound(object sender, EventArgs e)
        {//shton butonin fshi
            if (this.gvLupaShtoArt.Columns["Fshi"] == null)
            {
                GridViewDataTextColumn fshi = new GridViewDataTextColumn();
                fshi.Caption = "Fshi";
                fshi.Width = 50;
                gvLupaShtoArt.Columns.Add(fshi);

                gvLupaShtoArt.KeyFieldName = "IdArtikulliZevendesues";
                gvLupaShtoArt.SettingsBehavior.AllowSelectByRowClick = false;
                gvLupaShtoArt.SettingsBehavior.AllowFocusedRow = true;
            }
        }

        protected void Apliko_ASPxButton_Click(object sender, EventArgs e)
        {
            //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            //filtra.mbushFiltraGridaSipasFiltraKodi(Filtri_ASPxTextBox.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaShtoArt", "LupaShtoArtikull.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            filtra.mbushFilterPerGrideSipasKodit(Filtri_ASPxTextBox.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
            //DbCore.DbAdmin.clsFiltraGrida filtra = dbAdmin.merrFiltraGridaSipasFiltraKodi(Filtri_ASPxTextBox.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            gvLupaShtoArt.FilterExpression = filtra.FiltraVlera;
            GridUtil.renditGriden(filtra.KoloneRenditje, gvLupaShtoArt);
            konfiguroPopupGride(idKonfigambjenti, false, true);
            this.Filtri_ASPxTextBox.Text = "";
        }

        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
                DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida();
                filtri.FiltraKodi = Kodi_ASPxTextBox.Text;
                filtri.FiltraShenime = Shenime_ASPxTextBox.Text;
                filtri.FiltraUniversal = false;// Universal_ASPxCheckBox.Checked;
                //DbCore.DbAdmin.clsGridaKoka koka = dbAdmin.merrGridaKokaByEmri("gvLupaShtoArt", "LupaShtoArtikull.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaShtoArt", "LupaShtoArtikull.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                filtri.GridaKokaId = koka.IdGridaKoka;
                filtri.FiltraVlera = gvLupaShtoArt.FilterExpression;
                filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("IdArtikulliZevendesues", gvLupaShtoArt);
                //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvLupaShtoArt.GetSortedColumns();
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
                //    filtri.KoloneRenditje = "IdArtikulliZevendesues";
                //    filtri.DrejtimRenditje = true;
                //}
                DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();
                //oPerdorues = (DbCore.DbAdmin.clsPerdorues)(CacheLayer.GlobalCacheManager.MySessionCache["oClsPerdoruesi"]);
                oPerdorues = DbCore.mySessionObjects.kthePerdorues(Session);
                filtri.IdPerdoruesi = oPerdorues.IdPerdorues;
                filtri.IdNdermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                filtri.IdStatusDok = 1;
                filtri.ruaj();
                Kodi_ASPxTextBox.Text = "";
                Shenime_ASPxTextBox.Text = "";
                //Universal_ASPxCheckBox.Text = "";
                popRuaj.ShowOnPageLoad = false;
            }
        }


        protected void Kodi_CustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;
            //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            //if (dbAdmin.ekzistonFilter(Kodi_ASPxTextBox.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session)))
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaShtoArt", "LupaShtoArtikull.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            if (DbCore.DbAdmin.clsFiltraGrida.ekzistonFilterSipasKoditPerGride(Kodi_ASPxTextBox.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka))
                args.IsValid = false;
            //dbAdmin.Dispose();
        }

        protected void gvLupaShtoArt_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {//krijon rreshat sipas modelit
            bool ugjet;
            if (e.RowType == DevExpress.Web.GridViewRowType.Data)
            {
                GridViewDataTextColumn col0 = ((ASPxGridView)sender).Columns["Fshi"] as GridViewDataTextColumn;
                GridViewDataTextColumn col1 = ((ASPxGridView)sender).Columns["KodArtikulli"] as GridViewDataTextColumn;
                GridViewDataTextColumn col2 = ((ASPxGridView)sender).Columns["PershkrimArtikulli"] as GridViewDataTextColumn;
                GridViewDataTextColumn col3 = ((ASPxGridView)sender).Columns["Prioriteti"] as GridViewDataTextColumn;
                ASPxButton btn0 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col0, "btn") as ASPxButton;
                ASPxComboBox cmb1 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col1, "cmbBox") as ASPxComboBox;
                ASPxMemo txt2 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col2, "txtBox") as ASPxMemo;
                ASPxComboBox cmb4 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col3, "cmbBox") as ASPxComboBox;
                ugjet = false;
                //vendosen client side eventet e kolonave
                if (btn0 != null)
                {
                    btn0.ClientInstanceName = "btnFshi" + e.VisibleIndex.ToString();
                    btn0.ClientSideEvents.Click = "function(s,e){FshiClicked(" + e.VisibleIndex.ToString() + ");}";
                }

                idNdermVit = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
                //oPerdorues = (DbCore.DbAdmin.clsPerdorues)(CacheLayer.GlobalCacheManager.MySessionCache["oClsPerdoruesi"]);
                oPerdorues = DbCore.mySessionObjects.kthePerdorues(Session);
                // DbCore.DbInventari.colArtikujt colArtikujt = new DbCore.DbInventari.colArtikujt();
                // colArtikujt.merrSipasArtikujAktivNdermarrjesAndAutorizime(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), oPerdorues.IdPerdorues);
                //DbCore.DbInventari.colArtikujt colArtikujt = dbInventari.merrArtikujAktivNdermarrjesAndAutorizime(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), oPerdorues.IdPerdorues);
                CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
                ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                DataTable dt = DbCore.DbInventari.colArtikujt.merrSipasArtikujNdermarrjesAndAutorizimePerLupeArtikulli(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), false, -1, false, false, rm.GetString("postStringTvsh"), false, false, "", "", -1, -1, -1, false);
                if (cmb1 != null)
                {
                    cmb1.DropDownButton.Visible = false;
                    cmb1.Buttons.Add(); cmb1.TextFormatString = "{0},{1}";
                    ListBoxColumn colprove = new ListBoxColumn();
                    colprove.FieldName = "KodArtikulli";
                    cmb1.Columns.Add(colprove);
                    colprove = new ListBoxColumn();
                    colprove.FieldName = "PershkrimArtikulli";
                    cmb1.Columns.Add(colprove);
                    cmb1.DataSource = dt;// colArtikujt;
                    cmb1.DataBind();
                    cmb1.ClientInstanceName = "txtKod" + e.VisibleIndex.ToString();
                    cmb1.ClientSideEvents.TextChanged = "function(s,e){TextChanged(txtKod" + e.VisibleIndex.ToString() + ", txtEmertimi" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + ");}";
                    cmb1.ClientSideEvents.KeyPress = "function(s,e){var code = _getKeyCode(e.htmlEvent); KeyPress(code,txtKod" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + "," + idKonfigambjenti + "); }";
                    cmb1.ClientSideEvents.LostFocus = "function(s,e){LostFocus(txtKod" + e.VisibleIndex.ToString() + ", txtEmertimi" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + ");}";
                    cmb1.ClientSideEvents.ButtonClick = "function(s,e){ButtonClicked(txtKod" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + "," + idKonfigambjenti + "); }";
                    cmb1.ClientSideEvents.GotFocus = "function(s,e){GotFocus(txtKod" + e.VisibleIndex.ToString() + ", " + e.VisibleIndex.ToString() + ");}";
                    dt.Dispose();
                    if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 2)
                    {
                        if (ugjet)
                        {
                            tempcombo = cmb1;
                            ugjet = false;
                        }
                        //else if (koloneFocus == "KodArtikulli")
                        //{
                        //    ugjet = true;
                        //}
                    }
                }
                if (txt2 != null)
                {
                    txt2.ClientInstanceName = "txtEmertimi" + e.VisibleIndex.ToString();
                }

                if (cmb4 != null)
                {


                    cmb4.DataSource = mbushComboPrioriteti();

                    cmb4.DataBind();
                    if (cmb4.SelectedIndex == -1)
                        cmb4.SelectedIndex = 0;
                    cmb4.ClientInstanceName = "txtPrioritetiA" + e.VisibleIndex.ToString();
                    cmb4.ClientSideEvents.TextChanged = "function(s,e){TextChangedPrioritetiA(txtPrioritetiA" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + ");}";
                    if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 2)
                    {
                        if (ugjet)
                        {
                            tempcombo = cmb4;
                            ugjet = false;
                        }
                        //else if (koloneFocus == "Prioriteti")
                        //{
                        //    ugjet = true;
                        //}
                    }
                }
            }
            if (temptxt != null)
            {
                temptxt.Focus();
            }
        }

        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        }
        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        /// <param name="idViti"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNermarrje"></param>
        private void percaktoTemplateMenu(ASPxMenu aSPxMenu1, int idViti, int idPerdorues, int idNdermarrje)
        {
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, "LupaShtoArtikull.aspx", this, MenuInfo, true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            aSPxMenu1.Items.FindByName("Anullo").Text = rm.GetString("MenuItemMbyll", ci);

        }
    }
}
