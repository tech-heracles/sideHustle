using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_FormatShitje_RedisKosove : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FormatShitje_RedisKosove(){InitializeComponent();} 

public Rap_FormatShitje_RedisKosove(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):this(param.Ci,param.IdNdermarrje,param.IdPerdoruesi){}
        public Rap_FormatShitje_RedisKosove(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
        } 



        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel1.Text = rm.GetString("RaportFatureShitjeTitulli", ci);
           
            xrLabel80.Text = rm.GetString("lblNumerFiskal" , ci);
            xrLabel2.Text = rm.GetString("labelRaportNumriFatures", ci);
            xrLabel4.Text = rm.GetString("labelRaportDataFatures", ci);
       
            xrLabel17.Text = rm.GetString("labelRaportSubjektiBleres", ci);
            xrLabel18.Text = rm.GetString("labelRaportAdresa", ci);
            xrLabel61.Text = rm.GetString("labelRaportAdresa", ci);
            xrLabel20.Text = rm.GetString("lblNumerbiznesi", ci);
            xrLabel62.Text = rm.GetString("labelRaportNipt", ci);
            xrLabel21.Text = rm.GetString("labelRaportTel", ci);
            xrLabel22.Text = rm.GetString("labelWebPage", ci);
            xrLabel63.Text = rm.GetString("lblRaportTarga", ci);
            xrLabel64.Text = rm.GetString("lblRaportOraFurnizimit", ci);
            xrLabel70.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel72.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel53.Text = rm.GetString("labelBleresi", ci);
            xrLabel51.Text = rm.GetString("labelShitesi", ci);
            xrLabel26.Text = rm.GetString("RedisKosove_BKT", ci);
            xrLabel19.Text = rm.GetString("RKosoveDetaje", ci);
            xrLabel25.Text = rm.GetString("RedisKosoveShenim", ci);
            xrTableCell4.Text = rm.GetString("labelRaportPershkrimi", ci);
            xrTableCell21.Text = rm.GetString("labelRaportOrigjina", ci);
            xrTableCell5.Text = rm.GetString("lblNjesi1", ci);
            xrTableCell7.Text = rm.GetString("lblNjesi2", ci);
            xrTableCell10.Text = rm.GetString("labelCmimi", ci);
            xrTableCell17.Text = rm.GetString("labelZbritje", ci);
            xrTableCell8.Text = rm.GetString("labelVleraPaTVSH", ci);
            xrTableCell11.Text = rm.GetString("labelRaportiVatIdentifier", ci);
            xrTableCell6.Text = rm.GetString("labelVleraMeTVSH", ci);
            

        }
    }
}
