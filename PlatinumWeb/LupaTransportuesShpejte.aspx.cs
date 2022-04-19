using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DbCore.DbRegjistrim;
using DevExpress.Web;
using System.Globalization;
using System.Resources;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    public partial class LupaTransportuesShpejte : MyPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int idPerdoruesi;
            int idGjuha;
            int idNdermarrje;
            int idViti;
            int idNdermarrjeVit;
            if (hfState.Count == 0)
            {
                if (!DbCore.mySessionObjects.isLogedIn(Session))
                {
                    DbCore.clsFunksione.logout(Session,true,"FaqePaautorizuar");
                }
                idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
                idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
                idNdermarrjeVit = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
                hfState.Set("idPerdoruesi", idPerdoruesi);
                hfState.Set("idGjuha", idGjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idViti", idViti);
                hfState.Set("idNdermarrjeVit", idNdermarrjeVit);
                hfState.Set("Meme", DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
                hfState.Set("MonedhaNder", DbCore.DbAdmin.clsMonedha.ktheMonedhenENdermarrjes(idNdermarrje));
            }
            else
            {
                idPerdoruesi = (int)hfState["idPerdoruesi"];
                idGjuha = (int)hfState["idGjuha"];
                idNdermarrje = (int)hfState["idNdermarrje"];
                idViti = (int)hfState["idViti"];
                idNdermarrjeVit = (int)hfState["idNdermarrjeVit"];
            }
            if (!IsPostBack)
            {
                percaktoTemplateMenu(idViti, idPerdoruesi, idNdermarrje, ASPxMenu1);
                string vleraQueryString = "";
                if (Request.QueryString["idKonfigAmbjente"] != null && Request.QueryString["idKonfigAmbjente"] != "")
                    vleraQueryString = Request.QueryString["idKonfigAmbjente"].ToString();
                int idKonfigambjenti = DbCore.clsFunksione.getIdKonfigAmbLupa(vleraQueryString, idNdermarrje, "LTRSHP");
                hfState.Set("idKonfigambjenti", idKonfigambjenti);
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, "LupaTransportuesShpejte.aspx");
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                System.Globalization.CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
                System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                konfiguroVleraFillestare(idPerdoruesi, idNdermarrje, rm, cultinf, idGjuha);
            }
            else
            {
                if (!IsCallback || (IsCallback && Request["__CALLBACKID"].ToString().Contains("ASPxMenu1")))
                    percaktoTemplateMenu(idViti, idPerdoruesi, idNdermarrje, ASPxMenu1);
            }
        }
        /// <summary>
        /// perdoret per te percaktuar templatet e komboboxeve dhe te mbushe vlerat fillestare te gridave
        /// </summary>
        /// <param name="idPerdoruesi"></param>
        private void konfiguroVleraFillestare(int idPerdoruesi, int idNdermarrje, System.Resources.ResourceManager rm, System.Globalization.CultureInfo cultinf, int idGjuha)
        { //mbush komboboxet dhe gridat e faqes
            ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 84, rm, cultinf, idGjuha);
            cmbKonfigurimi.SelectedIndex = 0;
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()));
            hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
            //cmbKonfigurimi.SelectedIndex = -1;
        }

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu((int)hfState["idViti"], (int)hfState["idPerdoruesi"], (int)hfState["idNdermarrje"], ASPxMenu1);
        }

        /// <summary>
        /// metoda per te thirrur veprimet e menuse kur shtypen butonat
        /// </summary>
        /// <param name="source">derguesi</param>
        /// <param name="e">parametrat</param>
        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {
            if (e.Item.Name == "Ruaj")
            {
                Page.Validate("entries");
                ruajTransportues();
            }
        }

        /// <summary>
        ///sherben per te ruajtur nje transportues 
        /// </summary>
        private void ruajTransportues()
        {
            DbCore.DbInventari.clsTransportues transp;

            if (Page.IsValid == false)
                return;
            else
            {
                int idPerdoruesi = (int)hfState["idPerdoruesi"];
                int idNdermarrje = (int)hfState["idNdermarrje"];
                int idViti = (int)hfState["idViti"];

                transp = krijoTransportues(idPerdoruesi);
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, "LupaTransportuesShpejte.aspx");

                if (hfShtimModifikim.Value == "shtim")
                {
                    if (!tedrejtaInfo.DShtim)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                        hfStatusi.Value = "false";
                        return;
                    }
                    mesazh = transp.ruaj();
                }
                if (mesazh.Status == true)
                {
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["mesazhRuajtjeMeSukses"], pnlMesazhi);
                    hfStatusi.Value = "true";
                    pastroFusha();
                }
                else
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ndodhi një gabim gjatë ruajtjes!", pnlMesazhi);
                    hfStatusi.Value = "false";
                }

            }
            pnlMesazhi.Update();
        }

        /// <summary>
        /// pastron fushat e faqes
        /// </summary>
        private void pastroFusha()
        {//pastron fushat  
            this.txtTransportues.Text = "";
            this.txtNIPTTransp.Text = "";
            this.txtAdresaTransp.Text = "";
            this.txtTelTransp.Text = "";
        }


        /// <summary>
        /// krijon transportuesin qe do te ruhet
        /// </summary>
        /// <returns>kthen clsTransportues me transportuesin qe do te ruhet</returns>
        /// <param name="idPerdoruesi"></param>
        private DbCore.DbInventari.clsTransportues krijoTransportues(int idPerdoruesi)
        {//krijon nje llogari sipas te dhenave te futura nga perdoruesi
            DbCore.DbInventari.clsTransportues transp = new DbCore.DbInventari.clsTransportues();
            if (txtTransportues.Text == "")
                throw new Exception("Ju lutem, plotësoni emërtimin!");

            transp.Emertimi = this.txtTransportues.Text;
            transp.NIPT = this.txtNIPTTransp.Text;
            transp.Adresa = this.txtAdresaTransp.Text;
            transp.TEL = this.txtTelTransp.Text;
            DbCore.DbAdmin.clsMonedha mon = new DbCore.DbAdmin.clsMonedha();
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            transp.IdStatusDok = 1;
            transp.IdNdermarrje = idNdermarrje;
            transp.IdPerdorues = idPerdoruesi;
            transp.IdKrijuesi = idPerdoruesi;
            transp.Targa = string.Empty;
            //hfEmertimi.Value = this.txtTransportues.Text; 
            return transp;
        }


        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="idNderVit"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        private void percaktoTemplateMenu(int idViti, int idPerdorues, int idNdermarrje, ASPxMenu aSPxMenu1)
        {
            clsToolbarConfig.percaktoTemplateMenu((int)hfState["idGjuha"], idViti, idPerdorues, idNdermarrje, aSPxMenu1, "LupaTransportuesShpejte.aspx", this, MenuInfo, hfShtimModifikim.Value == "modifikim" ? false : true, true, false, (bool)hfState["Meme"]);

            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            aSPxMenu1.Items.FindByName("Anullo").Text = rm.GetString("MenuItemMbyll", ci);
        }




    }
}