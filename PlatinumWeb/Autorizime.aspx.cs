using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using DevExpress.Web;
using System.Drawing;
namespace PlatinumWeb
{
    /// <summary>
    /// Kjo klase sherben per te shfaqur listen e autorizimeve
    /// </summary>
    public partial class Autorizime : System.Web.UI.Page
    {
        /// <summary>
        /// thirret kur faqja lodohet. Ne te behet kontrolli nese perdoruesi eshte i loguar ne sistem  dhe nqs jo ridrejtohet tek forma e logimit
        /// thirret inicializimi i konfigurimeve fillestare te faqes
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e"> argumenti</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //behet kontrolli nese perdoruesi eshte i loguar ne sistem
            //n.q.s jo, atehere ridrejtohet edhe njeher tek forma e loginit
            if (Session["LoggedIn"].Equals("No"))
            {
                Response.Redirect("login.aspx?arsye=FaqePaautorizuar");
            }
            if (Session["KodiNdermarrjes"] == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + Session["Idperdoruesi"]);
            }
            if (!IsPostBack)
            {
                konfiguroVleraFillestare();
                konfiguroGride();
                new clsFunksione().konfiguroMenu(ASPxMenu1);
                new clsFunksione().percaktoTedrejtatPerKeteFaqe("Banka.aspx", ASPxMenu1);

            }

            if (Request.QueryString["ruaj"] == "ok")
            {
                pergjigja.Text = "Ruajtja perfundoi me sukses";
                pergjigja.ForeColor = Color.Green;
            }
            if (Request.QueryString["fshi"] == "ok")
            {
                pergjigja.Text = "Ka veprime me kete autorizim";
                pergjigja.ForeColor = Color.Red;
            }

            if (Request.QueryString["indexrow"] != null)
            {
                gvAutorizimet.FocusedRowIndex = int.Parse(Request.QueryString["indexrow"]);
            }

