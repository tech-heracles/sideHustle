using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Collections;
using System.Drawing;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using PlatinumWeb.Templates;
using System.Web.Script.Serialization;
using System.Globalization;
using System.Resources;
using DbCore.IMBUtils.Types;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;


namespace PlatinumWeb
{
    public partial class FormatNumrash : MyPageBase
    {
        private  string mesazhFshirjeSukses = "Fshirja perfundoi me sukses!";
        private  string mesazhFshirjeGabim = "Ndodhi nje gabim gjate fshirjes!";
        private  string mesazhRuajtjeSukses = "Ruajtja perfundoi me sukses!:Green";
        private  string mesazhRuajtjeGabim = "Ndodhi nje gabim gjate ruajtjes se te dhenave!:Red";
        public static int idNdermVit = -1;
        private string komponente = "FormatNumrash.aspx";
        private string guidString;
        ASPxTextBox temptxt = null;
        ASPxComboBox tempcombo = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            int idPerdoruesi;
            int idGjuha;
            int idNdermarrje;
            int idViti;
            int idNdermarrjeVit;
            if (hfState.Count == 0)
            {
                if (!DbCore.mySessionObjects.isLogedIn(Session))
                {
                    DbCore.clsFunksione.logout(Session, true, "FaqePaautorizuar");
                }
                idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
                {
                    Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerdoruesi);
                }
                guidString = Guid.NewGuid().ToString();
                hfState.Set("guidString", guidString);
                idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
                idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
                idNdermarrjeVit = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
                hfState.Set("idPerdoruesi", idPerdoruesi);
                hfState.Set("idGjuha", idGjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idViti", idViti);
                hfState.Set("idNdermarrjeVit", idNdermarrjeVit);
                hfState.Set("Meme", DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
                hfState.Set("formatRi", false);
            }
            else
            {
                idPerdoruesi = (int)hfState["idPerdoruesi"];
                idGjuha = (int)hfState["idGjuha"];
                idNdermarrje = (int)hfState["idNdermarrje"];
                idViti = (int)hfState["idViti"];
                idNdermarrjeVit = (int)hfState["idNdermarrjeVit"];
                guidString = (string)hfState.Get("guidString");
            }
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            percaktoTemplateMenu(ASPxMenu1, idViti, idPerdoruesi, idNdermarrje);
            clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, "gvFormati", 1, komponente);
            if (!Page.IsPostBack)
            {
                EmrateTabeve(cultinf, rm);
                konfiguroVleraFillestare(idPerdoruesi, idNdermarrje, idViti);
                mbushListeFormatesh(idNdermarrje);
                GridUtil.percaktoVisibleColumnsMeWidth(idGjuha, idNdermarrje, gvFormati, "gvFormati", komponente);
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, komponente);
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                EmraTeLabelave(cultinf, rm);
            }
            else
            {
                mbushListeFormateshNgaSesioni(idNdermarrje);
            }

            konfiguroGride();
            konfiguroGrideTrupi(idPerdoruesi, idNdermarrje);
            //percaktoTemplateTrupi(idNdermarrje, idPerdoruesi);
           
