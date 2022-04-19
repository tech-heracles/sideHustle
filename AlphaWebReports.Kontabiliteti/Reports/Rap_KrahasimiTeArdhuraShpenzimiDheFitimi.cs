using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraCharts;
using System.Data;
using System.Globalization;
using System.Resources;
using System.Reflection;


namespace AlphaWebReports.RaportetDs
{
    public partial class Rap_KrahasimiTeArdhuraShpenzimiDheFitimi : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_KrahasimiTeArdhuraShpenzimiDheFitimi()
        {
            InitializeComponent();
        }
        ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
        CultureInfo ci;

        private int gjuha;
        public Rap_KrahasimiTeArdhuraShpenzimiDheFitimi(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.IdGjuha, param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_KrahasimiTeArdhuraShpenzimiDheFitimi(int idGjuha,CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
        {
            this.ci = ci;
            gjuha = idGjuha;
            InitializeComponent();
            EmrateLabelave(ci);
            DtDokAktuale.Value = raport.Parameters["filterDtDok"].Value;
            DtDokKrahasues.Value = raport.Parameters["filterDtDokKrahasues"].Value;
            if (raport.Parameters["filterTipGrafikuKrahasues"].Value.ToString() == "")
                GrafikuKrahasues.Value = "0";
            else
                GrafikuKrahasues.Value = raport.Parameters["filterTipGrafikuKrahasues"].Value;
        }


       string TipGrafiku;
        private void Rap_KrahasimiTeArdhuraShpenzimiDheFitimi_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
          xrPictureBox1.Image = AlphaWebReports.raporteUtil.MerrLogoNdermarrje(this.Extensions["ndermarrjeLogo"]);

            var parameters = AlphaWebReports.raporteUtil.DeserializoParametrat(this.Extensions["filtrat"]);
            var filterTipGrafiku = parameters.Find(param => param.Emri == "filterTipGrafiku");
            TipGrafiku = AlphaWebReports.raporteUtil.ktheVlereFiltriTipGrafiku(filterTipGrafiku.Vlera, ci);

            this.ashfChart.DataSource = (System.Data.DataSet)this.DataSource;
            this.ashfChart.DataAdapter = this.DataAdapter;
            bool labelVisible = false;
            if (ShfaqLabel.Value.ToString() == "0")
                labelVisible = true;
            else labelVisible = false;
            ashfChart.Series.Clear();
            aktuale.DataBindings.Clear();
            paraardhese.DataBindings.Clear();
            data.DataBindings.Clear();
            XRBinding bindingAktuale;
            XRBinding bindingParaardhese;
            String periudhaAkt = DtDokAktuale.Value.ToString();
            String periudhaPar = DtDokKrahasues.Value.ToString();
            switch (GrafikuKrahasues.Value.ToString())
            {
                case "0":

                    switch (gjuha)
                    {
                        case 0:
                            konfiguroSeries(ashfChart, rm.GetString("filterTeArdhurat", ci) + " " + periudhaAkt.Substring(periudhaAkt.Length - Math.Min(4, periudhaAkt.Length)), ((System.Data.DataSet)this.DataSource).Tables[0], ScaleType.Qualitative, "data", ScaleType.Numerical, "teArdhuraAktuale", labelVisible);
                            konfiguroSeries(ashfChart, rm.GetString("filterTeArdhurat", ci) + " " + periudhaPar.Substring(periudhaPar.Length - Math.Min(4, periudhaPar.Length)), ((System.Data.DataSet)this.DataSource).Tables[0], ScaleType.Qualitative, "data", ScaleType.Numerical, "teArdhuraParaardhese", labelVisible);
                            break;
                        case 1:
                            konfiguroSeries(ashfChart, rm.GetString("filterTeArdhurat", ci) + " " + periudhaAkt.Substring(periudhaAkt.Length - Math.Min(4, periudhaAkt.Length)), ((System.Data.DataSet)this.DataSource).Tables[0], ScaleType.Qualitative, "data_eng", ScaleType.Numerical, "teArdhuraAktuale", labelVisible);
                            konfiguroSeries(ashfChart, rm.GetString("filterTeArdhurat", ci) + " " + periudhaPar.Substring(periudhaPar.Length - Math.Min(4, periudhaPar.Length)), ((System.Data.DataSet)this.DataSource).Tables[0], ScaleType.Qualitative, "data_eng", ScaleType.Numerical, "teArdhuraParaardhese", labelVisible);
                            break;
                    }

                   // titulliRaportLabel.Text += rm.GetString("labelRaportGrafikKrahasuesTeArdhura", ci);
                    bindingAktuale = new XRBinding("Text", ((System.Data.DataSet)this.DataSource).Tables[0], "teArdhuraAktuale");
                    bindingParaardhese = new XRBinding("Text", ((System.Data.DataSet)this.DataSource).Tables[0], "teArdhuraParaardhese");
                    xrLabel8.Text = rm.GetString("filterTeArdhurat", ci) + periudhaAkt.Substring(periudhaAkt.Length - Math.Min(4, periudhaAkt.Length));
                    xrLabel9.Text = rm.GetString("filterTeArdhurat", ci) + periudhaPar.Substring(periudhaPar.Length - Math.Min(4, periudhaPar.Length));
                    break;
                case "1":

                    switch (gjuha)
                    {
                        case 0:
                            konfiguroSeries(ashfChart, rm.GetString("labelShpenzimet", ci) + " " + periudhaAkt.Substring(periudhaAkt.Length - Math.Min(4, periudhaAkt.Length)), ((System.Data.DataSet)this.DataSource).Tables[0], ScaleType.Qualitative, "data", ScaleType.Numerical, "shpenzimeAktuale", labelVisible);
                            konfiguroSeries(ashfChart, rm.GetString("labelShpenzimet", ci) + " " + periudhaPar.Substring(periudhaPar.Length - Math.Min(4, periudhaPar.Length)), ((System.Data.DataSet)this.DataSource).Tables[0], ScaleType.Qualitative, "data", ScaleType.Numerical, "shpenzimeParaardhese", labelVisible);
                            break;
                        case 1:
                            konfiguroSeries(ashfChart, rm.GetString("labelShpenzimet", ci) + " " + periudhaAkt.Substring(periudhaAkt.Length - Math.Min(4, periudhaAkt.Length)), ((System.Data.DataSet)this.DataSource).Tables[0], ScaleType.Qualitative, "data_eng", ScaleType.Numerical, "shpenzimeAktuale", labelVisible);
                            konfiguroSeries(ashfChart, rm.GetString("labelShpenzimet", ci) + " " + periudhaPar.Substring(periudhaPar.Length - Math.Min(4, periudhaPar.Length)), ((System.Data.DataSet)this.DataSource).Tables[0], ScaleType.Qualitative, "data_eng", ScaleType.Numerical, "shpenzimeParaardhese", labelVisible);
                            break;
                     
                    }

                  //  titulliRaportLabel.Text += rm.GetString("labelRaportGrafikKrahasuesShpenzimeve", ci);
                    bindingAktuale = new XRBinding("Text", ((System.Data.DataSet)this.DataSource).Tables[0], "shpenzimeAktuale");
                    bindingParaardhese = new XRBinding("Text", ((System.Data.DataSet)this.DataSource).Tables[0], "shpenzimeParaardhese");
                    xrLabel8.Text = rm.GetString("labelShpenzimet", ci)  + periudhaAkt.Substring(periudhaAkt.Length - Math.Min(4, periudhaAkt.Length));
                    xrLabel9.Text = rm.GetString("labelShpenzimet", ci)  + periudhaPar.Substring(periudhaPar.Length - Math.Min(4, periudhaPar.Length));
                    break;
               
                case "2":
                    switch (gjuha)
                    {
                        case 0:
                            konfiguroSeries(ashfChart, rm.GetString("filterFitimi", ci) + " " + periudhaAkt.Substring(periudhaAkt.Length - Math.Min(4, periudhaAkt.Length)), ((System.Data.DataSet)this.DataSource).Tables[0], ScaleType.Qualitative, "data", ScaleType.Numerical, "fitimAktual", labelVisible);
                            konfiguroSeries(ashfChart, rm.GetString("filterFitimi", ci) + " " + periudhaPar.Substring(periudhaPar.Length - Math.Min(4, periudhaPar.Length)), ((System.Data.DataSet)this.DataSource).Tables[0], ScaleType.Qualitative, "data", ScaleType.Numerical, "fitimParaardhes", labelVisible);
                            break;
                        case 1:
                            konfiguroSeries(ashfChart, rm.GetString("filterFitimi", ci) + " " + periudhaAkt.Substring(periudhaAkt.Length - Math.Min(4, periudhaAkt.Length)), ((System.Data.DataSet)this.DataSource).Tables[0], ScaleType.Qualitative, "data_eng", ScaleType.Numerical, "fitimAktual", labelVisible);
                            konfiguroSeries(ashfChart, rm.GetString("filterFitimi", ci) + " " + periudhaPar.Substring(periudhaPar.Length - Math.Min(4, periudhaPar.Length)), ((System.Data.DataSet)this.DataSource).Tables[0], ScaleType.Qualitative, "data_eng", ScaleType.Numerical, "fitimParaardhes", labelVisible);
                            break;
                       
                    }

                  //  titulliRaportLabel.Text += rm.GetString("labelRaportGrafikKrahasuesFitimit", ci);
                    bindingAktuale = new XRBinding("Text", ((System.Data.DataSet)this.DataSource).Tables[0], "fitimAktual");
                    bindingParaardhese = new XRBinding("Text", ((System.Data.DataSet)this.DataSource).Tables[0], "fitimParaardhes");
                    xrLabel8.Text = rm.GetString("filterFitimi", ci) + periudhaAkt.Substring(periudhaAkt.Length - Math.Min(4, periudhaAkt.Length));
                    xrLabel9.Text = rm.GetString("filterFitimi", ci) + periudhaPar.Substring(periudhaPar.Length - Math.Min(4, periudhaPar.Length));
                    break;
                default :
                    bindingAktuale = new XRBinding();
                    bindingParaardhese = new XRBinding();
                    break;
            }
            XRBinding bindingData = new XRBinding("Text", ((System.Data.DataSet)this.DataSource).Tables[0], "data", "{0:MM/dd/yyyy}");
            aktuale.DataBindings.Add(bindingAktuale);
            paraardhese.DataBindings.Add(bindingParaardhese);
            data.DataBindings.Add(bindingData);
        }

        private void konfiguroSeries(XRChart chart, string emri, DataTable datasource, ScaleType argScale, string argMember, ScaleType valScale, string valMember, bool labelVisible)
        {
            ViewType type = ((ViewType)Enum.Parse(typeof(ViewType), TipGrafiku.ToString()));
            Series series = new Series(emri, type);
            series.DataSource = datasource;
            series.ArgumentScaleType = argScale;
            series.ArgumentDataMember = argMember;
            series.ValueScaleType = valScale;
            series.ValueDataMembers.AddRange(new string[] { valMember });
            series.Label.Visible = labelVisible;
            series.Label.Border.Visible = false;

            series.PointOptions.ValueNumericOptions.Format = NumericFormat.Number;
            series.PointOptions.ValueNumericOptions.Precision = 2;

            chart.Series.Add(series);
            MerrTipGrafiku(series);
        }

        private void MerrTipGrafiku(Series series)
        {
            ViewType type = ((ViewType)Enum.Parse(typeof(ViewType), TipGrafiku.ToString()));
            switch (type)
            {
                case DevExpress.XtraCharts.ViewType.Line:
                    LineSeriesView vija = new LineSeriesView();
                    vija.LineMarkerOptions.Size = 5;
                    series.Label.ResolveOverlappingMode = ResolveOverlappingMode.JustifyAllAroundPoint;
                    series.View = vija;
                    break;
                case DevExpress.XtraCharts.ViewType.Line3D:
                    Line3DSeriesView vija3d = new Line3DSeriesView();
                    series.View = vija3d;
                    break;
                case DevExpress.XtraCharts.ViewType.Area3D:
                    Area3DSeriesView siperfaqe3d = new Area3DSeriesView();
                    series.View = siperfaqe3d;
                    break;
                case DevExpress.XtraCharts.ViewType.Area:
                    AreaSeriesView siperfaqe = new AreaSeriesView();
                    series.Label.ResolveOverlappingMode = ResolveOverlappingMode.JustifyAllAroundPoint;
                    series.View = siperfaqe;
                    break;
                case DevExpress.XtraCharts.ViewType.Bar:
                    break;
                case DevExpress.XtraCharts.ViewType.Bar3D:
                    break;
            }
        }

     

      

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>

        private void EmrateLabelave(CultureInfo ci)
        {
           titulliRaportLabel.Text = rm.GetString("RaportGrafikuKrahasuesTeArdhuraveTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel4.Text = rm.GetString("RadioButtonListEditItemPeriudha", ci);
            xrLabel32.Text = rm.GetString("labelLogoIMB", ci);
        }

    }
}
