using DbCore.DbAdmin;
using DbCore.DbKontabiliteti;
using DbCore.DbQendraKosto;
using DbCore.DbRegjistrim;
using DbCore.DbShare;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.SessionState;
using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.Messages;
using DbCore.IMBUtils.DataBase;

namespace DbCore.DbListPagesat
{
    public class ListePagesaRegjistrues
    {
        #region properties
        private int statusDokumenti = 0;
        private StatusAprovimi statusAprovimi = StatusAprovimi.Undefined;
        private bool modifikim = false;

        public string Veprimi { get; set; }
        public string ShtimModifikim { get; set; }
        public DateTime DtDok { get; set; }
        public DateTime DtRegjistrimi { get; set; }
        public int Kontabilizim { get; set; }
        public int IdNgaQueryString { get; set; }
        public int IdEtapa { get; set; }
        public string ServerUrl { get; set; }

        public string IdKonfigurimi { get; set; }
        public string KodKonfigurimi { get; set; }
        public decimal Vlefta { get; set; }
        public decimal Vlefta2 { get; set; }

        public string NrDok { get; set; }
        public int Muaji { get; set; }
        public int IdMonedha { get; set; }
        public string KodMonedha { get; set; }
        public decimal Kursi { get; set; }
        public string Shenime { get; set; }

        public System.Resources.ResourceManager Rm { get; set; }
        public System.Globalization.CultureInfo Ci { get; set; }

        public int IdGjuha { get; set; }
        public int IdPerdoruesi { get; set; }
        public int IdNdermarrje { get; set; }
        public int IdNdermarrjeVit { get; set; }
        public int IdViti { get; set; }
        public clsPeriudhaKontabel Periudha { get; set; }

        public string GridDataObject { get; set; }
        public string HfKomp { get; set; }
        public int IdSkema { get; set; }
        public string Lidhur { get; set; }
        public string lblStatusAprovimi { get; set; }
        public string nrAutoNrDok { get; set; }
        public int IdDepartamenti { get; set; }
        public int IdNenDepartamenti { get; set; }
        public string departamenti { get; set; }
        public string nendepartamenti { get; set; }
        public IDictionary<string, object> HfArkiva { get; set; }
        #endregion properties

        public ListePagesaRegjistrues()
        {
        }

