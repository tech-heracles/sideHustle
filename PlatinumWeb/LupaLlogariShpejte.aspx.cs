using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DbCore.DbRegjistrim;
using DevExpress.Web;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class LupaLlogariShpejte : MyPageBase
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
                if (Request.QueryString["klonim"] != null)
                    hfShtimModifikim.Value = "klonim";
                else
                    hfShtimModifikim.Value = "shtim";
                int idKonfigambjenti = DbCore.clsFunksione.getIdKonfigAmbLupa(vleraQueryString, idNdermarrje, "LLOGSH");
                hfState.Set("idKonfigambjenti", idKonfigambjenti);
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, "LupaLlogariShpejte.aspx");
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                System.Globalization.CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
                System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                konfiguroVleraFillestare(idPerdoruesi, idNdermarrje, rm, cultinf, idGjuha);
                vendosHfMePerkthime(rm, cultinf);
            }
            else
            {
                if (!IsCallback || (IsCallback && Request["__CALLBACKID"].ToString().Contains("ASPxMenu1")))
                    percaktoTemplateMenu(idViti, idPerdoruesi, idNdermarrje, ASPxMenu1);
            }
        }

        private void vendosHfMePerkthime(System.Resources.ResourceManager rm, System.Globalization.CultureInfo cultinf)
        {
            hfState.Set("msgLupaLlogariShpejteZgjidhniGrupin", rm.GetString("msgLupaLlogariShpejteZgjidhniGrupin", cultinf));
            hfState.Set("headerPopUpZgjidhNenGrupin", rm.GetString("headerPopUpZgjidhNenGrupin", cultinf));
            hfState.Set("headerPopUpZgjidhGrupin", rm.GetString("headerPopUpZgjidhGrupin", cultinf));
            hfState.Set("headerPopUpZgjidhLlogarineStandarte", rm.GetString("headerPopUpZgjidhLlogarineStandarte", cultinf));
            hfState.Set("headerPopUpZgjidhQendrenKostos", rm.GetString("headerPopUpZgjidhQendrenKostos", cultinf));
            hfState.Set("headerPopUpZgjidhSkemenKostos", rm.GetString("headerPopUpZgjidhSkemenKostos", cultinf));
        }
        /// <summary>
        /// perdoret per te percaktuar templatet e komboboxeve dhe te mbushe vlerat fillestare te gridave
        /// </summary>
        /// <param name="idPerdoruesi"></param>
        private void konfiguroVleraFillestare(int idPerdoruesi, int idNdermarrje, System.Resources.ResourceManager rm, System.Globalization.CultureInfo cultinf, int idGjuha)
        { //mbush komboboxet dhe gridat e faqes
            ConfigureAspxComboBox.percaktoTemplateComboMeEnableCallback(cmbGrupi);
            ConfigureAspxComboBox.percaktoTemplateComboMeEnableCallback(cmbNengrupi);
            ConfigureAspxComboBox.percaktoTemplateComboMeEnableCallback(cmbKpf1);
            ConfigureAspxComboBox.mbushComboLlojQendre(cmbLloji, true, rm, ci);
            ConfigureAspxComboBox.mbushComboMonedha(idPerdoruesi, idNdermarrje, false, cmbMonedha);
            ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 82, rm, cultinf, idGjuha);
            ConfigureAspxComboBox.percaktoTemplateComboMeEnableCallback(qendraKostos_TextBox);
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
                ruajLlogari();
            }
        }

        /// <summary>
        ///sherben per te ruajtur nje llogarie 
        /// </summary>
        private void ruajLlogari()
        {
            DbCore.DbKontabiliteti.clsLlogari llogari;

            if (Page.IsValid == false)
                return;
            else
            {
                int idPerdoruesi = (int)hfState["idPerdoruesi"];
                int idNdermarrje = (int)hfState["idNdermarrje"];
                int idViti = (int)hfState["idViti"];
                int idGjuha = (int)hfState["idGjuha"];
                if (isValidLlogari())
                {
                    llogari = krijoLlogari(idPerdoruesi, idGjuha, idNdermarrje);
                    DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                    DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                    tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, "LupaLlogariShpejte.aspx");
                    System.Globalization.CultureInfo ci = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo(idGjuha);
                    System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                    if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
                    {
                        if (!tedrejtaInfo.DShtim)
                        {
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukKeniTeDrejta", ci), pnlMesazhi);
                            hfStatusi.Value = "false";
                            return;
                        }
                        mesazh = llogari.ruaj(false, "", "", "", "");
                    }
                    if (mesazh.Status)
                    {
                        clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("msgRuajtjeMeSukses", ci), pnlMesazhi);
                        hfStatusi.Value = "true";
                        hfId.Value = llogari.IdLlogari.ToString();
                        pastroFusha();
                    }
                    else
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgGabimiNeRuajtje", ci), pnlMesazhi);
                        hfStatusi.Value = "false";
                    }
                }
            }
            pnlMesazhi.Update();
        }

        /// <summary>
        /// pastron fushat e faqes
        /// </summary>
        private void pastroFusha()
        {//pastron fushat            
            this.cmbKpf1.Text = "";
            this.txtEmerLlogarie1.Text = "";
            this.txtNr.Text = "";
            this.cmbGrupi.SelectedIndex = -1;
            this.cmbMonedha.SelectedIndex = -1;
            this.cmbNengrupi.SelectedIndex = -1;
        }

        /// <summary>
        /// kontrollon nese te dhenat qe jane plotesuara jane 
        ///te lejueshme apo jo
        /// </summary>
        /// <returns> true  ose false</returns>
        private bool isValidLlogari()
        {
            bool isValid;
            isValid = true;
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            int idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            if (cmbMonedha.Text == "")
            {
                isValid = false;
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgShtoLlogariZgjidhMonedhen", ci), pnlMesazhi);
            }
            else
            {
                if (this.cmbKpf1.Text != "")
                {
                    if (DbCore.DbKontabiliteti.clsKPF.mbushIDKPF(this.cmbKpf1.Text, idNdermarrje, 1) == -1)
                    {
                        isValid = false; hfStatusi.Value = "false";
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgShtoLlogariLlogStandarteEStrukturesNukEkziston", ci), pnlMesazhi);
                        return isValid;
                    }
                    else
                    {
                        DbCore.DbKontabiliteti.clsKPF kpf = new DbCore.DbKontabiliteti.clsKPF(DbCore.DbKontabiliteti.clsKPF.mbushIDKPF(this.cmbKpf1.Text, idNdermarrje, 1));

                        if (DbCore.DbKontabiliteti.clsKPF.eshtePrind(kpf.KodiKPF, idNdermarrje, kpf.GrupiKPF, kpf.NiveliKPF))
                        {
                            isValid = false;
                            hfStatusi.Value = "false";
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgShtoLlogariNukMundTeZgjdhniLlogPrind", ci), pnlMesazhi);
                            return isValid;
                        }
                    }
                }
                else
                {
                    isValid = false; hfStatusi.Value = "false";
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgShtoLlogariZgjidhStrukturenEPare", ci), pnlMesazhi);
                    return isValid;
                }
            }

            if (this.cmbGrupi.Text != "")
            {
                if (DbCore.DbKontabiliteti.clsGrupiLlogaria.mbushIDGrupLlogari(cmbGrupi.Text, idNdermarrje, idGjuha) == -1)
                {
                    isValid = false; hfStatusi.Value = "false";
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgShtoLlogariGrupiNukEkziston", ci), pnlMesazhi);
                    return isValid;
                }
            }
            else
            {
                isValid = false; hfStatusi.Value = "false";
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgLupaLlogariShpejteJuLutemiZgjidhniGrupin", ci), pnlMesazhi);
                return isValid;
            }

            if (this.cmbNengrupi.Text != "")
            {
                if (DbCore.DbKontabiliteti.clsNenGrupiLlogaria.ktheIDNenGrupLlogaria(cmbNengrupi.Text, idNdermarrje, idGjuha) == -1)
                {
                    isValid = false; hfStatusi.Value = "false";
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgShtoLlogariNenGrupiNukEkziston", ci), pnlMesazhi);
                    return isValid;
                }
                int idgrupi = DbCore.DbKontabiliteti.clsGrupiLlogaria.mbushIDGrupLlogari(cmbGrupi.Text, idNdermarrje, idGjuha);
                int idnengrupi = DbCore.DbKontabiliteti.clsNenGrupiLlogaria.ktheIDNenGrupLlogaria(cmbNengrupi.Text, idNdermarrje, idGjuha);
                DbCore.DbKontabiliteti.clsNenGrupiLlogaria nen = new DbCore.DbKontabiliteti.clsNenGrupiLlogaria(idnengrupi, idGjuha);
                if (nen.IdGrupiLlogaria != idgrupi)
                {
                    isValid = false; hfStatusi.Value = "false";
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgShtoLlogariNenGrupiNukIPerketKetijGrupi", ci), pnlMesazhi);
                    return isValid;
                }
            }
            else
            {
                isValid = false; hfStatusi.Value = "false";
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgLupaLlogariShpejteZgjidhniNengrupin", ci), pnlMesazhi);
                return isValid;
            }
            if (DbCore.DbKontabiliteti.clsLlogari.ekzistonLlogari(txtNr.Text, idNdermarrje) && (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim"))
            {
                isValid = false;
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgShtoLlogariEkzistonNjeLlogariMeKeteNumer", ci), pnlMesazhi);
                hfStatusi.Value = "false";
                return isValid;
            }
            if (this.qendraKostos_TextBox.Text != "")
            {
                if (cmbLloji.Value.ToString() == "1")
                {
                    if (!DbCore.DbQendraKosto.clsQendraKosto.ekzistonQK(qendraKostos_TextBox.Text, idNdermarrje))
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgStrukturaAdministrativeNukEkzistonQendra", ci), pnlMesazhi);
                        hfStatusi.Value = "false";
                        return false;
                    }
                    DbCore.DbQendraKosto.clsQendraKosto obj = new DbCore.DbQendraKosto.clsQendraKosto(qendraKostos_TextBox.Text, idNdermarrje);
                    if (!obj.Aktiv)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgStrukturaAdministrativeQendraJoAktive", ci), pnlMesazhi);
                        hfStatusi.Value = "false";
                        return false;
                    }
                    DbCore.DbQendraKosto.colQendraKosto col = new DbCore.DbQendraKosto.colQendraKosto();
                    col.mbushQendraSipasPrindit(obj.Id);
                    if (col.Count > 0)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgStrukturaAdministrativeQendraPrindNukZgjidhet", ci), pnlMesazhi);
                        hfStatusi.Value = "false";
                        return false;
                    }
                }
                else
                {
                    if (!DbCore.DbQendraKosto.clsKokaSkemaQK.ekzistonSkeme(qendraKostos_TextBox.Text, idNdermarrje))
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgStrukturaAdministrativeNukEkzistonSkemaQK", ci), pnlMesazhi);
                        hfStatusi.Value = "false";
                        return false;
                    }
                }
            }
            return isValid;
        }

        /// <summary>
        /// krijon llogarine qe do te ruhet
        /// </summary>
        /// <returns>kthen clsLlogari me llogarine qe do te ruhet</returns>
        /// <param name="idPerdoruesi"></param>
        private DbCore.DbKontabiliteti.clsLlogari krijoLlogari(int idPerdoruesi, int idGjuha, int idNdermarrje)
        {//krijon nje llogari sipas te dhenave te futura nga perdoruesi
            DbCore.DbKontabiliteti.clsLlogari llogari = new DbCore.DbKontabiliteti.clsLlogari();
            if (hfShtimModifikim.Value == "shtim")
            {
                llogari.EmerLlogari2 = "";
                llogari.KPF2 = 0;
                llogari.KPF3 = 0;
                llogari.LlogariKonsoliduese = 0;
                llogari.LlogariKoresponduese = 0;
                llogari.NivelTakse = 0;
                llogari.IdPerdoruesi = idPerdoruesi;
                llogari.IdKategoriShpenzimi = 0;
                llogari.IdObjektivaKosto = 0;
                llogari.LlojQendre = 0;
                llogari.QendraKostos = 0;
                llogari.IdSkemaQendraKosto = 0;
                llogari.OColVleratFushatShtese = new DbCore.DbAdmin.colVleraFushaShtese();
                llogari.OColLidhjeAutorizim = new DbCore.DbAdmin.colLidhjetAutorizim();
                llogari.Aktiv = true;
                llogari.Shenime1 = "";
                llogari.Shenime2 = "";
                llogari.Shenime3 = "";
                llogari.Shenime4 = "";
                llogari.Shenime5 = "";

            }
            else
            {
                llogari.mbushLlogariSipasKodit(Request.QueryString["kodi"], idNdermarrje);
            }
            llogari.NrLlogari = this.txtNr.Text;
            llogari.EmerLlogari1 = this.txtEmerLlogarie1.Text;
            llogari.Shenime1 = this.txtShenime1.Text;
            llogari.Shenime2 = this.txtShenime2.Text;
            llogari.Shenime3 = this.txtShenime3.Text;
            llogari.Shenime4 = this.txtShenime4.Text;
            llogari.Shenime5 = this.txtShenime5.Text;
            llogari.LlojQendre = int.Parse(cmbLloji.Value.ToString());
            DbCore.DbAdmin.clsMonedha mon = new DbCore.DbAdmin.clsMonedha();
            mon.mbushMonedhen(cmbMonedha.Text, idNdermarrje);
            if (mon != null)
                llogari.IdMonedha = mon.IdMonedha;
            else
                llogari.IdMonedha = 0;
            llogari.Grupi = DbCore.DbKontabiliteti.clsGrupiLlogaria.mbushIDGrupLlogari(cmbGrupi.Text, idNdermarrje, idGjuha);
            llogari.Nengrupi = DbCore.DbKontabiliteti.clsNenGrupiLlogaria.ktheIDNenGrupLlogaria(cmbNengrupi.Text, idNdermarrje, idGjuha);
            if (this.cmbKpf1.Text != "")
                llogari.KPF1 = DbCore.DbKontabiliteti.clsKPF.mbushIDKPF(this.cmbKpf1.Text, idNdermarrje, 1);
            else llogari.KPF1 = 0;
            llogari.IdStatusDok = 1;
            llogari.IdNdermarja = idNdermarrje;
            DbCore.DbShare.clsKonfigurimAmbjenti konfig = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konfig.mbushKonfigAmbjSipasKod(cmbKonfigurimi.Text, idNdermarrje);
            llogari.IdKonfig = konfig.IdKonfigAmbjente;
            int idqendra, idskema;
            if (this.qendraKostos_TextBox.Text != "")
            {
                if (cmbLloji.Value.ToString() == "1")
                {
                    DbCore.DbQendraKosto.clsQendraKosto obj = new DbCore.DbQendraKosto.clsQendraKosto(qendraKostos_TextBox.Text, idNdermarrje);
                    idqendra = obj.Id;
                    idskema = 0;
                    llogari.QendraKostos = idqendra;
                }
                else
                {
                    DbCore.DbQendraKosto.clsKokaSkemaQK obj = new DbCore.DbQendraKosto.clsKokaSkemaQK(qendraKostos_TextBox.Text, idNdermarrje);
                    idskema = obj.IdKoka;
                    idqendra = 0;
                    llogari.IdSkemaQendraKosto = idskema;
                  
                }
            }
            else
            {
                idqendra = 0; idskema = 0;
            }


            return llogari;

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
            clsToolbarConfig.percaktoTemplateMenu((int)hfState["idGjuha"], idViti, idPerdorues, idNdermarrje, aSPxMenu1, "LupaLlogariShpejte.aspx", this, MenuInfo, hfShtimModifikim.Value == "modifikim" ? false : true, true, false, (bool)hfState["Meme"]);
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            aSPxMenu1.Items.FindByName("Anullo").Text = rm.GetString("MenuItemMbyll", ci);
        }

        /// <summary>
        /// perdoret per te mbushur combon e kpf 1 ne momentin qe perdoruesi fillon te shkruaj
        /// </summary>
        /// <param name="source">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void cmbKpf1_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("cmbKpf1"))
                {
                    ConfigureAspxComboBox.mbushComboKPFBij((int)hfState["idPerdoruesi"], (int)hfState["idNdermarrje"], cmbKpf1, 1);
                }
            }
        }

        /// <summary>
        /// perdoret per te mbushur combon e monedhes ne momentin qe perdoruesi fillon te shkruaj
        /// </summary>
        /// <param name="source">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void cmbMonedha_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("cmbMonedha"))
                {
                    ConfigureAspxComboBox.mbushComboMonedha((int)hfState["idPerdoruesi"], (int)hfState["idNdermarrje"], false, cmbMonedha);
                }
            }
        }

        /// <summary>
        /// perdoret per te mbushur combon e grupit ne momentin qe perdoruesi fillon te shkruaj
        /// </summary>
        /// <param name="source">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void cmbGrupi_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("cmbGrupi"))
                {
                    ConfigureAspxComboBox.mbushComboGrupeLlogarish((int)hfState["idNdermarrje"], cmbGrupi, (int)hfState["idGjuha"]);
                }
            }
        }
        protected void qendraKostos_TextBox_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].Contains("qendraKostos_TextBox"))
                {
                    if (cmbLloji.Value.ToString() == "1")
                        ConfigureAspxComboBox.mbushComboQendraKostoBij(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), qendraKostos_TextBox);
                    else ConfigureAspxComboBox.mbushComboSkemaQendraKosto(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), qendraKostos_TextBox);
                }
            }
        }


        protected void qendraKostos_TextBox_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].Contains("qendraKostos_TextBox"))
                {
                    if (string.IsNullOrWhiteSpace(e.Filter)) return;
                    DbCore.DbQendraKosto.colQendraKosto col = new DbCore.DbQendraKosto.colQendraKosto();
                    if (qendraKostos_TextBox.Value != null)
                        col.mbushQendraSipasPrindit(int.Parse(qendraKostos_TextBox.Value.ToString()));
                    else
                        col.mbushGjitheQendraKostoBijSipasNdermarjesAktiv(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    qendraKostos_TextBox.DataSource = col.Where(x => x.Kodi.IndexOf(e.Filter, StringComparison.InvariantCultureIgnoreCase) > -1).Skip(e.BeginIndex).Take(e.EndIndex + 1);

                    qendraKostos_TextBox.TextField = "Kodi";
                    qendraKostos_TextBox.ValueField = "Id";
                    qendraKostos_TextBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                    qendraKostos_TextBox.DataBind();


                }
            }
        }

        /// <summary>
        /// perdoret per te mbushur combon e nengrupit ne momentin qe perdoruesi fillon te shkruaj
        /// </summary>
        /// <param name="source">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void cmbNengrupi_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("cmbNengrupi"))
                {
                    int id = 0;
                    if (cmbGrupi.Text != "")
                    {
                        if (new DbCore.DbKontabiliteti.clsGrupiLlogaria(cmbGrupi.Text, (int)hfState["idNdermarrje"], (int)hfState["idGjuha"]) != null)
                            id = DbCore.DbKontabiliteti.clsGrupiLlogaria.mbushIDGrupLlogari(cmbGrupi.Text, (int)hfState["idNdermarrje"], (int)hfState["idGjuha"]);
                    }
                    ConfigureAspxComboBox.mbushComboNenGrupe(cmbNengrupi, id, DbCore.mySessionObjects.ktheGjuhe(Session));
                }
            }
        }
    }
}