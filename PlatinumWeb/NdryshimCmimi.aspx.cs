using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

using DevExpress.Web;

using DbCore;
using System.Resources;
using System.Globalization;
using DbCore.IMBUtils;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class NdryshimCmimi : MyPageBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                Response.Redirect($"{Paths.defaultLoginPath}arsye=FaqePaautorizuar");
            }
            int idPerd = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);

            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) != null)

                percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            else
                percaktoTemplateMenu(ASPxMenu1, 0, 0, 0);
            if (!IsPostBack)
            {
                AspxWebControlUtils.vendosDateEditMask(dteData);
                dteData.Date = DateTime.Today.Date;
                ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmbArtikulli);

            }
        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        /// <param name="idViti"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNdermarrje"></param>
        private void percaktoTemplateMenu(ASPxMenu aSPxMenu1, int idViti, int idPerdorues, int idNdermarrje)
        {
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, "NdryshimCmimi.aspx", this, MenuInfo, true, true, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
        }

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        }

        /// <summary>
        /// metoda per te thirrur veprimet e menuse kur shtypen butonat
        /// </summary>
        /// <param name="source">derguesi</param>
        /// <param name="e">parametrat</param>
        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {
            if (e.Item.Name == "Ruaj")
            {
                Page.Validate("entries");
                gjeneroFatura();
            }
        }

        private void gjeneroFatura()
        {
            clsMesazh mesazh = new clsMesazh();

            try
            {
                ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                CultureInfo ci = mySessionObjects.ktheCultureInfo(Session);
                DbCore.DbRegjistrim.colKokaMagazina colKokat = new DbCore.DbRegjistrim.colKokaMagazina();
                int idNdermarje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
                int idArtikulli = DbCore.DbInventari.clsArtikulli.ktheIdArtikulli(cmbArtikulli.Text, idNdermarje);
                DbCore.DbInventari.clsArtikulli art = new DbCore.DbInventari.clsArtikulli(idArtikulli);
                DbCore.DbRegjistrim.colTrupiMagazina colTrup = new DbCore.DbRegjistrim.colTrupiMagazina();
                colTrup.MerrGjendjeArtikulliMeImeiPerDaten(idArtikulli, dteData.Date.Date);
                int idperdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);
                if (colTrup.Count==0)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Nuk ka gjendje per kete artikull ne asnje nga magazinat", pnlMesazhi);
                    return ;
                }
                IEnumerable<IGrouping<int, DbCore.DbRegjistrim.clsTrupiMagazina>> groupmag = colTrup.GroupBy(x => x.IdMag);
                for (int i = 0; i < groupmag.Count(); i++)
                {

                    DbCore.DbRegjistrim.colTrupiMagazina trupiPerMag = new DbCore.DbRegjistrim.colTrupiMagazina();
                    trupiPerMag.AddRange(groupmag.ElementAt(i));
                    DbCore.DbRegjistrim.colTrupiMagazina trupiCmimiRi = new DbCore.DbRegjistrim.colTrupiMagazina();
                    trupiCmimiRi.AddRange(trupiPerMag);
                    foreach (DbCore.DbRegjistrim.clsTrupiMagazina tr in trupiPerMag)
                    {
                        DbCore.DbRegjistrim.clsTrupiMagazina trRi = new DbCore.DbRegjistrim.clsTrupiMagazina(tr.IdTrupiMagazina, tr.IdKokaMagazina, tr.IdLlojVeprimi, tr.IdArtikulli, "", "", tr.IdNjesia, -tr.Sasia, double.Parse(txtCmimi.Text), double.Parse(txtCmimi.Text) * (-tr.Sasia), tr.Koeficenti, tr.Shenja, tr.SasiProgresive, tr.VleftaProgresive, tr.IdMag, tr.Data, tr.IdStatusDok, tr.IdRenditjes, tr.IdDetajimi, tr.SasiProgresiveDetajimi, tr.VlefteProgresiveDetajimi, tr.IdDetajimi2, tr.IdTrupiRezervimi, tr.IdTrupiKonvertimFSH,tr.IdTrupiKonvertimUSH,tr.IdTrupiKonvertimUD,tr.IdKthimi,tr.IdTrupiShitjeGjenerimi,art,tr.Shenime,tr.IdArtikullSet, tr.IdBarkodi);
                        tr.Element = art;
                        trupiCmimiRi.Add(trRi);
                    }
                    string mesazhinfo = "";
                    DbCore.DbRegjistrim.clsNjesiAdministrative mag = new DbCore.DbRegjistrim.clsNjesiAdministrative(trupiPerMag[0].IdMag);
                    int idndervit=  DbCore.DbAdmin.clsNdermarrjeViti.ktheIdNdermarrjeVitiSipasNdermarjesDheKodVitit(mag.IdNdermarje, DateTime.Today.Year);
                    if (idndervit == 0)
                        continue;
                    DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                    konf.mbushKonfigAmbjSipasKod("NCD", mag.IdNdermarje);
                    DbCore.DbRegjistrim.clsKokaMagazina koka = new DbCore.DbRegjistrim.clsKokaMagazina();
                    mesazh = koka.krijoMagazine(0, konf.IdNivel, konf.IdKonfigAmbjente, 0, "", mag.IdNjesiAdministrative, mag.Kodi, dteData.Date, "NCD-"+cmbArtikulli.Text, 0, "", 6, trupiCmimiRi.Sum(x => x.Vlefta), 1, mag.IdNdermarje, idndervit, idperdoruesi, DateTime.Today.Date, 1, "Nga ndryshimi i cmimit", 0, "", 0, "", 0, "", false, 0, 0, 0, "", "", "", trupiCmimiRi, new DbCore.DbRegjistrim.clsKokaMagazina(), new DbCore.DbKontabiliteti.clsKokaFleteKontabel(), 0, out mesazhinfo, true, 0, "", 26, idperdoruesi, DateTime.Today.Date, "", "", "", "", 0, new DevExpress.Web.ASPxHiddenField(), true, 0, null, false, "", new DbCore.DbRegjistrim.clsKokaRezervime(),false,false,"","",0);
                   
                    colKokat.Add(koka);
                    if (!mesazh.Status)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                        return;
                    }
                }
                if (colKokat.Count == 0)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Nuk ka gjendje per kete artikull ne asnje nga magazinat", pnlMesazhi);
                    return;
                }
                mesazh = colKokat.ruaj(rm,ci);
                if (!mesazh.Status)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                    return;
                }
                else clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            }
            catch (Exception ex)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, ex.Message, pnlMesazhi);

            }
        }
        protected void btneArtikulli_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("cmbArtikulli"))
                {
                    CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
                    ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                    ConfigureAspxComboBox.mbushComboArtikulli(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), cmbArtikulli,e, rm.GetString("postStringTvsh"));
                }
            }
        }
        protected void cmbArtikulli_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("cmbArtikulli"))
                {
                    CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
                    ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                    ConfigureAspxComboBox.mbushComboArtikulli(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), cmbArtikulli, e, rm.GetString("postStringTvsh"));
                }
            }
        }
    }
}