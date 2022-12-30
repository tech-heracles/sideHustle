using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.Web;
using System.Globalization;
using System.Resources;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    public partial class ShtesaPage : MyPageBase
    {
        private const string kaveprime = "Ka veprime me kete shtesa";
        private const string mesazhfshirjegabimi = "Fshirja perfundoi me gabime!";
        private const string gabimEksitimi = "ekziston nje funksion perqindje me kete kod. Ju lutem shenoni nje tjeter!";
        private const string gabimEksitimiFunk = "ekziston nje funksion vlere me kete kod. Ju lutem shenoni nje tjeter!";
        private const string gabimEksitimiPoz = "ekziston nje pozicion me kete kod. Ju lutem shenoni nje tjeter!";
        private const string gabimEksitimiKual = "ekziston nje kualifikim me kete kod. Ju lutem shenoni nje tjeter!";
        private const string gabimEksitimiVesht = "ekziston nje veshtiresi me kete kod. Ju lutem shenoni nje tjeter!";
        private const string gabimEksitimiVjet = "ekziston nje vjetersi me kete kod. Ju lutem shenoni nje tjeter!";
        private int idndermarje, idperdoruesi, idnderviti, idviti;
        /// <summary>
        /// kur faqja lodohet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                Response.Redirect(DbCore.IMBUtils.Paths.defaultLoginPath);
                return;
            }
            idndermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            idperdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            idnderviti = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
            idviti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idperdoruesi);
                return;
            }

            percaktoTemplateMenu(ASPxMenu1, idviti, idperdoruesi, idndermarje);
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idndermarje, "gvShtesaPage", 1, "ShtesaPage.aspx");
            if (!IsPostBack)
            {
                EmrateTabeve();
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, ":");
                if (Request.QueryString["ruaj"] == "ok")
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["mesazhRuajtjeMeSukses"], pnlMesazhi);
                konfiguroVleraFillestare();
                konfiguroGridat();
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idperdoruesi, idndermarje, idviti, "ShtesaPage.aspx");
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);

            }
            DbCore.mySessionObjects.ruajMesazhNeSesion(Session, ":");
            konfiguroVleraFillestare();
        }

        /// <summary>
        /// Vendos emrat e tabeve ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateTabeve()
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            ASPxPageControl1.TabPages[0].Text = rm.GetString("funksioniNePerqindjeTab", cultinf);
            ASPxPageControl1.TabPages[1].Text = rm.GetString("funksioniVlereTab", cultinf);
            ASPxPageControl1.TabPages[2].Text = rm.GetString("pozicioniTab", cultinf);
            ASPxPageControl1.TabPages[3].Text = rm.GetString("kualifikimiTab", cultinf);
            ASPxPageControl1.TabPages[4].Text = rm.GetString("veshtiresiaTab", cultinf);
            ASPxPageControl1.TabPages[5].Text = rm.GetString("vjetersiaTab", cultinf);
        }
        /// <summary>
        /// mbush gridat me te dhena
        /// </summary>
        private void konfiguroVleraFillestare()
        {
            DbCore.DbListPagesat.colShtesaPage col1 = new DbCore.DbListPagesat.colShtesaPage(idndermarje, Convert.ToInt32(DbCore.DbListPagesat.TipeShtesaPage.FunksioniPerqindje));
            gvShtesaPage.DataSource = col1;
            gvShtesaPage.DataBind();

            DbCore.DbListPagesat.colShtesaPage col2 = new DbCore.DbListPagesat.colShtesaPage(idndermarje, Convert.ToInt32(DbCore.DbListPagesat.TipeShtesaPage.FunksioniVlere));
            gvShtesaPage2.DataSource = col2;
            gvShtesaPage2.DataBind();

            DbCore.DbListPagesat.colShtesaPage col3 = new DbCore.DbListPagesat.colShtesaPage(idndermarje, Convert.ToInt32(DbCore.DbListPagesat.TipeShtesaPage.Pozicioni));
            gvShtesaPage3.DataSource = col3;
            gvShtesaPage3.DataBind();

            DbCore.DbListPagesat.colShtesaPage col4 = new DbCore.DbListPagesat.colShtesaPage(idndermarje, Convert.ToInt32(DbCore.DbListPagesat.TipeShtesaPage.Kualifikimi));
            gvShtesaPage4.DataSource = col4;
            gvShtesaPage4.DataBind();

            DbCore.DbListPagesat.colShtesaPage col5 = new DbCore.DbListPagesat.colShtesaPage(idndermarje, Convert.ToInt32(DbCore.DbListPagesat.TipeShtesaPage.Veshtiresia));
            gvShtesaPage5.DataSource = col5;
            gvShtesaPage5.DataBind();

            DbCore.DbListPagesat.colShtesaPage col6 = new DbCore.DbListPagesat.colShtesaPage(idndermarje, Convert.ToInt32(DbCore.DbListPagesat.TipeShtesaPage.Vjetersia));
            gvShtesaPage6.DataSource = col6;
            gvShtesaPage6.DataBind();
        }
        /// <summary>
        /// konfiguron gridat
        /// </summary>
        private void konfiguroGridat()
        {
            konfiguroGride(gvShtesaPage);
            konfiguroGride(gvShtesaPage2);
            konfiguroGride(gvShtesaPage3);
            konfiguroGride(gvShtesaPage4);
            konfiguroGride(gvShtesaPage5);
            konfiguroGride(gvShtesaPage6);
        }
        /// <summary>
        /// konfiguron griden
        /// </summary>
        /// <param name="grida">grida</param>
        private void konfiguroGride(ASPxGridView grida)
        {
            shtoVlere(grida);
            GridUtil.percaktoVisibleColumnsMeWidth(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), grida, "gvShtesaPage", "ShtesaPage.aspx");
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            GridUtil.konfiguroGrideListeEvogelPaTheme(grida, "IdShtesaPage");
            if (grida.ClientInstanceName == "gvShtesaPage" || grida.ClientInstanceName == "gvShtesaPage4" || grida.ClientInstanceName == "gvShtesaPage5" || grida.ClientInstanceName == "gvShtesaPage6")
                grida.Columns["Vlera"].Caption = "Perqindja";
        }

        /// <summary>
        /// sherben per ta bere ne forme spin edit ne menyre qe te lejoje vetem numra
        /// </summary>
        private void shtoVlere(ASPxGridView grida)
        {
            grida.Columns.Remove(grida.Columns["Vlera"]);
            GridViewDataSpinEditColumn colnew = new GridViewDataSpinEditColumn();

            colnew.PropertiesSpinEdit.DecimalPlaces = 2;
            colnew.PropertiesSpinEdit.NumberType = SpinEditNumberType.Float;
            colnew.PropertiesSpinEdit.SpinButtons.Visible = false;
            colnew.PropertiesSpinEdit.MinValue = 0;
            if (grida.ClientInstanceName == "gvShtesaPage" || grida.ClientInstanceName == "gvShtesaPage4" || grida.ClientInstanceName == "gvShtesaPage5" || grida.ClientInstanceName == "gvShtesaPage6")
                colnew.PropertiesSpinEdit.MaxValue = 100;
            colnew.FieldName = "Vlera";
            grida.Columns.Add(colnew);
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
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, "ShtesaPage.aspx", this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, hfRuaj.Value == "Ruaj" ? true : false, true, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
        }


        ///// <summary>
        ///// mbush combon e filtrave
        ///// </summary>
        //private void mbushComboBoxFiltra()
        //{
        //    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka("gvShtesaPage", "ShtesaPage.aspx", idndermarje);
        //    DbCore.DbAdmin.colFiltratGrida colFiltra = new DbCore.DbAdmin.colFiltratGrida(koka.IdGridaKoka, idndermarje);
        //    colFiltra.Insert(0, new DbCore.DbAdmin.clsFiltraGrida());
        //    MenuFilter.colekstioni = colFiltra;
        //    MenuFilter.ValueField = "IdFiltra";
        //    MenuFilter.TextField = "FiltraShenime";
        //}

        /// <summary>
        /// ben databound menune
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(ASPxMenu1, idviti, idperdoruesi, idndermarje);
        }
        /// <summary>
        /// fshin rreshtin e fokusuar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonOk_Click2(object sender, EventArgs e)
        { //per t'u rregulluar sipas kodifikimit te kf
            int index = ASPxPageControl1.ActiveTabIndex;
            ASPxGridView grida;
            if (index == 0) grida = gvShtesaPage;
            else if (index == 1) grida = gvShtesaPage2;
            else if (index == 2) grida = gvShtesaPage3;
            else if (index == 3) grida = gvShtesaPage4;
            else if (index == 4) grida = gvShtesaPage5;
            else grida = gvShtesaPage6;
            int a = grida.FocusedRowIndex;
            grida.Selection.SelectRow(a);
            List<object> rreshtat = grida.GetSelectedFieldValues("IdShtesaPage");

            foreach (int id in rreshtat)
            {
                DbCore.DbListPagesat.clsShtesaPage kat = new DbCore.DbListPagesat.clsShtesaPage(id);

                if (DbCore.DbListPagesat.clsShtesaPage.kaVeprime(kat.IdShtesaPage, kat.Tipi, idndermarje))
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, kaveprime, pnlMesazhi);
                else
                {
                    kat.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                    DbCore.clsMesazh mesazh = kat.fshi();
                    if (mesazh.Status) clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["msgFshirjeMeSukses"], pnlMesazhi);
                    else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhfshirjegabimi, pnlMesazhi);

                }
                konfiguroVleraFillestare();

            }
            pnlGrida.Update();
        }
        /// <summary>
        /// ruan filtrin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            int index = ASPxPageControl1.ActiveTabIndex;
            ASPxGridView grida;
            if (index == 0) grida = gvShtesaPage;
            else if (index == 1) grida = gvShtesaPage2;
            else if (index == 2) grida = gvShtesaPage3;
            else if (index == 3) grida = gvShtesaPage4;
            else if (index == 4) grida = gvShtesaPage5;
            else grida = gvShtesaPage6;
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvShtesaPage", "ShtesaPage.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida() { FiltraKodi = cmbFiltra.Text, FiltraShenime = cmbFiltra.Text, FiltraUniversal = false, GridaKokaId = koka.IdGridaKoka, FiltraVlera = grida.FilterExpression, IdPerdoruesi = idperdoruesi, IdNdermarje = idndermarje, IdStatusDok = 1 };
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("Kodi", grida);
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
            //    filtri.KoloneRenditje = "Kodi";
            //    filtri.DrejtimRenditje = true;
            //}
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            mesazh = filtri.ruaj();
            //mbushComboBoxFiltra();
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idndermarje, "gvShtesaPage", 1, "ShtesaPage.aspx");
            percaktoTemplateMenu(ASPxMenu1, idviti, idperdoruesi, idndermarje);
            if (mesazh.Status == true)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            cmbFiltra.Text = "";
        }
        /// <summary>
        /// fshin filtrin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            int index = ASPxPageControl1.ActiveTabIndex;
            ASPxGridView grida;
            if (index == 0) grida = gvShtesaPage;
            else if (index == 1) grida = gvShtesaPage2;
            else if (index == 2) grida = gvShtesaPage3;
            else if (index == 3) grida = gvShtesaPage4;
            else if (index == 4) grida = gvShtesaPage5;
            else grida = gvShtesaPage6;
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;

            DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            //filtra.mbushFiltraGridaSipasFiltraEmri(cmbFiltra.Text, idndermarje);
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvShtesaPage", "ShtesaPage.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra();
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idndermarje, "gvShtesaPage", 1, "ShtesaPage.aspx");
                percaktoTemplateMenu(ASPxMenu1, idviti, idperdoruesi, idndermarje);
                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                grida.FilterExpression = String.Empty;
            }
        }
        /// <summary>
        /// ben insert te rreshtit te ri
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            //merr te dhenat e rreshtit te ri te grides
            ASPxGridView gride = sender as ASPxGridView;
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idperdoruesi, idndermarje, idviti, "ShtesaPage.aspx");
            if (!tedrejtaInfo.DShtim)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ju nuk keni te drejta per kete veprim!:Red");
                return;
            }
            DbCore.DbListPagesat.clsShtesaPage shtesa = new DbCore.DbListPagesat.clsShtesaPage() { Kodi = e.NewValues["Kodi"].ToString(), Klasa = e.NewValues["Klasa"].ToString(), Vlera = Convert.ToDecimal(e.NewValues["Vlera"]), IdNdermarje = idndermarje, IdPerdoruesi = idperdoruesi, IdStatusDok = 1 };
            if (gride.ClientInstanceName == "gvShtesaPage") shtesa.Tipi = Convert.ToInt32(DbCore.DbListPagesat.TipeShtesaPage.FunksioniPerqindje);
            else if (gride.ClientInstanceName == "gvShtesaPage2") shtesa.Tipi = Convert.ToInt32(DbCore.DbListPagesat.TipeShtesaPage.FunksioniVlere);
            else if (gride.ClientInstanceName == "gvShtesaPage3") shtesa.Tipi = Convert.ToInt32(DbCore.DbListPagesat.TipeShtesaPage.Pozicioni);
            else if (gride.ClientInstanceName == "gvShtesaPage4") shtesa.Tipi = Convert.ToInt32(DbCore.DbListPagesat.TipeShtesaPage.Kualifikimi);
            else if (gride.ClientInstanceName == "gvShtesaPage5") shtesa.Tipi = Convert.ToInt32(DbCore.DbListPagesat.TipeShtesaPage.Veshtiresia);
            else if (gride.ClientInstanceName == "gvShtesaPage6") shtesa.Tipi = Convert.ToInt32(DbCore.DbListPagesat.TipeShtesaPage.Vjetersia);





            e.Cancel = true;
            gride.CancelEdit();
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            if (!DbCore.DbListPagesat.clsShtesaPage.ekzistonShtesaPage(shtesa.Kodi, idndermarje, shtesa.Tipi))
            {
                mesazh = shtesa.ruaj();
                if (!mesazh.Status == true)
                {
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ndodhi nje gabim. Ruajtja nuk u krye!:Red");
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ndodhi nje gabim. Ruajtja nuk u krye!", pnlMesazhi);
                }
                else
                {
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, MessagesResource.Messages["msgRuajtjeMeSuksesGreen"]);
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["mesazhRuajtjeMeSukses"], pnlMesazhi);
                }
                konfiguroVleraFillestare();
            }
            else
            {
                //CacheLayer.GlobalCacheManager.MySessionCache["mesazh"] = "Ekziston nje shtese me kete kod! Ju lutem zgjidhni nje kod tjeter:Red";
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ekziston nje shtese me kete kod! Ju lutem zgjidhni nje kod tjeter:Red");
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ekziston nje shtese me kete kod! Ju lutem zgjidhni nje kod tjeter", pnlMesazhi);
                gride.AddNewRow();
            }
            gride.AddNewRow();
        }
        /// <summary>
        /// ben update te rreshtit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxGridView gride = sender as ASPxGridView;
            String id = e.Keys["IdShtesaPage"].ToString();
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idperdoruesi, idndermarje, idviti, "ShtesaPage.aspx");
            if (!tedrejtaInfo.DMod)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ju nuk keni te drejta per kete veprim!:Red");
                return;
            }
            DbCore.clsMesazh m = new DbCore.clsMesazh();
            DbCore.DbListPagesat.clsShtesaPage shtesa = new DbCore.DbListPagesat.clsShtesaPage() { Kodi = e.NewValues["Kodi"].ToString(), Klasa = e.NewValues["Klasa"].ToString(), Vlera = Convert.ToDecimal(e.NewValues["Vlera"]), IdNdermarje = idndermarje, IdPerdoruesi = idperdoruesi, IdStatusDok = 1, IdShtesaPage = int.Parse(id) };
            if (gride.ClientInstanceName == "gvShtesaPage") shtesa.Tipi = Convert.ToInt32(DbCore.DbListPagesat.TipeShtesaPage.FunksioniPerqindje);
            else if (gride.ClientInstanceName == "gvShtesaPage2") shtesa.Tipi = Convert.ToInt32(DbCore.DbListPagesat.TipeShtesaPage.FunksioniVlere);
            else if (gride.ClientInstanceName == "gvShtesaPage3") shtesa.Tipi = Convert.ToInt32(DbCore.DbListPagesat.TipeShtesaPage.Pozicioni);
            else if (gride.ClientInstanceName == "gvShtesaPage4") shtesa.Tipi = Convert.ToInt32(DbCore.DbListPagesat.TipeShtesaPage.Kualifikimi);
            else if (gride.ClientInstanceName == "gvShtesaPage5") shtesa.Tipi = Convert.ToInt32(DbCore.DbListPagesat.TipeShtesaPage.Veshtiresia);
            else if (gride.ClientInstanceName == "gvShtesaPage6") shtesa.Tipi = Convert.ToInt32(DbCore.DbListPagesat.TipeShtesaPage.Vjetersia);
            e.Cancel = true;


            m = shtesa.modifiko();


            if (m.Status)
            {
                //CacheLayer.GlobalCacheManager.MySessionCache["mesazh"] = m.PershkrimMesazhi + ":Green";
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, m.PershkrimMesazhi + ":Green");
            }
            else
            {
                //CacheLayer.GlobalCacheManager.MySessionCache["mesazh"] = m.PershkrimMesazhi + ":Red";
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, m.PershkrimMesazhi + ":Red");
            }
            gride.CancelEdit();
            konfiguroVleraFillestare();

        }

        /// <summary>
        /// validon te dhenat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {//per tu rregulluar me vone
            //validimi ne jane plotesuar gjithe fushat e detyruara

            ASPxGridView grida = sender as ASPxGridView;
            foreach (GridViewColumn column in grida.Columns)
            {
                if (column.Visible == true)
                {
                    GridViewDataColumn dataColumn = column as GridViewDataColumn;
                    if (dataColumn == null) continue;

                    if (e.NewValues[dataColumn.FieldName] == null)//validimi per kolonat e detyrueshme
                    {
                        e.Errors[dataColumn] = "Vlera nuk mund te jete null.";
                    }
                    if (e.NewValues["Kodi"] == null)
                    {
                        e.Errors[dataColumn] = "Kodi nuk mund te jete bosh.";
                    }
                }
            }
            if (e.NewValues["Kodi"] != null)
            {
                try
                {
                    int.Parse(e.NewValues["Kodi"].ToString());
                    int tipi;
                    if (grida.ClientInstanceName == "gvShtesaPage") tipi = 1;
                    else if (grida.ClientInstanceName == "gvShtesaPage2") tipi = 2;
                    else if (grida.ClientInstanceName == "gvShtesaPage3") tipi = 3;
                    else if (grida.ClientInstanceName == "gvShtesaPage4") tipi = 4;
                    else if (grida.ClientInstanceName == "gvShtesaPage5") tipi = 5;
                    else tipi = 6;

                    if (hfRuaj.Value == "Ruaj" && DbCore.DbListPagesat.clsShtesaPage.ekzistonShtesaPage(e.NewValues["Kodi"].ToString(), idndermarje, tipi))
                    {
                        switch (tipi)
                        {
                            case 1:
                                e.RowError = gabimEksitimi;
                                break;
                            case 2:
                                e.RowError = gabimEksitimiFunk;
                                break;
                            case 3:
                                e.RowError = gabimEksitimiPoz;
                                break;
                            case 4:
                                e.RowError = gabimEksitimiKual;
                                break;
                            case 5:
                                e.RowError = gabimEksitimiVesht;
                                break;
                            case 6:
                                e.RowError = gabimEksitimiVjet;
                                break;
                            default:
                                break;
                        }
                    }
                }
                catch (Exception err)
                {
                    string mesazhi = "Kodi duhet te jete numer!";
                    NLog.LogManager.GetCurrentClassLogger().Error(mesazhi, err.Message);
                    e.RowError = mesazhi;
                }


            }
            else e.RowError = "Kodi nuk mund te jete bosh.";
            if (e.NewValues["Vlera"] != null)
            {
                try
                {
                    decimal.Parse(e.NewValues["Vlera"].ToString());
                }
                catch (Exception err)
                {
                    string mesazhi = "Vlera duhet te jete numer!";
                    NLog.LogManager.GetCurrentClassLogger().Error( mesazhi, err.Message);
                    e.RowError = mesazhi;
                }
            }
            if (e.Errors.Count > 0)
            {
                e.RowError = "Ju lutemi, plotesoni te gjitha fushat.";
            }
            if (string.IsNullOrEmpty(e.RowError) && e.Errors.Count > 0)
            {
                e.RowError = "Ju lutemi, korrigjoni te gjithe gabimet.";
            }
        }
        /// <summary>
        /// kur reshti fillon te editohet therret metoden per validim
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void startRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {//kur fillon editimi te behet validimi
            ASPxGridView grida = sender as ASPxGridView;
            if (!grida.IsNewRowEditing)
            {
                grida.DoRowValidation();
            }
        }
        /// <summary>
        /// kur grida ben callback
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridat_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {

        }
        /// <summary>
        /// kur inicializohet rreshti i ri
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void initNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {

        }
        /// <summary>
        /// kur grida ben callback nga perdoruesi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridat_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            int index = ASPxPageControl1.ActiveTabIndex;
            ASPxGridView grida;
            if (index == 0) grida = gvShtesaPage;
            else if (index == 1) grida = gvShtesaPage2;
            else if (index == 2) grida = gvShtesaPage3;
            else if (index == 3) grida = gvShtesaPage4;
            else if (index == 4) grida = gvShtesaPage5;
            else grida = gvShtesaPage6;
            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                if (arr[2] == "")
                    grida.FilterExpression = "";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    //filtra.mbushFiltraGridaSipasFiltraKodi(arr[2], idndermarje);
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvShtesaPage", "ShtesaPage.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        grida.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, grida);

                        konfiguroVleraFillestare();

                    }
                }
            }
            konfiguroGride(grida);
        }
        /// <summary>
        /// vendos property te grides per tu aksesuar nga javascripti
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
        /// filtrimet
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
    }
}