using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{

    public partial class KategoriArkive : MyPageBase
    {

        DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();
        private string guidString;
        private string komponente = "KategoriArkive.aspx";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                Response.Redirect(DbCore.IMBUtils.Paths.defaultLoginPath);
                return;
            }
            int idPerd = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerd);
                return;
            }
            oPerdorues = DbCore.mySessionObjects.kthePerdorues(Session);
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvKategoriArkive", 1, komponente);
            if (!IsPostBack)
            {
                guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                hfState.Set("guidString", guidString);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, ":");
                if (Request.QueryString["ruaj"] == "ok")
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["mesazhRuajtjeMeSukses"], pnlMesazhi);
                konfiguroVleraFillestare(idNdermarrje);
                konfiguroGride(idNdermarrje);
                GridUtil.percaktoVisibleColumnsMeWidth(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, gvKategoriArkive, "gvKategoriArkive", komponente);

                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), komponente);
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
            }
            guidString = (string)hfState["guidString"];
            DbCore.mySessionObjects.ruajMesazhNeSesion(Session, ":");
            konfiguroVleraFillestare(idNdermarrje);
            konfiguroGride(idNdermarrje);
        }

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
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
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, komponente, this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, hfRuaj.Value == "Ruaj" ? true : false, true, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
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
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvKategoriArkive", komponente, idNdermarrje);
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvKategoriArkive.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("IdNivel", gvKategoriArkive);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvKategoriArkive.GetSortedColumns();
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
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvKategoriArkive", 1, komponente);
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);

            if (mesazh.Status == true)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
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
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvKategoriArkive", komponente, idNdermarrje);
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                mesazh = filtra.fshi();
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvKategoriArkive", 1, komponente);
                percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);

                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";                
                gvKategoriArkive.FilterExpression = String.Empty;
            }
        }

        private void konfiguroVleraFillestare(int idNdermarrje)
        {
            DbCore.DbShare.colKategoriArkive col = new DbCore.DbShare.colKategoriArkive(idNdermarrje);
            gvKategoriArkive.DataSource = col;
            gvKategoriArkive.DataBind();
        }

        private void konfiguroGride(int idNdermarrje)
        {


            KonfigurimComboGride.shtoNivelDok(gvKategoriArkive, Session, komponente, guidString, "IdNivel");
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            GridUtil.konfiguroGrideListeEvogelPaTheme(gvKategoriArkive, "IdKategoriArkive");
        }

     
     
        protected void gvKategoriArkive_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            e.Values.Clear();
            e.AddValue("(Te gjithe)", string.Empty, "true");
        }

        protected void gvKategoriArkive_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
        }

        protected void gvKategoriArkive_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), komponente);
            if (!tedrejtaInfo.DShtim)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ju nuk keni te drejta per kete veprim!:Red");
                return;
            }
            
            int idNivel = int.Parse(e.NewValues["IdNivel"].ToString());
            string kategoria = e.NewValues["Kategoria"].ToString();
            int idPerdoruesi = oPerdorues.IdPerdorues;
            int idNdermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            e.Cancel = true;
            gvKategoriArkive.CancelEdit();
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            if (DbCore.DbShare.clsKategoriArkive.ekzistonKategoriArkiveSipasNivelDheNdermarrje(idNdermarje, idNivel, kategoria))
            {
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ekziston nje kategori arkive me kete kod per nivelin perkates!Ju lutem zgjidhni nje kod tjeter!:Red");
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ekziston nje kategori arkive me kete kod per nivelin perkates!Ju lutem zgjidhni nje kod tjeter!", pnlMesazhi);
                konfiguroGride(idNdermarje);
                gvKategoriArkive.AddNewRow();
                return;
            }

            mesazh = DbCore.DbShare.clsKategoriArkive.ruaj(idNivel, kategoria, idNdermarje, idPerdoruesi);
            if (!mesazh.Status == true)
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ndodhi nje gabim gjate ruajtjes!:Red");
            else
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, MessagesResource.Messages["msgRuajtjeMeSuksesGreen"]);

            konfiguroVleraFillestare(idNdermarje);
        }

        protected void gvKategoriArkive_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {//validimi ne jane plotesuar gjithe fushat e detyruara
            DbCore.DbShare.clsKategoriArkive njesi = new DbCore.DbShare.clsKategoriArkive();
            foreach (GridViewColumn column in gvKategoriArkive.Columns)
            {
                if (column.Visible == true)
                {
                    GridViewDataColumn dataColumn = column as GridViewDataColumn;
                    if (dataColumn == null) continue;

                    if (e.NewValues[dataColumn.FieldName] == null)//validimi per kolonat e detyrueshme
                    {
                        e.Errors[dataColumn] = "Vlera nuk mund te jete bosh.";
                    }
                }
            }            
            if (e.Errors.Count > 0)
            {
                e.RowError = "Ju lutemi, plotesoni te gjitha fushat.";
            }
            if (string.IsNullOrEmpty(e.RowError) && e.Errors.Count > 0)
            {
                e.RowError = "Ju lutemi, korrigjoni te gjithe gabimet.";
            }
        }

        protected void gvKategoriArkive_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {//kur fillon editimi te behet validimi
            if (hfRuaj.Value == "Ruaj")
                if (!gvKategoriArkive.IsNewRowEditing)
                {
                    gvKategoriArkive.DoRowValidation();
                }
        }

        protected void gvKategoriArkive_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            String id = e.Keys["IdKategoriArkive"].ToString();
            e.Cancel = true;
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(oPerdorues.IdPerdoruesi, idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), komponente);
            if (!tedrejtaInfo.DMod)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ju nuk keni te drejta per kete veprim!:Red");
                return;
            }
            int idKategori = int.Parse(id);
            string kategoria = e.NewValues["Kategoria"].ToString();
            int idNivel = int.Parse(e.NewValues["IdNivel"].ToString());

            if (DbCore.DbShare.clsKategoriArkive.ekzistonKategoriArkiveSipasNivelDheNdermarrjePervecVetes(idNdermarrje, idNivel, kategoria, idKategori))
            {
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ekziston nje kategori arkive me kete kod per nivelin perkates! Ju lutem zgjidhni nje kod tjeter!:Red");
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ekziston nje kategori arkive me kete kod per nivelin perkates! Ju lutem zgjidhni nje kod tjeter!", pnlMesazhi);
                konfiguroGride(idNdermarrje);
                gvKategoriArkive.AddNewRow();
                return;
            }
            DbCore.clsMesazh m = DbCore.DbShare.clsKategoriArkive.modifiko(idKategori, kategoria, oPerdorues.IdPerdoruesi);
            if (m.Status)
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, m.PershkrimMesazhi + ":Green");
            else
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, m.PershkrimMesazhi + ":Red");

            gvKategoriArkive.CancelEdit();
            konfiguroVleraFillestare(idNdermarrje);
        }

        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            int a = gvKategoriArkive.FocusedRowIndex;
            gvKategoriArkive.Selection.SelectRow(a);
            List<object> rreshtat = gvKategoriArkive.GetSelectedFieldValues("IdKategoriArkive");
            foreach (int id in rreshtat)
            {
                //TODO: PATI te behet kontrolli a eshte e lidhur te tabela T_ARKIVA
                //if (DbCore.DbShare.clsKategoriArkive.kaVeprimeMeKategoriArkive(id))
                //    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ka veprime me kete kategori arkive!", pnlMesazhi);
                //else
                //{
                    DbCore.clsMesazh mesazh = DbCore.DbShare.clsKategoriArkive.fshi(id, DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
                    if (mesazh.Status)
                        clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["msgFshirjeMeSukses"], pnlMesazhi);
                    else
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ndodhi nje gabim gjate fshirjes!", pnlMesazhi);
                //}
                konfiguroVleraFillestare(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            }
            pnlGrida.Update();
        }

        protected void gvKategoriArkive_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
        }

        protected void gvKategoriArkive_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
        }

        protected void gvKategoriArkive_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
        }

        protected void gvKategoriArkive_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                if (arr[2] == "")
                    gvKategoriArkive.FilterExpression = "";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvKategoriArkive", komponente, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvKategoriArkive.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvKategoriArkive);
                    }
                }
            }
            konfiguroGride(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        }

        protected void gvKategoriArkive_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvKategoriArkive.PageIndex;
            e.Properties["cpPageRow"] = gvKategoriArkive.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvKategoriArkive.VisibleRowCount;
        }

    }
}