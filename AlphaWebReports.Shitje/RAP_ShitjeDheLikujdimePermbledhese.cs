using DevExpress.XtraReports.UI;
using System.Resources;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class RAP_ShitjeDheLikujdimePermbledhese : XtraReport
    {
        public RAP_ShitjeDheLikujdimePermbledhese()
        {
            InitializeComponent();
        }

        public RAP_ShitjeDheLikujdimePermbledhese(AlphaWebReports.Common.ParametraRaporti param, XtraReport report)
        {
            InitializeComponent();
            LabelNames(param.Ci);
        }

        public void LabelNames(System.Globalization.CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                         System.Reflection.Assembly.Load("App_GlobalResources"));

            xrLabel35.Text = rm.GetString("labelLogoIMB", ci);
            xrLabel12.Text = rm.GetString("lblTitullShitjeDheLikujdimePermbledhese", ci);
        }

    }
}
