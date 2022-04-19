using DbCore;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DbCore.IMBUtils.Types;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.Security;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    public partial class Pajisje : MyPageBase
    {
        private string komponente = "Pajisje.aspx";

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
                    DbCore.clsFunksione.logout(Session, true, "FaqePaautorizuar");
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
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            CultureInfo ci = mySessionObjects.ktheCultureInfo(Session);
            if (!Page.IsPostBack)
            {
                EmrateTabeve(ci, rm);
                konfiguroVleraFillestare(idNdermarrje, idGjuha);
                ASPxPageControl1.ActiveTabIndex = 0;
                mbushGridPajisjeshNgaDB(idNdermarrje);
                konfiguroGride(idNdermarrje, idPerdoruesi);
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "gvPajisje", gvPajisje, cmbKonfigurimi.Text, Convert.ToString(2030), (int)hfState["idGjuha"]);
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, komponente);
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
            }
            else
            {
                mbushGridPajisjeshNgaSesioni(idNdermarrje);
                konfiguroGride(idNdermarrje, idPerdoruesi);
            }
            GridUtil.konfigGrideListeEMadhePaTheme(gvPajisje, "IdPajisje");
            percaktoTemplateMenu(idGjuha, idViti, idPerdoruesi, idNdermarrje, ASPxMenu1);
            clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, "gvPajisje", int.Parse(cmbKonfigurimi.Value.ToString()),komponente);
            GridUtil.ToolTipButonaveMbiGride(gvPajisje, ci, rm);
        }

        /// <summary>
        /// Vendos emrat e tabeve ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateTabeve(CultureInfo cultinf,  ResourceManager rm)
        {
            ASPxPageControl1.TabPages[0].Text = rm.GetString("TePergjithshmeTab", cultinf);
            ASPxPageControl1.TabPages[1].Text = rm.GetString("labelAdministrimiInformacion", cultinf);
            hfState.Set("headerZgjidhKlientFurnitorin", rm.GetString("headerZgjidhKlientFurnitorin", cultinf));
            konfigurimi_Label.Text = rm.GetString("lblModeli", cultinf);
        }

        private void konfiguroVleraFillestare(int idNdermarrje, int idGjuha)
        { //mbush komboboxet dhe gridat e faqes
            int idPerdoruesi = (int)hfState["idPerdoruesi"];
            ConfigureAspxComboBox.ShtoKolonaPerKf(btneKlienti);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btneKlienti);
            ConfigureAspxComboBox.mbushComboLlojeKonvertimesh(cmbLlojKonvertimesh);
            mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 119, "PA", idGjuha);
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

        /// <summary>
        /// perdoret per te konfiguruar griden. Per te dhenat mbi kolonat qe do te shfaqen nga databaza si dhe percakton disa karakteristika te grides
        /// </summary>
        /// :  <see cref="GridUtil.percaktoVisibleColumns"/>
        /// :  <see cref="DbCore.clsFunksione.konfiguroGrideListeMadhe(grida,celesigrides)"/>
        /// <param name="idNdermarrje"></param>
        private void konfiguroGride(int idNdermarrje, int idPerdorues)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            CultureInfo ci = mySessionObjects.ktheCultureInfo(Session);

            KonfigurimComboGride.shtoAktivPoOseJo(gvPajisje, rm, ci, "Aktive");
            KonfigurimComboGride.shtoAktivPoOseJo(gvPajisje, rm, ci, "Regjistruar");

            gvPajisje.Columns["#"].VisibleIndex = 0;
        }

        private void shtoLlojKonvertimi()
        {
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            if (typeof(GridViewDataComboBoxColumn) != gvPajisje.Columns["LlojKonvertimi"].GetType())
            {
                gvPajisje.Columns.Remove(gvPajisje.Columns["LlojKonvertimi"]);
                gvPajisje.Columns.Add(colnew);
                DataTable dt = DbCore.DbInventari.colPajisjet.merrLlojeKonvertimeshPerPajisje();
                dt.Rows.InsertAt(dt.NewRow(), 0);
                colnew.PropertiesComboBox.DataSource = dt;
                colnew.PropertiesComboBox.TextField = "Lloji";
                colnew.PropertiesComboBox.ValueField = "LlojiID";
                colnew.FieldName = "LlojKonvertimi";
                colnew.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                DbCore.mySessionObjects.ruajDsComboGrideNeSession(Session, dt, "llojKonvertimeshPajisje");
            }
            else
            {
                colnew = (GridViewDataComboBoxColumn)gvPajisje.Columns["LlojKonvertimi"];

                if (colnew.PropertiesComboBox.Items.Count == 0)
                {
                    colnew.PropertiesComboBox.DataSource = DbCore.mySessionObjects.merrDsComboGrideNeSession(Session, "llojKonvertimeshPajisje");
                }
            }
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
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuhePerdoruesi, "gvPajisje", komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvPajisje.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("Targa", gvPajisje);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvPajisje.GetSortedColumns();
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
            clsToolbarConfig.mbushComboBoxFiltra(idGjuhePerdoruesi, idNdermarrje, "gvPajisje", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
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
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuhePerdoruesi, "gvPajisje", komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
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
                clsToolbarConfig.mbushComboBoxFiltra(idGjuhePerdoruesi, idNdermarrje, "gvPajisje", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
                percaktoTemplateMenu(idGjuhePerdoruesi, idViti, idPerdorues, idNdermarrje, ASPxMenu1);
                konfiguroVleraFillestare(idNdermarrje, idGjuhePerdoruesi);
                hfStatusi.Value = "true";
                this.gvPajisje.FilterExpression = String.Empty;
            }
        }

        private void mbushGridPajisjeshNgaDB(int idNdermarrje)
        {//mbush griden e automjeteve me te dhena            
            DataTable dt = DbCore.DbInventari.colPajisjet.merrPajisjetSipasNdermarrjes(idNdermarrje);
            DbCore.mySessionObjects.ruajGrideNeSession(Session, dt);
            gvPajisje.DataSource = dt;
            gvPajisje.DataBind();
            dt.Dispose();
        }

        private void mbushGridPajisjeshNgaSesioni(int idNdermarrje)
        {
            DataTable tmpObject;
            bool sukses = DbCore.mySessionObjects.merrGrideNgaSessioni(Session, out tmpObject);
            if (!sukses)
                mbushGridPajisjeshNgaDB(idNdermarrje);
            else
            {
                gvPajisje.DataSource = tmpObject;
                gvPajisje.DataBind();
                tmpObject.Dispose();
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
                ruajPajisje();
            }
        }

        private void ruajPajisje()
        {
            if (Page.IsValid == false)
                return;
            int idPerdoruesi = (int)hfState["idPerdoruesi"];
            int idGjuha = (int)hfState["idGjuha"];
            int idNdermarrje = (int)hfState["idNdermarrje"];
            int idViti = (int)hfState["idViti"];

            DbCore.DbInventari.clsPajisje pajisja;
            bool ndryshuarPass, ndryshuarKlienti;
            try
            {   
                pajisja = krijoPajisje(out ndryshuarPass, out ndryshuarKlienti);
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

            if (!tedrejtaInfo.DMod)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                hfStatusi.Value = "false";
                return;
            }

            mesazh = pajisja.modifikoPajisjeDheDergoEmail(ndryshuarPass, txtFjalekalimi.Text, ndryshuarKlienti);

            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                hfStatusi.Value = "false";
            }
            else
            {
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                modifikoPajisjeNeGrid(idNdermarrje, idPerdoruesi, pajisja.IdPajisje);
                hfStatusi.Value = "true";
                konfiguroGride(idNdermarrje, idPerdoruesi);
                ASPxPageControl1.ActiveTabIndex = 0;
            }
        }

        private DbCore.DbInventari.clsPajisje krijoPajisje(out bool ndryshuarFjalekalimi, out bool ndryshuarKlienti)
        {
            ndryshuarFjalekalimi = false; ndryshuarKlienti = false;
            DbCore.DbInventari.clsPajisje pajisja = new DbCore.DbInventari.clsPajisje(int.Parse(hfId.Value.ToString()));
            pajisja.Aktive = cbAktiv.Checked;
            pajisja.Regjistruar = cbAprovuar.Checked;

            double koeficentFitimi, koeficentKonsumi;
            if (!Double.TryParse(txtKoeficentFitimi.Text, out koeficentFitimi))
                throw new DbCore.MyException("Koeficenti i fitimit nuk eshte numer!");
            if (koeficentFitimi < 1)
                throw new DbCore.MyException("Koeficenti i fitimit duhet te jete me i madh se 1!");
            pajisja.KoeficentFitimi = koeficentFitimi;

            if (!Double.TryParse(txtKoeficentKonsumi.Text, out koeficentKonsumi))
                throw new DbCore.MyException("Koeficenti i konsumit nuk eshte numer!");
            if (koeficentKonsumi < 1)
                throw new DbCore.MyException("Koeficenti i konsumit duhet te jete me i madh se 1!");
            pajisja.KoeficentKonsumi = koeficentKonsumi;
            pajisja.IdPerdorues = (int)hfState["idPerdoruesi"];
            if (!String.IsNullOrEmpty(txtFjalekalimi.Text))
            {
                string fjalekalimiIRi = PasswordHelper.HashLogin(pajisja.Emertimi, txtFjalekalimi.Text);
                if (pajisja.Fjalekalimi != fjalekalimiIRi)
                {
                    //TODO send email
                    string oldPass = pajisja.Fjalekalimi;
                    ndryshuarFjalekalimi = true;
                }
                else
                    ndryshuarFjalekalimi = false;

                pajisja.Fjalekalimi = fjalekalimiIRi;
            }

            int idKlienti = 0, idLlojKonvertimi = 0;
            if (!int.TryParse(btneKlienti.Value.ToString(), out idKlienti))
                throw new DbCore.MyException("Klienti nuk ekziston!");

            if (pajisja.IdKlienti != idKlienti)
                ndryshuarKlienti = true;
            else
                ndryshuarKlienti = false;

            pajisja.IdKlienti = idKlienti;

            //if (!int.TryParse(cmbLlojKonvertimesh.Value.ToString(), out idLlojKonvertimi))
            //    throw new DbCore.MyException("Lloji i konvertimit nuk ekziston!");
            pajisja.IdLlojKonvertimi = 0;
            return pajisja;
        }

        private void modifikoPajisjeNeGrid(int idNdermarrje, int idPerdorues, int idPajisje)
        {
            if (gvPajisje.DataSource != null)
            {
                DataTable dt = (DataTable)gvPajisje.DataSource;
                DataRow[] drs = dt.Select("IdPajisje = " + idPajisje);
                if (drs.Length > 1)
                    throw new Exception("GABIM: Ndodhen 2 pajisje me te njejten id ne gride!");
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                DataRow newDr = DbCore.DbInventari.clsPajisje.kthePajisjeSipasId(idPajisje);
                object[] arr = newDr.ItemArray;
                dr.ItemArray = arr;
            }
            else mbushGridPajisjeshNgaDB(idNdermarrje);
            konfiguroGride(idNdermarrje, idPerdorues);
        }

        protected void RuajKolona_Click(object sender, EventArgs e)
        {
            int idViti = (int)hfState["idViti"];
            int idndermarrje = (int)hfState["idNdermarrje"];
            int idgjuha = (int)hfState["idGjuha"];
            int idperdorues = (int)hfState["idPerdoruesi"];
            int idfiltri = 0;
            int idkonf = DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(cmbKonfigurimi.Text.Split(';')[0], idndermarrje);
            DbCore.clsMesazh mesazh = GridUtil.ruajFiltra(idndermarrje, idperdorues, idgjuha, "gvPajisje", komponente, "FilterDefault", gvPajisje.FilterExpression, gvPajisje, "Targa", idkonf, out idfiltri);//ruan filtrin e zgjedhur tek filtrat
            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                return;

            }
            mesazh = GridUtil.ruajkonfigurimgride(gvPajisje, cmbKonfigurimi.Text, idndermarrje, idperdorues, 2030, idfiltri, idViti, DbCore.mySessionObjects.ktheCultureInfo(Session), DbCore.mySessionObjects.ktheGjuhe(Session));/// ruan konfigurimin e grides dhe filtrin

            clsToolbarConfig.mbushComboBoxFiltra(idgjuha, idndermarrje, "gvPajisje", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);

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
        protected void gvPajisje_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            if (e.CallbackName == "COLUMNMOVE" && gvPajisje.AllColumns[int.Parse(e.Args[0])].Width.Value == 0)
                gvPajisje.AllColumns[int.Parse(e.Args[0])].Width = Unit.Percentage(3);
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
        protected void gvPajisje_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
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

        protected void gvPajisje_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
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
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuhePerdoruesi, "gvPajisje", komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], idNdermarrje, koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvPajisje.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvPajisje);
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
            gvPajisje.Selection.UnselectAll();
        }

        protected void gvPajisje_CustomJSProperties(object sender, DevExpress.Web.ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvPajisje.PageIndex;
            e.Properties["cpPageRow"] = gvPajisje.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvPajisje.VisibleRowCount;
        }

        /// <summary>
        /// perdoret per te shtuar kolonen e selektimit tek grida dhe per te vendosur disa karakteristika te grides
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void gvPajisje_DataBound(object sender, EventArgs e)
        {// shton colonen # per selektim dhe disa karakteristika te grides
            if (this.gvPajisje.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
                //perzgjidh
                DevExpress.Web.GridViewCommandColumn check = new DevExpress.Web.GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true;
                check.Width = Unit.Percentage(2);
                check.VisibleIndex = 0;
                gvPajisje.Settings.ShowFilterRow = true;
                gvPajisje.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                gvPajisje.Settings.ShowFilterRowMenu = true;
                gvPajisje.Columns.Add(check);
                gvPajisje.KeyFieldName = "IdPajisje";
                gvPajisje.SettingsBehavior.AllowSelectByRowClick = true;
                gvPajisje.SettingsBehavior.AllowFocusedRow = true;
            }
        }

        protected void gvPajisje_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
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
    }
}