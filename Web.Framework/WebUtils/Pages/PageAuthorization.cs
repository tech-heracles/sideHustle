using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DbCore.IMBUtils.Extensions;

namespace PlatinumWeb.ApplicationUtils.Pages
{
    public class PageAuthorization
    {
        private HttpRequest _request;
        private string komponente;
        private string komponenteReferues;
        private string url;
        private int _idPerdoruesi;
        private int _idNdermarrje;
        private int _idViti;
        private int _idGjuha;
        private List<string> komponentetAll;

        public PageAuthorization(HttpRequest request, int idPerdoruesi, int idNdermarrje, int idViti, int idGjuha)
        {
            _request = request;
            _idPerdoruesi = idPerdoruesi;
            _idNdermarrje = idNdermarrje;
            _idViti = idViti;
            _idGjuha = idGjuha;

        }
        public PageAuthorization(HttpRequest request, int idPerdoruesi, int idNdermarrje, int idViti, int idGjuha, List<string> komponentet) : this(request, idPerdoruesi, idNdermarrje, idViti, idGjuha)
        {
            komponentetAll = komponentet;
            komponente = MerrKomponenteNgaUrlNew(_request.Url);
            komponenteReferues = _request.UrlReferrer != null ? MerrKomponenteNgaUrlNew(_request.UrlReferrer) : null;
        }
        public bool KaAutorizim()
        {
            if (string.IsNullOrWhiteSpace(komponente)) return true;
            //TODO GETSON remove this
            return RestApi.WebAPI.Models.AutorizimeRepository.kaTeDrejteTeHapeAmbjentin(_idPerdoruesi, _idNdermarrje, _idViti, _idGjuha, komponente, HttpUtility.ParseQueryString(_request.Url.Query).ToDictionary());
        }
        public bool IsPostBack()
        {
            return komponenteReferues != null && komponente == komponenteReferues;
        }
        public string MerrKomponenteNgaUrlNew(Uri url)
        {
            if (url.AbsolutePath == "/") return "/";
            var listUrl = HttpUtility.ParseQueryString(url.Query)["listUrl"];
            if (listUrl != null) return listUrl;

            var newQueryString = HttpUtility.ParseQueryString(url.Query).AsKVP();

            var baseUrl = url.AbsolutePath.Split('/')[1].Split('?')[0];

            return FindExact(baseUrl, komponentetAll, newQueryString);
        }


        /// <summary>
        /// funksion rekursiv qe gjen emrin e sakte te komponentes
        /// </summary>
        /// <param name="url"></param>
        /// <param name="komponente"></param>
        /// <param name="queryString"></param>
        /// <returns></returns>
        private string FindExact(string url, List<string> komponente, List<KeyValuePair<string, string>> queryString)
        {
            var matched = komponente.FindAll(x => x.IndexOf(url, StringComparison.InvariantCultureIgnoreCase) == 0).ToList();

            if (matched.Count == 0) return null;

            //nese gjendet nje komponente ekuivaleten ky eshte i kerkuari
            if (matched.Exists(x => x.Equals(url, StringComparison.InvariantCultureIgnoreCase))) return url;
            if (queryString.Count == 0) return url;
            var firstParam = queryString.Where(x => x.Key != CacheLayer.ScopeManager.ScopeIdKey).First();
            queryString.Remove(firstParam);

            //nese ska ? shtohet per here te pare nje param nga querystring else //shtohet nje param i ri nga qyerystring
            var newUrl = (url.IndexOf("?") == -1) ? $"{url}?{firstParam.Key}={firstParam.Value}" : $"{url}&{firstParam.Key}={firstParam.Value}";
            return FindExact(newUrl, matched, queryString);
        }

        public static string RemoveQueryStringByKey(Uri url, params string[] keys)
        {
            var newQueryString = HttpUtility.ParseQueryString(url.Query);
            var pagePathWithoutQueryString = url.AbsolutePath.Split('/')[1].Split('?')[0];
            foreach (var key in keys)
                newQueryString.Remove(key);
            return newQueryString.Count > 0 ? $"{pagePathWithoutQueryString}?{newQueryString}" : pagePathWithoutQueryString;
        }
    }
}