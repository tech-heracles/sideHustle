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
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    public partial class ABPBuxheti3Vjecar : MyPageBase
    {
        private int idNdermarrje;
        private int idKonfig;
        private int idPerdoruesi;
        private int idGjuha;
        private int idViti;
        private int idNdermVit;
        private const int idKomponente = 3004;
        private const string komponente = "ABPBuxheti3Vjecar.aspx";

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
                hfState.Set("idNdermVit", idNdermVit);
                hfState.Set("komponente", komponente);
                ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 124, "PB3VA", rm, ci, idGjuha);
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
                GridUtil.PercaktoNgjyrenPerKolonatReadOnly(gvPBuxheti3Vjecar, "PB3VId", "RreshtiId", "Emertimi", "IdNdermarrje", "IdKrijuesi", "IdModifikuesi", "DtKrijimi", "DtModifikimi");
                krijoTotalSummary();
            }

            gvPBuxheti3Vjecar.PercaktoTitlePanel( this, MenuInfo, pnlMesazhi, hfState, idPerdoruesi, idNdermarrje, idViti, idGjuha, idKonfig, komponente,  rm, ci,  DevExpress.Web.GridViewExportedRowType.All);
            percaktoTemplateMenu();
        }

        private void mbushGridenNgaDB(int idNdermarrje)
        {
            colPBuxheti3Vjecar col = new colPBuxheti3Vjecar(idNdermarrje, idNdermVit);
            gvPBuxheti3Vjecar.DataSource = col;
            gvPBuxheti3Vjecar.DataBind();
            mySessionObjects.ruajObjectNeSesion(Session, col, "pBuxheti3Vjecar");
        }

        /// <summary>
        /// merr ds e grides nga sessioni
        /// </summary>
        public void mbushGrideNgaSession()
        {
            object tmp = mySessionObjects.merrObjectNgaSesioni(Session, "pBuxheti3Vjecar");
            colPBuxheti3Vjecar col = (tmp as colPBuxheti3Vjecar) ?? new colPBuxheti3Vjecar(idNdermarrje, idNdermVit);
            gvPBuxheti3Vjecar.DataSource = tmp;
            gvPBuxheti3Vjecar.DataBind();
        }

        private void konfiguroGride(string kodi)
        {
            int vit = new clsNdermarrjeViti(idNdermVit).Viti;
            GridUtil.PercaktoVisibleColumnsAndTypesGridSipasKodKonfigurimi(idNdermarrje, "gvPBuxheti3Vjecar", gvPBuxheti3Vjecar, kodi, idKomponente.ToString(), idGjuha);

            GridUtil.PercaktoBandPerKolona(gvPBuxheti3Vjecar, "Fakti i vitit " + (vit - 1), "BuxhetiParaardhes", "TeArdhuratParaardhes");
            GridUtil.PercaktoBandPerKolona(gvPBuxheti3Vjecar, "I pritshmi vitit " + vit, "BuxhetiAktual", "TeArdhuratAktual");
            GridUtil.PercaktoBandPerKolona(gvPBuxheti3Vjecar, "Parashikimi vitin " + (vit + 1), "BuxhetiPlus1", "TeArdhuratPlus1");
            GridUtil.PercaktoBandPerKolona(gvPBuxheti3Vjecar, "Parashikimi vitin " + (vit + 2), "BuxhetiPlus2", "TeArdhuratPlus2");
            GridUtil.PercaktoBandPerKolona(gvPBuxheti3Vjecar, "Parashikimi vitin " + (vit + 3), "BuxhetiPlus3", "TeArdhuratPlus3");
            GridUtil.konfiguroGridaPerBatchEditing(gvPBuxheti3Vjecar, false, false, false);
            GridUtil.PercaktoNgjyrenPerKolonatReadOnly(gvPBuxheti3Vjecar, "PB3VId", "RreshtiId", "Emertimi", "IdNdermarrje", "IdKrijuesi", "IdModifikuesi", "DtKrijimi", "DtModifikimi");
            krijoTotalSummary();
        }

        protected void gvPBuxheti3Vjecar_DataBound(object sender, EventArgs e)
        {
            gvPBuxheti3Vjecar.KeyFieldName = "RreshtiId";
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

        protected void gvPBuxheti3Vjecar_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters.Contains("idKomponente"))//per te kontrolluar nese eshte callback konfigurimi
            {
                konfiguroGride(e.Parameters.Split(';')[1]);
            }
        }

        protected void gvPBuxheti3Vjecar_BatchUpdate(object sender, DevExpress.Web.Data.ASPxDataBatchUpdateEventArgs e)
        {
            try
            {
                clsTeDrejtaRoli teDrejta = new clsTeDrejtaRoli();
                teDrejta.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, komponente);

                object tmp = mySessionObjects.merrObjectNgaSesioni(Session, "pBuxheti3Vjecar");
                clsMesazh mesazhi = new clsMesazh();
                colPBuxheti3Vjecar col = (tmp as colPBuxheti3Vjecar) ?? new colPBuxheti3Vjecar(idNdermarrje, idNdermVit);

                if (!teDrejta.DMod)
                {
                    mesazhi = new clsMesazh(false, MessagesResource.Messages["msgNukKeniTeDrejta"]);
                    gvPBuxheti3Vjecar.CancelEdit();
                }
                else
                {
                    for (int i = 0, count = e.UpdateValues.Count; i < count; i++)
                    {
                        //string key = e.UpdateValues[i].Keys[0].ToString();
                        clsPBuxheti3Vjecar buxhetiVjeter = col.FirstOrDefault(x => x.RreshtiId == e.UpdateValues[i].MerrKeyValue<int>());

                        clsPBuxheti3Vjecar buxhetiRi = e.UpdateValues[i].MerrCustomUpdatedObject(buxhetiVjeter);
                        buxhetiRi.IdModifikuesi = idPerdoruesi;
                        mesazhi = buxhetiRi.Modifiko();
                    }                 
                    mySessionObjects.ruajObjectNeSesion(Session, col, "pBuxheti3Vjecar");
                }

                if (mesazhi.Status)
                {

                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Modifikimi u krye me sukses!:Green");
                }
                else
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, mesazhi.Status + ":Red");
            }
            catch (Exception err)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, err.Message + "!:Red");
            }
            e.Handled = true;
        }

        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {
        }

        private void krijoTotalSummary()
        {
            foreach (GridViewColumn col in gvPBuxheti3Vjecar.VisibleColumns)
            {
                if (col.Name == "Emertimi")
                {
                    col.FooterTemplate = new MyFooterCellTemplate(col.Name, "Totali ");
                    continue;
                }

                if (col.GetType() == typeof(GridViewDataSpinEditColumn))
                {
                    gvPBuxheti3Vjecar.ShtoTotalSummary("n2", DevExpress.Data.SummaryItemType.Sum, col.Name);
                    GridUtil.PercaktoTemplateTotalSummaryFooter(gvPBuxheti3Vjecar, "n2", col.Name);
                }
            }
        }

        protected void gvPBuxheti3Vjecar_HtmlFooterCellPrepared(object sender, ASPxGridViewTableFooterCellEventArgs e)
        {
        }

        protected void gvPBuxheti3Vjecar_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
        }
    }
}