        public ListePergjigjeRegjistrimListepagese Ruaj(HttpSessionState session, string hfStatus1, string pergjigja)
        {
            percaktoStatusAprovimi();
            ListePergjigjeRegjistrimListepagese pergjigje = new ListePergjigjeRegjistrimListepagese();
            pergjigje.StatusDokumenti = statusDokumenti;
            pergjigje.hfStatus1Value = hfStatus1;
            pergjigje.pergjigjaValue = pergjigja;

            clsMesazh mesazh = isValidRegjistrimDokumenti();

            if (!mesazh)
            {
                pergjigje.Mesazh = mesazh;
                pergjigje.hfStatus1Value = "false";
                return pergjigje;
            }
            if (ShtimModifikim == "modifikim" && IdEtapa == 0)
            {
                mesazh = kontrolloEtape();
                if (!mesazh)
                {
                    pergjigje.Mesazh = mesazh;
                    return pergjigje;
                }
            }
            try
            {
                var shfaqmesazhapolupe = "jo";
                var regjistrim = krijoRegjistrimListPagese(statusDokumenti, Periudha.IdPeriudha, Kontabilizim, out shfaqmesazhapolupe, statusAprovimi, IdEtapa, session);
                var colllog = new colLlogariShperndarjeQK(IdNdermarrje);

                if (colllog.Count == 0)
                {
                    shfaqmesazhapolupe = "jo";
                }
                if (regjistrim.OColTrupi.Count == 0)
                {
                    pergjigje.hfStatus1Value = "false";
                    return pergjigje;
                }

                if (!modifikim && statusAprovimi == StatusAprovimi.Undefined)
                    IdSkema = 0;
                var tedrejtaInfo = new clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrje, IdViti, "Shto_ListPagesa.aspx");

                if (ShtimModifikim == "shtim" || ShtimModifikim == "klonim")
                {
                    if ((statusDokumenti == 1 && !tedrejtaInfo.DShtim) || (statusDokumenti == 0 && !tedrejtaInfo.DShtimDraft))
                    {
                        pergjigje.Mesazh = new clsMesazh(false, Rm.GetString("msgNukKeniTeDrejta", Ci));
                        pergjigje.hfStatus1Value = "false";
                        return pergjigje;
                    }

                    Dictionary<string, object> hfNrAutoShitje = new Dictionary<string, object>();
                    if (!String.IsNullOrEmpty(nrAutoNrDok))
                        hfNrAutoShitje.Add("txtNrDok", nrAutoNrDok);
                    mesazh = regjistrim.ruaj(Kontabilizim, hfNrAutoShitje, Rm, Ci, IdSkema, statusAprovimi, IdEtapa, ServerUrl);
                }
                else if (ShtimModifikim == "modifikim")
                {
                    if ((statusDokumenti == 1 && !tedrejtaInfo.DMod) || (statusDokumenti == 0 && !tedrejtaInfo.DModifikimDraft))
                    {
                        pergjigje.Mesazh = new clsMesazh(false, Rm.GetString("msgNukKeniTeDrejta", Ci));
                        pergjigje.hfStatus1Value = "false";
                        return pergjigje;
                    }

                    regjistrim.IdKoka = IdNgaQueryString;
                    var kokaekzistuese = new clsKokaListPagese(regjistrim.IdKoka, false);
                    if (statusAprovimi == StatusAprovimi.Undefined)
                    {
                        regjistrim.StatusAprovimi = kokaekzistuese.StatusAprovimi;
                    }
                    var lidhur = regjistrim.eshteILidhur();
                    if (lidhur.ToString() != Lidhur)
                    {
                        mesazh.Status = false;
                        mesazh.PershkrimMesazhi = Rm.GetString("msgDokumentiEshteILidhur", Ci);
                        pergjigje.hfStatus1Value = "false";
                    }
                    else
                    {
                        if (lblStatusAprovimi != string.Empty && lblStatusAprovimi != StatusAprovimi.Aprovuar.ToString())
                            lidhur = true;

                        mesazh = regjistrim.modifiko(Kontabilizim, lidhur, Rm, Ci, IdSkema, statusAprovimi, IdEtapa, ServerUrl);

                    }
                }

                pergjigje.pergjigjaValue = "ruaj";
                if (mesazh.Status)
                {
                    pergjigje.hfNewIdValue = regjistrim.IdKoka.ToString();
                    if (statusDokumenti == 1 && clsAlternativaKushti.getAlternativa(regjistrim.IdKonfigAmbjente, "KP") == "Po")
                    {
                        var periudhe = new clsPeriudhaKontabel(regjistrim.DtDok, regjistrim.IdNdermarje)
                        {
                            Ekycur = true
                        };
                        mesazh = periudhe.modifikokycurperiudha(periudhe.IdPeriudha, true, Rm, Ci);

                        if (!mesazh.Status)
                        {
                            pergjigje.Mesazh = new clsMesazh(false, mesazh.PershkrimMesazhi + " " + Rm.GetString("msgKycjaEPeriudhesNukUBe", Ci));

                            pergjigje.hfStatus1Value = "true";
                            pergjigje.hfEshteNeCikelAprovimiValue = (regjistrim.StatusAprovimi != StatusAprovimi.Undefined).ToString();
                            pergjigje.hfShtimModifikimValue = "shtim";
                            return pergjigje;
                        }

                    }

                    pergjigje.hfqkmesazhiValue = shfaqmesazhapolupe;
                    if (shfaqmesazhapolupe != "jo")
                    {
                        var kok = new clsKokaFleteKontabel(regjistrim.IdKoka, 38);
                        pergjigje.hfUrlValue = $"LupaRegjistrimQendraKosto.aspx?shtim_modifikim=modifikim&idDokGjenerues={kok.IdKokaFleteKontabel}&idkonfig={kok.IdKonfigAmbjente}";
                    }
                    pergjigje.Mesazh = mesazh;


                    pergjigje.hfEshteNeCikelAprovimiValue = (regjistrim.StatusAprovimi != StatusAprovimi.Undefined).ToString();
                    pergjigje.hfStatus1Value = "true";


                    pergjigje.hfShtimModifikimValue = "shtim";
                    ShtimModifikim = "shtim";
                }
                else
                {
                    pergjigje.Mesazh = mesazh;
                    pergjigje.hfStatus1Value = "false";
                }
            }
            catch (Exception ex)
            {
                ImbLogger.Error(ex.Message);
                pergjigje.Mesazh = new clsMesazh(false, ex.Message);
                pergjigje.hfStatus1Value = "false";
            }


