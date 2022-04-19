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
using DbCore.IMBUtils.Extensions;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class LupaShfaqGrupimPerberes : MyPageBase
    {
        int idNdermarrje;
        protected void Page_Load(object sender, EventArgs e)
        {
            int idPerdoruesi;
            int idGjuha;
          
            int idViti;
            int idNdermarrjeVit;
            int idKonfigambjenti;
            string artikulli = "";
            string idartikullSasi = "";
            DateTime data = DateTime.Today;

            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (!String.IsNullOrEmpty(Request.QueryString["idartikull"]))
                artikulli = (Request.QueryString["idartikull"]).ToString();
            if (!String.IsNullOrEmpty(Request.QueryString["idartikullSasi"]))
                idartikullSasi = (Request.QueryString["idartikullSasi"]).ToString();
            if (!String.IsNullOrEmpty(Request.QueryString["data"]))
                data = DateTime.Parse((Request.QueryString["data"]).ToString());         


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
            gvLupaGrupimPerberes.Columns.Clear();
            this.gvLupaGrupimPerberes.AutoGenerateColumns = true;
            if (!IsPostBack)
            {
                mbushHiddenFieldMePerkthime(cultinf, rm);
                //percaktoTemplateMenu(ASPxMenu1, idViti, idPerdoruesi, idNdermarrje, rm, cultinf);
                //clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, "gvLupaGrupimPerberes", 1, "LupaArtikull.aspx");
                string vleraQueryString = "";
                if (Request.QueryString["idKonfigAmbjente"] != null && Request.QueryString["idKonfigAmbjente"] != "")
                    vleraQueryString = Request.QueryString["idKonfigAmbjente"].ToString();
                // duhet ndryshuar            
                int idNivel = clsNivelRegjistrimi.ktheIdNivelRegjistrimiSipasKodi("LGP", idNdermarrje);
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

                //DbCore.DbShare.clsKusht kushtkss = new DbCore.DbShare.clsKusht(idKonfigambjenti, "KSSH");
                //DbCore.DbShare.clsAlternativaKushti alterkss = new DbCore.DbShare.clsAlternativaKushti(kushtkss.Vlera);
                if (DbCore.DbShare.clsAlternativaKushti.getAlternativa(idKonfigambjenti, "KSSH") == "Po")
                    hfState.Add("KSSH", true);
                else hfState.Add("KSSH", false);
                if (DbCore.DbShare.clsAlternativaKushti.getAlternativa(idKonfigambjenti, "ES") == "Po")
                    hfState.Add("ES", true);
                else hfState.Add("ES", false);
                //  DbCore.clsFunksione.AplikoFilterDefault(gvLupaGrupimPerberes, idKonfigambjenti);
                hfState.Set("idKonfigambjenti", idKonfigambjenti);
                // mbushPopUpListeArtikujshNgaDB(idPerdoruesi, idNdermarrje, (kosto || gjendja), idMagazina, cmime, artikujTeShitshem);
                mbushPopUpListeGrupimPerberes(artikulli, idartikullSasi,data);
                //konfiguroPopupGride(idNdermarrje, idKonfigambjenti, true, kosto, gjendja, cultinf, rm);
            }
            else
            {
                idKonfigambjenti = (int)hfState["idKonfigambjenti"];
                if (!IsCallback || (IsCallback && Request["__CALLBACKID"].ToString().Contains("ASPxMenu1")))
                {
                    // percaktoTemplateMenu(ASPxMenu1, idViti, idPerdoruesi, idNdermarrje, rm, cultinf);
                }

                if (!IsCallback || (IsCallback && Request["__CALLBACKID"].ToString().Contains("gvLupaGrupimPerberes")))
                {

                    mbushPopUpListeArtikujshNgaSession(artikulli,idartikullSasi,data);
                    //  konfiguroPopupGride(idNdermarrje, idKonfigambjenti, false, cbKosto.Checked, cbGjendje.Checked,cultinf,rm);
                }
            }
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            GridUtil.percaktoVisibleColumnsSipasKonfigurimit(gvLupaGrupimPerberes, "gvLupaGrupimPerberes", "LupaShfaqGrupimPerberes.aspx", idKonfigambjenti, true, idGjuha);
            GridUtil.KonfiguroGrideListeMadhePopupiPaTheme(gvLupaGrupimPerberes, "IDARTIKULLIPERBERES", (bool)hfState["KSSH"], (bool)hfState["ES"]);
        }

        private void mbushHiddenFieldMePerkthime(CultureInfo cultinf, ResourceManager rm)
        {
              }



        private void mbushPopUpListeGrupimPerberes(string artikulli, string idartikullSasi, DateTime data)
        {
            DataTable dtb= new DataTable();
            DbCore.DbInventari.colArtikulliPerberes artper = new DbCore.DbInventari.colArtikulliPerberes();
            dtb = (artper.merrSipasIdArtikullKryesoreDt(artikulli, idNdermarrje,data));
            DataTable tabidartikullSasi = new DataTable();
            DataColumn artId = tabidartikullSasi.Columns.Add("artId", typeof(int));
            DataColumn sasi = tabidartikullSasi.Columns.Add("sasi", typeof(int));
          
 

            string[] lines = idartikullSasi.Split(';');

            foreach (var line in lines)
            {
                string[] split = line.Split(',');

                DataRow row = tabidartikullSasi.NewRow();

                row.SetField(artId, int.Parse(split[0]));
                row.SetField(sasi, split[1]);

                tabidartikullSasi.Rows.Add(row);
            }

            DataTable artikujPerberesTable = new DataTable();
            artikujPerberesTable.Columns.Add("IDARTIKULLIPERBERES", typeof(int));
            artikujPerberesTable.Columns.Add("IDARTIKULLKRYESOR", typeof(int));
            artikujPerberesTable.Columns.Add("kodartikulli", typeof(string));
            artikujPerberesTable.Columns.Add("pershkrimartikulli", typeof(string));
            artikujPerberesTable.Columns.Add("Njesia", typeof(string));
            artikujPerberesTable.Columns.Add("KOEFICIENTI", typeof(float));
            artikujPerberesTable.Columns.Add("SASIA", typeof(float));
            artikujPerberesTable.Columns.Add("TotalRresht", typeof(float));
            var TMPartikujPerberesTable = (from t1 in dtb.AsEnumerable()
                                          join t2 in tabidartikullSasi.AsEnumerable()
                                              on t1.Field<int>("IDARTIKULLKRYESOR") equals t2.Field<int>("artId")
                                          select new
                                          {
                                              IDARTIKULLIPERBERES = t1["IDARTIKULLIPERBERES"],
                                              IDARTIKULLKRYESOR = t1["IDARTIKULLKRYESOR"],
                                              kodartikulli = t1["kodartikulli"],
                                              pershkrimartikulli = t1["pershkrimartikulli"],
                                              Njesia = t1["Njesia"],
                                              KOEFICIENTI = t1["KOEFICIENTI"],
                                              SASIA = t2["sasi"],
                                              TotalRresht = Convert.ToDouble( t2["sasi"]) * Convert.ToDouble(t1["KOEFICIENTI"])
                                          });

            // Now with the results of the query fill in the columns of the new DataTable
            foreach (var dr in TMPartikujPerberesTable)
            {
                 
                artikujPerberesTable.Rows.Add(dr.IDARTIKULLIPERBERES, dr.IDARTIKULLKRYESOR,dr.kodartikulli,dr.pershkrimartikulli,dr.Njesia,dr.KOEFICIENTI,dr.SASIA,dr.TotalRresht);
               
            }
            var tabPerb = (from b in artikujPerberesTable.AsEnumerable()
                              group b by b.Field<int>("IDARTIKULLIPERBERES") into g
                              select new
                              {
                                  IDARTIKULLIPERBERES = g.Key,
                                  kodartikulli=g.Min(x => x.Field<string>("kodartikulli")),
                                  pershkrimartikulli = g.Min(x => x.Field<string>("pershkrimartikulli")),
                                  Njesia = g.Min(x => x.Field<string>("Njesia")),
                                  Totali = g.Sum(x => x.Field<float>("TotalRresht"))
                              }).ToDataTable("IDARTIKULLIPERBERES", "kodartikulli", "pershkrimartikulli", "Njesia", "Totali");
            gvLupaGrupimPerberes.Columns.Clear();
            gvLupaGrupimPerberes.AutoGenerateColumns = true;
            DbCore.mySessionObjects.ruajGrideNeSessionLupaArtikull(Session, tabPerb);
            gvLupaGrupimPerberes.DataSource = tabPerb;
            gvLupaGrupimPerberes.AutoGenerateColumns = true;
            gvLupaGrupimPerberes.DataBind();
        }


        private int merrKonfiguriminDefaultTeLupes(int idNdermarrje, int idNivel)
        {
            //do marr konfigurimin default per kete nivel regjistrimi i cili eshte i vetem per nje ndermarrje
            //DbCore.DbShare.clsKonfigurimAmbjenti ambj = new DbCore.DbShare.clsKonfigurimAmbjenti(idNdermarrje, idNivel);
            //return ambj.IdKonfigAmbjente;
            return DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimi(idNdermarrje, idNivel);
        }

        private void mbushPopUpListeArtikujshNgaSession(string artikulli, string idartikullSasi,DateTime data)
        {
            Object tmpObject;
            DbCore.mySessionObjects.merrGrideNgaSessioniLupaArtikull(Session, out tmpObject);
            if (tmpObject == null)
            {
                mbushPopUpListeGrupimPerberes(artikulli, idartikullSasi,data);
            }
            else
            {
                gvLupaGrupimPerberes.DataSource = tmpObject;
                gvLupaGrupimPerberes.DataBind();
            }
        }

        protected void gvLupaGrupimPerberes_DataBound(object sender, EventArgs e)
        {
            //perdoret per ti vene disa atribute grides
            //behet per te afishuar rreshtin qe do sherbej per filtrim
            gvLupaGrupimPerberes.Settings.ShowFilterRow = true;
            gvLupaGrupimPerberes.KeyFieldName = "IDARTIKULLIPERBERES";
            gvLupaGrupimPerberes.SettingsBehavior.AllowSelectByRowClick = true;
        }
       
    }
}