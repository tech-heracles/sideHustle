using System;
using System.Collections.Generic;
using Web.Framework.Templates;

namespace PlatinumWeb.ApplicationUtils.Filters
{
    public class TitlePeriudha
    {
        public static Dictionary<string, string> MappingPeriudha = new Dictionary<string, string>
        {
            { "Aktuale",LlojPeriudhe.Aktuale.ToString()},
            { "Ditore",LlojPeriudhe.Ditore.ToString()},
            { "3 Ditore",LlojPeriudhe.TreDitore.ToString()},
            { "Javore",LlojPeriudhe.Javore.ToString()},
            { "3 Mujore",LlojPeriudhe.TreMujore.ToString()},
            { "Vit ushtrimor",LlojPeriudhe.VitUshtrimor.ToString()},
            { "Gjithe Vitet", LlojPeriudhe.GjitheVitet.ToString()}
        };
        private string _periudhaDok;
        public const string KeyFieldPeriudha = "periudhatHfStateKey";
        public const string KeyParamNdryshimPeriudhe = "ndryshimPeriudhe";
        public string DataNgaAktuale { get; set; }

        public string DataDeriAktuale { get; set; }

        public string DataNgaViti { get; set; }

        public string DataDeriViti { get; set; }

        public string DataDokNga { get; set; }

        public string DataDokDeri { get; set; }
        public TopRowsControl TopRowsControl { get; set; }
        /// <summary>
        /// periudha kontabel
        /// </summary>
        public string PeriudhaDefault { get; set; }

        public string PeriudhaDok
        {
            get
            {

                return _periudhaDok;
            }
            set { _periudhaDok = value; }
        }

        public int IdViti { get; set; }

        public void LlogaritKufijtEDates()
        {
            LlojPeriudhe periudha;
            Enum.TryParse(PeriudhaDok, out periudha);

            switch (periudha)
            {
                case LlojPeriudhe.Aktuale:
                    DataDokNga = DataNgaAktuale;
                    DataDokDeri = DataDeriAktuale;
                    break;
                case LlojPeriudhe.GjitheVitet:
                    DataDokNga = "01/01/1900";
                    DataDokDeri = DataDeriAktuale;
                    break;
                case LlojPeriudhe.VitUshtrimor:
                    DataDokNga = DataNgaViti;
                    DataDokDeri = DataDeriViti;
                    break;
                case LlojPeriudhe.TreMujore:
                    DataDokNga = Convert.ToDateTime(DataNgaAktuale).AddMonths(-2).ToString("dd/MM/yyyy");
                    DataDokDeri = DataDeriAktuale;
                    break;
                case LlojPeriudhe.Javore:
                    DataDokNga = DateTime.Now.AddDays(-7).ToString("dd/MM/yyyy");
                    DataDokDeri = DateTime.Now.ToString("dd/MM/yyyy");
                    break;
                case LlojPeriudhe.TreDitore:
                    DataDokNga = DateTime.Now.AddDays(-3).ToString("dd/MM/yyyy");
                    DataDokDeri = DateTime.Now.ToString("dd/MM/yyyy");
                    break;
                case LlojPeriudhe.Ditore:
                    DataDokNga = DataDokDeri = DateTime.Now.ToString("dd/MM/yyyy");
                    break;

                default:
                    DataDokNga = DataNgaAktuale;
                    DataDokDeri = DataDeriAktuale;
                    PeriudhaDefault = "Aktuale";
                    break;
            }
        }
    }
}