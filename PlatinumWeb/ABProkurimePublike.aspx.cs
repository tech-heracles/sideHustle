using DbCore;
using DbCore.DbAnalizeBuxheti;
using DevExpress.Web;
using System;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using PlatinumWeb.Templates;
using DbCore.DbAdmin;
using DbCore.IMBUtils.Logging;
using DevExpress.Web.Data;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    public partial class ABProkurimePublike : MyPageBase
    {
        private int idNdermarrje;
        private int idKonfig;
        private int idPerdoruesi;
        private int idGjuha;
        private int idViti;
        private int idNdermVit;
        private int idKomponente = 3037;
        private const string komponente = "ABProkurimePublike.aspx";
        private CultureInfo ci;
        private System.Resources.ResourceManager rm;
        private bool eshteMeme;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                DbCore.clsFunksione.logout(Session,true,"FaqePaautorizuar");
            }
            idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerdoruesi);
            }

            rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            if (!IsPostBack)
            {
                eshteMeme = DbCore.mySessionObjects.merrEshteMemeSesioni(Session);
                idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
                idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
                idNdermVit = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
                ci = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo(idGjuha);
                hfState.Set("idGjuha", idGjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idPerdoruesi", idPerdoruesi);
                hfState.Set("idViti", idViti);
                hfState.Set("eshteMeme", eshteMeme);
                hfState.Set("idKomponente", idKomponente);
                hfState.Set("komponente", komponente);
                hfState.Set("idNdermVit", idNdermVit);
                ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 143, "RPP", rm, ci, idGjuha);
                cmbKonfigurimi.SelectedIndex = 0;
                DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()));
                hfKonffillestar.Value = String.Format("{0};{1}", konf.KodKonfigAmbjente, konf.PershkrimKonfigAmbjente);

                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, komponente);
                ConfigureAspxComboBox.mbushComboPeriudhaKaterMujore(cmbPeriudha);
                cmbPeriudha.SelectedIndex = (DateTime.Now.Month / 4);
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DPlot.ToString();
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);

                idKonfig = int.Parse(cmbKonfigurimi.Value.ToString());

                mbushGridenEDokumentaveNgaDB(idNdermarrje);
                konfiguroGrideListeProkurimesh(cmbKonfigurimi.Text);
                mbushGridenEProkurimevePerEditim(-1);
                konfiguroGrideProkurimetMod();
            }
            else
            {
                idNdermarrje = (int)hfState.Get("idNdermarrje");
                idPerdoruesi = (int)hfState.Get("idPerdoruesi");
                idGjuha = (int)hfState.Get("idGjuha");
                idViti = (int)hfState.Get("idViti");
                idNdermVit = (int)hfState.Get("idNdermVit");
                idKonfig = int.Parse(cmbKonfigurimi.Value.ToString());
                mbushGridenEProkurimeveNgaSession();
                // GridUtil.PercaktoNgjyrenPerKolonatReadOnly(gvProkurimet, "IdTrupiDok", "RreshtiId", "IdKokaDok", "IdNdermarrje", "IdKrijuesi", "IdModifikuesi", "DtKrijimi", "DtModifikimi");
            }
            
            ci = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo(idGjuha);
            gvListaProkurimet.PercaktoTitlePanel(this, MenuInfo, pnlMesazhi, hfState, idPerdoruesi, idNdermarrje, idViti, idGjuha, idKonfig, komponente, rm, ci);
            //GridUtil.PercaktoTitlePanelPerTrupDokumenti(gvProkurimet, this, MenuInfo, pnlMesazhi, hfState, idPerdoruesi, idNdermarrje, idViti, idGjuha, 1, komponente, idKomponente, "IdTrupiDok", rm, ci);
            percaktoTemplateMenu();
        }

       

        protected void gvProkurimet_DataBound(object sender, EventArgs e)
        {
            gvProkurimet.KeyFieldName = "RpkId";
           
        }
        public void ZevendesoPeriudhen()
        {
            GridViewDataComboBoxColumn colNew = new GridViewDataComboBoxColumn();

            this.gvListaProkurimet.Columns.Remove(gvListaProkurimet.Columns["KaterMujori"]);
            //  gvPasqyra.Columns.Add(colNew);
            colNew.FieldName = "KaterMujori";

            colNew.Caption = "Periudha";

            colNew.PropertiesComboBox.Items.Add("4-Mujori i I", 1);
            colNew.PropertiesComboBox.Items.Add("4-Mujori i II", 2);
            colNew.PropertiesComboBox.Items.Add("4-Mujori i III", 3);


            colNew.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            gvListaProkurimet.Columns.Add(colNew);
        }
       
        public void ZevendesoKolonenStatusi()
        {

            GridViewDataComboBoxColumn colNew = new GridViewDataComboBoxColumn();

            this.gvListaProkurimet.Columns.Remove(gvListaProkurimet.Columns["IdStatusDok"]);
            //  gvPasqyra.Columns.Add(colNew);
            colNew.FieldName = "IdStatusDok";

            colNew.Caption = "Statusi";

            colNew.PropertiesComboBox.Items.Add("Draft", 0);
            colNew.PropertiesComboBox.Items.Add("Ruajtur", 1);

            colNew.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            gvListaProkurimet.Columns.Add(colNew);



        }

        protected void gvListaProkurimet_DataBound(object sender, EventArgs e)
        {
            gvListaProkurimet.KeyFieldName = "IdKokaRp";
            if (this.gvListaProkurimet.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per
                //perzgjidh
                DevExpress.Web.GridViewCommandColumn check = new DevExpress.Web.GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true;
                check.Width = Unit.Percentage(2);
                check.VisibleIndex = 0;
                gvListaProkurimet.SettingsBehavior.AllowSelectByRowClick = true;
                gvListaProkurimet.Columns.Add(check);
            }
            ZevendesoKolonenStatusi();
            ZevendesoPeriudhen();
        }

        protected void ButtonOk_Click(object sender, EventArgs e)
        {
            clsMesazh mesazhi = new clsMesazh();
            //mund te behet funksion generic
            try
            {
                if (gvListaProkurimet.Selection.Count < 1)
                {
                    mesazhi = new clsMesazh(false, "Ju lutem zgjidhni te pakten nje rresht!");
                }
                else
                {
                    object tmp = mySessionObjects.merrObjectNgaSesioni(Session, "gvListaProkurimet");
                    colKokaRealizimProkurimesh col = (tmp as colKokaRealizimProkurimesh) ?? new colKokaRealizimProkurimesh(idNdermarrje, idNdermVit);
                    var selectedIDs = Array.ConvertAll(gvListaProkurimet.GetSelectedFieldValues("IdKokaRp").ToArray(), Convert.ToInt32);

                    foreach (int id in selectedIDs)
                    {
                        clsKokaRealizimProkurimesh kokaTmp = col.FirstOrDefault(x => x.IdKokaRp == id);
                        kokaTmp.IdModifikuesi = idPerdoruesi;

                        mesazhi = kokaTmp.Fshi();
                        if (mesazhi.Status)
                            col.Remove(kokaTmp);
                        else
                            break;
                    }
                    if (mesazhi.Status)
                    {
                        mySessionObjects.ruajObjectNeSesion(Session, col, "gvListaProkurimet");
                    }
                }

                if (mesazhi.Status)
                {
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazhi.PershkrimMesazhi, pnlMesazhi);
                    hfStatusVeprimi.Value = "true";
                }
                else
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhi.PershkrimMesazhi, pnlMesazhi);
                    hfStatusVeprimi.Value = "false";
                }
            }
            catch (Exception ex)
            {
                ImbLogger.Error(ex.Message);
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhi.PershkrimMesazhi, pnlMesazhi);
                hfStatusVeprimi.Value = "false";
            }
        }

        private void mbushGridenEDokumentaveNgaDB(int idNdermarrje)
        {
            colKokaRealizimProkurimesh col = new colKokaRealizimProkurimesh(idNdermarrje, idNdermVit);
            gvListaProkurimet.DataSource = col;
            gvListaProkurimet.DataBind();

            mySessionObjects.ruajObjectNeSesion(Session, col, "gvListaProkurimet");
        }

        private void mbushGridenEProkurimevePerEditim(int idKokaRp)
        {
            colTrupiRealizimProkurimesh col = null;
            if (idKokaRp == 0 || idKokaRp == -1)
            {
                //krijo nje trup bosh
                col = new colTrupiRealizimProkurimesh().MerrTrupDefault(idNdermarrje, idNdermVit);
            }
            else
            {
                //merr nje trup ekzistues
                col = new colTrupiRealizimProkurimesh(idKokaRp);
            }
            gvProkurimet.DataSource = col;
            gvProkurimet.DataBind();
            mySessionObjects.ruajObjectNeSesion(Session, col, "gvProkurimet");
        }

        /// <summary>
        /// merr ds e grides nga sessioni
        /// </summary>
        public void mbushGridenEProkurimeveNgaSession()
        {
            object tmp = mySessionObjects.merrObjectNgaSesioni(Session, "gvListaProkurimet");
            colKokaRealizimProkurimesh col = (tmp as colKokaRealizimProkurimesh) ?? new colKokaRealizimProkurimesh(idNdermarrje, idNdermVit);
            gvListaProkurimet.DataSource = tmp;
            gvListaProkurimet.DataBind();
        }

        private void konfiguroGrideListeProkurimesh(string kodi)
        {
            GridUtil.PercaktoVisibleColumnsAndTypesGridSipasKodKonfigurimi(idNdermarrje, "gvListaProkurimet", gvListaProkurimet, kodi, idKomponente.ToString(), idGjuha);
            GridUtil.konfigGrideListeEMadhePaTheme(gvListaProkurimet, "IdKokaRp");

            ZevendesoKolonenStatusi();
            ZevendesoPeriudhen();
        }

        private void konfiguroGrideProkurimetMod()
        {
            GridUtil.PercaktoVisibleColumnsAndTypesGridSipasKodKonfigurimi(idNdermarrje, "gvProkurimet", gvProkurimet, string.Empty, idKomponente.ToString(), idGjuha);
            GridUtil.konfiguroGridaPerBatchEditing(gvProkurimet, true, true, false, false);
            gvProkurimet.SettingsBehavior.AllowSort = false;
            gvProkurimet.SettingsBehavior.AllowDragDrop = false;
            gvProkurimet.SettingsBehavior.AllowGroup = false;
            gvProkurimet.GroupBy(gvProkurimet.Columns["ZeriPrind"]);

            gvProkurimet.KeyFieldName = "RpkId";
        }

        private void percaktoTemplateMenu()
        {
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdoruesi, idNdermarrje, ASPxMenu1, komponente, this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, false, true, false, eshteMeme, true);
        }

        public void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
        }

        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
        }

        protected void gvProkurimet_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            try
            {
                int idkoka = int.Parse(e.Parameters);
                mbushGridenEProkurimevePerEditim(idkoka);

                //  gvProkurimet.AddNewRow();
            }
            catch (Exception ex)
            {
                ImbLogger.Error(ex.Message);
                throw ex;
            }
        }

        protected void gvProkurimet_BatchUpdate(object sender, DevExpress.Web.Data.ASPxDataBatchUpdateEventArgs e)
        {
            clsMesazh mesazhi = null;
            try
            {
                clsTeDrejtaRoli teDrejta = new clsTeDrejtaRoli();
                teDrejta.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, komponente);
                int idStatusDok = hfStatusDokumenti.Value == "draft" ? 0 : 1;
                bool eshteShtim = hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim";
                object tmp = mySessionObjects.merrObjectNgaSesioni(Session, "gvListaProkurimet");
                colKokaRealizimProkurimesh col = (tmp as colKokaRealizimProkurimesh) ?? new colKokaRealizimProkurimesh(idNdermarrje, idNdermVit);

                clsKokaRealizimProkurimesh koka = KrijoKokeDokumentProkurimi(eshteShtim);
                koka.IdStatusDok = idStatusDok;

                if (eshteShtim)
                {
                    ///inicializohet ketu per te shmangur zenien dyfish te memories

                    //modifikon ekzistuese
                    foreach (ASPxDataUpdateValues updated in e.UpdateValues)
                    {
                        clsTrupiRealizimProkurimesh trupi = koka.ColTrupi.FirstOrDefault(x => x.RpkId == updated.MerrKeyValue<int>());

                        clsTrupiRealizimProkurimesh trupiNew = updated.MerrCustomUpdatedObject(trupi);
                        trupiNew.DtKrijimi = DateTime.Now;
                        trupiNew.IdKrijuesi = idPerdoruesi;
                        trupiNew.IdStatusDok = koka.IdStatusDok;
                        trupiNew.IdModifikuesi = idPerdoruesi;
                    }

                    mesazhi = isValid(koka, eshteShtim);
                    if (mesazhi.Status)
                    {

                        if ((idStatusDok == 0 && !teDrejta.DShtimDraft) || (idStatusDok == 1 && !teDrejta.DShtim))
                            mesazhi = new clsMesazh(false, MessagesResource.Messages["msgNukKeniTeDrejta"]);

                        else
                            mesazhi = koka.Ruaj();

                    }
                    if (mesazhi.Status)
                    {
                        col.Add(koka);
                    }
                }
                else //Modifikim
                {
                    koka.IdModifikuesi = idPerdoruesi;
                    if (koka.IdStatusDok == 1 && idStatusDok == 0)
                    {
                        mesazhi = new clsMesazh(false, "Ky dokument eshte me status  ruajtur dhe nuk mund te ruhet me status draft!");
                        //DbCore.mySessionObjects.ruajMesazhNeSesion(Session, mesazhi.PershkrimMesazhi + ":Red");
                        //  throw new Exception("Ky dokument eshte me status  ruajtur dhe nuk mund te ruhet me status draft!");
                    }
                    else
                    {
                        //modifikon ekzistuese
                        for (int i = 0; i < e.UpdateValues.Count; i++)
                        {
                            clsTrupiRealizimProkurimesh trupi = koka.ColTrupi.Where(x => x.RpkId == e.UpdateValues[i].MerrKeyValue<int>()).FirstOrDefault();

                            clsTrupiRealizimProkurimesh trupiNew = e.UpdateValues[i].MerrCustomUpdatedObject(trupi);
                            trupiNew.DtKrijimi = DateTime.Now;
                            trupiNew.IdKrijuesi = idPerdoruesi;
                            trupiNew.IdStatusDok = koka.IdStatusDok;
                            trupiNew.IdModifikuesi = idPerdoruesi;
                        }

                        mesazhi = isValid(koka, eshteShtim);
                        //merret objekti i vjeter
                        int indexi = col.FindIndex(x => x.IdKokaRp == koka.IdKokaRp);
                        if (mesazhi.Status)
                        {
                            if ((idStatusDok == 0 && !teDrejta.DModifikimDraft) || (idStatusDok == 1 && !teDrejta.DMod))
                                mesazhi = new clsMesazh(false, MessagesResource.Messages["msgNukKeniTeDrejta"]);

                            mesazhi = koka.Modifiko();
                            if (mesazhi.Status)
                                col[indexi] = koka;
                        }
                    }
                }
                if (mesazhi.Status)
                {
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, mesazhi.PershkrimMesazhi + ":Green");
                    mySessionObjects.ruajObjectNeSesion(Session, col, "gvListaProkurimet");
                    mySessionObjects.ruajObjectNeSesion(Session, null, "gvProkurimet");
                }
                else
                {
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, mesazhi.PershkrimMesazhi + ":Red");
                    mySessionObjects.ruajObjectNeSesion(Session, koka.ColTrupi, "gvProkurimet");
                }
                gvProkurimet.DataSource = koka.ColTrupi;
                gvProkurimet.DataBind();

            }
            catch (Exception err)
            {
                ImbLogger.Error(err.Message);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, err.Message + ":Red");
                throw;
            }

            e.Handled = true;
        }

        private clsKokaRealizimProkurimesh KrijoKokeDokumentProkurimi(bool eshteShtim)
        {
            clsKokaRealizimProkurimesh koka = null;
            colTrupiRealizimProkurimesh trupi = mySessionObjects.merrObjectNgaSesioni(Session, "gvProkurimet") as colTrupiRealizimProkurimesh;

            if (!eshteShtim || hfShtimModifikim.Value == "klonim")
            {
                int kokaId = Convert.ToInt32(gvListaProkurimet.GetRowValues(gvListaProkurimet.FocusedRowIndex, "IdKokaRp"));
                koka = new clsKokaRealizimProkurimesh(kokaId);
            }
            else
            {
                koka = new clsKokaRealizimProkurimesh();
            }

            koka.IdNdermarrje = idNdermarrje;
            koka.IdKrijuesi = idPerdoruesi;
            koka.DtKrijimi = DateTime.Now;
            koka.IdNdermVit = idNdermVit;
            koka.KaterMujori = Convert.ToInt16(cmbPeriudha.Value);
            koka.NrDok = txtNrDok.Text;
            koka.Parashikim = false;
            if (trupi.Count > 0)
                koka.ColTrupi = trupi;
            return koka;
        }

        private clsMesazh isValid(clsKokaRealizimProkurimesh koka, bool eshteShtim)
        {
            clsMesazh mesazhi = new clsMesazh(true);
            if (eshteShtim)
            {
                mesazhi = koka.EkzistonNjeDokumentPerKetePeriudhe();
                if (!mesazhi.Status)
                {
                    return mesazhi;
                }
            }
            mesazhi = koka.EkzistonNjeDokumentMeKeteNrDok(eshteShtim);
            if (!mesazhi.Status)
            {
                return mesazhi;
            }
            if (koka.ColTrupi.Where(x => x.FondiLimit < 0 || x.VleraKontrates < 0).FirstOrDefault() != null)
                return new clsMesazh(false, "Vlera e fondit limit dhe vleres se kontrates nuk mund te jete negative!");
            return mesazhi;
        }

        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {
        }

        protected void gvProkurimet_CustomErrorText(object sender, ASPxGridViewCustomErrorTextEventArgs e)
        {
            e.ErrorText = "";
        }
    }
}