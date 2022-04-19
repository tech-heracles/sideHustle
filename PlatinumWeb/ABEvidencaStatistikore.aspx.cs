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
using DevExpress.Web.Data;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    public partial class ABEvidencaStatistikore : MyPageBase
    {
        private int idNdermarrje;
        private int idKonfig;
        private int idPerdoruesi;
        private int idGjuha;
        private int idViti;
        private int idNdermVit;
        private int idKomponente = 3023;
        private const string komponente = "ABEvidencaStatistikore.aspx";
        private CultureInfo ci;
        private System.Resources.ResourceManager rm;
        private bool eshteMeme;
        string guidString;

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
                guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                hfState.Set("idGjuha", idGjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idPerdoruesi", idPerdoruesi);
                hfState.Set("idViti", idViti);
                hfState.Set("eshteMeme", eshteMeme);
                hfState.Set("idKomponente", idKomponente);
                hfState.Set("komponente", komponente);
                hfState.Set("idNdermVit", idNdermVit);
                hfState.Set("guidString", guidString);
                ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 132, "ABES", rm, ci, idGjuha);
                cmbKonfigurimi.SelectedIndex = 0;
                var konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()));
                hfKonffillestar.Value = String.Format("{0};{1}", konf.KodKonfigAmbjente, konf.PershkrimKonfigAmbjente);

                var tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, komponente);
                ConfigureAspxComboBox.mbushComboPeriudhaTreMujore(cmbPeriudha);
                cmbPeriudha.Value = (DateTime.Now.Month / 4) + 1;
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DPlot.ToString();
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);

                idKonfig = int.Parse(cmbKonfigurimi.Value.ToString());

                mbushGridenEEvidencaveNgaDB(idNdermarrje);
                konfiguroGrideListEvidencash(cmbKonfigurimi.Text);
                GridUtil.PercaktoVisibleColumnsAndTypesGridSipasKodKonfigurimi(idNdermarrje, "gvListaEvidencat", gvListaEvidencat, cmbKonfigurimi.Text, idKomponente.ToString(), idGjuha);
                mbushGridenEvidencaPerEditim(-1);
            }
            else
            {
                idNdermarrje = (int)hfState.Get("idNdermarrje");
                idPerdoruesi = (int)hfState.Get("idPerdoruesi");
                idGjuha = (int)hfState.Get("idGjuha");
                idViti = (int)hfState.Get("idViti");
                idNdermVit = (int)hfState.Get("idNdermVit");
                idKonfig = int.Parse(cmbKonfigurimi.Value.ToString());
                guidString = (string)hfState.Get("guidString");
                mbushGridenEEvidencaveNgaSession();
                konfiguroGrideListEvidencash(cmbKonfigurimi.Text);
                krijoTotalSummary();
                // GridUtil.PercaktoNgjyrenPerKolonatReadOnly(gvEvidenca, "IdTrupiDok", "RreshtiId", "IdKokaDok", "IdNdermarrje", "IdKrijuesi", "IdModifikuesi", "DtKrijimi", "DtModifikimi");
            }
            
            ci = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo(idGjuha);
            KonfigurimComboGride.shtoTreMujoret(gvListaEvidencat);
            KonfigurimComboGride.ShtoZeratAnalizeBuxheti(gvEvidenca, clsNdermarrje.ktheIdNdermarrjeRaportimiRoot(idNdermarrje), 11, Session, komponente, guidString, "RreshtiId");
            KonfigurimComboGride.shtoStatuse(gvListaEvidencat);

            konfiguroGrideEvidencaMod();
            gvListaEvidencat.PercaktoTitlePanel(this, MenuInfo, pnlMesazhi, hfState, idPerdoruesi, idNdermarrje, idViti, idGjuha, idKonfig, komponente, rm, ci);
            //GridUtil.PercaktoTitlePanelPerTrupDokumenti(gvEvidenca, this, MenuInfo, pnlMesazhi, hfState, idPerdoruesi, idNdermarrje, idViti, idGjuha, 1, komponente, idKomponente, "IdTrupiDok", rm, ci);
            percaktoTemplateMenu();
        }

        private void krijoTotalSummary()
        {

            foreach (var col in gvEvidenca.VisibleColumns)
            {
                if (col.Name == "RreshtiId")
                {
                    col.FooterTemplate = new MyFooterCellTemplate(col.Name, "Totali ");
                    continue;
                }

                if (col.GetType() == typeof(GridViewDataSpinEditColumn))
                {
                    gvEvidenca.ShtoTotalSummary("n2", DevExpress.Data.SummaryItemType.Sum, col.Name);
                    GridUtil.PercaktoTemplateTotalSummaryFooter(gvEvidenca, "n2", col.Name);
                }
            }
        }

        protected void gvEvidenca_DataBound(object sender, EventArgs e)
        {
            gvEvidenca.KeyFieldName = "RreshtiId";
        }
     
      
    

        protected void gvListaEvidencat_DataBound(object sender, EventArgs e)
        {
            gvListaEvidencat.KeyFieldName = "IdKokaDok";
            if (this.gvListaEvidencat.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per
                //perzgjidh
                var check = new DevExpress.Web.GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true;
                check.Width = Unit.Percentage(2);
                check.VisibleIndex = 0;
                gvListaEvidencat.SettingsBehavior.AllowSelectByRowClick = true;
                gvListaEvidencat.Columns.Add(check);
            }
            
        }

        protected void ButtonOk_Click(object sender, EventArgs e)
        {
            var mesazhi = new clsMesazh();
            //mund te behet funksion generic
            try
            {
                if (gvListaEvidencat.Selection.Count < 1)
                {
                    mesazhi = new clsMesazh(false, "Ju lutem zgjidhni te pakten nje rresht!");
                }
                else
                {
                    var tmp = mySessionObjects.merrObjectNgaSesioni(Session, "gvListaEvidencat");
                    var col = (tmp as colKokaEvidencaStatistikore) ?? new colKokaEvidencaStatistikore(idNdermarrje, idNdermVit);
                    var selectedIDs = Array.ConvertAll(gvListaEvidencat.GetSelectedFieldValues("IdKokaDok").ToArray(), Convert.ToInt32);

                    foreach (var id in selectedIDs)
                    {
                        var kokaTmp = col.FirstOrDefault(x => x.IdKokaDok == id);
                        kokaTmp.IdModifikuesi = idPerdoruesi;

                        mesazhi = kokaTmp.Fshi();
                        if (mesazhi.Status)
                            col.Remove(kokaTmp);
                        else
                            break;
                    }
                    if (mesazhi.Status)
                    {
                        mySessionObjects.ruajObjectNeSesion(Session, col, "gvListaEvidencat");
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
                NLog.LogManager.GetCurrentClassLogger().Error(ex.Message);
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhi.PershkrimMesazhi, pnlMesazhi);
                hfStatusVeprimi.Value = "false";
            }
        }

        private void mbushGridenEEvidencaveNgaDB(int idNdermarrje)
        {
            var col = new colKokaEvidencaStatistikore(idNdermarrje, idNdermVit);
            gvListaEvidencat.DataSource = col;
            gvListaEvidencat.DataBind();

            mySessionObjects.ruajObjectNeSesion(Session, col, "gvListaEvidencat");
        }

        private void mbushGridenEvidencaPerEditim(int idKoka)
        {
            colTrupiEvidencaStatistikore col = null;
            if (idKoka == 0 || idKoka == -1)
            {
                //krijo nje trup bosh
                col = new colTrupiEvidencaStatistikore().MerrTrupDefault(idNdermarrje, 11);
            }
            else
            {
                //merr nje trup ekzistues
                col = new colTrupiEvidencaStatistikore(idKoka);
            }
            gvEvidenca.DataSource = col;
            gvEvidenca.DataBind();
            mySessionObjects.ruajObjectNeSesion(Session, col, "gvEvidenca");
        }

        /// <summary>
        /// merr ds e grides nga sessioni
        /// </summary>
        public void mbushGridenEEvidencaveNgaSession()
        {
            var tmp = mySessionObjects.merrObjectNgaSesioni(Session, "gvListaEvidencat");
            var col = (tmp as colKokaEvidencaStatistikore) ?? new colKokaEvidencaStatistikore(idNdermarrje, idNdermVit);
            gvListaEvidencat.DataSource = tmp;
            gvListaEvidencat.DataBind();
        }

        private void konfiguroGrideListEvidencash(string kodi)
        {
            GridUtil.konfigGrideListeEMadhePaTheme(gvListaEvidencat, "IdKokaDok");
          
        }

        private void konfiguroGrideEvidencaMod()
        {
            GridUtil.PercaktoVisibleColumnsAndTypesGridSipasKodKonfigurimi(idNdermarrje, "gvEvidenca", gvEvidenca, string.Empty, idKomponente.ToString(), idGjuha);
            GridUtil.konfiguroGridaPerBatchEditing(gvEvidenca, true, true, false, false);
            krijoTotalSummary();
            gvEvidenca.SettingsBehavior.AllowSort = false;
            gvEvidenca.SettingsBehavior.AllowDragDrop = false;
            gvEvidenca.SettingsBehavior.AllowGroup = false;

            gvEvidenca.KeyFieldName = "RreshtiId";
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

        protected void gvEvidenca_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            try
            {
                var idkoka = int.Parse(e.Parameters);
                mbushGridenEvidencaPerEditim(idkoka);

                //  gvEvidenca.AddNewRow();
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex.Message);
                throw ex;
            }
        }

        protected void gvEvidenca_BatchUpdate(object sender, DevExpress.Web.Data.ASPxDataBatchUpdateEventArgs e)
        {
            clsMesazh mesazhi = null;
            try
            {
                var teDrejta = new clsTeDrejtaRoli();
                teDrejta.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, komponente);
                var idStatusDok = hfStatusDokumenti.Value == "draft" ? 0 : 1;
                var eshteShtim = hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim";
                var tmp = mySessionObjects.merrObjectNgaSesioni(Session, "gvListaEvidencat");
                var col = (tmp as colKokaEvidencaStatistikore) ?? new colKokaEvidencaStatistikore(idNdermarrje, idNdermVit);

                var koka = KrijoKokeEvidencaStatistikore(eshteShtim);
                koka.IdStatusDok = idStatusDok;

                if (eshteShtim)
                {
                    ///inicializohet ketu per te shmangur zenien dyfish te memories

                    //modifikon ekzistuese
                    foreach (var updatedValue in e.UpdateValues)
                    {
                        var trupi = koka.ColTrupi.FirstOrDefault(x => x.RreshtiId == updatedValue.MerrKeyValue<int>());

                        var trupiNew = updatedValue.MerrCustomUpdatedObject(trupi);
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
                    }
                    else
                    {
                        //modifikon ekzistuese
                        foreach (ASPxDataUpdateValues updatedValue in e.UpdateValues)
                        {
                            var trupi = koka.ColTrupi.FirstOrDefault(x => x.RreshtiId == updatedValue.MerrKeyValue<int>());

                            var trupiNew = updatedValue.MerrCustomUpdatedObject(trupi);
                            trupiNew.DtKrijimi = DateTime.Now;
                            trupiNew.IdKrijuesi = idPerdoruesi;
                            trupiNew.IdStatusDok = koka.IdStatusDok;
                            trupiNew.IdModifikuesi = idPerdoruesi;
                        }

                        mesazhi = isValid(koka, eshteShtim);
                        //merret objekti i vjeter
                        var indexi = col.FindIndex(x => x.IdKokaDok == koka.IdKokaDok);
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
                    mySessionObjects.ruajObjectNeSesion(Session, col, "gvListaEvidencat");
                    mySessionObjects.ruajObjectNeSesion(Session, null, "gvEvidenca");
                }
                else
                {
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, mesazhi.PershkrimMesazhi + ":Red");
                    mySessionObjects.ruajObjectNeSesion(Session, koka.ColTrupi, "gvEvidenca");
                }
                gvEvidenca.DataSource = koka.ColTrupi;
                gvEvidenca.DataBind();

            }
            catch (Exception err)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, err.Message + ":Red");
                throw;
            }

            e.Handled = true;
        }

        private clsKokaEvidencaStatistikore KrijoKokeEvidencaStatistikore(bool eshteShtim)
        {
            clsKokaEvidencaStatistikore koka = null;
            var trupi = mySessionObjects.merrObjectNgaSesioni(Session, "gvEvidenca") as colTrupiEvidencaStatistikore;

            if (!eshteShtim || hfShtimModifikim.Value == "klonim")
            {
                var kokaId = Convert.ToInt32(gvListaEvidencat.GetRowValues(gvListaEvidencat.FocusedRowIndex, "IdKokaDok"));
                koka = new clsKokaEvidencaStatistikore(kokaId);
            }
            else
            {
                koka = new clsKokaEvidencaStatistikore();
            }

            koka.IdNdermarrje = idNdermarrje;
            koka.IdKrijuesi = idPerdoruesi;
            koka.DtKrijimi = DateTime.Now;
            koka.IdNdermVit = idNdermVit;
            koka.TreMujori = Convert.ToInt16(cmbPeriudha.Value);
            koka.GjyqtarPlan = Convert.ToInt32(txtGjyqtarPlan.Text);
            if (trupi.Count > 0)
                koka.ColTrupi = trupi;
            return koka;
        }

        private clsMesazh isValid(clsKokaEvidencaStatistikore koka, bool eshteShtim)
        {
            var mesazhi = new clsMesazh(true);
            if (eshteShtim)
            {
                mesazhi = koka.EkzistonNjeDokumentPerKetePeriudhe();
                if (!mesazhi.Status)
                {
                    return mesazhi;
                }
            }
            if (koka.ColTrupi.Where(x => x.NumriPerfunduar < 0 || x.NumriGjithsej < 0).FirstOrDefault() != null)
                return new clsMesazh(false, "Numri i ceshtjeve gjithsej dhe perfunduar nuk mund te jete negativ!");
            return mesazhi;
        }

        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {
        }

        protected void gvEvidenca_CustomErrorText(object sender, ASPxGridViewCustomErrorTextEventArgs e)
        {
            e.ErrorText = "";
        }
    }
}