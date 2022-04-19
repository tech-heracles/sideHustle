using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using DevExpress.Web;
using System.Collections;

namespace PlatinumWeb.E_PaySlip
{
    /// <summary>
    /// klasa permban metoda qe lidhen me konfigurimin e menuse kryesore ne faqe
    /// </summary>
    public class clsToolbarConfig
    {
        /// <summary>
        /// ne menune kryesore krijohet dhe shtohet nje item i perbere nga user control-i per levizjet lart, poshte, fillim, fund ne griden perkatese te faqes
        /// </summary>
        ///<param name="page"></param>
        /// <param name="m"></param>
        /// <param name="emriGrida"></param>
        public static void ShtoMenuItemLevizNeGride(Page page, ASPxMenu m, String emriGrida)
        {
            DevExpress.Web.MenuItem item = m.Items.FindByName("TemplatedItemLevizjet");
            m.Items.Remove(item);
            item = m.Items.Add("", "TemplatedItemLevizjet");
            Control itemTemplate = page.LoadControl("MenuLeviz.ascx");
            //itemTemplate.me
            item.BeginGroup = true;
            item.Template = itemTemplate as ITemplate;

            ASPxHyperLink lnkPara = ((PlatinumWeb.E_PaySlip.MenuLeviz)(item.Template)).FindControl("lnkPara") as ASPxHyperLink;
            ASPxHyperLink lnkPas = ((PlatinumWeb.E_PaySlip.MenuLeviz)(item.Template)).FindControl("lnkPas") as ASPxHyperLink;
            ASPxHyperLink lnkFillim = ((PlatinumWeb.E_PaySlip.MenuLeviz)(item.Template)).FindControl("lnkFillim") as ASPxHyperLink;
            ASPxHyperLink lnkFund = ((PlatinumWeb.E_PaySlip.MenuLeviz)(item.Template)).FindControl("lnkFund") as ASPxHyperLink;

            lnkPara.ClientSideEvents.Click = "function(s,e){ Lart_click(e," + emriGrida + ");}";
            lnkPas.ClientSideEvents.Click = "function(s,e){ Poshte_click(e," + emriGrida + ");}";
            lnkFillim.ClientSideEvents.Click = "function(s,e){ Fillim_click(e," + emriGrida + ");}";
            lnkFund.ClientSideEvents.Click = "function(s,e){ Fund_click(e," + emriGrida + ");}";
        }


