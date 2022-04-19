using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Data;
using System.Resources;
using System.Globalization;
using DbCore.DbAdmin;
using DbCore;
using PlatinumWeb.Templates;
using DbCore.DbRegjistrim;
using DbCore.DbShare;
using System.Web.Script.Serialization;
using DbCore.DbKontabiliteti;
using DbCore.DbInventari;
using System.Threading;
using DbCore.IMBUtils.DataBase;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class GjenerimAutomatik : MyPageBase
    {
        private string komponente => "GjenerimAutomatik.aspx";
        private string guidString; 
        private DbCore.DbAdmin.clsPeriudhaKontabel periudha = new DbCore.DbAdmin.clsPeriudhaKontabel();
        protected void Page_Load(object sender, EventArgs e)
        {
            int idGjuha = mySessionObjects.ktheGjuhe(Session), idViti = mySessionObjects.ktheIdVitNdermarrje(Session);
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                DbCore.clsFunksione.logout(Session,true,"FaqePaautorizuar");
            }
            int idPerd = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);

            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerd);
            }
            if (DbCore.mySessionObjects.merrPeriudheKontabel(Session) != null)
            {
                periudha = DbCore.mySessionObjects.merrPeriudheKontabel(Session);
            }
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (!IsPostBack)
            {
                guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                hfState.Set("guidString", guidString);
                mbushHiddenFieldMePerkthime(ci, rm);
                ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerd, idNdermarrje, cmbKonfigurimi, 100, rm, ci, DbCore.mySessionObjects.ktheGjuhe(Session));
                cmbKonfigurimi.SelectedIndex = 0;
                mbushComboLlojDate();
                cmbKonfigurimi.ReadOnly = false;
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerd, idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                DbCore.mySessionObjects.ruajColArtikull2NeSesion(Session, DbCore.DbRegjistrim.colKokaShitje.merrKokaShitjeDTTePaGjeneruara(DbCore.mySessionObjects.ktheNdermarrjeVit(Session), cmbLlojDate.Text, idPerd, idNdermarrje, false, dtePeriudhaNga.Date.ToShortDateString(), dtePeriudhaDeri.Date.ToShortDateString()));
                DbCore.mySessionObjects.ruajColArtikullNeSesion(Session, DbCore.DbRegjistrim.colKokaShitje.merrKokaShitjeDTTePaGjeneruara(DbCore.mySessionObjects.ktheNdermarrjeVit(Session), cmbLlojDate.Text, idPerd, idNdermarrje, false, dtePeriudhaNga.Date.ToShortDateString(), dtePeriudhaDeri.Date.ToShortDateString()));
                
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaDokumenta = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaDokumenta.merrTeDrejtaPerKeteKomponente(idPerd, idNdermarrje, idViti, komponente);
                hfTeDrejtaGjitheDok.Value = tedrejtaDokumenta.DGjitheDok.ToString();
                konfiguroVleraFillestare(idNdermarrje);
                mbushListaCallback();
                konfiguroGride();
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "gvGjenerimi", gvGjenerimi, cmbKonfigurimi.Text.Split(';')[0], "546", idGjuha);
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "gvGjenerimi", gvGjenerimi2, cmbKonfigurimi.Text.Split(';')[0], "546", idGjuha);
            }
            else
            {
                mbushListaCallback();
                konfiguroGride();
            }

            perktheKontrollet(ci, rm);
            gvGjenerimi.PercaktoTitlePanel(this, MenuInfo, pnlMesazhi, hfState, idPerd, idNdermarrje, idViti, idGjuha, int.Parse(cmbKonfigurimi.Value.ToString()), "GjenerimAutomatik.aspx", rm, ci);


            GridViewDataTextColumn col0 = gvGjenerimi2.Columns["Fshi"] as GridViewDataTextColumn;
            col0.DataItemTemplate = new MyButtonTemplate("");
            col0.VisibleIndex = 120;
            this.gvGjenerimi.Columns["#"].VisibleIndex = 0;
            this.gvGjenerimi2.Columns["#"].VisibleIndex = 0;
            Container.Attributes["src"] = "";
        }

        private void mbushComboLlojDate()
        {
            cmbLlojDate.Items.Add("Date dokumenti", "Date dokumenti");
            cmbLlojDate.Items.Add("Date regjistrimi", "Date regjistrimi");
            cmbLlojDate.Items.Add("Date maturimi", "Date maturimi");
            cmbLlojDate.SelectedIndex = 2;

        }
        private void perktheKontrollet(CultureInfo ci, ResourceManager rm)
        {
            btnKerko.Text = rm.GetString("ReportToolbarButtonSearch", ci);
            GridUtil.ToolTipButonaveMbiGride(gvGjenerimi, ci, rm);
        }

        private void mbushHiddenFieldMePerkthime(CultureInfo ci, ResourceManager rm)
        {
            konfigurimi_Label.Text = rm.GetString("lblModeli", ci);
            lblLlojDate.Text = rm.GetString("lblLlojDate", ci);
            lblPeriudha.Text = rm.GetString("lblNga", ci);
            lblDeri.Text = rm.GetString("lblDeri", ci);

        }
        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        /// <param name="idViti"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNdermarrje"></param>
        private void percaktoTemplateMenu(ASPxMenu aSPxMenu1, int idViti, int idPerdorues, int idNdermarrje)
        {
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, komponente, this, MenuInfo, true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
        }

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));

        }
        /// <summary>
        /// Percakton veprimin qe kryhet kur klikohet nje nga butonat e menuse
        /// </summary>
        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {
            if (e.Item.Name == "Gjenero")
            {
                Page.Validate();
                rillogarit();
            }
        }

        /// <summary>
        /// Vendos vlerat default kur po behet shtim
        /// </summary>
        /// <param name="idNdermarrje"></param>
        private void konfiguroVleraFillestare(int idNdermarrje)
        {
            this.dtePeriudhaNga.Date = periudha.FillimiPeriudha;
            AspxWebControlUtils.vendosDateEditMask(dtePeriudhaNga);
            this.dtePeriudhaDeri.Date = periudha.MbarimiPeriudha;
            AspxWebControlUtils.vendosDateEditMask(dtePeriudhaDeri);
            mbushNgaDb();
        }

        /// <summary>
        /// mbush griden siper sipas filtrave
        /// </summary>
        private void mbushNgaDb()
        {//mbush griden e popupit me te dhena  

            bool teDrejtaGjitheDok = hfTeDrejtaGjitheDok.Value.ToString().ToLower() == "true";
            DataTable dt = DbCore.DbRegjistrim.colKokaShitje.merrKokaShitjeDTTePaGjeneruara(DbCore.mySessionObjects.ktheNdermarrjeVit(Session), cmbLlojDate.Text, mySessionObjects.ktheIdPerdoruesi(Session), mySessionObjects.merrIdNdermarrjeSesioni(Session), teDrejtaGjitheDok, dtePeriudhaNga.Date.ToShortDateString(), dtePeriudhaDeri.Date.ToShortDateString());

            gvGjenerimi.DataSource = dt;
            DbCore.mySessionObjects.ruajColArtikullNeSesion(Session, dt);
            gvGjenerimi.DataBind();
            dt.Dispose();
           
            DataTable dt2 = DbCore.mySessionObjects.merrColArtikull2NgaSesioni(Session);
            gvGjenerimi2.DataSource = dt2;
            gvGjenerimi2.DataBind();
            dt2.Dispose();
        }

        //private void shtoNivel(string komponente, int idNdermarrje, int idPerdoruesi, ASPxGridView grida)
        //{
        //    GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
        //    if (typeof(GridViewDataComboBoxColumn) != grida.Columns["IdNivel"].GetType())
        //    {
        //        grida.Columns.Remove(grida.Columns["IdNivel"]);
        //        grida.Columns.Add(colnew);

        //        DataView nivele = colNivelRegjistrimi.ktheGjitheNivelRegjistrimiSipasKategoriDtCombo(1, idNdermarrje, idPerdoruesi, true);
        //        DataView nivele1 = colNivelRegjistrimi.ktheGjitheNivelRegjistrimiSipasKategoriDtCombo(2, idNdermarrje, idPerdoruesi, true);
        //        nivele.Table.Merge(nivele1.Table);
        //        colnew.PropertiesComboBox.DataSource = nivele;
        //        colnew.PropertiesComboBox.TextField = "Pershkrimi";
        //        colnew.PropertiesComboBox.ValueField = "IdNivel";
        //        colnew.FieldName = "IdNivel";
        //        DbCore.mySessionObjects.ruajDsComboGrideNeSession(Session, nivele, komponente + "nivele" + idNdermarrje);
        //    }
        //    else
        //    {
        //        colnew = (GridViewDataComboBoxColumn)grida.Columns["IdNivel"];
        //        if (colnew.PropertiesComboBox.Items.Count == 0)
        //        {
        //            //gvGjenerimi.Columns.Remove(gvGjenerimi.Columns["IdNivel"]);
        //            colnew.PropertiesComboBox.DataSource = DbCore.mySessionObjects.merrDsComboGrideNeSession(Session, komponente + "nivele" + idNdermarrje);
        //            //gvGjenerimi.Columns.Add(colnew);
        //        }
        //    }
        //}
        
        //private void shtoTransportues(string komponente, int idNdermarrje, ASPxGridView grida)
        //{
        //    GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
        //    if (typeof(GridViewDataComboBoxColumn) != grida.Columns["IdTransportues"].GetType())
        //    {
        //        grida.Columns.Remove(grida.Columns["IdTransportues"]);
        //        grida.Columns.Add(colnew);
        //        DataTable dt = DbCore.DbInventari.colTransportues.merrTransportuesSipasNdermarrjesDtSmall(idNdermarrje);
        //        dt.Rows.InsertAt(dt.NewRow(), 0);
        //        colnew.PropertiesComboBox.DataSource = dt;
        //        colnew.PropertiesComboBox.TextField = "Emertimi";
        //        colnew.PropertiesComboBox.ValueField = "IdTransportues";
        //        colnew.FieldName = "IdTransportues";
        //        DbCore.mySessionObjects.ruajDsComboGrideNeSession(Session, dt, komponente + "colTransportues");
        //    }
        //    else
        //    {
        //        colnew = (GridViewDataComboBoxColumn)grida.Columns["IdTransportues"];
        //        if (colnew.PropertiesComboBox.Items.Count == 0)
        //        {
        //            colnew.PropertiesComboBox.DataSource = DbCore.mySessionObjects.merrDsComboGrideNeSession(Session, komponente + "colTransportues");
        //        }
        //    }
        //}

        private void shtoColor()
        {
            GridViewDataColumn g = gvGjenerimi.Columns["ColorVleraMbetur"] as GridViewDataColumn;
            if (g == null) return;
            g.DataItemTemplate = new MyGaugeTemplate();
            GridViewDataColumn gg = gvGjenerimi2.Columns["ColorVleraMbetur"] as GridViewDataColumn;
            if (gg == null) return;
            gg.DataItemTemplate = new MyGaugeTemplate();
        }

        //private void shtoModel(string komponente, int idNdermarrje, int idPerdoruesi, int idGjuha, ASPxGridView grida)
        //{
        //    GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
        //    if (typeof(GridViewDataComboBoxColumn) != grida.Columns["IdKonfigAmbjente"].GetType())
        //    {
        //        grida.Columns.Remove(grida.Columns["IdKonfigAmbjente"]);
        //        grida.Columns.Add(colnew);


        //        DataTable dt = DbCore.DbShare.colKonfigurimAmbjenti.ktheKonfigAmbjSipasIdKategoriDTSmall(1, idNdermarrje, idPerdoruesi, idGjuha);
        //        DataTable dt2 = DbCore.DbShare.colKonfigurimAmbjenti.ktheKonfigAmbjSipasIdKategoriDTSmall(2, idNdermarrje, idPerdoruesi, idGjuha);
        //        dt.Merge(dt2);
        //        DataRow dr = dt.NewRow();
        //        //  dr["IdKonfigAmbjente"] = 0;
        //        dt.Rows.InsertAt(dr, 0);
        //        colnew.PropertiesComboBox.DataSource = dt;
        //        colnew.PropertiesComboBox.TextField = "KodKonfigAmbjente";
        //        colnew.PropertiesComboBox.ValueField = "IdKonfigAmbjente";
        //        colnew.FieldName = "IdKonfigAmbjente";
        //        DbCore.mySessionObjects.ruajDsComboGrideNeSession(Session, dt, komponente + "colKonfig");
        //    }
        //    else
        //    {
        //        colnew = (GridViewDataComboBoxColumn)grida.Columns["IdKonfigAmbjente"];
        //        if (colnew.PropertiesComboBox.Items.Count == 0)
        //        {
        //            //gvGjenerimi.Columns.Remove(gvGjenerimi.Columns["IdKonfigAmbjente"]);
        //            colnew.PropertiesComboBox.DataSource = DbCore.mySessionObjects.merrDsComboGrideNeSession(Session, komponente + "colKonfig");
        //            //gvGjenerimi.Columns.Add(colnew);
        //        }
        //    }
        //}


        //private void shtoMonedhe(string komponente, int idNdermarrje, int idPerdoruesi, ASPxGridView grida)
        //{
        //    GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
        //    if (typeof(GridViewDataComboBoxColumn) != grida.Columns["IdMonedha"].GetType())
        //    {
        //        grida.Columns.Remove(grida.Columns["IdMonedha"]);
        //        grida.Columns.Add(colnew);

        //        DataTable dt = DbCore.DbAdmin.colMonedhat.ktheGjitheMonedhatAktiveDtSmall(idNdermarrje, idPerdoruesi);
        //        dt.Rows.InsertAt(dt.NewRow(), 0);

        //        colnew.PropertiesComboBox.DataSource = dt;
        //        colnew.PropertiesComboBox.TextField = "MONEDHAKOD";
        //        colnew.PropertiesComboBox.ValueField = "IDMONEDHA";
        //        colnew.FieldName = "IdMonedha";
        //        DbCore.mySessionObjects.ruajDsComboGrideNeSession(Session, dt, komponente + "colMonedhat");
        //    }
        //    else
        //    {
        //        colnew = (GridViewDataComboBoxColumn)grida.Columns["IdMonedha"];
        //        if (colnew.PropertiesComboBox.Items.Count == 0)
        //        {
        //            //gvGjenerimi.Columns.Remove(gvGjenerimi.Columns["IdMonedha"]);
        //            colnew.PropertiesComboBox.DataSource = DbCore.mySessionObjects.merrDsComboGrideNeSession(Session, komponente + "colMonedhat");
        //            //gvGjenerimi.Columns.Add(colnew);
        //        }
        //    }
        //}


        //private void shtoDegeAdm(string komponente, int idNdermarrje, int idPerdoruesi, ASPxGridView grida)
        //{
        //    GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
        //    if (typeof(GridViewDataComboBoxColumn) != grida.Columns["IdDegeAdministrative"].GetType())
        //    {
        //        grida.Columns.Remove(grida.Columns["IdDegeAdministrative"]);
        //        grida.Columns.Add(colnew);
        //        DataTable dt = DbCore.DbRegjistrim.colDegeAdministrative.ktheGjitheDegeAdministrativeDtSmall(idNdermarrje);
        //        dt.Rows.InsertAt(dt.NewRow(), 0);
        //        colnew.PropertiesComboBox.DataSource = dt;
        //        colnew.PropertiesComboBox.TextField = "Kodi";
        //        colnew.PropertiesComboBox.ValueField = "IdDegeAdministrative";
        //        colnew.FieldName = "IdDegeAdministrative";
        //        DbCore.mySessionObjects.ruajDsComboGrideNeSession(Session, dt, komponente + "colDege");
        //    }
        //    else
        //    {
        //        colnew = (GridViewDataComboBoxColumn)grida.Columns["IdDegeAdministrative"];
        //        if (colnew.PropertiesComboBox.Items.Count == 0)
        //        {
        //            //gvGjenerimi.Columns.Remove(gvGjenerimi.Columns["IdMonedha"]);
        //            colnew.PropertiesComboBox.DataSource = DbCore.mySessionObjects.merrDsComboGrideNeSession(Session, komponente + "colDege");
        //            //gvGjenerimi.Columns.Add(colnew);
        //        }
        //    }
        //}
        
        //private void shtoGrup1(string komponente, int idNdermarrje, int idPerdoruesi, ASPxGridView grida)
        //{
        //    GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
        //    if (typeof(GridViewDataComboBoxColumn) != grida.Columns["IdGrup1"].GetType())
        //    {
        //        grida.Columns.Remove(grida.Columns["IdGrup1"]);
        //        grida.Columns.Add(colnew);
        //        DataTable dt = colGrupimDokumentiKoka.merrGrupeSipasGrupitDtSmall(1, idNdermarrje, idPerdoruesi);
        //        dt.Rows.InsertAt(dt.NewRow(), 0);
        //        colnew.PropertiesComboBox.DataSource = dt;
        //        colnew.PropertiesComboBox.TextField = "Kodi";
        //        colnew.PropertiesComboBox.ValueField = "IdGrupimKoka";
        //        colnew.FieldName = "IdGrup1";
        //        DbCore.mySessionObjects.ruajDsComboGrideNeSession(Session, dt, komponente + "colGrup1");
        //    }
        //    else
        //    {
        //        colnew = (GridViewDataComboBoxColumn)grida.Columns["IdGrup1"];
        //        if (colnew.PropertiesComboBox.Items.Count == 0)
        //        {
        //            //gvGjenerimi.Columns.Remove(gvGjenerimi.Columns["IdMonedha"]);
        //            colnew.PropertiesComboBox.DataSource = DbCore.mySessionObjects.merrDsComboGrideNeSession(Session, komponente + "colGrup1");
        //            //gvGjenerimi.Columns.Add(colnew);
        //        }
        //    }
        //}


        //private void shtoPikeShitjeFurnizim(string komponente, int idNdermarrje, ASPxGridView grida)
        //{
        //    GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
        //    if (typeof(GridViewDataComboBoxColumn) != grida.Columns["IdPikeShitjeFurnizimi"].GetType())
        //    {
        //        grida.Columns.Remove(grida.Columns["IdPikeShitjeFurnizimi"]);
        //        grida.Columns.Add(colnew);
        //        DataTable dt;


        //        dt = colPikaShitjeFurnizimi.mbushGjithePikeShitjeFurnizimiDtSmall(idNdermarrje);
        //        dt.Rows.InsertAt(dt.NewRow(), 0);
        //        colnew.PropertiesComboBox.DataSource = dt;
        //        colnew.PropertiesComboBox.TextField = "Kodi";
        //        colnew.PropertiesComboBox.ValueField = "IdPikeShitjeFurnizimi";
        //        colnew.FieldName = "IdPikeShitjeFurnizimi";
        //        colnew.Caption = "Pike Shitje/Furnizimi";
        //        DbCore.mySessionObjects.ruajDsComboGrideNeSession(Session, dt, komponente + "colPikat");
        //    }
        //    else
        //    {
        //        colnew = (GridViewDataComboBoxColumn)grida.Columns["IdPikeShitjeFurnizimi"];
        //        if (colnew.PropertiesComboBox.Items.Count == 0)
        //        {
        //            //gvGjenerimi.Columns.Remove(gvGjenerimi.Columns["IdPikeShitjeFurnizimi"]);
        //            colnew.PropertiesComboBox.DataSource = DbCore.mySessionObjects.merrDsComboGrideNeSession(Session, komponente + "colPikat");
        //            //gvGjenerimi.Columns.Add(colnew);
        //        }
        //    }
        //    //int visibleIndex = gvGjenerimi.Columns["IdPikeShitjeFurnizimi"].VisibleIndex;

        //    //colnew.VisibleIndex = visibleIndex;            
        //}

        /// <summary>
        /// mbush combon e kolones LlojiKf
        /// </summary>
        /// <param name="komponente"></param>
        

        
        /// <summary>
        /// mbush combon e kolones Prodhuar
        /// </summary>
        /// <param name="komponente"></param>
        

        private void konfiguroGride()
        {
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            GridViewDataTextColumn col0 = gvGjenerimi2.Columns["Fshi"] as GridViewDataTextColumn;
            col0.DataItemTemplate = new MyButtonTemplate("");
            col0.VisibleIndex = 120;
            string komponente = DbCore.clsFunksione.GetKomponente(Page.Request);
            int idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);
            int idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            gvGjenerimi.Columns["#"].VisibleIndex = 0;

            KonfigurimComboGride.ShtoStatus(gvGjenerimi, rm, ci);
            KonfigurimComboGride.ShtoStatus(gvGjenerimi2, rm, ci);
            KonfigurimComboGride.ShtoLlojiKlientFurnitor(gvGjenerimi, rm, ci);
            KonfigurimComboGride.ShtoLlojiKlientFurnitor(gvGjenerimi2, rm, ci);
            KonfigurimComboGride.ShtoStatusStransferimi(gvGjenerimi, rm, ci);
            KonfigurimComboGride.ShtoStatusStransferimi(gvGjenerimi2, rm, ci);
            KonfigurimComboGride.ShtoProdhuar(gvGjenerimi, rm, ci);
            KonfigurimComboGride.ShtoProdhuar(gvGjenerimi2, rm, ci);
            KonfigurimComboGride.ShtoStatusAprovimi(gvGjenerimi, rm, ci);
            KonfigurimComboGride.ShtoStatusAprovimi(gvGjenerimi2, rm, ci);
            KonfigurimComboGride.ShtoNivelMeDataSource(gvGjenerimi, () =>
            {
                var nivele = colNivelRegjistrimi.ktheGjitheNivelRegjistrimiSipasKategoriDtCombo(1, idNdermarrje, idPerdoruesi, true);
                var nivele1 = colNivelRegjistrimi.ktheGjitheNivelRegjistrimiSipasKategoriDtCombo(2, idNdermarrje, idPerdoruesi, true);
                nivele.Table.Merge(nivele1.Table);
                return nivele;
            }, Session, komponente, guidString, "IdNivel");
            KonfigurimComboGride.ShtoNivelMeDataSource(gvGjenerimi2, () =>
            {
                var nivele = colNivelRegjistrimi.ktheGjitheNivelRegjistrimiSipasKategoriDtCombo(1, idNdermarrje, idPerdoruesi, true);
                var nivele1 = colNivelRegjistrimi.ktheGjitheNivelRegjistrimiSipasKategoriDtCombo(2, idNdermarrje, idPerdoruesi, true);
                nivele.Table.Merge(nivele1.Table);
                return nivele;
            }, Session, komponente, guidString, "IdNivel");
            KonfigurimComboGride.ShtoModelMeDataSource(gvGjenerimi, () =>
            {
                DataTable dt = DbCore.DbShare.colKonfigurimAmbjenti.ktheKonfigAmbjSipasIdKategoriDTSmall(1, idNdermarrje, idPerdoruesi, idGjuha);
                DataTable dt2 = DbCore.DbShare.colKonfigurimAmbjenti.ktheKonfigAmbjSipasIdKategoriDTSmall(2, idNdermarrje, idPerdoruesi, idGjuha);
                dt.Merge(dt2);
                DataRow dr = dt.NewRow();
                return dt;
            }, Session, komponente, guidString);
            KonfigurimComboGride.ShtoModelMeDataSource(gvGjenerimi2, () =>
            {
                DataTable dt = DbCore.DbShare.colKonfigurimAmbjenti.ktheKonfigAmbjSipasIdKategoriDTSmall(1, idNdermarrje, idPerdoruesi, idGjuha);
                DataTable dt2 = DbCore.DbShare.colKonfigurimAmbjenti.ktheKonfigAmbjSipasIdKategoriDTSmall(2, idNdermarrje, idPerdoruesi, idGjuha);
                dt.Merge(dt2);
                DataRow dr = dt.NewRow();
                return dt;
            }, Session, komponente, guidString);
            KonfigurimComboGride.ShtoTransportues(gvGjenerimi, idNdermarrje, Session, komponente, guidString, "IdTransportues");
            KonfigurimComboGride.ShtoTransportues(gvGjenerimi2, idNdermarrje, Session, komponente, guidString, "IdTransportues");
            KonfigurimComboGride.ShtoMonedhe(gvGjenerimi, idNdermarrje, idPerdoruesi, Session, komponente, guidString);
            KonfigurimComboGride.ShtoMonedhe(gvGjenerimi2, idNdermarrje, idPerdoruesi, Session, komponente, guidString);
            KonfigurimComboGride.shto_DegeAdministrative(gvGjenerimi, idNdermarrje, Session, komponente, guidString, "IdDegeAdministrative");
            KonfigurimComboGride.shto_DegeAdministrative(gvGjenerimi2, idNdermarrje, Session, komponente, guidString, "IdDegeAdministrative");
            KonfigurimComboGride.ShtoGrupimDokumentashMeDataSource(gvGjenerimi, () =>
            {
                var dt = colGrupimDokumentiKoka.merrGrupeSipasGrupitDtSmall(1, idNdermarrje, idPerdoruesi);
                dt.Rows.InsertAt(dt.NewRow(), 0);
                return dt;
            }, Session, komponente, guidString, "IdGrup1", 1);
            KonfigurimComboGride.ShtoGrupimDokumentashMeDataSource(gvGjenerimi2, () =>
            {
                var dt = colGrupimDokumentiKoka.merrGrupeSipasGrupitDtSmall(1, idNdermarrje, idPerdoruesi);
                dt.Rows.InsertAt(dt.NewRow(), 0);
                return dt;
            }, Session, komponente, guidString, "IdGrup1", 1);
            KonfigurimComboGride.ShtoPikeShitjeFurnizim(gvGjenerimi, idNdermarrje, Session, komponente, guidString, "IdPikeShitjeFurnizimi", "default");
            KonfigurimComboGride.ShtoPikeShitjeFurnizim(gvGjenerimi2, idNdermarrje, Session, komponente, guidString, "IdPikeShitjeFurnizimi", "default");
            shtoColor();
            //// shtoAutomjet(idNdermarrje);
           //  DbCore.clsFunksione.percaktoVisibleColumnsSipasKonfigurimit(gvGjenerimi, "gvGjenerimi", komponente, int.Parse(cmbKonfigurimi.Value.ToString()), true, idGjuha);
           // DbCore.clsFunksione.percaktoVisibleColumnsSipasKonfigurimit(gvGjenerimi2, "gvGjenerimi", komponente, int.Parse(cmbKonfigurimi.Value.ToString()), true, idGjuha);

            GridUtil.konfigGrideListeEMadhePaTheme(gvGjenerimi, "IdShitjeKoka");
            gvGjenerimi.SettingsPager.PageSize = 10;

        }

        protected void gvGjenerimi_DataBound(object sender, EventArgs e)
        {
            if (this.gvGjenerimi.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
                //perzgjidh
                GridViewCommandColumn check = new GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true; check.Width = Unit.Percentage(2);
                check.SetColVisibleIndex(0);
                //behet per te afishuar rreshtin qe do sherbej per filtrim
                gvGjenerimi.Settings.ShowFilterRow = true;
                gvGjenerimi.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                gvGjenerimi.Settings.ShowFilterRowMenu = true;
                gvGjenerimi.Columns.Add(check);


                gvGjenerimi.KeyFieldName = "IdShitjeKoka";
                gvGjenerimi.SettingsBehavior.AllowSelectByRowClick = true;
                gvGjenerimi.SettingsBehavior.AllowFocusedRow = true;
            }
            this.gvGjenerimi.Columns["#"].VisibleIndex = 0;

        }
        protected void gvGjenerimi2_DataBound(object sender, EventArgs e)
        {
            if (this.gvGjenerimi2.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
                //perzgjidh
                GridViewCommandColumn check = new GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true; check.Width = Unit.Percentage(2);
                check.SetColVisibleIndex(0);
                //behet per te afishuar rreshtin qe do sherbej per filtrim
                gvGjenerimi2.Settings.ShowFilterRow = true;
                gvGjenerimi2.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                gvGjenerimi2.Settings.ShowFilterRowMenu = true;
                gvGjenerimi2.Columns.Add(check);


                gvGjenerimi2.KeyFieldName = "IdShitjeKoka";
                gvGjenerimi2.SettingsBehavior.AllowSelectByRowClick = true;
                gvGjenerimi2.SettingsBehavior.AllowFocusedRow = true;
            }
            if (this.gvGjenerimi2.Columns["Fshi"] == null)
            {
                GridViewDataTextColumn fshi = new GridViewDataTextColumn();
                fshi.Caption = "Fshi";
                fshi.Width = Unit.Percentage(5);
                gvGjenerimi2.Columns.Add(fshi);

            }
            this.gvGjenerimi2.Columns["#"].VisibleIndex = 0;

        }
     
        protected void gvGjenerimi_AfterPerformCallback(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            mbushListaCallback();
        }
        protected void gvGjenerimi2_AfterPerformCallback(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            mbushListaCallback();
        }

        protected void gvGjenerimi_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string[] arr = e.Parameters.Split(';');
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "gvGjenerimi", gvGjenerimi, cmbKonfigurimi.Text.Split(';')[0], "546", idGjuha);
            
            
            if (arr.Length == 3)
            {
                if (arr[2] == "")
                    gvGjenerimi.FilterExpression = "";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                     DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuha, "gvGjenerimi", komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
                    //DbCore.DbAdmin.clsGridaKoka koka2 = new DbCore.DbAdmin.clsGridaKoka(idGjuha, "gvGjenerimi2", komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], idNdermarrje, koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvGjenerimi.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvGjenerimi);
                    }
                }
            }
            gvGjenerimi.Selection.UnselectAll();
            if (e.Parameters.ToString() == "kerko")
                mbushNgaDb();
            else
                if (e.Parameters.ToString() == "pastro")
                {
                    DataTable dt = DbCore.DbRegjistrim.colKokaShitje.merrKokaShitjeDTTePaGjeneruara(1, cmbLlojDate.Text, 1, 1, false, dtePeriudhaNga.Date.ToShortDateString(), dtePeriudhaDeri.Date.ToShortDateString());
                    DbCore.mySessionObjects.ruajColArtikull2NeSesion(Session, dt);
                    gvGjenerimi.Selection.UnselectAll();
                    gvGjenerimi2.Selection.UnselectAll();
                    mbushNgaDb();
                   
                    
                }
                else
                {
                    mbushListaCallback();
                }
        }
        private void mbushListaCallback()
        {

             gvGjenerimi2.DataSource = DbCore.mySessionObjects.merrColArtikull2NgaSesioni(Session);
            gvGjenerimi2.DataBind();
             gvGjenerimi.DataSource = DbCore.mySessionObjects.merrColArtikullNgaSesioni(Session);
            gvGjenerimi.DataBind();
        }
        protected void gvGjenerimi2_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "gvGjenerimi", gvGjenerimi2, cmbKonfigurimi.Text.Split(';')[0], "546", idGjuha);
            if (e.Parameters.ToString() == "select")
            {
                List<object> rreshtat = gvGjenerimi.GetSelectedFieldValues("IdShitjeKoka");
                DataTable dt = DbCore.mySessionObjects.merrColArtikull2NgaSesioni(Session);

                foreach (object id in rreshtat)
                {
                    DataRow[] drs = dt.Select("IdShitjeKoka = " + id);
                    if (drs.Length > 0)
                        continue;
                    DataRow dr = DbCore.DbRegjistrim.colKokaShitje.merrKokaShitjeDR(int.Parse(id.ToString()), mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    
                    dt.ImportRow(dr);
                }
                DbCore.mySessionObjects.ruajColArtikull2NeSesion(Session, dt);
                gvGjenerimi2.DataSource = dt;
                gvGjenerimi2.DataBind();
            }
            else
            {
                int key = 0;
                bool fshi = int.TryParse(e.Parameters.ToString(), out key);
                if (fshi)
                {
                    DataTable dt = DbCore.mySessionObjects.merrColArtikull2NgaSesioni(Session);
                    dt.Rows.RemoveAt(key);  //[key].Delete();
                    DbCore.mySessionObjects.ruajColArtikull2NeSesion(Session, dt);
                    gvGjenerimi2.DataSource = dt;
                    gvGjenerimi2.DataBind();
                }
                else mbushListaCallback();
            }
        }

        private void rillogarit()
        {
            DataTable dt = DbCore.mySessionObjects.merrColArtikull2NgaSesioni(Session);
            colKokaShitje col = new colKokaShitje();
            col.mbushKokatShitjeDT(dt);//krijojme colleksionin nga datatable i ruajtur
            //krijojme tabelen e erroreve
            DataTable err = new DataTable();
            err.Columns.Add("Kodi");
            err.Columns.Add("Gabimi");
            err.Columns.Add("Rreshti");
            int nrreshta = 0;
            clsMesazh mesazh = new clsMesazh();
            //krijojme variablat qe na duhen
            DevExpress.Web.ASPxHiddenField hfArkiva = new DevExpress.Web.ASPxHiddenField();
            string shfaqmesazhapolupemagazina, shfaqmesazhapolupebanka, shfaqmesazhapolupeVDK, shfaqmesazhapolupe, mesazhinformues;
            JavaScriptSerializer serializusi = new JavaScriptSerializer();
            serializusi.MaxJsonLength = 50000000;
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            string serverUrl = DbCore.clsFunksione.ktheServerUrl(Request);
            int idGjuha = mySessionObjects.ktheGjuhe(Session), idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session), idViti = mySessionObjects.ktheIdVitNdermarrje(Session);
            bool eshteOwn = mySessionObjects.merrEshteOwnSesioni(Session), eshteMeme = mySessionObjects.merrEshteMemeSesioni(Session);
            CultureInfo ci = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo(idGjuha);
            ///per cdo shitje krijojme shitjen e re
            DbData dbData = new DbData();
            foreach (clsKokaShitje k in col)
            {
                
                nrreshta++;
                DbCore.DbArkaBanka.clsVeprimBankaKoka veprimebanka = new DbCore.DbArkaBanka.clsVeprimBankaKoka();
                k.DtDok = DateTime.Today;//dokumentit i vendoset data e veprimit
                k.DtRegjistrimi = DateTime.Today;
                clsKurset kursi = new clsKurset(k.IdMonedha, DateTime.Today);//merret kursi i dates se veprimit
                k.Kursi = kursi.VleraKursi;
                DbCore.DbShare.clsKonfigurimAmbjenti konfig = new clsKonfigurimAmbjenti(k.IdKonfigAmbjente);
                DbCore.DbKontabiliteti.clsKlientFurnitor kf = new DbCore.DbKontabiliteti.clsKlientFurnitor(k.IdKlientFurnitor);
                if (kf.LimitBllokues != 0 && k.TotaliMeZbritjeMeTVSH > kf.LimitBllokues)//kontrollojme limit e klientit
                {
                    object[] arr = { konfig.KodKonfigAmbjente + " " + k.NrDok + " " + k.DtDok.ToShortDateString(), rm.GetString("msgTotalFatureKaluarLimitBllokues", ci), nrreshta };
                    err.Rows.Add(arr);
                    continue;
                }
                DbCore.DbInventari.clsMaturimi maturimi = new DbCore.DbInventari.clsMaturimi(kf.MaturimiKF);//dhe gjejme daten e maturimit te ketij klienti
                k.DtMaturimi = RestApi.WebAPI.Models.RregjistrimeRepository.ktheMaturim(maturimi.IdMaturimi.ToString(), DateTime.Today);

                colTrupiShitje coltrup = new colTrupiShitje();
                clsKokaMagazina kokam = new clsKokaMagazina();///marrim magazinen dhe trupin e shitjes
                if (konfig.IdKategori == 1)
                    kokam.mbushKokaMagazinaSipasIDGjenerues(k.IdShitjeKoka, 2, k.IdKonfigAmbjente);
                else
                    kokam.mbushKokaMagazinaSipasIDGjenerues(k.IdShitjeKoka, 1, k.IdKonfigAmbjente);
                //DbCore.DbAdmin.clsPeriudhaKontabel periudha = new DbCore.DbAdmin.clsPeriudhaKontabel(k.DtDok, idNdermarrje);
                int idPeriudhaKontabel = clsPeriudhaKontabel.ktheIdPeriudheSipasDatesDheNdermarrjes(k.DtDok, idNdermarrje);
                DbCore.DbInventari.colArtikujt colart = new colArtikujt(k.IdShitjeKoka);
                DbCore.DbKontabiliteti.colLlogarite colllog = new colLlogarite(k.IdShitjeKoka);

                coltrup.mbushGjitheTrupiShitjeNgaKoka(k.IdShitjeKoka);
                int count=0;
                bool eshtedokKthimi = false;
                foreach (clsTrupiShitje trup in coltrup)
                {
                    if (trup.IdTrupiKthim != 0)
                        eshtedokKthimi = true;
                    if (trup.IdLlojVeprimi == 1)
                        trup.Element = colart[count];
                    else trup.Element = colllog[count];
                    count++;
                }
                colArkiva oArkiva = new colArkiva();
                oArkiva = new colArkiva(k.IdShitjeKoka, konfig.IdKategori); //marrim arkivat
                if (oArkiva.Count > 0)
                {

                    hfArkiva.Set("ArkivaSaved", serializusi.Serialize(oArkiva));
                }
                clsKokaRezervime rez = new clsKokaRezervime();//marrim rezervimin nqs ka
                rez.mbushKokaRezervimiSipasIDGjenerues(kokam.IdKokaMagazina, 2, kokam.IdKonfigAmbjente);
                clsKokaFleteKontabel kokfk = new clsKokaFleteKontabel(k.IdShitjeKoka, konfig.IdKategori);//marrim fleten kontabel dhe qendrat e kostos
                DbCore.DbQendraKosto.clsKokaQendraKosto qend = new DbCore.DbQendraKosto.clsKokaQendraKosto();
                qend.KtheKokaQKSipasIDGjeneruesDheKonfigMeTrup(kokfk.IdKokaFleteKontabel, kokfk.IdKonfigAmbjente);

                ///marrim kushtet sipas konfigurimit
                DbCore.DbShare.clsKusht kushtamor = new DbCore.DbShare.clsKusht(k.IdKonfigAmbjente, "ZDAM");
                DbCore.DbShare.clsKonfigurimAmbjenti konfamortizimi = new clsKonfigurimAmbjenti(kushtamor.Vlera);
                bool gjenerodokmagazine = clsAlternativaKushti.getAlternativa(k.IdKonfigAmbjente, "GJDM") == "Po";
                bool dergoemail = clsAlternativaKushti.getAlternativa(k.IdKonfigAmbjente, "LE") == "Po";
                bool dergoemailVFOne = clsAlternativaKushti.getAlternativa(k.IdKonfigAmbjente, "DEVFOne") == "Po";
                bool printogarancifature, pageseFature;
                bool mekontabilizim = false;
                mekontabilizim = clsAlternativaKushti.getAlternativa(k.IdKonfigAmbjente, "GJK") == "Jo" ? false : true;

                //nqs konfigurimi eshte qe te gjeneroje nr me dok fillestar nr i dokumentit ri i njejte
                //nqs eshte me nr automatik gjejme nr e radhes per kete dokument

                DevExpress.Web.ASPxHiddenField hidden = new DevExpress.Web.ASPxHiddenField();
                List<NrAuto> list = new List<NrAuto>();
                if (DbCore.DbShare.clsAlternativaKushti.getAlternativa(k.IdKonfigAmbjente, "NRDA") != "Nr. dok fillestar")
                {

                    int idnrautonrdok = clsAtributeTrupi.merrNrAutomatikSipasKontrollitDheKonfigurimit(k.IdKonfigAmbjente, "txtNumer", 506);
                    string nrdokshitje = DbCore.DbAdmin.clsNrAutom.merrVlerenNrAutomatik(idnrautonrdok, k.DtDok);
                    if (!String.IsNullOrEmpty(nrdokshitje))//nqs ka nr automatik
                    {
                        DbCore.DbAdmin.NrAuto nrdokshi = new NrAuto();
                        nrdokshi.kodKontrolli = "NrDok";
                        nrdokshi.idNrAuto = idnrautonrdok;
                        nrdokshi.vlereNrAuto = nrdokshitje;

                        list.Add(nrdokshi);
                        nrdokshitje = nrdokshi.vlereNrAuto;
                        hidden.Add("NrDok", serializusi.Serialize(nrdokshi));
                    }
                    else
                    {//nqs nuk ka nr automatik gjenerojme nje nr duke mare parasyh nr ekzistues dhe i shtojme nje nr
                        Int64 nrd = clsKokaShitje.merrNrMaxDokumenti(idNdermarrje, k.NrDok + "_%") + 1;
                        nrdokshitje = k.NrDok + "_" + nrd;//nqs nuk ka nr automatik merr daten e dokumentit +nr incrementues
                        k.NrDok = nrdokshitje;
                    }
                }

                ///marrim kodet e objekteve kur kemi id
                string kodmonedhe = "", kodmenyretras = "", kodkushtderg = "", kodagjenti1 = "", kodkushtepages = "", koddege = "", kodpike = "", kodmagazina = "", kodgrup1 = "", kodagjenti2 = "", kodagjenti3 = "", kodtrasportuesi = "";
                if (k.IdMonedha != 0)
                {
                    //DbCore.DbAdmin.clsMonedha mon = new DbCore.DbAdmin.clsMonedha(k.IdMonedha);
                    kodmonedhe = clsMonedha.ktheKodMonedheSipasId(k.IdMonedha); //mon.KodiMonedha;
                }
                if (k.IdMenyreTransporti != 0)
                {
                    DbCore.DbAdmin.clsMenyreTransporti menyretrans = new DbCore.DbAdmin.clsMenyreTransporti(k.IdMenyreTransporti);
                    kodmenyretras = menyretrans.KodiMenyreTransporti;
                }
                if (k.IdKushtDergimi != 0)
                {
                    DbCore.DbAdmin.clsKushtDergimi kushtderg = new DbCore.DbAdmin.clsKushtDergimi(k.IdKushtDergimi);
                    kodkushtderg = kushtderg.KodiKushtDergimi;
                }
                if (k.IdAgjent != 0)
                {
                    DbCore.DbAdmin.clsAgjentShitje agjenti = new DbCore.DbAdmin.clsAgjentShitje(k.IdAgjent);
                    kodagjenti1 = agjenti.KodiAgjentShitje;
                }
                string menyrepag = "";
                menyrepag = clsFunksione.ktheMenyrePageseSipasID(k.IdMenyrePagese);
                if (k.IdKushtPagese != 0)
                {
                    DbCore.DbKontabiliteti.clsKushtPageseKoka kushtpagese = new clsKushtPageseKoka(k.IdKushtPagese);
                    kodkushtepages = kushtpagese.KodiKushtPagese;
                }
                if (k.IdDegeAdministrative != 0)
                {
                    clsDegeAdministrative dege = new clsDegeAdministrative(k.IdDegeAdministrative);
                    koddege = dege.Kodi;
                }
                if (k.IdPikeShitjeFurnizimi != 0)
                {
                    clsPikeShitjeFurnizimi pike = new clsPikeShitjeFurnizimi(k.IdPikeShitjeFurnizimi);
                    kodpike = pike.Kodi;
                }
                clsKokaShitje kokaMema = new clsKokaShitje();
                if (k.IdGrup1 != 0)
                {
                    clsGrupimDokumentiKoka grup1 = new clsGrupimDokumentiKoka(k.IdGrup1);
                    kodgrup1 = grup1.Kodi;
                }
                if (k.IdAgjenti2 != 0)
                {
                    DbCore.DbAdmin.clsAgjentShitje agjent2 = new DbCore.DbAdmin.clsAgjentShitje(k.IdAgjenti2);
                    kodagjenti2 = agjent2.KodiAgjentShitje;
                }
                if (k.IdAgjenti3 != 0)
                {
                    DbCore.DbAdmin.clsAgjentShitje agjent3 = new DbCore.DbAdmin.clsAgjentShitje(k.IdAgjenti3);
                    kodagjenti3 = agjent3.KodiAgjentShitje;
                }
                if (k.IdTransportues != 0)
                {
                    clsTransportues tranportues = new clsTransportues(k.IdTransportues);
                    kodtrasportuesi = tranportues.Emertimi;
                }
                clsKokaShitje faturashitjengaurdhershitjamekupontatimor = new clsKokaShitje();
                bool tollona = clsAlternativaKushti.getAlternativa(k.IdKonfigAmbjente, "RSHTT") == "Po";
                bool tollonakastrati = clsAlternativaKushti.getAlternativa(k.IdKonfigAmbjente, "RSHTTK") == "Po";
                bool tollonakastratielektronik = clsAlternativaKushti.getAlternativa(k.IdKonfigAmbjente, "RSHTTKE") == "Po";
                bool autoklient = clsAlternativaKushti.getAlternativa(k.IdKonfigAmbjente, "LAVK") == "Po";
                DbCore.DbShare.clsKonfigurimAmbjenti konfmag = new DbCore.DbShare.clsKonfigurimAmbjenti(konfig.IdKonfigurimi, idGjuha);
                if (kokam.IdMagazina != 0)
                {
                    clsNjesiAdministrative magazina = new clsNjesiAdministrative(kokam.IdMagazina);
                    kodmagazina = magazina.Kodi;
                }
                bool gjeneromeme = clsAlternativaKushti.getAlternativa(k.IdKonfigAmbjente, "GJOSHM") == "Po";
                bool gjenerobij = false; 
                var alternativa = clsAlternativaKushti.getAlternativa(k.IdKonfigAmbjente, "GJUBB");
                if (alternativa == "Po")
                    gjenerobij = true;
                else
                {
                    alternativa = clsAlternativaKushti.getAlternativa(k.IdKonfigAmbjente, "GJFBB");
                    if (alternativa == "Po")
                        gjenerobij = true;
                }

                bool kontrolloIMEIFifo = false;

                if (clsAlternativaKushti.getAlternativa(k.IdKonfigAmbjente, "AFI") == "Po")
                    kontrolloIMEIFifo = true;
                //Duhet te merret muaji aktual dhe jo ai i dokumentit fillestar.
                k.MuajRaportimi = DateTime.Now.Month;
                k.IdVitRaportimi = clsViti.ktheIdVitPerNdermarrjenSipasKodit(idNdermarrje, DateTime.Now.Year.ToString());
                k.IdNdermarrjeVit = clsNdermarrjeViti.ktheIdNdermarrjeVitiSipasNdermarjesDheKodVitit(idNdermarrje, DateTime.Now.Year);
                string llojZevendesimi = clsAlternativaKushti.getAlternativa(k.IdKonfigAmbjente, "ZT");
                var merrMagazinePerberesi = clsAlternativaKushti.getAlternativa(k.IdKonfigAmbjente, "KGJAPMR") == "Po";
                //krijojme dokumentin e ri duke patur parasysh qe iddoknga duhet te jete 0 dhe si dokument gjenerues i tij do shkoje dokumenti ekzistues per te ruajtur lidhjen e tyre
                clsKokaShitje kokare = new clsKokaShitje();
                mesazh = kokare.krijoShitje(ref gjenerodokmagazine, k.IdNivel, k.IdTemplate, k.IdKonfigAmbjente, k.IdKlientFurnitor, kf.KodKlientFurnitor, k.IdProjekt, k.NrProjekt, k.DtDok, k.NrDok, k.NrSerial, k.DtMaturimi, k.IdMonedha, kodmonedhe, k.Kursi, k.IdMenyreTransporti, kodmenyretras, k.DtTransportimi, k.IdKushtDergimi, kodkushtderg, k.IdAgjent, kodagjenti1, k.IdMenyrePagese, menyrepag, k.IdKushtPagese, kodkushtepages, k.Zbritje, k.Totali, k.Tvsh, k.DtRegjistrimi, 1, k.IdNdermarrje, k.IdNdermarrjeVit, k.IdNivel, k.IdKonfigAmbjente, k.IdShitjeKoka, 0, k.AdresaFaturimit, k.AdresaDergimit, k.Pershkrimi, k.Dogana, k.IdDegeAdministrative, koddege, k.IdPikeShitjeFurnizimi, kodpike, idPerdoruesi, k.IdRaportDesing, coltrup, konfig.IdKategori == 1 ? true : false, konfig.KodKonfigAmbjente, idPeriudhaKontabel, konfmag, kokam.IdMagazina, kodmagazina, mekontabilizim, k.IdGrup1, k.IdGrup2, k.IdGrup3, k.AfatKohor, k.Cash, k.StatusAprovimi, idPerdoruesi, k.PerqindjeAgjenti, out shfaqmesazhapolupe, hfArkiva, qend.ColTrupi, rez.IdKokaRezervimi, out mesazhinformues, gjeneromeme, kokaMema, k.IdTransferimi, k.IdKonfigTransferimi, eshteMeme, gjenerobij, k.StatusTransferimi, k.EmerKlienti, k.Kontakti, k.Kase, k.Kupon, kodgrup1, k.DtFillimi, k.DtMbarimi, k.IdAutomjet, k.KilometraAuto, k.Targa, k.IdAgjenti2, k.PerqindjeAgjenti2, kodagjenti2, k.IdAgjenti3, k.PerqindjeAgjenti3, kodagjenti3, k.Marresi, k.IdTransportues, kodtrasportuesi, k.FaturePermbledhese, faturashitjengaurdhershitjamekupontatimor, k.ShpenzimeJoTeZbritshme, k.IdArka, true, tollona, autoklient, false, k.DtFature, false, tollonakastrati, tollonakastratielektronik, k.MuajRaportimi, k.IdVitRaportimi, k.Shoferi, k.TargaShoferit, k.ZbritjeNeVlere, k.PerqindjeZbritje, k.IdKarta, k.Pike, k.OColFazat, k.IdFaza, k.ColKlienteFurnitoreVartes, DateTime.Now, new DbData(), llojZevendesimi, k.Koordinata, false, false, idGjuha, new clsKonfigurimAmbjenti(), -1, k.NiptK, k.QytetiK, false, k.IdKategoriSeriali, null, k.Shenime2, k.KartaPaPagese, k.IdDokTransferimNga, eshtedokKthimi, k.IdLlojMarreveshje, k.IdMarreveshje, k.StatusMarreveshje , k.KerkuarNga ,"shtim" ,k.DateKerkese, merrMagazinePerberesi, k.NrDok, "", "", 0,"","","",0,0,"");
                if (!mesazh.Status)
                {
                    object[] arr = { konfig.KodKonfigAmbjente + " " + k.NrDok + " " + k.DtDok.ToShortDateString(), mesazh.PershkrimMesazhi, nrreshta };
                    err.Rows.Add(arr);
                    continue;
                }
                ///ruajme dokumentin e ri duke patur parasysh kushtin ndryshostatusdokgjenerues true pra dokumentit ekzistues i shkon statusi gjeneruar true
                ///dhe nuk mund te gjenerohet me
                ///
                try
                {
                    bool printofature;
                    string mesazhmevonshem = "";
                    mesazh = kokare.ruaj(idGjuha, serverUrl, true, hidden, idPeriudhaKontabel, new colKonvertimi(), gjenerodokmagazine, out veprimebanka, 0, StatusAprovimi.Undefined, 0, out shfaqmesazhapolupemagazina, out shfaqmesazhapolupebanka, out shfaqmesazhapolupeVDK, new clsKokaShitje(), 0, 0, dergoemail, eshteOwn, dergoemailVFOne, "", new DbCore.DbAsete.colSerialetMagazine(), konfamortizimi, new clsKokaShitje(), out printofature, out printogarancifature, out pageseFature, mekontabilizim, out shfaqmesazhapolupe, konfig.KodKonfigAmbjente, false, "", 0, false, false, false, true, "", "", "", false, false, false, "", false, false, false, false, false, new colKokaShitje (),kontrolloIMEIFifo,false,false, ref dbData,false,"","",false, out mesazhmevonshem, false,true,"","");
                    if (!mesazh.Status)
                    {
                        object[] arr = { konfig.KodKonfigAmbjente + " " + k.NrDok + " " + k.DtDok.ToShortDateString(), mesazh.PershkrimMesazhi, nrreshta };
                        err.Rows.Add(arr);
                        continue;
                    }
                }
                catch (Exception ec)
                {
                    NLog.LogManager.GetCurrentClassLogger().Error(ec.Message);
                }
            }
            ///ruajme gabimet ne sesion dhe nqs ka gabime hapim griden e gabimeve
            
            DbCore.mySessionObjects.ruajTabeleGabimeshImporti(Session, err);
            if (err.Rows.Count > 0)
            {
                clsKokaErrorImporti koka;

                koka = new clsKokaErrorImporti(0, "Nga gjenerimi automatik", 100, idNdermarrje, idPerdoruesi);
                koka.ColTrupi.mbushErrorImportiNgaProgrami(err);
                mesazh = koka.ruajErrorImporti();
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "U gjeneruan " + (col.Count - err.Rows.Count) + " rreshta dhe deshtuan " + err.Rows.Count + " rreshta! ", pnlMesazhi);
                Container.Attributes["src"] = "RaportiShpejte.aspx?Sesioni=false&emriReal=gabimeImporti&printo=false&db=jo";
            }
            else
            {
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "U gjeneruan te gjitha rreshtat!", pnlMesazhi);
            }
            gvGjenerimi.Selection.UnselectAll();
            gvGjenerimi2.Selection.UnselectAll();
            DataTable dtt = DbCore.DbRegjistrim.colKokaShitje.merrKokaShitjeDTTePaGjeneruara(1, cmbLlojDate.Text, 1, 1, false, dtePeriudhaNga.Date.ToShortDateString(), dtePeriudhaDeri.Date.ToShortDateString());    
            DbCore.mySessionObjects.ruajColArtikull2NeSesion(Session, dtt);
            mbushNgaDb();
        }

        protected void gvGjenerimi_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpNoRows"] = gvGjenerimi.VisibleRowCount;
            e.Properties["cpNoPage"] = gvGjenerimi.PageIndex;
        }
        protected void gvGjenerimi2_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpNoRows"] = gvGjenerimi2.VisibleRowCount;
            e.Properties["cpNoPage"] = gvGjenerimi2.PageIndex;
        }


        protected void gvGjenerimi2_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType == DevExpress.Web.GridViewRowType.Data)
            {
                GridViewDataTextColumn col0 = gvGjenerimi2.Columns["Fshi"] as GridViewDataTextColumn;
                ASPxButton btn0 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col0, "btn") as ASPxButton;
                if (btn0 != null)
                {
                    btn0.ClientInstanceName = "btnFshiTVSH" + e.VisibleIndex.ToString();
                    btn0.ClientSideEvents.Click = "function(s,e){FshiClicked(" + e.VisibleIndex.ToString() + ");}";
                }
            }
        }


    }
}