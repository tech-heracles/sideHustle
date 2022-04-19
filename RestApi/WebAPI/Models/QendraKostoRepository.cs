using System;
using System.Collections.Generic;
using System.Linq;
using DbCore.IMBUtils;
using DbCore;
using DbCore.DbKontabiliteti;
using DbCore.DbQendraKosto;
using DbCore.DbShare;
using DbCore.IMBUtils.Messages;
using DbCore.DbAdmin;
using System.Web.SessionState;

namespace RestApi.WebAPI.Models
{
    public class QendraKostoRepository
    {
        internal static object KtheDataSourceKolonash(int idNdermarrje, int idPerdoruesi, int idKokaFleteKontabel, bool gjithellogarite)
        {
            colQendraKosto qk = new colQendraKosto();
            colObjektivaKosto ob = new colObjektivaKosto();
            qk.mbushGjitheQendraKostoBijSipasNdermarjesAktiv(idNdermarrje);
            ob.mbushGjitheObjketivatKostoSipasNdermarjesAktiv(idNdermarrje);
            var qendraKosto = qk;
            var objektivaKosto = ob;
            var llogari = (gjithellogarite) ? colLlogarite.ktheLlogariNdermarrjesAndAutorizimeDTTeMundshmePerQK(idNdermarrje, idPerdoruesi) : colLlogarite.ktheLlogariNdermarrjesAndAutorizimeDTDheKokaFletekontabelPerQK(idNdermarrje, idPerdoruesi, idKokaFleteKontabel);
            return new { qendraKosto, objektivaKosto, llogari };
        }

        internal static object KtheKokaQKSipasIDGjeneruesDheKonfigOseIdKoka(int idGjenerues, int idKonfig, int idKoka, bool sipasKokes)
        {
            clsKokaQendraKosto koka;
            if (sipasKokes)
                koka = new clsKokaQendraKosto(idKoka);
            else
            {
                koka = new clsKokaQendraKosto();
                koka.KtheKokaQKSipasIDGjeneruesDheKonfig(idGjenerues, idKonfig);
            }
            colTrupiQendraKosto trupi = new colTrupiQendraKosto(koka.IdKoka);
            return new { trupi };

        }
        public static object merrPershkrimLlogQKRe(int idLlog, decimal vlefta, string kodqendra, DateTime data, int idNdermarrje)
        {
            data = data.ToLocalTime();
            clsLlogari llog = new clsLlogari(idLlog);
            colLlogarite colLlog = new colLlogarite();
            colLlog.ktheLLogariteNdermarrjesTeMundshmePerQK(idNdermarrje);
            if (colLlog.Exists(p => p.IdLlogari == llog.IdLlogari))
            {
                clsQendraKosto qendra = new clsQendraKosto(kodqendra, idNdermarrje);
                clsNdermarrje nderm = new clsNdermarrje(idNdermarrje);
                decimal kursillog = 1;
                if (llog.IdMonedha == nderm.NdermarrjeMonedha)
                    kursillog = 1;
                else
                {
                    clsKurset kursi = new clsKurset(llog.IdMonedha, data);
                    if (kursi.VleraKursi == 0)
                        kursi.VleraKursi = 1;
                    kursillog = decimal.Parse(kursi.VleraKursi.ToString());
                }
                var vlera = vlefta * kursillog;
                decimal kursiqk = 1;
                if (qendra.IdMonedha == llog.IdMonedha)
                    kursiqk = kursillog;
                else if (qendra.IdMonedha == nderm.NdermarrjeMonedha)
                    kursiqk = 1;
                else
                {
                    clsKurset kursi = new clsKurset(qendra.IdMonedha, data);
                    if (kursi.VleraKursi == 0)
                        kursi.VleraKursi = 1;
                    kursiqk = decimal.Parse(kursi.VleraKursi.ToString());
                }
                return new { nrLlog = llog.NrLlogari, emertim = llog.EmerLlogari1, kodiMonedha = llog.KodiMonedha, vlera = vlera, vleraQK = ((vlera != 0) ? vlera / kursiqk : 0) };
            }
            else
                return new { nrLlog = "", emertim = "", kodiMonedha = "", vlera = 0, vleraQK = 0 };
        }
        internal static object VendosQKoseSkemeNeGride(int idGjenerues, int lloji, string kodi, int idNdermarrje)
        {
            colTrupatFletetKontabel col = new colTrupatFletetKontabel();
            col.mbushTrupiSipasKokesPerQK(idGjenerues);
            clsObjektivaKosto obj = new clsObjektivaKosto(kodi, idNdermarrje);
            colTrupiQendraKosto trupat = new colTrupiQendraKosto();
            int i = 1;
            if (lloji == 1)
            {
                clsQendraKosto qendra = new clsQendraKosto(kodi, idNdermarrje);
                foreach (clsTrupiFleteKontabel trup in col)
                {
                    clsTrupiQendraKosto t = new clsTrupiQendraKosto(i++, 0, qendra.Id, qendra.Kodi, qendra.Pershkrimi, obj.Id, obj.Kodi, obj.Pershkrimi, trup.IdLlogari, trup.NrLlogari, trup.EmerLlogari, trup.DK == "D" ? 1 : 2, trup.VleftaDebiTrupiFleteKontabel, 0, 0, trup.KodMonedha, trup.Kursi, "");
                    trupat.Add(t);
                }
            }
            else
            {
                clsKokaSkemaQK skema = new clsKokaSkemaQK(kodi, idNdermarrje);
                colTrupiSkemaQK trskema = new colTrupiSkemaQK(skema.IdKoka);

                foreach (clsTrupiFleteKontabel trup in col)
                {
                    double perqindjambetur = 100;
                    foreach (clsTrupiSkemaQK tsqk in trskema)
                    {
                        clsTrupiQendraKosto t = new clsTrupiQendraKosto(i++, 0, tsqk.IdQK, tsqk.Kodi, tsqk.Pershkrimi, obj.Id, obj.Kodi, obj.Pershkrimi, trup.IdLlogari, trup.NrLlogari, trup.EmerLlogari, trup.DK == "D" ? 1 : 2, trup.VleftaDebiTrupiFleteKontabel * Convert.ToDouble(tsqk.Perqindja) / 100, 0, 0, trup.KodMonedha, trup.Kursi, "");
                        trupat.Add(t);
                        perqindjambetur -= Convert.ToDouble(tsqk.Perqindja);
                    }

                    if (perqindjambetur > 0)
                    {
                        clsQendraKosto qkp = new clsQendraKosto("QKP", idNdermarrje);
                        clsTrupiQendraKosto t = new clsTrupiQendraKosto(i++, 0, qkp.Id, qkp.Kodi, qkp.Pershkrimi, obj.Id, obj.Kodi, obj.Pershkrimi, trup.IdLlogari, trup.NrLlogari, trup.EmerLlogari, trup.DK == "D" ? 1 : 2, trup.VleftaDebiTrupiFleteKontabel * perqindjambetur / 100, 0, 0, trup.KodMonedha, trup.Kursi, "");
                        trupat.Add(t);
                    }
                }
            }
            trupat.Add(new clsTrupiQendraKosto());
            return trupat;
        }


