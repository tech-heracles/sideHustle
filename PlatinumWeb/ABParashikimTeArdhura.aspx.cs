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
    public partial class ABParashikimTeArdhura : MyPageBase
    {
        private int idNdermarrje;
        private int idKonfig;
        private int idPerdoruesi;
        private int idGjuha;
        private int idViti;
        private int idNdermVit;

        private const string komponente = "ABParashikimTeArdhura.aspx";
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
                ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 122, "PAMIB", rm, ci, idGjuha);
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
                GridUtil.PercaktoNgjyrenPerKolonatReadOnly
                  (gvparashikimTeArdhura, "IdAuto", "Kodi", "PTaId", "RreshtiId", "Emertimi", "IdNdermarrje", "IdKrijuesi", "IdModifikuesi", "DtKrijimi", "DtModifikimi");
                krijoTotalSummary();
            }

            gvparashikimTeArdhura.PercaktoTitlePanel( this, MenuInfo, pnlMesazhi, hfState, idPerdoruesi, idNdermarrje, idViti, idGjuha, idKonfig, komponente, rm, ci, DevExpress.Web.GridViewExportedRowType.All);
            percaktoTemplateMenu();
        }

        private void mbushGridenNgaDB(int idNdermarrje)
        {
            colParashikimiTeArdhura col = new colParashikimiTeArdhura(idNdermarrje, idNdermVit);
            gvparashikimTeArdhura.DataSource = col;
            gvparashikimTeArdhura.DataBind();

            mySessionObjects.ruajObjectNeSesion(Session, col, "parashikimiTeArdhura");
        }

        /// <summary>
        /// merr ds e grides nga sessioni
        /// </summary>
        public void mbushGrideNgaSession()
        {
            object tmp = mySessionObjects.merrObjectNgaSesioni(Session, "parashikimiTeArdhura");
            colParashikimiTeArdhura col = (tmp as colParashikimiTeArdhura) ?? new colParashikimiTeArdhura(idNdermarrje, idNdermVit);
            gvparashikimTeArdhura.DataSource = tmp;
            gvparashikimTeArdhura.DataBind();
        }

        private void konfiguroGride(string kodi)
        {
            GridUtil.PercaktoVisibleColumnsAndTypesGridSipasKodKonfigurimi(idNdermarrje, "gvparashikimTeArdhura", gvparashikimTeArdhura, kodi, "3002", idGjuha);
            int vit = (new clsNdermarrjeViti(idNdermVit).Viti);
            GridUtil.PercaktoBandPerKolona(gvparashikimTeArdhura, "Fakti i vitit "+(vit-1), "TeArdhuraTotaleParaardhes");
            GridUtil.PercaktoBandPerKolona(gvparashikimTeArdhura, "I pritshmi "+vit, "ITakojneInstitucionitAktuale", "DerdhenNeBuxhetAktuale");
            GridUtil.PercaktoBandPerKolona(gvparashikimTeArdhura, "Parashikimi viti  "+(vit+1), "ITakojneInstitucionitPasardhes", "DerdhenNeBuxhetPasardhes");
            gvparashikimTeArdhura.PercaktoEmerPerKolonen("Parashikime per vitin " + (vit + 2), "ParashikimiPlus2");
            gvparashikimTeArdhura.PercaktoEmerPerKolonen("Parashikime per vitin " + (vit + 3), "ParashikimiPlus3");

            GridUtil.konfiguroGridaPerBatchEditing(gvparashikimTeArdhura, false, false, false);
            GridUtil.PercaktoNgjyrenPerKolonatReadOnly(gvparashikimTeArdhura,  "IdAuto", "Kodi", "PTaId", "RreshtiId", "Emertimi", "IdNdermarrje", "IdKrijuesi", "IdModifikuesi", "DtKrijimi", "DtModifikimi");
            krijoTotalSummary();
        }

        protected void gvparashikimTeArdhura_DataBound(object sender, EventArgs e)
        {
          
            gvparashikimTeArdhura.KeyFieldName = "RreshtiId";
           
             

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

        protected void gvparashikimTeArdhura_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters.Contains("3002"))//per te kontrolluar nese eshte callback konfigurimi
            {
                konfiguroGride(e.Parameters.Split(';')[1]);
            }
        }

        protected void gvparashikimTeArdhura_BatchUpdate(object sender, DevExpress.Web.Data.ASPxDataBatchUpdateEventArgs e)
        {
            try
            {
                clsMesazh mesazhi = new clsMesazh();
                clsTeDrejtaRoli teDrejta = new clsTeDrejtaRoli();
                teDrejta.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, komponente);
                if (!teDrejta.DMod)
                {
                    mesazhi = new clsMesazh(false, MessagesResource.Messages["msgNukKeniTeDrejta"]);
                }
                else
                {
                    object tmp = mySessionObjects.merrObjectNgaSesioni(Session, "parashikimiTeArdhura");

                    colParashikimiTeArdhura col = (tmp as colParashikimiTeArdhura) ?? new colParashikimiTeArdhura(idNdermarrje, idNdermVit);

                    for (int i = 0; i < e.UpdateValues.Count; i++)
                    {
                       // string key = e.UpdateValues[i].Keys[0].ToString();
                        clsParashikimiTeArdhura parashikimVjeter = col.Where(x => x.RreshtiId==e.UpdateValues[i].MerrKeyValue<int>()).FirstOrDefault();

                        clsParashikimiTeArdhura parashikimRi = e.UpdateValues[i].MerrCustomUpdatedObject<clsParashikimiTeArdhura>(parashikimVjeter);
                        parashikimRi.IdModifikuesi = idPerdoruesi;
                        mesazhi = parashikimRi.Modifiko();
                    }

                    mySessionObjects.ruajObjectNeSesion(Session, col, "parashikimiTeArdhura");
                }
                if (mesazhi.Status)
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Modifikimi u krye me sukses!:Green");
                else
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, mesazhi.PershkrimMesazhi + "!:Red");
            }
            catch (Exception err)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ndodhi nje gabim gjate modifikimit "+err.Message+"!:Red");
            }
            e.Handled = true;
        }

        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {
        }

        private void krijoTotalSummary()
        {
            foreach (GridViewColumn col in gvparashikimTeArdhura.VisibleColumns)
            {
                if (col.Name == "Emertimi")
                {
                    col.FooterTemplate = new MyFooterCellTemplate(col.Name, "Totali ");
                    continue;
                }

                if (col.GetType() == typeof(GridViewDataSpinEditColumn))
                {
                    gvparashikimTeArdhura.ShtoTotalSummary("n2", DevExpress.Data.SummaryItemType.Sum, col.Name);
                    GridUtil.PercaktoTemplateTotalSummaryFooter(gvparashikimTeArdhura, "n2", col.Name);
                }
            }
        }
       
        protected void gvparashikimTeArdhura_HtmlFooterCellPrepared(object sender, ASPxGridViewTableFooterCellEventArgs e)
        {

        }


        protected void gvparashikimTeArdhura_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {

        }
    }
}