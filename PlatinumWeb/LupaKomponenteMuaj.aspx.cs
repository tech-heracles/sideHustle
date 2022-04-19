using DbCore;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DbCore.DbListPagesat;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.DbRegjistrim;
using DbCore.DbShare;
using DbCore.IMBUtils.Types;
using Newtonsoft.Json;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;

namespace PlatinumWeb
{
    public partial class LupaKomponenteMuaj : MyPageBase
    {
        private const string KomponenteEmri = "LupaKomponenteMuaj.aspx";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!mySessionObjects.isLogedIn(Session))
            {
                clsFunksione.logout(Session, true, "FaqePaautorizuar");
            }
            int idKonfigambjenti;
            int idNdermarrje;
            int idViti;
            int idPerdoruesi;
            int idGjuha;
            int idNdermarrjeVit;
            if (!hfState.Contains("idPerdoruesi"))
            {
                idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);
                hfState.Set("idPerdoruesi", idPerdoruesi);
            }
            else
                idPerdoruesi = (int)hfState["idPerdoruesi"];
            if (mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerdoruesi);
            }
            if (!IsPostBack)
            {
                idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
                idViti = mySessionObjects.ktheIdVitNdermarrje(Session);
               idNdermarrjeVit = mySessionObjects.ktheNdermarrjeVit(Session);
                idGjuha = mySessionObjects.ktheGjuhe(Session);
                int idNivel = clsNivelRegjistrimi.ktheIdNivelRegjistrimiSipasKodi("LPKM", idNdermarrje);
                idKonfigambjenti = merrKonfiguriminDefaultTeLupes(idNdermarrje, idNivel);
                var kssh = clsAlternativaKushti.getAlternativa(idKonfigambjenti, "KSSH") == "Po";
                konfiguroVleraFillestare();
                clsToolbarConfig.mbushComboBoxFiltra(mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaKomp", 1, KomponenteEmri);
                mbushPopUpListe(idPerdoruesi, idNdermarrje);
                konfiguroGride(idPerdoruesi, idNdermarrje, true, idKonfigambjenti, kssh);
                hfState.Add("KSSH", kssh);
                hfState.Set("idKonfigambjenti", idKonfigambjenti);
                hfState.Set("idGjuha", idGjuha);
                hfState.Set("idViti", idViti);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idNdermarrjeVit", idNdermarrjeVit);
            }
            else
            {
                idNdermarrje = (int)hfState["idNdermarrje"];
                idNdermarrjeVit = (int)hfState["idNdermarrjeVit"];
                idKonfigambjenti = (int)hfState["idKonfigambjenti"];
                idViti = (int)hfState["idViti"];
                var kssh = (bool)hfState["KSSH"];
                idGjuha = (int)hfState["idGjuha"];
                konfiguroGride(idPerdoruesi, idNdermarrje, false, idKonfigambjenti, kssh);
                if (!IsCallback || (IsCallback && Request["__CALLBACKID"].ToString().Contains("gvLupaKomp")))
                {
                    mbushPopUpListeNgaSession(idPerdoruesi, idNdermarrje);
                    konfiguroGride(idPerdoruesi, idNdermarrje, false, idKonfigambjenti, kssh);
                }
            }
            gvLupaKomp.Columns["#"].VisibleIndex = 0;
            perktheLabel();
            percaktoTemplateMenu(idViti, idPerdoruesi, idNdermarrje, ASPxMenu1);
        }

        public void perktheLabel()
        {
            popFshi.HeaderText = rm.GetString("labelAdministrimiKujdes", ci);
            lblMsgbox.Text = rm.GetString("msgMbyllDritarenPaRuajturNdryshimet", ci);
            ButtonOk.Text = rm.GetString("labelOk", ci);
            ButtonCancel.Text = rm.GetString("labelAnullo", ci);
            lblKodi.Text = rm.GetString("lblKodi", ci);
            lblPershkrimi.Text = rm.GetString("lblPershkrimi", ci);
            lblParametri.Text = rm.GetString("lblParametri", ci);
            lblVlera.Text = rm.GetString("filterVlereShitje", ci);
            lblViti.Text = rm.GetString("labelAdministrimiViti", ci);
            lblVleraParametri.Text = rm.GetString("filterVlerParametri", ci);
            lblMuaji.Text = rm.GetString("filterMuaji", ci);

        }

        private void mbushPopUpListe(int idPerdorues, int idNdermarrje)
        {

            var idpunonjesi = Converter.MerrVlereOseDefault<int>(Request.QueryString["idpun"]);
            var komp = Converter.MerrVlereOseDefault<string>(Request.QueryString["komp"]);
            var dataString = Converter.MerrVlereOseDefault<string>(Request.QueryString["data"]);
            var data = string.IsNullOrEmpty(dataString) ? DateTime.Today : JsonConvert.DeserializeObject<DateTime>(dataString);
            var comp = new clsKomponentePage(komp, idNdermarrje, data);

            hfDiteMuaji.Value = comp.AplikoDiteMuaji.ToString();
            hfPageMuaji.Value = comp.AplikoPageMuaji.ToString();

            colKomponenteMuaji colKomp = Utils.MerrColKomponenteMuaji(idpunonjesi, comp.Kodi, Session);
            var col = new colKomponenteMuaji();

            if (colKomp == null || colKomp.Count == 0)
            {
                decimal vleraparam = Converter.MerrVlereOseDefault<decimal>(Request.QueryString["vleraparam"]);
                decimal vlera = Converter.MerrVlereOseDefault<decimal>(Request.QueryString["vlera"]);

                if (vlera != 0 || vleraparam != 0)
                {
                    var k = new clsKomponenteMuaji(0, comp.IdKomponentePage, data.Month, data.Year, vleraparam, vlera, comp.Kodi, comp.Pershkrimi, comp.ParamKodi, comp.Njesi, comp.Tipi);
                    col.ShtoMeIdNegative(k);
                }
            }
            else
                col = colKomp;
            VendosTotaletNeGride(col);
            gvLupaKomp.DataSource = col;
            gvLupaKomp.DataBind();
        }

        private void mbushPopUpListeNgaSession(int idPerdoruesi, int idNdermarrje)
        {
            var idpunonjesi = Converter.MerrVlereOseDefault<int>(Request.QueryString["idpun"]);
            var komp = Converter.MerrVlereOseDefault<string>(Request.QueryString["komp"]);
            var vleraMujorePerKomponenten = Utils.MerrColKomponenteMuaji(idpunonjesi, komp, Session);
            VendosTotaletNeGride(vleraMujorePerKomponenten);
            gvLupaKomp.DataSource = vleraMujorePerKomponenten;
            gvLupaKomp.DataBind();

        }


        private int merrKonfiguriminDefaultTeLupes(int idNdermarrje, int idNivel)
        {
            //do marr konfigurimin default per kete nivel regjistrimi i cili eshte i vetem per nje ndermarrje
            //DbCore.DbShare.clsKonfigurimAmbjenti ambj = new DbCore.DbShare.clsKonfigurimAmbjenti(idNdermarrje, idNivel);
            //return ambj.IdKonfigAmbjente;
            return DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimi(idNdermarrje, idNivel);
        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="idNderVit"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        private void percaktoTemplateMenu(int idViti, int idPerdorues, int idNdermarrje, ASPxMenu aSPxMenu1)
        {
            clsToolbarConfig.percaktoTemplateMenu(mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, KomponenteEmri, this, MenuInfo, hfShtimModifikim.Value == "modifikim" ? false : true, true, false, mySessionObjects.merrEshteMemeSesioni(Session));
            aSPxMenu1.Items.FindByName("Anullo").Text = rm.GetString("MenuItemMbyll", ci);
        }

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {

        }

        protected void gvLupaKomp_DataBound(object sender, EventArgs e)
        {// shton colonen # per selektim dhe disa karakteristika te grides
            if (gvLupaKomp.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
                //perzgjidh
                GridViewCommandColumn check = new GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true; check.Width = Unit.Percentage(2);
                gvLupaKomp.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                gvLupaKomp.Settings.ShowFilterRowMenu = true;
                check.SetColVisibleIndex(0);
                //behet per te afishuar rreshtin qe do sherbej per filtrim
                gvLupaKomp.Settings.ShowFilterRow = true;
                gvLupaKomp.Columns.Add(check);

                gvLupaKomp.KeyFieldName = "Id";
                gvLupaKomp.SettingsBehavior.AllowSelectByRowClick = true;
                gvLupaKomp.SettingsBehavior.AllowFocusedRow = true;
            }
        }

        protected void gvLupaKomp_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
        }

        protected void gvLupaKomp_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {

           
        }

        protected void gvLupaKomp_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {

        }
        /// <summary>
        /// perdoret per te trajtuar ngjarjet e butonave te menuse. Shkon ne faqen e modifikimit te artikujve kur perdoruesi klikon butonin modifiko
        /// ose ne faqen e shtimit te artikujve kur perdoruesi klikon butonin shto
        /// </summary>
        /// <param name="source"> derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {

            Page.Validate("entries");
            switch (e.Item.Name)
            {
                case "Ruaj":
                    ruajKomponente();
                    break;
                case "Fshi":
                    fshi();
                    break;
                default:
                    throw new MyException($"MenuItem {e.Item.Name} i pa trajtuar! ");
            }
        }
        private void fshi()
        {
            var rreshtat = gvLupaKomp.MerrRreshtatESelektuar("Id", "Muaji", "Viti");
            if (rreshtat.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgZgjidhNjeRresht", ci), pnlMesazhi);
                return;
            }
            var idpunonjesi = Converter.MerrVlereOseDefault<int>(Request.QueryString["idpun"]);
            var komp = Converter.MerrVlereOseDefault<string>(Request.QueryString["komp"]);
            var vleraMujorePerKomponenten = Utils.MerrColKomponenteMuaji(idpunonjesi, komp, Session);
            var mesazh = new clsMesazh();
            var idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
            foreach (var rreshti in rreshtat)
            {
                vleraMujorePerKomponenten.RemoveAll(x => x.Muaji == int.Parse(rreshti["Muaji"]) && x.Viti == int.Parse(rreshti["Viti"]));
            }
            Utils.VendosColKomponentePerPunonjesDheKomponente(idpunonjesi, vleraMujorePerKomponenten, komp, Session);
            VendosTotaletNeGride(vleraMujorePerKomponenten);
            hfStatusi.Value = "true";
            gvLupaKomp.DataSource = vleraMujorePerKomponenten;
            gvLupaKomp.DataBind();
            clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("msgFshirjaPerfundoiMeSukses", ci), pnlMesazhi);

        }

        private void ruajKomponente()
        {
            if (!Page.IsValid)
                return;
            hfState.Add("veprim", "");
            int idNdermarrje = (int)hfState["idNdermarrje"];
            int idPerdorues = mySessionObjects.ktheIdPerdoruesi(Session);
            var idpunonjesi = Converter.MerrVlereOseDefault<int>(Request.QueryString["idpun"]);
            var komp = Converter.MerrVlereOseDefault<string>(Request.QueryString["komp"]);
            var dataString = Converter.MerrVlereOseDefault<string>(Request.QueryString["data"]);
            var data = string.IsNullOrEmpty(dataString) ? DateTime.Today : JsonConvert.DeserializeObject<DateTime>(dataString).ToLocalTime();
            var kompon = new clsKomponentePage(komp, idNdermarrje, data);
            var idtrupi = Converter.MerrVlereOseDefault<int>(Request.QueryString["idtrupi"]);

            var vleraKomp = Converter.MerrVlereOseDefault<decimal>(txtVlera.Value);
            var vleraParamKomp = Converter.MerrVlereOseDefault<decimal>(txtVleraParametri.Value);
            var vleraMujorePerKomponenten = Utils.MerrColKomponenteMuaji(idpunonjesi, komp, Session);


            if (vleraKomp > 0)
            {
                var vitiZgjedhur = int.Parse(cmbViti.Value.ToString());
                var muajiZgjedhur = int.Parse(cmbMuaji.Value.ToString());
                var rekordiRi = new clsKomponenteMuaji(vleraMujorePerKomponenten.GetNewId(), idtrupi, muajiZgjedhur, vitiZgjedhur, vleraParamKomp, vleraKomp, kompon.Kodi, kompon.Pershkrimi, kompon.ParamEmri, kompon.Njesi, kompon.Tipi);
                if (vleraParamKomp == 0)
                {
                    clsPunesim punfundit = new clsPunesim();
                    punfundit.merrPunesimFundit(idpunonjesi, new DateTime(vitiZgjedhur,muajiZgjedhur, DateTime.DaysInMonth(vitiZgjedhur,muajiZgjedhur)));
                    if (punfundit.IdPunesim == 0)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, DbCore.IMBUtils.Messages.MessagesResource.Messages["msgPunonjesNukMundTeBeniVeprime"], pnlMesazhi);
                        UpdateTotale(vleraMujorePerKomponenten, idpunonjesi, komp);
                        return;
                    }
                }
                int index = vleraMujorePerKomponenten.FindIndex(x => x.Muaji == muajiZgjedhur && x.Viti == vitiZgjedhur);
                if (index >= 0)
                {
                    vleraMujorePerKomponenten[index] = rekordiRi;
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, DbCore.IMBUtils.Messages.MessagesResource.Messages["msgModifikimiMeSukses"], pnlMesazhi);
                }
                else
                {
                    vleraMujorePerKomponenten.ShtoMeIdNegative(rekordiRi);
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, DbCore.IMBUtils.Messages.MessagesResource.Messages["msgShtimiMeSukses"], pnlMesazhi);
                }
            }
            UpdateTotale(vleraMujorePerKomponenten, idpunonjesi, komp);


        }
        private void UpdateTotale(colKomponenteMuaji vleraMujorePerKomponenten, int idpunonjesi, string komp)
        {
            VendosTotaletNeGride(vleraMujorePerKomponenten);
            Utils.VendosColKomponentePerPunonjesDheKomponente(idpunonjesi, vleraMujorePerKomponenten, komp, Session);
            hfStatusi.Value = "true";
            gvLupaKomp.DataSource = vleraMujorePerKomponenten;
            gvLupaKomp.DataBind();
        }
        private void VendosTotaletNeGride(colKomponenteMuaji vleraMujorePerKomponenten)
        {
            var totalet = new
            {
                VleraParam = vleraMujorePerKomponenten.Sum(x => x.VleraParam),
                Vlera = vleraMujorePerKomponenten.Sum(x => x.Vlera)
            };
            gvLupaKomp.ShtoObjectNeGride(totalet, "cpTotaliKomponentes");
        }

        private void konfiguroVleraFillestare()
        {
            if (ci.Name == "sq-AL")
            {
                ConfigureAspxComboBox.mbushComboMuaji(cmbMuaji);
            }
            else
            {
                ConfigureAspxComboBox.mbushComboMuajiEng(cmbMuaji);
            }

            ConfigureAspxComboBox.mbushComboViti(cmbViti);

            string k = "";
            DateTime data = DateTime.Today;
            if (!String.IsNullOrEmpty(Request.QueryString["data"]) && Request.QueryString["data"] != "undefined")
            {
                data = Newtonsoft.Json.JsonConvert.DeserializeObject<DateTime>(Request.QueryString["data"]);
            }
            cmbMuaji.Value = data.Month.ToString();
            cmbViti.Value = data.Year.ToString();
            if (!String.IsNullOrEmpty(Request.QueryString["komp"]) && Request.QueryString["komp"] != "undefined")
            {
                k = Request.QueryString["komp"];

                var komp = new clsKomponentePage(k, mySessionObjects.merrIdNdermarrjeSesioni(Session), data);
                txtKodi.Text = komp.Kodi;
                txtPershkrimi.Text = komp.Pershkrimi;
                txtParametri.Text = komp.ParamEmri;
                if (komp.Njesi != 0)
                {
                    if (komp.Njesi == 1)
                    {
                        txtVleraParametri.ClientEnabled = false;
                        txtVlera.ClientEnabled = false;
                    }
                    else if (!komp.LejoModVlere) txtVlera.ClientEnabled = false;
                }
                else
                    txtVleraParametri.ClientEnabled = false;
            }
        }



        private void konfiguroGride(int idPerdoruesi, int idNdermarrje, bool visibleIndex, int idKonfigambjenti, bool kerkosaposhkruar)
        {
            KonfigurimComboGride.ShtoMuaj(gvLupaKomp, rm, ci, "Muaji");
            GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "gvLupaKomp", gvLupaKomp, "LP/KM", "616", mySessionObjects.ktheGjuhe(Session));
            var endlessScroll = clsAlternativaKushti.getAlternativa(idKonfigambjenti, "ES") == "Po";
            GridUtil.KonfiguroGrideListeMadhePopupiPaTheme(gvLupaKomp, "Id", kerkosaposhkruar, endlessScroll);
            GridViewDataTextColumn col1 = gvLupaKomp.Columns["Vlera"] as GridViewDataTextColumn;
            col1.PropertiesEdit.DisplayFormatString = "0.00";
            GridViewDataTextColumn col4 = gvLupaKomp.Columns["VleraParam"] as GridViewDataTextColumn;
            col4.PropertiesEdit.DisplayFormatString = "0.00";
        }
        private void shto_Kategori()
        {
            int visibleindex = gvLupaKomp.Columns["KategoriDetajimi"].VisibleIndex;
            gvLupaKomp.Columns.Remove(gvLupaKomp.Columns["KategoriDetajimi"]);
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            colnew.PropertiesComboBox.Items.Add("Detajim 1", 1);
            colnew.PropertiesComboBox.Items.Add("Detajim 2", 2);
            colnew.FieldName = "KategoriDetajimi";
            colnew.VisibleIndex = visibleindex;
            gvLupaKomp.Columns.Add(colnew);
        }

    }
}