        internal static object RuajQendraKosto(int idGjuha, int idNdermarrje, int idVitNdermarrje, int idPerdoruesi, int idKoka, object[] trupi, string nrDok, DateTime dteDtDok, string nrRef, DateTime dteDtRegj, string shenime, int idStatusDok, int idKonfig, int idGjenerues, int idKonfigGjenerues, string komponenteNga, string hfShtimModifikim, bool kontrolloLidhur, bool kontrolloShperndare)
        {
            clsMesazh mesazh = new clsMesazh(true);

            clsKonfigurimAmbjenti clsKonfigQK = new clsKonfigurimAmbjenti(idKonfig);

            clsKokaQendraKosto koka = new clsKokaQendraKosto();
            mesazh = ValidoDokumentQenderKosto(koka, idNdermarrje, idVitNdermarrje, idPerdoruesi, idKoka, idStatusDok, clsKonfigQK, komponenteNga, hfShtimModifikim, kontrolloLidhur);

            if (mesazh.Status)
                mesazh = KrijoDokumentQendraKosto(ref koka, idNdermarrje, idVitNdermarrje, idPerdoruesi, idKoka, trupi, nrDok, dteDtDok, nrRef, dteDtRegj, shenime, idStatusDok, clsKonfigQK, idGjenerues, kontrolloShperndare);
            if (!mesazh.Status)
                return mesazh;

            if (hfShtimModifikim == "shtim" || hfShtimModifikim == "klonim")
                mesazh = koka.Ruaj();
            else
            {
                koka.IdKoka = idKoka;
                mesazh = koka.Modifiko(kontrolloLidhur);
            }

            return mesazh;
        }

