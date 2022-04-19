using DbCore;
using DbCore.DbAdmin;
using DbCore.DbCRM;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Resources;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using DbCore.IMBUtils.Extensions;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    public partial class CRMLidhAnkete : MyPageBase
    {
        private int idNdermarrje;
        private int idPerdoruesi;
        private int idGjuha;
        private int idViti;
        private bool eshteMeme;
        private clsKomponente komponente;
        private CultureInfo ci;
        private ResourceManager rm;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                DbCore.clsFunksione.logout(Session,true,"FaqePaautorizuar");
            }
            int idPerd = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);

            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerd);
            }
            rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (!Page.IsPostBack)
            {
                idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
                komponente = new clsKomponente("CRMLidhAnkete.aspx");
                idGjuha = mySessionObjects.ktheGjuhe(Session);
                eshteMeme = mySessionObjects.merrEshteMemeSesioni(Session);

                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, ":");
                if (Request.QueryString["ruaj"] == "ok")
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["mesazhRuajtjeMeSukses"], pnlMesazhi);

                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, komponente.EmriKomponente);
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();

                hfState["idGjuha"] = idGjuha;
                hfState["idNdermarrje"] = idNdermarrje;
                hfState["idPerdoruesi"] = idPerdoruesi;
                hfState["idViti"] = idViti;
                hfState["komponente"] = (new JavaScriptSerializer()).Serialize(komponente);
                hfState["eshteMeme"] = eshteMeme;

                hfState["msgZgjdhniNjeNgaElementetEListes"] = rm.GetString("msgZgjdhniNjeNgaElementetEListes", ci);
                
                lblUserEmri.Text = DbCore.mySessionObjects.ktheEmerPerdorues(Session);
                mySessionObjects.RuajNeSession<Dictionary<int, bool>>(Session, new Dictionary<int, bool>(), Constants.VLEFSHMERIA);
            }
            else
            {
                idGjuha = (int)hfState["idGjuha"];
                idNdermarrje = (int)hfState["idNdermarrje"];
                idPerdoruesi = (int)hfState["idPerdoruesi"];
                idViti = (int)hfState["idViti"];
                komponente = (new JavaScriptSerializer()).Deserialize<clsKomponente>(hfState["komponente"].ToString());
                eshteMeme = (bool)hfState["eshteMeme"];
            }
            ci = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo(idGjuha);
            DbCore.mySessionObjects.ruajMesazhNeSesion(Session, ":");

            mbushGridKlientPerCRMNgaDB();
            hfStatusi.Value = "false";
            konfiguroGridenEKlienteve();
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdoruesi, idNdermarrje, ASPxMenu1, komponente.EmriKomponente, this, MenuInfo, hfRuaj.Value == "Ruaj", true, false, eshteMeme, true);

            gvKlienti.PercaktoTitlePanel( this, MenuInfo, pnlMesazhi, hfState, idPerdoruesi, idNdermarrje, idViti, idGjuha,1, komponente.EmriKomponente, komponente.IdKomponente, "IdKlientFurnitor", rm, ci,true);
        }

        /// <summary>
        /// Metoda per te thirrur veprimet e menuse kur shtypen butonat 
        /// </summary>
        /// <param name="source"> derguesi </param>
        /// <param name="e">      parametrat </param>
        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {
            switch (e.Item.Name)
            {
                case "HiqAnketa":
                    hiqLidhjet(hiqAnketa: true);
                    break;

                case "HiqDetyra":
                    hiqLidhjet(hiqDetyra: true);
                    break;

                case "Pastro":
                    hiqLidhjet(hiqAnketa: true,hiqDetyra:true);
                    break;

    
            }

      
        }
        private void hiqLidhjet(bool hiqAnketa = false, bool hiqDetyra = false)
        {

            if (gvKlienti.Selection.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Zgjidhni nje klient!", pnlMesazhi);
                return;
            }

            List<object> rreshtat = gvKlienti.GetSelectedFieldValues("IdKlientFurnitor");
            int[] klientIds = Array.ConvertAll(rreshtat.ToArray(), Convert.ToInt32);

            List<string> klientTeHequr = new List<string>();
            List<string> klientTePaHequr = new List<string>();

            if (hiqAnketa)
                HiqAnketa(idPerdoruesi, klientIds, ref klientTeHequr, ref klientTePaHequr);
            if (hiqDetyra)
                hiqDetyre(idPerdoruesi, klientIds, ref klientTeHequr, ref klientTePaHequr);

            if (klientTeHequr.Count == 0 && klientTePaHequr.Count == 0)
                return;

            gvKlienti.JSProperties["cpHequrLidhje"] = true;
            

            klientTePaHequr.RemoveAll(x=>klientTeHequr.Contains(x));
            hfStatusi.Value = "true";
            if (klientTePaHequr.Count == 0)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "Lidhja u hoq me sukses per te gjithe klientet qe kane anketa dhe detyra te panisura !", pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Lidhja nuk u hoq per klientet:" + String.Join(";", klientTePaHequr) + (klientTeHequr.Count > 0 ? " dhe u hoq per klientet:" + String.Join(";", klientTeHequr) : "") + "!", pnlMesazhi);

        }
        protected void gvKlienti_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            //if (e.RowType == GridViewRowType.Data)
            //{
            //    GridViewDataTextColumn col0 = ((ASPxGridView)sender).Columns["Konfiguro"] as GridViewDataTextColumn;
            //    ASPxButton btn0 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col0, "btn") as ASPxButton;

            //    if (btn0 != null)
            //    {
            //        btn0.ClientInstanceName = "btnKonfiguro" + e.VisibleIndex;
            //        btn0.ClientSideEvents.Click = String.Format("function(s,e){{ClickKonfiguro({0});}}", e.KeyValue);
            //    }
            //}
        }

        /// <summary>
        /// Thirret sa here qe behet Callback, dhe ben edhe njeher lidhjen me datasource-in 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">     </param>
        protected void gvKlienti_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            ///e tepert ketu

            //idGjuha =(int)hfState["idGjuha"];
            //if (e.CallbackName == "APPLYFILTER" && e.Args[0] == "")
            //{
            //    DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            //    ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            //    cmbFiltra.Text = "";
            //}
            //ci = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo(idGjuha);
            //rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            //DbCore.clsFunksione.ToolTipButonaveMbiGride(gvKlienti, ci, rm);
        }

        /// <summary>
        /// Percaktojme karakteristika te ndryshme te grides si : fusha celes apo filtrat 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">     </param>
        protected void gvKlienti_DataBound(object sender, EventArgs e)
        {
            if (gvKlienti.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per
                //perzgjidh
                GridViewCommandColumn check = new GridViewCommandColumn("#") { ShowSelectCheckbox = true, Width = System.Web.UI.WebControls.Unit.Percentage(2) };
                gvKlienti.Columns.Add(check);
                gvKlienti.KeyFieldName = "IdKlientFurnitor";
                //Afishon rreshtin qe do sherbej per filtrim
                gvKlienti.Settings.ShowFilterRow = true;
                gvKlienti.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                gvKlienti.Settings.ShowFilterRowMenu = true;
                gvKlienti.SettingsBehavior.AllowSelectByRowClick = true;
                gvKlienti.SettingsBehavior.AllowFocusedRow = true;
            }
        }

        /// <summary>
        /// Ruan disa karakteristika te grides 
        /// </summary>
        /// <param name="sender"> dergues </param>
        /// <param name="e">      argumentat </param>
        protected void gvKlienti_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            //NUK jane te nevojshme sepse tashme gridat kane suport per keyboard
            //e.Properties["cpPageIndex"] = gvKlienti.PageIndex;
            //e.Properties["cpPageRow"] = gvKlienti.SettingsPager.PageSize;
            //e.Properties["cpRowCount"] = gvKlienti.VisibleRowCount;
        }

        /// <summary>
        /// Ne rastin kur kolonat e grides jane combo, behet null kriteri i filtrimit kur zgjidhet
        /// vlera bosh e combos
        /// </summary>
        /// <param name="sender"> Derguesi </param>
        /// <param name="e">      Lista e paramterave </param>
        protected void gvKlienti_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
        }

        /// <summary>
        ///Sherben per te vendosur filtra tek header-i i grides
        /// </summary>
        /// <param name="sender">Derguesi</param>
        /// <param name="e">Lista e parametrave</param>
        protected void gvKlienti_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            if (e.Column.FieldName == "Kodi" || e.Column.FieldName == "Pershkrimi")
            {
                e.Values.Clear();
                e.AddValue("(Te gjithe)", string.Empty, "true");
                e.AddValue("Nga A-D ", string.Empty, String.Format("{0}>'A     ' and {0} <'DDDDDDD'", e.Column.FieldName));
                e.AddValue("Nga D-G ", string.Empty, String.Format("{0}>'D     ' and {0}<'GGGGGGG'", e.Column.FieldName));
                e.AddValue("Nga H-K ", string.Empty, String.Format("{0}>'H     ' and {0}<'KKKKKKK'", e.Column.FieldName));
                e.AddValue("Nga L-O ", string.Empty, String.Format("{0}>'L     ' and {0}  <'OOOOOOO'", e.Column.FieldName));
                e.AddValue("Nga P-S ", string.Empty, String.Format("{0}>'P     ' and {0}<'SSSSSSS'", e.Column.FieldName));
                e.AddValue("Nga T-W ", string.Empty, String.Format("{0}>'T     ' and {0}<'WWWWWWW'", e.Column.FieldName));
                e.AddValue("Nga X-Z ", string.Empty, String.Format("{0}>'X     ' and {0}<'ZZZZZZZ'", e.Column.FieldName));
            }
            else
            {
                e.Values.Clear();
                e.AddValue("(Te gjithe)", string.Empty, "true");
            }
        }

        protected void gvKlienti_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string idkomponente = "";
            string kodkonfigurimi = "";
            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                kodkonfigurimi = arr[1];
                idkomponente = arr[0];
                if (arr[2] == "")
                    gvKlienti.FilterExpression = "";
                else
                {
                    GridUtil.AplikoFilter(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), gvKlienti, arr[2], "gvKlienti", "CRMLidhAnkete.aspx", 1);

                    //clsFiltraGrida filtra = new clsFiltraGrida();
                    ////filtra.mbushFiltraGridaSipasFiltraKodi(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    //DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhePerdoruesi(Session), "gvKlienti", "CRMLidhAnkete.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    //filtra.mbushFilterPerGrideSipasKodit(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
                    ////DbCore.DbAdmin.clsFiltraGrida filtra = dbAdmin.merrFiltraGridaSipasFiltraKodi(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    //if (filtra.FiltraKodi != null)
                    //{
                    //    gvKlienti.FilterExpression = filtra.FiltraVlera;
                    //    if (filtra.DrejtimRenditje == true)
                    //        gvKlienti.SortBy(gvKlienti.Columns[filtra.KoloneRenditje], DevExpress.Data.ColumnSortOrder.Ascending);
                    //    else
                    //        gvKlienti.SortBy(gvKlienti.Columns[filtra.KoloneRenditje], DevExpress.Data.ColumnSortOrder.Descending);                        
                    //    //hfStatusi.Value = "true";
                    //}
                    ////else hfStatusi.Value = "false";
                }
            }
            else if (arr.Length == 2)//nese eshte zgjedhur nje konfigurim tek combo e konfigurimeve
            {
                kodkonfigurimi = arr[1];
                idkomponente = arr[0];
            }
            else
            {
                idkomponente = e.Parameters;
            }
            gvKlienti.Selection.UnselectAll();
        }

        /// <summary>
        /// ky event ndodh sa here behet expand nje rresht i grides se klienteve,merr id e klientit
        /// dhe mbush griden me anketa dhe detyra
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">     </param>
        protected void gvLidhjet_BeforePerformDataSelect(object sender, EventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            bool vetemTeVlefshme = true;
            int klientID = Convert.ToInt32(grid.GetMasterRowKeyValue());
            var vlefshmeria = mySessionObjects.MerrNgaSession<Dictionary<int, bool>>(Session, Constants.VLEFSHMERIA);
            if (vlefshmeria.ContainsKey(klientID))
                vetemTeVlefshme = vlefshmeria[klientID];
            else
            {
                vlefshmeria[klientID] = true;
                mySessionObjects.RuajNeSession<Dictionary<int, bool>>(Session, vlefshmeria, Constants.VLEFSHMERIA);
            }
            grid.JSProperties["cpID"] = klientID;
            grid.JSProperties["cpVlefshmeria"] = vetemTeVlefshme;
            grid.DataSource = colDetyreKlient.merrDetyratDheAnketat(klientID, idNdermarrje, vetemTeVlefshme);
        }

        protected void gvLidhjet_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            var vlefshmeria = mySessionObjects.MerrNgaSession<Dictionary<int, bool>>(Session, Constants.VLEFSHMERIA);
            vlefshmeria[Convert.ToInt32(grid.GetMasterRowKeyValue())] = Convert.ToBoolean(e.Parameters);
            mySessionObjects.RuajNeSession<Dictionary<int, bool>>(Session, vlefshmeria, Constants.VLEFSHMERIA);
            grid.DataSource = colDetyreKlient.merrDetyratDheAnketat(Convert.ToInt32(grid.GetMasterRowKeyValue()), idNdermarrje, Convert.ToBoolean(e.Parameters));
            grid.DataBind();

        }

        protected void gvLidhjet_DataBound(object sender, EventArgs e)
        {
            var grid = sender as ASPxGridView;
            grid.Columns["Pershkrimi"].Width = Unit.Percentage(70);
            grid.Columns["DtFillimi"].Width = Unit.Percentage(10);
            grid.Columns["DtMbarimi"].Width = Unit.Percentage(10);
            grid.Columns["Lloji"].Width = Unit.Percentage(10);
            grid.Columns["Lloji"].VisibleIndex = 1;
        }

        /// <summary>
        /// Merr te dhenat e info artikulli nga DB per griden gvKlienti 
        /// </summary>
        private void mbushGridKlientPerCRMNgaDB()
        {
            DataTable dt = DbCore.DbKontabiliteti.colKlienteFurnitore.ktheKFNdermarrjesAndAutorizimeDTPerCrm(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            DbCore.mySessionObjects.ruajGrideNeSession(Session, dt);
            gvKlienti.DataSource = dt;
            gvKlienti.DataBind();
            dt.Dispose();
        }

        private void HiqAnketa(int idPerdoruesi,int[] klientIds,ref List<string> klientTeHequr, ref List<string> klientTePaHequr)
        {
           
            for (int i = 0; i < klientIds.Length; i++)
            {
                DbCore.DbKontabiliteti.clsKlientFurnitor kf = new DbCore.DbKontabiliteti.clsKlientFurnitor(klientIds[i]);
                DbCore.clsMesazh mesazh = DbCore.DbCRM.clsKlientAnketa.fshiSipasKlientAktive(kf.IdKlientFurnitor, idPerdoruesi);
                if (mesazh.Status)
                    klientTeHequr.AddIfNotExists(kf.KodKlientFurnitor);
                else
                    klientTePaHequr.AddIfNotExists(kf.KodKlientFurnitor);
            }
        }

        private void hiqDetyre(int idPerdoruesi, int[] klientIds, ref List<string> klientTeHequr, ref List<string> klientTePaHequr)
        {
                    
                for (int i = 0; i < klientIds.Length; i++)
                {
                    DbCore.DbKontabiliteti.clsKlientFurnitor kf = new DbCore.DbKontabiliteti.clsKlientFurnitor(klientIds[i]);
                    DbCore.clsMesazh mesazh = DbCore.DbCRM.clsDetyreKlient.hiqDetyraTePaNisura(kf.IdKlientFurnitor,idPerdoruesi);
                    if (mesazh.Status)
                        klientTeHequr.AddIfNotExists(kf.KodKlientFurnitor);
                    else
                        klientTePaHequr.AddIfNotExists(kf.KodKlientFurnitor);
                } 
        }

        /// <summary>
        /// Konfiguron griden sipas kodit te konfigurimit dhe id se komponentes 
        /// </summary>
        /// <param name="kodKonfigurimi"> Kodi konfigurmit </param>
        /// <param name="idKomponente">   Id e komponentes </param>
        private void konfiguroGridenEKlienteve()
        {
            // shtoButtonKonfiguro(); 
          if(!IsPostBack) GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "gvKlienti", gvKlienti,"",komponente.IdKomponente.ToString(),idGjuha,true);
            gvKlienti.EnableRowsCache = true;//meqe ne kete rast nuk ndryshojne te dhenat
            GridUtil.konfigGrideListeEMadhePaTheme(gvKlienti, "IdKlientFurnitor", false);
            GridUtil.PercaktoSettings(gvKlienti, Session);
            gvKlienti.Columns["#"].VisibleIndex = 0;
            gvKlienti.SettingsPager.PageSize = 20;
            KonfigurimComboGride.ShtoProspekt(gvKlienti, rm, ci, "Statusi");
        }
        

    }
}