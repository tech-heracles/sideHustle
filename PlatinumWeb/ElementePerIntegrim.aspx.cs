using DbCore;
using DbCore.DbInventari;
using DevExpress.Web;
using System;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using PlatinumWeb.Templates;
using DbCore.DbAdmin;
using DbCore.IMBUtils.Logging;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class ElementePerIntegrim : MyPageBase
    {
        private int idNdermarrje;
        private int idKonfig;
        private int idPerdoruesi;
        private int idGjuha;
        private int idViti;
        private int idNdermVit;
        private int idKomponente = 3035;
        private const string komponente = "ElementePerIntegrim.aspx";
        private CultureInfo ci;
        private System.Resources.ResourceManager rm;
        private bool eshteMeme;
        string kodi;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                DbCore.clsFunksione.logout(Session, true, "FaqePaautorizuar");
            }
            idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerdoruesi);
            }

            ci = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo(idGjuha);
            rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

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
                hfState.Set("idKomponente", idKomponente);
                hfState.Set("komponente", komponente);
                hfState.Set("idNdermVit", idNdermVit);
                ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 141, "ELINT", rm, ci, idGjuha);
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
                mbushGridenElementeshNgaDB(idNdermarrje);
                konfiguroGrideElementesh(cmbKonfigurimi.Text);
                GridUtil.PercaktoVisibleColumnsAndTypesGridSipasKodKonfigurimi(idNdermarrje, "gvElementePerIntegrim", gvElementePerIntegrim, cmbKonfigurimi.Text, idKomponente.ToString(), idGjuha);

            }
            else
            {
                idNdermarrje = (int)hfState.Get("idNdermarrje");
                idPerdoruesi = (int)hfState.Get("idPerdoruesi");
                idGjuha = (int)hfState.Get("idGjuha");
                idViti = (int)hfState.Get("idViti");
                idNdermVit = (int)hfState.Get("idNdermVit");
                idKonfig = int.Parse(cmbKonfigurimi.Value.ToString());
                mbushGridenEElementeveNgaSession();
                konfiguroGrideElementesh(cmbKonfigurimi.Text);
            }

            
            MbushComboLloji();
            gvElementePerIntegrim.PercaktoTitlePanel(this, MenuInfo, pnlMesazhi, hfState, idPerdoruesi, idNdermarrje, idViti, idGjuha, idKonfig, komponente, rm, ci);
            percaktoTemplateMenu();

        }

        private void mbushGridenElementeshNgaDB(int idNdermarrje)
        {
            colElementePerIntegrim col = new colElementePerIntegrim(idNdermarrje);
            gvElementePerIntegrim.DataSource = col;
            gvElementePerIntegrim.DataBind();

            mySessionObjects.ruajObjectNeSesion(Session, col, "gvElementePerIntegrim");
        }

        public void ZevendesoCmbLloji()
        {
            GridViewDataComboBoxColumn colNew = new GridViewDataComboBoxColumn();
            if (typeof(GridViewDataComboBoxColumn) != gvElementePerIntegrim.Columns["Lloji"].GetType())
            {
                gvElementePerIntegrim.Columns.Remove(gvElementePerIntegrim.Columns["Lloji"]);
               
                colNew.FieldName = "Lloji";

                colNew.Caption = "Lloji";
                colLlojElementiPerIntegrim col = new colLlojElementiPerIntegrim();

                mySessionObjects.ruajObjectNeSesion(Session, col, "cmbLloji");
                colNew.PropertiesComboBox.DataSource = col;
                colNew.PropertiesComboBox.TextField = "Emertimi" + (ci.Name == "en-US" ? "_eng" : "");
                colNew.PropertiesComboBox.ValueField = "Id";
                colNew.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                gvElementePerIntegrim.Columns.Add(colNew);
            }
           
        }

        public void MbushComboLloji()
        {

            cmbLloji.DataSource = new colLlojElementiPerIntegrim();
            cmbLloji.TextField = "Emertimi";
            cmbLloji.ValueField = "Id";
            cmbLloji.DataBind();
            cmbLloji.SelectedItem = cmbLloji.Items[0];
            cmbLloji.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        }

        protected void gvElementePerIntegrim_DataBound(object sender, EventArgs e)
        {
            gvElementePerIntegrim.KeyFieldName = "IdElementi";
            if (this.gvElementePerIntegrim.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per
                //perzgjidh
                DevExpress.Web.GridViewCommandColumn check = new DevExpress.Web.GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true;
                check.Width = Unit.Percentage(2);
                check.VisibleIndex = 0;
                gvElementePerIntegrim.SettingsBehavior.AllowSelectByRowClick = true;
                gvElementePerIntegrim.Columns.Add(check);
            }

            ZevendesoCmbLloji();
        }



        protected void ButtonOk_Click(object sender, EventArgs e)
        {
            clsMesazh mesazhi = new clsMesazh();
            string kodeTePafshire = "";
            //mund te behet funksion generic
            try
            {
                if (gvElementePerIntegrim.Selection.Count < 1)
                {
                    mesazhi = new clsMesazh(false, rm.GetString("msgZgjidhniTePaktenNjeElement", ci));
                }
                else
                {
                    object tmp = mySessionObjects.merrObjectNgaSesioni(Session, "gvElementePerIntegrim");
                    colElementePerIntegrim col = (tmp as colElementePerIntegrim) ?? new colElementePerIntegrim(idNdermarrje);
                    var selectedIDs = Array.ConvertAll(gvElementePerIntegrim.GetSelectedFieldValues("IdElementi").ToArray(), Convert.ToInt32);
                   
                    foreach (int id in selectedIDs)
                    {
                        
                        
                        if (clsElementePerIntegrim.eshteElementILidhur(id))
                        {
                            string kodi = col.Find(x => x.IdElementi == id).Kodi;
                            kodeTePafshire = !String.IsNullOrEmpty(kodeTePafshire) ? $"{kodeTePafshire}, {kodi}" : kodi;
                            
                            continue;
                        }
                        mesazhi = clsElementePerIntegrim.FshiUpdateStatusDok(id, idPerdoruesi);
                        if (mesazhi.Status)
                            col.Remove(col.Find(x => x.IdElementi == id));
                        else
                            break;
                    }
                    if (mesazhi.Status)
                    {
                        mySessionObjects.ruajObjectNeSesion(Session, col, "gvElementePerIntegrim");
                    }
                }

                if (mesazhi.Status && String.IsNullOrEmpty(kodeTePafshire))
                {
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("msgFshirjaMeSukses", ci), pnlMesazhi);
                    hfStatusVeprimi.Value = "true";
                    mbushGridenEElementeveNgaSession();
                }
                else if (!String.IsNullOrEmpty(kodeTePafshire))
                {
                    
                    string njejesShumes = kodeTePafshire.Contains(",") ? "Shumes" : "Njejes";
                    string mesazhErrori = rm.GetString($"msgElementiMeKod{njejesShumes} ", ci) + kodeTePafshire + " " + rm.GetString($"msgBlerjeShitjeNukFshihet{njejesShumes}", ci);
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhErrori, pnlMesazhi);
                    hfStatusVeprimi.Value = "false";
                    mbushGridenEElementeveNgaSession();
                }
                else
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhi.PershkrimMesazhi, pnlMesazhi);
                    hfStatusVeprimi.Value = "false";
                }
            }
            catch (Exception ex)
            {
                ImbLogger.Error(ex);
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgGabimGjateFshirjes", ci), pnlMesazhi);
                hfStatusVeprimi.Value = "false";
            }
        }

        /// <summary>
        /// merr ds e grides nga sessioni
        /// </summary>
        public void mbushGridenEElementeveNgaSession()
        {
            object tmp = mySessionObjects.merrObjectNgaSesioni(Session, "gvElementePerIntegrim");
            colElementePerIntegrim col = (tmp as colElementePerIntegrim) ?? new colElementePerIntegrim(idNdermarrje);
            gvElementePerIntegrim.DataSource = tmp;
            gvElementePerIntegrim.DataBind();
        }

        private void konfiguroGrideElementesh(string kodi)
        {
            GridUtil.konfigGrideListeEMadhePaTheme(gvElementePerIntegrim, "IdElementi");
        }

        protected void gvElementePerIntegrim_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            gvElementePerIntegrim.Selection.UnselectAll();
        }

        private void percaktoTemplateMenu()
        {
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdoruesi, idNdermarrje, ASPxMenu1, komponente, this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, false, true, false, eshteMeme, true);
        }

        public void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
        }

        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
        }


        protected void ruajElementPerIntegrim()
        {
            clsMesazh mesazhi = null;
            try
            {
                clsTeDrejtaRoli teDrejta = new clsTeDrejtaRoli();
                teDrejta.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, komponente);
                bool eshteShtim = hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim";
                object tmp = mySessionObjects.merrObjectNgaSesioni(Session, "gvElementePerIntegrim");
                colElementePerIntegrim col = (tmp as colElementePerIntegrim) ?? new colElementePerIntegrim(idNdermarrje);

                clsElementePerIntegrim elementi = KrijoElementPerIntegrim(eshteShtim);

                hfStatusVeprimi.Value = "false";
                if (eshteShtim && !teDrejta.DShtim)
                {
                    mesazhi = new clsMesazh(false);
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("mesazhRaportNukKeniTeDrejta", ci), pnlMesazhi);
                    return;
                }
                if (eshteShtim && teDrejta.DShtim)
                {
                    if (clsElementePerIntegrim.ekzistonElementMeKeteKod(elementi.Kodi, idNdermarrje))
                    {
                        mesazhi = new clsMesazh(false);
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgEkzistonNjeElementNeGride", ci), pnlMesazhi);
                    }
                    else
                        mesazhi = elementi.Ruaj();
                }
                if (!eshteShtim && !teDrejta.DMod) //modifikim
                {
                    mesazhi = new clsMesazh(false);
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("mesazhRaportNukKeniTeDrejta", ci), pnlMesazhi);
                    return;
                }
                else if(!eshteShtim && teDrejta.DMod)
                {
                    elementi.IdModifikuesi = idPerdoruesi;
                    mesazhi = elementi.Modifiko();
                }
                    
                if (mesazhi.Status)
                {
                    hfStatusVeprimi.Value = "true";

                    if (eshteShtim)
                    {
                        clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("labelRaportMesazhRuajtjaPerfundoiSukses", ci), pnlMesazhi);
                         col.Add(elementi);
                    }
                    else
                    {
                        clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("msgModifikimiMeSukses", ci), pnlMesazhi);
                        int indexi = col.FindIndex(x => x.IdElementi == elementi.IdElementi);
                        col[indexi] = elementi;
                    }
                    mySessionObjects.ruajObjectNeSesion(Session, col, "gvElementePerIntegrim");
                    mbushGridenEElementeveNgaSession();
                    pastroFusha();
                    gvElementePerIntegrim.Selection.UnselectAll();



                }
               
            }
            catch (Exception err)
            {
                ImbLogger.Error(err);
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgGabimRuajtje", ci), pnlMesazhi);
                throw;
            }


        }
        

        private void pastroFusha()
        {
            txtKodi.Text = "";
            txtEmertimi.Text = "";
            memoShenime.Text = "";
            cbAktiv.Checked = false;
            cmbLloji.SelectedItem = cmbLloji.Items[0];
            deDateRegjistrimi.Date = DateTime.Now;

        }

        private clsElementePerIntegrim KrijoElementPerIntegrim(bool eshteShtim)
        {
            clsElementePerIntegrim element = null;

            if (!eshteShtim)
            {
                int idElementi = Convert.ToInt32(gvElementePerIntegrim.GetRowValues(gvElementePerIntegrim.FocusedRowIndex, "IdElementi"));
                element = new clsElementePerIntegrim(idElementi);
                element.Emertimi = txtEmertimi.Text;
                element.Aktiv = cbAktiv.Checked;
                element.Shenime = memoShenime.Text;
            }
            else
            {
                element = new clsElementePerIntegrim();
                element.IdStatusDok = 1;
                element.Kodi = txtKodi.Text;
                element.Emertimi = txtEmertimi.Text;
                element.Lloji = Convert.ToInt32(cmbLloji.SelectedItem.Value.ToString());
                element.Aktiv = cbAktiv.Checked;
                element.DateRegjistrimi = deDateRegjistrimi.Date;
                element.Shenime = memoShenime.Text;
            }

            element.IdNdermarje = idNdermarrje;
            element.IdPerdoruesi = idPerdoruesi;
            element.DtKrijimi = DateTime.Now;
            


            return element;
        }



        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {
            if (e.Item.Name == "Ruaj" || e.Item.Name == "Modifiko" || e.Item.Name == "Klono")
            {
                Page.Validate("entries");
                ruajElementPerIntegrim();
            }
        }


    }
}