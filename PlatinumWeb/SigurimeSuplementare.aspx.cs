using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DbCore.IMBUtils.Messages;
using DevExpress.Web;
using PlatinumWeb.ApplicationUtils.Pages;


namespace PlatinumWeb
{
    public partial class SigurimeSuplementare : MyPageBase
    {
        private const string kaveprime = "Ka veprime me kete sigurim";
        private const string mesazhfshirjegabimi = "Fshirja perfundoi me gabime!";
        private const string gabimEksitimi = "ekziston nje sigurim suplementar me kete kod. Ju lutem shenoni nje tjeter!";
        private int idndermarje, idperdoruesi, idnderviti;
        /// <summary>
        /// kur faqja lodohet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);

            //if (CacheLayer.GlobalCacheManager.MySessionCache["LoggedIn"].Equals("No"))
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                Response.Redirect(DbCore.IMBUtils.Paths.defaultLoginPath);
                return;
            }
            idndermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            idperdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            idnderviti = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
            //if (CacheLayer.GlobalCacheManager.MySessionCache["KodiNdermarrjes"] == null)
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idperdoruesi);
                return;
            }

            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idndermarje);

            //mbushComboBoxFiltra();
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idndermarje, "gvSigurimeSuplementare", 1, "SigurimeSuplementare.aspx");
            if (!IsPostBack)
            {
                //Session.Add("mesazh", ":Green");
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, ":");
                if (Request.QueryString["ruaj"] == "ok")
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["mesazhRuajtjeMeSukses"], pnlMesazhi);
                konfiguroVleraFillestare();
                konfiguroGridat();
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idperdoruesi, idndermarje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "SigurimeSuplementare.aspx");
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
            }
            //if (CacheLayer.GlobalCacheManager.MySessionCache["mesazh"].ToString().Split(':')[1] == "Green")
            //if (DbCore.mySessionObjects.merrMesazhNgaSesioni(Session).Split(':')[1] == "Green")
            //{
            //    //clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, CacheLayer.GlobalCacheManager.MySessionCache["mesazh"].ToString().Split(':')[0], pnlMesazhi);
            //    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, DbCore.mySessionObjects.merrMesazhNgaSesioni(Session).Split(':')[0], pnlMesazhi);
            //}
            //else if (DbCore.mySessionObjects.merrMesazhNgaSesioni(Session).Split(':')[1] == "Red")
            //{
            //    //clsMenuInfo.ShtoMesazhGabimi(MenuInfo, CacheLayer.GlobalCacheManager.MySessionCache["mesazh"].ToString().Split(':')[0], pnlMesazhi);
            //    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, DbCore.mySessionObjects.merrMesazhNgaSesioni(Session).Split(':')[0], pnlMesazhi);
            //}
            //CacheLayer.GlobalCacheManager.MySessionCache["mesazh"] = ":Green";
            DbCore.mySessionObjects.ruajMesazhNeSesion(Session, ":");

            konfiguroVleraFillestare();

        }
        /// <summary>
        /// mbush gridat me te dhena
        /// </summary>
        private void konfiguroVleraFillestare()
        {
            DbCore.DbListPagesat.colSigurimeSuplementare col1 = new DbCore.DbListPagesat.colSigurimeSuplementare(idndermarje);
            gvSigurimeSuplementare.DataSource = col1;
            gvSigurimeSuplementare.DataBind();
        }
        /// <summary>
        /// konfiguron gridat
        /// </summary>
        private void konfiguroGridat()
        {
            konfiguroGride(gvSigurimeSuplementare);
        }

        /// <summary>
        /// konfiguron griden
        /// </summary>
        /// <param name="grida">grida</param>
        private void konfiguroGride(ASPxGridView grida)
        {
            shtoVlere(grida);
            GridUtil.percaktoVisibleColumnsMeWidth(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), grida, "gvSigurimeSuplementare", "SigurimeSuplementare.aspx");
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            GridUtil.konfiguroGrideListeEvogelPaTheme(grida, "IdSigurimeSuplementare");
        }

        /// <summary>
        /// sherben per ta bere ne forme spin edit ne menyre qe te lejoje vetem numra
        /// </summary>
        private static void shtoVlere(ASPxGridView grida)
        {
            grida.Columns.Remove(grida.Columns["Perqindja"]);
            GridViewDataSpinEditColumn colnew = new GridViewDataSpinEditColumn();

            colnew.PropertiesSpinEdit.DecimalPlaces = 2;
            colnew.PropertiesSpinEdit.NumberType = SpinEditNumberType.Float;
            colnew.PropertiesSpinEdit.SpinButtons.Visible = false;
            colnew.PropertiesSpinEdit.MinValue = 0;
            colnew.PropertiesSpinEdit.MaxValue = 100;
            colnew.FieldName = "Perqindja";
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
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, "SigurimeSuplementare.aspx", this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, hfRuaj.Value == "Ruaj" ? true : false, true, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
        }

        ///// <summary>
        ///// mbush combon e filtrave
        ///// </summary>
        //private void mbushComboBoxFiltra()
        //{
        //    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka("gvSigurimeSuplementare", "SigurimeSuplementare.aspx", idndermarje);
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
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));

        }
        /// <summary>
        /// fshin rreshtin e fokusuar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonOk_Click2(object sender, EventArgs e)
        { //per t'u rregulluar sipas kodifikimit te kf
            ASPxGridView grida = gvSigurimeSuplementare;

            int a = grida.FocusedRowIndex;
            grida.Selection.SelectRow(a);
            List<object> rreshtat = grida.GetSelectedFieldValues("IdSigurimeSuplementare");

            foreach (int id in rreshtat)
            {
                DbCore.DbListPagesat.clsSigurimeSuplementare kat = new DbCore.DbListPagesat.clsSigurimeSuplementare(id);

                if (DbCore.DbListPagesat.clsSigurimeSuplementare.kaVeprime(kat.IdSigurimeSuplementare,idndermarje))
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
            ASPxGridView grida = gvSigurimeSuplementare;

            MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvSigurimeSuplementare", "SigurimeSuplementare.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
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
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idndermarje, "gvSigurimeSuplementare", 1, "SigurimeSuplementare.aspx");
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));

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
            ASPxGridView grida = gvSigurimeSuplementare;

            MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;

            DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            //filtra.mbushFiltraGridaSipasFiltraEmri(cmbFiltra.Text, idndermarje);
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvSigurimeSuplementare", "SigurimeSuplementare.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra();
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idndermarje, "gvSigurimeSuplementare", 1, "SigurimeSuplementare.aspx");
                percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));

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
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idndermarje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "SigurimeSuplementare.aspx");
            if (!tedrejtaInfo.DShtim)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ju nuk keni te drejta per kete veprim!:Red");
                return;
            }
            ASPxGridView gride = sender as ASPxGridView;
            DbCore.DbListPagesat.clsSigurimeSuplementare sigurime = new DbCore.DbListPagesat.clsSigurimeSuplementare() { Kodi = e.NewValues["Kodi"].ToString(), Grupi = e.NewValues["Grupi"].ToString(), Perqindja = Convert.ToDecimal(e.NewValues["Perqindja"]), DtAktivizimi = Convert.ToDateTime(e.NewValues["DtAktivizimi"]), IdNdermarje = idndermarje, IdPerdoruesi = idperdoruesi, IdStatusDok = 1, Germa = Convert.ToString(e.NewValues["Germa"]) };

            e.Cancel = true;
            gride.CancelEdit();
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            if (!DbCore.DbListPagesat.clsSigurimeSuplementare.ekzistonSigurimeSuplementare(sigurime.Kodi, idndermarje))
            {
                mesazh = sigurime.ruaj();
                if (!mesazh.Status == true)
                {
                    //CacheLayer.GlobalCacheManager.MySessionCache["mesazh"] = "Ndodhi nje gabim. Ruajtja nuk u krye!:Red";
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ndodhi nje gabim. Ruajtja nuk u krye!:Red");
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ndodhi nje gabim. Ruajtja nuk u krye!", pnlMesazhi);
                }
                else
                {
                    //CacheLayer.GlobalCacheManager.MySessionCache["mesazh"] = "Ruajtja perfundoi me sukses!:Green";
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ruajtja perfundoi me sukses!:Green");
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["mesazhRuajtjeMeSukses"], pnlMesazhi);
                }
                konfiguroVleraFillestare();
            }
            else
            {
                //CacheLayer.GlobalCacheManager.MySessionCache["mesazh"] = "Ekziston nje sigurim me kete kod! Ju lutem zgjidhni nje kod tjeter:Red";
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ekziston nje sigurim me kete kod! Ju lutem zgjidhni nje kod tjeter:Red");
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ekziston nje sigurim me kete kod! Ju lutem zgjidhni nje kod tjeter", pnlMesazhi);
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
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idndermarje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "SigurimeSuplementare.aspx");
            if (!tedrejtaInfo.DMod)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ju nuk keni te drejta per kete veprim!:Red");
                return;
            }
            ASPxGridView gride = sender as ASPxGridView;
            String id = e.Keys["IdSigurimeSuplementare"].ToString();

            DbCore.clsMesazh m = new DbCore.clsMesazh();
            DbCore.DbListPagesat.clsSigurimeSuplementare sigurime = new DbCore.DbListPagesat.clsSigurimeSuplementare() { Kodi = e.NewValues["Kodi"].ToString(), Grupi = e.NewValues["Grupi"].ToString(), Perqindja = Convert.ToDecimal(e.NewValues["Perqindja"]), DtAktivizimi = Convert.ToDateTime(e.NewValues["DtAktivizimi"]), IdNdermarje = idndermarje, IdPerdoruesi = idperdoruesi, IdStatusDok = 1, IdSigurimeSuplementare = int.Parse(id), Germa = Convert.ToString(e.NewValues["Germa"]) };
            e.Cancel = true;


            m = sigurime.modifiko();


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
                    if (dataColumn.FieldName == "Germa")
                        continue;
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


                    if (hfRuaj.Value == "Ruaj" && DbCore.DbListPagesat.clsSigurimeSuplementare.ekzistonSigurimeSuplementare(e.NewValues["Kodi"].ToString(), idndermarje))
                    {
                        e.RowError = gabimEksitimi;
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
            if (e.NewValues["Perqindja"] != null)
            {
                try
                {
                    decimal.Parse(e.NewValues["Perqindja"].ToString());
                }
                catch (Exception err)
                {
                    string mesazhi = "Perqindja duhet te jete numer!";
                    NLog.LogManager.GetCurrentClassLogger().Error(mesazhi, err.Message);
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
            e.NewValues["DtAktivizimi"] = "01/01/" + DateTime.Now.Year;
        }
        /// <summary>
        /// kur grida ben callback nga perdoruesi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridat_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {

            ASPxGridView grida = gvSigurimeSuplementare;

            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                if (arr[2] == "")
                    grida.FilterExpression = "";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    //filtra.mbushFiltraGridaSipasFiltraKodi(arr[2], idndermarje);
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvSigurimeSuplementare", "SigurimeSuplementare.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
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