        public static void ShtoMenuItemPerFilterRaport(Page page, ASPxMenu m, EventHandler handlerRuaj, EventHandler handlerFshi)
        {
            DevExpress.Web.MenuItem item = m.Items.FindByName("TemplatedItemFilter");
            if (item !=null)
            m.Items.Remove(item);
            item = m.Items.Add("", "TemplatedItemFilter");

            //PagedData.Header h = (PagedData.Header)Page.LoadControl("Header.ascx");
            Control itemTemplate = page.LoadControl("MenuFilter.ascx");

            item.Template = itemTemplate as ITemplate;

            ASPxButton btnRuaj = ((PlatinumWeb.E_PaySlip.MenuFilter)(item.Template)).FindControl("Button1") as ASPxButton;
            ASPxButton btnFshiFilter = ((MenuFilter)(item.Template)).FindControl("btnFshi") as ASPxButton;
            ASPxComboBox cmbFiltra = ((MenuFilter)(item.Template)).FindControl("btnFiltra") as ASPxComboBox;

            //i shtoj eventet kontrolleve te template qe te trajtohen ne faqen qe mban template-in
            btnRuaj.Click += handlerRuaj;
            btnFshiFilter.Click += handlerFshi;
            //cmbFiltra.SelectedIndexChanged += handlerApliko;            
            cmbFiltra.ClientInstanceName = "cmbfiltra";
            cmbFiltra.ClientSideEvents.SelectedIndexChanged = "function(s,e){ktheFiltra(cmbfiltra.GetValue());}";
            cmbFiltra.ClientSideEvents.KeyUp = "function(s,e){checkText(s,e,cmbfiltra);}";
            btnRuaj.ClientSideEvents.Click = "function(s,e){ RuajFilter_Click(s,e,cmbfiltra);}";
            btnFshiFilter.ClientSideEvents.Click = "function(s,e){FshiFilter_Clicked(s,e,cmbfiltra.GetValue());}";
            //if (!page.IsPostBack)
            // {
            btnRuaj.ClientEnabled = false;
            btnFshiFilter.ClientEnabled = false;
            // }
        }
        /// <summary>
        /// ne menune kryesore krijohet dhe shtohet nje item i perbere nga user control-i per te zgjedhur, shtuar dhe fshire nje filter te krijuar me pare per griden e faqes
        /// </summary>
        /// <param name="page"></param>
        /// <param name="m"></param>
        /// <param name="handlerRuaj"></param>
        /// <param name="handlerApliko"></param>
        /// <param name="handlerFshi"></param>
        public static void ShtoMenuItemPerFilter(Page page, ASPxMenu m, EventHandler handlerRuaj, EventHandler handlerFshi)
        {
            DevExpress.Web.MenuItem item = m.Items.FindByName("TemplatedItemFilter");
            m.Items.Remove(item);
            item = m.Items.Add("", "TemplatedItemFilter");

            //PagedData.Header h = (PagedData.Header)Page.LoadControl("Header.ascx");
            Control itemTemplate = page.LoadControl("MenuFilter.ascx");

            item.Template = itemTemplate as ITemplate;

            ASPxButton btnRuaj = ((PlatinumWeb.E_PaySlip.MenuFilter)(item.Template)).FindControl("Button1") as ASPxButton;
            ASPxButton btnFshiFilter = ((PlatinumWeb.E_PaySlip.MenuFilter)(item.Template)).FindControl("btnFshi") as ASPxButton;
            ASPxComboBox cmbFiltra = ((PlatinumWeb.E_PaySlip.MenuFilter)(item.Template)).FindControl("btnFiltra") as ASPxComboBox;
            cmbFiltra.ClientInstanceName = "cmbfiltra";
            cmbFiltra.ClientSideEvents.KeyUp = "function(s,e){checkText(s,e);}";
            cmbFiltra.ClientSideEvents.Init = "function(s,e){textChanged(s,e);}";
            cmbFiltra.ClientSideEvents.SelectedIndexChanged = "function(s,e){aplikoFiltra(s,e) ;}";
            //i shtoj eventet kontrolleve te template qe te trajtohen ne faqen qe mban template-in
            btnRuaj.Click += handlerRuaj;
            btnFshiFilter.Click += handlerFshi;
            btnRuaj.ClientEnabled = false;
            btnFshiFilter.ClientEnabled = false;

        }

        /// <summary>
        /// Shton ne menune kryesore nje item per exportin e raporteve te pivot grid-es
        /// </summary>
        /// <param name="page"></param>
        /// <param name="m"></param>
        /// <param name="clickHandler"></param>
        public static void ShtoMenuItemExporto(Page page, ASPxMenu m, EventHandler clickHandler, EventHandler preRenderHandler)
        {
            DevExpress.Web.MenuItem item = m.Items.FindByName("TemplatedItemExport");
            m.Items.Remove(item);
            item = m.Items.Add("", "TemplatedItemExport");

            Control itemTemplate = page.LoadControl("MenuExport.ascx");

            item.Template = itemTemplate as ITemplate;
            item.VisibleIndex = m.Items.Count - 2;
            ASPxButton btnExport = ((PlatinumWeb.E_PaySlip.MenuExport)(item.Template)).FindControl("exportButton") as ASPxButton;
            ASPxComboBox cmbExport = ((PlatinumWeb.E_PaySlip.MenuExport)(item.Template)).FindControl("cmbExport") as ASPxComboBox;
            cmbExport.ClientInstanceName = "cmbExport";
            btnExport.Click += clickHandler;
            btnExport.PreRender += preRenderHandler;
        }

        /// <summary>
        /// ne menyne kryesore krijohet dhe shtohet nje item i perbere nga dy butona qe jane siper njeri-tjetrit
        /// </summary>
        /// <param name="page"></param>
        /// <param name="m"></param>
        /// <param name="handlerRuaj"></param>
        /// <param name="handlerFshi"></param>
        public static void ShtoMenuItemPerFrame(Page page, ASPxMenu m, String helpUrl)
        {
            DevExpress.Web.MenuItem item = m.Items.FindByName("TemplatedItemFrame");
            m.Items.Remove(item);
            item = m.Items.Add("", "TemplatedItemFrame");

            //PagedData.Header h = (PagedData.Header)Page.LoadControl("Header.ascx");
            Control itemTemplate = page.LoadControl("MenuFrame.ascx");

            item.Template = itemTemplate as ITemplate;
            ASPxButton btnHelp = ((PlatinumWeb.E_PaySlip.MenuFrame)(item.Template)).FindControl("btnHelp") as ASPxButton;
            btnHelp.ClientSideEvents.Click = "function(s,e){openHelpWindow(s,e,\'" + helpUrl + "\');}";

            ASPxButton btnExpandAll = ((PlatinumWeb.E_PaySlip.MenuFrame)(item.Template)).FindControl("btnExpandAll") as ASPxButton;
            btnExpandAll.ClientSideEvents.Click = "function(s,e){expandAll(s,e);}";
            ASPxButton btnCollapseAll = ((PlatinumWeb.E_PaySlip.MenuFrame)(item.Template)).FindControl("btnCollapseAll") as ASPxButton;
            btnCollapseAll.ClientSideEvents.Click = "function(s,e){collapseAll(s,e);}";
        }

