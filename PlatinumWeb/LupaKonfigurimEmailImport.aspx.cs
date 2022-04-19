using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using DbCore.DbRegjistrim;
using System.Data;
using DbCore.DbAdmin;
using System.Web.Script.Serialization;
using PlatinumWeb.Templates;
using DbCore;
using DevExpress.Web;
using DbCore.IMBUtils.Messages;
using PlatinumWeb.ApplicationUtils.Pages;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;

namespace PlatinumWeb
{
    public partial class LupaKonfigurimEmailImport : MyPageBase
    {
        private int idKonfigImporti, idlicenca, idNdermarje;

    protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                Response.Redirect("login.aspx");
            }
            int idPerd = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerd);
            }
            idNdermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            idlicenca = DbCore.DbAdmin.clsLicenca.merrIdLicencePerdoruesi(idPerd);

            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarje);

            if (Request.QueryString["idKonfigImporti"] != null && Request.QueryString["idKonfigImporti"] != "")
                idKonfigImporti = Convert.ToInt32(Request.QueryString["idKonfigImporti"]);
            else
                idKonfigImporti = 0;

            if (!IsPostBack)
            {
                mbushGride();
                konfiguroPopupGride();
                if (idKonfigImporti != 0)
                    mbushPopUpListeNgaDB(idKonfigImporti);
            }
            //else
            //    mbushPopUpListeNgaSession();
            percaktoTemplateGride();

        }

        private void mbushGride()
        {
            DbCore.DbAdmin.colKonfigurimEmailKI col = new DbCore.DbAdmin.colKonfigurimEmailKI();
            DbCore.DbAdmin.clsKonfigurimEmailKI o = new DbCore.DbAdmin.clsKonfigurimEmailKI();
            col.Add(o);
            gvLupaKonfigurimEmailImport.DataSource = col;
            gvLupaKonfigurimEmailImport.DataBind();
        }

        private void mbushPopUpListeNgaDB(int idKonfigImporti)
        {
            colKonfigurimEmailKI col = new colKonfigurimEmailKI();
            col.merrKonfigurimeEmail(idKonfigImporti);
            if (!col.Any())
            {
                mbushGride();
                return;
            }
            clsKonfigurimEmailKI o = new clsKonfigurimEmailKI();
            col.Add(o);
            gvLupaKonfigurimEmailImport.DataSource = col;
            DbCore.mySessionObjects.ruajGrideNeSessionLupa(Session, gvLupaKonfigurimEmailImport.DataSource);
            gvLupaKonfigurimEmailImport.DataBind();
        }

        private void mbushPopUpListeNgaSession()
        {
            Object tmpObject;
            DbCore.mySessionObjects.merrGrideNgaSessioniLupa(Session, out tmpObject);
            if (tmpObject == null)
            {
                mbushPopUpListeNgaDB(idKonfigImporti);
                return;
            }
            gvLupaKonfigurimEmailImport.DataSource = tmpObject;
            gvLupaKonfigurimEmailImport.DataBind();
        }

        private void konfiguroPopupGride()
        {
            GridUtil.percaktoVisibleColumnsMeWidth(MessagesResource.Messages.IdGjuha, idNdermarje, gvLupaKonfigurimEmailImport, "gvLupaKonfigurimEmailImport", "LupaKonfigurimEmailImport.aspx");
            GridUtil.konfiguroGrideRegjistrimEvogelPaTheme(gvLupaKonfigurimEmailImport, "Id", true);
            gvLupaKonfigurimEmailImport.Settings.UseFixedTableLayout = false;
        }
        
        protected void gvLupaKonfigurimEmailImport_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpNoRows"] = gvLupaKonfigurimEmailImport.VisibleRowCount;
        }

        protected void gvLupaKonfigurimEmailImport_DataBound(object sender, EventArgs e)
        {
            gvLupaKonfigurimEmailImport.KeyFieldName = "Id";
            if (this.gvLupaKonfigurimEmailImport.Columns["Fshi"] == null)
            {
                GridViewDataTextColumn fshi = new GridViewDataTextColumn();
                fshi.Caption = "Fshi";
                fshi.Width = 30;
                gvLupaKonfigurimEmailImport.Columns.Add(fshi);
                gvLupaKonfigurimEmailImport.SettingsBehavior.AllowSelectByRowClick = false;
                gvLupaKonfigurimEmailImport.SettingsBehavior.AllowFocusedRow = true;
            }
        }

        protected void gvLupaKonfigurimEmailImport_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (!IsCallback)
                return;

            if (!Request.Params["__CALLBACKID"].ToString().Contains("gvLupaKonfigurimEmailImport"))
                return;

            if (e.Parameters.Split(';').Length == 2)
            {
                if (e.Parameters.Split(';')[1] == "modifiko")
                    mbushPopUpListeNgaDB(int.Parse(e.Parameters.Split(';')[0]));
                else
                    mbushGride();

                return;
            }

            int key = -1;
            if (e.Parameters.ToString() != "")
            {
                key = int.Parse(e.Parameters.ToString());
            }
            DbCore.DbAdmin.colKonfigurimEmailKI trupat = new DbCore.DbAdmin.colKonfigurimEmailKI();
            DbCore.DbAdmin.clsKonfigurimEmailKI tr = new DbCore.DbAdmin.clsKonfigurimEmailKI();
            JavaScriptSerializer serializusi = new JavaScriptSerializer();
            int rreshta = gvLupaKonfigurimEmailImport.VisibleRowCount + 1;
            object[] statusi = (object[])serializusi.DeserializeObject(hfStatusi.Value);
            object[] lloji = (object[])serializusi.DeserializeObject(hfLloji.Value);
            object[] destinacion = (object[])serializusi.DeserializeObject(hfDestinacion.Value);
            object[] email = (object[])serializusi.DeserializeObject(hfEmail.Value);
            for (int i = 0; i < rreshta -1; i++)
            {
                tr = new DbCore.DbAdmin.clsKonfigurimEmailKI();

                if (statusi.Length >= i && statusi[i] != null && statusi[i] != "null" && statusi[i] != "")
                    tr.Statusi = int.Parse(statusi[i].ToString());

                if (lloji.Length >= i &&  lloji[i] != null && lloji[i] != "null" && lloji[i] != "")
                    tr.Lloji = int.Parse(lloji[i].ToString());


                if (destinacion.Length >= i && destinacion[i] != null && destinacion[i] != "null" && destinacion[i] != "")
                {
                    tr.Destinacion = destinacion[i].ToString();
                    if (lloji[i].ToString() == "1")
                    {//TO DO : perdor metoden qe kthen id e perdoruesit nga username
                        clsPerdorues per = new clsPerdorues(tr.Destinacion);
                        tr.IdDestinacion = per.IdPerdorues;
                    }
                    else if (lloji[i].ToString() == "2")
                        tr.IdDestinacion = clsRoli.ktheIdRoli(tr.Destinacion, idlicenca);
                }

                if (email.Length >= i && email[i] != null && email[i] != "null" && email[i] != "")
                    tr.Email = email[i].ToString();

                trupat.Add(tr);
            }
            if (key != -1)
                trupat.RemoveAt(key);
            else
                trupat.Add(new DbCore.DbAdmin.clsKonfigurimEmailKI());
            if (trupat.Count == 0)
                trupat.Add(new DbCore.DbAdmin.clsKonfigurimEmailKI());

            this.gvLupaKonfigurimEmailImport.DataSource = trupat;
            DbCore.mySessionObjects.ruajGrideNeSessionLupa(Session, trupat);
            this.gvLupaKonfigurimEmailImport.DataBind();
            percaktoTemplateGride();
        }

        private void percaktoTemplateGride()
        {
            GridViewDataTextColumn col0 = gvLupaKonfigurimEmailImport.Columns["Statusi"] as GridViewDataTextColumn;
            col0.DataItemTemplate = new MyComboTemplate();
            GridViewDataTextColumn col1 = gvLupaKonfigurimEmailImport.Columns["Lloji"] as GridViewDataTextColumn;
            col1.DataItemTemplate = new MyComboTemplate();
            GridViewDataTextColumn col2 = gvLupaKonfigurimEmailImport.Columns["Destinacion"] as GridViewDataTextColumn;
            col2.DataItemTemplate = new MyComboTemplate();
            GridViewDataTextColumn col3 = gvLupaKonfigurimEmailImport.Columns["Email"] as GridViewDataTextColumn;
            col3.DataItemTemplate = new MyTextTemplate();
            GridViewDataTextColumn col4 = gvLupaKonfigurimEmailImport.Columns["Fshi"] as GridViewDataTextColumn;
            col4.DataItemTemplate = new MyButtonTemplate("");
        }

        protected void gvLupaKonfigurimEmailImport_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            gvLupaKonfigurimEmailImport.DataBind();
        }

        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        }
        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        /// <param name="idViti"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNermarrje"></param>
        private void percaktoTemplateMenu(ASPxMenu aSPxMenu1, int idViti, int idPerdorues, int idNdermarrje)
        {
            clsToolbarConfig.percaktoTemplateMenu(MessagesResource.Messages.IdGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, "LupaKonfigurimEmailImport.aspx", this, MenuInfo, false, true, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
            aSPxMenu1.Items.FindByName("Anullo").Text = "Mbyll";
        }
        
        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {
            if (e.Item.Name == "Ruaj")
            {
                //Page.Validate("entries");
                ruajKonfigurimeEmail();
            }
        }

        private void ruajKonfigurimeEmail()
        {
            DbCore.DbAdmin.colKonfigurimEmailKI trupi = new DbCore.DbAdmin.colKonfigurimEmailKI();
            DbCore.DbAdmin.clsKonfigurimEmailKI tr = new DbCore.DbAdmin.clsKonfigurimEmailKI();
            JavaScriptSerializer serializusi = new JavaScriptSerializer();
            int rreshta = gvLupaKonfigurimEmailImport.VisibleRowCount + 1;
            object[] lloji = (object[])serializusi.DeserializeObject(hfLloji.Value);
            object[] destinacion = (object[])serializusi.DeserializeObject(hfDestinacion.Value);
            object[] statusi = (object[])serializusi.DeserializeObject(hfStatusi.Value);
            object[] email = (object[])serializusi.DeserializeObject(hfEmail.Value);
            for (int i = 0; i < rreshta - 1; i++)
            {
                if ((lloji[i] != null && lloji[i].ToString() == "3" && (email[i] == null || email[i].ToString() == "null" || email[i].ToString() == "")) || 
                    (lloji[i] != null && lloji[i].ToString() != "3" && (destinacion[i] == null || destinacion[i].ToString() == "null" || destinacion[i].ToString() == "")))
                    continue;

                tr = new DbCore.DbAdmin.clsKonfigurimEmailKI();

                if (statusi[i] != null && statusi[i] != "null" && statusi[i] != "")
                    tr.Statusi = int.Parse(statusi[i].ToString());

                if (lloji[i] != null && lloji[i] != "null" && lloji[i] != "")
                    tr.Lloji = int.Parse(lloji[i].ToString());

                if (destinacion[i] != null && destinacion[i] != "null" && destinacion[i] != "")
                {
                    tr.Destinacion = destinacion[i].ToString();
                    if (lloji[i].ToString() == "1") {//TO DO ktheIdPerdoruesSipasUsername
                        clsPerdorues per = new clsPerdorues(tr.Destinacion);
                        tr.IdDestinacion = per.IdPerdorues;
                    }
                    else if (lloji[i].ToString() == "2")
                        tr.IdDestinacion = clsRoli.ktheIdRoli(tr.Destinacion, idlicenca);
                }

                if (email[i] != null && email[i] != "null" && email[i] != "")
                    tr.Email = email[i].ToString();

                tr.IdTemplateImporti = idKonfigImporti;
                tr.IdKrijuesi = mySessionObjects.ktheIdPerdoruesi(Session);

                trupi.Add(tr);
            }
            clsMesazh mesazh = trupi.Ruaj();
            if (mesazh)
            {
                gvLupaKonfigurimEmailImport.DataSource = trupi;
                gvLupaKonfigurimEmailImport.ShtoMesazhNeGride(mesazh);
                DbCore.mySessionObjects.ruajGrideNeSessionLupa(Session, gvLupaKonfigurimEmailImport.DataSource);
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            }
            else
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
        }

        protected void gvLupaKonfigurimEmailImport_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            //bool ugjet;
            if (e.RowType == GridViewRowType.Data)
            {
                GridViewDataTextColumn col0 = ((ASPxGridView)sender).Columns["Statusi"] as GridViewDataTextColumn;
                GridViewDataTextColumn col1 = ((ASPxGridView)sender).Columns["Lloji"] as GridViewDataTextColumn;
                GridViewDataTextColumn col2 = ((ASPxGridView)sender).Columns["Destinacion"] as GridViewDataTextColumn;
                GridViewDataTextColumn col3 = ((ASPxGridView)sender).Columns["Email"] as GridViewDataTextColumn;
                GridViewDataTextColumn col4 = ((ASPxGridView)sender).Columns["Fshi"] as GridViewDataTextColumn;
                ASPxComboBox cmb0 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col0, "cmbBox") as ASPxComboBox;
                ASPxComboBox cmb1 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col1, "cmbBox") as ASPxComboBox;
                ASPxComboBox cmb2 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col2, "cmbBox") as ASPxComboBox;
                ASPxTextBox txt0 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col3, "txtBox") as ASPxTextBox;
                ASPxButton btn0 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col4, "btn") as ASPxButton;

                #region vendosen client side eventet e kolonave
                if (btn0 != null)
                {
                    btn0.ClientInstanceName = String.Format("btnFshi{0}", e.VisibleIndex);
                    btn0.ClientSideEvents.Click = String.Format("function(s,e){{FshiClicked({0});}}", e.VisibleIndex);
                }
                if (cmb0 != null)
                {
                    cmb0.Items.Add("", 0);
                    cmb0.Items.Add("Importuar", 1);
                    cmb0.Items.Add("Deshtuar", 2);
                    if (cmb0.Text == "")
                        cmb0.SelectedIndex = 0;
                    cmb0.DataBind();
                    cmb0.ClientInstanceName = String.Format("Statusi{0}", e.VisibleIndex);
                    cmb0.ClientSideEvents.TextChanged = String.Format("function(s,e){{TextChangedStatusi(Statusi{0}, {0});}}", e.VisibleIndex);
                }
                if (cmb1 != null)
                {
                    cmb1.Items.Add("Perdorues", 1);
                    cmb1.Items.Add("Rol", 2);
                    cmb1.Items.Add("Tjeter", 3);
                    if (cmb1.Text == "")
                        cmb1.SelectedIndex = 0;
                    cmb1.DataBind();
                    cmb1.ClientInstanceName = String.Format("Lloji{0}", e.VisibleIndex);
                    cmb1.ClientSideEvents.TextChanged = String.Format("function(s,e){{TextChangedLloji(Lloji{0}, Destinacion{0}, {0});}}", e.VisibleIndex);
                }
                if (cmb2 != null)
                {
                    cmb2.ClientEnabled = (cmb1.Value.ToString() != "3");
                    cmb2.DropDownButton.Visible = false;
                    cmb2.Buttons.Add();
                    cmb2.AutoPostBack = false;
                    cmb2.EnableCallbackMode = false;
                    cmb2.TextFormatString = "{0}";
                    cmb2.Columns.Add(new ListBoxColumn("Kodi"));
                    cmb2.Columns.Add(new ListBoxColumn("Emertimi"));
                    cmb2.ClientInstanceName = String.Format("Destinacion{0}", e.VisibleIndex);
                    cmb2.DropDownStyle = DropDownStyle.DropDown;
                    cmb2.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
                    cmb2.ClientSideEvents.LostFocus = String.Format("function(s,e){{TextChangedDestinacion(Destinacion{0},{0});}}", e.VisibleIndex);
                    cmb2.ClientSideEvents.KeyUp = String.Format("function(s,e){{var code = _getKeyCode(e.htmlEvent); KeyPressDestinacion(code, Destinacion{0}, {0}); }}", e.VisibleIndex);
                    cmb2.ClientSideEvents.ButtonClick = String.Format("function(s,e){{ButtonClickedDestinacion(Destinacion{0},{0}); }}", e.VisibleIndex);
                }
                if (txt0 != null)
                {
                    txt0.ClientEnabled = (cmb1.Value.ToString() == "3");
                    txt0.ClientInstanceName = String.Format("Email{0}", e.VisibleIndex);
                    txt0.ClientSideEvents.TextChanged = "function (s,e){TextChangedEmail(Email" + e.VisibleIndex + "," + e.VisibleIndex + ")}";
                }
                #endregion
            }
        }
    }
}