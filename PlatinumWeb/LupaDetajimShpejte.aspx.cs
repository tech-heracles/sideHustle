using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Collections;
using System.Drawing;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Web.Script.Serialization;
using AjaxControlToolkit;
using DbCore;
using PlatinumWeb.Templates;
using DbCore.DbRegjistrim;
using System.Globalization;
using System.Resources;
using DbCore.IMBUtils.Validation;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.DbShare;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    public partial class LupaDetajimShpejte : MyPageBase
    {
        private static string STR_DateSkadenceJoVlefshme = "Kodi i dt skadences duhet te jete ne format date dd/MM/yyyy!";
        private static string STR_ekzistonKodDetajimiPerArtikull = "Ekziston nje detajim me kete kod i lidhur me kete artikull!";
        private static string STR_ekzistonKodDetajimi = "Ekziston nje detajim me kete kod!";
        private static string STR_shenoniKodin = "Ju lutemi shenoni kodin e detajimit!";
        private static string STR_shenoniEmertimin = "Ju lutemi shenoni emertimin e detajimit!";
        private static string STR_nukKeniTeDrejta = "Ju nuk keni te drejta per te kryer kete veprim!";
        private const string STR_EmertimiNumerikGabim = "Jepni një vlerë numerike për emërtimin!";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                DbCore.clsFunksione.logout(Session,true,"FaqePaautorizuar");
            }
            int idNdermarrje;
            int idViti;
            int idPerdoruesi;
            int idGjuha;
            if (!hfState.Contains("idPerdoruesi"))
            {
                idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                hfState.Set("idPerdoruesi", idPerdoruesi);
            }
            else
                idPerdoruesi = (int)hfState["idPerdoruesi"];
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerdoruesi);
            }
            string vleraQueryString = "";
            if (Request.QueryString["idKonfigAmbjente"] != null && Request.QueryString["idKonfigAmbjente"] != "")
                vleraQueryString = Request.QueryString["idKonfigAmbjente"].ToString();
            idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idKonfigambjenti = DbCore.clsFunksione.getIdKonfigAmbLupa(vleraQueryString, idNdermarrje, "DetShp");
            if (!IsPostBack)
            {
                idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
                idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
                hfState.Set("idGjuha", idGjuha);
                hfState.Set("idViti", idViti);
                hfState.Set("idNdermarrje", idNdermarrje);
                percaktoTemplateMenu(idViti, idPerdoruesi, idNdermarrje, ASPxMenu1);
                if (DbCore.DbShare.clsAlternativaKushti.getAlternativa(idKonfigambjenti, "KSSH") == "Po")
                    hfState.Add("KSSH", true);
                else hfState.Add("KSSH", false);
                if (DbCore.DbShare.clsAlternativaKushti.getAlternativa(idKonfigambjenti, "ES") == "Po")
                    hfState.Add("ES", true);
                else hfState.Add("ES", false);
                GridUtil.AplikoFilterDefault(gvLupaDetajime, idKonfigambjenti);
                hfState.Set("idKonfigambjenti", idKonfigambjenti);
                konfiguroVleraFillestare(idPerdoruesi, idNdermarrje, idGjuha);
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvLupaDetajime", int.Parse(cmbKonfigurimi.Value.ToString()), "LupaDetajimShpejte.aspx");
                mbushPopUpListe(idPerdoruesi, idNdermarrje);
                konfiguroGride(idPerdoruesi, idNdermarrje, true, idKonfigambjenti, (bool)hfState["KSSH"], (bool)hfState["ES"]);
                //mbushPopUpListeArtikujshNgaDB(idPerdoruesi, idNdermarrje);
                //konfiguroPopupGride(idNdermarrje, idKonfigambjenti, true);
            }
            else
            {
                idNdermarrje = (int)hfState["idNdermarrje"];
                idKonfigambjenti = (int)hfState["idKonfigambjenti"];
                idViti = (int)hfState["idViti"];
                
                if (!IsCallback || (IsCallback && Request["__CALLBACKID"].ToString().Contains("ASPxMenu1")))
                    percaktoTemplateMenu(idViti, idPerdoruesi, idNdermarrje, ASPxMenu1);
                if (!IsCallback || (IsCallback && Request["__CALLBACKID"].ToString().Contains("gvLupaDetajime")))
                {
                    mbushPopUpListeNgaSession(idPerdoruesi, idNdermarrje);
                    konfiguroGride(idPerdoruesi, idNdermarrje, false, idKonfigambjenti, (bool)hfState["KSSH"], (bool)hfState["ES"]);
                }
            }
        }


        private void mbushPopUpListe(int idPerdorues, int idNdermarrje)
        {
            DbCore.DbInventari.colDetajimeArtikulli col = new DbCore.DbInventari.colDetajimeArtikulli();
            DataTable dt;
            int idartikulli = 0;
            int lloji = 0;

            if (!String.IsNullOrEmpty(Request.QueryString["idArtikulli"]) && Request.QueryString["idArtikulli"] != "undefined")
            {
                idartikulli = int.Parse(Request.QueryString["idArtikulli"]);
                //DbCore.DbInventari.clsArtikulli art = new DbCore.DbInventari.clsArtikulli(idartikulli);
                string kodartikulli = DbCore.DbInventari.clsArtikulli.ktheKodArtikulliSipasId(idartikulli);
                if (!String.IsNullOrEmpty(Request.QueryString["lloji"]))
                    lloji = int.Parse(Request.QueryString["lloji"]);
                dt = DbCore.DbInventari.colDetajimeArtikulli.ktheDetajimeSipasArtikullitNdermarrjesDheLlojit(kodartikulli, idNdermarrje, idPerdorues, lloji);
            }
            else if (!String.IsNullOrEmpty(Request.QueryString["veprimi"]) && Request.QueryString["veprimi"] != "undefined")
            {
                //dt = DbCore.DbInventari.colDetajimeArtikulli.ktheDetajimeSipasArtikullitNdermarrjesDheLlojit(artikulli, idNdermarrje, idPerdorues, 0);//bosh
                dt = DbCore.DbInventari.colDetajimeArtikulli.ktheDetajimeSipasNdermarrjesAndAutorizimeSipasKategorise(idNdermarrje, idPerdorues, int.Parse(Request.QueryString["veprimi"]));
            }
            else
            {
                dt = DbCore.DbInventari.colDetajimeArtikulli.ktheDetajimeSipasAutorizimeDheKategorise(idNdermarrje, idPerdorues);
            }

            DbCore.mySessionObjects.ruajGrideNeSessionLupa(Session, dt);
            gvLupaDetajime.DataSource = dt;
            gvLupaDetajime.DataBind();
        }

        private void mbushPopUpListeNgaSession(int idPerdoruesi, int idNdermarrje)
        {
            Object tmpObject;
            DbCore.mySessionObjects.merrGrideNgaSessioniLupa(Session, out tmpObject);
            if (tmpObject == null)
                mbushPopUpListe(idPerdoruesi, idNdermarrje);
            else
            {
                gvLupaDetajime.DataSource = tmpObject;
                gvLupaDetajime.DataBind();
            }
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
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, "LupaDetajimShpejte.aspx", this, MenuInfo, hfShtimModifikim.Value == "modifikim" ? false : true, true, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            aSPxMenu1.Items.FindByName("Anullo").Text = rm.GetString("MenuItemMbyll", ci);
        }

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(DbCore.mySessionObjects.ktheNdermarrjeVit(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), ASPxMenu1);
        }

        protected void gvDetajimArtikulli_DataBound(object sender, EventArgs e)
        {// shton colonen # per selektim dhe disa karakteristika te grides
            if (this.gvLupaDetajime.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
                //perzgjidh
                DevExpress.Web.GridViewCommandColumn check = new DevExpress.Web.GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true; check.Width = Unit.Percentage(2);
                gvLupaDetajime.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                gvLupaDetajime.Settings.ShowFilterRowMenu = true;
                check.SetColVisibleIndex(0);
                //behet per te afishuar rreshtin qe do sherbej per filtrim
                gvLupaDetajime.Settings.ShowFilterRow = true;
                gvLupaDetajime.Columns.Add(check);

                gvLupaDetajime.KeyFieldName = "IdDetajimArtikulli";
                gvLupaDetajime.SettingsBehavior.AllowSelectByRowClick = true;
                gvLupaDetajime.SettingsBehavior.AllowFocusedRow = true;
            }
        }

        protected void gvLupaDetajime_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string idkomponente = "";
            string kodkonfigurimi = "";
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                kodkonfigurimi = arr[1];
                idkomponente = arr[0];
                if (arr[2] == "")
                    gvLupaDetajime.FilterExpression = "";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvLupaDetajime", "LupaDetajimShpejte.aspx", idNdermarrje);
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], idNdermarrje, koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvLupaDetajime.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvLupaDetajime);
                    }
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
            gvLupaDetajime.Selection.UnselectAll();
        }

        protected void gvDetajimArtikulli_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            gvLupaDetajime.Selection.UnselectAll();
        }

        protected void gvDetajimArtikulli_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {

        }

        /// <summary>
        /// perdoret per te trajtuar ngjarjet e butonave te menuse. Shkon ne faqen e modifikimit te artikujve kur perdoruesi klikon butonin modifiko
        /// ose ne faqen e shtimit te artikujve kur perdoruesi klikon butonin shto
        /// </summary>
        /// <param name="source"> derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {//veprimet e menuse
            if (e.Item.Name == "Ruaj")
            {
                Page.Validate("entries");
                ruajDetajim();
            }
        }

        private void ruajDetajim()
        {
            if (!Page.IsValid)
                return;
            hfState.Add("veprim", "");
            int idArt = 0;
            string kodArt;
            int lloji;
            int idNdermarrje = (int)hfState["idNdermarrje"];
            int idPerdorues = (int)hfState["idPerdoruesi"];
            int idKonfigambjenti = (int)hfState["idKonfigambjenti"];
            DbCore.DbInventari.clsArtikulli artikulli = new DbCore.DbInventari.clsArtikulli();
            if (!String.IsNullOrEmpty(Request.QueryString["idArtikulli"]) && Request.QueryString["idArtikulli"] != "undefined")
            {
                idArt = int.Parse(Request.QueryString["idArtikulli"]);
                artikulli.mbushArtikull(idArt);
            }
            else if (!String.IsNullOrEmpty(Request.QueryString["kodArtikulli"]) && Request.QueryString["kodArtikulli"] != "undefined")
            {
                kodArt = Request.QueryString["kodArtikulli"].ToString();
                artikulli.merrSipasKodArtikullit(kodArt, idNdermarrje);
                idArt = artikulli.IdArtikulli;
            }
            if (Request.QueryString["lloji"] != null)
                lloji = int.Parse(Request.QueryString["lloji"]);
            else lloji = 0;

            
            
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (isValidDetajimi(artikulli.KodArtikulli, lloji, idArt, rm, ci))
            {
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                DbCore.DbInventari.clsDetajimArtikulli detajim = new DbCore.DbInventari.clsDetajimArtikulli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdorues, idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "LupaDetajimShpejte.aspx");
                if (!tedrejtaInfo.DShtim)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, STR_nukKeniTeDrejta, pnlMesazhi);
                    hfStatusi.Value = "true";
                    return;
                }
                string veprim = hfState["veprim"].ToString();
                try
                {
                    if (veprim == "Krijo" || veprim == "KrijoDheLidh")
                        detajim = krijoDetajim();
                    else
                    {
                        detajim.mbushDetajimArtikulli(txtKodi.Text, idNdermarrje);
                        detajim.mbushAutorizime(); //TOCHECK NESTILA - DUHET?
                    }
                }
                catch (Exception e)
                {
                    NLog.LogManager.GetCurrentClassLogger().Error(e.Message);
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, e.Message, pnlMesazhi);
                    hfStatusi.Value = "false";
                    return;
                }

                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                if (veprim == "Krijo")
                    mesazh = detajim.ruaj();
                else if (veprim == "KrijoDheLidh")
                    mesazh = detajim.ruajShpejte(artikulli, lloji);
                else //vetem do lidhet
                {
                    mesazh = DbCore.DbInventari.clsDetajimPerArt.ruajLidhje(artikulli, detajim.IdDetajimArtikulli, lloji, idNdermarrje, idPerdorues);
                }

                if (!mesazh.Status)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                    hfStatusi.Value = "false";
                }
                else
                {
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["msgRuajtjeMeSuksesGreen"], pnlMesazhi);
                    shtoDetajimNeGrid(idNdermarrje, idPerdorues, detajim.IdDetajimArtikulli, idKonfigambjenti);
                    hfStatusi.Value = "true";
                }
                pnlMesazhi.Update();
            }
        }

        private void shtoDetajimNeGrid(int idNdermarrje, int idPerdorues, int idDetajimi, int idKonfigAbmjente)
        {
            if (gvLupaDetajime.DataSource != null)
            {
                DataTable dt = (DataTable)gvLupaDetajime.DataSource;
                DataRow[] drs = dt.Select("IdDetajimArtikulli = " + idDetajimi);
                if (drs.Length > 0)
                    throw new Exception("GABIM: Detajimi ekziston ne gride");
                DataRow newArtDr = DbCore.DbInventari.clsDetajimArtikulli.merrDetajiminSipasId(idDetajimi);
                dt.ImportRow(newArtDr);
            }
            else mbushPopUpListe(idPerdorues, idNdermarrje);
            konfiguroGride(idPerdorues, idNdermarrje, false, idKonfigAbmjente, (bool)hfState["KSSH"], (bool)hfState["ES"]);
        }

        public DbCore.DbInventari.clsDetajimArtikulli krijoDetajim()
        {
            int kategori = DbCore.DbInventari.clsDetajimArtikulli.ktheIdKategoriDetajimi(cmbKategoria.Text);
            if (kategori == 0)
                throw new MyException(String.Format("Kategoria me vlere {0} nuk eshte e sakte!", kategori));

            int lloji = DbCore.DbInventari.clsDetajimArtikulli.ktheIdLlojDetajimi(cmbLloji.Text);
            if (lloji == 0)
                throw new MyException(String.Format("Lloji me vlere {0} nuk eshte i sakte!", lloji));

            String pershkrimi;
            if (kategori == 3 || kategori == 4)
                pershkrimi = " ";
            else pershkrimi = txtPershkrimi.Text;
            bool shtim = true;
            return new DbCore.DbInventari.clsDetajimArtikulli(txtKodi.Text, lloji, pershkrimi, (int)hfState["idPerdoruesi"], kategori, (int)hfState["idNdermarrje"], hfAutorizime.Value, 1, 0, shtim);
        }

        private bool isValidDetajimi(string kodArt, int lloji, int idartikulli, ResourceManager rm, CultureInfo ci)
        {
            bool isValid;
            isValid = true;
            int idNdermarrje = (int)hfState["idNdermarrje"];
            if (txtKodi.Text != "")
            {
                DbCore.clsMesazh msgKaraktereTePalejuara = DbCore.clsFunksione.kontrolloKaraktereMeMesazh(txtKodi.Text, FusheKontrolli.Kodi, false);
                if (!msgKaraktereTePalejuara.Status)
                {
                    isValid = false;
                    hfStatusi.Value = "false";
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, msgKaraktereTePalejuara.PershkrimMesazhi, pnlMesazhi);
                    return isValid;
                }
                if (kodArt != "" && idartikulli > 0) //kur po krijohet detajim per nje artikull te caktuar dhe do lidhet 
                {
                    if (DbCore.DbInventari.clsDetajimArtikulli.ekzistonDetajimLidhurMeArtikullin(txtKodi.Text, idNdermarrje, kodArt, lloji))
                    {
                        isValid = false;
                        hfStatusi.Value = "false";
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, STR_ekzistonKodDetajimiPerArtikull, pnlMesazhi);
                        return isValid;
                    }
                    else
                    {
                        if (!DbCore.DbInventari.clsDetajimArtikulli.ekziston(txtKodi.Text, idNdermarrje))
                            hfState.Set("veprim", "KrijoDheLidh");
                        else
                        {
                            isValid = false;
                            hfStatusi.Value = "false";
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ekziston nje detajim me kete kod!", pnlMesazhi);
                            return isValid;
                        }
                        //     hfState.Set("veprim", "Lidh");
                    }
                }
                else //kur nuk po krijohet per ndonje artikull te caktuar dhe s'do lidhet
                {
                    if (DbCore.DbInventari.clsDetajimArtikulli.ekziston(txtKodi.Text, idNdermarrje))
                    {
                        isValid = false;
                        hfStatusi.Value = "false";
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, STR_ekzistonKodDetajimi, pnlMesazhi);
                        return isValid;
                    }
                    else
                        hfState.Set("veprim", "Krijo");
                }
            }
            else
            {
                isValid = false;
                hfStatusi.Value = "false";
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, STR_shenoniKodin, pnlMesazhi);
                return isValid;
            }
            if (cmbKategoria.Value != null)
            {
                if (int.Parse(cmbKategoria.Value.ToString()) == 0)
                {
                    isValid = false;
                    hfStatusi.Value = "false";
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ju lutemi, zgjidhni kategorine e detajimit!", pnlMesazhi);
                    return isValid;
                }
                else
                {
                    if (int.Parse(cmbKategoria.Value.ToString()) == 1 && txtPershkrimi.Text == "")
                    {
                        isValid = false;
                        hfStatusi.Value = "false";
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, STR_shenoniEmertimin, pnlMesazhi);
                        return isValid;
                    }
                    if (int.Parse(cmbKategoria.Value.ToString()) == 3)
                    {
                        DateTime date;
                        bool dateVlefshme = DateTime.TryParseExact(txtKodi.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date);
                        if (!dateVlefshme)
                        {
                            isValid = false;
                            hfStatusi.Value = "false";
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, STR_DateSkadenceJoVlefshme, pnlMesazhi);
                            return isValid;
                        }
                    }
                }
            }
            else
            {
                isValid = false;
                hfStatusi.Value = "false";
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ju lutemi, zgjidhni kategorine e detajimit!", pnlMesazhi);
                return isValid;
            }
            if (cmbLloji.Value != null)
            {
                if (int.Parse(cmbLloji.Value.ToString()) == 0)
                {
                    isValid = false;
                    hfStatusi.Value = "false";
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ju lutemi, zgjidhni llojin e detajimit!", pnlMesazhi);
                    return isValid;
                }
                else
                {
                    if (int.Parse(cmbLloji.Value.ToString()) == 2 && int.Parse(cmbKategoria.Value.ToString()) == 2)
                    {
                        long numer = 0;
                        bool vlefshem = Int64.TryParse(txtPershkrimi.Text, out numer);
                        if (!vlefshem)
                        {
                            isValid = false;
                            hfStatusi.Value = "false";
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, STR_EmertimiNumerikGabim, pnlMesazhi);
                            return isValid;
                        }
                    }
                }
            }
            else
            {
                isValid = false;
                hfStatusi.Value = "false";
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ju lutemi, zgjidhni llojin e detajimit!", pnlMesazhi);
                return isValid;
            }
            return isValid;
        }

        private void konfiguroVleraFillestare(int idPerdoruesi, int idNdermarrje, int idGjuha)
        { //mbush komboboxet dhe gridat e faqes
            ConfigureAspxComboBox.mbushComboKategoriDetajimesh(cmbKategoria);
            //enableComboKategoriaDheLloji();
            mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 71, "DetShp", idGjuha);
            cmbKonfigurimi.SelectedIndex = 0;
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()));
            hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
        }

        private void enableComboKategoriaDheLloji()
        {
            int kategoria;
            if (!String.IsNullOrEmpty(Request.QueryString["veprimi"]))
                kategoria = int.Parse(Request.QueryString["veprimi"]);
            else kategoria = 0;

            switch (kategoria)
            {
                case 1: //Detajim
                    cmbKategoria.SelectedIndex = 0;
                    cmbKategoria.ClientEnabled = false;
                    cmbLloji.Items.Clear();
                    cmbLloji.Items.Add("Alfanumerik", 1);
                    cmbLloji.SelectedIndex = 0;
                    cmbLloji.ClientEnabled = false;
                    txtKodi.ClientEnabled = true;
                    txtPershkrimi.ClientEnabled = true;
                    break;
                case 2://Serial
                    cmbKategoria.SelectedIndex = 1;
                    cmbKategoria.ClientEnabled = false;
                    cmbLloji.Items.Clear();
                    cmbLloji.Items.Add("Alfanumerik", 1);
                    cmbLloji.Items.Add("Numerik", 2);
                    cmbLloji.ClientEnabled = true;
                    txtKodi.ClientEnabled = true;
                    txtPershkrimi.ClientEnabled = true;
                    break;
                case 3://date skadence
                    cmbKategoria.SelectedIndex = 2;
                    cmbKategoria.ClientEnabled = false;
                    cmbLloji.Items.Clear();
                    cmbLloji.Items.Add("Date", 3);
                    cmbLloji.ClientEnabled = false;
                    cmbLloji.SelectedIndex = 0;
                    txtPershkrimi.ClientEnabled = false;
                    break;
                case 4://seri
                    cmbKategoria.SelectedIndex = 3;
                    cmbKategoria.ClientEnabled = false;
                    cmbLloji.Items.Clear();
                    cmbLloji.Items.Add("Alfanumerik", 1);
                    cmbLloji.ClientEnabled = false;
                    cmbLloji.SelectedIndex = 0;
                    txtPershkrimi.ClientEnabled = false;
                    break;
                case 0:
                default:
                    cmbLloji.Items.Clear();
                    cmbKategoria.SelectedIndex = -1;
                    cmbLloji.ClientEnabled = true;
                    cmbKategoria.ClientEnabled = true;
                    txtKodi.ClientEnabled = true;
                    txtPershkrimi.ClientEnabled = true;
                    break;
            }
        }

        public void mbushComboKonfigurimeshSipasKategorise(int idPerdoruesi, int idNdermarrje, ASPxComboBox combo, int kat, string nivel, int idGjuha)
        {
            //mbush griden e popupit me te dhena
            DbCore.DbShare.colKonfigurimAmbjenti col = new DbCore.DbShare.colKonfigurimAmbjenti();
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            if (kat != 0)
            {
                konf.IdKategori = kat;
                konf.IdNdermarje = idNdermarrje;
                //DbCore.DbRegjistrim.clsNivelRegjistrimi niv = new DbCore.DbRegjistrim.clsNivelRegjistrimi();
                //niv.mbushNivelRegjistrimiSipasKodiMeKonvertime(nivel, idNdermarrje);
                int idNivel = DbCore.DbRegjistrim.clsNivelRegjistrimi.ktheIdNivelRegjistrimiSipasKodi(nivel, idNdermarrje);
                col.mbushKonfigAmbjSipasIdKategoriIdNivelMeLloj(konf.IdKategori, idNivel, idPerdoruesi);
            }
            else
                col.mbushGjitheKonfigurimeAmbjentesh(idNdermarrje, idPerdoruesi, 1, idGjuha);
            ListBoxColumn colprove = new ListBoxColumn();
            colprove.FieldName = "KodKonfigAmbjente";
            colprove.Caption = "Kodi";
            ListBoxColumn colemer = new ListBoxColumn();
            colemer.FieldName = "PershkrimKonfigAmbjente";
            colemer.Caption = "Pershkrimi";
            combo.TextFormatString = "{0}";
            combo.Columns.Add(colprove);
            combo.Columns.Add(colemer);
            combo.DataSource = col;
            combo.ValueField = "IdKonfigAmbjente";
            combo.IncrementalFilteringMode = DevExpress.Web.IncrementalFilteringMode.Contains;
            combo.DataBind();
        }


        private void konfiguroGride(int idPerdoruesi, int idNdermarrje, bool visibleIndex, int idKonfigambjenti, bool kerkosaposhkruar, bool endlessScroll)
        {//konfigurohet grida
            shto_Lloj();
            //shto_Autorizim();
            shto_Kategori();
            GridUtil.percaktoVisibleColumnsSipasKonfigurimit(gvLupaDetajime, "gvLupaDetajime", "LupaDetajimShpejte.aspx", idKonfigambjenti, visibleIndex, DbCore.mySessionObjects.ktheGjuhe(Session));
            //funk.konfiguroGrideListeMadhe(gvLupaDetajime, "IdDetajimArtikulli");
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            GridUtil.KonfiguroGrideListeMadhePopupiPaTheme(gvLupaDetajime, "IdDetajimArtikulli", kerkosaposhkruar, endlessScroll);
        }


        private void shto_Lloj()
        {//shtohen komboja me Autorizimeve tek grida 

            //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            int visibleindex = gvLupaDetajime.Columns["LlojDetajimArtikulli"].VisibleIndex;
            gvLupaDetajime.Columns.Remove(gvLupaDetajime.Columns["LlojDetajimArtikulli"]);
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            colnew.PropertiesComboBox.Items.Add("Alfanumerik", 1);
            colnew.PropertiesComboBox.Items.Add("Numerik", 2);
            colnew.PropertiesComboBox.Items.Add("Date", 3);
            // colnew.PropertiesComboBox.Items.Add("Garanci", 4);
            colnew.VisibleIndex = visibleindex;
            colnew.FieldName = "LlojDetajimArtikulli";
            gvLupaDetajime.Columns.Add(colnew);
        }

        private void shto_Kategori()
        {//shtohen komboja  tek grida 

            //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            int visibleindex = gvLupaDetajime.Columns["KategoriDetajimi"].VisibleIndex;
            gvLupaDetajime.Columns.Remove(gvLupaDetajime.Columns["KategoriDetajimi"]);
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            colnew.PropertiesComboBox.Items.Add("Detajim 1", 1);
            colnew.PropertiesComboBox.Items.Add("Detajim 2", 2);
            colnew.FieldName = "KategoriDetajimi";
            colnew.VisibleIndex = visibleindex;
            gvLupaDetajime.Columns.Add(colnew);
        }
    }
}
