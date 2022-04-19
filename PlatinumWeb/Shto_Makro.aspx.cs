using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Drawing;
using DevExpress.Web;
using AjaxControlToolkit;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using PlatinumWeb.Templates;
using System.Globalization;
using System.Resources;



namespace PlatinumWeb
    {
    public partial class Shto_Makro : System.Web.UI.Page
        {
        TextBox temptxtNormal = null;
        private int nrRreshtatsh = 3;
        private string koloneFocus;
        ASPxTextBox temptxt = null;
        ASPxComboBox tempcombo = null;        
        private DbCore.DbInventari.clsKokaMakro MakroOverview;

        public static int idNdermVit = -1;


        protected void Page_Load(object sender, EventArgs e)
        {  // Prevent caching, so can't be viewed offline
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);

            //if (Session["LoggedIn"].Equals("No"))
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                Response.Redirect("login.aspx?arsye=FaqePaautorizuar");
            }
            int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            //if (Session["KodiNdermarrjes"] == null)
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerdoruesi);
            }
            idNdermVit = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            if (Page.IsPostBack == false)
            {
                EmrateTabeve();
                ASPxPageControl1.ActiveTabIndex = 0;
                konfiguroVleraFillestare(idNdermarrje, idPerdoruesi);
                konfiguroGride(idPerdoruesi, idNdermarrje);



                konfiguroGrideTrupash(idNdermarrje);
                //container per faqet e popupve
                container.Attributes["width"] = "400px";
                container.Attributes["height"] = "450px";

            }

            percaktoTemplateTrupash();

        }

        /// <summary>
        /// Vendos emrat e tabeve ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateTabeve()
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            ASPxPageControl1.TabPages[0].Text = rm.GetString("labelAdministrimiTePergjithshme", cultinf);
            ASPxPageControl1.TabPages[1].Text = rm.GetString("produktetTab", cultinf);
        }


        private void konfiguroVleraFillestare(int idNdermarrje, int idPerodoruesi)
        { //mbush komboboxet dhe gridat e faqes

            inicializoObjekte();

            mbushListeMakrosh(idPerodoruesi, idNdermarrje);
            mbushListeTrupash();

        }

        private void konfiguroGrideTrupash(int idNdermarrje)
        {//konfigurohet grida
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            DbCore.clsFunksione.percaktoVisibleColumnsMeWidth(DbCore.mySessionObjects.ktheGjuhePerdoruesi(Session), idNdermarrje, gvTrupi, "gvTrupi", "Shto_Makro.aspx");
            DbCore.clsFunksione.konfiguroGrideRegjistrimEvogelPaTheme(gvTrupi, "IdTrupiMakro", cultinf, rm);


        }

        private void inicializoObjekte()
        {            
        }

        private void mbushListeMakrosh(int idPerodoruesi, int idNdermarrje)
        {//mbush griden me te dhena            
            
            DbCore.DbInventari.colKokatMakro col = new DbCore.DbInventari.colKokatMakro(idNdermarrje, idPerodoruesi);
            //DbCore.DbInventari.colKokatMakro col = dbInventari.merrMakroSipasNdermarrjesAndAutorizime(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), oPerdorues.IdPerdorues);
            gvMakro.DataSource = col;
            gvMakro.DataBind();
        }

        private void mbushListeTrupash()
        {//mbushet grida me te dhena            
            DbCore.DbInventari.colTrupatMakro col = new DbCore.DbInventari.colTrupatMakro();
            for (int i = 0; i < 3; i++)
            {
                DbCore.DbInventari.clsTrupiMakro o = new DbCore.DbInventari.clsTrupiMakro();

                col.Add(o);
            }
            gvTrupi.DataSource = col;
            gvTrupi.DataBind();
        }


        private void percaktoTemplateTrupash()
        {//percaktohen templatet per fushat e grides
            GridViewDataTextColumn col0 = gvTrupi.Columns["Fshi"] as GridViewDataTextColumn;
            col0.DataItemTemplate = new MyButtonTemplate("");
            col0.VisibleIndex = 0;
            GridViewDataTextColumn col1 = gvTrupi.Columns["KodProdukti"] as GridViewDataTextColumn;
            col1.DataItemTemplate = new MyTemplateLupa();
            GridViewDataTextColumn col2 = gvTrupi.Columns["Pershkrimi"] as GridViewDataTextColumn;
            col2.DataItemTemplate = new MyTextTemplate();
            GridViewDataTextColumn col3 = gvTrupi.Columns["IdLlojMakro"] as GridViewDataTextColumn;
            col3.DataItemTemplate = new MyComboTemplate();
            GridViewDataTextColumn col4 = gvTrupi.Columns["IdFunksionMakro"] as GridViewDataTextColumn;
            col4.DataItemTemplate = new MyComboTemplate();
            GridViewDataTextColumn col5 = gvTrupi.Columns["Vlera"] as GridViewDataTextColumn;
            col5.DataItemTemplate = new MyDoubleTemplate(false, 2, "0"); // "0.00");
            GridViewDataTextColumn col6 = gvTrupi.Columns["Renditja"] as GridViewDataTextColumn;
            col6.DataItemTemplate = new MyComboTemplate();

        }



        private DbCore.DbInventari.colTrupatMakro ruajTrupat(int idNdermarrje)
        {//ruhet collectioni i kontakteve sipas te dhenave te futura nga perdoruesi
            

            DbCore.DbInventari.colTrupatMakro trupat = new DbCore.DbInventari.colTrupatMakro();
            DbCore.DbInventari.clsTrupiMakro trupi;
            int rreshta = gvTrupi.VisibleRowCount + 1;
            string initVal = this.hfLloji.Value;
            string[] pars1 = initVal.Split(',');
            string initVal2 = this.hfProdukti.Value;
            string[] pars3 = initVal2.Split(',');
            string initVal3 = this.hfPershkrimi.Value;
            string[] pars5 = initVal3.Split(',');
            string initVal4 = this.hfFunksioni.Value;
            string[] pars7 = initVal4.Split(',');
            string initVal5 = this.hfVlera.Value;
            string[] pars9 = initVal5.Split(',');
            string initVal6 = this.hfRenditja.Value;
            string[] pars11 = initVal6.Split(',');


            string[] lloji = new string[rreshta];
            string[] produkti = new string[rreshta];
            string[] pershkrimi = new string[rreshta];
            string[] funksioni = new string[rreshta];
            string[] vlera = new string[rreshta];
            string[] renditja = new string[rreshta];

            if (initVal != "")//merren te dhenat e hiden fieldeve te trupave te fleteve kontabel nga javascipti
            {
                for (int i = 0; i < pars1.Length; i++)
                {
                    string[] pars2 = pars1[i].Split(':');
                    lloji[Convert.ToInt32(pars2[0])] = pars2[1];
                }
            }
            if (initVal2 != "")
            {
                for (int i = 0; i < pars3.Length; i++)
                {
                    string[] pars4 = pars3[i].Split(':');
                    produkti[Convert.ToInt32(pars4[0])] = pars4[1];
                }
            }
            if (initVal3 != "")
            {
                for (int i = 0; i < pars5.Length; i++)
                {
                    string[] pars6 = pars5[i].Split(':');
                    pershkrimi[Convert.ToInt32(pars6[0])] = pars6[1];
                }
            }
            if (initVal4 != "")
            {
                for (int i = 0; i < pars7.Length; i++)
                {
                    string[] pars8 = pars7[i].Split(':');
                    funksioni[Convert.ToInt32(pars8[0])] = pars8[1];
                }
            }
            if (initVal5 != "")
            {
                for (int i = 0; i < pars9.Length; i++)
                {
                    string[] pars10 = pars9[i].Split(':');
                    vlera[Convert.ToInt32(pars10[0])] = pars10[1];
                }
            }
            if (initVal6 != "")
            {
                for (int i = 0; i < pars11.Length; i++)
                {
                    string[] pars12 = pars11[i].Split(':');
                    renditja[Convert.ToInt32(pars12[0])] = pars12[1];
                }
            }

            for (int i = 0; i < rreshta - 1; i++)//krijohet kolectioni me trupat e fleteve kontabel e futura nga perdoruesi
            {
                trupi = new DbCore.DbInventari.clsTrupiMakro();
                if (lloji[i] != null && lloji[i] != "null" && lloji[i] != "")
                {
                    trupi.IdLlojMakro = int.Parse(lloji[i]);
                    if (produkti[i] != null && produkti[i] != "null" && produkti[i] != "")
                    {
                        if (lloji[i] == "1")
                            trupi.IdProdukti = DbCore.DbInventari.clsArtikulli.ktheIdArtikulli(produkti[i], idNdermarrje);
                        //trupi.IdProdukti = dbInventari.ktheArtikull(produkti[i], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session))[0].IdArtikulli;
                        else
                            if (lloji[i] == "2")
                                trupi.IdProdukti = DbCore.DbInventari.clsKokaMakro.ktheIdKokaMakro(produkti[i], idNdermarrje);
                            //trupi.IdProdukti = dbInventari.ktheKokaMakro(produkti[i], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session))[0].IdKokaMakro;
                            else
                                if (lloji[i] == "3")
                                    trupi.IdProdukti = 0;
                    }
                    if (pershkrimi[i] != null && pershkrimi[i] != "null" && pershkrimi[i] != "")
                        trupi.Pershkrimi = pershkrimi[i];
                    if (funksioni[i] != null && funksioni[i] != "null" && funksioni[i] != "")
                        trupi.IdFunksionMakro = int.Parse(funksioni[i]);
                    if (vlera[i] != null && vlera[i] != "null" && vlera[i] != "")
                        trupi.Vlera = decimal.Parse(vlera[i]);
                    if (renditja[i] != null && renditja[i] != "null" && renditja[i] != "")
                        trupi.Renditja = int.Parse(renditja[i]);


                    trupat.Add(trupi);
                }
            }
            return trupat;
        }

        private void konfiguroGride(int idPerdoruesi, int idNdermarrje)
        {//konfiguron griden
            shto_Autorizim(idNdermarrje);

            DbCore.clsFunksione.percaktoVisibleColumnsShto(DbCore.mySessionObjects.ktheGjuhePerdoruesi(Session), idNdermarrje, gvMakro, "gvMakro", "Shto_Makro.aspx");
            //funksione.percaktoAtributeTeGridesShto(gvMakro, "IdKokaMakro");
            DbCore.clsFunksione.percaktoAtributeTeGridesShtoPaTheme(gvMakro, "IdKokaMakro");


            mbushListeMakrosh(idPerdoruesi, idNdermarrje);

        }
        private void shto_Autorizim(int idNdermarrje)
        {//shtohen komboja me Autorizimeve tek grida e KPF


            gvMakro.Columns.Remove(gvMakro.Columns["IdNivelAutorizimi"]);

            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            DbCore.DbAdmin.colAutorizimetKoka colAutorizim = new DbCore.DbAdmin.colAutorizimetKoka();
            colAutorizim.mbushGjitheAutorizimet(idNdermarrje);
            //DbCore.DbAdmin.colAutorizimetKoka colAutorizim = new DbCore.DbAdmin.colAutorizimetKoka();
            //colAutorizim = dbAdmin.merrGjitheAutorizimet();
            colnew.PropertiesComboBox.DataSource = colAutorizim;
            colnew.PropertiesComboBox.TextField = "KodiAutorizim";
            colnew.PropertiesComboBox.ValueField = "IdAutorizimKoka";
            colnew.FieldName = "IdNivelAutorizimi";
            gvMakro.Columns.Add(colnew);
        }



        protected void ruaj_Button_Click(object sender, EventArgs e)
        {
            ruajMakro();
        }

        //sherben per te ruajtur nje makro
        private void ruajMakro()
        {
            DbCore.DbInventari.clsKokaMakro makro;
            
            if (Page.IsValid == false)
                return;
            else
            {
                int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                if (isValidMakro(idNdermarrje))
                {
                    int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                    makro = krijoMakro(idPerdoruesi, idNdermarrje);
                    DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                    mesazh = makro.ruajMakro(makro.IdKokaMakro, makro.KodiKokaMakro, makro.PershkrimiKokaMakro, makro.IdPerdoruesi, makro.IdNdermarje, makro.OColTrupatMakro, makro.IdStatusDok);
                    //mesazh = makro.ruaj();
                    if (!mesazh.Status == true)
                    {
                        pergjigja.Text = mesazh.PershkrimMesazhi;//"Ndodhi nje gabim. Ruajtja nuk u krye!";                        
                        mbushListeMakrosh(idPerdoruesi, idNdermarrje);
                        percaktoTamplateMakro();
                    }
                    else
                    {
                        pergjigja.Text = "Ruajtja perfundoi me sukses!";
                        Response.Redirect("Makro.aspx?ruaj=ok&indexrow=" + gvMakro.VisibleRowCount);
                    }
                }
            }
        }

        private void ruajMakroOverview()
        {//ben ruajtjen vetem me te dhenat e grides


            if (Page.IsValid == false)
                return;
            else
            {
                gvMakro.UpdateEdit();
                int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                if (isValidMakroOverview(idNdermarrje))
                {
                    DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                    mesazh = MakroOverview.ruajMakro(MakroOverview.IdKokaMakro, MakroOverview.KodiKokaMakro, MakroOverview.PershkrimiKokaMakro, MakroOverview.IdPerdoruesi, MakroOverview.IdNdermarje, MakroOverview.OColTrupatMakro, MakroOverview.IdStatusDok);
                    //mesazh = MakroOverview.ruaj();
                    if (!mesazh.Status == true)
                    {
                        pergjigja.Text = "Ndodhi nje gabim. Ruajtja nuk u krye!";

                        percaktoTamplateMakro();


                    }
                    else
                    {
                        pergjigja.Text = "Ruajtja perfundoi me sukses!";
                        gvMakro.AddNewRow();
                        pastroFusha();
                    }

                }

                mbushListeMakrosh(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
            }
        }

        private void pastroFusha()
        {//pastron fushat
            pergjigja.Text = "";
            gvMakro.AddNewRow();
            this.txtKodi2.Text = "";
            this.txtPershkrimi2.Text = "";
            this.hfAutorizime.Value = "";
            mbushListeTrupash();
            HiddenField1.Value = "";
            this.hfFunksioni.Value = "";
            this.hfLloji.Value = "";
            this.hfPershkrimi.Value = "";
            this.hfProdukti.Value = "";
            this.hfRenditja.Value = "";
            this.hfVlera.Value = "";

        }

        private DbCore.DbInventari.clsKokaMakro krijoMakro(int idPerdoruesi, int idNdermarrje)
        {//krijon nje makro sipas te dhenave te futura nga perdoruesi
            

            DbCore.DbInventari.clsKokaMakro makro = new DbCore.DbInventari.clsKokaMakro();
            //makro.IdNderViti = idNdermVit;
            makro.KodiKokaMakro = txtKodi2.Text;
            makro.PershkrimiKokaMakro = txtPershkrimi2.Text;

            makro.IdPerdoruesi = idPerdoruesi;
            makro.IdNdermarje = idNdermarrje;
            makro.IdNivelAutorizimi = hfAutorizime.Value;
            makro.OColTrupatMakro = ruajTrupat(idNdermarrje);
            makro.IdStatusDok = 1;
            return makro;
        }

        //kontrollon nese te dhenat qe jane plotesuara jane 
        //te lejueshme apo jo
        private bool isValidMakro(int idNdermarrje)
        {
            bool isValid;
            isValid = true;

            DbCore.DbInventari.clsDatabaseInventari dbInventari = new DbCore.DbInventari.clsDatabaseInventari();

            if (dbInventari.ekzistonMakro(txtKodi2.Text, idNdermarrje))
            {
                isValid = false;
                pergjigja.ForeColor = Color.Red;
                pergjigja.Text = "Ekziston nje makro me kete kod!Ju lutemi shenoni nje kod tjeter.";
                percaktoTamplateMakro();               
            }
            dbInventari.Dispose();
            return isValid;
        }

        ////kontrollon nese llogaria ekziston
        private bool isValidMakroOverview(int idNdermarrje)
        {
            bool isValid;
            isValid = true;
            

            if (MakroOverview == null)
            {
                isValid = false;
                percaktoTamplateMakro();
            }
            else
            {
                DbCore.DbInventari.clsDatabaseInventari dbInventari = new DbCore.DbInventari.clsDatabaseInventari();
                if (dbInventari.ekzistonMakro(MakroOverview.KodiKokaMakro, idNdermarrje))
                {
                    isValid = false;
                    pergjigja.ForeColor = Color.Red;
                    pergjigja.Text = "Ekziston nje makro me kete kod!Ju lutemi shenoni nje kod tjeter.";
                    percaktoTamplateMakro();
                    dbInventari.Dispose();
                    return isValid;
                }
                dbInventari.Dispose();
            }
            return isValid;
        }

        protected void gvMakro_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {//merr te dhenat e rreshtit te ri te grides

            int i;
            for (i = 0; i < e.NewValues.Count; i++)
            {
                if (i == 2)
                    continue;
                if (e.NewValues[i] == null)
                {
                    e.Cancel = true;
                    return;
                }
            }

            MakroOverview = new DbCore.DbInventari.clsKokaMakro();            
            MakroOverview.KodiKokaMakro = e.NewValues["KodiKokaMakro"].ToString();
            MakroOverview.PershkrimiKokaMakro = e.NewValues["PershkrimiKokaMakro"].ToString();
            //MakroOverview.IdNderViti = idNdermVit;
            MakroOverview.IdNivelAutorizimi = hfAutorizime.Value;
            MakroOverview.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            MakroOverview.IdNdermarje = idNdermarrje;
            MakroOverview.OColTrupatMakro = ruajTrupat(idNdermarrje);
            MakroOverview.IdStatusDok = 1;
            e.Cancel = true;
        }

        protected void ASPxPageControl1_ActiveTabChanged(object source, DevExpress.Web.TabControlEventArgs e)
        {
        }

        protected void overview_Button_Click(object sender, EventArgs e)
        {//ben ruajtjen e shpejte
            ruajMakroOverview();
        }

        protected void gvMakro_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {//thirret kur grida ben callback
            mbushListeMakrosh(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            percaktoTamplateMakro();
            }


        //therritet kur therritet UpdateEdit dhe kontrollon nese ka ndonje kolone te grides ne rreshtin e ri te paplotesuar
        protected void gvMakro_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
            {
            string initVal = HiddenField1.Value;
            string[] pars1 = initVal.Split(';');
            foreach (GridViewColumn column in gvMakro.Columns)
                {
                if (column.Visible == true)
                    {
                    GridViewDataColumn dataColumn = column as GridViewDataColumn;
                    if (dataColumn == null) continue;
                    ////if (dataColumn.FieldName == "IdNivelAutorizimi")
                    ////    if (String.IsNullOrEmpty(hfAutorizime.Value))
                    ////        e.Errors[dataColumn] = "Vlera nuk mund te jete null.";

                    if (e.NewValues[dataColumn.FieldName] == null && dataColumn.FieldName != "IdNivelAutorizimi")//validimi per kolonat e tjera te detyrueshme
                        {
                        e.Errors[dataColumn] = "Vlera nuk mund te jete null.";
                        }
                    }
                }
            if (e.Errors.Count > 0)
                {
                e.RowError = "Ju lutemi, plotesoni te gjitha fushat.";
                percaktoTamplateMakro();
                }

            if (string.IsNullOrEmpty(e.RowError) && e.Errors.Count > 0)
                {
                e.RowError = "Ju lutemi, korrigjoni te gjithe gabimet.";
                percaktoTamplateMakro();
                }
            }

        ////shton ne nje collection te tipit Dictionary nje error.
        void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
            {
            if (errors.ContainsKey(column)) return;
            errors[column] = errorText;
            }

        protected void gvMakro_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
            {
            if (!gvMakro.IsNewRowEditing)
                {
                gvMakro.DoRowValidation();
                }
            }

        protected void pastro_Button_Click(object sender, EventArgs e)
            {
            pastroFusha();
            }

        protected void overview_pastro_ASPxButton_Click(object sender, EventArgs e)
            {
            pastroFusha();
            }

        protected void gvMakro_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
            {
            }

        protected void gvMakro_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
            {//funksioni qe theret javascriptin per kalimin e te dhenave nga grida tek textboxet e tabeve te tjera

            if (e.Editor.GetType().Name == "ASPxTextBox")
                {
                ASPxTextBox currentEditor = e.Editor as ASPxTextBox;
                currentEditor.ClientSideEvents.TextChanged = "function(s,e){ProcessTextCahnged('" + e.Column.FieldName + "',s.GetText());}";
                }
            else if (e.Editor.GetType().Name == "ASPxComboBox")
                {
                ASPxComboBox currentEditor = e.Editor as ASPxComboBox;
                currentEditor.ClientSideEvents.TextChanged = "function(s,e){ProcessTextCahnged('" + e.Column.FieldName + "',s.GetText());}";
                }
            }

        private void percaktoTamplateMakro()
            {//tempatet per kolonat e Autorizimeve
            GridViewDataComboBoxColumn col7 = gvMakro.Columns["IdNivelAutorizimi"] as GridViewDataComboBoxColumn;
            col7.EditItemTemplate = new MyTemplateAutorizime(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            col7.Width = 100;


            }

        protected void gvMakro_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
            {
            percaktoTamplateMakro();
            }



        protected void gvTrupi_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
            {
            gvTrupi.DataBind();
            }

        protected void gvTrupi_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
            {//kur grida ben callback te ruajme te dhenat
            int key = -1;

            if (e.Parameters.ToString() != "")
                {
                key = int.Parse(e.Parameters.ToString());
                }            

            DbCore.DbInventari.colTrupatMakro trupat = new DbCore.DbInventari.colTrupatMakro();
            DbCore.DbInventari.clsTrupiMakro trupi;
            int rreshta = gvTrupi.VisibleRowCount + 1;
            string initVal = this.hfLloji.Value;
            string[] pars1 = initVal.Split(',');
            string initVal2 = this.hfProdukti.Value;
            string[] pars3 = initVal2.Split(',');
            string initVal3 = this.hfPershkrimi.Value;
            string[] pars5 = initVal3.Split(',');
            string initVal4 = this.hfFunksioni.Value;
            string[] pars7 = initVal4.Split(',');
            string initVal5 = this.hfVlera.Value;
            string[] pars9 = initVal5.Split(',');
            string initVal6 = this.hfRenditja.Value;
            string[] pars11 = initVal6.Split(',');


            string[] lloji = new string[rreshta];
            string[] produkti = new string[rreshta];
            string[] pershkrimi = new string[rreshta];
            string[] funksioni = new string[rreshta];
            string[] vlera = new string[rreshta];
            string[] renditja = new string[rreshta];

            if (initVal != "")//merren te dhenat e hiden fieldeve te trupave te fleteve kontabel nga javascipti
                {
                for (int i = 0; i < pars1.Length; i++)
                    {
                    string[] pars2 = pars1[i].Split(':');
                    lloji[Convert.ToInt32(pars2[0])] = pars2[1];
                    }
                }
            if (initVal2 != "")
                {
                for (int i = 0; i < pars3.Length; i++)
                    {
                    string[] pars4 = pars3[i].Split(':');
                    produkti[Convert.ToInt32(pars4[0])] = pars4[1];
                    }
                }
            if (initVal3 != "")
                {
                for (int i = 0; i < pars5.Length; i++)
                    {
                    string[] pars6 = pars5[i].Split(':');
                    pershkrimi[Convert.ToInt32(pars6[0])] = pars6[1];
                    }
                }
            if (initVal4 != "")
                {
                for (int i = 0; i < pars7.Length; i++)
                    {
                    string[] pars8 = pars7[i].Split(':');
                    funksioni[Convert.ToInt32(pars8[0])] = pars8[1];
                    }
                }
            if (initVal5 != "")
                {
                for (int i = 0; i < pars9.Length; i++)
                    {
                    string[] pars10 = pars9[i].Split(':');
                    vlera[Convert.ToInt32(pars10[0])] = pars10[1];
                    }
                }
            if (initVal6 != "")
                {
                for (int i = 0; i < pars11.Length; i++)
                    {
                    string[] pars12 = pars11[i].Split(':');
                    renditja[Convert.ToInt32(pars12[0])] = pars12[1];
                    }
                }

            for (int i = 0; i < rreshta - 1; i++)//krijohet kolectioni me trupat e fleteve kontabel e futura nga perdoruesi
                {
                trupi = new DbCore.DbInventari.clsTrupiMakro();
                if (lloji[i] != null && lloji[i] != "null" && lloji[i] != "")
                    {
                    trupi.IdLlojMakro = int.Parse(lloji[i]);
                    }
                if (produkti[i] != null && produkti[i] != "null" && produkti[i] != "")
                    {
                        if (lloji[i] == "1")
                            trupi.IdProdukti = DbCore.DbInventari.clsArtikulli.ktheIdArtikulli(produkti[i], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    else
                        if (lloji[i] == "2")
                            trupi.IdProdukti = DbCore.DbInventari.clsKokaMakro.ktheIdKokaMakro(produkti[i], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                            //trupi.IdProdukti = dbInventari.ktheKokaMakro(produkti[i], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session))[0].IdKokaMakro;
                        else
                            if (lloji[i] == "3")
                                trupi.IdProdukti = 0;
                    trupi.KodProdukti = produkti[i];
                    }
                if (pershkrimi[i] != null && pershkrimi[i] != "null" && pershkrimi[i] != "")
                    trupi.Pershkrimi = pershkrimi[i];
                if (funksioni[i] != null && funksioni[i] != "null" && funksioni[i] != "")
                    trupi.IdFunksionMakro = int.Parse(funksioni[i]);
                if (vlera[i] != null && vlera[i] != "null" && vlera[i] != "")
                    trupi.Vlera = decimal.Parse(vlera[i]);
                if (renditja[i] != null && renditja[i] != "null" && renditja[i] != "")
                    trupi.Renditja = int.Parse(renditja[i]);


                trupat.Add(trupi);

                }
            if (key != -1)
                trupat.RemoveAt(key);
            else
                {
                DbCore.DbInventari.clsTrupiMakro trup = new DbCore.DbInventari.clsTrupiMakro();
                trupat.Add(trup);
                }
            if (trupat.Count == 0)
                {
                DbCore.DbInventari.clsTrupiMakro trup = new DbCore.DbInventari.clsTrupiMakro();
                trupat.Add(trup);
                }
            this.gvTrupi.DataSource = trupat;
            this.gvTrupi.DataBind();
            percaktoTemplateTrupash();
            percaktoTamplateMakro();
            }

        protected void gvTrupi_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
            {
            e.Properties["cpNoRows"] = gvTrupi.VisibleRowCount;
            }

        protected void gvTrupi_DataBound(object sender, EventArgs e)
            {//shton butonin fshi
            if (this.gvTrupi.Columns["Fshi"] == null)
                {
                GridViewDataTextColumn fshi = new GridViewDataTextColumn();
                fshi.Caption = "Fshi";
                fshi.Width = 50;
                gvTrupi.Columns.Add(fshi);

                gvTrupi.KeyFieldName = "IdTrupiMakro";
                gvTrupi.SettingsBehavior.AllowSelectByRowClick = false;
                gvTrupi.SettingsBehavior.AllowFocusedRow = true;
                }
            }

        protected void gvTrupi_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
            {//krijon rreshat sipas modelit
            bool ugjet;
            if (e.RowType == DevExpress.Web.GridViewRowType.Data)
                {
                GridViewDataTextColumn col0 = ((ASPxGridView)sender).Columns["Fshi"] as GridViewDataTextColumn;
                GridViewDataTextColumn col1 = ((ASPxGridView)sender).Columns["IdLlojMakro"] as GridViewDataTextColumn;
                GridViewDataTextColumn col2 = ((ASPxGridView)sender).Columns["KodProdukti"] as GridViewDataTextColumn;
                GridViewDataTextColumn col3 = ((ASPxGridView)sender).Columns["Pershkrimi"] as GridViewDataTextColumn;
                GridViewDataTextColumn col4 = ((ASPxGridView)sender).Columns["IdFunksionMakro"] as GridViewDataTextColumn;
                GridViewDataTextColumn col5 = ((ASPxGridView)sender).Columns["Vlera"] as GridViewDataTextColumn;
                GridViewDataTextColumn col7 = ((ASPxGridView)sender).Columns["Renditja"] as GridViewDataTextColumn;
                ASPxButton btn0 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col0, "btn") as ASPxButton;
                TextBox txt12 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col2, "txt") as TextBox;
                ASPxButton btn1 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col2, "btn") as ASPxButton;
                ASPxComboBox txt2 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col1, "cmbBox") as ASPxComboBox;
                ASPxTextBox txt3 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col3, "txtBox") as ASPxTextBox;
                ASPxComboBox cmb4 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col4, "cmbBox") as ASPxComboBox;
                ASPxTextBox txt5 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col5, "txtBox") as ASPxTextBox;
                ASPxComboBox txt7 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col7, "cmbBox") as ASPxComboBox;
                ugjet = false;                
                //vendosen client side eventet e kolonave
                if (btn0 != null)
                    {
                    btn0.ClientInstanceName = "btnFshi" + e.VisibleIndex.ToString();
                    btn0.ClientSideEvents.Click = "function(s,e){FshiClicked(" + e.VisibleIndex.ToString() + ");}";
                    }

                if (txt12 != null)
                    {
                    txt12.ID = "txtProdukti" + e.VisibleIndex.ToString();
                    AutoCompleteExtender auComplete = new AutoCompleteExtender();
                    auComplete.ServicePath = "wsfunc.asmx";
                    auComplete.ServiceMethod = "ktheArtikullOseMakro";
                    auComplete.MinimumPrefixLength = 1;
                    auComplete.EnableCaching = false;
                    auComplete.Enabled = true;
                    auComplete.UseContextKey = true;
                    auComplete.BehaviorID = "behaviortxtProdukti" + e.VisibleIndex.ToString();
                    auComplete.FirstRowSelected = true;
                    auComplete.TargetControlID = txt12.ID;
                    auComplete.CompletionInterval = 100;
                    auComplete.CompletionListItemCssClass = "AutoCompleteExtender_CompletionListItem";
                    auComplete.CompletionListHighlightedItemCssClass = "AutoCompleteExtender_HighlightedItem";
                    auComplete.CompletionListCssClass = "AutoCompleteExtender_CompletionList";
                    txt12.NamingContainer.Controls.Add(auComplete);
                    txt12.Attributes["onchange"] = "javascript: TextChangedProdukti('" + txt12.ClientID + "', txtPershkrimi" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + ");";
                    txt12.Attributes["onkeypress"] = "javascript: KeyPressProdukti('" + txt12.ClientID + "'," + e.VisibleIndex.ToString() + ");";
                    txt12.Attributes["onblur"] = "javascript: LostFocusProdukti('" + txt12.ClientID + "'," + e.VisibleIndex.ToString() + ");";
                    btn1.ClientSideEvents.Click = "function(s,e){ButtonClickedProdukti('" + txt12.ClientID + "'," + e.VisibleIndex.ToString() + ");}";
                    btn1.ClientInstanceName = "btnHap" + e.VisibleIndex.ToString();
                    if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 1 - nrRreshtatsh)
                        {
                        if (ugjet)
                            {
                            temptxtNormal = txt12;
                            ugjet = false;
                            }
                        else if (koloneFocus == "KodProdukti")
                            {
                            ugjet = true;
                            }
                        }
                    }
                if (txt2 != null)
                    {

                        DbCore.DbInventari.colLlojeMakrosh colLlojeMakrosh = new DbCore.DbInventari.colLlojeMakrosh();
                    colLlojeMakrosh.mbushGjitheLlojeMakro();
                    txt2.DataSource = colLlojeMakrosh;
                    //txt2.DataSource = dbInventari.merrGjitheLlojeMakrosh();
                    txt2.TextField = "PershkrimLlojMakro";
                    txt2.ValueField = "IdLlojMakro";
                    txt2.DataBind();

                    txt2.ClientInstanceName = "cmbLloji" + e.VisibleIndex.ToString();
                    txt2.ClientSideEvents.TextChanged = "function(s,e){TextChangedLloji(cmbLloji" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + ");}";
                    if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 1 - nrRreshtatsh)
                        {
                        if (ugjet)
                            {
                            tempcombo = txt2;
                            ugjet = false;
                            }
                        else if (koloneFocus == "IdLlojMakro")
                            {
                            ugjet = true;
                            }
                        }
                    }
                if (txt3 != null)
                    {
                    txt3.ClientInstanceName = "txtPershkrimi" + e.VisibleIndex.ToString();
                    txt3.ClientSideEvents.TextChanged = "function(s,e){TextChangedPershkrimi(txtPershkrimi" + e.VisibleIndex.ToString() + ", " + e.VisibleIndex.ToString() + ");}";

                    if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 1 - nrRreshtatsh)
                        {
                        if (ugjet)
                            {
                            temptxt = txt3;
                            ugjet = false;
                            }
                        else if (koloneFocus == "Pershkrimi")
                            {
                            ugjet = true;
                            }
                        }
                    }
                if (cmb4 != null)
                    {

                    DbCore.DbInventari.colFunksioneMakrosh makroFunks = new DbCore.DbInventari.colFunksioneMakrosh();
                    makroFunks.mbushGjitheFunksioneMakrosh();
                    cmb4.DataSource = makroFunks; 
                    //cmb4.DataSource = dbInventari.merrGjitheFunksioneMakrosh();
                    cmb4.TextField = "PershkrimFunksionMakro";
                    cmb4.ValueField = "IdFunksionMakro";
                    cmb4.DataBind();

                    cmb4.ClientInstanceName = "cmbFunksioni" + e.VisibleIndex.ToString();
                    cmb4.ClientSideEvents.TextChanged = "function(s,e){TextChangedFunksioni(cmbFunksioni" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + ");}";
                    if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 1 - nrRreshtatsh)
                        {
                        if (ugjet)
                            {
                            tempcombo = cmb4;
                            ugjet = false;
                            }
                        else if (koloneFocus == "IdFunksioni")
                            {
                            ugjet = true;
                            }
                        }
                    }
                if (txt5 != null)
                    {
                    txt5.ClientInstanceName = "txtVlera" + e.VisibleIndex.ToString();
                    txt5.ClientSideEvents.TextChanged = "function(s,e){TextChangedVlera(txtVlera" + e.VisibleIndex.ToString() + ", " + e.VisibleIndex.ToString() + ");}";

                    if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 1 - nrRreshtatsh)
                        {
                        if (ugjet)
                            {
                            temptxt = txt5;
                            ugjet = false;
                            }
                        else if (koloneFocus == "Vlera")
                            {
                            ugjet = true;
                            }
                        }
                    }
                if (txt7 != null)
                    {

                    txt7.Items.Add("1", 1);
                    txt7.Items.Add("2", 2);
                    txt7.Items.Add("3", 3);
                    txt7.Items.Add("4", 4);
                    txt7.Items.Add("5", 5);
                    txt7.Items.Add("6", 6);
                    txt7.Items.Add("7", 7);
                    txt7.Items.Add("8", 8);
                    txt7.Items.Add("9", 9);
                    txt7.Items.Add("10", 10);
                    txt7.Items.Add("11", 11);
                    txt7.Items.Add("12", 12);
                    txt7.Items.Add("13", 13);
                    txt7.Items.Add("14", 14);
                    txt7.Items.Add("15", 15);
                    txt7.Items.Add("16", 16);
                    txt7.Items.Add("17", 17);
                    txt7.Items.Add("18", 18);
                    txt7.Items.Add("19", 19);
                    txt7.Items.Add("20", 20);


                    txt7.DataBind();

                    txt7.ClientInstanceName = "cmbRenditja" + e.VisibleIndex.ToString();
                    txt7.ClientSideEvents.TextChanged = "function(s,e){TextChangedRenditja(cmbRenditja" + e.VisibleIndex.ToString() + "," + e.VisibleIndex.ToString() + ");}";
                    if (e.VisibleIndex == ((ASPxGridView)sender).VisibleRowCount - 1 - nrRreshtatsh)
                        {
                        if (ugjet)
                            {
                            tempcombo = txt7;
                            ugjet = false;
                            }
                        else if (koloneFocus == "Renditja")
                            {
                            ugjet = true;
                            }
                        }
                    }
                }

            if (temptxt != null)
                {
                temptxt.Focus();
                }

            }

    
        }
    }
