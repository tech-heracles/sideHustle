using DbCore;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Resources;
using DbCore.IMBUtils.Types;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.DataBase;
using DbCore.DbRegjistrim;

namespace PlatinumWeb
{

    /// <summary>
    /// nderfaqja e planifikimit te prodhimit
    /// </summary>
    public partial class RegjistrimAmortizimi : MyPageBase
    {
        /// <summary>
        /// id e ndermarjes
        /// </summary>
        private int idndermarje, /// <summary>
            /// id e perdoruesit
            /// </summary>
                                 idperdoruesi, /// <summary>
            /// id e ndermarje vitit
            /// </summary>
                                               idnderviti, idviti;

        /// <summary>
        /// vendos themen e faqes
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        private string komponente = "RegjistrimAmortizimi.aspx";
        private string guidString;
        /// <summary>
        /// kur lodohet faqja  mbushet me te dhena dhe kontrollohen ne eshte i autorizuar perdoruesi
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                DbCore.clsFunksione.logout(Session,true,"FaqePaautorizuar");
            }
            idperdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idperdoruesi);
            }
            idndermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            idviti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            idnderviti = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
            int idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            percaktoTemplateMenu(idGjuha, ASPxMenu1, idviti, idperdoruesi, idndermarje);
            CultureInfo cultinf = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo(idGjuha);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (!IsPostBack)
            {
                guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                hfState.Set("guidString", guidString);
                EmratEKontrolleve(rm, cultinf);
                ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idperdoruesi, idndermarje, cmbKonfigurimi, 88, rm, cultinf, idGjuha);
                cmbKonfigurimi.SelectedIndex = 0;
                DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()), idGjuha);
                hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
                mbushGridNgaDB();
                gvAmortizimi.FilterExpression = " [IdStatusDokumenti]=1";
                
                if (Request.QueryString["fshi"] == "po")
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("regjisDokMesazhSukesFshirjeDokument", cultinf), pnlMesazhi);
                else if (Request.QueryString["ruaj"] == "po")
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("regjMagModifikimiPerfundoiMeSukses", cultinf), pnlMesazhi);
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idperdoruesi, idndermarje, idviti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                konfiguroGride(idGjuha, rm, cultinf);
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idndermarje, "gvAmortizimi", gvAmortizimi, cmbKonfigurimi.Text.Split(';')[0], "1004", DbCore.mySessionObjects.ktheGjuhe(Session));
                guidString = (string)hfState["guidString"];
            }
            else

                mbushGridNgaSession();
            konfiguroGride(idGjuha, rm, cultinf);
            GridUtil.konfigGrideListeEMadhePaTheme(gvAmortizimi, "IdAmortizimi");
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idndermarje, "gvAmortizimi", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
            AspxWebControlUtils.perkthePopUp(popFshi, rm.GetString("labelKujdes", cultinf), lblMsgbox, rm.GetString("labelAdministrimiMsgJeniSigurt", cultinf), ButtonCancel, rm.GetString("labelAnullo", cultinf));
            gvAmortizimi.PercaktoTitlePanel(this, MenuInfo, pnlMesazhi, hfState, idperdoruesi, idndermarje, idviti, idGjuha, int.Parse(cmbKonfigurimi.Value.ToString()), "RegjistrimAmortizimi.aspx", rm, cultinf);
            if (Request.QueryString["indexrow"] != null)
            {
                gvAmortizimi.FocusedRowIndex = int.Parse(Request.QueryString["indexrow"]);
            }
        }

        private void EmratEKontrolleve(ResourceManager rm, CultureInfo cultinf)
        {
            konfigurimi_Label.Text = rm.GetString("lblModeli", cultinf);
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
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, komponente, this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
        }

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), ASPxMenu1, idviti, idperdoruesi, idndermarje);
        }

        /// <summary>
        /// mbush griden me te dhena te ruajtura ne sesion
        /// </summary>
        private void mbushGridNgaSession()
        {
            DataTable tmpObject;
            bool sukses = DbCore.mySessionObjects.merrGrideNgaSessioni(Session, out tmpObject);
            if (!sukses)
                mbushGridNgaDB();
            else
            {
                gvAmortizimi.DataSource = tmpObject;
                gvAmortizimi.DataBind();
                tmpObject.Dispose();
            }
        }

        /// <summary>
        /// mbush griden me te dhena nga db
        /// </summary>
        private void mbushGridNgaDB()
        {//mbush griden e popupit me te dhena            
            DataTable dt = DbCore.DbAsete.colAmortizimiKoka.ktheAmortizimKokaSipasDt(idnderviti, DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            DbCore.mySessionObjects.ruajGrideNeSession(Session, dt);
            gvAmortizimi.DataSource = dt;
            gvAmortizimi.DataBind();
            dt.Dispose();
        }

        /// <summary>
        /// konfiguron griden
        /// </summary>
        private void konfiguroGride(int idGjuha, ResourceManager rm, CultureInfo ci)
        {
            KonfigurimComboGride.ShtoStatus(gvAmortizimi, rm, ci, "IdStatusDokumenti");
            KonfigurimComboGride.ShtoModel(gvAmortizimi, 86, idndermarje, idperdoruesi, idGjuha, Session, komponente, guidString, "IdKonfigurimAmbjenti");
            KonfigurimComboGride.ShtoNivel(gvAmortizimi, 86, idndermarje, idperdoruesi, idGjuha, Session, komponente, guidString, "IdNiveli");
            KonfigurimComboGride.ShtoMagazinaNdermarrje(gvAmortizimi, Session, komponente, guidString, "IdNjesiAdministrative");
            KonfigurimComboGride.ShtoStandart(gvAmortizimi, idndermarje, Session, komponente, guidString, "IdLlojStandarti");
            KonfigurimComboGride.ShtoLlogariSipasNdermarrjesDhePerdoruesit(gvAmortizimi, idndermarje, idperdoruesi, Session, komponente, guidString, "IdLlogkunderparti");
            
            GridViewDataTextColumn col3 = gvAmortizimi.Columns["AmortizimiShteseTotal"] as GridViewDataTextColumn;
            col3.PropertiesEdit.DisplayFormatString = "0.00";
            gvAmortizimi.Columns["#"].VisibleIndex = 0;
        }

  
      /// <summary>
        /// kur grida ben databound per ti shtuar kolonen e selektimit
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvAmortizimi_DataBound(object sender, EventArgs e)
        {
            if (gvAmortizimi.Columns["#"] == null)
            {
                GridViewCommandColumn check = new GridViewCommandColumn("#") { ShowSelectCheckbox = true, Width = System.Web.UI.WebControls.Unit.Percentage(2) };
                gvAmortizimi.Settings.ShowFilterRow = true;
                gvAmortizimi.Settings.ShowHeaderFilterButton = true;
                gvAmortizimi.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                gvAmortizimi.Settings.ShowFilterRowMenu = true;
                gvAmortizimi.Columns.Add(check);
                gvAmortizimi.Settings.ShowGroupPanel = true;
                gvAmortizimi.KeyFieldName = "IdAmortizimi";
                gvAmortizimi.SettingsBehavior.AllowSelectByRowClick = true;
                gvAmortizimi.SettingsBehavior.AllowFocusedRow = true;
            }
        }

        /// <summary>
        /// fshin filtrin e zgjedhur dhe aplikuar mbi gride, fshirje nga DB ku ai eshte ruajtur, dhe jo pastrimi i grides nga aplikimi i filtrit
        /// per kete do sherbeje Clear Filter ne fund te grides
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;

            DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            //filtra.mbushFiltraGridaSipasFiltraEmri(cmbFiltra.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvAmortizimi", komponente, idndermarje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                filtra.IdPerdoruesi = idperdoruesi;
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra();
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idndermarje, "gvAmortizimi", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
                percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), ASPxMenu1, idviti, idperdoruesi, idndermarje);
                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                gvAmortizimi.FilterExpression = " [IdStatusDokumenti]=1";


            }
        }

        /// <summary>
        /// sherben per te ruajtur filtrin e zgjedhur
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
            int idgjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            //kap item qe ka template ne menune e kesaj faqeje
            MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvAmortizimi", komponente, idndermarje, int.Parse(cmbKonfigurimi.Value.ToString()));
            DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida() { FiltraKodi = cmbFiltra.Text, FiltraShenime = cmbFiltra.Text, FiltraUniversal = false, GridaKokaId = koka.IdGridaKoka, FiltraVlera = gvAmortizimi.FilterExpression, IdPerdoruesi = idperdoruesi, IdNdermarje = idndermarje, IdStatusDok = 1 };
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("NrDok", gvAmortizimi);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvAmortizimi.GetSortedColumns();
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
            //    filtri.KoloneRenditje = "NrDok";
            //    filtri.DrejtimRenditje = true;
            //}

            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            mesazh = filtri.ruaj();
            //mbushComboBoxFiltra();
            clsToolbarConfig.mbushComboBoxFiltra(idgjuha, idndermarje, "gvAmortizimi", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
            percaktoTemplateMenu(idgjuha, ASPxMenu1, idviti, idperdoruesi, idndermarje);
            if (mesazh.Status == true)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

            cmbFiltra.Text = "";
        }

        /// <summary>
        /// eventet e menuse
        /// </summary>
        /// <param name="source">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {

        }

        /// <summary>
        /// fshin rreshtat e selektuar
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            List<string> TeFshire = new List<string>(), TeLidhur = new List<string>(), PeriudheKycur = new List<string>(), closedPeriod = new List<string>();
            //clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "", pnlMesazhi);
            List<object> rreshtat = gvAmortizimi.GetSelectedFieldValues("IdAmortizimi");
            pergjigja.Text = "";
            //DbCore.DbAdmin.clsPeriudhaKontabel periudha;
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            foreach (object id in rreshtat)
            {
                DbCore.DbAsete.clsAmortizimiKoka clsKoka = new DbCore.DbAsete.clsAmortizimiKoka(Convert.ToInt32(id));

                bool ekycur = DbCore.DbAdmin.clsPeriudhaKontabel.eshteKycurPeriudheSipasDateDheNdermarrjes(clsKoka.DateDokumenti, idndermarje);
                if (ekycur)
                {
                    PeriudheKycur.Add(clsKoka.NrDok);
                    continue;
                }

                if (DbCore.MbylljePeriudhe.PeriodClosing.IsPeriodClosed(clsKoka.DateDokumenti, MyConnectionsManager.GetSelectedConNameServer(), clsKoka.IdNdermarrje, KategoriDokumenti.Amortizimi, clsKoka.IdKonfigurimAmbjenti))
                {
                    closedPeriod.Add(clsKoka.NrDok);
                    continue;
                }

                bool lidhur = clsKoka.eshteILidhur();
                if (lidhur)
                {
                    TeLidhur.Add(clsKoka.NrDok);
                    continue;
                }
                

                clsKoka.IdPerdoruesi = idperdoruesi;
                mesazh = clsKoka.fshiTrans(idperdoruesi, 86, false, new DbCore.DbAsete.colAmortizimiTrupi());
                if (mesazh.Status)
                {
                    #region Heq llogarite nga grida

                    hiqNgaGrida(clsKoka.IdAmortizimi, rm, ci);
                    #endregion
                    TeFshire.Add(clsKoka.NrDok);
                }
            }
            string mesazhInfoGabimLidhur = "", mesazhInfoGabimPeridheKycur = "", mesazhInfoSukses = "", mesazhClosedPeriod = "";
            if (TeLidhur.Count == 1)
                mesazhInfoGabimLidhur = String.Format("{0}{1}{2}", rm.GetString("msgAmortizimiPrefixNjejes", ci), String.Join(";", TeLidhur), rm.GetString("msgListaLidhjaSuffixNjejesGabimi", ci));
            else
                if (TeLidhur.Count > 1)
                    mesazhInfoGabimLidhur = String.Format("{0}{1}{2}", rm.GetString("msgAmortizimiPrefixShumes", ci), String.Join(";", TeLidhur), rm.GetString("msgListaLidhjaSuffixShumesGabimi", ci));
            if (PeriudheKycur.Count == 1)
                mesazhInfoGabimPeridheKycur = String.Format("{0}{1}{2}", rm.GetString("msgAmortizimiPrefixNjejes", ci), String.Join(";", PeriudheKycur), rm.GetString("regjMagSuffixMesazhNjejesPeriudheKycurGabimi", ci));
            else
                if (PeriudheKycur.Count > 1)
                    mesazhInfoGabimPeridheKycur = String.Format("{0}{1}{2}", rm.GetString("msgAmortizimiPrefixShumes", ci), String.Join(";", PeriudheKycur), rm.GetString("regjMagSuffixMesazhShumesPeriudheKycurGabimi", ci));

            if (closedPeriod.Count == 1)
                mesazhClosedPeriod = String.Format("{0}{1}{2}", rm.GetString("regjisDokPrefixMesazhNjejes", ci), String.Join(", ", closedPeriod), rm.GetString("suffixMesazhClosedPeriodNjejes", ci));
            else if (closedPeriod.Count > 1)
                mesazhClosedPeriod = String.Format("{0}{1}{2}", rm.GetString("regjisDokPrefixMesazhNjejes", ci), String.Join(", ", closedPeriod), rm.GetString("suffixMesazhClosedPeriodShumes", ci));

            if (TeFshire.Count == 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("msgAmortizimiPrefixNjejes", ci), String.Join(";", TeFshire), rm.GetString("suffixMesazhNjejesSuksesi", ci));
            else
                if (TeFshire.Count > 1)
                    mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("msgAmortizimiPrefixShumes", ci), String.Join(";", TeFshire), rm.GetString("suffixMesazhShumesSuksesi", ci));
            mesazhInfoGabimLidhur += mesazhInfoGabimPeridheKycur + mesazhClosedPeriod;
            if (mesazhInfoGabimLidhur != "" && mesazhInfoSukses != "")
            {
                mesazhInfoGabimLidhur += rm.GetString("msgLidhesMesazhi", ci) + mesazhInfoSukses;
                pergjigja.Text = mesazhInfoGabimLidhur;
                pergjigja.ClientVisible = false;
            }
            else
            {
                pergjigja.Text = mesazhInfoSukses;
                pergjigja.ClientVisible = false;
            }
            if (rreshtat.Count == 0)
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("regjMagMesazhZgjidhniNje", ci), pnlMesazhi);
            else
                if (mesazhInfoGabimLidhur != "")
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhInfoGabimLidhur, pnlMesazhi);
                else
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazhInfoSukses, pnlMesazhi);
        }

        /// <summary>
        /// heq nga grida reshtin e fshire
        /// </summary>
        /// <param name="idkoka">id e reshtit te fshire</param>
        private void hiqNgaGrida(int idkoka, ResourceManager rm, CultureInfo ci)
        {
            if (gvAmortizimi.DataSource != null)
            {
                DataTable dt = (DataTable)gvAmortizimi.DataSource;
                DataRow[] drs = dt.Select("IdAmortizimi = " + idkoka);
                if (drs.Length > 1)
                    throw new MyException(rm.GetString("msgAmortizimiJane2DokMeTeNjejtenIDNeGride", ci));
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                dt.Rows.Remove(dr);
                gvAmortizimi.DataSource = dt;
                gvAmortizimi.DataBind();
                dt.Dispose();
            }
            else mbushGridNgaDB();
        }

        /// <summary>
        /// kur grida ben callback
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvAmortizimi_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            GridUtil.ToolTipButonaveMbiGride(gvAmortizimi, ci, rm);
            if (e.CallbackName == "COLUMNMOVE" && gvAmortizimi.AllColumns[int.Parse(e.Args[0])].Width.Value == 0)
                gvAmortizimi.AllColumns[int.Parse(e.Args[0])].Width = System.Web.UI.WebControls.Unit.Percentage(3);
            if (e.CallbackName == "APPLYFILTER" && e.Args[0] == "")
            {
                MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
                ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
                cmbFiltra.Text = "";
            }
        }

        /// <summary>
        /// kur perdoruesi i ben grides callbak per te mare konfigurimin e ri
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvAmortizimi_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string[] arr = e.Parameters.Split(';');
            GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), "gvAmortizimi", gvAmortizimi, cmbKonfigurimi.Text.Split(';')[0], "1004", DbCore.mySessionObjects.ktheGjuhe(Session));
            if (arr.Length == 3)
            {
                if (arr[2] == "")
                    gvAmortizimi.FilterExpression = " [IdStatusDokumenti]=1";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    //filtra.mbushFiltraGridaSipasFiltraKodi(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvAmortizimi", komponente, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), int.Parse(cmbKonfigurimi.Value.ToString()));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvAmortizimi.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvAmortizimi);
                    }
                }
            }
            gvAmortizimi.Selection.UnselectAll();
        }

        /// <summary>
        /// perdoret per te vendosur karakteristika te griden ne javascript
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvAmortizimi_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvAmortizimi.PageIndex;
            e.Properties["cpPageRow"] = gvAmortizimi.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvAmortizimi.VisibleRowCount;
        }

        /// <summary>
        /// perdoret per te hequr filtrimin e grides kur zgjedhim elementin bosh
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvAmortizimi_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if (e.Column.FieldName == "IdNjesiAdministrative" || e.Column.FieldName == "IdKonfigAmbjente" || e.Column.FieldName == "IdLlogKunderparti" || e.Column.FieldName == "IdLlojStandarti")
                if ( Converter.ConvertToInt(e.Value)==0)
                {
                    e.Criteria = null;
                }

        }

        /// <summary>
        /// perdoret per te shtuar menyra filtrimi
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvAmortizimi_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            string TeGjithe = rm.GetString("GridHeaderFilterFillItemTeGjithe", ci);
            string nga = rm.GetString("GridHeaderFilterFillItemNga", ci);
            if (e.Column.FieldName == "NrDok" || e.Column.FieldName == "Shenime")
            {
                e.Values.Clear();
                e.AddValue(TeGjithe, string.Empty, "true");
                e.AddValue(nga + " A-D ", string.Empty, String.Format("{0}>'A     ' and {0} <'DDDDDDD'", e.Column.FieldName));
                e.AddValue(nga + " D-G ", string.Empty, String.Format("{0}>'D     ' and {0}<'GGGGGGG'", e.Column.FieldName));
                e.AddValue(nga + " H-K ", string.Empty, String.Format("{0}>'H     ' and {0}<'KKKKKKK'", e.Column.FieldName));
                e.AddValue(nga + " L-O ", string.Empty, String.Format("{0}>'L     ' and {0}  <'OOOOOOO'", e.Column.FieldName));
                e.AddValue(nga + " P-S ", string.Empty, String.Format("{0}>'P     ' and {0}<'SSSSSSS'", e.Column.FieldName));
                e.AddValue(nga + " T-W ", string.Empty, String.Format("{0}>'T     ' and {0}<'WWWWWWW'", e.Column.FieldName));
                e.AddValue(nga + " X-Z ", string.Empty, String.Format("{0}>'X     ' and {0}<'ZZZZZZZ'", e.Column.FieldName));
            }
            else
            {
                e.Values.Clear();
                e.AddValue(TeGjithe, string.Empty, "true");
            }
        }
    }
}