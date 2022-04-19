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

namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_GrafikuVleraveTeShiturSipasOreve : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_GrafikuVleraveTeShiturSipasOreve(){InitializeComponent();} 
        private ResourceManager rm = new ResourceManager("Resources.Strings",System.Reflection.Assembly.Load("App_GlobalResources"));
        CultureInfo ci;
        private int gjuha;
        public Rap_GrafikuVleraveTeShiturSipasOreve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.IdGjuha, param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_GrafikuVleraveTeShiturSipasOreve(int idGjuha, CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
        {
            this.ci = ci;
            InitializeComponent();
            gjuha = idGjuha;
            EmrateLabelave(ci);
            DtDok.Value = raport.Parameters[0].Value;
            if (raport.Parameters[4].Value != null)
            {
                switch (raport.Parameters[4].Value.ToString())
                {
                    case "0":
                        Ndermarja.Value = "30 " + rm.GetString("filterMinuta", ci);
                        break;
                    case "1":
                        Ndermarja.Value = "60 " + rm.GetString("filterMinuta", ci);
                        break;
                    case "2":
                        Ndermarja.Value = "120 " + rm.GetString("filterMinuta", ci);
                        break;
                    default: break;
                }
            }
            CikliEmer.Value = raport.Parameters[9].Value;
            if (raport.Parameters[9].Value.ToString() == "")
                TipGrafik.Value = "Bar";
            else TipGrafik.Value = raport.Parameters[9].Value;
            ShfaqParam.Value = raport.Parameters[5].Value;
            Pikeshitje.Value = raport.Parameters[3].Value;
            magazina.Value = raport.Parameters[1].Value;
            kartela.Value = raport.Parameters[2].Value;
        }

        string TipGrafiku;
        private void rap_TeArdhuraShpenzimiDheFitimi_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var parameters = AlphaWebReports.raporteUtil.DeserializoParametrat(this.Extensions["filtrat"]);
            var filterTipGrafiku = parameters.Find(param => param.Emri == "filterTipGrafiku");
            TipGrafiku = AlphaWebReports.raporteUtil.ktheVlereFiltriTipGrafiku(filterTipGrafiku.Vlera, ci);


            DataSet ds = (System.Data.DataSet)this.DataSource;
            if (ds.Tables[0].Rows.Count != 0)
                this.xrChart1.DataSource = ds;
            else this.xrChart1.DataSource = null;
            this.xrChart1.DataAdapter = this.DataAdapter;
            bool labelVisible = false;
            if (ShfaqParam.Value.ToString() == "0")
             labelVisible = true; 
            else labelVisible = false;
            xrChart1.Series.Clear();
            konfiguroSeries(rm.GetString("labelRaportVleraShitur", ci), ((System.Data.DataSet)this.DataSource).Tables[0], ScaleType.Qualitative, "Periudha", ScaleType.Numerical, "Totali", labelVisible);
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
                case DevExpress.XtraCharts.ViewType.Bar :
                    break;
                case DevExpress.XtraCharts.ViewType.Bar3D :
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
            xrChart1.Series.Add(series);
            MerrTipGrafiku(series);
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            xrLabel17.Text = rm.GetString("titulluRaportGrafikuVleresSeShiturSipasOreve", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel10.Text = rm.GetString("labelRaportVleraShitur", ci);
            xrLabel8.Text = rm.GetString("RadioButtonListEditItemPeriudha", ci);
        }
    }
}
