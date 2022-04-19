using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using DbCore.IMBUtils.Messages;
using DevExpress.Web;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class LupaModelAutomjeti : MyPageBase
    {

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
                    //FormsAuthentication.RedirectToLoginPage();
                    DbCore.clsFunksione.logout(Session, false, "", false);

            if (!IsPostBack)
            {
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, ":");
                if (Request.QueryString["ruaj"] == "ok")
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["mesazhRuajtjeMeSukses"], pnlMesazhi);
                percaktoTemplateMenu(idViti, idPerdoruesi, idNdermarrje, ASPxMenu1);
                clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, "gvModelAutomjeti", 1, "LupaModelAutomjeti.aspx");
                mbushGrideModelesh(idNdermarrje);
                GridUtil.percaktoVisibleColumnsMeWidth(idGjuha, idNdermarrje, gvModelAutomjeti, "gvModelAutomjeti", "LupaModelAutomjeti.aspx");
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, "LupaModelAutomjeti.aspx");
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
            }
            else
            {
                if (!IsCallback || (IsCallback && Request["__CALLBACKID"].ToString().Contains("ASPxMenu1")))
                {
                    percaktoTemplateMenu(idViti, idPerdoruesi, idNdermarrje, ASPxMenu1);
                    clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, "gvModelAutomjeti", 1, "LupaModelAutomjeti.aspx");
                }
                if (!IsCallback || (IsCallback && Request["__CALLBACKID"].ToString().Contains("gvModelAutomjeti")))
                {
                    mbushGrideModeleshNgaSesioni(idNdermarrje);
                }
            }
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            GridUtil.konfiguroGrideListeEvogelPaTheme(gvModelAutomjeti, "IdModeli");
            DbCore.mySessionObjects.ruajMesazhNeSesion(Session, ":");
        }

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            int idNderm = (int)hfState["idNdermarrje"];
            int idPerd = (int)hfState["idPerdoruesi"];
            int idViti = (int)hfState["idViti"];
            percaktoTemplateMenu(idViti, idPerd, idNderm, ASPxMenu1);
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
            int idGjuha = (int)hfState["idGjuha"];
            bool meme = (bool)hfState["Meme"];
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, "LupaModelAutomjeti.aspx", this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, hfRuaj.Value == "Ruaj" ? true : false, true, false, meme);
        }

        private void mbushGrideModelesh(int idNdermarrje)
        {
            //DataTable dt = DbCore.DbInventari.colFormulat.merrFormulatSipasNdermarrjes(idNdermarrje);
            DataTable dt = DbCore.DbInventari.colModeleAutomjetesh.merrModeleAutomjeteshSipasNdermarrjes(idNdermarrje);
            DbCore.mySessionObjects.ruajGrideNeSessionLupa(Session, dt);
            gvModelAutomjeti.DataSource = dt;
            gvModelAutomjeti.DataBind();
            dt.Dispose();
        }

        private void mbushGrideModeleshNgaSesioni(int idNdermarrje)
        {
            Object tmpObject;
            DbCore.mySessionObjects.merrGrideNgaSessioniLupa(Session, out tmpObject);
            if (tmpObject == null)
                mbushGrideModelesh(idNdermarrje);
            else
            {
                gvModelAutomjeti.DataSource = tmpObject;
                gvModelAutomjeti.DataBind();
            }
        }

        /// <summary>
        /// sherben per te ruajtur filtrin e zgjedhur
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
            int idNderm = (int)hfState["idNdermarrje"];
            int idGjuha = (int)hfState["idGjuha"];
            int idPerd = (int)hfState["idPerdoruesi"];
            int idViti = (int)hfState["idViti"];
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida();
            filtri.FiltraKodi = cmbFiltra.Text;
            filtri.FiltraShenime = cmbFiltra.Text;
            filtri.FiltraUniversal = false;

            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuha, "gvModelAutomjeti", "LupaModelAutomjeti.aspx", idNderm);
            filtri.GridaKokaId = koka.IdGridaKoka;

            filtri.FiltraVlera = gvModelAutomjeti.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("IdFormula", gvModelAutomjeti);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvModelAutomjeti.GetSortedColumns();
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
            //    filtri.KoloneRenditje = "IdFormula";
            //    filtri.DrejtimRenditje = true;
            //}
            filtri.IdPerdoruesi = (int)hfState["idPerdoruesi"];
            filtri.IdNdermarje = idNderm;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();

            filtri.IdStatusDok = 1;
            mesazh = filtri.ruaj();
            clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNderm, "gvModelAutomjeti", 1, "LupaModelAutomjeti.aspx");
            percaktoTemplateMenu(idViti, idPerd, idNderm, ASPxMenu1);
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
            int idGjuha = (int)hfState["idGjuha"];
            int idPerd = (int)hfState["idPerdoruesi"];
            int idViti = (int)hfState["idViti"];
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;

            DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuha, "gvModelAutomjeti", "LupaModelAutomjeti.aspx", idNderm);
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNderm, koka.IdGridaKoka);

            if (filtra.FiltraKodi != null)
            {
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                filtra.IdPerdoruesi = idPerd;
                mesazh = filtra.fshi();
                clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNderm, "gvModelAutomjeti", 1, "LupaModelAutomjeti.aspx");
                percaktoTemplateMenu(idViti, idPerd, idNderm, ASPxMenu1);
                if (mesazh.Status == true)
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, mesazh.PershkrimMesazhi + ":Green");
                else DbCore.mySessionObjects.ruajMesazhNeSesion(Session, mesazh.PershkrimMesazhi + ":Red");
                cmbFiltra.Text = "";
                gvModelAutomjeti.FilterExpression = String.Empty;
            }
        }


        protected void gvModelAutomjeti_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            e.Values.Clear();
            e.AddValue("(Te gjithe)", string.Empty, "true");
        }

        protected void gvModelAutomjeti_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string[] arr = e.Parameters.Split(';');
            int idNdermarrje = (int)hfState["idNdermarrje"];
            int idGjuha = (int)hfState["idGjuha"];
            if (arr.Length == 3)
            {
                if (arr[2] == "")
                    gvModelAutomjeti.FilterExpression = "";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuha, "gvModelAutomjeti", "LupaModelAutomjeti.aspx", idNdermarrje);
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], idNdermarrje, koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvModelAutomjeti.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvModelAutomjeti);
                    }
                }
            }
        }

        protected void gvModelAutomjeti_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {//funksioni qe theret javascriptin per kalimin e te dhenave nga grida tek textboxet e tabeve te tjera
            if (e.Editor.GetType().Name == "ASPxTextBox")
            {
                ASPxTextBox currentEditor = e.Editor as ASPxTextBox;
                //currentEditor.ClientSideEvents.TextChanged = "function(s,e){ProcessTextChanged('" + e.Column.FieldName + "',s.GetText());}";
                currentEditor.ClientInstanceName = e.Column.FieldName;
                if (e.Column == gvModelAutomjeti.Columns["KodModeli"])
                {
                    if (hfRuaj.Value == "Modifiko")
                        e.Column.ReadOnly = true;
                }
            }
        }

        protected void gvModelAutomjeti_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {//kur fillon editimi te behet validimi
            if (hfRuaj.Value == "Ruaj")
                if (!gvModelAutomjeti.IsNewRowEditing)
                {
                    gvModelAutomjeti.DoRowValidation();
                }
        }

        protected void gvModelAutomjeti_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {//validimi ne jane plotesuar gjithe fushat e detyruara
            foreach (GridViewColumn column in gvModelAutomjeti.Columns)
            {
                if (column.Visible == true)
                {
                    GridViewDataColumn dataColumn = column as GridViewDataColumn;
                    if (dataColumn == null) continue;

                    if (e.NewValues[dataColumn.FieldName] == null || e.NewValues[dataColumn.FieldName].ToString() == "")//validimi per kolonat e detyrueshme
                    {
                        e.Errors[dataColumn] = "Vlera nuk mund te jete bosh.";
                        return;
                    }
                }
            }
            int idNdermarrje = (int)hfState["idNdermarrje"];            
            if (e.Keys.Count == 0) //shtim
            {
                if (DbCore.DbInventari.clsModelAutomjeti.ekzistonModelAutomjeti(idNdermarrje, e.NewValues["KodModeli"].ToString()))
                {
                    e.RowError = "Ekziston nje model automjeti me këtë kod!";
                    return;
                }
            }                     
        }


        protected void gvModelAutomjeti_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {//merr te dhenat e rreshtit te ri te grides
            int idNderm = (int)hfState["idNdermarrje"];
            int idPerd = (int)hfState["idPerdoruesi"];
            int idViti = (int)hfState["idViti"];
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerd, idNderm, idViti, "LupaModelAutomjeti.aspx");
            if (!tedrejtaInfo.DShtim)
            {                
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ju nuk keni te drejta per kete veprim!:Red");
                return;
            }
            string kodi = e.NewValues["KodModeli"].ToString();
            string pershkrimi = e.NewValues["PershkrimModeli"].ToString();
            int idModel = 0;
            e.Cancel = true;
            gvModelAutomjeti.CancelEdit();
            DbCore.DbInventari.clsModelAutomjeti modeliRi = new DbCore.DbInventari.clsModelAutomjeti(idModel, kodi, pershkrimi, 1, idNderm, idPerd, idPerd);
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            mesazh = modeliRi.ruaj();
            if (!mesazh.Status)
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ndodhi nje gabim. Ruajtja nuk u krye!:Red");
            else
            {
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, MessagesResource.Messages["msgRuajtjeMeSuksesGreen"]);
                rifreskoModelAutomjetesh(idNderm);
            }
            mbushGrideModelesh(idNderm);
        }

        protected void gvModelAutomjeti_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            int idNderm = (int)hfState["idNdermarrje"];
            int idPerd = (int)hfState["idPerdoruesi"];
            int idViti = (int)hfState["idViti"];
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerd, idNderm, idViti, "LupaModelAutomjeti.aspx");
            if (!tedrejtaInfo.DMod)
            {
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ju nuk keni te drejta per kete veprim!:Red");
                return;
            }
            int idModel = int.Parse(e.Keys["IdModeli"].ToString());
            DbCore.DbInventari.clsModelAutomjeti modeli = new DbCore.DbInventari.clsModelAutomjeti(idModel);
            modeli.PershkrimModelAutomjeti = e.NewValues["PershkrimModeli"].ToString();
            modeli.IdPerdorues = idPerd;            
            e.Cancel = true;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            mesazh = modeli.modifiko();
            if (!mesazh.Status)
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ndodhi nje gabim. Ruajtja nuk u krye!:Red");
            else
            {
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, MessagesResource.Messages["msgRuajtjeMeSuksesGreen"]);
                rifreskoModelAutomjetesh(idNderm);
            }
            gvModelAutomjeti.CancelEdit();
            mbushGrideModelesh(idNderm);
        }
        private void rifreskoModelAutomjetesh(int idNdermarrje)
        {
            DbCore.DbInventari.colModeleAutomjetesh modelet = new DbCore.DbInventari.colModeleAutomjetesh();
            modelet.Add(new DbCore.DbInventari.clsModelAutomjeti(0, "", "", 0, 0, 0, 0));
            modelet.mbushModeleAutomjeteshSipasNdermarrjes(idNdermarrje);
            DbCore.mySessionObjects.ruajDsComboGrideNeSession(Session, modelet, "colModelet");
        }
        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            int a = gvModelAutomjeti.FocusedRowIndex;
            gvModelAutomjeti.Selection.SelectRow(a);
            List<object> rreshtat = gvModelAutomjeti.GetSelectedFieldValues("IdModeli");
            int idPerdorues = (int)hfState["idPerdoruesi"];
            foreach (object id in rreshtat)
            {
                if(DbCore.DbInventari.clsModelAutomjeti.eshteLidhurModelAutomjeti(int.Parse(id.ToString()))){
                    //mySessionObjects.ruajMesazhNeSesion(Session, "Ky model eshte i lidhur!:Red");
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ky model eshte i lidhur!", pnlMesazhi);
                    continue;
                }
                DbCore.DbInventari.clsModelAutomjeti modeli = new DbCore.DbInventari.clsModelAutomjeti(int.Parse(id.ToString()));
                DbCore.clsMesazh mesazh = modeli.fshi(idPerdorues);
                if (mesazh.Status)
                {
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                    rifreskoModelAutomjetesh((int)hfState["idNdermarrje"]);
                }
                else
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            }
            mbushGrideModelesh((int)hfState["idNdermarrje"]);

            pnlKryesor.Update();
        }
    }
}