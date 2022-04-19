using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DbCore.IMBUtils.Messages;
using DevExpress.Web;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class LupaKursiShpejte : MyPageBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            int idPerdoruesi;
            int idGjuha;
            int idNdermarrje;
            int idViti;
            int idNdermarrjeVit;
            int idMonedha;
            int formatKursi;
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
                if (!String.IsNullOrEmpty(Request.QueryString["idMonedha"]))
                    idMonedha = int.Parse(Request.QueryString["idMonedha"]);
                else
                {
                    string kodMon = Request.QueryString["kodMonedha"];
                    DbCore.DbAdmin.clsMonedha mon = new DbCore.DbAdmin.clsMonedha();
                    mon.mbushMonedhen(kodMon, idNdermarrje);
                    idMonedha = mon.IdMonedha;
                }
                formatKursi =  DbCore.clsFunksione.MerrVleraFormatKursi(idMonedha);
                
                hfState.Set("idPerdoruesi", idPerdoruesi);
                hfState.Set("idGjuha", idGjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idViti", idViti);
                hfState.Set("idNdermarrjeVit", idNdermarrjeVit);
                hfState.Set("idMonedha", idMonedha);
                hfState.Set("formatKursi", formatKursi);
                hfState.Set("Meme", DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
            }
            else
            {
                idPerdoruesi = (int)hfState["idPerdoruesi"];
                idGjuha = (int)hfState["idGjuha"];
                idNdermarrje = (int)hfState["idNdermarrje"];
                idViti = (int)hfState["idViti"];
                idNdermarrjeVit = (int)hfState["idNdermarrjeVit"];
                idMonedha = (int)hfState["idMonedha"];
                formatKursi = (int)hfState["formatKursi"];
            }
            if (!IsPostBack)
            {
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, ":");
                if (Request.QueryString["ruaj"] == "ok")
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["mesazhRuajtjeMeSukses"], pnlMesazhi);
                percaktoTemplateMenu(idViti, idPerdoruesi, idNdermarrje, ASPxMenu1);
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, "LupaKursiShpejte.aspx");
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                mbushGrideKursesh(idNdermarrje, idMonedha);
                konfiguroGride(idNdermarrje, idGjuha);
                gvKurset.FilterExpression = "[LlojKursi] = '" + Request.QueryString["llojKursi"] + "'";
            }
            else
            {
                if (!IsCallback || (IsCallback && Request["__CALLBACKID"].ToString().Contains("ASPxMenu1")))
                {
                    percaktoTemplateMenu(idViti, idPerdoruesi, idNdermarrje, ASPxMenu1);
                    clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, "gvKurset", 1, "LupaKursiShpejte.aspx");
                }
                if (!IsCallback || (IsCallback && Request["__CALLBACKID"].ToString().Contains("gvKurset")))
                {
                    mbushGrideKurseshNgaSesioni(idNdermarrje, idMonedha);
                    konfiguroGride(idNdermarrje, idGjuha);
                }
            }
            DbCore.mySessionObjects.ruajMesazhNeSesion(Session, ":");
        }

        private void mbushGrideKursesh(int idNdermarrje, int idMonedha)
        {
            //int llojKurs = int.Parse(Request.QueryString["llojKursi"]);
            DataTable dt = DbCore.DbAdmin.colKurset.ktheKursetMonedhes(idMonedha);
            DbCore.mySessionObjects.ruajGrideNeSessionLupa(Session, dt);
            gvKurset.DataSource = dt;
            gvKurset.DataBind();
        }

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu((int)hfState["idViti"], (int)hfState["idPerdoruesi"], (int)hfState["idNdermarrje"], ASPxMenu1);
        }

        private void mbushGrideKurseshNgaSesioni(int idNdermarrje, int idMonedha)
        {
            Object tmpObject;
            DbCore.mySessionObjects.merrGrideNgaSessioniLupa(Session, out tmpObject);
            if (tmpObject == null)
                mbushGrideKursesh(idNdermarrje, idMonedha);
            else
            {
                gvKurset.DataSource = tmpObject;
                gvKurset.DataBind();
            }
        }

        private void konfiguroGride(int idNdermarrje, int idGjuhe)
        {
            shtoLlojeKursi();
            GridUtil.percaktoVisibleColumnsMeWidth(idGjuhe, idNdermarrje, gvKurset, "gvKurset", "LupaKursiShpejte.aspx");
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            GridUtil.konfiguroGrideListeEvogelPaTheme(gvKurset, "IdKursi");
            GridViewDataTextColumn col = gvKurset.Columns["VleraKursi"] as GridViewDataTextColumn;
            int formatKursi = DbCore.clsFunksione.MerrVleraFormatKursi((int)hfState["idMonedha"]);
            col.PropertiesEdit.DisplayFormatString = DbCore.clsFunksione.krijoNumer(formatKursi, "0");
        }

        private void shtoLlojeKursi()
        {
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            if (typeof(GridViewDataComboBoxColumn) != gvKurset.Columns["LlojKursi"].GetType())
            {
                gvKurset.Columns.Remove(gvKurset.Columns["LlojKursi"]);
                gvKurset.Columns.Add(colnew);
                colnew.PropertiesComboBox.Items.Add("", null);
                colnew.PropertiesComboBox.Items.Add("Kursi1", 1);
                colnew.PropertiesComboBox.Items.Add("Kursi2", 2);
                colnew.PropertiesComboBox.Items.Add("Kursi3", 3);
                colnew.PropertiesComboBox.Items.Add("Kursi4", 4);
                colnew.PropertiesComboBox.Items.Add("Kursi5", 5);
                colnew.PropertiesComboBox.Items.Add("Kursi6", 6);
                colnew.PropertiesComboBox.Items.Add("Kursi7", 7);
                colnew.PropertiesComboBox.Items.Add("Kursi8", 8);
                colnew.PropertiesComboBox.Items.Add("Kursi9", 9);
                colnew.PropertiesComboBox.Items.Add("Kursi10", 10);
                colnew.PropertiesComboBox.Items.Add("Kursi11", 11);
                colnew.PropertiesComboBox.Items.Add("Kursi12", 12);
                colnew.PropertiesComboBox.Items.Add("Kursi13", 13);
                colnew.PropertiesComboBox.Items.Add("Kursi14", 14);
                colnew.PropertiesComboBox.Items.Add("Kursi15", 15);
                colnew.PropertiesComboBox.Items.Add("Kursi16", 16);
                colnew.PropertiesComboBox.Items.Add("Kursi17", 17);
                colnew.PropertiesComboBox.Items.Add("Kursi18", 18);
                colnew.PropertiesComboBox.Items.Add("Kursi19", 19);
                colnew.PropertiesComboBox.Items.Add("Kursi20", 20);
                colnew.FieldName = "LlojKursi";
                colnew.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                DbCore.mySessionObjects.ruajDsComboGrideNeSession(Session, colnew.PropertiesComboBox.Items, "LlojKursi");
            }
            else
            {
                colnew = (GridViewDataComboBoxColumn)gvKurset.Columns["LlojKursi"];
                if (colnew.PropertiesComboBox.Items.Count == 0)
                {
                    colnew.PropertiesComboBox.Items.AddRange((ListEditItemCollection)DbCore.mySessionObjects.merrDsComboGrideNeSession(Session, "LlojKursi"));
                }
            }
        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        /// <param name="idViti"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNdermarrje"></param>
        private void percaktoTemplateMenu(int idViti, int idPerdorues, int idNdermarrje, ASPxMenu aSPxMenu1)
        {
            clsToolbarConfig.percaktoTemplateMenu((int)hfState["idGjuha"], idViti, idPerdorues, idNdermarrje, aSPxMenu1, "LupaKursiShpejte.aspx", this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, hfRuaj.Value == "Ruaj" ? true : false, true, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            aSPxMenu1.Items.FindByName("Anullo").Text = rm.GetString("MenuItemMbyll", ci);
        }

        /// <summary>
        /// sherben per te ruajtur filtrin e zgjedhur
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
            int idNderm = (int)hfState["idNdermarrje"];
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida();
            filtri.FiltraKodi = cmbFiltra.Text;
            filtri.FiltraShenime = cmbFiltra.Text;
            filtri.FiltraUniversal = false;

            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka((int)hfState["idGjuha"], "gvKurset", "LupaKursiShpejte.aspx", idNderm);
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvKurset.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("IdKursi", gvKurset);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvKurset.GetSortedColumns();
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
            //    filtri.KoloneRenditje = "IdKursi";
            //    filtri.DrejtimRenditje = true;
            //}
            filtri.IdPerdoruesi = (int)hfState["idPerdoruesi"];
            filtri.IdNdermarje = idNderm;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();

            filtri.IdStatusDok = 1;
            mesazh = filtri.ruaj();

            clsToolbarConfig.mbushComboBoxFiltraMeValFieldTextField((int)hfState["idGjuhe"], idNderm, "gvKurset", "LupaKursiShpejte.aspx", "IdFiltra", "FiltraShenime", 1);

            percaktoTemplateMenu((int)hfState["idViti"], (int)hfState["idPerdoruesi"], idNderm, ASPxMenu1);
            if (mesazh.Status)
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, mesazh.PershkrimMesazhi + ":Green");
            else DbCore.mySessionObjects.ruajMesazhNeSesion(Session, mesazh.PershkrimMesazhi + ":Red");

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
            int idNderm = (int)hfState["idNdermarrje"];
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;

            DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka((int)hfState["idGjuha"], "gvKurset", "LupaKursiShpejte.aspx", idNderm);
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNderm, koka.IdGridaKoka);

            if (filtra.FiltraKodi != null)
            {
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                filtra.IdPerdoruesi = (int)hfState["idPerdoruesi"];
                mesazh = filtra.fshi();
                clsToolbarConfig.mbushComboBoxFiltra((int)hfState["idGjuha"], idNderm, "gvKurset", 1, "LupaKursiShpejte.aspx");
                percaktoTemplateMenu((int)hfState["idViti"], (int)hfState["idPerdoruesi"], idNderm, ASPxMenu1);
                if (mesazh.Status == true)
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, mesazh.PershkrimMesazhi + ":Green");//clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else DbCore.mySessionObjects.ruajMesazhNeSesion(Session, mesazh.PershkrimMesazhi + ":Red");//clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                gvKurset.FilterExpression = String.Empty;
            }
        }

        protected void gvKurset_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string[] arr = e.Parameters.Split(';');
            int idNdermarrje = (int)hfState["idNdermarrje"];
            int idGjuha = (int)hfState["idGjuha"];
            if (arr.Length == 3)
            {
                if (arr[2] == "")
                    gvKurset.FilterExpression = "";
                else
                {

                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuha, "gvKurset", "LupaKursiShpejte.aspx", idNdermarrje);
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], idNdermarrje, koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvKurset.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvKurset);
                    }
                }
            }
            konfiguroGride(idNdermarrje, idGjuha);
        }

        protected void gvKurset_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            e.Values.Clear();
            e.AddValue("(Te gjithe)", string.Empty, "true");
        }

        protected void gvKurset_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {//validimi ne jane plotesuar gjithe fushat e detyruara
            foreach (GridViewColumn column in gvKurset.Columns)
            {
                if (column.Visible == true)
                {
                    GridViewDataColumn dataColumn = column as GridViewDataColumn;
                    if (dataColumn == null)
                        continue;

                    if (e.NewValues[dataColumn.FieldName] == null || e.NewValues[dataColumn.FieldName].ToString() == "")    //validimi per kolonat e detyrueshme
                    {
                        e.Errors[dataColumn] = "Vlera nuk mund te jete bosh.";
                        return;
                    }
                }
            }
        }

        protected void gvKurset_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {//kur fillon editimi te behet validimi
            if (hfRuaj.Value == "Ruaj")
                if (!gvKurset.IsNewRowEditing)
                {
                    gvKurset.DoRowValidation();
                }
        }

        protected void gvKurset_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {//merr te dhenat e rreshtit te ri te grides
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente((int)hfState["idPerdoruesi"], (int)hfState["idNdermarrje"], (int)hfState["idViti"], "LupaKursiShpejte.aspx");
            if (!tedrejtaInfo.DShtim)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ju nuk keni te drejta per kete veprim!:Red");
                return;
            }
            int lloji = int.Parse(Convert.ToString(e.NewValues["LlojKursi"]));
            string pershkrimiLloji = "Kursi" + lloji.ToString();
            DateTime data = Convert.ToDateTime(e.NewValues["DataKursit"]);
            int njesiaKursit = int.Parse(Convert.ToString(e.NewValues["NjesiaKursit"]));
            double vleraKursit = double.Parse(Convert.ToString(e.NewValues["VleraKursi"]));
            int idMonedha = (int)hfState["idMonedha"];
            e.Cancel = true;
            gvKurset.CancelEdit();
            DbCore.DbAdmin.clsKurset kursi = new DbCore.DbAdmin.clsKurset(-1, lloji, data, vleraKursit, idMonedha, njesiaKursit, pershkrimiLloji);
            DbCore.clsMesazh mesazh = kursi.ruajKurs();
            if (!mesazh.Status == true)
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ndodhi nje gabim gjate ruajtjes!:Red");
            else
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, MessagesResource.Messages["msgRuajtjeMeSuksesGreen"]);
            mbushGrideKursesh((int)hfState["idNdermarrje"], (int)hfState["idMonedha"]);
        }

        protected void gvKurset_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (e.Editor.GetType().Name == "ASPxTextBox")
            {
                ASPxTextBox currentEditor = e.Editor as ASPxTextBox;
                if (e.Column == gvKurset.Columns["NjesiaKursit"])
                {
                    //e.Column.ReadOnly = true;
                    currentEditor.Text = "1";
                    currentEditor.ClientEnabled = false;
                }
            }
            else if (e.Editor.GetType().Name == "ASPxComboBox")
            {
                ASPxComboBox currentEditor = e.Editor as ASPxComboBox;
                if (e.Column == gvKurset.Columns["LlojKursi"])
                {
                    currentEditor.SelectedIndex = int.Parse(Request.QueryString["llojKursi"]);
                }
            }
            else if (e.Editor.GetType().Name == "ASPxDateEdit")
            {
                ASPxDateEdit currentEditor = e.Editor as ASPxDateEdit;
                if (e.Column == gvKurset.Columns["DataKursit"])
                {
                    currentEditor.Date = DateTime.Today;
                }
            }
        }
    }
}