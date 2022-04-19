using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using DbCore;
using DevExpress.Web;
using System.Globalization;
using System.Resources;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.Messages;
using DbCore.DbInventari;

namespace PlatinumWeb
{
    public partial class Shto_Transportues : MyPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            int idPerdoruesi, idGjuha, idNdermarrje, idViti, idNdermarrjeVit, idKonfigAmbjente;
            bool lupe = false;
            
            if (hfState.Count == 0)
            {
                if (!DbCore.mySessionObjects.isLogedIn(Session))
                {
                    DbCore.clsFunksione.logout(Session,true,"FaqePaautorizuar");
                }
                idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
                {
                    Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerdoruesi);
                }
                idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
                idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
                idNdermarrjeVit = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
                hfState.Set("idPerdoruesi", idPerdoruesi);
                hfState.Set("idGjuha", idGjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idViti", idViti);
                hfState.Set("idNdermarrjeVit", idNdermarrjeVit);
                lupe = !String.IsNullOrEmpty(Request.QueryString["lupe"]) && Request.QueryString["lupe"] == "true";
                hfState.Set("lupe", lupe);

                hfState.Set("Meme", DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
                hfState.Set("ModelIRi", false);
                if (!String.IsNullOrEmpty(Request.QueryString["idKonfigAmbjente"]))
                {
                    bool parse = int.TryParse(Request.QueryString["idKonfigAmbjente"], out idKonfigAmbjente);
                    if (!parse)
                        idKonfigAmbjente = 0;
                }
                else
                    idKonfigAmbjente = 0;
                hfState.Set("idKonfigAmbjente", idKonfigAmbjente);
            }
            else
            {
                idPerdoruesi = (int)hfState["idPerdoruesi"];
                idGjuha = (int)hfState["idGjuha"];
                idNdermarrje = (int)hfState["idNdermarrje"];
                idViti = (int)hfState["idViti"];
                idNdermarrjeVit = (int)hfState["idNdermarrjeVit"];
                lupe = (bool)hfState["lupe"];
                idKonfigAmbjente = (int)hfState["idKonfigAmbjente"];
            }

            if (!IsCallback)
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                    DbCore.clsFunksione.logout(Session, false, "", false);
            if (!Page.IsPostBack)
            {
                EmrateTabeve();
                mbushHiddenFieldMePerkthime();
                konfiguroVleraFillestare(idNdermarrje, idGjuha, idKonfigAmbjente);
                ASPxPageControl1.ActiveTabIndex = 0;
                mbushGridTransportuesNgaDB(idNdermarrje, lupe);
                konfiguroGride(idNdermarrje, idPerdoruesi);
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "ASPxGridView_Transportues", ASPxGridView_Transportues, cmbKonfigurimi.Text, Convert.ToString(913), (int)hfState["idGjuha"]);
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, "Shto_Transportues.aspx");
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
            }
            else
            {
                mbushGridTransportuesNgaSesioni(idNdermarrje);
                konfiguroGride(idNdermarrje, idPerdoruesi);
            }
            GridUtil.konfigGrideListeEMadhePaTheme(ASPxGridView_Transportues, "IdTransportues");
            percaktoTemplateMenu(idGjuha,idViti, idPerdoruesi, idNdermarrje, ASPxMenu1, lupe);
            mbushComboTipiId();
            clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, "ASPxGridView_Transportues", int.Parse(cmbKonfigurimi.Value.ToString()), "Shto_Transportues.aspx");
            ASPxGridView_Transportues.PercaktoTitlePanel(this, MenuInfo, pnlMesazhi, hfState, idPerdoruesi, idNdermarrje, idViti, idGjuha, int.Parse(cmbKonfigurimi.Value.ToString()), "Shto_Transportues.aspx", rm, cultinf);
            AspxWebControlUtils.perkthePopUp(popFshi, MessagesResource.Messages["labelKujdes"], lblMsgbox, MessagesResource.Messages["labelAdministrimiMsgJeniSigurt"], ButtonCancel, MessagesResource.Messages["labelAnullo"]);
        }

        /// <summary>
        /// Vendos emrat e tabeve ne baze te gjuhes se perdoruesit
        /// </summary>
        private void EmrateTabeve()
        {
            ASPxPageControl1.TabPages[0].Text = MessagesResource.Messages["TePergjithshmeTab"];
            ASPxPageControl1.TabPages[1].Text = MessagesResource.Messages["labelRaportTransportues"];
            ASPxPageControl1.TabPages[2].Text = MessagesResource.Messages["tabOperatore"];
            konfigurimi_Label.Text = MessagesResource.Messages["lblModeli"];
        }
        private void mbushComboTipiId()
        {
            cmbTipiId.Items.Add("NUIS");
            cmbTipiId.Items.Add("ID");
        }
        /// <summary>
        /// Metode qe sherben per te kaluar perkthime te js
        /// </summary>
        private void mbushHiddenFieldMePerkthime()
        {
            hfState.Set("msgPoTransferohetTeDhenatShtypniPerseriRuaj", MessagesResource.Messages["msgPoTransferohetTeDhenatShtypniPerseriRuaj"]);
            hfState.Set("msgZgjidhniNjeTransportues", MessagesResource.Messages["msgZgjidhniNjeTransportues"]);
        }

        private void konfiguroVleraFillestare(int idNdermarrje, int idGjuha, int idKonfigAmbjente)
        { //mbush komboboxet dhe gridat e faqes
            int idPerdoruesi = (int)hfState["idPerdoruesi"];
            mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 83, "TR", idGjuha);
            if (idKonfigAmbjente != 0)
                cmbKonfigurimi.SelectedItem = cmbKonfigurimi.Items.FindByValue(idKonfigAmbjente.ToString());
            else
                cmbKonfigurimi.SelectedIndex = 0;
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.Value.ToString()), idGjuha);
            hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
        }

        public void mbushComboKonfigurimeshSipasKategorise(int idPerdoruesi, int idNdermarrje, ASPxComboBox combo, int kat, string nivel, int idGjuha)
        {//mbush griden e popupit me te dhena
            DbCore.DbShare.colKonfigurimAmbjenti col = new DbCore.DbShare.colKonfigurimAmbjenti();
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            if (kat != 0)
            {
                konf.IdKategori = kat;
                konf.IdNdermarje = idNdermarrje;
                int idNivel = DbCore.DbRegjistrim.clsNivelRegjistrimi.ktheIdNivelRegjistrimiSipasKodi(nivel, idNdermarrje);
                col.mbushKonfigAmbjSipasIdKategoriIdNivelMeLloj(konf.IdKategori, idNivel, idPerdoruesi);
            }
            else
            {
                col.mbushGjitheKonfigurimeAmbjentesh(idNdermarrje, idPerdoruesi, 1, idGjuha);
            }
            ListBoxColumn colprove = new ListBoxColumn();
            colprove.FieldName = "KodKonfigAmbjente";
            colprove.Caption = "Kodi";
            ListBoxColumn colemer = new ListBoxColumn();
            colemer.FieldName = "PershkrimKonfigAmbjente";
            colemer.Caption = "Pershkrimi";
            combo.TextFormatString = "{0}";
            combo.Columns.Add(colprove);
            combo.Columns.Add(colemer);
            combo.DataSource = col;
            combo.ValueField = "IdKonfigAmbjente";
            combo.IncrementalFilteringMode = DevExpress.Web.IncrementalFilteringMode.Contains;
            combo.DataBind();
        }

        /// <summary>
        /// Mbush griden e transportuesit dhe te popupit me te dhena  
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="lupe"></param>
        private void mbushGridTransportuesNgaDB(int idNdermarrje, bool lupe)
        {
            DataTable dt = DbCore.DbInventari.colTransportues.merrTransportuesSipasNdermarrjes(idNdermarrje, lupe);
            DbCore.mySessionObjects.ruajGrideNeSession(Session, dt);
            ASPxGridView_Transportues.DataSource = dt;
            ASPxGridView_Transportues.DataBind();
            dt.Dispose();
        }

        
        private void mbushGridTransportuesNgaSesioni(int idNdermarrje)
        {
            DataTable tmpObject;
            bool sukses = DbCore.mySessionObjects.merrGrideNgaSessioni(Session, out tmpObject);
            if (!sukses)
                mbushGridTransportuesNgaDB(idNdermarrje, false);
            else
            {
                ASPxGridView_Transportues.DataSource = tmpObject;
                ASPxGridView_Transportues.DataBind();
                tmpObject.Dispose();
            }
        }

        /// <summary>
        /// perdoret per te konfiguruar griden. Per te dhenat mbi kolonat qe do te shfaqen nga databaza si dhe percakton disa karakteristika te grides
        /// </summary>
        /// :  <see cref="GridUtil.percaktoVisibleColumns"/>
        /// :  <see cref="DbCore.clsFunksione.konfiguroGrideListeMadhe(grida,celesigrides)"/>
        /// <param name="idNdermarrje"></param>
        private void konfiguroGride(int idNdermarrje, int idPerdorues)
        {
            ASPxGridView_Transportues.Columns["#"].VisibleIndex = 0;
        }

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            int idPerdoruesi = (int)hfState["idPerdoruesi"];
            int idNdermarrje = (int)hfState["idNdermarrje"];
            int idViti = (int)hfState["idViti"];
            int idGjuha = (int)hfState["idGjuha"];
            percaktoTemplateMenu(idGjuha,idViti, idPerdoruesi, idNdermarrje, ASPxMenu1, (bool)hfState["lupe"]);
        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="idNderVit"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        private void percaktoTemplateMenu(int idGjuha, int idViti, int idPerdorues, int idNdermarrje, ASPxMenu aSPxMenu1, bool lupe)
        {
            bool meme = (bool)hfState["Meme"];
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, "Shto_Transportues.aspx", this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, hfShtimModifikim.Value == "modifikim" ? false : true, false, false, meme);
            aSPxMenu1.Items.FindByName("OK").Visible = lupe;
        }

        /// <summary>
        /// sherben per te ruajtur filtrin e zgjedhur
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida();
            filtri.FiltraKodi = cmbFiltra.Text;
            filtri.FiltraShenime = cmbFiltra.Text;
            filtri.FiltraUniversal = false;
            int idViti = (int)hfState["idViti"];
            int idNdermarrje = (int)hfState["idNdermarrje"];
            int idGjuha = (int)hfState["idGjuha"];
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuha, "ASPxGridView_Transportues", "Shto_Transportues.aspx", idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = ASPxGridView_Transportues.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("Emertimi", ASPxGridView_Transportues);
          
            int idPerdorues = (int)hfState["idPerdoruesi"];
            filtri.IdPerdoruesi = idPerdorues;
            filtri.IdNdermarje = idNdermarrje;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            filtri.IdStatusDok = 1;
            mesazh = filtri.ruaj();
            clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, "ASPxGridView_Transportues", int.Parse(cmbKonfigurimi.Value.ToString()), "Shto_Transportues.aspx");
            percaktoTemplateMenu(idGjuha,idViti, idPerdorues, idNdermarrje, ASPxMenu1, (bool)hfState["lupe"]);
            if (mesazh.Status == true)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            hfStatusi.Value = "true";
            cmbFiltra.Text = "";
        }

        /// <summary>
        /// fshin filtrin e zgjedhur dhe aplikuar mbi gride, fshirje nga DB ku ai eshte ruajtur, dhe jo pastrimi i grides nga aplikimi i filtrit
        /// per kete do sherbeje Clear Filter ne fund te grides
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            int idViti = (int)hfState["idViti"];
            int idNdermarrje = (int)hfState["idNdermarrje"];
            int idGjuha = (int)hfState["idGjuha"];
            int idPerdorues = (int)hfState["idPerdoruesi"];
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuha, "ASPxGridView_Transportues", "Shto_Transportues.aspx", idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = idPerdorues;
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();

                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, "ASPxGridView_Transportues", int.Parse(cmbKonfigurimi.Value.ToString()), "Shto_Transportues.aspx");
                percaktoTemplateMenu(idGjuha,idViti, idPerdorues, idNdermarrje, ASPxMenu1, (bool)hfState["lupe"]);
                konfiguroVleraFillestare(idNdermarrje, idGjuha, (int)hfState["idKonfigAmbjente"]);
                hfStatusi.Value = "true";
                this.ASPxGridView_Transportues.FilterExpression = String.Empty;
            }
        }

        /// <summary>
        /// perdoret per te trajtuar ngjarjet e butonave te menuse. Shkon ne faqen e modifikimit te artikujve kur perdoruesi klikon butonin modifiko
        /// ose ne faqen e shtimit te artikujve kur perdoruesi klikon butonin shto
        /// </summary>
        /// <param name="source"> derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {
            if (e.Item.Name == "Ruaj")
            {
                Page.Validate("entries");
                if (ASPxPageControl1.ActiveTabIndex == 1)
                    ruajTransportues();
                else
                    ruajOperator();
            }
        }

        private void ruajOperator()
        {
            if (!Page.IsValid)
            {
                hfStatusi.Value = "false";
                return;
            }

            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);

            try
            {
                clsMesazh mesazh;
                bool shtim = true;
                if (hfShtimModifikim.Value == "shtim")
                    shtim = true;
                else if (hfShtimModifikim.Value == "modifikim")
                    shtim = false;
                clsOperator operatori = new clsOperator(int.Parse(hfId.Value.ToString()), txtKodiOp.Text, txtEmriOp.Text, txtMbiemriOp.Text, cbAktivOp.Checked, 1, idNdermarrje, idPerdoruesi, shtim);
                if (hfShtimModifikim.Value == "shtim")
                    mesazh = operatori.Ruaj();
                else
                    mesazh = operatori.Modifiko();

                if (mesazh.Status)
                {
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                    hfStatusi.Value = "true";

                    if (hfShtimModifikim.Value == "shtim")
                        shtoTransportuesNeGrid(idNdermarrje, idPerdoruesi, operatori.IdOperator);
                    else
                        modifikoTransportuesNeGrid(idNdermarrje, idPerdoruesi, operatori.IdOperator);
                }
                else
                {
                    hfStatusi.Value = "false";
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                }
                ASPxPageControl1.ActiveTabIndex = 0;
            }
            catch (Exception ex)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgGabimRuajtje"], pnlMesazhi);
            }
        }

        private void ruajTransportues()
        {
            if (Page.IsValid == false)
                return;
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            int idPerdoruesi = (int)hfState["idPerdoruesi"];
            int idGjuha = (int)hfState["idGjuha"];
            int idNdermarrje = (int)hfState["idNdermarrje"];
            int idViti = (int)hfState["idViti"];
            DbCore.DbInventari.clsTransportues transportuesi;
            try
            {
                if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
                    transportuesi = krijoTransportues(idNdermarrje, idPerdoruesi, true);
                else transportuesi = krijoTransportues(idNdermarrje, idPerdoruesi, false);
            }
            catch (Exception e)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(e.Message);
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, e.Message, pnlMesazhi);
                hfStatusi.Value = "false";
                return;
            }
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, "Shto_Transportues.aspx");
            bool eshteShtim;
            if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
            {
                if (!tedrejtaInfo.DShtim)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("mesazhRaportNukKeniTeDrejta",cultinf), pnlMesazhi);
                    hfStatusi.Value = "false";
                    return;
                }
                mesazh = transportuesi.ruaj();
                eshteShtim = true;
            }
            else
            {
                if (!tedrejtaInfo.DMod)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("mesazhRaportNukKeniTeDrejta", cultinf), pnlMesazhi);
                    hfStatusi.Value = "false";
                    return;
                }
                mesazh = transportuesi.modifiko();
                eshteShtim = false;
            }
            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                hfStatusi.Value = "false";
            }
            else
            {
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                if (eshteShtim)
                    shtoTransportuesNeGrid(idNdermarrje, idPerdoruesi, transportuesi.IdTransportues);
                else //modifikim
                    modifikoTransportuesNeGrid(idNdermarrje, idPerdoruesi, transportuesi.IdTransportues);
                hfStatusi.Value = "true";
                konfiguroGride(idNdermarrje, idPerdoruesi);
                ASPxPageControl1.ActiveTabIndex = 0;
            }
        }

        private DbCore.DbInventari.clsTransportues krijoTransportues(int idNdermarrje, int idPerdorues, bool shtim)
        {
            int idTransportues = 0;
            if (!shtim)
                idTransportues = int.Parse(hfId.Value.ToString());

            DbCore.DbInventari.clsTransportues transNew = new DbCore.DbInventari.clsTransportues(idTransportues, txtTransportues.Text, txtNIPTTransp.Text, txtAdresaTransp.Text, txtTelTransp.Text, 1, idPerdorues, idNdermarrje, idPerdorues, DateTime.Now, DateTime.Now, shtim, txtTarga.Text, cbAktiv.Checked,cmbTipiId.Text);

            return transNew;
        }

        private void shtoTransportuesNeGrid(int idNdermarrje, int idPerdorues, int idTransportues)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (ASPxGridView_Transportues.DataSource != null)
            {
                DataTable dt = (DataTable)ASPxGridView_Transportues.DataSource;
                DataRow[] drs = dt.Select("IdTransportues = " + idTransportues);
                if (drs.Length > 0)
                    throw new Exception(rm.GetString("msgTransportuesiEkzistonNeGride", cultinf));
                DataRow newDr = DbCore.DbInventari.clsTransportues.ktheTransportuesSipasId(idTransportues);
                dt.ImportRow(newDr);
            }
            else mbushGridTransportuesNgaDB(idNdermarrje, false);
            konfiguroGride(idNdermarrje, idPerdorues);
        }

        private void modifikoTransportuesNeGrid(int idNdermarrje, int idPerdorues, int idTransportues)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (ASPxGridView_Transportues.DataSource != null)
            {
                DataTable dt = (DataTable)ASPxGridView_Transportues.DataSource;
                DataRow[] drs = dt.Select("IdTransportues = " + idTransportues);
                if (drs.Length > 1)
                    throw new Exception(rm.GetString("msgNdodhenDyTrasportuesMeTeNjejtenId", cultinf));
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                DataRow newDr = DbCore.DbInventari.clsTransportues.ktheTransportuesSipasId(idTransportues);
                object[] arr = newDr.ItemArray;
                dr.ItemArray = arr;
                if (!cbAktiv.Checked && (bool)hfState["lupe"] == true)
                {
                    hiqTransportuesNgaGrida(idNdermarrje, idTransportues);
                    hfTransportuesFshire.Value = idTransportues.ToString();
                }
            }
            else mbushGridTransportuesNgaDB(idNdermarrje, false);
            konfiguroGride(idNdermarrje, idPerdorues);
        }


        /// <summary>
        /// perdoret per te fshire rreshtat e zgjedhur te artikujve nqs perdoruesi konfirmon fshirjen
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            List<object> rreshtat;
            if (ASPxPageControl1.ActiveTabIndex == 0)
                rreshtat = ASPxGridView_Transportues.GetSelectedFieldValues(new string[]{ "IdTransportues", "Lloji", "Emertimi", "Kodi"});
            else
            {
                rreshtat = new List<object>();
                if (ASPxPageControl1.ActiveTabIndex == 1)
                    rreshtat.Add(new object[] { hfId.Value, "Transportues", txtTransportues.Text, txtKodiOp.Text });
                else
                    rreshtat.Add(new object[] { hfId.Value, "Operator", txtTransportues.Text, txtKodiOp.Text });
            }
            if (rreshtat.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgZgjidhniTePaktenNjeTransportues"], pnlMesazhi);
                return;
            }
            List<string> transportuesTeFshire = new List<string>(), transportuesTePaFshire = new List<string>();
            int idNdermarrje = (int)hfState["idNdermarrje"];
            int idPerdorues = (int)hfState["idPerdoruesi"];
            DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegjistrim = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
            DbCore.DbInventari.clsTransportues transp = new DbCore.DbInventari.clsTransportues();
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            clsMesazh mesazhi;
            clsOperator op = new clsOperator();
            for (int i = 0; i < rreshtat.Count; i++)
            {
                if (((object[])rreshtat[i])[1].ToString() == "Transportues") {
                    transp.IdTransportues = Convert.ToInt32(((object[])rreshtat[i])[0]);
                  
                    konf.mbushKonfigAmbjSipasKod(cmbKonfigurimi.Text, idNdermarrje);
                    bool lidhur = dbRegjistrim.eshteDokumentiILidhurCelje(transp.IdTransportues.ToString(), konf.IdNivel.ToString());
                    if (lidhur)
                    {
                        transportuesTePaFshire.Add(((object[])rreshtat[i])[2].ToString());
                        continue;
                    }
                    mesazhi = transp.fshi(idPerdorues);
                    if (mesazhi.Status)
                    {
                        hiqTransportuesNgaGrida(idNdermarrje, transp.IdTransportues);
                        transportuesTeFshire.Add(((object[])rreshtat[i])[2].ToString());
                        ASPxPageControl1.ActiveTabIndex = 0;
                        hfStatusi.Value = "true";
                    }
                }
                else
                {
                    op.IdOperator = Convert.ToInt32(((object[])rreshtat[i])[0]);
                    mesazhi = op.Fshi();
                    if (mesazhi.Status)
                    {
                        hiqTransportuesNgaGrida(idNdermarrje, op.IdOperator);
                        transportuesTeFshire.Add(((object[])rreshtat[i])[3].ToString());
                        ASPxPageControl1.ActiveTabIndex = 0;
                        hfStatusi.Value = "true";
                    }
                }
               
            }
            dbRegjistrim.Dispose();
            string mesazhInfoGabim = "", mesazhInfoSukses = "";
            if (transportuesTePaFshire.Count == 1) 
                mesazhInfoGabim = String.Format("{0}{1}{2}", MessagesResource.Messages["msgTransportuesiMeEmertim"], String.Join(";", transportuesTePaFshire), MessagesResource.Messages["msgBlerjeShitjeNukFshihetNjejes"]);
            else
                if (transportuesTePaFshire.Count > 1)
                    mesazhInfoGabim = String.Format("{0}{1}{2}", MessagesResource.Messages["msgTransportuesitMeEmertime"], String.Join(";", transportuesTePaFshire), MessagesResource.Messages["msgBlerjeShitjeNukFshihetShumes"]);
            if (transportuesTeFshire.Count == 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", MessagesResource.Messages["msgTransportuesiMeEmertim"], String.Join(";", transportuesTeFshire), MessagesResource.Messages["msgCeljeMagazinatSuffixNjejesSuksesi"]);
            else
                if (transportuesTeFshire.Count > 1)
                    mesazhInfoSukses = String.Format("{0}{1}{2}", MessagesResource.Messages["msgTransportuesitMeEmertime"], String.Join(";", transportuesTeFshire), MessagesResource.Messages["msgCeljeMagazinatSuffixShumesSuksesi"]);
            if (mesazhInfoGabim != "" && mesazhInfoSukses != "")
                mesazhInfoGabim += MessagesResource.Messages["msgLidhesMesazhi"] + mesazhInfoSukses;
            if (mesazhInfoGabim != "")
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhInfoGabim, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazhInfoSukses, pnlMesazhi);
            pnlMesazhi.Update();
        }

        private void hiqTransportuesNgaGrida(int idNdermarrje, int idTransportues)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (ASPxGridView_Transportues.DataSource != null)
            {
                DataTable dt = (DataTable)ASPxGridView_Transportues.DataSource;
                DataRow[] drs = dt.Select("IdTransportues = " + idTransportues);
                if (drs.Length > 1)
                    throw new Exception(rm.GetString("msgNdodhenDyTrasportuesMeTeNjejtenId", cultinf));
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                dt.Rows.Remove(dr);
                ASPxGridView_Transportues.DataBind();
            }
            else
                mbushGridTransportuesNgaDB(idNdermarrje, false);
        }

        protected void RuajKolona_Click(object sender, EventArgs e)
        {
            int idViti = (int)hfState["idViti"];
            int idndermarrje = (int)hfState["idNdermarrje"];
            int idgjuha = (int)hfState["idGjuha"];
            int idperdorues = (int)hfState["idPerdoruesi"];
            int idfiltri = 0;
            int idkonf = DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(cmbKonfigurimi.Text.Split(';')[0], idndermarrje);
            DbCore.clsMesazh mesazh = GridUtil.ruajFiltra(idndermarrje, idperdorues, idgjuha, "ASPxGridView_Transportues", "Shto_Transportues.aspx", "FilterDefault", ASPxGridView_Transportues.FilterExpression, ASPxGridView_Transportues, "Emertimi", idkonf, out idfiltri);//ruan filtrin e zgjedhur tek filtrat
            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                return;
            }
            mesazh = GridUtil.ruajkonfigurimgride(ASPxGridView_Transportues, cmbKonfigurimi.Text, idndermarrje, idperdorues, 913, idfiltri, idViti, DbCore.mySessionObjects.ktheCultureInfo(Session), DbCore.mySessionObjects.ktheGjuhe(Session));/// ruan konfigurimin e grides dhe filtrin

            clsToolbarConfig.mbushComboBoxFiltra(idgjuha, idndermarrje, "ASPxGridView_Transportues", int.Parse(cmbKonfigurimi.Value.ToString()), "Shto_Transportues.aspx");

            percaktoTemplateMenu(idgjuha, idViti, idperdorues, idndermarrje, ASPxMenu1, (bool)hfState["lupe"]);
            if (mesazh.Status) clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
        }

        protected void btnXlsxExport_Click(object sender, EventArgs e)
        {
            try
            {
                gridExport.WriteXlsxToResponse("Transportues", true);
            }
            catch (Exception err)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
            }
        }

        protected void btnPdfExport_Click(object sender, EventArgs e)
        {
            try
            {
                gridExport.WritePdfToResponse("Transportues", true);
            }
            catch (Exception err)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
            }
        }

        /// <summary>
        /// perdoret ne rastet kur grida ben callback per ta rimbushur griden
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void ASPxGridView_Transportues_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            if (e.CallbackName == "COLUMNMOVE" && ASPxGridView_Transportues.AllColumns[int.Parse(e.Args[0])].Width.Value == 0)
                ASPxGridView_Transportues.AllColumns[int.Parse(e.Args[0])].Width = Unit.Percentage(3);
            if (e.CallbackName == "APPLYFILTER" && e.Args[0] == "")
            {
                DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
                ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
                cmbFiltra.Text = "";
            }
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            GridUtil.ToolTipButonaveMbiGride(ASPxGridView_Transportues, cultinf, rm);
        }

        /// <summary>
        /// perdoret per te vendosur filtrat tek headeri i grides
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxGridView_Transportues_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            string TeGjithe = rm.GetString("GridHeaderFilterFillItemTeGjithe", ci);
            string nga = rm.GetString("GridHeaderFilterFillItemNga", ci);
            if (e.Column.FieldName == "Emertimi" || e.Column.FieldName == "Nipt" || e.Column.FieldName == "Adresa" || e.Column.FieldName == "Tel" || e.Column.FieldName == "Targa")
            {
                e.Values.Clear();
                //e.AddShowAll();
                e.AddValue(TeGjithe, string.Empty, "true");
                e.AddValue(nga + " A-D ", string.Empty, e.Column.FieldName + ">'A     ' and " + e.Column.FieldName + " <'DDDDDDD'");
                e.AddValue(nga + " D-G ", string.Empty, e.Column.FieldName + ">'D     ' and " + e.Column.FieldName + "<'GGGGGGG'");
                e.AddValue(nga + " H-K ", string.Empty, e.Column.FieldName + ">'H     ' and " + e.Column.FieldName + "<'KKKKKKK'");
                e.AddValue(nga + " L-O ", string.Empty, e.Column.FieldName + ">'L     ' and " + e.Column.FieldName + "  <'OOOOOOO'");
                e.AddValue(nga + " P-S ", string.Empty, e.Column.FieldName + ">'P     ' and " + e.Column.FieldName + "<'SSSSSSS'");
                e.AddValue(nga + " T-W ", string.Empty, e.Column.FieldName + ">'T     ' and " + e.Column.FieldName + "<'WWWWWWW'");
                e.AddValue(nga + " X-Z ", string.Empty, e.Column.FieldName + ">'X     ' and " + e.Column.FieldName + "<'ZZZZZZZ'");
            }
            else
            {
                e.Values.Clear();
                e.AddValue(TeGjithe, string.Empty, "true");
            }
        }

        protected void ASPxGridView_Transportues_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string idkomponente = "";
            string kodkonfigurimi = "";
            int idNdermarrje = (int)hfState["idNdermarrje"];
            int idGjuhePerdoruesi = (int)hfState["idGjuha"];
            int idPerdoruesi = (int)hfState["idPerdoruesi"];
            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                kodkonfigurimi = arr[1];
                idkomponente = arr[0];
                if (arr[2] != "")
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuhePerdoruesi, "ASPxGridView_Transportues", "Shto_Transportues.aspx", idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], idNdermarrje, koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        ASPxGridView_Transportues.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, ASPxGridView_Transportues);
                        konfiguroVleraFillestare(idNdermarrje, idGjuhePerdoruesi, (int)hfState["idKonfigAmbjente"]);
                        hfStatusi.Value = "true";
                    }
                    else
                        hfStatusi.Value = "false";
                }
            }
            else
                if (arr.Length == 2)//nese eshte zgjedhur nje konfigurim tek combo e konfigurimeve
                {
                    kodkonfigurimi = arr[1];
                    idkomponente = arr[0];
                    GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "ASPxGridView_Transportues", ASPxGridView_Transportues, kodkonfigurimi, idkomponente.ToString(), idGjuhePerdoruesi);
                    konfiguroGride(idNdermarrje, idPerdoruesi);
            }
                else
                {
                    idkomponente = e.Parameters;
                }
            ASPxGridView_Transportues.Selection.UnselectAll();
        }

        protected void ASPxGridView_Transportues_CustomJSProperties(object sender, DevExpress.Web.ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = ASPxGridView_Transportues.PageIndex;
            e.Properties["cpPageRow"] = ASPxGridView_Transportues.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = ASPxGridView_Transportues.VisibleRowCount;
        }

        /// <summary>
        /// perdoret per te shtuar kolonen e selektimit tek grida dhe per te vendosur disa karakteristika te grides
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void ASPxGridView_Transportues_DataBound(object sender, EventArgs e)
        {// shton colonen # per selektim dhe disa karakteristika te grides
            if (this.ASPxGridView_Transportues.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
                //perzgjidh
                DevExpress.Web.GridViewCommandColumn check = new DevExpress.Web.GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true;
                check.Width = Unit.Percentage(2);
                check.VisibleIndex = 0;
                ASPxGridView_Transportues.Settings.ShowFilterRow = true;
                ASPxGridView_Transportues.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                ASPxGridView_Transportues.Settings.ShowFilterRowMenu = true;
                ASPxGridView_Transportues.Columns.Add(check);
                ASPxGridView_Transportues.KeyFieldName = "IdTransportues";
                ASPxGridView_Transportues.SettingsBehavior.AllowSelectByRowClick = true;
                ASPxGridView_Transportues.SettingsBehavior.AllowFocusedRow = true;
            }
        }
 
    }
}