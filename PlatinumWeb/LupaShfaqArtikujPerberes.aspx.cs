using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Collections;
using System.Drawing;
using DbCore.DbRegjistrim;
using System.Data;
using PlatinumWeb.Templates;
using System.Globalization;
using System.Resources;
using DbCore.DbInventari;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class LupaShfaqArtikujPerberes : MyPageBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            int idPerdoruesi;
            int idGjuha;
            int idNdermarrje;
            int idViti;
            int idNdermarrjeVit;
            int idKonfigambjenti;
            int artikulli = 0;
            DateTime data = DateTime.Today;

            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (!String.IsNullOrEmpty(Request.QueryString["idartikull"]))
                artikulli = int.Parse(Request.QueryString["idartikull"]);
            if (!String.IsNullOrEmpty(Request.QueryString["data"]))
                data = DateTime.Parse(Request.QueryString["data"]);

            if (hfState.Count == 0)
            {
                if (!DbCore.mySessionObjects.isLogedIn(Session))
                {
                    DbCore.clsFunksione.logout(Session,true,"FaqePaautorizuar");
                }
                idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
                idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
                idNdermarrjeVit = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
                hfState.Set("idPerdoruesi", idPerdoruesi);
                hfState.Set("idGjuha", idGjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idViti", idViti);
                hfState.Set("idNdermarrjeVit", idNdermarrjeVit);
            }
            else
            {
                idPerdoruesi = (int)hfState["idPerdoruesi"];
                idGjuha = (int)hfState["idGjuha"];
                idNdermarrje = (int)hfState["idNdermarrje"];
                idViti = (int)hfState["idViti"];
                idNdermarrjeVit = (int)hfState["idNdermarrjeVit"];

            }
            gvLupaArtikullPerberes.Columns.Clear();
            this.gvLupaArtikullPerberes.AutoGenerateColumns = true;
            if (!IsPostBack)
            {
                mbushHiddenFieldMePerkthime(cultinf, rm);
                //percaktoTemplateMenu(ASPxMenu1, idViti, idPerdoruesi, idNdermarrje, rm, cultinf);
                //clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, "gvLupaArtikullPerberes", 1, "LupaArtikull.aspx");
                string vleraQueryString = "";
                if (Request.QueryString["idKonfigAmbjente"] != null && Request.QueryString["idKonfigAmbjente"] != "")
                    vleraQueryString = Request.QueryString["idKonfigAmbjente"].ToString();
                // duhet ndryshuar            
                int idNivel = clsNivelRegjistrimi.ktheIdNivelRegjistrimiSipasKodi("ShfaqArtPerb", idNdermarrje);
                if (vleraQueryString != "")
                {
                  
                    string[] idte = vleraQueryString.Split('-');
                    if (idte.Length > 1)
                    {
                        for (int i = 0; i < idte.Length; i++)
                        {
                            //konfigLupa = new DbCore.DbShare.clsKonfigurimAmbjenti(Convert.ToInt32(idte[i]));
                            if (DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdNivel(Convert.ToInt32(idte[i])) == idNivel)
                            {
                                idKonfigambjenti = Convert.ToInt32(idte[i]);
                                break;
                            }
                        }
                        idKonfigambjenti = merrKonfiguriminDefaultTeLupes(idNdermarrje, idNivel);
                    }
                    else
                        if (idte.Length == 1)
                        {
                            idKonfigambjenti = Convert.ToInt32(vleraQueryString);
                            if (idKonfigambjenti == 0 || idKonfigambjenti == -1)
                                idKonfigambjenti = merrKonfiguriminDefaultTeLupes(idNdermarrje, idNivel);
                        }
                        else
                            idKonfigambjenti = merrKonfiguriminDefaultTeLupes(0, idNivel);
                }
                else
                    idKonfigambjenti = merrKonfiguriminDefaultTeLupes(idNdermarrje, idNivel);

               if (DbCore.DbShare.clsAlternativaKushti.getAlternativa(idKonfigambjenti, "KSSH") == "Po")
                    hfState.Add("KSSH", true);
                else hfState.Add("KSSH", false);
                if (DbCore.DbShare.clsAlternativaKushti.getAlternativa(idKonfigambjenti, "ES") == "Po")
                    hfState.Add("ES", true);
                else hfState.Add("ES", false);
                hfState.Set("idKonfigambjenti", idKonfigambjenti);
                mbushPopUpListeArtikujshPerberes(artikulli,data);
            }
            else
            {
                idKonfigambjenti = (int)hfState["idKonfigambjenti"];
                if (!IsCallback || (IsCallback && Request["__CALLBACKID"].ToString().Contains("ASPxMenu1")))
                {
                    // percaktoTemplateMenu(ASPxMenu1, idViti, idPerdoruesi, idNdermarrje, rm, cultinf);
                }

                if (!IsCallback || (IsCallback && Request["__CALLBACKID"].ToString().Contains("gvLupaArtikullPerberes")))
                {

                    mbushPopUpListeArtikujshNgaSession(artikulli,data);
                    //  konfiguroPopupGride(idNdermarrje, idKonfigambjenti, false, cbKosto.Checked, cbGjendje.Checked,cultinf,rm);
                }
            }
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            GridUtil.percaktoVisibleColumnsSipasKonfigurimit(gvLupaArtikullPerberes, "gvLupaArtikullPerberes", "LupaShfaqArtikujPerberes.aspx", idKonfigambjenti, true, idGjuha);
            GridUtil.KonfiguroGrideListeMadhePopupiPaTheme(gvLupaArtikullPerberes, "IDARTIKULLIPERBERES", (bool)hfState["KSSH"], (bool)hfState["ES"]);
        }

        private void mbushHiddenFieldMePerkthime(CultureInfo cultinf, ResourceManager rm)
        {
            //popupUniversal.HeaderText = rm.GetString("headerPopUpText", cultinf);
            //hfState.Set("msgZgjidhMagazinen", rm.GetString("msgZgjidhMagazinen", cultinf));
            //hfState.Set("headerShtoFilter", rm.GetString("headerShtoFilter", cultinf));
            //hfState.Set("JQgridShtoArtikull", rm.GetString("JQgridShtoArtikull", cultinf));
            //hfState.Set("msgSelektoniNjeRresht", rm.GetString("msgSelektoniNjeRresht", cultinf));
            //hfState.Set("JQgridShikoPerberesit", rm.GetString("JQgridShikoPerberesit", cultinf));
        }



        private void mbushPopUpListeArtikujshPerberes(int artikulli, DateTime data)
        {
            bool artikullIPerbere = Convert.ToBoolean(Request.QueryString["artikullIPerbere"]);
            DataTable dt = artikullIPerbere ? 
                DbCore.DbInventari.colArtikujt.merrArtikujtPerberesSipasIdArtikullitKryesor(artikulli, data)
                :
                DbCore.DbInventari.colArtikujt.merrArtikujtEPerbereSipasIdArtikullitPerberes(artikulli, data);
            gvLupaArtikullPerberes.Columns.Clear();
            gvLupaArtikullPerberes.AutoGenerateColumns = true;
            DbCore.mySessionObjects.ruajGrideNeSessionLupaArtikull(Session, dt);
            gvLupaArtikullPerberes.DataSource = dt;
            gvLupaArtikullPerberes.AutoGenerateColumns = true;
            gvLupaArtikullPerberes.DataBind();
        }


        private int merrKonfiguriminDefaultTeLupes(int idNdermarrje, int idNivel)
        {
            //do marr konfigurimin default per kete nivel regjistrimi i cili eshte i vetem per nje ndermarrje
            //DbCore.DbShare.clsKonfigurimAmbjenti ambj = new DbCore.DbShare.clsKonfigurimAmbjenti(idNdermarrje, idNivel);
            //return ambj.IdKonfigAmbjente;
            return DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimi(idNdermarrje, idNivel);
        }

        private void mbushPopUpListeArtikujshNgaSession(int artikulli, DateTime data)
        {
            Object tmpObject;
            DbCore.mySessionObjects.merrGrideNgaSessioniLupaArtikull(Session, out tmpObject);
            if (tmpObject == null)
            {
                mbushPopUpListeArtikujshPerberes(artikulli, data);
            }
            else
            {
                gvLupaArtikullPerberes.DataSource = tmpObject;
                gvLupaArtikullPerberes.DataBind();
            }
        }

        protected void gvLupaArtikullPerberes_DataBound(object sender, EventArgs e)
        {
            //perdoret per ti vene disa atribute grides
            //behet per te afishuar rreshtin qe do sherbej per filtrim
            gvLupaArtikullPerberes.Settings.ShowFilterRow = true;
            gvLupaArtikullPerberes.KeyFieldName = "IDARTIKULLIPERBERES";
            gvLupaArtikullPerberes.SettingsBehavior.AllowSelectByRowClick = true;
        }
       
    }
}