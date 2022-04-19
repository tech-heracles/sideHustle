using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Collections;
using System.Drawing;
using PlatinumWeb.Templates;
using System.Globalization;
using System.Resources;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class LupaLloji : MyPageBase
    {
        //private string koloneFocus;
        ASPxTextBox temptxt = null;
        //ASPxComboBox tempcombo = null;
        //ASPxCheckBox tempcb = null;
        public static int idNdermVit = -1;
        //DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();

        protected void Page_Load(object sender, EventArgs e)
        {
            String array = Request.QueryString["value"];
            //if (!Page.IsCallback)
            //{
                               
            //}
            mbushPopUpListeLlojesh(array);
            konfiguroPopupGride();
        }

        private static bool exist(int id, String[] ids)
        {
            for (int i = 0; i < ids.Length; i++)
            {
                if (id == Convert.ToInt32(ids[i].ToString()))
                    return true;
            }
            return false;
        }

        private void mbushPopUpListeLlojesh(String ids)
        {//mbush griden e popupit me te dhena
            DbCore.DbAdmin.colLlojeVeprimesh colLloje = new DbCore.DbAdmin.colLlojeVeprimesh();
            string[] idTe = ids.Split(',');
            bool zgjedhur = true;
            for (int i = 0; i < idTe.Length; i++)
            {
                int id = Convert.ToInt32(idTe[i].ToString());
                DbCore.DbShare.clsKonfLlojRreshtiVlere llojRreshtiVlere = new DbCore.DbShare.clsKonfLlojRreshtiVlere(id);
                DbCore.DbAdmin.clsLlojVeprimi lloj = new DbCore.DbAdmin.clsLlojVeprimi(llojRreshtiVlere.IdLlojRreshti,
                    llojRreshtiVlere.KodLlojRreshti, i + 1,
                    zgjedhur, 1, "");
                colLloje.Add(lloj);
            }
            int renditja = idTe.Length; DbCore.DbShare.clsKonfLlojRreshti konfLlojRreshti;
            if (Request.QueryString["kat"] == "Shitje" || Request.QueryString["kat"] == "Blerje")
                konfLlojRreshti = new DbCore.DbShare.clsKonfLlojRreshti(-1, "Shitje");
            else konfLlojRreshti = new DbCore.DbShare.clsKonfLlojRreshti(-1, "ArkaBanka");
            for (int i = 0; i < konfLlojRreshti.ColKonfLlojRreshtiVlere.Count; i++)
            {
                if (!LupaLloji.exist(konfLlojRreshti.ColKonfLlojRreshtiVlere[i].IdLlojRreshti, idTe))
                {
                    DbCore.DbAdmin.clsLlojVeprimi lloj = new DbCore.DbAdmin.clsLlojVeprimi(konfLlojRreshti.ColKonfLlojRreshtiVlere[i].IdLlojRreshti,
                    konfLlojRreshti.ColKonfLlojRreshtiVlere[i].KodLlojRreshti, ++renditja, !zgjedhur, 1, "");
                    colLloje.Add(lloj);
                }
            }
            gvLupaLloji.DataSource = colLloje;
            gvLupaLloji.DataBind();
        }

        private void konfiguroPopupGride()
        {//konfiguron popupgriden
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            GridUtil.percaktoVisibleColumnsMeWidth(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), gvLupaLloji, "gvLupaLloji", "LupaLloji.aspx");
            GridUtil.konfiguroGrideRegjistrimEvogelPaTheme(gvLupaLloji, "IdLloji");
            gvLupaLloji.Settings.UseFixedTableLayout = false;
            percaktoTemplateLlojesh();
        }

        private void percaktoTemplateLlojesh()
        {//percaktohen templatet per fushat e grides
            GridViewDataCheckColumn col0 = gvLupaLloji.Columns["Check"] as GridViewDataCheckColumn;
            col0.DataItemTemplate = new MyCheckTemplate(false, false);
            col0.VisibleIndex = 0;
            GridViewDataTextColumn col1 = gvLupaLloji.Columns["PershkrimLloji"] as GridViewDataTextColumn;
            col1.DataItemTemplate = new MyReadOnlyTextTemplate();
            GridViewDataTextColumn col3 = gvLupaLloji.Columns["Prioriteti"] as GridViewDataTextColumn;
            col3.DataItemTemplate = new MyReadOnlyTextTemplate();
        }

        protected void gvLupaLloji_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            gvLupaLloji.DataBind();
        }

        protected void gvLupaLloji_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {//kur grida ben callback te ruajme te dhenat
            //int key = -1;
            //DbCore.DbInventari.clsDatabaseInventari dbInventari = new DbCore.DbInventari.clsDatabaseInventari();

            //dbKontabiliteti = new DbCore.DbKontabiliteti.clsDatabaseKontabilitet();
            //if (e.Parameters.ToString() != "")
            //{
            //    key = int.Parse(e.Parameters.ToString());
            //}
            //DbCore.DbInventari.colArtikujtZevendesues artikujt = new DbCore.DbInventari.colArtikujtZevendesues();
            //DbCore.DbInventari.clsArtikullZevendesues artikulli;
            //int rreshta = gvLupaLloji.VisibleRowCount + 1;
            //string initVal = this.hfArtikulli.Text;
            //string[] pars1 = initVal.Split(',');
            //string initVal2 = this.hfEmertimiA.Text;
            //string[] pars3 = initVal2.Split(',');
            //string initVal3 = this.hfPrioritetiA.Text;
            //string[] pars5 = initVal3.Split(',');

            //string[] kodi = new string[pars1.Length];
            //string[] emri = new string[pars1.Length];
            //string[] prioriteti = new string[pars1.Length];
            //if (initVal != "")//merren te dhenat e hiden fieldeve te trupave te fleteve kontabel nga javascipti
            //{
            //    for (int i = 0; i < pars1.Length; i++)
            //    {
            //        string[] pars2 = pars1[i].Split(':');
            //        kodi[Convert.ToInt32(pars2[0])] = pars2[1];
            //    }
            //}
            //if (initVal2 != "")
            //{
            //    for (int i = 0; i < pars3.Length; i++)
            //    {
            //        string[] pars4 = pars3[i].Split(':');
            //        emri[Convert.ToInt32(pars4[0])] = pars4[1];
            //    }
            //}
            //if (initVal3 != "")
            //{
            //    for (int i = 0; i < pars5.Length; i++)
            //    {
            //        string[] pars6 = pars5[i].Split(':');
            //        prioriteti[Convert.ToInt32(pars6[0])] = pars6[1];
            //    }
            //}

            //for (int i = 0; i < rreshta - 1; i++)//krijohet kolectioni me trupat e fleteve kontabel e futura nga perdoruesi
            //{
            //    artikulli = new DbCore.DbInventari.clsArtikullZevendesues();
            //    if (kodi[i] != null && kodi[i] != "null" && kodi[i] != "")
            //    {
            //        artikulli.KodArtikulli = kodi[i];
            //        artikulli.IdArtikulliZevend = dbInventari.ktheArtikull(kodi[i], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session))[0].IdArtikulli;
            //    } if (emri[i] != null && emri[i] != "null" && emri[i] != "")
            //        artikulli.PershkrimArtikulli = emri[i];
            //    if (prioriteti[i] != null && prioriteti[i] != "null" && prioriteti[i] != "")
            //        artikulli.Prioriteti = prioriteti[i];

            //    artikujt.Add(artikulli);
            //}

            //if (key != -1)
            //    artikujt.RemoveAt(key);
            //else
            //{
            //    DbCore.DbInventari.clsArtikullZevendesues art = new DbCore.DbInventari.clsArtikullZevendesues();

            //    artikujt.Add(art);
            //}
            //if (artikujt.Count == 0)
            //{
            //    DbCore.DbInventari.clsArtikullZevendesues art = new DbCore.DbInventari.clsArtikullZevendesues();
            //    artikujt.Add(art);
            //}
            //this.gvLupaLloji.DataSource = artikujt;
            //this.gvLupaLloji.DataBind();
            //percaktoTemplateArtikujsh();
        }

        protected void gvLupaLloji_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpNoRows"] = gvLupaLloji.VisibleRowCount;
        }

        protected void gvLupaLloji_DataBound(object sender, EventArgs e)
        {//shton butonin fshi
            //if (this.gvLupaLloji.Columns["Fshi"] == null)
            //{
            //    GridViewDataTextColumn fshi = new GridViewDataTextColumn();
            //    fshi.Caption = "Fshi";
            //    fshi.Width = 50;
            //    gvLupaLloji.Columns.Add(fshi);

            gvLupaLloji.KeyFieldName = "IdLloji";
            gvLupaLloji.SettingsBehavior.AllowSelectByRowClick = false;
            gvLupaLloji.SettingsBehavior.AllowFocusedRow = true;
            // }
        }

        protected void gvLupaLloji_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {//krijon rreshat sipas modelit
            if (e.RowType == DevExpress.Web.GridViewRowType.Data)
            {
                GridViewDataTextColumn col0 = ((ASPxGridView)sender).Columns["Check"] as GridViewDataTextColumn;
                GridViewDataTextColumn col2 = ((ASPxGridView)sender).Columns["PershkrimLloji"] as GridViewDataTextColumn;
                GridViewDataTextColumn col3 = ((ASPxGridView)sender).Columns["Prioriteti"] as GridViewDataTextColumn;
                ASPxCheckBox btn0 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col0, "cb") as ASPxCheckBox;
                ASPxTextBox txt2 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col2, "txtBox") as ASPxTextBox;
                ASPxTextBox cmb4 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col3, "txtBox") as ASPxTextBox;
                //vendosen client side eventet e kolonave

                if (btn0 != null)
                {
                    btn0.ClientInstanceName = "cbCheck" + e.VisibleIndex.ToString();
                    btn0.ClientSideEvents.CheckedChanged = "function(s,e){CheckedChenged(" + e.VisibleIndex.ToString() + ");}";
                }

                if (txt2 != null)
                {
                    txt2.ClientInstanceName = "txtEmertimi" + e.VisibleIndex.ToString();
                }

                if (cmb4 != null)
                {
                    cmb4.ClientInstanceName = "txtPrioriteti" + e.VisibleIndex.ToString();
                }
            }

            if (temptxt != null)
            {
                temptxt.Focus();
            }
        }
    }
}