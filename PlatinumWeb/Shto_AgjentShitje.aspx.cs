using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using DevExpress.Web;
using PlatinumWeb.Templates;
using System.Data;
using System.Globalization;
using System.Resources;
using DbCore;
using DbCore.DbAdmin;
using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.Types;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class Shto_AgjentShitje : MyPageBase
    {
        private const string komponenteEmri = "Shto_AgjentShitje.aspx";
        private const int idKomponente = 149;
        private int idviti, idgjuha, idPerdoruesi, idNdermarrje, idKonfig;
        private string guidString;




        /// <summary>
        /// Metoda qe thirret sa here qe ngarkohet faqja
        /// </summary>
        /// <param name="sender">Derguesi</param>
        /// <param name="e">Argumentat</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            var rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                DbCore.clsFunksione.logout(Session, true, "FaqePaautorizuar");
            }
            idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            idviti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            idgjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            var cultinf = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo(idgjuha);
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerdoruesi);
            }
            percaktoTemplateMenu(idgjuha, idviti, idPerdoruesi, idNdermarrje, ASPxMenu1);

            if (!Page.IsPostBack)
            {
                EmrateTabeve(rm, cultinf);
                mbushHiddenFieldMePerkthime(cultinf, rm);
                hfState.Set("idGjuha", idgjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idPerdoruesi", idPerdoruesi);
                guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                hfState.Set("guidString", guidString);
                konfiguroVleraFillestare(idPerdoruesi, idNdermarrje, rm, cultinf, idgjuha);
                mbushListeAgjentesh(idNdermarrje, idPerdoruesi);
                hfState.Set("colAutorizime", Newtonsoft.Json.JsonConvert.SerializeObject(new colAutorizimetKoka(IdPerdoruesi)));
                var tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idviti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idviti, komponenteEmri);
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                konfiguroGride(idNdermarrje, cmbKonfigurimi.Text.Split(';')[0]);
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "grid_AgjenteShitje", grid_AgjenteShitje, cmbKonfigurimi.Text.Split(';')[0], "149", DbCore.mySessionObjects.ktheGjuhe(Session));

            }
            else
            {
                guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                mbushListAgjenteshNgaSesioni();
                konfiguroGride(idNdermarrje, cmbKonfigurimi.Text.Split(';')[0]);
            }
            idKonfig = int.Parse(cmbKonfigurimi.Value.ToString());
            grid_AgjenteShitje.PercaktoTitlePanel(this, MenuInfo, pnlMesazhi, hfState, idPerdoruesi, idNdermarrje, idviti, idgjuha, idKonfig, komponenteEmri, rm, cultinf);
            clsToolbarConfig.mbushComboBoxFiltra(idgjuha, idNdermarrje, "grid_AgjenteShitje", idKonfig, komponenteEmri);
            AspxWebControlUtils.perkthePopUp(popFshi, rm.GetString("labelKujdes", cultinf), lblMsgbox, rm.GetString("labelAdministrimiMsgJeniSigurt", cultinf), ButtonCancel, rm.GetString("labelAnullo", cultinf));
        }
        
        private void mbushKomboPerdoruesish( int idNdermarrje)
        {
            DataTable dt = DbCore.DbAdmin.colPerdoruesit.merrPerdoruesitSipasLicencesDTPerNdermaje(idNdermarrje);
            cmbDrejtori.DataSource = dt;
            cmbDrejtori.TextField = "PerdoruesUsername";
            cmbDrejtori.ValueField = "IdPerdorues";
            cmbDrejtori.DataBind();
            btnPerdoruesi.DataSource = dt;
            btnPerdoruesi.TextField = "PerdoruesUsername";
            btnPerdoruesi.ValueField = "IdPerdorues";
            btnPerdoruesi.DataBind();
        }

        /// <summary>
        /// Vendos emrat e tabeve ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateTabeve(ResourceManager rm, CultureInfo cultinf)
        {
            ASPxPageControl1.TabPages[0].Text = rm.GetString("labelBlerjeShitjeTePergjithshme", cultinf);
            ASPxPageControl1.TabPages[1].Text = rm.GetString("agjentShitjeTab", cultinf);
        }

        /// <summary>
        /// Metode qe sherben per te kaluar perkthime te js
        /// </summary>
        /// <param name="cultinf">Merr culture info</param>
        /// <param name="rm">Merr Resource Manager</param>
        private void mbushHiddenFieldMePerkthime(CultureInfo cultinf, ResourceManager rm)
        {
            hfState.Set("msgZgjidhniAutorizimet", rm.GetString("msgZgjidhniAutorizimet", cultinf));
            hfState.Set("popupAdministrimiUniversal", rm.GetString("popupAdministrimiUniversal", cultinf));
            hfState.Set("MsgBlerjeShitjeAfatMaturimi", rm.GetString("MsgBlerjeShitjeAfatMaturimi", cultinf));
            hfState.Set("msgCeljeArkaBankaLlogariaNukEkziston", rm.GetString("msgCeljeArkaBankaLlogariaNukEkziston", cultinf));
            hfState.Set("msgZgjidhniPerdoruesin", rm.GetString("msgZgjidhniPerdoruesin", cultinf));
            hfState.Set("msgZgjidhniDrejtorin", rm.GetString("msgZgjidhniDrejtorin", cultinf));

        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="idNderVit"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        private void percaktoTemplateMenu(int idGjuha, int idViti, int idPerdorues, int idNdermarrje, ASPxMenu aSPxMenu1)
        {
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, komponenteEmri, this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, hfShtimModifikim.Value == "modifikim" ? false : true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
        }
        /// <summary>
        /// Handleri per butonin ruaj tek filtri ne menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
            var itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            var cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            var idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);

            var filtri = new DbCore.DbAdmin.clsFiltraGrida();
            filtri.FiltraKodi = cmbFiltra.Text;
            filtri.FiltraShenime = cmbFiltra.Text;
            filtri.FiltraUniversal = false;
            var koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "grid_AgjenteShitje", komponenteEmri, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = grid_AgjenteShitje.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("Kodi", grid_AgjenteShitje);
            //var kolona = grid_AgjenteShitje.GetSortedColumns();
            //if (kolona.Count > 0)
            //{
            //    filtri.KoloneRenditje = kolona[0].FieldName;
            //    if (kolona[0].SortOrder == DevExpress.Data.ColumnSortOrder.Ascending)
            //    {
            //        filtri.DrejtimRenditje = true;
            //    }
            //    else
            //    {
            //        filtri.DrejtimRenditje = false;
            //    }
            //}
            //else
            //{
            //    filtri.KoloneRenditje = "Kodi";
            //    filtri.DrejtimRenditje = true;
            //}


            filtri.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            filtri.IdNdermarje = idNdermarrje;
            var mesazh = new DbCore.clsMesazh();
            filtri.IdStatusDok = 1;

            mesazh = filtri.ruaj();

            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "grid_AgjenteShitje", int.Parse(cmbKonfigurimi.Value.ToString()), komponenteEmri);
            percaktoTemplateMenu(idgjuha, idviti, idPerdoruesi, idNdermarrje, ASPxMenu1);

            if (mesazh.Status == true)
            {
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            }
            else
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            }
            cmbFiltra.Text = string.Empty;
        }

        /// <summary>
        /// Handleri per butonin fshi tek filtri ne menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
            var itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            var cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            var idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);

            var filtra = new DbCore.DbAdmin.clsFiltraGrida();

            var koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "grid_AgjenteShitje", komponenteEmri, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);

            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                var mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();

                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "grid_AgjenteShitje", int.Parse(cmbKonfigurimi.Value.ToString()), komponenteEmri);
                percaktoTemplateMenu(idgjuha, idviti, idPerdoruesi, idNdermarrje, ASPxMenu1);

                if (mesazh.Status == true)
                {
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                }
                else
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                }
                cmbFiltra.Text = string.Empty;


                grid_AgjenteShitje.FilterExpression = String.Empty;
            }
        }


        /// <summary>
        /// pasi behet bound menuja
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(idgjuha, idviti, idPerdoruesi, idNdermarrje, ASPxMenu1);
        }

        /// <summary>
        /// perdoret per te trajtuar ngjarjet e butonave te menuse. 
        /// </summary>
        /// <param name="source"> derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {
            if (e.Item.Name == "Ruaj")
            {
                Page.Validate("entries");
                ruajAgjentShitje();
            }
        }

        /// <summary>
        /// metoda qe ben ruajtjen e agjentit
        /// </summary>
        private void ruajAgjentShitje()
        {
            var cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            var rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (Page.IsValid == false)
            {
                return;
            }
            var idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            var idPerdorues = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            var mesazh = new DbCore.clsMesazh();
            DbCore.DbAdmin.clsAgjentShitje agjenti;
            try
            {
                if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
                {
                    agjenti = krijoAgjentShitje(true, idNdermarrje, idPerdorues);
                }
                else
                {
                    agjenti = krijoAgjentShitje(false, idNdermarrje, idPerdorues);
                }
            }
            catch (DbCore.MyException myEx)
            {
                ImbLogger.Error(myEx.Message);
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, myEx.Message, pnlMesazhi);
                hfStatusi.Value = "false";
                return;
            }
            catch (Exception e)
            {
                ImbLogger.Error(e.Message);
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, e.Message, pnlMesazhi);
                hfStatusi.Value = "false";
                return;
            }
            bool eshteShtim;
            var tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdorues, idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), komponenteEmri);

            if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
            {
                if (!tedrejtaInfo.DShtim)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("mesazhRaportNukKeniTeDrejta", cultinf), pnlMesazhi);
                    hfStatusi.Value = "false";
                    return;
                }
                mesazh = agjenti.ruaj();
                eshteShtim = true;
            }
            else
            {
                if (!tedrejtaInfo.DMod)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("mesazhRaportNukKeniTeDrejta", cultinf), pnlMesazhi);
                    hfStatusi.Value = "false";
                    return;
                }
                var dbRegjistrim = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
                var konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                konf.IdKonfigAmbjente = agjenti.IdKonfig;
                konf.mbushKonfigAmbjSipasId(konf.IdKonfigAmbjente);
                agjenti.IdAgjentShitje = int.Parse(hfId.Value.ToString());

                var lidhur = dbRegjistrim.eshteDokumentiILidhurCelje(agjenti.IdKonfig.ToString(), konf.IdNivel.ToString());



                mesazh = agjenti.modifiko();

                eshteShtim = false;
                dbRegjistrim.Dispose();
            }

            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                hfStatusi.Value = "false";
            }
            else
            {
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("labelRaportMesazhRuajtjaPerfundoiSukses", cultinf), pnlMesazhi);
                if (eshteShtim)
                {
                    shtoAgjentShitjeNeGrid(idNdermarrje, agjenti.IdAgjentShitje);
                }
                else
                {
                    modifikoAgjentShitjeNeGrid(idNdermarrje, agjenti.IdAgjentShitje);
                    ASPxPageControl1.ActiveTabIndex = 0;
                }
                hfStatusi.Value = "true";
                konfiguroGride(idNdermarrje, cmbKonfigurimi.Text.Split(';')[0]);
            }
            pnlMesazhi.Update();
        }

        /// <summary>
        /// shton agjentin ne gride
        /// </summary>
        /// <param name="idNdermarrje">Id e ndermarrjes</param>
        /// <param name="idAgjenti">Id e agjentit te shtuar</param>
        private void shtoAgjentShitjeNeGrid(int idNdermarrje, int idAgjenti)
        {
            var cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            var rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (grid_AgjenteShitje.DataSource != null)
            {
                var dt = (DataTable)grid_AgjenteShitje.DataSource;
                var drs = dt.Select("IdAgjentShitje = " + idAgjenti);
                if (drs.Length > 0)
                {
                    throw new Exception(rm.GetString("msgAgjentiEkzistonNeGride", cultinf));
                }
                var Agjent = new DbCore.DbAdmin.clsAgjentShitje();
                var dr = Agjent.ktheAgjentShitjeSipasID(idAgjenti);
                dt.ImportRow(dr);
            }
            else
            {
                mbushListeAgjentesh(idNdermarrje, idPerdoruesi);
            }
        }

        /// <summary>
        /// modifikon agjentin ne gride pas modifikimeve te perdoruesit
        /// </summary>
        /// <param name="idNdermarrje">Id e ndermarrjes</param>
        /// <param name="idAgjenti">Id e agjentit te modifikuar</param>
        private void modifikoAgjentShitjeNeGrid(int idNdermarrje, int idAgjenti)
        {
            var cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            var rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (grid_AgjenteShitje.DataSource != null)
            {
                var dt = (DataTable)grid_AgjenteShitje.DataSource;
                var drs = dt.Select("IdAgjentShitje = " + idAgjenti);
                if (drs.Length > 1)
                {
                    throw new Exception(rm.GetString("msgGabimNdodhen2AgjenteMeTeNjejtenId", cultinf));
                }
                if (drs.Length == 0)
                {
                    return;
                }
                var dr = drs[0];
                var Agjent = new DbCore.DbAdmin.clsAgjentShitje();

                var newDr = DbCore.DbAdmin.colAgjenteShitje.merrSipasAgjenteveNdermarrjesAndAutorizimeDR(idNdermarrje, idPerdoruesi, idAgjenti);
                if (newDr != null)
                {
                    var arr = newDr.ItemArray;
                    dr.ItemArray = arr;
                }
            }
            else
            {
                mbushListeAgjentesh(idNdermarrje, idPerdoruesi);
            }
        }

        /// <summary>
        /// Metoda qe krijon agjentin si objekt. Kontrollet per vlerat e dhena behen te metoda kontrolloAgjent te ClsAgjentShitje
        /// </summary>
        /// <param name="shtim">Variabel qe tregon nese po shtojme apo po modifikojme. true per shtim, false per modifikim</param>
        /// <param name="idNdermarrje">Id e ndermarrjes</param>
        /// <param name="idPerdorues">Id e perdoruesit</param>
        /// <returns></returns>
        private DbCore.DbAdmin.clsAgjentShitje krijoAgjentShitje(bool shtim, int idNdermarrje, int idPerdorues)
        {
            var idAgjentShitje = 0;
            int.TryParse(hfId.Value, out idAgjentShitje);
            var kodi = DbCore.clsFunksione.ktheStringunPaHapesira(kodiTextBox.Text, true);
            var cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            var rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            var emri = DbCore.clsFunksione.ktheStringunPaHapesira(emriTextBox.Text, false);
            var mbiemri = mbiemriTextBox.Text;
            var tel = telTextBox.Text;
            var fax = faxTextBox.Text;
            var email = emailTextBox.Text;
            var idperdoruesmobile = 0;
            if (btnPerdoruesi.Text != string.Empty)
                idperdoruesmobile = DbCore.DbAdmin.clsPerdorues.ktheIdPerdoruesSipasUsername(btnPerdoruesi.Text, idNdermarrje);
            double perqindja = 0;
            DbCore.DbAdmin.colLidhjetAutorizim colLidhje;
            if (cmbAutorizimiHf.Value == string.Empty)
            {
                colLidhje = new DbCore.DbAdmin.colLidhjetAutorizim();
            }
            else
            {
                var colLidhjet = new DbCore.DbAdmin.colLidhjetAutorizim();
                var pars11 = cmbAutorizimiHf.Value.Split(',');
                for (var i = 0; i < pars11.Length; i++)
                {
                    var lidhje = new DbCore.DbAdmin.clsLidhjeAutorizim();
                    lidhje.IdAutorizimeKoka = DbCore.DbAdmin.clsAutorizimKoka.ktheIDAutorizim(pars11[i]);
                    colLidhjet.Add(lidhje);
                }
                colLidhje = colLidhjet;
            }
            if (perqindjeASPxTextBox.Text != string.Empty)
            {
                try
                {
                    perqindja = double.Parse(perqindjeASPxTextBox.Text);
                    if (perqindja < 0 || perqindja > 100)
                    {
                        throw new DbCore.MyException(rm.GetString("msgPerqindjaDuhetNumerNga0ne100", cultinf));
                    }
                }
                catch (Exception err)
                {
                    ImbLogger.Error(err.Message);
                    throw new DbCore.MyException(rm.GetString("msgPerqindjaDuhetNumerNga0ne100", cultinf));
                }
            }


            var idQyteti = 0;
            if (qytetiASPxComboBox.Text != string.Empty)
            {
                try
                {
                    idQyteti = Convert.ToInt32(qytetiASPxComboBox.SelectedItem.Value);
                }
                catch (Exception err)
                {
                    ImbLogger.Error(err.Message);
                    throw new DbCore.MyException(rm.GetString("msgQytetiZgjedhurNukEshteIVlefshem", cultinf));
                }
            }
            var idLlogari = 0;
            if (txtLlogari.Text != string.Empty)
            {
                if (txtLlogari.SelectedItem != null || txtLlogari.SelectedIndex != -1)
                {
                    idLlogari = Convert.ToInt32(txtLlogari.SelectedItem.Value);
                }
                else
                {
                    if (DbCore.DbKontabiliteti.clsLlogari.ekzistonLlogari(txtLlogari.Text, idNdermarrje))
                    {
                        var llog = new DbCore.DbKontabiliteti.clsLlogari(txtLlogari.Text, idNdermarrje);
                        if (llog.Aktiv)
                        {
                            idLlogari = llog.IdLlogari;
                        }
                        else
                        {
                            throw new DbCore.MyException(rm.GetString("msgLlogariaNukEshteAktive", cultinf));
                        }
                    }
                    else
                    {
                        throw new DbCore.MyException(rm.GetString("msgCeljeArkaBankaLlogariaNukEkziston", cultinf));
                    }
                }



            }

            var agjentiRi = new DbCore.DbAdmin.clsAgjentShitje(idAgjentShitje,kodi, emri, mbiemri, tel, fax, email, idQyteti, perqindja, idLlogari, idNdermarrje, cmbKonfigurimi.Value.ToString() != string.Empty ? Convert.ToInt32(cmbKonfigurimi.Value) : 0, idPerdorues, shtim, colLidhje, idperdoruesmobile, Converter.ConvertToInt(cmbDrejtori.Value), rm, cultinf);
            return agjentiRi;
        }

        /// <summary>
        /// perdoret per te fshire rreshtat e zgjedhur te agjentit te shitjes nqs perdoruesi konfirmon fshirjen
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            var cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            var rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            List<object> rreshtat;
            if (ASPxPageControl1.ActiveTabIndex == 0)
            {
                rreshtat = grid_AgjenteShitje.GetSelectedFieldValues("IdAgjentShitje");
            }
            else
            {
                rreshtat = new List<object>();
                rreshtat.Add(hfId.Value);
            }

            if (rreshtat.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgZgjidhniTePaktenNjeAgjent", cultinf), pnlMesazhi);
                return;
            }
            var mesazh = new DbCore.clsMesazh();

            var TeFshire = new List<string>();
            var TePaFshire = new List<string>();
            var dbRegjistrim = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
            var konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            foreach (object id in rreshtat)
            {
                var clsAgjent = new DbCore.DbAdmin.clsAgjentShitje(Convert.ToInt32(id));
                konf.IdKonfigAmbjente = clsAgjent.IdKonfig;
                konf.mbushKonfigAmbjSipasId(konf.IdKonfigAmbjente);

                var lidhur = dbRegjistrim.eshteDokumentiILidhurCelje(clsAgjent.IdAgjentShitje.ToString(), konf.IdNivel.ToString());
                if (lidhur)
                {
                    TePaFshire.Add(clsAgjent.KodiAgjentShitje);
                    continue;
                }

                mesazh = clsAgjent.fshistatus();
                if (clsAgjent.IdAgjentShitje == 0)
                {
                    continue;
                }
                if (mesazh.Status)
                {
                    hiqAgjentShitjeNgaGrida(clsAgjent.IdAgjentShitje);
                    TeFshire.Add(clsAgjent.KodiAgjentShitje);

                    ASPxPageControl1.ActiveTabIndex = 0;
                    hfStatusi.Value = "true";
                }
            }
            dbRegjistrim.Dispose();
            var mesazhInfoGabim = string.Empty;
            var mesazhInfoSukses = string.Empty;
            if (TePaFshire.Count == 1)
            {
                mesazhInfoGabim = String.Format("{0}{1}{2}", rm.GetString("msgAgjentiMeKod", cultinf), String.Join(";", TePaFshire), rm.GetString("msgBlerjeShitjeNukFshihetNjejes", cultinf));
            }
            else
            {
                if (TePaFshire.Count > 1)
                {
                    mesazhInfoGabim = String.Format("{0}{1}{2}", rm.GetString("msgAgjentetMeKod", cultinf), String.Join(";", TePaFshire), rm.GetString("msgBlerjeShitjeNukFshihetShumes", cultinf));
                }
            }
            if (TeFshire.Count == 1)
            {
                mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("msgAgjentiMeKod", cultinf), String.Join(";", TeFshire), rm.GetString("msgCeljeMagazinatSuffixNjejesSuksesi", cultinf));
            }
            else
            {
                if (TeFshire.Count > 1)
                {
                    mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("msgAgjentetMeKod", cultinf), String.Join(";", TeFshire), rm.GetString("msgCeljeMagazinatSuffixShumesSuksesi", cultinf));
                }
            }
            if (mesazhInfoGabim != string.Empty && mesazhInfoSukses != string.Empty)
            {
                mesazhInfoGabim += rm.GetString("msgLidhesMesazhi", cultinf) + mesazhInfoSukses;
            }
            if (mesazhInfoGabim != string.Empty)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhInfoGabim, pnlMesazhi);
            }
            else
            {
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazhInfoSukses, pnlMesazhi);
            }
            pnlMesazhi.Update();
        }

        /// <summary>
        /// heq agjentin e fshire nga grida
        /// </summary>
        /// <param name="idAgjenti">Id e agjentit te fshire</param>
        private void hiqAgjentShitjeNgaGrida(int idAgjenti)
        {
            var cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            var rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (this.grid_AgjenteShitje.DataSource != null)
            {
                var dt = (DataTable)grid_AgjenteShitje.DataSource;
                var drs = dt.Select("IdAgjentShitje = " + idAgjenti);
                if (drs.Length > 1)
                {
                    throw new Exception(rm.GetString("msgGabimNdodhen2AgjenteMeTeNjejtenId", cultinf));
                }
                if (drs.Length == 0)
                {
                    return;
                }
                var dr = drs[0];
                dt.Rows.Remove(dr);
                grid_AgjenteShitje.DataBind();
            }
            else
            {
                mbushListeAgjentesh(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            }
        }

        /// <summary>
        /// metoda qe ben te gjitha konfigurimet fillestare ne faqe
        /// </summary>
        /// <param name="idPerdoruesi">Id e perdoruesit</param>
        /// <param name="idNdermarrje">Id e ndermarrjes</param>
        private void konfiguroVleraFillestare(int idPerdoruesi, int idNdermarrje, ResourceManager rm, CultureInfo cultinf, int idGjuha)
        {
            ASPxPageControl1.ActiveTabIndex = 0;
            ConfigureAspxComboBox.KonfiguroComboBoxComboQytete(IdNdermarrja, qytetiASPxComboBox);
            ConfigureAspxComboBox.mbushComboLlogaria(idNdermarrje, idPerdoruesi, txtLlogari);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(txtLlogari, cmbDrejtori,btnPerdoruesi);
            ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 19, rm, cultinf, idGjuha);
            mbushKomboPerdoruesish(idNdermarrje);

            cmbKonfigurimi.SelectedIndex = 0;
            var konf = new DbCore.DbShare.clsKonfigurimAmbjenti();

            konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()), idgjuha);
            hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
        }

        /// <summary>
        /// perdoret per te shtuar kolonen e selektimit tek grida dhe per te vendosur disa karakteristika te grides
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void grid_AgjenteShitje_DataBound(object sender, EventArgs e)
        {
            if (grid_AgjenteShitje.Columns["#"] == null)
            {
                var check = new DevExpress.Web.GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true;
                check.SetColVisibleIndex(0);
                check.Width = Unit.Percentage(2);

                grid_AgjenteShitje.Settings.ShowFilterRow = true;
                grid_AgjenteShitje.Columns.Add(check);
                grid_AgjenteShitje.KeyFieldName = "IdAgjentShitje";
                grid_AgjenteShitje.SettingsBehavior.AllowSelectByRowClick = true;
                grid_AgjenteShitje.SettingsBehavior.AllowFocusedRow = true;
                grid_AgjenteShitje.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                grid_AgjenteShitje.Settings.ShowFilterRowMenu = true;
            }
        }

        /// <summary>
        /// Metoda qe mbush data source-in e grides me te gjithe agjentet per ndermarrjen perkatese. Gjithashtu ruan griden ne sesion.
        /// </summary>
        /// <param name="idNdermarrje">Id e ndermarrjes</param>
        private void mbushListeAgjentesh(int idNdermarrje, int idPerdoruesi)
        {
            var dt = DbCore.DbAdmin.colAgjenteShitje.merrAgjenteShitjeSipasAutorizimeDt(idNdermarrje, idPerdoruesi);
            DbCore.mySessionObjects.ruajGrideNeSession(Session, dt);
            grid_AgjenteShitje.DataSource = dt;
            grid_AgjenteShitje.DataBind();
            dt.Dispose();
        }

        /// <summary>
        /// Metoda qe mbush data source-in e grides nga sesioni me te gjithe agjentet per ndermarrjen perkatese.
        /// </summary>
        private void mbushListAgjenteshNgaSesioni()
        {
            DataTable tmpObject;
            var sukses = DbCore.mySessionObjects.merrGrideNgaSessioni(Session, out tmpObject);
            if (!sukses)
            {
                mbushListeAgjentesh(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            }
            else
            {
                grid_AgjenteShitje.DataSource = tmpObject;
                grid_AgjenteShitje.DataBind();
                tmpObject.Dispose();
            }
        }

        /// <summary>
        /// Metoda qe ben konfigurimet fillestare te grides.
        /// </summary>
        /// <param name="idNdermarrje">Id e ndermarrjes</param>
        /// <param name="kodKonfigurimi">Kod i konfigurimit</param>
        private void konfiguroGride(int idNdermarrje, string kodKonfigurimi)
        {
            KonfigurimComboGride.ShtoQytetet(grid_AgjenteShitje, idNdermarrje, Session, komponenteEmri, guidString, "IdQyteti");
            KonfigurimComboGride.ShtoLlogariSipasNdermarrjes(grid_AgjenteShitje, idNdermarrje, Session, komponenteEmri, guidString, false);
            grid_AgjenteShitje.ShtoPerdorues(idNdermarrje, Session, komponenteEmri, guidString, "IdDrejtori");
         
            GridUtil.konfigGrideListeEMadhePaTheme(grid_AgjenteShitje, "IdAgjentShitje");
            grid_AgjenteShitje.Columns["#"].Width = 20;
            grid_AgjenteShitje.Columns["#"].VisibleIndex = 0;
            var kol = grid_AgjenteShitje.Columns["PerqindjeAgjentShitje"] as GridViewDataTextColumn;
            kol.PropertiesTextEdit.DisplayFormatString = "0.###";
        }

        protected void cmbDrejtori_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {

        }

        protected void txtLlogari_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("txtLlogari"))
            {
                ConfigureAspxComboBox.mbushComboLlogariaPaKolona(IdPerdoruesi, IdNdermarrja, txtLlogari, e);
            }
        }

        protected void txtLlogari_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("txtLlogari"))
            {
                ConfigureAspxComboBox.mbushComboLlogariaPaKolona(IdPerdoruesi, IdNdermarrja, txtLlogari, e);
            }
        }

        /// <summary>
        /// Perdoret per te shfaqur te gjithe qytetet kur perdoruesi zgjedh
        /// </summary>
        /// <param name="sender">Derguesi</param>
        /// <param name="e">Argumentat</param>
        protected void grid_AgjenteShitje_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if (e.Column.FieldName == "IdQyteti")
            {
                try
                {
                    var vlera = Converter.ConvertToInt(e.Value);
                    if (vlera == -3 || vlera == 0)
                    {
                        e.Criteria = null;
                    }
                }
                catch (Exception err)
                {
                    ImbLogger.Error(err.Message);
                    e.Criteria = null;
                }
            }
            else
            {
                if (e.Column.FieldName == "IdLlogari")
                {
                    try
                    {
                        var vlera = Converter.ConvertToInt(e.Value);
                        if (vlera == 0)
                        {
                            e.Criteria = null;
                        }
                    }
                    catch (Exception err)
                    {
                        ImbLogger.Error(err.Message);
                        e.Criteria = null;
                    }
                }
            }
        }

        /// <summary>
        /// Metoda qe thirret kur grida ben callback
        /// </summary>
        /// <param name="sender">Derguesi</param>
        /// <param name="e">Argumentat</param>
        protected void grid_AgjenteShitje_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            var idkomponente = string.Empty;
            var kodkonfigurimi = string.Empty;

            var arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                kodkonfigurimi = arr[1];
                idkomponente = arr[0];
                if (arr[2] == string.Empty)
                {
                    grid_AgjenteShitje.FilterExpression = string.Empty;
                }
                else
                {
                    var filtra = new DbCore.DbAdmin.clsFiltraGrida();

                    var koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "grid_AgjenteShitje", komponenteEmri, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), int.Parse(cmbKonfigurimi.Value.ToString()));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);

                    if (filtra.FiltraKodi != null)
                    {
                        grid_AgjenteShitje.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, grid_AgjenteShitje);
                        hfStatusi.Value = "true";
                    }
                    else
                    {
                        hfStatusi.Value = "false";
                    }
                }
            }
            else
            {
                if (arr.Length == 2)
                {
                    kodkonfigurimi = arr[1];
                    idkomponente = arr[0];
                }
                else
                {
                    idkomponente = e.Parameters;
                }
            }
            grid_AgjenteShitje.Selection.UnselectAll();
        }

        /// <summary>
        /// Metoda qe thirret pasi ka perfunduar se beri callback grida
        /// </summary>
        /// <param name="sender">Derguesi</param>
        /// <param name="e">Argumentat</param>
        protected void grid_AgjenteShitje_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            var cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            var rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (e.CallbackName == "COLUMNMOVE" && grid_AgjenteShitje.AllColumns[int.Parse(e.Args[0])].Width.Value == 0)
            {
                grid_AgjenteShitje.AllColumns[int.Parse(e.Args[0])].Width = Unit.Percentage(3);
            }
            mbushListeAgjentesh(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));

            GridUtil.ToolTipButonaveMbiGride(grid_AgjenteShitje, cultinf, rm);
        }

        /// <summary>
        /// Metoda qe percakton template e kolones se llogarise
        /// </summary>
        /// <param name="idNdermarrje">Id e ndermarrjes</param>
        private void percaktoTamplateLlogari(int idNdermarrje)
        {
            var col1 = grid_AgjenteShitje.Columns["IdLlogari"] as GridViewDataComboBoxColumn;
            col1.EditItemTemplate = new MyTemplateLlogari(idNdermarrje);
            col1.Width = 100;
        }
    }
}