            var kusht = new clsKusht(Convert.ToInt32(IdKonfigurimi), "ZSP");
            pergjigje.visibleMenus = clsFunksione.merrMenu(IdPerdoruesi, kusht.Vlera, ShtimModifikim != "modifikim", lblStatusAprovimi, IdNgaQueryString, KodKonfigurimi, 38);

            pergjigje.hfTeDrejtaModSkemaValue = pergjigje.visibleMenus[6] ? "True" : "False";
            return pergjigje;
        }

        private void percaktoStatusAprovimi()
        {
            switch (Veprimi)
            {
                case "Ruaj":
                    statusDokumenti = 1;
                    break;
                case "Draft":
                    break;
                case "Aprovo":
                    statusAprovimi = StatusAprovimi.Per_Aprovim;
                    break;
                case "Refuzo":
                    statusAprovimi = StatusAprovimi.Refuzuar;
                    break;
                case "Modifiko":
                    modifikim = true;
                    break;
                case "Delego":
                    statusAprovimi = StatusAprovimi.Deleguar;
                    break;
            }
        }

        private clsMesazh isValidRegjistrimDokumenti()
        {
            if (DtDok == DateTime.MinValue)
            {
                return new clsMesazh(false, Rm.GetString("msgLidhjaDokZgjidh1DateDokumenti", Ci));
            }

            if (ShtimModifikim != "shtim" && ShtimModifikim != "klonim")
                Periudha = new clsPeriudhaKontabel(DtDok, IdNdermarrje);

            string mesazhGabimi;
            if (!clsFunksione.checkPeriudheKontabel(out mesazhGabimi, DtDok, Periudha, statusDokumenti))
                return new clsMesazh(false, mesazhGabimi);

            if (MbylljePeriudhe.PeriodClosing.IsPeriodClosed(DtDok, MyConnectionsManager.GetSelectedConNameServer(), IdNdermarrje, KategoriDokumenti.ListPagesa, Convert.ToInt32(IdKonfigurimi)))
                return new MesazhGabimi(MessagesResource.Messages["msgPeriodIsClosed"]);


            if (DtRegjistrimi == DateTime.MinValue)
                return new clsMesazh(false, Rm.GetString("msgListPagesaZgjidhniNjeDateRegjistrimi", Ci));
            if (DtDok.Year != new clsNdermarrjeViti(IdNdermarrjeVit).Viti)
                return new clsMesazh(false, Rm.GetString("msgDataNukPerketVititUshtrimor", Ci));
            if (DtDok.Month != Muaji)
                return new clsMesazh(false, Rm.GetString("msgListPagesaMuajiNukPerkonMeMuajinEDates", Ci));
            if (departamenti != "" )
            {
                clsStrukturaAdministrative dep = (IdDepartamenti != 0) ? new clsStrukturaAdministrative(IdDepartamenti) : new clsStrukturaAdministrative(IdNdermarrje, departamenti);

                if (!clsStrukturaAdministrative.ekzistonEmri(departamenti, IdNdermarrje))
                    return new clsMesazh(false, $"Departamenti {departamenti} nuk ekziston!");
                else if (dep.Aktive == false)
                {
                    string mesazh = string.Format(MessagesResource.Messages["msgdepartamentijoaktiv"], dep.Kodi);
                    return new clsMesazh(false, mesazh);
                    
                    

                }
               
                else if (nendepartamenti != "")
                {
                    clsStrukturaAdministrative nendep = (IdNenDepartamenti != 0) ? new clsStrukturaAdministrative(IdNenDepartamenti) : new clsStrukturaAdministrative(IdNdermarrje, nendepartamenti);

                    if (!clsStrukturaAdministrative.ekzistonEmri(nendepartamenti, IdNdermarrje))
                        return new clsMesazh(false, $"Nendepartamenti {nendepartamenti} nuk ekziston!");
                   else if (nendep.Aktive == false)
                   {
                        string mesazh = string.Format(MessagesResource.Messages["msgdepartamentijoaktiv"], nendep.Kodi);
                        return new clsMesazh(false, mesazh);
                   }
                    
                }

                
               
            }

           
           
            return new clsMesazh(true);
        }

