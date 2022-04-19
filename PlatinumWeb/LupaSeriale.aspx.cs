using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Collections;
using System.Drawing;
using DbCore.DbAdmin;
using System.Web.Script.Serialization;
using System.Globalization;
using System.Resources;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class LupaSeriale : MyPageBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
           
                CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
                ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                mbushHiddenFieldMePerkthime(ci, rm);
                percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), rm, ci);
                int id = int.Parse(Request.QueryString["id"]);
                int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                hfState.Set("idNdermarrje", idNdermarrje);
                mbushgridaBosh();
                //mbushPopUpListeNgaDB(id, true, idNdermarrje);
                konfiguroGride();
            }
        }

        private void mbushHiddenFieldMePerkthime(CultureInfo ci, ResourceManager rm)
        {
            lblFushat.Text = rm.GetString("lblSerialetEArtikullit", ci);
            lblZgjedhur.Text = rm.GetString("lblSerialetEZgjedhura", ci);
            popupUniversal.HeaderText = rm.GetString("headerPopUpText", ci);
            hfState.Set("msgSerialetLupa", rm.GetString("msgSerialetLupa", ci));
        }
        private void mbushgridaBosh()
        {
            DbCore.DbAsete.colAQTSeriale col = new DbCore.DbAsete.colAQTSeriale();
            DbCore.DbAsete.colAQTSeriale colzgjedhur = new DbCore.DbAsete.colAQTSeriale();
            gvFushat.DataSource = col;
            gvZgjedhur.DataSource = colzgjedhur;
            gvZgjedhur.DataBind();
            gvFushat.DataBind();
        }
        private void percaktoTemplateMenu(ASPxMenu aSPxMenu1, int idViti, int idPerdorues, int idNdermarrje, ResourceManager rm, CultureInfo ci)
        {
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, "LupaSeriale.aspx", this, MenuInfo, true, true, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
            aSPxMenu1.Items.FindByName("Anullo").Text = rm.GetString("MenuItemMbyll", ci);

        }
        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), rm, ci);
        }
        private void mbushPopUpListeNgaDB(int id, int lastsel, int idNdermarrje, string vjennga, int idmag)
        {//mbush griden e popupit me te dhena     
            DbCore.DbAsete.colAQTSeriale col = new DbCore.DbAsete.colAQTSeriale();
            DbCore.DbAsete.colAQTSeriale colzgjedhur = new DbCore.DbAsete.colAQTSeriale();
            if (vjennga == "blerje" || vjennga == "hyrje")
                col.ktheAQTSerialSipasIDArtikulliTePaperdorura(id, idNdermarrje);
            else if (vjennga == "rillogaritja")
                col.ktheAQTSerialSipasIDArtikulliTeperdoruraTegjitha(id, idNdermarrje);
            else
            {
                DateTime data = DateTime.Parse(Request.QueryString["data"]);
                col.ktheAQTSerialSipasIDArtikulliDheMagazineTeperdorura(id, idNdermarrje, idmag, data);
            }
            JavaScriptSerializer serializusi = new JavaScriptSerializer();
            if (hfSeriale.Contains(id + "_" + lastsel))//gjejme serialet e ketij rreshti dhe nga lista e serialeve te vlefshme heqim serialet qe kemi zgjedhur
            {

                object[] dokumenti = (object[])serializusi.DeserializeObject(hfSeriale.Get(id + "_" + lastsel).ToString());
                for (int i = 0; i < dokumenti.Length; i++)
                {
                    DbCore.DbAsete.clsAQTSeriale serial = new DbCore.DbAsete.clsAQTSeriale((Dictionary<string, object>)dokumenti[i]);
                    colzgjedhur.Add(serial);
                    DbCore.DbAsete.clsAQTSeriale serialipazgjedhur = col.Find(bk => bk.IdAQTSerial == serial.IdAQTSerial);
                    if (serialipazgjedhur != null)
                        col.Remove(serialipazgjedhur);


                }

            }
            DbCore.DbAsete.colAQTSeriale serialeteperdoruraNgaKjoFature = new DbCore.DbAsete.colAQTSeriale();
            ArrayList sasiteeserialeve = new ArrayList();
            for (int m = 0; m < hfSerialeTePerdorura.Count; m++)//heqim serialet e perdorur ne rreshtat e tjere
            {
                object[] dokumenti = (object[])serializusi.DeserializeObject(hfSerialeTePerdorura.Get(m.ToString()).ToString());
                for (int i = 0; i < dokumenti.Length; i++)
                {
                    DbCore.DbAsete.clsAQTSeriale serial = new DbCore.DbAsete.clsAQTSeriale((Dictionary<string, object>)dokumenti[i]);
                    if (serialeteperdoruraNgaKjoFature.FindAll(x => x.IdAQTSerial == serial.IdAQTSerial).Count()>0)
                    {
                        int index = serialeteperdoruraNgaKjoFature.FindIndex(x => x.IdAQTSerial == serial.IdAQTSerial);
                        sasiteeserialeve[index] = (Double.Parse(sasiteeserialeve[index].ToString()) + Double.Parse(hfSasiSeriale.Get(m.ToString()).ToString())).ToString();
                    }
                    else
                    {
                        serialeteperdoruraNgaKjoFature.Add(serial);
                        sasiteeserialeve.Add(hfSasiSeriale.Get(m.ToString()));
                    }
                    

                }

            }
            for (int i = 0; i < serialeteperdoruraNgaKjoFature.Count; i++)
            {
                DbCore.DbAsete.clsAQTSeriale serialipazgjedhur = col.Find(bk => bk.IdAQTSerial == serialeteperdoruraNgaKjoFature[i].IdAQTSerial);
                if (serialipazgjedhur != null)
                    if (serialipazgjedhur.IdHistorikAktualPaSerial > 0)
                    {
                        DbCore.DbAsete.clsHistorikAQTSeriale historik = new DbCore.DbAsete.clsHistorikAQTSeriale();
                        historik.merrHistorikAQTSerialSipasID(serialipazgjedhur.IdHistorikAktualPaSerial, idNdermarrje);
                        double sasidokmod = 0;
                        if (Double.Parse(Request.QueryString["iddokmag"]) > 0)
                            sasidokmod = DbCore.DbAsete.clsSerialetMagazine.merrSerialetMagazineSipasIDSerialIDDokSasi(int.Parse(Request.QueryString["iddokmag"]), serialipazgjedhur.IdAQTSerial, idNdermarrje);

                        if (historik.SasiaProgresive + sasidokmod <= Double.Parse(sasiteeserialeve[i].ToString()))
                                col.Remove(serialipazgjedhur);
                    }
                    else
                        col.Remove(serialipazgjedhur);
            }

            DbCore.mySessionObjects.ruajSerialeArtikulliNeSession(Session, col, id);
            DbCore.mySessionObjects.ruajSerialeArtikulliZgjedhurNeSession(Session, colzgjedhur, id);
            gvFushat.DataSource = col;
            gvZgjedhur.DataSource = colzgjedhur;
            gvZgjedhur.KeyFieldName = "IdAQTSerial";
            gvFushat.KeyFieldName = "IdAQTSerial";

            gvZgjedhur.DataBind();
            gvFushat.DataBind();
        }

        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {
            if (e.Item.Name == "OK")
            {
                int id = int.Parse(Request.QueryString["id"]);
                int lastsel = int.Parse(Request.QueryString["lastsel"]);
                DbCore.DbAsete.colAQTSeriale colzgjedhur = DbCore.mySessionObjects.merrSerialeArtikulliZgjedhurNgaSesioni(Session, id);
                JavaScriptSerializer serializusi = new JavaScriptSerializer();
                if (hfSeriale.Contains(id + "_" + lastsel))
                    //  hfSeriale.Clear();
                    hfSeriale.Set(id + "_" + lastsel, serializusi.Serialize(colzgjedhur));
                else hfSeriale.Add(id + "_" + lastsel, serializusi.Serialize(colzgjedhur));

            }

        }

        protected void gvZgjedhur_AfterPerformCallback(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            int id = int.Parse(Request.QueryString["id"]);
            DbCore.DbAsete.colAQTSeriale colzgjedhur = DbCore.mySessionObjects.merrSerialeArtikulliZgjedhurNgaSesioni(Session, id);

            gvZgjedhur.DataSource = colzgjedhur;

            gvZgjedhur.DataBind();

        }
        protected void gvFushat_AfterPerformCallback(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            int id = int.Parse(Request.QueryString["id"]);
            DbCore.DbAsete.colAQTSeriale col = DbCore.mySessionObjects.merrSerialeArtikulliNgaSesioni(Session, id);
            gvFushat.DataSource = col;


            gvFushat.DataBind();
        }
        protected void gvZgjedhur_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("gvZgjedhur"))
                {
                    if (e.Parameters.ToString() == "pastro")
                    {
                        DbCore.DbAsete.colAQTSeriale colzgjedhur1 = new DbCore.DbAsete.colAQTSeriale();
                        gvZgjedhur.DataSource = colzgjedhur1;
                        gvZgjedhur.DataBind();
                        return;
                    }
                    int id = int.Parse(Request.QueryString["id"]);
                    DbCore.DbAsete.colAQTSeriale col = DbCore.mySessionObjects.merrSerialeArtikulliNgaSesioni(Session, id);
                    DbCore.DbAsete.colAQTSeriale colzgjedhur = DbCore.mySessionObjects.merrSerialeArtikulliZgjedhurNgaSesioni(Session, id);
                    gvFushat.DataSource = col;
                    gvZgjedhur.DataSource = colzgjedhur;

                    gvZgjedhur.DataBind();
                    gvFushat.DataBind();
                }
            }

        }

        protected void gvFushat_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("gvFushat"))
                {
                    int id = int.Parse(Request.QueryString["id"]);
                    string vjennga = Request.QueryString["vjennga"];
                    string magazina = Request.QueryString["magazina"];
                    int lastsel = int.Parse(Request.QueryString["lastsel"]);
                    DbCore.DbAsete.colAQTSeriale col = DbCore.mySessionObjects.merrSerialeArtikulliNgaSesioni(Session, id);
                    DbCore.DbAsete.colAQTSeriale colzgjedhur = DbCore.mySessionObjects.merrSerialeArtikulliZgjedhurNgaSesioni(Session, id);
                    switch (e.Parameters.ToString())
                    {
                        case "djathtas1":

                            djathtasNje(col, colzgjedhur);
                            break;
                        case "djathtasgjithe":

                            djathtasGjithe(col, colzgjedhur);
                            break;
                        case "majtas1":

                            majtasNje(col, colzgjedhur);
                            break;
                        case "majtasgjithe":

                            majtasGjithe(col, colzgjedhur);
                            break;
                        case "pastro":
                            int idmag = DbCore.DbRegjistrim.clsNjesiAdministrative.ktheIdMagazine(magazina, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                            mbushPopUpListeNgaDB(id, lastsel, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), vjennga, idmag);
                            return;


                    }
                    gvFushat.DataSource = col;
                    gvZgjedhur.DataSource = colzgjedhur;

                    gvZgjedhur.DataBind();
                    gvFushat.DataBind();
                    DbCore.mySessionObjects.ruajSerialeArtikulliNeSession(Session, col, id);
                    DbCore.mySessionObjects.ruajSerialeArtikulliZgjedhurNeSession(Session, colzgjedhur, id);
                }
            }

        }

        private void djathtasNje(DbCore.DbAsete.colAQTSeriale col, DbCore.DbAsete.colAQTSeriale colzgjedhur)
        {
            decimal sasia = decimal.Parse(Request.QueryString["sasia"]);
            if (colzgjedhur.Count >= sasia)
                return;
            int index = gvFushat.FocusedRowIndex;
            object rreshtat = gvFushat.GetRowValues(index, "IdAQTSerial");
            DbCore.DbAsete.clsAQTSeriale serial = col.Find(bk => bk.IdAQTSerial == (int)rreshtat);
            colzgjedhur.Add(serial);
            col.Remove(serial);

        }

        private void majtasGjithe(DbCore.DbAsete.colAQTSeriale col, DbCore.DbAsete.colAQTSeriale colzgjedhur)
        {
            col.AddRange(colzgjedhur);
            colzgjedhur.Clear();

        }

        private void majtasNje(DbCore.DbAsete.colAQTSeriale col, DbCore.DbAsete.colAQTSeriale colzgjedhur)
        {
            int index = gvZgjedhur.FocusedRowIndex;
            object rreshtat = gvZgjedhur.GetRowValues(index, "IdAQTSerial");
            DbCore.DbAsete.clsAQTSeriale serial = colzgjedhur.Find(bk => bk.IdAQTSerial == (int)rreshtat);
            col.Add(serial);
            colzgjedhur.Remove(serial);

        }

        private void djathtasGjithe(DbCore.DbAsete.colAQTSeriale col, DbCore.DbAsete.colAQTSeriale colzgjedhur)
        {
            decimal sasia = decimal.Parse(Request.QueryString["sasia"]);
            if (colzgjedhur.Count + col.Count > sasia)
            {
                int index = 0;
                for (int i = colzgjedhur.Count; i < sasia; i++)
                {
                    if (index < col.Count)
                    {
                        colzgjedhur.Add(col[index]);
                        col.RemoveAt(index);
                    }

                }
            }
            else
            {
                colzgjedhur.AddRange(col);
                col.Clear();
            }
        }

        private void konfiguroGride()
        {
            const string emriKomponentes = "LupaSeriale.aspx";
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            GridUtil.percaktoVisibleColumns(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), gvZgjedhur, "gvFushat", emriKomponentes);
            GridUtil.konfiguroGrideRegjistrimEvogelPaTheme(gvZgjedhur, "IdAQTSerial");
            gvZgjedhur.Settings.UseFixedTableLayout = false;
            GridUtil.percaktoVisibleColumns(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), gvFushat, "gvFushat", emriKomponentes);
            GridUtil.konfiguroGrideRegjistrimEvogelPaTheme(gvFushat, "IdAQTSerial", false);
            gvFushat.Settings.UseFixedTableLayout = false;
            gvFushat.Settings.ShowFilterRow = true;
            gvFushat.Settings.ShowFilterRowMenu = true;
            gvFushat.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
            gvZgjedhur.Settings.ShowFilterRow = true;
            gvZgjedhur.Settings.ShowFilterRowMenu = true;
            gvZgjedhur.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
        }
        protected void gvZgjedhur_CustomJSProperties(object sender, DevExpress.Web.ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpNoRows"] = gvZgjedhur.VisibleRowCount;
            e.Properties["cpNoPage"] = gvZgjedhur.PageIndex;
        }

        protected void gvZgjedhur_DataBound(object sender, EventArgs e)
        {
            gvZgjedhur.SettingsBehavior.AllowSelectByRowClick = true;
            gvZgjedhur.KeyFieldName = "IdAQTSerial";
            gvZgjedhur.SettingsBehavior.AllowFocusedRow = true;
        }
        protected void gvFushat_CustomJSProperties(object sender, DevExpress.Web.ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpNoRows"] = gvFushat.VisibleRowCount;
            e.Properties["cpNoPage"] = gvFushat.PageIndex;
        }

        protected void gvFushat_DataBound(object sender, EventArgs e)
        {
            gvZgjedhur.SettingsBehavior.AllowSelectByRowClick = true;
            gvZgjedhur.KeyFieldName = "IdAQTSerial";
            gvZgjedhur.SettingsBehavior.AllowFocusedRow = true;
        }
    }
}