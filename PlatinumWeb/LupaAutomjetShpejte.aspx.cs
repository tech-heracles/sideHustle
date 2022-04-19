using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DbCore;
using DbCore.DbRegjistrim;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Messages;
using DevExpress.Web;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class LupaAutomjetShpejte : MyPageBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            int idPerdoruesi;
            int idGjuha;
            int idNdermarrje;
            int idViti;
            int idNdermarrjeVit;
            int idKonfigambjenti;
            if (hfState.Count == 0)
            {
                if (!DbCore.mySessionObjects.isLogedIn(Session))
                {
                    DbCore.clsFunksione.logout(Session,true,"FaqePaautorizuar");
                }
                idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
                idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
                idNdermarrjeVit = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
                hfState.Set("idPerdoruesi", idPerdoruesi);
                hfState.Set("idGjuha", idGjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idViti", idViti);
                hfState.Set("idNdermarrjeVit", idNdermarrjeVit);
                hfState.Set("Meme", DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
                hfState.Set("MonedhaNder", DbCore.DbAdmin.clsMonedha.ktheMonedhenENdermarrjes(idNdermarrje));
            }
            else
            {
                idPerdoruesi = (int)hfState["idPerdoruesi"];
                idGjuha = (int)hfState["idGjuha"];
                idNdermarrje = (int)hfState["idNdermarrje"];
                idViti = (int)hfState["idViti"];
                idNdermarrjeVit = (int)hfState["idNdermarrjeVit"];
            }
            if (!IsPostBack)
            {
                percaktoTemplateMenu(idViti, idPerdoruesi, idNdermarrje, ASPxMenu1);
                string vleraQueryString = "";
                if (Request.QueryString["idKonfigAmbjente"] != null && Request.QueryString["idKonfigAmbjente"] != "")
                    vleraQueryString = Request.QueryString["idKonfigAmbjente"].ToString();
                int idNivel = clsNivelRegjistrimi.ktheIdNivelRegjistrimiSipasKodi("LLOGSH", idNdermarrje);
                if (vleraQueryString != "")
                {
                    //ketu me intereson id e nivelit
                    //ne rastin kur kemi disa konfigurime te lupes per kontrollin i cili gjeneroi thirrjen e kesaj lupe
                    //duhet te gjejme cili nga konfigurimet e ka idNivel sa niveli i artikujve
                    //i kontrollojme me radhe te gjitha konfigurimet qe i jane kaluar ne query string
                    string[] idte = vleraQueryString.Split('-');
                    if (idte.Length > 1)
                    {
                        //DbCore.DbShare.clsKonfigurimAmbjenti konfigLupa;
                        for (int i = 0; i < idte.Length; i++)
                        {
                            //konfigLupa = new DbCore.DbShare.clsKonfigurimAmbjenti(Convert.ToInt32(idte[i]));
                            if (DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdNivel(Convert.ToInt32(idte[i])) == idNivel)
                            {
                                idKonfigambjenti = Convert.ToInt32(idte[i]);
                                break;
                            }
                        }
                        idKonfigambjenti = merrKonfiguriminDefaultTeLupes(idNdermarrje, idNivel);
                    }
                    else
                        if (idte.Length == 1)
                        {
                            idKonfigambjenti = Convert.ToInt32(vleraQueryString);
                            if (idKonfigambjenti == 0 || idKonfigambjenti == -1)
                                idKonfigambjenti = merrKonfiguriminDefaultTeLupes(idNdermarrje, idNivel);
                        }
                        else
                            idKonfigambjenti = merrKonfiguriminDefaultTeLupes(0, idNivel);
                }
                else
                    idKonfigambjenti = merrKonfiguriminDefaultTeLupes(idNdermarrje, idNivel);
                hfState.Set("idKonfigambjenti", idKonfigambjenti);
                System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
                System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                hfState.Set("headerZgjidhKlientFurnitorin", rm.GetString("headerZgjidhKlientFurnitorin", ci));
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, "LupaAutomjetShpejte.aspx");
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                konfiguroVleraFillestare(idNdermarrje, idGjuha);
            }
            else
            {
                if (!IsCallback || (IsCallback && Request["__CALLBACKID"].ToString().Contains("ASPxMenu1")))
                    percaktoTemplateMenu(idViti, idPerdoruesi, idNdermarrje, ASPxMenu1);
            }
        }

        private void konfiguroVleraFillestare(int idNdermarrje, int idGjuha)
        { //mbush komboboxet dhe gridat e faqes
            int idPerdoruesi = (int)hfState["idPerdoruesi"];
            ConfigureAspxComboBox.ShtoKolonaPerKf(btneKlienti);
            ConfigureAspxComboBox.shtoKolonaPerModelAutomjeti(cmbModelAuto);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btneKlienti);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmbModelAuto);
            mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 87, "AUTOSH", idGjuha);
            cmbKonfigurimi.SelectedIndex = 0;
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()));
            hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
        }

        public void mbushComboKonfigurimeshSipasKategorise(int idPerdoruesi, int idNdermarrje, ASPxComboBox combo, int kat, string nivel, int idGjuha)
        {//mbush griden e popupit me te dhena
            DbCore.DbShare.colKonfigurimAmbjenti col = new DbCore.DbShare.colKonfigurimAmbjenti();
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            if (kat != 0)
            {
                konf.IdKategori = kat;
                konf.IdNdermarje = idNdermarrje;
                int idNivel = DbCore.DbRegjistrim.clsNivelRegjistrimi.ktheIdNivelRegjistrimiSipasKodi(nivel, idNdermarrje);
                col.mbushKonfigAmbjSipasIdKategoriIdNivelMeLloj(konf.IdKategori, idNivel, idPerdoruesi);
            }
            else
            {
                col.mbushGjitheKonfigurimeAmbjentesh(idNdermarrje, idPerdoruesi, 1, idGjuha);
            }
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

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu((int)hfState["idViti"], (int)hfState["idPerdoruesi"], (int)hfState["idNdermarrje"], ASPxMenu1);
        }

        /// <summary>
        /// metoda per te thirrur veprimet e menuse kur shtypen butonat
        /// </summary>
        /// <param name="source">derguesi</param>
        /// <param name="e">parametrat</param>
        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {
            if (e.Item.Name == "Ruaj")
            {
                Page.Validate("entries");
                ruajAutomjet();
            }
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
            clsToolbarConfig.percaktoTemplateMenu((int)hfState["idGjuha"], idViti, idPerdorues, idNdermarrje, aSPxMenu1, "LupaAutomjetShpejte.aspx", this, MenuInfo, hfShtimModifikim.Value == "modifikim" ? false : true, true, false, (bool)hfState["Meme"]);
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            aSPxMenu1.Items.FindByName("Anullo").Text = rm.GetString("MenuItemMbyll", ci);
        }

        private int merrKonfiguriminDefaultTeLupes(int idNdermarrje, int idNivel)
        {
            //do marr konfigurimin default per kete nivel regjistrimi i cili eshte i vetem per nje ndermarrje
            return DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimi(idNdermarrje, idNivel);

        }

        private void ruajAutomjet()
        {
            if (Page.IsValid == false)
                return;
            int idPerdoruesi = (int)hfState["idPerdoruesi"];
            int idGjuha = (int)hfState["idGjuha"];
            int idNdermarrje = (int)hfState["idNdermarrje"];
            int idViti = (int)hfState["idViti"];
            DbCore.DbInventari.clsAutomjete automjeti;
            try
            {
                if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
                    automjeti = krijoAutomjet(idNdermarrje, idPerdoruesi, true);
                else automjeti = krijoAutomjet(idNdermarrje, idPerdoruesi, false);
            }
            catch (Exception e)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(e.Message);
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, e.Message, pnlMesazhi);
                hfStatusi.Value = "false";
                return;
            }
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, "LupaAutomjetShpejte.aspx");
            if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
            {
                if (!tedrejtaInfo.DShtim)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                    hfStatusi.Value = "false";
                    return;
                }
                mesazh = automjeti.ruaj(hfNrAutoKF);
            }
            else
            {
                if (!tedrejtaInfo.DMod)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                    hfStatusi.Value = "false";
                    return;
                }
                mesazh = automjeti.modifiko();
            }
            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                hfStatusi.Value = "false";
            }
            else
            {
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);                
                hfStatusi.Value = "true";                
            }
        }

        private DbCore.DbInventari.clsAutomjete krijoAutomjet(int idNdermarrje, int idPerdorues, bool shtim)
        {
            var controls = this.GetAsPxTextEditIdValue();
            controls.AddRange(ASPxPanel1.GetAsPxTextEditIdValue());

            hfNrAuto = (ASPxHiddenField)DbCore.DbAdmin.NrAuto.VendosVleratNrAuto(hfNrAuto, controls);
            //DbCore.DbAdmin.NrAuto.vendosVleratNrAuto(hfNrAuto, this, null, null, ASPxPanel1);

            hfNrAutoKF = (ASPxHiddenField)DbCore.DbAdmin.NrAuto.ShtoNeRegjistrime(hfNrAutoKF, hfNrAuto, "txtNrShasie", "NrShasie");
            //DbCore.DbAdmin.NrAuto.shtoNeHfRegjistrime(hfNrAutoKF, hfNrAuto, "txtNrShasie", "NrShasie");

            int idAuto;
            if (shtim == true)
                idAuto = 0;
            else idAuto = int.Parse(hfId.Value.ToString());
            int idModelAuto;
            try
            {
                if (cmbModelAuto.Text != "")
                    idModelAuto = int.Parse(cmbModelAuto.Value.ToString());
                else idModelAuto = 0;
            }
            catch (Exception err)
            {
                string mesazhi = "Ju lutem plotesoni modelin e automjetit!";
                NLog.LogManager.GetCurrentClassLogger().Error(mesazhi,err.Message);
                throw new DbCore.MyException(mesazhi);
            }
            int idVitProdhimi;
            try
            {
                if (txtVitProdhimi.Text != "")
                    idVitProdhimi = int.Parse(txtVitProdhimi.Text);
                else idVitProdhimi = 0;
            }
            catch (Exception err)
            {
                string mesazhi = "Viti i prodhimit nuk eshte i sakte!";
                NLog.LogManager.GetCurrentClassLogger().Error(mesazhi, err.Message);
                throw new DbCore.MyException(mesazhi);
            }
            double kilometra;
            try
            {
                if (txtKilometra.Text != "")
                    kilometra = double.Parse(txtKilometra.Text);
                else kilometra = 0;
            }
            catch (Exception err)
            {
                string mesazhi = "Numri i kilometrave nuk eshte i sakte!";
                NLog.LogManager.GetCurrentClassLogger().Error(mesazhi, err.Message);
                throw new DbCore.MyException(mesazhi);
            }
            int idKlienti;
            try
            {
                if (btneKlienti.Text != "")
                    idKlienti = int.Parse(btneKlienti.Value.ToString());
                else idKlienti = 0;
            }
            catch (Exception err)
            {
                string mesazhi = "Ju lutem plotesoni klientin!";
                NLog.LogManager.GetCurrentClassLogger().Error(mesazhi, err.Message);
                throw new DbCore.MyException(mesazhi);
            }

            DbCore.DbInventari.clsAutomjete autoNew = new DbCore.DbInventari.clsAutomjete(idAuto, DbCore.clsFunksione.ktheStringunPaHapesira(txtNrShasie.Text, true), txtTarga.Text, idModelAuto, idVitProdhimi, kilometra, txtKodMotorri.Text, 1, idKlienti, idPerdorues, idNdermarrje, idPerdorues, DateTime.Now, DateTime.Now, btneKlienti.Text, cmbModelAuto.Text, shtim, txtMarka.Text, rm, ci);
            return autoNew;
        }

        protected void btneKlienti_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("btneKlienti"))
                {
                    int value = 0;
                    if (e.Value == null || !Int32.TryParse(e.Value.ToString(), out value))
                        return;
                    int idPerdoruesi = (int)hfState["idPerdoruesi"];
                    int idNdermarrje = (int)hfState["idNdermarrje"];
                    ConfigureAspxComboBox.KonfiguroComboBoxKlientFurnitoriById(idPerdoruesi, idNdermarrje, (ASPxComboBox)source, value);
                }
            }
        }

        protected void btneKlienti_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("btneKlienti"))
                {
                    int idPerdoruesi = (int)hfState["idPerdoruesi"];
                    int idNdermarrje = (int)hfState["idNdermarrje"];
                    ConfigureAspxComboBox.KonfiguroComboBoxKlientFurnitor(e.Filter, e.BeginIndex + 1, e.EndIndex + 1, idPerdoruesi, idNdermarrje, btneKlienti, 1);
                }
            }
        }

        protected void cmbModelAuto_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("cmbModelAuto"))
                {
                    int value = 0;
                    if (e.Value == null || !Int32.TryParse(e.Value.ToString(), out value))
                        return;
                    int idPerdoruesi = (int)hfState["idPerdoruesi"];
                    int idNdermarrje = (int)hfState["idNdermarrje"];
                    ConfigureAspxComboBox.mbushComboModelAutomjetiByID((ASPxComboBox)source, value);
                }
            }
        }

        protected void cmbModelAuto_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("cmbModelAuto"))
                {
                    int idNdermarrje = (int)hfState["idNdermarrje"];
                    ConfigureAspxComboBox.mbushComboModelAutomjetesh(e.Filter, e.BeginIndex + 1, e.EndIndex + 1, idNdermarrje, cmbModelAuto);
                }
            }
        }
    }
}