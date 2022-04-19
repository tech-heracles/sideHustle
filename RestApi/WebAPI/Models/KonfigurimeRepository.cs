using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.SessionState;
using DbCore;
using DbCore.DbAdmin;
using DbCore.DbArkaBanka;
using DbCore.DbInventari;
using DbCore.DbProdhimi;
using DbCore.DbRegjistrim;
using DbCore.DbShare;
using DbCore.IMBUtils.Logging;
using Newtonsoft.Json;
using System.Text;
using System.Globalization;
using System.Resources;
using DbCore.IMBUtils.Messages;

namespace RestApi.WebAPI.Models
{
    public class KonfigurimeRepository
    {
        public static clsPerdorues TestSession(HttpSessionState session)
        {
            return mySessionObjects.kthePerdorues(session);
        }

        public static object ktheKonfigDB(int idKomp, string kodKonf, int idNdermarrje, string kodKontrollKlienti, int idKlienti, bool shtim, bool merrFormatKursi, int idGjuha)
        {
            int idKonfigurim = -1;
            string kodniveli = "";
            int kategoria;
            if (kodKonf != "")  //nese eshte zgjedhur nje konfigurim tek combo e konfigurimeve
            {
                clsKonfigurimAmbjenti clsKonf = new clsKonfigurimAmbjenti();
                clsKonf.mbushKonfiguriminMeKod(kodKonf, idNdermarrje, idGjuha);
                idKonfigurim = clsKonf.IdKonfigAmbjente;
                kodniveli = clsNivelRegjistrimi.ktheKodNivelRegjistrimi(clsKonf.IdNivel);
                kategoria = clsKonf.IdKategori;
            }
            else
            {//nese nuk ehste zgjedhur asnje konfigurim merret konfigurimi default
                clsKonfigurimAmbjenti clsKonf = new clsKonfigurimAmbjenti();
                clsKonf.mbushKonfigDefaultKomponentes(idKomp, idNdermarrje);
                if (clsKonf != null)
                    clsKonf.mbushKonfigDefaultKomponentes(idKomp, -1);
                idKonfigurim = clsKonf.IdKonfigAmbjente;
                kodniveli = clsNivelRegjistrimi.ktheKodNivelRegjistrimi(clsKonf.IdNivel);
                kategoria = clsKonf.IdKategori;
            }
            //DbCore.DbShare.colKusht colKushte = new DbCore.DbShare.colKusht();
            DataTable kushtAlternativa = colKusht.mbushGjitheKushteAlternativa(idKonfigurim);
            clsKonfLlojRreshti konfLlojRreshti = new clsKonfLlojRreshti();

            if (kushtAlternativa.Select("KODI = 'LLD'").Length > 0)
            {
                DataRow lldKusht = kushtAlternativa.Select("KODI = 'LLD'").First();
                if (kategoria == 1 || kategoria == 2)
                    konfLlojRreshti = new clsKonfLlojRreshti(Convert.ToInt32(lldKusht["IDKUSHTEMPLATE"]), "Shitje");
                else
                    konfLlojRreshti = new clsKonfLlojRreshti(Convert.ToInt32(lldKusht["IDKUSHTEMPLATE"]), "ArkaBanka");
            }
            colAtributeTrupi colAtrTrupi = new colAtributeTrupi();
            colAtrTrupi.mbushKontrolletKonfigurimitKomponentes(idGjuha, idKomp, idKonfigurim);
            //DbCore.DbShare.colKontrolle colKontroll1 = colAtrTrupi.ktheKontrolle();
            colKontrolle colKontroll = new colKontrolle(idGjuha, idKomp, idKonfigurim);
            //colKontroll.TableName = "colKontroll";
            colGridaTrupi colGrida = new colGridaTrupi(idKomp, idKonfigurim, idGjuha);

            clsFormatKonfigTrup formatNumri = new clsFormatKonfigTrup();
            string formatiKursit;
            object[] form = ktheKonfigurimFormatNumri(idKonfigurim, idNdermarrje, kodKontrollKlienti, idKlienti, shtim, idKomp, merrFormatKursi, idGjuha);
            formatNumri = (clsFormatKonfigTrup)form[0];
            formatiKursit = form[1].ToString();

            //RregjistrimeRepository.ktheGrupimDokumentashNderm(kodKonf, idNdermarrje, idper)

            return new
            {
                colKontroll = colKontroll,
                colAtrTrupi = colAtrTrupi,
                colGrida = colGrida,
                colKushte = kushtAlternativa,
                kodniveli = kodniveli,
                konfLlojRreshti = konfLlojRreshti,
                formatNumri = formatNumri,
                formatiKursit = formatiKursit
            };
        }

        public static object[] ktheKonfigurimFormatNumri(int idKonfigurim, int idNdermarrje, string kodKontrolli, int idObjekt, bool shtim, int idKomponente, bool merrFormatKursi, int idGjuha)
        {
            clsFormatiKonfig formatNrPerKonfig = new clsFormatiKonfig();
            formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(idKonfigurim);
            int idMonedha = clsFunksione.ktheMonedhePerFormatNumri(idGjuha, idKonfigurim, idNdermarrje, idKomponente, kodKontrolli, idObjekt, shtim);
            clsFormatKonfigTrup formatMonedhe = new clsFormatKonfigTrup();
            if (formatNrPerKonfig.IdFormatKonfig > 0)
                formatMonedhe = formatNrPerKonfig.KonfigTrupi.merrFormatSipasMonedhes(idMonedha);
            if ((formatMonedhe != null && formatMonedhe.IdFormatKonfigTrup == 0) || formatMonedhe == null)
                formatMonedhe = new clsFormatKonfigTrup(clsFormatKonfigTrup.defaultFormatSasia, clsFormatKonfigTrup.defaultFormatCmimi, clsFormatKonfigTrup.defaultFormatVlefta, clsFormatKonfigTrup.defaultFormatZbritja, clsFormatKonfigTrup.defaultFormatStringSasia, clsFormatKonfigTrup.defaultFormatStringCmimi, clsFormatKonfigTrup.defaultFormatStringVlefta, clsFormatKonfigTrup.defaultFormatStringZbritja);
            int formatKursi = 2;
            if (merrFormatKursi)
            {
                formatKursi = clsFunksione.MerrVleraFormatKursi(idMonedha);
            }
            object[] formatet = new object[2];
            formatet[0] = formatMonedhe;
            formatet[1] = formatKursi;
            return formatet;
        }

        public static object[] ktheKonfigurimFormatNumriMerriTeGjithe(int idKonfigurim, int idNdermarrje, int idPerdoruesi)
        {
            clsFormatiKonfig formatNrPerKonfig = new clsFormatiKonfig();
            formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(idKonfigurim);
            if (formatNrPerKonfig.KonfigTrupi.Count == 0)
            {
                clsFormatKonfigTrup trup = new clsFormatKonfigTrup(clsFormatKonfigTrup.defaultFormatSasia, clsFormatKonfigTrup.defaultFormatCmimi, clsFormatKonfigTrup.defaultFormatVlefta, clsFormatKonfigTrup.defaultFormatZbritja, clsFormatKonfigTrup.defaultFormatStringSasia, clsFormatKonfigTrup.defaultFormatStringCmimi, clsFormatKonfigTrup.defaultFormatStringVlefta, clsFormatKonfigTrup.defaultFormatStringZbritja);
                formatNrPerKonfig.KonfigTrupi.Add(trup);
            }
            DbCore.DbAdmin.colMonedhat mon = new DbCore.DbAdmin.colMonedhat();
            mon.mbushGjitheMonedhat(idNdermarrje, idPerdoruesi);
            object[] formatet = new object[2];
            formatet[0] = formatNrPerKonfig;
            formatet[1] = mon;
            return formatet;
        }

        internal static object ktheVleraFiltri(string id)
        {
            return clsFunksione.ktheVleraFiltri(id);
        }

        public static bool RuajFilterGrida(HttpSessionState session, bool ruajFilter, string filterExpression, string idGrida, int idNdermarrje)
        {
            if (ruajFilter)
                mySessionObjects.SaveFilter(idNdermarrje, filterExpression, idGrida, "");
            return mySessionObjects.ruajcbRuajFilteriNeSesion(session, ruajFilter);
        }

