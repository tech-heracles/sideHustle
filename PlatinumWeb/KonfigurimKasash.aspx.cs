using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.IO;
using PlatinumWeb.Templates;
using System.Globalization;
using System.Resources;
using DbCore;
using System.Data;
using DbCore.DbAdmin;
using DbCore.IMBUtils.Types;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.DbRegjistrim;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    public partial class KonfigurimKasash : MyPageBase
    {
        private const String mesazhGabimi = "Ndodhi nje gabim gjate ruajtjes se te dhenave!";
        private string komponente = "KonfigurimKasash.aspx";
        private string guidString;
        protected void Page_Load(object sender, EventArgs e)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                DbCore.clsFunksione.logout(Session, true, "FaqePaautorizuar");
            }
            int idPerd = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);

            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerd);
            }
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), idPerd, idNdermarrje);
            if (!Page.IsPostBack)
            {
                guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                hfState.Set("guidString", guidString);
                hfState.Set("idNdermarrje", idNdermarrje);
                EmrateTabeve(cultinf, rm);
                EmrateButonave(cultinf, rm);
                perktheLabel(cultinf, rm);
                mbushHiddenFieldMePerkthime(cultinf, rm);
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                int idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerd, idNdermarrje, idViti, komponente);
                hfTeDrejta.Set("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Set("Modifikim", tedrejtaInfo.DMod);
                konfiguroVleraFillestare(idNdermarrje);
                mbushGridKonfigKasashNgaDB(idNdermarrje);
                konfiguroGride();
                ASPxPageControl1.ActiveTabIndex = 0;
            }
            else
            {
                guidString = (string)hfState["guidString"];
                mbushGridKonfigKasashNgaSession(idNdermarrje);
                konfiguroGride();
            }
        }

        /// <summary>
        /// Vendos emrat e tabeve ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateTabeve(CultureInfo cultinf, ResourceManager rm)
        {
            ASPxPageControl1.TabPages[0].Text = rm.GetString("lblTabKasat", cultinf);
            ASPxPageControl1.TabPages[1].Text = rm.GetString("konfigurimiKasesTab", cultinf);
            ASPxPageControl1.TabPages[2].Text = rm.GetString("raporteTab", cultinf);
        }

        /// <summary>
        /// Vendos emrat e butonave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="cultinf">Merr culture info perkatese</param>
        /// <param name="rm">Merr Resource Manager</param>
        private void EmrateButonave(CultureInfo cultinf, ResourceManager rm)
        {
            ButtonCancel.Text = rm.GetString("btnAdministrimiCancel", cultinf);
            mbyllXhiroButon.Text = rm.GetString("btnMbyllXhiron", cultinf);
            btnRaportiX.Text = rm.GetString("btnRaportiX", cultinf);
            btnRaportiZ.Text = rm.GetString("btnRaportiZ", cultinf);
            btnPlu.Text = rm.GetString("btnFshiPLU", cultinf);
        }

        public void perktheLabel(CultureInfo cultinf, ResourceManager rm)
        {
            // perkthime label tek P.A.SH
            lblKodi.Text = rm.GetString("lblKodi", cultinf);
            lblLlojiKases.Text = rm.GetString("lblLlojKase", cultinf);
            lblUrl.Text = rm.GetString("lblUrlKase", cultinf);
            lblPortaComBNTAClass.Text = rm.GetString("lblPortaCom", cultinf);
            cbKasaMeShifraDhjetoreBNTAClass.Text = rm.GetString("lblKasaBNTAClass", cultinf);
        }
        private void mbushGridKonfigKasashNgaDB(int idNdermarrje)
        {//mbush griden e popupit me te dhena
            DataTable dt = DbCore.DbAdmin.colKonfigurimeKase.merrKonfigurimetSipasNdermarjesDT(idNdermarrje);
            DbCore.mySessionObjects.ruajGrideNeSession(Session, dt);
            gvKasat.DataSource = dt;
            gvKasat.DataBind();
            dt.Dispose();
        }

        private void mbushGridKonfigKasashNgaSession(int idNdermarrje)
        {
            DataTable tmpObject;
            bool sukses = DbCore.mySessionObjects.merrGrideNgaSessioni(Session, out tmpObject);
            if (!sukses)
                mbushGridKonfigKasashNgaDB(idNdermarrje);
            else
            {
                gvKasat.DataSource = tmpObject;
                gvKasat.DataBind();
                tmpObject.Dispose();
            }
        }

        private void konfiguroGride()
        {
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            KonfigurimComboGride.shtoLlojKase(gvKasat, Session, komponente, guidString);
            KonfigurimComboGride.shtoLlojObjekti(gvKasat, rm, ci);

            gvKasat.Columns["IdKonfigurimi"].Visible = false;
            gvKasat.Columns["#"].VisibleIndex = 0;
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
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, komponente, this, MenuInfo, hfShtimModifikim.Value == "modifikim" ? false : true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
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
        /// metoda per te thirrur veprimet e menuse kur shtypen butonat
        /// </summary>
        /// <param name="source">derguesi</param>
        /// <param name="e">parametrat</param>
        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {
            if (e.Item.Name == "Ruaj")
            {
                Page.Validate("entries");
                ruajKonfigurim(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                percaktoTemplate();
            }
        }
        #region komentuar
        //public void lexoKonfigurimin(int idKonfigurimi)
        //{
        //    if (idKonfigurimi != 0)
        //    {
        //        colVleratKonfigurimiKasa OColVlerat = new colVleratKonfigurimiKasa(idKonfigurimi);
        //        string llojkase = OColVlerat.ktheVlereOpsioni("KASEFISKALELLOJ");
        //        string pathFile = hfFile.Value = OColVlerat.ktheVlereOpsioni("KASEFISKALEPATH");
        //        cmbLlojiKases.Value = llojkase;
        //        string urlKase = OColVlerat.ktheVlereOpsioni("URL"); ;
        //        txtUrl.Text = urlKase;
        //        hfState.Set("pathFile", pathFile);
        //        hfState.Set("URL", urlKase);
        //        url = urlKase;
        //        switch (llojkase)
        //        {
        //            case "0":
        //                //lblSkedariIVA.Text = pathFile;
        //                ucIVA.Text = pathFile;
        //                cbKasaMeShifraDhjetore.Checked = Convert.ToBoolean(OColVlerat.ktheVlereOpsioni("MESHIFRADHJETORE"));
        //                rbKase.Checked = Convert.ToBoolean(OColVlerat.ktheVlereOpsioni("KASEAPOPRINTER"));
        //                rbPrinter.Checked = !rbKase.Checked;
        //                cbPrintoNrFature.Checked = Convert.ToBoolean(OColVlerat.ktheVlereOpsioni("PRINTONRFATURE"));
        //                if (OColVlerat.ktheVlereOpsioni("CMIMMONEDHEDYTE") != "0")
        //                { this.cbPrintoTotalDheNeMon.Checked = true; cmbMonedha.ClientEnabled = true; }
        //                else
        //                    this.cbPrintoTotalDheNeMon.Checked = false;
        //                cmbMonedha.Value = OColVlerat.ktheVlereOpsioni("CMIMMONEDHEDYTE");
        //                this.rbKuponTatimor.Checked = Convert.ToBoolean(OColVlerat.ktheVlereOpsioni("KUPONTATIMOR"));
        //                this.rbFatureTatimore.Checked = !rbKuponTatimor.Checked;
        //                if (OColVlerat.ktheVlereOpsioni("NRKOPJESH") != "0")
        //                { this.cbPrintoNrKopjeFature.Checked = true; txtNrKopje.ClientEnabled = true; }
        //                else this.cbPrintoNrKopjeFature.Checked = false;
        //                txtNrKopje.Text = OColVlerat.ktheVlereOpsioni("NRKOPJESH");
        //                if (OColVlerat.ktheVlereOpsioni("NRKOPJESHKTHIMI") != "0")
        //                { this.cbPrintoKopjeTeKthimeve.Checked = true; txtNrKopjeKthimesh.ClientEnabled = true; }
        //                else this.cbPrintoKopjeTeKthimeve.Checked = false;
        //                this.txtNrKopjeKthimesh.Text = OColVlerat.ktheVlereOpsioni("NRKOPJESHKTHIMI");
        //                cbPrintoManualishtNgaKasa.Checked = Convert.ToBoolean(OColVlerat.ktheVlereOpsioni("PRINTOMANUALISHTNGAKASA"));
        //                break;
        //            case "1":
        //                //lblSkedariAED.Text = pathFile;
        //                ucAED.Text = pathFile;
        //                txtPorta.Text = OColVlerat.ktheVlereOpsioni("PORT");
        //                if (OColVlerat.ktheVlereOpsioni("CHIUS") == "1")
        //                    cbMbyllCdoFature.Checked = true;
        //                else cbMbyllCdoFature.Checked = false;
        //                this.cbPrintoBarkod.Checked = Convert.ToBoolean(OColVlerat.ktheVlereOpsioni("PRINTBARKOD"));
        //                this.cbRuajKopje.Checked = Convert.ToBoolean(OColVlerat.ktheVlereOpsioni("RUAJKOPJE"));
        //                cbKasaMeShifraDhjetoreAED.Checked = Convert.ToBoolean(OColVlerat.ktheVlereOpsioni("MESHIFRADHJETORE"));

        //                break;
        //            case "2":
        //                //lblSkedariBTN.Text = pathFile;
        //                ucBTN.Text = pathFile;
        //                cbKasaMeShifraDhjetoreBTN.Checked = Convert.ToBoolean(OColVlerat.ktheVlereOpsioni("MESHIFRADHJETORE"));
        //                rbKaseBTN.Checked = Convert.ToBoolean(OColVlerat.ktheVlereOpsioni("KASEAPOPRINTER"));
        //                rbPrinterBTN.Checked = !rbKaseBTN.Checked;
        //                cbPrintoNrFatureBTN.Checked = Convert.ToBoolean(OColVlerat.ktheVlereOpsioni("PRINTONRFATURE"));
        //                if (OColVlerat.ktheVlereOpsioni("CMIMMONEDHEDYTE") != "0")
        //                { this.cbPrintoTotalDheNeMonBTN.Checked = true; cmbMonedhaBTN.ClientEnabled = true; }
        //                else this.cbPrintoTotalDheNeMonBTN.Checked = false;
        //                cmbMonedhaBTN.Value = OColVlerat.ktheVlereOpsioni("CMIMMONEDHEDYTE");
        //                this.rbKuponTatimorBTN.Checked = Convert.ToBoolean(OColVlerat.ktheVlereOpsioni("KUPONTATIMOR"));
        //                this.rbFatureTatimoreBTN.Checked = !rbKuponTatimorBTN.Checked;
        //                if (OColVlerat.ktheVlereOpsioni("NRKOPJESH") != "0")
        //                { this.cbPrintoNrKopjeFatureBTN.Checked = true; txtNrKopjeBTN.ClientEnabled = true; }
        //                else this.cbPrintoNrKopjeFatureBTN.Checked = false;
        //                txtNrKopjeBTN.Text = OColVlerat.ktheVlereOpsioni("NRKOPJESH");
        //                if (OColVlerat.ktheVlereOpsioni("NRKOPJESHKTHIMI") != "0")
        //                { this.cbPrintoKopjeTeKthimeveBTN.Checked = true; txtNrKopjeKthimeshBTN.ClientEnabled = true; }
        //                else this.cbPrintoKopjeTeKthimeveBTN.Checked = false;
        //                this.txtNrKopjeKthimeshBTN.Text = OColVlerat.ktheVlereOpsioni("NRKOPJESHKTHIMI");
        //                break;
        //            case "3":
        //                //lblSkedariCKVNOKI.Text = pathFile;
        //                ucCKVNOKI.Text = pathFile;
        //                txtPortaCom.Text = OColVlerat.ktheVlereOpsioni("COMPORT");
        //                cbKasaMeShifraDhjetoreCKVNOKI.Checked = Convert.ToBoolean(OColVlerat.ktheVlereOpsioni("MESHIFRADHJETORE"));
        //                txtBoudRate.Text = OColVlerat.ktheVlereOpsioni("BOUDRATE");

        //                cbPrintoNrFatureCKVNOKI.Checked = Convert.ToBoolean(OColVlerat.ktheVlereOpsioni("PRINTONRFATURE"));
        //                if (OColVlerat.ktheVlereOpsioni("CMIMMONEDHEDYTE") != "0")
        //                { this.cbPrintoTotalDheNeMonCKVNOKI.Checked = true; cmbMonedhaCKVNOKI.ClientEnabled = true; }
        //                else this.cbPrintoTotalDheNeMonCKVNOKI.Checked = false;
        //                cmbMonedhaCKVNOKI.Value = OColVlerat.ktheVlereOpsioni("CMIMMONEDHEDYTE");

        //                break;
        //            case "4":
        //                //lblSkedariPKP.Text = pathFile;
        //                ucPKP.Text = pathFile;
        //                cbKasaMeShifraDhjetorePKP.Checked = Convert.ToBoolean(OColVlerat.ktheVlereOpsioni("MESHIFRADHJETORE"));
        //                cbPrintoNrFaturePKP.Checked = Convert.ToBoolean(OColVlerat.ktheVlereOpsioni("PRINTONRFATURE"));
        //                if (OColVlerat.ktheVlereOpsioni("CMIMMONEDHEDYTE") != "0")
        //                { this.cbPrintoTotalDheNeMonPKP.Checked = true; cmbMonedhaPKP.ClientEnabled = true; }
        //                else this.cbPrintoTotalDheNeMonPKP.Checked = false;
        //                cmbMonedhaPKP.Value = OColVlerat.ktheVlereOpsioni("CMIMMONEDHEDYTE");
        //                if (OColVlerat.ktheVlereOpsioni("NRKOPJESH") != "0")
        //                { this.cbPrintoNrKopjeFaturePKP.Checked = true; txtNrKopjePKP.ClientEnabled = true; }
        //                else this.cbPrintoNrKopjeFaturePKP.Checked = false;
        //                txtNrKopjePKP.Text = OColVlerat.ktheVlereOpsioni("NRKOPJESH");
        //                if (OColVlerat.ktheVlereOpsioni("NRKOPJESHKTHIMI") != "0")
        //                { this.cbPrintoKopjeTeKthimevePKP.Checked = true; txtNrKopjeKthimeshPKP.ClientEnabled = true; }
        //                else this.cbPrintoKopjeTeKthimevePKP.Checked = false;
        //                this.txtNrKopjeKthimeshPKP.Text = OColVlerat.ktheVlereOpsioni("NRKOPJESHKTHIMI");
        //                break;
        //            case "5":
        //                //lblSkedariGEKOS.Text = pathFile;
        //                ucGEKOS.Text = pathFile;
        //                this.txtPassOperator.Text = OColVlerat.ktheVlereOpsioni("OPERATORPASS");
        //                cmbGjuha.Value = OColVlerat.ktheVlereOpsioni("GJUHA");
        //                txtKodiTVSH.Text = OColVlerat.ktheVlereOpsioni("NIVELDEFAULTTVSH");
        //                if (OColVlerat.ktheVlereOpsioni("NRKOPJESH") != "0")
        //                { this.cbPrintoNrKopjeFatureGEKOS.Checked = true; txtNrKopjeGEKOS.ClientEnabled = true; }
        //                else this.cbPrintoNrKopjeFatureGEKOS.Checked = false;
        //                txtNrKopjeGEKOS.Text = OColVlerat.ktheVlereOpsioni("NRKOPJESH");
        //                cbKasaMeShifraDhjetoreGEKOS.Checked = Convert.ToBoolean(OColVlerat.ktheVlereOpsioni("MESHIFRADHJETORE"));
        //                cbPrintoNrFatureGEKOS.Checked = Convert.ToBoolean(OColVlerat.ktheVlereOpsioni("PRINTONRFATURE"));
        //                break;
        //            case "6":
        //                ucBNTAClass.Text = pathFile;
        //                txtPortaComBNTAClass.Text = OColVlerat.ktheVlereOpsioni("COMPORT");
        //                cbKasaMeShifraDhjetoreBNTAClass.Checked = Convert.ToBoolean(OColVlerat.ktheVlereOpsioni("MESHIFRADHJETORE"));
        //                txtBoudRateBNTAClass.Text = OColVlerat.ktheVlereOpsioni("BOUDRATE");
        //                break;
        //        }
        //    }

        //}
        //private void mbushkonfigurimin()
        //{
        //    DbCore.DbAdmin.clsKonfigurimKase konf = new DbCore.DbAdmin.clsKonfigurimKase();
        //    konf.merrKonfiguriminSipasNdermarjes(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //    if (konf.IdKonfigurimi != 0)
        //    {
        //        konf.OColVlerat = new DbCore.DbAdmin.colVleratKonfigurimiKasa(konf.IdKonfigurimi);
        //        DbCore.DbAdmin.clsVleraKonfigurimiKasa vlera = new DbCore.DbAdmin.clsVleraKonfigurimiKasa();
        //        string llojkase = vlera.merrVlereOpsioni("KASEFISKALELLOJ");
        //        hfFile.Value = vlera.merrVlereOpsioni("KASEFISKALEPATH");
        //        cmbLlojiKases.Value = llojkase;
        //        txtUrl.Text = vlera.merrVlereOpsioni("URL");
        //        switch (llojkase)
        //        {
        //            case "0":
        //                //lblSkedariIVA.Text = vlera.merrVlereOpsioni("KASEFISKALEPATH");
        //                ucIVA.Text = vlera.merrVlereOpsioni("KASEFISKALEPATH");
        //                cbKasaMeShifraDhjetore.Checked = Convert.ToBoolean(vlera.merrVlereOpsioni("MESHIFRADHJETORE"));
        //                rbKase.Checked = Convert.ToBoolean(vlera.merrVlereOpsioni("KASEAPOPRINTER"));
        //                rbPrinter.Checked = !rbKase.Checked;
        //                cbPrintoNrFature.Checked = Convert.ToBoolean(vlera.merrVlereOpsioni("PRINTONRFATURE"));
        //                if (vlera.merrVlereOpsioni("CMIMMONEDHEDYTE") != "0")
        //                { this.cbPrintoTotalDheNeMon.Checked = true; cmbMonedha.ClientEnabled = true; }
        //                else this.cbPrintoTotalDheNeMon.Checked = false;
        //                cmbMonedha.Value = vlera.merrVlereOpsioni("CMIMMONEDHEDYTE");
        //                this.rbKuponTatimor.Checked = Convert.ToBoolean(vlera.merrVlereOpsioni("KUPONTATIMOR"));
        //                this.rbFatureTatimore.Checked = !rbKuponTatimor.Checked;
        //                if (vlera.merrVlereOpsioni("NRKOPJESH") != "0")
        //                { this.cbPrintoNrKopjeFature.Checked = true; txtNrKopje.ClientEnabled = true; }
        //                else this.cbPrintoNrKopjeFature.Checked = false;
        //                txtNrKopje.Text = vlera.merrVlereOpsioni("NRKOPJESH");
        //                if (vlera.merrVlereOpsioni("NRKOPJESHKTHIMI") != "0")
        //                { this.cbPrintoKopjeTeKthimeve.Checked = true; txtNrKopjeKthimesh.ClientEnabled = true; }
        //                else this.cbPrintoKopjeTeKthimeve.Checked = false;
        //                this.txtNrKopjeKthimesh.Text = vlera.merrVlereOpsioni("NRKOPJESHKTHIMI");
        //                break;
        //            case "1":
        //                //lblSkedariAED.Text = vlera.merrVlereOpsioni("KASEFISKALEPATH");
        //                ucAED.Text = vlera.merrVlereOpsioni("KASEFISKALEPATH");
        //                txtPorta.Text = vlera.merrVlereOpsioni("PORT");
        //                if (vlera.merrVlereOpsioni("CHIUS") == "1")
        //                    cbMbyllCdoFature.Checked = true;
        //                else cbMbyllCdoFature.Checked = false;
        //                this.cbPrintoBarkod.Checked = Convert.ToBoolean(vlera.merrVlereOpsioni("PRINTBARKOD"));
        //                this.cbRuajKopje.Checked = Convert.ToBoolean(vlera.merrVlereOpsioni("RUAJKOPJE"));
        //                cbKasaMeShifraDhjetoreAED.Checked = Convert.ToBoolean(vlera.merrVlereOpsioni("MESHIFRADHJETORE"));

        //                break;
        //            case "2":
        //                //lblSkedariBTN.Text = vlera.merrVlereOpsioni("KASEFISKALEPATH");
        //                ucBTN.Text = vlera.merrVlereOpsioni("KASEFISKALEPATH");
        //                cbKasaMeShifraDhjetoreBTN.Checked = Convert.ToBoolean(vlera.merrVlereOpsioni("MESHIFRADHJETORE"));
        //                rbKaseBTN.Checked = Convert.ToBoolean(vlera.merrVlereOpsioni("KASEAPOPRINTER"));
        //                rbPrinterBTN.Checked = !rbKaseBTN.Checked;
        //                cbPrintoNrFatureBTN.Checked = Convert.ToBoolean(vlera.merrVlereOpsioni("PRINTONRFATURE"));
        //                if (vlera.merrVlereOpsioni("CMIMMONEDHEDYTE") != "0")
        //                { this.cbPrintoTotalDheNeMonBTN.Checked = true; cmbMonedhaBTN.ClientEnabled = true; }
        //                else this.cbPrintoTotalDheNeMonBTN.Checked = false;
        //                cmbMonedhaBTN.Value = vlera.merrVlereOpsioni("CMIMMONEDHEDYTE");
        //                this.rbKuponTatimorBTN.Checked = Convert.ToBoolean(vlera.merrVlereOpsioni("KUPONTATIMOR"));
        //                this.rbFatureTatimoreBTN.Checked = !rbKuponTatimorBTN.Checked;
        //                if (vlera.merrVlereOpsioni("NRKOPJESH") != "0")
        //                { this.cbPrintoNrKopjeFatureBTN.Checked = true; txtNrKopjeBTN.ClientEnabled = true; }
        //                else this.cbPrintoNrKopjeFatureBTN.Checked = false;
        //                txtNrKopjeBTN.Text = vlera.merrVlereOpsioni("NRKOPJESH");
        //                if (vlera.merrVlereOpsioni("NRKOPJESHKTHIMI") != "0")
        //                { this.cbPrintoKopjeTeKthimeveBTN.Checked = true; txtNrKopjeKthimeshBTN.ClientEnabled = true; }
        //                else this.cbPrintoKopjeTeKthimeveBTN.Checked = false;
        //                this.txtNrKopjeKthimeshBTN.Text = vlera.merrVlereOpsioni("NRKOPJESHKTHIMI");
        //                break;
        //            case "3":
        //                //lblSkedariCKVNOKI.Text = vlera.merrVlereOpsioni("KASEFISKALEPATH");
        //                ucCKVNOKI.Text = vlera.merrVlereOpsioni("KASEFISKALEPATH");
        //                txtPortaCom.Text = vlera.merrVlereOpsioni("COMPORT");
        //                cbKasaMeShifraDhjetoreCKVNOKI.Checked = Convert.ToBoolean(vlera.merrVlereOpsioni("MESHIFRADHJETORE"));
        //                txtBoudRate.Text = vlera.merrVlereOpsioni("BOUDRATE");

        //                cbPrintoNrFatureCKVNOKI.Checked = Convert.ToBoolean(vlera.merrVlereOpsioni("PRINTONRFATURE"));
        //                if (vlera.merrVlereOpsioni("CMIMMONEDHEDYTE") != "0")
        //                { this.cbPrintoTotalDheNeMonCKVNOKI.Checked = true; cmbMonedhaCKVNOKI.ClientEnabled = true; }
        //                else this.cbPrintoTotalDheNeMonCKVNOKI.Checked = false;
        //                cmbMonedhaCKVNOKI.Value = vlera.merrVlereOpsioni("CMIMMONEDHEDYTE");

        //                break;
        //            case "4":
        //                //lblSkedariPKP.Text = vlera.merrVlereOpsioni("KASEFISKALEPATH");
        //                ucPKP.Text = vlera.merrVlereOpsioni("KASEFISKALEPATH");
        //                cbKasaMeShifraDhjetorePKP.Checked = Convert.ToBoolean(vlera.merrVlereOpsioni("MESHIFRADHJETORE"));
        //                cbPrintoNrFaturePKP.Checked = Convert.ToBoolean(vlera.merrVlereOpsioni("PRINTONRFATURE"));
        //                if (vlera.merrVlereOpsioni("CMIMMONEDHEDYTE") != "0")
        //                { this.cbPrintoTotalDheNeMonPKP.Checked = true; cmbMonedhaPKP.ClientEnabled = true; }
        //                else this.cbPrintoTotalDheNeMonPKP.Checked = false;
        //                cmbMonedhaPKP.Value = vlera.merrVlereOpsioni("CMIMMONEDHEDYTE");
        //                if (vlera.merrVlereOpsioni("NRKOPJESH") != "0")
        //                { this.cbPrintoNrKopjeFaturePKP.Checked = true; txtNrKopjePKP.ClientEnabled = true; }
        //                else this.cbPrintoNrKopjeFaturePKP.Checked = false;
        //                txtNrKopjePKP.Text = vlera.merrVlereOpsioni("NRKOPJESH");
        //                if (vlera.merrVlereOpsioni("NRKOPJESHKTHIMI") != "0")
        //                { this.cbPrintoKopjeTeKthimevePKP.Checked = true; txtNrKopjeKthimeshPKP.ClientEnabled = true; }
        //                else this.cbPrintoKopjeTeKthimevePKP.Checked = false;
        //                this.txtNrKopjeKthimeshPKP.Text = vlera.merrVlereOpsioni("NRKOPJESHKTHIMI");
        //                break;
        //            case "5":
        //                //lblSkedariGEKOS.Text = vlera.merrVlereOpsioni("KASEFISKALEPATH");
        //                ucGEKOS.Text = vlera.merrVlereOpsioni("KASEFISKALEPATH");
        //                this.txtPassOperator.Text = vlera.merrVlereOpsioni("OPERATORPASS");
        //                cmbGjuha.Value = vlera.merrVlereOpsioni("GJUHA");
        //                txtKodiTVSH.Text = vlera.merrVlereOpsioni("NIVELDEFAULTTVSH");
        //                if (vlera.merrVlereOpsioni("NRKOPJESH") != "0")
        //                { this.cbPrintoNrKopjeFatureGEKOS.Checked = true; txtNrKopjeGEKOS.ClientEnabled = true; }
        //                else this.cbPrintoNrKopjeFatureGEKOS.Checked = false;
        //                txtNrKopjeGEKOS.Text = vlera.merrVlereOpsioni("NRKOPJESH");
        //                cbKasaMeShifraDhjetoreGEKOS.Checked = Convert.ToBoolean(vlera.merrVlereOpsioni("MESHIFRADHJETORE"));
        //                cbPrintoNrFatureGEKOS.Checked = Convert.ToBoolean(vlera.merrVlereOpsioni("PRINTONRFATURE"));
        //                break;
        //            case "6":
        //                ucBNTAClass.Text = vlera.merrVlereOpsioni("KASEFISKALEPATH");
        //                txtPortaComBNTAClass.Text = vlera.merrVlereOpsioni("COMPORT");
        //                cbKasaMeShifraDhjetoreBNTAClass.Checked = Convert.ToBoolean(vlera.merrVlereOpsioni("MESHIFRADHJETORE"));
        //                txtBoudRateBNTAClass.Text = vlera.merrVlereOpsioni("BOUDRATE");
        //                break;
        //        }
        //    }
        //}
        #endregion

        private void ruajKonfigurim(int idNdermarrje)
        {
            DbCore.DbAdmin.clsKonfigurimKase konfigurim;

            if (Page.IsValid == false)
                return;
            else
            {
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh(true);
                bool eshteShtim = hfShtimModifikim.Value.ToString() == "shtim" ? true : false;
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), komponente);
                hfKasa.Value = "";
                try
                {
                    konfigurim = krijoKonfigurim(eshteShtim);
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
                    string mesazhi = "Vlerat e dhena nuk jane te sakta!";
                    NLog.LogManager.GetCurrentClassLogger().Error(mesazhi, e.Message);
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhi, pnlMesazhi);
                    hfStatusi.Value = "false";
                    return;
                }

                if (konfigurim == null)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Vlerat e dhena nuk jane te sakta!", pnlMesazhi);
                    hfStatusi.Value = "false";
                    return;
                }
                if (eshteShtim)
                {
                    if (!tedrejtaInfo.DShtim)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                        hfStatusi.Value = "false";
                        return;
                    }
                    if (DbCore.DbAdmin.clsKonfigurimKase.ekzistonKonfigurimPerNdermarrjen(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), txtKodi.Text))
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ekziston nje konfigurimi me kete kod!", pnlMesazhi);
                        hfStatusi.Value = "false";
                        return;
                    }
                    if (konfigurim.OColVlerat != null && konfigurim.OColVlerat.Count > 0)
                        mesazh = konfigurim.ruajKonfigurim(eshteShtim);
                    if (!mesazh.Status)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhGabimi, pnlMesazhi);
                        return;
                    }
                }
                else
                {
                    if (!tedrejtaInfo.DMod)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                        hfStatusi.Value = "false";
                        return;
                    }

                    if (konfigurim.OColVlerat != null && konfigurim.OColVlerat.Count > 0)
                        mesazh = konfigurim.ruajKonfigurim(eshteShtim, Convert.ToInt32(hfId.Value));
                }
                if (!mesazh.Status)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhGabimi, pnlMesazhi);
                    hfStatusi.Value = "false";
                    return;
                }
                else
                {
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["msgRuajtjeMeSuksesGreen"], pnlMesazhi);
                    if (eshteShtim)
                        shtoKonfigurimKaseNeGrid(idNdermarrje, konfigurim.IdKonfigurimi);
                    else //modifikim
                        modifikoKonfigurimKaseNeGrid(idNdermarrje, konfigurim.IdKonfigurimi);
                    hfStatusi.Value = "true";
                    pastroFusha();
                    konfiguroGride();
                    ASPxPageControl1.ActiveTabIndex = 0;
                }
            }
            pnlMesazhi.Update();
        }
        private void pastroFusha()
        {
            txtKodi.Text = "";
            cmbLlojiKases.Value = -1;

        }

        private void shtoKonfigurimKaseNeGrid(int idNdermarrje, int IdKonfigurimi)
        {
            if (gvKasat.DataSource != null)
            {
                DataTable dt = (DataTable)gvKasat.DataSource;
                DataRow[] drs = dt.Select("IdKonfigurimi = " + IdKonfigurimi);
                if (drs.Length > 0)
                    throw new MyException("GABIM: Ky konfigurim ekziston ne gride");
                DataRow newArtDr = DbCore.DbAdmin.colKonfigurimeKase.merrKonfigurimetSipasNdermarjesDR(IdKonfigurimi);
                dt.ImportRow(newArtDr);
            }
            else
            {
                mbushGridKonfigKasashNgaDB(idNdermarrje);
            }
            //gvKasat.DataBind();
            //konfiguroGride();
        }

        private void modifikoKonfigurimKaseNeGrid(int idNdermarrje, int IdKonfigurimi)
        {
            if (gvKasat.DataSource != null)
            {
                DataTable dt = (DataTable)gvKasat.DataSource;
                DataRow[] drs = dt.Select("IdKonfigurimi = " + Convert.ToInt32(hfId.Value));
                if (drs.Length > 1)
                    throw new DbCore.MyException("GABIM: Ndodhen 2 konfigurime kasash me te njejten id ne gride");
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                dt.Rows.Remove(dr);
                DataRow newArtDr = DbCore.DbAdmin.colKonfigurimeKase.merrKonfigurimetSipasNdermarjesDR(IdKonfigurimi);
                dt.ImportRow(newArtDr);
            }
            else
            {
                mbushGridKonfigKasashNgaDB(idNdermarrje);
            }
            gvKasat.DataBind();
        }
        private void hiqKonfigurimKaseNgaGrida(int idNdermarrje, int IdKonfigurimi)
        {
            if (gvKasat.DataSource != null)
            {
                DataTable dt = (DataTable)gvKasat.DataSource;
                DataRow[] drs = dt.Select("IdKonfigurimi = " + IdKonfigurimi);
                if (drs.Length > 1)
                    throw new DbCore.MyException("GABIM: Ndodhen 2 konfigurime me te njejten id ne gride");
                if (drs.Length == 0)
                    return;
                DataRow dr = drs[0];
                dt.Rows.Remove(dr);
                gvKasat.DataBind();
            }
            else
                mbushGridKonfigKasashNgaDB(idNdermarrje);
            gvKasat.DataBind();
        }

        private string merrPluPerLLojinKasesBntElectronis(bool eshteShtim, int llojkaseRe)
        {
            int llojiKaseVjeter;
            string plu = "1";
            if (!eshteShtim)
            {
                DbCore.DbAdmin.clsKonfigurimKase kasaVjeter = new DbCore.DbAdmin.clsKonfigurimKase(Convert.ToInt32(hfId.Value));
                // kasaVjeter.merrKonfiguriminSipasNdermarjes(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                if (kasaVjeter.IdKonfigurimi != 0)
                {
                    llojiKaseVjeter = int.Parse(kasaVjeter.OColVlerat.ktheVlereOpsioni("KASEFISKALELLOJ"));
                }
                else llojiKaseVjeter = -1;
                DbCore.DbAdmin.clsVleraKonfigurimiKasa vlera = new DbCore.DbAdmin.clsVleraKonfigurimiKasa();
                plu = vlera.merrPLUActualNumberPerKase(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), Convert.ToInt32(hfId.Value));
            }
            else
            {
                llojiKaseVjeter = -1;
            }
            if (llojiKaseVjeter != llojkaseRe)
                plu = "1";
            return plu;
        }

        private DbCore.DbAdmin.clsKonfigurimKase krijoKonfigurim(bool eshteShtim)
        {
            if (txtKodi.Text == "")
                throw new MyException("Ju lutemi, plotesoni kodin e konfigurimit te kases!");
            DbCore.DbAdmin.clsKonfigurimKase kasa = new DbCore.DbAdmin.clsKonfigurimKase();
            int llojkaseRe;
            if (cmbLlojiKases.Value.ToString() != "")
                llojkaseRe = Convert.ToInt32(cmbLlojiKases.Value);
            else llojkaseRe = -1;
            switch (llojkaseRe)
            {
                case -1:
                    break;
                case 0:
                    kasa = krijoKonfigurimIva();
                    break;
                case 1:
                    kasa = krijoKonfigurimAed();
                    break;
                case 2:
                    kasa = krijoKonfigurimBtn(merrPluPerLLojinKasesBntElectronis(eshteShtim, llojkaseRe));
                    break;
                case 3:
                    kasa = krijoKonfigurimCkvNoki();
                    break;
                case 4:
                    kasa = krijoKonfigurimPkp();
                    break;
                case 5:
                    kasa = krijoKonfigurimGekos();
                    break;
                case 6:
                    kasa = krijoKonfigurimBNTAClass();
                    break;
                case 7:
                    kasa = krijoKonfigurimBntAclasSkedar();
                    break;
                case 8:
                    kasa = krijoKonfigurimPeshore();
                    break;
            }
            kasa.Kodi = txtKodi.Text;
            kasa.Lloji = int.Parse(cmbLlojObjekti.Value.ToString());
            kasa.Skema = txtSkema.Text;
            if (llojkaseRe != -1)
            {
                merrVleraTakse(kasa);
            }
            return kasa;
        }

        private DbCore.DbAdmin.clsKonfigurimKase krijoKonfigurimIva()
        {
            if (ucIVA.Text == "")
                throw new DbCore.MyException("Ju lutemi shkruani emrin e skedarit te komandave!");
            DbCore.DbAdmin.clsKonfigurimKase kasa = new DbCore.DbAdmin.clsKonfigurimKase();
            kasa.IdNdermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            kasa.Pershkrimi = "IVA" + kasa.IdNdermarje;
            kasa.IdStatusDok = 1;
            kasa.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            kasa.OColVlerat = new DbCore.DbAdmin.colVleratKonfigurimiKasa();

            kasa.OColVlerat.Add(krijoObjektVlera("KASEFISKALELLOJ", "0", 0));
            kasa.OColVlerat.Add(krijoObjektVlera("KASEFISKALELOGICALNR", "1", 0));
            kasa.OColVlerat.Add(krijoObjektVlera("KASEFISKALEPATH", ucIVA.Text, 0));
            kasa.OColVlerat.Add(krijoObjektVlera("MESHIFRADHJETORE", cbKasaMeShifraDhjetore.Checked.ToString(), 0));
            kasa.OColVlerat.Add(krijoObjektVlera("KASEAPOPRINTER", rbKase.Checked.ToString(), 0));
            kasa.OColVlerat.Add(krijoObjektVlera("PRINTONRFATURE", cbPrintoNrFature.Checked.ToString(), 0));
            if (cmbMonedha.Value != null && cmbMonedha.Value.ToString() != "")
                kasa.OColVlerat.Add(krijoObjektVlera("CMIMMONEDHEDYTE", cmbMonedha.Value.ToString(), 0));
            else
                kasa.OColVlerat.Add(krijoObjektVlera("CMIMMONEDHEDYTE", "0", 0));
            kasa.OColVlerat.Add(krijoObjektVlera("KUPONTATIMOR", this.rbKuponTatimor.Checked.ToString(), 0));
            kasa.OColVlerat.Add(krijoObjektVlera("NRKOPJESH", cbPrintoNrKopjeFature.Checked.ToString(), 0));
            kasa.OColVlerat.Add(krijoObjektVlera("NRKOPJESHKTHIMI", txtNrKopjeKthimesh.Text, 0));
            kasa.OColVlerat.Add(krijoObjektVlera("PRINTONESERVER", cbPrintoNeServerIVA.Checked.ToString(), 0));
            kasa.OColVlerat.Add(krijoObjektVlera("PRINTOMANUALISHTNGAKASA", cbPrintoManualishtNgaKasa.Checked.ToString(), 0));
            kasa.OColVlerat.Add(krijoObjektVlera("URL", txtUrl.Text, 0));
            kasa.OColVlerat.Add(krijoObjektVlera("PRINTOPERSHKRIM2", cbPrintoPershkrim2IVA.Checked.ToString(), 0));
            kasa.OColVlerat.Add(krijoObjektVlera("IPKASE", txtIPKase.Text, 0));
            return kasa;
        }


        private DbCore.DbAdmin.clsKonfigurimKase krijoKonfigurimAed()
        {
            if (ucAED.Text == "")
                throw new DbCore.MyException("Ju lutemi shkruani emrin e skedarit te komandave!");
            
            DbCore.DbAdmin.clsKonfigurimKase kasa = new DbCore.DbAdmin.clsKonfigurimKase();
            kasa.IdNdermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            kasa.Pershkrimi = "AED" + kasa.IdNdermarje;
            kasa.OColVlerat = new DbCore.DbAdmin.colVleratKonfigurimiKasa();
            kasa.IdStatusDok = 1;
            kasa.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            kasa.OColVlerat.Add(krijoObjektVlera("KASEFISKALELLOJ", "1", 0));
            kasa.OColVlerat.Add(krijoObjektVlera("KASEFISKALELOGICALNR", txtPorta.Text, 0));
            //if (ucAED.PostedFile.FileName == "")
            //    kasa.OColVlerat.Add(krijoObjektVlera("KASEFISKALEPATH", hfFile.Value.ToString(), 0));
            //else kasa.OColVlerat.Add(krijoObjektVlera("KASEFISKALEPATH", ucAED.PostedFile.FileName, 0));            
            kasa.OColVlerat.Add(krijoObjektVlera("KASEFISKALEPATH", ucAED.Text, 0));
            kasa.OColVlerat.Add(krijoObjektVlera("PORT", txtPorta.Text, 0));
            if (cbMbyllCdoFature.Checked)
                kasa.OColVlerat.Add(krijoObjektVlera("CHIUS", "1", 0));
            else kasa.OColVlerat.Add(krijoObjektVlera("CHIUS", "0", 0));
            kasa.OColVlerat.Add(krijoObjektVlera("PRINTBARKOD", this.cbPrintoBarkod.Checked.ToString(), 0));
            kasa.OColVlerat.Add(krijoObjektVlera("NRKOPJESH", cbPrintoNrKopjeFatureAED.Checked.ToString(), 0));
            kasa.OColVlerat.Add(krijoObjektVlera("RUAJKOPJE", this.cbRuajKopje.Checked.ToString(), 0));
            kasa.OColVlerat.Add(krijoObjektVlera("MESHIFRADHJETORE", this.cbKasaMeShifraDhjetoreAED.Checked.ToString(), 0));
            kasa.OColVlerat.Add(krijoObjektVlera("PRINTONESERVER", cbPrintoNeServerAED.Checked.ToString(), 0));
            //kasa.OColVlerat.Add(krijoObjektVlera("PLUACTUALNUMBER", "1", 0));
            kasa.OColVlerat.Add(krijoObjektVlera("URL", txtUrl.Text, 0));
            kasa.OColVlerat.Add(krijoObjektVlera("printoKodArtikulli", this.cbPrintoKodArtikulli.Checked.ToString(), 0));
            kasa.OColVlerat.Add(krijoObjektVlera("PRINTOPERSHKRIM2", cbPrintoPershkrim2AED.Checked.ToString(), 0));
            kasa.OColVlerat.Add(krijoObjektVlera("IPKASE", txtIPKase.Text, 0));
            return kasa;
        }

        private DbCore.DbAdmin.clsKonfigurimKase krijoKonfigurimBtn(string plu)
        {
            if (ucBTN.Text == "")
                throw new DbCore.MyException("Ju lutemi shkruani emrin e skedarit te komandave!");
            DbCore.DbAdmin.clsKonfigurimKase kasa = new DbCore.DbAdmin.clsKonfigurimKase();
            kasa.IdNdermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            kasa.Pershkrimi = "BTN" + kasa.IdNdermarje;
            kasa.OColVlerat = new DbCore.DbAdmin.colVleratKonfigurimiKasa();
            kasa.IdStatusDok = 1;
            kasa.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            kasa.OColVlerat.Add(krijoObjektVlera("KASEFISKALELLOJ", "2", 0));
            kasa.OColVlerat.Add(krijoObjektVlera("KASEFISKALELOGICALNR", "1", 0));
            kasa.OColVlerat.Add(krijoObjektVlera("KASEFISKALEPATH", ucBTN.Text, 0));
            kasa.OColVlerat.Add(krijoObjektVlera("MESHIFRADHJETORE", cbKasaMeShifraDhjetoreBTN.Checked.ToString(), 0));
            kasa.OColVlerat.Add(krijoObjektVlera("KASEAPOPRINTER", rbKaseBTN.Checked.ToString(), 0));
            kasa.OColVlerat.Add(krijoObjektVlera("PRINTONRFATURE", cbPrintoNrFatureBTN.Checked.ToString(), 0));
            if (cmbMonedhaBTN.Value != null && cmbMonedhaBTN.Value.ToString() != "")
                kasa.OColVlerat.Add(krijoObjektVlera("CMIMMONEDHEDYTE", cmbMonedhaBTN.Value.ToString(), 0));
            else
                kasa.OColVlerat.Add(krijoObjektVlera("CMIMMONEDHEDYTE", "0", 0));
            kasa.OColVlerat.Add(krijoObjektVlera("KUPONTATIMOR", this.rbKuponTatimorBTN.Checked.ToString(), 0));
            kasa.OColVlerat.Add(krijoObjektVlera("NRKOPJESH", cbPrintoNrKopjeFatureBTN.Checked.ToString(), 0));
            kasa.OColVlerat.Add(krijoObjektVlera("NRKOPJESHKTHIMI", txtNrKopjeKthimeshBTN.Text, 0));
            kasa.OColVlerat.Add(krijoObjektVlera("PRINTONESERVER", cbPrintoNeServerBTN.Checked.ToString(), 0));
            kasa.OColVlerat.Add(krijoObjektVlera("PLUACTUALNUMBER", plu, 0));
            kasa.OColVlerat.Add(krijoObjektVlera("URL", txtUrl.Text, 0));
            kasa.OColVlerat.Add(krijoObjektVlera("PRINTOPERSHKRIM2", cbPrintoPershkrim2BTN.Checked.ToString(), 0));
            kasa.OColVlerat.Add(krijoObjektVlera("IPKASE", txtIPKase.Text, 0));
            return kasa;
        }

        private DbCore.DbAdmin.clsKonfigurimKase krijoKonfigurimCkvNoki()
        {
            DbCore.DbAdmin.clsKonfigurimKase kasa = new DbCore.DbAdmin.clsKonfigurimKase();
            kasa.IdNdermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            kasa.Pershkrimi = "CKVNOKI" + kasa.IdNdermarje;
            kasa.OColVlerat = new DbCore.DbAdmin.colVleratKonfigurimiKasa();
            kasa.IdStatusDok = 1;
            kasa.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            kasa.OColVlerat.Add(krijoObjektVlera("KASEFISKALELLOJ", "3", 0));
            kasa.OColVlerat.Add(krijoObjektVlera("KASEFISKALELOGICALNR", "1", 0));
            kasa.OColVlerat.Add(krijoObjektVlera("KASEFISKALEPATH", ucCKV.Text, 0));
            kasa.OColVlerat.Add(krijoObjektVlera("IPCOM", "0", 0));
            kasa.OColVlerat.Add(krijoObjektVlera("COMPORT", this.txtPortaCom.Text, 0));
            kasa.OColVlerat.Add(krijoObjektVlera("MESHIFRADHJETORE", cbKasaMeShifraDhjetoreCKVNOKI.Checked.ToString(), 0));
            kasa.OColVlerat.Add(krijoObjektVlera("BOUDRATE", this.txtBoudRate.Text, 0));
            kasa.OColVlerat.Add(krijoObjektVlera("PRINTONRFATURE", cbPrintoNrFatureCKVNOKI.Checked.ToString(), 0));
            kasa.OColVlerat.Add(krijoObjektVlera("PRINTONESERVER", cbPrintoNeServerCKV.Checked.ToString(), 0));
            kasa.OColVlerat.Add(krijoObjektVlera("MESKEDAR", cbMeSkedar.Checked.ToString(), 0));
            kasa.OColVlerat.Add(krijoObjektVlera("URL", txtUrl.Text, 0));
            kasa.OColVlerat.Add(krijoObjektVlera("PRINTOPERSHKRIM2", cbPrintoPershkrim2CKV.Checked.ToString(), 0));
            kasa.OColVlerat.Add(krijoObjektVlera("IPKASE", txtIPKase.Text, 0));
            return kasa;
        }

        private DbCore.DbAdmin.clsKonfigurimKase krijoKonfigurimPkp()
        {
            if (ucPKP.Text == "")
                throw new DbCore.MyException("Ju lutemi shkruani emrin e skedarit te komandave!");
            
            DbCore.DbAdmin.clsKonfigurimKase kasa = new DbCore.DbAdmin.clsKonfigurimKase();
            kasa.IdNdermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            kasa.Pershkrimi = "PKP" + kasa.IdNdermarje;
            kasa.OColVlerat = new DbCore.DbAdmin.colVleratKonfigurimiKasa();
            kasa.IdStatusDok = 1;
            kasa.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            kasa.OColVlerat.Add(krijoObjektVlera("KASEFISKALELLOJ", "4", 0));
            kasa.OColVlerat.Add(krijoObjektVlera("KASEFISKALELOGICALNR", "1", 0));
            kasa.OColVlerat.Add(krijoObjektVlera("KASEFISKALEPATH", ucPKP.Text, 0));
            kasa.OColVlerat.Add(krijoObjektVlera("MESHIFRADHJETORE", cbKasaMeShifraDhjetorePKP.Checked.ToString(), 0));
            kasa.OColVlerat.Add(krijoObjektVlera("PRINTONRFATURE", cbPrintoNrFaturePKP.Checked.ToString(), 0));
            if (cmbMonedhaPKP.Value != null && cmbMonedhaPKP.Value.ToString() != "")
                kasa.OColVlerat.Add(krijoObjektVlera("CMIMMONEDHEDYTE", cmbMonedhaPKP.Value.ToString(), 0));
            else
                kasa.OColVlerat.Add(krijoObjektVlera("CMIMMONEDHEDYTE", "0", 0));
            kasa.OColVlerat.Add(krijoObjektVlera("NRKOPJESH", cbPrintoNrKopjeFaturePKP.Checked.ToString(), 0));
            kasa.OColVlerat.Add(krijoObjektVlera("NRKOPJESHKTHIMI", txtNrKopjeKthimeshPKP.Text, 0));
            kasa.OColVlerat.Add(krijoObjektVlera("PRINTONESERVER", cbPrintoNeServerPKP.Checked.ToString(), 0));
            kasa.OColVlerat.Add(krijoObjektVlera("URL", txtUrl.Text, 0));
            kasa.OColVlerat.Add(krijoObjektVlera("PRINTOPERSHKRIM2", cbPrintoPershkrim2PKP.Checked.ToString(), 0));
            kasa.OColVlerat.Add(krijoObjektVlera("IPKASE", txtIPKase.Text, 0));
            return kasa;
        }

        private DbCore.DbAdmin.clsKonfigurimKase krijoKonfigurimPeshore()
        {
            if (ucPeshore.Text == "")
            {
                throw new DbCore.MyException("Ju lutemi shkruani emrin e skedarit te komandave!");
            }
            DbCore.DbAdmin.clsKonfigurimKase kasa = new DbCore.DbAdmin.clsKonfigurimKase();
            kasa.IdNdermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            kasa.Pershkrimi = "Peshore" + kasa.IdNdermarje;
            kasa.OColVlerat = new DbCore.DbAdmin.colVleratKonfigurimiKasa();
            kasa.IdStatusDok = 1;
            kasa.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            kasa.OColVlerat.Add(krijoObjektVlera("KASEFISKALELLOJ", "8", 0));
            kasa.OColVlerat.Add(krijoObjektVlera("KASEFISKALEPATH", ucPeshore.Text, 0));
            kasa.OColVlerat.Add(krijoObjektVlera("URL", txtUrl.Text, 0));
            return kasa;
        }

        private DbCore.DbAdmin.clsKonfigurimKase krijoKonfigurimGekos()
        {
            if (ucGEKOS.Text == "")
                throw new DbCore.MyException("Ju lutemi shkruani emrin e skedarit te komandave!");
            DbCore.DbAdmin.clsKonfigurimKase kasa = new DbCore.DbAdmin.clsKonfigurimKase();
            kasa.IdNdermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            kasa.Pershkrimi = "GEKOS" + kasa.IdNdermarje;
            kasa.OColVlerat = new DbCore.DbAdmin.colVleratKonfigurimiKasa();
            kasa.IdStatusDok = 1;
            kasa.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            kasa.OColVlerat.Add(krijoObjektVlera("KASEFISKALELLOJ", "5", 0));
            kasa.OColVlerat.Add(krijoObjektVlera("KASEFISKALELOGICALNR", "1", 0));
            kasa.OColVlerat.Add(krijoObjektVlera("KASEFISKALEPATH", ucGEKOS.Text, 0));
            kasa.OColVlerat.Add(krijoObjektVlera("OPERATORPASS", this.txtPassOperator.Text, 0));
            kasa.OColVlerat.Add(krijoObjektVlera("GJUHA", this.cmbGjuha.Value.ToString(), 0));
            kasa.OColVlerat.Add(krijoObjektVlera("VLERAPAGUAR", "0", 0));
            kasa.OColVlerat.Add(krijoObjektVlera("NRKOPJESH", this.cbPrintoNrKopjeFatureGEKOS.Checked.ToString(), 0));
            kasa.OColVlerat.Add(krijoObjektVlera("SHIFRAPASPRESJE", "2", 0));
            kasa.OColVlerat.Add(krijoObjektVlera("NIVELDEFAULTTVSH", this.txtKodiTVSH.Text, 0));
            kasa.OColVlerat.Add(krijoObjektVlera("MESHIFRADHJETORE", cbKasaMeShifraDhjetoreGEKOS.Checked.ToString(), 0));
            kasa.OColVlerat.Add(krijoObjektVlera("PRINTONRFATURE", cbPrintoNrFatureGEKOS.Checked.ToString(), 0));
            kasa.OColVlerat.Add(krijoObjektVlera("PRINTONESERVER", cbPrintoNeServerGEKOS.Checked.ToString(), 0));
            kasa.OColVlerat.Add(krijoObjektVlera("URL", txtUrl.Text, 0));
            kasa.OColVlerat.Add(krijoObjektVlera("PRINTOPERSHKRIM2", cbPrintoPershkrim2GEKOS.Checked.ToString(), 0));
            kasa.OColVlerat.Add(krijoObjektVlera("IPKASE", txtIPKase.Text, 0));
            return kasa;
        }

        private DbCore.DbAdmin.clsKonfigurimKase krijoKonfigurimBNTAClass()
        {
            DbCore.DbAdmin.clsKonfigurimKase kasa = new DbCore.DbAdmin.clsKonfigurimKase();
            kasa.IdNdermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            kasa.Pershkrimi = "BNTAClass" + kasa.IdNdermarje;
            kasa.OColVlerat = new DbCore.DbAdmin.colVleratKonfigurimiKasa();
            kasa.IdStatusDok = 1;
            kasa.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            kasa.OColVlerat.Add(krijoObjektVlera("KASEFISKALELLOJ", "6", 0));
            kasa.OColVlerat.Add(krijoObjektVlera("KASEFISKALELOGICALNR", "1", 0));
            kasa.OColVlerat.Add(krijoObjektVlera("COMPORT", txtPortaComBNTAClass.Text, 0));
            kasa.OColVlerat.Add(krijoObjektVlera("MESHIFRADHJETORE", cbKasaMeShifraDhjetoreBNTAClass.Checked.ToString(), 0));
            kasa.OColVlerat.Add(krijoObjektVlera("BOUDRATE", txtBoudRateBNTAClass.Text, 0));
            kasa.OColVlerat.Add(krijoObjektVlera("PRINTONESERVER", cbPrintoNeServerBNTAclass.Checked.ToString(), 0));
            kasa.OColVlerat.Add(krijoObjektVlera("URL", txtUrl.Text, 0));
            kasa.OColVlerat.Add(krijoObjektVlera("PRINTOPERSHKRIM2", cbPrintoPershkrim2BNTAclass.Checked.ToString(), 0));
            kasa.OColVlerat.Add(krijoObjektVlera("IPKASE", txtIPKase.Text, 0));
            return kasa;
        }
        private DbCore.DbAdmin.clsKonfigurimKase krijoKonfigurimBntAclasSkedar()
        {
            if (ucIVA.Text == "")
                throw new DbCore.MyException("Ju lutemi shkruani emrin e skedarit te komandave!");
            DbCore.DbAdmin.clsKonfigurimKase kasa = new DbCore.DbAdmin.clsKonfigurimKase();
            kasa.IdNdermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            kasa.Pershkrimi = "BNTAClassSkedar" + kasa.IdNdermarje;
            kasa.IdStatusDok = 1;
            kasa.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            kasa.OColVlerat = new DbCore.DbAdmin.colVleratKonfigurimiKasa();

            kasa.OColVlerat.Add(krijoObjektVlera("KASEFISKALELLOJ", "7", 0));
            kasa.OColVlerat.Add(krijoObjektVlera("KASEFISKALELOGICALNR", "1", 0));
            kasa.OColVlerat.Add(krijoObjektVlera("KASEFISKALEPATH", ucIVA.Text, 0));
            kasa.OColVlerat.Add(krijoObjektVlera("MESHIFRADHJETORE", cbKasaMeShifraDhjetore.Checked.ToString(), 0));
            kasa.OColVlerat.Add(krijoObjektVlera("KASEAPOPRINTER", rbKase.Checked.ToString(), 0));
            kasa.OColVlerat.Add(krijoObjektVlera("PRINTONRFATURE", cbPrintoNrFature.Checked.ToString(), 0));
            if (cmbMonedha.Value != null && cmbMonedha.Value.ToString() != "")
                kasa.OColVlerat.Add(krijoObjektVlera("CMIMMONEDHEDYTE", cmbMonedha.Value.ToString(), 0));
            else
                kasa.OColVlerat.Add(krijoObjektVlera("CMIMMONEDHEDYTE", "0", 0));
            kasa.OColVlerat.Add(krijoObjektVlera("KUPONTATIMOR", this.rbKuponTatimor.Checked.ToString(), 0));
            kasa.OColVlerat.Add(krijoObjektVlera("NRKOPJESH", cbPrintoNrKopjeFature.Checked.ToString(), 0));
            kasa.OColVlerat.Add(krijoObjektVlera("NRKOPJESHKTHIMI", txtNrKopjeKthimesh.Text, 0));
            kasa.OColVlerat.Add(krijoObjektVlera("PRINTONESERVER", cbPrintoNeServerIVA.Checked.ToString(), 0));
            kasa.OColVlerat.Add(krijoObjektVlera("PRINTOMANUALISHTNGAKASA", cbPrintoManualishtNgaKasa.Checked.ToString(), 0));
            kasa.OColVlerat.Add(krijoObjektVlera("URL", txtUrl.Text, 0));
            kasa.OColVlerat.Add(krijoObjektVlera("PRINTOPERSHKRIM2", cbPrintoPershkrim2BNTAclass.Checked.ToString(), 0));
            kasa.OColVlerat.Add(krijoObjektVlera("IPKASE", txtIPKase.Text, 0));
            return kasa;
        }

        private void shtoVleraTakse(DbCore.DbAdmin.clsKonfigurimKase kasa)
        {
            string[] separator = { "],[" };
            string[] rreshtat = hfVlerat.Value.ToString().Split(separator, gvNiveleTVSHIVA.VisibleRowCount, System.StringSplitOptions.None);
            for (int i = 0; i < rreshtat.Length; i++)
            {
                string rreshti = rreshtat[i].Replace("\"", "");
                DbCore.DbRegjistrim.clsTaksa taksa = new DbCore.DbRegjistrim.clsTaksa(rreshti.Split(',')[1], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                kasa.OColVlerat.Add(krijoObjektVlera(taksa.NormaPerqindje.ToString(), rreshti.Split(',')[2].Split(']')[0], taksa.IdTaksa));
            }
        }

        private void merrVleraTakse(DbCore.DbAdmin.clsKonfigurimKase kasa)
        {
            string[] separator = { "],[" };
            string[] rreshtat = hfVlerat.Value.ToString().Split(separator, gvNiveleTVSHIVA.VisibleRowCount, System.StringSplitOptions.None);
            for (int i = 0; i < rreshtat.Length; i++)
            {
                string rreshti = rreshtat[i].Replace("\"", "");
                DbCore.DbRegjistrim.clsTaksa taksa = new DbCore.DbRegjistrim.clsTaksa(rreshti.Split(',')[1], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                kasa.OColVlerat.Add(krijoObjektVlera(taksa.NormaPerqindje.ToString(), rreshti.Split(',')[2].Split(']')[0], taksa.IdTaksa));
            }
        }

        private DbCore.DbAdmin.clsVleraKonfigurimiKasa krijoObjektVlera(string pershkrimi, string vler, int idtaksa)
        {
            DbCore.DbAdmin.clsVleraKonfigurimiKasa vlera = new DbCore.DbAdmin.clsVleraKonfigurimiKasa();
            vlera.IdOpsioni = vlera.merrIdOpsioni(pershkrimi);
            vlera.Vlera = vler;
            vlera.IdTaksa = idtaksa;
            return vlera;
        }

        protected void konfiguroVleraFillestare(int idNdermarrje)
        {
            //mbushComboLlojKase();
            mbushcmbLlojMbyllje();
            mbushcmbLlojObjekti();
            ConfigureAspxComboBox.mbushComboMonedha(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje, true, cmbMonedha, cmbMonedhaPKP, cmbMonedhaBTN);
            //DbCore.clsFunksione.mbushComboMonedha(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje, cmbMonedhaCKVNOKI, true);
            //ConfigureAspxComboBox.mbushComboMonedha(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje, cmbMonedhaPKP, true);
            //ConfigureAspxComboBox.mbushComboMonedha(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje, cmbMonedhaBTN, true);
            mbushComboGjuha();
            mbushGride(-1);
            percaktoTemplate();
            konfiguroNivelTVSH();
        }

        private void percaktoTemplate()
        {
            GridViewDataColumn colKodi = gvNiveleTVSHIVA.Columns["KodTaksa"] as GridViewDataColumn;
            colKodi.DataItemTemplate = new MyLabelTemplate();
            GridViewDataTextColumn col2 = gvNiveleTVSHIVA.Columns["LlogariDebi"] as GridViewDataTextColumn;
            col2.DataItemTemplate = new MyTextTemplate();
        }

        private void konfiguroNivelTVSH()
        {//konfiguron griden
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            GridUtil.percaktoVisibleColumnsMeWidth(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), gvNiveleTVSHIVA, "gvNiveleTVSHIVA", komponente);
            GridUtil.konfiguroGrideRegjistrimEvogelPaTheme(gvNiveleTVSHIVA, "IdTaksa", false);
        }

        protected void gvNiveleTVSHIVA_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        { //sherben per te marre me vone vlerat e futura nga perdoruesi me ane te javascriptit

            if (e.RowType == GridViewRowType.Data)
            {
                GridViewDataColumn colKasa = ((ASPxGridView)sender).Columns["LlogariDebi"] as GridViewDataColumn;
                ASPxTextBox txtKasa = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, colKasa, "txtBox") as ASPxTextBox;
                GridViewDataColumn colKodi = ((ASPxGridView)sender).Columns["KodTaksa"] as GridViewDataColumn;
                ASPxLabel lblKodi = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, colKodi, "lbl") as ASPxLabel;
                if (txtKasa != null)
                {
                    lblKodi.ClientInstanceName = "lblKodi" + e.VisibleIndex.ToString();
                    txtKasa.ClientInstanceName = "txtKasa" + e.VisibleIndex.ToString();
                }
            }
        }

        private void mbushGride(int idKonfigurimi)
        {
            DbCore.DbRegjistrim.colTaksa taksa = new DbCore.DbRegjistrim.colTaksa(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.DbRegjistrim.LlojTakse.Nivel_Tvsh, DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            foreach (DbCore.DbRegjistrim.clsTaksa t in taksa)
            {
                DbCore.DbAdmin.clsVleraKonfigurimiKasa vlera = new DbCore.DbAdmin.clsVleraKonfigurimiKasa();
                vlera.merrVleraSipasIdTaksaJoFshire(t.IdTaksa, idKonfigurimi);
                t.LlogariDebi = vlera.Vlera;
            }
            gvNiveleTVSHIVA.DataSource = taksa;
            gvNiveleTVSHIVA.DataBind();
        }


        private void mbushcmbLlojMbyllje()
        {
            cmbLlojMbyllje.Items.Add("Gjate", 0);
            cmbLlojMbyllje.Items.Add("Shkurter", 1);
            cmbLlojMbyllje.Items.Add("Mesatar", 2);
            cmbLlojMbyllje.SelectedIndex = -1;
        }

        private void mbushcmbLlojObjekti()
        {
            cmbLlojObjekti.Items.Add("Kase", 1);
            cmbLlojObjekti.Items.Add("Peshore", 2);
            cmbLlojObjekti.SelectedIndex = 0;
        }

        private void mbushComboGjuha()
        {
            cmbGjuha.Items.Add("Anglisht", 0);
            cmbGjuha.Items.Add("Shqip", 1);
            cmbGjuha.Items.Add("Serbisht", 2);
            cmbGjuha.SelectedIndex = 1;
        }

        protected void gvNiveleTVSHIVA_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 1)
            {
                // mbushGride(Convert.ToInt32(arr[0]));
                mbushGride(Converter.ConvertToInt(arr[0]));
                percaktoTemplate();
                konfiguroNivelTVSH();
            }
        }

        protected void gvNiveleTVSHIVA_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpNoRows"] = gvNiveleTVSHIVA.VisibleRowCount;
        }

        //protected void btnRaportiX_Click(object sender, EventArgs e)
        //      {
        //      hfKasa.Value = "";
        //      string kf_file;
        //      int kf_lloj;
        //      int kaseLogicalNumber;
        //      DbCore.DbAdmin.clsVleraKonfigurimiKasa vlera = new DbCore.DbAdmin.clsVleraKonfigurimiKasa();
        //      kf_lloj = int.Parse(cmbLlojiKases.Value.ToString());
        //      if (kf_lloj < 0) return;
        //      kf_file = vlera.merrVlereOpsioni("KASEFISKALEPATH");

        //      if (kf_file == "") return;
        //      string text = kf_file + "&&";

        //      kaseLogicalNumber = int.Parse(vlera.merrVlereOpsioni("KASEFISKALELOGICALNR"));
        //      switch (kf_lloj)
        //          {
        //          case 1:
        //              //frmKasaMbyllGjendje.Show;
        //              break;
        //          case 2:
        //              //StreamWriter sw = File.CreateText(kf_file);
        //              if (vlera.merrVlereOpsioni("KASEAPOPRINTER") == "True")  //kase
        //                  {     clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Funksioni i kerkuar nuk eshte i mundur per kasen tuaj!", pnlMesazhi); percaktoTemplate();  return;
        //                  }
        //              else text+="X," + kaseLogicalNumber + ",_______,_,__;";//  sw.WriteLine("X," + kaseLogicalNumber + ",_______,_,__;");
        //            //  sw.Close();
        //              break;
        //          case 5:
        //            //  StreamWriter swg = File.CreateText(kf_file);
        //           text+= "X," + kaseLogicalNumber + ",_______,_,__;";//  swg.WriteLine("X," + kaseLogicalNumber + ",_______,_,__;");
        //            //  swg.Close();
        //              break;
        //          case 0:
        //             // StreamWriter swi = File.CreateText(kf_file);
        //              if (vlera.merrVlereOpsioni("KASEAPOPRINTER") == "True")  //kase
        //                  { clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Funksioni i kerkuar nuk eshte i mundur per kasen tuaj!", pnlMesazhi); percaktoTemplate();  return;
        //                  }
        //              else
        //               text+="69," + kaseLogicalNumber + ",______,_,__;1;" ;//  swi.WriteLine("69," + kaseLogicalNumber + ",______,_,__;1;");
        //              //swi.Close();
        //              break;
        //          default:
        //              clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Funksioni i kerkuar nuk eshte i mundur per kasen tuaj!", pnlMesazhi);    percaktoTemplate();
        //              return;
        //              break;

        //          }
        //      hfKasa.Value = text;
        //      percaktoTemplate();
        //      }
        protected void btnRaportiX_Click(object sender, EventArgs e)
        {
            if (hfShtimModifikim.Value == "modifikim")
            {
                DbCore.DbAdmin.clsKonfigurimKase kase = new clsKonfigurimKase(Convert.ToInt32(hfId.Value));
                //kase.merrKonfiguriminSipasNdermarjes(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                hfKasa.Value = "";
                string kf_file;
                int kf_lloj;
                int kaseLogicalNumber;
                //DbCore.DbAdmin.clsVleraKonfigurimiKasa vlera = new DbCore.DbAdmin.clsVleraKonfigurimiKasa();
                kf_lloj = int.Parse(cmbLlojiKases.Value.ToString());
                if (kf_lloj < 0) return;
                //kf_file = vlera.merrVlereOpsioni("KASEFISKALEPATH");
                kf_file = kase.OColVlerat.ktheVlereOpsioni("KASEFISKALEPATH");

                if (kf_file == "") return;
                string text = kf_file + "&&";

                //kaseLogicalNumber = int.Parse(vlera.merrVlereOpsioni("KASEFISKALELOGICALNR"));
                kaseLogicalNumber = int.Parse(kase.OColVlerat.ktheVlereOpsioni("KASEFISKALELOGICALNR"));
                switch (kf_lloj)
                {
                    case 2:
                        //StreamWriter sw = File.CreateText(kf_file);
                        if (kase.OColVlerat.ktheVlereOpsioni("KASEAPOPRINTER") == "True")  //kaseapoprinter = true dmth qe eshte kase, false eshte printer
                        {
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Funksioni i kerkuar nuk eshte i mundur per kasen tuaj!", pnlMesazhi);
                            percaktoTemplate();
                            return;
                        }
                        else text += "X," + kaseLogicalNumber + ",_______,_,__;";//  sw.WriteLine("X," + kaseLogicalNumber + ",_______,_,__;");
                        //  sw.Close();
                        break;
                    case 5:
                        //  StreamWriter swg = File.CreateText(kf_file);
                        text += "X," + kaseLogicalNumber + ",_______,_,__;";//  swg.WriteLine("X," + kaseLogicalNumber + ",_______,_,__;");
                        //  swg.Close();
                        break;
                    case 0:
                    case 7:
                        // StreamWriter swi = File.CreateText(kf_file);
                        if (kase.OColVlerat.ktheVlereOpsioni("KASEAPOPRINTER") == "True")  //kase
                        {
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Funksioni i kerkuar nuk eshte i mundur per kasen tuaj!", pnlMesazhi);
                            percaktoTemplate();
                            return;
                        }
                        else text += "69," + kaseLogicalNumber + ",______,_,__;1;";//  swi.WriteLine("69," + kaseLogicalNumber + ",______,_,__;1;");
                        //swi.Close();
                        break;
                    default:
                        //clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Funksioni i kerkuar nuk eshte i mundur per kasen tuaj!", pnlMesazhi); percaktoTemplate();
                        return;
                        //break;
                }
                hfKasa.Value = text;
                percaktoTemplate();
            }
        }


        //protected void btnRaportiZ_Click(object sender, EventArgs e)
        //    {
        //    hfKasa.Value = "";
        //    string kf_file;
        //    int kf_lloj;
        //    int kaseLogicalNumber;

        //    DbCore.DbAdmin.clsVleraKonfigurimiKasa vlera = new DbCore.DbAdmin.clsVleraKonfigurimiKasa();
        //    kf_lloj = int.Parse(cmbLlojiKases.Value.ToString());
        //    if (kf_lloj < 0) return;
        //    kf_file = vlera.merrVlereOpsioni("KASEFISKALEPATH");

        //    if (kf_file == "") return;
        //    string text = kf_file + "&&";
        //    kaseLogicalNumber = int.Parse(vlera.merrVlereOpsioni("KASEFISKALELOGICALNR"));
        //    switch (kf_lloj)
        //        {
        //        case 1:
        //            //frmKasaMbyllGjendje.Show;
        //            break;
        //        case 2:
        //          //  StreamWriter sw = File.CreateText(kf_file);
        //            if (vlera.merrVlereOpsioni("KASEAPOPRINTER") == "True")  //kase
        //                { clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Funksioni i kerkuar nuk eshte i mundur per kasen tuaj!", pnlMesazhi);        percaktoTemplate();    return;}
        //            else text+="Z," + kaseLogicalNumber + ",_______,_,__;"; // sw.WriteLine("Z," + kaseLogicalNumber + ",_______,_,__;");

        //          //  sw.Close();
        //            break;
        //        case 5:
        //           // StreamWriter swg = File.CreateText(kf_file);
        //          text+="E," + kaseLogicalNumber + ",______,_,__;Printo Z raportin;F-Link ks;||";//  swg.WriteLine("E," + kaseLogicalNumber + ",______,_,__;Printo Z raportin;F-Link ks;");
        //        text+="Z," + kaseLogicalNumber + ",______,_,__;||";//    swg.WriteLine("Z," + kaseLogicalNumber + ",______,_,__;");
        //            DbCore.DbAdmin.clsNdermarrje nderm = new DbCore.DbAdmin.clsNdermarrje(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //        text+= "E," + kaseLogicalNumber + ",______,_,__;" + nderm.NdermarrjePershkrimi;//   swg.WriteLine("E," + kaseLogicalNumber + ",______,_,__;" + nderm.NdermarrjePershkrimi);

        //          //  swg.Close();
        //            break;
        //        case 0:
        //          //  StreamWriter swi = File.CreateText(kf_file);
        //            if (vlera.merrVlereOpsioni("KASEAPOPRINTER") == "True")  //kase
        //                text += "Z," + kaseLogicalNumber + ",______,_,__;";//   swi.WriteLine("Z," + kaseLogicalNumber + ",______,_,__;");
        //            else
        //               text+="69," + kaseLogicalNumber + ",______,_,__;0;";// swi.WriteLine("69," + kaseLogicalNumber + ",______,_,__;0;");
        //          //  swi.Close();
        //            break;
        //        default:
        //            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Funksioni i kerkuar nuk eshte i mundur per kasen tuaj!", pnlMesazhi);              percaktoTemplate();
        //            return;
        //            break;

        //        }
        //    hfKasa.Value = text;
        //    percaktoTemplate();
        //    }

        protected void btnRaportiZ_Click(object sender, EventArgs e)
        {
            if (hfShtimModifikim.Value == "modifikim")
            {
                hfKasa.Value = "";
                string kf_file;
                int kf_lloj;
                int kaseLogicalNumber;
                DbCore.DbAdmin.clsKonfigurimKase kase = new clsKonfigurimKase(Convert.ToInt32(hfId.Value));
                kf_lloj = int.Parse(cmbLlojiKases.Value.ToString());
                if (kf_lloj < 0) return;
                //kf_file = vlera.merrVlereOpsioni("KASEFISKALEPATH");
                kf_file = kase.OColVlerat.ktheVlereOpsioni("KASEFISKALEPATH");
                if (kf_lloj == 2)
                {
                    if (kf_file.LastIndexOf("\\") != 0)
                    {
                        kf_file = kf_file.Substring(0, kf_file.LastIndexOf("\\") + 1) + "cmdDR.txt";
                    }
                    else if (kf_file.LastIndexOf("/") != 0)
                    {
                        kf_file = kf_file.Substring(0, kf_file.LastIndexOf("/") + 1) + "pludel.txt";
                    }
                }
                if (kf_file == "") return;
                string text = kf_file + "&&";
                //kaseLogicalNumber = int.Parse(vlera.merrVlereOpsioni("KASEFISKALELOGICALNR"));
                kaseLogicalNumber = int.Parse(kase.OColVlerat.ktheVlereOpsioni("KASEFISKALELOGICALNR"));
                switch (kf_lloj)
                {
                    case 1:
                        //frmKasaMbyllGjendje.Show;
                        break;
                    case 2:
                        //  StreamWriter sw = File.CreateText(kf_file);
                        // if (vlera.merrVlereOpsioni("KASEAPOPRINTER") == "True")  //kase
                        if (kase.OColVlerat.ktheVlereOpsioni("KASEAPOPRINTER") == "False")  //kase
                        {
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Funksioni i kerkuar nuk eshte i mundur per kasen tuaj!", pnlMesazhi);
                            percaktoTemplate();
                            return;
                        }
                        else //text += "Z," + kaseLogicalNumber + ",_______,_,__;"; // sw.WriteLine("Z," + kaseLogicalNumber + ",_______,_,__;");
                            text += "10";
                        //  sw.Close();
                        break;
                    case 5:
                        // StreamWriter swg = File.CreateText(kf_file);
                        text += "E," + kaseLogicalNumber + ",______,_,__;Printo Z raportin;F-Link ks;||";//  swg.WriteLine("E," + kaseLogicalNumber + ",______,_,__;Printo Z raportin;F-Link ks;");
                        text += "Z," + kaseLogicalNumber + ",______,_,__;||";//    swg.WriteLine("Z," + kaseLogicalNumber + ",______,_,__;");
                        DbCore.DbAdmin.clsNdermarrje nderm = new DbCore.DbAdmin.clsNdermarrje(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                        text += "E," + kaseLogicalNumber + ",______,_,__;" + nderm.NdermarrjePershkrimi;//   swg.WriteLine("E," + kaseLogicalNumber + ",______,_,__;" + nderm.NdermarrjePershkrimi);

                        //  swg.Close();
                        break;
                    case 0:
                    case 7:
                        //  StreamWriter swi = File.CreateText(kf_file);
                        //if (vlera.merrVlereOpsioni("KASEAPOPRINTER") == "True")  //kase
                        if (kase.OColVlerat.ktheVlereOpsioni("KASEAPOPRINTER") == "True") //kase
                            text += "Z," + kaseLogicalNumber + ",______,_,__;";//   swi.WriteLine("Z," + kaseLogicalNumber + ",______,_,__;");
                        else
                            text += "69," + kaseLogicalNumber + ",______,_,__;0;";// swi.WriteLine("69," + kaseLogicalNumber + ",______,_,__;0;");
                        //  swi.Close();
                        break;
                    default:
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Funksioni i kerkuar nuk eshte i mundur per kasen tuaj!", pnlMesazhi); percaktoTemplate();
                        return;
                        //break;
                }
                hfKasa.Value = text;
                percaktoTemplate();
            }
        }


        //protected void btnPlu_Click(object sender, EventArgs e)
        //    {
        //    hfKasa.Value = "";
        //    string kf_file;
        //    int kf_lloj;
        //    int kaseLogicalNumber;

        //    DbCore.DbAdmin.clsVleraKonfigurimiKasa vlera = new DbCore.DbAdmin.clsVleraKonfigurimiKasa();
        //    kf_lloj = int.Parse(cmbLlojiKases.Value.ToString());
        //    if (kf_lloj < 0) return;
        //    kf_file = vlera.merrVlereOpsioni("KASEFISKALEPATH");

        //    if (kf_file == "") return;
        //    string text = kf_file + "&&";
        //    kaseLogicalNumber = int.Parse(vlera.merrVlereOpsioni("KASEFISKALELOGICALNR"));

        //    switch (kf_lloj)
        //        {
        //        case 1:
        //            break;
        //        case 5:
        //           // StreamWriter swg = File.CreateText(kf_file);
        //          //  swg.WriteLine("O," + kaseLogicalNumber + ",______,_,__;ALL");
        //         //   swg.Close();
        //            text += "O," + kaseLogicalNumber + ",______,_,__;ALL";
        //            break;
        //        case 0:
        //            break;

        //        }
        //    hfKasa.Value = text;
        //    percaktoTemplate();
        //    }

        protected void btnPlu_Click(object sender, EventArgs e)
        {
            var idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
            var idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);
            if (hfShtimModifikim.Value == "modifikim")
            {
                hfKasa.Value = "";
                string kf_file;
                int kf_lloj;
                int kaseLogicalNumber;
                DbCore.DbAdmin.clsKonfigurimKase kase = new clsKonfigurimKase(Convert.ToInt32(hfId.Value));
                kf_lloj = int.Parse(cmbLlojiKases.Value.ToString());
                /*    if (kf_lloj < 0) return;
                   //kf_file = vlera.merrVlereOpsioni("KASEFISKALEPATH");
                   kf_file = kase.OColVlerat.ktheVlereOpsioni("KASEFISKALEPATH");
                 /*  if (kf_lloj == 2)
                   {
                       if (kf_file.LastIndexOf("\\") != 0)
                       {
                           kf_file = kf_file.Substring(0, kf_file.LastIndexOf("\\") + 1) + "pludel.txt";
                       }
                       else if (kf_file.LastIndexOf("/") != 0)
                       {
                           kf_file = kf_file.Substring(0, kf_file.LastIndexOf("/") + 1) + "pludel.txt";
                       }
                   }
                  
                if (kf_file == "") return;
                string text = kf_file + "&&"; */
                kaseLogicalNumber = int.Parse(kase.OColVlerat.ktheVlereOpsioni("KASEFISKALELOGICALNR"));

                switch (kf_lloj)
                {
                    case 1:
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Funksioni i kerkuar nuk eshte i mundur per kasen tuaj!", pnlMesazhi);
                        percaktoTemplate();
                        return;
                    case 5:
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Funksioni i kerkuar nuk eshte i mundur per kasen tuaj!", pnlMesazhi);
                        percaktoTemplate();
                        // StreamWriter swg = File.CreateText(kf_file);
                        //  swg.WriteLine("O," + kaseLogicalNumber + ",______,_,__;ALL");
                        //   swg.Close();
                        //text += "O," + kaseLogicalNumber + ",______,_,__;ALL";
                        return;
                    case 2:
                        DbCore.DbAdmin.clsVleraKonfigurimiKasa konf = new DbCore.DbAdmin.clsVleraKonfigurimiKasa();
                        konf.updatePLU(idNdermarrje, 1, Convert.ToInt32(hfId.Value));
                        var mesazhKasa = KrijuesKasash.printoNeKase(Convert.ToInt32(kase.IdKonfigurimi), false, String.Empty, new clsKokaShitje(), idPerdoruesi, idNdermarrje, String.Empty, false, true);

                        if (mesazhKasa.Item1.Status)
                        {

                            if (mesazhKasa.Item2 != null)
                            {
                                hfKasaNew.Value = mesazhKasa.Item2;
                            }
                            else
                                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "PLU U Fshi me SUKSES!", pnlMesazhi);
                        }
                        else
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhKasa.Item1.PershkrimMesazhi, pnlMesazhi);


                        break;
                    case 0:
                    case 7:
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Funksioni i kerkuar nuk eshte i mundur per kasen tuaj!", pnlMesazhi);
                        percaktoTemplate();
                        return;
                    default:
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Funksioni i kerkuar nuk eshte i mundur per kasen tuaj!", pnlMesazhi);
                        percaktoTemplate();
                        return;
                }
                // hfKasa.Value = text;
                percaktoTemplate();
            }
        }

        //protected void ButtonOk_Click2(object sender, EventArgs e)
        //    {
        //    hfKasa.Value = "";
        //    string kf_file;
        //    int kf_lloj;
        //    int kaseLogicalNumber;
        //    DbCore.DbAdmin.clsVleraKonfigurimiKasa vlera = new DbCore.DbAdmin.clsVleraKonfigurimiKasa();
        //    kf_lloj = int.Parse(cmbLlojiKases.Value.ToString());
        //    if (kf_lloj < 0) return;
        //    kf_file = vlera.merrVlereOpsioni("KASEFISKALEPATH");

        //    if (kf_file == "") return;
        //    string text = kf_file + "&&";
        //    kaseLogicalNumber = int.Parse(vlera.merrVlereOpsioni("KASEFISKALELOGICALNR"));
        //    if (hfPyetje.Value == "X")
        //        {
        //        //StreamWriter swg = File.CreateText(kf_file);
        //        text += "X," + kaseLogicalNumber + ",_______,_,__;";// swg.WriteLine("X," + kaseLogicalNumber + ",_______,_,__;");
        //       // swg.Close();
        //        }
        //    else  if (hfPyetje.Value == "Z")
        //        {
        //        text += "E," + kaseLogicalNumber + ",______,_,__;Printo Z raportin;F-Link ks;||";//  swg.WriteLine("E," + kaseLogicalNumber + ",______,_,__;Printo Z raportin;F-Link ks;");
        //        text += "Z," + kaseLogicalNumber + ",______,_,__;||";//    swg.WriteLine("Z," + kaseLogicalNumber + ",______,_,__;");
        //        DbCore.DbAdmin.clsNdermarrje nderm = new DbCore.DbAdmin.clsNdermarrje(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //        text += "E," + kaseLogicalNumber + ",______,_,__;" + nderm.NdermarrjePershkrimi;//   swg.WriteLine("E," + kaseLogicalNumber + ",______,_,__;" + nderm.NdermarrjePershkrimi);

        //       // StreamWriter swg = File.CreateText(kf_file);
        //       // swg.WriteLine("E," + kaseLogicalNumber + ",______,_,__;Printo Z raportin;F-Link ks;");
        //       // swg.WriteLine("Z," + kaseLogicalNumber + ",______,_,__;");
        //      //  DbCore.DbAdmin.clsNdermarrje nderm = new DbCore.DbAdmin.clsNdermarrje(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //      //  swg.WriteLine("E," + kaseLogicalNumber + ",______,_,__;" + nderm.NdermarrjePershkrimi);

        //      //  swg.Close();
        //        }
        //    else   if (hfPyetje.Value == "PLU")
        //        {
        //        text += "O," + kaseLogicalNumber + ",______,_,__;ALL";// StreamWriter swg = File.CreateText(kf_file);
        //      //  swg.WriteLine("O," + kaseLogicalNumber + ",______,_,__;ALL");
        //      //  swg.Close();
        //        }
        //    percaktoTemplate();
        //    hfKasa.Value = text;
        //    }

        #region ishte eventi  ButtonOk_Click2
        //protected void ButtonOk_Click2(object sender, EventArgs e)
        //{
        //    hfKasa.Value = "";
        //    string kf_file;
        //    int kf_lloj;
        //    int kaseLogicalNumber;
        //    DbCore.DbAdmin.clsVleraKonfigurimiKasa vlera = new DbCore.DbAdmin.clsVleraKonfigurimiKasa();
        //    DbCore.DbAdmin.clsKonfigurimKase kase = new DbCore.DbAdmin.clsKonfigurimKase();
        //    kase.merrKonfiguriminSipasNdermarjes(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //    kf_lloj = int.Parse(cmbLlojiKases.Value.ToString());
        //    if (kf_lloj < 0) return;
        //    kf_file = kase.OColVlerat.ktheVlereOpsioni("KASEFISKALEPATH");

        //    if (kf_file == "") return;
        //    string text = kf_file + "&&";
        //    kaseLogicalNumber = int.Parse(kase.OColVlerat.ktheVlereOpsioni("KASEFISKALELOGICALNR"));
        //    if (hfPyetje.Value == "X")
        //    {
        //        //StreamWriter swg = File.CreateText(kf_file);
        //        text += "X," + kaseLogicalNumber + ",_______,_,__;";// swg.WriteLine("X," + kaseLogicalNumber + ",_______,_,__;");
        //        // swg.Close();
        //    }
        //    else if (hfPyetje.Value == "Z")
        //    {
        //        text += "E," + kaseLogicalNumber + ",______,_,__;Printo Z raportin;F-Link ks;||";//  swg.WriteLine("E," + kaseLogicalNumber + ",______,_,__;Printo Z raportin;F-Link ks;");
        //        text += "Z," + kaseLogicalNumber + ",______,_,__;||";//    swg.WriteLine("Z," + kaseLogicalNumber + ",______,_,__;");
        //        DbCore.DbAdmin.clsNdermarrje nderm = new DbCore.DbAdmin.clsNdermarrje(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //        text += "E," + kaseLogicalNumber + ",______,_,__;" + nderm.NdermarrjePershkrimi;//   swg.WriteLine("E," + kaseLogicalNumber + ",______,_,__;" + nderm.NdermarrjePershkrimi);

        //        // StreamWriter swg = File.CreateText(kf_file);
        //        // swg.WriteLine("E," + kaseLogicalNumber + ",______,_,__;Printo Z raportin;F-Link ks;");
        //        // swg.WriteLine("Z," + kaseLogicalNumber + ",______,_,__;");
        //        //  DbCore.DbAdmin.clsNdermarrje nderm = new DbCore.DbAdmin.clsNdermarrje(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //        //  swg.WriteLine("E," + kaseLogicalNumber + ",______,_,__;" + nderm.NdermarrjePershkrimi);

        //        //  swg.Close();
        //    }
        //    else if (hfPyetje.Value == "PLU")
        //    {
        //        text += "O," + kaseLogicalNumber + ",______,_,__;ALL";// StreamWriter swg = File.CreateText(kf_file);

        //        //  swg.WriteLine("O," + kaseLogicalNumber + ",______,_,__;ALL");
        //        //  swg.Close();
        //    }
        //    percaktoTemplate();
        //    hfKasa.Value = text;
        //}
        #endregion

        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            List<object> rreshtat;
            if (ASPxPageControl1.ActiveTabIndex == 0)
                rreshtat = gvKasat.GetSelectedFieldValues("IdKonfigurimi");
            else
            {
                rreshtat = new List<object>();
                rreshtat.Add(hfId.Value);
            }
            if (rreshtat.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ju lutem zgjidhni te pakten nje konfigurim kase!", pnlMesazhi);
                return;
            }
            List<string> konfigurimeTeFshira = new List<string>();
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idPerdorues = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            for (int i = 0; i < rreshtat.Count; i++)
            {
                DbCore.clsMesazh mesazhi = clsKonfigurimKase.fshikonfigurimkaseStatus(Convert.ToInt32(rreshtat[i]), idPerdorues);
                if (mesazhi.Status)
                {
                    clsKonfigurimKase kasa = new clsKonfigurimKase(Convert.ToInt32(rreshtat[i]));
                    hiqKonfigurimKaseNgaGrida(idNdermarrje, kasa.IdKonfigurimi);
                    konfigurimeTeFshira.Add(kasa.Kodi);
                    ASPxPageControl1.ActiveTabIndex = 0;
                    hfStatusi.Value = "true";
                }
            }
            string mesazhInfoGabim = "", mesazhInfoSukses = "";
            if (konfigurimeTeFshira.Count == 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", "Konfigurimi i kases me kod: ", String.Join(";", konfigurimeTeFshira), " u fshi me sukses!");
            else
                if (konfigurimeTeFshira.Count > 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", "Konfigurimet e kases me kod ", String.Join(";", konfigurimeTeFshira), " u fshine me sukses!");
            if (mesazhInfoGabim != "" && mesazhInfoSukses != "")
                mesazhInfoGabim += " . Kurse " + mesazhInfoSukses;
            if (mesazhInfoGabim != "")
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhInfoGabim, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazhInfoSukses, pnlMesazhi);
            pnlMesazhi.Update();
        }

        protected void mbyllXhiroButon_Click(object sender, EventArgs e)
        {
            if (hfShtimModifikim.Value == "modifikim")
            {
                hfKasa.Value = "";
                DbCore.DbAdmin.clsKonfigurimKase kase = new clsKonfigurimKase(Convert.ToInt32(hfId.Value));
                String descria;
                String printo;
                String kf_file = kase.OColVlerat.ktheVlereOpsioni("KASEFISKALEPATH");
                if (kf_file == "") return;
                String text = kf_file + "&&";

                if (printoCheck.Checked == true)
                {
                    printo = "";
                }
                else printo = "NOPRINT";

                if (cmbLlojMbyllje.Value != null)
                {
                    int numer = Convert.ToInt32(cmbLlojMbyllje.Value) + 1;
                    descria = "AZZGIO TIPO=" + numer + " " + printo + "||";
                    descria += "CHIAVE REG";
                    text += descria;
                }
                percaktoTemplate();
                konfiguroVleraFillestare(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                hfKasa.Value = text;
            }
        }

        protected void gvKasat_CustomJSProperties(object sender, DevExpress.Web.ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvKasat.PageIndex;
            e.Properties["cpPageRow"] = gvKasat.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvKasat.VisibleRowCount;
        }


        /// <summary>
        /// perdoret per te shtuar kolonen e selektimit tek grida dhe per te vendosur disa karakteristika te grides
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void gvKasat_DataBound(object sender, EventArgs e)
        {// shton colonen # per selektim dhe disa karakteristika te grides
            if (this.gvKasat.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
                //perzgjidh
                DevExpress.Web.GridViewCommandColumn check = new DevExpress.Web.GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true;
                check.Width = Unit.Percentage(2);
                gvKasat.Settings.ShowFilterRow = true;
                gvKasat.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                gvKasat.Settings.ShowFilterRowMenu = true;
                gvKasat.Columns.Add(check);
                gvKasat.KeyFieldName = "IdKonfigurimi";
                gvKasat.SettingsBehavior.AllowSelectByRowClick = true;
                gvKasat.SettingsBehavior.AllowFocusedRow = true;
            }
        }

        /// <summary>
        /// perdoret ne rastet kur grida ben callback per ta rimbushur griden
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void gvKasat_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
        }

        protected void gvKasat_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
        }

        /// <summary>
        /// perdoret per te vendosur filtrat tek headeri i grides
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvKasat_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            string TeGjithe = rm.GetString("GridHeaderFilterFillItemTeGjithe", ci);
            string nga = rm.GetString("GridHeaderFilterFillItemNga", ci);
            if (e.Column.FieldName == "Kodi")
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

        protected void gvKasat_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if (e.Column.FieldName == "IdKonfigurimi")
            {
                if (Converter.ConvertToInt(e.Value) == 0)
                {
                    e.Criteria = null;
                }
            }
        }

        protected void ASPxPageControl1_ActiveTabChanging(object source, DevExpress.Web.TabControlCancelEventArgs e)
        {

        }
        private void mbushHiddenFieldMePerkthime(CultureInfo cultinf, ResourceManager rm)
        {

            hfState.Set("msgFaturaDerguaKaseFiskaleSukses", rm.GetString("msgFaturaDerguaKaseFiskaleSukses", cultinf));
            hfState.Set("msgDownloadPrograminEKasesTeMenu", rm.GetString("msgDownloadPrograminEKasesTeMenu", cultinf));
        }
    }
}