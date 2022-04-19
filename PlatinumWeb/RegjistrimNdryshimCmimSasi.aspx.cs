using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Data;
using DbCore;
using DbCore.DbRegjistrim;
using System.Globalization;
using System.Resources;
using DbCore.DbShare;
using DbCore.DbKontabiliteti;
using DbCore.DbInventari;
using DbCore.IMBUtils.Types;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class RegjistrimNdryshimCmimSasi : MyPageBase
    {


        DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();

        private DbCore.DbRegjistrim.colTrupiMagazina trupat = new DbCore.DbRegjistrim.colTrupiMagazina();

        string komponente = "RegjistrimNdryshimCmimSasi.aspx";
        string guidString;
        protected void Page_Load(object sender, EventArgs e)
        {
            int idPerdoruesi;
            int idGjuha;
            int idNdermarrje;
            int idViti;
            int idNdermarrjeVit;
            bool eshteOwn;
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (!DbCore.mySessionObjects.isLogedIn(Session))
                DbCore.clsFunksione.logout(Session,true,"FaqePaautorizuar");
            int idPerd = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerd);
            oPerdorues = DbCore.mySessionObjects.kthePerdorues(Session);
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
                eshteOwn = DbCore.mySessionObjects.merrEshteOwnSesioni(Session);
                hfState.Set("idPerdoruesi", idPerdoruesi);
                hfState.Set("idGjuha", idGjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idViti", idViti);

                hfState.Set("idNdermarrjeVit", idNdermarrjeVit);
                hfState.Set("OwnShop", eshteOwn);
                guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                hfState.Set("guidString", guidString);
            }
            else
            {
                idPerdoruesi = (int)hfState["idPerdoruesi"];
                idGjuha = (int)hfState["idGjuha"];
                idNdermarrje = (int)hfState["idNdermarrje"];
                idViti = (int)hfState["idViti"];

                idNdermarrjeVit = (int)hfState["idNdermarrjeVit"];
                guidString = (string)hfState["guidString"];
            }
            percaktoTemplateMenu(idGjuha, ASPxMenu1, idViti, idPerd, idNdermarrje);
            if (!IsPostBack)
            {
                EmrateButonave(cultinf, rm);
                mbushHiddenFieldMePerkthime(cultinf, rm);
                ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerd, idNdermarrje, cmbKonfigurimi, 94, "LDRNSC", rm, cultinf, idGjuha);
                cmbKonfigurimi.SelectedIndex = 0;
                DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()), idGjuha);
                hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
                mbushGridDokumentNdryshimCmimSasiNgaDB();

                grid_RegMag.FilterExpression = "[IdStatusDok]=1";
                

                if (Request.QueryString["fshi"] == "po")
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("regjMagFshirjaPerfundoiMeSukses", cultinf), pnlMesazhi);
                else if (Request.QueryString["fshi"] == "rivleresimpo")
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("regjMagSuksesFshirjeDokDheRivleresim", cultinf), pnlMesazhi);
                else if (Request.QueryString["fshi"] == "rivleresimjo")
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("regjMagMesazhGabimiRivleresim", cultinf), pnlMesazhi);
                else if (Request.QueryString["fshi"] == "rivleresimruajpo")
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("regjMagMesazhSuksesiRuajteDokDheRivleresim", cultinf), pnlMesazhi);
                else if (Request.QueryString["fshi"] == "rivleresimruajjo")
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("regjMagMesazhGabimiGjateRivleresimit", cultinf), pnlMesazhi);
                else if (Request.QueryString["ruaj"] == "po")
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("regjMagModifikimiPerfundoiMeSukses", cultinf), pnlMesazhi);
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerd, idNdermarrje, idViti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                konfiguroGride(idGjuha, idNdermarrje, idPerd,rm,cultinf);
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "grid_RegMag", grid_RegMag, cmbKonfigurimi.Text.Split(';')[0], "544", idGjuha);
            }
            else
            {
                mbushGridDokumentNdryshimCmimSasiNgaSession();
                Container.Attributes["src"] = "";
                konfiguroGride(idGjuha, idNdermarrje, idPerd,rm,cultinf);
            }
            this.grid_RegMag.Columns["#"].VisibleIndex = 0;
            GridUtil.konfigGrideListeEMadhePaTheme(grid_RegMag, "IdKoka");
            grid_RegMag.PercaktoTitlePanel(this, MenuInfo, pnlMesazhi, hfState, idPerdoruesi, idNdermarrje, idViti, idGjuha, int.Parse(cmbKonfigurimi.Value.ToString()), "RegjistrimNdryshimCmimSasi.aspx", rm, cultinf);
            AspxWebControlUtils.perkthePopUp(popFshi, rm.GetString("labelKujdes", cultinf), lblMsgbox, rm.GetString("labelAdministrimiMsgJeniSigurt", cultinf), ButtonCancel, rm.GetString("labelAnullo", cultinf));
            clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, "grid_RegMag", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
            if (Request.QueryString["indexrow"] != null)
                grid_RegMag.FocusedRowIndex = int.Parse(Request.QueryString["indexrow"]);
            konfigurimi_Label.Text = rm.GetString("lblModeli", cultinf);
        }

        /// <summary>
        /// Metode qe sherben per te kaluar perkthime te js
        /// </summary>
        /// <param name="cultinf">Merr culture info</param>
        /// <param name="rm">Merr Resource Manager</param>
        private void mbushHiddenFieldMePerkthime(CultureInfo cultinf, ResourceManager rm)
        {
            hfState.Set("msgJuKeniZgjedhur", rm.GetString("msgJuKeniZgjedhur", cultinf));
            hfState.Set("msgRreshta", rm.GetString("msgRreshta", cultinf));
            hfState.Set("labelAdministrimiMsgJeniSigurt", rm.GetString("labelAdministrimiMsgJeniSigurt", cultinf));
            hfState.Set("msgZgjdhniNjeNgaElementetEListes", rm.GetString("msgZgjdhniNjeNgaElementetEListes", cultinf));
        }

        /// <summary>
        /// Vendos emrat e butonave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="cultinf">Merr culture info perkatese</param>
        /// <param name="rm">Merr Resource Manager</param>
        private void EmrateButonave(CultureInfo cultinf, ResourceManager rm)
        {
            ButtonCancel.Text = rm.GetString("btnAdministrimiCancel", cultinf);
        }



        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        /// <param name="idViti"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNermarrje"></param>
        private void percaktoTemplateMenu(int idGjuha, ASPxMenu aSPxMenu1, int idViti, int idPerdorues, int idNdermarrje)
        {
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, komponente, this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, btnPo_Click, btnJo_Click, true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session), false);
        }
        
        public void btnJo_Click(object sender, EventArgs e)
        {
            if (pergjigja.Text.Contains("lidhur") || pergjigja.Text.Contains("kycur"))
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, pergjigja.Text, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, pergjigja.Text, pnlMesazhi);
            DbCore.mySessionObjects.ruajTrupatNeSession(Session, new DbCore.DbRegjistrim.colTrupiMagazina());
        }

        public void btnPo_Click(object sender, EventArgs e)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            DbCore.DbRegjistrim.colTrupiMagazina trupat = DbCore.mySessionObjects.merrTrupatNgaSesioni(Session);

            DbCore.clsMesazh mesazh = new DbCore.clsMesazh(true, rm.GetString("regjMagMesazhSuksesRivleresimi", cultinf));
            string fileLogPath = Server.MapPath("~/log/log.txt");
            DbCore.DbAdmin.clsLogRivleresimInventari log = new DbCore.DbAdmin.clsLogRivleresimInventari();
            try
            {
                log = new DbCore.DbAdmin.clsLogRivleresimInventari(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            }
            catch (Exception err)
            {               
                NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
                throw new DbCore.MyException(rm.GetString("msgGabimGjateRuajtjesSeRivleresimitNeLog", cultinf));
            }
            foreach (DbCore.DbRegjistrim.clsTrupiMagazina t in trupat)
            {
                //DbCore.DbInventari.clsArtikulli art = new DbCore.DbInventari.clsArtikulli(t.IdArtikulli);
                mesazh = DbCore.DbInventari.clsArtikulli.rivleresimCmimiMesatar(t.IdArtikulli, DbCore.DbInventari.clsArtikulli.ktheMetodeKostoje(t.IdArtikulli), t.IdMag, t.Data, DateTime.Today,log,cultinf,rm, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
                if (!mesazh.Status)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("regjisDokMesazhGabimRivleresimi", cultinf), pnlMesazhi);
                }
            }
            DbCore.mySessionObjects.ruajTrupatNeSession(Session, new DbCore.DbRegjistrim.colTrupiMagazina());
            if (mesazh.Status)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("regjMagMesazhSuksesRivleresimi", cultinf), pnlMesazhi);
        }
        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));

        }
        private void mbushGridDokumentNdryshimCmimSasiNgaSession()
        {
            DataTable tmpObject;
            bool sukses = DbCore.mySessionObjects.merrGrideNgaSessioni(Session, out tmpObject);
            if (!sukses)
                mbushGridDokumentNdryshimCmimSasiNgaDB();
            else
            {
                grid_RegMag.DataSource = tmpObject;
                grid_RegMag.DataBind();
                tmpObject.Dispose();
            }
        }

        private void mbushGridDokumentNdryshimCmimSasiNgaDB()
        {//mbush griden e popupit me te dhena            
            DataTable dt = DbCore.DbRegjistrim.colKokaNdryshimCmimSasi.merrKokaNdryshimCmimSasiDT(DbCore.mySessionObjects.ktheNdermarrjeVit(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            DbCore.mySessionObjects.ruajGrideNeSession(Session, dt);
            grid_RegMag.DataSource = dt;
            grid_RegMag.DataBind();
            dt.Dispose();
        }

        private void konfiguroGride(int idGjuha, int idNdermarrje, int idPerdoruesi,ResourceManager rm,CultureInfo ci)
        {
            KonfigurimComboGride.ShtoNivel(grid_RegMag, 95, idNdermarrje, idPerdoruesi, idGjuha, Session, komponente, guidString);
            KonfigurimComboGride.ShtoModel(grid_RegMag, 95, idNdermarrje, idPerdoruesi, idGjuha, Session, komponente, guidString);
            KonfigurimComboGride.ShtoMagazinaNdermarrje(grid_RegMag, Session, komponente, guidString, "IdMagazina");
            KonfigurimComboGride.shto_DegeAdministrative(grid_RegMag,idNdermarrje, Session, komponente, guidString);
            KonfigurimComboGride.ShtoGrupimDokumentash(grid_RegMag, idNdermarrje, idPerdoruesi, Session, komponente, guidString,"IdGrup1",1);
            KonfigurimComboGride.ShtoStatus(grid_RegMag, rm, ci);
            GridViewDataTextColumn col3 = grid_RegMag.Columns["Vlefta"] as GridViewDataTextColumn;
            col3.PropertiesEdit.DisplayFormatString = "0.00";
            this.grid_RegMag.Columns["#"].VisibleIndex = 0;
         
        }
    
 

        protected void grid_RegMag_DataBound(object sender, EventArgs e)
        {
            if (this.grid_RegMag.Columns["#"] == null)
            {
                DevExpress.Web.GridViewCommandColumn check = new DevExpress.Web.GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true; check.Width = Unit.Percentage(2);

                grid_RegMag.Settings.ShowFilterRow = true;
                grid_RegMag.Settings.ShowHeaderFilterButton = true;
                grid_RegMag.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                grid_RegMag.Settings.ShowFilterRowMenu = true;
                grid_RegMag.Columns.Add(check);
                grid_RegMag.Settings.ShowGroupPanel = true;
                grid_RegMag.KeyFieldName = "IdKoka";
                grid_RegMag.SettingsBehavior.AllowSelectByRowClick = true;
                grid_RegMag.SettingsBehavior.AllowFocusedRow = true;
            }
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
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "grid_RegMag", komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "grid_RegMag", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
                percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);

                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                grid_RegMag.FilterExpression = "[IdStatusDok]=1";

            }
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
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida();
            filtri.FiltraKodi = cmbFiltra.Text;
            filtri.FiltraShenime = cmbFiltra.Text;
            filtri.FiltraUniversal = false;// Universal_ASPxCheckBox.Checked;
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "grid_RegMag", komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = grid_RegMag.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("IdNivel", grid_RegMag);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = grid_RegMag.GetSortedColumns();
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
            //    filtri.KoloneRenditje = "IdNivel";
            //    filtri.DrejtimRenditje = true;
            //}
            DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();
            oPerdorues = DbCore.mySessionObjects.kthePerdorues(Session);
            filtri.IdPerdoruesi = oPerdorues.IdPerdorues;
            filtri.IdNdermarje = idNdermarrje;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();

            filtri.IdStatusDok = 1;
            mesazh = filtri.ruaj();
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "grid_RegMag", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
            percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);

            if (mesazh.Status == true)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

            cmbFiltra.Text = "";

        }
        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (e.Item.Name == "PrintPreview")
            {
                //if (grid_RegMag.FocusedRowIndex == -1)
                //{
                //    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("regjMagMesazhZgjidhniFaturePerPrintim", cultinf), pnlMesazhi);
                //    Container.Attributes["src"] = "";
                //}
                //else
                //{
                //    string id = grid_RegMag.GetRowValues(grid_RegMag.FocusedRowIndex, "IdKoka").ToString();
                //    DbCore.DbRegjistrim.clsKokaNdryshimCmimSasi clsKoka = new clsKokaNdryshimCmimSasi ();
                //    clsKoka.mbushKokaNdryshimCmimSasiSipasID(int.Parse(id));
                //    DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                //    konf.mbushKonfiguriminMeID(clsKoka.IdKonfigAmbjente);

                //    Container.Attributes["src"] = "RaportiShpejte.aspx?Sesioni=false&idraporti=38&idDokumenti=" + clsKoka.IdKoka + "&printo=false";

                //}

            }
            else if (e.Item.Name == "Riruaj")
            {
                Riruaj();
            }

        }


        protected void Riruaj()
        {
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            List<string> TeRuajtur = new List<string>(), Teparuajtur = new List<string>();
            pergjigja.Text = "";

            List<object> rreshtat = this.grid_RegMag.GetSelectedFieldValues("IdKoka");
            pergjigja.Text = "";
            string serverUrl = DbCore.clsFunksione.ktheServerUrl(Request);
            int idPerdoruesi = (int)hfState["idPerdoruesi"];
            int idNdermarrje = (int)hfState["idNdermarrje"];

            int idNdermarrjeVit = (int)hfState["idNdermarrjeVit"];
            int idGjuha = (int)hfState["idGjuha"];
            bool eshteOwn = (bool)hfState["OwnShop"];
            DataTable err = new DataTable();
            err.Columns.Add("Kodi");
            err.Columns.Add("Gabimi");
            err.Columns.Add("Rreshti");
            int nrreshta = 0;
            foreach (object id in rreshtat)
            {
                nrreshta++;
                clsKokaNdryshimCmimSasi clsKoka = new clsKokaNdryshimCmimSasi();
                clsKoka.mbushKokaNdryshimCmimSasiSipasID(Convert.ToInt32(id));
                if (clsKoka.IdStatusDok == 2)
                    continue;

                clsKonfigurimAmbjenti konfig = new clsKonfigurimAmbjenti(clsKoka.IdKonfigAmbjente);
                if (DbCore.MbylljePeriudhe.PeriodClosing.IsPeriodClosed(clsKoka.DtDok, DbCore.IMBUtils.DataBase.MyConnectionsManager.GetSelectedConNameServer(), idNdermarrje, KategoriDokumenti.NdryshimSasiCmim, clsKoka.IdKonfigAmbjente))
                {
                    object[] arr = { konfig.KodKonfigAmbjente + " " + clsKoka.NrDok + " " + clsKoka.DtDok.ToShortDateString(), DbCore.IMBUtils.Messages.MessagesResource.Messages["msgPeriodIsClosed"], nrreshta };
                    err.Rows.Add(arr);
                    continue;
                }

                colTrupiNdryshimCmimSasi col = new colTrupiNdryshimCmimSasi();

                int idKategoria = DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKategori(clsKoka.IdKonfigAmbjente);

                col.mbushGjitheTrupiNdryshimCmimSasiNgaKoka(clsKoka.IdKoka);
                clsKokaMagazina kokam = new clsKokaMagazina();
                kokam.mbushKokaMagazinaSipasIDGjenerues(clsKoka.IdKoka, 1, clsKoka.IdKonfigAmbjente);
                //DbCore.DbAdmin.clsPeriudhaKontabel periudha = new DbCore.DbAdmin.clsPeriudhaKontabel(clsKoka.DtDok, idNdermarrje); ;

                bool lidhur = clsKoka.eshteILidhur();
                bool autorizimet = DbCore.DbRegjistrim.clsKokaNdryshimCmimSasi.kaAutorizime(clsKoka.IdKoka, idPerdoruesi);
                if (!autorizimet)
                    lidhur = true;

                //DbCore.DbShare.clsKusht kushtvartes = new DbCore.DbShare.clsKusht(clsKoka.IdKonfigAmbjente, "V");
                //DbCore.DbShare.clsAlternativaKushti alternativavartes = new DbCore.DbShare.clsAlternativaKushti(kushtvartes.Vlera);
                if (clsAlternativaKushti.getAlternativa(clsKoka.IdKonfigAmbjente, "V") == "Po")
                {
                    object[] arr = { konfig.KodKonfigAmbjente + " " + clsKoka.NrDok + " " + clsKoka.DtDok.ToShortDateString(), "Ky dokument eshte dokument vartes dhe nuk mund te riruhet!", nrreshta };
                    err.Rows.Add(arr);
                    continue;
                }

                clsKokaFleteKontabel kokfk = new clsKokaFleteKontabel(clsKoka.IdKoka, idKategoria);
                DbCore.DbQendraKosto.clsKokaQendraKosto qend = new DbCore.DbQendraKosto.clsKokaQendraKosto();
                qend.KtheKokaQKSipasIDGjeneruesDheKonfigMeTrup(kokfk.IdKokaFleteKontabel, kokfk.IdKonfigAmbjente);
                #region validime


                if (clsKoka.IdGrup1 != 0)
                {
                    DbCore.DbRegjistrim.clsGrupimDokumentiKoka grup = new clsGrupimDokumentiKoka(clsKoka.IdGrup1);
                    if (grup.IdGrupimKoka < 1)
                    {
                        object[] arr = { konfig.KodKonfigAmbjente + " " + clsKoka.NrDok + " " + clsKoka.DtDok.ToShortDateString(), rm.GetString("msgGrupimiIPareNukEkziston", ci), nrreshta };
                        err.Rows.Add(arr);
                        continue;
                    }

                }
                if (clsKoka.IdGrup2 != 0)
                {
                    DbCore.DbRegjistrim.clsGrupimDokumentiKoka grup = new clsGrupimDokumentiKoka(clsKoka.IdGrup2);
                    if (grup.IdGrupimKoka < 1)
                    {
                        object[] arr = { konfig.KodKonfigAmbjente + " " + clsKoka.NrDok + " " + clsKoka.DtDok.ToShortDateString(), rm.GetString("msgGrupimiDyteNukEkziston", ci), nrreshta };
                        err.Rows.Add(arr);
                        continue;
                    }

                }
                if (clsKoka.IdGrup3 != 0)
                {
                    DbCore.DbRegjistrim.clsGrupimDokumentiKoka grup = new clsGrupimDokumentiKoka(clsKoka.IdGrup3);
                    if (grup.IdGrupimKoka < 1)
                    {
                        object[] arr = { konfig.KodKonfigAmbjente + " " + clsKoka.NrDok + " " + clsKoka.DtDok.ToShortDateString(), rm.GetString("msgGrupimiITreteNukEkziston", ci), nrreshta };
                        err.Rows.Add(arr);
                        continue;
                    }

                }

                if (clsKoka.IdLlogKunderparti != 0)
                {
                    clsLlogari llogari = new clsLlogari(clsKoka.IdLlogKunderparti);

                    if (llogari.IdLlogari < 1)
                    {
                        object[] arr = { konfig.KodKonfigAmbjente + " " + clsKoka.NrDok + " " + clsKoka.DtDok.ToShortDateString(), rm.GetString("msgKjoLlogariNukEkziston", ci), nrreshta };
                        err.Rows.Add(arr);
                        continue;

                    }
                    else if (!clsLlogari.eshteLlogariAktive(llogari.NrLlogari, idNdermarrje))
                    {
                        object[] arr = { konfig.KodKonfigAmbjente + " " + clsKoka.NrDok + " " + clsKoka.DtDok.ToShortDateString(), rm.GetString("msgKjoLlogariNukEshteAktive", ci), nrreshta };
                        err.Rows.Add(arr);
                        continue;

                    }

                }

                if (clsKoka.IdMagazina != 0)
                {
                    DbCore.DbRegjistrim.clsNjesiAdministrative mag = new clsNjesiAdministrative(clsKoka.IdMagazina);
                    if (mag.IdNjesiAdministrative == -1)
                    {
                        object[] arr = { konfig.KodKonfigAmbjente + " " + clsKoka.NrDok + " " + clsKoka.DtDok.ToShortDateString(), rm.GetString("msgMagazinaNukEkziston", ci), nrreshta };
                        err.Rows.Add(arr);
                        continue;

                    }
                    else
                    {

                        if (mag.Aktiv == false)
                        {
                            object[] arr = { konfig.KodKonfigAmbjente + " " + clsKoka.NrDok + " " + clsKoka.DtDok.ToShortDateString(), rm.GetString("msgMagazinaNukEshteAktive", ci), nrreshta };
                            err.Rows.Add(arr);
                            continue;

                        }
                    }
                }
                if (clsKoka.IdDegeAdministrative != 0)
                {
                    DbCore.DbRegjistrim.clsDegeAdministrative dege = new clsDegeAdministrative(clsKoka.IdDegeAdministrative);
                    if (dege.IdDegeAdministrative == -1)
                    {
                        object[] arr = { konfig.KodKonfigAmbjente + " " + clsKoka.NrDok + " " + clsKoka.DtDok.ToShortDateString(), rm.GetString("msgKjoDegeAdministrativeNukEkziston", ci), nrreshta };
                        err.Rows.Add(arr);
                        continue;

                    }
                    else
                    {


                        if (dege.Aktiv == false)
                        {
                            object[] arr = { konfig.KodKonfigAmbjente + " " + clsKoka.NrDok + " " + clsKoka.DtDok.ToShortDateString(), rm.GetString("msgKjoDegeAdministrativeNukEshteAktive", ci), nrreshta };
                            err.Rows.Add(arr);
                            continue;

                        }
                    }
                }

                #endregion
                #region krijimi i kokes se re
                clsKokaNdryshimCmimSasi kokare = new clsKokaNdryshimCmimSasi();

                int meKontabilizim = clsAlternativaKushti.getAlternativa(clsKoka.IdKonfigAmbjente, "GJK") != "Jo" ? 1 : 0;


                string nrllogari = "", koddege = "", kodmagazina = "", kodgrup1 = "";


                if (clsKoka.IdDegeAdministrative != 0)
                {
                    clsDegeAdministrative dege = new clsDegeAdministrative(clsKoka.IdDegeAdministrative);
                    koddege = dege.Kodi;
                }


                DbCore.DbShare.clsKonfigurimAmbjenti konfmag = new DbCore.DbShare.clsKonfigurimAmbjenti(konfig.IdKonfigurimi, idGjuha);
                if (clsKoka.IdMagazina != 0)
                {
                    clsNjesiAdministrative magazina = new clsNjesiAdministrative(clsKoka.IdMagazina);
                    kodmagazina = magazina.Kodi;
                }
                string shfaqmesazhapolupe = "Jo", mesazhinformues = "", shfaqmesazhapolupemag = "Jo";

                if (clsKoka.IdGrup1 != 0)
                {
                    clsGrupimDokumentiKoka grup1 = new clsGrupimDokumentiKoka(clsKoka.IdGrup1);
                    kodgrup1 = grup1.Kodi;
                }

                if (clsKoka.IdLlogKunderparti != 0)
                {
                    clsLlogari llogari = new clsLlogari(clsKoka.IdLlogKunderparti);
                    nrllogari = llogari.NrLlogari;
                }

                int idstatusdok = 1;
                bool gjenerodokmag = clsAlternativaKushti.getAlternativa(konfig.IdKonfigAmbjente, "GJDM") == "Po";
                clsKokaRezervime rez = new clsKokaRezervime();
                rez.mbushKokaRezervimiSipasIDGjenerues(kokam.IdKokaMagazina, 2, kokam.IdKonfigAmbjente);

                mesazh = kokare.krijoNdryshimCmimSasi(clsKoka.IdKoka, clsKoka.IdNivel, clsKoka.IdKonfigAmbjente, clsKoka.IdMagazina, kodmagazina, clsKoka.DtDok, clsKoka.NrDok, clsKoka.Vlefta, idstatusdok, idNdermarrje, idNdermarrjeVit, idPerdoruesi, clsKoka.IdKrijues, clsKoka.DtRegjistrimi, clsKoka.IdDegeAdministrative, koddege, clsKoka.IdLlogKunderparti, nrllogari, clsKoka.IdGrup1, clsKoka.IdGrup2, clsKoka.IdGrup3, clsKoka.Pershkrimi, col, new clsKokaFleteKontabel(), out mesazhinformues, true, ref gjenerodokmag, konfmag);
                if (!mesazh.Status)
                {
                    object[] arr = { konfig.KodKonfigAmbjente + " " + clsKoka.NrDok + " " + clsKoka.DtDok.ToShortDateString(), mesazh.PershkrimMesazhi, nrreshta };
                    err.Rows.Add(arr);
                    continue;

                }
                kokare.IdKoka = clsKoka.IdKoka;
                #endregion

                string pershkrimFK;
                if (kokare.Pershkrimi != String.Empty)
                    pershkrimFK = kokare.Pershkrimi;
                else

                    pershkrimFK = "Nga ndryshim sasi/cmim";



                if (lidhur == true)
                    mesazh = kokare.modifiko(meKontabilizim, true, pershkrimFK, out shfaqmesazhapolupe, eshteOwn, qend.ColTrupi, out shfaqmesazhapolupemag, gjenerodokmag, rm, ci);
                else
                    mesazh = kokare.modifiko(meKontabilizim, false, pershkrimFK, out shfaqmesazhapolupe, eshteOwn, qend.ColTrupi, out shfaqmesazhapolupemag, gjenerodokmag, rm, ci);
                if (!mesazh.Status)
                {
                    object[] arr = { konfig.KodKonfigAmbjente + " " + clsKoka.NrDok + " " + clsKoka.DtDok.ToShortDateString(), mesazh.PershkrimMesazhi, nrreshta };
                    err.Rows.Add(arr);
                    continue;

                }
                //else
                //{
                //    object[] arr = { konfig.KodKonfigAmbjente + " " + clsKoka.NrDok + " " + clsKoka.DtDok.ToShortDateString(), mesazh.PershkrimMesazhi, nrreshta };
                //    err.Rows.Add(arr);
                //    continue;
                //}




            }
            DbCore.mySessionObjects.ruajTabeleGabimeshImporti(Session, err);
            if (err.Rows.Count > 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "U riruajten " + (rreshtat.Count - err.Rows.Count) + " rreshta dhe deshtuan " + err.Rows.Count + " rreshta! ", pnlMesazhi);
                Container.Attributes["src"] = "RaportiShpejte.aspx?Sesioni=false&emriReal=gabimeImporti&printo=false&db=jo";
            }
            else
            {
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "U riruajten te gjitha rreshtat!", pnlMesazhi);

            }
            this.grid_RegMag.Selection.UnselectAll();
            mbushGridDokumentNdryshimCmimSasiNgaDB();
            konfiguroGride(idGjuha, idNdermarrje, idPerdoruesi,rm,ci);


            if (rreshtat.Count == 0)
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("regjMagMesazhZgjidhniNje", ci), pnlMesazhi);

        }



        //fshin rreshtat e selektuar
        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            pergjigja.Text = "";
            List<object> rreshtat = grid_RegMag.GetSelectedFieldValues("IdKoka");
            if (rreshtat.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("regjMagMesazhZgjidhniNje", cultinf), pnlMesazhi);
                return;
            }
            List<string> TeFshire = new List<string>(), TeLidhur = new List<string>(), PeriudheKycur = new List<string>(), GjendjeNegative = new List<string>(), closedPeriod = new List<string>();
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            DbCore.DbRegjistrim.colTrupiMagazina tr = new DbCore.DbRegjistrim.colTrupiMagazina();
            DbCore.DbRegjistrim.colTrupiMagazina trupat = new DbCore.DbRegjistrim.colTrupiMagazina();
            bool rivleresim = false;
           // DbCore.DbAdmin.clsPeriudhaKontabel periudha;
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            foreach (object id in rreshtat)
            {
                DbCore.DbRegjistrim.clsKokaNdryshimCmimSasi kok = new DbCore.DbRegjistrim.clsKokaNdryshimCmimSasi();
                kok.IdKoka = Convert.ToInt32(id);
                kok.mbushKokaNdryshimCmimSasiSipasID(kok.IdKoka);
                bool kontrollorivleresim = clsAlternativaKushti.getAlternativa(kok.IdKonfigAmbjente, "KR") == "Po";
                bool lidhur = kok.eshteILidhur();
                bool autorizimet = DbCore.DbRegjistrim.clsKokaNdryshimCmimSasi.kaAutorizime(kok.IdKoka, mySessionObjects.ktheIdPerdoruesi(Session));
                if (!autorizimet)
                    lidhur = true;
                if (lidhur)
                {
                    TeLidhur.Add(kok.NrDok);
                    continue;
                }
                if (kok.IdStatusDok == 2)
                    continue;
                //periudha = new DbCore.DbAdmin.clsPeriudhaKontabel(kok.DtDok, idNdermarrje);
                //DbCore.clsMesazh mesazhi = periudha.isPeriudheKycur();
               
                bool ekycur = DbCore.DbAdmin.clsPeriudhaKontabel.eshteKycurPeriudheSipasDateDheNdermarrjes(kok.DtDok, idNdermarrje);
                if (ekycur)
                {
                    PeriudheKycur.Add(kok.NrDok);
                    continue;
                }

                if (DbCore.MbylljePeriudhe.PeriodClosing.IsPeriodClosed(kok.DtDok, DbCore.IMBUtils.DataBase.MyConnectionsManager.GetSelectedConNameServer(), kok.IdNdermarje, KategoriDokumenti.NdryshimSasiCmim, kok.IdKonfigAmbjente))
                {
                    closedPeriod.Add(kok.NrDok);
                    continue;
                }

                clsKokaMagazina kokm = new clsKokaMagazina();
                kokm.mbushKokaMagazinaSipasIDGjenerues(kok.IdKoka, 1, kok.IdKonfigAmbjente);
                if (kokm.IdKokaMagazina != 0)
                {
                    kokm.mbushTrupMagazine(false);
                    colArtikujt coleksistues1 = new colArtikujt(kokm.IdKokaMagazina, new clsDatabaseInventari());
                    int i2 = 0;
                    foreach (clsTrupiMagazina trup in kokm.OcolTrupiMagazina)
                    {
                        if (trup.IdLlojVeprimi == 1)
                            trup.Element = coleksistues1[i2];
                        i2++;
                    }
                    if (!kokm.kontrolloGjendjeNeFshirje(new DbCore.DbRegjistrim.colTrupiMagazina(), 0).Status)
                    {
                        GjendjeNegative.Add(kok.NrDok);
                        continue;
                    }
                }
                if (kokm.IdKokaMagazina != 0 && kontrollorivleresim && kok.IdStatusDok != 0)
                    if (kokm.rivleresim())
                    {
                        rivleresim = true; tr.mbushGjitheTrupiMagazinaNgaKoka(kokm.IdKokaMagazina); trupat.AddRange(tr);
                    }

                kok.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                mesazh = kok.fshi();
                DbCore.mySessionObjects.ruajTrupatNeSession(Session, trupat);
                if (mesazh.Status)
                {
                    #region Heq llogarite nga grida

                    hiqDokumentNdryshimCmimSasiNgaGrida(kok.IdKoka);
                    #endregion
                    TeFshire.Add(kok.NrDok);
                }
            }
            string mesazhInfoGabimLidhur = "", mesazhInfoGabimPeridheKycur = "", mesazhInfoSukses = "", mesazhGjendjeNegative = "", mesazhClosedPeriod = "";
            if (TeLidhur.Count == 1)
                mesazhInfoGabimLidhur = String.Format("{0}{1}", rm.GetString("regjMagPrefiksMesazhNjejes", cultinf), rm.GetString("regjMagSuffixMesazhNjejesLidhurGabimi", cultinf));
            else
                if (TeLidhur.Count > 1)
                    mesazhInfoGabimLidhur = String.Format("{0}{1}", rm.GetString("regjMagPrefixMesazhShumes", cultinf), rm.GetString("regjMagSuffixMesazhShumesLidhurGabimi", cultinf));
            if (PeriudheKycur.Count == 1)
                mesazhInfoGabimPeridheKycur = String.Format("{0}{1}{2}", rm.GetString("regjMagPrefiksMesazhNjejes", cultinf), String.Join(";", PeriudheKycur), rm.GetString("regjMagSuffixMesazhNjejesPeriudheKycurGabimi", cultinf));
            else
                if (PeriudheKycur.Count > 1)
                    mesazhInfoGabimPeridheKycur = String.Format("{0}{1}{2}", rm.GetString("regjMagPrefixMesazhShumes", cultinf), String.Join(";", PeriudheKycur), rm.GetString("regjMagSuffixMesazhShumesPeriudheKycurGabimi", cultinf));

            if (closedPeriod.Count == 1)
                mesazhClosedPeriod = String.Format("{0}{1}{2}", rm.GetString("regjisDokPrefixMesazhNjejes", ci), String.Join(", ", closedPeriod), rm.GetString("suffixMesazhClosedPeriodNjejes", ci));
            else if (closedPeriod.Count > 1)
                mesazhClosedPeriod = String.Format("{0}{1}{2}", rm.GetString("regjisDokPrefixMesazhNjejes", ci), String.Join(", ", closedPeriod), rm.GetString("suffixMesazhClosedPeriodShumes", ci));

            if (GjendjeNegative.Count == 1)
                mesazhGjendjeNegative = String.Format("{0}{1}{2}", rm.GetString("regjMagPrefiksMesazhNjejes", cultinf), String.Join(";", GjendjeNegative), rm.GetString("regjMagSuffixMesazhNjejesGjendjeNegative", cultinf));
            else
                if (GjendjeNegative.Count > 1)
                    mesazhGjendjeNegative = String.Format("{0}{1}{2}", rm.GetString("regjMagPrefixMesazhShumes", cultinf), String.Join(";", GjendjeNegative), rm.GetString("regjMagSuffixMesazhShumesGjendjeNegative", cultinf));
            if (TeFshire.Count == 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("regjMagPrefiksMesazhNjejes", cultinf), String.Join(";", TeFshire), rm.GetString("regjMagSuffixMesazhNjejesSuksesi", cultinf));
            else
                if (TeFshire.Count > 1)
                    mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("regjMagPrefixMesazhShumes", cultinf), String.Join(";", TeFshire), rm.GetString("regjMagSuffixMesazhShumesSuksesi", cultinf));

            mesazhInfoGabimLidhur += mesazhInfoGabimPeridheKycur + mesazhGjendjeNegative + mesazhClosedPeriod;
            if (mesazhInfoGabimLidhur != "" && mesazhInfoSukses != "")
            {
                mesazhInfoGabimLidhur += rm.GetString("regjMagLidhesMesazhi", cultinf) + mesazhInfoSukses;
                pergjigja.Text = mesazhInfoGabimLidhur;
                pergjigja.ClientVisible = false;
            }
            else
            {
                pergjigja.Text = mesazhInfoSukses;
                pergjigja.ClientVisible = false;
            }
            if (rivleresim)
                clsMenuInfo.ShtoPyetje(MenuInfo, rm.GetString("regjMagVeprimiSjellNdryshimNeCmimDalje", cultinf), pnlMesazhi, (int)hfState["idGjuha"]);
            else
                if (mesazhInfoGabimLidhur != "")
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhInfoGabimLidhur, pnlMesazhi);
                else
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazhInfoSukses, pnlMesazhi);

        }
        private void hiqDokumentNdryshimCmimSasiNgaGrida(int idkoka)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (this.grid_RegMag.DataSource != null)
            {
                DataTable dt = (DataTable)grid_RegMag.DataSource;
                DataRow[] drs = dt.Select("IdKoka = " + idkoka);
                if (drs.Length > 1)
                    throw new Exception(rm.GetString("regjMagMesazhGabimiNdodhen2DokMagazine", cultinf));
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                dt.Rows.Remove(dr);
                grid_RegMag.DataSource = dt;
                grid_RegMag.DataBind();
                dt.Dispose();
            }
            else mbushGridDokumentNdryshimCmimSasiNgaDB();
        }
        protected void grid_RegMag_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            if (e.CallbackName == "COLUMNMOVE" && grid_RegMag.AllColumns[int.Parse(e.Args[0])].Width.Value == 0)
                grid_RegMag.AllColumns[int.Parse(e.Args[0])].Width = Unit.Percentage(3);
            if (e.CallbackName == "APPLYFILTER" && e.Args[0] == "")
            {
                DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
                ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
                cmbFiltra.Text = "";


            }
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            GridUtil.ToolTipButonaveMbiGride(grid_RegMag, cultinf, rm);
        }

        protected void grid_RegMag_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = grid_RegMag.PageIndex;
            e.Properties["cpPageRow"] = grid_RegMag.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = grid_RegMag.VisibleRowCount;
        }

        protected void grid_RegMag_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string[] arr = e.Parameters.Split(';');
            GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), "grid_RegMag", grid_RegMag, cmbKonfigurimi.Text.Split(';')[0], "544", DbCore.mySessionObjects.ktheGjuhe(Session));
            if (arr.Length == 3)
            {
                if (arr[2] == "")
                {

                    grid_RegMag.FilterExpression = "[IdStatusDok]=1";
                }
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "grid_RegMag", komponente, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), int.Parse(cmbKonfigurimi.Value.ToString()));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        grid_RegMag.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, grid_RegMag);
                    }
                }
            }

            grid_RegMag.Selection.UnselectAll();
        }

        protected void grid_RegMag_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if (e.Column.FieldName == "IdMagazina" || e.Column.FieldName == "IdKonfigAmbjente" )
                if (Converter.ConvertToInt(e.Value)==0)
                {
                    e.Criteria = null;
                }
        }

        protected void grid_RegMag_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            string TeGjithe = rm.GetString("GridHeaderFilterFillItemTeGjithe", ci);
            string nga = rm.GetString("GridHeaderFilterFillItemNga", ci);
            if (e.Column.FieldName == "NrDok" || e.Column.FieldName == "Pershkrimi")
            {
                e.Values.Clear();
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

        protected void btnXlsxExport_Click(object sender, EventArgs e)
        {

            try
            {
                gridExport.WriteXlsxToResponse("faturat", true);
            }
            catch (Exception)
            { }
        }

        protected void btnPdfExport_Click(object sender, EventArgs e)
        {
            try
            {
                gridExport.WritePdfToResponse("faturat", true);
            }
            catch (Exception)
            { }
        }
    }
}