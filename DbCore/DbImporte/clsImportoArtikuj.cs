using DbCore.DbAdmin;
using DbCore.DbInventari;
using DbCore.DbKontabiliteti;
using DbCore.DbShare;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.DbImporte
{
    public class clsImportoArtikuj
    {
        public static clsMesazh importoArtikuj(DataTable dt, ref DataTable gabime, ref DataTable tePaImportuara, bool importo, int pozicionkodi, ResourceManager rm, CultureInfo ci, int idPerdorues, int idndermarje, int idNdermVit, int idGjuha, colTrupiFormatImporti col, bool vjenNgaImportSQL, string emerTab)
        {
            clsMesazh mesazh = new clsMesazh(true);
            string error = "";

            int i = 1;

            clsTrupiFormatImporti llojVeprimiTrupFormati = col.Find(x => x.KodKontrolli == "Lloj veprimi" && x.Visible == true);

            (string emerNjesia1, string skema, string vleradefaultnjesia, string vleradefaultskema) vlerat = clsKokaFormatImporti.ktheVleraDefaultKontrolli(col);
            string emerNjesia1 = vlerat.emerNjesia1, skema = vlerat.skema, vleradefaultnjesia = vlerat.vleradefaultnjesia, vleradefaultskema = vlerat.vleradefaultskema;

            foreach (DataRow dr in dt.Rows)
            {
                colTrupiFormatImporti colCopy = Newtonsoft.Json.JsonConvert.DeserializeObject<colTrupiFormatImporti>(Newtonsoft.Json.JsonConvert.SerializeObject(col));
                string llojVeprimi = llojVeprimiTrupFormati == null ? "Shtim" : clsFunksione.vendosVlere(llojVeprimiTrupFormati, dr, out error);
                if (string.IsNullOrEmpty(llojVeprimi) || (llojVeprimi != EnumLlojVeprimiImporti.Modifikim.ToString() && llojVeprimi != EnumLlojVeprimiImporti.Shtim.ToString()))
                {
                    object[] arr = { dr[pozicionkodi], $"Lloji i veprimit nuk ekziston.", i };
                    gabime.Rows.Add(arr);
                    if (importo)
                    {
                        tePaImportuara.ImportRow(dr);
                        gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                    }
                    continue;
                }
                bool shtim = (llojVeprimi != EnumLlojVeprimiImporti.Modifikim.ToString());

                if (shtim)
                    mesazh = ImportoArtikujShtim(ref dt, ref gabime, ref tePaImportuara, importo, pozicionkodi, rm, ci, idPerdorues, idndermarje, colCopy, vjenNgaImportSQL, emerTab, dr, out error, i, vlerat.emerNjesia1, vlerat.skema, vlerat.vleradefaultnjesia, vlerat.vleradefaultskema);
                else
                    mesazh = ImportoArtikujModifikim(ref gabime, ref tePaImportuara, importo, pozicionkodi, rm, ci, idPerdorues, idndermarje, colCopy, vjenNgaImportSQL, emerTab, dr, out error, i);
                if (error != "")
                    continue;

            }
            if (!mesazh.Status)
                return new clsMesazh(false, rm.GetString("msgGabimNeImportim", ci));
            return new clsMesazh(true, rm.GetString("msgImportimMeSukses", ci));
        }

        private static clsMesazh ImportoArtikujShtim(ref DataTable dt, ref DataTable gabime, ref DataTable tePaImportuara, bool importo, int pozicionkodi, ResourceManager rm, CultureInfo ci, int idPerdorues, int idndermarje, colTrupiFormatImporti col, bool vjenNgaImportSQL, string emerTab, DataRow dr, out string error, int i, string emerNjesia1, string skemaFormat, string vleraDefaultNjesia, string vleraDefaultSkema)
        {
            clsMesazh mesazh = new clsMesazh();
            clsArtikulli art = new clsArtikulli();
            int llogaritjaKMSHArtikulli = 0;
            int zevendesimAutomatikArtikulli = 0;
            bool kontrollGjendje = false;
            decimal scrap = 0m;
            bool kontrollCmimi = true;
            int idkonfigArt = clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod("ART", idndermarje);
            int idkonfigAqt = clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod("AQT", idndermarje);


            string primaryKey = "IDIMPORTSHITJE";
            string ndermarrjeKey = "";
            bool shtim = true;
            decimal koeficenti = 0, peshabruto = 0, peshaneto = 0, minimum = 0, maksimum = 0, garancia = 0;
            string kodi = "", pershkrimi = "", kodbari = "", pershkrimi2 = "", niveltvsh = "", vendodhje = "", kodidoganor = "", origjine = "", kodkodifikim1 = "", kodkodifikim2 = "", kodkodifikim3 = "", njesiart1 = "", njesiaart2 = "", furnitor = "", kategoridet = "", detajim1 = "", kategoridet2 = "", detajim2 = "", klasa = "", skema = "", lloginv = "", llogblerje = "", llogshitje = "", llogtrete = "", llogshpenzimi = "", llogamor = "", llogpak = "", metodekostoje = "", objektiva = "", magazina = "", llojgarancie = "", autorizimet = "", pershkrimifurnitori = "", siperfaqjam2 = "", nrkontrate = "", nrpasurie = "", marka = "", modeli = "", shasia = "", zonakadastrale = "", vitprodhimi = "", tedhenateknike = "", nrLlogRez = "", nrLlogPakRez = "", llogariKomisioni = "", formatSeriali = "", llojVeprimiImporti = "", kodiIBarit = "";
            bool medatajim = false, kontrollgjendje = false, aktiv = false, irezervueshem = false, kontrollGjendjeDet2 = false, kontrollGjendjeDet1 = false, iShitshem = true, meserial = false, llojart = false, perpeshore = false, merezerveriv = false, prodhimmeporosi = false, llogaritKomision = false, iRimbursueshem = false;
            int idkonfig = 0, nrKaraktereTAC = 0, stokuMaxVfOne = 0;
            decimal sasinjesi = 1m;

            mesazh = clsKokaFormatImporti.kontrolloDataRow(dr, gabime, dt, tePaImportuara, importo, pozicionkodi, 13, col, i, emerNjesia1, skemaFormat, vleraDefaultNjesia, vleraDefaultSkema, false);
            if (!mesazh.Status)
            {
                error = mesazh.PershkrimMesazhi;
                return mesazh;
            }

            error = "";
            foreach (clsTrupiFormatImporti trup in col)
            {
                error = "";
                #region fushat
                switch (trup.KodKontrolli)
                {
                    case "Kod Ndermarrje":
                        ndermarrjeKey = trup.EmerImporti;
                        break;
                    case "Kodi":
                        kodi = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Pershkrimi":
                        pershkrimi = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Kodbari":
                        kodbari = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Pershkrimi 2":
                        pershkrimi2 = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Pershkrim Te Furnitori":
                        pershkrimifurnitori = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Nivel TVSH-je":
                        niveltvsh = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Vendodhja":
                        vendodhje = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Kodi doganor":
                        kodidoganor = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Origjina":
                        origjine = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Grupimi 1":
                        kodkodifikim1 = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Grupimi 2":
                        kodkodifikim2 = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Me detajim":
                        medatajim = clsFunksione.vendosBool(trup, dr, out error);
                        break;
                    case "Njesia 1":
                        njesiart1 = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Njesia 2":
                        njesiaart2 = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Koeficenti":
                        koeficenti = clsFunksione.vendosDecimal(trup, dr, out error);
                        break;
                    case "Furnitori Kryesor":
                        furnitor = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Kategori Detajimi":
                        kategoridet = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Pesha Bruto":
                        peshabruto = clsFunksione.vendosDecimal(trup, dr, out error);
                        break;
                    case "Detajim 1":
                        detajim1 = clsFunksione.vendosVlere(trup, dr, out error);
                        DateTime dt1 = new DateTime();
                        bool parseDt = DateTime.TryParse(detajim1, out dt1);
                        if (parseDt)
                            detajim1 = dt1.ToString("dd/MM/yyyy");
                        break;
                    case "Pesha Neto":
                        peshaneto = clsFunksione.vendosDecimal(trup, dr, out error);
                        break;
                    case "Kontroll Gjendje":
                        kontrollgjendje = clsFunksione.vendosBool(trup, dr, out error);
                        break;
                    case "Klasa":
                        klasa = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Skema":
                        skema = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Llogari Inventari":
                        lloginv = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Llogari Blerje":
                        llogblerje = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Llogari Shitje":
                        llogshitje = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Llogari tek te Tretet":
                        llogtrete = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Llogari Shpenzimi":
                        llogshpenzimi = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Gjendja min":
                        minimum = clsFunksione.vendosDecimal(trup, dr, out error);
                        break;
                    case "Gjendja max":
                        maksimum = clsFunksione.vendosDecimal(trup, dr, out error);
                        break;
                    case "Metode Kostoje":
                        metodekostoje = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Aktiv":
                        aktiv = clsFunksione.vendosBool(trup, dr, out error);
                        break;
                    case "Me serial":
                        meserial = clsFunksione.vendosBool(trup, dr, out error);
                        break;
                    case "Llogari Amortizimi":
                        llogamor = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Llogari Pakesimi":
                        llogpak = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Me Rezerve":
                        merezerveriv = clsFunksione.vendosBool(trup, dr, out error);
                        break;
                    case "Llogari Rezerve":
                        nrLlogRez = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Llogari Pakesim Rezerve":
                        nrLlogPakRez = clsFunksione.vendosVlere(trup, dr, out error);
                        break;

                    case "Kategori Detajimi 2":
                        kategoridet2 = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Detajim 2":
                        detajim2 = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Objektiva kosto":
                        objektiva = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Magazina":
                        magazina = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Garancia":
                        garancia = clsFunksione.vendosDecimal(trup, dr, out error);
                        break;
                    case "Lloji i garancise":
                        llojgarancie = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Lloji":
                        string lloj = clsFunksione.vendosVlere(trup, dr, out error);
                        if (lloj == "Afatshkurter")
                        {
                            llojart = false;
                            idkonfig = idkonfigArt;
                        }
                        else if (lloj == "Afatgjate")
                        {
                            llojart = true;
                            idkonfig = idkonfigAqt;
                        }
                        else
                            error = " i artikullit nuk eshte i sakte!";
                        break;
                    case "I Rezervueshem":
                        irezervueshem = clsFunksione.vendosBool(trup, dr, out error);
                        break;
                    case "Kontroll gjendje per detajimin 2":
                        kontrollGjendjeDet2 = clsFunksione.vendosBool(trup, dr, out error);
                        break;
                    case "Kontroll gjendje per detajimin 1":
                        kontrollGjendjeDet1 = clsFunksione.vendosBool(trup, dr, out error);
                        break;
                    case "I Shitshem":
                        iShitshem = clsFunksione.vendosBool(trup, dr, out error);
                        break;
                    case "Prodhim me porosi":
                        prodhimmeporosi = clsFunksione.vendosBool(trup, dr, out error);
                        break;
                    case "Sasi Njesi":
                        if (trup.Visible)
                            sasinjesi = clsFunksione.vendosDecimal(trup, dr, out error);
                        break;
                    case "Autorizimi":
                        autorizimet = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Per Peshore":
                        perpeshore = clsFunksione.vendosBool(trup, dr, out error);
                        break;
                    case "Siperfaqja M2":
                        siperfaqjam2 = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Nr Kontrate":
                        nrkontrate = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Nr Porosie":
                        nrpasurie = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Zona Kadastrale":
                        zonakadastrale = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Marka":
                        marka = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Modeli":
                        modeli = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Shasia":
                        shasia = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Vit Prodhimi":
                        vitprodhimi = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Te Dhena Teknike":
                        tedhenateknike = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Grupimi 3":
                        kodkodifikim3 = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Nr Karaktere TAC":
                        nrKaraktereTAC = clsFunksione.vendosInt(trup, dr, out error);
                        break;
                    case "Llogarit Komision":
                        llogaritKomision = clsFunksione.vendosBool(trup, dr, out error);
                        break;
                    case "Llogari Komisioni":
                        llogariKomisioni = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Format Seriali":
                        formatSeriali = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Stoku max per VFONE":
                        stokuMaxVfOne = clsFunksione.vendosInt(trup, dr, out error);
                        break;
                    case "Lloj veprimi":
                        llojVeprimiImporti = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Kodi i barit":
                        kodiIBarit = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "I rimbursueshem":
                        iRimbursueshem = clsFunksione.vendosBool(trup, dr, out error);
                        break;
                }


                #endregion

                if (error != "")
                {
                    object[] arr = { dr[pozicionkodi], trup.EmerImporti + error, i };
                    gabime.Rows.Add(arr);
                    if (importo)
                    {
                        tePaImportuara.ImportRow(dr);
                        gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                    }
                    break;
                }
            }
            if (error != "")
                return mesazh;


            if (njesiaart2 == "")
            {
                njesiaart2 = njesiart1;
                koeficenti = 1;
            }
            if (skema != "")
            {
                clsSkemaKontabilitetiArtikulli sk = new clsSkemaKontabilitetiArtikulli(skema, idndermarje, llojart);
                lloginv = sk.NrLlogariInventari;
                llogshitje = sk.NrLlogariShitje;
                llogshpenzimi = sk.NrLlogariShpenzimi;
                llogtrete = sk.NrLlogariTekTeTretet;
                llogblerje = sk.NrLlogariBlerje;
                llogamor = sk.NrLlogariAmortizimi;
                llogpak = sk.NrLlogariPakesimi;
            }
            try
            {
                clsKodbari kodbar = new clsKodbari();
                kodbar.Pershkrimi = DbCore.clsFunksione.ktheStringunPaHapesira(kodbari, true);
                colKodbare colKodbare = new colKodbare();
                if (kodbari != "")
                    colKodbare.Add(kodbar);

                (string nrAutom, Dictionary<string, object> hiddenFieldPerNrAuto) nrAutoObject = clsFunksione.ktheNrAutoPerKonfigurim(idkonfig, kodi, "txtKodi", "Kodi", 402, DateTime.Today, new clsDatabaseShare());
                string nrAutom = nrAutoObject.nrAutom;
                Dictionary<string, object> hfNrAuto = nrAutoObject.hiddenFieldPerNrAuto;

                art = art.krijoArtikullPerImport(new clsArtikulli(), clsFunksione.ktheStringunPaHapesira(nrAutom, true), clsFunksione.ktheStringunPaHapesira(pershkrimi, false), clsFunksione.ktheStringunPaHapesira(pershkrimi2, false), kodidoganor, vendodhje, kodkodifikim1, kodkodifikim2, origjine, njesiart1, njesiaart2, koeficenti, furnitor, peshabruto, peshaneto, medatajim, klasa, skema, lloginv, llogblerje, llogshitje, llogtrete, llogshpenzimi, llogamor, llogpak, nrLlogPakRez, nrLlogRez, minimum, maksimum, metodekostoje, llogaritjaKMSHArtikulli, zevendesimAutomatikArtikulli, idPerdorues, aktiv, kontrollGjendjeDet1, kontrollCmimi, kontrollgjendje, new colArtikulliPerberes(), new colFurnitoreArtikujsh(), new colArtikujtZevendesues(), new colVleraFushaShtese(), new DbKontabiliteti.colBuxhetet(), detajim1, llojart, idndermarje, niveltvsh, idkonfig, autorizimet, shtim, sasinjesi, scrap, prodhimmeporosi, kategoridet, true, kategoridet2, kontrollGjendjeDet2, objektiva, detajim2, magazina, irezervueshem, llojgarancie, garancia, iShitshem, meserial, false, rm, ci, colKodbare, perpeshore, pershkrimifurnitori, siperfaqjam2, nrkontrate, nrpasurie, zonakadastrale, shasia, marka, modeli, vitprodhimi, tedhenateknike, false, "", kodkodifikim3, false, new colArtikullVfone(), formatSeriali, merezerveriv, nrKaraktereTAC, llogaritKomision, llogariKomisioni, stokuMaxVfOne, "", false);
                if (importo)
                {
                    if (vjenNgaImportSQL)
                        mesazh = art.ruaj(hfNrAuto, new colCmimeArtikujsh(), new clsArtikullPerberesTemplateKoka(), vjenNgaImportSQL, dr[primaryKey].ToString(), emerTab, primaryKey, ndermarrjeKey);
                    else
                        mesazh = art.ruaj(hfNrAuto, new colCmimeArtikujsh(), new clsArtikullPerberesTemplateKoka(), vjenNgaImportSQL, "0", "", primaryKey, ndermarrjeKey);


                    if (!mesazh.Status)
                    {
                        object[] arr = { dr[pozicionkodi], mesazh.PershkrimMesazhi, i };
                        gabime.Rows.Add(arr);

                        tePaImportuara.ImportRow(dr);
                        gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                    }
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            finally
            {
                if (error != "")
                {
                    object[] arr = { dr[pozicionkodi], error, i };
                    gabime.Rows.Add(arr);
                    if (importo)
                    {
                        tePaImportuara.ImportRow(dr);
                        gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                    }
                }
                i++;
            }
            return mesazh;
        }


        private static clsMesazh ImportoArtikujModifikim(ref DataTable gabime, ref DataTable tePaImportuara, bool importo, int pozicionkodi, ResourceManager rm, CultureInfo ci, int idPerdorues, int idndermarje, colTrupiFormatImporti col, bool vjenNgaImportSQL, string emerTab, DataRow dr, out string error, int i)
        {
            int idkonfigArt = clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod("ART", idndermarje);
            int idkonfigAqt = clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod("AQT", idndermarje);

            string primaryKey = "IDIMPORTSHITJE";
            string ndermarrjeKey = "";
            bool shtim = false;
            decimal? koeficenti = null, peshabruto = null, peshaneto = null, minimum = null, maksimum = null, garancia = null, sasinjesi = null;
            String kodi = null , pershkrimi = null , kodbari = null , pershkrimi2 = null , niveltvsh = null , vendodhje = null , kodidoganor = null , origjine = null , kodkodifikim1 = null , kodkodifikim2 = null , kodkodifikim3 = null , njesiart1 = null , njesiaart2 = null , furnitor = null , kategoridet = null , detajim1 = null , kategoridet2 = null , detajim2 = null , klasa = null , skema = null , lloginv = null , llogblerje = null , llogshitje = null , llogtrete = null , llogshpenzimi = null , llogamor = null , llogpak = null , metodekostoje = null , objektiva = null , magazina = null , llojgarancie = null , autorizimet = null , pershkrimifurnitori = null , siperfaqjam2 = null , nrkontrate = null , nrpasurie = null , marka = null , modeli = null , shasia = null , zonakadastrale = null , vitprodhimi = null , tedhenateknike = null , nrLlogRez = null , nrLlogPakRez = null , llogariKomisioni = null , formatSeriali = null , llojVeprimiImporti ;
            bool? medatajim = null , kontrollgjendje = null , aktiv = null , irezervueshem = null , kontrollGjendjeDet2 = null , kontrollGjendjeDet1 = null , iShitshem= null , meserial = null , llojart = null , perpeshore = null , merezerveriv = null , prodhimmeporosi = null , llogaritKomision = null ;
            int? idkonfig= null , nrKaraktereTAC= null , stokuMaxVfOne = null;
            string kodiBarit = ""; bool? iRimbursueshem = null;
            clsTrupiFormatImporti kodiTrupFormati = col.Find(x => x.KodKontrolli == "Kodi");
            kodi = clsFunksione.ktheStringunPaHapesira(clsFunksione.vendosVlere(kodiTrupFormati, dr, out error), true);
            clsArtikulli artEkzistues = new clsArtikulli(kodi, idndermarje);

            if (artEkzistues.IdArtikulli <= 0)
            {
                object[] arr = { dr[pozicionkodi], $"Artikulli me kod {kodi} nuk ekziston.", i };
                gabime.Rows.Add(arr);
                if (importo)
                {
                    tePaImportuara.ImportRow(dr);
                    gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                }
                error = "Artikulli nuk ekziston!";
                return new MesazhGabimi();
            }

            clsMesazh mesazh = new clsMesazh();
            clsArtikulli art = new clsArtikulli();
            int llogaritjaKMSHArtikulli = artEkzistues.LlogaritjaKMSHArtikulli;
            int zevendesimAutomatikArtikulli = artEkzistues.ZevendesimAutomatikArtikulli;
            decimal scrap = artEkzistues.Scrap;
            bool kontrollCmimi = artEkzistues.KontrollCmimi;


            error = "";
            foreach (clsTrupiFormatImporti trup in col)
            {
                trup.VleraDefault = string.Empty;
                string vlera = clsFunksione.vendosVlere(trup, dr, out error);
                bool kaVlere = trup.Visible && trup.Shfaq && !string.IsNullOrEmpty(vlera);
                error = "";
                #region fushat
                switch (trup.KodKontrolli)
                {
                    case "Kod Ndermarrje":
                        ndermarrjeKey = trup.EmerImporti;
                        break;
                    case "Pershkrimi":
                        pershkrimi = !kaVlere ? null : clsFunksione.ktheStringunPaHapesira(vlera, false);
                        break;
                    case "Kodbari":
                        kodbari = !kaVlere ? null : vlera;
                        break;
                    case "Pershkrimi 2":
                        pershkrimi2 = !kaVlere ? null : clsFunksione.ktheStringunPaHapesira(vlera, false);
                        break;
                    case "Pershkrim Te Furnitori":
                        pershkrimifurnitori = !kaVlere ? null : vlera;
                        break;
                    case "Nivel TVSH-je":
                        niveltvsh =  !kaVlere ? null : vlera;
                        break;
                    case "Vendodhja":
                        vendodhje =  !kaVlere ? null : vlera;
                        break;
                    case "Kodi doganor":
                        kodidoganor =  !kaVlere ? null : vlera;
                        break;
                    case "Origjina":
                        origjine =  !kaVlere ? null : vlera;
                        break;
                    case "Grupimi 1":
                        kodkodifikim1 =  !kaVlere ? null : vlera;
                        break;
                    case "Grupimi 2":
                        kodkodifikim2 =  !kaVlere ? null : vlera;
                        break;
                    case "Me detajim":
                        medatajim = !kaVlere ? null : (bool?)clsFunksione.vendosBool(trup, dr, out error);
                        break;
                    case "Njesia 1":
                        njesiart1 =  !kaVlere ? null : vlera;
                        break;
                    case "Njesia 2":
                        njesiaart2 =  !kaVlere ? null : vlera;
                        break;
                    case "Koeficenti":
                        koeficenti = !kaVlere ? null : (decimal?)clsFunksione.vendosDecimal(trup, dr, out error);
                        break;
                    case "Furnitori Kryesor":
                        furnitor =  !kaVlere ? null : vlera;
                        break;
                    case "Kategori Detajimi":
                        kategoridet =  !kaVlere ? null : vlera;
                        break;
                    case "Pesha Bruto":
                        peshabruto = !kaVlere ? null : (decimal?)clsFunksione.vendosDecimal(trup, dr, out error);
                        break;
                    case "Detajim 1":
                        if (!kaVlere)
                            break;
                        detajim1 = vlera;
                        DateTime dt1 = new DateTime();
                        bool parseDt = DateTime.TryParse(detajim1, out dt1);
                        if (parseDt)
                            detajim1 = dt1.ToString("dd/MM/yyyy");
                        break;
                    case "Pesha Neto":
                        peshaneto = !kaVlere ? null : (decimal?)clsFunksione.vendosDecimal(trup, dr, out error);
                        break;
                    case "Kontroll Gjendje":
                        kontrollgjendje = !kaVlere ? null : (bool?)clsFunksione.vendosBool(trup, dr, out error);
                        break;
                    case "Klasa":
                        klasa =  !kaVlere ? null : vlera;
                        break;
                    case "Skema":
                        skema =  !kaVlere ? null : vlera;
                        break;
                    case "Llogari Inventari":
                        lloginv =  !kaVlere ? null : vlera;
                        break;
                    case "Llogari Blerje":
                        llogblerje =  !kaVlere ? null : vlera;
                        break;
                    case "Llogari Shitje":
                        llogshitje =  !kaVlere ? null : vlera;
                        break;
                    case "Llogari tek te Tretet":
                        llogtrete =  !kaVlere ? null : vlera;
                        break;
                    case "Llogari Shpenzimi":
                        llogshpenzimi =  !kaVlere ? null : vlera;
                        break;
                    case "Gjendja min":
                        minimum = !kaVlere ? null : (decimal?)clsFunksione.vendosDecimal(trup, dr, out error);
                        break;
                    case "Gjendja max":
                        maksimum = !kaVlere ? null : (decimal?)clsFunksione.vendosDecimal(trup, dr, out error);
                        break;
                    case "Metode Kostoje":
                        metodekostoje =  !kaVlere ? null : vlera;
                        break;
                    case "Aktiv":
                        aktiv = !kaVlere ? null : (bool?)clsFunksione.vendosBool(trup, dr, out error);
                        break;
                    case "Me serial":
                        meserial = !kaVlere ? null : (bool?)clsFunksione.vendosBool(trup, dr, out error);
                        break;
                    case "Llogari Amortizimi":
                        llogamor =  !kaVlere ? null : vlera;
                        break;
                    case "Llogari Pakesimi":
                        llogpak =  !kaVlere ? null : vlera;
                        break;
                    case "Me Rezerve":
                        merezerveriv = !kaVlere ? null : (bool?)clsFunksione.vendosBool(trup, dr, out error);
                        break;
                    case "Llogari Rezerve":
                        nrLlogRez =  !kaVlere ? null : vlera;
                        break;
                    case "Llogari Pakesim Rezerve":
                        nrLlogPakRez =  !kaVlere ? null : vlera;
                        break;

                    case "Kategori Detajimi 2":
                        kategoridet2 =  !kaVlere ? null : vlera;
                        break;
                    case "Detajim 2":
                        detajim2 =  !kaVlere ? null : vlera;
                        break;
                    case "Objektiva kosto":
                        objektiva =  !kaVlere ? null : vlera;
                        break;
                    case "Magazina":
                        magazina =  !kaVlere ? null : vlera;
                        break;
                    case "Garancia":
                        garancia = !kaVlere ? null : (decimal?)clsFunksione.vendosDecimal(trup, dr, out error);
                        break;
                    case "Lloji i garancise":
                        llojgarancie =  !kaVlere ? null : vlera;
                        break;
                    case "Lloji":
                        if (!kaVlere)
                            break;
                        string lloj = vlera;
                        if (lloj == "Afatshkurter")
                        {
                            llojart = false;
                            idkonfig = idkonfigArt;
                        }
                        else if (lloj == "Afatgjate")
                        {
                            llojart = true;
                            idkonfig = idkonfigAqt;
                        }
                        else
                            error = " i artikullit nuk eshte i sakte!";
                        break;
                    case "I Rezervueshem":
                        irezervueshem = !kaVlere ? null : (bool?)clsFunksione.vendosBool(trup, dr, out error);
                        break;
                    case "Kontroll gjendje per detajimin 2":
                        kontrollGjendjeDet2 = !kaVlere ? null : (bool?)clsFunksione.vendosBool(trup, dr, out error);
                        break;
                    case "Kontroll gjendje per detajimin 1":
                        kontrollGjendjeDet1 = !kaVlere ? null : (bool?)clsFunksione.vendosBool(trup, dr, out error);
                        break;
                    case "I Shitshem":
                        iShitshem = !kaVlere ? null : (bool?)clsFunksione.vendosBool(trup, dr, out error);
                        break;
                    case "Prodhim me porosi":
                        prodhimmeporosi = !kaVlere ? null : (bool?)clsFunksione.vendosBool(trup, dr, out error);
                        break;
                    case "Sasi Njesi":
                        if (trup.Visible)
                            sasinjesi = !kaVlere ? null : (decimal?)clsFunksione.vendosDecimal(trup, dr, out error);
                        break;
                    case "Autorizimi":
                        autorizimet =  !kaVlere ? null : vlera;
                        break;
                    case "Per Peshore":
                        perpeshore = !kaVlere ? null : (bool?)clsFunksione.vendosBool(trup, dr, out error);
                        break;
                    case "Siperfaqja M2":
                        siperfaqjam2 =  !kaVlere ? null : vlera;
                        break;
                    case "Nr Kontrate":
                        nrkontrate =  !kaVlere ? null : vlera;
                        break;
                    case "Nr Porosie":
                        nrpasurie =  !kaVlere ? null : vlera;
                        break;
                    case "Zona Kadastrale":
                        zonakadastrale =  !kaVlere ? null : vlera;
                        break;
                    case "Marka":
                        marka =  !kaVlere ? null : vlera;
                        break;
                    case "Modeli":
                        modeli =  !kaVlere ? null : vlera;
                        break;
                    case "Shasia":
                        shasia =  !kaVlere ? null : vlera;
                        break;
                    case "Vit Prodhimi":
                        vitprodhimi =  !kaVlere ? null : vlera;
                        break;
                    case "Te Dhena Teknike":
                        tedhenateknike =  !kaVlere ? null : vlera;
                        break;
                    case "Grupimi 3":
                        kodkodifikim3 =  !kaVlere ? null : vlera;
                        break;
                    case "Nr Karaktere TAC":
                        nrKaraktereTAC = !kaVlere ? null : (int?)clsFunksione.vendosInt(trup, dr, out error);
                        break;
                    case "Llogarit Komision":
                        llogaritKomision = !kaVlere ? null : (bool?)clsFunksione.vendosBool(trup, dr, out error);
                        break;
                    case "Llogari Komisioni":
                        llogariKomisioni =  !kaVlere ? null : vlera;
                        break;
                    case "Format Seriali":
                        formatSeriali =  !kaVlere ? null : vlera;
                        break;
                    case "Stoku max per VFONE":
                        stokuMaxVfOne = !kaVlere ? null : (int?)clsFunksione.vendosInt(trup, dr, out error);
                        break;
                    case "Lloj veprimi":
                        llojVeprimiImporti =  !kaVlere ? null : vlera;
                        break;
                    case "I rimbursueshem":
                        iRimbursueshem = !kaVlere ? null : (bool?)clsFunksione.vendosBool(trup, dr, out error);
                        break;
                    case "Kodi i barit":
                        kodiBarit = !kaVlere ? null : vlera;
                        break;
                }

                #endregion

                if (error != "")
                {
                    object[] arr = { dr[pozicionkodi], trup.EmerImporti + error, i };
                    gabime.Rows.Add(arr);
                    if (importo)
                    {
                        tePaImportuara.ImportRow(dr);
                        gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                    }
                    break;
                }
            }
            if (error != "")
                return mesazh;

            if (!string.IsNullOrEmpty(njesiart1) && string.IsNullOrEmpty(njesiaart2))
            {
                njesiaart2 = njesiart1;
                koeficenti = 1;
            }
            if (!string.IsNullOrEmpty(skema))
            {
                clsSkemaKontabilitetiArtikulli sk = new clsSkemaKontabilitetiArtikulli(skema, idndermarje, llojart == null ? artEkzistues.LlojiArt : (bool)llojart);
                lloginv = sk.NrLlogariInventari;
                llogshitje = sk.NrLlogariShitje;
                llogshpenzimi = sk.NrLlogariShpenzimi;
                llogtrete = sk.NrLlogariTekTeTretet;
                llogblerje = sk.NrLlogariBlerje;
                llogamor = sk.NrLlogariAmortizimi;
                llogpak = sk.NrLlogariPakesimi;
            }
            try
            {
                colKodbare colKodbare = new colKodbare(artEkzistues.IdArtikulli);
                if(!string.IsNullOrEmpty(kodbari))
                {
                    clsKodbari kodbar = new clsKodbari();
                    kodbar.Pershkrimi = DbCore.clsFunksione.ktheStringunPaHapesira(kodbari, true);
                    if (kodbari != "" && colKodbare.Find(x=>x.Pershkrimi == kodbar.Pershkrimi) == null)
                        colKodbare.Add(kodbar);
                }

                colArtikulliPerberes artikulliPerberes = new colArtikulliPerberes();
                colFurnitoreArtikujsh furnitoreArtikujsh = new colFurnitoreArtikujsh();
                colArtikujtZevendesues artikujZevendesues = new colArtikujtZevendesues();
                colVleraFushaShtese vleraFushaShtese = new colVleraFushaShtese();
                colBuxhetet buxhetet = new colBuxhetet();
                colArtikullVfone artikullVfone = new colArtikullVfone();
                bool artikullVjeter = false;
                string skemaBarkodit = string.Empty;
                bool meBarkodLogjik = false;
                bool mbetjeShitshme = false;

                if(artEkzistues.IdArtikulli > 0)
                {
                    artikulliPerberes.merrSipasIdArtikullKryesore(artEkzistues.IdArtikulli);
                    furnitoreArtikujsh.mbushFurnitoreArtikulliSipasIdArtikulli(artEkzistues.IdArtikulli);
                    artikujZevendesues.merrArtZevendesuesSipasIdArtikulli(artEkzistues.IdArtikulli);
                    vleraFushaShtese = new colVleraFushaShtese(artEkzistues.IdArtikulli, null);
                    buxhetet = new colBuxhetet(artEkzistues.IdArtikulli, "Artikulli");
                    artikullVfone.merrSipasIdArtikull(artEkzistues.IdArtikulli);
                    artikullVjeter = artEkzistues.ArtikullIVjeter;
                    skemaBarkodit = artEkzistues.SkemaBarkodit;
                    meBarkodLogjik = artEkzistues.MeBarkodLogjik;
                    mbetjeShitshme = artEkzistues.MbetjeShitshme;
                }

                art = art.krijoArtikullPerImport(artEkzistues, kodi, pershkrimi, pershkrimi2, kodidoganor, vendodhje, kodkodifikim1, kodkodifikim2, origjine, njesiart1, njesiaart2, koeficenti, furnitor, peshabruto, peshaneto, medatajim, klasa, skema, lloginv, llogblerje, llogshitje, llogtrete, llogshpenzimi, llogamor, llogpak, nrLlogPakRez, nrLlogRez, minimum, maksimum, metodekostoje, llogaritjaKMSHArtikulli, zevendesimAutomatikArtikulli, idPerdorues, aktiv, kontrollGjendjeDet1, kontrollCmimi, kontrollgjendje, artikulliPerberes, furnitoreArtikujsh, artikujZevendesues, vleraFushaShtese, buxhetet, detajim1, llojart, idndermarje, niveltvsh, idkonfig, autorizimet, shtim, sasinjesi, scrap, prodhimmeporosi, kategoridet, true, kategoridet2, kontrollGjendjeDet2, objektiva, detajim2, magazina, irezervueshem, llojgarancie, garancia, iShitshem, meserial, mbetjeShitshme, rm, ci, colKodbare, perpeshore, pershkrimifurnitori, siperfaqjam2, nrkontrate, nrpasurie, zonakadastrale, shasia, marka, modeli, vitprodhimi, tedhenateknike, meBarkodLogjik, skemaBarkodit, kodkodifikim3, artikullVjeter, artikullVfone, formatSeriali, merezerveriv, nrKaraktereTAC, llogaritKomision, llogariKomisioni, stokuMaxVfOne, kodiBarit, iRimbursueshem);
                if (importo)
                {
                    if (vjenNgaImportSQL)
                        mesazh = art.modifiko(new clsArtikullPerberesTemplateKoka(), new colCmimeArtikujsh(), vjenNgaImportSQL, dr[primaryKey].ToString(), emerTab, primaryKey, ndermarrjeKey);
                    else
                        mesazh = art.modifiko(new clsArtikullPerberesTemplateKoka(), new colCmimeArtikujsh(), vjenNgaImportSQL, "0", "", primaryKey, ndermarrjeKey);


                    if (!mesazh.Status)
                    {
                        object[] arr = { dr[pozicionkodi], mesazh.PershkrimMesazhi, i };
                        gabime.Rows.Add(arr);

                        tePaImportuara.ImportRow(dr);
                        gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                    }
                }
                else
                    mesazh = new MesazhSuksesi();
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            finally
            {
                if (error != "")
                {
                    object[] arr = { dr[pozicionkodi], error, i };
                    gabime.Rows.Add(arr);
                    if (importo)
                    {
                        tePaImportuara.ImportRow(dr);
                        gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                    }
                }
                i++;
            }
            return mesazh;
        }
    }

}