        ///// <summary>
        ///// ne menu shtohet nje item 
        ///// </summary>
        ///// <param name="m"></param>
        //public static void ShtoMenuItem(ASPxMenu m, DbCore.DbShare.clsMenuItem menuitem, bool enabled)
        //{
        //    DevExpress.Web.MenuItem item3 = m.Items.FindByName(menuitem.Name);
        //    m.Items.Remove(item3);
        //    item3 = m.Items.Add();
        //    item3.Name = menuitem.Name;
        //    item3.Text = menuitem.Text;
        //    item3.Image.Url = menuitem.ImageUrl;
        //    m.ItemStyle.Width = 500;
        //    item3.Enabled = enabled;
        //    if (menuitem.Name == "Ruaj" || menuitem.Name == "ShtoPunesim" || menuitem.Name == "RuajPunesim" || menuitem.Name == "FshiPunesim")
        //        item3.ClientVisible = false;

        //}
        /// <summary>
        /// ne menu shtohet nje item 
        /// </summary>
        /// <param name="pageTheme"></param>
        /// <param name="themeName"></param>
        /// <param name="m"></param>
        /// <param name="menuPerCRM">true ne rastin kur eshte menu per komponentet e CRM pasi do lihet vetem sipas theme Moderno</param>
        public static void ShtoMenuItem(string pageTheme, ASPxMenu m, DbCore.DbShare.clsMenuItem menuitem, bool menuPerCRM = false)
        {
            DevExpress.Web.MenuItem itemToAdd;
            if (menuitem.IdPrindi == 0)
            {
                itemToAdd = m.Items.FindByName(menuitem.Name);
                m.Items.Remove(itemToAdd);
                
                //if (menuitem.Enabled) {
                    itemToAdd = m.Items.Add();
                    itemToAdd.ClientEnabled = menuitem.Enabled;
                //}
                //else
                //    return;
            }
            else
            {
                itemToAdd = m.Items.FindByName(menuitem.Prind).Items.FindByName(menuitem.Name);
                m.Items.FindByName(menuitem.Prind).Items.Remove(itemToAdd);
                itemToAdd = m.Items.FindByName(menuitem.Prind).Items.Add();
                itemToAdd.ItemStyle.Width = m.Items.FindByName(menuitem.Prind).ItemStyle.Width;
            }
            itemToAdd.Name = menuitem.Name;
            itemToAdd.Text = menuitem.Text;
            string menuUrl = menuitem.ImageUrl;
            if (menuUrl != "")
            {
                if (menuUrl.IndexOf("~") == -1 || menuPerCRM)
                {
                    string imageURlPaTheme = menuitem.ImageUrl.Substring(0, menuitem.ImageUrl.IndexOf(".png"));
                    string themeMenuFolder = "~/images/theme/" + pageTheme + "/menu/";
                    if (menuPerCRM)
                    {
                        string emriItem = menuUrl.Substring(menuUrl.LastIndexOf('/') + 1);
                        itemToAdd.Image.Url = themeMenuFolder + menuitem.Name + ".png";
                        itemToAdd.Image.UrlHottracked = themeMenuFolder + menuitem.Name + "_W.png";
                    }
                    else
                    {
                        itemToAdd.Image.Url = themeMenuFolder + menuUrl;
                        itemToAdd.Image.UrlHottracked = themeMenuFolder + imageURlPaTheme + "_W.png";
                    }
                    //itemToAdd.Image.Width = 25;
                    //itemToAdd.Image.Height = 25;
                }
                else
                    itemToAdd.Image.Url = menuitem.ImageUrl;
            }
            m.ItemStyle.Width = 500;
            
            if (menuitem.Name == "Ruaj" || menuitem.Name == "ShtoPunesim" || menuitem.Name == "RuajPunesim" || menuitem.Name == "FshiPunesim")
                itemToAdd.ClientVisible = false;
        }

