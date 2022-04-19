using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using DbCore;
using DbCore.DbAdmin;
using DbCore.DbArkaBanka;
using DbCore.DbAsete;
using DbCore.DbBuxheti;
using DbCore.DbKontabiliteti;
using DbCore.DbListPagesat;
using DbCore.DbProdhimi;
using DbCore.DbRegjistrim;
using DbCore.DbShare;
using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.Messages;
using DevExpress.Web;

namespace PlatinumWeb.ApplicationUtils
{
    public static class AspxWebControlUtils
    {
        public static void konfiguroMenuRuajPerLupaPerEksport(ASPxMenu menu, Page page)
        {//konfigurimi i menu si toolbar
            for (int i = 0; i < 2; i++)
            {
                menu.Items.Add();
            }
            menu.AutoPostBack = true;
            string themeMenuFolder = "~/images/theme/" + page.Theme + "/menu/";
            menu.Items[0].Name = "OK";
            menu.Items[0].Text = "Eksporto";
            menu.Items[0].Image.Url = themeMenuFolder + "Eksporto.png";
            menu.Items[0].Image.UrlHottracked = themeMenuFolder + "Eksporto_W.png";
            menu.Items[1].Name = "Mbyll";
            menu.Items[1].Text = "Mbyll";
            menu.Items[1].Image.Url = themeMenuFolder + "Lista.png";
            menu.Items[1].Image.UrlHottracked = themeMenuFolder + "Lista_W.png";
            menu.ItemImagePosition = ImagePosition.Top;
        }

