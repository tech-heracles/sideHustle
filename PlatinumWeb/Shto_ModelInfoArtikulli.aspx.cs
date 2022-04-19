using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DbCore.DbAdmin;
using System.Data;
using DbCore.IMBUtils.Types;
using DevExpress.Web;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using PlatinumWeb.Templates;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    public partial class Shto_ModelInfoArtikulli : MyPageBase
    {

        private const string prefixMesazhNjejes = "Info e artikullit me kod: ";
        private const string prefixMesazhShumes = "Infot e artikujve me kod: ";
        private const string suffixMesazhNjejesGabimi = " nuk u fshi";
        private const string suffixMesazhShumesGabimi = " nuk u fshine";
        private const string suffixMesazhNjejesLidhur = " eshte i lidhur dhe nuk mund te fshihet";
        private const string suffixMesazhShumesLidhur = " jante te lidhura dhe nuk mund te fshihet";
        private const string suffixMesazhNjejesSuksesi = " u fshi me sukses!";
        private const string suffixMesazhShumesSuksesi = " u fshine me sukses!";
        private const string lidhesMesazhi = ". Kurse ";
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);

            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                DbCore.clsFunksione.logout(Session, true, "FaqePaautorizuar");
            }
            int idPerd = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);

            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerd);
            }

            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            //mbushComboBoxFiltra();
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), "ASPxGridView_InfoArtikulli", 1, "Shto_ModelInfoArtikulli.aspx");
            if (Page.IsPostBack == false)
            {
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, ":");
                if (Request.QueryString["ruaj"] == "ok")
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["mesazhRuajtjeMeSukses"], pnlMesazhi);
                konfiguroVleraFillestare();
                mbushGridInfoArtikulliNgaDB();
                konfiguroGride(rm, ci);
                GridUtil.percaktoVisibleColumnsMeWidth(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), ASPxGridView_InfoArtikulli, "ASPxGridView_InfoArtikulli", "Shto_ModelInfoArtikulli.aspx");

                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "Shto_ModelInfoArtikulli.aspx");
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
            }
            DbCore.mySessionObjects.ruajMesazhNeSesion(Session, ":");
            mbushGridInfoArtikulliNgaDB();
            konfiguroGride(rm, ci);
            shtoButtonKonfiguro();
            GridUtil.konfiguroGrideListeEvogelPaTheme(ASPxGridView_InfoArtikulli, "IdInfoKoka");

        }

        protected void ASPxGridView_InfoArtikulli_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "Shto_ModelInfoArtikulli.aspx");
            if (!tedrejtaInfo.DShtim)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ju nuk keni te drejta per kete veprim!:Red");
                return;
            }
            clsInfoKoka info = new clsInfoKoka() { Kodi = e.NewValues["Kodi"].ToString(), Pershkrimi = e.NewValues["Pershkrimi"].ToString(), IdPeriudha = int.Parse(e.NewValues["Periudha"].ToString()), Lloji = int.Parse(e.NewValues["Lloji"].ToString()), IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session), IdNdermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), IdStatusDok = 1, IdFormatNumri = Convert.ToInt32(e.NewValues["IdFormatNumri"]) };
            colInfoTrupi trupat = new colInfoTrupi();
            trupat.AddRange(DbCore.mySessionObjects.merrInfoVisibleNgaSesioni(Session));
            trupat.AddRange(DbCore.mySessionObjects.merrInfoInVisibleNgaSesioni(Session));
            info.InfoTrupi = trupat;
            e.Cancel = true;
            ASPxGridView_InfoArtikulli.CancelEdit();
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();

            if (!dbAdmin.ekzistonInfoKokaSipasKodNdermarje(info.Kodi, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session)))
            {
                mesazh = info.ruajInfo();
                if (!mesazh.Status == true)
                {
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ndodhi nje gabim. Ruajtja nuk u krye!:Red");
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ndodhi nje gabim. Ruajtja nuk u krye!", pnlMesazhi);
                }
                else
                {
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ruajtja perfundoi me sukses!:Green");
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["mesazhRuajtjeMeSukses"], pnlMesazhi);

                }
                mbushGridInfoArtikulliNgaDB();
            }
            else
            {
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ekziston nje info me kete kod! Ju lutem zgjidhni nje kod tjeter:Red");
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ekziston nje info me kete kod! Ju lutem zgjidhni nje kod tjeter", pnlMesazhi);
                konfiguroGride(rm, ci);
                ASPxGridView_InfoArtikulli.AddNewRow();
            }
            dbAdmin.Dispose();
        }

        protected void ASPxGridView_InfoArtikulli_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {//validimi ne jane plotesuar gjithe fushat e detyruara
            foreach (GridViewColumn column in ASPxGridView_InfoArtikulli.Columns)
            {
                if (column.Visible == true)
                {
                    GridViewDataColumn dataColumn = column as GridViewDataColumn;
                    if (dataColumn == null) continue;

                    if (e.NewValues[dataColumn.FieldName] == null && dataColumn.FieldName != "Konfiguro")//validimi per kolonat e detyrueshme
                    {

                        e.Errors[dataColumn] = "Vlera nuk mund te jete null.";
                    }
                }
            }
            if (hfRuaj.Value == "Ruaj" && e.NewValues["IdInfoKoka"] == null && e.NewValues["Kodi"] != null)
            {
                clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();
                if (dbAdmin.ekzistonInfoKokaSipasKodNdermarje(e.NewValues["Kodi"].ToString(), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session)))
                {
                    e.RowError = "Ekziston nje info me kete kod! Ju lutem zgjidhni nje kod tjeter";
                }
                dbAdmin.Dispose();
            }
            if (e.NewValues["Lloji"].ToString() == "0")
                e.RowError = "Lloji nuk mund te jete bosh!";
            if (e.NewValues["Lloji"].ToString() == "1" && e.NewValues["Periudha"].ToString() == "-1")
                e.RowError = "Periudha nuk mund te jete bosh per llojin artikull!";
            if (e.NewValues["IdFormatNumri"] == null)
                e.RowError = "Ju lutem, plotesoni formatin e numrave!";
            if (e.Errors.Count > 0)
            {
                e.RowError = "Ju lutemi, plotesoni te gjitha fushat.";
            }
            if (string.IsNullOrEmpty(e.RowError) && e.Errors.Count > 0)
            {
                e.RowError = "Ju lutemi, korrigjoni te gjithe gabimet.";
            }
        }

        protected void ASPxGridView_InfoArtikulli_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {//kur fillon editimi te behet validimi
            if (hfRuaj.Value == "Ruaj")
                if (!ASPxGridView_InfoArtikulli.IsNewRowEditing)
                {
                    ASPxGridView_InfoArtikulli.DoRowValidation();
                }
        }

        protected void ASPxGridView_InfoArtikulli_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "Shto_ModelInfoArtikulli.aspx");
            if (!tedrejtaInfo.DMod)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ju nuk keni te drejta per kete veprim!:Red");
                return;
            }
            String id = e.Keys["IdInfoKoka"].ToString();
            e.Cancel = true;
            clsInfoKoka info = new clsInfoKoka() { IdInfoKoka = int.Parse(id), Kodi = e.NewValues["Kodi"].ToString(), Pershkrimi = e.NewValues["Pershkrimi"].ToString(), IdPeriudha = int.Parse(e.NewValues["Periudha"].ToString()), Lloji = int.Parse(e.NewValues["Lloji"].ToString()), IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session), IdNdermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), IdStatusDok = 1, IdFormatNumri = Convert.ToInt32(e.NewValues["IdFormatNumri"]) };
            colInfoTrupi trupat = new colInfoTrupi();
            trupat.AddRange(DbCore.mySessionObjects.merrInfoVisibleNgaSesioni(Session));
            trupat.AddRange(DbCore.mySessionObjects.merrInfoInVisibleNgaSesioni(Session));
            info.InfoTrupi = trupat;

            DbCore.clsMesazh m = info.modifikoInfo();
            if (m.Status)
            {
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, m.PershkrimMesazhi + ":Green");
            }
            else
            {
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, m.PershkrimMesazhi + ":Red");
            }
            ASPxGridView_InfoArtikulli.CancelEdit();
            mbushGridInfoArtikulliNgaDB();


        }
        /// <summary>
        /// Metoda per te thirrur veprimet e menuse kur shtypen butonat
        /// </summary>
        /// <param name="source">derguesi</param>
        /// <param name="e">parametrat</param>
        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {

        }


        /// <summary>
        /// Konfiguron vlerat fillestare te ambjentit
        /// </summary>
        private void konfiguroVleraFillestare()
        {

            DbCore.mySessionObjects.ruajInfoVisibleNeSession(Session, new colInfoTrupi());
            DbCore.mySessionObjects.ruajInfoInVisibleNeSession(Session, new colInfoTrupi());
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasKod("InfoArt", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            hfKonffillestar.Value = Convert.ToString(String.Format("{0};{1}", konf.IdKonfigAmbjente, konf.KodKonfigAmbjente));
        }

        /// <summary>
        /// Konfiguron griden sipas kodit te konfigurimit dhe id se komponentes
        /// </summary>
        /// <param name="kodKonfigurimi">Kodi konfigurmit</param>
        /// <param name="idKomponente">Id e komponentes</param>
        private void konfiguroGride(System.Resources.ResourceManager rm, System.Globalization.CultureInfo ci)
        {
            KonfigurimComboGride.shtoKolonePeridhaInfoArtikulli(ASPxGridView_InfoArtikulli, rm, ci);
            KonfigurimComboGride.shtoLlojiKFArtikull(ASPxGridView_InfoArtikulli, rm, ci);
            KonfigurimComboGride.shtoFormatNr(ASPxGridView_InfoArtikulli, rm, ci, "IdFormatNumri");
            shtoButtonKonfiguro();
            (ASPxGridView_InfoArtikulli.Columns["Lloji"] as GridViewDataComboBoxColumn).PropertiesComboBox.ClientSideEvents.SelectedIndexChanged = "function (s,e){SelectionChangedLloji()}";
            ASPxGridView_InfoArtikulli.Columns["#"].VisibleIndex = 0;
        }


        /// <summary>
        /// perdoret per te shfaqur llojin 
        /// </summary>


        private void shtoButtonKonfiguro()
        {
            GridViewDataTextColumn col0 = ASPxGridView_InfoArtikulli.Columns["Konfiguro"] as GridViewDataTextColumn;
            col0.EditItemTemplate = new MyButtonTemplate("Konfiguro");
            col0.DataItemTemplate = new MyButtonTemplate("Konfiguro");

        }
        /// <summary>
        /// Merr te dhenat e info artikulli nga DB per griden ASPxGridView_InfoArtikulli
        /// </summary>
        private void mbushGridInfoArtikulliNgaDB()
        {
            DataTable dt = clsInfoKoka.ktheInfoPerGride(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            DbCore.mySessionObjects.ruajGrideNeSession(Session, dt);
            ASPxGridView_InfoArtikulli.DataSource = dt;
            ASPxGridView_InfoArtikulli.DataBind();
            dt.Dispose();
        }
        protected void ASPxGridView_InfoArtikulli_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType == GridViewRowType.Data)
            {
                GridViewDataTextColumn col0 = ((ASPxGridView)sender).Columns["Konfiguro"] as GridViewDataTextColumn;
                ASPxButton btn0 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col0, "btn") as ASPxButton;

                if (btn0 != null)
                {
                    btn0.ClientInstanceName = "btnKonfiguro" + e.VisibleIndex;
                    btn0.ClientSideEvents.Click = String.Format("function(s,e){{ClickKonfiguro({0});}}", e.KeyValue);
                }
            }
            if (e.RowType == GridViewRowType.EditForm)
            {
                GridViewDataTextColumn col0 = ((ASPxGridView)sender).Columns["Konfiguro"] as GridViewDataTextColumn;
                ASPxButton btn1 = ((ASPxGridView)sender).FindEditRowCellTemplateControl(col0, "btn") as ASPxButton;
                if (btn1 != null)
                {
                    btn1.ClientSideEvents.Click = "function(s,e){HapLupe();}";
                    btn1.ClientSideEvents.Init = "function(s,e){InitKonf(" + e.VisibleIndex + ");}";
                }
            }


        }
        /// <summary>
        /// Thirret sa here qe behet Callback, dhe ben edhe njeher lidhjen me datasource-in
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ASPxGridView_InfoArtikulli_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            if (e.CallbackName == "APPLYFILTER" && e.Args[0] == "")
            {
                MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
                ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
                cmbFiltra.Text = "";
            }
        }
        protected void ASPxGridView_InfoArtikulli_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            //ASPxGridView grida = sender as ASPxGridView;
            //GridViewDataComboBoxColumn colFormatNr;
            //colFormatNr = grida.Columns["IdFormatNumri"] as GridViewDataComboBoxColumn;
            //colFormatNr.PropertiesComboBox.Items.FindByValue(Convert.ToInt32(2)).Selected = true;
            //e.NewValues["IdFormatNumri"] = colFormatNr.PropertiesComboBox.Items.FindByValue(Convert.ToInt32(2));
        }

        /// <summary>
        /// Percaktojme karakteristika te ndryshme  te grides si : fusha celes apo filtrat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ASPxGridView_InfoArtikulli_DataBound(object sender, EventArgs e)
        {
            if (ASPxGridView_InfoArtikulli.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
                //perzgjidh
                GridViewCommandColumn check = new GridViewCommandColumn("#") { ShowSelectCheckbox = true, Width = System.Web.UI.WebControls.Unit.Percentage(2) };
                ASPxGridView_InfoArtikulli.Columns.Add(check);
                ASPxGridView_InfoArtikulli.KeyFieldName = "IdInfoKoka";
                //Afishon rreshtin qe do sherbej per filtrim
                ASPxGridView_InfoArtikulli.Settings.ShowFilterRow = true;
                ASPxGridView_InfoArtikulli.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                ASPxGridView_InfoArtikulli.Settings.ShowFilterRowMenu = true;
                ASPxGridView_InfoArtikulli.SettingsBehavior.AllowSelectByRowClick = true;
                ASPxGridView_InfoArtikulli.SettingsBehavior.AllowFocusedRow = true;

            }
        }

        /// <summary>
        /// Ruan disa karakteristika te grides
        /// </summary>
        /// <param name="sender">dergues</param>
        /// <param name="e">argumentat</param>
        protected void ASPxGridView_InfoArtikulli_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = ASPxGridView_InfoArtikulli.PageIndex;
            e.Properties["cpPageRow"] = ASPxGridView_InfoArtikulli.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = ASPxGridView_InfoArtikulli.VisibleRowCount;
        }

        /// <summary>
        /// Ne rastin kur kolonat e  grides jane combo, behet null kriteri i filtrimit kur zgjidhet vlera bosh e combos
        /// </summary>
        /// <param name="sender">Derguesi</param>
        /// <param name="e">Lista e paramterave</param>
        protected void ASPxGridView_InfoArtikulli_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if (e.Column.FieldName == "Periudha" || e.Column.FieldName == "IdFormatNumri")
            {
                if (Converter.ConvertToInt(e.Value) == -1)
                {
                    e.Criteria = null;
                }
            }
            if (e.Column.FieldName == "Lloji")
            {
                if (Converter.ConvertToInt(e.Value) == 0)
                {
                    e.Criteria = null;
                }
            }
        }

        /// <summary>
        ///Sherben per te vendosur filtra tek header-i i grides
        /// </summary>
        /// <param name="sender">Derguesi</param>
        /// <param name="e">Lista e parametrave</param>
        protected void ASPxGridView_InfoArtikulli_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {

            if (e.Column.FieldName == "Kodi" || e.Column.FieldName == "Pershkrimi")
            {
                e.Values.Clear();
                e.AddValue("(Te gjithe)", string.Empty, "true");
                e.AddValue("Nga A-D ", string.Empty, String.Format("{0}>'A     ' and {0} <'DDDDDDD'", e.Column.FieldName));
                e.AddValue("Nga D-G ", string.Empty, String.Format("{0}>'D     ' and {0}<'GGGGGGG'", e.Column.FieldName));
                e.AddValue("Nga H-K ", string.Empty, String.Format("{0}>'H     ' and {0}<'KKKKKKK'", e.Column.FieldName));
                e.AddValue("Nga L-O ", string.Empty, String.Format("{0}>'L     ' and {0}  <'OOOOOOO'", e.Column.FieldName));
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



        protected void ASPxGridView_InfoArtikulli_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string idkomponente = "";
            string kodkonfigurimi = "";

            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                kodkonfigurimi = arr[1];
                idkomponente = arr[0];
                if (arr[2] == "")
                    ASPxGridView_InfoArtikulli.FilterExpression = "";
                else
                {

                    clsFiltraGrida filtra = new clsFiltraGrida();
                    //filtra.mbushFiltraGridaSipasFiltraKodi(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "ASPxGridView_InfoArtikulli", "Shto_ModelInfoArtikulli.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
                    //DbCore.DbAdmin.clsFiltraGrida filtra = dbAdmin.merrFiltraGridaSipasFiltraKodi(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    if (filtra.FiltraKodi != null)
                    {
                        ASPxGridView_InfoArtikulli.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, ASPxGridView_InfoArtikulli);

                        konfiguroVleraFillestare();
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
            ASPxGridView_InfoArtikulli.Selection.UnselectAll();
        }


        /// <summary>
        /// Perdoret per te fshire reshtat e zgjedhur ne gride pasi perdoruesi ka konfirmuar fshirjen
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void fshiInfoArtikulli(object sender, EventArgs e)
        {

            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            List<object> rreshtat = ASPxGridView_InfoArtikulli.GetSelectedFieldValues("IdInfoKoka");
            //clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "", pnlMesazhi);
            List<string> TeFshire = new List<string>(), TePaFshire = new List<string>(), telidhura = new List<string>();

            foreach (object id in rreshtat)
            {

                clsInfoKoka infoArtikulli = new clsInfoKoka(Convert.ToInt32(id));
                if (infoArtikulli.IdStatusDok == 2) continue;
                if (clsInfoKoka.kaVeprime(infoArtikulli.IdInfoKoka))
                {
                    telidhura.Add(infoArtikulli.Kodi);
                    continue;
                }
                mesazh = infoArtikulli.fshiInfo();
                if (mesazh.Status)
                {

                    TeFshire.Add(infoArtikulli.Kodi);

                    hfStatusi.Value = "true";
                }
                else
                {
                    TePaFshire.Add(infoArtikulli.Kodi);
                }

            }
            string mesazhInfoGabim = "", mesazhInfoSukses = "", mesazhLidhur = "";
            if (TePaFshire.Count == 1)
                mesazhInfoGabim = String.Format("{0}{1}{2}", prefixMesazhNjejes, String.Join(";", TePaFshire), suffixMesazhNjejesGabimi);
            else
                if (TePaFshire.Count > 1)
                mesazhInfoGabim = String.Format("{0}{1}{2}", prefixMesazhShumes, String.Join(";", TePaFshire), suffixMesazhShumesGabimi);
            if (telidhura.Count == 1)
                mesazhLidhur = String.Format("{0}{1}{2}", prefixMesazhNjejes, String.Join(";", telidhura), suffixMesazhNjejesLidhur);
            else
                if (telidhura.Count > 1)
                mesazhLidhur = String.Format("{0}{1}{2}", prefixMesazhShumes, String.Join(";", telidhura), suffixMesazhShumesLidhur);
            if (TeFshire.Count == 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", prefixMesazhNjejes, String.Join(";", TeFshire), suffixMesazhNjejesSuksesi);
            else
                if (TeFshire.Count > 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", prefixMesazhShumes, String.Join(";", TeFshire), suffixMesazhShumesSuksesi);
            if (mesazhInfoGabim != "" && mesazhInfoSukses != "")
                mesazhInfoGabim += lidhesMesazhi + mesazhInfoSukses;
            if (mesazhInfoGabim != "" && mesazhLidhur != "")
                mesazhInfoGabim += lidhesMesazhi + mesazhLidhur;
            if (mesazhInfoGabim == "" && mesazhInfoSukses != "" && mesazhLidhur != "")
                mesazhInfoGabim = mesazhInfoSukses + lidhesMesazhi + mesazhLidhur;

            if (mesazhInfoGabim != "")
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhInfoGabim, pnlMesazhi);
            else if (mesazhLidhur != "")
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhLidhur, pnlMesazhi);
            else
            {
                ASPxMenu1.Items.FindByText("Ruaj").ClientVisible = false;
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazhInfoSukses, pnlMesazhi);
            }
            mbushGridInfoArtikulliNgaDB();
            pnlGrida.Update();
        }

        ///// <summary>
        ///// Mbush combon e filtrave te grides ASPxGridView_InfoArtikulli
        ///// </summary>
        //private void mbushComboBoxFiltra()
        //{
        //    clsGridaKoka koka = new clsGridaKoka("ASPxGridView_InfoArtikulli", "Shto_ModelInfoArtikulli.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //    colFiltratGrida colFiltra = new colFiltratGrida(koka.IdGridaKoka, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //    colFiltra.Insert(0, new clsFiltraGrida());
        //    MenuFilter.colekstioni = colFiltra;
        //    MenuFilter.ValueField = "IdFiltra";
        //    MenuFilter.TextField = "FiltraShenime";
        //}
        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        /// <param name="idViti"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNdermarrje"></param>
        private void percaktoTemplateMenu(ASPxMenu aSPxMenu1, int idViti, int idPerdorues, int idNdermarrje)
        {
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, "Shto_ModelInfoArtikulli.aspx", this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, hfRuaj.Value == "Ruaj" ? true : false, true, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
        }


        /// <summary>
        /// Behet bound i menuse duke thirrur metoden percaktoTemplateMenu
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));

        }

        /// <summary>
        /// Sherben per te ruajtur filtrin e zgjedhur te grides
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            clsFiltraGrida filtri = new clsFiltraGrida() { FiltraKodi = cmbFiltra.Text, FiltraShenime = cmbFiltra.Text, FiltraUniversal = false };
            clsGridaKoka koka = new clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "ASPxGridView_InfoArtikulli", "Shto_ModelInfoArtikulli.aspx", idNdermarrje);
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = ASPxGridView_InfoArtikulli.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("Kodi", ASPxGridView_InfoArtikulli);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = ASPxGridView_InfoArtikulli.GetSortedColumns();
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
            filtri.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            filtri.IdNdermarje = idNdermarrje;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            filtri.IdStatusDok = 1;

            mesazh = filtri.ruaj();
            //mbushComboBoxFiltra();
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "ASPxGridView_InfoArtikulli", 1, "Shto_ModelInfoArtikulli.aspx");
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));

            if (mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                ASPxMenu1.Items.FindByText("Ruaj").ClientVisible = false;
            }
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            hfStatusi.Value = "true";
            cmbFiltra.Text = "";
        }

        /// <summary>
        /// Fshin filtrin e zgjedhur dhe aplikuar mbi gride, fshirje nga DB ku ai eshte ruajtur, dhe jo pastrimi i grides nga aplikimi i filtrit
        /// per kete do sherbeje Clear Filter ne fund te grides
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
            //Kap item qe ka template ne menune e kesaj faqeje
            MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            clsFiltraGrida filtra = new clsFiltraGrida();
            //filtra.mbushFiltraGridaSipasFiltraEmri(cmbFiltra.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "ASPxGridView_InfoArtikulli", "Shto_ModelInfoArtikulli.aspx", idNdermarrje);
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra();
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "ASPxGridView_InfoArtikulli", 1, "Shto_ModelInfoArtikulli.aspx");
                percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));

                if (mesazh.Status)
                {
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                    ASPxMenu1.Items.FindByText("Ruaj").ClientVisible = false;
                }
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                hfStatusi.Value = "true";
                ASPxGridView_InfoArtikulli.FilterExpression = String.Empty;
            }
        }
    }
}