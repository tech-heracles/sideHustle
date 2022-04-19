using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.Blerje
{
    public partial class LibriBlerjes2019MeNrReference : XtraReport
    {
		public LibriBlerjes2019MeNrReference(){InitializeComponent();} 
    

        public LibriBlerjes2019MeNrReference(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }

        public LibriBlerjes2019MeNrReference(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, XtraReport raport)
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
