using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using DevExpress.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Resources;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class Shto_Sigurimet : MyPageBase
    {
        private int idndermarje, idperdoruesi, idnderviti, idgjuha, idviti;
        private const int idstatusdok = 1;

        /// <summary>
        /// thirret kur faqja lodohet. Ne te behet kontrolli nese perdoruesi eshte i loguar ne sistem  dhe nqs jo ridrejtohet tek forma e logimit
        /// thirret inicializimi i konfigurimeve fillestare te faqes
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e"> argumenti</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                Response.Redirect(DbCore.IMBUtils.Paths.defaultLoginPath);
            }
            idperdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idperdoruesi);
            }
            idndermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            idviti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            idgjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            idnderviti = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
            if (!Page.IsPostBack)
            {
                CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
                ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                EmrateTabeve(rm, cultinf);
                hfState.Set("idGjuha", idgjuha);
                hfState.Set("idndermarje", idndermarje);
                ASPxPageControl1.ActiveTabIndex = 0; 
                percaktoTemplateMenu(idgjuha, idviti, idperdoruesi, idndermarje, ASPxMenu1);                
                konfiguroVleraFillestare(rm, cultinf, idgjuha);
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idndermarje, "gvSigurimet", int.Parse(cmbKonfigurimi.Value.ToString()), "Shto_Sigurimet.aspx");
                mbushGridSigurimeNgaDB();
                konfiguroGride(cmbKonfigurimi.Text.Split(';')[0], 705);
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idndermarje, "gvSigurimet", gvSigurimet, cmbKonfigurimi.Text.Split(';')[0], 705.ToString(), (int)hfState["idGjuha"]);
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idperdoruesi, idndermarje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idperdoruesi, idndermarje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "Shto_Sigurimet.aspx");
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
            }
            else
            {
                mbushGridSigurimeNgaSession();
                konfiguroGride(cmbKonfigurimi.Text.Split(';')[0], 705);
            }
            GridUtil.konfigGrideListeEMadhePaTheme(gvSigurimet, "IdSigurime");
            percaktoTemplateMenu(idgjuha, idviti, idperdoruesi, idndermarje, ASPxMenu1);
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idndermarje, "gvSigurimet", int.Parse(cmbKonfigurimi.Value.ToString()), "Shto_Sigurimet.aspx");
            gvSigurimet.PercaktoTitlePanel(this, MenuInfo, pnlMesazhi, hfState, idperdoruesi, idndermarje, idviti, idgjuha, int.Parse(cmbKonfigurimi.Value.ToString()), "Shto_Sigurimet.aspx", new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources")), DbCore.mySessionObjects.ktheCultureInfo(Session));
        }

        /// <summary>
        /// Vendos emrat e tabeve ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateTabeve(ResourceManager rm, CultureInfo cultinf)
        {
            ASPxPageControl1.TabPages[0].Text = rm.GetString("labelAdministrimiTePergjithshme", cultinf);
            ASPxPageControl1.TabPages[1].Text = rm.GetString("MenuItemSigurimet", cultinf);
            konfigurimi_Label.Text = rm.GetString("lblModeli", cultinf);
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
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, "Shto_Sigurimet.aspx", this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, hfShtimModifikim.Value == "modifikim" ? false : true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
        }

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(idgjuha, idviti, idperdoruesi, idndermarje, ASPxMenu1);

        }

        /// <summary>
        /// perdoret per te shtuar kolonen e selektimit tek grida dhe per te vendosur disa karakteristika te grides
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void gvSigurimet_DataBound(object sender, EventArgs e)
        {// shton colonen # per selektim dhe disa karakteristika te grides
            if (gvSigurimet.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
                //perzgjidh
                GridViewCommandColumn check = new GridViewCommandColumn("#") { ShowSelectCheckbox = true, Width = Unit.Percentage(2) };
                //behet per te afishuar rreshtin qe do sherbej per filtrim
                gvSigurimet.Settings.ShowFilterRow = true;
                gvSigurimet.Settings.ShowFilterRowMenu = true;
                gvSigurimet.Columns.Add(check);
                gvSigurimet.KeyFieldName = "IdSigurime";
                gvSigurimet.SettingsBehavior.AllowSelectByRowClick = true;
                gvSigurimet.SettingsBehavior.AllowFocusedRow = true;
            }
        }

        /// <summary>
        /// perdoret per te konfiguruar griden. Per te dhenat mbi kolonat qe do te shfaqen nga databaza si dhe percakton disa karakteristika te grides
        /// </summary>
        /// :  <see cref="GridUtil.percaktoVisibleColumns"/>
        /// :  <see cref="DbCore.clsFunksione.konfiguroGrideListeMadhe(grida,celesigrides)"/>
        private void konfiguroGride(string kodKonfigurimi, int idKomponente)
        {           
            gvSigurimet.Columns["#"].VisibleIndex = 0;
        }

        /// <summary>
        /// perdoret ne rastet kur grida ben callback per ta rimbushur griden
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void gvSigurimet_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            if (e.CallbackName == "COLUMNMOVE" && gvSigurimet.AllColumns[int.Parse(e.Args[0])].Width.Value == 0)
                gvSigurimet.AllColumns[int.Parse(e.Args[0])].Width = Unit.Percentage(3);
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
        protected void gvSigurimet_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            if (e.Column.FieldName == "Kodi")
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            //filtra.mbushFiltraGridaSipasFiltraEmri(cmbFiltra.Text, idndermarje);
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvSigurimet", "Shto_Sigurimet.aspx", idndermarje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra();
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idndermarje, "gvSigurimet", int.Parse(cmbKonfigurimi.Value.ToString()), "Shto_Sigurimet.aspx");
                percaktoTemplateMenu(idgjuha, idviti, idperdoruesi, idndermarje, ASPxMenu1);

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
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;

            DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida() { FiltraKodi = cmbFiltra.Text, FiltraShenime = cmbFiltra.Text, FiltraUniversal = false };
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvSigurimet", "Shto_Sigurimet.aspx", idndermarje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvSigurimet.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("Kodi", gvSigurimet);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvSigurimet.GetSortedColumns();
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
            filtri.IdNdermarje = idndermarje;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            filtri.IdStatusDok = 1;

            mesazh = filtri.ruaj();
            //mbushComboBoxFiltra();
            clsToolbarConfig.mbushComboBoxFiltra(idgjuha, idndermarje, "gvSigurimet", int.Parse(cmbKonfigurimi.Value.ToString()), "Shto_Sigurimet.aspx");
            percaktoTemplateMenu(idgjuha, idviti, idperdoruesi, idndermarje, ASPxMenu1);

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
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            List<object> rreshtat;
            if (ASPxPageControl1.ActiveTabIndex == 0)//nqs kemi zgjedhur vetem nje kartele per te pare fshijme vetem ate
                rreshtat = gvSigurimet.GetSelectedFieldValues("IdSigurime", "Kodi");
            else
            {
                rreshtat = new List<object>();
                string[] rreshti = { hfId.Value, txtKodi.Text };
                rreshtat.Add(rreshti);
            }
            //List<object> rreshtat = gvSigurimet.GetSelectedFieldValues("IdSigurime", "Kodi");
            if (rreshtat.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("mesazhZgjidhniNje1", ci), pnlMesazhi);
                return;
            }
            //clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "", pnlMesazhi);
            List<string> TeFshire = new List<string>(), TePaFshire = new List<string>(), TeLidhur = new List<string>();
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            DbCore.DbRegjistrim.clsDatabaseRegjistrim dbregjistrim = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
            foreach (object id in rreshtat)
            {
                DbCore.DbListPagesat.clsSigurimet sig = new DbCore.DbListPagesat.clsSigurimet(Convert.ToInt32(((object[])id)[0]));
                DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                konf.mbushKonfigAmbjSipasId(sig.IdKonfig);
                bool lidhur = dbregjistrim.eshteDokumentiILidhurCelje(sig.IdSigurime.ToString(), konf.IdNivel.ToString());
                if (sig.Model == 1)
                {
                    TePaFshire.Add(sig.Kodi);
                    continue;
                }
                if (lidhur)
                {
                    TeLidhur.Add(sig.Kodi);
                    continue;
                }
                sig.IdPerdoruesi = idperdoruesi;
                mesazh = sig.fshi();
                if (sig.IdSigurime == 0) continue;
                if (mesazh.Status)
                {
                    #region Heq llogarite nga grida

                    // ASPxGridView_Artikull.DataSource = dt;
                    hiqNgaGrida(sig.IdSigurime);
                    #endregion
                    TeFshire.Add(sig.Kodi);

                    ASPxPageControl1.ActiveTabIndex = 0;
                    hfStatusi.Value = "true";
                }
            }
            dbregjistrim.Dispose();
            string mesazhInfoGabim = "", mesazhInfoSukses = "", mesazhLidhur = "";
            if (TePaFshire.Count == 1)
                mesazhInfoGabim = String.Format("{0}{1}{2}",  rm.GetString("prefixMesazhNjejesSigurime", ci), String.Join(";", TePaFshire), rm.GetString("suffixMesazhNjejesGabimi", ci));
            else
                if (TePaFshire.Count > 1)
                    mesazhInfoGabim = String.Format("{0}{1}{2}", rm.GetString("prefixMesazhShumesSigurime", ci), String.Join(";", TePaFshire), rm.GetString("suffixMesazhShumesGabimi", ci));
            if (TeLidhur.Count == 1)
                mesazhLidhur = String.Format("{0}{1}{2}", rm.GetString("prefixMesazhNjejesSigurime", ci) , String.Join(";", TeLidhur), rm.GetString("suffixMesazhNjejesGabimiLidhur",ci));
            else
                if (TeLidhur.Count > 1)
                    mesazhLidhur = String.Format("{0}{1}{2}", rm.GetString("prefixMesazhShumesSigurime", ci), String.Join(";", TeLidhur), rm.GetString("suffixMesazhShumesGabimiLidhur",ci));
            if (TeFshire.Count == 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("prefixMesazhNjejesSigurime", ci), String.Join(";", TeFshire), rm.GetString("suffixMesazhNjejesSuksesi", ci));
            else
                if (TeFshire.Count > 1)
                    mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("prefixMesazhShumesSigurime", ci), String.Join(";", TeFshire), rm.GetString("suffixMesazhShumesSuksesi", ci));
            mesazhInfoGabim += mesazhLidhur;
            if (mesazhInfoGabim != "" && mesazhInfoSukses != "")
                mesazhInfoGabim += rm.GetString("msgGrupimeLidhesMesazhi", ci) + mesazhInfoSukses;
            if (mesazhInfoGabim != "")
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhInfoGabim, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazhInfoSukses, pnlMesazhi);
        }

        /// <summary>
        /// heq nga grida rreshtat e fshire
        /// </summary>
        /// <param name="id"> id e reshtit</param>
        private void hiqNgaGrida(int id)
        {
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            if (gvSigurimet.DataSource != null)
            {
                DataTable dt = (DataTable)gvSigurimet.DataSource;
                DataRow[] drs = dt.Select(String.Format("IdSigurime = {0}", id));
                if (drs.Length > 1)
                    throw new Exception(rm.GetString("gabim2signegrideSiguracion", ci));
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                dt.Rows.Remove(dr);
                gvSigurimet.DataBind();
            }
            else mbushGridSigurimeNgaDB();
        }

        /// <summary>
        /// shton nte gride rreshtat e rinj
        /// </summary>
        /// <param name="id">id e sig</param>
        /// <param name="idndermarje"> id e ndermarjes</param>
        private void shtoNeGrid(int idndermarje, int id)
        {
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            if (gvSigurimet.DataSource != null)
            {
                DataTable dt = (DataTable)gvSigurimet.DataSource;
                DataRow[] drs = dt.Select(String.Format("IdSigurime = {0}", id));
                if (drs.Length > 0)
                    throw new Exception((rm.GetString("gabim2signegrideSiguracion", ci)));
                DataRow newArtDr = DbCore.DbListPagesat.colSigurimet.merrSigurimeSipasNdermarjesDR(idndermarje, id);
                dt.ImportRow(newArtDr);
            }
            else mbushGridSigurimeNgaDB();
        }

        /// <summary>
        /// modifikon ne gride komponenten e modifikuar
        /// </summary>
        /// <param name="id">id e sig</param>
        /// <param name="idndermarje"> id e ndermarjes</param>
        private void modifikoNeGrid(int idndermarje, int id)
        {
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            if (gvSigurimet.DataSource != null)
            {
                DataTable dt = (DataTable)gvSigurimet.DataSource;
                DataRow[] drs = dt.Select(String.Format("IdSigurime = {0}", id));
                if (drs.Length > 1)
                {
                    throw new Exception((rm.GetString("gabim2signegrideSiguracion", ci)));
                }
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                DataRow newArtDr = DbCore.DbListPagesat.colSigurimet.merrSigurimeSipasNdermarjesDR(idndermarje, id);
                object[] arr = newArtDr.ItemArray;
                dr.ItemArray = arr;
            }
            else mbushGridSigurimeNgaDB();
        }

        /// <summary>
        /// perdoret per te trajtuar ngjarjet e butonave te menuse. Shkon ne faqen e modifikimit te bankave kur perdoruesi klikon butonin modifiko
        /// ose ne faqen e shtimit te bankave kur perdoruesi klikon butonin shto
        /// </summary>
        /// <param name="source"> derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {//kryen veprimet e menuse
            if (e.Item.Name == "Ruaj")
            {
                Page.Validate("entries");
                ruajSigurim();
            }
        }

        /// <summary>
        /// konfiguron vlerat fillestare
        /// </summary>
        private void konfiguroVleraFillestare(ResourceManager rm, CultureInfo cultinf, int idGjuha)
        { //mbush komboboxet dhe gridat e faqes
            ASPxPageControl1.ActiveTabIndex = 0;
            dteDateAkt.Date = DbCore.mySessionObjects.merrPeriudheKontabel(Session).FillimiPeriudha;
            ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idperdoruesi, idndermarje, cmbKonfigurimi, 35, rm, cultinf, idGjuha);
            cmbKonfigurimi.SelectedIndex = 0;
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()), idgjuha);
            hfKonffillestar.Value = String.Format("{0};{1}", konf.KodKonfigAmbjente, konf.PershkrimKonfigAmbjente);
        }

        /// <summary>
        /// mbush griden nga sesioni
        /// </summary>
        private void mbushGridSigurimeNgaSession()
        {
            DataTable tmpObject;
            bool sukses = DbCore.mySessionObjects.merrGrideNgaSessioni(Session, out tmpObject);
            if (!sukses)
                mbushGridSigurimeNgaDB();
            else
            {
                gvSigurimet.DataSource = tmpObject;
                gvSigurimet.DataBind();
                tmpObject.Dispose();
            }
        }

        /// <summary>
        /// mbush griden nga databaza
        /// </summary>
        private void mbushGridSigurimeNgaDB()
        {//mbush griden e popupit me te dhena            
            DataTable dt = DbCore.DbListPagesat.colSigurimet.merrSigurimeNdermarjeDT(idndermarje);
            DbCore.mySessionObjects.ruajGrideNeSession(Session, dt);
            gvSigurimet.DataSource = dt;
            gvSigurimet.DataBind();
            dt.Dispose();
        }

        /// <summary>
        /// ruan sigurimin 
        /// </summary>
        private void ruajSigurim()
        {
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            if (Page.IsValid == false)
                return;
            else
            {
                if (isValidSigurim())
                {
                    bool eshteShtim;
                    DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                    DbCore.DbListPagesat.clsSigurimet sig = krijoSigurim();

                    DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                    tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idperdoruesi, idndermarje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "Shto_Sigurimet.aspx");
                    if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
                    {
                        if (!tedrejtaInfo.DShtim)
                        {
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukKeniTeDrejta", ci), pnlMesazhi);
                            hfStatusi.Value = "false";
                            return;
                        }
                        mesazh = sig.ruaj();
                        eshteShtim = true;
                    }
                    else
                    {
                        if (!tedrejtaInfo.DMod)
                        {
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukKeniTeDrejta", ci), pnlMesazhi);
                            hfStatusi.Value = "false";
                            return;
                        }
                        DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegjistrim = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
                        DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                        konf.mbushKonfigAmbjSipasId(sig.IdKonfig);
                        sig.IdSigurime = int.Parse(hfId.Value);
                        DbCore.DbListPagesat.clsSigurimet sigivjeter = new DbCore.DbListPagesat.clsSigurimet(sig.IdSigurime);
                        sig.Model = sigivjeter.Model;
                        bool lidhur = dbRegjistrim.eshteDokumentiILidhurCelje(sig.IdSigurime.ToString(), konf.IdNivel.ToString());
                        if (lidhur.ToString() != hfLidhur.Value)
                        {
                            mesazh.Status = false;
                            mesazh.PershkrimMesazhi = rm.GetString("ilidhur", ci);
                        }
                        else
                            mesazh = sig.modifiko();
                        eshteShtim = false;
                        dbRegjistrim.Dispose();
                    }
                    if (mesazh.Status == true)
                    {
                        clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                        if (eshteShtim)
                            shtoNeGrid(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), sig.IdSigurime);
                        else //modifikim
                            modifikoNeGrid(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), sig.IdSigurime);
                        hfStatusi.Value = "true";
                        ASPxPageControl1.ActiveTabIndex = 0;
                    }
                    else
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                        hfStatusi.Value = "false";
                    }
                }
            }
            pnlMesazhi.Update();
        }

        /// <summary>
        /// krijon sigurimin sipas te dhenave
        /// </summary>
        /// <returns> sigurimin me te dhenat</returns>
        private DbCore.DbListPagesat.clsSigurimet krijoSigurim()
        {//krijon nje banke sipas te dhenave te futura nga perdoruesi

            DbCore.DbShare.clsKonfigurimAmbjenti konfig = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konfig.mbushKonfigAmbjSipasKod(cmbKonfigurimi.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            DbCore.DbListPagesat.clsSigurimet sig = new DbCore.DbListPagesat.clsSigurimet(0, txtKodi.Text, dteDateAkt.Date, Convert.ToDecimal(txtPagaMin.Text), Convert.ToDecimal(txtPagaMax.Text), Convert.ToDecimal(txtSigShoqPun.Text), Convert.ToDecimal(txtSigShenPun.Text), Convert.ToDecimal(txtSigShoqNder.Text), Convert.ToDecimal(txtSigShenNder.Text), Convert.ToDecimal(txtTotal.Text), Convert.ToInt32(DbCore.DbListPagesat.Model.NgaPerdoruesi), idperdoruesi, idndermarje, konfig.IdKonfigAmbjente, idstatusdok, Convert.ToDecimal(txtPagaMinShen.Text), Convert.ToDecimal(txtPagaMaxShen.Text), Convert.ToDecimal(txtSigSupPun.Text), Convert.ToDecimal(txtSigSupNder.Text));

            return sig;
        }

        /// <summary>
        /// kontrollon nese te dhenat qe jane plotesuara jane te lejueshme apo jo
        /// </summary>
        /// <returns> true ose false</returns>
        private bool isValidSigurim()
        {
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            if (dteDateAkt.Text == "")
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("mesazhaktivizimi", ci), pnlMesazhi);
                hfStatusi.Value = "false";
                return false;
            }
            if (Convert.ToDecimal(txtPagaMin.Text) > Convert.ToDecimal(txtPagaMax.Text))
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("gabimmin", ci), pnlMesazhi);
                hfStatusi.Value = "false";
                return false;
            }
            if (Convert.ToDecimal(txtPagaMin.Text) == 0 && Convert.ToDecimal(txtPagaMax.Text) == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgPagaShoqerore", ci), pnlMesazhi);
                hfStatusi.Value = "false";
                return false;
            }
            if (Convert.ToDecimal(txtPagaMinShen.Text) == 0 && Convert.ToDecimal(txtPagaMaxShen.Text) == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgPagaShendetesore",ci), pnlMesazhi);
                hfStatusi.Value = "false";
                return false;
            }
            if (Convert.ToDecimal(txtPagaMinShen.Text)>Convert.ToDecimal(txtPagaMaxShen.Text))
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("gabimmin", ci), pnlMesazhi);
                hfStatusi.Value = "false";
                return false;
            }
            return true;
        }

        /// <summary>
        /// cakton properti ne javascript
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvSigurimet_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvSigurimet.PageIndex;
            e.Properties["cpPageRow"] = gvSigurimet.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvSigurimet.VisibleRowCount;
        }

        /// <summary>
        /// kur grida ben callback
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvSigurimet_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string idkomponente = "";
            string kodkonfigurimi = "";

            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                kodkonfigurimi = arr[1];
                idkomponente = arr[0];
                if (arr[2] == "")
                    gvSigurimet.FilterExpression = "";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvSigurimet", "Shto_Sigurimet.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), int.Parse(cmbKonfigurimi.Value.ToString()));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvSigurimet.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvSigurimet);
                        hfStatusi.Value = "true";
                    }
                    else hfStatusi.Value = "false";
                }
            }
            else
                if (arr.Length == 2)//nese eshte zgjedhur nje konfigurim tek combo e konfigurimeve
                {
                    kodkonfigurimi = arr[1];
                    idkomponente = arr[0];
                }
                else
                {
                    idkomponente = e.Parameters;
                }

            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            if (cmbFiltra.Text == "")
            {
                gvSigurimet.FilterExpression = "";
            }
            gvSigurimet.Selection.UnselectAll();
        }

        /// <summary>
        /// per filtrimin me elementin bosh
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvSigurimet_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
        }
    }
}