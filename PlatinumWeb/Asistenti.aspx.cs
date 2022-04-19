using DbCore;
using DevExpress.Web;
using PlatinumWeb.Templates;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class Asistenti : MyPageBase
    {
        private const string komponente = "Asistenti.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {
            int idPerdoruesi;
            int idGjuha;
            int idNdermarrje;
            int idViti;
            int idNdermarrjeVit;
            if (hfState.Count == 0)
            {
                if (!DbCore.mySessionObjects.isLogedIn(Session))
                {
                    DbCore.clsFunksione.logout(Session, true, "FaqePaautorizuar");
                }
                idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
                {
                    Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerdoruesi);
                }
                idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
                idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
                idNdermarrjeVit = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
                hfState.Set("idPerdoruesi", idPerdoruesi);
                hfState.Set("idGjuha", idGjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idViti", idViti);
                hfState.Set("idNdermarrjeVit", idNdermarrjeVit);
                hfState.Set("Meme", DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
            }
            else
            {
                idPerdoruesi = (int)hfState["idPerdoruesi"];
                idGjuha = (int)hfState["idGjuha"];
                idNdermarrje = (int)hfState["idNdermarrje"];
                idViti = (int)hfState["idViti"];
                idNdermarrjeVit = (int)hfState["idNdermarrjeVit"];
            }
            CultureInfo cultinf = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo(idGjuha);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (!Page.IsPostBack)
            {
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, komponente);
                merrGrideAsistentiNgaDb(false, "");
                konfigurimerGride(idPerdoruesi, idNdermarrje, idViti, idGjuha, cultinf, rm);
            }
            else
            {
                merrGrideAsistentiNgaSession();
                konfigurimerGride(idPerdoruesi, idNdermarrje, idViti, idGjuha, cultinf, rm);
            }

            percaktoTemplateMenu(idGjuha, idViti, idPerdoruesi, idNdermarrje, ASPxMenu1);
        }

        protected void konfigurimerGride(int idPerdoruesi, int idNdermarrje, int idViti, int idGjuha, CultureInfo cultinf, ResourceManager rm)
        {
            gvAsistenti.PercaktoTitlePanel(this, MenuInfo, pnlMesazhi, hfState, idPerdoruesi, idNdermarrje, idViti, idGjuha, 1, komponente, 2032, "Ndermarrje", rm, cultinf);
            GridUtil.konfigGrideListeEMadhePaTheme(gvAsistenti, "Id");
            gvAsistenti.Columns["Id"].Visible = false;
            gvAsistenti.Columns["Mesazh Gabimi"].Width = Unit.Percentage(10);
            //if (gvAsistenti.Columns["Rregullo"] != null)
            //{
            //    GridViewDataTextColumn col = gvAsistenti.Columns["Rregullo"] as GridViewDataTextColumn;
            //    col.DataItemTemplate = new MyButtonTemplate("Rregullo");
            //}
        }

        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(Convert.ToInt32(hfState["idGjuha"]), Convert.ToInt32(hfState["idViti"]), Convert.ToInt32(hfState["idPerdoruesi"]), Convert.ToInt32(hfState["idNdermarrje"]), ASPxMenu1);
        }


        private void percaktoTemplateMenu(int idGjuha, int idViti, int idPerdorues, int idNdermarrje, ASPxMenu ASPxMenu1)
        {
            bool meme = (bool)hfState["Meme"];
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdorues, idNdermarrje, ASPxMenu1, komponente, this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, hfShtimModifikim.Value == "modifikim" ? false : true, false, false, meme);
        }

        public void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
        }

        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
        }


        public void merrGrideAsistentiNgaDb(bool ekzekuto, string idPerEkzekutim)
        {
            List<string> idTe = new List<string>();
            if (!String.IsNullOrEmpty(idPerEkzekutim))
                idTe = new List<string>(idPerEkzekutim.Split(','));           
            DataTable dt = DbCore.clsFunksione.merrgjitheSpAsistenti(ekzekuto, idTe, IdNdermarrja);
            DbCore.mySessionObjects.ruajGrideNeSession(komponente, Session, dt);
            gvAsistenti.DataSource = dt;
            gvAsistenti.DataBind();
            dt.Dispose();
        }

        private void merrGrideAsistentiNgaSession()
        {
            DataTable tmpObject;
            bool sukses = DbCore.mySessionObjects.merrGrideNgaSessioni(komponente, Session, out tmpObject);
            if (!sukses)
                merrGrideAsistentiNgaDb(false, "");
            else
            {
                gvAsistenti.DataSource = tmpObject;
                gvAsistenti.DataBind();
                tmpObject.Dispose();
            }
        }

        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {
            //if (e.Item.Name == "Rifresko")
            //    merrGrideAsistentiNgaDb();
        }

        protected void gvAsistenti_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            //if (e.RowType == DevExpress.Web.GridViewRowType.Data)
            //{
            //    GridViewDataColumn col = ((ASPxGridView)sender).Columns["Rregullo"] as GridViewDataColumn;
            //    ASPxButton btn = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col, "btn") as ASPxButton;

            //    if (btn != null)
            //    {
            //        btn.ClientInstanceName = "RregulloBtn" + e.VisibleIndex;
            //        btn.ClientSideEvents.Click = "function(s,e){ClickRregulloBtn(s,e," + e.VisibleIndex + ");}";
            //    }
            //}
        }

        protected void gvAsistenti_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string idPerEkzekutim = e.Parameters;
            if (String.IsNullOrEmpty(idPerEkzekutim))
                merrGrideAsistentiNgaDb(false, "");
            else
                merrGrideAsistentiNgaDb(true, idPerEkzekutim);
        }

        protected void gvAsistenti_DataBound(object sender, EventArgs e)
        {
            if (gvAsistenti.Columns["#"] == null)
            {
                DevExpress.Web.GridViewCommandColumn check = new DevExpress.Web.GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true;
                check.VisibleIndex = 0;
                check.Width = Unit.Percentage(2);
                gvAsistenti.Columns.Add(check);
            }

            //if (gvAsistenti.Columns["Rregullo"] == null)
            //{
            //    GridViewDataTextColumn update = new GridViewDataTextColumn();
            //    update.Caption = "Rregullo";
            //    update.Width = Unit.Percentage(10);
            //    update.Name = "Rregullo";
            //    gvAsistenti.Columns.Add(update);
            //    GridViewDataTextColumn col = gvAsistenti.Columns["Rregullo"] as GridViewDataTextColumn;
            //    col.DataItemTemplate = new MyButtonTemplate("Rregullo");
            //}

            gvAsistenti.Settings.ShowFilterRow = true;
            gvAsistenti.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
            gvAsistenti.Settings.ShowFilterRowMenu = true; gvAsistenti.KeyFieldName = "Id"; gvAsistenti.SettingsBehavior.AllowSelectByRowClick = true;
            gvAsistenti.SettingsBehavior.AllowFocusedRow = true;
        }

        protected void gvAsistenti_CustomColumnDisplayText(object sender,
    DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName != "Objekti") return;
            if (!String.IsNullOrEmpty(e.Value.ToString()))
                e.DisplayText = e.Value.ToString().Replace("\\n", "<br />");
            e.EncodeHtml = false;
        }
    }
}