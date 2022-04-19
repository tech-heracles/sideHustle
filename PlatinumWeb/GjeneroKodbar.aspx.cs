using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using DbCore;
using DbCore.DbAdmin;
using DbCore.DbInventari;
using DevExpress.Web;
using PlatinumWeb.ApplicationUtils.Pages;
using PlatinumWeb.Templates;

namespace PlatinumWeb
{
    public partial class GjeneroKodbar : MyPageBase
    {


        /// <summary>
        /// perdoret per te vendosur theme
        /// </summary>
        /// <param name="source">derguesi</param>
        /// <param name="e">argumentat</param>

        /// <summary>
        /// mbush te dhenat kur faqja ben loadim
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void Page_Load(object sender, EventArgs e)
        {

            int idNdermarrje, idViti, idPerdoruesi;
            if (!IsPostBack)
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
            idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);            
            idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);

            
                hfState.Set("idViti", idViti);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idPerdoruesi", idPerdoruesi);
                ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
                EmrateButonave(cultinf, rm);
                vendosPerkthimet(cultinf, rm);
                mbushHiddenFieldMePerkthime(cultinf, rm);
                percaktoTemplateMenu(ASPxMenu1, idViti, idPerdoruesi, idNdermarrje);
                //konfiguroVleraFillestareShto(idNdermarrje);
                mbushComboGjatesia(cmbGjatesiKodbar);
                //   mbushPopUpListeNgaDB();
                // percaktoTemplate();
                DbCore.mySessionObjects.ruajGrideNeSession(string.Empty, Session, (object)new DataTable());
            }
            else
            {
                if (!IsCallback || (IsCallback && Request["__CALLBACKID"].ToString().Contains("ASPxMenu1"))) {
                    idViti = (int)hfState.Get("idViti");
                    idPerdoruesi=(int)hfState.Get("idPerdoruesi");
                    idNdermarrje = (int)hfState.Get("idNdermarrje");
                    percaktoTemplateMenu(ASPxMenu1, idViti, idPerdoruesi, idNdermarrje);
                }

            }
            mbushPopUpListeNgaDB();
            //shtoSasi();  
        }

        /// <summary>
        /// Vendos emrat e label ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="cultinf"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void vendosPerkthimet(CultureInfo cultinf, ResourceManager rm)
        {
            lblEmerSheet.Text = rm.GetString("labelEmriExcel", cultinf);
            lblSimboliNdares.Text = rm.GetString("labelSimboliNdares", cultinf);
            lblEmerSkedari.Text = rm.GetString("labelEmriISkedarit", cultinf);
            lblGjatesiKodbar.Text = rm.GetString("lblGjatesiKodbar", cultinf);
            lblTipi.Text = rm.GetString("labelFilterAvancuarTipi", cultinf);
        }

        /// <summary>
        /// Vendos emrat e hidden fiels ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="cultinf"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void mbushHiddenFieldMePerkthime(CultureInfo cultinf, ResourceManager rm)
        {
            hfState.Set("msgNukKaRreshtaPerEksport", rm.GetString("msgNukKaRreshtaPerEksport", cultinf));
            hfState.Set("msgShenoKarakterinNdares", rm.GetString("msgShenoKarakterinNdares", cultinf));
            hfState.Set("msgZgjidhEmerSkedar", rm.GetString("msgZgjidhEmerSkedar", cultinf));
            hfState.Set("msgSkaRreshtaNeGrid", rm.GetString("msgSkaRreshtaNeGrid", cultinf));
            hfState.Set("msgNukEshteSelektuarAsnjeRresht", rm.GetString("msgNukEshteSelektuarAsnjeRresht", cultinf));
            hfState.Set("msgNukKaRReshtGrida", rm.GetString("msgNukKaRReshtGrida", cultinf));
            hfState.Set("msgSasiaNumerike", rm.GetString("msgSasiaNumerike", cultinf));
            hfState.Set("msgSasiaMeEVogelSeNje", rm.GetString("msgSasiaMeEVogelSeNje", cultinf));
        }

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(ASPxMenu1, (int)hfState.Get("idViti"), (int)hfState.Get("idPerdoruesi"), (int)hfState.Get("idNdermarrje"));


        }


        /// <summary>
        /// Percakton veprimin qe kryhet kur klikohet nje nga butonat e menuse
        /// </summary>
        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {
            if (e.Item.Name == "Gjenerokodbar")
            {
                //Page.Validate();
                // gjeneroKodbar();
            }
            if (e.Item.Name == "Ngarko")
            {

                ngarkogride();

            }

        }
        /// <summary>
        /// metoda per gjenerimin e kodbareve
        /// </summary>
        private void gjeneroKodbar()
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);

            //gvExport.Selection.SelectAll();
            List<object> rreshta = gvExport.GetSelectedFieldValues(new string[] { "Kodbari", "IdArtikulli" });
            Object selectedItem = cmbGjatesiKodbar.SelectedItem;
            clsDatabaseInventari db = new clsDatabaseInventari();
            Random random = new Random();
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            StringBuilder nrGjeneruar = new StringBuilder();
            int ugjeneruan = 0;
            if (rreshta.Count == 0)
            {
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("msgNukEshteSelektuarAsnjeRresht", cultinf), pnlMesazhi);
                return;
            }
            else
            {
                foreach (object[] kodbar in rreshta)
                {
                    // per rreshtat qe nuk kane kodbare
                    if (kodbar[0].ToString() == "")
                    {
                        //clsArtikulli art = new clsArtikulli(Convert.ToInt32(kodbar[1]));
                        //int kodBareCount = colKodbare.kodBareCount(Convert.ToInt32(kodbar[1])); //todo senada
                        if (colKodbare.kodBareCount(Convert.ToInt32(kodbar[1])) == 0)
                        {
                            //gjenerohet kodbari i tille qe mos te ekzistoje per artikuj te tjere
                            while (db.ekzistonKodbar(nrGjeneruar.ToString(), idNdermarrje) || (nrGjeneruar.ToString() == ""))
                            {
                                //Random random = new Random();
                                nrGjeneruar = new StringBuilder();
                                for (int i = 0; i < Convert.ToInt32(selectedItem.ToString()); i++)
                                {
                                    nrGjeneruar.Append(random.Next(0, 9));
                                }

                            }

                            //  colKodbare colKod = new colKodbare();

                            clsKodbari kod = new clsKodbari();
                            kod.Pershkrimi = nrGjeneruar.ToString();
                            kod.IdArtikulli = Convert.ToInt32(kodbar[1].ToString());
                            clsMesazh mesazh = kod.ruaj(idNdermarrje);
                            if (mesazh.Status == true) ugjeneruan++;
                        }
                    }
                } 
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("msgUgjeneruan", cultinf) +" " +  ugjeneruan+ " "  + rm.GetString("msgKodBar", cultinf), pnlMesazhi);
                ngarkogride();

            }


        }
  
        /// <summary>
        /// ngarkon artikujt ne gride
        /// </summary>

        private void ngarkogride()
        {
            gvExport.Selection.UnselectAll();
            gvExport.Columns.Clear();
            gvExport.AutoGenerateColumns = true;
            gvExport.DataSource = null;
            //gvExport.SettingsPager.PageSize = 10;
            gvExport.DataBind();

            System.Data.DataTable table = new System.Data.DataTable();

            table = DbCore.DbInventari.colArtikujt.ktheArtikujNdermarrjesAndAutorizimeDTGjeneroKodbar(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), false);
            foreach (DataColumn d in table.Columns)
                gvExport.Columns.Add(new GridViewDataColumn(d.ColumnName));

            gvExport.DataSource = table;
            gvExport.SettingsPager.PageSize = 15;
            gvExport.DataBind();
            gvExport.Columns["IdArtikulli"].Visible = false;
            gvExport.Columns["idcmimartikulli"].Visible = false;
            percaktoTemplate();
            gvExport.KeyFieldName = "IdArtikulli;Niveli i cmimit;Kodbari;idcmimartikulli";
            GridUtil.konfigGrideListeEMadhePaTheme(gvExport, gvExport.KeyFieldName);
            status1.Value = "export";

            DbCore.mySessionObjects.ruajGrideNeSession(string.Empty, Session, (object)table);
            if (hfSasia.Value == "")
            {
                for (int i = 0; i <= gvExport.VisibleRowCount; i++)
                {
                    a.Add("1");
                }


                //  hfSasia.Value =(Object)a.ToString();
                JavaScriptSerializer serializusi = new JavaScriptSerializer();
                hfSasia.Value = serializusi.Serialize((Object)a).ToString();
            }

        }


        protected void gvExport_DataBound(object sender, EventArgs e)
        {
            if (this.gvExport.Columns["#"] == null)
            {

                DevExpress.Web.GridViewCommandColumn check = new DevExpress.Web.GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true;
                check.Width = Unit.Percentage(2);
                gvExport.Settings.ShowFilterRow = true;
                gvExport.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                gvExport.Settings.ShowFilterRowMenu = true;
                gvExport.Columns.Add(check);
                //  gvExport.SettingsBehavior.AllowSelectByRowClick = true;
                //  gvExport.SettingsBehavior.AllowFocusedRow = true;
            }



            if (this.gvExport.Columns["Sasia"] == null)
            {
                GridViewDataTextColumn sasia = new GridViewDataTextColumn();
                sasia.Caption = "Sasia";
                sasia.Width = Unit.Percentage(5);
                sasia.VisibleIndex = 17;
                gvExport.Columns.Add(sasia);
                gvExport.KeyFieldName = "IdArtikulli;Niveli i cmimit;Kodbari;idcmimartikulli";

                gvExport.SettingsBehavior.AllowFocusedRow = true;
            }
            // shtoSasi();
        }

        protected void gvExport_AfterPerformCallback(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            DataTable table = new DataTable();
            DbCore.mySessionObjects.merrGrideNgaSessioni(Session, out table);
            gvExport.DataSource = table;
            gvExport.DataBind();
            table.Dispose();
            percaktoTemplate();
        }
        protected void gvExport_CustomJSProperties(object sender, DevExpress.Web.ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpNoRows"] = gvExport.VisibleRowCount;
            e.Properties["cpNoPage"] = gvExport.PageIndex;
        }

        protected void gvExport_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters.Contains("Filter"))
            {
                DataTable table = new DataTable();
                DbCore.mySessionObjects.merrGrideNgaSessioni(Session, out table);
                gvExport.DataSource = table;
                gvExport.DataBind();
                clsFiltraExporti filter = new clsFiltraExporti(int.Parse(e.Parameters.Split(';')[1]));
                gvExport.FilterExpression = filter.Pershkrimi;
                return;
            }
            if (e.Parameters == "pastro")
            {
                System.Data.DataTable tb = new System.Data.DataTable();
                gvExport.Columns.Clear();
                gvExport.AutoGenerateColumns = true;
                gvExport.DataSource = null;
                gvExport.DataBind();
                gvExport.FilterExpression = "";
                DbCore.mySessionObjects.ruajGrideNeSession(string.Empty, Session, (object)tb);
                gvExport.DataSource = tb;
                gvExport.DataBind();
                return;
            }
            gvExport.KeyFieldName = "IdArtikulli;Niveli i cmimit;Kodbari;idcmimartikulli ";



        }


        /// <summary>
        /// Vendos emrat e butonave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateButonave(CultureInfo cultinf, ResourceManager rm)
        {

            ButtonCancel.Text = rm.GetString("btnAdministrimiCancel", cultinf);
            btnSelectAll.Text = rm.GetString("btnZgjidhTeGjitha", cultinf);
            btnUnselectAll.Text = rm.GetString("btnHiqZgjedhjenTeGjitha", cultinf);
            btnSelectAllOnPage.Text = rm.GetString("zgjidhTeGjithaBtn", cultinf);
            btnUnselectAllOnPage.Text = rm.GetString("btnHiqZgjedhjenNeFaqe", cultinf);
        }



        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        /// <param name="idViti"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNdermarrje"></param>
        private void percaktoTemplateMenu(ASPxMenu aSPxMenu1, int idViti, int idPerdorues, int idNdermarrje)
        {
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, "GjeneroKodbar.aspx", this, MenuInfo, true, true, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
        }

