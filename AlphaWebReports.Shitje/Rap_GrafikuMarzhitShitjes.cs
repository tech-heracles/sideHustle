using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.XtraCharts;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Resources;
using System.Reflection;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_GrafikuMarzhitShitjes : DevExpress.XtraReports.UI.XtraReport
    {        
		public Rap_GrafikuMarzhitShitjes(){InitializeComponent();} 
        ResourceManager rm = new ResourceManager("Resources.Strings",System.Reflection.Assembly.Load("App_GlobalResources"));
        CultureInfo ci;
        private int gjuha;
        public Rap_GrafikuMarzhitShitjes(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.IdGjuha, param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_GrafikuMarzhitShitjes( int idGjuha,CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
        {
            this.ci = ci;
            InitializeComponent();
            EmrateLabelave(ci);
            gjuha = idGjuha;
            parametergjuha.Value = idGjuha;
            Ndermarja.Value = raport.Parameters[0].Value;
            DtDok.Value = raport.Parameters["filterDtDok"].Value;
            KartelaArtikullit.Value = raport.Parameters[3].Value;
            Qyteti.Value = raport.Parameters[4].Value;
            Grupim1.Value = raport.Parameters[5].Value;
            Grupim2.Value = raport.Parameters[6].Value;
            Cikli.Value = raport.Parameters["filterCikli"].Value;
            LlojArtikulli.Value = raport.Parameters["filterLlojArtikulli"].Value;
            ShfaqVlera.Value = raport.Parameters["filterShfaqVlerat"].Value;
            PikeShitjeFurnizim.Value = raport.Parameters[11].Value;
            KlasaArtikullit.Value = raport.Parameters[12].Value;
            parameter1.Value = raport.Parameters[13].Value;
            parameter2.Value = raport.Parameters[14].Value;
            parameter3.Value = raport.Parameters[15].Value;
            parameter4.Value = raport.Parameters[17].Value;
            parameter5.Value = raport.Parameters[18].Value;
            parameter6.Value = raport.Parameters[19].Value;
            DegaAdministrative.Value = raport.Parameters[16].Value;
        }



        string TipGrafiku;
        private void Rap_GrafikuMarzhitShitjes_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var parameters = AlphaWebReports.raporteUtil.DeserializoParametrat(this.Extensions["filtrat"]);
            var filterTipGrafiku = parameters.Find(param => param.Emri == "filterTipGrafiku");
            var parametraRaporti = AlphaWebReports.raporteUtil.DeserializoParametraPerKonstruktorRaporti(this.Extensions["parametraRaporti"]);
            TipGrafiku = AlphaWebReports.raporteUtil.ktheVlereFiltriTipGrafiku(filterTipGrafiku.Vlera, ci);

            System.Data.DataSet ds = ((System.Data.DataSet)this.DataSource);
            if (ds.Tables[0].Rows.Count != 0 && ds != null)
            {
                this.marzhiShitjeveChart.DataSource = ds;
                this.marzhiShitjeveChart.DataAdapter = this.DataAdapter;
                bool labelVisible = false;
                if (LlojArtikulli.Value.ToString() == "0")
                    labelVisible = true;
                else labelVisible = false;
                marzhiShitjeveChart.Series.Clear();
                switch (gjuha)
                {
                    case 0:
                        konfiguroSeries("Vlera me zbritje", ds.Tables[0], ScaleType.Qualitative, "DATA", ScaleType.Numerical, "VLERAMEZBRITJE", labelVisible);
                        konfiguroSeries("KMSH", ds.Tables[0], ScaleType.Qualitative, "DATA", ScaleType.Numerical, "KMSH", labelVisible);
                        konfiguroSeries("Marzhi me zbritje", ds.Tables[0], ScaleType.Qualitative, "DATA", ScaleType.Numerical, "MARZHIMEZBRITJE", labelVisible);
                        break;
                    case 1:
                        konfiguroSeries("Net amount (with discount)", ds.Tables[0], ScaleType.Qualitative, "DATA_eng", ScaleType.Numerical, "VLERAMEZBRITJE", labelVisible);
                        konfiguroSeries("KMSH", ds.Tables[0], ScaleType.Qualitative, "DATA_eng", ScaleType.Numerical, "KMSH", labelVisible);
                        konfiguroSeries("Margin with discount", ds.Tables[0], ScaleType.Qualitative, "DATA_eng", ScaleType.Numerical, "MARZHIMEZBRITJE", labelVisible);
                        break;
                }
            }
        }

        private void konfiguroSeries(string emri, DataTable datasource, ScaleType argScale, string argMember, ScaleType valScale, string valMember, bool labelVisible)
        {
            ViewType type = ((ViewType)Enum.Parse(typeof(ViewType), TipGrafiku.ToString()));
            Series series = new Series(emri, type);
            series.DataSource = datasource;
            series.ArgumentScaleType = argScale;
            series.ArgumentDataMember = argMember;
            series.ValueScaleType = valScale;
            series.ValueDataMembers.AddRange(new string[] { valMember });            
            series.LabelsVisibility = labelVisible ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;
            series.Label.Border.Visible = false;            
            series.Label.PointOptions.ValueNumericOptions.Format = NumericFormat.Number;
            series.Label.PointOptions.ValueNumericOptions.Precision = 2;
            //series.PointOptions.ValueNumericOptions.Format = NumericFormat.Number;
            //series.Label.Visible = labelVisible;
            //series.PointOptions.ValueNumericOptions.Precision = 2;

            marzhiShitjeveChart.Series.Add(series);
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
        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            MarzhiShitjeveLabel.Text = rm.GetString("RaportGrafikuMarzhiShitjeveTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel4.Text = rm.GetString("RadioButtonListEditItemPeriudha", ci);
            xrLabel1.Text = rm.GetString("labelKMSH", ci);
            xrLabel8.Text = rm.GetString("labelRaportVleraShitjeveMeZbritje", ci);
            xrLabel9.Text = rm.GetString("labelRaportMarzhiShitjeveMeZbritje", ci);
            xrLabel32.Text = rm.GetString("labelLogoIMB", ci);
        }        
    }
}
