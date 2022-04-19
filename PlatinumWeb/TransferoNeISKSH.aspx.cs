using DbCore;
using DbCore.DbAccessIntegration;
using DbCore.DbAccessIntegration.DbISKSH;
using DbCore.DbAdmin;
using DbCore.DbRegjistrim;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Extensions;
using DevExpress.Web;
using Newtonsoft.Json;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Filters;
using PlatinumWeb.ApplicationUtils.Pages;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Web.UI;

namespace PlatinumWeb
{
    public partial class TransferoNeISKSH : MyPageBase
    {

        //private string guidString;
        private string _komponente => DbCore.clsFunksione.GetKomponente(Page.Request);
        //private int idPerdoruesi;
        //private int idGjuha;
        //private int idNdermarrje;
        //private int idViti;
        //private CultureInfo ci;
        //private ResourceManager rm => new ResourceManager("Resources.Strings", Assembly.Load("App_GlobalResources"));

        private string filterExpressionDefault = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            base.ShtoMenuControlsDheMsgFrame();
            if (!IsPostBack)
            {
                if (!DbCore.mySessionObjects.isLogedIn(Session)) DbCore.clsFunksione.logout(Session, true, "FaqePaautorizuar");
                MbushKomboKategoria();
                ShtoVleraTePergjithshmeNeHfState();
                colTeDrejtaRoli teDrejtaNiveleRregjistrimi = new colTeDrejtaRoli();
                teDrejtaNiveleRregjistrimi.mbushTeDrejtaPerdoruesiNiveleRegjistrimiPerKomponente(IdPerdoruesi, IdNdermarrja, IdViti, _komponente);
                
                ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(IdPerdoruesi, IdNdermarrja, cmbKonfigurimi, 206, "TFISKSH", rm, ci, IdGjuha);
                cmbKonfigurimi.SelectedIndex = 0;
                DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()), IdGjuha);

                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaDokumenta = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaDokumenta.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, _komponente);

                mbushGride(false, Convert.ToInt32(cmbKategoria.Value), dataNga.Text, dataDeri.Text, IdNdermarrja);

                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(IdNdermarrja, gv_TransferoNeISKSH.ID, gv_TransferoNeISKSH, cmbKonfigurimi.Text.Split(';')[0], "3097", IdGjuha, true, 206, true);
            }
            else
            {
                mbushGride(true, Convert.ToInt32(cmbKategoria.Value), dataNga.Text, dataDeri.Text, IdNdermarrja);
            }

            clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, IdNdermarrja, "gv_TransferoNeISKSH", int.Parse(cmbKonfigurimi.Value.ToString()), "TransferoNeISKSH.aspx");
            konfiguroGride(IdGjuha, rm, ci);            
            if (String.IsNullOrEmpty(gv_TransferoNeISKSH.FilterExpression) && !IsPostBack)
                gv_TransferoNeISKSH.FilterExpression = filterExpressionDefault;
            gv_TransferoNeISKSH.PercaktoTitlePanel(this, _menuInfo, _pnlMesazhi, hfState, IdPerdoruesi, IdNdermarrja, IdViti, IdGjuha, int.Parse(cmbKonfigurimi.Value.ToString()), _komponente, rm, ci, true);
        }
        

        private void konfiguroGride(int idGjuha, ResourceManager rm, CultureInfo ci)
        {
            if (gv_TransferoNeISKSH.Columns.Count != 0)
                gv_TransferoNeISKSH.Columns["#"].VisibleIndex = 0;
            gv_TransferoNeISKSH.SettingsPager.PageSize = 20;

            int idKategoria = Convert.ToInt32(cmbKategoria.Value);

            GridUtil.konfigGrideListeEMadhePaTheme(gv_TransferoNeISKSH, "IDSHITJEKOKA");
            GridViewDataDateColumn colDtDok = gv_TransferoNeISKSH.Columns["DTDOK"] as GridViewDataDateColumn;
            colDtDok.PropertiesEdit.DisplayFormatString = "dd/MM/yyyy";
            colDtDok.PropertiesDateEdit.EditFormatString = "dd/MM/yyyy";
        }

        private void mbushGride(bool ngaSessioni, int idKatDok, string dataNga, string dataDeri, int idNdermarrje)
        {
            DataTable dt = new DataTable();
            bool gjetur = false;
            if (ngaSessioni)
                gjetur = mySessionObjects.merrGrideNgaSessioni(_komponente + idKatDok.ToString() + dataNga + dataDeri, Session, out dt);
            if (!gjetur)
                dt = DbCore.DbRegjistrim.colKokaShitje.merrKokaShitjePerTransferimNeISKSH(idKatDok, dataNga, dataDeri, IdNdermarrja);
            if (dt == null)
            {
                clsMenuInfo.ShtoMesazhGabimi(_menu, "Ndodhi nje problem gjate leximit te dokumentave! Ju lutem riperserisni veprimin!", _pnlMesazhi);
                return;
            }
            gv_TransferoNeISKSH.DataSource = dt;
            mySessionObjects.ruajGrideNeSession(_komponente + idKatDok.ToString() + dataNga + dataDeri, Session, dt);
            gv_TransferoNeISKSH.DataBind();
            dt.Dispose();

            if (!IsCallback && Request["__CALLBACKID"] == "gv_TransferoNeISKSH")
                gv_TransferoNeISKSH.RestoreFilter(IdNdermarrja);
            gv_TransferoNeISKSH.SaveFilter(IdNdermarrja);
            
        }

        private void MbushKomboKategoria()
        {
            cmbKategoria.Items.Add("Shitje", 1);
            cmbKategoria.Items.Add("Blerje", 2);
            //cmbKategoria.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            cmbKategoria.DataBind();
        }
        
        protected void PercaktoTemplateMenu(ASPxMenu menu, ASPxMenu menuInfo)
        {
            clsToolbarConfig.percaktoTemplateMenu(IdGjuha, IdViti, IdPerdoruesi, IdNdermarrja, menu, _komponente, this, menuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, null, null, true, true, false, Meme, false);
        }

        protected void Menu_ItemClick(object source, MenuItemEventArgs e)
        {
            switch (e.Item.Name)
            {
                case "Ngarko":
                    hfKaGabime.Value = "false";
                    mbushGride(false, Convert.ToInt32(cmbKategoria.Value), dataNga.Text, dataDeri.Text, IdNdermarrja);
                    break;
                case "Transfero":
                    TransferoKontrolloFaturaNeISKSH(true);
                    break;
                case "Kontrollo":
                    TransferoKontrolloFaturaNeISKSH(false);
                    break;
            }
        }

        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
            menu_msg_Frame.RuajFilter(gv_TransferoNeISKSH, _komponente, int.Parse(cmbKonfigurimi.Value.ToString()), ref hfStatusi);
        }

        protected void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
            menu_msg_Frame.FshiFilter(gv_TransferoNeISKSH, _komponente, int.Parse(cmbKonfigurimi.Value.ToString()), ref hfStatusi);
        }

        protected void gv_TransferoNeISKSH_DataBound(object sender, EventArgs e)
        {

            ASPxGridView grid = (ASPxGridView)sender;
            GridUtil.ShtoCommandColumnNeDatabound(grid, "#", "IDSHITJEKOKA");
        }

        protected void gv_TransferoNeISKSH_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            GridUtil.GridHeaderFilterFillItem(sender, e, rm, ci, "Numer dokumenti;Lloj dokumenti;Dege administrative");
        }

        protected void gv_TransferoNeISKSH_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            GridUtil.GridCustomCallbackDefault(sender, e, gv_TransferoNeISKSH, _komponente, IdNdermarrja, IdGjuha, cmbKonfigurimi, ref hfStatusi);
            var arr = e.Parameters.Split(';');
            if (arr.Length == 1 && arr[0] == "true")
                mbushGride(true, Convert.ToInt32(cmbKategoria.Value), dataNga.Text, dataDeri.Text, IdNdermarrja);
        }

        protected void gv_TransferoNeISKSH_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            GridUtil.GridAfterPerformCallback(sender, e, gv_TransferoNeISKSH, _menu);
            GridUtil.ToolTipButonaveMbiGride(gv_TransferoNeISKSH, ci, rm);
        }

        protected void gv_TransferoNeISKSH_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gv_TransferoNeISKSH.PageIndex;
            e.Properties["cpPageRow"] = gv_TransferoNeISKSH.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gv_TransferoNeISKSH.VisibleRowCount;
        }

        protected void btnXlsxExport_Click_Hidden(object sender, EventArgs e)
        {
            try
            {
                System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                CultureInfo ci = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo((int)hfState["idGjuha"]);
                gridExport.WriteXlsxToResponse(rm.GetString("msgFaturat", ci), true);
            }
            catch (Exception err)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
            }
        }

        private void TransferoKontrolloFaturaNeISKSH(bool transfero)
        {
            List<object> idTe = gv_TransferoNeISKSH.GetSelectedFieldValues("IDSHITJEKOKA");
            if (idTe.Count == 0)
                idTe = ((DataTable)gv_TransferoNeISKSH.DataSource).AsEnumerable().Select(row => row.Field<object>("IDSHITJEKOKA")).ToList<object>();

            if (idTe.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(_menuInfo, $"Ju nuk keni asnje dokument per te {(transfero ? "transferuar" : "kontrolluar")}.", _pnlMesazhi);
                return;
            }

            if (Convert.ToDateTime(dataNga.Value) > Convert.ToDateTime(dataDeri.Value))
            {
                clsMenuInfo.ShtoMesazhGabimi(_menuInfo, $"Data e fillimit nuk duhet te jete me e madhe se data e mbarimit.", _pnlMesazhi);
                return;
            }

            DataTable faturatFull = colKokaShitje.merrKokaShitjeMeTrupPerTransferimNeISKSH(string.Join(",", idTe));

            DataTable kokeTrup = null;
            DataRow koka = null;
            DataTable trupi = null;
            DataTable gabime = new DataTable();
            gabime.Columns.Add("Kodi");
            gabime.Columns.Add("Gabimi");
            gabime.Columns.Add("Rreshti");
            clsMesazh mesazh;
            DbAccess dbAccess = new DbAccess(@""+ txtPathDbAksesi.Text + "");
            ClsIntegrimFaturaISKSH integrimFaturash = new ClsIntegrimFaturaISKSH();
            EnumLlojVeprimiISKSH veprimi = Convert.ToInt32(cmbKategoria.Value) == 1 ? EnumLlojVeprimiISKSH.Shitje : EnumLlojVeprimiISKSH.Blerje;
            int i = 1;
            if (transfero)
            {
                mesazh = integrimFaturash.FshiFaturaISKSH(veprimi, Convert.ToDateTime(dataNga.Value), Convert.ToDateTime(dataDeri.Value), ref dbAccess);
                if (!mesazh)
                {
                    clsMenuInfo.ShtoMesazhGabimi(_menuInfo, mesazh.PershkrimMesazhi, _pnlMesazhi);
                    return;
                }
            }
            foreach (var id in idTe)
            {
                try
                {
                    int idKoka = Convert.ToInt32(id);
                    kokeTrup = faturatFull.Select($"IDSHITJEKOKA = {idKoka}").CopyToDataTable();
                    koka = (new DataView(kokeTrup)).ToTable(false, "NrDok", "Data", "DataRegjistrimit", "KodiFurnitorit", "KodiBleresit", "Emertimi", "Adresa", "NiptFurnitori", "NiptBleresi", "NrLicences", "Shuma", "NrTotaliBarnave", "Shenime", "RefKompjuterikePerFaturaShitjejeERuajtur", "FatureOsePasqyre", "Skonto").Rows[0];
                    trupi = (new DataView(kokeTrup)).ToTable(false, "KodiBarit", "Sasia", "FormaShitjes", "NJESIATJETER", "KoeficientiTransformimit", "CmimiPerNjesi", "NrSerise");
                    if (transfero)
                        mesazh = integrimFaturash.Transfero(koka, trupi, veprimi, ref dbAccess);
                    else
                        mesazh = integrimFaturash.Kontrollo(koka, trupi, veprimi, ref dbAccess);

                    if (!mesazh)
                        gabime.AddRow(Convert.ToString(koka["NrDok"]), $"Date Dokumenti {Convert.ToDateTime(koka["Data"]).ToShortDateString()} - {mesazh.PershkrimMesazhi}" , i);
                    i++;
                }
                catch(MyException ex)
                {
                    gabime.AddRow(Convert.ToString(koka["NrDok"]), $"Date Dokumenti { Convert.ToDateTime(koka["Data"]).ToShortDateString()} - { ex.Message}", i);
                    i++;
                }
            }
            mySessionObjects.ruajTabeleGabimeshImporti(Session, gabime);
            if (gabime.Rows.Count > 0)
            {
                hfKaGabime.Value = "true";
                if(!transfero)
                    clsMenuInfo.ShtoMesazhGabimi(_menuInfo, $"Kontrolli nuk kaloi me sukses. Kontrolloni listen e gabimeve per me shume detaje!", _pnlMesazhi);
                else
                    clsMenuInfo.ShtoMesazhGabimi(_menuInfo, $"U transferuan {idTe.Count - gabime.Rows.Count} rreshta dhe deshtuan {gabime.Rows.Count} rreshta. Kontrolloni listen e gabimeve per me shume detaje!", _pnlMesazhi);
                return;
            }
            if (!transfero)
                clsMenuInfo.ShtoMesazhSuksesi(_menuInfo, "Kontrollet u kaluan me sukses!", _pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhSuksesi(_menuInfo, "Transferimi u krye me sukses!", _pnlMesazhi);
        }
    }
}