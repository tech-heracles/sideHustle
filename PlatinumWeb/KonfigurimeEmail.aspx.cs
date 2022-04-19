using DbCore;
using DevExpress.Web;
using System;
using System.Web.UI;
using System.Globalization;
using System.Resources;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    public partial class KonfigurimeEmail : MyPageBase
    {
        DbCore.DbAdmin.clsKonfigurimEmail konf;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!DbCore.mySessionObjects.isLogedIn(Session))
                DbCore.clsFunksione.logout(Session,true,"FaqePaautorizuar");
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
                Response.Redirect("Login_Ndermarrje.aspx?id=" + IdPerdoruesi);

            konf = new DbCore.DbAdmin.clsKonfigurimEmail(IdNdermarrja);
            percaktoTemplateMenu(ASPxMenu1, IdNdermarrjeVit, IdPerdoruesi, IdNdermarrja);
            if (!IsPostBack)
                konfiguroVleraFillestare();
            EmraTeLabelave();
        }

        private void konfiguroVleraFillestare()
        {
            if (konf.IdKonfigurimEmail > 0)
            {
                txtOutgoingSmtp.Text = konf.OutgoingSmtp;
                txtDergoEmailNga.Text = konf.DergoEmailNga;
                //txtPassword.Text = StringCipher.Decrypt(konf.Password, celesi);
                txtPortaSmtp.Text = konf.PortaSmtp.ToString();
                cbEnableSsl.Checked = konf.EnableSsl;
            }
            else
            {
                txtOutgoingSmtp.Text = "";
                txtDergoEmailNga.Text = "";
                txtPassword.Text = "";
                txtPortaSmtp.Text = "25";
                cbEnableSsl.Checked = false;
            }
        }

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(ASPxMenu1, IdNdermarrjeVit, IdPerdoruesi, IdNdermarrja);
        }

        /// <summary>
        /// metoda per te thirrur veprimet e menuse kur shtypen butonat
        /// </summary>
        /// <param name="source">derguesi</param>
        /// <param name="e">parametrat</param>
        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {
            switch (e.Item.Name)
            {
                case "Ruaj":
                    Page.Validate("entries");
                    ruajKonfigurimEmaili();
                    break;
                case "KontrolloDergimEmail":
                    Page.Validate("entries");
                    kontrolloDergimEmail();
                    break;
            }
        }

        private void kontrolloDergimEmail()
        {
            konf = new DbCore.DbAdmin.clsKonfigurimEmail(IdNdermarrja);
            clsMesazh mesazh = konf.kontrolloDergimEmail();
            clsMenuInfo.ShtoMesazh(MenuInfo, mesazh, pnlMesazhi);
        }

        private void ruajKonfigurimEmaili()
        {
            if (Page.IsValid)
            {
                DbCore.clsMesazh mesazhValidimi = kontrolloKonfigurim();
                if (mesazhValidimi.Status)
                {

                    DbCore.DbAdmin.clsKonfigurimEmail konfigurimRi = new DbCore.DbAdmin.clsKonfigurimEmail(0, txtOutgoingSmtp.Text, txtDergoEmailNga.Text, txtPassword.Text, int.Parse(txtPortaSmtp.Text), IdNdermarrja, IdPerdoruesi, cbEnableSsl.Checked);
                    DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                    if (DbCore.DbAdmin.clsKonfigurimEmail.kaKonfigurimPerNdermarrje(IdNdermarrja))//ka konfigurim dhe do modifikohet ekzistuesi.
                    {
                        if (konf.IdKonfigurimEmail > 0)
                        {
                            konfigurimRi.IdKonfigurimEmail = konf.IdKonfigurimEmail;
                            konfigurimRi.Password = txtPassword.Text;
                            mesazh = konfigurimRi.modifiko();
                        }
                    }
                    else//nuk ka konfigurim dhe do krijohet nga fillimi.
                    {
                        konfigurimRi.Password = txtPassword.Text;
                        mesazh = konfigurimRi.ruaj();
                    }

                    if (mesazh.Status)
                    {
                        clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["msgRuajtjaEKonfigurimit"] + " " + MessagesResource.Messages["msgEmailTestEshteDerguarNePostenElektronike"], pnlMesazhi);
                        konf = konfigurimRi;
                    }
                    else
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgGabimNeRuajtjeKonfigurimi"], pnlMesazhi);
                }
                else
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhValidimi.PershkrimMesazhi, pnlMesazhi);
            }
        }

        private DbCore.clsMesazh kontrolloKonfigurim()
        {
            if (txtPassword.Text != txtVerifikoPassword.Text)
                return new clsMesazh(false, MessagesResource.Messages["msgFjalekalimJoInjejteMeVerfikimin"]);
            if (txtOutgoingSmtp.Text == "")
                return new clsMesazh(false, MessagesResource.Messages["msgPlotesoniSMTP"]);
            if (txtPortaSmtp.Text == "")
                return new clsMesazh(false, MessagesResource.Messages["msgPlotesoniNrPorteSMTP"]);
            int porta;
            if (!(int.TryParse(txtPortaSmtp.Text, out porta)))
                return new clsMesazh(false, MessagesResource.Messages["msgPorteSMTPportNumber"]);
            return new clsMesazh(true);
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
            clsToolbarConfig.percaktoTemplateMenu(IdGjuha, IdViti, idPerdorues, idNdermarrje, aSPxMenu1, "KonfigurimeEmail.aspx", this, MenuInfo, true, true, false, Meme);
        }

        private void EmraTeLabelave()
        {
            lblPassword.Text = MessagesResource.Messages["labelEmailFjalekalimi"] + ":";
            lblVerifikoPassword.Text = MessagesResource.Messages["labelVerifikoPassword"] + ":";
            lblPortaSmtp.Text = MessagesResource.Messages["labelPortaSMTP"] + ":";
            lblDergoEmailNga.Text = MessagesResource.Messages["labelDergoEmailNga"] + ":";
        }
    }


    //namespace EncryptStringSample


}