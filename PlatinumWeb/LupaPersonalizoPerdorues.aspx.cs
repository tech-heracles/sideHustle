using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Globalization;
using System.Resources;
using System.IO;
using DbCore.DbAdmin;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.DataBase;
using DbCore.DbShare;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    public partial class LupaPersonalizoPerdorues : MyPageBase
    {
        const string UploadDirectory = "~/images/";
        private string ThumbnailFileName;

  

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                DbCore.clsFunksione.logout(Session, true, "FaqePaautorizuar");
            }
            int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerdoruesi);
            }
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idviti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            int idgjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            percaktoTemplateMenu(ASPxMenu1, idviti, idPerdoruesi, idNdermarrje, idgjuha);
            if (!Page.IsPostBack)
            {
                hfState.Set("ndryshuarFoto", false);
                ASPxPageControl1.ActiveTabIndex = 0;
                DbCore.DbAdmin.clsPerdorues perdoruesi = new DbCore.DbAdmin.clsPerdorues(idPerdoruesi);
                emri_TextBox.Text = perdoruesi.EmriPerdorues;
                mbiemri_TextBox.Text = perdoruesi.MbiemriPerdorues;
                email_TextBox.Text = perdoruesi.PerdoruesEmail;
                tel_TextBox.Text = perdoruesi.PerdoruesTel;
                fax_TextBox.Text = perdoruesi.PerdoruesFax;
                string imazhi = DbCore.DbShare.clsArkiva.ktheImazhPerdoruesi(idPerdoruesi);
                if (imazhi != "" && File.Exists(Server.MapPath("~/" + imazhi)))
                {
                    string pathFinalZoro = Server.MapPath("~/" + imazhi);
                    System.Drawing.Image img = System.Drawing.Image.FromFile(pathFinalZoro);

                    byte[] arr = imageToByteArray(img);
                    DbCore.mySessionObjects.ruajImazhNeSesion(Session, arr);

                    ASPxBinaryImage1.ContentBytes = arr;
                    ASPxBinaryImage1.Height = 50;
                    ASPxBinaryImage1.Width = 50;
                }
                hfStatusi.Value = "false";
            }
        }

        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                return ms.ToArray();
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
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        private void percaktoTemplateMenu(ASPxMenu aSPxMenu1, int idViti, int idPerdorues, int idNdermarrje, int idGjuha)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, "LupaPersonalizoPerdorues.aspx", this, MenuInfo, true, true, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
            aSPxMenu1.Items.FindByName("Anullo").Text = rm.GetString("MenuItemMbyll", cultinf);
        }

        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {
            if (e.Item.Name == "Ruaj")
            {
                Page.Validate("entries");
                if (Page.IsValid)
                    modifikoPerdorues();                
            }
        }

        protected void modifikoPerdorues()
        {
            DbCore.DbAdmin.clsPerdorues perdoruesi = new DbCore.DbAdmin.clsPerdorues(DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            perdoruesi.EmriPerdorues = emri_TextBox.Text;
            perdoruesi.MbiemriPerdorues = mbiemri_TextBox.Text;
            perdoruesi.PerdoruesEmail = email_TextBox.Text;
            perdoruesi.PerdoruesTel = tel_TextBox.Text;
            perdoruesi.PerdoruesFax = fax_TextBox.Text;

            colRolPerdorues oColRolPerdoruesi = new colRolPerdorues();
            oColRolPerdoruesi.mbushRolePerdoruesSipasPerdoruesi(perdoruesi.IdPerdorues);
            perdoruesi.OColRolPerdoruesi = oColRolPerdoruesi;
            DbCore.clsMesazh mesazh = perdoruesi.modifiko("false", "");
            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                hfStatusi.Value = "false";
                return;
            }


            if (Convert.ToBoolean(hfState.Get("ndryshuarFoto")))
            {
                mesazh = modifikoFoton(perdoruesi);
                if (!mesazh.Status)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                    hfStatusi.Value = "false";
                }
                else
                {
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                    hfStatusi.Value = "true";
                }
            }
            else
            {
                hfStatusi.Value = "true";
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            }
        }

        protected DbCore.clsMesazh modifikoFoton(clsPerdorues perdoruesi)
        {
            var  conn = clsArkiva.ktheConnString(MyConnectionsManager.GetSelectedConNameServer());
           
            var UploadDirectory = $"~/Arkiva/{conn}/UserPics/";
            if (!Directory.Exists(Server.MapPath(UploadDirectory)))
            {
                try
                {
                    Directory.CreateDirectory(Server.MapPath(UploadDirectory));
                }
                catch (System.IO.PathTooLongException err)
                {
                    return new DbCore.clsMesazh(false, "Pathi i imazhit eshte shume i gjate!");
                }
                catch (System.IO.DirectoryNotFoundException err)
                {
                    return new DbCore.clsMesazh(false, "Pathi nuk eshte i sakte ( for example, it is on an unmapped drive)!");
                }
                catch (System.IO.IOException err)
                {
                    return new DbCore.clsMesazh(false, "Direktoria Arkiva eshte 'read-only'!");
                }
                catch (System.UnauthorizedAccessException err)
                {
                    return new DbCore.clsMesazh(false, "Ju nuk keni te drejta te shkruani ne direktorine Arkiva!");
                }
                catch (System.ArgumentNullException err)
                {
                    return new DbCore.clsMesazh(false, "Pathi eshte null!");
                }
                catch (System.ArgumentException err)
                {
                    return new DbCore.clsMesazh(false, "Pathi eshte string bosh, permban vetem hapesire, ose ka karaktere jo te vlefshme!");
                }
                catch (System.NotSupportedException err)
                {
                    return new DbCore.clsMesazh(false, "Pathi permban karakterin ':' !");
                }
            }
          
            var filename = perdoruesi.PerdoruesUsername + DateTime.Now.ToFileTime() + ".Jpeg";
            var resultFilePath = UploadDirectory + filename;
            DbCore.DbShare.clsArkiva ark = new DbCore.DbShare.clsArkiva(0, perdoruesi.IdPerdorues, 21, "image/Jpeg", resultFilePath.Substring(2), filename, "", 1, perdoruesi.IdPerdorues, perdoruesi.IdPerdorues, 0, conn);
            DbCore.clsMesazh mesazh = ark.ruaj();
            if (!mesazh.Status)
                return mesazh;

            try
            {
                using (MemoryStream ms = new MemoryStream(DbCore.mySessionObjects.merrImazhNgaSesioni(Session)))
                {
                    try
                    {
                        System.Drawing.Image foto = System.Drawing.Image.FromStream(ms);
                        try
                        {
                            string pathFinalZoro = Server.MapPath(resultFilePath);
                            foto.Save(pathFinalZoro, System.Drawing.Imaging.ImageFormat.Jpeg);
                            return new DbCore.clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
                        }
                        catch (System.ArgumentNullException err)
                        {
                            return new DbCore.clsMesazh(false, "Emri i file nuk eshte i sakte!");
                        }
                        catch (System.Runtime.InteropServices.ExternalException err)
                        {
                            return new DbCore.clsMesazh(false, "Imazhi " + resultFilePath + " eshte ruajtur me formatin e gabuar!");
                        }
                    }
                    catch (System.ArgumentException err)
                    {
                        return new DbCore.clsMesazh(false, "Ndodhi nje gabim gjate krijimit te imazhit nga stringu!");
                    }
                }
            }
            catch (System.ArgumentNullException err)
            {
                return new DbCore.clsMesazh(false, "Ndodhi nje gabim gjate ruajtjes se imazhit!");
            }
        }

        protected void ngarkoImazh_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            e.CallbackData = RuajFilePostuar(e.UploadedFile);
        }

        string RuajFilePostuar(UploadedFile uploadedFile)
        {
            if (!uploadedFile.IsValid)
                return string.Empty;
           
            DbCore.mySessionObjects.ruajImazhNeSesion(Session, uploadedFile.FileBytes);
            return ThumbnailFileName;
        }

        protected void btnShfaqImazh_Click(object sender, EventArgs e)
        {
            //ASPxBinaryImage1.ContentBytes = ((Byte[])CacheLayer.GlobalCacheManager.MySessionCache["Imazhi"]);
            ASPxBinaryImage1.ContentBytes = DbCore.mySessionObjects.merrImazhNgaSesioni(Session);
            pnlImazh.Update();
            hfState.Set("ndryshuarFoto", true);
            if (ASPxBinaryImage1.Width.Value > ASPxBinaryImage1.Height.Value)
            {
                ASPxBinaryImage1.Width = 50;
                ASPxBinaryImage1.Style.Add("height", "auto");
            }
            else
            {
                ASPxBinaryImage1.Height = 50;
                ASPxBinaryImage1.Style.Add("width", "auto");
            }
        }
    }
}