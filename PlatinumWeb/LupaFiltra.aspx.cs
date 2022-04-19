using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using DevExpress.Web;
using DbCore.DbRegjistrim;
namespace PlatinumWeb
{
    public partial class LupaFiltra : System.Web.UI.Page
    {
        private int idKonfigambjenti;
        protected void Page_PreInit(object sender, EventArgs e)
        {
            DbCore.clsFunksione.percaktoThemeAmbjenteDheJQuery(Page, mySessionObjects.ktheIdPerdoruesi(Session));
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            string vleraQueryString = "";
            if (Request.QueryString["idKonfigAmbjente"] != null && Request.QueryString["idKonfigAmbjente"] != "")
                vleraQueryString = Request.QueryString["idKonfigAmbjente"].ToString();

            clsNivelRegjistrimi nivel = new clsNivelRegjistrimi();
            nivel.Kodi = "FILT"; //eshte kodi i lupes, mund te shihet ne DB ne T_NIVELREGJISTRIMI
            nivel.IdNdermarje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
            nivel = nivel.merrNivelRegjSipasKodi();
            //if (vleraQueryString != "")
            //{
            //    //ketu me intereson id e nivelit
            //    //ne rastin kur kemi disa konfigurime te lupes per kontrollin i cili gjeneroi thirrjen e kesaj lupe
            //    //duhet te gjejme cili nga konfigurimet e ka idNivel sa niveli i artikujve
            //    //i kontrollojme me radhe te gjitha konfigurimet qe i jane kaluar ne query string
            //    string[] idte = vleraQueryString.Split('-');
            //    if (idte.Length > 1)
            //    {
            //        DbCore.DbShare.clsKonfigurimAmbjenti konfigLupa = new DbCore.DbShare.clsKonfigurimAmbjenti();
            //        for (int i = 0; i < idte.Length; i++)
            //        {
            //            konfigLupa.IdKonfigAmbjente = Convert.ToInt32(idte[i]);
            //            konfigLupa = konfigLupa.merrSipasId();
            //            if (konfigLupa.IdNivel == nivel.IdNivel)
            //            {
            //                idKonfigambjenti = Convert.ToInt32(idte[i]);
            //            }
            //        }
            //    }
            //    else
            //        if (idte.Length == 1)
            //        {
            //            idKonfigambjenti = Convert.ToInt32(Request.QueryString["idKonfigAmbjente"].ToString());
            //            if (idKonfigambjenti == 0)
            //                merrKonfiguriminDefaultTeLupes(nivel.IdNivel);
            //        }
            //        else
            //            merrKonfiguriminDefaultTeLupes(nivel.IdNivel); 
            //}
            //else
            //{ 
            //    merrKonfiguriminDefaultTeLupes(nivel.IdNivel);
            //}
            //lupa e filtrave nuk eshte e perdorshme me konfigurim
            merrKonfiguriminDefaultTeLupes(nivel.IdNivel);
            mbushPopUpListeFiltrash();
            konfiguroPopupGride();
        }
        private void merrKonfiguriminDefaultTeLupes(int idNivel)
        {
            //do marr konfigurimin default per kete nivel regjistrimi i cili eshte i vetem per nje ndermarrje
            DbCore.DbShare.clsKonfigurimAmbjenti ambj = new DbCore.DbShare.clsKonfigurimAmbjenti();
            ambj.IdNivel = idNivel;
            DbCore.clsFunksione funk = new DbCore.clsFunksione(mySessionObjects.ktheCultureInfo(Session));
            ambj.IdNdermarje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
            ambj = ambj.merrDefaultSipasNivel();
            idKonfigambjenti = ambj.IdKonfigAmbjente;
        }
        private void mbushPopUpListeFiltrash()
        {//mbush griden e popupit me te dhena
            //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
             DbCore.DbAdmin.colFiltratGrida colFiltra=new DbCore.DbAdmin.colFiltratGrida();
            string grida = Request.QueryString["grida"];
            string page = Request.QueryString["page"];
            string idKoka = Request.QueryString["IdKoka"];
            if (idKoka !=null)
                colFiltra= new DbCore.DbAdmin.colFiltratGrida(Convert.ToInt32(idKoka.ToString()), mySessionObjects.merrIdNdermarrjeSesioni(Session));
                 //colFiltra = dbAdmin.merrGjitheFiltratGridaByGridaKoka(Convert.ToInt32(idKoka.ToString()), mySessionObjects.merrIdNdermarrjeSesioni(Session));
            else
            {
                //DbCore.DbAdmin.clsGridaKoka koka = dbAdmin.merrGridaKokaByEmri(grida, page, mySessionObjects.merrIdNdermarrjeSesioni(Session));
                DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(grida, page, mySessionObjects.merrIdNdermarrjeSesioni(Session));
                colFiltra= new DbCore.DbAdmin.colFiltratGrida(koka.IdGridaKoka, mySessionObjects.merrIdNdermarrjeSesioni(Session));
                //colFiltra = dbAdmin.merrGjitheFiltratGridaByGridaKoka(koka.IdGridaKoka, mySessionObjects.merrIdNdermarrjeSesioni(Session));
            }
            gvLupaFiltra.DataSource = colFiltra;
            gvLupaFiltra.DataBind();
        }

        protected void gvLupaFiltra_DataBound(object sender, EventArgs e)
        {
            gvLupaFiltra.Settings.ShowFilterRow = true;
            gvLupaFiltra.KeyFieldName = "IdFiltra";
            gvLupaFiltra.SettingsBehavior.AllowSelectByRowClick = true;
            gvLupaFiltra.Columns[1].Width = 100;
            gvLupaFiltra.Columns[2].Width = 150;
        }

        private void konfiguroPopupGride()
        {
            if (gvLupaFiltra.Columns["#"] == null)
            {
                DevExpress.Web.GridViewCommandColumn colFshi = new DevExpress.Web.GridViewCommandColumn("#");
                colFshi.DeleteButton.Visible = true;                
                colFshi.Width = Unit.Percentage(10);
                colFshi.VisibleIndex = 0;
                colFshi.DeleteButton.Text = "Fshi";
                gvLupaFiltra.Columns.Add(colFshi);
            }

            DbCore.clsFunksione.percaktoVisibleColumnsSipasKonfigurimit(gvLupaFiltra, "gvLupaFiltra", "LupaFiltra.aspx", idKonfigambjenti);
            //funk.konfiguroGrideListeMadhePopupi(gvLupaFiltra, "IdFiltra");
            DbCore.clsFunksione.konfiguroGrideListeMadhePopupiPaTheme(gvLupaFiltra, "IdFiltra");
        }

        protected void gvLupaFiltra_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida();
            DbCore.DbAdmin.clsDatabaseAdmin data = new DbCore.DbAdmin.clsDatabaseAdmin();
            if (e.Values[0].ToString() != "")
            {
                filtri.IdFiltra = int.Parse(e.Values[0].ToString());                

                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = data.fshiFiltraGrida(filtri.IdFiltra);
                e.Cancel = true;
                mbushPopUpListeFiltrash();                
            }
            else
            {
                e.Cancel = true;
                mbushPopUpListeFiltrash();
            }
            data.Dispose();
        }

       

    
    }
}
