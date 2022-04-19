using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Web;
using System.Data;
using PlatinumWeb.Templates;
using System.Globalization;
using System.Resources;
using DbCore;
using DbCore.IMBUtils.Types;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{

    /// <summary>
    /// nderfaqja e skedulimit te prodhimit
    /// </summary>
    public partial class SkedulimProdhimi : MyPageBase
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
        private string guidString;
        private string komponente= "SkedulimProdhimi.aspx";

        /// <summary>
        /// vendos themen e faqes
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>

        /// <summary>
        /// kur lodohet faqja  mbushet me te dhena dhe kontrollohen ne eshte i autorizuar perdoruesi
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
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
            CultureInfo cultinf = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo(idGjuha);
            percaktoTemplateMenu(idGjuha, ASPxMenu1, idviti, idperdoruesi, idndermarje);
            if (!IsPostBack)
            {
                guidString = Guid.NewGuid().ToString();
                hfState.Set("guidString", guidString);
                ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idperdoruesi, idndermarje, cmbKonfigurimi, 96, rm, cultinf, idGjuha);
                cmbKonfigurimi.SelectedIndex = 0;
                DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()), idGjuha);
                hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
                mbushGridNgaDB();
                gvSkedulimi.FilterExpression = " [IdStatusDok]=1 ";
                konfiguroGride(idGjuha,rm,cultinf);
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idndermarje, "gvSkedulimi", gvSkedulimi, cmbKonfigurimi.Text.Split(';')[0], "808", idGjuha);
                if (Request.QueryString["fshi"] == "po")
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("regjisDokMesazhSukesFshirjeDokument", cultinf), pnlMesazhi);
                else if (Request.QueryString["ruaj"] == "po")
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("regjMagModifikimiPerfundoiMeSukses", cultinf), pnlMesazhi);
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idperdoruesi, idndermarje, idviti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
            }
            else
            {
                guidString = hfState.Get("guidString").ToString();
                mbushGridNgaSession();
                konfiguroGride(idGjuha, rm, cultinf);
            }
            GridUtil.konfigGrideListeEMadhePaTheme(gvSkedulimi, "IdKoka");
            clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idndermarje, "gvSkedulimi", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
            if (Request.QueryString["indexrow"] != null)
                gvSkedulimi.FocusedRowIndex = int.Parse(Request.QueryString["indexrow"]);
            AspxWebControlUtils.perkthePopUp(popFshi, rm.GetString("labelKujdes", cultinf), lblMsgbox, rm.GetString("labelAdministrimiMsgJeniSigurt", cultinf), ButtonCancel, rm.GetString("labelAnullo", cultinf));
            GridUtil.ToolTipButonaveMbiGride(gvSkedulimi, cultinf, rm);
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
        /// ruan konfigurimin e grides dhe filtrin e zgjedhur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RuajKolona_Click(object sender, EventArgs e)
        {
            int idfiltri = 0;
            int idndermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idperdorues = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            int idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            int idgjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            int idkonf = DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(cmbKonfigurimi.Text.Split(';')[0], idndermarrje);
            DbCore.clsMesazh mesazh = GridUtil.ruajFiltra(idndermarrje, idperdorues, idgjuha, "gvSkedulimi", komponente, "FilterDefault", gvSkedulimi.FilterExpression, gvSkedulimi, "IdNivel", idkonf, out idfiltri);//ruan filtrin e zgjedhur tek filtrat
            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                return;
            }
            mesazh = GridUtil.ruajkonfigurimgride(gvSkedulimi, cmbKonfigurimi.Text, idndermarrje, idperdorues, 808, idfiltri, idViti, DbCore.mySessionObjects.ktheCultureInfo(Session), DbCore.mySessionObjects.ktheGjuhe(Session));
            clsToolbarConfig.mbushComboBoxFiltra(idgjuha, idndermarrje, "gvSkedulimi", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
            percaktoTemplateMenu(idgjuha, ASPxMenu1, idViti, idperdorues, idndermarrje);
            if (mesazh.Status) clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
           
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
                gvSkedulimi.DataSource = tmpObject;
                gvSkedulimi.DataBind();
                tmpObject.Dispose();
            }
        }

        /// <summary>
        /// mbush griden me te dhena nga db
        /// </summary>
        private void mbushGridNgaDB()
        {//mbush griden e popupit me te dhena            
            DataTable dt = DbCore.DbProdhimi.colKokaSkedulimProdhimi.merrKokaSkedulimProdhimiDT(idnderviti, DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            DbCore.mySessionObjects.ruajGrideNeSession(Session, dt);
            gvSkedulimi.DataSource = dt;
            gvSkedulimi.DataBind();
            dt.Dispose();
        }

        /// <summary>
        /// konfiguron griden
        /// </summary>
        private void konfiguroGride(int idGjuha,ResourceManager rm,CultureInfo ci)
        {       
            KonfigurimComboGride.ShtoModel(gvSkedulimi, 97, idndermarje, idperdoruesi, idGjuha, Session, komponente, guidString);
            KonfigurimComboGride.ShtoNivel(gvSkedulimi, 97, idndermarje, idperdoruesi, idGjuha, Session, komponente, guidString);
            KonfigurimComboGride.ShtoBurim(gvSkedulimi, idndermarje, Session, komponente, guidString);
            KonfigurimComboGride.ShtoStatus(gvSkedulimi, rm, ci, "IdStatusDok");
            gvSkedulimi.Columns["#"].VisibleIndex = 0;
        }
   

        /// <summary>
        /// kur grida ben databound per ti shtuar kolonen e selektimit
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvSkedulimi_DataBound(object sender, EventArgs e)
        {
            if (gvSkedulimi.Columns["#"] == null)
            {
                GridViewCommandColumn check = new GridViewCommandColumn("#") { ShowSelectCheckbox = true, Width = System.Web.UI.WebControls.Unit.Percentage(2) };
                gvSkedulimi.Settings.ShowFilterRow = true;
                gvSkedulimi.Settings.ShowHeaderFilterButton = true;
                gvSkedulimi.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                gvSkedulimi.Settings.ShowFilterRowMenu = true;
                gvSkedulimi.Columns.Add(check);
                gvSkedulimi.Settings.ShowGroupPanel = true;
                gvSkedulimi.KeyFieldName = "IdKoka";
                gvSkedulimi.SettingsBehavior.AllowSelectByRowClick = true;
                gvSkedulimi.SettingsBehavior.AllowFocusedRow = true;
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
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvSkedulimi", komponente, idndermarje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                filtra.IdPerdoruesi = idperdoruesi;
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra();
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idndermarje, "gvSkedulimi", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
                percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), ASPxMenu1, idviti, idperdoruesi, idndermarje);
                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                gvSkedulimi.FilterExpression = " [IdStatusDok]=1  ";


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
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvSkedulimi", komponente, idndermarje, int.Parse(cmbKonfigurimi.Value.ToString()));
            DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida() { FiltraKodi = cmbFiltra.Text, FiltraShenime = cmbFiltra.Text, FiltraUniversal = false, GridaKokaId = koka.IdGridaKoka, FiltraVlera = gvSkedulimi.FilterExpression, IdPerdoruesi = idperdoruesi, IdNdermarje = idndermarje, IdStatusDok = 1 };
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("NrDok", gvSkedulimi);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvSkedulimi.GetSortedColumns();
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
            clsToolbarConfig.mbushComboBoxFiltra(idgjuha, idndermarje, "gvSkedulimi", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
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
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            List<string> TeFshire = new List<string>(), TeLidhur = new List<string>(), PeriudheKycur = new List<string>();

            //clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "", pnlMesazhi);
            List<object> rreshtat = gvSkedulimi.GetSelectedFieldValues("IdKoka");
            pergjigja.Text = "";
            //DbCore.DbAdmin.clsPeriudhaKontabel periudha;
            DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            foreach (object id in rreshtat)
            {
                DbCore.DbProdhimi.clsKokaSkedulimProdhimi clsKoka = new DbCore.DbProdhimi.clsKokaSkedulimProdhimi(Convert.ToInt32(id));

                bool lidhur = dbAdmin.eshteDokumentiILidhur(clsKoka.IdKoka, clsKoka.IdNivel, "T_KOKASKEDULIMPRODHIMI", "IDKOKA");
                bool autorizimet = DbCore.DbProdhimi.clsKokaPlanifikim.kaAutorizime(clsKoka.IdKoka, mySessionObjects.ktheIdPerdoruesi(Session));
                if (!autorizimet)
                    lidhur = true;
                if (lidhur)
                {
                    TeLidhur.Add(clsKoka.NrDok);
                    continue;
                }

                //periudha = new DbCore.DbAdmin.clsPeriudhaKontabel(clsKoka.DtDok, idndermarje);
                //DbCore.clsMesazh mesazhi = periudha.isPeriudheKycur();
                
                bool ekycur = DbCore.DbAdmin.clsPeriudhaKontabel.eshteKycurPeriudheSipasDateDheNdermarrjes(clsKoka.DtDok, idndermarje);
                if (ekycur)
                {
                    PeriudheKycur.Add(clsKoka.NrDok);
                    continue;
                }
                clsKoka.IdPerdoruesi = idperdoruesi;
                mesazh = clsKoka.fshi();
                if (mesazh.Status)
                {
                    #region Heq llogarite nga grida

                    hiqNgaGrida(clsKoka.IdKoka);
                    #endregion
                    TeFshire.Add(clsKoka.NrDok);

                }
            }
            dbAdmin.Dispose();
            string mesazhInfoGabimLidhur = "", mesazhInfoGabimPeridheKycur = "", mesazhInfoSukses = "";

            if (TeLidhur.Count == 1)
                mesazhInfoGabimLidhur = String.Format("{0}{1}{2}", rm.GetString("msgDokumentiSkedulimProdhimitMeNr", cultinf), String.Join(";", TeLidhur), rm.GetString("msgListaLidhjaSuffixNjejesGabimi", cultinf));
            else
                if (TeLidhur.Count > 1)
                    mesazhInfoGabimLidhur = String.Format("{0}{1}{2}", rm.GetString("msgDokumentatESkedulimProdhimitMeNr", cultinf), String.Join(";", TeLidhur), rm.GetString("msgListaLidhjaSuffixShumesGabimi", cultinf));
            if (PeriudheKycur.Count == 1)
                mesazhInfoGabimPeridheKycur = String.Format("{0}{1}{2}", rm.GetString("msgDokumentiSkedulimProdhimitMeNr", cultinf), String.Join(";", PeriudheKycur), rm.GetString("regjMagSuffixMesazhNjejesPeriudheKycurGabimi", cultinf));
            else
                if (PeriudheKycur.Count > 1)
                    mesazhInfoGabimPeridheKycur = String.Format("{0}{1}{2}", rm.GetString("msgDokumentatESkedulimProdhimitMeNr", cultinf), String.Join(";", PeriudheKycur), rm.GetString("regjMagSuffixMesazhShumesPeriudheKycurGabimi", cultinf));

            if (TeFshire.Count == 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("msgDokumentiSkedulimProdhimitMeNr", cultinf), String.Join(";", TeFshire), rm.GetString("suffixMesazhNjejesSuksesi", cultinf));
            else
                if (TeFshire.Count > 1)
                    mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("msgDokumentatESkedulimProdhimitMeNr", cultinf), String.Join(";", TeFshire), rm.GetString("suffixMesazhShumesSuksesi", cultinf));

            mesazhInfoGabimLidhur += mesazhInfoGabimPeridheKycur;
            if (mesazhInfoGabimLidhur != "" && mesazhInfoSukses != "")
            {
                mesazhInfoGabimLidhur += rm.GetString("msgLidhesMesazhi", cultinf) + mesazhInfoSukses;
                pergjigja.Text = mesazhInfoGabimLidhur;
                pergjigja.ClientVisible = false;
            }
            else
            {
                pergjigja.Text = mesazhInfoSukses;
                pergjigja.ClientVisible = false;
            }

            if (rreshtat.Count == 0)
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("regjMagMesazhZgjidhniNje", cultinf), pnlMesazhi);
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
        private void hiqNgaGrida(int idkoka)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (gvSkedulimi.DataSource != null)
            {
                DataTable dt = (DataTable)gvSkedulimi.DataSource;
                DataRow[] drs = dt.Select("IdKoka = " + idkoka);
                if (drs.Length > 1)
                    throw new Exception(rm.GetString("msgGabimNdodhen2DokumentaSkedulimProdhimiMeTeNjejtenId", cultinf));
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                dt.Rows.Remove(dr);
                gvSkedulimi.DataSource = dt;
                gvSkedulimi.DataBind();
                dt.Dispose();
            }
            else mbushGridNgaDB();
        }

        /// <summary>
        /// kur grida ben callback
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvSkedulimi_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            if (e.CallbackName == "COLUMNMOVE" && gvSkedulimi.AllColumns[int.Parse(e.Args[0])].Width.Value == 0)
                gvSkedulimi.AllColumns[int.Parse(e.Args[0])].Width = System.Web.UI.WebControls.Unit.Percentage(3);
            if (e.CallbackName == "APPLYFILTER" && e.Args[0] == "")
            {
                MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
                ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
                cmbFiltra.Text = "";
            }
            //  konfiguroGride();
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            GridUtil.ToolTipButonaveMbiGride(gvSkedulimi, cultinf, rm);
        }

        /// <summary>
        /// kur perdoruesi i ben grides callbak per te mare konfigurimin e ri
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvSkedulimi_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string[] arr = e.Parameters.Split(';');
            GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), "gvSkedulimi", gvSkedulimi, cmbKonfigurimi.Text.Split(';')[0], "808", DbCore.mySessionObjects.ktheGjuhe(Session));
            if (arr.Length == 3)
            {
                if (arr[2] == "")
                    gvSkedulimi.FilterExpression = " [IdStatusDok]=1 ";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    //filtra.mbushFiltraGridaSipasFiltraKodi(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvSkedulimi", komponente, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), int.Parse(cmbKonfigurimi.Value.ToString()));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvSkedulimi.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvSkedulimi);
                    }
                }
            }

            gvSkedulimi.Selection.UnselectAll();
        }

        /// <summary>
        /// perdoret per te vendosur karakteristika te griden ne javascript
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvSkedulimi_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvSkedulimi.PageIndex;
            e.Properties["cpPageRow"] = gvSkedulimi.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvSkedulimi.VisibleRowCount;
        }

        /// <summary>
        /// perdoret per te hequr filtrimin e grides kur zgjedhim elementin bosh
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvSkedulimi_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if (e.Column.FieldName == "IdPlanifikimi" ||
                 e.Column.FieldName == "IdBurimi" || e.Column.FieldName == "IdKonfigAmbjente" )
                if (Converter.ConvertToInt(e.Value) == 0)
                {
                    e.Criteria = null;
                }
           
        }

        /// <summary>
        /// perdoret per te shtuar menyra filtrimi
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvSkedulimi_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            string TeGjithe = rm.GetString("GridHeaderFilterFillItemTeGjithe", ci);
            string nga = rm.GetString("GridHeaderFilterFillItemNga", ci);
            if (e.Column.FieldName == "NrDok" || e.Column.FieldName == "Shenime")
            {
                e.Values.Clear();
                e.AddValue(TeGjithe, string.Empty, "true");
                e.AddValue(nga + " A-D", string.Empty, e.Column.FieldName + ">'A     ' and " + e.Column.FieldName + " <'DDDDDDD'");
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
    }
}