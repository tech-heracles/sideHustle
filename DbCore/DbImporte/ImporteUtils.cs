using DbCore.DbAdmin;
using DbCore.DbAsete;
using DbCore.DbInventari;
using DbCore.DbKontabiliteti;
using DbCore.DbListPagesat;
using DbCore.DbRegjistrim;
using DbCore.DbShare;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.Messages;
using DbCore.Raporte;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Web.SessionState;

namespace DbCore.DbImporte
{
    public class ImporteUtils
    {
        public int IdGjuha { get; set; }
        public ResourceManager Rm { get; set; }
        public CultureInfo Ci { get; set; }
        public int IdNdermarrja { get; set; }
        public int IdPerdoruesi { get; set; }
        public int IdNdermarrjeVit { get; set; }
        public int IdViti { get; set; }
        public int VitiNdermarrjes { get; set; }
        public bool VjenNgaImportSQL { get; set; }
        public int IdKategoria { get; set; }
        public bool Permbledhese { get; set; }
        public string TabKoka { get; set; }
        public string TabTrupi { get; set; }
        public string TabReceptura { get; set; }
        public string Status { get; set; }
        public clsKonfigImporti KonfigImporti { get; set; }
        public clsKokaFormatImporti FormatImporti { get; set; }
        public HttpSessionState Session { get; set; }

        public clsMesazh KontrolloImporto(bool mbishkruajVleratEMeparshme, out bool kaVleraTeImportuara, DataTable teDhenaImporti, ref DataTable rreshtaJoOk, bool importo)
        {
            kaVleraTeImportuara = false;
            try
            {
                DataTable err = new DataTable();
                int rreshta = teDhenaImporti.Rows.Count;
                err.Columns.Add("Kodi");
                err.Columns.Add("Gabimi");
                err.Columns.Add("Rreshti me id");
                DataTable rreshtaok = new DataTable();
                DataTable deadLocked = new DataTable();
                rreshtaJoOk = teDhenaImporti.Clone();
                rreshtaok = teDhenaImporti.Copy();

                Tuple<int, string> pozicionKodiDheError = kthePozicionKodiDhePershkrimErrori(IdKategoria);
                int pozicionKodi = pozicionKodiDheError.Item1;
                string pershkrimErrori = pozicionKodiDheError.Item2;

                colTrupiFormatImporti col = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKoka(FormatImporti.IdKoka);
                clsMesazh mesazh = new clsMesazh();
                if (IdKategoria != 13 && IdKategoria != 12)
                {
                    mesazh = clsKokaFormatImporti.kontrolloDataTable(teDhenaImporti, FormatImporti.IdKoka, err, rreshtaok, rreshtaJoOk, importo, pozicionKodi, FormatImporti.IdKategori, col, IdNdermarrja);
                    if (!mesazh.Status && !importo)
                    {
                        clsKokaErrorImporti koka = new clsKokaErrorImporti(0, pershkrimErrori, IdKategoria, IdNdermarrja, IdPerdoruesi);

                        koka.ColTrupi.mbushErrorImportiNgaAmbienti(err);
                        mesazh = koka.ruajErrorImporti();
                        mySessionObjects.ruajTabeleGabimeshImporti(Session, err);
                        return new MesazhGabimi(MessagesResource.Messages["msgGabimeNeRReshtatEgrides"]);
                    }
                }

                Tuple<clsMesazh, string> mesazhDheError = KontrolloOseImportoDokument(IdKategoria, col, teDhenaImporti, err, rreshtaok, ref rreshtaJoOk, ref deadLocked, pozicionKodi, importo, mbishkruajVleratEMeparshme, mesazh);
                mesazh = mesazhDheError.Item1;
                pershkrimErrori = mesazhDheError.Item2;

                kaVleraTeImportuara = (mesazh.KodMesazhi == 100);
                mySessionObjects.ruajTabeleGabimeshImporti(Session, err);

                if (importo && rreshtaJoOk.Rows.Count > 0 && VjenNgaImportSQL)
                {

                    IEnumerable<int> idTePaImportuara = rreshtaJoOk.AsEnumerable().Select(val => Convert.ToInt32(val["IDIMPORTSHITJE"]));
                    IEnumerable<int> idDeadLocked = deadLocked.AsEnumerable().Select(val => Convert.ToInt32(val["IDIMPORTSHITJE"]));
                    var perStatus3 = idTePaImportuara.Except(idDeadLocked);

                    string ndermarrjeKey = FormatImporti.ColTrupi.ktheEmerImportiSipasKodKontrolli("Kod Ndermarrje");
                    clsDatabazeImporte dbImport = new clsDatabazeImporte();
                    string idPerUpdate = string.Join("','", perStatus3.ToList());
                    ImbLogger.LogInfoImporti("ID e dokumentave me gabime qe nuk u importuan nga tabela " + TabKoka + " : " + idPerUpdate.Replace("'", "") + " .");
                    colImportSQL.updateDokTabeleTemportal(idPerUpdate, IdNdermarrja, 3, TabKoka, "IDIMPORTSHITJE", ndermarrjeKey, dbImport);
                }

                if (err.Rows.Count > 0)
                {
                    clsKokaErrorImporti koka = new clsKokaErrorImporti(0, pershkrimErrori, IdKategoria, IdNdermarrja, IdPerdoruesi);
                    koka.ColTrupi.mbushErrorImportiNgaAmbienti(err);
                    koka.ruajErrorImporti();

                    if (!importo)
                    {
                        return new MesazhGabimi(mesazh.KodMesazhi == 200 ? MessagesResource.Messages["msgGabimIPapriturNeKontrollImporti"]
                                                                         : MessagesResource.Messages["msgGabimeNeRReshtatEgrides"]);
                    }
                    else
                    {
                        if (mesazh.KodMesazhi == 200)
                            return new MesazhGabimi(MessagesResource.Messages["msgGabimIPapriturNeImportim"]);
                        else
                        {
                            switch (IdKategoria)
                            {
                                case 1:
                                case 2:
                                case 5:
                                case 6:
                                case 7:
                                case 3:
                                case 4:
                                case 45:
                                case 135:
                                case 136:
                                case 20:
                                case 90:
                                case 92:
                                case 98:
                                case 99:
                                case 111:
                                case 112:
                                case 113:
                                case 114:
                                case 115:
                                case 150:
                                    return new MesazhGabimi(MessagesResource.Messages["msgKaGabimeNeImportim"]);
                                default:
                                    var nrRreshtashJoImportuar = rreshta - err.Rows.Count;
                                    if (nrRreshtashJoImportuar < 0)
                                        nrRreshtashJoImportuar = 0;
                                    return new MesazhGabimi(Ci.Name == "sq-AL" ? ("U importuan " + nrRreshtashJoImportuar + " rreshta dhe deshtuan " + err.Rows.Count + " rreshta! Hapni listen e gabimeve per me shume informacion!") : (nrRreshtashJoImportuar + " rows were imported and " + err.Rows.Count + " rows failed! Open error list for more information!"));
                            }
                        }
                    }
                }
                if (mesazh.Status)
                    return new MesazhSuksesi(importo ? MessagesResource.Messages["msgImportimMeSukses"] : MessagesResource.Messages["msgKontrolliKaloiMeSukses"]);
                return mesazh;
            }
            catch (Exception err)
            {
                LogManager.GetCurrentClassLogger().Error(err.Message);
                return new MesazhGabimi(importo ? MessagesResource.Messages["msgGabimNeImportim"] : MessagesResource.Messages["msgKontrolliKaloiPaSukses"]);
            }
        }

        protected Tuple<int, string> kthePozicionKodiDhePershkrimErrori(int idKategoria)
        {
            int formati = FormatImporti.IdKoka;
            int pozicionKodi = -1;
            string pershkrimErori = string.Empty;
            switch (idKategoria)
            {
                case 1:
                case 2:
                    pershkrimErori = "Nga importi i shitje/blerjeve";
                    pozicionKodi = clsFunksione.gjejVendodhjenEKodit(formati, "Nr Dokumenti");
                    break;
                case 3:
                case 4:
                    pozicionKodi = clsFunksione.gjejVendodhjenEKodit(formati, "IdDokumenti");
                    break;
                case 5:
                    pershkrimErori = "Nga importi i fleteve kontabel";
                    pozicionKodi = clsFunksione.gjejVendodhjenEKodit(formati, "Nr Dokumenti");
                    break;
                case 6:
                    pershkrimErori = "Nga importi i dokumenteve te magazines";
                    pozicionKodi = clsFunksione.gjejVendodhjenEKodit(formati, "Nr Dokumenti");
                    break;
                case 7:
                    pershkrimErori = "Nga importi i dokumenteve te shperndarje shpenzimi";
                    pozicionKodi = clsFunksione.gjejVendodhjenEKodit(formati, "Nr dokumenti");
                    break;
                case 12:
                case 13:
                case 23:
                case 31:
                case 32:
                case 67:
                case 117:
                    pozicionKodi = clsFunksione.gjejVendodhjenEKodit(formati, "Kodi");
                    break;
                case 14:
                    pershkrimErori = "Nga importi i llogarive";
                    pozicionKodi = clsFunksione.gjejVendodhjenEKodit(formati, "Numer");
                    break;
                case 17:
                    pershkrimErori = "Nga importi i cmimeve";
                    pozicionKodi = clsFunksione.gjejVendodhjenEKodit(formati, "Kod Artikulli");
                    break;
                case 18:
                    pershkrimErori = "Nga importi i zbritjeve analitike";
                    pozicionKodi = clsFunksione.gjejVendodhjenEKodit(formati, "Niveli");
                    break;
                case 20:
                    pershkrimErori = "Nga importi i veprime klient/furnitor";
                    pozicionKodi = clsFunksione.gjejVendodhjenEKodit(formati, "Kod klient/furnitori");
                    break;
                case 21:
                    pershkrimErori = "Nga importi i perdoruesve";
                    pozicionKodi = clsFunksione.gjejVendodhjenEKodit(formati, "Perdoruesi");
                    break;
                case 37:
                    pershkrimErori = "Nga importi i punonjeve";
                    pozicionKodi = clsFunksione.gjejVendodhjenEKodit(formati, "Nr Personal");
                    break;
                case 45:
                case 150:
                    pozicionKodi = clsFunksione.gjejVendodhjenEKodit(formati, "Nr Dokumenti");
                    break;
                case 71:
                    pershkrimErori = "Nga importi i detajimeve te artikullit";
                    pozicionKodi = clsFunksione.gjejVendodhjenEKodit(formati, "Kodi");
                    break;
                case 90:
                    pershkrimErori = "Nga importi i dokumenteve te amortizimit";
                    pozicionKodi = clsFunksione.gjejVendodhjenEKodit(formati, "Nr Dokumenti");
                    break;
                case 93:
                    pershkrimErori = "Nga importi i normave te amortizimit";
                    pozicionKodi = clsFunksione.gjejVendodhjenEKodit(formati, "Kod artikulli");
                    break;
                case 135:
                    pershkrimErori = "Nga importi i dokumenteve te inventarizimit te artikujve afatshkurter";
                    pozicionKodi = clsFunksione.gjejVendodhjenEKodit(formati, "Barkodi");
                    break;
                case 98:
                    pozicionKodi = clsFunksione.gjejVendodhjenEKodit(formati, "Kod punonjesi");
                    break;
                case 99:
                case 111:
                case 112:
                case 113:
                case 114:
                case 115:
                    pozicionKodi = clsFunksione.gjejVendodhjenEKodit(formati, "Kod punonjesi");
                    break;
                case 133:
                    pershkrimErori = "Nga importi i recepturave te artikujve";
                    pozicionKodi = clsFunksione.gjejVendodhjenEKodit(formati, "Kod Artikulli");
                    break;
                case 136:
                    pershkrimErori = "Nga importi i dokumenteve te inventarizimi te artikujve afatgjate";
                    pozicionKodi = clsFunksione.gjejVendodhjenEKodit(formati, "Seriali");
                    break;
                case 138:
                    pershkrimErori = "Nga importi i punesimeve";
                    pozicionKodi = clsFunksione.gjejVendodhjenEKodit(formati, "Nr Personal");
                    break;
                case 139:
                    pershkrimErori = "Nga importi i qendrave te kostos punonjes";
                    pozicionKodi = clsFunksione.gjejVendodhjenEKodit(formati, "Nr Personal");
                    break;
                case 147:
                    pershkrimErori = "Nga importi i klient me kupon";
                    pozicionKodi = clsFunksione.gjejVendodhjenEKodit(formati, "Kodi i kuponit");
                    break;
                case 148:
                    pershkrimErori = "Nga importi i klient me bazaar";
                    pozicionKodi = clsFunksione.gjejVendodhjenEKodit(formati, "MSISDN");
                    break;
                case 149:
                    pershkrimErori = "Nga importi i normave te amortizimit te rezerves";
                    pozicionKodi = clsFunksione.gjejVendodhjenEKodit(formati, "Kod artikulli");
                    break;
                case 155:
                    pershkrimErori = "Nga importi i kategorive te shpenzimit";
                    pozicionKodi = clsFunksione.gjejVendodhjenEKodit(formati, "Kodi");
                    break;
                case 157:
                    pershkrimErori = "Nga importi i qyteteve";
                    pozicionKodi = clsFunksione.gjejVendodhjenEKodit(formati, "Kodi");
                    break;
                case 163:
                    pershkrimErori = "Nga importi i grupeve te klient/furnitoreve";
                    pozicionKodi = clsFunksione.gjejVendodhjenEKodit(formati, "Kodi");
                    break;
                case 164:
                    pershkrimErori = "Nga importi i kodbareve";
                    pozicionKodi = clsFunksione.gjejVendodhjenEKodit(formati, "Kod artikulli");
                    break;
                case 170:
                case 172:
                    pozicionKodi = clsFunksione.gjejVendodhjenEKodit(formati, "Id");
                    break;
                case 175:
                    pershkrimErori = "Nga importi i dokumentave te Rialokim Buxhetit";
                    pozicionKodi = clsFunksione.gjejVendodhjenEKodit(formati, "IdKokaBuxheti");
                    break;
                case 177:
                    pershkrimErori = "Nga importi i dokumentave te Perfitim Buxhetit";
                    pozicionKodi = clsFunksione.gjejVendodhjenEKodit(formati, "IdKokaBuxheti");
                    break;
                case 179:
                    pershkrimErori = "Nga importi i dokumentave te Planifikim Ekzekutim Buxheti";
                    pozicionKodi = clsFunksione.gjejVendodhjenEKodit(formati, "IdKokaBuxheti");
                    break;
                case 181:
                    pershkrimErori = "Nga importi i dokumentave te Ekzekutim Buxhetit";
                    pozicionKodi = clsFunksione.gjejVendodhjenEKodit(formati, "IdKokaBuxheti");
                    break;
            }
            return new Tuple<int, string>(pozicionKodi, pershkrimErori);
        }