        /// <summary>
        /// perdoret per te konfiguar menune me butonat perkates, por pa percaktuar temen
        /// </summary>
        /// <param name="menu">kontrollin e menuse</param>
        public static void konfiguroMenuPaTheme(ASPxMenu menu)
        {//konfigurimi i menu si toolbar
            //menu.CssFilePath = "~/App_Themes/Aqua/{0}/styles.css";
            //menu.CssPostfix = "Aqua";
            //menu.ImageFolder = "~/App_Themes/Aqua/{0}/";
            menu.ItemSpacing = 0;

            menu.SeparatorHeight = 40;
            menu.SeparatorWidth = 1;
            menu.TextIndent = 5;

            // menu.ImageSpacing = 5;
            menu.Height = 5;
            for (int i = 0; i < 11; i++)
            {
                menu.Items.Add();
            }
            menu.AutoPostBack = true;
            menu.Items[0].Name = "Modifiko";
            menu.Items[0].Text = "Modifiko";
            menu.Items[0].Image.Url = "~/images/document_edit.png";

            menu.Items[1].Name = "Shto";
            menu.Items[1].Text = "Shto";
            menu.Items[1].Image.Url = "~/images/document_add.png";
            menu.Items[2].Name = "Klono";
            menu.Items[2].Text = "Klono";
            menu.Items[2].Image.Url = "~/images/document_add.png";
            menu.Items[2].Visible = false;
            menu.Items[3].Name = "Fshi";
            menu.Items[3].Text = "Fshi";
            menu.Items[3].Image.Url = "~/images/document_delete.png";
            menu.Items[4].Name = "Ruaj";
            menu.Items[4].Text = "Ruaj";
            menu.Items[4].Image.Url = "~/images/disk_blue.png";

            menu.Items[5].Name = "Filtra";
            menu.Items[5].Text = "Filtra";
            menu.Items[5].Image.Url = "~/images/06.png";
            menu.Items[6].Name = "RuajFilter";
            menu.Items[6].Text = "Ruaj Filter";
            menu.Items[6].Image.Url = "~/images/disk_blue.png";

            menu.Items[7].Name = "Poshte";
            menu.Items[7].Text = "Poshte";
            menu.Items[7].Image.Url = "~/images/arrow_down_green.png";
            menu.Items[8].Name = "Lart";
            menu.Items[8].Text = "Lart";
            menu.Items[8].Image.Url = "~/images/arrow_up_green.png";
            menu.Items[9].Name = "Fillim";
            menu.Items[9].Text = "Fillim";
            menu.Items[9].Image.Url = "~/images/layout_northwest.png";
            menu.Items[10].Name = "Fund";
            menu.Items[10].Text = "Fund";
            menu.Items[10].Image.Url = "~/images/layout_southwest.png";

            menu.VerticalPopOutImage.Height = 11;
            menu.VerticalPopOutImage.Width = 11;
            menu.ItemStyle.ImageSpacing = 5;
            menu.ItemStyle.PopOutImageSpacing = 18;
            menu.SubMenuStyle.GutterWidth = 0;
            menu.SubMenuItemStyle.ImageSpacing = 7;
            menu.BorderWidth = 1;
            menu.HorizontalPopOutImage.Height = 7;
            menu.HorizontalPopOutImage.Width = 7;
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="menu"></param>
        public static void konfiguroMenuRuajPerLupaPerInfo(ASPxMenu menu)
        {//konfigurimi i menu si toolbar
            for (int i = 0; i < 2; i++)
            {
                menu.Items.Add();
            }
            menu.AutoPostBack = true;
            menu.Items[0].Name = "OK";
            menu.Items[0].Text = "OK";

            menu.Items[0].Image.Url = "~/images/new/check2 (2).png";
            menu.Items[1].Name = "Default";
            menu.Items[1].Text = "Default";
            menu.Items[1].Image.Url = "~/images/new/replace2(2).png";

            menu.AutoPostBack = true;
            menu.ItemImagePosition = ImagePosition.Top;
        }

        /// <summary>
        /// ky funsksion sherben per te bere enable ose disable butonat e faqeve te ndryshme sipas te drejtave
        /// </summary>
        /// <param name="emriFaqes"> emri i faqes</param>
        /// <param name="menu">kontrolli i menuse</param>
        public static void percaktoTedrejtatPerKeteFaqe(string emriFaqes, ASPxMenu menu)
        {

            clsKomponente oKomponente = new clsKomponente(emriFaqes);
            int indexKomponenteje = oKomponente.IdKomponente;


        }

        /// <summary>
        /// perdoret per te shfaqur daten ne formatin dd/MM/yyyy,
        /// pershtatur per te kaluar nje grup datash ne nje thirrje te vetme --GETSON
        /// </summary>
        /// <param name="d"> kontrolli date edit</param>
        public static void vendosDateEditMask(params ASPxDateEdit[] d)
        {
            for (int i = 0; i < d.Length; i++)
            {
                VendosDateEditMask(d[i]);
            }
        }

        public static void vendosDateEditMask(params DateEditProperties[] d)
        {
            for (var i = 0; i < d.Length; i++)
            {
                VendosDateEditMask(d[i]);
            }
        }

        public static void InicializoDate(DateTime data, params ASPxDateEdit[] d)
        {
            for (int i = 0; i < d.Length; i++)
            {
                d[i].Value = data;
            }
        }


        private static void VendosDateEditMask(ASPxDateEdit d)
        {
            d.EditFormat = EditFormat.Custom;
            d.UseMaskBehavior = true;
            d.EditFormatString = "dd/MM/yyyy";
        }
        private static void VendosDateEditMask(DateEditProperties d)
        {
            d.EditFormat = EditFormat.Custom;
            d.UseMaskBehavior = true;
            d.EditFormatString = "dd/MM/yyyy";
        }

        /// <summary>
        ///kjo metode sherben per shfaqur linqet qe te shpien ne dokumentat qe e kane lidhur kete dokument
        /// </summary>
        /// <param name="idPerdoruesi"> id e perdoruesit</param>
        /// <param name="idViti">id e vitit</param>
        /// <param name="idNdermarrje">id e ndermarjes</param>
        /// <param name="t">tabela html ne te cilen do te shtohen linqet</param>
        /// <param name="dtlidhur">tabela me id e dokumentave qe e lidhin kete dokument</param>
        /// <param name="idgjenerues"> id e dokumentit gjenerues</param>
        /// <param name="idnivelgjenerues"> id e nivelit gjenerues</param>
        /// <param name="idkonfiggjenerues"> id e konfigurimit gjenerues</param>
        public static void ShtoLidhje(int idPerdoruesi, int idViti, int idNdermarrje, HtmlTable t, DataTable dtlidhur, int idgjenerues, int idnivelgjenerues, int idkonfiggjenerues, int idGjuha)
        {
            ImbLogger.LogTraceShitje("Filloi metoda ShtoLidhje");
            const int id = 0;
            const string index = "&indexrow=0";
            CultureInfo ci = MessagesResource.KtheCultureInfo(idGjuha);
            ResourceManager rm = new ResourceManager("Resources.Strings", Assembly.Load("App_GlobalResources"));
            if (dtlidhur.Rows.Count == 0)/// nqs dokumenti nuk ka dokumenta lidhes e shfaqim si dokument kryesor
            {
                HtmlTableRow row = new HtmlTableRow();
                HtmlTableCell cell = new HtmlTableCell();
                ASPxLabel label = new ASPxLabel();
                label.Text = rm.GetString("msgDokumentKryesor", ci);
                t.Rows.Add(row);
                cell.Controls.Add(label);
                row.Cells.Add(new HtmlTableCell());
                row.Cells.Add(cell);
                return;
            }
            try
            {
                int i = 1;
                foreach (DataRow dr in dtlidhur.Rows)
                {
                    clsKonfigurimAmbjenti konf = new clsKonfigurimAmbjenti();
                    string url = "";
                    string text = "";
                    string lloji = "";
                    bool enabled = true;
                    switch (dr["tipi"].ToString())
                    {
                        case "Shperndarje":///kur dokumenti eshte perdorur tek dokumenti i shperndarjes se shpenzimeve
                            clsShperndarjeShpenzimeKoka shpk = new clsShperndarjeShpenzimeKoka(Int32.Parse(dr[id].ToString()));
                            konf.mbushKonfigAmbjSipasId(shpk.IdKonfigAmbjente, idGjuha);
                            enabled = clsFunksione.kaTeDrejteTeHapeAmbjentin(idPerdoruesi, idNdermarrje, idViti, "Shto_ShperndarjeShpenzimesh.aspx", false, true, shpk.IdKokaShperndarjeShpenz, idGjuha);
                            text = " I lidhur nga dokumenti " + konf.KodKonfigAmbjente + " Nr." + shpk.NrDok + " Date: " + shpk.DtDok.ToShortDateString();
                            url = "Shto_ShperndarjeShpenzimesh.aspx?shtim_modifikim=modifikim&id=" + dr[id].ToString() + index;
                            break;

                        case "FleteDok":/// kur dokumenti eshte perdorur tek dokumenti i fletes kontabel
                            clsFleteDoganoreKoka fd = new clsFleteDoganoreKoka(Int32.Parse(dr[id].ToString()));
                            konf.mbushKonfigAmbjSipasId(fd.IdKonfigAmbjente, idGjuha);
                            if (fd.ImportExport == 1)
                                lloji = "import";
                            else
                                lloji = "export";
                            enabled = clsFunksione.kaTeDrejteTeHapeAmbjentin(idPerdoruesi, idNdermarrje, idViti, "Shto_FleteDoganore.aspx?lloji=" + lloji, false, true, fd.IdFleteDoganoreKoka, idGjuha);
                            url = "Shto_FleteDoganore.aspx?lloji=" + lloji + "&id=" + dr[id].ToString() + index + "&shtim_modifikim=modifikim";
                            text = " I lidhur nga dokumenti " + konf.KodKonfigAmbjente + " Nr." + fd.NrDok + " Date: " + fd.DtDok.ToShortDateString();
                            break;

                        case "dlidhesbanka":/// kur dokumenti perdoret tek veprimet e bankes
                            //clsKonfigurimAmbjenti konfgjenerues = new clsKonfigurimAmbjenti(int.Parse(dr["idkonfigambjente"].ToString()));

                            if (clsKonfigurimAmbjenti.ktheIdKategori(Int32.Parse(dr["idkonfigambjente"].ToString())) != 20)
                            {
                                clsVeprimBankaKoka vbk = new clsVeprimBankaKoka(Int32.Parse(dr[id].ToString()));
                                if (vbk.IdBanka == 0)
                                    return; // KEVI nese nuk e gjen te mos japi error po mos ta shtoj
                                konf.mbushKonfigAmbjSipasId(vbk.IdKonfigAmbjente, idGjuha);
                                enabled = clsFunksione.kaTeDrejteTeHapeAmbjentin(idPerdoruesi, idNdermarrje, idViti, "ShtoVeprimBanka.aspx?lloji=" + vbk.LlojiVeprimit.ToLower(), false, true, vbk.IdKoka, idGjuha);
                                url = "ShtoVeprimBanka.aspx?lloji=" + vbk.LlojiVeprimit.ToLower() + "&id=" + dr[id].ToString() + "&numer=" + vbk.NrDokumenti + index + "&shtim_modifikim=modifikim";
                                text = " I lidhur nga dokumenti " + konf.KodKonfigAmbjente + " Nr." + vbk.NrDokumenti + " Date: " + vbk.DateDokumenti.ToShortDateString();
                            }
                            else
                            {
                                clsVeprimeKFKoka vkf = new clsVeprimeKFKoka(Int32.Parse(dr[id].ToString()));
                                enabled = clsFunksione.kaTeDrejteTeHapeAmbjentin(idPerdoruesi, idNdermarrje, idViti, "Shto_VeprimeKF.aspx", false, true, vkf.IdVeprimeKFKoka, idGjuha);
                                url = "Shto_VeprimeKF.aspx?id=" + Int32.Parse(dr[id].ToString()) + "&numer=" + vkf.NrDok + index + "&shtim_modifikim=modifikim";
                                text = " I lidhur nga dokumenti " + konf.KodKonfigAmbjente + " Nr." + vkf.NrDok + " Date: " + vkf.DtDok.ToShortDateString();
                            }
                            break;

                        case "dlidheskthim":/// kur dokumenti perdoret tek khtimet
                            //clsKonfigurimAmbjenti konfgjenerues = new clsKonfigurimAmbjenti(int.Parse(dr["idkonfigambjente"].ToString()));

                            if (clsKonfigurimAmbjenti.ktheIdKategori(Int32.Parse(dr["idkonfigambjente"].ToString())) == 1)
                            {
                                clsKokaShitje shitje = new clsKokaShitje();
                                shitje.mbushKokaShitjeSipasIDPaTrup(Int32.Parse(dr[id].ToString()));
                                enabled = clsFunksione.kaTeDrejteTeHapeAmbjentin(idPerdoruesi, idNdermarrje, idViti, "Shto_RegjistrimDokumentash.aspx?shitje_blerje=shitje", false, true, shitje.IdShitjeKoka, idGjuha);
                                url = "Shto_RegjistrimDokumentash.aspx?shitje_blerje=shitje&id=" + Int32.Parse(dr[id].ToString()) + "&numer=" + shitje.NrDok + index + "&shtim_modifikim=modifikim";
                                text = " I lidhur nga dokumenti " + konf.KodKonfigAmbjente + " Nr." + shitje.NrDok + " Date: " + shitje.DtDok.ToShortDateString();
                            }
                            else
                            {
                                clsKokaShitje shitje = new clsKokaShitje();
                                shitje.mbushKokaShitjeSipasIDPaTrup(Int32.Parse(dr[id].ToString()));
                                enabled = clsFunksione.kaTeDrejteTeHapeAmbjentin(idPerdoruesi, idNdermarrje, idViti, "Shto_RegjistrimDokumentash.aspx?shitje_blerje=blerje", false, true, shitje.IdShitjeKoka, idGjuha);
                                url = "Shto_RegjistrimDokumentash.aspx?shitje_blerje=blerje&id=" + Int32.Parse(dr[id].ToString()) + "&numer=" + shitje.NrDok + index + "&shtim_modifikim=modifikim";
                                text = " I lidhur nga dokumenti " + konf.KodKonfigAmbjente + " Nr." + shitje.NrDok + " Date: " + shitje.DtDok.ToShortDateString();
                            }
                            break;

                        case "dlidhes":/// kur dokumenti eshte perdorur ne ambjentin e lidhjes se dokumentave
                            clsDokumentLidhesKoka dlk = new clsDokumentLidhesKoka();
                            dlk.IdKoka = Int32.Parse(dr[id].ToString());
                            dlk.merrSipasId();
                            konf.mbushKonfigAmbjSipasId(dlk.IdKonfigAmbjente, idGjuha);
                            enabled = clsFunksione.kaTeDrejteTeHapeAmbjentin(idPerdoruesi, idNdermarrje, idViti, "LidhjaDokumentave.aspx", false, true, dlk.IdKoka, idGjuha);
                            url = "LidhjaDokumentave.aspx?id=" + dr[id].ToString() + "&numer=" + dlk.NrLidhje + index + "&shtim_modifikim=modifikim";
                            text = " I lidhur nga dokumenti " + konf.KodKonfigAmbjente + " Nr." + dlk.NrLidhje + " Date: " + dlk.DateDokumenti.ToShortDateString();

                            break;

                        case "transferuar":/// rast specifik per vodafonin kur dokumenti eshte transferuar ne ndermarjen meme
                            enabled = false;
                            url = "";
                            text = " I transferuar ne ndermarjen meme ";
                            break;

                        case "transferuarmeme":/// rast specifik per vodafonin kur dokumenti eshte transferuar nga ndermarja meme tek bija
                            enabled = false;
                            url = "";
                            text = " I transferuar nga ndermarrja meme ";
                            break;

                        case "transferuarwk":/// rast specifik per vodafonin kur dokumenti eshte transferuar nga Magazina Vodafone (ish winline karta)
                            enabled = false;
                            url = "";
                            text = " Transferim nga Magazina Vodafone ";
                            break;

                        case "transferuarngamema":
                            enabled = false;
                            url = "";
                            text = " I transferuar nga ndermarrja meme ";
                            break;
                        case "transferuar_teknika":
                            enabled = false;
                            url = "";
                            text = "Transferuar";
                            break;
                        case "anulluar":
                            clsVeprimBankaKoka bank = new clsVeprimBankaKoka(Int32.Parse(dr[id].ToString()));

                            konf.mbushKonfigAmbjSipasId(bank.IdKonfigAmbjente);
                            enabled = clsFunksione.kaTeDrejteTeHapeAmbjentin(idPerdoruesi, idNdermarrje, idViti, "ShtoVeprimBanka.aspx?lloji=pagese", false, true, bank.IdKoka, idGjuha);// teDrejta(idPerdoruesi, idNderViti, idNdermarrje, "LidhjaDokumentave.aspx");
                            url = "ShtoVeprimBanka.aspx?lloji=pagese&id=" + dr[id].ToString() + "&numer=" + bank.NrDokumenti + index + "&shtim_modifikim=modifikim";
                            text = " I anulluar nga dokumenti " + konf.KodKonfigAmbjente + " Nr." + bank.NrDokumenti + " Date: " + bank.DateDokumenti.ToShortDateString();

                            break;
                        case "prodhim":///kur planifikimi eshte ekzekutuar ne prodhim
                            clsKokaEkzekutim ekz = new clsKokaEkzekutim(Int32.Parse(dr[id].ToString()));
                            konf.mbushKonfigAmbjSipasId(ekz.IdKonfigAmbjente, idGjuha);
                            enabled = clsFunksione.kaTeDrejteTeHapeAmbjentin(idPerdoruesi, idNdermarrje, idViti, "Shto_Ekzekutim.aspx", false, true, ekz.IdKokaEkzekutim, idGjuha);
                            url = "Shto_Ekzekutim.aspx?id=" + dr[id].ToString() + "&numer=" + ekz.NrDok + index + "&shtim_modifikim=modifikim";
                            text = " I lidhur nga dokumenti " + konf.KodKonfigAmbjente + " Nr." + ekz.NrDok + " Date: " + ekz.DtDok.ToShortDateString();
                            break;

                        case "skedulim":///kur planifikimi eshte perdorur ne skedulim prodhimi
                            clsKokaSkedulimProdhimi sked = new clsKokaSkedulimProdhimi(Int32.Parse(dr[id].ToString()));
                            konf.mbushKonfigAmbjSipasId(sked.IdKonfigAmbjente, idGjuha);
                            enabled = clsFunksione.kaTeDrejteTeHapeAmbjentin(idPerdoruesi, idNdermarrje, idViti, "Shto_SkedulimProdhimi.aspx", false, true, sked.IdKoka, idGjuha);
                            url = "Shto_SkedulimProdhimi.aspx?id=" + dr[id].ToString() + "&numer=" + sked.NrDok + index + "&shtim_modifikim=modifikim";
                            text = " I lidhur nga dokumenti " + konf.KodKonfigAmbjente + " Nr." + sked.NrDok + " Date: " + sked.DtDok.ToShortDateString();
                            break;

                        case "planifikim":///kur urdher shitja eshte gjeneruar ne planifikim
                            clsKokaPlanifikim pl = new clsKokaPlanifikim(Int32.Parse(dr[id].ToString()));
                            konf.mbushKonfigAmbjSipasId(pl.IdKonfigAmbjente, idGjuha);
                            enabled = clsFunksione.kaTeDrejteTeHapeAmbjentin(idPerdoruesi, idNdermarrje, idViti, "Shto_Planifikim.aspx", false, true, pl.IdKokaPlanifikim, idGjuha);
                            url = "Shto_Planifikim.aspx?id=" + dr[id].ToString() + "&numer=" + pl.NrDok + index + "&shtim_modifikim=modifikim";
                            text = " I lidhur nga dokumenti " + konf.KodKonfigAmbjente + " Nr." + pl.NrDok + " Date: " + pl.DtDok.ToShortDateString();
                            break;

                        case "gjeneruar":/// rastet kur dokumenti eshte gjeneruar nga nje dokument tjeter
                            if (idgjenerues == -20)//mbyllje viti
                            {
                                enabled = false;
                                url = "";
                                text = " Dokument i krijuar nga mbyllja e vitit";
                                break;
                            }
                            int idKategori = clsNivelRegjistrimi.ktheIdKategoriaNivelRegjistrimi(idnivelgjenerues);
                            konf.mbushKonfigAmbjSipasId(idkonfiggjenerues, idGjuha);
                            switch (idKategori)
                            {
                                case 1:///shitja
                                    clsKokaShitje shitje = new clsKokaShitje();
                                    shitje.mbushKokaShitjeSipasIDPaTrup(idgjenerues);
                                    enabled = clsFunksione.kaTeDrejteTeHapeAmbjentin(idPerdoruesi, idNdermarrje, idViti, "Shto_RegjistrimDokumentash.aspx?shitje_blerje=shitje", false, true, shitje.IdShitjeKoka, idGjuha);
                                    url = "Shto_RegjistrimDokumentash.aspx?shitje_blerje=shitje&id=" + idgjenerues + "&numer=" + shitje.NrDok + index + "&shtim_modifikim=modifikim";
                                    text = " Gjeneruar nga dokumenti " + konf.KodKonfigAmbjente + " Nr." + shitje.NrDok + " Date: " + shitje.DtDok.ToShortDateString();
                                    break;

                                case 2:///blerja
                                    clsKokaShitje blerje = new clsKokaShitje();
                                    blerje.mbushKokaShitjeSipasIDPaTrup(idgjenerues);
                                    enabled = clsFunksione.kaTeDrejteTeHapeAmbjentin(idPerdoruesi, idNdermarrje, idViti, "Shto_RegjistrimDokumentash.aspx?shitje_blerje=blerje", false, true, blerje.IdShitjeKoka, idGjuha);
                                    url = "Shto_RegjistrimDokumentash.aspx?shitje_blerje=blerje&id=" + idgjenerues + "&numer=" + blerje.NrDok + index + "&shtim_modifikim=modifikim";
                                    text = " Gjeneruar nga dokumenti " + konf.KodKonfigAmbjente + " Nr." + blerje.NrDok + " Date: " + blerje.DtDok.ToShortDateString();
                                    break;

                                case 3:
                                case 4:/// arka banka
                                    clsVeprimBankaKoka vb = new clsVeprimBankaKoka(idgjenerues);
                                    enabled = clsFunksione.kaTeDrejteTeHapeAmbjentin(idPerdoruesi, idNdermarrje, idViti, "ShtoVeprimBanka.aspx?lloji=" + vb.LlojiVeprimit.ToLower(), false, true, vb.IdKoka, idGjuha);
                                    url = "ShtoVeprimBanka.aspx?lloji=" + vb.LlojiVeprimit.ToLower() + "&id=" + idgjenerues + "&numer=" + vb.NrDokumenti + index + "&shtim_modifikim=modifikim";
                                    text = " Gjeneruar nga dokumenti " + konf.KodKonfigAmbjente + " Nr." + vb.NrDokumenti + " Date: " + vb.DateDokumenti.ToShortDateString();
                                    break;

                                case 5:///fleta kontabel
                                    clsKokaFleteKontabel fk = new clsKokaFleteKontabel(idgjenerues);
                                    enabled = clsFunksione.kaTeDrejteTeHapeAmbjentin(idPerdoruesi, idNdermarrje, idViti, "Shto_FleteKontabel.aspx", false, true, fk.IdKokaFleteKontabel, idGjuha);
                                    url = "Shto_FleteKontabel.aspx?id=" + idgjenerues + "&numur=" + fk.NrDukumentiKokaFleteKontabel + "&shtim_modifikim=modifikim";
                                    text = " Gjeneruar nga dokumenti " + konf.KodKonfigAmbjente + " Nr." + fk.NrDukumentiKokaFleteKontabel + " Date: " + fk.DateDokumentiKokaFleteKontabel.ToShortDateString();
                                    break;

                                case 6:/// magazina
                                    clsKokaMagazina mag = new clsKokaMagazina();
                                    mag.mbushKokaMagazinaSipasID(idgjenerues);
                                    if (mag.IdLlojDokumentiMagazine == 1)
                                        lloji = "hyrje";
                                    else lloji = "dalje";
                                    enabled = clsFunksione.kaTeDrejteTeHapeAmbjentin(idPerdoruesi, idNdermarrje, idViti, "Shto_RegjistrimMagazine.aspx?lloj=" + lloji, false, true, mag.IdKokaMagazina, idGjuha);
                                    url = "Shto_RegjistrimMagazine.aspx?lloj=" + lloji + "&id=" + idgjenerues + "&numer=" + mag.NrDok + index + "&shtim_modifikim=modifikim";
                                    text = " Gjeneruar nga dokumenti " + konf.KodKonfigAmbjente + " Nr." + mag.NrDok + " Date: " + mag.DtDok.ToShortDateString();
                                    break;

                                case 95:///ndryshim cmim sasi
                                    clsKokaNdryshimCmimSasi ndryshim = new clsKokaNdryshimCmimSasi();
                                    ndryshim.mbushKokaNdryshimCmimSasiSipasID(idgjenerues);

                                    enabled = clsFunksione.kaTeDrejteTeHapeAmbjentin(idPerdoruesi, idNdermarrje, idViti, "Shto_RegjistrimNdryshimCmimSasi.aspx", false, true, ndryshim.IdKoka, idGjuha);
                                    url = "Shto_RegjistrimNdryshimCmimSasi.aspx?shtim_modifikim=modifikim&id=" + idgjenerues + "&numer=" + ndryshim.NrDok + index + "";
                                    text = " Gjeneruar nga dokumenti " + konf.KodKonfigAmbjente + " Nr." + ndryshim.NrDok + " Date: " + ndryshim.DtDok.ToShortDateString();
                                    break;

                                case 7:///shperndarje shpenzimesh
                                    clsShperndarjeShpenzimeKoka shp = new clsShperndarjeShpenzimeKoka(Int32.Parse(dr[id].ToString()));
                                    enabled = clsFunksione.kaTeDrejteTeHapeAmbjentin(idPerdoruesi, idNdermarrje, idViti, "Shto_ShperndarjeShpenzimesh.aspx", false, true, shp.IdKokaShperndarjeShpenz, idGjuha);
                                    url = "Shto_ShperndarjeShpenzimesh.aspx?id=" + idgjenerues + index + "&shtim_modifikim=modifikim";
                                    text = " Gjeneruar nga dokumenti " + konf.KodKonfigAmbjente + " Nr." + shp.NrDok + " Date: " + shp.DtDok.ToShortDateString();
                                    break;

                                case 8:///fleta doganore
                                    clsFleteDoganoreKoka fdk = new clsFleteDoganoreKoka(Int32.Parse(dr[id].ToString()));
                                    if (fdk.ImportExport == 1)
                                        lloji = "import";
                                    else
                                        lloji = "export";
                                    enabled = clsFunksione.kaTeDrejteTeHapeAmbjentin(idPerdoruesi, idNdermarrje, idViti, "Shto_FleteDoganore.aspx?lloji=" + lloji, false, true, fdk.IdFleteDoganoreKoka, idGjuha);
                                    url = "Shto_FleteDoganore.aspx?lloji=" + lloji + "&id=" + idgjenerues + index + "&shtim_modifikim=modifikim";
                                    text = " Gjeneruar nga dokumenti " + konf.KodKonfigAmbjente + " Nr." + fdk.NrDok + " Date: " + fdk.DtDok.ToShortDateString();
                                    break;

                                case 10:///dokumenta lidhes
                                    clsDokumentLidhesKoka dl = new clsDokumentLidhesKoka();
                                    dl.IdKoka = Int32.Parse(dr[id].ToString());
                                    dl.merrSipasId();
                                    enabled = clsFunksione.kaTeDrejteTeHapeAmbjentin(idPerdoruesi, idNdermarrje, idViti, "LidhjaDokumentave.aspx", false, true, dl.IdKoka, idGjuha);
                                    url = "LidhjaDokumentave.aspx?id=" + idgjenerues + "&numer=" + dl.NrLidhje + index + "&shtim_modifikim=modifikim";
                                    text = " Gjeneruar nga dokumenti " + konf.KodKonfigAmbjente + " Nr." + dl.NrLidhje + " Date: " + dl.DateDokumenti.ToShortDateString();
                                    break;

                                case 11: /// azhornim kf
                                    clsAzhornimKFKoka azh = new clsAzhornimKFKoka(idgjenerues);
                                    enabled = clsFunksione.kaTeDrejteTeHapeAmbjentin(idPerdoruesi, idNdermarrje, idViti, "Shto_AzhornimKlientFurnitor.aspx?vep=azhornim", false, true, azh.IdAzhornimKfKoka, idGjuha);
                                    url = "Shto_AzhornimKlientFurnitor.aspx?vep=azhornim&id=" + idgjenerues + "&numer=" + azh.NrDok + index + "&shtim_modifikim=modifikim";
                                    text = " Gjeneruar nga dokumenti " + konf.KodKonfigAmbjente + " Nr." + azh.NrDok + " Date: " + azh.DateDok.ToShortDateString();
                                    break;

                                case 64:/// mbyllje kf
                                    clsKokaMbylljeKF mbyllje = new clsKokaMbylljeKF(idgjenerues);
                                    enabled = clsFunksione.kaTeDrejteTeHapeAmbjentin(idPerdoruesi, idNdermarrje, idViti, "Shto_AzhornimKlientFurnitor.aspx?vep=mbyllje", false, true, mbyllje.IdKoka, idGjuha);
                                    url = "Shto_AzhornimKlientFurnitor.aspx?vep=mbyllje&id=" + idgjenerues + "&numer=" + mbyllje.NrDok + index + "&shtim_modifikim=modifikim";
                                    text = " Gjeneruar nga dokumenti " + konf.KodKonfigAmbjente + " Nr." + mbyllje.NrDok + " Date: " + mbyllje.DateDok.ToShortDateString();
                                    break;

                                case 20:/// veprime kf
                                    clsVeprimeKFKoka vkf = new clsVeprimeKFKoka(idgjenerues);
                                    enabled = clsFunksione.kaTeDrejteTeHapeAmbjentin(idPerdoruesi, idNdermarrje, idViti, "Shto_VeprimeKF.aspx", false, true, vkf.IdVeprimeKFKoka, idGjuha);
                                    url = "Shto_VeprimeKF.aspx?id=" + idgjenerues + "&numer=" + vkf.NrDok + index + "&shtim_modifikim=modifikim";
                                    text = " Gjeneruar nga dokumenti " + konf.KodKonfigAmbjente + " Nr." + vkf.NrDok + " Date: " + vkf.DtDok.ToShortDateString();
                                    break;

                                case 38:/// list pagesa
                                    clsKokaListPagese lp = new clsKokaListPagese(idgjenerues, false);
                                    enabled = clsFunksione.kaTeDrejteTeHapeAmbjentin(idPerdoruesi, idNdermarrje, idViti, "Shto_ListPagesa.aspx", false, true, lp.IdKoka, idGjuha);
                                    url = "Shto_ListPagesa.aspx?id=" + idgjenerues + "&numer=" + lp.NrDok + index + "&shtim_modifikim=modifikim";
                                    text = " Gjeneruar nga dokumenti " + konf.KodKonfigAmbjente + " Nr." + lp.NrDok + " Date: " + lp.DtDok.ToShortDateString();
                                    break;

                                case 45:/// ekzekutim prodhimi
                                    clsKokaEkzekutim pp = new clsKokaEkzekutim(idgjenerues);
                                    enabled = clsFunksione.kaTeDrejteTeHapeAmbjentin(idPerdoruesi, idNdermarrje, idViti, "Shto_Ekzekutim.aspx", false, true, pp.IdKokaEkzekutim, idGjuha);
                                    url = "Shto_Ekzekutim.aspx?id=" + idgjenerues + "&numer=" + pp.NrDok + index + "&shtim_modifikim=modifikim";
                                    text = " Gjeneruar nga dokumenti " + konf.KodKonfigAmbjente + " Nr." + pp.NrDok + " Date: " + pp.DtDok.ToShortDateString();
                                    break;

                                case 86:/// amortizimi
                                    clsAmortizimiKoka amortizimi = new clsAmortizimiKoka(idgjenerues);
                                    enabled = clsFunksione.kaTeDrejteTeHapeAmbjentin(idPerdoruesi, idNdermarrje, idViti, "Shto_RegjistrimAmortizimi.aspx", false, true, amortizimi.IdAmortizimi, idGjuha);
                                    url = "Shto_RegjistrimAmortizimi.aspx?id=" + idgjenerues + "&numer=" + amortizimi.NrDok + index + "&shtim_modifikim=modifikim";
                                    text = " Gjeneruar nga dokumenti " + konf.KodKonfigAmbjente + " Nr." + amortizimi.NrDok + " Date: " + amortizimi.DateDokumenti.ToShortDateString();
                                    break;

                                case 90:/// amortizimi
                                    clsAmortizimiKoka amortizimi1 = new clsAmortizimiKoka(idgjenerues);
                                    clsNivelRegjistrimi niv = new clsNivelRegjistrimi();
                                    niv.mbushNivelRegjistrimiSipasIdPaKonvertime(amortizimi1.IdNiveli);
                                    string llojiam = "amortizim";
                                    if (niv.Kodi.StartsWith("RIAM"))
                                        llojiam = "rivleresim";
                                    enabled = clsFunksione.kaTeDrejteTeHapeAmbjentin(idPerdoruesi, idNdermarrje, idViti, "Shto_RivleresimeAmortizimi.aspx?lloj=" + llojiam, false, true, amortizimi1.IdAmortizimi, idGjuha);
                                    url = "Shto_RivleresimeAmortizimi.aspx?lloj=" + llojiam + "&id=" + idgjenerues + "&numer=" + amortizimi1.NrDok + index + "&shtim_modifikim=modifikim";
                                    text = " Gjeneruar nga dokumenti " + konf.KodKonfigAmbjente + " Nr." + amortizimi1.NrDok + " Date: " + amortizimi1.DateDokumenti.ToShortDateString();
                                    break;

                                case 80:///rast specifik i vodafonit riparimet e aparateve
                                    clsKokaRiparime rip = new clsKokaRiparime(idgjenerues);
                                    enabled = clsFunksione.kaTeDrejteTeHapeAmbjentin(idPerdoruesi, idNdermarrje, idViti, "Shto_RegjistrimRiparimi.aspx", false, true, rip.IdKoka, idGjuha);
                                    url = "Shto_RegjistrimRiparimi.aspx?shtim_modifikim=modifikim&id=" + idgjenerues + "&numer=" + rip.NrKontakti + index;
                                    text = " Gjeneruar nga dokumenti " + konf.KodKonfigAmbjente + " Nr kontakti." + rip.NrKontakti + " Date: " + rip.DtDok.ToShortDateString();
                                    break;

                                case 78:/// rezervime
                                    clsKokaRezervime rez = new clsKokaRezervime();
                                    rez.mbushKokaRezervimiSipasID(idgjenerues);
                                    string kodNivelRegjistrimi = clsNivelRegjistrimi.ktheKodNivelRegjistrimi(rez.IdNivel);
                                    if (kodNivelRegjistrimi == "RH")
                                        lloji = "hyrje";
                                    else lloji = "dalje";
                                    enabled = clsFunksione.kaTeDrejteTeHapeAmbjentin(idPerdoruesi, idNdermarrje, idViti, "Shto_RegjistrimRezervimi.aspx?lloj=" + lloji, false, true, rez.IdKokaRezervimi, idGjuha);
                                    url = "Shto_RegjistrimRezervimi.aspx?lloj=" + lloji + "&id=" + idgjenerues + "&numer=" + rez.NrDok + index + "&shtim_modifikim=modifikim";
                                    text = " Gjeneruar nga dokumenti " + konf.KodKonfigAmbjente + " Nr." + rez.NrDok + " Date: " + rez.DtDok.ToShortDateString();
                                    break;
                                case 177:///rast gjenerimi nga dokument perfitim buxheti
                                    ClsBKokaBuxheti kokaBuxheti = new ClsBKokaBuxheti(idgjenerues);
                                    var dtDok = (DateTime)kokaBuxheti.DtDok;
                                    enabled = clsFunksione.kaTeDrejteTeHapeAmbjentin(idPerdoruesi, idNdermarrje, idViti, "B_Shto_RegjistrimDokumentBuxheti.aspx?lloji=perfitim", false, true, kokaBuxheti.IdBuxhetiKoka, idGjuha);
                                    url = "B_Shto_RegjistrimDokumentBuxheti.aspx?lloji=perfitim&id=" + idgjenerues + "&shtim_modifikim=modifikim" + index;
                                    text = " Gjeneruar nga dokumenti " + konf.KodKonfigAmbjente + " Nr dokumenti. " + kokaBuxheti.NrDok + " Date: " + dtDok.ToShortDateString();
                                    break;
                                default:
                                    if (idKategori <= 0) {
                                        ImbLogger.LogErrorShitje("Kategoria e nivelit te regjistrimit nuk ekziston");
                                        throw new MyException("Kategoria e nivelit te regjistrimit nuk ekziston");
                                    }
                                    break;
                            }
                            break;
                    }

                    HtmlTableRow row = new HtmlTableRow();
                    HtmlTableCell cell = new HtmlTableCell();
                    ASPxHyperLink hyperlink = new ASPxHyperLink();
                    hyperlink.NavigateUrl = url;
                    hyperlink.Text = text;
                    hyperlink.Enabled = enabled;
                    hyperlink.ID = "hyperlink" + i;

                    hyperlink.ClientInstanceName = "hyperlink" + i;
                    t.Rows.Add(row);
                    cell.Controls.Add(hyperlink);
                    row.Cells.Add(new HtmlTableCell());
                    row.Cells.Add(cell);
                    i++;
                }
            }
            catch (Exception)
            {
                ImbLogger.LogErrorShitje("Gabim gjate leximit te dokumentave lidhes");
                throw new Exception("ERROR: Gabim gjate leximit te dokumentave lidhes");
            }
            ImbLogger.LogTraceShitje("Mbaroi metoda ShtoLidhje");
        }

        /// <summary>
        /// Funksion qe sherben per perkthimin e popUp-eve
        /// </summary>
        /// <param name="popUp">Merr popUp</param>
        /// <param name="lblMsgbox">Merr label qe shfaqe mesazhin</param>
        /// <param name="ButtonCancel">Merr butonin cancel</param>
        public static void perkthePopUp(ASPxPopupControl popUp, string headerText, ASPxLabel lblMsgbox, string lblMsgBoxText, ASPxButton ButtonCancel, string cancelText, ASPxButton ok, string okText)
        {
            popUp.HeaderText = headerText;
            lblMsgbox.Text = lblMsgBoxText;
            if (ButtonCancel != null)
                ButtonCancel.Text = cancelText;
            if (ok != null)
                ok.Text = okText;
        }

        /// <summary>
        /// Funksion qe sherben per perkthimin e popUp-eve, rasti kur nuk ka button cancel
        /// </summary>
        /// <param name="popUp">Merr popUp</param>
        /// <param name="lblMsgbox">Merr label qe shfaqe mesazhin</param>
        public static void perkthePopUp(ASPxPopupControl popUp, string headerText, ASPxLabel lblMsgbox, string lblMsgBoxText)
        {
            perkthePopUp(popUp, headerText, lblMsgbox, lblMsgBoxText, null, null, null, null);
        }

        /// <summary>
        /// Funksion qe sherben per perkthimin e popUp-eve
        /// </summary>
        /// <param name="popUp">Merr popUp</param>
        /// <param name="lblMsgbox">Merr label qe shfaqe mesazhin</param>
        public static void perkthePopUp(ASPxPopupControl popUp, string headerText, ASPxLabel lblMsgbox, string lblMsgBoxText, ASPxButton ButtonCancel, string cancelText)
        {
            perkthePopUp(popUp, headerText, lblMsgbox, lblMsgBoxText, ButtonCancel, cancelText, null, null);
        }

        public static bool RedirectOnCallback(string url)
        {
            ASPxWebControl.RedirectOnCallback(url);
            return true;
        }
    }
}
