
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Web.UI;
using System.Web.UI.WebControls;
using DbCore;
using DbCore.DbShare;
using DbCore.IMBUtils.Fiskalizimi.Controls;
using DevExpress.Web;
using Newtonsoft.Json;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Filters;

namespace PlatinumWeb.Templates
{
    public class MyTitlePanelRegjistrimDokumentash : MyTitlePanelMePeriudheDheComboTop
    {
        private ASPxButton _btnProcedimProdhimi;
        private ASPxButton _btnPorosiDealer;
        private ASPxButton _btnOfertaBlerje;
        private ASPxButton _btnAparateShitur;
        private ASPxButton _btnKartaShitur;
        private ASPxButton _btnRingarkuesShitur;
        private ASPxButton _btnShitjeLikujdimePermbledhese;
        private ASPxButton _btnShitjeEinvoce;
        private ASPxButton _btnBlerjeEinvoce;
        private ASPxButton _btnFatureFiskalizimi;

        protected ASPxButton BtnProcedimProdhimi => _btnProcedimProdhimi;
        protected ASPxButton BtnPorosiDealer => _btnPorosiDealer;
        protected ASPxButton BtnOfertaBlerje => _btnOfertaBlerje;
        protected ASPxButton BtnAparateShitur => _btnAparateShitur;
        protected ASPxButton BtnKartaShitur => _btnKartaShitur;
        protected ASPxButton BtnRingarkuesShitur => _btnRingarkuesShitur;
        protected ASPxButton BtnShitjeLikujdimePermbledhese => _btnShitjeLikujdimePermbledhese;
        protected ASPxButton BtnShitjeEinvoce => _btnShitjeEinvoce;
        protected ASPxButton BtnBlerjeEinvoice => _btnBlerjeEinvoce;
        protected ASPxButton BtnFatureFiskalizimi => _btnFatureFiskalizimi;




        public MyTitlePanelRegjistrimDokumentash(string emriGrides, Page page, ASPxMenu menuMesazhesh, UpdatePanel pnlMesazhi, ASPxHiddenField hfState, int idkonfigAmbjenti, int idPerdoruesi, int idNdermarrje, int idViti, int idGjuha, string komponente, int idKomponente, string renditjeDefault, ResourceManager rm, CultureInfo cultureInfo, bool meExpandCollapse, GridViewExportedRowType exportType, bool meComboAutomatike) : base(emriGrides, page, menuMesazhesh, pnlMesazhi, hfState, idkonfigAmbjenti, idPerdoruesi, idNdermarrje, idViti, idGjuha, komponente, idKomponente, renditjeDefault, rm, cultureInfo, meExpandCollapse, exportType, meComboAutomatike, false)
        {
            ////nese menyra e filtrimit eshte 0 i ath nuk ka nje konfigurim te percaktuar
            //if (gridaKoka.MenyreFiltrimi == 0)
            //    _filterRowMode = GridViewFilterRowMode.Auto;
            //else
            //    _filterRowMode = (GridViewFilterRowMode)gridaKoka.MenyreFiltrimi;
        }

        protected override List<Control> KontrolletEPeriudhes(bool raiseValueChanged = true)
        {
            var periudhaControls = new List<Control>();
            InicializoFilterDefault();
            InicializoLblPeriudha();
            InicializoRadioDtDok(raiseValueChanged, false);
            InicializoButonaShitje();
            if(clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizim())
                periudhaControls.AddRange(new Control[] { _btnProcedimProdhimi, _btnPorosiDealer, _btnOfertaBlerje, _btnAparateShitur, _btnKartaShitur, _btnRingarkuesShitur, _btnShitjeLikujdimePermbledhese, BtnFilterDefault, _btnFatureFiskalizimi, _btnShitjeEinvoce, _btnBlerjeEinvoce, new TableCell(), LblPeriudha, RadDtDok});
            else
                periudhaControls.AddRange(new Control[] { _btnProcedimProdhimi, _btnPorosiDealer, _btnOfertaBlerje, _btnAparateShitur, _btnKartaShitur, _btnRingarkuesShitur, _btnShitjeLikujdimePermbledhese, BtnFilterDefault, new TableCell(), LblPeriudha, RadDtDok });
            return periudhaControls;
        }

