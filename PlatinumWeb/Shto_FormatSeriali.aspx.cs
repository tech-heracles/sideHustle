using DevExpress.Web;
using PlatinumWeb.Templates;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using DbCore;
using System.Linq;
using System.Resources;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.DbInventari;
using Newtonsoft.Json;
using DbCore.IMBUtils.Extensions;
using DbCore.DbAdmin;
using DbCore.IMBUtils.Messages;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;

namespace PlatinumWeb
{
    public partial class Shto_FormatSeriali : MyPageBase
    {
        private const string prefixMesazhNjejes = "Formati me Kod: ";
        private const string prefixMesazhShumes = "Formatet me Kod: ";
        private const string suffixMesazhNjejesGabimiLidhur = " eshte i lidhur dhe nuk mund te fshihet!";
        private const string suffixMesazhShumesGabimiLidhur = " jane te lidhur dhe nuk mund te fshihen!";
        private const string suffixMesazhNjejesSuksesi = " u fshi me sukses!";
        private const string suffixMesazhShumesSuksesi = " u fshine me sukses!";
        private const string lidhesMesazhi = " Kurse ";
        private const string gabim2burimenegride = "GABIM: Ndodhen 2 formate me te njejten id ne gride";
        private const string mesazhZgjidhniNje = "Ju lutem zgjidhni te pakten nje format!";
        private int idperdoruesi, idnderviti, idNdermarrje, idviti, idgjuha;
        private bool lupe;
        private const int idstatusdok = 1;
        /// <summary>
        /// perdoret per te vendosur themen e devexpresit faqes
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>

