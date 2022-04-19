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
    public partial class ABPlanifikimiIProdukteve : MyPageBase
    {
        private int idNdermarrje;
        private int idKonfig;
        private int idPerdoruesi;
        private int idGjuha;
        private int idViti;
        private int idNdermVit;
        private const string komponente = "ABPlanifikimiIProdukteve.aspx";
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
                ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 125, "PPPSV", rm, ci, idGjuha);
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
                idKonfig = int.Parse(cmbKonfigurimi.Value.ToString());
                idNdermVit = (int)hfState.Get("idNdermVit");
                mbushGrideNgaSession();
            }

            gvPlanifikimiIProdukteve.PercaktoTitlePanel( this, MenuInfo, pnlMesazhi, hfState, idPerdoruesi, idNdermarrje, idViti, idGjuha, idKonfig, komponente, rm, ci, DevExpress.Web.GridViewExportedRowType.All);
            percaktoTemplateMenu();
        }

        private void mbushGridenNgaDB(int idNdermarrje)
        {
            colPlanifikimiProdukteve col = new colPlanifikimiProdukteve(idNdermarrje, idNdermVit);
            gvPlanifikimiIProdukteve.DataSource = col;
            gvPlanifikimiIProdukteve.DataBind();

            mySessionObjects.ruajObjectNeSesion(Session, col, "planifikimiIProdukteve");
        }

        /// <summary>
        /// merr ds e grides nga sessioni
        /// </summary>
        public void mbushGrideNgaSession()
        {
            object tmp = mySessionObjects.merrObjectNgaSesioni(Session, "planifikimiIProdukteve");
            colPlanifikimiProdukteve col = (tmp as colPlanifikimiProdukteve) ?? new colPlanifikimiProdukteve(idNdermarrje, idNdermVit);
            gvPlanifikimiIProdukteve.DataSource = tmp;
            gvPlanifikimiIProdukteve.DataBind();
        }

        private void konfiguroGride(string kodi)
        {
            
            int vitiNdermarrje = new clsNdermarrjeViti(idNdermVit).Viti;
            GridUtil.PercaktoVisibleColumnsAndTypesGridSipasKodKonfigurimi(idNdermarrje, "gvPlanifikimiIProdukteve", gvPlanifikimiIProdukteve, kodi, "3005", idGjuha);
            GridUtil.PercaktoBandPerKolona(gvPlanifikimiIProdukteve, "Sasia e planifikuar "+(vitiNdermarrje+1), "SasiorPlus1", "VlerorPlus1");
            GridUtil.PercaktoBandPerKolona(gvPlanifikimiIProdukteve, "Sasia e planifikuar "+ (vitiNdermarrje + 2), "SasiorPlus2", "VlerorPlus2");
            GridUtil.PercaktoBandPerKolona(gvPlanifikimiIProdukteve, "Sasia e planifikuar "+ (vitiNdermarrje + 3), "SasiorPlus3", "VlerorPlus3");
            
            GridUtil.konfiguroGridaPerBatchEditing(gvPlanifikimiIProdukteve, false, false, false);

        }

        protected void gvPlanifikimiIProdukteve_DataBound(object sender, EventArgs e)
        {
            gvPlanifikimiIProdukteve.KeyFieldName = "RreshtiId";
        
            GridUtil.PercaktoNgjyrenPerKolonatReadOnly(gvPlanifikimiIProdukteve, "PPID", "RreshtiId", "NjesiaId", "Emertimi", "IdNdermarrje", "IdKrijuesi", "IdModifikuesi", "DtKrijimi", "DtModifikimi");
            
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

        protected void gvPlanifikimiIProdukteve_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters.Contains("3005"))//per te kontrolluar nese eshte callback konfigurimi
            {
                konfiguroGride(e.Parameters.Split(';')[1]);
            }
        }

        protected void gvPlanifikimiIProdukteve_BatchUpdate(object sender, DevExpress.Web.Data.ASPxDataBatchUpdateEventArgs e)
        {
            try
            {
                clsMesazh mesazh = new clsMesazh(true);
                clsTeDrejtaRoli teDrejta = new clsTeDrejtaRoli();
                teDrejta.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, komponente);
                if (!teDrejta.DMod)
                {
                    mesazh = new clsMesazh(false, MessagesResource.Messages["msgNukKeniTeDrejta"]);
                }
                else
                {
                    object tmp = mySessionObjects.merrObjectNgaSesioni(Session, "planifikimiIProdukteve");

                    colPlanifikimiProdukteve col = (tmp as colPlanifikimiProdukteve) ?? new colPlanifikimiProdukteve(idNdermarrje, idNdermVit);

                    for (int i = 0; i < e.UpdateValues.Count; i++)
                    {
                        //string key = e.UpdateValues[i].Keys[0].ToString();
                        clsPlanifikimiProdukteve planifikimiVjeter = col.FirstOrDefault(x => x.RreshtiId == e.UpdateValues[i].MerrKeyValue<int>());

                        clsPlanifikimiProdukteve planifikimiRi = e.UpdateValues[i].MerrCustomUpdatedObject<clsPlanifikimiProdukteve>(planifikimiVjeter);
                        planifikimiRi.IdModifikuesi = idPerdoruesi;
                        mesazh = planifikimiRi.Modifiko();
                        mySessionObjects.ruajObjectNeSesion(Session, col, "planifikimiIProdukteve");
                    }
                }
                if (mesazh.Status)
                {
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Modifikimi u krye me sukses!:Green");
                }
                else
                {
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session,mesazh.PershkrimMesazhi+":Red");
                }
            }
            catch (Exception err)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session,  err.Message + "!:Red");
            }
            e.Handled = true;
        }

        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {
        }

        

        protected void gvPlanifikimiIProdukteve_HtmlFooterCellPrepared(object sender, ASPxGridViewTableFooterCellEventArgs e)
        {

        }


        protected void gvPlanifikimiIProdukteve_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {

        }
    }
}