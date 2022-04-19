using DbCore.DbAdmin;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.Extensions;
using DbCore.DbShare;

namespace PlatinumWeb
{
    public partial class LupaNdermarjeBij : MyPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string vleraQueryString;
            if (Request.QueryString["idKonfigAmbjente"] != null && Request.QueryString["idKonfigAmbjente"] != "")
                vleraQueryString = Request.QueryString["idKonfigAmbjente"].ToString();
            else
                vleraQueryString = "";

            bool perfshiNdermarrjeOwn = Request.QueryString["vjenNga"] != null && Request.QueryString["vjenNga"].ToString().EqualsAnyIgnoreCase("Shto_KF", "LlojDifekti", "StatusRiparimi", "KodifikimArtikulli", "Artikulli");


            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
            int idKonfigAmbjenti = DbCore.clsFunksione.getIdKonfigAmbLupa(vleraQueryString, idNdermarrje, "LNDER");
            bool kerkosaposhkruar = true;
            if (DbCore.DbShare.clsAlternativaKushti.getAlternativa(idKonfigAmbjenti, "KSSH") == "Po")
                kerkosaposhkruar = true;
            else kerkosaposhkruar = false;
            bool eshteNdermRaportuese = !String.IsNullOrEmpty(Request.QueryString["Raportuese"]);
            int nivelStrukture = Request.QueryString["NivelStrukture"] != null? Convert.ToInt16(Request.QueryString["NivelStrukture"]) : 0;
            if (!IsPostBack)
            {
                cmbKonfigurimi.Value = idKonfigAmbjenti.ToString();
                GridUtil.AplikoFilterDefault(gvLupaNdermarje, idKonfigAmbjenti);
                mbushPopUpListeNgaDB(eshteNdermRaportuese, idNdermarrje, perfshiNdermarrjeOwn, nivelStrukture);
                konfiguroPopupGride(idKonfigAmbjenti, true, kerkosaposhkruar);
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaNdermarje", idKonfigAmbjenti, "LupaNdermarjeBij.aspx");
            }
            else
            {
                mbushPopUpListeNgaSession(eshteNdermRaportuese, idNdermarrje, perfshiNdermarrjeOwn, nivelStrukture);
                konfiguroPopupGride(idKonfigAmbjenti, false, kerkosaposhkruar);
            }

        }
        private void mbushPopUpListeNgaSession(bool eshteNdermRaportuese, int idNdermarrje, bool perfshiNdermarrjeOwn, int nivelStrukture)
        {
            Object tmpObject;
            DbCore.mySessionObjects.merrGrideNgaSessioniLupa(Session, out tmpObject);
            if (tmpObject == null)
                mbushPopUpListeNgaDB(eshteNdermRaportuese, idNdermarrje, perfshiNdermarrjeOwn, nivelStrukture);
            else
            {
                gvLupaNdermarje.DataSource = tmpObject;
                gvLupaNdermarje.DataBind();
            }
        }
        /// <summary>
        /// mbush lupen e ndermarrjeve ne varesi te ndermarrjes nese eshte raportuese apo eshte meme
        /// </summary>
        /// <param name="eshteRaportuese">true nese duhet mbushur datasource per ndermarrjet bija te ndermarrjes raportuese dhe false nese duhet mbushur me bijat e nderm MEME</param>
        private void mbushPopUpListeNgaDB(bool eshteRaportuese, int idNdermarrje, bool perfshiNdermarrjeOwn, int nivelStrukture)
        {//mbush griden e popupit me te dhena      
            DataTable dt;
            if (nivelStrukture != 0)
                dt = colNdermarrjet.ktheNdermarjeBijSipasNivelitDT(idNdermarrje, nivelStrukture);
            else if (eshteRaportuese)
                dt = colNdermarrjet.ktheNdermarjeBijSipasNdermRaportueseDT(idNdermarrje);
            else if (perfshiNdermarrjeOwn)
                dt = colNdermarrjet.ktheNdermarjeBijSipasMemeDheOwnDT(idNdermarrje);
            else 
                dt = colNdermarrjet.ktheNdermarjeBijSipasMemeDT(idNdermarrje);

            DbCore.mySessionObjects.ruajGrideNeSessionLupa(Session, dt);
            gvLupaNdermarje.DataSource = dt;
            gvLupaNdermarje.DataBind();
        }


        private void konfiguroPopupGride(int idKonfigAmbjenti, bool visibleIndex, bool kerkosaposhkruar)
        {
            GridUtil.percaktoVisibleColumnsSipasKonfigurimit(gvLupaNdermarje, "gvLupaNdermarje", "LupaNdermarjeBij.aspx", idKonfigAmbjenti, visibleIndex, DbCore.mySessionObjects.ktheGjuhe(Session));
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            var endlessScroll = clsAlternativaKushti.getAlternativa(idKonfigAmbjenti, "ES") == "Po";
            GridUtil.KonfiguroGrideListeMadhePopupiPaTheme(gvLupaNdermarje, "IdNdermarrje", kerkosaposhkruar, endlessScroll);
            this.gvLupaNdermarje.Columns["#"].VisibleIndex = 0;
            gvLupaNdermarje.Settings.ShowTitlePanel = true;
        }

        protected void gvLupaNdermarje_DataBound(object sender, EventArgs e)
        {
            if (this.gvLupaNdermarje.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
                //perzgjidh
                GridViewCommandColumn check = new GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true;
                check.Width = Unit.Percentage(2);
                //   check.SetColVisibleIndex(0);
                //behet per te afishuar rreshtin qe do sherbej per filtrim
                gvLupaNdermarje.Settings.ShowFilterRow = true;
                gvLupaNdermarje.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                gvLupaNdermarje.Settings.ShowFilterRowMenu = true;
                gvLupaNdermarje.Columns.Add(check);

                gvLupaNdermarje.KeyFieldName = "IdNdermarrje";
                gvLupaNdermarje.SettingsBehavior.AllowSelectByRowClick = true;
                gvLupaNdermarje.SettingsBehavior.AllowFocusedRow = true;
            }

        }

        protected void gvLupaNdermarje_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {

            gvLupaNdermarje.Selection.UnselectAll();
        }

        protected void gvLupaNdermarje_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvLupaNdermarje.PageIndex;
            e.Properties["cpPageRow"] = gvLupaNdermarje.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvLupaNdermarje.VisibleRowCount;
        }

        protected void gvLupaNdermarje_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                if (arr[2] == "")
                    gvLupaNdermarje.FilterExpression = "";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaNdermarje", "LupaNdermarjeBij.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], idNdermarrje, koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvLupaNdermarje.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvLupaNdermarje);
                    }
                }
            }
            gvLupaNdermarje.Selection.UnselectAll();
        }

        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        }
        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        /// <param name="idViti"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNermarrje"></param>
        private void percaktoTemplateMenu(ASPxMenu aSPxMenu1, int idViti, int idPerdorues, int idNdermarrje)
        {
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, "LupaNdermarjeBij.aspx", this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
            if (Request.QueryString["vjenNgaRaporti"] == "true")
                aSPxMenu1.Items.FindByName("Anullo").Text = "Mbyll";
        }


        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            clsMenuInfo.ShtoMenuItemInfo(this, MenuInfo);
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida();
            filtri.FiltraKodi = cmbFiltra.Text;
            filtri.FiltraShenime = cmbFiltra.Text;
            filtri.FiltraUniversal = false;// Universal_ASPxCheckBox.Checked;
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaNdermarje", "LupaNdermarjeBij.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvLupaNdermarje.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("IdNdermarrje", gvLupaNdermarje);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvLupaNdermarje.GetSortedColumns();
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
            //    filtri.KoloneRenditje = "IdNdermarrje";
            //    filtri.DrejtimRenditje = true;
            //}
            DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();

            oPerdorues = DbCore.mySessionObjects.kthePerdorues(Session);
            filtri.IdPerdoruesi = oPerdorues.IdPerdorues;
            filtri.IdNdermarje = idNdermarrje;
            filtri.IdStatusDok = 1;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            mesazh = filtri.ruaj();
            //mbushComboBoxFiltra(idNdermarrje);
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaNdermarje", Convert.ToInt32(cmbKonfigurimi.Value), "LupaNdermarjeBij.aspx");
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
            if (mesazh.Status == true)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            cmbFiltra.Text = "";
        }

        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            clsMenuInfo.ShtoMenuItemInfo(this, MenuInfo);
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaNdermarje", "LupaNdermarjeBij.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra(idNdermarrje);
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaNdermarje", Convert.ToInt32(cmbKonfigurimi.Value), "LupaNdermarjeBij.aspx");
                percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                gvLupaNdermarje.FilterExpression = String.Empty;
            }
        }

        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {

        }

        private void transferoTeDhena()
        {
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            switch (Request.QueryString["vjenNga"])
            {

                case "KodifikimArtikulli":
                    mesazh = transferoTeDhenaKodifikim();
                    break;
                case "LlojDifekti":
                    mesazh = transferoTeDhenaLlojDifekti();
                    break;
                case "StatusRiparimi":
                    mesazh = transferoTeDhenaStatusRiparimi();
                    break;
                case "Artikulli":
                    mesazh = transferoTeDhenaArtikull();
                    break;
                case "Cmime":
                    mesazh = transferoTeDhenaCmime();
                    break;
            }
            hfMesazhi.Value = (new JavaScriptSerializer()).Serialize(mesazh);
            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                hfStatusi.Value = "false";
              
            }
            else
            {
                clsMenuInfo.ShtoMesazh(MenuInfo, mesazh, pnlMesazhi);
                hfStatusi.Value = "true";
            }
            pnlMesazhi.Update();
        }

        private DbCore.clsMesazh transferoTeDhenaKodifikim()
        {
            JavaScriptSerializer serializusi = new JavaScriptSerializer();
            object[] id = (object[])serializusi.DeserializeObject(hfId.Value);
            List<object> rreshtat = gvLupaNdermarje.GetSelectedFieldValues("IdNdermarrje");
            DbCore.DbInventari.clsKodifikimArtikulli kod = new DbCore.DbInventari.clsKodifikimArtikulli();
            DbCore.clsMesazh mesazh = kod.transfero(id, rreshtat, DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            return mesazh;
        }
        private DbCore.clsMesazh transferoTeDhenaLlojDifekti()
        {
            JavaScriptSerializer serializusi = new JavaScriptSerializer();
            object[] id = (object[])serializusi.DeserializeObject(hfId.Value);
            List<object> rreshtat = gvLupaNdermarje.GetSelectedFieldValues("IdNdermarrje");
            DbCore.DbInventari.clsLlojDifekti kod = new DbCore.DbInventari.clsLlojDifekti();
            DbCore.clsMesazh mesazh = kod.transfero(id, rreshtat, DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            return mesazh;
        }
        private DbCore.clsMesazh transferoTeDhenaStatusRiparimi()
        {
            JavaScriptSerializer serializusi = new JavaScriptSerializer();
            object[] id = (object[])serializusi.DeserializeObject(hfId.Value);
            List<object> rreshtat = gvLupaNdermarje.GetSelectedFieldValues("IdNdermarrje");
            DbCore.DbInventari.clsStatusRiparimi kod = new DbCore.DbInventari.clsStatusRiparimi();
            DbCore.clsMesazh mesazh = kod.transfero(id, rreshtat, DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            return mesazh;
        }
        private DbCore.clsMesazh transferoTeDhenaArtikull()
        {
            JavaScriptSerializer serializusi = new JavaScriptSerializer();
            object[] id = (object[])serializusi.DeserializeObject(hfId.Value);
            List<object> rreshtat = gvLupaNdermarje.GetSelectedFieldValues("IdNdermarrje");
            DbCore.DbInventari.clsArtikulli art = new DbCore.DbInventari.clsArtikulli();
            DbCore.clsMesazh mesazh = art.transfero(id, rreshtat, DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            return mesazh;
        }
        private DbCore.clsMesazh transferoTeDhenaCmime()
        {
            DbCore.DbInventari.colCmimeArtikujsh col = new DbCore.DbInventari.colCmimeArtikujsh();
            List<object> rreshtat = gvLupaNdermarje.GetSelectedFieldValues("IdNdermarrje");
            col = ruajCmime(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            DbCore.DbInventari.clsCmimArtikulli art = new DbCore.DbInventari.clsCmimArtikulli();
            DbCore.clsMesazh mesazh = art.transfero(col, rreshtat, DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            return mesazh;
        }
        public DbCore.DbInventari.colCmimeArtikujsh ruajCmime(int idNdermarrje)
        {//ruhet collectioni i trupave te fleteve kontabel sipas te dhenave te futura nga perdoruesi


            DbCore.DbInventari.colCmimeArtikujsh cmimet = new DbCore.DbInventari.colCmimeArtikujsh();

            DataTable dt = DbCore.mySessionObjects.merrDtNgaSessioni(Session);
            JavaScriptSerializer serializusi = new JavaScriptSerializer();

            int rreshta = dt.Rows.Count;//gvCmimArtikulli.VisibleRowCount + 1;

            object[] dtFill = (object[])serializusi.DeserializeObject(hfDtFillimi.Value);
            object[] dtMbar = (object[])serializusi.DeserializeObject(this.hfDtMbarimi.Value);
            object[] cmim = (object[])serializusi.DeserializeObject(this.hfCmimi.Value);
            object[] cmim2 = (object[])serializusi.DeserializeObject(this.hfCmimi2.Value);
            object[] check = (object[])serializusi.DeserializeObject(this.hfReshtaTeSelektuar.Value);
            object[] sasimin = (object[])serializusi.DeserializeObject(this.hfSasiMin.Value);
            object[] sasimax = (object[])serializusi.DeserializeObject(this.hfSasiMax.Value);
            int j = 0;
            for (int i = 0; i < dt.Rows.Count; i++)//krijohet kolectioni me trupat e fleteve kontabel e futura nga perdoruesi
            {
                DbCore.DbInventari.clsCmimArtikulli cmimi = new DbCore.DbInventari.clsCmimArtikulli();

                if ((bool)check[i] == true)
                {

                    cmimi.IdArtikulli = Convert.ToInt32(dt.Rows[i]["IdArtikulli"]);
                    cmimi.IdNivelCmimi = Convert.ToInt32(dt.Rows[i]["IdNivelCmimi"]);
                    cmimi.IdMonedha = Convert.ToInt32(dt.Rows[i]["IdMonedha"]);
                    cmimi.IdNjesia = Convert.ToInt32(dt.Rows[i]["IdNjesia"]);
                    cmimi.IdNjesia2 = Convert.ToInt32(dt.Rows[i]["IdNjesia2"]);
                    //cmimi.DateMbarimi = Convert.ToDateTime(dt.Rows[i]["DateMbarimi"]);
                    //cmimi.SasiMax = Convert.ToDecimal(dt.Rows[i]["SasiMax"]);
                    //cmimi.SasiMin = Convert.ToDecimal(dt.Rows[i]["SasiMin"]);
                    cmimi.IdCmimArtikulli = Convert.ToInt32(dt.Rows[i]["IdCmimArtikulli"]);
                    //cmimi.Formula = "";
                    cmimi.IdStatusDok = 1;
                    cmimet.Add(cmimi);
                    //if (njesi[i] != null&&njesi[i] != "null"&&njesi[i] != "")
                    //{
                    //    cmimet[i].IdNjesia = int.Parse(njesi[i]);
                    //}
                    if (dtFill[i] != null && dtFill[i].ToString() != "")
                        cmimet[j].DateFillimi = DateTime.Parse(dtFill[i].ToString());
                    if (dtMbar[i] != null && dtMbar[i].ToString() != "")
                    {
                        cmimet[j].DateMbarimi = DateTime.Parse(dtMbar[i].ToString());
                    }
                    if (sasimin[i] != null && sasimin[i].ToString() != "")
                        cmimet[j].SasiMin = decimal.Parse(sasimin[i].ToString());
                    if (sasimax[i] != null && sasimax[i].ToString() != "")
                        cmimet[j].SasiMax = decimal.Parse(sasimax[i].ToString());
                    if (cmim[i] != null && cmim[i].ToString() != "")
                        cmimet[j].Cmimi = decimal.Parse(cmim[i].ToString());
                    if (cmim2[i] != null && cmim2[i].ToString() != "")
                        cmimet[j].Cmimi2 = decimal.Parse(cmim2[i].ToString());
                    cmimet[j].IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                    //cmimet[j].IdNderViti = new DbCore.clsFunksione().ktheNdermarrjeVit();
                    cmimet[j].IdNdermarje = idNdermarrje;
                    DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                    konf.mbushKonfigAmbjSipasKod("CSH", cmimet[j].IdNdermarje);
                    cmimet[j].IdKonfig = konf.IdKonfigAmbjente;
                    j++;
                }

            }
            return cmimet;
        }
        protected void ASPxButton1_Click(object sender, EventArgs e)
        {
            transferoTeDhena();

        }

    }
}