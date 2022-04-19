using DbCore;
using DbCore.DbAdmin;
using DbCore.DbAnalizeBuxheti;
using DevExpress.Web;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    public partial class ABKonfiguroShpenzimeOperative : MyPageBase
    {
        private int idNdermarrje;
        private int idKonfig;
        private int idPerdoruesi;
        private int idGjuha;
        private int idViti;
        private const string komponente = "ABKonfiguroShpenzimeOperative.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                DbCore.clsFunksione.logout(Session,true,"FaqePaautorizuar");
            }
            idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerdoruesi);
            }

            idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (!IsPostBack)
            {
                hfState.Set("idGjuha", idGjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idPerdoruesi", idPerdoruesi);
                hfState.Set("komponente", komponente);
                mbushGridenNgaDB(idNdermarrje);
                konfiguroGride();
            }
            else
            {
                //idNdermarrje = (int)hfState.Get("idNdermarrje");
                //idPerdoruesi = (int)hfState.Get("idPerdoruesi");
                //idGjuha = (int)hfState.Get("idGjuha");
                mbushGrideNgaSession(idNdermarrje);
            }
            shtoComboPrindi();
            percaktoTemplateMenu();
        }

        private void konfiguroGride()
        {
            GridUtil.percaktoVisibleColumnsMeWidth(idGjuha, idNdermarrje, gvKonfiguroShpenzimeOperative, "gvKonfiguroShpenzimeOperative", komponente);
            GridUtil.konfiguroGridaPerBatchEditing(gvKonfiguroShpenzimeOperative);
           
        }

        private void PercaktoTemplateComboPrindi(GridViewDataComboBoxColumn cmb, int idNdermarrje)
        {
           // object tmp = mySessionObjects.merrObjectNgaSesioni(Session, "shpenzimeKonfig");

            colShpenzimeOperativeKonfig col = new colShpenzimeOperativeKonfig(idNdermarrje);

            col.Add(new clsShpenzimeOperativeKonfig());
            cmb.PropertiesComboBox.DataSource = col;

            ListBoxColumn colprove = new ListBoxColumn();
            colprove.FieldName = "Kodi";
            colprove.Caption = "Kodi";
            ListBoxColumn colemer = new ListBoxColumn();
            colemer.FieldName = "Pershkrimi";
            colemer.Caption = "Pershkrimi";
            colemer.Width = 150;
            cmb.PropertiesComboBox.Columns.Add(colprove);
            cmb.PropertiesComboBox.Columns.Add(colemer);
            cmb.PropertiesComboBox.ValueField = "ShokId";
            cmb.PropertiesComboBox.TextField = "Pershkrimi";
            cmb.PropertiesComboBox.TextFormatString = "{0}";
            // combo.TextField = "KodKonfigAmbjente";
            cmb.PropertiesComboBox.IncrementalFilteringMode = DevExpress.Web.IncrementalFilteringMode.Contains;
        }

        /// <summary>
        /// mbush datasourcein e grides direkt nga db
        /// </summary>
        /// <param name="idNdermarrje"></param>
        private void mbushGridenNgaDB(int idNdermarrje)
        {
            colShpenzimeOperativeKonfig col = new colShpenzimeOperativeKonfig(idNdermarrje);
            gvKonfiguroShpenzimeOperative.DataSource = col;
            gvKonfiguroShpenzimeOperative.DataBind();
            gvKonfiguroShpenzimeOperative.JSProperties["cpshpenzime"] = Newtonsoft.Json.JsonConvert.SerializeObject(col.Select(x => x.Kodi));
            
            mySessionObjects.ruajObjectNeSesion(Session, col,"shpenzimeOperativeKonfig");
        }

        /// <summary>
        /// merr ds e grides nga sessioni
        /// </summary>
        public void mbushGrideNgaSession(int idNdermarrje)
        {
            colShpenzimeOperativeKonfig tmp = (colShpenzimeOperativeKonfig) mySessionObjects.merrObjectNgaSesioni(Session, "shpenzimeOperativeKonfig");
            gvKonfiguroShpenzimeOperative.DataSource = (tmp as colShpenzimeOperativeKonfig) ?? new colShpenzimeOperativeKonfig(idNdermarrje);
            gvKonfiguroShpenzimeOperative.DataBind();
            gvKonfiguroShpenzimeOperative.JSProperties["cpshpenzime"] = Newtonsoft.Json.JsonConvert.SerializeObject(tmp.Select(x => x.Kodi));
           
        }

        private void percaktoTemplateMenu()
        {
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdoruesi, idNdermarrje, ASPxMenu1, komponente, this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, false, true, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session), true);
        }

        public void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
        }

        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
        }

        private void shtoComboPrindi()
        {

            var col = gvKonfiguroShpenzimeOperative.Columns["IdPrindi"];

            GridViewDataComboBoxColumn cmb = new GridViewDataComboBoxColumn
            {
                FieldName = "IdPrindi",
                Name = "IdPrindi",
                Width = col.Width,
                Caption = "Kodi i prindit"
            };
            cmb.HeaderStyle.BackColor = col.HeaderStyle.BackColor;
            cmb.VisibleIndex = col.VisibleIndex;

            gvKonfiguroShpenzimeOperative.Columns.Remove(col);
            PercaktoTemplateComboPrindi(cmb, idNdermarrje);
            gvKonfiguroShpenzimeOperative.Columns.Add(cmb);
        }


        protected void gvKonfiguroShpenzimeOperative_BatchUpdate(object sender, DevExpress.Web.Data.ASPxDataBatchUpdateEventArgs e)
        {
            try
            {
                int idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
                int idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);
                object tmp = mySessionObjects.merrObjectNgaSesioni(Session, "shpenzimeOperativeKonfig");
                clsMesazh mesazhi = new clsMesazh(true);

                clsTeDrejtaRoli teDrejta = new clsTeDrejtaRoli();
                teDrejta.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, komponente);
                colShpenzimeOperativeKonfig col = (tmp as colShpenzimeOperativeKonfig) ?? new colShpenzimeOperativeKonfig(idNdermarrje);
                colShpenzimeOperativeKonfig newCol = new colShpenzimeOperativeKonfig(idNdermarrje);
                if (!teDrejta.DMod)
                {
                    mesazhi = new clsMesazh(false, MessagesResource.Messages["msgNukKeniTeDrejta"]);
                }
                else
                {


                    for (int i = 0, count = e.UpdateValues.Count; i < count; i++)
                    {
                        clsShpenzimeOperativeKonfig oldShpenzime = col.FirstOrDefault(x => x.ShokId == e.UpdateValues[i].MerrKeyValue<int>());
                        clsShpenzimeOperativeKonfig newShpenzime = e.UpdateValues[i].MerrCustomUpdatedObject(oldShpenzime);
                        newShpenzime.IdModifikuesi = idPerdoruesi;
                        mesazhi = newShpenzime.Modifiko(newCol);
                    }

                    for (int i = 0; i < e.InsertValues.Count; i++)
                    {
                        clsShpenzimeOperativeKonfig newShpenzime = new clsShpenzimeOperativeKonfig
                        {
                            IdKrijuesi = idPerdoruesi,
                            IdNdermarrje = idNdermarrje
                        };

                        newShpenzime = e.InsertValues[i].MerrCustomInsertedObject(newShpenzime);
                        // newShpenzime.Niveli = AnalizeBuxheti.ktheNivelShpenzimiOperativ(newCol, newShpenzime);
                        if (newShpenzime.Niveli == 0)
                            newShpenzime.Niveli = 1;
                        mesazhi = newShpenzime.Ruaj();
                        if (mesazhi.Status)
                            col.Add(newShpenzime);

                    }

                    for (int i = 0; i < e.DeleteValues.Count; i++)
                    {
                        int key = e.DeleteValues[i].MerrKeyValue<int>();
                        if (AnalizeBuxheti.KaVeprimeShpenzimi(key))
                        {
                            throw new Exception("Ka veprime me kete shpenzim operativ!");
                        }
                        mesazhi = clsShpenzimeOperativeKonfig.Fshi(key, idPerdoruesi);
                        mesazhi = clsShpenzimeOperative.FshiUpdateShpenzimeOperative(key, idPerdoruesi);
                        if (mesazhi.Status) col.RemoveAll(x => x.ShokId == key);
                    }
                    if (mesazhi.Status)
                    {
                        mesazhi = col.PerditesoNiveletEShpenzimeveOperative(new clsDatabaseAnalizeBuxheti());
                        if (mesazhi.Status)
                        {
                            mesazhi = AnalizeBuxheti.RuajShpenzimeOperative(idNdermarrje);
                            if (mesazhi.Status)
                            {
                                mySessionObjects.ruajObjectNeSesion(Session, col, "shpenzimeOperativeKonfig");
                                shtoComboPrindi();
                            }
                        }
                    }
                }
                if (mesazhi.Status)
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ruajtja u krye me sukses!:Green");
                else
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, mesazhi.PershkrimMesazhi + ":Red");

                gvKonfiguroShpenzimeOperative.DataSource = col;
                gvKonfiguroShpenzimeOperative.DataBind();




            }
            catch (Exception err)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session,  err.Message + ":Red");
            }
            e.Handled = true;
        }

        protected void gvKonfiguroShpenzimeOperative_DataBound(object sender, EventArgs e)
        {
            if (gvKonfiguroShpenzimeOperative.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per
                //perzgjidh
                GridViewCommandColumn check = new GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true;
                check.Width = Unit.Percentage(2);
                gvKonfiguroShpenzimeOperative.Settings.ShowFilterRow = false;
                gvKonfiguroShpenzimeOperative.Settings.ShowFilterBar = GridViewStatusBarMode.Hidden;
                gvKonfiguroShpenzimeOperative.Settings.ShowFilterRowMenu = false;
                check.VisibleIndex = 0;
                gvKonfiguroShpenzimeOperative.Columns.Insert(0, check);

                gvKonfiguroShpenzimeOperative.KeyFieldName = "ShokId";
                gvKonfiguroShpenzimeOperative.SettingsBehavior.AllowSelectByRowClick = true;
                gvKonfiguroShpenzimeOperative.SettingsBehavior.AllowFocusedRow = true;
            }
            
        }

     
        protected void gvKonfiguroShpenzimeOperative_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters.Length > 0)
            {
                if (e.Parameters.Contains("Riruaj"))
                {
                    clsMesazh mesazhi = AnalizeBuxheti.RuajShpenzimeOperative(mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    if (mesazhi.Status)
                        DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ruajtja u krye me sukses!:Green");
                    else
                        DbCore.mySessionObjects.ruajMesazhNeSesion(Session, mesazhi.PershkrimMesazhi + ":Red");
                }
                // mbushGridenNgaDB(mySessionObjects.merrIdNdermarrjeSesioni(Session), Convert.ToInt32(cmbAmbjenti.Value));
            }
        }

        protected void gvKonfiguroShpenzimeOperative_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {

            List<string> kodet = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(Convert.ToString(gvKonfiguroShpenzimeOperative.JSProperties["cpshpenzime"]).ToLower());

            colShpenzimeOperativeKonfig col = (mySessionObjects.merrObjectNgaSesioni(Session, "konfigFushash") as colShpenzimeOperativeKonfig) ?? new colShpenzimeOperativeKonfig(idNdermarrje);

            string newKodi = e.NewValues["Kodi"].ToString().ToLower();
            string oldKodi = Convert.ToString(e.OldValues["Kodi"]).ToLower();

            if (!e.IsNewRow && newKodi == oldKodi)
                return;


            if (kodet.Contains(newKodi))
            {
                e.Errors[gvKonfiguroShpenzimeOperative.Columns["Kodi"]] = "Ky shpenzim nuk eshte ruajtur pasi kodi i tij eshte perdorur ne gride";

            }
            else
            {
                kodet.Add(newKodi);
                if (!e.IsNewRow)
                    kodet.Remove(oldKodi);
                gvKonfiguroShpenzimeOperative.JSProperties["cpshpenzime"] = Newtonsoft.Json.JsonConvert.SerializeObject(kodet);
            }
        }
    }
}