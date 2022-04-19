using DbCore;
using DbCore.DbAdmin;
using DbCore.DbInventari;
using DbCore.DbKontabiliteti;
using DbCore.DbShare;
using DevExpress.Web;
using DevExpress.Web.Data;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Web.UI;
using System.Web.UI.WebControls;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    public partial class ZbritjeAnalitike : MyPageBase
    {
        private static Logger _logu = LogManager.GetCurrentClassLogger();
        private const string KeyDataSourcePerSession = "gvZbritjeAnalitike";
        private const string KomponenteEmri = "ZbritjeAnalitike.aspx";
        private string guidString;
        private CultureInfo _ci;
        private ResourceManager _rm;
        private int _idPerdoruesi;
        private ResourceManager Rm => _rm ?? (_rm = new ResourceManager("Resources.Strings", Assembly.Load("App_GlobalResources")));
        private CultureInfo Ci => _ci ?? (_ci = mySessionObjects.ktheCultureInfo(Session));

        private int IdPerdoruesi
        {
            get
            {
                if (_idPerdoruesi == 0)
                    _idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);
                return _idPerdoruesi;
            }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            int idGjuha;
            int idNdermarrje;
            int idViti;
            int idNdermarrjeVit;
            int idKonfigambjenti;

            if (!Page.IsPostBack)
            {
                if (!mySessionObjects.isLogedIn(Session))
                {
                    clsFunksione.logout(Session, true, "FaqePaautorizuar");
                }
                if (mySessionObjects.ktheKodNdermarrje(Session) == null)
                {
                    Response.Redirect("Login_Ndermarrje.aspx?id=" + IdPerdoruesi);
                }

                idGjuha = mySessionObjects.ktheGjuhe(Session);
                idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
                idViti = mySessionObjects.ktheIdVitNdermarrje(Session);
                idNdermarrjeVit = mySessionObjects.ktheNdermarrjeVit(Session);
                guidString = Guid.NewGuid().ToString();
                hfState.Set("guidString", guidString);
                KonfiguroVleraFillestare(IdPerdoruesi, idNdermarrje, Rm, Ci, idGjuha);
                EmratELabelave(Rm, Ci);
                MbushHiddenFieldMePerkthime(Rm, Ci);
                AspxWebControlUtils.perkthePopUp(popFshi, Rm.GetString("labelKujdes", Ci), lblMsgbox, Rm.GetString("labelAdministrimiMsgJeniSigurt", Ci), ButtonCancel, Rm.GetString("labelAnullo", Ci));

                idKonfigambjenti = int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString());
                var konf = new clsKonfigurimAmbjenti(idKonfigambjenti, idGjuha);

                hfState.Set("idPerdoruesi", IdPerdoruesi);
                hfState.Set("idGjuha", idGjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idViti", idViti);
                hfState.Set("idNdermarrjeVit", idNdermarrjeVit);
                hfState.Set("Meme", mySessionObjects.merrEshteMemeSesioni(Session));
                hfState.Set("idKonfigambjenti", idKonfigambjenti);
                hfState.Set("EmriFile", "Lista e zbritjeve analitike");
                hfState.Set("konfigFillestar", JsonConvert.SerializeObject(new
                {
                    Kodi = konf.KodKonfigAmbjente,
                    Pershkrimi = konf.PershkrimKonfigAmbjente
                }));
                MbushGridenNgaDb(idNdermarrje);
                KonfiguroGriden(idNdermarrje, idKonfigambjenti, IdPerdoruesi, idGjuha);
                GridUtil.konfiguroGridaPerBatchEditing(gvZbritjeAnalitike, false, false, true, true, 10, false);
                GridUtil.PercaktoVisibleColumnsAndTypesGridSipasKodKonfigurimi(idNdermarrje, "gvZbritjeAnalitike", gvZbritjeAnalitike, konf.KodKonfigAmbjente, "423", idGjuha);
                EmrateButonave(Ci, Rm);
            }
            else
            {
                idGjuha = (int)hfState["idGjuha"];
                idNdermarrje = (int)hfState["idNdermarrje"];
                idViti = (int)hfState["idViti"];
                idNdermarrjeVit = (int)hfState["idNdermarrjeVit"];
                idKonfigambjenti = int.Parse(cmbKonfigurimi.Value.ToString());
                guidString = (string)hfState["guidString"];

                // if (!this.EshteCallbackuIm(gvCmimArtikulli.ID)
                  MbushGrideNgaSession(idNdermarrje);
                KonfiguroGriden(idNdermarrje, idKonfigambjenti, IdPerdoruesi, idGjuha);
            }

            FormatoGriden(idNdermarrje, idKonfigambjenti);
            gvZbritjeAnalitike.PercaktoTitlePanel(Page, MenuInfo, pnlMesazhi, hfState, IdPerdoruesi, idNdermarrje, idViti, idGjuha, idKonfigambjenti, KomponenteEmri, Rm, Ci);
            PercaktoTemplateMenu(ASPxMenu1, idViti, IdPerdoruesi, idNdermarrje);

        }
        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        /// <param name="idViti"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNermarrje"></param>
        private void PercaktoTemplateMenu(ASPxMenu aSPxMenu1, int idViti, int idPerdorues, int idNdermarrje)
        {
            clsToolbarConfig.percaktoTemplateMenu((int)hfState["idGjuha"], idViti, idPerdorues, idNdermarrje, aSPxMenu1, KomponenteEmri, this, MenuInfo, true, true, false, (bool)hfState["Meme"]);
        }


        /// <summary>
        /// mbush kombot me vlerat perkatese
        /// </summary>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idNdermarrje"></param>
        private void KonfiguroVleraFillestare(int idPerdoruesi, int idNdermarrje, ResourceManager rm, CultureInfo ci, int idGjuha)
        {
            AspxWebControlUtils.vendosDateEditMask(dteDtFillimi2, dteDtMbarimi2);

            ConfigureAspxComboBox.mbushComboVlerePerqidje(cmbVlerePerqindje, rm, ci);
            ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 18, rm, ci, idGjuha);
            cmbKonfigurimi.SelectedIndex = 0;
            cmbKonfigurimi.ClientEnabled = false;

            //mbushListeZbritjeAnalitike
        }

        private void MbushGridenNgaDb(int idNdermarrje)
        {
            var col = new colZbritjetAnalitike(idNdermarrje);
            gvZbritjeAnalitike.DataSource = col;
            gvZbritjeAnalitike.DataBind();

            mySessionObjects.ruajObjectNeSesion(Session, col, KeyDataSourcePerSession);
        }

        private void KonfiguroGriden(int idNdermarrje, int idKonfig, int idperdoruesi, int idGjuha)
        {//konfigurohet grida
            KonfigurimComboGride.shtoNiveleZbritjeshSipasNdermarjes(gvZbritjeAnalitike, idNdermarrje, Session, KomponenteEmri, guidString, "IdNivelZbritje");
            KonfigurimComboGride.ShtoKodifikim(gvZbritjeAnalitike, idNdermarrje, Session, KomponenteEmri, guidString, "KodifikimArtikulli1");
            KonfigurimComboGride.ShtoKodifikim(gvZbritjeAnalitike, idNdermarrje, Session, KomponenteEmri, guidString, "KodifikimArtikulli2");
            KonfigurimComboGride.ShtoLlojZbritje(gvZbritjeAnalitike, Session,KomponenteEmri,guidString);
            if (cbCmimBazeZbAnalitike.Checked)
    
           {
                gvZbritjeAnalitike.Columns["CmimBazeZbritjeAnalitike"].Visible = true;
                gvZbritjeAnalitike.Columns["CmimBazeZbritjeAnalitike"].Width = 70;
                GridViewDataTextColumn colCmimBaze = gvZbritjeAnalitike.Columns["CmimBazeZbritjeAnalitike"] as GridViewDataTextColumn;
                colCmimBaze.PropertiesEdit.DisplayFormatString = "0.00";
            }
            else
            {
                gvZbritjeAnalitike.Columns["CmimBazeZbritjeAnalitike"].Visible = false;
                gvZbritjeAnalitike.Columns["CmimBazeZbritjeAnalitike"].Width = 0;
            }


            ShtoKomandButton();      
            GridUtil.AplikoStileBatchEdit(gvZbritjeAnalitike, Color.Yellow, Color.Red, 12);
        }
    

        private void EmratELabelave(ResourceManager rm, CultureInfo ci)
        {
            popupUniversal.HeaderText = rm.GetString("headerPopUpText", ci);
            lblKonfigurimi.Text = rm.GetString("lblModeli", ci);
        }

        private void MbushHiddenFieldMePerkthime(ResourceManager rm, CultureInfo ci)
        {
            hfState.Set("headerPopUpZgjidhNivelinECmimit", rm.GetString("headerPopUpZgjidhNivelinECmimit", ci));

            hfState.Set("cmbCmimeArtikulliVlere", rm.GetString("cmbCmimeArtikulliVlere", ci));
            hfState.Set("cmbCmimeArtikulliPerqidje", rm.GetString("cmbCmimeArtikulliPerqidje", ci));

            hfState.Set("cmbCmimeArtikulliKosto", rm.GetString("cmbCmimeArtikulliKosto", ci));
            hfState.Set("msgCmimeArtikulliSasiteMinimaleDuhenNumerike", rm.GetString("msgCmimeArtikulliSasiteMinimaleDuhenNumerike", ci));
            hfState.Set("msgCmimeArtikulliSasiteMaxDuhenNumerike", rm.GetString("msgCmimeArtikulliSasiteMaxDuhenNumerike", ci));
            hfState.Set("msgCmimeArtikulliCmimetDuhenNumerike", rm.GetString("msgCmimeArtikulliCmimetDuhenNumerike", ci));
            hfState.Set("msgCmimeArtikulliCmimet2DuhenNumerike", rm.GetString("msgCmimeArtikulliCmimet2DuhenNumerike", ci));
            hfState.Set("msgCmimeArtikulliArtikujTeNdryshuarPorTePaSelektuar", rm.GetString("msgCmimeArtikulliArtikujTeNdryshuarPorTePaSelektuar", ci));
            hfState.Set("msgCmimeArtikulliDoniTeVazhdoni", rm.GetString("msgCmimeArtikulliDoniTeVazhdoni", ci));
            hfState.Set("cmbCmimeArtikulliBarazim", rm.GetString("cmbCmimeArtikulliBarazim", ci));
            hfState.Set("lblModeli", rm.GetString("lblModeli", ci));
        }
        /// <summary>
        /// Vendos emrat e butonave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateButonave(CultureInfo cultinf, ResourceManager rm)
        {
            //btnKerko.Text = rm.GetString("ReportToolbarButtonSearch", cultinf);
            btnNdrysho.Text = rm.GetString("btnNdrysho", cultinf);
        }




        private void ShtoKomandButton()
        {// shton kombobox tek grida per monedhen

            if (typeof(GridViewCommandColumn) != gvZbritjeAnalitike.Columns["Check"].GetType())
            {
                gvZbritjeAnalitike.Columns.Remove(gvZbritjeAnalitike.Columns["Check"]);
                var colnew = new GridViewCommandColumn
                {
                    Name = "Check",
                    Visible = true,
                    Caption = "Check"
                };
                colnew.ShowSelectCheckbox = true;
                colnew.SelectAllCheckboxMode = GridViewSelectAllCheckBoxMode.AllPages;
                colnew.Width = Unit.Pixel(30);
                gvZbritjeAnalitike.Columns.Add(colnew);
                gvZbritjeAnalitike.Columns["Check"].VisibleIndex = 0;
            }
            gvZbritjeAnalitike.Columns["Check"].VisibleIndex = 0;
        }

        private void FormatoGriden(int idNdermarrje, int idKonfig)
        {
            var monedheNdermarrje = clsMonedha.ktheIdMonedhenENdermarrjes(idNdermarrje);
            var formatetPerMonedhat = MerrSipasIdKonfig(idKonfig);

            var formatNumri = MerrFormatDefault();
            var trupFormati = formatetPerMonedhat.KonfigTrupi.Find(x => x.IdMonedha == monedheNdermarrje) ?? MerrFormatDefault();

            GridUtil.VendosFormatNumriPerFushatNumerike(gvZbritjeAnalitike, trupFormati.ShifraPasPresjesSasia, "SasiMin", "SasiMax");
            GridUtil.VendosFormatNumriPerFushatNumerike(gvZbritjeAnalitike, trupFormati.ShifraPasPresjesVlefta, "VleftaMin", "VleftaMax");
            GridUtil.VendosFormatNumriPerFushatNumerike(gvZbritjeAnalitike, trupFormati.ShifraPasPresjesZbritja, "Zbritja", "Zbritja2");
        }

        private clsFormatiKonfig MerrSipasIdKonfig(int idKonfig)
        {
            var formatNrPerKonfig = new clsFormatiKonfig();
            formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(idKonfig);
            return formatNrPerKonfig;
        }

        private clsFormatKonfigTrup MerrFormatDefault()
        {
            return new clsFormatKonfigTrup(clsFormatKonfigTrup.defaultFormatSasia, clsFormatKonfigTrup.defaultFormatCmimi, clsFormatKonfigTrup.defaultFormatVlefta, clsFormatKonfigTrup.defaultFormatZbritja, clsFormatKonfigTrup.defaultFormatStringSasia, clsFormatKonfigTrup.defaultFormatStringCmimi, clsFormatKonfigTrup.defaultFormatStringVlefta, clsFormatKonfigTrup.defaultFormatStringZbritja);
        }

        public colZbritjetAnalitike MbushGrideNgaSession(int idNdermarrje)
        {
           
                var col = (mySessionObjects.merrObjectNgaSesioni(Session, KeyDataSourcePerSession) as colZbritjetAnalitike) ?? new colZbritjetAnalitike(idNdermarrje);
                gvZbritjeAnalitike.DataSource = col;
                gvZbritjeAnalitike.DataBind();
                return col;
            
        }

        protected void gvZbritjeAnalitike_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            GjejTeSelektuarDheTeNdryshuar(MbushGrideNgaSession((int)hfState["idNdermarrje"]));
        }
        private void GjejTeSelektuarDheTeNdryshuar(colZbritjetAnalitike cmimet)
        {

            //nese ka problem performance te gjejme nje menyre tjeter per te bere kete pune
            var teNdryshuar = cmimet.Where(x => x.Check).Select(x => new { x.IdZbritjeAnalitike, x.KodArtikulli });
            var teSelektuar = gvZbritjeAnalitike.GetSelectedFieldValues("IdZbritjeAnalitike");
            var teNdryshuarPorTePaSelektuar = teNdryshuar.Where(x => !teSelektuar.Contains(x.IdZbritjeAnalitike));
            gvZbritjeAnalitike.JSProperties["cpTeSelektuar"] = JsonConvert.SerializeObject(teSelektuar);
            gvZbritjeAnalitike.JSProperties["cpTeNdryshuar"] = JsonConvert.SerializeObject(teNdryshuar);
            gvZbritjeAnalitike.JSProperties["cpTeNdryshuarJoTeSelektuar"] = JsonConvert.SerializeObject(teNdryshuarPorTePaSelektuar);

        }

        protected void gvZbritjeAnalitike_DataBound(object sender, EventArgs e)
        {
            gvZbritjeAnalitike.SettingsBehavior.AllowSelectByRowClick = true;
            gvZbritjeAnalitike.KeyFieldName = "IdZbritjeAnalitike";
        }

        protected void gvZbritjeAnalitike_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            var idNdermarrje = (int)hfState.Get("idNdermarrje");
            if (e.Parameters.Contains("Pastro"))
            {
                gvZbritjeAnalitike.Selection.UnselectAll();
                ResetDataSource(idNdermarrje);
                return;
            }

            if (e.Parameters.Contains("ndryshoZbritjet"))
            {
                decimal vlera;
                if (!String.IsNullOrWhiteSpace(txtVlera.Text))
                    vlera = decimal.Parse(txtVlera.Text);
                else
                    vlera = -1;

                var perqindje = (string)cmbVlerePerqindje.Text;
                var datefillimi = dteDtFillimi2.Date;
                var datembarimi = dteDtMbarimi2.Date;
                AplikoNdryshiminECmimevePerRreshtatESelektuar(vlera, perqindje, datefillimi, datembarimi);
                return;
            }

            if (e.Parameters.Contains("ShtoCmBazeNeGride"))
            {
                KonfiguroGriden(idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()), IdPerdoruesi,IdGjuha);

            }
                if (e.Parameters.Contains("Ruaj"))
            {
                bool kontrolluar = bool.Parse(e.Parameters.Split(';')[1]);
                var mesazhi = RuajNdryshimet(kontrolluar);
                if (!mesazhi.Status && !kontrolluar)//kontrolluar eshte true kur ne popup kemi shtypur OK, ne kete rast nuk duhet te dali perseri
                {
                    gvZbritjeAnalitike.JSProperties["cpShowPopUp"] = mesazhi.PershkrimMesazhi;
                }
                gvZbritjeAnalitike.ShtoMesazhNeGride(mesazhi);
            }
        }

        private void ResetDataSource(int idNdermarrje)
        {
            var col = new colZbritjetAnalitike(idNdermarrje);
            mySessionObjects.ruajObjectNeSesion(Session, col, KeyDataSourcePerSession);
        }

        private void AplikoNdryshiminECmimevePerRreshtatESelektuar(decimal vlera, string perqindje, DateTime datefillimi, DateTime datembarimi)
        {
            var mesazhi = new clsMesazh(true, "Ndryshimi u aplikua me sukses!");
            try
            {

                var cmbCmimeArtikulliVlere = (string)hfState.Get("cmbCmimeArtikulliVlere");
                var cmbCmimeArtikulliPerqidje = (string)hfState.Get("cmbCmimeArtikulliPerqidje");
                var cmbCmimeArtikulliKosto = (string)hfState.Get("cmbCmimeArtikulliKosto");

                var teSelektuar = gvZbritjeAnalitike.GetSelectedFieldValues("IdZbritjeAnalitike");

                if (teSelektuar == null)
                    throw new MyException(_logu, "Nuk ka rreshta te selektuar!");
                //merr nga sessioni,modifikoje dhe ruaje prape aty
                var col = mySessionObjects.merrObjectNgaSesioni(Session, KeyDataSourcePerSession) as colZbritjetAnalitike;
                foreach (var rreshti in teSelektuar)
                {
                    clsZbritjeAnalitike zbritjeAnalitike = col.FirstOrDefault(x => x.IdZbritjeAnalitike == (int)rreshti);

                    zbritjeAnalitike.Check = true;//modifikuar
                    zbritjeAnalitike.IdPerdoruesi = IdPerdoruesi;
                    zbritjeAnalitike.LlojZbritje = Convert.ToInt32(perqindje == cmbCmimeArtikulliVlere);
                    if (vlera != -1)
                        zbritjeAnalitike.Zbritja = vlera;

                    if (zbritjeAnalitike.LlojZbritje == 0)
                    {
                        zbritjeAnalitike.Zbritja2 = zbritjeAnalitike.Zbritja;

                    }
                    else if (zbritjeAnalitike.LlojZbritje == 1)
                    {
                        zbritjeAnalitike.Zbritja2 = zbritjeAnalitike.Zbritja * zbritjeAnalitike.Koeficent;
                    }



                    if (datefillimi != DateTime.MinValue)
                        zbritjeAnalitike.DateFillimi = datefillimi;
                    if (datembarimi != DateTime.MinValue)
                        zbritjeAnalitike.DateMbarimi = datembarimi;



                }

                mySessionObjects.ruajObjectNeSesion(Session, col, KeyDataSourcePerSession);
            }
            catch (Exception ex)
            {
                mesazhi.Status = false;
                mesazhi.PershkrimMesazhi = $"Ndodhi nje gabim ne aplikimin e ndryshimit te zbritjeve! {ex.Message}";
                _logu.Error(ex.Message);
            }
            gvZbritjeAnalitike.ShtoMesazhNeGride(mesazhi);
        }

        private clsMesazh RuajNdryshimet(bool kontrolluar)
        {
            clsMesazh mesazhi = new clsMesazh(false);
            try
            {
                var col = mySessionObjects.merrObjectNgaSesioni(Session, KeyDataSourcePerSession) as colZbritjetAnalitike;
                if (col == null)
                    throw new MyException(_logu, "collectioni me te zbritjet eshte null,ruajta nuk mund te kryhet!!");
                var zbritjetAnalitike = col.Where(x => x.Check).ToList();

                if (zbritjetAnalitike.FirstOrDefault() == null)
                {
                    mesazhi.PershkrimMesazhi = "Nuk ka asnje zbritje te ndryshuar!";
                    return mesazhi;
                }
                if (!kontrolluar)
                {
                    var teSelektuar = gvZbritjeAnalitike.GetSelectedFieldValues("IdZbritjeAnalitike");
                    string[] teNdryshuarPorTePaSelektuar = zbritjetAnalitike.Where(x => !teSelektuar.Contains(x.IdZbritjeAnalitike)).Select(y => y.KodArtikulli).ToArray();
                    mesazhi = clsFunksione.KontrolloTeNdryshuar(teNdryshuarPorTePaSelektuar);
                    if (!mesazhi.Status)
                        return mesazhi;
                }

                mesazhi = colZbritjetAnalitike.RuajRreshtaTeModifikuar(zbritjetAnalitike);
                if (mesazhi.Status)
                {
                    zbritjetAnalitike.Select(x => { x.Check = false; return x; }).ToList();// u ruajt, keshtu qe s'duhet te dali si i modifikuar me ngjyre te verdhe
                    mySessionObjects.ruajObjectNeSesion(Session, col, KeyDataSourcePerSession);
                }

            }
            catch (Exception ex)
            {
                _logu.Error(ex.Message);
            }
            return mesazhi;
        }






        protected void gvZbritjeAnalitike_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpNoRows"] = gvZbritjeAnalitike.VisibleRowCount;
            e.Properties["cpNoPage"] = gvZbritjeAnalitike.PageIndex;
        }


        protected void gvZbritjeAnalitike_OnHtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType == GridViewRowType.Data)
            {

                var iModifikuar = (bool)e.GetValue("Check");
                if (iModifikuar)
                    e.Row.BackColor = Color.Yellow;
            }
        }

        protected void gvZbritjeAnalitike_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
        {
            var idNdermarrje = (int)hfState.Get("idNdermarrje");
            var grida = sender as ASPxGridView;
            var mesazhi = new clsMesazh(true);
            try
            {
                var col = (mySessionObjects.merrObjectNgaSesioni(Session, KeyDataSourcePerSession) as colZbritjetAnalitike) ?? new colZbritjetAnalitike(idNdermarrje);
                if (e.UpdateValues.Count > 0)
                {
                    foreach (var rreshtiModifikuar in e.UpdateValues)
                    {
                        var oldZbritje = col.FirstOrDefault(x => x.IdZbritjeAnalitike == rreshtiModifikuar.MerrKeyValue<int>());
                        if (oldZbritje == null)
                            throw new MyException(_logu, "col.FirstOrDefault(x => x.IdZbritjeAnalitike == rreshtiGrides.MerrKeyValue<int>()); returns null");

                        var newZbritje = rreshtiModifikuar.MerrCustomUpdatedObject(oldZbritje);
                        newZbritje.IdPerdoruesi = IdPerdoruesi;
                        newZbritje.Check = true;
                    }
                    mySessionObjects.ruajObjectNeSesion(Session, col, KeyDataSourcePerSession);
                }


            }
            catch (Exception err)
            {
                _logu.Error(err.Message);
                mesazhi.Status = false;
                mesazhi.PershkrimMesazhi = "Ndodhi nje gabim ne modifikim!";
                grida.ShtoMesazhNeGride(mesazhi);
            }


            e.Handled = true;
        }




    }
}
