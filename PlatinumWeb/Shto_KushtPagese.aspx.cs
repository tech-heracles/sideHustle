using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Drawing;
using AjaxControlToolkit;
using PlatinumWeb.Templates;
using System.Globalization;
using System.Resources;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class Shto_KushtPagese : MyPageBase
    {        
        private DbCore.DbKontabiliteti.clsKushtPageseKoka kokaOverview;
        private DbCore.DbKontabiliteti.colKushtPageseTrupi oColTrupiKushtPagese;
        //DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();
        ASPxComboBox tempcombo = null;
        ASPxTextBox temptxt = null;
        TextBox temptxtNormal = null;
        private int nrRreshtash = 5;
        private string koloneFocus;
        private string komponente = "KushtePagese.aspx";
        private string guidString;
        private int idPerdoruesi;
        protected void Page_Load(object sender, EventArgs e)
        {
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);

            //if (CacheLayer.GlobalCacheManager.MySessionCache["LoggedIn"].Equals("No"))
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                DbCore.clsFunksione.logout(Session,true,"FaqePaautorizuar");
            }
            idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            //if (CacheLayer.GlobalCacheManager.MySessionCache["KodiNdermarrjes"] == null)
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerdoruesi);
            }
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            if (Page.IsPostBack == false)
            {
                guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                hfState.Set("guidString", guidString);
                EmrateTabeve();
                konfiguroVleraFillestare(idPerdoruesi, idNdermarrje);
                konfiguroGride(idPerdoruesi, idNdermarrje, rm, ci);
                konfiguroGrideTrupi(idNdermarrje);
            }
            guidString = (string)hfState["guidString"];
            percaktoTemplate();
            Container.Attributes["width"] = "450px";
            Container.Attributes["height"] = "450px";
        }

        /// <summary>
        /// Vendos emrat e tabeve ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateTabeve()
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            ASPxPageControl1.TabPages[0].Text = rm.GetString("labelBlerjeShitjeTePergjithshme", cultinf);
            ASPxPageControl1.TabPages[1].Text = rm.GetString("kushtPageseTab", cultinf);
            ASPxPageControl1.TabPages[2].Text = rm.GetString("trupiTab", cultinf);
        }

        /// <summary>
        /// Konfiguron vlerat fillestare te faqes. Mbush combot e pagesave, ndarjeve, intervaleve, autorizimeve.
        /// Therret funksionet <see cref="mbushListeKushteshPagese"/> dhe <see cref="InitializeGridTrupi"/>
        /// </summary>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idNdermarrje"></param>
        private void konfiguroVleraFillestare(int idPerdoruesi, int idNdermarrje)
        {
            ASPxPageControl1.ActiveTabIndex = 0;

            pagesa_ASPxComboBox.Items.Add("E plote", true);
            pagesa_ASPxComboBox.Items.Add("Me pjese", false);

            ndarja_ASPxComboBox.Items.Add("Perqindje", true);
            ndarja_ASPxComboBox.Items.Add("Interval", false);

            intervali_ASPxComboBox.Items.Add("Dite", 1);
            intervali_ASPxComboBox.Items.Add("Jave", 2);
            intervali_ASPxComboBox.Items.Add("Muaj", 3);
            ConfigureAspxComboBox.mbushComboAutorizime(idPerdoruesi, txtAutorizimi);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(txtAutorizimi);

            mbushListeKushteshPagese(idNdermarrje);
            InitializeGridTrupi();
        }

        /// <summary>
        /// Mbush griden e kushteve te pageses me gjithe kushtet e pageses se ndermarrjes.
        /// </summary>
        /// <param name="idNdermarrje"></param>
        private void mbushListeKushteshPagese(int idNdermarrje)
        {
            //dbKontab = new DbCore.DbKontabiliteti.clsDatabaseKontabilitet();
            //DbCore.DbKontabiliteti.colKushtPageseKoka colKushtetPageses = dbKontab.merrGjitheKushtetPageses(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            DbCore.DbKontabiliteti.colKushtPageseKoka colKushtetPageses = new DbCore.DbKontabiliteti.colKushtPageseKoka(idNdermarrje);
            grid_KushtePagese.DataSource = colKushtetPageses;
            grid_KushtePagese.DataBind();
        }

        /// <summary>
        /// Konfiguron griden e kushteve te pageses
        /// </summary>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idNdermarrje"></param>
        private void konfiguroGride(int idPerdoruesi, int idNdermarrje, ResourceManager rm, CultureInfo ci)
        {
      
            KonfigurimComboGride.shtoAutorizimSipasKushtePagese(grid_KushtePagese, idPerdoruesi, Session, komponente, guidString, "IdAutorizim");
            KonfigurimComboGride.shtoLlojPagese(grid_KushtePagese, rm, ci);
            GridUtil.percaktoVisibleColumnsShto(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, grid_KushtePagese, "grid_KushtePagese", komponente);
            //funksione.percaktoAtributeTeGridesShto(grid_KushtePagese, "IdKoka");
            GridUtil.percaktoAtributeTeGridesShtoPaTheme(grid_KushtePagese, "IdKoka");

        }

        /// <summary>
        /// Konfiguron griden e trupit te kushteve te pageses
        /// </summary>
        /// <param name="idNdermarrje"></param>
        private void konfiguroGrideTrupi(int idNdermarrje)
        {
            shtoKolone("Intervali");
            shtoKolone("Periudha");
            shtoKolone("KushtPagese");

            GridUtil.percaktoVisibleColumnsMeWidth(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, grid_trupi, "grid_KushtPagesetrupi", "Shto_KushtPagese.aspx");
            //funksione.percaktoAtributeTeGridesShto(grid_trupi, "IdTrupi");
            GridUtil.percaktoAtributeTeGridesShtoPaTheme(grid_trupi, "IdTrupi");
        }

        /// <summary>
        /// Merr nje kolone te grides dhe e shnderron ne combobox
        /// </summary>
        /// <param name="emri">Emri i kolones</param>
        private void shtoKolone(String emri)
        {
            grid_trupi.Columns.Remove(grid_trupi.Columns[emri]);
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            grid_trupi.Columns.Add(colnew);
            colnew.FieldName = emri;
        }

        /// <summary>
        /// Therret funksionin <see cref="formoColKushtePagese"/> per te formuar collectionin em kushtet e pageses dhe funksionin <see cref="ruajKushtPagese"/> per ta ruajtur ate.
        /// </summary>
        protected void ruaj_Button_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                String vlerat = HiddenFieldTrupi.Value;
                if (vlerat != "Gabim")
                {
                    int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                    formoColKushtePagese(idNdermarrje, vlerat);
                    pergjigja.Text = "";
                    ruajKushtPagese(idNdermarrje);
                }
            }
        }

        /// <summary>
        /// Formon nje collection me objekte te tipit clsKushtPageseTrupi.
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="vlerat">Vlerat e kushteve te pageses te marra nga client side</param>
        protected void formoColKushtePagese(int idNdermarrje, String vlerat)
        {
            oColTrupiKushtPagese = new DbCore.DbKontabiliteti.colKushtPageseTrupi();
            string[] arrKushtePagese = vlerat.ToString().Split(';');

            string[] arrIntervali = arrKushtePagese[0].Split(',');
            for (int i = 0; i < arrIntervali.Length; i++)
            {
                string[] arrCompIntervali = arrIntervali[i].Split(':');
                if (arrCompIntervali.Length != 1)
                {
                    if (Convert.ToInt32(arrCompIntervali[0]) < oColTrupiKushtPagese.Count())
                    {
                        oColTrupiKushtPagese[Convert.ToInt32(arrCompIntervali[0])].Intervali = arrCompIntervali[1];
                    }
                    else
                    {
                        DbCore.DbKontabiliteti.clsKushtPageseTrupi oTrupi = new DbCore.DbKontabiliteti.clsKushtPageseTrupi();
                        oTrupi.Intervali = arrCompIntervali[1];
                        oColTrupiKushtPagese.Add(oTrupi);
                    }
                }
            }

            string[] arrPeriudha = arrKushtePagese[1].Split(',');
            for (int i = 0; i < arrPeriudha.Length; i++)
            {
                string[] arrCompPeriudha = arrPeriudha[i].Split(':');
                if (arrCompPeriudha.Length != 1)
                {
                    if (!(arrCompPeriudha[1].ToString() == " "))
                    {
                        if (Convert.ToInt32(arrCompPeriudha[0]) < oColTrupiKushtPagese.Count())
                        {
                            oColTrupiKushtPagese[Convert.ToInt32(arrCompPeriudha[0])].Periudha = arrCompPeriudha[1].ToString();
                        }
                        else
                        {
                            DbCore.DbKontabiliteti.clsKushtPageseTrupi oTrupi = new DbCore.DbKontabiliteti.clsKushtPageseTrupi();
                            oTrupi.Periudha = arrCompPeriudha[1].ToString();
                            oColTrupiKushtPagese.Add(oTrupi);
                        }
                    }
                }
            }

            string[] arrDite = arrKushtePagese[2].Split(',');
            for (int i = 0; i < arrDite.Length; i++)
            {
                string[] arrCompDite = arrDite[i].Split(':');
                if (arrCompDite.Length != 1)
                {
                    if (!(arrCompDite[1].ToString() == " "))
                    {
                        if (Convert.ToInt32(arrCompDite[0]) < oColTrupiKushtPagese.Count())
                        {
                            oColTrupiKushtPagese[Convert.ToInt32(arrCompDite[0])].Dite = Convert.ToInt32((arrCompDite[1].ToString()));
                        }
                        else
                        {
                            DbCore.DbKontabiliteti.clsKushtPageseTrupi oTrupi = new DbCore.DbKontabiliteti.clsKushtPageseTrupi();
                            oTrupi.Dite = Convert.ToInt32(arrCompDite[1].ToString());
                            oColTrupiKushtPagese.Add(oTrupi);
                        }
                    }
                }
            }

            string[] arrZbritje = arrKushtePagese[3].Split(',');
            for (int i = 0; i < arrZbritje.Length; i++)
            {
                string[] arrCompZbritje = arrZbritje[i].Split(':');
                if (arrCompZbritje.Length != 1)
                {
                    if (!(arrCompZbritje[1].ToString() == " "))
                    {
                        if (Convert.ToInt32(arrCompZbritje[0]) < oColTrupiKushtPagese.Count())
                        {
                            oColTrupiKushtPagese[Convert.ToInt32(arrCompZbritje[0])].Zbritje = Convert.ToInt32((arrCompZbritje[1].ToString()));
                        }
                        else
                        {
                            DbCore.DbKontabiliteti.clsKushtPageseTrupi oTrupi = new DbCore.DbKontabiliteti.clsKushtPageseTrupi();
                            oTrupi.Zbritje = Convert.ToInt32(arrCompZbritje[1].ToString());
                            oColTrupiKushtPagese.Add(oTrupi);
                        }
                    }
                }
            }

            string[] arrKushtPagese = arrKushtePagese[4].Split(',');
            for (int i = 0; i < arrKushtPagese.Length; i++)
            {
                string[] arrCompKushtPagese = arrKushtPagese[i].Split(':');
                if (arrCompKushtPagese.Length != 1)
                {
                    if (!(arrCompKushtPagese[1].ToString() == " "))
                    {
                        //DbCore.DbKontabiliteti.clsDatabaseKontabilitet dbKontab = new DbCore.DbKontabiliteti.clsDatabaseKontabilitet();
                        //DbCore.DbKontabiliteti.colKushtPageseKoka col = dbKontab.merrKushtPageseSipasKodit(arrCompKushtPagese[1].ToString(), new DbCore.clsFunksione ().ktheIdNdermarrje ());
                        DbCore.DbKontabiliteti.clsKushtPageseKoka clsKoka = new DbCore.DbKontabiliteti.clsKushtPageseKoka(arrCompKushtPagese[1].ToString(), idNdermarrje);
                        if (clsKoka != null)
                        {
                            if (Convert.ToInt32(arrCompKushtPagese[0]) < oColTrupiKushtPagese.Count())
                            {
                                oColTrupiKushtPagese[Convert.ToInt32(arrCompKushtPagese[0])].KushtPagese = Convert.ToInt32(clsKoka.IdKoka);
                            }
                            else
                            {
                                DbCore.DbKontabiliteti.clsKushtPageseTrupi oTrupi = new DbCore.DbKontabiliteti.clsKushtPageseTrupi();
                                oTrupi.KushtPagese = Convert.ToInt32(clsKoka.IdKoka);
                                oColTrupiKushtPagese.Add(oTrupi);
                            }
                        }
                    }
                }
            }

            string[] focus = arrKushtePagese[5].Split(':');
            koloneFocus = focus[0];
            grid_trupi.DataSource = oColTrupiKushtPagese;
            grid_trupi.DataBind();
        }

        /// <summary>
        /// Pastron fushat dhe iniciaizon griden e trupit
        /// Shiko funksionin <see cref="InitializeGridTrupi"/>
        /// </summary>
        private void pastroFusha()
        {
            ASPxPageControl1.ActiveTabIndex = 0;
            pagesa_ASPxComboBox.Text = "";
            kodi_TextBox.Text = "";
            kodi1_TextBox.Text = "";
            emertimi_TextBox.Text = "";
            emertimi1_TextBox.Text = "";
            afati_TextBox.Text = "";
            ndarja_ASPxComboBox.Text = "";
            intervali_ASPxComboBox.Text = "";
            nrNdarjeve_TextBox.Text = "";
            grid_KushtePagese.CancelEdit();
            grid_KushtePagese.AddNewRow();
            HiddenField1.Value = "";
            InitializeGridTrupi();
        }

        /// <summary>
        /// Therret funksionin <see cref="krijoKushtPagese"/> per te krijuar kushtin e pageses.
        /// Therret funksionin <see cref="DbCore.DbKontabiliteti.clsKushtPageseKoka.ruaj"/> per te ruajtur kushtin e pageses.
        /// </summary>
        /// <param name="idNdermarrje"></param>
        private void ruajKushtPagese(int idNdermarrje)
        {
            DbCore.DbKontabiliteti.clsKushtPageseKoka koka;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            if (Page.IsValid == false)
            {
                percaktoTamplateAutorizim(); return;
            }
            else
            {
                koka = krijoKushtPagese();

                mesazh = koka.ruaj();
                pergjigja.Text = mesazh.PershkrimMesazhi;
                if (mesazh.Status == true)
                    Response.Redirect("KushtePagese.aspx?ruaj=ok&indexrow=" + grid_KushtePagese.VisibleRowCount);
                else
                {
                    pergjigja.ForeColor = Color.Red;
                    mbushListeKushteshPagese(idNdermarrje);
                    percaktoTamplateAutorizim();
                }

            }
        }

        /// <summary>
        /// Ruan kushtin e pageses kur klikohet ruaj nga tabi ku ndodhet grida overview.
        /// </summary>
        /// <param name="idNdermarrje"></param>
        private void ruajKushtOverview(int idNdermarrje)
        {
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            if (Page.IsValid == false)
                return;
            else
            {
                grid_KushtePagese.UpdateEdit();

                if (isValidAgjentOverview())
                {
                    mesazh = kokaOverview.ruaj();
                    pergjigja.Text = mesazh.PershkrimMesazhi;
                    if (mesazh.Status == true)
                    {
                        pergjigja.ForeColor = Color.Green;
                        grid_KushtePagese.AddNewRow();
                        pastroFusha();
                    }
                    else
                    {
                        pergjigja.ForeColor = Color.Red;
                    }
                    mbushListeKushteshPagese(idNdermarrje);
                    percaktoTamplateAutorizim();
                }
            }
        }

        /// <summary>
        /// Kontrollon nese kushti i pageses i percaktuar ne overview eshte i rregullt
        /// </summary>
        /// <returns></returns>
        private bool isValidAgjentOverview()
        {
            bool isValid;
            isValid = true;

            if (kokaOverview == null)
            {
                isValid = false;
            }
            percaktoTamplateAutorizim();
            return isValid;

        }

        /// <summary>
        /// Nderton nje objekt te tipit DbCore.DbKontabiliteti.clsKushtPageseKoka
        /// </summary>
        private DbCore.DbKontabiliteti.clsKushtPageseKoka krijoKushtPagese()
        {
            DbCore.DbKontabiliteti.clsKushtPageseKoka koka = new DbCore.DbKontabiliteti.clsKushtPageseKoka();
            //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            koka.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            koka.IdStatusDok = 1;
            koka.KodiKushtPagese = kodi_TextBox.Text;
            koka.EmertimiKushtPagese = emertimi_TextBox.Text;
            koka.LlojiKushtPagese = pagesa_ASPxComboBox.SelectedItem.Text.ToString();
            if (txtAutorizimi.Text != "")
                koka.IdAutorizim = DbCore.DbAdmin.clsAutorizimKoka.ktheIDAutorizim(txtAutorizimi.Text);
            //koka.IdAutorizim = dbAdmin.ktheAutorizim(txtAutorizimi.Text)[0].IdAutorizimKoka;
            else koka.IdAutorizim = 0;
            if (koka.LlojiKushtPagese == "E plote")
            {
                if (afati_TextBox.Text != "")
                    koka.Afati = int.Parse(afati_TextBox.Text);
                else koka.Afati = 0;
                koka.Ndarja = "";
                koka.IntervaliMidisNdarjeve = "";
                koka.Numri = -1;
                koka.NumriNdarjeve = -1;
            }
            else if (koka.LlojiKushtPagese == "Me pjese")
            {
                koka.Afati = -1;
                koka.Ndarja = ndarja_ASPxComboBox.Text.ToString();
                if (koka.Ndarja == "Interval")
                {
                    if (intervali_ASPxComboBox.SelectedItem != null)
                        koka.IntervaliMidisNdarjeve = intervali_ASPxComboBox.SelectedItem.Text.ToString();
                    else koka.IntervaliMidisNdarjeve = "";
                    koka.Numri = 1;
                    if (nrNdarjeve_TextBox.Text != "")
                        koka.NumriNdarjeve = int.Parse(nrNdarjeve_TextBox.Text);
                    else koka.NumriNdarjeve = 0;
                }
                else if (koka.Ndarja == "Perqindje")
                {
                    koka.IntervaliMidisNdarjeve = "";
                    koka.Numri = -1;
                    koka.NumriNdarjeve = -1;
                }
                else if (koka.Ndarja == "")
                {
                    koka.IntervaliMidisNdarjeve = "";
                    koka.Numri = -1;
                    koka.NumriNdarjeve = -1;
                }
            }
            koka.IdNdermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            koka.OColTrupi = oColTrupiKushtPagese;
            return koka;
        }

        /// <summary>
        /// Nderton nje objekt DbCore.DbKontabiliteti.clsKushtPageseKoka duke e plotesuar me vlerat qe shenohen te grida overview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grid_KushtePagese_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            int i;
            for (i = 0; i < e.NewValues.Count; i++)
            {
                if (i == 3)
                    continue;
                else
                {
                    if (e.NewValues[i] == null)
                    {
                        e.Cancel = true;
                        return;
                    }
                }
            }
            kokaOverview = new DbCore.DbKontabiliteti.clsKushtPageseKoka();
            kokaOverview.KodiKushtPagese = e.NewValues["KodiKushtPagese"].ToString();
            kokaOverview.EmertimiKushtPagese = e.NewValues["EmertimiKushtPagese"].ToString();
            kokaOverview.LlojiKushtPagese = e.NewValues["LlojiKushtPagese"].ToString();
            kokaOverview.Afati = 0;
            kokaOverview.Ndarja = "";
            kokaOverview.IntervaliMidisNdarjeve = "";
            kokaOverview.Numri = 0;
            kokaOverview.NumriNdarjeve = 0;
            kokaOverview.IdNdermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            kokaOverview.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            kokaOverview.IdStatusDok = 1;
            string initVal = HiddenField1.Value;
            string[] pars1 = initVal.Split(';');
            if (pars1[3] != "")
            {
                //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
                kokaOverview.IdAutorizim = DbCore.DbAdmin.clsAutorizimKoka.ktheIDAutorizim(pars1[3]);
                //kokaOverview.IdAutorizim = dbAdmin.ktheAutorizim(pars1[3])[0].IdAutorizimKoka;
            }
            else kokaOverview.IdAutorizim = 0;

            //kokaOverview.OColTrupi = new DbCore.DbKontabiliteti.colKushtPageseTrupi();
            kokaOverview.OColTrupi = oColTrupiKushtPagese;

            e.Cancel = true;
        }

        /// <summary>
        /// Therret funksionet <see cref="formoColKushtePagese"/> dhe <see cref="ruajKushtOverview"/> per te ruajtur nje objekt DbCore.DbKontabiliteti.clsKushtPageseKoka nga overview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void overview_Button_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                String vlerat = HiddenFieldTrupi.Value;
                if (vlerat != "Gabim")
                {
                    int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                    formoColKushtePagese(idNdermarrje, vlerat);
                    pergjigja.Text = "";
                    ruajKushtOverview(idNdermarrje);
                }
            }
        }

        /// <summary>
        /// Therret funksionet <see cref="mbushListeKushteshPagese"/> dhe <see cref="percaktoTamplateAutorizim"/> pasi behet nje callback i grides
        /// </summary>
        protected void grid_KushtePagese_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            mbushListeKushteshPagese(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            percaktoTamplateAutorizim();
        }

        /// <summary>
        /// Percakton templaten per kolonen autorizim te grides se trupit
        /// </summary>
        private void percaktoTamplateAutorizim()
        {
            GridViewDataComboBoxColumn col1 = grid_KushtePagese.Columns["IdAutorizim"] as GridViewDataComboBoxColumn;
            col1.EditItemTemplate = new MyTemplateAutorizime(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), idPerdoruesi);
            col1.Width = 100;
        }

        /// <summary>
        /// Therret funksionin <see cref="percaktoTamplateAutorizim"/> kur shtohet nje rresht i ri ne gride.
        /// </summary>
        protected void grid_KushtePagese_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            percaktoTamplateAutorizim();
        }

        /// <summary>
        /// Therret funksionin <see cref="pastroFusha"/> per te pastruar te gjitha fushat e faqes.
        /// </summary>
        protected void overview_pastro_ASPxButton_Click(object sender, EventArgs e)
        {
            pastroFusha();
        }

        /// <summary>
        /// Therret funksionin <see cref="pastroFusha"/> per te pastruar te gjitha fushat e faqes.
        /// </summary>
        protected void pastro_ASPxButton_Click(object sender, EventArgs e)
        {
            pastroFusha();
        }

        /// <summary>
        /// Kontrollon nese jane plotesuar te gjitha fushat e detyrueshme
        /// </summary>
        protected void grid_KushtePagese_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            string initVal = HiddenField1.Value;
            string[] pars1 = initVal.Split(';');
            foreach (GridViewColumn column in grid_KushtePagese.Columns)
            {
                if (column.Visible == true)
                {
                    GridViewDataColumn dataColumn = column as GridViewDataColumn;
                    if (dataColumn == null) continue;
                    //if (dataColumn.FieldName == "IdAutorizim")
                    //    if (pars1.Length < 9)
                    //    {
                    //        if (HiddenField1.Value == "" || pars1[3] == "undefined" || pars1[3] == "")
                    //            e.Errors[dataColumn] = "Vlera nuk mund te jete null.";
                    //    }
                    //    else
                    //    {
                    //        if (HiddenField1.Value == "" || pars1[8] == "undefined" || pars1[8] == "")
                    //            e.Errors[dataColumn] = "Vlera nuk mund te jete null.";
                    //    }

                    if (e.NewValues[dataColumn.FieldName] == null && dataColumn.FieldName != "IdAutorizim")
                    {
                        e.Errors[dataColumn] = "Vlera nuk mund te jete null.";
                    }
                }
            }
            if (e.Errors.Count > 0)
            {
                e.RowError = "Ju lutemi, plotesoni te gjitha fushat.";
                percaktoTamplateAutorizim();
            }
            if (string.IsNullOrEmpty(e.RowError) && e.Errors.Count > 0)
            {
                e.RowError = "Ju lutemi, korrigjoni te gjithe gabimet.";
                percaktoTamplateAutorizim();
            }
        }

        /// <summary>
        /// Ben validimin e rreshtit kur nis editimi i rreshtit
        /// </summary>
        protected void grid_KushtePagese_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
        {
            if (!grid_KushtePagese.IsNewRowEditing)
            {
                grid_KushtePagese.DoRowValidation();
            }
        }

        /// <summary>
        /// Percakton funksionin qe thirret ne eventin TextChanged te editorit
        /// </summary>
        protected void grid_KushtePagese_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (e.Editor.GetType().Name == "ASPxTextBox")
            {

                ASPxTextBox currentEditor = e.Editor as ASPxTextBox;
                currentEditor.ClientSideEvents.TextChanged = "function(s,e){ProcessTextChanged('" + e.Column.FieldName + "',s.GetText());}";
            }
            else if (e.Editor.GetType().Name == "ASPxComboBox")
            {
                ASPxComboBox currentEditor = e.Editor as ASPxComboBox;
                currentEditor.ClientSideEvents.TextChanged = "function(s,e){ProcessTextChanged('" + e.Column.FieldName + "',s.GetText());}";
            }
        }

        /// <summary>
        /// Vendos 5 rreshta bosh ne giden e trupit
        /// </summary>
        private void InitializeGridTrupi()
        {
            oColTrupiKushtPagese = new DbCore.DbKontabiliteti.colKushtPageseTrupi();
            DbCore.DbKontabiliteti.clsKushtPageseTrupi oTrupi = new DbCore.DbKontabiliteti.clsKushtPageseTrupi();
            for (int i = 0; i < 5; i++)
            {
                oTrupi = new DbCore.DbKontabiliteti.clsKushtPageseTrupi();
                oColTrupiKushtPagese.Add(oTrupi);
            }
            grid_trupi.DataSource = oColTrupiKushtPagese;
            grid_trupi.DataBind();
        }

        /// <summary>
        /// Percakton templatet per kolonat e grides se trupit.
        /// </summary>
        private void percaktoTemplate()
        {
            GridViewDataComboBoxColumn col1 = grid_trupi.Columns["Intervali"] as GridViewDataComboBoxColumn;
            col1.DataItemTemplate = new MyComboTemplate();
            GridViewDataComboBoxColumn col2 = grid_trupi.Columns["Periudha"] as GridViewDataComboBoxColumn;
            col2.DataItemTemplate = new MyComboTemplate();
            GridViewDataTextColumn col3 = grid_trupi.Columns["Dite"] as GridViewDataTextColumn;
            col3.DataItemTemplate = new MyTextTemplate();
            GridViewDataTextColumn col4 = grid_trupi.Columns["Zbritje"] as GridViewDataTextColumn;
            col4.DataItemTemplate = new MyTextTemplate();
            GridViewDataComboBoxColumn col5 = grid_trupi.Columns["KushtPagese"] as GridViewDataComboBoxColumn;
            col5.DataItemTemplate = new MyTemplateKushtePagese();            
        }

        protected void grid_trupi_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            grid_trupi.DataBind();
        }

        protected void grid_trupi_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            bool ugjet;
            if (e.RowType == DevExpress.Web.GridViewRowType.Data)
            {
                GridViewDataComboBoxColumn col1 = ((ASPxGridView)sender).Columns["Intervali"] as GridViewDataComboBoxColumn;
                GridViewDataComboBoxColumn col2 = ((ASPxGridView)sender).Columns["Periudha"] as GridViewDataComboBoxColumn;
                GridViewDataColumn col3 = ((ASPxGridView)sender).Columns["Dite"] as GridViewDataColumn;
                GridViewDataColumn col4 = ((ASPxGridView)sender).Columns["Zbritje"] as GridViewDataColumn;
                GridViewDataComboBoxColumn col5 = ((ASPxGridView)sender).Columns["KushtPagese"] as GridViewDataComboBoxColumn; ;


                ASPxComboBox cmb1 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col1, "cmbBox") as ASPxComboBox;
                ASPxComboBox cmb2 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col2, "cmbBox") as ASPxComboBox;
                ASPxTextBox txt1 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col3, "txtBox") as ASPxTextBox;
                ASPxTextBox txt2 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col4, "txtBox") as ASPxTextBox;
                TextBox txt4 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col5, "txt") as TextBox;
                ASPxButton btn1 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col5, "btn") as ASPxButton;


                ugjet = false;

                if (cmb1 != null)
                {
                    cmb1.Items.Add("Dite", 0);
                    cmb1.Items.Add("Jave", 1);
                    cmb1.Items.Add("Muaj", 2);
                    cmb1.ClientInstanceName = "cmbIntervali" + e.VisibleIndex.ToString();
                    cmb1.ClientSideEvents.TextChanged = "function(s,e){TextChangedIntervali(cmbIntervali" + e.VisibleIndex.ToString() + ",'Intervali'," + e.VisibleIndex.ToString() + ");}";
                    if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 2)
                    {
                        if (ugjet)
                        {
                            tempcombo = cmb1;
                            ugjet = false;
                        }
                        else if (koloneFocus == "Intervali")
                        {
                            ugjet = true;
                        }
                    }
                }

                if (cmb2 != null)
                {
                    cmb2.ClientInstanceName = "cmbPeriudha" + e.VisibleIndex.ToString();
                    cmb2.ClientSideEvents.TextChanged = "function(s,e){TextChangedPeriudha(cmbPeriudha" + e.VisibleIndex.ToString() + ",'Periudha'," + e.VisibleIndex.ToString() + ");}";
                    if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 2)
                    {
                        if (ugjet)
                        {
                            tempcombo = cmb2;
                            ugjet = false;
                        }
                        else if (koloneFocus == "Periudha")
                        {
                            ugjet = true;
                        }
                    }
                }

                if (txt1 != null)
                {
                    txt1.ClientInstanceName = "txtDite" + e.VisibleIndex.ToString();
                    txt1.ClientSideEvents.LostFocus = "function(s,e){LostFocusDite(txtDite" + e.VisibleIndex.ToString() + ",'txtDite', " + e.VisibleIndex.ToString() + ");}";
                    if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 2)
                    {
                        if (ugjet)
                        {
                            temptxt = txt1;
                            ugjet = false;
                        }
                        else if (koloneFocus == "txtDite")
                        {
                            ugjet = true;
                        }
                    }
                }

                if (txt2 != null)
                {
                    txt2.ClientInstanceName = "txtZbritje" + e.VisibleIndex.ToString();
                    txt2.ClientSideEvents.LostFocus = "function(s,e){LostFocusZbritje(txtZbritje" + e.VisibleIndex.ToString() + ",'txtZbritje', " + e.VisibleIndex.ToString() + ");}";
                    if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 2)
                    {
                        if (ugjet)
                        {
                            temptxt = txt2;
                            ugjet = false;
                        }
                        else if (koloneFocus == "txtZbritje")
                        {
                            ugjet = true;
                        }
                    }
                }

                if (txt4 != null)
                {
                    txt4.ID = "KushtPagese" + e.VisibleIndex.ToString();
                    btn1.ID = "ButonKushtPagese" + e.VisibleIndex.ToString();

                    AutoCompleteExtender auComplete = new AutoCompleteExtender();
                    auComplete.ServiceMethod = "ktheKushtPagese";
                    auComplete.MinimumPrefixLength = 1;
                    auComplete.EnableCaching = false;
                    auComplete.Enabled = true;
                    auComplete.FirstRowSelected = true;
                    auComplete.TargetControlID = txt4.ID;
                    auComplete.CompletionInterval = 100;
                    auComplete.CompletionListItemCssClass = "AutoCompleteExtender_CompletionListItem";
                    auComplete.CompletionListHighlightedItemCssClass = "AutoCompleteExtender_HighlightedItem";
                    auComplete.CompletionListCssClass = "AutoCompleteExtender_CompletionList";
                    txt4.NamingContainer.Controls.Add(auComplete);
                    txt4.Attributes["onchange"] = "javascript: TextChangedKushtPagese('" + txt4.ClientID + "'," + e.VisibleIndex.ToString() + ");";
                    txt4.Attributes["onkeypress"] = "javascript: KeyPress('" + txt4.ClientID + "'," + e.VisibleIndex.ToString() + ");";
                    txt4.Attributes["onblur"] = "javascript: LostFocusKushtPagese('" + txt4.ClientID + "'," + e.VisibleIndex.ToString() + ");";
                    btn1.ClientSideEvents.Click = "function(s,e){ButtonClickKushtPagese('" + txt4.ClientID + "'," + e.VisibleIndex.ToString() + ");}";
                    if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 1 - nrRreshtash)
                    {
                        if (ugjet)
                        {
                            temptxtNormal = txt4;
                            ugjet = false;
                        }
                        else if (koloneFocus == "KushtPagese")
                        {
                            ugjet = true;
                        }
                    }
                }
            }

            if (tempcombo != null)
            {
                tempcombo.Focus();
            }
            else if (temptxt != null)
            {
                temptxt.Focus();
            }
        }

        /// <summary>
        /// Therret funksionin <see cref="formoColKushtePagese"/> kur behet nje callback i grides.
        /// </summary>
        protected void grid_trupi_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            formoColKushtePagese(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), e.Parameters.ToString());
        }

        protected void grid_trupi_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpNoRows"] = grid_trupi.VisibleRowCount;
        }

        void SetStyle(TableCell cell)
        {
            cell.Style[HtmlTextWriterStyle.TextOverflow] = "ellipsis";
            cell.Style[HtmlTextWriterStyle.Overflow] = "hidden";
            cell.Style[HtmlTextWriterStyle.WhiteSpace] = "nowrap";
        }

        protected void grid_trupi_HtmlFooterCellPrepared(object sender, ASPxGridViewTableFooterCellEventArgs e)
        {
            SetStyle(e.Cell);
        }




    }
}
