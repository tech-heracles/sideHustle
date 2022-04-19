using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Sockets;
using System.Web.Configuration;
using System.Web.UI;
using DbCore;
using DbCore.DbInventari;
using DbCore.DbRegjistrim;
using DbCore.DbShare;
using DbCore.Integrime;
using DevExpress.Web;
using Newtonsoft.Json;
using NLog;
using PlatinumWeb.Templates;
using DbCore.IMBUtils;
using DbCore.IMBUtils.Logging;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class LupaValidim : MyPageBase
    {
        private const string gvSessionKeyLupaValidim = "LupaValidim_gvArtikulli";



        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                int idPerdoruesi;
                int idGjuha;
                int idNdermarrje;
                int idViti;
                int idNdermarrjeVit;
                bool eshteOwn, eshteMeme;

                int IdKonfigurimi;
                if (!IsPostBack)
                {
                    if (!mySessionObjects.isLogedIn(Session))
                    {
                        Response.Redirect($"{Paths.defaultLoginPath}arsye=FaqePaautorizuar");
                    }
                    idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);
                    idGjuha = mySessionObjects.ktheGjuhe(Session);
                    idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
                    idViti = mySessionObjects.ktheIdVitNdermarrje(Session);
                    idNdermarrjeVit = mySessionObjects.ktheNdermarrjeVit(Session);
                    eshteOwn = mySessionObjects.merrEshteOwnSesioni(Session);
                    eshteMeme = mySessionObjects.merrEshteMemeSesioni(Session);
                    hfState.Set("idPerdoruesi", idPerdoruesi);
                    hfState.Set("idGjuha", idGjuha);
                    hfState.Set("idNdermarrje", idNdermarrje);
                    hfState.Set("idViti", idViti);
                    hfState.Set("idNdermarrjeVit", idNdermarrjeVit);
                    hfState.Set("OwnShop", eshteOwn);
                    hfState.Set("Meme", eshteMeme);
                    int idmag = 0;
                    if (Request.QueryString["mag"] != "null")
                        idmag = int.Parse(Request.QueryString["mag"]);
                    if (Request.QueryString["kodi"] == "VFONE" && Request.QueryString["KGJVFONE"] != null)
                        hfState.Set("KontrolloGjendjeOwn", bool.Parse(Request.QueryString["KGJVFONE"]));
                    else
                        hfState.Set("KontrolloGjendjeOwn", false);

                    clsNjesiAdministrative njesi = new clsNjesiAdministrative(idmag);
                    clsKonfigurimAmbjenti konf = new clsKonfigurimAmbjenti();
                    if (Request.QueryString["kodi"] == "VFONE")
                    {
                        konf.mbushKonfigAmbjSipasKod("VFONE" + (njesi.Kodi == null ? "" : njesi.Kodi), idNdermarrje);

                        if (konf.IdKonfigAmbjente == 0)
                            konf.mbushKonfigAmbjSipasKod("VFONE", idNdermarrje);
                    }
                    else if (Request.QueryString["kodi"] == "USHDD")
                    {
                        hfLloji.Value = "DD";
                        konf.mbushKonfigAmbjSipasKod("POROSIDD" + njesi.Kodi, idNdermarrje);
                    }
                    else if (Request.QueryString["kodi"] == "BAZAA")
                    {
                        hfLloji.Value = "BAZAAR";
                        konf.mbushKonfigAmbjSipasKod("POROSIBAZAAR" + njesi.Kodi, idNdermarrje);
                    }
                    IdKonfigurimi = konf.IdKonfigAmbjente;
                    hfState.Set("IdNivel", konf.IdNivel.ToString());
                    hfState.Set("IdKonfigurimi", IdKonfigurimi);

                    percaktoTemplateMenu(ASPxMenu1, idViti, idPerdoruesi, idNdermarrje, idGjuha, eshteMeme);
                    //clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, "gvLupaArtikull",1, "LupaValidim.aspx");

                    mbushPopUpListeArtikujshNgaDB(idPerdoruesi, idNdermarrje, new List<string>());
                    konfiguroPopupGride(idNdermarrje);
                    GridUtil.percaktoVisibleColumns(idGjuha, idNdermarrje, gvLupaArtikull, "gvLupaArtikull", "LupaValidim.aspx");
                }
                else
                {
                    idPerdoruesi = (int)hfState["idPerdoruesi"];
                    idGjuha = (int)hfState["idGjuha"];
                    idNdermarrje = (int)hfState["idNdermarrje"];
                    idViti = (int)hfState["idViti"];
                    idNdermarrjeVit = (int)hfState["idNdermarrjeVit"];
                    eshteOwn = (bool)hfState["OwnShop"];
                    eshteMeme = (bool)hfState["Meme"];
                    IdKonfigurimi = (int)hfState["IdKonfigurimi"];
                    if (!IsCallback || (IsCallback && Request["__CALLBACKID"].Contains("ASPxMenu1")))
                        percaktoTemplateMenu(ASPxMenu1, idViti, idPerdoruesi, idNdermarrje, idGjuha, eshteMeme);

                    if (!IsCallback || (IsCallback && Request["__CALLBACKID"].Contains("gvLupaArtikull")))
                    {
                        mbushPopUpListeArtikujshNgaSession(idPerdoruesi, idNdermarrje);
                        konfiguroPopupGride(idNdermarrje);
                    }
                }
                System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
                var endlessScroll = clsAlternativaKushti.getAlternativa(IdKonfigurimi, "ES") == "Po";
                        GridUtil.KonfiguroGrideListeMadhePopupiPaTheme(gvLupaArtikull, "IdArtikulli", true, endlessScroll);
            
                    
            }
            catch (Exception ex)
            {
                hfStatus.Value = "false";
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, ex.Message, pnlMesazhi);
            }
        }

        private void percaktoTemplateMenu(ASPxMenu aSPxMenu1, int idViti, int idPerdorues, int idNdermarrje, int idGjuha, bool meme)
        {
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, "LupaValidim.aspx", this, MenuInfo, null, null, true, false, false, meme);
        }

        private void mbushPopUpListeArtikujshNgaSession(int idPerdoruesi, int idNdermarrje)
        {
            DataTable dt = (DataTable)mySessionObjects.merrObjectNgaSesioni(Session, gvSessionKeyLupaValidim);
            if (dt == null)
                mbushPopUpListeArtikujshNgaDB(idPerdoruesi, idNdermarrje, new List<string>());
            else
            {
                gvLupaArtikull.DataSource = dt;
                gvLupaArtikull.DataBind();
            }
        }

        private void mbushPopUpListeArtikujshNgaDB(int idPerdoruesi, int idNdermarrje, List<String> dhuratat)
        {//mbush griden e popupit me te dhena
            DataTable dt;
            DataTable dtdhur = new DataTable();
            dt = colArtikujt.ktheArtikujNdermarrjesAndAutorizimeSipasPikeve(idNdermarrje, idPerdoruesi, decimal.Parse(lblPiket.Text), int.Parse(hfIdMag.Value == "" ? "0" : hfIdMag.Value), hfData.Value == "" ? DateTime.Today : DateTime.Parse(hfData.Value));

            dtdhur = dt.Clone();

            foreach (DataRow dr in dt.Rows)
            {
                if (dhuratat.Contains(dr["KodVFOne"].ToString()))
                {
                    dtdhur.Rows.Add(dr.ItemArray);
                }
            }

            mySessionObjects.ruajObjectNeSesion(Session, dtdhur, gvSessionKeyLupaValidim);
            gvLupaArtikull.DataSource = dtdhur;
            gvLupaArtikull.DataBind();
            dt.Dispose();
        }

        private void mbushPopUpListeArtikujshNgaDBDiscount(int idPerdoruesi, int idNdermarrje, clsKlientMeKupon kuponi)
        {//mbush griden e popupit me te dhena
            DataTable dt;
            DataTable dtdhur = new DataTable();
            //string kodi = dhuratat.Count > 0 ? dhuratat[0] : "";

            int idmagazina = int.Parse(hfIdMag.Value == "" ? "0" : hfIdMag.Value);
            DateTime data = hfData.Value == "" ? DateTime.Today : DateTime.Parse(hfData.Value);

            dt = colArtikujt.ktheArtikujNdermarrjesAndAutorizimeSipasKodit(idNdermarrje, idPerdoruesi, kuponi.Aparati, idmagazina, data);

            //todo do merret nga ata apo ta llogarisim ne ne alphaweb  ne baze te cmimit

            foreach (DataRow dr in dt.Rows)
            {
                dr["Vlere"] = kuponi.VleraEZbritjes;
            }

            mySessionObjects.ruajObjectNeSesion(Session, dt, gvSessionKeyLupaValidim);
            gvLupaArtikull.DataSource = dt;
            gvLupaArtikull.DataBind();
            dt.Dispose();
        }

        private void mbushPopUpListeArtikujshNgaDBBazaar(int idPerdoruesi, int idNdermarrje)
        {//mbush griden e popupit me te dhena

            int idmagazina = int.Parse(hfIdMag.Value == "" ? "0" : hfIdMag.Value);
            DateTime data = hfData.Value == "" ? DateTime.Today : DateTime.Parse(hfData.Value);
            DataTable dt = colArtikujt.ktheArtikujNdermarrjesAndAutorizimeSipasBazaarit(idNdermarrje, idPerdoruesi, idmagazina, data);
            mySessionObjects.ruajObjectNeSesion(Session, dt, gvSessionKeyLupaValidim);
            gvLupaArtikull.DataSource = dt;
            gvLupaArtikull.DataBind();
        }

        private void konfiguroPopupGride(int idNdermarrje)
        {//konfiguron popupgriden
            GridViewDataColumn col2 = gvLupaArtikull.Columns["Gjendje"] as GridViewDataColumn;
            col2.DataItemTemplate = new MyLabelTemplate();
            gvLupaArtikull.Columns["Gjendje"].VisibleIndex = 100;
            GridViewDataColumn col42 = gvLupaArtikull.Columns["Gjendje2"] as GridViewDataColumn;
            col42.DataItemTemplate = new MyLabelTemplate();
            col42.Width = 0;
            GridViewDataColumn cole2 = gvLupaArtikull.Columns["Vlere"] as GridViewDataColumn;
            cole2.DataItemTemplate = new MyLabelTemplate();
            GridViewDataColumn col = gvLupaArtikull.Columns["Shitje"] as GridViewDataColumn;
            col.DataItemTemplate = new MyButtonTemplate("Shitje");
            col.VisibleIndex = 101;
            GridViewDataColumn col1 = gvLupaArtikull.Columns["Porosit"] as GridViewDataColumn;
            col1.DataItemTemplate = new MyButtonTemplate("Porosit");
            col1.VisibleIndex = 102;
            if (Request.QueryString["kodi"] == "USHDD" || Request.QueryString["kodi"] == "BAZAA")
            {
                GridViewDataColumn col12 = gvLupaArtikull.Columns["Pike"] as GridViewDataColumn;
                col12.Visible = false;

                GridViewDataColumn col32 = gvLupaArtikull.Columns["KodVFOne"] as GridViewDataColumn;
                col32.Visible = false;

                GridViewDataColumn col3 = gvLupaArtikull.Columns["Vlere"] as GridViewDataColumn;
                if (Request.QueryString["kodi"] == "USHDD") col3.Caption = "Zbritja";
                else
                {
                    col3.Visible = false;
                    //  col3.Width = 0;
                    col3.Caption = "Disponibiliteti";
                }
            }
            GridViewDataColumn colArtikulli = gvLupaArtikull.Columns["KodArtikulli"] as GridViewDataColumn;
            colArtikulli.Width = System.Web.UI.WebControls.Unit.Percentage(40);

            GridViewDataColumn colOwn = gvLupaArtikull.Columns["GjendjeOwn"] as GridViewDataColumn;
            if (colOwn != null)
            {
                colOwn.DataItemTemplate = new MyLabelTemplate();
                colOwn.VisibleIndex = 126;
                colOwn.Visible = false;
            }
            GridViewDataColumn colPorositur = gvLupaArtikull.Columns["Porositur"] as GridViewDataColumn;
            if (colPorositur != null)
            {
                colPorositur.DataItemTemplate = new MyLabelTemplate();
                colPorositur.VisibleIndex = 127;
                colPorositur.Visible = false;
            }
        }

        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            try
            {
                percaktoTemplateMenu(ASPxMenu1, (int)hfState["idViti"], (int)hfState["idPerdoruesi"], (int)hfState["idNdermarrje"], (int)hfState["idGjuha"], (bool)hfState["Meme"]);
            }
            catch (Exception ex)
            {
                hfStatus.Value = "false";
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, ex.Message, pnlMesazhi);
            }
        }

        protected void gvLupaArtikull_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            try
            {
                gvLupaArtikull.Selection.UnselectAll();
            }
            catch (Exception ex)
            {
                hfStatus.Value = "false";
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, ex.Message, pnlMesazhi);
            }
        }

        protected void gvLupaArtikull_DataBound(object sender, EventArgs e)
        {
            try
            {
                //perdoret per ti vene disa atribute grides
                //behet per te afishuar rreshtin qe do sherbej per filtrim
                gvLupaArtikull.Settings.ShowFilterRow = true;
                gvLupaArtikull.KeyFieldName = "IdArtikulli";
                gvLupaArtikull.SettingsBehavior.AllowSelectByRowClick = true;
            }
            catch (Exception ex)
            {
                hfStatus.Value = "false";
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, ex.Message, pnlMesazhi);
            }
        }

        protected void gvLupaArtikull_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            try
            {
                //kur popupgrida ben callback
                gvLupaArtikull.Selection.UnselectAll();
            }
            catch (Exception ex)
            {
                hfStatus.Value = "false";
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, ex.Message, pnlMesazhi);
            }
        }

        protected void btnGjeneroPin_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNrKontakti.Text == "")
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ju lutem jepni nr e kontaktit!", pnlMesazhi);
                else
                {
                    clsMesazh mesazh = /*new DbCore.clsMesazh(true);*/  clsSocket.GjeneroPinVfOne(txtNrKontakti.Text);
                    if (mesazh.Status)
                    {
                        clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                    }
                    else
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                    }
                }
            }
            catch (Exception ex)
            {
                hfStatus.Value = "false";
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, ex.Message, pnlMesazhi);
            }
        }

        protected void btnVerifikoPIN_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPIN.Text == "")
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ju lutem jepni nr e pinin!", pnlMesazhi);
                else
                {
                    clsMesazh mesazh = clsSocket.VerifikoPinVfOne(txtNrKontakti.Text, txtPIN.Text);
                    if (mesazh.Status)
                    { //to do merr pike
                        hfStatus.Value = "true";

                        clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "PIN i vendosur eshte i sakte mund te vazhdoni me shitjen!", pnlMesazhi);
                        if (Request.QueryString["status"] != "0")
                        {
                            var vfOneAdapter = new PromocioneAdapter();
                            int piketKthyera = 0;

                            var infoNumri = vfOneAdapter.MerrInfoPerNumrin("3556" + txtNrKontakti.Text, out piketKthyera);
                            mesazh = infoNumri.Item1;
                            List<String> dhuratat = infoNumri.Item2;
                            if (mesazh.Status)
                            {
                                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                            }
                            else
                            {
                                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                            }
                            lblPiket.Text = piketKthyera.ToString();
                            int idPerdoruesi = (int)hfState["idPerdoruesi"];
                            int idNdermarrje = (int)hfState["idNdermarrje"];

                            mbushPopUpListeArtikujshNgaDB(idPerdoruesi, idNdermarrje, dhuratat);
                        }
                    }

                    else
                    {
                        hfStatus.Value = "false";

                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "PIN i vendosur nuk eshte i sakte!", pnlMesazhi);
                    }
                }
            }
            catch (Exception ex)
            {
                hfStatus.Value = "false";
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, ex.Message, pnlMesazhi);
            }
        }

        protected void gvLupaArtikull_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType != GridViewRowType.Data)
                return;
            try
            {

                bool eshteOwn = (bool)hfState["OwnShop"];
                GridViewDataTextColumn col0 = ((ASPxGridView)sender).Columns["Shitje"] as GridViewDataTextColumn;
                GridViewDataTextColumn col1 = ((ASPxGridView)sender).Columns["Porosit"] as GridViewDataTextColumn;
                GridViewDataTextColumn col2 = ((ASPxGridView)sender).Columns["Gjendje"] as GridViewDataTextColumn;
                GridViewDataTextColumn col42 = ((ASPxGridView)sender).Columns["Gjendje2"] as GridViewDataTextColumn;
                GridViewDataTextColumn col12 = ((ASPxGridView)sender).Columns["Vlere"] as GridViewDataTextColumn;

                ASPxButton btnShit = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col0, "btn") as ASPxButton;
                ASPxButton btnPorosit = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col1, "btn") as ASPxButton;
                ASPxLabel lblGjendje = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col2, "lbl") as ASPxLabel;
                ASPxLabel lblbGjendje2 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col42, "lbl") as ASPxLabel;
                ASPxLabel lblVlere = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col12, "lbl") as ASPxLabel;
                string[] fusha = { "Porositur", "GjendjeOwn", "Vlere" };
                var data = ((ASPxGridView)sender).GetRowValuesByKeyValue(e.KeyValue, fusha);
                decimal vlera = decimal.Parse(((object[])data)[2].ToString());
                //vendosen client side eventet e kolonave
                if (btnShit != null)
                {
                    if (Request.QueryString["kodi"] == "BAZAA")
                    {
                        if (vlera <= 0)
                            btnShit.ClientEnabled = false;
                        else
                            btnShit.ClientEnabled = decimal.Parse(lblGjendje.Text) > 0;
                    }
                    else
                        btnShit.ClientEnabled = decimal.Parse(lblGjendje.Text) > 0;

                    btnShit.ClientInstanceName = String.Format("btnShitje{0}", e.VisibleIndex);
                    btnShit.ClientSideEvents.Click = "function(s,e){ShitjeClicked(" + e.VisibleIndex + ",e);}";
                }

                if (btnPorosit != null)
                {

                    clsArtikulli artikulli = new clsArtikulli(int.Parse(e.KeyValue.ToString()));

                    btnPorosit.ClientEnabled = artikulli.MundTePorositet(decimal.Parse(lblGjendje.Text), decimal.Parse(lblbGjendje2.Text), decimal.Parse(((object[])data)[1].ToString()), decimal.Parse(((object[])data)[0].ToString()), eshteOwn, vlera, Request.QueryString["kodi"] == "BAZAA", Convert.ToBoolean(hfState.Get("KontrolloGjendjeOwn")));

                    btnPorosit.ClientInstanceName = String.Format("btnPorosit{0}", e.VisibleIndex);
                    btnPorosit.ClientSideEvents.Click = "function(s,e){PorositClicked(" + e.VisibleIndex + ",e);}";
                }

            }
            catch (Exception ex)
            {
                hfStatus.Value = "false";
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, ex.Message, pnlMesazhi);
            }
        }



        protected void btnValido_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtKodi.Text == "")
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ju lutem jepni Kodin!", pnlMesazhi);
                else
                {
                    int idPerdoruesi = (int)hfState["idPerdoruesi"];
                    int idNdermarrje = (int)hfState["idNdermarrje"];
                    clsMesazh mesazh = new clsMesazh(false);

                    //kontrollo nese eshte i vlefshem ne alphaweb kodi i kuponit
                    mesazh = clsKlientMeKupon.ValidoKuponKlienti(txtKodi.Text.ToUpper());
                    ImbLogger.LogInfoPromocione("po behet validimi i klientit me kodin {0} nga perdoruesi {1}", txtKodi.Text, idPerdoruesi);


                    if (mesazh.Status)
                    {
                        hfStatus.Value = "true";

                        clsKlientMeKupon kuponi = new clsKlientMeKupon(txtKodi.Text.ToUpper());

                        hfKodKuponiDD.Value = kuponi.KodKuponi;
                        clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "Kodi i vendosur eshte i sakte mund te vazhdoni me shitjen!", pnlMesazhi);
                        if (Request.QueryString["status"] != "0")
                        {


                            mbushPopUpListeArtikujshNgaDBDiscount(idPerdoruesi, idNdermarrje, kuponi);
                        }
                    }
                    else
                    {
                        hfStatus.Value = "false";

                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                    }
                }
            }
            catch (Exception ex)
            {
                hfStatus.Value = "false";
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, ex.Message, pnlMesazhi);
            }
        }

        protected void btnGjeneroPinBazaar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(hfLloji.Value))
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "mungon lloji i msisdn!", pnlMesazhi);
                    return;
                }
                LlojMsisdn lloji = LlojMsisdn.Undefined;

                switch (hfLloji.Value)
                {
                    case "BAZAAR":
                        lloji = LlojMsisdn.Bazaar;
                        break;
                    case "DD":
                        lloji = LlojMsisdn.DeviceWithDiscount;
                        break;
                }

                if (txtKontaktiBazaar.Text == "")
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ju lutem jepni nr e kontaktit!", pnlMesazhi);
                else
                {
                    int idPerdoruesi = (int)hfState["idPerdoruesi"];
                    int idNdermarrje = (int)hfState["idNdermarrje"];
                    clsMesazh mesazh = new clsMesazh(true);

                    //string kodiAparatit = "";
                    //string kodAparatiEsales = "";

                    ImbLogger.LogInfoPromocione("po behet validimi i klientit me msisdn {0} nga perdoruesi {1}", txtKontaktiBazaar.Text, idPerdoruesi);

                    mesazh = clsKlientPerBazaar.ValidoMsisdn(txtKontaktiBazaar.Text, lloji);

                    ImbLogger.LogInfoPromocione(" validimi i klientit me msisdn {0} perfundoi me statusin {1} dhe mesazh '{2}' nga perdoruesi {3}", txtKodi.Text, mesazh.Status, mesazh.PershkrimMesazhi, idPerdoruesi);


                    if (!mesazh.Status) throw new Exception(mesazh.PershkrimMesazhi);


                    switch (lloji)
                    {
                        case LlojMsisdn.Bazaar:
                            mesazh = clsSocket.GjeneroPinPerBazaar(txtKontaktiBazaar.Text);
                            break;
                        case LlojMsisdn.DeviceWithDiscount:
                            mesazh = clsSocket.GjeneroPinPerDD(txtKontaktiBazaar.Text);
                            break;
                        default:
                            mesazh = new clsMesazh(false, "lloj promocioni i panjohur!!");
                            break;
                    }


                    if (!mesazh.Status) throw new Exception(mesazh.PershkrimMesazhi);

                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                    hfStatus.Value = "true";
                    //hfTenure.Value = JsonConvert.SerializeObject(new
                    //{
                    //    KODAPARATI = kodiAparatit,
                    //    KODAPARATI_ESALES = kodAparatiEsales

                    //});

                }
            }
            catch (Exception err)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, err.Message, pnlMesazhi);
                ImbLogger.LogErrorPromocione(err.Message);
            }


        }

        protected void btnVerifikoPINBazaar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPINBazaar.Text == "")
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ju lutem jepni nr e pinin!", pnlMesazhi);
                else
                {
                    LlojMsisdn lloji = LlojMsisdn.Undefined;

                    if (hfLloji.Value == "BAZAAR")
                        lloji = LlojMsisdn.Bazaar;
                    else if (hfLloji.Value == "DD")
                        lloji = LlojMsisdn.DeviceWithDiscount;
                    int idPerdoruesi = (int)hfState["idPerdoruesi"];
                    int idNdermarrje = (int)hfState["idNdermarrje"];
                    clsMesazh mesazh = new clsMesazh(true);

                    if (lloji == LlojMsisdn.Bazaar)
                        mesazh = clsSocket.VerifikoPinBazaar(txtKontaktiBazaar.Text, txtPINBazaar.Text);
                    else if (lloji == LlojMsisdn.DeviceWithDiscount)
                        mesazh = clsSocket.VerifikoPinDD(txtKontaktiBazaar.Text, txtPINBazaar.Text);

                    if (mesazh.Status)
                    {
                        hfStatus.Value = "true";
                        if (lloji == LlojMsisdn.DeviceWithDiscount)
                            hfStatusDD.Value = bool.TrueString;
                        clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "PIN i vendosur eshte i sakte mund te vazhdoni me shitjen!", pnlMesazhi);
                        if (Request.QueryString["status"] != "0" && lloji == LlojMsisdn.Bazaar)
                        {
                            mbushPopUpListeArtikujshNgaDBBazaar(idPerdoruesi, idNdermarrje);
                        }
                    }
                    else
                    {
                        hfStatus.Value = "false";
                        if (lloji == LlojMsisdn.DeviceWithDiscount)
                            hfStatusDD.Value = bool.FalseString;
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "PIN i vendosur nuk eshte i sakte!", pnlMesazhi);
                    }
                }
            }
            catch (Exception ex)
            {
                hfStatus.Value = "false";
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, ex.Message, pnlMesazhi);
                ImbLogger.LogErrorPromocione("gabim ne validim per numrin {0}  error {1}", txtKontaktiBazaar.Text, ex.Message);
            }
        }

        protected void btnAktivizo_Click(object sender, EventArgs e)
        {

            try
            {
                if (string.IsNullOrEmpty(txtKodiBundle.Text))
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ju lutem jepni nr e kontaktit!", pnlMesazhi);
                    return;
                }
                clsMesazh mesazh = new clsMesazh(true);
                int idPerdoruesi = (int)hfState["idPerdoruesi"];
                int idNdermarrje = (int)hfState["idNdermarrje"];
                string bundleCode = "";

                mesazh = clsKokaShitje.AktivizoBundle(txtKodiBundle.Text, bundleCode, "Device with promo", "0", idPerdoruesi);
                if (mesazh.KodMesazhi == -1)//ka ndodhur gabim
                {
                    hfStatusBundle.Value = "false";
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                }
                else if (mesazh.KodMesazhi == 0)//info 
                {
                    clsMenuInfo.ShtoMesazhInformues(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                    hfStatusBundle.Value = "true";
                }
                else//sukses
                {
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                    hfStatusBundle.Value = "true";
                }

            }
            catch (Exception ex)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, ex.Message, pnlMesazhi);
                hfStatusBundle.Value = "false";
            }

            ///to do validimi nqs nr e ka kete kod oferte.nqs po te jepet nje nr tjeter nqs jo te aktivizohet

        }

    }
}