using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Web.UI.HtmlControls;
using DbAdmin;
using DbKontabiliteti;
using System.Collections;
namespace PlatinumWeb
{
    public partial class SkematKontabel : System.Web.UI.Page
    {
        private string koloneFocus;
        //ASPxButtonEdit temptxt = null;
        ASPxComboBox tempcombo = null;
        DbKontabiliteti.clsSkemaKontabelKoka koka;
        DbKontabiliteti.colSkemaKontabelTrupi oColSkemaKontabelTrupi = new DbKontabiliteti.colSkemaKontabelTrupi();
        DbKontabiliteti.colSkemaKontabelKoka colSkema;
        DbKontabiliteti.colLlogarite colLlog;
        DbAdmin.clsPerdorues oPerdorues = new DbAdmin.clsPerdorues();
        protected void Page_PreInit(object sender, EventArgs e)
        {
            clsFunksione.percaktoThemeAmbjenteDheJQuery(Page, clsFunksione.ktheIdPerdoruesi(Session), null);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            if (Session["LoggedIn"].Equals("No"))
            {
                Response.Redirect("login.aspx?arsye=FaqePaautorizuar");
            }

            if (Session["KodiNdermarrjes"] == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + Session["Idperdoruesi"]);
            }

            oPerdorues = (DbAdmin.clsPerdorues)(Session["oClsPerdoruesi"]);
            if (!IsPostBack)
            {
                konfiguroVleraFillestare();
                konfiguroGride();
                konfiguroGridenERe();
                new clsFunksione().konfiguroMenuPaTheme(ASPxMenu1);
                new clsFunksione().percaktoTedrejtatPerKeteFaqe("SkematKontabel.aspx", ASPxMenu1);
                if (ASPxMenu1.Items.FindByName("Shto").Enabled == false)
                {
                    ruaj_Button.Enabled = false;
                    anullo_Button.Enabled = false;
                    pastro_Button.Enabled = false;
                }
            }
            //DbKontabiliteti.clsDatabaseKontabilitet dbKontabiliteti = new DbKontabiliteti.clsDatabaseKontabilitet();
            clsFunksione funk = new clsFunksione();
        
            oPerdorues = (DbAdmin.clsPerdorues)(Session["oClsPerdoruesi"]);
            colSkema.mbushGjitheSkematKontabelAndAutorizim(funk.ktheIdNdermarrje(), oPerdorues.IdPerdorues);
            //colSkema = dbKontabiliteti.merrGjitheSkematKontabelAndAutorizim(funk.ktheIdNdermarrje(),oPerdorues.IdPerdorues );
            //colLlog = dbKontabiliteti.merrLLogariteNdermarrjesAndAutorizime(funk.ktheIdNdermarrje(), oPerdorues.IdPerdorues);
            colLlog.mbushLLogariteNdermarrjesAndAutorizime(funk.ktheIdNdermarrje(), oPerdorues.IdPerdorues);
          
            container.Attributes["width"] = "350px";
            container.Attributes["height"] = "400px";
            container1.Attributes["width"] = "350px";
            container1.Attributes["height"] = "400px";
                percaktoTemplate();
        }
        
        private void databind_grid_SkematKontabel()
        {
            //DbKontabiliteti.clsDatabaseKontabilitet dbKontabiliteti = new DbKontabiliteti.clsDatabaseKontabilitet();
            clsFunksione funk = new clsFunksione();
            DbAdmin.clsPerdorues oPerdorues = new DbAdmin.clsPerdorues();
            oPerdorues = (DbAdmin.clsPerdorues)(Session["oClsPerdoruesi"]);
            DbKontabiliteti.colSkemaKontabelKoka colKoka = new colSkemaKontabelKoka();
            colKoka.mbushGjitheSkematKontabelAndAutorizim(funk.ktheIdNdermarrje(), oPerdorues.IdPerdorues);
            //DbKontabiliteti.colSkemaKontabelKoka colKoka = dbKontabiliteti.merrGjitheSkematKontabelAndAutorizim(funk.ktheIdNdermarrje(),oPerdorues.IdPerdorues);
            grid_SkematKontabel.DataSource = colKoka;
            grid_SkematKontabel.DataBind();
        }

        private void databind_grid_SkemaReKontabel()
        {
            clsFunksione funk = new clsFunksione();

            DbKontabiliteti.clsDatabaseKontabilitet dbKontabiliteti = new DbKontabiliteti.clsDatabaseKontabilitet();
            DbKontabiliteti.colSkemaKontabelTrupi col = new DbKontabiliteti.colSkemaKontabelTrupi();
            grid_SkemaReKontabel.DataSource = col;
            grid_SkemaReKontabel.DataBind();

            ////String colTrupi = funk.KtheNeString(col);
            ////Session["TrupiIKokesSeSelektuar"] = colTrupi;
            ////int indeksi = grid_SkematKontabel.FocusedRowIndex;
            ////int id;

            ////if (indeksi > 0)
            ////{
            ////    if (grid_SkematKontabel.GetRowValues(indeksi, "IdSkemaKontabelKoka") != null)
            ////    {
            ////        //vlera e id se kokes se skemes konatabel qe eshte ne fokus
            ////        id = int.Parse(grid_SkematKontabel.GetRowValues(indeksi, "IdSkemaKontabelKoka").ToString());
            ////        col = dbKontabiliteti.ktheTrupinESkemes(id);
            ////    }
            ////    else
            ////        id = -1;
            ////}
            //////Session["SkemaTrupiEdituar"] merr vlere kur po editohet trupi i ndonje skeme kontabel
            //////Nese ky parameter i seksionit eshte != null dmth qe po behet editimi i trupit te nje skeme ekzistuese
            ////if (Session["SkemaTrupiEdituar"] != null)
            ////    col = funk.KtheNeObjekt(Session["SkemaTrupiEdituar"].ToString());

            //////Session["SkemaTrupiRi"] merr vlere kur shtohen rreshta per nje skeme te re kontabel
            //////nese ky parameter i seksionit eshte != null dmth qe po shtohen rreshta per nje skeme te re
            ////if (Session["SkemaTrupiRi"] != null)
            ////{
            ////    //sa here shtohet nje rresht i ri ruhet ne seksion dhe hapet nje rresht tjeter
            ////    col = funk.KtheNeObjekt(Session["SkemaTrupiRi"].ToString());               
            ////    grid_SkemaReKontabel.AddNewRow();
            ////}
            //////Nese nuk po shtojme apo editojme (pra s'po ndosh ndonje nga rastet e mesiperme
            //////atehete grid_SkemaReKontabel mbushet me vlerat e trupit te skemes se selektuar
            //////Keto vlera ruhen ne Session["TrupiIKokesSeSelektuar"]
            ////grid_SkemaReKontabel.DataSource = col;
            ////grid_SkemaReKontabel.DataBind();
        }

        private void konfiguroVleraFillestare()
        {
            DbKontabiliteti.clsDatabaseKontabilitet dbKontabiliteti = new DbKontabiliteti.clsDatabaseKontabilitet();
            databind_grid_SkematKontabel();
            databind_grid_SkemaReKontabel();
        }

        private void konfiguroGride()
        {    
            shto_Autorizim();
            shto_NrAutomatik();
            clsFunksione funk = new clsFunksione();
            clsFunksione.percaktoVisibleColumns(clsFunksione.merrIdNdermarrjeSesioni(Session), grid_SkematKontabel, "grid_SkematKontabel", "SkematKontabel.aspx");
            //funk.konfiguroGrideListeEvogel(grid_SkematKontabel, "IdSkemaKontabelKoka");
            clsFunksione.konfiguroGrideListeEvogelPaTheme(grid_SkematKontabel, "IdSkemaKontabelKoka");
            grid_SkematKontabel.SettingsPager.PageSize = 10;           
        }

        private void shto_Autorizim()
        {//shtohen komboja me Autorizimeve tek grida e skemes kontabel

            DbAdmin.clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();
            grid_SkematKontabel.Columns.Remove(grid_SkematKontabel.Columns["IdAutorizimSkemaKontabelKoka"]);

            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            DbAdmin.colAutorizimetKoka colAutorizim = new colAutorizimetKoka();
            colAutorizim.mbushGjitheAutorizimet(clsFunksione.merrIdNdermarrjeSesioni(Session));
            //colAutorizim = dbAdmin.merrGjitheAutorizimet();
            colnew.PropertiesComboBox.DataSource = colAutorizim;
            colnew.PropertiesComboBox.TextField = "KodiAutorizim";
            colnew.PropertiesComboBox.ValueField = "KodiAutorizim";
            colnew.FieldName = "IdAutorizimSkemaKontabelKoka";
            grid_SkematKontabel.Columns.Add(colnew);
        }