        protected Tuple<clsMesazh, string> KontrolloOseImportoDokument(int idKategoria, colTrupiFormatImporti col, DataTable dt, DataTable err, DataTable rreshtaOk, ref DataTable rreshtajoOk, ref DataTable deadLocked, int pozicionKodi, bool importo, bool mbishkruajVleratMeparshme, clsMesazh mesazh)
        {
            string pershkrimErrori = string.Empty;
            switch (idKategoria)
            {
                case 1:
                case 2:
                    kontrolloDokShitjeBlerje(dt, err, ref rreshtajoOk, importo, pozicionKodi, col, ref deadLocked);
                    pershkrimErrori = "Nga importi i shitje/blerjeve";
                    break;
                case 3:
                case 4:
                    kontrolloOseImportoDokumentaSipasKategorise(dt, err, ref rreshtajoOk, importo, pozicionKodi, col, ref deadLocked);
                    pershkrimErrori = "Nga importi i dokumenteve te arkes/bankes";
                    break;
                case 5:
                    kontrolloOseImportoDokumentaSipasKategorise(dt, err, ref rreshtajoOk, importo, pozicionKodi, col, ref deadLocked);
                    pershkrimErrori = "Nga importi i fleteve kontabel";
                    break;
                case 6:
                    kontrolloOseImportoDokumentaSipasKategorise(dt, err, ref rreshtajoOk, importo, pozicionKodi, col, ref deadLocked);
                    pershkrimErrori = "Nga importi i dokumenteve te magazines";
                    break;
                case 7:
                    kontrolloOseImportoDokumentaSipasKategorise(dt, err, ref rreshtajoOk, importo, pozicionKodi, col, ref deadLocked);
                    pershkrimErrori = "Nga importi i dokumenteve te shperndarje shpenzimi";
                    break;
                case 12:
                    mesazh = KontrolloOseImportoObjektePerImport(rreshtaOk, err, ref rreshtajoOk, importo, pozicionKodi, IdKategoria, col);
                    pershkrimErrori = "Nga importi i klient/furnitorit";
                    break;
                case 13:
                    mesazh = KontrolloOseImportoObjektePerImport(rreshtaOk, err, ref rreshtajoOk, importo, pozicionKodi, IdKategoria, col);
                    pershkrimErrori = "Nga importi i artikujve";
                    break;
                case 14:
                    KontrolloOseImportoObjektePerImport(rreshtaOk, err, ref rreshtajoOk, importo, pozicionKodi, IdKategoria, col);
                    pershkrimErrori = "Nga importi i llogarive";
                    break;
                case 17:
                    kontrolloCmime(dt, err, ref rreshtajoOk, importo, pozicionKodi, col);
                    pershkrimErrori = "Nga importi i artikujve";
                    break;
                case 18:
                    KontrolloOseImportoObjektePerImport(rreshtaOk, err, ref rreshtajoOk, importo, pozicionKodi, IdKategoria, col);
                    pershkrimErrori = "Nga importi i zbritjeve analitike";
                    break;
                case 20:
                    kontrolloVeprimeKF(dt, err, ref rreshtajoOk, importo, pozicionKodi, col);
                    pershkrimErrori = "Nga importi i veprime klient/furnitor";
                    break;
                case 21:
                    kontrolloPerdorues(rreshtaOk, err, ref rreshtajoOk, importo, pozicionKodi, col, VjenNgaImportSQL);
                    pershkrimErrori = "Nga importi perdoruesve";
                    break;
                case 23:
                    kontrolloMagazina(rreshtaOk, err, ref rreshtajoOk, importo, pozicionKodi, col);
                    pershkrimErrori = "Nga importi i magazinave";
                    break;
                case 31:
                    kontrolloPikeShitjeFurnizimi(dt, err, ref rreshtajoOk, importo, pozicionKodi, col);
                    pershkrimErrori = "Nga importi i pike shitje/furnizimi";
                    break;
                case 32:
                    kontrolloDegeAdministrative(dt, err, ref rreshtajoOk, importo, pozicionKodi, col);
                    pershkrimErrori = "Nga importi i deges administrative";
                    break;
                case 37:
                    KontrolloOseImportoObjektePerImport(rreshtaOk, err, ref rreshtajoOk, importo, pozicionKodi, IdKategoria, col);
                    pershkrimErrori = "Nga importi i punonjesve";
                    break;
                case 45:
                    kontrolloOseImportoDokumentaSipasKategorise(dt, err, ref rreshtajoOk, importo, pozicionKodi, col, ref deadLocked);
                    pershkrimErrori = "Nga importi i dokumenteve te ekzekutim prodhimi";
                    break;
                case 67:
                    KontrolloOseImportoObjektePerImport(rreshtaOk, err, ref rreshtajoOk, importo, pozicionKodi, IdKategoria, col);
                    pershkrimErrori = "Nga importi i grupeve te artikujve";
                    break;
                case 71:
                    kontrolloDetajimeArtikulli(dt, err, ref rreshtajoOk, importo, pozicionKodi, col);
                    pershkrimErrori = "Nga importi i detajimeve te artikullit";
                    break;
                case 90:
                case 150:
                    kontrolloDokAmortizimiFillestar(dt, err, ref rreshtajoOk, importo, pozicionKodi, col);
                    pershkrimErrori = "Nga importi i dokumenteve te amortizimit";
                    break;
                case 93:
                    KontrolloOseImportoObjektePerImport(rreshtaOk, err, ref rreshtajoOk, importo, pozicionKodi, IdKategoria, col);
                    pershkrimErrori = "Nga importi i normave te amortizimit";
                    break;
                case 98:
                    kontrolloListOrari(dt, err, ref rreshtajoOk, importo, pozicionKodi, col);
                    pershkrimErrori = "Nga importi i list orareve";
                    break;
                case 99:
                    mesazh = KontrolloOseImportoObjekteListPagese(dt, err, ref rreshtajoOk, importo, pozicionKodi, mbishkruajVleratMeparshme, col);
                    pershkrimErrori = "Nga importi i komponenteve formule";
                    break;
                case 111:
                    mesazh = KontrolloOseImportoObjekteListPagese(dt, err, ref rreshtajoOk, importo, pozicionKodi, mbishkruajVleratMeparshme, col);
                    pershkrimErrori = "Nga importi i komponenteve nr";
                    break;
                case 112:
                    kontrolloDiteLeje(dt, err, ref rreshtajoOk, importo, pozicionKodi, col);
                    pershkrimErrori = "Nga importi i diteve te lejes";
                    break;
                case 113:
                    kontrolloKontrolliMjekesor(dt, err, ref rreshtajoOk, importo, pozicionKodi, col);
                    pershkrimErrori = "Nga importi i kontrollit mjekesor";
                    break;
                case 114:
                    kontrolloPagaDheShtesa(dt, err, ref rreshtajoOk, importo, pozicionKodi, col);
                    pershkrimErrori = "Nga importi i paga dhe shtesa";
                    break;
                case 115:
                    kontrolloKomponenteListpagesePunonjesi(dt, err, ref rreshtajoOk, importo, pozicionKodi, col);
                    pershkrimErrori = "Nga importi i komponente listpagese per punonjesit";
                    break;
                case 117:
                    pershkrimErrori = "Nga importi i kartave te klienteve";
                    KontrolloOseImportoObjektePerImport(rreshtaOk, err, ref rreshtajoOk, importo, pozicionKodi, IdKategoria, col);
                    break;
                case 133:
                    kontrolloRecepturaArtikulli(dt, err, ref rreshtajoOk, importo, pozicionKodi, col);
                    pershkrimErrori = "Nga importi i recepturave te artikujve";
                    break;
                case 135:
                case 136:
                    kontrolloOseImportoDokumentaSipasKategorise(dt, err, ref rreshtajoOk, importo, pozicionKodi, col, ref deadLocked);
                    pershkrimErrori = "Nga importi i dokumenteve te inventarizimit";
                    break;
                case 138:
                    KontrolloOseImportoObjektePerImport(rreshtaOk, err, ref rreshtajoOk, importo, pozicionKodi, IdKategoria, col);
                    pershkrimErrori = "Nga importi i Punesimeve";
                    break;
                case 139:
                    KontrolloOseImportoObjektePerImport(rreshtaOk, err, ref rreshtajoOk, importo, pozicionKodi, IdKategoria, col);
                    pershkrimErrori = "Nga importi i qendrave te kostos te punonjesit";
                    break;
                case 147:
                    kontrolloKlientMeKupon(dt, err, ref rreshtajoOk, importo, pozicionKodi, col);
                    pershkrimErrori = "Nga importi i klienteve me kupn";
                    break;
                case 148:
                    kontrolloKlientPerBazaar(dt, err, ref rreshtajoOk, importo, pozicionKodi, col);
                    pershkrimErrori = "Nga importi i klient me bazaar";
                    break;
                case 149:
                    KontrolloOseImportoObjektePerImport(rreshtaOk, err, ref rreshtajoOk, importo, pozicionKodi, IdKategoria, col);
                    pershkrimErrori = "Nga importi i normave te amortizimit te rezerves";
                    break;
                case 155:
                    KontrolloKategoriShpenzimi(rreshtaOk, err, ref rreshtajoOk, importo, pozicionKodi, col);
                    pershkrimErrori = "Nga importi i kategorive te shpenzimit";
                    break;
                case 157:
                    KontrolloOseImportoObjektePerImport(rreshtaOk, err, ref rreshtajoOk, importo, pozicionKodi, IdKategoria, col);
                    pershkrimErrori = "Nga importi i qyteteve";
                    break;
                case 163:
                    KontrolloOseImportoObjektePerImport(rreshtaOk, err, ref rreshtajoOk, importo, pozicionKodi, IdKategoria, col);
                    pershkrimErrori = "Nga importi i grupeve te klient/furnitoreve";
                    break;
                case 164:
                    KontrolloOseImportoObjektePerImport(rreshtaOk, err, ref rreshtajoOk, importo, pozicionKodi, IdKategoria, col);
                    pershkrimErrori = "Nga importi i kodbareve";
                    break;
                case 170:
                    kontrolloOseImportoDokumentaSipasKategorise(dt, err, ref rreshtajoOk, importo, pozicionKodi, col, ref deadLocked);
                    pershkrimErrori = "Nga importi i dokumentave te Miratim Buxhetit";
                    break;
                case 172:
                    kontrolloOseImportoDokumentaSipasKategorise(dt, err, ref rreshtajoOk, importo, pozicionKodi, col, ref deadLocked);
                    pershkrimErrori = "Nga importi i dokumenteve te Perfitim Buxhetit";
                    break;
                case 175:
                    kontrolloOseImportoDokumentaSipasKategorise(dt, err, ref rreshtajoOk, importo, pozicionKodi, col, ref deadLocked);
                    pershkrimErrori = "Nga importi i dokumentave te Rialokim Buxhetit";
                    break;
                case 177:
                    kontrolloOseImportoDokumentaSipasKategorise(dt, err, ref rreshtajoOk, importo, pozicionKodi, col, ref deadLocked);
                    pershkrimErrori = "Nga importi i dokumenteve te Perfitim Buxhetit";
                    break;
                case 179:
                    kontrolloOseImportoDokumentaSipasKategorise(dt, err, ref rreshtajoOk, importo, pozicionKodi, col, ref deadLocked);
                    pershkrimErrori = "Nga importi i dokumenteve te Planifikim Ekzekutim Buxheti";
                    break;
                case 181:
                    kontrolloOseImportoDokumentaSipasKategorise(dt, err, ref rreshtajoOk, importo, pozicionKodi, col, ref deadLocked);
                    pershkrimErrori = "Nga importi i dokumenteve te Ekzekutim Buxhetit";
                    break;
            }
            return new Tuple<clsMesazh, string>(mesazh, pershkrimErrori);
        }

        public clsMesazh kontrolloDokShitjeBlerje(DataTable dt, DataTable gabime, ref DataTable tePaImportuara, bool importo, int pozicionkodi, colTrupiFormatImporti col, ref DataTable deadLocked)
        {
            var nderm = new clsNdermarrje(IdNdermarrja);
            var primaryKey = "";
            var ndermarrjeKey = "";

            foreach (var trupi in col)
            {
                if (trupi.FusheKokeApoTrupi == 3)
                    primaryKey = trupi.EmerImporti;
                if (trupi.KodKontrolli == "Kod Ndermarrje")
                    ndermarrjeKey = trupi.EmerImporti;
                clsFunksione.vendosVlereDefaultTeDataTable(trupi, dt);
            }

            var nenkategoria = col.ktheEmerImportiSipasKodKontrolli("Nenkategoria");

            var mesazh = clsFunksione.importDokumenteshShitjeBlerje(dt, IdNdermarrja, IdNdermarrjeVit, IdPerdoruesi, ref gabime, ref tePaImportuara, nenkategoria, ndermarrjeKey, primaryKey, col, IdKategoria, Permbledhese, Rm, Ci, IdGjuha, TabKoka, TabTrupi, nderm, importo, VjenNgaImportSQL, false, ref deadLocked);
            if (!importo)
                return mesazh;

            Status = "import";
            return mesazh;
        }

        public clsMesazh KontrolloOseImportoObjektePerImport(DataTable dt, DataTable gabime, ref DataTable tePaImportuara, bool importo, int pozicionkodi, int kategoria, colTrupiFormatImporti col)
        {
            clsMesazh mesazh;
            switch (kategoria)
            {
                case 12:
                    mesazh = clsImportoKlientFurnitore.importoKlientFurnitor(dt, ref gabime, ref tePaImportuara, importo, pozicionkodi, Rm, Ci, IdPerdoruesi, IdNdermarrja, IdNdermarrjeVit, IdGjuha, col, VjenNgaImportSQL, TabKoka);
                    break;
                case 13:
                    mesazh = clsImportoArtikuj.importoArtikuj(dt, ref gabime, ref tePaImportuara, importo, pozicionkodi, Rm, Ci, IdPerdoruesi, IdNdermarrja, IdNdermarrjeVit, IdGjuha, col, VjenNgaImportSQL, TabKoka);
                    break;
                case 37:
                    mesazh = clsFunksione.importoPunonjes(dt, ref gabime, ref tePaImportuara, importo, pozicionkodi, Rm, Ci, IdPerdoruesi, IdNdermarrja, IdNdermarrjeVit, IdGjuha, col, VjenNgaImportSQL, TabKoka);
                    break;
                case 138:
                    mesazh = clsFunksione.importoPunesim(dt, ref gabime, ref tePaImportuara, importo, pozicionkodi, Rm, Ci, IdPerdoruesi, IdNdermarrja, IdNdermarrjeVit, IdGjuha, col, VjenNgaImportSQL, TabKoka);
                    break;
                case 139:
                    mesazh = clsFunksione.importoQendraKostoPunonjes(dt, ref gabime, ref tePaImportuara, importo, pozicionkodi, Rm, Ci, IdPerdoruesi, IdNdermarrja, IdNdermarrjeVit, IdGjuha, col, VjenNgaImportSQL, TabKoka);
                    break;
                case 14:
                    mesazh = clsFunksione.importoLlogari(dt, ref gabime, ref tePaImportuara, importo, pozicionkodi, Rm, Ci, IdPerdoruesi, IdNdermarrja, IdNdermarrjeVit, IdGjuha, col, VjenNgaImportSQL, TabKoka);
                    break;
                case 67:
                    mesazh = clsFunksione.importoGrupeArtikujsh(dt, ref gabime, ref tePaImportuara, importo, pozicionkodi, Rm, Ci, IdPerdoruesi, IdNdermarrja, IdNdermarrjeVit, IdGjuha, col, VjenNgaImportSQL, TabKoka);
                    break;
                case 18:
                    mesazh = clsFunksione.importoZbritjeAnalitike(dt, ref gabime, ref tePaImportuara, importo, pozicionkodi, Rm, Ci, IdPerdoruesi, IdNdermarrja, IdNdermarrjeVit, IdGjuha, col, VjenNgaImportSQL, TabKoka);
                    break;
                case 117:
                    mesazh = clsFunksione.importoKartaKlienti(dt, ref gabime, ref tePaImportuara, importo, pozicionkodi, Rm, Ci, IdPerdoruesi, IdNdermarrja, IdNdermarrjeVit, IdGjuha, col, VjenNgaImportSQL, TabKoka);
                    break;
                case 157:
                    mesazh = clsFunksione.importoQytete(dt, ref gabime, ref tePaImportuara, importo, pozicionkodi, Rm, Ci, IdPerdoruesi, IdNdermarrja, IdNdermarrjeVit, IdGjuha, col, VjenNgaImportSQL, TabKoka);
                    break;
                case 93:
                case 149:
                    mesazh = clsFunksione.importoNorma(dt, ref gabime, ref tePaImportuara, importo, pozicionkodi, Rm, Ci, IdPerdoruesi, IdNdermarrja, IdNdermarrjeVit, IdGjuha, col, IdKategoria);
                    break;
                case 164:
                    mesazh = clsFunksione.importoKodbareArtikulli(dt, ref gabime, ref tePaImportuara, importo, pozicionkodi, Rm, Ci, IdPerdoruesi, IdNdermarrja, IdNdermarrjeVit, IdGjuha, col, VjenNgaImportSQL, TabKoka);
                    break;
                case 163:
                    mesazh = clsFunksione.importoGrupeKlientFurnitore(dt, ref gabime, ref tePaImportuara, importo, pozicionkodi, Rm, Ci, IdPerdoruesi, IdNdermarrja, IdNdermarrjeVit, IdGjuha, col, VjenNgaImportSQL, TabKoka);
                    break;
                default:
                    mesazh = new clsMesazh(false, "Nuk eshte implementuar kjo kategori per import!");
                    break;
            }

            if (!importo) return mesazh;

            Status = "import";
            return mesazh;
        }

