using System;
using System.Reflection;
using System.Web.UI;
using DevExpress.Web;
using DbCore;
using System.Web.UI.WebControls;

namespace PlatinumWeb
{
    public partial class UcMenuAndMsgFrame : UserControl
    {
        protected BindingFlags MyBindingFlags = BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance;
        //Event delegator
        public delegate void UserEventMenuClick(object sender, MenuItemEventArgs e);
        public delegate void UserEventMenuTemplate(ASPxMenu menu, ASPxMenu menuInfo);
        public delegate void UserEventRuajFilter(object sender, EventArgs e);
        public delegate void UserEventFshiFilter(object sender, EventArgs e);
        //Event name 
        public event UserEventMenuClick MenuClick;
        public event UserEventMenuTemplate MenuTemplate;
        public event UserEventRuajFilter FilterSave;
        public event UserEventFshiFilter FilterDelete;

        private int IdNdermarrja => mySessionObjects.merrIdNdermarrjeSesioni(Session);
        private int IdPerdoruesi => mySessionObjects.ktheIdPerdoruesi(Session);

        protected void Page_Load(object sender, EventArgs e)
        {
            MenuTemplate?.Invoke(ASPxMenu1, MenuInfo);
        }
        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {
            MenuClick?.Invoke(source, e);
        }

        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            MenuTemplate?.Invoke(ASPxMenu1, MenuInfo);
        }

        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
            FilterSave?.Invoke(sender, e);
        }
        protected void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
            FilterDelete?.Invoke(sender, e);
        }

        public void RuajFilter(ASPxGridView grida, string komponente, int idKonfigurimi, ref HiddenField hfStatusi, string koloneRenditje = "Kodi")
        {
            //kap item qe ka template ne menune e kesaj faqeje
            var itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            var cmbFiltra = ((MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            var filtri = new DbCore.DbAdmin.clsFiltraGrida();

            if (cmbFiltra == null) return;

            filtri.FiltraKodi = cmbFiltra.Text;
            filtri.FiltraShenime = cmbFiltra.Text;
            filtri.FiltraUniversal = false;

            var koka = new DbCore.DbAdmin.clsGridaKoka(mySessionObjects.ktheGjuhe(Session), grida.ID, komponente, IdNdermarrja, idKonfigurimi);
            filtri.GridaKokaId = koka.IdGridaKoka;
            filtri.FiltraVlera = grida.FilterExpression;
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje(koloneRenditje, grida);
            //var kolona = grida.GetSortedColumns();
            //if (kolona.Count > 0)
            //{
            //    filtri.KoloneRenditje = kolona[0].FieldName;
            //    if (kolona[0].SortOrder == DevExpress.Data.ColumnSortOrder.Ascending)
            //        filtri.DrejtimRenditje = true;
            //    else
            //        filtri.DrejtimRenditje = false;
            //}
            //else
            //{
            //    filtri.KoloneRenditje = koloneRenditje;
            //    filtri.DrejtimRenditje = true;
            //}

            filtri.IdPerdoruesi = IdPerdoruesi;
            filtri.IdNdermarje = IdNdermarrja;
            filtri.IdStatusDok = 1;

            var mesazh = filtri.ruaj();

            MenuTemplate?.Invoke(ASPxMenu1, MenuInfo);

            if (mesazh.Status)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            hfStatusi.Value = "true";
            cmbFiltra.Text = "";
            GridUtil.AplikoFilterDefault(grida, idKonfigurimi, cmbFiltra);
        }

        public void FshiFilter(ASPxGridView grida, string komponente, int idKonfigurimi, ref HiddenField hfStatusi)
        {   //kap item qe ka template ne menune e kesaj faqeje
            var itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            var cmbFiltra = ((MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            var filtra = new DbCore.DbAdmin.clsFiltraGrida();

            var koka = new DbCore.DbAdmin.clsGridaKoka(mySessionObjects.ktheGjuhe(Session), grida.ID, komponente, IdNdermarrja, idKonfigurimi);
            if (cmbFiltra != null)
            {
                filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, IdNdermarrja, koka.IdGridaKoka);

                if (filtra.FiltraKodi == null) return;

                filtra.IdPerdoruesi = IdPerdoruesi;

                int vleraKushtDefault = DbCore.DbShare.clsKusht.kthevlereSipasKushtitDheIdKonfig(idKonfigurimi, "FILTER");
                if (vleraKushtDefault == filtra.IdFiltra)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Filtri default nuk mund te fshihet.", pnlMesazhi);
                    cmbFiltra.Text = cmbFiltra.Text;
                    return;
                }
                var mesazh = filtra.fshi();

                clsToolbarConfig.mbushComboBoxFiltra(mySessionObjects.ktheGjuhe(Session), IdNdermarrja, grida.ID,
                    idKonfigurimi, komponente);

                MenuTemplate?.Invoke(ASPxMenu1, MenuInfo);

                if (mesazh.Status)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
            }

            hfStatusi.Value = "true";
            grida.FilterExpression = String.Empty;
        }
    }
}