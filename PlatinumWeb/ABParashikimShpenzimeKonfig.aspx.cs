using DbCore;
using DbCore.DbAdmin;
using DbCore.DbAnalizeBuxheti;
using DevExpress.Web;
using System;
using System.Linq;
using System.Web.UI;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    public partial class ABParashikimShpenzimeKonfig : MyPageBase
    {
        private int idNdermarrje;
        private int idKonfig;
        private int idPerdoruesi;
        private int idGjuha;
        private int idViti;
        private const string komponente = "ABParashikimShpenzimeKonfig.aspx";

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

            if (!clsNdermarrje.EshteRaportuese(idNdermarrje))
                DbCore.clsFunksione.logout(Session,true,"FaqePaautorizuar");

            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (!IsPostBack)
            {
                idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
                idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
                hfState.Set("idGjuha", idGjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idPerdoruesi", idPerdoruesi);
                hfState.Set("idViti", idViti);
                hfState.Set("komponente", komponente);
                ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 126, "PSHPP", rm, ci, idGjuha);
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
                mbushGrideNgaSession();
            }

            gvKonfigParashikimShpenz.PercaktoTitlePanel(this, MenuInfo, pnlMesazhi, hfState, idPerdoruesi, idNdermarrje, idViti, idGjuha, idKonfig, komponente, rm, ci, DevExpress.Web.GridViewExportedRowType.All);
            percaktoTemplateMenu();
        }

        private void mbushGridenNgaDB(int idNdermarrje)
        {
            colParashikimShpenzPersoneliConfig col = new colParashikimShpenzPersoneliConfig(idNdermarrje);
            gvKonfigParashikimShpenz.DataSource = col;
            gvKonfigParashikimShpenz.DataBind();

            mySessionObjects.ruajObjectNeSesion(Session, col, "parashikimShpenzKonfig");
        }

        /// <summary>
        /// merr ds e grides nga sessioni
        /// </summary>
        public void mbushGrideNgaSession()
        {
            object tmp = mySessionObjects.merrObjectNgaSesioni(Session, "parashikimShpenzKonfig");
            colParashikimShpenzPersoneliConfig col = (tmp as colParashikimShpenzPersoneliConfig) ?? new colParashikimShpenzPersoneliConfig(idNdermarrje);
            gvKonfigParashikimShpenz.DataSource = tmp;
            gvKonfigParashikimShpenz.DataBind();
        }

        private void konfiguroGride(string kodi)
        {
            GridUtil.PercaktoVisibleColumnsAndTypesGridSipasKodKonfigurimi(idNdermarrje, "gvKonfigParashikimShpenz", gvKonfigParashikimShpenz, kodi, "3006", idGjuha);
            GridUtil.konfiguroGridaPerBatchEditing(gvKonfigParashikimShpenz, false, false, false);
        }

        protected void gvKonfigParashikimShpenz_DataBound(object sender, EventArgs e)
        {
            gvKonfigParashikimShpenz.KeyFieldName = "RreshtiId";
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

        protected void gvKonfigParashikimShpenz_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            //if (e.Parameters.Contains("3006"))//per te kontrolluar nese eshte callback konfigurimi
            //{
            //    konfiguroGride(e.Parameters.Split(';')[1]);
            //}
            //if (e.Parameters.Contains("Riruaj"))
            //{
            //   // clsMesazh mesazhi = AnalizeBuxheti.RuajKonfigurimPerShpenzimePersoneli(mySessionObjects.merrIdNdermarrjeSesioni(Session));
            //    if (mesazhi.Status)
            //        DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ruajtja u krye me sukses!:Green");
            //    else
            //        DbCore.mySessionObjects.ruajMesazhNeSesion(Session, mesazhi.PershkrimMesazhi + ":Red");
            //}
        }

        protected void gvKonfigParashikimShpenz_BatchUpdate(object sender, DevExpress.Web.Data.ASPxDataBatchUpdateEventArgs e)
        {
            try
            {
                clsMesazh mesazh = new clsMesazh(true);
                object tmp = mySessionObjects.merrObjectNgaSesioni(Session, "parashikimShpenzKonfig");
                clsTeDrejtaRoli teDrejta = new clsTeDrejtaRoli();
                teDrejta.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, komponente);
                colParashikimShpenzPersoneliConfig col = (tmp as colParashikimShpenzPersoneliConfig) ?? new colParashikimShpenzPersoneliConfig(idNdermarrje);
                if (!teDrejta.DMod)
                {
                    mesazh = new clsMesazh(false, MessagesResource.Messages["msgNukKeniTeDrejta"]);
                }
                else
                {
                    for (int i = 0, teUpdatetuara = e.UpdateValues.Count; i < teUpdatetuara; i++)
                    {
                        //string key = e.UpdateValues[i].Keys[0].ToString();
                        clsParashikimShpenzPersoneliConfig konfigVjeter = col.Where(x => x.RreshtiId == e.UpdateValues[i].MerrKeyValue<int>()).FirstOrDefault();

                        clsParashikimShpenzPersoneliConfig konfigUpdated = e.UpdateValues[i].MerrCustomUpdatedObject(konfigVjeter);
                        konfigUpdated.IdModifikuesi = idPerdoruesi;
                        mesazh = konfigUpdated.Modifiko();
                    }
                }
                if (mesazh.Status)
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, MessagesResource.Messages["msgModifikimiMeSuksesGreen"]);
                else
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, mesazh.PershkrimMesazhi + ":Red");

                gvKonfigParashikimShpenz.DataSource = col;
                gvKonfigParashikimShpenz.DataBind();
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

        protected void gvKonfigParashikimShpenz_HtmlFooterCellPrepared(object sender, ASPxGridViewTableFooterCellEventArgs e)
        {
        }

        protected void gvKonfigParashikimShpenz_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
        }

        protected void gvKonfigParashikimShpenz_CustomErrorText(object sender, ASPxGridViewCustomErrorTextEventArgs e)
        {
        }
    }
}