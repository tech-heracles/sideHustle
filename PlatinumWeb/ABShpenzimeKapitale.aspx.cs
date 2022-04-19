using DbCore;
using DbCore.DbAdmin;
using DbCore.DbAnalizeBuxheti;
using DevExpress.Utils;
using DevExpress.Web;
using PlatinumWeb.Templates;
using System;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using DevExpress.Web.Data;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class ABShpenzimeKapitale : MyPageBase
    {
        private int idNdermarrje;
        private int idKonfig;
        private int idPerdoruesi;
        private int idGjuha;
        private int idViti;
        private int idNdermVit;
        private const int idKomponente = 3003;
        private const string komponente = "ABShpenzimeKapitale.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                DbCore.clsFunksione.logout(Session,true,"FaqePaautorizuar");
            }
            idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerdoruesi);
            }

            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (!IsPostBack)
            {
                idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
                idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
                idNdermVit = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
                hfState.Set("idGjuha", idGjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idPerdoruesi", idPerdoruesi);
                hfState.Set("idViti", idViti);
                hfState.Set("komponente", komponente);
                hfState.Set("idNdermVit", idNdermVit);
                ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 123, "PSHK3VA", rm, ci, idGjuha);
                cmbKonfigurimi.SelectedIndex = 0;
                DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()));
                hfKonffillestar.Value = String.Format("{0};{1}", konf.KodKonfigAmbjente, konf.PershkrimKonfigAmbjente);

                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, komponente);
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DPlot.ToString();
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);

                idKonfig = int.Parse(cmbKonfigurimi.Value.ToString());

                mbushGridenNgaDB(idNdermarrje);
                konfiguroGride(cmbKonfigurimi.Text);
            }
            else
            {
                idNdermarrje = (int)hfState.Get("idNdermarrje");
                idPerdoruesi = (int)hfState.Get("idPerdoruesi");
                idGjuha = (int)hfState.Get("idGjuha");
                idViti = (int)hfState.Get("idViti");
                idNdermVit = (int)hfState.Get("idNdermVit");
                idKonfig = int.Parse(cmbKonfigurimi.Value.ToString());
                mbushGrideNgaSession();
                GridUtil.PercaktoNgjyrenPerKolonatReadOnly(gvShpenzimeKapitale, "ShKId", "RreshtiId", "Emertimi", "TotaliArdhme", "IdNdermarrje", "IdKrijuesi", "IdModifikuesi", "DtKrijimi", "DtModifikimi");
                krijoTotalSummary();
            }

            gvShpenzimeKapitale.PercaktoTitlePanel( this, MenuInfo, pnlMesazhi, hfState, idPerdoruesi, idNdermarrje, idViti, idGjuha, idKonfig, komponente,  rm, ci, DevExpress.Web.GridViewExportedRowType.All);
            percaktoTemplateMenu();
        }

        private void mbushGridenNgaDB(int idNdermarrje)
        {
            colShpenzimeKapitale col = new colShpenzimeKapitale(idNdermarrje, idNdermVit);
            gvShpenzimeKapitale.DataSource = col;
            gvShpenzimeKapitale.DataBind();

            mySessionObjects.ruajObjectNeSesion(Session, col, "shpenzimeKapitale");
        }

        /// <summary>
        /// merr ds e grides nga sessioni
        /// </summary>
        public void mbushGrideNgaSession()
        {
            object tmp = mySessionObjects.merrObjectNgaSesioni(Session, "shpenzimeKapitale");
            colShpenzimeKapitale col = (tmp as colShpenzimeKapitale) ?? new colShpenzimeKapitale(idNdermarrje, idNdermVit);
            gvShpenzimeKapitale.DataSource = tmp;
            gvShpenzimeKapitale.DataBind();
        }

        private void konfiguroGride(string kodi)
        {
            
            int vitiNdermarrjes= new clsNdermarrjeViti(idNdermVit).Viti;
            GridUtil.PercaktoVisibleColumnsAndTypesGridSipasKodKonfigurimi(idNdermarrje, "gvShpenzimeKapitale", gvShpenzimeKapitale, kodi, idKomponente.ToString(), idGjuha);
            GridUtil.PercaktoBandPerKolona(gvShpenzimeKapitale, "Projekti", "Emertimi");
            GridUtil.PercaktoBandPerKolona(gvShpenzimeKapitale, ( vitiNdermarrjes-1).ToString(), "TotalParardhes");
            GridUtil.PercaktoBandPerKolona(gvShpenzimeKapitale, (vitiNdermarrjes).ToString(), "PritshmiAktual");
            GridUtil.PercaktoBandPerKolona(gvShpenzimeKapitale, "Parashikimi i shpenzimeve per vitin " + (vitiNdermarrjes + 1).ToString(), "ShpenzKapitalePatrupezuarArdhme", "ShpenzKapitaleTrupezuarArdhme", "TransferimKapitalArdhme", "TotaliArdhme");
            GridUtil.PercaktoBandPerKolona(gvShpenzimeKapitale, (vitiNdermarrjes + 2).ToString(), "ParashikimTotaliPlus2");
            GridUtil.PercaktoBandPerKolona(gvShpenzimeKapitale, (vitiNdermarrjes + 3).ToString(), "ParashikimTotaliPlus3");
            GridUtil.konfiguroGridaPerBatchEditing(gvShpenzimeKapitale, false, false, false);
            GridUtil.PercaktoNgjyrenPerKolonatReadOnly(gvShpenzimeKapitale, "ShKId", "RreshtiId", "Emertimi", "TotaliArdhme", "IdNdermarrje", "IdKrijuesi", "IdModifikuesi", "DtKrijimi", "DtModifikimi");
            krijoTotalSummary();
        }

        protected void gvShpenzimeKapitale_DataBound(object sender, EventArgs e)
        {
            gvShpenzimeKapitale.KeyFieldName = "RreshtiId";
        }

        private void percaktoTemplateMenu()
        {
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdoruesi, idNdermarrje, ASPxMenu1, komponente, this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, false, true, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session), true);
        }

        public void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
        }

        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
        }

        protected void gvShpenzimeKapitale_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters.Contains("3003"))//per te kontrolluar nese eshte callback konfigurimi
            {
                konfiguroGride(e.Parameters.Split(';')[1]);
            }
        }

        protected void gvShpenzimeKapitale_BatchUpdate(object sender, DevExpress.Web.Data.ASPxDataBatchUpdateEventArgs e)
        {
            try
            {
                object tmp = mySessionObjects.merrObjectNgaSesioni(Session, "shpenzimeKapitale");
                clsMesazh mesazh = new clsMesazh(true);
                colShpenzimeKapitale col = (tmp as colShpenzimeKapitale) ?? new colShpenzimeKapitale(idNdermarrje, idNdermVit);
                clsTeDrejtaRoli teDrejta = new clsTeDrejtaRoli();
                teDrejta.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, komponente);

                if (!teDrejta.DMod)
                {
                    mesazh = new clsMesazh(false, "Nuk keni te drejta per kete veprim!");
                    gvShpenzimeKapitale.CancelEdit();
                }
                else
                {
                    foreach (ASPxDataUpdateValues updated in e.UpdateValues)
                    {
//string key = e.UpdateValues[i].Keys[0].ToString();

                        clsShpenzimeKapitale shpenzimVjeter = col.FirstOrDefault(x => x.RreshtiId == updated.MerrKeyValue<int>());

                        clsShpenzimeKapitale shpenzimRi = updated.MerrCustomUpdatedObject(shpenzimVjeter);
                        shpenzimRi.IdModifikuesi = idPerdoruesi;
                        mesazh = shpenzimRi.Modifiko();
                    }
                    mySessionObjects.ruajObjectNeSesion(Session, col, "shpenzimeKapitale");
                }
                if (mesazh.Status)
                {

                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Modifikimi u krye me sukses!:Green");
                }
                else
                {
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, mesazh.PershkrimMesazhi + ":Red");
                }
            }
            catch (Exception err)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, err.Message + "!:Red");
            }
            e.Handled = true;
        }

        //private void shtoKoloneTotali()
        //{
        //    if (gvShpenzimeKapitale.Columns["TotaliEArdhme"] == null)
        //    {
        //        GridViewDataTextColumn colTotal = new GridViewDataTextColumn();
        //        colTotal.Caption = "Totali";
        //        colTotal.FieldName = "TotaliEArdhme";
        //        colTotal.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
        //        colTotal.VisibleIndex = gvShpenzimeKapitale.Columns["TransferimKapitalArdhme"].VisibleIndex;
        //        colTotal.PropertiesTextEdit.DisplayFormatString = "n2";
        //        colTotal.HeaderStyle.Wrap = DefaultBoolean.True;
        //        colTotal.HeaderStyle.BackColor = Color.LightGray;

        //        colTotal.Width = gvShpenzimeKapitale.Columns["ParashikimTotaliPlus3"].Width;
        //        gvShpenzimeKapitale.Columns.Add(colTotal);
        //    }
        //}

        protected void gvShpenzimeKapitale_CustomUnboundColumnData(object sender,
    ASPxGridViewColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "TotaliEArdhme")
            {
                e.Value = Convert.ToDecimal(e.GetListSourceFieldValue("ShpenzKapitalePatrupezuarArdhme"))
                    + Convert.ToDecimal(e.GetListSourceFieldValue("ShpenzKapitaleTrupezuarArdhme"))
                    + Convert.ToDecimal(e.GetListSourceFieldValue("TransferimKapitalArdhme"))
                    ;
            }
        }

        private void krijoTotalSummary()
        {
            foreach (GridViewColumn col in gvShpenzimeKapitale.VisibleColumns)
            {
                if (col.Name == "Emertimi")
                {
                    col.FooterTemplate = new MyFooterCellTemplate(col.Name, "Totali ");
                    continue;
                }

                if (col.GetType() == typeof(GridViewDataSpinEditColumn))
                {
                    gvShpenzimeKapitale.ShtoTotalSummary("n2", DevExpress.Data.SummaryItemType.Sum, col.Name);
                    GridUtil.PercaktoTemplateTotalSummaryFooter(gvShpenzimeKapitale, "n2", col.Name);
                }
            }
        }

        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {
        }

        protected void gvShpenzimeKapitale_HtmlFooterCellPrepared(object sender, ASPxGridViewTableFooterCellEventArgs e)
        {
        }

        protected void gvShpenzimeKapitale_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
        }
    }
}