        private clsMesazh kontrolloOseImportoDokumentaSipasKategorise(DataTable dt, DataTable gabime, ref DataTable tePaImportuara, bool importo, int pozicionkodi, colTrupiFormatImporti col, ref DataTable deadLocked)
        {
            clsNdermarrje nderm = new clsNdermarrje(IdNdermarrja);
            string primaryKey = "", primaryKeyProdukt = "";
            string ndermarrjeKey = "";
            foreach (clsTrupiFormatImporti trupi in col)
            {
                if (trupi.FusheKokeApoTrupi == 3)
                    primaryKey = trupi.EmerImporti;
                if (trupi.FusheKokeApoTrupi == 5)
                    primaryKeyProdukt = trupi.EmerImporti;
                if (trupi.KodKontrolli == "Kod Ndermarrje")
                    ndermarrjeKey = trupi.EmerImporti;
                clsFunksione.vendosVlereDefaultTeDataTable(trupi, dt);
            }
            string nenkategoria = col.ktheEmerImportiSipasKodKontrolli("Nenkategoria");
            clsMesazh mesazh = new MesazhSuksesi();
            switch (IdKategoria)
            {
                case 3:
                case 4:
                    mesazh = clsFunksione.importDokumenteshArkaBanka(dt, IdNdermarrja, IdPerdoruesi, ref gabime, ref tePaImportuara, nenkategoria, ndermarrjeKey, primaryKey, col, IdKategoria, Rm, Ci, IdGjuha, TabKoka, TabTrupi, nderm, importo, VjenNgaImportSQL, false, IdNdermarrjeVit);
                    break;
                case 5:
                    clsFunksione.ImportFleteKontabel(dt, IdNdermarrja, IdPerdoruesi, ref gabime, ref tePaImportuara, nenkategoria, ndermarrjeKey, primaryKey, col, IdKategoria, IdGjuha, TabKoka, TabTrupi, nderm, importo, VjenNgaImportSQL, false, IdNdermarrjeVit);
                    break;
                case 6:
                    mesazh = clsFunksione.importDokumenteMagazine(dt, IdNdermarrja, IdPerdoruesi, ref gabime, ref tePaImportuara, nenkategoria, ndermarrjeKey, primaryKey, col, IdKategoria, Rm, Ci, IdGjuha, TabKoka, TabTrupi, nderm, importo, VjenNgaImportSQL, false, IdNdermarrjeVit, ref deadLocked);
                    break;
                case 7:
                    mesazh = clsFunksione.importDokumenteShperndarjeShpenz(dt, IdNdermarrja, IdPerdoruesi, ref gabime, ref tePaImportuara, nenkategoria, ndermarrjeKey, primaryKey, col, IdKategoria, Rm, Ci, IdGjuha, TabKoka, TabTrupi, nderm, importo, VjenNgaImportSQL, false, IdNdermarrjeVit, ref deadLocked);
                    break;
                case 45:
                    mesazh = clsFunksione.importDokumenteshEkzekutimProdhimi(dt, IdNdermarrja, IdPerdoruesi, ref gabime, ref tePaImportuara, primaryKey, ndermarrjeKey, primaryKeyProdukt, col, IdKategoria, Rm, Ci, IdGjuha, TabKoka, TabTrupi, TabReceptura, importo, VjenNgaImportSQL, false, IdNdermarrjeVit);
                    break;
                case 135:
                case 136:
                    mesazh = clsFunksione.importDokumenteshInventarizimi(dt, IdNdermarrja, IdPerdoruesi, ref gabime, ref tePaImportuara, nenkategoria, ndermarrjeKey, primaryKey, col, IdKategoria, Rm, Ci, IdGjuha, TabKoka, TabTrupi, IdNdermarrjeVit, pozicionkodi, importo, VjenNgaImportSQL, false);
                    break;
                case 170:
                case 172:
                case 175:
                case 177:
                case 179:
                case 181:
                    mesazh = clsFunksione.importoDokumentBuxheti(dt, IdNdermarrja, IdPerdoruesi, ref gabime, ref tePaImportuara, ndermarrjeKey, primaryKey, col, IdKategoria, Rm, Ci, IdGjuha, TabKoka, TabTrupi, importo, VjenNgaImportSQL, false, IdNdermarrjeVit);
                    break;
            }
            if (!importo)
                return mesazh;

            Status = "import";
            return mesazh;

        }


