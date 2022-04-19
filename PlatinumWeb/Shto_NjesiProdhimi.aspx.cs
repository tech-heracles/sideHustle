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
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    public partial class Shto_NjesiProdhimi : MyPageBase
    {
        private const string prefixMesazhNjejes = "Njesia e prodhimit me kod: ";
        private const string prefixMesazhShumes = "Njesite e prodhimit me kode: ";
        private const string suffixMesazhNjejesGabimi = " eshte e lidhur dhe nuk mund te fshihet";
        private const string suffixMesazhShumesGabimi = " jane te lidhura dhe nuk mund te fshihen";
        private const string suffixMesazhNjejesSuksesi = " u fshi me sukses!";
        private const string suffixMesazhShumesSuksesi = " u fshine me sukses!";
        private const string lidhesMesazhi = ". Kurse ";
        private const string mesazhZgjidhniNje = "Ju lutem zgjidhni te pakten nje njesi prodhimi!";

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
                    DbCore.clsFunksione.logout(Session, false, "", false);

            if (!Page.IsPostBack)
            {
                EmrateTabeve();
                konfiguroVleraFillestare(idNdermarrje, idGjuha);
                ASPxPageControl1.ActiveTabIndex = 0;
                mbushGridNjesiProdhimeshNgaDB(idNdermarrje);
                konfiguroGride(idNdermarrje, idPerdoruesi);
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "ASPxGridView_NjProdhimi", ASPxGridView_NjProdhimi, cmbKonfigurimi.Text, Convert.ToString(512), (int)hfState["idGjuha"]);
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, "Shto_NjesiProdhimi.aspx");
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
            }
            else
            {
                mbushGridNjesiProdhimeshNgaSesioni(idNdermarrje);
                konfiguroGride(idNdermarrje, idPerdoruesi);
            }
            GridUtil.konfigGrideListeEMadhePaTheme(ASPxGridView_NjProdhimi, "IdNjesiProdhimi");
            percaktoTemplateMenu(idGjuha, idViti, idPerdoruesi, idNdermarrje, ASPxMenu1);
            clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, "ASPxGridView_NjProdhimi", int.Parse(cmbKonfigurimi.Value.ToString()), "Shto_NjesiProdhimi.aspx");
            GridUtil.ToolTipButonaveMbiGride(ASPxGridView_NjProdhimi, DbCore.mySessionObjects.ktheCultureInfo(Session), new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources")));
        }

        private void mbushGridNjesiProdhimeshNgaDB(int idNdermarrje)
        {//mbush griden e njesive te prodhimit me te dhena            
            DataTable dt = DbCore.DbRegjistrim.colNjesiProdhimi.merrNjesiProdhimiSipasNdermarrjeDt(idNdermarrje);
            DbCore.mySessionObjects.ruajGrideNeSession(Session, dt);
            ASPxGridView_NjProdhimi.DataSource = dt;
            ASPxGridView_NjProdhimi.DataBind();
            dt.Dispose();
        }

        private void mbushGridNjesiProdhimeshNgaSesioni(int idNdermarrje)
        {
            DataTable tmpObject;
            bool sukses = DbCore.mySessionObjects.merrGrideNgaSessioni(Session, out tmpObject);
            if (!sukses)
                mbushGridNjesiProdhimeshNgaDB(idNdermarrje);
            else
            {
                ASPxGridView_NjProdhimi.DataSource = tmpObject;
                ASPxGridView_NjProdhimi.DataBind();
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
            ASPxGridView_NjProdhimi.Columns["#"].VisibleIndex = 0;
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
        }

        private void konfiguroVleraFillestare(int idNdermarrje, int idGjuha)
        { //mbush komboboxet dhe gridat e faqes
            int idPerdoruesi = (int)hfState["idPerdoruesi"];
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmbDegeAdministrative);
            ConfigureAspxComboBox.shtoKolonaPerDegeAdministrative(cmbDegeAdministrative);
            mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 101, "NJPR", idGjuha);
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
                ruajNjesiProdhimi();
            }
        }

        private void ruajNjesiProdhimi()
        {
            if (Page.IsValid == false)
                return;
            int idPerdoruesi = (int)hfState["idPerdoruesi"];
            int idGjuha = (int)hfState["idGjuha"];
            int idNdermarrje = (int)hfState["idNdermarrje"];
            int idViti = (int)hfState["idViti"];
            DbCore.DbRegjistrim.clsNjesiProdhimi njesiProdhimi;
            try
            {
                if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
                    njesiProdhimi = krijoNjesiProdhimi(idNdermarrje, idPerdoruesi, true);
                else njesiProdhimi = krijoNjesiProdhimi(idNdermarrje, idPerdoruesi, false);
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
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, "Shto_NjesiProdhimi.aspx");
            bool eshteShtim;
            if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
            {
                if (!tedrejtaInfo.DShtim)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                    hfStatusi.Value = "false";
                    return;
                }
                mesazh = njesiProdhimi.ruaj();
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
                mesazh = njesiProdhimi.modifiko();
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
                    shtoNjesiProdhimiNeGrid(idNdermarrje, idPerdoruesi, njesiProdhimi.IdNjesiProdhimi);
                else //modifikim
                    modifikoNjesiProdhimiNeGrid(idNdermarrje, idPerdoruesi, njesiProdhimi.IdNjesiProdhimi);
                hfStatusi.Value = "true";
                konfiguroGride(idNdermarrje, idPerdoruesi);
                ASPxPageControl1.ActiveTabIndex = 0;
            }
        }

        private DbCore.DbRegjistrim.clsNjesiProdhimi krijoNjesiProdhimi(int idNdermarrje, int idPerdorues, bool shtim)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            CultureInfo ci = mySessionObjects.ktheCultureInfo(Session);
            int idNjesiProdhimi;
            int idKrijuesi;
            if (shtim == true)
            {
                idNjesiProdhimi = 0;
                idKrijuesi = idPerdorues;
            }
            else
            {
                idNjesiProdhimi = int.Parse(hfId.Value.ToString());
                idKrijuesi = DbCore.DbRegjistrim.clsNjesiProdhimi.ktheIdKrijuesNjesiProdhimiSipasId(idNjesiProdhimi);
            }
            string kodi = txtKodi.Text;
            string pershkrimi = txtPershkrimi.Text;
            DateTime dtRegjistrimi = dteDtRegjistrimi.Date;
            string adresa = txtAdresa.Text;
            bool aktiv = cbAktiv.Checked;
            string shenime = txtShenime.Text;
            
            int idDegeAdministrative = 0;
            string kodDegeAdministrative = "";
            try
            {
                if (cmbDegeAdministrative.Text != "")
                {
                    kodDegeAdministrative = cmbDegeAdministrative.Text;
                    idDegeAdministrative = int.Parse(cmbDegeAdministrative.Value.ToString());
                }
            }
            catch (Exception err)
            {
                string mesazhi = "Dega administrative nuk ekziston!";
                NLog.LogManager.GetCurrentClassLogger().Error(mesazhi, err.Message);
                kodDegeAdministrative = "";             
                throw new DbCore.MyException(mesazhi);
            }

            DbCore.DbRegjistrim.clsNjesiProdhimi njesiProdh = new DbCore.DbRegjistrim.clsNjesiProdhimi(idNjesiProdhimi, DbCore.clsFunksione.ktheStringunPaHapesira(kodi, true), DbCore.clsFunksione.ktheStringunPaHapesira(pershkrimi, false), dtRegjistrimi, idDegeAdministrative, adresa, aktiv, shenime, idPerdorues, idKrijuesi, idNdermarrje, 1, kodDegeAdministrative, shtim, int.Parse(cmbKonfigurimi.Value.ToString()), rm, ci);
            return njesiProdh;
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
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, "Shto_NjesiProdhimi.aspx", this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, hfShtimModifikim.Value == "modifikim" ? false : true, false, false, meme);
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
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuhePerdoruesi, "ASPxGridView_NjProdhimi", "Shto_NjesiProdhimi.aspx", idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = ASPxGridView_NjProdhimi.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("Kodi", ASPxGridView_NjProdhimi);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = ASPxGridView_NjProdhimi.GetSortedColumns();
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
            //    filtri.KoloneRenditje = "Kodi";
            //    filtri.DrejtimRenditje = true;
            //}
            int idPerdoruesi = (int)hfState["idPerdoruesi"];
            filtri.IdPerdoruesi = idPerdoruesi;
            filtri.IdNdermarje = idNdermarrje;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            filtri.IdStatusDok = 1;
            mesazh = filtri.ruaj();
            clsToolbarConfig.mbushComboBoxFiltra(idGjuhePerdoruesi, idNdermarrje, "ASPxGridView_NjProdhimi", int.Parse(cmbKonfigurimi.Value.ToString()), "Shto_NjesiProdhimi.aspx");
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
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuhePerdoruesi, "ASPxGridView_NjProdhimi", "Shto_NjesiProdhimi.aspx", idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
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
                clsToolbarConfig.mbushComboBoxFiltra(idGjuhePerdoruesi, idNdermarrje, "ASPxGridView_NjProdhimi", int.Parse(cmbKonfigurimi.Value.ToString()), "Shto_NjesiProdhimi.aspx");
                percaktoTemplateMenu(idGjuhePerdoruesi, idViti, idPerdorues, idNdermarrje, ASPxMenu1);
                //konfiguroVleraFillestare(idNdermarrje, idGjuhePerdoruesi);
                hfStatusi.Value = "true";
                this.ASPxGridView_NjProdhimi.FilterExpression = String.Empty;
            }
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
                rreshtat = ASPxGridView_NjProdhimi.GetSelectedFieldValues("IdNjesiProdhimi");
            else
            {
                rreshtat = new List<object>();
                rreshtat.Add(hfId.Value);
            }
            
            if (rreshtat.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhZgjidhniNje, pnlMesazhi);
                return;
            }
            List<string> njesiTeFshira = new List<string>(), njesiTePaFshira = new List<string>();
            int idNdermarrje = (int)hfState["idNdermarrje"];
            int idPerdorues = (int)hfState["idPerdoruesi"];
            DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegjistrim = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
            DbCore.DbRegjistrim.clsNjesiProdhimi njesiProdhimi = new DbCore.DbRegjistrim.clsNjesiProdhimi();
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            for (int i = 0; i < rreshtat.Count; i++)
            {
                njesiProdhimi.mbushNjesiProdhimi(Convert.ToInt32(rreshtat[i]));
                if (njesiProdhimi.IdNjesiProdhimi == 0)
                    continue;

                //konf.mbushKonfigAmbjSipasId(auto.IdAutomjeti);
                konf.mbushKonfigAmbjSipasKod(cmbKonfigurimi.Text, idNdermarrje);
                bool lidhur = dbRegjistrim.eshteDokumentiILidhurCelje(njesiProdhimi.IdNjesiProdhimi.ToString(), konf.IdNivel.ToString());
                if (lidhur)
                {
                    njesiTePaFshira.Add(njesiProdhimi.Kodi);
                    continue;
                }
                DbCore.clsMesazh mesazhi = DbCore.DbRegjistrim.clsNjesiProdhimi.fshi(njesiProdhimi.IdNjesiProdhimi, idPerdorues);
                if (mesazhi.Status)
                {
                    #region Heq njesine e prodhimit nga grida
                    hiqNjesiProdhimiNgaGrida(idNdermarrje, njesiProdhimi.IdNjesiProdhimi);
                    #endregion
                    njesiTeFshira.Add(njesiProdhimi.Kodi);

                    ASPxPageControl1.ActiveTabIndex = 0;
                    hfStatusi.Value = "true";
                }
            }
            dbRegjistrim.Dispose();
            string mesazhInfoGabim = "", mesazhInfoSukses = "";
            if (njesiTePaFshira.Count == 1)
                mesazhInfoGabim = String.Format("{0}{1}{2}", prefixMesazhNjejes, String.Join(";", njesiTePaFshira), suffixMesazhNjejesGabimi);
            else
                if (njesiTePaFshira.Count > 1)
                    mesazhInfoGabim = String.Format("{0}{1}{2}", prefixMesazhShumes, String.Join(";", njesiTePaFshira), suffixMesazhShumesGabimi);
            if (njesiTeFshira.Count == 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", prefixMesazhNjejes, String.Join(";", njesiTeFshira), suffixMesazhNjejesSuksesi);
            else
                if (njesiTeFshira.Count > 1)
                    mesazhInfoSukses = String.Format("{0}{1}{2}", prefixMesazhShumes, String.Join(";", njesiTeFshira), suffixMesazhShumesSuksesi);
            if (mesazhInfoGabim != "" && mesazhInfoSukses != "")
                mesazhInfoGabim += lidhesMesazhi + mesazhInfoSukses;
            if (mesazhInfoGabim != "")
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhInfoGabim, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazhInfoSukses, pnlMesazhi);
            pnlMesazhi.Update();
        }

        private void hiqNjesiProdhimiNgaGrida(int idNdermarrje, int idNjesiProdhimi)
        {
            if (ASPxGridView_NjProdhimi.DataSource != null)
            {
                DataTable dt = (DataTable)ASPxGridView_NjProdhimi.DataSource;
                DataRow[] drs = dt.Select("IdNjesiProdhimi = " + idNjesiProdhimi);
                if (drs.Length > 1)
                    throw new Exception("GABIM: Ndodhen 2 njesi prodhimi me te njejten id ne gride");
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                dt.Rows.Remove(dr);
                ASPxGridView_NjProdhimi.DataBind();
            }
            else
                mbushGridNjesiProdhimeshNgaDB(idNdermarrje);
        }

        private void shtoNjesiProdhimiNeGrid(int idNdermarrje, int idPerdorues, int idNjesiProdhimi)
        {
            if (ASPxGridView_NjProdhimi.DataSource != null)
            {
                DataTable dt = (DataTable)ASPxGridView_NjProdhimi.DataSource;
                DataRow[] drs = dt.Select("IdNjesiProdhimi = " + idNjesiProdhimi);
                if (drs.Length > 0)
                    throw new Exception("GABIM: Njesia e prodhimit ekziston ne gride!");
                DataRow newDr = DbCore.DbRegjistrim.clsNjesiProdhimi.merrNjesiProdhimiSipasIdDR(idNjesiProdhimi);
                dt.ImportRow(newDr);
            }
            else mbushGridNjesiProdhimeshNgaDB(idNdermarrje);
            konfiguroGride(idNdermarrje, idPerdorues);
        }

        private void modifikoNjesiProdhimiNeGrid(int idNdermarrje, int idPerdorues, int idNjesiProdhimi)
        {
            if (ASPxGridView_NjProdhimi.DataSource != null)
            {
                DataTable dt = (DataTable)ASPxGridView_NjProdhimi.DataSource;
                DataRow[] drs = dt.Select("IdNjesiProdhimi = " + idNjesiProdhimi);
                if (drs.Length > 1)
                    throw new Exception("GABIM: Ndodhen 2 njesi prodhimi me te njejten id ne gride!");
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                DataRow newDr = DbCore.DbRegjistrim.clsNjesiProdhimi.merrNjesiProdhimiSipasIdDR(idNjesiProdhimi);
                object[] arr = newDr.ItemArray;
                dr.ItemArray = arr;
            }
            else mbushGridNjesiProdhimeshNgaDB(idNdermarrje);
            konfiguroGride(idNdermarrje, idPerdorues);
        }

        /// <summary>
        /// perdoret ne rastet kur grida ben callback per ta rimbushur griden
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void ASPxGridView_NjProdhimi_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            if (e.CallbackName == "COLUMNMOVE" && ASPxGridView_NjProdhimi.AllColumns[int.Parse(e.Args[0])].Width.Value == 0)
                ASPxGridView_NjProdhimi.AllColumns[int.Parse(e.Args[0])].Width = Unit.Percentage(3);
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
        protected void ASPxGridView_NjProdhimi_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            if (e.Column.FieldName == "Kodi" || e.Column.FieldName == "Pershkrimi" || e.Column.FieldName == "Adresa")
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

        protected void ASPxGridView_NjProdhimi_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
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
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuhePerdoruesi, "ASPxGridView_NjProdhimi", "Shto_NjesiProdhimi.aspx", idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], idNdermarrje, koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        ASPxGridView_NjProdhimi.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, ASPxGridView_NjProdhimi);
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
            ASPxGridView_NjProdhimi.Selection.UnselectAll();
        }

        protected void ASPxGridView_NjProdhimi_CustomJSProperties(object sender, DevExpress.Web.ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = ASPxGridView_NjProdhimi.PageIndex;
            e.Properties["cpPageRow"] = ASPxGridView_NjProdhimi.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = ASPxGridView_NjProdhimi.VisibleRowCount;
        }

        /// <summary>
        /// perdoret per te shtuar kolonen e selektimit tek grida dhe per te vendosur disa karakteristika te grides
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void ASPxGridView_NjProdhimi_DataBound(object sender, EventArgs e)
        {// shton colonen # per selektim dhe disa karakteristika te grides
            if (this.ASPxGridView_NjProdhimi.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
                //perzgjidh
                DevExpress.Web.GridViewCommandColumn check = new DevExpress.Web.GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true;
                check.Width = Unit.Percentage(2);
                check.VisibleIndex = 0;
                ASPxGridView_NjProdhimi.Settings.ShowFilterRow = true;
                ASPxGridView_NjProdhimi.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                ASPxGridView_NjProdhimi.Settings.ShowFilterRowMenu = true;
                ASPxGridView_NjProdhimi.Columns.Add(check);
                ASPxGridView_NjProdhimi.KeyFieldName = "IdNjesiProdhimi";
                ASPxGridView_NjProdhimi.SettingsBehavior.AllowSelectByRowClick = true;
                ASPxGridView_NjProdhimi.SettingsBehavior.AllowFocusedRow = true;
            }
        }

        protected void ASPxGridView_NjProdhimi_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if (e.Column.FieldName == "IdDegeAdministrative")
            {
                if (Converter.ConvertToInt(e.Value)==0 || Converter.ConvertToInt(e.Value) == -1)
                {
                    e.Criteria = null;
                }
            }
        }

        protected void cmbDegeAdministrative_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("cmbDegeAdministrative"))
                {
                    int value = 0;
                    if (e.Value == null || !Int32.TryParse(e.Value.ToString(), out value))
                        return;
                    int idPerdoruesi = (int)hfState["idPerdoruesi"];
                    int idNdermarrje = (int)hfState["idNdermarrje"];
                    ConfigureAspxComboBox.mbushComboDegaAdminByID((ASPxComboBox)source, value);
                }
            }
        }

        protected void cmbDegeAdministrative_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("cmbDegeAdministrative"))
                {
                    int idNdermarrje = (int)hfState["idNdermarrje"];
                    ConfigureAspxComboBox.mbushComboDegeAdministrative(e.Filter, e.BeginIndex + 1, e.EndIndex + 1, idNdermarrje, cmbDegeAdministrative);
                }
            }
        }

        protected void RuajKolona_Click(object sender, EventArgs e)
        {
            int idViti = (int)hfState["idViti"];
            int idndermarrje = (int)hfState["idNdermarrje"];
            int idgjuha = (int)hfState["idGjuha"];
            int idperdorues = (int)hfState["idPerdoruesi"];
            int idfiltri = 0;
            int idkonf = DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(cmbKonfigurimi.Text.Split(';')[0], idndermarrje);
            DbCore.clsMesazh mesazh = GridUtil.ruajFiltra(idndermarrje, idperdorues, idgjuha, "ASPxGridView_NjProdhimi", "Shto_NjesiProdhimi.aspx", "FilterDefault", ASPxGridView_NjProdhimi.FilterExpression, ASPxGridView_NjProdhimi, "Kodi", idkonf, out idfiltri);//ruan filtrin e zgjedhur tek filtrat
            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                return;
            }
            mesazh = GridUtil.ruajkonfigurimgride(ASPxGridView_NjProdhimi, cmbKonfigurimi.Text, idndermarrje, idperdorues, 404, idfiltri, idViti, DbCore.mySessionObjects.ktheCultureInfo(Session), DbCore.mySessionObjects.ktheGjuhe(Session));/// ruan konfigurimin e grides dhe filtrin

            clsToolbarConfig.mbushComboBoxFiltra(idgjuha, idndermarrje, "ASPxGridView_NjProdhimi", int.Parse(cmbKonfigurimi.Value.ToString()), "Shto_NjesiProdhimi.aspx");

            percaktoTemplateMenu(idgjuha, idViti, idperdorues, idndermarrje, ASPxMenu1);
            if (mesazh.Status)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
        }

        protected void btnXlsxExport_Click(object sender, EventArgs e)
        {
            try
            {
                gridExport.WriteXlsxToResponse("Njesite e Prodhimit", true);
            }
            catch (Exception)
            {
            }
        }

        protected void btnPdfExport_Click(object sender, EventArgs e)
        {
            try
            {
                gridExport.WritePdfToResponse("Njesite e Prodhimit", true);
            }
            catch (Exception)
            {
            }
        }
    }
}