        internal static clsMesazh ValidoDokumentQenderKosto(clsKokaQendraKosto koka, int idNdermarrje, int idVitNdermarrje, int idPerdoruesi, int idKoka, int idStatusDok, clsKonfigurimAmbjenti clsKonfigQK, string komponenteNga, string hfShtimModifikim, bool kontrolloLidhur)
        {
            clsMesazh mesazh = new clsMesazh(false);

            clsTeDrejtaRoli tedrejtaInfo = new clsTeDrejtaRoli();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idVitNdermarrje, komponenteNga);
            switch (hfShtimModifikim)
            {
                case "shtim":
                case "klonim":
                    if ((idStatusDok == 1 && !tedrejtaInfo.DShtim) || (idStatusDok == 0 && !tedrejtaInfo.DShtimDraft))
                        return new clsMesazh(false, MessagesResource.Messages["msgAdministrimiNukKeniTeDrejteVeprimi"]);
                    break;
                default:
                    if ((idStatusDok == 1 && !tedrejtaInfo.DMod) || (idStatusDok == 0 && !tedrejtaInfo.DModifikimDraft))
                        return new clsMesazh(false, MessagesResource.Messages["msgAdministrimiNukKeniTeDrejteVeprimi"]);
                    if (kontrolloLidhur)
                    {
                        bool lidhur = koka.eshteDokumentILidhur(idKoka, clsKonfigQK.IdNivel);
                        if (!lidhur)
                            return new clsMesazh(false, MessagesResource.Messages["msgDokumentiEshteILidhur"]);
                    }
                    break;
            }

            return new clsMesazh(true, "Kontrolli per dokumentin e Qendrave te Kostos kaloi me sukses!");
        }


        private static clsMesazh KrijoDokumentQendraKosto(ref clsKokaQendraKosto koka, int idNdermarrje, int idVitNdermarrje, int idPerdoruesi, int idKoka, object[] trupatQK, string nrDok, DateTime dteDtDok, string nrRef, DateTime dteDtRegj, string shenime, int idStatusDok, clsKonfigurimAmbjenti clsKonfigQK, int idGjenerues, bool kontrolloShperndare)
        {
            clsMesazh mesazh = new clsMesazh(true);
            colTrupiQendraKosto trupat = new colTrupiQendraKosto();

            if (idGjenerues != 0)
            {
                clsKokaFleteKontabel kokaFk = new clsKokaFleteKontabel(idGjenerues);
                kokaFk.OColTrupi = new colTrupatFletetKontabel(kokaFk.IdKokaFleteKontabel);

                foreach (object o in trupatQK)
                {
                    var trupi = Newtonsoft.Json.JsonConvert.DeserializeObject<clsTrupiQendraKosto>(o.ToString());
                    clsTrupiQendraKosto trup = new clsTrupiQendraKosto(idNdermarrje, trupi, dteDtDok.Date, true, kokaFk.OColTrupi, shenime);
                    if (trup.IdQK != 0)
                        trupat.Add(trup);
                }
                mesazh = koka.KrijoQK(clsKonfigQK.IdNivel, clsKonfigQK.IdKonfigAmbjente, int.Parse(nrRef), dteDtDok.Date, nrDok, 0, idStatusDok, idNdermarrje, idVitNdermarrje, idPerdoruesi, dteDtRegj.Date, shenime, kokaFk.IdNivel, kokaFk.IdKonfigAmbjente, kokaFk.IdKokaFleteKontabel, trupat, kontrolloShperndare, false);

            }
            else
            {
                foreach (object o in trupatQK)
                {
                    var trupi = Newtonsoft.Json.JsonConvert.DeserializeObject<clsTrupiQendraKosto>(o.ToString());
                    clsTrupiQendraKosto trup = new clsTrupiQendraKosto(idNdermarrje, trupi, dteDtDok.Date, false, new colTrupatFletetKontabel(), shenime);
                    if (trup.IdQK != 0)
                        trupat.Add(trup);
                }
                mesazh = koka.KrijoQK(clsKonfigQK.IdNivel, clsKonfigQK.IdKonfigAmbjente, int.Parse(nrRef), dteDtDok.Date, nrDok, 0, idStatusDok, idNdermarrje, idVitNdermarrje, idPerdoruesi, dteDtRegj.Date, shenime, 0, 0, 0, trupat, kontrolloShperndare, false);
            }

            return mesazh;
        }

        //internal static object RuajTrupRegjQendraKosto(bool kontrolloShperndare, object[] trupiQK, int idNdermarrje, int idVitNdermarrje, int idPerdorues, int idKoka, int idGjenerues, int idKonfigGjenerues, int idKonfig, string kodKonfig, string nrDok, string nrRef, string shenime, string hfShtimModifikim, DateTime dteDtDok, DateTime dteDtRegj, int idStatusDok, string komponenteNga, bool kontrolloLidhur, bool hfLidhur, int idGjuha)
        //{
        //    clsKokaQendraKosto koka = new clsKokaQendraKosto();
        //    clsMesazh mesazh;
        //    try
        //    {
        //        if (idKonfig == 0 && kodKonfig != "")
        //            idKonfig = clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(kodKonfig, idNdermarrje);