        private clsMesazh kontrolloCmime(DataTable dt, DataTable gabime, ref DataTable tePaImportuara, bool importo, int pozicionkodi, colTrupiFormatImporti col)
        {
            try
            {
                var colNiveleCmimi = new colNiveleCmimesh();
                colNiveleCmimi.mbushGjitheNiveleCmimeshSipasNdermarjes(IdNdermarrja);

                int idKonfigCSH = clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod("CSH", IdNdermarrja);
                int idKonfigCB = clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod("CB", IdNdermarrja);
                string error = "";
                int i = 1;
                foreach (DataRow dr in dt.Rows)
                {
                    var cm = new clsCmimArtikulli();
                    string niveli = "", artikulli = "", monedha = "", njesia1 = "", njesia2 = "", pershkrimartikulli = "", tvsh = "";
                    decimal cmimi1 = 0, cmimi2 = 0, sasimin = 0, sasimax = 0, cmimitvsh = 0, cmimitvsh2 = 0;
                    var dtfillimi = DateTime.Today;
                    var dtMbarimi = DateTime.MaxValue.AddSeconds(-1);
                    var kohefillimi = new DateTime(DateTime.Today.Year, 1, 1, 0, 0, 0);
                    var kohembarimi = new DateTime(9999, 12, 31, 23, 59, 59);

                    foreach (var trup in col)
                    {
                        error = "";
                        switch (trup.KodKontrolli)
                        {
                            case "Niveli":
                                niveli = clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                            case "Niveli Tvsh":
                                tvsh = clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                            case "Kod Artikulli":
                                artikulli = clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                            case "Emer Artikulli":
                                pershkrimartikulli = clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                            case "Monedha":
                                monedha = clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                            case "Njesia 1":
                                njesia1 = clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                            case "Njesia 2":
                                njesia2 = clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                            case "Cmimi":
                                cmimi1 = clsFunksione.vendosDecimal(trup, dr, out error);
                                break;
                            case "Cmimi 2":
                                cmimi2 = clsFunksione.vendosDecimal(trup, dr, out error);
                                break;
                            case "Cmimi me Tvsh":
                                cmimitvsh = clsFunksione.vendosDecimal(trup, dr, out error);
                                break;
                            case "Cmimi 2 me Tvsh":
                                cmimitvsh2 = clsFunksione.vendosDecimal(trup, dr, out error);
                                break;
                            case "Date Fillimi":
                                dtfillimi = clsFunksione.vendosDate(trup, dr, out error);
                                break;
                            case "Date Mbarimi":
                                dtMbarimi = clsFunksione.vendosDate(trup, dr, out error);
                                break;
                            case "Kohe Fillimi":
                                kohefillimi = (clsFunksione.vendosOre(trup, dr, out error) == new DateTime()) ? kohefillimi : clsFunksione.vendosOre(trup, dr, out error);
                                break;
                            case "Kohe Mbarimi":
                                kohembarimi = (clsFunksione.vendosOre(trup, dr, out error) == new DateTime()) ? kohembarimi : clsFunksione.vendosOre(trup, dr, out error);
                                break;
                            case "Sasi Min":
                                sasimin = clsFunksione.vendosDecimal(trup, dr, out error);
                                break;
                            case "Sasi Max":
                                sasimax = clsFunksione.vendosDecimal(trup, dr, out error);
                                break;
                        }
                        if (error == "") continue;
                        object[] arr = { dr[pozicionkodi], error, i };
                        gabime.Rows.Add(arr);
                        if (importo)
                        {
                            tePaImportuara.ImportRow(dr);
                            gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                        }
                        break;
                    }
                    if (error != "")
                        continue;
                    try
                    {
                        cm = cm.krijoCmimPerImport(niveli, artikulli.RemoveSpaces(), monedha, njesia1, njesia2, cmimi1, cmimi2, dtfillimi, dtMbarimi, sasimin, sasimax, IdPerdoruesi, IdNdermarrja, idKonfigCSH, idKonfigCB, kohefillimi, kohembarimi, tvsh, cmimitvsh, cmimitvsh2, Rm, Ci, colNiveleCmimi);
                        if (importo)
                        {
                            colCmimeArtikujsh colcm = new colCmimeArtikujsh { cm };

                            //shtuar per gjenerimin e cmimeve retail 
                            DbCore.DbInventari.colCmimeArtikujsh colCmimArtikulli = new DbCore.DbInventari.colCmimeArtikujsh();
                            colCmimArtikulli.mbushCmimArtikulliSipasArtikullitMeKostoMeAutorizime(cm.IdArtikulli, IdNdermarrja, IdPerdoruesi, false);
                            colcm.ShtoCmimeRetailNeKoleksion(colCmimArtikulli);

                            var mesazhinv = colCmimeArtikujsh.RuajRreshtaTeModifikuar(colcm);
                            if (!mesazhinv.Status)
                            {
                                object[] arr = { dr[pozicionkodi], mesazhinv.PershkrimMesazhi, i };
                                gabime.Rows.Add(arr);
                                tePaImportuara.ImportRow(dr);
                                gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LogManager.GetCurrentClassLogger().Error(ex.Message);
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
                }
                if (!importo)
                    return new clsMesazh(true);

                Status = "import";
                return new clsMesazh(true);
            }
            catch (Exception err)
            {
                LogManager.GetCurrentClassLogger().Error(err.Message);
                return new clsMesazh(false);
            }
        }

        #region Veprime Klient Furnitor
        private clsMesazh kontrolloVeprimeKF(DataTable dt, DataTable gabime, ref DataTable tePaImportuara, bool importo, int pozicionkodi, colTrupiFormatImporti col)
        {
            try
            {
                string primaryKey = String.Empty;
                string ndermarrjeKey = String.Empty;
                foreach (clsTrupiFormatImporti trupi in col)
                {
                    if (trupi.FusheKokeApoTrupi == 3)
                        primaryKey = trupi.EmerImporti;
                    if (trupi.KodKontrolli == "Kod Ndermarrje")
                        ndermarrjeKey = trupi.EmerImporti;
                    clsFunksione.vendosVlereDefaultTeDataTable(trupi, dt);
                }

                string fushatEGrupimit = "";
                DataTable dataGrupime;

                //grupojme dokumentet qe vijne si datatable sipas fushave te kokes dhe i ruajme ato tek tabela dataGrupime
                string fushaGrupimi = "";
                foreach (clsTrupiFormatImporti trupi in col)
                {
                    if (trupi.Visible && trupi.Shfaq && (trupi.FusheKokeApoTrupi == 1 || trupi.FusheKokeApoTrupi == 3))
                    {
                        //shtojme te stringu fushat e percaktuara te formatit qe jane fusha te kokes se dokumentit, per kete perjashtojme fushat e trupit     
                        fushaGrupimi += trupi.EmerImporti + ";";
                    }
                }
                fushatEGrupimit = fushaGrupimi.Substring(0, fushaGrupimi.LastIndexOf(';'));//heqim pikepresjen e fundit
                string[] fushat = fushatEGrupimit.Split(';');
                dataGrupime = dt.DefaultView.ToTable(true, fushat);

                int indexRreshtImporti = 1;

                clsMonedha monedhaNdermarrjes = new clsMonedha();
                monedhaNdermarrjes.mbushMonedhenENdermarrjes(IdNdermarrja);
                using(DbData dbData = new DbData())
                {
                    foreach (DataRow drDok in dataGrupime.Rows)
                    {
                        DataTable dokumentKokTrup = null;
                        try
                        {

                            //krijojme nje tabele te re, ku vendosim dokumentin 
                            string selekti = "";

                            for (int j = 0; j < fushat.Count(); j++)
                            {
                                if (!String.IsNullOrEmpty(drDok[fushat[j]].ToString()))
                                {
                                    string fusha = drDok[fushat[j]].ToString().Replace("'", "''");
                                    selekti += "[" + fushat[j] + "] = '" + fusha + "' AND ";
                                }
                            }
                            selekti += "1 = 1";
                            dokumentKokTrup = dt.Select(selekti).GetDataTable(dt);
                            clsMesazh mesazh = krijoDokVeprimeKF(dbData, dokumentKokTrup, col, pozicionkodi, gabime, tePaImportuara, importo, false, ref indexRreshtImporti, "", "", monedhaNdermarrjes);

                        }
                        catch (Exception ex)
                        {
                            LogManager.GetCurrentClassLogger().Error(ex.Message);
                            //write something
                            object[] arr = { "X", MessagesResource.Messages["msgGagimIPanohur"], indexRreshtImporti };
                            gabime.Rows.Add(arr);
                        }
                    }
                }                
                indexRreshtImporti = 1;
                if (importo)
                    Status = "import";
            }
            catch (Exception ex)
            {
                LogManager.GetCurrentClassLogger().Error(ex.Message);
                return new clsMesazh(false);
            }
            return new clsMesazh(true);
        }

        private clsMesazh krijoDokVeprimeKF(DbData dbData, DataTable dokTable, colTrupiFormatImporti col, int pozicionkodi, DataTable gabime, DataTable tePaImportuara, bool importo, bool vjenNgaImportSQL, ref int indexRreshtImporti, string primaryKey, string ndermarrjeKey, clsMonedha monedhaNdermarrjes)
        {
            if (dokTable == null)
                return new clsMesazh(false);
            string error = "";
            string nrDokumentiEmerImport = "";
            string dtDokumentiEmerImport = "";
            string kodKlientEmerImport = "";
            string llojDokumentiEmerImport = "";

            try
            {
                nrDokumentiEmerImport = col.filtroFormatImportiSipasFushes("Nr dokumenti").EmerImporti;
                dtDokumentiEmerImport = col.filtroFormatImportiSipasFushes("Date dokumenti").EmerImporti;
                kodKlientEmerImport = col.filtroFormatImportiSipasFushes("Kod klient/furnitori").EmerImporti;
                llojDokumentiEmerImport = col.filtroFormatImportiSipasFushes("Lloji").EmerImporti;


                clsMesazh mesazh = new clsMesazh();

                string nenkategoria = String.Empty, llojDokumenti = String.Empty, nrDok = String.Empty, pershkrimi = String.Empty;
                DateTime dtDok = new DateTime();

                error = "";

                #region Fushat e kokes
                foreach (clsTrupiFormatImporti trup in col)
                {
                    error = "";
                    switch (trup.KodKontrolli)
                    {
                        case "Nenkategoria":
                            nenkategoria = clsFunksione.vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                        case "Lloji":
                            llojDokumenti = clsFunksione.vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                        case "Nr dokumenti":
                            nrDok = clsFunksione.vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                        case "Pershkrimi":
                            pershkrimi = clsFunksione.vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                        case "Date dokumenti":
                            dtDok = clsFunksione.vendosDate(trup, dokTable.Rows[0], out error);
                            if (dtDok == DateTime.MinValue)
                                dtDok = DateTime.Today;
                            break;
                    }
                    if (error != "")
                    {
                        try
                        {
                            DateTime datedok = new DateTime();
                            bool dateVlefshme = DateTime.TryParse(dokTable.Rows[0][dtDokumentiEmerImport].ToString(), out datedok);
                            if (dateVlefshme)
                            {
                                object[] arr = { dokTable.Rows[0][nrDokumentiEmerImport], MessagesResource.Messages["labelDateDokumenti"] + " " + datedok.Day + "/" + datedok.Month + "/" + datedok.Year + " :" + trup.EmerImporti + " duhet te jete numer!", indexRreshtImporti };
                                gabime.Rows.Add(arr);
                            }
                            else
                            {
                                object[] arr = { dokTable.Rows[0][nrDokumentiEmerImport].ToString(), error, indexRreshtImporti };
                                gabime.Rows.Add(arr);
                            }
                        }
                        catch (Exception ex)
                        {
                            LogManager.GetCurrentClassLogger().Error(ex.Message);
                            object[] arr = { dokTable.Rows[0][nrDokumentiEmerImport].ToString(), error, indexRreshtImporti };
                            gabime.Rows.Add(arr);
                        }

                        if (importo)
                        {
                            string id = "Id";
                            foreach (DataRow dr in dokTable.Rows)
                            {
                                if (tePaImportuara.Select(String.Format("{0} = '{1}'", id, dr[id])).Count() == 0)
                                    tePaImportuara.ImportRow(dr);
                            }
                            gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                        }
                    }
                }

                #endregion

                clsVeprimeKFKoka koka = new clsVeprimeKFKoka();
                colVeprimeKFTrupi colTrupi = new colVeprimeKFTrupi();
                clsDatabaseShare dbShare = new clsDatabaseShare(dbData);
                clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin(dbData);

                clsKonfigurimAmbjenti konfigAmbjenti = new clsKonfigurimAmbjenti(llojDokumenti, IdNdermarrja, new clsDatabaseShare(dbData));

                if (konfigAmbjenti.IdKonfigAmbjente == 0)
                    throw new Exception(MessagesResource.Messages["labelFilterAvancuarLlojDok"] + " " + llojDokumenti + " " + MessagesResource.Messages["msgNukEkziston"]);

                string alternativa = clsAlternativaKushti.getAlternativa(konfigAmbjenti.IdKonfigAmbjente, "LLK", dbShare);
                int llojKursi = 1;
                if (alternativa != "")
                {
                    llojKursi = int.Parse(alternativa.Substring(alternativa.Length - 1));
                }
                bool kushtNDKF = clsAlternativaKushti.getAlternativa(konfigAmbjenti.IdKonfigAmbjente, "NDKF", dbShare) == "Po";

                colTrupi = krijoTrupVeprimeKF(dbData, dokTable, nrDok, col, pozicionkodi, gabime, tePaImportuara, importo, nrDokumentiEmerImport, dtDokumentiEmerImport, kodKlientEmerImport, llojDokumentiEmerImport, dtDok, ref indexRreshtImporti, llojKursi, monedhaNdermarrjes, kushtNDKF);
                if (colTrupi.Count == 0)
                    throw new Exception("Trupi i dokumentit nuk u krijua sepse ka te dhena te pasakta!");
                clsPeriudhaKontabel per = new clsPeriudhaKontabel(dtDok, IdNdermarrja, dbAdmin);

                DateTime dtRegjistrimi = DateTime.Today;

                int idStatusDok = 0;
                bool meKontabilizim = false;


                if (clsAlternativaKushti.getAlternativa(konfigAmbjenti.IdKonfigAmbjente, "SDI", dbShare) == "Draft")
                {
                    idStatusDok = 0;
                    meKontabilizim = false;
                }
                else
                {
                    idStatusDok = 1;
                    if (clsAlternativaKushti.getAlternativa(konfigAmbjenti.IdKonfigAmbjente, "GJK", dbShare) != "Jo")
                        meKontabilizim = true;
                }

                clsKonfigurimAmbjenti konfigdokLidhes = new clsKonfigurimAmbjenti(konfigAmbjenti.IdKonfigurimi, dbShare);

                string alternativaMF = clsAlternativaKushti.getAlternativa(konfigAmbjenti.IdKonfigAmbjente, "MF", dbShare);
                bool meFatura = false;

                if (alternativaMF == "Po")
                    meFatura = true;

                int llojVeprimi = 1;
                if (meFatura)
                    llojVeprimi = 2;
                else if (kushtNDKF)
                    llojVeprimi = 3;

                clsNdermarrjeViti vitNdermarrje = new clsNdermarrjeViti(IdViti, IdNdermarrja, dbAdmin);
                mesazh = koka.KrijoVeprimeKfImport(dbData, llojVeprimi, nrDok, dtDok, DateTime.Today, colTrupi[0].KodKF, colTrupi[0].NrLlogKunderParti, pershkrimi, colTrupi[0].KodMonedha, colTrupi[0].Vlefta, IdNdermarrja, IdNdermarrjeVit, vitNdermarrje.Viti, idStatusDok, nenkategoria, konfigAmbjenti, IdPerdoruesi, colTrupi, per.IdPeriudha, meKontabilizim, meFatura, IdGjuha, Rm, Ci, colTrupi[0].IdKF, colTrupi[0].IdLlogKunderParti, konfigdokLidhes, colTrupi[0].IdMonedha, importo);

                if (!mesazh.Status)
                {
                    throw new Exception(mesazh.PershkrimMesazhi);
                }

                if (importo)
                {
                    string id = "Id";

                    mesazh = koka.Ruaj(null, dbData);
                    if (!mesazh.Status)
                    {
                        foreach (DataRow dr in dokTable.Rows)
                        {
                            if (tePaImportuara.Select(String.Format("{0} = '{1}'", id, dr[id])).Count() == 0)
                                tePaImportuara.ImportRow(dr);
                        }
                        try
                        {
                            DateTime datedok = new DateTime();
                            bool dateVlefshme = DateTime.TryParse(dokTable.Rows[0][dtDokumentiEmerImport].ToString(), out datedok);
                            if (dateVlefshme)
                            {
                                object[] arr = { dokTable.Rows[0][nrDokumentiEmerImport], MessagesResource.Messages["labelDateDokumenti"] + " " + datedok.Day + "/" + datedok.Month + "/" + datedok.Year + ": " + mesazh.PershkrimMesazhi, indexRreshtImporti - 1 };
                                gabime.Rows.Add(arr);
                            }
                            else
                            {
                                object[] arr = { dokTable.Rows[0][nrDokumentiEmerImport].ToString(), error, indexRreshtImporti };
                                gabime.Rows.Add(arr);
                            }
                        }
                        catch (Exception ex)
                        {
                            LogManager.GetCurrentClassLogger().Error(ex.Message);
                            object[] arr = { dokTable.Rows[0][nrDokumentiEmerImport].ToString(), error, indexRreshtImporti };
                            gabime.Rows.Add(arr);
                        }
                        gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                    }
                }
                return mesazh;
            }
            catch (Exception ex)
            {
                LogManager.GetCurrentClassLogger().Error(ex.Message);
                error = ex.Message;
                if (error == "")
                {
                    error = MessagesResource.Messages["msgGagimIPanohur"];
                }
                try
                {
                    DateTime datedok = new DateTime();
                    bool dateVlefshme = DateTime.TryParse(dokTable.Rows[0][dtDokumentiEmerImport].ToString(), out datedok);
                    if (dateVlefshme)
                    {
                        object[] arr = { dokTable.Rows[0][nrDokumentiEmerImport], MessagesResource.Messages["labelDateDokumenti"] + " " + datedok.Day + "/" + datedok.Month + "/" + datedok.Year + " " + error, indexRreshtImporti - 1 };
                        gabime.Rows.Add(arr);
                    }
                    else
                    {
                        object[] arr = { dokTable.Rows[0][nrDokumentiEmerImport].ToString(), error, indexRreshtImporti };
                        gabime.Rows.Add(arr);
                    }
                    if (importo)
                    {
                        string id = "Id";
                        foreach (DataRow dr in dokTable.Rows)
                        {
                            if (tePaImportuara.Select(String.Format("{0} = '{1}'", id, dr[id])).Count() == 0)
                                tePaImportuara.ImportRow(dr);
                        }
                        gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                    }
                }
                catch (Exception)
                {
                    LogManager.GetCurrentClassLogger().Error(ex.Message);
                    object[] arr = { dokTable.Rows[0][nrDokumentiEmerImport].ToString(), error, indexRreshtImporti };
                    gabime.Rows.Add(arr);
                }
                return new clsMesazh(false, error);
            }
        }

        private colVeprimeKFTrupi krijoTrupVeprimeKF(DbData dbData, DataTable dokTrupi, string nrdok, colTrupiFormatImporti col, int pozicionkodi, DataTable gabime, DataTable tePaImportuara, bool importo, string nrDokumentiEmerImport, string dtDokumentiEmerImport, string kodKlientEmerImport, string llojDokumentiEmerImport, DateTime dtDok, ref int indexRreshtImporti, int llojKursi, clsMonedha monedhaNdermarrjes, bool kushtNDKF)
        {
            string error = "";
            int j = 1;
            colVeprimeKFTrupi colTrupi = new colVeprimeKFTrupi();
            int index = 0;
            foreach (DataRow dr in dokTrupi.Rows)
            {
                try
                {
                    error = "";
                    clsVeprimeKFTrupi trupi = new clsVeprimeKFTrupi();
                    string kodKlientFurn = String.Empty, llogKunderparti = String.Empty, monedha = String.Empty, debiKredi = String.Empty, kfKunderParti = String.Empty;
                    double vlera = 0, vleraMonBaze = 0, kursi = 1;

                    DateTime dtFature = DateTime.MinValue;

                    foreach (clsTrupiFormatImporti trup in col)
                    {
                        #region trupi
                        switch (trup.KodKontrolli)
                        {
                            case "Kod klient/furnitori":
                                kodKlientFurn = clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                            case "Llogari kunderparti":
                                llogKunderparti = clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                            case "Klient/Furnitor Kunderparti":
                                kfKunderParti = clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                            case "Debi/Kredi":
                                debiKredi = clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                            case "Monedha":
                                monedha = clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                            case "Vlera":
                                vlera = clsFunksione.vendosDouble(trup, dr, out error);
                                break;
                            case "Vlera mon baze":
                                vleraMonBaze = clsFunksione.vendosDouble(trup, dr, out error);
                                break;
                            case "Kursi":
                                kursi = clsFunksione.vendosDouble(trup, dr, out error);
                                break;
                        }
                        #endregion
                        if (error != "")
                        {
                            try
                            {
                                DateTime datedok = new DateTime();
                                bool dateVlefshme = DateTime.TryParse(dr[dtDokumentiEmerImport].ToString(), out datedok);
                                if (dateVlefshme)
                                {
                                    object[] arr = { dr[nrDokumentiEmerImport], MessagesResource.Messages["labelDateDokumenti"] + " " + datedok.Day + "/" + datedok.Month + "/" + datedok.Year + ", Rreshti " + j + " :" + trup.EmerImporti + " duhet te jete numer!", indexRreshtImporti };
                                    gabime.Rows.Add(arr);
                                }
                                else
                                {
                                    object[] arr = { dr[nrDokumentiEmerImport].ToString(), error, indexRreshtImporti };
                                    gabime.Rows.Add(arr);
                                }
                            }
                            catch (Exception ex)
                            {
                                LogManager.GetCurrentClassLogger().Error(ex.Message);
                                object[] arr = { dr[nrDokumentiEmerImport].ToString(), error, indexRreshtImporti };
                                gabime.Rows.Add(arr);
                            }
                            if (importo)
                            {
                                tePaImportuara.ImportRow(dr);
                                gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                            }
                        }
                    }
                    clsMesazh mesazh = new clsMesazh();
                    mesazh = trupi.KrijoTrupVeprimeKfImport(dbData, kodKlientFurn, monedha, llogKunderparti, debiKredi, vlera, vleraMonBaze, IdNdermarrja, dtDok, llojKursi, kursi, monedhaNdermarrjes, kfKunderParti, kushtNDKF);

                    if ((dokTrupi.Select($"[{kodKlientEmerImport}] = '{kodKlientFurn}'").Length > 1) && (dokTrupi.Select($"[{nrDokumentiEmerImport}] = '{nrdok}'").Length > 1) &&
                        (dokTrupi.Select($"[{dtDokumentiEmerImport}] = '{dtDok}'").Length > 1) && ((dokTrupi.Select($"[{llojDokumentiEmerImport}] = 'ND'").Length > 1) || (dokTrupi.Select($"[{llojDokumentiEmerImport}] = 'HGJFKF'").Length > 1)))


                        mesazh = new clsMesazh(false, String.Format("Klienti me kod {0} ekziston njehere ne gride! !", kodKlientFurn));



                    index++;
                    if (mesazh.Status)
                    {
                        colTrupi.Add(trupi);
                        indexRreshtImporti++; j++;
                    }
                    else
                        throw new MyException(mesazh.PershkrimMesazhi);
                }
                catch (MyException ex)
                {
                    LogManager.GetCurrentClassLogger().Error(ex.Message);
                    try
                    {
                        DateTime datedok = new DateTime();
                        bool dateVlefshme = DateTime.TryParse(dr[dtDokumentiEmerImport].ToString(), out datedok);
                        if (dateVlefshme)
                        {
                            error = MessagesResource.Messages["labelDateDokumenti"] + " " + datedok.Day + "/" + datedok.Month + "/" + datedok.Year + ", Rreshti " + j + ": " + ex.Message;
                            object[] arr = { dr[nrDokumentiEmerImport], error, indexRreshtImporti };
                            gabime.Rows.Add(arr);
                        }
                        else
                        {
                            object[] arr = { dr[nrDokumentiEmerImport].ToString(), ex.Message, indexRreshtImporti };
                            gabime.Rows.Add(arr);
                        }
                    }
                    catch (Exception err)
                    {
                        LogManager.GetCurrentClassLogger().Error(err.Message);
                        object[] arr = { dr[nrDokumentiEmerImport].ToString(), ex.Message, indexRreshtImporti };
                        gabime.Rows.Add(arr);
                    }
                    if (importo)
                    {
                        tePaImportuara.ImportRow(dr);
                        gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                    }
                    indexRreshtImporti++; j++;
                }
            }
            return colTrupi;
        }
        #endregion

        public void kontrolloPerdorues(DataTable dt, DataTable gabime, ref DataTable tePaImportuara, bool importo, int pozicionKodi, colTrupiFormatImporti col, bool isSQLtype)
        {
            DataTable dtImportuar = new DataTable();
            dtImportuar = gabime.Clone();
            
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, IdNdermarrja, IdViti, "Shto_Perdorues.aspx");
            int idlicenca = clsLicenca.merrIdLicencePerdoruesi(IdPerdoruesi);
            clsLicenca licenca = new clsLicenca(idlicenca);
            int nrPerdoruesishPerLicence = DbCore.DbAdmin.colPerdoruesit.merrNrPerdoruesishSipasLicences(IdPerdoruesi, licenca.IdLicenca);
            string primaryKey = "IDIMPORTSHITJE";
            string ndermarrjeKey = "";
            string error = "";
            int i = 1;
            foreach (DataRow dr in dt.Rows)
            {
                if (gabime.AsEnumerable().Any(row => dr["Perdoruesi"] == row.Field<string>("Kodi"))) { i++; continue; }
                clsPerdorues perdorues = new clsPerdorues();
                string emriperdorues = "", mbiemriperdorues = "", perdoruesusername = "", perdoruespassword = "", perdoruespasswordkonfirmo = "", perdoruestel = "", perdoruesfax = "", perdoruesemail = "", perdoruesadresa = "", kerkeseResetPass = "", kodNdermarje = "", usericrm = "", userEtopuP = "", iDEtopUp = "", typeofDevice = "", salesrepMobileNumber = "", salesrepMPesaMSISDN = "", gjinia = "", commentsretailSales = "", accountexecutive = "", idNumber = "", commentsretailOpSpecialist = "", regionalsupervisor = "", retailsalesAccountExecutive = "", retailsalesAreaManager = "", sitecode = "", districti = "", shopmainCode = "", latitude = "", longitude = "", rolet = "", qyteti = "", status = "", leaveReason = "", uniform = "", autorizime = "", gjuha = "", llojiVeprimit = "", fjalekalimiekzistues = "";
                string passwordiPerkohshem = "", perdoruesiKycur = "", kontrolloPassword = "", perdoruesaktiv = "", isinsured = "", statusAprovimi = "";
                string salesrepStartDateVod = "", salesrepTrainingStart = "", salesrepStartDateShop = "", salesrepMaternityLeaveStart = "", leavedateVod = "", leavedateShop = "", maternityleaveEndDate = "", trainingendDate = "", birthDate = "", channel = ""; bool boshLeaveDateShop = false; ;
                foreach (DbCore.DbAdmin.clsTrupiFormatImporti trup in col)
                {
                    error = "";
                    switch (trup.KodKontrolli)
                    {
                        #region Fushat
                        case "Emri":

                            emriperdorues = clsFunksione.vendosVlere(trup, dr, out error);
                            break;
                        case "Mbiemri":
                            mbiemriperdorues = clsFunksione.vendosVlere(trup, dr, out error);
                            break;
                        case "Perdoruesi":
                            perdoruesusername = clsFunksione.vendosVlere(trup, dr, out error);
                            break;
                        case "Fjalekalimi":
                            perdoruespassword = clsFunksione.vendosVlere(trup, dr, out error);
                            break;
                        case "Konfirmimi i fjalekalimit":
                            perdoruespasswordkonfirmo = clsFunksione.vendosVlere(trup, dr, out error);
                            break;
                        case "Kontrollo fjalekalim":
                            kontrolloPassword = clsFunksione.vendosVlere(trup, dr, out error);
                            break;
                        case "Fjalekalim ekzistues":
                            fjalekalimiekzistues = clsFunksione.vendosVlere(trup, dr, out error);
                            break;
                        case "Gjuha":
                            gjuha = clsFunksione.vendosVlere(trup, dr, out error);
                            break;
                        case "Aktiv":
                            perdoruesaktiv = clsFunksione.vendosVlere(trup, dr, out error);
                            break;
                        case "Roli":
                            rolet = clsFunksione.vendosVlere(trup, dr, out error);
                            break;
                        case "Lloji i veprimit":
                            llojiVeprimit = clsFunksione.vendosVlere(trup, dr, out error);
                            break;
                        case "Qyteti":
                            qyteti = clsFunksione.vendosVlere(trup, dr, out error);
                            break;
                        case "Tel":
                            perdoruestel = clsFunksione.vendosVlere(trup, dr, out error);
                            break;
                        case "Fax":
                            perdoruesfax = clsFunksione.vendosVlere(trup, dr, out error);
                            break;
                        case "Email":
                            perdoruesemail = clsFunksione.vendosVlere(trup, dr, out error);
                            break;
                        case "Adresa":
                            perdoruesadresa = clsFunksione.vendosVlere(trup, dr, out error);
                            break;
                        case "Kycur":
                            perdoruesiKycur = clsFunksione.vendosVlere(trup, dr, out error);
                            break;
                        case "Fjalekalim i perkohshem":
                            passwordiPerkohshem = clsFunksione.vendosVlere(trup, dr, out error);
                            break;
                        case "Kerkese per reset fjalekalimi":
                            kerkeseResetPass = clsFunksione.vendosVlere(trup, dr, out error);
                            break;
                        case "Useri i CRM":
                            usericrm = clsFunksione.vendosVlere(trup, dr, out error);
                            break;
                        case "Useri i ETopUp":
                            userEtopuP = clsFunksione.vendosVlere(trup, dr, out error);
                            break;
                        case "ID e ETopUp":
                            iDEtopUp = clsFunksione.vendosVlere(trup, dr, out error);
                            break;
                        case "Tipi I Aparatit qe perdor":
                            typeofDevice = clsFunksione.vendosVlere(trup, dr, out error);
                            break;
                        case "Numri I telefonit te perfaqesuesit te Shitjes":
                            salesrepMobileNumber = clsFunksione.vendosVlere(trup, dr, out error);
                            break;
                        case "Date fillimi Trajnimi":
                            salesrepTrainingStart = clsFunksione.vendosVlere(trup, dr, out error);
                            break;
                        case "Revoke Eforms":
                            salesrepMPesaMSISDN = clsFunksione.vendosVlere(trup, dr, out error);
                            break;
                        case "Date Fillimi ne dyqan":
                            salesrepStartDateShop = clsFunksione.vendosVlere(trup, dr, out error);
                            break;
                        case "Date Fillimi te lejes se lindjes":
                            salesrepMaternityLeaveStart = clsFunksione.vendosVlere(trup, dr, out error);
                            break;
                        case "Date largimi nga VF":
                            leavedateVod = clsFunksione.vendosVlere(trup, dr, out error);
                            break;
                        case "Date largimi nga dyqani":
                            leavedateShop = clsFunksione.vendosVlere(trup, dr, out error);
                            break;
                        case "Date mbarimi te lejes se lindjes":
                            maternityleaveEndDate = clsFunksione.vendosVlere(trup, dr, out error);
                            break;
                        case "Date mbarimi Trajnimi End Date":
                            trainingendDate = clsFunksione.vendosVlere(trup, dr, out error);
                            break;
                        case "Gjinia":
                            gjinia = clsFunksione.vendosVlere(trup, dr, out error);
                            break;
                        case "Komente nga Retail Sales":
                            commentsretailSales = clsFunksione.vendosVlere(trup, dr, out error);
                            break;
                        case "Opened Date":
                            accountexecutive = clsFunksione.vendosVlere(trup, dr, out error);
                            break;
                        case "Numer personal":
                            idNumber = clsFunksione.vendosVlere(trup, dr, out error);
                            break;
                        case "I Siguruar (Po/Jo)":
                            isinsured = clsFunksione.vendosVlere(trup, dr, out error);
                            break;
                        case "Komente nga Retail Operations Specialist":
                            commentsretailOpSpecialist = clsFunksione.vendosVlere(trup, dr, out error);
                            break;
                        case "Regional Supervisor":
                            regionalsupervisor = clsFunksione.vendosVlere(trup, dr, out error);
                            break;
                        case "Retail Sales Account Executive":
                            retailsalesAccountExecutive = clsFunksione.vendosVlere(trup, dr, out error);
                            break;
                        case "Retail Sales Area Manager":
                            retailsalesAreaManager = clsFunksione.vendosVlere(trup, dr, out error);
                            break;
                        case "Datelindje":
                            birthDate = clsFunksione.vendosVlere(trup, dr, out error);
                            break;
                        case "Site Code":
                            sitecode = clsFunksione.vendosVlere(trup, dr, out error);
                            break;
                        case "District":
                            districti = clsFunksione.vendosVlere(trup, dr, out error);
                            break;
                        case "Shop Main Code":
                            shopmainCode = clsFunksione.vendosVlere(trup, dr, out error);
                            break;
                        case "Longitude":
                            longitude = clsFunksione.vendosVlere(trup, dr, out error);
                            break;
                        case "Latitude":
                            latitude = clsFunksione.vendosVlere(trup, dr, out error);
                            break;
                        case "Status":
                            status = clsFunksione.vendosVlere(trup, dr, out error);
                            break;
                        case "Uniform":
                            uniform = clsFunksione.vendosVlere(trup, dr, out error);
                            break;
                        case "Leave Reason":
                            leaveReason = clsFunksione.vendosVlere(trup, dr, out error);
                            break;
                        case "Data kur ka filluar ne Vodafone":
                            salesrepStartDateVod = clsFunksione.vendosVlere(trup, dr, out error);
                            break;
                        case "Autorizime":
                            autorizime = clsFunksione.vendosVlere(trup, dr, out error);
                            break;
                        case "Status aprovimi":
                            statusAprovimi = clsFunksione.vendosVlere(trup, dr, out error);
                            break;
                        case "Channel":
                            channel = clsFunksione.vendosVlere(trup, dr, out error);
                            break;
                        case "Kod Ndermarrje":
                            ndermarrjeKey = trup.EmerImporti;
                            break;
                            #endregion
                    }
                    if (error != "")
                    {
                        object[] arr = { dr[pozicionKodi], trup.EmerImporti + " duhet te jete numer!", i };
                        gabime.Rows.Add(arr);
                        if (importo)
                        {
                            tePaImportuara.ImportRow(dr);
                            //gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                        }
                        break;
                    }
                }
                if (error != "")
                {
                    i++; continue;
                }
                try
                {
                    if (rolet.StartsWith("SR") && isSQLtype)
                    {
                        if (llojiVeprimit.ToLower() == "shtim")
                        {
                            clsPerdorues perd = new clsPerdorues(perdoruesusername);
                            status = "In Trainning";
                        }

                        if (llojiVeprimit.ToLower() == "modifikim")
                        {
                            clsPerdorues perd = new clsPerdorues(perdoruesusername);
                            if (perd.IdPerdorues == 0) { error = $"Perdoruesi nuk ekziston."; throw new Exception(error); }
                            if (perd.ShopMainCode != shopmainCode)
                            {
                                perd.LeaveDateShop = DateTime.Today.Date;
                                perd.Status = clsShopsHierarkiStatus.merrIdStatusPerdoruesiSipasPershkrimi("Transferred");
                                if (importo)
                                    perd.modifiko("false", "", IdNdermarrja, false, IdNdermarrjeVit, true);
                                boshLeaveDateShop = true;
                                leavedateShop = "";
                                salesrepStartDateShop = DateTime.Now.ToString("dd/MM/yyyy");
                                switch (channel)
                                {
                                    case "Dealer":
                                        status = "In Work";
                                        break;
                                    case "Express":
                                        status = "In Work_Exp";
                                        break;
                                    case "Own":
                                        status = "In Work_OWN";
                                        break;
                                }
                            }
                        }
                    }

                    perdorues = perdorues.krijoPerdoruesPerImport(tedrejtaInfo.DShtim, tedrejtaInfo.DMod, licenca, nrPerdoruesishPerLicence, IdNdermarrja, emriperdorues, mbiemriperdorues, perdoruesusername, perdoruespassword, perdoruespasswordkonfirmo, kontrolloPassword, gjuha, perdoruesaktiv, rolet, llojiVeprimit, qyteti, perdoruestel, perdoruesfax, perdoruesemail, perdoruesadresa, perdoruesiKycur, passwordiPerkohshem, fjalekalimiekzistues, kodNdermarje, kerkeseResetPass, usericrm, userEtopuP, iDEtopUp, typeofDevice, salesrepMobileNumber, salesrepTrainingStart, salesrepMPesaMSISDN, salesrepStartDateShop, salesrepMaternityLeaveStart, leavedateVod, leavedateShop, maternityleaveEndDate, trainingendDate, gjinia, commentsretailSales, accountexecutive, idNumber, isinsured, commentsretailOpSpecialist, regionalsupervisor, retailsalesAccountExecutive, retailsalesAreaManager, birthDate, sitecode, districti, shopmainCode, longitude, latitude, status, uniform, leaveReason, salesrepStartDateVod, autorizime, IdPerdoruesi, statusAprovimi, channel, boshLeaveDateShop);

                    if (importo)
                    {
                        string idDokImporti = "";
                        if (isSQLtype)
                        {
                            idDokImporti = dr["IDIMPORTSHITJE"].ToString();
                        }
                        clsMesazh mesazhinv = (llojiVeprimit.ToLower() == "shtim") ? perdorues.ruaj(IdNdermarrja, IdNdermarrjeVit, idDokImporti, isSQLtype, TabKoka, ndermarrjeKey, primaryKey, "true") : perdorues.modifiko("false", "", IdNdermarrja, false, IdNdermarrjeVit, true, idDokImporti, isSQLtype, TabKoka, ndermarrjeKey, primaryKey);
                        object[] arr = { dr[pozicionKodi], mesazhinv.PershkrimMesazhi, i };
                        if (!mesazhinv.Status)
                        {
                            gabime.Rows.Add(arr);
                            tePaImportuara.ImportRow(dr);
                        }
                        else
                            dtImportuar.Rows.Add(arr);
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
                        object[] arr = { dr[pozicionKodi], error, i };
                        gabime.Rows.Add(arr);
                        if (importo)
                        {
                            tePaImportuara.ImportRow(dr);
                        }
                    }
                    i++;
                }
            }
            if (importo)
            {
                Status = "import";

                if (KonfigImporti.DergoMeEmail)
                    ReportFunctions.DergoEmailRaporteRezultatImporti(dtImportuar, gabime, KonfigImporti.Id, FormatImporti.Kodi, IdNdermarrja, IdPerdoruesi);
            }
            return;
        }

        private clsMesazh kontrolloMagazina(DataTable dt, DataTable gabime, ref DataTable tePaImportuara, bool importo, int pozicionkodi, colTrupiFormatImporti col)
        {
            try
            {
                var konf = new clsKonfigurimAmbjenti();
                konf.mbushKonfigAmbjSipasKod("MAG", IdNdermarrja);
                var error = "";
                const bool shtim = true;

                var i = 1;
                foreach (DataRow dr in dt.Rows)
                {
                    var njesi = new clsNjesiAdministrative();
                    string kodi = "", pershkrimi = "", adresa = "", degeadministrative = "", lloji = "", statusi = "", shenime = "", telefon = "", llojLayeri = "", email = "", magvartese = "", autorizimi = "", kodbari = "";
                    var aktiv = false;
                    const bool ndjekjegjendje = true;
                    int idinventarizimi = 1, jetegjatesia = 1;
                    var dateregjistrimi = DateTime.Today;
                    var datefillimistatusi = DateTime.Today;
                    bool ownShop = false;
                    foreach (var trup in col)
                    {
                        error = "";
                        switch (trup.KodKontrolli)
                        {
                            case "Kodi":
                                kodi = clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                            case "Pershkrimi":
                                pershkrimi = clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                            case "Adresa":
                                adresa = clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                            case "Dege Administrative":
                                degeadministrative = clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                            case "Aktiv":
                                aktiv = clsFunksione.vendosBool(trup, dr, out error);
                                break;
                            case "Lloji":
                                lloji = clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                            case "Data e fillimit te statusit":
                                datefillimistatusi = clsFunksione.vendosDate(trup, dr, out error);
                                if (datefillimistatusi == DateTime.MinValue)
                                    datefillimistatusi = DateTime.Today;
                                break;
                            case "Jetegjatesia ne vite":
                                jetegjatesia = clsFunksione.vendosVlere(trup, dr, out error) == ""
                                    ? 1
                                    : Convert.ToInt32(Convert.ToDecimal(clsFunksione.vendosVlere(trup, dr, out error)));
                                break;
                            case "Statusi":
                                statusi = clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                            case "Shenime":
                                shenime = clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                            case "Telefon":
                                telefon = clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                            case "Lloj Layeri":
                                llojLayeri = clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                            case "Email":
                                email = clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                            case "Magazine Vartese":
                                magvartese = clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                            case "Autorizimi":
                                autorizimi = clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                            case "Kodbari":
                                kodbari = clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                            case "Own Shop":
                                ownShop = clsFunksione.vendosBool(trup, dr, out error);
                                break;
                        }
                        if (error == "") continue;
                        object[] arr = { dr[pozicionkodi], trup.EmerImporti + " " + MessagesResource.Messages["msgDuhetTeJeteNumer"], i };
                        gabime.Rows.Add(arr);
                        if (importo)
                        {
                            tePaImportuara.ImportRow(dr);
                            gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                        }
                        break;
                    }

                    if (error != "")
                        continue;

                    try
                    {
                        njesi = clsNjesiAdministrative.krijoPerImport(kodi.RemoveSpaces(), pershkrimi.RemoveSpaces(), adresa, degeadministrative, aktiv, idinventarizimi, ndjekjegjendje, IdNdermarrja, IdPerdoruesi, dateregjistrimi, konf.IdKonfigAmbjente, shtim, lloji, statusi, datefillimistatusi, 0, jetegjatesia, "", false, shenime, telefon, llojLayeri, magvartese, email, autorizimi, Rm, Ci, false, false, ownShop);
                        var idPeriudheZgjedhur = mySessionObjects.merrPeriudheKontabel(Session).IdPeriudha;
                        if (importo)
                        {
                            var mesazhinv = njesi.ruajMagazine(new Dictionary<string, object>(), IdNdermarrjeVit, idPeriudheZgjedhur, konf.IdNivel);
                            if (!mesazhinv.Status)
                            {
                                object[] arr = { dr[pozicionkodi], mesazhinv.PershkrimMesazhi, i };
                                gabime.Rows.Add(arr);
                                tePaImportuara.ImportRow(dr);
                                gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LogManager.GetCurrentClassLogger().Error(ex.Message);
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
                }
                if (!importo)
                    return new clsMesazh(true);

                Status = "import";
                return new clsMesazh(true);
            }
            catch (Exception err)
            {
                LogManager.GetCurrentClassLogger().Error(err.Message);
                return new clsMesazh(false);
            }
        }

        private clsMesazh kontrolloPikeShitjeFurnizimi(DataTable dt, DataTable gabime, ref DataTable tePaImportuara, bool importo, int pozicionkodi, colTrupiFormatImporti col)
        {
            try
            {
                clsDatabaseShare dbShare = new clsDatabaseShare();
                clsKonfigurimAmbjenti konf = new clsKonfigurimAmbjenti("PSH", IdNdermarrja, dbShare);
                clsKonfigurimAmbjenti konf2 = new clsKonfigurimAmbjenti("PF", IdNdermarrja, dbShare);

                string error = "";
                const bool shtim = true;

                int i = 1;
                foreach (DataRow dr in dt.Rows)
                {
                    clsPikeShitjeFurnizimi njesi = new clsPikeShitjeFurnizimi();
                    string kodi = "", pershkrimi = "", adresa = "", degeadministrative = "", pikeshitje = "";
                    bool aktiv = false, pshf = true; ;
                    int idkonfig = konf.IdKonfigAmbjente;
                    DateTime dateregjistrimi = DateTime.Today;
                    foreach (clsTrupiFormatImporti trup in col)
                    {
                        error = "";
                        switch (trup.KodKontrolli)
                        {
                            case "Kodi":
                                kodi = clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                            case "Pershkrimi":
                                pershkrimi = clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                            case "Adresa":
                                adresa = clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                            case "Dege Administrative":
                                degeadministrative = clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                            case "Aktiv":
                                aktiv = clsFunksione.vendosBool(trup, dr, out error);
                                break;
                            case "Pike Shitje/Furnizimi":
                                pikeshitje = clsFunksione.vendosVlere(trup, dr, out error);
                                if (pikeshitje == "Pike Shitje")
                                {
                                    pshf = true;
                                    idkonfig = konf.IdKonfigAmbjente;
                                }
                                else if (pikeshitje == "Pike Furnizimi")
                                {
                                    pshf = false;
                                    idkonfig = konf2.IdKonfigAmbjente;
                                }
                                else
                                {
                                    object[] arr = { dr[pozicionkodi], " " + MessagesResource.Messages["msgPikeNukEkziston"], i };
                                    gabime.Rows.Add(arr);
                                    if (importo)
                                    {
                                        tePaImportuara.ImportRow(dr);
                                        gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;

                                    }
                                }
                                break;
                        }
                        if (error != "")
                        {
                            object[] arr = { dr[pozicionkodi], trup.EmerImporti + " " + MessagesResource.Messages["msgDuhetTeJeteNumer"], i };
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
                        continue;

                    try
                    {
                        njesi = njesi.krijoPerImport(kodi.RemoveSpaces(), pershkrimi.RemoveSpaces(), adresa, degeadministrative, aktiv, IdNdermarrja, IdPerdoruesi, dateregjistrimi, idkonfig, pshf, shtim, Rm, Ci);

                        if (importo)
                        {
                            clsMesazh mesazhinv = njesi.ruaj(new Dictionary<string, object>());

                            if (!mesazhinv.Status)
                            {
                                object[] arr = { dr[pozicionkodi], mesazhinv.PershkrimMesazhi, i };
                                gabime.Rows.Add(arr);
                                tePaImportuara.ImportRow(dr);
                                gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LogManager.GetCurrentClassLogger().Error(ex.Message);
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

                }
                if (importo)
                    Status = "import";

                return new clsMesazh(true);
            }
            catch (Exception err)
            {
                LogManager.GetCurrentClassLogger().Error(err.Message);
                return new clsMesazh(false);
            }
        }

        private clsMesazh kontrolloDegeAdministrative(DataTable dt, DataTable gabime, ref DataTable tePaImportuara, bool importo, int pozicionkodi, colTrupiFormatImporti col)
        {
            try
            {

                clsKonfigurimAmbjenti konf = new clsKonfigurimAmbjenti();
                konf.mbushKonfigAmbjSipasKod("DA", IdNdermarrja);
                string error = "";
                const bool shtim = true;

                int i = 1;
                foreach (DataRow dr in dt.Rows)
                {
                    clsDegeAdministrative njesi = new clsDegeAdministrative();
                    string kodi = "", pershkrimi = "", adresa = "", qendra = ""; int llojqendre = 1;
                    bool aktiv = false;

                    DateTime dateregjistrimi = DateTime.Today;
                    //      DateTime dateregjistrimi = DateTime.Today;
                    foreach (clsTrupiFormatImporti trup in col)
                    {
                        error = "";
                        switch (trup.KodKontrolli)
                        {
                            case "Kodi":
                                kodi = clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                            case "Pershkrimi":
                                pershkrimi = clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                            case "Adresa":
                                adresa = clsFunksione.vendosVlere(trup, dr, out error);
                                break;

                            case "Aktiv":
                                aktiv = clsFunksione.vendosBool(trup, dr, out error);
                                break;
                            case "Lloj qendre":
                                llojqendre = (clsFunksione.vendosVlere(trup, dr, out error) == "Qendra Kosto") ? 1 : 2;
                                break;
                            case "Qender kosto":
                                qendra = clsFunksione.vendosVlere(trup, dr, out error);
                                break;

                        }
                        if (error != "")
                        {
                            object[] arr = { dr[pozicionkodi], trup.EmerImporti + " " + MessagesResource.Messages["msgDuhetTeJeteNumer"], i };
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
                        continue;

                    try
                    {
                        njesi = njesi.krijoPerImport(kodi.RemoveSpaces(), pershkrimi.RemoveSpaces(), adresa, aktiv, IdNdermarrja, IdPerdoruesi, dateregjistrimi, konf.IdKonfigAmbjente, llojqendre, qendra, shtim, Rm, Ci, "");

                        if (importo)
                        {
                            clsMesazh mesazhinv = njesi.ruaj();

                            if (!mesazhinv.Status)
                            {
                                object[] arr = { dr[pozicionkodi], mesazhinv.PershkrimMesazhi, i };
                                gabime.Rows.Add(arr);
                                tePaImportuara.ImportRow(dr);
                                gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LogManager.GetCurrentClassLogger().Error(ex.Message);
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
                }
                if (importo)
                    Status = "import";

                return new clsMesazh(true);
            }
            catch (Exception err)
            {
                LogManager.GetCurrentClassLogger().Error(err.Message);
                return new clsMesazh(false);
            }
        }

        private clsMesazh kontrolloDetajimeArtikulli(DataTable dt, DataTable gabime, ref DataTable tePaImportuara, bool importo, int pozicionkodi, colTrupiFormatImporti col)
        {
            try
            {
                string error = "";
                int i = 1;
                foreach (DataRow dr in dt.Rows)
                {
                    clsDetajimArtikulli detajimi;
                    string kodidetajimi = String.Empty, emertimdetajimi = String.Empty, kategoridetajimi = String.Empty, llojdetajimi = String.Empty;

                    foreach (clsTrupiFormatImporti trup in col)
                    {
                        error = "";
                        switch (trup.KodKontrolli)
                        {
                            case "Kategori":
                                kategoridetajimi = clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                            case "Lloji":
                                llojdetajimi = clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                            case "Kodi":
                                kodidetajimi = clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                            case "Emertimi":
                                emertimdetajimi = clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                        }
                        if (error != "")
                        {
                            object[] arr = { dr[pozicionkodi], trup.EmerImporti + " " + MessagesResource.Messages["msgDuhetTeJeteNumer"], i };
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
                        continue;

                    try
                    {
                        int idStatusDok = 1;
                        detajimi = clsDetajimArtikulli.krijoDetajimPerImport(kategoridetajimi, llojdetajimi, kodidetajimi, emertimdetajimi, IdPerdoruesi, IdNdermarrja, idStatusDok);

                        if (importo)
                        {
                            clsMesazh mesazhinv = detajimi.ruaj();

                            if (!mesazhinv.Status)
                            {
                                object[] arr = { dr[pozicionkodi], mesazhinv.PershkrimMesazhi, i };
                                gabime.Rows.Add(arr);
                                tePaImportuara.ImportRow(dr);
                                gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LogManager.GetCurrentClassLogger().Error(ex.Message);
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
                }
                if (importo)
                    Status = "import";

                return new clsMesazh(true);
            }
            catch (Exception err)
            {
                LogManager.GetCurrentClassLogger().Error(err.Message);
                return new clsMesazh(false);
            }
        }

        #region Amortizimi Fillestar
        private clsMesazh kontrolloDokAmortizimiFillestar(DataTable dt, DataTable gabime, ref DataTable tePaImportuara, bool importo, int pozicionkodi, colTrupiFormatImporti col)
        {
            try
            {
                foreach (clsTrupiFormatImporti trupi in col)
                {
                    clsFunksione.vendosVlereDefaultTeDataTable(trupi, dt);
                }

                string fushatEGrupimit = "";

                DataTable dataGrupime;
                //grupojme dokumentet qe vijne si datatable sipas Nr te dokumentit, dates se dokumentit dhe llojit te dokumentit dhe i ruajme ato tek tabela dataGrupime
                string fushaGrupimi = "";
                foreach (clsTrupiFormatImporti trupi in col)
                {
                    if (trupi.Visible && trupi.Shfaq && (trupi.FusheKokeApoTrupi == 1 || trupi.FusheKokeApoTrupi == 3))
                        fushaGrupimi += trupi.EmerImporti + ";";
                }
                fushatEGrupimit = fushaGrupimi.Substring(0, fushaGrupimi.LastIndexOf(';'));//heqim pikepresjen e fundit
                string[] fushat = fushatEGrupimit.Split(';');
                dataGrupime = dt.DefaultView.ToTable(true, fushat);
                colKokaShitje colBlerjeShitje = new colKokaShitje();
                int indexRreshtImporti = 1;
                string serverName = MyConnectionsManager.GetSelectedConNameServer();
                foreach (DataRow drDok in dataGrupime.Rows)
                {
                    DataTable dokumentKokTrup = null;
                    try
                    {

                        //krijojme nje tabele te re, ku vendosim dokumentin 
                        string selekti = "";
                        for (int j = 0; j < fushat.Count(); j++)
                        {
                            if (!String.IsNullOrEmpty(drDok[fushat[j]].ToString()))
                            {
                                string fusha = drDok[fushat[j]].ToString().Replace("'", "''");
                                selekti += "[" + fushat[j] + "] = '" + drDok[fushat[j]] + "' AND ";
                            }
                        }
                        selekti += "1 = 1";
                        dokumentKokTrup = dt.Select(selekti).GetDataTable(dt);
                        clsMesazh mesazh = krijoDokAmortizimiFillestar(serverName, dokumentKokTrup, col, pozicionkodi, gabime, tePaImportuara, importo, false, ref indexRreshtImporti);
                    }
                    catch (Exception ex)
                    {
                        //write something                   
                        LogManager.GetCurrentClassLogger().Error(ex.Message);
                        object[] arr = { "XXX", MessagesResource.Messages["msgGagimIPanohur"], indexRreshtImporti };
                        gabime.Rows.Add(arr);
                    }
                }
                if (importo)
                    Status = "import";
            }
            catch (Exception ex)
            {
                LogManager.GetCurrentClassLogger().Error(ex.Message);
                return new clsMesazh(false);
            }
            return new clsMesazh(true);
        }
        private clsMesazh krijoDokAmortizimiFillestar(string serverName, DataTable dokTable, colTrupiFormatImporti col, int pozicionkodi, DataTable gabime, DataTable tePaImportuara, bool importo, bool vjenNgaImportSQL, ref int indexRreshtImporti)
        {
            if (dokTable == null)
                return new clsMesazh(false);
            string error = "";
            string nrDokumentiEmerImport = "";
            string dtDokumentiEmerImport = "";
            try
            {
                nrDokumentiEmerImport = col.filtroFormatImportiSipasFushes("Nr Dokumenti").EmerImporti;
                dtDokumentiEmerImport = col.filtroFormatImportiSipasFushes("Date Dokumenti").EmerImporti;

                clsMesazh mesazh = new clsMesazh();

                string nenkategoria = "", llojDokumenti = "", nrDok = "", pershkrimi = "", llogKunderParti = "", standarti = "";
                DateTime dtDok = new DateTime();
                error = "";

                #region Fushat e kokes
                foreach (clsTrupiFormatImporti trup in col)
                {
                    error = "";
                    switch (trup.KodKontrolli)
                    {
                        case "Nenkategoria":
                            nenkategoria = clsFunksione.vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                        case "Lloji":
                            llojDokumenti = clsFunksione.vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                        case "Nr Dokumenti":
                            nrDok = clsFunksione.vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                        case "Pershkrimi":
                            pershkrimi = clsFunksione.vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                        case "Date Dokumenti":
                            dtDok = clsFunksione.vendosDate(trup, dokTable.Rows[0], out error);
                            if (dtDok == DateTime.MinValue)
                                dtDok = DateTime.Today;
                            break;
                        case "Llogari Kunderparti":
                            llogKunderParti = clsFunksione.vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                        case "Standarti":
                            standarti = clsFunksione.vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                    }
                    if (error != "")
                    {
                        try
                        {
                            DateTime datedok = new DateTime();
                            bool dateVlefshme = DateTime.TryParse(dokTable.Rows[0][dtDokumentiEmerImport].ToString(), out datedok);
                            if (dateVlefshme)
                            {
                                object[] arr = { dokTable.Rows[0][nrDokumentiEmerImport], "Date Dokumenti " + datedok.Day + "/" + datedok.Month + "/" + datedok.Year + " :" + trup.EmerImporti + " duhet te jete numer!", indexRreshtImporti };
                                gabime.Rows.Add(arr);
                            }
                            else
                            {
                                object[] arr = { dokTable.Rows[0][nrDokumentiEmerImport].ToString(), error, indexRreshtImporti };
                                gabime.Rows.Add(arr);
                            }
                        }
                        catch (Exception ex)
                        {
                            LogManager.GetCurrentClassLogger().Error(ex.Message);
                            object[] arr = { dokTable.Rows[0][nrDokumentiEmerImport].ToString(), error, indexRreshtImporti };
                            gabime.Rows.Add(arr);
                        }

                        if (importo)
                        {
                            foreach (DataRow dr in dokTable.Rows)
                            {
                                if (tePaImportuara.Select(String.Format("Id = '{0}'", dokTable.Rows[0]["Id"])).Count() == 0)
                                    tePaImportuara.ImportRow(dokTable.Rows[0]);
                            }
                            gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                        }
                    }
                }

                #endregion

                colAmortizimiKoka amortizimetKoka = new colAmortizimiKoka();
                clsAmortizimiKoka koka = new clsAmortizimiKoka();
                colAmortizimiTrupiAbstract colTrupi = new colAmortizimiTrupi();
                colSerialetMagazine seriale = new colSerialetMagazine();
                clsKonfigurimAmbjenti konfigAmbjenti = new clsKonfigurimAmbjenti();

                konfigAmbjenti.mbushKonfigAmbjSipasKod(llojDokumenti, IdNdermarrja);

                if (MbylljePeriudhe.PeriodClosing.IsPeriodClosed(dtDok, serverName, IdNdermarrja, KategoriDokumenti.RivleresimeAmortizimi, konfigAmbjenti.IdKonfigAmbjente))
                    throw new Exception(MessagesResource.Messages["msgPeriodIsClosed"]);

                bool kontrolloSerial = false;
                if (clsAlternativaKushti.getAlternativa(konfigAmbjenti.IdKonfigAmbjente, "FAAP") == "Analitike")
                    kontrolloSerial = true;
                else
                    kontrolloSerial = false;

                bool isMagENjejte = true;

                List<double> amortizimiFillestar = new List<double>();

                colTrupi = krijoTrupAmortizimi(dokTable, col, pozicionkodi, gabime, tePaImportuara, importo, out isMagENjejte, nrDokumentiEmerImport, dtDokumentiEmerImport, kontrolloSerial, amortizimiFillestar, ref indexRreshtImporti);

                clsPeriudhaKontabel per = new clsPeriudhaKontabel(dtDok, IdNdermarrja);
                DateTime dtRegjistrimi = DateTime.Today;

                int idStatusDok = 0;

                int meKontabilizim = 0;
                if (clsAlternativaKushti.getAlternativa(konfigAmbjenti.IdKonfigAmbjente, "SDI") == "Draft")
                {
                    idStatusDok = 0;
                    meKontabilizim = 0;
                }
                else
                {
                    idStatusDok = 1;
                    meKontabilizim = 1;
                }

                colAmortizimiFillestar colaqt = new colAmortizimiFillestar();
                int idMag = -1;
                string kodMag = "";
                if (isMagENjejte && colTrupi.Count > 0)
                {
                    idMag = colTrupi[0].IdNjesiAdministrative;
                    kodMag = clsNjesiAdministrative.ktheKodiNjesiAdministrativeSipasiDPaAutorizime(idMag);
                }
                mesazh = koka.krijoDokumentAmortizimiPerAmortizimFillestarPerImport(nrDok, nenkategoria, dtDok, dtDok, kodMag, idMag, standarti, pershkrimi, IdNdermarrjeVit, IdNdermarrja, IdPerdoruesi, dtRegjistrimi, idStatusDok, llogKunderParti, colTrupi, colaqt, amortizimiFillestar, IdPerdoruesi, konfigAmbjenti, per, idStatusDok, Rm, Ci);

                if (!mesazh.Status)
                {
                    throw new Exception(mesazh.PershkrimMesazhi);
                }

                amortizimetKoka.Add(koka);

                if (importo)
                {
                    if (gabime.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dokTable.Rows)
                        {
                            if (tePaImportuara.Select(String.Format("Id = '{0}'", dr["Id"])).Count() == 0)
                                tePaImportuara.ImportRow(dr);
                        }

                        gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                        return new clsMesazh(false, MessagesResource.Messages["msgKaGabime"]);
                    }
                    string shfaqmesazhapolupe = "jo";
                    string mesazhmevonshem = "";
                    mesazh = amortizimetKoka.ruajListAmortizimeTrans(0, meKontabilizim, out shfaqmesazhapolupe, new colAmortizimiKoka(), per.IdPeriudha, 90, seriale, true, colaqt, null, Rm, Ci, false, out mesazhmevonshem);

                    if (!mesazh.Status)
                    {
                        foreach (DataRow dr in dokTable.Rows)
                        {
                            if (tePaImportuara.Select(String.Format("Id = '{0}'", dr["Id"])).Count() == 0)
                                tePaImportuara.ImportRow(dr);
                        }
                        try
                        {
                            DateTime datedok = new DateTime();
                            bool dateVlefshme = DateTime.TryParse(dokTable.Rows[0][dtDokumentiEmerImport].ToString(), out datedok);
                            if (dateVlefshme)
                            {
                                object[] arr = { dokTable.Rows[0][nrDokumentiEmerImport], MessagesResource.Messages["labelDateDokumenti"] + " " + datedok.Day + "/" + datedok.Month + "/" + datedok.Year + ": " + mesazh.PershkrimMesazhi, indexRreshtImporti - 1 };
                                gabime.Rows.Add(arr);
                            }
                            else
                            {
                                object[] arr = { dokTable.Rows[0][nrDokumentiEmerImport].ToString(), error, indexRreshtImporti };
                                gabime.Rows.Add(arr);
                            }
                        }
                        catch (Exception ex)
                        {
                            LogManager.GetCurrentClassLogger().Error(ex.Message);
                            object[] arr = { dokTable.Rows[0][nrDokumentiEmerImport].ToString(), error, indexRreshtImporti };
                            gabime.Rows.Add(arr);
                        }
                        gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                    }
                }
                return mesazh;
            }
            catch (Exception ex)
            {
                LogManager.GetCurrentClassLogger().Error(ex.Message);
                error = ex.Message;
                if (error == "")
                {
                    error = MessagesResource.Messages["msgGagimIPanohur"];
                }
                try
                {
                    DateTime datedok = new DateTime();
                    bool dateVlefshme = DateTime.TryParse(dokTable.Rows[0][dtDokumentiEmerImport].ToString(), out datedok);
                    if (dateVlefshme)
                    {
                        object[] arr = { dokTable.Rows[0][nrDokumentiEmerImport], MessagesResource.Messages["labelDateDokumenti"] + " " + datedok.Day + "/" + datedok.Month + "/" + datedok.Year + " " + error, indexRreshtImporti - 1 };
                        gabime.Rows.Add(arr);
                    }
                    else
                    {
                        object[] arr = { dokTable.Rows[0][nrDokumentiEmerImport].ToString(), error, indexRreshtImporti };
                        gabime.Rows.Add(arr);
                    }
                    if (importo)
                    {
                        foreach (DataRow dr in dokTable.Rows)
                        {
                            if (tePaImportuara.Select(String.Format("Id = '{0}'", dr["Id"])).Count() == 0)
                                tePaImportuara.ImportRow(dr);
                        }
                        gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                    }
                }
                catch (Exception err)
                {
                    LogManager.GetCurrentClassLogger().Error(err.Message);
                    object[] arr = { dokTable.Rows[0][nrDokumentiEmerImport].ToString(), error, indexRreshtImporti };
                    gabime.Rows.Add(arr);
                }
                return new clsMesazh(false, error);
            }
        }
        private colAmortizimiTrupiAbstract krijoTrupAmortizimi(DataTable dokTrupi, colTrupiFormatImporti col, int pozicionkodi, DataTable gabime, DataTable tePaImportuara, bool importo, out bool isMagENjejte, string nrDokumentiEmerImport, string dtDokumentiEmerImport, bool kontrolloserial, List<double> amortizimiFillestar, ref int indexRreshtImporti)
        {
            string error = "";
            int j = 1;
            colAmortizimiTrupiAbstract colTrupi;
            if (IdKategoria != 150)
                colTrupi = new colAmortizimiTrupi();
            else
                colTrupi = new colAmortizimiTrupiRezerva();

            int idMagTemp = -1;
            isMagENjejte = true;
            foreach (DataRow dr in dokTrupi.Rows)
            {
                try
                {
                    error = "";
                    clsAmortizimiTrupiAbstract trupi;
                    if (IdKategoria != 150)
                        trupi = new clsAmortizimiTrupi();
                    else
                        trupi = new clsAmortizimiTrupiRezerva();
                    string kodi = "", magazina = "", seriale = "";
                    double amortizimVjetor = 0, amortizimGjithsej = 0, amortizimFillestar = 0, gjendja = 0;
                    DateTime dtDok = new DateTime();

                    foreach (clsTrupiFormatImporti trup in col)
                    {
                        #region trupi
                        switch (trup.KodKontrolli)
                        {
                            case "Kodi i Artikullit":
                                kodi = clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                            case "Amortizim Vjetor":
                                amortizimVjetor = clsFunksione.vendosDouble(trup, dr, out error);
                                break;
                            case "Amortizim Gjithsej":
                                amortizimGjithsej = clsFunksione.vendosDouble(trup, dr, out error);
                                break;
                            case "Amortizim Fillestar":
                                amortizimFillestar = clsFunksione.vendosDouble(trup, dr, out error);
                                break;
                            case "Magazina":
                                magazina = clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                            case "Seriale":
                                seriale = clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                            case "Date Dokumenti":
                                dtDok = clsFunksione.vendosDate(trup, dr, out error);
                                if (dtDok == DateTime.MinValue)
                                    dtDok = DateTime.Today;
                                break;
                            case "Gjendja":
                                gjendja = clsFunksione.vendosDouble(trup, dr, out error);
                                break;
                        }
                        #endregion
                        if (error != "")
                        {
                            try
                            {
                                DateTime datedok = new DateTime();
                                bool dateVlefshme = DateTime.TryParse(dr[dtDokumentiEmerImport].ToString(), out datedok);
                                if (dateVlefshme)
                                {
                                    object[] arr = { dr[nrDokumentiEmerImport], MessagesResource.Messages["labelDateDokumenti"] + " " + datedok.Day + "/" + datedok.Month + "/" + datedok.Year + ", Rreshti " + j + " :" + trup.EmerImporti + " duhet te jete numer!", indexRreshtImporti };
                                    gabime.Rows.Add(arr);
                                }
                                else
                                {
                                    object[] arr = { dr[nrDokumentiEmerImport].ToString(), error, indexRreshtImporti };
                                    gabime.Rows.Add(arr);
                                }
                            }
                            catch (Exception ex)
                            {
                                LogManager.GetCurrentClassLogger().Error(ex.Message);
                                object[] arr = { dr[nrDokumentiEmerImport].ToString(), error, indexRreshtImporti };
                                gabime.Rows.Add(arr);
                            }
                            if (importo)
                            {
                                tePaImportuara.ImportRow(dr);
                                gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                            }
                        }
                    }

                    clsMesazh mesazh = new clsMesazh();

                    mesazh = trupi.krijoTrupAmortizimiPerImport(kodi, magazina, seriale, amortizimVjetor, amortizimGjithsej, amortizimFillestar, IdNdermarrja, IdPerdoruesi, kontrolloserial, amortizimiFillestar, dtDok, gjendja);
                    if (mesazh.Status)
                    {
                        if (idMagTemp == -1)
                            idMagTemp = trupi.IdNjesiAdministrative;
                        else
                            if (isMagENjejte && trupi.IdNjesiAdministrative != idMagTemp)
                            isMagENjejte = false;
                        colTrupi.Add(trupi);
                        indexRreshtImporti++; j++;
                    }
                    else
                        throw new MyException(mesazh.PershkrimMesazhi);
                }
                catch (MyException ex)
                {
                    LogManager.GetCurrentClassLogger().Error(ex.Message);
                    try
                    {
                        DateTime datedok = new DateTime();
                        bool dateVlefshme = DateTime.TryParse(dr[dtDokumentiEmerImport].ToString(), out datedok);
                        if (dateVlefshme)
                        {
                            error = MessagesResource.Messages["labelDateDokumenti"] + " " + datedok.Day + "/" + datedok.Month + "/" + datedok.Year + ", Rreshti " + j + ": " + ex.Message;
                            object[] arr = { dr[nrDokumentiEmerImport], error, indexRreshtImporti };
                            gabime.Rows.Add(arr);
                        }
                        else
                        {
                            object[] arr = { dr[nrDokumentiEmerImport].ToString(), ex.Message, indexRreshtImporti };
                            gabime.Rows.Add(arr);
                        }
                    }
                    catch (Exception err)
                    {
                        LogManager.GetCurrentClassLogger().Error(err.Message);
                        object[] arr = { dr[nrDokumentiEmerImport].ToString(), ex.Message, indexRreshtImporti };
                        gabime.Rows.Add(arr);
                    }
                    if (importo)
                    {
                        tePaImportuara.ImportRow(dr);
                        gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                    }
                    indexRreshtImporti++; j++;
                }
            }
            return colTrupi;
        }
        #endregion

        private clsMesazh kontrolloListOrari(DataTable dt, DataTable gabime, ref DataTable tePaImportuara, bool importo, int pozicionkodi, colTrupiFormatImporti col)
        {
            try
            {
                string error = "";

                int i = 1;
                foreach (DataRow dr in dt.Rows)
                {
                    clsListOrare list = new clsListOrare();
                    string kodi = "", emer = "", mbiemer = "", simboli = "";

                    DateTime datefillimi = new DateTime();
                    DateTime datembarimi = new DateTime();
                    DateTime datalist = new DateTime();

                    DateTime.TryParse(col.Find(x => x.KodKontrolli == "Fillim periudhe").VleraDefault, out datefillimi);
                    DateTime.TryParse(col.Find(x => x.KodKontrolli == "Mbarim periudhe").VleraDefault, out datembarimi);
                    foreach (clsTrupiFormatImporti trup in col)
                    {
                        error = "";
                        switch (trup.KodKontrolli)
                        {
                            case "Kod punonjesi":
                                kodi = clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                            case "Emer":
                                emer = clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                            case "Mbiemer":
                                mbiemer = clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                            case "Fillim periudhe":
                                break;
                            case "Mbarim periudhe":

                                break;

                        }
                        if (error != "")
                        {
                            object[] arr = { dr[pozicionkodi], trup.EmerImporti + " " + MessagesResource.Messages["msgDuhetTeJeteNumer"], i };
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
                        continue;
                    clsDatabazeListPagesa dblistpagesa = new clsDatabazeListPagesa();
                    if (importo)
                        dblistpagesa.beginTransaksion();///fillojme transaksionin per rastin kur ndonje nga simbolet nuk ekziston te kthehen mbrapsht datat e insertuara para simbolit qe nuk ekziston
                    bool rollback = false;
                    try
                    {

                        for (DateTime data = datefillimi; data <= datembarimi; data = data.AddDays(1))
                        {
                            clsTrupiFormatImporti trupdata = new clsTrupiFormatImporti();
                            trupdata.EmerImporti = data.ToString("dddd, d MMMM yyyy", Ci);
                            trupdata.Visible = true;
                            trupdata.Shfaq = true;
                            simboli = clsFunksione.vendosVlere(trupdata, dr, out error);
                            datalist = data;

                            list = list.krijoPerImport(kodi.RemoveSpaces(), emer.RemoveSpaces(), mbiemer, datalist, simboli, IdPerdoruesi, IdPerdoruesi, IdNdermarrja, 1, dblistpagesa);


                            if (importo)
                            {
                                clsMesazh mesazhinv = list.ruaj(dblistpagesa);
                                if (!mesazhinv.Status)
                                {
                                    if (importo) dblistpagesa.rollbackTransaksion();
                                    rollback = true;
                                    object[] arr = { dr[pozicionkodi], mesazhinv.PershkrimMesazhi, i };
                                    gabime.Rows.Add(arr);
                                    tePaImportuara.ImportRow(dr);
                                    gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LogManager.GetCurrentClassLogger().Error(ex.Message);
                        rollback = true;
                        if (importo) dblistpagesa.rollbackTransaksion();
                        error = ex.Message;
                    }

                    finally
                    {
                        if (!rollback)
                            if (importo) dblistpagesa.commitTransaksion();
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

                }
                if (importo)
                    Status = "import";

                return new clsMesazh(true);
            }
            catch (Exception err)
            {
                LogManager.GetCurrentClassLogger().Error(err.Message);
                return new clsMesazh(false);
            }
        }

        private clsMesazh KontrolloOseImportoObjekteListPagese(DataTable dt, DataTable gabime, ref DataTable tePaImportuara, bool importo, int pozicionkodi, bool mbishkruajVleratEMeparshme, colTrupiFormatImporti col)
        {
            int kodMesazhi = 0;
            try
            {
                switch (IdKategoria)
                {
                    case 99:
                        colOreShtese.Import(dt, col, gabime, tePaImportuara, pozicionkodi, IdPerdoruesi, IdNdermarrja, VitiNdermarrjes, mbishkruajVleratEMeparshme, ref kodMesazhi, importo);
                        break;
                    case 111:
                        colKomponenteNr.Import(dt, col, gabime, tePaImportuara, pozicionkodi, IdPerdoruesi, IdNdermarrja, VitiNdermarrjes, mbishkruajVleratEMeparshme, ref kodMesazhi, importo);
                        break;
                }
                if (importo)
                    Status = "import";

                return new MesazhSuksesi { KodMesazhi = kodMesazhi };
            }
            catch (Exception ex)
            {
                ImbLogger.Error(ex);
                return new MesazhGabimi { KodMesazhi = kodMesazhi };
            }
        }

        private clsMesazh kontrolloDiteLeje(DataTable dt, DataTable gabime, ref DataTable tePaImportuara, bool importo, int pozicionkodi, colTrupiFormatImporti col)
        {
            try
            {
                string error = "";

                int i = 1;
                foreach (DataRow dr in dt.Rows)
                {
                    clsDiteLeje list = new clsDiteLeje();
                    string kodi = "", emer = "", mbiemer = "";
                    decimal totali = 0;

                    DateTime dtfillimi = new DateTime();
                    DateTime dtmbarimi = new DateTime();



                    foreach (clsTrupiFormatImporti trup in col)
                    {
                        error = "";
                        switch (trup.KodKontrolli)
                        {
                            case "Kod punonjesi":
                                kodi = clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                            case "Emer":
                                emer = clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                            case "Mbiemer":
                                mbiemer = clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                            case "Date fillimi":
                                dtfillimi = clsFunksione.vendosDate(trup, dr, out error);
                                break;
                            case "Date mbarimi":
                                dtmbarimi = clsFunksione.vendosDate(trup, dr, out error);
                                break;

                            case "Nr ditesh":
                                totali = clsFunksione.vendosDecimal(trup, dr, out error);
                                break;

                        }
                        if (error != "")
                        {
                            object[] arr = { dr[pozicionkodi], error, i };// trup.EmerImporti + " duhet te jete numer!", i };
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
                        continue;

                    try
                    {



                        list = list.krijoPerImport(kodi.RemoveSpaces(), emer.RemoveSpaces(), mbiemer, dtfillimi, dtmbarimi, totali, IdPerdoruesi, IdPerdoruesi, IdNdermarrja, 1, mySessionObjects.ktheVitiNdermarrjes(Session));


                        if (importo)
                        {
                            clsMesazh mesazhinv = list.ruaj();
                            if (!mesazhinv.Status)
                            {

                                object[] arr = { dr[pozicionkodi], mesazhinv.PershkrimMesazhi, i };
                                gabime.Rows.Add(arr);
                                tePaImportuara.ImportRow(dr);
                                gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        LogManager.GetCurrentClassLogger().Error(ex.Message);
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

                }
                if (importo)
                    Status = "import";

                return new clsMesazh(true);
            }
            catch (Exception ex)
            {
                LogManager.GetCurrentClassLogger().Error(ex.Message);
                return new clsMesazh(false);
            }
        }

        private clsMesazh kontrolloKontrolliMjekesor(DataTable dt, DataTable gabime, ref DataTable tePaImportuara, bool importo, int pozicionkodi, colTrupiFormatImporti col)
        {
            try
            {
                string error = "";

                int i = 1;
                foreach (DataRow dr in dt.Rows)
                {
                    clsKontrolliMjekesor list = new clsKontrolliMjekesor();
                    string kodi = "", emer = "", mbiemer = "";


                    DateTime data = new DateTime();



                    foreach (clsTrupiFormatImporti trup in col)
                    {
                        error = "";
                        switch (trup.KodKontrolli)
                        {
                            case "Kod punonjesi":
                                kodi = clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                            case "Emer":
                                emer = clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                            case "Mbiemer":
                                mbiemer = clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                            case "Data":
                                data = clsFunksione.vendosDate(trup, dr, out error);
                                break;


                        }
                        if (error != "")
                        {
                            object[] arr = { dr[pozicionkodi], error, i };// trup.EmerImporti + " duhet te jete numer!", i };
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
                        continue;

                    try
                    {



                        list = list.krijoPerImport(kodi.RemoveSpaces(), emer.RemoveSpaces(), mbiemer, data, IdPerdoruesi, IdPerdoruesi, IdNdermarrja, 1, mySessionObjects.ktheVitiNdermarrjes(Session));


                        if (importo)
                        {
                            clsMesazh mesazhinv = list.ruaj();
                            if (!mesazhinv.Status)
                            {

                                object[] arr = { dr[pozicionkodi], mesazhinv.PershkrimMesazhi, i };
                                gabime.Rows.Add(arr);
                                tePaImportuara.ImportRow(dr);
                                gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        LogManager.GetCurrentClassLogger().Error(ex.Message);
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

                }
                if (importo)
                    Status = "import";

                return new clsMesazh(true);
            }
            catch (Exception ex)
            {
                LogManager.GetCurrentClassLogger().Error(ex.Message);
                return new clsMesazh(false);
            }
        }

        private clsMesazh kontrolloPagaDheShtesa(DataTable dt, DataTable gabime, ref DataTable tePaImportuara, bool importo, int pozicionkodi, colTrupiFormatImporti col)
        {
            try
            {
                string error = "";

                int i = 1;
                foreach (DataRow dr in dt.Rows)
                {
                    clsPagaShtesa list = new clsPagaShtesa();
                    string kodi = "", emer = "", mbiemer = "", komponente = "";
                    decimal totali = 0;

                    DateTime data = new DateTime();


                    foreach (clsTrupiFormatImporti trup in col)
                    {
                        error = "";
                        switch (trup.KodKontrolli)
                        {
                            case "Kod punonjesi":
                                kodi = clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                            case "Emer":
                                emer = clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                            case "Mbiemer":
                                mbiemer = clsFunksione.vendosVlere(trup, dr, out error);
                                break;

                            case "Data":
                                data = clsFunksione.vendosDate(trup, dr, out error);
                                break;
                            case "Vlera":
                                totali = clsFunksione.vendosDecimal(trup, dr, out error);
                                break;
                            case "Komponente":
                                komponente = clsFunksione.vendosVlere(trup, dr, out error);
                                break;

                        }
                        if (error != "")
                        {
                            object[] arr = { dr[pozicionkodi], error, i };// trup.EmerImporti + " duhet te jete numer!", i };
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
                        continue;

                    try
                    {



                        list = list.krijoPerImport(kodi.RemoveSpaces(), emer.RemoveSpaces(), mbiemer, data, totali, IdPerdoruesi, IdNdermarrja, komponente, mySessionObjects.ktheVitiNdermarrjes(Session));


                        if (importo)
                        {
                            clsMesazh mesazhinv = list.ruaj(IdNdermarrja);
                            if (!mesazhinv.Status)
                            {

                                object[] arr = { dr[pozicionkodi], mesazhinv.PershkrimMesazhi, i };
                                gabime.Rows.Add(arr);
                                tePaImportuara.ImportRow(dr);
                                gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        LogManager.GetCurrentClassLogger().Error(ex.Message);
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

                }
                if (importo)
                    Status = "import";

                return new clsMesazh(true);
            }
            catch (Exception ex)
            {
                LogManager.GetCurrentClassLogger().Error(ex.Message);
                return new clsMesazh(false);
            }
        }

        private clsMesazh kontrolloKomponenteListpagesePunonjesi(DataTable dt, DataTable gabime, ref DataTable tePaImportuara, bool importo, int pozicionkodi, colTrupiFormatImporti col)
        {
            try
            {
                string error = "";

                int i = 1;
                foreach (DataRow dr in dt.Rows)
                {
                    clsKomponenteListPagesePunonjesi list = new clsKomponenteListPagesePunonjesi();
                    string kodi = "", emer = "", mbiemer = "", komponente = "";
                    decimal totali = 0;

                    DateTime data = new DateTime();


                    foreach (clsTrupiFormatImporti trup in col)
                    {
                        error = "";
                        switch (trup.KodKontrolli)
                        {
                            case "Kod punonjesi":
                                kodi = clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                            case "Emer":
                                emer = clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                            case "Mbiemer":
                                mbiemer = clsFunksione.vendosVlere(trup, dr, out error);
                                break;

                            case "Data":
                                data = clsFunksione.vendosDate(trup, dr, out error);
                                break;
                            case "Vlera":
                                totali = clsFunksione.vendosDecimal(trup, dr, out error);
                                break;
                            case "Komponente":
                                komponente = clsFunksione.vendosVlere(trup, dr, out error);
                                break;

                        }
                        if (error != "")
                        {
                            object[] arr = { dr[pozicionkodi], error, i };// trup.EmerImporti + " duhet te jete numer!", i };
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
                        continue;

                    try
                    {



                        list = list.krijoPerImport(kodi.RemoveSpaces(), emer.RemoveSpaces(), mbiemer, data, totali, IdPerdoruesi, IdNdermarrja, komponente, mySessionObjects.ktheVitiNdermarrjes(Session));


                        if (importo)
                        {
                            clsMesazh mesazhinv = list.ruaj(IdNdermarrja);
                            if (!mesazhinv.Status)
                            {

                                object[] arr = { dr[pozicionkodi], mesazhinv.PershkrimMesazhi, i };
                                gabime.Rows.Add(arr);
                                tePaImportuara.ImportRow(dr);
                                gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        LogManager.GetCurrentClassLogger().Error(ex.Message);
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
                }
                if (importo)
                    Status = "import";

                return new clsMesazh(true);
            }
            catch (Exception ex)
            {
                LogManager.GetCurrentClassLogger().Error(ex.Message);
                return new clsMesazh(false);
            }
        }

        private clsMesazh kontrolloRecepturaArtikulli(DataTable dt, DataTable gabime, ref DataTable tePaImportuara, bool importo, int pozicionkodi, colTrupiFormatImporti col)
        {
            try
            {
                string error = "";
                colArtikulliPerberes colartper = new colArtikulliPerberes();
                int i = 1; string ndermarrjeKey = "";
                foreach (DataRow dr in dt.Rows)
                {
                    clsArtikulliPerberes artPerbere = new clsArtikulliPerberes();
                    string kodArtikulli = "", lloji = "", kodi = "";
                    decimal koeficienti = 0, firoligjore = 0;
                    int idImportSQL = (dr.Table.Columns["IDIMPORTSHITJE"] != null) ? Convert.ToInt16(dr["IDIMPORTSHITJE"].ToString()) : 0;

                    DateTime data = new DateTime();


                    foreach (clsTrupiFormatImporti trup in col)
                    {
                        error = "";
                        switch (trup.KodKontrolli)
                        {
                            case "Kod Artikulli":
                                kodArtikulli = clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                            case "Lloji":
                                lloji = clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                            case "Kodi":
                                kodi = clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                            case "Date Aktivizimi":
                                data = clsFunksione.vendosDate(trup, dr, out error);
                                break;
                            case "Koeficienti":
                                koeficienti = clsFunksione.vendosDecimal(trup, dr, out error);
                                break;
                            case "Firo ligjore":
                                firoligjore = clsFunksione.vendosDecimal(trup, dr, out error);
                                break;
                            case "IDIMPORTSHITJE":
                                idImportSQL = clsFunksione.vendosInt(trup, dr, out error);
                                break;
                            case "Kod Ndermarrje":
                                ndermarrjeKey = trup.EmerImporti;
                                break;
                        }
                        if (error != "")
                        {
                            object[] arr = { dr[pozicionkodi], error, i };// trup.EmerImporti + " duhet te jete numer!", i };
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
                        continue;

                    try
                    {
                        artPerbere = artPerbere.krijoPerImport(kodArtikulli.RemoveSpaces(), lloji.RemoveSpaces(), kodi.RemoveSpaces(), data, koeficienti, firoligjore, IdNdermarrja, idImportSQL);
                        artPerbere.IdArtikulliPerberes = i - 1;
                        colartper.Add(artPerbere);
                    }
                    catch (Exception ex)
                    {
                        LogManager.GetCurrentClassLogger().Error(ex.Message);
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
                }

                if (importo)
                {
                    IEnumerable<IGrouping<int, clsArtikulliPerberes>> recSipasArt = colartper.GroupBy(x => x.IdArtikulliKryesor);
                    for (int a = 0; a < recSipasArt.Count(); a++)
                    {
                        string kod = clsArtikulli.ktheKodArtikulliSipasId(recSipasArt.ElementAt(a).Key);
                        if (tePaImportuara.Select("[Kod Artikulli]='" + kod + "'").Length > 0)//nqs nje nga recepturat ka probleme nuk importohet asnje per ate artikull
                        {
                            for (int j = 0; j < recSipasArt.ElementAt(a).Count(); j++)
                            {
                                clsArtikulliPerberes r = recSipasArt.ElementAt(a).ElementAt(j);
                                tePaImportuara.ImportRow(dt.Rows[r.IdArtikulliPerberes]);
                            }
                        }
                        else
                        {
                            colArtikulliPerberes recepturaart = new colArtikulliPerberes();
                            for (int j = 0; j < recSipasArt.ElementAt(a).Count(); j++)
                            {
                                clsArtikulliPerberes r = recSipasArt.ElementAt(a).ElementAt(j);
                                recepturaart.Add(r);
                            }

                            clsMesazh mesazhinv = recepturaart.ruaj(IdNdermarrja, TabKoka, ndermarrjeKey);
                            if (!mesazhinv.Status)
                            {
                                foreach (clsArtikulliPerberes r in recepturaart)
                                {
                                    object[] arr = { kod, mesazhinv.PershkrimMesazhi, r.IdArtikulliPerberes };
                                    gabime.Rows.Add(arr);

                                    tePaImportuara.ImportRow(dt.Rows[r.IdArtikulliPerberes]);
                                    gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                                }
                            }
                        }
                    }

                    Status = "import";
                }
                return new clsMesazh(true);
            }
            catch (Exception ex)
            {
                LogManager.GetCurrentClassLogger().Error(ex.Message);
                return new clsMesazh(false);
            }
        }

        private void kontrolloKlientMeKupon(DataTable dt, DataTable gabime, ref DataTable tePaImportuara, bool importo, int pozicionKodi, colTrupiFormatImporti col)
        {
            var error = "";

            int i = 1;
            foreach (DataRow dr in dt.Rows)
            {
                clsKlientMeKupon klientMeKupon = new clsKlientMeKupon();
                string kodi = "", msisdn = "", aparati = "";
                bool statusi = false;
                decimal vleraZbritjes = 0;
                DateTime dtSkadimi = default(DateTime);
                foreach (clsTrupiFormatImporti trup in col)
                {

                    error = "";
                    switch (trup.KodKontrolli)
                    {
                        case "Kodi i kuponit":
                            kodi = clsFunksione.vendosVlere(trup, dr, out error);
                            break;

                        case "MSISDN":
                            msisdn = clsFunksione.vendosVlere(trup, dr, out error);
                            break;

                        case "Aparati":
                            aparati = clsFunksione.vendosVlere(trup, dr, out error);
                            break;

                        case "Vlera e zbritjes":
                            vleraZbritjes = clsFunksione.vendosDecimal(trup, dr, out error);
                            break;

                        case "Date skadimi":
                            dtSkadimi = clsFunksione.vendosDate(trup, dr, out error);
                            break;
                        case "Statusi":
                            statusi = clsFunksione.vendosBool(trup, dr, out error);
                            break;
                    }
                    if (error != "")
                    {
                        object[] arr = { dr[pozicionKodi], trup.EmerImporti + " duhet te jete numer!", i };
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
                    continue;

                try
                {
                    klientMeKupon = klientMeKupon.KrijoKlientKuponPerImport(kodi.RemoveSpaces(), aparati.RemoveSpaces(), msisdn, vleraZbritjes, statusi, dtSkadimi, IdPerdoruesi);

                    if (importo)
                    {
                        clsMesazh mesazhinv = klientMeKupon.Ruaj();

                        if (!mesazhinv.Status)
                        {
                            object[] arr = { dr[pozicionKodi], mesazhinv.PershkrimMesazhi, i };
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
                        object[] arr = { dr[pozicionKodi], error, i };
                        gabime.Rows.Add(arr);
                        if (importo)
                        {
                            tePaImportuara.ImportRow(dr);
                            gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                        }
                    }
                    i++;
                }
            }
            if (importo)
                Status = "import";

        }

        private void kontrolloKlientPerBazaar(DataTable dt, DataTable gabime, ref DataTable tePaImportuara, bool importo, int pozicionKodi, colTrupiFormatImporti col)
        {
            string error = "";

            int i = 1;
            foreach (DataRow dr in dt.Rows)
            {
                clsKlientPerBazaar klientPerBazaar = new clsKlientPerBazaar();
                string msisdn = "";
                bool statusi = false;
                LlojMsisdn lloji = 0;
                foreach (clsTrupiFormatImporti trup in col)
                {

                    error = "";
                    switch (trup.KodKontrolli)
                    {

                        case "MSISDN":
                            msisdn = clsFunksione.vendosVlere(trup, dr, out error);
                            break;
                        case "Statusi":
                            statusi = clsFunksione.vendosBool(trup, dr, out error);
                            break;
                        case "Lloji":
                            Enum.TryParse(clsFunksione.vendosVlere(trup, dr, out error), out lloji);
                            break;
                    }
                    if (error != "")
                    {
                        object[] arr = { dr[pozicionKodi], trup.EmerImporti + " duhet te jete numer!", i };
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
                    continue;

                try
                {
                    klientPerBazaar = klientPerBazaar.KrijoKlientPerBazaarPerImport(msisdn.RemoveSpaces(), lloji, IdPerdoruesi);

                    if (importo)
                    {
                        clsMesazh mesazhinv = klientPerBazaar.Ruaj();

                        if (!mesazhinv.Status)
                        {
                            object[] arr = { dr[pozicionKodi], mesazhinv.PershkrimMesazhi, i };
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
                        object[] arr = { dr[pozicionKodi], error, i };
                        gabime.Rows.Add(arr);
                        if (importo)
                        {
                            tePaImportuara.ImportRow(dr);
                            gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                        }
                    }
                    i++;
                }
            }
            if (importo)
                Status = "import";
        }

        private void KontrolloKategoriShpenzimi(DataTable dt, DataTable gabime, ref DataTable tePaImportuara, bool importo, int pozicionkodi, colTrupiFormatImporti col)
        {
            try
            {
                var error = "";

                var i = 1;
                foreach (DataRow dr in dt.Rows)
                {
                    string kodi = "",
                        pershkrimi = "",
                        kodPrindi = "";
                    var aktiv = false;

                    foreach (var trup in col)
                    {
                        error = "";
                        switch (trup.KodKontrolli)
                        {
                            case "Kodi":
                                kodi = clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                            case "Emertimi":
                                pershkrimi = clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                            case "Aktiv":
                                aktiv = clsFunksione.vendosBool(trup, dr, out error);
                                break;
                            case "Prindi":
                                kodPrindi = clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                        }

                        if (error == "")
                            continue;

                        object[] arr =
                            {
                                dr[pozicionkodi],
                                trup.EmerImporti + " " + MessagesResource.Messages["msgDuhetTeJeteNumer"],
                                i
                            };

                        gabime.Rows.Add(arr);

                        if (importo)
                        {
                            tePaImportuara.ImportRow(dr);
                            gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                        }
                        break;
                    }

                    if (error != "")
                        continue;

                    try
                    {
                        var kategoriShpenzimi = clsKategoriShpenzimi.KrijoKategoriShpenzimiNgaImporti(kodi, pershkrimi, aktiv, kodPrindi, IdNdermarrja, IdPerdoruesi);
                        if (importo)
                        {
                            var mesazh = kategoriShpenzimi.Ruaj(IdNdermarrjeVit, false);
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
                        LogManager.GetCurrentClassLogger().Error(ex.Message);
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
                }
                if (importo)
                    Status = "import";
            }
            catch (Exception err)
            {
                LogManager.GetCurrentClassLogger().Error(err.Message);
            }
        }
    }
}
