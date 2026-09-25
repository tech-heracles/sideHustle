using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Data;
using System.Resources;
using System.Globalization;
using DbCore.DbAdmin;
using System.IO;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.DbInventari;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;

namespace PlatinumWeb
{
    public partial class RivleresimMagazine : MyPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ////Response.Cache.SetCacheability(HttpCacheability.NoCache);
            ////if (CacheLayer.GlobalCacheManager.MySessionCache["LoggedIn"].Equals("No"))

            ////if (CacheLayer.GlobalCacheManager.MySessionCache["KodiNdermarrjes"] == null)
            ////if (CacheLayer.GlobalCacheManager.MySessionCache["oPeriudhaAktuale"] != null)
        } 


        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        /// <param name="idViti"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNermarrje"></param>

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Percakton veprimin qe kryhet kur klikohet nje nga butonat e menuse
        /// </summary>
        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {
        }

        /// <summary>
        /// Vendos vlerat default kur po behet shtim
        /// </summary>
        /// <param name="idNdermarrje"></param>
        private void konfiguroVleraFillestare(int idNdermarrje, DbCore.DbAdmin.clsPeriudhaKontabel periudha)
        {

            //// DbCore.clsFunksione.percaktoTemplateCombo(cmbArtikuj);
            //// funk.mbushComboArtikulli(cmbArtikuj);
        }

        private void konfiguroGride()
        {
        }
        //        //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
        //        //perzgjidh
        //        //behet per te afishuar rreshtin qe do sherbej per filtrim


        protected void gvRivleresim2_DataBound(object sender, EventArgs e)
        {
            //    //behet nepermjet kodit afishimi i checkboxit qe do perdoret per 
            //    //perzgjidh
            //    //behet per te afishuar rreshtin qe do sherbej per filtrim


        }

        //private bool zgjidhMbrapa(object sender, ASPxGridViewCustomCallbackEventArgs e) {
             
        //private void mbushListaCallback()

            
       // protected void btnDjathtas1_Click(object sender, EventArgs e)
              

       //         //if (drs.Length > 1)
       //         //{
       //         //}
               

       // protected void btnDjathtasGjitha_Click(object sender, EventArgs e)


       // protected void btnMajtas1_Click(object sender, EventArgs e)


       //         //if (drs.Length > 1)
       //         //{
       //         //}
                

       // protected void btnMajtaGjitha_Click(object sender, EventArgs e)

       //     DateTimeOffset timezoneIShqiperise = TimeZoneInfo.ConvertTime(DateTime.Now,


       //     else
       //     try

       //         try
       //             try
       //                         position++;


       //         try
       //             try
                       

       //     try
       //         try
       //                 //for (int j = 0; j < magazinat.Count; j++)
       //                 //{
       //                 position++;
                        
       //                 // }


       // private void kaloArtikujtSiper(int i, DataTable artikujPerRivleresim, DataTable artikujPerTuZgjedhur)
    }
}