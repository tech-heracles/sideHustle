using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.Types;
using PlatinumWeb.ApplicationUtils.Pages;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using DbCore.IMBUtils.Messages;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;

namespace PlatinumWeb
{
    public partial class ProfesioneTitujPune : MyPageBase
    {
        private  string kaveprime = "Ka veprime me kete profesion";
        private  string kaveprime1 = "Ka veprime me kete pozicion";
        private  string mesazhfshirjesukses = "Fshirja perfundoi me sukses!";
        private  string mesazhfshirjegabimi = "Fshirja perfundoi me gabime!";
        private  string gabimEkzistence = "Ekziston nje profesion me kete kod. Ju lutem shenoni nje tjeter!";
        private  string gabimEkzistencePozicion = "Ekziston nje pozicion pune me kete kod. Ju lutem shenoni nje tjeter!";
        private  string gabimEkzistence1 = "Ekziston nje profesion me kete pershkrim anglisht. Ju lutem shenoni nje tjeter!";
        private  string gabimEkzistencePozicion1 = "Ekziston nje pozicion pune me kete pershkrim anglisht. Ju lutem shenoni nje tjeter!";
        private string gabimEkzistencePozicionShqip = "Ekziston nje pozicion pune me kete pershkrim. Ju lutem shenoni nje tjeter!";
        private int idndermarje, idperdoruesi, idnderviti, idviti;

        private CultureInfo ci;
        private ResourceManager rm;
        /// <summary>
        /// kur faqja lodohet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                Response.Redirect(DbCore.IMBUtils.Paths.defaultLoginPath);
            }
            ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
         
            idndermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            idperdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            idnderviti = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
            idviti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            //if (CacheLayer.GlobalCacheManager.MySessionCache["KodiNdermarrjes"] == null)
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idperdoruesi);
            }

            percaktoTemplateMenu(ASPxMenu1, idviti, idperdoruesi, idndermarje);
            
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idndermarje, "gvProfesione", 1, "ProfesioneTitujPune.aspx");
            if (!IsPostBack)
            {
                perkthime();
                if (Request.QueryString["ruaj"] == "ok")
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("labelRaportMesazhRuajtjaPerfundoiSukses", ci), pnlMesazhi);
                ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idperdoruesi, idndermarje, cmbKonfigurimi, 106, rm, ci, DbCore.mySessionObjects.ktheGjuhe(Session));
           
                hfState.Set("idPerdoruesi", idperdoruesi);
                hfState.Set("idGjuha", DbCore.mySessionObjects.ktheGjuhe(Session));
                hfState.Set("idNdermarrje", idndermarje);
                cmbKonfigurimi.SelectedIndex = 0;
                DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()), DbCore.mySessionObjects.ktheGjuhe(Session));
                hfKonffillestar.Value = String.Format("{0};{1}", konf.KodKonfigAmbjente, konf.PershkrimKonfigAmbjente);
                MbushGridatNgaDatabaza();
                konfiguroGridat();
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idperdoruesi, idndermarje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "ProfesioneTitujPune.aspx");
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idndermarje, "gvProfesione", int.Parse(cmbKonfigurimi.Value.ToString()), "ProfesioneTitujPune.aspx");
            }
            
          AspxWebControlUtils.perkthePopUp(popFshi, rm.GetString("labelKujdes", ci), lblMsgbox, rm.GetString("labelAdministrimiMsgJeniSigurt", ci), ButtonCancel, rm.GetString("labelAnullo", ci));
           
            MbushGridatNgaDatabaza();
            perktheLabel();
        }

        /// <summary>
        /// Vendos emrat e tabeve ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void perkthime()
        {
            ASPxPageControl1.TabPages[0].Text = rm.GetString("tabTitleProfesione", ci);
            ASPxPageControl1.TabPages[1].Text = rm.GetString("tabTitlePozicionePune", ci);
            konfigurimi_Label.Text = rm.GetString("labelAdministrimiModeli", ci);
        }


        public void perktheLabel()
        {
            ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            kaveprime = rm.GetString("msgKaVeprimeMeKeteProfesion", ci);
            kaveprime1 = rm.GetString("msgKaVeprimeMeKetePozicion", ci);
            mesazhfshirjesukses = rm.GetString("msgFshirjaMeSukses", ci);
            mesazhfshirjegabimi = rm.GetString("msgFshirjaMeGabime", ci);
            gabimEkzistence = rm.GetString("msgEkzistonProfesioniMeKeteKod", ci);
            gabimEkzistencePozicion = rm.GetString("msgEkzistonPozicionPuneMeKeteKod", ci);
            gabimEkzistence1 = rm.GetString("msgEkzistonProfesioniMeKetePershkrimEng", ci);
            gabimEkzistencePozicion1 = rm.GetString("msgEkzistonProzicionPuneMeKetePershkrimEng", ci);
            gabimEkzistencePozicionShqip = rm.GetString("msgEkzistonProzicionPuneMeKetePershkrim", ci);
            hfState.Set("msgNukKeniDrejtaPerVeprim", rm.GetString("msgNukKeniDrejtaPerVeprim", ci));
            hfState.Set("msgEkzistonProfesionPozicion", rm.GetString("msgEkzistonProfesionPozicion", ci));
        }


        /// <summary>
        /// mbush gridat me te dhena
        /// </summary>
        private void MbushGridatNgaDatabaza()
        {
            DbCore.DbListPagesat.colProfesioneTitujPune col1 = new DbCore.DbListPagesat.colProfesioneTitujPune(idndermarje, 1);
            gvProfesione.DataSource = col1;
            gvProfesione.DataBind();
            DbCore.DbListPagesat.colProfesioneTitujPune col2 = new DbCore.DbListPagesat.colProfesioneTitujPune(idndermarje, 2);
            gvtituj.DataSource = col2;
            gvtituj.DataBind();
            DbCore.mySessionObjects.ruajObjectNeSesion(Session, col1, "profesione");
            DbCore.mySessionObjects.ruajObjectNeSesion(Session, col2, "tituj");

        }
        private void MbushGridatNgaSesioni()
        {
            object tmpProf = DbCore.mySessionObjects.merrObjectNgaSesioni(Session, "profesione");
            DbCore.DbListPagesat.colProfesioneTitujPune colProf = (tmpProf as DbCore.DbListPagesat.colProfesioneTitujPune) ?? new DbCore.DbListPagesat.colProfesioneTitujPune(idndermarje, 1);

            object tmpTituj = DbCore.mySessionObjects.merrObjectNgaSesioni(Session, "tituj");
            DbCore.DbListPagesat.colProfesioneTitujPune colTituj = (tmpTituj as DbCore.DbListPagesat.colProfesioneTitujPune) ?? new DbCore.DbListPagesat.colProfesioneTitujPune(idndermarje, 21);

            gvProfesione.DataSource = colProf;
            gvProfesione.DataBind();
            
            gvtituj.DataSource = colTituj;
            gvtituj.DataBind();

        }
        /// <summary>
        /// konfiguron gridat
        /// </summary>
        private void konfiguroGridat()
        {
            konfiguroGride(gvProfesione);
            konfiguroGride(gvtituj);
   
        }
        /// <summary>
        /// konfiguron griden
        /// </summary>
        /// <param name="grida">grida</param>
        private void konfiguroGride(ASPxGridView grida)
        {

            GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), "gvProfesione", grida, cmbKonfigurimi.Text.Split(';')[0],"718", DbCore.mySessionObjects.ktheGjuhe(Session));
            if (grida.ID == "gvtituj")
            {
                grida.Columns["Pershkrimi"].Caption = rm.GetString("labelTitullshqip", ci);
                grida.Columns["PershkrimiAng"].Caption = rm.GetString("labelTitullanglisht", ci);
            }
            else grida.Columns["PershkrimiAng"].Visible = false;
          

            GridUtil.konfiguroGrideListeEvogelPaTheme(grida, "Id");
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
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, "ProfesioneTitujPune.aspx", this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, hfRuaj.Value == "Ruaj" ? true : false, true, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
        }

        /// <summary>
        /// ben databound menune
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(ASPxMenu1, idviti, idperdoruesi, idndermarje);
        }
        /// <summary>
        /// fshin rreshtin e fokusuar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonOk_Click2(object sender, EventArgs e)
        { //per t'u rregulluar sipas kodifikimit te kf
            int index = ASPxPageControl1.ActiveTabIndex;
            ASPxGridView grida;
            if (index == 0) grida = gvProfesione;
            else  grida = gvtituj;


            int a = grida.FocusedRowIndex;
            grida.Selection.SelectRow(a);
            object tmpProf = DbCore.mySessionObjects.merrObjectNgaSesioni(Session, "profesione");
            DbCore.DbListPagesat.colProfesioneTitujPune colProf = (tmpProf as DbCore.DbListPagesat.colProfesioneTitujPune) ?? new DbCore.DbListPagesat.colProfesioneTitujPune(idndermarje, 1);
            object tmpTituj = DbCore.mySessionObjects.merrObjectNgaSesioni(Session, "tituj");
            DbCore.DbListPagesat.colProfesioneTitujPune colTituj = (tmpTituj as DbCore.DbListPagesat.colProfesioneTitujPune) ?? new DbCore.DbListPagesat.colProfesioneTitujPune(idndermarje, 21);
            List<object> rreshtat = grida.GetSelectedFieldValues("Id");

            foreach (int id in rreshtat)
            {
                DbCore.DbListPagesat.clsProfesioneTitujPune kat = new DbCore.DbListPagesat.clsProfesioneTitujPune(id);

                if (DbCore.DbListPagesat.clsProfesioneTitujPune.kaVeprimi(kat.Id))
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo,  kat.Lloji==1?kaveprime:kaveprime1, pnlMesazhi);
                else
                {
                    kat.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                    DbCore.clsMesazh mesazh = kat.fshi();
                    if (mesazh.Status)
                    {
                        clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazhfshirjesukses, pnlMesazhi);
                        if(kat.Lloji == 1)
                            colProf.Remove(colProf.Find(x => x.Id == id));
                        else
                            colTituj.Remove(colTituj.Find(x => x.Id == id));
                    }
                    else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhfshirjegabimi, pnlMesazhi);

                }
                DbCore.mySessionObjects.ruajObjectNeSesion(Session, colProf, "profesione");
                DbCore.mySessionObjects.ruajObjectNeSesion(Session, colTituj, "tituj");
                MbushGridatNgaSesioni();

            }
            pnlGridaProfesione.Update();
            pnlGridaTituj.Update();
        }
        /// <summary>
        /// ruan filtrin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            int index = ASPxPageControl1.ActiveTabIndex;
            ASPxGridView grida;
            if (index == 0) grida = gvProfesione;
            else  grida = gvtituj;

            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvProfesione", "ProfesioneTitujPune.aspx", idNdermarrje);
            DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida() { FiltraKodi = cmbFiltra.Text, FiltraShenime = cmbFiltra.Text, FiltraUniversal = false, GridaKokaId = koka.IdGridaKoka, FiltraVlera = grida.FilterExpression, IdPerdoruesi = idperdoruesi, IdNdermarje = idNdermarrje, IdStatusDok = 1 };
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("Kodi", grida);
       
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            mesazh = filtri.ruaj();
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvProfesione", int.Parse(cmbKonfigurimi.Value.ToString()), "ProfesioneTitujPune.aspx");
            percaktoTemplateMenu(ASPxMenu1, idviti, idperdoruesi, idndermarje);
            if (mesazh)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            cmbFiltra.Text = "";
        }
        /// <summary>
        /// fshin filtrin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            int index = ASPxPageControl1.ActiveTabIndex;
            ASPxGridView grida;
            if (index == 0) grida = gvProfesione;
            else  grida = gvtituj;
        
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvProfesione", "ProfesioneTitujPune.aspx", idNdermarrje);
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                mesazh = filtra.fshi();
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvProfesione", int.Parse(cmbKonfigurimi.Value.ToString()), "ProfesioneTitujPune.aspx");
                percaktoTemplateMenu(ASPxMenu1, idviti, idperdoruesi, idndermarje);
                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                grida.FilterExpression = String.Empty;
            }
        }
        /// <summary>
        /// ben insert te rreshtit te ri
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            //merr te dhenat e rreshtit te ri te grides
            ASPxGridView gride = sender as ASPxGridView;
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
            
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idperdoruesi, idndermarje, idviti, "ProfesioneTitujPune.aspx");
            if (!tedrejtaInfo.DShtim)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                gride.ShtoMesazhErrori(MessagesResource.Messages["msgNukKeniTeDrejta"]);
                return;
            }
            if (e.NewValues["Aktiv"] == null)
                e.NewValues["Aktiv"] = false;

            hfNrAuto = (ASPxHiddenField)DbCore.DbAdmin.NrAuto.VendosVleratNrAutoPerKod(hfNrAuto, e.NewValues["Kodi"].ToString());

            DbCore.DbListPagesat.clsProfesioneTitujPune kategori = new DbCore.DbListPagesat.clsProfesioneTitujPune() {
                Kodi = e.NewValues["Kodi"].ToString(),
                Pershkrimi = e.NewValues["Pershkrimi"].ToString(),
                PershkrimiAng = (gride.ClientInstanceName == "gvProfesione")?e.NewValues["Pershkrimi"].ToString():e.NewValues["PershkrimiAng"].ToString(),
                IdNdermarje = idndermarje,
                IdPerdoruesi = idperdoruesi,
                IdStatusDok = 1,
                IdKrijuesi =idperdoruesi,
                IdKonfig =int.Parse(cmbKonfigurimi.Value.ToString()),
                Aktiv=bool.Parse(e.NewValues["Aktiv"].ToString()),
                Lloji = gride.ClientInstanceName == "gvProfesione" ? 1 : 2
            };

            hfNrAutoKF = (ASPxHiddenField)DbCore.DbAdmin.NrAuto.ShtoNeRegjistrime(hfNrAutoKF, hfNrAuto, "Kodi", "Kodi");
            e.Cancel = true;
            gride.CancelEdit();
            DbCore.clsMesazh mesazh = kategori.ruaj(hfNrAutoKF);
            gride.ShtoMesazhNeGride(mesazh);
            MbushGridatNgaDatabaza();   
        }
        /// <summary>
        /// ben update te rreshtit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e )
        {
            ASPxGridView gride = sender as ASPxGridView;
            String id = e.Keys["Id"].ToString();
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idperdoruesi, idndermarje, idviti, "ProfesioneTitujPune.aspx");
            if (!tedrejtaInfo.DMod)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                gride.ShtoMesazhErrori(MessagesResource.Messages["msgNukKeniTeDrejta"]);
                return;
            }
            DbCore.clsMesazh m = new DbCore.clsMesazh();
            int gjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            DbCore.DbListPagesat.clsProfesioneTitujPune kategori = new DbCore.DbListPagesat.clsProfesioneTitujPune() { Kodi = e.NewValues["Kodi"].ToString(), Pershkrimi = e.NewValues["Pershkrimi"].ToString(), PershkrimiAng = (gride.ClientInstanceName == "gvProfesione") ? e.NewValues["Pershkrimi"].ToString() : e.NewValues["PershkrimiAng"].ToString(), IdNdermarje = idndermarje, IdPerdoruesi = idperdoruesi, IdStatusDok = 1, Id = int.Parse(id), Aktiv = bool.Parse(e.NewValues["Aktiv"].ToString()), IdKonfig = int.Parse(cmbKonfigurimi.Value.ToString()) };
            if (gride.ClientInstanceName == "gvProfesione") kategori.Lloji = 1;
            else if (gride.ClientInstanceName == "gvtituj") kategori.Lloji = 2;
           
            e.Cancel = true;
            m = kategori.modifiko();
            m.PershkrimMesazhi = MessagesResource.Messages["msgModifikimiMeSukses"];
            gride.ShtoMesazhNeGride(m);
            gride.CancelEdit();
            MbushGridatNgaDatabaza();
        }

        /// <summary>
        /// validon te dhenat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {//per tu rregulluar me vone
            //validimi ne jane plotesuar gjithe fushat e detyruara

            ASPxGridView grida = sender as ASPxGridView;
            foreach (GridViewColumn column in grida.Columns)
            {
                if (column.Visible)
                {
                    GridViewDataColumn dataColumn = column as GridViewDataColumn;
                    if (dataColumn == null) continue;
                    if (dataColumn.FieldName == "Aktiv" && e.NewValues[dataColumn.FieldName] == null)
                        e.NewValues[dataColumn.FieldName] = false;
                    if (e.NewValues[dataColumn.FieldName] == null)//validimi per kolonat e detyrueshme
                    {
                        e.Errors[dataColumn] = rm.GetString("msgVleraNukMundTeJeteNull", ci);
                    }
                    if (e.NewValues["Kodi"] == null)
                    {
                        e.Errors[dataColumn] = rm.GetString("msgKodiNukMundTeJeteBosh",ci);
                    }
                }
            }
            if (e.NewValues["Kodi"] != null)
            {
                try
                {
               
                    int tipi = grida.ClientInstanceName == "gvProfesione" ? 1 : 2;
                    
                    if (tipi==2 &&DbCore.DbListPagesat.clsProfesioneTitujPune.ekzistonProfesionTitujPunePershkrimiAng(e.NewValues["PershkrimiAng"].ToString(), idndermarje, tipi,hfRuaj.Value == "Ruaj"?0: int.Parse(e.Keys["Id"].ToString())))
                        e.RowError = gabimEkzistencePozicion1;

                    if (DbCore.DbListPagesat.clsProfesioneTitujPune.ekzistonProfesionTitujPunePershkrimi(e.NewValues["Pershkrimi"].ToString(), idndermarje, tipi, hfRuaj.Value == "Ruaj" ? 0 : int.Parse(e.Keys["Id"].ToString())))
                        e.RowError = tipi == 1 ? gabimEkzistence1 : gabimEkzistencePozicionShqip;

                    if (hfRuaj.Value == "Ruaj" && DbCore.DbListPagesat.clsProfesioneTitujPune.ekzistonProfesionTitujPune(e.NewValues["Kodi"].ToString(), idndermarje, tipi))
                        e.RowError = tipi == 1 ? gabimEkzistence : gabimEkzistencePozicion;

                }
                catch (Exception ex)
                {
                    ImbLogger.Error(ex.Message);
                    e.RowError = ex.Message;
                }


            }
            else e.RowError = rm.GetString("msgKodiNukMundTeJeteBosh", ci);
           
            if (e.Errors.Count > 0)
            {
                e.RowError = rm.GetString("msgStrukturaAdministrativePlotesoFushat",ci);
            }
            if (string.IsNullOrEmpty(e.RowError) && e.Errors.Count > 0)
            {
                e.RowError = rm.GetString("msgStrukturaAdministrativeKorrigjoGabimet", ci);
            }
        }
        /// <summary>
        /// kur reshti fillon te editohet therret metoden per validim
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void startRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {//kur fillon editimi te behet validimi
            ASPxGridView grida = sender as ASPxGridView;
            if (!grida.IsNewRowEditing)
            {
                grida.DoRowValidation();
            }
        }
        /// <summary>
        /// kur grida ben callback
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridat_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
           
        }
        /// <summary>
        /// kur inicializohet rreshti i ri
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void initNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {

        }
        /// <summary>
        /// kur grida ben callback nga perdoruesi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridat_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            int index = ASPxPageControl1.ActiveTabIndex;
            ASPxGridView grida;
            if (index == 0) grida = gvProfesione;
            else  grida = gvtituj;
           

            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                if (arr[2] == "")
                    grida.FilterExpression = "";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    //filtra.mbushFiltraGridaSipasFiltraKodi(arr[2], idndermarje);
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvProfesione", "ProfesioneTitujPune.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        grida.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, grida);

                        MbushGridatNgaDatabaza();

                    }
                }
            }
            konfiguroGride(grida);
        }
        /// <summary>
        /// vendos property te grides per tu aksesuar nga javascripti
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridat_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            ASPxGridView grida = sender as ASPxGridView;
            e.Properties["cpPageIndex"] = grida.PageIndex;
            e.Properties["cpPageRow"] = grida.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = grida.VisibleRowCount;
        }
        /// <summary>
        /// filtrimet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridat_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            string TeGjithe = rm.GetString("GridHeaderFilterFillItemTeGjithe", ci);
                e.Values.Clear();
                e.AddValue(TeGjithe, string.Empty, "true");
        }
      protected void gridat_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if(e.Column.FieldName == "gvtituj")
            {
                if (Converter.ConvertToInt(e.Value)==0)
                {
                    e.Criteria = null;
                }
            }

        }
    protected void btnXlsxExport_Click1(object sender, EventArgs e)
        {
            try
            {
                gridExport1.WriteXlsxToResponse("Profesionet", true);
            }
            catch (Exception ex)
            {
            }
        }
        protected void btnPdfExport_Click1(object sender, EventArgs e)
        {
            try
            {
                gridExport1.WritePdfToResponse("Profesionet", true);
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnXlsxExport_Click2(object sender, EventArgs e)
        {
            try
            {
                gridExport2.WriteXlsxToResponse("Titujt", true);
            }
            catch (Exception ex)
            {
            }
        }
        protected void btnPdfExport_Click2(object sender, EventArgs e)
        {
            try
            {
                gridExport2.WritePdfToResponse("Titujt", true);
            }
            catch (Exception ex)
            {
            }
        }
    }
}