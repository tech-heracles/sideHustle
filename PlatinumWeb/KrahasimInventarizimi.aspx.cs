using DbCore;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PlatinumWeb.ApplicationUtils.Pages;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using DbCore.DbRegjistrim;
using DbCore.IMBUtils.Types;
using CacheLayer;
using DbCore.IMBUtils.Messages;
using PlatinumWeb.ApplicationUtils;

namespace PlatinumWeb
{
    public partial class KrahasimInventarizimi : MyPageBase
    {
        private int idndermarje, idnderviti, idviti;
        string komponente => clsFunksione.GetKomponente(Request);
        /// <summary>
        /// kur faqja lodohet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                Response.Redirect(DbCore.IMBUtils.Paths.defaultLoginPath);
                return;
            }

            idndermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            idnderviti = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
            idviti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + IdPerdoruesi);
                return;
            }
            if (!IsPostBack)
            {
                EmrateTabeve();
                var tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session, ":");
                hfState.Set("IdPerdoruesi", IdPerdoruesi);
                hfState.Set("idGjuha", IdGjuha);
                hfState.Set("idNdermarrje", idndermarje);
                hfState.Set("idNdermarrjeVit", idnderviti);
                hfState.Set("msgNukKaArtikujTePerbashket", MessagesResource.Messages["msgNukKaArtikujTePerbashket"]);
                if (Request.QueryString["lloj"] == "ash")
                    ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(IdPerdoruesi, idndermarje, cmbKonfigurimi, 162, "KIAASH", rm, ci, IdGjuha);
                else ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(IdPerdoruesi, idndermarje, cmbKonfigurimi, 162, "KIAAGJ", rm, ci, IdGjuha);
                cmbKonfigurimi.SelectedIndex = 0;
                DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                konf.mbushKonfigAmbjSipasId(int.Parse(cmbKonfigurimi.SelectedItem.Value.ToString()), IdGjuha);
                hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, idndermarje, idviti, "Konfigurim Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                konfiguroVleraFillestare();
                konfiguroGridat();
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idndermarje, gvEkzistuese.ID.ToString(), gvEkzistuese, cmbKonfigurimi.Text, "552", IdGjuha, !IsPostBack);
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idndermarje, gvPerbashket.ID.ToString(), gvPerbashket, cmbKonfigurimi.Text, "552", IdGjuha, !IsPostBack);
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idndermarje, gvJoEkzistues.ID.ToString(), gvJoEkzistues, cmbKonfigurimi.Text, "552", IdGjuha, !IsPostBack);
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idndermarje, gvMagTjeter.ID.ToString(), gvMagTjeter, cmbKonfigurimi.Text, "552", IdGjuha, !IsPostBack);
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, idndermarje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), komponente);
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);

            }
            clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, idndermarje, "gvEkzistuese", 1, komponente);
            //clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, idndermarje, "gvEkzistuese", 1, komponente);
            //clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, idndermarje, "gvEkzistuese", 1, komponente);
            //clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, idndermarje, "gvEkzistuese", 1, komponente);
            DbCore.mySessionObjects.ruajMesazhNeSesion(Session, ":");
            mbushGridDokumentInventarizimNgaSession(komponente);
            gvEkzistuese.Columns["#"].VisibleIndex = 0;
            gvPerbashket.Columns["#"].VisibleIndex = 0;
            gvJoEkzistues.Columns["#"].VisibleIndex = 0;
            gvMagTjeter.Columns["#"].VisibleIndex = 0;

            percaktoTemplateMenu(ASPxMenu1, idviti, IdPerdoruesi, idndermarje);
            GridUtil.ToolTipButonaveMbiGride(gvEkzistuese, ci, rm);
            GridUtil.ToolTipButonaveMbiGride(gvPerbashket, ci, rm);
            GridUtil.ToolTipButonaveMbiGride(gvJoEkzistues, ci, rm);
            GridUtil.ToolTipButonaveMbiGride(gvMagTjeter, ci, rm);
            gvEkzistuese.PercaktoTitlePanel(this, MenuInfo, pnlMesazhi, hfState, IdPerdoruesi, idndermarje, idviti, IdGjuha, Convert.ToInt32(cmbKonfigurimi.Value), komponente, rm, ci);
            gvPerbashket.PercaktoTitlePanel(this, MenuInfo, pnlMesazhi, hfState, IdPerdoruesi, idndermarje, idviti, IdGjuha, Convert.ToInt32(cmbKonfigurimi.Value), komponente, rm, ci);
            gvJoEkzistues.PercaktoTitlePanel(this, MenuInfo, pnlMesazhi, hfState, IdPerdoruesi, idndermarje, idviti, IdGjuha, Convert.ToInt32(cmbKonfigurimi.Value), komponente, rm, ci);
            gvMagTjeter.PercaktoTitlePanel(this, MenuInfo, pnlMesazhi, hfState, IdPerdoruesi, idndermarje, idviti, IdGjuha, Convert.ToInt32(cmbKonfigurimi.Value), komponente, rm, ci);
        }

        /// <summary>
        /// Vendos emrat e tabeve ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateTabeve()
        {

            if (Request.QueryString["lloj"] == "ash")
            {
                ASPxPageControl1.TabPages[2].Text = "Art jo ne mag";
                lblArtMagTjeter.Text = "Artikujt qe jane ne program por nuk jane bere hyrje";
            }

        }

        private void mbushCombo()
        {
            dteDtDok.Date = DateTime.Today;
            AspxWebControlUtils.vendosDateEditMask(dteDtDok);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btneMagazina);
            var lloji = Request.QueryString["lloj"] == "agj" ? 2 : 1;
            ConfigureAspxComboBox.mbushComboMagazinat(idndermarje, btneMagazina, IdPerdoruesi, true, lloji, true);
            dteDtDok.ClientEnabled = false;
            btneMagazina.ClientEnabled = false;
        }
        private clsKokaInventarizim krijoKoka(int id)
        {
            clsKokaInventarizim koka = new clsKokaInventarizim();
            koka.mbushKokaInventarizimSipasID(id);
            btneMagazina.Value = koka.IdMag.ToString();
            dteDtDok.Date = koka.DtDok;
            DbCore.DbShare.clsKusht kusht = new DbCore.DbShare.clsKusht(koka.IdKonfigAmbjente, "ZKDM");
            DbCore.DbShare.clsKonfigurimAmbjenti konfmag = new DbCore.DbShare.clsKonfigurimAmbjenti(kusht.Vlera);
            hfKonfigurimi.Value = konfmag.IdKonfigAmbjente.ToString();
            hfNiveli.Value = konfmag.IdNivel.ToString();
            string lloji = Request.QueryString["lloj"];
            if (lloji == "agj")
            {
                DbCore.DbShare.clsKusht kusht2 = new DbCore.DbShare.clsKusht(koka.IdKonfigAmbjente, "ZKDM2");
                DbCore.DbShare.clsKonfigurimAmbjenti konfmag2 = new DbCore.DbShare.clsKonfigurimAmbjenti(kusht2.Vlera);
                hfKonfigurimi2.Value = konfmag2.IdKonfigAmbjente.ToString();
                hfNiveli2.Value = konfmag.IdNivel.ToString();
            }
            return koka;
        }
        private static void krahasoVleraAfatshkurter(colKrahasimInventarizimics colekzistuese, colKrahasimInventarizimics colperbashket, colKrahasimInventarizimics colMagTjeter, colKrahasimInventarizimics coljoekzistues, clsKokaInventarizim koka, colTrupiInventarizim coltrupatSel, int idndermarje)
        {
            IEnumerable<IGrouping<string, clsTrupiInventarizim>> trupaSipasKodbarit = coltrupatSel.GroupBy(x => x.Barkod);
            DataTable kodbargjendje = colTrupiMagazina.ktheGjitheTrupiMagazinaMerrKodbargjendjeSipasDatesdheMagazines(koka.IdMag, idndermarje, koka.DtDok);
            DataColumn[] keys = new DataColumn[1];
            keys[0] = kodbargjendje.Columns[1];
            kodbargjendje.PrimaryKey = keys;
            for (int a = 0, count = trupaSipasKodbarit.Count(); a < count; a++)
            {

                DbCore.DbInventari.clsKodbari kodbar = new DbCore.DbInventari.clsKodbari();
                kodbar.ktheKodbarSipasPershkrimit(trupaSipasKodbarit.ElementAt(a).Key, idndermarje);
                DataRow dr = kodbargjendje.Rows.Find(kodbar.IdArtikulli);
                if (dr != null)//ky kodbar ekziston dhe ne magazine dhe ne inventarizim
                {
                    decimal sasiinv = (trupaSipasKodbarit.ElementAt(a)).Sum(x => x.Sasi);
                    if (kodbar.Njesia == 2)
                    {
                        DbCore.DbInventari.clsArtikulli art = new DbCore.DbInventari.clsArtikulli(kodbar.IdArtikulli);
                        sasiinv = sasiinv * art.KoeficientArtikulli;
                    }
                    clsKrahasimInventarizimi kraha = new clsKrahasimInventarizimi(a, "", dr["kodbar"].ToString(), dr["Kodi"].ToString(), dr["Pershkrimi"].ToString(), sasiinv, decimal.Parse(dr["GJENDJESASI"].ToString()), decimal.Parse(dr["GJENDJESASI"].ToString()) - sasiinv, int.Parse(dr["idmag"].ToString()), decimal.Parse(dr["CMIMI"].ToString()), dr["Grup1"].ToString());
                    colperbashket.Add(kraha);
                    kodbargjendje.Rows.Remove(dr);
                }
                else
                {
                    DbCore.DbInventari.clsArtikulli art = new DbCore.DbInventari.clsArtikulli();

                    if (kodbar.IdArtikulli != 0)
                    {
                        art = new DbCore.DbInventari.clsArtikulli(kodbar.IdArtikulli);

                    }
                    decimal sasiinv = (trupaSipasKodbarit.ElementAt(a)).Sum(x => x.Sasi);
                    if (kodbar.Njesia == 2)
                    {

                        sasiinv = sasiinv * art.KoeficientArtikulli;
                    }
                    int index = colperbashket.FindIndex(x => x.Kodi == art.KodArtikulli);
                    if (index != -1)//kur artikulli i kodbarit ekziston tek kjo magazine por kemi marre kodbarin tjeter
                    {
                        colperbashket[index].SasiaInv += sasiinv;
                        colperbashket[index].Diferenca -= sasiinv;
                    }
                    else // kodbari nuk ekziston ne magazine
                    {
                        if (kodbar.IdKodbari > 0)
                        {
                            DbCore.DbInventari.clsArtikulli art1 = new DbCore.DbInventari.clsArtikulli(kodbar.IdArtikulli);
                            clsKrahasimInventarizimi kraha = new clsKrahasimInventarizimi(a, "", trupaSipasKodbarit.ElementAt(a).Key, art1.KodArtikulli, art1.PershkrimArtikulli, sasiinv, 0, 0 - sasiinv, 0, 0, art1.KodKodifikimi1);
                            colMagTjeter.Add(kraha);
                        }
                        else
                        {
                            clsKrahasimInventarizimi kraha = new clsKrahasimInventarizimi(a, "", trupaSipasKodbarit.ElementAt(a).Key, "", "", sasiinv, 0, 0 - sasiinv, koka.IdMag, 0, "");
                            coljoekzistues.Add(kraha);
                        }
                    }
                }

            }
            foreach (DataRow dr in kodbargjendje.Rows)
            {
                clsKrahasimInventarizimi kraha = new clsKrahasimInventarizimi(int.Parse(dr["IDARTIKULLI"].ToString()), "", dr["kodbar"].ToString(), dr["Kodi"].ToString(), dr["Pershkrimi"].ToString(), 0, decimal.Parse(dr["GJENDJESASI"].ToString()), decimal.Parse(dr["GJENDJESASI"].ToString()), int.Parse(dr["idmag"].ToString()), 0, dr["Grup1"].ToString());
                colekzistuese.Add(kraha);
            }
        }
        private static void krahasoVleraAfatgjate(colKrahasimInventarizimics colekzistuese, colKrahasimInventarizimics colperbashket, colKrahasimInventarizimics coljoekzistues, colKrahasimInventarizimics colmagtjeter, colKrahasimInventarizimics colperbashketgabim, colKrahasimInventarizimics coljoekzistuesgabim, colKrahasimInventarizimics colmagtjetergabim, clsKokaInventarizim koka, colTrupiInventarizim coltrupatSel, int idndermarje)
        {
            IEnumerable<IGrouping<string, clsTrupiInventarizim>> trupaSipasSeriali = coltrupatSel.GroupBy(x => x.Serial);
            DataTable serialgjendje = colTrupiMagazina.ktheGjitheTrupiMagazinaMerrSerialgjendjeSipasDatesdheMagazines(koka.IdMag, idndermarje, koka.DtDok);
            DataColumn[] keys = new DataColumn[1];
            keys[0] = serialgjendje.Columns[6];
            serialgjendje.PrimaryKey = keys;
            for (int a = 0; a < trupaSipasSeriali.Count(); a++)
            {
                DbCore.DbAsete.clsAQTSeriale serial = new DbCore.DbAsete.clsAQTSeriale();
                serial.merrAQTSerialSipasKodAQT(trupaSipasSeriali.ElementAt(a).Key, idndermarje);
                DataRow dr = serialgjendje.Rows.Find(serial.IdAQTSerial);
                if (dr != null)//ky seriali ekziston dhe ne magazine dhe ne inventarizim
                {
                    decimal sasiinv = (trupaSipasSeriali.ElementAt(a)).Sum(x => x.Sasi);
                    clsKrahasimInventarizimi kraha = new clsKrahasimInventarizimi(a, dr["seriali"].ToString(), "", dr["Kodi"].ToString(), dr["Pershkrimi"].ToString(), sasiinv, decimal.Parse(dr["GJENDJESASI"].ToString()), decimal.Parse(dr["GJENDJESASI"].ToString()) - sasiinv, int.Parse(dr["idmag"].ToString()), 0, dr["Grup1"].ToString());
                    if (sasiinv > 1)
                        colperbashketgabim.Add(kraha);
                    else colperbashket.Add(kraha);
                    serialgjendje.Rows.Remove(dr);
                }
                else
                {
                    decimal sasiinv = (trupaSipasSeriali.ElementAt(a)).Sum(x => x.Sasi);
                    if (serial.IdNjesiAdministrativeAktuale != 0)
                    {

                        DataTable trupimagMeSerialeGjendje = colTrupiMagazina.ktheGjitheTrupiMagazinaMerrSerialgjendjeSipasSerialitdheMagazines(serial.IdNjesiAdministrativeAktuale, idndermarje, serial.IdAQTSerial);
                        decimal sasiprg;
                        if (trupimagMeSerialeGjendje.Rows.Count > 0)
                            sasiprg = Convert.ToDecimal(trupimagMeSerialeGjendje.Rows[0][0]);
                        else sasiprg = 0;
                        DbCore.DbInventari.clsArtikulli art = new DbCore.DbInventari.clsArtikulli(serial.IdAQTArt);
                        clsKrahasimInventarizimi krahamag = new clsKrahasimInventarizimi(a, trupaSipasSeriali.ElementAt(a).Key, "", art.KodArtikulli, art.PershkrimArtikulli, sasiinv, sasiprg, sasiprg - sasiinv, serial.IdNjesiAdministrativeAktuale, 0, art.KodKodifikimi1);
                        if (sasiinv > 1)
                            colmagtjetergabim.Add(krahamag);
                        else colmagtjeter.Add(krahamag);                        
                    }
                    else
                    {
                        clsKrahasimInventarizimi kraha = new clsKrahasimInventarizimi(a, trupaSipasSeriali.ElementAt(a).Key, "", "", "", sasiinv, 0, 0 - sasiinv, koka.IdMag, 0, "");
                        if (sasiinv > 1)
                            coljoekzistuesgabim.Add(kraha);
                        else coljoekzistues.Add(kraha);
                    }

                }

            }
            foreach (DataRow dr in serialgjendje.Rows)
            {
                clsKrahasimInventarizimi kraha = new clsKrahasimInventarizimi(int.Parse(dr["IDARTIKULLI"].ToString()), dr["seriali"].ToString(), "", dr["Kodi"].ToString(), dr["Pershkrimi"].ToString(), 0, decimal.Parse(dr["GJENDJESASI"].ToString()), decimal.Parse(dr["GJENDJESASI"].ToString()), int.Parse(dr["idmag"].ToString()), 0, dr["Grup1"].ToString());
                colekzistuese.Add(kraha);
            }
        }
        /// <summary>
        /// mbush gridat me te dhena
        /// </summary>
        private void konfiguroVleraFillestare()
        {
            mbushCombo();
            var previusPageID = Converter.MerrVlereOseDefault<string>(Request.QueryString["RegjistrimInventarizimiPageId"]);
            var pageCache = GlobalCacheManager.GetPageCacheByPageID(previusPageID);
            int[] ids = pageCache.Get<int[]>("idinventarizimi");

            clsKokaInventarizim koka = krijoKoka(ids[0]);

            colTrupiInventarizim coltrupatSel = new colTrupiInventarizim();
            for (int i = 0; i < ids.Length; i++)
            {
                colTrupiInventarizim coltrupat = new colTrupiInventarizim();
                coltrupat.mbushGjitheTrupiInventarizimNgaKoka(ids[i]);
                coltrupatSel.AddRange(coltrupat);
            }

            var colekzistuese = new colKrahasimInventarizimics();
            var colperbashket = new colKrahasimInventarizimics();
            var coljoekzistues = new colKrahasimInventarizimics();
            var colmagtjeter = new colKrahasimInventarizimics();
            var colperbashketgabim = new colKrahasimInventarizimics();
            var coljoekzistuesgabim = new colKrahasimInventarizimics();
            var colmagtjetergabim = new colKrahasimInventarizimics();


            if (Request.QueryString["lloj"] == "agj")
                krahasoVleraAfatgjate(colekzistuese, colperbashket, coljoekzistues, colmagtjeter, colperbashketgabim, coljoekzistuesgabim, colmagtjetergabim, koka, coltrupatSel, idndermarje);
            else
                krahasoVleraAfatshkurter(colekzistuese, colperbashket, colmagtjeter, coljoekzistues, koka, coltrupatSel, idndermarje);

            gvEkzistuese.DataSource = colekzistuese;
            gvEkzistuese.DataBind();
            gvPerbashket.DataSource = colperbashket;
            gvPerbashket.DataBind();
            gvJoEkzistues.DataSource = coljoekzistues;
            gvJoEkzistues.DataBind();
            gvMagTjeter.DataSource = colmagtjeter;
            gvMagTjeter.DataBind();

            mySessionObjects.ruajObjectNeSesion(Session, colperbashketgabim, "Perbashket");
            mySessionObjects.ruajObjectNeSesion(Session, coljoekzistuesgabim, "JoEkzistues");
            mySessionObjects.ruajObjectNeSesion(Session, colmagtjetergabim, "MagTjeter");
            DbCore.mySessionObjects.RuajNeSession<colKrahasimInventarizimics>(Session, colekzistuese, komponente + gvEkzistuese.ID + idnderviti);
            DbCore.mySessionObjects.RuajNeSession<colKrahasimInventarizimics>(Session, colperbashket, komponente + gvPerbashket.ID + idnderviti);
            DbCore.mySessionObjects.RuajNeSession<colKrahasimInventarizimics>(Session, coljoekzistues, komponente + gvJoEkzistues.ID + idnderviti);
            DbCore.mySessionObjects.RuajNeSession<colKrahasimInventarizimics>(Session, colmagtjeter, komponente + gvMagTjeter.ID + idnderviti);
            if (colperbashketgabim.Count > 0 || coljoekzistuesgabim.Count > 0 || colmagtjetergabim.Count > 0)
                clsMenuInfo.ShtoMesazhInformues(MenuInfo, "Ka gabime ne dokumenta. Ju lutem hapni listen e gabimeve!", pnlMesazhi);

        }

        private void mbushGridDokumentInventarizimNgaSession(string komponente)
        {
            colKrahasimInventarizimics tmpObject, tmpObject2, tmpObject3, tmpObject4;
            tmpObject = DbCore.mySessionObjects.MerrNgaSession<colKrahasimInventarizimics>(Session, komponente + gvEkzistuese.ID + idnderviti);
            tmpObject2 = DbCore.mySessionObjects.MerrNgaSession<colKrahasimInventarizimics>(Session, komponente + gvPerbashket.ID + idnderviti);
            tmpObject3 = DbCore.mySessionObjects.MerrNgaSession<colKrahasimInventarizimics>(Session, komponente + gvJoEkzistues.ID + idnderviti);
            tmpObject4 = DbCore.mySessionObjects.MerrNgaSession<colKrahasimInventarizimics>(Session, komponente + gvMagTjeter.ID + idnderviti);
            if (tmpObject == null || tmpObject2 == null || tmpObject3 == null || tmpObject4 == null)
            {

                konfiguroVleraFillestare();
            }
            else
            {
                gvEkzistuese.DataSource = tmpObject;
                gvEkzistuese.DataBind();
                gvPerbashket.DataSource = tmpObject2; ;
                gvPerbashket.DataBind();
                gvJoEkzistues.DataSource = tmpObject3; ;
                gvJoEkzistues.DataBind();
                gvMagTjeter.DataSource = tmpObject4; ;
                gvMagTjeter.DataBind();
            }
        }
        /// <summary>
        /// konfiguron gridat
        /// </summary>
        private void konfiguroGridat()
        {
            konfiguroGride(gvEkzistuese);
            konfiguroGride(gvPerbashket);
            konfiguroGride(gvJoEkzistues);
            konfiguroGride(gvMagTjeter);

        }
        /// <summary>
        /// konfiguron griden
        /// </summary>
        /// <param name="grida">grida</param>
        private void konfiguroGride(ASPxGridView grida)
        {
            KonfigurimComboGride.ShtoMagazinaNdermarrje(grida, Session, komponente, MerrIdentifikuesFaqje(), "Magazina");
            //DbCore.clsFunksione.percaktoVisibleColumns(DbCore.mySessionObjects.ktheGjuhe(base.Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(base.Session), grida, grida.ID, komponente);
            GridViewDataTextColumn col1 = grida.Columns["SasiaInv"] as GridViewDataTextColumn;
            col1.PropertiesEdit.DisplayFormatString = "0.00";
            GridViewDataTextColumn col4 = grida.Columns["SasiaPrg"] as GridViewDataTextColumn;
            col4.PropertiesEdit.DisplayFormatString = "0.00";
            GridViewDataTextColumn col5 = grida.Columns["Diferenca"] as GridViewDataTextColumn;
            col5.PropertiesEdit.DisplayFormatString = "0.00";
            GridViewDataTextColumn col8 = grida.Columns["Cmimi"] as GridViewDataTextColumn;
            col8.PropertiesEdit.DisplayFormatString = "0.00";

            if (grida.Columns["#"] == null)
            {
                DevExpress.Web.GridViewCommandColumn check = new DevExpress.Web.GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true;
                check.Width = Unit.Percentage(2);

                grida.Columns.Add(check);

            }
            grida.Columns["#"].VisibleIndex = 0;
            GridUtil.konfigGrideListeEMadhePaTheme(grida, "Id", true);
            grida.Settings.ShowTitlePanel = true;
            //    DbCore.clsFunksione.percaktoVisibleColumnsGridSipasKodKonfigurimi(idndermarje,grida.ID.ToString(),grida,cmbKonfigurimi.Value.ToString(),"552",IdGjuha,!IsPostBack);

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
            clsToolbarConfig.percaktoTemplateMenu(IdGjuha, idViti, idPerdorues, idNdermarrje, aSPxMenu1, komponente, this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, hfRuaj.Value == "Ruaj" ? true : false, true, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
        }

        /// <summary>
        /// ben databound menune
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(ASPxMenu1, idviti, IdPerdoruesi, idndermarje);
        }

        /// <summary>
        /// ruan filtrin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            int index = ASPxPageControl1.ActiveTabIndex;
            ASPxGridView grida;
            if (index == 0) grida = gvEkzistuese;
            else if (index == 1) grida = gvPerbashket;
            else if (index == 2) grida = gvMagTjeter;
            else grida = gvJoEkzistues;

            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(IdGjuha, "gvEkzistuese", komponente, idNdermarrje);
            DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida() { FiltraKodi = cmbFiltra.Text, FiltraShenime = cmbFiltra.Text, FiltraUniversal = false, GridaKokaId = koka.IdGridaKoka, FiltraVlera = grida.FilterExpression, IdPerdoruesi = IdPerdoruesi, IdNdermarje = idNdermarrje, IdStatusDok = 1 };
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("Kodi", grida);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = grida.GetSortedColumns();
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
            //    filtri.KoloneRenditje = "Kodi";
            //    filtri.DrejtimRenditje = true;
            //}
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            mesazh = filtri.ruaj();
            clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, idNdermarrje, "gvEkzistuese", 1, komponente);
            if (mesazh.Status == true)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            cmbFiltra.Text = "";
        }
        /// <summary>
        /// fshin filtrin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje
            int index = ASPxPageControl1.ActiveTabIndex;
            ASPxGridView grida;
            if (index == 0) grida = gvEkzistuese;
            else if (index == 1) grida = gvPerbashket;
            else if (index == 2) grida = gvMagTjeter;
            else grida = gvJoEkzistues;

            DevExpress.Web.MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            //filtra.mbushFiltraGridaSipasFiltraEmri(cmbFiltra.Text, idndermarje);
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(IdGjuha, "gvEkzistuese", komponente, idNdermarrje);
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                filtra.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                mesazh = filtra.fshi();
                clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, idNdermarrje, "gvEkzistuese", 1, komponente);
                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";
                grida.FilterExpression = String.Empty;
            }
        }

        /// <summary>
        /// kur grida ben callback
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridat_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {

        }

        /// <summary>
        /// kur grida ben callback nga perdoruesi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridat_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            int index = ASPxPageControl1.ActiveTabIndex;
            ASPxGridView grida;
            if (index == 0) grida = gvEkzistuese;
            else if (index == 1) grida = gvPerbashket;
            else if (index == 2) grida = gvMagTjeter;
            else grida = gvJoEkzistues;


            string[] arr = e.Parameters.Split(';');
            if (arr.Length == 3)
            {
                if (arr[2] == "")
                    grida.FilterExpression = "";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    //filtra.mbushFiltraGridaSipasFiltraKodi(arr[2], idndermarje);
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(IdGjuha, "gvEkzistuese", komponente, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        grida.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, grida);
                        konfiguroVleraFillestare();

                    }
                }
            }
            konfiguroGride(grida);
        }
        /// <summary>
        /// vendos property te grides per tu aksesuar nga javascripti
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridat_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            ASPxGridView grida = sender as ASPxGridView;
            e.Properties["cpPageIndex"] = grida.PageIndex;
            e.Properties["cpPageRow"] = grida.SettingsPager.PageSize;
            e.Properties["cpRowCount"] = grida.VisibleRowCount;
        }
        /// <summary>
        /// filtrimet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridat_HeaderFilterFillItems(object sender, ASPxGridViewHeaderFilterEventArgs e)
        {
            e.Values.Clear();
            e.AddValue("(Te gjithe)", string.Empty, "true");
        }

        protected void gridat_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {

        }

        protected void btnXlsxExport_Click1(object sender, EventArgs e)
        {
            try
            {
                gridExport1.WriteXlsxToResponse("ArtEkzistues", true);
            }
            catch (Exception)
            {
            }
        }
        protected void btnPdfExport_Click1(object sender, EventArgs e)
        {
            try
            {
                gridExport1.WritePdfToResponse("ArtEkzistues", true);
            }
            catch (Exception)
            {
            }
        }

        protected void btnXlsxExport_Click2(object sender, EventArgs e)
        {
            try
            {
                gridExport2.WriteXlsxToResponse("ArtPerbashket", true);
            }
            catch (Exception)
            {
            }
        }
        protected void btnPdfExport_Click2(object sender, EventArgs e)
        {
            try
            {
                gridExport2.WritePdfToResponse("ArtPerbashket", true);
            }
            catch (Exception)
            {
            }
        }
        protected void btnXlsxExport_Click4(object sender, EventArgs e)
        {
            try
            {
                gridExport4.WriteXlsxToResponse("ArtMagTjeter", true);
            }
            catch (Exception)
            {
            }
        }
        protected void btnPdfExport_Click4(object sender, EventArgs e)
        {
            try
            {
                gridExport4.WritePdfToResponse("ArtMagTjeter", true);
            }
            catch (Exception)
            {
            }
        }
        protected void btnXlsxExport_Click3(object sender, EventArgs e)
        {
            try
            {
                gridExport3.WriteXlsxToResponse("ArtJoEkzistues", true);
            }
            catch (Exception)
            {
            }
        }
        protected void btnPdfExport_Click3(object sender, EventArgs e)
        {
            try
            {
                gridExport3.WritePdfToResponse("ArtJoEkzistues", true);
            }
            catch (Exception)
            {
            }
        }

        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {
            if (e.Item.Name == "ListaGabimeve")
            {
                DataTable err = new DataTable();
                err.Columns.Add("Kodi");
                err.Columns.Add("Gabimi");
                err.Columns.Add("Rreshti");

                colKrahasimInventarizimics col = new colKrahasimInventarizimics();
                if (ASPxPageControl1.ActiveTabIndex == 1)
                    col = (colKrahasimInventarizimics)mySessionObjects.merrObjectNgaSesioni(Session, "Perbashket");
                else if (ASPxPageControl1.ActiveTabIndex == 3)
                    col = (colKrahasimInventarizimics)mySessionObjects.merrObjectNgaSesioni(Session, "JoEkzistues");
                else if (ASPxPageControl1.ActiveTabIndex == 2)
                    col = (colKrahasimInventarizimics)mySessionObjects.merrObjectNgaSesioni(Session, "MagTjeter");
                int i = 1;
                foreach (clsKrahasimInventarizimi krah in col)
                {
                    object[] arr = { krah.Seriali, krah.SasiaInv, i };
                    err.Rows.Add(arr);
                    i++;
                }
                DbCore.mySessionObjects.ruajTabeleGabimeshImporti(Session, err);
                hfRaport.Value = "po";
            }
            else hfRaport.Value = "jo";

        }
    }
}