using System.Globalization;

namespace DbCore.Raporte
{
    public class ParametraRaporti
    {
        public CultureInfo Ci { get; set; }
        public int IdNdermarrje { get; set; }
        public int IdPerdoruesi { get; set; }
        public string GuidString { get; set; }
        public int IdRaporti { get; set; }
        public int IdGjuha { get; set; }
        public string Vjen { get; set; }
        public int IdViti { get; set; }
        public int IdSubRaporti { get; set; }
        public string ScopeID { get; set; }

        public string NdermarrjePershkrimi { get; set; }
        public string NdermarrjeKodi { get; set; }

        public string NdermarrjeNipt { get; set; }

        public string NdermarrjeVendi { get; set; }
        public string KodiViti { get; set; }

        public string EmriPerdorues { get; set; }

        public string MbiemriPerdorues { get; set; }

        public string NdermarrjeQytetiPershkrimi { get; set; }

        public string NdermarrjeTel { get; set; }

        public string NdermarrjeKodiFiskal { get; set; }
    }
}
