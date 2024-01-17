using System;
using System.Diagnostics;
using System.Net;
using System.Web;
using System.Web.SessionState;
using DbCore.IMBUtils;
using DbCore.IMBUtils.Extensions;

namespace PlatinumWeb
{
	public static class HttpApplicationHelper
	{
		/// <summary>
		/// nese nuk eshte .aspx ose .ashx nuk ka gjenerim, bejne perjashtim rastet e pjeses se dyte te statement
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		public static bool EshteUrlPaScopeID(string absoluteUrl, string queryUrl)
		{
			return ($"~{absoluteUrl}".ContainsAnyIgnoreCase("GISProxyGEO.ashx", "E-Payslip") || !(absoluteUrl.EndsWith(".aspx")));
		}
		/// <summary>
		/// kontrollon nese po tentohet te aksesohet nje faqe login apo reset pass te cilat jane pa auth
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		public static bool EshteUrlLogini(string url)
		{
			return $"~{url}".ContainsAnyIgnoreCase(Paths.defaultLoginPath, Paths.loginPathEpaySlip, Paths.ndryshimFjalekalimi, Paths.ndryshimFjalekalimiEpaysLip);
		}

		/// <summary>
		/// kontrollon nese kerkesa eshte per nje faqe raporti apo jo
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		public static bool EshteFaqeRaporti(HttpRequest request)
		{
			return request.Url.LocalPath.ContainsAnyIgnoreCase("Raporti.aspx", "RaportiShpejte.aspx", "RaportiEPaySlip.aspx");
		}
		/// <summary>
		/// kontrollon nese kerkesa eshte rest api apo jo
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		public static bool IsApiUrl(HttpRequest request)
		{
			return request.Url.LocalPath.IndexOf("/api/", StringComparison.InvariantCultureIgnoreCase) != -1;
		}
		public static bool KaNevojeTeJeteILoguar(string url)
		{
			return !(EshteUrlLogini(url) || $"~{url}".ContainsAnyIgnoreCase(".asmx"));
		}
		/// <summary>
		/// kontrollon nese sesioni i kesaj kerkese ka skaduar dhe po krijohet nje session i ri
		/// </summary>
		/// <param name="session"></param>
		/// <param name="request"></param>
		/// <returns></returns>
		public static bool KaSkaduarSessioni(HttpSessionState session, HttpRequest request)
		{
			var szCookieHeader = request.Headers["Cookie"];
			return (!String.IsNullOrWhiteSpace(szCookieHeader) && szCookieHeader.EqualsIgnoreCase("ASP.NET_SessionId"));
		}
		/// <summary>
		/// shkruan ne response nje unauthorized content
		/// </summary>
		/// <param name="response"></param>
		public static void WriteUnAuthorizetResponse(HttpResponse response, bool ndermarrje)
		{
			response.StatusCode = (int)HttpStatusCode.Unauthorized;
			if (ndermarrje)
				response.StatusDescription = "ScopeExpired";
			else
				response.StatusDescription = "Authentication required";
			response.SuppressFormsAuthenticationRedirect = true;
			response.SuppressContent = true;
			response.ContentType = "application/json";
			response.Flush();
		}

		[Conditional("DEBUG")]
		public static void WriteErrorOnResponse(HttpResponse response, Exception ex)
		{
			response.Write("<h2>Global Page Error</h2>\n");
			response.Write($"<p>{ex.Message}</p>\n");
			response.Write($"<p>{ex}</p>\n");
			response.Write("Return to the <a href='FaqeKryesore.aspx'>Default Page</a>\n");
		}
	}
}