using DbCore;
using DbCore.DbAnalizeBuxheti;
using DevExpress.Web;
using System;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using DbCore.DbAdmin;
using DbCore.IMBUtils.Extensions;
using DevExpress.Web.Data;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    public partial class ABPasqyraOrganike : MyPageBase
    {
        private int idNdermarrje;
        private int idKonfig;
        private int idPerdoruesi;
        private int idGjuha;
        private int idViti;
        private int idNdermVit;
        private int idKomponente = 3020;
        private const string komponente = "ABPasqyraOrganike.aspx";
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
                hfState.Set("idNdermVit", idNdermVit);
                hfState.Set("komponente", komponente);
                ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 130, "ABPO", rm, ci, idGjuha);
                cmbKonfigurimi.SelectedIndex = 0;
                DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()));
                hfKonffillestar.Value = String.Format("{0};{1}", konf.KodKonfigAmbjente, konf.PershkrimKonfigAmbjente);

                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, komponente);
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DPlot.ToString();
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);

                idKonfig = int.Parse(cmbKonfigurimi.Value.ToString());

                mbushGridenEPasqyraveNgaDB(idNdermarrje);
                konfiguroGrideListPasqyrash(cmbKonfigurimi.Text);
                mbushGridenPasqyraPerEditim(-1);
                konfiguroGridePasqyraMod();
                
            }
            else
            {
                idNdermarrje = (int)hfState.Get("idNdermarrje");
                idPerdoruesi = (int)hfState.Get("idPerdoruesi");
                idGjuha = (int)hfState.Get("idGjuha");
                idKonfig = int.Parse(cmbKonfigurimi.Value.ToString());
                idViti = (int)hfState.Get("idViti");
                idNdermVit = (int)hfState.Get("idNdermVit");
                
                mbushGridenEPasqyraveNgaSession();
                // GridUtil.PercaktoNgjyrenPerKolonatReadOnly(gvPasqyra, "PBuxhetId", "RreshtiId", "Emertimi", "Totali", "IdNdermarrje", "IdKrijuesi", "IdModifikuesi", "DtKrijimi", "DtModifikimi");
            }
            GridUtil.KtheKolonenNeComboNeGride(gvPasqyra, "IdProfesioni", "Profesioni", ZevendesoKolonenProfesione);
            KonfigurimComboGride.shtoStatuse(gvListaPasqyrat);
            ConfigureAspxComboBox.mbushComboMuaji(cmbMuaji);

            gvListaPasqyrat.PercaktoTitlePanel(this, MenuInfo, pnlMesazhi, hfState, idPerdoruesi, idNdermarrje, idViti, idGjuha, idKonfig, komponente, rm, ci);
            GridUtil.PercaktoTitlePanelPerTrupDokumenti(gvPasqyra, this, MenuInfo, pnlMesazhi, hfState, idPerdoruesi, idNdermarrje, idViti, idGjuha, 1, komponente, idKomponente, "IdTrupiDok", rm, ci,true);
            percaktoTemplateMenu();
        }

        protected void gvPasqyra_DataBound(object sender, EventArgs e)
        {
            gvPasqyra.KeyFieldName = "IdTrupiDok";
           // if (!IsPostBack) ZevendesoKolonenProfesione();
        }
        private void ZevendesoKolonenProfesione(GridViewDataComboBoxColumn cmb)
        {
             object tmp = mySessionObjects.merrObjectNgaSesioni(Session, "pasqyraOrganikeColProfesione");

            DbCore.DbListPagesat.colProfesioneTitujPune col;
            if (tmp != null)
            {
                col = (DbCore.DbListPagesat.colProfesioneTitujPune)tmp;
            }
            else
            {
                col = new DbCore.DbListPagesat.colProfesioneTitujPune();
                col.ktheGjitheProfesioneTitujPunetSipasNdermarjesDheLlojitAktiv(idNdermarrje, 1);
                mySessionObjects.ruajObjectNeSesion(Session, col, "pasqyraOrganikeColProfesione");
            }

            cmb.PropertiesComboBox.DataSource = col;

            ListBoxColumn colprove = new ListBoxColumn();
            colprove.FieldName = "Kodi";
            colprove.Caption = "Kodi";
            ListBoxColumn colemer = new ListBoxColumn();
            colemer.FieldName = "Pershkrimi";
            colemer.Caption = "Pershkrimi";
            colemer.Width = 150;
            cmb.PropertiesComboBox.Columns.Add(colprove);
            cmb.PropertiesComboBox.Columns.Add(colemer);
            cmb.PropertiesComboBox.ValueField = "Id";
            cmb.PropertiesComboBox.TextField = "Pershkrimi";
            cmb.PropertiesComboBox.TextFormatString = "{0}";
            // combo.TextField = "KodKonfigAmbjente";
            cmb.PropertiesComboBox.IncrementalFilteringMode = DevExpress.Web.IncrementalFilteringMode.Contains;
        }
      
  


        public void ZevendesoKolonenMuaji()
        {
            GridViewDataComboBoxColumn colNew = new GridViewDataComboBoxColumn();
            gvListaPasqyrat.Columns.Remove(gvListaPasqyrat.Columns["Muaji"]);
            //  gvPasqyra.Columns.Add(colNew);
            colNew.FieldName = "Muaji";

            colNew.Caption = "Muaji";
            colNew.PropertiesComboBox.Items.Add("Janar", 1);
            colNew.PropertiesComboBox.Items.Add("Shkurt", 2);
            colNew.PropertiesComboBox.Items.Add("Mars", 3);
            colNew.PropertiesComboBox.Items.Add("Prill", 4);
            colNew.PropertiesComboBox.Items.Add("Maj", 5);
            colNew.PropertiesComboBox.Items.Add("Qershor", 6);
            colNew.PropertiesComboBox.Items.Add("Korrik", 7);
            colNew.PropertiesComboBox.Items.Add("Gusht", 8);
            colNew.PropertiesComboBox.Items.Add("Shtator", 9);
            colNew.PropertiesComboBox.Items.Add("Tetor", 10);
            colNew.PropertiesComboBox.Items.Add("Nentor", 11);
            colNew.PropertiesComboBox.Items.Add("Dhjetor", 12);
            colNew.PropertiesComboBox.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            gvListaPasqyrat.Columns.Add(colNew);

        }
        protected void gvListaPasqyrat_DataBound(object sender, EventArgs e)
        {
            gvListaPasqyrat.KeyFieldName = "IdKokaDok";
            if (this.gvListaPasqyrat.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per
                //perzgjidh
                DevExpress.Web.GridViewCommandColumn check = new DevExpress.Web.GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true;
                check.Width = Unit.Percentage(2);
                check.VisibleIndex = 0;
                gvListaPasqyrat.SettingsBehavior.AllowSelectByRowClick = true;
                gvListaPasqyrat.Columns.Add(check);
            }
            
        }

        protected void ButtonOk_Click(object sender, EventArgs e)
        {
            clsMesazh mesazhi = new clsMesazh();
            //mund te behet funksion generic
            try
            {
                if (gvListaPasqyrat.Selection.Count < 1)
                {
                    mesazhi = new clsMesazh(false, "Ju lutem zgjidhni te pakten nje rresht!");
                }
                else
                {
                    object tmp = mySessionObjects.merrObjectNgaSesioni(Session, "gvListaPasqyrat");
                    colKokaPasqyraOrganike col = (tmp as colKokaPasqyraOrganike) ?? new colKokaPasqyraOrganike(idNdermarrje, idNdermVit);
                    var selectedIDs = Array.ConvertAll(gvListaPasqyrat.GetSelectedFieldValues("IdKokaDok").ToArray(), Convert.ToInt32);

                    foreach (int id in selectedIDs)
                    {
                        clsKokaPasqyraOrganike kokaTmp = col.FirstOrDefault(x => x.IdKokaDok == id);
                        kokaTmp.IdModifikuesi = idPerdoruesi;

                        mesazhi = kokaTmp.Fshi();
                        if (mesazhi.Status)
                            col.Remove(kokaTmp);
                        else
                            break;
                    }
                    if (mesazhi.Status)
                    {

                        mySessionObjects.ruajObjectNeSesion(Session, col, "gvListaPasqyrat");
                    }
                }

                if (mesazhi.Status)
                {
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazhi.PershkrimMesazhi, pnlMesazhi);
                    hfStatusVeprimi.Value ="true";
                }
                else
                {
                    hfStatusVeprimi.Value = "false";
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhi.PershkrimMesazhi, pnlMesazhi);
                }
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex.Message);
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhi.PershkrimMesazhi, pnlMesazhi);
                hfStatusVeprimi.Value = "false";
            }
        }
  
        private void mbushGridenEPasqyraveNgaDB(int idNdermarrje)
        {
            colKokaPasqyraOrganike col = new colKokaPasqyraOrganike(idNdermarrje, idNdermVit);
            gvListaPasqyrat.DataSource = col;
            gvListaPasqyrat.DataBind();

            mySessionObjects.ruajObjectNeSesion(Session, col, "gvListaPasqyrat");
        }

        private void mbushGridenPasqyraPerEditim(int idKoka)
        {
            // gvPasqyra.Columns.Clear();
            colTrupiPasqyraOrganike col = new colTrupiPasqyraOrganike(idKoka);
            // col.ShtoObjektBosh();
            gvPasqyra.DataSource = col.ToDataTable();
            gvPasqyra.DataBind();
            mySessionObjects.ruajObjectNeSesion(Session, col, "gvPasqyra");
            //   konfiguroGridePasqyraMod();
        }

        /// <summary>
        /// merr ds e grides nga sessioni
        /// </summary>
        public void mbushGridenEPasqyraveNgaSession()
        {
            object tmp = mySessionObjects.merrObjectNgaSesioni(Session, "gvListaPasqyrat");
            colKokaPasqyraOrganike col = (tmp as colKokaPasqyraOrganike) ?? new colKokaPasqyraOrganike(idNdermarrje, idNdermVit);
            gvListaPasqyrat.DataSource = tmp;
            gvListaPasqyrat.DataBind();
        }

        private void konfiguroGrideListPasqyrash(string kodi)
        {
            ZevendesoKolonenMuaji();
            GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "gvListaPasqyrat", gvListaPasqyrat, kodi, idKomponente.ToString(), idGjuha);
            GridUtil.konfigGrideListeEMadhePaTheme(gvListaPasqyrat, "IdKokaDok");
           
        }

        private void konfiguroGridePasqyraMod()
        {

            GridUtil.PercaktoVisibleColumnsAndTypesGridSipasKodKonfigurimi(idNdermarrje, "gvPasqyra", gvPasqyra, string.Empty, idKomponente.ToString(), idGjuha);
            GridUtil.konfiguroGridaPerBatchEditing(gvPasqyra, true, true, false, false);
            gvPasqyra.SettingsBehavior.AllowSort = false;
            gvPasqyra.SettingsBehavior.AllowDragDrop = false;
            gvPasqyra.SettingsBehavior.AllowGroup = false;
            
           
            gvPasqyra.KeyFieldName = "IdTrupiDok";
            GridUtil.ShtoButtonFshi(gvPasqyra);
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

        protected void gvPasqyra_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            try
            {
                int idkoka = int.Parse(e.Parameters);
                mbushGridenPasqyraPerEditim(idkoka);

                //  gvPasqyra.AddNewRow();
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex.Message);
                throw ex;
            }
        }

        protected void gvPasqyra_BatchUpdate(object sender, DevExpress.Web.Data.ASPxDataBatchUpdateEventArgs e)
        {
            clsMesazh mesazhi = null;
            try
            {

                clsTeDrejtaRoli teDrejta = new clsTeDrejtaRoli();
                teDrejta.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, komponente);

                int idStatusDok = hfStatusDokumenti.Value == "draft" ? 0 : 1;
                bool eshteShtim = hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim";
                object tmp = mySessionObjects.merrObjectNgaSesioni(Session, "gvListaPasqyrat");
                colKokaPasqyraOrganike col = (tmp as colKokaPasqyraOrganike) ?? new colKokaPasqyraOrganike(idNdermarrje, idNdermVit);

                clsKokaPasqyraOrganike koka = KrijoKokePasqyraOrganike(eshteShtim);





                if (eshteShtim)
                {
                    koka.IdStatusDok = idStatusDok;
                    //inicializohet ketu per te shmangur zenien dyfish te memories
                    koka.ColTrupi.Capacity += e.InsertValues.Count;

                    for (int i = 0; i < e.InsertValues.Count - 1; i++)
                    {
                        clsTrupiPasqyraOrganike trup = new clsTrupiPasqyraOrganike
                        {
                            IdKrijuesi = idPerdoruesi,
                            DtKrijimi = DateTime.Today,
                            IdStatusDok = idStatusDok
                        };
                        trup = e.InsertValues[i].MerrCustomInsertedObject(trup);
                        if (trup.IdProfesioni != 0)//nuk merr rreshtat bosh
                            koka.ColTrupi.Add(trup);
                    }
                    //modifikon ekzistuese
                    foreach (ASPxDataUpdateValues updateded in e.UpdateValues)
                    {
                        clsTrupiPasqyraOrganike trupi = koka.ColTrupi.FirstOrDefault(x => x.IdTrupiDok == updateded.MerrKeyValue<int>());

                        clsTrupiPasqyraOrganike trupiNew = updateded.MerrCustomUpdatedObject(trupi);
                        trupiNew.DtKrijimi = DateTime.Now;
                        trupiNew.IdKrijuesi = idPerdoruesi;
                        trupiNew.IdStatusDok = koka.IdStatusDok;
                        trupiNew.IdModifikuesi = idPerdoruesi;
                    }
                    //heq nga collectioni rreshtat e fshire
                    for (int i = 0; i < e.DeleteValues.Count; i++)
                    {
                        koka.ColTrupi.RemoveAll(x => x.IdTrupiDok == e.DeleteValues[i].MerrKeyValue<int>());
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
                        koka.IdStatusDok = idStatusDok;
                        // koka.ColTrupi.Capacity = e.InsertValues.Count + e.UpdateValues.Count - e.DeleteValues.Count;
                        //merr vlerat e reja
                        for (int i = 0; i < e.InsertValues.Count - 1; i++)//hiqet rreshti bosh
                        {
                            clsTrupiPasqyraOrganike trup = new clsTrupiPasqyraOrganike
                            {
                                IdKrijuesi = idPerdoruesi,
                                DtKrijimi = DateTime.Today,
                                IdStatusDok = idStatusDok
                            };
                            trup = e.InsertValues[i].MerrCustomInsertedObject(trup);
                            if (trup.IdProfesioni != 0)//nuk merr rreshtat bosh
                                koka.ColTrupi.Add(trup);
                        }
                        //modifikon ekzistuese
                        for (int i = 0; i < e.UpdateValues.Count; i++)
                        {
                            clsTrupiPasqyraOrganike trupi = koka.ColTrupi.Where(x => x.IdTrupiDok == e.UpdateValues[i].MerrKeyValue<int>()).FirstOrDefault();

                            clsTrupiPasqyraOrganike trupiNew = e.UpdateValues[i].MerrCustomUpdatedObject(trupi);
                            trupiNew.DtKrijimi = DateTime.Now;
                            trupiNew.IdKrijuesi = idPerdoruesi;
                            trupiNew.IdStatusDok = koka.IdStatusDok;
                            trupiNew.IdModifikuesi = idPerdoruesi;
                        }
                        //heq nga collectioni rreshtat e fshire
                        for (int i = 0; i < e.DeleteValues.Count; i++)
                        {
                            koka.ColTrupi.RemoveAll(x => x.IdTrupiDok == e.DeleteValues[i].MerrKeyValue<int>());
                        }
                        mesazhi = isValid(koka, eshteShtim);
                        //merret objekti i vjeter
                        int indexi = col.FindIndex(x => x.IdKokaDok == koka.IdKokaDok);
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
                    mySessionObjects.ruajObjectNeSesion(Session, col, "gvListaPasqyrat");
                    mySessionObjects.ruajObjectNeSesion(Session, null, "gvPasqyra");
                }
                else
                {
                    //e ruajme ne session qe ta marrim heren tjeter
                    mySessionObjects.ruajObjectNeSesion(Session, koka.ColTrupi, "gvPasqyra");
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, mesazhi.PershkrimMesazhi + ":Red");
                }

                gvPasqyra.DataSource = koka.ColTrupi.ToDataTable();
                gvPasqyra.DataBind();

            }
            catch (Exception err)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, err.Message + ":Red");
                //  throw;
            }
            e.Handled = true;
        }

        private clsKokaPasqyraOrganike KrijoKokePasqyraOrganike(bool eshteShtim)
        {
            clsKokaPasqyraOrganike koka = null;
            colTrupiPasqyraOrganike trupi = mySessionObjects.merrObjectNgaSesioni(Session, "gvPasqyra") as colTrupiPasqyraOrganike;

            if (!eshteShtim || hfShtimModifikim.Value == "klonim")
            {
                int kokaId = Convert.ToInt32(gvListaPasqyrat.GetRowValues(gvListaPasqyrat.FocusedRowIndex, "IdKokaDok"));
                koka = new clsKokaPasqyraOrganike(kokaId);
            }
            else
            {
                koka = new clsKokaPasqyraOrganike();
            }

            koka.IdNdermarrje = idNdermarrje;
            koka.IdKrijuesi = idPerdoruesi;
            koka.DtKrijimi = DateTime.Now;
            koka.IdNdermVit = idNdermVit;
            koka.DtDok = txtDate.Date;
            koka.NrDok = txtNrDok.Text;
            koka.Muaji = Convert.ToInt16(cmbMuaji.Value);
            koka.TotaliFemra = 0;
            koka.TotaliMeshkuj = 0;
          
            if (trupi.Count >0)
                koka.ColTrupi = trupi;
            
            return koka;
        }


        private clsMesazh isValid(clsKokaPasqyraOrganike koka, bool eshteShtim)
        {
            clsMesazh mesazhi = new clsMesazh(true);
            if (eshteShtim)
            {
                mesazhi = koka.EkzistonNjeDokumentMeKeteNumer();
                if (!mesazhi.Status)
                {
                    return mesazhi;
                }
            } if (koka.ColTrupi.Select(x => x.IdProfesioni).Distinct().Count() < koka.ColTrupi.Count)
                return new clsMesazh(false, "I njejti profesion gjendet dy here ne trupin e dokumentit!");
            if (koka.ColTrupi.FirstOrDefault(x=>x.VleraFakt < 0||x.VleraPlan<0)!=null)
                return new clsMesazh(false, "Nuk mund te vendosen vlera negative per vlerat plan dhe fakt!");
            if(koka.ColTrupi.Count==0)
                return new clsMesazh(false, "Nuk mund te ruhet dokumenti me trup bosh!");

            return mesazhi;
        }

        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {
        }

        protected void gvPasqyra_CustomErrorText(object sender, ASPxGridViewCustomErrorTextEventArgs e)
        {
            e.ErrorText = "";
            
        }

        protected void gvListaPasqyrat_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            //if(e.NewValues["IdProfesioni"]==null)
            //    gvPasqyra.DeleteRow()
        }
    }
}