        private void InicializoButonaShitje()
        {
            //TODO handlerat
            _btnProcedimProdhimi = new ASPxButton();
            _btnProcedimProdhimi.ID = "konvertimetBtn";
            _btnProcedimProdhimi.ClientInstanceName = "konvertimetBtn";
            _btnProcedimProdhimi.ToolTip = rm.GetString("btnProcedimProdhimi", cultinf);
            _btnProcedimProdhimi.AutoPostBack = false;
            _btnProcedimProdhimi.Image.Url = $"images/theme/{CurrentPage.Theme}/grida/procedim_prodhimi.png";
            _btnProcedimProdhimi.Image.UrlHottracked = $"images/theme/{CurrentPage.Theme}/grida/procedim_prodhimi_W.png";
            _btnProcedimProdhimi.Font.Size = 8;
            _btnProcedimProdhimi.Image.Height = 16;
            _btnProcedimProdhimi.ClientSideEvents.Init = "function(s,e){myFaqeCelje.shfaqButonRaportPorosiDealerdheProcedimProdhimi(s, e, 'procedimProdhimi')}";
            _btnProcedimProdhimi.ClientSideEvents.Click = $"function(s,e){{myFaqeCelje.hapRaporti(s,e,{Grid.ClientInstanceName})}}";

            _btnPorosiDealer = new ASPxButton();
            _btnPorosiDealer.ID = "pagesaBtn";
            _btnPorosiDealer.ClientInstanceName = "pagesaBtn";
            _btnPorosiDealer.ToolTip = rm.GetString("btnPorosiDealer", cultinf);
            _btnPorosiDealer.AutoPostBack = false;
            _btnPorosiDealer.Image.Url = $"images/theme/{CurrentPage.Theme}/grida/porosi_dealer.png";
            _btnPorosiDealer.Image.UrlHottracked = $"images/theme/{CurrentPage.Theme}/grida/porosi_dealer_W.png";
            _btnPorosiDealer.Font.Size = 8;
            _btnPorosiDealer.Image.Height = 16;
            _btnPorosiDealer.ClientSideEvents.Init = "function(s,e){myFaqeCelje.shfaqButonRaportPorosiDealerdheProcedimProdhimi(s, e, 'porosiDealerVodafone')}";
            _btnPorosiDealer.ClientSideEvents.Click = $"function(s,e){{myFaqeCelje.hapRaportinPagesa(s,e,{Grid.ClientInstanceName})}}";

            _btnOfertaBlerje = new ASPxButton();
            _btnOfertaBlerje.ID = "ofertaBlerjeBtn";
            _btnOfertaBlerje.ClientInstanceName = "ofertaBlerjeBtn";
            _btnOfertaBlerje.ToolTip = rm.GetString("btnOfertaBlerje", cultinf);
            _btnOfertaBlerje.AutoPostBack = false;
            _btnOfertaBlerje.Image.Url = $"images/theme/{CurrentPage.Theme}/grida/ofertaBlerje.png";
            _btnOfertaBlerje.Image.UrlHottracked = $"images/theme/{CurrentPage.Theme}/grida/ofertaBlerje_W.png";
            _btnOfertaBlerje.Font.Size = 8;
            _btnOfertaBlerje.Image.Height = 16;
            _btnOfertaBlerje.ClientSideEvents.Init = "function(s,e){myFaqeCelje.shfaqButonRaportOfertaBlerje(s, e, 'ofertBlerje')}";
            _btnOfertaBlerje.ClientSideEvents.Click = $"function(s,e){{myFaqeCelje.hapRaportinOfertaBlerje(s,e)}}";

            _btnAparateShitur = new ASPxButton();
            _btnAparateShitur.ID = "aparateTeShiturBtn";
            _btnAparateShitur.ClientInstanceName = "aparateTeShiturBtn";
            _btnAparateShitur.ToolTip = rm.GetString("btnAparateShitur", cultinf);
            _btnAparateShitur.AutoPostBack = false;
            _btnAparateShitur.Image.Url = $"images/theme/{CurrentPage.Theme}/grida/aparateShitur.png";
            _btnAparateShitur.Image.UrlHottracked = $"images/theme/{CurrentPage.Theme}/grida/aparateShitur_W.png";
            _btnAparateShitur.Font.Size = 8;
            _btnAparateShitur.Image.Height = 16;
            _btnAparateShitur.ClientSideEvents.Init = "function(s,e){myFaqeCelje.shfaqButonRaporti(s, e, 'AparateTeShitur')}";
            _btnAparateShitur.ClientSideEvents.Click = $"function(s,e){{myFaqeCelje.hapRaportin(s,e,'AparateTeShitur')}}";

            _btnKartaShitur = new ASPxButton();
            _btnKartaShitur.ID = "kartaTeShituraBtn";
            _btnKartaShitur.ClientInstanceName = "kartaTeShituraBtn";
            _btnKartaShitur.ToolTip = rm.GetString("btnKartaShitur", cultinf);
            _btnKartaShitur.AutoPostBack = false;
            _btnKartaShitur.Image.Url = $"images/theme/{CurrentPage.Theme}/grida/kartaShitur.png";
            _btnKartaShitur.Image.UrlHottracked = $"images/theme/{CurrentPage.Theme}/grida/kartaShitur_W.png";
            _btnKartaShitur.Font.Size = 8;
            _btnKartaShitur.Image.Height = 16;
            _btnKartaShitur.ClientSideEvents.Init = "function(s,e){myFaqeCelje.shfaqButonRaporti(s, e, 'KartaTeShitura')}";
            _btnKartaShitur.ClientSideEvents.Click = $"function(s,e){{myFaqeCelje.hapRaportin(s, e, 'KartaTeShitura')}}";

            _btnRingarkuesShitur = new ASPxButton();
            _btnRingarkuesShitur.ID = "ringarkuesTeShiturBtn";
            _btnRingarkuesShitur.ClientInstanceName = "ringarkuesTeShiturBtn";
            _btnRingarkuesShitur.ToolTip = rm.GetString("btnRingarkuesShitur", cultinf);
            _btnRingarkuesShitur.AutoPostBack = false;
            _btnRingarkuesShitur.Image.Url = $"images/theme/{CurrentPage.Theme}/grida/ringarkuesShitur.png";
            _btnRingarkuesShitur.Image.UrlHottracked = $"images/theme/{CurrentPage.Theme}/grida/ringarkuesShitur_W.png";
            _btnRingarkuesShitur.Font.Size = 8;
            _btnRingarkuesShitur.Image.Height = 16;
            _btnRingarkuesShitur.ClientSideEvents.Init = "function(s,e){myFaqeCelje.shfaqButonRaporti(s, e, 'RingarkuesTeShitur')}";
            _btnRingarkuesShitur.ClientSideEvents.Click = $"function(s,e){{myFaqeCelje.hapRaportin(s, e, 'RingarkuesTeShitur')}}";

            _btnShitjeLikujdimePermbledhese = new ASPxButton();
            _btnShitjeLikujdimePermbledhese.ID = "shitjeLikujdimePermbledheseBtn";
            _btnShitjeLikujdimePermbledhese.ClientInstanceName = "shitjeLikujdimePermbledheseBtn";
            _btnShitjeLikujdimePermbledhese.ToolTip = rm.GetString("btnShitjeLikujdimePermbledhese", cultinf);
            _btnShitjeLikujdimePermbledhese.AutoPostBack = false;
            _btnShitjeLikujdimePermbledhese.Image.Url = $"images/theme/{CurrentPage.Theme}/grida/shitjeLikujdimePermbledhese.png";
            _btnShitjeLikujdimePermbledhese.Image.UrlHottracked = $"images/theme/{CurrentPage.Theme}/grida/shitjeLikujdimePermbledhese_W.png";
            _btnShitjeLikujdimePermbledhese.Font.Size = 8;
            _btnShitjeLikujdimePermbledhese.Image.Height = 16;
            _btnShitjeLikujdimePermbledhese.ClientSideEvents.Init = "function(s,e){myFaqeCelje.shfaqButonRaporti(s, e, 'ShitjeDheLikujdimePermbledhese')}";
            _btnShitjeLikujdimePermbledhese.ClientSideEvents.Click = $"function(s,e){{myFaqeCelje.hapRaportinShitjeLikujdime(s, e)}}";


            _btnShitjeEinvoce = new ASPxButton();
            _btnShitjeEinvoce.ID = "shitjeEinvoceBtn";
            _btnShitjeEinvoce.ClientInstanceName = "shitjeEinvoceBtn";
            _btnShitjeEinvoce.ToolTip = "Shitje Einvoice";
            _btnShitjeEinvoce.AutoPostBack = false;
            _btnShitjeEinvoce.Image.Url = $"images/theme/{CurrentPage.Theme}/grida/ofertaBlerje.png";
            _btnShitjeEinvoce.Image.UrlHottracked = $"images/theme/{CurrentPage.Theme}/grida/ofertaBlerje_W.png";
            _btnShitjeEinvoce.Font.Size = 8;
            _btnShitjeEinvoce.Image.Height = 16;
            _btnShitjeEinvoce.ClientSideEvents.Init = "function(s,e){myFaqeCelje.shfaqButonRaporti(s, e, 'FaturaShitjeEinvoice')}";
            _btnShitjeEinvoce.ClientSideEvents.Click = $"function(s,e){{myFaqeCelje.hapRaportinShitjeEinvoice(s,e)}}";

            _btnBlerjeEinvoce = new ASPxButton();
            _btnBlerjeEinvoce.ID = "blerjeEinvoceBtn";
            _btnBlerjeEinvoce.ClientInstanceName = "blerjeEinvoceBtn";
            _btnBlerjeEinvoce.ToolTip = "Blerje Einvoice";
            _btnBlerjeEinvoce.AutoPostBack = false;
            _btnBlerjeEinvoce.Image.Url = $"images/theme/{CurrentPage.Theme}/grida/ofertaBlerje.png";
            _btnBlerjeEinvoce.Image.UrlHottracked = $"images/theme/{CurrentPage.Theme}/grida/ofertaBlerje_W.png";
            _btnBlerjeEinvoce.Font.Size = 8;
            _btnBlerjeEinvoce.Image.Height = 16;
            _btnBlerjeEinvoce.ClientSideEvents.Init = "function(s,e){myFaqeCelje.shfaqButonRaportiBlerje(s, e, 'FaturaBlerjeEinvoice')}";
            _btnBlerjeEinvoce.ClientSideEvents.Click = $"function(s,e){{myFaqeCelje.hapRaportinBlerjeEinvoice(s,e)}}";
            
            _btnFatureFiskalizimi = new ASPxButton();
            _btnFatureFiskalizimi.ID = "fatureFiskalizimiBtn";
            _btnFatureFiskalizimi.ClientInstanceName = "fatureFiskalizimiBtn";
            _btnFatureFiskalizimi.ToolTip = "Fature Fiskalizimi";
            _btnFatureFiskalizimi.AutoPostBack = false;
            _btnFatureFiskalizimi.Image.Url = $"images/theme/{CurrentPage.Theme}/grida/ofertaBlerje.png";
            _btnFatureFiskalizimi.Image.UrlHottracked = $"images/theme/{CurrentPage.Theme}/grida/ofertaBlerje_W.png";
            _btnFatureFiskalizimi.Font.Size = 8;
            _btnFatureFiskalizimi.Image.Height = 16;
            _btnFatureFiskalizimi.ClientSideEvents.Init = "function(s,e){myFaqeCelje.shfaqButonFiskalizimi(s, e)}";
            _btnFatureFiskalizimi.ClientSideEvents.Click = $"function(s,e){{myFaqeCelje.hapFaturenEFiskalizuar(s,e)}}";

        }

        public override void InstantiateIn(Control container)
        {
            Grid = ((DevExpress.Web.GridViewTitleTemplateContainer)container).Grid;
            var tbl = KrijoTable(1, 23);
            MbushTableMeControle(tbl, BaseControls(), KontrolletEPeriudhes(Periudhat.TopRowsControl.MenyreFiltrimi == 0));
         
            container.Controls.Add(tbl);
        }
    

    }
}