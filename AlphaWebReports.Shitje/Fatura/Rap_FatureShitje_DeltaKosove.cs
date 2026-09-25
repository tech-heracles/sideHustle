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
    public partial class Rap_FatureShitje_DeltaKosove : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FatureShitje_DeltaKosove(){InitializeComponent();} 

       
        public Rap_FatureShitje_DeltaKosove(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FatureShitje_DeltaKosove(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
            EmrateLabelave(ci);
        }

        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));
        ////    xrLabel19.Text = rm.GetString("lblRaportTarga", ci);
        ////    xrLabel24.Text = rm.GetString("lblRaportOraFurnizimit", ci);

            //xrLabel28.Text = rm.GetString("lblNrLlogarieAlPetrol1", ci);\
            xrLabel45.Text= rm.GetString("lblVleraSelia", ci);
            xrLabel38.Text = rm.GetString("lblSelia", ci);
            xrLabel39.Text= rm.GetString("lblRaportAdresa", ci);
            xrLabel57.Text= rm.GetString("lblVleraAdresa", ci);
            xrLabel40.Text = rm.GetString("lblVleraLlogBank", ci);
            xrLabel59.Text= rm.GetString("lblVleraNRB", ci);
            xrLabel60.Text= rm.GetString("lblVleraTVSh", ci);
            xrLabel61.Text = rm.GetString("lblNrFiskal", ci);
            xrLabel28.Text = rm.GetString("lblDeltaKosoveProCredit", ci);
            xrLabel30.Text = rm.GetString("lblDeltaKsRaiffeisen", ci);
        }
    }
}
