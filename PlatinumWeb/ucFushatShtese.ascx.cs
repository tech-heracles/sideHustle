using DbCore;
using DbCore.DbAdmin;
using DbCore.DbShare;
using DevExpress.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PlatinumWeb.Templates;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PlatinumWeb
{
    public partial class ucFushatShtese : UserControl
    {
        private CultureInfo cultinf => mySessionObjects.ktheCultureInfo(Session);
        private ResourceManager rm => new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
        private int IdGjuha => mySessionObjects.ktheGjuhe(Session);
        /// <summary>
        /// konstante e cila sherben per te identifikuar vlerat e fushave shtese te pa modifikuara
        /// </summary>
        private const string VleraTePaModifikuaraSuffix = "VleraFushaShteseOrig";

        public int IdKonfigurimi
        {
            get { return Convert.ToInt32(HfFushaShtese.Get("idKonfigurimi")); }
            set { HfFushaShtese.Set("idKonfigurimi", value); }
        }

        public int IdKomponente
        {
            get { return Convert.ToInt32(HfFushaShtese.Get("idKomponente")); }
            set { HfFushaShtese.Set("idKomponente", value); }
        }

        /// <summary>
        /// tregon nese ky entitet ka fusha shtese apo jo
        /// </summary>
        public bool KaFushaShtese
        {
            get
            {
                return cmbModeli.Items.Count > 0;
            }
        }

        /// <summary>
        /// kthen/vendos llojin e fushes entitetit
        /// </summary>
        public string LlojFushe
        {
            get { return HfFushaShtese.Contains("llojFushe") ? HfFushaShtese.Get("llojFushe").ToString() : null; }
            set { HfFushaShtese.Set("llojFushe", value); }
        }

        public string EmerKomponente
        {
            get { return HfFushaShtese.Get("emerkomp").ToString(); }
            set { HfFushaShtese.Set("emerkomp", value); }
        }

        /// <summary>
        /// kthen/vendos ID e entitetit. psh per nje klient id 5
        /// </summary>
        public int IDEntiteti
        {
            get { return Convert.ToInt32(HfFushaShtese.Get("idRreshti")); }
            set { HfFushaShtese.Set("idRreshti", value); }
        }

        /// <summary>
        /// kthen fushat shtese me vlerat perkatese
        /// </summary>
        public string fushat
        {
            get { return Convert.ToString(HfFushaShtese.Get("fushat")); }
        }

        public int IdPerdoruesi
        {
            get { return Convert.ToInt32(HfFushaShtese.Get("idPerdoruesi")); }
            set { HfFushaShtese.Set("idPerdoruesi", value); }
        }

        private string GetSessionKeyVleraOrigjinale()
        {
            return $"{EmerKomponente}_{VleraTePaModifikuaraSuffix}";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// vendosen vlerat fillestare
        /// </summary>
        /// <param name="llojFushe"></param>
        public void KonfiguroVleraFillestare(int idNdermarrje, int idPerdoruesi, int idGjuha, string emerKomponente, string llojFushe, int idRreshti, int idkonfigurimi)
        {
            LlojFushe = llojFushe;
            IDEntiteti = idRreshti;
            IdKonfigurimi = idkonfigurimi;
            EmerKomponente = emerKomponente;
            IdPerdoruesi = idPerdoruesi;
            IdKomponente = clsKomponente.MerrIdKomponenteSipasEmrit(emerKomponente);//TODO NEDJANI zevenedeso emrin e komponentes me idkomponente ne parametra dhe perdorim
            mbushComboModeli(idPerdoruesi, idNdermarrje);
            var idModeli = Convert.ToInt32(cmbModeli.Value);
            var vleratEFundit = NgarkoNeSessionVleratSipasDates(idModeli, new DateTime(2000, 1, 1));
            mbushListeFushashShtese(idModeli, idRreshti, vleratEFundit);
            KonfiguroGrideFushash(idNdermarrje, idGjuha);
        }

        public void mbushComboModeli(int idPerdoruesi, int idNdermarrje)
        {//mbush kombon e modelit me te dhena nga databasa
            var llojmod = new clsLlojModeliFushaShtese(LlojFushe);
            var idlloj = llojmod.IdLlojModeliFushaShtese;
            var colModeli = new colModeletFushaShtese(idlloj, idNdermarrje, idPerdoruesi);
            cmbModeli.DataSource = colModeli;
            cmbModeli.TextField = "KodiModeliFushashtese";
            cmbModeli.ValueField = "IdModeliFushaShtese";
            cmbModeli.DataBind();
            VendosVlereDefaultModeliFushaShtese();
        }

        private void VendosVlereDefaultModeliFushaShtese()
        {
            var selectedItem = MerrVlereDefaultModeliSipasKonfigurimit();
            if (selectedItem != null)
            {
                cmbModeli.SelectedItem = selectedItem;
                HfFushaShtese.Set("selectedIndex", cmbModeli.Value);
                HfFushaShtese.Set("modeliDefault", cmbModeli.Value);
            }
            gvFushat.JSProperties["cpMerrData"] = true;
        }

        private ListEditItem MerrVlereDefaultModeliSipasKonfigurimit()
        {
            var vlereDefaultModelFusheShtese = clsAtributeTrupi.merrVleredefaultSipasKontrollitDheKonfigurimit(IdKonfigurimi, "cmbModeli", IdKomponente);
            if (!String.IsNullOrEmpty(vlereDefaultModelFusheShtese))
                return cmbModeli.Items.FindByTextWithTrim(vlereDefaultModelFusheShtese);
            return cmbModeli.Items.Count > 0 ? cmbModeli.Items[0] : null;
        }

        public void mbushListeFushashShtese(int idModeli, int idEntiteti, colVleraFushaShtese vleratSipasDates)
        {//mbushet grida me te dhena

            colFushatShtese fushatEKetijModeli;

            //    var dataEzgjedhur = cmbDtNdryshimi.Text;
            var colFushatTeGjitha = mySessionObjects.merrFushaShteseNGaSessioni(Session, EmerKomponente);
            if (!colFushatTeGjitha.TryGetValue(idModeli, out fushatEKetijModeli))
            {
                fushatEKetijModeli = new colFushatShtese(idModeli);
                colFushatTeGjitha[idModeli] = fushatEKetijModeli;
            }
            var colVlerat = colVleraFushaShtese.MerrVleratSipasFushave(idModeli, vleratSipasDates, IdGjuha, fushatEKetijModeli, IdPerdoruesi);

            mySessionObjects.ruajFushaShteseNeSession(Session, EmerKomponente, colFushatTeGjitha);
            mySessionObjects.RuajFushaShteseNeSessionGrida(Session, EmerKomponente, fushatEKetijModeli);
            mySessionObjects.ruajVleraNeSesionGrida(Session, EmerKomponente, colVlerat);
            gvFushat.DataSource = colVlerat;
            gvFushat.DataBind();
        }



        /// <summary>
        /// kur grida ben callback. kur kemi rast shtimi apo modifikimi marrim vlerat per kete artikull
        /// kur ndryshohet modeli ndryshojme griden sipas modelit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvFushat_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            //e.Parameters
            /* 0-indexi
             * 1-veprimi
             * 2-indexiModelit para ndryshimit te tij
             * 3-dtNdryshimi nese ka
             */
            var rowValueItems = e.Parameters.Split(';');
            if (rowValueItems.Length <= 0) return;

            var idModeli = Convert.ToInt32(cmbModeli.Value);

            var indexi = -1;
            var veprimi = "";
            var idModeliMeparshem = -1;
            colVleraFushaShtese colVleratSipasDates = null;
            var idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);//Convert.ToInt32(rowValueItems[1]);
            var idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session); //Convert.ToInt32(rowValueItems[2]);

            if (rowValueItems.Length == 1 && rowValueItems[0] == "shtim")
            {
                veprimi = rowValueItems[0];
            }
            else if (rowValueItems.Length == 2 && rowValueItems[0] != "")
            {
                indexi = Convert.ToInt32(rowValueItems[0]);
                veprimi = rowValueItems[1];
            }
            else if (rowValueItems.Length >= 3)
            {
                indexi = Convert.ToInt32(rowValueItems[0]);
                veprimi = rowValueItems[1];
            }
            if (string.IsNullOrWhiteSpace(veprimi))
            {

                DbCore.IMBUtils.Logging.ImbLogger.Error($"Callback qe nuk duhet !! verpimi i pa percaktuar! '{veprimi}'");
                return;
            }
            switch (veprimi)
            {
                case "mod":
                case "shtim":
                    PastroFushatShteseNgaSessioni();
                    var modeliDefault = MerrVlereDefaultModeliSipasKonfigurimit();
                    if (modeliDefault != null)
                    {
                        idModeli = Convert.ToInt32(modeliDefault.Value);
                        gvFushat.JSProperties["cpModelDefault"] = idModeli;
                        cmbModeli.Value = idModeli;
                    }

                    var vlerat = colVleraFushaShtese.MerrDataAktivizmi(indexi, idModeli);
                    var data = new DateTime(2000, 1, 1);
                    if (vlerat.Count != 0)
                    {
                        data = vlerat[0];
                        dtAktivizimi.Date = data;
                        //dtAktivizimi.Text = data.ToString();
                    }
                    IDEntiteti = indexi;//ruajm id e rreshtit te entitetit qe po modifikohet
                    colVleratSipasDates = NgarkoNeSessionVleratSipasDates(idModeli, veprimi == "shtim" ? new DateTime(2000, 1, 1) : data);//dtAktivizimi.Date);

                    gvFushat.JSProperties["cpMerrData"] = true;//force to load data from api
                    gvFushat.JSProperties["cpIdEntieti"] = indexi;
                    gvFushat.JSProperties["cpModeliMeparshem"] = cmbModeli.Value;
                    break;
                case "ruaj":
                    idModeliMeparshem = Convert.ToInt32(rowValueItems[2]);
                    // ne rastin kur ndryshohet modeli ruajme te dhenat e modelit te meparshem
                    //dhe shfaqim te dhenat e modelit te zgjedhur
                    MerrFushatTePerditesuara(idModeliMeparshem, dtAktivizimi.Date);//ruaj vlerat e  modelit te vjeter
                    break;
                case "ndryshoiModeli":
                    idModeliMeparshem = Convert.ToInt32(rowValueItems[2]);
                    var dtModelitTeMeparshem = Convert.ToDateTime(rowValueItems[3]);
                    // ne rastin kur ndryshohet modeli ruajme te dhenat e modelit te meparshem
                    //dhe shfaqim te dhenat e modelit te zgjedhur

                    MerrFushatTePerditesuara(idModeliMeparshem, dtModelitTeMeparshem);//ruaj vlerat e  modelit te vjeter
                    colVleratSipasDates = NgarkoNeSessionVleratSipasDates(idModeli, dtAktivizimi.Date);
                    break;
                case "ndryshoiData":
                    DateTime dtMerparshme;
                    if (!DateTime.TryParse(rowValueItems[2], out dtMerparshme))
                        dtMerparshme = dtAktivizimi.Date;
                    MerrFushatTePerditesuara(Convert.ToInt32(cmbModeli.Value), dtMerparshme);
                    colVleratSipasDates = NgarkoNeSessionVleratSipasDates(idModeli, dtAktivizimi.Date);
                    break;
            }

            mbushListeFushashShtese(idModeli, IDEntiteti, colVleratSipasDates);
            percaktoTemplateFushash();
        }

        /// <summary>
        /// merr nga sesioni ose nga db nese nuk gjenden ne sesion vlerat sipas dates dhe modelit
        /// </summary>
        /// <param name="idModeli"></param>
        /// <param name="dtAkt"></param>
        /// <returns></returns>
        private colVleraFushaShtese NgarkoNeSessionVleratSipasDates(int idModeli, DateTime? dtAkt)
        {
            if (IDEntiteti == 0) return new colVleraFushaShtese();
            Dictionary<int, Dictionary<string, colVleraFushaShtese>> colVleratGjitha = null;
            Dictionary<int, Dictionary<string, colVleraFushaShtese>> colVleratGjithaTePaModifikuara = null;
            Dictionary<string, colVleraFushaShtese> vleratEKetijModeli = null;

            colVleratGjitha = mySessionObjects.merrVleraNgaSesioni(Session, EmerKomponente);//marim vlerat ekzistuese
            colVleratGjithaTePaModifikuara = mySessionObjects.merrVleraNgaSesioni(Session, GetSessionKeyVleraOrigjinale());

            if (!colVleratGjitha.TryGetValue(idModeli, out vleratEKetijModeli))
            {
                vleratEKetijModeli = new Dictionary<string, colVleraFushaShtese>();
            }
            if (!colVleratGjithaTePaModifikuara.ContainsKey(idModeli))
            {
                colVleratGjithaTePaModifikuara[idModeli] = new Dictionary<string, colVleraFushaShtese>();
            }
            var dataMeVleratPerkatese = MerrVleratSipasDates(idModeli, dtAkt, vleratEKetijModeli);

            vleratEKetijModeli[dataMeVleratPerkatese.Item1] = dataMeVleratPerkatese.Item2;
            colVleratGjithaTePaModifikuara[idModeli][dataMeVleratPerkatese.Item1] = dataMeVleratPerkatese.Item2.Clone();
            colVleratGjitha[idModeli] = vleratEKetijModeli;

            mySessionObjects.ruajVleraNeSesion(Session, EmerKomponente, colVleratGjitha);
            mySessionObjects.ruajVleraNeSesion(Session, GetSessionKeyVleraOrigjinale(), colVleratGjithaTePaModifikuara);
            return dataMeVleratPerkatese.Item2;
        }

        /// <summary>
        /// kthen vlerat per e fushave shtese per modelin dhe daten e kaluar si param
        /// </summary>
        /// <param name="idModeli"></param>
        /// <param name="dtAkt"></param>
        /// <param name="vleratEKetijModeli"></param>
        /// <returns></returns>
        private Tuple<string, colVleraFushaShtese> MerrVleratSipasDates(int idModeli, DateTime? dtAkt, Dictionary<string, colVleraFushaShtese> vleratEKetijModeli)
        {
            var dataEzgjedhur = dtAkt?.ToString("dd/MM/yyyy") ?? "";
            colVleraFushaShtese vleratEKesajDate;
            if (!string.IsNullOrEmpty(dataEzgjedhur))
            {
                if (!vleratEKetijModeli.TryGetValue(dataEzgjedhur, out vleratEKesajDate))
                    vleratEKesajDate = new colVleraFushaShtese(IDEntiteti, idModeli, dtAkt);
            }
            else
            {
                //ngarkojme vlerat me te fundit te ketij modeli
                vleratEKesajDate = new colVleraFushaShtese(IDEntiteti, idModeli, null);
                if (vleratEKesajDate.Count > 0) dataEzgjedhur = vleratEKesajDate[0].DtAktivizimi.ToString("dd/MM/yyyy");
            }
            var vleraPerFushaTeReja = colVleraFushaShtese.MerrVleratSipasFushave(idModeli, vleratEKesajDate, 0, MerrFushatSipasModelit(idModeli), IdPerdoruesi).Where(x => x.IdVleraFushaShtese < 0).ToList();
            if (vleraPerFushaTeReja.Count > 0)
                vleratEKesajDate.AddRange(vleraPerFushaTeReja);
            return new Tuple<string, colVleraFushaShtese>(dataEzgjedhur, vleratEKesajDate);
        }

        private colFushatShtese MerrFushatSipasModelit(int idModeli)
        {
            colFushatShtese fushatEKetijModeli;
            var colFushatTeGjitha = mySessionObjects.merrFushaShteseNGaSessioni(Session, EmerKomponente);
            if (!colFushatTeGjitha.TryGetValue(idModeli, out fushatEKetijModeli))
            {
                fushatEKetijModeli = new colFushatShtese(idModeli);
            }
            return fushatEKetijModeli;
        }

        /// <summary>
        /// percakton templatet e fushave sipas tipit
        /// </summary>
        public void percaktoTemplateFushash()
        {//percaktohen templatet per fushat e grides
            var colVlerat = mySessionObjects.merrVleraNgaSesioniGrida(Session, EmerKomponente);
            var col1 = gvFushat.Columns["PershkrimiFushaShtese"] as GridViewDataTextColumn;
            col1.DataItemTemplate = new MyLabelTemplate();
            var col2 = gvFushat.Columns["VleraFushaShtese"] as GridViewDataTextColumn;

            if (colVlerat.Count > 0)
            {
                if (colVlerat[0].TipiFushaShtese == 0)//percaktohet template ne varesi te tipit
                    col2.DataItemTemplate = new MyLabelTemplate();
                else if (colVlerat[0].TipiFushaShtese == 1)
                {
                    col2.DataItemTemplate = new MyTextTemplate();
                }
                else if (colVlerat[0].TipiFushaShtese == 2)
                    col2.DataItemTemplate = new MyIntSpinTemplate(false);
                else if (colVlerat[0].TipiFushaShtese == 3)
                    col2.DataItemTemplate = new MyDoubleSpinTemplate(false, 3, "0"); // "0.00");
                else if (colVlerat[0].TipiFushaShtese == 4)
                    col2.DataItemTemplate = new MyCalendarTemplate();
                else if (colVlerat[0].TipiFushaShtese == 5)
                    col2.DataItemTemplate = new MyCheckTemplate(false, false);
                else if (colVlerat[0].TipiFushaShtese == 6)
                    col2.DataItemTemplate = new MyComboTemplate(DropDownStyle.DropDownList);
            }
        }

        protected void gvFushat_DataBound(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// vendos funksionet javascritp per fushat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvFushat_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            //krijon rreshtat sipas modelit
            if (e.RowType != GridViewRowType.Data) return;
            if (((ASPxGridView)sender).Columns.Count == 0) return;
            var fushatShtese = mySessionObjects.merrFushaShteseNgaSesioniGrida(Session, EmerKomponente);
            if (fushatShtese.Count == 0) return;

            var idModeli = Convert.ToInt32(cmbModeli.Value);
            var colVlerat = mySessionObjects.merrVleraNgaSesioniGrida(Session, EmerKomponente);
            if (colVlerat.Count <= e.VisibleIndex) return;
            var vleraFushesShtese = colVlerat[e.VisibleIndex];//rreshti i radhes

            var fushaShtese = fushatShtese.Find(x => x.IdFushaShtese == vleraFushesShtese.IdFushaShtese);
            var col2 = ((ASPxGridView)sender).Columns["VleraFushaShtese"] as GridViewDataTextColumn;
            var col3 = ((ASPxGridView)sender).Columns["Shenime"] as GridViewDataTextColumn;
            e.Row.Visible = fushaShtese.Shfaq;
            var rows = 1;
            var columns = 10;
            if (colVlerat.Count > e.VisibleIndex)
            {
                var memo1 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col3, "txtBox") as ASPxMemo;
                if (memo1 != null)
                {
                    memo1.Text = vleraFushesShtese.Shenime;
                    if (memo1.Text != "") { memo1.Rows = 3; }
                }

                if (vleraFushesShtese.TipiFushaShtese == 0)
                {
                    var txt1 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col2, "lbl") as ASPxLabel;
                    if (txt1 != null)
                    {
                        txt1.ClientInstanceName = "txtVlera" + e.VisibleIndex;
                        txt1.ReadOnly = true;
                    }
                }
                if (vleraFushesShtese.TipiFushaShtese == 1 || vleraFushesShtese.TipiFushaShtese == 4)
                {
                    var txt1 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col2, "txtBox") as ASPxTextBox;
                    if (txt1 != null)
                    {
                        txt1.ClientInstanceName = "txtVlera" + e.VisibleIndex;
                        txt1.ClientSideEvents.TextChanged = "function(s,e){ShtoStringOrDate(txtVlera" + e.VisibleIndex + "," + e.VisibleIndex + ");}";
                        txt1.Text = vleraFushesShtese.MerrVlereDefaultOseEkzistuesen(txt1.Text, fushaShtese.VlereDefault);
                        aplikoValidationSettings(txt1, fushaShtese);
                    }
                }
                if (vleraFushesShtese.TipiFushaShtese == 2 || vleraFushesShtese.TipiFushaShtese == 3)
                {
                    var txt1 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col2, "txtBox") as ASPxSpinEdit;
                    if (txt1 != null)
                    {
                        txt1.ClientInstanceName = "txtVlera" + e.VisibleIndex;
                        txt1.ClientSideEvents.TextChanged = "function(s,e){ShtoIntOrDouble(txtVlera" + e.VisibleIndex + "," + e.VisibleIndex + ");}";
                        txt1.Text = vleraFushesShtese.MerrVlereDefaultOseEkzistuesen(txt1.Text, fushaShtese.VlereDefault);
                        aplikoValidationSettings(txt1, fushaShtese);
                    }
                }
                if (vleraFushesShtese.TipiFushaShtese == 4)
                {
                    var txt1 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col2, "cal") as ASPxDateEdit;
                    if (txt1 != null)
                    {
                        //  txt1.Value = txt1.Text;
                        txt1.ClientInstanceName = "txtVlera" + e.VisibleIndex;
                        txt1.ClientSideEvents.ValueChanged = "function(s,e){ShtoStringOrDate(txtVlera" + e.VisibleIndex + "," + e.VisibleIndex + ");}";
                        txt1.Text = vleraFushesShtese.MerrVlereDefaultOseEkzistuesen(txt1.Text, fushaShtese.VlereDefault);
                        aplikoValidationSettings(txt1, fushaShtese);
                    }
                }
                if (vleraFushesShtese.TipiFushaShtese == 5)
                {
                    var txt1 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col2, "cb") as ASPxCheckBox;

                    var vl = (colVleraFushaShtese)gvFushat.DataSource;//to check
                    if (txt1 != null)
                    {
                        if (vl != null && vl[e.VisibleIndex].VleraFushaShtese == "true") txt1.Checked = true;

                        txt1.ClientInstanceName = "txtVlera" + e.VisibleIndex;
                        txt1.ClientSideEvents.CheckedChanged = "function(s,e){ShtoCheck(txtVlera" + e.VisibleIndex + "," + e.VisibleIndex + ");}";
                        aplikoValidationSettings(txt1, fushaShtese);
                    }

                }
                if (colVlerat[e.VisibleIndex].TipiFushaShtese == 6)
                {
                    var txt1 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex, col2, "cmbBox") as ASPxComboBox;

                    if (txt1 != null)
                    {
                        var colFushatShtese = new colFushatShtese();
                        colFushatShtese.MbushFushatShteseSipasAtit(vleraFushesShtese.IdFushaShtese);
                        if (IdGjuha == 1) colFushatShtese.ForEach(x => x.PershkrimiFushaShtese = x.PershkrimiEng);
                        colFushatShtese.RemoveAll(x => !x.Shfaq);//heq ato qe kane opsionin shfaq false
                        txt1.DataSource = colFushatShtese;
                        txt1.TextField = "PershkrimiFushaShtese";
                        txt1.ValueField = "PershkrimiFushaShtese";
                        txt1.DataBind();
                        txt1.ClientInstanceName = "txtVlera" + e.VisibleIndex;
                        txt1.ClientSideEvents.SelectedIndexChanged = "function(s,e){ShtoList(txtVlera" + e.VisibleIndex + "," + e.VisibleIndex + ");}";

                        txt1.Text = vleraFushesShtese.MerrVlereDefaultOseEkzistuesen(txt1.Text, fushaShtese.VlereDefault);
                        aplikoValidationSettings(txt1, fushaShtese);
                    }
                }
            }

            if (e.VisibleIndex + 1 >= colVlerat.Count) return;
            col3.DataItemTemplate = new MyReadOnlyMemoTemplate(rows, columns);
            var rreshtiTjeter = colVlerat[e.VisibleIndex + 1];

            if (rreshtiTjeter.TipiFushaShtese == 0)//vendoset modeli i rreshtit tjeter
                col2.DataItemTemplate = new MyLabelTemplate();
            else if (rreshtiTjeter.TipiFushaShtese == 1)
                col2.DataItemTemplate = new MyTextTemplate();
            else if (rreshtiTjeter.TipiFushaShtese == 2)
                col2.DataItemTemplate = new MyIntSpinTemplate(false);
            else if (rreshtiTjeter.TipiFushaShtese == 3)
                col2.DataItemTemplate = new MyDoubleSpinTemplate(false, 3, "0");
            else if (rreshtiTjeter.TipiFushaShtese == 4)
                col2.DataItemTemplate = new MyCalendarTemplate();
            else if (rreshtiTjeter.TipiFushaShtese == 5)
                col2.DataItemTemplate = new MyCheckTemplate(false, false);
            else if (rreshtiTjeter.TipiFushaShtese == 6)
            {
                col2.DataItemTemplate = new MyComboTemplate(DropDownStyle.DropDownList);
                var txt1 = ((ASPxGridView)sender).FindRowCellTemplateControl(e.VisibleIndex + 1, col2, "cmbBox") as ASPxComboBox;
            }
        }

        /// <summary>
        /// konfiguro griden
        /// </summary>
        /// <param name="idNdermarrje"></param>
        public void KonfiguroGrideFushash(int idNdermarrje, int idGjuha)
        {//konfigurohet grida
            GridUtil.percaktoVisibleColumnsMeWidth(idGjuha, idNdermarrje, gvFushat, "gvFushat", EmerKomponente);
            GridUtil.konfiguroGrideRegjistrimEvogelPaTheme(gvFushat, "IdVleraFushaShtese", false);
            gvFushat.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
        }

        /// <summary>
        /// kur grida ben callback
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvFushat_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
        }

        /// <summary>
        /// merr nga sesioni vlerat e vjetra dhe i update-n me vlerat qe jane ne hiddenField
        /// </summary>
        /// <param name="index">indexi i modelit</param>
        /// <returns></returns>
        protected Dictionary<int, Dictionary<string, colVleraFushaShtese>> MerrFushatTePerditesuara(int idModeli, DateTime dt)
        {//ruhet collectioni i fushave shtese sipas te dhenave te futura nga perdoruesi
            var dataString = dt.ToString("dd/MM/yyyy");
            //kontrollojme nese dt e aktivizimit qe kemi eshte data e fundit per keto fusha
            var vleraFundit = colVleraFushaShtese.EshteDataMeEfundit(IDEntiteti, idModeli, dt);

            //marrim vlera nga hiddenfield
            var dokumenti = JsonConvert.DeserializeObject<object[]>(fushat);

            var colVleratGjitha = mySessionObjects.merrVleraNgaSesioni(Session, EmerKomponente);
            if (colVleratGjitha.Count == 0 && IDEntiteti == -1)
                colVleratGjitha[idModeli] = new Dictionary<string, colVleraFushaShtese>();
            Dictionary<string, colVleraFushaShtese> vleratPerKeteModel = null;

            if (!colVleratGjitha.TryGetValue(idModeli, out vleratPerKeteModel)) return colVleratGjitha;
            //marrim vlerat e nje modeli sipas idModelit
            colVleraFushaShtese vleratPerDatenEAktivizimit;
            if (!vleratPerKeteModel.TryGetValue(dataString, out vleratPerDatenEAktivizimit) || vleratPerDatenEAktivizimit.Count == 0)
            {
                vleratPerKeteModel.Clear();
                vleratPerDatenEAktivizimit = mySessionObjects.merrVleraNgaSesioniGrida(Session, EmerKomponente).Clone();
                vleratPerKeteModel[dataString] = vleratPerDatenEAktivizimit;
            }
            //vendos vlerat e reja tek vleratPerDatenEAktivizimit
            vleratPerDatenEAktivizimit.PerditesoVlerat(dt, vleraFundit, dokumenti, IdPerdoruesi, IDEntiteti);
            if (vleraFundit)
            {
                foreach (var data in vleratPerKeteModel.Keys.Where(data => data != dataString))
                {
                    vleratPerKeteModel[data].ForEach(x => x.VleraFundit = false);
                }
            }

            colVleratGjitha[idModeli] = vleratPerKeteModel;
            mySessionObjects.ruajVleraNeSesion(Session, EmerKomponente, colVleratGjitha);
            return colVleratGjitha;
        }



        private void PastroFushatShteseNgaSessioni()
        {
            mySessionObjects.RuajFushaShteseNeSessionGrida(Session, EmerKomponente, null);
            mySessionObjects.ruajVleraNeSesionGrida(Session, EmerKomponente, null);
            mySessionObjects.ruajVleraNeSesion(Session, EmerKomponente, null);
            mySessionObjects.ruajFushaShteseNeSession(Session, EmerKomponente, null);
            mySessionObjects.ruajVleraNeSesion(Session, GetSessionKeyVleraOrigjinale(), null);
        }

        /// <summary>
        /// percakton disa property te grides te cilat mund te perdoren ne javascript
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvFushat_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            e.Properties["cpPageIndex"] = gvFushat.PageIndex;
            e.Properties["cpPageRow"] = gvFushat.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = gvFushat.VisibleRowCount;
        }

        public void PastroFusha(int id)
        {
            HfFushaShtese.Set("fushat", "");
            percaktoTemplateFushash();
        }



        private void aplikoValidationSettings(ASPxEdit editBox, clsFushaShtese fushaShtese)
        {
            var type = editBox.GetType().Name;
            if (type == "ASPxTextBox" || type== "ASPxSpinEdit")
            {
                editBox.ValidationSettings.RegularExpression.ValidationExpression = @"^(?!^.{" + (fushaShtese.GjatesiaFushaShtese + 1) + @"}).+";
                editBox.ValidationSettings.RegularExpression.ErrorText = "Nuk mund te shkruani me shume se sa keni percaktuar tek madhesia e fushes";
                editBox.ValidationSettings.CausesValidation = true;
            }
            editBox.ValidationSettings.EnableCustomValidation = true;

            if (type == "ASPxTextBox")
                ((ASPxTextBox)editBox).MaxLength = fushaShtese.GjatesiaFushaShtese;
            else if(type == "ASPxSpinEdit")
            {
                ((ASPxSpinEdit)editBox).MaxLength = fushaShtese.GjatesiaFushaShtese;
            }
            
            editBox.ValidationSettings.SetFocusOnError = true;
            editBox.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithTooltip;
            editBox.ClientEnabled = fushaShtese.Lejueshme;
            
            editBox.ValidationSettings.RequiredField.IsRequired = fushaShtese.Detyrueshme;
            if (editBox.ValidationSettings.RequiredField.IsRequired)
            {
                editBox.ValidationSettings.ValidationGroup = "entries";
                editBox.ValidationSettings.ErrorText = "Kjo fushe eshte e detyrueshme!";

            }
            else
               editBox.ValidationSettings.ValidationGroup = "entries1";
        

        }


        /// <summary>
        /// merr vlerat e fushave shtese per ruajtje
        /// </summary>
        /// <returns></returns>
        public colVleraFushaShtese merrFushatShtese()
        {
            //modeli dhe data zgjedhur
            var idModeli = Convert.ToInt32(cmbModeli.Value);
            var dtAkt = dtAktivizimi.Date != new DateTime() ? dtAktivizimi.Date : new DateTime(2000, 1, 1);
            //marrim fushat e perditesuara (duke vendosur vlerat e fundit qe jane ne gride tek objeket e modelin te zgjedhur)
            //merr vlerat origjinale te pa modifikuara
            var vleraTeModifikuaraSipasDates = colVleraFushaShtese.GrupoSipasDates(colVleraFushaShtese.ZbertheTeGrupuarat(MerrFushatTePerditesuara(idModeli, dtAkt)));

            var vleraOrigjinialeSipasDates = colVleraFushaShtese.GrupoSipasDates(colVleraFushaShtese.ZbertheTeGrupuarat(mySessionObjects.merrVleraNgaSesioni(Session, GetSessionKeyVleraOrigjinale())));

            //kontrollohet nese modifikimi i bere eshte i vlefshem
            var modifikimIVlefshem = colVleraFushaShtese.EshteModifikimIVlefshem(IdKonfigurimi, vleraTeModifikuaraSipasDates, vleraOrigjinialeSipasDates);

            if (!modifikimIVlefshem) throw new MyException(modifikimIVlefshem.PershkrimMesazhi);

            PastroFushatShteseNgaSessioni();
            return new colVleraFushaShtese(vleraTeModifikuaraSipasDates.SelectMany(y => y.Value));
        }
    }
}