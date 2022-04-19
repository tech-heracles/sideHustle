using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using DbCore;
using System.Web.Script.Serialization;
using DbCore.DbProdhimi;
using System.Data;
using PlatinumWeb.Templates;
using DbCore.DbRegjistrim;
using DbCore.DbKontabiliteti;
using System.Globalization;
using System.Resources;
using DbCore.DbShare;
using DbCore.IMBUtils.Types;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.DbInventari;

namespace PlatinumWeb
{
    public partial class GjeneroProjektProdhimi : MyPageBase
    {
        bool visibleindex;
        //private static string STR_njeProdhimPorosi = "Duhet te kete te pakten nje prodhim per porosi!";

        /// <summary>
        /// perdoret per te vendosur theme
        /// </summary>
        /// <param name="source">derguesi</param>
        /// <param name="e">argumentat</param>
 
        private string komponente = "GjeneroProjektProdhimi.aspx";
        private string guidString;
        /// <summary>
        /// mbush te dhenat kur faqja ben loadim
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            int idGjuha;
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                DbCore.clsFunksione.logout(Session,true,"FaqePaautorizuar");
            }
            int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerdoruesi);
            }
            int idNdermarrje;
            int idNderViti = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
            int idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);


            if (!IsPostBack)
            {
                idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                hfState.Set("idGjuha", idGjuha);
                hfState.Set("idNdermarrje", idNdermarrje); 
                hfState.Set("idPerdoruesi", idPerdoruesi);
                hfState.Set("idViti", idViti);
                guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                hfState.Set("guidString", guidString);

                mbushHiddenFieldMePerkthime(cultinf, rm);
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaDokumenta = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaDokumenta.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, DbCore.clsFunksione.GetKomponente(Page.Request, false));
                hfTeDrejtaGjitheDok.Value = tedrejtaDokumenta.DGjitheDok.ToString();
                konfiguroVleraFillestareShto(idGjuha, idNdermarrje, rm, cultinf);
                percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), idPerdoruesi, idNdermarrje);
                if (hfTeDrejtaGjitheDok.Value.ToString() == "True" || hfTeDrejtaGjitheDok.Value.ToString() == "true")
                    inicializoGridFaturat(idNdermarrje, true);
                else
                    inicializoGridFaturat(idNdermarrje, false);
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                konfiguroGrideFaturat(false, idGjuha, idNdermarrje, idPerdoruesi);
             

                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                GridUtil.perktheButonaGride(hfState, cultinf);
            }
            else {
                idGjuha = (int)hfState["idGjuha"];
                idNdermarrje = (int)hfState["idNdermarrje"];
                percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), idPerdoruesi, idNdermarrje);
                mbushGridNgaSession(idNdermarrje);
                konfiguroGrideFaturat(false, idGjuha, idNdermarrje, idPerdoruesi);

            }
            
        }
        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        /// <param name="idViti"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNermarrje"></param>
        private void percaktoTemplateMenu(ASPxMenu aSPxMenu1, int idViti, int idPerdorues, int idNdermarrje)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            clsToolbarConfig.percaktoTemplateMenu((int)hfState["idGjuha"], idViti, idPerdorues, idNdermarrje, aSPxMenu1, komponente, this, MenuInfo, true, true, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
            aSPxMenu1.Items.FindByName("Ruaj").Text = rm.GetString("btnGjenero",cultinf);

        }

        /// <summary>
        /// Metode qe sherben per te kaluar perkthime te js
        /// </summary>
        /// <param name="cultinf">Merr culture info</param>
        /// <param name="rm">Merr Resource Manager</param>
        private void mbushHiddenFieldMePerkthime(CultureInfo cultinf, ResourceManager rm)
        {
            btnKerko.Text = rm.GetString("ReportToolbarButtonSearch", cultinf);
            hfState.Set("msgZgjidhDokumentin", rm.GetString("msgZgjidhDokumentin", cultinf));
            hfState.Set("msgDataNgaNukDuhetMeEMadheSeDataDeri", rm.GetString("msgDataNgaNukDuhetMeEMadheSeDataDeri", cultinf));
            hfState.Set("roundPanelZgjidhArtikullin", rm.GetString("roundPanelZgjidhArtikullin", cultinf));
            hfState.Set("msgZgjidhKlientin", rm.GetString("msgZgjidhKlientin", cultinf));
            hfState.Set("headerPopUpZgjidhKodifikiminArtikullit", rm.GetString("headerPopUpZgjidhKodifikiminArtikullit", cultinf));
            hfState.Set("msgPoTransferohetTeDhenatShtypniPerseriRuaj", rm.GetString("msgPoTransferohetTeDhenatShtypniPerseriRuaj", cultinf));
            hfState.Set("msgKyDokumentProdhimMePorosiEshteZgjedhurNjeHere", rm.GetString("msgKyDokumentProdhimMePorosiEshteZgjedhurNjeHere", cultinf));
            hfState.Set("msgSasiaAktualeDuhetTeJeteNr", rm.GetString("msgSasiaAktualeDuhetTeJeteNr", cultinf));
            hfState.Set("msgSasiaAktualeNukMundTeJeteZero", rm.GetString("msgSasiaAktualeNukMundTeJeteZero", cultinf));
            hfState.Set("msgSasiaPermaseDuhetTeJeteNumer", rm.GetString("msgSasiaPermaseDuhetTeJeteNumer", cultinf));
            hfState.Set("msgSasiaPermaseNukMundTeJeteZero", rm.GetString("msgSasiaPermaseNukMundTeJeteZero", cultinf));
            ASPxNavBar1.Groups[0].Text = rm.GetString("menuPorosite", cultinf);
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
        /// vendos daten default
        /// </summary>
        private void vendosDataDefault()
        {
            DateTime sot = new DateTime();
            sot = DateTime.Today;

            DbCore.DbAdmin.clsPeriudhaKontabel periudha = DbCore.mySessionObjects.merrPeriudheKontabel(Session);
            DbCore.DbAdmin.clsViti viti = new DbCore.DbAdmin.clsViti(periudha.IdViti);
            dteDtNga.Value = periudha.FillimiPeriudha;
            if (sot >= periudha.FillimiPeriudha && sot <= periudha.MbarimiPeriudha)
                dteDtDeri.Value = DateTime.Today;
            else
                dteDtDeri.Value = periudha.MbarimiPeriudha;
        }

        /// <summary>
        /// Vendos vlerat default kur po behet shtim
        /// </summary>
        /// <param name="idGjuha"></param>
        /// <param name="idNdermarrje"></param>
        private void konfiguroVleraFillestareShto(int idGjuha, int idNdermarrje, System.Resources.ResourceManager rm, System.Globalization.CultureInfo cultinf)
        {

            AspxWebControlUtils.vendosDateEditMask(dteDtNga);
            vendosDataDefault();
            AspxWebControlUtils.vendosDateEditMask(dteDtDeri);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btneArtikulli);
            ConfigureAspxComboBox.mbushComboArtikulli(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje, rm.GetString("postStringTvsh", cultinf), btneArtikulli);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btneGrup);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btneKlienti);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btneNenGrup);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btneNrUrdherShitje);
            int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 46, rm, cultinf, idGjuha);
            cmbKonfigurimi.SelectedIndex = 0;
            ConfigureAspxComboBox.shtoKolonaPerGrupim(cmbGrup1);
            ConfigureAspxComboBox.mbushComboGrupeDokumentashSipasKonfigurimit(cmbGrup1, idNdermarrje, 1, int.Parse(cmbKonfigurimi.Value.ToString()), idPerdoruesi);
            ConfigureAspxComboBox.shtoKolonaPerGrupim(cmbGrup2);
            ConfigureAspxComboBox.mbushComboGrupeDokumentashSipasKonfigurimit(cmbGrup2, idNdermarrje, 2, int.Parse(cmbKonfigurimi.Value.ToString()), idPerdoruesi);
            ConfigureAspxComboBox.shtoKolonaPerGrupim(cmbGrup3);
            ConfigureAspxComboBox.mbushComboGrupeDokumentashSipasKonfigurimit(cmbGrup3, idNdermarrje, 3, int.Parse(cmbKonfigurimi.Value.ToString()), idPerdoruesi);
            //   cmbKonfigurimi.TextFormatString = "{0}";
            hfKonffillestar.Value = cmbKonfigurimi.Text;
            colProjektProdhimi col = new colProjektProdhimi();
            //Session.Add("Produktet", col);
            DbCore.mySessionObjects.ruajProjektProdhimiNeSesion(Session, col);
            ConfigureAspxComboBox.mbushComboKodifikim(idNdermarrje, btneGrup, 1, false, false);
            ConfigureAspxComboBox.mbushComboKodifikim(idNdermarrje, btneNenGrup, 2, false, false);
            int idMonedheZgjedhur = DbCore.clsFunksione.ktheMonedhePerFormatNumri(idGjuha, int.Parse(cmbKonfigurimi.Value.ToString()), idNdermarrje, 807, "", -1, true);
            DbCore.DbShare.clsFormatiKonfig formatNrPerKonfig = new DbCore.DbShare.clsFormatiKonfig();
            formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(int.Parse(cmbKonfigurimi.Value.ToString()));
            DbCore.DbShare.clsFormatKonfigTrup formatMonedhe = clsFormatKonfigTrup.ktheFormatNumriMonedhe(formatNrPerKonfig, idMonedheZgjedhur);
            System.Web.Script.Serialization.JavaScriptSerializer serializusi = new System.Web.Script.Serialization.JavaScriptSerializer();
            hfState.Set("formatMonedhe", serializusi.Serialize(formatMonedhe));
        }

        /// <summary>
        /// Percakton veprimin qe kryhet kur klikohet nje nga butonat e menuse
        /// </summary>
        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {
            if (e.Item.Name == "Ruaj")
            {
                Page.Validate();
                gjeneroPlanifikime(true);
            }

        }

        /// <summary>
        /// Ruan/Modifikon nje objekt clsKokaEkzekutim.
        /// </summary>
        /// <param name="statusDokumenti">Statusi me te cilin po ruhet dokumenti (I rregullt apo draft)</param>
        private void gjeneroPlanifikime(bool kontrollokonvertim)
        {
            if (Page.IsValid == false)
                return;
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            List<string> TeKrijuar = new List<string>(), TePaKrijuar = new List<string>();
            colKokaPlanifikim koka = new colKokaPlanifikim();
            colUrdherPorosiPlanifikim colkonvetimi = new colUrdherPorosiPlanifikim();// merrIdPlanifikimi();
            clsMesazh mesazh;
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), komponente);
            if (!tedrejtaInfo.DShtim)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukKeniTeDrejta", cultinf), pnlMesazhi);
                status1.Value = "false";
                return;
            }
            try
            {

                object[] result = ruajTrupinEPlanifikimit();
                koka = (colKokaPlanifikim)result[0];
                colkonvetimi = (colUrdherPorosiPlanifikim)result[1];

                if (koka.Count == 0)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgZgjidhniNjeDokumentPerTeGjeneruar!", cultinf), pnlMesazhi, LoadingPanel);
                    status1.Value = "false";
                    return;
                }
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex.Message);
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, ex.Message, pnlMesazhi, LoadingPanel);
                status1.Value = "false";
                return;

            }
            int i = 0;
            foreach (clsKokaPlanifikim p in koka)
            {
                if (kontrollokonvertim)
                {
                    foreach (clsTrupiPlanifikim trsh in p.ColTrupi)
                    {
                        if (trsh.IdUrdherPorosi != 0)
                        {
                            clsTrupiShitje trupikon = new clsTrupiShitje(trsh.IdUrdherPorosi);
                            clsKokaShitje kokash = new clsKokaShitje();
                            kokash.mbushKokaShitjeSipasIDPaTrup(trupikon.IdShitjeKoka);
                            string ngjyra = clsKokaShitje.merrNgjyreGjenerimi(kokash.IdNdermarrje, trupikon.IdShitjeKoka);
                            if (ngjyra == "kuqe" || ngjyra == "gjelber")
                            {
                                lblMsgboxKonv.Text = "Dokumenti Nr." + kokash.NrDok + " Dt." + kokash.DtDok.ToShortDateString() + " eshte gjeneruar plotesisht, doni te vazhdoni?";
                                status1.Value = "konvertuar";
                                return;
                            }
                        }
                    }

                }
                colUrdherPorosiPlanifikim colurdh = new colUrdherPorosiPlanifikim();
                colurdh.Add(colkonvetimi[i]);
                mesazh = p.ruaj(hfNrAutoShitje, colurdh);

                if (!mesazh.Status)
                    TePaKrijuar.Add(p.NrDok);
                else TeKrijuar.Add(p.NrDok);
                i++;

            }
            if (TePaKrijuar.Count == 0)
            {
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("msgDokumentatEPlanifikimitMeNrDok", cultinf) + String.Join(";", TeKrijuar) + rm.GetString("msgUKrijuanMeSukses",cultinf), pnlMesazhi);
                status1.Value = "true";
                hfShtimModifikim.Value = "shtim";
            }
            else
            {
                if (TeKrijuar.Count > 0)
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgDokumentatEPlanifikimitMeNrDok", cultinf) + String.Join(";", TeKrijuar) + rm.GetString("msgUKrijuanMeSukses", cultinf) + rm.GetString("msgLidhesMesazhi", cultinf) + rm.GetString("msgDokumentatEPlanifikimitMeNrDok", cultinf) + String.Join(";", TePaKrijuar) + rm.GetString("msgNukUKrijuan",cultinf), pnlMesazhi, LoadingPanel);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgDokumentatEPlanifikimitMeNrDok", cultinf) + String.Join(";", TePaKrijuar) + rm.GetString("msgNukUKrijuan", cultinf), pnlMesazhi);
                status1.Value = "false";
            }
        }


        /// <summary>
        /// krijon koleksionin me trupin e dokumentit
        /// </summary>
        /// <param name="ruaj">ruaj boolean qe tregon ne duhen shtuar apo jo reshtat bosh ne ruajtje nuk duhen</param>
        /// <returns>coleksion me trupin e dokumentit</returns>
        private object[] ruajTrupinEPlanifikimit()
        {
            object[] result = new object[2];

            colProjektProdhimi col = DbCore.mySessionObjects.merrProjektProdhimiNgaSesioni(Session);
            colKokaPlanifikim colkoka = new colKokaPlanifikim();
            colUrdherPorosiPlanifikim coludher = new colUrdherPorosiPlanifikim();
            JavaScriptSerializer serializusi = new JavaScriptSerializer();
            object[] dokumenti = (object[])serializusi.DeserializeObject(gridDataObject.Value);
            //DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti(int.Parse(hfFD.Value.ToString()));

            int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            for (int i = 0; i < dokumenti.Length; i++)
            {
                string gjenero = ((Dictionary<string, object>)dokumenti[i])["cbGjenero"].ToString();
                if (gjenero.ToLower() == "true")
                {
                    clsProjektProdhimi projekt = new clsProjektProdhimi((Dictionary<string, object>)dokumenti[i]);
                    if (projekt.NrProjekti == "" || projekt.NrProjekti == null)
                        continue;
                    clsKokaShitje kokashit = new clsKokaShitje();
                    kokashit.mbushKokaShitjeSipasIDPaTrup(projekt.IdKoka);
                    clsUrdherPorosiPlanifikim urdher = new clsUrdherPorosiPlanifikim();
                    urdher.IdUrdherPorosia = kokashit.IdShitjeKoka;
                    urdher.IdKonfigUrdher = kokashit.IdKonfigAmbjente;
                    coludher.Add(urdher);
                    clsNjesiAdministrative mag = new clsNjesiAdministrative(projekt.IdMag);
                    colTrupiPlanifikim coltrupi = new colTrupiPlanifikim();
                    clsDetajimArtikulli detajim1 = new clsDetajimArtikulli(projekt.KodDetajim1, (int)hfState["idNdermarrje"]);
                    clsDetajimArtikulli detajim2 = new clsDetajimArtikulli(projekt.KodDetajim2, (int)hfState["idNdermarrje"]);

                    clsTrupiPlanifikim tr = new clsTrupiPlanifikim(0, 0, projekt.IdArtikulli, projekt.KodArtikulli, projekt.PershkrimArtikull, projekt.IdNjesi, projekt.SasiAktuale, projekt.IdMag, projekt.GjeresiPorositur, projekt.GjatesiPorositur, projekt.SasiPermase, projekt.IdTrupiShitje, projekt.Shenime, detajim1.IdDetajimArtikulli, detajim2.IdDetajimArtikulli);
                    coltrupi.Add(tr);
                    clsKokaPlanifikim koka = new clsKokaPlanifikim();
                    int idgrup = 0, idgrup2 = 0, idgrup3 = 0;
                    if (cmbGrup1.Text != "")
                        idgrup = int.Parse(cmbGrup1.Value.ToString());
                    if (cmbGrup2.Text != "")
                        idgrup2 = int.Parse(cmbGrup2.Value.ToString());
                    if (cmbGrup3.Text != "")
                        idgrup3 = int.Parse(cmbGrup3.Value.ToString());
                    int idRaportdesing = Convert.ToInt32(DbCore.DbShare.clsAtributeTrupi.merrVleredefaultSipasKontrollitDheKonfigurimit(Convert.ToInt32(hfFD.Value), "cmbFormatiPrintimit", 804));
                    clsMesazh mesazh = koka.krijoPlanifikim(DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdNivel(int.Parse(hfFD.Value.ToString())), int.Parse(hfFD.Value.ToString()), projekt.IdKlienti, projekt.KodKlienti, projekt.IdMag, mag.Kodi, projekt.Data, projekt.NrProjekti, 1, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheNdermarrjeVit(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DateTime.Today, kokashit.Pershkrimi, idgrup, idgrup2, idgrup3, kokashit.AfatKohor, coltrupi, idRaportdesing, 0, "");
                    if (!mesazh.Status)
                        throw new Exception(mesazh.PershkrimMesazhi);
                    colkoka.Add(koka);
                    col.Add(projekt);
                }
            }
            result[0] = colkoka;
            result[1] = coludher;
            return result;
        }

        /// <summary>
        /// perdoret per te marre listen e artikujve ne momentin qe filtrohet
        /// </summary>
        /// <param name="source">derguesi</param>
        /// <param name="e">parametra</param>
        protected void btneArtikulli_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("btneArtikulli"))
                {
                    CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
                    ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                    ConfigureAspxComboBox.mbushComboArtikulli(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), rm.GetString("postStringTvsh", cultinf), btneArtikulli);
                }
            }
        }

        /// <summary>
        /// perdoret per te marre listen e klienteve ne momentin qe filtrohet
        /// </summary>
        /// <param name="source">derguesi</param>
        /// <param name="e">parametrat</param>
        protected void btneKlienti_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("btneKlienti"))
                {
                    int value = 0;
                    if (e.Value == null || !Int32.TryParse(e.Value.ToString(), out value))
                        return;
                    ConfigureAspxComboBox.KonfiguroComboBoxKlientFurnitoriById(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), (ASPxComboBox)source, value);
                }
            }
        }
        /// <summary>
        /// perdoret per marre listen e klienteve sipas kushtit
        /// </summary>
        /// <param name="source">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void btneKlienti_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("btneKlienti"))
                {
                    ConfigureAspxComboBox.KonfiguroComboBoxKlientFurnitor(e.Filter, e.BeginIndex + 1, e.EndIndex + 1, DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), btneKlienti, 1);
                }
            }
        }
        protected void btneNrUrdherShitje_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("btneNrUrdherShitje"))
                {
                    if (e.Value == null)
                        return;
                    ConfigureAspxComboBox.mbushComboNrUrdherShitje(e.Value.ToString(), btneNrUrdherShitje);
                }
            }
        }
        protected void btneNrUrdherShitje_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("btneNrUrdherShitje"))
                {
                    ConfigureAspxComboBox.mbushComboNrUrdherShitje(e.Filter, e.BeginIndex + 1, e.EndIndex + 1, btneNrUrdherShitje, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), dteDtNga.Text, dteDtDeri.Text, DbCore.mySessionObjects.ktheIdPerdoruesi(Session), "", btneArtikulli.Text, btneGrup.Text, btneNenGrup.Text, btneKlienti.Text);
                }
            }
        }
        protected void ButtonOk5_Click(object sender, EventArgs e)
        {
            Page.Validate();
            gjeneroPlanifikime(false);
        }


        #region  GRIDA E FATURAVE
        /// <summary>
        /// inicializon griden e faturave
        /// </summary>
        private void inicializoGridFaturat(int idNdermarrje, bool gjitheDokumentat)
        {
            ASPxGridView grid_faturat = (ASPxGridView)ASPxNavBar1.Groups[0].FindControl("grid_faturat");
            DbCore.DbAdmin.clsNdermarrjeViti nder = new DbCore.DbAdmin.clsNdermarrjeViti(DbCore.mySessionObjects.ktheNdermarrjeVit(Session));
            
            DataTable dt = new DataTable();
            dt = colDokumentat.ktheGjitheDokumentatRegjistrimDokumentashSipasFiltrave(idNdermarrje, dteDtNga.Text, dteDtDeri.Text, DbCore.mySessionObjects.ktheIdPerdoruesi(Session), "", btneArtikulli.Text, btneGrup.Text, btneNenGrup.Text, btneKlienti.Text, btneNrUrdherShitje.Text, gjitheDokumentat);//perdoruesi mund te shohe te gjitha dokumentet
            grid_faturat.DataSource = dt;
            DbCore.mySessionObjects.ruajGrideNeSession(Session, dt);
            grid_faturat.DataBind();
            dt.Dispose();
        }
        private void mbushGridNgaSession(int idndermarje)
        {
            ASPxGridView grid_faturat = (ASPxGridView)ASPxNavBar1.Groups[0].FindControl("grid_faturat");

            DataTable tmpObject;
            bool sukses = DbCore.mySessionObjects.merrGrideNgaSessioni(Session, out tmpObject);
            if (!sukses)
            {
                if (hfTeDrejtaGjitheDok.Value.ToString() == "True" || hfTeDrejtaGjitheDok.Value.ToString() == "true")
                    inicializoGridFaturat(idndermarje, true);
                else
                    inicializoGridFaturat(idndermarje, false);
            }
            else
            {
                grid_faturat.DataSource = tmpObject;
                grid_faturat.DataBind();

                tmpObject.Dispose();
            }
        }
        /// <summary>
        /// konfiguron griden e faturave
        /// </summary>
        private void konfiguroGrideFaturat(bool visibleindex, int idGjuha, int idNdermarrje, int idPerdoruesi)
        {
             shtokolona();
            ASPxGridView grid_faturat = (ASPxGridView)ASPxNavBar1.Groups[0].FindControl("grid_faturat");
            if (visibleindex)
                GridUtil.percaktoVisibleColumnsMeWidth(idGjuha, idNdermarrje, grid_faturat, "grid_faturat", komponente);
            else GridUtil.percaktoVisibleColumnsMeWidthPaVisibleIndex(idGjuha, idNdermarrje, grid_faturat, "grid_faturat", komponente);

            KonfigurimComboGride.ShtoMonedhe(grid_faturat, idNdermarrje, idPerdoruesi, Session, komponente, guidString);
            KonfigurimComboGride.ShtoNivel(grid_faturat, 1, idNdermarrje, idPerdoruesi, idGjuha, Session, komponente, guidString, "IdNiveli");
            KonfigurimComboGride.ShtoModel(grid_faturat, 1, idNdermarrje, idPerdoruesi, idGjuha, Session, komponente, guidString);


           
            shtoColor();
            //shtoKlientFurnitor(grid_faturat,"IdKlientFurnitori");
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            GridUtil.KonfiguroGrideListeMadhePopupiPaTheme(grid_faturat, "IdDokumenti", true, false);
            grid_faturat.Columns["#"].VisibleIndex = 0;
            grid_faturat.SettingsPager.PageSize = 20;

        }
        
        /// <summary>
        /// shton kolonat te grida e faturave
        /// </summary>
        private void shtokolona()
        {
            ASPxGridView grid_faturat = (ASPxGridView)ASPxNavBar1.Groups[0].FindControl("grid_faturat");
            GridViewDataTextColumn colnew1;
            GridViewDataDateColumn colnew2;
            GridViewDataColumn colnew3;
            if (grid_faturat.Columns["IdDokumenti"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "IdDokumenti"; colnew1.VisibleIndex = 0;
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["IdKonfigAmbjente"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "IdKonfigAmbjente"; colnew1.VisibleIndex = 2;
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["IdNiveli"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "IdNiveli"; colnew1.VisibleIndex = 1;
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["NrDokumenti"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "NrDokumenti"; colnew1.VisibleIndex = 3;
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["DtDokumenti"] == null)
            {
                colnew2 = new GridViewDataDateColumn();
                colnew2.FieldName = "DtDokumenti"; colnew2.VisibleIndex = 4;
                grid_faturat.Columns.Add(colnew2);
            }
            if (grid_faturat.Columns["IdKlientFurnitori"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "IdKlientFurnitori";
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["KodKlientFurnitor"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "KodKlientFurnitor"; colnew1.VisibleIndex = 5;
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["AfatKohor"] == null)
            {
                colnew2 = new GridViewDataDateColumn();
                colnew2.FieldName = "AfatKohor"; colnew2.VisibleIndex = 6;
                grid_faturat.Columns.Add(colnew2);
            }
            if (grid_faturat.Columns["IdMonedha"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "IdMonedha"; colnew1.VisibleIndex = 8;
                grid_faturat.Columns.Add(colnew1);
            }

            if (grid_faturat.Columns["Vlefta"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "Vlefta"; colnew1.VisibleIndex = 9;
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["Pershkrimi"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "Pershkrimi"; colnew1.VisibleIndex = 7;
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["Ngjyra"] == null)
            {
                colnew3 = new GridViewDataColumn();
                colnew3.FieldName = "Ngjyra"; colnew3.VisibleIndex = 10;
                grid_faturat.Columns.Add(colnew3);
            }
            if (grid_faturat.Columns["Status"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "Status";
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["IdKrijuesi"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "IdKrijuesi";
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["Krijuesi"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "Krijuesi";
                grid_faturat.Columns.Add(colnew1);
            }
        }
        private void shtoColor()
        {
            ASPxGridView grid_faturat = (ASPxGridView)ASPxNavBar1.Groups[0].FindControl("grid_faturat");
            GridViewDataColumn g = grid_faturat.Columns["Ngjyra"] as GridViewDataColumn;
            g.Caption = "  ";
            g.DataItemTemplate = new MyGaugeTemplate();

        }
        /// <summary>
        /// Mbush combon e klienteve/furnitoreve ne gride
        /// </summary>
        private void shtoKlientFurnitor(ASPxGridView grid, string emerkolone)
        {
            int visibleindex = grid.Columns["IdKlientFurnitori"].VisibleIndex;
            //DbCore.DbKontabiliteti.clsDatabaseKontabilitet dbKontab = new DbCore.DbKontabiliteti.clsDatabaseKontabilitet();
            grid.Columns.Remove(grid.Columns[emerkolone]);
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            DbCore.DbKontabiliteti.colKlienteFurnitore colKlientet = new colKlienteFurnitore();
            colKlientet.Add(new DbCore.DbKontabiliteti.clsKlientFurnitor());
            colKlientet.mbushKlienteFurnitoreNdermarrjes(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            //DbCore.DbKontabiliteti.colKlienteFurnitore colKlientet = dbKontab.merrKlienteFurnitoreNdermarrjes(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            colnew.PropertiesComboBox.DataSource = colKlientet;
            colnew.PropertiesComboBox.TextField = "KodKlientFurnitor";
            colnew.PropertiesComboBox.ValueField = "IdKlientFurnitor";
            colnew.FieldName = emerkolone; colnew.VisibleIndex = visibleindex;
            grid.Columns.Add(colnew);
        }

        /// <summary>
        /// I vendos nje emer identifikues kontrolleve te grides se dok kryesore dhe percakton funksinet qe manipulojne ato ne client side
        /// </summary>
        protected void grid_faturat_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {

        }
        /// <summary>
        /// kur grida ben bound per te shtuar kolonen e selektimit
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void grid_faturat_DataBound(object sender, EventArgs e)
        {
            ASPxGridView grid_faturat = (ASPxGridView)this.ASPxNavBar1.Groups[0].FindControl("grid_faturat");
            if (grid_faturat.Columns["#"] == null)
            {
                DevExpress.Web.GridViewCommandColumn check = new DevExpress.Web.GridViewCommandColumn("#") { ShowSelectCheckbox = true, Width = Unit.Percentage(2) };

                grid_faturat.Settings.ShowFilterRow = true;
                grid_faturat.Settings.ShowHeaderFilterButton = true;
                grid_faturat.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                grid_faturat.Settings.ShowFilterRowMenu = true;
                grid_faturat.Columns.Add(check);
                grid_faturat.Settings.ShowGroupPanel = false;
                grid_faturat.KeyFieldName = "IdDokumenti";
                grid_faturat.SettingsBehavior.AllowSelectByRowClick = true;
                grid_faturat.SettingsBehavior.AllowFocusedRow = true;
                grid_faturat.Settings.ShowTitlePanel = false;
                grid_faturat.SettingsText.Title = "Zgjidhni faturat";
            }
        }
        /// <summary>
        /// kur grida ben callback 
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void grid_faturat_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (hfTeDrejtaGjitheDok.Value.ToString() == "True" || hfTeDrejtaGjitheDok.Value.ToString() == "true")
                inicializoGridFaturat(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), true);
            else inicializoGridFaturat(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), false);
            ASPxGridView grid_faturat = (ASPxGridView)ASPxNavBar1.Groups[0].FindControl("grid_faturat");
            grid_faturat.Selection.UnselectAll();
        }
        /// <summary>
        /// vendos karakteristika te grides ne javascript
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void grid_faturat_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            ASPxGridView grid_faturat = (ASPxGridView)this.ASPxNavBar1.Groups[0].FindControl("grid_faturat");
            e.Properties["cpNoRows"] = grid_faturat.VisibleRowCount;
        }
        /// <summary>
        /// kur grida ben callback
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void grid_faturat_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {

        }
        /// <summary>
        /// perdoret per te hequr filtrimin kur zgjedh elementin bosh
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void grid_faturat_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if ( e.Column.FieldName == "IdKonfigAmbjente" ||
                  e.Column.FieldName == "IdMonedha" || e.Column.FieldName == "IdKlientFurnitori")
            {
                if (Converter.ConvertToInt(e.Value)==0)
                {
                    e.Criteria = null;
                }
            }
        }

        #endregion
    }
}