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
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    public partial class ABBuxhetPermbledhes : MyPageBase
    {
        private int idNdermarrje;
        private int idKonfig;
        private int idPerdoruesi;
        private int idGjuha;
        private int idViti;
        private int idNdermVit;
        private const int idKomponente= 3000;
        private const string komponente = "ABBuxhetPermbledhes.aspx";

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

                ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 121, "PBVP", rm, ci, idGjuha);
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

                mbushGridenNgaDB(idNdermarrje, idNdermVit);
                GridUtil.PercaktoVisibleColumnsAndTypesGridSipasKodKonfigurimi(idNdermarrje, "gvbuxhetPermbledhes", gvbuxhetPermbledhes, cmbKonfigurimi.Text, "3000", idGjuha);
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
                konfiguroGride(cmbKonfigurimi.Text);
                GridUtil.PercaktoNgjyrenPerKolonatReadOnly(gvbuxhetPermbledhes, "PBuxhetId", "RreshtiId", "Emertimi", "Totali", "IdNdermarrje", "IdKrijuesi", "IdModifikuesi", "DtKrijimi", "DtModifikimi");
                krijoTotalSummary();
            }

            gvbuxhetPermbledhes.PercaktoTitlePanel(this, MenuInfo, pnlMesazhi, hfState, idPerdoruesi, idNdermarrje, idViti, idGjuha, idKonfig, komponente, rm, ci, DevExpress.Web.GridViewExportedRowType.All);
            percaktoTemplateMenu();
        }

        /// <summary>
        /// mbush datasourcein e grides direkt nga db
        /// </summary>
        /// <param name="idNdermarrje"></param>
        private void mbushGridenNgaDB(int idNdermarrje,int idNdermVit)
        {
            colPBuxhetPermbledhes col = new colPBuxhetPermbledhes(idNdermarrje, idNdermVit);
            gvbuxhetPermbledhes.DataSource = col;
            gvbuxhetPermbledhes.DataBind();

            mySessionObjects.ruajObjectNeSesion(Session, col, "pBuxhetPermbledhes");
        }

        /// <summary>
        /// merr ds e grides nga sessioni
        /// </summary>
        public void mbushGrideNgaSession()
        {
            object tmp = mySessionObjects.merrObjectNgaSesioni(Session, "pBuxhetPermbledhes");
            colPBuxhetPermbledhes col = (tmp as colPBuxhetPermbledhes) ?? new colPBuxhetPermbledhes(idNdermarrje, idNdermVit);
            gvbuxhetPermbledhes.DataSource = tmp;
            gvbuxhetPermbledhes.DataBind();
        }

        private void konfiguroGride(string kodi)
        {
            GridUtil.konfiguroGridaPerBatchEditing(gvbuxhetPermbledhes, false, false, false);
            GridUtil.PercaktoNgjyrenPerKolonatReadOnly(gvbuxhetPermbledhes,  "PBuxhetId", "RreshtiId", "Emertimi", "Totali", "IdNdermarrje", "IdKrijuesi", "IdModifikuesi", "DtKrijimi", "DtModifikimi");
            krijoTotalSummary();
        }

        protected void gvbuxhetPermbledhes_DataBound(object sender, EventArgs e)
        {
            gvbuxhetPermbledhes.KeyFieldName = "RreshtiId";
            
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

        protected void gvbuxhetPermbledhes_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters.Contains("3000"))//per te kontrolluar nese eshte callback konfigurimi
            {
                konfiguroGride(e.Parameters.Split(';')[1]);
            }
        }

        protected void gvbuxhetPermbledhes_BatchUpdate(object sender, DevExpress.Web.Data.ASPxDataBatchUpdateEventArgs e)
        {
            clsMesazh mesazhi = null;
            try
            {
                clsTeDrejtaRoli teDrejta = new clsTeDrejtaRoli();
                teDrejta.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, komponente);
                object tmp = mySessionObjects.merrObjectNgaSesioni(Session, "pBuxhetPermbledhes");
                if (teDrejta.DMod)
                {
                    colPBuxhetPermbledhes col = (tmp as colPBuxhetPermbledhes) ?? new colPBuxhetPermbledhes(idNdermarrje, idNdermVit);

                    foreach (ASPxDataUpdateValues updated in e.UpdateValues)
                    {
                        clsPBuxhetPermbledhes oldbuxhet = col.FirstOrDefault(x => x.RreshtiId == updated.MerrKeyValue<int>());

                        clsPBuxhetPermbledhes newBuxhet = updated.MerrCustomUpdatedObject(oldbuxhet);
                        newBuxhet.IdModifikuesi = idPerdoruesi;
                        mesazhi = newBuxhet.Modifiko();
                    }
                    mySessionObjects.ruajObjectNeSesion(Session, col, "pBuxhetPermbledhes");
                }
                else
                    mesazhi = new clsMesazh(false, MessagesResource.Messages["msgNukKeniTeDrejta"]);
                if (mesazhi.Status)
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, mesazhi.PershkrimMesazhi + ":Green");
                else
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, mesazhi.PershkrimMesazhi + ":Red");
            }
            catch (Exception err)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ndodhi nje gabim gjate modifikimit!" + err.Message + ":Red");
            }
            e.Handled = true;
        }

        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {
        }

        private void krijoTotalSummary()
        {
            
            foreach (GridViewColumn col in gvbuxhetPermbledhes.VisibleColumns)
            {
                if (col.Name == "Emertimi")
                {
                    col.FooterTemplate = new MyFooterCellTemplate(col.Name, "Totali ");
                    continue;
                }

                if (col.GetType() == typeof(GridViewDataSpinEditColumn))
                {
                    gvbuxhetPermbledhes.ShtoTotalSummary("n2", DevExpress.Data.SummaryItemType.Sum, col.Name);
                    GridUtil.PercaktoTemplateTotalSummaryFooter(gvbuxhetPermbledhes, "n2", col.Name);
                }
            }
        }

        

        protected void gvbuxhetPermbledhes_CustomUnboundColumnData(object sender,
    ASPxGridViewColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "Totali")
            {
                e.Value = Convert.ToDecimal(e.GetListSourceFieldValue("Pagat"))
                    + Convert.ToDecimal(e.GetListSourceFieldValue("FondiVecante"))
                    + Convert.ToDecimal(e.GetListSourceFieldValue("SigurimeShoqerore"))
                    + Convert.ToDecimal(e.GetListSourceFieldValue("MallRadheSherbime"))
                    + Convert.ToDecimal(e.GetListSourceFieldValue("Subvencione"))
                    + Convert.ToDecimal(e.GetListSourceFieldValue("TransferimKorrBrendshme"))
                    + Convert.ToDecimal(e.GetListSourceFieldValue("TransferimKorrHuaja"))
                    + Convert.ToDecimal(e.GetListSourceFieldValue("ShpenzimeKapitalePaTrup"))
                    + Convert.ToDecimal(e.GetListSourceFieldValue("ShpenzimeKapitaleTrup"))
                    + Convert.ToDecimal(e.GetListSourceFieldValue("TransfertaKapitale"))
                    ;
            }
        }

        protected void gvbuxhetPermbledhes_HtmlFooterCellPrepared(object sender, ASPxGridViewTableFooterCellEventArgs e)
        {
        }

        protected void gvbuxhetPermbledhes_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
        }
    }
}