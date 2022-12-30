using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using PlatinumWeb.Templates;
using System.Globalization;
using System.Resources;
using System.Collections;
using DbCore.DbListPagesat;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class KonfigurimListOrari : MyPageBase
    {
       
       
        /// <summary>
        /// metoda pre init per vendosjen e themes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <summary>
        /// metoda load per marjen e te dhenave dhe konfigurimet kur lodohet faqja
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                Response.Redirect(DbCore.IMBUtils.Paths.defaultLoginPath);
                return;
            }
            int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);

            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerdoruesi);
                return;
            }

            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            mbushHiddenFieldMePerkthime(ci, rm);
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
            AspxWebControlUtils.perkthePopUp(popFshi, rm.GetString("labelKujdes", ci), lblMsgbox, rm.GetString("labelAdministrimiMsgJeniSigurt", ci), ButtonCancel, rm.GetString("labelAnullo", ci));

            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvKonfigurimi", 1, "KonfigurimListOrari.aspx");
            if (!IsPostBack)
            {

                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, ":");
                if (Request.QueryString["ruaj"] == "ok")
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("msgRuajtjeMeSukses", ci) ,pnlMesazhi);
                konfiguroVleraFillestare();
                konfiguroGridat();
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "KonfigurimListOrari.aspx");
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
            }
            DbCore.mySessionObjects.ruajMesazhNeSesion(Session, ":");
            konfiguroVleraFillestare();
            konfiguroGridat();
            gvKonfigurimi.Columns["#"].VisibleIndex = 0;
        }


        private void mbushHiddenFieldMePerkthime(CultureInfo ci, ResourceManager rm)
        {
            hfState.Set("msgKoeficientiDuhetNumer", rm.GetString("msgKoeficientiDuhetNumer", ci));
            hfState.Set("msgNukKeniTeDrejta", rm.GetString("msgNukKeniTeDrejta", ci));
        }
        /// <summary>
        /// mbush griden me te dhena nga DB
        /// </summary>

        private void konfiguroVleraFillestare()
        {
            konfigVleraFillestarePerGride(gvKonfigurimi);

        }
        /// <summary>
        /// mbush griden me te dhena nga db
        /// </summary>
        /// <param name="grida"></param>
        private void konfigVleraFillestarePerGride(ASPxGridView grida)
        {
            DbCore.DbListPagesat.colKokaKonfigListOrari col = new DbCore.DbListPagesat.colKokaKonfigListOrari();
            col.mbushGjitheKonfigurimeListOrariSipasNdermarrjes(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheGjuhe(Session));
            grida.DataSource = col;
            grida.DataBind();
        }
        /// <summary>
        /// konfiguron griden sipas konfigurimit
        /// </summary>
        private void konfiguroGridat()
        {
            konfiguroGride(gvKonfigurimi);

        }
        /// <summary>
        /// konfiguron griden sipas konfigurimit
        /// </summary>
        /// <param name="grida"></param>
        private void konfiguroGride(ASPxGridView grida)
        {
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            percaktoTamplatePrindi(grida);
            GridUtil.percaktoVisibleColumnsMeWidth(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), grida, "gvKonfigurimi", "KonfigurimListOrari.aspx");
            GridUtil.konfiguroGrideListeEvogelPaTheme(grida, "IdKoka");
        }
                                
        /// <summary>
        /// percakton tipe te vecanta te kolonave
        /// </summary>
        /// <param name="grida"></param>
        private void percaktoTamplatePrindi(ASPxGridView grida)
        {
            GridViewDataTextColumn colk = grida.Columns["Koeficienti"] as GridViewDataTextColumn;
            colk.PropertiesTextEdit.ValidationSettings.EnableCustomValidation=true;
            colk.PropertiesTextEdit.ClientSideEvents.Validation="function (s,e){validate(s,e);}";///perdoret per te validuar nje fushe sipas menyres tone dhe jo te devexpresit

            GridViewDataColumn col = grida.Columns["DiteJave"] as GridViewDataColumn;
            col.EditItemTemplate = new MyListboxTemplate();
          GridViewDataColumn col1 = grida.Columns["OreFillimi"] as GridViewDataColumn;
            
            col1.EditItemTemplate = new MySpinTemplate();
            GridViewDataColumn col2 = grida.Columns["OreMbarimi"] as GridViewDataColumn;
            col2.EditItemTemplate = new MySpinTemplate();
        }
        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        /// <param name="idViti"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNdermarrje"></param>
        private void percaktoTemplateMenu(ASPxMenu aSPxMenu1, int idViti, int idPerdorues, int idNdermarrje)
        {
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, "KonfigurimListOrari.aspx", this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, hfRuaj.Value == "Ruaj" ? true : false, true, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
        }

        /// <summary>
        /// mbush menune me te dhena
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));

        }
        /// <summary>
        /// kur perdoruesi konfirmon fshirjen ben fshirjen e objektit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonOk_Click2(object sender, EventArgs e)
        {

            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            ASPxGridView grida = gvKonfigurimi;

            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int a = grida.FocusedRowIndex;
            grida.Selection.SelectRow(a);
            List<object> rreshtat = grida.GetSelectedFieldValues("IdKoka");

            foreach (int id in rreshtat)
            {
                DbCore.DbListPagesat.clsKokaKonfigListOrari koka = new DbCore.DbListPagesat.clsKokaKonfigListOrari(id);

                koka.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.clsMesazh mes = koka.fshi();
                if (mes.Status)
                {
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("msgFshirjaPerfundoiMeSukses", ci), pnlMesazhi);
                    ASPxMenu1.Items.FindByName("Ruaj").ClientVisible = false;
                }
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mes.PershkrimMesazhi, pnlMesazhi);
          
                konfigVleraFillestarePerGride(grida);
            }
            pnlGrida.Update();
        }
        /// <summary>
        /// ruan filtrin e zgjedhur nga perdoruesi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {

            ASPxGridView grida = gvKonfigurimi;

            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;

            DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida();
            filtri.FiltraKodi = cmbFiltra.Text;
            filtri.FiltraShenime = cmbFiltra.Text;
            filtri.FiltraUniversal = false;

            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvKonfigurimi", "KonfigurimListOrari.aspx", idNdermarrje);
            filtri.GridaKokaId = koka.IdGridaKoka;


            filtri.FiltraVlera = grida.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("OreFillimi", grida);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = grida.GetSortedColumns();
            //if (kolona.Count > 0)
            //{
            //    filtri.KoloneRenditje = kolona[0].FieldName;
            //    if (kolona[0].SortOrder == DevExpress.Data.ColumnSortOrder.Ascending)
            //        filtri.DrejtimRenditje = true;
            //    else
            //        filtri.DrejtimRenditje = false;
            //}
            //else
            //{
            //    filtri.KoloneRenditje = "OreFillimi";
            //    filtri.DrejtimRenditje = true;
            //}


            filtri.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            filtri.IdNdermarje = idNdermarrje;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();

            filtri.IdStatusDok = 1;
            mesazh = filtri.ruaj();
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvKonfigurimi", 1, "KonfigurimListOrari.aspx");
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);

            if (mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                ASPxMenu1.Items.FindByName("Ruaj").ClientVisible = false;
            }
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            cmbFiltra.Text = "";
        }
        /// <summary>
        /// fshin filtrin e zgjedhur nga perdoruesi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje

            ASPxGridView grida = gvKonfigurimi;

            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;

            DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            //filtra.mbushFiltraGridaSipasFiltraEmri(cmbFiltra.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvKonfigurimi", "KonfigurimListOrari.aspx", idNdermarrje);
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra();
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvKonfigurimi", 1, "KonfigurimListOrari.aspx");
                percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);

                if (mesazh.Status)
                {
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                    ASPxMenu1.Items.FindByName("Ruaj").ClientVisible = false;
                }
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                grida.FilterExpression = String.Empty;
            }
        }
        /// <summary>
        /// kontrollon nese intervalet priten me njera tjetren
        /// </summary>
        /// <param name="idNdermarrje">id e ndermarjes</param>
        /// <param name="koka">koka qe do ruhet</param>
        /// <param name="idkokamod">id e kokes qe po modifikohet . ne rast shtimi vjen 0</param>
        /// <returns> kthen true nqs ka prerje, false ne te kundert</returns>
        private  bool rowInsertingKontrolloPrerje(int idNdermarrje, DbCore.DbListPagesat.clsKokaKonfigListOrari koka, int idkokamod)
        {
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            bool shouldReturn = false;
            DbCore.DbListPagesat.colKokaKonfigListOrari colkoka = new DbCore.DbListPagesat.colKokaKonfigListOrari();
            colkoka.mbushGjitheKonfigurimeListOrariSipasNdermarrjes(idNdermarrje, DbCore.mySessionObjects.ktheGjuhe(Session));
            foreach (DbCore.DbListPagesat.clsKokaKonfigListOrari ko in colkoka)
            {
                if (idkokamod == ko.IdKoka)
                    continue;
                if (DateTime.Parse(ko.OreFillimi) >= DateTime.Parse(koka.OreMbarimi))//koha e re eshte me e vogel se ora e fillimit te konfigurimit ekzistues
                    continue;
                else if (DateTime.Parse(ko.OreMbarimi) <= DateTime.Parse(koka.OreFillimi))//koha e re eshte me e madhe se ora e mbarimit te konfigurimit ekzistues
                    continue;
                else//prn kemi prerje te orareve dhe shohim nqs kemi prerje te diteve
                {

                    DbCore.DbListPagesat.colTrupiKonfigListOrari coltrupi = new DbCore.DbListPagesat.colTrupiKonfigListOrari(ko.IdKoka);
                    foreach (DbCore.DbListPagesat.clsTrupiKonfigListOrari trup in koka.ColTrupi)
                    {
                       
                        if ((coltrupi.Where(x => x.DiteJave == trup.DiteJave)).Count() > 0)//nqs e permban diten atehere kemi prerje oraresh
                        {
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgPrerjeOrareshKonfigurimi", ci), pnlMesazhi);
                            DbCore.mySessionObjects.ruajMesazhNeSesion(Session, rm.GetString("msgPrerjeOrareshKonfigurimi", ci) + ":Red");
                            shouldReturn = true;
                         
                        }
                    }
                }
            }
            return shouldReturn;
        }
        /// <summary>
        /// shton rreshtin e ri ne gride
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            //merr te dhenat e rreshtit te ri te grides
            ASPxGridView gride = sender as ASPxGridView;
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DbCore.DbListPagesat.clsKokaKonfigListOrari koka = new DbCore.DbListPagesat.clsKokaKonfigListOrari();

            koka.OreFillimi = hfOreFillimi.Value.ToString();
            koka.OreMbarimi = hfOreMbarimi.Value.ToString();
            koka.Koeficienti = decimal.Parse(e.NewValues["Koeficienti"].ToString());
            if (e.NewValues["Shenime"] != null)
                koka.Shenime = e.NewValues["Shenime"].ToString();
            else koka.Shenime = "";
            koka.IdNdermarje = idNdermarrje;
            koka.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            koka.IdKrijuesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            koka.IdStatusDok = 1;
            koka.ColTrupi = new DbCore.DbListPagesat.colTrupiKonfigListOrari();
             GridViewDataColumn col2 = gride.Columns["DiteJave"] as GridViewDataColumn; 
            ASPxListBox lstBox2 = ((ASPxGridView)sender).FindEditRowCellTemplateControl(col2, "lbx") as ASPxListBox;
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "KonfigurimListOrari.aspx");
            if (!tedrejtaInfo.DShtim)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukKeniTeDrejta", ci), pnlMesazhi);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, rm.GetString("msgNukKeniTeDrejta", ci) + ":Red");
                e.Cancel = true;
                return;
            }
            DevExpress.Web.SelectedValueCollection b = lstBox2.SelectedValues;
            if (b.Count == 0)
                lstBox2.SelectAll();
            b = lstBox2.SelectedValues;
            for (int i = 0; i < b.Count; i++)
            {
                DbCore.DbListPagesat.clsTrupiKonfigListOrari t = new DbCore.DbListPagesat.clsTrupiKonfigListOrari();
                int idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
                if (idGjuha == 0)
                {
                    t.DiteJave = b[i].ToString();
                    t.DiteJave_ENG = mbushDiteJaveSipasGjuhes(b[i].ToString());
                }
                else
                {
                    t.DiteJave = mbushDiteJaveSipasGjuhes(b[i].ToString());
                    t.DiteJave_ENG = b[i].ToString();
                }
                koka.ColTrupi.Add(t);
            }
            bool shouldReturn = rowInsertingKontrolloPrerje(idNdermarrje, koka,0);
            if (shouldReturn)
            {
                e.Cancel = true;
                return;
            }
            e.Cancel = true;
            gride.CancelEdit();
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
           
            mesazh = koka.ruaj();
            if (!mesazh.Status == true)
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, rm.GetString("msgGabimRuajtje", ci) + ":Red");
            else
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, rm.GetString("msgRuajtjeMeSuksesGreen", ci));

            konfigVleraFillestarePerGride(gride);

            konfiguroGride(gride);
            pnlGrida.Update();

        }
        /// <summary>
        /// modifikon rreshtin ne gride
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            ASPxGridView gride = sender as ASPxGridView;
            String id = e.Keys["IdKoka"].ToString();
            DbCore.clsMesazh m = new DbCore.clsMesazh();
            string OreFillimi = hfOreFillimi.Value.ToString();
            string OreMbarimi = hfOreMbarimi.Value.ToString();
            decimal Koeficienti = decimal.Parse(e.NewValues["Koeficienti"].ToString());
            string Shenime = "";
            if(e.NewValues["Shenime"]!=null) Shenime=e.NewValues["Shenime"].ToString();

            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "KonfigurimListOrari.aspx");
            if (!tedrejtaInfo.DMod)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukKeniTeDrejta", ci), pnlMesazhi);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, rm.GetString("msgNukKeniTeDrejta", ci) + ":Red");
                return;
            }
            DbCore.DbListPagesat.clsKokaKonfigListOrari koka = new DbCore.DbListPagesat.clsKokaKonfigListOrari(int.Parse(id), OreFillimi, OreMbarimi, Shenime, Koeficienti, idPerdoruesi, idPerdoruesi, idNdermarrje, 1);
            koka.ColTrupi = new DbCore.DbListPagesat.colTrupiKonfigListOrari();

            GridViewDataColumn col2 = gride.Columns["DiteJave"] as GridViewDataColumn;
            ASPxListBox lstBox2 = ((ASPxGridView)sender).FindEditRowCellTemplateControl(col2, "lbx") as ASPxListBox;

            DevExpress.Web.SelectedValueCollection b = lstBox2.SelectedValues;

          
            if (b.Count == 0)
                lstBox2.SelectAll();
            b = lstBox2.SelectedValues;
            for (int i = 0; i < b.Count; i++)
            {
                DbCore.DbListPagesat.clsTrupiKonfigListOrari t = new DbCore.DbListPagesat.clsTrupiKonfigListOrari();
                int idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
                if(idGjuha == 0)
                {
                    t.DiteJave = b[i].ToString();
                    t.DiteJave_ENG =mbushDiteJaveSipasGjuhes(b[i].ToString());
                }
               else
                {
                    t.DiteJave = mbushDiteJaveSipasGjuhes(b[i].ToString());
                    t.DiteJave_ENG = b[i].ToString();
                }
                koka.ColTrupi.Add(t);
            }
            bool shouldReturn = rowInsertingKontrolloPrerje(idNdermarrje, koka, int.Parse(id));
            if (shouldReturn)
                return;
            m = koka.modifiko();

            if (m.Status)
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, rm.GetString("msgModifikimiMeSuksesGreen",ci));

            else
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, m.PershkrimMesazhi + ":Red");
            e.Cancel = true;
            gride.CancelEdit();
            konfigVleraFillestarePerGride(gride);
            konfiguroGridat();

        }
        /// <summary>
        /// Validon rreshtin qe po ndryshohet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void rowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {//per tu rregulluar me vone
         //validimi ne jane plotesuar gjithe fushat e detyruara
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            ASPxGridView grida = sender as ASPxGridView;
            if (hfOreFillimi == null || hfOreFillimi.Value.ToString() == "")
            {
                e.RowError = rm.GetString("msgPlotesoniOreFillimi", ci);
                return;
            }
            if (hfOreMbarimi == null || hfOreMbarimi.Value.ToString() == "")
            {
                e.RowError = rm.GetString("msgPlotesoniOreMbarimi", ci);
                return;
            }
            if (e.NewValues["Koeficienti"] == null || e.NewValues["Koeficienti"].ToString() == "")
            {
                e.RowError = rm.GetString("msgPlotesoniKoeficientin", ci); ;
                return;
            }
            else
            {
                try
                {
                    decimal.Parse(e.NewValues["Koeficienti"].ToString());

                }
                catch (Exception ex)
                {
                    string mesazhi = rm.GetString("msgKoeficientiDuhetNumer", ci);
                    NLog.LogManager.GetCurrentClassLogger().Error(mesazhi, ex.Message);
                    e.RowError = mesazhi;
                    return;
                }

            }
            GridViewDataColumn col1 = grida.Columns["DiteJave"] as GridViewDataColumn;
            ASPxListBox lstBox = ((ASPxGridView)sender).FindEditRowCellTemplateControl(col1, "lbx") as ASPxListBox;
            DevExpress.Web.SelectedValueCollection kategoriteSel = lstBox.SelectedValues;
            if (kategoriteSel.Count == 0)
            {
                e.RowError = rm.GetString("msgZgjidhDiteJave", ci); return;
            }

            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);

         
        }

        protected void startRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
        }

        protected void gridat_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {


        }

        protected  string  mbushDiteJaveSipasGjuhes(string dite)
        {

            switch (dite)
            {
                case "E hënë":
                    return  "Monday";
                      break;
                case "E martë":
                    return "Tuesday";
                    break;
                case "E mërkurë":
                    return  "Wednesday";
                    break;
                case "E enjte":
                    return "Thursday";
                    break;
                case "E premte":
                    return  "Friday";
                    break;
                case "E shtunë":
                    return "Saturday";
                    break;
                case "E diel":
                    return "Sunday";
                    break;
                case "Monday":
                    return  "E hënë";
                    break;
                case "Tuesday":
                    return  "E martë";
                    break;
                case "Wednesday":
                    return  "E mërkurë";
                    break;
                case "Thursday":
                    return  "E enjte";
                    break;
                case "Friday":
                    return "E premte";
                    break;
                case "Saturday":
                    return  "E shtunë";
                    break;
                case "Sunday":
                    return "E diel";
                    break;
                default:
                    return "undefined";
                    break;
            }
        }
        /// <summary>
        /// shton metoda specifike per fushat e ndryshuara nga ne
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void HtmlRowCreated(object sender, ASPxGridViewEditFormEventArgs e)
        {
            ASPxGridView grida = sender as ASPxGridView;

            int idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            GridViewDataColumn col2 = grida.Columns["DiteJave"] as GridViewDataColumn;
            ASPxListBox lstBox2 = ((ASPxGridView)sender).FindEditRowCellTemplateControl(col2, "lbx") as ASPxListBox;
            GridViewDataColumn col1 = grida.Columns["OreFillimi"] as GridViewDataColumn;
            ASPxTimeEdit txt1 = ((ASPxGridView)sender).FindEditRowCellTemplateControl(col1, "txtBox") as ASPxTimeEdit;
            GridViewDataColumn col3 = grida.Columns["OreMbarimi"] as GridViewDataColumn;
            ASPxTimeEdit txt2 = ((ASPxGridView)sender).FindEditRowCellTemplateControl(col3, "txtBox") as ASPxTimeEdit;

            if (lstBox2 != null)
            {
                ArrayList dite = new ArrayList();
                if (idGjuha == 0)
                {
                   

                    dite.Add("E hënë");
                    dite.Add("E martë");
                    dite.Add("E mërkurë");
                    dite.Add("E enjte");
                    dite.Add("E premte");
                    dite.Add("E shtunë");
                    dite.Add("E diel");
                }
                else {
                    
                    dite.Add("Monday");
                    dite.Add("Tuesday");
                    dite.Add("Wednesday");
                    dite.Add("Thursday");
                    dite.Add("Friday");
                    dite.Add("Saturday");
                    dite.Add("Sunday");

                }
                lstBox2.Height = 240;
                lstBox2.ClientSideEvents.Init = "function (s,e){InitKat(s,e)}";
                lstBox2.ClientInstanceName = "lbxKategoria";
                lstBox2.DataSource = dite;
                lstBox2.DataBind();

            }
            if (txt1 != null)
            {
                txt1.ClientInstanceName = "txtOreFillimi";
                txt1.ClientSideEvents.DateChanged = "function(s,e){DateChanged(s,e, 'OreFillimi')}";
                txt1.ClientSideEvents.Init = "function(s,e){DateInit(s,e, 'OreFillimi')}";
            }
            if (txt2 != null)
            {
                txt2.ClientInstanceName = "txtOreMbarimi";
                txt2.ClientSideEvents.DateChanged = "function(s,e){DateChanged(s,e, 'OreMbarimi')}";
                txt2.ClientSideEvents.Init = "function(s,e){DateInit(s,e, 'OreMbarimi')}";
            }

        }
        protected void initNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {

        }


        /// <summary>
        /// metoda kur ne bejme callback grides
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridat_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {

            ASPxGridView grida = gvKonfigurimi;

            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                if (arr[2] == "")
                    grida.FilterExpression = "";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    var idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                    //filtra.mbushFiltraGridaSipasFiltraKodi(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(base.Session), "gvKonfigurimi", "KonfigurimListOrari.aspx", idNdermarrje);
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], idNdermarrje, koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        grida.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, grida);

                        konfigVleraFillestarePerGride(grida);

                    }
                }
            }
            konfiguroGride(grida);
        }
        /// <summary>
        /// vendosja e disa variablave per ti aksesuar nga javascripti
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridat_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            ASPxGridView grida = sender as ASPxGridView;
            e.Properties["cpPageIndex"] = grida.PageIndex;
            e.Properties["cpPageRow"] = grida.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = grida.VisibleRowCount;
        }
        /// <summary>
        /// shtim i fushes te gjitha tek filtrat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridat_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            e.Values.Clear();
            e.AddValue("(Te gjithe)", string.Empty, "true");
        }

        protected void gridat_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {

        }

        protected void gvKonfigurimi_DataBound(object sender, EventArgs e)
        {
            if (gvKonfigurimi.Columns["#"] == null)
            {
                GridViewCommandColumn check = new GridViewCommandColumn("#") { ShowSelectCheckbox = true, Width = System.Web.UI.WebControls.Unit.Percentage(2) };
                gvKonfigurimi.Settings.ShowFilterRow = true;
                gvKonfigurimi.Settings.ShowHeaderFilterButton = true;
                gvKonfigurimi.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                gvKonfigurimi.Settings.ShowFilterRowMenu = true;
                gvKonfigurimi.Columns.Add(check);
                gvKonfigurimi.Settings.ShowGroupPanel = true;
                gvKonfigurimi.KeyFieldName = "IdKoka";
                gvKonfigurimi.SettingsBehavior.AllowSelectByRowClick = true;
                gvKonfigurimi.SettingsBehavior.AllowFocusedRow = true;
            }
        }


    }
}