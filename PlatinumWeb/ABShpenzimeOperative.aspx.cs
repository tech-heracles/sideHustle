using DbCore;
using DbCore.DbAdmin;
using DbCore.DbAnalizeBuxheti;
using DevExpress.Utils;
using DevExpress.Web;
using PlatinumWeb.Templates;
using System;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class ABShpenzimeOperative : MyPageBase
    {
        private int idNdermarrje;
        private int idKonfig;
        private int idPerdoruesi;
        private int idGjuha;
        private int idViti;
        private int idNdermVit;
        private const string komponente = "ABShpenzimeOperative.aspx";

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

            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (!IsPostBack)
            {
                idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
                idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
                idNdermVit = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
                hfState.Set("idGjuha", idGjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idPerdoruesi", idPerdoruesi);
                hfState.Set("idViti", idViti);
                hfState.Set("komponente", komponente);
                hfState.Set("idNdermVit", idNdermVit);
                hfState.Set("modifikuarNrCeshtjesh", false);
                ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 127, "PBSHOA", rm, ci, idGjuha);
                cmbKonfigurimi.SelectedIndex = 0;
                DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()));
                hfKonffillestar.Value = String.Format("{0};{1}", konf.KodKonfigAmbjente, konf.PershkrimKonfigAmbjente);

                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, komponente);
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DPlot.ToString();
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);

                idKonfig = int.Parse(cmbKonfigurimi.Value.ToString());

                mbushGridenNgaDB(idNdermarrje);

                konfiguroGride(cmbKonfigurimi.Text);
            }
            else
            {
                idNdermarrje = (int)hfState.Get("idNdermarrje");
                idPerdoruesi = (int)hfState.Get("idPerdoruesi");
                idGjuha = (int)hfState.Get("idGjuha");
                idViti = (int)hfState.Get("idViti");
                idNdermVit = (int)hfState.Get("idNdermVit");
                idKonfig = int.Parse(cmbKonfigurimi.Value.ToString());

                mbushGrideNgaSession();
                GridUtil.PercaktoNgjyrenPerKolonatReadOnly(gvShpenzimeOperative, "ShoId", "ShokId", "Pershkrimi", "Niveli", "TotalParaardhes", "TotalAktuale", "DiferencaKerkeseLimitArdhme", "TotalKerkesaTeArdhuraArdhme", "IdNdermarrje", "IdKrijuesi", "IdModifikuesi", "DtKrijimi", "DtModifikimi");
                krijoGrupSummary();
                krijoTotalSummary();
            }

            gvShpenzimeOperative.PercaktoTitlePanel( this, MenuInfo, pnlMesazhi, hfState, idPerdoruesi, idNdermarrje, idViti, idGjuha, idKonfig, komponente, rm, ci, DevExpress.Web.GridViewExportedRowType.All);
            percaktoTemplateMenu();
        }

        private void mbushGridenNgaDB(int idNdermarrje)
        {
            colShpenzimeOperative col = new colShpenzimeOperative(idNdermarrje, idNdermVit);
            gvShpenzimeOperative.DataSource = col;
            gvShpenzimeOperative.DataBind();
            if (col.Count > 0)
                mbushKontrolletMeCeshtje(col[0].KokaId);
            mySessionObjects.ruajObjectNeSesion(Session, col, "shpenzimeOperative");
        }

        /// <summary>
        /// merr ds e grides nga sessioni
        /// </summary>
        public void mbushGrideNgaSession()
        {
            object tmp = mySessionObjects.merrObjectNgaSesioni(Session, "shpenzimeOperative");
            colShpenzimeOperative col = (tmp as colShpenzimeOperative) ?? new colShpenzimeOperative(idNdermarrje, idNdermVit);
            gvShpenzimeOperative.DataSource = tmp;
            gvShpenzimeOperative.DataBind();
        }

        private void konfiguroGride(string kodi)
        {
            GridUtil.PercaktoVisibleColumnsAndTypesGridSipasKodKonfigurimi(idNdermarrje, "gvShpenzimeOperative", gvShpenzimeOperative, kodi, "3009", idGjuha);

            gvShpenzimeOperative.GroupBy(gvShpenzimeOperative.Columns["Kodi"]);

           
            GridUtil.PercaktoBandPerKolona(gvShpenzimeOperative, "FAKTI Viti i kaluar sipas situacionit", "NgaBuxhetiParaardhes", "NgaTeArdhuratParaardhes", "TotalParaardhes");
            GridUtil.PercaktoBandPerKolona(gvShpenzimeOperative, "I pritshmi viti aktual", "NjesiaAktuale", "NgaBuxhetiAktuale", "NgaTeArdhuratAktuale", "TotalAktuale");
            GridUtil.PercaktoBandPerKolona(gvShpenzimeOperative, "Kerkesa per vitin e pare te PBA-se", "NjesiaArdhme", "KostoPerNjesiArdhme", "ShpenzimePlanifikuarArdhme", "LimitiArdhme", "KerkesaGjykatesArdhme", "DiferencaKerkeseLimitArdhme", "ShpenzimeTePlanifikuarTeArdhuraArdhme", "TotalKerkesaTeArdhuraArdhme", "VlersimiZyresArdhme");
            GridUtil.PercaktoBandPerKolona(gvShpenzimeOperative, "Kerkesa per vitin e dyte te PBA-se", "LimitiArdhmePlus1", "KerkesaGjykatesArdhmePlus1", "DiferencaKerkeseLimitArdhmePlus1", "VlersimiZyresArdhmePlus1");
            GridUtil.PercaktoBandPerKolona(gvShpenzimeOperative, "Kerkesa per vitin e trete te PBA-se", "LimitiArdhmePlus2", "KerkesaGjykatesArdhmePlus2", "DiferencaKerkeseLimitArdhmePlus2", "VlersimiZyresArdhmePlus2");
            GridUtil.konfiguroGridaPerBatchEditing(gvShpenzimeOperative, false, false, false);

            GridUtil.PercaktoNgjyrenPerKolonatReadOnly(gvShpenzimeOperative, "ShoId", "ShokId", "Pershkrimi", "Niveli", "TotalParaardhes", "TotalAktuale", "DiferencaKerkeseLimitArdhme", "DiferencaKerkeseLimitArdhmePlus1", "DiferencaKerkeseLimitArdhmePlus2", "TotalKerkesaTeArdhuraArdhme", "IdNdermarrje", "IdKrijuesi", "IdModifikuesi", "DtKrijimi", "DtModifikimi");
            krijoGrupSummary();
            krijoTotalSummary();
        }

        protected void gvShpenzimeOperative_DataBound(object sender, EventArgs e)
        {
            gvShpenzimeOperative.KeyFieldName = "ShokId";

            GridUtil.PercaktoNgjyrenPerKolonatReadOnly(gvShpenzimeOperative, "ShoId", "ShokId", "Pershkrimi", "Niveli", "TotalParaardhes", "TotalAktuale", "DiferencaKerkeseLimitArdhme", "TotalKerkesaTeArdhuraArdhme", "IdNdermarrje", "IdKrijuesi", "IdModifikuesi", "DtKrijimi", "DtModifikimi");
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

        protected void gvShpenzimeOperative_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters.Contains("3009"))//per te kontrolluar nese eshte callback konfigurimi
            {
                konfiguroGride(e.Parameters.Split(';')[1]);
            }
            else
            {
                RuajCeshtjet();
            }
        }

        private void RuajCeshtjet()
        {
            object tmp = mySessionObjects.merrObjectNgaSesioni(Session, "shpenzimeOperative");
            clsMesazh mesazh = new clsMesazh(false, "Pati nje problem ne ruajtjen e ceshtjeve!");
            colShpenzimeOperative col = (tmp as colShpenzimeOperative) ?? new colShpenzimeOperative(idNdermarrje, idNdermVit);
            clsShpenzimeOperativeCeshtje ceshtjet = ModifikoCeshtjet(col[0].KokaId);
            mesazh = ceshtjet.Ruaj();


            if (mesazh.Status)
            {
               
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Modifikimi u krye me sukses!:Green");
            }
            else
            {
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, mesazh.PershkrimMesazhi + "!:Red");
            }
        }

        private clsShpenzimeOperativeCeshtje ModifikoCeshtjet(int kokaId)
        {
            clsShpenzimeOperativeCeshtje ceshtjet = new clsShpenzimeOperativeCeshtje(kokaId);
            ceshtjet.NrCeshtjeParaardhes = Convert.ToInt32(nrCeshtjeshVitiParaArdhes.Value);
            ceshtjet.NrCeshtjeVitiAktual = Convert.ToInt32(nrCeshtjeshVitiAktual.Value);
            ceshtjet.NrCeshtjeVitiPasardhes = Convert.ToInt32(nrCeshtjeshVitiPasardhes.Value);
            ceshtjet.DtDok = DateTime.Now.Date;
            return ceshtjet;
        }

        private void mbushKontrolletMeCeshtje(int kokaID)
        {
            clsShpenzimeOperativeCeshtje ceshtjet = new clsShpenzimeOperativeCeshtje(kokaID);
            nrCeshtjeshVitiParaArdhes.Value = ceshtjet.NrCeshtjeParaardhes;
            nrCeshtjeshVitiAktual.Value = ceshtjet.NrCeshtjeVitiAktual;
            nrCeshtjeshVitiPasardhes.Value = ceshtjet.NrCeshtjeVitiPasardhes;
        }

        protected void gvShpenzimeOperative_BatchUpdate(object sender, DevExpress.Web.Data.ASPxDataBatchUpdateEventArgs e)
        {
            try
            {
                object tmp = mySessionObjects.merrObjectNgaSesioni(Session, "shpenzimeOperative");
                clsMesazh mesazh = new clsMesazh(true);
                clsTeDrejtaRoli teDrejta = new clsTeDrejtaRoli();
                teDrejta.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, komponente);
                colShpenzimeOperative col = (tmp as colShpenzimeOperative) ?? new colShpenzimeOperative(idNdermarrje, idNdermVit);
                if (!teDrejta.DMod)
                {
                    mesazh = new clsMesazh(false, "Nuk keni te drejta per kete veprim!");
                    gvShpenzimeOperative.CancelEdit();
                }
                else
                {
                    for (int i = 0; i < e.UpdateValues.Count; i++)
                    {
                       //string key = e.UpdateValues[i].Keys[0].ToString();
                        clsShpenzimeOperative shpenzimVjeter = col.Where(x => x.ShokId == e.UpdateValues[i].MerrKeyValue<int>()).FirstOrDefault();

                        clsShpenzimeOperative shpenzimRi = e.UpdateValues[i].MerrCustomUpdatedObject(shpenzimVjeter);
                        shpenzimRi.IdModifikuesi = idPerdoruesi;

                        mesazh = shpenzimRi.Modifiko();
                    }

                    //te rregullohet me vone eshte bere per shpejtesi keshtu
                    hfState.Set("modifikuarNrCeshtjesh", true);
                    clsShpenzimeOperativeCeshtje ceshtjet = ModifikoCeshtjet(col[0].KokaId);
                    mesazh = ceshtjet.Ruaj();
                }

                if (mesazh.Status)
                {
                    mySessionObjects.ruajObjectNeSesion(Session, col, "shpenzimeOperative");
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Modifikimi u krye me sukses!:Green");
                }
                else
                {
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, mesazh.PershkrimMesazhi + "!:Red");
                }
            }
            catch (Exception err)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, err.Message + "!:Red");
            }
            e.Handled = true;
        }

   
        protected void gvShpenzimeOperative_CustomUnboundColumnData(object sender,
            ASPxGridViewColumnDataEventArgs e)
        {
            //if (e.Column.FieldName == "TotalParaardhes")
            //{
            //    e.Value = Convert.ToDecimal(e.GetListSourceFieldValue("NgaBuxhetiParaardhes"))
            //        + Convert.ToDecimal(e.GetListSourceFieldValue("NgaTeArdhuratParaardhes"))
            //        ;
            //}
            //else if (e.Column.FieldName == "TotalAktuale")
            //{
            //    e.Value = Convert.ToDecimal(e.GetListSourceFieldValue("NgaBuxhetiAktuale"))
            //        + Convert.ToDecimal(e.GetListSourceFieldValue("NgaTeArdhuratAktuale"))
            //        ;
            //}
            //else if (e.Column.FieldName == "DiferencaKerkeseLimitArdhme")
            //{
            //    e.Value = Convert.ToDecimal(e.GetListSourceFieldValue("KerkesaGjykatesArdhme"))
            //        - Convert.ToDecimal(e.GetListSourceFieldValue("LimitiArdhme"))
            //        ;
            //}
            //else if (e.Column.FieldName == "TotalKerkesaTeArdhuraArdhme")
            //{
            //    e.Value = Convert.ToDecimal(e.GetListSourceFieldValue("DiferencaKerkeseLimitArdhme"))
            //        + Convert.ToDecimal(e.GetListSourceFieldValue("ShpenzimeTePlanifikuarTeArdhuraArdhme"))
            //        ;
            //}
            //else if (e.Column.FieldName == "DiferencaKerkeseLimitArdhmePlus1")
            //{
            //    e.Value = Convert.ToDecimal(e.GetListSourceFieldValue("KerkesaGjykatesArdhmePlus1"))
            //        - Convert.ToDecimal(e.GetListSourceFieldValue("LimitiArdhmePlus1"))
            //        ;
            //}
            //else if (e.Column.FieldName == "DiferencaKerkeseLimitArdhmePlus2")
            //{
            //    e.Value = Convert.ToDecimal(e.GetListSourceFieldValue("KerkesaGjykatesArdhmePlus2"))
            //        - Convert.ToDecimal(e.GetListSourceFieldValue("LimitiArdhmePlus2"))
            //        ;
            //}
        }

        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {
        }

        private void krijoGrupSummary()
        {
            foreach (GridViewColumn col in gvShpenzimeOperative.VisibleColumns)
            {
                if (col.Name == "NjesiaAktuale" || col.Name == "NjesiaArdhme")
                    continue;
                if (col.GetType() == typeof(GridViewDataSpinEditColumn))
                {
                    gvShpenzimeOperative.PercaktoTemplateGroupSummaryFooter("n2", col.Name);
                    gvShpenzimeOperative.ShtoGroupSummary( "n2", DevExpress.Data.SummaryItemType.Sum, col.Name);
                }
            }
        }

        private void krijoTotalSummary()
        {
            foreach (GridViewColumn col in gvShpenzimeOperative.VisibleColumns)
            {
                if (col.Name == "Pershkrimi")
                {
                    col.FooterTemplate = new MyFooterCellTemplate(col.Name, "Totali ");
                    continue;
                }
                if (col.Name == "NjesiaAktuale" || col.Name == "NjesiaArdhme")
                    continue;
                if (col.GetType() == typeof(GridViewDataSpinEditColumn))
                {
                    gvShpenzimeOperative.ShtoTotalSummary("n2", DevExpress.Data.SummaryItemType.Sum, col.Name);
                    GridUtil.PercaktoTemplateTotalSummaryFooter(gvShpenzimeOperative, "n2", col.Name);
                }
            }
        }

        protected void gvShpenzimeOperative_HtmlFooterCellPrepared(object sender, ASPxGridViewTableFooterCellEventArgs e)
        {
        }

        protected void gvShpenzimeOperative_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
        }
    }
}