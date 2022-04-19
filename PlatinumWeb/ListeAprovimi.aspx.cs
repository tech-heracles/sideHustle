using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Web;
using System.Data;
using DbCore.DbRegjistrim;
using PlatinumWeb.Templates;
using System.ComponentModel;
using System.Globalization;
using System.Resources;
using DbCore;
using DbCore.DbArkaBanka;
using System.Reflection;
using DbCore.DbShare;
using DbCore.IMBUtils.Extensions;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using System.Drawing;
using AlphaWebCommon.WebUtils.ASPxControlExtensions;
using NLog;

namespace PlatinumWeb
{
    public partial class ListeAprovimi : MyPageBase
    {
        private int idndermarje, idperdoruesi, idnderviti, idviti, idgjuha, shifraPasPresjesSasia;
        string veprimi;
        ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
        
        /// <summary>
        /// kur lodohet faqja  mbushet me te dhena dhe kontrollohen ne eshte i autorizuar perdoruesi
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                DbCore.clsFunksione.logout(Session, true, "FaqePaautorizuar");
            }
            idperdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idperdoruesi);
            }
            idndermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            idnderviti = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
            idviti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            idgjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            CultureInfo cultinf = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo(idgjuha);
            if (Request.QueryString["status"] == "aprovim")
                veprimi = "aprovim";
            else
                veprimi = "kerkese";
            percaktoTemplateMenu(idgjuha, ASPxMenu1, idviti, idperdoruesi, idndermarje);
            if (!IsPostBack)
            {
                mbushHiddenFieldMePerkthime(cultinf, rm);
                hfState.Set("idGjuha", idgjuha);
                if (Request.QueryString["status"] == "aprovim")
                {
                    veprimi = "aprovim";
                    ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idperdoruesi, idndermarje, cmbKonfigurimi, 69, "APR", rm, cultinf, idgjuha);
                }
                else
                {
                    ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idperdoruesi, idndermarje, cmbKonfigurimi, 70, "KAPR", rm, cultinf, idgjuha);
                    veprimi = "kerkese";
                }
                cmbKonfigurimi.SelectedIndex = 0;
                DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                konf.mbushKonfigAmbjSipasId(Convert.ToInt32(cmbKonfigurimi.SelectedItem.Value), idgjuha);
                hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
                hfState.Set("kushtHapRapPerAprovim", DbCore.DbShare.clsAlternativaKushti.getAlternativa(Convert.ToInt32(cmbKonfigurimi.SelectedItem.Value), "RAPPERAPR"));
                hfState.Set("RAPPERAN", DbCore.DbShare.clsAlternativaKushti.getAlternativa(Convert.ToInt32(cmbKonfigurimi.SelectedItem.Value), "RAPPERAN"));
                mbushGridNgaDB();
                var formatMonedhe = clsFormatKonfigTrup.MerrFormatMonedheSipasNdermarrjesDheKonfig(idndermarje, konf.IdKonfigAmbjente, 0);
                shifraPasPresjesSasia = formatMonedhe.ShifraPasPresjesSasia;
                konfiguroGride(rm, cultinf, shifraPasPresjesSasia);
                hfState.Set("shifraPasPresjesSasia", shifraPasPresjesSasia);
                if (veprimi == "aprovim")
                    GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idndermarje, "gvAprovimet", gvAprovimet, cmbKonfigurimi.Text.Split(';')[0], "535", (int)hfState["idGjuha"]);
                else
                {
                    GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idndermarje, "gvAprovimet", gvAprovimet, cmbKonfigurimi.Text.Split(';')[0], "536", (int)hfState["idGjuha"]);
                }
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idperdoruesi, idndermarje, idviti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
            }
            else
                mbushGridNgaDB();
            shifraPasPresjesSasia = hfState.Get<int>("shifraPasPresjesSasia");
            konfiguroGride(rm, cultinf, shifraPasPresjesSasia);
            GridUtil.konfigGrideListeEMadhePaTheme(gvAprovimet, "IdEtapa");
            hfVeprimi.Value = veprimi;
            clsToolbarConfig.mbushComboBoxFiltra(idgjuha, idndermarje, "gvAprovimet", int.Parse(cmbKonfigurimi.Value.ToString()), "ListeAprovimi.aspx");
            if (Request.QueryString["indexrow"] != null)
                gvAprovimet.FocusedRowIndex = int.Parse(Request.QueryString["indexrow"]);
            Container.Attributes["src"] = "";
            shtoKomente();
            gvAprovimet.PercaktoTitlePanel(this, MenuInfo, pnlMesazhi, hfState, idperdoruesi, idndermarje, idviti, idgjuha, int.Parse(cmbKonfigurimi.Value.ToString()), "ListeAprovimi.aspx", rm, cultinf);
        }

        /// <summary>
        /// Metode qe sherben per te kaluar perkthime te js
        /// </summary>
        /// <param name="cultinf">Merr culture info</param>
        /// <param name="rm">Merr Resource Manager</param>
        private void mbushHiddenFieldMePerkthime(CultureInfo cultinf, ResourceManager rm)
        {
            hfState.Set("msgDuhetTeZgjidhniNjeEtape", rm.GetString("msgDuhetTeZgjidhniNjeEtape", cultinf));
            hfState.Set("LupaKomentet", rm.GetString("LupaKomentet", cultinf));
            konfigurimi_Label.Text = rm.GetString("lblModeli", cultinf);
        }


        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        /// <param name="idViti"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNermarrje"></param>
        private void percaktoTemplateMenu(int idGjuha, ASPxMenu aSPxMenu1, int idViti, int idPerdorues, int idNdermarrje)
        {
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, DbCore.clsFunksione.GetKomponente(Page.Request), this, MenuInfo, true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));

        }
    
        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(idgjuha, ASPxMenu1, idviti, idperdoruesi, idndermarje);
        }


        /// <summary>
        /// mbush griden me te dhena nga db
        /// </summary>
        private void mbushGridNgaDB()
        {//mbush griden e popupit me te dhena            
            DataTable dt = new DataTable();
            if (veprimi == "aprovim")
                dt = DbCore.DbRegjistrim.colEtapeAprovimi.merrEtapaSipasPerdoruesitDheStatusit(IdPerdoruesi, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), Convert.ToInt16(DbCore.DbRegjistrim.StatusAprovimi.Per_Aprovim), DbCore.mySessionObjects.ktheGjuhe(Session));
            else dt = DbCore.DbRegjistrim.colEtapeAprovimi.merrEtapaSipasPerdoruesit(IdPerdoruesi, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheGjuhe(Session));
            if (dt == null) return;
            DbCore.mySessionObjects.ruajGrideNeSession(Session, dt);
            gvAprovimet.DataSource = dt;
            gvAprovimet.DataBind();
            dt.Dispose();
        }

        /// <summary>
        /// konfiguron griden
        /// </summary>
        private void konfiguroGride(ResourceManager rm, CultureInfo ci, int shifraPasPresjes)
        {

            shtoKomente();
            //shtoProgres();
            KonfigurimComboGride.ShtoMuaj(gvAprovimet, rm, ci);
            KonfigurimComboGride.ShtoStatusAprovimi(gvAprovimet, rm, ci, "Statusi");
            GridViewDataTextColumn col3 = gvAprovimet.Columns["Vlefta"] as GridViewDataTextColumn;
            GridUtil.VendosFormatNumriPerFushatNumerike(gvAprovimet, shifraPasPresjes, "Vlefta");
            GridViewDataDateColumn col5 = gvAprovimet.Columns["DtModifikimi"] as GridViewDataDateColumn;
            GridViewDataDateColumn col6 = gvAprovimet.Columns["DtKujtese"] as GridViewDataDateColumn;
            GridViewDataDateColumn col7 = gvAprovimet.Columns["DtDergimi"] as GridViewDataDateColumn;
            AspxWebControlUtils.vendosDateEditMask(col7.PropertiesDateEdit, col5.PropertiesDateEdit, col6.PropertiesDateEdit);
            col5.PropertiesEdit.DisplayFormatString = "dd/MM/yyyy hh:mm:ss";
            col6.PropertiesEdit.DisplayFormatString = "dd/MM/yyyy hh:mm:ss";
            col7.PropertiesEdit.DisplayFormatString = "dd/MM/yyyy hh:mm:ss";
            GridViewDataTextColumn col4 = gvAprovimet.Columns["VleftaMonBaze"] as GridViewDataTextColumn;
            col4.PropertiesEdit.DisplayFormatString = "0.00";

        }

        private void shtoKomente()
        {
            GridViewDataColumn g = gvAprovimet.Columns["Komente"] as GridViewDataColumn;

            g.DataItemTemplate = new MyLinkKomenteTemplate();
        }
        private void shtoProgres()
        {
            GridViewDataColumn g = gvAprovimet.Columns["Progresi"] as GridViewDataColumn;
            gvAprovimet.Columns.Remove(g);
            GridViewDataProgressBarColumn col9 = new GridViewDataProgressBarColumn();
            col9.PropertiesProgressBar.Minimum = 0;
            col9.PropertiesProgressBar.Maximum = 100;
            col9.PropertiesProgressBar.IndicatorStyle.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);

            col9.FieldName = "Progresi";
            col9.Caption = IdGjuha == 0 ? "Progresi" : "Progress";
            gvAprovimet.Columns.Add(col9);
        }

        /// <summary>
        /// kur grida ben databound per ti shtuar kolonen e selektimit
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvAprovimet_DataBound(object sender, EventArgs e)
        {
            //if (gvAprovimet.Columns["#"] == null)
            //{
            //    GridViewCommandColumn check = new GridViewCommandColumn("#") { ShowSelectCheckbox = true, Width = System.Web.UI.WebControls.Unit.Percentage(2) };
            gvAprovimet.Settings.ShowFilterRow = true;
            gvAprovimet.Settings.ShowHeaderFilterButton = true;
            gvAprovimet.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
            gvAprovimet.Settings.ShowFilterRowMenu = true;
            // gvAprovimet.Columns.Add(check);
            gvAprovimet.Settings.ShowGroupPanel = true;
            gvAprovimet.KeyFieldName = "IdEtapa";
            gvAprovimet.SettingsBehavior.AllowSelectByRowClick = true;
            gvAprovimet.SettingsBehavior.AllowFocusedRow = true;
            // }
        }

        /// <summary>
        /// eventet e menuse
        /// </summary>
        /// <param name="source">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {

            int idPerdorues = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            if (gvAprovimet.GetRowValues(gvAprovimet.FocusedRowIndex, "IdEtapa") == null && gvAprovimet.GetRowValues(gvAprovimet.FocusedRowIndex, "IDKATDOK") == null)
            { 
            LogManager.GetCurrentClassLogger().Info("Etapa dhe kategoria nuk kane vlera!");
            return;
          
       
            }
            var idetapa = gvAprovimet.GetRowValues(gvAprovimet.FocusedRowIndex, "IdEtapa").ToString();
            int kategoria = Convert.ToInt32(gvAprovimet.GetRowValues(gvAprovimet.FocusedRowIndex, "IDKATDOK").ToString());

            switch (e.Item.Name)
            {
                case "PrintPreview":
                    if (gvAprovimet.FocusedRowIndex == -1)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("regjMagMesazhZgjidhniFaturePerPrintim", ci), pnlMesazhi);
                        Container.Attributes["src"] = "";
                    }
                    else
                    {
                        object data = gvAprovimet.GetRowValues(gvAprovimet.FocusedRowIndex, "IdKokaShitje", "IDKATDOK");
                        string id = ((object[])data)[0].ToString();
                        string idKatDok = ((object[])data)[1].ToString();
                        int idRaporti = 0, idRaportDesign = 0, statusAprovimi = 0;
                        switch (idKatDok)
                        {
                            case "1":
                            case "2":
                                DbCore.DbRegjistrim.clsKokaShitje clsKoka = new DbCore.DbRegjistrim.clsKokaShitje();
                                clsKoka.mbushKokaShitjeSipasIDPaTrup(int.Parse(id));
                                idRaporti = DbCore.DbShare.clsRaporti.KtheIdRaporti(DbCore.IMBUtils.Messages.MessagesResource.Messages.IdGjuha, clsKoka.IdRaportDesing);
                                idRaportDesign = clsKoka.IdRaportDesing;
                                statusAprovimi = Convert.ToInt16(clsKoka.StatusAprovimi);
                                break;
                            case "3":
                            case "4":
                                clsVeprimBankaKoka koka = new clsVeprimBankaKoka(int.Parse(id));
                                if (hfState.Get<string>("RAPPERAN") == "Po")
                                {
                                    idRaporti = clsRaporti.KtheIdRaportiSipasEmritReal("raportUserKrediti");
                                    idRaportDesign = new colRaporteDesign(IdNdermarrja, idRaporti).FirstOrDefault().IdRaportDesign;
                                    statusAprovimi = Convert.ToInt16(StatusAprovimi.Undefined);
                                }
                                else
                                {
                                    idRaporti = DbCore.DbShare.clsRaporti.KtheIdRaporti(DbCore.IMBUtils.Messages.MessagesResource.Messages.IdGjuha, koka.IdRaportDesing);
                                    idRaportDesign = koka.IdRaportDesing;
                                    statusAprovimi = Convert.ToInt16(koka.StatusAprovimi);
                                }
                                break;
                            }
                        Container.Attributes["src"] = "RaportiShpejte.aspx?Sesioni=false&idraporti=" + idRaporti + "&idDokumenti=" + id + "&printo=false&iddesign=" + idRaportDesign + "&wf=" + statusAprovimi;


                    }
                    break;
                case "Aprovo":

                    Ruaj(idPerdorues, int.Parse(idetapa), StatusAprovimi.Per_Aprovim, idPerdorues, kategoria);
                    break;
                case "Refuzo":

                    Ruaj(idPerdorues, int.Parse(idetapa), StatusAprovimi.Refuzuar, idPerdorues, kategoria);
                    break;
                case "Delego":
                    var statusi = Convert.ToInt32(gvAprovimet.GetRowValues(gvAprovimet.FocusedRowIndex, "Statusi"));
                    string idaprovuesi = gvAprovimet.GetRowValues(gvAprovimet.FocusedRowIndex, "IdAprovuesi").ToString();
                    string lloji = gvAprovimet.GetRowValues(gvAprovimet.FocusedRowIndex, "LlojAprovuesi").ToString();
                    if (lloji == "2")//nqs eshte rol
                    {
                        DbCore.DbAdmin.colRolPerdorues col = new DbCore.DbAdmin.colRolPerdorues();
                        col.mbushRolePerdoruesSipasRoli(int.Parse(idaprovuesi));
                        if (col.Count > 0)
                            idaprovuesi = col[0].IdPerdorues.ToString();
                        else
                        {
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgKyRolNukKaPerdorues", ci), pnlMesazhi);
                            return;
                        }

                    }
                    if (statusi != (int)DbCore.DbRegjistrim.StatusAprovimi.Per_Aprovim)
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukMundTeDelegoniDokQeNukJaneNeStatPerAprovim", ci), pnlMesazhi);
                    else
                        Ruaj(idPerdorues, int.Parse(idetapa), StatusAprovimi.Deleguar, int.Parse(idaprovuesi), kategoria);
                    break;
                default:
                    break;
            }
        
        }

        private void Ruaj(int idPerdorues, int idetapa, StatusAprovimi statusapp, int idaprovuesi, int kategoria)
        {
            DbCore.clsMesazh mesazh;
            CultureInfo ci = mySessionObjects.ktheCultureInfo(Session);
            switch (kategoria)
            {
                case 38:
                    mesazh = RuajVeprimListPagesa(idPerdorues, idetapa, statusapp, idaprovuesi, ci);
                    break;

                case 3:
                    mesazh = RuajVeprimArketimi(idPerdorues, idetapa, statusapp, idaprovuesi, ci);
                    break;
                case 2:
                case 1:
                    mesazh = RuajVeprimShitje(kategoria, idPerdorues, idetapa, statusapp, idaprovuesi);
                    break;
                default:
                    throw new MyException($"Kategoria {kategoria} e panjohur!");
            }
            if (mesazh)
            {
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                mbushGridNgaDB();
            }
            else
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
        }

        private clsMesazh RuajVeprimListPagesa(int idPerdorues, int idetapa, StatusAprovimi statusapp, int idaprovuesi, CultureInfo ci)
        {
            clsEtapeAprovimi etapa = new clsEtapeAprovimi(idetapa, 38);
            colKomenteAprovimi col = new colKomenteAprovimi();
            col.merrKomenteSipasEtapes(etapa.IdEtapa);
            if (statusapp == StatusAprovimi.Refuzuar)
            {
                if (col.Count == 0)
                    return new MesazhGabimi(rm.GetString("msgShenoniKomentRefuzim", ci));

                bool kakoment = false;
                foreach (clsKomenteAprovimi k in col)
                {
                    if (k.IdPerdoruesi == idPerdorues)
                    {
                        kakoment = true;
                        break;
                    }
                }
                if (!kakoment) return new MesazhGabimi(rm.GetString("msgShenoniKomentRefuzim", ci));
            }
            var koka = new DbCore.DbListPagesat.clsKokaListPagese(etapa.IdKokaShitje, false);
            return etapa.ruaj(idPerdorues, DbCore.clsFunksione.ktheServerUrl(Request), Page, etapa.IdSkema, koka.IdKoka, idaprovuesi, statusapp, idetapa, 38, koka.IdStatusDok, koka.IdNdermarje, koka.StatusAprovimi, 0, koka.IdKoka, koka.IdPerdoruesi, Double.Parse(koka.Totali.ToString()), koka.DtKrijimi, koka.IdKonfigAmbjente, koka.NrDok, koka.DtDok);
        }

        private clsMesazh RuajVeprimShitje(int idkategoria, int idPerdorues, int idetapa, StatusAprovimi statusapp, int idaprovuesi)
        {
            clsEtapeAprovimi etapa = new clsEtapeAprovimi(idetapa, idkategoria);
            clsKokaShitje koka = new clsKokaShitje();
            koka.mbushKokaShitjeSipasIDPaTrup(etapa.IdKokaShitje);
            StatusAprovimi stsAprovimi = statusapp;
            if (statusapp == StatusAprovimi.Per_Aprovim && koka.IdStatusDok != 1 && clsAlternativaKushti.getAlternativa(koka.IdKonfigAmbjente, "SDAF") == "Ruajtur")
            {
                stsAprovimi = clsEtapeAprovimi.MerrStatusAprovimi(etapa.IdSkema, new clsDatabaseRegjistrim(), koka.IdShitjeKoka, idaprovuesi, statusapp, idetapa, idkategoria, koka.IdNdermarrje, koka.IdKlientFurnitor, koka.TotaliMeZbritjeMeTVSH * koka.Kursi, 0);
                if (stsAprovimi == StatusAprovimi.Aprovuar)
                {
                    clsKonfigurimAmbjenti konfig = new clsKonfigurimAmbjenti(koka.IdKonfigAmbjente);
                    return koka.Riruaj(true, koka.IdNdermarrje, idPerdorues, konfig, idgjuha, DbCore.mySessionObjects.merrEshteMemeSesioni(Session), DbCore.mySessionObjects.merrEshteOwnSesioni(Session), rm, ci, new Dictionary<string, object>(), clsFunksione.ktheServerUrl(Request));
                }
            }
            
            return etapa.ruaj(idPerdorues, DbCore.clsFunksione.ktheServerUrl(Request), Page, etapa.IdSkema, koka.IdShitjeKoka, idaprovuesi, stsAprovimi, idetapa, idkategoria, koka.IdStatusDok, koka.IdNdermarrje, koka.StatusAprovimi, koka.IdKlientFurnitor, koka.IdShitjeKoka, koka.IdPerdoruesi, koka.TotaliMeZbritjeMeTVSH * koka.Kursi, koka.DtKrijimi, koka.IdKonfigAmbjente, koka.NrDok, koka.DtDok);//koka.Totali - koka.Zbritje - koka.Tvsh
        }

        private clsMesazh RuajVeprimArketimi(int idPerdorues, int idetapa, StatusAprovimi statusapp, int idaprovuesi, CultureInfo ci)
        {
            clsMesazh mesazh;
            clsEtapeAprovimi etapa = new clsEtapeAprovimi(idetapa, 3);
            DbCore.DbArkaBanka.clsVeprimBankaKoka koka = new DbCore.DbArkaBanka.clsVeprimBankaKoka(etapa.IdKokaShitje);
            int dite = 0;
            if (koka.IdDokAnullimi > 0)
            {
                clsVeprimBankaKoka anull = new clsVeprimBankaKoka(koka.IdDokAnullimi);
                dite = (koka.DateDokumenti - anull.DateDokumenti).Days;
            }
            mesazh = etapa.ruaj(idPerdorues, DbCore.clsFunksione.ktheServerUrl(Request), Page, etapa.IdSkema, koka.IdKoka, idaprovuesi, statusapp, idetapa, 3, koka.IdStatusDokumenti, koka.IdNdermarje, koka.StatusAprovimi, 0, koka.IdKoka, koka.IdPerdoruesi, koka.Vlera, koka.DtKrijimi, koka.IdKonfigAmbjente, koka.NrDokumenti, koka.DateDokumenti, dite);
            return mesazh;
        }


        /// <summary>
        /// kur grida ben callback
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvAprovimet_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            if (e.CallbackName == "COLUMNMOVE" && gvAprovimet.AllColumns[int.Parse(e.Args[0])].Width.Value == 0)
                gvAprovimet.AllColumns[int.Parse(e.Args[0])].Width = System.Web.UI.WebControls.Unit.Percentage(3);
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            GridUtil.ToolTipButonaveMbiGride(gvAprovimet, cultinf, rm);
        }

        /// <summary>
        /// kur perdoruesi i ben grides callbak per te mare konfigurimin e ri
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvAprovimet_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string[] arr = e.Parameters.Split(';');
            if (veprimi == "aprovim")
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), "gvAprovimet", gvAprovimet, cmbKonfigurimi.Text.Split(';')[0], "535", DbCore.mySessionObjects.ktheGjuhe(Session));
            else GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), "gvAprovimet", gvAprovimet, cmbKonfigurimi.Text.Split(';')[0], "536", DbCore.mySessionObjects.ktheGjuhe(Session));
            if (arr.Length == 3)
            {
                if (arr[2] == "")
                    gvAprovimet.FilterExpression = " ";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    //filtra.mbushFiltraGridaSipasFiltraKodi(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvAprovimet", "ListeAprovimi.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvAprovimet.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvAprovimet);
                    }
                }
            }

            gvAprovimet.Selection.UnselectAll();
        }

        /// <summary>
        /// perdoret per te vendosur karakteristika te griden ne javascript
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvAprovimet_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvAprovimet.PageIndex;
            e.Properties["cpPageRow"] = gvAprovimet.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvAprovimet.VisibleRowCount;
        }

        /// <summary>
        /// perdoret per te hequr filtrimin e grides kur zgjedhim elementin bosh
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvAprovimet_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if (e.Column.Name.ContainsAnyIgnoreCase("Muaji", "Statusi"))
            {
                if (Convert.ToInt32(e.Value) == 0) e.Criteria = null;
            }
        }
        protected void gvAprovimet_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
            if (e.DataColumn.FieldName == "Statusi")
            {
                switch ((e.CellValue.ToString()))
                {
                    case "1":
                        e.Cell.BackColor = Color.Orange;
                        break;
                    case "2":
                        e.Cell.BackColor = Color.Green;
                        break;
                    case "3":
                        e.Cell.BackColor = Color.Red;
                        break;
                    case "4":
                        e.Cell.BackColor = Color.DeepSkyBlue;
                        break;
                    default:
                        e.Cell.BackColor = Color.Transparent;
                        break;
                }
            }
        }
        /// <summary>
        /// perdoret per te shtuar menyra filtrimi
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvAprovimet_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            string TeGjithe = rm.GetString("GridHeaderFilterFillItemTeGjithe", ci);
            string nga = rm.GetString("GridHeaderFilterFillItemNga", ci);
            if (e.Column.FieldName == "NrDokokumenti" || e.Column.FieldName == "Krijuesi")
            {
                e.Values.Clear();
                //e.AddShowAll();
                e.AddValue(TeGjithe, string.Empty, "true");
                e.AddValue(nga + " A-D ", string.Empty, e.Column.FieldName + ">'A     ' and " + e.Column.FieldName + " <'DDDDDDD'");
                e.AddValue(nga + " D-G ", string.Empty, e.Column.FieldName + ">'D     ' and " + e.Column.FieldName + "<'GGGGGGG'");
                e.AddValue(nga + " H-K ", string.Empty, e.Column.FieldName + ">'H     ' and " + e.Column.FieldName + "<'KKKKKKK'");
                e.AddValue(nga + " L-O ", string.Empty, e.Column.FieldName + ">'L     ' and " + e.Column.FieldName + "  <'OOOOOOO'");
                e.AddValue(nga + " P-S ", string.Empty, e.Column.FieldName + ">'P     ' and " + e.Column.FieldName + "<'SSSSSSS'");
                e.AddValue(nga + " T-W ", string.Empty, e.Column.FieldName + ">'T     ' and " + e.Column.FieldName + "<'WWWWWWW'");
                e.AddValue(nga + " X-Z ", string.Empty, e.Column.FieldName + ">'X     ' and " + e.Column.FieldName + "<'ZZZZZZZ'");
            }
            else
            {
                e.Values.Clear();
                e.AddValue(TeGjithe, string.Empty, "true");
            }
        }


    }
}