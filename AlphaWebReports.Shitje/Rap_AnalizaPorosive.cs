using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_AnalizaPorosive : DevExpress.XtraReports.UI.XtraReport
    {        
		public Rap_AnalizaPorosive(){InitializeComponent();} 
       
       public Rap_AnalizaPorosive(AlphaWebReports.Common.ParametraRaporti param, XtraReport report)
        {
            InitializeComponent();
            EmrateLabelave(param.Ci);
        }
      

     
        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {

            ResourceManager rm = new ResourceManager("Resources.Strings",
                        System.Reflection.Assembly.Load("App_GlobalResources"));
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            KodiArtKokaR.Text =rm.GetString("lblKodiArt", ci);
            PershkrimiKokaR.Text = rm.GetString("cmbCmimeArtikulliCaptionPershkrimi", ci);
            FurKryeKokaR.Text =  rm.GetString("lblFurnKryesor", ci);
            Grup1KokaR.Text = rm.GetString("labelGrupimKlientPare", ci);
            Njesia1KokaR.Text =rm.GetString("lblNjesia1", ci);
            Njesia2KokaR.Text = rm.GetString("lblNjesia2", ci);
            KoefKokaR.Text = rm.GetString("lblKoef", ci);
            SasiBlKokaR.Text =  rm.GetString("lblSasiaBlerje", ci);
            SasiShKokaR.Text = rm.GetString("lblSasiashitje", ci);
            GjendjeKokaR.Text = rm.GetString("filterGjendjeDetyrime",ci);
            KostoKokaR.Text = rm.GetString("koloneKosto", ci);
            VleftaKokaR.Text =  rm.GetString("labelRaportVlefta", ci);
            CmimLirBlKokaR.Text =  rm.GetString("lblcmMireBlerjes", ci);
            CmimFunBlKokaR.Text = rm.GetString("lblcfmFunditBlerjes",ci);
            CmimShKokaR.Text = rm.GetString("lblcmShitje",ci);
            UrdherShKokaR.Text =  rm.GetString("lblSasiUSH", ci);
            MesShMKokaR.Text = rm.GetString("lblMesShitjeMuj", ci);
            SasiaShMFunKokaR.Text =  rm.GetString("lblSasShiturMuajFundit", ci);
            SasiaSh2MFunKokaR.Text = rm.GetString("lblSasShitur2MuajtFundit", ci);
            SasiaSh3MFunKokaR.Text =  rm.GetString("lblSasShitur3MuajtFundit", ci);
        }
    }
}
        
