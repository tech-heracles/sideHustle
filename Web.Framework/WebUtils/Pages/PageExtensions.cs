using System.Web.UI;
using DbCore.IMBUtils.Extensions;
using DevExpress.Web;
using Newtonsoft.Json;
using PlatinumWeb.ApplicationUtils.Filters;
using Web.Framework.Templates;

namespace PlatinumWeb.ApplicationUtils.Pages
{
    /// <summary>
    /// ne kete klase duhet te perfshihen funksione te pergjithshme qe kane lidhje me faqet dhe controllet e faqeve
    /// KUJDES nuk duhet te perfshihen funksione qe aksesojne DB
    /// </summary>

    public static class PageUtils
    {


        /// <summary>
        /// merr id e kontrollit dhe kthen true nese kerkesa eshte callback i ketij kontrolli ne kete faqe
        /// </summary>
        /// <param name="page"></param>
        /// <param name="idKontrolli"></param>
        /// <returns></returns>
        public static bool EshteCallbackuIm(this Page page, string idKontrolli)
        {
            if (!page.IsCallback)
                return false;
            var callbackId = page.Request.Params["__CALLBACKID"];

            return null != callbackId && callbackId.Contains(idKontrolli);

        }
        public static bool ContainsCallbackParam(this Page page, params string[] callbackParam)
        {
            if (!page.IsCallback)
                return false;
            var __callbackParam = page.Request.Params["__CALLBACKPARAM"];
            return __callbackParam.ContainsAnyIgnoreCase(callbackParam);
        }

        /// <summary>
        ///tregon nese grida ka ndryshim filtri periudhe ose top rows
        /// ne kete rast grida duhet te rimarri vlerat nga db
        /// </summary>
        /// <param name="page"></param>
        /// <param name="gridaID"></param>
        /// <returns></returns>
        public static bool NdryshimFiltriPeriudhaGrida(this Page page, string gridaID)
        {
            if (!page.IsCallback)
                return false;
            var callbackParam = page.Request.Params["__CALLBACKPARAM"];
            return EshteCallbackuIm(page, gridaID) && callbackParam.ContainsAnyIgnoreCase(TitlePeriudha.KeyParamNdryshimPeriudhe, TopRowsControl.KeyParamNdryshimTopRows);
        }
        /// <summary>
        /// tregon nese grida ka bere nje callback per te aplikuar nje filter te ri mbi datasource-in e saj,
        /// perdoret per te rimarr te dhenat nga db nese ka ndodhur nje ndryshim ne filter
        /// </summary>
        /// <param name="page"></param>
        /// <param name="gridaID"></param>
        /// <returns></returns>
        public static bool NdryshimFiltriGrida(this Page page, string gridaID)
        {
            if (!page.IsCallback)
                return false;
            var callbackParam = page.Request.Params["__CALLBACKPARAM"];
            if (string.IsNullOrEmpty(callbackParam)) return false;
            return EshteCallbackuIm(page, gridaID) && callbackParam.ContainsAnyIgnoreCase(TitlePeriudha.KeyParamNdryshimPeriudhe,
                                                                                    TopRowsControl.KeyParamNdryshimTopRows,
                                                                                    GridUtil.CallbackParamFilterGrida,
                                                                                    GridUtil.CallbackParamApplyColumnFilter,
                                                                                    GridUtil.CallbackParamApplyFilter,
                                                                                    GridUtil.CallbackParamFilterEnabled);

        }



        public static TitlePeriudha MerrPeriudhe(this Page page, ASPxHiddenField hfState)
        {
            object periudhat;
            return hfState.TryGet(TitlePeriudha.KeyFieldPeriudha, out periudhat) ? JsonConvert.DeserializeObject<TitlePeriudha>(periudhat.ToString()) : null;
        }
        public static TopRowsControl MerrTopRowsControl(this Page page, ASPxHiddenField hfState)
        {
            return hfState.TryGet(TopRowsControl.KeyFieldTopRows, out object topRowsControl) ? JsonConvert.DeserializeObject<TopRowsControl>(topRowsControl.ToString()) : null;
        }

        public static string GetCurrentPageReportViewer(this Page page, string reportViewerId)
        {
            var currentPage = page.Request.Form[reportViewerId];
            return (string)JsonConvert.DeserializeObject<dynamic>(currentPage.Replace("&quot;", ""))?.currentPageIndex;
        }

    }
}