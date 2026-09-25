using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Collections;
using System.Drawing;
using DbCore.DbRegjistrim;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.DbShare;

namespace PlatinumWeb
{
    public partial class LupaKonfigurime : MyPageBase
    {
        string idKategori;
        string kushti = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            string vleraQueryString = "";
            if (Request.QueryString["idKonfigAmbjente"] != null && Request.QueryString["idKonfigAmbjente"] != "")
                vleraQueryString = Request.QueryString["idKonfigAmbjente"].ToString();

            if (Request.QueryString["kushti"] != null && Request.QueryString["kushti"] != "")
                kushti = Request.QueryString["kushti"].ToString();
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            int idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            //nivel.Kodi = "KONF"; //eshte kodi i lupes, mund te shihet ne DB ne T_NIVELREGJISTRIMI
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), idPerdoruesi, idNdermarrje);
            int idKonfigambjenti = DbCore.clsFunksione.getIdKonfigAmbLupa(vleraQueryString, idNdermarrje, "KONF");
            bool kerkosaposhkruar = true;
            if (DbCore.DbShare.clsAlternativaKushti.getAlternativa(idKonfigambjenti, "KSSH") == "Po")
                kerkosaposhkruar = true;
            else kerkosaposhkruar = false;
            bool endlessScroll = false;
            if (clsAlternativaKushti.getAlternativa(idKonfigambjenti, "ES") == "Po")
                endlessScroll = true;
            idKategori = Request.QueryString["veprimi"];
            mbushPopUpListeKonfigurimesh(idNdermarrje, idPerdoruesi, idGjuha);
            konfiguroPopupGride(idGjuha, idNdermarrje, kerkosaposhkruar, endlessScroll);
            if (!IsPostBack)
            {
                cmbKonfigurimi.Value = idKonfigambjenti.ToString();
                if (idKategori != null && vleraQueryString.Contains("75"))
                {
                    gvLupaKonfig.FilterExpression = "([Kategoria] Equals 'Regjistrim Qendra Kosto')";
                }
                else
                    GridUtil.AplikoFilterDefault(gvLupaKonfig, idKonfigambjenti);
                gridaSelectButtons.Visible = (Request.QueryString["vjenNgaRaporti"] == "true");
                clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, "gvLupaKonfig", idKonfigambjenti, "LupaKonfigurime.aspx");
            }
        }

        //        oKusht = colKushtet[0]; //cdo konfigurim ambjenti per LUPAT ka vetem nje kusht qe eshte filtri default i grides
        private void mbushPopUpListeKonfigurimesh(int idNdermarrje, int idPerdoruesi, int idGjuha)
        {
            //mbush griden e popupit me te dhena
            DbCore.DbShare.colKonfigurimAmbjenti col = new DbCore.DbShare.colKonfigurimAmbjenti();
            if (idKategori != null)
            {
                idKategori = idKategori.Contains("-") ? $"'{idKategori.Replace("-", "','")}'" : idKategori;
                switch (idKategori)
                {
                    case "90":
                        idKategori = $"{idKategori},'86'";
                        col.mbushKonfigAmbjSipasKategoriveDheGjuhes(idKategori, idNdermarrje, idPerdoruesi, idGjuha);
                        break;
                    case "91":
                        if ((String.IsNullOrEmpty(Request.QueryString["vjenNgaRaporti"])))
                        {
                            if (kushti == "true")
                                col.mbushKonfigAmbjSipasIdKategoriMePershkrimEngMeKusht(6, idNdermarrje, idPerdoruesi, "jo", idGjuha);
                            else
                                col.mbushKonfigAmbjSipasIdKategoriMePershkrimEngMeKusht(6, idNdermarrje, idPerdoruesi, "po", idGjuha);
                        }
                        break;
                    default:
                        col.mbushKonfigAmbjSipasKategoriveDheGjuhes(idKategori, idNdermarrje, idPerdoruesi, idGjuha);
                        break;
                }
            }
            else
                col.mbushGjitheKonfigurimeAmbjenteshMePershkrimEng(idNdermarrje, idPerdoruesi, 2, idGjuha);

            //        else                  
            //    else
            //        col.mbushGjitheKonfigurimeAmbjenteshMePershkrimEng(idNdermarrje, idPerdoruesi, 2, idGjuha); //regjistrime
            //else
            //            else
            //        else
            //            else
            //    else
            //        col.mbushGjitheKonfigurimeAmbjentesh(idNdermarrje, idPerdoruesi, 2, idGjuha); //regjistrime
            gvLupaKonfig.DataSource = col;
            gvLupaKonfig.DataBind();
        }

        private void konfiguroPopupGride(int idGjuha, int idNdermarrje, bool kerkosaposhkruar, bool endlessScroll)
        {
            //konfiguron popupgriden
            shtoKategoriDok();
            shtoNivel();
            shtoSkeme();
            GridUtil.percaktoVisibleColumns(idGjuha, idNdermarrje, gvLupaKonfig, "gvLupaKonfig", "LupaKonfigurime.aspx");
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            GridUtil.KonfiguroGrideListeMadhePopupiPaTheme(gvLupaKonfig, "IdKonfigAmbjente", kerkosaposhkruar, endlessScroll);
        }

        private void shtoKategoriDok()
        {
            gvLupaKonfig.Columns.Remove(gvLupaKonfig.Columns["IdKategori"]);
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            DbCore.DbRegjistrim.clsKategoriNivelDok oKategori = new DbCore.DbRegjistrim.clsKategoriNivelDok();
            DbCore.DbRegjistrim.colKategoriNiveleDok colKategori = new DbCore.DbRegjistrim.colKategoriNiveleDok();
            colKategori.Add(new DbCore.DbRegjistrim.clsKategoriNivelDok(-1, "", 0, 1, false, 0, false));
            colKategori.AddRange(oKategori.merriTeGjithePa(1)); //nuk e marr parasysh ne SP ndermarjen mqs kategorite nuk jane ne nivel ndermarje

            colnew.PropertiesComboBox.DataSource = colKategori;
            colnew.PropertiesComboBox.TextField = "Pershkrimi";
            colnew.PropertiesComboBox.ValueField = "IdKategori";
            colnew.FieldName = "IdKategori";
            gvLupaKonfig.Columns.Add(colnew);
        }

        //sherben per ta bere ne forme combo-je shtyllen e nengrupeve
        private void shtoNivel()
        {
            gvLupaKonfig.Columns.Remove(gvLupaKonfig.Columns["IdNivel"]);

            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();

            colnew.PropertiesComboBox.DataSource = DbCore.DbRegjistrim.colNivelRegjistrimi.ktheGjitheNivelRegjistrimi(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), true);
            colnew.PropertiesComboBox.TextField = "Pershkrimi";
            colnew.PropertiesComboBox.ValueField = "IdNivel";
            colnew.FieldName = "IdNivel";
            gvLupaKonfig.Columns.Add(colnew);
        }

        private void shtoSkeme()
        {

            gvLupaKonfig.Columns.Remove(gvLupaKonfig.Columns["IdSkemeKontabel"]);
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            DbCore.DbKontabiliteti.colSkemaKontabelNew colSkema = new DbCore.DbKontabiliteti.colSkemaKontabelNew(DbCore.mySessionObjects.ktheNdermarrjeVit(Session));
            colnew.PropertiesComboBox.DataSource = colSkema;
            colnew.PropertiesComboBox.TextField = "KodSkemeKont";
            colnew.PropertiesComboBox.ValueField = "IdSkemeKont";
            colnew.FieldName = "IdSkemeKontabel";
            gvLupaKonfig.Columns.Add(colnew);
        }

        protected void gvLupaKonfig_DataBound(object sender, EventArgs e)
        {
            gvLupaKonfig.KeyFieldName = "IdKonfigAmbjente";
            GridUtil.GridDataBound(sender, e, gvLupaKonfig, "IdKonfigAmbjente");
        }

        protected void gvLupaKonfig_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            if (String.IsNullOrEmpty(Request.QueryString["vjenNgaRaporti"]))
                gvLupaKonfig.Selection.UnselectAll();
        }

        //    else


        //            else
        //        else


        //    //if (dbAdmin.ekzistonFilter(Kodi_ASPxTextBox.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session)))

        protected void gvLupaKonfig_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                if (arr[2] == "")
                    gvLupaKonfig.FilterExpression = "";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaKonfig", "LupaKonfigurime.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], idNdermarrje, koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvLupaKonfig.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvLupaKonfig);
                    }
                }
            }
            if (String.IsNullOrEmpty(Request.QueryString["vjenNgaRaporti"]))
                gvLupaKonfig.Selection.UnselectAll();
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
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, "LupaKonfigurime.aspx", this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            aSPxMenu1.Items.FindByName("Anullo").Text = rm.GetString("MenuItemMbyll", ci);
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
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaKonfig", "LupaKonfigurime.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvLupaKonfig.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("KodKonfigAmbjente", gvLupaKonfig);
            //    else
            //else
            DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();

            oPerdorues = DbCore.mySessionObjects.kthePerdorues(Session);
            filtri.IdPerdoruesi = oPerdorues.IdPerdorues;
            filtri.IdNdermarje = idNdermarrje;
            filtri.IdStatusDok = 1;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            mesazh = filtri.ruaj();
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaKonfig", Convert.ToInt32(cmbKonfigurimi.Value), "LupaKonfigurime.aspx");
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
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaKonfig", "LupaKonfigurime.aspx", idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaKonfig", Convert.ToInt32(cmbKonfigurimi.Value), "LupaKonfigurime.aspx");
                percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                gvLupaKonfig.FilterExpression = String.Empty;
            }
        }

        //private void mbushComboBoxFiltra(int idNdermarrje)
    }
}