        ///// <summary>
        ///// ne menu shtohet nje sub item 
        ///// </summary>
        ///// <param name="m"></param>
        //public static void ShtoMenuSubItem(ASPxMenu m, DbCore.DbShare.clsMenuItem menuitem, DbCore.DbShare.clsMenuItem parentItem, bool enabled)
        //{
        //    DevExpress.Web.MenuItem item3 = m.Items.FindByName(parentItem.Name).Items.FindByName(menuitem.Name);
        //    m.Items.FindByName(parentItem.Name).Items.Remove(item3);
        //    item3 = m.Items.FindByName(parentItem.Name).Items.Add();
        //    item3.Name = menuitem.Name;
        //    item3.Text = menuitem.Text;
        //    item3.Image.Url = menuitem.ImageUrl;
        //    item3.Enabled = enabled;

        //}

        /// <summary>
        /// ne menu shtohet nje item bosh
        /// </summary>
        /// <param name="m"></param>
        public static void ShtoMenuItemBosh(ASPxMenu m)
        {
            DevExpress.Web.MenuItem item3 = m.Items.FindByName("Bosh");
            m.Items.Remove(item3);
            item3 = m.Items.Add("", "Bosh");
            item3.BeginGroup = true;
        }

        public static void mbushComboBoxFiltra(int idGjuha, int idNdermarrje, string emriGrides, int idkonfig, string emriKomponentes)
        {
            
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuha, emriGrides, emriKomponentes, idNdermarrje, idkonfig);
            DbCore.DbAdmin.colFiltratGrida colFiltra = new DbCore.DbAdmin.colFiltratGrida(koka.IdGridaKoka, idNdermarrje);
            colFiltra.Insert(0, new DbCore.DbAdmin.clsFiltraGrida());
            var model = new DbCore.MyMenuFilterModel
            {
                ValueField = "IdFiltra",
                TextField = "FiltraKodi",
                DataSource = colFiltra
            };
            DbCore.DbAdmin.clsFiltraGrida filtriDef = DbCore.clsFunksione.merrFilterDefault(idkonfig);
            if (filtriDef != null) model.ValueToSelect = filtriDef.FiltraKodi;
            DbCore.mySessionObjects.RuajNeSession(System.Web.HttpContext.Current.Session, model, $"filtraGride_{idkonfig}");
        }

