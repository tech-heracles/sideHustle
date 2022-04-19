using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PlatinumWeb.Templates
{
    //TO DO : te rregullohet qe ikonat te vendosen sipas temes,dhe tooltipi sipas gjuhes
    public class MyBatchEditTitleTemplate : ITemplate
    {
        protected int idPerdoruesi;
        protected int idNdermarrje;
        protected int idViti;
        protected int idGjuha;
        protected int idKomponente;
        protected int idKonfig;
        protected bool meKonfigurimKolonash;

        protected string emerKomponente;
        protected ASPxMenu menuInfo;
        protected string renditjeDefault;
        protected UpdatePanel pnlMesazhi;
        protected ASPxHiddenField hfState;
        protected Page CurrentPage;
        protected ResourceManager rm;
        protected CultureInfo cultinf;

        private ScriptManager scriptManager;


        /// <summary>
        /// konstruktori i templatet
        /// </summary>
        /// <param name="menuMesazhesh">
        /// ASPxMenu-ja ne te cilen do shtohet mesazhi ne lidhje me ruajtjen e konfigurimit
        /// </param>
        /// <param name="pnlMesazhi">      UpdatePanel-i ne te cilin ndodhet menuja e mesazheve </param>
        /// <param name="cmbKonfig">
        /// ASPxComboBox i cili ka llojet e konfigurimit(do perdoret per ruajtjen e konfigurimeve)
        /// </param>
        /// <param name="idPerdoruesi">    idperdoruesi </param>
        /// <param name="idNdermarrje">    idNdermmarje </param>
        /// <param name="idViti">         </param>
        /// <param name="idGjuha">        </param>
        /// <param name="komponente">      emri i komponentes ku jemi </param>
        /// <param name="idKomponente">    id-ja e saj </param>
        /// <param name="renditjeDefault"> fusha sipas te ciles behet renditja default </param>
        /// <param name="page">
        /// Page,do perdoret per te shtuar kontrollet qe nuk mund te shtohen ne grid
        /// </param>
        public MyBatchEditTitleTemplate(Page page, ASPxGridView grid, ASPxMenu menuMesazhesh, UpdatePanel pnlMesazhi, ASPxHiddenField hfState, int idkonfigAmbjenti, int idPerdoruesi, int idNdermarrje, int idViti, int idGjuha, string komponente, int idKomponente, string renditjeDefault,bool meKonfigurimKolonash,  ResourceManager rm, CultureInfo cultinf)
        {
            this.menuInfo = menuMesazhesh;

            this.idKomponente = idKomponente;
            this.emerKomponente = komponente;
            this.renditjeDefault = renditjeDefault;
            this.pnlMesazhi = pnlMesazhi;
            this.idPerdoruesi = idPerdoruesi;
            this.idNdermarrje = idNdermarrje;
            this.idKonfig = idkonfigAmbjenti;
            this.idViti = idViti;
            this.idGjuha = idGjuha;
            this.CurrentPage = page;
            this.cultinf = cultinf;
            this.rm = rm;
            this.hfState = hfState;

            //hfState.Set("idKonfig", idKonfig);
            scriptManager = ScriptManager.GetCurrent(CurrentPage);
        }

        public ASPxGridView Grid { get; set; }

        protected System.Web.SessionState.HttpSessionState Session
        {
            get { return System.Web.HttpContext.Current.Session; }
        }

        public void InstantiateIn(Control Container)
        {
            Grid = (Container as GridViewTitleTemplateContainer).Grid;

            Container.Controls.Add(KtheTableMeControle(KrijoKontrolle()));
        }

        /// <summary>
        /// kthen nje liste me controllet baze,te cilet i ka cdo grid
        /// </summary>
        /// <returns></returns>
        protected List<Control> KrijoKontrolle()
        {
            List<Control> baseControls = new List<Control>();

            ASPxButton btnZgjidhkol = new ASPxButton();
            ASPxButton btnRuajKolonat = new ASPxButton();
            ASPxButton btnShtoRresht = new ASPxButton();
            ASPxButton btnRuajTrup = new ASPxButton();

            UpdatePanel pnlRuaj = new UpdatePanel();
            if (meKonfigurimKolonash)
            {
                btnZgjidhkol.ClientSideEvents.Init = "myFaqeCelje.InitTeDrejtaKonf";
                btnRuajKolonat.ClientSideEvents.Init = "myFaqeCelje.InitTeDrejtaKonf";
            }
            btnZgjidhkol.ToolTip = rm.GetString("btnAdministrimiZgjidhKolonat", cultinf);
            btnZgjidhkol.AutoPostBack = false;
            btnZgjidhkol.ClientVisible = false;
            btnZgjidhkol.Image.Url = $"images/theme/{CurrentPage.Theme}/grida/wrench.png";
            btnZgjidhkol.Image.UrlHottracked = $"images/theme/{CurrentPage.Theme}/grida/wrench_W.png";
            btnZgjidhkol.Image.Height = 16;
            btnZgjidhkol.Font.Size = 8;
            btnZgjidhkol.ClientSideEvents.Click = string.Format("function(s,e){{myFaqeCelje.buttonKonfiguroClick(s,e,{0})}}", Grid.ClientInstanceName);
            baseControls.Add(btnZgjidhkol);
            hfState.Set("msgZgjdhniNjeNgaElementetEListes", rm.GetString("msgZgjdhniNjeNgaElementetEListes", cultinf));
            btnRuajKolonat.ToolTip = rm.GetString("btnBlerjeShitjeRuajKolonat", cultinf);
            btnRuajKolonat.AutoPostBack = true;
            btnRuajKolonat.ClientVisible = false;
            btnRuajKolonat.Image.Url = $"images/theme/{CurrentPage.Theme}/grida/disk_blue (3).png";
            btnRuajKolonat.Image.UrlHottracked = $"images/theme/{CurrentPage.Theme}/grida/disk_blue (3)_W.png";
            btnRuajKolonat.Image.Height = 16;
            btnRuajKolonat.Font.Size = 8;
            btnRuajKolonat.Click += RuajKolona_Click;
            pnlRuaj.ContentTemplateContainer.Controls.Add(btnRuajKolonat);
            baseControls.Add(pnlRuaj);
                       
            btnShtoRresht.ToolTip = "Shto Rresht";
            btnShtoRresht.AutoPostBack = false;
            btnShtoRresht.ClientVisible = true;
			btnShtoRresht.Image.Url = $"images/theme/{CurrentPage.Theme}/grida/add2.png";
            btnShtoRresht.Image.UrlHottracked = $"images/theme/{CurrentPage.Theme}/grida/add2_W.png";
            btnShtoRresht.Image.Height = 16;
            btnShtoRresht.Font.Size = 8;
            btnShtoRresht.ClientSideEvents.Click = $"function(s,e){{{Grid.ClientInstanceName}.AddNewRow();{Grid.ClientInstanceName}.cpUShtuaNjeRresht=true}}";
            baseControls.Add(btnShtoRresht);
            return baseControls;
        }

        /// <summary>
        /// mbush tabelen me controllet
        /// </summary>
        /// <param name="tbl">     </param>
        /// <param name="controls"></param>
        protected Table KtheTableMeControle(List<Control> controls)
        {
            Table tbl = KrijoTable(1, controls.Count);
            int indexitd = 0;
            for (int i = 0; i < controls.Count; i++)
            {
                tbl.Rows[0].Cells[indexitd].Controls.Add(controls[i]);
                indexitd++;
            }
            return tbl;
        }

        protected Table KrijoTable(int numRows, int numCells)
        {
            Table tbl = new Table();
            for (int i = 0; i < numRows; i++)
            {
                tbl.Controls.Add(new TableRow());
                for (int j = 0; j < numCells; j++)
                    tbl.Rows[i].Controls.Add(new TableCell());
            }
            return tbl;
        }

        #region EVENTET E KONTROLLEVE TE HEADERIT

        protected void RuajKolona_Click(object sender, EventArgs e)
        {
            int idfiltri = 0;

            DbCore.clsMesazh mesazh = GridUtil.ruajFiltra(idNdermarrje, idPerdoruesi, idGjuha, Grid.ID, emerKomponente, "FilterDefault", Grid.FilterExpression, Grid, renditjeDefault, idKonfig, out idfiltri);//ruan filtrin e zgjedhur tek filtrat
            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(menuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                return;
            }
            if (idKonfig == 0 || idKonfig == 1)
            {
                GridUtil.ruajkonfigurimgridePaKonfigurim(Grid, idNdermarrje, idPerdoruesi, emerKomponente, idViti, cultinf, idGjuha);
            }
            else
            {
                string kodi = DbCore.DbShare.clsKonfigurimAmbjenti.ktheKodKonfigurimi(idKonfig);
                mesazh = GridUtil.ruajkonfigurimgride(Grid, kodi, idNdermarrje, idPerdoruesi, idKomponente, idfiltri, idViti, this.cultinf, idGjuha);
            }

            if (mesazh.Status)
                clsMenuInfo.ShtoMesazhSuksesi(menuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhGabimi(menuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
        }

        #endregion EVENTET E KONTROLLEVE TE HEADERIT
    }
}