        private void shto_NrAutomatik()
        {//shtohen komboja me KPF e kredive tek grida e llogarive
            clsFunksione funksion = new clsFunksione();
            DbAdmin.clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();

            grid_SkematKontabel.Columns.Remove(grid_SkematKontabel.Columns["NrAutoSkemaKontabelKoka"]);
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            DbAdmin.colNrAutom colNrAutomatik = new DbAdmin.colNrAutom();
            colNrAutomatik.mbushGjitheNumratAutomatike(funksion.ktheIdNdermarrje());
            //DbAdmin.colNrAutom colNrAutomatik = dbAdmin.merrGjitheNumratAutomatike(funksion.ktheIdNdermarrje());
            colnew.PropertiesComboBox.DataSource = colNrAutomatik;
            colnew.PropertiesComboBox.TextField = "KodiNrAutom";
            colnew.PropertiesComboBox.ValueField = "IdNrAutom";
            colnew.FieldName = "NrAutoSkemaKontabelKoka";
            grid_SkematKontabel.Columns.Add(colnew);
            percaktoTamplateSkemaKontabel();
        }

        private void konfiguroGridenERe()
        {   shtoKolone();
            
            clsFunksione.percaktoVisibleColumns(clsFunksione.merrIdNdermarrjeSesioni(Session), grid_SkemaReKontabel, "grid_SkemaReKontabel", "SkematKontabel.aspx");
            clsFunksione.konfiguroGrideRegjistrimEvogelPaTheme(grid_SkemaReKontabel, "IdKoka");
          
            //grid_SkemaReKontabel.SettingsEditing.Mode = GridViewEditingMode.Inline;
            
            grid_SkemaReKontabel.Columns[0].Visible = false;
            grid_SkemaReKontabel.Columns[2].Visible = false;
            grid_SkemaReKontabel.Columns[3].Caption = "Skema Model";
            ////grid_SkemaReKontabel.Columns[3].VisibleIndex = 0;
            grid_SkemaReKontabel.Columns[4].Caption = "LLogaria";
            ////grid_SkemaReKontabel.SettingsBehavior.ColumnResizeMode = ColumnResizeMode.NextColumn;
            ////grid_SkemaReKontabel.SettingsCookies.Enabled = true;
            ////grid_SkemaReKontabel.SettingsCookies.StoreColumnsWidth = true;         
        }

        private void shtoKolone()
        {
            grid_SkemaReKontabel.Columns.Remove(grid_SkemaReKontabel.Columns["DebiKrediSkemaKontabelTrupi"]);
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            grid_SkemaReKontabel.Columns.Add(colnew);
            colnew.VisibleIndex = 1;
            //colnew.Caption = "Debi/Kredi";
            colnew.FieldName = "DebiKrediSkemaKontabelTrupi";
        }
              
