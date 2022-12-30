using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using DevExpress.Web;
using System.Data;
using PlatinumWeb.Templates;
using System.Web.Script.Serialization;
using System.Globalization;
using System.Resources;
using DbCore.DbAdmin;
using Newtonsoft.Json;
using DbCore;
using DbCore.IMBUtils.Logging;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    public partial class FushatShtese : MyPageBase
    {
        private const string prefixMesazhNjejes = "Modeli me Kod: ";
        private const string prefixMesazhShumes = "Modelet me Kod: ";
        private const string suffixMesazhNjejesGabimiLidhur = " eshte i lidhur dhe nuk mund te fshihet";
        private const string suffixMesazhShumesGabimiLidhur = " jane te lidhur dhe nuk mund te fshihen";
        private const string suffixMesazhNjejesSuksesi = " u fshi me sukses!";
        private const string suffixMesazhShumesSuksesi = " u fshine me sukses!";
        private const string lidhesMesazhi = " Kurse ";
        private const string gabim2aktivitetenegride = "GABIM: Ndodhen 2 modele me te njejten id ne gride";
        private const string mesazhZgjidhniNje = "Ju lutem zgjidhni te pakten nje model!";
        private int idperdoruesi, idnderviti, idNdermarrje, idviti, idgjuha;
        private const int idstatusdok = 1;
        private string komponente = "Shto_FushatShtese.aspx";
        private string guidString;

        protected void Page_Load(object sender, EventArgs e)
        {//kontrollon nese perdoruesi eshte i loguar
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                Response.Redirect(DbCore.IMBUtils.Paths.defaultLoginPath);
                return;
            }
            idperdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idperdoruesi);
                return;
            }
            idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            idviti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            idnderviti = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
            idgjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            if (Page.IsPostBack == false)
            {
                guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                hfState.Set("guidString", guidString);
                hfState.Set("idPerdoruesi", IdPerdoruesi);
                EmrateTabeve();
                ASPxPageControl1.ActiveTabIndex = 0;
                percaktoTemplateMenu(idgjuha, idviti, idperdoruesi, idNdermarrje, ASPxMenu1);

                mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje);
                konfiguroVleraFillestare(idperdoruesi, idNdermarrje);
                mbushGridNgaDB(idNdermarrje, idperdoruesi);

                konfiguroGrideFushaShtese(idNdermarrje);

                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idperdoruesi, idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idperdoruesi, idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), komponente);
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);

                DbCore.mySessionObjects.ruajRreshtiNeSesion(Session, 40);
                konfiguroGride(idperdoruesi, idNdermarrje);
                GridUtil.percaktoVisibleColumns(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, ASPxGridView_Modelet, "ASPxGridView_Modelet", komponente);

            }
            else
            {
                guidString = (string)hfState["guidString"];
                mbushGridNgaSession(idNdermarrje);
                konfiguroGride(idperdoruesi, idNdermarrje);
            }
            GridUtil.konfigGrideListeEMadhePaTheme(ASPxGridView_Modelet, "IdModeliFushaShtese");
            percaktoTemplateMenu(idgjuha, idviti, idperdoruesi, idNdermarrje, ASPxMenu1);

            percaktoTemplate();
            mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje);
            ASPxGridView_Modelet.Columns["#"].VisibleIndex = 0;
            GridUtil.EmrateButonaveMbiGride(ASPxGridView_Modelet);
        }

        /// <summary>
        /// Vendos emrat e tabeve ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateTabeve()
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            ASPxPageControl1.TabPages[0].Text = rm.GetString("labelRaportTePergjithshme", cultinf);
            ASPxPageControl1.TabPages[1].Text = rm.GetString("fushatShteseTab", cultinf);
            lblKodi.Text = rm.GetString("lblKodi", cultinf);
            lblLloji.Text = rm.GetString("lblLlojiModeli", cultinf);
            lblPershkrimi.Text = rm.GetString("lblPershkrimi", cultinf);
            lblAutorizimi.Text = rm.GetString("lblAutorizimi", cultinf);

        }

        /// <summary>
        /// mbush combon e filtrave
        /// </summary>
        /// <param name="idNdermarrje">id e ndermarjes</param>
        private static void mbushComboBoxFiltra(int idGjuha, int idNdermarrje)
        {
            clsToolbarConfig.mbushComboBoxFiltraMeValFieldTextField(idGjuha, idNdermarrje, "ASPxGridView_Modelet", "Shto_FushatShtese.aspx", "IdFiltra", "FiltraShenime", 1);
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
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, komponente, this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, hfShtimModifikim.Value == "modifikim" ? false : true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
        }

        protected void RuajKolona_Click(object sender, EventArgs e)
        {
            int idfiltri = 0;
            int idndermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idperdorues = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            int idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            int idgjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            int idkonf = 1;
            DbCore.clsMesazh mesazh = GridUtil.ruajFiltra(idndermarrje, idperdorues, idgjuha, "ASPxGridView_Modelet", komponente, "FilterDefault", ASPxGridView_Modelet.FilterExpression, ASPxGridView_Modelet, "KodiModeliFushaShtese", idkonf, out idfiltri);//ruan filtrin e zgjedhur tek filtrat
            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                return;

            }
            mesazh = GridUtil.ruajkonfigurimgride(ASPxGridView_Modelet, "", idndermarrje, idperdorues, 207, idfiltri, idViti, DbCore.mySessionObjects.ktheCultureInfo(Session), DbCore.mySessionObjects.ktheGjuhe(Session));/// ruan konfigurimin e grides dhe filtrin

            clsToolbarConfig.mbushComboBoxFiltra(idgjuha, idndermarrje, "ASPxGridView_Modelet", 1, komponente);

            percaktoTemplateMenu(idgjuha, idViti, idperdorues, idndermarrje, ASPxMenu1);
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
            percaktoTemplateMenu(idgjuha, idviti, idperdoruesi, idNdermarrje, ASPxMenu1);

        }

        protected void ASPxGridView_Modelet_DataBound(object sender, EventArgs e)
        {
            if (ASPxGridView_Modelet.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
                //perzgjidh
                GridViewCommandColumn check = new GridViewCommandColumn("#") { ShowSelectCheckbox = true, Width = System.Web.UI.WebControls.Unit.Percentage(2) };
                //behet per te afishuar rreshtin qe do sherbej per filtrim
                ASPxGridView_Modelet.Settings.ShowFilterRow = true;
                ASPxGridView_Modelet.Settings.ShowFilterRowMenu = true;
                ASPxGridView_Modelet.Columns.Add(check);
                ASPxGridView_Modelet.KeyFieldName = "IdModeliFushaShtese";
                ASPxGridView_Modelet.SettingsBehavior.AllowSelectByRowClick = true;
                ASPxGridView_Modelet.SettingsBehavior.AllowFocusedRow = true;
            }
        }

        private void konfiguroGride(int idPerdoruesi, int idNdermarrje)
        {//konfigurimet e grides

            KonfigurimComboGride.shtoLlojModeliFushaShtese(ASPxGridView_Modelet, Session, komponente, guidString);
            KonfigurimComboGride.ShtoAutorizimSipasPerdoruesit(ASPxGridView_Modelet, idPerdoruesi, Session, komponente, guidString, "IdNivelAutorizimi");
        }
        protected void ASPxGridView_Modelet_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            if (e.CallbackName == "COLUMNMOVE" && ASPxGridView_Modelet.AllColumns[int.Parse(e.Args[0])].Width.Value == 0)
                ASPxGridView_Modelet.AllColumns[int.Parse(e.Args[0])].Width = System.Web.UI.WebControls.Unit.Percentage(3);
            if (e.CallbackName == "APPLYFILTER" && e.Args[0] == "")
            {
                MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
                ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
                cmbFiltra.Text = "";

            }
        }

        protected void ASPxGridView_Modelet_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            if (e.Column.FieldName == "Pershkrimi" || e.Column.FieldName == "Kodi")
            {
                e.Values.Clear();
                e.AddValue("(Te gjithe)", string.Empty, "true");
                e.AddValue("Nga A-D ", string.Empty, String.Format("{0}>'A     ' and {0}<'DDDDDDD'", e.Column.FieldName));
                e.AddValue("Nga D-G ", string.Empty, String.Format("{0}>'D     ' and {0}<'GGGGGGG'", e.Column.FieldName));
                e.AddValue("Nga H-K ", string.Empty, String.Format("{0}>'H     ' and {0}<'KKKKKKK'", e.Column.FieldName));
                e.AddValue("Nga L-O ", string.Empty, String.Format("{0}>'L     ' and {0}<'OOOOOOO'", e.Column.FieldName));
                e.AddValue("Nga P-S ", string.Empty, String.Format("{0}>'P     ' and {0}<'SSSSSSS'", e.Column.FieldName));
                e.AddValue("Nga T-W ", string.Empty, String.Format("{0}>'T     ' and {0}<'WWWWWWW'", e.Column.FieldName));
                e.AddValue("Nga X-Z ", string.Empty, String.Format("{0}>'X     ' and {0}<'ZZZZZZZ'", e.Column.FieldName));
            }
            else
            {
                e.Values.Clear();
                e.AddValue("(Te gjithe)", string.Empty, "true");
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
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            //filtra.mbushFiltraGridaSipasFiltraEmri(cmbFiltra.Text, idNdermarrje);
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "ASPxGridView_Modelet", komponente, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje);
                percaktoTemplateMenu(idgjuha, idviti, idperdoruesi, idNdermarrje, ASPxMenu1);

                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                cmbFiltra.Text = "";

                hfStatusi.Value = "true";

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
            MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;

            DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida() { FiltraKodi = cmbFiltra.Text, FiltraShenime = cmbFiltra.Text, FiltraUniversal = false };
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "ASPxGridView_Modelet", komponente, idNdermarrje);
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = ASPxGridView_Modelet.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("Kodi", ASPxGridView_Modelet);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = ASPxGridView_Modelet.GetSortedColumns();
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
            DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();
            //oPerdorues = (DbCore.DbAdmin.clsPerdorues)(CacheLayer.GlobalCacheManager.MySessionCache["oClsPerdoruesi"]);
            oPerdorues = DbCore.mySessionObjects.kthePerdorues(Session);
            filtri.IdPerdoruesi = oPerdorues.IdPerdorues;
            filtri.IdNdermarje = idNdermarrje;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            filtri.IdStatusDok = 1;

            mesazh = filtri.ruaj();
            mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje);
            percaktoTemplateMenu(idgjuha, idviti, idperdoruesi, idNdermarrje, ASPxMenu1);

            if (mesazh.Status == true)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            hfStatusi.Value = "true";
            cmbFiltra.Text = "";

        }

        /// <summary>
        /// perdoret per te fshire rreshtat e zgjedhur te bankave nqs perdoruesi konfirmon fshirjen
        /// </summary>
        /// 
        /// <param name="sender"> derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ButtonOk_Click2(object sender, EventArgs e)
        {//fshin rreshtat e selektuar            
            List<object> rreshtat;
            if (ASPxPageControl1.ActiveTabIndex == 0)//nqs kemi zgjedhur vetem nje kartele per te pare fshijme vetem ate
                rreshtat = ASPxGridView_Modelet.GetSelectedFieldValues("IdModeliFushaShtese");
            else
            {
                rreshtat = new List<object>();
                rreshtat.Add(hfId.Value);
            }
            //List<object> rreshtat = ASPxGridView_Modelet.GetSelectedFieldValues("IdModeliFushaShtese");
            if (rreshtat.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhZgjidhniNje, pnlMesazhi);
                return;
            }
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            List<string> TeFshire = new List<string>(), TePaFshire = new List<string>();

            foreach (object id in rreshtat)
            {
                DbCore.DbAdmin.clsModeliFushaShtese fusha = new DbCore.DbAdmin.clsModeliFushaShtese(Convert.ToInt32(id));
                fusha.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                if (DbCore.DbAdmin.clsModeliFushaShtese.kaveprime(fusha.IdModeliFushaShtese))
                {
                    TePaFshire.Add(fusha.KodiModeliFushaShtese);
                    continue;
                }

                mesazh = fusha.fshi();
                if (fusha.IdModeliFushaShtese == 0)
                    continue;
                if (mesazh.Status)
                {
                    #region Heq llogarite nga grida
                    hiqNgaGrida(fusha.IdModeliFushaShtese, fusha.IdNdermarje);
                    #endregion
                    TeFshire.Add(fusha.KodiModeliFushaShtese);

                    ASPxPageControl1.ActiveTabIndex = 0;
                    hfStatusi.Value = "true";
                }
            }

            string mesazhInfoGabim = "", mesazhInfoSukses = "";
            if (TePaFshire.Count == 1)
                mesazhInfoGabim = String.Format("{0}{1}{2}", prefixMesazhNjejes, String.Join(";", TePaFshire), suffixMesazhNjejesGabimiLidhur);
            else
                if (TePaFshire.Count > 1)
                mesazhInfoGabim = String.Format("{0}{1}{2}", prefixMesazhShumes, String.Join(";", TePaFshire), suffixMesazhShumesGabimiLidhur);
            if (TeFshire.Count == 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", prefixMesazhNjejes, String.Join(";", TeFshire), suffixMesazhNjejesSuksesi);
            else
                if (TeFshire.Count > 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", prefixMesazhShumes, String.Join(";", TeFshire), suffixMesazhShumesSuksesi);
            if (mesazhInfoGabim != "" && mesazhInfoSukses != "")
                mesazhInfoGabim += lidhesMesazhi + mesazhInfoSukses;
            if (mesazhInfoGabim != "")
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhInfoGabim, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazhInfoSukses, pnlMesazhi);

        }

        /// <summary>
        /// heq nga grida rreshtat e fshire
        /// </summary>
        /// <param name="idndermarje">idndermarjes</param>
        /// <param name="idkoka"> idkoka</param>
        private void hiqNgaGrida(int idkoka, int idndermarje)
        {
            if (ASPxGridView_Modelet.DataSource != null)
            {
                DataTable dt = (DataTable)ASPxGridView_Modelet.DataSource;
                DataRow[] drs = dt.Select(String.Format("IdModeliFushaShtese = '{0}'", idkoka));
                if (drs.Length > 1)
                    throw new Exception(gabim2aktivitetenegride);
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                dt.Rows.Remove(dr);
                ASPxGridView_Modelet.DataBind();
            }
            else mbushGridNgaDB(idndermarje, idperdoruesi);
        }

        /// <summary>
        /// shton nte gride rreshtat e rinj
        /// </summary>
        /// <param name="idNdermarrje">idndermarje</param>
        /// <param name="idkoka">id e kokes</param>
        private void shtoNeGrid(int idNdermarrje, int idkoka)
        {
            if (ASPxGridView_Modelet.DataSource != null)
            {
                DataTable dt = (DataTable)ASPxGridView_Modelet.DataSource;
                DataRow[] drs = dt.Select("IdModeliFushaShtese = " + idkoka);
                if (drs.Length > 0)
                    throw new Exception(gabim2aktivitetenegride);
                DataRow newArtDr = DbCore.DbAdmin.colModeletFushaShtese.merrModeletFushaShteseDR(idkoka);
                dt.ImportRow(newArtDr);
            }
            else mbushGridNgaDB(idNdermarrje, idperdoruesi);
        }

        /// <summary>
        /// modifikon ne gride aktivitetin e modifikuar
        /// </summary>
        /// <param name="idNdermarrje">idndermarje</param>
        ///<param name="idkoka">id koka</param>
        private void modifikoNeGrid(int idNdermarrje, int idkoka)
        {
            if (ASPxGridView_Modelet.DataSource != null)
            {
                DataTable dt = (DataTable)ASPxGridView_Modelet.DataSource;
                DataRow[] drs = dt.Select("IdModeliFushaShtese = " + idkoka);
                if (drs.Length > 1)
                    throw new Exception(gabim2aktivitetenegride);
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                DataRow newArtDr = DbCore.DbAdmin.colModeletFushaShtese.merrModeletFushaShteseDR(idkoka);
                object[] arr = newArtDr.ItemArray;
                dr.ItemArray = arr;
            }
            else mbushGridNgaDB(idNdermarrje, idperdoruesi);
        }

        /// <summary>
        /// perdoret per te trajtuar ngjarjet e butonave te menuse. 
        /// </summary>
        /// <param name="source"> derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {//kryen veprimet e menuse
            if (e.Item.Name == "Ruaj")
            {
                Page.Validate("entries");
                ruajModel();
            }

        }


        private void konfiguroVleraFillestare(int idPerdoruesi, int idNdermarrje)
        {
            ASPxPageControl1.ActiveTabIndex = 0;
            ConfigureAspxComboBox.percaktoTemplateComboMeEnableCallback(cmbAutorizimi);
            ConfigureAspxComboBox.mbushComboLlojModeli(cmbLloji, false);
            MbushGrideMeFushaBosh();
            ConfigureAspxComboBox.mbushComboAutorizime(idPerdoruesi, cmbAutorizimi);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmbAutorizimi);
        }
        /// <summary>
        /// mbush griden nga sesioni
        /// </summary>
        /// <param name="idNdermarrje">id e ndermarjes</param>
        private void mbushGridNgaSession(int idNdermarrje)
        {
            DataTable tmpObject;
            bool sukses = DbCore.mySessionObjects.merrGrideNgaSessioni(Session, out tmpObject);
            if (!sukses)
                mbushGridNgaDB(idNdermarrje, idperdoruesi);
            else
            {
                ASPxGridView_Modelet.DataSource = tmpObject;
                ASPxGridView_Modelet.DataBind();
                tmpObject.Dispose();
            }
        }

        /// <summary>
        /// mbush griden nga databaza
        /// </summary>
        /// <param name="idNdermarrje">id e ndermarjes</param>
        private void mbushGridNgaDB(int idNdermarrje, int idperdoruesi)
        {//mbush griden e popupit me te dhena         

            DataTable dt = DbCore.DbAdmin.colModeletFushaShtese.ktheGjitheModeletFushaShteseDT(idNdermarrje, idperdoruesi);
            DbCore.mySessionObjects.ruajGrideNeSession(Session, dt);
            ASPxGridView_Modelet.DataSource = dt;
            ASPxGridView_Modelet.DataBind();
            dt.Dispose();
        }


        //sherben per te ruajtur nje modeli
        private void ruajModel()
        {
            DbCore.DbAdmin.clsModeliFushaShtese model;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            if (Page.IsValid == false)
                return;
            else
            {
                int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                var idVitNdermarrje = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
                if (isValidModel(idNdermarrje, idPerdoruesi))
                {
                    try
                    {
                        model = krijoModel(idNdermarrje, idPerdoruesi);
                    }
                    catch (Exception e)
                    {
                        ImbLogger.Error(e);
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, e.Message, pnlMesazhi);
                        hfStatusi.Value = "false";
                        return;
                    }
                    bool eshteShtim;
                    DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();


                    tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idperdoruesi, idNdermarrje, idVitNdermarrje, komponente);

                    if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
                    {
                        if (!tedrejtaInfo.DShtim)
                        {
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                            hfStatusi.Value = "false";
                            return;
                        }
                        mesazh = model.ruaj();
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

                        eshteShtim = false;
                        model.IdModeliFushaShtese = int.Parse(hfId.Value);

                        mesazh = model.modifiko();

                    }
                    if (mesazh.Status == true)
                    {
                        clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                        if (eshteShtim)
                            shtoNeGrid(idNdermarrje, model.IdModeliFushaShtese);
                        else //modifikim
                            modifikoNeGrid(idNdermarrje, model.IdModeliFushaShtese);
                        hfStatusi.Value = "true";
                    }
                    else
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                        hfStatusi.Value = "false";
                    }
                    ASPxPageControl1.ActiveTabIndex = 0;
                }
            }
        }
        private DbCore.DbAdmin.clsModeliFushaShtese krijoModel(int idNdermarrje, int idPerdoruesi)
        {//krijon nje model te ri sipas te dhenave te plotesuara nga perdoruesi

            DbCore.DbAdmin.clsModeliFushaShtese model = new DbCore.DbAdmin.clsModeliFushaShtese(0, txtKodi.Text, txtPershkrimi.Text, int.Parse(this.cmbLloji.SelectedItem.Value.ToString()), idPerdoruesi, idNdermarrje, 1, cmbAutorizimi.Text, MerrFushaShteseNgaHiddenField(false));
           

            return model;
        }

        //kontrollon nese ekziston kodi i modelit
        private bool isValidModel(int idNdermarrje, int idPerdoruesi)
        {
            bool isValid;
            isValid = true;
            if (cmbLloji.SelectedIndex == -1)
            {
                isValid = false;
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Jepni llojin!", pnlMesazhi);
                percaktoTamplateModele(idNdermarrje, idPerdoruesi);
            }

            return isValid;
        }
        /// cakton properti ne javascript
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxGridView_Modelet_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = ASPxGridView_Modelet.PageIndex;
            e.Properties["cpPageRow"] = ASPxGridView_Modelet.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = ASPxGridView_Modelet.VisibleRowCount;
        }
        /// <summary>
        /// kur grida ben callback
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxGridView_Modelet_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string idkomponente = "";
            string kodkonfigurimi = "";

            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                kodkonfigurimi = arr[1];
                idkomponente = arr[0];
                if (arr[2] == "")
                    ASPxGridView_Modelet.FilterExpression = "";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    //filtra.mbushFiltraGridaSipasFiltraKodi(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "ASPxGridView_Modelet", komponente, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        ASPxGridView_Modelet.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, ASPxGridView_Modelet);
                        hfStatusi.Value = "true";
                    }
                    else hfStatusi.Value = "false";
                }
            }
            else if (arr.Length == 2)//nese eshte zgjedhur nje konfigurim tek combo e konfigurimeve
            {
                kodkonfigurimi = arr[1];
                idkomponente = arr[0];
            }
            else
            {
                idkomponente = e.Parameters;
            }

            ASPxGridView_Modelet.Selection.UnselectAll();
        }


        /// <summary>
        /// perdoret per te shfaqur po jo tek komboja e aktivit
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxGridView_Modelet_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {

        }

        /// <summary>
        /// per filtrimin me elementin bosh
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxGridView_Modelet_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {

        }

        private void percaktoTamplateModele(int idNdermarrje, int idPerdoruesi)
        {//tempatet per kolonat e llogarine

            GridViewDataComboBoxColumn col7 = ASPxGridView_Modelet.Columns["IdNivelAutorizimi"] as GridViewDataComboBoxColumn;
            col7.EditItemTemplate = new MyTemplateAutorizime(idNdermarrje, idPerdoruesi);
            col7.Width = 100;
        }

        #region fushatShtese
        private void konfiguroGrideFushaShtese(int idNdermarrje)
        {//konfigurimet e grides e fushave shtese
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            GridUtil.percaktoVisibleColumnsMeWidth(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, grid_fushatShtese, "grid_fushatShtese", komponente);
            GridUtil.konfiguroGrideRegjistrimEvogelPaTheme(grid_fushatShtese, "IdFushaShtese", false);

            grid_fushatShtese.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;

        }

        private void MbushGrideMeFushaBosh()
        {
            var colfushat = new colFushatShtese();
            for (int i = 0; i < 40; i++)
            {

                colfushat.Add(new clsFushaShtese
                {
                    IdFushaShtese = (-i - 1),
                    Shfaq = true,
                    Lejueshme = true,
                    GjatesiaFushaShtese = 0,
                    IdGjuha = idgjuha

                });
            }
            grid_fushatShtese.DataSource = colfushat;
            grid_fushatShtese.DataBind();

        }

        private void MbushGrideMeFushaTePlotesuara(int idModeli)
        {//mbush griden me te dhena

            var colfushat = new DbCore.DbAdmin.colFushatShtese(idModeli);
            if (colfushat.Count >= 40)
            {

                colfushat.Add(new DbCore.DbAdmin.clsFushaShtese
                {
                    IdFushaShtese = (-colfushat.Count - 1),
                    Shfaq = true,
                    Lejueshme = true,
                    IdGjuha = idgjuha
                });
                //TOCHECK
                HiddenField2.Value = colfushat.Count.ToString();
            }
            else
            {
                for (int i = colfushat.Count; i < 40; i++)
                {
                    colfushat.Add(new DbCore.DbAdmin.clsFushaShtese
                    {
                        IdFushaShtese = (-i - 1),
                        Shfaq = true,
                        Lejueshme = true,
                        IdGjuha = idgjuha
                    });
                }
            }

            grid_fushatShtese.DataSource = colfushat;
            DbCore.mySessionObjects.RuajNeSession(Session, colfushat, $"{guidString}_TrupiGridaFushaShtese");
            DbCore.mySessionObjects.ruajRreshtiNeSesion(Session, colfushat.Count);
            grid_fushatShtese.DataBind();


        }

        private void percaktoTemplate()
        {// percaktohen tipet e kolonave per griden e fushave shtese
            GridViewDataTextColumn col1 = grid_fushatShtese.Columns["PershkrimiFushaShtese"] as GridViewDataTextColumn;
            col1.VisibleIndex = 1;
            col1.DataItemTemplate = new MyTextTemplate();
            GridViewDataTextColumn col2 = grid_fushatShtese.Columns["TipiFushaShtese"] as GridViewDataTextColumn;
            col2.VisibleIndex = 0;
            col2.DataItemTemplate = new MyComboTemplate();
            GridViewDataTextColumn col3 = grid_fushatShtese.Columns["GjatesiaFushaShtese"] as GridViewDataTextColumn;
            col3.VisibleIndex = 2;
            col3.DataItemTemplate = new MyTextTemplate();

            GridViewDataTextColumn col4 = grid_fushatShtese.Columns["AtiTipiFushaShtese"] as GridViewDataTextColumn;
            col4.VisibleIndex = 3;
            col4.DataItemTemplate = new MyComboTemplate();
            GridViewDataTextColumn col10 = grid_fushatShtese.Columns["VlereDefault"] as GridViewDataTextColumn;
            col10.VisibleIndex = 4;
            col10.DataItemTemplate = new MyTextTemplate();

            GridViewDataCheckColumn col5 = grid_fushatShtese.Columns["Shfaq"] as GridViewDataCheckColumn;
            col5.VisibleIndex = 5;
            col5.DataItemTemplate = new MyCheckTemplate(false, false);

            GridViewDataCheckColumn col6 = grid_fushatShtese.Columns["Lejueshme"] as GridViewDataCheckColumn;
            col6.VisibleIndex = 6;
            col6.DataItemTemplate = new MyCheckTemplate(false, false);

            GridViewDataCheckColumn col7 = grid_fushatShtese.Columns["Detyrueshme"] as GridViewDataCheckColumn;
            col7.VisibleIndex = 7;
            col7.DataItemTemplate = new MyCheckTemplate(false, false);

            GridViewDataTextColumn col8 = grid_fushatShtese.Columns["Kodi"] as GridViewDataTextColumn;
            col8.VisibleIndex = 8;
            col8.DataItemTemplate = new MyTextTemplate();

            GridViewDataTextColumn col9 = grid_fushatShtese.Columns["PershkrimiEng"] as GridViewDataTextColumn;
            col9.VisibleIndex = 9;
            col9.DataItemTemplate = new MyTextTemplate();

            GridViewDataColumn col11 = grid_fushatShtese.Columns["Shenime"] as GridViewDataColumn;
            col11.VisibleIndex = 10;
            col11.DataItemTemplate = new MyMemoTemplate();
        }

        protected void cmbAutorizimi_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("cmbAutorizimi"))
                {
                    ConfigureAspxComboBox.mbushComboAutorizime(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), cmbAutorizimi);
                }
            }
        }

        private DbCore.DbAdmin.colFushatShtese MerrFushaShteseNgaHiddenField(bool rreshtabosh)
        {
            var serializationSettings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            var dokumenti = JsonConvert.DeserializeObject<colFushatShtese>(gridDataObject.Value, serializationSettings);
            if (dokumenti == null || dokumenti.Count == 0)
                return new colFushatShtese();
            return new colFushatShtese(dokumenti.Where(fusha => !string.IsNullOrEmpty(fusha.PershkrimiFushaShtese) || rreshtabosh));
        }
        protected void grid_fushatShtese_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        { //sherben per te marre me vone vlerat e futura nga perdoruesi me ane te javascriptit

            if (e.RowType == GridViewRowType.Data)
            {
                ASPxGridView grid = (ASPxGridView)sender;
                GridViewDataColumn colPershkrimi = grid.Columns["PershkrimiFushaShtese"] as GridViewDataColumn;
                GridViewDataColumn colTipi = grid.Columns["TipiFushaShtese"] as GridViewDataColumn;
                GridViewDataColumn colGjatesia = grid.Columns["GjatesiaFushaShtese"] as GridViewDataColumn;
                GridViewDataColumn colPrindi = grid.Columns["AtiTipiFushaShtese"] as GridViewDataColumn;
                GridViewDataColumn colShfaq = grid.Columns["Shfaq"] as GridViewDataColumn;
                GridViewDataColumn colDetyrueshme = grid.Columns["Detyrueshme"] as GridViewDataColumn;
                GridViewDataColumn colLejueshme = grid.Columns["Lejueshme"] as GridViewDataColumn;
                GridViewDataColumn colVlereDefault = grid.Columns["VlereDefault"] as GridViewDataColumn;
                GridViewDataColumn colKodi = grid.Columns["Kodi"] as GridViewDataColumn;
                GridViewDataColumn colPershkrimiEng = grid.Columns["PershkrimiEng"] as GridViewDataColumn;
                GridViewDataColumn colShenime = grid.Columns["Shenime"] as GridViewDataColumn;
                ASPxTextBox txtPershkrimi = grid.FindRowCellTemplateControl(e.VisibleIndex, colPershkrimi, "txtBox") as ASPxTextBox;
                ASPxComboBox cmbTipi = grid.FindRowCellTemplateControl(e.VisibleIndex, colTipi, "cmbBox") as ASPxComboBox;
                ASPxTextBox txtGjatesia = grid.FindRowCellTemplateControl(e.VisibleIndex, colGjatesia, "txtBox") as ASPxTextBox;
                ASPxComboBox cmbPrindi = grid.FindRowCellTemplateControl(e.VisibleIndex, colPrindi, "cmbBox") as ASPxComboBox;
                ASPxCheckBox ckbShfaq = grid.FindRowCellTemplateControl(e.VisibleIndex, colShfaq, "cb") as ASPxCheckBox;
                ASPxCheckBox ckbDetyrueshme = grid.FindRowCellTemplateControl(e.VisibleIndex, colDetyrueshme, "cb") as ASPxCheckBox;
                ASPxCheckBox ckbLejueshme = grid.FindRowCellTemplateControl(e.VisibleIndex, colLejueshme, "cb") as ASPxCheckBox;
                ASPxTextBox txtVlereDefault = grid.FindRowCellTemplateControl(e.VisibleIndex, colVlereDefault, "txtBox") as ASPxTextBox;
                ASPxTextBox txtKodi = grid.FindRowCellTemplateControl(e.VisibleIndex, colKodi, "txtBox") as ASPxTextBox;
                ASPxMemo txtShenime = grid.FindRowCellTemplateControl(e.VisibleIndex, colShenime, "txtBox") as ASPxMemo;
                ASPxTextBox txtPershkrimiEng = grid.FindRowCellTemplateControl(e.VisibleIndex, colPershkrimiEng, "txtBox") as ASPxTextBox;
               
                if (txtPershkrimi != null && txtGjatesia != null & cmbTipi != null)
                {
                    txtPershkrimi.ClientInstanceName = "txtPershkrimi" + e.VisibleIndex.ToString();
                    cmbTipi.ClientInstanceName = "cmbTipi" + e.VisibleIndex.ToString();
                    cmbTipi.Items.Add("Titull", 0);
                    cmbTipi.Items.Add("String", 1);
                    cmbTipi.Items.Add("Integer", 2);
                    cmbTipi.Items.Add("Double", 3);
                    cmbTipi.Items.Add("Date", 4);
                    cmbTipi.Items.Add("CheckBox", 5);
                    cmbTipi.Items.Add("ListBox", 6);
                    if (cmbTipi.Text == "")
                        cmbTipi.SelectedIndex = 0;
                    else if (cmbTipi.Value.ToString() == "6")
                    {
                        cmbPrindi.ClientEnabled = true;
                        MbushComboPrindi(cmbPrindi, Convert.ToInt32(e.KeyValue), Convert.ToInt32(cmbPrindi.Value));
                    }
                    else
                        cmbPrindi.ClientEnabled = false;

                    txtGjatesia.ClientInstanceName = "txtGjatesia" + e.VisibleIndex.ToString();
                    txtKodi.ClientInstanceName = "txtKodi" + e.VisibleIndex;
                    txtPershkrimiEng.ClientInstanceName = "txtPershkrimiEng" + e.VisibleIndex;
                    txtVlereDefault.ClientInstanceName = "txtVlereDefault" + e.VisibleIndex;
                    ckbShfaq.ClientInstanceName = "ckbShfaq" + e.VisibleIndex;
                    ckbLejueshme.ClientInstanceName = "ckbLejueshme" + e.VisibleIndex;
                    ckbDetyrueshme.ClientInstanceName = "ckbDetyrueshme" + e.VisibleIndex;
                    txtShenime.ClientInstanceName = "txtShenime" + e.VisibleIndex; 
                    txtPershkrimi.ClientSideEvents.TextChanged = "function(s,e){ ShtoPershkrim(txtPershkrimi" + e.VisibleIndex.ToString() + ",txtGjatesia" + e.VisibleIndex.ToString() + ",cmbTipi" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + ");}";//"," + e.GetValue("Muaj").ToString() + ;
                    txtGjatesia.ClientSideEvents.TextChanged = "function(s,e){ShtoGjatesi(txtGjatesia" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + ");}";//"," + e.GetValue("Muaj").ToString() + ;
                    cmbTipi.ClientSideEvents.TextChanged = "function(s,e){ShtoTip(cmbTipi" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + ");}";

                    cmbPrindi.ClientInstanceName = "cmbPrindi" + e.VisibleIndex.ToString();
                    cmbPrindi.ClientSideEvents.SelectedIndexChanged = "function(s,e){ShtoPrind(cmbPrindi" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + ");}";
                    txtKodi.ClientSideEvents.TextChanged = $"function(s, e){{ ShtoPershkrim(txtKodi{e.VisibleIndex}, txtGjatesia{e.VisibleIndex},cmbTipi{ e.VisibleIndex},{ e.VisibleIndex}); }}";
                    txtVlereDefault.ClientSideEvents.TextChanged = $"function(s, e){{ ShtoPershkrim(txtVlereDefault{e.VisibleIndex}, txtGjatesia{e.VisibleIndex},cmbTipi{ e.VisibleIndex},{ e.VisibleIndex}); }}";
                    txtPershkrimiEng.ClientSideEvents.TextChanged = $"function(s, e){{ ShtoPershkrim(txtPershkrimiEng{e.VisibleIndex}, txtGjatesia{e.VisibleIndex},cmbTipi{ e.VisibleIndex},{ e.VisibleIndex}); }}";
                    ckbShfaq.ClientSideEvents.CheckedChanged = $"function(s, e){{ ShtoPershkrim(ckbShfaq{e.VisibleIndex}, txtGjatesia{e.VisibleIndex},cmbTipi{ e.VisibleIndex},{ e.VisibleIndex}); }}";
                    ckbLejueshme.ClientSideEvents.CheckedChanged = $"function(s, e){{ ShtoPershkrim(ckbLejueshme{e.VisibleIndex}, txtGjatesia{e.VisibleIndex},cmbTipi{ e.VisibleIndex},{ e.VisibleIndex}); }}";
                    ckbDetyrueshme.ClientSideEvents.CheckedChanged = $"function(s, e){{ ShtoPershkrim(ckbDetyrueshme{e.VisibleIndex}, txtGjatesia{e.VisibleIndex},cmbTipi{ e.VisibleIndex},{ e.VisibleIndex}); }}";

                    int rows = 1;
                    if (txtShenime != null)

                    {
                        txtShenime.ClientSideEvents.TextChanged = $"function(s, e){{ ShtoPershkrim(txtShenime{e.VisibleIndex}, txtGjatesia{e.VisibleIndex},cmbTipi{ e.VisibleIndex},{ e.VisibleIndex}); }}";

                        if (txtShenime.Text != "") { txtShenime.Rows = 3; }
                        else { txtShenime.Rows = 1; }
                        colShenime.DataItemTemplate = new MyMemoTemplate(rows);
                    }
                }
            }
        }


        private void MbushComboPrindi(ASPxComboBox cmbPrindi, int rowKey, int value)
        {

            var fushatShtese = DbCore.mySessionObjects.MerrNgaSession<colFushatShtese>(Session, $"{guidString}_TrupiGridaFushaShtese");
            if (fushatShtese != null && fushatShtese.FirstOrDefault(x => x.AtiTipiFushaShtese == rowKey) == null)
            {
                var prinderitEVelfshem = fushatShtese.Where(x => x.AtiTipiFushaShtese == 0 && x.TipiFushaShtese == 6).ToList();
                prinderitEVelfshem.Add(new DbCore.DbAdmin.clsFushaShtese());
                cmbPrindi.DataSource = prinderitEVelfshem;
                cmbPrindi.TextField = "PershkrimiFushaShtese";
                cmbPrindi.ValueField = "IdFushaShtese";
                cmbPrindi.ValueType = typeof(Int32);
                cmbPrindi.Value = value;
                cmbPrindi.DataBind();


            }
        }

        protected void grid_fushatShtese_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {//shtojme rreshtin e ri kur fokusohet rreshti i fundit

            //grid_fushatShtese.DataBind();

        }

        protected void grid_fushatShtese_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {

            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("grid_fushatShtese"))
                {
                    if (e.Parameters.Split(';').Length == 2)
                    {
                        if (e.Parameters.Split(';')[1] == "modifiko")
                            MbushGrideMeFushaTePlotesuara(int.Parse(e.Parameters.Split(';')[0]));
                        else
                            MbushGrideMeFushaBosh();
                    }
                    else
                    {
                        var trupi = MerrFushaShteseNgaHiddenField(true);
                        trupi.Add(new DbCore.DbAdmin.clsFushaShtese
                        {
                            IdFushaShtese = (-trupi.Count - 1),
                            Shfaq = true,
                            Lejueshme = true
                        });
                        this.grid_fushatShtese.DataSource = trupi;
                        this.grid_fushatShtese.DataBind();
                        mySessionObjects.RuajNeSession(Session, trupi, $"{guidString}_TrupiGridaFushaShtese");
                        percaktoTemplate();

                    }
                }
            }
        }

        protected void grid_fushatShtese_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpNoRows"] = grid_fushatShtese.VisibleRowCount;

            e.Properties["cpNoPage"] = grid_fushatShtese.PageIndex;
        }

        #endregion


    }
}
