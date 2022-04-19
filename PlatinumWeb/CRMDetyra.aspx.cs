using DbCore;
using DbCore.DbCRM;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Resources;
using System.Web.Script.Serialization;
using System.Web.UI;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Types;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.DbAdmin;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using System.Linq;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    public partial class CRMDetyra : MyPageBase
    {
        /*kjo faqe do te perdoret ne tre raste
         * rasti 1 perdoret ne shtimin/modifikim/fshirje detyrash
         * rasti 2 perdoret si lupe per te zgjedhur detyrat te kategorise agjent (tek skeduleri)
         * rasti 3 perdoret si lupe per te zgjedur anketat per tua lidhur klienteve
         *
         *
         *
         *
         *
         *
         *
         *
         */

        private const int idstatusdok = 1;
        private const string prefixMesazhNjejes = "Detyra me kod: ";
        private const string prefixMesazhShumes = "Detyrat me kode: ";
        private const string suffixMesazhNjejesGabimi = " eshte i lidhur dhe nuk mund te fshihet";
        private const string suffixMesazhShumesGabimi = " jane te lidhur dhe nuk mund te fshihen";
        private const string suffixMesazhNjejesSuksesi = " u fshi me sukses!";
        private const string suffixMesazhShumesSuksesi = " u fshine me sukses!";
        private const string lidhesMesazhi = ". Kurse ";
        private const string mesazhZgjidhniNje = "Zgjidhni te pakten nje detyre!";
        private const string komponente = "CRMDetyra.aspx";
        private const int idKomponente = 2011;
        private int idNdermarrje;
        private int idPerdoruesi;
        private int idGjuha;
        private int idViti;
        private bool eshteMeme;
        private CultureInfo ci;
        private ResourceManager rm;
        private bool eshteLupe;
        private bool meAnketa;
        private int kategoriDetyre = -1;

        /// <summary>
        /// fshin filtrin e zgjedhur dhe aplikuar mbi gride, fshirje nga DB ku ai eshte ruajtur, dhe
        /// jo pastrimi i grides nga aplikimi i filtrit per kete do sherbeje Clear Filter ne fund te grides
        /// </summary>
        /// <param name="sender"> derguesi </param>
        /// <param name="e">      argumentat </param>
        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            int idNdermarrje = (int)hfState["idNdermarrje"];
            int idGjuha = (int)hfState["idGjuha"];
            DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            //filtra.mbushFiltraGridaSipasFiltraEmri(cmbFiltra.Text, idNdermarrje);
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuha, "ASPxGridView_Detyrat", "CRMDetyra.aspx", idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                //percaktoTemplateMenu(idGjuha, idViti, idPerdoruesi, idNdermarrje, ASPxMenu1, eshteLupe);
                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                cmbFiltra.Text = "";
                hfStatusi.Value = "true";
            }
        }

        /// <summary>
        /// perdoret per t'i vendosur faqes temen e devexpresit 
        /// </summary>
        /// <param name="sender"> derguesi </param>
        /// <param name="e">      argumentat </param>

        /// <summary>
        /// thirret kur ngarkohet faqja. Behet kontrolli nese perdoruesi eshte i loguar ne sistem
        /// dhe nqs jo ridrejtohet tek forma e logimit thirret inicializimi i konfigurimeve
        /// fillestare te faqes
        /// </summary>
        /// <param name="sender"> derguesi </param>
        /// <param name="e">      argumenti </param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                DbCore.clsFunksione.logout(Session, true, "FaqePaautorizuar");
            }
            int idPerd = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);

            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerd);
            }

            rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (!Page.IsPostBack)
            {
                var qstrLupa = Request.QueryString["eshteLupe"];
                var qstrAnketa = Request.QueryString["meAnketa"];

                eshteLupe = !string.IsNullOrWhiteSpace(qstrLupa) ? Convert.ToBoolean(qstrLupa) : false;
                meAnketa = !string.IsNullOrWhiteSpace(qstrAnketa) ? Convert.ToBoolean(qstrAnketa) : false;

                if (eshteLupe)
                {
                    kategoriDetyre = (int)Convert.ToInt32(Request.QueryString["kategoria"]);
                }

                idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
                hfState.Set("colAutorizime", Newtonsoft.Json.JsonConvert.SerializeObject(new colAutorizimetKoka(IdPerdoruesi)));
                idGjuha = mySessionObjects.ktheGjuhe(Session);
                eshteMeme = mySessionObjects.merrEshteMemeSesioni(Session);
                ci = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo(idGjuha);

                hfState["idGjuha"] = idGjuha;
                hfState["idNdermarrje"] = idNdermarrje;
                hfState["idPerdoruesi"] = idPerdoruesi;
                hfState["idViti"] = idViti;

                hfState["eshteMeme"] = eshteMeme;

                hfState.Set("eshteLupe", eshteLupe);
                hfState.Set("kategoria", kategoriDetyre);
                hfState.Set("meAnketa", meAnketa);

                lblUserEmri.Text = DbCore.mySessionObjects.ktheEmerPerdorues(Session);

                hfState["msgZgjdhniNjeNgaElementetEListes"] = rm.GetString("msgZgjdhniNjeNgaElementetEListes", ci);
                hfState["msgZgjidhniAutorizimet"] = rm.GetString("msgZgjidhniAutorizimet", ci);

                konfiguroVleraFillestareDetyrat(idNdermarrje, rm, ci, idGjuha);

                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, komponente);
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);

                mbushGridDetyraNgaDB();
            }
            else
            {
                idGjuha = (int)hfState["idGjuha"];
                idNdermarrje = (int)hfState["idNdermarrje"];
                idPerdoruesi = (int)hfState["idPerdoruesi"];
                idViti = (int)hfState["idViti"];

                eshteMeme = (bool)hfState["eshteMeme"];

                eshteLupe = (bool)hfState.Get("eshteLupe");
                kategoriDetyre = Convert.ToInt32(hfState["kategoria"]);
                meAnketa = Convert.ToBoolean(hfState["meAnketa"]);
                ci = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo(idGjuha);
                mbushGridDetyraNgaSession();
            }

            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti(int.Parse(cmbKonfigurimi.Value.ToString()), idGjuha);
            //konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()), idgjuha);

            konfiguroGrideDetyrash(idNdermarrje, konf.KodKonfigAmbjente, idKomponente);

            if (eshteLupe & meAnketa)
            {
                //behet kjo sepse ne rastet qe bene grida callback

                ASPxPageControl1.TabPages[2].ClientVisible = true;//bene visible tabin e anketave

                mbushGridAnketash();
                konfiguroGrideAnketash(konf.KodKonfigAmbjente, idKomponente);

                if (!string.IsNullOrWhiteSpace(Request.QueryString["klienti"]))
                {
                    int klienti = Convert.ToInt32(Request.QueryString["klienti"]);
                    if (klienti > 0)
                    {
                        SelektoDetyratEKlientit(klienti);

                        SelektoAnketatEKlientit(klienti);
                    }
                }
            }

            percaktoTemplateMenu();
            if (!eshteLupe)
                ASPxGridView_Detyrat.PercaktoTitlePanel(this, MenuInfo, pnlMesazhi, hfState, idPerdoruesi, idNdermarrje, idViti, idGjuha, konf.IdKonfigAmbjente, komponente, idKomponente, "Kodi", rm, ci);
        }

        protected void RuajKolona_Click(object sender, EventArgs e)
        {
            clsMesazh mesazh = RuajKolona_Detyrat();

            if (mesazh.Status)
            {
                mesazh = RuajKolona_Anketat();

                if (mesazh.Status)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                return;
            }

            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
        }

        /// <summary>
        /// ndodh kur menuja ben bound 
        /// </summary>
        /// <param name="sender"> derguesi </param>
        /// <param name="e">      argumentat </param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            //percaktoTemplateMenu(idgjuha, idviti, idperdoruesi, idNdermarrje, ASPxMenu1, eshteLupe);
        }

        /// <summary>
        /// sherben per te ruajtur filtrin e zgjedhur 
        /// </summary>
        /// <param name="sender"> derguesi </param>
        /// <param name="e">      argumentat </param>
        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
            idNdermarrje = (int)hfState["idNdermarrje"];
            idGjuha = (int)hfState["idGjuha"];
            //kap item qe ka template ne menune e kesaj faqeje
            MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;

            DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida() { FiltraKodi = cmbFiltra.Text, FiltraShenime = cmbFiltra.Text, FiltraUniversal = false };

            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuha, "ASPxGridView_Detyrat", "CRMDetyra.aspx", idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = ASPxGridView_Detyrat.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("Kodi", ASPxGridView_Detyrat);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = ASPxGridView_Detyrat.GetSortedColumns();
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
            DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();
            //oPerdorues = (DbCore.DbAdmin.clsPerdorues)(CacheLayer.GlobalCacheManager.MySessionCache["oClsPerdoruesi"]);
            oPerdorues = DbCore.mySessionObjects.kthePerdorues(Session);
            filtri.IdPerdoruesi = oPerdorues.IdPerdorues;
            filtri.IdNdermarje = idNdermarrje;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            filtri.IdStatusDok = 1;

            mesazh = filtri.ruaj();

            if (mesazh.Status == true)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            hfStatusi.Value = "true";
            cmbFiltra.Text = "";
        }

        /// <summary>
        /// perdoret per te trajtuar ngjarjet e butonave te menuse. 
        /// </summary>
        /// <param name="source"> derguesi </param>
        /// <param name="e">      argumentat </param>
        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {//kryen veprimet e menuse
            if (e.Item.Name == "OK" && Convert.ToBoolean(hfState["meAnketa"]))
            {
                lidhKlientetMeAnketaDheDetyra();
            }
            else if (e.Item.Name == "Ruaj")
            {
                Page.Validate("entries");
                ruajDetyre();

            }
        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes 
        /// </summary>
        /// <param name="idNderVit">   </param>
        /// <param name="idPerdorues"> </param>
        /// <param name="idNdermarrje"></param>
        /// <param name="aSPxMenu1">    menuja ne te cilat do te shtohen kontrollet </param>
        private void percaktoTemplateMenu()
        {
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdoruesi, idNdermarrje, ASPxMenu1, komponente, this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, hfShtimModifikim.Value == "modifikim" ? false : true, false, false, eshteMeme, true);
            if (!eshteLupe)
            {
                ASPxMenu1.Items.FindByName("Anullo").Visible = false;
                ASPxMenu1.Items.FindByName("OK").Visible = false;
            }
            else
                ASPxMenu1.Items.FindByName("Anullo").Text = "Mbyll";
        }

        private void lidhKlientetMeAnketaDheDetyra()
        {
            List<string> klientetelidhur = new List<string>();
            List<string> klientetepalidhur = new List<string>();

            if (gvLupaAnketa.Selection.Count == 0 && ASPxGridView_Detyrat.Selection.Count == 0)
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Zgjidhni nje ankete ose detyre! ", pnlMesazhi);
            else if (gvLupaAnketa.Selection.Count > 1 || ASPxGridView_Detyrat.Selection.Count > 1)
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Nuk mund te zgjidhni me shume se nje ankete ose detyre!", pnlMesazhi);
            else
            {
                if (hfKontrollet.Value == "")
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Zgjidhni nje klient!", pnlMesazhi);
                    return;
                }
            }

            string[] ids = hfKontrollet.Value.Split(',');
            int[] klientIds = Array.ConvertAll(ids, int.Parse);

            if (!ruajLidhjetAnketeKlient(klientIds, ref klientetelidhur, ref klientetepalidhur))
                return;
            if (!ruajLidhjetDetyreKlient(klientIds, ref klientetelidhur, ref klientetepalidhur))
                return;

            klientetepalidhur.RemoveAll(x => klientetelidhur.Contains(x));//per momentin,me vone do rregullohet 
            if (klientetepalidhur.Count == 0)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "Lidhja u be me sukses per te gjithe klientet!", pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Lidhja nuk u krye per klientet:" + String.Join(";", klientetepalidhur) + (klientetelidhur.Count > 0 ? " dhe u krye per klientet:" + String.Join(";", klientetelidhur) : "") + "!", pnlMesazhi);
        }

        #region PER DETYRAT

        /// <summary>
        /// perdoret per te fshire rreshtat e zgjedhur te bankave nqs perdoruesi konfirmon fshirjen 
        /// </summary>
        /// <param name="sender"> derguesi </param>
        /// <param name="e">      argumentat </param>
        protected void ButtonOk_Click2(object sender, EventArgs e)
        {//fshin rreshtat e selektuar
            List<object> rreshtat;
            if (ASPxPageControl1.ActiveTabIndex == 0)//nqs kemi zgjedhur vetem nje kartele per te pare fshijme vetem ate
                rreshtat = ASPxGridView_Detyrat.GetSelectedFieldValues("IdDetyra");
            else
            {
                rreshtat = new List<object>();
                rreshtat.Add(hfId.Value);
            }
            // List<object> rreshtat = ASPxGridView_Automjete.GetSelectedFieldValues("IdAutomjeti"); 
            if (rreshtat.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhZgjidhniNje, pnlMesazhi);
                return;
            }
            List<string> detyraTeFshira = new List<string>(), detyraTePaFshira = new List<string>();
            int idNdermarrje = (int)hfState["idNdermarrje"];
            int idPerdorues = (int)hfState["idPerdoruesi"];
            DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegjistrim = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
            DbCore.DbCRM.clsDetyra detyra;
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            for (int i = 0; i < rreshtat.Count; i++)
            {
                if (Convert.ToInt32(rreshtat[i]) == 0)
                    continue;
                detyra = new DbCore.DbCRM.clsDetyra(Convert.ToInt32(rreshtat[i]));
                if (detyra.IdDetyre == 0)
                    continue;

                konf.mbushKonfigAmbjSipasKod(cmbKonfigurimi.Text, idNdermarrje);
                bool lidhur = dbRegjistrim.eshteDokumentiILidhurCelje(detyra.IdDetyre.ToString(), konf.IdNivel.ToString());
                if (lidhur)
                {
                    detyraTePaFshira.Add(detyra.Kodi);
                    continue;
                }
                DbCore.clsMesazh mesazhi = DbCore.DbCRM.clsDetyra.fshi(detyra.IdDetyre, idPerdoruesi);
                if (mesazhi.Status)
                {
                    #region Heq detyren nga grida

                    hiqDetyreNgaGrida(idNdermarrje, idPerdorues, detyra.IdDetyre);

                    #endregion Heq detyren nga grida

                    detyraTeFshira.Add(detyra.Kodi);

                    ASPxPageControl1.ActiveTabIndex = 0;
                    hfStatusi.Value = "true";
                }
            }
            dbRegjistrim.Dispose();
            string mesazhInfoGabim = "", mesazhInfoSukses = "";
            if (detyraTePaFshira.Count == 1)
                mesazhInfoGabim = String.Format("{0}{1}{2}", prefixMesazhNjejes, String.Join(";", detyraTePaFshira), suffixMesazhNjejesGabimi);
            else
                if (detyraTePaFshira.Count > 1)
                mesazhInfoGabim = String.Format("{0}{1}{2}", prefixMesazhShumes, String.Join(";", detyraTePaFshira), suffixMesazhShumesGabimi);
            if (detyraTeFshira.Count == 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", prefixMesazhNjejes, String.Join(";", detyraTeFshira), suffixMesazhNjejesSuksesi);
            else
                if (detyraTeFshira.Count > 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", prefixMesazhShumes, String.Join(";", detyraTeFshira), suffixMesazhShumesSuksesi);
            if (mesazhInfoGabim != "" && mesazhInfoSukses != "")
                mesazhInfoGabim += lidhesMesazhi + mesazhInfoSukses;
            if (mesazhInfoGabim != "")
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhInfoGabim, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazhInfoSukses, pnlMesazhi);
            pnlMesazhi.Update();
        }

        protected void filterDefault_Click(object sender, EventArgs e) =>
            GridUtil.AplikoFilterDefault(ASPxGridView_Detyrat, Convert.ToInt32(cmbKonfigurimi.Value));

        protected void btnXlsxExport_Click(object sender, EventArgs e)
        {
            try
            {
                System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                CultureInfo ci = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo((int)hfState["idGjuha"]);
                gridExport.WriteXlsxToResponse(rm.GetString("historikuTab", ci), true);
            }
            catch (Exception)
            {
            }
        }

        protected void btnPdfExport_Click(object sender, EventArgs e)
        {
            try
            {
                System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                CultureInfo ci = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo((int)hfState["idGjuha"]);
                gridExport.WritePdfToResponse(rm.GetString("msgFaturat", ci), true);
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// perdoret ne rastet kur grida ben callback per ta rimbushur griden 
        /// </summary>
        /// <param name="sender"> derguesi </param>
        /// <param name="e">      argumentat </param>
        protected void ASPxGridView_Detyrat_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            if (e.CallbackName == "COLUMNMOVE" && ASPxGridView_Detyrat.AllColumns[int.Parse(e.Args[0])].Width.Value == 0)
                ASPxGridView_Detyrat.AllColumns[int.Parse(e.Args[0])].Width = System.Web.UI.WebControls.Unit.Percentage(3);
            //if (e.CallbackName == "APPLYFILTER" && e.Args[0] == "")
            //{
            //    MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            //    ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            //    cmbFiltra.Text = "";
            //}
            CultureInfo cultinf = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo((int)hfState["idGjuha"]);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            //DbCore.clsFunksione.ToolTipButonaveMbiGride(ASPxGridView_Detyrat, cultinf, rm);
        }

        /// <summary>
        /// perdoret per te vendosur filtrat tek headeri i grides 
        /// </summary>
        /// <param name="sender"> derguesi </param>
        /// <param name="e">      argumentat </param>
        protected void ASPxGridView_Detyrat_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            if (e.Column.FieldName == "Pershkrimi" || e.Column.FieldName == "Kodi")
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
        /// cakton properti ne javascript 
        /// </summary>
        /// <param name="sender"> derguesi </param>
        /// <param name="e">      argumentat </param>
        protected void ASPxGridView_Detyrat_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = ASPxGridView_Detyrat.PageIndex;
            e.Properties["cpPageRow"] = ASPxGridView_Detyrat.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = ASPxGridView_Detyrat.VisibleRowCount;
        }

        /// <summary>
        /// kur grida ben callback 
        /// </summary>
        /// <param name="sender"> derguesi </param>
        /// <param name="e">      argumentat </param>
        protected void ASPxGridView_Detyrat_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string idkomponente = "";
            string kodkonfigurimi = "";

            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 2)
            {
                idPerdoruesi = (int)hfState["idPerdoruesi"];
                idNdermarrje = (int)hfState["idNdermarrje"];
                idViti = (int)hfState["idViti"];

                eshteLupe = (bool)hfState["eshteLupe"];
                mbushGridDetyraNgaSession();
            }
            if (arr.Length == 3)
            {
                kodkonfigurimi = arr[1];
                idkomponente = arr[0];
                if (arr[2] == "")
                    ASPxGridView_Detyrat.FilterExpression = "";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "ASPxGridView_Detyrat", "CRMDetyra.aspx", idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], idNdermarrje, koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        ASPxGridView_Detyrat.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, ASPxGridView_Detyrat);
                        hfStatusi.Value = "true";
                    }
                    else
                        hfStatusi.Value = "false";
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

            ASPxGridView_Detyrat.Selection.UnselectAll();
        }

        /// <summary>
        /// perdoret per te shfaqur po jo tek komboja e aktivit 
        /// </summary>
        /// <param name="sender"> derguesi </param>
        /// <param name="e">      argumentat </param>
        protected void ASPxGridView_Detyrat_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
        }

        /// <summary>
        /// per filtrimin me elementin bosh 
        /// </summary>
        /// <param name="sender"> derguesi </param>
        /// <param name="e">      argumentat </param>
        protected void ASPxGridView_Detyrat_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if (e.Column.FieldName == "Kategoria" || e.Column.FieldName == "Rendesia")
            {
                if (Converter.ConvertToInt(e.Value) == 0)
                {
                    e.Criteria = null;
                }
            }
        }

        /// <summary>
        /// perdoret per te shtuar kolonen e selektimit tek grida dhe per te vendosur disa
        /// karakteristika te grides
        /// </summary>
        /// <param name="sender"> derguesi </param>
        /// <param name="e">      argumentat </param>
        protected void ASPxGridView_Detyrat_DataBound(object sender, EventArgs e)
        {// shton colonen # per selektim dhe disa karakteristika te grides
            if (ASPxGridView_Detyrat.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per
                //perzgjidh
                GridViewCommandColumn check = new GridViewCommandColumn("#") { ShowSelectCheckbox = true, Width = System.Web.UI.WebControls.Unit.Percentage(2) };
                //behet per te afishuar rreshtin qe do sherbej per filtrim
                ASPxGridView_Detyrat.Settings.ShowFilterRow = true;
                ASPxGridView_Detyrat.Settings.ShowFilterRowMenu = true;
                ASPxGridView_Detyrat.Columns.Add(check);
                ASPxGridView_Detyrat.KeyFieldName = "IdDetyra";
                ASPxGridView_Detyrat.SettingsBehavior.AllowSelectByRowClick = true;
                ASPxGridView_Detyrat.SettingsBehavior.AllowFocusedRow = true;
                check.VisibleIndex = 0;
            }
        }
        

        private bool ruajLidhjetDetyreKlient(int[] klientIds, ref List<string> klientetelidhur, ref List<string> klientetepalidhur)
        {
            idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            Dictionary<string, object>[] detyraMeData;

            detyraMeData = serializer.Deserialize<Dictionary<string, object>[]>(hfDataVlefshmerie.Value);

            if (detyraMeData.Length > 0)
                for (int i = 0; i < klientIds.Length; i++)
                {
                    DbCore.DbKontabiliteti.clsKlientFurnitor kf = new DbCore.DbKontabiliteti.clsKlientFurnitor(klientIds[i]);
                    clsMesazh mesazh = new clsMesazh(false);
                    for (int r = 0; r < detyraMeData.Length; r++)
                    {
                        int idDetyre = Convert.ToInt32(detyraMeData[r]["DetyraID"]);
                        DateTime dtFillimi = Convert.ToDateTime(detyraMeData[r]["DtFillimi"]);
                        DateTime dtMbarimi = Convert.ToDateTime(detyraMeData[r]["DtMbarimi"]);

                        if (dtMbarimi < dtFillimi)
                        {
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Data e mbarimit nuk mund te jete me e vogel se data e fillimit!", pnlMesazhi);
                            return false;
                        }
                        clsDetyreKlient detyreKlient = new clsDetyreKlient(0, klientIds[i], idDetyre, 1, DateTime.Now, DateTime.Now, idPerdoruesi, idPerdoruesi, idNdermarrje, dtFillimi, dtMbarimi);

                        if (DbCore.DbCRM.clsDetyreKlient.EshtePeriudhaReMeEVogelSeEvjetra(idNdermarrje, klientIds[i], dtFillimi, dtMbarimi, idDetyre))
                        {
                            mesazh = detyreKlient.modifiko();
                            continue;
                        }
                        if (DbCore.DbCRM.clsDetyreKlient.kaPrerjeDetyrash(idNdermarrje, klientIds[i], dtFillimi, dtMbarimi, idDetyre))
                        {
                            klientetepalidhur.AddIfNotExists(kf.KodKlientFurnitor);
                            continue;
                        }

                        mesazh = detyreKlient.ruaj();

                        if (!mesazh.Status)
                        {
                            klientetepalidhur.AddIfNotExists(kf.KodKlientFurnitor);
                            break;
                        }
                    }
                    if (mesazh.Status)
                        klientetelidhur.AddIfNotExists(kf.KodKlientFurnitor);

                }
            return true;
        }

        /// <summary>
        /// perdoret per te konfiguruar griden. Per te dhenat mbi kolonat qe do te shfaqen nga
        /// databaza si dhe percakton disa karakteristika te grides
        /// </summary>
        /// : 
        /// <see cref="GridUtil.percaktoVisibleColumns" />
        /// : 
        /// <see cref="DbCore.clsFunksione.konfiguroGrideListeMadhe(grida,celesigrides)" />
        /// <param name="idNdermarrje">   id e ndermarjes </param>
        /// <param name="idKomponente">   id e komponentes </param>
        /// <param name="kodKonfigurimi"> kodi i konfigurimit </param>
        private void konfiguroGrideDetyrash(int idNdermarrje, string kodKonfigurimi, int idKomponente)
        {
            shtoKategoria();
            shtoRendesia();

            GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "ASPxGridView_Detyrat", ASPxGridView_Detyrat, kodKonfigurimi, idKomponente.ToString(), DbCore.mySessionObjects.ktheGjuhe(Session));
            KonfiguroDataVlefshmerie();

            if (eshteLupe && kategoriDetyre == 1)
                ASPxGridView_Detyrat.Columns["#"].VisibleIndex = -1;
            else
                ASPxGridView_Detyrat.Columns["#"].VisibleIndex = 0;
        }

        private clsMesazh RuajKolona_Detyrat()
        {
            int idfiltri = 0;
            int idkonf = DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(cmbKonfigurimi.Text.Split(';')[0], idNdermarrje);
            DbCore.clsMesazh mesazh = GridUtil.ruajFiltra(idNdermarrje, idPerdoruesi, idGjuha, "ASPxGridView_Detyrat ", "CRMDetyra.aspx", "FilterDefault", ASPxGridView_Detyrat.FilterExpression, ASPxGridView_Detyrat, "Kodi", idkonf, out idfiltri);//ruan filtrin e zgjedhur tek filtrat
            if (!mesazh.Status)
            {
                return mesazh;
            }

            mesazh = GridUtil.ruajkonfigurimgride(ASPxGridView_Detyrat, cmbKonfigurimi.Text, idNdermarrje, idPerdoruesi, 2011, idfiltri, idViti, DbCore.mySessionObjects.ktheCultureInfo(Session), DbCore.mySessionObjects.ktheGjuhe(Session));/// ruan konfigurimin e grides dhe filtrin
            clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, "ASPxGridView_Detyrat ", int.Parse(cmbKonfigurimi.Value.ToString()), "CRMDetyra.aspx");
            //percaktoTemplateMenu(idGjuha, idViti, idPerdoruesi, idNdermarrje, ASPxMenu1, eshteLupe);
            return mesazh;
        }

        private void shtoKategoria()
        {//shton comboboxin e llojit
            var colKategoria = ASPxGridView_Detyrat.Columns["Kategoria"];

            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            colnew.PropertiesComboBox.Items.Add("Agjent", 1);
            colnew.PropertiesComboBox.Items.Add("Klient", 2);
            colnew.FieldName = "Kategoria";
            colnew.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            colnew.VisibleIndex = colKategoria.VisibleIndex;
            ASPxGridView_Detyrat.Columns.Remove(colKategoria);
            ASPxGridView_Detyrat.Columns.Add(colnew);
        }

        private void shtoRendesia()
        {//shton comboboxin e llojit
            var colRendesia = ASPxGridView_Detyrat.Columns["Rendesia"];

            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            colnew.PropertiesComboBox.Items.Add("Niveli 1", 1);
            colnew.PropertiesComboBox.Items.Add("Niveli 2", 2);
            colnew.PropertiesComboBox.Items.Add("Niveli 3", 3);
            colnew.FieldName = "Rendesia";
            colnew.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            colnew.VisibleIndex = colRendesia.VisibleIndex;
            ASPxGridView_Detyrat.Columns.Remove(colRendesia);
            ASPxGridView_Detyrat.Columns.Add(colnew);
        }

        ///shto datat per vlefshemerine e detyres
        private void KonfiguroDataVlefshmerie()
        {
            if (meAnketa)
            {
                GridViewDataDateColumn dtFillimi = ASPxGridView_Detyrat.Columns["DtFillimi"] as GridViewDataDateColumn;
                GridViewDataDateColumn dtMbarimi = ASPxGridView_Detyrat.Columns["DtMbarimi"] as GridViewDataDateColumn;

                dtFillimi.DataItemTemplate = new Templates.MyDateTemplate();
                dtFillimi.VisibleIndex = 25;

                dtMbarimi.DataItemTemplate = new Templates.MyDateTemplate();
                dtMbarimi.VisibleIndex = 26;
            }
        }

        //sherben per te ruajtur nje detyre
        private void ruajDetyre()
        {
            if (Page.IsValid == false)
                return;
            idPerdoruesi = (int)hfState["idPerdoruesi"];
            idGjuha = (int)hfState["idGjuha"];
            idNdermarrje = (int)hfState["idNdermarrje"];
            idViti = (int)hfState["idViti"];
            eshteLupe = (bool)hfState["eshteLupe"];
            DbCore.DbCRM.clsDetyra detyra;
            try
            {
                if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
                    detyra = krijoDetyra(idNdermarrje, idPerdoruesi, true);
                else detyra = krijoDetyra(idNdermarrje, idPerdoruesi, false);
            }
            catch (Exception e)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, e.Message, pnlMesazhi);
                hfStatusi.Value = "false";
                return;
            }
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, "CRMDetyra.aspx");
            bool eshteShtim;
            if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
            {
                if (!tedrejtaInfo.DShtim)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                    hfStatusi.Value = "false";
                    return;
                }
                mesazh = detyra.ruaj(hfNrAutoDet);
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
                detyra.IdDetyre = Convert.ToInt32(hfId.Value);
                mesazh = detyra.modifiko(idPerdoruesi);
                eshteShtim = false;
            }
            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                hfStatusi.Value = "false";
            }
            else
            {
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                if (eshteShtim)
                    shtoDetyreNeGrid(idNdermarrje, idPerdoruesi, detyra.IdDetyre);
                else //modifikim
                    modifikoDetyreNeGrid(idNdermarrje, idPerdoruesi, detyra.IdDetyre);
                hfStatusi.Value = "true";
                // konfiguroGrideDetyrash(idNdermarrje, cmbKonfigurimi.Text.Split(';')[0], 2011); 
                ASPxPageControl1.ActiveTabIndex = 0;

                ScriptManager1.RegisterDataItem(hfId, detyra.IdDetyre.ToString());
                ScriptManager1.RegisterDataItem(hfKodi, detyra.Kodi);
                ScriptManager1.RegisterDataItem(hfEmertimi, detyra.Pershkrim);
                //per momentin po e le keshtu
                if (Request.QueryString["vjenNgaRoute"] == "true")
                    CacheLayer.GlobalCacheManager.MySessionCache["AppFormDetyra"] = null;

            }
        }

        private DbCore.DbCRM.clsDetyra krijoDetyra(int idNdermarrje, int idPerdorues, bool shtim)
        {
            var controls = this.GetAsPxTextEditIdValue();
            controls.AddRange(ASPxPageControl1.GetAsPxTextEditIdValue());

            hfNrAuto = (ASPxHiddenField)NrAuto.VendosVleratNrAuto(hfNrAuto, controls);
            //DbCore.DbAdmin.NrAuto.vendosVleratNrAuto(hfNrAuto, this, this.ASPxPageControl1, null, null);

            hfNrAutoDet = (ASPxHiddenField)NrAuto.ShtoNeRegjistrime(hfNrAutoDet, hfNrAuto, "txtKodi", "KodDetyra");
            //DbCore.DbAdmin.NrAuto.shtoNeHfRegjistrime(hfNrAutoDet, hfNrAuto, "txtKodi", "KodDetyra");

            if (txtKodi.Text == "")
                throw new DbCore.MyException("Plotesoni kodin e detyres!");
            if (txtPershkrimi.Text == "")
                throw new DbCore.MyException("Plotesoni pershkrimin e detyres!");
            int idRendesia = 0;
            try
            {
                idRendesia = int.Parse(cmbRendesia.Value.ToString());
            }
            catch (Exception)
            {
                throw new DbCore.MyException("Zgjidhni rendesine e detyres!");
            }

            int idKategoria = 0;
            try
            {
                idKategoria = int.Parse(cmbKategoria.Value.ToString());
            }
            catch (Exception)
            {
                throw new DbCore.MyException("Zgjidhni kategorine e detyres!");
            }

            DbCore.DbCRM.clsDetyra detyra;
            if (shtim)
                detyra = new DbCore.DbCRM.clsDetyra(txtKodi.Text, cmbAutorizimiHf.Value, int.Parse(cmbRendesia.Value.ToString()), int.Parse(cmbKategoria.Value.ToString()), txtPershkrimi.Text, idPerdorues, DateTime.Today, 0, new DateTime(), 1, idNdermarrje, shtim);
            else
                detyra = new DbCore.DbCRM.clsDetyra(txtKodi.Text, cmbAutorizimiHf.Value, int.Parse(cmbRendesia.Value.ToString()), int.Parse(cmbKategoria.Value.ToString()), txtPershkrimi.Text, idPerdorues, DateTime.Today, idPerdorues, new DateTime(), 1, idNdermarrje, shtim);
            return detyra;
        }

        private void shtoDetyreNeGrid(int idNdermarrje, int idPerdorues, int idDetyre)
        {
            if (ASPxGridView_Detyrat.DataSource != null)
            {
                DataTable dt = (DataTable)ASPxGridView_Detyrat.DataSource;
                DataRow[] drs = dt.Select("IdDetyra = " + idDetyre);
                if (drs.Length > 0)
                    throw new Exception("GABIM: Detyra ekziston ne gride!");
                DataRow newDr = DbCore.DbCRM.clsDetyra.ktheDetyre(idDetyre);
                dt.ImportRow(newDr);
                ASPxGridView_Detyrat.DataBind();
            }
            else
                mbushGridDetyraNgaDB();
        }

        private void modifikoDetyreNeGrid(int idNdermarrje, int idPerdorues, int idDetyre)
        {
            if (ASPxGridView_Detyrat.DataSource != null)
            {
                DataTable dt = (DataTable)ASPxGridView_Detyrat.DataSource;
                DataRow[] drs = dt.Select("IdDetyra = " + idDetyre);
                if (drs.Length > 1)
                    throw new Exception("GABIM: Ndodhen 2 detyra me te njejten id ne gride!");
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                DataRow newDr = DbCore.DbCRM.clsDetyra.ktheDetyre(idDetyre);
                object[] arr = newDr.ItemArray;
                dr.ItemArray = arr;
                ASPxGridView_Detyrat.DataBind();
            }
            else
                mbushGridDetyraNgaDB();
        }

        private void hiqDetyreNgaGrida(int idNdermarrje, int idPerdoruesi, int idDetyre)
        {
            if (ASPxGridView_Detyrat.DataSource != null)
            {
                DataTable dt = (DataTable)ASPxGridView_Detyrat.DataSource;
                DataRow[] drs = dt.Select("IdDetyra = " + idDetyre);
                if (drs.Length > 1)
                    throw new Exception("GABIM: Ndodhen 2 detyra me te njejten id ne gride");
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                dt.Rows.Remove(dr);
                ASPxGridView_Detyrat.DataBind();
            }
            else
                mbushGridDetyraNgaDB();
        }

        /// <summary>
        /// konfiguron vlerat fillestare 
        /// </summary>
        /// <param name="idNdermarrje"> id e ndermarjes </param>
        private void konfiguroVleraFillestareDetyrat(int idNdermarrje, System.Resources.ResourceManager rm, System.Globalization.CultureInfo cultinf, int idGjuha)
        {
            //mbush komboboxet dhe gridat e faqes
            ASPxPageControl1.ActiveTabIndex = 0;
            ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 110, rm, cultinf, idGjuha);
            cmbKonfigurimi.SelectedIndex = 0;
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()));
            hfKonffillestar.Value = String.Format("{0};{1}", konf.KodKonfigAmbjente, konf.PershkrimKonfigAmbjente);
            ConfigureAspxComboBox.mbushComboKategoriDetyrash(cmbKategoria);
            ConfigureAspxComboBox.mbushComboRendesiDetyrash(cmbRendesia);
        }

        /// <summary>
        /// mbush griden nga sesioni 
        /// </summary>
        /// <param name="idNdermarrje"> id e ndermarjes </param>
        private void mbushGridDetyraNgaSession()
        {
            DataTable tmpObject;
            bool sukses = DbCore.mySessionObjects.merrGrideNgaSessioni(komponente, Session, out tmpObject);
            if (!sukses)
                mbushGridDetyraNgaDB();
            else
            {
                ASPxGridView_Detyrat.DataSource = tmpObject;
                ASPxGridView_Detyrat.DataBind();
                tmpObject.Dispose();
            }
        }

        /// <summary>
        /// mbush griden nga databaza 
        /// </summary>
        /// <param name="idNdermarrje"> id e ndermarjes </param>
        private void mbushGridDetyraNgaDB()
        {
            //mbush griden e popupit me te dhena

            kategoriDetyre = Convert.ToInt32(hfState.Get("kategoria"));
            meAnketa = Convert.ToBoolean(hfState.Get("meAnketa"));

            int klienti = Convert.ToInt32(Request.QueryString["klienti"]);
            DataTable dt = null;

            if (meAnketa)//DETYRAT PER KLIENTET
            {
                //merr detyrat,dergohet -1 qe te merren te gjitha detyrat ose klienti nese do merren vetem detyrat per nje klient specifik
                dt = DbCore.DbCRM.colDetyreKlient.merrDetyratPerKlientMeDataVlefshmerie(idNdermarrje, idPerdoruesi, klienti > 0 ? klienti : -1);
            }
            else
                dt = DbCore.DbCRM.colDetyra.merrDetyratPerNdermarrjeKategoriAutorizim(idNdermarrje, idPerdoruesi, eshteLupe ? kategoriDetyre : -1);

            DbCore.mySessionObjects.ruajGrideNeSession(komponente, Session, dt);

            ASPxGridView_Detyrat.DataSource = dt;
            ASPxGridView_Detyrat.DataBind();
            dt.Dispose();
        }

        private void SelektoDetyratEKlientit(int idKlienti)
        {
            DbCore.DbCRM.colDetyreKlient colDetyra = new DbCore.DbCRM.colDetyreKlient(idKlienti);
            for (int i = 0; i < colDetyra.Count; i++)
                ASPxGridView_Detyrat.Selection.SelectRowByKey(colDetyra[i].IdDetyre);
        }

        #endregion PER DETYRAT

        #region PER ANKETAT

        protected void gvLupaAnketa_DataBound(object sender, EventArgs e)
        {
            if (this.gvLupaAnketa.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per
                //perzgjidh
                GridViewCommandColumn check = new GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true; check.Width = System.Web.UI.WebControls.Unit.Percentage(4);

                //behet per te afishuar rreshtin qe do sherbej per filtrim
                gvLupaAnketa.Settings.ShowFilterRow = true;
                gvLupaAnketa.Columns.Add(check);
                check.VisibleIndex = 0;
                gvLupaAnketa.KeyFieldName = "IdKokaAnketa";
                gvLupaAnketa.SettingsBehavior.AllowSelectByRowClick = true;
            }
        }

        protected void gvLupaAnketa_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
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
                    gvLupaAnketa.FilterExpression = "";
                else
                {
                    GridUtil.AplikoFilter(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, gvLupaAnketa, arr[2], "gvLupaAnketa", "CRMDetyra.aspx", 1);
                    //DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    //DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhePerdoruesi(Session), "gvLupaAnketa", "CRMDetyra.aspx", idNdermarrje);
                    //filtra.mbushFilterPerGrideSipasKodit(arr[2], idNdermarrje, koka.IdGridaKoka);
                    //if (filtra.FiltraKodi != null)
                    //{
                    //    gvLupaAnketa.FilterExpression = filtra.FiltraVlera;
                    //    if (filtra.DrejtimRenditje == true)
                    //        gvLupaAnketa.SortBy(gvLupaAnketa.Columns[filtra.KoloneRenditje], DevExpress.Data.ColumnSortOrder.Ascending);
                    //    else
                    //        gvLupaAnketa.SortBy(gvLupaAnketa.Columns[filtra.KoloneRenditje], DevExpress.Data.ColumnSortOrder.Descending);
                    //}
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
            gvLupaAnketa.Selection.UnselectAll();
        }

        private void SelektoAnketatEKlientit(int idKlienti)
        {
            DbCore.DbCRM.colKlientAnketa col = new DbCore.DbCRM.colKlientAnketa(idKlienti);
            for (int i = 0; i < col.Count; i++)
                gvLupaAnketa.Selection.SelectRowByKey(col[i].IdKokaAnketa);
            gvLupaAnketa.DataBind();
        }

        private bool ruajLidhjetAnketeKlient(int[] klientIds, ref List<string> klientetelidhur, ref List<string> klientetepalidhur)
        {
            object[] rreshtat = gvLupaAnketa.GetSelectedFieldValues("IdKokaAnketa").ToArray();
            int[] anketaIds = Array.ConvertAll(rreshtat, Convert.ToInt32);

            if (anketaIds.Length > 0)
                for (int i = 0; i < klientIds.Length; i++)
                {
                    DbCore.DbKontabiliteti.clsKlientFurnitor kf = new DbCore.DbKontabiliteti.clsKlientFurnitor(klientIds[i]);
                    clsMesazh mesazh = new clsMesazh(false);

                    for (int a = 0; a < anketaIds.Length; a++)
                    {
                        if (DbCore.DbCRM.clsKlientAnketa.ekzistonKlientAnketa(klientIds[i], anketaIds[a]))
                            continue;

                        DbCore.DbCRM.clsKokaAnketa kok = new DbCore.DbCRM.clsKokaAnketa(anketaIds[a]);
                        if (DbCore.DbCRM.clsKokaAnketa.kaPrerjeAnketashKlienti(idNdermarrje, klientIds[i], kok.DtFillimi, kok.DtMbarimi))
                        {
                            klientetepalidhur.AddIfNotExists(kf.KodKlientFurnitor);
                            continue;
                        }

                        DbCore.DbCRM.clsKlientAnketa klient = new DbCore.DbCRM.clsKlientAnketa(0, klientIds[i], anketaIds[a], 1, DateTime.Now, DateTime.Now, DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                        mesazh = klient.ruaj();

                        if (!mesazh.Status)
                            klientetepalidhur.AddIfNotExists(kf.KodKlientFurnitor);
                        break;
                    }

                    if (mesazh.Status)
                        klientetelidhur.AddIfNotExists(kf.KodKlientFurnitor);
                }
            return true;
        }

        private void mbushGridAnketash()
        {//mbush griden e popupit me te dhena
            DbCore.DbCRM.colKokaAnketa colAnketa = new DbCore.DbCRM.colKokaAnketa();
            colAnketa.merrKokaAnketeSipasNdermarrjesAktive(idNdermarrje);

            gvLupaAnketa.DataSource = colAnketa;
            gvLupaAnketa.DataBind();
        }

        private void konfiguroGrideAnketash(string kodKonfigurimi, int idKomponente)
        {
            //funk.konfiguroGrideListeMadhe(gvLupaAnketa, "IdAutorizimKoka");

            GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "gvLupaAnketa", gvLupaAnketa, kodKonfigurimi, idKomponente.ToString(), idGjuha);

            GridUtil.KonfiguroGrideListeMadhePopupiPaTheme(gvLupaAnketa, "IdKokaAnketa", true, false);

            gvLupaAnketa.Columns["#"].VisibleIndex = 0;
        }

        private clsMesazh RuajKolona_Anketat()
        {
            int idfiltri = 0;
            int idkonf = DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(cmbKonfigurimi.Text.Split(';')[0], idNdermarrje);
            DbCore.clsMesazh mesazh = GridUtil.ruajFiltra(idNdermarrje, idPerdoruesi, idGjuha, "gvLupaAnketa ", "CRMDetyra.aspx", "FilterDefault", ASPxGridView_Detyrat.FilterExpression, ASPxGridView_Detyrat, "Kodi", idkonf, out idfiltri);//ruan filtrin e zgjedhur tek filtrat
            if (!mesazh.Status)
            {
                return mesazh;
            }

            mesazh = GridUtil.ruajkonfigurimgride(ASPxGridView_Detyrat, cmbKonfigurimi.Text, idNdermarrje, idPerdoruesi, 2011, idfiltri, idViti, DbCore.mySessionObjects.ktheCultureInfo(Session), DbCore.mySessionObjects.ktheGjuhe(Session));/// ruan konfigurimin e grides dhe filtrin
            clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, "gvLupaAnketa ", int.Parse(cmbKonfigurimi.Value.ToString()), "CRMLupaAnketa.aspx");
            return mesazh;
        }

        #endregion PER ANKETAT
    }
}