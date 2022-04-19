using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.Magazina.Format_Printimi
{
    public partial class Rap_FatureShoqerimi_Vodafone_Aparate : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FatureShoqerimi_Vodafone_Aparate(){InitializeComponent();} 
        public Rap_FatureShoqerimi_Vodafone_Aparate(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FatureShoqerimi_Vodafone_Aparate(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
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
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel1.Text = rm.GetString("RaportFatureShoqerimiVodafone", ci);
            xrLabel44.Text = rm.GetString("RptFatShoqVodafoneNrSerial", ci);
            xrLabel16.Text = rm.GetString("RptFatShoqVodafoneNrFletDalje", ci);
            xrLabel50.Text = rm.GetString("RptFatShoqVodafoneEmriTatueshem", ci);
            xrLabel7.Text = rm.GetString("RptFatShoqVodafoneData", ci);
            xrLabel12.Text = rm.GetString("RptFatShoqVodafoneNipt", ci);
            xrLabel13.Text = rm.GetString("RptFatShoqVodafoneAdresaPersonit", ci);
            xrLabel14.Text = rm.GetString("RptFatShoqVodafoneTel", ci);
            xrLabel2.Text = rm.GetString("RptFatShoqVodafoneNrSerialNipt", ci);
            xrLabel20.Text = rm.GetString("RptFatShoqVodafoneNrSerialNipt", ci);


            xrLabel4.Text = rm.GetString("RptFatShoqVodafoneAdresaNisjes", ci);
            xrLabel5.Text = rm.GetString("RptFatShoqVodafoneAdresaMberritjes", ci);
            xrLabel8.Text = rm.GetString("RptFatShoqVodafoneDataNisjes", ci);
            xrLabel11.Text = rm.GetString("RptFatShoqVodafoneOraNisjes", ci);
            xrLabel15.Text = rm.GetString("RptFatShoqVodafoneTargaMjetit", ci);
            xrLabel29.Text = rm.GetString("RptFatShoqVodafoneLevizjeMalliNga", ci);
            xrLabel34.Text = rm.GetString("RptFatShoqVodafonePerNe", ci);

            xrLabel38.Text = rm.GetString("RptFatShoqVodafonePerNeMagazine", ci);
            xrCheckBox10.Text = rm.GetString("RptFatShoqVodafonePerNeMagazine", ci);

            xrCheckBox5.Text = rm.GetString("RptFatShoqVodafonePerNeEkspozite", ci);
            xrCheckBox6.Text = rm.GetString("RptFatShoqVodafonePerNeEkspozite", ci);

            xrCheckBox4.Text = rm.GetString("RptFatShoqVodafonePerNeDyqan", ci);
            xrLabel40.Text = rm.GetString("RptFatShoqVodafonePerNeDyqan", ci);

            xrCheckBox3.Text = rm.GetString("RptFatShoqVodafonePerNePikeShitje", ci);
            xrCheckBox8.Text = rm.GetString("RptFatShoqVodafonePerNePikeShitje", ci);

            xrCheckBox2.Text = rm.GetString("RptFatShoqVodafoneperNeTjera", ci);
            xrCheckBox9.Text = rm.GetString("RptFatShoqVodafoneperNeTjera", ci);

            xrLabel28.Text = rm.GetString("RptFatShoqVodafoneTransportMeMjetin", ci);

            xrLabel42.Text = rm.GetString("RptFatShoqVodafoneEVet", ci);
            xrCheckBox12.Text = rm.GetString("RptFatShoqVodafoneETeTreteve", ci);

            xrTableCell4.Text = rm.GetString("RptFatShoqVodafoneNr", ci);
            xrTableCell5.Text = rm.GetString("RptFatShoqVodafonePershkrimMalli", ci);
            xrTableCell7.Text = rm.GetString("RptFatShoqVodafoneNjesia", ci);
            xrTableCell9.Text = rm.GetString("RptFatShoqVodafoneSasia", ci);
            xrTableCell8.Text = rm.GetString("RptFatShoqVodafoneVlera", ci);

            xrLabel22.Text = rm.GetString("RptFatShoqVodafoneLlojiAmballazhit", ci);
            xrLabel23.Text = rm.GetString("RptFatShoqVodafoneNrKoli", ci);
            xrLabel31.Text = rm.GetString("RptFatShoqVodafoneTranspTeTrete", ci);
            xrLabel26.Text = rm.GetString("RptFatShoqVodafoneEmriShoqerise", ci);
            xrLabel27.Text = rm.GetString("RptFatShoqVodafoneAdresa", ci);
            xrLabel30.Text = rm.GetString("RptFatShoqVodafoneNipt2", ci);

            xrLabel64.Text = rm.GetString("RptFatShoqVodafoneNisesi", ci);
            xrLabel63.Text = rm.GetString("RptFatShoqVodafoneTransportuesi", ci);
            xrLabel62.Text = rm.GetString("RptFatShoqVodafonePritesi", ci);
            xrLabel35.Text = rm.GetString("RptFatShoqVodafoneEmerMbiemerNenShkrim", ci);
            xrLabel33.Text = rm.GetString("RptFatShoqVodafoneEmerMbiemerNenShkrim", ci);
            xrLabel32.Text = rm.GetString("RptFatShoqVodafoneEmerMbiemerNenShkrim", ci);


        }
    }
}
