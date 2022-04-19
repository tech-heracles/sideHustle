using System;
using DevExpress.Web;
using DbCore.DbRegjistrim;
using DbCore;
using DbCore.IMBUtils.Messages;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    /// <summary>
    /// Faqja e dhuratave
    /// </summary>
    public partial class ShperndaDhurate : MyPageBase
    {
        private const string Komponente = "ShperndaDhurate.aspx";

        /// <summary>
        /// thirret kur faqja lodohet. Ne te behet kontrolli nese perdoruesi eshte i loguar ne sistem  dhe nqs jo ridrejtohet tek forma e logimit
        /// thirret inicializimi i konfigurimeve fillestare te faqes
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e"> argumenti</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!mySessionObjects.isLogedIn(Session))
            {
                Response.Redirect(DbCore.IMBUtils.Paths.defaultLoginPath);
            }

            if (mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + IdPerdoruesi);
            }

            if (!Page.IsPostBack)
            {
                EmrateTabeve();
                hfState.Set("idPerdoruesi", IdPerdoruesi);
                hfState.Set("idGjuha", IdGjuha);
                hfState.Set("idNdermarrje", IdNdermarrja);
                hfState.Set("idViti", IdViti);
                hfState.Set("idNdermarrjeVit", IdNdermarrjeVit);
                hfRuaj.Value = "shtim";

                ASPxPageControl1.ActiveTabIndex = 0;
                PercaktoTemplateMenu();
                KonfiguroVleraFillestare();

                var tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, Komponente);
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
            }
            
            PercaktoTemplateMenu();
        }

        /// <summary>
        /// Vendos emrat e tabeve ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateTabeve()
        {
            ASPxPageControl1.TabPages[0].Text = MessagesResource.Messages["labelRaportTePergjithshme"];
        }
        
        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="idNderVit"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        private void PercaktoTemplateMenu()
        {
            clsToolbarConfig.percaktoTemplateMenu(IdGjuha, IdViti, IdPerdoruesi, IdNdermarrja, ASPxMenu1, Komponente, this, MenuInfo, false, true, false, Meme);
        }
        
        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            PercaktoTemplateMenu();
        }

        /// <summary>
        /// perdoret per te trajtuar ngjarjet e butonave te menuse. 
        /// </summary>
        /// <param name="source"> derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {
            if (e.Item.Name == "Ruaj")
            {
                Page.Validate("entries");
                RuajDhurate();
            }
        }

        /// <summary>
        /// konfiguron vlerat fillestare
        /// </summary>
        /// <param name="idNdermarrje">id e ndermarjes</param>
        private void KonfiguroVleraFillestare()
        {
            ASPxPageControl1.ActiveTabIndex = 0;
            ConfigureAspxComboBox.KonfiguroComboBoxKartatPerDhurata(cmbKarta, IdNdermarrja);
            ConfigureAspxComboBox.KonfiguroComboBoxDhurata(cmbDhurata, cmbKarta.SelectedIndex != -1 && cmbKarta.Value != null
                ? int.Parse(cmbKarta.Value.ToString())
                : 0);
        }

        /// <summary>
        /// ruan kartat 
        /// </summary>
        private void RuajDhurate()
        {
            if (Page.IsValid == false)
                return;

            try
            {
                var dhurate = KrijoDhurate();

                if (hfRuaj.Value == "shtim" || hfRuaj.Value == "klonim")
                {
                    var tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                    tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, Komponente);
                    if (!tedrejtaInfo.DShtim)
                    {
                        throw new MyException(MessagesResource.Messages["Dhurata.NukKeniTeDrejta"]);
                    }

                    dhurate.Ruaj();

                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["Dhurata.RuajtjeSukses"], pnlMesazhi);
                    hfStatusi.Value = "true";
                }
            }
            catch (MyException ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex.Message);
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, ex.Message, pnlMesazhi);
                hfStatusi.Value = "false";
            }
            catch (Exception e)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(e.Message);
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["Dhurata.RuajtjaGabim"], pnlMesazhi);
                hfStatusi.Value = "false";
            }

            ASPxPageControl1.ActiveTabIndex = 0;
        }

        /// <summary>
        /// krijon burimin sipas te dhenave
        /// </summary>
        /// <returns> burimin me te dhenat</returns>
        private Dhurata KrijoDhurate()
        {
            int idKarta;
            int idKategori;
            int pike;
            
            if (cmbKarta.Text != "")
                int.TryParse(cmbKarta.SelectedItem.Value.ToString(), out idKarta);
            else
                throw new MyException(MessagesResource.Messages["Dhurata.ZgjidhKarten"]);

            if (cmbDhurata.Text != "")
            {
                int.TryParse(cmbDhurata.Value.ToString(), out idKategori);
                int.TryParse(cmbDhurata.Text, out pike);
            }
            else
                throw new MyException(MessagesResource.Messages["Dhurata.ZgjidhPiket"]);

            return new Dhurata(idKarta, idKategori, pike, IdPerdoruesi, IdNdermarrja, IdNdermarrjeVit);
        }
        
        protected void cmbKarta_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("cmbKarta"))
                ConfigureAspxComboBox.KonfiguroComboBoxKartatPerDhurata(cmbKarta, IdNdermarrja);
        }

        protected void cmbDhurata_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("cmbDhurata"))
                ConfigureAspxComboBox.KonfiguroComboBoxDhurata(cmbDhurata, cmbKarta.SelectedIndex != -1 && cmbKarta.Value != null
                    ? int.Parse(cmbKarta.Value.ToString())
                    : 0);
        }
    }
}
