using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Collections.Generic;
using System.Data;
namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_FormatShitjeAlpetrolA5_2 : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FormatShitjeAlpetrolA5_2(){InitializeComponent();} 
        public Rap_FormatShitjeAlpetrolA5_2(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FormatShitjeAlpetrolA5_2(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
            EmrateLabelave(ci);
        }
        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));
            
            xrLabel12.Text = rm.GetString("labelphoneAlpetrol", ci);
            xrLabel11.Text = rm.GetString("lblAlPetrolPhone", ci);
            xrLabel10.Text = rm.GetString("lblAlpetrolemail", ci);
            xrLabel9.Text = rm.GetString("label_Klienti", ci);
            xrLabel4.Text = rm.GetString("labelNrFiskal", ci)+":";
            xrLabel5.Text = rm.GetString("lblAlPetrolReportHeader2", ci);
            xrLabel6.Text = rm.GetString("lblalPetrolWeb", ci);
            xrLabel16.Text = rm.GetString("labelRaportiMysafiri", ci) + ":";
            xrLabel15.Text = rm.GetString("labelRaportiSHOFERI", ci) + ":";
            xrLabel20.Text = rm.GetString("labelNrIdentifikues", ci) + ":";
            xrLabel22.Text = rm.GetString("labelRaportNumriFatures", ci) + ":";
            xrLabel21.Text = rm.GetString("labelRaportiDateFaturimi", ci) + ":";
            xrLabel24.Text = rm.GetString("labelAfatiPageses", ci) + ":";
            xrLabel42.Text = rm.GetString("labelRaportiAddAlpetrol", ci);
            xrLabel6.Text = rm.GetString("lblalPetrolWeb", ci);
            xrLabel38.Text = rm.GetString("lblAlPetrolMob", ci);
            xrLabel37.Text = rm.GetString("lblAlPetrolMob1", ci);
            xrLabel36.Text = rm.GetString("lblAlpetrolFax", ci);
            xrLabel24.Text = rm.GetString("lblAlPetrolWeb1", ci);
            xrLabel14.Text = rm.GetString("lblAlPEtrolemail1", ci);
            xrLabel48.Text = rm.GetString("lblAlPetrolPhone", ci);
            xrLabel47.Text = rm.GetString("lblAlpetrolemail", ci);
            xrLabel62.Text = rm.GetString("label_Klienti", ci);
            xrLabel58.Text = rm.GetString("labelNrFiskal", ci) + ":";
            xrLabel45.Text = rm.GetString("lblAlPetrolReportHeader2", ci);
            xrLabel46.Text = rm.GetString("lblalPetrolWeb", ci);
            xrLabel64.Text = rm.GetString("labelRaportiMysafiri", ci) + ":";
            xrLabel65.Text = rm.GetString("labelRaportiSHOFERI", ci) + ":";
            xrLabel69.Text = rm.GetString("labelNrIdentifikues", ci) + ":";
            xrLabel71.Text = rm.GetString("labelRaportNumriFatures", ci) + ":";
            xrLabel70.Text = rm.GetString("labelRaportiDateFaturimi", ci) + ":";
            xrLabel44.Text = rm.GetString("labelRaportiAddAlpetrol", ci);
            xrLabel55.Text = rm.GetString("lblAlPetrolMob", ci);
            xrLabel37.Text = rm.GetString("lblAlPetrolMob1", ci);
            xrLabel53.Text = rm.GetString("lblAlpetrolFax", ci);
            xrLabel51.Text = rm.GetString("lblAlPetrolWeb1", ci);
            xrLabel50.Text = rm.GetString("lblAlPEtrolemail1", ci);
            xrLabel91.Text = rm.GetString("labelRaportiNrLLogarieFixAlpetrol", ci);
            xrLabel90.Text = rm.GetString("labelRaportNrLlogarise", ci) + ":";
            xrLabel89.Text = rm.GetString("labelRaportiNrLLogarieFixAlpetrol", ci);
            xrLabel88.Text = rm.GetString("labelRaportNrLlogarise", ci) + ":";
            xrLabel1.Text = rm.GetString("labelRaportiFaturuarNga", ci) + ":";
            xrLabel60.Text = rm.GetString("labelRaportiFaturuarNga", ci) + ":";
        }
        
    }
}
