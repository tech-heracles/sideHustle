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
    public partial class ABParashikimShpenzPersoneli : MyPageBase
    {
        private int idNdermarrje;
        private int idKonfig;
        private int idPerdoruesi;
        private int idGjuha;
        private int idViti;
        private int idNdermVit;
        private const string komponente = "ABParashikimShpenzPersoneli.aspx";
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

            idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            idNdermVit = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (!IsPostBack)
            {
                hfState.Set("idGjuha", idGjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idPerdoruesi", idPerdoruesi);
                hfState.Set("komponente", komponente);
                hfState.Set("idNdermVit", idNdermVit);
                ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 128, "PSHP", rm, ci, idGjuha);
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
                idNdermVit = (int)hfState.Get("idNdermVit");
                idKonfig = int.Parse(cmbKonfigurimi.Value.ToString());
                mbushGrideNgaSession();
                krijoGrupSummary();
                krijoTotalSummary();
            }

            gvParashikimShpenzPersoneli.PercaktoTitlePanel( this, MenuInfo, pnlMesazhi, hfState, idPerdoruesi, idNdermarrje, idViti, idGjuha, idKonfig, komponente, rm, ci,DevExpress.Web.GridViewExportedRowType.All);
            percaktoTemplateMenu();
        }

        private void mbushGridenNgaDB(int idNdermarrje)
        {
            colParashikimShpenzPersoneli col = new colParashikimShpenzPersoneli(idNdermarrje, idNdermVit);
            gvParashikimShpenzPersoneli.DataSource = col;
            gvParashikimShpenzPersoneli.DataBind();

            mySessionObjects.ruajObjectNeSesion(Session, col, "parashikimiIShpenzimeve");
        }

        /// <summary>
        /// merr ds e grides nga sessioni
        /// </summary>
        public void mbushGrideNgaSession()
        {
            object tmp = mySessionObjects.merrObjectNgaSesioni(Session, "parashikimiIShpenzimeve");
            colParashikimShpenzPersoneli col = (tmp as colParashikimShpenzPersoneli) ?? new colParashikimShpenzPersoneli(idNdermarrje, idNdermVit);
            gvParashikimShpenzPersoneli.DataSource = tmp;
            gvParashikimShpenzPersoneli.DataBind();
        }

        private void konfiguroGride(string kodi)
        {
            GridUtil.PercaktoVisibleColumnsAndTypesGridSipasKodKonfigurimi(idNdermarrje, "gvParashikimShpenzPersoneli", gvParashikimShpenzPersoneli, kodi, "3008", idGjuha);
            GridUtil.konfiguroGridaPerBatchEditing(gvParashikimShpenzPersoneli, false, false, false);
            gvParashikimShpenzPersoneli.GroupBy(gvParashikimShpenzPersoneli.Columns["Prindi"]);
            krijoGrupSummary();
            krijoTotalSummary();
        }

        private void krijoGrupSummary()
        {
            foreach (GridViewColumn col in gvParashikimShpenzPersoneli.VisibleColumns)
            {
                
                if (col.GetType() == typeof(GridViewDataSpinEditColumn))
                {
                    gvParashikimShpenzPersoneli.PercaktoTemplateGroupSummaryFooter("n2", col.Name);
                    gvParashikimShpenzPersoneli.ShtoGroupSummary("n2", DevExpress.Data.SummaryItemType.Sum, col.Name);
                }
            }
        }
        private void krijoTotalSummary()
        {
            foreach (GridViewColumn col in gvParashikimShpenzPersoneli.VisibleColumns)
            {
                if (col.Name == "Funksioni")
                {
                    col.FooterTemplate = new MyFooterCellTemplate(col.Name, "Totali ");
                    continue;
                }
                
                if (col.GetType() == typeof(GridViewDataSpinEditColumn))
                {
                    gvParashikimShpenzPersoneli.ShtoTotalSummary("n2", DevExpress.Data.SummaryItemType.Sum, col.Name);
                    GridUtil.PercaktoTemplateTotalSummaryFooter(gvParashikimShpenzPersoneli, "n2", col.Name);
                }
            }
        }
        protected void gvParashikimShpenzPersoneli_DataBound(object sender, EventArgs e)
        {
            gvParashikimShpenzPersoneli.KeyFieldName = "FunksioniId";
                GridUtil.PercaktoNgjyrenPerKolonatReadOnly(gvParashikimShpenzPersoneli, "PShPId", "RreshtiId", "FunksioniId", "Funksioni", "IdNdermarrje", "IdKrijuesi", "IdModifikuesi", "DtKrijimi", "DtModifikimi", "Prindi");


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

        protected void gvParashikimShpenzPersoneli_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters.Contains("3008"))//per te kontrolluar nese eshte callback konfigurimi
            {
                konfiguroGride(e.Parameters.Split(';')[1]);
            }
        }

        protected void gvParashikimShpenzPersoneli_BatchUpdate(object sender, DevExpress.Web.Data.ASPxDataBatchUpdateEventArgs e)
        {
            try
            {
                object tmp = mySessionObjects.merrObjectNgaSesioni(Session, "parashikimiIShpenzimeve");
                clsMesazh mesazh = new clsMesazh(true);
                clsTeDrejtaRoli teDrejta = new clsTeDrejtaRoli();
                teDrejta.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, komponente);
                if (!teDrejta.DMod)
                {
                    mesazh = new clsMesazh(false, MessagesResource.Messages["msgNukKeniTeDrejta"]);
                }
                else
                {
                    colParashikimShpenzPersoneli col = (tmp as colParashikimShpenzPersoneli) ?? new colParashikimShpenzPersoneli(idNdermarrje, idNdermVit);

                    for (int i = 0; i < e.UpdateValues.Count; i++)
                    {

                        clsParashikimShpenzPersoneli parashikimVjeter = col.Where(x => x.FunksioniId == e.UpdateValues[i].MerrKeyValue<int>()).FirstOrDefault();

                        clsParashikimShpenzPersoneli parashikimRi = e.UpdateValues[i].MerrCustomUpdatedObject<clsParashikimShpenzPersoneli>(parashikimVjeter);
                        parashikimRi.IdModifikuesi = idPerdoruesi;
                        mesazh = parashikimRi.Modifiko();
                    }
                    mySessionObjects.ruajObjectNeSesion(Session, col, "parashikimiIShpenzimeve");
                }
                if (mesazh.Status)
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Modifikimi u krye me sukses!:Green");
                else
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, mesazh.PershkrimMesazhi + "!:Red");

            }
            catch (Exception err)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ndodhi nje gabim gjate modifikimit " + err.Message + "!:Red");
            }
            e.Handled = true;
        }

        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {
        }



        protected void gvParashikimShpenzPersoneli_HtmlFooterCellPrepared(object sender, ASPxGridViewTableFooterCellEventArgs e)
        {

        }


        protected void gvParashikimShpenzPersoneli_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {

        }
    }
}