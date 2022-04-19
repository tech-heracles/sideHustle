using DbCore;
using DbCore.DbAdmin;
using DbCore.DbAnalizeBuxheti;
using DevExpress.Web;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Text;
using DbCore.IMBUtils.Logging;
using DevExpress.Web.Data;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    public partial class ABKonfiguroFusha : MyPageBase
    {
        private int idNdermarrje;
        private int idKonfig;
        private int idPerdoruesi;
        private int idGjuha;
        private int idViti;
        private int idNdermVit;
        private const string komponente = "ABKonfiguroFusha.aspx";

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

            if(!clsNdermarrje.EshteRaportuese(idNdermarrje))
                DbCore.clsFunksione.logout(Session,true,"FaqePaautorizuar");
           
            idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            idNdermVit = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (!IsPostBack)
            {
                hfState.Set("idGjuha", idGjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idPerdoruesi", idPerdoruesi);
                hfState.Set("komponente", komponente);
                hfState.Set("idNdermVit", idNdermVit);
                mbushKomboAmbjentesh();

                
                cmbAmbjenti.SelectedIndex = 0;
                hfKonffillestar.Value = Convert.ToString(cmbAmbjenti.Value);
                mbushGridenNgaDB(idNdermarrje, Convert.ToInt32(cmbAmbjenti.Value));
                konfiguroGride(gvKonfiguroFusha, "gvKonfiguroFusha", true);
                mbushGridenNgaDBShpenzimeOperative(idNdermarrje);
                konfiguroGride(gvKonfiguroShpenzimeOperative, "gvKonfiguroShpenzimeOperative");
                mbushGridenNgaDBParashikimShpenzimesh(idNdermarrje);
                konfiguroGride(gvKonfigParashikimShpenzimesh, "gvKonfigParashikimShpenzimesh");
                mbushGridenNgaDBZeraProkurorimesh(idNdermarrje);
                konfiguroGride(gvKonfiguroZeraProkurimesh, "gvKonfiguroZeraProkurimesh");
            }
            else
            {
                idNdermarrje = (int)hfState.Get("idNdermarrje");
                idPerdoruesi = (int)hfState.Get("idPerdoruesi");
                idGjuha = (int)hfState.Get("idGjuha");
                idNdermVit = (int)hfState.Get("idNdermVit");
                mbushGrideNgaSession(idNdermarrje, Convert.ToInt32(cmbAmbjenti.Value));
                mbushGrideNgaSessionShpenzimeOperative(idNdermarrje);
                mbushGrideNgaSessionParashikimShpenzimesh();
                mbushGrideNgaSessionZeraProkurorimesh(idNdermarrje);
            }
            GridUtil.KtheKolonenNeComboNeGride(gvKonfiguroShpenzimeOperative, "IdPrindi", "Kodi i prindit", PercaktoTemplateComboPrindi);
            shtoCheckColumnEshtePrindParashikimShpenzimesh();
            GridUtil.KtheKolonenNeComboNeGride(gvKonfigParashikimShpenzimesh, "IdPrindi", "Kodi i prindit", PercaktoTemplateComboPrindiParashikimShpenzimesh);
            GridUtil.KtheKolonenNeComboNeGride(gvKonfiguroZeraProkurimesh, "IdPrindi", "Kodi i prindit", PercaktoTemplateComboPrindiZeraProkurorimesh);

            percaktoTemplateMenu();
        }

        private void konfiguroGride(ASPxGridView grida, string emerGride, bool CommandColumn = false)
        {
            GridUtil.percaktoVisibleColumnsMeWidth(idGjuha, idNdermarrje, grida, emerGride, komponente);
            GridUtil.konfiguroGridaPerBatchEditing(grida);
            if (!CommandColumn)
                return;
            grida.SettingsCommandButton.DeleteButton.ButtonType = GridCommandButtonRenderMode.Image;
            grida.SettingsCommandButton.DeleteButton.Image.Url = @"images/new/button_cancel-32.png";
            grida.Columns.Add(new GridViewCommandColumn { ShowDeleteButton = true, Width = 15, VisibleIndex = grida.Columns.Count });
        }

       

      

        #region Ambjentet


        private void mbushKomboAmbjentesh()
        {

            
            ListBoxColumn colprove = new ListBoxColumn();
            colprove.FieldName = "Kodi";
            colprove.Caption = "Kodi";
            ListBoxColumn colemer = new ListBoxColumn();
            colemer.FieldName = "Pershkrim";
            colemer.Caption = "Pershkrimi";
            colemer.Width = 250;
            colAmbjenti ambjentet = new colAmbjenti();
            ambjentet.Add(new clsAmbjenti(4, "PBSHO", "PROJEKT BUXHETI NE ZERIN E SHPENZIMEVE OPERATIVE"));
            cmbAmbjenti.DataSource = ambjentet; // new colAmbjenti();

            cmbAmbjenti.TextFormatString = "{0} ({1})";
            cmbAmbjenti.Columns.Add(colprove);
            cmbAmbjenti.Columns.Add(colemer);
            cmbAmbjenti.ValueField = "IdAmbjenti";
            //cmbAmbjenti.TextField = "Pershkrim";

            // combo.TextField = "KodKonfigAmbjente";
            cmbAmbjenti.ClientSideEvents.KeyUp = @"function(s,e) { var keyCode = e.htmlEvent.keyCode; if((keyCode==46 || keyCode==8) && s.GetInputElement().value == '')  s.SetSelectedIndex(-1); }";
            cmbAmbjenti.DropDownStyle = DropDownStyle.DropDownList;
            cmbAmbjenti.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            cmbAmbjenti.DataBind();
        }

        /// <summary>
        /// mbush datasourcein e grides direkt nga db
        /// </summary>
        /// <param name="idNdermarrje"></param>
        private void mbushGridenNgaDB(int idNdermarrje, int idAmbjenti)
        {
            colRreshtaAmbjenti col = new colRreshtaAmbjenti(idNdermarrje, idAmbjenti);
            gvKonfiguroFusha.DataSource = col;
            gvKonfiguroFusha.DataBind();
            gvKonfiguroFusha.JSProperties["cprreshta"] = Newtonsoft.Json.JsonConvert.SerializeObject(col.Select(x=>x.KodiRreshtit));
            mySessionObjects.ruajObjectNeSesion(Session, col, "konfigFushash");
        }

        /// <summary>
        /// merr ds e grides nga sessioni
        /// </summary>
        public void mbushGrideNgaSession(int idNdermarrje, int idAmbjenti)
        {
            colRreshtaAmbjenti tmp = (colRreshtaAmbjenti)mySessionObjects.merrObjectNgaSesioni(Session, "konfigFushash");
            if (tmp == null)
            {
                tmp = new colRreshtaAmbjenti(idNdermarrje, idAmbjenti);
            }
            gvKonfiguroFusha.DataSource = tmp;
            gvKonfiguroFusha.DataBind();
            gvKonfiguroFusha.JSProperties["cprreshta"] = Newtonsoft.Json.JsonConvert.SerializeObject(tmp.Select(x => x.KodiRreshtit));
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

        protected void gvKonfiguroFusha_BatchUpdate(object sender, DevExpress.Web.Data.ASPxDataBatchUpdateEventArgs e)
        {
            try
            {
                int idAmbjenti = Convert.ToInt32(cmbAmbjenti.Value);
                int idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
                int idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);
                object tmp = mySessionObjects.merrObjectNgaSesioni(Session, "konfigFushash");
                clsMesazh mesazhi = new clsMesazh(true);

                

                colRreshtaAmbjenti col = (tmp as colRreshtaAmbjenti) ?? new colRreshtaAmbjenti(idNdermarrje, idAmbjenti);

                for (int i = 0; i < e.UpdateValues.Count; i++)
                {
                    //int key = Convert.ToInt32(e.UpdateValues[i].Keys[0]);
                    clsRreshtaAmbjenti oldRreshti = col.FirstOrDefault(x => x.RreshtiId == e.UpdateValues[i].MerrKeyValue<int>());
                    //viti i ndermarrjes duhet ndryshuar gjithashtu
                    clsRreshtaAmbjenti newRreshti = e.UpdateValues[i].MerrCustomUpdatedObject<clsRreshtaAmbjenti>(oldRreshti);
                    newRreshti.IdModifikuesi = idPerdoruesi;
                    mesazhi = newRreshti.Modifiko();
                }

                for (int i = 0; i < e.InsertValues.Count; i++)
                {
                    clsRreshtaAmbjenti newRreshti = new clsRreshtaAmbjenti
                    {
                        IdKrijuesi = idPerdoruesi,
                        IdAmbjenti = idAmbjenti,
                        IdNdermarrje = idNdermarrje,
                        IdNdermVit = idNdermVit
                    };

                    newRreshti = e.InsertValues[i].MerrCustomInsertedObject(newRreshti);
                  
                        mesazhi = newRreshti.Ruaj();
                        if (mesazhi.Status)
                            col.Add(newRreshti);
                    
                    
                }

                foreach (ASPxDataDeleteValues deleted in e.DeleteValues)
                {
                    int key = Convert.ToInt32(deleted.Keys[0]);
                    if(DbCore.DbAnalizeBuxheti.AnalizeBuxheti.KaVeprimeRreshti(idAmbjenti,key))
                    {
                        throw new Exception("Me kete rresht ka veprime!");
                    }
                    mesazhi = clsRreshtaAmbjenti.Fshi(key, idAmbjenti);
                    if (mesazhi.Status) col.RemoveAll(x => x.RreshtiId == key);
                }

                mySessionObjects.ruajObjectNeSesion(Session, col, "konfigFushash");
                if (mesazhi.Status)
                {
                    AnalizeBuxheti.RuajKonfigurimRreshtash(idAmbjenti, idNdermarrje);
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ruajtja u krye me sukses!:Green");
                }
                else
                {
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ndodhi nje gabim gjate ruajtes se zerave!:Red");
                }
                gvKonfiguroFusha.DataSource = col;
                gvKonfiguroFusha.DataBind();
           
            }
            catch (Exception err)
            {
                ImbLogger.Error(err.Message);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session,  err.Message + ":Red");
            }
                e.Handled = true;
        }

        protected void gvKonfiguroFusha_DataBound(object sender, EventArgs e)
        {
            GridUtil.ShtoCommandColumnNeDatabound((ASPxGridView)sender, "#", "RreshtiId");
            
        }

        protected void gvKonfiguroFusha_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters.Length > 0)
            {
                idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
                int idAmbjenti = Convert.ToInt32(cmbAmbjenti.Value);
                
                if(idAmbjenti == 8)
                {
                    gvKonfiguroFusha.Columns["NjesiaMatese"].Visible = true;
                }else
                    gvKonfiguroFusha.Columns["NjesiaMatese"].Visible = false;
                if (e.Parameters.Contains("Riruaj"))
                {
                    //riruan konfigurimin e rreshtave
                    clsMesazh mesazh = AnalizeBuxheti.RuajKonfigurimRreshtash(idAmbjenti, idNdermarrje);
                    if (mesazh.Status)
                        mySessionObjects.ruajMesazhNeSesion(Session, "Konfigurimi u ruajt me sukses!:Green");
                    else
                        mySessionObjects.ruajMesazhNeSesion(Session, mesazh.PershkrimMesazhi + ":Red");

                }
                else if (idAmbjenti == 4)
                {
                    Tabs.ActiveTabIndex = 2;
                }
                else if (idAmbjenti == 3)
                {
                    Tabs.ActiveTabIndex = 1;
                }else if(idAmbjenti == 12)
                {
                    Tabs.ActiveTabIndex = 3;
                }
                else
                {

                    mbushGridenNgaDB(idNdermarrje, idAmbjenti);
                }
            }
        }

        protected void gvKonfiguroFusha_CustomErrorText(object sender, ASPxGridViewCustomErrorTextEventArgs e)
        {

        }

        protected void gvKonfiguroFusha_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {

            List<string> kodet = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(Convert.ToString(gvKonfiguroFusha.JSProperties["cprreshta"]).ToLower());

            colRreshtaAmbjenti col = (mySessionObjects.merrObjectNgaSesioni(Session, "konfigFushash") as colRreshtaAmbjenti) ?? new colRreshtaAmbjenti(idNdermarrje, Convert.ToInt16(cmbAmbjenti.Value));

            string newKodi = e.NewValues["KodiRreshtit"].ToString().ToLower();
            string oldKodi = Convert.ToString(e.OldValues["KodiRreshtit"]).ToLower();

            if (!e.IsNewRow && newKodi==oldKodi)
                return;


            if (kodet.Contains(newKodi))
            {
                e.Errors[gvKonfiguroFusha.Columns["KodiRreshtit"]] = "Ky ze nuk eshte ruajtur pasi kodi i tij eshte perdorur ne gride";
                
            }
            else
            {
                kodet.Add(newKodi);
                if (!e.IsNewRow)
                    kodet.Remove(oldKodi);
                gvKonfiguroFusha.JSProperties["cprreshta"] = Newtonsoft.Json.JsonConvert.SerializeObject(kodet);
                
            }



        }

        #endregion Ambjentet

       

        #region konfiguro shpenzime operative

        private void mbushGridenNgaDBShpenzimeOperative(int idNdermarrje)
        {
            
            colShpenzimeOperativeKonfig col = new colShpenzimeOperativeKonfig(idNdermarrje);
            gvKonfiguroShpenzimeOperative.DataSource = col;
            gvKonfiguroShpenzimeOperative.DataBind();
            gvKonfiguroShpenzimeOperative.JSProperties["cpshpenzime"] = Newtonsoft.Json.JsonConvert.SerializeObject(col.Select(x => x.Kodi));

            mySessionObjects.ruajObjectNeSesion(Session, col, "shpenzimeOperativeKonfig");
        }

        /// <summary>
        /// merr ds e grides nga sessioni
        /// </summary>
        public void mbushGrideNgaSessionShpenzimeOperative(int idNdermarrje)
        {
            colShpenzimeOperativeKonfig tmp = (colShpenzimeOperativeKonfig)mySessionObjects.merrObjectNgaSesioni(Session, "shpenzimeOperativeKonfig");
            gvKonfiguroShpenzimeOperative.DataSource = (tmp as colShpenzimeOperativeKonfig) ?? new colShpenzimeOperativeKonfig(idNdermarrje);
            gvKonfiguroShpenzimeOperative.DataBind();
            gvKonfiguroShpenzimeOperative.JSProperties["cpshpenzime"] = Newtonsoft.Json.JsonConvert.SerializeObject(tmp.Select(x => x.Kodi));

        }
    

       

        private void PercaktoTemplateComboPrindi(GridViewDataComboBoxColumn cmb)
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
        protected void gvKonfiguroShpenzimeOperative_BatchUpdate(object sender, DevExpress.Web.Data.ASPxDataBatchUpdateEventArgs e)
        {
            try
            {

                //int idAmbjenti = Convert.ToInt32(cmbAmbjenti.Value);
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
                        // int key = Convert.ToInt32(e.UpdateValues[i].Keys[0]);
                        clsShpenzimeOperativeKonfig oldShpenzime = col.Where(x => x.ShokId == e.UpdateValues[i].MerrKeyValue<int>()).FirstOrDefault();
                        
                        clsShpenzimeOperativeKonfig newShpenzime = e.UpdateValues[i].MerrCustomUpdatedObject(oldShpenzime);
                        newShpenzime.IdModifikuesi = idPerdoruesi;
                        //newShpenzime.Niveli = newShpenzime.NiveliPrindit;
                        mesazhi = newShpenzime.Modifiko(newCol);


                    }

                    for (int i = 0; i < e.InsertValues.Count; i++)
                    {
                        clsShpenzimeOperativeKonfig newShpenzime = new clsShpenzimeOperativeKonfig
                        {
                            IdKrijuesi = idPerdoruesi,
                            IdNdermarrje = idNdermarrje,
                            IdNdermVit = idNdermVit
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
                                GridUtil.KtheKolonenNeComboNeGride(gvKonfiguroShpenzimeOperative, "IdPrindi", "Kodi i prindit", PercaktoTemplateComboPrindi);
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
                ImbLogger.Error(err.Message);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, err.Message + ":Red");
            }
            e.Handled = true;
        }

        protected void gvKonfiguroShpenzimeOperative_DataBound(object sender, EventArgs e)
        {
            GridUtil.ShtoCommandColumnNeDatabound((ASPxGridView)sender, "#", "ShokId");
           

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

            colShpenzimeOperativeKonfig col = (mySessionObjects.merrObjectNgaSesioni(Session, "shpenzimeOperativeKonfig") as colShpenzimeOperativeKonfig) ?? new colShpenzimeOperativeKonfig(idNdermarrje);

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
        #endregion

        #region konfiguro parashikim shpenzimesh per personelin
        private void mbushGridenNgaDBParashikimShpenzimesh(int idNdermarrje)
        {
            colParashikimShpenzPersoneliConfig col = new colParashikimShpenzPersoneliConfig(idNdermarrje);
            gvKonfigParashikimShpenzimesh.DataSource = col;
            gvKonfigParashikimShpenzimesh.DataBind();
            gvKonfigParashikimShpenzimesh.JSProperties["cpParashikim"] = Newtonsoft.Json.JsonConvert.SerializeObject(col.Select(x => x.Kodi));
             mySessionObjects.ruajObjectNeSesion(Session, col, "parashikimShpenzKonfig");
        }

        /// <summary>
        /// merr ds e grides nga sessioni
        /// </summary>
        public void mbushGrideNgaSessionParashikimShpenzimesh()
        {
            object tmp = mySessionObjects.merrObjectNgaSesioni(Session, "parashikimShpenzKonfig");
            colParashikimShpenzPersoneliConfig col = (tmp as colParashikimShpenzPersoneliConfig) ?? new colParashikimShpenzPersoneliConfig(idNdermarrje);
            gvKonfigParashikimShpenzimesh.DataSource = tmp;
            gvKonfigParashikimShpenzimesh.DataBind();
            gvKonfigParashikimShpenzimesh.JSProperties["cpParashikim"] = Newtonsoft.Json.JsonConvert.SerializeObject(col.Select(x => x.Kodi));
            
        }

        

        private void PercaktoTemplateComboPrindiParashikimShpenzimesh(GridViewDataComboBoxColumn cmb)
        {
            // object tmp = mySessionObjects.merrObjectNgaSesioni(Session, "shpenzimeKonfig");

            colParashikimShpenzPersoneliConfig col = new colParashikimShpenzPersoneliConfig(idNdermarrje, true);

            col.Add(new clsParashikimShpenzPersoneliConfig());
            cmb.PropertiesComboBox.DataSource = col;

            ListBoxColumn colprove = new ListBoxColumn();
            colprove.FieldName = "Kodi";
            colprove.Caption = "Kodi";
            ListBoxColumn colemer = new ListBoxColumn();
            colemer.FieldName = "Emertimi";
            colemer.Caption = "Emertimi";
            colemer.Width = 150;
            cmb.PropertiesComboBox.Columns.Add(colprove);
            cmb.PropertiesComboBox.Columns.Add(colemer);
            cmb.PropertiesComboBox.ValueField = "PShPConfigId";
            cmb.PropertiesComboBox.TextField = "Kodi";
            cmb.PropertiesComboBox.TextFormatString = "{0}";
            // combo.TextField = "KodKonfigAmbjente";
            cmb.PropertiesComboBox.IncrementalFilteringMode = DevExpress.Web.IncrementalFilteringMode.Contains;
        }

        private void shtoCheckColumnEshtePrindParashikimShpenzimesh()
        {

            var col = gvKonfigParashikimShpenzimesh.Columns["Prind"];
            GridViewDataCheckColumn check = new GridViewDataCheckColumn
            {
                FieldName = "Prind",
                Name = "Prind",
                Width = col.Width,
                Caption = "Prind"
            };
            check.HeaderStyle.BackColor = col.HeaderStyle.BackColor;
            check.VisibleIndex = col.VisibleIndex;

            gvKonfigParashikimShpenzimesh.Columns.Remove(col);
            gvKonfigParashikimShpenzimesh.Columns.Add(check);

        }

       

      
        protected void gvKonfigParashikimShpenzimesh_DataBound(object sender, EventArgs e)
        {
            GridUtil.ShtoCommandColumnNeDatabound((ASPxGridView)sender, "#", "RreshtiId");

        }

        protected void gvKonfigParashikimShpenzimesh_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {

            if (e.Parameters.Contains("Riruaj"))
            {
                clsMesazh mesazhi = AnalizeBuxheti.RuajKonfigurimPerShpenzimePersoneli(mySessionObjects.merrIdNdermarrjeSesioni(Session));
                if (mesazhi.Status)
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ruajtja u krye me sukses!:Green");
                else
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, mesazhi.PershkrimMesazhi + ":Red");
            }
        }

        protected void gvKonfigParashikimShpenzimesh_BatchUpdate(object sender, DevExpress.Web.Data.ASPxDataBatchUpdateEventArgs e)
        {
            try
            {
                clsMesazh mesazh = new clsMesazh(true);
                object tmp = mySessionObjects.merrObjectNgaSesioni(Session, "parashikimShpenzKonfig");
                clsTeDrejtaRoli teDrejta = new clsTeDrejtaRoli();
                teDrejta.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, komponente);
                colParashikimShpenzPersoneliConfig col = (tmp as colParashikimShpenzPersoneliConfig) ?? new colParashikimShpenzPersoneliConfig(idNdermarrje);
                int shpenzimeJoTeRuajtur = 0;
                if (!teDrejta.DMod)
                {
                    mesazh = new clsMesazh(false, MessagesResource.Messages["msgNukKeniTeDrejta"]);
                }
                else
                {
                    for (int i = 0, teUpdatetuara = e.UpdateValues.Count; i < teUpdatetuara; i++)
                    {
                        //string key = e.UpdateValues[i].Keys[0].ToString();
                        clsParashikimShpenzPersoneliConfig konfigVjeter = col.Where(x => x.RreshtiId == e.UpdateValues[i].MerrKeyValue<int>()).FirstOrDefault();

                        clsParashikimShpenzPersoneliConfig konfigUpdated = e.UpdateValues[i].MerrCustomUpdatedObject(konfigVjeter);
                        if(konfigUpdated.Prind && konfigUpdated.IdPrindi > 0)
                        {
                            
                            shpenzimeJoTeRuajtur++;
                            continue;
                        }
                        konfigUpdated.IdModifikuesi = idPerdoruesi;
                        mesazh = konfigUpdated.Modifiko();
                    }

                    for (int i = 0; i < e.InsertValues.Count; i++)
                    {
                        clsParashikimShpenzPersoneliConfig newShpenzime = new clsParashikimShpenzPersoneliConfig
                        {
                            IdKrijuesi = idPerdoruesi,
                            IdNdermarrje = idNdermarrje,
                            IdNdermVit = idNdermVit
                        };

                        newShpenzime = e.InsertValues[i].MerrCustomInsertedObject(newShpenzime);
                        if (newShpenzime.Prind && newShpenzime.IdPrindi > 0)
                        {
                            
                            shpenzimeJoTeRuajtur++;
                            continue;
                        }
                        mesazh = newShpenzime.Ruaj();
                        if (mesazh.Status)
                            col.Add(newShpenzime);

                    }

                    for (int i = 0; i < e.DeleteValues.Count; i++)
                    {
                        int key = e.DeleteValues[i].MerrKeyValue<int>();
                        if (AnalizeBuxheti.KaVeprimeParaShikimiIShpenzimeve(key))
                        {
                            throw new Exception("Ka veprime me kete shpenzim !");
                        }

                        if (AnalizeBuxheti.EshtePerdorurKyZePrindParashikimShpenzimesh(key))
                        {
                            throw new Exception("Ky prind eshte i lidhur me shpenzime bija");
                        }
                        mesazh = clsParashikimShpenzPersoneliConfig.Fshi(key);
                        //  mesazh = clsParashikimShpenzPersoneliConfig.FshiUpdateShpenzimeOperative(key, idPerdoruesi);
                        if (mesazh.Status) col.RemoveAll(x => x.PShPConfigId == key);
                    }

                    if (mesazh.Status)
                    {
                        mesazh = AnalizeBuxheti.RuajKonfigurimPerShpenzimePersoneli(idNdermarrje);
                        if (mesazh.Status)
                            mySessionObjects.ruajObjectNeSesion(Session, col, "parashikimShpenzKonfig");
                    }
                }
                if(shpenzimeJoTeRuajtur > 0)
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Prinderve nuk duhet tu caktohen zera prind!:Red");
                else if (mesazh.Status)
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Modifikimi u krye me sukses!:Green");
                else
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, $"{mesazh.PershkrimMesazhi}:Red");

                gvKonfigParashikimShpenzimesh.DataSource = col;
                gvKonfigParashikimShpenzimesh.DataBind();
                GridUtil.KtheKolonenNeComboNeGride(gvKonfigParashikimShpenzimesh, "IdPrindi", "Kodi i prindit", PercaktoTemplateComboPrindiParashikimShpenzimesh);
                
            }
            catch (Exception err)
            {
                ImbLogger.Error(err.Message);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, err.Message + "!:Red");
            }
            e.Handled = true;
        }

        protected void gvKonfigParashikimShpenzimesh_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {

            List<string> kodet = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(Convert.ToString(gvKonfigParashikimShpenzimesh.JSProperties["cpParashikim"]).ToLower());

            colShpenzimeOperativeKonfig col = (mySessionObjects.merrObjectNgaSesioni(Session, "konfigFushash") as colShpenzimeOperativeKonfig) ?? new colShpenzimeOperativeKonfig(idNdermarrje);

            string newKodi = e.NewValues["Kodi"].ToString().ToLower();
            string oldKodi = Convert.ToString(e.OldValues["Kodi"]).ToLower();

            if (!e.IsNewRow && newKodi == oldKodi)
                return;


            if (kodet.Contains(newKodi))
            {
                e.Errors[gvKonfigParashikimShpenzimesh.Columns["Kodi"]] = "Ky shpenzim nuk eshte ruajtur pasi kodi i tij eshte perdorur ne gride";

            }
            else
            {
                kodet.Add(newKodi);
                if (!e.IsNewRow)
                    kodet.Remove(oldKodi);
                gvKonfiguroShpenzimeOperative.JSProperties["cpParashikim"] = Newtonsoft.Json.JsonConvert.SerializeObject(kodet);
            }



            
        }


        #endregion

        #region konfiguro zerat e prokurimeve publike
        private void mbushGridenNgaDBZeraProkurorimesh(int idNdermarrje)
        {

            colRealizimProkurimeshKonfig col = new colRealizimProkurimeshKonfig(idNdermarrje);
            gvKonfiguroZeraProkurimesh.DataSource = col;
            gvKonfiguroZeraProkurimesh.DataBind();
            gvKonfiguroZeraProkurimesh.JSProperties["cpprokurimesh"] = Newtonsoft.Json.JsonConvert.SerializeObject(col.Select(x => x.Kodi));

            mySessionObjects.ruajObjectNeSesion(Session, col, "zeraProkurorimeshKonfig");
        }

        /// <summary>
        /// merr ds e grides nga sessioni
        /// </summary>
        public void mbushGrideNgaSessionZeraProkurorimesh(int idNdermarrje)
        {
            colRealizimProkurimeshKonfig tmp = (colRealizimProkurimeshKonfig)mySessionObjects.merrObjectNgaSesioni(Session, "zeraProkurorimeshKonfig");
            gvKonfiguroZeraProkurimesh.DataSource = (tmp as colRealizimProkurimeshKonfig) ?? new colRealizimProkurimeshKonfig(idNdermarrje);
            gvKonfiguroZeraProkurimesh.DataBind();
            gvKonfiguroZeraProkurimesh.JSProperties["cpprokurimesh"] = Newtonsoft.Json.JsonConvert.SerializeObject(tmp.Select(x => x.Kodi));

        }

        private void PercaktoTemplateComboPrindiZeraProkurorimesh(GridViewDataComboBoxColumn cmb)
        {
            // object tmp = mySessionObjects.merrObjectNgaSesioni(Session, "shpenzimeKonfig");

            colRealizimProkurimeshKonfig col = new colRealizimProkurimeshKonfig(idNdermarrje);

            col.Add(new clsRealizimProkurimesh());
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
            cmb.PropertiesComboBox.ValueField = "RpkId";
            cmb.PropertiesComboBox.TextField = "Pershkrimi";
            cmb.PropertiesComboBox.TextFormatString = "{0}";
            // combo.TextField = "KodKonfigAmbjente";
            cmb.PropertiesComboBox.IncrementalFilteringMode = DevExpress.Web.IncrementalFilteringMode.Contains;
        }




        protected void gvKonfiguroZeraProkurimesh_DataBound(object sender, EventArgs e)
        {
            GridUtil.ShtoCommandColumnNeDatabound((ASPxGridView)sender, "#", "RpkId");


        }


        protected void gvKonfiguroZeraProkurimesh_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters.Length > 0)
            {
                if (e.Parameters.Contains("Riruaj"))
                {
                    clsMesazh mesazhi = AnalizeBuxheti.RuajZeraProkurimesh(mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    if (mesazhi.Status)
                        DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ruajtja u krye me sukses!:Green");
                    else
                        DbCore.mySessionObjects.ruajMesazhNeSesion(Session, mesazhi.PershkrimMesazhi + ":Red");
                }
                // mbushGridenNgaDB(mySessionObjects.merrIdNdermarrjeSesioni(Session), Convert.ToInt32(cmbAmbjenti.Value));
            }
        }

        protected void gvKonfiguroZeraProkurimesh_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {

            List<string> kodet = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(Convert.ToString(gvKonfiguroZeraProkurimesh.JSProperties["cpprokurimesh"]).ToLower());

            colRealizimProkurimeshKonfig col = (mySessionObjects.merrObjectNgaSesioni(Session, "zeraProkurorimeshKonfig") as colRealizimProkurimeshKonfig) ?? new colRealizimProkurimeshKonfig(idNdermarrje);

            string newKodi = e.NewValues["Kodi"].ToString().ToLower();
            string oldKodi = Convert.ToString(e.OldValues["Kodi"]).ToLower();

            if (!e.IsNewRow && newKodi == oldKodi)
                return;


            if (kodet.Contains(newKodi))
            {
                e.Errors[gvKonfiguroZeraProkurimesh.Columns["Kodi"]] = "Ky Ze nuk eshte ruajtur pasi kodi i tij eshte perdorur ne gride";

            }
            else
            {
                kodet.Add(newKodi);
                if (!e.IsNewRow)
                    kodet.Remove(oldKodi);
                gvKonfiguroZeraProkurimesh.JSProperties["cpprokurimesh"] = Newtonsoft.Json.JsonConvert.SerializeObject(kodet);
            }
        }

        protected void gvKonfiguroZeraProkurimesh_BatchUpdate(object sender, DevExpress.Web.Data.ASPxDataBatchUpdateEventArgs e)
        {
            try
            {

                //int idAmbjenti = Convert.ToInt32(cmbAmbjenti.Value);
                int idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
                int idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);
                object tmp = mySessionObjects.merrObjectNgaSesioni(Session, "zeraProkurorimeshKonfig");
                clsMesazh mesazhi = new clsMesazh(true);

                clsTeDrejtaRoli teDrejta = new clsTeDrejtaRoli();
                teDrejta.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, komponente);
                colRealizimProkurimeshKonfig col = (tmp as colRealizimProkurimeshKonfig) ?? new colRealizimProkurimeshKonfig(idNdermarrje);
                colRealizimProkurimeshKonfig newCol = new colRealizimProkurimeshKonfig(idNdermarrje);


                if (!teDrejta.DMod)
                {
                    mesazhi = new clsMesazh(false, MessagesResource.Messages["msgNukKeniTeDrejta"]);
                }
                else
                {


                    for (int i = 0, count = e.UpdateValues.Count; i < count; i++)
                    {
                        // int key = Convert.ToInt32(e.UpdateValues[i].Keys[0]);
                        clsRealizimProkurimesh zeIVjeter = col.Where(x => x.RpkId == e.UpdateValues[i].MerrKeyValue<int>()).FirstOrDefault();

                        clsRealizimProkurimesh zeIRi = e.UpdateValues[i].MerrCustomUpdatedObject(zeIVjeter);
                        zeIRi.IdModifikuesi = idPerdoruesi;
                        //newShpenzime.Niveli = newShpenzime.NiveliPrindit;
                        mesazhi = zeIRi.Modifiko(newCol);


                    }

                    for (int i = 0; i < e.InsertValues.Count; i++)
                    {
                        clsRealizimProkurimesh newShpenzime = new clsRealizimProkurimesh
                        {
                            IdPerdoruesi = idPerdoruesi,
                            IdNdermarrje = idNdermarrje,
                            IdNdermVit = idNdermVit
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
                        if (clsRealizimProkurimesh.KaRegjistrimeMeKeteRealizimProkurimesh(key))
                        {
                            throw new Exception("Ka veprime me kete ze!");
                        }
                        mesazhi = clsRealizimProkurimesh.Fshi(key, idPerdoruesi);
                        if(mesazhi.Status)
                            mesazhi = clsTrupiRealizimProkurimesh.FshiZerat(key);
                        if (mesazhi.Status) col.RemoveAll(x => x.RpkId == key);
                    }
                    if (mesazhi.Status)
                    {
                        mesazhi = col.PerditesoNivelet(new clsDatabaseAnalizeBuxheti());
                        if (mesazhi.Status)
                        {
                            mesazhi = AnalizeBuxheti.RuajZeraProkurimesh(idNdermarrje);
                            if (mesazhi.Status)
                            {
                                mySessionObjects.ruajObjectNeSesion(Session, col, "zeraProkurorimeshKonfig");
                                GridUtil.KtheKolonenNeComboNeGride(gvKonfiguroZeraProkurimesh, "IdPrindi", "Kodi i prindit", PercaktoTemplateComboPrindiZeraProkurorimesh);
                            }
                        }
                    }
                }
                if (mesazhi.Status)
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ruajtja u krye me sukses!:Green");
                else
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, mesazhi.PershkrimMesazhi + ":Red");

                gvKonfiguroZeraProkurimesh.DataSource = col;
                gvKonfiguroZeraProkurimesh.DataBind();




            }
            catch (Exception err)
            {
                ImbLogger.Error(err.Message);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, err.Message + ":Red");
            }
            e.Handled = true;
        }
        #endregion konfiguro zerat e prokurimeve publike
    }
}