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
    public partial class rap_TeArdhuraShpenzimiDheFitimi : DevExpress.XtraReports.UI.XtraReport
    {
		public rap_TeArdhuraShpenzimiDheFitimi(){InitializeComponent();} 
        private ResourceManager rm = new ResourceManager("Resources.Strings",System.Reflection.Assembly.Load("App_GlobalResources"));
        CultureInfo ci;
        
        private int gjuha;
        public rap_TeArdhuraShpenzimiDheFitimi(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.IdGjuha, param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public rap_TeArdhuraShpenzimiDheFitimi(int idGjuha,CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
        {
            this.ci = ci;
            InitializeComponent();
            gjuha = idGjuha;
            EmrateLabelave(ci);
            xrLabel54.Text = raport.Parameters[0].Description;
            DtDok.Value = raport.Parameters[0].Value;
            xrLabel55.Text = raport.Parameters[1].Description;
            Ndermarja.Value = raport.Parameters[1].Value;
            xrLabel16.Text = raport.Parameters[4].Description;
            CikliEmer.Value = raport.Parameters[4].Value;
            ShfaqParam.Value = raport.Parameters[5].Value;
            xrLabel12.Text = raport.Parameters[5].Description;
            if (raport.Parameters[6].Value.ToString() == "")
                xrLabel12.Value = "Line";
            else xrLabel12.Value = raport.Parameters[6].Value;
            xrLabel14.Text = raport.Parameters[6].Description;
            azhornimLabel.Text = raport.Parameters[3].Description;
            Azhornim.Value = raport.Parameters[3].Value;
        }
        string TipGrafiku;

        private void rap_TeArdhuraShpenzimiDheFitimi_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var parameters = AlphaWebReports.raporteUtil.DeserializoParametrat(this.Extensions["filtrat"]);
            var filterTipGrafiku = parameters.Find(param => param.Emri == "filterTipGrafiku");
            TipGrafiku = AlphaWebReports.raporteUtil.ktheVlereFiltriTipGrafiku(filterTipGrafiku.Vlera, ci);

            xrPictureBox1.Image = AlphaWebReports.raporteUtil.MerrLogoNdermarrje(this.Extensions["ndermarrjeLogo"]);

            this.xrChart1.DataSource = (System.Data.DataSet)this.DataSource;
            this.xrChart1.DataAdapter = this.DataAdapter;
            bool labelVisible = false;
            if (ShfaqParam.Value.ToString() == "0")
                labelVisible = true;
            else labelVisible = false;
            xrChart1.Series.Clear();
            switch (gjuha)
            {
                case 0:
                    konfiguroSeries(rm.GetString("filterTeArdhurat", ci), ((System.Data.DataSet)this.DataSource).Tables[0], ScaleType.Qualitative, "data", ScaleType.Numerical, "teardhura", labelVisible);
                    konfiguroSeries(rm.GetString("labelShpenzimet", ci), ((System.Data.DataSet)this.DataSource).Tables[0], ScaleType.Qualitative, "data", ScaleType.Numerical, "shpenzime", labelVisible);
                    konfiguroSeries(rm.GetString("filterFitimi", ci), ((System.Data.DataSet)this.DataSource).Tables[0], ScaleType.Qualitative, "data", ScaleType.Numerical, "fitim", labelVisible);
                    break;
                case 1:

                    konfiguroSeries(rm.GetString("filterTeArdhurat", ci), ((System.Data.DataSet)this.DataSource).Tables[0], ScaleType.Qualitative, "data_eng", ScaleType.Numerical, "teardhura", labelVisible);
                    konfiguroSeries(rm.GetString("labelShpenzimet", ci), ((System.Data.DataSet)this.DataSource).Tables[0], ScaleType.Qualitative, "data_eng", ScaleType.Numerical, "shpenzime", labelVisible);
                    konfiguroSeries(rm.GetString("filterFitimi", ci), ((System.Data.DataSet)this.DataSource).Tables[0], ScaleType.Qualitative, "data_eng", ScaleType.Numerical, "fitim", labelVisible);
                    break;
            }
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
                    xrLabel13.Text = rm.GetString("cmbboxRaportiVija", ci);
                    break;
                case DevExpress.XtraCharts.ViewType.Line3D:
                    Line3DSeriesView vija3d = new Line3DSeriesView();
                    series.View = vija3d;
                    xrLabel13.Text = rm.GetString("cmbboxRaportiVija", ci) + " 3D";
                    break;
                case DevExpress.XtraCharts.ViewType.Area3D:
                    Area3DSeriesView siperfaqe3d = new Area3DSeriesView();
                    series.View = siperfaqe3d;
                    xrLabel13.Text = rm.GetString("cmbboxRaportiSiperfaqe", ci) + " 3D";
                    break;
                case DevExpress.XtraCharts.ViewType.Area:
                    AreaSeriesView siperfaqe = new AreaSeriesView();
                    series.Label.ResolveOverlappingMode = ResolveOverlappingMode.JustifyAllAroundPoint;
                    series.View = siperfaqe;
                    xrLabel13.Text = rm.GetString("cmbboxRaportiSiperfaqe", ci);
                    break;
                case DevExpress.XtraCharts.ViewType.Bar :
                    xrLabel13.Text = rm.GetString("cmbboxRaportiKolona", ci);
                    break;
                case DevExpress.XtraCharts.ViewType.Bar3D :
                    xrLabel13.Text = rm.GetString("cmbboxRaportiKolona", ci) + " 3D";
                    break;
            }

        }
        private void konfiguroSeries(string emri, DataTable datasourse, ScaleType argScale, string argMember, ScaleType valScale, string valMember, bool labelVisible)
        {
            ViewType type = ((ViewType)Enum.Parse(typeof(ViewType), TipGrafiku.ToString())); //tocheck KELVIN - jep error nqs eshte ne faqe te re
            Series series = new Series(emri, type);
            series.DataSource = datasourse;
            series.ArgumentScaleType =argScale;
            series.ArgumentDataMember = argMember;
            series.ValueScaleType = valScale;
            series.ValueDataMembers.AddRange(new string[] { valMember });
            series.Label.Visible = labelVisible;
            series.Label.Border.Visible = false;
            series.PointOptions.ValueNumericOptions.Format=NumericFormat.Number;
            series.PointOptions.ValueNumericOptions.Precision = 2;
            
            xrChart1.Series.Add(series);
            MerrTipGrafiku(series);
        }


        private void xrLabel15_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            switch (CikliEmer.Value.ToString())
            {
                case "1": xrLabel15.Text = rm.GetString("labelRaportDitore", ci);
                    break;
                case "0": xrLabel15.Text = rm.GetString("labelRaportMujore", ci);
                    break;
                default: break;
            }
        }

        private void xrLabel7_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            switch (ShfaqParam.Value.ToString())
            {
                case "0": xrLabel7.Text = rm.GetString("cmbboxRaportiTeDukshme", ci);
                    break;
                case "1": xrLabel7.Text = rm.GetString("cmbboxRaportiTePaDukshme", ci);
                    break;
                default: break;
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
            xrLabel17.Text = rm.GetString("RaportGrafikuArdhuraveShpenzimevFitimitTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel10.Text = rm.GetString("labelShpenzimet", ci);
            xrLabel9.Text = rm.GetString("filterTeArdhurat", ci);
            xrLabel8.Text = rm.GetString("RadioButtonListEditItemPeriudha", ci);
            xrLabel11.Text = rm.GetString("filterFitimi", ci);
        }
    }
}