        //        koka = krijoRegjistrim(trupiQK, idNdermarrje, idGjenerues, idKonfig, idPerdorues, idVitNdermarrje, dteDtDok, dteDtRegj, nrDok, nrRef, shenime, idStatusDok, kontrolloShperndare);
        //        if (koka.ColTrupi.Count == 0)
        //            return new clsMesazh(false, MessagesResource.Messages["msgTrupiDokNukDuhetBosh"]);
        //    }
        //    catch (Exception ex)
        //    {
        //        NLog.LogManager.GetCurrentClassLogger().Error(ex.Message);
        //        return new clsMesazh(false, ex.Message);
        //    }

        //    clsTeDrejtaRoli tedrejtaInfo = new clsTeDrejtaRoli();
        //    tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdorues, idNdermarrje, idVitNdermarrje, komponenteNga);

        //    if (hfShtimModifikim == "shtim" || hfShtimModifikim == "klonim")
        //    {
        //        if ((idStatusDok == 1 && !tedrejtaInfo.DShtim) || (idStatusDok == 0 && !tedrejtaInfo.DShtimDraft))
        //                return new clsMesazh(false, MessagesResource.Messages["msgAdministrimiNukKeniTeDrejteVeprimi"]);
        //        mesazh = koka.Ruaj();
        //    }
        //    else
        //    {
        //        if ((idStatusDok == 1 && !tedrejtaInfo.DMod) || (idStatusDok == 0 && !tedrejtaInfo.DModifikimDraft))
        //            return new clsMesazh(false, MessagesResource.Messages["msgAdministrimiNukKeniTeDrejteVeprimi"]);

        //        if (kontrolloLidhur)
        //        {
        //            koka.IdKoka = idKoka;
        //            bool lidhur = koka.EshteILidhur();
        //            if (lidhur != hfLidhur)
        //                return new clsMesazh(false, MessagesResource.Messages["msgDokumentiEshteILidhur"]);
        //            else
        //                if (lidhur == true)
        //                mesazh = koka.Modifiko(true);
        //            else
        //                mesazh = koka.Modifiko(false);
        //        }
        //        else
        //        {
        //            koka.IdKoka = idKoka;
        //            mesazh = koka.Modifiko(false);
        //        }
        //    }

        //    if (!mesazh.Status)
        //        return mesazh;

        //    return mesazh;
        //}

        internal static object MerrTrupFKSipasKokes(int idKoka)
        {
            return new colTrupatFletetKontabel(idKoka);
        }


        public static object LlogaritVleraQKMeKursTrupi(string nrllogari, string kodqendra, DateTime data, decimal vlefta, string lloji, decimal kursiTrupi, int idNdermarrje)
        {
            return LlogaritVleraQendraKosto(nrllogari, kodqendra, data, vlefta, lloji, kursiTrupi, true, idNdermarrje);
        }

        public static object LlogaritVleraQK(string nrllogari, string kodqendra, DateTime data, decimal vlefta, string lloji, int idNdermarrje)
        {
            return LlogaritVleraQendraKosto(nrllogari, kodqendra, data, vlefta, lloji, 0, false, idNdermarrje);
        }

