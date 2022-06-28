using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI.HtmlControls;
using AlphaWeb.Core.SharedKernel;
using DbCore;
using DbCore.DbAdmin;
using DbCore.DbImporte;
using DbCore.DbRegjistrim;
using DbCore.IMBUtils.Extensions;
using DevExpress.Web;
using NLog;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.Messages;
using Newtonsoft.Json;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;

namespace PlatinumWeb
{
    public partial class Import : MyPageBase
    {
        private const string Komponente = "Import.aspx";

        protected void Page_PreInit(object sender, EventArgs e)
        {
            base.Page_PreInit(sender, e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            ShtoMenuControlsDheMsgFrame();
            PercaktoTemplateMenu(_menu, _menuInfo);
            VendosPerkthimet();
            MbushHiddenFieldMePerkthime();
            if (!IsPostBack)
            {
                ShtoVleraTePergjithshmeNeHfState();
                if (!mySessionObjects.isLogedIn(Session))
                    clsFunksione.logout(Session, true, "FaqePaautorizuar");
                if (mySessionObjects.ktheKodNdermarrje(Session) == null)
                    Response.Redirect("Login_Ndermarrje.aspx?id=" + IdPerdoruesi);

                mySessionObjects.ruajTabeleGabimeshImporti(Session, new DataTable());
                KonfiguroVleraFillestareShto();
                var tedrejtaInfo = new clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, Komponente);
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                ucEmerSkedari.ValidationSettings.MaxFileSize = clsFunksione.merrMaxFileSizePerImport();
                var roli = colRolPerdorues.merrRoleSipasPerdoruesiDheKodRoli(IdPerdoruesi, "RSU");
                hfState.Set("superuser", roli == -1);
            }
        }


        private void MbushHiddenFieldMePerkthime()
        {
            hfState.Set("MsgSkaRreshtPerKontroll", MessagesResource.Messages["MsgSkaRreshtaGridaPerKontroll"]);
            hfState.Set("msgKontrolloGabimet", MessagesResource.Messages["msgKontrolliPerGabimet"]);
            hfState.Set("msgGabimGjateTransferimit", MessagesResource.Messages["msgGabimeGjateTransferimit"]);
            hfState.Set("msgPoTransferohetTeDhenatShtypniPerseriRuaj", MessagesResource.Messages["msgPoTransferohetTeDhenatShtypniPerseriRuaj"]);
            hfState.Set("msgShenoEmer", MessagesResource.Messages["msgShenoEmrin"]);
            hfState.Set("msgShenoKategori", MessagesResource.Messages["msgShenoKategori"]);
            hfState.Set("msgShenoFormatin", MessagesResource.Messages["msgShenoFormatin"]);
            hfState.Set("msgNukKeniTeDrejta", MessagesResource.Messages["msgNukKeniTeDrejta"]);
            hfState.Set("msgZgjidhFormatin", MessagesResource.Messages["msgZgjidhFormatin"]);
            hfState.Set("msgZgjidhSkedarin", MessagesResource.Messages["msgZgjidhSkedarin"]);
            hfState.Set("msgPlotesoniFushat", MessagesResource.Messages["msgPlotesoniFushat"]);
            hfState.Set("msgPlotesoniFushatETabelave", MessagesResource.Messages["msgPlotesoniFushatETabelave"]);
            hfState.Set("msgPlotesoniFushatETabelesKoke", MessagesResource.Messages["msgPlotesoniFushatETabelesKoke"]);
            hfState.Set("msgDeshironiTeMbishkruaniVlerat", MessagesResource.Messages["msgDeshironiTeMbishkruaniVlerat"]);
            hfState.Set("msgPlotesoniEmratETabelave", MessagesResource.Messages["msgPlotesoniEmratETabelave"]);
        }

