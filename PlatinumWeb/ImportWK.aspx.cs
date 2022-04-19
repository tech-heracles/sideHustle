using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Data;
using DbCore.DbAdmin;
using System.Data.OleDb;
using System.Collections;
using DbCore.DbKontabiliteti;
using System.Resources;
using System.Globalization;
using DbCore;
using DbCore.DbShare;
using DbCore.IMBUtils.DataBase;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    public partial class ImportWK : MyPageBase
    {
        System.Globalization.CultureInfo ci;
        System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

        /// <summary>
        /// perdoret per te vendosur theme
        /// </summary>
        /// <param name="source">derguesi</param>
        /// <param name="e">argumentat</param>


        /// <summary>
        /// mbush te dhenat kur faqja ben loadim
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                DbCore.clsFunksione.logout(Session, true, "FaqePaautorizuar");
            }
            int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerdoruesi);
            }
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idNderViti = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
            int idviti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            percaktoTemplateMenu(ASPxMenu1, idviti, idPerdoruesi, idNdermarrje);
            mbushHiddenFieldMePerkthime(rm, ci);
            if (!IsPostBack)
            {
                DbCore.mySessionObjects.ruajRreshtaImportiNgaGrida(Session, new DataTable());
                DbCore.mySessionObjects.ruajTabeleGabimeshImporti(Session, new DataTable());
                percaktoTemplateMenu(ASPxMenu1, idviti, idPerdoruesi, idNdermarrje);
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                if (Request.QueryString["lloji"] == "importwk")
                    tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "ImportWK.aspx?lloji=importwk");
                else if (Request.QueryString["lloji"] == "importtollona")
                    tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "ImportWK.aspx?lloji=importtollona");
                else if (Request.QueryString["lloji"] == "importtollonaleter")
                    tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "ImportWK.aspx?lloji=importtollonaleter");
                else
                    if (Request.QueryString["lloji"] == "importtollonaelektronik")
                    tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "ImportWK.aspx?lloji=importtollonaelektronik");
                else
                        if (Request.QueryString["lloji"] == "importtollonaelektronikspecifik")
                    tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "ImportWK.aspx?lloji=importtollonaelektronikspecifik");
                else
                    tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "ImportWK.aspx?lloji=importfk");

                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                mbushPopUpListeNgaDB();
            }
            else
                mbushGrideNgaSessioni();
            konfiguroGride();
            gvImport.Columns["#"].VisibleIndex = 0;
            gvImport.Columns["#"].Width = 5;
        }


        private void mbushHiddenFieldMePerkthime(ResourceManager rm, CultureInfo ci)
        {

            hfTeDrejta.Set("msgSkaRreshtaPerImport", rm.GetString("msgSkaRreshtaPerImport", ci));
            hfTeDrejta.Set("MsgSkaRreshtaGridaPerKontroll", rm.GetString("MsgSkaRreshtaGridaPerKontroll", ci));
            hfTeDrejta.Set("msgKontrolliPerGabimet", rm.GetString("msgKontrolliPerGabimet", ci));

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
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, DbCore.clsFunksione.GetKomponente(Page.Request), this, MenuInfo, true, true, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
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
        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {
            if (e.Item.Name == "Kontrollo")
            {
                int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                kontrolloGride(idNdermarrje);
            }
            if (e.Item.Name == "Importo")
            {
                int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                ruajGride(idNdermarrje);
            }
        }

        private void ruajGride(int idNdermarrje)
        {
            try
            {
                DataTable dt = DbCore.mySessionObjects.merrRreshtaImportiNgaGrida(Session);
                int rreshta = dt.Rows.Count;
                DataTable err = new DataTable();
                err.Columns.Add("Kodi");
                err.Columns.Add("Gabimi");
                err.Columns.Add("Rreshti");
                DataTable rreshtaok = new DataTable();
                DataTable rreshtajoOk = new DataTable();
                rreshtajoOk = dt.Clone();
                rreshtaok = dt.Copy();
                CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
                ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                if (Request.QueryString["lloji"] == "importwk")
                    kontrolloShitje(idNdermarrje, rreshtaok, err, rreshtajoOk, true, 5);
                else if (Request.QueryString["lloji"] == "importtollona")
                    kontrolloShitjeTollona(idNdermarrje, rreshtaok, err, rreshtajoOk, true, 0);
                else if (Request.QueryString["lloji"] == "importtollonaleter")
                    kontrolloShitjeTollonaLeter(idNdermarrje, rreshtaok, err, rreshtajoOk, true, 0);
                else if (Request.QueryString["lloji"] == "importtollonaelektronik")
                    kontrolloShitjeTollonaElektronik(idNdermarrje, rreshtaok, err, rreshtajoOk, true, 0);
                else if (Request.QueryString["lloji"] == "importtollonaelektronikspecifik")
                    kontrolloShitjeTollonaElektronikSpecifik(idNdermarrje, rreshtaok, err, rreshtajoOk, true, 0);
                else kontrolloFleteKont(idNdermarrje, dt, err, rreshtajoOk, true, 2, rm, cultinf);

                DbCore.mySessionObjects.ruajTabeleGabimeshImporti(Session, err);

                if (err.Rows.Count > 0)
                {
                    clsKokaErrorImporti koka;
                    if (Request.QueryString["lloji"] == "importwk")
                        koka = new clsKokaErrorImporti(0, "Nga importi i shitjeve nga winline karta", 1, idNdermarrje, DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
                    else if (Request.QueryString["lloji"] == "importfk")
                        koka = new clsKokaErrorImporti(0, "Nga importi i fleteve kontabel", 5, idNdermarrje, DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
                    else if (Request.QueryString["lloji"] == "importtollonaleter")
                        koka = new clsKokaErrorImporti(0, "Nga importi i shitjeve me tollona leter", 1, idNdermarrje, DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
                    else if (Request.QueryString["lloji"] == "importtollonaelektronik")
                        koka = new clsKokaErrorImporti(0, "Nga importi i shitjeve me tollona elektronik", 1, idNdermarrje, DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
                    else if (Request.QueryString["lloji"] == "importtollonaelektronikspecifik")
                        koka = new clsKokaErrorImporti(0, "Nga importi i shitjeve me tollona elektronik specifik", 1, idNdermarrje, DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
                    else
                        koka = new clsKokaErrorImporti(0, "Nga importi i shitjeve me tollona", 1, idNdermarrje, DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
                    koka.ColTrupi.mbushErrorImportiNgaProgrami(err);
                    DbCore.clsMesazh mesazh = koka.ruajErrorImporti();
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgImportuan", ci) + " " + (rreshta - err.Rows.Count) + " " + rm.GetString("msgRreshtatDeshtuan", ci) + " " + " " + err.Rows.Count + " " + rm.GetString("msgHapniListenGabimeve", ci), pnlMesazhi);
                }
                else
                {
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("msgImportimMeSukses", ci), pnlMesazhi);
                    mbushPopUpListeNgaDB();
                }

            }
            catch (Exception e)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(e.Message);
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, e.Message, pnlMesazhi);
            }
        }
        private void kontrolloShitjeTollona(int idNdermarrje, DataTable dt, DataTable gabime, DataTable tePaImportuara, bool importo, int pozicionkodi)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            int idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            System.Globalization.CultureInfo ci = MessagesResource.Messages.CurrentCultureInfo;
            bool eshteOwn = DbCore.mySessionObjects.merrEshteOwnSesioni(Session);
            int idperdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            int idndermvit = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);

            DbCore.DbTollona.colShitjeMeSerial.kontrolloShitjeTollona(idNdermarrje, dt, gabime, tePaImportuara, importo, pozicionkodi, cultinf, rm, idGjuha, eshteOwn, idperdoruesi, idndermvit);
            if (importo)
            {
                DbCore.mySessionObjects.ruajRreshtaImportiNgaGrida(Session, tePaImportuara);
                gvImport.DataSource = tePaImportuara;
                gvImport.DataBind();
                status1.Value = "import";
            }
        }
        private void kontrolloShitjeTollonaLeter(int idNdermarrje, DataTable dt, DataTable gabime, DataTable tePaImportuara, bool importo, int pozicionkodi)
        {

            int idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            System.Globalization.CultureInfo ci = MessagesResource.Messages.CurrentCultureInfo;
            bool eshteOwn = DbCore.mySessionObjects.merrEshteOwnSesioni(Session);
            int idperdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            int idndermvit = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
            DbCore.DbTollona.colTollonaLeter.kontrolloShitjeTollona(idNdermarrje, dt, gabime, tePaImportuara, importo, pozicionkodi, MessagesResource.Messages.CurrentCultureInfo, MessagesResource.CurrentResourceManager, idGjuha, eshteOwn, idperdoruesi, idndermvit);
            if (importo)
            {
                DbCore.mySessionObjects.ruajRreshtaImportiNgaGrida(Session, tePaImportuara);
                gvImport.DataSource = tePaImportuara;
                gvImport.DataBind();
                status1.Value = "import";
            }
        }
        private void kontrolloShitjeTollonaElektronik(int idNdermarrje, DataTable dt, DataTable gabime, DataTable tePaImportuara, bool importo, int pozicionkodi)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            int idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            System.Globalization.CultureInfo ci = MessagesResource.Messages.CurrentCultureInfo;
            bool eshteOwn = DbCore.mySessionObjects.merrEshteOwnSesioni(Session);
            int idperdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            int idndermvit = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
            DbCore.DbTollona.colTollonaElektronik.kontrolloShitjeTollona(idNdermarrje, dt, gabime, tePaImportuara, importo, pozicionkodi, idGjuha, eshteOwn, idperdoruesi, idndermvit);
            if (importo)
            {
                DbCore.mySessionObjects.ruajRreshtaImportiNgaGrida(Session, tePaImportuara);
                gvImport.DataSource = tePaImportuara;
                gvImport.DataBind();
                status1.Value = "import";
            }
        }
        private void kontrolloShitjeTollonaElektronikSpecifik(int idNdermarrje, DataTable dt, DataTable gabime, DataTable tePaImportuara, bool importo, int pozicionkodi)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            int idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);

            bool eshteOwn = DbCore.mySessionObjects.merrEshteOwnSesioni(Session);
            int idperdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            int idndermvit = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
            DbCore.DbTollona.colTollonaElektronik.kontrolloShitjeTollonaSpecifik(idNdermarrje, dt, gabime, tePaImportuara, importo, pozicionkodi, MessagesResource.CurrentResourceManager, idGjuha, eshteOwn, idperdoruesi, idndermvit);
            if (importo)
            {
                DbCore.mySessionObjects.ruajRreshtaImportiNgaGrida(Session, tePaImportuara);
                gvImport.DataSource = tePaImportuara;
                gvImport.DataBind();
                status1.Value = "import";
            }
        }

        private void kontrolloGride(int idNdermarrje)
        {
            DataTable dt = DbCore.mySessionObjects.merrRreshtaImportiNgaGrida(Session);
            DataTable err = new DataTable();
            err.Columns.Add("Kodi");
            err.Columns.Add("Gabimi");
            err.Columns.Add("Rreshti");
            DataTable rreshtaok = new DataTable();
            DataTable rreshtajoOk = new DataTable();
            rreshtajoOk = dt.Clone();
            rreshtaok = dt.Copy();
            try
            {
                CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
                ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                if (Request.QueryString["lloji"] == "importwk")
                    kontrolloShitje(idNdermarrje, dt, err, rreshtajoOk, false, 5);
                else
                    if (Request.QueryString["lloji"] == "importtollona")
                    kontrolloShitjeTollona(idNdermarrje, dt, err, rreshtajoOk, false, 0);
                else if (Request.QueryString["lloji"] == "importtollonaleter")
                    kontrolloShitjeTollonaLeter(idNdermarrje, dt, err, rreshtajoOk, false, 0);
                else if (Request.QueryString["lloji"] == "importtollonaelektronik")
                    kontrolloShitjeTollonaElektronik(idNdermarrje, dt, err, rreshtajoOk, false, 0);
                else if (Request.QueryString["lloji"] == "importtollonaelektronikspecifik")
                    kontrolloShitjeTollonaElektronikSpecifik(idNdermarrje, dt, err, rreshtajoOk, false, 0);
                else kontrolloFleteKont(idNdermarrje, dt, err, rreshtajoOk, false, 2, rm, ci);

                DbCore.mySessionObjects.ruajTabeleGabimeshImporti(Session, err);

                if (err.Rows.Count > 0)
                {
                    clsKokaErrorImporti koka;
                    if (Request.QueryString["lloji"] == "importwk")
                        koka = new clsKokaErrorImporti(0, "Nga importi i shitjeve nga winline karta", 1, idNdermarrje, DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
                    else if (Request.QueryString["lloji"] == "importfk")
                        koka = new clsKokaErrorImporti(0, "Nga importi i fleteve kontabel", 5, idNdermarrje, DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
                    else if (Request.QueryString["lloji"] == "importtollonaleter")
                        koka = new clsKokaErrorImporti(0, "Nga importi i shitjeve me tollona leter", 1, idNdermarrje, DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
                    else if (Request.QueryString["lloji"] == "importtollonaelektronik")
                        koka = new clsKokaErrorImporti(0, "Nga importi i shitjeve me tollona elektronik", 1, idNdermarrje, DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
                    else if (Request.QueryString["lloji"] == "importtollonaelektronikspecifik")
                        koka = new clsKokaErrorImporti(0, "Nga importi i shitjeve me tollona elektronik specifik", 1, idNdermarrje, DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
                    else
                        koka = new clsKokaErrorImporti(0, "Nga importi i shitjeve me tollona", 1, idNdermarrje, DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
                    koka.ColTrupi.mbushErrorImportiNgaProgrami(err);
                    DbCore.clsMesazh mesazh = koka.ruajErrorImporti();



                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgGabimeNeRReshtatEgrides", ci), pnlMesazhi);
                }
                else clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("msgKontrolliKaloiMeSukses", ci), pnlMesazhi);
            }
            catch (Exception e)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(e.Message);
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgKontrolliKaloiPaSukses", ci), pnlMesazhi);
            }
        }

        private void kontrolloShitje(int idNdermarrje, DataTable dt, DataTable gabime, DataTable tePaImportuara, bool importo, int pozicionkodi)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasKod("FSH", idNdermarrje);
            string error = "";
            int idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            bool eshteOwn = DbCore.mySessionObjects.merrEshteOwnSesioni(Session);
            int i = 1;
            DbData dbData = new DbData();
            foreach (DataRow dr in dt.Rows)
            {
                DbCore.DbRegjistrim.clsKokaShitje koka = new DbCore.DbRegjistrim.clsKokaShitje();
                int idtransferimi, idshitje;
                bool statustransf;
                string klienti, monedha, nrshitje, nrserial, usertrasferimi;
                double kursi, vleftapatvsh, vleftametvsh, vleftazbritje;
                DateTime dateshitje, datemarrje;
                try
                {
                    idshitje = int.Parse(dr["ID_URDHER_SHITJE"].ToString());
                    idtransferimi = int.Parse(dr["ID_TRANSFERIM"].ToString());
                    statustransf = bool.Parse(dr["AW_STATUS_TRANSF"].ToString());
                    klienti = dr["AW_KLIENTI"].ToString();
                    monedha = dr["AW_MONEDHA"].ToString();
                    nrshitje = dr["AW_SHITJENR"].ToString();
                    nrserial = dr["AW_SHITJESERIAL"].ToString();
                    usertrasferimi = dr["AW_USERI_TRANSF"].ToString();
                    dateshitje = DateTime.Parse(dr["AW_SHITJEDATE"].ToString());
                    datemarrje = DateTime.Parse(dr["AW_DATA_MARRE_TRANS"].ToString());
                    vleftapatvsh = double.Parse(dr["AW_VLEFTAPATVSH"].ToString());
                    vleftametvsh = double.Parse(dr["AW_VLEFTAMETVSH"].ToString());
                    vleftazbritje = double.Parse(dr["AW_VLEFTAZBRITJE"].ToString());
                    kursi = double.Parse(dr["AW_SHITJEKURSI"].ToString());
                }
                catch (Exception ex)
                {
                    NLog.LogManager.GetCurrentClassLogger().Error(ex.Message);
                    error = ex.Message;
                    object[] arr = { dr[pozicionkodi], ex.Message, i };
                    gabime.Rows.Add(arr);
                    if (importo)
                    {
                        tePaImportuara.ImportRow(dr);
                        gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                    }
                    continue;
                }

                if (error != "")
                    continue;
                DbCore.DbRegjistrim.clsKokaShitje kokaurdhershitje = new DbCore.DbRegjistrim.clsKokaShitje();
                kokaurdhershitje.mbushKokaShitjeSipasIDPaTrup(idshitje);

                //clsPeriudhaKontabel per = new clsPeriudhaKontabel(dateshitje, kokaurdhershitje.IdNdermarrje);
                int idPeriudheKontabel = clsPeriudhaKontabel.ktheIdPeriudheSipasDatesDheNdermarrjes(dateshitje, kokaurdhershitje.IdNdermarrje);
                int loan = 0;
                DbCore.DbKontabiliteti.clsKlientFurnitor kf = new DbCore.DbKontabiliteti.clsKlientFurnitor();
                kf.mbushKlientFurnitorSipasKodit(klienti, kokaurdhershitje.IdNdermarrje);
                if (kf.LlojPorosie == 3)
                    loan = 1;//artikujt jane loan

                DbCore.DbRegjistrim.colTrupiShitje trupi = new DbCore.DbRegjistrim.colTrupiShitje();
                error = kontrolloTrupShitje(idtransferimi, trupi, loan);
                if (error != "")
                {
                    object[] arr = { dr[pozicionkodi], error, i };
                    gabime.Rows.Add(arr);

                    tePaImportuara.ImportRow(dr);
                    gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                    continue;
                }
                DbCore.DbRegjistrim.colKonvertimi colkonv = new DbCore.DbRegjistrim.colKonvertimi();

                DbCore.DbRegjistrim.clsKokaShitje kokameme = new DbCore.DbRegjistrim.clsKokaShitje();
                koka.krijoShitjePerImportWK(konf.IdNivel, kokaurdhershitje.IdTemplate, konf.IdKonfigAmbjente, klienti, kokaurdhershitje.IdProjekt, kokaurdhershitje.NrProjekt, dateshitje, nrshitje, nrserial, kokaurdhershitje.DtMaturimi, monedha, kursi, kokaurdhershitje.IdMenyreTransporti, kokaurdhershitje.DtTransportimi, kokaurdhershitje.IdKushtDergimi, kokaurdhershitje.IdAgjent, kokaurdhershitje.IdMenyrePagese, kokaurdhershitje.IdKushtPagese, vleftazbritje, vleftametvsh, vleftametvsh != 0 ? (vleftametvsh - vleftapatvsh) * (100 - (vleftazbritje / vleftametvsh)) : 0, kokaurdhershitje.DtRegjistrimi, kokaurdhershitje.IdStatusDok, kokaurdhershitje.IdNdermarrje, kokaurdhershitje.IdNdermarrjeVit, 0, 0, 0, 0, kokaurdhershitje.AdresaFaturimit, kokaurdhershitje.AdresaDergimit, kokaurdhershitje.Pershkrimi, kokaurdhershitje.Dogana, kokaurdhershitje.IdDegeAdministrative, kokaurdhershitje.IdPikeShitjeFurnizimi, DbCore.mySessionObjects.ktheIdPerdoruesi(Session), kokaurdhershitje.IdRaportDesing, trupi, true, konf.KodKonfigAmbjente, idPeriudheKontabel, new DbCore.DbShare.clsKonfigurimAmbjenti(), 0, "", true, kokaurdhershitje.IdGrup1, kokaurdhershitje.IdGrup2, kokaurdhershitje.IdGrup3, kokaurdhershitje.AfatKohor, kokaurdhershitje.Cash, kokaurdhershitje.StatusAprovimi, DbCore.mySessionObjects.ktheIdPerdoruesi(Session), kokaurdhershitje.PerqindjeAgjenti, kokaurdhershitje.StatusTransferimi, false, kokameme, 0, 0, true, true, kokaurdhershitje.EmerKlienti, kokaurdhershitje.Kontakti, kokaurdhershitje.DtFillimi, kokaurdhershitje.DtMbarimi, kokaurdhershitje.IdAutomjet, kokaurdhershitje.KilometraAuto, kokaurdhershitje.IdAgjenti2, kokaurdhershitje.PerqindjeAgjenti2, kokaurdhershitje.IdAgjenti3, kokaurdhershitje.PerqindjeAgjenti3, kokaurdhershitje.Marresi, kokaurdhershitje.ShpenzimeJoTeZbritshme, kokaurdhershitje.IdArka, kokaurdhershitje.DtFature, DateTime.Today, dbData, idGjuha, kokaurdhershitje.IIC, kokaurdhershitje.NIVF,kokaurdhershitje.IdOperator, kokaurdhershitje.EIC,kokaurdhershitje.Procesi,kokaurdhershitje.EInvoiceType,kokaurdhershitje.TipiIVetefaturimit,kokaurdhershitje.NivfKthim);
                DbCore.DbRegjistrim.clsKonvertimi konv = new DbCore.DbRegjistrim.clsKonvertimi(0, koka.IdShitjeKoka, kokaurdhershitje.IdShitjeKoka, kokaurdhershitje.IdKonfigAmbjente, koka.IdKonfigAmbjente);
                colkonv.Add(konv);
                string shfaqmesazhapolupe;
                if (importo)
                {
                    DbCore.DbArkaBanka.clsVeprimBankaKoka vep = new DbCore.DbArkaBanka.clsVeprimBankaKoka();
                    string mesazhvdk, mesazhmag, mesazhbanka;
                    bool printofature, printogarancifature, pagesefature;
                    try
                    {
                        bool kontrolloIMEIFifo = clsAlternativaKushti.getAlternativa(koka.IdKonfigAmbjente, "AFI") == "Po";
                        string mesazhmevonshem = "";
                        DbCore.clsMesazh mesazhinv = koka.ruaj(idGjuha, ci, true, new DevExpress.Web.ASPxHiddenField(), idPeriudheKontabel, colkonv, false, out vep, 0, kokaurdhershitje.StatusAprovimi, 0, out mesazhmag, out mesazhbanka, out mesazhvdk, kokameme, kokaurdhershitje.IdShitjeKoka, idtransferimi, false, eshteOwn, false, "", new DbCore.DbAsete.colSerialetMagazine(), new DbCore.DbShare.clsKonfigurimAmbjenti(), new DbCore.DbRegjistrim.clsKokaShitje(), out printofature, out printogarancifature, out pagesefature, true, out shfaqmesazhapolupe, "FSH", rm, false, "", 0, false, false, false, false, "", "", "", false, false, false, false, new DbCore.DbRegjistrim.colKokaShitje(), kontrolloIMEIFifo, false, false, ref dbData, false, "", "", false, out mesazhmevonshem, importo,true,"","");

                        if (!mesazhinv.Status)
                        {
                            object[] arr = { dr[pozicionkodi], mesazhinv.PershkrimMesazhi, i };
                            gabime.Rows.Add(arr);

                            tePaImportuara.ImportRow(dr);
                            gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                        }
                    }
                    catch (Exception ex)
                    {
                        NLog.LogManager.GetCurrentClassLogger().Error(ex.Message);
                    }
                }
                i++;
            }
            if (importo)
            {
                DbCore.mySessionObjects.ruajRreshtaImportiNgaGrida(Session, tePaImportuara);
                gvImport.DataSource = tePaImportuara;
                gvImport.DataBind();
                status1.Value = "import";
            }
            return;
        }


        private void kontrolloFleteKont(int idNdermarrje, DataTable dt, DataTable gabime, DataTable tePaImportuara, bool importo, int pozicionkodi, ResourceManager rm, CultureInfo ci)
        {
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasKod("FleteKontabel", idNdermarrje);
            string error = "";
            int idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            int idNdermViti = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
            int idPerdorues = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            bool eshteOwn = DbCore.mySessionObjects.merrEshteOwnSesioni(Session);
            DbCore.DbShare.clsKonfigurimAmbjenti konfig = new DbCore.DbShare.clsKonfigurimAmbjenti();
            int idPeriudha = DbCore.mySessionObjects.merrPeriudheKontabel(Session).IdPeriudha;
            int i = 1;
            foreach (DataRow dr in dt.Rows)
            {
                error = "";
                DbCore.DbRegjistrim.clsKokaShitje koka = new DbCore.DbRegjistrim.clsKokaShitje();
                DbCore.DbKontabiliteti.clsKokaFleteKontabel kokaFleteKont = new DbCore.DbKontabiliteti.clsKokaFleteKontabel();

                int idImportKoka, idDokumentImporti;
                string nrdok, pershkrimi, kodkonfigambjent;
                DateTime dtDok;
                double vlera;
                try
                {
                    idImportKoka = int.Parse(dr["ID_KOKAIMPORT"].ToString());
                    idDokumentImporti = int.Parse(dr["ID_DOKUMENTIMPORTUAR"].ToString());
                    nrdok = dr["NRKOKAFLETEKONTABEL"].ToString();
                    pershkrimi = dr["PERSHKRIMIFLETEKONTABEL"].ToString();
                    dtDok = DateTime.Parse(dr["DATEDOKUMENTIKOKAFLETEKONTABEL"].ToString());
                    kodkonfigambjent = dr["KONFIGAMBJENTI"].ToString();
                    vlera = double.Parse(dr["VLEFTAFLETEKONTABEL"].ToString());
                }
                catch (Exception ex)
                {
                    NLog.LogManager.GetCurrentClassLogger().Error(ex.Message);
                    error = ex.Message;
                    object[] arr = { dr[pozicionkodi], ex.Message, i };
                    gabime.Rows.Add(arr);
                    if (importo)
                    {
                        tePaImportuara.ImportRow(dr);
                        gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                    }
                    continue;
                }

                if (error != "")
                    continue;
                konfig.mbushKonfigAmbjSipasKod(kodkonfigambjent, idNdermarrje);
                DbCore.DbKontabiliteti.colTrupatFletetKontabel trupi = new DbCore.DbKontabiliteti.colTrupatFletetKontabel();
                error = kontrolloTrupFleteKont(idImportKoka, idDokumentImporti, idNdermarrje, trupi, dtDok);
                if (error != "")
                {
                    object[] arr = { dr[pozicionkodi], error, i };
                    gabime.Rows.Add(arr);
                    tePaImportuara.ImportRow(dr);
                    gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                    continue;
                }

                DbCore.clsMesazh mesazh = kokaFleteKont.KrijoFleteKontPerImportFk(MyConnectionsManager.GetSelectedConNameServer(), idNdermViti, nrdok, clsKokaFleteKontabel.GjeneroNrReference(idNdermViti).ToString(), dtDok, dtDok, kodkonfigambjent, 0, pershkrimi, idPerdorues, trupi, true, 5, 5, idPeriudha, idNdermarrje, krijoQenderRe(konfig, trupi, new DbCore.DbQendraKosto.colObjektivaKosto(), new List<double>(), new List<double>(), new List<int>(), 1, nrdok, pershkrimi, dtDok));

                if (!mesazh.Status)
                    error = mesazh.PershkrimMesazhi;

                if (error != "")
                {
                    object[] arr = { dr[pozicionkodi], error, i };
                    gabime.Rows.Add(arr);
                    tePaImportuara.ImportRow(dr);
                    gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                    continue;
                }

                if (importo)
                {
                    DbCore.DbArkaBanka.clsVeprimBankaKoka vep = new DbCore.DbArkaBanka.clsVeprimBankaKoka();
                    DbCore.clsMesazh mesazhinv = kokaFleteKont.ruajNgaImporti(idImportKoka, idDokumentImporti, rm, ci);

                    if (!mesazhinv.Status)
                    {
                        object[] arr = { dr[pozicionkodi], mesazhinv.PershkrimMesazhi, i };
                        gabime.Rows.Add(arr);

                        tePaImportuara.ImportRow(dr);
                        gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                    }
                }
                i++;
            }
            if (importo)
            {
                DbCore.mySessionObjects.ruajRreshtaImportiNgaGrida(Session, tePaImportuara);
                gvImport.DataSource = tePaImportuara;
                gvImport.DataBind();
                status1.Value = "import";
            }
            return;
        }

        private DbCore.DbQendraKosto.clsKokaQendraKosto krijoQenderRe(DbCore.DbShare.clsKonfigurimAmbjenti konfig, DbCore.DbKontabiliteti.colTrupatFletetKontabel colTrupFK, DbCore.DbQendraKosto.colObjektivaKosto objektivat, List<double> vleratobjektiva, List<double> vleratobjektivamonbaze, List<int> idllogobj, int statusdok, string nrDok, string pershkrimi, DateTime dtDok)
        {
            string shfaqmesazhapolupe = "jo";
            if (statusdok == 0)
            {
                return new DbCore.DbQendraKosto.clsKokaQendraKosto();
            }
            DbCore.DbShare.clsKonfigurimAmbjenti clsKonf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            clsKonf.mbushKonfigDefaultKomponentes(906, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));

            DbCore.DbShare.clsKusht kusht = new DbCore.DbShare.clsKusht(konfig.IdKonfigAmbjente, "ZRQK");
            if (kusht.Vlera != 0)
                clsKonf.mbushKonfigAmbjSipasId(kusht.Vlera);
            else
                clsKonf.mbushKonfigAmbjSipasKod("RQK", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            DbCore.DbQendraKosto.clsKokaQendraKosto qendravjeter = new DbCore.DbQendraKosto.clsKokaQendraKosto();
            colTrupatFletetKontabel trupipadublikime = new colTrupatFletetKontabel();
            foreach (clsTrupiFleteKontabel trup in colTrupFK)
            {
                bool ekziston = false;
                foreach (clsTrupiFleteKontabel trupiri in trupipadublikime)
                {
                    if (trup.IdLlogari == trupiri.IdLlogari)
                    {
                        ekziston = true;
                        if (trup.VleftaDebiMonBazeTrupiFleteKontabel != 0)
                        {
                            trupiri.VleftaDebiMonBazeTrupiFleteKontabel += trup.VleftaDebiMonBazeTrupiFleteKontabel;
                            trupiri.VleftaDebiTrupiFleteKontabel += trup.VleftaDebiTrupiFleteKontabel;
                        }
                        else if (trup.VleftaKrediMonBazeTrupiFleteKontabel != 0)
                        {
                            trupiri.VleftaKrediMonBazeTrupiFleteKontabel += trup.VleftaKrediMonBazeTrupiFleteKontabel;
                            trupiri.VleftaKrediTrupiFleteKontabel += trup.VleftaKrediTrupiFleteKontabel;
                        }
                        break;
                    }
                }
                if (!ekziston)
                {
                    clsTrupiFleteKontabel trupr = new clsTrupiFleteKontabel(trup.IdTrupiFleteKontabel, trup.IdKokaFleteKontabel, trup.IdLlogari, trup.PershkrimTrupiFleteKontabel, trup.IdMonedha, trup.Kursi, trup.VleftaDebiTrupiFleteKontabel, trup.VleftaKrediTrupiFleteKontabel, trup.KodMonedha, trup.KodiSkemaKontabel, trup.VleftaDebiMonBazeTrupiFleteKontabel, trup.VleftaKrediMonBazeTrupiFleteKontabel);
                    trupipadublikime.Add(trupr);
                }
            }
            bool rishpernda = clsAlternativaKushti.getAlternativa(clsKonf.IdKonfigAmbjente, "RSKDMD") == "Po";
            bool shperndaDifQKPModDok = clsAlternativaKushti.getAlternativa(clsKonf.IdKonfigAmbjente, "SHDQKPMD") == "Po";
            DbCore.DbQendraKosto.clsKokaQendraKosto koka = DbCore.DbQendraKosto.clsKokaQendraKosto.KrijoQK(new DbData(), clsKonf.IdNivel, clsKonf.IdKonfigAmbjente, 1, dtDok, nrDok, 0, statusdok, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheNdermarrjeVit(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), dtDok, pershkrimi, konfig.IdNivel, konfig.IdKonfigAmbjente, 0, trupipadublikime, 0, 0, 0, objektivat, vleratobjektiva, vleratobjektivamonbaze, idllogobj, out shfaqmesazhapolupe, qendravjeter.ColTrupi, 0, rishpernda, shperndaDifQKPModDok);
            return koka;
        }


        private string kontrolloTrupFleteKont(int idKoka, int idDokumentiImporti, int idNdermarrje, colTrupatFletetKontabel trupi, DateTime dtDok)
        {
            DataTable dt = DbCore.DbImporte.colImportTrupiFleteKontabel.merrTrupiFleteKontabelSipasIdDokumentiImportuarDt(idDokumentiImporti, idKoka);
            string error = "";

            int i = 1;
            foreach (DataRow dr in dt.Rows)
            {
                DbCore.DbKontabiliteti.clsTrupiFleteKontabel tr = new DbCore.DbKontabiliteti.clsTrupiFleteKontabel();
                string nrLlog = "", pershkrimtrupi = "", kodmon = "";
                double kursi = 0, vleftadebi = 0, vleftakredi = 0, vleftadebimonbaze = 0, vleftakredimonbaze = 0;
                try
                {
                    nrLlog = dr["NRLLOGARI"].ToString();
                    pershkrimtrupi = dr["PERSHKRIMITRUPFLETEKONTABEL"].ToString();
                    kodmon = dr["MONEDHA"].ToString();
                    kursi = double.Parse(dr["KURSI"].ToString());
                    vleftadebi = double.Parse(dr["VLEFTADEBIMONHUAJ"].ToString());
                    vleftakredi = double.Parse(dr["VLEFTAKREDIMONHUAJ"].ToString());
                    vleftadebimonbaze = double.Parse(dr["VLEFTADEBIMONBAZE"].ToString());
                    vleftakredimonbaze = double.Parse(dr["VLEFTAKREDIMONBAZE"].ToString());
                }
                catch (Exception ex)
                {
                    NLog.LogManager.GetCurrentClassLogger().Error(ex.Message);
                    error = ex.Message;
                }

                if (error != "")
                    continue;

                var mesazh = tr.KrijoTrupFleteKontabelImport(nrLlog, pershkrimtrupi, kodmon, kursi, vleftadebi, vleftakredi, vleftadebimonbaze, vleftakredimonbaze, idNdermarrje, dtDok);

                if (mesazh.Status)
                    trupi.Add(tr);
                else error = mesazh.PershkrimMesazhi;
                i++;
            }
            return error;
        }

        private string kontrolloTrupShitje(int idtrasferimi, DbCore.DbRegjistrim.colTrupiShitje trupi, int loan)
        {
            DataTable dt = DbCore.DbRegjistrim.colTrupiShitje.merrTrupiShitjeTransferWKDT(idtrasferimi);
            string error = "";

            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idPerdorues = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);

            int i = 1;
            foreach (DataRow dr in dt.Rows)
            {
                DbCore.DbRegjistrim.clsTrupiShitje tr = new DbCore.DbRegjistrim.clsTrupiShitje();
                int idtransferimi = 0, idtrupi = 0, idurdhershitje = 0;
                string seriali = "", artikulli = "";
                double sasia = 0, cmime = 0, vleftapatvsh = 0, vleftametvsh = 0, vleftazbritje = 0;
                try
                {
                    idtrupi = int.Parse(dr["ID_TRUPI"].ToString());
                    idtransferimi = int.Parse(dr["ID_TRANSFERIMI"].ToString());
                    idurdhershitje = int.Parse(dr["ID_URDHER_SHITJE_TRUPI"].ToString());
                    artikulli = dr["AW_ARTIKULLI"].ToString();
                    seriali = dr["AW_SERIALI"].ToString();

                    sasia = double.Parse(dr["AW_SASIA"].ToString());
                    cmime = double.Parse(dr["AW_CMIMI"].ToString());
                    vleftazbritje = double.Parse(dr["AW_ZBRITJE"].ToString());
                    vleftametvsh = double.Parse(dr["AW_VLERAMETVSH"].ToString());
                    vleftapatvsh = double.Parse(dr["AW_VLERAPATVSH"].ToString());
                }
                catch (Exception ex)
                {
                    NLog.LogManager.GetCurrentClassLogger().Error(ex.Message);
                    error = ex.Message;
                }

                if (error != "")
                    continue;

                int idtaksa = 0; double tvsh = 0;
                DbCore.DbRegjistrim.clsTrupiShitje trupiurdher = new DbCore.DbRegjistrim.clsTrupiShitje(idurdhershitje);
                if (trupiurdher.IdLlojVeprimi == 1)//per artikujt marrim tvsh e caktuar tek kartela dhe jo te winlinekartes
                {
                    idtaksa = DbCore.DbInventari.clsArtikulli.ktheIdTvshSipasKodArt(artikulli, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    DbCore.DbRegjistrim.clsTaksa taks = new DbCore.DbRegjistrim.clsTaksa(idtaksa);
                    vleftapatvsh = vleftametvsh / (1 + double.Parse(taks.NormaPerqindje.ToString()) / 100);
                    tvsh = vleftametvsh - vleftapatvsh;
                    cmime = (vleftapatvsh + vleftazbritje) / sasia;
                }
                else
                {
                    tvsh = vleftametvsh - vleftapatvsh;
                    double perqindjatvsh = Math.Round(tvsh / vleftapatvsh, 2);

                    DbCore.DbRegjistrim.colTaksa taksat = new DbCore.DbRegjistrim.colTaksa(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
                    foreach (DbCore.DbRegjistrim.clsTaksa taksa in taksat)
                        if (Convert.ToDouble(taksa.NormaPerqindje) == perqindjatvsh * 100)
                            idtaksa = taksa.IdTaksa;

                }
                double zbritjevlere = vleftazbritje * cmime * sasia / 100;
                DbCore.DbRegjistrim.clsTrupiShitje trupiurdherbij = new DbCore.DbRegjistrim.clsTrupiShitje();
                trupiurdherbij.merrSipasIdTrasferimi(trupiurdher.IdShitjeTrupi);
                DbCore.clsMesazh mesazh = tr.krijoTrupShitjeImportWK(idurdhershitje, trupiurdher.IdShitjeKoka, trupiurdher.IdLlojVeprimi, artikulli, trupiurdher.Pershkrimi, seriali, trupiurdher.IdNjesia, sasia, cmime, vleftazbritje, vleftametvsh, idtaksa, vleftapatvsh, trupiurdher.IdMagazina, trupiurdher.Gjeresi, trupiurdher.Gjatesi, trupiurdher.SasiPermasa, trupiurdher.Shenime, trupiurdher.DtFillimi, trupiurdher.DtMbarimi, trupiurdher.IdShitjeTrupi, trupiurdher.SasiRez, trupiurdher.IdTrupiRezervimi, trupiurdherbij.IdShitjeTrupi, trupiurdher.IdTrupiKthim, idNdermarrje, idPerdorues, loan, trupiurdher.IdLlogShpenzimi, 0, trupiurdher.Shenime2, 1, zbritjevlere, 0, 0);
                if (mesazh.Status)
                    trupi.Add(tr);
                else error = mesazh.PershkrimMesazhi;
                i++;
            }
            return error;
        }

        private void konfiguroGride()
        {
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (Request.QueryString["lloji"] == "importwk")
            {
                GridUtil.percaktoVisibleColumnsMeWidth(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), gvImport, "gvImport", "ImportWK.aspx?lloji=importwk");
                GridUtil.konfiguroGrideListeEvogelPaTheme(gvImport, "ID_TRANSFERIM");
            }
            else if (Request.QueryString["lloji"] == "importfk")
            {
                GridUtil.percaktoVisibleColumnsMeWidth(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), gvImport, "gvImport", "ImportWK.aspx?lloji=importfk");
                GridUtil.konfiguroGrideListeEvogelPaTheme(gvImport, "ID_KOKAIMPORT");
            }
            else if (Request.QueryString["lloji"] == "importtollonaleter")
            {
                GridUtil.percaktoVisibleColumnsMeWidth(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), gvImport, "gvImport", "ImportWK.aspx?lloji=importtollonaleter");
                GridUtil.konfiguroGrideListeEvogelPaTheme(gvImport, "id");
            }
            else if (Request.QueryString["lloji"] == "importtollonaelektronik")
            {
                GridUtil.percaktoVisibleColumnsMeWidth(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), gvImport, "gvImport", "ImportWK.aspx?lloji=importtollonaelektronik");
                GridUtil.konfiguroGrideListeEvogelPaTheme(gvImport, "id");
            }
            else if (Request.QueryString["lloji"] == "importtollonaelektronikspecifik")
            {
                GridUtil.percaktoVisibleColumnsMeWidth(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), gvImport, "gvImport", "ImportWK.aspx?lloji=importtollonaelektronikspecifik");
                GridUtil.konfiguroGrideListeEvogelPaTheme(gvImport, "id");
            }
            else
            {
                GridUtil.percaktoVisibleColumnsMeWidth(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), gvImport, "gvImport", "ImportWK.aspx?lloji=importtollona");
                GridUtil.konfiguroGrideListeEvogelPaTheme(gvImport, "id");
            }
        }

        private void mbushPopUpListeNgaDB()
        {//mbush griden e popupit me te dhena
            DataTable dt;
            if (Request.QueryString["lloji"] == "importfk")
            {
                DbCore.DbImporte.colImportKokaFleteKontabel import = new DbCore.DbImporte.colImportKokaFleteKontabel();
                bool pozitive = import.merrFleteKontabelAlbSigTePaImportuara(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                if (pozitive)
                {
                    if (import.Count > 0)
                        import.ruajImportiKokaFleteKontabelNeTabelaTemporale(null, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    dt = DbCore.DbImporte.colImportKokaFleteKontabel.merrKokaFleteKontabelTePaImportuaraDt();
                }
                else
                    dt = new DataTable();
            }
            else if (Request.QueryString["lloji"] == "importwk")
                dt = DbCore.DbRegjistrim.colKokaShitje.merrKokaShitjeTransferWKDT();
            else if (Request.QueryString["lloji"] == "importtollonaleter")
                dt = DbCore.DbTollona.colTollonaLeter.merrTollonaLeterKonsumuaraPerImport(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DateTime.Now);
            else if (Request.QueryString["lloji"] == "importtollonaelektronik")
                dt = DbCore.DbTollona.colTollonaElektronik.merrTollonaElektronikKonsumuaraPerImport(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DateTime.Now);
            else if (Request.QueryString["lloji"] == "importtollonaelektronikspecifik")
                dt = DbCore.DbTollona.colTollonaElektronik.merrTollonaElektronikKonsumuaraPerImportSpecifik(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DateTime.Now);
            else dt = DbCore.DbTollona.colShitjeMeSerial.merrShitjeMeSerialKonsumuaraPerImport(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DateTime.Now);
            DbCore.mySessionObjects.ruajRreshtaImportiNgaGrida(Session, dt);
            gvImport.DataSource = dt;
            gvImport.DataBind();
        }

        private void mbushGrideNgaSessioni()
        {
            gvImport.DataSource = DbCore.mySessionObjects.merrRreshtaImportiNgaGrida(Session);
            gvImport.DataBind();
        }

        protected void gvImport_AfterPerformCallback(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            mbushGrideNgaSessioni();
        }

        protected void gvImport_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters == "imp")
            {
                gvImport.DataSource = DbCore.mySessionObjects.merrRreshtaImportiNgaGrida(Session);
                gvImport.DataBind();
                return;
            }
            if (e.Parameters == "pastro")
            {
                mbushPopUpListeNgaDB();
                return;
            }
            if (e.Parameters == "ngarko")
                mbushPopUpListeNgaDB();
        }

        protected void gvImport_CustomJSProperties(object sender, DevExpress.Web.ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpNoRows"] = gvImport.VisibleRowCount;
            e.Properties["cpNoPage"] = gvImport.PageIndex;
        }

        private void shto_comanda()
        {
            if (gvImport.Columns["#"] == null)
            {
                GridViewCommandColumn check = new GridViewCommandColumn("#");
                check.ShowEditButton = true;
                check.ShowDeleteButton = true;
                ///  check.ShowNewButton = true;// nuk ka kuptim sepse nuk do kishim id dhe trupin e dokumentit
                check.Caption = "#";
                gvImport.Columns.Add(check);
            }
            gvImport.Columns["#"].VisibleIndex = 0;
        }

        protected void gvImport_DataBound(object sender, EventArgs e)
        {
            shto_comanda();
            if (Request.QueryString["lloji"] == "importwk")
                gvImport.KeyFieldName = "ID_TRANSFERIM";
            else if (Request.QueryString["lloji"] == "importfk")
                gvImport.KeyFieldName = "ID_KOKAIMPORT";
            else gvImport.KeyFieldName = "id";
        }

        protected void gvImport_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
        {

        }

        protected void gvImport_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            DataTable dt = DbCore.mySessionObjects.merrRreshtaImportiNgaGrida(Session);
            DataColumn[] keys = new DataColumn[1];
            if (Request.QueryString["lloji"] == "importfk")
                keys[0] = dt.Columns["ID_KOKAIMPORT"];
            else if (Request.QueryString["lloji"] == "importwk")
                keys[0] = dt.Columns["ID_TRANSFERIM"];
            else keys[0] = dt.Columns["id"];
            dt.PrimaryKey = keys;

            int id = 0;
            if (Request.QueryString["lloji"] == "importfk")
                id = Convert.ToInt32(e.Keys["ID_KOKAIMPORT"]);
            else if (Request.QueryString["lloji"] == "importwk")
                id = Convert.ToInt32(e.Keys["ID_TRANSFERIM"]);
            else id = Convert.ToInt32(e.Keys["id"]);
            DataRow dr = dt.Rows.Find(id);

            foreach (DataColumn dc in dt.Columns)
                if (((Request.QueryString["lloji"] == "importfk" && dc.ColumnName != "ID_KOKAIMPORT") || (Request.QueryString["lloji"] == "importwk" && dc.ColumnName != "ID_TRANSFERIM") || (Request.QueryString["lloji"] == "importtollona" && dc.ColumnName != "id") || (Request.QueryString["lloji"] == "importtollonaleter" && dc.ColumnName != "id") || (Request.QueryString["lloji"] == "importtollonaelektronik" && dc.ColumnName != "id") || (Request.QueryString["lloji"] == "importtollonaelektronikspecifik" && dc.ColumnName != "id")) && e.NewValues[dc.ColumnName] != null)
                    dr[dc.ColumnName] = e.NewValues[dc.ColumnName];
            if (Request.QueryString["lloji"] == "importfk")
                dr["ID_KOKAIMPORT"] = id;
            else if (Request.QueryString["lloji"] == "importwk")
                dr["ID_TRANSFERIM"] = id;
            else dr["id"] = id;
            e.Cancel = true;
            gvImport.CancelEdit();
            gvImport.DataSource = dt;
            gvImport.DataBind();
            DbCore.mySessionObjects.ruajRreshtaImportiNgaGrida(Session, dt);
        }

        protected void gvImport_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            DataTable dt = DbCore.mySessionObjects.merrRreshtaImportiNgaGrida(Session);
            DataColumn[] keys = new DataColumn[1];
            if (Request.QueryString["lloji"] == "importfk")
                keys[0] = dt.Columns["ID_KOKAIMPORT"];
            else if (Request.QueryString["lloji"] == "importwk")
                keys[0] = dt.Columns["ID_TRANSFERIM"];
            else keys[0] = dt.Columns["id"];
            dt.PrimaryKey = keys;
            int id = dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[dt.Rows.Count - 1]["ID_TRANSFERIM"]) + 1 : 0;
            DataRow dr = dt.NewRow();
            foreach (DataColumn dc in dt.Columns)
                if (((Request.QueryString["lloji"] == "importfk" && dc.ColumnName != "ID_KOKAIMPORT") || (Request.QueryString["lloji"] == "importwk" && dc.ColumnName != "ID_TRANSFERIM") || (Request.QueryString["lloji"] == "importtollona" && dc.ColumnName != "id") || (Request.QueryString["lloji"] == "importtollonaleter" && dc.ColumnName != "id") || (Request.QueryString["lloji"] == "importtollonaelektronik" && dc.ColumnName != "id") || (Request.QueryString["lloji"] == "importtollonaelektronikspecifik" && dc.ColumnName != "id")) && e.NewValues[dc.ColumnName] != null)
                    dr[dc.ColumnName] = e.NewValues[dc.ColumnName];
            if (Request.QueryString["lloji"] == "importfk")
                dr["ID_KOKAIMPORT"] = id;
            else
                if (Request.QueryString["lloji"] == "importwk")
                dr["ID_TRANSFERIM"] = id;
            else dr["id"] = id;
            dt.Rows.Add(dr);
            e.Cancel = true;
            gvImport.CancelEdit();
            gvImport.DataSource = dt;
            gvImport.DataBind();
            DbCore.mySessionObjects.ruajRreshtaImportiNgaGrida(Session, dt);
        }

        protected void gvImport_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            DataTable dt = DbCore.mySessionObjects.merrRreshtaImportiNgaGrida(Session);
            DataColumn[] keys = new DataColumn[1];
            if (Request.QueryString["lloji"] == "importfk")
                keys[0] = dt.Columns["ID_KOKAIMPORT"];
            else if (Request.QueryString["lloji"] == "importwk")
                keys[0] = dt.Columns["ID_TRANSFERIM"];
            else keys[0] = dt.Columns["id"];
            dt.PrimaryKey = keys;
            int id = 0;
            if (Request.QueryString["lloji"] == "importfk")
                id = Convert.ToInt32(e.Keys["ID_KOKAIMPORT"]);
            else if (Request.QueryString["lloji"] == "importwk")
                id = Convert.ToInt32(e.Keys["ID_TRANSFERIM"]);
            else id = Convert.ToInt32(e.Keys["id"]);

            DataRow dr = dt.Rows.Find(id);
            //dt.Rows.Remove(dr);
            if (Request.QueryString["lloji"] == "importfk")
            {
                DbCore.clsMesazh fshire = DbCore.DbImporte.clsImportKokaFleteKontabel.fshiKokaFleteKont(id);
                if (fshire.Status)
                {
                    dt.Rows.Remove(dr);
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, fshire.PershkrimMesazhi, pnlMesazhi);
                }
                else
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgGabimGjateFshirjes", ci), pnlMesazhi);
            }
            else
                dt.Rows.Remove(dr);
            e.Cancel = true;
            gvImport.CancelEdit();
            gvImport.DataSource = dt;
            gvImport.DataBind();
            DbCore.mySessionObjects.ruajRreshtaImportiNgaGrida(Session, dt);
        }

        protected void gvImport_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (Request.QueryString["lloji"] == "importfk" && e.Column.FieldName == "VLEFTAFLETEKONTABEL")
                e.Column.ReadOnly = true;
        }
    }
}