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
    public partial class ABInventariPerdorues : MyPageBase
    {
        private int idNdermarrje;
        private int idKonfig;
        private int idPerdoruesi;
        private int idGjuha;
        private int idViti;
        string kodi;
        private const string komponente = "ABInventariPerdorues.aspx";
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
                hfState.Set("idGjuha", idGjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idPerdoruesi", idPerdoruesi);
                hfState.Set("idViti", idViti);
                hfState.Set("komponente", komponente);
                ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 129, "ISP", rm, ci, idGjuha);
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
                GridUtil.PercaktoVisibleColumnsAndTypesGridSipasKodKonfigurimi(idNdermarrje, "gvinventariPerdorues", gvinventariPerdorues, kodi, "3018", idGjuha);

            }
            else
            {
                idNdermarrje = (int)hfState.Get("idNdermarrje");
                idPerdoruesi = (int)hfState.Get("idPerdoruesi");
                idGjuha = (int)hfState.Get("idGjuha");
                idViti = (int)hfState.Get("idViti");

                idKonfig = int.Parse(cmbKonfigurimi.Value.ToString());
                mbushGrideNgaSession();
                konfiguroGride(cmbKonfigurimi.Text);
                GridUtil.PercaktoNgjyrenPerKolonatReadOnly
                  (gvinventariPerdorues, "InventariId", "RreshtiId", "Artikulli", "IdNdermarrje", "IdKrijuesi", "IdModifikuesi", "DtKrijimi", "DtModifikimi");
                krijoTotalSummary();
            }

            gvinventariPerdorues.PercaktoTitlePanel(this, MenuInfo, pnlMesazhi, hfState, idPerdoruesi, idNdermarrje, idViti, idGjuha, idKonfig, komponente, rm, ci, DevExpress.Web.GridViewExportedRowType.All);
            percaktoTemplateMenu();
        }

        private void mbushGridenNgaDB(int idNdermarrje)
        {
            colInventariPerdorues col = new colInventariPerdorues(idNdermarrje);
            gvinventariPerdorues.DataSource = col;
            gvinventariPerdorues.DataBind();

            mySessionObjects.ruajObjectNeSesion(Session, col, "inventariPerdorues");
        }

        /// <summary>
        /// merr ds e grides nga sessioni
        /// </summary>
        public void mbushGrideNgaSession()
        {
            object tmp = mySessionObjects.merrObjectNgaSesioni(Session, "inventariPerdorues");
            colInventariPerdorues col = (tmp as colInventariPerdorues) ?? new colInventariPerdorues(idNdermarrje);
            gvinventariPerdorues.DataSource = tmp;
            gvinventariPerdorues.DataBind();
        }

        private void konfiguroGride(string kodi)
        {
            GridUtil.konfiguroGridaPerBatchEditing(gvinventariPerdorues, false, false, false);


            GridUtil.PercaktoNgjyrenPerKolonatReadOnly(gvinventariPerdorues, "InventariId", "RreshtiId", "Artikulli", "IdNdermarrje", "IdKrijuesi", "IdModifikuesi", "DtKrijimi", "DtModifikimi");
            krijoTotalSummary();
        }

        protected void gvinventariPerdorues_DataBound(object sender, EventArgs e)
        {

            gvinventariPerdorues.KeyFieldName = "RreshtiId";
           
             

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

        protected void gvinventariPerdorues_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters.Contains("3018"))//per te kontrolluar nese eshte callback konfigurimi
            {
                konfiguroGride(e.Parameters.Split(';')[1]);
            }
        }
        private void krijoTotalSummary()
        {
            foreach (GridViewColumn col in gvinventariPerdorues.VisibleColumns)
            {
                if (col.Name == "Artikulli")
                {
                    col.FooterTemplate = new MyFooterCellTemplate(col.Name, "Totali ");
                    continue;
                }

                if (col.GetType() == typeof(GridViewDataSpinEditColumn))
                {
                    gvinventariPerdorues.ShtoTotalSummary("n2", DevExpress.Data.SummaryItemType.Sum, col.Name);
                    GridUtil.PercaktoTemplateTotalSummaryFooter(gvinventariPerdorues, "n2", col.Name);
                }
            }
        }
        protected void gvinventariPerdorues_BatchUpdate(object sender, DevExpress.Web.Data.ASPxDataBatchUpdateEventArgs e)
        {
            try
            {
                clsMesazh mesazhi = new clsMesazh(true);
                clsTeDrejtaRoli teDrejta = new clsTeDrejtaRoli();
                teDrejta.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, komponente);

                if (!teDrejta.DMod)
                {
                    mesazhi = new clsMesazh(false, MessagesResource.Messages["msgNukKeniTeDrejta"]);
                }
                else
                {
                    object tmp = mySessionObjects.merrObjectNgaSesioni(Session, "inventariPerdorues");

                    colInventariPerdorues col = (tmp as colInventariPerdorues) ?? new colInventariPerdorues(idNdermarrje);

                    for (int i = 0; i < e.UpdateValues.Count; i++)
                    {
                        string key = e.UpdateValues[i].Keys[0].ToString();
                        clsInventariPerdorues inventariVjeter = col.Where(x => Convert.ToString(x.RreshtiId) == key).FirstOrDefault();

                        clsInventariPerdorues inventariRi = e.UpdateValues[i].MerrCustomUpdatedObject<clsInventariPerdorues>(inventariVjeter);
                        inventariRi.IdModifikuesi = idPerdoruesi;
                        mesazhi = inventariRi.Modifiko();
                    }
                    mySessionObjects.ruajObjectNeSesion(Session, col, "inventariPerdorues");
                }

                if (mesazhi.Status)
                {
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Modifikimi u krye me sukses!:Green");
                }
                else
                {
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, mesazhi.PershkrimMesazhi + ":Red");
                }
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



        protected void gvinventariPerdorues_HtmlFooterCellPrepared(object sender, ASPxGridViewTableFooterCellEventArgs e)
        {

        }


        protected void gvinventariPerdorues_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {

        }
        
    }
}