            container.Attributes["width"] = "350px";
            container.Attributes["height"] = "400px";
            container.Attributes["src"] = "LupaFiltra.aspx?grida=gvAutorizimet&page=Autorizime.aspx";
            konfiguroVleraFillestare();
        }
        /// <summary>
        /// perdoret per te inicializuar griden me bankat sipas ndermarjes dhe autorizimeve te perdoruesit
        /// </summary>
        ///  :  <see cref="DbArkaBanka.clsDatabaseArkaBanka.merrGjitheBankatSipasAutorizimeve(idndermarje, idperdoruesi)"/> 
        private void konfiguroVleraFillestare()
        {//mbush griden me te dhena
            clsFunksione funk = new clsFunksione();
            DbAdmin.clsDatabaseAdmin   dbAdmin = new DbAdmin.clsDatabaseAdmin();
            DbAdmin.colAutorizimetKoka autorizimet = new DbAdmin.colAutorizimetKoka();
            autorizimet.mbushGjitheAutorizimet();
            //DbAdmin.colAutorizimetKoka autorizimet = dbAdmin.merrGjitheAutorizimet();
            gvAutorizimet.DataSource = autorizimet;
            gvAutorizimet.DataBind();
      
        }
        /// <summary>
        /// perdoret per te shtuar kolonen e selektimit tek grida dhe per te vendosur disa karakteristika te grides
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void gvAutorizimet_DataBound(object sender, EventArgs e)
        {// shton colonen # per selektim dhe disa karakteristika te grides
            if (this.gvAutorizimet.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
                //perzgjidh
                DevExpress.Web.GridViewCommandColumn check = new DevExpress.Web.GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true;
                check.SetColVisibleIndex(0);
                //behet per te afishuar rreshtin qe do sherbej per filtrim
                gvAutorizimet.Settings.ShowFilterRow = true;
                gvAutorizimet.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                gvAutorizimet.Settings.ShowFilterRowMenu = true;
                gvAutorizimet.Columns.Add(check);

                gvAutorizimet.KeyFieldName = "IdAutorizimKoka";
                gvAutorizimet.SettingsBehavior.AllowMultiSelection = true;
                gvAutorizimet.SettingsBehavior.AllowFocusedRow = true;
            }
        }
        /// <summary>
        /// perdoret per te konfiguruar griden. Per te dhenat mbi kolonat qe do te shfaqen nga databaza si dhe percakton disa karakteristika te grides
        /// </summary>
        /// :  <see cref="PlatinumWeb.clsFunksione.percaktoVisibleColumns(grida, emrigrides, emrikomponentes)"/>
        /// :  <see cref="PlatinumWeb.clsFunksione.konfiguroGrideListeMadhe(grida,celesigrides)"/>
        private void konfiguroGride()
        {
            clsFunksione funksione = new clsFunksione();
            funksione.percaktoVisibleColumns(gvAutorizimet, "gvAutorizimet", "Autorizime.aspx");
            funksione.konfiguroGrideListeMadhe(gvAutorizimet, "IdAutorizimKoka");
        }
        /// <summary>
        /// perdoret ne rastet kur grida ben callback per ta rimbushur griden
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void gvAutorizimet_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            konfiguroVleraFillestare();
        }
        /// <summary>
        /// perdoret per te vendosur filtrat tek headeri i grides
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvAutorizimet_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
           
                e.Values.Clear();
                //e.AddShowAll();
                e.AddValue("(Te gjithe)", string.Empty, "true");
                e.AddValue("Nga A-D ", string.Empty, e.Column.FieldName + ">'A     ' and " + e.Column.FieldName + " <'DDDDDDD'");
                e.AddValue("Nga D-G ", string.Empty, e.Column.FieldName + ">'D     ' and " + e.Column.FieldName + "<'GGGGGGG'");
                e.AddValue("Nga H-K ", string.Empty, e.Column.FieldName + ">'H     ' and " + e.Column.FieldName + "<'KKKKKKK'");
                e.AddValue("Nga L-O ", string.Empty, e.Column.FieldName + ">'L     ' and " + e.Column.FieldName + "  <'OOOOOOO'");
                e.AddValue("Nga P-S ", string.Empty, e.Column.FieldName + ">'P     ' and " + e.Column.FieldName + "<'SSSSSSS'");
                e.AddValue("Nga T-W ", string.Empty, e.Column.FieldName + ">'T     ' and " + e.Column.FieldName + "<'WWWWWWW'");
                e.AddValue("Nga X-Z ", string.Empty, e.Column.FieldName + ">'X     ' and " + e.Column.FieldName + "<'ZZZZZZZ'");
            
        }
        /// <summary>
        /// perdoret per te filtruar griden sipas nje filtri te ruajtur me pare ne databaze
        /// </summary>
        /// :  <see cref="DbAdmin.clsDatabaseAdmin.merrFiltraGridaSipasFiltraKodi(kodfiltri,idndermarje)"/>
        /// <param name="sender"> derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void Apliko_ASPxButton_Click(object sender, EventArgs e)
        {//aplikon filtrin tek grida
            DbAdmin.clsDatabaseAdmin dbAdmin = new DbAdmin.clsDatabaseAdmin();
            DbAdmin.clsFiltraGrida filtra = new DbAdmin.clsFiltraGrida();
            filtra.mbushFiltraGridaSipasFiltraKodi(Filtri_ASPxTextBox.Text, new clsFunksione().ktheIdNdermarrje());
            //DbAdmin.clsFiltraGrida filtra = dbAdmin.merrFiltraGridaSipasFiltraKodi(Filtri_ASPxTextBox.Text, new clsFunksione().ktheIdNdermarrje());
            gvAutorizimet.FilterExpression = filtra.FiltraVlera;
            if (filtra.DrejtimRenditje == true)
                gvAutorizimet.SortBy(gvAutorizimet.Columns[filtra.KoloneRenditje], DevExpress.Data.ColumnSortOrder.Ascending);
            else
                gvAutorizimet.SortBy(gvAutorizimet.Columns[filtra.KoloneRenditje], DevExpress.Data.ColumnSortOrder.Descending);

            konfiguroVleraFillestare();
            this.Filtri_ASPxTextBox.Text = "";
        }
        /// <summary>
        /// perdoret per te ruajtur filtrin e zgjedhur ne gride ne databaze
        /// </summary>
        /// :  <see cref="DbAdmin.clsDatabaseAdmin.merrGridaKokaByEmri(emergride,emerkomponente)"/>
        /// :  <see cref="DbAdmin.clsFiltraGrida.ruaj()"/>
        /// <param name="sender"> derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {//ruan filtrin
            if (Page.IsValid)
            {
                DbAdmin.clsDatabaseAdmin dbAdmin = new DbAdmin.clsDatabaseAdmin();
                DbAdmin.clsFiltraGrida filtri = new DbAdmin.clsFiltraGrida();
                filtri.FiltraKodi = Kodi_ASPxTextBox.Text;
                filtri.FiltraShenime = Shenime_ASPxTextBox.Text;
                filtri.FiltraUniversal = false;//Universal_ASPxCheckBox.Checked;
                //DbAdmin.clsGridaKoka koka = dbAdmin.merrGridaKokaByEmri("gvAutorizimet", "Autorizime.aspx", new clsFunksione().ktheIdNdermarrje());
                DbAdmin.clsGridaKoka koka = new DbAdmin.clsGridaKoka("gvAutorizimet", "Autorizime.aspx", new clsFunksione().ktheIdNdermarrje());
                filtri.GridaKokaId = koka.IdGridaKoka;
                filtri.FiltraVlera = gvAutorizimet.FilterExpression;
                System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvAutorizimet.GetSortedColumns();
                if (kolona.Count > 0)
                {

                    filtri.KoloneRenditje = kolona[0].FieldName;
                    if (kolona[0].SortOrder == DevExpress.Data.ColumnSortOrder.Ascending)
                        filtri.DrejtimRenditje = true;
                    else
                        filtri.DrejtimRenditje = false;
                }
                else
                {
                    filtri.KoloneRenditje = "IdAutorizimKoka";
                    filtri.DrejtimRenditje = true;
                }
                DbAdmin.clsPerdorues oPerdorues = new DbAdmin.clsPerdorues();
                oPerdorues = (DbAdmin.clsPerdorues)(Session["oClsPerdoruesi"]);
                filtri.IdPerdoruesi = oPerdorues.IdPerdorues;
                filtri.IdNdermarje = new clsFunksione().ktheIdNdermarrje();
                filtri.IdStatusDok = 1;
                filtri.ruaj();
                Kodi_ASPxTextBox.Text = "";
                Shenime_ASPxTextBox.Text = "";
                //Universal_ASPxCheckBox.Text = "";
                popRuaj.ShowOnPageLoad = false;
            }
        }
        /// <summary>
        /// perdoret per te validuar nese eksiston nje filter me kete kod ne databaze
        /// </summary>
        /// :  <see cref="DbAdmin.clsDatabaseAdmin.ekzistonFilter(kod, idndermarje)"/>
        /// <param name="source"> derguesi</param>
        /// <param name="args">argumentat</param>
        protected void Kodi_CustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {//kontrollon nese kodi i filtrit eksiston
            args.IsValid = true;
            DbAdmin.clsDatabaseAdmin dbAdmin = new DbAdmin.clsDatabaseAdmin();
            if (dbAdmin.ekzistonFilter(Kodi_ASPxTextBox.Text, new clsFunksione().ktheIdNdermarrje()))
                args.IsValid = false;
        }
        /// <summary>
        /// perdoret per te fshire rreshtat e zgjedhur te bankave nqs perdoruesi konfirmon fshirjen
        /// </summary>
        ///  :  <see cref=DbArkaBanka.clsDatabaseArkaBanka.ktheBanke(id)"/> 
        ///  :  <see cref="DbArkaBanka.clsBanka.fshi()"/> 
        /// <param name="sender"> derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            bool kaveprim = false;
            //ben fshirjen e nje autorizimi
            List<object> rreshtat = gvAutorizimet.GetSelectedFieldValues("IdAutorizimKoka");
            foreach (int id in rreshtat)
            {
                DbAdmin.clsDatabaseAdmin dbAdmin = new DbAdmin.clsDatabaseAdmin();
                //DbAdmin.colAutorizimetKoka colAutorizime = dbAdmin.ktheAutorizim(id);
                DbAdmin.clsAutorizimKoka clsAutorizime = new DbAdmin.clsAutorizimKoka(id);
              clsAutorizime  .IdPerdoruesi = clsFunksione.ktheIdPerdoruesi(Session);
                //foreach (DbAdmin.clsAutorizimKoka l in colAutorizime)
                //{
                    if (dbAdmin.kaVeprimeAutorizim(clsAutorizime.IdAutorizimKoka))
                    { kaveprim = true; }
                    else

                        clsAutorizime.fshi();
                //}
            }
            if (kaveprim)
            {

                Response.Redirect("Autorizime.aspx?fshi=ok");
            }
            else
                Response.Redirect("Autorizime.aspx");
        }
        /// <summary>
        /// perdoret per te trajtuar ngjarjet e butonave te menuse. Shkon ne faqen e modifikimit te bankave kur perdoruesi klikon butonin modifiko
        /// ose ne faqen e shtimit te bankave kur perdoruesi klikon butonin shto
        /// </summary>
        /// <param name="source"> derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {//kryen veprimet e menuse
            if (e.Item.Name == "Shto")
            {
                Response.Redirect("Shto_Autorizimet.aspx");
            }
            else if (e.Item.Name == "Modifiko")
            {
                int indeksi = gvAutorizimet.FocusedRowIndex;
                string id;
                string nr;

                if (gvAutorizimet.GetRowValues(indeksi, "IdAutorizimKoka") != null)
                {
                    id = gvAutorizimet.GetRowValues(indeksi, "IdAutorizimKoka").ToString();
                    nr = gvAutorizimet.GetRowValues(indeksi, "KodiAutorizim").ToString();
                }
                else
                {
                    id = null;
                    nr = null;
                }
                Response.Redirect("~/Modifiko_Autorizim.aspx?id=" + id + "&kodi=" + nr + "&indexrow=" + indeksi);
            }
        }
        /// <summary>
        /// perdoret per te shtuar properti ne javascript
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void gvAutorizimet_CustomJSProperties(object sender, DevExpress.Web.ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvAutorizimet.PageIndex;
            e.Properties["cpPageRow"] = gvAutorizimet.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvAutorizimet.VisibleRowCount;
        } 
  
    }
}