        public static object LlogaritVleraQendraKosto(string nrllogari, string kodqendra, DateTime data, decimal vlefta, string lloji, decimal kursiTrupi, bool merrKursTrupi, int idNdermarrje)
        {
            data = data.ToLocalTime();
            decimal vleftallog = 0, vleftamonbaze = 0, vleftaqk = 0;
            clsLlogari llog = new clsLlogari(nrllogari, idNdermarrje);
            clsQendraKosto qendra = new clsQendraKosto(kodqendra, idNdermarrje);
            clsNdermarrje nderm = new clsNdermarrje(idNdermarrje);
            switch (lloji)
            {
                case "vleftallog":
                    vleftallog = vlefta;
                    if (nderm.NdermarrjeMonedha == llog.IdMonedha)
                        vleftamonbaze = vlefta;
                    else
                    {
                        clsKurset kursi = new clsKurset(llog.IdMonedha, data);
                        if (kursi.VleraKursi == 0)
                            kursi.VleraKursi = 1;
                        vleftamonbaze = vlefta * ((merrKursTrupi) ? kursiTrupi : decimal.Parse(kursi.VleraKursi.ToString()));
                    }
                    if (qendra.Id != 0 && qendra.Id != -1)
                    {
                        if (qendra.IdMonedha == llog.IdMonedha)
                            vleftaqk = vlefta;
                        else if (qendra.IdMonedha == nderm.NdermarrjeMonedha)
                            vleftaqk = vleftamonbaze;
                        else
                        {
                            clsKurset kursi = new clsKurset(qendra.IdMonedha, data);
                            if (kursi.VleraKursi == 0)
                                kursi.VleraKursi = 1;
                            vleftaqk = (decimal)vleftamonbaze / ((merrKursTrupi) ? kursiTrupi : decimal.Parse(kursi.VleraKursi.ToString()));
                        }
                    }
                    else
                    {
                        vleftaqk = 0;
                    }
                    break;
                case "vleftamon":
                    vleftamonbaze = vlefta;
                    if (nderm.NdermarrjeMonedha == llog.IdMonedha)
                        vleftallog = vlefta;
                    else
                    {
                        clsKurset kursi = new clsKurset(llog.IdMonedha, data);
                        if (kursi.VleraKursi == 0)
                            kursi.VleraKursi = 1;
                        vleftallog = vlefta / ((merrKursTrupi) ? kursiTrupi : decimal.Parse(kursi.VleraKursi.ToString()));
                    }

                    if (qendra.Id != 0 && qendra.Id != -1)
                    {
                        if (qendra.IdMonedha == llog.IdMonedha)
                            vleftaqk = vleftallog;
                        else if (qendra.IdMonedha == nderm.NdermarrjeMonedha)
                            vleftaqk = vleftamonbaze;
                        else
                        {
                            clsKurset kursi = new clsKurset(qendra.IdMonedha, data);
                            if (kursi.VleraKursi == 0)
                                kursi.VleraKursi = 1;
                            vleftaqk = (decimal)vleftamonbaze / ((merrKursTrupi) ? kursiTrupi : decimal.Parse(kursi.VleraKursi.ToString()));
                        }
                    }
                    else
                    {
                        vleftaqk = 0;
                    }
                    break;
                case "vleftaQK":
                    vleftaqk = vlefta;
                    if (qendra.Id != 0 && qendra.Id != -1)
                    {
                        if (qendra.IdMonedha == nderm.NdermarrjeMonedha)
                            vleftamonbaze = vleftaqk;
                        else
                        {
                            clsKurset kursi = new clsKurset(qendra.IdMonedha, data);
                            if (kursi.VleraKursi == 0)
                                kursi.VleraKursi = 1;
                            vleftamonbaze = (decimal)vleftaqk * ((merrKursTrupi) ? kursiTrupi : decimal.Parse(kursi.VleraKursi.ToString()));
                        }
                        if (qendra.IdMonedha == llog.IdMonedha)
                            vleftallog = vleftaqk;
                        else if (nderm.NdermarrjeMonedha == llog.IdMonedha)
                            vleftallog = vleftamonbaze;
                        else
                        {
                            clsKurset kursi = new clsKurset(llog.IdMonedha, data);
                            if (kursi.VleraKursi == 0)
                                kursi.VleraKursi = 1;
                            vleftallog = (decimal)vleftamonbaze / ((merrKursTrupi) ? kursiTrupi : decimal.Parse(kursi.VleraKursi.ToString()));
                        }
                    }
                    else
                    {
                        vleftamonbaze = 0;
                        vleftallog = 0;
                    }
                    break;
            }
            return new { vleftallog, vleftamonbaze, vleftaqk };
        }

        public static object merrPershkrimLlog(int idLlog, int idGjenerues, int idMonedhaQendra, DateTime data, int idNdermarrje)
        {
            data = data.ToLocalTime();
            clsTrupiFleteKontabel tr = new clsTrupiFleteKontabel(idGjenerues, idLlog);
            clsNdermarrje nderm = new clsNdermarrje(idNdermarrje);
            decimal kursiqk = 1;
            if (idMonedhaQendra == tr.IdMonedha)
                kursiqk = Convert.ToDecimal((double)tr.Kursi);
            else if (idMonedhaQendra == nderm.NdermarrjeMonedha)
                kursiqk = 1;
            else
            {
                clsKurset kursi = new clsKurset(idMonedhaQendra, data);
                if (kursi.VleraKursi == 0)
                    kursi.VleraKursi = 1;
                kursiqk = decimal.Parse(kursi.VleraKursi.ToString());
            }
            return new { DebiKredi = tr.DK, VleftaDebiTrupiFK = tr.VleftaDebiTrupiFleteKontabel, VleftaDebiTrupiFKMonBaze = tr.VleftaDebiTrupiFleteKontabel * tr.Kursi, kursiQK = kursiqk };
        }
    }
}
