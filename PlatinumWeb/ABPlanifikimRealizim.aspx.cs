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
using DbCore.IMBUtils.Logging;
using DevExpress.Web.Data;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    public partial class ABPlanifikimRealizim : MyPageBase
    {
        private int idNdermarrje;
        private int idKonfig;
        private int idPerdoruesi;
        private int idGjuha;
        private int idViti;
        private int idNdermVit;
        private bool raportuese;
        private const string komponente = "ABPlanifikimRealizim.aspx";
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
                raportuese = clsNdermarrje.EshteRaportuese(idNdermarrje);
                hfState.Set("idGjuha", idGjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idPerdoruesi", idPerdoruesi);
                hfState.Set("idViti", idViti);
                hfState.Set("komponente", komponente);
                hfState.Set("idNdermVit", idNdermVit);
                hfState.Set("raportuese", raportuese);
                ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 145, "PR", rm, ci, idGjuha);
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


                mbushGridenNgaDB();
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
                raportuese = (bool)hfState.Get("raportuese");
                mbushGrideNgaSession();
            }

            gvPlanifikim.PercaktoTitlePanel( this, MenuInfo, pnlMesazhi, hfState, idPerdoruesi, idNdermarrje, idViti, idGjuha, idKonfig, komponente, rm, ci, DevExpress.Web.GridViewExportedRowType.All);
            PercaktoTemplateMenu();
        }

        private void mbushGridenNgaDB()
        {
            colPlanifikimRealizim col = new colPlanifikimRealizim(idNdermarrje, idNdermVit, idPerdoruesi, raportuese);
            gvPlanifikim.DataSource = col;
            gvPlanifikim.DataBind();

            mySessionObjects.ruajObjectNeSesion(Session, col, "planifikimRealizim");
        }

        /// <summary>
        /// merr ds e grides nga sessioni
        /// </summary>
        public void mbushGrideNgaSession()
        {
            object tmp = mySessionObjects.merrObjectNgaSesioni(Session, "planifikimRealizim");
            colPlanifikimRealizim col = (tmp as colPlanifikimRealizim) ?? new colPlanifikimRealizim(idNdermarrje, idNdermVit, idPerdoruesi, raportuese);
            gvPlanifikim.DataSource = tmp;
            gvPlanifikim.DataBind();
        }

        private void konfiguroGride(string kodi)
        {

            GridUtil.PercaktoVisibleColumnsAndTypesGridSipasKodKonfigurimi(idNdermarrje, "gvPlanifikim", gvPlanifikim, kodi, "3040", idGjuha);
            PercaktoMuajtEFshehur();
            GridUtil.PercaktoBandPerKolona(gvPlanifikim, "Sasia e terhequr", "Janar", "Shkurt", "Mars", "Prill", "Maj", "Qershor", "Korrik", "Gusht", "Shtator", "Tetor", "Nentor", "Dhjetor");

            GridUtil.konfiguroGridaPerBatchEditing(gvPlanifikim, false, false, raportuese);
            
        }

        private void PercaktoMuajtEFshehur()
        {
            object tmp = mySessionObjects.merrObjectNgaSesioni(Session, "planifikimRealizim");
            colPlanifikimRealizim col = (tmp as colPlanifikimRealizim) ?? new colPlanifikimRealizim(idNdermarrje, idNdermVit, idPerdoruesi, raportuese);
            Array array = Enum.GetValues(typeof(DbCore.DbListPagesat.Muajt));
            for (int i = 1; i < array.Length; i++)
            {
                string muaj = array.GetValue(i).ToString();
                bool kaVlere = clsFunksione.KaNdonjeVlereKolonaFloat(col, muaj);
                gvPlanifikim.Columns[muaj].Visible = kaVlere;
                if (!kaVlere)
                    gvPlanifikim.Columns[muaj].Width = 0;
                
            }
        }
        protected void gvPlanifikim_DataBound(object sender, EventArgs e)
        {
            gvPlanifikim.KeyFieldName = "IdArtikulli";
        
            GridUtil.PercaktoNgjyrenPerKolonatReadOnly(gvPlanifikim, "RowNum", "Emertimi", "Njesia");
            
        }

      

        private void PercaktoTemplateMenu()
        {
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdoruesi, idNdermarrje, ASPxMenu1, komponente, this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, false, true, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session), true);
            if (raportuese)
            {
                clsToolbarConfig.DisableMenuItems(ASPxMenu1, "Ruaj", "Pastro");
            }
        }

        public void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
        }

        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
        }

        protected void gvPlanifikim_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters.Contains("3040"))//per te kontrolluar nese eshte callback konfigurimi
            {
                konfiguroGride(e.Parameters.Split(';')[1]);
            }
        }

        protected void gvPlanifikim_BatchUpdate(object sender, DevExpress.Web.Data.ASPxDataBatchUpdateEventArgs e)
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
                    object tmp = mySessionObjects.merrObjectNgaSesioni(Session, "planifikimRealizim");

                    colPlanifikimRealizim col = (tmp as colPlanifikimRealizim) ?? new colPlanifikimRealizim(idNdermarrje, idNdermVit, idPerdoruesi, raportuese);

                    foreach (ASPxDataUpdateValues updated in e.UpdateValues)
                    {
                        clsPlanifikimRealizim planifikimiVjeter = col.FirstOrDefault(x => x.IdArtikulli == updated.MerrKeyValue<int>());
                        clsPlanifikimRealizim planifikimiRi = updated.MerrCustomUpdatedObject<clsPlanifikimRealizim>(planifikimiVjeter);
                        planifikimiRi.IdModifikuesi = idPerdoruesi;
                        mySessionObjects.ruajObjectNeSesion(Session, col, "planifikimRealizim");
                    }

                    mesazh = col.Ruaj();
                }
                if (mesazh.Status)
                {
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Modifikimi u krye me sukses!:Green");
                    mbushGridenNgaDB();
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

        

        protected void gvPlanifikim_HtmlFooterCellPrepared(object sender, ASPxGridViewTableFooterCellEventArgs e)
        {

        }


        protected void gvPlanifikim_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {

        }
    }
}