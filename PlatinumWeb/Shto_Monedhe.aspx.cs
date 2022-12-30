using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Data;
using PlatinumWeb.Templates;
using DbCore;
using System.Web.Script.Serialization;
using System.Globalization;
using System.Resources;
using DbCore.IMBUtils.Types;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.DbAdmin;

namespace PlatinumWeb
{
    public partial class Shto_Monedhe : MyPageBase
    {
        private DbCore.DbAdmin.clsMonedha monedha;

        private DbCore.DbAdmin.colKurset colKurset;


        private int idgjuha, idviti, idPerdoruesi, idNdermarrje;
        private string komponente = "Shto_Monedhe.aspx";
        private string guidString;




        protected void Page_Init(object sender, EventArgs e)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                Response.Redirect(DbCore.IMBUtils.Paths.defaultLoginPath);
                return;
            }
            idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerdoruesi);
                return;
            }
            idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            idviti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            idgjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            percaktoTemplateMenu(idgjuha, idviti, idPerdoruesi, idNdermarrje, ASPxMenu1);
            var cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            var rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (!Page.IsPostBack)
            {
                guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                hfState.Set("guidString", guidString);
                EmrateTabeve(rm, cultinf);
                vendosHfMePerkthime(rm, cultinf);
                hfState.Set("idGjuha", idgjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idPerdoruesi", IdPerdoruesi);
                hfState.Set("MonedheNderm", DbCore.DbAdmin.clsMonedha.ktheMonedhenENdermarrjes(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session)));
                ASPxPageControl1.ActiveTabIndex = 0;
                ConfigureAspxComboBox.mbushComboLlogaria(idNdermarrje, idPerdoruesi, btneLlogFitimi);
                ConfigureAspxComboBox.mbushComboLlogaria(idNdermarrje, idPerdoruesi, btneLlogHumbje);
                ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btneLlogFitimi);
                ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btneLlogHumbje);
                hfState.Set("colAutorizime", Newtonsoft.Json.JsonConvert.SerializeObject(new colAutorizimetKoka(IdPerdoruesi)));
                ConfigureAspxComboBox.mbushComboFormateNumrash(btneFormatNumri);
                ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btneFormatNumri);
                ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 27, rm, cultinf, idgjuha);
                cmbKonfigurimi.SelectedIndex = 0;
                var konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()), idgjuha);
                hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
                mbushGridMonedhashNgaDB();
                hfId.Value = "0";
                mbushGridenHistoriku();
                konfiguroGrideHistoriku(idNdermarrje, idgjuha);
                GridUtil.percaktoVisibleColumnsMeWidth(idgjuha, idNdermarrje, gvHistoriku, "gvKurset", komponente);
                konfiguroGrideKurset(idgjuha, idNdermarrje);
                konfiguroGride(idPerdoruesi, idNdermarrje);
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "ASPxGridView_Monedhat", ASPxGridView_Monedhat, cmbKonfigurimi.Text.Split(';')[0], "133", (int)hfState["idGjuha"]);
                ASPxGridView_Monedhat.Columns["#"].VisibleIndex = 0;
                var tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idviti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idviti, komponente);
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
            }
            else
            {
                guidString = (string)hfState["guidString"];
                mbushGridMonedhashNgaSession();
                mbushGridenHistorikuNgaSesioni();
                konfiguroGride(idPerdoruesi, idNdermarrje);
                konfiguroGrideHistoriku(idNdermarrje, idgjuha);
            }
            clsToolbarConfig.mbushComboBoxFiltra(idgjuha, idNdermarrje, "ASPxGridView_Monedhat", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
            GridUtil.konfigGrideListeEMadhePaTheme(ASPxGridView_Monedhat, "IdMonedha");
            percaktoTemplate();
            percaktoTamplate1();
            konfigurimi_Label.Text = rm.GetString("lblModeli", cultinf);
            popupUniversal.HeaderText = rm.GetString("headerPopUpText", cultinf);
            AspxWebControlUtils.perkthePopUp(popFshi, rm.GetString("labelKujdes", cultinf), lblMsgbox, rm.GetString("labelAdministrimiMsgJeniSigurt", cultinf), ButtonCancel, rm.GetString("labelAnullo", cultinf));
            GridUtil.EmrateButonaveMbiGride(ASPxGridView_Monedhat);
        }

        /// <summary>
        /// Vendos emrat e tabeve ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateTabeve(ResourceManager rm, CultureInfo cultinf)
        {
            ASPxPageControl1.TabPages[0].Text = rm.GetString("labelAdministrimiTePergjithshme", cultinf);
            ASPxPageControl1.TabPages[1].Text = rm.GetString("labelFilterAvancuarMonedha", cultinf);
            ASPxPageControl1.TabPages[2].Text = rm.GetString("kursetTab", cultinf);
            ASPxPageControl1.TabPages[3].Text = rm.GetString("historikuTab", cultinf);
        }

        /// <summary>
        /// vendos mesazhet sipas gjuhes per mesazhet ne js
        /// </summary>
        /// <param name="rm"></param>
        /// <param name="cultinf"></param>
        private void vendosHfMePerkthime(ResourceManager rm, CultureInfo cultinf)
        {
            hfState.Set("headerPopUpZgjidhAutorizimet", rm.GetString("headerPopUpZgjidhAutorizimet", cultinf));
            hfState.Set("msgKursiNukMundTeJeteZero", rm.GetString("msgKursiNukMundTeJeteZero", cultinf));
            hfState.Set("msgKursiNumerPozitiv", rm.GetString("msgKursiNumerPozitiv", cultinf));
            hfState.Set("msgKursiNje", rm.GetString("msgKursiNje", cultinf));
            hfState.Set("msgKursiDuhetNumer", rm.GetString("msgKursiDuhetNumer", cultinf));
            hfState.Set("msgMonedhaShenoniMonedhenPastajKurset", rm.GetString("msgMonedhaShenoniMonedhenPastajKurset", cultinf));
            hfState.Set("headerPopUpText", rm.GetString("headerPopUpText", cultinf));
            hfState.Set("msgMonedhaDuhetTeZgjidhni1Monedhe", rm.GetString("msgMonedhaDuhetTeZgjidhni1Monedhe", cultinf));
        }


        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="idNderVit"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        private void percaktoTemplateMenu(int idGjuha, int idViti, int idPerdorues, int idNdermarrje, ASPxMenu aSPxMenu1)
        {
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, komponente, this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, hfShtimModifikim.Value == "modifikim" ? false : true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
        }

        protected void RuajKolona_Click(object sender, EventArgs e)
        {
            var idfiltri = 0;
            var idkonf = DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(cmbKonfigurimi.Text.Split(';')[0], idNdermarrje);
            var mesazh = GridUtil.ruajFiltra(idNdermarrje, idPerdoruesi, idgjuha, "ASPxGridView_Monedhat ", komponente, "FilterDefault", ASPxGridView_Monedhat.FilterExpression, ASPxGridView_Monedhat, "KodiMonedha", idkonf, out idfiltri);
            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                return;
            }
            mesazh = GridUtil.ruajkonfigurimgride(ASPxGridView_Monedhat, cmbKonfigurimi.Text, idNdermarrje, idPerdoruesi, 133, idfiltri, idviti, DbCore.mySessionObjects.ktheCultureInfo(Session), DbCore.mySessionObjects.ktheGjuhe(Session));/// ruan konfigurimin e grides dhe filtrin
            clsToolbarConfig.mbushComboBoxFiltra(idgjuha, idNdermarrje, "ASPxGridView_Monedhat ", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);

            percaktoTemplateMenu(idgjuha, idviti, idPerdoruesi, idNdermarrje, ASPxMenu1);
            if (mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            }
            else
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            }
        }

        protected void btnXlsxExport_Click(object sender, EventArgs e)
        {
            try
            {
                gridExport.WriteXlsxToResponse("Historiku", true);
            }
            catch (Exception)
            {
            }
        }
        protected void btnPdfExport_Click(object sender, EventArgs e)
        {
            try
            {
                gridExport.WritePdfToResponse("Historiku", true);
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(idgjuha, idviti, idPerdoruesi, idNdermarrje, ASPxMenu1);
        }

        private void mbushGridMonedhashNgaSession()
        {
            DataTable tmpObject;
            var sukses = DbCore.mySessionObjects.merrGrideNgaSessioni(Session, out tmpObject);
            if (!sukses)
            {
                mbushGridMonedhashNgaDB();
            }
            else
            {
                ASPxGridView_Monedhat.DataSource = tmpObject;
                ASPxGridView_Monedhat.DataBind();
                InitializeGridKurset();
                tmpObject.Dispose();
            }
        }

        private void mbushGridenHistorikuNgaSesioni()
        {

            var tmpObject = mySessionObjects.MerrNgaSession<colKurset>(Session, gvHistoriku.ID);
            if (tmpObject == null || !(tmpObject.Count > 0))
            {
                mbushGridenHistoriku();
            }
            else
            {
                gvHistoriku.DataSource = tmpObject;
                gvHistoriku.DataBind();
                InitializeGridKurset();
            }
        }
        private void mbushGridMonedhashNgaDB()
        {
            var dt = DbCore.DbAdmin.colMonedhat.merrMonedhaNdermarjeDT(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            DbCore.mySessionObjects.ruajGrideNeSession(ASPxGridView_Monedhat.ID, Session, dt);
            ASPxGridView_Monedhat.DataSource = dt;
            ASPxGridView_Monedhat.DataBind();
            InitializeGridKurset();
            dt.Dispose();
        }

        private void mbushGridenHistoriku()
        {
            var id = int.Parse(hfId.Value.ToString());
            var colKurset = new DbCore.DbAdmin.colKurset();
            colKurset.mbushKursetMonedhes(id);
            mySessionObjects.RuajNeSession<colKurset>(Session, colKurset, gvHistoriku.ID);
            gvHistoriku.DataSource = colKurset;
            gvHistoriku.DataBind();
        }

        private void konfiguroVleraFillestare()
        {
            var colMonedhat = new DbCore.DbAdmin.colMonedhat();
            colMonedhat.mbushGjitheMonedhatPozitive(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));

            ASPxGridView_Monedhat.DataSource = colMonedhat;
            ASPxGridView_Monedhat.DataBind();
            InitializeGridKurset();
        }

        /// <summary>
        /// fshin filtrin e zgjedhur dhe aplikuar mbi gride, fshirje nga DB ku ai eshte ruajtur, dhe jo pastrimi i grides nga aplikimi i filtrit
        /// per kete do sherbeje Clear Filter ne fund te grides
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
            var itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            var cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            var idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            var idPerdorues = mySessionObjects.ktheIdPerdoruesi(Session);
            var idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            var filtra = new DbCore.DbAdmin.clsFiltraGrida();
            var koka = new DbCore.DbAdmin.clsGridaKoka(idGjuha, "ASPxGridView_Monedhat", komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = idPerdorues;
                var mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, "ASPxGridView_Monedhat", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
                percaktoTemplateMenu(idgjuha, idviti, idPerdoruesi, idNdermarrje, ASPxMenu1);

                if (mesazh.Status == true)
                {
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                }
                else
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                }
                cmbFiltra.Text = string.Empty;
                hfStatusi.Value = "true";
                ASPxGridView_Monedhat.FilterExpression = String.Empty;
            }
        }

        /// <summary>
        /// sherben per te ruajtur filtrin e zgjedhur
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
            var itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            var cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            var idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            var idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);
            var idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            var filtri = new DbCore.DbAdmin.clsFiltraGrida();
            filtri.FiltraKodi = cmbFiltra.Text;
            filtri.FiltraShenime = cmbFiltra.Text;
            filtri.FiltraUniversal = false;
            var koka = new DbCore.DbAdmin.clsGridaKoka(idGjuha, "ASPxGridView_Monedhat", komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = ASPxGridView_Monedhat.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("KodiMonedha", ASPxGridView_Monedhat);
            //var kolona = ASPxGridView_Monedhat.GetSortedColumns();
            //if (kolona.Count > 0)
            //{
            //    filtri.KoloneRenditje = kolona[0].FieldName;
            //    if (kolona[0].SortOrder == DevExpress.Data.ColumnSortOrder.Ascending)
            //    {
            //        filtri.DrejtimRenditje = true;
            //    }
            //    else
            //    {
            //        filtri.DrejtimRenditje = false;
            //    }
            //}
            //else
            //{
            //    filtri.KoloneRenditje = "KodiMonedha";
            //    filtri.DrejtimRenditje = true;
            //}
            filtri.IdPerdoruesi = idPerdoruesi;
            filtri.IdNdermarje = idNdermarrje;
            var mesazh = new DbCore.clsMesazh();

            filtri.IdStatusDok = 1;
            mesazh = filtri.ruaj();
            clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, "ASPxGridView_Monedhat", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
            percaktoTemplateMenu(idgjuha, idviti, idPerdoruesi, idNdermarrje, ASPxMenu1);

            if (mesazh.Status == true)
            {
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            }
            else
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            }
            hfStatusi.Value = "true";
            cmbFiltra.Text = string.Empty;
        }

        private void konfiguroGride(int idPerdoruesi, int idNdermarrje)
        {
            var ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            var rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            KonfigurimComboGride.ShtoLlogari(ASPxGridView_Monedhat, idPerdoruesi, Session, komponente, guidString, "IdLlogFitimi"); 
            KonfigurimComboGride.ShtoLlogari(ASPxGridView_Monedhat, idPerdoruesi, Session, komponente, guidString, "IdLlogHumbje");
            KonfigurimComboGride.ShtoAutorizim(ASPxGridView_Monedhat, Session, komponente, guidString, "IdNivelAutorizimi");
            KonfigurimComboGride.shtoFormatNr(ASPxGridView_Monedhat, rm, ci, "IdFormatNrKursi");
        }

  
        private void konfiguroGrideKurset(int idGjuhe, int idNdermarrje)
        {
            var cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            var rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            gvKurset.SettingsBehavior.AllowSort = false;
            GridUtil.percaktoVisibleColumnsMeWidth(idGjuhe, idNdermarrje, gvKurset, "gvKurset", komponente);
            GridUtil.konfiguroGrideRegjistrimEvogelPaTheme(gvKurset, "IdKursi", false);
        }

        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            List<object> rreshtat;
            if (ASPxPageControl1.ActiveTabIndex == 0)
            {
                rreshtat = ASPxGridView_Monedhat.GetSelectedFieldValues("IdMonedha");
            }
            else
            {
                rreshtat = new List<object>();
                rreshtat.Add(hfId.Value);
            }

            var ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            var rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (rreshtat.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgMonedhaZgjidhniTePakten1Monedhe", ci), pnlMesazhi);
                return;
            }
            var TeFshire = new List<string>();
            var TePaFshire = new List<string>();
            var dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();

            foreach (object id in rreshtat)
            {
                var clsMonedhat = new DbCore.DbAdmin.clsMonedha(Convert.ToInt32(id));

                if (clsMonedhat.IdMonedha == 0)
                {
                    continue;
                }
                if (dbAdmin.kaVeprimeMonedha(clsMonedhat.IdMonedha).Status)
                {
                    TePaFshire.Add(clsMonedhat.KodiMonedha);
                    continue;
                }
                clsMonedhat.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                var mesazh = clsMonedhat.fshi();

                if (mesazh.Status)
                {
                    hiqMonedhaNgaGrida(clsMonedhat.IdMonedha, rm, ci);

                    TeFshire.Add(clsMonedhat.KodiMonedha);

                    ASPxPageControl1.ActiveTabIndex = 0;
                    hfStatusi.Value = "true";
                }
                else
                {
                    TePaFshire.Add(clsMonedhat.KodiMonedha);
                }
            }
            dbAdmin.Dispose();
            var mesazhInfoGabim = string.Empty;
            var mesazhInfoSukses = string.Empty;
            if (TePaFshire.Count == 1)
            {
                mesazhInfoGabim = String.Format("{0}{1}{2}", rm.GetString("msgMonedhaPrefixNjejes", ci), String.Join(";", TePaFshire), rm.GetString("msgShtoLlogariSuffixNjejesGabimi", ci));
            }
            else
            {
                if (TePaFshire.Count > 1)
                {
                    mesazhInfoGabim = String.Format("{0}{1}{2}", rm.GetString("msgMonedhaPrefixShumes", ci), String.Join(";", TePaFshire), rm.GetString("msgShtoLlogariSuffixShumesGabimi", ci));
                }
            }
            if (TeFshire.Count == 1)
            {
                mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("msgMonedhaPrefixNjejes", ci), String.Join(";", TeFshire), rm.GetString("regjMagSuffixMesazhNjejesSuksesi", ci));
            }
            else
            {
                if (TeFshire.Count > 1)
                {
                    mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("msgMonedhaPrefixShumes", ci), String.Join(";", TeFshire), rm.GetString("msgCeljeMagazinatSuffixShumesSuksesi", ci));
                }
            }
            if (mesazhInfoGabim != string.Empty && mesazhInfoSukses != string.Empty)
            {
                mesazhInfoGabim += rm.GetString("msgLidhesMesazhi", ci) + mesazhInfoSukses;
            }
            if (mesazhInfoGabim != string.Empty)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhInfoGabim, pnlMesazhi);
            }
            else
            {
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazhInfoSukses, pnlMesazhi);
            }
        }

        private void hiqMonedhaNgaGrida(int idmonedha, ResourceManager rm, CultureInfo ci)
        {
            if (this.ASPxGridView_Monedhat.DataSource != null)
            {
                var dt = (DataTable)ASPxGridView_Monedhat.DataSource;
                var drs = dt.Select("IdMonedha = " + idmonedha);
                if (drs.Length > 1)
                {
                    throw new MyException(rm.GetString("msgMonedhaNdodhen2MonedhaMeTeNjejtenIDNeGride", ci));
                }
                if (drs.Length == 0)
                {
                    return;
                }
                var dr = drs[0];
                dt.Rows.Remove(dr);
                ASPxGridView_Monedhat.DataBind();
            }
            else
            {
                mbushGridMonedhashNgaDB();
            }
        }

        private void shtoMonedhaNeGrid(int idNdermarrje, int idmonedha, ResourceManager rm, CultureInfo ci)
        {
            if (ASPxGridView_Monedhat.DataSource != null)
            {
                var dt = (DataTable)ASPxGridView_Monedhat.DataSource;
                var drs = dt.Select("IdMonedha = " + idmonedha);
                if (drs.Length > 0)
                {
                    throw new Exception(rm.GetString("msgMonedhaMonedhaEkzistonNeGride", ci));
                }
                var newArtDr = DbCore.DbAdmin.colMonedhat.merrMonedhaSipasNdermarjesDR(idNdermarrje, idmonedha);
                dt.ImportRow(newArtDr);
            }
            else
            {
                mbushGridMonedhashNgaDB();
            }
        }

        private void modifikoMonedhaNeGrid(int idNdermarrje, int idmonedha, ResourceManager rm, CultureInfo ci)
        {
            if (ASPxGridView_Monedhat.DataSource != null)
            {
                var dt = (DataTable)ASPxGridView_Monedhat.DataSource;
                var drs = dt.Select("IdMonedha = " + idmonedha);
                if (drs.Length > 1)
                {
                    throw new MyException(rm.GetString("msgMonedhaNdodhen2MonedhaMeTeNjejtenIDNeGride", ci));
                }
                if (drs.Length == 0)
                {
                    return;
                }
                var dr = drs[0];
                var newArtDr = DbCore.DbAdmin.colMonedhat.merrMonedhaSipasNdermarjesDR(idNdermarrje, idmonedha);


                var arr = newArtDr.ItemArray;
                dr.ItemArray = arr;
            }
            else
            {
                mbushGridMonedhashNgaDB();
            }
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
                ruajMonedhe();
            }
        }

        protected void ASPxGridView_Monedhat_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName == "AktivMonedha")
            {
                var ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
                var rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                (e.Editor as ASPxComboBox).Items.Clear();
                (e.Editor as ASPxComboBox).Items.Add(string.Empty);
                (e.Editor as ASPxComboBox).Items.Add(rm.GetString("cmbPerdoruesitAktiv", ci), true);
                (e.Editor as ASPxComboBox).Items.Add(rm.GetString("cmbPerdoruesitJoAktiv", ci), false);
            }
        }

        private void InitializeGridKurset()
        {
            colKurset = new DbCore.DbAdmin.colKurset();
            var oTrupi = new DbCore.DbAdmin.clsKurset();
            var llojetKurseve = new string[] { "Kursi1", "Kursi2", "Kursi3", "Kursi4", "Kursi5", "Kursi6", "Kursi7", "Kursi8", "Kursi9", "Kursi10", "Kursi11", "Kursi12", "Kursi13", "Kursi14", "Kursi15", "Kursi16", "Kursi17", "Kursi18", "Kursi19", "Kursi20" };










            oTrupi.PershkrimLlojKursi = "Kursi1";
            oTrupi.LlojKursi = 1;
            oTrupi.DataKursit = DateTime.Today;
            oTrupi.VleraKursi = 1;
            colKurset.Add(oTrupi);
            gvKurset.DataSource = colKurset;
            gvKurset.DataBind();
        }

        private void percaktoTemplate()
        {
            var col1 = gvKurset.Columns["LlojKursi"] as GridViewDataTextColumn;

            col1.DataItemTemplate = new MyComboTemplate();
            var col2 = gvKurset.Columns["VleraKursi"] as GridViewDataTextColumn;
            if (btneFormatNumri.Text != string.Empty)
            {
                col2.DataItemTemplate = new MyDoubleTemplate(false, int.Parse(btneFormatNumri.Value.ToString()), "0");
            }
            else
            {
                col2.DataItemTemplate = new MyDoubleTemplate(true, 2, "0");
            }
            var col3 = gvKurset.Columns["DataKursit"] as GridViewDataDateColumn;
            col3.DataItemTemplate = new MyCalendarTemplate();
            var col4 = gvKurset.Columns["NjesiaKursit"] as GridViewDataTextColumn;
            col4.DataItemTemplate = new MyIntTemplate(false);
        }

        protected void gvKurset_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            bool ugjet;
            if (e.RowType == DevExpress.Web.GridViewRowType.Data)
            {
                var col1 = ((ASPxGridView)sender).Columns["LlojKursi"] as GridViewDataColumn;
                var col2 = ((ASPxGridView)sender).Columns["VleraKursi"] as GridViewDataColumn;
                var col3 = ((ASPxGridView)sender).Columns["DataKursit"] as GridViewDataColumn;
                var col4 = ((ASPxGridView)sender).Columns["NjesiaKursit"] as GridViewDataColumn;

                var cmb1 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col1, "cmbBox") as ASPxComboBox;
                var txt2 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col2, "txtBox") as ASPxTextBox;
                var cal = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col3, "cal") as ASPxDateEdit;
                var txt3 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col4, "txtBox") as ASPxTextBox;

                ugjet = false;

                if (cmb1 != null)
                {
                    ConfigureAspxComboBox.mbushComboLlojeKursi(cmb1);
                    cmb1.ClientInstanceName = "LlojKursi" + e.VisibleIndex.ToString();
                    cmb1.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                    cmb1.DropDownStyle = DropDownStyle.DropDownList;
                    cmb1.ClientSideEvents.TextChanged = "function(s,e){TextChangedLlojKursit(LlojKursi" + e.VisibleIndex.ToString() + ",'LlojKursi', " + e.VisibleIndex.ToString() + ");}";
                }

                if (txt2 != null)
                {
                    if (hfShtimModifikim.Value == "modifikim")
                    {
                        var id = 0;
                        int.TryParse(hfId.Value.ToString(), out id);
                        var kodmonedhanderm = DbCore.DbAdmin.clsMonedha.ktheMonedhenENdermarrjes(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));

                        if (DbCore.DbAdmin.clsMonedha.ktheKodMonedheSipasId(id) == kodmonedhanderm)
                        {
                            txt2.Enabled = false;
                        }
                    }
                    txt2.ClientInstanceName = "VleraKursi" + e.VisibleIndex.ToString();
                    txt2.ClientSideEvents.TextChanged = "function(s,e){TextChangedVleraKursit(VleraKursi" + e.VisibleIndex.ToString() + ",'VleraKursi', " + e.VisibleIndex.ToString() + ");}";
                    txt2.ClientSideEvents.Init = "function(s,e){Utils.initTxtNumber(s,e)}";
                    txt2.ClientSideEvents.GotFocus = "function(s,e){Utils.gotFocusTxtNumer(s,e);}";
                    txt2.ClientSideEvents.LostFocus = "function(s,e){Utils.lostFocusTxtNumer(s,e); LostFocusVleraKursit(VleraKursi" + e.VisibleIndex.ToString() + ",'VleraKursi', " + e.VisibleIndex.ToString() + ");}";
                    if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 2)
                    {
                        if (ugjet)
                        {
                            ugjet = false;
                        }
                    }
                }

                if (cal != null)
                {
                    cal.ClientInstanceName = "DataKursit" + e.VisibleIndex.ToString();
                    cal.ClientSideEvents.DateChanged = "function(s,e){TextChangedDataKursit(DataKursit" + e.VisibleIndex.ToString() + ",'DataKursit', " + e.VisibleIndex.ToString() + ");}";
                }

                if (txt3 != null)
                {
                    txt3.ClientInstanceName = "NjesiaKursit" + e.VisibleIndex.ToString();
                    txt3.Text = njesia_TextBox.Text;
                    txt3.ReadOnly = true;
                }
            }
            if (e.VisibleIndex != 0)
            {
                e.Row.Visible = false;
            }
        }

        protected void ASPxGridView_Monedhat_AfterPerformCallback(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            if (e.CallbackName == "COLUMNMOVE" && ASPxGridView_Monedhat.AllColumns[int.Parse(e.Args[0])].Width.Value == 0)
            {
                ASPxGridView_Monedhat.AllColumns[int.Parse(e.Args[0])].Width = Unit.Percentage(3);
            }
            if (e.CallbackName == "APPLYFILTER" && e.Args[0] == string.Empty)
            {
                var itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
                var cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
                cmbFiltra.Text = string.Empty;
            }
            percaktoTamplate1();
            var cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            var rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            GridUtil.EmrateButonaveMbiGride(ASPxGridView_Monedhat);
        }

        protected void ruajMonedhe()
        {
            if (!Page.IsValid)
            {
                return;
            }
            var ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            var rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            bool eshteShtim;
            var monedhe = new DbCore.DbAdmin.clsMonedha();
            var idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            var idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            var idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            try
            {
                monedhe = krijoMonedhe(idPerdoruesi, idNdermarrje, rm, ci);
            }
            catch (MyException e)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(e.Message);
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, e.Message, pnlMesazhi);
                hfStatusi.Value = "false";
                return;
            }
            catch (Exception e)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(e.Message);
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgMonedhaGabimGjateKrijimit", ci), pnlMesazhi);
                hfStatusi.Value = "false";
                return;
            }
            var mesazh = new DbCore.clsMesazh();
            var tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, komponente);

            if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
            {
                if (!tedrejtaInfo.DShtim)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("mesazhRaportNukKeniTeDrejta", ci), pnlMesazhi);
                    hfStatusi.Value = "false";
                    return;
                }
                mesazh = monedhe.ruaj();
                eshteShtim = true;
            }
            else
            {
                if (!tedrejtaInfo.DMod)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("mesazhRaportNukKeniTeDrejta", ci), pnlMesazhi);
                    hfStatusi.Value = "false";
                    return;
                }
                eshteShtim = false;
                monedhe.IdMonedha = int.Parse(hfId.Value.ToString());
                mesazh = monedhe.modifiko();
            }
            if (mesazh.Status == true)
            {
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("labelRaportMesazhRuajtjaPerfundoiSukses", ci), pnlMesazhi);
                hfMonedha.Value = string.Empty;
                hfStatusi.Value = "true";
                if (eshteShtim)
                {
                    shtoMonedhaNeGrid(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), monedhe.IdMonedha, rm, ci);
                }
                else
                {
                    modifikoMonedhaNeGrid(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), monedhe.IdMonedha, rm, ci);
                }
            }
            else
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                hfStatusi.Value = "false";
            }
            ASPxPageControl1.ActiveTabIndex = 0;
        }

        private DbCore.DbAdmin.clsMonedha krijoMonedhe(int idPerdoruesi, int idNdermarrje, ResourceManager rm, CultureInfo ci)
        {
            monedha = new DbCore.DbAdmin.clsMonedha();
            monedha.KodiMonedha = kodi_TextBox.Text;
            monedha.PershkrimiMonedha = pershkrimiTextBox.Text;
            monedha.AktivMonedha = active_CheckBox.Checked;
            monedha.IdPerdoruesi = idPerdoruesi;
            monedha.IdNdermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);

            if (hfShtimModifikim.Value != "modifikim" && DbCore.DbAdmin.clsMonedha.ekziston(kodi_TextBox.Text, idNdermarrje))
            {
                throw new MyException(rm.GetString("msgMonedhaEkzistonMonMeKeteKod", ci));
            }
            if (btneLlogFitimi.Text != string.Empty)
            {
                if (!DbCore.DbKontabiliteti.clsLlogari.ekzistonLlogari(btneLlogFitimi.Text, idNdermarrje))
                {
                    throw new MyException(rm.GetString("msgMonedhaLlogariaEFitimitNukEkziston", ci));
                }
                else
                {
                    var llog = new DbCore.DbKontabiliteti.clsLlogari(btneLlogFitimi.Text, idNdermarrje);
                    if (llog.Aktiv)
                    {
                        monedha.IdLlogFitimi = llog.IdLlogari;
                    }
                    else
                    {
                        throw new MyException(rm.GetString("msgMonedhaLlogariaEFitimitJoAktive", ci));
                    }
                }
            }

            if (btneLlogHumbje.Text != string.Empty)
            {
                if (!DbCore.DbKontabiliteti.clsLlogari.ekzistonLlogari(btneLlogHumbje.Text, idNdermarrje))
                {
                    throw new MyException(rm.GetString("msgMonedhaLlogariaEHumbjesNukEkziston", ci));
                }
                else
                {
                    var llog = new DbCore.DbKontabiliteti.clsLlogari(btneLlogHumbje.Text, idNdermarrje);
                    if (llog.Aktiv)
                    {
                        monedha.IdLlogHumbje = llog.IdLlogari;
                    }
                    else
                    {
                        throw new MyException(rm.GetString("msgMonedhaLlogariaEHumbjesJoAktive", ci));
                    }
                }
            }

            colKurset = new DbCore.DbAdmin.colKurset();
            colKurset = ruajKurset();
            monedha.IdStatusDok = 1;
            monedha.IdNivelAutorizimi = cmbAutorizimiHf.Value;
            monedha.OColKurset = ruajKurset();
            string vlera = btneFormatNumri.Text;
            if(vlera == "")
            {

                monedha.IdFormatNrKursi = 3;
            }
             else   monedha.IdFormatNrKursi = DbCore.DbShare.clsFormatNr.mbushFormatNrSipasVlera(vlera);

            DbCore.DbAdmin.colLidhjetAutorizim colLidhje;
            if (cmbAutorizimiHf.Value == string.Empty)
            {
                colLidhje = new DbCore.DbAdmin.colLidhjetAutorizim();
            }
            else
            {
                var colLidhjet = new DbCore.DbAdmin.colLidhjetAutorizim();
                var pars = cmbAutorizimiHf.Value.Split(',');
                for (var i = 0; i < pars.Length; i++)
                {
                    var lidhje = new DbCore.DbAdmin.clsLidhjeAutorizim();
                    lidhje.IdAutorizimeKoka = DbCore.DbAdmin.clsAutorizimKoka.ktheIDAutorizim(pars[i]);
                    if (lidhje.IdAutorizimeKoka <= 0)
                    {
                        throw new MyException(rm.GetString("msgCeljeArkaBankaNiveliAutorizimitNukEkziston", ci));
                    }
                    colLidhjet.Add(lidhje);
                }
                colLidhje = colLidhjet;
            }
            monedha.OColLidhjetAutorizim = colLidhje;
            return monedha;
        }

        protected void gvKurset_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
        }

        protected void gvKurset_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpNoRows"] = gvKurset.VisibleRowCount;
            e.Properties["cpNoPage"] = gvKurset.PageIndex;
        }

        protected DbCore.DbAdmin.colKurset formoColKurset(String kurset)
        {
            colKurset = new DbCore.DbAdmin.colKurset();
            var arrKurset = kurset.ToString().Split(';');

            var arrLloji = arrKurset[0].Split(',');
            for (var i = 0; i < arrLloji.Length; i++)
            {
                var arrCompLloji = arrLloji[i].Split(':');
                if (arrCompLloji.Length != 1)
                {
                    if (Convert.ToInt32(arrCompLloji[0]) < colKurset.Count())
                    {
                        colKurset[Convert.ToInt32(arrCompLloji[0])].LlojKursi = int.Parse(arrCompLloji[1].ToString());
                    }
                    else
                    {
                        var oKursi = new DbCore.DbAdmin.clsKurset();
                        oKursi.LlojKursi = int.Parse(arrCompLloji[1].ToString());
                        colKurset.Add(oKursi);
                    }
                }
            }

            var arrVlera = arrKurset[1].Split(',');
            for (var i = 0; i < arrVlera.Length; i++)
            {
                var arrCompVlera = arrVlera[i].Split(':');
                if (arrCompVlera.Length != 1 && arrCompVlera[1] != " ")
                {
                    if (Convert.ToInt32(arrCompVlera[0]) < colKurset.Count())
                    {
                        colKurset[Convert.ToInt32(arrCompVlera[0])].VleraKursi = double.Parse(arrCompVlera[1]);
                    }
                    else
                    {
                        var oKursi = new DbCore.DbAdmin.clsKurset();
                        oKursi.VleraKursi = double.Parse(arrCompVlera[1]);
                        colKurset.Add(oKursi);
                    }
                }
            }

            var arrData = arrKurset[2].Split(',');
            for (var i = 0; i < arrData.Length; i++)
            {
                var arrCompData = arrData[i].Split(':');
                if (arrCompData.Length != 1 && arrCompData[1] != " ")
                {
                    if (Convert.ToInt32(arrCompData[0]) < colKurset.Count())
                    {
                        colKurset[Convert.ToInt32(arrCompData[0])].DataKursit = DateTime.Parse(arrCompData[1]);
                    }
                    else
                    {
                        var oKursi = new DbCore.DbAdmin.clsKurset();
                        oKursi.DataKursit = DateTime.Parse(arrCompData[1]);
                        colKurset.Add(oKursi);
                    }
                }
            }

            var arrNjesia = arrKurset[3].Split(',');
            for (var i = 0; i < arrNjesia.Length; i++)
            {
                var arrCompNjesia = arrNjesia[i].Split(':');
                if (arrCompNjesia.Length != 1 && arrCompNjesia[1] != " ")
                {
                    if (Convert.ToInt32(arrCompNjesia[0]) < colKurset.Count())
                    {
                        colKurset[Convert.ToInt32(arrCompNjesia[0])].NjesiaKursit = int.Parse(arrCompNjesia[1]);
                    }
                    else
                    {
                        var oKursi = new DbCore.DbAdmin.clsKurset();
                        oKursi.NjesiaKursit = int.Parse(arrCompNjesia[1]);
                        colKurset.Add(oKursi);
                    }
                }
            }
            var arrPerLloji = arrKurset[4].Split(',');
            for (var i = 0; i < arrPerLloji.Length; i++)
            {
                var arrCompLloji = arrPerLloji[i].Split(':');
                if (arrCompLloji.Length != 1)
                {
                    if (Convert.ToInt32(arrCompLloji[0]) < colKurset.Count())
                    {
                        colKurset[Convert.ToInt32(arrCompLloji[0])].PershkrimLlojKursi = arrCompLloji[1];
                    }
                    else
                    {
                        var oKursi = new DbCore.DbAdmin.clsKurset();
                        oKursi.PershkrimLlojKursi = arrCompLloji[1];
                        colKurset.Add(oKursi);
                    }
                }
            }
            return colKurset;
        }

        protected DbCore.DbAdmin.colKurset ruajKurset()
        {
            var colKurset = new DbCore.DbAdmin.colKurset();
            var serializusi = new JavaScriptSerializer();
            serializusi.MaxJsonLength = 500000000;
            var kurset = (object[])serializusi.DeserializeObject(hfKurset.Value);

            if (kurset == null)
            {
                return colKurset;
            }
            foreach (object oo in kurset)
            {
                var kursi = new DbCore.DbAdmin.clsKurset();
                kursi = kursi.krijoKursin((Dictionary<string, object>)oo, idNdermarrje);
                colKurset.Add(kursi);
            }
            return colKurset;
        }

        private void percaktoTamplate1()
        {
            var col = ASPxGridView_Monedhat.Columns["AktivMonedha"] as GridViewDataColumn;
            col.DataItemTemplate = new MyCheckTemplate(true, false);
        }

        protected void ASPxGridView_Monedhat_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            var idkomponente = string.Empty;
            var kodkonfigurimi = string.Empty;

            var arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                kodkonfigurimi = arr[1];
                idkomponente = arr[0];
                if (arr[2] == string.Empty)
                {
                    ASPxGridView_Monedhat.FilterExpression = string.Empty;
                }
                else
                {
                    var filtra = new DbCore.DbAdmin.clsFiltraGrida();

                    var koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "ASPxGridView_Monedhat", komponente, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), int.Parse(cmbKonfigurimi.Value.ToString()));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);

                    if (filtra.FiltraKodi != null)
                    {
                        ASPxGridView_Monedhat.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, ASPxGridView_Monedhat);
                        hfStatusi.Value = "true";
                    }
                    else
                    {
                        hfStatusi.Value = "false";
                    }
                }
            }
            else
            {
                if (arr.Length == 2)
                {
                    kodkonfigurimi = arr[1];
                    idkomponente = arr[0];
                }
                else
                {
                    idkomponente = e.Parameters;
                }
            }
            ASPxGridView_Monedhat.Selection.UnselectAll();
        }

        protected void ASPxGridView_Monedhat_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = ASPxGridView_Monedhat.PageIndex;
            e.Properties["cpPageRow"] = ASPxGridView_Monedhat.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = ASPxGridView_Monedhat.VisibleRowCount;
        }

        protected void ASPxGridView_Monedhat_DataBound(object sender, EventArgs e)
        {
            if (ASPxGridView_Monedhat.Columns["#"] == null)
            {
                var check = new GridViewCommandColumn("#");
                ASPxGridView_Monedhat.Columns.Add(check);
                check.ShowSelectCheckbox = true;
                check.Width = Unit.Percentage(2);
                ASPxGridView_Monedhat.SettingsText.CommandUpdate = "Ruaj";
                ASPxGridView_Monedhat.SettingsText.CommandCancel = "Anullo";
                ASPxGridView_Monedhat.Settings.ShowFilterRow = true;
                ASPxGridView_Monedhat.KeyFieldName = "IdMonedha";
                ASPxGridView_Monedhat.SettingsBehavior.AllowSelectByRowClick = true;
                ASPxGridView_Monedhat.SettingsBehavior.AllowFocusedRow = true;
                ASPxGridView_Monedhat.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                ASPxGridView_Monedhat.Settings.ShowFilterRowMenu = true;
            }
        }

        protected void ASPxGridView_Monedhat_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            var ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            var rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            var TeGjithe = rm.GetString("GridHeaderFilterFillItemTeGjithe", ci);
            var nga = rm.GetString("GridHeaderFilterFillItemNga", ci);
            if (e.Column.FieldName == "PershkrimiMonedha")
            {
                e.Values.Clear();
                e.AddValue(TeGjithe, string.Empty, "true");
                e.AddValue(nga + " A-D ", string.Empty, e.Column.FieldName + ">'A     ' and " + e.Column.FieldName + " <'DDDDDDD'");
                e.AddValue(nga + " D-G ", string.Empty, e.Column.FieldName + ">'D     ' and " + e.Column.FieldName + "<'GGGGGGG'");
                e.AddValue(nga + " H-K ", string.Empty, e.Column.FieldName + ">'H     ' and " + e.Column.FieldName + "<'KKKKKKK'");
                e.AddValue(nga + " L-O ", string.Empty, e.Column.FieldName + ">'L     ' and " + e.Column.FieldName + "  <'OOOOOOO'");
                e.AddValue(nga + " P-S ", string.Empty, e.Column.FieldName + ">'P     ' and " + e.Column.FieldName + "<'SSSSSSS'");
                e.AddValue(nga + " T-W ", string.Empty, e.Column.FieldName + ">'T     ' and " + e.Column.FieldName + "<'WWWWWWW'");
                e.AddValue(nga + " X-Z ", string.Empty, e.Column.FieldName + ">'X     ' and " + e.Column.FieldName + "<'ZZZZZZZ'");
            }
            else
            {
                e.Values.Clear();
                e.AddValue(TeGjithe, string.Empty, "true");
            }
        }

        protected void ASPxGridView_Monedhat_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if (e.Column.FieldName == "IdLlogHumbje" || e.Column.FieldName == "IdLlogFitimi")
            {
                if (Converter.ConvertToInt(e.Value)==0)
                {
                    e.Criteria = null;
                }
            }
        }

        protected void gvHistoriku_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            mbushGridenHistoriku();
        }

        protected void gvKurset_DataBound(object sender, EventArgs e)
        {
        }

        private void mbushGridenKurset()
        {
            var trupiFillestar = string.Empty;
            var id = int.Parse(hfId.Value.ToString());
            var tempColKurset = new DbCore.DbAdmin.colKurset();
            var colKurset = new DbCore.DbAdmin.colKurset();
            tempColKurset.mbushKursetFunditMonedhes(id);
            var lloji = 0;
            foreach (DbCore.DbAdmin.clsKurset kursi in tempColKurset)
            {
                if (lloji != kursi.LlojKursi)
                {
                    string vlereKursi;
                    if (btneFormatNumri.Text != string.Empty)
                    {
                        vlereKursi = kursi.VleraKursi.ToString("################################" + btneFormatNumri.Text);
                    }
                    else
                    {
                        vlereKursi = kursi.VleraKursi.ToString();
                    }
                    trupiFillestar += kursi.LlojKursi + ":" + vlereKursi + ":" + DbCore.clsFunksione.ktheDateFormat(kursi.DataKursit) + ":" + kursi.NjesiaKursit + ":" + kursi.PershkrimLlojKursi + ";";
                    njesia_TextBox.Text = tempColKurset[0].NjesiaKursit.ToString();
                    colKurset.Add(kursi);
                    lloji = kursi.LlojKursi;
                }
            }
            hfTrupiFillimit.Value = trupiFillestar;

            gvKurset.DataSource = colKurset;
            gvKurset.DataBind();
            updateraporti.Update();
        }

        protected void btneLlogFitimi_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("btneLlogFitimi"))
            {
                ConfigureAspxComboBox.mbushComboLlogariaPaKolona(IdPerdoruesi, IdNdermarrja, btneLlogFitimi, e);
            }
        }

        protected void btneLlogFitimi_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("btneLlogFitimi"))
            {
                ConfigureAspxComboBox.mbushComboLlogariaPaKolona(IdPerdoruesi, IdNdermarrja, btneLlogFitimi, e);
            }
        }

        protected void btneLlogHumbje_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("btneLlogHumbje"))
            {
                ConfigureAspxComboBox.mbushComboLlogariaPaKolona(IdPerdoruesi, IdNdermarrja, btneLlogHumbje, e);
            }
        }

        protected void btneLlogHumbje_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("btneLlogHumbje"))
            {
                ConfigureAspxComboBox.mbushComboLlogariaPaKolona(IdPerdoruesi, IdNdermarrja, btneLlogHumbje, e);
            }
        }

        private void konfiguroGrideHistoriku(int idNdermarrje, int idGjuha)
        {
            var ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            var rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            KonfigurimComboGride.shtoLlojeKursesh(gvHistoriku, rm, ci);
            GridUtil.konfigGrideListeEMadhePaTheme(gvHistoriku, "IdKursi");
            var col2 = gvHistoriku.Columns["VleraKursi"] as GridViewDataColumn;
            col2.PropertiesEdit.DisplayFormatString = btneFormatNumri.Text;
        }


        protected void gvKurset_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (btneFormatNumri.Text != string.Empty)
            {
                var kursi = gvKurset.Columns["VleraKursi"] as GridViewDataTextColumn;
                kursi.PropertiesEdit.DisplayFormatString = btneFormatNumri.Text;
                kursi.DataItemTemplate = new MyDoubleTemplate(true, int.Parse(btneFormatNumri.Value.ToString()), "0");
            }
        }

        protected void gvHistoriku_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (hfShtimModifikim.Value == "modifikim" || hfShtimModifikim.Value == "klonim")
            {
                mbushGridenHistoriku();
            }
            else
            {
                gvHistoriku.DataSource = new DbCore.DbAdmin.colKurset();
                gvHistoriku.DataBind();
            }
            var col2 = gvHistoriku.Columns["VleraKursi"] as GridViewDataColumn;
            col2.PropertiesEdit.DisplayFormatString = btneFormatNumri.Text;
        }
    }
}
