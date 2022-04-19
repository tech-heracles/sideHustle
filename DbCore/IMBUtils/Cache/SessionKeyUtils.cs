namespace DbCore.IMBUtils.Cache
{
    public class SessionKeyUtils
    {
        public static string MerrSessionKeyPerDsGride(string emerKomponente, int idViti, string periudhe) => $"{emerKomponente}_{idViti}_{periudhe}";

        public static string MerrSessionKeyPerDsGride(string emerKomponente, int idViti, string periudhe, int TopRows) => $"{emerKomponente}_{idViti}_{periudhe}_{TopRows}";

        public static string MerrSessionKeyPerDsGride(string emerKomponente, int idViti, string periudhe,string kodKonfigambjente) => $"{emerKomponente}_{kodKonfigambjente}_{idViti}_{periudhe}";

        public static string MerrSessionKeyPerDsGride(string emerKomponente, string guidStirng, int idViti, string periudhe) => $"{emerKomponente}_{guidStirng}_{idViti}_{periudhe}";

        public static string MerrSessionKeyPerCmb(string emerKomp, string kolona)
        {
            return $"{emerKomp}_cmb_{kolona}";
        }
    }
}