        protected void grid_SkematKontabel_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName == "AktivSkemaKontabelKoka")
            {
                (e.Editor as ASPxComboBox).Items.Clear();
                (e.Editor as ASPxComboBox).Items.Add("");
                (e.Editor as ASPxComboBox).Items.Add("Aktive", true);
                (e.Editor as ASPxComboBox).Items.Add("Jo Aktive", false);
            }
        }

        protected void grid_SkematKontabel_DataBound(object sender, EventArgs e)
        {
            //GridViewCommandColumn check = new GridViewCommandColumn("#");
            //check.ShowSelectCheckbox = true;
            //check.SetColVisibleIndex(0);
            //grid_SkematKontabel.Columns.Add(check);

            //behet per te afishuar rreshtin qe do sherbej per filtrim
            grid_SkematKontabel.Settings.ShowFilterRow = true;
            grid_SkematKontabel.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
            grid_SkematKontabel.Settings.ShowFilterRowMenu = true;

            grid_SkematKontabel.KeyFieldName = "IdSkemaKontabelKoka";
            grid_SkematKontabel.SettingsBehavior.AllowMultiSelection = true;
            grid_SkematKontabel.SettingsBehavior.AllowFocusedRow = true;
        }

        protected void grid_SkematKontabel_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            //boshatisim Session["SkemaTrupiRi"] sepse PerformCallback behet kur perdoruesi ben doubleclick ne gride
            Session["SkemaTrupiRi"] = null;
            konfiguroVleraFillestare();
            percaktoTamplateSkemaKontabel();
        }

        protected void grid_SkematKontabel_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            int j;
            for (j = 0; j < e.NewValues.Count; j++)
            {
                if (j == 2 || j == 3 || j == 5)
                    continue;
                if (e.NewValues[j] == null)
                {
                    e.Cancel = true;
                    return;
                }
            }
            string kodiSkemesKontabelTeZgjdhur = "";
            koka = new DbKontabiliteti.clsSkemaKontabelKoka();
            DbKontabiliteti.clsDatabaseKontabilitet dbAction = new DbKontabiliteti.clsDatabaseKontabilitet();
            koka.KodiSkemaKontabelKoka = e.NewValues["KodiSkemaKontabelKoka"].ToString();
            koka.PershkrimiSkemaKontabelKoka = e.NewValues["PershkrimiSkemaKontabelKoka"].ToString();
            if (e.NewValues["AktivSkemaKontabelKoka"] != null)
                koka.AktivSkemaKontabelKoka = (bool)(e.NewValues["AktivSkemaKontabelKoka"]);
            else
                koka.AktivSkemaKontabelKoka = false;
            koka.IdAutorizimSkemaKontabelKoka = hfAutorizime.Value;
            //if (hfAutorizime.Value != "")
            //{
            //    koka.IdAutorizimSkemaKontabelKoka = new clsDatabaseAdmin().ktheAutorizim(hfAutorizime.Value)[0].IdAutorizimKoka;
            //}
            koka.IdKursiSkemaKontabelKoka = int.Parse(e.NewValues["IdKursiSkemaKontabelKoka"].ToString());
            if (hfNrAutomatik.Value != "")
            {
                DbAdmin.clsNrAutom num = new clsNrAutom(hfNrAutomatik.Value);
                koka.NrAutoSkemaKontabelKoka = num.IdNrAutom;// int.Parse(e.NewValues[5].ToString());
            }
            clsFunksione funk = new clsFunksione();
            koka.IdNderViti = funk.ktheNdermarrjeVit();
            koka.IdPerdoruesi = oPerdorues.IdPerdorues;
            ////clsFunksione funk = new clsFunksione();
            ////DbKontabiliteti.colSkemaKontabelTrupi colTrupi = new DbKontabiliteti.colSkemaKontabelTrupi();
            ////if (Session["SkemaTrupiRi"] != null)
            ////{
            ////    //merren nga seksioni gjithe rreshtat e shtuar per skemen e re kontabel
            ////    colTrupi = funk.KtheNeObjekt(Session["SkemaTrupiRi"].ToString());
            ////    koka.OColTrupi = colTrupi;
            ////}
            koka.OColTrupi = oColSkemaKontabelTrupi;
            if (koka.OColTrupi.Count > 0)
            {
                for (int i = 0; i < koka.OColTrupi.Count; i++)
                {
                    if (koka.OColTrupi[i].DebiKrediSkemaKontabelTrupi.Trim() == "")
                    {
                        koka.OColTrupi.RemoveAt(i);
                    }
                    else
                    {
                        DbKontabiliteti.clsSkemaKontabelKoka oSkemaKoka = new DbKontabiliteti.clsSkemaKontabelKoka();
                        DbKontabiliteti.clsLlogari oLlogaria = new DbKontabiliteti.clsLlogari();
                        oLlogaria.NrLlogari = koka.OColTrupi[i].LlogariSkemaKontabelTrupi.Trim();
                        oLlogaria = oLlogaria.merrLlogariSipasKodit();
                        koka.OColTrupi[i].IdLlogariSkemaKontabelTrupi = oLlogaria.IdLlogari;
                        //behet kjo gje, sepse ne analize eshte kerkuar qe kur nuk vendoset skeme tek trupi,
                        //te merret ajo e paraardheses.
                        if (koka.OColTrupi[i].KodiSkemaModel.Trim() != "")
                        {
                            kodiSkemesKontabelTeZgjdhur = koka.OColTrupi[i].KodiSkemaModel.Trim();
                        }
                        else
                        {
                            koka.OColTrupi[i].KodiSkemaModel = kodiSkemesKontabelTeZgjdhur;
                        }
                        oSkemaKoka.KodiSkemaKontabelKoka = koka.OColTrupi[i].KodiSkemaModel.Trim();
                        oSkemaKoka = oSkemaKoka.merrSkemeKontabelSipasKodit();
                        koka.OColTrupi[i].IdSkemaModel = oSkemaKoka.IdSkemaKontabelKoka;
                    }
                }
                ////foreach (DbKontabiliteti.clsSkemaKontabelTrupi trupi in colTrupi)
                ////{
                //ruaj koken ne databaze bashke me trupin
                //kontrolloKushtetPerSkematDefaulte(koka);
                koka.ruajKokenTrupin(koka.IdSkemaKontabelKoka, koka.KodiSkemaKontabelKoka, koka.PershkrimiSkemaKontabelKoka, koka.AktivSkemaKontabelKoka, koka.IdKursiSkemaKontabelKoka,
                    koka.NrAutoSkemaKontabelKoka, koka.IdNderViti, koka.IdPerdoruesi, koka.IdNdermarje, koka.OColTrupi);
                ////}
            }
            //else
            //lajmero perdoruesin qe duhet te plotesoje trupin
            //grid_SkematKontabel.CancelEdit();
            e.Cancel = true;
            ASPxGridView gv = sender as ASPxGridView;
            gv.CancelEdit();
            //grid_SkematKontabel.CancelEdit();
            InitializeSkemaTrupiGrid();
            hfAutorizime.Value = "";
            hfNrAutomatik.Value = "";
        }

        //protected void grid_SkematKontabel_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        //{
          
        //    foreach (GridViewColumn column in grid_SkematKontabel.Columns)
        //    {
        //        if (column.Visible == true)
        //        {
        //            GridViewDataColumn dataColumn = column as GridViewDataColumn;
        //            if (dataColumn == null) continue;
        //            //if (dataColumn.FieldName == "IdAutorizimSkemaKontabelKoka")//validimi per Autorizim qe eshte tip tjeter kolone
        //            //    if (this.hfAutorizime .Value == "" )
        //            //        e.Errors[dataColumn] = "Vlera nuk mund te jete null.";

        //            //if (dataColumn.FieldName == "NrAutoSkemaKontabelKoka")//validimi per Nr Automatik kredi qe eshte tip tjeter kolone
        //            //    if (this.hfNrAutomatik .Value == "" )
        //            //        e.Errors[dataColumn] = "Vlera nuk mund te jete null.";
        //            if (e.NewValues[dataColumn.FieldName] == null & dataColumn.FieldName != "IdAutorizimSkemaKontabelKoka" & dataColumn.FieldName != "NrAutoSkemaKontabelKoka" & dataColumn.FieldName != "AktivSkemaKontabelKoka")//validimi per kolonat e tjera te detyrueshme
        //            {
        //                e.Errors[dataColumn] = "Vlera nuk mund te jete null.";
        //            }
        //        }

        //    }
        //    if (e.Errors.Count > 0)
        //    {
        //        e.RowError = "Ju lutemi, plotesoni te gjitha fushat.";
        //        percaktoTamplateSkemaKontabel();
        //    }

        //    if (string.IsNullOrEmpty(e.RowError) && e.Errors.Count > 0)
        //    {
        //        e.RowError = "Ju lutemi, korrigjoni te gjithe gabimet.";
        //        percaktoTamplateSkemaKontabel();
        //    }

        //}

        //protected void grid_SkematKontabel_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        //{
        //    if (!grid_SkematKontabel.IsNewRowEditing)
        //    {
        //        grid_SkematKontabel.DoRowValidation();
        //    }

        //}
       
        private void kontrolloKushtetPerSkematDefaulte(clsSkemaKontabelKoka oKoka)
        {
            IEnumerable<clsSkemaKontabelTrupi> oColTrupi = (from clsSkemaKontabelTrupi skt in oKoka.OColTrupi
                                                            where
                                                     skt.KodiSkemaModel.Equals("FSA")
                                                            select skt);
            for (int i = 0; i < oColTrupi.Count(); i++)
            {
                clsLlogari oLLogari = new clsLlogari();
                oLLogari.IdLlogari = oColTrupi.ElementAt(i).IdLlogariSkemaKontabelTrupi;
                DbKontabiliteti.clsMesazh mesazh = oLLogari.eshteLlogariKlienti();
            }
            ////colSkemaKontabelTrupi oColTrupi1 =(colSkemaKontabelTrupi)(new List<clsSkemaKontabelTrupi>());
            //IEnumerable<clsSkemaKontabelTrupi> oColTrupi = oKoka.OColTrupi.Where(p => p.KodiSkemaModel == "FSL");
        }

        private void InitializeSkemaTrupiGrid()
        {
            oColSkemaKontabelTrupi = new colSkemaKontabelTrupi();
            clsSkemaKontabelTrupi oSkemaTrupi = new clsSkemaKontabelTrupi();
            for (int i = 0; i < 5; i++)
            {
                oSkemaTrupi = new clsSkemaKontabelTrupi();
                oColSkemaKontabelTrupi.Add(oSkemaTrupi);
            }
            grid_SkemaReKontabel.DataSource = oColSkemaKontabelTrupi;
            grid_SkemaReKontabel.DataBind();
        }

        protected void grid_SkemaReKontabel_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            clsFunksione funk = new clsFunksione();

            DbKontabiliteti.colSkemaKontabelTrupi colTrupi = new DbKontabiliteti.colSkemaKontabelTrupi();

            //kontrollohet nese ka rreshta te shtuar me pare per kete skeme
            if (Session["SkemaTrupiRi"] != null)
                colTrupi = funk.KtheNeObjekt(Session["SkemaTrupiRi"].ToString());

            DbKontabiliteti.clsSkemaKontabelTrupi trupi = new DbKontabiliteti.clsSkemaKontabelTrupi();
            trupi.IdSkemaModel = int.Parse(e.NewValues[0].ToString());
            trupi.DebiKrediSkemaKontabelTrupi = e.NewValues[1].ToString();
            trupi.LlogariSkemaKontabelTrupi = e.NewValues[2].ToString();

            //koleksionit te rreshtave ekzistues u shtohet dhe rreshti aktual colTrupi.Add(trupi);

            //if (Session["TrupiIKokesSeSelektuar"] != null)
            //{
            //    DbKontabiliteti.colSkemaKontabelTrupi colEkzistues = funk.KtheNeObjekt(Session["TrupiIKokesSeSelektuar"].ToString());
            //    foreach (DbKontabiliteti.clsSkemaKontabelTrupi o in colEkzistues)
            //    {
            //        colTrupi.Add(o);
            //    }
            //}

            //Session["SkemaTrupiRi"] azhornohet me rreshtin e ri te shtuar ne koleksion
            String colIRi = funk.KtheNeString(colTrupi);
            Session["SkemaTrupiRi"] = colIRi;
            //Session["SkemaTrupiEdituar"] = colIRi;

            e.Cancel = true;
            grid_SkemaReKontabel.CancelEdit();
        }

        protected void ASPxButton1_Click(object sender, EventArgs e)
        {
            grid_SkemaReKontabel.UpdateEdit();

            clsFunksione funk = new clsFunksione();
            DbKontabiliteti.colSkemaKontabelTrupi colTrupi = new DbKontabiliteti.colSkemaKontabelTrupi();
            colTrupi = funk.KtheNeObjekt(Session["SkemaTrupiRi"].ToString());
            grid_SkemaReKontabel.DataSource = colTrupi;
            grid_SkemaReKontabel.DataBind();

            grid_SkemaReKontabel.AddNewRow();
        }

        protected void grid_SkemaReKontabel_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            grid_SkemaReKontabel.DataBind();
        }

        protected void grid_SkemaReKontabel_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            //if (e.Editor.GetType().Name == "ASPxTextBox")
            //{

            //    ASPxTextBox currentEditor = e.Editor as ASPxTextBox;
            //    currentEditor.ClientSideEvents.TextChanged = "function(s,e){ProcessTextChanged('" + e.Column.FieldName + "',s.GetText());}";
            //}
            //else if (e.Editor.GetType().Name == "ASPxComboBox")
            //{
            //    ASPxComboBox currentEditor = e.Editor as ASPxComboBox;
            //    currentEditor.ClientSideEvents.TextChanged = "function(s,e){ProcessTextChanged('" + e.Column.FieldName + "',s.GetText());}";
            //}

            if (e.Column.FieldName == "DebiKrediSkemaKontabelTrupi")
            {
                (e.Editor as ASPxComboBox).Items.Clear();
                (e.Editor as ASPxComboBox).Items.Add("");
                (e.Editor as ASPxComboBox).Items.Add("Debi", "Debi");
                (e.Editor as ASPxComboBox).Items.Add("Kredi", "Kredi");
            }
        }

        protected void ruaj_Button_Click(object sender, EventArgs e)
        {
            grid_SkematKontabel.UpdateEdit();
            //Boshatiset Session["SkemaTrupiRi"] duke qene se tashme 
            //rreshtat e shtuar per skemen e re u shtuan ne databaze dhe nuk na duhen me
            Session["SkemaTrupiRi"] = null;
        }

        protected void grid_SkematKontabel_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            //id e skemes qe po modifikohet
            String idKoka = e.Keys["IdSkemaKontabelKoka"].ToString();
            DbKontabiliteti.clsDatabaseKontabilitet db = new clsDatabaseKontabilitet();
            clsSkemaKontabelKoka skemaEVjeter = new clsSkemaKontabelKoka(int.Parse(idKoka));
            //clsSkemaKontabelKoka skemaEVjeter = db.merrSkemeKontabelSipasId(int.Parse (idKoka));
            DbKontabiliteti.clsSkemaKontabelKoka skemaEEdituar = new DbKontabiliteti.clsSkemaKontabelKoka();
            skemaEEdituar.KodiSkemaKontabelKoka = e.NewValues["KodiSkemaKontabelKoka"].ToString();
            skemaEEdituar.PershkrimiSkemaKontabelKoka = e.NewValues["PershkrimiSkemaKontabelKoka"].ToString();
            if (e.NewValues["AktivSkemaKontabelKoka"] != null)
                skemaEEdituar.AktivSkemaKontabelKoka = (bool)(e.NewValues["AktivSkemaKontabelKoka"]);
            else
                skemaEEdituar.AktivSkemaKontabelKoka = false;
            skemaEEdituar.IdPerdoruesi = oPerdorues.IdPerdorues;
            if (hfAutorizime.Value != "")
                skemaEEdituar.IdAutorizimSkemaKontabelKoka = hfAutorizime.Value;
            else
                skemaEEdituar.IdAutorizimSkemaKontabelKoka = skemaEVjeter.IdAutorizimSkemaKontabelKoka;
            
            //if (hfAutorizime.Value != "")
            //{
            //    skemaEEdituar.IdAutorizimSkemaKontabelKoka = new clsDatabaseAdmin().ktheAutorizim(hfAutorizime.Value)[0].IdAutorizimKoka;
            //}
            skemaEEdituar.IdKursiSkemaKontabelKoka = int.Parse(e.NewValues["IdKursiSkemaKontabelKoka"].ToString());
            if (hfNrAutomatik.Value != "")
            {
                DbAdmin.clsNrAutom num = new clsNrAutom(hfNrAutomatik.Value);
                skemaEEdituar.NrAutoSkemaKontabelKoka = num.IdNrAutom;// int.Parse(e.NewValues[5].ToString());
            }
            else skemaEEdituar.NrAutoSkemaKontabelKoka = skemaEVjeter.NrAutoSkemaKontabelKoka;
            skemaEEdituar.IdSkemaKontabelKoka = int.Parse(idKoka);
              string kodiSkemesKontabelTeZgjdhur = "";
            clsFunksione funk = new clsFunksione();
            if (Session["SkemaTrupiEdituar"] != null) //nese ndonje nga rreshtat e trupit eshte modifikuar
            {
                DbKontabiliteti.colSkemaKontabelTrupi colTrupi = funk.KtheNeObjekt(Session["SkemaTrupiEdituar"].ToString());
                 skemaEEdituar.OColTrupi = colTrupi ;
                 if (skemaEEdituar.OColTrupi.Count > 0)
                 {
                     for (int i = 0; i < skemaEEdituar.OColTrupi.Count; i++)
                     {
                         if (skemaEEdituar.OColTrupi[i].DebiKrediSkemaKontabelTrupi.Trim() == "")
                         {
                             skemaEEdituar.OColTrupi.RemoveAt(i);
                         }
                         else
                         {
                             DbKontabiliteti.clsSkemaKontabelKoka oSkemaKoka = new DbKontabiliteti.clsSkemaKontabelKoka();
                             DbKontabiliteti.clsLlogari oLlogaria = new DbKontabiliteti.clsLlogari();
                             oLlogaria.NrLlogari = skemaEEdituar.OColTrupi[i].LlogariSkemaKontabelTrupi.Trim();
                             oLlogaria = oLlogaria.merrLlogariSipasKodit();
                             skemaEEdituar.OColTrupi[i].IdLlogariSkemaKontabelTrupi = oLlogaria.IdLlogari;
                             //behet kjo gje, sepse ne analize eshte kerkuar qe kur nuk vendoset skeme tek trupi,
                             //te merret ajo e paraardheses.
                             if (skemaEEdituar.OColTrupi[i].KodiSkemaModel.Trim() != "")
                             {
                                 kodiSkemesKontabelTeZgjdhur = skemaEEdituar.OColTrupi[i].KodiSkemaModel.Trim();
                             }
                             else
                             {
                                 skemaEEdituar.OColTrupi[i].KodiSkemaModel = kodiSkemesKontabelTeZgjdhur;
                             }
                             oSkemaKoka.KodiSkemaKontabelKoka = skemaEEdituar.OColTrupi[i].KodiSkemaModel.Trim();
                             oSkemaKoka = oSkemaKoka.merrSkemeKontabelSipasKodit();
                             skemaEEdituar.OColTrupi[i].IdSkemaModel = oSkemaKoka.IdSkemaKontabelKoka;
                         }
                     }

                 }

            }

            else //nese nuk kemi modifikuar trupin mbajme trupin eksiztues
            {
                colSkemaKontabelTrupi colskemaKontTrupi = new colSkemaKontabelTrupi();
                colskemaKontTrupi.mbushTrupinESkemes(skemaEEdituar.IdSkemaKontabelKoka);
                skemaEEdituar.OColTrupi = colskemaKontTrupi;
                //DbKontabiliteti.clsDatabaseKontabilitet data = new DbKontabiliteti.clsDatabaseKontabilitet();
                //skemaEEdituar.OColTrupi = data.ktheTrupinESkemes(skemaEEdituar.IdSkemaKontabelKoka);
            }

            skemaEEdituar.modifikoKokenTrupin(koka.IdSkemaKontabelKoka, koka.KodiSkemaKontabelKoka, koka.PershkrimiSkemaKontabelKoka, koka.AktivSkemaKontabelKoka, koka.IdKursiSkemaKontabelKoka,
                    koka.NrAutoSkemaKontabelKoka, koka.IdPerdoruesi, koka.IdNdermarje, koka.OColTrupi);

            e.Cancel = true;
            grid_SkematKontabel.CancelEdit();
            InitializeSkemaTrupiGrid();
            hfAutorizime.Value = "";
            hfNrAutomatik.Value = "";
        }

        protected void grid_SkemaReKontabel_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            String idTrupi = e.Keys["IdSkemaKontabelTrupi"].ToString();

            clsFunksione funk = new clsFunksione();
            DbKontabiliteti.colSkemaKontabelTrupi col = new DbKontabiliteti.colSkemaKontabelTrupi();
            if (Session["SkemaTrupiEdituar"] == null) //nese nuk eshte edituar akoma asnje rresht
                col = funk.KtheNeObjekt(Session["TrupiIKokesSeSelektuar"].ToString());
            else
                //nese kemi rreshta te edituar ath koleksioni i perban tashme te gjithe rreshtat (te ndryshuar dhe jo)
                col = funk.KtheNeObjekt(Session["SkemaTrupiEdituar"].ToString());

            foreach (DbKontabiliteti.clsSkemaKontabelTrupi trupi in col)
            {
                //nese trupi eshte elementi qe po azhornohet ath te gjitha property-t e tij i mbushim me vlerat e reja
                if (trupi.IdSkemaKontabelTrupi.ToString() == idTrupi)
                {
                    trupi.IdSkemaModel = int.Parse(e.NewValues[0].ToString());
                    trupi.DebiKrediSkemaKontabelTrupi = e.NewValues[1].ToString();
                    trupi.LlogariSkemaKontabelTrupi = e.NewValues[2].ToString();
                }
            }

            //ne Session["SkemaTrupiEdituar"] ruhet koleksioni i rreshtave bashke me ndryshimet e ndodhura
            String colIRi = funk.KtheNeString(col);
            Session["SkemaTrupiEdituar"] = colIRi;

            e.Cancel = true;
            grid_SkemaReKontabel.CancelEdit();
        }

        protected void Apliko_ASPxButton_Click(object sender, EventArgs e)
        {
            DbAdmin.clsDatabaseAdmin dbAdmin = new DbAdmin.clsDatabaseAdmin();
            DbAdmin.clsFiltraGrida filtra = new DbAdmin.clsFiltraGrida();
            filtra.mbushFiltraGridaSipasFiltraKodi(Filtri_ASPxTextBox.Text, new clsFunksione().ktheIdNdermarrje());
            //DbAdmin.clsFiltraGrida filtra = dbAdmin.merrFiltraGridaSipasFiltraKodi(Filtri_ASPxTextBox.Text, new clsFunksione().ktheIdNdermarrje());
            grid_SkematKontabel.FilterExpression = filtra.FiltraVlera;
            if (filtra.DrejtimRenditje == true)
                grid_SkematKontabel.SortBy(grid_SkematKontabel.Columns[filtra.KoloneRenditje], DevExpress.Data.ColumnSortOrder.Ascending);
            else
                grid_SkematKontabel.SortBy(grid_SkematKontabel.Columns[filtra.KoloneRenditje], DevExpress.Data.ColumnSortOrder.Descending);
          
            konfiguroVleraFillestare();
            this.Filtri_ASPxTextBox.Text = "";
        }

        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                DbAdmin.clsDatabaseAdmin dbAdmin = new DbAdmin.clsDatabaseAdmin();
                DbAdmin.clsFiltraGrida filtri = new DbAdmin.clsFiltraGrida();
                filtri.FiltraKodi = Kodi_ASPxTextBox.Text;
                filtri.FiltraShenime = Shenime_ASPxTextBox.Text;
                filtri.FiltraUniversal = false;//Universal_ASPxCheckBox.Checked;
                //DbAdmin.clsGridaKoka koka = dbAdmin.merrGridaKokaByEmri("grid_SkematKontabel", "SkematKontabel.aspx", new clsFunksione().ktheIdNdermarrje());
                DbAdmin.clsGridaKoka koka = new clsGridaKoka("grid_SkematKontabel", "SkematKontabel.aspx", new clsFunksione().ktheIdNdermarrje());
                filtri.GridaKokaId = koka.IdGridaKoka;
                filtri.FiltraVlera = grid_SkematKontabel.FilterExpression;
                System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = grid_SkematKontabel.GetSortedColumns();
                if (kolona.Count > 0)
                {

                    filtri.KoloneRenditje = kolona[0].FieldName;
                    if (kolona[0].SortOrder == DevExpress.Data.ColumnSortOrder.Ascending)
                        filtri.DrejtimRenditje = true;
                    else
                        filtri.DrejtimRenditje = false;
                }
                else
                {
                    filtri.KoloneRenditje = "KodiSkemaKontabelKoka";
                    filtri.DrejtimRenditje = true;
                }
                DbAdmin.clsPerdorues oPerdorues = new DbAdmin.clsPerdorues();
                oPerdorues = (DbAdmin.clsPerdorues)(Session["oClsPerdoruesi"]);
                filtri.IdPerdoruesi = oPerdorues.IdPerdorues;
                filtri.IdNdermarje = new clsFunksione().ktheIdNdermarrje();
                filtri.IdStatusDok = 1;
                filtri.ruaj();
                Kodi_ASPxTextBox.Text = "";
                Shenime_ASPxTextBox.Text = "";
                //Universal_ASPxCheckBox.Text = "";
                popRuaj.ShowOnPageLoad = false;
            }
        }

        protected void Kodi_CustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;
            DbAdmin.clsDatabaseAdmin dbAdmin = new DbAdmin.clsDatabaseAdmin();
            if (dbAdmin.ekzistonFilter(Kodi_ASPxTextBox.Text, new clsFunksione().ktheIdNdermarrje()))
                args.IsValid = false;
        }

        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            List<object> rreshtat = grid_SkematKontabel.GetSelectedFieldValues("IdSkemaKontabelKoka");

            foreach (int id in rreshtat)
            {
                //DbKontabiliteti.clsDatabaseKontabilitet dbKontabilitet = new DbKontabiliteti.clsDatabaseKontabilitet();
                //DbKontabiliteti.clsSkemaKontabelKoka skema = dbKontabilitet.merrSkemeKontabelSipasId(id);
                clsSkemaKontabelKoka skema = new clsSkemaKontabelKoka(id);
                skema.fshiSkemeKontabel(id);
            }
            Response.Redirect("SkematKontabel.aspx");
        }

        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {
            if (e.Item.Name == "Shto")
            {
                grid_SkematKontabel.AddNewRow();

                //grid_SkemaReKontabel.AddNewRow();
                DbKontabiliteti.colSkemaKontabelTrupi col = new DbKontabiliteti.colSkemaKontabelTrupi();
                DbKontabiliteti.clsSkemaKontabelTrupi o;
                for (int i = 0; i < 5; i++)
                {
                    o = new DbKontabiliteti.clsSkemaKontabelTrupi();
                    col.Add(o);
                }
                grid_SkemaReKontabel.DataSource = col;
                grid_SkemaReKontabel.DataBind();
            }
            else if (e.Item.Name == "Modifiko")
            {
                konfiguroVleraFillestare();

                int indeksi = grid_SkematKontabel.FocusedRowIndex;
                string id;

                grid_SkematKontabel.StartEdit(indeksi);
                if (grid_SkematKontabel.GetRowValues(indeksi, "IdSkemaKontabelKoka") != null)
                {
                    id = grid_SkematKontabel.GetRowValues(indeksi, "IdSkemaKontabelKoka").ToString();
                    DbKontabiliteti.colSkemaKontabelTrupi col = new colSkemaKontabelTrupi();
                    col.mbushSkemeTrupiSipasIdKoka(id);
                   //DbKontabiliteti.colSkemaKontabelTrupi col = new DbKontabiliteti.clsDatabaseKontabilitet ().merrSkemeTrupiSipasIdKoka(id);
                   DbKontabiliteti.clsSkemaKontabelTrupi o;
                   for (int i = 0; i < 1; i++)
                   {
                       o = new DbKontabiliteti.clsSkemaKontabelTrupi();
                       col.Add(o);
                   }
                    grid_SkemaReKontabel.DataSource = col;
                grid_SkemaReKontabel.DataBind();
                percaktoTamplateSkemaKontabel();
                }
                ////DbKontabiliteti.clsSkemaKontabelTrupi o;
                //for (int i = 0; i < 5; i++)
                //{
                //    o = new DbKontabiliteti.clsSkemaKontabelTrupi();
                //    col.Add(o);
                //}
                //
            }
        }

        private void percaktoTemplate()
        {
            GridViewDataTextColumn col1 = grid_SkemaReKontabel.Columns["KodiSkemaModel"] as GridViewDataTextColumn;
            col1.DataItemTemplate = new MyTemplate();
            GridViewDataComboBoxColumn col2 = grid_SkemaReKontabel.Columns["DebiKrediSkemaKontabelTrupi"] as GridViewDataComboBoxColumn;
            col2.DataItemTemplate = new MyTemplateCombo();
            GridViewDataTextColumn col3 = grid_SkemaReKontabel.Columns["LlogariSkemaKontabelTrupi"] as GridViewDataTextColumn;
            col3.DataItemTemplate = new MyTemplate();
        }

        protected void grid_SkemaReKontabel_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            bool ugjet;
            if (e.RowType == DevExpress.Web.GridViewRowType.Data)
            {
                GridViewDataColumn col1 = ((ASPxGridView)sender).Columns["KodiSkemaModel"] as GridViewDataColumn;
                GridViewDataColumn col2 = ((ASPxGridView)sender).Columns["DebiKrediSkemaKontabelTrupi"] as GridViewDataColumn;
                GridViewDataColumn col3 = ((ASPxGridView)sender).Columns["LlogariSkemaKontabelTrupi"] as GridViewDataColumn;
                ////GridViewDataColumn col4 = ((ASPxGridView)sender).Columns["btn"] as GridViewDataColumn;
                ////ASPxButtonEdit t = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col1, "txtBox") as ASPxButtonEdit;
                ////t.ClientSideEvents.
                ASPxComboBox txt1 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col1, "txtBox") as ASPxComboBox;

                ASPxComboBox cmb1 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col2, "cmbBox") as ASPxComboBox;
                ASPxComboBox txt2 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col3, "txtBox") as ASPxComboBox;
                ////ASPxTextBox txt3 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col3, "txtBox") as ASPxTextBox;
                ////ASPxButton b = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col4, "btn") as ASPxButton;

                ugjet = false;
                if (txt1 != null)
                {
                    ////c.ClientSideEvents.b
                    ////ASPxComboBox c = new ASPxComboBox();
                    ////c.TextField = "text";
                    ////c.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                    ////c.DropDownStyle = DropDownStyle.DropDown;
                    ////c.ValueField = "";
                    ////c.DataSource = "";
                    ////c.DataBind();
                    txt1.TextField = "KodiSkemaKontabelKoka";
                    txt1.ValueField = "IdSkemaKontabelKoka";
                    txt1.DataSource = colSkema;
                    txt1.DataBind();
                    txt1.ClientInstanceName = "txtKodiSkemaModel" + e.VisibleIndex.ToString();
                    txt1.ClientSideEvents.TextChanged = "function(s,e){TextChangedIdSkemaModel(txtKodiSkemaModel" + e.VisibleIndex.ToString() + ",'txtKodiSkemaModel', " + e.VisibleIndex.ToString() + ");}";
                    txt1.ClientSideEvents.KeyPress = "function(s,e){var code = _getKeyCode(e.htmlEvent); KeyPresKodiSkemes(code,txtKodiSkemaModel" + e.VisibleIndex.ToString() + "); }";
                    txt1.ClientSideEvents.LostFocus = "function(s,e){LostFocusIdSkemaModel(txtKodiSkemaModel" + e.VisibleIndex.ToString() + ",'txtKodiSkemaModel', " + e.VisibleIndex.ToString() + ");}";
                    txt1.ClientSideEvents.ButtonClick = "function(s,e){ButtonClickedkodiSkemes(txtKodiSkemaModel" + e.VisibleIndex.ToString() + "); }";
                    txt1.ClientSideEvents.GotFocus = "function(s,e){GotFocusKodiSkemes(txtKodiSkemaModel" + e.VisibleIndex.ToString() + ",'txtKodiSkemaModel', " + e.VisibleIndex.ToString() + ");}";
                  
                    if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 2)
                    {
                        if (ugjet)
                        {
                            tempcombo = txt1;
                            ugjet = false;
                        }
                        else if (koloneFocus == "txtKodiSkemaModel")
                        {
                            ugjet = true;
                        }
                    }
                    //txt1.ClientSideEvents.GotFocus = "function(s,e){GotFocus();}";
                }

                if (cmb1 != null)
                {
                    cmb1.ClientInstanceName = "cmbDebiKredi" + e.VisibleIndex.ToString();
                    cmb1.ClientSideEvents.TextChanged = "function(s,e){TextChangedDebiKredi(cmbDebiKredi" + e.VisibleIndex.ToString() + ",'DebiKrediSkemaKontabelTrupi'," + e.VisibleIndex.ToString() + ");}";
                    cmb1.ClientSideEvents.GotFocus = "function(s,e){GotFocusDebiKredi(cmbDebiKredi" + e.VisibleIndex.ToString() + ",'DebiKrediSkemaKontabelTrupi', " + e.VisibleIndex.ToString() + ");}";
                    if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 2)
                    {
                        if (ugjet)
                        {
                            tempcombo = cmb1;
                            ugjet = false;
                        }
                        else if (koloneFocus == "DebiKrediSkemaKontabelTrupi")
                        {
                            ugjet = true;
                        }
                    }
                    //txt2.ClientSideEvents.GotFocus = "function(s,e){GotFocus(txtProveQyteti" + e.VisibleIndex.ToString() + ");}";
                }
                if (txt2 != null)
                {
                    txt2.TextField = "NrLlogari";
                    txt2.ValueField = "IdLlogari";
                    txt2.DataSource = colLlog;
                    txt2.DataBind();
                    txt2.ClientInstanceName = "txtLlogaria" + e.VisibleIndex.ToString();
                    txt2.ClientSideEvents.TextChanged = "function(s,e){TextChangedLlogari(txtLlogaria" + e.VisibleIndex.ToString() + ",'LlogariSkemaKontabelTrupi'," + e.VisibleIndex.ToString() + ");}";
                    txt2.ClientSideEvents.ButtonClick = "function(s,e){ButtonClickedLlogaria(txtLlogaria" + e.VisibleIndex.ToString() + "); }";
                    txt2.ClientSideEvents.LostFocus = "function(s,e){LostFocusLlogaria(txtLlogaria" + e.VisibleIndex.ToString() + ",'txtLlogaria', " + e.VisibleIndex.ToString() + ");}";
                    txt2.ClientSideEvents.KeyPress = "function(s,e){var code = _getKeyCode(e.htmlEvent); KeyPresLlogaria(code,txtLlogaria" + e.VisibleIndex.ToString() + "); }";
                    txt2.ClientSideEvents.GotFocus = "function(s,e){GotFocusLlogaria(txtLlogaria" + e.VisibleIndex.ToString() + ", 'LlogariSkemaKontabelTrupi'," + e.VisibleIndex.ToString() + ");}";
                    if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 2)
                    {
                        if (ugjet)
                        {
                            tempcombo = txt2;
                            ugjet = false;
                        }
                        else if (koloneFocus == "LlogariSkemaKontabelTrupi")
                        {
                            ugjet = true;
                        }
                    }
                    //txt2.ClientSideEvents.GotFocus = "function(s,e){GotFocus(txtProveQyteti" + e.VisibleIndex.ToString() + ");}";
                }
            }
            if (tempcombo != null)
            {
                tempcombo.Focus();
            }
            else if (tempcombo != null)
            {
                tempcombo.Focus();
            }
        }

        protected void grid_SkemaReKontabel_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            oColSkemaKontabelTrupi = new DbKontabiliteti.colSkemaKontabelTrupi();
            string[] arrSkemakontabel = e.Parameters.ToString().Split(';');
            if (arrSkemakontabel[0] == "modifiko")
            {
               
                //ketu duhet te merret trupi i skemes kontables sipas idse se kokes.
                oColSkemaKontabelTrupi = oColSkemaKontabelTrupi.merrSkemaTrupiSipasIdKoka(arrSkemakontabel[1]);
                DbKontabiliteti.clsSkemaKontabelTrupi oSkemaTrupiNew = new DbKontabiliteti.clsSkemaKontabelTrupi();
                   
                  oSkemaTrupiNew = new DbKontabiliteti.clsSkemaKontabelTrupi();
                oSkemaTrupiNew.IdSkemaModel = 0;
                oSkemaTrupiNew.DebiKrediSkemaKontabelTrupi = "";
                oSkemaTrupiNew.LlogariSkemaKontabelTrupi = "";
                oColSkemaKontabelTrupi.Add(oSkemaTrupiNew);
                  grid_SkemaReKontabel.DataSource = oColSkemaKontabelTrupi;
                grid_SkemaReKontabel.DataBind();
            }
            else
            {
                string[] arrKodiSkemaKontabel = arrSkemakontabel[0].Split(',');
                for (int i = 0; i < arrKodiSkemaKontabel.Length; i++)
                {
                    string[] arrCompKodiSkemaKontabel = arrKodiSkemaKontabel[i].Split(':');
                    if (arrCompKodiSkemaKontabel.Length != 1)
                    {
                        if (Convert.ToInt32(arrCompKodiSkemaKontabel[0]) < oColSkemaKontabelTrupi.Count())
                        {
                            oColSkemaKontabelTrupi[Convert.ToInt32(arrCompKodiSkemaKontabel[0])].KodiSkemaModel = arrCompKodiSkemaKontabel[1];
                        }
                        else
                        {
                            DbKontabiliteti.clsSkemaKontabelTrupi oSkemaTrupi = new DbKontabiliteti.clsSkemaKontabelTrupi();
                            oSkemaTrupi.KodiSkemaModel = arrCompKodiSkemaKontabel[1];
                            oColSkemaKontabelTrupi.Add(oSkemaTrupi);
                        }
                    }
                }

                string[] arrDebiKredi = arrSkemakontabel[1].Split(',');
                for (int i = 0; i < arrDebiKredi.Length; i++)
                {
                    string[] arrCompDebikredi = arrDebiKredi[i].Split(':');
                    if (arrCompDebikredi.Length != 1)
                    {
                        if (Convert.ToInt32(arrCompDebikredi[0]) < oColSkemaKontabelTrupi.Count())
                        {
                            oColSkemaKontabelTrupi[Convert.ToInt32(arrCompDebikredi[0])].DebiKrediSkemaKontabelTrupi = arrCompDebikredi[1];
                        }
                        else
                        {
                            DbKontabiliteti.clsSkemaKontabelTrupi oSkemaTrupi = new DbKontabiliteti.clsSkemaKontabelTrupi();
                            oSkemaTrupi.DebiKrediSkemaKontabelTrupi = arrCompDebikredi[1];
                            oColSkemaKontabelTrupi.Add(oSkemaTrupi);
                        }
                    }
                }

                string[] arrLlogari = arrSkemakontabel[2].Split(',');
                for (int i = 0; i < arrLlogari.Length; i++)
                {
                    string[] arrCompLlogari = arrLlogari[i].Split(':');
                    if (arrCompLlogari.Length != 1)
                    {
                        if (Convert.ToInt32(arrCompLlogari[0]) < oColSkemaKontabelTrupi.Count())
                        {
                            oColSkemaKontabelTrupi[Convert.ToInt32(arrCompLlogari[0])].LlogariSkemaKontabelTrupi = arrCompLlogari[1];
                        }
                        else
                        {
                            DbKontabiliteti.clsSkemaKontabelTrupi oSkemaTrupi = new DbKontabiliteti.clsSkemaKontabelTrupi();
                            oSkemaTrupi.LlogariSkemaKontabelTrupi = arrCompLlogari[1];
                            oColSkemaKontabelTrupi.Add(oSkemaTrupi);
                        }
                    }
                }

                if (arrSkemakontabel[3] == "ruaj")
                {
                    clsFunksione funk = new clsFunksione();
                    string col = funk.KtheNeString(oColSkemaKontabelTrupi);
                    Session["SkemaTrupiEdituar"] = col;
                     
                    grid_SkematKontabel.UpdateEdit();
        
                        DbKontabiliteti.clsSkemaKontabelTrupi oSkemaTrupiNew = new DbKontabiliteti.clsSkemaKontabelTrupi();
                        for (int i = oColSkemaKontabelTrupi.Count; i < 5; i++)
                        {
                            oSkemaTrupiNew = new DbKontabiliteti.clsSkemaKontabelTrupi();
                            oSkemaTrupiNew.IdSkemaModel = 0;
                            oSkemaTrupiNew.DebiKrediSkemaKontabelTrupi = "";
                            oSkemaTrupiNew.LlogariSkemaKontabelTrupi = "";
                            oColSkemaKontabelTrupi.Add(oSkemaTrupiNew);
                        }
                        grid_SkemaReKontabel.DataSource = oColSkemaKontabelTrupi;
                        grid_SkemaReKontabel.DataBind();
                  
                }
                else
                {
                    string[] focus = arrSkemakontabel[3].Split(':');
                    koloneFocus = focus[0];
                    ////rreshtFocus = focus[1];
                    ////DbKontabiliteti.clsSkemaKontabelTrupi oSkemaTrupiNew = new DbKontabiliteti.clsSkemaKontabelTrupi();
                    ////for (int i = 1; i <= 5; i++)
                    ////{
                    ////    oSkemaTrupiNew = new DbKontabiliteti.clsSkemaKontabelTrupi();
                    ////    oSkemaTrupiNew.IdSkemaModel = 0;
                    ////    oSkemaTrupiNew.DebiKrediSkemaKontabelTrupi = "";
                    ////    oSkemaTrupiNew.LlogariSkemaKontabelTrupi = "";
                    ////    oColSkemaKontabelTrupi.Add(oSkemaTrupiNew);
                    ////}
                    grid_SkemaReKontabel.DataSource = oColSkemaKontabelTrupi;
                    grid_SkemaReKontabel.DataBind();
                }
            }
        }

        protected void grid_SkemaReKontabel_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpNoRows"] = grid_SkemaReKontabel.VisibleRowCount;
            ////if (temptxt != null)
            ////{
            ////    temptxt.Focus();
            ////}
            ////else if (tempcombo != null)
            ////{
            ////    tempcombo.Focus();
            ////}
        }

        protected void grid_SkemaReKontabel_DataBound(object sender, EventArgs e)
        {
        }

        void SetStyle(TableCell cell)
        {
            cell.Style[HtmlTextWriterStyle.TextOverflow] = "ellipsis";
            cell.Style[HtmlTextWriterStyle.Overflow] = "hidden";
            cell.Style[HtmlTextWriterStyle.WhiteSpace] = "nowrap";
        }

        protected void grid_SkemaReKontabel_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            ////if (e.RowType != DevExpress.Web.GridViewRowType.Data) return;
            ////e.Row.Height = 50;// Unit.Pixel(15 + (e.VisibleIndex % 2 == 0 ? 20 : 0));
        }

        protected void grid_SkemaReKontabel_HtmlFooterCellPrepared(object sender, ASPxGridViewTableFooterCellEventArgs e)
        {
            SetStyle(e.Cell);
        }

        protected void pastro_Button_Click(object sender, EventArgs e)
        {
            pastro();
        }

        private void pastro()
        {
            if (grid_SkematKontabel.IsNewRowEditing)
            {
                grid_SkematKontabel.CancelEdit();
                grid_SkematKontabel.AddNewRow();
                refreshSkemaTrupi();
            }
           
            hfNrAutomatik.Value = "";
            hfAutorizime.Value = "";
        }

        private void refreshSkemaTrupi()
        {
            DbKontabiliteti.clsSkemaKontabelTrupi oSkemaTrupiNew = new DbKontabiliteti.clsSkemaKontabelTrupi();
            for (int i = 1; i <= 5; i++)
            {
                oSkemaTrupiNew = new DbKontabiliteti.clsSkemaKontabelTrupi();
                oSkemaTrupiNew.IdSkemaModel = 0;
                oSkemaTrupiNew.DebiKrediSkemaKontabelTrupi = "";
                oSkemaTrupiNew.LlogariSkemaKontabelTrupi = "";
                oColSkemaKontabelTrupi.Add(oSkemaTrupiNew);
            }
            grid_SkemaReKontabel.DataSource = oColSkemaKontabelTrupi;
            grid_SkemaReKontabel.DataBind();
        }

        private void percaktoTamplateSkemaKontabel()
        {
            //tempatet per kolonat e Autorizimeve dhe NrAutomatik
            GridViewDataComboBoxColumn col7 = this.grid_SkematKontabel.Columns["IdAutorizimSkemaKontabelKoka"] as GridViewDataComboBoxColumn;
            col7.EditItemTemplate = new MyTemplateAutorizime();
            col7.Width = 100;

            GridViewDataComboBoxColumn col8 = grid_SkematKontabel.Columns["NrAutoSkemaKontabelKoka"] as GridViewDataComboBoxColumn;
            col8.EditItemTemplate = new MyTemplateNrAutomatik();
            col8.Width = 100;
        }

        protected void grid_SkematKontabel_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            percaktoTamplateSkemaKontabel();
        }

        protected void anullo_Button_Click(object sender, EventArgs e)
        {
            Response.Redirect("SkematKontabel.aspx");
        }
    } 

    class MyTemplateAutorizime : ITemplate //template i krijuar per Autorizime
    {
        public void InstantiateIn(Control container)
        {
            DbAdmin.clsDatabaseAdmin db = new  clsDatabaseAdmin();
            DbAdmin.colAutorizimetKoka colAutorizime = new DbAdmin.colAutorizimetKoka();
            colAutorizime.mbushGjitheAutorizimet(new clsFunksione().ktheIdNdermarrje());
            //DbAdmin.colAutorizimetKoka colAutorizime = db.merrGjitheAutorizimet();
            ASPxComboBox cmb = new ASPxComboBox();
            cmb.ClientInstanceName = "Autorizime";
            cmb.ClientSideEvents.KeyPress = "function(s,e){var code =_getKeyCode(e.htmlEvent);KeyPresAutorizime(code,Autorizime,0); }";
            cmb.ClientSideEvents.TextChanged = "function(s,e){TextChangedAutorizime(0); }";
            cmb.ClientSideEvents.LostFocus = "function(s,e){LostFocusAutorizime(0);}";
            cmb.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedAutorizime(Autorizime, 0) }";
            cmb.DropDownButton.Visible = false;
            EditButton b1 = new EditButton();

            cmb.Buttons.Add(b1);
            cmb.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            cmb.DropDownStyle = DropDownStyle.DropDownList;
            cmb.TextFormatString = "{0}";
            GridViewDataItemTemplateContainer gridContainer = (GridViewDataItemTemplateContainer)container;
            cmb.ID = "cmbBox";
            ListBoxColumn colprove = new ListBoxColumn();
            colprove.FieldName = "KodiAutorizim";
            ListBoxColumn colemer = new ListBoxColumn();
            colemer.FieldName = "PershkrimAutorizim";
            cmb.Columns.Add(colprove);
            cmb.Columns.Add(colemer);
            cmb.ValueField = "KodiAutorizim";
            cmb.DataSource = colAutorizime;
            cmb.DataBind();
            if (gridContainer.Column.FieldName == "IdAutorizimSkemaKontabelKoka" )
            {
                if (gridContainer.Text == "0")
                {
                    //cmb.Text = "";
                    cmb.SelectedIndex = -1;
                    cmb.Width = Unit.Percentage(100);
                }
                else
                {
                    if (gridContainer.Text == "&nbsp;")
                    {
                        //cmb.Text = "";
                        cmb.SelectedIndex = -1;
                        cmb.Width = Unit.Percentage(100);
                    }
                    else
                    {
                        string text = "";
                        text = gridContainer.Text;
                        
                        text = clsFunksione.zevendesoKaraktere(text);
                        cmb.Text = text;
                        cmb.Width = Unit.Percentage(100);
                    }
                }
            }
            else
            {
                if (gridContainer.Text == "&nbsp;")
                {
                    string text = "";
                    text = gridContainer.Text;
                    
                    text = clsFunksione.zevendesoKaraktere(text);
                    cmb.Text = text;
                    cmb.Width = Unit.Percentage(100);
                }
                else
                {
                    string text = "";
                    text = gridContainer.Text;
                    
                    text = clsFunksione.zevendesoKaraktere(text);
                    cmb.Text = text;
                    cmb.Width = Unit.Percentage(100);
                }
            }
            container.Controls.Add(cmb);
        }
    }

    class MyTemplateNrAutomatik : ITemplate //template i krijuar per KPF per kredi
    {
        public void InstantiateIn(Control container)
        {
            clsFunksione funksion = new clsFunksione();
            DbAdmin.clsDatabaseAdmin  db = new  clsDatabaseAdmin();
            DbAdmin.colNrAutom colNrAutomatik = new DbAdmin.colNrAutom();
            colNrAutomatik.mbushGjitheNumratAutomatike(funksion.ktheIdNdermarrje());
            //DbAdmin.colNrAutom colNrAutomatik = db.merrGjitheNumratAutomatike(funksion.ktheIdNdermarrje());
            ASPxComboBox cmb = new ASPxComboBox();
            cmb.ClientInstanceName = "NrAutomatik";
            cmb.ClientSideEvents.KeyPress = "function(s,e){var code =_getKeyCode(e.htmlEvent);KeyPresNrAutomatik(code,NrAutomatik,0); }";
            cmb.ClientSideEvents.TextChanged = "function(s,e){TextChangedNrAutomatik(0); }";
            cmb.ClientSideEvents.LostFocus = "function(s,e){LostFocusNrAutomatik(0);}";
            cmb.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedNrAutomatik(NrAutomatik, 0) }";
            cmb.DropDownButton.Visible = false;
            EditButton b1 = new EditButton();

            cmb.Buttons.Add(b1);
            cmb.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            cmb.DropDownStyle = DropDownStyle.DropDownList;
            cmb.TextFormatString = "{0}";
            GridViewDataItemTemplateContainer gridContainer = (GridViewDataItemTemplateContainer)container;
            cmb.ID = "cmbBox";
            ListBoxColumn colprove = new ListBoxColumn();
            colprove.FieldName = "KodiNrAutom";
            ListBoxColumn colemer = new ListBoxColumn();
            colemer.FieldName = "EmertimiNrAutom";
            cmb.Columns.Add(colprove);
            cmb.Columns.Add(colemer);
            cmb.ValueField = "IdNrAutom";
            cmb.DataSource = colNrAutomatik ;
            cmb.DataBind();
            if (gridContainer.Column.FieldName == "NrAutoSkemaKontabelKoka")
            {
                if (gridContainer.Text == "0")
                {
                    //cmb.Text = "";
                    cmb.SelectedIndex = -1;
                    cmb.Width = Unit.Percentage(100);
                }
                else
                {
                    if (gridContainer.Text == "&nbsp;")
                    {
                        //cmb.Text = "";
                        cmb.SelectedIndex = -1;
                        cmb.Width = Unit.Percentage(100);
                    }
                    else
                    {
                        string text = "";
                        text = gridContainer.Text;
                        text = clsFunksione.zevendesoKaraktere(text);
                        cmb.Text = text;
                        cmb.Width = Unit.Percentage(100);
                    }
                }
            }
            else
            {
                if (gridContainer.Text == "&nbsp;")
                {
                    string text = "";
                    text = gridContainer.Text;
                   text = clsFunksione.zevendesoKaraktere(text);
                    cmb.Text = text;
                    cmb.Width = Unit.Percentage(100);
                }
                else
                {
                    string text = "";
                    text = gridContainer.Text;
                    text = clsFunksione.zevendesoKaraktere(text);
                    cmb.Text = text;
                    cmb.Width = Unit.Percentage(100);
                }
            }
            container.Controls.Add(cmb);
        }
    }

    class MyTemplate : ITemplate
    {
        public void InstantiateIn(Control container)
        {
            //ASPxTextBox txt = new ASPxTextBox();
            ////ASPxButton b = new ASPxButton();
            ////PanelContent p1 = new PanelContent();

            ASPxComboBox txt = new ASPxComboBox();
            txt.DropDownButton.Visible = false;
            txt.Buttons.Add();
            txt.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            txt.DropDownStyle = DropDownStyle.DropDownList;
            txt.TextFormatString = "{0}";
            ////txt.EnableAnimation = false;
            ////txt.EnableTheming = false;
            ////txt.ShowShadow = false;
            ////txt.EnableViewState = false;
            ////txt.EncodeHtml = false;
            ////txt.EnableDefaultAppearance = false;
            ////txt.EnableClientSideAPI = false;
            ////txt.Native = true; // Use this if you need a simple selection. 
            //txt.EnableClientSideAPI = true;
            GridViewDataItemTemplateContainer gridContainer = (GridViewDataItemTemplateContainer)container;
            txt.ID = "txtBox";
            if (gridContainer.Column.FieldName == "KodiSkemaModel" || gridContainer.Column.FieldName == "LlogariSkemaKontabelTrupi")
            {
                if (gridContainer.Text == "0")
                {
                    txt.Text = "";
                    txt.Width = Unit.Percentage(100);
                    
                }
                else
                {
                    if (gridContainer.Text == "&nbsp;")
                    {
                        //text.Value = "";
                        txt.Text = "";
                        txt.Width = Unit.Percentage(100);
                    }
                    else
                    {
                        string text = "";
                        text = gridContainer.Text;
                        
                        text = clsFunksione.zevendesoKaraktere(text);
                        txt.Text = text;
                        txt.Width = Unit.Percentage(100);
                    }
                }
            }
            else
            {
                if (gridContainer.Text == "&nbsp;")
                {
                    string text = "";
                    text = gridContainer.Text;
                    
                    text = clsFunksione.zevendesoKaraktere(text);
                    txt.Text = text;
                    txt.Width = Unit.Percentage(100);
                    //text.Value = "";
                }
                else
                {
                    string text = "";
                    text = gridContainer.Text;
                    
                    text = clsFunksione.zevendesoKaraktere(text);
                    txt.Text = text;
                    txt.Width = Unit.Percentage(100);
                }
            }
            
            ////p1.Controls.Add(txt);
            ////p1.Controls.Add(b);
            ////p1.Width = Unit.Percentage(90);
            ////PanelCollection p = new PanelCollection(p1);
            container.Controls.Add(txt);
        }
    }

    class MyTemplateCombo : ITemplate
    {
        public void InstantiateIn(Control container)
        {
            ASPxComboBox cmb = new ASPxComboBox();
            GridViewDataItemTemplateContainer gridContainer = (GridViewDataItemTemplateContainer)container;
            cmb.ID = "cmbBox";
            cmb.Items.Add("Debi", true);
            cmb.Items.Add("Kredi", false);
            ////cmb.EnableAnimation = false;
            ////cmb.EnableTheming = false;
            ////cmb.ShowShadow = false;
            ////cmb.EnableViewState = false;
            ////cmb.EncodeHtml = false;
            ////cmb.EnableDefaultAppearance = false;
            ////cmb.EnableClientSideAPI = false;
            ////cmb.Native = true; // Use this if you need a simple selection. 
            if (!(gridContainer.Text == "&nbsp;"))
            {
                cmb.Text = gridContainer.Text;                
            }
            cmb.DropDownStyle = DropDownStyle.DropDownList;
            cmb.Width = Unit.Percentage(100);
            container.Controls.Add(cmb);
            ////if (temptxt != null)
            ////{
            ////    temptxt.Focus();
            ////}
            ////else if (tempcombo != null)
            ////{
            ////    tempcombo.Focus();
            ////}

        }
    }
}


