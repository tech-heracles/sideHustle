using System;
using System.Collections.Generic;
using System.Data;
using DbCore;
using DbCore.DbAdmin;
using DbCore.DbInventari;
using DbCore.DbShare;
using DbCore.IMBUtils.Messages;
using DevExpress.Web;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class Shto_AfateMaturimi : MyPageBase
    {
        private static string Komponente => "Shto_AfateMaturimi.aspx";

        protected void Page_Init(object sender, EventArgs e)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!mySessionObjects.isLogedIn(Session))
            {
                clsFunksione.logout(Session, true, "FaqePaautorizuar");
            }

            ShtoMenuControlsDheMsgFrame();

            if (!Page.IsPostBack)
            {
                ShtoVleraTePergjithshmeNeHfState();
                EmrateTabeve();
                EmrateLabelave();
                KonfiguroVleraFillestare();
                ASPxPageControl1.ActiveTabIndex = 0;
                MbushListeMaturimesh();

                var tedrejtaInfo = new clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();

                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, "Shto_AfateMaturimi.aspx");
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(IdNdermarrja, "gvAfateMaturimi", gvAfateMaturimi, cmbKonfigurimi.Text.Split(';')[0], 426.ToString(), IdGjuha);
            }
            else
            {
                MbushGridMaturimeshNgaSession();
            }

            GridUtil.konfigGrideListeEMadhePaTheme(gvAfateMaturimi, "IdMaturimi");
            clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, IdNdermarrja, "gvAfateMaturimi", int.Parse(cmbKonfigurimi.Value.ToString()), Komponente);
            GridUtil.EmrateButonaveMbiGride(gvAfateMaturimi);
            KonfiguroGride();
        }

        /// <summary>
        /// Vendos emrat e tabeve ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateTabeve()
        {
            ASPxPageControl1.TabPages[0].Text = MessagesResource.Messages["labelAdministrimiTePergjithshme"];
            ASPxPageControl1.TabPages[1].Text = MessagesResource.Messages["labelBlerjeShitjeAfateMaturimi"];
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        private void EmrateLabelave()
        {
            popFshi.HeaderText = MessagesResource.Messages["labelBlerjeShitjeKujdes"];
            lblMsgbox.Text = MessagesResource.Messages["labelBlerjeShitjeMesazhJeniiSigurt"];
            ButtonOk.Text = MessagesResource.Messages["labelBlerjeShitjeOK"];
            ButtonCancel.Text = MessagesResource.Messages["labelBlerjeShitjeAnullo"];
            popupUniversal.HeaderText = MessagesResource.Messages["popupBlerjeShitjeUniversal"];
        }

        private void KonfiguroVleraFillestare()
        {
            ConfigureAspxComboBox.mbushComboLloji(cmbLloji);
            ConfigureAspxComboBox.KonfiguroComboBoxDateFillimiMaturiteti(cmbDtFillimi);
            ConfigureAspxComboBox.KonfiguroComboBoxPeriudheMaturiteti(cmbPeriudha);
            ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(IdPerdoruesi, IdNdermarrja, cmbKonfigurimi, 40, rm, ci, IdGjuha);
            cmbKonfigurimi.SelectedIndex = 0;

            var konfigurimAmbjenti = new clsKonfigurimAmbjenti();
            konfigurimAmbjenti.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()), IdGjuha);

            hfKonffillestar.Value = konfigurimAmbjenti.KodKonfigAmbjente + ";" + konfigurimAmbjenti.PershkrimKonfigAmbjente;
        }

        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            var rreshtat = ASPxPageControl1.ActiveTabIndex == 0
                ? gvAfateMaturimi.GetSelectedFieldValues("IdMaturimi")
                : new List<object> { hfId.Value };

            if (rreshtat.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(_menuInfo, MessagesResource.Messages["msgBlerjeShitjeZgjidhniMaturim"], _pnlMesazhi);
                return;
            }

            List<string> teFshire = new List<string>(), tePaFshire = new List<string>();
            var db = new clsDatabaseInventari();

            foreach (var id in rreshtat)
            {
                var maturim = new clsMaturimi(Convert.ToInt32(id));

                if (db.KaVeprimeMaturimi(maturim.IdMaturimi))
                {
                    tePaFshire.Add(maturim.KodMaturimi);
                    continue;
                }

                var mesazh = maturim.Fshi();
                if (maturim.IdMaturimi == 0)
                    continue;
                if (mesazh.Status)
                {
                    HiqAfatMaturimNgaGrida(maturim.IdMaturimi);
                    teFshire.Add(maturim.KodMaturimi);

                    ASPxPageControl1.ActiveTabIndex = 0;
                    hfStatusi.Value = "true";
                }
            }

            db.Dispose();

            string mesazhInfoGabim = "", mesazhInfoSukses = "";

            if (tePaFshire.Count == 1)
                mesazhInfoGabim = string.Format("{0}{1}{2}", MessagesResource.Messages["msgBlerjeShitjeMaturimKod"], string.Join(";", tePaFshire), MessagesResource.Messages["msgBlerjeShitjeNukFshihetNjejes"]);
            else if (tePaFshire.Count > 1)
                mesazhInfoGabim = string.Format("{0}{1}{2}", MessagesResource.Messages["msgBlerjeShitjeMaturimetKod"], string.Join(";", tePaFshire), MessagesResource.Messages["msgBlerjeShitjeNukFshihetShumes"]);

            if (teFshire.Count == 1)
                mesazhInfoSukses = string.Format("{0}{1}{2}", MessagesResource.Messages["msgBlerjeShitjeMaturimKod"], string.Join(";", teFshire), MessagesResource.Messages["msgBlerjeShitjeFshirjeSuksesNjejes"]);
            else if (teFshire.Count > 1)
                mesazhInfoSukses = string.Format("{0}{1}{2}", MessagesResource.Messages["msgBlerjeShitjeMaturimetKod"], string.Join(";", teFshire), MessagesResource.Messages["msgBlerjeShitjeFshirjeSuksesShumes"]);

            if (mesazhInfoGabim != "" && mesazhInfoSukses != "")
                mesazhInfoGabim += MessagesResource.Messages["msgBlerjeShitjelidhesKurse"] + mesazhInfoSukses;

            if (mesazhInfoGabim != "")
                clsMenuInfo.ShtoMesazhGabimi(_menuInfo, mesazhInfoGabim, _pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhSuksesi(_menuInfo, mesazhInfoSukses, _pnlMesazhi);
        }

        public clsMaturimi KrijoAfatMaturimi()
        {
            return new clsMaturimi
            {
                KodMaturimi = txtKodi.Text,
                PershkrimMaturimi = txtEmertimi.Text,
                PercaktimMaturimi = int.Parse(txtPercaktimi.Text),
                IdStatusDok = 1,
                LlojMaturimi = cmbLloji.Text == "Klient" || cmbLloji.Value.ToString() == "True",
                IdPeriudha = (PeriudheMaturiteti)Convert.ToInt32(cmbPeriudha.Value),
                IdDateFillimi = (DateFillimiMaturiteti)Convert.ToInt32(cmbDtFillimi.Value),
                IdPerdoruesi = IdPerdoruesi,
                IdNdermarje = IdNdermarrja
            };
        }

        private void RuajAfatMaturimi()
        {
            if (Page.IsValid == false)
                return;

            if (IsValidMaturimi())
            {
                var maturimi = KrijoAfatMaturimi();

                var tedrejtaInfo = new clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, Komponente);
                try
                {
                    if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
                    {
                        if (!tedrejtaInfo.DShtim)
                        {
                            clsMenuInfo.ShtoMesazhGabimi(_menuInfo, MessagesResource.Messages["msgBlerjeShitjeNukKeniTeDrejta"],
                                _pnlMesazhi);
                            hfStatusi.Value = "false";
                            return;
                        }

                        maturimi.Ruaj();
                        ShtoAfatMaturimiNeGrid(maturimi.IdMaturimi);

                        clsMenuInfo.ShtoMesazhSuksesi(_menuInfo, MessagesResource.Messages["msgBlerjeShitjeRuajtjeMeSukses"],
                            _pnlMesazhi);
                        hfStatusi.Value = "true";
                    }
                    else
                    {
                        if (!tedrejtaInfo.DMod)
                        {
                            clsMenuInfo.ShtoMesazhGabimi(_menuInfo, MessagesResource.Messages["msgBlerjeShitjeNukKeniTeDrejta"],
                                _pnlMesazhi);
                            hfStatusi.Value = "false";
                            return;
                        }

                        maturimi.IdMaturimi = int.Parse(hfId.Value);
                        maturimi.Modifiko();
                        ModifikoAfatMaturimiNeGrid(maturimi.IdMaturimi);

                        clsMenuInfo.ShtoMesazhSuksesi(_menuInfo, MessagesResource.Messages["msgBlerjeShitjeRuajtjeMeSukses"],
                            _pnlMesazhi);
                        hfStatusi.Value = "true";
                    }
                }
                catch (Exception ex)
                {
                    clsMenuInfo.ShtoMesazhGabimi(_menuInfo, MessagesResource.Messages["msgBlerjeShitjeRuajtjeMeGabime"], _pnlMesazhi);
                    hfStatusi.Value = "false";
                }

                ASPxPageControl1.ActiveTabIndex = 0;
                KonfiguroVleraFillestare();
                MbushListeMaturimesh();
                KonfiguroGride();
            }
        }

        private bool IsValidMaturimi()
        {
            if (txtKodi.Text != "")
            {
                var dbInventari = new clsDatabaseInventari();
                if (clsMaturimi.Ekziston(txtKodi.Text, IdNdermarrja) && (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim"))
                {
                    hfStatusi.Value = "false";
                    clsMenuInfo.ShtoMesazhGabimi(_menuInfo, MessagesResource.Messages["msgBlerjeShitjeMaturimeKodNjejte"], _pnlMesazhi);
                    dbInventari.Dispose();
                    return false;
                }
                dbInventari.Dispose();
            }
            else
            {
                hfStatusi.Value = "false";
                clsMenuInfo.ShtoMesazhGabimi(_menuInfo, MessagesResource.Messages["msgBlerjeShitjeShenoniKodMaturimi"], _pnlMesazhi);
                return false;
            }

            if (txtEmertimi.Text == "")
            {
                clsMenuInfo.ShtoMesazhGabimi(_menuInfo, MessagesResource.Messages["msgBlerjeShitjeShenoniEmertimMaturimi"], _pnlMesazhi);
                hfStatusi.Value = "false";
                return false;
            }

            if (cmbPeriudha.Text == "")
            {
                clsMenuInfo.ShtoMesazhGabimi(_menuInfo, MessagesResource.Messages["msgBlerjeShitjeZgjidhPeriudheMaturimi"], _pnlMesazhi);
                hfStatusi.Value = "false";
                return false;
            }

            if (cmbLloji.Text == "")
            {
                clsMenuInfo.ShtoMesazhGabimi(_menuInfo, MessagesResource.Messages["msgBlerjeShitjeZgjidhLlojMaturiteti"], _pnlMesazhi);
                hfStatusi.Value = "false";
                return false;
            }

            if (cmbDtFillimi.Text == "")
            {
                clsMenuInfo.ShtoMesazhGabimi(_menuInfo, MessagesResource.Messages["msgBlerjeShitjeZgjidhDateFillimiMaturitet"], _pnlMesazhi);
                hfStatusi.Value = "false";
                return false;
            }

            if (txtPercaktimi.Text == "")
            {
                clsMenuInfo.ShtoMesazhGabimi(_menuInfo, MessagesResource.Messages["msgBlerjeShitjeShenoAfatMaturimi"], _pnlMesazhi);
                hfStatusi.Value = "false";
                return false;
            }

            return true;
        }

        #region Menu

        protected void Menu_ItemClick(object sender, MenuItemEventArgs e)
        {
            if (e.Item.Name == "Ruaj")
            {
                Page.Validate("entries");
                RuajAfatMaturimi();
            }
        }

        ///// <summary>
        ///// Mbush menune me buttonat perkates sipas faqes
        ///// </summary>
        protected void PercaktoTemplateMenu(ASPxMenu menu, ASPxMenu menuinfo)
        {
            clsToolbarConfig.percaktoTemplateMenu(IdGjuha, IdViti, IdPerdoruesi, IdNdermarrja, _menu, Komponente, this, _menuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, hfShtimModifikim.Value != "modifikim", false, false, Meme);
        }

        protected void RuajKolona_Click(object sender, EventArgs e)
        {
            var idkonf = clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(cmbKonfigurimi.Text.Split(';')[0], IdNdermarrja);
            var mesazh = GridUtil.ruajFiltra(IdNdermarrja, IdPerdoruesi, IdGjuha, "gvAfateMaturimi ", Komponente, "FilterDefault", gvAfateMaturimi.FilterExpression, gvAfateMaturimi, "Kodi", idkonf, out var idfiltri);//ruan filtrin e zgjedhur tek filtrat
            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(_menuInfo, mesazh.PershkrimMesazhi, _pnlMesazhi);
                return;
            }

            mesazh = GridUtil.ruajkonfigurimgride(gvAfateMaturimi, cmbKonfigurimi.Text, IdNdermarrja, IdPerdoruesi, 426, idfiltri, IdViti, ci, IdGjuha);/// ruan konfigurimin e grides dhe filtrin

            clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, IdNdermarrja, "gvAfateMaturimi ", int.Parse(cmbKonfigurimi.Value.ToString()), Komponente);

            PercaktoTemplateMenu(_menu, _menuInfo);

            if (mesazh.Status)
                clsMenuInfo.ShtoMesazhSuksesi(_menuInfo, mesazh.PershkrimMesazhi, _pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhGabimi(_menuInfo, mesazh.PershkrimMesazhi, _pnlMesazhi);
        }

        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
            menu_msg_Frame.FshiFilter(gvAfateMaturimi, Komponente, int.Parse(cmbKonfigurimi.Value.ToString()), ref hfStatusi);
        }

        #endregion

        #region Grid

        private void ModifikoAfatMaturimiNeGrid(int idmaturimi)
        {
            if (gvAfateMaturimi.DataSource != null)
            {
                var dt = (DataTable)gvAfateMaturimi.DataSource;
                var drs = dt.Select("IdMaturimi = " + idmaturimi);
                if (drs.Length > 1)
                    throw new Exception(MessagesResource.Messages["msgBlerjeShitjeMaturimeIdNjejte"]);

                if (drs.Length == 0)
                    return;

                drs[0].ItemArray = clsMaturimi.MerrAfatMaturimiDr(idmaturimi).ItemArray;
            }
            else
                MbushListeMaturimesh();
        }

        private void ShtoAfatMaturimiNeGrid(int idmaturimi)
        {
            if (gvAfateMaturimi.DataSource != null)
            {
                var dt = (DataTable)gvAfateMaturimi.DataSource;
                var drs = dt.Select("IdMaturimi = " + idmaturimi);
                if (drs.Length > 0)
                    throw new Exception(MessagesResource.Messages["msgBlerjeShitjeMaturimiEkziston"]);

                dt.ImportRow(clsMaturimi.MerrAfatMaturimiDr(idmaturimi));
            }
            else
                MbushListeMaturimesh();
        }

        private void HiqAfatMaturimNgaGrida(int idmaturimi)
        {
            if (gvAfateMaturimi.DataSource != null)
            {
                var dt = (DataTable)gvAfateMaturimi.DataSource;
                var drs = dt.Select("IdMaturimi = " + idmaturimi);

                if (drs.Length > 1)
                    throw new Exception(MessagesResource.Messages["msgBlerjeShitjeMaturimeIdNjejte"]);

                if (drs.Length == 0)
                    return;

                dt.Rows.Remove(drs[0]);
                gvAfateMaturimi.DataBind();
            }
            else
                MbushListeMaturimesh();
        }

        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
            menu_msg_Frame.RuajFilter(gvAfateMaturimi, Komponente, int.Parse(cmbKonfigurimi.Value.ToString()), ref hfStatusi);
        }

        private void MbushListeMaturimesh()
        {
            var dt = colMaturimet.MerrAfatMaturimiSipasNdermDt(IdNdermarrja);
            mySessionObjects.ruajGrideNeSession(Session, dt);
            gvAfateMaturimi.DataSource = dt;
            gvAfateMaturimi.DataBind();
            dt.Dispose();
        }

        private void MbushGridMaturimeshNgaSession()
        {
            var sukses = mySessionObjects.merrGrideNgaSessioni(Session, out DataTable tmpObject);
            if (!sukses)
                MbushListeMaturimesh();
            else
            {
                gvAfateMaturimi.DataSource = tmpObject;
                gvAfateMaturimi.DataBind();
                tmpObject.Dispose();
            }
        }

        private void KonfiguroGride()
        {
            KonfigurimComboGride.ShtoLlojiKlientFurnitor(gvAfateMaturimi, rm, ci, "LlojMaturimi");
            KonfigurimComboGride.ShtoDateFillimi(gvAfateMaturimi);
            KonfigurimComboGride.ShtoPeriudhe(gvAfateMaturimi);
            KonfigurimComboGride.ShtoAutorizim(gvAfateMaturimi, Session, Komponente, GuidString, "IdMaturimi");

            gvAfateMaturimi.Columns["#"].VisibleIndex = 0;
        }

        /// <summary>
        /// Handles the DataBound event of the gvAfateMaturimi control.
        /// Shton colonen # per selektim dhe disa karakteristika te grides
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void gvAfateMaturimi_DataBound(object sender, EventArgs e)
        {
            GridUtil.GridDataBound(sender, e, gvAfateMaturimi, "IdMaturimi");
        }

        protected void gvAfateMaturimi_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            GridUtil.GridAfterPerformCallback(sender, e, gvAfateMaturimi, _menu);
        }

        protected void gvAfateMaturimi_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            GridUtil.GridHeaderFilterFillItem(sender, e, rm, ci, "PershkrimMaturimi");
        }

        protected void gvAfateMaturimi_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            GridUtil.GridCustomCallbackDefault(sender, e, gvAfateMaturimi, Komponente, IdNdermarrja, IdGjuha, cmbKonfigurimi, ref hfStatusi);
            gvAfateMaturimi.Columns["#"].VisibleIndex = 0;
        } 

        #endregion
    }
}