        private clsMesazh kontrolloEtape()
        {

            var etapafund = new clsEtapeAprovimi();
            etapafund.ktheEtapeFunditSipasKokaListpagesaDhePerdorues(IdNgaQueryString, IdPerdoruesi);
            IdEtapa = etapafund.IdEtapa;

            var col = new colKomenteAprovimi();
            col.merrKomenteSipasEtapes(IdEtapa);
            if (statusAprovimi == StatusAprovimi.Refuzuar)
                if (col.Count == 0)
                {
                    return new clsMesazh(false, Rm.GetString("msgShenoniKomentRefuzim", Ci));
                }
                else
                {
                    var kakoment = col.Any(k => k.IdPerdoruesi == IdPerdoruesi);
                    if (!kakoment)
                    {
                        return new clsMesazh(false, Rm.GetString("msgShenoniKomentRefuzim", Ci));
                    }
                }
            return new clsMesazh(true);
        }

        private clsKokaListPagese krijoRegjistrimListPagese(int statusDokumenti, int idperiudha, int mekontabilizim, out string shfaqmesazhapolupe, StatusAprovimi statusAprovimi, int idEtapa, HttpSessionState session)
        {
            if (!string.IsNullOrEmpty(nrAutoNrDok))
            {
                NrAuto nrAuto = JsonConvert.DeserializeObject<NrAuto>(nrAutoNrDok);
                if (nrAuto.vlereNrAuto != NrDok)
                    nrAuto.isModified = true;
                nrAutoNrDok = JsonConvert.SerializeObject(nrAuto);
            }

            var clsKonf = new clsKonfigurimAmbjenti();
            clsKonf.mbushKonfigDefaultKomponentes(710, IdNdermarrje);
            if (!String.IsNullOrEmpty(IdKonfigurimi) && KodKonfigurimi != string.Empty)
            {
                clsKonf.mbushKonfigAmbjSipasKod(KodKonfigurimi, IdNdermarrje);
            }
            decimal totali = Vlefta;

            var kontabilizo = mekontabilizim != 0;
            var qend = new clsKokaQendraKosto();
            if (ShtimModifikim == "modifikim")
            {
                var kok = new clsKokaFleteKontabel(IdNgaQueryString, 38);
                qend.KtheKokaQKSipasIDGjeneruesDheKonfigMeTrup(kok.IdKokaFleteKontabel, kok.IdKonfigAmbjente);
            }
            var idperdoruesi = IdPerdoruesi;
            if (statusAprovimi == StatusAprovimi.Deleguar)
            {
                var etapa = new clsEtapeAprovimi(idEtapa, 38);
                if (etapa.LlojAprovuesi == 2) //nqs eshte rol
                {
                    var col = new colRolPerdorues();
                    col.mbushRolePerdoruesSipasRoli(etapa.IdAprovuesi);
                    if (col.Count > 0)
                        idperdoruesi = col[0].IdPerdorues;
                }
            }
            var koka = new clsKokaListPagese();

            koka.krijoListPagese(clsKonf.IdNivel, clsKonf.IdKonfigAmbjente, IdDepartamenti, IdNenDepartamenti, DtDok, NrDok, Convert.ToInt32(Muaji), totali, IdMonedha, KodMonedha, 0, Kursi, statusDokumenti, IdNdermarrje, IdNdermarrjeVit, idperdoruesi, DtRegjistrimi, Shenime, 0, 0, 0, krijoTrupin(session, DtDok), idperiudha, kontabilizo, out shfaqmesazhapolupe, qend.ColTrupi, IdGjuha, Rm, Ci, statusAprovimi, Vlefta2, HfArkiva);
            return koka;
        }


        private colTrupiListPagese krijoTrupin(HttpSessionState session, DateTime data)
        {
            var dokumenti = JsonConvert.DeserializeObject<Dictionary<string, string>[]>(GridDataObject); //rreshtat e dokumentit
            var komponente = JsonConvert.DeserializeObject<List<colKompListPagese>>(HfKomp);
            var numraPersonalMeId = colPunonjes.GetDictionaryNrPersonalIdPunonjesi(IdNdermarrje);
            var trupatNew = new colTrupiListPagese(numraPersonalMeId, dokumenti, komponente, IdNdermarrje, session, IdGjuha, data);
            return trupatNew;
        }

    }
}
