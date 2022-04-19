using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using DevExpress.Web.ASPxTreeList;
using DbCore;
using System.Globalization;
using System.Resources;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.Messages;
using DbCore.DbKontabiliteti;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;

namespace PlatinumWeb
{
    public partial class LlogariShperndarjeQK : MyPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!DbCore.mySessionObjects.isLogedIn(Session))
                Response.Redirect(DbCore.IMBUtils.Paths.defaultLoginPath);
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
                Response.Redirect("Login_Ndermarrje.aspx?id=" + IdPerdoruesi);

            perktheLabel(ci, rm);
            percaktoTemplateMenu(ASPxMenu1);
            if (!IsPostBack)
            {
                konfiguroVleraFillestare();
                konfiguroGride();
            }
            else
                konfiguroVleraFillestareNgaSession();
        }

        /// <summary>
        /// Metode per te perkthyer label
        /// </summary>
        /// <param name="ci"></param>
        /// <param name="rm"></param>
        public void perktheLabel(CultureInfo ci, ResourceManager rm)
        {
            lblLlog.Text = MessagesResource.Messages["lblLlogariQeShperndahenNeQK"];
            lblLlogMund.Text = MessagesResource.Messages["lblLlogariTeMundshme"];
        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        /// <param name="idViti"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNdermarrje"></param>
        private void percaktoTemplateMenu(ASPxMenu aSPxMenu1)
        {
            clsToolbarConfig.percaktoTemplateMenu(IdGjuha, IdViti, IdPerdoruesi, IdNdermarrja, aSPxMenu1, "LlogariShperndarjeQK.aspx", this, MenuInfo, true, true, false, Meme);
        }

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(ASPxMenu1);
        }

        /// <summary>
        /// mbush tree listen me te dhena
        /// </summary>
        /// <param name="idNdermarrje"></param>
        private void konfiguroVleraFillestare()
        {
            DbCore.DbQendraKosto.colLlogariShperndarjeQK colllog = new DbCore.DbQendraKosto.colLlogariShperndarjeQK(IdNdermarrja);
            DbCore.mySessionObjects.ruajLlogariteQKNeSession(Session, colllog);
            trlStruktura.DataSource = colllog;
            trlStruktura.DataBind();

            DbCore.DbKontabiliteti.colKPFte col = new DbCore.DbKontabiliteti.colKPFte();
            col.ktheGjitheKPFteSipasGrupitPozitiveAndAutorizimeNiveli1Dhe2(1, IdNdermarrja, IdPerdoruesi);
            DbCore.mySessionObjects.ruajKPFQKNeSession(Session, col);
            trlStruktura2.DataSource = col;
            trlStruktura2.DataBind();
        }

        private void konfiguroVleraFillestareNgaSession()
        {
            DbCore.DbQendraKosto.colLlogariShperndarjeQK colllog = DbCore.mySessionObjects.merrLlogariteQKNgaSesioni(Session);
            trlStruktura.DataSource = colllog;
            trlStruktura.DataBind();

            DbCore.DbKontabiliteti.colKPFte col2 = DbCore.mySessionObjects.merrKPFQKNgaSesioni(Session);
            trlStruktura2.DataSource = col2;
            trlStruktura2.DataBind();
        }
        
        /// <summary>
        /// konfiguron listen
        /// </summary>
        /// <param name="idNdermarrje"></param>
        private void konfiguroGride()
        {
            TreeListUtil.percaktoVisibleColumns(DbCore.mySessionObjects.ktheGjuhe(Session), IdNdermarrja, trlStruktura, "trlStruktura", "LlogariShperndarjeQK.aspx");
            TreeListUtil.konfiguroTreeListeEvogelPaTheme(trlStruktura, "EmertimiKPF", "Prindi", true);
            TreeListUtil.percaktoVisibleColumns(DbCore.mySessionObjects.ktheGjuhe(Session), IdNdermarrja, trlStruktura2, "trlStruktura2", "LlogariShperndarjeQK.aspx");
            TreeListUtil.konfiguroTreeListeEvogelPaTheme(trlStruktura2, "EmertimiKPF", "Prind", true);
            konfiguroGrideSettings();
        }

        private void konfiguroGrideSettings()
        {
            trlStruktura.SettingsSelection.Enabled = false;
            trlStruktura.SettingsBehavior.AutoExpandAllNodes = false;
            trlStruktura.SettingsBehavior.AllowDragDrop = false;
            trlStruktura.SettingsEditing.AllowNodeDragDrop = true;
            trlStruktura.SettingsBehavior.AllowFocusedNode = true;
            trlStruktura.SettingsBehavior.AllowSort = true;

            trlStruktura2.SettingsSelection.Enabled = false;
            trlStruktura2.SettingsBehavior.AutoExpandAllNodes = false;
            trlStruktura2.SettingsBehavior.AllowDragDrop = false;
            trlStruktura2.SettingsEditing.AllowNodeDragDrop = true;
            trlStruktura2.SettingsBehavior.AllowFocusedNode = true;
            trlStruktura2.SettingsBehavior.AllowSort = true;
        }

        /// <summary>
        /// veprimet e menuse nga server side
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {
            if (e.Item.Name == "Ruaj")
            {
                Page.Validate();
                ruajLlogaritePerShperndarje();
            }
        }

        private void ruajLlogaritePerShperndarje()
        {
            if (Page.IsValid == false)
                return;

            DbCore.DbQendraKosto.colLlogariShperndarjeQK col = new DbCore.DbQendraKosto.colLlogariShperndarjeQK();
            try
            {
                col = DbCore.mySessionObjects.merrLlogariteQKNgaSesioni(Session);
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex.Message);
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, ex.Message, pnlMesazhi, LoadingPanel);
                return;
            }
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdNdermarrjeVit, "LlogariShperndarjeQK.aspx");
            if (!tedrejtaInfo.DShtim)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                return;
            }
            clsMesazh mesazh = col.ruaj(IdNdermarrja);
            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi, LoadingPanel);
                return;
            }
            clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
        }

        protected void btnDjathtas1_Click(object sender, EventArgs e)
        {
            if (trlStruktura.FocusedNode == null)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgJuLutemZgjidhniNjeLlogari"], pnlMesazhi);
                return;
            }

            colKPFte colKPFte = mySessionObjects.merrKPFQKNgaSesioni(Session);
            DbCore.DbQendraKosto.colLlogariShperndarjeQK colllog = DbCore.mySessionObjects.merrLlogariteQKNgaSesioni(Session);
            
            TreeListNode node = trlStruktura.FocusedNode;
            string nodeEmertimi = node.Key;
            int nodeIDKPF = ((DbCore.DbQendraKosto.clsLlogariShperndarjeQK)node.DataItem).IdKPF;

            if (DbCore.DbQendraKosto.clsLlogariShperndarjeQK.kaVeprime(nodeIDKPF, IdNdermarrja))
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgKjoLlogariEshtePerdorurNeVepNukMundTeHiqet"], pnlMesazhi);
                return;
            }

            if (trlStruktura.FocusedNode.Level == 2)
            {
                string prindi = node.ParentNode.Key;
                if (!colKPFte.Any(x => x.EmertimiKPF == prindi))
                {
                    clsKPF pr = new clsKPF(((DbCore.DbQendraKosto.clsLlogariShperndarjeQK)node.ParentNode.DataItem).IdKPF);
                    colKPFte.Add(pr);
                }
                if (node.ParentNode.ChildNodes.Count == 1)
                    colllog.Remove(colllog.Find(a => a.EmertimiKPF == node.ParentNode.Key));
                colllog.Remove(colllog.Find(a => a.EmertimiKPF == nodeEmertimi));

                clsKPF b = new clsKPF(nodeIDKPF);
                b.Prind = prindi;
                colKPFte.Add(b);
            }
            else
            {
                if (!colKPFte.Any(x => x.EmertimiKPF == nodeEmertimi))
                {
                    clsKPF pr = new clsKPF(nodeIDKPF);
                    colKPFte.Add(pr);
                }
                foreach (TreeListNode bij in node.ChildNodes)
                {
                    if (!colKPFte.Any(x => x.EmertimiKPF == bij.Key))
                    {
                        clsKPF b = new clsKPF(((DbCore.DbQendraKosto.clsLlogariShperndarjeQK)bij.DataItem).IdKPF);
                        b.Prind = nodeEmertimi;
                        colKPFte.Add(b);
                    }
                    colllog.Remove(colllog.Find(a => a.EmertimiKPF == bij.Key));
                }
                colllog.Remove(colllog.Find(a => a.EmertimiKPF == nodeEmertimi));
            }
            
            DbCore.mySessionObjects.ruajLlogariteQKNeSession(Session, colllog);
            trlStruktura.DataSource = colllog;
            trlStruktura.DataBind();
            
            DbCore.mySessionObjects.ruajKPFQKNeSession(Session, colKPFte);
            trlStruktura2.DataSource = colKPFte;
            trlStruktura2.DataBind();            
        }
        
        protected void btnMajtas1_Click(object sender, EventArgs e)
        {
            if (trlStruktura2.FocusedNode == null)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgJuLutemZgjidhniNjeLlogari"], pnlMesazhi);
                return;
            }

            colKPFte colKPFte = mySessionObjects.merrKPFQKNgaSesioni(Session);
            DbCore.DbQendraKosto.colLlogariShperndarjeQK colllog = DbCore.mySessionObjects.merrLlogariteQKNgaSesioni(Session);

            TreeListNode node = trlStruktura2.FocusedNode;
            int nodeIDKPF = ((DbCore.DbKontabiliteti.clsKPF)node.DataItem).IdKPF;
            string nodeEmertimi = node.Key;
            string prindi = "";
            if (node.Level == 2)
            {
                prindi = node.ParentNode.Key;
                if (colKPFte.Count(x => x.Prind == prindi)==1)
                    colKPFte.RemoveAll(x => x.EmertimiKPF == prindi);
                if (!colllog.Any(x => x.EmertimiKPF == prindi))
                {
                    int prindiikpf = ((DbCore.DbKontabiliteti.clsKPF)node.ParentNode.DataItem).IdKPF;
                    DbCore.DbQendraKosto.clsLlogariShperndarjeQK pr = new DbCore.DbQendraKosto.clsLlogariShperndarjeQK(0, prindiikpf, IdPerdoruesi, IdNdermarrja, prindi, "");
                    colllog.Add(pr);
                }
            }
            else
            {
                foreach (TreeListNode bij in trlStruktura2.FocusedNode.ChildNodes)
                {
                    DbCore.DbQendraKosto.clsLlogariShperndarjeQK clsbij = new DbCore.DbQendraKosto.clsLlogariShperndarjeQK(0, ((DbCore.DbKontabiliteti.clsKPF)bij.DataItem).IdKPF, IdPerdoruesi, IdNdermarrja, bij.Key, nodeEmertimi);
                    if (!colllog.Exists(a => a.EmertimiKPF == clsbij.EmertimiKPF))
                        colllog.Add(clsbij);
                    colKPFte.RemoveAll(x => x.IdKPF == clsbij.IdKPF);
                }
            }

            DbCore.DbQendraKosto.clsLlogariShperndarjeQK cls = new DbCore.DbQendraKosto.clsLlogariShperndarjeQK(0, nodeIDKPF, IdPerdoruesi, IdNdermarrja, nodeEmertimi, prindi);
            if (!colllog.Exists(a => a.EmertimiKPF == cls.EmertimiKPF)) 
                colllog.Add(cls);
            colKPFte.RemoveAll(x => x.IdKPF == nodeIDKPF);
                        
            DbCore.mySessionObjects.ruajLlogariteQKNeSession(Session, colllog);
            trlStruktura.DataSource = colllog;
            trlStruktura.DataBind();
            
            DbCore.mySessionObjects.ruajKPFQKNeSession(Session, colKPFte);
            trlStruktura2.DataSource = colKPFte;
            trlStruktura2.DataBind();
        }
        
    }
}