        public static void percaktoTemplateMenu(int idgjuha, int idViti, int idPerdorues, int idNdermarrje, ASPxMenu aSPxMenu1, string emerkomponente, Page page, ASPxMenu MenuInfo, EventHandler Ruaj_ASPxButton_Click, EventHandler FshiFilter_ASPxButton_Click, EventHandler btnPo_Click, EventHandler btnJo_Click, bool shtim, bool shfaqRuaj, bool raport, bool meme, bool kubi, bool menuPerCRM = false)
        {
            //DbCore.DbShare.colMenuItem menu = new DbCore.DbShare.colMenuItem(idgjuha);
            //if (!kubi)
            //    menu.merrMenuItemSipasKomponentes(idgjuha, emerkomponente, idPerdorues, idNdermarrje, idViti, shtim);
            //else menu.merrMenuItemPerKubin(idgjuha, emerkomponente, idPerdorues, idNdermarrje, idViti, shtim);
            DbCore.DbShare.colMenuItem menu = merrMenuSipasKomponentes(idgjuha, kubi, emerkomponente, idPerdorues, idNdermarrje, aSPxMenu1, idViti, shtim);
            
            for (int i =0,menuItemCount = menu.Count; i < menuItemCount; i++)
            {
                DbCore.DbShare.clsMenuItem m = menu[i];
                //if (!m.Enabled) continue;
                if (m.Name != "ItemFilter" && m.Name != "ItemFrame" && m.Name != "ItemExport")
                {
                    ShtoMenuItem(page.Theme, aSPxMenu1, m, menuPerCRM);
                    if (m.Name == "Ruaj"&&shfaqRuaj)
                        aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = true; 
                    if (m.Name == "Sinkronizo"&&!meme)
                        aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                    if (m.Name == "Trasfero" && !meme)
                        aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                }
                else
                    if (m.Name == "ItemFilter")
                    {
                        //krijohen handler per te caktuar evente server side per kontrolle
                        //keto handler i kalohen si parametra user control per filtrat
                        EventHandler handlerPerRuajFilter = new EventHandler(Ruaj_ASPxButton_Click);
                        EventHandler handlerPerFshiFilter = new EventHandler(FshiFilter_ASPxButton_Click);

                        //shtohet ne menu user control per filtrat e grides
                        if(!raport)
                        clsToolbarConfig.ShtoMenuItemPerFilter(page, aSPxMenu1, handlerPerRuajFilter, handlerPerFshiFilter);  
                        else clsToolbarConfig.ShtoMenuItemPerFilterRaport(page, aSPxMenu1, handlerPerRuajFilter, handlerPerFshiFilter);
                    }
                    else
                    {
                        clsToolbarConfig.ShtoMenuItemPerFrame(page, aSPxMenu1, DbCore.clsFunksione.ktheUrlHelpi(m.UrlHelp).Item1);
                    }
                if (m.Name == "Shto" || m.Name == "Ndihme" || m.Name == "ItemFilter" || m.Name == "ItemFrame" || m.Name == "Grupo" || m.Name == "Kerko" || m.Name == "Pastro" || m.Name == "ItemExport")
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].BeginGroup = true;

                if (m.Name == "Arkiva")
                {
                    DbCore.DbAdmin.clsNdermarrje nd = new DbCore.DbAdmin.clsNdermarrje(idNdermarrje);
                    if (!nd.isArkiva) aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                }
            }
            if (btnJo_Click != null)
                clsMenuInfo.ShtoMenuItemInfo(page, MenuInfo, btnPo_Click, btnJo_Click);
            else clsMenuInfo.ShtoMenuItemInfo(page, MenuInfo);
        }

        /// <summary>
        /// merr menu item per menune
        /// </summary>
        /// <param name="idgjuha"></param>
        /// <param name="kubi"></param>
        /// <param name="emerkomponente"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="aSPxMenu1"></param>
        /// <param name="idViti"></param>
        /// <param name="shtim"></param>
        /// <returns>kthen nje collection te menuitem</returns>
        private static DbCore.DbShare.colMenuItem merrMenuSipasKomponentes(int idgjuha, bool kubi, string emerkomponente, int idPerdorues, int idNdermarrje, ASPxMenu aSPxMenu1, int idViti, bool shtim)
        {
            DbCore.DbShare.colMenuItem menu = new DbCore.DbShare.colMenuItem(idgjuha);
            if (idNdermarrje != 0)
            {
                if (!kubi)
                    menu.merrMenuItemSipasKomponentesRegjistrime(idgjuha, emerkomponente, idPerdorues, idNdermarrje, idViti, shtim);
                else 
                    menu.merrMenuItemPerKubin(idgjuha, emerkomponente, idPerdorues, idNdermarrje, idViti, shtim);
            }
            else  //rasti kur hapet ambjentii ndryshimit te fjalekalimit direkt pasi perdoruesi logohet(ndodh kur perdoruesi do ndryshoje pass sepse e ka te perkohshem ose per arsye se i ka skaduar)
            {
                DbCore.DbShare.clsMenuItem RuajMenuItem = new DbCore.DbShare.clsMenuItem(0, 3);
                DbCore.DbShare.clsMenuItem menuItem2 = new DbCore.DbShare.clsMenuItem(0, 27);
                menu.Add(RuajMenuItem);
                menu.Add(menuItem2);
            }
            return menu;
        }

        public static void percaktoTemplateMenu(int idgjuha, int idViti, int idPerdorues, int idNdermarrje, ASPxMenu aSPxMenu1, string emerkomponente, Page page, ASPxMenu MenuInfo, bool shtim, bool shfaqRuaj, bool raport, bool meme, bool menuPerCRM = false)
        {
            percaktoTemplateMenu(idgjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, emerkomponente, page, MenuInfo, null, null, null, null, shtim, shfaqRuaj, raport,meme, false, menuPerCRM);
        }

        
        public static void percaktoTemplateMenu(int idgjuha, int idViti, int idPerdorues, int idNdermarrje, ASPxMenu aSPxMenu1, string emerkomponente, Page page, ASPxMenu MenuInfo, EventHandler Ruaj_ASPxButton_Click, EventHandler FshiFilter_ASPxButton_Click, bool shtim, bool shfaqRuaj, bool raport, bool meme, bool menuPerCRM = false)
        {
            percaktoTemplateMenu(idgjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, emerkomponente, page, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, null, null, shtim, shfaqRuaj, raport, meme, false, menuPerCRM);
        }
    }
}