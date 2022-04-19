using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Drawing;
using AjaxControlToolkit;
using PlatinumWeb.Templates;
using System.Globalization;
using System.Resources;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    public partial class Modifiko_KushtPagese : MyPageBase
    {
        
        //private DbCore.DbKontabiliteti.clsKushtPageseKoka kokaOverview;
        private DbCore.DbKontabiliteti.colKushtPageseTrupi oColTrupiKushtPagese;
        DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();
        ASPxComboBox tempcombo = null;
        ASPxTextBox temptxt = null;
        TextBox temptxtNormal = null;
        private int nrRreshtash = 5;
        private string koloneFocus;

        protected void Page_Init(object sender, EventArgs e)
        {
            
            if (!Page.IsPostBack)
                konfiguroVleraFillestare();

            int id = int.Parse(Request.QueryString["id"]);

            //DbCore.DbKontabiliteti.colKushtPageseKoka colKoka = dbKontab.merrKushtPageseSipasID(id);
            DbCore.DbKontabiliteti.clsKushtPageseKoka clsKoka = new DbCore.DbKontabiliteti.clsKushtPageseKoka(id);
            if (clsKoka != null)
            {
                MerrTedhenat(clsKoka);
            }
        }

        public void MerrTedhenat(DbCore.DbKontabiliteti.clsKushtPageseKoka koka)
        {
            kodi_TextBox.Text = koka.KodiKushtPagese;
            kodi1_TextBox.Text = koka.KodiKushtPagese;
            emertimi_TextBox.Text = koka.EmertimiKushtPagese;
            emertimi1_TextBox.Text = koka.EmertimiKushtPagese;
            if (koka.LlojiKushtPagese == "E plote")
                pagesa_ASPxComboBox.SelectedIndex = 0;
            else if (koka.LlojiKushtPagese == "Me pjese")
                pagesa_ASPxComboBox.SelectedIndex = 1;
            if (koka.IdAutorizim != 0)
            {
                //DbCore.DbAdmin.clsDatabaseAdmin data = new DbCore.DbAdmin.clsDatabaseAdmin();
                txtAutorizimi.Text = DbCore.DbAdmin.clsAutorizimKoka.ktheKodAutorizim(koka.IdAutorizim);
                //txtAutorizimi.Text = data.ktheAutorizim(koka.IdAutorizim)[0].KodiAutorizim;
            }
            if (koka.Afati == -1)
                afati_TextBox.Text = "";
            else afati_TextBox.Text = koka.Afati.ToString();
            ndarja_ASPxComboBox.Text = koka.Ndarja;
            intervali_ASPxComboBox.Text = koka.IntervaliMidisNdarjeve;
            if (koka.NumriNdarjeve == -1)
                nrNdarjeve_TextBox.Text = "";
            else nrNdarjeve_TextBox.Text = koka.NumriNdarjeve.ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //if (CacheLayer.GlobalCacheManager.MySessionCache["LoggedIn"].Equals("No"))
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                DbCore.clsFunksione.logout(Session,true,"FaqePaautorizuar");
            }
            int idPerd = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            //if (CacheLayer.GlobalCacheManager.MySessionCache["KodiNdermarrjes"] == null)
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerd);
            }

            //oPerdorues = (DbCore.DbAdmin.clsPerdorues)(CacheLayer.GlobalCacheManager.MySessionCache["oClsPerdoruesi"]);
            oPerdorues = DbCore.mySessionObjects.kthePerdorues(Session);

            Container.Attributes["width"] = "450px";
            Container.Attributes["height"] = "450px";

            if (!IsPostBack)
            {
                EmrateTabeve();
                konfiguroGrideTrupi();
            }
            percaktoTemplate();
        }

        /// <summary>
        /// Vendos emrat e tabeve ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateTabeve()
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            ASPxPageControl1.TabPages[0].Text = rm.GetString("kushtPageseTab", cultinf);
            ASPxPageControl1.TabPages[1].Text = rm.GetString("trupiTab", cultinf);
        }

        private void konfiguroVleraFillestare()
        {
            ASPxPageControl1.ActiveTabIndex = 0;
            ConfigureAspxComboBox.mbushComboAutorizime(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), txtAutorizimi);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(txtAutorizimi);
            pagesa_ASPxComboBox.Items.Add("E plote", true);
            pagesa_ASPxComboBox.Items.Add("Me pjese", false);

            ndarja_ASPxComboBox.Items.Add("Perqindje", true);
            ndarja_ASPxComboBox.Items.Add("Interval", false);

            intervali_ASPxComboBox.Items.Add("Dite", 1);
            intervali_ASPxComboBox.Items.Add("Jave", 2);
            intervali_ASPxComboBox.Items.Add("Muaj", 3);

            InitializeGridTrupi();
        }

        private void mbushTrupin()
        {
            String trupiFillestar = "";

            
            int id = int.Parse(Request.QueryString["id"]);

            //DbCore.DbKontabiliteti.colKushtPageseTrupi colTrupi = dbKontab.merrTrupatKushtevePagesesSipasKokes(id);
            DbCore.DbKontabiliteti.colKushtPageseTrupi colTrupi = new DbCore.DbKontabiliteti.colKushtPageseTrupi(id);
            string kodiKushtPagese;
            foreach (DbCore.DbKontabiliteti.clsKushtPageseTrupi tr in colTrupi)
            {
                //DbCore.DbKontabiliteti.colKushtPageseKoka col = dbKontab.merrKushtPageseSipasID(tr.KushtPagese);
                DbCore.DbKontabiliteti.clsKushtPageseKoka clsKusht = new DbCore.DbKontabiliteti.clsKushtPageseKoka(tr.KushtPagese);
                if (clsKusht != null)
                    kodiKushtPagese = clsKusht.KodiKushtPagese;
                else kodiKushtPagese = " ";
                trupiFillestar += tr.Intervali + ":" + tr.Periudha + ":" + tr.Dite + ":" + tr.Zbritje + ":" + kodiKushtPagese + ";";
            }
            hfTrupiFillimit.Value = trupiFillestar;

            if (colTrupi.Count >= 5)
            {
                DbCore.DbKontabiliteti.clsKushtPageseTrupi t = new DbCore.DbKontabiliteti.clsKushtPageseTrupi();
                colTrupi.Add(t);
            }
            else
            {
                for (int i = colTrupi.Count; i < 5; i++)
                {
                    DbCore.DbKontabiliteti.clsKushtPageseTrupi t = new DbCore.DbKontabiliteti.clsKushtPageseTrupi();
                    colTrupi.Add(t);
                }
            }
            grid_trupi.DataSource = colTrupi;
            grid_trupi.DataBind();
        }

        private void konfiguroGrideTrupi()
        {
            shtoKolone("Intervali");
            shtoKolone("Periudha");
            shtoKolone("KushtPagese");
            GridUtil.percaktoVisibleColumnsMeWidth(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), grid_trupi, "grid_KushtPagesetrupi", "Shto_KushtPagese.aspx");
            //funk.percaktoAtributeTeGridesShto(grid_trupi, "IdTrupi");
            GridUtil.percaktoAtributeTeGridesShtoPaTheme(grid_trupi, "IdTrupi");

            mbushTrupin();
        }

        private void shtoKolone(String emri)
        {
            grid_trupi.Columns.Remove(grid_trupi.Columns[emri]);
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            grid_trupi.Columns.Add(colnew);
            colnew.FieldName = emri;
        }

        private void InitializeGridTrupi()
        {
            oColTrupiKushtPagese = new DbCore.DbKontabiliteti.colKushtPageseTrupi();
            DbCore.DbKontabiliteti.clsKushtPageseTrupi oTrupi = new DbCore.DbKontabiliteti.clsKushtPageseTrupi();
            for (int i = 0; i < 5; i++)
            {
                oTrupi = new DbCore.DbKontabiliteti.clsKushtPageseTrupi();
                oColTrupiKushtPagese.Add(oTrupi);
            }
            grid_trupi.DataSource = oColTrupiKushtPagese;
            grid_trupi.DataBind();
        }

        private void percaktoTemplate()
        {
            GridViewDataComboBoxColumn col1 = grid_trupi.Columns["Intervali"] as GridViewDataComboBoxColumn;
            col1.DataItemTemplate = new MyComboTemplate();
            GridViewDataComboBoxColumn col2 = grid_trupi.Columns["Periudha"] as GridViewDataComboBoxColumn;
            col2.DataItemTemplate = new MyComboTemplate();
            GridViewDataTextColumn col3 = grid_trupi.Columns["Dite"] as GridViewDataTextColumn;
            col3.DataItemTemplate = new MyTextTemplate();
            GridViewDataTextColumn col4 = grid_trupi.Columns["Zbritje"] as GridViewDataTextColumn;
            col4.DataItemTemplate = new MyTextTemplate();
            GridViewDataComboBoxColumn col5 = grid_trupi.Columns["KushtPagese"] as GridViewDataComboBoxColumn;
            col5.DataItemTemplate = new MyTemplateKushtePagese();
        }

        protected void grid_trupi_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            grid_trupi.DataBind();
        }

        protected void grid_trupi_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            bool ugjet;
            if (e.RowType == DevExpress.Web.GridViewRowType.Data)
            {
                GridViewDataComboBoxColumn col1 = ((ASPxGridView)sender).Columns["Intervali"] as GridViewDataComboBoxColumn;
                GridViewDataComboBoxColumn col2 = ((ASPxGridView)sender).Columns["Periudha"] as GridViewDataComboBoxColumn;
                GridViewDataColumn col3 = ((ASPxGridView)sender).Columns["Dite"] as GridViewDataColumn;
                GridViewDataColumn col4 = ((ASPxGridView)sender).Columns["Zbritje"] as GridViewDataColumn;
                GridViewDataComboBoxColumn col5 = ((ASPxGridView)sender).Columns["KushtPagese"] as GridViewDataComboBoxColumn; ;


                ASPxComboBox cmb1 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col1, "cmbBox") as ASPxComboBox;
                ASPxComboBox cmb2 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col2, "cmbBox") as ASPxComboBox;
                ASPxTextBox txt1 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col3, "txtBox") as ASPxTextBox;
                ASPxTextBox txt2 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col4, "txtBox") as ASPxTextBox;
                TextBox txt4 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col5, "txt") as TextBox;
                ASPxButton btn1 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col5, "btn") as ASPxButton;


                ugjet = false;

                if (cmb1 != null)
                {
                    cmb1.ClientInstanceName = "cmbIntervali" + e.VisibleIndex.ToString();
                    cmb1.Items.Add("Dite", 0);
                    cmb1.Items.Add("Jave", 1);
                    cmb1.Items.Add("Muaj", 2);

                    cmb1.ClientSideEvents.TextChanged = "function(s,e){TextChangedIntervali(cmbIntervali" + e.VisibleIndex.ToString() + ",'Intervali'," + e.VisibleIndex.ToString() + ");}";
                    if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 2)
                    {
                        if (ugjet)
                        {
                            tempcombo = cmb1;
                            ugjet = false;
                        }
                        else if (koloneFocus == "Intervali")
                        {
                            ugjet = true;
                        }
                    }
                }

                if (cmb2 != null)
                {
                    cmb2.ClientInstanceName = "cmbPeriudha" + e.VisibleIndex.ToString();
                    cmb2.ClientSideEvents.TextChanged = "function(s,e){TextChangedPeriudha(cmbPeriudha" + e.VisibleIndex.ToString() + ",'Periudha'," + e.VisibleIndex.ToString() + ");}";
                    if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 2)
                    {
                        if (ugjet)
                        {
                            tempcombo = cmb2;
                            ugjet = false;
                        }
                        else if (koloneFocus == "Periudha")
                        {
                            ugjet = true;
                        }
                    }
                }

                if (txt1 != null)
                {
                    txt1.ClientInstanceName = "txtDite" + e.VisibleIndex.ToString();
                    txt1.ClientSideEvents.LostFocus = "function(s,e){LostFocusDite(txtDite" + e.VisibleIndex.ToString() + ",'txtDite', " + e.VisibleIndex.ToString() + ");}";
                    if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 2)
                    {
                        if (ugjet)
                        {
                            temptxt = txt1;
                            ugjet = false;
                        }
                        else if (koloneFocus == "txtDite")
                        {
                            ugjet = true;
                        }
                    }
                }

                if (txt2 != null)
                {
                    txt2.ClientInstanceName = "txtZbritje" + e.VisibleIndex.ToString();
                    txt2.ClientSideEvents.LostFocus = "function(s,e){LostFocusZbritje(txtZbritje" + e.VisibleIndex.ToString() + ",'txtZbritje', " + e.VisibleIndex.ToString() + ");}";
                    if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 2)
                    {
                        if (ugjet)
                        {
                            temptxt = txt2;
                            ugjet = false;
                        }
                        else if (koloneFocus == "txtZbritje")
                        {
                            ugjet = true;
                        }
                    }
                }

                if (txt4 != null)
                {
                    txt4.ID = "KushtPagese" + e.VisibleIndex.ToString();
                    btn1.ID = "ButonKushtPagese" + e.VisibleIndex.ToString();

                    AutoCompleteExtender auComplete = new AutoCompleteExtender();
                    
                    auComplete.ServiceMethod = "ktheKushtPagese";
                    auComplete.MinimumPrefixLength = 1;
                    auComplete.EnableCaching = false;
                    auComplete.Enabled = true;
                    auComplete.FirstRowSelected = true;
                    auComplete.TargetControlID = txt4.ID;
                    auComplete.CompletionInterval = 100;
                    auComplete.CompletionListItemCssClass = "AutoCompleteExtender_CompletionListItem";
                    auComplete.CompletionListHighlightedItemCssClass = "AutoCompleteExtender_HighlightedItem";
                    auComplete.CompletionListCssClass = "AutoCompleteExtender_CompletionList";
                    txt4.NamingContainer.Controls.Add(auComplete);
                    txt4.Attributes["onchange"] = "javascript: TextChangedKushtPagese('" + txt4.ClientID + "'," + e.VisibleIndex.ToString() + ");";
                    txt4.Attributes["onkeypress"] = "javascript: KeyPress('" + txt4.ClientID + "'," + e.VisibleIndex.ToString() + ");";
                    txt4.Attributes["onblur"] = "javascript: LostFocusKushtPagese('" + txt4.ClientID + "'," + e.VisibleIndex.ToString() + ");";
                    btn1.ClientSideEvents.Click = "function(s,e){ButtonClickKushtPagese('" + txt4.ClientID + "'," + e.VisibleIndex.ToString() + ");}";
                    if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 1 - nrRreshtash)
                    {
                        if (ugjet)
                        {
                            temptxtNormal = txt4;
                            ugjet = false;
                        }
                        else if (koloneFocus == "KushtPagese")
                        {
                            ugjet = true;
                        }
                    }
                }
            }

            if (tempcombo != null)
            {
                tempcombo.Focus();
            }
            else if (temptxt != null)
            {
                temptxt.Focus();
            }
        }

        protected void formoColKushtePagese(String vlerat)
        {
            oColTrupiKushtPagese = new DbCore.DbKontabiliteti.colKushtPageseTrupi();
            string[] arrKushtePagese = vlerat.ToString().Split(';');

            string[] arrIntervali = arrKushtePagese[0].Split(',');
            for (int i = 0; i < arrIntervali.Length; i++)
            {
                string[] arrCompIntervali = arrIntervali[i].Split(':');
                if (arrCompIntervali.Length != 1)
                {
                    if (Convert.ToInt32(arrCompIntervali[0]) < oColTrupiKushtPagese.Count())
                    {
                        oColTrupiKushtPagese[Convert.ToInt32(arrCompIntervali[0])].Intervali = arrCompIntervali[1];
                    }
                    else
                    {
                        DbCore.DbKontabiliteti.clsKushtPageseTrupi oTrupi = new DbCore.DbKontabiliteti.clsKushtPageseTrupi();
                        oTrupi.Intervali = arrCompIntervali[1];
                        oColTrupiKushtPagese.Add(oTrupi);
                    }
                }
            }

            string[] arrPeriudha = arrKushtePagese[1].Split(',');
            for (int i = 0; i < arrPeriudha.Length; i++)
            {
                string[] arrCompPeriudha = arrPeriudha[i].Split(':');
                if (arrCompPeriudha.Length != 1)
                {
                    if (!(arrCompPeriudha[1].ToString() == " "))
                    {
                        if (Convert.ToInt32(arrCompPeriudha[0]) < oColTrupiKushtPagese.Count())
                        {
                            oColTrupiKushtPagese[Convert.ToInt32(arrCompPeriudha[0])].Periudha = arrCompPeriudha[1].ToString();
                        }
                        else
                        {
                            DbCore.DbKontabiliteti.clsKushtPageseTrupi oTrupi = new DbCore.DbKontabiliteti.clsKushtPageseTrupi();
                            oTrupi.Periudha = arrCompPeriudha[1].ToString();
                            oColTrupiKushtPagese.Add(oTrupi);
                        }
                    }
                }
            }

            string[] arrDite = arrKushtePagese[2].Split(',');
            for (int i = 0; i < arrDite.Length; i++)
            {
                string[] arrCompDite = arrDite[i].Split(':');
                if (arrCompDite.Length != 1)
                {
                    if (!(arrCompDite[1].ToString() == " "))
                    {
                        if (Convert.ToInt32(arrCompDite[0]) < oColTrupiKushtPagese.Count())
                        {
                            oColTrupiKushtPagese[Convert.ToInt32(arrCompDite[0])].Dite = Convert.ToInt32((arrCompDite[1].ToString()));
                        }
                        else
                        {
                            DbCore.DbKontabiliteti.clsKushtPageseTrupi oTrupi = new DbCore.DbKontabiliteti.clsKushtPageseTrupi();
                            oTrupi.Dite = Convert.ToInt32(arrCompDite[1].ToString());
                            oColTrupiKushtPagese.Add(oTrupi);
                        }
                    }
                }
            }

            string[] arrZbritje = arrKushtePagese[3].Split(',');
            for (int i = 0; i < arrZbritje.Length; i++)
            {
                string[] arrCompZbritje = arrZbritje[i].Split(':');
                if (arrCompZbritje.Length != 1)
                {
                    if (!(arrCompZbritje[1].ToString() == " "))
                    {
                        if (Convert.ToInt32(arrCompZbritje[0]) < oColTrupiKushtPagese.Count())
                        {
                            oColTrupiKushtPagese[Convert.ToInt32(arrCompZbritje[0])].Zbritje = Convert.ToInt32((arrCompZbritje[1].ToString()));
                        }
                        else
                        {
                            DbCore.DbKontabiliteti.clsKushtPageseTrupi oTrupi = new DbCore.DbKontabiliteti.clsKushtPageseTrupi();
                            oTrupi.Zbritje = Convert.ToInt32(arrCompZbritje[1].ToString());
                            oColTrupiKushtPagese.Add(oTrupi);
                        }
                    }
                }
            }

            string[] arrKushtPagese = arrKushtePagese[4].Split(',');
            for (int i = 0; i < arrKushtPagese.Length; i++)
            {
                string[] arrCompKushtPagese = arrKushtPagese[i].Split(':');
                if (arrCompKushtPagese.Length != 1)
                {
                    if (!(arrCompKushtPagese[1].ToString() == " "))
                    {
                        DbCore.DbKontabiliteti.clsKushtPageseKoka clsKoka = new DbCore.DbKontabiliteti.clsKushtPageseKoka(arrCompKushtPagese[1].ToString(), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                        //DbCore.DbKontabiliteti.clsDatabaseKontabilitet dbKontab = new DbCore.DbKontabiliteti.clsDatabaseKontabilitet();
                        //DbCore.DbKontabiliteti.colKushtPageseKoka col = dbKontab.merrKushtPageseSipasKodit(arrCompKushtPagese[1].ToString(), new DbCore.clsFunksione().ktheIdNdermarrje ());
                        if (clsKoka != null)
                        {
                            if (Convert.ToInt32(arrCompKushtPagese[0]) < oColTrupiKushtPagese.Count())
                            {
                                oColTrupiKushtPagese[Convert.ToInt32(arrCompKushtPagese[0])].KushtPagese = Convert.ToInt32(clsKoka.IdKoka);
                            }
                            else
                            {
                                DbCore.DbKontabiliteti.clsKushtPageseTrupi oTrupi = new DbCore.DbKontabiliteti.clsKushtPageseTrupi();
                                oTrupi.KushtPagese = Convert.ToInt32(clsKoka.IdKoka);
                                oColTrupiKushtPagese.Add(oTrupi);
                            }
                        }
                    }
                }
            }

            string[] focus = arrKushtePagese[5].Split(':');
            koloneFocus = focus[0];
            grid_trupi.DataSource = oColTrupiKushtPagese;
            grid_trupi.DataBind();
        }

        protected void grid_trupi_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            formoColKushtePagese(e.Parameters.ToString());
        }

        protected void ruajKushtPagese()
        {
            DbCore.DbKontabiliteti.clsKushtPageseKoka koka;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            if (Page.IsValid == false)
            {
                return;
            }
            else
            {
                koka = krijoKushtPagese();
                mesazh = koka.modifiko();
                if (mesazh.Status == true)
                {
                    pergjigja.Text = MessagesResource.Messages["mesazhRuajtjeMeSukses"];
                    pergjigja.ForeColor = Color.Green;
                    Response.Redirect("~/KushtePagese.aspx?indexrow=" + Request.QueryString["indexrow"]);
                }
                else
                {
                    pergjigja.Text = mesazh.PershkrimMesazhi;
                    pergjigja.ForeColor = Color.Red;
                }

            }
        }

        private DbCore.DbKontabiliteti.clsKushtPageseKoka krijoKushtPagese()
        {
            DbCore.DbKontabiliteti.clsKushtPageseKoka koka = new DbCore.DbKontabiliteti.clsKushtPageseKoka();
            //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            koka.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            koka.IdStatusDok = 1;

            koka.IdKoka = int.Parse(Request.QueryString["id"]);
            koka.KodiKushtPagese = kodi_TextBox.Text;
            koka.EmertimiKushtPagese = emertimi_TextBox.Text;
            koka.LlojiKushtPagese = pagesa_ASPxComboBox.SelectedItem.Text.ToString();
            koka.IdAutorizim = DbCore.DbAdmin.clsAutorizimKoka.ktheIDAutorizim(txtAutorizimi.Text);
            //koka.IdAutorizim = dbAdmin.ktheAutorizim(txtAutorizimi.Text)[0].IdAutorizimKoka;
            if (koka.LlojiKushtPagese == "E plote")
            {
                if (afati_TextBox.Text != "")
                    koka.Afati = int.Parse(afati_TextBox.Text);
                else koka.Afati = 0;
                koka.Ndarja = "";
                koka.IntervaliMidisNdarjeve = "";
                koka.Numri = -1;
                koka.NumriNdarjeve = -1;
            }
            else if (koka.LlojiKushtPagese == "Me pjese")
            {
                koka.Afati = -1;
                koka.Ndarja = ndarja_ASPxComboBox.Text.ToString();
                if (koka.Ndarja == "Interval")
                {
                    if (intervali_ASPxComboBox.SelectedItem != null)
                        koka.IntervaliMidisNdarjeve = intervali_ASPxComboBox.SelectedItem.Text.ToString();
                    else koka.IntervaliMidisNdarjeve = "";
                    koka.Numri = 1;
                    if (nrNdarjeve_TextBox.Text != "")
                        koka.NumriNdarjeve = int.Parse(nrNdarjeve_TextBox.Text);
                    else koka.NumriNdarjeve = 0;
                }
                else if (koka.Ndarja == "Perqindje")
                {
                    koka.IntervaliMidisNdarjeve = "";
                    koka.Numri = -1;
                    koka.NumriNdarjeve = -1;
                }
                else if (koka.Ndarja == "")
                {
                    koka.IntervaliMidisNdarjeve = "";
                    koka.Numri = -1;
                    koka.NumriNdarjeve = -1;
                }
            }
            koka.IdNdermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            koka.OColTrupi = oColTrupiKushtPagese;
            return koka;
        }

        protected void pastro()
        {
            ASPxPageControl1.ActiveTabIndex = 0;
            pagesa_ASPxComboBox.Text = "";
            kodi_TextBox.Text = "";
            kodi1_TextBox.Text = "";
            emertimi_TextBox.Text = "";
            emertimi1_TextBox.Text = "";
            afati_TextBox.Text = "";
            ndarja_ASPxComboBox.Text = "";
            intervali_ASPxComboBox.Text = "";
            nrNdarjeve_TextBox.Text = "";
            HiddenField1.Value = "";
            InitializeGridTrupi();
        }

        protected void grid_trupi_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpNoRows"] = grid_trupi.VisibleRowCount;
        }

        void SetStyle(TableCell cell)
        {
            cell.Style[HtmlTextWriterStyle.TextOverflow] = "ellipsis";
            cell.Style[HtmlTextWriterStyle.Overflow] = "hidden";
            cell.Style[HtmlTextWriterStyle.WhiteSpace] = "nowrap";
        }

        protected void grid_trupi_HtmlFooterCellPrepared(object sender, ASPxGridViewTableFooterCellEventArgs e)
        {
            SetStyle(e.Cell);
        }




        protected void ruaj_Button_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                String vlerat = HiddenFieldTrupi.Value;
                formoColKushtePagese(vlerat);
                ruajKushtPagese();
            }
        }

        protected void pastro_Button_Click(object sender, EventArgs e)
        {
            pastro();
        }

        protected void anullo_Button_Click(object sender, EventArgs e)
        {
            Response.Redirect("KushtePagese.aspx");
        }
    }
}