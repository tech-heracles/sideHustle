using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Web;
using System.Data;
using PlatinumWeb.Templates;
using System.Resources;
using DbCore;
using DbCore.IMBUtils.Types;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{

    /// <summary>
    /// nderfaqja e planifikimit te prodhimit
    /// </summary>
    public partial class Planifikim : MyPageBase
    {
        /// <summary>
        /// konstante per mesazhin e fshirjes ne njejes
        /// </summary>
        /// <summary>
        /// konstante per mesazhin e fshirjes ne shumes
        /// </summary>
        /// <summary>
        /// konstante per mesazhin e mos fshirjes ne njejes per lidhjen
        /// </summary>
        /// <summary>
        /// konstante per mesazhin e mos fshirjes ne shumes per lidhjen
        /// </summary>
        /// <summary>
        /// mesazhi per mos fshirjen ne njejes per periudhen
        /// </summary>
        /// <summary>
        /// mesazhi per mos fshirjen ne shumes per periudhen
        /// </summary>
        /// <summary>
        /// mesazhi per fshirjen ne njejes
        /// </summary>
        /// <summary>
        /// mesazhi per fshirjen ne shumes
        /// </summary>
        /// <summary>
        /// konstante per lidhezen
        /// </summary>
        /// <summary>
        /// mesazhi per mos zgjedhjen e asnje dokumenti
        /// </summary>
        /// <summary>
        /// mesazhi kur ekzistojne dy dokumenta me te njejen id ne grid
        /// </summary>
        /// <summary>
        /// id e ndermarjes
        /// </summary>
        private int idndermarje, /// <summary>
        /// id e perdoruesit
        /// </summary>
        idperdoruesi, /// <summary>
        /// id e ndermarje vitit
        /// </summary>
        idnderviti, idviti;
      
        /// <summary>
        /// vendos themen e faqes
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
  
        private string komponente = "Planifikimi.aspx";
        private string guidString;
        /// <summary>
        /// kur lodohet faqja  mbushet me te dhena dhe kontrollohen ne eshte i autorizuar perdoruesi
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            var rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                DbCore.clsFunksione.logout(Session,true,"FaqePaautorizuar");
            }
            idperdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idperdoruesi);
            }
            idndermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            idviti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            idnderviti = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
            var idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            var cultinf = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo(idGjuha);
            percaktoTemplateMenu(idGjuha, ASPxMenu1, idviti, idperdoruesi, idndermarje);
            if (!IsPostBack)
            {

                guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                hfState.Set("guidString", guidString);
                ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idperdoruesi, idndermarje, cmbKonfigurimi, 61, rm, cultinf, idGjuha);
                cmbKonfigurimi.SelectedIndex = 0;
                var konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()), idGjuha);
                hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
                mbushGridNgaDB();

           
                gvPlanifikimi.FilterExpression = " [IdStatusDok]=1  and ([Ngjyra]='gri' or [Ngjyra]='verdhe') ";
               
                if (Request.QueryString["fshi"] == "po")
                {
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("regjisDokMesazhSukesFshirjeDokument", cultinf), pnlMesazhi);
                }
                else
                {
                    if (Request.QueryString["ruaj"] == "po")
                    {
                        clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("regjMagModifikimiPerfundoiMeSukses", cultinf), pnlMesazhi);
                    }
                }
                var tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idperdoruesi, idndermarje, idviti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                konfiguroGride(idGjuha, rm, cultinf);
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idndermarje, "gvPlanifikimi", gvPlanifikimi, cmbKonfigurimi.Text.Split(';')[0], "803", idGjuha);
            }
            else
            {
                mbushGridNgaSession();
                konfiguroGride(idGjuha, rm, cultinf);
                guidString = (string)hfState["guidString"];
                
            }
            GridUtil.konfigGrideListeEMadhePaTheme(gvPlanifikimi, "IdKokaPlanifikim");
            clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idndermarje, "gvPlanifikimi", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
            if (Request.QueryString["indexrow"] != null)
            {
                gvPlanifikimi.FocusedRowIndex = int.Parse(Request.QueryString["indexrow"]);
            }
            AspxWebControlUtils.perkthePopUp(popFshi, rm.GetString("labelKujdes", cultinf), lblMsgbox, rm.GetString("labelAdministrimiMsgJeniSigurt", cultinf), ButtonCancel, rm.GetString("labelAnullo", cultinf));
            gvPlanifikimi.PercaktoTitlePanel(this, MenuInfo, pnlMesazhi, hfState, idperdoruesi, idndermarje, idviti, idGjuha, int.Parse(cmbKonfigurimi.Value.ToString()), "Planifikimi.aspx", rm, cultinf);
        }













        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        /// <param name="idViti"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNermarrje"></param>
        private void percaktoTemplateMenu(int idGjuha, ASPxMenu aSPxMenu1, int idViti, int idPerdorues, int idNdermarrje)
        {
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, komponente, this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
        }

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), ASPxMenu1, idviti, idperdoruesi, idndermarje);
        }

        /// <summary>
        /// mbush griden me te dhena te ruajtura ne sesion
        /// </summary>
        private void mbushGridNgaSession()
        {
            DataTable tmpObject;
            var sukses = DbCore.mySessionObjects.merrGrideNgaSessioni(Session, out tmpObject);
            if (!sukses)
            {
                mbushGridNgaDB();
            }
            else
            {
                gvPlanifikimi.DataSource = tmpObject;
                gvPlanifikimi.DataBind();
                tmpObject.Dispose();
            }
        }

        /// <summary>
        /// mbush griden me te dhena nga db
        /// </summary>
        private void mbushGridNgaDB()
        {
            var dt = DbCore.DbProdhimi.colKokaPlanifikim.merrKokaPlanifikimDT(idnderviti, DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            DbCore.mySessionObjects.ruajGrideNeSession(Session, dt);
            gvPlanifikimi.DataSource = dt;
            gvPlanifikimi.DataBind();
            dt.Dispose();
        }

        /// <summary>
        /// konfiguron griden
        /// </summary>
        private void konfiguroGride(int idGjuha, ResourceManager rm, System.Globalization.CultureInfo ci)
        {
            KonfigurimComboGride.ShtoStatus(gvPlanifikimi, rm, ci);
            KonfigurimComboGride.ShtoModel(gvPlanifikimi, 44, idndermarje, idperdoruesi, idGjuha, Session, komponente, guidString, "IdKonfigAmbjente");
            KonfigurimComboGride.ShtoNivel(gvPlanifikimi, 44, idndermarje, idperdoruesi, idGjuha, Session, komponente, guidString);
            KonfigurimComboGride.ShtoMagazinaSipasPerdoruesitDheNdermarrjes(gvPlanifikimi, idndermarje, idperdoruesi, Session, komponente, guidString);
            shtoColor();

            gvPlanifikimi.Columns["#"].VisibleIndex = 0;
        }
        private void shtoColor()
        {
            var g = gvPlanifikimi.Columns["Ngjyra"] as GridViewDataColumn;
            g.DataItemTemplate = new MyGaugeTemplate();
        }

  

        /// <summary>
        /// kthen kolonen e idklient ne kombo me kodet e klientit
        /// </summary>
        private void shtoKlient()
        {
            gvPlanifikimi.Columns.Remove(gvPlanifikimi.Columns["IdKlientFurnitor"]);
            var colnew = new GridViewDataComboBoxColumn();
            var colKlientet = new DbCore.DbKontabiliteti.colKlienteFurnitore();
            colKlientet.Add(new DbCore.DbKontabiliteti.clsKlientFurnitor());
            colKlientet.mbushKlienteFurnitoreNdermarrjes(idndermarje);
            colnew.PropertiesComboBox.DataSource = colKlientet;
            colnew.PropertiesComboBox.TextField = "EmertimiKF";
            colnew.PropertiesComboBox.ValueField = "IdKlientFurnitor";
            colnew.FieldName = "IdKlientFurnitor";
            gvPlanifikimi.Columns.Add(colnew);
        }

        /// <summary>
        /// kur grida ben databound per ti shtuar kolonen e selektimit
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvPlanifikimi_DataBound(object sender, EventArgs e)
        {
            if (gvPlanifikimi.Columns["#"] == null)
            {
                var check = new GridViewCommandColumn("#") { ShowSelectCheckbox = true, Width = System.Web.UI.WebControls.Unit.Percentage(2) };
                gvPlanifikimi.Settings.ShowFilterRow = true;
                gvPlanifikimi.Settings.ShowHeaderFilterButton = true;
                gvPlanifikimi.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                gvPlanifikimi.Settings.ShowFilterRowMenu = true;
                gvPlanifikimi.Columns.Add(check);
                gvPlanifikimi.Settings.ShowGroupPanel = true;
                gvPlanifikimi.KeyFieldName = "IdKokaPlanifikim";
                gvPlanifikimi.SettingsBehavior.AllowSelectByRowClick = true;
                gvPlanifikimi.SettingsBehavior.AllowFocusedRow = true;
            }
        }

        /// <summary>
        /// fshin filtrin e zgjedhur dhe aplikuar mbi gride, fshirje nga DB ku ai eshte ruajtur, dhe jo pastrimi i grides nga aplikimi i filtrit
        /// per kete do sherbeje Clear Filter ne fund te grides
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
            var itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            var cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;

            var filtra = new DbCore.DbAdmin.clsFiltraGrida();

            var koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvEkzekutimi", "EkzekutimProdhimi.aspx", idndermarje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                var mesazh = new DbCore.clsMesazh();
                filtra.IdPerdoruesi = idperdoruesi;
                mesazh = filtra.fshi();

                clsToolbarConfig.mbushComboBoxFiltra(DbCore.mySessionObjects.ktheGjuhe(Session), idndermarje, "gvPlanifikimi", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
                percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), ASPxMenu1, idviti, idperdoruesi, idndermarje);
                if (mesazh.Status == true)
                {
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                }
                else
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                }
                cmbFiltra.Text = string.Empty;
                gvPlanifikimi.FilterExpression = " [IdStatusDok]=1  and ([Ngjyra]='gri' or [Ngjyra]='verdhe') ";
            }
        }

        /// <summary>
        /// sherben per te ruajtur filtrin e zgjedhur
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
            var idgjuha = DbCore.mySessionObjects.ktheGjuhe(Session);

            var itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            var cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            var koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvPlanifikimi", komponente, idndermarje, int.Parse(cmbKonfigurimi.Value.ToString()));
            var filtri = new DbCore.DbAdmin.clsFiltraGrida() { FiltraKodi = cmbFiltra.Text, FiltraShenime = cmbFiltra.Text, FiltraUniversal = false, GridaKokaId = koka.IdGridaKoka, FiltraVlera = gvPlanifikimi.FilterExpression, IdPerdoruesi = idperdoruesi, IdNdermarje = idndermarje, IdStatusDok = 1 };
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("NrDok", gvPlanifikimi);
            //var kolona = gvPlanifikimi.GetSortedColumns();
            //if (kolona.Count > 0)
            //{
            //    filtri.KoloneRenditje = kolona[0].FieldName;
            //    if (kolona[0].SortOrder == DevExpress.Data.ColumnSortOrder.Ascending)
            //    {
            //        filtri.DrejtimRenditje = true;
            //    }
            //    else
            //    {
            //        filtri.DrejtimRenditje = false;
            //    }
            //}
            //else
            //{
            //    filtri.KoloneRenditje = "NrDok";
            //    filtri.DrejtimRenditje = true;
            //}

            var mesazh = new DbCore.clsMesazh();
            mesazh = filtri.ruaj();

            clsToolbarConfig.mbushComboBoxFiltra(idgjuha, idndermarje, "gvPlanifikimi", int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
            percaktoTemplateMenu(idgjuha, ASPxMenu1, idviti, idperdoruesi, idndermarje);
            if (mesazh.Status == true)
            {
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            }
            else
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            }
            cmbFiltra.Text = string.Empty;
        }

        /// <summary>
        /// eventet e menuse
        /// </summary>
        /// <param name="source">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {
            var cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            var rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            switch (e.Item.Name)
            {
                case "PrintPreview":
                    if (gvPlanifikimi.FocusedRowIndex == -1)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgZgjidhniNjeDokumentPerTePrintuar", cultinf), pnlMesazhi);
                        Container.Attributes["src"] = "";
                    }
                    else
                    {
                        
                        var id = gvPlanifikimi.GetRowValues(gvPlanifikimi.FocusedRowIndex, "IdKokaPlanifikim").ToString();
                        var clsKoka = new DbCore.DbProdhimi.clsKokaPlanifikim(Convert.ToInt32(id));
                        Container.Attributes["src"] = "RaportiShpejte.aspx?Sesioni=false&emriReal=formatPlanifikimProdhimi&idDokumenti=" + clsKoka.IdKokaPlanifikim + "&printo=false" + "&printo=false&raportdyte=jo&iddesign=" + clsKoka.IdRaportDesing.ToString();
                    }
                    break;
            }
        }

        /// <summary>
        /// fshin rreshtat e selektuar
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            var cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            var rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            var mesazh = new DbCore.clsMesazh();
            var TeFshire = new List<string>();
            var TeLidhur = new List<string>();
            var PeriudheKycur = new List<string>();


            var rreshtat = gvPlanifikimi.GetSelectedFieldValues("IdKokaPlanifikim");
            pergjigja.Text = string.Empty;
          //  DbCore.DbAdmin.clsPeriudhaKontabel periudha;
            var dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            foreach (object id in rreshtat)
            {
                var clsKoka = new DbCore.DbProdhimi.clsKokaPlanifikim(Convert.ToInt32(id));

                var lidhur = dbAdmin.eshteDokumentiILidhur(clsKoka.IdKokaPlanifikim, clsKoka.IdNivel, "T_KOKAPLANIFIKIM", "IDKOKAPLANIFIKIM");
                var autorizimet = DbCore.DbProdhimi.clsKokaPlanifikim.kaAutorizime(clsKoka.IdKokaPlanifikim, mySessionObjects.ktheIdPerdoruesi(Session));
                if (!autorizimet)
                {
                    lidhur = true;
                }
                if (lidhur)
                {
                    TeLidhur.Add(clsKoka.NrDok);
                    continue;
                }

               // periudha = new DbCore.DbAdmin.clsPeriudhaKontabel(clsKoka.DtDok, idndermarje);
                //var mesazhi = periudha.isPeriudheKycur();
               
                bool ekycur = DbCore.DbAdmin.clsPeriudhaKontabel.eshteKycurPeriudheSipasDateDheNdermarrjes(clsKoka.DtDok, idndermarje);
                if (ekycur)
                {
                    PeriudheKycur.Add(clsKoka.NrDok);
                    continue;
                }
                clsKoka.IdPerdoruesi = idperdoruesi;
                mesazh = clsKoka.fshi();
                if (mesazh.Status)
                {
                    hiqNgaGrida(clsKoka.IdKokaPlanifikim);

                    TeFshire.Add(clsKoka.NrDok);
                }
            }
            dbAdmin.Dispose();
            var mesazhInfoGabimLidhur = string.Empty;
            var mesazhInfoGabimPeridheKycur = string.Empty;
            var mesazhInfoSukses = string.Empty;

            if (TeLidhur.Count == 1)
            {
                mesazhInfoGabimLidhur = String.Format("{0}{1}{2}", rm.GetString("msgDokumentiPlanifikimitMeNr", cultinf), String.Join(";", TeLidhur), rm.GetString("msgListaLidhjaSuffixNjejesGabimi", cultinf));
            }
            else
            {
                if (TeLidhur.Count > 1)
                {
                    mesazhInfoGabimLidhur = String.Format("{0}{1}{2}", rm.GetString("msgDokumentatEPlanifikimitMeNr", cultinf), String.Join(";", TeLidhur), rm.GetString("msgListaLidhjaSuffixShumesGabimi", cultinf));
                }
            }
            if (PeriudheKycur.Count == 1)
            {
                mesazhInfoGabimPeridheKycur = String.Format("{0}{1}{2}", rm.GetString("msgDokumentiPlanifikimitMeNr", cultinf), String.Join(";", PeriudheKycur), rm.GetString("regjMagSuffixMesazhNjejesPeriudheKycurGabimi", cultinf));
            }
            else
            {
                if (PeriudheKycur.Count > 1)
                {
                    mesazhInfoGabimPeridheKycur = String.Format("{0}{1}{2}", rm.GetString("msgDokumentatEPlanifikimitMeNr", cultinf), String.Join(";", PeriudheKycur), rm.GetString("regjMagSuffixMesazhShumesPeriudheKycurGabimi", cultinf));
                }
            }
            if (TeFshire.Count == 1)
            {
                mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("msgDokumentiPlanifikimitMeNr", cultinf), String.Join(";", TeFshire), rm.GetString("suffixMesazhNjejesSuksesi", cultinf));
            }
            else
            {
                if (TeFshire.Count > 1)
                {
                    mesazhInfoSukses = String.Format("{0}{1}{2}", rm.GetString("msgDokumentatEPlanifikimitMeNr", cultinf), String.Join(";", TeFshire), rm.GetString("suffixMesazhShumesSuksesi", cultinf));
                }
            }
            mesazhInfoGabimLidhur += mesazhInfoGabimPeridheKycur;
            if (mesazhInfoGabimLidhur != string.Empty && mesazhInfoSukses != string.Empty)
            {
                mesazhInfoGabimLidhur += rm.GetString("msgLidhesMesazhi", cultinf) + mesazhInfoSukses;
                pergjigja.Text = mesazhInfoGabimLidhur;
                pergjigja.ClientVisible = false;
            }
            else
            {
                pergjigja.Text = mesazhInfoSukses;
                pergjigja.ClientVisible = false;
            }

            if (rreshtat.Count == 0)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("regjMagMesazhZgjidhniNje", cultinf), pnlMesazhi);
            }
            else
            {
                if (mesazhInfoGabimLidhur != string.Empty)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhInfoGabimLidhur, pnlMesazhi);
                }
                else
                {
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazhInfoSukses, pnlMesazhi);
                }
            }
            shtoColor();
        }

        /// <summary>
        /// heq nga grida reshtin e fshire
        /// </summary>
        /// <param name="idkoka">id e reshtit te fshire</param>
        private void hiqNgaGrida(int idkoka)
        {
            var cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            var rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (gvPlanifikimi.DataSource != null)
            {
                var dt = (DataTable)gvPlanifikimi.DataSource;
                var drs = dt.Select("IdKokaPlanifikim = " + idkoka);
                if (drs.Length > 1)
                {
                    throw new Exception(rm.GetString("msgGabimNdodhen2DokumentaPlanifikimiMeTeNjejtenId", cultinf));
                }
                if (drs.Length == 0)
                {
                    return;
                }
                var dr = drs[0];
                dt.Rows.Remove(dr);
                gvPlanifikimi.DataSource = dt;
                gvPlanifikimi.DataBind();
                dt.Dispose();
            }
            else
            {
                mbushGridNgaDB();
            }
        }

        /// <summary>
        /// kur grida ben callback
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvPlanifikimi_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            if (e.CallbackName == "COLUMNMOVE" && gvPlanifikimi.AllColumns[int.Parse(e.Args[0])].Width.Value == 0)
            {
                gvPlanifikimi.AllColumns[int.Parse(e.Args[0])].Width = System.Web.UI.WebControls.Unit.Percentage(3);
            }
            if (e.CallbackName == "APPLYFILTER" && e.Args[0] == string.Empty)
            {
                var itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
                var cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
                cmbFiltra.Text = string.Empty;
            }
            shtoColor();

            var cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            var rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            GridUtil.ToolTipButonaveMbiGride(gvPlanifikimi, cultinf, rm);
        }

        /// <summary>
        /// kur perdoruesi i ben grides callbak per te mare konfigurimin e ri
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvPlanifikimi_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            var arr = e.Parameters.Split(';');
            GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), "gvPlanifikimi", gvPlanifikimi, cmbKonfigurimi.Text.Split(';')[0], "803", DbCore.mySessionObjects.ktheGjuhe(Session));
            if (arr.Length == 3)
            {
                if (arr[2] == string.Empty)
                {
                    gvPlanifikimi.FilterExpression = " [IdStatusDok]=1  and ([Ngjyra]='gri' or [Ngjyra]='verdhe') ";
                }
                else
                {
                    var filtra = new DbCore.DbAdmin.clsFiltraGrida();

                    var koka = new DbCore.DbAdmin.clsGridaKoka(DbCore.mySessionObjects.ktheGjuhe(Session), "gvPlanifikimi", komponente, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), int.Parse(cmbKonfigurimi.Value.ToString()));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        gvPlanifikimi.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, gvPlanifikimi);
                    }
                }
            }

            gvPlanifikimi.Selection.UnselectAll();
        }

        /// <summary>
        /// perdoret per te vendosur karakteristika te griden ne javascript
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvPlanifikimi_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvPlanifikimi.PageIndex;
            e.Properties["cpPageRow"] = gvPlanifikimi.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvPlanifikimi.VisibleRowCount;
        }

        /// <summary>
        /// perdoret per te hequr filtrimin e grides kur zgjedhim elementin bosh
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvPlanifikimi_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if (e.Column.FieldName == "IdKlientFurnitor" ||
                 e.Column.FieldName == "IdMagazina" || e.Column.FieldName == "IdKonfigAmbjente" )
            {
                if (Converter.ConvertToInt(e.Value) == 0)
                {
                    e.Criteria = null;
                }
            }
            if (e.Column.FieldName == "Ngjyra")
            {
                if (e.Value.ToString() == "0")
                {
                    e.Criteria = null;
                }
            }
        }

        /// <summary>
        /// perdoret per te shtuar menyra filtrimi
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void gvPlanifikimi_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            var ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            var rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            var TeGjithe = rm.GetString("GridHeaderFilterFillItemTeGjithe", ci);
            var nga = rm.GetString("GridHeaderFilterFillItemNga", ci);
            if (e.Column.FieldName == "NrDok" || e.Column.FieldName == "Shenime")
            {
                e.Values.Clear();
                e.AddValue(TeGjithe, string.Empty, "true");
                e.AddValue(nga + " A-D", string.Empty, e.Column.FieldName + ">'A     ' and " + e.Column.FieldName + " <'DDDDDDD'");
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
    }
}
