using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.Web;
using DevExpress.Web.ASPxTreeList;
using DbCore.DbShare;
using DbCore.DbAdmin;
using System.Web.Script.Serialization;
using DbCore.IMBUtils.Cache;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using PlatinumWeb.ApplicationUtils.Pages;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;

namespace PlatinumWeb
{
    public partial class StrukturaAdministrative : MyPageBase
    {
        private int idperdoruesi, idnderviti;
        protected void Page_Load(object sender, EventArgs e)
        {

            //Response.Cache.SetCacheability(HttpCacheability.NoCache);

            //if (CacheLayer.GlobalCacheManager.MySessionCache["LoggedIn"].Equals("No"))
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                Response.Redirect(DbCore.IMBUtils.Paths.defaultLoginPath);
            }
            idperdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);

            //if (CacheLayer.GlobalCacheManager.MySessionCache["KodiNdermarrjes"] == null)
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idperdoruesi);
            }
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);

            idnderviti = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);

            //mbushComboBoxFiltra(idNdermarrje);
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "trlStruktura", 1, "StrukturaAdministrative.aspx");
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (!IsPostBack)
            {
                CacheDataProvider.ClearSessionCache("StrukturaAdministrative", "colStrukturatAdministrative");
                //Session.Add("mesazh", ":Green");
                mbushHiddenFieldMePerkthime(ci, rm);
                EmratELabelave(rm, ci);
                AspxWebControlUtils.perkthePopUp(popFshi, rm.GetString("labelKujdes", ci), lblMsgbox, rm.GetString("msgStrukturaAdministrativePopFshiText", ci), ButtonCancel, rm.GetString("labelAnullo", ci));
                ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idperdoruesi, idNdermarrje, cmbKonfigurimi, 105, rm, ci, DbCore.mySessionObjects.ktheGjuhe(Session));
                hfLloji.Value = "1";
                hfState.Set("idPerdoruesi", idperdoruesi);
                hfState.Set("idGjuha", DbCore.mySessionObjects.ktheGjuhe(Session));
                hfState.Set("idNdermarrje", idNdermarrje);
                cmbKonfigurimi.SelectedIndex = 0;
                DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()), DbCore.mySessionObjects.ktheGjuhe(Session));
                hfKonffillestar.Value = String.Format("{0};{1}", konf.KodKonfigAmbjente, konf.PershkrimKonfigAmbjente);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, ":");
                konfiguroVleraFillestare(idNdermarrje);
                konfiguroGride(idNdermarrje, rm, ci);
                TreeListUtil.percaktoVisibleColumnsKonf(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, trlStruktura, cmbKonfigurimi.Text.Split(';')[0], "701");
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idperdoruesi, idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "StrukturaAdministrative.aspx");
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);

            }

            DbCore.mySessionObjects.ruajMesazhNeSesion(Session, ":");
            konfiguroVleraFillestare(idNdermarrje);
            konfiguroGride(idNdermarrje, rm, ci);
            if (trlStruktura.IsNewNodeEditing)
                trlStruktura.SettingsSelection.Enabled = false;
            else trlStruktura.SettingsSelection.Enabled = true;
            percaktoTemplateArtPerb();
        }

        private void EmratELabelave(System.Resources.ResourceManager rm, System.Globalization.CultureInfo ci)
        {
        }

        private void mbushHiddenFieldMePerkthime(System.Globalization.CultureInfo ci, System.Resources.ResourceManager rm)
        {
            hfState.Set("msgNukKeniDrejtaPerVeprim", rm.GetString("msgNukKeniDrejtaPerVeprim", ci));
            hfState.Set("cmbVleraQendraKosto", rm.GetString("cmbVleraQendraKosto", ci));
            hfState.Set("headerTextPopUpStrukturaAdministrative", rm.GetString("headerTextPopUpStrukturaAdministrative", ci));
            hfState.Set("headerTextPopSkemaQKStrukturaAdministrative", rm.GetString("headerTextPopSkemaQKStrukturaAdministrative", ci));
            hfState.Set("regjisDokZgjidhDokPerTeBashkengjitur", rm.GetString("regjisDokZgjidhDokPerTeBashkengjitur", ci));
            hfState.Set("msgStrukturaAdministrativeZgjidhniNje", rm.GetString("msgStrukturaAdministrativeZgjidhniNje", ci));
            konfigurimi_Label.Text = rm.GetString("lblModeli", ci);
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
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, "StrukturaAdministrative.aspx", this, MenuInfo, hfRuaj.Value == "Ruaj" ? true : false, true, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
        }

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));

        }

        /// <summary>
        /// mbush tree listen me te dhena
        /// </summary>
        /// <param name="idNdermarrje"></param>
        private void konfiguroVleraFillestare(int idNdermarrje)
        {
            trlStruktura.DataSource = DbCore.DbListPagesat.colStrukturatAdministrative.merrStruktureAdmNdermarjeDT(idNdermarrje);
            trlStruktura.DataBind();

        }
        /// <summary>
        /// konfiguron listen
        /// </summary>e
        /// <param name="idNdermarrje"></param>
        private void konfiguroGride(int idNdermarrje, System.Resources.ResourceManager rm, System.Globalization.CultureInfo ci)
        {
            // KonfigurimComboGride.shtoLlojSipasQendresKosto(trlStruktura, rm, ci);  // TODO GETSON
            shtoQender();
            shtoPerson();
            shtoLloj(rm, ci);
            ShtoAktiv();




            if (!IsPostBack)
            {
                TreeListUtil.konfiguroTreeListeEvogelPaTheme(trlStruktura, "IdStrukturaAdm", "IdPrindi", true);
                trlStruktura.SettingsSelection.Recursive = false;
                trlStruktura.SettingsBehavior.AutoExpandAllNodes = false;
                trlStruktura.ExpandToLevel(1);
            }

            TreeListUtil.percaktoVisibleColumnsKonf(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, trlStruktura, cmbKonfigurimi.Text.Split(';')[0], "701");
        }
        private void ShtoAktiv()
        {
            trlStruktura.Columns.Remove(trlStruktura.Columns["Aktiv"]);
            TreeListCheckColumn colnew = new TreeListCheckColumn();
            colnew.FieldName = "Aktiv";
            trlStruktura.Columns.Add(colnew);
            //Font - Bold = "False"
        }
        private void shtoQender()
        {
            trlStruktura.Columns.Remove(trlStruktura.Columns["Qendra"]);
            TreeListComboBoxColumn colnew = new TreeListComboBoxColumn();
            colnew.FieldName = "Qendra";
            colnew.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            trlStruktura.Columns.Add(colnew);
        }
        private void shtoPerson()
        {
            trlStruktura.Columns.Remove(trlStruktura.Columns["Personi"]);
            TreeListComboBoxColumn colnew = new TreeListComboBoxColumn();
            colnew.FieldName = "Personi";
            colnew.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            trlStruktura.Columns.Add(colnew);
        }
        /// <summary>
        /// fshin rreshtat e selektuar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            List<TreeListNode> rreshtat = trlStruktura.GetSelectedNodes();
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (rreshtat.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgStrukturaAdministrativeZgjidhniNje", ci), pnlMesazhi);
                return;
            }
            List<string> TeFshire = new List<string>(), TePaFshire = new List<string>();
            foreach (TreeListNode id in rreshtat)
            {
                DbCore.DbListPagesat.clsStrukturaAdministrative strukt = new DbCore.DbListPagesat.clsStrukturaAdministrative(int.Parse(id.Key)) { IdPerdoruesi = idperdoruesi };
                if (DbCore.DbListPagesat.clsStrukturaAdministrative.eshteILidhur(strukt.IdStrukturaAdm))
                {
                    TePaFshire.Add(strukt.Kodi);
                    continue;
                }
                DbCore.clsMesazh mesazh = strukt.Fshi();
                if (mesazh.Status)
                    TeFshire.Add(strukt.Kodi);
                else TePaFshire.Add(strukt.Kodi);
            }
            string mesazhInfoGabim = "", mesazhInfoSukses = "";
            if (TePaFshire.Count == 1)
                mesazhInfoGabim = String.Format("{0}{1}{2}", rm.GetString("msgStrukturaAdministrativePrefixNjejes", ci), String.Join(";", TePaFshire), rm.GetString("msgStrukturaAdministrativeSuffixNjejesGabimi", ci));
            else
                if (TePaFshire.Count > 1)
                mesazhInfoGabim = String.Format("{0}{1}{2}", rm.GetString("msgStrukturaAdministrativePrefixShumes", ci), String.Join(";", TePaFshire), rm.GetString("msgStrukturaAdministrativeSuffixShumesGabimi", ci));
            if (TeFshire.Count == 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("msgStrukturaAdministrativePrefixNjejes", ci), String.Join(";", TeFshire), rm.GetString("msgStrukturaAdministrativeSuffixNjejesSuksesi", ci));
            else
                if (TeFshire.Count > 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("msgStrukturaAdministrativePrefixShumes", ci), String.Join(";", TeFshire), rm.GetString("msgStrukturaAdministrativeSuffixShumesSuksesi", ci));
            if (mesazhInfoGabim != "" && mesazhInfoSukses != "")
                mesazhInfoGabim += rm.GetString("msgLidhesMesazhi", ci) + mesazhInfoSukses;
            if (mesazhInfoGabim != "")
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhInfoGabim, pnlMesazhi);
            else
            {
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazhInfoSukses, pnlMesazhi);
                try
                {
                    ASPxMenu1.Items.FindByText("Ruaj").ClientVisible = false;
                }
                catch (Exception ex)
                {
                    NLog.LogManager.GetCurrentClassLogger().Error(ex.Message);
                    ASPxMenu1.Items.FindByText("Save").ClientVisible = false;
                }
            }

            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            konfiguroVleraFillestare(idNdermarrje);
            konfiguroGride(idNdermarrje, rm, ci);

        }
        /// <summary>
        /// veprimet e menuse nga server side
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {

        }
        /// <summary>
        /// kur shtojme rreshta te rinj
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void trlStruktura_NodeInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            //merr te dhenat e rreshtit te ri te grides
            string emri;
            string personi;
            string nrtel;
            string shenime;
            const int idstatusdok = 1;
            int idprindi, qendra, skema, lloji;
            bool aktive;
            aktive = Convert.ToBoolean(hfAktiv.Value);
            if (e.NewValues["Emri"] != null)
                emri = e.NewValues["Emri"].ToString();
            else
                emri = "";
            //if (hfPersoni.Value != "")
            //{
            //    DbCore.DbListPagesat.clsPunonjes pun = new DbCore.DbListPagesat.clsPunonjes(hfPersoni.Value, idNdermarrje);
            //    personi = pun.IdPunonjes;
            //}
            ////if (e.NewValues["Personi"] != null)
            ////    personi = e.NewValues["Personi"].ToString();
            //else
            personi = hfPersoni.Value;
            if (e.NewValues["NrTel"] != null)
                nrtel = e.NewValues["NrTel"].ToString();
            else
                nrtel = "";
            if (e.NewValues["Shenime"] != null)
                shenime = e.NewValues["Shenime"].ToString();
            else
                shenime = "";
            if (e.NewValues["IdPrindi"] != null)
                idprindi = int.Parse(e.NewValues["IdPrindi"].ToString());
            else
                idprindi = 0;
        
             if (hfLloji.Value != "")
                if (hfLloji.Value == rm.GetString("cmbVleraQendraKosto", ci))
                    lloji = 1;
                else if (hfLloji.Value == rm.GetString("cmbVleraSkemaQendraKosto", ci))
                    lloji = 2;
                else lloji = int.Parse(hfLloji.Value);
            else
                lloji = 0;
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            if (hfQendra.Value != "")
                if (lloji == 1)
                {
                    DbCore.DbQendraKosto.clsQendraKosto qen = new DbCore.DbQendraKosto.clsQendraKosto(hfQendra.Value, idNdermarrje);
                    qendra = qen.Id;
                    skema = 0;
                }
                else
                {
                    DbCore.DbQendraKosto.clsKokaSkemaQK skem = new DbCore.DbQendraKosto.clsKokaSkemaQK(hfQendra.Value, idNdermarrje);
                    skema = skem.IdKoka;
                    qendra = 0;
                }
            else
            {
                qendra = 0;
                skema = 0;
            }

            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "StrukturaAdministrative.aspx");
            if (!tedrejtaInfo.DShtim)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukKeniTeDrejta", ci), pnlMesazhi);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, rm.GetString("msgNukKeniTeDrejtaRed", ci));
                return;
            }

            if (trlStruktura.Columns["Kodi"].Visible == false)
            {
                JavaScriptSerializer serializusi = new JavaScriptSerializer();
                serializusi.MaxJsonLength = 50000000;
                List<NrAuto> list = new List<NrAuto>();
                int idnrautonrdok = new clsKusht(int.Parse(cmbKonfigurimi.Value.ToString()), "NrAutoKodi").Vlera;
                string nrdokshitje = DbCore.DbAdmin.clsNrAutom.merrVlerenNrAutomatik(idnrautonrdok, DateTime.Today);
                if (!String.IsNullOrEmpty(nrdokshitje))//nqs ka nr automatik
                {
                    DbCore.DbAdmin.NrAuto nrdokshi = new NrAuto();
                    nrdokshi.kodKontrolli = "Kodi";
                    nrdokshi.idNrAuto = idnrautonrdok;
                    nrdokshi.vlereNrAuto = nrdokshitje;

                    list.Add(nrdokshi);
                    nrdokshitje = nrdokshi.vlereNrAuto;
                    hfNrAuto.Add("Kodi", serializusi.Serialize(nrdokshi));
                    e.NewValues["Kodi"] = nrdokshitje;
                }
            }

            hfNrAuto = (ASPxHiddenField)NrAuto.VendosVleratNrAutoPerKod(hfNrAuto, e.NewValues["Kodi"].ToString());
            //DbCore.DbAdmin.NrAuto.vendosVleratNrAutoPerKodi(hfNrAuto, e.NewValues["Kodi"].ToString());

            DbCore.DbListPagesat.clsStrukturaAdministrative strukt = new DbCore.DbListPagesat.clsStrukturaAdministrative(0, e.NewValues["Kodi"]?.ToString(), emri, personi, nrtel, shenime, idprindi, idNdermarrje, idperdoruesi, idstatusdok, qendra, skema, lloji,aktive);

            hfNrAutoKF = (ASPxHiddenField)NrAuto.ShtoNeRegjistrime(hfNrAutoKF, hfNrAuto, "Kodi", "Kodi");
            //NrAuto.shtoNeHfRegjistrime(hfNrAutoKF, hfNrAuto, "Kodi", "Kodi");

            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            e.Cancel = true;

            hfArkiva.Set("kopjoArkiven", true);
            mesazh = strukt.ruajStruktureAdm(hfNrAutoKF, hfArkiva);
            if (!mesazh.Status == true)
            {
                //CacheLayer.GlobalCacheManager.MySessionCache["mesazh"] = mesazh.PershkrimMesazhi + ":Red";
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, mesazh.PershkrimMesazhi + ":Red");
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            }
            else
            {
                trlStruktura.CancelEdit();
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, rm.GetString("msgRuajtjeMeSuksesGreen", ci));
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("msgRuajtjeMeSukses", ci), pnlMesazhi);

            }
            konfiguroVleraFillestare(idNdermarrje);
            konfiguroGride(idNdermarrje, rm, ci);
        }
        /// <summary>
        /// kur modifikojme reshtat eksitues
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void trlStruktura_NodeUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "StrukturaAdministrative.aspx");
            if (!tedrejtaInfo.DMod)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukKeniTeDrejta", ci), pnlMesazhi);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, rm.GetString("msgNukKeniTeDrejtaRed", ci));
                return;
            }
            int id = int.Parse(e.Keys["IdStrukturaAdm"].ToString());
            var struktold = new DbCore.DbListPagesat.clsStrukturaAdministrative(id);
            string emri;
            string personi;
            string nrtel;
            string shenime;
            string kodi = "";
            const int idstatusdok = 1;
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idprindi = 0, qendra, skema, lloji;
            bool aktive;

            aktive = Convert.ToBoolean(hfAktiv.Value);
            if (e.NewValues["Emri"] != null)
                emri = e.NewValues["Emri"].ToString();
            else
                emri = "";

           
            personi = hfPersoni.Value;


            if (e.NewValues["NrTel"] != null)
                nrtel = e.NewValues["NrTel"].ToString();
            else
                nrtel = "";
            if (e.NewValues["Kodi"] != null)
            {
                kodi = e.NewValues["Kodi"].ToString();
            }
            else
            {
                kodi = struktold.Kodi;
            }
            if (e.NewValues["Shenime"] != null)
                shenime = e.NewValues["Shenime"].ToString();
            else
                shenime = "";
            idprindi = struktold.IdPrindi;
            if (hfLloji.Value != "")
                if (hfLloji.Value == rm.GetString("cmbVleraQendraKosto", ci))
                    lloji = 1;
                else if (hfLloji.Value == rm.GetString("cmbVleraSkemaQendraKosto", ci))
                    lloji = 2;
                else lloji = int.Parse(hfLloji.Value);
            else
                lloji = 0;

            if (hfQendra.Value != "")
                if (lloji == 1)
                {
                    DbCore.DbQendraKosto.clsQendraKosto qen = new DbCore.DbQendraKosto.clsQendraKosto(hfQendra.Value, idNdermarrje);
                    qendra = qen.Id;
                    skema = 0;
                }
                else
                {
                    DbCore.DbQendraKosto.clsKokaSkemaQK skem = new DbCore.DbQendraKosto.clsKokaSkemaQK(hfQendra.Value, idNdermarrje);
                    skema = skem.IdKoka;
                    qendra = 0;
                }
            else
            {
                qendra = 0;
                skema = 0;
            }

            DbCore.DbListPagesat.clsStrukturaAdministrative strukt = new DbCore.DbListPagesat.clsStrukturaAdministrative(id, kodi, emri, personi, nrtel, shenime, idprindi, idNdermarrje, idperdoruesi, idstatusdok, qendra, skema, lloji, aktive);
            e.Cancel = true;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();

               mesazh = strukt.Modifiko();
            if (!mesazh.Status == true)
            {
                //CacheLayer.GlobalCacheManager.MySessionCache["mesazh"] = mesazh.PershkrimMesazhi + ":Red";
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, mesazh.PershkrimMesazhi + ":Red");
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            }
            else
            {
                trlStruktura.CancelEdit();
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, rm.GetString("msgModifikimiMeSuksesGreen", ci));
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("msgModifikimiMeSukses", ci), pnlMesazhi);

            }
            konfiguroVleraFillestare(idNdermarrje);
            konfiguroGride(idNdermarrje, rm, ci);

        }
        /// <summary>
        /// validimi i te dhenave
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void trlStruktura_NodeValidating(object sender, TreeListNodeValidationEventArgs e)
        {
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            foreach (TreeListColumn column in trlStruktura.Columns)
            {
                if (column.Visible == true)
                {
                    TreeListDataColumn dataColumn = column as TreeListDataColumn;
                    if (dataColumn == null) continue;

                    if (e.NewValues[dataColumn.FieldName] == null && dataColumn.FieldName != "Shenime" && dataColumn.FieldName != "LlojQendre" && dataColumn.FieldName != "Qendra" && dataColumn.FieldName != "NrTel" && dataColumn.FieldName != "Personi" && dataColumn.FieldName != "Aktiv")//validimi per kolonat e detyrueshme
                    {

                        e.Errors[dataColumn.FieldName] = rm.GetString("msgStrukturaAdministrativeVlereJoNull", ci);
                    }
                }
            }
            
            if (hfLloji.Value == "" || hfLloji.Value == "0")
                e.Errors["LlojQendre"] = rm.GetString("msgStrukturaAdministrativeVlereJoNull", ci);
            if (e.NewValues["Kodi"] != null)
            {
                string kodi = e.NewValues["Kodi"].ToString();
                kodi = kodi.Trim();
                if (kodi.Length > 10)
                {
                    e.Errors["Kodi"] = rm.GetString("msgStrukturaAdministrativeKodiGjatesiaMax", ci);
                    e.NodeError = rm.GetString("msgStrukturaAdministrativeKodiGjatesiaMax", ci);
                    return;
                }

                //int kod = 0;

                //int.TryParse(kodi, out kod);
                //if (kod == 0 && kodi != "0")
                //{
                //    e.Errors["Kodi"] = rm.GetString("msgStrukturaAdministrativeKodiDuhetNumer", ci);
                //    e.NodeError = rm.GetString("msgStrukturaAdministrativeKodiDuhetNumer", ci);
                //    return;
                //}
                int idNdermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                if (hfQendra.Value != "")
                {
                    if (hfLloji.Value == "1" || hfLloji.Value == rm.GetString("cmbVleraQendraKosto", ci))
                    {
                        if (!DbCore.DbQendraKosto.clsQendraKosto.ekzistonQK(hfQendra.Value, idNdermarje))
                        {
                            e.NodeError = rm.GetString("msgStrukturaAdministrativeNukEkzistonQendra", ci);

                        }
                        else
                        {
                            DbCore.DbQendraKosto.clsQendraKosto obj = new DbCore.DbQendraKosto.clsQendraKosto(hfQendra.Value, idNdermarje);
                            if (!obj.Aktiv)
                            {
                                e.NodeError = rm.GetString("msgStrukturaAdministrativeQendraJoAktive", ci);

                            }
                            else
                            {
                                DbCore.DbQendraKosto.colQendraKosto col = new DbCore.DbQendraKosto.colQendraKosto();
                                col.mbushQendraSipasPrindit(obj.Id);
                                if (col.Count > 0)
                                {
                                    e.NodeError = rm.GetString("msgStrukturaAdministrativeQendraPrindNukZgjidhet", ci);

                                }
                            }
                        }
                    }
                    else
                    {
                        if (!DbCore.DbQendraKosto.clsKokaSkemaQK.ekzistonSkeme(hfQendra.Value, idNdermarje))
                        {
                            e.NodeError = rm.GetString("msgStrukturaAdministrativeNukEkzistonSkemaQK", ci);

                        }
                    }                    
                }
            }
            if (e.Errors.Count > 0)
            {
                e.NodeError = rm.GetString("msgStrukturaAdministrativePlotesoFushat", ci);

            }

            if (string.IsNullOrEmpty(e.NodeError) && e.Errors.Count > 0)
            {
                e.NodeError = rm.GetString("msgStrukturaAdministrativeKorrigjoGabimet", ci);

            }
        }
        /// <summary>
        /// levizja e nyjeve nga nje prind tek nje tjeter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void trlStruktura_ProcessDragNode(object sender, TreeListNodeDragEventArgs e)
        {
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "StrukturaAdministrative.aspx");
            if (!tedrejtaInfo.DMod)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukKeniTeDrejta", ci), pnlMesazhi);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, rm.GetString("msgNukKeniTeDrejtaRed", ci));
                return;
            }
            int id = int.Parse(e.Node.Key);
            DbCore.DbListPagesat.clsStrukturaAdministrative strukt = new DbCore.DbListPagesat.clsStrukturaAdministrative(id);
            
            int idprind = 0;
            int.TryParse(e.NewParentNode.Key, out idprind);
            DbCore.clsMesazh mesazh = strukt.modifikoStrukturaAdm(idprind, idperdoruesi);
            if (!mesazh)
            {
                e.Cancel = true;
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, mesazh.PershkrimMesazhi + ":Red");
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            }
            else
            {
                e.Handled = true;
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, rm.GetString("msgModifikimiMeSuksesGreen", ci));
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("msgModifikimiMeSukses", ci), pnlMesazhi);
            }
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            konfiguroVleraFillestare(idNdermarrje);
            konfiguroGride(idNdermarrje, rm, ci);
        }


        void SetNodeSelectionSettings()
        {
            TreeListNodeIterator iterator = trlStruktura.CreateNodeIterator();
            TreeListNode node;
            while (true)
            {
                node = iterator.GetNext();
                if (node == null) break;
                node.AllowSelect = !node.HasChildren;

            }
        }
        protected void trlStruktura_StartNodeEditing(object sender, TreeListNodeEditingEventArgs e)
        {
          
        }
        private void percaktoTemplateArtPerb()
        {
            TreeListComboBoxColumn col1 = trlStruktura.Columns["LlojQendre"] as TreeListComboBoxColumn;
            col1.EditCellTemplate = new Templates.MyComboTreeList();// col1.VisibleIndex = 0;
            TreeListComboBoxColumn col2 = trlStruktura.Columns["Qendra"] as TreeListComboBoxColumn;
            col2.EditCellTemplate = new Templates.MyComboTreeList();// col1.VisibleIndex = 0;
            TreeListComboBoxColumn col3 = trlStruktura.Columns["Personi"] as TreeListComboBoxColumn;
            col3.EditCellTemplate = new Templates.MyComboTreeList();// col1.VisibleIndex = 0;
            TreeListCheckColumn checkColumn = trlStruktura.Columns["Aktiv"] as  TreeListCheckColumn;
            checkColumn.EditCellTemplate = new Templates.MyCheckTemplateTreeList();

        }
        private void shtoLloj(System.Resources.ResourceManager rm, System.Globalization.CultureInfo ci)
        {
            trlStruktura.Columns.Remove(trlStruktura.Columns["LlojQendre"]);
            TreeListComboBoxColumn colnew = new TreeListComboBoxColumn();
            colnew.PropertiesComboBox.Items.Add(rm.GetString("cmbVleraQendraKosto", ci), 1);
            colnew.PropertiesComboBox.Items.Add(rm.GetString("cmbVleraSkemaQendraKosto", ci), 2);
            colnew.FieldName = "LlojQendre";
            colnew.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            trlStruktura.Columns.Add(colnew);
        }
        protected void btnXlsxExport_Click(object sender, EventArgs e)
        {
            try
            {
                ASPxTreeListExporter1.WriteXlsxToResponse("Departamentet", true);
            }
            catch (Exception err)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
            }
        }
        protected void btnPdfExport_Click(object sender, EventArgs e)
        {
            try
            {
                ASPxTreeListExporter1.WritePdfToResponse("Departamentet", true);
            }
            catch (Exception err)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
            }
        }

        protected void trlStruktura_HtmlRowPrepared(object sender, TreeListHtmlRowEventArgs e)
        {
            if (e.RowKind == TreeListRowKind.EditForm)
            {
                ASPxTreeList tr = (ASPxTreeList)sender;
                bool shto = tr.IsNewNodeEditing;

                TreeListComboBoxColumn col = tr.Columns["LlojQendre"] as TreeListComboBoxColumn;
                ASPxComboBox cmb = (ASPxComboBox)tr.FindEditCellTemplateControl(col, "cmbBox");
                TreeListComboBoxColumn col1 = tr.Columns["Qendra"] as TreeListComboBoxColumn;
                ASPxComboBox cmb1 = (ASPxComboBox)tr.FindEditCellTemplateControl(col1, "cmbBox");
                TreeListComboBoxColumn col2 = tr.Columns["Personi"] as TreeListComboBoxColumn;
                ASPxComboBox cmb2 = (ASPxComboBox)tr.FindEditCellTemplateControl(col2, "cmbBox");
                TreeListCheckColumn col3 = tr.Columns["Aktiv"] as TreeListCheckColumn;
                ASPxCheckBox ck = (ASPxCheckBox)tr.FindEditCellTemplateControl(col3, "cb");
                System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
                System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

                if (cmb != null)
                {
                    cmb.Items.Add(rm.GetString("cmbVleraQendraKosto", ci), 1);
                    cmb.Items.Add(rm.GetString("cmbVleraSkemaQendraKosto", ci), 2);
                    cmb.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                    cmb.ClientInstanceName = "LlojQendre";
                    cmb.ClientSideEvents.Init = "function(s, e) {$('#hfLloji').val(LlojQendre.GetValue());  }";
                    cmb.ClientSideEvents.SelectedIndexChanged = "SelectedIndexChangedHtmlRowPrepared";
                    if (shto)
                    {
                        cmb.SelectedIndex = 0;
                    }
                }
                if (cmb1 != null)
                {
                    ConfigureAspxComboBox.percaktoTemplateComboMeEnableCallback(cmb1);
                    cmb1.ClientInstanceName = "Qendra";
                    cmb1.AutoPostBack = false;
                    cmb1.EnableCallbackMode = false;
                    cmb1.ClientSideEvents.Init = "function(s, e) {$('#hfQendra').val(Qendra.GetText());  }";
                    cmb1.ClientSideEvents.ButtonClick = "function(s, e) {Qendra_Click();}";
                    cmb1.ClientSideEvents.TextChanged = "function(s, e) { Qendra_Changed(); }";
                    cmb1.ClientSideEvents.SelectedIndexChanged = "function(s, e) { Qendra_Changed(); }";
                    cmb1.ClientSideEvents.ValueChanged = "function(s, e) { Qendra_Changed(); }";
                    if (cmb != null && (cmb.Value.ToString() == "1" || cmb.Value.ToString() == rm.GetString("cmbVleraQendraKosto", ci)))
                        ConfigureAspxComboBox.mbushComboQendraKostoBij(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), cmb1);
                    else ConfigureAspxComboBox.mbushComboSkemaQendraKosto(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), cmb1);
                }
                if (cmb2 != null)
                {
                    ConfigureAspxComboBox.percaktoTemplateComboMeEnableCallback(cmb2);
                    cmb2.ClientInstanceName = "Personi";
                    cmb2.AutoPostBack = false;
                    cmb2.EnableCallbackMode = false;
                    cmb2.ClientSideEvents.Init = "function(s, e) { Personi_Changed(); }";
                    cmb2.ClientSideEvents.ButtonClick = "function(s, e) {Personi_Click();}";
                    cmb2.ClientSideEvents.TextChanged = "function(s, e) { Personi_Changed(); }";
                    //cmb2.ClientSideEvents.LostFocus = "function(s, e) { Personi_Changed(); }";
                    cmb2.ClientSideEvents.SelectedIndexChanged = "function(s, e) { Personi_Changed(); }";
                    //cmb2.ClientSideEvents.ValueChanged = "function(s, e) { Personi_Changed(); }";
                    ConfigureAspxComboBox.mbushComboPunonjes(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), cmb2);

                }
                if (ck != null)
                {


                    ck.ClientInstanceName = "Aktiv";
                    if (shto)
                    {
                        ck.Checked = true;

                    }

                    ck.ClientSideEvents.CheckedChanged = "function(s, e) { Aktiv_Changed(s, e);  }";
                }
            }
        }
    }
}