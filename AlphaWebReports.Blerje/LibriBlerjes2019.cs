using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.Blerje
{
    public partial class LibriBlerjes2019 : XtraReport
    {
		public LibriBlerjes2019(){InitializeComponent();} 
       
        double maxblerjeperjashtuar = 0;
        double shumatotale = 0;

        public LibriBlerjes2019(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }

        public LibriBlerjes2019(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, XtraReport raport)
        {
            InitializeComponent();

            // Modifikimi i fushes Muaji
            string data = raport.Parameters["filterDtDok"].Value.ToString();
            string dtFillimi = data.Split('-')[0];   
            string muaji = dtFillimi.Split('/')[1];
            xrLabel103.Text = muaji.TrimStart('0');
        }
    }
}
