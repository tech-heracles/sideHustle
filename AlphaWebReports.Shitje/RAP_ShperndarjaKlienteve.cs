using DevExpress.XtraReports.UI;


namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class RAP_ShperndarjaKlienteve : XtraReport
    {
        public RAP_ShperndarjaKlienteve()
        {
            InitializeComponent();
        }

        public RAP_ShperndarjaKlienteve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) 
        {
            InitializeComponent();
            LabelNames(param.Ci);            
        }
        
        public void LabelNames( System.Globalization.CultureInfo ci)
        {
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings",
                        System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel35.Text = rm.GetString("labelLogoIMB", ci); ;
            xrLabel12.Text = rm.GetString("lblTitullShperndarjaKlienteve", ci); ;
        }
    }
}