/// <summary>
/// mbush combon e gjatesise se kodbareve
/// </summary>
/// <param name="cmb"></param>
        private void mbushComboGjatesia(ASPxComboBox cmb)
        {
            for (int i = 5; i <= 20; i++)
            {
                cmb.Items.Add(i.ToString(), i);
            }
            cmb.SelectedIndex = 5;

        }

        private void mbushPopUpListeNgaDB()
        {//mbush griden e popupit me te dhena

            DataTable table = new DataTable();
            DbCore.mySessionObjects.merrGrideNgaSessioni(Session, out table);
            gvExport.DataSource = table;
            gvExport.DataBind();
            if (table != null && table.Rows.Count > 0)
                gvExport.KeyFieldName = "IdArtikulli;Niveli i cmimit;Kodbari;idcmimartikulli";

        }
        protected void ASPxGridViewExporter1_RenderBrick(object sender, DevExpress.Web.ASPxGridViewExportRenderingEventArgs e)
        {
            GridViewDataColumn col = e.Column as GridViewDataColumn;
            if (e.RowType == GridViewRowType.Data && (col.FieldName == "BRANCH_CODE" || col.FieldName == "ACCOUNT_BRANCH"))
                e.TextValue = e.Value.ToString(); //behet kjo per rastin kur nje numer si 001 te mos eksportohet ne excel si 1 por sic eshte ne grid 001
            if (e.RowType == GridViewRowType.Data && (col.FieldName == "INITIATION_DATE" || col.FieldName == "VALUE_DATE"))
                e.TextValue = Convert.ToDateTime(e.Value).ToString("dd-MMM-yyyy");
        }
        protected void btnExporto_Click(object sender, EventArgs e)
        {
            //gjeneroKodbar();
            exportoGride();
        }
        protected void gjeneroKodbar_Click(object sender, EventArgs e)
        {
            gjeneroKodbar();

        }
        /// <summary>
        /// metode per mbushjen e hfSasise kur selektohen te gjithe artikujt
        /// </summary>
        List<string> a = new List<string>();
        protected void btnSelectAll_Click(object sender, EventArgs e)
        {
            Page page = HttpContext.Current.CurrentHandler as Page;
            page.ClientScript.RegisterStartupScript(typeof(Page), "Test", "<script type='text/javascript'>gvExport.SelectRows();</script>");
            for (int i = 0; i <= gvExport.VisibleRowCount; i++)
            {
                a.Add("1");
            }
  
       
          //  hfSasia.Value =(Object)a.ToString();
            JavaScriptSerializer serializusi = new JavaScriptSerializer();
            hfSasia.Value = serializusi.Serialize((Object)a).ToString();
           // gvExport.Selection.SelectAll();
           
        }

        protected void btnSelectAllPage_Click(object sender, EventArgs e)
        {

            Int32 start = gvExport.VisibleStartIndex;

            Int32 end = gvExport.VisibleStartIndex + gvExport.SettingsPager.PageSize;
            for (int i = start; i < end; i++)
            {
                gvExport.Selection.SetSelection(i, true);
            }
           
            //Page page = HttpContext.Current.CurrentHandler as Page;
            //page.ClientScript.RegisterStartupScript(typeof(Page), "Test1", "<script type='text/javascript'>gvExport.SelectAllRowsOnPage();</script>");
        
        
        }

        protected void btnUnSelectAllPage_Click(object sender, EventArgs e) {

            Page page = HttpContext.Current.CurrentHandler as Page;
            page.ClientScript.RegisterStartupScript(typeof(Page), "Test", "<script type='text/javascript'>gvExport.UnselectAllRowsOnPage();</script>");
        }

        protected void btnUnSelectAll_Click(object sender, EventArgs e)
        {

            gvExport.Selection.UnselectAll();
        }

        /// <summary>
        /// behet eksporti i grides
        /// </summary>
        private void exportoGride()
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);

            List<object> rreshta =
                //foreach (DataRow dr in gvExport)
                //DataRow   rreshta = gvExport.GetDataRow(1);

                //gvExport.GetSelectedFieldValues(new string[] {"IdArtikulli" });

               gvExport.GetSelectedFieldValues(new string[] { "IdArtikulli", "Niveli i cmimit", "Monedha e nivelit te cmimit", "Date fillimi i cmimit", "Kodi", "Emer i artikullit", "Kodbari","Detajim 1","Detajim 2", "Njesia 1", "Cmimi 1", "Njesia 2", "Cmimi 2", "Klasa", "Lloji i artikullit", "Grupimi 1", "Grupimi 2", "Furnitori Kryesor", "idcmimartikulli" });

            //list to datatable
            string[] fieldNames = new string[] { "IdArtikulli", "Niveli i cmimit", "Monedha e nivelit te cmimit", "Date fillimi i cmimit", "Kodi", "Emer i artikullit", "Kodbari", "Detajim 1", "Detajim 2", "Njesia 1", "Cmimi 1", "Njesia 2", "Cmimi 2", "Klasa", "Lloji i artikullit", "Grupimi 1", "Grupimi 2", "Furnitori Kryesor", "idcmimartikulli" };
            List<object> selectedValues = gvExport.GetSelectedFieldValues(fieldNames);
            DataTable table = new DataTable();
            DataTable table1 = new DataTable();
            foreach (string field in fieldNames)
            {
                table.Columns.Add(field);
                table1.Columns.Add(field);
            }

            int k = 0;
            foreach (object[] item in selectedValues)
            {

                DataRow row = table.NewRow();
                for (int i = 0; i < item.Length; i++)
                {
                    row[i] = item[i];
                    //   if (i == item.Length - 1) k =Convert.ToInt32( item[i]);

                }
                //if ((item[6] == null) || (item[6].ToString() == ""))
                //{
                    // popKodbare.ShowOnPageLoad = true;
                    //  clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Disa nga artikujt nuk kane kodbare!", pnlMesazhi);
                    // break;
               // }
                //else
               // {
                    int index = gvExport.FindVisibleIndexByKeyValue(new object[] { item[0], item[1], item[6],item[18] });
                    //  GridViewDataTextColumn col0 = gvExport.Columns["Sasia"] as GridViewDataTextColumn;
                    JavaScriptSerializer serializusi = new JavaScriptSerializer();
                    object[] Sasia = (object[])serializusi.DeserializeObject(this.hfSasia.Value);
                    object[] Sasia1 = (object[])serializusi.DeserializeObject(this.hfSasia1.Value);
                    //for (int i = 0; i < Sasia.Length; i++)
                    //{
                    //    if (Sasia[i] != null)
                    //    {
                int sasia=0;
                sasia = Convert.ToInt32(Sasia[index].ToString());
                if (Sasia1!=null)
                if (index <= Sasia1.Length-1)  
                    if (Sasia1[index] != null) sasia = Convert.ToInt32(Sasia1[index].ToString());
                   // else sasia = Convert.ToInt32(Sasia[index].ToString()); ;

               
                    for (int j = 1; j <= sasia; j++)
                    {
                        DataRow dr3 = table.NewRow();
                        dr3.ItemArray = row.ItemArray;
                        // dataTable.Rows.Add(newRow);
                        table.Rows.Add(dr3);
                        //    }
                        //}
                    }

                    //DataRow dr= gvExport.GetDataRow(index);
                    //int sasia =Convert.ToInt32( dr["Sasia"].ToString());
                    //   for (int j=1;j<=sasia;j++)
                    // table.Rows.Add(row);
                    // DataRow dr3 = table.NewRow();
                    // dr3.ItemArray = row.ItemArray;
                    //// dataTable.Rows.Add(newRow);
                    // table.Rows.Add(dr3);  
                    //table1.ImportRow(row);
                    //   table.Rows.Add(row);
                    k++;
                }
            //}

            DataSet ds = new DataSet();
            ds.Tables.Add(table);
            ds.Tables.Add(table1);

            //
            //List<object> rreshta1= new  List<object>();
            //List<object> rreshta = gvExport.DataSource as List<object>;
            //DataRow   rreshta1=rreshta;
            //  int i=0;
            //foreach (object[] rr in rreshta)
            //{

            //if (rr[1].ToString()=="2")
            //{
            // int index=  gvExport.FindVisibleIndexByKeyValue(rr[0]);
            //    rreshta1.Add(rr);
            //string[] b = new string[14];
            //b[0] = "7";
            //b[1] = "6";
            //b[2] = "6";
            //b[3] = "7";
            //b[4] = "7";
            //b[5] = "7";
            //b[6] = "7";
            //b[7] = "7";
            //b[8] = "7";
            //b[9] = "7";
            //b[10] = "7";
            //b[11] = "7";
            //b[12] = "7";
            //b[13] = "7";
            ////b[14] = "7";

            //rreshta1.AddRange(b);
            //rreshta[i] = rr.;
            //i++;

            //rreshta1.Add(rr);
            // rreshta1.Add(rr);
            // }
            // }
            //    
            // gvExport.get
            //gvExport.GetDataRow(IndexedString);

            //foreach (GridViewDataColumn d in fieldNames)
            //    gvExport.Columns.Add(d);
            //DataSet ds =(DataSet) rreshta;
            //    (DataSet) gvExport.DataSource;
            gvExport.DataSource = table;
            gvExport.DataBind();
            gvExport.Columns["Sasia"].Visible = false;
            //ASPxGridViewExporter1.DataBind();
            switch (rbTipi.Value.ToString())
            {
                case "CSV":
                    {
                        string simbolndares = txtSimboliNdares.Text;
                        if (cbSimboliNdares.Checked)
                            simbolndares = "\t";
                        DevExpress.XtraPrinting.CsvExportOptions o = new DevExpress.XtraPrinting.CsvExportOptions();
                        o.Separator = simbolndares;
                        o.TextExportMode = DevExpress.XtraPrinting.TextExportMode.Text;

                        try
                        {
                            //  DbCore.clsFunksione.eksportoGridenKodbare(txtEmerSheet.Text, txtEmerSkedari.Text, ASPxGridViewExporter1, Session, Response, gvExport, table);
                            ASPxGridViewExporter1.WriteCsvToResponse(txtEmerSkedari.Text, o);
                        }
                        catch (Exception err)
                        {
                            NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNdodhiNjeGabimGjateKrijimitTeDok", cultinf), pnlMesazhi);
                        }
                    }
                    break;
                case "XLSX":
                    {
                        DevExpress.XtraPrinting.XlsxExportOptions o = new DevExpress.XtraPrinting.XlsxExportOptions();
                        if (txtEmerSheet.Text != "")
                            o.SheetName = txtEmerSheet.Text;
                        o.TextExportMode = DevExpress.XtraPrinting.TextExportMode.Text;

                        try
                        {
                            // DbCore.clsFunksione.eksportoGridenKodbare(txtEmerSheet.Text, txtEmerSkedari.Text, ASPxGridViewExporter1, Session, Response, gvExport, table);
                            ASPxGridViewExporter1.WriteXlsxToResponse(txtEmerSkedari.Text, o);
                        }
                        catch (Exception err)
                        {
                            NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNdodhiNjeGabimGjateKrijimitTeDok", cultinf), pnlMesazhi);
                        }

                    }
                    break;
                case "XLS":
                    {
                        DevExpress.XtraPrinting.XlsExportOptions o = new DevExpress.XtraPrinting.XlsExportOptions();
                        if (txtEmerSheet.Text != "")
                            o.SheetName = txtEmerSheet.Text;
                        o.TextExportMode = DevExpress.XtraPrinting.TextExportMode.Text;

                        try
                        {
                            // DbCore.clsFunksione.eksportoGridenKodbare(txtEmerSheet.Text, txtEmerSkedari.Text, ASPxGridViewExporter1, Session, Response, gvExport, table);
                            ASPxGridViewExporter1.WriteXlsToResponse(txtEmerSkedari.Text, o);

                        }
                        catch (Exception err)
                        {                       
                            NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNdodhiNjeGabimGjateKrijimitTeDok", cultinf), pnlMesazhi);
                        }

                    }
                    break;
                default:
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukBehetEksportMeKeteTip", cultinf), pnlMesazhi);
                    break;
            }
            gvExport.Columns["Sasia"].Visible = true;
            //  ngarkogride();
        }


        //String[] a = new String[] { }; int i = 0;


        protected void gvExport_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {

            if (e.RowType == DevExpress.Web.GridViewRowType.Data)
            {
                GridViewDataTextColumn col0 = ((ASPxGridView)sender).Columns["Sasia"] as GridViewDataTextColumn;
                ASPxTextBox txt1 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col0, "txtBox") as ASPxTextBox;


                if (txt1 != null)
                {
                    txt1.ClientInstanceName = "Sasia" + e.VisibleIndex.ToString();


                    txt1.ClientSideEvents.TextChanged = "function(s,e){TextChangedSasi(Sasia" + e.VisibleIndex.ToString() + ", " + e.VisibleIndex.ToString() + ");}";
                    txt1.ClientSideEvents.Init = "function(s,e){Utils.initTxtNumber(s,e); }";
                    txt1.ClientSideEvents.GotFocus = "function(s,e){Utils.gotFocusTxtNumer(s,e);}";
                    txt1.ClientSideEvents.LostFocus = "function(s,e){Utils.lostFocusTxtNumer(s,e)}";
               

                }
                //a[i] = "1";
                //i++;
                //hfSasia.Value = a.ToString();
                if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 2)
                {

                }
            }
        }


        private void percaktoTemplate()
        {//percaktohen templatet per fushat e grides
            GridViewDataTextColumn col2 = gvExport.Columns["Sasia"] as GridViewDataTextColumn;
            col2.DataItemTemplate = new PlatinumWeb.Templates.MyDoubleTemplate(true, 0, "1"); // "0.00");



        }



        protected void gvExport_SelectedIndexChanged(object sender, EventArgs e) {

            //Page page = HttpContext.Current.CurrentHandler as Page;
            //page.ClientScript.RegisterStartupScript(typeof(Page), "Test", "function (s,e){merrTeDhena('');}");
        }


    }
}