        public static object merrTeDhenaPerNumerAutomatik(int idkomp, string kodkonfi, string iddokumenti, int idndermarje, int gjuhe, int idKategoria, int selectedIdLlojPeriudhe, bool klonim = false)
        {
            string lidhur = eshteLidhur(idkomp, kodkonfi, iddokumenti, idndermarje, gjuhe);
            object llojePeriudhash = colLlojPeriudhe.KtheTeDhenaLlojPeriudheNew(idKategoria, selectedIdLlojPeriudhe);

            var kaNumerAutomatikTeFundit = 0;

            if (!klonim)
            {
                clsNrAutomatikFundit cls = new clsNrAutomatikFundit();
                kaNumerAutomatikTeFundit = cls.kaNumraAutoTeFundit(Convert.ToInt32(iddokumenti));
            }

            return new { eshteLidhur = lidhur, KtheTeDhenaLlojPeriudheNew = llojePeriudhash, kaNrAutomatik = kaNumerAutomatikTeFundit };
        }

        internal static clsMesazh ruajStyleRaporti(string styleName, string zoomFactor, int exportFormat, int exportMode, int idDesign, HttpSessionState session)
        {
            int idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(session);
            int idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(session);

            var stiliURuajt = clsFunksione.ruajStilRaporti(styleName, session, zoomFactor, exportFormat, exportMode, idPerdoruesi);
            if (stiliURuajt)
            {
                var zgjedhjaURuajt = clsRaportDesign.RuajZgjedhjePerNdermarrje(idNdermarrje, idDesign);
                if (!zgjedhjaURuajt)
                    return new clsMesazh(true, "Stili i zgjedhur u ruajt me sukses,por dizajni nuk u ruajt!");
                return new clsMesazh(true, "Stili dhe zgjedhja e dizajnit u ruajt me sukses!");
            }
            return new clsMesazh(false, "Stili i raportit dhe designi i zgjedhur nuk u ruajten");
        }

        public static object merrVlerenNrAutomatik(string kodKontrolli, int idNrAuto, DateTime date)
        {
            return clsFunksione.merrVlerenNrAutomatik(kodKontrolli, idNrAuto, date);
        }

        public static string eshteLidhur(int idkomp, string kodkonfi, string iddokumenti, int idndermarje, int gjuhe)
        {
            return clsFunksione.eshteLidhur(idkomp, kodkonfi, iddokumenti, idndermarje, gjuhe);
        }
        public static string ktheTipiIdTransportuesi(int idTransportues)
        {
            return new clsTransportues(idTransportues).TipiId;
        }

        public static clsBanka merrBankaSipasKodit(string kodBanka, int idNdermarrje, int idPerdoruesi)
        {
            clsBanka banka = new clsBanka();
            banka.mbushBankeSipasKoditMeAutorizime(kodBanka, idNdermarrje, idPerdoruesi);
            return banka;
        }

        public static DataTable merrBankaSipasLLojit(int idNdermarrje, int idPerdoruesi, bool idLlojiArka)
        {
            return colBankat.ktheGjitheBankatSipasAutorizimeveSipasLlojit(idNdermarrje, idPerdoruesi, idLlojiArka, false);
        }

        internal static object ruajNeSessionURLLupaShpejte(string url, string lupa, HttpSessionState Session)
        {
            return mySessionObjects.ruajURLLupaShpejteNeSesion(Session, url, lupa);
        }

        internal static clsMesazh ruajKolonaGride(int idGride, Dictionary<string, string>[] gridColumns, int idGjuha, int idNdermarrje, int idViti, int idPerdoruesi)
        {
            clsTeDrejtaRoli tedrejtaInfo = new clsTeDrejtaRoli();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, "Konfigurime Gride");
            if (!tedrejtaInfo.DAmb)
                return new MesazhGabimi(MessagesResource.Messages["msgNukKeniTeDrejta"]);

            clsMesazh mesazh = new clsMesazh(false, MessagesResource.Messages["mesazhNdodhiNjeGabimGjateRuatjesSeKonfigurimit"]);
            colGridaTrupi trupiGrida = new colGridaTrupi();
            if (!trupiGrida.mbushTrupin(idGjuha, idGride)) return mesazh;
            foreach (Dictionary<string, string> dic in gridColumns)
            {
                var dic1 = dic;
                clsGridaTrupi gridElemGjetur = trupiGrida.Find(delegate (clsGridaTrupi gridElem)
                {
                    return gridElem.KodiTrupi == dic1["KodiTrupi"];
                });
                gridElemGjetur.IndexTrupi = Int32.Parse(dic["IndexTrupi"]);
                gridElemGjetur.VisibleTrupi = bool.Parse(dic["VisibleTrupi"]);
                gridElemGjetur.WidthTrupi = Int32.Parse(dic["WidthTrupi"]);
            }
            if (!trupiGrida.Update(idGjuha)) return mesazh;
            return new clsMesazh(true, MessagesResource.Messages["mesazhKonfigurimiSuksess"]);
        }

        internal static clsMesazh RuajKolonaGrideDheFilter(int idGride, Dictionary<string, string>[] gridColumns, int idGjuha, int idNdermarrje, int idViti, int idPerdoruesi, string kodFiltri, string filterExp, string koloneRenditje, bool renditja)
        {

            var mesazh = ruajKolonaGride(idGride, gridColumns, idGjuha, idNdermarrje, idViti, idPerdoruesi);
            if (!mesazh)
                return mesazh;

            clsFiltraGrida filtri = new clsFiltraGrida() { FiltraKodi = kodFiltri, FiltraShenime = kodFiltri, FiltraUniversal = false, GridaKokaId = idGride, FiltraVlera = filterExp, IdPerdoruesi = idPerdoruesi, IdNdermarje = idNdermarrje, IdStatusDok = 1 };

            if (!string.IsNullOrEmpty(koloneRenditje))
            {
                filtri.KoloneRenditje = koloneRenditje;
                filtri.DrejtimRenditje = renditja;
            }

            clsFiltraGrida filtraekzistues = new clsFiltraGrida();
            filtraekzistues.mbushFilterPerGrideSipasKodit(kodFiltri, idNdermarrje, idGride);

            if (filtraekzistues.IdFiltra > 0)
            {
                filtri.IdFiltra = filtraekzistues.IdFiltra;
                mesazh = filtri.modifiko();
            }
            else
                mesazh = filtri.ruaj();

            if (!mesazh)
                return mesazh;
            return new clsMesazh(true, MessagesResource.Messages["mesazhKonfigurimiSuksess"]);
        }

