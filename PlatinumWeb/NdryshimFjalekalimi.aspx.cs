using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using DevExpress.Web;
using DbCore;
using DbCore.DbAdmin;
using DbCore.IMBUtils;
using DbCore.IMBUtils.Security;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.Messages;
using PlatinumWeb.ApplicationUtils;

namespace PlatinumWeb
{
    public partial class NdryshimFjalekalimi : MyPageBase
    {
     

        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            if (Request.QueryString["em"] == null && !DbCore.mySessionObjects.isLogedIn(Session))
            {
                DbCore.clsFunksione.logout(Session, true, "FaqePaautorizuar");
            }

            int idPerd = 0; int idGjuha = 0;
            if (!IsPostBack)
            {
                hyperLinkDalje.NavigateUrl = $"{DbCore.IMBUtils.Paths.defaultLoginPath}?arsye=logout";
                System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                System.Globalization.CultureInfo ci;
                if (Request.QueryString["em"] != null && ((Request["__EVENTTARGET"] != null && !Request["__EVENTTARGET"].Contains("ASPxMenu1")) || (Request["__EVENTTARGET"] == null)))
                {
                    string username = "";
                    clsMesazh mesazh = DbCore.EmailComposer.verifikoLinkResetPassword(Convert.ToString(Request.Url), Request.QueryString["em"], ref username, ref idGjuha);
                    if (!mesazh.Status)
                        Response.Redirect($"{Paths.defaultLoginPath}?arsye=LinkIPavlefshem");
                    btnPerdorues.Text = username;
                    hfResetimPass.Set("resetPass", true);
                    mySessionObjects.ruajGjuhe(Session, idGjuha);
                    ci = MessagesResource.KtheCultureInfo(idGjuha);
                    idPerd = clsFunksione.validoUserNgaResetimPass(Session, Response, username, ci, rm);
                    txtPasswordieksistues.Enabled = false;
                    linkuDalje.Visible = true;
                }
                else
                {
                    hfResetimPass.Set("resetPass", false);
                    idGjuha = mySessionObjects.ktheGjuhe(Session);
                    ci = MessagesResource.KtheCultureInfo(idGjuha);
                }
                if (idPerd == 0) // per te mos u marr dy here nga sessioni ne rastin kur behet resetimi i pass
                    idPerd = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                EmratELabelave(rm, ci);
                mbushHiddenFieldMePerkthime(rm, ci);
                DbCore.DbAdmin.clsPerdorues per = new DbCore.DbAdmin.clsPerdorues(idPerd);
                lblUserEmri.Text = per.PerdoruesUsername + "   |   ";
                hyperLinkDalje.Text = rm.GetString("labelLogOut", ci);
                DbCore.DbAdmin.clsKonfigurimeFjalekalimi konfig = new DbCore.DbAdmin.clsKonfigurimeFjalekalimi(per.IdPerdorues);
                hfGjatesiMinPassword.Set("GjatesiMinPass", konfig.GjatesiaMinPassword);
                if (Request.QueryString["em"] == null && per.PasswordIPerkohshem) // ne rast se behet resetim pass, nuk duhet te merret parasysh rasti i ndryshimit te passwordit kur perdoruesi ka passowrdin e perkohshem
                {
                    if (konfig.NdryshimPasswordiDetyruar)
                    {
                        hfPassPerkohshem.Set("PassPerkohshem", "Po");
                        linkuDalje.Visible = true;
                    }
                }
                if (Request.QueryString["skaduarPass"] != null)
                {
                    if (Request.QueryString["skaduarPass"].ToString().Equals("true"))
                    {
                        popNdryshoPass.ShowOnPageLoad = true;
                        hfSkaduarPassIPerdoruesit.Set("PassSkaduar", "Po");
                        linkuDalje.Visible = true;
                    }
                }
                btnPerdorues.Text = per.PerdoruesUsername;
            }else
            {
                idGjuha = mySessionObjects.ktheGjuhe(Session);
            }
            if (!Session.IsNewSession && DbCore.mySessionObjects.ktheKodNdermarrje(Session) != null)
                percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), idGjuha);
            else
                percaktoTemplateMenu(ASPxMenu1, 0, 0, 0, idGjuha);
        }

        private void mbushHiddenFieldMePerkthime(System.Resources.ResourceManager rm, System.Globalization.CultureInfo ci)
        {
            hfState.Set("msgPerdoruesitMinGjatesiPassword", rm.GetString("msgPerdoruesitMinGjatesiPassword", ci));
            hfState.Set("msgPerdoruesitMinKarakterePass", rm.GetString("msgPerdoruesitMinKarakterePass", ci));
        }

        private void EmratELabelave(System.Resources.ResourceManager rm, System.Globalization.CultureInfo ci)
        {
            lblUsername.Text = rm.GetString("lblNdryshimFjalekalimiPerdoruesi", ci);
            lblPasswordieksistues.Text = rm.GetString("lblNdryshimFjalekalimiPassEkzistues", ci);
            lblPassword.Text = rm.GetString("lblNdryshimFjalekalimiPassi", ci);
            lblKonfirmoPassword.Text = rm.GetString("lblNdryshimFjalekalimiKonfirmoPassin", ci);
            AspxWebControlUtils.perkthePopUp(popNdryshoPass, rm.GetString("headerTextNdryshimFjalekalimiText", ci), lblSkaduarPass, rm.GetString("msgNdryshimFjalekalimiSkaduarPass", ci));
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
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, "NdryshimFjalekalimi.aspx", this, MenuInfo, true, true, false, false);
            if (Request.QueryString["em"] != null || hfPassPerkohshem.Contains("PassPerkohshem")|| hfSkaduarPassIPerdoruesit.Contains("PassSkaduar"))
            {
                var menu = aSPxMenu1.Items.FindByName("TemplatedItemFrame");
                if(menu != null) menu.Visible = false;
            }
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
                System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
                System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                ruajFjalekalim(rm, ci);
            }
        }

        private void ruajFjalekalim(System.Resources.ResourceManager rm, System.Globalization.CultureInfo ci)
        {
            //IsStrongPassword;

            String pass_vjeter = this.txtPasswordieksistues.Text;
            String pass_ri = password_TextBox.Text;
            String konfirmo_pass = this.konfirmo_Textbox.Text;
            String password;
            if (pass_vjeter == "" && !Convert.ToBoolean(hfResetimPass.Get("resetPass")))
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNdryshimFjalekalimiPlotesoPassEkzistues", ci), pnlMesazhi);
                return;
            }
            if (pass_ri == "")
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNdryshimFjalekalimiPlotesoPassERi", ci), pnlMesazhi);
                return;
            }
            if (konfirmo_pass == "")
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNdryshimFjalekalimiKonfirmoPassERi", ci), pnlMesazhi);
                return;
            }
            if (hfGjatesiMinPassword.Contains("GjatesiMinPass"))
            {
                int gjatesimin = Convert.ToInt32(hfGjatesiMinPassword.Get("GjatesiMinPass"));
                if (pass_ri.Length < gjatesimin)
                {

                    string mesazhGjatesi = rm.GetString("msgPerdoruesitMinGjatesiPassword", ci) + " " + gjatesimin + " " + rm.GetString("msgPerdoruesitMinKarakterePass", ci);
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhGjatesi, pnlMesazhi);
                    return;
                }
            }
            int idPerdoruesiloguar = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            DbCore.DbAdmin.clsPerdorues clsperd = new DbCore.DbAdmin.clsPerdorues(idPerdoruesiloguar);
            if ((PasswordHelper.HashLogin(btnPerdorues.Text, pass_vjeter) == clsperd.PerdoruesPassword) || Convert.ToBoolean(hfResetimPass.Get("resetPass")))// rasti i dyte vlen kur resetohet passwordi dhe fjalekalimi i vjeter eshte bosh
            {
                if (pass_ri == konfirmo_pass)
                {
                    password = PasswordHelper.HashLogin(btnPerdorues.Text, pass_ri);
                    var konfigPass = new DbCore.DbAdmin.clsKonfigurimeFjalekalimi(clsperd.IdPerdorues);
                    var msg = konfigPass.isValidPassword(pass_ri, password, clsperd.IdPerdorues,ci,rm);
                    if (!msg.Status)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, msg.PershkrimMesazhi, pnlMesazhi);
                        txtPasswordieksistues.Text = "";
                        password_TextBox.Text = "";
                        konfirmo_Textbox.Text = "";

                        return;
                    }
                    clsperd.PerdoruesPassword = password;
                    if (clsperd.PasswordIPerkohshem)
                        hfPassPerkohshem.Set("PassPerkohshem", "Po");
                    var mesazh = clsperd.modifikoPassword(password, idPerdoruesiloguar);
                    if (mesazh.Status)
                    {
                        clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi); //"Ndryshimi perfundoi me sukses!"
                        txtPasswordieksistues.Text = "";
                        password_TextBox.Text = "";
                        konfirmo_Textbox.Text = "";

                        if (Convert.ToBoolean(hfResetimPass.Get("resetPass")) || (konfigPass.NdryshimPasswordiDetyruar && clsperd.PasswordIPerkohshem) || (konfigPass.SkadoPassword && hfSkaduarPassIPerdoruesit.Contains("PassSkaduar") && hfSkaduarPassIPerdoruesit.Get("PassSkaduar").ToString() == "Po"))
                            DbCore.clsFunksione.avancoPerpara(Response, Session, clsperd.IdPerdorues, rm, ci, (bool)Application["validInstall"], false);
                    }
                    else
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNdryshimFjalekalimiGabimNeNdryshimPassi", ci), pnlMesazhi);
                    }
                }
                else
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgPerdoruesitPasswordetNukPerkojne", ci), pnlMesazhi);
                    return;
                }
            }
            else
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNdryshimFjalekalimiPassIGabuar", ci), pnlMesazhi);
                return;
            }
        }

        protected void btnRuaj_Click(object sender, EventArgs e)
        {
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            ruajFjalekalim(rm, ci);
        }
    }
}