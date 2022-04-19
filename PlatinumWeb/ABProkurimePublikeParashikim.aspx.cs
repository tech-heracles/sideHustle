using DbCore;
using DbCore.DbAdmin;
using DbCore.DbAnalizeBuxheti;
using DevExpress.Utils;
using DevExpress.Web;
using PlatinumWeb.Templates;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using DbCore.IMBUtils.Logging;
using DevExpress.Web.Data;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    public partial class ABProkurimePublikeParashikim : MyPageBase
    {
        private int idNdermarrje;
        private int idKonfig;
        private int idPerdoruesi;
        private int idGjuha;
        private int idViti;
        private int idNdermVit;
        private int idKoka;
        bool raportuese;
        private const string komponente = "ABProkurimePublikeParashikim.aspx";
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
                idKoka = clsKokaRealizimProkurimesh.merrIdParashikimi(idNdermarrje, idNdermVit);

                raportuese = clsNdermarrje.EshteRaportuese(idNdermarrje);
                hfState.Set("idGjuha", idGjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idPerdoruesi", idPerdoruesi);
                hfState.Set("idViti", idViti);
                hfState.Set("komponente", komponente);
                hfState.Set("idNdermVit", idNdermVit);
                hfState.Set("idKoka", idNdermVit);
                hfState.Set("raportuese", raportuese);
                ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 144, "RPPP", rm, ci, idGjuha);
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
                idKoka = (int)hfState.Get("idKoka");
                raportuese = (bool)hfState.Get("raportuese");
                mbushGrideNgaSession();
            }

            gvProkurimetParashikim.PercaktoTitlePanel( this, MenuInfo, pnlMesazhi, hfState, idPerdoruesi, idNdermarrje, idViti, idGjuha, idKonfig, komponente, rm, ci, DevExpress.Web.GridViewExportedRowType.All);
            percaktoTemplateMenu();
        }

        private void mbushGridenNgaDB(int idNdermarrje)
        {
            
            if (raportuese)
            {
                DataTable dt = AnalizeBuxheti.MerrTeDhenatERaportitParashikimProkurimeshPublike(idNdermarrje, idNdermVit);
                gvProkurimetParashikim.DataSource = dt;
                mySessionObjects.ruajObjectNeSesion(Session, dt, "parashikimProkurimesh");
            }else
            {
                colTrupiRealizimProkurimesh trupi = new colTrupiRealizimProkurimesh(idKoka);
                gvProkurimetParashikim.DataSource = trupi;
                mySessionObjects.ruajObjectNeSesion(Session, trupi, "parashikimProkurimesh");
            }
            
            
            gvProkurimetParashikim.DataBind();

            
        }

        /// <summary>
        /// merr ds e grides nga sessioni
        /// </summary>
        public void mbushGrideNgaSession()
        {
            object tmp = mySessionObjects.merrObjectNgaSesioni(Session, "parashikimProkurimesh");
            if (raportuese)
            {
                DataTable dt = (tmp as DataTable) ?? AnalizeBuxheti.MerrTeDhenatERaportitParashikimProkurimeshPublike(idNdermarrje, idNdermVit);
                gvProkurimetParashikim.DataSource = dt;
            }else
            {
                colTrupiRealizimProkurimesh col = (tmp as colTrupiRealizimProkurimesh) ?? new colTrupiRealizimProkurimesh(idKoka);
                gvProkurimetParashikim.DataSource = col;
            }
            gvProkurimetParashikim.DataBind();
        }

        private void konfiguroGride(string kodi)
        {
            
            int vitiNdermarrje = new clsNdermarrjeViti(idNdermVit).Viti;
            GridUtil.PercaktoVisibleColumnsAndTypesGridSipasKodKonfigurimi(idNdermarrje, "gvProkurimetParashikim", gvProkurimetParashikim, kodi, "3039", idGjuha);
            
            
            GridUtil.konfiguroGridaPerBatchEditing(gvProkurimetParashikim, false, false, raportuese);
            gvProkurimetParashikim.GroupBy(gvProkurimetParashikim.Columns["ZeriPrind"]);
        }

        protected void gvProkurimetParashikim_DataBound(object sender, EventArgs e)
        {
            gvProkurimetParashikim.KeyFieldName = (raportuese) ? "RowNum" : "RpkId";
        
            GridUtil.PercaktoNgjyrenPerKolonatReadOnly(gvProkurimetParashikim, "RowNum", "IdTrupiRp", "IdKokaRp", "RpkId", "IdKrijuesi", "IdModifikuesi", "DtKrijimi", "DtModifikimi", "OperatoriEkonomik", "Zeri", "ZeriPrind");


        }

      

        private void percaktoTemplateMenu()
        {
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdoruesi, idNdermarrje, ASPxMenu1, komponente, this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, false, true, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session), true);
            if (raportuese)
            {
                clsToolbarConfig.DisableMenuItems(ASPxMenu1, "Ruaj", "Fshi");
            }
        }

        public void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
        }

        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
        }

        protected void gvProkurimetParashikim_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters.Contains("3039"))//per te kontrolluar nese eshte callback konfigurimi
            {
                konfiguroGride(e.Parameters.Split(';')[1]);
            }
        }

        protected void gvProkurimetParashikim_BatchUpdate(object sender, DevExpress.Web.Data.ASPxDataBatchUpdateEventArgs e)
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
                    object tmp = mySessionObjects.merrObjectNgaSesioni(Session, "parashikimProkurimesh");

                    colTrupiRealizimProkurimesh col = (tmp as colTrupiRealizimProkurimesh) ?? new colTrupiRealizimProkurimesh(idKoka); 

                    foreach (ASPxDataUpdateValues updated in e.UpdateValues)
                    {
                        clsTrupiRealizimProkurimesh parashikimiVjeter = col.FirstOrDefault(x => x.RpkId == updated.MerrKeyValue<int>());
                        clsTrupiRealizimProkurimesh parashikimiRi = updated.MerrCustomUpdatedObject<clsTrupiRealizimProkurimesh>(parashikimiVjeter);
                        parashikimiRi.IdModifikuesi = idPerdoruesi;
                        mesazh = parashikimiRi.Modifiko();
                        mySessionObjects.ruajObjectNeSesion(Session, col, "parashikimProkurimesh");
                    }
                }
                if (mesazh.Status)
                {
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, MessagesResource.Messages["msgModifikimiMeSuksesGreen"]);
                }
                else
                {
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session,mesazh.PershkrimMesazhi+":Red");
                }
            }
            catch (Exception err)
            {
                ImbLogger.Error(err.Message);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session,  err.Message + "!:Red");
            }
            e.Handled = true;
        }

        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {
        }

        

        protected void gvProkurimetParashikim_HtmlFooterCellPrepared(object sender, ASPxGridViewTableFooterCellEventArgs e)
        {

        }


        protected void gvProkurimetParashikim_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {

        }
    }
}