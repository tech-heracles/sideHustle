using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using DevExpress.Web;
using System.Globalization;
using System.Resources;
using DbCore;
using DbCore.DbGIS;
using DbCore.DbAdmin;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using PlatinumWeb.Templates;

namespace PlatinumWeb
{
    public partial class GISWorkspace : MyPageBase
    {
        private const int idKategoria = 146;
        private const string kodiNivelRegj = "GIS/WS";
        private int idPerdoruesi, idGjuha, idNdermarrje, idViti;

        private ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

        protected void Page_Load(object sender, EventArgs e)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);

            if (hfState.Count == 0)
            {
                if (!DbCore.mySessionObjects.isLogedIn(Session))
                {
                    DbCore.clsFunksione.logout(Session, true, "FaqePaautorizuar");
                }
                idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
                {
                    Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerdoruesi);
                    return;
                }
                idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
                idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
                hfState.Set("idPerdoruesi", idPerdoruesi);
                hfState.Set("idGjuha", idGjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idViti", idViti);
                hfState.Set("Meme", DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
            }
            else
            {
                idPerdoruesi = (int)hfState["idPerdoruesi"];
                idGjuha = (int)hfState["idGjuha"];
                idNdermarrje = (int)hfState["idNdermarrje"];
                idViti = (int)hfState["idViti"];
            }

            if (!IsCallback)
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                    DbCore.clsFunksione.logout(Session, false, "", false);

            if (!Page.IsPostBack)
            {
                EmrateTabeve(cultinf, rm);
                mbushHiddenFieldMePerkthime(cultinf, rm);

                konfiguroVleraFillestare(idNdermarrje, idGjuha);

                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, "GISWorkspace.aspx");
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
            }
            else
            {
                mbushGridWorkspaceNgaDB();
                konfiguroGride(idNdermarrje, idPerdoruesi);
            }
            GridUtil.konfigGrideListeEMadhePaTheme(ASPxGridView_GISWorkspace, "id_workspace");
            percaktoTemplateMenu(idGjuha, idViti, idPerdoruesi, idNdermarrje, ASPxMenu1);
            clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, "ASPxGridView_GISWorkspace", int.Parse(cmbKonfigurimi.Value.ToString()), "GISWorkspace.aspx");
            GridUtil.ToolTipButonaveMbiGride(ASPxGridView_GISWorkspace, cultinf, rm);
            AspxWebControlUtils.perkthePopUp(popFshi, rm.GetString("labelKujdes", cultinf), lblMsgbox, rm.GetString("labelAdministrimiMsgJeniSigurt", cultinf), ButtonCancel, rm.GetString("labelAnullo", cultinf));
        }

        /// <summary>
        /// Vendos emrat e tabeve ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateTabeve(CultureInfo cultinf, ResourceManager rm)
        {
            ASPxPageControl1.TabPages[0].Text = rm.GetString("TePergjithshmeTab", cultinf);
            ASPxPageControl1.TabPages[1].Text = rm.GetString("labelAdministrimiInformacion", cultinf);
            konfigurimi_Label.Text = rm.GetString("lblModeli", cultinf);
        }

        /// <summary>
        /// Metode qe sherben per te kaluar perkthime te js
        /// </summary>
        /// <param name="cultinf">Merr culture info</param>
        /// <param name="rm">Merr Resource Manager</param>
        private void mbushHiddenFieldMePerkthime(CultureInfo cultinf, ResourceManager rm)
        {
            hfState.Set("msgPoTransferohetTeDhenatShtypniPerseriRuaj", rm.GetString("msgPoTransferohetTeDhenatShtypniPerseriRuaj", cultinf));
            hfState.Set("msgZgjdhniNjeNgaElementetEListes", rm.GetString("msgZgjdhniNjeNgaElementetEListes", cultinf));
        }

        private void konfiguroVleraFillestare(int idNdermarrje, int idGjuha)
        {
            DbCore.DbAdmin.clsKomponente komponente = new DbCore.DbAdmin.clsKomponente("GISWorkspace.aspx");

            //Kombo e konfigurimit te ambjentit
            DbCore.DbShare.colKonfigurimAmbjenti col = new DbCore.DbShare.colKonfigurimAmbjenti();
            col.mbushKonfigAmbjSipasIdKategoriKodNivel(idKategoria, kodiNivelRegj, idNdermarrje, idPerdoruesi, idGjuha);
            ConfigureAspxComboBox.shtoKolonaPerCombo(cmbKonfigurimi, "{0}", "KodKonfigAmbjente", "Kodi", true, "PershkrimKonfigAmbjente", "Pershkrimi", false, "");
            ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(cmbKonfigurimi, col);
            hfKonffillestar.Value = col[0].KodKonfigAmbjente + ";" + col[0].PershkrimKonfigAmbjente;

            //Mbushet grida e gjithe workspaceve te konfiguruara
            mbushGridWorkspaceNgaDB();
            konfiguroGride(idNdermarrje, idPerdoruesi);
            GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "ASPxGridView_GISWorkspace", ASPxGridView_GISWorkspace, cmbKonfigurimi.Text, Convert.ToString(komponente.IdKomponente), idGjuha);

            //Mbushet kombo e ndermarrjes
            konfiguroComboNdermarrje();

            //percaktohet tabi 1 si tab kryesor
            ASPxPageControl1.ActiveTabIndex = 0;
        }

        private void mbushGridWorkspaceNgaDB()
        {
            DataTable dt = DbCore.DbGIS.colWorkspaceGIS.merrAllWorkspaceGrideAmbjenti();
            DbCore.mySessionObjects.ruajGrideNeSession(Session, dt);
            ASPxGridView_GISWorkspace.DataSource = dt;
            ASPxGridView_GISWorkspace.DataBind();
            dt.Dispose();
        }


        /// <summary>
        /// perdoret per te konfiguruar griden. Per te dhenat mbi kolonat qe do te shfaqen nga databaza si dhe percakton disa karakteristika te grides
        /// </summary>
        /// :  <see cref="GridUtil.percaktoVisibleColumns"/>
        /// :  <see cref="DbCore.clsFunksione.konfiguroGrideListeMadhe(grida,celesigrides)"/>
        /// <param name="idNdermarrje"></param>
        private void konfiguroGride(int idNdermarrje, int idPerdorues)
        {
            ASPxGridView_GISWorkspace.Columns["#"].VisibleIndex = 0;
        }

        private void konfiguroComboNdermarrje()
        {
            ConfigureAspxComboBox.shtoKolonaPerCombo(cmbNdermarrje, "{0}", "NdermarrjeKodi", "NdermarrjeKodi", true, "NdermarrjePershkrimi", "NdermarrjePershkrimi", true, "IdNdermarrje");
            ConfigureAspxComboBox.mbushComboNdermarrjePerGIS(cmbNdermarrje, true);
        }

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(idGjuha, idViti, idPerdoruesi, idNdermarrje, ASPxMenu1);
        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="idNderVit"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        private void percaktoTemplateMenu(int idGjuha, int idViti, int idPerdorues, int idNdermarrje, ASPxMenu aSPxMenu1)
        {
            bool meme = (bool)hfState["Meme"];
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, "GISWorkspace.aspx", this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, hfShtimModifikim.Value == "modifikim" ? false : true, false, false, meme);
        }

        /// <summary>
        /// perdoret per te trajtuar ngjarjet e butonave te menuse. Shkon ne faqen e modifikimit te artikujve kur perdoruesi klikon butonin modifiko
        /// ose ne faqen e shtimit te artikujve kur perdoruesi klikon butonin shto
        /// </summary>
        /// <param name="source"> derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {   //veprimet e menuse
            if (e.Item.Name == "Ruaj")
            {
                Page.Validate("entries");
                if (Page.IsValid == false) return;

                ruajWorkspace();
            }
            else if (e.Item.Name == "Rifresko")
            {
                reloadWorkspace();
            }
        }

        #region Ruaj/Modifiko Workspace
        private void ruajWorkspace()
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            
            try
            {
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                bool eshteShtim = false;
                if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim") eshteShtim = true;

                if ((eshteShtim && !((bool)hfTeDrejta["Shtim"])) || (!eshteShtim && !((bool)hfTeDrejta["Modifikim"]))) {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("mesazhRaportNukKeniTeDrejta", cultinf), pnlMesazhi);
                    hfStatusi.Value = "false";
                    return;
                }

                int idNdLidhese = (int)cmbNdermarrje.Value;

                if (txtPasswordDB.Text != txtKonfirmoPasswordDB.Text)
                    mesazh.PershkrimMesazhi = rm.GetString("msgFjalekalimJoInjejteMeVerfikimin", cultinf);
                else if (eshteShtim && txtPasswordDB.Text == "")
                    mesazh.PershkrimMesazhi = rm.GetString("msgNdryshimFjalekalimiPlotesoPassERi", cultinf);
                else
                {
                    if (eshteShtim)
                        mesazh = clsGeoserverGIS.AddNewWorkspace(txtWorkspace.Text, idNdLidhese, txtUrl.Text, txtUsername.Text, txtPassword.Text, txtDatabase.Text, txtPortDB.Text, txtUserDB.Text, txtPasswordDB.Text, txtWSFunksionesh.Text, txtWSDefault.Text, txtWSOrientues.Text, txtWSPublik.Text);
                    else
                        mesazh = clsGeoserverGIS.ModifyWorkspace(int.Parse(hfId.Value.ToString()), txtWorkspace.Text, idNdLidhese, txtUrl.Text, txtUsername.Text, txtPassword.Text, txtDatabase.Text, txtPortDB.Text, txtUserDB.Text, txtPasswordDB.Text, txtWSFunksionesh.Text, txtWSDefault.Text, txtWSOrientues.Text, txtWSPublik.Text);
                }


                if (!mesazh.Status)
                {
                    hfStatusi.Value = "false";
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                }
                else
                {
                    hfStatusi.Value = "true";
                    modifikoPasRuajNeGrid(cultinf, rm, idNdLidhese, eshteShtim);
                    konfiguroGride(idNdermarrje, idPerdoruesi);
                    ASPxPageControl1.ActiveTabIndex = 0;
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                }

            }
            catch (Exception e)
            {
                hfStatusi.Value = "false";
                NLog.LogManager.GetCurrentClassLogger().Error(e.Message);
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, e.Message, pnlMesazhi);
                return;
            }
        }

        private void modifikoPasRuajNeGrid(CultureInfo cultinf, ResourceManager rm, int idNdermarrje, bool eshteShtim)
        {
            if (ASPxGridView_GISWorkspace.DataSource != null)
            {
                DataTable dt = (DataTable)ASPxGridView_GISWorkspace.DataSource;
                DataRow newDr = clsWorkspaceGIS.ktheWorkspaceSipasId(idNdermarrje);
                DataRow[] drs = dt.Select("id_workspace = " + newDr.ItemArray[0]);

                if (eshteShtim && drs.Length == 0)
                {
                    dt.ImportRow(newDr);
                }
                else if (!eshteShtim && drs.Length == 1)
                {
                    object[] arr = newDr.ItemArray;
                    DataRow dr = drs[0];
                    dr.ItemArray = arr;
                }
                else
                    throw new Exception(rm.GetString("labelRaportMesazhKonfiguimiEkziston", cultinf));
            }
            else
                mbushGridWorkspaceNgaDB();
        }
        #endregion

        #region Fshi Workspace
        /// <summary>
        /// perdoret per te fshire rreshtat e zgjedhur te artikujve nqs perdoruesi konfirmon fshirjen
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session); 
            hfVeprimeObj.Value= new JavaScriptSerializer().Serialize("");

            #region fshirja
            List<object> rreshtaSelected = ASPxGridView_GISWorkspace.GetSelectedFieldValues("id_workspace");

            int countRreshtaMbetur = 0; 
            string wsTeFshira = "", wsTePafshira = "";

            clsMesazh mesazh = new clsMesazh();
            colWorkspaceGIS tempAllWorkspace = new colWorkspaceGIS(true);
            clsWorkspaceGIS tempWorkspace = new clsWorkspaceGIS();

            colNdermarrjet colNdermarrjePerGIS = new colNdermarrjet();
            colNdermarrjePerGIS.mbushNdermarrjePerGIS(false);
            colNdermarrjet tempNdermarrje = new colNdermarrjet();

            foreach (int id in rreshtaSelected)
            {
                tempWorkspace = tempAllWorkspace.Find(x => x.Id_workspace == Convert.ToInt32(id));
                mesazh = clsGeoserverGIS.FshiWorkspaceTeZgjedhur(tempWorkspace);
                if (!mesazh.Status)
                {
                    countRreshtaMbetur++;
                    wsTePafshira += ((wsTePafshira == "") ? "'" : "; '") + mesazh.PershkrimMesazhi + "'";
                }
                else
                {
                    wsTeFshira += ((wsTeFshira == "") ? "'" : "; '") + mesazh.PershkrimMesazhi + "'";
                    hiqWorkspaceNgaGrida(cultinf, rm, tempWorkspace.Id_workspace);
                    tempNdermarrje.Add(colNdermarrjePerGIS.Find(x=> x.IdNdermarrje == tempWorkspace.IDNDERMARJE));
                }
            }
            #endregion

            #region mesazhi
            mesazh = ktheMesazhMultiSelectedGrideVeprimesh(rreshtaSelected.Count, countRreshtaMbetur, wsTeFshira, wsTePafshira, rm.GetString("msgZgjidhniTePaktenNjeElement", cultinf), rm.GetString("suffixMesazhNjejesLidhurGabimi", cultinf), rm.GetString("suffixMesazhNjejesSuksesi", cultinf), rm.GetString("suffixMesazhShumesLidhurGabimi", cultinf), rm.GetString("suffixMesazhShumesSuksesi", cultinf), rm.GetString("msgLidhesMesazhi", cultinf));
            if (mesazh.Status)
            {
                hfVeprimeObj.Value = new JavaScriptSerializer().Serialize(tempNdermarrje);
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            }
            else
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            pnlMesazhi.Update();
            #endregion

            #region pasFshirjes
            if (rreshtaSelected.Count - countRreshtaMbetur > 0)
            {
                konfiguroComboNdermarrje();
                ASPxPageControl1.ActiveTabIndex = 0;
                hfStatusi.Value = "true";
            }
            #endregion
        }

        private DbCore.clsMesazh ktheMesazhMultiSelectedGrideVeprimesh(int rreshtaSelected, int rreshtaMbetur, string elementeMeSukses, string elementePaSukses, string msgZgjidhniTePaktenNjeElement, string njejesGabimi, string njejesSuksesi, string shumesGabimi, string shumesSuksesi, string msgLidhes)
        {
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            
            if (rreshtaSelected == 0)
                mesazh.PershkrimMesazhi = msgZgjidhniTePaktenNjeElement;
            else if (rreshtaSelected == 1 && rreshtaMbetur == 0 )
            {
                mesazh.Status = true;
                mesazh.PershkrimMesazhi = elementeMeSukses + ":" + njejesSuksesi;
            }
            else if (rreshtaMbetur == 0)
            {
                mesazh.Status = true;
                mesazh.PershkrimMesazhi = elementeMeSukses + ":" + shumesSuksesi;
            }
            else if (rreshtaSelected == 1 && rreshtaMbetur == 1)
                mesazh.PershkrimMesazhi = elementePaSukses + ":" + njejesGabimi;
            else if (rreshtaSelected == rreshtaMbetur)
                mesazh.PershkrimMesazhi = elementePaSukses + ":" + shumesGabimi;
            else if (rreshtaSelected == 2 && rreshtaMbetur == 1)
                mesazh.PershkrimMesazhi = elementeMeSukses + ":" + njejesSuksesi + msgLidhes + elementePaSukses + ":" + njejesGabimi;
            else if (rreshtaSelected - rreshtaMbetur == 1)
                mesazh.PershkrimMesazhi = elementeMeSukses + ":" + njejesSuksesi + msgLidhes + elementePaSukses + ":" + shumesGabimi;
            else if (rreshtaMbetur == 1)
                mesazh.PershkrimMesazhi = elementeMeSukses + ":" + shumesSuksesi + msgLidhes + elementePaSukses + ":" + njejesGabimi;
            else
                mesazh.PershkrimMesazhi = elementeMeSukses + ":" + shumesSuksesi + msgLidhes + elementePaSukses + ":" + shumesGabimi;

            return mesazh;
        }

        private void hiqWorkspaceNgaGrida(CultureInfo cultinf, ResourceManager rm, int idWorkspace)
        {
            if (ASPxGridView_GISWorkspace.DataSource != null)
            {
                DataTable dt = (DataTable)ASPxGridView_GISWorkspace.DataSource;
                DataRow[] drs = dt.Select("id_workspace = " + idWorkspace);
                if (drs.Length > 1)
                    throw new Exception(rm.GetString("labelRaportMesazhGabimiDyKonfigurimeNeGride", cultinf));
                if (drs.Length == 0) return;
                DataRow dr = drs[0];
                dt.Rows.Remove(dr);
                ASPxGridView_GISWorkspace.DataBind();
            }
            else
                mbushGridWorkspaceNgaDB();
        }
        #endregion

        #region Rifresko Workspace
        private void reloadWorkspace()
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);

            #region reload
            List<object> rreshtaPerTuRifreskuar = ASPxGridView_GISWorkspace.GetSelectedFieldValues("GEOURL").Distinct().ToList();

            int countTePaReload= 0;
            string wsTeRifreskuar= "", wsTePafreskuar = "";

            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            colWorkspaceGIS tempAllWorkspace = new colWorkspaceGIS(true);
            clsWorkspaceGIS tempWorkspace = new clsWorkspaceGIS();            

            foreach (string id in rreshtaPerTuRifreskuar)
            {
                tempWorkspace = tempAllWorkspace.Find(x => x.GEOURL == id.ToString());
                mesazh = clsGeoserverGIS.ReloadGeoserverStatus(tempWorkspace.GEOURL, tempWorkspace.GEOUSER, tempWorkspace.GEOPASSWORD);
                if (mesazh.Status)
                    wsTeRifreskuar += ((wsTeRifreskuar == "") ? "'" : "; '") + tempWorkspace.GEOURL + "'";
                else
                {
                    countTePaReload++;
                    wsTePafreskuar += ((wsTePafreskuar == "") ? "'" : "; '") + tempWorkspace.GEOURL + "'";
                }
            }
            #endregion

            #region mesazhi
            mesazh = ktheMesazhMultiSelectedGrideVeprimesh(rreshtaPerTuRifreskuar.Count, countTePaReload, wsTeRifreskuar, wsTePafreskuar, rm.GetString("msgZgjidhniTePaktenNjeElement", cultinf), rm.GetString("suffixMesazhNjejesReloadGabimi", cultinf), rm.GetString("suffixMesazhNjejesReloadSukses", cultinf), rm.GetString("suffixMesazhShumesReloadGabimi", cultinf), rm.GetString("suffixMesazhShumesReloadSukses", cultinf), rm.GetString("msgLidhesMesazhi", cultinf));
            if (mesazh.Status)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            pnlMesazhi.Update();
            #endregion
        }
        #endregion

        #region Grida kryesore GISWorkspace
        /// <summary>
        /// sherben per te ruajtur filtrin e zgjedhur
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida();
            filtri.FiltraKodi = cmbFiltra.Text;
            filtri.FiltraShenime = cmbFiltra.Text;
            filtri.FiltraUniversal = false;
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuha, "ASPxGridView_GISWorkspace", "GISWorkspace.aspx", idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = ASPxGridView_GISWorkspace.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("id_workspace", ASPxGridView_GISWorkspace);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = ASPxGridView_GISWorkspace.GetSortedColumns();
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
            //    filtri.KoloneRenditje = "id_workspace";
            //    filtri.DrejtimRenditje = true;
            //}
            filtri.IdPerdoruesi = idPerdoruesi;
            filtri.IdNdermarje = idNdermarrje;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            filtri.IdStatusDok = 1;
            mesazh = filtri.ruaj();
            clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, "ASPxGridView_GISWorkspace", int.Parse(cmbKonfigurimi.Value.ToString()), "GISWorkspace.aspx");
            percaktoTemplateMenu(idGjuha, idViti, idPerdoruesi, idNdermarrje, ASPxMenu1);
            if (mesazh.Status == true)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            hfStatusi.Value = "true";
            cmbFiltra.Text = "";
        }

        /// <summary>
        /// fshin filtrin e zgjedhur dhe aplikuar mbi gride, fshirje nga DB ku ai eshte ruajtur, dhe jo pastrimi i grides nga aplikimi i filtrit
        /// per kete do sherbeje Clear Filter ne fund te grides
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuha, "ASPxGridView_GISWorkspace", "GISWorkspace.aspx", idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                filtra.IdPerdoruesi = idPerdoruesi;
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                mesazh = filtra.fshi();

                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, "ASPxGridView_GISWorkspace", int.Parse(cmbKonfigurimi.Value.ToString()), "GISWorkspace.aspx");
                percaktoTemplateMenu(idGjuha, idViti, idPerdoruesi, idNdermarrje, ASPxMenu1);
                konfiguroVleraFillestare(idNdermarrje, idGjuha);
                hfStatusi.Value = "true";
                this.ASPxGridView_GISWorkspace.FilterExpression = String.Empty;
            }
        }

        protected void RuajKolona_Click(object sender, EventArgs e)
        {
            int idfiltri = 0;
            DbCore.DbAdmin.clsKomponente komponente = new DbCore.DbAdmin.clsKomponente("GISWorkspace.aspx");
            int idkonf = DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(cmbKonfigurimi.Text.Split(';')[0], idNdermarrje);
            DbCore.clsMesazh mesazh = GridUtil.ruajFiltra(idNdermarrje, idPerdoruesi, idGjuha, "ASPxGridView_GISWorkspace", "GISWorkspace.aspx", "FilterDefault", ASPxGridView_GISWorkspace.FilterExpression, ASPxGridView_GISWorkspace, "id_workspace", idkonf, out idfiltri);//ruan filtrin e zgjedhur tek filtrat
            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                return;
            }
            mesazh = GridUtil.ruajkonfigurimgride(ASPxGridView_GISWorkspace, cmbKonfigurimi.Text, idNdermarrje, idPerdoruesi, komponente.IdKomponente, idfiltri, idViti, DbCore.mySessionObjects.ktheCultureInfo(Session), DbCore.mySessionObjects.ktheGjuhe(Session));/// ruan konfigurimin e grides dhe filtrin

            clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, "ASPxGridView_GISWorkspace", int.Parse(cmbKonfigurimi.Value.ToString()), "GISWorkspace.aspx");

            percaktoTemplateMenu(idGjuha, idViti, idPerdoruesi, idNdermarrje, ASPxMenu1);
            if (mesazh.Status) clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

        }

        protected void btnXlsxExport_Click(object sender, EventArgs e)
        {
            try
            {
                gridExport.WriteXlsxToResponse("Workspace", true);
            }
            catch (Exception err)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
            }
        }

        protected void btnPdfExport_Click(object sender, EventArgs e)
        {
            try
            {
                gridExport.WritePdfToResponse("Workspace", true);
            }
            catch (Exception err)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
            }
        }

        /// <summary>
        /// perdoret ne rastet kur grida ben callback per ta rimbushur griden
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void ASPxGridView_GISWorkspace_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            if (e.CallbackName == "COLUMNMOVE" && ASPxGridView_GISWorkspace.AllColumns[int.Parse(e.Args[0])].Width.Value == 0)
                ASPxGridView_GISWorkspace.AllColumns[int.Parse(e.Args[0])].Width = Unit.Percentage(3);
            if (e.CallbackName == "APPLYFILTER" && e.Args[0] == "")
            {
                DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
                ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
                cmbFiltra.Text = "";
            }
            GridUtil.ToolTipButonaveMbiGride(ASPxGridView_GISWorkspace, cultinf, rm);
        }

        /// <summary>
        /// perdoret per te vendosur filtrat tek headeri i grides
        /// </summary>
        /// <param name="sender"> derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxGridView_GISWorkspace_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            string TeGjithe = rm.GetString("GridHeaderFilterFillItemTeGjithe", ci);
            string nga = rm.GetString("GridHeaderFilterFillItemNga", ci);
            if (e.Column.FieldName == "Workspace")
            {
                e.Values.Clear();
                e.AddValue(TeGjithe, string.Empty, "true");
                e.AddValue(nga + " A-D ", string.Empty, e.Column.FieldName + ">'A     ' and " + e.Column.FieldName + " <'DDDDDDD'");
                e.AddValue(nga + " D-G ", string.Empty, e.Column.FieldName + ">'D     ' and " + e.Column.FieldName + "<'GGGGGGG'");
                e.AddValue(nga + " H-K ", string.Empty, e.Column.FieldName + ">'H     ' and " + e.Column.FieldName + "<'KKKKKKK'");
                e.AddValue(nga + " L-O ", string.Empty, e.Column.FieldName + ">'L     ' and " + e.Column.FieldName + "  <'OOOOOOO'");
                e.AddValue(nga + " P-S ", string.Empty, e.Column.FieldName + ">'P     ' and " + e.Column.FieldName + "<'SSSSSSS'");
                e.AddValue(nga + " T-W ", string.Empty, e.Column.FieldName + ">'T     ' and " + e.Column.FieldName + "<'WWWWWWW'");
                e.AddValue(nga + " X-Z ", string.Empty, e.Column.FieldName + ">'X     ' and " + e.Column.FieldName + "<'ZZZZZZZ'");
            }
            else
            {
                e.Values.Clear();
                e.AddValue(TeGjithe, string.Empty, "true");
            }
        }

        protected void ASPxGridView_GISWorkspace_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string idkomponente = "";
            string kodkonfigurimi = "";
            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                kodkonfigurimi = arr[1];
                idkomponente = arr[0];
                if (arr[2] != "")
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuha, "ASPxGridView_GISWorkspace", "GISWorkspace.aspx", idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], idNdermarrje, koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        ASPxGridView_GISWorkspace.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, ASPxGridView_GISWorkspace);
                        konfiguroVleraFillestare(idNdermarrje, idGjuha);
                        hfStatusi.Value = "true";
                    }
                    else
                        hfStatusi.Value = "false";
                }
            }
            else
                if (arr.Length == 2)//nese eshte zgjedhur nje konfigurim tek combo e konfigurimeve
                {
                    kodkonfigurimi = arr[1];
                    idkomponente = arr[0]; //konfiguroGride(idNdermarrje, kodkonfigurimi, Convert.ToInt32(idkomponente));
                }
                else
                {
                    idkomponente = e.Parameters;
                }
            ASPxGridView_GISWorkspace.Selection.UnselectAll();
        }

        protected void ASPxGridView_GISWorkspace_CustomJSProperties(object sender, DevExpress.Web.ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = ASPxGridView_GISWorkspace.PageIndex;
            e.Properties["cpPageRow"] = ASPxGridView_GISWorkspace.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = ASPxGridView_GISWorkspace.VisibleRowCount;
        }

        /// <summary>
        /// perdoret per te shtuar kolonen e selektimit tek grida dhe per te vendosur disa karakteristika te grides
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e"> argumentat</param>
        protected void ASPxGridView_GISWorkspace_DataBound(object sender, EventArgs e)
        {
            // shton colonen # per selektim dhe disa karakteristika te grides
            if (this.ASPxGridView_GISWorkspace.Columns["#"] == null)
            {
                DevExpress.Web.GridViewCommandColumn check = new DevExpress.Web.GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true;
                check.Width = Unit.Percentage(2);
                check.VisibleIndex = 0;
                ASPxGridView_GISWorkspace.Settings.ShowFilterRow = true;
                ASPxGridView_GISWorkspace.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                ASPxGridView_GISWorkspace.Settings.ShowFilterRowMenu = true;
                ASPxGridView_GISWorkspace.Columns.Add(check);
                ASPxGridView_GISWorkspace.KeyFieldName = "id_workspace";
                ASPxGridView_GISWorkspace.SettingsBehavior.AllowSelectByRowClick = true;
                ASPxGridView_GISWorkspace.SettingsBehavior.AllowFocusedRow = true;
            }
        }
        #endregion

    }
}