        private void VendosPerkthimet()
        {
            lblEmer.Text = MessagesResource.Messages["labelRaportEmri"];
            lblTipi.Text = MessagesResource.Messages["labelFilterAvancuarTipi"];
            lblKategoria.Text = MessagesResource.Messages["labelKategoria"];
            lblFormati.Text = MessagesResource.Messages["labelFilterAvancuarFormati"];
            lblEmerSheet.Text = MessagesResource.Messages["labelEmriExcel"];
            cbPermbledhese.Text = MessagesResource.Messages["menuItemFaturaShitjeGjeneroFaturePermbledhese"];
            lblEmerSkedari.Text = MessagesResource.Messages["labelEmriISkedarit"];
            ucEmerSkedari.ValidationSettings.MaxFileSizeErrorText = MessagesResource.Messages["msgSkedarMbi10mb"];
            lblEmerTabKoka.Text = MessagesResource.Messages["lblEmriTabeleKoke"];
            lblEmerTabTrupi.Text = MessagesResource.Messages["lblEmriTabeleTrup"];
            lblEmerTabRec.Text = MessagesResource.Messages["lblEmriTabeleReceptura"];
            popFshi.HeaderText = MessagesResource.Messages["labelAdministrimiKujdes"];
            lblMsgbox.Text = MessagesResource.Messages["labelAdministrimiMsgJeniSigurt"];
            ButtonCancel.Text = MessagesResource.Messages["labelAnullo"];
            ButtonOk.Text = MessagesResource.Messages["labelOk"];
        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="menu"> menuja ne te cilat do te shtohen kontrollet</param>
        /// <param name="menuInfo"> menuja ne te cilat do te shtohen mesazhet</param>
        protected void PercaktoTemplateMenu(ASPxMenu menu, ASPxMenu menuInfo) =>
            clsToolbarConfig.percaktoTemplateMenu(IdGjuha, IdViti, IdPerdoruesi, IdNdermarrja, menu, Komponente, this, menuInfo, cmbEmer.SelectedIndex == -1, true, false, Meme);

        /// <summary>
        /// Vendos vlerat default kur po behet shtim
        /// </summary>
        /// <param name="IdNdermarrja"></param>
        private void KonfiguroVleraFillestareShto()
        {
            ConfigureAspxComboBox.mbushComboKonfigurimImporti(cmbEmer, IdNdermarrja, IdViti, IdPerdoruesi, Komponente);
            ConfigureAspxComboBox.KonfiguroComboBoxKategoriPerImport(cmbKategoria, false, IdNdermarrja, IdViti, IdPerdoruesi, Komponente);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmbFormati);
            _menu.Items.FindByName("Anullo").Text = MessagesResource.Messages["MenuItemLista"];
        }

        /// <summary>
        /// Percakton veprimin qe kryhet kur klikohet nje nga butonat e menuse
        /// </summary>
        protected void Menu_ItemClick(object source, MenuItemEventArgs e)
        {
            switch (e.Item.Name)
            {
                case "Ruaj":
                    Page.Validate();
                    RuajKonfigurim();
                    break;
            }
            hfMbishkruajVleraImporti.Value = bool.FalseString;
        }

        /// <summary>
        /// Ruan/Modifikon nje objekt clsKokaEkzekutim.
        /// </summary>
        /// <param name="statusDokumenti">Statusi me te cilin po ruhet dokumenti (I rregullt apo draft)</param>
        private void RuajKonfigurim()
        {
            
            if (Page.IsValid == false)
                return;
            var idSuperKategori = clsKategoriNivelDok.mbushIDSuperKategoriNivDok(int.Parse(cmbKategoria.Value.ToString()));
            try
            {
                var konfigImporti = KrijoKonfigurim(idSuperKategori);
                //if (konfigImporti.EmerTabKoka.StartsWith("T_") || konfigImporti.EmerTabKokaHistorik.StartsWith("T_") || konfigImporti.EmerTabTrupi.StartsWith("T_") || konfigImporti.EmerTabTrupiHistorik.StartsWith("T_") || konfigImporti.EmerTabRec.StartsWith("T_") || konfigImporti.EmerTabRecHistorik.StartsWith("T_"))
                //{
                //    clsMenuInfo.ShtoMesazhGabimi(_menuInfo, "Tabelat nuk duhet te fillojne me T_ !", _pnlMesazhi);
                //    status1.Value = "false";
                //    return;
                //}
                var tedrejtaInfo = new clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, Komponente);

                if (cmbEmer.SelectedIndex == -1)
                {
                    if (!tedrejtaInfo.DShtim)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(_menuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], _pnlMesazhi);
                        status1.Value = "false";
                        return;
                    }

