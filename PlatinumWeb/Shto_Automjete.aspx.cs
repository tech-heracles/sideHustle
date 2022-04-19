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
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Types;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    public partial class Shto_Automjete : MyPageBase
    {
        private const string prefixMesazhNjejes = "Automjeti me targe: ";
        private const string prefixMesazhShumes = "Automjetet me targa: ";
        private const string suffixMesazhNjejesGabimi = " eshte i lidhur dhe nuk mund te fshihet";
        private const string suffixMesazhShumesGabimi = " jane te lidhur dhe nuk mund te fshihen";
        private const string suffixMesazhNjejesSuksesi = " u fshi me sukses!";
        private const string suffixMesazhShumesSuksesi = " u fshine me sukses!";
        private const string lidhesMesazhi = ". Kurse ";
        private const string mesazhZgjidhniNje = "Ju lutem zgjidhni te pakten nje automjet!";
        private string komponente => "Shto_Automjete.aspx";
        private string guidString;

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
                hfState.Set("Meme", DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
                hfState.Set("ModelIRi", false);
            }
            else
            {
                idPerdoruesi = (int)hfState["idPerdoruesi"];
                idGjuha = (int)hfState["idGjuha"];
                idNdermarrje = (int)hfState["idNdermarrje"];
                idViti = (int)hfState["idViti"];
                idNdermarrjeVit = (int)hfState["idNdermarrjeVit"];
            }

            if (!IsCallback)
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                    //FormsAuthentication.RedirectToLoginPage();
                    DbCore.clsFunksione.logout(Session, false, "", false);
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (!Page.IsPostBack)
            {
                guidString = Guid.NewGuid().ToString();
                hfState.Set("guidString", guidString);
                EmrateTabeve();
                konfiguroVleraFillestare(idNdermarrje, idGjuha);
                ASPxPageControl1.ActiveTabIndex = 0;
                mbushGridAutomjeteshNgaDB(idNdermarrje);
                konfiguroGride(idNdermarrje, idPerdoruesi);
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "ASPxGridView_Automjete", ASPxGridView_Automjete, cmbKonfigurimi.Text, Convert.ToString(404), (int)hfState["idGjuha"]);
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, komponente);
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
            }
            else
            {
                guidString = (string)hfState.Get("guidString");
                mbushGridAutomjeteshNgaSesioni(idNdermarrje);
                if ((bool)hfState["ModelIRi"])
                    konfiguroGride(idNdermarrje, idPerdoruesi);
                else
                    konfiguroGride(idNdermarrje, idPerdoruesi);
            }
            GridUtil.konfigGrideListeEMadhePaTheme(ASPxGridView_Automjete, "IdAutomjeti");
            percaktoTemplateMenu(idGjuha, idViti, idPerdoruesi, idNdermarrje, ASPxMenu1);
            clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, "ASPxGridView_Automjete", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
            GridUtil.ToolTipButonaveMbiGride(ASPxGridView_Automjete, cultinf, rm);
        }

        /// <summary>
        /// Vendos emrat e tabeve ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateTabeve()
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            ASPxPageControl1.TabPages[0].Text = rm.GetString("TePergjithshmeTab", cultinf);
            ASPxPageControl1.TabPages[1].Text = rm.GetString("labelAdministrimiInformacion", cultinf);
            hfState.Set("headerZgjidhKlientFurnitorin", rm.GetString("headerZgjidhKlientFurnitorin", cultinf));
            konfigurimi_Label.Text = rm.GetString("lblModeli", cultinf);
        }

        private void konfiguroVleraFillestare(int idNdermarrje, int idGjuha)
        { //mbush komboboxet dhe gridat e faqes
            int idPerdoruesi = (int)hfState["idPerdoruesi"];
            ConfigureAspxComboBox.ShtoKolonaPerKf(btneKlienti);
            ConfigureAspxComboBox.shtoKolonaPerModelAutomjeti(cmbModelAuto);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btneKlienti);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmbModelAuto);
            mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 81, "AUTO", idGjuha);
            cmbKonfigurimi.SelectedIndex = 0;
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()), idGjuha);
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


        private void mbushGridAutomjeteshNgaDB(int idNdermarrje)
        {//mbush griden e automjeteve me te dhena            
            DataTable dt = DbCore.DbInventari.colAutomjete.merrAutomjetetSipasNdermarrjes(idNdermarrje);
            DbCore.mySessionObjects.ruajGrideNeSession(Session, dt);
            ASPxGridView_Automjete.DataSource = dt;
            ASPxGridView_Automjete.DataBind();
            dt.Dispose();
        }

        private void mbushGridAutomjeteshNgaSesioni(int idNdermarrje)
        {
            DataTable tmpObject;
            bool sukses = DbCore.mySessionObjects.merrGrideNgaSessioni(Session, out tmpObject);
            if (!sukses)
                mbushGridAutomjeteshNgaDB(idNdermarrje);
            else
            {
                ASPxGridView_Automjete.DataSource = tmpObject;
                ASPxGridView_Automjete.DataBind();
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
            //Konfigurimi i grides            
            shtokolona(idNdermarrje, idPerdorues);
            ASPxGridView_Automjete.Columns["#"].VisibleIndex = 0;
        }

        private void shtokolona(int idNdermarrje, int idPerdorues)
        {
            //shtoModelAutomjeti(idNdermarrje, visibleIndex);
            GridUtil.shtoModelAutomjeti(this.ASPxGridView_Automjete, idNdermarrje, Session);
            KonfigurimComboGride.shtoKlientLinear(ASPxGridView_Automjete, idNdermarrje, idPerdorues, Session, komponente, guidString, "IdKlienti");
        }

        /// <summary>
        /// perdoret per te shfaqur kodet e modelit te automjetit ne vend te id si dhe filtri i modelit te auto te shfaqet ne forme komboje
        /// </summary>       
        /// <param name="idNdermarrje"></param>
        private void shtoModelAutomjeti(int idNdermarrje, bool visibleIndex)
        {
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            //if (typeof(GridViewDataComboBoxColumn) != ASPxGridView_Automjete.Columns["ModelAutomjeti"].GetType())
            //{
            int indexi = ASPxGridView_Automjete.Columns["ModelAutomjeti"].VisibleIndex;
            ASPxGridView_Automjete.Columns.Remove(ASPxGridView_Automjete.Columns["ModelAutomjeti"]);
            ASPxGridView_Automjete.Columns.Add(colnew);
            if (visibleIndex)
                colnew.VisibleIndex = indexi;
            DbCore.DbInventari.colModeleAutomjetesh modelet = new DbCore.DbInventari.colModeleAutomjetesh();
            modelet.Add(new DbCore.DbInventari.clsModelAutomjeti(0, "", "", 0, 0, 0, 0));
            modelet.mbushModeleAutomjeteshSipasNdermarrjes(idNdermarrje);
            colnew.PropertiesComboBox.DataSource = modelet;
            colnew.PropertiesComboBox.TextField = "KodModelAutomjeti";
            colnew.PropertiesComboBox.ValueField = "IdModelAutomjeti";
            colnew.FieldName = "ModelAutomjeti";

            colnew.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            //DbCore.mySessionObjects.ruajDsComboGrideNeSession(Session, modelet, "colModelet");
            //}
            //else
            //{
            //    colnew = (GridViewDataComboBoxColumn)ASPxGridView_Automjete.Columns["ModelAutomjeti"];
            //    if (colnew.PropertiesComboBox.Items.Count == 0)
            //    {
            //        colnew.PropertiesComboBox.DataSource = DbCore.mySessionObjects.merrDsComboGrideNeSession(Session, "colModelet");
            //    }
            //}
        }

        /// <summary>
        /// perdoret per te shfaqur kodet e klienteve ne vend te id si dhe filtri i klientit te shfaqet ne forme komboje
        /// </summary>        
        /// <param name="idNdermarrje"></param>
        //private void shtoKlient(int idNdermarrje, int idPerdorues)
        //{
        //    GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
        //    if (typeof(GridViewDataComboBoxColumn) != ASPxGridView_Automjete.Columns["IdKlienti"].GetType())
        //    {
        //        ASPxGridView_Automjete.Columns.Remove(ASPxGridView_Automjete.Columns["IdKlienti"]);
        //        ASPxGridView_Automjete.Columns.Add(colnew);
        //        DbCore.DbKontabiliteti.colKlienteFurnitore klientet = new DbCore.DbKontabiliteti.colKlienteFurnitore();
        //        klientet.Add(new DbCore.DbKontabiliteti.clsKlientFurnitor(0, "", 0, false, 0, "", "", "", "", 0, "", "", "", "", "", "", "", "", false, 0, 0, 0, 0, 0, 0, 0, 0, 0, "", "", false, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "", "", 0, "", 0, 0, 0, "", 0, 0, 0, 0, false, "", 0, false, false, false, false, 0, 0, false, "", "", new DbCore.DbKontabiliteti.colMarreveshjetPerKlient(),0));
        //        klientet.mbushKlienteFurnitoreNdermarrjes(idNdermarrje);
        //        colnew.PropertiesComboBox.DataSource = klientet;
        //        colnew.PropertiesComboBox.TextField = "KodKlientFurnitor";
        //        colnew.PropertiesComboBox.ValueField = "IdKlientFurnitor";
        //        colnew.FieldName = "IdKlienti";
        //        colnew.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        //        DbCore.mySessionObjects.ruajDsComboGrideNeSession(Session, klientet, "colKlient");
        //    }
        //    else
        //    {
        //        colnew = (GridViewDataComboBoxColumn)ASPxGridView_Automjete.Columns["IdKlienti"];
        //        if (colnew.PropertiesComboBox.Items.Count == 0)
        //        {
        //            colnew.PropertiesComboBox.DataSource = DbCore.mySessionObjects.merrDsComboGrideNeSession(Session, "colKlient");
        //        }
        //    }
        //}
        
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
            percaktoTemplateMenu(idGjuha, idViti, idPerdoruesi, idNdermarrje, ASPxMenu1);
        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="idNderVit"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        private void percaktoTemplateMenu(int idGjuha, int idViti, int idPerdorues, int idNdermarrje, ASPxMenu aSPxMenu1)
        {
            bool meme = (bool)hfState["Meme"];
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, komponente, this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, hfShtimModifikim.Value == "modifikim" ? false : true, false, false, meme);
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
            int idGjuhePerdoruesi = (int)hfState["idGjuha"];
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuhePerdoruesi, "ASPxGridView_Automjete", komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = ASPxGridView_Automjete.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("Targa", ASPxGridView_Automjete);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = ASPxGridView_Automjete.GetSortedColumns();
            //if (kolona.Count > 0)
            //{
            //    filtri.KoloneRenditje = kolona[0].FieldName;
            //    if (kolona[0].SortOrder == DevExpress.Data.ColumnSortOrder.Ascending)
            //        filtri.DrejtimRenditje = true;
            //    else
            //        filtri.DrejtimRenditje = false;
            //}
            //else
            //{
            //    filtri.KoloneRenditje = "Targa";
            //    filtri.DrejtimRenditje = true;
            //}
            int idPerdoruesi = (int)hfState["idPerdoruesi"];
            filtri.IdPerdoruesi = idPerdoruesi;
            filtri.IdNdermarje = idNdermarrje;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            filtri.IdStatusDok = 1;
            mesazh = filtri.ruaj();
            clsToolbarConfig.mbushComboBoxFiltra(idGjuhePerdoruesi, idNdermarrje, "ASPxGridView_Automjete", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
            percaktoTemplateMenu(idGjuhePerdoruesi, idViti, idPerdoruesi, idNdermarrje, ASPxMenu1);
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
            int idGjuhePerdoruesi = (int)hfState["idGjuha"];
            int idPerdorues = (int)hfState["idPerdoruesi"];
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuhePerdoruesi, "ASPxGridView_Automjete", komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
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
                clsToolbarConfig.mbushComboBoxFiltra(idGjuhePerdoruesi, idNdermarrje, "ASPxGridView_Automjete", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
                percaktoTemplateMenu(idGjuhePerdoruesi, idViti, idPerdorues, idNdermarrje, ASPxMenu1);
                konfiguroVleraFillestare(idNdermarrje, idGjuhePerdoruesi);
                hfStatusi.Value = "true";
                this.ASPxGridView_Automjete.FilterExpression = String.Empty;
            }
        }


        /// <summary>
        /// perdoret per te trajtuar ngjarjet e butonave te menuse. Shkon ne faqen e modifikimit te artikujve kur perdoruesi klikon butonin modifiko
        /// ose ne faqen e shtimit te artikujve kur perdoruesi klikon butonin shto
        /// </summary>
        /// <param name="source"> derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {//veprimet e menuse
            if (e.Item.Name == "Ruaj")
            {
                Page.Validate("entries");
                ruajAutomjet();
            }
        }

        private void ruajAutomjet()
        {
            if (Page.IsValid == false)
                return;
            int idPerdoruesi = (int)hfState["idPerdoruesi"];
            int idGjuha = (int)hfState["idGjuha"];
            int idNdermarrje = (int)hfState["idNdermarrje"];
            int idViti = (int)hfState["idViti"];
            DbCore.DbInventari.clsAutomjete automjeti;
            try
            {
                if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
                    automjeti = krijoAutomjet(idNdermarrje, idPerdoruesi, true);
                else automjeti = krijoAutomjet(idNdermarrje, idPerdoruesi, false);
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
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, komponente);
            bool eshteShtim;
            if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
            {
                if (!tedrejtaInfo.DShtim)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                    hfStatusi.Value = "false";
                    return;
                }
                mesazh = automjeti.ruaj(hfNrAutoKF);
                eshteShtim = true;
            }
            else
            {
                if (!tedrejtaInfo.DMod)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                    hfStatusi.Value = "false";
                    return;
                }
                mesazh = automjeti.modifiko();
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
                    shtoAutomjetNeGrid(idNdermarrje, idPerdoruesi, automjeti.IdAutomjeti);
                else //modifikim
                    modifikoAutomjetNeGrid(idNdermarrje, idPerdoruesi, automjeti.IdAutomjeti);
                hfStatusi.Value = "true";
                konfiguroGride(idNdermarrje, idPerdoruesi);
                ASPxPageControl1.ActiveTabIndex = 0;
            }
        }

        private DbCore.DbInventari.clsAutomjete krijoAutomjet(int idNdermarrje, int idPerdorues, bool shtim)
        {
            var controls = this.GetAsPxTextEditIdValue();
            controls.AddRange(ASPxPageControl1.GetAsPxTextEditIdValue());

            hfNrAuto = (ASPxHiddenField)DbCore.DbAdmin.NrAuto.VendosVleratNrAuto(hfNrAuto, controls);
            //DbCore.DbAdmin.NrAuto.vendosVleratNrAuto(hfNrAuto, this, this.ASPxPageControl1, null, null);

            hfNrAutoKF = (ASPxHiddenField)DbCore.DbAdmin.NrAuto.ShtoNeRegjistrime(hfNrAutoKF, hfNrAuto, "txtNrShasie", "NrShasie");
            //DbCore.DbAdmin.NrAuto.shtoNeHfRegjistrime(hfNrAutoKF, hfNrAuto, "txtNrShasie", "NrShasie");

            int idAuto;
            if (shtim == true)
                idAuto = 0;
            else idAuto = int.Parse(hfId.Value.ToString());
            int idModelAuto;
            try
            {
                if (cmbModelAuto.Text != "")
                    idModelAuto = int.Parse(cmbModelAuto.Value.ToString());
                else idModelAuto = 0;
            }
            catch (Exception err)
            {
                string mesazhi = " Ju lutem plotesoni modelin e automjetit";
                NLog.LogManager.GetCurrentClassLogger().Error(mesazhi, err.Message);
                throw new DbCore.MyException(mesazhi);
            }
                int idVitProdhimi;
                try
            {
                if (txtVitProdhimi.Text != "")
                    idVitProdhimi = int.Parse(txtVitProdhimi.Text);
                else idVitProdhimi = 0;
            }
            catch (Exception err)
            {
                string mesazhi = "Viti i prodhimit nuk eshte i sakte!";
                NLog.LogManager.GetCurrentClassLogger().Error(mesazhi, err.Message);
                throw new DbCore.MyException(mesazhi);
            }
            double kilometra;
            try
            {
                if (txtKilometra.Text != "")
                    kilometra = double.Parse(txtKilometra.Text);
                else kilometra = 0;
            }
            catch (Exception err)
            {
                string mesazhi = "Numri i kilometrave nuk eshte i sakte!";
                NLog.LogManager.GetCurrentClassLogger().Error(mesazhi, err.Message);
                throw new DbCore.MyException(mesazhi);
            }
            int idKlienti;
            try
            {
                if (btneKlienti.Text != "")
                    idKlienti = int.Parse(btneKlienti.Value.ToString());
                else idKlienti = 0;
            }
            catch (Exception err)
            {
                string mesazhi = " Ju lutemi plotesoni klientit!";
                NLog.LogManager.GetCurrentClassLogger().Error(mesazhi, err.Message);
                throw new DbCore.MyException(mesazhi);
            }

            DbCore.DbInventari.clsAutomjete autoNew = new DbCore.DbInventari.clsAutomjete(idAuto, DbCore.clsFunksione.ktheStringunPaHapesira(txtNrShasie.Text, true), txtTarga.Text, idModelAuto, idVitProdhimi, kilometra, txtKodMotorri.Text, 1, idKlienti, idPerdorues, idNdermarrje, idPerdorues, DateTime.Now, DateTime.Now, btneKlienti.Text, cmbModelAuto.Text, shtim, txtMarka.Text, rm, ci);
            return autoNew;
        }

        private void shtoAutomjetNeGrid(int idNdermarrje, int idPerdorues, int idAuto)
        {
            if (ASPxGridView_Automjete.DataSource != null)
            {
                DataTable dt = (DataTable)ASPxGridView_Automjete.DataSource;
                DataRow[] drs = dt.Select("IdAutomjeti = " + idAuto);
                if (drs.Length > 0)
                    throw new Exception("GABIM: Automjeti ekziston ne gride!");
                DataRow newDr = DbCore.DbInventari.clsAutomjete.ktheAutomjetSipasId(idAuto);
                dt.ImportRow(newDr);
            }
            else mbushGridAutomjeteshNgaDB(idNdermarrje);
            konfiguroGride(idNdermarrje, idPerdorues);
        }

        private void modifikoAutomjetNeGrid(int idNdermarrje, int idPerdorues, int idAuto)
        {
            if (ASPxGridView_Automjete.DataSource != null)
            {
                DataTable dt = (DataTable)ASPxGridView_Automjete.DataSource;
                DataRow[] drs = dt.Select("IdAutomjeti = " + idAuto);
                if (drs.Length > 1)
                    throw new Exception("GABIM: Ndodhen 2 automjete me te njejten id ne gride!");
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                DataRow newDr = DbCore.DbInventari.clsAutomjete.ktheAutomjetSipasId(idAuto);
                object[] arr = newDr.ItemArray;
                dr.ItemArray = arr;
            }
            else mbushGridAutomjeteshNgaDB(idNdermarrje);
            konfiguroGride(idNdermarrje, idPerdorues);
        }


        /// <summary>
        /// perdoret per te fshire rreshtat e zgjedhur te artikujve nqs perdoruesi konfirmon fshirjen
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ButtonOk_Click2(object sender, EventArgs e)
        {//fshirja e automjetit
            List<object> rreshtat;
            if (ASPxPageControl1.ActiveTabIndex == 0)//nqs kemi zgjedhur vetem nje kartele per te pare fshijme vetem ate
                rreshtat = ASPxGridView_Automjete.GetSelectedFieldValues("IdAutomjeti");
            else
            {
                rreshtat = new List<object>();
                rreshtat.Add(hfId.Value);
            }
           // List<object> rreshtat = ASPxGridView_Automjete.GetSelectedFieldValues("IdAutomjeti");
            if (rreshtat.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhZgjidhniNje, pnlMesazhi);
                return;
            }
            List<string> automjeteTeFshire = new List<string>(), automjeteTePaFshire = new List<string>();
            int idNdermarrje = (int)hfState["idNdermarrje"];
            int idPerdorues = (int)hfState["idPerdoruesi"];
            DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegjistrim = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
            DbCore.DbInventari.clsAutomjete auto = new DbCore.DbInventari.clsAutomjete();
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            for (int i = 0; i < rreshtat.Count; i++)
            {
                auto.mbushAutomjet(Convert.ToInt32(rreshtat[i]));
                if (auto.IdAutomjeti == 0)
                    continue;

                //konf.mbushKonfigAmbjSipasId(auto.IdAutomjeti);
                konf.mbushKonfigAmbjSipasKod(cmbKonfigurimi.Text, idNdermarrje);
                bool lidhur = dbRegjistrim.eshteDokumentiILidhurCelje(auto.IdAutomjeti.ToString(), konf.IdNivel.ToString());
                if (lidhur)
                {
                    automjeteTePaFshire.Add(auto.Targa);
                    continue;
                }
                DbCore.clsMesazh mesazhi = auto.fshi(idPerdorues);
                if (mesazhi.Status)
                {
                    #region Heq automjetin nga grida
                    hiqAutomjetNgaGrida(idNdermarrje, auto.IdAutomjeti);
                    #endregion
                    automjeteTeFshire.Add(auto.Targa);

                    ASPxPageControl1.ActiveTabIndex = 0;
                    hfStatusi.Value = "true";
                }
            }
            dbRegjistrim.Dispose();
            string mesazhInfoGabim = "", mesazhInfoSukses = "";
            if (automjeteTePaFshire.Count == 1)
                mesazhInfoGabim = String.Format("{0}{1}{2}", prefixMesazhNjejes, String.Join(";", automjeteTePaFshire), suffixMesazhNjejesGabimi);
            else
                if (automjeteTePaFshire.Count > 1)
                    mesazhInfoGabim = String.Format("{0}{1}{2}", prefixMesazhShumes, String.Join(";", automjeteTePaFshire), suffixMesazhShumesGabimi);
            if (automjeteTeFshire.Count == 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", prefixMesazhNjejes, String.Join(";", automjeteTeFshire), suffixMesazhNjejesSuksesi);
            else
                if (automjeteTeFshire.Count > 1)
                    mesazhInfoSukses = String.Format("{0}{1}{2}", prefixMesazhShumes, String.Join(";", automjeteTeFshire), suffixMesazhShumesSuksesi);
            if (mesazhInfoGabim != "" && mesazhInfoSukses != "")
                mesazhInfoGabim += lidhesMesazhi + mesazhInfoSukses;
            if (mesazhInfoGabim != "")
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhInfoGabim, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazhInfoSukses, pnlMesazhi);
            pnlMesazhi.Update();
        }

        private void hiqAutomjetNgaGrida(int idNdermarrje, int idAuto)
        {
            if (ASPxGridView_Automjete.DataSource != null)
            {
                DataTable dt = (DataTable)ASPxGridView_Automjete.DataSource;
                DataRow[] drs = dt.Select("IdAutomjeti = " + idAuto);
                if (drs.Length > 1)
                    throw new Exception("GABIM: Ndodhen 2 automjete me te njejten id ne gride");
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                dt.Rows.Remove(dr);
                ASPxGridView_Automjete.DataBind();
            }
            else
                mbushGridAutomjeteshNgaDB(idNdermarrje);
        }

        protected void RuajKolona_Click(object sender, EventArgs e)
        {
            int idViti = (int)hfState["idViti"];
            int idndermarrje = (int)hfState["idNdermarrje"];
            int idgjuha = (int)hfState["idGjuha"];
            int idperdorues = (int)hfState["idPerdoruesi"];
            int idfiltri = 0;
            int idkonf = DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(cmbKonfigurimi.Text.Split(';')[0], idndermarrje);
            DbCore.clsMesazh mesazh = GridUtil.ruajFiltra(idndermarrje, idperdorues, idgjuha, "ASPxGridView_Automjete", komponente, "FilterDefault", ASPxGridView_Automjete.FilterExpression, ASPxGridView_Automjete, "Targa", idkonf, out idfiltri);//ruan filtrin e zgjedhur tek filtrat
            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                return;

            }
            mesazh = GridUtil.ruajkonfigurimgride(ASPxGridView_Automjete, cmbKonfigurimi.Text, idndermarrje, idperdorues, 404, idfiltri, idViti, DbCore.mySessionObjects.ktheCultureInfo(Session), DbCore.mySessionObjects.ktheGjuhe(Session));/// ruan konfigurimin e grides dhe filtrin

            clsToolbarConfig.mbushComboBoxFiltra(idgjuha, idndermarrje, "ASPxGridView_Automjete", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);

            percaktoTemplateMenu(idgjuha, idViti, idperdorues, idndermarrje, ASPxMenu1);
            if (mesazh.Status) clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

        }

        protected void btnXlsxExport_Click(object sender, EventArgs e)
        {
            try
            {
                gridExport.WriteXlsxToResponse("Automjetet", true);
            }
            catch (Exception)
            {
            }
        }

        protected void btnPdfExport_Click(object sender, EventArgs e)
        {
            try
            {
                gridExport.WritePdfToResponse("Automjetet", true);
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// perdoret ne rastet kur grida ben callback per ta rimbushur griden
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void ASPxGridView_Automjete_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            if (e.CallbackName == "COLUMNMOVE" && ASPxGridView_Automjete.AllColumns[int.Parse(e.Args[0])].Width.Value == 0)
                ASPxGridView_Automjete.AllColumns[int.Parse(e.Args[0])].Width = Unit.Percentage(3);
            if (e.CallbackName == "APPLYFILTER" && e.Args[0] == "")
            {
                DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
                ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
                cmbFiltra.Text = "";
            }
        }

        /// <summary>
        /// perdoret per te vendosur filtrat tek headeri i grides
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxGridView_Automjete_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            if (e.Column.FieldName == "Klienti" || e.Column.FieldName == "Targa" || e.Column.FieldName == "ModelAutomjeti")
            {
                e.Values.Clear();
                e.AddValue("(Te gjithe)", string.Empty, "true");
                e.AddValue("Nga A-D ", string.Empty, e.Column.FieldName + ">'A     ' and " + e.Column.FieldName + " <'DDDDDDD'");
                e.AddValue("Nga D-G ", string.Empty, e.Column.FieldName + ">'D     ' and " + e.Column.FieldName + "<'GGGGGGG'");
                e.AddValue("Nga H-K ", string.Empty, e.Column.FieldName + ">'H     ' and " + e.Column.FieldName + "<'KKKKKKK'");
                e.AddValue("Nga L-O ", string.Empty, e.Column.FieldName + ">'L     ' and " + e.Column.FieldName + "  <'OOOOOOO'");
                e.AddValue("Nga P-S ", string.Empty, e.Column.FieldName + ">'P     ' and " + e.Column.FieldName + "<'SSSSSSS'");
                e.AddValue("Nga T-W ", string.Empty, e.Column.FieldName + ">'T     ' and " + e.Column.FieldName + "<'WWWWWWW'");
                e.AddValue("Nga X-Z ", string.Empty, e.Column.FieldName + ">'X     ' and " + e.Column.FieldName + "<'ZZZZZZZ'");
            }
            else
            {
                e.Values.Clear();
                e.AddValue("(Te gjithe)", string.Empty, "true");
            }
        }

        protected void ASPxGridView_Automjete_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string idkomponente = "";
            string kodkonfigurimi = "";
            int idNdermarrje = (int)hfState["idNdermarrje"];
            int idGjuhePerdoruesi = (int)hfState["idGjuha"];
            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                kodkonfigurimi = arr[1];
                idkomponente = arr[0];
                if (arr[2] != "")
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuhePerdoruesi, "ASPxGridView_Automjete", komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], idNdermarrje, koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        ASPxGridView_Automjete.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, ASPxGridView_Automjete);
                        konfiguroVleraFillestare(idNdermarrje, idGjuhePerdoruesi);
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
                    idkomponente = arr[0]; //konfiguroGride(idNdermarrje, kodkonfigurimi, Convert.ToInt32(idkomponente));
                }
                else
                {
                    idkomponente = e.Parameters;
                }
            ASPxGridView_Automjete.Selection.UnselectAll();
        }

        protected void ASPxGridView_Automjete_CustomJSProperties(object sender, DevExpress.Web.ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = ASPxGridView_Automjete.PageIndex;
            e.Properties["cpPageRow"] = ASPxGridView_Automjete.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = ASPxGridView_Automjete.VisibleRowCount;
        }

        /// <summary>
        /// perdoret per te shtuar kolonen e selektimit tek grida dhe per te vendosur disa karakteristika te grides
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void ASPxGridView_Automjete_DataBound(object sender, EventArgs e)
        {// shton colonen # per selektim dhe disa karakteristika te grides
            if (this.ASPxGridView_Automjete.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
                //perzgjidh
                DevExpress.Web.GridViewCommandColumn check = new DevExpress.Web.GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true;
                check.Width = Unit.Percentage(2);
                check.VisibleIndex = 0;
                ASPxGridView_Automjete.Settings.ShowFilterRow = true;
                ASPxGridView_Automjete.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                ASPxGridView_Automjete.Settings.ShowFilterRowMenu = true;
                ASPxGridView_Automjete.Columns.Add(check);
                ASPxGridView_Automjete.KeyFieldName = "IdAutomjeti";
                ASPxGridView_Automjete.SettingsBehavior.AllowSelectByRowClick = true;
                ASPxGridView_Automjete.SettingsBehavior.AllowFocusedRow = true;
            }
        }

        protected void ASPxGridView_Automjete_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if (e.Column.FieldName == "ModelAutomjeti" || e.Column.FieldName == "IdKlienti")
            {
                if (Converter.ConvertToInt(e.Value)==0 || Converter.ConvertToInt(e.Value) == -1)
                {
                    e.Criteria = null;
                }
            }
        }

        protected void btneKlienti_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("btneKlienti"))
                {
                    int value = 0;
                    if (e.Value == null || !Int32.TryParse(e.Value.ToString(), out value))
                        return;
                    int idPerdoruesi = (int)hfState["idPerdoruesi"];
                    int idNdermarrje = (int)hfState["idNdermarrje"];
                    ConfigureAspxComboBox.KonfiguroComboBoxKlientFurnitoriById(idPerdoruesi, idNdermarrje, (ASPxComboBox)source, value);
                }
            }
        }

        protected void btneKlienti_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("btneKlienti"))
                {
                    int idPerdoruesi = (int)hfState["idPerdoruesi"];
                    int idNdermarrje = (int)hfState["idNdermarrje"];
                    ConfigureAspxComboBox.KonfiguroComboBoxKlientFurnitor(e.Filter, e.BeginIndex + 1, e.EndIndex + 1, idPerdoruesi, idNdermarrje, btneKlienti, 1);
                }
            }
        }

        protected void cmbModelAuto_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("cmbModelAuto"))
                {
                    int value = 0;
                    if (e.Value == null || !Int32.TryParse(e.Value.ToString(), out value))
                        return;
                    int idPerdoruesi = (int)hfState["idPerdoruesi"];
                    int idNdermarrje = (int)hfState["idNdermarrje"];
                    ConfigureAspxComboBox.mbushComboModelAutomjetiByID((ASPxComboBox)source, value);
                }
            }
        }

        protected void cmbModelAuto_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("cmbModelAuto"))
                {
                    int idNdermarrje = (int)hfState["idNdermarrje"];
                    ConfigureAspxComboBox.mbushComboModelAutomjetesh(e.Filter, e.BeginIndex + 1, e.EndIndex + 1, idNdermarrje, cmbModelAuto);
                }
            }
        }
    }
}