            AspxWebControlUtils.perkthePopUp(popFshi, rm.GetString("labelKujdes", cultinf), lblMsgbox, rm.GetString("labelAdministrimiMsgJeniSigurt", cultinf), ButtonCancel, rm.GetString("labelAnullo", cultinf));
        }

        /// <summary>
        /// Vendos emrat e tabeve ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateTabeve(CultureInfo cultinf, ResourceManager rm)
        {
               ASPxPageControl1.TabPages[0].Text = rm.GetString("TePergjithshmeTab", cultinf);
             ASPxPageControl1.TabPages[1].Text = rm.GetString("labelAdministrimiInformacion", cultinf);
        }

        private void EmraTeLabelave(CultureInfo cultinf, ResourceManager rm)
        {
            lblKodi.Text = rm.GetString("labelKodi", cultinf) + ":";
            lblEmertimi.Text = rm.GetString("lblEmertimi", cultinf);
            lblKategoria.Text = rm.GetString("labelKategoria", cultinf) + ":";

            mesazhFshirjeSukses = rm.GetString("msgFshirjaMeSukses", cultinf);
            mesazhFshirjeGabim = rm.GetString("msgGabimGjateFshirjes", cultinf);
            mesazhRuajtjeSukses = rm.GetString("mesazhRuajtjeMeSukses", cultinf) + ": Green";
            mesazhRuajtjeGabim = rm.GetString("msgNdodhiGabimGjateRuajtesSeTeDhenave", cultinf) + ": Red";
            hfState.Set("msgShtoArtikullZgjidhArt", rm.GetString("msgShtoArtikullZgjidhArt", cultinf));
            hfState.Set("mesazhRaportNukKeniTeDrejta", rm.GetString("mesazhRaportNukKeniTeDrejta", cultinf));  
        }
        private void konfiguroVleraFillestare(int idPerdoruesi, int idNdermarrje, int idViti)
        {
            ConfigureAspxComboBox.mbushComboKategoriPerFormatNumrash(Kategoria_ComboBox, idViti);
            mbushTrupiKonfig(idPerdoruesi, idNdermarrje);
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
            clsToolbarConfig.percaktoTemplateMenu((int)hfState["idGjuha"], idViti, idPerdorues, idNdermarrje, aSPxMenu1, komponente, this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, hfRuaj.Value == "Ruaj" ? true : false, true, false, (bool)hfState["Meme"]);
        }

        /// <summary>
        /// sherben per te ruajtur filtrin e zgjedhur
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            //kap item qe ka template ne menune e kesaj faqeje
            int idPerdoruesi = (int)hfState["idPerdoruesi"];
            int idGjuha = (int)hfState["idGjuha"];
            int idNdermarrje = (int)hfState["idNdermarrje"];
            int idViti = (int)hfState["idViti"];
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida();
            filtri.FiltraKodi = cmbFiltra.Text;
            filtri.FiltraShenime = cmbFiltra.Text;
            filtri.FiltraUniversal = false;
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuha, "gvFormati", komponente, idNdermarrje);
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvFormati.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("IdFormatKonfig", gvFormati);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvFormati.GetSortedColumns();
            //if (kolona.Count > 0)
            //{
            //    filtri.KoloneRenditje = kolona[0].FieldName;
            //    if (kolona[0].SortOrder == DevExpress.Data.ColumnSortOrder.Ascending)
            //        filtri.DrejtimRenditje = true;
            //    else
            //        filtri.DrejtimRenditje = false;
            //}
            //else
            //{
            //    filtri.KoloneRenditje = "IdFormatKonfig";
            //    filtri.DrejtimRenditje = true;
            //}
            DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();
            oPerdorues = DbCore.mySessionObjects.kthePerdorues(Session);
            filtri.IdPerdoruesi = oPerdorues.IdPerdorues;
            filtri.IdNdermarje = idNdermarrje;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            filtri.IdStatusDok = 1;

            mesazh = filtri.ruaj();
            clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, "gvFormati",1, komponente);
            percaktoTemplateMenu(ASPxMenu1, idViti, idPerdoruesi, idNdermarrje);

            if (mesazh.Status == true)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("mesazhRuajtjeMeSukses", cultinf), pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            cmbFiltra.Text = "";
        }

        /// <summary>
        /// fshin filtrin e zgjedhur dhe aplikuar mbi gride, fshirje nga DB ku ai eshte ruajtur, dhe jo pastrimi i grides nga aplikimi i filtrit
        /// per kete do sherbeje Clear Filter ne fund te grides
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            int idPerdoruesi = (int)hfState["idPerdoruesi"];
            int idGjuha = (int)hfState["idGjuha"];
            int idNdermarrje = (int)hfState["idNdermarrje"];
            int idViti = (int)hfState["idViti"];
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuha, "gvFormati", komponente, idNdermarrje);
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                filtra.IdPerdoruesi = idPerdoruesi;
                mesazh = filtra.fshi();
                clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, "gvFormati", 1, komponente);
                percaktoTemplateMenu(ASPxMenu1, idViti, idPerdoruesi, idNdermarrje);
                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                cmbFiltra.Text = "";
                gvFormati.FilterExpression = String.Empty;
            }
        }

        private void konfiguroGride()
        {//konfigurohet grida
            GridUtil.konfigGrideListeEMadhePaTheme(gvFormati, "IdFormatKonfig");
            //KonfigurimComboGride.ShtoKategoriNivelDokPerFormatNumrash(gvFormati, Session, komponente, guidString, "IdKategoria");
        }

        /// <summary>
        /// shton monedhen ne gride dhe percakton template e tij
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="idPerdorues"></param>
        private void shtoMonedha(int idNdermarrje, int idPerdorues)
        {
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            if (typeof(GridViewDataComboBoxColumn) != gvTrupiFormatNr.Columns["IdMonedha"].GetType())
            {
                gvTrupiFormatNr.Columns.Remove(gvTrupiFormatNr.Columns["IdMonedha"]);
                gvTrupiFormatNr.Columns.Add(colnew);
                DbCore.DbAdmin.colMonedhat colMonedha = new DbCore.DbAdmin.colMonedhat();
                colMonedha.Add(new DbCore.DbAdmin.clsMonedha(0, "", "", false, 0, 0, 0, 0, 0, new DbCore.DbAdmin.colLidhjetAutorizim(), 0));
                colMonedha.mbushGjitheMonedhatAktive(idNdermarrje, idPerdorues);
                colnew.PropertiesComboBox.DataSource = colMonedha;
                colnew.PropertiesComboBox.TextField = "KodiMonedha";
                colnew.PropertiesComboBox.ValueField = "IdMonedha";
                colnew.FieldName = "IdMonedha";
                DbCore.mySessionObjects.ruajDsComboGrideNeSession(Session, colnew.PropertiesComboBox.Items, "colMonedha");
            }
            else
            {
                colnew = (GridViewDataComboBoxColumn)gvTrupiFormatNr.Columns["IdMonedha"];
                if (colnew.PropertiesComboBox.Items.Count == 0)
                {
                    colnew.PropertiesComboBox.Items.AddRange((ListEditItemCollection)DbCore.mySessionObjects.merrDsComboGrideNeSession(Session, "colMonedha"));
                }
            }
        }

 
        /// <summary>
        /// mbush griden me formatet e ruajtura ne db.
        /// </summary>
        private void mbushListeFormatesh(int idNdermarrje)
        {//mbushet grida me te dhena
            DataTable dt = DbCore.DbShare.colFormatKonfig.merrGjitheFormatetSipasNdermarrjes(idNdermarrje);
            DbCore.mySessionObjects.ruajGrideNeSession(Session, dt);
            gvFormati.DataSource = dt;
            gvFormati.DataBind();
        }

        /// <summary>
        /// mbush griden me formatet e ruajtura ne sesion.
        /// </summary>
        private void mbushListeFormateshNgaSesioni(int idNdermarrje)
        {
            DataTable tmpObject;
            bool sukses = DbCore.mySessionObjects.merrGrideNgaSessioni(Session, out tmpObject);
            if (!sukses)
                mbushListeFormatesh(idNdermarrje);
            else
            {
                gvFormati.DataSource = tmpObject;
                gvFormati.DataBind();
                tmpObject.Dispose();
            }
        }

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(ASPxMenu1, (int)hfState["idViti"], (int)hfState["idPerdoruesi"], (int)hfState["idNdermarrje"]);
        }

        /// <summary>
        /// eventet e menuse
        /// </summary>
        /// <param name="source">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {//veprimet e menuse
            #region Kod i vjeter i komentuar
            //if (e.Item.Name == "Shto" || e.Item.Name == "Modifiko")
            //{
            //    DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            //    if (isValidFormat())
            //    {
            //        DbCore.DbShare.colFormatKonfig formatet = new DbCore.DbShare.colFormatKonfig();
            //        formatet = ruajFormatKonfig();

            //        mesazh = formatet.ruajFormatet(formatet);

            //        if (!mesazh.StatusMesazhi == true)
            //        {
            //            pergjigja.Text = mesazh.PershkrimMesazhi;
            //            mbushListeFormatesh();
            //            //percaktoTemplateFormatesh();
            //        }
            //        else
            //        {
            //            pergjigja.Text = "Ndryshimet e konfigurimit te numrave u ruajten me sukses.";
            //            pergjigja.ForeColor = Color.Green;
            //            mbushListeFormatesh();
            //            //percaktoTemplateFormatesh();
            //        }
            //    }
            //    else
            //    {                   
            //        konfiguroVleraFillestare(); 
            //        konfiguroGride();
            //        //percaktoTemplateFormatesh();
            //    }
            //}
            #endregion

            if (e.Item.Name == "Ruaj")
            {
                Page.Validate("entries");
                ruajFormatNr();
            }
        }

        private void ruajFormatNr()
        {
            int idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            if (Page.IsValid == false)
                return;
            DbCore.DbShare.clsFormatiKonfig formatNr; // = new DbCore.DbShare.clsFormatiKonfig();
            int idPerdoruesi = (int)hfState["idPerdoruesi"];
            int idViti = (int)hfState["idViti"];
            int idNdermarrje = (int)hfState["idNdermarrje"];
            try
            {
                if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
                    formatNr = krijoKonfigFormati(idNdermarrje, idPerdoruesi, true, cultinf, rm);
                else formatNr = krijoKonfigFormati(idNdermarrje, idPerdoruesi, false, cultinf, rm);
            }
            catch (Exception e)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(e.Message);
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, e.Message, pnlMesazhi);
                hfStatusi.Value = "false";
                return;
            }
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, komponente);
            bool eshteShtim;
            if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
            {
                if (!tedrejtaInfo.DShtim)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("mesazhRaportNukKeniTeDrejta", cultinf), pnlMesazhi);
                    hfStatusi.Value = "false";
                    return;
                }
                mesazh = formatNr.ruaj();
                eshteShtim = true;
            }
            else
            {
                if (!tedrejtaInfo.DMod)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("mesazhRaportNukKeniTeDrejta", cultinf) , pnlMesazhi);
                    hfStatusi.Value = "false";
                    return;
                }
                formatNr.IdFormatKonfig = int.Parse(hfId.Value.ToString());
                mesazh = formatNr.modifiko();
                eshteShtim = false;
            }
            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                hfStatusi.Value = "false";
            }
            else
            {
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("mesazhRuajtjeMeSukses", cultinf), pnlMesazhi);
                if (eshteShtim)
                    shtoFormatNrNeGrid(idNdermarrje, idPerdoruesi, formatNr.IdFormatKonfig);
                else //modifikim
                    modifikoFormatNrNeGrid(idNdermarrje, idPerdoruesi, formatNr.IdFormatKonfig);
                hfStatusi.Value = "true";
                ASPxPageControl1.ActiveTabIndex = 0;
            }
        }

        private DbCore.DbShare.clsFormatiKonfig krijoKonfigFormati(int idNdermarrje, int idPerdorues, bool shtim, CultureInfo cultinf, ResourceManager rm)
        {
            int idkategoria = -1;
            if (Kategoria_ComboBox.Text != "" && Kategoria_ComboBox.Value != null)
                idkategoria = int.Parse(Kategoria_ComboBox.Value.ToString());
            DbCore.DbShare.colFormatKonfigTrupi trupi = krijoTrupiKonfigFormati(idNdermarrje);
            DbCore.DbShare.clsFormatiKonfig formati = new DbCore.DbShare.clsFormatiKonfig(idkategoria, idNdermarrje, Kategoria_ComboBox.Text, 1, txtKodi.Text, txtEmertimi.Text, idPerdorues, trupi, shtim, cultinf, rm);
            return formati;
        }

        private DbCore.DbShare.colFormatKonfigTrupi krijoTrupiKonfigFormati(int idNdermarrje)
        {
            DbCore.DbShare.colFormatKonfigTrupi trupi = new DbCore.DbShare.colFormatKonfigTrupi();
            JavaScriptSerializer serializusi = new JavaScriptSerializer();
            serializusi.MaxJsonLength = 500000000;
            object[] trupiKonfig = (object[])serializusi.DeserializeObject(hfTrupiKonfig.Value);

            if (trupiKonfig == null)
                return trupi;

            foreach (object oo in trupiKonfig)
            {
                DbCore.DbShare.clsFormatKonfigTrup format = new DbCore.DbShare.clsFormatKonfigTrup();
                format = format.krijoFormatKonfigTrup((Dictionary<string, object>)oo, idNdermarrje);
                trupi.Add(format);
            }
            return trupi;
        }

        private void shtoFormatNrNeGrid(int idNdermarrje, int idPerdorues, int idFormati)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            if (gvFormati.DataSource != null)
            {
                DataTable dt = (DataTable)gvFormati.DataSource;
                DataRow[] drs = dt.Select("IdFormatKonfig = " + idFormati);
                if (drs.Length > 0)
                    throw new Exception(rm.GetString("msgKyFormatEkziston", cultinf));
                DataRow newFormatDr = DbCore.DbShare.clsFormatiKonfig.merrFormatNrKonfigSipasId(idFormati);
                dt.ImportRow(newFormatDr);
            }
            else mbushListeFormatesh(idNdermarrje);
           // konfiguroGride();
        }

        private void modifikoFormatNrNeGrid(int idNdermarrje, int idPerdorues, int idFormati)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            if (gvFormati.DataSource != null)
            {
                DataTable dt = (DataTable)gvFormati.DataSource;
                DataRow[] drs = dt.Select("IdFormatKonfig = " + idFormati);
                if (drs.Length > 1)
                    throw new Exception(rm.GetString("msgDyFormateMeTeNjejtenID", cultinf));
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                DataRow newFormatDr = DbCore.DbShare.clsFormatiKonfig.merrFormatNrKonfigSipasId(idFormati);
                object[] arr = newFormatDr.ItemArray;
                dr.ItemArray = arr;
            }
            else mbushListeFormatesh(idNdermarrje);
           // konfiguroGride();
        }

        /// <summary>
        /// Funksioni qe fshin formatet nga grida
        /// </summary>
        /// <param name="sender">Derguesi (butoni)</param>
        /// <param name="e">Eventi</param>
        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            int a = gvFormati.FocusedRowIndex;
            gvFormati.Selection.SelectRow(a);
            List<object> rreshtat = gvFormati.GetSelectedFieldValues("IdFormatKonfig");
            DbCore.clsMesazh mesazhi = new DbCore.clsMesazh();
            DbCore.DbShare.clsFormatiKonfig formati = new DbCore.DbShare.clsFormatiKonfig();
            foreach (object id in rreshtat)
            {
                int idja = int.Parse(id.ToString());
                if (idja <= 0)
                    continue;
                bool lidhur = DbCore.DbShare.clsFormatiKonfig.eshteLidhurFormatNrMeKonfigurim(idja);
                if (lidhur)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgFormatiLidhur", cultinf), pnlMesazhi);
                    break; //mund te selektohet vetem nje rresht
                }
                else
                {
                    formati.mbushFormatNrKonfigSipasId(idja);
                    mesazhi = formati.fshiFormat();
                    if (mesazhi.Status)
                    {
                        hiqFormatNgaGrida((int)hfState["idPerdoruesi"], (int)hfState["idNdermarrje"], formati.IdFormatKonfig);
                        clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("msgFshirjaMeSukses", cultinf), pnlMesazhi);
                        ASPxPageControl1.ActiveTabIndex = 0;
                        hfStatusi.Value = "true";
                    }
                    else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgGabimGjateFshirjes", cultinf), pnlMesazhi);
                }
            }
            pnlMesazhi.Update();
            gvFormati.Selection.UnselectAll();
        }

        private void hiqFormatNgaGrida(int idPerdorues, int idNdermarrje, int idFormatKonfig)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            if (gvFormati.DataSource != null)
            {
                DataTable dt = (DataTable)gvFormati.DataSource;
                DataRow[] drs = dt.Select("IdFormatKonfig = " + idFormatKonfig);
                if (drs.Length > 1)
                    throw new Exception(rm.GetString("msgDyFormateMeTeNjejtenID", cultinf));
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                dt.Rows.Remove(dr);
                gvFormati.DataBind();
            }
            else
                mbushListeFormatesh(idNdermarrje);
        }

        /// <summary>
        /// Ben veprimet gjate callback-ut te grides
        /// </summary>
        /// <param name="sender">Derguesi</param>
        /// <param name="e">Eventi</param>
        /// </summary>        
        protected void gvFormati_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {//kur grida ben callback te ruajme te dhenat
           
            int idPerdoruesi = (int)hfState["idPerdoruesi"];
            int idGjuha = (int)hfState["idGjuha"];
            int idNdermarrje = (int)hfState["idNdermarrje"];
            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                if (arr[2] == "")
                    gvFormati.FilterExpression = "";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuha, "gvFormati", komponente, idNdermarrje);
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], idNdermarrje, koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvFormati.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvFormati);
                    }
                }
                konfiguroGride();
            }
        }

        protected void gvFormati_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpNoRows"] = gvFormati.VisibleRowCount;
            e.Properties["cpPageIndex"] = gvFormati.PageIndex;
            e.Properties["cpPageRow"] = gvFormati.SettingsPager.PageSize;
        }

        protected void gvFormati_DataBound(object sender, EventArgs e)
        {//shton butonin fshi
            //if (this.gvFormati.Columns["Fshi"] == null)
            //{
            //    GridViewDataTextColumn fshi = new GridViewDataTextColumn();
            //    fshi.Caption = "Fshi";
            //    fshi.Width = 50;
            //    gvFormati.Columns.Add(fshi);                  
            //    gvFormati.KeyFieldName = "IdFormatKonfig";
            //    gvFormati.SettingsBehavior.AllowSelectByRowClick = false;
            //    gvFormati.SettingsBehavior.AllowFocusedRow = true;
            //}
        }

        protected void gvFormati_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            string TeGjithe = rm.GetString("GridHeaderFilterFillItemTeGjithe", ci);
            e.Values.Clear();
            e.AddValue(TeGjithe, string.Empty, "true");
        }

        protected void pastroHiddenFieldet()
        {
            hfKategori.Value = "";
            hfMonedha.Value = "";
            hfSasi.Value = "";
            hfCmimi.Value = "";
            hfVlefta.Value = "";
            hfZbritje.Value = "";
        }

        protected void gvTrupiFormatNr_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            if (hfTrupiKonfig.Value != "")
            {
                DbCore.DbShare.colFormatKonfigTrupi trupi = new DbCore.DbShare.colFormatKonfigTrupi();
                JavaScriptSerializer serializusi = new JavaScriptSerializer();
                serializusi.MaxJsonLength = 500000000;
                object[] trupiKonfig = (object[])serializusi.DeserializeObject(hfTrupiKonfig.Value);
                foreach (object oo in trupiKonfig)
                {
                    DbCore.DbShare.clsFormatKonfigTrup format = new DbCore.DbShare.clsFormatKonfigTrup();
                    format = format.krijoFormatKonfigTrup((Dictionary<string, object>)oo, (int)hfState["idNdermarrje"]);
                    trupi.Add(format);
                }
                gvTrupiFormatNr.DataSource = trupi;
                gvTrupiFormatNr.DataBind();
            }
        }

        protected void gvTrupiFormatNr_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters.Split(';').Length == 2)
            {
                int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                int idPerdorues = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                if (e.Parameters.Split(';')[1] == "modifiko")
                {
                    mbushTrupiKonfigMod(idPerdorues, idNdermarrje, int.Parse(e.Parameters.Split(';')[0]));
                }
                else
                {
                    mbushTrupiKonfig(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje);
                }
            }
        }

        private void mbushTrupiKonfig(int idPerdoruesi, int idNdermarrje)
        {
            DbCore.DbShare.colFormatKonfigTrupi trupFormati = new DbCore.DbShare.colFormatKonfigTrupi();
            DbCore.DbAdmin.colMonedhat colMonedha = new DbCore.DbAdmin.colMonedhat();
            colMonedha.mbushGjitheMonedhatAktive(idNdermarrje, idPerdoruesi);
            for (int i = 0; i < colMonedha.Count; i++)
            {
                DbCore.DbShare.clsFormatKonfigTrup trupi = new DbCore.DbShare.clsFormatKonfigTrup(0, 0, colMonedha[i].IdMonedha, DbCore.DbShare.clsFormatKonfigTrup.defaultFormatSasia, DbCore.DbShare.clsFormatKonfigTrup.defaultFormatCmimi, DbCore.DbShare.clsFormatKonfigTrup.defaultFormatVlefta, DbCore.DbShare.clsFormatKonfigTrup.defaultFormatZbritja, colMonedha[i].KodiMonedha, DbCore.DbShare.clsFormatKonfigTrup.defaultFormatStringSasia, DbCore.DbShare.clsFormatKonfigTrup.defaultFormatStringCmimi, DbCore.DbShare.clsFormatKonfigTrup.defaultFormatStringVlefta, DbCore.DbShare.clsFormatKonfigTrup.defaultFormatStringZbritja);
                trupFormati.Add(trupi);
            }
            gvTrupiFormatNr.DataSource = trupFormati;
            gvTrupiFormatNr.DataBind();
        }

        private void mbushTrupiKonfigMod(int idPerdoruesi, int idNdermarrje, int idFormatKoka)
        {
            DbCore.DbShare.colFormatKonfigTrupi trupFormati = new DbCore.DbShare.colFormatKonfigTrupi(idFormatKoka);
            gvTrupiFormatNr.DataSource = trupFormati;
            gvTrupiFormatNr.DataBind();
        }

        protected void gvTrupiFormatNr_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType == DevExpress.Web.GridViewRowType.Data)
            {
                GridViewDataTextColumn col1 = ((ASPxGridView)sender).Columns["Monedha"] as GridViewDataTextColumn;
                GridViewDataComboBoxColumn col2 = ((ASPxGridView)sender).Columns["ShifraPasPresjesSasia"] as GridViewDataComboBoxColumn;
                GridViewDataComboBoxColumn col3 = ((ASPxGridView)sender).Columns["ShifraPasPresjesCmimi"] as GridViewDataComboBoxColumn;
                GridViewDataComboBoxColumn col4 = ((ASPxGridView)sender).Columns["ShifraPasPresjesVlefta"] as GridViewDataComboBoxColumn;
                GridViewDataComboBoxColumn col5 = ((ASPxGridView)sender).Columns["ShifraPasPresjesZbritja"] as GridViewDataComboBoxColumn;

                ASPxTextBox txt1 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col1, "txtBox") as ASPxTextBox;
                ASPxComboBox cmb2 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col2, "cmbBox") as ASPxComboBox;
                ASPxComboBox cmb3 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col3, "cmbBox") as ASPxComboBox;
                ASPxComboBox cmb4 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col4, "cmbBox") as ASPxComboBox;
                ASPxComboBox cmb5 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col5, "cmbBox") as ASPxComboBox;
                if (txt1 != null)
                {
                    txt1.ClientInstanceName = "Monedha" + e.VisibleIndex.ToString();
                    txt1.ClientEnabled = false;
                }

                //DbCore.DbShare.colFormatNr colFormat = new DbCore.DbShare.colFormatNr();
                //colFormat.mbushFormatNr();
                if (cmb2 != null)
                {
                    cmb2.ClientEnabled = true;
                    cmb2.ClientInstanceName = "FormatSasia" + e.VisibleIndex.ToString();
                    cmb2.DropDownButton.Visible = false;
                    cmb2.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                    cmb2.DropDownStyle = DropDownStyle.DropDownList;
                    if (cmb2.Items.Count == 0)
                        ConfigureAspxComboBox.mbushComboFormateNumrashNew(cmb2);
                    //cmb2.TextFormatString = "{1}";
                    //cmb2.DataSource = colFormat;
                    //if (cmb2.Columns.Count == 0)
                    //{
                    //    ListBoxColumn colprove = new ListBoxColumn();
                    //    colprove.FieldName = "KodFormati";
                    //    ListBoxColumn colemer = new ListBoxColumn();
                    //    colemer.FieldName = "VlereFormati";
                    //    cmb2.Columns.Add(colprove);
                    //    cmb2.Columns.Add(colemer);
                    //}
                    //cmb2.ValueField = "IdFormatNr";                    
                }
                if (cmb3 != null)
                {
                    cmb3.ClientEnabled = true;
                    cmb3.ClientInstanceName = "FormatCmimi" + e.VisibleIndex.ToString();
                    cmb3.DropDownButton.Visible = false;
                    cmb3.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                    cmb3.DropDownStyle = DropDownStyle.DropDownList;
                    if (cmb3.Items.Count == 0)
                        ConfigureAspxComboBox.mbushComboFormateNumrashNew(cmb3);
                    //cmb3.TextFormatString = "{1}";
                    //cmb3.DataSource = colFormat;
                    //if (cmb3.Columns.Count == 0)
                    //{
                    //    ListBoxColumn colprove = new ListBoxColumn();
                    //    colprove.FieldName = "KodFormati";
                    //    ListBoxColumn colemer = new ListBoxColumn();
                    //    colemer.FieldName = "VlereFormati";
                    //    cmb3.Columns.Add(colprove);
                    //    cmb3.Columns.Add(colemer);
                    //}
                    //cmb3.ValueField = "IdFormatNr";
                }
                if (cmb4 != null)
                {
                    cmb4.ClientEnabled = true;
                    cmb4.ClientInstanceName = "FormatVlefta" + e.VisibleIndex.ToString();
                    cmb4.DropDownButton.Visible = false;
                    cmb4.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                    cmb4.DropDownStyle = DropDownStyle.DropDownList;
                    if (cmb4.Items.Count == 0)
                        ConfigureAspxComboBox.mbushComboFormateNumrashNew(cmb4);
                    //cmb4.TextFormatString = "{1}";
                    //cmb4.DataSource = colFormat;
                    //if (cmb4.Columns.Count == 0)
                    //{
                    //    ListBoxColumn colprove = new ListBoxColumn();
                    //    colprove.FieldName = "KodFormati";
                    //    ListBoxColumn colemer = new ListBoxColumn();
                    //    colemer.FieldName = "VlereFormati";
                    //    cmb4.Columns.Add(colprove);
                    //    cmb4.Columns.Add(colemer);
                    //}
                    //cmb4.ValueField = "IdFormatNr";
                }
                if (cmb5 != null)
                {
                    cmb5.ClientEnabled = true;
                    cmb5.ClientInstanceName = "FormatZbritja" + e.VisibleIndex.ToString();
                    cmb5.DropDownButton.Visible = false;
                    cmb5.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                    cmb5.DropDownStyle = DropDownStyle.DropDownList;
                    if (cmb5.Items.Count == 0)
                        ConfigureAspxComboBox.mbushComboFormateNumrashNew(cmb5);
                }
            }
            if (temptxt != null)
            {
                temptxt.Focus();
            }
            else if (tempcombo != null)
            {
                tempcombo.Focus();
            }
        }

        private void percaktoTemplateTrupi(int idNdermarrje, int idPerdorues)
        {//percaktohen templatet per fushat e grides se trupit
            GridViewDataTextColumn col1 = gvTrupiFormatNr.Columns["Monedha"] as GridViewDataTextColumn;
            GridViewDataComboBoxColumn col2 = gvTrupiFormatNr.Columns["ShifraPasPresjesSasia"] as GridViewDataComboBoxColumn;
            GridViewDataComboBoxColumn col3 = gvTrupiFormatNr.Columns["ShifraPasPresjesCmimi"] as GridViewDataComboBoxColumn;
            GridViewDataComboBoxColumn col4 = gvTrupiFormatNr.Columns["ShifraPasPresjesVlefta"] as GridViewDataComboBoxColumn;
            GridViewDataComboBoxColumn col5 = gvTrupiFormatNr.Columns["ShifraPasPresjesZbritja"] as GridViewDataComboBoxColumn;
            //col1.DataItemTemplate = new MyTextTemplate();
            col2.DataItemTemplate = new MyComboTemplateFormatNr(DbCore.mySessionObjects.ktheCultureInfo(Session));
            col3.DataItemTemplate = new MyComboTemplateFormatNr(DbCore.mySessionObjects.ktheCultureInfo(Session));
            col4.DataItemTemplate = new MyComboTemplateFormatNr(DbCore.mySessionObjects.ktheCultureInfo(Session));
            col5.DataItemTemplate = new MyComboTemplateFormatNr(DbCore.mySessionObjects.ktheCultureInfo(Session));
        }

        protected void gvTrupiFormatNr_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpNoRows"] = gvTrupiFormatNr.VisibleRowCount;
            e.Properties["cpNoPage"] = gvTrupiFormatNr.PageIndex;
        }

        private void konfiguroGrideTrupi(int idPerdoruesi, int idNdermarrje)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            KonfigurimComboGride.shtoFormatNr(gvTrupiFormatNr, rm, cultinf, "ShifraPasPresjesSasia");
            KonfigurimComboGride.shtoFormatNr(gvTrupiFormatNr, rm, cultinf, "ShifraPasPresjesCmimi");
            KonfigurimComboGride.shtoFormatNr(gvTrupiFormatNr, rm, cultinf, "ShifraPasPresjesVlefta");
            KonfigurimComboGride.shtoFormatNr(gvTrupiFormatNr, rm, cultinf, "ShifraPasPresjesZbritja");

            GridUtil.percaktoVisibleColumnsMeWidth((int)hfState["idGjuha"], idNdermarrje, gvTrupiFormatNr, "gvTrupiFormatNr", komponente);
            GridUtil.konfiguroGrideRegjistrimEvogelPaTheme(gvTrupiFormatNr, "IdFormatKonfigTrup", false);
            gvTrupiFormatNr.Settings.UseFixedTableLayout = false;
            gvTrupiFormatNr.SettingsPager.Mode = GridViewPagerMode.ShowPager;
            gvTrupiFormatNr.SettingsPager.PageSize = 15;
            gvTrupiFormatNr.SettingsBehavior.AllowSort = false;
            percaktoTemplateTrupi(idNdermarrje, idPerdoruesi);
        }

        protected void gvFormati_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if (e.Column.FieldName == "IdKategoria")
                if (Converter.ConvertToInt(e.Value)==0)
                {
                    e.Criteria = null;
                }
        }
    }
}