using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DbCore;
using DevExpress.Web;
using System.Globalization;
using System.Resources;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class KonfigurimeQK : MyPageBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            perktheLabel(cultinf, rm);
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                Response.Redirect(DbCore.IMBUtils.Paths.defaultLoginPath);
                return;
            }
            int idperdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idperdoruesi);
                return;
            }
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
            if (!IsPostBack)
            {
                mbushHiddenFieldMePerkthime();
                konfiguroVleraFillestare(idNdermarrje);
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idperdoruesi, idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "KonfigurimeQK.aspx");
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
            }

        }

        /// <summary>
        /// Metode qe sherben per te kaluar perkthime te js
        /// </summary>
        /// <param name="cultinf">Merr culture info</param>
        /// <param name="rm">Merr Resource Manager</param>
        private void mbushHiddenFieldMePerkthime()
        {
            hfState.Set("idGjuha",DbCore.mySessionObjects.ktheGjuhe(Session)); 
   
        }

        /// <summary>
        /// Metode per te perkthyer label
        /// </summary>
        /// <param name="ci"></param>
        /// <param name="rm"></param>
        public void perktheLabel(CultureInfo ci, ResourceManager rm)
        {
            lblMenyra.Text = rm.GetString("lblMenyraEShperndarjesNeQendraKosto", ci);
            lblPrioriteti1.Text = rm.GetString("lblPrioriteti1", ci);
            lblPrioriteti2.Text = rm.GetString("lblPrioriteti2", ci);
            lblPrioriteti3.Text = rm.GetString("lblPrioriteti3", ci);
            lblPrioriteti4.Text = rm.GetString("lblPrioriteti4", ci);
            lblPrioriteti5.Text = rm.GetString("lblPrioriteti5", ci);
            lblMesazhi.Text = rm.GetString("msgZgjidhniOpsioninPerShfaqjenEmsgNeQKRegjistrime", ci);
        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        /// <param name="idViti"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNdermarrje"></param>
        private void percaktoTemplateMenu(ASPxMenu aSPxMenu1, int idViti, int idPerdorues, int idNdermarrje)
        {
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, "KonfigurimeQK.aspx", this, MenuInfo, true, true, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
        }

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));

        }

        private void mbushrbMesazhi()
        {
            int idGjuha;
            idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            DbCore.DbQendraKosto.colMenyreMesazhi mes = new DbCore.DbQendraKosto.colMenyreMesazhi();
            mes.mbushGjitheMenyreMesazhi(idGjuha);
            rbMesazhi.DataSource = mes;
            rbMesazhi.TextField = "Menyra"; 
            rbMesazhi.ValueField = "IdMenyreMesazhi";
            rbMesazhi.DataBind();
        }
        /// <summary>
        /// mbush tree listen me te dhena
        /// </summary>
        /// <param name="idNdermarrje"></param>
        private void konfiguroVleraFillestare(int idNdermarrje)
        {
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            DbCore.DbQendraKosto.clsKonfigurimQK konf = new DbCore.DbQendraKosto.clsKonfigurimQK();
            konf.merrSipasIdNdermarje(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), mySessionObjects.ktheIdPerdoruesi(Session));
            ConfigureAspxComboBox.mbushComboPrioritetShperndarje(cmbPrioriteti1,ci,rm);
            ConfigureAspxComboBox.mbushComboPrioritetShperndarje(cmbPrioriteti2,ci,rm);
            ConfigureAspxComboBox.mbushComboPrioritetShperndarje(cmbPrioriteti3,ci,rm);
            ConfigureAspxComboBox.mbushComboPrioritetShperndarje(cmbPrioriteti4,ci,rm); ConfigureAspxComboBox.mbushComboPrioritetShperndarje(cmbPrioriteti5,ci,rm);
            if (konf.Prioriteti1 == 0)
                cmbPrioriteti1.SelectedIndex = 0;
            else
            cmbPrioriteti1.Value = konf.Prioriteti1.ToString();
            if (konf.Prioriteti2 == 0)
                cmbPrioriteti2.SelectedIndex = 1;
            else cmbPrioriteti2.Value = konf.Prioriteti2.ToString();
            if (konf.Prioriteti3== 0)
                cmbPrioriteti3.SelectedIndex = 2;
            else cmbPrioriteti3.Value = konf.Prioriteti3.ToString();
            if (konf.Prioriteti4 == 0)
                cmbPrioriteti4.SelectedIndex = 3;
            else cmbPrioriteti4.Value = konf.Prioriteti4.ToString();
            if (konf.Prioriteti5 == 0)
                cmbPrioriteti5.SelectedIndex = 4;
            else cmbPrioriteti5.Value = konf.Prioriteti5.ToString();
            mbushrbMesazhi();
            if (konf.IdMenyreMesazhi == 0)
                rbMesazhi.SelectedIndex = 2;
            else
            rbMesazhi.Value = konf.IdMenyreMesazhi.ToString();
            
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
                ruajKonfigurim();
            }
        }
        private void ruajKonfigurim()
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (Page.IsValid == false)
                return;

            clsMesazh mesazh;
            DbCore.DbQendraKosto.clsKonfigurimQK konf;
            try
            {
                konf = krijoKonfigurim();
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex.Message);
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, ex.Message, pnlMesazhi, LoadingPanel);
                return;
            }
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "KonfigurimeQK.aspx");
            if (!tedrejtaInfo.DShtim)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("mesazhRaportNukKeniTeDrejta", cultinf), pnlMesazhi);
                return;
            }
            DbCore.DbQendraKosto.clsLlogariShperndarjeQK llog = new DbCore.DbQendraKosto.clsLlogariShperndarjeQK();
            mesazh = konf.ruaj();
            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi, LoadingPanel);
                return;
            }

            clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);


        }

        private DbCore.DbQendraKosto.clsKonfigurimQK krijoKonfigurim()
        {
            DbCore.DbQendraKosto.clsKonfigurimQK konf = new DbCore.DbQendraKosto.clsKonfigurimQK(0, int.Parse(cmbPrioriteti1.Value.ToString()), int.Parse(cmbPrioriteti2.Value.ToString()), int.Parse(cmbPrioriteti3.Value.ToString()), int.Parse(cmbPrioriteti4.Value.ToString()), int.Parse(cmbPrioriteti5.Value.ToString()),int.Parse(rbMesazhi.Value.ToString()), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            return konf;
        }

    }
}