                    konfigImporti.Ruaj(idSuperKategori);
                }
                else
                {
                    if (!tedrejtaInfo.DMod)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(_menuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], _pnlMesazhi);
                        status1.Value = "false";
                        return;
                    }

                    konfigImporti.Id = int.Parse(cmbEmer.Value.ToString());
                    var oldKonfig = new clsKonfigImporti(int.Parse(cmbEmer.Value.ToString()));

                    konfigImporti.Modifiko(oldKonfig, idSuperKategori);
                }

                tabHistoriku.Value = JsonConvert.SerializeObject(new { EmerTabKokaHistorik = konfigImporti.EmerTabKokaHistorik, EmerTabTrupiHistorik = konfigImporti.EmerTabTrupiHistorik, EmerTabRecHistorik = konfigImporti.EmerTabRecHistorik });

            }
            catch (MyException mesazhi)
            {
                LogManager.GetCurrentClassLogger().Error(mesazhi.Message);
                clsMenuInfo.ShtoMesazhGabimi(_menuInfo, mesazhi.Message, _pnlMesazhi, LoadingPanel);
                status1.Value = "false";
                return;
            }
            catch (Exception err)
            {
                LogManager.GetCurrentClassLogger().Error(err.Message);
                clsMenuInfo.ShtoMesazhGabimi(_menuInfo, MessagesResource.Messages["msgGabimRuajtje"], _pnlMesazhi, LoadingPanel);
                status1.Value = "false";
                return;
            }

            clsMenuInfo.ShtoMesazhSuksesi(_menuInfo, MessagesResource.Messages["mesazhRuajtjeMeSukses"], _pnlMesazhi);
            ConfigureAspxComboBox.mbushComboKonfigurimImporti(cmbEmer, IdNdermarrja, IdViti, IdPerdoruesi, Komponente);
            cmbEmer_pnlEmer.Update();
            hl = new HtmlTable();
            pnlLidhur.Update();
            status1.Value = "false";
            hfShtimModifikim.Value = "shtim";
        }

        /// <summary>
        /// Krijon nje objekt te tipit clsKokaEkzekutim 
        /// </summary>
        /// <param name="statusDokumenti">Statusi i dokumentit (I rregullt ose draft)</param>
        /// <returns>Kthen nje objekt te tipit DbCore.DbRegjistrim.clsKokaMagazina</returns>
        private clsKonfigImporti KrijoKonfigurim(int idSuperKategori)
        {
            int idkategori = 0, idformati = 0;
            if (cmbKategoria.Text != "")
                idkategori = int.Parse(cmbKategoria.Value.ToString());
            if (cmbFormati.Text != "")
            {
                int.TryParse(cmbFormati.Value.ToString(), out idformati);
                if (idformati == 0)
                    throw new MyException(MessagesResource.Messages["msgImportKyFormatNukEkziston"]);
                var format = new clsKokaFormatImporti(idformati);
                if (format.IdKoka == 0)
                    throw new MyException(MessagesResource.Messages["msgImportKyFormatNukEkziston"]);
            }

            if (rbTipi.Value.ToString() == "SQL")
            {
                txtEmerTabKoka.Text = txtEmerTabKoka.Text.Replace(" ", "");
                var mesazh = ci.Name == "sq-AL"
                    ? clsFunksione.kontrolloPerKaraktereSpeciale(txtEmerTabKoka.Text)
                    : clsFunksione.kontrolloPerKaraktereSpecialeEng(txtEmerTabKoka.Text);
                if (!mesazh.Status)
                {
                    mesazh.PershkrimMesazhi = MessagesResource.Messages["tabelaEKokes"] + mesazh.PershkrimMesazhi;
                    throw new MyException(mesazh.PershkrimMesazhi);
                }
                if (idSuperKategori == (int)SuperKategori.Regjistrime)
                {
                    txtEmerTabTrupi.Text = txtEmerTabTrupi.Text.Replace(" ", "");
                    txtEmerTabRec.Text = txtEmerTabRec.Text.Replace(" ", "");
                    mesazh = ci.Name == "sq-AL"
                        ? clsFunksione.kontrolloPerKaraktereSpeciale(txtEmerTabTrupi.Text)
                        : clsFunksione.kontrolloPerKaraktereSpecialeEng(txtEmerTabTrupi.Text);
                    if (!mesazh.Status)
                    {
                        if (cmbKategoria.Value.ToString() == "45")
                            mesazh.PershkrimMesazhi = MessagesResource.Messages["tabelaEProdukteve"] + mesazh.PershkrimMesazhi;
                        else
                            mesazh.PershkrimMesazhi = MessagesResource.Messages["tabelaETrupit"] + mesazh.PershkrimMesazhi;
                        throw new MyException(mesazh.PershkrimMesazhi);
                    }
                    if (cmbKategoria.Value.ToString() == "45")
                    {
                        mesazh = ci.Name == "sq-AL"
                            ? clsFunksione.kontrolloPerKaraktereSpeciale(txtEmerTabRec.Text)
                            : clsFunksione.kontrolloPerKaraktereSpecialeEng(txtEmerTabRec.Text);
                        if (!mesazh.Status)
                        {
                            mesazh.PershkrimMesazhi = MessagesResource.Messages["tabelaERecepturave"] + mesazh.PershkrimMesazhi;
                            throw new MyException(mesazh.PershkrimMesazhi);
                        }
                    }
                }
            }

            return new clsKonfigImporti(cmbEmer.Text, rbTipi.Value.ToString(), idkategori, idformati, txtEmerSheet.Text, IdNdermarrja, IdPerdoruesi, 1, txtEmerTabKoka.Text, txtEmerTabTrupi.Text, cbPermbledhese.Checked, txtEmerTabRec.Text, cbTransferoFatura.Checked, cbTePaImportuara.Checked, cbDergoMeEmail.Checked, txtEmerTabKokaHistorik.Text, txtEmerTabTrupiHistorik.Text, txtEmerTabRecHistorik.Text, cbRimerrTeImportuara.Checked, (int?)Convert.ToInt32(txtNrDokumentash.Value));
        }

        protected void ucEmerSkedari_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            try
            {
                var f = ucEmerSkedari.UploadedFiles[0];
                mySessionObjects.ruajFileUpload(Session, f.FileName, f.FileContent);
                var pathDir = HttpContext.Current.Server.MapPath(null) + @"\Import\";
                DirectoryExtension.CreateDirIfNotExists(pathDir);
                if (f.FileName.EndsWith(".xls") || f.FileName.EndsWith(".xlsx")) //perdoret kur kemi OleDbConnection
                    f.SaveAs(pathDir + f.FileName);
            }
            catch (Exception err)
            {
                LogManager.GetCurrentClassLogger().Error(err.Message);
                clsMenuInfo.ShtoMesazhGabimi(_menuInfo, MessagesResource.Messages["msgGabimUploadim"], _pnlMesazhi);
            }
        }

        /// <summary>
        /// fshin dokumentin
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            if (cmbEmer.SelectedIndex == -1 || cmbEmer.Text == "")
            {
                clsMenuInfo.ShtoMesazhGabimi(_menuInfo, MessagesResource.Messages["msgNukKeniZgjedhurKonfigurimImporti"], _pnlMesazhi);
                return;
            }

            try
            {
                clsKonfigImporti.Fshi(int.Parse(cmbEmer.Value.ToString()), IdPerdoruesi);
            }
            catch (Exception ex)
            {
                LogManager.GetCurrentClassLogger().Error(ex.Message);
                clsMenuInfo.ShtoMesazhGabimi(_menuInfo, "Nje gabim i papritur ka ndodhur!", _pnlMesazhi);
                status1.Value = "false";
            }

            clsMenuInfo.ShtoMesazhSuksesi(_menuInfo, MessagesResource.Messages["msgFshirjeMeSukses"], _pnlMesazhi);
            status1.Value = "true";
            cmbEmer.SelectedIndex = -1;
            ConfigureAspxComboBox.mbushComboKonfigurimImporti(cmbEmer, IdNdermarrja, IdViti, IdPerdoruesi, Komponente);
            cmbEmer_pnlEmer.Update();
        }

        protected void cmbFormati_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("cmbFormati"))
            {
                ConfigureAspxComboBox.mbushComboFormat(cmbFormati, IdNdermarrja, Convert.ToInt32(cmbKategoria.Value), e);
            }
        }

        protected void cmbFormati_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("cmbFormati"))
            {
                ConfigureAspxComboBox.mbushComboFormat(cmbFormati, IdNdermarrja, Convert.ToInt32(cmbKategoria.Value), e);
            }
        }
    }
}