        internal static List<Dictionary<string, object>> ktheTipeStandarteKasashPeshoreshSipasLlojit(int lloji)
        {

            DataTable dt = DbCore.DbAdmin.colVleratKonfigurimiKasa.ktheLlojeKasashDhePeshoreshSipasLlojit(lloji);
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = dt.Columns.Cast<DataColumn>().ToDictionary(col => col.ColumnName, col => dr[col]);
                rows.Add(row);
            }
            return rows;

        }

        public static object lexoMesazhNgaSessioni(HttpSessionState Session, string guidString)
        {
            return !String.IsNullOrEmpty(guidString) ?
                mySessionObjects.hiqMesazhNgaSesioni(Session, guidString)
                :
                mySessionObjects.hiqMesazhNgaSesioni(Session);
        }

        public static string merrNgaSessionURLART(HttpSessionState Session)
        {
            return mySessionObjects.merrURLARTNgaSesioni(Session);
        }

        public static ListeVleraArtikulli ktheRowVleraKodArt(string kodKodBarArt, int rreshti, DateTime data, string idDetajimi, int idNdermarrje, int idPerdoruesi, KonfigurimTVSHGjateRregj llojTvsh, clsTaksa taksaKF)
        {
            clsArtikulli artikulli = new clsArtikulli();
            artikulli.mbushArtikull(kodKodBarArt, idNdermarrje);
            int idDetajim = int.Parse(idDetajimi);
            if (artikulli.IdArtikulli <= 0)
                artikulli.merrSipasKodbarit(kodKodBarArt, idNdermarrje);
            ListeVleraArtikulli listArt = new ListeVleraArtikulli();
            if (artikulli.IdArtikulli > 0)
            {
                artikulli.mbushKodBare();
                listArt.gjendjeTot = clsTrupiMagazina.merrSasi(artikulli, -1, data, idDetajim);
                listArt.idRreshti = rreshti;
                listArt.artikulli = artikulli;
                listArt.detajimFundit = new clsMesazh(true);
                clsTaksa taksendermarje = new clsTaksa();
                taksendermarje.mbushTakseDefaultNdermarrje(idNdermarrje);
                DbCore.DbRegjistrim.colTaksa taksaNdermarrje = new DbCore.DbRegjistrim.colTaksa(idNdermarrje, DbCore.DbRegjistrim.LlojTakse.Nivel_Tvsh, idPerdoruesi);
                listArt.listeTvsh = artikulli.ktheTvsh(llojTvsh, idPerdoruesi, taksendermarje, taksaNdermarrje, taksaKF);
                return listArt;
            }
            listArt.idRreshti = rreshti;
            listArt.detajimFundit = new clsMesazh(true);
            return listArt;
        }

        internal static object ruajNeSessionURLllog(string url, HttpSessionState Session)
        {
            return mySessionObjects.ruajURLllogNeSesion(Session, url);
        }

        public static object ktheKonfigAmbjentiMeFormatNumrash(int idKomp, string kodKonf, string kodKontrolli, int idObjekti, bool shtim, bool merrFormatKursi, bool merrGjitheKonf, int idGjuha, DateTime? dateDok, int idPerdoruesi, int idNdermarrje)
        {
            int idKonfigurim = -1;
            string kodniveli = "";
            int kategoria;

            if (kodKonf != "")  //nese eshte zgjedhur nje konfigurim tek combo e konfigurimeve
            {
                clsKonfigurimAmbjenti clsKonf = new clsKonfigurimAmbjenti();
                clsKonf.mbushKonfiguriminMeKod(kodKonf, idNdermarrje, idGjuha);
                idKonfigurim = clsKonf.IdKonfigAmbjente;
                kodniveli = kodniveli = clsNivelRegjistrimi.ktheKodNivelRegjistrimi(clsKonf.IdNivel);
                kategoria = clsKonf.IdKategori;
            }
            else
            {//nese nuk ehste zgjedhur asnje konfigurim merret konfigurimi default
                clsKonfigurimAmbjenti clsKonf = new clsKonfigurimAmbjenti();
                clsKonf.mbushKonfigDefaultKomponentes(idKomp, idNdermarrje);
                if (clsKonf == null)
                    clsKonf.mbushKonfigDefaultKomponentes(idKomp, -1);
                idKonfigurim = clsKonf.IdKonfigAmbjente;
                kodniveli = kodniveli = clsNivelRegjistrimi.ktheKodNivelRegjistrimi(clsKonf.IdNivel);
                kategoria = clsKonf.IdKategori;
            }
            colKusht colKushte = new colKusht();
            colKushte.mbushGjitheKushteKonfigurimi(idKonfigurim);
            colAlternativatKushti colAlterKusht = new colAlternativatKushti();
            clsKonfLlojRreshti konfLlojRreshti = new clsKonfLlojRreshti();
            foreach (clsKusht kusht in colKushte)
            {
                if (kusht.Kodi == "LLD") //llojrreshti
                {
                    if (kategoria == 1 || kategoria == 2)
                        konfLlojRreshti = new clsKonfLlojRreshti(kusht.IdKushtTemplate, "Shitje");
                    else
                        konfLlojRreshti = new clsKonfLlojRreshti(kusht.IdKushtTemplate, "ArkaBanka");
                    colAlterKusht.Add(new clsAlternativaKushti());
                    continue;
                }
                clsAlternativaKushti alterKusht = new clsAlternativaKushti();
                if (kusht.Vlera != 0)
                    alterKusht = new clsAlternativaKushti(kusht.Vlera);
                colAlterKusht.Add(alterKusht);
            }
            colAtributeTrupi colAtrTrupi = new colAtributeTrupi();
            colAtrTrupi.mbushKontrolletKonfigurimitKomponentes(idGjuha, idKomp, idKonfigurim);
            colKontrolle colKontroll = new colKontrolle(idGjuha, idKomp, idKonfigurim);
            DbCore.DbAdmin.colGridaTrupi colGrida = new DbCore.DbAdmin.colGridaTrupi(idKomp, idKonfigurim, idGjuha);

            Dictionary<string, object> objekteDefault = clsFunksione.vendosObjektetDefault(colAtrTrupi, colKontroll, shtim, "", dateDok, idKonfigurim, idNdermarrje, idPerdoruesi, idKomp, idGjuha, null);

            if (!merrGjitheKonf)
            {
                object[] obj = ktheKonfigurimFormatNumri(idKonfigurim, idNdermarrje, kodKontrolli, idObjekti, shtim, idKomp, merrFormatKursi, idGjuha);
                return new { colKontroll = colKontroll, colAtrTrupi = colAtrTrupi, colGrida = colGrida, colKushte = colKushte, colAlterKusht, kodniveli = kodniveli, konfLlojRreshti = konfLlojRreshti, formatNumri = (clsFormatKonfigTrup)obj[0], formatKursi = obj[1].ToString(), objekteDefault = objekteDefault };
            }
            else
            {
                object[] obj = ktheKonfigurimFormatNumriMerriTeGjithe(idKonfigurim, idNdermarrje, idPerdoruesi);
                return new { colKontroll = colKontroll, colAtrTrupi = colAtrTrupi, colGrida = colGrida, colKushte = colKushte, colAlterKusht, kodniveli = kodniveli, konfLlojRreshti = konfLlojRreshti, formatNumri = (clsFormatiKonfig)obj[0], formatKursi = (DbCore.DbAdmin.colMonedhat)obj[1], objekteDefault = objekteDefault };
            }
        }

        public static object ktheQytetePerNdermarrje(int idNdermarrje) {

            DbCore.DbAdmin.colQytetet colQyt = new DbCore.DbAdmin.colQytetet();
            colQyt.mbushGjitheQytetetPozitive(idNdermarrje);
            return new { colQyt = colQyt };
        }

        public static object[] ktheArtikujPerberes2(int id, HttpSessionState Session)
        {
            object[] result = new object[4];
            List<string> data = colArtikulliPerberes.merrDataArtikujPerberes(id);
            colArtikulliPerberes col = new colArtikulliPerberes();
            colArtikujt colart = new colArtikujt();
            colAktiviteteKoka colakt = new colAktiviteteKoka();
            ArrayList kosto = new ArrayList();
            if (data.Count > 0)
                if (id != -1)
                {
                    col.merrSipasIdArtikullKryesoreDates(id, DateTime.Parse(data[0]));
                }
            foreach (clsArtikulliPerberes c in col)
            {
                if (c.Lloji == 1)
                {
                    clsArtikulli art = new clsArtikulli(c.IdLidheseArt);
                    colart.Add(art);
                    colakt.Add(new clsAktiviteteKoka());
                    clsTrupiMagazina mag = new clsTrupiMagazina();
                    kosto.Add(mag.llogaritCmimMesatar(art, 0, DateTime.Parse(data[0]), -1, 1, DbCore.mySessionObjects.ktheIdPerdoruesi(Session)));
                }
                else
                {
                    clsAktiviteteKoka akt = new clsAktiviteteKoka(c.IdLidheseAkt);
                    colAktiviteteTrupi trupAkt = new colAktiviteteTrupi();
                    colakt.Add(akt);
                    colart.Add(new clsArtikulli());
                    //llogarisim koston e aktivitetit
                    double kostoAkt = 0;
                    trupAkt.mbushAktiviteteTrupiSipasIdKoka(akt.IdKoka);
                    foreach (clsAktiviteteTrupi trup in trupAkt)
                    {
                        kostoAkt += double.Parse(trup.Kosto.ToString());
                    }
                    kosto.Add(kostoAkt);
                }
            }
            result[0] = col;
            result[1] = colart;
            result[2] = colakt;
            result[3] = kosto;

            return result;
        }

        public static double ktheKostonArtikujvePerberes(string artikujtPerberes, int idNdermarrje, HttpSessionState Session)
        {
            clsArtikulli artikulli = new clsArtikulli();
            clsTrupiMagazina tr = new clsTrupiMagazina();
            double kosto = 0;
            string lloji;
            double koeficienti;
            string kodi;
            clsAktiviteteKoka aktivitet = new clsAktiviteteKoka();
            colAktiviteteTrupi trupat = new colAktiviteteTrupi();
            object[] artPerb = JsonConvert.DeserializeObject<object[]>(artikujtPerberes);
            if (artPerb != null)
            {
                foreach (Newtonsoft.Json.Linq.JObject oo in artPerb)
                {
                    //DbCore.DbInventari.clsArtikulliPerberes artPerberes = new DbCore.DbInventari.clsArtikulliPerberes();
                    // ((Newtonsoft.Json.Linq.JObject)oo)["Kodi"]
                    //Dictionary<string, object> rresht = (Dictionary<string, object>)oo;
                    kodi = oo["Kodi"].ToString();
                    if (kodi != "")
                    {
                        lloji = oo["Lloji"].ToString();
                        if (!string.IsNullOrWhiteSpace(oo["Koeficienti"].ToString()))
                            koeficienti = double.Parse(oo["Koeficienti"].ToString());
                        else
                            koeficienti = 1;
                        if (lloji == "Artikull")
                        {
                            artikulli.mbushArtikull(kodi, idNdermarrje);
                            if (artikulli.IdArtikulli == 0)
                                continue;
                            kosto += tr.llogaritCmimMesatar(artikulli, 0, DateTime.Now, -1, 1, mySessionObjects.ktheIdPerdoruesi(Session)) * koeficienti;
                        }
                        else if (lloji == "Aktivitete")
                        {
                            aktivitet = new clsAktiviteteKoka(kodi, idNdermarrje);
                            if (aktivitet.IdKoka == 0)
                                continue;
                            trupat.mbushAktiviteteTrupiSipasIdKoka(aktivitet.IdKoka);
                            kosto += trupat.Sum(trup => double.Parse(trup.Kosto.ToString()));
                        }
                    }
                }
            }
            return kosto;
        }

        public static object merrcolMeCmimeMeFormatNumrash(int idartikulli, int idNdermarrje, int idPerdorues, bool merrCmimeBlerje)
        {
            colCmimeArtikujsh col = new colCmimeArtikujsh();

            if (idartikulli <= 0)
            {
                DataTable dt = merrCmimeBlerje ? colNiveleCmimesh.merrNiveleNdermarjeDTBlerjeDheShitjeMeAutorizime(idNdermarrje, idPerdorues) :
                    colNiveleCmimesh.merrNiveleNdermarjeDTBlerjeShitjeMeAutorizime(idNdermarrje, 0, idPerdorues);


                //clsNdermarrje nderm = new clsNdermarrje(idNdermarrje);
                //decimal norma = clsTaksa.ktheNormePerqindjeMeId(nderm.IdTakse);
                clsTaksa taksa = new clsTaksa();
                taksa.mbushTakseDefaultNdermarrje(idNdermarrje);
                foreach (DataRow dr in dt.Rows)
                {
                    DbCore.DbAdmin.clsKurset kursi = new DbCore.DbAdmin.clsKurset(int.Parse(dr["IdMonedha"].ToString()), DateTime.Now);

                    clsCmimArtikulli cm = new clsCmimArtikulli(0, 0, int.Parse(dr["IdNivelCmimi"].ToString()), 0, int.Parse(dr["IdMonedha"].ToString()), new DateTime(DateTime.Today.Year, 1, 1), new DateTime(9999, 12, 31), 0, 0, 0, idPerdorues, idNdermarrje, 0, 1, 0, 0, bool.Parse(dr["NjesiTeVarura"].ToString()), kursi.VleraKursi, new DateTime(DateTime.Today.Year, 1, 1, 0, 0, 0), new DateTime(9999, 12, 31, 23, 59, 59), taksa.IdTaksa, 0, 0, taksa.NormaPerqindje, 0, dr["IdCmimRetail"].ToString() == "" ? 0 : int.Parse(dr["IdCmimRetail"].ToString()));
                    col.Add(cm);
                }
            }
            else
            {
                col.mbushCmimArtikulliSipasArtikullitMeKostoMeAutorizime(idartikulli, idNdermarrje, idPerdorues, merrCmimeBlerje);
                double kostoArt = 0;
                if (idartikulli > 0)
                {
                    clsTrupiMagazina tr = new clsTrupiMagazina();
                    clsArtikulli artikull = new clsArtikulli(idartikulli);
                    if (artikull.Klasa == 4 || artikull.Klasa == 5 || artikull.Klasa == 6)
                    {
                        List<string> datat = colArtikulliPerberes.merrDataArtikujPerberes(idartikulli);
                        if (datat.Count == 0)
                            datat.Add("01/01/2012");
                        colArtikulliPerberes per = new colArtikulliPerberes();
                        per.merrSipasIdArtikullKryesoreDates(artikull.IdArtikulli, DateTime.Parse(datat[0]));

                        if (artikull.Klasa == 4)
                        {
                            foreach (clsArtikulliPerberes art in per)
                            {
                                kostoArt += tr.llogaritCmimMesatar(new clsArtikulli(art.IdLidheseArt), 0, DateTime.Now, -1, 1, idPerdorues) * (double)art.Koeficienti;
                            }
                        }
                        else if (artikull.Klasa == 5 || artikull.Klasa == 6)
                        {
                            clsAktiviteteKoka aktivitet = new clsAktiviteteKoka();
                            colAktiviteteTrupi trupat = new colAktiviteteTrupi();
                            foreach (clsArtikulliPerberes art in per)
                            {
                                if (art.IdLidheseArt != 0)
                                {
                                    kostoArt += tr.llogaritCmimMesatar(new clsArtikulli(art.IdLidheseArt), 0, DateTime.Now, -1, 1, idPerdorues) * (double)art.Koeficienti;
                                }
                                else if (art.IdLidheseAkt != 0)
                                {
                                    trupat.mbushAktiviteteTrupiSipasIdKoka(art.IdLidheseAkt);
                                    foreach (clsAktiviteteTrupi trup in trupat)
                                    {
                                        kostoArt += double.Parse(trup.Kosto.ToString()) * (double)art.Koeficienti;
                                    }
                                }
                            }
                        }
                        for (int i = 0; i < col.Count; i++)
                        {
                            col[i].Kosto = kostoArt;
                        }
                    }
                }
            }
            int[] formatesasi = new int[col.Count];
            int[] formatecmim = new int[col.Count];
            int[] formatekurs = new int[col.Count];
            if (col.Count == 0)
                return new { cmimet = new colCmimeArtikujsh(), formatisasi = formatesasi, formaticmim = formatecmim, formatikurs = formatekurs };

            int j = 0;
            clsKonfigurimAmbjenti konfig = new clsKonfigurimAmbjenti();
            konfig.mbushKonfigAmbjSipasKod("CSH", idNdermarrje);
            clsFormatiKonfig formatNrPerKonfigCmimi = new clsFormatiKonfig();
            formatNrPerKonfigCmimi.mbushFormatNrKonfigSipasIdKonfigAmbjente(konfig.IdKonfigAmbjente);
            DbCore.DbAdmin.colMonedhat colMon = new DbCore.DbAdmin.colMonedhat();
            colMon.mbushGjitheMonedhat(idNdermarrje, idPerdorues);
            foreach (clsCmimArtikulli cm in col)
            {
                int idMonedha = cm.IdMonedha;
                if (formatNrPerKonfigCmimi.IdFormatKonfig == 0)
                {
                    formatesasi[j] = 2;
                    formatecmim[j] = 2;
                }
                else
                {
                    formatesasi[j] = formatNrPerKonfigCmimi.KonfigTrupi.merrFormatSipasMonedhes(idMonedha).ShifraPasPresjesSasia;
                    formatecmim[j] = formatNrPerKonfigCmimi.KonfigTrupi.merrFormatSipasMonedhes(idMonedha).ShifraPasPresjesCmimi;
                }
                formatekurs[j] = colMon.Find(x => x.IdMonedha == idMonedha).IdFormatNrKursi;
                j++;
            }
            return new
            {
                cmimet = col,
                formatisasi = formatesasi,
                formaticmim = formatecmim,
                formatikurs = formatekurs
            };
        }

        internal static object merrAutorizimet(int idPerdoruesi)
        {
            return new colAutorizimetKoka(idPerdoruesi);
        }

        public static AutoCompleteItem[] ktheAktivitete(string prefixText, int idNdermarrje)
        {
            DataTable tmpTable;
            tmpTable = colAktiviteteKoka.ktheGjitheAktivitetetSipasNdermarjesLikeDt(idNdermarrje, prefixText);
            tmpTable.Columns["des"].ColumnName = "desc";
            AutoCompleteItem[] autoCompleteItem = new AutoCompleteItem[tmpTable.Rows.Count];
            for (int i = 0; i < tmpTable.Rows.Count; i++)
            {
                autoCompleteItem[i].label = tmpTable.Rows[i]["label"].ToString();
                autoCompleteItem[i].value = tmpTable.Rows[i]["value"].ToString();
                autoCompleteItem[i].desc = tmpTable.Rows[i]["desc"].ToString();
            }
            return autoCompleteItem;
        }

        public static clsKodifikimArtikulli merrSkeme(int idkodifikim)
        {
            clsKodifikimArtikulli cls = new clsKodifikimArtikulli(idkodifikim);
            return cls;
        }

        public static string merrNgaSessionURLLupaShpejte(string lupa, HttpSessionState Session)
        {
            return mySessionObjects.merrURLLupaShpejteNgaSesioni(Session, lupa);
        }

        public static object[] ktheTeDhenaArtikullit(int idArt, int idNdermarrje)
        {
            object[] obj = new object[2];
            clsArtikulli artikull = new clsArtikulli();
            artikull.mbushArtikull(idArt);
            if (artikull.IdArtikulli != 0)
            {
                obj[0] = artikull;
                if (artikull.DetajimArtikulli == false)
                {
                    obj[1] = clsTrupiMagazina.kaVeprimeMagPerArtikull(artikull.IdArtikulli, idNdermarrje);
                }
                else obj[1] = false;
                //obj[1] = eshteLidhur(int.Parse(idkomp), kodkonfi, artikull.IdArtikulli.ToString());
                return obj;
            }
            else
            {
                obj[0] = null;
                obj[1] = false;
                return obj;
            }
        }

        public static string kontrolloDetajimePerVeprime(string detajimereja, string detajimevjetra, string kodartikulli, int idNdermarrje)
        {
            if (detajimevjetra == "")
                return "";
            string[] detreja = detajimereja.Split(',');
            string[] detvjetra = detajimevjetra.Split(',');
            clsArtikulli art = new clsArtikulli();
            art.mbushArtikull(kodartikulli, idNdermarrje);
            for (int i = 0; i < detvjetra.Length; i++)
            {
                if (!detreja.Contains(detvjetra[i]))
                {
                    clsDetajimArtikulli detajim = new clsDetajimArtikulli();
                    detajim.mbushDetajimArtikulli(detvjetra[i], idNdermarrje);
                    if (clsDetajimArtikulli.kaVeprimeDetajimRegj(detajim.IdDetajimArtikulli, art.IdArtikulli).Status)
                        return "Nuk mund te hiqni detajimin " + detvjetra[i] + " pasi keni kryer veprime me te!";
                }
            }
            return "";
        }

        public static bool eshteKodifikimiKFPrind(string input, int idNdermarrje)
        {
            int idGrupi = Convert.ToInt32(input);
            bool eshtePrind = DbCore.DbKontabiliteti.clsGrupeKF.EshtePrind(idGrupi, idNdermarrje);
            return eshtePrind;
        }

        public static string eshteKodifikimiPrind(string input, int idNdermarrje)
        {
            clsDatabaseInventari data = new clsDatabaseInventari();
            clsKodifikimArtikulli kodifikimartikulli = new clsKodifikimArtikulli();
            kodifikimartikulli.IdKodifikimi = Convert.ToInt32(input);
            kodifikimartikulli.IdNdermarje = idNdermarrje;
            bool eshtePrind = kodifikimartikulli.eshtePrind();
            if (eshtePrind)
            {
                return "prind";
            }
            else
            {
                return "bij";
            }
        }

        internal static object GetGridColumns(HttpSessionState session, string gridId, int komponenteId, int konfigId)
        {
            colGridaTrupi colGrida = new colGridaTrupi(gridId, komponenteId, konfigId, mySessionObjects.ktheGjuhe(session));
            return colGrida.FindAll(x => x.VisibleCostumize == true);
        }

        internal static object GetGridColumnsByIdKonfig(HttpSessionState session, string emerGrida, string emerKomponente, int idKonfig)
        {
            int idKompon = clsKomponente.MerrIdKomponenteSipasEmrit(emerKomponente);
            colGridaTrupi colGrida = new colGridaTrupi(emerGrida, idKompon, idKonfig, mySessionObjects.ktheGjuhe(session));
            return colGrida.FindAll(x => x.VisibleCostumize == true);
        }

        internal static object SaveGridColumns(HttpSessionState session, object gridColumns)
        {
            colGridaTrupi colGrida = JsonConvert.DeserializeObject<colGridaTrupi>(gridColumns.ToString());
            if (colGrida.Update(mySessionObjects.ktheGjuhe(session)))
                return new clsMesazh(true, MessagesResource.Messages["mesazhKonfigurimiSuksess"]);
            return new clsMesazh(false, MessagesResource.Messages["mesazhNdodhiNjeGabimGjateRuatjesSeKonfigurimit"]);
        }

        public static bool eshteKPFPrind(string kodi, int niveli, int grupi, int idNdermarrje)
        {
            bool prind = DbCore.DbKontabiliteti.clsKPF.eshtePrind(kodi, idNdermarrje, grupi, niveli);
            return prind;
        }

        public static string merrNgaSessionURLllog(HttpSessionState Session)
        {
            return mySessionObjects.merrURLllogNgaSesioni(Session);
        }

        public static object ktheKodDheIdTransportuesi(string kodTransportues, int idNdermarrje)
        {
            clsTransportues transp = new clsTransportues(kodTransportues, idNdermarrje);
            return new { emertimi = transp.Emertimi, id = transp.IdTransportues };
        }

        public static object KaVeprimeProfesioni(int id)
        {
            return DbCore.DbListPagesat.clsProfesioneTitujPune.kaVeprimi(id);
        }

        public static bool fshiTeDhenatPivotGrideNgaSessioni(HttpSessionState Session)
        {
            return mySessionObjects.fshiTeDhenaPivotGridNgaSessioni(Session);
        }

        public static bool ekzistonNrLlogarie(string prefixText, int idNdermarrje)
        {
            return DbCore.DbKontabiliteti.clsLlogari.ekzistonLlogari(prefixText, idNdermarrje);
        }

        public static object ktheKonfig(int idKomp, string kodKonf, int idndermarje, int idGjuha)
        {
            int idKonfigurim = -1;
            string kodniveli = "";
            int kategoria;
            if (kodKonf != "")//nese eshte zgjedhur nje konfigurim tek combo e konfigurimeve
            {
                clsKonfigurimAmbjenti clsKonf = new clsKonfigurimAmbjenti();
                clsKonf.mbushKonfiguriminMeKod(kodKonf, idndermarje, idGjuha);
                idKonfigurim = clsKonf.IdKonfigAmbjente;
                kodniveli = kodniveli = clsNivelRegjistrimi.ktheKodNivelRegjistrimi(clsKonf.IdNivel);
                kategoria = clsKonf.IdKategori;
            }
            else
            {//nese nuk ehste zgjedhur asnje konfigurim merret konfigurimi default                
                clsKonfigurimAmbjenti clsKonf = new clsKonfigurimAmbjenti();
                clsKonf.mbushKonfigDefaultKomponentes(idKomp, idndermarje);
                if (clsKonf.IdKonfigAmbjente == 0)
                    clsKonf.mbushKonfigDefaultKomponentes(idKomp, -1);
                idKonfigurim = clsKonf.IdKonfigAmbjente;
                kodniveli = kodniveli = clsNivelRegjistrimi.ktheKodNivelRegjistrimi(clsKonf.IdNivel);
                kategoria = clsKonf.IdKategori;
            }
            colKusht colKushte = new colKusht();
            colKushte.mbushGjitheKushteKonfigurimi(idKonfigurim);
            colAlternativatKushti colAlterKusht = new colAlternativatKushti();
            clsKonfLlojRreshti konfLlojRreshti = new clsKonfLlojRreshti();
            foreach (clsKusht kusht in colKushte)
            {
                if (kusht.Kodi == "LLD") //llojrreshti
                {
                    if (kategoria == 1 || kategoria == 2)
                        konfLlojRreshti = new clsKonfLlojRreshti(kusht.IdKushtTemplate, "Shitje");
                    else
                        konfLlojRreshti = new clsKonfLlojRreshti(kusht.IdKushtTemplate, "ArkaBanka");
                    colAlterKusht.Add(new clsAlternativaKushti());
                    continue;
                }
                clsAlternativaKushti alterKusht = new clsAlternativaKushti();
                if (kusht.Vlera != 0)
                    alterKusht = new clsAlternativaKushti(kusht.Vlera);
                colAlterKusht.Add(alterKusht);
            }
            colAtributeTrupi colAtrTrupi = new colAtributeTrupi();
            colAtrTrupi.mbushKontrolletKonfigurimitKomponentes(idGjuha, idKomp, idKonfigurim);
            colKontrolle colKontrollet = new colKontrolle(idGjuha, idKomp, idKonfigurim);
            colGridaTrupi colGrida = new colGridaTrupi(idKomp, idKonfigurim, idGjuha);
            colFiltratGrida colFiltraGrida = new DbCore.DbAdmin.colFiltratGrida();
            colFiltraGrida.merrFiltraSipasKonfigurimit(idKonfigurim, idndermarje);
            colFiltraGrida.Insert(0, new DbCore.DbAdmin.clsFiltraGrida());
            clsFiltraGrida filtriDefault = clsFunksione.merrFilterDefault(idKonfigurim);

            return new
            {
                colKontrollet = colKontrollet,
                colAtrTrupi = colAtrTrupi,
                colGrida = colGrida,
                colKushte = colKushte,
                colAlterKusht = colAlterKusht,
                kodniveli = kodniveli,
                konfLlojRreshti = konfLlojRreshti,
                colFiltraGrida = colFiltraGrida,
                filtriDefault = filtriDefault
            };
        }


        internal static object GetMenuToolbarItems(HttpSessionState session, string emerKomponente)
        {
            int idGjuha = mySessionObjects.ktheGjuhe(session);
            int idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(session);
            int idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(session);
            int idViti = mySessionObjects.ktheIdVitNdermarrje(session);

            colMenuItem menuItems = new colMenuItem(idGjuha);
            menuItems.merrMenuItemSipasKomponentes(idGjuha, emerKomponente, idPerdoruesi, idNdermarrje, idViti, false);

            return menuItems;
        }

        public static object ktheVleratEkonfigurimitSipaasLlojitKases(int idkonfigurimi, string Lloji)
        {
            if (idkonfigurimi != 0)
            {
                DbCore.DbAdmin.colVleratKonfigurimiKasa OColVlerat = new DbCore.DbAdmin.colVleratKonfigurimiKasa(idkonfigurimi);
                object result = new object();

                switch (Lloji)
                {

                    case "IVA"://Iva
                    case "BNT Aclass Skedar":
                        result = new
                        {
                            lloji = OColVlerat.ktheVlereOpsioni("KASEFISKALELLOJ"),
                            pathFile = OColVlerat.ktheVlereOpsioni("KASEFISKALEPATH"),
                            url = OColVlerat.ktheVlereOpsioni("URL"),
                            meShifraDhjetore = OColVlerat.ktheVlereOpsioniKasaBOOL("MESHIFRADHJETORE"),
                            kaseApoPrinter = OColVlerat.ktheVlereOpsioniKasaBOOL("KASEAPOPRINTER"),
                            printoNrFature = OColVlerat.ktheVlereOpsioniKasaBOOL("PRINTONRFATURE"),
                            cmimMonedheDyte = OColVlerat.ktheVlereOpsioni("CMIMMONEDHEDYTE"),
                            kuponTatimor = OColVlerat.ktheVlereOpsioniKasaBOOL("KUPONTATIMOR"),
                            nrKopjesh = OColVlerat.ktheVlereOpsioni("NRKOPJESH"),
                            nrKopjeshKthimi = OColVlerat.ktheVlereOpsioni("NRKOPJESHKTHIMI"),
                            printomanualisht = OColVlerat.ktheVlereOpsioniKasaBOOL("PRINTOMANUALISHTNGAKASA"),
                            printoNeServer = OColVlerat.ktheVlereOpsioniKasaBOOL("PRINTONESERVER"),
                            printoPershkrim2 = OColVlerat.ktheVlereOpsioniKasaBOOL("PRINTOPERSHKRIM2"),
                            IP = OColVlerat.ktheVlereOpsioni("IPKASE")
                        };
                        break;
                    case "AED":
                        result = new
                        {
                            lloji = OColVlerat.ktheVlereOpsioni("KASEFISKALELLOJ"),
                            pathFile = OColVlerat.ktheVlereOpsioni("KASEFISKALEPATH"),
                            url = OColVlerat.ktheVlereOpsioni("URL"),
                            port = OColVlerat.ktheVlereOpsioni("PORT"),
                            chius = OColVlerat.ktheVlereOpsioni("PORT"),
                            printoBarKod = OColVlerat.ktheVlereOpsioniKasaBOOL("PRINTBARKOD"),
                            nrKopjesh = OColVlerat.ktheVlereOpsioni("NRKOPJESH"),
                            ruajKopje = OColVlerat.ktheVlereOpsioniKasaBOOL("RUAJKOPJE"),
                            meShifraDhjetore = OColVlerat.ktheVlereOpsioniKasaBOOL("MESHIFRADHJETORE"),
                            printoKodArtikulli = OColVlerat.ktheVlereOpsioniKasaBOOL("printoKodArtikulli"),
                            printoNeServer = OColVlerat.ktheVlereOpsioniKasaBOOL("PRINTONESERVER"),
                            printoPershkrim2 = OColVlerat.ktheVlereOpsioniKasaBOOL("PRINTOPERSHKRIM2"),
                            IP = OColVlerat.ktheVlereOpsioni("IPKASE")
                        };
                        break;
                    case "BNT Electronics":
                        result = new
                        {
                            lloji = OColVlerat.ktheVlereOpsioni("KASEFISKALELLOJ"),
                            pathFile = OColVlerat.ktheVlereOpsioni("KASEFISKALEPATH"),
                            url = OColVlerat.ktheVlereOpsioni("URL"),
                            meShifraDhjetore = OColVlerat.ktheVlereOpsioniKasaBOOL("MESHIFRADHJETORE"),
                            kaseApoPrinter = OColVlerat.ktheVlereOpsioniKasaBOOL("KASEAPOPRINTER"),
                            printoNrFature = OColVlerat.ktheVlereOpsioniKasaBOOL("PRINTONRFATURE"),
                            cmimMonedheDyte = OColVlerat.ktheVlereOpsioni("CMIMMONEDHEDYTE"),
                            kuponTatimor = OColVlerat.ktheVlereOpsioniKasaBOOL("KUPONTATIMOR"),
                            nrKopjesh = OColVlerat.ktheVlereOpsioni("NRKOPJESH"),
                            nrKopjeshKthimi = OColVlerat.ktheVlereOpsioni("NRKOPJESHKTHIMI"),
                            printoNeServer = OColVlerat.ktheVlereOpsioniKasaBOOL("PRINTONESERVER"),
                            printoPershkrim2 = OColVlerat.ktheVlereOpsioniKasaBOOL("PRINTOPERSHKRIM2"),
                            IP = OColVlerat.ktheVlereOpsioni("IPKASE")
                        };
                        break;
                    case "CKV-NOKI":
                        result = new
                        {
                            lloji = OColVlerat.ktheVlereOpsioni("KASEFISKALELLOJ"),
                            pathFile = OColVlerat.ktheVlereOpsioni("KASEFISKALEPATH"),
                            printoNrFature = OColVlerat.ktheVlereOpsioni("PRINTONRFATURE"),
                            meSkedar = OColVlerat.ktheVlereOpsioni("MESKEDAR"),
                            url = OColVlerat.ktheVlereOpsioni("URL"),
                            comPort = OColVlerat.ktheVlereOpsioni("COMPORT"),
                            meShifraDhjetore = OColVlerat.ktheVlereOpsioniKasaBOOL("MESHIFRADHJETORE"),
                            boudRate = OColVlerat.ktheVlereOpsioni("BOUDRATE"),
                            printoNeServer = OColVlerat.ktheVlereOpsioniKasaBOOL("PRINTONESERVER"),
                            printoPershkrim2 = OColVlerat.ktheVlereOpsioniKasaBOOL("PRINTOPERSHKRIM2"),
                            IP = OColVlerat.ktheVlereOpsioni("IPKASE")
                        };
                        break;
                    case "PKP":
                        result = new
                        {
                            lloji = OColVlerat.ktheVlereOpsioni("KASEFISKALELLOJ"),
                            pathFile = OColVlerat.ktheVlereOpsioni("KASEFISKALEPATH"),
                            url = OColVlerat.ktheVlereOpsioni("URL"),
                            meShifraDhjetore = OColVlerat.ktheVlereOpsioniKasaBOOL("MESHIFRADHJETORE"),
                            printoNrFature = OColVlerat.ktheVlereOpsioniKasaBOOL("PRINTONRFATURE"),
                            cmimMondheDyte = OColVlerat.ktheVlereOpsioni("CMIMMONEDHEDYTE"),
                            nrKopjesh = OColVlerat.ktheVlereOpsioni("NRKOPJESH"),
                            nrKopjeshKthimi = OColVlerat.ktheVlereOpsioni("NRKOPJESHKTHIMI"),
                            printoNeServer = OColVlerat.ktheVlereOpsioniKasaBOOL("PRINTONESERVER"),
                            printoPershkrim2 = OColVlerat.ktheVlereOpsioniKasaBOOL("PRINTOPERSHKRIM2"),
                            IP = OColVlerat.ktheVlereOpsioni("IPKASE")
                        };
                        break;
                    case "GEKOS":
                        result = new
                        {
                            lloji = OColVlerat.ktheVlereOpsioni("KASEFISKALELLOJ"),
                            pathFile = OColVlerat.ktheVlereOpsioni("KASEFISKALEPATH"),
                            url = OColVlerat.ktheVlereOpsioni("URL"),
                            operatorPass = OColVlerat.ktheVlereOpsioni("OPERATORPASS"),
                            gjuha = OColVlerat.ktheVlereOpsioni("GJUHA"),
                            nivelDefaultTvsh = OColVlerat.ktheVlereOpsioni("NIVELDEFAULTTVSH"),
                            nrKopjesh = OColVlerat.ktheVlereOpsioni("NRKOPJESH"),
                            meShifraDhjetore = OColVlerat.ktheVlereOpsioniKasaBOOL("MESHIFRADHJETORE"),
                            printoNrFature = OColVlerat.ktheVlereOpsioniKasaBOOL("PRINTONRFATURE"),
                            printoNeServer = OColVlerat.ktheVlereOpsioniKasaBOOL("PRINTONESERVER"),
                            printoPershkrim2 = OColVlerat.ktheVlereOpsioniKasaBOOL("PRINTOPERSHKRIM2"),
                            IP = OColVlerat.ktheVlereOpsioni("IPKASE")
                        };
                        break;
                    case "BNT Aclass":
                        result = new
                        {
                            lloji = OColVlerat.ktheVlereOpsioni("KASEFISKALELLOJ"),
                            url = OColVlerat.ktheVlereOpsioni("URL"),
                            comport = OColVlerat.ktheVlereOpsioni("COMPORT"),
                            meShifraDhjetore = OColVlerat.ktheVlereOpsioniKasaBOOL("MESHIFRADHJETORE"),
                            boudrate = OColVlerat.ktheVlereOpsioni("BOUDRATE"),
                            printoNeServer = OColVlerat.ktheVlereOpsioniKasaBOOL("PRINTONESERVER"),
                            printoPershkrim2 = OColVlerat.ktheVlereOpsioniKasaBOOL("PRINTOPERSHKRIM2"),
                            IP = OColVlerat.ktheVlereOpsioni("IPKASE")
                        };
                        break;
                }
                return result;
            }
            return null;
        }

        public static object ruajKonfigMenuMajtas(int idPerdoruesi, int idNdermarrje, string konfigurimi)
        {
            return clsKonfigMenu.ruajKonfigMenuMajtas(idPerdoruesi, idNdermarrje, konfigurimi);
        }

        public static clsMesazh ruajKonfigListRaportesh(int idPerdoruesi, int idNdermarrje, int idModuli, string konfigurimi)
        {
            return clsRaporti.RuajKonfigListRaportesh(idPerdoruesi, idNdermarrje, idModuli, konfigurimi);
        }

        public static int ktheIdKonfigurimiSipasKodit(string kodKonfig, int idNderm)
        {
            return clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(kodKonfig, idNderm);
        }

        public static List<Dictionary<string, object>> ktheTeDrejtaAmbjenteshPerCRM(int idPerdoruesi, int idNdermarrje, int idVitNdermarrje)
        {
            DataTable dt = DbCore.DbAdmin.colTeDrejtaRoli.mbushTeDrejtaRoliPerCRM(idPerdoruesi, idNdermarrje, idVitNdermarrje);
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = dt.Columns.Cast<DataColumn>().ToDictionary(col => col.ColumnName, col => dr[col]);

                if (dr["KOMPONEMRI"].ToString() == "CRMFushaAnkete.aspx")
                    rows.Insert(2, row);
                else if (dr["KOMPONEMRI"].ToString() == "CRMListaAnketa.aspx")
                    rows.Insert(4, row);
                else if (dr["KOMPONEMRI"].ToString() == "CRMLidhAnkete.aspx")
                    rows.Insert(4, row);
                else
                    rows.Add(row);
            }
            return rows;
        }

        public static Object ktheIdMagNgaKodi(string kodMag, int idNdermarrje)
        {
            int idMagazina = clsNjesiAdministrative.ktheIdMagazine(kodMag, idNdermarrje);
            if (idMagazina > 0)
                return new { idObjekti = idMagazina, veprimi = "njesiadmin" };
            else return new { idObjekti = -1, veprimi = "njesiadmin" };
        }

        public static Object ktheIdSerialNgaKodi(string kodSeriali, int idNdermarrje)
        {
            int idSerial = DbCore.DbAsete.clsAQTSeriale.merrIDAQTSerialSipasKodAQT(kodSeriali, idNdermarrje);
            if (idSerial > 0)
                return new { idObjekti = idSerial, veprimi = "seriale" };
            else return new { idObjekti = idSerial, veprimi = "seriale" };
        }

        public static int ktheIdKonfigurimiSipasIdKodifikimi(int idKodifikimi, int idNderm)
        {//perdoret nga gis, kthen id e konfigurimit qe ka kodin njesoj si grupi i artikullit me id e kaluar            
            return clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(clsKodifikimArtikulli.ktheKodKodifikimi(idKodifikimi), idNderm);
        }

        public static List<clsMesazh> rregulloGabimAsistenti(string idPerEkzekutim, int idndermarrje, int idViti)
        {
            List<clsMesazh> mesazhet = new List<clsMesazh>();
            try
            {
                clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();

                DataTable spRregulluese = dbAdmin.ktheSpRregullueseSipasId("(" + idPerEkzekutim + ")");

                if (spRregulluese.Rows.Count == 0)
                {
                    mesazhet.Add(new clsMesazh() { Status = true, PershkrimMesazhi = "Nuk ka asnje sp rregulluese per gabimet e zgjedhura!" });
                    return mesazhet;
                }

                clsMesazh mesazh = new clsMesazh();
                foreach (DataRow row in spRregulluese.Rows)
                {
                    if (!String.IsNullOrEmpty(row.ItemArray[0].ToString()))
                    {
                        switch (row.ItemArray[0].ToString()) {
                            case "prc_Asistenti_RregulloMagazinaMeSerialeTeGabuara":
                            case "prc_Asistenti_DokumentaArkabankaKursNdrysheNeTrupDheFleteKontabel":
                            case "prc_Asistenti_FshiDokQendraKostoTeGjeneruarMeShumeSeNjeHerePerFK":
                                mesazh = dbAdmin.ekzekutoSpRregulluese(row.ItemArray[0].ToString(), idndermarrje);
                                break;
                            case "prc_Update_NrRendorShperndarjeShpenzimi":
                                mesazh = dbAdmin.ekzekutoSpRregulluese(row.ItemArray[0].ToString(), idndermarrje, idViti);
                                break;
                            default:
                                mesazh = dbAdmin.ekzekutoSpRregulluese(row.ItemArray[0].ToString());
                                break;
                        }
                    }
                    else
                        mesazh = new clsMesazh(true, String.Format("Nuk ka sp rregulluese per sp: {0}!", row.ItemArray[1].ToString()));
                    mesazhet.Add(new clsMesazh() { Status = mesazh.Status, PershkrimMesazhi = mesazh.PershkrimMesazhi });
                }
                return mesazhet;
            }
            catch (Exception ex)
            {
                ImbLogger.Error(ex.Message);
                mesazhet.Add(new clsMesazh() { Status = false, PershkrimMesazhi = "Ndodhi nje gabim gjate ekzekutimit te sp rregulluese! Kontrolloni log-un per me shume informacion." });
                return mesazhet;
            }
        }

        public static Object merrTeDhenaArtikulli(int idkomp, string kodkonfi, string idArtikulli, int idndermarje, int gjuhe, bool merrCmime, int idPerdorues, bool merrCmimeBlerje, string kodArtikulli, HttpSessionState Session)
        {
            if (String.IsNullOrEmpty(idArtikulli) || idArtikulli == "0")
                return null;

            string lidhur = eshteLidhur(idkomp, kodkonfi, idArtikulli, idndermarje, gjuhe);
            object[] artPerberes = ktheArtikujPerberes2(int.Parse(idArtikulli), Session);
            Object detajimet = clsArtikulli.MerrDetajimeArtikulli(kodArtikulli, idndermarje, idPerdorues);
            List<string> dataNorma = DbCore.DbAsete.colAseteNormaAmortizimi.merrDataArtikujNorma(int.Parse(idArtikulli));
            colArtikullVfone col = new colArtikullVfone();
            col.merrSipasIdArtikull(int.Parse(idArtikulli));
            if (merrCmime)
                return new { lidhur = lidhur, artPerberes = artPerberes, cmimet = merrcolMeCmimeMeFormatNumrash(int.Parse(idArtikulli), idndermarje, idPerdorues, merrCmimeBlerje), detajimet = detajimet, dataNorma = dataNorma, artVFOne = col };
            else
                return new { lidhur = lidhur, artPerberes = artPerberes, detajimet = detajimet, dataNorma = dataNorma, artVFOne = col };
        }


        /// <summary>
        /// Metode qe sherben per te marre konfigurimet per aplikacionin AlphaWebMobile
        /// </summary>
        /// <param name="perdorues">Perdoruesi i loguar ne AlphaWeb</param>
        /// <param name="ndermarrje">Ndermarrja ne te cilen eshte loguar perdoruesi</param>
        /// <returns></returns>
        public static Object merrKonfigurimeMobile(clsPerdorues perdorues, clsNdermarrje ndermarrje, HttpSessionState Session)
        {
            byte[] toEncodeAsBytes = Encoding.ASCII.GetBytes(perdorues.PerdoruesUsername + ":" + perdorues.PerdoruesPassword);
            return new
            {
                urlMobile = $"{clsServerConfiguration.LexoKonfigurimSipasKey<string>(ServerKonfigKey.URL_MOBILE)}?param=",
                param = new
                {
                    perdoruesObject = perdorues,
                    token = Convert.ToBase64String(toEncodeAsBytes),
                    nodeServerURL = clsServerConfiguration.LexoKonfigurimSipasKey<string>(ServerKonfigKey.NODE_SERVER_URL),
                    ndermarrjeObject = ndermarrje,
                    faqe = clsServerConfiguration.LexoKonfigurimSipasKey<string>(ServerKonfigKey.DEFAULT_PAGE_MOBILE),
                    periudha = mySessionObjects.merrPeriudheKontabel(Session),
                    connectionStringName = DbCore.IMBUtils.DataBase.MyConnectionsManager.GetSelectedConNameServer()
                }
            };
        }

        public static string merrMenuPersonalizuar(HttpSessionState Session, int idNdermarrje, int idPerdoruesi)
        {
            string menute = mySessionObjects.merrMenuPersonalizuar(Session);
            if (menute != "")
                return menute;
            menute = clsKonfigMenu.ktheListKonfigMenu(idPerdoruesi, idNdermarrje);
            mySessionObjects.ruajMenuPersonalizuar(Session, menute);
            return menute;
        }

        public static DataTable merrMenuSipasTeDrejtave(HttpSessionState Session, int idPerdoruesi, int idNdermarrje, int idviti)
        {
            DataTable menute = mySessionObjects.merrMenuSipasTeDrejtave(Session);
            if (mySessionObjects.merrMenuSipasTeDrejtave(Session) != null)
                return menute;
            menute = colTeDrejtaRoli.merrVetemTeDrejtaAmbjete(idPerdoruesi, idNdermarrje, idviti);
            mySessionObjects.ruajMenuSipasTeDrejtave(Session, menute);
            return menute;
        }

        public static colNiveleCmimesh merrNiveleCmimeshNdermarrjeSipasLlojit(HttpSessionState Session, int idNdermarrje, int lloji)
        {
            int idPerdorues = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            colNiveleCmimesh col = new colNiveleCmimesh();
            col.mbushGjitheNiveleCmimeshSipasNdermarjesDheLlojit(idNdermarrje, lloji, idPerdorues);
            return col;
        }

        /// <summary>
        /// marrim ne db konf e dok qe te parsojme url e arketimit sipas komponentes se duhur 
        /// </summary>
        /// <param name="Session"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="idDok"></param>
        /// <returns></returns>
        public static string ktheKonfigurimDok(HttpSessionState Session, string lloji, int IdKoka)
        {
            return clsVeprimBankaKoka.ktheKonfigurimDok(IdKoka, lloji);
        }

        public static List<konfigAmbientiSlim>  ktheKonfigurimAmbjentiSipasNenkategorise(HttpSessionState session, int idNiveli)
        {
            int idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(session);
            int idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(session);
            int idGjuha = mySessionObjects.ktheGjuhe(session);
            int idKatDok = clsNivelRegjistrimi.ktheIdKategoriaNivelRegjistrimi(idNiveli);

            colKonfigurimAmbjenti col = new colKonfigurimAmbjenti();
            col.mbushKonfigAmbjSipasIdKategoriIdNivel(idKatDok, idNiveli, idPerdoruesi, idGjuha, false);

            List<konfigAmbientiSlim> konfigAmbientiSlim = new List<konfigAmbientiSlim>();
            col.ForEach(x => konfigAmbientiSlim.Add(new konfigAmbientiSlim() { IdKonfigAmbjente = x.IdKonfigAmbjente, KodKonfigAmbjente = x.KodKonfigAmbjente }));
            return konfigAmbientiSlim;
        }

        public static object ktheNenkategoriSipasKategorive(HttpSessionState session, object katDokAndKomponentObj, int idNiveli)
        {
            int idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(session);
            int idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(session);
            int idGjuha = mySessionObjects.ktheGjuhe(session);
            int idViti = mySessionObjects.ktheIdVitNdermarrje(session);

            List<Dictionary<string, object>> katDokAndKomponent = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(katDokAndKomponentObj.ToString());
            DataTable tbl = new DataTable();
            DataView allNivelet = new DataView(tbl);

            katDokAndKomponent.ForEach(x =>
            {
                DataView nivelet = new DataView();
                if (string.IsNullOrEmpty(Convert.ToString(x["komponente"])))
                    nivelet = colNivelRegjistrimi.ktheGjitheNivelRegjistrimiSipasKategoriDtCombo(Convert.ToInt32(x["idKatDok"]), idNdermarrje, idPerdoruesi, false);
                else
                    nivelet = colNivelRegjistrimi.ktheGjitheNivelRegjistrimiSipasKategoriDtComboSipasTeDrejtave(Convert.ToInt32(x["idKatDok"]), idNdermarrje, idPerdoruesi, idViti, Convert.ToString(x["komponente"]), false);
                allNivelet.Table.Merge(nivelet.Table);
            });

            int idKatDok = clsNivelRegjistrimi.ktheIdKategoriaNivelRegjistrimi(idNiveli);
            colKonfigurimAmbjenti col = new colKonfigurimAmbjenti();
            col.mbushKonfigAmbjSipasIdKategoriIdNivel(idKatDok, idNiveli, idPerdoruesi, idGjuha, false);

            List<konfigAmbientiSlim> konfigAmbientiSlim = new List<konfigAmbientiSlim>();
            col.ForEach(x => konfigAmbientiSlim.Add(new konfigAmbientiSlim() { IdKonfigAmbjente = x.IdKonfigAmbjente, KodKonfigAmbjente = x.KodKonfigAmbjente }));
            return new { idKatDok, allNivelet = allNivelet.Table, konfigAmbientiSlim };
        }


        internal static object GetPageConfigurations(HttpSessionState session, string emerKomponente)
        {
            object mesazhet = new object();
            object options = new object();
            switch (emerKomponente)
            {
                case "MessageToAll.html":
                    mesazhet = new
                    {
                        MTA_lblInformacion = MessagesResource.Messages["labelAdministrimiInformacion"],
                        MTA_msgAktivizoSignalR = MessagesResource.Messages["MTA_msgAktivizoSignalR"]
                    };
                    options = new
                    {
                        activateSignalR = System.Web.Configuration.WebConfigurationManager.AppSettings["activateSignalR"].ToString()
                    };
                    break;
                default:
                    break;
            }

            return new {  Messages = mesazhet, Options = options };
        }
    }
}