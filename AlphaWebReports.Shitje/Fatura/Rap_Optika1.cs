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
    public partial class Rap_Optika1 : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_Optika1(){InitializeComponent();} 
       
     

        public Rap_Optika1(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_Optika1(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
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
            xrLabel14.Text = rm.GetString("lblAdresaOp1", ci);
            xrLabel1.Text = rm.GetString("lblTitullOp1", ci);
            xrLabel6.Text = rm.GetString("lblEmerMbiemerOp1", ci);
            xrLabel15.Text = rm.GetString("lblMoshaOp1", ci);
            xrLabel9.Text = rm.GetString("lblAdresaOp", ci);
            xrLabel16.Text = rm.GetString("lblTelOp1", ci);
            xrLabel8.Text = rm.GetString("lblShenimeOp1", ci);
            xrLabel3.Text = rm.GetString("lblDistancaOp1", ci);
            xrLabel30.Text = rm.GetString("lblDrOp1", ci);
            xrTableCell8.Text = rm.GetString("lblSFOp1", ci );
            xrTableCell9.Text = rm.GetString("lblSFOp1", ci);
            xrTableCell2.Text = rm.GetString("lblSFOp1", ci);
            xrTableCell4.Text = rm.GetString("lblSFOp1", ci);
            xrTableCell1.Text = rm.GetString("lblCILOp1", ci);
            xrTableCell14.Text = rm.GetString("lblCILOp1", ci);
            xrTableCell5.Text = rm.GetString("lblCILOp1", ci);
            xrTableCell10.Text = rm.GetString("lblCILOp1", ci);
            xrTableCell12.Text = rm.GetString("lblAXOp1", ci);
            xrTableCell3.Text = rm.GetString("lblAXOp1", ci);
            xrTableCell6.Text = rm.GetString("lblAXOp1", ci);
            xrTableCell11.Text = rm.GetString("lblAXOp1", ci);
            xrTableCell43.Text = rm.GetString("lblDtOp1", ci);
            xrLabel13.Text = rm.GetString("lblAdresaFBOp1", ci);

        }
}
}
