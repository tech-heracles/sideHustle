using DevExpress.Web;
using PlatinumWeb.Templates;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Resources;
using System.Web.UI;
using System.Web.UI.WebControls;
using DbCore;
using DbCore.DbRegjistrim;
using Newtonsoft.Json;
using System.Linq;
using AlphaWebCommon.WebUtils.ASPxControlExtensions;
using DbCore.DbAdmin;
using DbCore.DbInventari;
using DevExpress.Web.Data;
using DbCore.DbShare;
using PlatinumWeb.ApplicationUtils.Pages;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Logging;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
namespace PlatinumWeb
{
    public partial class Shto_RecetaOptike : MyPageBase
    {

        private int idViti;
        private int idNdermarrje;
        private int idNdermVit;
        private const string emerKomponente = "Shto_RecetaOptike.aspx";
        private const string emerGrideRecetaTrupi = "gvRecetaTrupi";
        private const string idKomponente = "3058";
        private string ShtimModifikim
        {
            get { return hfShtimModifikim.Value; }
            set { hfShtimModifikim.Value = value; }
        }
        private int Id
        {
            get { return hfState.Get<int>("Id"); }
            set { hfState.Set("Id", value); }

        }
        private string GuidString
        {
            get { return hfState.Get<string>("GuidString"); }
        }
        /// <summary>
        /// thirret kur faqja lodohet. Ne te behet kontrolli nese perdoruesi eshte i loguar ne sistem  dhe nqs jo ridrejtohet tek forma e logimit
        /// thirret inicializimi i konfigurimeve fillestare te faqes
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e"> argumenti</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!mySessionObjects.isLogedIn(Session))
            {
                clsFunksione.logout(Session, true, "FaqePaautorizuar");
            }
            if (mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + IdPerdoruesi);
            }
            if (!Page.IsPostBack)
            {
                idNdermVit = mySessionObjects.ktheNdermarrjeVit(Session);
                idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
                idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
                hfState.Set("IdPerdoruesi", IdPerdoruesi);
                hfState.Set("IdGjuha", IdGjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idNdermarrjeVit", idNdermVit);
                hfState.Set("GuidString", Convert.ToBase64String(Guid.NewGuid().ToByteArray()));
                Id = Request.QueryString["Id"] == null ? 0 : Convert.ToInt32(Request.QueryString["Id"]);
                ShtimModifikim = Id == 0 ? "shtim" : "modifikim";
                var tedrejtaInfo = new clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, idNdermarrje, idViti, "Konfigurim Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                konfiguroVleraFillestare(IdPerdoruesi, idNdermarrje, IdGjuha, rm, ci);
                if (ShtimModifikim == "shtim")
                {
                    VendosVleraFillestareNeGride();
                } else
                {
                    MbushKontrolletSipasDokumentit();
                }
                konfiguroGride_gvRecetaTrupi(ci);
                konfiguroGrideRO();
                GridUtil.PercaktoBandPerKolona(gvRecetaTrupi, "Syri i majte", "IdArtikulliSyriMajte", "ShenimeSyriMajte");
                GridUtil.PercaktoBandPerKolona(gvRecetaTrupi, "Syri i djathte", "IdArtikulliSyriDjathte", "ShenimeSyriDjathte");


            }
            else
            {
                idNdermarrje = hfState.Get<int>("idNdermarrje");
                idNdermVit = hfState.Get<int>("idNdermarrjeVit");

                konfiguroGride_gvRecetaTrupi(ci);
            }
            percaktoTemplateMenu(idViti, IdPerdoruesi, idNdermarrje, ASPxMenu1);
            percaktoTemplate();
            Container.Attributes["src"] = "";
            Container2.Attributes["src"] = "";
        }

        private void MbushKontrolletSipasDokumentit()
        {
            var koka = new clsKokaRecetaOptike(Id);
            mbushGridenEFazaveTePunimeve(new colTrupiRecetaOptikePunime(Id));
            mbushGridenMeFushatRO(new colTrupiRecetaOptikeSyri(Id));
            txtNumri.Text = koka.NrDok;
            txtNrSerial.Text = koka.NrSerial;
            if (koka.IdKlient != 0)
            {
                ConfigureAspxComboBox.KonfiguroComboBoxKlientFurnitoriById(IdPerdoruesi, idNdermarrje, cmbKlienti, koka.IdKlient);
                cmbKlienti.SelectedIndex = 0;
            }
            txtAdresa.Text = koka.Adresa;
            dteDateDokumenti.Date = koka.DtDok;
            dteDateRegj.Date = koka.DtRegj;
            txtShenime.Text = koka.Shenime;
            txtDIAfer.Text = koka.DiAfer;
            txtDILarg.Text = koka.DiLarg;
            txtReferimi.Text = koka.Referimi;
            txtLartesia.Text = koka.Lartesia;
            hfStatusDokumenti.Value = koka.IdStatusDok == 0 ? "Draft" : "Ruaj";
            clsRaportDesign design = new clsRaportDesign(Convert.ToInt32(cmbFormatPrintimi.Value));
            gvRecetaTrupi.JSProperties["cpIdKoka"] = koka.IdStatusDok == 1 ? koka.IdKoka : 0;
            gvRecetaTrupi.JSProperties["cpIdRaporti"] = design.IdRaporti;
        }


