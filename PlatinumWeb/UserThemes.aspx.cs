using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;
using DevExpress.Web;
using System.Data;
using System.Collections;
using DevExpress.Web.Data;
using PlatinumWeb.Templates;
using System.Globalization;
using System.Resources;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    public partial class UserThemes : MyPageBase
    {
        DbCore.DbAdmin.clsThemesAmbjente themeAmbjente;

        private const string prefixMesazhNjejes = "Motivi me Kod: ";
        private const string prefixMesazhShumes = "Motivet me Kod: ";
        private const string suffixMesazhNjejesGabimi = " nuk mund te fshihet";
        private const string suffixMesazhShumesGabimi = " nuk mund te fshihen";
        private const string suffixMesazhNjejesSuksesi = " u fshi me sukses!";
        private const string suffixMesazhShumesSuksesi = " u fshine me sukses!";
        private const string lidhesMesazhi = ". Kurse ";
        private const string mesazhZgjidhniNje = "Ju lutem zgjidhni te pakten nje motiv!";

        protected void Page_Init(object sender, EventArgs e)
        {
            //if (!IsPostBack) mbushTemplate();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (CacheLayer.GlobalCacheManager.MySessionCache["LoggedIn"].Equals("No"))
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                DbCore.clsFunksione.logout(Session,true,"FaqePaautorizuar");
            }
            int idPerd = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerd);
            }
            int idviti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);

            percaktoTemplateMenu(DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), ASPxMenu1);


            if (!IsPostBack)
            {
                EmrateTabeve();
                mbushListeThemes();
                konfiguroVleraFillestare();
                konfigurogride();
                GridUtil.percaktoVisibleColumnsMeWidth(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), gvThemesAmbjente, "gvThemesAmbjente", "UserThemes.aspx");

                ASPxPageControl1.ActiveTabIndex = 0;
                selektoThemeZgjedhur();
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerd, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "UserThemes.aspx");
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
            }
            else
            {
                mbushGrideThemesNgaSession();
                konfigurogride();
            }
            //mbushTemplate();
            //mbushComboBoxFiltra();
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), "gvThemesAmbjente", 1, "UserThemes.aspx");
            //clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "", pnlMesazhi);
            GridUtil.konfigGrideListeEMadhePaTheme(gvThemesAmbjente, "IdThemeAmbjente");
        }

        /// <summary>
        /// Vendos emrat e tabeve ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateTabeve()
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            ASPxPageControl1.TabPages[0].Text = rm.GetString("MenuItemLista", cultinf);
            ASPxPageControl1.TabPages[1].Text = rm.GetString("TePergjithshmeTab", cultinf);
            ASPxPageControl1.TabPages[2].Text = rm.GetString("menuteTab", cultinf);
            ASPxPageControl1.TabPages[3].Text = rm.GetString("ambjentiKryesorTab", cultinf);
            ASPxPageControl1.TabPages[4].Text = rm.GetString("gridatRegjistrimeTab", cultinf);
            ASPxPageControl1.TabPages[5].Text = rm.GetString("sfondiTab", cultinf);
        }

        private void selektoThemeZgjedhur()
        {
            DbCore.DbAdmin.clsThemesAmbjente themeZgjedhur = new DbCore.DbAdmin.clsThemesAmbjente();
            themeZgjedhur.ktheThemeZgjedhurPerdorues(DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            int rowIndex = gvThemesAmbjente.FindVisibleIndexByKeyValue(themeZgjedhur.IdThemeAmbjente);
            if (!IsRowVisibleOnScreen(rowIndex))
            {
                // Switch to the page which contains the required row.
                GoToPage(rowIndex);
            }
            //gvThemesAmbjente.Selection.SelectRowByKey(themeZgjedhur.IdThemeAmbjente);
            gvThemesAmbjente.FocusedRowIndex = rowIndex;
        }

        private bool IsRowVisibleOnScreen(int rowIndex)
        {
            int startIndex = gvThemesAmbjente.PageIndex * gvThemesAmbjente.SettingsPager.PageSize;
            int endIndex = startIndex + gvThemesAmbjente.SettingsPager.PageSize;
            return rowIndex >= startIndex && rowIndex < endIndex;
        }

        private void GoToPage(int rowIndex)
        {
            gvThemesAmbjente.PageIndex = rowIndex / gvThemesAmbjente.SettingsPager.PageSize;
        }

        private void konfiguroVleraFillestare()
        {
            mbushThemesDevExpressJQuery(dataViewFrames, true);
            mbushThemesDevExpressJQuery(dataViewAmbjKryesor, true);
            mbushThemesDevExpressJQuery(dataViewJQuery, false);
        }

        private void mbushThemesDevExpressJQuery(ASPxDataView dataView, bool lloji)
        {
            DbCore.DbAdmin.colThemesDevexpressJQuery col = new DbCore.DbAdmin.colThemesDevexpressJQuery();
            col.mbushThemesDevExpressJQuerysSipasLloji(lloji);
            dataView.DataSource = col;
            dataView.DataBind();
        }

        private void mbushGrideThemesNgaSession()
        {
            DataTable tmpObject;
            bool sukses = DbCore.mySessionObjects.merrGrideNgaSessioni(DbCore.clsFunksione.GetKomponente(Page.Request), Session, out tmpObject);
            if (!sukses)
                mbushListeThemes();
            else
            {
                gvThemesAmbjente.DataSource = tmpObject;
                gvThemesAmbjente.DataBind();
                tmpObject.Dispose();
            }

        }

        private void mbushListeThemes()
        {
            DataTable dt = DbCore.DbAdmin.colThemesAmbjente.merrgjitheThemesAmbjentePerPerd(DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            DbCore.mySessionObjects.ruajGrideNeSession(DbCore.clsFunksione.GetKomponente(Page.Request), Session, dt);
            gvThemesAmbjente.DataSource = dt;
            gvThemesAmbjente.DataBind();
            dt.Dispose();
        }

        private void konfigurogride()
        {
            shtoDefault();
            shtoZgjedhur();
            this.gvThemesAmbjente.Columns["#"].VisibleIndex = 0;
        }

        private void shtoZgjedhur()
        {
            GridViewDataColumn col = gvThemesAmbjente.Columns["Zgjedhur"] as GridViewDataColumn;
            col.DataItemTemplate = new MyCheckTemplate(true, false);
        }

        private void shtoDefault()
        {
            GridViewDataColumn col = gvThemesAmbjente.Columns["DefaultTheme"] as GridViewDataColumn;
            col.DataItemTemplate = new MyCheckTemplate(true, false);
        }
        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="idNderVit"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        private void percaktoTemplateMenu(int idViti, int idPerdorues, int idNdermarrje, ASPxMenu aSPxMenu1)
        {
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, "UserThemes.aspx", this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, hfShtimModifikim.Value == "modifikim" ? false : true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
        }


        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), ASPxMenu1);

        }

        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {
            if (e.Item.Name == "Ruaj")
            {
                Page.Validate("entries");
                if (Page.IsValid)
                    ruajThemeAmbjente();
                else return;
            }
            if (e.Item.Name == "ZgjidhTheme")
            {
                zgjidhThemeAmbjente();
            }
        }

        private void zgjidhThemeAmbjente()
        {
            themeAmbjente = new DbCore.DbAdmin.clsThemesAmbjente();
            int idja;
            if (gvThemesAmbjente.Selection.Count != 1)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Duhet te selektoni vetem nje motiv!", pnlMesazhi);
                return;
            }
            List<Object> obj = gvThemesAmbjente.GetSelectedFieldValues("IdThemeAmbjente");
            idja = int.Parse(obj[0].ToString());
            DbCore.clsMesazh mesazh = DbCore.DbAdmin.clsThemesAmbjente.zgjidhMotiv(idja, DbCore.mySessionObjects.ktheIdPerdoruesi(Session));

            if (mesazh.Status == true)
            {
                Response.Redirect("FaqeKryesore.aspx");
            }
            else
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ndodhi nje gabim. Ruajtja nuk u krye!", pnlMesazhi);
                hfStatusi.Value = "false";
            }
            ASPxPageControl1.ActiveTabIndex = 0;
            konfiguroVleraFillestare();
            mbushListeThemes();
        }

        private void ruajThemeAmbjente()
        {
            themeAmbjente = new DbCore.DbAdmin.clsThemesAmbjente();
            int idja;

            idja = int.Parse(hfId.Value.ToString());
            if (isValidTheme())
            {
                themeAmbjente = krijoThemeAmbjente();
                bool eshteShtim;
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                DbCore.DbAdmin.clsThemesAmbjente themeVjeter = new DbCore.DbAdmin.clsThemesAmbjente();
                themeVjeter.ktheThemeZgjedhurPerdorues(DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
                int idBgVjeter = themeVjeter.IdBgImage, idThemeFrameKryesorVjeter = themeVjeter.IdThemeFrameKryesor, idThemeFramesVjeter = themeVjeter.IdThemeFrames;
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "UserThemes.aspx");

                if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
                {
                    if (!tedrejtaInfo.DShtim)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                        hfStatusi.Value = "false";
                        return;
                    }
                    mesazh = themeAmbjente.Ruaj();
                    eshteShtim = true;
                }
                else
                {
                    if (!tedrejtaInfo.DMod)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                        hfStatusi.Value = "false";
                        return;
                    }
                    eshteShtim = false;
                    themeAmbjente.IdThemeAmbjente = idja;
                    mesazh = themeAmbjente.Modifiko();
                }

                if (mesazh.Status == true)
                {
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, MessagesResource.Messages["mesazhRuajtjeMeSukses"], pnlMesazhi);
                    if (eshteShtim)
                        shtoThemeAmbjNeGrid(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), themeAmbjente.IdThemeAmbjente);
                    else //modifikim
                        modifikoThemeAmbjNeGrid(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), themeAmbjente.IdThemeAmbjente);
                    hfStatusi.Value = "true";

                    if (themeAmbjente.Zgjedhur == true)
                    {
                        if (themeAmbjente.IdBgImage != 0)
                        {
                            //mySessionObjects.ruajBackgroundPathNeSesion(new DbCore.DbAdmin.clsTheme(themeAmbjente.IdBgImage).PathTheme, Session);
                            if (themeAmbjente.IdBgImage != idBgVjeter)
                            {
                                Response.Redirect("FaqeKryesore.aspx");
                            }
                        }
                        else
                        {
                            //mySessionObjects.ruajBackgroundPathNeSesion("images/backgrounds/imagesJ105.jpg", Session);
                        }

                        if (themeAmbjente.IdThemeFrames != idThemeFramesVjeter)
                        {
                            Response.Redirect("FaqeKryesore.aspx");
                        }
                        else
                        {
                            Response.Redirect("UserThemes.aspx");
                        }
                    }
                }
                else
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ndodhi nje gabim. Ruajtja nuk u krye!", pnlMesazhi);
                    hfStatusi.Value = "false";
                }
                ASPxPageControl1.ActiveTabIndex = 0;
                konfiguroVleraFillestare();
                mbushListeThemes();
            }
        }

        private void modifikoThemeAmbjNeGrid(int idnderm, int idtheme)
        {
            if (gvThemesAmbjente.DataSource != null)
            {
                DataTable dt = (DataTable)gvThemesAmbjente.DataSource;
                DataRow[] drs = dt.Select("IdThemeAmbjente = " + idtheme);
                if (drs.Length > 1)
                    throw new Exception("GABIM: Ndodhen 2 tema me te njejten id ne gride");
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                DataRow newArtDr = DbCore.DbAdmin.clsThemesAmbjente.merrThemeAmbjenteDR(idtheme);
                //  dt.Rows.Remove(dr);
                // dt.ImportRow(newArtDr);
                object[] arr = newArtDr.ItemArray;
                Array.Copy(arr, dr.ItemArray, dr.ItemArray.Length);
            }
            else mbushListeThemes();
        }

        private void shtoThemeAmbjNeGrid(int idnderm, int idtheme)
        {
            if (gvThemesAmbjente.DataSource != null)
            {
                DataTable dt = (DataTable)gvThemesAmbjente.DataSource;
                DataRow[] drs = dt.Select("IdThemeAmbjente = " + idtheme);
                if (drs.Length > 0)
                    throw new Exception("GABIM: Tema ekziston ne gride");
                DataRow newArtDr = DbCore.DbAdmin.clsThemesAmbjente.merrThemeAmbjenteDR(idtheme);
                dt.ImportRow(newArtDr);
            }
            else mbushListeThemes();
        }

        private DbCore.DbAdmin.clsThemesAmbjente krijoThemeAmbjente()
        {
            DbCore.DbAdmin.clsThemesAmbjente theme = new DbCore.DbAdmin.clsThemesAmbjente();
            theme.KodTheme = txtKodi.Text;
            theme.PershkrimTheme = txtPershkrimi.Text;
            theme.DefaultTheme = cbDefault.Checked;
            theme.Zgjedhur = cbZgjedhur.Checked;
            //if (zgjidh == true)
            //{
            //    cbZgjedhur.Checked = true;
            //    theme.Zgjedhur = true;
            //}
            if (hfShtimModifikim.Value == "klonim")
            {
                theme.DefaultTheme = false;
                cbDefault.Checked = false;
            }
            theme.IdThemeFrames = new DbCore.DbAdmin.clsThemesDevExpressJQuery(hfThemeFramet.Value.ToString()).IdTheme;
            theme.IdThemeFrameKryesor = new DbCore.DbAdmin.clsThemesDevExpressJQuery(hfThemeAmbjKr.Value.ToString()).IdTheme;
            theme.IdThemeJQuery = new DbCore.DbAdmin.clsThemesDevExpressJQuery(hfThemeJQuery.Value.ToString()).IdTheme;
            theme.IdBgImage = new DbCore.DbAdmin.clsTheme(hfBgImage.Value.ToString()).IdTheme;
            theme.IdPerdorues = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            theme.IdStatusDok = 1;

            return theme;
        }

        private bool isValidTheme()
        {
            bool isValid;
            isValid = true;
            if (themeAmbjente == null)
            {
                isValid = false;
            }
            else
            {
                if (hfThemeFramet.Value == null || hfThemeFramet.Value == "")
                {
                    isValid = false;
                    hfStatusi.Value = "false";
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Zgjidhni motivin e menuve!", pnlMesazhi);
                    return isValid;
                }
                if (hfThemeAmbjKr.Value == null || hfThemeAmbjKr.Value == "")
                {
                    isValid = false;
                    hfStatusi.Value = "false";
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Zgjidhni motivin e ambjentit kryesor!", pnlMesazhi);
                    return isValid;
                }
                if (hfThemeJQuery.Value == null || hfThemeJQuery.Value == "")
                {
                    isValid = false;
                    hfStatusi.Value = "false";
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Zgjidhni motivin e gridave te regjistrimeve!", pnlMesazhi);
                    return isValid;
                }
                if (hfBgImage.Value == null || hfBgImage.Value == "")
                {
                    isValid = false;
                    hfStatusi.Value = "false";
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Zgjidhni imazhin e sfondit!", pnlMesazhi);
                    return isValid;
                }               
                if (hfShtimModifikim.Value == "modifikim")
                {
                    DbCore.DbAdmin.clsThemesAmbjente theme = new DbCore.DbAdmin.clsThemesAmbjente(int.Parse(hfId.Value.ToString()));
                    if (theme.DefaultTheme == true)
                    {
                        isValid = false;
                        hfStatusi.Value = "false";
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Motivet default nuk mund te modifikohen!", pnlMesazhi);
                        return isValid;
                    }

                }
                if (txtKodi.Text != "")
                {
                    if (DbCore.DbAdmin.clsThemesAmbjente.ekzistonThemeAmbjent(txtKodi.Text, DbCore.mySessionObjects.ktheIdPerdoruesi(Session)) && (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim"))
                    {
                        isValid = false;
                        hfStatusi.Value = "false";
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ekziston nje motiv me kete kod!Ju lutemi shenoni nje kod tjeter.", pnlMesazhi);
                        return isValid;
                    }
                }
                else
                {
                    isValid = false;
                    hfStatusi.Value = "false";
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ju lutemi shenoni kodin e motivit.", pnlMesazhi);
                    return isValid;
                }
                //if (hfThemeFramet.Value.ToString() == "")
                //{
                //    isValid = false;
                //    hfStatusi.Value = "false";
                //    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ju lutemi zgjidhni nje motiv per menute.", pnlMesazhi);
                //    return isValid;
                //}
            }
            return isValid;
        }

        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida();
            filtri.FiltraKodi = cmbFiltra.Text;
            filtri.FiltraShenime = cmbFiltra.Text;
            filtri.FiltraUniversal = false;// Universal_ASPxCheckBox.Checked;
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvThemesAmbjente", "UserThemes.aspx", idNdermarrje);
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = gvThemesAmbjente.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("Kodi", gvThemesAmbjente);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = gvThemesAmbjente.GetSortedColumns();
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
            //    filtri.KoloneRenditje = "Kodi";
            //    filtri.DrejtimRenditje = true;
            //}
            //DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();
            //oPerdorues = (DbCore.DbAdmin.clsPerdorues)(CacheLayer.GlobalCacheManager.MySessionCache["oClsPerdoruesi"]);
            filtri.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            filtri.IdNdermarje = idNdermarrje;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            filtri.IdStatusDok = 1;

            mesazh = filtri.ruaj();
            //mbushComboBoxFiltra();
            clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvThemesAmbjente", 1, "UserThemes.aspx");
            percaktoTemplateMenu(DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), ASPxMenu1);

            if (mesazh.Status == true)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            hfStatusi.Value = "true";
            cmbFiltra.Text = "";
        }

        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            //filtra.mbushFiltraGridaSipasFiltraEmri(cmbFiltra.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvThemesAmbjente", "UserThemes.aspx", idNdermarrje);
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            //DbCore.DbAdmin.clsFiltraGrida filtra = dbAdmin.merrFiltraGridaSipasFiltraEmri(cmbFiltra.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra();
                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idNdermarrje, "gvThemesAmbjente", 1, "UserThemes.aspx");
                percaktoTemplateMenu(DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), ASPxMenu1);

                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                // konfiguroVleraFillestare();
                hfStatusi.Value = "true";
                gvThemesAmbjente.FilterExpression = String.Empty;
            }
        }

        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            List<object> rreshtat = gvThemesAmbjente.GetSelectedFieldValues("IdThemeAmbjente");
            if (rreshtat.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhZgjidhniNje, pnlMesazhi);
                return;
            }
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            //clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "", pnlMesazhi);
            List<string> TeFshire = new List<string>(), TePaFshire = new List<string>();
            DbCore.DbAdmin.clsThemesAmbjente theme = new DbCore.DbAdmin.clsThemesAmbjente();
            foreach (object id in rreshtat)
            {
                theme.mbushThemeSipasID(Convert.ToInt32(id));
                bool defaulti = DbCore.DbAdmin.clsThemesAmbjente.eshteThemeDefault(Convert.ToInt32(id));
                bool zgjedhur = DbCore.DbAdmin.clsThemesAmbjente.eshteThemeZgjedhur(Convert.ToInt32(id));

                if (defaulti == true || zgjedhur == true)
                {
                    TePaFshire.Add(theme.KodTheme);
                    continue;
                }

                mesazh = theme.Fshi();
                if (theme.IdThemeAmbjente == 0)
                    continue;
                if (mesazh.Status)
                {
                    hiqThemeAmbjenteNgaGrida(theme.IdThemeAmbjente);
                    TeFshire.Add(theme.KodTheme);

                    ASPxPageControl1.ActiveTabIndex = 0;
                    hfStatusi.Value = "true";
                }
            }

            string mesazhInfoGabim = "", mesazhInfoSukses = "";
            if (TePaFshire.Count == 1)
                mesazhInfoGabim = String.Format("{0}{1}{2}", prefixMesazhNjejes, String.Join(";", TePaFshire), suffixMesazhNjejesGabimi);
            else
                if (TePaFshire.Count > 1)
                    mesazhInfoGabim = String.Format("{0}{1}{2}", prefixMesazhShumes, String.Join(";", TePaFshire), suffixMesazhShumesGabimi);
            if (TeFshire.Count == 1)
                mesazhInfoSukses = String.Format("{0}{1}{2}", prefixMesazhNjejes, String.Join(";", TeFshire), suffixMesazhNjejesSuksesi);
            else
                if (TeFshire.Count > 1)
                    mesazhInfoSukses = String.Format("{0}{1}{2}", prefixMesazhShumes, String.Join(";", TeFshire), suffixMesazhShumesSuksesi);
            if (mesazhInfoGabim != "" && mesazhInfoSukses != "")
                mesazhInfoGabim += lidhesMesazhi + mesazhInfoSukses;
            if (mesazhInfoGabim != "")
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhInfoGabim, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazhInfoSukses, pnlMesazhi);
        }

        private void hiqThemeAmbjenteNgaGrida(int id)
        {
            if (this.gvThemesAmbjente.DataSource != null)
            {
                DataTable dt = (DataTable)gvThemesAmbjente.DataSource;
                DataRow[] drs = dt.Select("IdThemeAmbjente = " + id);
                if (drs.Length > 1)
                    throw new Exception("GABIM: Ndodhen 2 themes me te njejten id ne gride");
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                dt.Rows.Remove(dr);
                gvThemesAmbjente.DataBind();
            }
            else mbushListeThemes();
        }

        //private void mbushComboBoxFiltra()
        //{
        //    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka("gvThemesAmbjente", "UserThemes.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //    DbCore.DbAdmin.colFiltratGrida colFiltra = new DbCore.DbAdmin.colFiltratGrida(koka.IdGridaKoka, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //    colFiltra.Insert(0, new DbCore.DbAdmin.clsFiltraGrida()); //colFiltra = dbAdmin.merrGjitheFiltratGridaByGridaKoka(koka.IdGridaKoka, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        //    MenuFilter.colekstioni = colFiltra;
        //    MenuFilter.ValueField = "IdFiltra";
        //    MenuFilter.TextField = "FiltraShenime";
        //}

        protected void gvThemesAmbjente_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            //konfiguroVleraFillestare();           
            if (e.CallbackName == "APPLYFILTER" && e.Args[0] == "")
            {
                DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
                ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
                cmbFiltra.Text = "";
            }
        }

        protected void gvThemesAmbjente_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName == "DefaultTheme" || e.Column.FieldName == "Zgjedhur")
            {
                (e.Editor as ASPxComboBox).Items.Clear();
                (e.Editor as ASPxComboBox).Items.Add("");
                (e.Editor as ASPxComboBox).Items.Add("Po", true);
                (e.Editor as ASPxComboBox).Items.Add("Jo", false);
            }
        }

        protected void gvThemesAmbjente_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string idkomponente = "";
            string kodkonfigurimi = "";

            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                kodkonfigurimi = arr[1];
                idkomponente = arr[0];
                if (arr[2] == "")
                    gvThemesAmbjente.FilterExpression = "";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    //filtra.mbushFiltraGridaSipasFiltraKodi(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvThemesAmbjente", "UserThemes.aspx", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
                    //DbCore.DbAdmin.clsFiltraGrida filtra = dbAdmin.merrFiltraGridaSipasFiltraKodi(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    if (filtra.FiltraKodi != null)
                    {
                        gvThemesAmbjente.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvThemesAmbjente);
                        hfStatusi.Value = "true";
                    }
                    else hfStatusi.Value = "false";
                }
            }
            else if (arr.Length == 2)//nese eshte zgjedhur nje konfigurim tek combo e konfigurimeve
            {
                kodkonfigurimi = arr[1];
                idkomponente = arr[0];
            }
            else
            {
                idkomponente = e.Parameters;
            }
            gvThemesAmbjente.Selection.UnselectAll();
        }

        protected void gvThemesAmbjente_DataBound(object sender, EventArgs e)
        {// shton colonen # per selektim dhe disa karakteristika te grides
            if (this.gvThemesAmbjente.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
                //perzgjidh
                DevExpress.Web.GridViewCommandColumn check = new DevExpress.Web.GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true; check.Width = Unit.Percentage(2);
                gvThemesAmbjente.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                gvThemesAmbjente.Settings.ShowFilterRowMenu = true;
                check.SetColVisibleIndex(0);
                //behet per te afishuar rreshtin qe do sherbej per filtrim
                gvThemesAmbjente.Settings.ShowFilterRow = true;
                gvThemesAmbjente.Columns.Add(check);

                gvThemesAmbjente.KeyFieldName = "IdThemeAmbjente";
                gvThemesAmbjente.SettingsBehavior.AllowSelectByRowClick = true;
                gvThemesAmbjente.SettingsBehavior.AllowFocusedRow = true;
            }
        }

        protected void gvThemesAmbjente_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            if (e.Column.FieldName == "PershkrimTheme")
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
            else
            {
                e.Values.Clear();
                e.AddValue("(Te gjithe)", string.Empty, "true");
            }
        }

        //protected void Ruaj_Click(object sender, EventArgs e)
        //{
        //    int idUser = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);            
        //    DbCore.DbAdmin.clsPerdorues user = new DbCore.DbAdmin.clsPerdorues(idUser);
        //    user.IdStatusDok = 1;
        //    //user.IdTheme = Convert.ToInt32(CacheLayer.GlobalCacheManager.MySessionCache["IdTheme"].ToString());
        //    //user.IdTheme = DbCore.mySessionObjects.merrIdThemeNgaSesioni(Session);
        //    user.modifiko();
        //}

        protected void mbushTemplate()
        {
            DbCore.DbAdmin.clsTheme theme = new DbCore.DbAdmin.clsTheme();
            DbCore.DbAdmin.colTheme themes = new DbCore.DbAdmin.colTheme();
            themes = theme.merrGjitheTheme();
            DataThemes.DataSource = themes;
            DataThemes.DataBind();
        }

        //protected void DataThemes_ItemCommand(object source, DevExpress.Web.DataViewItemCommandEventArgs e)
        //{
        //    string emriTheme = ((DevExpress.Web.ASPxButton)(e.CommandSource)).ClientInstanceName.ToString();
        //    int output = 0;
        //    if (int.TryParse(emriTheme, out output))
        //    {
        //        //rasti Ruaj theme default per userin
        //        int idUser = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
        //        int idNdermVit = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
        //        DbCore.DbAdmin.clsPerdorues user = new DbCore.DbAdmin.clsPerdorues(idUser);                                

        //        DbCore.DbAdmin.clsTheme MyTheme = new DbCore.DbAdmin.clsTheme();
        //        MyTheme.IdTheme = output;
        //        MyTheme = MyTheme.merrSipasID();
        //        //user.IdTheme = output;
        //        //CacheLayer.GlobalCacheManager.MySessionCache["IdTheme"] = output;
        //        DbCore.mySessionObjects.ruajIdThemeNeSesion(Session, output);
        //        //CacheLayer.GlobalCacheManager.MySessionCache["BackgroundPath"] = MyTheme.PathTheme;
        //        //mySessionObjects.ruajBackgroundPathNeSesion(MyTheme.PathTheme, Session);
        //        user.IdStatusDok = 1;
        //        DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
        //        mesazh = user.modifiko();
        //        if (mesazh.StatusMesazhi == true)
        //        {
        //            clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "Modeli i zgjedhur u ruajt si Template default per kete perdorues", pnlMesazhi);

        //            //Response.Redirect("FaqeKryesore.aspx", true); 
        //        }
        //        else
        //        {
        //            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
        //        }
        //    }
        //    else
        //    {
        //        //rasti Apliko theme pa e ruajtur
        //        DbCore.DbAdmin.clsTheme MyTheme = new DbCore.DbAdmin.clsTheme();
        //        MyTheme.EmriTheme = emriTheme;
        //        MyTheme = MyTheme.merrSipasEmri();
        //        //CacheLayer.GlobalCacheManager.MySessionCache["BackgroundPath"] = MyTheme.PathTheme;
        //        //mySessionObjects.ruajBackgroundPathNeSesion(MyTheme.PathTheme, Session);
        //        //CacheLayer.GlobalCacheManager.MySessionCache["IdTheme"] = MyTheme.IdTheme;
        //        DbCore.mySessionObjects.ruajIdThemeNeSesion(Session, MyTheme.IdTheme);
        //        // Response.Redirect("FaqeKryesore.aspx", true);
        //    }
        //}

        protected void DataThemes_PageIndexChanging(object source, DevExpress.Web.DataViewPageEventArgs e)
        {
            mbushTemplate();
        }

        /// <summary>
        /// nuk perdoret me
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Click(object sender, EventArgs e)
        {
            mbushTemplate();
            //UpdDataThemes.Update();
            //int idUser = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            //int idNdermVit = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            //DbCore.DbAdmin.clsPerdorues user = new DbCore.DbAdmin.clsPerdorues();
            //user.IdPerdorues = idUser;
            //user = user.kthe(idNdermVit);

            //DbCore.DbAdmin.clsTheme MyTheme = new DbCore.DbAdmin.clsTheme();
            //MyTheme.IdTheme = int.Parse(hfBgImage.Value);
            //MyTheme = MyTheme.merrSipasID();
            //user.IdTheme = int.Parse(hfId.Value);
            ////CacheLayer.GlobalCacheManager.MySessionCache["IdTheme"] = int.Parse(hfId.Value);
            //mySessionObjects.ruajIdThemeNeSesion(Session, int.Parse(hfId.Value));
            ////CacheLayer.GlobalCacheManager.MySessionCache["BackgroundPath"] = MyTheme.PathTheme;
            //mySessionObjects.ruajBackgroundPathNeSesion(MyTheme.PathTheme, Session);
            //user.IdStatusDok = 1;
            //DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            //mesazh = user.modifiko();
            //if (mesazh.StatusMesazhi == true)
            //{
            //    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "Modeli i zgjedhur u ruajt si Template default per kete perdorues", pnlMesazhi);
            //    //Response.Redirect("FaqeKryesore.aspx", true); 
            //}
            //else
            //{
            //    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            //}
        }

        protected void DataThemes_CustomCallback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            mbushTemplate();
        }
    }
}