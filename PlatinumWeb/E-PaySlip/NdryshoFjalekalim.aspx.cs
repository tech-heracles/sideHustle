using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using DevExpress.Web;
using System.Globalization;
using DbCore;
using DbCore.IMBUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.Security;
using DbCore.IMBUtils.Messages;
using PlatinumWeb.ApplicationUtils;

namespace PlatinumWeb.E_PaySlip
{
    public partial class NdryshoFjalekalim : MyPageBase
    {
        System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["em"] == null && !DbCore.mySessionObjects.isLogedIn(Session))
            {
                DbCore.clsFunksione.Logout(Session, true, true, false, Paths.loginPathEpaySlip, "FaqePaautorizuar");
            }
            int idPerd = 0; int idGjuha = 0;
            if (!IsPostBack)
            {
                if (Request.QueryString["em"] != null && ((Request["__EVENTTARGET"] != null && !Request["__EVENTTARGET"].Contains("ASPxMenu1")) || (Request["__EVENTTARGET"] == null)))
                {
                    string username = "";
                    clsMesazh mesazh = DbCore.EmailComposer.verifikoLinkResetPassword(Convert.ToString(Request.Url), Request.QueryString["em"], ref username, ref idGjuha, true);
                    if (!mesazh.Status)
                        Response.Redirect($"{Paths.loginPathEpaySlip}?arsye=LinkIPavlefshem");

                    btnPerdorues.Text = username;
                    hfResetimPass.Set("resetPass", true);
                    idPerd = clsFunksione.ruajTedhenatPasValidimitTeLoginUserit(Session: Session, username: username, punonjes: true);
                    mySessionObjects.ruajGjuhe(Session, idGjuha);
                    txtPasswordieksistues.Enabled = false;
                }
                else
                {
                    hfResetimPass.Set("resetPass", false);
                    idGjuha = mySessionObjects.ktheGjuhe(Session);
                }
                if (idPerd == 0)
                    idPerd = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.DbListPagesat.clsPunonjes punonjes = new DbCore.DbListPagesat.clsPunonjes(idPerd);
                lblUserEmri.Text = punonjes.Emer;
                // DbCore.mySessionObjects.kthePerdorues(Session).PerdoruesUsername + "   |   ";
                ASPxHyperLink2.Text = "Log out";
                ASPxHyperLink2.NavigateUrl = $"{DbCore.IMBUtils.Paths.loginPathEpaySlip}?arsye=logout";
                System.Globalization.CultureInfo ci = MessagesResource.KtheCultureInfo(idGjuha);
                EmratELabelave(rm, ci);
                // DbCore.DbAdmin.clsPerdorues per = new DbCore.DbAdmin.clsPerdorues(idPerd);
                DbCore.DbAdmin.clsKonfigurimeFjalekalimi konfig = new DbCore.DbAdmin.clsKonfigurimeFjalekalimi(punonjes.IdPunonjes, true);
                hfGjatesiMinPassword.Set("GjatesiMinPass", konfig.GjatesiaMinPassword);

                if (Request.QueryString["skaduarPass"] != null)
                {
                    if (Request.QueryString["skaduarPass"].ToString().Equals("true"))
                    {
                        popNdryshoPass.ShowOnPageLoad = true;
                        hfSkaduarPassIPerdoruesit.Set("PassSkaduar", "Po");
                        linkuDalje.Visible = true;
                    }
                }
                btnPerdorues.Text = punonjes.Username;
            }
        }
        private void mbushHiddenFieldMePerkthime(System.Resources.ResourceManager rm, System.Globalization.CultureInfo ci)
        {
            hfState.Set("msgPerdoruesitMinGjatesiPassword", rm.GetString("msgPerdoruesitMinGjatesiPassword", ci));
            hfState.Set("msgPerdoruesitMinKarakterePass", rm.GetString("msgPerdoruesitMinKarakterePass", ci));
        }

        private void EmratELabelave(System.Resources.ResourceManager rm, System.Globalization.CultureInfo ci)
        {
            string html = "";
            lblUsername.Text = rm.GetString("lblNdryshimFjalekalimiPerdoruesi", ci);
            lblPasswordieksistues.Text = rm.GetString("lblNdryshimFjalekalimiPassEkzistues", ci);
            lblPassword.Text = rm.GetString("lblNdryshimFjalekalimiPassi", ci);
            lblKonfirmoPassword.Text = rm.GetString("lblNdryshimFjalekalimiKonfirmoPassin", ci);
            btnruaj.Text = rm.GetString("buttonRuaj", ci);
            lblNdryshoFjalekalim.Text = rm.GetString("lblNdryshoFjalekalim", ci);
            dalje.InnerText = rm.GetString("lblDalje", ci);
            html += "<li><a href='ListaRaporte.aspx?vjenNga=Pini'" + "'>" + "<span style='vertical-align:middle' class='fa " + "CRM/AlphaWeb.png" + " fa-2x'></span>" + "&nbsp; " + rm.GetString("lblListaRaporteve", ci) + "</a></li>";
            html += "<li><a href=" + "'NdryshoFjalekalim.aspx'" + ">" + "<span style='vertical-align:middle' class='fa " + "CRM/AlphaWeb.png" + " fa-2x'></span>" + "&nbsp; " + rm.GetString("lblNdryshoFjalekalim", ci) + "</a></li>";
            ulMenu.InnerHtml = html;
            AspxWebControlUtils.perkthePopUp(popNdryshoPass, rm.GetString("headerTextNdryshimFjalekalimiText", ci), lblSkaduarPass, rm.GetString("msgNdryshimFjalekalimiSkaduarPass", ci));
            ASPxHyperLink2.NavigateUrl = $"{Paths.loginPathEpaySlip}?arsye=logout";
        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        /// <param name="idViti"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNdermarrje"></param>
        private void percaktoTemplateMenu(ASPxMenu aSPxMenu1, int idViti, int idPerdorues, int idNdermarrje, int idGjuha)
        {
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, "NdryshimFjalekalimi.aspx", this, MenuInfo, true, true, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
        }

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheGjuhe(Session));
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
                ruajFjalekalim();
            }
        }
        public void updateFusha(string mesazhi)
        {
            lblmsg.Text = mesazhi;
            lblmsg.Visible = true;
            txtPasswordieksistues.Text = "";
            password_TextBox.Text = "";
            konfirmo_Textbox.Text = "";
            pnlPaswordi.Update();
        }
        public clsMesazh valido(System.Resources.ResourceManager rm, System.Globalization.CultureInfo ci, string pass_vjeter, string pass_ri, string konfirmo_pass, string password)
        {
            if (pass_vjeter == "" && !Convert.ToBoolean(hfResetimPass.Get("resetPass")))//kushti i dyte vlen kur behet resetimi i pass qe ska nevoje per pass e vjeter
                return new clsMesazh(false, rm.GetString("msgNdryshimFjalekalimiPlotesoPassEkzistues", ci));
            if (pass_ri == "")
                return new clsMesazh(false, rm.GetString("msgNdryshimFjalekalimiPlotesoPassERi", ci));
            if (konfirmo_pass == "")
                return new clsMesazh(false, rm.GetString("msgNdryshimFjalekalimiKonfirmoPassERi", ci));
            if (hfGjatesiMinPassword.Contains("GjatesiMinPass"))
            {
                int gjatesimin = Convert.ToInt32(hfGjatesiMinPassword.Get("GjatesiMinPass"));
                if (pass_ri.Length < gjatesimin)
                    return new clsMesazh(false, rm.GetString("msgPerdoruesitMinGjatesiPassword", ci) + " " + gjatesimin + " " + rm.GetString("msgPerdoruesitMinKarakterePass", ci));
            }
            if (!((PasswordHelper.HashLogin(btnPerdorues.Text, pass_vjeter) == password) || Convert.ToBoolean(hfResetimPass.Get("resetPass"))))// rasti i dyte vlen kur resetohet passwordi dhe fjalekalimi i vjeter eshte bosh
                return new clsMesazh(false, rm.GetString("msgNdryshimFjalekalimiPassIGabuar", ci));
            if (pass_ri != konfirmo_pass)
                return new clsMesazh(false, rm.GetString("msgPerdoruesitPasswordetNukPerkojne", ci));
            return new clsMesazh(true);
        }

        private void ruajFjalekalim()
        {
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            String pass_vjeter = this.txtPasswordieksistues.Text;
            String pass_ri = password_TextBox.Text;
            String konfirmo_pass = this.konfirmo_Textbox.Text;
            int idPerdoruesiloguar = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            DbCore.DbListPagesat.clsPunonjes clsperd = new DbCore.DbListPagesat.clsPunonjes(idPerdoruesiloguar);
            clsMesazh mesazhRuatje = valido(rm, ci, pass_vjeter, pass_ri, konfirmo_pass, clsperd.Password);
            if (!mesazhRuatje.Status)
            {
                lblmsg.Text = mesazhRuatje.PershkrimMesazhi;
                lblmsg.Visible = true;
                return;
            }
            String password = PasswordHelper.HashLogin(btnPerdorues.Text, pass_ri);
            DbCore.DbAdmin.clsKonfigurimeFjalekalimi konfigPass = new DbCore.DbAdmin.clsKonfigurimeFjalekalimi(clsperd.IdPunonjes, true);
            DbCore.clsMesazh msg = konfigPass.isValidPassword(pass_ri, password, clsperd.IdPunonjes,ci,rm,"epayslip");
            if (!msg.Status)
            {
                updateFusha(msg.PershkrimMesazhi);
                return;
            }
            clsperd.Password = password;
            msg = clsperd.modifikoPassword(password, idPerdoruesiloguar);
            if (msg.Status)
            {
                updateFusha(rm.GetString("msgPasswordiURuajtMeSukses", ci));
                if (Convert.ToBoolean(hfResetimPass.Get("resetPass")))
                {
                    DbCore.clsFunksione.Logout(Session, true, true, false, Paths.loginPathEpaySlip, "NdryshimPasswordi");
                }
            }
            else
            {
                lblmsg.Text = rm.GetString("msgNdodhiNjeGabimGjateRuajtjesSePasswordit", ci);
                lblmsg.Visible = true;
                return;
            }
        }

        protected void btnRuaj_Click(object sender, EventArgs e)
        {
            ruajFjalekalim();
        }
    }
}