        private string komponente = "Shto_FormatSeriali.aspx";
        private string guidString;
        /// <summary>
        /// thirret kur faqja lodohet. Ne te behet kontrolli nese perdoruesi eshte i loguar ne sistem  dhe nqs jo ridrejtohet tek forma e logimit
        /// thirret inicializimi i konfigurimeve fillestare te faqes
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e"> argumenti</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                Response.Redirect(DbCore.IMBUtils.Paths.defaultLoginPath);
                return;
            }
            idperdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idperdoruesi);
                return;
            }
            idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            idviti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            idgjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            idnderviti = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
            lupe = false;
            if (!String.IsNullOrEmpty(Request.QueryString["lupe"]) && Request.QueryString["lupe"] == "true")
                lupe = true;
            else
                lupe = false;
            hfState.Set("lupe", lupe);
            CultureInfo cultinf = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo(idgjuha);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (!Page.IsPostBack)
            {
                guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                hfState.Set("guidString", guidString);
                EmrateTabeve(rm, cultinf);
                hfState.Set("idGjuha", idgjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                ASPxPageControl1.ActiveTabIndex = 0;
                percaktoTemplateMenu(idgjuha, idviti, idperdoruesi, idNdermarrje, ASPxMenu1, lupe);
                konfiguroVleraFillestare(idNdermarrje, rm, cultinf, idgjuha);
                mbushComboBoxFiltra(idNdermarrje, idgjuha, int.Parse(cmbKonfigurimi.Value.ToString()));
                mbushGridFormatSerialiNgaDB(idNdermarrje);
                konfiguroGrideTrupi(idNdermarrje);
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idperdoruesi, idNdermarrje, idviti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idperdoruesi, idNdermarrje, idviti, komponente);
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                konfiguroGride(idNdermarrje, idperdoruesi, cmbKonfigurimi.Text.Split(';')[0], 3061, rm, cultinf);
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "ASPxGridView_FormatSeriali", ASPxGridView_FormatSeriali, cmbKonfigurimi.Text.Split(';')[0], 3061.ToString(), DbCore.mySessionObjects.ktheGjuhe(Session));
            }
            else
            {
                guidString = (string)hfState["guidString"];
                mbushGridFormatSerialiNgaSession(idNdermarrje);
                konfiguroGride(idNdermarrje, idperdoruesi, cmbKonfigurimi.Text.Split(';')[0], 3061, rm, cultinf);
            }
            if (!lupe)
                percaktoTemplateTrupash();
            GridUtil.konfigGrideListeEMadhePaTheme(ASPxGridView_FormatSeriali, "idFormatSerialesh");
            percaktoTemplateMenu(idgjuha, idviti, idperdoruesi, idNdermarrje, ASPxMenu1, lupe);
            mbushComboBoxFiltra(idNdermarrje, idgjuha, int.Parse(cmbKonfigurimi.Value.ToString()));
            GridUtil.ToolTipButonaveMbiGride(ASPxGridView_FormatSeriali, cultinf, rm);
            ASPxGridView_FormatSeriali.Columns["#"].VisibleIndex = 0;
        }

        /// <summary>
        /// Vendos emrat e tabeve ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateTabeve(ResourceManager rm, CultureInfo cultinf)
        {
            ASPxPageControl1.TabPages[0].Text = rm.GetString("labelRaportTePergjithshme", cultinf);
            ASPxPageControl1.TabPages[1].Text = rm.GetString("KarakteristikaTab", cultinf);
            konfigurimi_Label.Text = rm.GetString("lblModeli", cultinf);
        }

        /// <summary>
        /// mbush combon e filtrave
        /// </summary>
        /// <param name="idNdermarrje">id e ndermarjes</param>
        private static void mbushComboBoxFiltra(int idNdermarrje, int idgjuhe, int idkonfig)
        {
            clsToolbarConfig.mbushComboBoxFiltraMeValFieldTextField(idgjuhe, idNdermarrje, "ASPxGridView_FormatSeriali", "Shto_FormatSeriali.aspx", "IdFiltra", "FiltraShenime", idkonfig);
        }
        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="idNderVit"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        private void percaktoTemplateMenu(int idGjuha, int idViti, int idPerdorues, int idNdermarrje, DevExpress.Web.ASPxMenu aSPxMenu1, bool lupe)
        {
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, komponente, this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, hfShtimModifikim.Value == "modifikim" ? false : true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
            if (!lupe)
            {
                aSPxMenu1.Items.FindByName("OK").Visible = false;
                aSPxMenu1.Items.FindByName("Anullo").Visible = false;
            }
            else
            {
                aSPxMenu1.Items.FindByName("Ruaj").Visible = false;
                aSPxMenu1.Items.FindByName("Shto").Visible = false;
                aSPxMenu1.Items.FindByName("Modifiko").Visible = false;
                aSPxMenu1.Items.FindByName("Fshi").Visible = false;
                aSPxMenu1.Items.FindByName("TemplatedItemFrame").Visible = false;
                aSPxMenu1.Items.FindByName("Anullo").Text = MessagesResource.Messages["MenuItemMbyll"];
                ASPxPageControl1.TabPages[1].Visible = false;
            }
        }


        protected void RuajKolona_Click(object sender, EventArgs e)
        {
            int idfiltri = 0;
            int idkonf = DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(cmbKonfigurimi.Text.Split(';')[0], idNdermarrje);
            DbCore.clsMesazh mesazh = GridUtil.ruajFiltra(idNdermarrje, idperdoruesi, idgjuha, "ASPxGridView_FormatSeriali ", komponente, "FilterDefault", ASPxGridView_FormatSeriali.FilterExpression, ASPxGridView_FormatSeriali, "Kodi", idkonf, out idfiltri);//ruan filtrin e zgjedhur tek filtrat
            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                return;

            }
            mesazh = GridUtil.ruajkonfigurimgride(ASPxGridView_FormatSeriali, cmbKonfigurimi.Text, idNdermarrje, idperdoruesi, 3061, idfiltri, idviti, DbCore.mySessionObjects.ktheCultureInfo(Session), DbCore.mySessionObjects.ktheGjuhe(Session));/// ruan konfigurimin e grides dhe filtrin

            clsToolbarConfig.mbushComboBoxFiltra(idgjuha, idNdermarrje, "ASPxGridView_FormatSeriali ", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);

            percaktoTemplateMenu(idgjuha, idviti, idperdoruesi, idNdermarrje, ASPxMenu1, lupe);
            if (mesazh.Status) clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

        }
        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(idgjuha, idviti, idperdoruesi, idNdermarrje, ASPxMenu1, lupe);

        }

        /// <summary>
        /// perdoret per te shtuar kolonen e selektimit tek grida dhe per te vendosur disa karakteristika te grides
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void ASPxGridView_FormatSeriali_DataBound(object sender, EventArgs e)
        {// shton colonen # per selektim dhe disa karakteristika te grides
            if (ASPxGridView_FormatSeriali.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
                //perzgjidh
                GridViewCommandColumn check = new GridViewCommandColumn("#") { ShowSelectCheckbox = true, Width = Unit.Percentage(2) };
                //behet per te afishuar rreshtin qe do sherbej per filtrim
                ASPxGridView_FormatSeriali.Settings.ShowFilterRow = true;
                ASPxGridView_FormatSeriali.Settings.ShowFilterRowMenu = true;
                ASPxGridView_FormatSeriali.Columns.Add(check);
                ASPxGridView_FormatSeriali.KeyFieldName = "idFormatSerialesh";
                ASPxGridView_FormatSeriali.SettingsBehavior.AllowSelectByRowClick = true;
                ASPxGridView_FormatSeriali.SettingsBehavior.AllowFocusedRow = true;
            }

        }

        /// <summary>
        /// perdoret per te konfiguruar griden. Per te dhenat mbi kolonat qe do te shfaqen nga databaza si dhe percakton disa karakteristika te grides
        /// </summary>
        /// :  <see cref="GridUtil.percaktoVisibleColumns"/>
        /// :  <see cref="DbCore.clsFunksione.konfiguroGrideListeMadhe(grida,celesigrides)"/>
        /// <param name="idNdermarrje">id e ndermarjes</param>
        /// <param name="idKomponente"> id e komponentes</param>
        /// <param name="kodKonfigurimi">kodi i konfigurimit</param>
        private void konfiguroGride(int idNdermarrje, int idPerdoruesi, string kodKonfigurimi, int idKomponente, ResourceManager rm, CultureInfo ci)
        {
            ASPxGridView_FormatSeriali.Columns["#"].VisibleIndex = 0;
        }

        /// <summary>
        /// perdoret ne rastet kur grida ben callback per ta rimbushur griden
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void ASPxGridView_FormatSeriali_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            if (e.CallbackName == "COLUMNMOVE" && ASPxGridView_FormatSeriali.AllColumns[int.Parse(e.Args[0])].Width.Value == 0)
                ASPxGridView_FormatSeriali.AllColumns[int.Parse(e.Args[0])].Width = Unit.Percentage(3);
            if (e.CallbackName == "APPLYFILTER" && e.Args[0] == "")
            {
                DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
                ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
                cmbFiltra.Text = "";

            }
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            GridUtil.ToolTipButonaveMbiGride(ASPxGridView_FormatSeriali, cultinf, rm);


        }



        /// <summary>
        /// perdoret per te vendosur filtrat tek headeri i grides
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxGridView_FormatSeriali_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            if (e.Column.FieldName == "Emertimi" || e.Column.FieldName == "Kategori")
            {
                e.Values.Clear();
                e.AddValue("(Te gjithe)", string.Empty, "true");
                e.AddValue("Nga A-D ", string.Empty, String.Format("{0}>'A     ' and {0}<'DDDDDDD'", e.Column.FieldName));
                e.AddValue("Nga D-G ", string.Empty, String.Format("{0}>'D     ' and {0}<'GGGGGGG'", e.Column.FieldName));
                e.AddValue("Nga H-K ", string.Empty, String.Format("{0}>'H     ' and {0}<'KKKKKKK'", e.Column.FieldName));
                e.AddValue("Nga L-O ", string.Empty, String.Format("{0}>'L     ' and {0}<'OOOOOOO'", e.Column.FieldName));
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

        /// <summary>
        /// fshin filtrin e zgjedhur dhe aplikuar mbi gride, fshirje nga DB ku ai eshte ruajtur, dhe jo pastrimi i grides nga aplikimi i filtrit
        /// per kete do sherbeje Clear Filter ne fund te grides
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "ASPxGridView_FormatSeriali", komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                mbushComboBoxFiltra(idNdermarrje, DbCore.mySessionObjects.ktheGjuhe(Session), int.Parse(cmbKonfigurimi.Value.ToString()));
                percaktoTemplateMenu(idgjuha, idviti, idperdoruesi, idNdermarrje, ASPxMenu1, lupe);

                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                cmbFiltra.Text = "";

                hfStatusi.Value = "true";

            }
        }

        /// <summary>
        /// sherben per te ruajtur filtrin e zgjedhur
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;

            DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida() { FiltraKodi = cmbFiltra.Text, FiltraShenime = cmbFiltra.Text, FiltraUniversal = false };
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "ASPxGridView_FormatSeriali", komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = ASPxGridView_FormatSeriali.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("Kategori", ASPxGridView_FormatSeriali);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = ASPxGridView_FormatSeriali.GetSortedColumns();
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
            //    filtri.KoloneRenditje = "Kategori";
            //    filtri.DrejtimRenditje = true;
            //}
            DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();
            oPerdorues = DbCore.mySessionObjects.kthePerdorues(Session);
            filtri.IdPerdoruesi = oPerdorues.IdPerdorues;
            filtri.IdNdermarje = idNdermarrje;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            filtri.IdStatusDok = 1;

            mesazh = filtri.ruaj();
            mbushComboBoxFiltra(idNdermarrje, DbCore.mySessionObjects.ktheGjuhe(Session), int.Parse(cmbKonfigurimi.Value.ToString()));
            percaktoTemplateMenu(idgjuha, idviti, idperdoruesi, idNdermarrje, ASPxMenu1, lupe);

            if (mesazh.Status == true)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            hfStatusi.Value = "true";
            cmbFiltra.Text = "";

        }

        /// <summary>
        /// perdoret per te fshire rreshtat e zgjedhur te bankave nqs perdoruesi konfirmon fshirjen
        /// </summary>
        /// 
        /// <param name="sender"> derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ButtonOk_Click2(object sender, EventArgs e)
        {//fshin rreshtat e selektuar            
            List<object> rreshtat;
            if (ASPxPageControl1.ActiveTabIndex == 0)//nqs kemi zgjedhur vetem nje kartele per te pare fshijme vetem ate
                rreshtat = ASPxGridView_FormatSeriali.GetSelectedFieldValues("idFormatSerialesh");
            else
            {
                rreshtat = new List<object>();
                rreshtat.Add(hfId.Value);
            }
            if (rreshtat.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhZgjidhniNje, pnlMesazhi);
                return;
            }
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            //clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "", pnlMesazhi);
            List<string> TeFshire = new List<string>(), TePaFshire = new List<string>();
            DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegjistrim = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();

            foreach (object id in rreshtat)
            {
                DbCore.DbInventari.clsSerialeUnikeFormate serial = new DbCore.DbInventari.clsSerialeUnikeFormate(Convert.ToInt32(id));
                DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti() { IdKonfigAmbjente = int.Parse(cmbKonfigurimi.Value.ToString()) };
                konf.mbushKonfigAmbjSipasId(konf.IdKonfigAmbjente);
                bool lidhur = dbRegjistrim.eshteDokumentiILidhurCelje(serial.ID.ToString(), konf.IdNivel.ToString());
                if (lidhur)
                {
                    TePaFshire.Add(serial.Kod);
                    continue;
                }
                serial.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                mesazh = serial.Fshi();
                if (serial.ID == 0)
                    continue;
                if (mesazh.Status)
                {
                    #region Heq llogarite nga grida
                    hiqNgaGrida(serial.ID, serial.IdNdermarje);
                    #endregion
                    TeFshire.Add(serial.Kod);

                    ASPxPageControl1.ActiveTabIndex = 0;
                    hfStatusi.Value = "true";
                }
            }
            dbRegjistrim.Dispose();
            string mesazhInfoGabim = "", mesazhInfoSukses = "";
            if (TePaFshire.Count == 1)
                mesazhInfoGabim = String.Format("{0}{1}{2}", prefixMesazhNjejes, String.Join(";", TePaFshire), suffixMesazhNjejesGabimiLidhur);
            else
                if (TePaFshire.Count > 1)
                mesazhInfoGabim = String.Format("{0}{1}{2}", prefixMesazhShumes, String.Join(";", TePaFshire), suffixMesazhShumesGabimiLidhur);
            if (TeFshire.Count == 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", prefixMesazhNjejes, String.Join(";", TeFshire), suffixMesazhNjejesSuksesi);
            else
                if (TeFshire.Count > 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", prefixMesazhShumes, String.Join(";", TeFshire), suffixMesazhShumesSuksesi);
            if (mesazhInfoGabim != "" && mesazhInfoSukses != "")
                mesazhInfoGabim += lidhesMesazhi + mesazhInfoSukses;
            if (mesazhInfoGabim != "")
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhInfoGabim, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazhInfoSukses, pnlMesazhi);

        }

        /// <summary>
        /// heq nga grida rreshtat e fshire
        /// </summary>
        /// <param name="idndermarje">id e ndermarjes</param>
        ///<param name="idburim"> id e burimit</param>
        private void hiqNgaGrida(int id, int idndermarje)
        {
            if (ASPxGridView_FormatSeriali.DataSource != null)
            {
                DataTable dt = (DataTable)ASPxGridView_FormatSeriali.DataSource;
                DataRow[] drs = dt.Select(String.Format("idFormatSerialesh = '{0}'", id));
                if (drs.Length > 1)
                    throw new Exception(gabim2burimenegride);
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                dt.Rows.Remove(dr);
                ASPxGridView_FormatSeriali.DataBind();
            }
            else
                mbushGridFormatSerialiNgaDB(idndermarje);
        }

        /// <summary>
        /// shton nte gride rreshtat e rinj
        /// </summary>
        /// <param name="idburim"> id e burimit</param>
        /// <param name="idNdermarrje">id e ndermarjes</param>
        private void shtoNeGrid(int idNdermarrje, int id)
        {
            if (ASPxGridView_FormatSeriali.DataSource != null)
            {
                DataTable dt = (DataTable)ASPxGridView_FormatSeriali.DataSource;
                DataRow[] drs = dt.Select("idFormatSerialesh = " + id);
                if (drs.Length > 0)
                    throw new Exception(gabim2burimenegride);
                DataRow newArtDr = DbCore.DbInventari.colSerialeUnikeFormate.merrSerialeNdermarjeDR(id);
                if (newArtDr != null)
                    dt.ImportRow(newArtDr);
                else
                    mbushGridFormatSerialiNgaDB(idNdermarrje);
            }
            else
                mbushGridFormatSerialiNgaDB(idNdermarrje);
        }

        /// <summary>
        /// modifikon ne gride komponenten e modifikuar
        /// </summary>
        /// <param name="idburim"> id e burimit</param>
        /// <param name="idNdermarrje">id e ndermarjes</param>
        private void modifikoNeGrid(int idNdermarrje, int id)
        {
            if (ASPxGridView_FormatSeriali.DataSource != null)
            {
                DataTable dt = (DataTable)ASPxGridView_FormatSeriali.DataSource;
                DataRow[] drs = dt.Select("idFormatSerialesh = " + id);
                if (drs.Length > 1)
                    throw new Exception(gabim2burimenegride);
                if (drs.Length == 0) return;
                    DataRow dr = drs[0];
                DataRow newArtDr = DbCore.DbInventari.colSerialeUnikeFormate.merrSerialeNdermarjeDR(id);
                if (newArtDr != null)
                {
                    object[] arr = newArtDr.ItemArray;
                    dr.ItemArray = arr;
                }
                else mbushGridFormatSerialiNgaDB(idNdermarrje);
            }
            else mbushGridFormatSerialiNgaDB(idNdermarrje);
        }

        /// <summary>
        /// perdoret per te trajtuar ngjarjet e butonave te menuse. 
        /// </summary>
        /// <param name="source"> derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {//kryen veprimet e menuse
            if (e.Item.Name == "Ruaj")
            {
                Page.Validate("entries");
                ruajFormatSeriali();
            }

        }

        /// <summary>
        /// konfiguron vlerat fillestare
        /// </summary>
        /// <param name="idNdermarrje">id e ndermarjes</param>
        private void konfiguroVleraFillestare(int idNdermarrje, ResourceManager rm, CultureInfo cultinf, int idGjuha)
        { //mbush komboboxet dhe gridat e faqes
            ASPxPageControl1.ActiveTabIndex = 0;
            mbushListeTrupash();
            ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idperdoruesi, idNdermarrje, cmbKonfigurimi, 167, rm, cultinf, idGjuha);
            cmbKonfigurimi.SelectedIndex = 0;
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()), idGjuha);
            hfKonffillestar.Value = String.Format("{0};{1}", konf.KodKonfigAmbjente, konf.PershkrimKonfigAmbjente);
            ConfigureAspxComboBox.mbushComboKategoriSeriali(idNdermarrje, cmbKategoriaFormat);
        }

        /// <summary>
        /// mbush griden nga sesioni
        /// </summary>
        /// <param name="idNdermarrje">id e ndermarjes</param>
        private void mbushGridFormatSerialiNgaSession(int idNdermarrje)
        {
            DataTable tmpObject;
            bool sukses = DbCore.mySessionObjects.merrGrideNgaSessioni(Session, out tmpObject);
            if (!sukses)
                mbushGridFormatSerialiNgaDB(idNdermarrje);
            else
            {
                ASPxGridView_FormatSeriali.DataSource = tmpObject;
                ASPxGridView_FormatSeriali.DataBind();
                tmpObject.Dispose();
            }
        }

        /// <summary>
        /// mbush griden nga databaza
        /// </summary>
        /// <param name="idNdermarrje"> id e ndermarjes</param>
        private void mbushGridFormatSerialiNgaDB(int idNdermarrje)
        {//mbush griden e popupit me te dhena            
            DataTable dt = DbCore.DbInventari.colSerialeUnikeFormate.merrSerialeNdermarjeDT(idNdermarrje);
            DbCore.mySessionObjects.ruajGrideNeSession(Session, dt);
            ASPxGridView_FormatSeriali.DataSource = dt;
            ASPxGridView_FormatSeriali.DataBind();
            dt.Dispose();
        }

        /// <summary>
        /// ruan burimet 
        /// </summary>
        private void ruajFormatSeriali()
        {
            clsSerialeUnikeFormate formati = new clsSerialeUnikeFormate();
            if (Page.IsValid == false)
                return;
            try
            {
                formati = krijoFormat();
            } catch (Exception e)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(e.Message);
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, e.Message, pnlMesazhi);
                hfStatusi.Value = "false";
                return;
            }
            bool eshteShtim;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idperdoruesi, idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), komponente);

            if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
            {
                if (!tedrejtaInfo.DShtim)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                    hfStatusi.Value = "false";
                    return;
                }
                mesazh = formati.Ruaj();
                eshteShtim = true;
            }
            else
            {
                if (!tedrejtaInfo.DMod)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                    hfStatusi.Value = "false";
                    return;
                }
                DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegjistrim = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
                DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti() { IdKonfigAmbjente = int.Parse(cmbKonfigurimi.Value.ToString()) };
                konf.mbushKonfigAmbjSipasId(konf.IdKonfigAmbjente);
                eshteShtim = false;
                formati.ID = int.Parse(hfId.Value);
                bool lidhur = dbRegjistrim.eshteDokumentiILidhurCelje(formati.ID.ToString(), konf.IdNivel.ToString());
                if (lidhur.ToString() != hfLidhur.Value)
                {
                    mesazh.Status = false;
                    mesazh.PershkrimMesazhi = "Formati eshte i lidhur.";
                }
                else
                    mesazh = formati.Modifiko();
                dbRegjistrim.Dispose();
            }
            if (mesazh.Status == true)
            {
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                if (eshteShtim)
                    shtoNeGrid(idNdermarrje, formati.ID);
                else //modifikim
                    modifikoNeGrid(idNdermarrje, formati.ID);
                hfStatusi.Value = "true";
            }
            else
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                hfStatusi.Value = "false";
            }
            ASPxPageControl1.ActiveTabIndex = 0;
        }

        /// <summary>
        /// krijon burimin sipas te dhenave
        /// </summary>
        /// <returns> burimin me te dhenat</returns>
        private clsSerialeUnikeFormate krijoFormat()
        {            
            bool isshtim;
            if (hfShtimModifikim.Value == "shtim")
                isshtim = true;
            else
                isshtim = false;
            clsSerialeUnikeKategori kategoria = new clsSerialeUnikeKategori(cmbKategoriaFormat.Text, idNdermarrje);
            clsSerialeUnikeFormate format = new clsSerialeUnikeFormate(txtKodiFormat.Text, txtPershkrimFormat.Text, kategoria.ID, idperdoruesi, idNdermarrje, idstatusdok, cbAktiv.Checked);
            format.ColSeriale = ruajTrup(idNdermarrje);
            return format;
        }

        /// <summary>
        /// cakton properti ne javascript
        /// </summary>
        /// <param name="sender">derguesi </param>
        /// <param name="e">argumentat</param>
        protected void ASPxGridView_FormatSeriali_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = ASPxGridView_FormatSeriali.PageIndex;
            e.Properties["cpPageRow"] = ASPxGridView_FormatSeriali.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = ASPxGridView_FormatSeriali.VisibleRowCount;
        }

        /// <summary>
        /// kur grida ben callback
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxGridView_FormatSeriali_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string idkomponente = "";
            string kodkonfigurimi = "";

            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                kodkonfigurimi = arr[1];
                idkomponente = arr[0];
                if (arr[2] == "")
                    ASPxGridView_FormatSeriali.FilterExpression = "";
                else
                {
                    clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "ASPxGridView_FormatSeriali", komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], idNdermarrje, koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        ASPxGridView_FormatSeriali.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, ASPxGridView_FormatSeriali);
                        hfStatusi.Value = "true";
                    }
                    else hfStatusi.Value = "false";
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

            ASPxGridView_FormatSeriali.Selection.UnselectAll();
        }

        /// <summary>
        /// perdoret per te shfaqur po jo tek komboja e aktivit
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxGridView_FormatSeriali_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
        }

        /// <summary>
        /// per filtrimin me elementin bosh
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxGridView_FormatSeriali_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
        }
        

        #region Grida e serialeve


        /// <summary>
        /// konfiguron griden e burimeve
        /// </summary>
        /// <param name="idNdermarrje">id e ndermarjes</param>
        private void konfiguroGrideTrupi(int idNdermarrje)
        {//konfigurohet grida
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            GridUtil.percaktoVisibleColumnsMeWidth(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, gvSeriali, "gvSeriali", komponente);
            GridUtil.konfiguroGrideRegjistrimEvogelPaTheme(gvSeriali, "ID", false);
            gvSeriali.Settings.UseFixedTableLayout = false;
            // gvAktivitetet.Columns["Fshi"].VisibleIndex = 14;
        }

        /// <summary>
        /// mbush griden me rreshta bosh
        /// </summary>
        private void mbushListeTrupash()
        {//mbushet grida me te dhena            
            DbCore.DbInventari.colSerialeUnike col = new DbCore.DbInventari.colSerialeUnike();
            for (int i = 0; i < 5; i++)
            {
                DbCore.DbInventari.clsSerialeUnike o = new DbCore.DbInventari.clsSerialeUnike();
                col.Add(o);
            }
            gvSeriali.DataSource = col;
            gvSeriali.DataBind();
        }

        /// <summary>
        /// mbush griden e burimeve me burimet e ketij aktiviteti
        /// </summary>
        /// <param name="id">id e aktivitetit</param>
        private void mbushListeTrupashMod(int id)
        {//mbushet grida me te dhena
            DbCore.DbInventari.colSerialeUnike col = new DbCore.DbInventari.colSerialeUnike();
            col.mbushSipasFormatit(id);
            col.Add(new DbCore.DbInventari.clsSerialeUnike());
            gvSeriali.DataSource = col;
            gvSeriali.DataBind();
        }

        /// <summary>
        /// percakton templatet e kolonave te grides
        /// </summary>
        private void percaktoTemplateTrupash()
        {//percaktohen templatet per fushat e grides
            GridViewDataTextColumn col0 = gvSeriali.Columns["Fshi"] as GridViewDataTextColumn;
            col0.DataItemTemplate = new MyButtonTemplate("");
            col0.VisibleIndex = 14;
            GridViewDataTextColumn col1 = gvSeriali.Columns["Kodi"] as GridViewDataTextColumn;
            col1.DataItemTemplate = new MyComboTemplate();

        }

        /// <summary>
        /// perdoret ne rastet kur grida ben callback per ta rimbushur griden
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void gvSeriali_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            gvSeriali.DataBind();
        }

        /// <summary>
        /// kur grida ben callback
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvSeriali_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {//kur grida ben callback te ruajme te dhenat
            int key = -1;
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("gvSeriali"))
                {
                    if (e.Parameters.Split(';').Length == 2)
                    {
                        if (e.Parameters.Split(';')[1] == "modifiko")
                        {

                            mbushListeTrupashMod(int.Parse(e.Parameters.Split(';')[0]));
                        }
                        else
                        {
                            mbushListeTrupash();
                        }

                    }
                    else
                    {


                        if (e.Parameters.ToString() != "")
                        {
                            key = int.Parse(e.Parameters.ToString());
                        }
                        DbCore.DbInventari.colSerialeUnike trupi = new DbCore.DbInventari.colSerialeUnike();
                        DbCore.DbInventari.clsSerialeUnike tr = new DbCore.DbInventari.clsSerialeUnike();
                        JavaScriptSerializer serializusi = new JavaScriptSerializer();
                        int rreshta = gvSeriali.VisibleRowCount + 1;
                        object[] kodi = (object[])serializusi.DeserializeObject(hfKodi.Value);

                        for (int i = 0; i < rreshta - 1; i++)//krijohet kolectioni me trupat e fleteve kontabel e futura nga perdoruesi
                        {
                            tr = new DbCore.DbInventari.clsSerialeUnike();
                            if (kodi[i] != null && kodi[i].ToString() != "")
                            {
                                tr.Kodi = kodi[i].ToString();

                            }

                            trupi.Add(tr);

                        }
                        if (key != -1)
                            trupi.RemoveAt(key);
                        else
                        {
                            trupi.Add(new DbCore.DbInventari.clsSerialeUnike());
                        }
                        if (trupi.Count == 0)
                        {
                            trupi.Add(new DbCore.DbInventari.clsSerialeUnike());
                        }
                        this.gvSeriali.DataSource = trupi;
                        this.gvSeriali.DataBind();
                        percaktoTemplateTrupash();

                    }
                }
            }
        }

        /// <summary>
        /// cakton properti ne javascript
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvSeriali_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpNoRows"] = gvSeriali.VisibleRowCount;
        }

        /// <summary>
        /// perdoret per te shtuar kolonen e selektimit tek grida dhe per te vendosur disa karakteristika te grides
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void gvSeriali_DataBound(object sender, EventArgs e)
        {//shton butonin fshi
            if (this.gvSeriali.Columns["Fshi"] == null)
            {
                GridViewDataTextColumn fshi = new GridViewDataTextColumn();
                fshi.Caption = "Fshi";
                fshi.Width = 30;
                gvSeriali.Columns.Add(fshi);

                gvSeriali.KeyFieldName = "ID";
                gvSeriali.SettingsBehavior.AllowSelectByRowClick = false;
                gvSeriali.SettingsBehavior.AllowFocusedRow = true;
            }
        }

        /// <summary>
        /// kur krijohen rreshtat e grides
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvSeriali_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {//krijon rreshat sipas modelit
            if (e.RowType == DevExpress.Web.GridViewRowType.Data)
            {
                GridViewDataTextColumn col0 = ((ASPxGridView)sender).Columns["Fshi"] as GridViewDataTextColumn;
                GridViewDataTextColumn col1 = ((ASPxGridView)sender).Columns["Kodi"] as GridViewDataTextColumn;

                ASPxButton btn0 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col0, "btn") as ASPxButton;
                ASPxComboBox cmb1 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col1, "cmbBox") as ASPxComboBox;

                //vendosen client side eventet e kolonave
                if (btn0 != null)
                {
                    btn0.ClientInstanceName = String.Format("btnFshi{0}", e.VisibleIndex);
                    btn0.ClientSideEvents.Click = String.Format("function(s,e){{FshiClicked({0});}}", e.VisibleIndex);
                }
                if (cmb1 != null)
                {
                    // cmb1.DropDownButton.Visible = false;
                    //cmb1.Buttons.Add();
                    cmb1.DropDownStyle = DropDownStyle.DropDownList;
                    cmb1.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                    DbCore.DbInventari.colSerialeUnike col = new DbCore.DbInventari.colSerialeUnike((int)hfState["idNdermarrje"]);
                    cmb1.DataSource = col;
                    cmb1.TextField = "Kodi";
                    cmb1.ValueField = "ID";
                    cmb1.DataBind();

                    cmb1.ClientInstanceName = String.Format("Kodi{0}", e.VisibleIndex);
                    cmb1.ClientSideEvents.LostFocus = String.Format("function(s,e){{LostFocusSeriali(Kodi{0},{0});}}", e.VisibleIndex);

                }

            }


        }

        /// <summary>
        /// krijon koleksionin me burimet e ketij aktiviteti me te dhenat e grides
        /// </summary>
        /// <param name="idNdermarrje">id e ndermarjes</param>
        /// <returns>koleksionin me burimet</returns>
        private DbCore.DbInventari.colSerialeUnike ruajTrup(int idNdermarrje)
        {
            DbCore.DbInventari.colSerialeUnike trupi = new DbCore.DbInventari.colSerialeUnike();
            DbCore.DbInventari.clsSerialeUnike tr = new DbCore.DbInventari.clsSerialeUnike();
            JavaScriptSerializer serializusi = new JavaScriptSerializer();
            int rreshta = gvSeriali.VisibleRowCount + 1;
            object[] kodi = (object[])serializusi.DeserializeObject(hfKodi.Value);

            for (int i = 0; i < rreshta - 1; i++)//krijohet kolectioni me trupat e fleteve kontabel e futura nga perdoruesi
            {
                tr = new DbCore.DbInventari.clsSerialeUnike();
                if (kodi[i] != null && kodi[i].ToString() != "")
                {
                    tr.Kodi = kodi[i].ToString();
                    DbCore.DbInventari.clsSerialeUnike burim = new DbCore.DbInventari.clsSerialeUnike(kodi[i].ToString(), idNdermarrje);
                    trupi.Add(burim);

                }



            }
            return trupi;
        }

        #endregion

    }
}