        private void konfiguroVleraFillestare(int IdPerdoruesi, int idNdermarrje, int IdGjuha, ResourceManager rm, CultureInfo cultinf)
        {
            ConfigureAspxComboBox.KonfiguroComboBoxNiveletSipasKategoriDtCombo(cmbNiveli, idNdermarrje, IdPerdoruesi, 160, true, true,false);
            ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(IdPerdoruesi, idNdermarrje, cmbKonfigurimi, 160, cmbNiveli.Text, rm, cultinf, IdGjuha);
            if (cmbKonfigurimi.Items.Count > 0)
                cmbKonfigurimi.SelectedItem = cmbKonfigurimi.Items[0];
            ConfigureAspxComboBox.ShtoKolonaPerKf(cmbKlienti);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmbKlienti);
            ConfigureAspxComboBox.mbushComboFormatPrintimi(cmbFormatPrintimi, 160, idNdermarrje);
            if (cmbFormatPrintimi.Items.Count > 0)
                cmbFormatPrintimi.SelectedIndex = 0;

        }
        private void konfiguroGrideRO()
        {
            GridUtil.percaktoVisibleColumnsMeWidth(IdGjuha, idNdermarrje, gvFushatRO, "gvFushatRO", emerKomponente);
            GridUtil.konfiguroGrideRegjistrimEvogelPaTheme(gvFushatRO, "IdFusha", false);
            
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
            clsToolbarConfig.percaktoTemplateMenu(IdGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, emerKomponente, this, MenuInfo, null, null, hfShtimModifikim.Value == "modifikim" ? false : true, true, false, mySessionObjects.merrEshteMemeSesioni(Session));
        }


        #region fushatRecetaOptike

        private void mbushGridenMeFushatRO(colTrupiRecetaOptikeSyri col)
        {
            gvFushatRO.DataSource = col;
            gvFushatRO.DataBind();
            mySessionObjects.RuajNeSession<colTrupiRecetaOptikeSyri>(Session, col, $"{GuidString}_trupiRecetaOptikeSyri");
        }




        private void percaktoTemplate()
        {

            var col1 = gvFushatRO.Columns["IdFusha"] as GridViewDataColumn;
            col1.DataItemTemplate = new MyLabelTemplate();
            col1.VisibleIndex = 0;
            col1.Width = 20;
            var colFusha = gvFushatRO.Columns["Fusha"] as GridViewDataColumn;
            colFusha.VisibleIndex = 1;
            colFusha.DataItemTemplate = new MyLabelTemplate();
            var col2 = gvFushatRO.Columns["SyriMajte"] as GridViewDataColumn;
            col2.VisibleIndex = 2;
            col2.DataItemTemplate = new MyComboTemplate();
            var col3 = gvFushatRO.Columns["SyriDjathte"] as GridViewDataColumn;
            col3.VisibleIndex = 3;
            col3.DataItemTemplate = new MyComboTemplate();

        }


        protected void gvFushatRO_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType != GridViewRowType.Data)
                return;
            
            var lblFusha = merrTemplateNgaGridaPerKeteRresht((ASPxGridView)sender, "IdFusha", "lbl", e.VisibleIndex) as ASPxLabel;
            var cmbSyriMajte = merrTemplateNgaGridaPerKeteRresht((ASPxGridView)sender, "SyriMajte", "cmbBox", e.VisibleIndex) as ASPxComboBox;
            var cmbSyriDjathte = merrTemplateNgaGridaPerKeteRresht((ASPxGridView)sender, "SyriDjathte", "cmbBox", e.VisibleIndex) as ASPxComboBox;
            var fusha = merrTemplateNgaGridaPerKeteRresht((ASPxGridView)sender, "Fusha", "lbl", e.VisibleIndex) as ASPxLabel;
            if (fusha == null)
                return;
            lblFusha.ClientInstanceName = $"IdFusha{e.VisibleIndex}";
            cmbSyriMajte.ClientInstanceName = $"SyriMajte{e.VisibleIndex}";
            cmbSyriDjathte.ClientInstanceName = $"SyriDjathte{e.VisibleIndex}";
            if (fusha.Text == "Artikulli Kryesor")
            {
                ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmbSyriMajte, cmbSyriDjathte);
                cmbSyriMajte.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedArtikulli(SyriMajte" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + " ); }";
                cmbSyriDjathte.ClientSideEvents.ButtonClick = "function(s,e){ ButtonClickedArtikulli(SyriDjathte" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + " ); }";
                ConfigureAspxComboBox.mbushComboArtikulli(IdPerdoruesi, idNdermarrje, rm.GetString("postStringTvsh", ci), MerrDataSourceKolonaArtikulli(false), cmbSyriMajte, cmbSyriDjathte);

            }
            else
            {
                cmbSyriDjathte.DropDownButton.Visible = false;
                cmbSyriMajte.DropDownButton.Visible = false;
                cmbSyriDjathte.DropDownStyle = DropDownStyle.DropDown;
                cmbSyriMajte.DropDownStyle = DropDownStyle.DropDown;
            }
            

        }

        protected Control merrTemplateNgaGridaPerKeteRresht(ASPxGridView grida, string fieldName, string templateName, int Rreshti)
        {
            var col = grida.Columns[fieldName] as GridViewDataColumn;
            return grida.FindRowCellTemplateControl(Rreshti, col, templateName);
        }

        #endregion


        #region recetaTrupiPunime

        private void konfiguroGride_gvRecetaTrupi(CultureInfo ci)
        {
            GridUtil.PercaktoVisibleColumnsAndTypesGridSipasKodKonfigurimi(idNdermarrje, emerGrideRecetaTrupi, gvRecetaTrupi, cmbKonfigurimi.Text, idKomponente, IdGjuha);
            gvRecetaTrupi.KonfiguroCombo("IdArtikulliSyriMajte", "IdArtikulli", "KodArtikulli", () => MerrDataSourceKolonaArtikulli(true), Session, emerKomponente, string.Empty);
            gvRecetaTrupi.KonfiguroCombo("IdArtikulliSyriDjathte", "IdArtikulli", "KodArtikulli", () => MerrDataSourceKolonaArtikulli(true), Session, emerKomponente, string.Empty);
            GridUtil.ShtoButtonFshi(gvRecetaTrupi);
            GridUtil.konfiguroGridaPerBatchEditing(gvRecetaTrupi, true, true, false, false, 20, true, false, false);
            gvRecetaTrupi.SettingsBehavior.AllowSort = false;
            gvRecetaTrupi.SettingsBehavior.AllowDragDrop = false;
            gvRecetaTrupi.SettingsBehavior.AllowGroup = false;
            GridUtil.PercaktoTitlePanelPerTrupDokumenti(gvRecetaTrupi, this, MenuInfo, pnlMesazhi, hfState, IdPerdoruesi, idNdermarrje, idViti, IdGjuha, 1, emerKomponente, 3058, "IdTrupi", rm, ci, true);
        }

        private DataTable MerrDataSourceKolonaArtikulli(bool sherbim)
        {
            var dt = mySessionObjects.MerrNgaSession<DataTable>(Session, $"{GuidString}_DataSourceArtikulli");
            if(dt == null)
            {
                dt = colArtikujt.merrSipasArtikujAktivNdermarrjesAndAutorizimeDT(idNdermarrje, IdPerdoruesi);
                mySessionObjects.RuajNeSession<DataTable>(Session, dt, $"{GuidString}_DataSourceArtikulli");
            }
            if(!sherbim)
                return dt;
            return dt.Select("Klasa = 3").GetDataTable(dt);
        }

        private void mbushGridenEFazaveTePunimeve(colTrupiRecetaOptikePunime col)
        {
           
            gvRecetaTrupi.DataSource = col;
            gvRecetaTrupi.DataBind();
            mySessionObjects.RuajNeSession<colTrupiRecetaOptikePunime>(Session, col, $"{GuidString}_trupiRecetaOptikePunime");
        }

        

        protected void gvRecetaTrupi_DataBound(object sender, EventArgs e)
        {

            if (gvRecetaTrupi.Columns["#"] != null)
                return;

            gvRecetaTrupi.KeyFieldName = "IdTrupi";
            GridUtil.PercaktoNgjyrenPerKolonatReadOnly(gvRecetaTrupi, "NrFaza");

        }
        #endregion




        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {

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

            }
        }


        protected void gvFushatRO_DataBound(object sender, EventArgs e)
        {
            gvFushatRO.KeyFieldName = "IdFusha";
        }

        protected void gvFushatRO_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpNoPage"] = gvFushatRO.PageIndex;
            e.Properties["cpNoRows"] = gvFushatRO.VisibleRowCount;
        }


        private void VendosVleraFillestareNeGride()
        {
            ShtimModifikim = "shtim";
            dteDateDokumenti.Date = DateTime.Now;
            dteDateRegj.Date = DateTime.Now;
            mbushGridenMeFushatRO(new colTrupiRecetaOptikeSyri());
            mbushGridenEFazaveTePunimeve(new colTrupiRecetaOptikePunime());
        
        }
        #region RUAJTJE

        protected void gvRecetaTrupi_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            switch (e.Parameters)
            {
                case "Pastro":
                    VendosVleraFillestareNeGride();
                    break;
                case "Ruaj":
                    Ruaj();
                    break;

            }

        }
        private void Ruaj()
        {
            try
            {
                clsTeDrejtaRoli teDrejta = new clsTeDrejtaRoli();
                teDrejta.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, idNdermarrje, idViti, emerKomponente);

                int idStatusDok = hfStatusDokumenti.Value == "Draft" ? 0 : 1;
                if (ShtimModifikim == "shtim" && ((idStatusDok == 0 && !teDrejta.DShtimDraft) || (idStatusDok == 1 && !teDrejta.DShtim)))
                {
                    gvRecetaTrupi.ShtoMesazhNeGride(new clsMesazh(false, "Ju nuk keni te drejta te shtoni Receta!"));
                    return;
                }
                if (ShtimModifikim == "modifikim" && !teDrejta.DMod)
                {
                    gvRecetaTrupi.ShtoMesazhNeGride(new clsMesazh(false, "Ju nuk keni te drejta te modifikoni Receta!"));
                    return;
                }


                clsKokaRecetaOptike koka = krijoKokaRecetaOptike(idStatusDok);
                koka.ColTrupiSyri = mbushTrupiRecetasyri();
                koka.ColTrupiPunime = mySessionObjects.MerrNgaSession<colTrupiRecetaOptikePunime>(Session, $"{GuidString}_trupiRecetaOptikePunime");

                var mesazh = ShtimModifikim == "shtim" ? koka.Ruaj() : koka.Modifiko();
                if (mesazh)
                {
                    VendosVleraFillestareNeGride();

                    clsRaportDesign design = new clsRaportDesign(Convert.ToInt32(cmbFormatPrintimi.Value));
                    gvRecetaTrupi.JSProperties["cpIdKoka"] = koka.IdStatusDok == 1 ? koka.IdKoka : 0;
                    gvRecetaTrupi.JSProperties["cpIdRaporti"] = design.IdRaporti;
                }
                else
                {
                    mbushGridenEFazaveTePunimeve(koka.ColTrupiPunime);
                }
                gvRecetaTrupi.ShtoMesazhNeGride(mesazh);
              
            }

            catch (Exception ex)
            {

                gvRecetaTrupi.ShtoMesazhNeGride(new clsMesazh(false, "Nje gabim i papritur ka ndodhur"));
                ImbLogger.Error(ex);
            }

        }
        protected void gvRecetaTrupi_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
        {

            merrNdryshimeRecetaOptikePunimeNgaGrida(e);

        }


        private clsKokaRecetaOptike krijoKokaRecetaOptike(int idstatusdok)
        {
            var koka =ShtimModifikim == "shtim" ? new clsKokaRecetaOptike(hfNrAuto, mySessionObjects.merrPeriudheKontabel(Session), rm, ci) : new clsKokaRecetaOptike(Id);

            hfNrAuto = (ASPxHiddenField)NrAuto.VendosVleratNrAuto(hfNrAuto, this.GetAsPxTextEditIdValue());
            //DbCore.DbAdmin.NrAuto.vendosVleratNrAuto(hfNrAuto, this, null, null, null);

            koka.NrDok = txtNumri.Text;
            koka.NrSerial = txtNrSerial.Text;
            koka.IdKlient = Convert.ToInt32(cmbKlienti.Value);
            koka.Adresa = txtAdresa.Text;
            koka.DtDok = dteDateDokumenti.Date;
            koka.DtRegj = dteDateRegj.Date;
            koka.Shenime = txtShenime.Text;
            koka.DiAfer = txtDIAfer.Text;
            koka.DiLarg = txtDILarg.Text;
            koka.Referimi = txtReferimi.Text;
            koka.Lartesia = txtLartesia.Text;
            koka.IdStatusDok = idstatusdok;
            koka.IdRaportDesign = Convert.ToInt32(cmbFormatPrintimi.Value);
            if (ShtimModifikim == "shtim")
            {
                koka.IdNdermarrje = idNdermarrje;
                koka.IdNderVit = idNdermVit;
                koka.IdKrijuesi = IdPerdoruesi;
                koka.IdPerdoruesi = IdPerdoruesi;
                koka.IdKonfigAmbjente = Convert.ToInt32(cmbKonfigurimi.Value);
                koka.IdNivel = Convert.ToInt32(cmbNiveli.Value);
            }else
            {
                koka.IdPerdoruesi = IdPerdoruesi;
            }
            return koka;
        }
        private colTrupiRecetaOptikeSyri mbushTrupiRecetasyri()
        {
            colTrupiRecetaOptikeSyri col = new colTrupiRecetaOptikeSyri();
            object[] fushat = JsonConvert.DeserializeObject<object[]>(hfState.Get<string>("fushat"));
            object[] syriMajteValues = JsonConvert.DeserializeObject<object[]>(hfState.Get<string>("syriMajteValues"));
            object[] syriDjathteValues = JsonConvert.DeserializeObject<object[]>(hfState.Get<string>("syriDjatheValues"));
            colFushaRecetaOptike fusha = new colFushaRecetaOptike();
            for(int i = 0; i < fushat.Length; i++)
            {
                clsTrupiRecetaOptikeSyri cls = col[i];
                cls.IdKoka = Id;
                cls.IdFusha = Convert.ToInt32(fushat[i]);
                cls.SyriMajte = syriMajteValues[i].ToString();
                cls.SyriDjathte = syriDjathteValues[i].ToString();
                if (fusha.Exists(x => x.Pershkrimi == "Artikulli Kryesor" && x.IdFusha == cls.IdFusha))
                {
                    var artMajte = new clsArtikulli(cls.SyriMajte, idNdermarrje);
                    var artDjathte = new clsArtikulli(cls.SyriDjathte, idNdermarrje);
                    cls.SyriMajte = artMajte.IdArtikulli.ToString();
                    cls.SyriDjathte = artDjathte.IdArtikulli.ToString();
                }
            }

            return col;
        }

        private colTrupiRecetaOptikePunime merrNdryshimeRecetaOptikePunimeNgaGrida(ASPxDataBatchUpdateEventArgs e)
        {
            var trupi = mySessionObjects.MerrNgaSession<colTrupiRecetaOptikePunime>(Session, $"{GuidString}_trupiRecetaOptikePunime");
            for(int i = 0; i < e.InsertValues.Count; i++)
            {
                var rresht = new clsTrupiRecetaOptikePunime();
                rresht.IdKoka = Id;
                rresht = e.InsertValues[i].MerrCustomInsertedObject(rresht);
                    trupi.Add(rresht);
            }
            for(int i = 0; i < e.UpdateValues.Count; i++)
            {
                var rresht = trupi.Where(x => x.IdTrupi == e.UpdateValues[i].MerrKeyValue<int>()).FirstOrDefault();
                rresht = e.UpdateValues[i].MerrCustomUpdatedObject(rresht);
               
            }
            for(int i = 0; i < e.DeleteValues.Count; i++)
            {
                var rresht = trupi.Where(x => x.IdTrupi == e.DeleteValues[i].MerrKeyValue<int>()).FirstOrDefault();
                trupi.Remove(rresht);
            }
            mySessionObjects.RuajNeSession<colTrupiRecetaOptikePunime>(Session, trupi, $"{GuidString}_trupiRecetaOptikePunime");
            return trupi;
        }

        #endregion RUAJTJE
        #region AUTOCOMPLETE

        protected void cmbKlienti_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (!IsCallback) return;
            if (Request.Params["__CALLBACKID"].Contains("cmbKlienti"))
            {
                int value = 0;
                if (e.Value == null || !Int32.TryParse(e.Value.ToString(), out value))
                    return;

                ConfigureAspxComboBox.KonfiguroComboBoxKlientFurnitoriById(IdPerdoruesi, (int)hfState["idNdermarrje"], (ASPxComboBox)source, value);
            }
        }

        protected void cmbKlienti_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].Contains("cmbKlienti"))
                {
                    ConfigureAspxComboBox.KonfiguroComboBoxKlientFurnitor(e.Filter, e.BeginIndex + 1, e.EndIndex + 1, IdPerdoruesi, idNdermarrje, (ASPxComboBox)source, 1);
                }
            }
        }
        #endregion

        
    }
}