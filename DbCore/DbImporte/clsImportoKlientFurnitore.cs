 using DbCore.DbAdmin;
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
    class clsImportoKlientFurnitore
    {
        public static clsMesazh importoKlientFurnitor(DataTable dt, ref DataTable gabime, ref DataTable tePaImportuara, bool importo, int pozicionkodi, ResourceManager rm, CultureInfo ci, int idPerdorues, int idndermarje, int idNdermVit, int idGjuha, colTrupiFormatImporti col, bool vjenNgaImportSQL, string emerTab)
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
                    mesazh = ImportoKlientFurnitoreShtim(ref dt, ref gabime, ref tePaImportuara, importo, pozicionkodi, rm, ci, idPerdorues, idndermarje, colCopy, vjenNgaImportSQL, emerTab, dr, out error, i, vlerat.emerNjesia1, vlerat.skema, vlerat.vleradefaultnjesia, vlerat.vleradefaultskema);
                else
                    mesazh = ImportoKlientFurnitoreModifikim(ref gabime, ref tePaImportuara, importo, pozicionkodi, rm, ci, idPerdorues, idndermarje, colCopy, vjenNgaImportSQL, emerTab, dr, out error, i);
                if (error != "")
                    continue;

            }
            if (!mesazh.Status)
                return new clsMesazh(false, rm.GetString("msgGabimNeImportim", ci));
            return new clsMesazh(true, rm.GetString("msgImportimMeSukses", ci));
        }

        private static clsMesazh ImportoKlientFurnitoreShtim(ref DataTable dt, ref DataTable gabime, ref DataTable tePaImportuara, bool importo, int pozicionkodi, ResourceManager rm, CultureInfo ci, int idPerdorues, int idndermarje, colTrupiFormatImporti col, bool vjenNgaImportSQL, string emerTab, DataRow dr, out string error, int i, string emerNjesia1, string skemaFormat, string vleraDefaultNjesia, string vleraDefaultSkema)
        {
            clsMesazh mesazh = new clsMesazh(true);
            error = "";
            const int idQenderKosto = 0;
            const int idFushata = 0;
            const bool shtim = true, kontrolloekzistence = true;
            const int idKategoriKlienti = 0, prioriteti = 0;
            const decimal vleraLimitPorositur = 0m, cmimUlet = 0, zbritjeTotal = 0;
            const bool ofertaAutomatike = false;
            const string nrLlogZbritje = "", kushtepagese = "", kushtedergimi = "", menyratransporti = "", adresaBanka = "";
            int vit = DateTime.Today.Year;
            //clsNdermarrje nder = new clsNdermarrje(idndermarje);
            int monndermarje = clsNdermarrje.ktheIdMonedheNdermSipasID(idndermarje);

            int idKonfigFurnitor = clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod("F", idndermarje);
            int idKonfigKlient = clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod("K", idndermarje);

            clsKlientFurnitor kf = new clsKlientFurnitor();
            int idkonfig = 0; //konf.IdKonfigAmbjente;
            int limitParalajmerues = 0, limitBllokues = 0;
            string kodi = "", emertimi = "", nrLlog = "", llojiklientfurnitor = "", titulli = "", aktivitetiKF = "", emerKerkimiKF = "", nipti = "", qyteti = "", shtetiKF = "", telKF = "", faxKF = "", celKF = "", emailKF = "", webpageKF = "", ibanKF = "", llogariBankareKF = "", maturimi = "", kategorizbritje = "", nivelcmimi = "", pershkrimzbritjeAnalitike = "", licenca = "", swift = "", kodemriBanka = "", kodgrupim1kf = "", kodgrupim2kf = "", kodgrupim3kf = "", nrtvsh = "", llojadrese = "", adresa = "", kodipostar = "", objektiva = "", pershkrimMetoda = "", kodPerfaqsuesShitje1 = "", kodPerfaqsuesShitje2 = "", llogDytesore = "", autorizimi = "", kodPerfaqsuesShitje3 = "",
                llojmarreveshje = "", idmarreveshje = "", tipiId = "", kodIsksh = "";
            bool aktiv = false, llojiKF = false, shitjePaTvsh = false, autongarkese = false, fermer = false, prospekt = false; int idklientfurnitorkryesor = 0; string kodklienti = ""; string shenime = "";
            string emertimFature = "", kodIntegrimi = ""; bool llogaritKomision = false, gjeneroKupon = false;

            string primaryKey = "IDIMPORTSHITJE";
            string ndermarrjeKey = "";
            
            mesazh = clsKokaFormatImporti.kontrolloDataRow(dr, gabime, dt, tePaImportuara, importo, pozicionkodi, 13, col, i, emerNjesia1, skemaFormat, vleraDefaultNjesia, vleraDefaultSkema, false);
            if (!mesazh.Status)
            {
                error = mesazh.PershkrimMesazhi;
                return mesazh;
            }

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
                    case "Emertimi":
                        emertimi = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Nr Llogari":
                        nrLlog = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Klient/Furnitor":
                        llojiklientfurnitor = clsFunksione.vendosVlere(trup, dr, out error);
                        if (llojiklientfurnitor == "Klient")
                        {
                            llojiKF = true;
                            idkonfig = idKonfigKlient;
                        }
                        else if (llojiklientfurnitor == "Furnitor")
                        {
                            llojiKF = false;
                            idkonfig = idKonfigFurnitor;
                        }
                        else
                        {
                            object[] arr = { dr[pozicionkodi], " " + rm.GetString("msgLlojKFnukEkziston", ci), i };
                            gabime.Rows.Add(arr);
                            if (importo)
                            {
                                tePaImportuara.ImportRow(dr);
                                gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                            }
                        }
                        break;

                    case "Klient/Furnitori Kryesor":
                        kodklienti = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Titulli":
                        titulli = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Nipti":
                        nipti = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Licenca":
                        licenca = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Numri i TVSH":
                        nrtvsh = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Aktiviteti":
                        aktivitetiKF = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Emer Kerkimi":
                        emerKerkimiKF = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Tel":
                        telKF = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Qyteti":
                        qyteti = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Shteti":
                        shtetiKF = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Fax":
                        faxKF = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Cel":
                        celKF = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Email":
                        emailKF = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Limiti Paralajmerues":
                        limitParalajmerues = clsFunksione.vendosInt(trup, dr, out error);
                        break;
                    case "Web page":
                        webpageKF = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Limiti Bllokues":
                        limitBllokues = clsFunksione.vendosInt(trup, dr, out error);
                        break;
                    case "IBAN":
                        ibanKF = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Llogari Bankare":
                        llogariBankareKF = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Maturimi":
                        maturimi = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Kategori Zbritje":
                        kategorizbritje = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Nivel Cmimi":
                        nivelcmimi = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Zbritje Analitike":
                        pershkrimzbritjeAnalitike = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "SWIFT":
                        swift = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Aktiv":
                        aktiv = clsFunksione.vendosBool(trup, dr, out error);
                        break;
                    case "Arka/Banka":
                        kodemriBanka = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Grupimi 1":
                        kodgrupim1kf = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Grupimi 2":
                        kodgrupim2kf = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Grupimi 3":
                        kodgrupim3kf = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Lloj Adrese":
                        llojadrese = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Adresa":
                        adresa = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Kodi Postar":
                        kodipostar = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Objektiva kosto":
                        objektiva = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Menyre pagese":
                        pershkrimMetoda = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Perfaqesues Shitje 1":
                        kodPerfaqsuesShitje1 = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Perfaqesues Shitje 2":
                        kodPerfaqsuesShitje2 = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Shitje Pa TVSH":
                        shitjePaTvsh = clsFunksione.vendosBool(trup, dr, out error);
                        break;
                    case "Autongarkese":
                        autongarkese = clsFunksione.vendosBool(trup, dr, out error);
                        break;
                    case "Fermer":
                        fermer = clsFunksione.vendosBool(trup, dr, out error);
                        break;
                    case "Llogari Dytesore":
                        llogDytesore = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Prospekt":
                        prospekt = clsFunksione.vendosBool(trup, dr, out error);
                        break;
                    case "Autorizimi":
                        autorizimi = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Perfaqesues Shitje 3":
                        kodPerfaqsuesShitje3 = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Emertim Fature":
                        emertimFature = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Llogarit Komision":
                        llogaritKomision = clsFunksione.vendosBool(trup, dr, out error);
                        break;
                    case "Kod Integrimi":
                        kodIntegrimi = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "ID e marreveshjes":
                        idmarreveshje = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Lloji i marreveshjes":
                        llojmarreveshje = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Gjenero fature shitje tatimore":
                        gjeneroKupon = clsFunksione.vendosBool(trup, dr, out error);
                        break;
                    case "Kodi ISKSH":
                        kodIsksh = clsFunksione.vendosVlere(trup, dr, out error);
                        break;
                    case "Tipi Id":
                        tipiId = clsFunksione.vendosVlere(trup, dr, out error);
                        if (tipiId != "NUIS" && tipiId != "ID" && tipiId != "PASS" && tipiId != "VAT" && tipiId != "TAX" && tipiId != "SOC" && tipiId != "")
                        {
                            object[] arr = { dr[pozicionkodi], " " + $"Vlera e vendosur {tipiId} per fushen Tipi Id nuk ekziston!", i };
                            gabime.Rows.Add(arr);
                            return mesazh;
                        }
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

            try
            {
                colAdresatKlientFurnitor colAdresa = new colAdresatKlientFurnitor();
                if (adresa != "")
                {
                    int tip = 0;
                    if (llojadrese == "Biznesi")
                        tip = 1;
                    else if (llojadrese == "Magazine")
                        tip = 2;
                    else if (llojadrese == "Dege")
                        tip = 3;
                    else
                    {
                        object[] arr = { dr[pozicionkodi], rm.GetString("msgLlojAdreseNukEkziston", ci), i };
                        gabime.Rows.Add(arr);
                        if (importo)
                        {
                            tePaImportuara.ImportRow(dr);
                            gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                        }
                        error = "adresa";
                        return mesazh;
                    }
                    clsAdresaKlientFurnitor adr = new clsAdresaKlientFurnitor(0, 0, tip, adresa, kodipostar);
                    colAdresa.Add(adr);
                }

                (string nrAutom, Dictionary<string, object> hiddenFieldPerNrAuto) nrAutoObject = clsFunksione.ktheNrAutoPerKonfigurim(idkonfig, kodi, "txtKodi", "Kodi", 123, DateTime.Today, new clsDatabaseShare());
                string nrAutom = nrAutoObject.nrAutom;
                Dictionary<string, object> hfNrAuto = nrAutoObject.hiddenFieldPerNrAuto;

                kf = kf.KrijoKlientFurnitorPerImport(new clsKlientFurnitor(), clsFunksione.ktheStringunPaHapesira(nrAutom, true), nrLlog, llojiKF, titulli, aktivitetiKF, clsFunksione.ktheStringunPaHapesira(emertimi, false), emerKerkimiKF, nipti, qyteti, shtetiKF, telKF, faxKF, celKF, emailKF, webpageKF, ibanKF, llogariBankareKF, aktiv, nrLlogZbritje, llogDytesore, kushtepagese, pershkrimMetoda, maturimi, kategorizbritje, limitParalajmerues, limitBllokues, idKategoriKlienti, kushtedergimi, menyratransporti, ofertaAutomatike, vleraLimitPorositur, prioriteti, cmimUlet, nivelcmimi, kodPerfaqsuesShitje1, idQenderKosto, idFushata, pershkrimzbritjeAnalitike, zbritjeTotal, idndermarje, vit, idPerdorues, idkonfig, licenca, swift, kodemriBanka, adresaBanka, kodgrupim1kf, kodgrupim2kf, kodgrupim3kf, nrtvsh, colAdresa, new colKontaktiKlientFurnitor(), new colBuxhetet(), new colVleraFushaShtese(), shtim, monndermarje, kontrolloekzistence, objektiva, idPerdorues, 0, 0, gjeneroKupon, rm, ci, kodPerfaqsuesShitje2, 0, 0, prospekt, "", fermer, shitjePaTvsh, autongarkese, autorizimi, "", kodklienti, shenime, kodPerfaqsuesShitje3, emertimFature, llogaritKomision, kodIntegrimi, kodIsksh, tipiId);

                if (importo)
                {
                    if (vjenNgaImportSQL)
                        mesazh = kf.Ruaj(hfNrAuto, vjenNgaImportSQL, dr[primaryKey].ToString(), emerTab, primaryKey, ndermarrjeKey);
                    else
                        mesazh = kf.Ruaj(hfNrAuto, vjenNgaImportSQL, "0", "", primaryKey, ndermarrjeKey);

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

        private static clsMesazh ImportoKlientFurnitoreModifikim(ref DataTable gabime, ref DataTable tePaImportuara, bool importo, int pozicionkodi, ResourceManager rm, CultureInfo ci, int idPerdorues, int idndermarje, colTrupiFormatImporti col, bool vjenNgaImportSQL, string emerTab, DataRow dr, out string error, int i)
        {
            error = "";
            const bool shtim = false, kontrolloekzistence = true;

            int idKonfigFurnitor = clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod("F", idndermarje);
            int idKonfigKlient = clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod("K", idndermarje);
            int monndermarje = clsNdermarrje.ktheIdMonedheNdermSipasID(idndermarje);

            int? limitParalajmerues = null, limitBllokues = null, idklientfurnitorkryesor = null, idkonfig = null;
            string kodi = null, emertimi = null, nrLlog = null, llojiklientfurnitor = null, titulli = null, aktivitetiKF = null, emerKerkimiKF = null, nipti = null, qyteti = null, shtetiKF = null, telKF = null, faxKF = null, celKF = null, emailKF = null, webpageKF = null, ibanKF = null, llogariBankareKF = null, maturimi = null, kategorizbritje = null, nivelcmimi = null, pershkrimzbritjeAnalitike = null, licenca = null, swift = null, kodemriBanka = null, kodgrupim1kf = null, kodgrupim2kf = null, kodgrupim3kf = null, nrtvsh = null, llojadrese = null, adresa = null, kodipostar = null, objektiva = null, pershkrimMetoda = null, kodPerfaqsuesShitje1 = null, kodPerfaqsuesShitje2 = null, llogDytesore = null, autorizimi = null, kodPerfaqsuesShitje3 = null, llojmarreveshje = null, idmarreveshje = null, shenime = null, emertimFature = null, kodIntegrimi = null, kodKlientiKryesor = null;
            bool? aktiv = null, shitjePaTvsh = null, autongarkese = null, fermer = null, prospekt = null, llogaritKomision = null, gjeneroKupon = null, llojiKF = null;
            string kodiIsksh = "", tipiId = "";
            string primaryKey = "IDIMPORTSHITJE";
            string ndermarrjeKey = "";


            clsMesazh mesazh = new clsMesazh(true);
            clsKlientFurnitor kf = new clsKlientFurnitor();
            clsTrupiFormatImporti kodiTrupFormati = col.Find(x => x.KodKontrolli == "Kodi");
            kodi = clsFunksione.ktheStringunPaHapesira(clsFunksione.vendosVlere(kodiTrupFormati, dr, out error), true);
            clsKlientFurnitor kfEkzistues = new clsKlientFurnitor(kodi, idndermarje);

            if(kfEkzistues.IdKlientFurnitor <= 0)
            {
                object[] arr = { dr[pozicionkodi], $"Klient/Furnitori me kod {kodi} nuk ekziston.", i };
                gabime.Rows.Add(arr);
                if (importo)
                {
                    tePaImportuara.ImportRow(dr);
                    gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                }
                error = "Klienti nuk ekziston!";
                return new MesazhGabimi();
            }

            int idQenderKosto = kfEkzistues.IdQenderKosto;
            int idFushata = kfEkzistues.IdFushata;
            int idKategoriKlienti = kfEkzistues.IdKategoriKlienti;
            int prioriteti = kfEkzistues.Prioriteti;
            decimal vleraLimitPorositur = kfEkzistues.VleraLimitPorositur;
            decimal cmimUlet = kfEkzistues.CmimUlet;
            decimal zbritjeTotal = kfEkzistues.ZbritjeTotal;
            bool ofertaAutomatike = kfEkzistues.OfertaAutomatike;
            string nrLlogZbritje = kfEkzistues.NrLlogZbritje ?? string.Empty,
                kushtepagese = kfEkzistues.KodKushtePagese ?? string.Empty,
                kushtedergimi = kfEkzistues.KushteDergimi ?? string.Empty,
                menyratransporti = kfEkzistues.MenyraTransportit ?? string.Empty,
                adresaBanka = kfEkzistues.AdresaBanka ?? string.Empty;
            int vit = kfEkzistues.Viti;

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
                    case "Emertimi":
                        emertimi = !kaVlere ? null : clsFunksione.ktheStringunPaHapesira(vlera, false);
                        break;
                    case "Nr Llogari":
                        nrLlog = !kaVlere ? null : vlera;
                        break;
                    case "Klient/Furnitor":
                        if (!kaVlere)
                            break;
                        llojiklientfurnitor = vlera;
                        if (llojiklientfurnitor == "Klient")
                        {
                            llojiKF = true;
                            idkonfig = idKonfigKlient;
                        }
                        else if (llojiklientfurnitor == "Furnitor")
                        {
                            llojiKF = false;
                            idkonfig = idKonfigFurnitor;
                        }
                        else
                        {
                            object[] arr = { dr[pozicionkodi], " " + rm.GetString("msgLlojKFnukEkziston", ci), i };
                            gabime.Rows.Add(arr);
                            if (importo)
                            {
                                tePaImportuara.ImportRow(dr);
                                gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                            }
                        }
                        break;

                    case "Klient/Furnitori Kryesor":
                        kodKlientiKryesor = !kaVlere ? null : vlera;
                        break;
                    case "Titulli":
                        titulli = !kaVlere ? null : vlera;
                        break;
                    case "Nipti":
                        nipti = !kaVlere ? null : vlera;
                        break;
                    case "Licenca":
                        licenca = !kaVlere ? null : vlera;
                        break;
                    case "Numri i TVSH":
                        nrtvsh = !kaVlere ? null : vlera;
                        break;
                    case "Aktiviteti":
                        aktivitetiKF = !kaVlere ? null : vlera;
                        break;
                    case "Emer Kerkimi":
                        emerKerkimiKF = !kaVlere ? null : vlera;
                        break;
                    case "Tel":
                        telKF = !kaVlere ? null : vlera;
                        break;
                    case "Qyteti":
                        qyteti = !kaVlere ? null : vlera;
                        break;
                    case "Shteti":
                        shtetiKF = !kaVlere ? null : vlera;
                        break;
                    case "Fax":
                        faxKF = !kaVlere ? null : vlera;
                        break;
                    case "Cel":
                        celKF = !kaVlere ? null : vlera;
                        break;
                    case "Email":
                        emailKF = !kaVlere ? null : vlera;
                        break;
                    case "Limiti Paralajmerues":
                        limitParalajmerues = !kaVlere ? null : (int?)clsFunksione.vendosInt(trup, dr, out error);
                        break;
                    case "Web page":
                        webpageKF = !kaVlere ? null : vlera;
                        break;
                    case "Limiti Bllokues":
                        limitBllokues = !kaVlere ? null : (int?)clsFunksione.vendosInt(trup, dr, out error);
                        break;
                    case "IBAN":
                        ibanKF = !kaVlere ? null : vlera;
                        break;
                    case "Llogari Bankare":
                        llogariBankareKF = !kaVlere ? null : vlera;
                        break;
                    case "Maturimi":
                        maturimi = !kaVlere ? null : vlera;
                        break;
                    case "Kategori Zbritje":
                        kategorizbritje = !kaVlere ? null : vlera;
                        break;
                    case "Nivel Cmimi":
                        nivelcmimi = !kaVlere ? null : vlera;
                        break;
                    case "Zbritje Analitike":
                        pershkrimzbritjeAnalitike = !kaVlere ? null : vlera;
                        break;
                    case "SWIFT":
                        swift = !kaVlere ? null : vlera;
                        break;
                    case "Aktiv":
                        aktiv = !kaVlere ? null : (bool?)clsFunksione.vendosBool(trup, dr, out error);
                        break;
                    case "Arka/Banka":
                        kodemriBanka = !kaVlere ? null : vlera;
                        break;
                    case "Grupimi 1":
                        kodgrupim1kf = !kaVlere ? null : vlera;
                        break;
                    case "Grupimi 2":
                        kodgrupim2kf = !kaVlere ? null : vlera;
                        break;
                    case "Grupimi 3":
                        kodgrupim3kf = !kaVlere ? null : vlera;
                        break;
                    case "Lloj Adrese":
                        llojadrese = !kaVlere ? null : vlera;
                        break;
                    case "Adresa":
                        adresa = !kaVlere ? null : vlera;
                        break;
                    case "Kodi Postar":
                        kodipostar = !kaVlere ? null : vlera;
                        break;
                    case "Objektiva kosto":
                        objektiva = !kaVlere ? null : vlera;
                        break;
                    case "Menyre pagese":
                        pershkrimMetoda = !kaVlere ? null : vlera;
                        break;
                    case "Perfaqesues Shitje 1":
                        kodPerfaqsuesShitje1 = !kaVlere ? null : vlera;
                        break;
                    case "Perfaqesues Shitje 2":
                        kodPerfaqsuesShitje2 = !kaVlere ? null : vlera;
                        break;
                    case "Shitje Pa TVSH":
                        shitjePaTvsh = !kaVlere ? null : (bool?)clsFunksione.vendosBool(trup, dr, out error);
                        break;
                    case "Autongarkese":
                        autongarkese = !kaVlere ? null : (bool?)clsFunksione.vendosBool(trup, dr, out error);
                        break;
                    case "Fermer":
                        fermer = !kaVlere ? null : (bool?)clsFunksione.vendosBool(trup, dr, out error);
                        break;
                    case "Llogari Dytesore":
                        llogDytesore = !kaVlere ? null : vlera;
                        break;
                    case "Prospekt":
                        prospekt = !kaVlere ? null : (bool?)clsFunksione.vendosBool(trup, dr, out error);
                        break;
                    case "Autorizimi":
                        autorizimi = !kaVlere ? null : vlera;
                        break;
                    case "Perfaqesues Shitje 3":
                        kodPerfaqsuesShitje3 = !kaVlere ? null : vlera;
                        break;
                    case "Emertim Fature":
                        emertimFature = !kaVlere ? null : vlera;
                        break;
                    case "Llogarit Komision":
                        llogaritKomision = !kaVlere ? null : (bool?)clsFunksione.vendosBool(trup, dr, out error);
                        break;
                    case "Kod Integrimi":
                        kodIntegrimi = !kaVlere ? null : vlera;
                        break;
                    case "ID e marreveshjes":
                        idmarreveshje = !kaVlere ? null : vlera;
                        break;
                    case "Lloji i marreveshjes":
                        llojmarreveshje = !kaVlere ? null : vlera;
                        break;
                    case "Gjenero fature shitje tatimore":
                        gjeneroKupon = !kaVlere ? null : (bool?)clsFunksione.vendosBool(trup, dr, out error);
                        break;
                    case "Kodi ISKSH":
                        kodiIsksh = !kaVlere ? null : vlera;
                        break;
                    case "Tipi Id":
                        tipiId = !kaVlere ? null : vlera;
                        if (tipiId == null)
                            break;
                        if (tipiId != "NUIS" && tipiId != "ID" && tipiId != "PASS" && tipiId != "VAT" && tipiId != "TAX" && tipiId != "SOC")
                        {
                            object[] arr = { dr[pozicionkodi], " " + $"Vlera e vendosur {tipiId} per fushen Tipi Id nuk ekziston!", i };
                            gabime.Rows.Add(arr);
                            return mesazh;
                        }
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

            try
            {
                colAdresatKlientFurnitor colAdresa = new colAdresatKlientFurnitor(kfEkzistues.IdKlientFurnitor);
                if (!string.IsNullOrEmpty(adresa))
                {
                    int tip = 0;
                    if (llojadrese == "Biznesi")
                        tip = 1;
                    else if (llojadrese == "Magazine")
                        tip = 2;
                    else if (llojadrese == "Dege")
                        tip = 3;
                    else
                    {
                        object[] arr = { dr[pozicionkodi], rm.GetString("msgLlojAdreseNukEkziston", ci), i };
                        gabime.Rows.Add(arr);
                        if (importo)
                        {
                            tePaImportuara.ImportRow(dr);
                            gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                        }
                        error = "adresa";
                        return mesazh;
                    }
                    colAdresa.Remove(colAdresa.Find(x => x.IdTipAdrese == tip));
                    clsAdresaKlientFurnitor adr = new clsAdresaKlientFurnitor(0, 0, tip, adresa, kodipostar?? string.Empty);
                    colAdresa.Add(adr);
                }
                colKontaktiKlientFurnitor kontaktiKlientFurnitor = new colKontaktiKlientFurnitor(kfEkzistues.IdKlientFurnitor);
                colBuxhetet buxhetet = new colBuxhetet(kfEkzistues.IdKlientFurnitor, "KlientFurnitor");
                colVleraFushaShtese vleraFushaShtese = new colVleraFushaShtese(kfEkzistues.IdKlientFurnitor, null);
                int idNdermarrjeBije = kfEkzistues.IdNdermarjeBij;
                int llojPorosie = kfEkzistues.LlojPorosie;
                decimal perqindjeAgjent = kfEkzistues.PerqindjeAgjenti;
                decimal perqindjeAgjent2 = kfEkzistues.PerqindjeAgjenti2;
                string kodiMobile = kfEkzistues.KodiMobile?? string.Empty;
                string emailperPajisje = kfEkzistues.EmailPerPajisje?? string.Empty;

                kf = kf.KrijoKlientFurnitorPerImport(kfEkzistues, kodi, nrLlog, llojiKF, titulli, aktivitetiKF, emertimi, emerKerkimiKF, nipti, qyteti, shtetiKF, telKF, faxKF, celKF, emailKF, webpageKF, ibanKF, llogariBankareKF, aktiv, nrLlogZbritje, llogDytesore, kushtepagese, pershkrimMetoda, maturimi, kategorizbritje, limitParalajmerues, limitBllokues, idKategoriKlienti, kushtedergimi, menyratransporti, ofertaAutomatike, vleraLimitPorositur, prioriteti, cmimUlet, nivelcmimi, kodPerfaqsuesShitje1, idQenderKosto, idFushata, pershkrimzbritjeAnalitike, zbritjeTotal, idndermarje, vit, idPerdorues, idkonfig, licenca, swift, kodemriBanka, adresaBanka, kodgrupim1kf, kodgrupim2kf, kodgrupim3kf, nrtvsh, colAdresa, kontaktiKlientFurnitor, buxhetet, vleraFushaShtese, shtim, monndermarje, kontrolloekzistence, objektiva, idPerdorues, idNdermarrjeBije, llojPorosie, gjeneroKupon, rm, ci, kodPerfaqsuesShitje2, perqindjeAgjent, perqindjeAgjent2, prospekt, kodiMobile, fermer, shitjePaTvsh, autongarkese, autorizimi, emailperPajisje, kodKlientiKryesor, shenime, kodPerfaqsuesShitje3, emertimFature, llogaritKomision, kodIntegrimi, kodiIsksh, tipiId);
                kf.IdKlientFurnitor = kfEkzistues.IdKlientFurnitor;
                if (importo)
                {
                    if (vjenNgaImportSQL)
                        mesazh = kf.Modifiko(kfEkzistues.DtModifikimi, vjenNgaImportSQL, dr[primaryKey].ToString(), emerTab, primaryKey, ndermarrjeKey);
                    else
                        mesazh = kf.Modifiko(kfEkzistues.DtModifikimi, vjenNgaImportSQL, "0", "", primaryKey, ndermarrjeKey);

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
    }
}
